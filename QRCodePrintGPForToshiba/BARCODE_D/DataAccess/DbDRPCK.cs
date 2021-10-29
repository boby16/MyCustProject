using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
    public class DbDRPCK : DbObject
    {

        #region 构造
        public DbDRPCK(string connectionString)
            : base(connectionString)
        {

        }
        #endregion

        #region 字段属性
        #endregion

        #region 取数据
        /// <summary>
        /// 去数据
        /// </summary>
        /// <param name="ckid"></param>
        /// <param name="ckno"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string ckid, string ckno)
        {
            string _sql = @"SELECT * FROM MF_CK 
                            WHERE CK_NO = @CK_NO AND CK_ID=@CK_ID;
                            SELECT TF.CK_ID, TF.CK_NO, TF.ITM, TF.PRD_NO,TF.PRD_NO AS PRD_NO_NO, TF.PRD_NAME, TF.PRD_MARK, TF.UNIT, TF.WH, TF.QTY, TF.UP, TF.DIS_CNT, TF.AMT, TF.AMTN_NET, 
                              TF.TAX, TF.EST_DD, TF.OS_ID, TF.OS_NO, TF.REM, TF.QTY1, TF.UP_QTY1, TF.CST_STD, TF.TAX_RTO, TF.EST_ITM, TF.VALID_DD, TF.CSTN_SAL, 
                              TF.BAT_NO, TF.QTY_PS, TF.QTY_PS_UNSH, TF.QTY_RK, TF.QTY_RK_UNSH, TF.PAK_UNIT, TF.PAK_EXC, TF.PAK_NW, TF.PAK_WEIGHT_UNIT, 
                              TF.PAK_GW, TF.PAK_MEAST_UNIT, TF.PAK_MEAST, TF.REM AS TFREM, TF.FREE_ID, TF.CK_DD, TF.ID_NO, TF.AMT_FP, TF.AMTN_NET_FP, 
                              TF.TAX_FP, TF.PRICE_ID, TF.OTH_ITM, TF.PRE_ITM, TF.ZHANG_ID, TF.CHK_TAX, TF.SUP_PRD_NO, TF.COMPOSE_IDNO, TF.CUS_OS_NO, TF.REM2, 
                              TF.QTY_FP, TF.FH_NO, TF.QTY_ARK, TF.QTY_PRE, TF.QTY_PRE_UNSH, TF.QTY1_PRE, TF.QTY1_PRE_UNSH, TF.RK_DD, TF.DEP_RK, TF.PW_ITM, 
                              TF.CNTT_NO, TF.BZ_KND, TF.WH_SEND, TF.AMT_DIS_CNT,OS.OS_NO AS OSNO, '' AS UNIT_DP,P.SPC
                            FROM  MF_CK AS MF INNER JOIN
                            TF_CK AS TF ON MF.CK_ID = TF.CK_ID AND MF.CK_NO = TF.CK_NO
                            LEFT JOIN TF_POS OS ON TF.OS_ID=OS.OS_ID AND TF.OS_NO=OS.OS_NO AND OS.EST_ITM=TF.EST_ITM
                            LEFT JOIN PRDT P ON TF.PRD_NO=P.PRD_NO
                            WHERE MF.CK_NO = @CK_NO AND MF.CK_ID=@CK_ID;";
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _sp = new SqlParameter[2];
            _sp[0] = new SqlParameter("@CK_ID", SqlDbType.VarChar, 2);
            _sp[1] = new SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _sp[0].Value = ckid;
            _sp[1].Value = ckno;

            this.FillDataset(_sql, _ds, new String[] { "MF_CK", "TF_CK" }, _sp);
            DataColumn[] _dcPk1 = new DataColumn[2];
            _dcPk1[0] = _ds.Tables["MF_CK"].Columns["CK_ID"];
            _dcPk1[1] = _ds.Tables["MF_CK"].Columns["CK_NO"];
            _ds.Tables["MF_CK"].PrimaryKey = _dcPk1;
            DataColumn[] _dcPk2 = new DataColumn[2];
            _dcPk2[0] = _ds.Tables["TF_CK"].Columns["CK_ID"];
            _dcPk2[1] = _ds.Tables["TF_CK"].Columns["CK_NO"];
            DataColumn[] _dcPk3 = new DataColumn[3];
            _dcPk3[0] = _ds.Tables["TF_CK"].Columns["CK_ID"];
            _dcPk3[1] = _ds.Tables["TF_CK"].Columns["CK_NO"];
            _dcPk3[2] = _ds.Tables["TF_CK"].Columns["ITM"];
            _ds.Tables["TF_CK"].PrimaryKey = _dcPk3;

            _ds.Relations.Add("MF_CKTF_CK", _dcPk1, _dcPk2);
            return _ds;
        }

        /// <summary>
        /// 获取原单数量
        /// </summary>
        /// <param name="bilid">源单单别</param>
        /// <param name="bilno">源单单号</param>
        /// <param name="itm">ITM</param>
        /// <returns>原单数量</returns>
        public decimal GetQtySoOrg(string bilid, string bilno, string itm)
        {
            string tbName = "";
            string qtyField = "QTY";
            string idField = "";
            string noField = "";
            string itmField = "";
            switch (bilid)
            {
                case "SO":
                    tbName = "TF_POS";
                    idField = "OS_ID";
                    noField = "OS_NO";
                    itmField = "EST_ITM";
                    break;
                case "LN":
                    tbName = "TF_BLN";
                    idField = "BL_ID";
                    noField = "BL_NO";
                    itmField = "EST_ITM";
                    break;
                case "CK":
                    tbName = "TF_CK";
                    idField = "CK_ID";
                    noField = "CK_NO";
                    itmField = "PRE_ITM";
                    break;
                case "SA":
                    tbName = "TF_PSS";
                    idField = "PS_ID";
                    noField = "PS_NO";
                    itmField = "OTH_ITM";
                    break;
            }
            string sql = string.Format("SELECT ISNULL({0},0) AS QTY FROM {1} WHERE {2}='{3}' AND {4}='{5}' AND {6}='{7}'", qtyField, tbName, idField, bilid, noField, bilno, itmField, itm);
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(sql, _ds, new string[] { "QTY" });
            if (_ds != null && _ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    string obj = Convert.ToString(_ds.Tables[0].Rows[0][0]);
                    if (!string.IsNullOrEmpty(obj))
                        return Convert.ToDecimal(obj);
                }
            }
            return 0;
        }

        /// <summary>
        /// 得到KB表身对应的CK行的字段OS_ID,OS_NO,_EST_ITM
        /// </summary>
        /// <param name="ckid"></param>
        /// <param name="ckno"></param>
        /// <param name="preitm"></param>
        public void GetOSbyPre(ref string ckid, ref string ckno, ref string preitm)
        {
            string sql = string.Format("SELECT OS_ID,OS_NO,EST_ITM FROM TF_CK WHERE CK_ID='{0}' AND CK_NO='{1}' AND PRE_ITM='{2}'", ckid, ckno, preitm);
            using (SqlDataReader _read = this.ExecuteReader(sql))
            {
                while (_read.Read())
                {
                    ckid = Convert.ToString(_read["OS_ID"]);
                    ckno = Convert.ToString(_read["OS_NO"]);
                    preitm = Convert.ToString(_read["EST_ITM"]);
                }
            }
        }

        /// <summary>
        /// 取对应表身
        /// </summary>
        /// <param name="ckId"></param>
        /// <param name="ckNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string ckId, string ckNo, int preItm)
        {
            string _sql = "SELECT * FROM TF_CK WHERE CK_ID = @CK_ID AND CK_NO = @CK_NO AND PRE_ITM = @PRE_ITM";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@CK_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = ckId;
            _aryPt[1] = new SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = ckNo;
            _aryPt[2] = new SqlParameter("@PRE_ITM", SqlDbType.Int);
            _aryPt[2].Value = preItm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_CK" }, _aryPt);
            return _ds;
        }

        #endregion

        #region 回写原单数据
        public void UpdateOS(string osId, string osNo, string osItm, decimal _qty,string qtyField)
        {
            string tbNameTf = "";//0
            //string qtyField = "QTY_RK";//1
            string idField = "";//2
            string noField = "";//3
            string itmField = "";//4
            string tbNameMf = "";//5
            string osClsid = "";//6
            string upWhere = "";//7
            switch (osId)
            {
                case "SO":
                    tbNameMf = "MF_POS";
                    tbNameTf = "TF_POS";
                    idField = "OS_ID";
                    noField = "OS_NO";
                    itmField = "EST_ITM";
                    osClsid = "RK_CLS_ID";
                    upWhere = "AND ISNULL(QTY,0)-ISNULL(QTY_PRE,0)-ISNULL(QTY_RK,0)>0";
                    break;
                case "LN":
                    tbNameMf = "MF_BLN";
                    tbNameTf = "TF_POS";
                    idField = "BL_ID";
                    noField = "BL_NO";
                    itmField = "EST_ITM";
                    osClsid = "CK_CLS_ID";
                    upWhere = "AND ISNULL(QTY,0)-ISNULL(QTY_RK,0)>0";
                    break;
                case "SA":
                    tbNameMf = "MF_PSS";
                    tbNameTf = "TF_PSS";
                    idField = "PS_ID";
                    noField = "PS_NO";
                    itmField = "OTH_ITM";
                    osClsid = "CK_CLS_ID";
                    upWhere = "AND ISNULL(QTY,0)-ISNULL(QTY_SB,0)-ISNULL(QTY_CK,0)>0";
                    //qtyField = "QTY_CK";//1
                    break;
            }
            string sql = " UPDATE {0} SET {1}=ISNULL({1},0)+@QTY WHERE {2}=@OS_ID AND {3}=@OS_NO AND {4}=@ITM ;"
            + " IF (@@ROWCOUNT > 0) "
            + " BEGIN \n"
            + " UPDATE {5} SET {6}='T'  WHERE {2}=@OS_ID AND {3}=@OS_NO AND (SELECT COUNT({2}) FROM {0} WHERE {2}=@OS_ID AND {3}=@OS_NO {7})=0;"
            + " UPDATE {5} SET {6}='F'  WHERE {2}=@OS_ID AND {3}=@OS_NO AND (SELECT COUNT({2}) FROM {0} WHERE {2}=@OS_ID AND {3}=@OS_NO {7})>0;"
            + " END ";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[0].Value = osId;
            _spc[1].Value = osNo;
            _spc[2].Value = Convert.ToInt16(osItm);
            _spc[3].Value = _qty;

            this.ExecuteNonQuery(string.Format(sql, tbNameTf, qtyField, idField, noField, itmField, tbNameMf, osClsid, upWhere), _spc);

        }
        #endregion

        #region 回写出库表身已交量
        public void UpdateQtyPS(string osId, string osNo, string osItm, decimal _qty)
        {
            string tbNameTf = "TF_CK";//0
            string qtyField = "QTY_PS";//1
            string idField = "CK_ID";//2
            string noField = "CK_NO";//3
            string itmField = "PRE_ITM";//4
            string tbNameMf = "MF_CK";//5
            string osClsid = "SA_CLS_ID";//6
            string upWhere = "AND ISNULL(QTY,0)-ISNULL(QTY_PRE,0)-ISNULL(QTY_PS,0)>0";//7  自动结案标记

            string sql = " UPDATE {0} SET {1}=ISNULL({1},0)+@QTY WHERE {2}=@OS_ID AND {3}=@OS_NO AND {4}=@ITM ;"
            + " IF (@@ROWCOUNT > 0) "
            + " BEGIN \n"
            + " UPDATE {5} SET {6}='T' ,CLSSA='T'  WHERE {2}=@OS_ID AND {3}=@OS_NO AND (SELECT COUNT({2}) FROM {0} WHERE {2}=@OS_ID AND {3}=@OS_NO {7})=0;"
            + " UPDATE {5} SET {6}='F' ,CLSSA='F'  WHERE {2}=@OS_ID AND {3}=@OS_NO AND (SELECT COUNT({2}) FROM {0} WHERE {2}=@OS_ID AND {3}=@OS_NO {7})>0;"
            + " END ";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[0].Value = osId;
            _spc[1].Value = osNo;
            _spc[2].Value = Convert.ToInt16(osItm);
            _spc[3].Value = _qty;

            this.ExecuteNonQuery(string.Format(sql, tbNameTf, qtyField, idField, noField, itmField, tbNameMf, osClsid, upWhere), _spc);
            UpdateYDID(osId, osNo);
        }
        #endregion

        #region 回写出库表身退回量
        public void UpdateQtyPre(string osId, string osNo, string osItm, decimal _qty, decimal _qty1,string _qtyField,string _qtyField1)
        {
            string tbNameTf = "TF_CK";//0
            string qtyField = "QTY_PRE";//1
            string idField = "CK_ID";//2
            string noField = "CK_NO";//3
            string itmField = "PRE_ITM";//4
            string tbNameMf = "MF_CK";//5
            string osClsid = "SA_CLS_ID";//6
            string upWhere = "AND ISNULL(QTY,0)-ISNULL(QTY_PRE,0)-ISNULL(QTY_PS,0)>0";//7  自动结案标记
            string qtyField1 = "QTY1_PRE";//8

            string sql = " UPDATE {0} SET {1}=ISNULL({1},0)+@QTY,{8}=ISNULL({8},0)+@QTY1 WHERE {2}=@OS_ID AND {3}=@OS_NO AND {4}=@ITM ;"
            + " IF (@@ROWCOUNT > 0) "
            + " BEGIN \n"
            + " UPDATE {5} SET {6}='T' ,CLSSA='T'  WHERE {2}=@OS_ID AND {3}=@OS_NO AND (SELECT COUNT({2}) FROM {0} WHERE {2}=@OS_ID AND {3}=@OS_NO {7})=0;"
            + " UPDATE {5} SET {6}='F' ,CLSSA='F'  WHERE {2}=@OS_ID AND {3}=@OS_NO AND (SELECT COUNT({2}) FROM {0} WHERE {2}=@OS_ID AND {3}=@OS_NO {7})>0;"
            + " END ";
            SqlParameter[] _spc = new SqlParameter[5];
            _spc[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[4] = new SqlParameter("@QTY1", SqlDbType.Decimal);
            _spc[0].Value = osId;
            _spc[1].Value = osNo;
            _spc[2].Value = Convert.ToInt16(osItm);
            _spc[3].Value = _qty;
            _spc[4].Value = _qty1;
            this.ExecuteNonQuery(string.Format(sql, tbNameTf, _qtyField, idField, noField, itmField, tbNameMf, osClsid, upWhere, _qtyField1), _spc);

        }
        #endregion

        #region 回写表头YD_ID
        public void UpdateYDID(string osId, string osNo)
        {
            string sql = "UPDATE MF_CK SET YD_ID='T' WHERE CK_ID=@CK_ID AND CK_NO=@CK_NO AND (SELECT sum(isnull(QTY_PS,0)) from tf_ck where CK_ID=@CK_ID AND CK_NO=@CK_NO) =0"
                       + "UPDATE MF_CK SET YD_ID='F' WHERE CK_ID=@CK_ID AND CK_NO=@CK_NO AND (SELECT sum(isnull(QTY_PS,0)) from tf_ck where CK_ID=@CK_ID AND CK_NO=@CK_NO) >0";

            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@CK_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = osId;
            _spc[1].Value = osNo;
            this.ExecuteNonQuery(sql, _spc);
        }

        #endregion

        #region IAuditing
        /// <summary>
        /// 修改结案标记、审核人和审核日期
        /// </summary>
        ///<param name="bil_Id">bil_Id</param>
        /// <param name="bil_No">bil_No</param>
        /// <param name="chk_Man">审核人</param>
        /// <param name="cls_Date">审核日期</param>
        public void UpdateChkMan(string bil_Id, string bil_No, string chk_Man, DateTime cls_Date)
        {
            string _sql = "UPDATE MF_CK SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE CK_NO=@CK_NO AND CK_ID=@CK_ID";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _spc[2] = new SqlParameter("@CK_NO", SqlDbType.VarChar, 38);
            _spc[2].Value = bil_No;
            _spc[3] = new SqlParameter("@CK_ID", SqlDbType.VarChar, 38);
            _spc[3].Value = bil_Id;

            if (string.IsNullOrEmpty(chk_Man))
            {
                _spc[0].Value = System.DBNull.Value;
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = chk_Man;
                _spc[1].Value = cls_Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion

        #region 结案作业

        /// <summary>
        /// 在单据上打上结案标记
        /// </summary>
        /// <param name="otId"></param>
        /// <param name="otNo"></param>
        /// <param name="cls_name"></param>
        /// <param name="close"></param>
        public void DoCloseBill(string otId, string otNo, string cls_name, bool close)
        {
            string _sqlStr = " UPDATE MF_CK SET SA_CLS_ID=@CLS_ID WHERE  CK_ID=@CK_ID AND CK_NO=@CK_NO";
            if (System.Collections.CaseInsensitiveComparer.Default.Compare(cls_name, "LZ_CLS_ID") == 0)
                _sqlStr = " UPDATE MF_CK SET LZ_CLS_ID=@CLS_ID WHERE  CK_ID=@CK_ID AND CK_NO=@CK_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@CK_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = otId;
            _sqlPara[1] = new SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = otNo;
            _sqlPara[2] = new SqlParameter("@CLS_ID", SqlDbType.VarChar, 1);
            _sqlPara[2].Value = close ? "T" : "F";

            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }

        #endregion
    }
}
