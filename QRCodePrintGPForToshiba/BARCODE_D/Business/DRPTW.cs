using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
	/// <summary>
	/// �й���
	/// </summary>
	public class DRPTW : BizObject
	{
		#region ���캯��
		/// <summary>
		/// ���캯��
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
		/// <param name="twNo">����</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string twNo)
		{
			DbDRPTW _tw = new DbDRPTW(Comp.Conn_DB);
			return _tw.GetData(twNo);
		}
		#endregion

		#region �õ��й������䷽
		/// <summary>
		/// 
		/// </summary>
		/// <param name="twNo">����</param>
		/// <returns></returns>
		public string GetIdNo(string twNo)
		{
			DbDRPTW _dbTw = new DbDRPTW(Comp.Conn_DB);
			return _dbTw.GetIdNo(twNo);
		}
		#endregion

		#region ��д�й���
		/// <summary>
		/// ��д�й���
		/// </summary>
		/// <param name="twNo">����</param>
		/// <param name="qtyRk">�������</param>
		public void SetFromTi(string twNo, decimal qtyRk)
		{
			DbDRPTW _dbDrpTw = new DbDRPTW(Comp.Conn_DB);
			_dbDrpTw.SetFromTi(twNo, qtyRk);
		}
		#endregion

		#region �õ���ȷ���й���
		/// <summary>
		/// �õ���ȷ���й�����Ŀ
		/// </summary>
		/// <param name="usr">ȷ����</param>
		/// <returns></returns>
		public int GetScmNum(string usr)
		{
			DbDRPTW _tw = new DbDRPTW(Comp.Conn_DB);
			return _tw.GetScmNum(usr);
		}
		#endregion

		#region ȷ��/��ȷ���й���
		/// <summary>
		/// ȷ��/��ȷ���й���
		/// </summary>		
		/// <param name="twNoAry">�й�����</param>
		/// <param name="usr">ȷ����</param>
		/// <param name="scm">ȷ�Ϸ�</param>
		/// <returns></returns>
		public int UpdateTwScm(string[] twNoAry,string usr,bool scm)		
		{
			DbDRPTW _tw = new DbDRPTW(Comp.Conn_DB);
			int _rowCount = _tw.UpdateTwScm(twNoAry,usr,scm);

			#region �޸�Ԥ����¼
			AlertModule _am = new AlertModule();
			_am.SetAltDoc("DRP","TW",usr);
			#endregion			
			
			return _rowCount;
		}
		#endregion
	}
}
