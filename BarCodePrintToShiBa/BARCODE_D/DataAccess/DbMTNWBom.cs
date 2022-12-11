using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// ά����Ŀ�Ƴ̹滮
    /// </summary>
    public class DbMTNWBom : DbObject
    {
        #region Constructor
        /// <summary>
        /// ά����Ŀ�Ƴ̹滮
        /// </summary>
        /// <param name="connectionString"></param>
        public DbMTNWBom(string connectionString) : base(connectionString)
        {
        }
        #endregion

        #region Get MTN Bom information
        /// <summary>
        /// ȡ��BOM by BOM_NO
        /// </summary>
        /// <param name="bom_No">BOM�䷽</param>
        /// <param name="onlyFillSchema">�Ƿ�ֻ��ȡSchema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bom_No, bool onlyFillSchema)
        {
            string _sqlString = "SELECT MF.BOM_NO,MF.PRD_NO,MF.NAME,MF.PRD_MARK,B.SPC AS PRD_SPC,1 AS QTY,1 AS UNIT,MF.PF_NO,MF.WH_NO,MF.DEP,MF.VALID_DD,MF.END_DD,MF.CST_EXP,MF.CST_PRD,MF.CST_MAN,MF.REM,MF.USR,MF.CHK_MAN,MF.PRT_SW,MF.CLS_DATE,MF.SYS_DATE "
                + " FROM MF_WBOM MF"
                + " LEFT JOIN PRDT B ON B.PRD_NO=MF.PRD_NO"
                + " WHERE MF.BOM_NO=@BOM_NO;"
                + " SELECT TF.BOM_NO,TF.ITM,TF.PRD_NO,B.NAME,TF.PRD_MARK,B.SPC AS PRD_SPC,TF.ID_NO,TF.WH_NO,TF.UNIT,TF.QTY,TF.REM,TF.CST,(CASE WHEN ISNULL(TF.QTY,0) <> 0 THEN ISNULL(TF.CST,0)/ISNULL(TF.QTY,0) ELSE TF.CST END) AS UP_STD FROM TF_WBOM TF"
                + " LEFT JOIN PRDT B ON B.PRD_NO=TF.PRD_NO"
                + " WHERE TF.BOM_NO=@BOM_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = bom_No;
            string[] _aryTableName = new string[] { "MF_WBOM", "TF_WBOM" };
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
                this.FillDatasetSchema(_sqlString, _ds, _aryTableName, _spc);
            else
                this.FillDataset(_sqlString, _ds, _aryTableName, _spc);
            if (_ds.Tables["MF_WBOM"].Columns.Contains("UNIT"))
            {
                _ds.Tables["MF_WBOM"].Columns["UNIT"].ReadOnly = false;
            }
            if (_ds.Tables["MF_WBOM"].Columns.Contains("QTY"))
            {
                _ds.Tables["MF_WBOM"].Columns["QTY"].ReadOnly = false;
            }
            if (_ds.Tables["TF_WBOM"].Columns.Contains("UP_STD"))
            {
                if (_ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly)
                {
                    _ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly = false;
                }
            }
            _ds.Relations.Add("MF_WBOMTF_WBOM", _ds.Tables["MF_WBOM"].Columns["BOM_NO"], _ds.Tables["TF_WBOM"].Columns["BOM_NO"]);
            return _ds;
        }
        /// <summary>
        /// ȡ��BOM by PRD_NO from MF_WBOM
        /// </summary>
        /// <param name="bom_No">BOM�䷽</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromMF(string prdNo)
        {
            string _sqlString = "SELECT BOM_NO,PRD_NO,NAME,PRD_MARK,1 AS QTY,1 AS UNIT,PF_NO,WH_NO,DEP,VALID_DD,END_DD,CST_EXP,CST_PRD,CST_MAN,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,SYS_DATE "
                + " FROM MF_WBOM"
                + " WHERE PRD_NO=@PRD_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _spc[0].Value = prdNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "MF_WBOM" }, _spc);
            return _ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromTF(string prdNo)
        {
            string _sqlString = "SELECT BOM_NO,ITM,PRD_NO,PRD_MARK,ID_NO,WH_NO,UNIT,QTY,REM,CST,(CASE WHEN ISNULL(QTY,0) <> 0 THEN ISNULL(CST,0)/ISNULL(QTY,0) ELSE CST END) AS UP_STD "
                + " FROM TF_WBOM"
                + " WHERE PRD_NO=@PRD_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _spc[0].Value = prdNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "TF_WBOM"}, _spc);
            if (_ds.Tables["TF_WBOM"].Columns.Contains("UP_STD"))
            {
                if (_ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly)
                {
                    _ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly = false;
                }
            }
            return _ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bomNo)
        {
            string _sqlString = "SELECT TF_WBOM.BOM_NO,TF_WBOM.ITM,TF_WBOM.PRD_NO,TF_WBOM.ID_NO,PRDT.NAME AS PRD_NAME,TF_WBOM.QTY,TF_WBOM.CST, "
               + " (CASE WHEN ISNULL(TF_WBOM.QTY,0) <> 0 THEN ISNULL(TF_WBOM.CST,0)/ISNULL(TF_WBOM.QTY,0) ELSE TF_WBOM.CST END) AS UP_STD "
               + " FROM TF_WBOM"
               + " LEFT JOIN PRDT ON PRDT.PRD_NO = TF_WBOM.PRD_NO"
               + " WHERE BOM_NO=@BOM_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = bomNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "TF_WBOM" }, _spc);
            if (_ds.Tables["TF_WBOM"].Columns.Contains("UP_STD"))
            {
                if (_ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly)
                {
                    _ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly = false;
                }
            }
            return _ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataMF(string bomNo)
        {
            string _sqlString = "SELECT BOM_NO,PRD_NO,NAME,PRD_MARK,1 AS QTY,1 AS UNIT,PF_NO,WH_NO,DEP,VALID_DD,END_DD,CST_EXP,CST_PRD,CST_MAN,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,SYS_DATE "
               + " FROM MF_WBOM"
               + " WHERE BOM_NO=@BOM_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = bomNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "MF_WBOM" }, _spc);
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
                + " FROM TF_WBOM"
                + " WHERE ID_NO=@ID_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@ID_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = idNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "TF_WBOM" }, _spc);
            if (_ds.Tables["TF_WBOM"].Columns.Contains("UP_STD"))
            {
                if (_ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly)
                {
                    _ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly = false;
                }
            }
            return _ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBomByPrdt(string prdNo)
        {
            string _sqlString = "SELECT MF.BOM_NO,MF.PRD_NO,MF.NAME,MF.PRD_MARK,B.SPC AS PRD_SPC,MF.QTY,MF.UNIT,MF.PF_NO,MF.WH_NO,MF.DEP,MF.VALID_DD,"
                + " MF.END_DD,MF.CST_PRD,MF.CST_MAN,MF.REM,MF.USR,MF.CHK_MAN,MF.PRT_SW,MF.CLS_DATE,MF.SYS_DATE,MF.QTY,MF.CST "
                + " FROM MF_BOM MF LEFT JOIN PRDT B ON B.PRD_NO=MF.PRD_NO WHERE MF.PRD_NO=@PRD_NO;";
                
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _spc[0].Value = prdNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "MF_BOM" }, _spc);
            if (_ds != null && _ds.Tables["MF_BOM"].Rows.Count > 0)
            {
                StringBuilder _bomNo = new StringBuilder();
                foreach (DataRow _dr in _ds.Tables["MF_BOM"].Rows)
                {
                    if (string.IsNullOrEmpty(_bomNo.ToString()))
                        _bomNo.Append("'" +_dr["BOM_NO"].ToString() + "'");
                    else if (_bomNo.ToString().IndexOf("'" + _dr["BOM_NO"].ToString()+ "'") < 0)
                    {
                        _bomNo.Append(",'" + _dr["BOM_NO"].ToString() + "'");
                    }
                }
                if (!string.IsNullOrEmpty(_bomNo.ToString()))
                {
                    _sqlString = "SELECT TF.BOM_NO,TF.ITM,TF.PRD_NO,TF.PRD_MARK,B.SPC AS PRD_SPC,TF.ID_NO,TF.WH_NO,TF.UNIT,TF.QTY,TF.REM,TF.CST,TF.UP_STD FROM TF_BOM TF LEFT JOIN PRDT B ON B.PRD_NO=TF.PRD_NO "
                        + " WHERE BOM_NO IN (" + _bomNo.ToString() + ")";
                    this.FillDataset(_sqlString, _ds, new string[] { "TF_BOM" });
                }
            }
            return _ds;
        }
        /// <summary>
        /// ���ݻ�Ʒ����ȡ��׼�䷽
        /// </summary>
        /// <param name="prdNo">��Ʒ����</param>
        /// <param name="isTop1">�Ƿ�ֻȡTOP 1</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByPrdNo(string prdNo, bool isTop1)
        {
            string _topSql = "";
            if (isTop1)
                _topSql = " TOP 1";
            string _sqlString = "SELECT " + _topSql + " BOM_NO,PRD_NO,NAME,PRD_MARK,1 AS QTY,1 AS UNIT,PF_NO,WH_NO,DEP,"
                + " VALID_DD,END_DD,CST_EXP,CST_PRD,CST_MAN,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,SYS_DATE"
                + " FROM MF_WBOM"
                + " WHERE PRD_NO=@PRD_NO;"
                + " SELECT A.BOM_NO,A.ITM,A.PRD_NO,A.PRD_MARK,A.ID_NO,A.WH_NO,A.UNIT,A.QTY,A.REM,A.CST,"
                + " (CASE WHEN ISNULL(A.QTY,0) <> 0 THEN ISNULL(A.CST,0)/ISNULL(A.QTY,0) ELSE A.CST END) AS UP_STD "
                + " FROM TF_WBOM A "
                + " WHERE A.BOM_NO IN (SELECT " + _topSql + " BOM_NO FROM MF_WBOM WHERE PRD_NO=@PRD_NO);";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = prdNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "MF_WBOM", "TF_WBOM" }, _sqlPara);
            if (_ds.Tables["TF_WBOM"].Columns.Contains("UP_STD"))
            {
                if (_ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly)
                {
                    _ds.Tables["TF_WBOM"].Columns["UP_STD"].ReadOnly = false;
                }
            }
            return _ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBomByBomNo(string bomNo)
        {
            string _sqlString = "SELECT MF.BOM_NO,MF.PRD_NO,MF.NAME,MF.PRD_MARK,B.SPC AS PRD_SPC,MF.UNIT,MF.PF_NO,MF.WH_NO,MF.DEP,MF.VALID_DD,"
                + " MF.END_DD,MF.CST_PRD,MF.CST_MAN,MF.REM,MF.USR,MF.CHK_MAN,MF.PRT_SW,MF.CLS_DATE,MF.SYS_DATE,MF.QTY "
                + " FROM MF_BOM MF LEFT JOIN PRDT B ON B.PRD_NO=MF.PRD_NO WHERE MF.BOM_NO=@BOM_NO;"
                + " SELECT TF.BOM_NO,TF.ITM,TF.PRD_NO,TF.PRD_MARK,B.SPC AS PRD_SPC,TF.ID_NO,TF.WH_NO,TF.UNIT,TF.QTY,TF.REM,TF.CST,TF.UP_STD "
                + " FROM TF_BOM TF LEFT JOIN PRDT B ON B.PRD_NO=TF.PRD_NO WHERE TF.BOM_NO=@BOM_NO;";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = bomNo;
            string[] _aryTableName = new string[] { "MF_BOM", "TF_BOM" };
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, _aryTableName, _spc);
            _ds.Relations.Add("MF_BOMTF_BOM", _ds.Tables["MF_BOM"].Columns["BOM_NO"], _ds.Tables["TF_BOM"].Columns["BOM_NO"]);
            return _ds;
        }

        /// <summary>
        /// ���ݴ�������SQL��䣬���ط��ϼ�¼��MF_WBOM��DS
        /// </summary>
        /// <param name="sqlWhere">and ��ͷ��sqlwhere���</param>
        /// <param name="sp">����</param>
        /// <returns>ds</returns>
        public SunlikeDataSet GetDataBySqlWhere(string sqlWhere, SqlParameter[] sp)
        {
            string _sqlString = "SELECT BOM_NO,PRD_NO,(SELECT NAME FROM PRDT WHERE PRD_NO=MF_WBOM.PRD_NO)AS PRD_NAME,NAME,PRD_MARK,PF_NO,WH_NO,DEP,VALID_DD,END_DD,CST_EXP,CST_PRD,CST_MAN,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,SYS_DATE "
                  + " FROM MF_WBOM"
                  + " WHERE 1=1 "+sqlWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlString, _ds, new string[] { "MF_WBOM" }, sp);
            return _ds;
        
        }
        /// <summary>
        /// ȥWBOM����
        /// </summary>
        /// <param name="bom_No"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBySqlWhere(string bom_No, string sqlWhere)
        {
            string _sqlString = "SELECT BOM_NO,PRD_NO,NAME,PRD_MARK,1 AS QTY,1 AS UNIT,PF_NO,WH_NO,DEP,VALID_DD,END_DD,CST_EXP,CST_PRD,CST_MAN,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,SYS_DATE "
                + " FROM MF_WBOM"
                + " WHERE BOM_NO=@BOM_NO ;"
                + " SELECT BOM_NO,ITM,PRD_NO,PRD_MARK,ID_NO,WH_NO,UNIT,QTY,REM,CST,(CASE WHEN ISNULL(QTY,0) <> 0 THEN ISNULL(CST,0)/ISNULL(QTY,0) ELSE CST END) AS UP_STD FROM TF_WBOM"
                + " WHERE 1=1 " + sqlWhere;
               
            SqlParameter[] _spc = new SqlParameter[1];
                _spc[0] = new SqlParameter("@BOM_NO", SqlDbType.VarChar, 38);
                _spc[0].Value = bom_No;
            string[] _aryTableName = new string[] { "MF_WBOM", "TF_WBOM" };
            SunlikeDataSet _ds = new SunlikeDataSet();
      
            this.FillDataset(_sqlString, _ds, _aryTableName, _spc);           
            return _ds;
        }
        #endregion

        #region IAuditing
        /// <summary>
        /// �޸Ľ᰸��ǡ�����˺��������
        /// </summary>
        /// <param name="bom_No">BOM����</param>
        /// <param name="chk_Man">�����</param>
        /// <param name="cls_Date">�������</param>
        public void UpdateChkMan(string bom_No, string chk_Man, DateTime cls_Date)
        {
            string _sql = "UPDATE MF_WBOM SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE BOM_NO=@BOM_NO";
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
        #endregion


    }
}