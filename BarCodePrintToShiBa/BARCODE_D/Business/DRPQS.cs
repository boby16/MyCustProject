using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;

namespace Sunlike.Business
{
	/// <summary>
	/// ѯ�۵�
	/// </summary>
	public class DRPQS :BizObject
	{
		/// <summary>
		/// ѯ�۵�
		/// </summary>
		public DRPQS()
		{
		}
		#region �޸�ѯ�۵��Ĳɹ�����
		/// <summary>
		///	 �޸�ѯ�۵��Ĳɹ�����
		/// </summary>
		/// <param name="qtId">���ݱ�</param>
		/// <param name="qtNo">ѯ�۵���</param>
		/// <param name="osNo">ת�뵥��</param>
		public void UpdateOsNo(string qtId,string qtNo,string osNo)
		{
			DbDRPQS _dbQs = new DbDRPQS(Comp.Conn_DB);
			_dbQs.UpdateOsNo(qtId,qtNo,osNo);
		}
		#endregion

		#region �޸�ѯ�۵����ѽ��빺��

		/// <summary>
		///	 �޸�ѯ�۵����ѽ��빺��
		/// </summary>
		/// <param name="qtId">���ݱ�</param>
		/// <param name="qtNo">ѯ�۵���</param>
		/// <param name="itm">���</param>
		/// <param name="qtyPo">�ѽ��빺��</param>
		public void UpdateQtyPo(string qtId,string qtNo,int itm,decimal qtyPo)
		{
			DbDRPQS _dbQs = new DbDRPQS(Comp.Conn_DB);
			_dbQs.UpdateQtyPo(qtId,qtNo,itm,qtyPo);
		}
		#endregion
	}
}
