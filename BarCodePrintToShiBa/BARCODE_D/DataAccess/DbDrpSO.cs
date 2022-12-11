/* title: DbDrpSO  受订,受订退回
 * create :
 * doc:
 * 
 * modify: lzj 090623(BUGNO: )  RK_CLS_ID对应，受订退回时候要回写RK_CLS_ID        
 *
 */
using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Text;
namespace Sunlike.Business.Data
{
    /// <summary>
    /// Summary description for DbInvAD.
    /// </summary>
    public class DbDrpSO : DbObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">连接字符串</param>

        public DbDrpSO(string connStr)
            : base(connStr)
        {
        }


        #region 查询受订单
        #region 查询受订单信息
        /// <summary>
        /// 查询受订单信息
        /// 取受订单时，有取销货退回数量以及相关表的字段
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="IsDataSetSchema"></param>
        /// <param name="isContainSR">是否包含销货退回数量和相关表的虚拟字段</param>
        /// <returns></returns>
        public SunlikeDataSet GetDrpSO(string osId, string osNo, bool IsDataSetSchema, bool isContainSR)
        {
            string _sqlStr = "";
            SunlikeDataSet _ds = new SunlikeDataSet();

            string _sqlPlus = "";
            _sqlPlus += ",A.CUS_NO_POS,A.INST_TEAM,A.AMTN_DS";

            string _sqlCaseTf = "";


            string _isSchema = "";
            if (IsDataSetSchema)
            {
                _isSchema = " AND 1<> 1 ";
            }
            SqlParameter[] _sqlPara;
            _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;
            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;
            _sqlStr = " SELECT "
                + " A.OS_ID,A.OS_NO,A.OS_DD,A.BAT_NO,A.CUS_NO,A.QT_NO,A.SAL_NO,A.USE_DEP,A.EST_DD,A.CUR_ID,A.TAX_ID,A.BIL_TYPE,"
                + " A.CNTT_NO,A.REM,A.PAY_MTH,A.PAY_DAYS,A.CHK_DAYS,A.PAY_REM,A.SEND_MTH,A.SEND_WH,A.ADR,A.DIS_CNT,A.AMTN_NET,A.FX_WH,A.YH_NO,"
                + " A.PO_DEP,A.PAY_DD,A.CHK_DD,A.INT_DAYS,A.CLS_REM,A.USR,A.CLS_ID,A.PRT_SW,A.BIL_ID,A.BIL_NO,A.CLS_DATE,A.CHK_MAN,A.EXC_RTO,"
                + " A.BYBOX,A.TOT_BOX,A.TOT_QTY,A.SYS_DATE,A.ISOVERSH,A.HS_ID,A.HIS_PRICE,A.CARD_NO,A.SEND_AREA,A.BACK_ID,A.MOB_ID,A.VOH_ID,"
                + " A.VOH_NO,A.AMTN_INT,A.AMT_INT,A.RP_NO,A.TAX,A.FJ_NUM,A.INV_NO,A.PO_SO_NO,H.AMT_BB,H.AMTN_BB,H.AMT_BC,H.AMTN_BC,H.CACC_NO,A.CUS_OS_NO,"
                + " H.BACC_NO,H.AMT_CHK1 AMT_CHK,H.AMTN_CHK AMTN_CHK,H.AMTN_CLS,I.CHK_NO,I.CHK_KND,I.BANK_NO,I.BACC_NO BACC_NO_CHK,I.CRECARD_NO,I.END_DD,"
                + " A.CONTRACT,A.ISSVS,A.CHK_FX,A.HAS_FX,A.AMTN_YJBX,A.AMTN_BX,A.INV_CHK,A.INV_UNI_NO,A.INV_TITLE,A.INV_AMT,A.AMTN_CBAC,A.CBAC_CLS,A.LOCK_MAN,A.RK_CLS_ID,A.CFM_SW,"
                + " (SELECT TOP 1 NAME FROM CUR_ID WHERE CUR_ID= A.CUR_ID) AS CUR_NAME" + _sqlPlus
                + " FROM MF_POS A "
                + " left join TF_MON H on A.RP_NO=H.RP_NO and H.RP_ID='1'"
                + " left join TF_MON4 N4  on N4.RP_NO=H.RP_NO and N4.RP_ID= H.RP_ID and N4.ITM=1 "
                + " left join MF_CHK I on I.CHK_NO=N4.CHK_NO and I.CHK_ID='0'"
                + " WHERE A.OS_ID=@OS_ID AND A.OS_NO=@OS_NO " + _isSchema
                + " ;"
                + " SELECT "
                + " TF_POS.OS_ID,TF_POS.OS_NO,TF_POS.ITM,TF_POS.QT_NO,TF_POS.PRD_NO,TF_POS.PRD_NAME,TF_POS.PRD_MARK,TF_POS.WH,TF_POS.UNIT"
                + " ,TF_POS.QTY,TF_POS.UP,TF_POS.DIS_CNT,TF_POS.AMT,TF_POS.AMTN,TF_POS.TAX_RTO,TF_POS.TAX,TF_POS.PRE_ITM"
                + " ,TF_POS.QTY_PS,TF_POS.QTY1_PS,TF_POS.EST_DD,TF_POS.CST_STD,TF_POS.QTY_PO,TF_POS.QTY_PRE,TF_POS.QTY_RK,TF_POS.QTY_RK_UNSH,TF_POS.OS_DD,TF_POS.QTY_IC"
                + " ,TF_POS.BOX_ITM,TF_POS.REM,TF_POS.BIL_ID,TF_POS.EST_ITM,TF_POS.VALID_DD,TF_POS.BAT_NO,TF_POS.OTH_NO,TF_POS.OTH_ITM"
                + " ,TF_POS.MTN_REM,TF_POS.CUS_OS_NO,TF_POS.QTY1,TF_POS.UP_QTY1"
                + " ,CONVERT(VARCHAR(1000),PRDT.SPC) AS SPC,PRDT.TPL_NO"
                + " ,TF_POS.MTN_TYPE,TF_POS.WC_NO,TF_POS.MTN_DD,TF_POS.RTN_DD,TF_POS.MTN_ALL_ID,TF_POS.CHK_RTN,"
                + " TF_POS.PAK_UNIT,TF_POS.PAK_EXC,TF_POS.PAK_NW,TF_POS.PAK_WEIGHT_UNIT,TF_POS.PAK_GW,TF_POS.PAK_MEAST,TF_POS.PAK_MEAST_UNIT ";
            if (isContainSR)
            {
                _sqlStr += " ,(SELECT SUM(ISNULL(QTY_RTN,0)) FROM TF_PSS WHERE PS_ID='SA' AND OS_NO=TF_POS.OS_NO AND EST_ITM= TF_POS.EST_ITM) AS QTY_SB";

            }
            _sqlStr += " ,(CASE WHEN TF_POS.UNIT='1' THEN PRDT.UT WHEN TF_POS.UNIT='2' THEN PRDT.PK2_UT"
                + " WHEN TF_POS.UNIT='3' THEN PRDT.PK3_UT ELSE TF_POS.UNIT END) AS UNITNAME,TF_POS.FREE_ID" + _sqlCaseTf
                + " FROM TF_POS "
                + " LEFT JOIN PRDT "
                + " ON TF_POS.PRD_NO= PRDT.PRD_NO "
                + " LEFT JOIN MF_POS "
                + " ON TF_POS.OS_ID=MF_POS.OS_ID AND TF_POS.OS_NO=MF_POS.OS_NO "
                + " WHERE 1= 1 " + _isSchema + " AND TF_POS.OS_ID=@OS_ID AND TF_POS.OS_NO=@OS_NO "
                + " ORDER BY TF_POS.ITM"
                + " ; "
                + "	SELECT "
                + " TF_POS1.OS_ID,TF_POS1.OS_NO,TF_POS1.ITM,TF_POS1.PRD_NO,TF_POS1.WH,TF_POS1.CONTENT,TF_POS1.QTY,TF_POS1.KEY_ITM"
                + " ,TF_POS1.QTY_IC,TF_POS1.EST_DD,TF_POS1.BIL_ID,TF_POS1.EST_ITM,PRDT.NAME AS PRDTNAME "
                + " FROM TF_POS1 "
                + " LEFT JOIN PRDT "
                + " ON TF_POS1.PRD_NO=PRDT.PRD_NO"
                + " LEFT JOIN MF_POS "
                + " ON TF_POS1.OS_ID=MF_POS.OS_ID AND TF_POS1.OS_NO=MF_POS.OS_NO "
                + " WHERE 1=1  " + _isSchema + " AND MF_POS.OS_ID=@OS_ID AND MF_POS.OS_NO=@OS_NO "
                + " ORDER BY TF_POS1.ITM"
                + " ; "
                + "select PAY_B2C.BIL_ID,PAY_B2C.BIL_NO,PAY_B2C.PAY_REM,PAY_B2C.PAY_NO,PAY_B2C.PAY_ID,PAY_B2C.PAY_DD,PAY_B2C.REM from PAY_B2C"
                + " where 1=1 " + _isSchema + " and PAY_B2C.BIL_ID=@OS_ID and PAY_B2C.BIL_NO=@OS_NO";

            try
            {
                if (_sqlPara != null && _sqlPara.Length > 0)
                {
                    if (IsDataSetSchema)
                    {
                        this.FillDatasetSchema(_sqlStr, _ds, new string[4] { "MF_POS", "TF_POS", "TF_POS1", "PAY_B2C" }, _sqlPara);
                    }
                    else
                    {
                        this.FillDataset(_sqlStr, _ds, new string[4] { "MF_POS", "TF_POS", "TF_POS1", "PAY_B2C" }, _sqlPara);
                    }
                }
                else
                {
                    if (IsDataSetSchema)
                    {
                        this.FillDatasetSchema(_sqlStr, _ds, new string[4] { "MF_POS", "TF_POS", "TF_POS1", "PAY_B2C" });
                    }
                    else
                    {
                        this.FillDataset(_sqlStr, _ds, new string[4] { "MF_POS", "TF_POS", "TF_POS1", "PAY_B2C" });
                    }
                }
                DataTable _dtMf = _ds.Tables["MF_POS"];
                DataTable _dtBody = _ds.Tables["TF_POS"];
                DataTable _dtBodyBox = _ds.Tables["TF_POS1"];
                //去除as只读
                if (_dtMf.Columns.Contains("CUR_NAME"))
                {
                    _dtMf.Columns["CUR_NAME"].ReadOnly = false;
                }
                if (_dtBody.Columns.Contains("UNITNAME"))
                {
                    _dtBody.Columns["UNITNAME"].ReadOnly = false;
                }
                if (_dtBody.Columns.Contains("SPC"))
                {
                    _dtBody.Columns["SPC"].ReadOnly = false;
                }
                if (_dtBodyBox.Columns.Contains("PRDTNAME"))
                {
                    _dtBodyBox.Columns["PRDTNAME"].ReadOnly = false;
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            DataColumn[] _pkmf_pos = new DataColumn[2];
            _pkmf_pos[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
            _pkmf_pos[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];

            DataColumn[] _pktf_pos = new DataColumn[3];
            _pktf_pos[0] = _ds.Tables["TF_POS"].Columns["OS_ID"];
            _pktf_pos[1] = _ds.Tables["TF_POS"].Columns["OS_NO"];
            _pktf_pos[2] = _ds.Tables["TF_POS"].Columns["ITM"];

            DataColumn[] _pktf_pos1 = new DataColumn[3];
            _pktf_pos1[0] = _ds.Tables["TF_POS1"].Columns["OS_ID"];
            _pktf_pos1[1] = _ds.Tables["TF_POS1"].Columns["OS_NO"];
            _pktf_pos1[2] = _ds.Tables["TF_POS1"].Columns["ITM"];

            _ds.Tables["MF_POS"].PrimaryKey = _pkmf_pos;
            _ds.Tables["TF_POS"].PrimaryKey = _pktf_pos;
            _ds.Tables["TF_POS1"].PrimaryKey = _pktf_pos1;

            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
            _dca1[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_POS"].Columns["OS_ID"];
            _dca2[1] = _ds.Tables["TF_POS"].Columns["OS_NO"];
            _ds.Relations.Add("MF_POSTF_POS", _dca1, _dca2);
            //表头和表身(箱)关联
            _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
            _dca1[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];
            _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_POS1"].Columns["OS_ID"];
            _dca2[1] = _ds.Tables["TF_POS1"].Columns["OS_NO"];
            _ds.Relations.Add("MF_POSTF_POS1", _dca1, _dca2);
            //表头和B2C在线付款记录关联
            _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
            _dca1[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];
            _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["PAY_B2C"].Columns["BIL_ID"];
            _dca2[1] = _ds.Tables["PAY_B2C"].Columns["BIL_NO"];
            _ds.Relations.Add("MF_POSPAY_B2C", _dca1, _dca2);
            return _ds;
        }

        /// <summary>
        ///  查询银行付款单号信息
        /// </summary>
        /// <param name="payNo">银行付款单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetPayB2C(string payNo)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@PAY_NO", SqlDbType.VarChar, 30);
            _sqlPara[0].Value = payNo;
            string _sqlStr = "select BIL_ID,BIL_NO,PAY_REM,PAY_NO,PAY_ID,PAY_DD,REM from PAY_B2C where PAY_NO=@PAY_NO";
            FillDataset(_sqlStr, _ds, new string[] { "PAY_B2C" }, _sqlPara);
            return _ds;
        }
        #endregion

        #region 取受订单信息
        #region 根据单号及序号取得受订单表身
        /// <summary>
        /// 根据单号及序号取得受订单表身
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <param name="uniqueFeildItm"></param>
        /// <param name="uniqueFeild"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(System.Collections.ArrayList os_id, System.Collections.ArrayList os_no, System.Collections.ArrayList uniqueFeildItm, string uniqueFeild)
        {
            if (os_id.Count != os_no.Count || os_id.Count != uniqueFeildItm.Count || os_no.Count != uniqueFeildItm.Count)
            {
                throw new Sunlike.Common.Utility.SunlikeException("RCID=DBDRPSO.SELECTDRPSOPARAM");//传参数错误
            }
            StringBuilder _os_ids = new StringBuilder();
            StringBuilder _os_nos = new StringBuilder();
            StringBuilder _uniqueFeildItms = new StringBuilder();
            for (int i = 0; i < os_no.Count; i++)
            {
                if (!String.IsNullOrEmpty(_os_ids.ToString()))
                    _os_ids.Append(",");
                _os_ids.Append("'" + os_id[i].ToString() + "'");
                if (!String.IsNullOrEmpty(_os_nos.ToString()))
                    _os_nos.Append(",");
                _os_nos.Append("'" + os_no[i].ToString() + "'");
                if (!String.IsNullOrEmpty(_uniqueFeildItms.ToString()))
                    _uniqueFeildItms.Append(",");
                _uniqueFeildItms.Append("'" + Convert.ToInt32(uniqueFeildItm[i]).ToString() + "'");
            }

            string _sqlStr = " SELECT OS_ID,OS_NO,OS_DD,BAT_NO,CUS_NO,QT_NO,SAL_NO,USE_DEP,EST_DD,CUR_ID,TAX_ID,BIL_TYPE,CUS_OS_NO,CNTT_NO,REM,PAY_MTH,PAY_DAYS,CHK_DAYS,PAY_REM,SEND_MTH,SEND_WH,ADR,DIS_CNT,AMTN_NET,FX_WH,YH_NO,PO_DEP,"
                + " PAY_DD,CHK_DD,INT_DAYS,CLS_REM,USR,CLS_ID,PRT_SW,BIL_ID,BIL_NO,CLS_DATE,CHK_MAN,EXC_RTO,BYBOX,TOT_BOX,TOT_QTY,SYS_DATE,ISOVERSH,HS_ID,HIS_PRICE,LOCK_MAN,"
                + " (SELECT TOP 1 NAME FROM CUR_ID WHERE CUR_ID= MF_POS.CUR_ID) AS CUR_NAME,CARD_NO,SEND_AREA,BACK_ID,ISSVS,CHK_FX,HAS_FX "
                + " FROM MF_POS WHERE OS_ID IN (" + _os_ids + ") AND OS_NO IN (" + _os_nos + ") "
                + " ;"
                + " SELECT "
                + "TF_POS.ITM,TF_POS.OS_ID,TF_POS.OS_NO,TF_POS.PRD_NO,TF_POS.PRD_NAME,TF_POS.PRD_MARK,TF_POS.QT_NO,TF_POS.WH,TF_POS.UNIT,TF_POS.QTY,TF_POS.UP,TF_POS.DIS_CNT,TF_POS.AMT ,TF_POS.AMTN,TF_POS.TAX_RTO,TF_POS.TAX,TF_POS.PRE_ITM,TF_POS.EST_ITM,"
                + " TF_POS.QTY_PS,TF_POS.QTY1_PS,TF_POS.EST_DD,TF_POS.CST_STD,TF_POS.QTY_PO,TF_POS.QTY_PRE,TF_POS.MTN_REM,TF_POS.CUS_OS_NO,TF_POS.QTY_RK,TF_POS.QTY_RK_UNSH,TF_POS.OS_DD,TF_POS.QTY_IC,TF_POS.BOX_ITM,TF_POS.BAT_NO,TF_POS.VALID_DD,TF_POS.OTH_NO,TF_POS.OTH_ITM,"
                + " ISNULL(MF_POS.BACK_ID,'') AS BACK_ID,ISNULL(MF_POS.CLS_ID,'F') AS CLS_ID,TF_POS.MTN_TYPE,TF_POS.WC_NO,TF_POS.MTN_DD,TF_POS.RTN_DD,TF_POS.MTN_ALL_ID,TF_POS.CHK_RTN "
                + " FROM TF_POS "
                + " LEFT JOIN MF_POS ON MF_POS.OS_ID=TF_POS.OS_ID AND MF_POS.OS_NO=TF_POS.OS_NO "
                + " LEFT JOIN PRDT "
                + " ON TF_POS.PRD_NO= PRDT.PRD_NO "
                + " WHERE 1= 1 AND TF_POS.OS_ID IN (" + _os_ids + ") AND TF_POS.OS_NO IN (" + _os_nos + ") AND TF_POS." + uniqueFeild + " IN (" + _uniqueFeildItms + ")"
                + " ORDER BY TF_POS.ITM";
            SunlikeDataSet _ds = new SunlikeDataSet();
            try
            {
                this.FillDataset(_sqlStr, _ds, new string[2] { "MF_POS", "TF_POS" });
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _ds;

        }
        #endregion
        #endregion

        #region SelectDrpSOFromYH
        /// <summary>
        /// SelectDrpSOFromYH
        /// </summary>
        /// <param name="yh_id"></param>
        /// <param name="yh_no"></param>
        /// <returns></returns>
        public DataTable SelectDrpSOFromYH(string yh_id, string yh_no)
        {
            string _where = "";
            _where = " YH_NO=@YH_NO";
            string _sqlStr = "";
            _sqlStr = "SELECT OS_ID,OS_NO,USR FROM MF_POS WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = yh_no;
            SunlikeDataSet _ds = new SunlikeDataSet();
            try
            {
                this.FillDataset(_sqlStr, _ds, new string[] { "MF_POS" }, _sqlPara);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _ds.Tables["MF_POS"];

        }
        #endregion

        #region DeleteDrpSO
        /// <summary>
        /// DeleteDrpSO
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <returns></returns>
        public bool DeleteDrpSO(string os_id, string os_no)
        {
            string _where = "";
            bool _result = false;
            _where = " OS_ID=@OS_ID AND OS_NO=@OS_NO ";
            string _sqlStr = "";
            _sqlStr = " Delete FROM MF_POS WHERE " + _where
                + " Delete FROM TF_POS WHERE " + _where
                + " Delete FROM TF_POS1 WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = os_id;

            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = os_no;

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

        #region SelectDrpMf_Pos
        /// <summary>
        /// SelectDrpMf_Pos
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <returns></returns>
        public DataTable SelectDrpMf_Pos(string os_id, string os_no)
        {
            string _where = "";

            _where = " MF_POS.OS_ID=@OS_ID AND MF_POS.OS_NO=@OS_NO ";
            string _sqlStr = "";
            _sqlStr = "  SELECT MF_POS.OS_ID,MF_POS.OS_NO,MF_POS.OS_DD,DEPT.NAME AS DEP_NAME,MF_POS.CUS_NO,"
                + " CUST.NAME AS CUS_NAME,MF_POS.EST_DD,MF_POS.SAL_NO,MF_POS.PO_DEP,MF_POS.YH_NO,MF_POS.FX_WH,"
                + " MF_POS.USR,MF_POS.CHK_MAN,MF_POS.CLS_ID,MF_POS.CARD_NO,MF_POS.SEND_AREA,MF_POS.BACK_ID,MF_POS.BIL_ID,MF_POS.ISSVS,MF_POS.CHK_FX,MF_POS.HAS_FX "
                + " FROM MF_POS "
                + " LEFT JOIN CUST ON CUST.CUS_NO=MF_POS.CUS_NO "
                + " LEFT JOIN DEPT ON DEPT.DEP = MF_POS.PO_DEP "
                + " WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = os_id;

            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = os_no;

            SunlikeDataSet _ds = new SunlikeDataSet();
            try
            {
                this.FillDataset(_sqlStr, _ds, new string[] { "MF_POS" }, _sqlPara);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _ds.Tables["MF_POS"];
        }
        #endregion

        #region 取得表身数据
        /// <summary>
        /// 取得表身数据(TF_POS)
        /// 查询字段: ITM,OS_ID,OS_NO,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY,UP,DIS_CNT,AMT,AMTN,TAX_RTO,TAX,PRE_ITM,
        /// QTY_PS,EST_DD,CST_STD,QTY_PO,QTY_PRE,QTY_RK,OS_DD,BOX_ITM,REM,BIL_ID,EST_ITM,CUS_OS_NO,QT_NO,OTH_ITM
        /// </summary>
        /// <param name="osId">单据别</param>
        /// <param name="osNo">单号</param>
        /// <param name="itmColumnName">项次</param>
        /// <param name="preItm">项值</param>
        /// <param name="isPrimaryUnit">是否换算成主单位</param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string osId, string osNo, string itmColumnName, int preItm, bool isPrimaryUnit)
        {
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;
            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;
            _sqlPara[2] = new SqlParameter("@ITM", System.Data.SqlDbType.Int);
            _sqlPara[2].Value = preItm;
            string _qtyStr = "";
            string _joinStr = "";
            if (isPrimaryUnit)
            {
                _qtyStr = ",'1' AS UNIT,A.QTY * (CASE WHEN A.UNIT='2' THEN ISNULL(B.PK2_QTY,0) WHEN A.UNIT = '3' THEN ISNULL(B.PK3_QTY,0) ELSE 1 END) AS QTY";
                _joinStr = " LEFT JOIN PRDT B ON B.PRD_NO = A.PRD_NO ";
            }
            else
            {
                _qtyStr = ",A.UNIT,A.QTY";
                _joinStr = "";
            }
            string _sqlStr = "SELECT "
                + " A.ITM,A.OS_ID,A.OS_NO,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WH" + _qtyStr + ",A.UP,A.DIS_CNT,A.AMT,A.AMTN,A.TAX_RTO,A.TAX,A.PRE_ITM,"
                + " A.QTY_IC,A.QTY_PS,A.EST_DD,A.CST_STD,A.QTY_PO,A.QTY_PRE,A.QTY_RK,A.QTY_RK_UNSH,A.OS_DD,A.BOX_ITM,A.REM,A.BIL_ID,A.EST_ITM,A.MTN_REM,A.CUS_OS_NO,A.QT_NO,A.OTH_ITM,A.CHK_RTN "
                + " FROM TF_POS A "
                + _joinStr
                + " WHERE A.OS_ID=@OS_ID AND A.OS_NO=@OS_NO AND A." + itmColumnName + "=@ITM ";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[1] { "TF_POS" }, _sqlPara);
            return _ds;
        }
        #endregion

        #region 查询tf_pos1
        /// <summary>
        /// 查询TF_POS1
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <param name="key_itm"></param>
        /// <returns></returns>
        public DataTable SelectTf_pos1(string os_id, string os_no, string key_itm)
        {
            string _sqlStr = " SELECT TF_POS1.OS_NO,TF_POS1.PRD_NO,TF_POS1.WH,TF_POS1.KEY_ITM,TF_POS1.CONTENT,ISNULL(TF_POS1.QTY,0) AS QTY,TF_POS1.QTY_IC, "
                            + " ISNULL(MF_POS.BACK_ID,'') AS BACK_ID,ISNULL(MF_POS.CLS_ID,'F') AS CLS_ID "
                            + " FROM TF_POS1 "
                            + " JOIN MF_POS ON MF_POS.OS_ID=TF_POS1.OS_ID AND MF_POS.OS_NO=TF_POS1.OS_NO "
                            + " WHERE TF_POS1.OS_ID=@OS_ID AND TF_POS1.OS_NO=@OS_NO AND TF_POS1.KEY_ITM =@KEY_ITM "
                            + " ";
            SunlikeDataSet _tf_pos1Table = new SunlikeDataSet();
            SqlParameter[] _sqlPara1 = new SqlParameter[3];
            _sqlPara1[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara1[0].Value = os_id;
            _sqlPara1[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara1[1].Value = os_no;
            _sqlPara1[2] = new SqlParameter("@KEY_ITM", SqlDbType.VarChar, 50);
            _sqlPara1[2].Value = key_itm;
            this.FillDataset(_sqlStr, _tf_pos1Table, new string[] { "TF_POS1" }, _sqlPara1);
            return _tf_pos1Table.Tables["TF_POS1"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="os_no"></param>
        /// <param name="key_itm"></param>
        /// <param name="qty"></param>
        /// <param name="os_id"></param>
        /// <returns></returns>
        public bool UpdateTf_pos1Qty_ic(string os_id, string os_no, string key_itm, decimal qty)
        {
            bool _result = false;
            string _sqlStr = "";
            SqlParameter[] _sqlPara = new SqlParameter[4];

            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = os_id;

            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = os_no;

            _sqlPara[2] = new SqlParameter("@KEY_ITM", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = key_itm;

            _sqlPara[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _sqlPara[3].Precision = 28;
            _sqlPara[3].Scale = 8;
            _sqlPara[3].Value = qty;

            _sqlStr = "UPDATE TF_POS1 SET QTY_IC = ISNULL(QTY_IC,0)+@QTY WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO AND KEY_ITM=@KEY_ITM AND ISNULL(QTY_IC,0)+@QTY <= QTY";

            try
            {
                int _count = this.ExecuteNonQuery(_sqlStr, _sqlPara);
                if (_count == 0)
                {
                    throw new Exception("配送量大于箱数量");
                }
                else
                {
                    _result = true;
                }

            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _result;
        }
        /// <summary>
        /// 修改TF_POS1已配送数量
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <param name="key_itm"></param>
        /// <param name="qty"></param>
        /// <returns></returns>

        public bool IsUpdateTf_pos1Qty_ic(string os_id, string os_no, string key_itm, decimal qty)
        {
            bool _result = false;
            string _sqlStr = "";
            SqlParameter[] _sqlPara = new SqlParameter[4];

            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = os_id;

            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = os_no;

            _sqlPara[2] = new SqlParameter("@KEY_ITM", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = key_itm;

            _sqlPara[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _sqlPara[3].Precision = 28;
            _sqlPara[3].Scale = 8;
            _sqlPara[3].Value = qty;

            _sqlStr = "SELECT OS_NO,KEY_ITM,QTY_IC FROM TF_POS1 WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO AND KEY_ITM=@KEY_ITM AND @QTY >= ISNULL(QTY_IC,0)";
            SunlikeDataSet _ds = new SunlikeDataSet();
            try
            {
                this.FillDataset(_sqlStr, _ds, new string[] { "TF_POS1" }, _sqlPara);
                if (_ds.Tables["TF_POS1"].Rows.Count > 0)
                {
                    _result = true;
                }
                else
                {
                    throw new Exception("配送量大于箱数量");
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _result;
        }
        #endregion

        #region 取得受订单内容
        /// <summary>
        /// 取得受订单内容
        /// </summary>
        /// <param name="SqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet SelectBodyDrpSO(string SqlWhere)
        {
            string _sql = "select OS_NO,PO_DEP,CUS_NO,SAL_NO,REM,FX_WH,BIL_TYPE,CFM_SW \n"
                + " from MF_POS\n"
                + " where OS_ID='SO' and OS_NO in (" + SqlWhere + ")\n"
                + "select OS_NO,OS_DD,EST_DD,PRD_NO,PRD_NAME,PRD_MARK,UNIT,WH,QTY,QTY AS QTY_OLD,QTY_IC,QTY_PS,QTY_PRE,UP,PRE_ITM,BOX_ITM,VALID_DD,BAT_NO,REM,QTY1,UP_QTY1\n"
                + " from TF_POS\n"
                + " where OS_ID='SO' and OS_NO in (" + SqlWhere + ") and (isnull(QTY,0)-isnull(QTY_IC,0))>0\n"
                + "select OS_NO,PRD_NO,CONTENT,QTY,QTY AS QTY_OLD,QTY_IC,KEY_ITM,WH\n"
                + " from TF_POS1\n"
                + " where OS_ID='SO' and OS_NO in (" + SqlWhere + ") and (isnull(QTY,0)-isnull(QTY_IC,0))>0";
            SunlikeDataSet _ds = new SunlikeDataSet();
            try
            {
                this.FillDataset(_sql, _ds, new string[] { "MF_POS", "TF_POS", "TF_POS1" });
            }
            catch (Exception _ex)
            {
                throw new Exception(_ex.Message);
            }
            return _ds;
        }
        #endregion

        #region 根据要货单号查询受订单
        /// <summary>
        /// 根据要货单号查询受订单
        /// </summary>
        /// <param name="yhNo">要货单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetOsNo4YH(string yhNo)
        {
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 30);
            _aryPt[0].Value = yhNo;
            string _sSql = "SELECT OS_ID,OS_NO,USR,CHK_MAN,CLS_DATE,CLS_ID FROM MF_POS WHERE OS_ID = 'SO' AND YH_NO = @YH_NO";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sSql, _ds, new string[1] { "MF_POS" }, _aryPt);
            if (_ds.Tables["MF_POS"].Rows.Count > 0)
            {
                _aryPt = new SqlParameter[2];
                _aryPt[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
                _aryPt[0].Value = _ds.Tables["MF_POS"].Rows[0]["OS_ID"].ToString();
                _aryPt[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 30);
                _aryPt[1].Value = _ds.Tables["MF_POS"].Rows[0]["OS_NO"].ToString();
                _sSql = "SELECT OS_ID, OS_NO, QTY_IC, QTY_PS,QTY_RK,QTY_RK_UNSH,QTY_PRE FROM TF_POS WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO";
                this.FillDataset(_sSql, _ds, new string[1] { "TF_POS" }, _aryPt);
            }
            return _ds;
        }
        #endregion

        #region 取得原受订单数量(主单位)
        /// <summary>
        ///取得原受订单数量((已转换成主单位数量)
        /// </summary>
        /// <param name="osId">单据类别</param>
        /// <param name="osNo">单号</param>
        /// <param name="uniqueItemField">SO:PRE_ITM SR:EST_ITM</param>
        /// <param name="uniqueItm">uniqueItemField的值</param>		
        /// <returns></returns>
        public decimal GetOrgSoQty(string osId, string osNo, string uniqueItemField, int uniqueItm)
        {
            string _sqlStr = " SELECT A.QTY * (CASE WHEN A.UNIT='2' THEN ISNULL(B.PK2_QTY,0) WHEN A.UNIT = '3' THEN ISNULL(B.PK3_QTY,0) ELSE 1 END) AS QTY "
                            + " FROM TF_POS A "
                            + " LEFT JOIN PRDT B ON A.PRD_NO = B.PRD_NO "
                            + " WHERE A.OS_ID = @OS_ID AND A.OS_NO = @OS_NO AND A." + uniqueItemField + " = @UNIQUE_ITM";
            SqlParameter[] _sqlSpara = new SqlParameter[3];
            _sqlSpara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlSpara[0].Value = osId;
            _sqlSpara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlSpara[1].Value = osNo;
            _sqlSpara[2] = new SqlParameter("@UNIQUE_ITM", SqlDbType.Int);
            _sqlSpara[2].Value = uniqueItm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, null, _sqlSpara);
            if (_ds.Tables[0].Rows.Count > 0
                && !string.IsNullOrEmpty(_ds.Tables[0].Rows[0]["QTY"].ToString()))
            {
                return Convert.ToDecimal(_ds.Tables[0].Rows[0]["QTY"]);
            }
            return 0;
        }

        #endregion

        #endregion

        #region UpdateDrpSO
        /// <summary>
        /// UpdateDrpSO
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="chk_dd"></param>
        /// <returns></returns>
        public bool UpdateDrpSO(string os_id, string os_no, string chk_man, DateTime cls_date)
        {
            string _where = "";
            bool _result = false;
            _where = " OS_ID=@OS_ID AND OS_NO=@OS_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_POS SET CHK_MAN=@CHK_MAN,CLS_DATE=@CHK_DD WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = os_id;

            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = os_no;

            _sqlPara[2] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _sqlPara[2].Value = chk_man;

            _sqlPara[3] = new SqlParameter("@CHK_DD", SqlDbType.DateTime);
            _sqlPara[3].Value = cls_date;
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

        #region 判断是否手工结案
        /// <summary>
        /// 判断是否手工结案
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <returns></returns>
        public bool IsCloseByUsr(string osId, string osNo)
        {
            bool _result = false;
            SunlikeDataSet _ds = new SunlikeDataSet();
            string _sqlStr = "SELECT OS_ID FROM MF_POS WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO AND ISNULL(BACK_ID,'')='' AND ISNULL(CLS_ID,'')='T'";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;
            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;
            this.FillDataset(_sqlStr, _ds, new string[1] { "MF_POS" }, _sqlPara);
            if (_ds != null && _ds.Tables["MF_POS"].Rows.Count > 0)
            {
                _result = true;
            }
            return _result;
        }
        #endregion

        #region 修改已交量
        /// <summary>
        /// 修改受订单量和结案标记
        /// 回写单据数量且回写手工结案标记
        /// </summary>
        /// <param name="backId">手工结案标记</param>
        /// <param name="osNo">原受订单单号</param>
        /// <param name="uniqueItemField">项次</param>
        /// <param name="uniqueItm">项次值</param>
        /// <param name="updateField">原受订单量字段</param>
        /// <param name="updateValue">原受订单量值</param>
        /// <returns></returns>
        public bool UpdateClsId(string backId, string osNo, string uniqueItemField, int uniqueItm, string updateField, decimal updateValue)
        {
            bool _result = false;
            string _sqlStr = "UPDATE TF_POS SET " + updateField + "=ISNULL(" + updateField + ",0)+@QTY WHERE OS_ID='SO' AND OS_NO=@OS_NO AND " + uniqueItemField + "=@UNIQUE_ITM \n"
                + "IF (@@ROWCOUNT > 0)"
                + " BEGIN \n"
                + "IF EXISTS(SELECT OS_ID FROM TF_POS WHERE OS_ID='SO' AND OS_NO=@OS_NO AND (ISNULL(QTY,0)-ISNULL(QTY_IC,0)-ISNULL(QTY_PS,0)-ISNULL(QTY_PRE,0)> 0))\n"// qty_rk 不作为标记
                + "	UPDATE MF_POS SET CLS_ID='F',BACK_ID=NULL WHERE OS_ID='SO' AND OS_NO=@OS_NO AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"
                + "ELSE\n"
                + "	UPDATE MF_POS SET CLS_ID='T',BACK_ID=@BACK_ID WHERE OS_ID='SO' AND OS_NO=@OS_NO AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"
                + " END ";
            //lzj 090623 RK_CLS_ID对应
            _sqlStr += "UPDATE MF_POS SET RK_CLS_ID='T'  WHERE OS_ID='SO' AND OS_NO=@OS_NO AND (SELECT COUNT(OS_ID) FROM TF_POS WHERE OS_ID='SO' AND OS_NO=@OS_NO AND ISNULL(QTY,0)-ISNULL(QTY_PRE,0)-ISNULL(QTY_RK,0)>0)=0;"
                     + "UPDATE MF_POS SET RK_CLS_ID='F'  WHERE OS_ID='SO' AND OS_NO=@OS_NO AND (SELECT COUNT(OS_ID) FROM TF_POS WHERE OS_ID='SO' AND OS_NO=@OS_NO AND ISNULL(QTY,0)-ISNULL(QTY_PRE,0)-ISNULL(QTY_RK,0)>0)>0;";
            SqlParameter[] _sqlSpara = new SqlParameter[4];
            _sqlSpara[0] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlSpara[0].Value = osNo;
            _sqlSpara[1] = new SqlParameter("@UNIQUE_ITM", SqlDbType.Int);
            _sqlSpara[1].Value = uniqueItm;
            _sqlSpara[2] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _sqlSpara[2].Precision = 28;
            _sqlSpara[2].Scale = 8;
            _sqlSpara[2].Value = updateValue;
            _sqlSpara[3] = new SqlParameter("@BACK_ID", SqlDbType.VarChar, 2);
            _sqlSpara[3].Value = backId;

            try
            {
                this.ExecuteNonQuery(_sqlStr, _sqlSpara);
                _result = true;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _result;
        }
        #region 根据受订数量和配送数量判断是否结案：受订数量大于配送数量-不结案，受订数量等于配送数量-结案
        /// <summary>
        /// 根据受订数量和配送数量判断是否结案：受订数量小于配送数量+出库量+销售量+受订退回量=不结案，受订数量>=配送数量+出库量+销售量+受订退回量=结案
        /// 没有回写单据数量也没有回写手工结案标记
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        public void UpdateClsId(string os_id, string os_no)
        {
            string _sqlStr = "";
            _sqlStr = "IF EXISTS(SELECT OS_NO FROM MF_POS WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO)"
                + "	BEGIN "
                + " IF EXISTS(SELECT OS_ID FROM TF_POS WHERE OS_ID='SO' AND OS_NO=@OS_NO AND (ISNULL(QTY,0)-ISNULL(QTY_IC,0)-ISNULL(QTY_PS,0)-ISNULL(QTY_PRE,0) > 0))\n"
                + "			BEGIN"
                + "				UPDATE MF_POS SET CLS_ID='F',BACK_ID=NULL WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO"

                + "			END"
                + "		ELSE"
                + "			BEGIN"
                + "				UPDATE MF_POS SET CLS_ID='T' WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO"
                + "			END"
                + "	END ";
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = os_id;
            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = os_no;
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

        #endregion

        #region RollbackDrpSO
        /// <summary>
        /// RollbackDrpSO
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <returns></returns>
        public bool RollbackDrpSO(string os_id, string os_no)
        {
            string _where = "";
            bool _result = false;
            _where = " OS_ID=@OS_ID AND OS_NO=@OS_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_POS SET CHK_MAN=NULL,CLS_DATE=NULL WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = os_id;

            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = os_no;

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

        #region 修改受订单配送量
        /// <summary>
        /// 修改受订单配送量
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <param name="key_itm"></param>
        /// <param name="qty"></param>
        /// <param name="backId"></param>
        /// <returns></returns>
        public string UpdateBoxQty(string os_id, string os_no, string key_itm, decimal qty, string backId)
        {
            string _sqlStr = "DECLARE @QTYBOX NUMERIC\n"
                + "DECLARE @QTY_IC NUMERIC\n"
                + "SET @QTYBOX=(SELECT ISNULL(QTY,0) FROM TF_POS1 WHERE OS_ID =@OS_ID AND OS_NO=@OS_NO AND KEY_ITM=@KEY_ITM)\n"
                + "SET @QTY_IC=@QTY+ISNULL((SELECT ISNULL(QTY_IC,0) FROM TF_POS1 WHERE OS_ID =@OS_ID AND OS_NO=@OS_NO AND KEY_ITM=@KEY_ITM),0)\n"
                + "IF (@QTYBOX IS NULL)\n"
                + "	SELECT 1\n"
                + "ELSE\n"
                + "BEGIN\n"
                + "		UPDATE TF_POS1 SET QTY_IC=@QTY_IC WHERE OS_ID =@OS_ID AND OS_NO=@OS_NO AND KEY_ITM=@KEY_ITM\n"
                + "		SELECT 0\n"
                + "		IF EXISTS(SELECT OS_ID FROM TF_POS WHERE OS_ID =@OS_ID AND OS_NO=@OS_NO AND (ISNULL(QTY,0)-ISNULL(QTY_IC,0)-ISNULL(QTY_RK,0)-ISNULL(QTY_PS,0)-ISNULL(QTY_PRE,0) > 0))\n"
                + "			UPDATE MF_POS SET CLS_ID='F',BACK_ID=NULL WHERE OS_ID =@OS_ID AND OS_NO=@OS_NO AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"
                + "		ELSE\n"
                + "			UPDATE MF_POS SET CLS_ID='T',BACK_ID=@BACK_ID WHERE OS_ID =@OS_ID AND OS_NO=@OS_NO  AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"
                + "END";
            System.Data.SqlClient.SqlParameter[] _sqlPara = new System.Data.SqlClient.SqlParameter[5];
            _sqlPara[0] = new System.Data.SqlClient.SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = os_id;
            _sqlPara[1] = new System.Data.SqlClient.SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = os_no;
            _sqlPara[2] = new System.Data.SqlClient.SqlParameter("@KEY_ITM", SqlDbType.SmallInt);
            _sqlPara[2].Value = key_itm;
            _sqlPara[3] = new System.Data.SqlClient.SqlParameter("@QTY", SqlDbType.Decimal);
            _sqlPara[3].Precision = 28;
            _sqlPara[3].Scale = 8;
            _sqlPara[3].Value = qty;
            _sqlPara[4] = new SqlParameter("@BACK_ID", SqlDbType.VarChar, 2);
            _sqlPara[4].Value = backId;

            string _result = "1";
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sqlStr, _sqlPara))
            //{
            //    if (_sdr.Read())
            //    {
            //        _result = _sdr[0].ToString();
            //    }
            //}
            object _rObj = this.ExecuteScalar(_sqlStr, _sqlPara);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }

        #endregion

        #region 得到受定单明细
        /// <summary>
        /// 得到单据明细
        /// </summary>
        /// <param name="Bil_Id">单号</param>
        /// <param name="Bil_No">单号</param>
        /// <returns></returns>
        public DataTable GetTfPosForAuditing(string Bil_Id, string Bil_No)
        {
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 8);
            _spc[0].Value = Bil_Id;
            _spc[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 38);
            _spc[1].Value = Bil_No;

            string _sql = "SELECT PRD_NO,ISNULL(PRD_MARK,'') AS PRD_MARK,ISNULL(WH,'') AS WH,ISNULL(QTY,0) AS QTY,BOX_ITM,AMTN,TAX FROM TF_POS WHERE OS_ID=@BIL_ID AND OS_NO=@BIL_NO";

            DataTable _myDt = this.ExecuteDataset(_sql, _spc).Tables[0];
            return _myDt;
        }
        /// <summary>
        /// 得到单据明细
        /// </summary>
        /// <param name="Bil_Id">单号</param>
        /// <param name="Bil_No">单号</param>
        /// <returns></returns>
        public DataTable GetTfPos1ForAuditing(string Bil_Id, string Bil_No)
        {
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 8);
            _spc[0].Value = Bil_Id;
            _spc[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 38);
            _spc[1].Value = Bil_No;

            string _sql = "SELECT PRD_NO,ISNULL(WH,'') AS WH,ISNULL(QTY,0) AS QTY,CONTENT FROM TF_POS1 WHERE OS_ID=@BIL_ID AND OS_NO=@BIL_NO";

            DataTable _myDt = this.ExecuteDataset(_sql, _spc).Tables[0];
            return _myDt;
        }
        #endregion

        #region	取受定单明细资料
        /// <summary>
        ///	取受定单明细资料
        /// </summary>
        /// <param name="Os_Id">单据别</param>
        /// <param name="Os_No">单号</param>
        /// <returns>取受定单明细资料</returns>
        public SunlikeDataSet GetTableOs(string Os_Id, string Os_No)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();

            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 8);
            _spc[0].Value = Os_Id;
            _spc[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 38);
            _spc[1].Value = Os_No;

            string _sql = " SELECT A.OS_NO,A.YH_NO,A.OS_DD,A.EST_DD,A.CUS_NO,ISNULL(C.NAME,A.CUS_NO) AS CUS_NAME,A.BYBOX,A.CARD_NO,A.SEND_AREA,A.SAL_NO,ISNULL(S.NAME,'') AS S_NAME,ISNULL(YH_NO,'') AS YH_NO,"
                        + " A.BIL_ID,A.PO_DEP,ISNULL(D.NAME,'') AS PO_NAME,A.FX_WH,ISNULL(M.NAME,'') AS FX_NAME,ISNULL(A.REM,'') AS REM,"
                        + " A.TAX_ID,A.CUR_ID,(SELECT TOP 1 NAME FROM CUR_ID WHERE CUR_ID= A.CUR_ID) AS CUR_NAME,A.EXC_RTO, "
                        + " QTY=(SELECT SUM(ISNULL(QTY,0.00)) FROM TF_POS B WHERE A.OS_NO=B.OS_NO AND A.OS_ID=B.OS_ID),A.CUS_NO_POS,A.INST_TEAM,A.AMTN_DS,A.CUS_OS_NO,"
                        + " AMTN=(SELECT SUM(ISNULL(AMTN,0.00)+ISNULL(TAX,0.00)) FROM TF_POS B WHERE A.OS_NO=B.OS_NO AND A.OS_ID=B.OS_ID) FROM MF_POS A "
                        + " LEFT JOIN CUST C ON A.CUS_NO=C.CUS_NO "
                        + " LEFT JOIN SALM S ON A.SAL_NO=S.SAL_NO "
                        + " LEFT JOIN DEPT D ON A.PO_DEP=D.DEP "
                        + " LEFT JOIN MY_WH M ON A.FX_WH=M.WH "
                        + " WHERE A.OS_ID=@OS_ID AND A.OS_NO=@OS_NO;"

                        + " SELECT A.OS_ID,A.OS_NO,A.ITM,A.OTH_ITM,A.BIL_ID,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,P.SPC,A.WH,B.NAME AS WH_NAME,CONVERT(CHAR(10),A.EST_DD,120) AS EST_DD,A.QTY,A.QTY1,A.UP_QTY1,A.UP,A.TAX,A.DIS_CNT,A.AMTN,A.REM,A.AMT,"
                        + " 0 AS WH_QTY,A.BOX_ITM,A.VALID_DD,A.BAT_NO,A.QT_NO FROM TF_POS A "
                        + " LEFT JOIN MY_WH B ON A.WH=B.WH "
                        + " LEFT JOIN PRDT P ON A.PRD_NO=P.PRD_NO "
                        + " LEFT JOIN PRDT1 D ON A.WH=D.WH AND A.PRD_NO=D.PRD_NO AND A.PRD_MARK=D.PRD_MARK  "
                        + " WHERE A.OS_ID=@OS_ID AND A.OS_NO=@OS_NO ORDER BY A.PRD_NO;"

                        + " SELECT A.OS_ID,A.OS_NO,A.ITM,A.PRD_NO,B.NAME AS PRD_NAME,A.CONTENT,A.QTY,A.WH,C.NAME AS WH_NAME,CONVERT(CHAR(10),A.EST_DD,120) AS EST_DD,"
                        + "(ISNULL(P.QTY,0.00)-ISNULL(P.QTY_ON_ODR,0.00)) AS WH_QTY,AVG(D.UP) AS UP,A.KEY_ITM FROM TF_POS1 A "
                        + " LEFT JOIN PRDT1_BOX P ON A.PRD_NO=P.PRD_NO AND A.WH=P.WH AND A.CONTENT=P.CONTENT "
                        + " LEFT JOIN PRDT B ON A.PRD_NO=B.PRD_NO "
                        + " LEFT JOIN MY_WH C ON A.WH=C.WH "
                        + " LEFT JOIN TF_POS D ON  D.OS_ID=A.OS_ID AND D.OS_NO=A.OS_NO AND D.BOX_ITM=A.KEY_ITM"
                        + " WHERE A.OS_ID=@OS_ID AND A.OS_NO=@OS_NO"
                        + " GROUP BY A.OS_ID,A.OS_NO,A.ITM,A.PRD_NO,B.NAME,A.CONTENT,A.QTY,A.WH,C.NAME,CONVERT(CHAR(10),A.EST_DD,120),(ISNULL(P.QTY,0.00)-ISNULL(P.QTY_ON_ODR,0.00)),A.KEY_ITM ORDER BY A.PRD_NO";

            this.FillDataset(_sql, _ds, new string[] { "MF_POS", "TF_POS", "TF_POS1" }, _spc);
            DataColumn[] _dc = new DataColumn[2];
            _dc[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
            _dc[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];
            _ds.Tables["MF_POS"].PrimaryKey = _dc;
            _dc = new DataColumn[3];
            _dc[0] = _ds.Tables["TF_POS"].Columns["OS_ID"];
            _dc[1] = _ds.Tables["TF_POS"].Columns["OS_NO"];
            _dc[2] = _ds.Tables["TF_POS"].Columns["ITM"];
            _ds.Tables["TF_POS"].PrimaryKey = _dc;
            _dc = new DataColumn[3];
            _dc[0] = _ds.Tables["TF_POS1"].Columns["OS_ID"];
            _dc[1] = _ds.Tables["TF_POS1"].Columns["OS_NO"];
            _dc[2] = _ds.Tables["TF_POS1"].Columns["ITM"];
            _ds.Tables["TF_POS1"].PrimaryKey = _dc;
            return _ds;
        }
        #endregion

        #region	取受定单明细资料
        /// <summary>
        ///	取受定单明细资料
        /// </summary>
        /// <param name="Os_Id">单据别</param>
        /// <param name="Os_No">单号</param>
        /// <returns>取受定单明细资料</returns>
        public SunlikeDataSet GetTableOsForAudting(string Os_Id, string Os_No)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();

            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 8);
            _spc[0].Value = Os_Id;
            _spc[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 38);
            _spc[1].Value = Os_No;

            string _sql = " SELECT * FROM MF_POS WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO;"
                        + " SELECT * FROM TF_POS WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO;"
                        + " SELECT * FROM TF_POS1 WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO";

            this.FillDataset(_sql, _ds, new string[] { "MF_POS", "TF_POS", "TF_POS1" }, _spc);
            return _ds;
        }
        #endregion

        #region 审核删除受定单明细
        /// <summary>
        /// 审核删除受定单明细
        /// </summary>
        /// <param name="Os_Id"></param>
        /// <param name="Os_No"></param>
        /// <param name="Itm"></param>
        /// <param name="tableName"></param>
        public void DelSo(string tableName, string Os_Id, string Os_No, int Itm)
        {
            SqlParameter[] _pt = new SqlParameter[3];
            _pt[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 8);
            _pt[0].Value = Os_Id;
            _pt[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 38);
            _pt[1].Value = Os_No;
            _pt[2] = new SqlParameter("@ITM", SqlDbType.SmallInt);
            _pt[2].Value = Itm;
            string _sql = "";
            if (tableName == "TF_POS1")
            {
                _sql = "Delete FROM TF_POS WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO AND BOX_ITM IN (SELECT KEY_ITM FROM TF_POS1 WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO AND ITM=@ITM);"
                    + "Delete FROM TF_POS1 WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO AND ITM=@ITM;"
                    + "UPDATE MF_POS SET TOT_QTY=A.QTY FROM (SELECT SUM(QTY) AS QTY FROM TF_POS WHERE TF_POS.OS_NO=@OS_NO AND TF_POS.OS_ID=@OS_ID) AS A WHERE MF_POS.OS_NO=@OS_NO AND MF_POS.OS_ID=@OS_ID;"
                    + "UPDATE MF_POS SET TOT_BOX = A.QTY  FROM (SELECT SUM(QTY) AS QTY FROM TF_POS1 WHERE TF_POS1.OS_NO=@OS_NO AND TF_POS1.OS_ID=@OS_ID) AS A  WHERE MF_POS.OS_NO=@OS_NO AND MF_POS.OS_ID=@OS_ID";
            }
            else
            {
                _sql = "Delete FROM " + tableName + " WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO AND ITM=@ITM";
            }
            this.ExecuteNonQuery(_sql, _pt);
        }
        #endregion

        #region 重整箱数量
        /// <summary>
        /// 重整箱数量
        /// </summary>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        public void ResetBoxQty(string BeginDate, string EndDate)
        {
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@BDate", SqlDbType.VarChar, 10);
            _spc[0].Value = BeginDate;
            _spc[1] = new SqlParameter("@EDate", SqlDbType.VarChar, 10);
            _spc[1].Value = EndDate;
            this.ExecuteSpNonQuery("sp_CalculateBOXQTYSO", _spc);
        }

        /// <summary>
        /// 重整箱数量
        /// </summary>
        /// <param name="OsNo">受订单号</param>
        /// <param name="PrdMark">产品特征</param>
        /// <param name="BoxItm">箱序号</param>
        /// <param name="Qty">数量</param>
        /// <param name="QtyIc">已配送数量</param>
        public void ResetBoxQty(string OsNo, string PrdMark, int BoxItm, decimal Qty, decimal QtyIc)
        {
            SqlParameter[] _spc = new SqlParameter[5];
            _spc[0] = new SqlParameter("@OsNo", SqlDbType.VarChar, 20);
            _spc[0].Value = OsNo;
            _spc[1] = new SqlParameter("@PrdMark", SqlDbType.VarChar, 40);
            _spc[1].Value = PrdMark;
            _spc[2] = new SqlParameter("@BoxItm", SqlDbType.Int);
            _spc[2].Value = BoxItm;
            _spc[3] = new SqlParameter("@Qty", SqlDbType.Decimal);
            _spc[3].Precision = 28;
            _spc[3].Scale = 8;
            _spc[3].Value = Qty;
            _spc[4] = new SqlParameter("@QtyIc", SqlDbType.Decimal);
            _spc[4].Precision = 28;
            _spc[4].Scale = 8;
            _spc[4].Value = QtyIc;
            this.ExecuteSpNonQuery("sp_CalculateBOXQTYSO1", _spc);
        }
        #endregion

        #region 得出受订单据不同配码比在prdt1中的第一笔数据
        /// <summary>
        /// 得出受订单据不同配码比在prdt1中的第一笔数据
        /// </summary>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetFirstPrdt1ByBox(string BeginDate, string EndDate)
        {
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@BDate", SqlDbType.VarChar, 10);
            _spc[0].Value = BeginDate;
            _spc[1] = new SqlParameter("@EDate", SqlDbType.VarChar, 10);
            _spc[1].Value = EndDate;
            string _sqlStr = "select OS_ID,OS_NO,ITM,PRD_NO,PRD_MARK,BOX_ITM,QTY,QTY_IC from fn_box_tf_pos(@BDate,@EDate)";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, null, _spc);
            return _ds.Tables[0];
        }
        #endregion

        #region 更新要货单数量
        /// <summary>
        /// 更新要货单数量
        /// </summary>
        /// <param name="sql">更新语句</param>
        public void SetYhQty(string sql)
        {
            this.ExecuteNonQuery(sql);
        }
        #endregion

        #region 结案
        /// <summary>
        /// 在单据上打上结案标记
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="close"></param>
        public bool DoCloseSO(string osId, string osNo, bool close)
        {
            string _where = "";
            bool _result = false;
            _where = " OS_ID=@OS_ID AND OS_NO=@OS_NO ";
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_POS SET CLS_ID=@CLS_ID  WHERE " + _where;
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;
            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;
            _sqlPara[2] = new SqlParameter("@CLS_ID", SqlDbType.VarChar, 1);
            _sqlPara[2].Value = close.ToString().Substring(0, 1);
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

        #region 修改受订放行状态
        /// <summary>
        /// 修改受订放行状态
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="hasFx"></param>
        /// <returns></returns>
        public bool UpdateHasFx(string osId, string osNo, bool hasFx)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_POS SET HAS_FX=@HAS_FX WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;

            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;

            _sqlPara[2] = new SqlParameter("@HAS_FX", SqlDbType.VarChar, 1);
            _sqlPara[2].Value = hasFx.ToString().Substring(0, 1);
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

        #region 回写受订单客户储值结案标记
        /// <summary>
        /// 回写受订单客户储值结案标记
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="cbacCls"></param>
        /// <returns></returns>
        public bool UpdateCbacCls(string osId, string osNo, bool cbacCls)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_POS SET CBAC_CLS=@CBAC_CLS WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;

            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;

            _sqlPara[2] = new SqlParameter("@CBAC_CLS", SqlDbType.VarChar, 1);
            _sqlPara[2].Value = cbacCls.ToString().Substring(0, 1);
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

        #region 回写采购分析

        /// <summary>
        /// 回写采购分析
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="estItm"></param>
        /// <param name="mpId"></param>
        public void UpdateMpId(string osId, string osNo, string estItm, string mpId)
        {
            string _sql = "UPDATE TF_POS SET CLS_MP_ID = @CLS_MP_ID WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO AND EST_ITM = @EST_ITM";
            SqlParameter[] _aryPt = new SqlParameter[4];
            _aryPt[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = osId;
            _aryPt[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = osNo;
            _aryPt[2] = new SqlParameter("@EST_ITM", SqlDbType.Int);
            _aryPt[2].Value = Convert.ToInt32(estItm);
            _aryPt[3] = new SqlParameter("@CLS_MP_ID", SqlDbType.VarChar, 1);
            _aryPt[3].Value = mpId;
            this.ExecuteNonQuery(_sql, _aryPt);
        }
        #endregion

    }
}
