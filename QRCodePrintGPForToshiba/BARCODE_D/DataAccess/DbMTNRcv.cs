using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// 维修产品收件单

	/// </summary>
	public class DbMTNRcv : DbObject
	{
		#region Constructor
		/// <summary>
		/// 维修产品收件单

		/// </summary>
		/// <param name="connectionString"></param>
		public DbMTNRcv(string connectionString) : base(connectionString)
		{
		}
		#endregion

		#region Method

		#region 取得维修产品收件单

		/// <summary>
		/// 维修产品收件单

		/// </summary>
		/// <param name="rv_Id">收件/收件退回单</param>
		/// <param name="rv_No">单号</param>
		/// <param name="onlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string rv_Id, string rv_No, bool onlyFillSchema)
		{
			StringBuilder _sql = new StringBuilder();
			_sql.Append("SELECT A.RV_ID,A.RV_NO,A.RV_DD,A.DEP_RCV,A.CUS_NO,A.BIL_TYPE,A.SAL_NO,A.DEP_SEND,A.RCV_TYPE,A.IJ_ID,A.IJ_NO,A.CLS_ID,A.REM,A.USR,A.SYS_DATE,A.CHK_MAN,A.CLS_DATE,A.MOB_ID,A.LOCK_MAN,A.PRT_SW,A.PRT_USR,A.CPY_SW,A.MA_ID,A.MA_NO,A.CNT_NO,A.CNT_REM,A.CLS_AUTO,A.EST_DD, ")
                .Append("B.NAME CUS_NAME,A.CNT_NAME,A.TEL_NO,A.CELL_NO,A.CNT_ADR,A.OTH_NAME,A.CNT_REM,D.VOH_ID,D.VOH_NO ")
				.Append("FROM MF_RCV A ")
				.Append("LEFT JOIN CUST B ON B.CUS_NO=A.CUS_NO ")
                .Append("LEFT JOIN CONTACT C ON C.CUS_NO=A.CUS_NO AND C.CNT_NO=A.CNT_NO ")
                .Append("LEFT JOIN MF_IJ D ON D.IJ_ID=A.IJ_ID AND D.IJ_NO=A.IJ_NO ")
				.Append("WHERE A.RV_ID=@RV_ID AND A.RV_NO=@RV_NO;")
                .Append("SELECT ISNULL(B.QTY, 0) - ISNULL(B.QTY_MTN, 0)+ISNULL(A.QTY,0) AS QTY_RV_ORG, A.RV_ID,A.RV_NO,A.ITM,A.KEY_ITM,A.PRD_NO,A.PRD_NAME,A.UNIT,A.WH,A.PRD_MARK,A.WC_NO,A.QTY,A.MTN_DD,A.MTN_ALL_ID,A.RTN_DD,A.REM,A.MA_ID,A.MA_NO,A.MA_ITM,A.BAT_NO,A.VALID_DD,A.SA_NO,A.SA_ITM,A.QTY_SO,A.QTY_RR,A.MTN_TYPE,A.EST_DD,P.SPC ")
				.Append("FROM TF_RCV A LEFT JOIN TF_MA B ON B.MA_ID = A.MA_ID AND B.MA_NO = A.MA_NO AND B.KEY_ITM = A.MA_ITM ")
                .Append("LEFT JOIN PRDT P ON P.PRD_NO = A.PRD_NO ")
				.Append("WHERE A.RV_ID=@RV_ID AND A.RV_NO=@RV_NO");
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@RV_ID", SqlDbType.VarChar, 2);
			_spc[0].Value = rv_Id;
			_spc[1] = new SqlParameter("@RV_NO", SqlDbType.VarChar, 20);
			_spc[1].Value = rv_No;
			string[] _aryTableName = new string[] { "MF_RCV", "TF_RCV" };
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (onlyFillSchema)
				this.FillDatasetSchema(_sql.ToString(), _ds, _aryTableName, _spc);
			else
				this.FillDataset(_sql.ToString(), _ds, _aryTableName, _spc);

			if (_ds.Tables["MF_RCV"].Columns.Contains("CUS_NAME"))
				_ds.Tables["MF_RCV"].Columns["CUS_NAME"].ReadOnly = false;
			DataColumn[] _dcPk = null;
			_dcPk = new DataColumn[2];
			_dcPk[0] = _ds.Tables["MF_RCV"].Columns["RV_ID"];
			_dcPk[1] = _ds.Tables["MF_RCV"].Columns["RV_NO"];
			_ds.Tables["MF_RCV"].PrimaryKey = _dcPk;
			_dcPk = new DataColumn[3];
			_dcPk[0] = _ds.Tables["TF_RCV"].Columns["RV_ID"];
			_dcPk[1] = _ds.Tables["TF_RCV"].Columns["RV_NO"];
			_dcPk[2] = _ds.Tables["TF_RCV"].Columns["ITM"];
			_ds.Tables["TF_RCV"].PrimaryKey = _dcPk;

			DataColumn[] _dcHead = new DataColumn[2];
			_dcHead[0] = _ds.Tables["MF_RCV"].Columns["RV_ID"];
			_dcHead[1] = _ds.Tables["MF_RCV"].Columns["RV_NO"];
			DataColumn[] _dcBody = new DataColumn[2];
			_dcBody[0] = _ds.Tables["TF_RCV"].Columns["RV_ID"];
			_dcBody[1] = _ds.Tables["TF_RCV"].Columns["RV_NO"];
			_ds.Relations.Add("MF_RCVTF_RCV", _dcHead, _dcBody);
			return _ds;
		}
		#endregion

		#region 修改审核人和审核日期
		/// <summary>
		/// 修改审核人和审核日期
		/// </summary>
		/// <param name="rv_No">单号</param>
		/// <param name="chk_Man">审核人</param>
		/// <param name="cls_Date">审核日期</param>
		public void UpdateChkMan(string rv_No, string chk_Man, DateTime cls_Date)
		{
			string _sql = "UPDATE MF_RCV SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE RV_NO=@RV_NO";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
			_spc[1] = new System.Data.SqlClient.SqlParameter("@CLS_DATE", SqlDbType.DateTime);
			_spc[2] = new System.Data.SqlClient.SqlParameter("@RV_NO", SqlDbType.VarChar, 38);
			_spc[2].Value = rv_No;
			if (chk_Man.Length == 0)
			{
				_spc[0].Value = System.DBNull.Value;
				_spc[1].Value = System.DBNull.Value;
			}
			else
			{
				_spc[0].Value = chk_Man;
				_spc[1].Value = cls_Date.ToString("yyyy-MM-dd");
			}
			this.ExecuteNonQuery(_sql, _spc);
		}
		#endregion

		#region 结案
		/// <summary>
		/// 结案
		/// </summary>
		/// <param name="rv_Id">收件/收件退回</param>
		/// <param name="rv_No">单号</param>
		/// <param name="isClose">结案否</param>
		public void CloseBill(string rv_Id,string rv_No, bool isClose)
		{
			SqlParameter[] _params = new SqlParameter[2];
			_params[0] = new SqlParameter("@RV_ID", SqlDbType.VarChar, 2);
			_params[0].Value = rv_Id;
			_params[1] = new SqlParameter("@RV_NO", SqlDbType.VarChar, 20);
			_params[1].Value = rv_No;
			string _sqlStr = "UPDATE MF_RCV SET CLS_ID = " + (isClose ? "'T'" : "NULL") + " WHERE RV_ID=@RV_ID AND RV_NO=@RV_NO";
			this.ExecuteNonQuery(_sqlStr, _params);
		}
		#endregion

		#region 根据收件单单号取得调整单单号
		/// <summary>
		/// 根据收件单单号取得调整单单号
		/// </summary>
		/// <param name="rv_Id">收件/收件退回</param>
		/// <param name="rv_No">收件单单号</param>
		/// <returns></returns>
		public string GetIJ_NO(string rv_Id,string rv_No)
		{
			string _sqlStr = "SELECT IJ_NO FROM MF_RCV WHERE RV_ID=@RV_ID AND RV_NO=@RV_NO";
			SqlParameter[] _params = new SqlParameter[2];
			_params[0] = new SqlParameter("@RV_ID", SqlDbType.VarChar, 2);
			_params[0].Value = rv_Id;
			_params[1] = new SqlParameter("@RV_NO", SqlDbType.VarChar, 20);
			_params[1].Value = rv_No;
			DataSet _ds = this.ExecuteDataset(_sqlStr, _params);
			string _ij_No = String.Empty;
			if (_ds.Tables[0].Rows.Count > 0)
				_ij_No = _ds.Tables[0].Rows[0][0].ToString();
			return _ij_No;
		}
		#endregion

		#region 取得表身数据
		/// <summary>
		/// 取得表身数据
		/// </summary>
		/// <param name="rvId">单据别</param>
		/// <param name="rvNo">单号</param>
		/// <param name="key_itm">单据唯一项次</param>
		/// <returns></returns>
		public SunlikeDataSet GetBody(string rvId, string rvNo, int key_itm)
		{
			SqlParameter[] _sqlPara = new SqlParameter[3];
			_sqlPara[0] = new SqlParameter("@RV_ID", SqlDbType.VarChar, 2);
			_sqlPara[0].Value = rvId;
			_sqlPara[1] = new SqlParameter("@RV_NO", SqlDbType.VarChar, 20);
			_sqlPara[1].Value = rvNo;
			_sqlPara[2] = new SqlParameter("@KEY_ITM", System.Data.SqlDbType.Int);
			_sqlPara[2].Value = key_itm;
			string _sqlStr = "SELECT A.RV_ID,A.RV_NO,A.ITM,A.KEY_ITM,A.PRD_NO,A.UNIT,A.WH,A.PRD_MARK,A.WC_NO,A.QTY,A.MTN_DD,A.MTN_ALL_ID,A.RTN_DD,"
						+ " A.REM,A.MA_ID,A.MA_NO,A.MA_ITM,A.BAT_NO,A.VALID_DD,A.SA_NO,A.SA_ITM,A.QTY_SO,A.QTY_RR,A.MTN_TYPE,A.EST_DD"
						+ " FROM TF_RCV A WHERE A.RV_ID=@RV_ID AND A.RV_NO=@RV_NO AND A.KEY_ITM=@KEY_ITM ";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr, _ds, new string[1] { "TF_RCV" }, _sqlPara);
			return _ds;
		}
		#endregion 

		#region 更新收件单的[调整单据别]、[调整单号]
		/// <summary>
		/// 更新收件单的[调整单据别]、[调整单号]
		/// </summary>
		/// <param name="rv_id">收件单据别</param>
		/// <param name="rv_no">收件单号</param>
		/// <param name="ij_id">调整单据别</param>
		/// <param name="ij_no">调整单号</param>
		public void UpdateIJ_NO(string rv_id,string rv_no,string ij_id,string ij_no)
		{
			string _sql = "UPDATE MF_RCV SET IJ_ID=@IJ_ID,IJ_NO=@IJ_NO WHERE RV_ID=@RV_ID AND RV_NO=@RV_NO";
			SqlParameter[] _spc = new SqlParameter[4];
			_spc[0] = new SqlParameter("@RV_ID", SqlDbType.VarChar, 12);
			_spc[1] = new SqlParameter("@RV_NO", SqlDbType.VarChar, 20);
			_spc[2] = new SqlParameter("@IJ_ID", SqlDbType.VarChar, 12);
			_spc[3] = new SqlParameter("@IJ_NO", SqlDbType.VarChar, 20);
			_spc[0].Value = rv_id;
			_spc[1].Value = rv_no;
			_spc[2].Value = ij_id;
			_spc[3].Value = ij_no;
			this.ExecuteNonQuery(_sql, _spc);
		}
		#endregion

		#region 回写维修申请单

		/// <summary>
		/// 回写维修申请单

		/// </summary>
		/// <param name="os_Id">申请单据别</param>
		/// <param name="os_No">申请单号</param>
		/// <param name="itm">申请项次</param>
		/// <param name="qty">回写的数量</param>
		public void UpdateMA(string os_Id, string os_No, string itm, decimal qty)
		{
			string sql = " UPDATE TF_MA SET QTY_MTN=ISNULL(QTY_MTN,0)+@QTY WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND KEY_ITM=@ITM ;"
			+ " IF (@@ROWCOUNT > 0) "
			+ " BEGIN \n"
			+ " UPDATE MF_MA SET CLS_ID='T',CLS_AUTO='T' WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND (SELECT COUNT(MA_ID) FROM TF_MA WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND ISNULL(QTY,0)-ISNULL(QTY_MTN,0)>0)=0;"
			+ " UPDATE MF_MA SET CLS_ID='F',CLS_AUTO='F' WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND (SELECT COUNT(MA_ID) FROM TF_MA WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND ISNULL(QTY,0)-ISNULL(QTY_MTN,0)>0)>0;"
			+ " END ";
			SqlParameter[] _spc = new SqlParameter[4];
			_spc[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 12);
			_spc[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
			_spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
			_spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
			_spc[0].Value = os_Id;
			_spc[1].Value = os_No;
			_spc[2].Value = Convert.ToInt16(itm);
			_spc[3].Value = qty;
			this.ExecuteNonQuery(sql, _spc);
		}
		#endregion

		#region 回写保修卡[产品现状]
		/// <summary>
		/// 回写保修卡[产品现状]
		/// </summary>
		/// <param name="wc_no">保修卡号</param>
		/// <param name="wc_id">产品现状</param>
		public void UpdateWC(string wc_no, string wc_id)
		{
			string sql = " UPDATE MF_WC SET WC_ID=@WC_ID WHERE WC_NO=@WC_NO";
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@WC_NO", SqlDbType.VarChar, 25);
			_spc[1] = new SqlParameter("@WC_ID", SqlDbType.VarChar, 1);
			_spc[0].Value = wc_no;
			_spc[1].Value = wc_id;
			this.ExecuteNonQuery(sql, _spc);
		}
		#endregion

		#region 修改已退数量
		/// <summary>
		/// 修改已退数量
		/// </summary>
		/// <param name="RvID">单据代号</param>
		/// <param name="RvNo">单据号码</param>
		/// <param name="Itm">项次</param>
		/// <param name="Qty">退回数量</param>
		public void UpdateQtyRtn(string RvID, string RvNo, int Itm, decimal Qty)
		{
			string _sql = "update TF_RCV set QTY_RR=isNull(QTY_RR,0)+@QTY where RV_ID=@RvID and RV_NO=@RvNo and KEY_ITM=@Itm \n"
                    + " if not Exists(select RV_NO from MF_RCV WHERE RV_ID=@RvID AND RV_NO=@RvNo AND ISNULL(CLS_ID,'')='T' AND ISNULL(CLS_AUTO,'')<>'T') \n"//如果是手动结案，则不修改结案状态

                    + "	    if Exists(select RV_NO from TF_RCV where RV_ID=@RvID and RV_NO=@RvNo and (isnull(QTY,0)-isnull(QTY_SO,0)-isnull(QTY_RR,0)>0)) \n"
					+ "	        update MF_RCV set CLS_ID='F',CLS_AUTO='F' where RV_ID=@RvID and RV_NO=@RvNo \n"
					+ "	    else \n"
					+ "		    update MF_RCV set CLS_ID='T',CLS_AUTO='T' where RV_ID=@RvID and RV_NO=@RvNo \n";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@Qty", SqlDbType.Decimal);
			_spc[0].Precision = 38;
			_spc[0].Scale = 10;
			_spc[0].Value = Qty;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@RvID", SqlDbType.Char, 2);
			_spc[1].Value = RvID;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@RvNo", SqlDbType.VarChar, 20);
			_spc[2].Value = RvNo;
			_spc[3] = new System.Data.SqlClient.SqlParameter("@Itm", SqlDbType.SmallInt);
			_spc[3].Value = Itm;
			this.ExecuteNonQuery(_sql, _spc);
		}

		#endregion

        #region 修改已送修量

        /// <summary>
        /// 修改已送修量

        /// </summary>
        /// <param name="rvId">单据代号</param>
        /// <param name="rvNo">单据号码</param>
        /// <param name="keyItm">项次</param>
        /// <param name="Qty">退回数量</param>
        public void UpdateQtySo(string rvId, string rvNo, int keyItm, decimal Qty)
        {
            string _sql = "update TF_RCV set QTY_SO=isNull(QTY_SO,0)+@QTY_SO where RV_ID=@RvID and RV_NO=@RvNo and KEY_ITM=@KEY_ITM \n"
                    + " if not Exists(select RV_NO from MF_RCV WHERE RV_ID=@RvID AND RV_NO=@RvNo AND ISNULL(CLS_ID,'')='T' AND ISNULL(CLS_AUTO,'')<>'T') \n"
                    + "		if Exists(select RV_NO from TF_RCV where RV_ID=@RvID and RV_NO=@RvNo and (isnull(QTY,0) -isnull(QTY_SO,0)-isnull(QTY_RR,0) > 0 )) \n"
                    + "			update MF_RCV set CLS_ID='F',CLS_AUTO='F' where RV_ID=@RvID and RV_NO=@RvNo \n"
                    + "		else \n"
                    + "			update MF_RCV set CLS_ID='T',CLS_AUTO='T' where RV_ID=@RvID and RV_NO=@RvNo \n";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@QTY_SO", SqlDbType.Decimal);
            _spc[0].Precision = 38;
            _spc[0].Scale = 10;
            _spc[0].Value = Qty;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@RvID", SqlDbType.Char, 2);
            _spc[1].Value = rvId;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@RvNo", SqlDbType.VarChar, 20);
            _spc[2].Value = rvNo;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@KEY_ITM", SqlDbType.SmallInt);
            _spc[3].Value = keyItm;
            this.ExecuteNonQuery(_sql, _spc);
        }

        #endregion

		#endregion

		#region 取申请单表身数据
		/// <summary>
		/// 取申请单表身数据
		/// </summary>
		/// <param name="maId">单据别</param>
		/// <param name="maNo">申请单号</param>
		/// <param name="key_itm">单据唯一项次</param>
		/// <returns></returns>
		public SunlikeDataSet GetMaQty(string maId, string maNo, int key_itm)
		{
			SqlParameter[] _sqlPara = new SqlParameter[3];
			_sqlPara[0] = new SqlParameter("@MA_ID", SqlDbType.VarChar, 2);
			_sqlPara[0].Value = maId;
			_sqlPara[1] = new SqlParameter("@MA_NO", SqlDbType.VarChar, 20);
			_sqlPara[1].Value = maNo;
			_sqlPara[2] = new SqlParameter("@KEY_ITM", System.Data.SqlDbType.Int);
			_sqlPara[2].Value = key_itm;
			string _sqlStr = "SELECT MA_ID,MA_NO,ITM,PRD_NO,PRD_MARK,WC_NO,UNIT,SA_NO,SA_ITM,QTY,MTN_DD,"
						+ " EST_DD,RTN_DD,REM,KEY_ITM,QTY_MTN,MTN_TYPE FROM TF_MA"
						+ " WHERE MA_ID=@MA_ID AND MA_NO=@MA_NO AND KEY_ITM=@KEY_ITM ";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr, _ds, new string[1] { "TF_MA" }, _sqlPara);
			return _ds;
		}
		#endregion
	}
}
