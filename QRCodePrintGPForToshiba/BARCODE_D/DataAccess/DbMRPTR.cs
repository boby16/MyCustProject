using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// 异常通知单
    /// </summary>
    public class DbMRPTR : DbObject
    {
        /// <summary>
        /// 异常通知单
        /// </summary>
        /// <param name="connStr"></param>
        public DbMRPTR(string connStr)
            : base(connStr)
        {
        }
        #region 取数据
        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="trNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string trNo, bool onlyFillSchema)
        {
            string _sql = " SELECT TZERR.*,TF_TY.QTY_LOST,TF_TY.WH,PRDT.NAME AS MRP_NAME,PRDT.SPC AS MRP_SPC "
                        + " FROM TZERR "                        
                        + " LEFT JOIN TF_TY ON TF_TY.BUILD_BIL = TZERR.TR_NO "
                        + " LEFT JOIN PRDT ON PRDT.PRD_NO = TZERR.MRP_NO "
                        + " WHERE TZERR.TR_NO = @TR_NO "
                        + " ;"
                        + " SELECT TF_TZERR.*,PRDT.NAME AS PRD_NAME,PRDT.SPC FROM TF_TZERR "
                        + " LEFT JOIN PRDT ON PRDT.PRD_NO = TF_TZERR.PRD_NO "
                        + "WHERE TF_TZERR.TR_NO = @TR_NO;";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@TR_NO", SqlDbType.VarChar, 20);
            _aryPt[0].Value = trNo;
            if (string.IsNullOrEmpty(trNo))
                onlyFillSchema = true;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[2] { "TZERR", "TF_TZERR" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[2] { "TZERR", "TF_TZERR" }, _aryPt);
            }

            _ds.Relations.Add("TZERRTF_TZERR", _ds.Tables["TZERR"].Columns["TR_NO"], _ds.Tables["TF_TZERR"].Columns["TR_NO"]);

            return _ds;
        }
        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            string _sql = "SELECT TZERR.*,TF_TY.QTY_LOST,TF_TY.WH,PRDT.NAME AS MRP_NAME,CONTACT.CNT_NO,CONTACT.NAME AS CNT_NAME,PRDT.SPC AS MRP_SPC "
                        + " FROM TZERR "
                        + " LEFT JOIN TF_TY ON TF_TY.BUILD_BIL = TZERR.TR_NO "
                        + " LEFT JOIN MF_MO ON TZERR.MO_NO = MF_MO.MO_NO "
                        + " LEFT JOIN \n"
                        + " TF_POS ON TF_POS.OS_ID = 'SO' AND TF_POS.OS_NO = MF_MO.SO_NO AND TF_POS.EST_ITM=MF_MO.EST_ITM \n"
                        + " LEFT JOIN \n"
                        + " TF_RCV ON TF_RCV.RV_ID = TF_POS.BIL_ID AND TF_RCV.RV_NO = TF_POS.QT_NO AND TF_RCV.KEY_ITM=TF_POS.OTH_ITM \n"
                        + " LEFT JOIN \n"
                        + " MF_MA ON MF_MA.MA_ID = TF_RCV.MA_ID AND MF_MA.MA_NO = TF_RCV.MA_NO \n"
                        + " LEFT JOIN \n"
                        + " CONTACT ON CONTACT.CNT_NO = MF_MA.CNT_NO AND CONTACT.CUS_NO = MF_MA.CUS_NO \n"
                        + " LEFT JOIN PRDT ON PRDT.PRD_NO = TZERR.MRP_NO "
                        + " WHERE ISNULL(TZERR.CHK_MAN, '') <> '' AND (ISNULL(MF_MO.ISSVS, 'F') = 'T' OR ISNULL(TZERR.MO_NO, '') = '') " + sqlWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "TZERR" });
            _ds.Tables["TZERR"].PrimaryKey = new DataColumn[1] { _ds.Tables["TZERR"].Columns["TR_NO"] };
            return _ds;
        }
        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDetail(string sqlWhere)
        {
            string _sql = "SELECT * FROM TF_TZERR WHERE 1 = 1 " + sqlWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "TF_TZERR" });
            return _ds;
        }
        /// <summary>
        /// 通过验收单号取数据
        /// </summary>
        /// <param name="tyId"></param>
        /// <param name="tyNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByTy(string tyId, string tyNo)
        {
            string _sqlStr = " SELECT A.TR_NO,A.TR_DD,A.TZ_NO,A.ZC_NO,A.QTY,A.DEP,A.SPC_NO,A.PRC_AD,A.PRC_ID,A.USR_NO,A.USR,A.CHK_MAN,A.LOCK_MAN,"
                        + " A.PRT_SW,A.CPY_SW,A.REM1,A.REM2,A.CLOSE_ID,A.MO_NO,A.MRP_NO,A.PRD_MARK,A.BIL_BUILD,A.ID_NO,A.BIL_ID,"
                        + " A.BIL_NO,A.CLS_DATE,A.BAT_NO,A.OLEFIELD,A.BIL_TYPE,A.COMPOSE_IDNO,A.MOB_ID,A.LOCK_MAN,A.SYS_DATE,A.PRC_ID2,"
                        + " A.BIL_BUILD2,A.CAS_NO,A.TASK_ID,A.QTY1,A.UNIT,A.PRT_USR,A.ZC_PRD,A.PRD_ZC "
                        + " FROM TZERR A "
                        + " WHERE A.BIL_ID=@BIL_ID AND A.BIL_NO=@BIL_NO "
                        + " ;"
                        + " SELECT A.TR_NO,A.ZC_NO,A.DEP,A.DEP_UP,A.DEP_DOWN,A.QTY,A.STA_DD,A.END_DD,A.TZ_NO,A.MD_NO,A.PRD_NO,A.PRD_MARK,"
                        + " A.WH,A.BAT_NO,A.UP,A.ZC_ITM,A.CHK_ID,A.MV_ID,A.QTY_PRC,A.ZC_NO_DN,A.ZC_NO_UP,A.CUS_NO,A.TW_ID,A.SEB_NO,A.GRP_NO,"
                        + " A.MM_NO "
                        + " FROM TF_TZERR A "
                        + " WHERE A.TR_NO IN (SELECT TR_NO FROM TZERR WHERE BIL_ID=@BIL_ID AND BIL_NO=@BIL_NO)";
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = tyId;
            _sqlPara[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = tyNo;

            this.FillDataset(_sqlStr, _ds, new string[2] {"TZERR", "TF_TZERR" }, _sqlPara);
            return _ds;
        }

        #endregion               
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        public void Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _sql = "UPDATE TZERR SET CHK_MAN = @CHK_MAN, CLS_DATE = @CLS_DATE WHERE TR_NO = @TR_NO";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _aryPt[0].Value = chk_man;
            _aryPt[1] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _aryPt[1].Value = cls_dd.ToString("yyyy-MM-dd HH:mm:ss");
            _aryPt[2] = new SqlParameter("@TR_NO", SqlDbType.VarChar, 20);
            _aryPt[2].Value = bil_no;
            this.ExecuteNonQuery(_sql, _aryPt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        public void RollBack(string bil_id, string bil_no)
        {
            string _sql = "UPDATE TZERR SET CHK_MAN = NULL, CLS_DATE = NULL WHERE TR_NO = @TR_NO";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@TR_NO", SqlDbType.VarChar, 20);
            _aryPt[0].Value = bil_no;
            this.ExecuteNonQuery(_sql, _aryPt);
        }
        /// <summary>
        /// 设置异常单的处理状态为未处理
        /// </summary>
        /// <param name="trNo"></param>
        public void UpdateUnCloseId(string trNo)
        {
            string _sqlStr = "UPDATE TZERR SET  CLOSE_ID=NULL,MO_NO=NULL WHERE TR_NO=@TR_NO";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@TR_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = trNo;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        /// <summary>
        /// 产生不合格数量的单据
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <param name="qtyLost"></param>
        public void UpdateMOQtyLost(string moNo, decimal qtyLost)
        {
            string _sqlStr = "UPDATE MF_MO SET QTY_LOST=ISNULL(QTY_LOST,0) + @QTY_LOST WHERE MO_NO=@MO_NO";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            _sqlPara[1] = new SqlParameter("@QTY_LOST", SqlDbType.Decimal);
            _sqlPara[1].Precision = 28;
            _sqlPara[1].Scale = 8;
            _sqlPara[1].Value = qtyLost;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
    }
}
