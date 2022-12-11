using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
	/// <summary>
	/// 收料计划
	/// </summary>
	public class DRPSL : BizObject
	{
		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		public DRPSL()
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
		/// <param name="slNo"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string slNo)
		{
			DbDRPSL _dbSl = new DbDRPSL(Comp.Conn_DB);
			CompInfo _compInfo = Comp.GetCompInfo("");
			if (_compInfo.SystemInfo.CHK_DRP2)
			{
				return _dbSl.GetData(slNo, true);
			}
			return _dbSl.GetData(slNo, false);
		}
		#endregion

		#region 得到待确认收料计划
		/// <summary>
		/// 得到待确认收料计划
		/// </summary>
		/// <param name="usr">确认人</param>
		/// <returns></returns>
		public int GetScmNum(string usr)
		{
			DbDRPSL _sl = new DbDRPSL(Comp.Conn_DB);
			return _sl.GetScmNum(usr);
		}
		#endregion

		#region 确认/反确认收料单
		/// <summary>
		/// 确认/反确认收料单
		/// </summary>		
		/// <param name="slNoAry">收料单号</param>
		/// <param name="usr">确认人</param>
		/// <param name="scm">确认否</param>
		/// <returns></returns>
		public int UpdateSlScm(string[] slNoAry,string usr,bool scm)		
		{
			DbDRPSL _sl = new DbDRPSL(Comp.Conn_DB);
			int _rowCount = _sl.UpdateSlScm(slNoAry,usr,scm);

			#region 修改预警记录
			AlertModule _am = new AlertModule();
			_am.SetAltDoc("DRP","SL",usr);
			#endregion

			return _rowCount;
		}
		#endregion

		#region 入库回写
		/// <summary>
		/// 入库回写
		/// </summary>
		/// <param name="slNo">收料单号</param>
		/// <param name="itm">项次</param>
		/// <param name="bilId">单据类别</param>
		/// <param name="bilNo">入库单号</param>
		public void SetFromTi(string slNo, string itm, string bilId, string bilNo)
		{
			if (!String.IsNullOrEmpty(slNo) && !String.IsNullOrEmpty(itm))
			{
				DbDRPSL _dbDrpSl = new DbDRPSL(Comp.Conn_DB);
				_dbDrpSl.SetFromTi(slNo, itm, bilId, bilNo);
			}
		}
		#endregion

		#region 删除入库单时清空对应的收料计划的转出单号
		/// <summary>
		/// 删除入库单时清空对应的收料计划的转出单号
		/// </summary>
		/// <param name="BilNo">转出单号（入库单号）</param>
		public void SetBilNoNull(string BilNo)
		{
			DbDRPSL _dbDrpSl = new DbDRPSL(Comp.Conn_DB);
			_dbDrpSl.SetBilNoNull(BilNo);
		}
		#endregion
	}
}
