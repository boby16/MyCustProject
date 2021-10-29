using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
	/// <summary>
	/// 托工单
	/// </summary>
	public class DRPTW : BizObject
	{
		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		public DRPTW()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region GetData
		/// <summary>
		/// GetData
		/// </summary>		
		/// <param name="twNo">单号</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string twNo)
		{
			DbDRPTW _tw = new DbDRPTW(Comp.Conn_DB);
			return _tw.GetData(twNo);
		}
		#endregion

		#region 得到托工单的配方
		/// <summary>
		/// 
		/// </summary>
		/// <param name="twNo">单号</param>
		/// <returns></returns>
		public string GetIdNo(string twNo)
		{
			DbDRPTW _dbTw = new DbDRPTW(Comp.Conn_DB);
			return _dbTw.GetIdNo(twNo);
		}
		#endregion

		#region 反写托工单
		/// <summary>
		/// 反写托工单
		/// </summary>
		/// <param name="twNo">单号</param>
		/// <param name="qtyRk">入库数量</param>
		public void SetFromTi(string twNo, decimal qtyRk)
		{
			DbDRPTW _dbDrpTw = new DbDRPTW(Comp.Conn_DB);
			_dbDrpTw.SetFromTi(twNo, qtyRk);
		}
		#endregion

		#region 得到待确认托工单
		/// <summary>
		/// 得到待确认托工单数目
		/// </summary>
		/// <param name="usr">确认人</param>
		/// <returns></returns>
		public int GetScmNum(string usr)
		{
			DbDRPTW _tw = new DbDRPTW(Comp.Conn_DB);
			return _tw.GetScmNum(usr);
		}
		#endregion

		#region 确认/反确认托工单
		/// <summary>
		/// 确认/反确认托工单
		/// </summary>		
		/// <param name="twNoAry">托工单号</param>
		/// <param name="usr">确认人</param>
		/// <param name="scm">确认否</param>
		/// <returns></returns>
		public int UpdateTwScm(string[] twNoAry,string usr,bool scm)		
		{
			DbDRPTW _tw = new DbDRPTW(Comp.Conn_DB);
			int _rowCount = _tw.UpdateTwScm(twNoAry,usr,scm);

			#region 修改预警记录
			AlertModule _am = new AlertModule();
			_am.SetAltDoc("DRP","TW",usr);
			#endregion			
			
			return _rowCount;
		}
		#endregion
	}
}
