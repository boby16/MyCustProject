using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPTT.
	/// </summary>
	public class DbDRPTT :Sunlike.Business.Data.DbObject
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
		public DbDRPTT(string connStr) : base(connStr)
		{
		}

		#region 取批次调价单表的结构
		/// <summary>
		/// 取批次调价单表的结构
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetTT_Comp()
		{
			SunlikeDataSet _ds = new SunlikeDataSet("DRPTT");
			this.FillDataset("SELECT TT_NO,TT_DD,SEL_ID,AREA_NO,CHK_MAN,CLS_DATE,USR,DEP,REM,CHG_TYPE,SAVE_ID,MANU_TJ,LOCK_MAN FROM MF_TT WHERE (NULL <> NULL);"
				+" SELECT TT_NO, ITM, PRD_NO, PRD_MARK, S_DD, E_DD, V_TYPE, [VALUE] FROM TF_TT WHERE (NULL <> NULL);"
				+" SELECT TT_NO, CUS_NO FROM TF_TT1 WHERE (NULL <> NULL)",_ds , new string[]{"MF_TT" , "TF_TT" , "TF_TT1"});

			_ds.Relations.Add(new DataRelation("MF_TF_TT_RL" , _ds.Tables["MF_TT"].Columns["TT_NO"] , _ds.Tables["TF_TT"].Columns["TT_NO"]));
			_ds.Relations.Add(new DataRelation("MF_TF_TT_RL1" , _ds.Tables["MF_TT"].Columns["TT_NO"] , _ds.Tables["TF_TT1"].Columns["TT_NO"]));
			return _ds;
		}
		#endregion

		#region 取批次调价单表
		/// <summary>
		/// 取批次调价单表
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetDRPTT(string tt_No)
		{
			SunlikeDataSet _ds = new SunlikeDataSet("DRPTT");
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = tt_No;
			this.FillDataset("SELECT TT_NO, TT_DD, SEL_ID, CHG_TYPE, ISNULL(AREA_NO, '') AS AREA_NO, "
				+"ISNULL ((SELECT NAME FROM AREA WHERE AREA_NO = M.AREA_NO), '') AS AREA_NAME, "
				+"ISNULL(CHK_MAN, '') AS CHK_MAN,ISNULL((SELECT TOP 1 NAME FROM SunSystem..PSWD WHERE USR = M.CHK_MAN),'') AS CHK_MAN_NAME, "
				+"CLS_DATE, USR,ISNULL((SELECT TOP 1 NAME FROM SunSystem..PSWD WHERE USR = M.USR),'') AS USR_NAME, DEP, REM, "
				+"CHG_TYPE,SAVE_ID,MANU_TJ,LOCK_MAN FROM MF_TT M WHERE (M.TT_NO = @TT_NO);"
				+"SELECT TF_TT.TT_NO, TF_TT.ITM, TF_TT.PRD_NO, PRDT.NAME, TF_TT.PRD_MARK, ISNULL(TF_TT.S_DD, '') AS S_DD, "
				+"TF_TT.E_DD, TF_TT.V_TYPE, TF_TT.[VALUE] FROM TF_TT "
				+"LEFT OUTER JOIN PRDT ON TF_TT.PRD_NO = PRDT.PRD_NO WHERE (TF_TT.TT_NO = @TT_NO);"
				+"SELECT TF_TT1.TT_NO, TF_TT1.CUS_NO, CUST.NAME FROM TF_TT1 "
				+"INNER JOIN CUST ON TF_TT1.CUS_NO = CUST.CUS_NO WHERE (TF_TT1.TT_NO = @TT_NO)",
				_ds , new string[]{"MF_TT" , "TF_TT" , "TF_TT1"} , _pt);
			return _ds;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tt_No"></param>
		/// <param name="OnlyFillSchema"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDRPTT(string tt_No,bool OnlyFillSchema )
		{
			SunlikeDataSet _ds = new SunlikeDataSet("DRPTT");
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = tt_No;
			if (!OnlyFillSchema)
			{
				this.FillDataset(" SELECT M.*,"
					+" ISNULL ((SELECT NAME FROM AREA WHERE AREA_NO = M.AREA_NO), '') AS AREA_NAME, "
					+" ISNULL((SELECT TOP 1 NAME FROM SunSystem..PSWD WHERE USR = M.CHK_MAN),'') AS CHK_MAN_NAME,"
					+" ISNULL((SELECT TOP 1 NAME FROM SunSystem..PSWD WHERE USR = M.USR),'') AS USR_NAME"
					+" FROM MF_TT M WHERE M.TT_NO = @TT_NO;"
					+" SELECT T.*,"
					+" PRDT.NAME"
					+" FROM TF_TT T "
					+" LEFT OUTER JOIN PRDT ON T.PRD_NO = PRDT.PRD_NO WHERE T.TT_NO = @TT_NO;"
					+" SELECT T1.*, CUST.NAME FROM TF_TT1 T1 INNER JOIN CUST ON T1.CUS_NO = CUST.CUS_NO WHERE T1.TT_NO = @TT_NO",
					_ds , new string[]{"MF_TT" , "TF_TT" , "TF_TT1"} , _pt);
			}
			else
			{
				this.FillDatasetSchema(" SELECT M.*,"
					+" ISNULL ((SELECT NAME FROM AREA WHERE AREA_NO = M.AREA_NO), '') AS AREA_NAME, "
					+" ISNULL((SELECT TOP 1 NAME FROM SunSystem..PSWD WHERE USR = M.CHK_MAN),'') AS CHK_MAN_NAME,"
					+" ISNULL((SELECT TOP 1 NAME FROM SunSystem..PSWD WHERE USR = M.USR),'') AS USR_NAME"
					+" FROM MF_TT M WHERE M.TT_NO = @TT_NO;"
					+" SELECT T.*,"
					+" PRDT.NAME"
					+" FROM TF_TT T "
					+" LEFT OUTER JOIN PRDT ON T.PRD_NO = PRDT.PRD_NO WHERE (T.TT_NO = @TT_NO);"
					+" SELECT T1.*, CUST.NAME FROM TF_TT1 T1 INNER JOIN CUST ON T1.CUS_NO = CUST.CUS_NO WHERE T1.TT_NO = @TT_NO",
					_ds , new string[]{"MF_TT" , "TF_TT" , "TF_TT1"} , _pt);
			}
			DataColumn[] _dca = new DataColumn[2];
			_dca[0] = _ds.Tables["TF_TT"].Columns["TT_NO"];
			_dca[1] = _ds.Tables["TF_TT"].Columns["ITM"];
			_ds.Tables["TF_TT"].PrimaryKey = _dca;

			DataColumn[] _dca1 = new DataColumn[2];
			_dca1[0] = _ds.Tables["TF_TT1"].Columns["TT_NO"];
			_dca1[1] = _ds.Tables["TF_TT1"].Columns["CUS_NO"];
			_ds.Tables["TF_TT1"].PrimaryKey = _dca1;

			_ds.Relations.Add(new DataRelation("MF_TT_TF_TT", _ds.Tables[0].Columns["TT_NO"],_ds.Tables[1].Columns["TT_NO"]));
			_ds.Relations.Add(new DataRelation("MF_TT_TF_TT1", _ds.Tables[0].Columns["TT_NO"],_ds.Tables[2].Columns["TT_NO"]));
			return _ds;
		}
		#endregion

		#region 取批次调价单表内容
		/// <summary>
		/// 取批次调价单表内容
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetTT(string tt_No)
		{
			SunlikeDataSet _ds = new SunlikeDataSet("DRPTT");
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = tt_No;
			this.FillDataset("SELECT TT_NO, TT_DD, SEL_ID, AREA_NO, CHK_MAN, CLS_DATE, USR, DEP, REM, CHG_TYPE,SAVE_ID,LOCK_MAN FROM MF_TT WHERE (TT_NO = @TT_NO);SELECT TT_NO, ITM, PRD_NO, PRD_MARK, S_DD, E_DD, V_TYPE, [VALUE] FROM TF_TT WHERE (TT_NO = @TT_NO);SELECT TT_NO, CUS_NO FROM TF_TT1 WHERE (TT_NO = @TT_NO)",
				_ds , new string[]{"MF_TT" , "TF_TT" , "TF_TT1"} , _pt);
			_ds.Relations.Add(new DataRelation("MF_TF_TT_RL" , _ds.Tables["MF_TT"].Columns["TT_NO"] , _ds.Tables["TF_TT"].Columns["TT_NO"]));
			_ds.Relations.Add(new DataRelation("MF_TF_TT_RL1" , _ds.Tables["MF_TT"].Columns["TT_NO"] , _ds.Tables["TF_TT1"].Columns["TT_NO"]));
			return _ds;
		}
		#endregion
		
		#region 批次调价单终审
		/// <summary>
		/// 批次调价单终审
		/// </summary>
		/// <param name="tt_No">单号</param>
		/// <param name="chk_Man">审核人</param>
		/// <param name="chk_DD">审核时间</param>
		public void Auditing_TT(string tt_No , string chk_Man , DateTime chk_DD)
		{
			SqlParameter[] _pt = new SqlParameter[3];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar ,20);
			_pt[0].Value = tt_No;
			_pt[1] = new SqlParameter("@CLS_DATE" , SqlDbType.DateTime);
			_pt[1].Value = chk_DD.ToString("yyyy-MM-dd HH:mm:ss");
			_pt[2] = new SqlParameter("@CHK_MAN" , SqlDbType.Char ,8);
			_pt[2].Value = chk_Man;
			this.ExecuteNonQuery("UPDATE MF_TT SET CLS_DATE = @CLS_DATE , CHK_MAN = @CHK_MAN WHERE TT_NO = @TT_NO" , _pt);
		}
		#endregion

		#region 批次调价单反审核
		/// <summary>
		/// 批次调价单反审核
		/// </summary>
		/// <param name="tt_No">单号</param>
		public void RollBack_TT(string tt_No)
		{
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar ,20);
			_pt[0].Value = tt_No;
			this.ExecuteNonQuery("UPDATE MF_TT SET CLS_DATE = NULL , CHK_MAN = NULL WHERE TT_NO = @TT_NO" , _pt);
		}
		#endregion

		#region 删除批次调价单
		/// <summary>
		/// 删除批次调价单
		/// </summary>
		/// <param name="tt_No">单号</param>
		/// <param name="Del">是否删除批次调价单</param>
		/// <param name="Del_P">是否恢复调价前价格</param>
		/// <returns></returns>
		public string Delete(string tt_No,bool Del,bool Del_P)
		{
			SqlParameter[] _pt = new SqlParameter[4];
			_pt[0] =  new SqlParameter("@TT_NO",SqlDbType.VarChar , 20);
			_pt[0].Value = tt_No;
			_pt[1] =  new SqlParameter("@DEL",SqlDbType.Char);
			if(Del)
			{
				_pt[1].Value = "T";
			}
			else
			{
				_pt[1].Value = "F";
			}
			_pt[2] =  new SqlParameter("@DEL_P",SqlDbType.Char);
			if(Del_P)
			{
				_pt[2].Value = "T";
			}
			else
			{
				_pt[2].Value = "F";
			}
			_pt[3] = new System.Data.SqlClient.SqlParameter("@error",SqlDbType.VarChar,1);
			_pt[3].Direction = System.Data.ParameterDirection.Output;

			this.ExecuteSpNonQuery("sp_DelTt", _pt);
			return (string)_pt[3].Value;
		}
		#endregion

		#region 审核批次调价
		/// <summary>
		/// GetTjData
		/// </summary>
		/// <param name="pTjNo"></param>
		/// <returns></returns>
		public DataTable GetTjData(string pTjNo)
		{
			string _ptString = "";
			if (!String.IsNullOrEmpty(pTjNo))
			{
				_ptString = " AND TJ_NO = @TJ_NO";
			}
			string _sqlString = "SELECT MT.*,CU.CUS_NAME,TT.ITM,TT.PRD_NO,TT.QTY,TT.UPR4_OLD,TT.UPR4,TT.AMTN_NET,TT.REM,PT.PRD_NAME,MT.AUD_DD FROM"
				+ " (SELECT TJ_NO,TJ_DD,CUS_NO,AUD_FLAG,TT_NO,AUD_DD FROM MF_TJ WHERE 1=1 "+_ptString+") AS MT"
				+ " LEFT JOIN"
				+ " (SELECT CUS_NO,SNM AS CUS_NAME FROM CUST) AS CU"
				+ " ON MT.CUS_NO = CU.CUS_NO"
				+ " LEFT JOIN"
				+ " (SELECT TJ_NO,ITM,PRD_NO,QTY,UPR4_OLD,UPR4,AMTN_NET,REM FROM TF_TJ) AS TT"
				+ " ON MT.TJ_NO = TT.TJ_NO"
				+ " LEFT JOIN"
				+ " (SELECT PRD_NO,NAME AS PRD_NAME FROM PRDT) AS PT"
				+ " ON TT.PRD_NO = PT.PRD_NO"
				+ " ORDER BY TT.ITM ";
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TJ_NO",SqlDbType.VarChar);
			_pt[0].Value = pTjNo;
			DataTable _dt = this.ExecuteDataset(_sqlString,_pt).Tables[0];
			return _dt;
		}

		/// <summary>
		/// 得到稽核/待稽核记录
		/// </summary>
		/// <param name="pFlag"></param>
		/// <param name="CusB"></param>
		/// <param name="CusE"></param>
		/// <returns></returns>
		public DataTable GetTjData(bool pFlag,string CusB,string CusE)
		{
			SqlParameter[] _aryPt = new SqlParameter[2];
			_aryPt[0] = new SqlParameter("@CUSB", SqlDbType.VarChar, 12);
			_aryPt[0].Value = CusB;
			_aryPt[1] = new SqlParameter("@CUSE", SqlDbType.VarChar, 12);
			_aryPt[1].Value = CusE;

			string _sqlFlag = "";
			if (pFlag)
			{
				_sqlFlag = " WHERE AUD_FLAG = 'T' ";
			}
			else
			{
				_sqlFlag = " WHERE (AUD_FLAG IS NULL OR AUD_FLAG = 'F') ";
			}
			if (CusB != null && CusB.Length > 0 )
			{
				_sqlFlag += " AND CUS_NO>=@CUSB";
			}
			if (CusE != null && CusE.Length > 0 )
			{
				_sqlFlag += " AND CUS_NO<=@CUSE";
			}			
			string _sqlString = "SELECT * FROM MF_TJ "+_sqlFlag;
			DataTable _dt = this.ExecuteDataset(_sqlString,_aryPt).Tables[0];
			_dt.TableName = "MF_TJ";
			return _dt;
		}

		/// <summary>
		/// 得到稽核/待稽核记录
		/// </summary>
		/// <param name="Date1"></param>
		/// <param name="Date2"></param>
		/// <param name="pFlag"></param>
		/// <param name="CusB"></param>
		/// <param name="CusE"></param>
		/// <returns></returns>
		public DataTable GetTjData(DateTime Date1,DateTime Date2,bool pFlag,string CusB,string CusE)
		{
			SqlParameter[] _aryPt = new SqlParameter[4];
			_aryPt[0] = new SqlParameter("@CUSB", SqlDbType.VarChar, 12);
			_aryPt[0].Value = CusB;
			_aryPt[1] = new SqlParameter("@CUSE", SqlDbType.VarChar, 12);
			_aryPt[1].Value = CusE;
			_aryPt[2] = new SqlParameter("@DATE1", SqlDbType.DateTime);
			_aryPt[2].Value = Date1;
			_aryPt[3] = new SqlParameter("@DATE2", SqlDbType.DateTime);
			_aryPt[3].Value = Date2;

			string _sqlFlag = "";
			if (pFlag)
			{
				_sqlFlag = " WHERE AUD_FLAG = 'T' ";
			}
			else
			{
				_sqlFlag = " WHERE (AUD_FLAG IS NULL OR AUD_FLAG = 'F') ";
			}
			_sqlFlag += " AND CUS_NO>=@CUSB AND CUS_NO<=@CUSE AND TJ_DD>=@DATE1 AND TJ_DD <= @DATE2";			
			string _sqlString = "SELECT * FROM MF_TJ "+_sqlFlag;
			DataTable _dt = this.ExecuteDataset(_sqlString,_aryPt).Tables[0];
			_dt.TableName = "MF_TJ";
			return _dt;
		}

        /// <summary>
        /// 反稽核
        /// </summary>
        /// <param name="pTjNo">待稽核单号集合</param>
        public int UnSetFlag(string pTjNo, string isAudit, string jhDate)
        {
            int _isOk = 0;//0：成功；1：已开票；2：超过处理时间
            string _sqlString = String.Empty;
            string _sql = String.Empty;

            if (isAudit == "T")
            {
                if (jhDate != String.Empty)
                {
                    _sql = "SELECT AUD_DD FROM MF_TJ WHERE TJ_NO='" + pTjNo + "' AND AUD_DD>='" + jhDate + "'";
                    int _isRow = this.ExecuteNonQuery(_sql);
                    if (_isRow <= 0)
                    {
                        return 2;
                    }
                }
            }

            _sql = "SELECT SUM(ISNULL(AMTN_NET_FP,0)) AS AMTN_NET_FP,SUM(ISNULL(QTY_FP,0)) AS QTY_FP FROM TF_TJ WHERE TJ_NO='" + pTjNo + "'";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_TJ" });
            decimal _sumAmtn = 0;
            if (_ds.Tables["TF_TJ"].Rows.Count > 0)
            {
                for (int i = 0; i < _ds.Tables["TF_TJ"].Rows.Count; i++)
                {
                    if (_ds.Tables["TF_TJ"].Rows[i]["AMTN_NET_FP"] != System.DBNull.Value)
                    {
                        _sumAmtn += Convert.ToDecimal(_ds.Tables["TF_TJ"].Rows[i]["AMTN_NET_FP"]);
                    }
                    if (_ds.Tables["TF_TJ"].Rows[i]["QTY_FP"] != System.DBNull.Value)
                    {
                        _sumAmtn += Convert.ToDecimal(_ds.Tables["TF_TJ"].Rows[i]["QTY_FP"]);
                    }
                }
            }
            if (_sumAmtn == 0)
            {
                _sqlString = "UPDATE MF_TJ SET AUD_FLAG = 'F',AUD_DD=null WHERE TJ_NO = '" + pTjNo + "'";
            }
            else
            {
                _sqlString = "UPDATE MF_TJ SET AUD_FLAG = 'F',AUD_DD=null WHERE 1<>1";
            }
            int _rowCount = this.ExecuteNonQuery(_sqlString);
            if (_rowCount <= 0)
            {
                _isOk = 1;
            }
            return _isOk;
        }
        /// <summary>
        /// 稽核调价单
        /// </summary>
        /// <param name="pTjNo">待稽核单号集合</param>
        public bool SetFlag(string pTjNo)
        {
            bool _isOk = true;
            string _sqlString = "UPDATE MF_TJ SET AUD_FLAG = 'T',AUD_DD='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE TJ_NO ='" + pTjNo + "'";
            int _rowCount = this.ExecuteNonQuery(_sqlString);
            if (_rowCount <= 0)
            {
                _isOk = false;
            }
            return _isOk;
        }
		#endregion

		#region 根据批次调价单取客户调价单内容
		/// <summary>
		/// 根据批次调价单取客户调价单内容
		/// </summary>
		/// <param name="ttNo">批次调价单号</param>
		/// <returns></returns>
		public SunlikeDataSet GetTjFromTT(string ttNo)
		{
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@TT_NO", SqlDbType.VarChar, 30);
			_aryPt[0].Value = ttNo;
			string _sql = "SELECT TJ_NO,TJ_DD,CUS_NO,ISNULL(AUD_FLAG,'F') AUD_FLAG FROM MF_TJ WHERE TT_NO = @TT_NO";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null,_aryPt);
			return _ds;
		}
		#endregion

		#region 判断区域上层区域定价政策是否为含下属
		/// <summary>
		/// 判断区域上层区域定价政策是否为含下属
		/// </summary>
		/// <param name="AreaNo"></param>
		/// <param name="PrdNo"></param>
		/// <returns></returns>
		public SunlikeDataSet GetAreaPriceIncludeUnder(string AreaNo,string PrdNo)
		{
			string _sql = "SELECT AREA_INCLUDE FROM PRDT_UPR4 WHERE PRD_NO=@PRD_NO AND AREA_NO=@AREA_NO ";
			SqlParameter[] _pt = new SqlParameter[2];
			_pt[0] = new SqlParameter("@PRD_NO" , SqlDbType.VarChar , 30);
			_pt[0].Value = PrdNo;
			_pt[1] = new SqlParameter("@AREA_NO" , SqlDbType.VarChar , 8);
			_pt[1].Value = AreaNo;

			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"PRDT_UPR4"},_pt);
			
			return _ds;
		}
		#endregion
	}
}
