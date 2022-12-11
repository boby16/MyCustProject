using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
namespace Sunlike.Business.Data
{
    public class DbCBac : DbObject
    {
        private string _connStr = "";
         #region contructor
        public DbCBac(string connectionString)
            : base(connectionString)
        {
            _connStr = connectionString;
        }
        #endregion

        #region 取得客户帐户变动单数据
        /// <summary>
        /// 取得客户帐户变动单数据
        /// </summary>
        /// <param name="bcId"></param>
        /// <param name="bcNo"></param>
        /// <param name="isSchema"></param>
        /// <returns>BC_ID,BC_NO,ADD_ID,AMTN,BACC_NO,BACC_SW,BC_DD,BIL_ID,BIL_NO,CHK_MAN,CLS_DATE,CR_NO,CUS_NO,DEP
        /// VOH_NO,DR_NO,MOB_ID,PAY_TYPE,PRT_USR,REM,SAL_NO,SAVE_ID,SYS_DATE,USR
        /// </returns>
        public SunlikeDataSet GetData(string bcId,string bcNo, bool isSchema)
        {
            string _sqlStr = "";
            _sqlStr = " SELECT BC_ID,BC_NO,ADD_ID,AMTN,BACC_NO,BACC_SW,BC_DD,BIL_ID,BIL_NO,CHK_MAN,CLS_DATE,CR_NO,CUS_NO,DEP,"
                    + " VOH_NO,"
                    + " DR_NO,MOB_ID,PAY_TYPE,PRT_SW,PRT_USR,REM,SAL_NO,SAVE_ID,SYS_DATE,USR "
                    + " FROM MF_CBAC "
                    + " WHERE BC_ID= @BC_ID AND BC_NO = @BC_NO ;"
                    + "select BIL_ID,BIL_NO,PAY_REM,PAY_NO,PAY_ID,PAY_DD,REM from PAY_B2C"
                    + " where BIL_ID=@BC_ID and BIL_NO=@BC_NO";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@BC_ID", SqlDbType.VarChar,2);
            _sqlPara[0].Value = bcId;
            _sqlPara[1] = new SqlParameter("@BC_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = bcNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (isSchema)
            {
                this.FillDatasetSchema(_sqlStr, _ds, new string[] { "MF_CBAC", "PAY_B2C" }, _sqlPara);
            }
            else
            {
                this.FillDataset(_sqlStr, _ds, new string[] { "MF_CBAC", "PAY_B2C" }, _sqlPara);
            }
            //表头和B2C在线付款记录关联
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_CBAC"].Columns["BC_ID"];
            _dca1[1] = _ds.Tables["MF_CBAC"].Columns["BC_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["PAY_B2C"].Columns["BIL_ID"];
            _dca2[1] = _ds.Tables["PAY_B2C"].Columns["BIL_NO"];
            _ds.Relations.Add("MF_CBACPAY_B2C", _dca1, _dca2);
            return _ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="bilNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBilNo(string bilId, string bilNo)
        {
            string _sqlStr = " SELECT BC_ID,BC_NO,ADD_ID,AMTN,BACC_NO,BACC_SW,BC_DD,BIL_ID,BIL_NO,CHK_MAN,CLS_DATE,CR_NO,CUS_NO,DEP,"
                    + " VOH_NO,"
                    + " DR_NO,MOB_ID,PAY_TYPE,PRT_SW,PRT_USR,REM,SAL_NO,SAVE_ID,SYS_DATE,USR "
                    + " FROM MF_CBAC "
                    + " WHERE BIL_ID= @BIL_ID AND BIL_NO = @BIL_NO";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = bilId;
            _sqlPara[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = bilNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "MF_CBAC" }, _sqlPara);
			_sqlStr = "select BIL_ID,BIL_NO,PAY_REM,PAY_NO,PAY_ID,PAY_DD,REM from PAY_B2C where ";
            if (_ds.Tables["MF_CBAC"].Rows.Count > 0)
            {
                _sqlStr += "BIL_ID=@BIL_ID and BIL_NO=@BIL_NO";
                _sqlPara = new SqlParameter[2];
                _sqlPara[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
                _sqlPara[0].Value = _ds.Tables["MF_CBAC"].Rows[0]["BC_ID"].ToString();
                _sqlPara[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
                _sqlPara[1].Value = _ds.Tables["MF_CBAC"].Rows[0]["BC_NO"].ToString();
                FillDataset(_sqlStr, _ds, new string[] { "PAY_B2C" }, _sqlPara);
            }
            else
            {
                _sqlStr += "1<>1";
                FillDataset(_sqlStr, _ds, new string[] { "PAY_B2C" });
            }
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_CBAC"].Columns["BC_ID"];
            _dca1[1] = _ds.Tables["MF_CBAC"].Columns["BC_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["PAY_B2C"].Columns["BIL_ID"];
            _dca2[1] = _ds.Tables["PAY_B2C"].Columns["BIL_NO"];
            _ds.Relations.Add("MF_CBACPAY_B2C", _dca1, _dca2);
            return _ds;
        }
        
        #endregion

        #region 取得客户账户余额
        /// <summary>
        /// 取得客户账户余额
        /// </summary>
        /// <param name="cusNo">客户代号</param>
        /// <returns>CUS_NO,AMTN_UNSH_ADD,AMTN_UNSH_SUB,AMTN_SO_UNSH,AMTN_SO,AMTN</returns>
        public SunlikeDataSet GetDataCustBac(string cusNo)
        {
            string _sqlStr = "";
            _sqlStr = " SELECT CUS_NO,AMTN_UNSH_ADD,AMTN_UNSH_SUB,AMTN_SO_UNSH,AMTN_SO,AMTN "
                    + " FROM CUST_BAC "
                    + " WHERE CUS_NO= @CUS_NO ";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _sqlPara[0].Value = cusNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "CUST_BAC" }, _sqlPara);
            return _ds;
        }
        #endregion

        #region 更新客户账户余额
        /// <summary>
        /// 更新客户账户余额
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="updateField"></param>
        public void UpdateCustBac(string cusNo, Dictionary<string, decimal> updateField)
        {
            string _sqlStr = "";
            string _sqlInsertField = "";
            string _sqlInsertValue = "";
            string _sqlUpdate = "";
            if (updateField.Count == 0)
                return;
            foreach (KeyValuePair<string, decimal> _kvp in updateField)
            {
                if (!string.IsNullOrEmpty(_sqlInsertField))
                {
                    _sqlInsertField += ",";
                }
                _sqlInsertField += _kvp.Key;

                if (!string.IsNullOrEmpty(_sqlInsertValue))
                {
                    _sqlInsertValue += ",";
                }
                _sqlInsertValue += "@" + _kvp.Key;

                if (!string.IsNullOrEmpty(_sqlUpdate))
                {
                    _sqlUpdate += ",";
                }
                _sqlUpdate += _kvp.Key + "=ISNULL(" + _kvp.Key + ",0) + " + "@" + _kvp.Key;
            }

            _sqlStr = " IF EXISTS( SELECT CUS_NO FROM CUST_BAC WHERE CUS_NO = @CUS_NO)"
                    + " BEGIN "
                    + " UPDATE CUST_BAC SET " + _sqlUpdate + " WHERE CUS_NO = @CUS_NO "
                    + " END"
                    + " ELSE "
                    + " BEGIN "
                    + " INSERT CUST_BAC (CUS_NO," + _sqlInsertField + ") VALUES ( @CUS_NO," + _sqlInsertValue + ")"
                    + " END";
            SqlParameter[] _sqlPara = new SqlParameter[updateField.Count + 1];
            _sqlPara[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _sqlPara[0].Value = cusNo;
            int _paraCount = 0;
            foreach (KeyValuePair<string, decimal> _kvp in updateField)
            {
                _paraCount += 1;
                _sqlPara[_paraCount] = new SqlParameter("@" + _kvp.Key, SqlDbType.Decimal);
                _sqlPara[_paraCount].Precision = 28;
                _sqlPara[_paraCount].Scale = 8;
                _sqlPara[_paraCount].Value = _kvp.Value;
            }
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        #endregion

        #region 更新审核人
        /// <summary>
        /// 更新审核人
        /// </summary>
        /// <param name="bcId"></param>
        /// <param name="bcNo"></param>
        /// <param name="chkMan"></param>
        /// <param name="clsDate"></param>
        public void UpdateChkMan(string bcId, string bcNo,string chkMan,DateTime clsDate)
        {
            string _where = "";
            _where = " BC_ID=@BC_ID AND BC_NO=@BC_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_CBAC SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@BC_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = bcId;
            _sqlPara[1] = new SqlParameter("@BC_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = bcNo;
            _sqlPara[2] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _sqlPara[2].Value = chkMan;
            _sqlPara[3] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            if (string.IsNullOrEmpty(chkMan))
            {
                _sqlPara[3].Value = System.DBNull.Value;
            }
            else
            {
                _sqlPara[3].Value = clsDate;
            }

            try
            {
                this.ExecuteNonQuery(_sqlStr, _sqlPara);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
           
        }

        #endregion

        #region 更新客户储值单凭证号码
        /// <summary>
        /// 更新客户储值单凭证号码
        /// </summary>
        /// <param name="bcId"></param>
        /// <param name="bcNo"></param>
        /// <param name="vohNo"></param>
        public void UpdateVohNo(string bcId, string bcNo, string vohNo)
        {
            string _where = "";
            _where = " BC_ID=@BC_ID AND BC_NO=@BC_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_CBAC SET VOH_NO=@VOH_NO WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@BC_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = bcId;
            _sqlPara[1] = new SqlParameter("@BC_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = bcNo;
            _sqlPara[2] = new SqlParameter("@VOH_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = vohNo;         
            try
            {
                this.ExecuteNonQuery(_sqlStr, _sqlPara);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
         #endregion

        #region Cust_sbac
        /// <summary>
        /// 取得客户服务账户余额
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataCustSBac(string cusNo, string cardNo, string subNo, string prdNo)
        {
            string _sqlStr = " SELECT CUS_NO,CARD_NO,SUB_NO,PRD_NO,PRD_NAME,NUM_SET,NUM_USE,LAST_DD,DAY_ZQ,UP_FREEZE,GROUP_PRDT"
                    + " FROM CUST_SBAC "
                    + " WHERE CUS_NO= @CUS_NO AND CARD_NO=@CARD_NO";
            if (!string.IsNullOrEmpty(subNo))
            {
                _sqlStr += " AND SUB_NO=@SUB_NO ";
            }
            if (!string.IsNullOrEmpty(prdNo))
            {
                _sqlStr += " AND PRD_NO=@PRD_NO";
            }
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _sqlPara[0].Value = cusNo;
            _sqlPara[1] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 30);
            _sqlPara[1].Value = cardNo;
            _sqlPara[2] = new SqlParameter("@SUB_NO", SqlDbType.VarChar, 30);
            _sqlPara[2].Value = subNo;
            _sqlPara[3] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _sqlPara[3].Value = prdNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "CUST_SBAC" }, _sqlPara);
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="numSet"></param>
        /// <param name="numUse"></param>
        /// <param name="lastDd"></param>
        /// <param name="dayZq"></param>
        /// <param name="upFreeze"></param>
        public void UpdateCustSBac(string cusNo, string cardNo, string subNo, string prdNo, decimal numUse, DateTime? lastDd)
        {
            string _sqlStr = "UPDATE CUST_SBAC SET NUM_USE=ISNULL(NUM_USE,0)+@NUM_USE,LAST_DD=@LAST_DD WHERE CUS_NO= @CUS_NO AND CARD_NO=@CARD_NO AND SUB_NO=@SUB_NO AND PRD_NO=@PRD_NO \n"
            + " IF NOT EXISTS(SELECT 1 FROM CUST_SBAC WHERE CUS_NO= @CUS_NO AND CARD_NO=@CARD_NO AND SUB_NO=@SUB_NO AND ISNULL(GROUP_PRDT,'')='' AND ISNULL(NUM_SET,0)>ISNULL(NUM_USE,0))\n"
            + " UPDATE CUST_CARD1 SET END_DD=Convert(Varchar(10),GetDate(),120) WHERE CARD_NO=@CARD_NO AND SUB_NO=@SUB_NO \n"
            + " ELSE \n"
            + " UPDATE CUST_CARD1 SET END_DD=null WHERE CARD_NO=@CARD_NO AND SUB_NO=@SUB_NO AND ISNULL(END_DD,'')<>''\n";

            SqlParameter[] _sqlPara = new SqlParameter[6];
            _sqlPara[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _sqlPara[0].Value = cusNo;
            _sqlPara[1] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 30);
            _sqlPara[1].Value = cardNo;
            _sqlPara[2] = new SqlParameter("@SUB_NO", SqlDbType.VarChar, 30);
            _sqlPara[2].Value = subNo;
            _sqlPara[3] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _sqlPara[3].Value = prdNo;
            _sqlPara[4] = new SqlParameter("@LAST_DD", SqlDbType.DateTime);
            _sqlPara[4].Value = lastDd;
            _sqlPara[5] = new SqlParameter("@NUM_USE", SqlDbType.Decimal);
            _sqlPara[5].Value = numUse;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }


        public void InsertCustSbac(string cusNo, string cardNo, string subNo, string prdNo, decimal numSet, int dayZq, decimal upFreeze, decimal upZq, string prdName, string groupPrdt)
        {
            string _sqlStr = " INSERT CUST_SBAC (CUS_NO,CARD_NO,SUB_NO,PRD_NO,NUM_SET,DAY_ZQ,UP_FREEZE,UP_ZQ,PRD_NAME,GROUP_PRDT) "
            + "VALUES ( @CUS_NO,@CARD_NO,@SUB_NO,@PRD_NO,@NUM_SET,@DAY_ZQ,@UP_FREEZE,@UP_ZQ,@PRD_NAME,@GROUP_PRDT)";
            SqlParameter[] _sqlPara = new SqlParameter[10];
            _sqlPara[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _sqlPara[0].Value = cusNo;
            _sqlPara[1] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 30);
            _sqlPara[1].Value = cardNo;
            _sqlPara[2] = new SqlParameter("@SUB_NO", SqlDbType.VarChar, 30);
            _sqlPara[2].Value = subNo;
            _sqlPara[3] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _sqlPara[3].Value = prdNo;
            _sqlPara[4] = new SqlParameter("@DAY_ZQ", SqlDbType.Int);
            _sqlPara[4].Value = dayZq;
            _sqlPara[5] = new SqlParameter("@NUM_SET", SqlDbType.Decimal);
            _sqlPara[5].Value = numSet;
            _sqlPara[6] = new SqlParameter("@UP_FREEZE", SqlDbType.Decimal);
            _sqlPara[6].Value = upFreeze;
            _sqlPara[7] = new SqlParameter("@UP_ZQ", SqlDbType.Decimal);
            _sqlPara[7].Value = upZq;
            _sqlPara[8] = new SqlParameter("@PRD_NAME", SqlDbType.VarChar, 100);
            _sqlPara[8].Value = prdName;
            _sqlPara[9] = new SqlParameter("@GROUP_PRDT", SqlDbType.VarChar, 30);
            _sqlPara[9].Value = groupPrdt;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        /// <summary>
        /// 删除客户服务次数
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <param name="idxNo"></param>
        public void DeleteCustSBac(string cusNo, string cardNo, string subNo)
        {
            string _sqlStr = "DELETE FROM CUST_SBAC WHERE  CUS_NO= @CUS_NO AND CARD_NO=@CARD_NO AND SUB_NO=@SUB_NO ";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _sqlPara[0].Value = cusNo;
            _sqlPara[1] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 30);
            _sqlPara[1].Value = cardNo;
            _sqlPara[2] = new SqlParameter("@SUB_NO", SqlDbType.VarChar, 30);
            _sqlPara[2].Value = subNo;
            ExecuteNonQuery(_sqlStr, _sqlPara);
        } 
        #endregion
    }
}
