using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// 领、退、补料单
    /// </summary>
    public class DbMRPML : DbObject
    {
        public DbMRPML(string connStr)
            : base(connStr)
        {
        }
        #region 取数据
        /// <summary>
        ///  取领、退、补料单
        /// </summary>
        /// <param name="mlId"></param>
        /// <param name="mlNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string mlId, string mlNo, bool onlyFillSchema)
        {
            string _sqlStr = " SELECT A.MLID,A.ML_NO,A.ML_DD,A.BIL_ID,A.BIL_NO,A.ML_ID,A.FIX_CST,A.FIX_CST1,A.BAT_NO,A.MO_NO,A.MRP_NO,A.PRD_NAME,"
                        + " A.PRD_MARK,A.UNIT,A.WH_MTL,A.QTY,A.QTY1,A.VOH_ID,A.VOH_NO,A.USR_NO,A.DEP,A.REM,A.CUS_NO,A.TZ_NO,A.CPY_SW,"
                        + " A.FT_ID,A.USR,A.CHK_MAN,A.PRT_SW,A.CLS_DATE,A.ID_NO,A.LM_NO,A.BIL_TYPE,A.CNTT_NO,A.MOB_ID,A.LOCK_MAN,"
                        + " A.FJ_NUM,A.SYS_DATE,A.MM_NO,A.CAS_NO,A.TASK_ID,A.CUS_OS_NO,A.PRT_USR,A.QL_ID,A.QL_NO,A.MC_NO,A.VOH_ID_MC,"
                        + " A.QL_TYPE,A.IDX_NO,A.TAX_ID,A.CUR_ID,A.EXC_RTO,A.AMT,A.AMTN_NET,A.TAX,A.EP_ID,A.EP_NO,A.VOH_ID_EP,A.YL_ID,A.TAX_RTO, "
                        + " B.SO_NO,B.ID_NO, "
                        + " C.WC_NO,"
                        + " P.SPC "
                        + " FROM MF_ML A "
                        + " LEFT JOIN MF_MO B ON B.MO_NO = A.MO_NO "
                        + " LEFT JOIN TF_POS C ON  C.OS_ID ='SO' AND C.OS_NO = B.SO_NO AND C.EST_ITM = B.EST_ITM "
                        + " LEFT JOIN PRDT P ON P.PRD_NO = A.MRP_NO "
                        + " WHERE A.MLID = @MLID AND A.ML_NO = @ML_NO;"
                        + " SELECT A.MLID,A.ML_NO,A.ITM,A.ML_DD,A.ML_ID,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.UNIT,A.QTY,A.QTY1,A.WH,A.CST,A.REM,"
                        + " (CASE WHEN ISNULL(A.QTY,0)<>0 THEN ISNULL(A.CST,0)/ISNULL(A.QTY,0) ELSE 0 END) AS CST_UNIT,"
                        + " A.BAT_NO,A.CPY_SW,A.CST_STD,A.PRD_NO_CHG,A.ID_NO,A.PRD_NO_MO,A.QTY_LM,A.QTY_DIFF,A.CNTT_NO,A.COMPOSE_IDNO,"
                        + " A.PRE_ITM,A.EST_ITM,A.USEIN_NO,A.LOS_RTO,A.QTY_STD,A.ZC_PRD,A.CHG_RTO,A.CHG_ITM,A.QTY_CHG_RTO,"
                        + " A.WH_LC,A.QTY_OVER,A.PK_ID,A.QTY_LC,A.RK_DD,A.QL_TYPE,A.QTY_ST,A.VALID_DD,A.DEP_RK,A.PW_ITM,A.QTY_QL_GG, "
                        + " A.MO_NO,A.FIX_CST1,A.QTY_ML,A.TZ_NO,A.QTY_WH,"
                //+ "  NULL AS QTY_LEFT_V, "//可领料量=应发量+损耗量-已领料量ISNULL(A.QTY_RSV,0)+ISNULL(A.QTY_LOST,0)-ISNULL(A.QTY_RTN,0)
                //+ " NULL AS QTY_LEFT_V1, "//未领量=应发量+损耗量-已领料量-本单领料量ISNULL(A.QTY_RSV,0)+ISNULL(A.QTY_LOST,0)-ISNULL(A.QTY_RTN,0)-ISNULL(A.QTY,0)
                        + " A.QTY_LEFT, "//本单原未领量
                        + " A.QTY_RSV,"//应发量
                        + " A.QTY_LOST,"//应损耗量
                        + " A.QTY_RTN,"//已领料量
                        + " A.UNIT_H,"//套数单位
                        + " D.QTY AS QTY_ML_H,D.MRP_NO,P1.NAME AS MRP_NAME,D.PRD_MARK AS PRD_MARK_H,D.BAT_NO AS BAT_NO_H,P1.SPC AS SPC_H,"
                        + " P.SPC "
                        + " FROM TF_ML A"
                        + " LEFT JOIN MF_ML B ON B.MLID = A.MLID AND B.ML_NO =A.ML_NO"
                        + " LEFT JOIN TF_MO C ON C.MO_NO = A.MO_NO AND C.EST_ITM = A.EST_ITM "
                        + " LEFT JOIN MF_MO D ON D.MO_NO = C.MO_NO "
                        + " LEFT JOIN PRDT P ON P.PRD_NO = A.PRD_NO "
                        + " LEFT JOIN PRDT P1 ON P1.PRD_NO = D.MRP_NO "
                        + " WHERE A.MLID = @MLID AND A.ML_NO = @ML_NO "
                        + " ORDER BY A.ITM ";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MLID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = mlId;
            _sqlPara[1] = new SqlParameter("@ML_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = mlNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sqlStr, _ds, new string[2] { "MF_ML", "TF_ML" }, _sqlPara);

            }
            else
            {
                this.FillDataset(_sqlStr, _ds, new string[2] { "MF_ML", "TF_ML" }, _sqlPara);
            }
            if (_ds.Tables["MF_ML"].Columns.Contains("WC_NO"))
                _ds.Tables["MF_ML"].Columns["WC_NO"].ReadOnly = false;
            if (_ds.Tables["MF_ML"].Columns.Contains("SPC"))
                _ds.Tables["MF_ML"].Columns["SPC"].ReadOnly = false;
            if (_ds.Tables["TF_ML"].Columns.Contains("QTY_RSV"))
                _ds.Tables["TF_ML"].Columns["QTY_RSV"].ReadOnly = false;
            if (_ds.Tables["TF_ML"].Columns.Contains("QTY_LEFT_V"))
                _ds.Tables["TF_ML"].Columns["QTY_LEFT_V"].ReadOnly = false;
            if (_ds.Tables["TF_ML"].Columns.Contains("SPC"))
                _ds.Tables["TF_ML"].Columns["SPC"].ReadOnly = false;
            if (_ds.Tables["TF_ML"].Columns.Contains("CST_UNIT"))
                _ds.Tables["TF_ML"].Columns["CST_UNIT"].ReadOnly = false;
            DataColumn[] _pkDc1 = new DataColumn[2];
            _pkDc1[0] = _ds.Tables["MF_ML"].Columns["MLID"];
            _pkDc1[1] = _ds.Tables["MF_ML"].Columns["ML_NO"];

            DataColumn[] _pkDc2 = new DataColumn[3];
            _pkDc2[0] = _ds.Tables["TF_ML"].Columns["MLID"];
            _pkDc2[1] = _ds.Tables["TF_ML"].Columns["ML_NO"];
            _pkDc2[2] = _ds.Tables["TF_ML"].Columns["ITM"];
            _ds.Tables["MF_ML"].PrimaryKey = _pkDc1;
            _ds.Tables["TF_ML"].PrimaryKey = _pkDc2;


            DataColumn[] _dcHead = null;
            DataColumn[] _dcBody = null;
            _dcHead = new DataColumn[2];
            _dcHead[0] = _ds.Tables["MF_ML"].Columns["MLID"];
            _dcHead[1] = _ds.Tables["MF_ML"].Columns["ML_NO"];
            _dcBody = new DataColumn[2];
            _dcBody[0] = _ds.Tables["TF_ML"].Columns["MLID"];
            _dcBody[1] = _ds.Tables["TF_ML"].Columns["ML_NO"];
            _ds.Relations.Add("MF_MLTF_ML", _dcHead, _dcBody);
            return _ds;
        }
        /// <summary>
        ///  取领、退、补料单
        /// </summary>
        /// <param name="mlId"></param>
        /// <param name="mlNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string mlId, string mlNo, int preItm)
        {
            string _sqlStr = "SELECT A.MLID,A.ML_NO,A.ITM,A.ML_DD,A.ML_ID,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.UNIT,A.QTY,A.QTY1,A.WH,A.CST,A.REM,"
                        + " A.BAT_NO,A.CPY_SW,A.CST_STD,A.PRD_NO_CHG,A.ID_NO,A.PRD_NO_MO,A.QTY_LM,A.QTY_DIFF,A.CNTT_NO,A.COMPOSE_IDNO,"
                        + " A.PRE_ITM,A.EST_ITM,A.USEIN_NO,A.LOS_RTO,A.QTY_STD,A.ZC_PRD,A.CHG_RTO,A.CHG_ITM,A.QTY_CHG_RTO,"
                        + " A.WH_LC,A.QTY_OVER,A.PK_ID,A.QTY_LC,A.RK_DD,A.QL_TYPE,A.QTY_ST,A.VALID_DD,A.DEP_RK,A.PW_ITM,A.QTY_QL_GG, "
                        + " B.MO_NO,"
                        + " ISNULL(C.QTY_RSV,0)+ISNULL(C.QTY_LOST,0)  AS QTY_RSV,"//应领量=应发量+损耗量
                        + " ISNULL(C.QTY_RSV,0)+ISNULL(C.QTY_LOST,0)+ISNULL(A.QTY,0)-ISNULL(C.QTY,0)  AS QTY_LEFT_V, "//未领量=应发量+损耗量+本单领料量-实发量
                        + " A.QTY_LEFT, "//本单原未领量
                        + " P.SPC "
                        + " FROM TF_ML A"
                        + " LEFT JOIN MF_ML B ON B.MLID = A.MLID AND B.ML_NO =A.ML_NO"
                        + " LEFT JOIN TF_MO C ON C.MO_NO = A.MO_NO AND C.EST_ITM = A.EST_ITM "
                        + " LEFT JOIN PRDT P ON P.PRD_NO = A.PRD_NO "
                        + " WHERE A.MLID = @MLID AND A.ML_NO = @ML_NO AND A.PRE_ITM=@PRE_ITM"
                        + " ORDER BY A.ITM ";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@MLID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = mlId;
            _sqlPara[1] = new SqlParameter("@ML_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = mlNo;
            _sqlPara[2] = new SqlParameter("@PRE_ITM", SqlDbType.Int);
            _sqlPara[2].Value = preItm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[1] { "TF_ML" }, _sqlPara);

            if (_ds.Tables["TF_ML"].Columns.Contains("QTY_RSV"))
                _ds.Tables["TF_ML"].Columns["QTY_RSV"].ReadOnly = false;
            if (_ds.Tables["TF_ML"].Columns.Contains("QTY_LEFT_V"))
                _ds.Tables["TF_ML"].Columns["QTY_LEFT_V"].ReadOnly = false;
            if (_ds.Tables["TF_ML"].Columns.Contains("SPC"))
                _ds.Tables["TF_ML"].Columns["SPC"].ReadOnly = false;


            DataColumn[] _pkDc2 = new DataColumn[3];
            _pkDc2[0] = _ds.Tables["TF_ML"].Columns["MLID"];
            _pkDc2[1] = _ds.Tables["TF_ML"].Columns["ML_NO"];
            _pkDc2[2] = _ds.Tables["TF_ML"].Columns["ITM"];

            _ds.Tables["TF_ML"].PrimaryKey = _pkDc2;
            return _ds;
        }

        /// <summary>
        /// 获取指定托工单的最小领料日
        /// </summary>
        /// <param name="twNo">托工单号</param>
        /// <returns>领料日期</returns>
        public object GetMinMlDd(string twNo)
        {
            string sql = "SELECT MIN(ML_DD) FROM MF_ML WHERE BIL_NO=@TW_NO";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@TW_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = twNo;
            return this.ExecuteScalar(sql, _spc);
        }
        #endregion

        #region 是否存在领料的制令单
        /// <summary>
        /// 是否存在领料的制令单
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <returns></returns>
        public bool IsExistsForMo(string moNo)
        {
            bool _result = false;
            string _sqlStr = " SELECT TOP 1 ML_NO FROM MF_ML WHERE MO_NO = @MO_NO";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[1] { "MF_ML" }, _sqlPara);
            if (_ds.Tables["MF_ML"].Rows.Count > 0)
                _result = true;
            return _result;
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
            _where = " MLID=@MLID AND ML_NO=@ML_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_ML SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@MLID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = bilId;
            _sqlPara[1] = new SqlParameter("@ML_NO", SqlDbType.VarChar, 20);
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

        #region 更新领料单凭证号码
        /// <summary>
        /// 更新销货单凭证号码
        /// </summary>
        /// <param name="mlId"></param>
        /// <param name="mlNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string mlId, string mlNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_ML SET VOH_NO=@VOH_NO WHERE MLID=@MLID AND ML_NO=@ML_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@MLID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = mlId;

            _sqlPara[1] = new SqlParameter("@ML_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = mlNo;

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

        #region 更改回写原单

        public void UpdateOS(string osId, string osNo, string osItm, decimal _qty, string qtyField)
        {
            string _sql = "";
            switch (osId)//预留扩展接口
            {
                case "TW":
                    _sql = string.Format(@" UPDATE TF_TW SET {0}=ISNULL({0},0)+@QTY WHERE TW_NO=@BIL_NO
                                            AND EST_ITM=@ITM", qtyField);
                    break;
            }
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[0].Value = osId;
            _spc[1].Value = osNo;
            _spc[2].Value = osItm;
            _spc[3].Value = _qty;
            this.ExecuteNonQuery(_sql, _spc);
        }

        public void UpdateTWQtyML(string bilNo)
        {
            string _sql = @"UPDATE    MF_TW
                            SET QTY_ML = C.QTY_ML
                            FROM         MF_TW INNER JOIN
	                            (SELECT MIN( (ABS(ISNULL(B.QTY_RTN, 0)) * H.QTY) / B.QTY) AS QTY_ML, B.TW_NO
	                            FROM  TF_TW AS B INNER JOIN
	                            MF_TW AS H ON H.TW_NO = B.TW_NO
	                            WHERE  (ISNULL(B.CHG_ITM, 0) = 0) AND (ISNULL(B.QTY_RSV, 0) + ISNULL(B.QTY_LOST, 0) > 0) AND (ISNULL(B.QTY, 0) >= 0) AND 
	                            (H.TW_NO = @TW_NO)
                                 GROUP BY B.TW_NO) AS C ON MF_TW.TW_NO = C.TW_NO
                            WHERE     (MF_TW.TW_NO = @TW_NO)";

            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@TW_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = bilNo;
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion




    }
}
