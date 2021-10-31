using System;
using System.Data;
using System.Data.SqlClient;
namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbAccn.
	/// </summary>
	public class DbAccn	: Sunlike.Business.Data.DbObject
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
		public DbAccn(string connStr):base(connStr)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region GetData
		/// <summary>
		/// 取得会计客户资料
		/// </summary>
		/// <param name="accNo"></param>
		/// <returns></returns>
		public DataSet GetData(string accNo)
		{
			DataSet _ds = new DataSet();

			string _sqlStr = "SELECT ACC_NO,NAME,ACC_NO_UP,DC FROM ACCN ";
			string _sqlWhere = "";
			SqlParameter[] _sqlPara = new SqlParameter[1];
			if (accNo != null)
			{
				_sqlWhere += " ACC_NO=@ACC_NO";
				_sqlPara[0] = new SqlParameter("@ACC_NO",System.Data.SqlDbType.VarChar,20);
				_sqlPara[0].Value = accNo;
			}
			if (_sqlWhere != "")
			{
				 _sqlStr += " WHERE ";
			}
			_sqlStr += _sqlWhere;			
			this.FillDataset(_sqlStr, _ds, new string[]{"ACCN"},_sqlPara);			
			return _ds;

		}
		#endregion
	}
}
