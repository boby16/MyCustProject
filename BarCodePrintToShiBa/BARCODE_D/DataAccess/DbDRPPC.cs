using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// Summary description for DbDRPPC.
    /// </summary>
    public class DbDRPPC : DbObject
    {
        /// <summary>
        /// 进货单
        /// </summary>
        /// <param name="connectionString">SQL连接字串</param>
        public DbDRPPC(string connectionString)
            : base(connectionString)
        {
        }

        #region 取得进货单资料
        /// <summary>
        /// 取得进货单资料
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="OnlyFillSchema">是否只读取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string PsID, string PsNo, bool OnlyFillSchema)
        {
            string _rpId = "1";
            if (string.Compare("SA", PsID) == 0)
            {
                _rpId = "1";
            }
            else if (string.Compare("SB", PsID) == 0)
            {
                _rpId = "2";
            }
            string _sql = "select A.MOB_ID,A.PS_ID,A.PS_NO,A.PS_DD,A.PAY_DD,A.CHK_DD,A.CUS_NO,B.NAME as CUS_NAME,A.DEP,D.NAME as DEP_NAME,A.TAX_ID"
                + ",A.SEND_MTH,A.SEND_WH,A.ZHANG_ID,A.CUR_ID,A.EXC_RTO,A.SAL_NO,C.NAME as SAL_NAME,A.ARP_NO,A.PAY_MTH,A.PAY_DAYS,A.CHK_DAYS,A.INT_DAYS,A.PAY_REM"
                + ",A.CLS_ID,A.USR,A.CHK_MAN,A.PRT_SW,A.CLS_DATE,A.CK_CLS_ID,A.LZ_CLS_ID,A.YD_ID,A.BIL_TYPE,A.PH_NO,A.CUST_YG,E.NAME as CUST_YG_NAME,I.CHK_NO,I.CHK_KND,I.BANK_NO,I.BACC_NO BACC_NO_CHK,I.CRECARD_NO,I.END_DD"
                + ",isnull(F.AMTN_RCV,0) as AMTN_RCV,A.OS_ID,A.OS_NO,A.PO_ID,A.SYS_DATE,A.BIL_NO,A.REM,A.TOT_QTY,A.BAT_NO,A.CLS_REM,A.ADR,A.AMT,A.DIS_CNT,A.VOH_ID,A.VOH_NO,A.INV_NO,A.KP_ID,A.LOCK_MAN "
                + ",H.AMT_BB,H.AMTN_BB,H.AMT_BC,H.AMTN_BC,H.CACC_NO,H.BACC_NO,H.AMT_CHK AMT_CHK,H.AMTN_CHK AMTN_CHK,H.AMT_OTHER,H.AMTN_OTHER,A.AMTN_NET_CLS,A.AMT_CLS,A.TAX_CLS,A.QTY_CLS,A.TURN_ID,A.ACC_FP_NO,A.CLSLZ,A.CAS_NO,A.TASK_ID,A.EP_NO,A.EP_NO1,A.AMTN_EP,A.AMTN_EP1 "
                + " from MF_PSS A"
                + " left join CUST B on B.CUS_NO=A.CUS_NO"
                + " left join SALM C on C.SAL_NO=A.SAL_NO"
                + " left join DEPT D on D.DEP=A.DEP"
                + " left join CUST_YG E on (E.CUS_NO=A.CUS_NO and E.YG_NO=A.CUST_YG)"
                + " left join MF_ARP F on F.ARP_NO=A.ARP_NO"
                + " left join TF_MON H on A.RP_NO=H.RP_NO and H.RP_ID='" + _rpId + "' and H.ITM=1"
                //+ " left join TF_MON1 J on A.RP_NO=J.RP_NO and J.RP_ID='" + _rpId + "' and J.ITM=1"
                + " left join TF_MON4 N4 on A.RP_NO=N4.RP_NO and N4.RP_ID='" + _rpId + "' and N4.ITM=1"
                + " left join MF_CHK I on N4.CHK_NO=I.CHK_NO and I.CHK_ID='1'"
                + " where PS_ID=@PsID and PS_NO=@PsNo;"
                + "select A.PS_ID,A.PS_NO,A.ITM,A.PS_DD,A.WH,B.NAME as WH_NAME,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.UNIT,A.TAX_RTO,A.REM"
                + ",(case when A.UNIT='1' then C.UT when A.UNIT='2' then C.PK2_UT when A.UNIT='3' then C.PK3_UT else A.UNIT end) as UNIT_NAME"
                + ",A.QTY,A.UP,A.QTY1,A.UP_QTY1,A.DIS_CNT,A.AMTN_NET,A.AMT,A.CST_STD,A.UP_SALE,A.AMTN_SALE,A.PRE_ITM,A.OS_NO,A.OS_ID,A.QTY_RTN,A.QTY_RTN_UNSH,A.CSTN_SAL,A.AMTN,A.TAX,A.OTH_ITM,A.EST_ITM,A.VALID_DD,A.BAT_NO,A.BOX_ITM,C.SPC,A.QTY_FP,A.FREE_ID,A.QTY_PS, "
                + " A.PAK_UNIT,A.PAK_EXC,A.PAK_NW,A.PAK_WEIGHT_UNIT,A.PAK_GW,A.PAK_MEAST,A.PAK_MEAST_UNIT,A.SH_NO_CUS "
                + ",A.AMTN_NET_FP,A.AMT_FP,A.TAX_FP,A.QTY_CK,A.UP_MAIN,A.CST_SAL,A.AMTN_EP,A.RK_DD,A.DEP_RK "
                + " from TF_PSS A"
                + " left join MY_WH B on B.WH=A.WH"
                + " left join PRDT C on C.PRD_NO=A.PRD_NO"
                + " where A.PS_ID=@PsID and A.PS_NO=@PsNo;"
                + "select PS_ID,PS_NO,PS_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO"
                + " from TF_PSS3"
                + " where PS_ID=@PsID and PS_NO=@PsNo;"
                + " select PS_ID,PS_NO,ITM,PRD_NO,CONTENT,QTY,KEY_ITM,WH"
                + " from TF_PSS4"
                + " where PS_ID=@PsID and PS_NO=@PsNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[0].Value = PsID;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[1].Value = PsNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (OnlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[] { "MF_PSS", "TF_PSS", "TF_PSS_BAR", "TF_PSS_BOX" }, _spc);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[] { "MF_PSS", "TF_PSS", "TF_PSS_BAR", "TF_PSS_BOX" }, _spc);
            }
            _ds.Tables["TF_PSS"].Columns["UNIT_NAME"].ReadOnly = false;
            //设定PK，因为用了left join以后，PK会取不到
            DataColumn[] _dca = new DataColumn[2];
            _dca[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _ds.Tables["MF_PSS"].PrimaryKey = _dca;
            _dca = new DataColumn[3];
            _dca[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca[2] = _ds.Tables["TF_PSS"].Columns["ITM"];
            _ds.Tables["TF_PSS"].PrimaryKey = _dca;
            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _ds.Relations.Add("MF_PSSTF_PSS", _dca1, _dca2);
            //设定表身的PRE_ITM为自动递增
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrement = true;
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrementSeed = _ds.Tables["TF_PSS"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_PSS"].Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].Unique = true;
            //表身和BAR_CODE关联
            _dca1 = new DataColumn[3];
            _dca1[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca1[2] = _ds.Tables["TF_PSS"].Columns["PRE_ITM"];
            _dca2 = new DataColumn[3];
            _dca2[0] = _ds.Tables["TF_PSS_BAR"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS_BAR"].Columns["PS_NO"];
            _dca2[2] = _ds.Tables["TF_PSS_BAR"].Columns["PS_ITM"];
            _ds.Relations.Add("TF_PSSTF_PSS_BAR", _dca1, _dca2);
            //表头和表身(箱)关联
            _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_PSS_BOX"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS_BOX"].Columns["PS_NO"];
            _ds.Relations.Add("MF_PSSTF_PSS_BOX", _dca1, _dca2);
            //将KEY_ITM设为自动递增
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_PSS_BOX"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_PSS_BOX"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].Unique = true;
            return _ds;
        }
        #endregion

        #region 取得进货单资料(进货单转进货退回时用)
        /// <summary>
        /// 取得进货单资料(进货单转进货退回时用)
        /// </summary>
        /// <param name="PsNo">单据号码</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForPB(string PsNo)
        {
            string _sql = "select A.PS_ID,A.PS_NO,A.PS_DD,A.PAY_DD,A.CHK_DD,A.CUS_NO,B.NAME as CUS_NAME,A.DEP,D.NAME as DEP_NAME,A.TAX_ID"
                + ",A.SEND_MTH,A.SEND_WH,A.ZHANG_ID,A.CUR_ID,A.EXC_RTO,A.SAL_NO,C.NAME as SAL_NAME,A.ARP_NO,A.PAY_MTH,A.PAY_DAYS,A.CHK_DAYS,A.INT_DAYS,A.PAY_REM"
                + ",A.CLS_ID,A.USR,A.CHK_MAN,A.PRT_SW,A.CLS_DATE,A.CK_CLS_ID,A.LZ_CLS_ID,A.YD_ID,A.BIL_TYPE,A.PH_NO,A.CUST_YG,E.NAME as CUST_YG_NAME,A.VOH_ID,A.VOH_NO "
                + ",isnull(F.AMTN_RCV,0) as AMTN_RCV,A.OS_ID,A.OS_NO,A.PO_ID,A.SYS_DATE,A.BIL_NO,A.REM,A.TOT_QTY,A.BAT_NO,A.CLS_REM,A.ADR,A.AMT,A.DIS_CNT,A.LOCK_MAN "
                + " from MF_PSS A"
                + " left join CUST B on B.CUS_NO=A.CUS_NO"
                + " left join SALM C on C.SAL_NO=A.SAL_NO"
                + " left join DEPT D on D.DEP=A.DEP"
                + " left join CUST_YG E on (E.CUS_NO=A.CUS_NO and E.YG_NO=A.CUST_YG)"
                + " left join MF_ARP F on F.ARP_NO=A.ARP_NO"
                + " where PS_ID='PC' and PS_NO in (" + PsNo + ");"
                + "select A.PS_ID,A.PS_NO,A.ITM,A.PS_DD,A.WH,B.NAME as WH_NAME,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.UNIT,A.TAX_RTO,A.REM"
                + ",(Case When isNull(A.QTY_RTN,0)=0 or A.UNIT='1' Then C.UT When A.UNIT='2' Then C.PK2_UT When A.UNIT='3' Then C.PK3_UT End ) as UNIT_NAME"
                + ",Case When isNull(A.QTY_RTN,0)=0 or A.UNIT='1' Then (isnull(A.QTY,0)-isnull(A.QTY_RTN,0)) "
                + "	When A.UNIT='2' Then Round((isnull(A.QTY,0)-isnull(A.QTY_RTN,0))*C.PK2_QTY,0) "
                + "	When A.UNIT='3' Then Round((isnull(A.QTY,0)-isnull(A.QTY_RTN,0))*C.PK3_QTY,0) End AS QTY "
                + ",A.UP,A.DIS_CNT,A.AMTN_NET,A.AMT,A.UP_SALE,A.AMTN_SALE,A.PRE_ITM,A.OS_NO,A.OS_ID,A.QTY_RTN,A.CSTN_SAL,A.AMTN,A.TAX,A.OTH_ITM,A.EST_ITM,A.VALID_DD,A.BAT_NO,A.BOX_ITM,C.SPC,A.FREE_ID,"
                + " A.PAK_UNIT,A.PAK_EXC,A.PAK_NW,A.PAK_WEIGHT_UNIT,A.PAK_GW,A.PAK_MEAST,A.PAK_MEAST_UNIT,A.QTY1,A.UP_QTY1 "
                + " from TF_PSS A"
                + " left join MY_WH B on B.WH=A.WH"
                + " left join PRDT C on C.PRD_NO=A.PRD_NO"
                + " where A.PS_ID='PC' and A.PS_NO in (" + PsNo + ");"
                + "select A.PS_ID,A.PS_NO,A.PS_ITM,A.ITM,A.PRD_NO,A.PRD_MARK,A.BAR_CODE,A.BOX_NO"
                + " from TF_PSS3 A"
                + " inner join TF_PSS B on A.PS_ID=B.PS_ID and A.PS_NO=B.PS_NO and A.PS_ITM=B.ITM "
                + " where A.PS_ID='PC' and A.PS_NO in (" + PsNo + ") and isNull(B.QTY,0) > isNull(B.QTY_RTN,0);"
                + " select PS_ID,PS_NO,ITM,PRD_NO,CONTENT,QTY,KEY_ITM,WH"
                + " from TF_PSS4"
                + " where PS_ID='PC' and PS_NO in (" + PsNo + ") ";

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "MF_PSS", "TF_PSS", "TF_PSS_BAR", "TF_PSS_BOX" });
            _ds.Tables["TF_PSS"].Columns["UNIT_NAME"].ReadOnly = false;
            //设定PK，因为用了left join以后，PK会取不到
            DataColumn[] _dca = new DataColumn[2];
            _dca[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _ds.Tables["MF_PSS"].PrimaryKey = _dca;
            _dca = new DataColumn[3];
            _dca[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca[2] = _ds.Tables["TF_PSS"].Columns["ITM"];
            _ds.Tables["TF_PSS"].PrimaryKey = _dca;
            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _ds.Relations.Add("MF_PSSTF_PSS", _dca1, _dca2);

            _ds.Tables["TF_PSS"].Columns["QTY"].ReadOnly = false;
            //设定表身的PRE_ITM为自动递增
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrement = true;
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrementSeed = _ds.Tables["TF_PSS"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_PSS"].Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrementStep = 1;
            //表身和BAR_CODE关联
            _dca1 = new DataColumn[3];
            _dca1[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca1[2] = _ds.Tables["TF_PSS"].Columns["PRE_ITM"];
            _dca2 = new DataColumn[3];
            _dca2[0] = _ds.Tables["TF_PSS_BAR"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS_BAR"].Columns["PS_NO"];
            _dca2[2] = _ds.Tables["TF_PSS_BAR"].Columns["PS_ITM"];
            _ds.Relations.Add("TF_PSSTF_PSS_BAR", _dca1, _dca2);
            //表头和表身(箱)关联
            _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_PSS_BOX"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS_BOX"].Columns["PS_NO"];
            _ds.Relations.Add("MF_PSSTF_PSS_BOX", _dca1, _dca2);
            //将KEY_ITM设为自动递增
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_PSS_BOX"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_PSS_BOX"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            return _ds;
        }
        #endregion

        #region 取得进货单资料(进货单转进货折让时用)
        /// <summary>
        /// 取得进货单资料(进货单转进货折让时用)
        /// </summary>
        /// <param name="PsNo">单据号码</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForPD(string PsNo)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.AppendFormat(@"select A.PS_ID,A.PS_NO,A.PS_DD,A.PAY_DD,A.CHK_DD,A.CUS_NO,B.NAME as CUS_NAME,A.DEP,D.NAME as DEP_NAME,A.TAX_ID
                                ,A.SEND_MTH,A.SEND_WH,A.ZHANG_ID,A.CUR_ID,A.EXC_RTO,A.SAL_NO,C.NAME as SAL_NAME,A.ARP_NO,A.PAY_MTH,A.PAY_DAYS,A.CHK_DAYS,A.INT_DAYS,A.PAY_REM
                                ,A.CLS_ID,A.USR,A.CHK_MAN,A.PRT_SW,A.CLS_DATE,A.CK_CLS_ID,A.LZ_CLS_ID,A.YD_ID,A.BIL_TYPE,A.PH_NO,A.CUST_YG,E.NAME as CUST_YG_NAME,A.VOH_ID,A.VOH_NO 
                                ,isnull(F.AMTN_RCV,0) as AMTN_RCV,A.OS_ID,A.OS_NO,A.PO_ID,A.SYS_DATE,A.BIL_NO,A.REM,A.TOT_QTY,A.BAT_NO,A.CLS_REM,A.ADR,A.AMT,A.DIS_CNT,A.LOCK_MAN 
                                 from MF_PSS A
                                 left join CUST B on B.CUS_NO=A.CUS_NO
                                 left join SALM C on C.SAL_NO=A.SAL_NO
                                 left join DEPT D on D.DEP=A.DEP
                                 left join CUST_YG E on (E.CUS_NO=A.CUS_NO and E.YG_NO=A.CUST_YG)
                                 left join MF_ARP F on F.ARP_NO=A.ARP_NO
                                 where PS_ID='PC' and PS_NO in ( {0} );
                                SELECT     A.PS_ID, A.PS_NO, A.ITM, A.PS_DD, A.WH, B.NAME AS WH_NAME, A.PRD_NO, A.PRD_NAME, A.PRD_MARK, A.UNIT, A.TAX_RTO, A.REM, 
                                 (CASE WHEN A.UNIT = '1' THEN C.UT WHEN A.UNIT = '2' THEN C.PK2_UT WHEN A.UNIT = '3' THEN C.PK3_UT END) AS UNIT_NAME, ISNULL(A.QTY, 0)-ISNULL(A.QTY_RTN,0) 
                                 AS QTY, A.DIS_CNT, 
                                 A.AMTN_NET-(SELECT ISNULL(SUM(ISNULL(AMTN_NET,0)),0) FROM TF_PSS  WHERE PS_ID='PB' AND OS_NO in ({0}) AND OTH_ITM=A.PRE_ITM) as AMTN_NET,
                                 A.AMT-(SELECT  ISNULL(SUM(ISNULL(AMT,0)),0) FROM TF_PSS WHERE PS_ID='PB' AND OS_NO in ({0}) AND OTH_ITM=A.PRE_ITM) as AMT,
                                 A.PRE_ITM, A.OS_NO, A.OS_ID, A.CSTN_SAL, A.AMTN, A.TAX, A.OTH_ITM, A.EST_ITM, A.VALID_DD, 
                                 A.BAT_NO, A.BOX_ITM, C.SPC, A.FREE_ID, A.PAK_UNIT, A.PAK_EXC, A.PAK_NW, A.PAK_WEIGHT_UNIT, A.PAK_GW, A.PAK_MEAST, 
                                 A.PAK_MEAST_UNIT, A.QTY1
                                 FROM  TF_PSS AS A LEFT OUTER JOIN
                                 MY_WH AS B ON B.WH = A.WH LEFT OUTER JOIN
                                 PRDT AS C ON C.PRD_NO = A.PRD_NO
				                 WHERE A.PS_ID = 'PC' and A.PS_NO in ( {0} );
                                select A.PS_ID,A.PS_NO,A.PS_ITM,A.ITM,A.PRD_NO,A.PRD_MARK,A.BAR_CODE,A.BOX_NO
                                 from TF_PSS3 A
                                 inner join TF_PSS B on A.PS_ID=B.PS_ID and A.PS_NO=B.PS_NO and A.PS_ITM=B.ITM 
                                 where A.PS_ID='PC' and A.PS_NO in ( {0} ) 
                                and isNull(B.QTY,0) > isNull(B.QTY_RTN,0);
                                 select PS_ID,PS_NO,ITM,PRD_NO,CONTENT,QTY,KEY_ITM,WH
                                 from TF_PSS4
                                 where PS_ID='PC' and PS_NO in ( {0} ) ", PsNo);

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql.ToString(), _ds, new string[] { "MF_PSS", "TF_PSS", "TF_PSS_BAR", "TF_PSS_BOX" });
            _ds.Tables["TF_PSS"].Columns["UNIT_NAME"].ReadOnly = false;
            //设定PK，因为用了left join以后，PK会取不到
            DataColumn[] _dca = new DataColumn[2];
            _dca[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _ds.Tables["MF_PSS"].PrimaryKey = _dca;
            _dca = new DataColumn[3];
            _dca[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca[2] = _ds.Tables["TF_PSS"].Columns["ITM"];
            _ds.Tables["TF_PSS"].PrimaryKey = _dca;
            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _ds.Relations.Add("MF_PSSTF_PSS", _dca1, _dca2);

            _ds.Tables["TF_PSS"].Columns["QTY"].ReadOnly = false;
            //设定表身的PRE_ITM为自动递增
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrement = true;
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrementSeed = _ds.Tables["TF_PSS"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_PSS"].Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _ds.Tables["TF_PSS"].Columns["PRE_ITM"].AutoIncrementStep = 1;
            //表身和BAR_CODE关联
            _dca1 = new DataColumn[3];
            _dca1[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca1[2] = _ds.Tables["TF_PSS"].Columns["PRE_ITM"];
            _dca2 = new DataColumn[3];
            _dca2[0] = _ds.Tables["TF_PSS_BAR"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS_BAR"].Columns["PS_NO"];
            _dca2[2] = _ds.Tables["TF_PSS_BAR"].Columns["PS_ITM"];
            _ds.Relations.Add("TF_PSSTF_PSS_BAR", _dca1, _dca2);
            //表头和表身(箱)关联
            _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_PSS_BOX"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS_BOX"].Columns["PS_NO"];
            _ds.Relations.Add("MF_PSSTF_PSS_BOX", _dca1, _dca2);
            //将KEY_ITM设为自动递增
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_PSS_BOX"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_PSS_BOX"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_PSS_BOX"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            return _ds;
        }
        #endregion


        #region 取退回数
        /// <summary>
        /// 取退回数
        /// </summary>
        /// <param name="PsID">单据别</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="Itm">项次</param>
        /// <returns></returns>
        public decimal GetQtyRtn(string PsID, string PsNo,int Itm)
        {
            string _sql = "select (Case When A.UNIT='2' Then B.PK2_QTY When A.UNIT='3' Then B.PK3_QTY Else 1 End )*(isNull(A.QTY,0)-isNull(A.QTY_RTN,0)) QTY_PC from TF_PSS A "
                        + "left join prdt B on A.prd_No=B.prd_no where A.PS_ID=@PsID and A.PS_NO=@PsNo and A.ITM=@Itm ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[0].Value = PsID;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[1].Value = PsNo;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@Itm", SqlDbType.Int);
            _spc[2].Value = Itm;
            decimal _result = 0;
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _spc))
            //{
            //    if (_sdr.Read())
            //    {
            //        _result = Convert.ToDecimal(_sdr[0]);
            //    }
            //}
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = Convert.ToDecimal(_rObj);
            }
            return _result;
        }
        #endregion

        /// <summary>
        /// 取得表身数据
        /// </summary>
        /// <param name="psId">单据别</param>
        /// <param name="psNo">单号</param>
        /// <param name="itmColumnName">项次名</param>
        /// <param name="preItm">项次值</param>
        /// <param name="isPrimaryUnit">是否转成主单位数量</param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string psId, string psNo, string itmColumnName, int preItm, bool isPrimaryUnit)
        {
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = psId;
            _sqlPara[1] = new SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = psNo;
            _sqlPara[2] = new SqlParameter("@ITM", System.Data.SqlDbType.Int);
            _sqlPara[2].Value = preItm;
            string _qtyStr = "";
            string _joinStr = "";
            if (isPrimaryUnit)
            {
                _qtyStr = ",'1' AS UNIT,A.QTY * (CASE WHEN A.UNIT='2' THEN ISNULL(B.PK2_QTY,0) WHEN A.UNIT = '3' THEN ISNULL(B.PK3_QTY,0) ELSE 1 END) AS QTY"
                         + ",A.QTY1 ";
                _joinStr = " LEFT JOIN PRDT B ON B.PRD_NO = A.PRD_NO ";
            }
            else
            {
                _qtyStr = ",A.UNIT,A.QTY";
                _joinStr = "";
            }
            string _sqlStr = "SELECT "
                        + " A.PS_ID,A.PS_NO,A.ITM,A.PS_DD,A.WH,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.TAX_RTO,A.REM"
                        + _qtyStr
                        + ",A.UP,A.QTY1,A.UP,A.UP_QTY1,A.DIS_CNT,A.AMTN_NET,A.AMT,A.CST_STD,A.UP_SALE,A.AMTN_SALE,A.PRE_ITM,A.OS_NO,A.OS_ID,A.QTY_RTN,A.QTY_RTN_UNSH,A.CSTN_SAL,A.AMTN,A.TAX,A.OTH_ITM,A.EST_ITM,A.EST_ITM,A.VALID_DD,A.BAT_NO,A.BOX_ITM,A.QTY_FP,A.FREE_ID,A.QTY_PS, "
                        + " A.PAK_UNIT,A.PAK_EXC,A.PAK_NW,A.PAK_WEIGHT_UNIT,A.PAK_GW,A.PAK_MEAST,A.PAK_MEAST_UNIT,A.SH_NO_CUS "
                        + " FROM TF_PSS A "
                        + _joinStr
                        + " WHERE A.PS_ID=@PS_ID AND A.PS_NO=@PS_NO AND A." + itmColumnName + "=@ITM ";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[1] { "TF_PSS" }, _sqlPara);
            return _ds;
        }

        #region 更新单据的立账单号
        /// <summary>
        /// 更新单据的立账单号
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="ArpNo">立账单号</param>
        public void UpdateArpNo(string PsID, string PsNo,string ArpNo)
        {
            string _sql = "update MF_PSS set ARP_NO=@ArpNo where PS_ID=@PsID and PS_NO=@PsNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ArpNo", SqlDbType.VarChar, 20);
            _spc[0].Value = ArpNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[1].Value = PsID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[2].Value = PsNo;
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion

        #region 修改审核人和审核日期
        /// <summary>
        /// 修改审核人和审核日期
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="ChkMan">审核人</param>
        /// <param name="ClsDate">审核日期</param>
        /// <param name="vohNo"></param>
        public void UpdateChkMan(string PsID, string PsNo,string ChkMan,DateTime ClsDate,string vohNo)
        {
            string _sql = "update MF_PSS set CHK_MAN=@ChkMan,CLS_DATE=@ClsDate,VOH_NO=@VohNo where PS_ID=@PsID and PS_NO=@PsNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[5];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ChkMan", SqlDbType.VarChar, 12);
            if (String.IsNullOrEmpty(ChkMan))
            {
                _spc[0].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = ChkMan;
            }
            _spc[1] = new System.Data.SqlClient.SqlParameter("@ClsDate", SqlDbType.DateTime);
            if (String.IsNullOrEmpty(ChkMan))
            {
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[1].Value = ClsDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[2].Value = PsID;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[3].Value = PsNo;
            _spc[4] = new System.Data.SqlClient.SqlParameter("@VohNo", SqlDbType.VarChar, 20);
            _spc[4].Value = vohNo;
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion

        #region 修改已退数量，返回修改的记录数
        /// <summary>
        /// 修改已退数量，返回修改的记录数
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="PreItm">追踪退回数量项次</param>
        /// <param name="Qty">退回数量</param>
        /// <returns></returns>
        public string UpdateQtyRtn(string PsID, string PsNo,int PreItm,decimal Qty)
        {
            string _sql = "";
            if (PsID == "PC")
            {
                _sql = "		update TF_PSS set QTY_RTN=isNull(QTY_RTN,0)+@QTY where PS_ID=@PsID and PS_NO=@PsNo and PRE_ITM=@PreItm \n"
                    + "		if Exists(select PS_NO from TF_PSS WHERE PS_ID=@PsID and PS_NO=@PsNo and (isnull((QTY),0) > isnull((QTY_RTN),0))) \n"
                    + "			update MF_PSS set CLS_ID='F' where PS_ID=@PsID and PS_NO=@PsNo \n"
                    + "		else \n"
                    + "			update MF_PSS set CLS_ID='T' where PS_ID=@PsID and PS_NO=@PsNo \n"
                    + "	select 0\n";
            }
            else
            {
                _sql = "		update TF_POS set QTY_PS=isNull(QTY_PS,0)+@QTY where OS_ID=@PsID and OS_NO=@PsNo and PRE_ITM=@PreItm \n"
                    + "		if Exists(select OS_NO from TF_POS WHERE OS_ID=@PsID and OS_NO=@PsNo and ( (isnull((QTY),0)-isnull((QTY_PRE),0)) > isnull((QTY_PS),0) )) \n"
                    + "			update MF_POS set CLS_ID='F',BACK_ID=NULL where OS_ID=@PsID and OS_NO=@PsNo AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"//手工结案的单据不能更改
                    + "		else \n"
                    + "			update MF_POS set CLS_ID='T',BACK_ID='PC' where OS_ID=@PsID and OS_NO=@PsNo AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"//手工结案的单据不能更改
                    + "	select 0\n";
            }
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@Qty", SqlDbType.Decimal);
            _spc[0].Value = Qty;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[1].Value = PsID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[2].Value = PsNo;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@PreItm", SqlDbType.SmallInt);
            _spc[3].Value = PreItm;
            string _result = "2";
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _spc))
            //{
            //    if (_sdr.Read())
            //    {
            //        _result = _sdr[0].ToString();
            //    }
            //}
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }
        #endregion

        #region 手动更新立账结案
        /// <summary>
        /// 手动更新立账结案
        /// </summary>
        /// <param name="PsNo">单号</param>
        /// <param name="ClsID">结案标志:true为结案，false为反结案</param>
        /// <returns>错误信息</returns>
        public string CloseBill(string PsNo, bool ClsID)
        {
            string _sql = "Update MF_PSS set LZ_CLS_ID=@ClsID where PS_ID='PC' and PS_NO=@PsNo and isNull(CLSLZ,'')!='T' ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[0].Value = PsNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@ClsID", SqlDbType.VarChar, 1);
            _spc[1].Value = (ClsID ? "T" : "F");
            string _result = "";
            if (this.ExecuteNonQuery(_sql, _spc) == 0)
            {
                if (!ClsID)
                    _result = "RCID=COMMON.HINT.CLS_ERROR";	//手动反结案进货单{0}失败，因为该单是被系统自动结案的，无法反结案！
            }
            return _result;
        }
        #endregion

        #region 转进货单回写销货单
        /// <summary>
        /// 转进货单回写销货单
        /// </summary>
        /// <param name="psId">进货单ID</param>
        /// <param name="psNo">进货单号</param>
        /// <param name="osNo">销货单号</param>
        public bool UpdatePoNo(string psId, string psNo, string osNo)
        {
            string _sql = "UPDATE MF_PSS SET POPC_ID = @POPC_ID, PO_NO = @PO_NO WHERE PS_ID = 'SA' AND PS_NO = @PS_NO";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@POPC_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = psId;
            _aryPt[1] = new SqlParameter("@PO_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = psNo;
            _aryPt[2] = new SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _aryPt[2].Value = osNo;
            return this.ExecuteNonQuery(_sql, _aryPt) > 0;
        }
        #endregion

        #region 修改修改进货退回单的已退量
        /// <summary>
        /// 修改修改进货退回单的已退量
        /// </summary>
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="itmColumnName"></param>
        /// <param name="itm"></param>
        /// <param name="qtyColumnName"></param>
        /// <param name="qty"></param>
        public string UpdateQty(string osId, string osNo, string itmColumnName, int itm, string qtyColumnName, decimal qty)
        {
            string _sql = "	update TF_PSS set " + qtyColumnName + "=isNull(" + qtyColumnName + ",0)+@QTY where PS_ID=@PS_ID and PS_NO=@PS_NO and " + itmColumnName + "=@ITM ";
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;
            _sqlPara[1] = new SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;
            _sqlPara[2] = new SqlParameter("@ITM", SqlDbType.Int);
            _sqlPara[2].Value = itm;
            _sqlPara[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _sqlPara[3].Precision = 38;
            _sqlPara[3].Scale = 10;
            _sqlPara[3].Value = qty;
            string _result = "2";
            object _rObj = this.ExecuteScalar(_sql, _sqlPara);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }

        #endregion

        #region 更新进货货单凭证号码
        /// <summary>
        /// 更新进货货单凭证号码
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string psId, string psNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_PSS SET VOH_NO=@VOH_NO WHERE PS_ID=@PS_ID AND PS_NO=@PS_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = psId;

            _sqlPara[1] = new SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = psNo;

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


        #region INVIK & INVLI  回写用

        /// <summary>
        /// 补开发票如来源为CK ,转销货开发票要回MF_PSS ACC_FP_NO
        /// </summary>
        /// <param name="LzNo">补开发票单号</param>
        /// <param name="invNo">发票号</param>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        public void UpdateMFPSSACC_FP_NO(string LzNo, string psId, string psNo)
        {
            string _sql = "update MF_PSS set ACC_FP_NO=@ACC_FP_NO Where PS_ID=@PS_ID and  PS_NO=@PS_NO  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ACC_FP_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = LzNo;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[1].Value = psId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = psNo;

            this.ExecuteNonQuery(_sql, _spc);
        }

        /// <summary>
        /// 补开发票如来源为CK ,转销货开发票要回MF_PSS ACC_FP_NO
        /// </summary>
        /// <param name="LzNo">补开发票单号</param>
        /// <param name="invNo">发票号</param>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        public void UpdateMFPSSACC_FP_NO(string LzNo, string invNo, string psId, string psNo)
        {
            string _sql = "update MF_PSS set ACC_FP_NO=@ACC_FP_NO,INV_NO=@INV_NO Where PS_ID=@PS_ID and  PS_NO=@PS_NO  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ACC_FP_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = LzNo;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[1].Value = invNo;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[2].Value = psId;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[3].Value = psNo;

            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        /// 补开发票如来源为CK ,转销货开发票要回MF_PSS INV_NO
        /// </summary>
        /// <param name="invNo">发票号</param>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        public void UpdateMFPSSINV_NO(string invNo, string psId, string psNo)
        {
            string _sql = "update MF_PSS set INV_NO=@INV_NO Where PS_ID=@PS_ID and  PS_NO=@PS_NO  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[0].Value = invNo;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[1].Value = psId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = psNo;

            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        /// 补收发票回写销货表头档
        /// </summary>
        /// <param name="turnId">会滙开票识别 1.明细 2.滙总</param>
        /// <param name="lzclsId">开票结案注记</param>
        /// <param name="clsLz">回写结案注记</param>
        /// <param name="accFpNo">补开单号</param>
        /// <param name="invNo">发票号码</param>
        /// <param name="amtCls">已开金额</param>
        /// <param name="amtn_netCls">已开未税金额</param>
        /// <param name="taxCls">已开税额</param> 
        /// <param name="qtyCls">已开数量</param> 
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        public void UpdateInvIkHeadData(string turnId, string lzclsId, string clsLz, string accFpNo, string invNo, decimal amtCls, decimal amtn_netCls, decimal taxCls, decimal qtyCls, string psId, string psNo)
        {
            string _sql = "update MF_PSS set TURN_ID=@TURN_ID,LZ_CLS_ID=@LZ_CLS_ID,CLSLZ=@CLSLZ,ACC_FP_NO=@ACC_FP_NO,INV_NO=@INV_NO,AMT_CLS=@AMT_CLS,AMTN_NET_CLS=@AMTN_NET_CLS,TAX_CLS=@TAX_CLS,QTY_CLS=@QTY_CLS " +
                           " Where PS_ID=@PS_ID and PS_NO=@PS_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[11];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TURN_ID", SqlDbType.VarChar, 1);
            _spc[0].Value = turnId;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = lzclsId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@CLSLZ", SqlDbType.VarChar, 1);
            _spc[2].Value = clsLz;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@ACC_FP_NO", SqlDbType.VarChar, 20);
            _spc[3].Value = accFpNo;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[4].Value = invNo;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@AMT_CLS", SqlDbType.Decimal, 0);
            _spc[5].Value = amtCls;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_CLS", SqlDbType.Decimal, 0);
            _spc[6].Value = amtn_netCls;

            _spc[7] = new System.Data.SqlClient.SqlParameter("@TAX_CLS", SqlDbType.Decimal, 0);
            _spc[7].Value = taxCls;

            _spc[8] = new System.Data.SqlClient.SqlParameter("@QTY_CLS", SqlDbType.Decimal, 0);
            _spc[8].Value = qtyCls;

            _spc[9] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[9].Value = psId;
            _spc[10] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[10].Value = psNo;
            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        ///补开发票回写来源单表身栏位
        /// </summary>
        /// <param name="amtFp">已开金额</param>
        /// <param name="amtn_netFp">已开未税金额</param>
        /// <param name="taxFp">已开税额</param>
        /// <param name="qtyFp">已开数量</param>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="itm">表身项次</param>
        public void UpdateInvIkBodyData(decimal amtFp, decimal amtn_netFp, decimal taxFp, decimal qtyFp, string psId, string psNo, int itm)
        {
            string _sql = "update TF_PSS set AMT_FP=@AMT_FP,AMTN_NET_FP=@AMTN_NET_FP,TAX_FP=@TAX_FP,QTY_FP=@QTY_FP Where PS_ID=@PS_ID and  PS_NO=@PS_NO and ITM=@ITM  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[7];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_FP", SqlDbType.Decimal);
            _spc[0].Value = amtFp;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP", SqlDbType.Decimal);
            _spc[1].Value = amtn_netFp;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@TAX_FP", SqlDbType.Decimal);
            _spc[2].Value = taxFp;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@QTY_FP", SqlDbType.Decimal);
            _spc[3].Value = qtyFp;


            _spc[4] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[4].Value = psNo;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@ITM", SqlDbType.Int);
            _spc[5].Value = itm;


            _spc[6] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[6].Value = psId;
            this.ExecuteNonQuery(_sql, _spc);

        }
        #endregion
    }
}
