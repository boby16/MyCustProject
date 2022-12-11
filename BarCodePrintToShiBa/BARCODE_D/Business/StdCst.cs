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
		/// ��׼�ɱ�
		/// </summary>
		public StdCst()
		{
		}

		/// <summary>
		/// ȡ��׼�ɱ�
		/// </summary>
		/// <param name="prdNo">Ʒ��</param>
		/// <param name="prdMark">����</param>
		/// <param name="wh">��λ</param>
		/// <param name="ijdd">����</param>
		/// <returns></returns>
		public decimal GetUP_STD(string prdNo,string prdMark, string wh,DateTime ijdd)
		{
			DbStdCst _sc = new DbStdCst(Comp.Conn_DB);
			return _sc.GetUP_STD(prdNo, prdMark, wh, ijdd);
		}

		/// <summary>
		/// ȡ��׼�ɱ�
		/// </summary>
		/// <param name="prdNo">���ϴ���</param>
		/// <param name="prdMark">��������</param>
		/// <param name="wh">��λ</param>
		/// <returns></returns>
		public decimal GetUP_STD(string prdNo, string prdMark, string wh)
		{
			DbStdCst _sc = new DbStdCst(Comp.Conn_DB);
			return _sc.GetUP_STD(prdNo, prdMark, wh);
		}
	}
}
