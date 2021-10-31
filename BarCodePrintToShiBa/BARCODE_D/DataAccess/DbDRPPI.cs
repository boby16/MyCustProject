using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPPI.
	/// </summary>
	public class DbDRPPI : DbObject
	{
		private string _connectionString;
		/// <summary>
		/// 盘点单
		/// </summary>
		/// <param name="connectionString"></param>
		public DbDRPPI(string connectionString) : base(connectionString)
		{
			this._connectionString = connectionString;
		}


		#region 取数据
		/// <summary>
		/// 取得单据资料
		/// </summary>
        /// <param name="ptId"></param>
		/// <param name="ptNo">盘点单号</param>
		/// <param name="OnlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string ptId,string ptNo,bool OnlyFillSchema)
		{
            string _sql = "select PT_ID,PT_NO,PT_DD,PT_DD1,PT_MTH,BAT_NO,F_BAT_NO,E_BAT_NO,F_WH,E_WH,F_PRD_NO,E_PRD_NO,RATE,MAN_NO,REM,USR,CHK_MAN,PRT_SW,CPY_SW,F_IDX_NO,E_IDX_NO,CLS_DATE,SYS_DATE,MOB_ID,BIL_TYPE,CUS_NO,IJ_NO,IJ_NO1,SA_NO,DEP,BLN_OK,BR_NO,LOCK_MAN "
				+ " from MF_PT"
				+ " where PT_ID=@ptId and PT_NO=@PtNo "
				+ " ;"
                + "select PT_ID,PT_NO,ITM,PRD_NO,PRD_MARK,BAT_NO,WH,UNIT,QTY1,QTY2,QTY11,QTY21,CST_BOOK,CST_INV,CST_DIFF,CST_STD,QTY_RNG,QTY1_RNG,DISCRIPT,BOX_ITM,BIL_NO,BIL_ID,PRE_ITM "
				+ " from TF_PT"
                + " where PT_ID=@ptId and PT_NO=@PtNo "
				+ " order by ITM "
				+ " ;"
                + "select PT_ID,PT_NO,ITM,PRD_NO,CONTENT,WH,QTY1,QTY2,QTY_RNG,KEY_ITM,DISCRIPT "
				+ " from TF_PT3"
                + " where PT_ID=@ptId and PT_NO=@PtNo "
				+ " order by ITM "
                + " ;"
                + "select A.PT_ID,A.PT_NO,A.PT_ITM,A.ITM,A.PRD_NO,A.PRD_MARK,A.BAR_CODE,A.BOX_NO,A.PH_FLAG,A.STOP_ID,A.SPC_NO,A.WH,A.BAT_NO, "
                + " B.WH,B.BAT_NO "
                + " from PD_BARCODE A "
                + " left join TF_PT B ON B.PT_ID = A.PT_ID AND B.PT_NO = A.PT_NO AND B.PRE_ITM = A.PT_ITM"
                + " where A.PT_ID=@ptId and A.PT_NO=@PtNo "
                + " order by A.ITM "
                + " ;"
                + "select A.PT_ID,A.PT_NO,A.PT_ITM,A.ITM,A.PRD_NO,A.PRD_MARK,A.BAR_CODE,A.BOX_NO, "
                + " A.PL_FLAG,(CASE WHEN ISNULL(A.BOX_NO,'')<>'' THEN C.PH_FLAG ELSE D.PH_FLAG END) AS PH_FLAG,A.PH_FLAG_BAK,(CASE WHEN ISNULL(A.BOX_NO,'')<>'' THEN C.STOP_ID ELSE D.STOP_ID END) AS STOP_ID,"
                + " D.SPC_NO SPC_NO,D.SPC_NO AS SPC_NO_BAK,"
                + " A.STOP_ID_BAK,A.WH_BAK,A.BAT_NO_BAK,A.UPDDATE_BAK,CASE WHEN A.PL_FLAG='+' THEN B.WH ELSE '' END AS WH,CASE WHEN A.PL_FLAG='+' THEN B.BAT_NO ELSE '' END AS BAT_NO "
                + " from PD_BARCODE_IJ A "
                + " left join TF_PT B ON B.PT_ID = A.PT_ID AND B.PT_NO = A.PT_NO AND B.PRE_ITM = A.PT_ITM"
                + " left join BAR_BOX C ON C.BOX_NO = A.BOX_NO "
                + " left join BAR_REC D ON D.BAR_NO = A.BAR_CODE "
                + " where A.PT_ID=@ptId and A.PT_NO=@PtNo "
                + " order by A.ITM "
				+ " ;";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ptId", SqlDbType.VarChar, 2);
            _spc[0].Value = ptId;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PtNo", SqlDbType.VarChar, 20);
            _spc[1].Value = ptNo;
            string[] _aryTableName = new string[] { "MF_PT", "TF_PT", "TF_PT3", "PD_BARCODE", "PD_BARCODE_IJ" };
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (OnlyFillSchema)
			{
				this.FillDatasetSchema(_sql,_ds,_aryTableName,_spc);
			}
			else
			{
				this.FillDataset(_sql,_ds,_aryTableName,_spc);
			}
            //去除只读
            if (_ds != null && _ds.Tables.Contains("PD_BARCODE_IJ"))
            {
                if (_ds.Tables["PD_BARCODE_IJ"].Columns.Contains("WH"))
                    _ds.Tables["PD_BARCODE_IJ"].Columns["WH"].ReadOnly = false;
                if (_ds.Tables["PD_BARCODE_IJ"].Columns.Contains("BAT_NO"))
                    _ds.Tables["PD_BARCODE_IJ"].Columns["BAT_NO"].ReadOnly = false;
                if (_ds.Tables["PD_BARCODE_IJ"].Columns.Contains("PH_FLAG"))
                    _ds.Tables["PD_BARCODE_IJ"].Columns["PH_FLAG"].ReadOnly = false;
                if (_ds.Tables["PD_BARCODE_IJ"].Columns.Contains("STOP_ID"))
                    _ds.Tables["PD_BARCODE_IJ"].Columns["STOP_ID"].ReadOnly = false;
                if (_ds.Tables["PD_BARCODE_IJ"].Columns.Contains("SPC_NO_BAK"))
                    _ds.Tables["PD_BARCODE_IJ"].Columns["SPC_NO_BAK"].ReadOnly = false;

            }
            DataColumn[] _dc = null;            
            _dc = new DataColumn[2];
            _dc[0] = _ds.Tables["MF_PT"].Columns["PT_ID"];
            _dc[1] = _ds.Tables["MF_PT"].Columns["PT_NO"];
            _ds.Tables["MF_PT"].PrimaryKey = _dc;
            _dc = new DataColumn[3];
            _dc[0] = _ds.Tables["TF_PT"].Columns["PT_ID"];
            _dc[1] = _ds.Tables["TF_PT"].Columns["PT_NO"];
            _dc[2] = _ds.Tables["TF_PT"].Columns["ITM"];
            _ds.Tables["TF_PT"].PrimaryKey = _dc;
            _dc = new DataColumn[3];
            _dc[0] = _ds.Tables["TF_PT3"].Columns["PT_ID"];
            _dc[1] = _ds.Tables["TF_PT3"].Columns["PT_NO"];
            _dc[2] = _ds.Tables["TF_PT3"].Columns["ITM"];
            _ds.Tables["TF_PT3"].PrimaryKey = _dc;
            _dc = new DataColumn[3];
            _dc[0] = _ds.Tables["PD_BARCODE"].Columns["PT_ID"];
            _dc[1] = _ds.Tables["PD_BARCODE"].Columns["PT_NO"];
            _dc[2] = _ds.Tables["PD_BARCODE"].Columns["ITM"];
            _ds.Tables["PD_BARCODE"].PrimaryKey = _dc;
            _dc = new DataColumn[3];
            _dc[0] = _ds.Tables["PD_BARCODE_IJ"].Columns["PT_ID"];
            _dc[1] = _ds.Tables["PD_BARCODE_IJ"].Columns["PT_NO"];
            _dc[2] = _ds.Tables["PD_BARCODE_IJ"].Columns["ITM"];
            _ds.Tables["PD_BARCODE_IJ"].PrimaryKey = _dc;

            //创建关联
            DataColumn[] _dc1= null;
            DataColumn[] _dc2 = null;
            _dc1 = new DataColumn[2];
            _dc1[0] = _ds.Tables["MF_PT"].Columns["PT_ID"];
            _dc1[1] = _ds.Tables["MF_PT"].Columns["PT_NO"];
            _dc2 = new DataColumn[2];
            _dc2[0] = _ds.Tables["TF_PT"].Columns["PT_ID"];
            _dc2[1] = _ds.Tables["TF_PT"].Columns["PT_NO"];
            _ds.Relations.Add("MF_PTTF_PT", _dc1, _dc2);
            _dc1 = new DataColumn[2];
            _dc1[0] = _ds.Tables["MF_PT"].Columns["PT_ID"];
            _dc1[1] = _ds.Tables["MF_PT"].Columns["PT_NO"];
            _dc2 = new DataColumn[2];
            _dc2[0] = _ds.Tables["TF_PT3"].Columns["PT_ID"];
            _dc2[1] = _ds.Tables["TF_PT3"].Columns["PT_NO"];
            _ds.Relations.Add("MF_PTTF_PT3", _dc1, _dc2);
            _dc1 = new DataColumn[2];
            _dc1[0] = _ds.Tables["MF_PT"].Columns["PT_ID"];
            _dc1[1] = _ds.Tables["MF_PT"].Columns["PT_NO"];
            _dc2 = new DataColumn[2];
            _dc2[0] = _ds.Tables["PD_BARCODE"].Columns["PT_ID"];
            _dc2[1] = _ds.Tables["PD_BARCODE"].Columns["PT_NO"];
            _ds.Relations.Add("MF_PTPD_BARCODE", _dc1, _dc2);
            _dc1 = new DataColumn[2];
            _dc1[0] = _ds.Tables["MF_PT"].Columns["PT_ID"];
            _dc1[1] = _ds.Tables["MF_PT"].Columns["PT_NO"];
            _dc2 = new DataColumn[2];
            _dc2[0] = _ds.Tables["PD_BARCODE_IJ"].Columns["PT_ID"];
            _dc2[1] = _ds.Tables["PD_BARCODE_IJ"].Columns["PT_NO"];
            _ds.Relations.Add("MF_PTPD_BARCODE_IJ", _dc1, _dc2);
    		return _ds;
		}

		#endregion

		#region 按条件取得盘点单的表身
		/// <summary>
		/// 按条件取得盘点单的表身
		/// </summary>
		///	<param name="selectMode">条件选择模式</param>
		///	<param name="cusNo">客户代号</param>
		/// <param name="StartWh">起始库位</param>
		/// <param name="EndWh">截止库位</param>
		/// <param name="StartIdxNo">起始中类</param>
		/// <param name="EndIdxNo">截止中类</param>
		/// <param name="StartPrdNo">起始品号</param>
		/// <param name="EndPrdNo">截止品号</param>
		/// <param name="prdNos"></param>
		/// <param name="StartPrdMark">起始特征</param>
		/// <param name="EndPrdMark">截止特征</param>
		/// <returns></returns>
		public SunlikeDataSet GetPrdt1(string selectMode,string cusNo, string StartWh,string EndWh,string StartIdxNo,string EndIdxNo,string StartPrdNo,string EndPrdNo,string prdNos,string StartPrdMark,string EndPrdMark)
		{

            string _sqlPrdNo = "select DISTINCT A.WH,A.PRD_NO,A.PRD_MARK,B.DFU_UT"
				+ " FROM PRDT1 A"
				+ " INNER JOIN PRDT B ON B.PRD_NO=A.PRD_NO"
				+ " WHERE (B.NOUSE_DD IS NULL OR B.NOUSE_DD>GETDATE())";
			string _sqlBox = "SELECT A.WH,A.PRD_NO,A.CONTENT FROM PRDT1_BOX A"
							+" INNER JOIN PRDT B ON B.PRD_NO=A.PRD_NO "
							+"WHERE (B.NOUSE_DD IS NULL OR B.NOUSE_DD>GETDATE()) ";
			if (prdNos.Length > 0 )
			{
				_sqlPrdNo += " AND A.PRD_NO IN ('"+prdNos.Replace(";","','")+"') ";
				_sqlBox += " AND A.PRD_NO IN ('"+prdNos.Replace(";","','")+"') ";
			}

			int _paraCount = 0;
			if (!String.IsNullOrEmpty(cusNo))
			{
				_sqlBox += " and A.WH in (select wh from MY_WH where cus_no =@CusNo)";
				_sqlPrdNo += " and A.WH in (select wh from MY_WH where cus_no =@CusNo)";
				_paraCount ++;

			}
			//起始库位
			if (!String.IsNullOrEmpty(StartWh))
			{
				_sqlBox += " and A.WH>=@StartWh";
				_sqlPrdNo += " and A.WH>=@StartWh";
				_paraCount ++;
			}
			//截止库位
			if (!String.IsNullOrEmpty(EndWh))
			{
				_sqlBox += " and A.WH<=@EndWh";
				_sqlPrdNo += " and A.WH<=@EndWh";
				_paraCount ++;
			}
			//起始中类
			if (!String.IsNullOrEmpty(StartIdxNo))
			{
				_sqlBox += " and B.IDX1>=@StartIdxNo";
				_sqlPrdNo += " and B.IDX1>=@StartIdxNo";
				_paraCount ++;
			}
			//截止中类
			if (!String.IsNullOrEmpty(EndIdxNo))
			{
				_sqlBox += " and B.IDX1<=@EndIdxNo";
				_sqlPrdNo += " and B.IDX1<=@EndIdxNo";
					_paraCount ++;
			}
			//起始品号
			if (!String.IsNullOrEmpty(StartPrdNo))
			{
				_sqlBox += " and A.PRD_NO>=@StartPrdNo";
				_sqlPrdNo += " and A.PRD_NO>=@StartPrdNo";
				_paraCount ++;
			}
			//截止品号
			if (!String.IsNullOrEmpty(EndPrdNo))
			{
				_sqlBox += " and A.PRD_NO<=@EndPrdNo";
				_sqlPrdNo += " and A.PRD_NO<=@EndPrdNo";
				_paraCount ++;
			}
			//起始特征
			if (!String.IsNullOrEmpty(StartPrdMark))
			{
				_sqlPrdNo += " and A.PRD_MARK>=@StartPrdMark";
				_paraCount ++;
			}
			//截止特征
			if (!String.IsNullOrEmpty(EndPrdMark))
			{
				_sqlPrdNo += " and A.PRD_MARK<=@EndPrdMark";
				_paraCount ++;
			}
			string _sql = "";
			string[] _tableName;
			if( selectMode == "0")
			{
				_sql = _sqlPrdNo;
				_tableName = new string[] {"PRDT1"};
			}
			else if (selectMode == "1")
			{
				_sql = _sqlBox;
				_tableName = new string[] {"PRDT1_BOX"};
			}
			else 
			{
				_tableName = new string[] {"PRDT1","PRDT1_BOX"};
				_sql = _sqlPrdNo +";"+ _sqlBox;
			}
		//看是否有带入参数			
		SunlikeDataSet _ds = new SunlikeDataSet();
			if (_paraCount > 0)
			{
				System.Data.SqlClient.SqlParameter[] _sqlPara = new System.Data.SqlClient.SqlParameter[_paraCount];
				_paraCount = -1;
				//客户
				if (!String.IsNullOrEmpty(cusNo))
				{
						_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@CusNo",SqlDbType.VarChar,12);
					_sqlPara[_paraCount].Value = cusNo;
				}
				//起始库位
				if (!String.IsNullOrEmpty(StartWh))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@StartWh",SqlDbType.VarChar,12);
					_sqlPara[_paraCount].Value = StartWh;
					
				}
				//截止库位
				if (!String.IsNullOrEmpty(EndWh))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@EndWh",SqlDbType.VarChar,12);
					_sqlPara[_paraCount].Value = EndWh;
				}
				//起始中类
				if (!String.IsNullOrEmpty(StartIdxNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@StartIdxNo",SqlDbType.VarChar,10);
					_sqlPara[_paraCount].Value = StartIdxNo;
				}
				//截止中类
				if (!String.IsNullOrEmpty(EndIdxNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@EndIdxNo",SqlDbType.VarChar,10);
					_sqlPara[_paraCount].Value = EndIdxNo;
				}
				//起始品号
				if (!String.IsNullOrEmpty(StartPrdNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@StartPrdNo",SqlDbType.VarChar,30);
					_sqlPara[_paraCount].Value = StartPrdNo;
				}
				//截止品号
				if (!String.IsNullOrEmpty(EndPrdNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@EndPrdNo",SqlDbType.VarChar,30);
					_sqlPara[_paraCount].Value = EndPrdNo;
				}
				//起始特征
				if (!String.IsNullOrEmpty(StartPrdMark))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@StartPrdMark",SqlDbType.VarChar,40);
					_sqlPara[_paraCount].Value = StartPrdMark;
				}
				//截止特征
				if (!String.IsNullOrEmpty(EndPrdMark))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@EndPrdMark",SqlDbType.VarChar,40);
					_sqlPara[_paraCount].Value = EndPrdMark;
				}
					this.FillDataset(_sql,_ds,_tableName,_sqlPara);
			}
			else
			{
				this.FillDataset(_sql,_ds,_tableName);
			}
			return _ds;
		}
		/// <summary>
		/// 按条件取得盘点单的表身(批号)
		/// </summary>
		/// <param name="cusNo">客户代号</param>
		/// <param name="startBatNo">起始批号</param>
		/// <param name="endBatNo">截止批号</param>
		/// <param name="startWh">起始库位</param>
		/// <param name="endWh">截止库位</param>
		/// <param name="startIdxNo">起始中类</param>
		/// <param name="endIdxNo">截止中类</param>
		/// <param name="startPrdNo">起始品号</param>
		/// <param name="endPrdNo">截止品号</param>
		/// <param name="prdNos">品号</param>
		/// <param name="startPrdMark">起始特征</param>
		/// <param name="endPrdMark">截止特征</param>
		/// <returns></returns>
		public SunlikeDataSet GetBatRec1(string cusNo,string startBatNo,string endBatNo,string startWh,string endWh,string startIdxNo,string endIdxNo,string startPrdNo,string endPrdNo,string prdNos,string startPrdMark,string endPrdMark)
		{
			string _sqlStr = "";
            _sqlStr = " SELECT DISTINCT A.BAT_NO,A.WH,A.PRD_NO,A.PRD_MARK,B.DFU_UT"
					+ " FROM BAT_REC1 A"
					+ " INNER JOIN PRDT B ON B.PRD_NO=A.PRD_NO"
					+ " WHERE (B.NOUSE_DD IS NULL OR B.NOUSE_DD>GETDATE()) ";
			if (prdNos.Length > 0)
			{
				_sqlStr += " AND A.PRD_NO IN ('"+prdNos.Replace(";","','")+"') ";
			}
			int _paraCount = 0;
			if (!String.IsNullOrEmpty(cusNo))
			{				
				_sqlStr += " AND A.WH IN (SELECT WH FROM MY_WH where CUS_NO =@CUS_NO)";
				_paraCount ++;
			}
			//起始批号
			if (!String.IsNullOrEmpty(startBatNo))
			{
				_sqlStr += " and A.BAT_NO>=@startBatNo";
				_paraCount ++;
			}
			//截止批号
			if (!String.IsNullOrEmpty(endBatNo))
			{
				_sqlStr += " and A.BAT_NO<=@endBatNo";
				_paraCount ++;
			}			
			//起始库位
			if (!String.IsNullOrEmpty(startWh))
			{
				_sqlStr += " and A.WH>=@startWh";				
				_paraCount ++;
			}
			//截止库位
			if (!String.IsNullOrEmpty(endWh))
			{
				_sqlStr += " and A.WH<=@endWh";
				_paraCount ++;
			}
			//起始中类
			if (!String.IsNullOrEmpty(startIdxNo))
			{
				_sqlStr += " and B.IDX1>=@startIdxNo";
				_paraCount ++;
			}
			//截止中类
			if (!String.IsNullOrEmpty(endIdxNo))
			{
				_sqlStr += " and B.IDX1<=@endIdxNo";
				_paraCount ++;
			}
			//起始品号
			if (!String.IsNullOrEmpty(startPrdNo))
			{
				_sqlStr += " and A.PRD_NO>=@startPrdNo";
				_paraCount ++;
			}
			//截止品号
			if (!String.IsNullOrEmpty(endPrdNo))
			{
				_sqlStr += " and A.PRD_NO<=@endPrdNo";
				_paraCount ++;
			}
			//起始特征
			if (!String.IsNullOrEmpty(startPrdMark))
			{
				_sqlStr += " and A.PRD_MARK>=@startPrdMark";
				_paraCount ++;
			}
			//截止特征
			if (!String.IsNullOrEmpty(endPrdMark))
			{
				_sqlStr += " and A.PRD_MARK<=@endPrdMark";
				_paraCount ++;
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (_paraCount > 0)
			{
				System.Data.SqlClient.SqlParameter[] _sqlPara = new System.Data.SqlClient.SqlParameter[_paraCount];
				_paraCount = -1;
				//客户
				if (!String.IsNullOrEmpty(cusNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@CUS_NO",SqlDbType.VarChar,12);
					_sqlPara[_paraCount].Value = cusNo;
				}
				//起始批号
				if (!String.IsNullOrEmpty(startBatNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startBatNo",SqlDbType.VarChar,20);
					_sqlPara[_paraCount].Value = startBatNo;
				}
				//截止批号
				if (!String.IsNullOrEmpty(endBatNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endBatNo",SqlDbType.VarChar,20);
					_sqlPara[_paraCount].Value = endBatNo;
				}			
				//起始库位
				if (!String.IsNullOrEmpty(startWh))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startWh",SqlDbType.VarChar,12);
					_sqlPara[_paraCount].Value = startWh;
					
				}
				//截止库位
				if (!String.IsNullOrEmpty(endWh))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endWh",SqlDbType.VarChar,12);
					_sqlPara[_paraCount].Value = endWh;
				}
				//起始中类
				if (!String.IsNullOrEmpty(startIdxNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startIdxNo",SqlDbType.VarChar,10);
					_sqlPara[_paraCount].Value = startIdxNo;
				}
				//截止中类
				if (!String.IsNullOrEmpty(endIdxNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endIdxNo",SqlDbType.VarChar,10);
					_sqlPara[_paraCount].Value = endIdxNo;
				}
				//起始品号
				if (!String.IsNullOrEmpty(startPrdNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startPrdNo",SqlDbType.VarChar,30);
					_sqlPara[_paraCount].Value = startPrdNo;
				}
				//截止品号
				if (!String.IsNullOrEmpty(endPrdNo))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endPrdNo",SqlDbType.VarChar,30);
					_sqlPara[_paraCount].Value = endPrdNo;
				}
				//起始特征
				if (!String.IsNullOrEmpty(startPrdMark))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startPrdMark",SqlDbType.VarChar,40);
					_sqlPara[_paraCount].Value = startPrdMark;
				}
				//截止特征
				if (!String.IsNullOrEmpty(endPrdMark))
				{
					_paraCount += 1;
					_sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endPrdMark",SqlDbType.VarChar,40);
					_sqlPara[_paraCount].Value = endPrdMark;
				}
				this.FillDataset(_sqlStr,_ds,new string[] {"BAT_REC1"},_sqlPara);
			}
			else
			{
				this.FillDataset(_sqlStr,_ds,new string[] {"BAT_REC1"});
			}
			return _ds;
		}
        /// <summary>
        /// 按条件取得盘点单的表身(序列号)
        /// </summary>
        /// <param name="cusNo">客户代号</param>
        /// <param name="startBatNo">起始批号</param>
        /// <param name="endBatNo">截止批号</param>
        /// <param name="startWh">起始库位</param>
        /// <param name="endWh">截止库位</param>
        /// <param name="startIdxNo">起始中类</param>
        /// <param name="endIdxNo">截止中类</param>
        /// <param name="startPrdNo">起始品号</param>
        /// <param name="endPrdNo">截止品号</param>
        /// <param name="prdNos">品号</param>
        /// <param name="startPrdMark">起始特征</param>
        /// <param name="endPrdMark">截止特征</param>
        /// <returns></returns>
        public SunlikeDataSet GetBarRec(string cusNo, string startBatNo, string endBatNo, string startWh, string endWh, string startIdxNo, string endIdxNo, string startPrdNo, string endPrdNo, string prdNos, string startPrdMark, string endPrdMark)
        {
            string _sqlStr = "";
            _sqlStr = " SELECT DISTINCT A.BAT_NO,A.WH,A.PRD_NO,A.PRD_MARK,B.DFU_UT"
                    + " FROM BAR_REC A"
                    + " INNER JOIN PRDT B ON B.PRD_NO=A.PRD_NO"
                    + " WHERE (B.NOUSE_DD IS NULL OR B.NOUSE_DD>GETDATE()) "
                    + " AND A.WH <>'' AND A.STOP_ID <>'T' "
                    + " AND NOT EXISTS (SELECT WH FROM PRDT1 WHERE PRDT1.WH=A.WH AND PRDT1.PRD_NO=A.PRD_NO AND PRDT1.PRD_MARK=A.PRD_MARK )"
                    + " AND NOT EXISTS (SELECT WH FROM BAT_REC1 WHERE BAT_REC1.BAT_NO = A.BAT_NO AND BAT_REC1.WH=A.WH AND BAT_REC1.PRD_NO=A.PRD_NO AND BAT_REC1.PRD_MARK=A.PRD_MARK )";
            if (prdNos.Length > 0)
            {
                _sqlStr += " AND A.PRD_NO IN ('" + prdNos.Replace(";", "','") + "') ";
            }
            int _paraCount = 0;
            if (!String.IsNullOrEmpty(cusNo))
            {
                _sqlStr += " AND A.WH IN (SELECT WH FROM MY_WH where CUS_NO =@CUS_NO)";
                _paraCount++;
            }
            //起始批号
            if (!String.IsNullOrEmpty(startBatNo))
            {
                _sqlStr += " and A.BAT_NO>=@startBatNo";
                _paraCount++;
            }
            //截止批号
            if (!String.IsNullOrEmpty(endBatNo))
            {
                _sqlStr += " and A.BAT_NO<=@endBatNo";
                _paraCount++;
            }
            //起始库位
            if (!String.IsNullOrEmpty(startWh))
            {
                _sqlStr += " and A.WH>=@startWh";
                _paraCount++;
            }
            //截止库位
            if (!String.IsNullOrEmpty(endWh))
            {
                _sqlStr += " and A.WH<=@endWh";
                _paraCount++;
            }
            //起始中类
            if (!String.IsNullOrEmpty(startIdxNo))
            {
                _sqlStr += " and B.IDX1>=@startIdxNo";
                _paraCount++;
            }
            //截止中类
            if (!String.IsNullOrEmpty(endIdxNo))
            {
                _sqlStr += " and B.IDX1<=@endIdxNo";
                _paraCount++;
            }
            //起始品号
            if (!String.IsNullOrEmpty(startPrdNo))
            {
                _sqlStr += " and A.PRD_NO>=@startPrdNo";
                _paraCount++;
            }
            //截止品号
            if (!String.IsNullOrEmpty(endPrdNo))
            {
                _sqlStr += " and A.PRD_NO<=@endPrdNo";
                _paraCount++;
            }
            //起始特征
            if (!String.IsNullOrEmpty(startPrdMark))
            {
                _sqlStr += " and A.PRD_MARK>=@startPrdMark";
                _paraCount++;
            }
            //截止特征
            if (!String.IsNullOrEmpty(endPrdMark))
            {
                _sqlStr += " and A.PRD_MARK<=@endPrdMark";
                _paraCount++;
            }
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (_paraCount > 0)
            {
                System.Data.SqlClient.SqlParameter[] _sqlPara = new System.Data.SqlClient.SqlParameter[_paraCount];
                _paraCount = -1;
                //客户
                if (!String.IsNullOrEmpty(cusNo))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
                    _sqlPara[_paraCount].Value = cusNo;
                }
                //起始批号
                if (!String.IsNullOrEmpty(startBatNo))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startBatNo", SqlDbType.VarChar, 20);
                    _sqlPara[_paraCount].Value = startBatNo;
                }
                //截止批号
                if (!String.IsNullOrEmpty(endBatNo))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endBatNo", SqlDbType.VarChar, 20);
                    _sqlPara[_paraCount].Value = endBatNo;
                }
                //起始库位
                if (!String.IsNullOrEmpty(startWh))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startWh", SqlDbType.VarChar, 12);
                    _sqlPara[_paraCount].Value = startWh;

                }
                //截止库位
                if (!String.IsNullOrEmpty(endWh))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endWh", SqlDbType.VarChar, 12);
                    _sqlPara[_paraCount].Value = endWh;
                }
                //起始中类
                if (!String.IsNullOrEmpty(startIdxNo))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startIdxNo", SqlDbType.VarChar, 10);
                    _sqlPara[_paraCount].Value = startIdxNo;
                }
                //截止中类
                if (!String.IsNullOrEmpty(endIdxNo))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endIdxNo", SqlDbType.VarChar, 10);
                    _sqlPara[_paraCount].Value = endIdxNo;
                }
                //起始品号
                if (!String.IsNullOrEmpty(startPrdNo))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startPrdNo", SqlDbType.VarChar, 30);
                    _sqlPara[_paraCount].Value = startPrdNo;
                }
                //截止品号
                if (!String.IsNullOrEmpty(endPrdNo))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endPrdNo", SqlDbType.VarChar, 30);
                    _sqlPara[_paraCount].Value = endPrdNo;
                }
                //起始特征
                if (!String.IsNullOrEmpty(startPrdMark))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@startPrdMark", SqlDbType.VarChar, 40);
                    _sqlPara[_paraCount].Value = startPrdMark;
                }
                //截止特征
                if (!String.IsNullOrEmpty(endPrdMark))
                {
                    _paraCount += 1;
                    _sqlPara[_paraCount] = new System.Data.SqlClient.SqlParameter("@endPrdMark", SqlDbType.VarChar, 40);
                    _sqlPara[_paraCount].Value = endPrdMark;
                }
                this.FillDataset(_sqlStr, _ds, new string[] { "BAR_REC" }, _sqlPara);
            }
            else
            {
                this.FillDataset(_sqlStr, _ds, new string[] { "BAR_REC" });
            }
            return _ds;
        }
		#endregion

		#region 结案
		/// <summary>
		/// 结案盘点单
		/// </summary>
        /// <param name="ptId"></param>
        /// <param name="ptNo"></param>
        /// <param name="chkMan"></param>
        /// <param name="clsDd"></param>
		/// <returns></returns>
		public bool ClosePI(string ptId,string ptNo,string chkMan,DateTime clsDd)
		{
			string _where = "";
			bool _result = false;
			_where = "  PT_ID =@PT_ID AND PT_NO=@PT_NO ";
			string _sqlStr = "";
			_sqlStr = " UPDATE MF_PT SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE "+_where;
			SqlParameter[] _sqlPara = new SqlParameter[4];

            _sqlPara[0] = new SqlParameter("@PT_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = ptId;

			_sqlPara[1] = new SqlParameter("@PT_NO",SqlDbType.VarChar,20);
            _sqlPara[1].Value = ptNo;

			_sqlPara[2] = new SqlParameter("@CHK_MAN",SqlDbType.VarChar,12);
            _sqlPara[2].Value = chkMan;

			_sqlPara[3] = new SqlParameter("@CLS_DATE",SqlDbType.DateTime);
            _sqlPara[3].Value = clsDd.ToString("yyyy-MM-dd HH:mm:ss");
			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);				
				_result = false;
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _result;

		}
		/// <summary>
		/// 反结案盘点单
		/// </summary>
        /// <param name="ptId"></param>
        /// <param name="ptNo"></param>
		/// <returns></returns>
		public bool RollbackPI(string ptId,string ptNo)
		{
			string _where = "";
			bool _result = false;
			_where = "  PT_ID=@PT_ID AND PT_NO=@PT_NO ";
			string _sqlStr = "";
			_sqlStr = " UPDATE MF_PT SET CHK_MAN=NULL,CLS_DATE=NULL WHERE "+_where;
			SqlParameter[] _sqlPara = new SqlParameter[2];

            _sqlPara[0] = new SqlParameter("@PT_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = ptId;
            _sqlPara[1] = new SqlParameter("@PT_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = ptNo;

			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);				
				_result = false;
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _result;
		}
		#endregion

		#region 修改盘点单资料添加结转单据
		/// <summary>
		/// 修改盘点单资料添加结转单据
		/// </summary>
		/// <param name="ptNo"></param>
		/// <param name="billNo"></param>
		/// <param name="updateField"></param>
		/// <returns></returns>
        public string UpdateMF_PT(string ptNo, string billNo, string updateField)
		{
			string _errorMsg = "";
			string _sqlStr = "";
            _sqlStr = " UPDATE MF_PT SET " + updateField + "=@BIL_NO WHERE PT_NO=@PT_NO";
			SqlParameter [] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
			_sqlPara[0].Value = billNo;
            _sqlPara[1] = new SqlParameter("@PT_NO", SqlDbType.VarChar, 20);
			_sqlPara[1].Value = ptNo;

			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);
			}
			catch (Exception _ex)
			{
				_errorMsg = _ex.Message.ToString();
			}
			return _errorMsg;

		}
		/// <summary>
		/// 修改盘点单表身资料添加结转单据
		/// </summary>
		/// <param name="ptNo">要回写的盘点单号</param>
		/// <param name="itm">要回写的项次</param>
		/// <param name="billId">回写识别码</param>
		/// <param name="billNo">回写单号</param>
		/// <param name="updateField">表头修改的字段</param>
		/// <returns></returns>
		public string UpdateTF_PT(string ptNo,int itm,string billId,string billNo,string updateField)
		{
			string _errorMsg = "";
			string _sqlStr = "";			
			_sqlStr = " UPDATE TF_PT SET BIL_ID=@BIL_ID,BIL_NO=@BIL_NO WHERE PT_NO=@PT_NO AND ITM=@ITM AND ISNULL(ITM,-1) <> -1";
			SqlParameter [] _sqlPara = new SqlParameter[4];
			_sqlPara[0] = new SqlParameter("@BIL_NO",SqlDbType.VarChar,20);
			_sqlPara[0].Value = billNo;

			_sqlPara[1] = new SqlParameter("@BIL_ID",SqlDbType.Char,2);
			_sqlPara[1].Value = billId;

            _sqlPara[2] = new SqlParameter("@PT_NO", SqlDbType.VarChar, 20);
			_sqlPara[2].Value = ptNo;

			_sqlPara[3] = new SqlParameter("@ITM",SqlDbType.Int);
			_sqlPara[3].Value = itm;

			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);
			}
			catch (Exception _ex)
			{
				_errorMsg = _ex.Message.ToString();
			}
			return _errorMsg;
		}
				
		/// <summary>
		/// 修改盘点单资料添加结转单据
		/// </summary>
		/// <param name="ptNo">盘点单号</param>
		/// <param name="updateField">盘点单表头修改的字段</param>
		/// <returns></returns>
        public string DeleteDRPPI(string ptNo, string updateField)
		{
			string _errorMsg = "";
			string _sqlStr = "";			
			_sqlStr = " UPDATE MF_PT SET "+updateField+"=NULL WHERE PT_NO=@PT_NO"
                + " UPDATE TF_PT SET TF_PT.BIL_ID=NULL,TF_PT.BIL_NO=MF_PT." + updateField + "  FROM MF_PT WHERE MF_PT.PT_ID=TF_PT.PT_ID AND MF_PT.PT_NO=TF_PT.PT_NO AND MF_PT.PT_NO=@PT_NO ";
			SqlParameter [] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@PT_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = ptNo;

			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);
			}
			catch (Exception _ex)
			{
				_errorMsg = _ex.Message.ToString();
			}
			return _errorMsg;

		}

		#endregion

		#region 计算帐载数量
		/// <summary>
		///	 计算件的帐载数量
		/// </summary>
		/// <param name="bodyDT"></param>
		/// <param name="ptDD"></param>
		/// <param name="hasBln"></param>
		/// <param name="hasBox"></param>
		/// <returns></returns>
		public SunlikeDataSet CalculatePt(SunlikeDataSet bodyDT,DateTime ptDD,bool hasBln,bool hasBox)
		{
			string _sqlStr = "";
			System.Data.SqlClient.SqlConnection _conn = new SqlConnection("Connect Timeout=1000;"+this._connectionString);
			System.Data.SqlClient.SqlCommand _cmd = new SqlCommand();
			try
			{
				_conn.Open();
				_cmd.Connection = _conn;
				_cmd.CommandTimeout = _conn.ConnectionTimeout;
				_cmd.CommandType = System.Data.CommandType.Text;
				//创建临时表
				_sqlStr = "IF EXISTS(SELECT * FROM tempdb..sysobjects WHERE ID=OBJECT_ID('tempdb..#DRPWHPRD'))"
					+ " DROP TABLE #DRPWHPRD"
					+ " Create Table #DRPWHPRD"
					+ " ("
					+ " WH VarChar(12) COLLATE database_default NOT NULL ,"
					+ " PRD_NO VarChar(30) COLLATE database_default NOT NULL ,"
					+ " PRD_MARK VarChar(40) COLLATE database_default NOT NULL,"
					+ " UNIT VARCHAR(1)   COLLATE database_default NOT NULL"
					+ ")";
				_cmd.CommandText = _sqlStr;
				_cmd.ExecuteNonQuery();
				//插入临时表数据
				_sqlStr = "";
				System.Text.StringBuilder _sqlBuilder = new System.Text.StringBuilder();
				for (int i = 0 ; i < bodyDT.Tables["TF_PT"].Rows.Count;i++)
				{	
					_sqlBuilder.Append(
						" INSERT #DRPWHPRD(WH,PRD_NO,PRD_MARK,UNIT)VALUES"
						+"("
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["WH"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["PRD_NO"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["PRD_MARK"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["UNIT"].ToString()+"'"
						+") ");
				}
				if (!String.IsNullOrEmpty(_sqlBuilder.ToString()))
				{
					_cmd.CommandText = _sqlBuilder.ToString();
					_cmd.ExecuteNonQuery();
				}

				//执行存贮过程
				_cmd.CommandType = System.Data.CommandType.StoredProcedure;
				_cmd.CommandText = "sp_calcPT";
				System.Data.SqlClient.SqlParameter _ptddPara = new SqlParameter("@PTDD",System.Data.SqlDbType.DateTime);
				_ptddPara.Value = ptDD;
				_cmd.Parameters.Add(_ptddPara);
				System.Data.SqlClient.SqlParameter _hasBlnPara = new SqlParameter("@HASBLN",SqlDbType.VarChar,1);
				if (hasBln)
					_hasBlnPara.Value = "T";
				else
					_hasBlnPara.Value = "F";
				_cmd.Parameters.Add(_hasBlnPara);
				System.Data.SqlClient.SqlParameter _hasBoxPara = new SqlParameter("@HASBOX",SqlDbType.VarChar,1);
				if (hasBox)
					_hasBoxPara.Value = "T";
				else
					_hasBoxPara.Value = "F";
				_cmd.Parameters.Add(_hasBoxPara);

				System.Data.SqlClient.SqlDataAdapter _sda = new SqlDataAdapter(_cmd);
				SunlikeDataSet _ds = new SunlikeDataSet();
				_sda.Fill(_ds);
				return _ds;
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			finally
			{
				
				_conn.Close();
			}
			
		}
		/// <summary>
		///	  计算箱的帐载数量
		/// </summary>
		/// <param name="bodyDT"></param>
		/// <param name="ptDD"></param>
		/// <param name="hasBln"></param>
		/// <returns></returns>
		public SunlikeDataSet CalculatePtBox(SunlikeDataSet bodyDT,DateTime ptDD,bool hasBln)
		{
			string _sqlStr = "";
			System.Data.SqlClient.SqlConnection _conn = new SqlConnection("Connect Timeout=1000;"+this._connectionString);			
			System.Data.SqlClient.SqlCommand _cmd = new SqlCommand();
			try
			{
				_conn.Open();

				_cmd.Connection = _conn;
				_cmd.CommandTimeout = _conn.ConnectionTimeout;
				_cmd.CommandType = System.Data.CommandType.Text;
				//创建临时表
				_sqlStr = "IF EXISTS(SELECT * FROM tempdb..sysobjects WHERE ID=OBJECT_ID('tempdb..#DRPWHPRDBOX'))"
					+ " DROP TABLE #DRPWHPRDBOX"
					+ " Create Table #DRPWHPRDBOX"
					+ " ("
					+ " WH VarChar(12) COLLATE database_default NOT NULL ,"
					+ " PRD_NO VarChar(30) COLLATE database_default NOT NULL ,"
					+ " CONTENT VarChar(255) COLLATE database_default NOT NULL"
					+ ")";
				_cmd.CommandText = _sqlStr;
				_cmd.ExecuteNonQuery();
				//插入临时表数据
				_sqlStr = "";
				System.Text.StringBuilder _sqlBuilder = new System.Text.StringBuilder();
				for (int i = 0 ; i < bodyDT.Tables["TF_PT3"].Rows.Count;i++)
				{				
					_sqlBuilder.Append("INSERT #DRPWHPRDBOX(WH,PRD_NO,CONTENT)VALUES"
						+"("
						+"'"+bodyDT.Tables["TF_PT3"].Rows[i]["WH"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT3"].Rows[i]["PRD_NO"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT3"].Rows[i]["CONTENT"].ToString()+"'"
						+") ");
				}
				if (!String.IsNullOrEmpty(_sqlBuilder.ToString()))
				{
					_cmd.CommandText = _sqlBuilder.ToString();
					_cmd.ExecuteNonQuery();
				}

				//执行存贮过程
				_cmd.CommandType = System.Data.CommandType.StoredProcedure;
				_cmd.CommandText = "sp_calcPTBox";
				System.Data.SqlClient.SqlParameter _ptddPara = new SqlParameter("@PTDD",System.Data.SqlDbType.DateTime);
				_ptddPara.Value = ptDD;
				_cmd.Parameters.Add(_ptddPara);
				System.Data.SqlClient.SqlParameter _hasBlnPara = new SqlParameter("@HASBLN",SqlDbType.VarChar,1);
				if (hasBln)
					_hasBlnPara.Value = "T";
				else
					_hasBlnPara.Value = "F";
				_cmd.Parameters.Add(_hasBlnPara);

				System.Data.SqlClient.SqlDataAdapter _sda = new SqlDataAdapter(_cmd);
				SunlikeDataSet _ds = new SunlikeDataSet();
				_sda.Fill(_ds);
				return _ds;
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			finally
			{
				_conn.Close();
			}
		}
		/// <summary>
		///  计算件的帐载数量(批号)
		/// </summary>
		/// <param name="bodyDT"></param>
		/// <param name="ptDD"></param>
		/// <param name="hasBln"></param>
		/// <param name="hasBox"></param>
		/// <returns></returns>
		public SunlikeDataSet CalculatePj(SunlikeDataSet bodyDT,DateTime ptDD,bool hasBln,bool hasBox)
		{
			string _sqlStr = "";
			System.Data.SqlClient.SqlConnection _conn = new SqlConnection("Connect Timeout=1000;"+this._connectionString);
			System.Data.SqlClient.SqlCommand _cmd = new SqlCommand();
			try
			{
				_conn.Open();
				_cmd.Connection = _conn;
				_cmd.CommandTimeout = _conn.ConnectionTimeout;
				_cmd.CommandType = System.Data.CommandType.Text;
				//创建临时表
				_sqlStr = "IF EXISTS(SELECT * FROM tempdb..sysobjects WHERE ID=OBJECT_ID('tempdb..#DRPWHBAT'))"
					+ " DROP TABLE #DRPWHBAT"
					+ " Create Table #DRPWHBAT"
					+ " ("
					+ " BAT_NO VarChar(20) COLLATE database_default NOT NULL ,"
					+ " WH VarChar(12) COLLATE database_default NOT NULL ,"
					+ " PRD_NO VarChar(30) COLLATE database_default NOT NULL ,"
					+ " PRD_MARK VarChar(40) COLLATE database_default NOT NULL,"
					+ " UNIT VARCHAR(1)   COLLATE database_default NOT NULL"
					+ ")";
				_cmd.CommandText = _sqlStr;
				_cmd.ExecuteNonQuery();
				//插入临时表数据
				_sqlStr = "";
				System.Text.StringBuilder _sqlBuilder = new System.Text.StringBuilder();
				for (int i = 0 ; i < bodyDT.Tables["TF_PT"].Rows.Count;i++)
				{	
					_sqlBuilder.Append(
						" INSERT #DRPWHBAT(BAT_NO,WH,PRD_NO,PRD_MARK,UNIT)VALUES"
						+"("
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["BAT_NO"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["WH"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["PRD_NO"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["PRD_MARK"].ToString()+"',"
						+"'"+bodyDT.Tables["TF_PT"].Rows[i]["UNIT"].ToString()+"'"
						+") ");
				}
				if (!String.IsNullOrEmpty(_sqlBuilder.ToString()))
				{
					_cmd.CommandText = _sqlBuilder.ToString();
					_cmd.ExecuteNonQuery();
				}

				//执行存贮过程
				_cmd.CommandType = System.Data.CommandType.StoredProcedure;
				_cmd.CommandText = "SP_CALCPJ";
				System.Data.SqlClient.SqlParameter _ptddPara = new SqlParameter("@PTDD",System.Data.SqlDbType.DateTime);
				_ptddPara.Value = ptDD;
				_cmd.Parameters.Add(_ptddPara);
				System.Data.SqlClient.SqlParameter _hasBlnPara = new SqlParameter("@HASBLN",SqlDbType.VarChar,1);
				if (hasBln)
					_hasBlnPara.Value = "T";
				else
					_hasBlnPara.Value = "F";
				_cmd.Parameters.Add(_hasBlnPara);
				System.Data.SqlClient.SqlParameter _hasBoxPara = new SqlParameter("@HASBOX",SqlDbType.VarChar,1);
				if (hasBox)
					_hasBoxPara.Value = "T";
				else
					_hasBoxPara.Value = "F";
				_cmd.Parameters.Add(_hasBoxPara);

				System.Data.SqlClient.SqlDataAdapter _sda = new SqlDataAdapter(_cmd);
				SunlikeDataSet _ds = new SunlikeDataSet();
				_sda.Fill(_ds);
				return _ds;
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			finally
			{
				
				_conn.Close();
			}
			
		}
		#endregion
	}
}
