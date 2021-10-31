using System;
using System.Data;
using Sunlike.Business.Data;
using System.Collections;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;


namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for Event_Log.
	/// </summary>
	public class Event_Log:Sunlike.Business.BizObject
	{
		/// <summary>
		/// creat
		/// </summary>
		public Event_Log()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetModInfo()
		{
			Sunlike.Business.Data.DbEventLog _dbEveLog = new DbEventLog(Comp.Conn_DB);
			return _dbEveLog.GetModInfo();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sql"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string _sql)
		{
			Sunlike.Business.Data.DbEventLog _dbEveLog = new DbEventLog(Comp.Conn_DB);
			return _dbEveLog.GetData(_sql);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_itm"></param>
		public void DelteData(string _itm)
		{
			Sunlike.Business.Data.DbEventLog _dbEveLog = new DbEventLog(Comp.Conn_DB);
			_dbEveLog.DelteData(_itm);
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
			Sunlike.Business.Data.DbEventLog _dbEveLog = new DbEventLog(Comp.Conn_DB);
			_dbEveLog.InsertData(ModId,ModNo,LogEvent,Rem);
		}

	}
}
