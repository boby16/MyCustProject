using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// DbDRPTI 的摘要说明。
	/// </summary>
	public class DbDRPTI : Sunlike.Business.Data.DbObject
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
		public DbDRPTI(string connStr) : base(connStr)
		{
        }
        #region 取数据
        /// <summary>
		/// 取得出库单信息
		/// </summary>
		/// <param name="tiId">单据类别</param>
		/// <param name="tiNo">单据号</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string tiId, string tiNo)
		{
			string _sql = "SELECT M.*, S.NAME AS SAL_NAME, C.NAME AS CUS_NAME FROM"
				+ " (SELECT TI_ID, TI_NO, TI_DD, SAL_NO, CUS_NO, DEP, BIL_ID,"
				+ " BIL_NO, BIL_TYPE, CUS_OS_NO, BAT_NO, REM, USR, CHK_MAN, LOCK_MAN,CLS_DATE, PRT_SW, CLOSE_ID, CHKTY_ID,"
				+ " SYS_DATE, OS_ID, OS_NO,MOB_ID FROM MF_TI WHERE TI_ID=@TI_ID AND TI_NO=@TI_NO) AS M"
				+ " LEFT JOIN"
				+ " CUST AS C"
				+ " ON M.CUS_NO = C.CUS_NO"
				+ " LEFT JOIN"
				+ " SALM AS S"
				+ " ON M.SAL_NO = S.SAL_NO;"
				+ " SELECT T.*, PT.SPC, MW.NAME AS WH_NAME FROM"
				+ " (SELECT TI_ID, TI_NO, ITM, PRD_NO, PRD_NAME, PRD_MARK, WH, UNIT, QTY, QTY_CUS, REM, BIL_ID,"
				+ " BIL_NO, ID_NO, CUS_NO, SUP_PRD_NO, GF_NO, EST_ITM, SL_NO, SL_ITM, PRE_ITM, CK_ITM, CUS_OS_NO, B_DD, E_DD,"
				+ " QTY_RTN,QTY_PS,SH_NO_CUS,CHKTY_ID,MM_NO,QTY1,BAT_NO,QTY_RK,RK_DD FROM TF_TI WHERE TI_ID=@TI_ID AND TI_NO=@TI_NO) AS T"
				+ " LEFT JOIN"
				+ " MY_WH AS MW"
				+ " ON T.WH = MW.WH"
				+ " LEFT JOIN"
				+ " PRDT AS PT"
				+ " ON T.PRD_NO = PT.PRD_NO";
			SqlParameter[] _aryPt = new SqlParameter[2];
			_aryPt[0] = new SqlParameter("@TI_ID", SqlDbType.VarChar, 2);
			_aryPt[0].Value = tiId;
			_aryPt[1] = new SqlParameter("@TI_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = tiNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql, _ds, new string[2]{"MF_TI", "TF_TI"}, _aryPt);
			
			//表头和表身关联
			DataColumn[] _dca1 = new DataColumn[2];
			_dca1[0] = _ds.Tables["MF_TI"].Columns["TI_ID"];
			_dca1[1] = _ds.Tables["MF_TI"].Columns["TI_NO"];
			DataColumn[] _dca2 = new DataColumn[2];
			_dca2[0] = _ds.Tables["TF_TI"].Columns["TI_ID"];
			_dca2[1] = _ds.Tables["TF_TI"].Columns["TI_NO"];
			_ds.Relations.Add("MF_TITF_TI",_dca1,_dca2);

			return _ds;
		}
        /// <summary>
        /// 取入库免检转进货
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataTiHl(string sqlWhere)
        {
            string _sql = " SELECT TF.*,MF.TI_DD,MF.DEP,MF.CUS_NO AS CUS_NO_MF,MF.SAL_NO,MF.REM AS MFREM,MF.USR "
                             + " FROM TF_TI TF "
                             + " JOIN MF_TI MF ON MF.TI_ID=TF.TI_ID AND MF.TI_NO=TF.TI_NO "
                             + " WHERE MF.TI_ID='TI' AND ISNULL(MF.CHK_MAN,'')<>'' AND ISNULL(TF.QTY_PS,0)<ISNULL(TF.QTY,0) AND ISNULL(MF.CUS_NO, '') <> '' AND ISNULL(MF.SAL_NO, '') <> '' " + sqlWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "TF_TI" });
            return _ds;
        }
        /// <summary>
        /// 取得出库单信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetT6Data(string sqlWhere)
        {
            string _sql = "SELECT TF_TI.*, MF_TI.TI_DD,MF_TI.DEP,MF_TI.BIL_TYPE,MF_TI.REM AS REM_MF,CONTACT.CNT_NO,CONTACT.NAME AS CNT_NAME FROM \n"
                + "MF_TI \n"
                + "JOIN \n"
                + "TF_TI \n"
                + "ON MF_TI.TI_ID = TF_TI.TI_ID AND MF_TI.TI_NO = TF_TI.TI_NO \n"
                + "JOIN \n"
                + "MF_MO \n"
                + "ON TF_TI.BIL_ID = 'MO' AND TF_TI.BIL_NO = MF_MO.MO_NO \n"
                + "LEFT JOIN \n"
                + "TF_POS ON TF_POS.OS_ID = 'SO' AND TF_POS.OS_NO = MF_MO.SO_NO AND TF_POS.EST_ITM=MF_MO.EST_ITM \n"
                + "LEFT JOIN \n"
                + "TF_RCV ON TF_RCV.RV_ID = TF_POS.BIL_ID AND TF_RCV.RV_NO = TF_POS.QT_NO AND TF_RCV.KEY_ITM=TF_POS.OTH_ITM \n"
                + "LEFT JOIN \n"
                + "MF_MA ON MF_MA.MA_ID = TF_RCV.MA_ID AND MF_MA.MA_NO = TF_RCV.MA_NO \n"
                + "LEFT JOIN \n"
                + "CONTACT ON CONTACT.CNT_NO = MF_MA.CNT_NO AND CONTACT.CUS_NO = MF_MA.CUS_NO \n"
                + "WHERE MF_TI.TI_ID = 'T6' AND MF_MO.ISSVS = 'T' AND ISNULL(MF_TI.CHK_MAN, '') <> '' AND TF_TI.QTY > ISNULL(TF_TI.QTY_RTN, 0) \n" + sqlWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "TF_TI" });
            return _ds;
        }
        /// <summary>
        /// 取得表身信息
        /// </summary>
        /// <param name="tiId">送检单据别</param>
        /// <param name="tiNo">送检单号</param>
        /// <param name="keyField">项次名称</param>
        /// <param name="keyValue">项次值</param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string tiId, string tiNo,string keyField,int keyValue)
        {
            string _sqlStr = "SELECT T.*, PT.SPC, MW.NAME AS WH_NAME FROM"
                + " (SELECT TI_ID, TI_NO, ITM, PRD_NO, PRD_NAME, PRD_MARK, WH, UNIT, QTY, QTY_CUS, REM, BIL_ID,"
                + " BIL_NO, ID_NO, CUS_NO, SUP_PRD_NO, GF_NO, EST_ITM, SL_NO, SL_ITM, PRE_ITM, CK_ITM, CUS_OS_NO, B_DD, E_DD,"
                + " QTY_RTN,QTY_PS,SH_NO_CUS,CHKTY_ID,MM_NO,QTY1,BAT_NO,QTY_RK,RK_DD FROM TF_TI WHERE TI_ID=@TI_ID AND TI_NO=@TI_NO AND " + keyField + "=@KEY_VALUE) AS T"
                + " LEFT JOIN"
                + " MY_WH AS MW"
                + " ON T.WH = MW.WH"
                + " LEFT JOIN"
                + " PRDT AS PT"
                + " ON T.PRD_NO = PT.PRD_NO";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@TI_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = tiId;
            _aryPt[1] = new SqlParameter("@TI_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = tiNo;
            _aryPt[2] = new SqlParameter("@KEY_VALUE", SqlDbType.Int);
            _aryPt[2].Value = keyValue;            
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[1] { "TF_TI" }, _aryPt);           
            return _ds;
        }

        #endregion

        #region 获取厂商可送货的最大的数量
        /// <summary>
        /// 获取厂商可送货的最大的数量
        /// </summary>
        /// <param name="type">TW|PO|SL</param>
        /// <param name="bilNo">原单号</param>
        /// <param name="estItm">项次</param>
        /// <param name="slNo">收料转入时的收料单号</param>
        /// <param name="ratio">允许超交比率</param>
        /// <returns>最大的可送货数量</returns>
        public string GetMaxQty(string type, string bilNo, string estItm, string slNo, decimal ratio)
        {
            string _sql = "";
            switch (type)
            {
                case "PO":
                    _sql = "SELECT ISNULL(QTY, 0)*@RATIO - ISNULL(QTY_RK, 0) AS QTY_L FROM TF_POS "
                        + "WHERE OS_NO=@BILNO AND ITM=@ESTITM AND OS_ID='PO'";
                    break;
                case "SL":
                    _sql = "SELECT ISNULL(TN.QTY_SL, 0)*@RATIO - ISNULL(TP.QTY_RK, 0) AS QTY_L FROM TF_SL_NX AS TN"
                        + " LEFT JOIN TF_POS AS TP"
                        + " ON TN.PO_ID = TP.OS_ID AND TN.PO_NO = TP.OS_NO AND TN.PO_ITM = TP.ITM"
                        + " WHERE TN.SL_NO = @SLNO;";
                    break;
                case "TW":
                    _sql = "SELECT ISNULL(QTY, 0)*@RATIO - ISNULL(QTY_RK, 0) AS QTY_L FROM MF_TW "
                        + " WHERE TW_NO=@BILNO";
                    break;
            }
            SqlParameter [] _sp = new SqlParameter[4];
            _sp[0] = new SqlParameter("@BILNO", SqlDbType.VarChar, 20);
            _sp[0].Value = bilNo;
            _sp[1] = new SqlParameter("@ESTITM", SqlDbType.Int);
            _sp[1].Value = estItm;
            _sp[2] = new SqlParameter("@SLNO", SqlDbType.VarChar, 20);
            _sp[2].Value = slNo;
            _sp[3] = new SqlParameter("@RATIO", SqlDbType.Decimal);
            _sp[3].Value = ratio;

            return Convert.ToString(this.ExecuteScalar(_sql, _sp));

        }
        #endregion

        /// <summary>
        /// 根据货号和厂商号，返回货品的检验标志
        /// </summary>
        /// <param name="prdt">货号</param>
        /// <param name="cus">厂商号</param>
        /// <param name="isTw">采购 or 托外</param>
        /// <returns>chkty_id</returns>
        public string GetChkId(string prdt, string cus, bool isTw) 
        {
            string chkType = isTw ? "CHK_TW" : "CHK_PC";
            StringBuilder _sql=new StringBuilder();
            _sql.Append(" SELECT isnull(").Append(chkType).Append(",'') FROM PRDT_CUS")
                .Append(" WHERE PRD_NO=@PRD_NO AND CUS_NO=@CUS_NO");
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 2);
            _aryPt[0].Value = prdt;
            _aryPt[1] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = cus;
            return Convert.ToString(this.ExecuteScalar(_sql.ToString(), _aryPt));
        }
		/// <summary>
		/// 审核
		/// </summary>
		/// <param name="tiId">单据类别</param>
		/// <param name="tiNo">单号</param>
		/// <param name="chkMan">审核人</param>
		/// <param name="clsDate">审核日期</param>
		public void Approve(string tiId, string tiNo, string chkMan, DateTime clsDate)
		{
			string _sql = "UPDATE MF_TI SET CHK_MAN = @CHK_MAN, CLS_DATE = @CLS_DATE WHERE TI_ID = @TI_ID AND TI_NO = @TI_NO";
			SqlParameter[] _aryPt = new SqlParameter[4];
			_aryPt[0] = new SqlParameter("@TI_ID", SqlDbType.VarChar, 2);
			_aryPt[0].Value = tiId;
			_aryPt[1] = new SqlParameter("@TI_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = tiNo;
			_aryPt[2] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
			_aryPt[2].Value = chkMan;
			_aryPt[3] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
			_aryPt[3].Value = clsDate.ToString("yyyy-MM-dd HH:mm:ss");
			this.ExecuteNonQuery(_sql, _aryPt);
		}
		/// <summary>
		/// 反审核
		/// </summary>
		/// <param name="tiId">单据类别</param>
		/// <param name="tiNo">单号</param>
		public void Rollback(string tiId, string tiNo)
		{
			string _sql = "UPDATE MF_TI SET CHK_MAN = '', CLS_DATE = NULL WHERE TI_ID = @TI_ID AND TI_NO = @TI_NO";
			SqlParameter[] _aryPt = new SqlParameter[2];
			_aryPt[0] = new SqlParameter("@TI_ID", SqlDbType.VarChar, 2);
			_aryPt[0].Value = tiId;
			_aryPt[1] = new SqlParameter("@TI_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = tiNo;
			this.ExecuteNonQuery(_sql, _aryPt);
		}
        /// <summary>
        /// 更新MF_MO.QTY_RK
        /// </summary>
        /// <param name="moNo"></param>
        /// <param name="qtyRk"></param>
        public void UpdateQtyRk(string moNo, decimal qtyRk)
        {
            string _sql = "UPDATE MF_MO SET QTY_RK = ISNULL(QTY_RK, 0) + @QTY_RK WHERE MO_NO = @MO_NO";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _aryPt[0].Value = moNo;
            _aryPt[1] = new SqlParameter("@QTY_RK", SqlDbType.Decimal);
            _aryPt[1].Value = qtyRk;

            this.ExecuteNonQuery(_sql, _aryPt);
        }

        #region 修改回写入库单（TF_TI）的已验货量QTY_RTN
        /// <summary>
        /// 回写入库单（TF_TI）的已验货量QTY_RTN
        /// </summary>
        /// <param name="tiId"></param>
        /// <param name="tiNo"></param>
        /// <param name="tiItm"></param>
        /// <param name="qtyRtn"></param>
        /// <param name="qty1Rtn"></param>
        public void UpdateQtyRtn(string tiId, string tiNo, string tiItm, decimal qtyRtn, decimal qty1Rtn)
        {
            string _sqlStr = "UPDATE TF_TI SET QTY_RTN = ISNULL(QTY_RTN, 0) + @QTY_RTN, QTY1_RTN = ISNULL(QTY1_RTN, 0) + @QTY1_RTN "
                + " WHERE TI_ID = @TI_ID AND TI_NO = @TI_NO AND PRE_ITM = @PRE_ITM;\n";
            _sqlStr += "IF EXISTS(SELECT 1 FROM TF_TI WHERE TI_ID = @TI_ID AND TI_NO = @TI_NO AND ISNULL(QTY, 0) > ISNULL(QTY_RTN, 0)) \n"
                + "BEGIN \n"
                + "	UPDATE MF_TI SET CLOSE_ID = 'F' WHERE TI_ID = @TI_ID AND TI_NO = @TI_NO \n"
                + "END ELSE \n"
                + "BEGIN \n"
                + " UPDATE MF_TI SET CLOSE_ID = 'T' WHERE TI_ID = @TI_ID AND TI_NO = @TI_NO \n"
                + "END";
            SqlParameter[] _sqlPara = new SqlParameter[5];
            _sqlPara[0] = new SqlParameter("@TI_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = tiId;
            _sqlPara[1] = new SqlParameter("@TI_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = tiNo;
            _sqlPara[2] = new SqlParameter("@PRE_ITM", SqlDbType.Int);
            _sqlPara[2].Value = Convert.ToInt32(tiItm);
            _sqlPara[3] = new SqlParameter("@QTY_RTN", SqlDbType.Decimal);
            _sqlPara[3].Precision = 28;
            _sqlPara[3].Scale = 8;
            _sqlPara[3].Value = qtyRtn;
            _sqlPara[4] = new SqlParameter("@QTY1_RTN", SqlDbType.Decimal);
            _sqlPara[4].Precision = 28;
            _sqlPara[4].Scale = 8;
            _sqlPara[4].Value = qty1Rtn;

            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
        #endregion

        /// <summary>
        /// 更新转进货量
        /// </summary>
        /// <param name="tiId"></param>
        /// <param name="tiNo"></param>
        /// <param name="preItm"></param>
        /// <param name="qtyPs"></param>
        public void UpdateQtyPs(string tiId, string tiNo, string preItm, decimal qtyPs)
        {
            string _sqlStr = "UPDATE TF_TI SET QTY_PS = ISNULL(QTY_PS, 0) + @QTY_PS \n"
                + " WHERE TI_ID = @TI_ID AND TI_NO = @TI_NO AND PRE_ITM = @PRE_ITM;\n";
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@TI_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = tiId;
            _sqlPara[1] = new SqlParameter("@TI_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = tiNo;
            _sqlPara[2] = new SqlParameter("@PRE_ITM", SqlDbType.Int);
            _sqlPara[2].Value = Convert.ToInt32(preItm);
            _sqlPara[3] = new SqlParameter("@QTY_PS", SqlDbType.Decimal);
            _sqlPara[3].Precision = 28;
            _sqlPara[3].Scale = 8;
            _sqlPara[3].Value = qtyPs;

            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
    }
}
