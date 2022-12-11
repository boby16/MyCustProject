using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// 调价单
	/// </summary>
	public class DbDRPTJ :Sunlike.Business.Data.DbObject
	{
		#region 构造
		/// <summary>
		/// 调价单
		/// </summary>
		/// <param name="connStr"></param>
		public DbDRPTJ(string connStr) : base(connStr)
		{

		}
		#endregion

		#region 取调价单表的结构
		/// <summary>
		/// 取调价单表的结构
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetTJ_Comp()
		{
			SunlikeDataSet _ds = new SunlikeDataSet("DRPTJ");
			this.FillDataset("SELECT TJ_NO, TJ_DD, CUS_NO, TT_NO, SAL_NO,CHK_MAN,CLS_DATE,USR,SYS_DATE,BIL_TYPE FROM MF_TJ WHERE (NULL <> NULL);"
				+" SELECT TJ_NO, ITM, PRD_NO, PRD_MARK, UPR4_OLD, UPR4, KEY_ITM, REM, AMTN_NET, QTY, AMTN_NET_FP FROM TF_TJ WHERE (NULL <> NULL)",
				_ds , new string[]{"MF_TJ" , "TF_TJ"});
			_ds.Relations.Add(new DataRelation("MF_TF_TJ_RL" , _ds.Tables["MF_TJ"].Columns["TJ_NO"] , _ds.Tables["TF_TJ"].Columns["TJ_NO"]));
			DataTable _dtBody = _ds.Tables["TF_TJ"];
			_dtBody.Columns["KEY_ITM"].AutoIncrement = true;
			_dtBody.Columns["KEY_ITM"].AutoIncrementSeed = _dtBody.Rows.Count > 0 ? Convert.ToInt32(_dtBody.Select("","KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
			_dtBody.Columns["KEY_ITM"].AutoIncrementStep = 1;
			_dtBody.Columns["KEY_ITM"].Unique = true;
			return _ds;
		}
		/// <summary>
		/// 取调价单内容
		/// </summary>
		/// <param name="tj_No"></param>
		/// <param name="OnlyFillSchema"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string tj_No,bool OnlyFillSchema)
		{			
			string _sql = " SELECT * FROM MF_TJ WHERE TJ_NO =@TJ_NO;"
						+ " SELECT * FROM TF_TJ WHERE TJ_NO=@TJ_NO";

			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@TJ_NO" , SqlDbType.VarChar , 20);
			_spc[0].Value = tj_No;

			string[] _aryTableName = new string[] {"MF_TJ","TF_TJ"};
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (OnlyFillSchema)
			{
				this.FillDatasetSchema(_sql,_ds,_aryTableName,_spc);
			}
			else
			{
				this.FillDataset(_sql,_ds,_aryTableName,_spc);
			}
			//表头和表身关联
			DataColumn[] _dca1 = new DataColumn[1];
			_dca1[0] = _ds.Tables["MF_TJ"].Columns["TJ_NO"];
			DataColumn[] _dca2 = new DataColumn[1];
			_dca2[0] = _ds.Tables["TF_TJ"].Columns["TJ_NO"];

			_ds.Relations.Add("MF_TJTF_TJ",_dca1,_dca2);
			return _ds;
		}
		/// <summary>
		/// 取调价单内容
		/// </summary>
		/// <param name="TjNo"></param>
		/// <param name="Chk"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string TjNo,string Chk)
		{			
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@TJ_NO",SqlDbType.VarChar,20);
			_spc[0].Value = TjNo;
			string _sql = " SELECT B.TJ_NO,A.TJ_DD,B.ITM,B.PRD_NO,B.PRD_MARK,A.CUS_NO,B.QTY,B.QTY_OLD,B.UPR4_OLD,B.UPR4,B.AMTN_NET FROM MF_TJ A,TF_TJ B "
				+ " WHERE A.TJ_NO=B.TJ_NO AND A.TJ_NO=@TJ_NO";
			if(Chk == "T")
			{
				_sql += " and ISNULL(A.AUD_FLAG,'F')='T'";
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"TF_TJ"},_spc);
			return _ds;
		}
		#endregion

		#region 删除调价单
		/// <summary>
		/// 删除调价单
		/// </summary>
		/// <param name="tt_No">批次调价单号</param>
		public void UpdateMfTt(string tt_No)
		{
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar ,20);
			_pt[0].Value = tt_No;
			this.ExecuteNonQuery("UPDATE MF_TT SET CHK_MAN=NULL,CLS_DATE=NULL WHERE TT_NO=@TT_NO" , _pt);
		}
		#endregion

		#region 取调价单内容
		/// <summary>
		/// 取调价单内容(WinForm)
		/// </summary>
		/// <param name="tt_No">批次调价单号</param>
		/// <returns></returns>
		public DataTable GetTJFromTT(string tt_No)
		{
			DataTable _dt = null;
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = tt_No;
			_dt = this.ExecuteDataset("SELECT MF_TJ.TT_NO, MF_TJ.TJ_NO, MF_TJ.TJ_DD, MF_TJ.CUS_NO, TF_TJ.PRD_NO, TF_TJ.PRD_MARK FROM MF_TJ INNER JOIN TF_TJ ON MF_TJ.TJ_NO = TF_TJ.TJ_NO WHERE MF_TJ.TT_NO = @TT_NO" , _pt).Tables[0];
			return _dt;
		}
		/// <summary>
		/// 取调价单内容
		/// </summary>
		/// <param name="tt_No">批次调价单号</param>
		/// <returns></returns>
		public DataTable GetMF_TJ(string tt_No)
		{
			DataTable _dt = null;
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = tt_No;
			_dt = this.ExecuteDataset("SELECT MF_TJ.TJ_NO, MF_TJ.CUS_NO, ISNULL(CUST.NAME, '') AS CUS_NAME, MF_TJ.SAL_NO, ISNULL(SALM.NAME, '') AS SAL_NAME, (CASE WHEN (ISNULL(MF_TJ.PRT_SW, 'F') = 'F') THEN 'Checked' ELSE '' END) AS PRT_SW FROM MF_TJ LEFT OUTER JOIN SALM ON MF_TJ.SAL_NO = SALM.SAL_NO LEFT OUTER JOIN CUST ON MF_TJ.CUS_NO = CUST.CUS_NO WHERE MF_TJ.TT_NO = @TT_NO" , _pt).Tables[0];
			return _dt;
		}
		#endregion

		#region 是否已有调价单被审核
		/// <summary>
		/// 是否已有调价单被审核
		/// </summary>
		/// <param name="tt_No">批次调价单</param>
		/// <returns></returns>
		public bool GetAud_FlagForTT(string tt_No)
		{
			bool _isAuditing = false;
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@TT_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = tt_No;
			DataTable _dt = this.ExecuteDataset("SELECT COUNT(*) AS TJ_NO_COUNT FROM MF_TJ WHERE (TT_NO = @TT_NO)" , _pt).Tables[0];
			foreach(DataRow _dr in _dt.Rows)
			{
				if(Convert.ToInt32(_dr["TJ_NO_COUNT"]) > 0)
				{
					_isAuditing = true;
				}
				break;
			}
			return _isAuditing;
		}
		#endregion

		#region 得到重算历史记录
		/// <summary>
		/// 得到重算历史记录
		/// </summary>
		/// <param name="PssNo">调价单号</param>
		/// <returns></returns>
		public DataTable GetTjHistory(string PssNo)
		{
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@BIL_NO" , SqlDbType.VarChar , 20);
			_pt[0].Value = PssNo;
			string _sql = "SELECT * FROM TF_TJ_H WHERE BIL_NO=@BIL_NO";
			return this.ExecuteDataset(_sql,_pt).Tables[0];
		}
		#endregion


        #region INVIK & INVLI  回写用



        /// <summary>
        /// 补收发票回写MF_BLN表头档
        /// </summary>
        /// <param name="turnId">会滙开票识别 1.明细 2.滙总</param>
        /// <param name="lzclsId">开票结案注记</param>
        
        /// <param name="amtCls">已开金额</param>
        /// <param name="amtn_netCls">已开未税金额</param>
        /// <param name="taxCls">已开税额</param> 
        /// <param name="qtyCls">已开数量</param> 
        
        /// <param name="tjNo"></param>
        public void UpdateInvIkHeadData(string turnId, string lzclsId,  decimal amtCls, decimal amtn_netCls, decimal taxCls, decimal qtyCls, string tjNo)
        {
            string _sql = "update MF_TJ set TURN_ID=@TURN_ID,LZ_CLS_ID=@LZ_CLS_ID,AMT_CLS=@AMT_CLS,AMTN_NET_CLS=@AMTN_NET_CLS,TAX_CLS=@TAX_CLS,QTY_CLS=@QTY_CLS " +
                           " Where  TJ_NO=@TJ_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[7];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TURN_ID", SqlDbType.VarChar, 1);
            _spc[0].Value = turnId;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = lzclsId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@AMT_CLS", SqlDbType.Decimal, 0);
            _spc[2].Value = amtCls;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_CLS", SqlDbType.Decimal, 0);
            _spc[3].Value = amtn_netCls;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@TAX_CLS", SqlDbType.Decimal, 0);
            _spc[4].Value = taxCls;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@QTY_CLS", SqlDbType.Decimal, 0);
            _spc[5].Value = qtyCls;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@TJ_NO", SqlDbType.VarChar, 20);
            _spc[6].Value = tjNo;
            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        ///补开发票回写来源单表身栏位
        /// </summary>
        /// <param name="amtFp">已开金额</param>
        /// <param name="amtn_netFp">已开未税金额</param>
        /// <param name="taxFp">已开税额</param>
        /// <param name="qtyFp">已开数量</param>
        /// <param name="tjNo"></param>
        /// <param name="itm">表身项次</param>
        public void UpdateInvIkBodyData(decimal amtFp, decimal amtn_netFp, decimal taxFp, decimal qtyFp, string tjNo, int itm)
        {
            string _sql = "update TF_TJ set AMT_FP=@AMT_FP,AMTN_NET_FP=@AMTN_NET_FP,TAX_FP=@TAX_FP,QTY_FP=@QTY_FP Where   TJ_NO=@TJ_NO and ITM=@ITM  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[6];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_FP", SqlDbType.Decimal);
            _spc[0].Value = amtFp;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP", SqlDbType.Decimal);
            _spc[1].Value = amtn_netFp;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@TAX_FP", SqlDbType.Decimal);
            _spc[2].Value = taxFp;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@QTY_FP", SqlDbType.Decimal);
            _spc[3].Value = qtyFp;


            _spc[4] = new System.Data.SqlClient.SqlParameter("@TJ_NO", SqlDbType.VarChar, 20);
            _spc[4].Value = tjNo;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@ITM", SqlDbType.Int);
            _spc[5].Value = itm;

            this.ExecuteNonQuery(_sql, _spc);

        }
        #endregion
	}
}
