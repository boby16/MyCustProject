using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPBR.
	/// </summary>
	public class DbDRPBR : DbObject
	{
		#region 构造
		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="connectionString"></param>
		public DbDRPBR(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region GetData
		/// <summary>
		/// 
		/// </summary>
		/// <param name="brNo"></param>
		/// <param name="OnlyFillSchema"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string brNo,bool OnlyFillSchema)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@BR_NO",SqlDbType.VarChar,20);
			_pt[0].Value = brNo;
			string _sql = " SELECT * FROM MF_BAR WHERE BR_NO=@BR_NO;"
						+ " SELECT * FROM TF_BAR WHERE BR_NO=@BR_NO";
			if (!OnlyFillSchema)
			{
				this.FillDataset(_sql,_ds,new string[]{"MF_BAR","TF_BAR"},_pt);
			}
			else
			{
				this.FillDatasetSchema(_sql,_ds,new string[]{"MF_BAR","TF_BAR"},_pt);
			}
			_ds.Relations.Add(new DataRelation("MF_BAR_TF_BAR", _ds.Tables[0].Columns["BR_NO"],_ds.Tables[1].Columns["BR_NO"]));			
			return _ds;
		}
		#endregion

		#region 修改终审人
		/// <summary>
		/// 修改终审人
		/// </summary>
		/// <param name="brNo">单号</param>
		/// <param name="chkMan">终审人</param>
		/// <param name="clsDate">终审日期</param>
		public void UpdateChkMan(string brNo,string chkMan,DateTime clsDate)
		{
			string _sql = "UPDATE MF_BAR SET CHK_MAN=@chkMan,CLS_DATE=@clsDate WHERE BR_NO=@brNo";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@chkMan",SqlDbType.VarChar,12);
			if (String.IsNullOrEmpty(chkMan))
			{
				_spc[0].Value = System.DBNull.Value;
			}
			else
			{
				_spc[0].Value = chkMan;
			}
			_spc[1] = new System.Data.SqlClient.SqlParameter("@clsDate",SqlDbType.DateTime);
			if (String.IsNullOrEmpty(chkMan))
			{
				_spc[1].Value = System.DBNull.Value;
			}
			else
			{
				_spc[1].Value = clsDate.ToString("yyyy-MM-dd HH:mm:ss");
			}
			_spc[2] = new System.Data.SqlClient.SqlParameter("@brNo",SqlDbType.VarChar,20);
			_spc[2].Value = brNo;
			this.ExecuteNonQuery(_sql,_spc);
		}
		#endregion
	}
}
