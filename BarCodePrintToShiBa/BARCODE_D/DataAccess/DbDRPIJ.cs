
using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Collections;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPIJ.
	/// </summary>
	public class DbDRPIJ : DbObject
	{
		/// <summary>
		/// 调整单
		/// </summary>
		/// <param name="connectionString"></param>
		public DbDRPIJ(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// 取得单据资料
		/// </summary>
		/// <param name="IjID">单据别</param>
		/// <param name="IjNo">调整单号</param>
		/// <param name="OnlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
        public SunlikeDataSet GetData(string IjID, string IjNo, bool OnlyFillSchema)
        {
            string _sql = "select IJ_ID,IJ_NO,IJ_DD,IJ_REASON,FIX_CST,DEP,REF_NO,(SELECT TOP 1 A.PT_ID FROM MF_PT A WHERE MF_IJ.IJ_NO = A.IJ_NO OR MF_IJ.IJ_NO = A.IJ_NO1 OR MF_IJ.IJ_NO=A.SA_NO) AS REF_ID,MAN_NO,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,BIL_TYPE,SYS_DATE,CUS_NO,BAT_NO,VOH_ID,VOH_NO,BIL_ID,BIL_NO,SQ_ID,SQ_NO,LOCK_MAN,MOB_ID"
                + " from MF_IJ"
                + " where IJ_ID=@IjID and IJ_NO=@IjNo;"
                //+ "select ISNULL(MFOT.CLS_LJ_ID,'F') AS CLS_LJ_ID,T.IJ_ID,T.IJ_NO,T.ITM,T.IJ_DD,(case when T.QTY<0 then '-' else '+' end) as PLUS_FLAG,T.PRD_NO,T.PRD_MARK,P.SPC,T.WH,T.UNIT,T.QTY,T.QTY1,T.CST,T.CST_STD,T.FIX_CST,T.KEY_ITM,T.BOX_ITM,T.UP,T.PRE_ITM,T.VALID_DD,T.BAT_NO, T.SQ_ID, T.SQ_NO, T.SQ_ITM,ISNULL(OT.QTY,0)-ISNULL(OT.QTY_OT,0)+ISNULL(T.QTY,0) AS QTY_SQ_ORG,ISNULL(OT.QTY_OT,0)+ISNULL(T.QTY,0) as QTY_SQ_ORG1"
                ////+ "select T.IJ_ID,T.IJ_NO,T.ITM,T.IJ_DD,(case when T.QTY<0 then '-' else '+' end) as PLUS_FLAG,T.PRD_NO,T.PRD_MARK,P.SPC,T.WH,T.UNIT,T.QTY,T.QTY1,T.CST,T.CST_STD,T.FIX_CST,T.KEY_ITM,T.BOX_ITM,T.UP,T.PRE_ITM,T.VALID_DD,T.BAT_NO, T.SQ_ID, T.SQ_NO, T.SQ_ITM,ISNULL(OT.QTY,0)-ISNULL(OT.QTY_OT,0)+ISNULL(T.QTY,0) AS QTY_SQ_ORG"
                //+ " from TF_IJ T JOIN PRDT P ON T.PRD_NO = P.PRD_NO"
                //+ " LEFT JOIN TF_MOUT1 OT ON T.SQ_ID=OT.OT_ID AND T.SQ_NO=OT.OT_NO AND T.SQ_ITM=OT.KEY_ITM"
                //+ " LEFT JOIN MF_MOUT MFOT ON T.SQ_ID=MFOT.OT_ID AND T.SQ_NO=MFOT.OT_NO "
                + " SELECT     ISNULL(MFOT.CLS_ID, 'F') AS CLS_LJ_ID, T.IJ_ID, T.IJ_NO, T.ITM, T.IJ_DD, (CASE WHEN T .QTY < 0 THEN '-' ELSE '+' END) AS PLUS_FLAG, "
                + " T.PRD_NO, T.PRD_NAME,T.PRD_MARK, P.SPC, T.WH, T.UNIT, T.QTY, T.QTY1, T.CST, T.CST_STD, T.FIX_CST, T.KEY_ITM, T.BOX_ITM, T.UP, T.PRE_ITM, "
                + " T.VALID_DD, T.BAT_NO,T.BIL_ID,T.BIL_NO, T.SQ_ID, T.SQ_NO, T.SQ_ITM, (ISNULL(OT.QTY, 0) - ISNULL(OT.QTY_OT, 0)) "
                + " * (CASE WHEN OT.UNIT = '1' THEN 1 WHEN OT.UNIT = '2' THEN P.PK2_QTY WHEN OT.UNIT = '3' THEN P.PK3_QTY END) + ISNULL(T.QTY, 0) "
                + " * (CASE WHEN T .UNIT = '1' THEN 1 WHEN T .UNIT = '2' THEN P.PK2_QTY WHEN T .UNIT = '3' THEN P.PK3_QTY END)*(-1) AS QTY_SQ_ORG, "//领料单数据库中为负数所以这里要*(-1)
                + " ISNULL(OT.QTY_OT, 0) * (CASE WHEN OT.UNIT = '1' THEN 1 WHEN OT.UNIT = '2' THEN P.PK2_QTY WHEN OT.UNIT = '3' THEN P.PK3_QTY END)"
                + " + ISNULL(T.QTY, 0) * (CASE WHEN T .UNIT = '1' THEN 1 WHEN T .UNIT = '2' THEN P.PK2_QTY WHEN T .UNIT = '3' THEN P.PK3_QTY END) "
                + " AS QTY_SQ_ORG1,TFOT.PRD_NO AS PRD_NOTF, KEY_ITMTF = NULL,T.DEP_RK,T.RK_DD,"
                + " T.PAK_UNIT,T.PAK_EXC,T.PAK_NW,T.PAK_WEIGHT_UNIT,T.PAK_GW,T.PAK_MEAST,T.PAK_MEAST_UNIT "
                + " FROM  TF_IJ AS T INNER JOIN  PRDT AS P ON T.PRD_NO = P.PRD_NO LEFT OUTER JOIN TF_MOUT1 AS OT ON T.SQ_ID = OT.OT_ID AND T.SQ_NO = OT.OT_NO AND T.SQ_ITM = OT.KEY_ITM LEFT OUTER JOIN MF_MOUT AS MFOT ON T.SQ_ID = MFOT.OT_ID AND T.SQ_NO = MFOT.OT_NO LEFT OUTER JOIN TF_MOUT AS TFOT ON OT.OT_ID = TFOT.OT_ID AND OT.OT_NO = TFOT.OT_NO AND OT.OT_ITM=TFOT.KEY_ITM"
                + " where IJ_ID=@IjID and IJ_NO=@IjNo order by T.ITM;"
                + "select IJ_ID,IJ_NO,IJ_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO"
                + " from TF_IJ1"
                + " where IJ_ID=@IjID and IJ_NO=@IjNo;"
                + "select IJ_ID,IJ_NO,ITM,(case when QTY<0 then '-' else '+' end) as PLUS_FLAG,PRD_NO,CONTENT,QTY,KEY_ITM,WH"
                + " from TF_IJ2"
                + " where IJ_ID=@IjID and IJ_NO=@IjNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@IjID", SqlDbType.VarChar, 2);
            _spc[0].Value = IjID;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@IjNo", SqlDbType.VarChar, 20);
            _spc[1].Value = IjNo;
            string[] _aryTableName = new string[] { "MF_IJ", "TF_IJ", "TF_IJ_BAR", "TF_IJ_BOX" };
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (OnlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, _aryTableName, _spc);
            }
            else
            {
                this.FillDataset(_sql, _ds, _aryTableName, _spc);
            }
            DataColumn[] _dca = new DataColumn[3];
            _dca[0] = _ds.Tables["TF_IJ"].Columns["IJ_ID"];
            _dca[1] = _ds.Tables["TF_IJ"].Columns["IJ_NO"];
            _dca[2] = _ds.Tables["TF_IJ"].Columns["ITM"];
            _ds.Tables["TF_IJ"].PrimaryKey = _dca;
            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_IJ"].Columns["IJ_ID"];
            _dca1[1] = _ds.Tables["MF_IJ"].Columns["IJ_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_IJ"].Columns["IJ_ID"];
            _dca2[1] = _ds.Tables["TF_IJ"].Columns["IJ_NO"];
            _ds.Relations.Add("MF_IJTF_IJ", _dca1, _dca2);
            //表身和BAR_CODE关联
            _dca1 = new DataColumn[3];
            _dca1[0] = _ds.Tables["TF_IJ"].Columns["IJ_ID"];
            _dca1[1] = _ds.Tables["TF_IJ"].Columns["IJ_NO"];
            _dca1[2] = _ds.Tables["TF_IJ"].Columns["KEY_ITM"];
            _dca2 = new DataColumn[3];
            _dca2[0] = _ds.Tables["TF_IJ_BAR"].Columns["IJ_ID"];
            _dca2[1] = _ds.Tables["TF_IJ_BAR"].Columns["IJ_NO"];
            _dca2[2] = _ds.Tables["TF_IJ_BAR"].Columns["IJ_ITM"];
            _ds.Relations.Add("TF_IJTF_IJ_BAR", _dca1, _dca2);
            //表头与箱内容关联
            _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_IJ"].Columns["IJ_ID"];
            _dca1[1] = _ds.Tables["MF_IJ"].Columns["IJ_NO"];
            _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_IJ_BOX"].Columns["IJ_ID"];
            _dca2[1] = _ds.Tables["TF_IJ_BOX"].Columns["IJ_NO"];
            _ds.Relations.Add("MF_IJTF_IJ_BOX", _dca1, _dca2);
            return _ds;
        }

		/// <summary>
		/// 取得调整原因资料
		/// </summary>
		/// <returns></returns>
		public DataTable GetIjReasonData()
		{
            string _sql = "select IJ_REASON,DESCRIPTION,FIX_CST,ADD_ID,VOH_ID from IJREASON";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[] {"IJREASON"});
			return _ds.Tables["IJREASON"];
		}

		/// <summary>
		/// 更新BAR_PRINT表
		/// </summary>
		/// <param name="IjID">单据别</param>
		/// <param name="IjNo">单据单号</param>
		public void UpdateBarPrint(string IjID,string IjNo)
		{
			string _sql = "update BAR_REC set SPC_NO=(select SPC_NO from BAR_PRINT where BAR_NO=BAR_REC.BAR_NO)"
				+ " where BAR_NO in (select BAR_CODE from TF_IJ1 where IJ_ID=@IjID and IJ_NO=@IjNo) AND ISNULL(SPC_NO,'')= '' \n"
				+ "delete from BAR_PRINT where BAR_NO in (select BAR_CODE from TF_IJ1 where IJ_ID=@IjID and IJ_NO=@IjNo)";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@IjID",SqlDbType.VarChar,2);
			_spc[0].Value = IjID;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@IjNo",SqlDbType.VarChar,20);
			_spc[1].Value = IjNo;
			this.ExecuteNonQuery(_sql,_spc);
		}

		/// <summary>
		/// 修改结案标记、审核人和审核日期
		/// </summary>
		/// <param name="IjID">单据别</param>
		/// <param name="IjNo">单据号码</param>
		/// <param name="ChkMan">审核人</param>
		/// <param name="ClsDate">审核日期</param>
		public void UpdateChkMan(string IjID,string IjNo,string ChkMan,DateTime ClsDate)
		{
			string _sql = "update MF_IJ set CHK_MAN=@ChkMan,CLS_DATE=@ClsDate where IJ_ID=@IjID and IJ_NO=@IjNo";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@ChkMan",SqlDbType.VarChar,12);
			if (String.IsNullOrEmpty(ChkMan))
			{
				_spc[0].Value = System.DBNull.Value;
			}
			else
			{
				_spc[0].Value = ChkMan;
			}
			_spc[1] = new System.Data.SqlClient.SqlParameter("@ClsDate",SqlDbType.DateTime);
			if (String.IsNullOrEmpty(ChkMan))
			{
				_spc[1].Value = System.DBNull.Value;
			}
			else
			{
				_spc[1].Value = ClsDate.ToString("yyyy-MM-dd HH:mm:ss");
			}
			_spc[2] = new System.Data.SqlClient.SqlParameter("@IjID",SqlDbType.VarChar,2);
			_spc[2].Value = IjID;
			_spc[3] = new System.Data.SqlClient.SqlParameter("@IjNo",SqlDbType.VarChar,20);
			_spc[3].Value = IjNo;
			this.ExecuteNonQuery(_sql,_spc);
		}

		#region 重整箱数量
		/// <summary>
		/// 重整箱数量
		/// </summary>
		/// <param name="BeginDate">开始日期</param>
		/// <param name="EndDate">结束日期</param>
		public void ResetBoxQty(string BeginDate,string EndDate)
		{
			SqlParameter [] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@BDate",SqlDbType.VarChar,10);
			_spc[0].Value = BeginDate;
			_spc[1] = new SqlParameter("@EDate",SqlDbType.VarChar,10);
			_spc[1].Value = EndDate;
			this.ExecuteSpNonQuery("sp_CalculateBOXQTYIJ",_spc);
		}

		/// <summary>
		/// 重整箱数量
		/// </summary>
		/// <param name="IjNo">调整单号</param>
		/// <param name="PrdMark">产品特征</param>
		/// <param name="BoxItm">箱序号</param>
		/// <param name="Qty">数量</param>
		public void ResetBoxQty(string IjNo,string PrdMark,int BoxItm,decimal Qty)
		{
			SqlParameter [] _spc = new SqlParameter[4];
			_spc[0] = new SqlParameter("@IjNo",SqlDbType.VarChar,20);
			_spc[0].Value = IjNo;
			_spc[1] = new SqlParameter("@PrdMark",SqlDbType.VarChar,40);
			_spc[1].Value = PrdMark;
			_spc[2] = new SqlParameter("@BoxItm",SqlDbType.Int);
			_spc[2].Value = BoxItm;
			_spc[3] = new SqlParameter("@Qty",SqlDbType.Decimal);
			_spc[3].Precision = 28;
			_spc[3].Scale = 8;
			_spc[3].Value = Qty;

			this.ExecuteSpNonQuery("sp_CalculateBOXQTYIJ1",_spc);
		}
		#endregion

		#region 得出调整单据不同配码比在prdt1中的第一笔数据
		/// <summary>
		///  得出调整单据不同配码比在prdt1中的第一笔数据
		/// </summary>
		/// <param name="BeginDate">开始日期</param>
		/// <param name="EndDate">结束日期</param>
		/// <returns></returns>
		public DataTable GetFirstPrdt1ByBox(string BeginDate,string EndDate)
		{
			SqlParameter [] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@BDate",SqlDbType.VarChar,10);
			_spc[0].Value = BeginDate;
			_spc[1] = new SqlParameter("@EDate",SqlDbType.VarChar,10);
			_spc[1].Value = EndDate;
			string _sqlStr= "select IJ_NO,ITM,PRD_NO,PRD_MARK,BOX_ITM,QTY from fn_box_tf_ij(@BDate,@EDate)";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,_ds,null,_spc);
			return _ds.Tables[0];
		}

		#endregion

		#region 调整单是否已终审

		/// <summary>
		/// 调整单是否已终审
		/// </summary>
		/// <param name="IjNo">调整单号</param>
		/// <returns></returns>
		public bool IsFinalAuditing(string IjNo)
		{
			bool _isFinish = false;
			string _sqlStr = "SELECT 'T' FROM MF_IJ WHERE IJ_ID='IJ' AND IJ_NO=@IJ_NO AND ISNULL(CHK_MAN,'')<>''";
			SqlParameter[] _params = new SqlParameter[1];
			_params[0] = new SqlParameter("@IJ_NO", SqlDbType.VarChar, 20);
			_params[0].Value = IjNo;
			using (DataSet _ds = this.ExecuteDataset(_sqlStr, _params))
			{
				if (_ds.Tables[0].Rows.Count > 0)
					_isFinish = true;
			}
			return _isFinish;
		}

		#endregion


        #region 更新调整单凭证号码
        /// <summary>
        /// 更新调整单凭证号码
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string ijId, string ijNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_IJ SET VOH_NO=@VOH_NO WHERE IJ_ID=@IJ_ID AND IJ_NO=@IJ_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@IJ_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = ijId;

            _sqlPara[1] = new SqlParameter("@IJ_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = ijNo;

            _sqlPara[2] = new SqlParameter("@VOH_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = vohNo;
            try
            {
                this.ExecuteNonQuery(_sqlStr, _sqlPara);
                _result = true;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _result;
        }
        #endregion

      
    }
}
