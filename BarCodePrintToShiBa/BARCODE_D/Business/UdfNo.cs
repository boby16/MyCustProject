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
		/// 使用者自定义单号
		/// </summary>
		public UdfNo()
		{
		}

		/// <summary>
		/// 取自定义单号流水号
		/// </summary>
		/// <param name="pat">编码原则前置码</param>
		/// <param name="sqLen">流水号长度</param>
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
					throw new Sunlike.Common.Utility.SunlikeException("RCID=SYS.UDFNO.TOO_LONG");//自定义单号过长
				}
				else
				{
					throw _ex;
				}
			}
			return _no;
		}

		/// <summary>
		/// 写自定义单号流水号
		/// </summary>
		/// <param name="pat">编码原则前置码</param>
		/// <param name="sqLen">流水号长度</param>
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
					throw new Sunlike.Common.Utility.SunlikeException("RCID=SYS.UDFNO.TOO_LONG");//自定义单号过长
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
