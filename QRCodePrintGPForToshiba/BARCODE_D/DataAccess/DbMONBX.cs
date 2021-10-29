using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// 报销单
    /// </summary>
    public class DbMONBX : DbObject
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">连接字串</param>
        public DbMONBX(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region 取报销单资料
        /// <summary>
        /// 取报销单资料
        /// </summary>
        /// <param name="bx_no">报销单号</param>
        /// <param name="compNo">帐套代号</param>
        /// <param name="onlyFillSchema">是否取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bx_no, string compNo, bool onlyFillSchema)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append(" SELECT DISTINCT A.BX_NO,A.BX_DD,A.DEP,A.USR_NO,A.PAY_ID,A.PRT_SW,A.CPY_SW,A.CHK_MAN,A.USR,A.CLS_DATE,")
                .Append(" A.REM,A.CUR_ID,A.EXC_RTO,A.CLS_ID,A.MOB_ID,A.LOCK_MAN,A.SYS_DATE,A.PRT_SW,A.CPY_SW,A.PRT_USR,B.NAME DEP_NAME,C.NAME SAL_NAME,")
                .Append(" D.NAME CHK_NAME,E.NAME USR_NAME,F.NAME LOCK_NAME,G.NAME+'['+ Convert(varchar(20),A.EXC_RTO) +']' CUR_NAME,A.OS_ID,A.OS_NO")
                .Append(" FROM MF_BX A")
                .Append(" LEFT JOIN DEPT B ON A.DEP=B.DEP")
                .Append(" LEFT JOIN SALM C ON A.USR_NO=C.SAL_NO")
                .Append(" LEFT JOIN SUNSYSTEM..PSWD D ON A.CHK_MAN=D.USR AND D.COMPNO=@COMPNO")
                .Append(" LEFT JOIN SUNSYSTEM..PSWD E ON A.USR=E.USR AND E.COMPNO=@COMPNO")
                .Append(" LEFT JOIN SUNSYSTEM..PSWD F ON A.LOCK_MAN=F.USR AND F.COMPNO=@COMPNO")
                .Append(" LEFT JOIN (SELECT CUR_ID,NAME FROM CUR_ID WHERE (SELECT TOP 1 B.IJ_DD FROM CUR_ID B WHERE B.CUR_ID=CUR_ID.CUR_ID)=CUR_ID.IJ_DD) G ON A.CUR_ID=G.CUR_ID")
                .Append(" WHERE A.BX_NO=@BX_NO;")
                .Append(" SELECT A.BX_NO,A.ITM,A.BX_DD,A.ACC_NO,A.AMT,A.AMTN,A.AMTN_SH,A.REM,A.BX_ITM,A.BX_AMT,A.AMT_SH,A.SAL_NO,A.FEE_ID,A.SDAY,A.EDAY,A.DEP,")
                .Append(" A.DAYS,A.FORM_CNT,A.CAS_NO,A.TASK_ID,A.OS_ID,A.OS_NO,B.NAME ACC_NAME,C.NAME SAL_NAME,D.NAME FEE_NAME,G.NAME AS DEP_NAME,E.NAME CAS_NAME,F.NAME TASK_NAME,")
                .Append(" (SELECT COUNT(TF_BAC.BB_NO) FROM TF_BAC WHERE TF_BAC.BX_NO=A.BX_NO) AS BAC_EXIST")
                .Append(" FROM TF_BX A")
                .Append(" LEFT JOIN ACCN B ON A.ACC_NO=B.ACC_NO")
                .Append(" LEFT JOIN SALM C ON A.SAL_NO=C.SAL_NO")
                .Append(" LEFT JOIN EXPENSE D ON A.FEE_ID=D.FEE_ID")
                .Append(" LEFT JOIN DEPT G ON G.DEP=A.DEP")
                .Append(" LEFT JOIN CASN E ON E.CAS_NO=A.CAS_NO")
                .Append(" LEFT JOIN TASK F ON F.CAS_NO=A.CAS_NO AND F.TASK_ID=A.TASK_ID")
                .Append(" WHERE A.BX_NO=@BX_NO");
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@BX_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = bx_no;
            _spc[1] = new SqlParameter("@COMPNO", SqlDbType.VarChar, 20);
            _spc[1].Value = compNo;
            if (!onlyFillSchema)
                this.FillDataset(_sql.ToString(), _ds, new string[] { "MF_BX", "TF_BX" }, _spc);
            else
                this.FillDatasetSchema(_sql.ToString(), _ds, new string[] { "MF_BX", "TF_BX" }, _spc);
            DataColumn[] _dcPk = null;
            _dcPk = new DataColumn[1];
            _dcPk[0] = _ds.Tables["MF_BX"].Columns["BX_NO"];
            _ds.Tables["MF_BX"].PrimaryKey = _dcPk;
            _dcPk = new DataColumn[2];
            _dcPk[0] = _ds.Tables["TF_BX"].Columns["BX_NO"];
            _dcPk[1] = _ds.Tables["TF_BX"].Columns["ITM"];
            _ds.Tables["TF_BX"].PrimaryKey = _dcPk;

            DataColumn[] _parent = new DataColumn[1];
            _parent[0] = _ds.Tables["MF_BX"].Columns["BX_NO"];

            DataColumn[] _children = new DataColumn[1];
            _children[0] = _ds.Tables["TF_BX"].Columns["BX_NO"];
            _ds.Relations.Add("MF_BXTF_BX", _parent, _children);

            _ds.Tables["TF_BX"].Columns["BX_ITM"].AutoIncrement = true;
            _ds.Tables["TF_BX"].Columns["BX_ITM"].AutoIncrementSeed = _ds.Tables["TF_BX"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_BX"].Select("", "BX_ITM desc")[0]["BX_ITM"]) + 1 : 1;
            _ds.Tables["TF_BX"].Columns["BX_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_BX"].Columns["BX_ITM"].Unique = true;
            return _ds;
        }
        /// <summary>
        /// 取报销单资料
        /// </summary>
        ///<param name="bxNo">报销单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBX(string bxNo)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append(" SELECT A.BX_NO,A.BX_DD,A.DEP,A.USR_NO,A.PAY_ID,A.CUR_ID,A.EXC_RTO,A.REM,A.CLS_ID,A.USR,A.SYS_DATE,A.CHK_MAN,A.CLS_DATE,A.MOB_ID,A.LOCK_MAN,A.PRT_SW,A.CPY_SW,A.PRT_USR,A.OS_ID,A.OS_NO")
                .Append(" FROM MF_BX A ")
                .Append(" WHERE A.BX_NO=@BX_NO;")
                .Append(" SELECT A.BX_NO,A.ITM,A.BX_DD,A.ACC_NO,A.AMT,A.AMTN,A.AMTN_SH,A.BX_ITM,A.BX_AMT,A.AMT_SH,A.SAL_NO,A.FEE_ID,A.SDAY,A.EDAY,A.REM,A.DAYS,A.FORM_CNT,A.CAS_NO,A.TASK_ID,A.DEP,A.OS_ID,A.OS_NO,")
                .Append(" (SELECT COUNT(TF_BAC.BB_NO) FROM TF_BAC WHERE TF_BAC.BX_NO=A.BX_NO) AS BAC_EXIST")
                .Append(" FROM TF_BX A ")
                .Append(" WHERE A.BX_NO=@BX_NO");
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@BX_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = bxNo;
            string[] _aryTable = new string[] { "MF_BX", "TF_BX" };
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql.ToString(), _ds, _aryTable, _spc);
            return _ds;
        }

        /// <summary>
        ///获取待冲销的报销单
        /// </summary>
        /// <param name="bxNo">报销单号</param>
        /// <returns></returns>
        public DataTable GetMfData(string bxNo)
        {
            string sql = @"SELECT BX_DD,BX_NO,USR_NO,REM,AMTN_SH=(SELECT SUM(ISNULL(AMTN_SH,0)) FROM TF_BX WHERE BX_NO=MF.BX_NO),AMT_SH=(SELECT SUM(ISNULL(AMT_SH,0))  FROM TF_BX WHERE BX_NO=MF.BX_NO)
                           FROM MF_BX MF WHERE BX_NO=@BX_NO";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@BX_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = bxNo;
            string[] _aryTable = new string[] { "MF_BX" };
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(sql.ToString(), _ds, _aryTable, _spc);
            return _ds.Tables[0];
        }
        #endregion

        #region 修改审核人和审核日期
        /// <summary>
        /// 修改审核人和审核日期
        /// </summary>
        /// <param name="bx_No">单号</param>
        /// <param name="chk_Man">审核人</param>
        /// <param name="cls_Date">审核日期</param>
        public void UpdateChkMan(string bx_No, string chk_Man, DateTime cls_Date)
        {
            string _sql = "UPDATE MF_BX SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE BX_NO=@BX_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _spc[1] = new System.Data.SqlClient.SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _spc[2] = new System.Data.SqlClient.SqlParameter("@BX_NO", SqlDbType.VarChar, 38);
            _spc[2].Value = bx_No;
            if (chk_Man.Length == 0)
            {
                _spc[0].Value = System.DBNull.Value;
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = chk_Man;
                _spc[1].Value = cls_Date.ToString("yyyy-MM-dd");
            }
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion

        #region 结案
        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="bx_No">单号</param>
        /// <param name="isClose">结案否</param>
        public void CloseBill(string bx_No, bool isClose)
        {
            SqlParameter[] _params = new SqlParameter[1];
            _params[0] = new SqlParameter("@BX_NO", SqlDbType.VarChar, 20);
            _params[0].Value = bx_No;
            string _sqlStr = "UPDATE MF_BX SET CLS_ID = " + (isClose ? "'T'" : "NULL") + " WHERE BX_NO=@BX_NO";
            this.ExecuteNonQuery(_sqlStr, _params);
        }
        #endregion

        #region 取支付方式
        /// <summary>
        /// 取支付方式
        /// </summary>
        /// <returns></returns>
        public SunlikeDataSet GetPayID()
        {
            string _sql = "SELECT DISTINCT PAY_ID FROM MF_BX";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "MF_BX" });
            return _ds;
        }
        #endregion

        #region 取费用类别
        /// <summary>
        /// 取费用类别
        /// </summary>
        /// <param name="fee_id">费用代号</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFee(string fee_id)
        {
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@FEE_ID", SqlDbType.VarChar, 10);
            _spc[0].Value = fee_id;

            SunlikeDataSet _ds = new SunlikeDataSet();
            string _sql = "select A.FEE_ID,A.NAME,A.ENG_NAME,A.ACC_NO,A.CTRL_ID,A.AMTN,A.OVER_LIM,B.NAME ACC_NAME "
                + "from EXPENSE A left join ACCN B on A.ACC_NO=B.ACC_NO where A.FEE_ID=@FEE_ID ";
            this.FillDataset(_sql, _ds, new string[] { "EXPENSE" }, _spc);
            return _ds;
        }
        #endregion

        #region 得到费用会计科目
        /// <summary>
        /// 得到费用会计科目
        /// </summary>
        /// <param name="feeId">费用代号</param>
        /// <returns></returns>
        public string GetAccNo(string feeId)
        {
            string _accNo = "";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@feeId", SqlDbType.VarChar, 20);
            _spc[0].Value = feeId;
            SunlikeDataSet _ds = new SunlikeDataSet();
            string _sql = "SELECT ACC_NO FROM EXPENSE WHERE FEE_ID=@feeId";
            this.FillDataset(_sql, _ds, new string[] { "TF_BX" }, _spc);
            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                _accNo = _ds.Tables[0].Rows[0][0].ToString();
            }
            return _accNo;
        }
        #endregion

        #region 取来源单报销费用资料
        /// <summary>
        /// 取来源单报销费用资料
        /// </summary>
        /// <param name="osId">单据别</param>
        /// <param name="osNo">单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetOsAmtBxMsg(string osId, string osNo)
        {
            string _sql = "";
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (osId == "SO")
                _sql = "select OS_ID,OS_NO,OS_DD,CUS_NO,SAL_NO,PO_DEP AS DEP,BIL_TYPE,CUR_ID,EXC_RTO,AMTN_YJBX,AMTN_BX,REM from MF_POS where OS_ID=@OS_ID and OS_NO=@OS_NO ";
            if (osId == "OI" || osId == "OT")
                _sql = "select OT_ID AS OS_ID,OT_NO AS OS_NO,OT_DD AS OS_DD,CUS_NO,SAL_NO,DEP,BIL_TYPE,CUR_ID,EXC_RTO,AMTN_YJBX,AMTN_BX,REM from MF_MOUT where OT_ID=@OS_ID and OT_NO=@OS_NO ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@OS_ID", SqlDbType.Char, 2);
            _spc[0].Value = osId;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _spc[1].Value = osNo;
            this.FillDataset(_sql, _ds, new string[] { "BX_OS" }, _spc);
            return _ds;
        }
        #endregion

        #region 取报销单表身信息（根据来源单）
        /// <summary>
        /// 取报销单表身信息（根据来源单）
        /// </summary>
        /// <param name="osId">来源单单据别</param>
        /// <param name="osNo">来源单单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetBxBody(string osId, string osNo)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            string _sql = "select BX_NO,ITM,OS_ID,OS_NO,CAS_NO from TF_BX where OS_ID=@OS_ID and OS_NO=@OS_NO ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@OS_ID", SqlDbType.Char, 2);
            _spc[0].Value = osId;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _spc[1].Value = osNo;
            this.FillDataset(_sql, _ds, new string[] { "TF_BX" }, _spc);
            return _ds;
        }
        #endregion

        #region 修改报销金额
        /// <summary>
        /// 修改报销金额
        /// </summary>
        /// <param name="osID">单据代号</param>
        /// <param name="osNO">单据号码</param>
        /// <param name="amtBx">报销金额</param>
        public void UpdateAmtBx(string osID, string osNO, decimal amtBx)
        {
            string _sql = "";
            if (osID == "SO")
                _sql = "update MF_POS set AMTN_BX=isNull(AMTN_BX,0)+@AMT_BX where OS_ID=@OS_ID and OS_NO=@OS_NO \n";
            if (osID == "OI" || osID == "OT")
                _sql = "update MF_MOUT set AMTN_BX=isNull(AMTN_BX,0)+@AMT_BX where OT_ID=@OS_ID and OT_NO=@OS_NO \n";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_BX", SqlDbType.Decimal);
            _spc[0].Precision = 38;
            _spc[0].Scale = 10;
            _spc[0].Value = amtBx;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@OS_ID", SqlDbType.Char, 2);
            _spc[1].Value = osID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = osNO;
            if (_sql != "")
                this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion
    }
}
