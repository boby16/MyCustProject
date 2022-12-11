using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
namespace Sunlike.Business.Data
{
	/// <summary>
	/// 要货单管理
	/// </summary>
	public class DbDRPYH :Sunlike.Business.Data.DbObject
	{
		/// <summary>
		/// 要货单管理
		/// </summary>
		/// <param name="connStr">数据库连接字符串</param>
		public DbDRPYH(string connStr) : base(connStr)
		{
		}

		#region Old Item
		#region 取要货单TF_DYH表
		/// <summary>
		/// 取要货单TF_DYH表
		/// </summary>
		/// <param name="yh_ID">要货编号</param>
		/// <param name="yh_NO">要货单号</param>
		/// <returns>取要货单TF_DYH表</returns>
		public DataTable GetTF_DYH(string yh_ID , string yh_NO)
		{
			System.Data.DataTable _dt = new DataTable("TF_DYH");
			try
			{
				System.Data.SqlClient.SqlParameter[] _pt = new SqlParameter[2];
				_pt[0] = new SqlParameter("@YH_ID" , SqlDbType.Char , 2);
				_pt[0].Value = yh_ID;
				_pt[1] = new SqlParameter("@YH_NO" , SqlDbType.VarChar , 20);
				_pt[1].Value = yh_NO;
				_dt = this.ExecuteDataset("SELECT TF_DYH.YH_ID, TF_DYH.YH_NO, PRDT.NAME, PRDT.SNM, TF_DYH.ITM,ISNULL(TF_DYH.QTY_SO,0) AS QTY_SO,  " +
					"      TF_DYH.PRD_NO, TF_DYH.PRD_MARK, TF_DYH.WH, MY_WH.NAME AS WH_NAME,  " +
					"      TF_DYH.EST_DD, TF_DYH.QTY, TF_DYH.QTY_OLD, TF_DYH.UNIT, TF_DYH.AMTN, TF_DYH.UP,  " +
					"      PRDT.SPC,TF_DYH.DEL_ID, TF_DYH.REM, ISNULL(TF_DYH.BOX_ITM,0) AS BOX_ITM " +
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

		#region 取要货单MF_DYH表
		/// <summary>
		/// 取要货单MF_DYH表
		/// </summary>
		/// <param name="yh_ID">要货编号</param>
		/// <param name="yh_NO">要货单号</param>
		/// <param name="pCompNo"></param>
		/// <returns>取要货单MF_DYH表</returns>
		public DataTable GetMF_DYH(string yh_ID , string yh_NO, string pCompNo)
		{
			System.Data.DataTable _dt = new DataTable("MF_DYH");
			try
			{
				System.Data.SqlClient.SqlParameter[] _pt = new SqlParameter[3];
				_pt[0] = new SqlParameter("@YH_ID" , SqlDbType.Char , 2);
				_pt[0].Value = yh_ID;
				_pt[1] = new SqlParameter("@YH_NO" , SqlDbType.VarChar , 20);
				_pt[1].Value = yh_NO;
				_pt[2] = new SqlParameter("@COMPNO", SqlDbType.VarChar, 4);
				_pt[2].Value = pCompNo;
                //_dt = this.ExecuteDataset("SELECT MF_DYH.YH_ID,MF_DYH.BIL_TYPE, MF_DYH.YH_NO, MF_DYH.YH_DD, MF_DYH.CLS_DATE, MF_DYH.EST_DD, ISNULL(MF_DYH.CHK_MAN, '') AS CHK_MAN, ISNULL(MF_DYH.CLS_ID, 'F') AS CLS_ID, ISNULL(SunSystem.dbo.PSWD.NAME, '') AS NAME, MF_DYH.CUS_NO, ISNULL(CUST.NAME, '') AS CUST_NAME, MF_DYH.FX_WH, ISNULL(MF_DYH.BYBOX,'F') AS BYBOX, MF_DYH.REM FROM MF_DYH LEFT OUTER JOIN SunSystem.dbo.PSWD ON SunSystem.dbo.PSWD.COMPNO=@COMPNO AND MF_DYH.CHK_MAN = SunSystem.dbo.PSWD.USR LEFT OUTER JOIN SunSystem.dbo.PSWD CUST ON CUST.COMPNO=@COMPNO AND MF_DYH.CUS_NO = CUST.USR WHERE MF_DYH.YH_ID = @YH_ID AND MF_DYH.YH_NO = @YH_NO", _pt).Tables[0];
                _dt = this.ExecuteDataset("SELECT MF_DYH.YH_ID,MF_DYH.BIL_TYPE, MF_DYH.YH_NO, MF_DYH.YH_DD, MF_DYH.CLS_DATE, MF_DYH.EST_DD, ISNULL(MF_DYH.CHK_MAN, '') AS CHK_MAN, ISNULL(MF_DYH.CLS_ID, 'F') AS CLS_ID, ISNULL(SunSystem.dbo.PSWD.NAME, '') AS NAME, MF_DYH.CUS_NO, ISNULL(CUST.NAME, '') AS CUST_NAME, MF_DYH.FX_WH, ISNULL(MF_DYH.BYBOX,'F') AS BYBOX, MF_DYH.REM FROM MF_DYH LEFT OUTER JOIN SunSystem.dbo.PSWD ON SunSystem.dbo.PSWD.COMPNO=@COMPNO AND MF_DYH.CHK_MAN = SunSystem.dbo.PSWD.USR LEFT OUTER JOIN  CUST ON CUST.CUS_NO = MF_DYH.CUS_NO  WHERE MF_DYH.YH_ID = @YH_ID AND MF_DYH.YH_NO = @YH_NO", _pt).Tables[0];
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return _dt;
		}
		#endregion

		#region 取按箱要货的箱内容
		/// <summary>
		/// 取按箱要货的箱内容
		/// </summary>
		/// <param name="yh_No">要货单号</param>
		/// <returns></returns>
		public DataTable GetYH_Box(string yh_No)
		{
			System.Data.SqlClient.SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = yh_No; 
			return this.ExecuteDataset("SELECT TF_DYH1.YH_ID, TF_DYH1.YH_NO, TF_DYH1.ITM, TF_DYH1.PRD_NO, PRDT.NAME, TF_DYH1.CONTENT, '' AS CONTENT_SPC, TF_DYH1.QTY, ISNULL(TF_DYH1.KEY_ITM,0) AS KEY_ITM, TF_DYH1.WH, MY_WH.NAME AS WH_NAME, TF_DYH1.EST_DD,TF_DYH1.QTY_OLD FROM TF_DYH1 INNER JOIN PRDT ON TF_DYH1.PRD_NO = PRDT.PRD_NO INNER JOIN MY_WH ON TF_DYH1.WH = MY_WH.WH WHERE TF_DYH1.YH_ID = 'YH' AND TF_DYH1.YH_NO = @YH_NO" , _pt).Tables[0];
		}
		#endregion

		#region 取MF_DYH和TF_DYH和TF_DYH1表的结构
		/// <summary>
		/// 取MF_DYH和TF_DYH和TF_DYH1表的结构
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetMT()
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			try
			{
				this.FillDataset("SELECT YH_ID, YH_NO, DEP, YH_DD, CLS_ID, CLS_DATE, EST_DD, CHK_MAN, CUS_NO, USR, REM , FX_WH , BYBOX FROM MF_DYH WHERE (NULL <> NULL);SELECT YH_ID, YH_NO, ITM, PRD_NO, PRD_MARK, WH, EST_DD, QTY, UNIT, AMTN, QTY_RTN, UP, BOX_ITM, KEY_ITM FROM TF_DYH WHERE (NULL <> NULL);SELECT YH_ID, YH_NO, ITM, PRD_NO, CONTENT, QTY, KEY_ITM, WH, EST_DD FROM TF_DYH1 WHERE (NULL <> NULL)",
					_ds , new string[]{"MF_DYH" , "TF_DYH" , "TF_DYH1"});
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL" , _ds.Tables["MF_DYH"].Columns["YH_ID"] , _ds.Tables["TF_DYH"].Columns["YH_ID"]));
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL1" , _ds.Tables["MF_DYH"].Columns["YH_NO"] , _ds.Tables["TF_DYH"].Columns["YH_NO"]));
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL2" , _ds.Tables["MF_DYH"].Columns["YH_ID"] , _ds.Tables["TF_DYH1"].Columns["YH_ID"]));
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL3" , _ds.Tables["MF_DYH"].Columns["YH_NO"] , _ds.Tables["TF_DYH1"].Columns["YH_NO"]));
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

		#region 审核要货单据
		/// <summary>
		/// 审核要货单据
		/// </summary>
		/// <param name="yh_No">单据编号</param>
		/// <param name="chk_Man">审核人</param>
		/// <param name="chk_DD">审核时间</param>
		public void CheckYH(string yh_No , string chk_Man , DateTime chk_DD)
		{
			try
			{
				SqlParameter[] _pt = new SqlParameter[3];
				_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar ,20);
				_pt[0].Value = yh_No;
				_pt[1] = new SqlParameter("@CLS_DATE" , SqlDbType.DateTime);
				_pt[1].Value = chk_DD.ToString("yyyy-MM-dd HH:mm:ss");
				_pt[2] = new SqlParameter("@CHK_MAN" , SqlDbType.Char ,8);
				_pt[2].Value = chk_Man;
				this.ExecuteNonQuery("UPDATE MF_DYH SET CLS_ID = 'T' , CLS_DATE = @CLS_DATE , CHK_MAN = @CHK_MAN WHERE YH_ID = 'YH' AND YH_NO = @YH_NO" , _pt);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region 结案
		/// <summary>
		/// 结案
		/// </summary>
		/// <param name="yh_No">单号</param>
		public void CheckForSO(string yh_No)
		{
			try
			{
				SqlParameter[] _pt = new SqlParameter[3];
				_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar ,20);
				_pt[0].Value = yh_No;
				this.ExecuteNonQuery("UPDATE MF_DYH SET CLS_ID = 'T' WHERE YH_NO = @YH_NO" , _pt);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 检查是否已经结案
		/// </summary>
		/// <param name="yh_No"></param>
		/// <returns></returns>
		public bool CheckClose(string yh_No)
		{
			try
			{
				SqlParameter[] _pt = new SqlParameter[3];
				_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar ,20);
				_pt[0].Value = yh_No;
				DataTable _dt = this.ExecuteDataset("SELECT 1 FROM MF_DYH WHERE CLS_ID = 'T' AND YH_NO = @YH_NO" , _pt).Tables[0];
				if (_dt.Rows.Count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch(Exception ex)
			{
				throw ex;
				//return false;
			}
		}
		#endregion

		#region 反结案
		/// <summary>
		/// 反结案
		/// </summary>
		/// <param name="yh_No">单号</param>
		public void UnCheckForSO(string yh_No)
		{
			try
			{
				SqlParameter[] _pt = new SqlParameter[3];
				_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar ,20);
				_pt[0].Value = yh_No;
				this.ExecuteNonQuery("UPDATE MF_DYH SET CLS_ID = 'F' WHERE YH_NO = @YH_NO" , _pt);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region 审核要货单据错误回退
		/// <summary>
		/// 审核要货单据错误回退
		/// </summary>
		/// <param name="yh_No">单据编号</param>
		public void RollBackYH(string yh_No)
		{
			try
			{
				SqlParameter[] _pt = new SqlParameter[1];
				_pt[0] = new SqlParameter("@YH_NO" , SqlDbType.VarChar ,20);
				_pt[0].Value = yh_No;
				this.ExecuteNonQuery("UPDATE MF_DYH SET CLS_ID = 'F' , CLS_DATE = NULL , CHK_MAN = NULL WHERE YH_ID = 'YH' AND YH_NO = @YH_NO" , _pt);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region 取要货单
		/// <summary>
		/// 取要货单
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetYH(string yh_Id ,string yh_No)
		{
			SunlikeDataSet _ds = new SunlikeDataSet("DRPYH");
			try
			{
				SqlParameter[] _pt = new SqlParameter[2];
				_pt[0] = new SqlParameter("@YH_ID" , SqlDbType.VarChar , 20);
				_pt[0].Value = yh_Id;
				_pt[1] = new SqlParameter("@YH_NO" , SqlDbType.VarChar , 20);
				_pt[1].Value = yh_No;
				this.FillDataset("SELECT YH_ID, YH_NO, DEP, YH_DD, CLS_ID, CLS_DATE, EST_DD, CHK_MAN, CUS_NO, USR, REM , FX_WH , BYBOX,BIL_TYPE FROM MF_DYH WHERE (YH_ID = @YH_ID) AND (YH_NO = @YH_NO);SELECT YH_ID, YH_NO, ITM, PRD_NO, PRD_MARK, WH, WH_OLD, EST_DD, EST_OLD, QTY, QTY_OLD, UNIT, AMTN, QTY_RTN, UP, BOX_ITM, KEY_ITM FROM TF_DYH WHERE (YH_ID =@YH_ID) AND (ISNULL(DEL_ID,'')<>'T') AND (YH_NO = @YH_NO);SELECT YH_ID, YH_NO, ITM, PRD_NO, CONTENT, QTY, QTY_OLD, KEY_ITM, WH, WH_OLD,EST_DD FROM TF_DYH1 WHERE (YH_ID = @YH_ID) AND (YH_NO = @YH_NO) AND (ISNULL(DEL_ID,'')<>'T')",
					_ds , new string[]{"MF_DYH" , "TF_DYH" , "TF_DYH1"} , _pt);
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL" , _ds.Tables["MF_DYH"].Columns["YH_ID"] , _ds.Tables["TF_DYH"].Columns["YH_ID"]));
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL1" , _ds.Tables["MF_DYH"].Columns["YH_NO"] , _ds.Tables["TF_DYH"].Columns["YH_NO"]));
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL2" , _ds.Tables["MF_DYH"].Columns["YH_ID"] , _ds.Tables["TF_DYH1"].Columns["YH_ID"]));
				_ds.Relations.Add(new DataRelation("MF_TF_DYH_RL3" , _ds.Tables["MF_DYH"].Columns["YH_NO"] , _ds.Tables["TF_DYH1"].Columns["YH_NO"]));
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
		#endregion
	}
}
