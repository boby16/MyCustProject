using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbPrdt_PRO.
	/// </summary>
	public class DbPrdt_PRO : DbObject
	{
		/// <summary>
		/// 促销商品设定
		/// </summary>
		/// <param name="connectionString"></param>
		public DbPrdt_PRO(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// 取促销商品库资料
		/// </summary>
		/// <param name="sqlWhere"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string sqlWhere)
		{
			string _sql = "select START_DD,END_DD,PRD_NO,TYPE from PRDT_PRO";
			if(sqlWhere != null && !String.IsNullOrEmpty(sqlWhere))
			{
				_sql += " where " + sqlWhere;
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null);
			_ds.Tables[0].TableName = "PRDT_PRO";
			return _ds;
		}
	}
}
