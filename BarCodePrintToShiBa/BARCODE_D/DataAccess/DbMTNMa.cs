using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;


namespace Sunlike.Business.Data
{
    /// <summary>
    /// 维修申请单
    /// </summary>
    public class DbMTNMa:DbObject
    {
        #region constructor
        /// <summary>
        /// 维修申请单
        /// </summary>
        /// <param name="connectionString"></param>
        public DbMTNMa(string connectionString):base(connectionString)
        {
 
        }
        #endregion

        #region 取数据
        /// <summary>
        /// 取维修申请单数据
        /// </summary>
        /// <param name="maId">申请单据别</param>
        /// <param name="maNo">申请单号</param>
        /// <param name="isSchema">是否取schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string maId,string maNo,bool isSchema)
        {
            string _sqlStr = "";
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MA_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = maId;
            _sqlPara[1] = new SqlParameter("@MA_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = maNo;
            _sqlStr = " SELECT A.MA_ID,A.MA_NO,A.MA_DD,A.CUS_NO,A.SAL_NO,A.DEP,A.CNT_NO,A.CNT_REM,A.BIL_TYPE,A.REM,"
                    + " A.SYS_DATE,A.CLS_DATE,A.PRT_SW,A.USR,A.CHK_MAN,A.CLS_ID,A.CLS_AUTO,A.MTN_FLOW,A.EST_DD,A.LOCK_MAN,A.QA_NO,"
                    + " A.CNT_NAME,A.OTH_NAME,A.TEL_NO,A.CELL_NO,A.CNT_ADR,"
                    + " B.NAME AS CUS_NAME,A.MOB_ID,A.BIL_ID,A.BIL_NO "
                    + " FROM MF_MA A "
                    + " LEFT JOIN CUST B ON B.CUS_NO = A.CUS_NO"
                    + " WHERE A.MA_ID=@MA_ID AND A.MA_NO = @MA_NO "
                    + " SELECT A.MA_ID ,A.MA_NO,A.ITM,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WC_NO,A.UNIT,A.SA_NO,A.SA_ITM,A.QTY,A.MTN_DD,"
                    + " A.EST_DD,A.RTN_DD,A.REM,A.KEY_ITM,A.QTY_MTN,A.MTN_TYPE,A.MTN_ALL_ID,P.SPC,W.SA_NO AS TMPSA_NO "
                    + " FROM TF_MA A "
                    + " LEFT JOIN PRDT P ON A.PRD_NO = P.PRD_NO"
                    + " LEFT JOIN MF_WC W ON A.WC_NO=W.WC_NO "
                    + " WHERE A.MA_ID=@MA_ID AND A.MA_NO = @MA_NO "
                    + " ORDER BY A.ITM";
            if (isSchema)
            {
                this.FillDatasetSchema(_sqlStr, _ds, new string[] { "MF_MA", "TF_MA" },_sqlPara);
            }
            else
            {
                this.FillDataset(_sqlStr, _ds, new string[] { "MF_MA", "TF_MA" }, _sqlPara);
            }
            DataColumn[] _dcHead = null;
            DataColumn[] _dcBody = null;
            if (_ds.Tables["MF_MA"].Columns.Contains("CUS_NAME"))
                _ds.Tables["MF_MA"].Columns["CUS_NAME"].ReadOnly = false;
            _dcHead = new DataColumn[2];
            _dcHead[0] = _ds.Tables["MF_MA"].Columns["MA_ID"];
            _dcHead[1] = _ds.Tables["MF_MA"].Columns["MA_NO"];
            _dcBody = new DataColumn[2];
            _dcBody[0] = _ds.Tables["TF_MA"].Columns["MA_ID"];
            _dcBody[1] = _ds.Tables["TF_MA"].Columns["MA_NO"];
            _ds.Relations.Add("MF_MATF_MA", _dcHead, _dcBody);            
            return _ds;
        }

        /// <summary>
        /// 取维修申请单数据
        /// </summary>
        /// <param name="maId">申请单据别</param>
        /// <param name="maNo">申请单号</param>
        /// <param name="keyIem">表身项次</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string maId, string maNo, int keyIem)
        {
            string _sqlStr = "";
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@MA_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = maId;
            _sqlPara[1] = new SqlParameter("@MA_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = maNo;
            _sqlPara[2] = new SqlParameter("@KEY_ITM", SqlDbType.Int);
            _sqlPara[2].Value = keyIem;
            _sqlStr = " SELECT A.MA_ID,A.MA_NO,A.MA_DD,A.CUS_NO,A.SAL_NO,A.DEP,A.CNT_NO,A.CNT_REM,A.BIL_TYPE,A.REM,"
                    + " A.SYS_DATE,A.CLS_DATE,A.PRT_SW,A.USR,A.CHK_MAN,A.CLS_ID,A.CLS_AUTO,A.MTN_FLOW,A.EST_DD,A.LOCK_MAN,"
                    + " A.QA_NO,A.BIL_ID,A.BIL_NO,"
                    + " A.CNT_NAME,A.OTH_NAME,A.TEL_NO,A.CELL_NO,A.CNT_ADR,"
                    + " B.NAME AS CUS_NAME "
                    + " FROM MF_MA A "
                    + " LEFT JOIN CUST B ON B.CUS_NO = A.CUS_NO"
                    + " WHERE A.MA_ID=@MA_ID AND A.MA_NO = @MA_NO "
                    + " SELECT A.MA_ID ,A.MA_NO,A.ITM,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WC_NO,A.UNIT,A.SA_NO,A.SA_ITM,A.QTY,A.MTN_DD,"
                    + " A.EST_DD,A.RTN_DD,A.REM,A.KEY_ITM,A.QTY_MTN,A.MTN_TYPE,A.MTN_ALL_ID,P.SPC "
                    + " FROM TF_MA A "
                    + " LEFT JOIN PRDT P ON A.PRD_NO = P.PRD_NO"
                    + " WHERE A.MA_ID=@MA_ID AND A.MA_NO = @MA_NO AND A.KEY_ITM =@KEY_ITM";

            this.FillDataset(_sqlStr, _ds, new string[] { "MF_MA", "TF_MA" }, _sqlPara);

            DataColumn[] _dcHead = null;
            DataColumn[] _dcBody = null;
            if (_ds.Tables["MF_MA"].Columns.Contains("CUS_NAME"))
                _ds.Tables["MF_MA"].Columns["CUS_NAME"].ReadOnly = false;
            _dcHead = new DataColumn[2];
            _dcHead[0] = _ds.Tables["MF_MA"].Columns["MA_ID"];
            _dcHead[1] = _ds.Tables["MF_MA"].Columns["MA_NO"];
            _dcBody = new DataColumn[2];
            _dcBody[0] = _ds.Tables["TF_MA"].Columns["MA_ID"];
            _dcBody[1] = _ds.Tables["TF_MA"].Columns["MA_NO"];
            _ds.Relations.Add("MF_MATF_MA", _dcHead, _dcBody);
            return _ds;
        }
        /// <summary>
        /// 取表头信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            string _sqlStr = "";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            SunlikeDataSet _ds = new SunlikeDataSet();
            _sqlStr = " SELECT A.MA_ID,A.MA_NO,A.MA_DD,A.CUS_NO,A.SAL_NO,A.DEP,A.CNT_NO,A.CNT_REM,A.BIL_TYPE,A.REM,"
                    + " A.SYS_DATE,A.CLS_DATE,A.PRT_SW,A.USR,A.CHK_MAN,A.CLS_ID,A.CLS_AUTO,A.MTN_FLOW,A.EST_DD,A.LOCK_MAN,"
                    + " A.QA_NO,A.BIL_ID,A.BIL_NO,"
                    + " A.CNT_NAME,A.OTH_NAME,A.TEL_NO,A.CELL_NO,A.CNT_ADR,"
                    + " B.NAME AS CUS_NAME "
                    + " FROM MF_MA A "
                    + " LEFT JOIN CUST B ON B.CUS_NO = A.CUS_NO"
                    + " WHERE 1=1 " + sqlWhere;

            this.FillDataset(_sqlStr, _ds, new string[] { "MF_MA" }, _sqlPara);
            return _ds;
        }
        #endregion

        #region 结案
        /// <summary>
        /// 自动结案维修申请单
        /// </summary>
        /// <param name="maId">申请单据别</param>
        /// <param name="maNo">申请单号</param>
        public void UpdateClsId(string maId,string maNo)
        {
            string _sqlStr = "";
            _sqlStr = " IF EXISTS(SELECT MA_NO FROM MF_MA WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO)"
                    + "	BEGIN "
                    + " IF EXISTS(SELECT MA_ID FROM TF_MA WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO AND (ISNULL(QTY,0)-ISNULL(QTY_MTN,0) > 0))\n"
                    + "			BEGIN"
                    + "				UPDATE MF_MA SET CLS_ID='F',CLS_AUTO='F' WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO"
                    + "			END"
                    + "		ELSE"
                    + "			BEGIN"
                    + "				UPDATE MF_MA SET CLS_ID='T',CLS_AUTO='T' WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO"
                    + "			END"
                    + "	END ";

            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MA_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = maId;
            _sqlPara[1] = new SqlParameter("@MA_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = maNo;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        /// <summary>
        /// 自动结案维修申请单
        /// </summary>
        /// <param name="maId">申请单据别</param>
        /// <param name="maNo">申请单号</param>
        /// <param name="clsFieldName">结案字段</param>
        /// <param name="close">是否结案</param>
        public void UpdateClsId(string maId, string maNo, string clsFieldName, bool close)
        {
            string _sqlStr = "";
            string _clsAuto = close ? "T" : "F";
            _sqlStr = " IF EXISTS(SELECT MA_NO FROM MF_MA WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO)"
                    + "	BEGIN "
                    + " IF EXISTS(SELECT MA_ID FROM TF_MA WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO AND (ISNULL(QTY,0)-ISNULL(QTY_MTN,0) > 0))\n"
                    + "			BEGIN"
                    + "				UPDATE MF_MA SET " + clsFieldName + "=@CLS_ID,CLS_AUTO='F' WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO"
                    + "			END"
                    + "		ELSE"
                    + "			BEGIN"
                    + "				UPDATE MF_MA SET " + clsFieldName + "=@CLS_ID,CLS_AUTO='" + _clsAuto + "' WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO"
                    + "			END"
                    + "	END ";

            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@MA_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = maId;
            _sqlPara[1] = new SqlParameter("@MA_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = maNo;
            _sqlPara[2] = new SqlParameter("@CLS_ID", SqlDbType.VarChar, 1);
            _sqlPara[2].Value = close.ToString().ToUpper().Substring(0, 1);
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        #endregion

        #region 审核/反审核单据标记
        /// <summary>
        /// 审核单据标记
        /// </summary>
        /// <param name="maId">申请单据别</param>
        /// <param name="maNo">申请单号</param>
        /// <param name="chkMan">审核人</param>
        /// <param name="clsDate">审核日</param>
        public void UpdateMtnMa(string maId,string maNo,string chkMan,DateTime clsDate)
        {
            string _sqlStr = "";
            _sqlStr = "UPDATE MF_MA SET CHK_MAN= @CHK_MAN,CLS_DATE =@CLS_DATE WHERE MA_ID=@MA_ID AND MA_NO = @MA_NO";
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@MA_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = maId;
            _sqlPara[1] = new SqlParameter("@MA_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = maNo;
            _sqlPara[2] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _sqlPara[2].Value = chkMan;
            _sqlPara[3] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _sqlPara[3].Value = clsDate;
            this.ExecuteDataset(_sqlStr, _sqlPara);
        }
        /// <summary>
        /// 反审核单据标记
        /// </summary>
        /// <param name="maId">申请单据别</param>
        /// <param name="maNo">申请单号</param>
        public void RollbackMtnMa(string maId,string maNo)
        {
            string _sqlStr = "";
            _sqlStr = "UPDATE MF_MA SET CHK_MAN= NULL,CLS_DATE = NULL WHERE MA_ID=@MA_ID AND MA_NO = @MA_NO";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MA_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = maId;
            _sqlPara[1] = new SqlParameter("@MA_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = maNo;
            this.ExecuteDataset(_sqlStr, _sqlPara);
        }
        #endregion

        #region 回写维修申请单数量

        /// <summary>
        /// 回写维修申请单数量
        /// </summary>
        /// <param name="maId">申请单据别</param>
        /// <param name="maNo">申请单号</param>
        /// <param name="keyItm">申请项次</param>
        /// <param name="qty">回写的数量</param>
        public void UpdateQtyMtn(string maId, string maNo, int keyItm, decimal qty)
        {
            string sql = " UPDATE TF_MA SET QTY_MTN=ISNULL(QTY_MTN,0)+@QTY WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO AND KEY_ITM=@KEY_ITM ;"
            + " IF (@@ROWCOUNT > 0) "
            + " BEGIN \n"
            + " UPDATE MF_MA SET CLS_ID='T',CLS_AUTO='T' WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO AND (SELECT COUNT(MA_ID) FROM TF_MA WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO AND ISNULL(QTY,0)-ISNULL(QTY_MTN,0)>0)=0;"
            + " UPDATE MF_MA SET CLS_ID='F',CLS_AUTO='F' WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO AND (SELECT COUNT(MA_ID) FROM TF_MA WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO AND ISNULL(QTY,0)-ISNULL(QTY_MTN,0)>0)>0;"
            + " END ";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@MA_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@MA_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@KEY_ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[3].Precision = 28;
            _spc[3].Scale = 8;
            _spc[0].Value = maId;
            _spc[1].Value = maNo;
            _spc[2].Value = keyItm;
            _spc[3].Value = qty;
            this.ExecuteNonQuery(sql, _spc);
        }
        #endregion


      
    }
}
