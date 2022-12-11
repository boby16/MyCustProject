using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Sunlike.Business;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DBDrpYN.
	/// </summary>
	public class DBDrpYN :Sunlike.Business.Data.DbObject
	{
		#region GetData
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
		public DBDrpYN(string connStr) : base(connStr)
		{
		}
		
		/// <summary>
		/// 取得表单数据 
		/// </summary>
		/// <param name="yhId"></param>
		/// <param name="yhNo"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string yhId, string yhNo)
		{
			SqlParameter[] _sp = new SqlParameter[2];
			_sp[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 2);
			_sp[0].Value = yhId;
			_sp[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
			_sp[1].Value = yhNo;
			string _sql = "SELECT YH_ID, YH_NO, DEP, YH_DD, CLS_ID, CLS_DATE,BACK_ID, CHK_MAN, CUS_NO, REM, USR, FX_WH, WH,"
                + "BYBOX, EST_DD, FUZZY_ID, ISNULL(SAVE_ID, 'T') AS SAVE_ID,SYS_DATE,SEND_MTH,SEND_WH,ADR,BIL_TYPE,mob_id FROM MF_DYH "
				+ "WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO; "
				+ "SELECT A.YH_ID, A.YH_NO, A.ITM, A.PRD_NO,A.PRD_MARK, A.WH, A.EST_DD,A.QTY, A.UNIT, A.AMTN, A.UP, A.REM, A.BOX_ITM, A.KEY_ITM, A.QTY_OLD, A.WH_OLD, A.EST_OLD, A.DEL_ID, A.QTY_SO, "
				+ "B.SNM,B.IDX1,B.NAME AS PRD_NAME,B.SPC,B.MRK,"
				+ "(case when A.UNIT='1' then B.UT when A.UNIT='2' then B.PK2_UT when A.UNIT='3' then B.PK3_UT else A.UNIT end) as UNIT_NAME "
				+ "FROM TF_DYH A "
				+ "LEFT JOIN PRDT B ON A.PRD_NO = B.PRD_NO "
				+ "WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO";
			string[] _tableName = new string[2] { "MF_DYH", "TF_DYH"} ;
			SunlikeDataSet _ds = new SunlikeDataSet("DS_YN");
			if (!String.IsNullOrEmpty(yhNo))
			{
				this.FillDataset(_sql, _ds, _tableName, _sp);
			}
			else
			{
				this.FillDatasetSchema(_sql, _ds, _tableName, _sp);
			}
			_ds.Tables["TF_DYH"].Columns["UNIT_NAME"].ReadOnly = false;
			// Set PrimaryKey
			DataColumn[] _dca = new DataColumn[2];
			_dca[0] = _ds.Tables["MF_DYH"].Columns["YH_ID"];
			_dca[1] = _ds.Tables["MF_DYH"].Columns["YH_NO"];
			_ds.Tables["MF_DYH"].PrimaryKey = _dca;
			_dca = new DataColumn[3];
			_dca[0] = _ds.Tables["TF_DYH"].Columns["YH_ID"];
			_dca[1] = _ds.Tables["TF_DYH"].Columns["YH_NO"];
			_dca[2] = _ds.Tables["TF_DYH"].Columns["ITM"];
			_ds.Tables["TF_DYH"].PrimaryKey = _dca;

			// CreateRelation
			_ds.Tables[0].TableName = "MF_DYH";
			_ds.Tables[1].TableName = "TF_DYH";

			DataColumn[] _dc1 = new DataColumn[2] { _ds.Tables["MF_DYH"].Columns["YH_ID"], _ds.Tables["MF_DYH"].Columns["YH_NO"] } ;
			DataColumn[] _dc2 = new DataColumn[2] { _ds.Tables["TF_DYH"].Columns["YH_ID"], _ds.Tables["TF_DYH"].Columns["YH_NO"] } ;
			_ds.Relations.Add("MF_DYHTF_DYH", _dc1, _dc2);

			_ds.Tables["MF_DYH"].Columns["SAVE_ID"].ReadOnly = false;
			return _ds;
		}
 
		/// <summary>
		/// 取得要货单表身资料
		/// </summary>
		/// <param name="ynId">单据别</param>
		/// <param name="ynNoAry">单号</param>
		/// <returns>未受定完毕的要货单</returns>
		public SunlikeDataSet GetData(string ynId,string[] ynNoAry)
		{
			StringBuilder _ynNoStr = new StringBuilder();
			if(ynNoAry.Length > 0)
			{
				for(int i = 0;i < ynNoAry.Length;i++)
				{
					if(!string.IsNullOrEmpty(ynNoAry[i]))
					{
						if(i == ynNoAry.Length - 1)
						{
							_ynNoStr.Append("'"+ynNoAry[i]+"'");
						}
						else
						{
							_ynNoStr.Append("'"+ynNoAry[i]+"',");
						}
					}
				}
			}
			SqlParameter[] _sp = new SqlParameter[1];
			_sp[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 2);
			_sp[0].Value = ynId;			
			StringBuilder _sqlWhere = new StringBuilder();
			if(!string.IsNullOrEmpty(_ynNoStr.ToString()))
			{
				_sqlWhere.Append(" AND YH_NO IN ("+_ynNoStr+")");
			}
			else
			{
				_sqlWhere.Append(" AND 1<>1");
			}
			string _sql = "SELECT top 1 * FROM MF_DYH WHERE YH_ID=@YH_ID " + _sqlWhere.ToString() 
				+";SELECT * FROM TF_DYH WHERE YH_ID=@YH_ID " + _sqlWhere.ToString();
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"MF_DYH","TF_DYH"},_sp);
			return _ds;
		}

		/// <summary>
		/// 检查新品是否进入新品要货流程
		/// </summary>
		/// <param name="prdNo"></param>
		/// <returns></returns>
		public bool chkIsYn(string prdNo)
		{
			string _sql = "SELECT PRD_NO FROM MF_DYH MF "
			+ " LEFT JOIN TF_DYH TF ON"
			+ " MF.YH_ID = TF.YH_ID AND MF.YH_NO = TF.YH_NO"
			+ " WHERE MF.YH_ID = 'YN' AND MF.CLS_ID <> 'T' AND TF.PRD_NO= '"+prdNo+"'";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null);
			if ( _ds.Tables[0].Rows.Count > 0 )
			{
				return true;
			}
			return false;
		}
		#endregion

		#region Auditing
		/// <summary>
		/// 审核通过
		/// </summary>
		/// <param name="yhId">单据别</param>
		/// <param name="yhNo">单据号</param>
		/// <param name="chk_man">审核人</param>
		/// <param name="clsDd">审核日期</param>
		public void Approve(string yhId, string yhNo, string chk_man, DateTime clsDd)
		{
			SqlParameter[] _aryPt = new SqlParameter[4];
			_aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 20);
			_aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
			_aryPt[2] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
			_aryPt[3] = new SqlParameter("@CLS_DD", SqlDbType.DateTime);
			_aryPt[0].Value = yhId;
			_aryPt[1].Value = yhNo;
			_aryPt[2].Value = chk_man;
			_aryPt[3].Value = clsDd.ToString("yyyy-MM-dd HH:mm:ss");
			string _strSql = "UPDATE MF_DYH SET CHK_MAN = @CHK_MAN, CLS_DATE = @CLS_DD WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO";
			this.ExecuteNonQuery(_strSql, _aryPt);
		}
		/// <summary>
		/// 反审核
		/// </summary>
		/// <param name="yhId">单据别</param>
		/// <param name="yhNo">单据号</param>
		public void RollBack(string yhId, string yhNo)
		{
			SqlParameter[] _aryPt = new SqlParameter[2];
			_aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 20);
			_aryPt[0].Value = yhId;
			_aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = yhNo;
			string _strSql = "UPDATE MF_DYH SET CHK_MAN = null, CLS_DATE = null WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO";
			this.ExecuteNonQuery(_strSql, _aryPt);
		}
		#endregion

		#region
		/// <summary>
		/// CanSaveDs
		/// </summary>
		/// <param name="ynId"></param>
		/// <param name="prdNo"></param>
		/// <param name="pMark"></param>
		/// <param name="cusNo"></param>
		/// <returns></returns>
		public bool ChkCanSaveDs(string ynId, string prdNo,string pMark,string cusNo)
		{
			SqlParameter[] _sp = new SqlParameter[4];
			_sp[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 20);
			_sp[0].Value = ynId;
			_sp[1] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 20);
			_sp[1].Value = prdNo;
			_sp[2] = new SqlParameter("@PRD_MARK", SqlDbType.VarChar, 20);
			_sp[2].Value = pMark;
			_sp[3] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 20);
			_sp[3].Value = cusNo;
			string _sql = "SELECT T.YH_ID FROM TF_DYH T LEFT JOIN"
					+ " MF_DYH M ON T.YH_ID = M.YH_ID AND T.YH_NO = M.YH_NO"
					+ " WHERE T.YH_ID = @YH_ID AND T.PRD_NO = @PRD_NO AND T.PRD_MARK = @PRD_MARK AND M.CUS_NO = @CUS_NO";
			try
			{
                //SqlDataReader _sdr = this.ExecuteReader(_sql, _sp);
                //if (_sdr.Read())
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
                //_sdr.Close();
                return this.ExecuteScalar(_sql, _sp) == null;
			}
			catch(Exception _ex)
			{
				throw new Exception(_ex.ToString());
			}
		}
		#endregion

	}
}
