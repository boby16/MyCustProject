using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPBT.
	/// </summary>
	public class DbBTMng : Sunlike.Business.Data.DbObject
	{
		#region	构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="connStr"></param>
		public DbBTMng(string connStr):base(connStr)
		{}
		#endregion

		#region	取得费用/收入表身记录

		/// <summary>
		/// 取得费用/收入表头记录
		/// </summary>
		/// <param name="pBB_ID">帐户识别码</param>
		/// <param name="pBB_NO">帐户变动单号</param>
		/// <returns></returns>
		public DataTable GetMF_BAC(string pBB_ID,string pBB_NO)
		{
			SqlParameter[] _ptArray = new SqlParameter[2];
			SqlParameter _ptBB_ID = new SqlParameter("@BB_ID",SqlDbType.VarChar);
			_ptBB_ID.Value = pBB_ID;
			SqlParameter _ptBB_NO = new SqlParameter("@BB_NO",SqlDbType.VarChar);
			_ptBB_NO.Value = pBB_NO;
			_ptArray[0] = _ptBB_ID;
			_ptArray[1] = _ptBB_NO;
			string _sqlStr = "SELECT BB_DD,BB_NO,BACC_NO,ACC_NO,DEP,PAY_MAN,CUR_ID,REM,SYS_DATE,CHK_MAN,CLS_DATE,AMT,AMTN FROM MF_BAC WHERE BB_ID=@BB_ID AND BB_NO=@BB_NO";
			DataTable _dt = this.ExecuteDataset(_sqlStr, _ptArray).Tables[0];
			return _dt;
		}

		/// <summary>
		/// 取得费用/收入表身记录
		/// </summary>
		/// <param name="pBB_ID">帐户识别码</param>
		/// <param name="pBB_NO">帐户变动单号</param>
		/// <returns></returns>
		public DataTable GetTF_BAC(string pBB_ID,string pBB_NO)
		{
			SqlParameter[] _ptArray = new SqlParameter[2];
			SqlParameter _ptBB_ID = new SqlParameter("@BB_ID",SqlDbType.VarChar);
			_ptBB_ID.Value = pBB_ID;
			SqlParameter _ptBB_NO = new SqlParameter("@BB_NO",SqlDbType.VarChar);
			_ptBB_NO.Value = pBB_NO;
			_ptArray[0] = _ptBB_ID;
			_ptArray[1] = _ptBB_NO;
			string _sqlStr = "SELECT ITM,ADD_ID,DEP,AMT,AMTN,RCV_MAN,ACC_NO,REM FROM TF_BAC WHERE BB_ID=@BB_ID AND BB_NO=@BB_NO";
			DataTable _dt = this.ExecuteDataset(_sqlStr,_ptArray).Tables[0];
			return _dt;
		}

		/// <summary>
		/// 取得序号
		/// </summary>
		/// <returns></returns>
		public int GetITM()
		{
			string _sqlStr = "SELECT ISNULL(MAX(ITM),0) AS ITM FROM TF_BAC";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			if (_dt.Rows.Count>0)
			{
				int _tempItm = Convert.ToInt32(_dt.Rows[0][0].ToString());
				return _tempItm+1;
			}
			else
				return 0;
		}
		#endregion

		#region	取得会计科目信息
		/// <summary>
		/// 取得会计科目信息
		/// </summary>
		/// <param name="pAccNo">科目编号</param>
		/// <returns>科目信息</returns>
		public DataTable GetAccnInfo(string pAccNo)
		{
			SqlParameter[] _ptArray = new SqlParameter[1];
			SqlParameter _ptAccNo = new SqlParameter("@ACC_NO",SqlDbType.VarChar);
			_ptAccNo.Value = pAccNo;
			_ptArray[0] = _ptAccNo;
			string _sqlStr = "SELECT * FROM ACCN WHERE ACC_NO=@ACC_NO";
			DataTable _dt = this.ExecuteDataset(_sqlStr,_ptArray).Tables[0];
			return _dt;
		}

		/// <summary>
		/// 取得会计科目名称
		/// </summary>
		/// <param name="pAccNo">科目编号</param>
		/// <returns>会计科目名称</returns>
		public string GetAccName(string pAccNo)
		{
			DataTable _dt = this.GetAccnInfo(pAccNo);
			if (_dt.Rows.Count>0)
				return _dt.Rows[0]["NAME"].ToString();
			else
				return "";
		}

		/// <summary>
		/// 取得会计科目类别
		/// </summary>
		/// <param name="pAccNo">科目编号</param>
		/// <returns></returns>
		public string GetAccArp(string pAccNo)
		{
			DataTable _dt = this.GetAccnInfo(pAccNo);
			if (_dt.Rows.Count>0)
				return _dt.Rows[0]["SW_ARP"].ToString();
			else
				return "";
		}
		#endregion

		#region	取得币别信息
		/// <summary>
		///  取得币别名称
		/// </summary>
		/// <param name="pCurId">币别号</param>
		/// <returns></returns>
		public string GetCurName(string pCurId)
		{
			SqlParameter[] _ptArray = new SqlParameter[1];
			SqlParameter _ptCurId = new SqlParameter("@CUR_ID",SqlDbType.VarChar);
			_ptCurId.Value = pCurId;
			_ptArray[0] = _ptCurId;
			string _sqlStr = "SELECT NAME FROM CUR_ID WHERE CUR_ID=@CUR_ID";
			DataTable _dt = this.ExecuteDataset(_sqlStr,_ptArray).Tables[0];
			if (_dt.Rows.Count>0)
				return _dt.Rows[0][0].ToString();
			else
				return "";
		}
		#endregion
		
		#region	取得银行帐户信息
		/// <summary>
		/// 取得银行帐户信息
		/// </summary>
		/// <param name="pBaccNo">帐户号</param>
		/// <returns></returns>
		public DataTable GetBACC(string pBaccNo)
		{
			SqlParameter[] _ptArray = new SqlParameter[1];
			SqlParameter _ptBaccNo = new SqlParameter("@BACC_NO",SqlDbType.VarChar);
			_ptBaccNo.Value = pBaccNo;
			_ptArray[0] = _ptBaccNo;
			string _sqlStr = "SELECT * FROM BACC WHERE BACC_NO=@BACC_NO";
			DataTable _dt = this.ExecuteDataset(_sqlStr,_ptArray).Tables[0];
			return _dt;
		}
		/// <summary>
		/// 取得银行帐户名称
		/// </summary>
		/// <param name="pBaccNo">帐户号</param>
		/// <returns></returns>
		public string GetBaccName(string pBaccNo)
		{
			DataTable _dt = this.GetBACC(pBaccNo);
			if (_dt.Rows.Count>0)
				return _dt.Rows[0]["NAME"].ToString();
			else
				return "";
		}
		#endregion

		#region GetDateSet
		/// <summary>
		/// 取得表结构
		/// </summary>
		/// <param name="pBB_NO"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBacDs(string pBB_NO)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			string _sqlStr = "";
			_sqlStr = "SELECT BB_ID,BB_NO,BB_DD,BACC_NO,VOH_NO,ACC_NO,DEP,PAY_MAN,CUR_ID,EXC_RTO,AMT,AMTN,REM,OPN_ID,PRT_SW,USR,CHK_MAN,CLS_DATE,SYS_DATE,BIL_TYPE FROM MF_BAC WHERE BB_ID='BT' AND BB_NO='"+pBB_NO+"'";
			_sqlStr += " SELECT BB_ID,BB_NO,BB_DD,ITM,ADD_ID,DEP,AMT,AMTN,RCV_MAN,ACC_NO,EXC_RTO,REM FROM TF_BAC WHERE BB_ID='BT' AND BB_NO='"+pBB_NO+"'";
			this.FillDataset(_sqlStr,_ds,null);
			_ds.Tables[0].TableName = "MF_BAC";
			_ds.Tables[1].TableName = "TF_BAC";
			
			//关联
			DataColumn[] _aryDcm = new DataColumn[2];
			_aryDcm[0] = _ds.Tables["MF_BAC"].Columns["BB_ID"];
			_aryDcm[1] = _ds.Tables["MF_BAC"].Columns["BB_NO"];
			DataColumn[] _aryDct = new DataColumn[2];
			_aryDct[0] = _ds.Tables["TF_BAC"].Columns["BB_ID"];
			_aryDct[1] = _ds.Tables["TF_BAC"].Columns["BB_NO"];
			_ds.Relations.Add("MF_BACTF_BAC", _aryDcm, _aryDct);

			//添加新行
			if (_ds.Tables["MF_BAC"].Rows.Count == 0)
			{
				DataRow _dr = _ds.Tables["MF_BAC"].NewRow();
				_dr["BB_ID"] = "BT";
				_dr["BB_NO"] = pBB_NO;
				_dr["AMT"] = "0.00000000";
				_dr["AMTN"] = "0.00000000";
				_dr["OPN_ID"] = "F";
				_dr["PRT_SW"] = "N";
				_ds.Tables["MF_BAC"].Rows.Add(_dr);
			}

			return _ds;
		}
		#endregion

		#region 结案
		/// <summary>
		/// 结案
		/// </summary>
		/// <param name="pBB_NO"></param>
		/// <param name="pCHK_MAN"></param>
		/// <param name="pCLS_DD"></param>
		/// <returns></returns>
		public bool Approve(string pBB_NO,string pCHK_MAN,System.DateTime pCLS_DD)
		{
			SqlParameter[] _ptArray = new SqlParameter[3];
			SqlParameter _ptBB_NO = new SqlParameter("@BB_NO",SqlDbType.VarChar);
			SqlParameter _ptCHK_MAN = new SqlParameter("@CHK_MAN",SqlDbType.VarChar);
			SqlParameter _ptCLS_DD = new SqlParameter("@CLS_DD",SqlDbType.DateTime);
			_ptBB_NO.Value = pBB_NO;
			_ptCHK_MAN.Value = pCHK_MAN;
			_ptCLS_DD.Value = pCLS_DD;
			_ptArray[0] = _ptBB_NO;
			_ptArray[1] = _ptCHK_MAN;
			_ptArray[2] = _ptCLS_DD;
			string _sqlStr = "UPDATE MF_BAC SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DD WHERE BB_NO=@BB_NO";
			if (this.ExecuteNonQuery(_sqlStr,_ptArray) > 0)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 反审核
		/// </summary>
		/// <param name="pBB_ID"></param>
		/// <param name="pBB_NO"></param>
		/// <returns></returns>
		public bool RollBack(string pBB_ID,string pBB_NO)
		{
			SqlParameter[] _ptArray = new SqlParameter[1];
			SqlParameter _ptBB_NO = new SqlParameter("@BB_NO",SqlDbType.VarChar);
			_ptBB_NO.Value = pBB_NO;
			_ptArray[0] = _ptBB_NO;
			string _sqlStr = "UPDATE MF_BAC SET CHK_MAN=NULL,CLS_DATE=NULL WHERE BB_NO=@BB_NO";
			if (this.ExecuteNonQuery(_sqlStr,_ptArray) > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 取得资讯
		/// </summary>
		/// <param name="pBB_NO"></param>
		/// <returns></returns>
		public DataTable GetAuView(string pBB_NO)
		{
			SqlParameter[] _ptArray = new SqlParameter[1];
			SqlParameter _ptBB_NO = new SqlParameter("@BB_NO",SqlDbType.VarChar);
			_ptBB_NO.Value = pBB_NO;
			_ptArray[0] = _ptBB_NO;
			string _sqlStr = "SELECT USR,CHK_MAN,CLS_DATE,PRT_SW,BIL_NO FROM MF_BAC WHERE BB_NO=@BB_NO";
			DataTable _dt = this.ExecuteDataset(_sqlStr,_ptArray).Tables[0];
			return _dt;
		}
		#endregion
	}
}
