using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Collections.Generic;
namespace Sunlike.Business.Data
{
	/// <summary>
	/// DbPact 的摘要说明。
	/// </summary>
	public class DbPact : Sunlike.Business.Data.DbObject
	{
		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="connStr">连接字符串</param>
		public DbPact(string connStr):base(connStr)
		{
		}
		#endregion

		#region 得到合约信息
		/// <summary>
		/// 得到合约信息
		/// </summary>
		/// <param name="usr">操作用户代号</param>
		/// <param name="pactID">合约代号</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string usr, string pactID)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@PAC_NO", SqlDbType.VarChar, 20);
			_spc[0].Value = pactID;
            string _sql = "Select A.PAC_NO,A.PAC_DD,A.TITLE,A.MASTER_ID,A.CUS_NO,A.STATUS,A.START_DD,A.END_DD,A.AMTN_BG,A.AMTN_FACT,A.SAL_NO,"
                + " A.SRV_NO,A.FILE1,A.FILE_ID1,A.FILE_NAME1,A.FILE2,A.FILE_ID2,A.FILE_NAME2,"
                + " A.REM,A.USR,A.DEP,A.SRV_MTH,B.NAME CUS_NAME,C.NAME SAL_NAME,"
                + " D.NAME SRV_NAME,E.NAME DEP_NAME,A.MA_DD,A.IDX_NO,A.PAC_NOSELF,"
                + " I.NAME IDX_NAME FROM CUS_PACT A "
                + " LEFT JOIN ufn_GetCustList('" + usr + "') B ON B.CUS_NO=A.CUS_NO "
                + " LEFT JOIN SALM C ON C.SAL_NO=A.SAL_NO "
                + " LEFT JOIN MF_YG D ON D.YG_NO=A.SRV_NO "
                + " LEFT JOIN DEPT E ON A.DEP= E.DEP "
                + " LEFT JOIN CUS_PACT_I I ON I.KND_NO=A.IDX_NO "
                + "  WHERE PAC_NO = @PAC_NO "
                //+ " SELECT A.PAC_NO, A.ITM, A.WC_NO, A.REM, "
                //+ " (CASE WHEN ISNULL(A.WC_NO,'')='' THEN A.PRD_NO ELSE B.PRD_NO END) AS PRD_NO, "
                //+ " (CASE WHEN ISNULL(A.WC_NO,'')='' THEN A.PRD_MARK ELSE B.PRD_MARK END) AS PRD_MARK,"
                //+ " (CASE WHEN ISNULL(A.WC_NO,'')='' THEN A.QTY ELSE 1 END) AS QTY "
                //+ " FROM CUS_PACT1 A "
                //+ " LEFT JOIN MF_WC B ON B.WC_NO=A.WC_NO "
                //+ " WHERE A.PAC_NO = @PAC_NO "
                + " SELECT A.PAC_NO, A.ITM, A.WC_NO, A.REM, "
                + " A.PRD_NO, A.PRD_MARK, P.NAME PRD_NAME, A.QTY, "
                + " C.NAME CUS_NAME, C.SNM CUS_SNM "
                + " FROM CUS_PACT1 A "
                + " LEFT JOIN CUS_PACT B ON A.PAC_NO=B.PAC_NO "
                + " LEFT JOIN PRDT P ON A.PRD_NO=P.PRD_NO "
                + " LEFT JOIN MF_WC W ON A.WC_NO=W.WC_NO "
                + " LEFT JOIN CUST C ON W.CUS_NO=C.CUS_NO "
                + " WHERE A.PAC_NO = @PAC_NO "
                + " SELECT A.PAC_NO, A.ITM, A.WC_NO, A.REM,  "
                + " (CASE WHEN ISNULL(A.WC_NO,'')='' THEN B.CUS_NO ELSE B.CUS_NO END) AS CUS_NO,"
                + " (CASE WHEN ISNULL(A.WC_NO,'')='' THEN A.PRD_NO ELSE B.PRD_NO END) AS PRD_NO, "
                + " (CASE WHEN ISNULL(A.WC_NO,'')='' THEN P1.NAME ELSE P.NAME END) AS PRD_NAME,"
                + " (CASE WHEN ISNULL(A.WC_NO,'')='' THEN A.PRD_MARK ELSE B.PRD_MARK END) AS PRD_MARK,"
                + " (CASE WHEN ISNULL(A.WC_NO,'')='' THEN A.QTY ELSE 1 END) AS QTY "
                + " FROM CUS_PACT1 A "
                + " LEFT JOIN MF_WC B ON B.WC_NO=A.WC_NO "
                + " LEFT JOIN CUS_PACT C ON C.PAC_NO=A.PAC_NO "
                + " LEFT JOIN PRDT P ON P.PRD_NO=B.PRD_NO "
                + " LEFT JOIN PRDT P1 ON P1.PRD_NO=A.PRD_NO "
                + " WHERE A.PAC_NO = @PAC_NO "
                + " SELECT PAC_NO,NET_USER,SYS_DATE,SAL_NO,REM_MOD,REM,"
                + " CHK1,CHK2,CHK3,CHK4,CHK5,CHK6,CHK7,CHK8,CHK9,CHK10,"
                + " CHK11,CHK12,CHK13,CHK14,CHK15,CHK16,CHK17,CHK18,CHK19,CHK20,"
                + " CHK21,CHK22,CHK23,CHK24,CHK25,CHK26,CHK27,CHK28,CHK29,CHK30,"
                + " CHK31, CHK32, CHK33, CHK34 ,CHK35 ,CHK36 ,CHK37 ,CHK38 ,CHK39 ,CHK40,"
                + " CHK41, CHK42, CHK43, CHK44 ,CHK45, CHK46, CHK47, CHK48 ,CHK49 ,CHK50 ,"
                + " CHK51, CHK52, CHK53, CHK54 ,CHK55, CHK56, CHK57, CHK58, CHK59 ,CHK60 ,"
                + " CHK61, CHK62, CHK63, CHK64 from CUS_PACT2 "
                + " WHERE PAC_NO = @PAC_NO ";

            //this.FillDataset(_sql, _ds, new string[2] { "CUS_PACT", "CUS_PACT1" }, _spc);
            this.FillDataset(_sql, _ds, new string[4] { "CUS_PACT", "CUS_PACT1", "TMP_CUSPACT1", "CUS_PACT2" }, _spc);
            if (_ds.Tables["CUS_PACT1"].Columns["PRD_NO"].ReadOnly)
            {
                _ds.Tables["CUS_PACT1"].Columns["PRD_NO"].ReadOnly = false;
            }
            if (_ds.Tables["CUS_PACT1"].Columns["PRD_MARK"].ReadOnly)
            {
                _ds.Tables["CUS_PACT1"].Columns["PRD_MARK"].ReadOnly = false;
            }
            if (_ds.Tables["CUS_PACT1"].Columns["QTY"].ReadOnly)
            {
                _ds.Tables["CUS_PACT1"].Columns["QTY"].ReadOnly = false;
            }
            DataColumn[] _dca = new DataColumn[1];
            _dca[0] = _ds.Tables["CUS_PACT"].Columns["PAC_NO"];
            _ds.Tables["CUS_PACT"].PrimaryKey = _dca;

            _dca = new DataColumn[2];
            _dca[0] = _ds.Tables["CUS_PACT1"].Columns["PAC_NO"];
            _dca[1] = _ds.Tables["CUS_PACT1"].Columns["ITM"];
            _ds.Tables["CUS_PACT1"].PrimaryKey = _dca;
            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[1];
            _dca1[0] = _ds.Tables["CUS_PACT"].Columns["PAC_NO"];            
            DataColumn[] _dca2 = new DataColumn[1];
            _dca2[0] = _ds.Tables["CUS_PACT1"].Columns["PAC_NO"];

            _ds.Relations.Add("CUS_PACTCUS_PACT1", _dca1, _dca2);

            DataColumn[] _dca3 = new DataColumn[1];
            _dca3[0] = _ds.Tables["CUS_PACT2"].Columns["PAC_NO"];
            _ds.Tables["CUS_PACT2"].PrimaryKey = _dca3;

            _ds.Relations.Add("CUS_PACTCUS_PACT2", _dca1, _dca3);

			return _ds;
		}
        /// <summary>
        /// 得到合约表头信息
        /// </summary>
        /// <param name="dyCondtion"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(Dictionary<string, object> dyCondtion)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();           
            int _paraCount = 0;
            string _sqlWhere = "";
            //截止日期（起）
            if (dyCondtion.ContainsKey("END_DD_B"))
            {
                _sqlWhere += " AND A.END_DD >=@END_DD_B";
                _paraCount++;
            }
            //截止日期（止）
            if (dyCondtion.ContainsKey("END_DD_E"))
            {
                _sqlWhere += " AND A.END_DD <=@END_DD_E";
                _paraCount++;
            }
            //客户(起)
            if (dyCondtion.ContainsKey("CUS_NO_B"))
            {
                if (!string.IsNullOrEmpty(dyCondtion["CUS_NO_B"].ToString()))
                {
                    _sqlWhere += " AND A.CUS_NO >=@CUS_NO_B";
                }
                _paraCount++;
            }
            //客户(止)
            if (dyCondtion.ContainsKey("CUS_NO_E"))
            {
                if (!string.IsNullOrEmpty(dyCondtion["CUS_NO_E"].ToString()))
                    _sqlWhere += " AND A.CUS_NO <=@CUS_NO_E";
                _paraCount++;
            }
            //合约代号(起)
            if (dyCondtion.ContainsKey("PAC_NO_B"))
            {
                if (!string.IsNullOrEmpty(dyCondtion["PAC_NO_B"].ToString()))
                    _sqlWhere += " AND A.PAC_NO >=@PAC_NO_B";
                _paraCount++;
            }
            //合约代号(止)
            if (dyCondtion.ContainsKey("PAC_NO_E"))
            {
                if (!string.IsNullOrEmpty(dyCondtion["PAC_NO_E"].ToString()))
                    _sqlWhere += " AND A.PAC_NO <=@PAC_NO_E";
                _paraCount++;
            }
            //区域(起)
            if (dyCondtion.ContainsKey("ARE_NO_B"))
            {
                if (!string.IsNullOrEmpty(dyCondtion["ARE_NO_B"].ToString()))
                    _sqlWhere += " AND C.AREA_NO >=@ARE_NO_B";
                _paraCount++;
            }
            //区域(止)
            if (dyCondtion.ContainsKey("ARE_NO_E"))
            {
                if (!string.IsNullOrEmpty(dyCondtion["ARE_NO_E"].ToString()))
                    _sqlWhere += " AND C.AREA_NO >=@ARE_NO_E";
                _paraCount++;
            }

            if (_paraCount > 0)
            {
                SqlParameter[] _sqlPara = new SqlParameter[_paraCount];
                _paraCount = -1;
                //截止日期（起）
                if (dyCondtion.ContainsKey("END_DD_B"))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@END_DD_B",SqlDbType.DateTime);
                    if (!string.IsNullOrEmpty(dyCondtion["END_DD_B"].ToString()))
                    _sqlPara[_paraCount].Value = dyCondtion["END_DD_B"].ToString();
                }
                //截止日期（止）
                if (dyCondtion.ContainsKey("END_DD_E"))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@END_DD_E", SqlDbType.DateTime);
                    if (!string.IsNullOrEmpty(dyCondtion["END_DD_E"].ToString()))
                        _sqlPara[_paraCount].Value = dyCondtion["END_DD_E"].ToString();
                }
                //客户(起)
                if (dyCondtion.ContainsKey("CUS_NO_B"))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@CUS_NO_B", SqlDbType.VarChar,20);
                    _sqlPara[_paraCount].Value = dyCondtion["CUS_NO_B"].ToString();
                }
                //客户(止)
                if (dyCondtion.ContainsKey("CUS_NO_E"))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@CUS_NO_E", SqlDbType.VarChar, 20);
                    _sqlPara[_paraCount].Value = dyCondtion["CUS_NO_E"].ToString();
                }
                //合约代号(起)
                if (dyCondtion.ContainsKey("PAC_NO_B"))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@PAC_NO_B", SqlDbType.VarChar, 20);
                    _sqlPara[_paraCount].Value = dyCondtion["PAC_NO_B"].ToString();
                }
                //合约代号(止)
                if (dyCondtion.ContainsKey("PAC_NO_E"))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@PAC_NO_E", SqlDbType.VarChar, 20);
                    _sqlPara[_paraCount].Value = dyCondtion["PAC_NO_E"].ToString();
                }
                //区域(起)
                if (dyCondtion.ContainsKey("ARE_NO_B"))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@ARE_NO_B", SqlDbType.VarChar, 20);
                    _sqlPara[_paraCount].Value = dyCondtion["ARE_NO_B"].ToString();
                }
                //区域(止)
                if (dyCondtion.ContainsKey("ARE_NO_E"))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@ARE_NO_E", SqlDbType.VarChar, 20);
                    _sqlPara[_paraCount].Value = dyCondtion["ARE_NO_E"].ToString();
                }
                if (dyCondtion.ContainsKey("SQLWHERE"))
                {
                    if (!string.IsNullOrEmpty(dyCondtion["SQLWHERE"].ToString()))
                        _sqlWhere += dyCondtion["SQLWHERE"].ToString();
                }                
                string _sqlStr = "SELECT A.CUS_NO,B.NAME AS CUS_NAME,A.PAC_DD,A.PAC_NO,A.TITLE,A.START_DD,A.END_DD,DATEDIFF(DAY,GETDATE(),ISNULL(A.END_DD,GETDATE())) AS DAY_LEFT,A.REM "
                    + " FROM CUS_PACT A "                
                    + " LEFT JOIN CUST B ON B.CUS_NO =A.CUS_NO "
                    + " LEFT JOIN AREA C ON C.AREA_NO = B.CUS_ARE "
                    + " WHERE 1=1 " + _sqlWhere;

                this.FillDataset(_sqlStr, _ds, new string[1] { "CUS_PACT" }, _sqlPara);
                DataColumn[] _dca = new DataColumn[1];
                _dca[0] = _ds.Tables["CUS_PACT"].Columns["PAC_NO"];
                _ds.Tables["CUS_PACT"].PrimaryKey = _dca;
            }
            return _ds;
        }
        /// <summary>
        /// 根据保修卡取合约
        /// </summary>
        /// <param name="wcNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByWcNo(string wcNo)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@WC_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = wcNo;
            string _sqlStr = "SELECT WC_NO,PAC_NO FROM CUS_PACT1 WHERE WC_NO = @WC_NO";
            this.FillDataset(_sqlStr, _ds, new string[] { "CUS_PACT1" }, _sqlPara);
            return _ds;
        }
		#endregion

        #region 取得上一比资料的实签约金额
		/// <summary>
		/// 取得上一比资料的实签约金额
		/// </summary>
		/// <param name="CusNo">客户代号</param>
		/// <returns></returns>
        public SunlikeDataSet GetFACT(string CusNo)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar,12);
			_spc[0].Value = CusNo;
			string _sql = "Select PAC_NO,PAC_DD,TITLE,MASTER_ID,CUS_NO,STATUS,START_DD,END_DD,AMTN_BG,AMTN_FACT,SAL_NO,"
				+ " SRV_NO,FILE1,FILE_ID1,FILE_NAME1,FILE2,FILE_ID2,FILE_NAME2,REM,USR,DEP FROM CUS_PACT WHERE CUS_NO = @CUS_NO order by PAC_DD desc";
			this.FillDataset(_sql,_ds,new string[]{"CUS_PACT"},_spc);
			return _ds;
		}
		#endregion

		#region 判断一个客户是否只有一个主约
		/// <summary>
		/// 判断一个客户是否只有一个主约
		/// </summary>
		/// <param name="cusNo">客户代号</param>
		/// <returns></returns>
		public SunlikeDataSet boolMaster(string cusNo)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@cusNO", SqlDbType.VarChar,12);
			_spc[0].Value = cusNo;
			string _sql = "select MASTER_ID,CUS_NO,PAC_NO from cus_pact where cus_no =@cusNO";
			this.FillDataset(_sql,_ds,new string[]{"CUS_PACT"},_spc);
			return _ds;

		}
		#endregion

        #region GetPrdt
        /// <summary>
        /// 取得保修卡的货品资料
        /// </summary>
        /// <param name="WC_NO">保修卡号</param>
        /// <returns></returns>
        public SunlikeDataSet GetPrdt(string WC_NO)
        {
            SunlikeDataSet ds = new SunlikeDataSet();
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@WC_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = WC_NO;
            string _sql = "Select A.WC_NO,A.PRD_NO,A.PRD_MARK,B.NAME PRD_NAME,A.CUS_NO,C.NAME CUS_NAME " +
                " FROM MF_WC A LEFT OUTER JOIN PRDT B ON B.PRD_NO=A.PRD_NO " +
                " LEFT OUTER JOIN CUST C ON C.CUS_NO=A.CUS_NO "+
                " WHERE A.WC_NO = @WC_NO ";
            this.FillDataset(_sql, ds, new string[1] { "MF_WCPRDT" }, _spc);
            return ds;
        }
        #endregion

        #region 取合约种类
        /// <summary>
        /// 取合约种类
        /// </summary>
        /// <param name="KndNo">种类代号</param>
        /// <param name="isTree">含下属否</param>
        /// <returns></returns>
        public SunlikeDataSet GetPACTKnd(string KndNo, bool isTree)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@KndNo", SqlDbType.VarChar, 10);
            _spc[0].Value = KndNo;
            string _sql = "";
            if (!isTree)
            {
                _sql = "select KND_NO,UP,NAME,USR from CUS_PACT_I where KND_NO=@KndNo";
            }
            else
            {
                _sql = "select KND_NO,UP,NAME,LEVEL,PATH from ufn_GetPACTModTree(@KndNo)";
            }
            FillDataset(_sql, _ds, new string[] { "CUS_PACT_I" }, _spc);            
            return _ds;
        }
        #endregion

        #region 取得天心註冊訊息
        public SunlikeDataSet GetAttnReg(object cus_no)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            string _sqlString = "Select * from ATTN_REG ";
            string _sqlWhere = " Where CUS_NO='" + cus_no + "'";
            _sqlString += _sqlWhere;
            this.FillDataset(_sqlString, _ds, new string[] { "ATTN_REG" });
            return _ds;
        }
        #endregion
    }
}
