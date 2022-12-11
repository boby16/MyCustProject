using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// 缴库单
    /// </summary>
    public class DbMRPMM: DbObject
    {
        /// <summary>
        /// 缴库单
        /// </summary>
        /// <param name="connStr"></param>
        public DbMRPMM(string connStr)
            : base(connStr)
        {
        }
        #region 取数据
        /// <summary>
        /// 取缴库单资料
        /// </summary>
        /// <param name="mmNo">缴库单号</param>
        /// <param name="onlyFillSchema">是否取schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string mmId, string mmNo, bool onlyFillSchema)
        {
            string _sqlStr = " SELECT A.MM_ID,A.MM_NO,A.MM_DD,A.BIL_TYPE,A.DEP,A.BIL_ID,A.BIL_ID,A.BIL_NO,A.MO_NO,A.VOH_ID,A.VOH_NO,A.FJ_NUM,"
                                   + " A.USR,A.USR_NO,A.CHK_MAN,A.LOCK_MAN,A.PRT_USR,A.SYS_DATE,A.CLS_DATE,A.MOB_ID,A.PRT_SW,A.CPY_SW,A.FIN_ID,A.REM"
                                   + " FROM MF_MM0 A "
                                   + " LEFT JOIN MF_MO B ON B.MO_NO = A.MO_NO"
                                   + " WHERE A.MM_ID=@MM_ID AND A.MM_NO = @MM_NO;"
                                   + " SELECT A.MM_ID,A.MM_NO,A.ITM,A.MM_DD,A.MO_NO,A.TW_NO,A.SO_NO,A.CUS_OS_NO,A.DEP,A.BAT_NO,A.PRD_NO,A.ID_NO,A.PRD_MARK,"
                                   + " A.PRD_NAME,A.UNIT,A.WH,A.VALID_DD,A.FREE_ID,A.PRE_ITM,A.EST_ITM,A.QTY,A.QTY1,A.QTY_RTN,A.QTY_SA,A.AMTN_VAL,A.CST_MAKE,"
                                   + " A.CST_PRD,A.CST_OUT,A.CST_MAN,A.CST,A.USED_TIME,A.TIME_CNT,A.CST_SMAKE,A.CST_SPRD,A.CST_SOUT,A.CST_SMAN,A.CST_STD,"
                                   + " ISNULL(A.CST_MAKE,0)+ISNULL(A.CST_PRD,0)+ISNULL(A.CST_MAN,0)+ISNULL(A.CST_OUT,0)+ISNULL(A.CST,0) AS CST_TOTAL," //
                                   + " A.USED_STIME,A.TIME_SCNT,A.OLD_MM_ID,A.OLD_MM_NO,A.MM_ITM,A.CALC_ID,A.ZC_FLAG,A.CNTT_NO,A.CAS_NO,A.TASK_ID,A.RK_DD,"
                                   + " A.DEP_RK,A.REM,A.ML_NO,A.BIL_ID,A.BIL_NO,A.BIL_ITM,A.QC_FLAG,A.UP_MAIN,"
                                   + " B.CUS_NO,C.WC_NO,P.SPC "
                                   + " FROM TF_MM0 A "
                                   + " LEFT JOIN MF_MO B ON B.MO_NO = A.MO_NO "
                                   + " LEFT JOIN PRDT P ON P.PRD_NO = A.PRD_NO "
                                   + " LEFT JOIN TF_POS C ON C.OS_ID='SO' AND C.OS_NO = B.SO_NO AND C.EST_ITM = B.EST_ITM"
                                   + " WHERE A.MM_ID=@MM_ID AND A.MM_NO = @MM_NO;"
                                   + " SELECT MM_ID,MM_NO,MM_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO FROM TF_MM0_B WHERE MM_ID=@MM_ID AND MM_NO=@MM_NO";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MM_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = mmId;
            _sqlPara[1] = new SqlParameter("@MM_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = mmNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[3] { "MF_MM0", "TF_MM0", "TF_MM0_B" }, _sqlPara);
            if (_ds.Tables["TF_MM0"].Columns.Contains("WC_NO"))
                _ds.Tables["TF_MM0"].Columns["WC_NO"].ReadOnly = false;
            if (_ds.Tables["TF_MM0"].Columns.Contains("SPC"))
                _ds.Tables["TF_MM0"].Columns["SPC"].ReadOnly = false;
            if (_ds.Tables["TF_MM0"].Columns.Contains("CST_TOTAL"))
                _ds.Tables["TF_MM0"].Columns["CST_TOTAL"].ReadOnly = false;
            //设定PK，因为用了left join以后，PK会取不到
            DataColumn[] _dca = new DataColumn[2];
            _dca[0] = _ds.Tables["MF_MM0"].Columns["MM_ID"];
            _dca[1] = _ds.Tables["MF_MM0"].Columns["MM_NO"];
            _ds.Tables["MF_MM0"].PrimaryKey = _dca;
            _dca = new DataColumn[3];
            _dca[0] = _ds.Tables["TF_MM0"].Columns["MM_ID"];
            _dca[1] = _ds.Tables["TF_MM0"].Columns["MM_NO"];
            _dca[2] = _ds.Tables["TF_MM0"].Columns["ITM"];
            _ds.Tables["TF_MM0"].PrimaryKey = _dca;
            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_MM0"].Columns["MM_ID"];
            _dca1[1] = _ds.Tables["MF_MM0"].Columns["MM_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_MM0"].Columns["MM_ID"];
            _dca2[1] = _ds.Tables["TF_MM0"].Columns["MM_NO"];
            _ds.Relations.Add("MF_MM0TF_MM0", _dca1, _dca2);

            DataColumn[] _dca3 = new DataColumn[3];
            _dca3[0] = _ds.Tables["TF_MM0"].Columns["MM_ID"];
            _dca3[1] = _ds.Tables["TF_MM0"].Columns["MM_NO"];
            _dca3[2] = _ds.Tables["TF_MM0"].Columns["ITM"];
            DataColumn[] _dca4 = new DataColumn[3];
            _dca4[0] = _ds.Tables["TF_MM0_B"].Columns["MM_ID"];
            _dca4[1] = _ds.Tables["TF_MM0_B"].Columns["MM_NO"];
            _dca4[2] = _ds.Tables["TF_MM0_B"].Columns["MM_ITM"];
            _ds.Relations.Add("TF_MM0TF_MM0_B", _dca3, _dca4);

            return _ds;
        }
        /// <summary>
        /// 取缴库单资料
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            string _sqlStr = " SELECT A.MM_ID,A.MM_NO,A.MM_DD,A.BIL_TYPE,A.DEP,A.BIL_ID,A.BIL_ID,A.BIL_NO,A.MO_NO,A.VOH_ID,A.VOH_NO,A.FJ_NUM,"
                                   + " A.USR,A.USR_NO,A.CHK_MAN,A.LOCK_MAN,A.PRT_USR,A.SYS_DATE,A.CLS_DATE,A.MOB_ID,A.PRT_SW,A.CPY_SW,A.FIN_ID,A.REM"
                                   + " FROM MF_MM0 A "
                                   + " LEFT JOIN MF_MO B ON B.MO_NO = A.MO_NO"
                                   + " WHERE 1=1" + sqlWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[1] { "MF_MM0" });
            return _ds;
        }
        #endregion

        #region 修改审核人
        /// <summary>
        /// 修改审核人
        /// </summary>
        /// <param name="bilId">单据别</param>
        /// <param name="bilNo">单号</param>
        /// <param name="chkMan">审核人</param>
        /// <param name="clsDd">审核日</param>
        /// <returns></returns>
        public bool UpdateChkMan(string bilId, string bilNo, string chkMan, DateTime clsDd)
        {
            string _where = "";
            bool _result = false;
            _where = "  MM_ID=@MM_ID AND MM_NO=@MM_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_MM0 SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@MM_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = bilId;
            _sqlPara[1] = new SqlParameter("@MM_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = bilNo;
            _sqlPara[2] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _sqlPara[2].Value = chkMan;
            _sqlPara[3] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            if (string.IsNullOrEmpty(chkMan))
            {
                _sqlPara[3].Value = System.DBNull.Value;
            }
            else
            {
                _sqlPara[3].Value = clsDd;
            }
            try
            {
                this.ExecuteNonQuery(_sqlStr, _sqlPara);
                _result = false;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _result;
        }
        #endregion

        #region 更新缴库单凭证号码
        /// <summary>
        /// 更新缴库单凭证号码
        /// </summary>
        /// <param name="mmId"></param>
        /// <param name="mmNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string mmId, string mmNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_MM0 SET VOH_NO=@VOH_NO WHERE MM_ID=@MM_ID AND MM_NO=@MM_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@MM_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = mmId;

            _sqlPara[1] = new SqlParameter("@MM_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = mmNo;

            _sqlPara[2] = new SqlParameter("@VOH_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = vohNo;
            try
            {
                this.ExecuteNonQuery(_sqlStr, _sqlPara);
                _result = true;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _result;

        }
        #endregion

    }
}
