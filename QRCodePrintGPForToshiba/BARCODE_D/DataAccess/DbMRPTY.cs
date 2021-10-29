using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// 验收单
    /// </summary>
    public class DbMRPTY : DbObject
    {
        /// <summary>
        /// 验收单
        /// </summary>
        /// <param name="connStr"></param>
        public DbMRPTY(string connStr)
            : base(connStr)
        {
        }
        #region 取数据
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="tyId"></param>
        /// <param name="tyNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string tyId, string tyNo, bool onlyFillSchema)
        {
            string _sql = "SELECT * FROM MF_TY WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO"
                        + " ; "
                        + " SELECT TF_TY.*,PRDT.SPC FROM TF_TY "
                        + " LEFT JOIN PRDT ON PRDT.PRD_NO = TF_TY.PRD_NO "
                        + " WHERE TF_TY.TY_ID = @TY_ID AND TF_TY.TY_NO = @TY_NO;";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@TY_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = tyId;
            _aryPt[1] = new SqlParameter("@TY_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = tyNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[2] { "MF_TY", "TF_TY" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[2] { "MF_TY", "TF_TY" }, _aryPt);
            }

            DataColumn[] _dcPk = new DataColumn[2];
            _dcPk[0] = _ds.Tables["MF_TY"].Columns["TY_ID"];
            _dcPk[1] = _ds.Tables["MF_TY"].Columns["TY_NO"];
            DataColumn[] _dcFk = new DataColumn[2];
            _dcFk[0] = _ds.Tables["TF_TY"].Columns["TY_ID"];
            _dcFk[1] = _ds.Tables["TF_TY"].Columns["TY_NO"];
            _ds.Relations.Add("MF_TYTF_TY", _dcPk, _dcFk);

            return _ds;
        }
        /// <summary>
        /// 取得转单记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        public SunlikeDataSet GetExportData(string sqlWhere)
        {
            string _sql = "SELECT TF_TY.PRD_NO, TF_TY.PRD_NAME, TF_TY.PRD_MARK, TF_TY.WH, TF_TY.TY_NO, TF_TY.QTY_LOST, TF_TY.QTY_OK, TF_TY.SPC_NO, TZERR.PRC_ID, \n"
                + "TZERR.PRC_ID2, TZERR.TR_NO, TF_TY.BAT_NO, MF_TY.BIL_TYPE, MF_MO.DEP, MF_MO.MO_NO, MF_MO.CUS_NO,CUST.NAME AS CUS_NAME,CONTACT.CNT_NO,CONTACT.NAME AS CNT_NAME, TF_TY.REM, \n"
                + "TF_TY.TI_NO, TF_TY.UNIT, TF_TY.QTY1_LOST, TF_TY.QTY1_OK, TF_TY.ID_NO, TF_TY.TY_ID, TF_TY.PRC_ID AS PRC_ID_TY, \n"
                + "MF_MO.STA_DD,MF_MO.END_DD, \n"
                + "TF_TY.MM_NO, TF_TY.PRE_ITM, \n"
                + "PRDT.SPC \n"
                + "FROM MF_TY \n"
                + "JOIN \n"
                + "TF_TY ON MF_TY.TY_ID = TF_TY.TY_ID AND MF_TY.TY_NO = TF_TY.TY_NO \n"
                + "LEFT JOIN \n"
                + "TZERR ON TF_TY.BUILD_BIL = TZERR.TR_NO \n"
                + "LEFT JOIN \n"
                + "MF_MO ON TF_TY.BIL_NO = MF_MO.MO_NO \n"
                + "LEFT JOIN \n"
                + "TF_POS ON TF_POS.OS_ID = 'SO' AND TF_POS.OS_NO = MF_MO.SO_NO AND TF_POS.EST_ITM=MF_MO.EST_ITM \n"
                + "LEFT JOIN \n"
                + "TF_RCV ON TF_RCV.RV_ID = TF_POS.BIL_ID AND TF_RCV.RV_NO = TF_POS.QT_NO AND TF_RCV.KEY_ITM=TF_POS.OTH_ITM \n"
                + "LEFT JOIN \n"
                + "MF_MA ON MF_MA.MA_ID = TF_RCV.MA_ID AND MF_MA.MA_NO = TF_RCV.MA_NO \n"
                + "LEFT JOIN \n"
                + "CONTACT ON CONTACT.CNT_NO = MF_MA.CNT_NO AND CONTACT.CUS_NO = MF_MA.CUS_NO \n"
                + "LEFT JOIN \n"
                + "PRDT ON PRDT.PRD_NO = TF_TY.PRD_NO \n"
                + "LEFT JOIN \n"
                + "CUST ON CUST.CUS_NO = MF_MO.CUS_NO \n"
                + "WHERE ISNULL(MF_TY.CHK_MAN, '') <> '' AND TF_TY.TY_ID = 'TP' AND MF_MO.ISSVS = 'T' " + sqlWhere;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "TF_TY_EXPORT" });

            return _ds;
        }
        /// <summary>
        /// 取得检验单信息
        /// </summary>
        /// <param name="tyId"></param>
        /// <param name="tyNo"></param>
        /// <param name="itmField"></param>
        /// <param name="bilItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string tyId, string tyNo, string itmField, int bilItm)
        {
            string _sql = " SELECT * FROM MF_TY WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO"
                        + " ;"
                        + " SELECT * FROM TF_TY WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO AND " + itmField + "=@PRE_ITM;";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@TY_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = tyId;
            _aryPt[1] = new SqlParameter("@TY_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = tyNo;
            _aryPt[2] = new SqlParameter("@PRE_ITM", SqlDbType.Int);
            _aryPt[2].Value = bilItm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[2] { "MF_TY", "TF_TY" });
            return _ds;
        }
        #endregion

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="tyId">单据类别</param>
        /// <param name="tyNo">单号</param>
        /// <param name="chkMan">审核人</param>
        /// <param name="clsDd">审核日期</param>
        public void Approve(string tyId, string tyNo, string chkMan, DateTime clsDd)
        {
            string _sql = "UPDATE MF_TY SET CHK_MAN = @CHK_MAN, CLS_DATE = @CLS_DATE WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO";
            SqlParameter[] _aryPt = new SqlParameter[4];
            _aryPt[0] = new SqlParameter("@TY_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = tyId;
            _aryPt[1] = new SqlParameter("@TY_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = tyNo;
            _aryPt[2] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _aryPt[2].Value = chkMan;
            _aryPt[3] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _aryPt[3].Value = clsDd.ToString("yyyy-MM-dd HH:mm:ss");
            this.ExecuteNonQuery(_sql, _aryPt);
        }
        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="tyId">单据类别</param>
        /// <param name="tyNo">单号</param>
        public void Rollback(string tyId, string tyNo)
        {
            string _sql = "UPDATE MF_TY SET CHK_MAN = '', CLS_DATE = NULL WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@TY_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = tyId;
            _aryPt[1] = new SqlParameter("@TY_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = tyNo;
            this.ExecuteNonQuery(_sql, _aryPt);
        }
        /// <summary>
        /// 产生不合格数量的单据
        /// </summary>
        /// <param name="tyId">检验单据别</param>
        /// <param name="tyNo">检验单号</param>
        /// <param name="bilItm">追踪项次</param>
        /// <param name="trNo"></param>
        /// <param name="qtyLostRtn"></param>
        public void UpdateBuildBil(string tyId, string tyNo, string bilItm, string trNo, decimal qtyLostRtn)
        {
            string _trNoCurrent = trNo;
            string _sqlStr = "";
            _sqlStr = "UPDATE TF_TY SET BUILD_BIL = @TR_NO_C ,QTY_LOST_RTN = ISNULL(QTY_LOST_RTN,0) + @QTY_LOST_RTN WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO";
            if (!String.IsNullOrEmpty(bilItm))
            {
                _sqlStr += " AND PRE_ITM = @PRE_ITM";
            }
            else if (!String.IsNullOrEmpty(trNo))
            {
                _sqlStr += " AND BUILD_BIL = @TR_NO";
                _trNoCurrent = "";
            }
            else
            {
                return;
            }
            _sqlStr += ";IF NOT EXISTS(SELECT 1 FROM TF_TY WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO AND (ISNULL(BUILD_BIL, '') = '')) \n"
                + "  UPDATE MF_TY SET CLS_ID_LOST = 'T' WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO \n"
                + "ELSE \n"
                + "  UPDATE MF_TY SET CLS_ID_LOST = 'F' WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO \n";
            SqlParameter[] _sqlPara = new SqlParameter[6];
            _sqlPara[0] = new SqlParameter("@TY_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = tyId;
            _sqlPara[1] = new SqlParameter("@TY_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = tyNo;
            _sqlPara[2] = new SqlParameter("@TR_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = trNo;
            _sqlPara[3] = new SqlParameter("@TR_NO_C", SqlDbType.VarChar, 20);
            _sqlPara[3].Value = _trNoCurrent;
            _sqlPara[4] = new SqlParameter("@PRE_ITM", SqlDbType.Int);
            if (!String.IsNullOrEmpty(bilItm))
            {
                _sqlPara[4].Value = Convert.ToInt32(bilItm);
            }
            _sqlPara[5] = new SqlParameter("@QTY_LOST_RTN", SqlDbType.Decimal);
            _sqlPara[5].Precision = 28;
            _sqlPara[5].Scale = 8;
            _sqlPara[5].Value = qtyLostRtn;
            
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }

        /// <summary>
        /// 回写检验单的缴库单号及合格量
        /// </summary>
        /// <param name="tyId">检验单据别</param>
        /// <param name="tyNo">检验单号</param>
        /// <param name="bilItm">项次</param>
        /// <param name="mmNo">缴库单号</param>
        /// <param name="qtyOkRtn">已转合格量</param>
        public void UpdateMmNo(string tyId, string tyNo, string bilItm,string mmId, string mmNo,decimal qtyOkRtn)
        {
            string _mmNoCurrent = mmNo;
            string _sqlStr = "UPDATE TF_TY SET MM_ID=@MM_ID,MM_NO = @MM_NO_C,QTY_OK_RTN = ISNULL(QTY_OK_RTN,0) + @QTY_OK_RTN WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO";
            if (!String.IsNullOrEmpty(bilItm))
            {
                _sqlStr += " AND PRE_ITM = @PRE_ITM";
            }
            else if (!String.IsNullOrEmpty(mmNo))
            {
                _sqlStr += " AND MM_NO = @MM_NO";
                _mmNoCurrent = "";
            }
            else
            {
                return;
            }
            _sqlStr += ";IF NOT EXISTS(SELECT 1 FROM TF_TY WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO AND ISNULL(MM_NO, '') = '') \n"
                + "  UPDATE MF_TY SET CLS_ID_OK = 'T' WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO \n"
                + "ELSE \n"
                + "  UPDATE MF_TY SET CLS_ID_OK = 'F' WHERE TY_ID = @TY_ID AND TY_NO = @TY_NO \n";
            SqlParameter[] _sqlPara = new SqlParameter[7];
            _sqlPara[0] = new SqlParameter("@TY_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = tyId;
            _sqlPara[1] = new SqlParameter("@TY_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = tyNo;
            _sqlPara[2] = new SqlParameter("@MM_ID", SqlDbType.VarChar, 2);
            _sqlPara[2].Value = mmId;
            _sqlPara[3] = new SqlParameter("@MM_NO", SqlDbType.VarChar, 20);
            _sqlPara[3].Value = mmNo;
            _sqlPara[4] = new SqlParameter("@MM_NO_C", SqlDbType.VarChar, 20);
            _sqlPara[4].Value = _mmNoCurrent;
            _sqlPara[5] = new SqlParameter("@PRE_ITM", SqlDbType.Int);
            if (!String.IsNullOrEmpty(bilItm))
            {
                _sqlPara[5].Value = Convert.ToInt32(bilItm);
            }
            _sqlPara[6] = new SqlParameter("@QTY_OK_RTN", SqlDbType.Decimal);
            _sqlPara[6].Precision = 28;
            _sqlPara[6].Scale = 8;
            _sqlPara[6].Value = qtyOkRtn;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
    }
}
