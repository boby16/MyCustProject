using System;
using System.Data;
using System.Data.SqlClient;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbUdfNo.
	/// </summary>
	public class DbUdfNo : DbObject
	{
		/// <summary>
		/// 使用者自定义单号
		/// </summary>
		/// <param name="connectionString">SQL连接字串</param>
		public DbUdfNo(string connectionString) : base(connectionString)
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
			SqlParameter[] _spc = new SqlParameter[3];
			_spc[0] = new SqlParameter("@pat",SqlDbType.VarChar,20);
			_spc[0].Value = pat;
			_spc[1] = new SqlParameter("@sq_len",SqlDbType.Int);
			_spc[1].Value = sqLen;
			_spc[2] = new SqlParameter("@sq",SqlDbType.VarChar,20);
			_spc[2].Direction = ParameterDirection.Output;
			this.ExecuteSpNonQuery("usp_GetUdfNo",_spc);
			string _no = _spc[2].Value.ToString();
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
			SqlParameter[] _spc = new SqlParameter[3];
			_spc[0] = new SqlParameter("@pat",SqlDbType.VarChar,20);
			_spc[0].Value = pat;
			_spc[1] = new SqlParameter("@sq_len",SqlDbType.Int);
			_spc[1].Value = sqLen;
			_spc[2] = new SqlParameter("@sq",SqlDbType.VarChar,20);
			_spc[2].Direction = ParameterDirection.Output;
			this.ExecuteSpNonQuery("usp_SetUdfNo",_spc);
			string _no = _spc[2].Value.ToString();
			return _no;
		}
	}
}