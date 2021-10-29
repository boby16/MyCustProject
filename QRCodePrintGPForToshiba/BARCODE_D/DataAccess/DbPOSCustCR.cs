using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Collections;


namespace Sunlike.Business.Data
{
    public class DbPOSCustCR : DbObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr"></param>
        public DbPOSCustCR(string connStr)
            : base(connStr)
        {
        }

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string cardNo, bool onlyFillSchema)
        {
            string _sql = "SELECT CARD_NO,CUS_NO,CARD_DD,USR,SYS_DATE, STATUS_ID,NEW_NO,DEP,SN_NO"
               + " FROM CUST_CARD"
               + " WHERE CARD_NO=@CARD_NO;"
               + " SELECT CARD_NO,SUB_NO,START_DD,END_DD,CTYPE_NO,AMTN_PAY,CHG_USR_NO,CHG_DD,OLDSUB_NO,SAVING_CHK,SYS_CHK,UP,SYS_DATE,USR,FIN_DD,SAL_NO,AMTN FROM CUST_CARD1 WHERE CARD_NO=@CARD_NO;";

            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[] { "CUST_CARD", "CUST_CARD1" }, _spc);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[] { "CUST_CARD", "CUST_CARD1" }, _spc);
            }
            _ds.Relations.Add(new DataRelation("CUST_CARDCUST_CARD1", new DataColumn[] { _ds.Tables["CUST_CARD"].Columns["CARD_NO"] },
                new DataColumn[] { _ds.Tables["CUST_CARD1"].Columns["CARD_NO"] }));
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string cusNo)
        {
            string _sql = " SELECT  CARD_NO,CUS_NO,CARD_DD,USR,SYS_DATE, STATUS_ID,NEW_NO "
                    + " FROM CUST_CARD "
                    + " WHERE CUS_NO=@CUS_NO";
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _spc[0].Value = cusNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "CUST_CARD" }, _spc);
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string cardNo, string subNo)
        {
            string _sql = " SELECT A.CARD_NO,B.NAME AS CTYPE_NAME,A.SUB_NO ,A.START_DD,A.END_DD,A.CTYPE_NO,A.AMTN_PAY,A.AMTN,A.CHG_USR_NO,A.CHG_DD,A.SYS_CHK,A.SAVING_CHK,A.OLDSUB_NO,A.SYS_DATE,A.USR,A.SAL_NO,A.FIN_DD,A.UP,B.DSC_NO,B.VALID_DAY"
                    + " FROM CUST_CARD1 A INNER JOIN CTYPE B ON A.CTYPE_NO=B.CTYPE_NO"
                    + " WHERE CARD_NO=@CARD_NO";

            if (!string.IsNullOrEmpty(subNo))
            {
                _sql += " AND SUB_NO=@SUB_NO";
            }

            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;
            _spc[1] = new SqlParameter("@SUB_NO", SqlDbType.VarChar, 30);
            _spc[1].Value = subNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "CUST_CARD1" }, _spc);
            return _ds;
        }

        /// <summary>
        /// 卡号是否存在
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool CheckCardExist(string cardNo)
        {
            string _sql = "SELECT 1 FROM CUST_CARD WHERE CARD_NO=@CARD_NO";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;

            object _obj = ExecuteScalar(_sql, _spc);
            if (_obj == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <returns></returns>
        public bool CheckCardExist(string cardNo, string subNo)
        {
            string _sql = "SELECT 1 FROM CUST_CARD1 WHERE CARD_NO=@CARD_NO AND SUB_NO=@SUB_NO";
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;
            _spc[1] = new SqlParameter("@SUB_NO", SqlDbType.VarChar, 30);
            _spc[1].Value = subNo;
            object _obj = ExecuteScalar(_sql, _spc);
            if (_obj == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 卡号是否存在购卡记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool CheckCardUse(string cardNo)
        {
            string _sql = "SELECT COUNT(1) FROM CUST_CARD1 WHERE CARD_NO=@CARD_NO";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;

            object _obj = ExecuteScalar(_sql, _spc);

            if (Convert.ToInt16(_obj) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cTypeNo"></param>
        /// <returns></returns>
        public bool CheckCTypeUse(string cTypeNo)
        {
            string _sql = "SELECT COUNT(1) FROM CUST_CARD1 WHERE CTYPE_NO=@CTYPE_NO";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@CTYPE_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = cTypeNo;

            object _obj = ExecuteScalar(_sql, _spc);

            if (Convert.ToInt16(_obj) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <param name="amtn"></param>
        public void UpdatePayAmtn(string cardNo, string subNo, decimal amtn)
        {
            string _sql = "UPDATE CUST_CARD1 SET AMTN_PAY=ISNULL(AMTN_PAY,0)+@AMTN WHERE CARD_NO=@CARD_NO AND SUB_NO=@SUB_NO";
            SqlParameter[] _spc = new SqlParameter[3];
            _spc[0] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;
            _spc[1] = new SqlParameter("@SUB_NO", SqlDbType.VarChar, 30);
            _spc[1].Value = subNo;
            _spc[2] = new SqlParameter("@AMTN", SqlDbType.Decimal);
            _spc[2].Value = amtn;

            ExecuteNonQuery(_sql, _spc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="snNo"></param>
        public void UpdateSnNo(string cardNo, string snNo)
        {
            string _sql = "IF EXISTS(SELECT 1 FROM CUST_CARD WHERE SN_NO=@SN_NO)\n"
                +"UPDATE CUST_CARD SET SN_NO='' WHERE SN_NO=@SN_NO;\n"
                +"UPDATE CUST_CARD SET SN_NO=@SN_NO WHERE CARD_NO=@CARD_NO";
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@CARD_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;
            _spc[1] = new SqlParameter("@SN_NO", SqlDbType.VarChar, 30);
            _spc[1].Value = snNo;

            ExecuteNonQuery(_sql, _spc);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="snNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBySnNo(string snNo)
        {
            string _sql = "SELECT CARD_NO,CUS_NO,CARD_DD,USR,SYS_DATE, STATUS_ID,NEW_NO,DEP,SN_NO"
               + " FROM CUST_CARD"
               + " WHERE SN_NO=@SN_NO";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@SN_NO", SqlDbType.VarChar, 30);
            _spc[0].Value = snNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "CUST_CARD" }, _spc);
            return _ds;
        }
    }
}
