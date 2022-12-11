using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// 退货单申请
	/// </summary>
	public class DbDRPYI :Sunlike.Business.Data.DbObject
	{
		/// <summary>
		/// 退货单申请
		/// </summary>
		/// <param name="connStr">数据库连接字符串</param>
		public DbDRPYI(string connStr) : base(connStr)
		{
		}

		#region 取退货单TF_DYH表
		/// <summary>
		/// 取退货单TF_DYH表
		/// </summary>
		/// <param name="yi_ID">退货编号</param>
		/// <param name="yi_NO">退货单号</param>
		/// <returns>取退货单TF_DYH表</returns>
		public DataTable GetTF_DYH(string yi_ID , string yi_NO)
		{
			System.Data.DataTable _dt = new DataTable("TF_DYH");
			try
			{
				System.Data.SqlClient.SqlParameter[] _pt = new SqlParameter[2];
				_pt[0] = new SqlParameter("@YH_ID" , SqlDbType.Char , 2);
				_pt[0].Value = yi_ID;
				_pt[1] = new SqlParameter("@YH_NO" , SqlDbType.VarChar , 20);
				_pt[1].Value = yi_NO;
				_dt = this.ExecuteDataset("SELECT TF_DYH.YH_ID, TF_DYH.YH_NO, PRDT.NAME, PRDT.SNM, TF_DYH.ITM,  " +
					"      TF_DYH.PRD_NO, TF_DYH.PRD_MARK, TF_DYH.WH, MY_WH.NAME AS WH_NAME,  " +
					"      TF_DYH.EST_DD, TF_DYH.QTY, TF_DYH.QTY_OLD, TF_DYH.UNIT, TF_DYH.AMTN, TF_DYH.UP,  " +
					"      PRDT.SPC , TF_DYH.REM, TF_DYH.DEL_ID " +
					"FROM TF_DYH INNER JOIN " +
					"      PRDT ON TF_DYH.PRD_NO = PRDT.PRD_NO LEFT OUTER JOIN " +
					"      MY_WH ON TF_DYH.WH = MY_WH.WH " +
					"WHERE (YH_ID = @YH_ID) AND (YH_NO = @YH_NO) " +
					"ORDER BY TF_DYH.YH_ID, TF_DYH.YH_NO, TF_DYH.ITM" , _pt).Tables[0];

			}
			catch(Exception ex)
			{
				throw ex;
			}
			return _dt;
		}
		#endregion

		#region 取退货单MF_DYH表
		/// <summary>
		/// 取退货单MF_DYH表
		/// </summary>
		/// <param name="yi_ID">退货编号</param>
		/// <param name="yi_NO">退货单号</param>
		/// <param name="compNo"></param>
		/// <returns>取退货单MF_DYH表</returns>
		public DataTable GetMF_DYH(string yi_ID , string yi_NO, string compNo)
		{
			System.Data.DataTable _dt = new DataTable("MF_DYH");
			try
			{
				System.Data.SqlClient.SqlParameter[] _pt = new SqlParameter[3];
				_pt[0] = new SqlParameter("@YH_ID" , SqlDbType.Char , 2);
				_pt[0].Value = yi_ID;
				_pt[1] = new SqlParameter("@YH_NO" , SqlDbType.VarChar , 20);
				_pt[1].Value = yi_NO;
				_pt[2] = new SqlParameter("@COMPNO" , SqlDbType.VarChar , 4);
				_pt[2].Value = compNo;
				_dt = this.ExecuteDataset("SELECT MF_DYH.USR,MF_DYH.YH_ID, MF_DYH.YH_NO, MF_DYH.YH_DD, MF_DYH.CLS_DATE, ISNULL(MF_DYH.CHK_MAN, '') AS CHK_MAN, ISNULL(MF_DYH.CLS_ID, 'F') AS CLS_ID, ISNULL(SunSystem.dbo.PSWD.NAME, '') AS NAME, MF_DYH.CUS_NO, ISNULL(CUST.NAME, '') AS CUST_NAME, MF_DYH.FX_WH , MF_DYH.REM, MF_DYH.FUZZY_ID, MF_DYH.SAVE_ID FROM MF_DYH LEFT OUTER JOIN SunSystem.dbo.PSWD ON MF_DYH.CHK_MAN = SunSystem.dbo.PSWD.USR AND SunSystem.dbo.PSWD.COMPNO = @COMPNO LEFT OUTER JOIN SunSystem.dbo.PSWD CUST ON MF_DYH.CUS_NO = CUST.USR WHERE MF_DYH.YH_ID = @YH_ID AND MF_DYH.YH_NO = @YH_NO AND CUST.COMPNO = @COMPNO" , _pt).Tables[0];
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return _dt;
		}
		#endregion

		#region 取MF_DYH和TF_DYH表的结构
		/// <summary>
		/// 取MF_DYH和TF_DYH表的结构
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetMT()
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			try
			{
				this.FillDataset("SELECT YH_ID, YH_NO, DEP, YH_DD, CLS_ID, CLS_DATE, CHK_MAN, CUS_NO, USR, REM , FX_WH, FUZZY_ID, SAVE_ID FROM MF_DYH WHERE (NULL <> NULL);SELECT YH_ID, YH_NO, ITM, PRD_NO, PRD_MARK, WH, EST_DD, QTY, UNIT, AMTN, QTY_RTN, UP, KEY_ITM FROM TF_DYH WHERE (NULL <> NULL)",
					_ds , new string[]{"MF_DYH" , "TF_DYH"});
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL" , _ds.Tables["MF_DYH"].Columns["YH_ID"] , _ds.Tables["TF_DYH"].Columns["YH_ID"]));
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL1" , _ds.Tables["MF_DYH"].Columns["YH_NO"] , _ds.Tables["TF_DYH"].Columns["YH_NO"]));
				DataTable _dtBody = _ds.Tables["TF_DYH"];
				_dtBody.Columns["KEY_ITM"].AutoIncrement = true;
				_dtBody.Columns["KEY_ITM"].AutoIncrementSeed = _dtBody.Rows.Count > 0 ? Convert.ToInt32(_dtBody.Select("","KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
				_dtBody.Columns["KEY_ITM"].AutoIncrementStep = 1;
				_dtBody.Columns["KEY_ITM"].Unique = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return _ds;
		}
		#endregion
		
		#region 审核处理

		/// <summary>
		/// 单据结案
		/// </summary>
		/// <param name="bilNo"></param>
		/// <param name="bFlag"></param>
		public void CloseBil(string bilNo, bool bFlag)
		{
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
			_aryPt[0].Value = bilNo;
			string _sSql = "";
			if (bFlag)
			{
				_sSql = "UPDATE MF_DYH SET CLS_ID = null WHERE YH_ID = 'YI' AND YH_NO = @YH_NO";
			}
			else
			{
				_sSql = "UPDATE MF_DYH SET CLS_ID = 'T' WHERE YH_ID = 'YI' AND YH_NO = @YH_NO";
			}
			this.ExecuteNonQuery(_sSql, _aryPt);
		}

		/// <summary>
		/// 检查是否已经结案
		/// </summary>
		/// <param name="yiNo"></param>
		/// <returns></returns>
		public bool CheckClose(string yiNo)
		{
			SqlParameter[] _ptAry = new SqlParameter[1];
			_ptAry[0] = new SqlParameter("@YI_NO", SqlDbType.VarChar, 20);
			_ptAry[0].Value = yiNo;
			string _strSql = "SELECT 1 FROM MF_DYH WHERE YH_ID = 'YI' AND CLS_ID='T' AND YH_NO=@YI_NO";
			DataTable _dt = this.ExecuteDataset(_strSql, _ptAry).Tables[0];
			if (_dt.Rows.Count > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region 审核退货单据错误回退
		/// <summary>
		/// 审核退货单据错误回退
		/// </summary>
		/// <param name="yi_No">单据编号</param>
		public void RollBackYI(string yi_No)
		{
			try
			{
				SqlParameter[] _pt = new SqlParameter[1];
				_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar ,20);
				_pt[0].Value = yi_No;
				this.ExecuteNonQuery("UPDATE MF_DYH SET CLS_DATE = NULL , CHK_MAN = NULL WHERE YH_ID = 'YI' AND YH_NO = @YH_NO" , _pt);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region 取退货单
		/// <summary>
		/// 取MF_DYH和TF_DYH表的结构
		/// </summary>
		/// <param name="yi_No">单据编号</param>
		/// <returns></returns>
		public SunlikeDataSet GetYI(string yi_No)
		{
			SunlikeDataSet _ds = new SunlikeDataSet("DRPYI");
			try
			{
				SqlParameter[] _pt = new SqlParameter[1];
				_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar , 20);
				_pt[0].Value = yi_No;
				this.FillDataset("SELECT YH_ID, YH_NO, DEP, YH_DD, CLS_ID, CLS_DATE, CHK_MAN, CUS_NO, USR, REM , FX_WH, FUZZY_ID, SAVE_ID FROM MF_DYH WHERE (YH_ID = 'YI') AND (YH_NO = @YH_NO);SELECT YH_ID, YH_NO, ITM, KEY_ITM, PRD_NO, PRD_MARK, WH, EST_DD, QTY, QTY_OLD, UNIT, AMTN, QTY_RTN, UP, DEL_ID FROM TF_DYH WHERE (YH_ID = 'YI') AND (YH_NO = @YH_NO)",
					_ds , new string[]{"MF_DYH" , "TF_DYH"} , _pt);
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL" , _ds.Tables["MF_DYH"].Columns["YH_ID"] , _ds.Tables["TF_DYH"].Columns["YH_ID"]));
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL1" , _ds.Tables["MF_DYH"].Columns["YH_NO"] , _ds.Tables["TF_DYH"].Columns["YH_NO"]));
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return _ds;
		}
		#endregion

		#region 判断退货是否已入库
		/// <summary>
		/// 判断退货是否已入库
		/// </summary>
		/// <param name="yi_No">退货单号</param>
		/// <returns></returns>
		public bool CheckQty_Rtn(string yi_No)
		{
			bool _isRtn = false;
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = yi_No;
			DataTable _dt = this.ExecuteDataset("SELECT ISNULL(QTY_RTN , 0) AS QTY_RTN FROM TF_DYH WHERE YH_ID = 'YI' AND YH_NO = @YH_NO" , _pt).Tables[0];
			if(_dt.Rows.Count > 0)
			{
				foreach(DataRow _dr in _dt.Rows)
				{
					if(Convert.ToDecimal(_dr["QTY_RTN"]) > 0)
					{
						_isRtn = true;
						break;
					}
				}
			}
			return _isRtn;
		}
		#endregion

		//-----by db---------

		#region	取要货/退回单明细资料
		/// <summary>
		///	取要货/退回单明细资料
		/// </summary>
		/// <param name="Yh_Id">单据别</param>
		/// <param name="Yh_No">单号</param>
		/// <returns>要货/退回单明细资料</returns>
		public SunlikeDataSet GetTableYI(string Yh_Id,string Yh_No)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();

			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@YH_ID",SqlDbType.VarChar,8);
			_spc[0].Value = Yh_Id;
			_spc[1] = new SqlParameter("@YH_NO",SqlDbType.VarChar,38);
			_spc[1].Value = Yh_No;

			#region 旧SQL
			/*
			string _sql = " SELECT A.YH_NO,A.YH_DD,A.CUS_NO,B.NAME AS CUS_NAME,ISNULL(CONVERT(CHAR(10),A.EST_DD,120),'') AS EST_DD,ISNULL(A.BYBOX,'F') AS BYBOX,"
						+ " QTY=(SELECT SUM(ISNULL(QTY,0)) FROM TF_DYH B WHERE A.YH_NO=B.YH_NO AND A.YH_ID=B.YH_ID),"
						+ " AMTN=(SELECT SUM(ISNULL(AMTN,0)) FROM TF_DYH B WHERE A.YH_NO=B.YH_NO AND A.YH_ID=B.YH_ID) FROM MF_DYH A "
						+ " LEFT JOIN CUST B ON A.CUS_NO = B.CUS_NO WHERE A.YH_NO=@YH_NO AND A.YH_ID = @YH_ID;"
		
						+ " SELECT A.ITM,A.PRD_NO,B.NAME AS PRD_NAME,A.PRD_MARK,CONVERT(CHAR(10),A.EST_DD,120) AS EST_DD,A.WH,ISNULL(A.WH_OLD,'') AS WH_OLD,C.NAME AS WH_NAME,"
						+ " ISNULL(A.QTY,0) AS QTY,A.QTY_OLD,ISNULL(A.UP,0) AS UP,ISNULL(A.AMTN,0) AS AMTN,ISNULL(A.EST_OLD,'') AS EST_OLD,"
						+ " ISNULL(D.QTY,0)AS WH_QTY,ISNULL(E.QTY,0) AS CUST_WH_QTY,"

						+ " QTY_OUT=(SELECT ISNULL(SUM(ISNULL(T.QTY,0)),0) AS QTY FROM MF_IC M,TF_IC T,MY_WH W WHERE T.WH1=W.WH AND ISNULL(M.CHK_MAN,'')<>''"
						+ " AND DATEPART(YY,M.IC_DD)<=DATEPART(YY,GETDATE()) AND DATEPART(MM,M.IC_DD)<=DATEPART(MM,GETDATE()) "
						+ " AND DATEPART(YY,M.IC_DD)>=DATEPART(YY,DATEADD(YY,-1,GETDATE())) AND DATEPART(MM,M.IC_DD)>=DATEPART(MM,DATEADD(YY,-1,GETDATE())) "
						+ " AND M.IC_NO=T.IC_NO AND T.PRD_NO=A.PRD_NO AND T.PRD_MARK=A.PRD_MARK AND M.CUS_NO1=(SELECT CUS_NO FROM MF_DYH MF WHERE MF.YH_NO=A.YH_NO AND A.ITM='1') GROUP BY T.PRD_NO,C.CUS_NO),"

						+ " QTY_IN=(SELECT ISNULL(SUM(ISNULL(T.QTY,0)),0) FROM MF_IC M,TF_IC T,MY_WH W WHERE T.WH2=W.WH AND ISNULL(M.CHK_MAN,'')<>''"
						+ " AND DATEPART(YY,M.IC_DD)<=DATEPART(YY,GETDATE()) AND DATEPART(MM,M.IC_DD)<=DATEPART(MM,GETDATE()) "
						+ " AND DATEPART(YY,M.IC_DD)>=DATEPART(YY,DATEADD(YY,-1,GETDATE())) AND DATEPART(MM,M.IC_DD)>=DATEPART(MM,DATEADD(YY,-1,GETDATE())) "
						+ " AND M.IC_NO=T.IC_NO AND T.PRD_NO=A.PRD_NO AND T.PRD_MARK=A.PRD_MARK AND M.CUS_NO2=(SELECT CUS_NO FROM MF_DYH MF WHERE MF.YH_NO=A.YH_NO AND A.ITM='1') GROUP BY T.PRD_NO,C.CUS_NO),"

						+ " QTY_RATE=((SELECT ISNULL(SUM(ISNULL(T.QTY,0)),0) AS QTY FROM MF_IC M,TF_IC T,MY_WH W WHERE T.WH1=W.WH AND ISNULL(M.CHK_MAN,'')<>''"
						+ " AND DATEPART(YY,M.IC_DD)<=DATEPART(YY,GETDATE()) AND DATEPART(MM,M.IC_DD)<=DATEPART(MM,GETDATE()) "
						+ " AND DATEPART(YY,M.IC_DD)>=DATEPART(YY,DATEADD(YY,-1,GETDATE())) AND DATEPART(MM,M.IC_DD)>=DATEPART(MM,DATEADD(YY,-1,GETDATE())) "
						+ " AND M.IC_NO=T.IC_NO AND T.PRD_NO=A.PRD_NO AND T.PRD_MARK=A.PRD_MARK AND M.CUS_NO1=(SELECT CUS_NO FROM MF_DYH MF WHERE MF.YH_NO=A.YH_NO AND A.ITM='1') GROUP BY T.PRD_NO,C.CUS_NO)/"
						+ " (SELECT ISNULL(SUM(ISNULL(T.QTY,0)),1) FROM MF_IC M,TF_IC T,MY_WH W WHERE T.WH2=W.WH AND ISNULL(M.CHK_MAN,'')<>''"
						+ " AND DATEPART(YY,M.IC_DD)<=DATEPART(YY,GETDATE()) AND DATEPART(MM,M.IC_DD)<=DATEPART(MM,GETDATE()) "
						+ " AND DATEPART(YY,M.IC_DD)>=DATEPART(YY,DATEADD(YY,-1,GETDATE())) AND DATEPART(MM,M.IC_DD)>=DATEPART(MM,DATEADD(YY,-1,GETDATE())) "
						+ " AND M.IC_NO=T.IC_NO AND T.PRD_NO=A.PRD_NO AND T.PRD_MARK=A.PRD_MARK AND M.CUS_NO2=(SELECT CUS_NO FROM MF_DYH MF WHERE MF.YH_NO=A.YH_NO AND A.ITM='1') GROUP BY T.PRD_NO,C.CUS_NO))"

						+ " FROM TF_DYH A LEFT JOIN  PRDT B ON A.PRD_NO = B.PRD_NO LEFT JOIN MY_WH C ON A.WH = C.WH "
						+ " LEFT JOIN PRDT1 D ON A.WH=D.WH AND A.PRD_NO=D.PRD_NO AND A.PRD_MARK=D.PRD_MARK "
						+ " LEFT JOIN PRDT1 E ON (SELECT TOP 1 FX_WH FROM MF_DYH F WHERE A.YH_NO=F.YH_NO AND A.YH_ID=F.YH_ID)=E.WH AND A.PRD_NO=E.PRD_NO AND A.PRD_MARK=E.PRD_MARK "
						+ " WHERE A.YH_NO=@YH_NO AND A.YH_ID = @YH_ID AND ISNULL(A.DEL_ID,'')<>'T';"

						+ " SELECT A.ITM,A.PRD_NO,D.NAME AS PRD_NAME,A.CONTENT,CONVERT(CHAR(10),B.EST_DD,120) AS EST_DD,A.WH,ISNULL(A.WH_OLD,'') AS WH_OLD,C.NAME AS WH_NAME,"
						+ " ISNULL(A.QTY,0) AS QTY,A.QTY_OLD,ISNULL(B.EST_OLD,'') AS EST_OLD,0 AS WH_QTY"
						+ " FROM TF_DYH1 A LEFT JOIN MY_WH C ON A.WH = C.WH LEFT JOIN  PRDT D ON A.PRD_NO = D.PRD_NO "
						+ " INNER JOIN TF_DYH B ON A.YH_ID=B.YH_ID AND A.YH_NO=B.YH_NO AND A.ITM=B.ITM WHERE A.YH_NO=@YH_NO AND A.YH_ID = @YH_ID AND ISNULL(A.DEL_ID,'')<>'T'";
			*/
			#endregion
			/* **********************备注***********************
			 * @FUZZY_ID主要判断要货及退货单据中是否要考虑特征字段
			 * 
			 * @FUZZY_ID 为T时则不考虑特征
			 * 其他则考虑特征
			 ************************************************* */
			string _sql = "DECLARE @FUZZY_ID CHAR(1)"
				+ " SELECT @FUZZY_ID = FUZZY_ID FROM MF_DYH WHERE YH_NO = @YH_NO"
				+ " IF (@FUZZY_ID = 'T')"
 				+ " BEGIN"
  				+ " SELECT A.FUZZY_ID,A.YH_NO,A.YH_DD,A.CUS_NO,B.NAME AS CUS_NAME,"
  				+ " ISNULL(CONVERT(CHAR(10),A.EST_DD,120),'') AS EST_DD,"
  				+ " ISNULL(A.BYBOX,'F') AS BYBOX, T.QTY, T.AMTN, A.DEP,"
  				+ " ISNULL(D.NAME,'') AS DEP_NAME,A.FX_WH,ISNULL(M.NAME,'') AS FX_NAME,"
  				+ " ISNULL(A.REM,'') AS REM, T.DEL_ID "
  				+ " FROM MF_DYH A "
  				+ " JOIN CUST B "
  				+ " ON A.CUS_NO = B.CUS_NO "
  				+ " JOIN"
  				+ " (SELECT YH_ID, YH_NO,"
  				+ " SUM(ISNULL(QTY,0)) AS QTY,"
  				+ " SUM(ISNULL(AMTN,0)) AS AMTN, DEL_ID"
  				+ " FROM TF_DYH"
  				+ " WHERE ISNULL(DEL_ID,'')<>'T'"
  				+ " GROUP BY YH_ID, YH_NO, DEL_ID) T"
  				+ " ON A.YH_ID = T.YH_ID AND A.YH_NO = T.YH_NO"
  				+ " LEFT JOIN DEPT D"
  				+ " ON A.DEP = D.DEP"
  				+ " LEFT JOIN MY_WH M"
  				+ " ON A.FX_WH = M.WH"
  				+ " WHERE A.YH_NO=@YH_NO"
  				+ " AND A.YH_ID = @YH_ID"
  				+ " SELECT A.ITM,A.PRD_NO,B.NAME AS PRD_NAME,A.PRD_MARK,CONVERT(CHAR(10),A.EST_DD,120) AS EST_DD,"
  				+ " A.WH,ISNULL(A.WH_OLD,'') AS WH_OLD,C.NAME AS WH_NAME,"
  				+ " ISNULL(A.QTY,0) AS QTY,A.QTY_OLD,ISNULL(A.UP,0) AS UP,"
  				+ " ISNULL(A.AMTN,0) AS AMTN,ISNULL(A.EST_OLD,'') AS EST_OLD,"
  				+ " ISNULL(D.QTY,0)AS WH_QTY,ISNULL((SELECT SUM(QTY) FROM PRDT1 WHERE  WH=M.FX_WH AND PRD_NO= A.PRD_NO),0) AS CUST_WH_QTY,A.DEL_ID"
  				+ " FROM MF_DYH M"
  				+ " JOIN TF_DYH A"
  				+ " ON M.YH_ID = A.YH_ID AND M.YH_NO = A.YH_NO"
  				+ " JOIN  PRDT B"
  				+ " ON A.PRD_NO = B.PRD_NO"
  				+ " LEFT JOIN MY_WH C"
  				+ " ON A.WH = C.WH"
  				+ " LEFT JOIN (SELECT WH, PRD_NO, SUM(QTY) AS QTY FROM PRDT1 GROUP BY WH, PRD_NO) D ON A.WH=D.WH  AND A.PRD_NO=D.PRD_NO"
  				+ " WHERE A.YH_NO=@YH_NO AND A.YH_ID = @YH_ID"
  				+ " AND ISNULL(A.DEL_ID,'')<>'T'; SELECT A.ITM,A.PRD_NO,D.NAME AS PRD_NAME,"
  				+ " A.CONTENT,CONVERT(CHAR(10),B.EST_DD,120) AS EST_DD,A.WH,ISNULL(A.WH_OLD,'') AS WH_OLD,C.NAME AS WH_NAME, ISNULL(A.QTY,0) AS QTY,"
  				+ " A.QTY_OLD,ISNULL(B.EST_OLD,'') AS EST_OLD,0 AS WH_QTY,A.DEL_ID FROM (SELECT * FROM TF_DYH1 WHERE YH_NO = @YH_NO) A LEFT JOIN MY_WH C ON A.WH = C.WH"
  				+ " JOIN  PRDT D ON A.PRD_NO = D.PRD_NO  INNER JOIN TF_DYH B ON A.YH_ID=B.YH_ID AND A.YH_NO=B.YH_NO AND A.ITM=B.ITM"
  				+ " WHERE A.YH_NO=@YH_NO AND A.YH_ID = @YH_ID AND ISNULL(A.DEL_ID,'')<>'T'"
  				+ " END"
  				+ " ELSE"
  				+ " BEGIN"
  				+ " SELECT A.FUZZY_ID,A.YH_NO,A.YH_DD,A.CUS_NO,B.NAME AS CUS_NAME,"
  				+ " ISNULL(CONVERT(CHAR(10),A.EST_DD,120),'') AS EST_DD,"
  				+ " ISNULL(A.BYBOX,'F') AS BYBOX, T.QTY, T.AMTN, A.DEP,"
  				+ " ISNULL(D.NAME,'') AS DEP_NAME,A.FX_WH,ISNULL(M.NAME,'') AS FX_NAME,"
  				+ " ISNULL(A.REM,'') AS REM, T.DEL_ID "
  				+ " FROM MF_DYH A "
  				+ " JOIN CUST B "
  				+ " ON A.CUS_NO = B.CUS_NO "
  				+ " JOIN"
  				+ " (SELECT YH_ID, YH_NO,"
  				+ " SUM(ISNULL(QTY,0)) AS QTY,"
  				+ " SUM(ISNULL(AMTN,0)) AS AMTN, DEL_ID"
  				+ " FROM TF_DYH"
  				+ " WHERE ISNULL(DEL_ID,'')<>'T'"
  				+ " GROUP BY YH_ID, YH_NO, DEL_ID) T"
  				+ " ON A.YH_ID = T.YH_ID AND A.YH_NO = T.YH_NO"
  				+ " LEFT JOIN DEPT D"
  				+ " ON A.DEP = D.DEP"
  				+ " LEFT JOIN MY_WH M"
  				+ " ON A.FX_WH = M.WH"
  				+ " WHERE A.YH_NO=@YH_NO"
  				+ " AND A.YH_ID = @YH_ID"
  				+ " SELECT A.ITM,A.PRD_NO,B.NAME AS PRD_NAME,A.PRD_MARK,CONVERT(CHAR(10),A.EST_DD,120) AS EST_DD,"
  				+ " A.WH,ISNULL(A.WH_OLD,'') AS WH_OLD,C.NAME AS WH_NAME,"
  				+ " ISNULL(A.QTY,0) AS QTY,A.QTY_OLD,ISNULL(A.UP,0) AS UP,"
  				+ " ISNULL(A.AMTN,0) AS AMTN,ISNULL(A.EST_OLD,'') AS EST_OLD,A.DEL_ID,"
  				+ " ISNULL(D.QTY,0)AS WH_QTY,ISNULL((SELECT SUM(QTY) FROM PRDT1 WHERE  WH=M.FX_WH AND PRD_NO= A.PRD_NO AND PRD_MARK=A.PRD_MARK),0) AS CUST_WH_QTY"
  				+ " FROM MF_DYH M"
  				+ " JOIN TF_DYH A"
  				+ " ON M.YH_ID = A.YH_ID AND M.YH_NO = A.YH_NO"
  				+ " JOIN  PRDT B"
  				+ " ON A.PRD_NO = B.PRD_NO"
  				+ " LEFT JOIN MY_WH C"
  				+ " ON A.WH = C.WH"
  				+ " LEFT JOIN PRDT1 D"
  				+ " ON A.WH=D.WH"
  				+ " AND A.PRD_NO=D.PRD_NO"
  				+ " AND A.PRD_MARK=D.PRD_MARK"
  				+ " WHERE A.YH_NO=@YH_NO AND A.YH_ID = @YH_ID"
  				+ " AND ISNULL(A.DEL_ID,'')<>'T'; SELECT A.ITM,A.PRD_NO,D.NAME AS PRD_NAME,"
  				+ " A.CONTENT,CONVERT(CHAR(10),B.EST_DD,120) AS EST_DD,A.WH,ISNULL(A.WH_OLD,'') AS WH_OLD,C.NAME AS WH_NAME, ISNULL(A.QTY,0) AS QTY,"
  				+ " A.QTY_OLD,ISNULL(B.EST_OLD,'') AS EST_OLD,0 AS WH_QTY,A.DEL_ID FROM (SELECT * FROM TF_DYH1 WHERE YH_NO = @YH_NO) A LEFT JOIN MY_WH C ON A.WH = C.WH"
  				+ " JOIN  PRDT D ON A.PRD_NO = D.PRD_NO  INNER JOIN TF_DYH B ON A.YH_ID=B.YH_ID AND A.YH_NO=B.YH_NO AND A.ITM=B.ITM"
  				+ " END";

			try
			{
				this.FillDataset(_sql,_ds,new string[]{"MF_DYH","TF_DYH","TF_DYH1"},_spc);
			}
			catch(Exception _ex)
			{
				throw _ex;
			}
			return _ds;
		}
		#endregion

		#region 取要货/退回单明细资料(含批号管制)
		/// <summary>
		/// 取要货/退回单明细资料(含批号管制)
		/// </summary>
		/// <param name="Yh_Id"></param>
		/// <param name="Yh_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetTableYI1(string Yh_Id,string Yh_No)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@YH_ID",SqlDbType.VarChar,8);
			_spc[0].Value = Yh_Id;
			_spc[1] = new SqlParameter("@YH_NO",SqlDbType.VarChar,38);
			_spc[1].Value = Yh_No;

			string _sql = " SELECT A.FUZZY_ID,A.YH_NO,A.YH_DD,A.CUS_NO,B.NAME AS CUS_NAME,"
  				+ " ISNULL(CONVERT(CHAR(10),A.EST_DD,120),'') AS EST_DD,"
  				+ " ISNULL(A.BYBOX,'F') AS BYBOX, T.QTY, T.AMTN, A.DEP,"
  				+ " ISNULL(D.NAME,'') AS DEP_NAME,A.FX_WH,ISNULL(M.NAME,'') AS FX_NAME,"
                + " ISNULL(A.REM,'') AS REM,ISNULL(A.BIL_TYPE,'') AS BIL_TYPE, T.DEL_ID "
  				+ " FROM MF_DYH A "
  				+ " JOIN CUST B ON A.CUS_NO = B.CUS_NO "
  				+ " JOIN"
  				+ " (SELECT YH_ID, YH_NO,SUM(ISNULL(QTY,0)) AS QTY,SUM(ISNULL(AMTN,0)) AS AMTN, DEL_ID FROM TF_DYH"
  				+ " WHERE ISNULL(DEL_ID,'')<>'T'"
  				+ " GROUP BY YH_ID, YH_NO, DEL_ID) T"
  				+ " ON A.YH_ID = T.YH_ID AND A.YH_NO = T.YH_NO"
  				+ " LEFT JOIN DEPT D"
  				+ " ON A.DEP = D.DEP"
  				+ " LEFT JOIN MY_WH M"
  				+ " ON A.FX_WH = M.WH"
  				+ " WHERE A.YH_NO=@YH_NO"
  				+ " AND A.YH_ID = @YH_ID;"
  				+ " SELECT A.ITM,A.PRD_NO,B.NAME AS PRD_NAME,A.PRD_MARK,CONVERT(CHAR(10),A.EST_DD,120) AS EST_DD,"
  				+ " A.WH,ISNULL(A.WH_OLD,'') AS WH_OLD,C.NAME AS WH_NAME,B.SPC,"
  				+ " ISNULL(A.QTY,0) AS QTY,A.QTY_OLD,ISNULL(A.UP,0) AS UP,"
  				+ " ISNULL(A.AMTN,0) AS AMTN,ISNULL(A.EST_OLD,'') AS EST_OLD,"
  				+ " 0 AS WH_QTY,0 AS CUST_WH_QTY,A.DEL_ID,M.FX_WH"
  				+ " FROM MF_DYH M"
  				+ " JOIN TF_DYH A"
  				+ " ON M.YH_ID = A.YH_ID AND M.YH_NO = A.YH_NO"
  				+ " JOIN  PRDT B"
  				+ " ON A.PRD_NO = B.PRD_NO"
  				+ " LEFT JOIN MY_WH C"
  				+ " ON A.WH = C.WH"
  				+ " LEFT JOIN (SELECT WH, PRD_NO, SUM(QTY) AS QTY FROM PRDT1 GROUP BY WH, PRD_NO) D ON A.WH=D.WH  AND A.PRD_NO=D.PRD_NO"
  				+ " WHERE A.YH_NO=@YH_NO AND A.YH_ID = @YH_ID"
  				+ " AND ISNULL(A.DEL_ID,'')<>'T'; "
				+ " SELECT A.ITM,A.PRD_NO,D.NAME AS PRD_NAME,"
  				+ " A.CONTENT,CONVERT(CHAR(10),B.EST_DD,120) AS EST_DD,A.WH,ISNULL(A.WH_OLD,'') AS WH_OLD,C.NAME AS WH_NAME, ISNULL(A.QTY,0) AS QTY,"
  				+ " A.QTY_OLD,ISNULL(B.EST_OLD,'') AS EST_OLD,0 AS WH_QTY,A.DEL_ID FROM (SELECT * FROM TF_DYH1 WHERE YH_NO = @YH_NO) A LEFT JOIN MY_WH C ON A.WH = C.WH"
  				+ " JOIN  PRDT D ON A.PRD_NO = D.PRD_NO  INNER JOIN TF_DYH B ON A.YH_ID=B.YH_ID AND A.YH_NO=B.YH_NO AND A.ITM=B.ITM"
  				+ " WHERE A.YH_NO=@YH_NO AND A.YH_ID = @YH_ID AND ISNULL(A.DEL_ID,'')<>'T'";
			this.FillDataset(_sql,_ds,new string[]{"MF_DYH","TF_DYH","TF_DYH1"},_spc);
			_ds.Tables["TF_DYH"].Columns["WH_QTY"].ReadOnly = false;
			_ds.Tables["TF_DYH"].Columns["CUST_WH_QTY"].ReadOnly = false;
			return _ds;
		}
		#endregion

		#region 审核修改退货内容
		/// <summary>
		/// 审核修改退货内容
		/// </summary>
		/// <param name="Yh_Id"></param>
		/// <param name="Yh_No"></param>
		/// <param name="Itm"></param>
		/// <param name="Qty"></param>
		/// <param name="Est_Dd"></param>
		/// <param name="Wh"></param>
		/// <param name="Change"></param>
		public void UpdateYi(string Yh_Id , string Yh_No , int Itm , decimal Qty, string Est_Dd, string Wh, bool Change)
		{
			try
			{
				SqlParameter[] _pt = new SqlParameter[6];
				_pt[0] = new SqlParameter("@YH_ID" , SqlDbType.VarChar,8);
				_pt[0].Value = Yh_Id;
				_pt[1] = new SqlParameter("@YH_NO" , SqlDbType.VarChar,38);
				_pt[1].Value = Yh_No;
				_pt[2] = new SqlParameter("@ITM" , SqlDbType.SmallInt);
				_pt[2].Value = Itm;
				_pt[3] = new SqlParameter("@QTY" , SqlDbType.Decimal);
				_pt[3].Precision = 28;
				_pt[3].Scale = 8;
				_pt[3].Value = Qty;
				_pt[4] = new SqlParameter("@WH" , SqlDbType.VarChar,12);
				_pt[4].Value = Wh;
				_pt[5] = new SqlParameter("@EST_DD" , SqlDbType.DateTime);
				if ((!String.IsNullOrEmpty(Est_Dd)) && (Est_Dd != null))
				{
					_pt[5].Value = Convert.ToDateTime(Est_Dd);
				}
				else
				{
					_pt[5].Value = null;
				}

				if (Change)
				{
					this.ExecuteNonQuery("UPDATE TF_DYH SET QTY=@QTY,AMTN=@QTY*ISNULL(UP,0),WH=@WH,EST_DD=@EST_DD WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM=@ITM" , _pt);
				}
				else
				{
					this.ExecuteNonQuery(" UPDATE TF_DYH SET QTY_OLD=QTY,QTY=@QTY,AMTN=@QTY*ISNULL(UP,0),WH_OLD=WH,WH=@WH,EST_OLD=EST_DD,EST_DD=@EST_DD "
						                +" WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM=@ITM" , _pt);
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region 删除修改退货数量
		/// <summary>
		/// 审核删除退货数量
		/// </summary>
		/// <param name="Yh_Id"></param>
		/// <param name="Yh_No"></param>
		/// <param name="Itm"></param>
		/// <param name="tableName"></param>
		public void DelYi(string tableName,string Yh_Id , string Yh_No , int Itm )
		{
			try
			{
				SqlParameter[] _pt = new SqlParameter[3];
				_pt[0] = new SqlParameter("@YH_ID" , SqlDbType.VarChar,8);
				_pt[0].Value = Yh_Id;
				_pt[1] = new SqlParameter("@YH_NO" , SqlDbType.VarChar,38);
				_pt[1].Value = Yh_No;
				_pt[2] = new SqlParameter("@ITM" , SqlDbType.SmallInt);
				_pt[2].Value = Itm;
				string _sql = "";
				if (tableName == "TF_DYH1")
				{
					_sql = "IF EXISTS(SELECT YH_ID FROM TF_DYH1 WHERE ISNULL(QTY_OLD,0)=0 AND YH_NO = @YH_NO AND YH_ID = @YH_ID AND ITM=@ITM)\n"
						+ "BEGIN \n"
						+ "		UPDATE TF_DYH1 SET DEL_ID='T',QTY_OLD=QTY,QTY=0 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM=@ITM;\n"
						+ "		UPDATE TF_DYH SET DEL_ID='T',QTY_OLD=QTY,QTY=0 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND BOX_ITM IN (SELECT KEY_ITM FROM TF_DYH1 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM=@ITM)\n"
						+ "END \n"
						+ "ELSE \n"
						+ "BEGIN \n"
						+ "		UPDATE TF_DYH1 SET DEL_ID='T',QTY=0  WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM=@ITM;\n"
						+ "		UPDATE TF_DYH SET DEL_ID='T',QTY=0  WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND BOX_ITM IN (SELECT KEY_ITM FROM TF_DYH1 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM=@ITM)\n"
					    + "END";
				}
				else
				{
					_sql = "IF EXISTS(SELECT YH_ID FROM TF_DYH WHERE ISNULL(QTY_OLD,0)=0 AND YH_NO = @YH_NO AND YH_ID = @YH_ID AND ITM=@ITM)\n"
						+ "   UPDATE "+ tableName +" SET DEL_ID='T',QTY_OLD=QTY,QTY=0 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM=@ITM;\n"
						+ "ELSE \n"
						+ "   UPDATE "+ tableName +" SET DEL_ID='T',QTY=0 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM=@ITM;\n";
				}
				this.ExecuteNonQuery(_sql , _pt);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		//-------------------
		
		#region 修改退回数量
		/// <summary>
		/// 修改退回数量
		/// </summary>
		/// <param name="YhNo">要货退回单号</param>
		/// <param name="KeyItm">唯一项次</param>
		/// <param name="QtyRtn">退回数量</param>
		public void UpdateQtyRtn(string YhNo,int KeyItm,decimal QtyRtn)
		{
			string _sqlStr = "declare @QtyRtn numeric(38,10) \n"
				+ "declare @QtySA numeric(38,10) \n"
				+ "declare @QtyPK numeric(38,10) \n"
				+ "select @QtyRtn=isNull(A.QTY_RTN,0),@QtySA=isNull(QTY,0),@QtyPK=(Case When A.UNIT='2' Then isNull(B.PK2_QTY,0) When A.UNIT='3' Then isNull(B.PK3_QTY,0) Else 1 end ) from TF_DYH A \n"
				+ "inner join PRDT B ON A.PRD_NO=B.PRD_NO where A.YH_ID='YI' and A.YH_NO=@YhNo and A.KEY_ITM=@KeyItm \n"
				+ "if Round((@QtyRtn+(@QTY/@QtyPK)),0)>@QtySA\n"
				+ "	select 1\n"
				+ "else\n"
				+ "	begin\n"
				+ "		update TF_DYH set QTY_RTN=@QtyRtn+(@QTY/@QtyPK) where YH_ID='YI' and YH_NO=@YhNo and KEY_ITM=@KeyItm \n"
				+ "		if Exists(select YH_NO from TF_DYH where YH_ID='YI' and YH_NO=@YhNo and (isnull(QTY,0) > isnull(QTY_RTN,0))) \n"
				+ "			update MF_DYH set CLS_ID='F',BACK_ID=NULL where YH_ID='YI' and YH_NO=@YhNo and (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T') \n"
				+ "		else \n"
				+ "			update MF_DYH set CLS_ID='T',BACK_ID='YI' where YH_ID='YI' and YH_NO=@YhNo and (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T') \n"
				+ "	select 0\n"
				+ "end";
			SqlParameter [] _sqlSpara = new SqlParameter[3];
			_sqlSpara[0] = new SqlParameter("@YhNo",SqlDbType.VarChar,20);
			_sqlSpara[0].Value = YhNo;
			_sqlSpara[1] = new SqlParameter("@KeyItm",SqlDbType.Int);
			_sqlSpara[1].Value = KeyItm;
			_sqlSpara[2] = new SqlParameter("@Qty",SqlDbType.Decimal);
			_sqlSpara[2].Precision = 28;
			_sqlSpara[2].Scale = 8;
			_sqlSpara[2].Value = QtyRtn;

			int _iRow = this.ExecuteNonQuery(_sqlStr,_sqlSpara);
			if (_iRow < 1)
			{
				throw new SunlikeException("RCID=DRPYI.QTY_RTN_ERROR");//退回数量大于退回单数量！
			}
		}
		#endregion

		/// <summary>
		/// 配送退回从退回申请单截取
		/// </summary>
		/// <param name="yiNo">退回申请单单号</param>
		/// <param name="sPrdNo">轉入的貨品</param>
		/// <returns></returns>
		public SunlikeDataSet GetYi4Ib(string yiNo, string sPrdNo)
		{
			string _sSql = "SELECT YH_ID,YH_NO,ITM,KEY_ITM,PRD_NO,PRD_MARK, WH, EST_DD, QTY, UNIT, AMTN, QTY_RTN, UP, REM, QTY_OLD, WH_OLD, EST_OLD,DEL_ID "
				+ " FROM TF_DYH WHERE YH_ID = 'YI' AND YH_NO = '" + yiNo + "' AND PRD_NO IN (" + sPrdNo + ")";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sSql,_ds,null);
			return _ds;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="yiNo"></param>
		/// <returns></returns>
		public SunlikeDataSet GetMfYi(string yiNo)
		{
			string _sSql = "SELECT * FROM MF_DYH WHERE YH_ID = 'YI' AND YH_NO = @YH_NO";
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 30);
			_aryPt[0].Value = yiNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sSql,_ds,null,_aryPt);
			return _ds;
		}

		/// <summary>
		/// 取得退货明细
		/// </summary>
		/// <param name="sYiNo"></param>
		/// <returns></returns>
		public SunlikeDataSet GetYiList(string sYiNo)
		{
			string _sSql = ""
				//----退货资料
				+ " SELECT M.YH_ID,M.YH_NO, M.YH_DD, M.FX_WH, M.DEP, M.CUS_NO, C.SAL AS SAL_NO,"
				+ " M.USR, M.EST_DD, M.FUZZY_ID, M.REM,"
				+ " T.ITM, T.PRD_NO, T.PRD_MARK, T.WH,T.QTY_OLD, ISNULL(T.DEL_ID,'F') AS DEL_ID,"
				+ " (Case When isNull(T.QTY_RTN,0)=0 Then T.UNIT Else 1 End) UNIT,"
				+ " Round((isnull(T.QTY,0)-isnull(T.QTY_RTN,0))*(Case When isNull(T.QTY_RTN,0)=0 or T.UNIT='1' Then 1 When T.UNIT='2' Then B.PK2_QTY When T.UNIT='3' Then B.PK3_QTY End ),0) AS QTY,"
				+ " T.UP, T.AMTN, T.KEY_ITM, T.QTY_RTN,"
				+ " T.QTY_OLD, T.WH_OLD, T.EST_OLD"
				+ " FROM MF_DYH M, TF_DYH T, CUST C,PRDT B"
				+ " WHERE M.YH_ID = T.YH_ID AND M.YH_NO = T.YH_NO AND M.CUS_NO = C.CUS_NO "
				+ " AND (ISNULL(T.QTY,0)-ISNULL(T.QTY_RTN,0) > 0 OR ISNULL(DEL_ID,'F') ='T')"
				+ " AND T.PRD_NO=B.PRD_NO AND M.YH_ID = 'YI' AND M.YH_NO IN (" + sYiNo + ");";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sSql,_ds,null);
			DataColumn[] _dca = new DataColumn[3];
			_dca[0] = _ds.Tables[0].Columns["YH_ID"];
			_dca[1] = _ds.Tables[0].Columns["YH_NO"];
			_dca[2] = _ds.Tables[0].Columns["ITM"];
			_ds.Tables[0].PrimaryKey = _dca;
			return _ds;
		}
	}
}