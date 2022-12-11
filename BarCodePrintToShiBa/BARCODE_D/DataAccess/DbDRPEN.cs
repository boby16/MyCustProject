using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// 结案处理作业
	/// </summary>
	public class DbDRPEN : DbObject
	{
		#region 构造函数
		/// <summary>
		/// 结案处理作业
		/// </summary>
		/// <param name="connectionString"></param>
		public DbDRPEN(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region GetData
		/// <summary>
		///	 取数据
		/// </summary>
		/// <param name="endNo"></param>
		/// <param name="isSchema"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData( string endNo, bool isSchema)
		{
			SqlParameter[] _sqlPara = new SqlParameter[2];
			_sqlPara[0] = new SqlParameter("@END_NO",SqlDbType.VarChar,20);
			_sqlPara[0].Value = endNo;
			string _sqlStr = "";
			string _sqlWhere = "";
			if (isSchema)
			{
				_sqlWhere += " AND 1<>1 ";
			}
			_sqlStr = " SELECT "
				+ " END_NO,END_DD,BIL_ID,DEP,SAL_NO,USR,CHK_MAN,BIL_TYPE,CLS_DATE,SYS_DATE,LOCK_MAN,MOB_ID,CPY_SW,PRT_SW,REM"
				+ " FROM MF_END "
				+ " WHERE 1=1 AND END_NO=@END_NO " + _sqlWhere
				+ " ; "
				+ " SELECT "
				+ " END_NO,ITM,BIL_NO,CLS_TYPE,CLS_NAME,REM"
				+ " FROM TF_END "
				+ " WHERE 1=1 AND END_NO=@END_NO " + _sqlWhere
				+ " ORDER BY ITM ";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,_ds,new string[2]{"MF_END","TF_END"},_sqlPara);			

			//建立主键
			DataColumn[] _pkmf = new DataColumn[1];
			_pkmf[0] = _ds.Tables["MF_END"].Columns["END_NO"];			

			DataColumn[] _pktf = new DataColumn[2];
			_pktf[0] = _ds.Tables["TF_END"].Columns["END_NO"];
			_pktf[1] = _ds.Tables["TF_END"].Columns["ITM"];

			_ds.Tables["MF_END"].PrimaryKey = _pkmf;
			_ds.Tables["TF_END"].PrimaryKey = _pktf;

			//表头和表身关联
			DataColumn[] _dc1 = new DataColumn[1];
			_dc1[0] = _ds.Tables["MF_END"].Columns["END_NO"];
			DataColumn[] _dc2 = new DataColumn[1];
			_dc2[0] = _ds.Tables["TF_END"].Columns["END_NO"];
			_ds.Relations.Add("MF_ENDTF_END",_dc1,_dc2);
			return _ds;
		}
		#endregion

		#region 是否存在结案单
		/// <summary>
		///	 是否存在结案单
		/// </summary>
		/// <param name="bil_id"></param>
		/// <param name="bil_no"></param>
		/// <returns></returns>
		public bool IsExists(string bil_id,string bil_no)
		{
			string _sqlStr = " SELECT A.END_NO,A.END_DD,A.BIL_ID,A.DEP,A.SAL_NO,A.USR,A.CHK_MAN,A.CLS_DATE,A.SYS_DATE,A.LOCK_MAN,"
							+" A.MOB_ID,A.CPY_SW,A.PRT_SW,A.REM,A.BIL_TYPE FROM MF_END A"
							+" INNER JOIN TF_END B ON B.END_NO=A.END_NO "
							+" WHERE A.BIL_ID=@BIL_ID AND B.BIL_NO=@BIL_NO";												
			SqlParameter[] _sqlPara = new SqlParameter[2];
			_sqlPara[0] = new SqlParameter("@BIL_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = bil_id;
			_sqlPara[1] = new SqlParameter("@BIL_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = bil_no;
			SunlikeDataSet ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,ds,new string[] {"MF_END"},_sqlPara);
			if (ds.Tables["MF_END"].Rows.Count > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		#endregion
	}
}
