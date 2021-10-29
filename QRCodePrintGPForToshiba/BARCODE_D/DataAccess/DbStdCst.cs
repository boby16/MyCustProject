using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbStdCst.
	/// </summary>
	public class DbStdCst : DbObject
	{
		/// <summary>
		/// 标准成本
		/// </summary>
		/// <param name="connectionString"></param>
		public DbStdCst(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// 取标准成本
		/// </summary>
		/// <param name="wh">库位</param>
		/// <param name="prdno">品号</param>
		/// <param name="prdmark">特征</param>
		/// <param name="ijdd">日期</param>
		/// <returns></returns>
		public decimal GetUP_STD(string prdno, string prdmark, string wh, DateTime ijdd)
		{
			string _sql = "SELECT TOP 1 UP_STD FROM CST_STD WHERE PRD_NO=@PRD_NO AND PRD_MARK=@PRD_MARK AND WH=@WH AND IJ_DD<=@IJ_DD AND ISNULL(CHK_MAN,'')<>''"
					+ " ORDER BY IJ_DD DESC";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@PRD_NO",SqlDbType.VarChar,30);
			_spc[0].Value = prdno;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@PRD_MARK",SqlDbType.VarChar,40);
			_spc[1].Value = prdmark;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@WH",SqlDbType.VarChar,12);
			_spc[2].Value = wh;
			_spc[3] = new System.Data.SqlClient.SqlParameter("@IJ_DD",SqlDbType.DateTime,8);
			_spc[3].Value = ijdd;
			string[] _aryTableName = new string[] {"CST_STD"};
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,_aryTableName,_spc);
			if (!String.IsNullOrEmpty(wh) && _ds.Tables["CST_STD"].Rows.Count == 0)
			{
				return this.GetUP_STD(prdno, prdmark, "", ijdd);
			}
			else
			{
				if (_ds.Tables["CST_STD"].Rows.Count != 0)
					return Convert.ToDecimal(_ds.Tables["CST_STD"].Rows[0]["UP_STD"]);
				else
					return 0;
			}
		}

		/// <summary>
		/// 取标准成本
		/// </summary>
		/// <param name="prdNo">材料代号</param>
		/// <param name="prdMark">材料特征</param>
		/// <param name="wh">库位</param>
		/// <returns></returns>
		public decimal GetUP_STD(string prdNo,string prdMark,string wh)
		{
			string _sql = "SELECT TOP 1 UP_STD FROM CST_STD WHERE PRD_NO=@PRD_NO AND PRD_MARK=@PRD_MARK AND WH=@WH AND ISNULL(CHK_MAN,'')<>''"
				+ " ORDER BY IJ_DD DESC";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@PRD_NO",SqlDbType.VarChar,30);
			_spc[0].Value = prdNo;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@PRD_MARK",SqlDbType.VarChar,40);
			_spc[1].Value = prdMark;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@WH",SqlDbType.VarChar,12);
			_spc[2].Value = wh;
			string[] _aryTableName = new string[] {"CST_STD"};
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,_aryTableName,_spc);
			if (!String.IsNullOrEmpty(wh) && _ds.Tables["CST_STD"].Rows.Count == 0)
			{
				return this.GetUP_STD(prdNo, prdMark, "");
			}
			else
			{
				if (_ds.Tables["CST_STD"].Rows.Count != 0)
					return Convert.ToDecimal(_ds.Tables["CST_STD"].Rows[0]["UP_STD"]);
				else
					return 0;
			}
		}
	}
}
