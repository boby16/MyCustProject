using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;

namespace Sunlike.Business
{
	/// <summary>
	/// 询价单
	/// </summary>
	public class DRPQS :BizObject
	{
		/// <summary>
		/// 询价单
		/// </summary>
		public DRPQS()
		{
		}
		#region 修改询价单的采购单号
		/// <summary>
		///	 修改询价单的采购单号
		/// </summary>
		/// <param name="qtId">单据别</param>
		/// <param name="qtNo">询价单号</param>
		/// <param name="osNo">转入单号</param>
		public void UpdateOsNo(string qtId,string qtNo,string osNo)
		{
			DbDRPQS _dbQs = new DbDRPQS(Comp.Conn_DB);
			_dbQs.UpdateOsNo(qtId,qtNo,osNo);
		}
		#endregion

		#region 修改询价单的已交请购量

		/// <summary>
		///	 修改询价单的已交请购量
		/// </summary>
		/// <param name="qtId">单据别</param>
		/// <param name="qtNo">询价单号</param>
		/// <param name="itm">项次</param>
		/// <param name="qtyPo">已交请购量</param>
		public void UpdateQtyPo(string qtId,string qtNo,int itm,decimal qtyPo)
		{
			DbDRPQS _dbQs = new DbDRPQS(Comp.Conn_DB);
			_dbQs.UpdateQtyPo(qtId,qtNo,itm,qtyPo);
		}
		#endregion
	}
}
