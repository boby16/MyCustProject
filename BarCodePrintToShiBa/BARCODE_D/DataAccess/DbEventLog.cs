using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbEventLog.
	/// </summary>
	public class DbEventLog:Sunlike.Business.Data.DbObject
	{
		/// <summary>
		/// creat
		/// </summary>
		/// <param name="connectinString"></param>
		public DbEventLog(string connectinString) : base(connectinString)
		{
		}
		/// <summary>
		/// 取得模块信息
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetModInfo()
		{
			string _sql = "SELECT DISTINCT MOD_ID,MOD_NAME FROM EVENT_LOG";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset( _sql,_ds,new string[] {"EVENT_LOG"});
			return _ds;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sql"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string _sql)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset( _sql,_ds,new string[] {"EVENT_LOG"});
			return _ds;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_itm"></param>
		/// <returns></returns>
		public void DelteData(string _itm)
		{
			if (!String.IsNullOrEmpty(_itm))
			{
				_itm = _itm.TrimEnd(new char[] {';'});
				string _sql = "DELETE FROM EVENT_LOG WHERE ITM IN (" + _itm.Replace("';'","','") + ")";
				this.ExecuteNonQuery( _sql );
			}
			
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ModId"></param>
		/// <param name="ModNo"></param>
		/// <param name="LogEvent"></param>
		/// <param name="Rem"></param>
		public void InsertData(string ModId,string ModNo,string LogEvent,string Rem)
		{
			SqlParameter[] _spc = new SqlParameter[5];
			_spc[0] = new SqlParameter("@MOD_ID",SqlDbType.VarChar,20);
			_spc[0].Value = ModId;
			_spc[1] = new SqlParameter("@MOD_NAME",SqlDbType.VarChar,40);
			_spc[1].Value = ModNo;
			_spc[2] = new SqlParameter("@LOG_DD",SqlDbType.DateTime);
			_spc[2].Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			_spc[3] = new SqlParameter("@LOG_LEVEL",SqlDbType.VarChar,1);
			_spc[3].Value = LogEvent;
			_spc[4] = new SqlParameter("@REM",SqlDbType.Text);
			_spc[4].Value = Rem;

			string _sql = "insert into event_log (MOD_ID,MOD_NAME,LOG_DD,LOG_LEVEL,REM) values(@MOD_ID,@MOD_NAME,@LOG_DD,@LOG_LEVEL,@REM)";
			this.ExecuteNonQuery(_sql,_spc);
		}
	}
}
