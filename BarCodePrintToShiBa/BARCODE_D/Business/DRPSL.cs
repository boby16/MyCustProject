using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
	/// <summary>
	/// ���ϼƻ�
	/// </summary>
	public class DRPSL : BizObject
	{
		#region ���캯��
		/// <summary>
		/// ���캯��
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

		#region �õ���ȷ�����ϼƻ�
		/// <summary>
		/// �õ���ȷ�����ϼƻ�
		/// </summary>
		/// <param name="usr">ȷ����</param>
		/// <returns></returns>
		public int GetScmNum(string usr)
		{
			DbDRPSL _sl = new DbDRPSL(Comp.Conn_DB);
			return _sl.GetScmNum(usr);
		}
		#endregion

		#region ȷ��/��ȷ�����ϵ�
		/// <summary>
		/// ȷ��/��ȷ�����ϵ�
		/// </summary>		
		/// <param name="slNoAry">���ϵ���</param>
		/// <param name="usr">ȷ����</param>
		/// <param name="scm">ȷ�Ϸ�</param>
		/// <returns></returns>
		public int UpdateSlScm(string[] slNoAry,string usr,bool scm)		
		{
			DbDRPSL _sl = new DbDRPSL(Comp.Conn_DB);
			int _rowCount = _sl.UpdateSlScm(slNoAry,usr,scm);

			#region �޸�Ԥ����¼
			AlertModule _am = new AlertModule();
			_am.SetAltDoc("DRP","SL",usr);
			#endregion

			return _rowCount;
		}
		#endregion

		#region ����д
		/// <summary>
		/// ����д
		/// </summary>
		/// <param name="slNo">���ϵ���</param>
		/// <param name="itm">���</param>
		/// <param name="bilId">�������</param>
		/// <param name="bilNo">��ⵥ��</param>
		public void SetFromTi(string slNo, string itm, string bilId, string bilNo)
		{
			if (!String.IsNullOrEmpty(slNo) && !String.IsNullOrEmpty(itm))
			{
				DbDRPSL _dbDrpSl = new DbDRPSL(Comp.Conn_DB);
				_dbDrpSl.SetFromTi(slNo, itm, bilId, bilNo);
			}
		}
		#endregion

		#region ɾ����ⵥʱ��ն�Ӧ�����ϼƻ���ת������
		/// <summary>
		/// ɾ����ⵥʱ��ն�Ӧ�����ϼƻ���ת������
		/// </summary>
		/// <param name="BilNo">ת�����ţ���ⵥ�ţ�</param>
		public void SetBilNoNull(string BilNo)
		{
			DbDRPSL _dbDrpSl = new DbDRPSL(Comp.Conn_DB);
			_dbDrpSl.SetBilNoNull(BilNo);
		}
		#endregion
	}
}
