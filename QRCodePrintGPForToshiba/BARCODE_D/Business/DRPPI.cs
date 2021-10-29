using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.CommonVar;
using Sunlike.Common.Utility;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for DRPPI.
	/// </summary>
	public class DRPPI : BizObject , IAuditing
	{
		private bool _isRunAuditing;	
		private string _loginUsr = "";
		private string _ptId = "";
		private DateTime _timePt_dd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
		/// <summary>
		/// 盘点单
		/// </summary>
		public DRPPI()
		{
		}


		#region 取得数据
		/// <summary>
		/// 取得单据资料
		/// </summary>
        /// <param name="pgm"></param>
		///  <param name="usr">当前用户</param>
		///  <param name="ptId">单据别</param>
		/// <param name="PtNo">盘点单号</param>
		/// <param name="OnlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetUpdateData(string pgm,string usr,string ptId,string PtNo,bool OnlyFillSchema)
		{
			DbDRPPI _pi = new DbDRPPI(Comp.Conn_DB);
            SunlikeDataSet _ds = _pi.GetData(ptId,PtNo, OnlyFillSchema);
			if (usr != null && !String.IsNullOrEmpty(usr))
			{
				Users _users = new Users();
				_ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
				string _pgm = "DRP"+ptId;
                if (!string.IsNullOrEmpty(pgm))
                {
                    _pgm = pgm;
                }
				if (_ds.Tables["MF_PT"].Rows.Count > 0 )
				{
					string _bill_Dep = _ds.Tables["MF_PT"].Rows[0]["DEP"].ToString();
					string _bill_Usr = _ds.Tables["MF_PT"].Rows[0]["USR"].ToString();
					System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr,_bill_Dep,_bill_Usr);
					_ds.ExtendedProperties["UPD"] = _billRight["UPD"];
					_ds.ExtendedProperties["DEL"] = _billRight["DEL"];
					_ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
				}
			}
			SetCanModify(_ds,true);
			//增加条件中的选择模式
			DataTable _dtHead = _ds.Tables["MF_PT"];
			_dtHead.Columns.Add("SELECTMODE");
			_dtHead.Columns.Add("MUTI_PRD_NO");
			
			DataTable _dtBox = _ds.Tables["TF_PT3"];
            _dtBox.Columns.Add("PRD_NO_NO", typeof(System.String));
            //设定箱内容KeyItm为自动递增
			_dtBox.Columns["KEY_ITM"].AutoIncrement = true;
			_dtBox.Columns["KEY_ITM"].AutoIncrementSeed = _dtBox.Rows.Count > 0 ? Convert.ToInt32(_dtBox.Select("","KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
			_dtBox.Columns["KEY_ITM"].AutoIncrementStep = 1;
			_dtBox.Columns["KEY_ITM"].Unique = true;

			DataTable _dtBody = _ds.Tables["TF_PT"];
            _dtBody.Columns.Add("PRD_NO_NO", typeof(System.String));
            //设定表身KeyItm为自动递增
            _dtBody.Columns["PRE_ITM"].AutoIncrement = true;
            _dtBody.Columns["PRE_ITM"].AutoIncrementSeed = _dtBox.Rows.Count > 0 ? Convert.ToInt32(_dtBody.Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _dtBody.Columns["PRE_ITM"].AutoIncrementStep = 1;
            _dtBody.Columns["PRE_ITM"].Unique = true;

            _dtBody.Columns.Add("SPC");
			//增加分销单价
			_dtBody.Columns.Add("UP", typeof(System.Decimal));
			//增加单位标准成本
			DataColumn _up_std = new DataColumn("CST_STD_UNIT",Type.GetType("System.Decimal"));
			_dtBody.Columns.Add(_up_std);
			foreach (DataRow _drBody in _dtBody.Rows)
			{
				_drBody["PRD_NO_NO"] = _drBody["PRD_NO"];
				//给单位标准成本赋值
				if (!String.IsNullOrEmpty(_drBody["CST_STD"].ToString()) && !String.IsNullOrEmpty(_drBody["QTY_RNG"].ToString()) && Convert.ToDecimal(_drBody["QTY_RNG"]) != 0)
				{
					_drBody["CST_STD_UNIT"] = Convert.ToDecimal(_drBody["CST_STD"])/Convert.ToDecimal(_drBody["QTY_RNG"]);
				}
				else
					_drBody["CST_STD_UNIT"] = 0;
			}
			foreach (DataRow _drBox in _dtBox.Rows)
			{
				_drBox["PRD_NO_NO"] = _drBox["PRD_NO"];
			}
//			_dtBody.Columns["KEY_ITM"].AutoIncrement = true;
//			_dtBody.Columns["KEY_ITM"].AutoIncrementSeed = 1;
//			_dtBody.Columns["KEY_ITM"].AutoIncrementStep = 1;
			//创建存放序列号的暂存表
			DataTable _dt = new DataTable("BAR_COLLECT");
			_dt.Columns.Add("ITEM",typeof(int));
			_dt.Columns.Add("BAR_CODE");
			_dt.Columns.Add("BAT_NO");
			_dt.Columns.Add("WH1");
			_dt.Columns.Add("BOX_NO");
			_dt.Columns.Add("PRD_NO");
			_dt.Columns.Add("PRD_MARK");
			_dt.Columns.Add("SERIAL_NO");
            //序列号是否存在、库位和批号
            _dt.Columns.Add("ISEXIST");
            _dt.Columns.Add("WH_REC");
            _dt.Columns.Add("BAT_NO_REC");
            _dt.Columns.Add("STOP_ID");
            _dt.Columns.Add("PH_FLAG");

			DataColumn[] _dca = new DataColumn[1];
			_dca[0] = _dt.Columns["ITEM"];
			_dt.PrimaryKey = _dca;
			_ds.Tables.Add(_dt);
            //把序列号的记录转入暂存表中
            DataView _dv = new DataView(_ds.Tables["PD_BARCODE"]);
            _dv.Sort = "BOX_NO,BAR_CODE";
            for (int i = 0; i < _dv.Count; i++)
            {
                string _barCode = _dv[i]["BAR_CODE"].ToString();
                DataRow _dr = _dt.NewRow();
                _dr["ITEM"] = _dt.Rows.Count + 1;
                _dr["BAR_CODE"] = _barCode;
                if (!String.IsNullOrEmpty(_dv[i]["BOX_NO"].ToString()))
                {
                    _dr["BOX_NO"] = _dv[i]["BOX_NO"];
                }
                BarCode.BarInfo _barInfo = BarCode.GetBarInfo(_barCode);
                _dr["PRD_NO"] = _barInfo.Prd_No;
                _dr["PRD_MARK"] = _barInfo.Prd_Mark;
                _dr["SERIAL_NO"] = _barInfo.Serial_No;
                _dt.Rows.Add(_dr);
            }
            //取序列号库位、批号
            foreach (DataRow dr in _dt.Rows)
            {
                DataRow[] _aryDrBar = _ds.Tables["PD_BARCODE"].Select("BAR_CODE='" + dr["BAR_CODE"].ToString() + "'");
                if (_aryDrBar.Length > 0)
                {
                    DataRow[] _aryDr = _ds.Tables["PD_BARCODE"].Select("PT_ITM=" + _aryDrBar[0]["PT_ITM"].ToString());
                    if (_aryDr.Length > 0)
                    {
                        dr["WH1"] = _aryDr[0]["WH"];
                        dr["BAT_NO"] = _aryDr[0]["BAT_NO"];
                    }
                }
            }
            _dv.Dispose();
            GC.Collect(GC.GetGeneration(_dv));
			_ds.AcceptChanges();
			return _ds;
		}

		/// <summary>
		/// 检查是否可以修改
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="bCheckAuditing"></param>
		private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
		{
			string errorMsg = "";
			DataTable _dtMf = ds.Tables["MF_PT"];
			if (_dtMf.Rows.Count > 0)
			{
				bool _bCanModify = true;
				if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["PT_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
				{
					_bCanModify = false;
                    errorMsg += "COMMON.HINT.CLOSE_CLS";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");                   
				}
				if (bCheckAuditing)
				{
					Auditing _aud = new Auditing();
					if (_aud.GetIfEnterAuditing("PT", _dtMf.Rows[0]["PT_NO"].ToString()))
					{
						_bCanModify = false;
                        errorMsg += "COMMON.HINT.CLOSE_AUDIT";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
					}
				}
                //判断是否锁单
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    errorMsg += " COMMON.HINT.CLOSE_LOCK";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                } 
				ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0 ,1);
			}
			return errorMsg;
		}

		#endregion

		#region 按条件取得盘点单的表身		
		/// <summary>
		/// 按条件取得盘点单的表身
		/// </summary>
		/// <param name="selectMode">选择模式</param>
		/// <param name="cusNo">客户代号</param>
		/// <param name="StartWh">起始库位</param>
		/// <param name="EndWh">截止库位</param>
		/// <param name="StartIndx">起始中类</param>
		/// <param name="EndIndx">截止中类</param>
		/// <param name="StartPrdNo">起始品号</param>
		/// <param name="EndPrdNo">截止品号</param>
		/// <param name="prdNos">多选品名</param>
		/// <param name="StartPrdMark">起始特征</param>
		/// <param name="EndPrdMark">截止特征</param>
		/// <returns></returns>
		public SunlikeDataSet GetPrdt1(string selectMode,string cusNo, string StartWh,string EndWh,string StartIndx,string EndIndx,string StartPrdNo,string EndPrdNo,string prdNos,string StartPrdMark,string EndPrdMark)
		{
			DbDRPPI _pi = new DbDRPPI(Comp.Conn_DB);
			SunlikeDataSet _ds = _pi.GetPrdt1(selectMode,cusNo,StartWh,EndWh,StartIndx,EndIndx,StartPrdNo,EndPrdNo,prdNos,StartPrdMark,EndPrdMark);
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
		/// <param name="prdNos">多选品名</param>		
		/// <param name="startPrdMark">起始特征</param>
		/// <param name="endPrdMark">截止特征</param>
		/// <returns></returns>
		public SunlikeDataSet GetBatRec1(string cusNo,string startBatNo,string endBatNo,string startWh,string endWh,string startIdxNo,string endIdxNo,string startPrdNo,string endPrdNo,string prdNos,string startPrdMark,string endPrdMark)
		{
			DbDRPPI _pi = new DbDRPPI(Comp.Conn_DB);
			return _pi.GetBatRec1(cusNo,startBatNo,endBatNo,startWh,endWh,startIdxNo,endIdxNo,startPrdNo,endPrdNo,prdNos,startPrdMark,endPrdMark);
		}

        public SunlikeDataSet GetBarRec(string cusNo, string startBatNo, string endBatNo, string startWh, string endWh, string startIdxNo, string endIdxNo, string startPrdNo, string endPrdNo, string prdNos, string startPrdMark, string endPrdMark)
        {
            DbDRPPI _pi = new DbDRPPI(Comp.Conn_DB);
            return _pi.GetBarRec(cusNo, startBatNo, endBatNo, startWh, endWh, startIdxNo, endIdxNo, startPrdNo, endPrdNo, prdNos, startPrdMark, endPrdMark);
        }

		#endregion

		#region 计算帐载数量
		/// <summary>
		/// 计算帐载数量
		/// </summary>
		/// <param name="bodyDT">表身数据表</param>
		/// <param name="ptDD1">帐载日期</param>
		/// <param name="hasBln">含借入出否：T/F</param>
		/// <param name="hasBox">是否包含箱</param>
		/// <returns></returns>
		public void CalculatePt(SunlikeDataSet bodyDT,DateTime ptDD1,bool hasBln,bool hasBox)
		{
			DbDRPPI _dbDRPPI = new DbDRPPI(Comp.Conn_DB);
			SunlikeDataSet _caculatedDS = _dbDRPPI.CalculatePt(bodyDT,ptDD1,hasBln,hasBox);
			for (int i = 0 ; i < _caculatedDS.Tables[0].Rows.Count;i++)
			{
				DataRow[] _selDR = bodyDT.Tables["TF_PT"].Select(" ISNULL(BAT_NO,'') = '' AND  WH='"+_caculatedDS.Tables[0].Rows[i]["WH"].ToString()+"' AND PRD_NO='"+_caculatedDS.Tables[0].Rows[i]["PRD_NO"].ToString()+"' AND PRD_MARK='"+_caculatedDS.Tables[0].Rows[i]["PRD_MARK"].ToString()+"'");
				if (_selDR.Length > 0 )
				{
					for (int j = 0 ; j < _selDR.Length;j++)
					{
						_selDR[j]["QTY1"] = _caculatedDS.Tables[0].Rows[i]["QTY"];
						_selDR[j]["CST_BOOK"] = _caculatedDS.Tables[0].Rows[i]["CST"];
						_selDR[j]["CST_STD"] = _caculatedDS.Tables[0].Rows[i]["CST_STD"];
					}
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="bodyDT"></param>
		/// <param name="ptDD1"></param>
		/// <param name="hasBln"></param>
		public void CalculatePtBox(SunlikeDataSet bodyDT,DateTime ptDD1,bool hasBln)
		{
			DbDRPPI _dbDRPPI = new DbDRPPI(Comp.Conn_DB);
			SunlikeDataSet _caculatedDS = _dbDRPPI.CalculatePtBox(bodyDT,ptDD1,hasBln);
			for (int i = 0 ; i < _caculatedDS.Tables[0].Rows.Count;i++)
			{
				DataRow[] _selDR = bodyDT.Tables["TF_PT3"].Select(" WH='"+_caculatedDS.Tables[0].Rows[i]["WH"].ToString()+"' AND PRD_NO='"+_caculatedDS.Tables[0].Rows[i]["PRD_NO"].ToString()+"' AND CONTENT='"+_caculatedDS.Tables[0].Rows[i]["CONTENT"].ToString()+"'");
				if (_selDR.Length > 0 )
				{
					for (int j = 0 ; j < _selDR.Length;j++)
					{
						_selDR[j]["QTY1"] = _caculatedDS.Tables[0].Rows[i]["QTY"];
					}
				}
			}
		}
		/// <summary>
		///  计算件的帐载数量(批号)
		/// </summary>
		/// <param name="bodyDT"></param>
		/// <param name="ptDD1"></param>
		/// <param name="hasBln"></param>
		/// <param name="hasBox"></param>
		/// <returns></returns>
		public void CalculatePj(SunlikeDataSet bodyDT,DateTime ptDD1,bool hasBln,bool hasBox)
		{
			DbDRPPI _dbDRPPI = new DbDRPPI(Comp.Conn_DB);
			SunlikeDataSet _caculatedDS = _dbDRPPI.CalculatePj(bodyDT,ptDD1,hasBln,hasBox);
			for (int i = 0 ; i < _caculatedDS.Tables[0].Rows.Count;i++)
			{
                DataRow[] _selDR = bodyDT.Tables["TF_PT"].Select(" ISNULL(BAT_NO,'') <> '' AND   BAT_NO='" + _caculatedDS.Tables[0].Rows[i]["BAT_NO"].ToString() + "' AND WH='" + _caculatedDS.Tables[0].Rows[i]["WH"].ToString() + "' AND PRD_NO='" + _caculatedDS.Tables[0].Rows[i]["PRD_NO"].ToString() + "' AND PRD_MARK='" + _caculatedDS.Tables[0].Rows[i]["PRD_MARK"].ToString() + "'");
				if (_selDR.Length > 0 )
				{
					for (int j = 0 ; j < _selDR.Length;j++)
					{
						_selDR[j]["QTY1"] = _caculatedDS.Tables[0].Rows[i]["QTY"];
						_selDR[j]["CST_BOOK"] = _caculatedDS.Tables[0].Rows[i]["CST"];
						_selDR[j]["CST_STD"] = _caculatedDS.Tables[0].Rows[i]["CST_STD"];
					}
				}
			}
		}
        /// <summary>
        /// 计算序列号盘点
        /// </summary>
        /// <param name="bodyDT"></param>
        /// <param name="ptDD1"></param>
        /// <param name="hasBln"></param>
        /// <param name="hasBox"></param>
        public void CalculatePQ(SunlikeDataSet bodyDT, DateTime ptDD1, bool hasBln, bool hasBox)
        {
            DbDRPPI _dbDRPPI = new DbDRPPI(Comp.Conn_DB);
            SunlikeDataSet _ds = new SunlikeDataSet();
            DataTable _dtPI = bodyDT.Tables["TF_PT"].Copy();
            DataTable _dtPJ = bodyDT.Tables["TF_PT"].Copy();                       
            //取无批号产品信息
            for (int i = 0; i < _dtPI.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(_dtPI.Rows[i]["BAT_NO"].ToString()))
                {
                    _dtPI.Rows[i].Delete();
                }
            }
            _dtPI.AcceptChanges();
            _ds.Tables.Add(_dtPI);
            //取有批号产品信息
            for (int i = 0; i < _dtPJ.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(_dtPJ.Rows[i]["BAT_NO"].ToString()))
                {
                    _dtPJ.Rows[i].Delete();
                }
            }
            _dtPJ.AcceptChanges();
            _ds.Tables.Add(_dtPJ);
            //计算无批号                                            
            SunlikeDataSet _caculatedPIDS = _dbDRPPI.CalculatePt(_ds, ptDD1, hasBln, hasBox);
            for (int i = 0; i < _caculatedPIDS.Tables[0].Rows.Count; i++)
            {
                DataRow[] _selDR = bodyDT.Tables["TF_PT"].Select(" WH='" + _caculatedPIDS.Tables[0].Rows[i]["WH"].ToString() + "' AND PRD_NO='" + _caculatedPIDS.Tables[0].Rows[i]["PRD_NO"].ToString() + "' AND PRD_MARK='" + _caculatedPIDS.Tables[0].Rows[i]["PRD_MARK"].ToString() + "'");
                if (_selDR.Length > 0)
                {
                    for (int j = 0; j < _selDR.Length; j++)
                    {
                        _selDR[j]["QTY1"] = _caculatedPIDS.Tables[0].Rows[i]["QTY"];
                        _selDR[j]["CST_BOOK"] = _caculatedPIDS.Tables[0].Rows[i]["CST"];
                        _selDR[j]["CST_STD"] = _caculatedPIDS.Tables[0].Rows[i]["CST_STD"];
                    }
                }
            }
            //计算有批号
            SunlikeDataSet _caculatedPJDS = _dbDRPPI.CalculatePj(_ds, ptDD1, hasBln, hasBox);
            for (int i = 0; i < _caculatedPJDS.Tables[0].Rows.Count; i++)
            {
                DataRow[] _selDR = bodyDT.Tables["TF_PT"].Select(" BAT_NO='" + _caculatedPJDS.Tables[0].Rows[i]["BAT_NO"].ToString() + "' AND WH='" + _caculatedPJDS.Tables[0].Rows[i]["WH"].ToString() + "' AND PRD_NO='" + _caculatedPJDS.Tables[0].Rows[i]["PRD_NO"].ToString() + "' AND PRD_MARK='" + _caculatedPJDS.Tables[0].Rows[i]["PRD_MARK"].ToString() + "'");
                if (_selDR.Length > 0)
                {
                    for (int j = 0; j < _selDR.Length; j++)
                    {
                        _selDR[j]["QTY1"] = _caculatedPJDS.Tables[0].Rows[i]["QTY"];
                        _selDR[j]["CST_BOOK"] = _caculatedPJDS.Tables[0].Rows[i]["CST"];
                        _selDR[j]["CST_STD"] = _caculatedPJDS.Tables[0].Rows[i]["CST_STD"];
                    }
                }
            }
            //计算箱
            CalculatePtBox(bodyDT, ptDD1, hasBln);
        }
		#endregion

		#region 保存盘点单

		#region 保存盘点单
		/// <summary>
		/// 保存盘点单
		/// </summary>
        /// <param name="pgm"></param>
		/// <param name="changedDs">DataSet</param>
		/// <param name="bubbleException">是否抛出异常（true为直接抛出异常，false返回ErrorTable）</param>
		/// <returns></returns>
		public DataTable UpdateData(string pgm,SunlikeDataSet changedDs,bool bubbleException)
		{
			DataTable _mf_ptTable = changedDs.Tables["MF_PT"];
            
			
			#region 取得单据的审核状态
			if (_mf_ptTable.Rows[0].RowState != DataRowState.Deleted)
			{
                this._ptId = _mf_ptTable.Rows[0]["PT_ID"].ToString(); 
				_loginUsr = _mf_ptTable.Rows[0]["USR"].ToString();                
				if (!(_mf_ptTable.Rows[0]["PT_DD"] is System.DBNull))
				{
					_timePt_dd = Convert.ToDateTime(_mf_ptTable.Rows[0]["PT_DD"]);
				}
			}
			else
			{
                this._ptId = _mf_ptTable.Rows[0]["PT_ID", System.Data.DataRowVersion.Original].ToString(); 
				_loginUsr = _mf_ptTable.Rows[0]["USR",System.Data.DataRowVersion.Original].ToString();
				if (!(_mf_ptTable.Rows[0]["PT_DD",System.Data.DataRowVersion.Original] is System.DBNull))
				{
					_timePt_dd = Convert.ToDateTime(_mf_ptTable.Rows[0]["PT_DD",System.Data.DataRowVersion.Original]);
				}
			}
			Auditing _auditing = new Auditing();
            DataRow _dr = _mf_ptTable.Rows[0];
            string bilType = "";
            if (_dr.Table.Columns.Contains("BIL_TYPE"))
            {
                if (_dr.RowState == DataRowState.Deleted)
                    bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
                else
                    bilType = _dr["BIL_TYPE"].ToString();
            }
            string _mobID = "";
            if (_dr.Table.Columns.Contains("MOB_ID"))
            {
                if (_dr.RowState == DataRowState.Deleted)
                    _mobID = _dr["MOB_ID", DataRowVersion.Original].ToString();
                else
                    _mobID = _dr["MOB_ID"].ToString();
            }
            //_isRunAuditing = _auditing.IsRunAuditing(this._ptId, _loginUsr, bilType, _mobID);		


            #endregion
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
			_ht.Add("MF_PT","PT_ID,PT_NO,PT_DD,PT_DD1,PT_MTH,F_WH,BAT_NO,F_BAT_NO,E_BAT_NO,E_WH,F_PRD_NO,E_PRD_NO,RATE,MAN_NO,REM,USR,CHK_MAN,PRT_SW,CPY_SW,F_IDX_NO,E_IDX_NO,CLS_DATE,SYS_DATE,MOB_ID,BIL_TYPE,CUS_NO,IJ_NO,IJ_NO1,SA_NO,DEP,BLN_OK");
            _ht.Add("TF_PT", "PT_ID,PT_NO,ITM,PRD_NO,PRD_MARK,WH,UNIT,QTY1,QTY2,QTY11,QTY21,CST_BOOK,CST_INV,CST_DIFF,CST_STD,QTY_RNG,QTY1_RNG,DISCRIPT,BOX_ITM,BIL_NO,BIL_ID,BAT_NO,PRE_ITM");
            _ht.Add("TF_PT3", "PT_ID,PT_NO,ITM,PRD_NO,CONTENT,WH,QTY1,QTY2,QTY_RNG,KEY_ITM,DISCRIPT");
            _ht.Add("PD_BARCODE", "PT_ID,PT_NO,PT_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO");
            _ht.Add("PD_BARCODE_IJ", "PT_ID,PT_NO,PT_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO,PL_FLAG,WH_BAK,BAT_NO_BAK,UPDDATE_BAK,PH_FLAG_BAK,STOP_ID_BAK");
			this.UpdateDataSet(changedDs,_ht);
            if (changedDs.HasErrors)
            {
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDs);
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=DRPPI.UpdateData() Error:;" + _errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changedDs);
                }
            }
            else
            {
                //增加单据权限
                string _UpdUsr = "";
                if (changedDs.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changedDs.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "DRP" + this._ptId;
                    if (!string.IsNullOrEmpty(pgm))
                    {
                        _pgm = pgm;
                    }
                    DataTable _dtMf = changedDs.Tables["MF_PT"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changedDs.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDs.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDs.ExtendedProperties["PRN"] = _billRight["PRN"];
                       changedDs.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
            }
			return Sunlike.Business.BizObject.GetAllErrors(changedDs);
		}
		#endregion

		#region BeforeUpdate
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="status"></param>
		protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
		{
            string _ptId = "", _ptNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _ptId = dr["PT_ID"].ToString();
                _ptNo = dr["PT_NO"].ToString();
            }
            else
            {
                _ptId = dr["PT_ID", DataRowVersion.Original].ToString();
                _ptNo = dr["PT_NO", DataRowVersion.Original].ToString();
            }
            if (statementType != StatementType.Insert)
            {
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "PT_ID = '" + _ptId + "' AND PT_NO = '" + _ptNo + "'";
                if (_Users.IsLocked("MF_PT", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
			#region 表头
			if (tableName =="MF_PT")
			{
				string _cusNo = "";	//客户代号
				string _depNo = "";	//部门
				string _salNo = ""; //盘点人员
				
				if (statementType != StatementType.Delete)
				{
					#region 计算表头信息是否正确
					//客户代号检测				
					_cusNo = dr["CUS_NO"].ToString();
					if (!String.IsNullOrEmpty(_cusNo))
					{
						Cust _cust = new Cust();
						if (_cust.IsExist(_loginUsr,_cusNo, Convert.ToDateTime(dr["PT_DD"])) == false)
						{						
							dr.SetColumnError("CUS_NO",/*客户代号不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM="+_cusNo+"");
							status = UpdateStatus.SkipAllRemainingRows;
						}
					}
					//部门检测
					_depNo =  dr["DEP"].ToString();
					if (!String.IsNullOrEmpty(_depNo))
					{
						Dept _dept = new Dept();
						if (_dept.IsExist(_loginUsr,_depNo, Convert.ToDateTime(dr["PT_DD"])) == false)
						{
							dr.SetColumnError("DEP",/*部门代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.DEPTERROR,PARAM="+_depNo);
							status = UpdateStatus.SkipAllRemainingRows;
						}
					}
					//盘点人员检测（必填）
					_salNo = dr["MAN_NO"].ToString();				
					Salm _salm = new Salm();
					if (_salm.IsExist(_loginUsr,_salNo, Convert.ToDateTime(dr["PT_DD"])) == false)
					{
						dr.SetColumnError("MAN_NO",/*业务员代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM="+_salNo);
						status = UpdateStatus.SkipAllRemainingRows;
					}
					#endregion

					#region 必添项
					//含借出借入否
					//string _dept = _users.GetUserDepNo(_loginUsr);
					//				string _dep_chk = Comp.GetCompInfo(_dept,"DEP_CHK");
					//				if (_dep_chk == "T")
					//				{
					dr["BLN_OK"] = "T";
					//				}
					//				else
					//				{
					//					dr["BLN_OK"] = "T";
					//				}
					if (statementType == StatementType.Insert)
					{
						if (this._ptId == "PJ")
						{
							if (dr.Table.DataSet.Tables.Contains("TF_PT") && dr.Table.DataSet.Tables["TF_PT"].Rows.Count > 0 )
							{
								dr["BAT_NO"] = dr.Table.DataSet.Tables["TF_PT"].Rows[0]["BAT_NO"].ToString();
							}
						}
					}					

					#endregion

					#region 审核
					//审核
					if(!_isRunAuditing)//不需要审核
					{
						if (statementType == StatementType.Insert)//更改审核条件时不需要更改之前的资料
						{	
							dr["CHK_MAN"] = _loginUsr;
							dr["CLS_DATE"] = _timePt_dd;
						}
					}
					else
					{
						dr["CHK_MAN"] = System.DBNull.Value;
						dr["CLS_DATE"] = System.DBNull.Value;
					}
					#endregion
				}
				if (statementType == StatementType.Insert)
				{
					#region --生成单号 
					string _dept = "";
					string _userId = "";
					string _pt_no = "";
					string _bil_type = "";
					SQNO SunlikeSqNo = new SQNO();
					_userId = dr["USR"].ToString();
					_dept = "";//部门
					_bil_type =dr["BIL_TYPE"].ToString();
					_pt_no = SunlikeSqNo.Set(this._ptId,_userId,_dept,_timePt_dd,_bil_type);
					dr["PT_NO"] = _pt_no;
					dr["PT_DD"] = _timePt_dd.ToShortDateString();
					dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
					dr["PRT_SW"] = "N";
					#endregion
				}
			}
			#endregion

			#region 表身
            #region     TF_PT
            if (tableName =="TF_PT")
			{
				if (statementType != StatementType.Delete)
				{
					#region --计算表身信息是否正确
					Prdt SunlikePrdt = new Prdt();
					//产品检测
					string _prd_no = dr["PRD_NO"].ToString();
					if (!String.IsNullOrEmpty(_prd_no))
					{

						
						if (SunlikePrdt.IsExist(_loginUsr,_prd_no, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PT"].Rows[0]["PT_DD"])) == false)
						{
							dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM="+_prd_no);
							status = UpdateStatus.SkipAllRemainingRows;
						}					
					}
					//仓库
					string _wh =  dr["WH"].ToString();
					if (!String.IsNullOrEmpty(_wh))
					{
						WH SunlikeWH = new WH();
						if (SunlikeWH.IsExist(_loginUsr,_wh, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PT"].Rows[0]["PT_DD"])) == false)
						{
							dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM="+_wh);
							status = UpdateStatus.SkipAllRemainingRows;
						}					
					}
					//PMARK
					string _mark = dr["PRD_MARK"].ToString();
					PrdMark _prd_Mark = new PrdMark();
                    if (_prd_Mark.RunByPMark(_loginUsr))
                    {
						string [] _prd_markAry = _prd_Mark.BreakPrdMark(_mark);
						DataTable _markTable = _prd_Mark.GetSplitData("");
						for (int i = 0; i < _markTable.Rows.Count;i ++)
						{
							string _markName = _markTable.Rows[i]["FLDNAME"].ToString();
							if (!_prd_Mark.IsExist(_markName,dr["PRD_NO"].ToString(),_prd_markAry[i]))
							{
								dr.SetColumnError(_markName,/*货品特征[{0}]不存在,请检查*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM="+_prd_markAry[i].Trim());
								status = UpdateStatus.SkipAllRemainingRows;
							}
						}					
					}
					//批号检测
					if (this._ptId == "PJ")
					{
						if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
						{
							Bat _bat = new Bat();
							if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
							{
								dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM="+dr["BAT_NO"].ToString());
								status = UpdateStatus.SkipAllRemainingRows;
							}
						}
					}
					#endregion	
				}
            }
            #endregion

            #region  PD_BARCODE_IJ
            if (tableName == "PD_BARCODE_IJ")
            {
                if (statementType != StatementType.Delete)
                {
                    #region --计算信息是否正确
                    Prdt SunlikePrdt = new Prdt();
                    //产品检测
                    if (!String.IsNullOrEmpty(dr["PRD_NO"].ToString()))
                    {


                        if (SunlikePrdt.IsExist(_loginUsr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PT"].Rows[0]["PT_DD"])) == false)
                        {
                            dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + dr["PRD_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //特征检测                    
                    if (string.IsNullOrEmpty(dr["BOX_NO"].ToString()))
                    {
                        PrdMark _prd_Mark = new PrdMark();
                        if (_prd_Mark.RunByPMark(_loginUsr))
                        {
                            string[] _prd_markAry = _prd_Mark.BreakPrdMark(dr["PRD_MARK"].ToString());
                            DataTable _markTable = _prd_Mark.GetSplitData("");
                            for (int i = 0; i < _markTable.Rows.Count; i++)
                            {
                                string _markName = _markTable.Rows[i]["FLDNAME"].ToString();
                                if (!_prd_Mark.IsExist(_markName, dr["PRD_NO"].ToString(), _prd_markAry[i]))
                                {
                                    dr.SetColumnError(_markName,/*货品特征[{0}]不存在,请检查*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[i].Trim());
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #endregion

            base.BeforeUpdate (tableName, statementType, dr, ref status);
		}
		#endregion

		#region AfterUpdate
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="status"></param>
		/// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {

            Auditing _auditing = new Auditing();
            string _pt_no = "";	 //盘点单号
            string _ijNo = "";//调整条调增单号
            string _ijNo1 = "";//调整条调减单号
            string _saNo = "";//销货单号			
            string _brNo = "";//条码变更单

            #region 表头
            if (tableName == "MF_PT")
            {
                if (statementType == StatementType.Delete)
                {
                    #region 删除时在BILD中插入一笔数据
                    _pt_no = dr["PT_NO", DataRowVersion.Original].ToString();
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(_pt_no, _loginUsr);//删除时在BILD中插入一笔数据
                    #endregion

                    #region 盘点单删除时同时删除对应的数据
                    _pt_no = "";
                    _ijNo = "";
                    _ijNo1 = "";
                    _saNo = "";
                    _pt_no = dr["PT_NO", DataRowVersion.Original].ToString();
                    _ijNo = dr["IJ_NO", DataRowVersion.Original].ToString();
                    _ijNo1 = dr["IJ_NO1", DataRowVersion.Original].ToString();
                    _saNo = dr["SA_NO", DataRowVersion.Original].ToString();
                    _brNo = dr["BR_NO", DataRowVersion.Original].ToString();
                    //删除调增
                    if (!String.IsNullOrEmpty(_ijNo))
                    {
                        Sunlike.Business.DRPIJ _drpIJ = new DRPIJ();
                        SunlikeDataSet _drpIJDS = _drpIJ.GetData(_loginUsr, "IJ", _ijNo, false);
                        if ((_drpIJDS.ExtendedProperties.ContainsKey("DEL")) && (_drpIJDS.ExtendedProperties["DEL"].ToString() == "Y"))
                        {
                            _drpIJDS.Tables["MF_IJ"].Rows[0].Delete();
                            _drpIJ.UpdateData(null,_drpIJDS, true);
                        }
                        else
                        {
                            dr.SetColumnError("IJ_NO",/*调整单[{0}]删除不成功!*/"RCID=INV.HINT.IJFAIL,PARAM=" + _ijNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                    }
                    //删除调减
                    if (!String.IsNullOrEmpty(_ijNo1))
                    {
                        Sunlike.Business.DRPIJ _drpIJ = new DRPIJ();
                        SunlikeDataSet _drpIJDS = _drpIJ.GetData(_loginUsr, "IJ", _ijNo1, false);
                        if ((_drpIJDS.ExtendedProperties.ContainsKey("DEL")) && (_drpIJDS.ExtendedProperties["DEL"].ToString() == "Y"))
                        {
                            _drpIJDS.Tables["MF_IJ"].Rows[0].Delete();
                            _drpIJ.UpdateData(null,_drpIJDS, true);
                        }
                        else
                        {
                            dr.SetColumnError("IJ_NO1",/*调整单[{0}]删除不成功!*/"RCID=INV.HINT.IJFAIL,PARAM=" + _ijNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                    }
                    //删除销货单
                    if (!String.IsNullOrEmpty(_saNo))
                    {
                        Sunlike.Business.DRPSA _drpSA = new DRPSA();
                        SunlikeDataSet _drpSADS = _drpSA.GetData("", _loginUsr, "SA", _saNo);
                        if ((_drpSADS.ExtendedProperties.ContainsKey("DEL")) && (_drpSADS.ExtendedProperties["DEL"].ToString() == "Y"))
                        {
                            _drpSADS.Tables["MF_PSS"].Rows[0].Delete();
                            _drpSA.UpdateData("DRPSA",_drpSADS);
                        }
                        else
                        {
                            dr.SetColumnError("SA_NO",/*销货单[{0}]删除不成功!*/"RCID=INV.HINT.SAFAIL,PARAM=" + _saNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //删除条码变更单
                    if (!string.IsNullOrEmpty(_brNo))
                    {
                        Sunlike.Business.DRPBR _drpBR = new DRPBR();
                        SunlikeDataSet _drpBRDS = _drpBR.GetData(_loginUsr, _brNo, false);
                        if ((_drpBRDS.ExtendedProperties.ContainsKey("DEL")) && (_drpBRDS.ExtendedProperties["DEL"].ToString() == "Y"))
                        {
                            _drpBRDS.Tables["MF_BAR"].Rows[0].Delete();
                            _drpBR.UpdateData(_drpBRDS);
                        }
                        else
                        {
                            dr.SetColumnError("BR_NO",/*条码变更单[{0}]删除不成功!*/"RCID=INV.HINT.BRFAIL,PARAM=" + _brNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion

                }
                else
                {
                    _pt_no = dr["PT_NO"].ToString();
                }
               
                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["PT_DD"]);
                //    _aps.BIL_ID = this._ptId;
                //    _aps.BIL_NO = _pt_no;
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["MAN_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //    _aps = new AudParamStruct(_ptId, _pt_no);
                //_auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
            #endregion


            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
		#endregion

        #region BeforeDsSave
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dtMf = ds.Tables["MF_PT"];
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["PT_NO"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["PT_NO", DataRowVersion.Original].ToString();
            //    }
            //    if (_bilId.IndexOf("PI") > -1)
            //    {
            //        _bilId = "PI";
            //    }
            //    else if (_bilId.IndexOf("PJ") > -1)
            //    {
            //        _bilId = "PJ";
            //    }
            //    else
            //    {
            //        _bilId = "PI";
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion
        }
        #endregion
       
        #endregion

        #region 调整单,销售单回写盘点单表身
        /// <summary>
		///	 调整单,销售单回写盘点单表身
		/// </summary>
		/// <param name="ptNo"></param>
		/// <param name="billId"></param>
		/// <param name="billNo"></param>
		/// <param name="isPlus"></param>
		/// <returns></returns>
		public string UpdateMF_PT(string ptNo,string billId,string billNo,bool isPlus)
		{
			string _errorMsg = "";
			string _updateField = "";            
			
			if (billId == "SA")
			{
				_updateField = "SA_NO";
			}
            else if (billId == "BR")            
            {
                _updateField = "BR_NO";
            }
            else
            {
                if (isPlus)
                {
                    _updateField = "IJ_NO";
                }
                else
                {
                    _updateField = "IJ_NO1";
                }
            }
			DbDRPPI _dbDrpPI = new DbDRPPI(Comp.Conn_DB);
            string _ptId = ptNo.Substring(0,2);
            SunlikeDataSet _ds = this.GetUpdateData("",null, _ptId, ptNo, false);
            if (_ds.Tables[0].Rows.Count == 0)
            {
                _errorMsg = "RCID=COMMON.HINT.OS_NO_NULL";
                return _errorMsg;
            }
            _errorMsg = _dbDrpPI.UpdateMF_PT(ptNo, billNo, _updateField);
			return _errorMsg;
		}

		/// <summary>
		/// 修改盘点单表身资料添加结转单据
		/// </summary>
        /// <param name="ptId">要回写的盘点单据别</param>
		/// <param name="ptNo">要回写的盘点单号</param>
		/// <param name="itm">要回写的项次</param>
		/// <param name="billId">回写识别码</param>
		/// <param name="billNo">回写单号</param>
		/// <param name="isPlus">是否新增</param>
		/// <returns></returns>
		public string UpdateTF_PT(string ptNo,int itm,string billId,string billNo,bool isPlus)
		{
			string _errorMsg = "";
			string _updateField = "";

			if (billId == "SA")
			{
				_updateField = "SA_NO";
			}
			else
			{
				if (isPlus)
				{
					_updateField = "IJ_NO";
				}
				else
				{
					_updateField = "IJ_NO1";
				}
			}
			DbDRPPI _dbDrpPI = new DbDRPPI(Comp.Conn_DB);
            _errorMsg = _dbDrpPI.UpdateTF_PT(ptNo, itm, billId, billNo, _updateField);			
			return _errorMsg;
		}

		/// <summary>
		///	 删除盘点单表身资料的结转单据
		/// </summary>
		/// <param name="ptNo">盘点单号</param>
		/// <param name="billId">识别码</param>
		/// <param name="isPlus"></param>
		/// <returns></returns>
		public string DeleteDRPPI(string ptNo,string billId,bool isPlus)
		{
			string _errorMsg = "";
			string _updateField = "";

			if (billId == "SA")
			{
				_updateField = "SA_NO";
			}
            else if (billId == "BR")
            {
                _updateField = "BR_NO";
            }
			else
			{
				if (isPlus)
				{
					_updateField = "IJ_NO";
				}
				else
				{
					_updateField = "IJ_NO1";
				}
			}
			DbDRPPI _dbDrpPI = new DbDRPPI(Comp.Conn_DB);
            _errorMsg = _dbDrpPI.DeleteDRPPI(ptNo, _updateField);
			return _errorMsg;
		}

		#endregion

		#region 审核单据
		/// <summary>
		/// 改变审核状态变成审核
		/// </summary>
        /// <param name="ptId"></param>
		/// <param name="ptNo">受订单号</param>
		/// <param name="chk_man">审核人</param>
		/// <param name="cls_dd">终审日期</param>
		public void ClosePI(string ptId,string ptNo,string chk_man,DateTime cls_dd)
		{
			DbDRPPI _drpPI = new DbDRPPI(Comp.Conn_DB);
            _drpPI.ClosePI(ptId,ptNo, chk_man, cls_dd);
		}
		/// <summary>
		/// 反审核
		/// </summary>
        /// <param name="ptId"></param>
		/// <param name="ptNo"></param>
        public void RollbackPI(string ptId, string ptNo)
		{
			DbDRPPI _drpPI = new DbDRPPI(Comp.Conn_DB);
            _drpPI.RollbackPI(ptId,ptNo);
		}
		#endregion

		#region IAuditing Members
		/// <summary>
		/// 审核不同意
		/// </summary>
		/// <param name="pBB_ID"></param>
		/// <param name="pBB_NO"></param>
		/// <param name="pCHK_MAN"></param>
		/// <param name="chk_DD"></param>
		/// <returns></returns>
		public string Deny(string pBB_ID,string pBB_NO,string pCHK_MAN,System.DateTime chk_DD)
		{
			return "";
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="bil_id"></param>
		/// <param name="bil_no"></param>
		/// <param name="chk_man"></param>
		/// <param name="cls_dd"></param>
		/// <returns></returns>
		public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
		{
            ClosePI(bil_id,bil_no, chk_man, cls_dd);
			return "";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bil_id"></param>
		/// <param name="bil_no"></param>
		/// <returns></returns>
		public string RollBack(string bil_id, string bil_no)
		{
			try
			{
				SunlikeDataSet _ds = this.GetUpdateData("",null,bil_id,bil_no,false);
				string _errorMsg = this.SetCanModify(_ds, false);
				if (_ds.ExtendedProperties.Contains("CAN_MODIFY"))
				{
					if (_errorMsg != "COMMON.HINT.NOFX" && _ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
					{
						if (_errorMsg.Length > 0 )
							return "RCID=INV.HINT.CANMODIFY1;RCID="+_errorMsg;
						else
							return "RCID=INV.HINT.CANMODIFY1;";
					}
				}
                RollbackPI(bil_id,bil_no);
				// TODO:  Add DRPPI.RollBack implementation
			}
			catch (Exception _ex)
			{
				return _ex.Message.ToString();
			}
			return "";
		}

		#endregion
	}
}
