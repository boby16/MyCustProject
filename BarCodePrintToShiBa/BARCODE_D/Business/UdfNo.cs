using System;
using Sunlike.Business.Data;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for UdfNo.
	/// </summary>
	public class UdfNo : BizObject
	{
		/// <summary>
		/// ʹ�����Զ��嵥��
		/// </summary>
		public UdfNo()
		{
		}

		/// <summary>
		/// ȡ�Զ��嵥����ˮ��
		/// </summary>
		/// <param name="pat">����ԭ��ǰ����</param>
		/// <param name="sqLen">��ˮ�ų���</param>
		/// <returns></returns>
		public string Get(string pat,int sqLen)
		{
			DbUdfNo _udfNo = new DbUdfNo(Comp.Conn_DB);
			string _no = "";
			try
			{
				_no = _udfNo.Get(pat,sqLen);
			}
			catch(Exception _ex)
			{
				if (_ex.Message == "Too Long")
				{
					throw new Sunlike.Common.Utility.SunlikeException("RCID=SYS.UDFNO.TOO_LONG");//�Զ��嵥�Ź���
				}
				else
				{
					throw _ex;
				}
			}
			return _no;
		}

		/// <summary>
		/// д�Զ��嵥����ˮ��
		/// </summary>
		/// <param name="pat">����ԭ��ǰ����</param>
		/// <param name="sqLen">��ˮ�ų���</param>
		/// <returns></returns>
		public string Set(string pat,int sqLen)
		{
			DbUdfNo _udfNo = new DbUdfNo(Comp.Conn_DB);
			string _no = "";
			try
			{
				_no = _udfNo.Set(pat,sqLen);
			}
			catch(Exception _ex)
			{
				if (_ex.Message == "Too Long")
				{
					throw new Sunlike.Common.Utility.SunlikeException("RCID=SYS.UDFNO.TOO_LONG");//�Զ��嵥�Ź���
				}
				else
				{
					throw _ex;
				}
			}
			return _no;
		}
	}
}
