using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbJF.
	/// </summary>
	public class DbJF : DbObject
	{
		/// <summary>
		/// 积分
		/// </summary>
		/// <param name="connectionString">SQL连接字串</param>
		public DbJF(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// 取得积分
		/// </summary>
		/// <param name="bilDD">单据日期</param>
		/// <param name="prdNo">品号</param>
		/// <param name="cardNo">会员卡号</param>
		/// <param name="amtnNet">金额</param>
		/// <param name="cusNo"></param>
		/// <returns></returns>
		public decimal GetJF(DateTime bilDD,string prdNo,string cusNo,string cardNo,decimal amtnNet)
		{
			SqlParameter[] _spc = new SqlParameter[6];
			_spc[0] = new SqlParameter("@bil_dd",SqlDbType.DateTime);
			_spc[0].Value = bilDD;
			_spc[1] = new SqlParameter("@prd_no",SqlDbType.VarChar,30);
			_spc[1].Value = prdNo;
			_spc[2] = new SqlParameter("@cus_no",SqlDbType.VarChar,12);
			_spc[2].Value = cusNo;
			_spc[3] = new SqlParameter("@card_no",SqlDbType.VarChar,20);
			_spc[3].Value = cardNo;
			_spc[4] = new SqlParameter("@amtn_net",SqlDbType.Decimal);
			_spc[4].Precision = 28;
			_spc[4].Scale = 8;
			_spc[4].Value = amtnNet;
			_spc[5] = new SqlParameter("@jf_scr",SqlDbType.Decimal);
			_spc[5].Precision = 28;
			_spc[5].Scale = 8;
			_spc[5].Direction = System.Data.ParameterDirection.Output;
			this.ExecuteSpNonQuery("usp_GetJF",_spc);
			decimal _score = Convert.ToDecimal(_spc[5].Value);
			return _score;
		}

		/// <summary>
		/// 取得积分规则
		/// </summary>
		/// <param name="jfID">规则代号</param>
		/// <param name="onlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetRule(int jfID,bool onlyFillSchema)
		{
			string _sql = "select JF_ID,REM,F_DD,E_DD,AMTN_NET,JF_SCR"
				+ " from JF_RULE"
				+ " where JF_ID=@JfID;"
				+ "select A.JF_ID,A.ITM,A.IDX_NO,B.NAME as IDX_NAME"
				+ ",A.PRD_NO,C.NAME as PRD_NAME,A.AREA_NO,E.NAME AS ARE_NAME,A.CUS_NO,F.NAME AS CUS_NAME,A.CARD_CLS,D.NAME as CARD_CLS_NAME"
				+ " from JF_RULE1 A"
				+ " left join INDX B on B.IDX_NO=A.IDX_NO"
				+ " left join PRDT C on C.PRD_NO=A.PRD_NO"
				+ " left join POSCARDTP D on D.CARD_CLS=A.CARD_CLS"
				+ " left join AREA E on E.AREA_NO=A.AREA_NO"
				+ " left join CUST F on F.CUS_NO=A.CUS_NO"
				+ " where A.JF_ID=@JfID";
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@JfID",SqlDbType.Int);
			_spc[0].Value = jfID;
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (onlyFillSchema)
			{
				this.FillDatasetSchema(_sql,_ds,new string[] {"JF_RULE","JF_RULE1"},_spc);
			}
			else
			{
				this.FillDataset(_sql,_ds,new string[] {"JF_RULE","JF_RULE1"},_spc);
			}
			//设定PK，因为用了left join以后，PK会取不到
			DataColumn[] _dca = new DataColumn[2];
			_dca[0] = _ds.Tables["JF_RULE1"].Columns["JF_ID"];
			_dca[1] = _ds.Tables["JF_RULE1"].Columns["ITM"];
			_ds.Tables["JF_RULE1"].PrimaryKey = _dca;
			//表头和表身关联
			DataColumn[] _dca1 = new DataColumn[1];
			_dca1[0] = _ds.Tables["JF_RULE"].Columns["JF_ID"];
			DataColumn[] _dca2 = new DataColumn[1];
			_dca2[0] = _ds.Tables["JF_RULE1"].Columns["JF_ID"];
			_ds.Relations.Add("JF_RULEJF_RULE1",_dca1,_dca2);
			return _ds;
		}

		/// <summary>
		/// 取得积分单
		/// </summary>
		/// <param name="jfNo">积分单号</param>
		/// <param name="onlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string jfNo,bool onlyFillSchema)
		{
            string _sql = "select JF_NO,JF_DD,CUS_NO,CARD_NO,END_DD,JF_NET,JF_CLS,CLS_ID,USR,DEP,CHK_MAN,CLS_DATE,BIL_ID,BIL_NO,SYS_DATE,REM,BACK_ID,LOCK_MAN,MOB_ID "
				+ " from MF_JF"
				+ " where JF_NO=@JfNo";
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@JfNo",SqlDbType.VarChar,20);
			_spc[0].Value = jfNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (onlyFillSchema)
			{
				this.FillDatasetSchema(_sql,_ds,new string[] {"MF_JF"},_spc);
			}
			else
			{
				this.FillDataset(_sql,_ds,new string[] {"MF_JF"},_spc);
			}
			return _ds;
		}

		/// <summary>
		/// 根据来源单信息取得积分单
		/// </summary>
		/// <param name="bilId">来源单据类别</param>
		/// <param name="bilNo">来源单号</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string bilId, string bilNo)
		{
            string _sql = "select JF_NO,JF_DD,CUS_NO,CARD_NO,END_DD,JF_NET,JF_CLS,CLS_ID,USR,DEP,CHK_MAN,CLS_DATE,BIL_ID,BIL_NO,SYS_DATE,REM,BACK_ID,LOCK_MAN,MOB_ID "
				+ " from MF_JF"
				+ " where BIL_ID = @BIL_ID AND BIL_NO = @BIL_NO";
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@BIL_ID",SqlDbType.VarChar,2);
			_spc[0].Value = bilId;
			_spc[1] = new SqlParameter("@BIL_NO",SqlDbType.VarChar,20);
			_spc[1].Value = bilNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql, _ds, new string[]{"MF_JF"}, _spc);
			return _ds;
		}

		/// <summary>
		/// 取得积分单
		/// </summary>
		/// <param name="cardNo">会员代号</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string cardNo)
		{
            string _sql = "select JF_NO,JF_DD,CUS_NO,CARD_NO,END_DD,JF_NET,JF_CLS,CLS_ID,USR,DEP,CHK_MAN,CLS_DATE,BIL_ID,BIL_NO,SYS_DATE,REM,BACK_ID,LOCK_MAN,MOB_ID "
				+ " from MF_JF"
				+ " where (JF_NET < 0 OR ISNULL(JF_CLS,0) < JF_NET) AND ISNULL(END_DD,getdate()) >= getdate() AND CARD_NO='"+cardNo+"' AND ISNULL(CLS_ID,'F')<>'T'"
				+ " order by JF_DD";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null);
			_ds.Tables[0].TableName = "MF_JF";
			return _ds;
		}

		/// <summary>
		/// 修改审核人和审核日期
		/// </summary>
		/// <param name="jfNo">积分单号</param>
		/// <param name="chkMan">审核人</param>
		/// <param name="clsDate">审核日期</param>
		public void UpdateChkMan(string jfNo,string chkMan,string clsDate)
		{
			string _sql = "update MF_JF set CHK_MAN=@ChkMan,CLS_DATE=@ClsDate where JF_NO=@JfNo";
			SqlParameter[] _spc = new SqlParameter[3];
			_spc[0] = new SqlParameter("@ChkMan",SqlDbType.VarChar,12);
			_spc[1] = new SqlParameter("@ClsDate",SqlDbType.DateTime);
			if (String.IsNullOrEmpty(chkMan))
			{
				_spc[0].Value = System.DBNull.Value;
				_spc[1].Value = System.DBNull.Value;
			}
			else
			{
				_spc[0].Value = chkMan;
				_spc[1].Value = clsDate;
			}
			_spc[2] = new SqlParameter("@JfNo",SqlDbType.VarChar,20);
			_spc[2].Value = jfNo;
			this.ExecuteNonQuery(_sql,_spc);
		}

		/// <summary>
		/// 保存结案标记
		/// </summary>
		/// <param name="clsId"></param>
		/// <param name="jfNo"></param>
		public void SaveClsID(string clsId,string jfNo)
		{
			string _sql = "update MF_JF set CLS_ID=@ClsId";
			if (clsId == "T")
				_sql += ",BACK_ID='CH' where JF_NO=@jfNo";
			else if (clsId == "F")
				_sql += ",BACK_ID=NULL where JF_NO=@jfNo";
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@ClsId",SqlDbType.VarChar,2);
			_spc[0].Value = clsId;
			_spc[1] = new SqlParameter("@jfNo",SqlDbType.VarChar,20);
			_spc[1].Value = jfNo;
			this.ExecuteNonQuery(_sql,_spc);
		}

		#region 手动更新结案
		/// <summary>
		/// 
		/// </summary>
		/// <param name="bilNo"></param>
		/// <param name="clsId"></param>
		/// <returns></returns>
		public string CloseBill(string bilNo , bool clsId )
		{
			string _result = "";
			string _sql = "Update MF_JF set CLS_ID=@ClsID where JF_NO=@JfNo and isNull(BACK_ID,'')='' ";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@JfNo",SqlDbType.VarChar,20);
			_spc[0].Value = bilNo;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@ClsID",SqlDbType.VarChar,1);
			_spc[1].Value = (clsId?"T":"F");
			
		if ( this.ExecuteNonQuery(_sql,_spc) == 0 )
			{
				if ( !clsId )
					_result = "RCID=INV.DRPJF.CLS_ERROR";	//手动反结案积分单{0}失败，因为该单是被系统自动结案的，无法反结案！
			}
			return _result;
		}
		#endregion
	}
}
