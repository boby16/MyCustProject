using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// DbMRPBom 的摘要说明。
    /// </summary>
    public class DbMRPBom : Sunlike.Business.Data.DbObject
    {
        /// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
        public DbMRPBom(string connStr)
            : base(connStr)
		{
		}
        /// <summary>
        /// 取得BOM
        /// </summary>
        /// <param name="bom_No">BOM配方</param>
        /// <param name="onlyFillSchema">是否只读取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bomNo, bool onlyFillSchema)
        {
            string _sqlString = "SELECT BOM_NO,NAME,PRD_NO,PRD_MARK,PF_NO,WH_NO,PRD_KND,UNIT,"
                + " QTY,QTY1,CST_MAKE,CST_PRD,CST_MAN,CST_OUT,USED_TIME,CST,USR_NO,TREE_STRU,"
                + " DEP,PHOTO_BOM,EC_NO,VALID_DD,END_DD,REM,USR,CHK_MAN,PRT_SW,CPY_SW,CLS_DATE,"
                + " MOB_ID,LOCK_MAN,SEB_NO,MOD_NO,TIME_CNT,PRT_USR,DEP_INC,SPC,SYS_DATE,BZ_KND "
                + " FROM MF_BOM"
                + " WHERE BOM_NO=@BOM_NO;"
                + " SELECT BOM_NO,ITM,PRD_NO,PRD_MARK,ID_NO,NAME,WH_NO,BOM_ID,UNIT,QTY,QTY1,"
                + " LOS_RTO,CST,PRD_NO_UP,ID_NO_UP,EXP_ID,PRD_NO_CHG,REM,START_DD,END_DD,ZC_NO,"
                + " TW_ID,USEIN_NO,QTY_BAS,PZ_ID,COMPOSE_IDNO,UP_STD,UP_TAX,CUS_NO,RTO_TAX,EXC_RTO,"
                + " CUR_ID,IS_SO_RES,CHG_RTO,PRE_ITM,BL_RTO,CUS_NO2,PRD_NO_OLD,CL_RTO,RTO_PEI,PEI_RTO "
                + " FROM TF_BOM "
                + " WHERE BOM_NO=@BOM_NO;";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
            _sqlPara[0].Value = bomNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
                this.FillDatasetSchema(_sqlString, _ds, new string[] { "MF_BOM", "TF_BOM" }, _sqlPara);
            else
                this.FillDataset(_sqlString, _ds, new string[] { "MF_BOM", "TF_BOM" }, _sqlPara);
            _ds.Relations.Add("MF_BOMTF_BOM", _ds.Tables["MF_BOM"].Columns["BOM_NO"], _ds.Tables["TF_BOM"].Columns["BOM_NO"]);
            return _ds;
        }
        /// <summary>
        /// 根据货品代号取标准配方
        /// </summary>
        /// <param name="prdNo">货品代号</param>
        /// <param name="isTop1">是否只取TOP 1</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByPrdNo(string prdNo,bool isTop1)
        {
            string _topSql = "";
            if (isTop1)
                _topSql = " TOP 1";
            string _sqlString = "SELECT " + _topSql + " BOM_NO,NAME,PRD_NO,PRD_MARK,PF_NO,WH_NO,PRD_KND,UNIT,"
                + " QTY,QTY1,CST_MAKE,CST_PRD,CST_MAN,CST_OUT,USED_TIME,CST,USR_NO,TREE_STRU,"
                + " DEP,PHOTO_BOM,EC_NO,VALID_DD,END_DD,REM,USR,CHK_MAN,PRT_SW,CPY_SW,CLS_DATE,"
                + " MOB_ID,LOCK_MAN,SEB_NO,MOD_NO,TIME_CNT,PRT_USR,DEP_INC,SPC,SYS_DATE,BZ_KND "
                + " FROM MF_BOM"
                + " WHERE PRD_NO=@PRD_NO;"
                + " SELECT A.BOM_NO,A.ITM,A.PRD_NO,A.PRD_MARK,A.ID_NO,A.NAME,A.WH_NO,A.BOM_ID,A.UNIT,A.QTY,A.QTY1,"
                + " A.LOS_RTO,A.CST,A.PRD_NO_UP,A.ID_NO_UP,A.EXP_ID,A.PRD_NO_CHG,A.REM,A.START_DD,A.END_DD,A.ZC_NO,"
                + " A.TW_ID,A.USEIN_NO,A.QTY_BAS,A.PZ_ID,A.COMPOSE_IDNO,A.UP_STD,A.UP_TAX,A.CUS_NO,A.RTO_TAX,A.EXC_RTO,"
                + " A.CUR_ID,A.IS_SO_RES,A.CHG_RTO,A.PRE_ITM,A.BL_RTO,A.CUS_NO2,A.PRD_NO_OLD,A.CL_RTO,A.RTO_PEI,A.PEI_RTO "
                + " FROM TF_BOM A "
                + " WHERE A.BOM_NO IN (SELECT " + _topSql + " BOM_NO FROM MF_BOM WHERE PRD_NO=@PRD_NO);";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = prdNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "MF_BOM", "TF_BOM" }, _sqlPara);
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bom_No"></param>
        /// <param name="chk_Man"></param>
        /// <param name="cls_Date"></param>
        public void UpdateChkMan(string bom_No, string chk_Man, DateTime cls_Date)
        {
            string _sql = "UPDATE MF_BOM SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE BOM_NO=@BOM_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _spc[1] = new System.Data.SqlClient.SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _spc[2] = new System.Data.SqlClient.SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
            _spc[2].Value = bom_No;
            if (chk_Man.Length == 0)
            {
                _spc[0].Value = System.DBNull.Value;
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = chk_Man;
                _spc[1].Value = cls_Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.ExecuteNonQuery(_sql, _spc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string bomNo)
        {
            string _sqlString = "SELECT TF_BOM.BOM_NO,TF_BOM.ITM,TF_BOM.PRD_NO,TF_BOM.ID_NO,PRDT.NAME AS PRD_NAME,TF_BOM.QTY,TF_BOM.CST, "
               + " (CASE WHEN ISNULL(TF_BOM.QTY,0) <> 0 THEN ISNULL(TF_BOM.CST,0)/ISNULL(TF_BOM.QTY,0) ELSE TF_BOM.CST END) AS UP_STD "
               + " FROM TF_BOM"
               + " LEFT JOIN PRDT ON PRDT.PRD_NO = TF_BOM.PRD_NO"
               + " WHERE BOM_NO=@BOM_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = bomNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "TF_BOM" }, _spc);
            if (_ds.Tables["TF_BOM"].Columns.Contains("UP_STD"))
            {
                if (_ds.Tables["TF_BOM"].Columns["UP_STD"].ReadOnly)
                {
                    _ds.Tables["TF_BOM"].Columns["UP_STD"].ReadOnly = false;
                }
            }
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByIDNO(string idNo)
        {
            string _sqlString = "SELECT BOM_NO,ITM,PRD_NO,PRD_MARK,ID_NO,WH_NO,UNIT,QTY,REM,CST,(CASE WHEN ISNULL(QTY,0) <> 0 THEN ISNULL(CST,0)/ISNULL(QTY,0) ELSE CST END) AS UP_STD "
                + " FROM TF_BOM"
                + " WHERE ID_NO=@ID_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@ID_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = idNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "TF_BOM" }, _spc);
            if (_ds.Tables["TF_BOM"].Columns.Contains("UP_STD"))
            {
                if (_ds.Tables["TF_BOM"].Columns["UP_STD"].ReadOnly)
                {
                    _ds.Tables["TF_BOM"].Columns["UP_STD"].ReadOnly = false;
                }
            }
            return _ds;
        }

        /// <summary>
        /// 取得BOM by PRD_NO from MF_WBOM
        /// </summary>
        /// <param name="bom_No">BOM配方</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromMF(string prdNo)
        {
            string _sqlString = "SELECT BOM_NO,PRD_NO,NAME,PRD_MARK,1 AS QTY,1 AS UNIT,PF_NO,WH_NO,DEP,VALID_DD,END_DD,CST_PRD,CST_MAN,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,SYS_DATE "
                + " FROM MF_BOM"
                + " WHERE PRD_NO=@PRD_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _spc[0].Value = prdNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "MF_BOM" }, _spc);
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromTF(string prdNo)
        {
            string _sqlString = "SELECT TF_BOM.BOM_NO,TF_BOM.ITM,TF_BOM.PRD_NO,TF_BOM.PRD_MARK,TF_BOM.ID_NO,TF_BOM.WH_NO,TF_BOM.UNIT,TF_BOM.QTY,TF_BOM.REM,TF_BOM.CST,"
                + " (CASE WHEN ISNULL(TF_BOM.QTY,0) <> 0 THEN ISNULL(TF_BOM.CST,0)/ISNULL(TF_BOM.QTY,0) ELSE CST END) AS UP_STD,PRDT.KND "
                + " FROM TF_BOM INNER JOIN PRDT ON TF_BOM.PRD_NO=PRDT.PRD_NO"
                + " WHERE TF_BOM.PRD_NO=@PRD_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _spc[0].Value = prdNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "TF_BOM" }, _spc);
            if (_ds.Tables["TF_BOM"].Columns.Contains("UP_STD"))
            {
                if (_ds.Tables["TF_BOM"].Columns["UP_STD"].ReadOnly)
                {
                    _ds.Tables["TF_BOM"].Columns["UP_STD"].ReadOnly = false;
                }
            }
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataMFByCondition(string sqlWhere)
        {
            string _sqlString = "SELECT BOM_NO,PRD_NO,NAME,PRD_MARK,1 AS QTY,1 AS UNIT,PF_NO,WH_NO,DEP,VALID_DD,END_DD,CST_PRD,CST_MAN,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,SYS_DATE "
                + " FROM MF_BOM";
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                _sqlString += " WHERE " + sqlWhere;
            }
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "MF_BOM" });
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForDetail(string bomNo)
        {
            string _sql = "SELECT B.*,C.KND,C.NAME,ISNULL(A.QTY,0) AS UP_QTY,ISNULL(A.QTY1,0) AS UP_QTY1,A.PRD_NO AS UP_NO,A.UNIT AS UP_UNIT"
                + " FROM MF_BOM A INNER JOIN TF_BOM B ON A.BOM_NO=B.BOM_NO "
                + " INNER JOIN PRDT C ON B.PRD_NO=C.PRD_NO "
                + " WHERE A.BOM_NO=@BOM_NO";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = bomNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "BOM" }, _spc);
            return _ds;
        }

        #region 取BOM相关数据
        public string GetBomNoByPrdt(string prdNo, string bomTb, bool isMaxVer)
        {
            string _sql = string.Format("SELECT TOP 1 BOM_NO FROM {0} WHERE PRD_NO=@PRD_NO", bomTb);
            if (isMaxVer)
                _sql += " ORDER BY PF_NO DESC";
            SqlParameter[] _sp = new SqlParameter[1];
            _sp[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _sp[0].Value = prdNo;
            return Convert.ToString(this.ExecuteScalar(_sql, _sp));
        }

        public decimal GetMFQty(string bomMF, string bomNo)
        {
            string _sql = string.Format(@"SELECT QTY=QTY * (CASE WHEN MF.UNIT = '2' THEN P.PK2_QTY WHEN MF.UNIT = '3' THEN P.PK3_QTY ELSE 1 END) 
                                          FROM MF_BOM MF LEFT JOIN PRDT P ON MF.PRD_NO=P.PRD_NO 
                                          WHERE BOM_NO=@BOM_NO", bomMF);
            SqlParameter[] _sp = new SqlParameter[1];
            _sp[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 28);
            _sp[0].Value = bomNo;
            decimal qty = 1;
            decimal.TryParse(Convert.ToString(this.ExecuteScalar(_sql, _sp)), out qty);
            return qty > 0 ? qty : 1;
        }

        public DataTable GetTFBom(string bomTb)
        {
            string _sql = string.Format("SELECT *,FACTOR=0.0 FROM {0} ", bomTb);
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new String[] { bomTb });
            return _ds.Tables[0];
        }

        public bool IsExistsBom(string bomNo, string bomTb)
        {
            string _sql = string.Format("SELECT COUNT(BOM_NO) FROM {0} WHERE BOM_NO=@BOM_NO ", bomTb);
            SqlParameter[] _sp = new SqlParameter[1];
            _sp[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 28);
            _sp[0].Value = bomNo;
            decimal qty = 0;
            decimal.TryParse(Convert.ToString(this.ExecuteScalar(_sql, _sp)), out qty);
            return qty > 0;
        }

        #endregion
    }
}
