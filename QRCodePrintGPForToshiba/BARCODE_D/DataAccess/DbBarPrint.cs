using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbBarPrint.
	/// </summary>
	public class DbBarPrint: DbObject
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public DbBarPrint(string connectionString) : base(connectionString)
		{
		}

		#region 对bar_prnset操作
		/// <summary>
		/// 取得打印设置的值 注(prn_id为空则查出所有的设置)
		/// </summary>
		/// <param name="prn_id"></param>
		/// <returns></returns>
		public DataTable SelecrtBarPrnSet(string prn_id)
		{
			string _sqlStr = "";
			_sqlStr = "SELECT PRN_ID,PRN_NAME,DLL_NAME,OBJECT_URI FROM BAR_PRNSET";
			SqlParameter[] _sqlPara = new SqlParameter[1];
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (!String.IsNullOrEmpty(prn_id))
			{
				_sqlStr += " WHERE PRN_ID=@PRN_ID";
				_sqlPara[0] = new SqlParameter("@PRN_ID",SqlDbType.VarChar,10);
				_sqlPara[0].Value = prn_id;				
				
			}
			try
			{
				if (!String.IsNullOrEmpty(prn_id))
				{
					this.FillDataset(_sqlStr,_ds,new string [] {"BAR_PRNSET"},_sqlPara);
				}
				else
				{
					this.FillDataset(_sqlStr,_ds,new string [] {"BAR_PRNSET"});
				}

			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _ds.Tables[0];
		}
		#endregion

		#region 对bar_sqno操作
		/// <summary>
		/// 查询序列号档流水号
		/// </summary>
		/// <param name="prd_no"></param>
		/// <param name="prd_mark"></param>
		/// <returns></returns>
		public DataTable SelectBarSqNo(string prd_no,string prd_mark)
		{
			string _sqlStr= "";			
			_sqlStr = "SELECT PRD_NO,PRD_MARK,MAX_NO FROM BAR_SQNO WHERE 1=1 ";
			string _sqlWhere = "";
			int _count = 0;
			if (!String.IsNullOrEmpty(prd_no))
			{
				_sqlWhere += " AND PRD_NO=@PRD_NO ";
				_count += 1;
			}
			if (!String.IsNullOrEmpty(prd_mark))
			{
				 _sqlWhere += " AND PRD_MARK=@PRD_MARK";
				_count += 1;
			}
			SqlParameter[] _sqlPara = new SqlParameter[_count];
			_count = -1;
			if (!String.IsNullOrEmpty(prd_no))
			{
				_count += 1;
				_sqlPara[_count] = new SqlParameter("@PRD_NO",SqlDbType.VarChar,20);
				_sqlPara[_count].Value = prd_no;

			}
			if (!String.IsNullOrEmpty(prd_mark))
			{
				_count += 1;
				_sqlPara[_count] = new SqlParameter("@PRD_MARK",SqlDbType.VarChar,40);
				_sqlPara[_count].Value = prd_mark;
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			try
			{
				this.FillDataset(_sqlStr+_sqlWhere,_ds,new string [] {"BAR_SQNO"},_sqlPara);
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _ds.Tables[0];

		}
		/// <summary>
		/// 插入打印的最大单号
		/// </summary>
		/// <param name="prd_no"></param>
		/// <param name="prd_mark"></param>
		/// <param name="max_no"></param>
		public void InsertBarSqNo(string prd_no,string prd_mark,string max_no)
		{
			string _sqlStr = "";
			_sqlStr = "IF NOT EXISTS(SELECT PRD_NO FROM BAR_SQNO WHERE PRD_NO=@PRD_NO AND PRD_MARK=@PRD_MARK)"
					+ "		INSERT BAR_SQNO(PRD_NO,PRD_MARK,MAX_NO)VALUES(@PRD_NO,@PRD_MARK,@MAX_NO)"
					+ "ELSE"
					+ "		UPDATE BAR_SQNO SET MAX_NO=@MAX_NO WHERE PRD_NO=@PRD_NO AND PRD_MARK=@PRD_MARK";
			SqlParameter[] _sqlPara = new SqlParameter[3];
			_sqlPara[0] = new SqlParameter("@PRD_NO",SqlDbType.VarChar,20);
			_sqlPara[0].Value = prd_no;
			_sqlPara[1] = new SqlParameter("@PRD_MARK",SqlDbType.VarChar,40);
			_sqlPara[1].Value = prd_mark;
			_sqlPara[2] = new SqlParameter("@MAX_NO",SqlDbType.Int,40);
			_sqlPara[2].Value = Convert.ToInt32(max_no);
			try			
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);
			}
			catch (Exception _ex)
			{
				throw _ex;
			}

		}
		#endregion

		#region 对bar_print操作[已经打印的序列号档案]
		/// <summary>
		/// 对bar_print操作[查询已经打印的序列号档案]
		/// </summary>
		/// <param name="bar_no"></param>
		/// <returns></returns>
		public DataTable SelectBarPrint(string bar_no)
		{
			string _sqlStr= "";			
			_sqlStr = "SELECT BAR_NO,SPC_NO,PRN_DD,DEP FROM BAR_PRINT WHERE 1=1 ";
			string _sqlWhere = "";
			int _count = 0;
			if (!String.IsNullOrEmpty(bar_no))
			{
				_sqlWhere += " AND BAR_NO=@BAR_NO ";
				_count += 1;
			}
			SqlParameter[] _sqlPara = new SqlParameter[_count];
			_count = -1;
			if (!String.IsNullOrEmpty(bar_no))
			{
				_count += 1;
				_sqlPara[_count] = new SqlParameter("@BAR_NO",SqlDbType.VarChar,90);
				_sqlPara[_count].Value = bar_no;

			}
			_sqlStr += _sqlWhere;
			SunlikeDataSet _ds = new SunlikeDataSet();
			try
			{
				this.FillDataset(_sqlStr,_ds,new string [] {"BAR_PRINT"},_sqlPara);
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _ds.Tables[0];
		}
		/// <summary>
		///	 取得指定序列号的序列号档案
		/// </summary>
		/// <param name="barNos">序列号 格式: 001,002,003</param>
		/// <returns></returns>
		public DataTable GetBarPrint(string barNos)
		{
			string _sqlStr= "";			
			_sqlStr = "SELECT BAR_NO,SPC_NO,PRN_DD,DEP FROM BAR_PRINT WHERE 1=1 ";
			
			_sqlStr += " AND BAR_NO IN ('"+barNos.Replace(",","','")+"')";
			SunlikeDataSet _ds = new SunlikeDataSet();
			try
			{
				
				this.FillDataset(_sqlStr,_ds,new string [] {"BAR_PRINT"});
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _ds.Tables[0];
		}
		/// <summary>
		///  插入打印的纪录		
		/// </summary>
		/// <param name="bar_no"></param>
		/// <param name="spc_no"></param>
		/// <param name="prn_dd"></param>
		/// <param name="dep"></param>
		public void InsertBarPrint(string bar_no,string spc_no,string prn_dd,string dep)
		{
			string _sqlStr = "";
			_sqlStr = "IF NOT EXISTS(SELECT BAR_NO FROM BAR_PRINT WHERE BAR_NO=@BAR_NO)"
				+ "		INSERT BAR_PRINT(BAR_NO,SPC_NO,PRN_DD,DEP)VALUES(@BAR_NO,@SPC_NO,@PRN_DD,@DEP)"
				+ "ELSE"
				+ "		UPDATE BAR_PRINT SET SPC_NO=@SPC_NO,PRN_DD=@PRN_DD,DEP=@DEP WHERE BAR_NO=@BAR_NO";
			SqlParameter[] _sqlPara = new SqlParameter[4];
			_sqlPara[0] = new SqlParameter("@BAR_NO",SqlDbType.VarChar,90);
			_sqlPara[0].Value = bar_no;
			_sqlPara[1] = new SqlParameter("@SPC_NO",SqlDbType.VarChar,10);
			_sqlPara[1].Value = spc_no;
			_sqlPara[2] = new SqlParameter("@PRN_DD",SqlDbType.DateTime);
			_sqlPara[2].Value = Convert.ToDateTime(prn_dd);
			_sqlPara[3] = new SqlParameter("@DEP",SqlDbType.VarChar,8);
			_sqlPara[3].Value =dep;

			try			
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
		}
		#endregion

		#region 对bar_spc操作
		/// <summary>
		/// 查询规格
		/// </summary>
		/// <param name="spc_no"></param>
		/// <returns></returns>
		public DataTable SelectBar_spc(string spc_no)
		{
			string _sqlStr= "";			
			_sqlStr = "SELECT SPC_NO,NAME FROM BAR_SPC WHERE 1=1 ";
			string _sqlWhere = "";
			int _count = 0;
			if (!String.IsNullOrEmpty(spc_no))
			{
				_sqlWhere += " AND SPC_NO=@SPC_NO ";
				_count += 1;
			}
			SqlParameter[] _sqlPara = new SqlParameter[_count];
			_count = -1;
			if (!String.IsNullOrEmpty(spc_no))
			{
				_count += 1;
				_sqlPara[_count] = new SqlParameter("@SPC_NO",SqlDbType.VarChar,10);
				_sqlPara[_count].Value = spc_no;

			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			try
			{
				this.FillDataset(_sqlStr+_sqlWhere,_ds,new string [] {"BAR_SPC"},_sqlPara);
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _ds.Tables[0];
		}
		/// <summary>
		/// 新增规格
		/// </summary>
		/// <param name="spc_no"></param>
		/// <param name="name"></param>
		public void InsertBar_spc(string spc_no,string name)
		{
			string _sqlStr = "";
			_sqlStr = "INSERT BAR_SPC (SPC_NO,NAME)VALUES(@SPC_NO,@NAME);DELETE FROM BAR_SPC_DEL WHERE SPC_NO = @SPC_NO;";
			SqlParameter[] _sqlPara = new SqlParameter[2];
			_sqlPara[0] = new SqlParameter("@SPC_NO",SqlDbType.VarChar,10);
			_sqlPara[0].Value = spc_no;
			_sqlPara[1] = new SqlParameter("@NAME",SqlDbType.VarChar,90);
			_sqlPara[1].Value = name;
			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);
			}
			catch (Exception _ex)
			{
				throw _ex;
			}

		}
		/// <summary>
		/// 修改规格
		/// </summary>
		/// <param name="spc_no"></param>
		/// <param name="name"></param>
		public void UpdateBar_spc(string spc_no,string name)
		{
			string _sqlStr = "";
			_sqlStr = "UPDATE BAR_SPC SET NAME=@NAME WHERE SPC_NO=@SPC_NO";
			SqlParameter[] _sqlPara = new SqlParameter[2];
			_sqlPara[0] = new SqlParameter("@SPC_NO",SqlDbType.VarChar,10);
			_sqlPara[0].Value = spc_no;
			_sqlPara[1] = new SqlParameter("@NAME",SqlDbType.VarChar,90);
			_sqlPara[1].Value = name;
			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);
			}
			catch (Exception _ex)
			{
				throw _ex;
			}


		}
		/// <summary>
		/// 删除规格
		/// </summary>
		/// <param name="spc_no"></param>
		public void DeleteBar_spc(string spc_no)
		{
			string _sqlStr = "";
            _sqlStr = "DELETE FROM BAR_SPC WHERE SPC_NO=@SPC_NO;INSERT INTO BAR_SPC_DEL(SPC_NO) VALUES(@SPC_NO);";
			SqlParameter[] _sqlPara = new SqlParameter[1];
			_sqlPara[0] = new SqlParameter("@SPC_NO",SqlDbType.VarChar,10);
			_sqlPara[0].Value = spc_no;
			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
		}
		#endregion

		#region 保存最大号和已打印信息
		/// <summary>
		/// 
		/// </summary>
		/// <param name="barcode"></param>
		/// <param name="prd_no"></param>
		/// <param name="prd_mark"></param>
		/// <param name="spc_no"></param>
		/// <param name="dep"></param>
		/// <param name="page_count"></param>
		/// <param name="sn"></param>
		public void SavePrintData(string barcode,string prd_no,string prd_mark,string spc_no,string dep,int page_count,string sn)
		{
			string _sqlStr = "dbo.sp_SavePrintData";
			SqlParameter[] _sqlPara = new SqlParameter[7];
			_sqlPara[0] = new SqlParameter("@BARCODE",SqlDbType.VarChar,90);
			_sqlPara[0].Value = barcode;
			_sqlPara[1] = new SqlParameter("@PRD_NO",SqlDbType.VarChar,30);
			_sqlPara[1].Value = prd_no;
			_sqlPara[2] = new SqlParameter("@PRD_MARK",SqlDbType.VarChar,40);
			_sqlPara[2].Value = prd_mark;
			_sqlPara[3] = new SqlParameter("@SPC_NO",SqlDbType.VarChar,90);
			_sqlPara[3].Value = spc_no;
			_sqlPara[4] = new SqlParameter("@DEP",SqlDbType.VarChar,8);
			_sqlPara[4].Value = dep;
			_sqlPara[5] = new SqlParameter("@PAGE_COUNT",SqlDbType.Int);
			_sqlPara[5].Value = page_count;
			_sqlPara[6] = new SqlParameter("@SN",SqlDbType.VarChar,20);
			_sqlPara[6].Value = sn;
			this.ExecuteSpNonQuery(_sqlStr,_sqlPara);

		}
		#endregion

	}
}
