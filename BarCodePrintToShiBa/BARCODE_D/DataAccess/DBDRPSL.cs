using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// 收料计划
	/// </summary>
	public class DbDRPSL : DbObject
	{
		#region 构造函数
		/// <summary>
		/// 采购单
		/// </summary>
		/// <param name="connectionString">SQL连接字串</param>
		public DbDRPSL(string connectionString) : base(connectionString)
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
		/// <param name="slNo">单号</param>
		/// <param name="bChk">是否走确认</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string slNo, bool bChk)
		{
			string _filter = "";
			if (bChk)
			{
				_filter = " AND ISNULL(TN.SCM_USR, '') <> ''";
			}
			string _sql = "SELECT * FROM MF_SL_NX WHERE SL_NO = @SL_NO;"
				+ " SELECT TN.SL_NO, TN.ITM, TN.SL_DD, TN.PO_ID, TN.PO_NO, TN.PO_DD, TN.PO_ITM, TN.PRD_NO,"
				+ " TN.PRD_MARK, TN.WH, TN.BAT_NO, TN.QTY_SL, TN.SCM_USR, TN.SCM_DD, TP.QTY_RK,"
				+ " ISNULL(TN.QTY_SL, 0) - ISNULL(TP.QTY_RK, 0) AS QTY_L, TP.CUS_OS_NO, TN.B_DD, TN.E_DD"
				+ " FROM"
				+ " TF_SL_NX AS TN"
				+ " LEFT JOIN"
				+ " TF_POS AS TP"
				+ " ON TN.PO_ID = TP.OS_ID AND TN.PO_NO = TP.OS_NO AND TN.PO_ITM = TP.ITM"
				+ " WHERE TN.SL_NO = @SL_NO"+_filter+"";
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@SL_NO", SqlDbType.VarChar, 20);
			_aryPt[0].Value = slNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql, _ds, new string[2]{"MF_SL_NX", "TF_SL_NX"}, _aryPt);
			
			//表头和表身关联
			DataColumn[] _dca1 = new DataColumn[1];
			_dca1[0] = _ds.Tables["MF_SL_NX"].Columns["SL_NO"];
			DataColumn[] _dca2 = new DataColumn[1];
			_dca2[0] = _ds.Tables["TF_SL_NX"].Columns["SL_NO"];
			_ds.Relations.Add("MF_SL_NXTF_SL_NX",_dca1,_dca2);
			return _ds;
		}
		#endregion

		#region 得到待确认收料计划
		/// <summary>
		/// 得到待确认收料计划
		/// </summary>
		/// <param name="usr">确认人</param>
		/// <returns></returns>
		public int GetScmNum(string usr)
		{
			int _scm = 0;
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@CUS_NO",SqlDbType.VarChar,12);
			_spc[0].Value = usr;

			string _sql = " SELECT COUNT(*) FROM MF_SL_NX A WITH (NOLOCK)"
				+" INNER JOIN TF_SL_NX B ON A.SL_NO=B.SL_NO "
				+" WHERE A.CUS_NO=@CUS_NO AND ISNULL(B.SCM_USR,'')='' ";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"TF_SL"},_spc);
			if(_ds.Tables.Contains("TF_SL"))
			{
				if(_ds.Tables["TF_SL"].Rows.Count > 0)
				{
					_scm = Convert.ToInt32(_ds.Tables["TF_SL"].Rows[0][0]);
				}
			}
			return _scm;
		}
		#endregion

		#region 确认/反确认收料单
		/// <summary>
		/// 确认/反确认收料单
		/// </summary>		
		/// <param name="slNoAry">收料单号</param>
		/// <param name="usr">确认人</param>
		/// <param name="scm">确认否</param>
		/// <returns></returns>
		public int UpdateSlScm(string[] slNoAry,string usr,bool scm)
		{
			string _sql = "UPDATE TF_SL_NX SET SCM_USR=@SCM_USR,SCM_DD=@SCM_DD ";			
			if(!scm)
			{			
				_sql = "UPDATE TF_SL_NX SET SCM_USR=null,SCM_DD=null ";
			}
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@SCM_USR",SqlDbType.VarChar,12);
			_spc[0].Value = usr;
			_spc[1] = new SqlParameter("@SCM_DD",SqlDbType.DateTime);
			_spc[1].Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			
			string _sqlWhere = " WHERE (1<>1";
			
			if(slNoAry.Length > 0)
			{
				string[] _noItm = new string[2];
				for(int i = 0;i < slNoAry.Length;i++)
				{
					_noItm = slNoAry[i].Split(new char[]{';'});
					//_sqlWhere += " OR (SL_NO='"+_noItm[0]+"' AND ITM='"+_noItm[1]+"' AND ISNULL(QTY_PC,0)=0 AND ISNULL(QTY_RK,0)=0 )";
					_sqlWhere += " OR (SL_NO='"+_noItm[0]+"' AND ITM='"+_noItm[1]+"' AND ISNULL(QTY_PC,0)=0 )";
				}				
			}
			_sqlWhere += ")";
			_sql += _sqlWhere;
			return this.ExecuteNonQuery(_sql,_spc);
		}
		#endregion

		#region 入库回写
		/// <summary>
		/// 入库回写
		/// </summary>
		/// <param name="slNo">收料单号</param>
		/// <param name="itm">项次</param>
		/// <param name="bilId">单据类别</param>
		/// <param name="bilNo">入库单号</param>
		public void SetFromTi(string slNo, string itm, string bilId, string bilNo)
		{
			string _sql = "UPDATE TF_SL_NX SET BIL_ID = @BIL_ID, BIL_NO = @BIL_NO WHERE SL_NO = @SL_NO AND ITM = @ITM";
			SqlParameter[] _aryPt = new SqlParameter[4];
			_aryPt[0] = new SqlParameter("@SL_NO", SqlDbType.VarChar, 20);
			_aryPt[0].Value = slNo;
			_aryPt[1] = new SqlParameter("@ITM", SqlDbType.Int);
			_aryPt[1].Value = Convert.ToInt32(itm);
			_aryPt[2] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
			_aryPt[2].Value = bilId;
			_aryPt[3] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
			_aryPt[3].Value = bilNo;
			this.ExecuteNonQuery(_sql, _aryPt);
		}
		#endregion

		#region 删除入库单时清空对应的收料计划的转出单号
		/// <summary>
		/// 删除入库单时清空对应的收料计划的转出单号
		/// </summary>
		/// <param name="BilNo">转出单号（入库单号）</param>
		public void SetBilNoNull(string BilNo)
		{
			string _sql = "UPDATE TF_SL_NX SET BIL_ID=NULL,BIL_NO=NULL WHERE BIL_NO=@BIL_NO";
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
			_aryPt[0].Value = BilNo;
			this.ExecuteNonQuery(_sql,_aryPt);
		}
		#endregion
	}
}
