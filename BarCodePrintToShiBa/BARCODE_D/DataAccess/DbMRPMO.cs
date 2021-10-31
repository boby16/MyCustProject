using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    public class DbMRPMO : DbObject
    {
        public DbMRPMO(string connStr)
            : base(connStr)
        {
        }

        #region 取数据
        /// <summary>
        /// 取制令单资料
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            string _sqlFilter = "ISNULL(MF_MO.CHK_MAN, '') <> '' AND ISNULL(MF_MO.CF_ID, '') = 'T' AND ISNULL(MF_MO.CLOSE_ID, '') <> 'T' "
                + " AND ISNULL(MF_MO.ISSVS, '') = 'T' AND ISNULL(MF_MO.QTY, 0) > ISNULL(MF_MO.QTY_RK, 0)";
            string _sql = "SELECT DISTINCT MF_MO.MO_DD,MF_MO.MO_NO,MF_MO.CUS_NO,CUST.NAME  AS CUS_NAME,MF_MO.NEED_DD,MF_MO.MRP_NO,MF_MO.PRD_MARK,CONVERT(VARCHAR(1000),PRDT.SPC) AS SPC,"
                + " MF_MO.WH,MF_MO.UNIT,MF_MO.QTY,MF_MO.QTY1,MF_MO.BAT_NO,CONVERT(VARCHAR(1000),MF_MO.REM) AS REM,MF_MO.QTY_RK,MF_MO.QTY_RK_UNSH,MF_MO.ID_NO,MF_MO.DEP,MF_MO.BIL_TYPE, "
                + " MF_MA.CNT_NO,CONTACT.NAME AS CNT_NAME "
                + " FROM MF_MO "
                + " JOIN TF_MO ON MF_MO.MO_NO = TF_MO.MO_NO"
                + " LEFT JOIN TF_POS ON TF_POS.OS_ID='SO' AND TF_POS.OS_NO = MF_MO.SO_NO AND TF_POS.EST_ITM=MF_MO.EST_ITM "
                + " LEFT JOIN TF_RCV ON TF_RCV.RV_ID=TF_POS.BIL_ID AND TF_RCV.RV_NO = TF_POS.QT_NO AND TF_RCV.KEY_ITM = TF_POS.OTH_ITM "
                + " LEFT JOIN MF_MA ON MF_MA.MA_ID=TF_RCV.MA_ID AND MF_MA.MA_NO = TF_RCV.MA_NO "
                + " LEFT JOIN CONTACT ON CONTACT.CNT_NO=MF_MA.CNT_NO AND CONTACT.CUS_NO = MF_MA.CUS_NO "
                + " LEFT JOIN PRDT ON PRDT.PRD_NO = MF_MO.MRP_NO"
                + " LEFT JOIN CUST ON CUST.CUS_NO = MF_MO.CUS_NO"
                + " WHERE " + _sqlFilter + sqlWhere + ";"
                + " SELECT TF_MO.*,MF_MA.CNT_NO,CONTACT.NAME AS CNT_NAME "
                + " FROM  TF_MO "
                + " JOIN MF_MO ON MF_MO.MO_NO = TF_MO.MO_NO"
                + " LEFT JOIN TF_POS ON TF_POS.OS_ID='SO' AND TF_POS.OS_NO = MF_MO.SO_NO AND TF_POS.EST_ITM=MF_MO.EST_ITM "
                + " LEFT JOIN TF_RCV ON TF_RCV.RV_ID=TF_POS.BIL_ID AND TF_RCV.RV_NO = TF_POS.QT_NO AND TF_RCV.KEY_ITM = TF_POS.OTH_ITM "
                + " LEFT JOIN MF_MA ON MF_MA.MA_ID=TF_RCV.MA_ID AND MF_MA.MA_NO = TF_RCV.MA_NO "
                + " LEFT JOIN CONTACT ON CONTACT.CNT_NO=MF_MA.CNT_NO AND CONTACT.CUS_NO = MF_MA.CUS_NO "
                + " WHERE " + _sqlFilter + sqlWhere + ";";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[2] { "MF_MO", "TF_MO" });
            return _ds;
        }

        /// <summary>
        /// 取制令单资料
        /// </summary>
        /// <param name="onlyFillSchema"></param>
        /// <param name="moNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string moNo, bool onlyFillSchema)
        {
            string _sql = " SELECT A.MO_NO,A.MO_DD,A.STA_DD,A.END_DD,A.BIL_ID,A.BIL_NO,A.MRP_NO,A.PRD_MARK,A.WH,A.SO_NO,A.UNIT,A.QTY,A.QTY1,"
                        + " A.NEED_DD,A.DEP,A.CUS_NO,A.CLOSE_ID,A.USR,A.CHK_MAN,A.BAT_NO,A.REM,A.PO_OK,A.MO_NO_ADD,"
                        + " A.QTY_FIN,A.QTY_FIN_UNSH,A.TIME_AJ,A.QTY_ML,A.QTY_ML_UNSH,A.BUILD_BIL,A.CST_MAKE,A.CST_PRD,A.CST_OUT,A.CST_MAN,A.USED_TIME,"
                        + " A.CST,A.PRT_SW,A.OPN_DD,A.FIN_DD,A.BIL_MAK,A.CPY_SW,A.CONTRACT,A.EST_ITM,A.ML_OK,A.MD_NO,A.QTY_RK,A.QTY_RK_UNSH,"
                        + " A.CLS_DATE,A.ID_NO,A.QTY_CHK,A.CONTROL,A.ISNORMAL,A.QC_YN,A.MM_CURML,A.TS_ID,A.BIL_TYPE,A.CNTT_NO,"
                        + " A.MOB_ID,A.LOCK_MAN,A.SEB_NO,A.GRP_NO,A.OUT_DD_MOJ,A.SYS_DATE,A.PG_ID,A.SUP_PRD_NO,A.TIME_CNT,"
                        + " A.ML_BY_MM,A.CAS_NO,A.TASK_ID,A.OLD_ID,A.CF_ID,A.CUS_OS_NO,A.PRT_USR,A.QTY_QL,A.QTY_QL_UNSH,A.QL_ID,A.Q2_ID,"
                        + " A.Q3_ID,A.ISSVS,A.QTY_DM,A.LOCK,A.QTY_LOST,A.ISFROMQD,A.ZT_ID,A.ZT_DD,A.CV_ID,"
                        + " C.WC_NO,"
                        + " P.SPC "
                        + " FROM MF_MO A"
                        + " LEFT JOIN PRDT P ON A.MRP_NO= P.PRD_NO "
                        + " LEFT JOIN TF_POS C ON  C.OS_ID ='SO' AND C.OS_NO = A.SO_NO AND C.EST_ITM = A.EST_ITM "
                        + " WHERE A.MO_NO = @MO_NO;"
                        + " SELECT A.MO_NO,A.ITM,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WH,A.UNIT,A.QTY_RSV,A.QTY_LOST,A.QTY,A.QTY_UNSH,A.BAT_NO,A.REM,"
                        + " A.CST,A.ZC_NO,A.TW_ID,A.ZC_REM,A.USEIN,A.CPY_SW,A.USEIN_NO,A.PRD_NO_CHG,A.QTY1_RSV,A.QTY1_LOST,"
                        + " A.ID_NO,A.MD_NO,A.QTY_TS,A.QTY_TS_UNSH,A.TS_ITM,A.COMPOSE_IDNO,A.EST_ITM,A.PRE_ITM,A.LOS_RTO,A.QTY_STD,A.SEB_NO,"
                        + " A.GRP_NO,A.ZC_PRD,A.CHG_RTO,A.CHG_ITM,A.QTY_CHG_RTO,A.QTY_QL,A.QTY_QL_UNSH,A.QTY1_QL,A.QTY_DM,A.QTY1_DM,A.QTY_BL,A.QTY_BL_UNSH,A.CHK_RTN,"
                        + " ISNULL(A.QTY_RSV,0) +ISNULL(A.QTY_LOST,0) AS QTY_GET, "
                        + "P.SPC"
                        + " FROM TF_MO A"
                        + " LEFT JOIN PRDT P ON A.PRD_NO= P.PRD_NO "
                        + " WHERE A.MO_NO = @MO_NO"
                        + " ORDER BY A.ITM";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _aryPt[0].Value = moNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[2] { "MF_MO", "TF_MO" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[2] { "MF_MO", "TF_MO" }, _aryPt);
            }
            if (_ds.Tables["MF_MO"].Columns.Contains("WC_NO"))
                _ds.Tables["MF_MO"].Columns["WC_NO"].ReadOnly = false;
            if (_ds.Tables["MF_MO"].Columns.Contains("SPC"))
                _ds.Tables["MF_MO"].Columns["SPC"].ReadOnly = false;
            if (_ds.Tables["TF_MO"].Columns.Contains("SPC"))
                _ds.Tables["TF_MO"].Columns["SPC"].ReadOnly = false;
            DataColumn[] _dcHead = null;
            DataColumn[] _dcBody = null;
            _dcHead = new DataColumn[1];
            _dcHead[0] = _ds.Tables["MF_MO"].Columns["MO_NO"];
            _dcBody = new DataColumn[1];
            _dcBody[0] = _ds.Tables["TF_MO"].Columns["MO_NO"];
            _ds.Relations.Add("MF_MOTF_MO", _dcHead, _dcBody);   
            return _ds;
        }
        /// <summary>
        /// 取得未领完料制令单信息
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataUnUse(string moNo)
        {
            string _sqlStr = " SELECT MO_NO,MO_DD,STA_DD,END_DD,BIL_ID,BIL_NO,MRP_NO,PRD_MARK,WH,SO_NO,UNIT,QTY,QTY1,"
                                   + " NEED_DD,DEP,CUS_NO,CLOSE_ID,USR,CHK_MAN,BAT_NO,REM,PO_OK,MO_NO_ADD,"
                                   + " QTY_FIN,QTY_FIN_UNSH,TIME_AJ,QTY_ML,QTY_ML_UNSH,BUILD_BIL,CST_MAKE,CST_PRD,CST_OUT,CST_MAN,USED_TIME,"
                                   + " CST,PRT_SW,OPN_DD,FIN_DD,BIL_MAK,CPY_SW,CONTRACT,EST_ITM,ML_OK,MD_NO,QTY_RK,QTY_RK_UNSH,"
                                   + " CLS_DATE,ID_NO,QTY_CHK,CONTROL,ISNORMAL,QC_YN,MM_CURML,TS_ID,BIL_TYPE,CNTT_NO,"
                                   + " MOB_ID,LOCK_MAN,SEB_NO,GRP_NO,OUT_DD_MOJ,SYS_DATE,PG_ID,SUP_PRD_NO,TIME_CNT,"
                                   + " ML_BY_MM,CAS_NO,TASK_ID,OLD_ID,CF_ID,CUS_OS_NO,PRT_USR,QTY_QL,QL_ID,Q2_ID,"
                                   + " Q3_ID,ISSVS,QTY_DM,LOCK,QTY_LOST,ISFROMQD,ZT_ID,ZT_DD,CV_ID"
                                   + " FROM MF_MO "
                                   + " WHERE MO_NO = @MO_NO;"
                                   + " SELECT A.MO_NO,A.ITM,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WH,A.UNIT,A.QTY_RSV,A.QTY_LOST,A.QTY,A.QTY_UNSH,A.BAT_NO,A.REM,"
                                   + " A.CST,A.ZC_NO,A.TW_ID,A.ZC_REM,A.USEIN,A.CPY_SW,A.USEIN_NO,A.PRD_NO_CHG,A.QTY1_RSV,A.QTY1_LOST,"
                                   + " A.ID_NO,A.MD_NO,A.QTY_TS,A.QTY_TS_UNSH,A.TS_ITM,A.COMPOSE_IDNO,A.EST_ITM,A.PRE_ITM,A.LOS_RTO,A.QTY_STD,A.SEB_NO,"
                                   + " A.GRP_NO,A.ZC_PRD,A.CHG_RTO,A.CHG_ITM,A.QTY_CHG_RTO,A.QTY_QL,A.QTY_QL_UNSH,A.QTY1_QL,A.QTY_DM,A.QTY1_DM,A.QTY_BL,A.QTY_BL_UNSH,A.CHK_RTN,"
                                   + " ISNULL(A.QTY_RSV,0) +ISNULL(A.QTY_LOST,0) AS QTY_GET "
                                   + " FROM TF_MO A"
                                   + " WHERE A.MO_NO = @MO_NO AND ISNULL(A.QTY_RSV,0) + ISNULL(A.QTY_LOST,0) > ISNULL(A.QTY,0) "
                                   + " ORDER BY A.ITM";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[2] { "MF_MO", "TF_MO" }, _sqlPara);            
            DataColumn[] _dcHead = null;
            DataColumn[] _dcBody = null;
            _dcHead = new DataColumn[1];
            _dcHead[0] = _ds.Tables["MF_MO"].Columns["MO_NO"];
            _dcBody = new DataColumn[1];
            _dcBody[0] = _ds.Tables["TF_MO"].Columns["MO_NO"];
            _ds.Relations.Add("MF_MOTF_MO", _dcHead, _dcBody);
            return _ds;
        }
        /// <summary>
        /// 取得已领完料制令单信息
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataUse(string moNo)
        {
            string _sqlStr = " SELECT MO_NO,MO_DD,STA_DD,END_DD,BIL_ID,BIL_NO,MRP_NO,PRD_MARK,WH,SO_NO,UNIT,QTY,QTY1,"
                                   + " NEED_DD,DEP,CUS_NO,CLOSE_ID,USR,CHK_MAN,BAT_NO,REM,PO_OK,MO_NO_ADD,"
                                   + " QTY_FIN,QTY_FIN_UNSH,TIME_AJ,QTY_ML,QTY_ML_UNSH,BUILD_BIL,CST_MAKE,CST_PRD,CST_OUT,CST_MAN,USED_TIME,"
                                   + " CST,PRT_SW,OPN_DD,FIN_DD,BIL_MAK,CPY_SW,CONTRACT,EST_ITM,ML_OK,MD_NO,QTY_RK,QTY_RK_UNSH,"
                                   + " CLS_DATE,ID_NO,QTY_CHK,CONTROL,ISNORMAL,QC_YN,MM_CURML,TS_ID,BIL_TYPE,CNTT_NO,"
                                   + " MOB_ID,LOCK_MAN,SEB_NO,GRP_NO,OUT_DD_MOJ,SYS_DATE,PG_ID,SUP_PRD_NO,TIME_CNT,"
                                   + " ML_BY_MM,CAS_NO,TASK_ID,OLD_ID,CF_ID,CUS_OS_NO,PRT_USR,QTY_QL,QL_ID,Q2_ID,"
                                   + " Q3_ID,ISSVS,QTY_DM,LOCK,QTY_LOST,ISFROMQD,ZT_ID,ZT_DD,CV_ID"
                                   + " FROM MF_MO "
                                   + " WHERE MO_NO = @MO_NO;"
                                   + " SELECT A.MO_NO,A.ITM,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WH,A.UNIT,A.QTY_RSV,A.QTY_LOST,A.QTY,A.BAT_NO,A.REM,"
                                   + " A.CST,A.ZC_NO,A.TW_ID,A.ZC_REM,A.USEIN,A.CPY_SW,A.USEIN_NO,A.PRD_NO_CHG,A.QTY1_RSV,A.QTY1_LOST,"
                                   + " A.ID_NO,A.MD_NO,A.QTY_TS,A.QTY_TS_UNSH,A.TS_ITM,A.COMPOSE_IDNO,A.EST_ITM,A.PRE_ITM,A.LOS_RTO,A.QTY_STD,A.SEB_NO,"
                                   + " A.GRP_NO,A.ZC_PRD,A.CHG_RTO,A.CHG_ITM,A.QTY_CHG_RTO,A.QTY_QL,A.QTY1_QL,A.QTY_DM,A.QTY1_DM,A.QTY_BL,A.CHK_RTN,"
                                   + " ISNULL(A.QTY_RSV,0) +ISNULL(A.QTY_LOST,0) AS QTY_GET "
                                   + " FROM TF_MO A"
                                   + " WHERE A.MO_NO = @MO_NO AND  ISNULL(A.QTY,0) > 0 "
                                   + " ORDER BY A.ITM";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[2] { "MF_MO", "TF_MO" }, _sqlPara);
            DataColumn[] _dcHead = null;
            DataColumn[] _dcBody = null;
            _dcHead = new DataColumn[1];
            _dcHead[0] = _ds.Tables["MF_MO"].Columns["MO_NO"];
            _dcBody = new DataColumn[1];
            _dcBody[0] = _ds.Tables["TF_MO"].Columns["MO_NO"];
            _ds.Relations.Add("MF_MOTF_MO", _dcHead, _dcBody);
            return _ds;
        }
        /// <summary>
        /// 取制令单表头数据
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataHead(string moNo)
        {
            string _sqlStr = " SELECT MO_NO,MO_DD,STA_DD,END_DD,BIL_ID,BIL_NO,MRP_NO,PRD_MARK,WH,SO_NO,UNIT,QTY,QTY1,"
                                   + " NEED_DD,DEP,CUS_NO,CLOSE_ID,USR,CHK_MAN,BAT_NO,REM,PO_OK,MO_NO_ADD,"
                                   + " QTY_FIN,QTY_FIN_UNSH,TIME_AJ,QTY_ML,QTY_ML_UNSH,BUILD_BIL,CST_MAKE,CST_PRD,CST_OUT,CST_MAN,USED_TIME,"
                                   + " CST,PRT_SW,OPN_DD,FIN_DD,BIL_MAK,CPY_SW,CONTRACT,EST_ITM,ML_OK,MD_NO,QTY_RK,QTY_RK_UNSH,"
                                   + " CLS_DATE,ID_NO,QTY_CHK,CONTROL,ISNORMAL,QC_YN,MM_CURML,TS_ID,BIL_TYPE,CNTT_NO,"
                                   + " MOB_ID,LOCK_MAN,SEB_NO,GRP_NO,OUT_DD_MOJ,SYS_DATE,PG_ID,SUP_PRD_NO,TIME_CNT,"
                                   + " ML_BY_MM,CAS_NO,TASK_ID,OLD_ID,CF_ID,CUS_OS_NO,PRT_USR,QTY_QL,QL_ID,Q2_ID,"
                                   + " Q3_ID,ISSVS,QTY_DM,LOCK,QTY_LOST,ISFROMQD,ZT_ID,ZT_DD,CV_ID"
                                   + " FROM MF_MO "
                                   + " WHERE MO_NO = @MO_NO;";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[1] { "MF_MO" }, _sqlPara);
            return _ds;
        }
        /// <summary>
        /// 根据回写项次取得表身记录
        /// </summary>
        /// <param name="moNo">制令单</param>
        /// <param name="fieldName">回写项次名称</param>
        /// <param name="estItm">回写项次</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string moNo,string fieldName, int estItm)
        {
            string _sqlStr = " SELECT MO_NO,MO_DD,STA_DD,END_DD,BIL_ID,BIL_NO,MRP_NO,PRD_MARK,WH,SO_NO,UNIT,QTY,QTY1,"
                           + " NEED_DD,DEP,CUS_NO,CLOSE_ID,USR,CHK_MAN,BAT_NO,REM,PO_OK,MO_NO_ADD,"
                           + " QTY_FIN,QTY_FIN_UNSH,TIME_AJ,QTY_ML,QTY_ML_UNSH,BUILD_BIL,CST_MAKE,CST_PRD,CST_OUT,CST_MAN,USED_TIME,"
                           + " CST,PRT_SW,OPN_DD,FIN_DD,BIL_MAK,CPY_SW,CONTRACT,EST_ITM,ML_OK,MD_NO,QTY_RK,QTY_RK_UNSH,"
                           + " CLS_DATE,ID_NO,QTY_CHK,CONTROL,ISNORMAL,QC_YN,MM_CURML,TS_ID,BIL_TYPE,CNTT_NO,"
                           + " MOB_ID,LOCK_MAN,SEB_NO,GRP_NO,OUT_DD_MOJ,SYS_DATE,PG_ID,SUP_PRD_NO,TIME_CNT,"
                           + " ML_BY_MM,CAS_NO,TASK_ID,OLD_ID,CF_ID,CUS_OS_NO,PRT_USR,QTY_QL,QL_ID,Q2_ID,"
                           + " Q3_ID,ISSVS,QTY_DM,LOCK,QTY_LOST,ISFROMQD,ZT_ID,ZT_DD,CV_ID"
                           + " FROM MF_MO "
                           + " WHERE MO_NO = @MO_NO;"
                           + "SELECT A.MO_NO,A.ITM,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WH,A.UNIT,A.QTY_RSV,A.QTY_LOST,A.QTY,A.QTY_UNSH,A.BAT_NO,A.REM,"
                           + " A.CST,A.ZC_NO,A.TW_ID,A.ZC_REM,A.USEIN,A.CPY_SW,A.USEIN_NO,A.PRD_NO_CHG,A.QTY1_RSV,A.QTY1_LOST,"
                           + " A.ID_NO,A.MD_NO,A.QTY_TS,A.QTY_TS_UNSH,A.TS_ITM,A.COMPOSE_IDNO,A.EST_ITM,A.PRE_ITM,A.LOS_RTO,A.QTY_STD,A.SEB_NO,"
                           + " A.GRP_NO,A.ZC_PRD,A.CHG_RTO,A.CHG_ITM,A.QTY_CHG_RTO,A.QTY_QL,A.QTY_QL_UNSH,A.QTY1_QL,A.QTY_DM,A.QTY1_DM,A.QTY_BL,A.QTY_BL_UNSH,A.CHK_RTN,"
                           + " ISNULL(A.QTY_RSV,0) +ISNULL(A.QTY_LOST,0) AS QTY_GET "
                           + " FROM TF_MO A"
                           + " WHERE A.MO_NO = @MO_NO AND " + fieldName + "=@EST_ITM "
                           + " ORDER BY A.ITM";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            _sqlPara[1] = new SqlParameter("@EST_ITM", SqlDbType.Int);
            _sqlPara[1].Value = estItm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[2] {"MF_MO","TF_MO" }, _sqlPara);
            return _ds; 
        }
        /// <summary>
        /// 取得表身记录
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <param name="prdNo">品号</param>
        /// <param name="prdMark">特征</param>
        /// <param name="wh">库位</param>
        /// <param name="useInNo">组装位置</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string moNo, string prdNo, string prdMark, string wh, string useInNo)
        {
            string _sqlStr = "";
            string _sqlWhere = "";
            int _paraCount = 1;
            if (!string.IsNullOrEmpty(prdNo))
                _paraCount++;
            if (!string.IsNullOrEmpty(prdMark))
                _paraCount++;
            if (!string.IsNullOrEmpty(wh))
                _paraCount++;
            if (!string.IsNullOrEmpty(useInNo))
                _paraCount++;

            SqlParameter[] _sqlPara = new SqlParameter[_paraCount];
            _paraCount = 0;
            _sqlPara[_paraCount] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[_paraCount].Value = moNo;
            _sqlWhere += " AND A.MO_NO = @MO_NO";
            _paraCount++;
            if (!string.IsNullOrEmpty(prdNo))
            {
                _sqlPara[_paraCount] = new SqlParameter("@PRD_NO", SqlDbType.VarChar,30);
                _sqlPara[_paraCount].Value = prdNo;
                _sqlWhere += " AND A.PRD_NO = @PRD_NO";
                _paraCount++;
            }

            if (!string.IsNullOrEmpty(prdMark))
            {
                _sqlPara[_paraCount] = new SqlParameter("@PRD_MARK", SqlDbType.VarChar, 40);
                _sqlPara[_paraCount].Value = prdMark;
                _sqlWhere += " AND A.PRD_MARK = @PRD_MARK";
                _paraCount++;
            }
            if (!string.IsNullOrEmpty(wh))
            {
                _sqlPara[_paraCount] = new SqlParameter("@WH", SqlDbType.VarChar, 12);
                _sqlPara[_paraCount].Value = wh;
                _sqlWhere += " AND A.WH = @WH";
                _paraCount++;
            }
            if (!string.IsNullOrEmpty(useInNo))
            {
                _sqlPara[_paraCount] = new SqlParameter("@USEIN_NO", SqlDbType.VarChar, 200);
                _sqlPara[_paraCount].Value = useInNo;
                _sqlWhere += " AND A.USEIN_NO = @USEIN_NO";
                _paraCount++;
            }
            _sqlStr = "SELECT A.MO_NO,A.ITM,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WH,A.UNIT,A.QTY_RSV,A.QTY_LOST,A.QTY,A.QTY_UNSH,A.BAT_NO,A.REM,"
                                          + " A.CST,A.ZC_NO,A.TW_ID,A.ZC_REM,A.USEIN,A.CPY_SW,A.USEIN_NO,A.PRD_NO_CHG,A.QTY1_RSV,A.QTY1_LOST,"
                                          + " A.ID_NO,A.MD_NO,A.QTY_TS,A.QTY_TS_UNSH,A.TS_ITM,A.COMPOSE_IDNO,A.EST_ITM,A.PRE_ITM,A.LOS_RTO,A.QTY_STD,A.SEB_NO,"
                                          + " A.GRP_NO,A.ZC_PRD,A.CHG_RTO,A.CHG_ITM,A.QTY_CHG_RTO,A.QTY_QL,A.QTY_QL_UNSH,A.QTY1_QL,A.QTY_DM,A.QTY1_DM,A.QTY_BL,A.QTY_BL_UNSH,A.CHK_RTN,"
                                          + " ISNULL(A.QTY_RSV,0) +ISNULL(A.QTY_LOST,0) AS QTY_GET "
                                          + " FROM TF_MO A"
                                          + " WHERE  1=1 " + _sqlWhere
                                          + " ORDER BY A.ITM";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[1] { "TF_MO" }, _sqlPara);
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
            _where = " MO_NO=@MO_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_MO SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = bilNo;
            _sqlPara[1] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _sqlPara[1].Value = chkMan;
            _sqlPara[2] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            if (string.IsNullOrEmpty(chkMan))
            {
                _sqlPara[2].Value = System.DBNull.Value;
            }
            else
            {
                _sqlPara[2].Value = clsDd;
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

        #region 修改表头领料量
        /// <summary>
        /// 修改表头领料量
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <param name="qtyML">领料数量</param>
        public void UpdateQtyMl(string moNo,decimal qtyMl)
        {
            string _sqlStr = "UPDATE MF_MO SET QTY_ML =ISNULL(QTY_ML,0)+@QTY_ML WHERE MO_NO=@MO_NO";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            _sqlPara[1] = new SqlParameter("@QTY_ML", SqlDbType.Decimal);
            _sqlPara[1].Precision = 28;
            _sqlPara[1].Scale = 8;
            _sqlPara[1].Value = qtyMl;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        #endregion

        #region 修改表头开工日期
        /// <summary>
        /// 设置开工日期为NULL
        /// </summary>
        /// <param name="moNo"></param>
        public void UpdateOpnDdEmpty(string moNo)
        {
            string _sqlStr = "UPDATE MF_MO SET OPN_DD = NULL WHERE MO_NO=@MO_NO AND OPN_DD IS NOT NULL AND MO_NO NOT IN (SELECT MO_NO FROM MF_ML) ";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }

        /// <summary>
        /// 修改表头开工日期
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <param name="opnDd">开工日期</param>
        public void UpdateOpnDd(string moNo, DateTime opnDd)
        {
            string _sqlStr = "UPDATE MF_MO SET OPN_DD =@OPN_DD WHERE MO_NO=@MO_NO AND OPN_DD IS NULL ";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            _sqlPara[1] = new SqlParameter("@OPN_DD", SqlDbType.DateTime);
            _sqlPara[1].Value = opnDd;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        #endregion

        #region 修改表身量
        /// <summary>
        /// 修改表身量
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <param name="estItmFieldName">回写项次字段名</param>
        /// <param name="estItm">回写项次</param>
        /// <param name="qtyFieldName">回写数量字段名</param>
        /// <param name="qty">回写数量</param>
        public void UpdateQty(string moNo,string estItmFieldName,int estItm,string qtyFieldName, decimal qty)
        {
            string _sqlStr = "UPDATE TF_MO SET " + qtyFieldName + " =ISNULL(" + qtyFieldName + ",0)+@QTY WHERE MO_NO=@MO_NO AND " 
                            + estItmFieldName + "=@EST_ITM";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            _sqlPara[1] = new SqlParameter("@EST_ITM", SqlDbType.Int);
            _sqlPara[1].Value = estItm;
            _sqlPara[2] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _sqlPara[2].Precision = 28;
            _sqlPara[2].Scale = 8;
            _sqlPara[2].Value = qty;
            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        #endregion

        #region 修改表头已缴库量
        /// <summary>
        /// 修改表头已缴库量
        /// 回写单据数量且回写手工结案标记
        /// </summary>
        /// <param name="moNo">制令单号</param>
        /// <param name="qtyFin">缴库量</param>
        /// <returns></returns>
        public bool UpdateQtyFin(string moNo, decimal qtyFin)
        {
            bool _result = false;
            string _sqlStr = "UPDATE MF_MO SET QTY_FIN =ISNULL(QTY_FIN,0)+@QTY_FIN WHERE  MO_NO=@MO_NO "
                + "IF (@@ROWCOUNT > 0)"
                + " BEGIN \n"
                + "IF EXISTS(SELECT MO_NO FROM MF_MO WHERE MO_NO=@MO_NO AND (ISNULL(QTY,0)-ISNULL(QTY_FIN,0)-ISNULL(QTY_LOST,0)> 0))\n"
                + "	UPDATE MF_MO SET CLOSE_ID='F',FIN_DD=NULL WHERE MO_NO=@MO_NO AND ISNULL(CLOSE_ID,'F')<>'F'\n"
                + "ELSE\n"
                + "	UPDATE MF_MO SET CLOSE_ID='T',FIN_DD=@FIN_DD WHERE MO_NO=@MO_NO AND ISNULL(CLOSE_ID,'F')<>'T'\n"
                + " END ";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            _sqlPara[1] = new SqlParameter("@QTY_FIN", SqlDbType.Decimal);
            _sqlPara[1].Precision = 28;
            _sqlPara[1].Scale = 8;
            _sqlPara[1].Value = qtyFin;
            _sqlPara[2] = new SqlParameter("@FIN_DD", SqlDbType.DateTime);
            _sqlPara[2].Value = DateTime.Today;
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

        #region 回写检验合格量(QTY_RK)及不合格量(QTY_LOST)
        /// <summary>
        /// 制令单中的检验合格量(QTY_RK)及不合格量(QTY_LOST)
        /// </summary>
        /// <param name="moNo"></param>
        /// <param name="qtyChk"></param>
        /// <param name="qtyLost"></param>
        public void UpdateQtyChk(string moNo, decimal qtyChk, decimal qtyLost)
        {
            string _sql = "UPDATE MF_MO SET QTY_CHK = ISNULL(QTY_CHK, 0) + @QTY_CHK,QTY_LOST = ISNULL(QTY_LOST, 0) + @QTY_LOST WHERE MO_NO = @MO_NO";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _aryPt[0].Value = moNo;
            _aryPt[1] = new SqlParameter("@QTY_CHK", SqlDbType.Decimal);
            _aryPt[1].Value = qtyChk;
            _aryPt[2] = new SqlParameter("@QTY_LOST", SqlDbType.Decimal);
            _aryPt[2].Value = qtyLost;

            this.ExecuteNonQuery(_sql, _aryPt);
        }
        #endregion

        #region 结案
        /// <summary>
        /// 在单据上打上结案标记
        /// </summary>
        /// <param name="moNo"></param>
        /// <param name="close"></param>
        public bool UpdateClsId( string moNo, bool close)
        {
            string _sqlWhere = "";
            bool _result = false;
            _sqlWhere = " MO_NO=@MO_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_MO SET CLOSE_ID=@CLOSE_ID,CLS_DATE=@CLS_DATE WHERE " + _sqlWhere;
            SqlParameter[] _sqlPara = new SqlParameter[3];

            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;

            _sqlPara[1] = new SqlParameter("@CLOSE_ID", SqlDbType.VarChar, 1);
            _sqlPara[1].Value = close.ToString().Substring(0, 1);

            _sqlPara[2] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            if (close)
            {
                _sqlPara[2].Value = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                _sqlPara[2].Value = System.DBNull.Value;
            }

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
      /// <summary>
        /// 自动结案
      /// </summary>
      /// <param name="moNo"></param>
      /// <returns></returns>
        public bool UpdateClsId(string moNo)
        {
            bool _result = false;            
            string _sqlStr = "";
            _sqlStr = " IF EXISTS ( SELECT MO_NO FROM MF_MO WHERE ISNULL(QTY_FIN,0)+ISNULL(QTY_LOST,0) >= ISNULL(QTY,0) AND MO_NO=@MO_NO)"
                    + "     UPDATE MF_MO SET CLOSE_ID='T',CLS_DATE=@CLS_DATE WHERE MO_NO=@MO_NO "
                    + " ELSE "
                    + "     UPDATE MF_MO SET CLOSE_ID='F',CLS_DATE=NULL WHERE MO_NO=@MO_NO ";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = moNo;
            _sqlPara[1] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _sqlPara[1].Value = System.DateTime.Now.ToString("yyyy-MM-dd");
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

        #region 更新制令单表头已领料量
        /// <summary>
        /// 更新制令单表头已领料量
        /// </summary>
        /// <param name="moNo">制令单号</param>
        public void UpdateQtyMlOfMo(string moNo)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] _sqlPara = new System.Data.SqlClient.SqlParameter[1];
                _sqlPara[0] = new System.Data.SqlClient.SqlParameter("@MO_NO", SqlDbType.Char, 20);
                _sqlPara[0].Value = moNo;
                this.ExecuteSpNonQuery("SP_UPDATEQTY_ML_MO", _sqlPara);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
