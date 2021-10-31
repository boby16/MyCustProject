using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for StdCst.
	/// </summary>
	public class StdCst : BizObject
	{
		/// <summary>
		/// 标准成本
		/// </summary>
		public StdCst()
		{
		}

		/// <summary>
		/// 取标准成本
		/// </summary>
		/// <param name="prdNo">品号</param>
		/// <param name="prdMark">特征</param>
		/// <param name="wh">库位</param>
		/// <param name="ijdd">日期</param>
		/// <returns></returns>
		public decimal GetUP_STD(string prdNo,string prdMark, string wh,DateTime ijdd)
		{
			DbStdCst _sc = new DbStdCst(Comp.Conn_DB);
			return _sc.GetUP_STD(prdNo, prdMark, wh, ijdd);
		}

		/// <summary>
		/// 取标准成本
		/// </summary>
		/// <param name="prdNo">材料代号</param>
		/// <param name="prdMark">材料特征</param>
		/// <param name="wh">库位</param>
		/// <returns></returns>
		public decimal GetUP_STD(string prdNo, string prdMark, string wh)
		{
			DbStdCst _sc = new DbStdCst(Comp.Conn_DB);
			return _sc.GetUP_STD(prdNo, prdMark, wh);
		}
	}
}
