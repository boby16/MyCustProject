using System;
using System.Data;
using Sunlike.Business.Data;
using System.Collections;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections.Generic;
using System.Text;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for BarCode.
	/// </summary>
	public class BarCode : BizObject
	{	
        private static Hashtable _htBarRole = new Hashtable();
		#region 序列号编码原则
		/// <summary>
		/// 序列号编码原则
		/// </summary>
		public class BarRole
		{
            private  int _barLen;
            private string _boxFlag = "";
            private int _boxPos;
            private int _sPrdt;
            private int _ePrdt;
            private int _bPMark;
            private int _ePMark;
            private int _bSn;
            private int _eSn;
            private int _endChar = 13;
            private string _trimChar = "";
            private int _boxLen;
            private string _legalCh = "";

            private static void EnsureInit()
            {
                if (!_htBarRole.ContainsKey(Comp.CompNo))
                {
                    DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
                    DataTable _dt = _bar.GetBarRole().Tables["BAR_ROLE"];
                    BarRole _barCode = new BarRole();
                    if (_dt.Rows.Count > 0)
                    {
                        DataRow _dr = _dt.Rows[0];
                        if (!String.IsNullOrEmpty(_dr["BAR_LEN"].ToString()))
                        {
                            _barCode._barLen = Convert.ToInt32(_dr["BAR_LEN"]);
                            _barCode._boxFlag = _dr["BOX_FLAG"].ToString();
                            if (!String.IsNullOrEmpty(_dr["BOXPOS"].ToString()))
                            {
                                _barCode._boxPos = Convert.ToInt32(_dr["BOXPOS"]) - 1;
                            }
                            _barCode._sPrdt = Convert.ToInt32(_dr["S_PRDT"]) - 1;
                            _barCode._ePrdt = Convert.ToInt32(_dr["E_PRDT"]) - 1;
                            if (!String.IsNullOrEmpty(_dr["B_PMARK"].ToString()))
                            {
                                _barCode._bPMark = Convert.ToInt32(_dr["B_PMARK"]) - 1;
                            }
                            if (!String.IsNullOrEmpty(_dr["E_PMARK"].ToString()))
                            {
                                _barCode._ePMark = Convert.ToInt32(_dr["E_PMARK"]) - 1;
                            }
                            _barCode._bSn = Convert.ToInt32(_dr["B_SN"]) - 1;
                            _barCode._eSn = Convert.ToInt32(_dr["E_SN"]) - 1;
                            _barCode._endChar = Convert.ToInt32(_dr["END_CHAR"]);
                            _barCode._trimChar = _dr["TRIM_CHAR"].ToString();
                            if (!String.IsNullOrEmpty(_dr["BOX_LEN"].ToString()))
                            {
                                _barCode._boxLen = Convert.ToInt32(_dr["BOX_LEN"]);
                            }
                            _barCode._legalCh = _dr["LEGAL_CH"].ToString();
                        }
                    }
                    _htBarRole[Comp.CompNo] = _barCode;
                }
            }

            private BarRole() {}
			/// <summary>
			/// 总长度
			/// </summary>
            public static int BarLen
            {
                get
                {
                   EnsureInit();
                   BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                   return _barRole._barLen;
                }
            }
			/// <summary>
			/// 箱标签代号
			/// </summary>
            public static string BoxFlag
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._boxFlag;
                }
            }
			/// <summary>
			/// 箱条码识别位置
			/// </summary>
            public static int BoxPos
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._boxPos;
                }
            }
			/// <summary>
			/// 货品起始位置
			/// </summary>
            public static int SPrdt
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._sPrdt;
                }
            }
			/// <summary>
			/// 货品截止位置
			/// </summary>
            public static int EPrdt
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._ePrdt;
                }
            }
			/// <summary>
			/// 特征起始位置
			/// </summary>
            public static int BPMark
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._bPMark;
                }
            }
			/// <summary>
			/// 特征截止位置
			/// </summary>
            public static int EPMark
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._ePMark;
                }
            }
			/// <summary>
			/// 流水号起始位置
			/// </summary>
            public static int BSn
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._bSn;
                }
            }
			/// <summary>
			/// 流水号截止位置
			/// </summary>
            public static int ESn
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._eSn;
                }
            }
			/// <summary>
			/// 结束字符ASCII码
			/// </summary>
            public static int EndChar
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._endChar;
                }
            }
			/// <summary>
			/// 空白替代字符
			/// </summary>
            public static string TrimChar
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._trimChar;
                }
            }
			/// <summary>
			/// 箱条码长度
			/// </summary>
            public static int BoxLen
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._boxLen;
                }
            }
			/// <summary>
			/// 合法字符
			/// </summary>
            public static string LegalCh
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._legalCh;
                }
            }
		}
		#endregion


      
		#region 取得条码信息
		/// <summary>
		/// 取得条码信息
		/// </summary>
		/// <param name="bar_Code">条码内容</param>
		/// <returns></returns>
		public static BarInfo GetBarInfo(string bar_Code)
		{
			BarInfo _info = new BarInfo();
			if(bar_Code.Length == BarRole.BarLen)
			{
				_info.Prd_No = bar_Code.Substring(BarRole.SPrdt, BarRole.EPrdt - BarRole.SPrdt + 1).Replace(BarRole.TrimChar, "");
				_info.Prd_Mark = "";
				if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
				{
					_info.Prd_Mark = bar_Code.Substring(BarRole.BPMark, BarRole.EPMark - BarRole.BPMark + 1);
				}
				_info.Serial_No = bar_Code.Substring(BarRole.BSn, BarRole.ESn - BarRole.BSn + 1).Replace(BarRole.TrimChar, "");
			}
			return _info;
		}


		#endregion

		#region 结构

		/// <summary>
		/// 条码内容
		/// </summary>
		public struct BarInfo
		{
			/// <summary>
			/// 货品代号
			/// </summary>
			public string Prd_No;
			/// <summary>
			/// 货品特征代号
			/// </summary>
			public string Prd_Mark;
			/// <summary>
			/// 货品流水号
			/// </summary>
			public string Serial_No;
		}

		#endregion

		#region 序列号
		/// <summary>
		/// 序列号
		/// </summary>
		public BarCode()
		{
		}
		#endregion

		#region 取得序列号编码原则

		/// <summary>
		/// 取得序列号编码原则，放到BarCode.BarRole中
		/// </summary>
		/// <returns></returns>
        //public void GetBarRole()
        //{
        //    DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
        //    DataTable _dt = _bar.GetBarRole().Tables["BAR_ROLE"];
        //    if (_dt.Rows.Count > 0)
        //    {
        //        DataRow _dr = _dt.Rows[0];
        //        if (!String.IsNullOrEmpty(_dr["BAR_LEN"].ToString()))
        //        {
        //            BarRole.BarLen = Convert.ToInt32(_dr["BAR_LEN"]);
        //            BarRole.BoxFlag = _dr["BOX_FLAG"].ToString();
        //            if (!String.IsNullOrEmpty(_dr["BOXPOS"].ToString()))
        //            {
        //                BarRole.BoxPos = Convert.ToInt32(_dr["BOXPOS"]) - 1;
        //            }
        //            BarRole.SPrdt = Convert.ToInt32(_dr["S_PRDT"]) - 1;
        //            BarRole.EPrdt = Convert.ToInt32(_dr["E_PRDT"]) - 1;
        //            if (!String.IsNullOrEmpty(_dr["B_PMARK"].ToString()))
        //            {
        //                BarRole.BPMark = Convert.ToInt32(_dr["B_PMARK"]) - 1;
        //            }
        //            if (!String.IsNullOrEmpty(_dr["E_PMARK"].ToString()))
        //            {
        //                BarRole.EPMark = Convert.ToInt32(_dr["E_PMARK"]) - 1;
        //            }
        //            BarRole.BSn = Convert.ToInt32(_dr["B_SN"]) - 1;
        //            BarRole.ESn = Convert.ToInt32(_dr["E_SN"]) - 1;
        //            BarRole.EndChar = Convert.ToInt32(_dr["END_CHAR"]);
        //            BarRole.TrimChar = _dr["TRIM_CHAR"].ToString();
        //            if (!String.IsNullOrEmpty(_dr["BOX_LEN"].ToString()))
        //            {
        //                BarRole.BoxLen = Convert.ToInt32(_dr["BOX_LEN"]);
        //            }
        //            BarRole.LegalCh = _dr["LEGAL_CH"].ToString();
        //        }
        //    }
        //}

		/// <summary>
		/// 取得序列号编码原则
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetBarRoleData()
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.GetBarRole();
		}

		#endregion

		#region 更新序列号编码原则

		/// <summary>
		/// 更新序列号编码原则
		/// </summary>
		/// <param name="changeDs"></param>
		/// <param name="bubbleException">是否希望抛出异常</param>
		public DataTable UpdateBarRoleData(SunlikeDataSet changeDs, bool bubbleException)
		{
			DataTable _dtErr = null;
			Hashtable _ht = new Hashtable();
			_ht["BAR_ROLE"] = "ITM,BAR_LEN,BOX_FLAG,BOXPOS,S_PRDT,E_PRDT,B_PMARK,E_PMARK,B_SN,E_SN,END_CHAR,TRIM_CHAR,BOX_LEN,LEGAL_CH";
			this.UpdateDataSet(changeDs, _ht);
			if (changeDs.HasErrors)
			{
				_dtErr = GetAllErrors(changeDs);
				if (bubbleException)
					throw new System.Exception(changeDs.Tables[0].Rows[0].RowError);
			}
			return _dtErr;
		}

		#endregion

		#region 取得序列号记录
		/// <summary>
		/// 取得序列号记录
		/// </summary>
		/// <param name="SqlWhere">条件语句，EX：BAR_NO='001' or BAR_NO='002'</param>
		/// <param name="HasStop">是否包含停用的序列号</param>
		/// <returns></returns>
		public DataTable GetBarRecord(string SqlWhere,bool HasStop)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
            return _bar.GetBarRecord(SqlWhere, HasStop, null);
		}
		/// <summary>
		/// 取得序列号记录
		/// </summary>
		/// <param name="SqlWhere">条件语句，EX：BAR_NO='001' or BAR_NO='002'</param>
		/// <param name="HasStop">是否包含停用的序列号</param>
		/// <param name="dataSetStop">记录已停用的序列号</param>
		/// <returns></returns>
		public DataTable GetBarRecord(string SqlWhere,bool HasStop, DataSet dataSetStop)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
            return _bar.GetBarRecord(SqlWhere, HasStop, dataSetStop);
		}
		#endregion

		#region 取得箱条码资料
		/// <summary>
		/// 取得箱条码主从表资料
		/// </summary>
		/// <param name="BoxNo">箱条码</param>
		/// <param name="OnlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetBoxData(string Usr,string BoxNo, bool OnlyFillSchema)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			SunlikeDataSet _ds = _bar.GetBoxData(BoxNo, OnlyFillSchema);
			//取得特征分段数据
			PrdMark _mark = new PrdMark();
			DataTable _dtMark = _mark.GetSplitData("");
			//创建拆解序列号的表
			DataTable _dt = new DataTable("BAR_COLLECT");
			_dt.Columns.Add("BAR_CODE");
			_dt.Columns.Add("PRD_NO");
			_dt.Columns.Add("PRD_MARK");
			_dt.Columns.Add("PRD_NAME");
			_dt.Columns.Add("WH");
			_dt.Columns.Add("BAT_NO");
			
			_ds.Tables["BAR_REC"].Columns.Add("PRD_NAME");
            if (_mark.RunByPMark(Usr))
            {
                for (int i = 0; i < _dtMark.Rows.Count; i++)
                {
                    _dt.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString());
                    _dt.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString() + "_DSC");
                }
            }
			_dt.Columns.Add("SERIAL_NO");
			DataColumn[] _dca = new DataColumn[1];
			_dca[0] = _dt.Columns["BAR_CODE"];
			_dt.PrimaryKey = _dca;
			_ds.Tables.Add(_dt);
            //增加单据权限
            if (!OnlyFillSchema)
            {
                if (!String.IsNullOrEmpty(Usr))
                {
                    string _pgm = "BARBOX";
                    DataTable _dtMf = _ds.Tables["MF_BOX"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, Usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    }
                }
            }
			return _ds;
		}

		/// <summary>
		/// 取得箱条码表资料
		/// </summary>
		/// <param name="SqlWhere">条件语句，EX：BOX_NO='001' or BOX_NO='002'</param>
		/// <returns></returns>
		public DataTable GetBoxData(string SqlWhere)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBoxData(SqlWhere);
			return _dt;
		}
		#endregion

		#region 装箱作业：取得序列号整理内容
		/// <summary>
		/// 装箱作业：取得序列号整理内容
		/// </summary>
		/// <param name="ChangedDS"></param>
		/// <returns></returns>
		public string BreakBarCode(SunlikeDataSet ChangedDS)
		{
			//取得特征分段数据
			PrdMark _mark = new PrdMark();
			DataTable _dtMark = _mark.GetSplitData("");
			SunlikeDataSet _dsCopy = ChangedDS.Copy();
			DataRow _drHead = ChangedDS.Tables["BAR_BOX"].Rows[0];
			DataTable _dtBody = ChangedDS.Tables["BAR_REC"];
			DataTable _dtBarCode = ChangedDS.Tables["BAR_COLLECT"];
			//判断批号
			string _batNo = String.Empty;
			if(ChangedDS.ExtendedProperties["BAT_NO"] != null)
			{
				if(string.IsNullOrEmpty(ChangedDS.ExtendedProperties["BAT_NO"].ToString()))
				{
					_batNo = ChangedDS.ExtendedProperties["BAT_NO"].ToString();
				}
			}
			//查找删除记录
			DataRow[] _draBody = _dtBody.Select();
			DataRow[] _dra;
			string _barCode;
			for (int i=0;i<_draBody.Length;i++)
			{
				_barCode = _draBody[i]["BAR_NO"].ToString();
				_dra = _dtBarCode.Select("BAR_CODE='" + _barCode + "'");
				if (_dra.Length == 0)
				{
					if (_drHead.RowState == DataRowState.Added)
					{
						//新增箱
						if (_draBody[i].RowState == DataRowState.Added)
						{
							_draBody[i].Delete();
						}
						else
						{
							_draBody[i].Delete();
							_draBody[i].AcceptChanges();
						}
					}
					else
					{
						//修改箱
						_draBody[i]["BOX_NO"] = System.DBNull.Value;
					}
				}
			}
			//取得输入的序列号
			System.Text.StringBuilder _sbBar = new System.Text.StringBuilder();
			for (int i=0;i<_dtBarCode.Rows.Count;i++)
			{
				_barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
				if (_sbBar.Length > 0)
				{
					_sbBar.Append(",");
				}
				_sbBar.Append(_barCode);
			}
			//查找序列号资料
			string _where = "";
			if (_sbBar.Length > 0)
			{
				if (!String.IsNullOrEmpty(_where))
				{
					_where += " or ";
				}
				_where += "BAR_NO in ('" + _sbBar.ToString().Replace(",","','") + "')";
			}
			if (String.IsNullOrEmpty(_where.Trim()))
			{
				_where = "1<>1";
			}
			//从资料库读取序列号资料
			DataTable _dtBarRec = this.GetBarRecord(_where,true);
			string _wh = _drHead["WH"].ToString();			
			
			//检查序列号
			System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
			if (String.IsNullOrEmpty(_wh))
			{
				//入库前做装箱
				for (int i=0;i<_dtBarRec.Rows.Count;i++)
				{
					_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
					if(!String.IsNullOrEmpty(_dtBarRec.Rows[i]["BOX_NO"].ToString()))
					{
						_sbError.Append("序列号[" + _barCode + "]已存在于箱[" + _dtBarRec.Rows[i]["BOX_NO"].ToString() + "]中\n");
					}					
					else if (_dtBarRec.Rows[i]["STOP_ID"].ToString() == "T")
					{
						_sbError.Append("序列号[" + _barCode + "]已停用！\n");
					}
					if(_dtBarRec.Rows.Count > 0)
					{
						if(_dtBarRec.Rows[0]["WH"].ToString() != _wh 
							|| (_dtBarRec.Rows[0]["BAT_NO"].ToString() != _batNo && !String.IsNullOrEmpty(_batNo)))
						{
                            this.InsertBarChange(_barCode, _dtBarRec.Rows[0]["WH"].ToString(), _wh, "", "BAR_BOX", ChangedDS.Tables["MF_BOX"].Rows[0]["USR"].ToString(), _dtBarRec.Rows[0]["BAT_NO"].ToString(), _batNo, "", "", false);
						}
					}
					DataRow _drWh = _dtBarCode.Rows.Find(new object[]{_dtBarRec.Rows[i]["BAR_NO"]});
					if(_drWh != null)
					{
						_drWh["WH"] = _dtBarRec.Rows[i]["WH"];
					}
				}
			}
			else
			{
				//入库后做装箱
				for (int i=0;i<_dtBarCode.Rows.Count;i++)
				{
					_barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
					_dra = _dtBarRec.Select("BAR_NO='" + _barCode + "'");
					if (_dra.Length > 0)
					{
						if (_dra[0]["WH"].ToString() != _wh || (_dra[0]["BAT_NO"].ToString() != _batNo && !String.IsNullOrEmpty(_batNo)))
						{
							//条码所在库位不符
                            this.InsertBarChange(_barCode, _dra[0]["WH"].ToString(), _wh, "", "BAR_BOX", ChangedDS.Tables["MF_BOX"].Rows[0]["USR"].ToString(), _dra[0]["BAT_NO"].ToString(), _batNo, "", "", false);
						}
					}
				}
			}			

			//开始整理
			if (String.IsNullOrEmpty(_sbError.ToString()))
			{
				string _boxNo = _drHead["BOX_NO"].ToString();
				for (int i=0;i<_dtBarCode.Rows.Count;i++)
				{
					_barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();					
					string _prdNo = _dtBarCode.Rows[i]["PRD_NO"].ToString();
					if(String.IsNullOrEmpty(_prdNo.Trim()))
					{
						_prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1).Replace(BarCode.BarRole.TrimChar,"");
					}
					string _prdMark = _dtBarCode.Rows[i]["PRD_MARK"].ToString();
					if(String.IsNullOrEmpty(_prdMark.Trim()))
					{
						if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
						{
							_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
						}
					}
					string[] _aryPrdMark = _mark.BreakPrdMark(_prdMark);					
					_dtBarCode.Rows[i]["PRD_NO"] = _prdNo;					
					for (int j=0;j<_dtMark.Rows.Count;j++)
					{
						_dtBarCode.Rows[i][_dtMark.Rows[j]["FLDNAME"].ToString()] = _aryPrdMark[j];
					}					
					//查找表身有无此序列号
					_draBody = _dtBody.Select("BAR_NO='" + _barCode + "'");
					if (_draBody.Length == 0)
					{
						//表身加一笔记录
						DataRow _dr = _dtBody.NewRow();
						_dr["BAR_NO"] = _barCode;
						_dr["PRD_NO"] = _dtBarCode.Rows[i]["PRD_NO"];
						_dr["PRD_MARK"] = _dtBarCode.Rows[i]["PRD_MARK"];						
						_dtBody.Rows.Add(_dr);
						//检查此序列号是否已经存在
						_dra = _dtBarRec.Select("BAR_NO='" + _barCode + "'");
						if (_dra.Length > 0)
						{
							_dr["BOX_NO"] = _dra[0]["BOX_NO"];
                            //_dr["CUS_NO"] = _dra[0]["CUS_NO"];
							_dr["WH"] = _dra[0]["WH"];
							_dr["SPC_NO"] = _dra[0]["SPC_NO"];
							_dr["PRD_NO"] = _dra[0]["PRD_NO"];
							_dr["PRD_MARK"] = _dra[0]["PRD_MARK"];
							_dr["STOP_ID"] = _dra[0]["STOP_ID"];
							_dr["BAT_NO"] = _dra[0]["BAT_NO"];
							_dr.AcceptChanges();
						}
						_dr["BOX_NO"] = _boxNo;
						_dr["WH"] = _wh;
						_dr["STOP_ID"] = "F";
						if(!String.IsNullOrEmpty(_batNo))
						{
							_dr["BAT_NO"] =  _batNo;
						}
					}
				}
			}
			else
			{
				ChangedDS = _dsCopy;
			}
			return _sbError.ToString();
		}
		#endregion

		#region 整理单据序列号数据
        		/// <summary>
		/// 整理单据序列号数据
		/// </summary>
		/// <param name="ChangedDS">单据的DataSet</param>
		/// <param name="BarCodeDT">序列号原始表（输入的序列号存放在此表中）</param>
		/// <param name="BodyTableName">表身的名称</param>
		/// <param name="BodyBarItemName">表身与序列号表做对应的Key栏位名称</param>
		/// <param name="BodyBoxItemName">表身与箱内容表做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="BarTableName">序列号表的名称</param>
		/// <param name="BarItemName">序列号表与表身做对应的Key栏位名称</param>
		/// <param name="BoxTableName">箱内容表的名称（无则传入空值）</param>
		/// <param name="BoxItemName">箱内容表与表身做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="IsControl">是否严格控管序列号</param>
        /// <param name="IsStop">序列号是否已停用</param>
        /// <param name="IsExist">序列号是否必须存在</param>
        /// <param name="IsPhFlag">序列号是否必须存在</param>
        public void BreakBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName,
            string BodyBoxItemName, string BarTableName, string BarItemName, string BoxTableName, string BoxItemName,
            bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag)

        {
            BreakBilData(ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, "QTY", BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
        }
		/// <summary>
		/// 整理单据序列号数据
		/// </summary>
		/// <param name="ChangedDS">单据的DataSet</param>
		/// <param name="BarCodeDT">序列号原始表（输入的序列号存放在此表中）</param>
		/// <param name="BodyTableName">表身的名称</param>
		/// <param name="BodyBarItemName">表身与序列号表做对应的Key栏位名称</param>
		/// <param name="BodyBoxItemName">表身与箱内容表做对应的Key栏位名称（无箱内容表则传入空值）</param>
        /// <param name="BodyQtyName">表身与箱内容表做对应的QTY栏位名称（无箱内容表则传入空值）</param>
		/// <param name="BarTableName">序列号表的名称</param>
		/// <param name="BarItemName">序列号表与表身做对应的Key栏位名称</param>
		/// <param name="BoxTableName">箱内容表的名称（无则传入空值）</param>
		/// <param name="BoxItemName">箱内容表与表身做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="IsControl">是否严格控管序列号</param>
        /// <param name="IsStop">序列号是否已停用</param>
        /// <param name="IsExist">序列号是否必须存在</param>
        /// <param name="IsPhFlag">序列号是否必须存在</param>
        public void BreakBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName,
            string BodyBoxItemName, string BodyQtyName, string BarTableName, string BarItemName, string BoxTableName, string BoxItemName,
            bool IsControl,bool IsStop,bool IsExist, bool IsPhFlag)
		{
            this.GetCollectData(false, ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, BodyQtyName, BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
			//记录原序列号资料,以便前台判断库位和批号
			DataTable _dtBarRec = ChangedDS.Tables["BAR_REC"];
			DataTable _dtBarError = ChangedDS.Tables["BAR_ERROR"];
			if (_dtBarError.Rows.Count == 0)
			{
				//建立一个虚拟字段，记录序列号是否存在
				if (!BarCodeDT.Columns.Contains("ISEXIST"))
				{
					BarCodeDT.Columns.Add("ISEXIST");
					BarCodeDT.Columns["ISEXIST"].DefaultValue = "T";
				}
				DataColumn[] _dca = new DataColumn[2];
				_dca[0] = BarCodeDT.Columns["BAR_CODE"];
				_dca[1] = BarCodeDT.Columns["BOX_NO"];
				BarCodeDT.PrimaryKey = _dca;
				DataRow _drBarCode = null;
				foreach (DataRow dr in _dtBarRec.Rows)
				{
					_drBarCode = null;
					_drBarCode = BarCodeDT.Rows.Find(new object[2]{dr["BAR_NO"].ToString(), ""});
                    if (_drBarCode == null)
                    {
                        _drBarCode = BarCodeDT.Rows.Find(new object[2] { "", dr["BAR_NO"].ToString() });
                    }
                    if (_drBarCode == null)
                    {
                        _drBarCode = BarCodeDT.Rows.Find(new object[2] { "", dr["BOX_NO"].ToString() });
                    }
					if (_drBarCode != null)
					{
						//是否存在
						_drBarCode["ISEXIST"] = dr["ISEXIST"];
						//序列号库位
						_drBarCode["WH_REC"] = dr["WH"];
						//没有开放收集时的批号输入，因此收集到的批号默认为序列号批号
						if (IsExist)
						{
							_drBarCode["BAT_NO"] = dr["BAT_NO"];
						}
						//序列号批号
                        _drBarCode["BAT_NO_REC"] = dr["BAT_NO"];
                        //序列号是否停用
                        _drBarCode["STOP_ID"] = dr["STOP_ID"];
                        //在途标记
                        _drBarCode["PH_FLAG"] = dr["PH_FLAG"];
                        //序列号规格
                        if (BarCodeDT.Columns.Contains("SPC_NO"))
                        {
                            _drBarCode["SPC_NO"] = dr["SPC_NO"];
                        }
					}
				}
				_dca = new DataColumn[1];
				_dca[0] = BarCodeDT.Columns["ITEM"];
				BarCodeDT.PrimaryKey = _dca;
			}
		}
        		/// <summary>
		/// 整理单据序列号数据
		/// </summary>
		/// <param name="IsRepair">是否序列号补入</param>
		/// <param name="ChangedDS">单据的DataSet</param>
		/// <param name="BarCodeDT">序列号原始表（输入的序列号存放在此表中）</param>
		/// <param name="BodyTableName">表身的名称</param>
		/// <param name="BodyBarItemName">表身与序列号表做对应的Key栏位名称</param>
		/// <param name="BodyBoxItemName">表身与箱内容表做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="BarTableName">序列号表的名称</param>
		/// <param name="BarItemName">序列号表与表身做对应的Key栏位名称</param>
		/// <param name="BoxTableName">箱内容表的名称（无则传入空值）</param>
		/// <param name="BoxItemName">箱内容表与表身做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="IsControl">是否严格控管序列号</param>
        /// <param name="IsStop">序列号是否已停用</param>
        /// <param name="IsExist">序列号是否必须存在</param>
        /// <param name="IsPhFlag">到货标志</param>
        private void GetCollectData(bool IsRepair, DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName,
            string BodyBarItemName, string BodyBoxItemName, string BarTableName, string BarItemName, string BoxTableName,
            string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag)
        {
            GetCollectData(IsRepair, ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, "QTY", BodyBoxItemName, BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
        }
		/// <summary>
		/// 整理单据序列号数据
		/// </summary>
		/// <param name="IsRepair">是否序列号补入</param>
		/// <param name="ChangedDS">单据的DataSet</param>
		/// <param name="BarCodeDT">序列号原始表（输入的序列号存放在此表中）</param>
		/// <param name="BodyTableName">表身的名称</param>
		/// <param name="BodyBarItemName">表身与序列号表做对应的Key栏位名称</param>
		/// <param name="BodyBoxItemName">表身与箱内容表做对应的Key栏位名称（无箱内容表则传入空值）</param>
        /// <param name="BodyQtyName">表身与箱内容表做对应的QTY栏位名称（无箱内容表则传入空值）</param>
		/// <param name="BarTableName">序列号表的名称</param>
		/// <param name="BarItemName">序列号表与表身做对应的Key栏位名称</param>
		/// <param name="BoxTableName">箱内容表的名称（无则传入空值）</param>
		/// <param name="BoxItemName">箱内容表与表身做对应的Key栏位名称（无箱内容表则传入空值）</param>
        /// <param name="IsControl">是否严格控管序列号</param>
        /// <param name="IsStop">序列号是否已停用</param>
        /// <param name="IsExist">序列号是否必须存在</param>
        /// <param name="IsPhFlag">到货标志</param>
		private void GetCollectData(bool IsRepair, DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName,
            string BodyBarItemName, string BodyBoxItemName, string BodyQtyName, string BarTableName, string BarItemName, string BoxTableName,
            string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag)
		{
			//取得表头的Key
			string[] _headerKey = new string[ChangedDS.Tables[0].PrimaryKey.Length];
			for (int i=0;i<ChangedDS.Tables[0].PrimaryKey.Length;i++)
			{
				_headerKey[i] = ChangedDS.Tables[0].PrimaryKey[i].ColumnName;
			}
			DataTable _dtBody = ChangedDS.Tables[BodyTableName];
			DataTable _dtBarCode = ChangedDS.Tables[BarTableName];
			DataTable _dtBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBox = ChangedDS.Tables[BoxTableName];
			}
			//记录原有的表身，为了前台不对这些记录重新取价格
            if (ChangedDS.Tables.Contains(BodyTableName))
            {
                StringBuilder _bodyFlag = new StringBuilder();
                foreach (DataRow dr in ChangedDS.Tables[BodyTableName].Select())
                {
                    if (!String.IsNullOrEmpty(_bodyFlag.ToString()))
                    {
                        _bodyFlag.Append(",");
                    }
                    _bodyFlag.Append(dr["ITM"].ToString());
                }
                ChangedDS.ExtendedProperties["INHERE_DATA"] = _bodyFlag;
            }
			//取得已输入的序列号
			Hashtable _htBarCode = new Hashtable();
			DataRow[] _draBarCode = _dtBarCode.Select();
			for (int i=0;i<_draBarCode.Length;i++)
			{
				_htBarCode[_draBarCode[i]["BOX_NO"].ToString()] = "";
				_htBarCode[_draBarCode[i]["BAR_CODE"].ToString()] = "";
			}
			//把箱条码和序列号分开
			System.Text.StringBuilder _sbBox = new System.Text.StringBuilder();
			System.Text.StringBuilder _sbBar = new System.Text.StringBuilder();
			Hashtable _htCollect = new Hashtable();
			string _barCode,_boxNo;
			for (int i=0;i<BarCodeDT.Rows.Count;i++)
			{
				_htCollect[BarCodeDT.Rows[i]["BOX_NO"].ToString()] = "";
				_htCollect[BarCodeDT.Rows[i]["BAR_CODE"].ToString()] = "";
				_barCode = BarCodeDT.Rows[i]["BOX_NO"].ToString();
				if (String.IsNullOrEmpty(_barCode))
				{
					_barCode = BarCodeDT.Rows[i]["BAR_CODE"].ToString();
					BarCodeDT.Rows[i]["BOX_NO"] = "";
				}
				else if (_htBarCode[_barCode] == null)
				{
					BarCodeDT.Rows[i]["BAR_CODE"] = "";
				}
				if (_htBarCode[_barCode] == null)
				{
					//是箱条码放到_boxList中，否则放入_barList
					if (_barCode.Substring(BarRole.BoxPos,1) == BarRole.BoxFlag)
					{
						if (_sbBox.ToString().IndexOf(_barCode) < 0)
						{
							if (_sbBox.Length > 0)
							{
								_sbBox.Append(",");
							}
							_sbBox.Append(_barCode);
						}
					}
					else
					{
						if (_sbBar.Length > 0)
						{
							_sbBar.Append(",");
						}
						_sbBar.Append(_barCode);
					}
				}
			}
			//查找序列号资料
			string _where = "";
			string _whereBox = "";
			if (_sbBox.Length > 0)
			{
				_whereBox = "BOX_NO in ('" + _sbBox.ToString().Replace(",","','") + "')";
			}
			else
			{
				_whereBox = "1<>1";
			}
			if (_sbBar.Length > 0)
			{
				_where += "BAR_NO in ('" + _sbBar.ToString().Replace(",","','") + "')";
			}
			if (String.IsNullOrEmpty(_where.Trim()))
			{
				_where = "1<>1";
			}
			DataTable _dtBarError = ChangedDS.Tables["BAR_ERROR"];
			DataRow _drBarError;
			if (_dtBarError == null)
			{
				_dtBarError = new DataTable("BAR_ERROR");
				_dtBarError.Columns.Add("UNUSE");
				DataColumn _dc = new DataColumn("BAR_NO");
				_dtBarError.Columns.Add(_dc);
				_dc = new DataColumn("BOX_NO");
				_dtBarError.Columns.Add(_dc);
				_dc = new DataColumn("REM");
				_dtBarError.Columns.Add(_dc);
				ChangedDS.Tables.Add(_dtBarError);
			}
			else
			{
				_dtBarError.Clear();
			}
			//用来放自动剔除的序列号
            DataTable _dtBarDel = ChangedDS.Tables["BAR_DEL"];
			if (_dtBarDel == null)
			{
				_dtBarDel = new DataTable("BAR_DEL");
				DataColumn _dc = new DataColumn("BAR_NO");
				_dtBarDel.Columns.Add(_dc);
				_dc = new DataColumn("BOX_NO");
				_dtBarDel.Columns.Add(_dc);
				_dc = new DataColumn("REM");
				_dtBarDel.Columns.Add(_dc);
				ChangedDS.Tables.Add(_dtBarDel);
			}
			else
			{
				_dtBarDel.Clear();
			}
			//把条件语句分段
			ArrayList _alBox = new ArrayList();
			ArrayList _alBar = new ArrayList();
			int _maxWhereLength = 1024;
			string _subWhere;
			int _pos;
			while (true)
			{
				if (_whereBox.Length > _maxWhereLength)
				{
					_subWhere = _whereBox.Substring(0,_maxWhereLength-1);
					_pos = _subWhere.LastIndexOf(",");
					_alBox.Add(_subWhere.Substring(0,_pos) + ")");
					_whereBox = "BOX_NO in (" + _whereBox.Substring(_pos+1,_whereBox.Length-_pos-1);
				}
				else
				{
					_alBox.Add(_whereBox);
					break;
				}
			}
			_maxWhereLength = 10240;
			while (true)
			{
				if (_where.Length > _maxWhereLength)
				{
					_subWhere = _where.Substring(0,_maxWhereLength-1);
					_pos = _subWhere.LastIndexOf(",");
					_alBar.Add(_subWhere.Substring(0,_pos) + ")");
					_where = "BAR_NO in (" + _where.Substring(_pos+1,_where.Length-_pos-1);
				}
				else
				{
					_alBar.Add(_where);
					break;
				}
			}
			//从资料库读取序列号资料
			SunlikeDataSet _dsBar = new SunlikeDataSet();
			DataTable _dtBarBox = null;
//			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				for (int i=0;i<_alBox.Count;i++)
				{
					_dtBarBox = this.GetBoxData(_alBox[i].ToString());
					if (_dsBar.Tables["BAR_BOX"] == null)
					{
						_dsBar.Tables.Add(_dtBarBox.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBarBox,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBarBox = _dsBar.Tables["BAR_BOX"];
			}
			DataTable _dtBarRec;
			for (int i=0;i<_alBar.Count;i++)
			{
				DataSet _dsBarRecStop = new DataSet();
				_dtBarRec = this.GetBarRecord(_alBar[i].ToString(),IsStop,_dsBarRecStop);
				//建立一个虚拟字段，记录序列号是否存在
				if (!_dtBarRec.Columns.Contains("ISEXIST"))
				{
					_dtBarRec.Columns.Add("ISEXIST");
					_dtBarRec.Columns["ISEXIST"].DefaultValue = "T";
                }
                //判断已经输入的箱码,在序列号变更单里使用
                foreach (DataRow dr in _dtBarRec.Rows)
                {
                    if (!String.IsNullOrEmpty(dr["BOX_NO"].ToString()))
                    {
                        if (_htBarCode.ContainsKey(dr["BOX_NO"].ToString()))
                        {
                            _drBarError = _dtBarError.NewRow();
                            _drBarError["BAR_NO"] = dr["BAR_NO"].ToString();
                            _drBarError["BOX_NO"] = dr["BOX_NO"].ToString();
                            _drBarError["REM"] = "RCID=INV.HINT.SAMEDATA";//"序列号归属于箱码，不能同时输入这两笔记录！";
                            _dtBarError.Rows.Add(_drBarError);
                        }
                    }
                }
				if (_dsBar.Tables["BAR_REC"] == null)
				{
					_dsBar.Tables.Add(_dtBarRec.Copy());
				}
				else
				{
					_dsBar.Merge(_dtBarRec,true,MissingSchemaAction.AddWithKey);
				}
				if (_dsBarRecStop.Tables["BAR_REC_STOP"] != null)
				{
					if (_dsBar.Tables["BAR_REC_STOP"] == null)
					{
						_dsBar.Tables.Add(_dsBarRecStop.Tables["BAR_REC_STOP"].Copy());
					}
					else
					{
						_dsBar.Merge(_dsBarRecStop, true, MissingSchemaAction.AddWithKey);
					}
				}
			}
			for (int i=0;i<_alBox.Count;i++)
			{
				_dtBarRec = this.GetBarRecord(_alBox[i].ToString(),IsStop);
				if (_dsBar.Tables["BAR_REC"] == null)
				{
					_dsBar.Tables.Add(_dtBarRec.Copy());
				}
				else
				{
					for (int j=0;j<_dtBarRec.Rows.Count;j++)
					{
						try
						{
                            //判断之前输入过的序列号
                            if (_htBarCode.ContainsKey(_dtBarRec.Rows[j]["BAR_NO"].ToString()))
                            {
                                throw new Exception();
                            }
							_dsBar.Tables["BAR_REC"].ImportRow(_dtBarRec.Rows[j]);
						}
						catch
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["BAR_NO"] = _dtBarRec.Rows[j]["BAR_NO"].ToString();
							_drBarError["BOX_NO"] = _dtBarRec.Rows[j]["BOX_NO"].ToString();
							_drBarError["REM"] = "RCID=INV.HINT.SAMEDATA";//"序列号归属于箱码，不能同时输入这两笔记录！";
							_dtBarError.Rows.Add(_drBarError);
						}
					}
				}
			}
			_dtBarRec = _dsBar.Tables["BAR_REC"];
			//查找是否有此箱条码
			if (_sbBox.Length > 0 && _dtBarBox != null)
			{
				string[] _aryBoxList = _sbBox.ToString().Split(new char[] {','});
				for (int i=0;i<_aryBoxList.Length;i++)
				{
					//2006-06-07修改从_dtBarBox用Find方法查找是否存在箱条码
					DataRow _drBoxExist = _dtBarBox.Rows.Find(_aryBoxList[i]);
					if (_drBoxExist == null)
					{
						_drBarError = _dtBarError.NewRow();
						_drBarError["BOX_NO"] = _aryBoxList[i];
						_drBarError["REM"] = "RCID=INV.HINT.BOXNOEXISTS";//"箱条码不存在！";
						_dtBarError.Rows.Add(_drBarError);
					}
					else if (_drBoxExist["STOP_ID"].ToString() == "T")
					{
						bool _isBreak = false;
						DataRow[] _draBarRecCheckedNull = _dtBarRec.Select("BOX_NO='" + _aryBoxList[i] + "'");
						//已经拆箱
						if (_draBarRecCheckedNull.Length == 0)
						{
							if (_dsBar.Tables.Contains("BAR_REC_STOP"))
							{
								_draBarRecCheckedNull = _dsBar.Tables["BAR_REC_STOP"].Select("BOX_NO='" + _aryBoxList[i] + "'");
								if (_draBarRecCheckedNull.Length == 0)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BOX_NO"] = _aryBoxList[i];
									_drBarError["REM"] = "RCID=INV.HINT.HASBREAK";//已经拆箱";
									_dtBarError.Rows.Add(_drBarError);
									_isBreak = true;
								}
								else
								{
									foreach (DataRow drStop in _draBarRecCheckedNull)
									{
										_dtBarRec.ImportRow(drStop);
									}
								}
							}
							else
							{
								_drBarError = _dtBarError.NewRow();
								_drBarError["BOX_NO"] = _aryBoxList[i];
								_drBarError["REM"] = "RCID=INV.HINT.HASBREAK";//已经拆箱";
								_dtBarError.Rows.Add(_drBarError);
								_isBreak = true;
							}
						}
						if (!_isBreak && IsControl && IsExist)
						{
							//序列号管制，已拆箱
							_drBarError = _dtBarError.NewRow();
							_drBarError["BOX_NO"] = _aryBoxList[i];
							_drBarError["REM"] = "RCID=INV.HINT.HASSTOP";//已经停用";
							_dtBarError.Rows.Add(_drBarError);
						}
					}
					//Bug tag 1
					else if (IsControl && !IsExist && !String.IsNullOrEmpty(_drBoxExist["WH"].ToString()))
					{
						//序列号管制，库存增加，库位不为空
						_drBarError = _dtBarError.NewRow();
						_drBarError["BOX_NO"] = _aryBoxList[i];
						_drBarError["REM"] = "RCID=INV.HINT.BOXEXISTS,PARAM="+_aryBoxList[i]+",PARAM="+_drBoxExist["WH"].ToString();//XXX箱码不合法，箱码库位应该为[XXX]";
						_dtBarError.Rows.Add(_drBarError);
					}
				}
			}
			//查找是否有此序列号
			if (_sbBar.Length > 0)
			{
				string[] _aryBarList = _sbBar.ToString().Split(new char[] {','});
				for (int i=0;i<_aryBarList.Length;i++)
				{
					DataRow[] _dra = _dtBarRec.Select("BAR_NO='" + _aryBarList[i] + "'");
					if (_dra.Length == 0)
					{
						if (IsControl && IsExist)
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["BAR_NO"] = _aryBarList[i];
							_drBarError["REM"] = "RCID=INV.HINT.BARCODENOEXIST";//"序列号不存在或已停用！";
							_dtBarError.Rows.Add(_drBarError);
						}
						else
						{
							//如果序列号不管制，则在BAR_REC表里新增此序列号，这里只是加一条记录，不做保存
							DataRow _dr = _dtBarRec.NewRow();
							_dr["BAR_NO"] = _aryBarList[i];
							_dr["WH"] = "";
							if (_dsBar.Tables["BAR_REC_STOP"] != null)
							{
								DataRow _drStop = _dsBar.Tables["BAR_REC_STOP"].Rows.Find(_aryBarList[i]);
								if (_drStop == null)
								{
									//如果在停用序列号中也不能找到改序列号的话，改序列号则为不存在
									_dr["ISEXIST"] = "F";
								}
								else
								{
									_dr["WH"] = _drStop["WH"];
									_dr["BAT_NO"] = _drStop["BAT_NO"];
                                    _dr["STOP_ID"] = "T";
                                    _dr["PH_FLAG"] = _drStop["PH_FLAG"];
								}
							}
							else
							{
								//如果在停用序列号中也不能找到改序列号的话，改序列号则为不存在
								_dr["ISEXIST"] = "F";
							}
							_dtBarRec.Rows.Add(_dr);
						}
					}
					else if (IsStop && IsControl && _dra[0]["STOP_ID"].ToString() != "T" && !String.IsNullOrEmpty(_dra[0]["WH"].ToString()))
					{
						_drBarError = _dtBarError.NewRow();
						_drBarError["BAR_NO"] = _aryBarList[i];
						_drBarError["REM"] = "RCID=INV.HINT.BAREXISTS";//"序列号已存在！";
						_dtBarError.Rows.Add(_drBarError);
					}
                    else if (IsControl && IsPhFlag && (_dra[0]["PH_FLAG"].ToString() == "" || _dra[0]["PH_FLAG"].ToString() == "F"))
                    {
                        _drBarError = _dtBarError.NewRow();
                        _drBarError["BAR_NO"] = _aryBarList[i];
                        _drBarError["REM"] = "RCID=COMMON.HINT.PH_FLAG1";//"序列号状态为未到货！";
                        _dtBarError.Rows.Add(_drBarError);
                    }
                    else if (IsControl && !IsPhFlag && _dra[0]["PH_FLAG"].ToString() == "T")
                    {
                        _drBarError = _dtBarError.NewRow();
                        _drBarError["BAR_NO"] = _aryBarList[i];
                        _drBarError["REM"] = "RCID=COMMON.HINT.PH_FLAG2";//"序列号状态为未到货！";
                        _dtBarError.Rows.Add(_drBarError);
                    }
				}
			}
			if (_dtBarError.Rows.Count == 0)
			{
				//查找删除记录
				DataRow[] _draBody,_draBox;
				int _keyItm,_boxItm,_qty;
				Hashtable _htBoxDel = new Hashtable();
				_draBarCode = _dtBarCode.Select();
				int _countBarCode = _draBarCode.Length;
				bool _isReset = false;
				for (int i=0;i<_countBarCode;i++)
				{
					_barCode = _draBarCode[i]["BAR_CODE"].ToString();
					_boxNo = _draBarCode[i]["BOX_NO"].ToString();
					//找不到则将它删掉
					if ((!String.IsNullOrEmpty(_boxNo) && _htCollect[_boxNo] == null) || (String.IsNullOrEmpty(_boxNo) && _htCollect[_barCode] == null))
					{
						//如果是补入，只要删除序列号的记录就可以了；做收集还需要扣减表身和箱内容的数量
						if (IsRepair)
						{
							_draBarCode[i].Delete();
						}
						else if (!String.IsNullOrEmpty(BarItemName))
						{
							_keyItm = Convert.ToInt32(_draBarCode[i][BarItemName]);
							_draBarCode[i].Delete();
							_draBody = _dtBody.Select(BodyBarItemName + "=" + _keyItm.ToString());
							if (_draBody.Length > 0)
							{
								//箱内容数量减少
								if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName)
									&& !String.IsNullOrEmpty(_boxNo) && _htBoxDel[_boxNo] == null
									&& Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "T")
								{
									_htBoxDel[_boxNo] = "";
									_boxItm = Convert.ToInt32(_draBody[0][BodyBoxItemName]);
									_draBox = _dtBox.Select(BoxItemName + "='" + _boxItm.ToString() + "'");
									_qty = Math.Abs(Convert.ToInt32(_draBox[0]["QTY"])) - 1;
									if (_qty > 0)
									{
                                        _draBox[0]["QTY"] = _qty;
									}
									else
									{
										_draBox[0].Delete();
									}
								}
								//表身货品数量相应减少
                                _qty = Math.Abs(Convert.ToInt32(_draBody[0][BodyQtyName])) - 1;
								if (_qty > 0)
								{
                                    _draBody[0][BodyQtyName] = _qty;
								}
								else
								{
									_draBody[0].Delete();
								}
							}
						}
						_isReset = true;
					}
				}
				//重整ITM
				if (_isReset)
				{
					_draBarCode = _dtBarCode.Select();
					for (int i=0;i<_draBarCode.Length;i++)
					{
						_draBarCode[i]["ITM"] = i + 1;
					}
				}
				ChangedDS.Tables.Add(_dtBarRec.Copy());
				if (_dtBarBox != null)
				{
					ChangedDS.Tables.Add(_dtBarBox.Copy());
				}
			}
//			if (!IsExist && _dtBarError.Rows.Count == 0)
//			{
//				foreach (DataRow dr in ChangedDS.Tables["BAR_COLLECT"].Rows)
//				{
//					if (!String.IsNullOrEmpty(dr["BAR_CODE"].ToString()))
//					{
//						DataRow _dr = ChangedDS.Tables["BAR_REC"].Rows.Find(new string[1]{dr["BAR_CODE"].ToString()});
//						if (_dr != null)
//						{
//							if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
//							{
//								_dr["BAT_NO"] = dr["BAT_NO"];
//							}
//						}
//					}
//					else
//					{
//						//箱条码
//						DataRow[] _draBarCollect = ChangedDS.Tables["BAR_REC"].Select("BOX_NO='"+dr["BOX_NO"].ToString()+"'");
//						foreach (DataRow drCollect in _draBarCollect)
//						{
//							if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
//							{
//								drCollect["BAT_NO"] = dr["BAT_NO"];
//							}
//						}
//					}
//				}
//			}
		}

		/// <summary>
        /// 整理单据序列号数据(该方法表身数量字段名称须自行添入)
		/// </summary>
		/// <param name="ChangedDS">单据的DataSet</param>
		/// <param name="BarCodeDT">序列号原始表（输入的序列号存放在此表中）</param>
		/// <param name="BodyTableName">表身的名称</param>
		/// <param name="BodyBarItemName">表身与序列号表做对应的Key栏位名称</param>
		/// <param name="BodyBoxItemName">表身与箱内容表做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="BodyWhName">表身的库栏位名称</param>
        /// <param name="BodyQtyName">表身的数量名称(因盘点单中数量字段名称为QTY2,故加该字段)</param>
		/// <param name="BarTableName">序列号表的名称</param>
		/// <param name="BarItemName">序列号表与表身做对应的Key栏位名称</param>
		/// <param name="BoxTableName">箱内容表的名称（无则传入空值）</param>
		/// <param name="BoxItemName">箱内容表与表身做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="IsControl">是否严格控管序列号</param>
        /// <param name="IsStop">序列号是否已停用或不存在</param>
        /// <param name="IsExist">序列号是否必须存在</param>
        /// <param name="IsPhFlag">到货标志</param>
		/// <param name="ParaHT">取得货品资料名称要带的参数：不取名称则传入null
		/// PrdField：货品名称栏位名（不取货品名称，此参数填空白）
		/// MarkDscName：特征分段名称栏位名，如：分段栏位为Size，分段名称栏位为Size_DSC，则传入"_DSC"即可（不取特征分段名称，此参数填空白）
		/// WhNameField：库名称栏位名（不取库位名称，此参数填空白）
		/// UnitField：单位栏位名（不处理单位，此参数填空白）
		/// UnitNameField：单位名称栏位名（不取单位名称，此参数填空白）
		/// UpField：货品单价栏位名（不取货品定价政策，此参数填空白）
		/// BilDD：单据日期（要取货品定价政策，则传入单据日期，否则传当天）
		/// CusNo：客户代号（要取货品定价政策，则传入客户代号，否则传空）
		/// </param>
        public void BreakBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName, string BodyBoxItemName, string BodyWhName, string BodyQtyName, string BarTableName, string BarItemName, string BoxTableName, string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag, System.Collections.Hashtable ParaHT)
		{
            this.BreakBilData(ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
			DataTable _dtBarError = ChangedDS.Tables["BAR_ERROR"];
			DataRow _drBarError;
			string[] _headerKey = new string[ChangedDS.Tables[0].PrimaryKey.Length];
			for (int i=0;i<ChangedDS.Tables[0].PrimaryKey.Length;i++)
			{
				_headerKey[i] = ChangedDS.Tables[0].PrimaryKey[i].ColumnName;
			}
			DataTable _dtBody = ChangedDS.Tables[BodyTableName];
			DataTable _dtBarCode = ChangedDS.Tables[BarTableName];
			DataTable _dtBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBox = ChangedDS.Tables[BoxTableName];
			}
			DataTable _dtBarRec = ChangedDS.Tables["BAR_REC"];
			DataTable _dtBarBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBarBox = ChangedDS.Tables["BAR_BOX"];
			}
			if (_dtBarError.Rows.Count == 0)
			{
				DataRow[] _draBox;
				DataRow _drBody,_drBarCode,_drBox;
				int _keyItm,_boxItm,_qty;
				string _barCode,_boxNo,_batNo;
				DataRow[] _draBarCode = _dtBarCode.Select();
				int _countBarCode = _draBarCode.Length;
				DataRow[] _draBody = _dtBody.Select();
				int _countBody = _draBody.Length;
				//取得特征分段数据
				PrdMark _mark = new PrdMark();
				DataTable _dtMark = _mark.GetSplitData("");
				//把已输入的序列号放到Hashtable中
				System.Collections.Hashtable _htBarCode = new System.Collections.Hashtable();
				_draBarCode = _dtBarCode.Select();
				for (int i=0;i<_draBarCode.Length;i++)
				{
					_htBarCode[_draBarCode[i]["BAR_CODE"].ToString()] = "";
				}
				//是箱条码放到_boxList中
				System.Collections.Hashtable _htBox = new System.Collections.Hashtable();
				DataRow[] _draBarCollect = BarCodeDT.Select();
				for (int i=0;i<_draBarCollect.Length;i++)
				{
					_boxNo = _draBarCollect[i]["BOX_NO"].ToString();
					if (!String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] == null)
					{
						_htBox[_boxNo] = "";
					}
				}
				System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
				for (int i=0;i<_dtBarCode.Rows.Count;i++)
				{
					if (_dtBarCode.Rows[i].RowState != DataRowState.Deleted)
					{
						if (_htBoxNo[_dtBarCode.Rows[i]["BOX_NO"].ToString()] == null)
						{
							_htBoxNo[_dtBarCode.Rows[i]["BOX_NO"].ToString()] = i;
						}
					}
				}
				for (int i=0;i<_dtBarRec.Rows.Count;i++)
				{
					_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
					_batNo = _dtBarRec.Rows[i]["BAT_NO"].ToString();
					//在BAR_CODE表中查找此序列号是否存在
					if (_htBarCode[_barCode] == null)
					{
						//拆出货号、特征
						string _prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
						_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
						string _prdMark = "";
						if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
						{
							_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
						}
						string[] _markList = _mark.BreakPrdMark(_prdMark);
						_boxNo = _dtBarRec.Rows[i]["BOX_NO"].ToString();
						//是否要处理箱内容
						if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName)
							&& !String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] != null
							&& Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "T")
						{
							if (String.IsNullOrEmpty(_htBoxNo[_boxNo].ToString()) || _htBoxNo[_boxNo] == null)
							{
								_drBarCode = null;
							}
							else
							{
								_drBarCode = _dtBarCode.Rows[Convert.ToInt32(_htBoxNo[_boxNo])];
							}
							//序列号表中是否有此BOX_NO
							if (_drBarCode != null)
							{
								_boxItm = Convert.ToInt32(_dtBody.Select(BodyBarItemName + "=" + _drBarCode[BarItemName].ToString())[0][BodyBoxItemName]);
								_draBody = _dtBody.Select("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
									+ "' and " + BodyBoxItemName + "=" + _boxItm.ToString());
								if (_draBody.Length > 0)
								{
									//表身数量加一
                                    _draBody[0][BodyQtyName] = Math.Abs(Convert.ToInt32(_draBody[0][BodyQtyName])) + 1;
									_keyItm = Convert.ToInt32(_draBody[0][BodyBarItemName]);
								}
								else
								{
									//增加一笔表身
									_drBody = _dtBody.NewRow();
									for (int j=0;j<_headerKey.Length;j++)
									{
										_drBody[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
									}
									_countBody ++;
									_drBody["ITM"] = _countBody;
									_drBody["PRD_NO"] = _prdNo;
									_drBody["PRD_MARK"] = _prdMark;
									for (int j=0;j<_markList.Length;j++)
									{
										_drBody[_dtMark.Rows[j]["FLDNAME"].ToString()] = _markList[j];
									}
									_drBody[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
                                    _drBody[BodyQtyName] = 1;
									_drBody[BodyBoxItemName] = _boxItm;
									_drBody["BAT_NO"] = _batNo;
									_dtBody.Rows.Add(_drBody);
									_keyItm = Convert.ToInt32(_drBody[BodyBarItemName]);
								}
							}
							else
							{
								string _content = _dtBarBox.Select("BOX_NO='" + _boxNo + "'")[0]["CONTENT"].ToString();
								_draBox = _dtBox.Select("PRD_NO='" + _prdNo + "' and CONTENT='" + _content + "'");
								//箱内容有无此货品、配码比的记录
								if (_draBox.Length > 0)
								{
                                    _qty = Math.Abs(Convert.ToInt32(_draBox[0][BodyQtyName]));
									_boxItm = Convert.ToInt32(_draBox[0][BoxItemName]);
									//箱数量加一
                                    _draBox[0][BodyQtyName] = _qty + 1;
									//查找表身的该箱记录
									_draBody = _dtBody.Select(BodyBoxItemName + "=" + _boxItm.ToString()
										+ " and PRD_MARK='" + _prdMark + "'");
									if (_draBody.Length > 0)
									{
                                        _draBody[0][BodyQtyName] = Math.Abs(Convert.ToInt32(_draBody[0][BodyQtyName])) + 1;
										_keyItm = Convert.ToInt32(_draBody[0][BodyBarItemName]);
									}
									else
									{
										//加一笔表身
										_drBody = _dtBody.NewRow();
										for (int j=0;j<_headerKey.Length;j++)
										{
											_drBody[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
										}
										_countBody ++;
										_drBody["ITM"] = _countBody;
										_drBody["PRD_NO"] = _prdNo;
										_drBody["PRD_MARK"] = _prdMark;
										for (int j=0;j<_markList.Length;j++)
										{
											_drBody[_dtMark.Rows[j]["FLDNAME"].ToString()] = _markList[j];
										}
										_drBody[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
                                        _drBody[BodyQtyName] = 1;
										_drBody[BodyBoxItemName] = _boxItm;
										_drBody["BAT_NO"] = _batNo;
										_dtBody.Rows.Add(_drBody);
										_keyItm = Convert.ToInt32(_drBody[BodyBarItemName]);
									}
								}
								else
								{
									//加一笔箱内容记录
									_drBox = _dtBox.NewRow();
									for (int j=0;j<_headerKey.Length;j++)
									{
										_drBox[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
									}
									_drBox["ITM"] = _dtBox.Select().Length + 1;
									_drBox["PRD_NO"] = _prdNo;
									_drBox["CONTENT"] = _content;
                                    _drBox["QTY"] = 1;
									_drBox[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
									_dtBox.Rows.Add(_drBox);
									_boxItm = Convert.ToInt32(_drBox[BoxItemName]);
									//加一笔表身
									_drBody = _dtBody.NewRow();
									for (int j=0;j<_headerKey.Length;j++)
									{
										_drBody[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
									}
									_countBody ++;
									_drBody["ITM"] = _countBody;
									_drBody["PRD_NO"] = _prdNo;
									_drBody["PRD_MARK"] = _prdMark;
									for (int j=0;j<_markList.Length;j++)
									{
										_drBody[_dtMark.Rows[j]["FLDNAME"].ToString()] = _markList[j];
									}
									_drBody[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
                                    _drBody[BodyQtyName] = 1;
									_drBody[BodyBoxItemName] = _boxItm;
									_drBody["BAT_NO"] = _batNo;
									_dtBody.Rows.Add(_drBody);
									_keyItm = Convert.ToInt32(_drBody[BodyBarItemName]);
								}
								if (String.IsNullOrEmpty(_htBoxNo[_boxNo].ToString()) || _htBoxNo[_boxNo].ToString() == null)
								{
									_htBoxNo[_boxNo] = _dtBarCode.Rows.Count;
								}
							}
						}
						else
						{
							//自动拆箱处理
							_keyItm = 0;
							if (!String.IsNullOrEmpty(_boxNo))
							{
								if (_htBox[_boxNo] == null)
								{
									if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
									{
										_boxNo = "";
									}
									else
									{
										_drBarError = _dtBarError.NewRow();
										_drBarError["BAR_NO"] = _barCode;
										_drBarError["REM"] = "RCID=INV.HINT.NOTBREAK";//"序列号已经装箱，你不能拆箱录入！";
										_dtBarError.Rows.Add(_drBarError);
									}
								}
							}
							if (_dtBarError.Rows.Count == 0)
							{
								//在表身中查找此货品是否存在
								StringBuilder _where = new StringBuilder("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark + "'");
								if (IsControl && !String.IsNullOrEmpty(_dtBarRec.Rows[i]["WH"].ToString()))
								{
									_where.Append(" and " + BodyWhName + "='" + _dtBarRec.Rows[i]["WH"].ToString() + "'");
								}
								if (!String.IsNullOrEmpty(_dtBarRec.Rows[i]["BAT_NO"].ToString()))
								{
									_where.Append(" and BAT_NO='" + _batNo + "'");
								}
								if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
								{
									_where.Append(" and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
								}
								_draBody = _dtBody.Select(_where.ToString());
								//找到有此货品，则数量加一，否则加一行
								if (_draBody.Length > 0)
								{
                                    _draBody[0][BodyQtyName] = Math.Abs(Convert.ToInt32(_draBody[0][BodyQtyName])) + 1;
									_keyItm = Convert.ToInt32(_draBody[0][BodyBarItemName]);
								}
								else
								{
									_drBody = _dtBody.NewRow();
									for (int j=0;j<_headerKey.Length;j++)
									{
										_drBody[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
									}
									_countBody ++;
									_drBody["ITM"] = _countBody;
									_drBody["PRD_NO"] = _prdNo;
									_drBody["PRD_MARK"] = _prdMark;
									for (int j=0;j<_markList.Length;j++)
									{
										_drBody[_dtMark.Rows[j]["FLDNAME"].ToString()] = _markList[j];
									}
									_drBody[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
                                    _drBody[BodyQtyName] = 1;
									_drBody["BAT_NO"] = _batNo;
									_dtBody.Rows.Add(_drBody);
									_keyItm = Convert.ToInt32(_drBody[BodyBarItemName]);
								}
							}
						}
						if (_dtBarError.Rows.Count == 0)
						{
							//插入到BAR_CODE表中
							_drBarCode = _dtBarCode.NewRow();
							for (int j=0;j<_headerKey.Length;j++)
							{
								_drBarCode[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
							}
							_countBarCode ++;
							_drBarCode["ITM"] = _countBarCode;
							_drBarCode["PRD_NO"] = _prdNo;
							_drBarCode["PRD_MARK"] = _prdMark;
							_drBarCode["BAR_CODE"] = _barCode;
							_drBarCode["BOX_NO"] = _boxNo;
							_drBarCode[BarItemName] = _keyItm;
							_dtBarCode.Rows.Add(_drBarCode);
							_htBarCode[_barCode] = "";
						}
					}
				}
				//取得货品相关资料的名称
				if (_dtBarError.Rows.Count == 0)
				{
					if (_countBody > 0)
					{
						if (ParaHT == null)
						{
							ParaHT = new System.Collections.Hashtable();
						}
						string _prdField = (string)ParaHT["PrdField"];
						string _markDscName = (string)ParaHT["MarkDscName"];
						string _whNameField = (string)ParaHT["WhNameField"];
						string _unitField = (string)ParaHT["UnitField"];
						string _unitNameField = (string)ParaHT["UnitNameField"];
						string _upField = (string)ParaHT["UpField"];
						DateTime _bilDD = DateTime.Today;
						if (ParaHT["BilDD"] != null)
						{
							_bilDD = Convert.ToDateTime(ParaHT["BilDD"]);
						}
						string _cusNo = (string)ParaHT["CusNo"];
						Prdt _prdt = new Prdt();
						string _errorPrdt = _prdt.AddBilPrdName(_dtBody,_prdField,_markDscName,BodyWhName,_whNameField,false,_unitField,_unitNameField,_upField,_bilDD,_cusNo);
						if (!String.IsNullOrEmpty(_errorPrdt))
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["REM"] = _errorPrdt;
							_dtBarError.Rows.Add(_drBarError);
						}
						if (_dtBarError.Rows.Count == 0)
						{
							//重新整理ITM
							_draBody = _dtBody.Select();
							_countBody = _draBody.Length;
							if (_countBody > 0 && _draBody[_countBody - 1]["ITM"].ToString() != (_countBody - 1).ToString())
							{
								for (int i=0;i<_countBody;i++)
								{
									_draBody[i]["ITM"] = i + 1;
								}
							}
						}
					}
				}
				if (_dtBarError.Rows.Count == 0)
				{
					BarCodeDT.AcceptChanges();
				}
				ChangedDS.Tables.Remove("BAR_REC");
				if (ChangedDS.Tables["BAR_BOX"] != null)
				{
					ChangedDS.Tables.Remove("BAR_BOX");
				}
			}
		}
        /// <summary>
        /// 整理单据序列号数据(该方法表身数量字段名称固定为QTY)
        /// </summary>
        /// <param name="ChangedDS">单据的DataSet</param>
        /// <param name="BarCodeDT">序列号原始表（输入的序列号存放在此表中）</param>
        /// <param name="BodyTableName">表身的名称</param>
        /// <param name="BodyBarItemName">表身与序列号表做对应的Key栏位名称</param>
        /// <param name="BodyBoxItemName">表身与箱内容表做对应的Key栏位名称（无箱内容表则传入空值）</param>
        /// <param name="BodyWhName">表身的库栏位名称</param>
        /// <param name="BarTableName">序列号表的名称</param>
        /// <param name="BarItemName">序列号表与表身做对应的Key栏位名称</param>
        /// <param name="BoxTableName">箱内容表的名称（无则传入空值）</param>        
        /// <param name="BoxItemName">箱内容表与表身做对应的Key栏位名称（无箱内容表则传入空值）</param>
        /// <param name="IsControl">是否严格控管序列号</param>
        /// <param name="IsStop">序列号是否已停用或不存在</param>
        /// <param name="IsExist">序列号是否必须存在</param>
        /// <param name="IsPhFlag">到货标志</param>
        /// <param name="ParaHT">取得货品资料名称要带的参数：不取名称则传入null
        /// PrdField：货品名称栏位名（不取货品名称，此参数填空白）
        /// MarkDscName：特征分段名称栏位名，如：分段栏位为Size，分段名称栏位为Size_DSC，则传入"_DSC"即可（不取特征分段名称，此参数填空白）
        /// WhNameField：库名称栏位名（不取库位名称，此参数填空白）
        /// UnitField：单位栏位名（不处理单位，此参数填空白）
        /// UnitNameField：单位名称栏位名（不取单位名称，此参数填空白）
        /// UpField：货品单价栏位名（不取货品定价政策，此参数填空白）
        /// BilDD：单据日期（要取货品定价政策，则传入单据日期，否则传当天）
        /// CusNo：客户代号（要取货品定价政策，则传入客户代号，否则传空）
        /// </param>
        public void BreakBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName, string BodyBoxItemName, string BodyWhName, string BarTableName, string BarItemName, string BoxTableName, string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag, System.Collections.Hashtable ParaHT)
        {
            BreakBilData(ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, BodyWhName, "QTY", BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag, ParaHT);
        }
		#endregion

		#region 拆箱
		/// <summary>
		/// 拆箱
		/// </summary>
        /// <param name="alBoxNo">箱条码集合</param>
		/// <param name="usr">用户代号</param>
        /// <param name="bilId">单据类别</param>
		/// <param name="bilNo">来源单号</param>
        public void DeleteBox(ArrayList alBoxNo, string usr, string bilId, string bilNo)
        {
            string[] _aryBoxNo = new string[alBoxNo.Count];
            for (int i = 0; i < alBoxNo.Count; i++)
            {
                _aryBoxNo[i] = alBoxNo[i].ToString();
            }
            this.DeleteBox(_aryBoxNo, usr, bilId, bilNo);
        }
		/// <summary>
		/// 拆箱
		/// </summary>
		/// <param name="AryBoxNo">箱条码集合</param>
		/// <param name="usr">用户代号</param>
		/// <param name="bilID">单据类别</param>
		/// <param name="bilNo">来源单号</param>
		public void DeleteBox(string[] AryBoxNo,string usr,string bilID,string bilNo)
		{
			System.Text.StringBuilder _sbWhere = new System.Text.StringBuilder();
			for (int i=0;i<AryBoxNo.Length;i++)
			{
				if (AryBoxNo[i] != null && !String.IsNullOrEmpty(AryBoxNo[i]) && _sbWhere.ToString().IndexOf(AryBoxNo[i]) < 0)
				{
					if (!String.IsNullOrEmpty(_sbWhere.ToString()))
					{
						_sbWhere.Append(",");
					}
					_sbWhere.Append(AryBoxNo[i]);
				}
			}
			if (!String.IsNullOrEmpty(_sbWhere.ToString()))
			{
				//把条件语句分段
				System.Collections.ArrayList _alBar = new System.Collections.ArrayList();
				int _maxWhereLength = 20480;
				string _subWhere;
				int _pos;
				string _where = "BOX_NO in ('" + _sbWhere.ToString().Replace(",","','") + "')";
				while (true)
				{
					if (_where.Length > _maxWhereLength)
					{
						_subWhere = _where.Substring(0,_maxWhereLength-1);
						_pos = _subWhere.LastIndexOf(",");
						_alBar.Add(_subWhere.Substring(0,_pos) + ")");
						_where = "BOX_NO in (" + _where.Substring(_pos+1,_where.Length-_pos-1);
					}
					else
					{
						_alBar.Add(_where);
						break;
					}
				}
				//从资料库读取序列号资料
				DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
				SunlikeDataSet _dsBar = new SunlikeDataSet();
				DataTable _dtBox;
				for (int i=0;i<_alBar.Count;i++)
				{
					_dtBox = _bar.GetBoxData(_alBar[i].ToString());
					if (_dsBar.Tables["BAR_BOX"] == null)
					{
						_dsBar.Tables.Add(_dtBox.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBox,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBox = _dsBar.Tables["BAR_BOX"];
				string[] _aryBox = _sbWhere.ToString().Split(',');
				if (_dtBox.Rows.Count == _aryBox.Length)
				{
					//更新装箱主库
					Hashtable _ht = new Hashtable();
					for (int i=0;i<_dtBox.Rows.Count;i++)
					{
						string _wh = _dtBox.Rows[i]["WH"].ToString();
						string _boxNo = _dtBox.Rows[i]["BOX_NO"].ToString();
						if (_ht[_wh] == null)
						{
							_ht[_wh] = _boxNo;
						}
						else
						{
							_ht[_wh] += "," + _boxNo;
						}
					}
					IDictionaryEnumerator _ide = _ht.GetEnumerator();
					SunlikeDataSet _dsMfBox = null;
					while (_ide.MoveNext())
					{
						if (_dsMfBox == null)
						{
							_dsMfBox = _bar.GetMfBox(_ide.Key.ToString(),_ide.Value.ToString().Split(','));
						}
						else
						{
							SunlikeDataSet _dsTemp = _bar.GetMfBox(_ide.Key.ToString(),_ide.Value.ToString().Split(','));
							_dsMfBox.Merge(_dsTemp,false,MissingSchemaAction.AddWithKey);
						}
					}
					for (int i=0;i<_dtBox.Rows.Count;i++)
					{
						DataRow[] _dra = _dsMfBox.Tables["MF_BOX"].Select("WH='"
							+ _dtBox.Rows[i]["WH"].ToString() + "' and BOX_NO='"
							+ _dtBox.Rows[i]["BOX_NO"].ToString() + "'");
						if (_dra.Length > 0)
						{
							_dra[0]["QTY"] = Convert.ToInt32(_dra[0]["QTY"]) - 1;
							_dra[0]["UB_DD"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
						}
						else
						{
							DataRow _dr = _dsMfBox.Tables["MF_BOX"].NewRow();
							_dr["WH"] = _dtBox.Rows[i]["WH"].ToString();
							_dr["BOX_NO"] = _dtBox.Rows[i]["BOX_NO"].ToString();
							_dr["QTY"] = -1;
							_dr["UB_DD"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
							_dsMfBox.Tables["MF_BOX"].Rows.Add(_dr);
						}
						//修改箱库存
						WH _wh = new WH();
						_wh.UpdateBoxQty(_dtBox.Rows[i]["PRD_NO"].ToString(),_dtBox.Rows[i]["WH"].ToString(),_dtBox.Rows[i]["CONTENT"].ToString(),WH.BoxQtyTypes.QTY,-1);
					}
					this.UpdateDataSet(_dsMfBox);
					if (_dsMfBox.HasErrors)
					{
						throw new SunlikeException("无法更新装箱主库！");
					}
				}
				else
				{
					System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
					for (int i=0;i<_aryBox.Length;i++)
					{
						DataRow[] _draBox = _dtBox.Select("BOX_NO='" + _aryBox[i] + "'");
						if (_draBox.Length == 0)
						{
							_sbError.Append("箱条码[" + _aryBox[i] + "]不存在！\n");
						}
					}
					throw new Sunlike.Common.Utility.SunlikeException(_sbError.ToString());
				}
				//拆箱
				_where = "BOX_NO in ('" + _sbWhere.ToString().Replace(",","','") + "')";
				_bar.DeleteBox(_where,usr,bilID,bilNo);
			}
		}

		/// <summary>
		/// 拆箱（装箱作业）
		/// </summary>
		/// <param name="Wh">仓库代号</param>
		/// <param name="BoxNo">箱条码</param>
		/// <param name="Dep">部门代号</param>
		/// <param name="Usr">装箱人员</param>
		public void DeleteBox(string Wh,string BoxNo,string Dep,string Usr)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			_bar.DeleteBox(Wh,BoxNo,Dep,Usr);
		}
		#endregion

		#region 更新箱的库位
		/// <summary>
		/// 更新箱的库位
		/// </summary>
		/// <param name="BoxNoList">箱条码集合</param>
		/// <param name="WhList">库位集合</param>
		/// <param name="StopList">停用集合</param>
		public void UpdateBarBox(System.Collections.ArrayList BoxNoList,System.Collections.ArrayList WhList,System.Collections.ArrayList StopList)
		{
			StringBuilder _sql = new StringBuilder();
			for (int i=0;i<BoxNoList.Count;i++)
			{
				_sql.Append("update BAR_BOX set ");
				if (WhList[i] != null)
				{
					_sql.Append("WH='" + WhList[i] + "'");
				}
				if (StopList[i] != null)
				{
					if (WhList[i] != null)
					{
						_sql.Append(",");
					}
					_sql.Append("STOP_ID='" + StopList[i] + "'");
				}
				_sql.Append(" where BOX_NO='" + BoxNoList[i] + "'\n");
			}
			if (!String.IsNullOrEmpty(_sql.ToString()))
			{
				Query _query = new Query();
				_query.RunSql(_sql.ToString());
			}
		}
		#endregion

		#region 保存序列号记录
		/// <summary>
		/// 保存序列号记录
		/// </summary>
		/// <param name="ChangedDS"></param>
		/// <returns>错误信息表，如果没有行则没错误</returns>
		public DataTable UpdateRec(DataSet ChangedDS)
		{
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
			_ht["BAR_REC"] = "BAR_NO,BOX_NO,WH,UPDDATE,STOP_ID,PRD_NO,PRD_MARK,BAT_NO,SPC_NO,PH_FLAG";
			this.UpdateDataSet(ChangedDS,_ht);
			DataTable _dt = GetAllErrors(ChangedDS);
			return _dt;
		}
		/// <summary>
		/// BeforeUpdate
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="updateStatus"></param>
		protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus updateStatus)
		{
			if (tableName == "BAR_REC")
			{
                if (statementType == StatementType.Insert)
                {
                    //写入条码规格
                    BarPrint _barPrint = new BarPrint();
                    DataTable _dt = _barPrint.SelectBarPrint(dr["BAR_NO"].ToString());
                    if (_dt.Rows.Count > 0)
                    {
                        dr["SPC_NO"] = _dt.Rows[0]["SPC_NO"];
                    }
                }
                if (statementType != StatementType.Delete)
                {
                    //调拨单据保存时需要到货确认
                    if (dr.Table.DataSet.ExtendedProperties.ContainsKey("IC_CFM_SW"))
                    {
                        dr["PH_FLAG"] = dr.Table.DataSet.ExtendedProperties["IC_CFM_SW"].ToString();
                    }
                }
			}
		}

		#endregion

		#region 单据序列号补入
		
		/// <summary>
		/// 单据序列号补入
		/// </summary>
		/// <param name="ChangedDS">单据的DataSet</param>
		/// <param name="BarCodeDT">序列号原始表（输入的序列号存放在此表中）</param>
		/// <param name="BodyTableName">表身的名称</param>
		/// <param name="BodyBarItemName">表身与序列号表做对应的Key名称</param>
		/// <param name="BodyBoxItemName">表身与箱内容表做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="BodyWh1Name">表身的库栏位名称</param>
		/// <param name="BodyWh2Name">表身的库栏位名称</param>
		/// <param name="BarTableName">序列号表的名称</param>
		/// <param name="BarItemName">序列号表与表身做对应的Key名称</param>
		/// <param name="BoxTableName">箱内容表的名称（无则传入空值）</param>
		/// <param name="BoxItemName">箱内容表与表身做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="IsControl">是否严格控管序列号</param>
        /// <param name="IsStop">序列号是否已停用或不存在</param>
        /// <param name="IsExist">序列号是否必须存在</param>
        /// <param name="IsPhFlag">到货标志</param>
		public void RepairBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName,
            string BodyBoxItemName, string BodyWh1Name, string BodyWh2Name, string BarTableName, string BarItemName,
            string BoxTableName, string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag)
		{
			RepairBilData(ChangedDS,BarCodeDT,BodyTableName,BodyBarItemName,BodyBoxItemName,BodyWh1Name,BodyWh2Name,BarTableName,BarItemName,BoxTableName,BoxItemName,IsControl,IsStop,IsExist,false,IsPhFlag);
		}
		/// <summary>
		/// 单据序列号补入
		/// </summary>
		/// <param name="ChangedDS">单据的DataSet</param>
		/// <param name="BarCodeDT">序列号原始表（输入的序列号存放在此表中）</param>
		/// <param name="BodyTableName">表身的名称</param>
		/// <param name="BodyBarItemName">表身与序列号表做对应的Key名称</param>
		/// <param name="BodyBoxItemName">表身与箱内容表做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="BodyWh1Name">表身的出货库栏位名称</param>
		/// <param name="BodyWh2Name">表身的入货库栏位名称</param>
		/// <param name="BarTableName">序列号表的名称</param>
		/// <param name="BarItemName">序列号表与表身做对应的Key名称</param>
		/// <param name="BoxTableName">箱内容表的名称（无则传入空值）</param>
		/// <param name="BoxItemName">箱内容表与表身做对应的Key栏位名称（无箱内容表则传入空值）</param>
		/// <param name="IsControl">是否严格控管序列号</param>
		/// <param name="IsStop">序列号是否已停用或不存在</param>
        /// <param name="IsExist">序列号是否必须存在</param>
        /// <param name="IsSmartSplit">是否智能拆分</param>
        /// <param name="IsPhFlag">到货标志</param>
		public void RepairBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName,
            string BodyBoxItemName, string BodyWh1Name, string BodyWh2Name, string BarTableName, string BarItemName,
            string BoxTableName, string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsSmartSplit, bool IsPhFlag)
		{
            this.GetCollectData(true, ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
			DataTable _dtBarError = ChangedDS.Tables["BAR_ERROR"];
			DataTable _dtBarDel = ChangedDS.Tables["BAR_DEL"];
			DataRow _drBarError,_drBarDel;
			string[] _headerKey = new string[ChangedDS.Tables[0].PrimaryKey.Length];
			for (int i=0;i<ChangedDS.Tables[0].PrimaryKey.Length;i++)
			{
				_headerKey[i] = ChangedDS.Tables[0].PrimaryKey[i].ColumnName;
			}
			DataTable _dtBody = ChangedDS.Tables[BodyTableName];
			DataTable _dtBarCode = ChangedDS.Tables[BarTableName];
			DataTable _dtBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBox = ChangedDS.Tables[BoxTableName];
			}
			DataTable _dtBarRec = ChangedDS.Tables["BAR_REC"];
			DataTable _dtBarCollect = ChangedDS.Tables["BAR_COLLECT"];
			DataTable _dtBarBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBarBox = ChangedDS.Tables["BAR_BOX"];
			}
			if (_dtBarError.Rows.Count == 0)
			{
				DataRow[] _draBox;
				DataRow _drBarCode;
				int _keyItm = 0;
				int _boxItm = 0;
				string _barCode,_boxNo;
                StringBuilder _where = new StringBuilder();
				DataRow[] _draBarCode = _dtBarCode.Select();
				DataRow[] _draBody = _dtBody.Select();
				//把已输入的序列号放到Hashtable中
				System.Collections.Hashtable _htBarCode = new System.Collections.Hashtable();
				//把需要自动剔除的序列号放到Hashtable中
				System.Collections.Hashtable _htAutoDel = new System.Collections.Hashtable();
				_draBarCode = _dtBarCode.Select();
				for (int i=0;i<_draBarCode.Length;i++)
				{
					_htBarCode[_draBarCode[i]["BAR_CODE"].ToString()] = "";
				}
				//是箱条码放到_boxList中
				System.Collections.Hashtable _htBox = new System.Collections.Hashtable();
				DataRow[] _draBarCollect = BarCodeDT.Select();
				for (int i=0;i<_draBarCollect.Length;i++)
				{
					_boxNo = _draBarCollect[i]["BOX_NO"].ToString();
					if (!String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] == null)
					{
						_htBox[_boxNo] = "";
					}
				}
				if (_dtBarError.Rows.Count == 0)
				{
					System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
					for (int i=0;i<_dtBarCode.Rows.Count;i++)
					{
						if (_dtBarCode.Rows[i].RowState != DataRowState.Deleted)
						{
							if (_htBoxNo[_dtBarCode.Rows[i]["BOX_NO"].ToString()] == null)
							{
								_htBoxNo[_dtBarCode.Rows[i]["BOX_NO"].ToString()] = i;
							}
						}
					}
					
					for (int i=0;i<_dtBarRec.Rows.Count;i++)
					{
						_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
						string _batNo = _dtBarRec.Rows[i]["BAT_NO"].ToString();
						bool _canAdd = true;
						//在BAR_CODE表中查找此序列号是否已经补入
						if (_htBarCode[_barCode] == null)
						{
							//拆出货号、特征
							string _prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
							_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
							string _prdMark = "";
							if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
							{
								_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
							}
							_boxNo = _dtBarRec.Rows[i]["BOX_NO"].ToString();
							DataRow[] _aryDrCode = _dtBarCollect.Select("BAR_CODE='"+_barCode+"'");
							if (_aryDrCode.Length == 0 && !String.IsNullOrEmpty(_boxNo))
							{
								_aryDrCode = _dtBarCollect.Select("BOX_NO='"+_boxNo+"'");
							}
							//WebForm销货单收集箱时记录到BAR_CODE
							if (_aryDrCode.Length == 0 && !String.IsNullOrEmpty(_boxNo))
							{
								_aryDrCode = _dtBarCollect.Select("BAR_CODE='"+_boxNo+"'");
							}
							//记录原序列号资料,以便前台判断库位和批号
							_aryDrCode[0]["ISEXIST"] = _dtBarRec.Rows[i]["ISEXIST"];
                            _aryDrCode[0]["WH_REC"] = _dtBarRec.Rows[i]["WH"];
                            _aryDrCode[0]["BAT_NO_REC"] = _dtBarRec.Rows[i]["BAT_NO"];
                            _aryDrCode[0]["PH_FLAG"] = _dtBarRec.Rows[i]["PH_FLAG"];
							//是否要处理箱内容
							if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName)
								&& !String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] != null
								&& Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "T")
							{
								if (_htBoxNo[_boxNo] == null || string.IsNullOrEmpty(_htBoxNo[_boxNo].ToString()))
								{
									_drBarCode = null;
								}
								else
								{
									_drBarCode = _dtBarCode.Rows[Convert.ToInt32(_htBoxNo[_boxNo])];
								}
								//序列号表中是否有此BOX_NO
								if (_drBarCode != null)
								{
									_draBody = _dtBody.Select(BodyBarItemName + "=" + _drBarCode[BarItemName].ToString());
									if (String.IsNullOrEmpty(_draBody[0][BodyBoxItemName].ToString()))
									{
										_draBody = _dtBody.Select("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
											+ "' and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
									}
									else
									{
										_boxItm = Convert.ToInt32(_draBody[0][BodyBoxItemName]);
										_draBody = _dtBody.Select("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
											+ "' and " + BodyBoxItemName + "=" + _boxItm.ToString());
									}
									if (_draBody.Length == 0)
									{
										_drBarError = _dtBarError.NewRow();
										_drBarError["BAR_NO"] = _barCode;
										_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"找不到序列号对应的表身记录！";
										_drBarError["UNUSE"] = "T";
										_dtBarError.Rows.Add(_drBarError);
										_canAdd = false;
									}
									else
									{
										_canAdd = false;
										for (int j=0;j<_draBody.Length;j++)
										{
											//在BAR_CODE表中查找此货品、特征的数量
											_keyItm = Convert.ToInt32(_draBody[j][BodyBarItemName]);
											_draBarCode = _dtBarCode.Select(BarItemName + "=" + _keyItm.ToString());
											if (_draBarCode.Length < Math.Abs(Convert.ToDecimal(_draBody[j]["QTY"])))
											{
												if (!String.IsNullOrEmpty(_batNo) && _draBody[j]["BAT_NO"].ToString() != _batNo)
												{
													_drBarError = _dtBarError.NewRow();
													_drBarError["BAR_NO"] = _barCode;
													_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"找不到序列号对应的表身记录！";
													_drBarError["UNUSE"] = "T";
													_dtBarError.Rows.Add(_drBarError);
												}
												_canAdd = true;
												break;
											}
										}
										if (!_canAdd)
										{
											_drBarError = _dtBarError.NewRow();
											_drBarError["BAR_NO"] = _barCode;
											_drBarError["REM"] = "RCID=INV.HINT.COMPLETED";//"序列号对应货品的序列号已经补全！";
											_dtBarError.Rows.Add(_drBarError);
										}
									}
								}
								else
								{
									string _content = _dtBarBox.Select("BOX_NO='" + _boxNo + "'")[0]["CONTENT"].ToString();
									_draBox = _dtBox.Select("PRD_NO='" + _prdNo + "' and CONTENT='" + _content + "'");
									//箱内容有无此货品、配码比的记录
									if (_draBox.Length > 0)
									{
										_canAdd = false;
										for (int j=0;j<_draBox.Length;j++)
										{
											//查找这种箱补入序列号的数量
											_boxItm = Convert.ToInt32(_draBox[j][BoxItemName]);
											_draBody = _dtBody.Select(BodyBoxItemName + "=" + _boxItm.ToString());
											StringBuilder _itmList = new StringBuilder();
											for (int k=0;k<_draBody.Length;k++)
											{
												if (!String.IsNullOrEmpty(_itmList.ToString()))
												{
													 _itmList.Append(",");
												}
												_itmList.Append(_draBody[k][BodyBarItemName].ToString());
											}
											_draBarCode = _dtBarCode.Select(BarItemName + " in (" + _itmList + ")");
											StringBuilder _boxList = new StringBuilder();
											int _boxCount = 0;
											for (int k=0;k<_draBarCode.Length;k++)
											{
												if (_boxList.ToString().IndexOf(_draBarCode[k]["BOX_NO"].ToString()) < 0)
												{
													_boxList.Append(_draBarCode[k]["BOX_NO"].ToString() + ",");
													_boxCount ++;
												}
											}
											//这种箱已补入的数量小于这种箱的数量，就继续补入，否则再看单件产品
											if (_boxCount < Math.Abs(Convert.ToInt32(_draBox[j]["QTY"])))
											{
												_canAdd = true;
												_where = new StringBuilder("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
													+ "' and " + BodyBoxItemName + "=" + _boxItm.ToString());
												_keyItm = Convert.ToInt32(_dtBody.Select(_where.ToString())[0][BodyBarItemName]);
												if (!String.IsNullOrEmpty(_batNo))
												{
													_where.Append(" and BAT_NO = '" + _batNo + "'");
													if (_dtBody.Select(_where.ToString()).Length == 0)
													{
														_drBarError = _dtBarError.NewRow();
														_drBarError["BAR_NO"] = _barCode;
														_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"找不到序列号对应的表身记录！";
														_drBarError["UNUSE"] = "T";
														_dtBarError.Rows.Add(_drBarError);
													}
												}
												break;
											}
										}
										if (!_canAdd)
										{
											//在表身中查找此货品是否存在
                                            _where.Append("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark);
                                            _where.Append("' and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
											if (!String.IsNullOrEmpty(_batNo))
											{
												_where.Append(" and BAT_NO = '" + _batNo + "'");
											}
											_where.Append(" and " + BodyWh1Name + "='" + _aryDrCode[0]["WH1"].ToString() + "'");
											if (!String.IsNullOrEmpty(BodyWh2Name))
											{
												_where.Append(" and " + BodyWh2Name + "='" + _aryDrCode[0]["WH2"].ToString() + "'");
											}
											_draBody = _dtBody.Select(_where.ToString());
											if (_draBody.Length == 0 && ((IsControl && IsStop && string.IsNullOrEmpty(_dtBarRec.Rows[i]["WH"].ToString())) || !IsControl))
											{
												_draBody = _dtBody.Select(_where.ToString());
											}
											for (int k=0;k<_draBody.Length;k++)
											{
												//在BAR_CODE表中查找此货品、特征的数量
												_keyItm = Convert.ToInt32(_draBody[k][BodyBarItemName]);
												_draBarCode = _dtBarCode.Select(BarItemName + "=" + _keyItm.ToString());
												if (_draBarCode.Length < Math.Abs(Convert.ToDecimal(_draBody[k]["QTY"])))
												{
													_canAdd = true;
													break;
												}
											}
											if (_draBody.Length == 0)
											{
												_drBarError = _dtBarError.NewRow();
												_drBarError["BAR_NO"] = _barCode;
												_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"找不到序列号对应的表身记录！";
												_drBarError["UNUSE"] = "T";
												_dtBarError.Rows.Add(_drBarError);
											}
											else if (!_canAdd)
											{
												_drBarError = _dtBarError.NewRow();
												_drBarError["BAR_NO"] = _barCode;
												_drBarError["REM"] = "RCID=INV.HINT.COMPLETED";//"序列号对应货品的序列号已经补全！";
												_drBarError["UNUSE"] = "T";
												_dtBarError.Rows.Add(_drBarError);
											}
										}
									}
									else
									{
										//在表身中查找此货品是否存在
										_where = new StringBuilder("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
											+ "' and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
										if (IsExist)
										{
											_where.Append(_where.Append(_where.Append(_where.Append(" and ISNULL(BAT_NO, '') = '" + _batNo + "'"))));
										}
										else
										{
                                            _where.Append(" and ISNULL(BAT_NO, '') = '" + _aryDrCode[0]["BAT_NO"].ToString() + "'");
										}
										_where.Append(" and " + BodyWh1Name + "='" + _aryDrCode[0]["WH1"].ToString() + "'");
										if (!String.IsNullOrEmpty(BodyWh2Name))
										{
											_where.Append(" and " + BodyWh2Name + "='" + _aryDrCode[0]["WH2"].ToString() + "'");
										}
										_draBody = _dtBody.Select(_where.ToString());
										_canAdd = false;
										for (int j=0;j<_draBody.Length;j++)
										{
											//在BAR_CODE表中查找此货品、特征的数量
											_keyItm = Convert.ToInt32(_draBody[j][BodyBarItemName]);
											_draBarCode = _dtBarCode.Select(BarItemName + "=" + _keyItm.ToString());
											if (_draBarCode.Length < Math.Abs(Convert.ToDecimal(_draBody[j]["QTY"])))
											{
												_canAdd = true;
												break;
											}
										}
										if (_draBody.Length == 0)
										{
											_drBarError = _dtBarError.NewRow();
											_drBarError["BAR_NO"] = _barCode;
											_drBarError["REM"] = "RCID=COMMON.HINT.NOBODY";//"找不到序列号对应的表身记录！";
											_drBarError["UNUSE"] = "T";
											_dtBarError.Rows.Add(_drBarError);
										}
										else if (!_canAdd)
										{
											_drBarError = _dtBarError.NewRow();
											_drBarError["BAR_NO"] = _barCode;
											_drBarError["REM"] = "RCID=INV.HINT.COMPLETED";//"序列号对应货品的序列号已经补全！";
											_drBarError["UNUSE"] = "T";
											_dtBarError.Rows.Add(_drBarError);
										}
									}
									if (_canAdd && (_htBoxNo[_boxNo] == null || string.IsNullOrEmpty(_htBoxNo[_boxNo].ToString())))
									{
										_htBoxNo[_boxNo] = _dtBarCode.Rows.Count;
									}
								}
							}
							else
							{
								//在表身中查找此货品是否存在
                                _where = new StringBuilder("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark + "'");
								if (IsExist)
								{
									_where.Append(" and ISNULL(BAT_NO, '') = '" + _batNo + "'");
								}
								else
								{
									_where.Append(" and ISNULL(BAT_NO, '') = '" + _aryDrCode[0]["BAT_NO"].ToString() + "'");
								}
								if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
								{
									_where.Append(" and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
								}
								_where.Append(" and " + BodyWh1Name + "='" + _aryDrCode[0]["WH1"].ToString() + "'");
								if (!String.IsNullOrEmpty(BodyWh2Name))
								{
									_where.Append(" and " + BodyWh2Name + "='" + _aryDrCode[0]["WH2"].ToString() + "'");
								}
								_draBody = _dtBody.Select(_where.ToString());
								_canAdd = false;
								for (int j=0;j<_draBody.Length;j++)
								{
									//在BAR_CODE表中查找此货品、特征的数量
									_keyItm = Convert.ToInt32(_draBody[j][BodyBarItemName]);
									_draBarCode = _dtBarCode.Select(BarItemName + "=" + _keyItm.ToString());
									if (_draBarCode.Length < Math.Abs(Convert.ToDecimal(_draBody[j]["QTY"])))
									{
										_canAdd = true;
										break;
									}
								}
								if (_draBody.Length == 0)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BAR_NO"] = _barCode;
									_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"找不到序列号对应的表身记录！";
									_drBarError["UNUSE"] = "T";
									if (String.IsNullOrEmpty(_boxNo) || (!String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] == null))
									{
										_htAutoDel[_barCode] = "";
									}
									_dtBarError.Rows.Add(_drBarError);
								}
								else if (!_canAdd)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BAR_NO"] = _barCode;
									_drBarError["REM"] = "RCID=INV.HINT.COMPLETED";//"序列号对应货品的序列号已经补全！";
									_drBarError["UNUSE"] = "T";
									if (String.IsNullOrEmpty(_boxNo) || (!String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] == null))
									{
										_htAutoDel[_barCode] = "";
									}
									_dtBarError.Rows.Add(_drBarError);
								}
								else if (!String.IsNullOrEmpty(_boxNo))
								{
									//自动拆箱处理
									if (_htBox[_boxNo] == null)
									{
										if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
										{
											_boxNo = "";
										}
										else
										{
											_canAdd = false;
											_drBarError = _dtBarError.NewRow();
											_drBarError["BAR_NO"] = _barCode;
											_drBarError["REM"] = "RCID=INV.HINT.NOTBREAK";//"序列号已经装箱，你不能拆箱录入！";
											_dtBarError.Rows.Add(_drBarError);
										}
									}
								}
							}
							//插入到BAR_CODE表中
							if (_canAdd)
							{
								_drBarCode = _dtBarCode.NewRow();
								for (int k=0;k<_headerKey.Length;k++)
								{
									_drBarCode[_headerKey[k]] = ChangedDS.Tables[0].Rows[0][_headerKey[k]];
								}
								_drBarCode["ITM"] = _dtBarCode.Select().Length + 1;
								_drBarCode["PRD_NO"] = _prdNo;
								_drBarCode["PRD_MARK"] = _prdMark;
								_drBarCode["BAR_CODE"] = _barCode;
								_drBarCode["BOX_NO"] = _boxNo;
								_drBarCode[BarItemName] = _keyItm;
								_dtBarCode.Rows.Add(_drBarCode);
								_htBarCode[_barCode] = "";
							}
						}
					}
				}
				//智能拆分
				if (IsSmartSplit)
				{
					string _billIdFieldName = "";
					string _billNoFieldName = "";
					string _billItmFiledName = "";
					if (ChangedDS.ExtendedProperties["BILLID_FIELD"] != null)
					{
						_billIdFieldName = ChangedDS.ExtendedProperties["BILLID_FIELD"].ToString();
					}
                    if (ChangedDS.ExtendedProperties["BILLNO_FIELD"] != null)
					{
						_billNoFieldName = ChangedDS.ExtendedProperties["BILLNO_FIELD"].ToString();
					}
                    if (ChangedDS.ExtendedProperties["BILLITM_FIELD"] != null)
					{
						_billItmFiledName = ChangedDS.ExtendedProperties["BILLITM_FIELD"].ToString();
					}
					Prdt _prdt = new Prdt();
					foreach (DataRow dr in _dtBody.Select("isnull(BAT_NO, '') = ''"))
					{
						DataRow[] _dra = _dtBarCode.Select(BarItemName + "=" + dr[BodyBarItemName].ToString());
						int _codeCount = _dra.Length;
						if (_codeCount < Math.Abs(Convert.ToInt32(dr["QTY"])))
						{
							//序列号没有补全
							DataRow[] _draError = _dtBarError.Select("UNUSE = 'T' AND BAR_NO LIKE '%" + dr["PRD_NO"].ToString() + "%'");
							foreach (DataRow drError in _draError)
							{
								DataRow _drBarRec = _dtBarRec.Rows.Find(new string[1]{drError["BAR_NO"].ToString()});
								if (_drBarRec != null)
								{
									//拆出货号、特征
									int _keyItm1;
									string _barCode1 = _drBarRec["BAR_NO"].ToString();
									string _boxNo1 = _drBarRec["BOX_NO"].ToString();
									if (_htBox[_boxNo1] == null)
									{
										_boxNo1 = "";
									}
									string _prdNo = _barCode1.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1).Replace(BarRole.TrimChar,"");
									string _prdMark = "";
									if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
									{
										_prdMark = _barCode1.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
									}
									string _wh1 = "";
									string _wh2 = "";
									string _batNoNew = _drBarRec["BAT_NO"].ToString();
									DataRow[] _aryBarCollect = _dtBarCollect.Select("BAR_CODE='"+_barCode1+"'");
									if (_aryBarCollect.Length == 0)
									{
										_aryBarCollect = _dtBarCollect.Select("BOX_NO='"+_boxNo1+"'");
									}
									if (_aryBarCollect.Length > 0)
									{
										_wh1 = _aryBarCollect[0]["WH1"].ToString();
										if (!String.IsNullOrEmpty(BodyWh2Name))
										{
											_wh2 = _aryBarCollect[0]["WH2"].ToString();
										}
										if (!IsExist)
										{
											_batNoNew = _aryBarCollect[0]["BAT_NO"].ToString();
										}
									}
									if (dr["PRD_NO"].ToString() == _prdNo
										&& dr["PRD_MARK"].ToString() == _prdMark
										&& dr[BodyWh1Name].ToString() == _wh1)
									{
										if (String.IsNullOrEmpty(BodyWh2Name) || (!String.IsNullOrEmpty(BodyWh2Name) && dr[BodyWh2Name].ToString() == _wh2))
										{
											string _sqlWhere = "PRD_NO='" + dr["PRD_NO"].ToString()
												+ "' AND PRD_MARK='" + dr["PRD_MARK"].ToString()
												+ "' AND BAT_NO='" + _batNoNew
												+ "' AND " + BodyWh1Name + "='" + _wh1 + "'";
											if (!String.IsNullOrEmpty(BodyWh2Name))
											{
												_sqlWhere += " AND " + BodyWh2Name + "='" + _wh2 + "'";
											}
											if (!String.IsNullOrEmpty(_billIdFieldName))
											{
												_sqlWhere += " AND ISNULL(" + _billIdFieldName + ", '') = '" + dr[_billIdFieldName].ToString() + "'";
											}
											if (!String.IsNullOrEmpty(_billNoFieldName))
											{
												_sqlWhere += " AND ISNULL(" + _billNoFieldName + ", '') = '" + dr[_billNoFieldName].ToString() + "'";
											}
											if (!String.IsNullOrEmpty(_billItmFiledName))
											{
												_sqlWhere += " AND ISNULL(" + _billItmFiledName + ", '') = '" + dr[_billItmFiledName].ToString() + "'";
											}
											DataRow[] _draBody1 = _dtBody.Select(_sqlWhere);
											if (_draBody1.Length == 0)
											{
												//拆分并新增表身
												DataRow _drNew = _dtBody.NewRow();
												foreach (DataColumn dc in _dtBody.Columns)
												{
													if (!dc.AutoIncrement)
													{
														_drNew[dc.ColumnName] = dr[dc.ColumnName];
													}
												}
												_drNew["ITM"] = GetMaxItm(_dtBody, "ITM");
												_drNew["QTY"] = Convert.ToInt32(dr["QTY"]) < 0 ? -1 : 1;
												_drNew["BAT_NO"] = _batNoNew;
												SunlikeDataSet _dsBatRec = _prdt.GetBatRecData(_batNoNew, dr["PRD_NO"].ToString(),
													dr["PRD_MARK"].ToString(), _wh1);
												if (_dsBatRec.Tables[0].Rows.Count > 0)
												{
													_drNew["VALID_DD"] = _dsBatRec.Tables[0].Rows[0]["VALID_DD"];
												}
												_dtBody.Rows.Add(_drNew);
												_keyItm1 = Convert.ToInt32(_drNew[BodyBarItemName]);
												//删除BarError
												drError.Delete();
											}
											else
											{
												//修改已有记录
												_draBody1[0]["QTY"] = Convert.ToDecimal(_draBody1[0]["QTY"])
													+ (Convert.ToDecimal(_draBody1[0]["QTY"]) < 0 ? -1 : 1);
												_keyItm1 = Convert.ToInt32(_draBody1[0][BodyBarItemName]);
												//删除BarError
												drError.Delete();
											}
											//扣减原数量
											dr["QTY"] = Convert.ToDecimal(dr["QTY"]) - (Convert.ToInt32(dr["QTY"]) < 0 ? -1 : 1);

											//新增序列号记录
											DataRow _drBarCode1 = _dtBarCode.NewRow();
											for (int k=0;k<_headerKey.Length;k++)
											{
												_drBarCode1[_headerKey[k]] = ChangedDS.Tables[0].Rows[0][_headerKey[k]];
											}
											_drBarCode1["ITM"] = this.GetMaxItm(_dtBarCode, "ITM");
											_drBarCode1["PRD_NO"] = _prdNo;
											_drBarCode1["PRD_MARK"] = _prdMark;
											_drBarCode1["BAR_CODE"] = _barCode1;
											_drBarCode1["BOX_NO"] = _boxNo1;
											_drBarCode1[BarItemName] = _keyItm1;
											_dtBarCode.Rows.Add(_drBarCode1);
											if (Convert.ToDecimal(dr["QTY"]) == 0)
											{
												dr.Delete();
												break;
											}
										}
									}
								}
							}
						}
					}
				}
				//把需要自动剔除的序列号放到BAR_DEL
				for (int i=_dtBarError.Rows.Count-1;i>=0;i--)
				{
					if (_htAutoDel[_dtBarError.Rows[i]["BAR_NO"].ToString()] != null)
					{
						//删除序列号信息
						_draBarCollect = _dtBarCollect.Select("BAR_CODE='" + _dtBarError.Rows[i]["BAR_NO"].ToString() + "'");
						if (_draBarCollect.Length > 0)
						{
							_draBarCollect[0].Delete();
						}
						//增加警告信息
						_drBarDel = _dtBarDel.NewRow();
						_drBarDel["BAR_NO"] = _dtBarError.Rows[i]["BAR_NO"].ToString();
						_drBarDel["REM"] = _dtBarError.Rows[i]["REM"];//"找不到序列号对应的表身记录！";
						_dtBarDel.Rows.Add(_drBarDel);
						//删除错误信息
						_dtBarError.Rows[i].Delete();
					}
				}
				if (_dtBarError.Rows.Count == 0)
				{
					BarCodeDT.AcceptChanges();
				}
				ChangedDS.Tables.Remove("BAR_REC");
				if (ChangedDS.Tables["BAR_BOX"] != null)
				{
					ChangedDS.Tables.Remove("BAR_BOX");
				}
			}
		}
		private int GetMaxItm(DataTable dt, string field)
		{
			DataRow[] _aryDr = dt.Select("", field + " DESC");
			if (_aryDr.Length > 0)
			{
				return Convert.ToInt32(_aryDr[0][field]) + 1;
			}
			return 1;
		}
		#endregion

		#region 取得序列号转入格式
		/// <summary>
		/// 取得序列号转入格式
		/// </summary>
		/// <param name="DocNo">文档格式代号</param>
		/// <returns></returns>
		public DataTable GetBarDoc(string DocNo)
		{
			string _where = " where ";
			if (String.IsNullOrEmpty(DocNo.Trim()))
			{
				_where += "DEF_ID='T'";
			}
			else
			{
				_where += "DOC_NO='" + DocNo + "'";
			}
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBarDoc(_where);
			return _dt;
		}
		#endregion
		
		#region 序列号追踪 by db
		/// <summary>
		/// 序列号追踪
		/// </summary>
		/// <param name="BarCodeTrail">序列号号码组合</param>
		/// <returns></returns>
		public SunlikeDataSet GetBarCodeTrail(string BarCodeTrail)
		{
			//取得特征分段数据
			PrdMark _mark = new PrdMark();
			DataTable _dtMark = _mark.GetSplitData("");

			#region Create Table BAR_CODE
			DataTable _dtBarCode = new DataTable("BAR_CODE");

			DataColumn BAR_NO = new DataColumn("BAR_NO");//序列号
			BAR_NO.DataType = typeof(System.String);
			BAR_NO.MaxLength = 90;

			DataColumn UPDDATE = new DataColumn("UPDDATE");//更新时间
			UPDDATE.DataType = typeof(System.DateTime);			

			DataColumn BOX_NO = new DataColumn("BOX_NO");//箱条码
			BOX_NO.DataType = typeof(System.String);
			BOX_NO.MaxLength = 90;

			DataColumn CONTENT = new DataColumn("CONTENT");//配码比
			BOX_NO.DataType = typeof(System.String);
			BOX_NO.MaxLength = 255;

			DataColumn PRD_NO = new DataColumn("PRD_NO");//货品代号
			PRD_NO.DataType = typeof(System.String);
			PRD_NO.MaxLength = 30;

			DataColumn PRD_NAME = new DataColumn("PRD_NAME");//货品名称
			PRD_NAME.DataType = typeof(System.String);
			PRD_NAME.MaxLength = 40;

			DataColumn WH = new DataColumn("WH");//库位代号
			WH.DataType = typeof(System.String);
			WH.MaxLength = 12;

			DataColumn WH_NAME = new DataColumn("WH_NAME");//库位名称
			WH_NAME.DataType = typeof(System.String);
			WH_NAME.MaxLength = 40;

			DataColumn CUS_NO = new DataColumn("CUS_NO");//客户代号
			CUS_NO.DataType = typeof(System.String);
			CUS_NO.MaxLength = 12;

			DataColumn CUS_NAME = new DataColumn("CUS_NAME");//客户名称
			CUS_NAME.DataType = typeof(System.String);
			CUS_NAME.MaxLength = 50;

			DataColumn STOP_ID = new DataColumn("STOP_ID");//停用否
			STOP_ID.DataType = typeof(System.String);
			STOP_ID.MaxLength = 2;

			DataColumn SPC_NAME = new DataColumn("SPC_NAME");//规格
			SPC_NAME.DataType = typeof(System.String);
			SPC_NAME.MaxLength = 30;

			DataColumn PMARK = new DataColumn("PMARK");//PMARK
			PMARK.DataType = typeof(System.String);
			PMARK.MaxLength = 100;

			DataColumn PMARK_NAME = new DataColumn("PMARK_NAME");//PMARK_NAME
			PMARK_NAME.DataType = typeof(System.String);
			PMARK_NAME.MaxLength = 100;

			DataColumn USR = new DataColumn("USR");//PMARK_NAME
			USR.DataType = typeof(System.String);
			USR.MaxLength = 50;

            
			_dtBarCode.Columns.Add(BAR_NO);
			_dtBarCode.Columns.Add(UPDDATE);
			_dtBarCode.Columns.Add(BOX_NO);
			_dtBarCode.Columns.Add(CONTENT);
			_dtBarCode.Columns.Add(PRD_NO);
			_dtBarCode.Columns.Add(PRD_NAME);
			_dtBarCode.Columns.Add(WH);
			_dtBarCode.Columns.Add(WH_NAME);
			_dtBarCode.Columns.Add(CUS_NO);
			_dtBarCode.Columns.Add(CUS_NAME);
			_dtBarCode.Columns.Add(STOP_ID);
			_dtBarCode.Columns.Add(SPC_NAME);
			_dtBarCode.Columns.Add(PMARK);
			_dtBarCode.Columns.Add(PMARK_NAME);
			_dtBarCode.Columns.Add(USR);

			for (int i=0;i<_dtMark.Rows.Count;i++)
			{
				_dtBarCode.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString());
				_dtBarCode.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString() + "_DSC");
			}
			#endregion

			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			StringBuilder _barBoxStr = new StringBuilder(" 1<>1 ");
            StringBuilder _barCodeStr = new StringBuilder(" ");
			string[] _barCodeAry = BarCodeTrail.Split(new char[]{';'});
			SunlikeDataSet _dsBox = new SunlikeDataSet();
			SunlikeDataSet _dsRec = new SunlikeDataSet();
			SunlikeDataSet _ds = new SunlikeDataSet();
			Prdt _prdt = new Prdt();

			if (!String.IsNullOrEmpty(BarCodeTrail))
			{

				#region 全部序列号统一整理为货品序列号字符串
				for (int i = 0; i < _barCodeAry.Length; i++)
				{
					if ((_barCodeAry[i] != null) && (!String.IsNullOrEmpty(_barCodeAry[i])))
					{
						//判断是不是箱条码
						if (_barCodeAry[i].Substring(BarCode.BarRole.BoxPos,1) == BarCode.BarRole.BoxFlag)
						{
							_barBoxStr.Append(" OR A.BOX_NO = '"+_barCodeAry[i]+"'");
						}
						else
						{
                            _barCodeStr.Append(_barCodeAry[i] + ";");
						}
					}
				}
				#endregion	
				

				#region 开始对箱条码进行拆解码动作
				_dsBox = _bar.GetBoxDataFind(_barBoxStr.ToString(),Comp.CompNo);
				if ( _dsBox.Tables["BAR_BOX"].Rows.Count > 0 )
				{
					DataTable _dtBarBox = _dsBox.Tables[0];

					if (_dtBarBox.Rows.Count > 0)
					{
						//开始拆解
						DataRow[] _drb = _dtBarBox.Select();
						for (int i=0;i<_drb.Length;i++)
						{
							DataRow _dr = _dtBarCode.NewRow();							
							_dr["BAR_NO"] = _drb[i]["BOX_NO"];
							_dr["BOX_NO"] = _drb[i]["BOX_NO"];
							_dr["CONTENT"] = _drb[i]["CONTENT"];
							_dr["WH"] = _drb[i]["WH"];
							_dr["UPDDATE"] = _drb[i]["BB_DD"];
							_dr["STOP_ID"] = _drb[i]["STOP_ID"];
							_dr["SPC_NAME"] = "";
                            //_dr["CUS_NO"] = _drb[i]["CUS_NO"];
                            //_dr["CUS_NAME"] = _drb[i]["CUS_NAME"];
							_dr["PRD_NO"] = _drb[i]["PRD_NO"];
							_dr["USR"] = _drb[i]["USR_NAME"];
							_dtBarCode.Rows.Add(_dr);
						}
						_dtBarCode.AcceptChanges();
						//取得货号名称、特征名						
						_prdt.AddBilPrdName(_dtBarCode,"PRD_NAME","_DSC","WH","WH_NAME",false,"","","",DateTime.Today,"");
					}
				}
				#endregion

				#region 开始对货品序列号进行拆解码动作
				_dsRec = _bar.GetBarCodeTrail(_barCodeStr.ToString(),Comp.CompNo);
				if ( _dsRec.Tables["BAR_REC"].Rows.Count > 0 )
				{
					DataTable _dtBarRec = _dsRec.Tables["BAR_REC"];

					if (_dtBarRec.Rows.Count > 0)
					{
						//开始拆解
						DataRow[] _dra = _dtBarRec.Select();
						for (int i=0;i<_dra.Length;i++)
						{
							DataRow _dr = _dtBarCode.NewRow();
							string _barCode = _dra[i]["BAR_NO"].ToString();
							_dr["BAR_NO"] = _barCode;
							_dr["BOX_NO"] = _dra[i]["BOX_NO"];
							_dr["WH"] = _dra[i]["WH"];
							_dr["UPDDATE"] = _dra[i]["UPDDATE"];
                            //_dr["CUS_NO"] = _dra[i]["CUS_NO"];
							_dr["CUS_NAME"] = _dra[i]["CUS_NAME"];
							_dr["STOP_ID"] = _dra[i]["STOP_ID"];
							_dr["SPC_NAME"] = _dra[i]["SPC_NAME"];
							_dr["USR"] = _dra[i]["USR_NAME"];


							string _prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
							_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
							_dr["PRD_NO"] = _prdNo;
							string _prdMark = "";
							if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
							{
								_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
							}
							string[] _aryPrdMark = _mark.BreakPrdMark(_prdMark);
							for (int j=0;j<_dtMark.Rows.Count;j++)
							{
								_dr[_dtMark.Rows[j]["FLDNAME"].ToString()] = _aryPrdMark[j];
								if (j == 0)
									_dr["PMARK"] += _aryPrdMark[j].ToString();
								else
									_dr["PMARK"] += " " + _aryPrdMark[j].ToString();
							}
							_dtBarCode.Rows.Add(_dr);
						}
						_dtBarCode.AcceptChanges();
						//取得货号名称、特征名
						_prdt.AddBilPrdName(_dtBarCode,"PRD_NAME","_DSC","WH","WH_NAME",false,"","","",DateTime.Today,"");
						for (int i = 0; i < _dtBarCode.Rows.Count; i++)
						{
							for (int j=0;j<_dtMark.Rows.Count;j++)
							{							
								if ( j == 0)
								{
									_dtBarCode.Rows[i]["PMARK_NAME"] += _dtBarCode.Rows[i][_dtMark.Rows[j]["FLDNAME"].ToString() + "_DSC"].ToString();
								}
								else
								{
									_dtBarCode.Rows[i]["PMARK_NAME"] += " " + _dtBarCode.Rows[i][_dtMark.Rows[j]["FLDNAME"].ToString() + "_DSC"].ToString();
								}
							}
						}
						for (int i=0;i<_dra.Length;i++)
						{
							if (_dtBarCode.Select("BAR_NO='" + _dra[i]["BAR_NO"].ToString() + "'").Length == 0)
							{
								_dra[i].Delete();
							}
						}
					}
				}
				#endregion

			}	
			
			_ds.Tables.Add(_dtBarCode);		

			return _ds;
		}
		#endregion

		#region 得到序列号追踪明细记录 by db
		/// <summary>
		/// 得到序列号追踪明细记录
		/// </summary>
		/// <param name="Bar_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarCodeDetailList(string Bar_No)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.GetBarCodeDetailList(Bar_No);			
		}
		/// <summary>
		/// 得到序列号追踪明细记录
		/// </summary>
		/// <param name="Box_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarBoxDetailList(string Box_No)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			SunlikeDataSet _ds = _bar.GetBarBoxDetailList(Box_No);
			return _ds;
		}
		#endregion

		#region 得到要下载到本地的文件(离线装箱)
		/// <summary>
		/// 得到要下载到本地的文件(离线装箱)
		/// </summary>
		/// <param name="Ht"></param>
		/// <returns></returns>
        public SunlikeDataSet GetBarBoxToLocal(Dictionary<String, Object> Ht)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);			
			SunlikeDataSet _ds = _bar.GetBarBoxToLocal(Ht);			
			return _ds;
		}
		#endregion
		
		#region 装箱套版
		/// <summary>
		/// 装箱套版
		/// </summary>
		/// <param name="boxNo"></param>
		/// <returns></returns>
		public DataTable GetPrintData(string boxNo)
		{
			DbBarCode _dbCode = new DbBarCode(Comp.Conn_DB);
			return _dbCode.GetPrintData(boxNo);
		}
		#endregion

		#region 写入序列号变更记录
		/// <summary>
		/// 写入序列号变更记录
		/// </summary>
		/// <param name="BarNo">序列号</param>
		/// <param name="Wh1">原始库位</param>
		/// <param name="Wh2">当前库位</param>
		/// <param name="BilID">来源单据别</param>
		/// <param name="BilNo">来源单号</param>
        /// <param name="Usr">操作员</param>
        /// <param name="BatNo1">原始批号</param>
        /// <param name="BatNo2">当前批号</param>
        /// <param name="phFlag1"></param>
        /// <param name="phFlag2"></param>
        /// <param name="stopId1"></param>
        /// <param name="stopId2"></param>
		/// <param name="IsGetSQL">True：返回Insert SQL语句，方便做批次增加。False：直接写入</param>
		/// <returns></returns>
		public string InsertBarChange(string BarNo,string Wh1,string Wh2,string BilID,string BilNo,string Usr,string BatNo1,string BatNo2,string phFlag1, string phFlag2, string stopId1, string stopId2, bool IsGetSQL)
		{
            string _sql = "insert into BAR_CHANGE (BAR_NO,UPDDATE,WH1,WH2,BIL_NO,USR,BIL_ID,BAT_NO1,BAT_NO2,PH_FLAG1,PH_FLAG2,STOP_ID1,STOP_ID2) values ('"
                + BarNo + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Wh1 + "','"
                + Wh2 + "','" + BilNo + "','" + Usr + "','" + BilID + "','" + BatNo1 + "','" + BatNo2 + "','" + phFlag1 + "','" + phFlag2 + "','" + stopId1 + "','" + stopId2 + "')\n";
			if (IsGetSQL)
			{
				return _sql;
			}
			else
			{
				Query _query = new Query();
				_query.RunSql(_sql);
				return "";
			}
        }
        /// <summary>
        /// 写入序列号变更记录
        /// </summary>
        /// <param name="BarNo">序列号</param>
        /// <param name="Wh1">原始库位</param>
        /// <param name="Wh2">当前库位</param>
        /// <param name="BilID">来源单据别</param>
        /// <param name="BilNo">来源单号</param>
        /// <param name="Usr">操作员</param>
        /// <param name="BatNo1">原始批号</param>
        /// <param name="BatNo2">当前批号</param>
        /// <param name="phFlag1"></param>
        /// <param name="phFlag2"></param>
        /// <param name="IsGetSQL">True：返回Insert SQL语句，方便做批次增加。False：直接写入</param>
        /// <returns></returns>
        public string InsertBarChange(string BarNo, string Wh1, string Wh2, string BilID, string BilNo, string Usr, string BatNo1, string BatNo2, string phFlag1, string phFlag2, bool IsGetSQL)
        {
            string _sql = "insert into BAR_CHANGE (BAR_NO,UPDDATE,WH1,WH2,BIL_NO,USR,BIL_ID,BAT_NO1,BAT_NO2,PH_FLAG1,PH_FLAG2) values ('"
                + BarNo + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Wh1 + "','"
                + Wh2 + "','" + BilNo + "','" + Usr + "','" + BilID + "','" + BatNo1 + "','" + BatNo2 + "','" + phFlag1 + "','" + phFlag2 + "')\n";
            if (IsGetSQL)
            {
                return _sql;
            }
            else
            {
                Query _query = new Query();
                _query.RunSql(_sql);
                return "";
            }
        }
		#endregion

		#region 取得序列号变更记录
		/// <summary>
		/// 取得序列号变更记录
		/// </summary>
		/// <param name="BilID">来源单据类别</param>
		/// <param name="BilNo">来源单号</param>
		/// <returns></returns>
		public DataTable GetBarChange(string BilID,string BilNo)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBarChange(BilID,BilNo);
			return _dt;
		}
        /// <summary>
        /// 取得序列号变更记录
        /// </summary>
        /// <param name="barNo">序列号</param>
        /// <param name="upDate">更新日期</param>
        /// <returns></returns>
        public DataTable GetBarChange(string barNo, DateTime upDate)
        {
            DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
            DataTable _dt = _bar.GetBarChange(barNo, upDate);
            return _dt;
        }
		#endregion

		#region 删除序列号变更记录
		/// <summary>
		/// 删除序列号变更记录
		/// </summary>
		/// <param name="BarNo">序列号</param>
		/// <param name="BilID">来源单据别</param>
		/// <param name="BilNo">来源单号</param>
		/// <param name="IsGetSQL">True：返回Delete SQL语句，方便做批次增加。False：直接删除</param>
		/// <returns></returns>
		public string DeleteBarChange(string BarNo,string BilID,string BilNo,bool IsGetSQL)
		{
			string _sql = "delete from BAR_CHANGE where BAR_NO='" + BarNo + "' and BIL_ID='" + BilID + "' and BIL_NO='" + BilNo + "'\n";
			if (IsGetSQL)
			{
				return _sql;
			}
			else
			{
				Query _query = new Query();
				_query.RunSql(_sql);
				return "";
			}
		}
		#endregion

		#region 判断在BAR_PRINT中有无此序列号
		/// <summary>
		/// 判断在BAR_PRINT中有无此序列号
		/// </summary>
		/// <param name="BarCode">条码字符串(组SQL条件)</param>
		/// <returns>返回一个ArrayList,存储两个字符串，第一个存储不存在的条码记录，第二个存储不存在规格的条码</returns>
		public ArrayList ChkBarPrint(string BarCode)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.ChkBarPrint(BarCode);			
		}
		#endregion

		#region 得到产品的规格(离线装箱用)
		/// <summary>
		/// 得到产品的规格(离线装箱用)
		/// </summary>
		/// <param name="Ht">存储规格的Hashtable</param>
		/// <returns></returns>
        public SunlikeDataSet GetSpc(Dictionary<String, Object> Ht)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.GetSpc(Ht);	
		}
		#endregion

		#region 整理单据序列号得出箱及件的数量
		/// <summary>
		/// 整理单据序列号得出箱及件的数量  
		/// 返回DataSet中有表 BARCODE(PRD_NO,PRD_MARK,QTY), BOX(PRD_NO,CONENT,QTY), BARBOX(PRD_NO,PRD_MARK,QTY)
		/// </summary>
		/// <param name="barCollect">收集的序列号</param>
		/// <param name="errorMessage">返回的错误信息</param>
		/// <returns></returns>
		public SunlikeDataSet CollectBarCode(DataTable barCollect,out string errorMessage)
		{
			SunlikeDataSet _coalitionBarDS = new SunlikeDataSet();
			#region 表结构
			//序列号表
			DataTable _barTable = new DataTable("BARCODE");
			_barTable.Columns.Add("PRD_NO");
			_barTable.Columns.Add("PRD_MARK");
			_barTable.Columns.Add("QTY");
			DataColumn[] _dcaBar = new DataColumn[2];
			_dcaBar[0] = _barTable.Columns["PRD_NO"];
			_dcaBar[1] = _barTable.Columns["PRD_MARK"];
			_barTable.PrimaryKey = _dcaBar;
			//箱条码表
			DataTable _boxTable = new DataTable("BOX");
			_boxTable.Columns.Add("PRD_NO");
			_boxTable.Columns.Add("CONTENT");
			_boxTable.Columns.Add("QTY");
			DataColumn[] _dcaBox = new DataColumn[2];
			_dcaBar[0] = _boxTable.Columns["PRD_NO"];
			_dcaBar[1] = _boxTable.Columns["CONTENT"];
			_boxTable.PrimaryKey = _dcaBox;

			#endregion

			//把箱条码和序列号分开
			System.Text.StringBuilder _sbBox = new System.Text.StringBuilder();
			System.Text.StringBuilder _sbBar = new System.Text.StringBuilder();
			string _barCode,_boxNo;

			try
			{
				for (int i = 0 ; i < barCollect.Rows.Count;i++)			
				{
					_barCode = barCollect.Rows[i]["BOX_NO"].ToString();
					if (String.IsNullOrEmpty(_barCode))
						_barCode = barCollect.Rows[i]["BAR_CODE"].ToString();
					if (_barCode.Substring(BarRole.BoxPos,1) == BarRole.BoxFlag)
					{
						if (_sbBox.ToString().IndexOf(_barCode) < 0)
						{
							if (_sbBox.Length > 0)
							{
								_sbBox.Append(",");
							}
							_sbBox.Append(_barCode);
						}
					}
					else
					{
						if (_sbBar.Length > 0)
						{
							_sbBar.Append(",");
						}
						_sbBar.Append(_barCode);
					}
				
				}
				//查找序列号资料
				string _where = "";
				string _whereBox = "";
				if (_sbBox.Length > 0)
				{
					_whereBox = "BOX_NO in ('" + _sbBox.ToString().Replace(",","','") + "')";
				}
				else
				{
					_whereBox = "1<>1";
				}
				if (_sbBar.Length > 0)
				{
					_where += "BAR_NO in ('" + _sbBar.ToString().Replace(",","','") + "')";
				}
				if (String.IsNullOrEmpty(_where.Trim()))
				{
					_where = "1<>1";
				}
				//从资料库读取序列号资料
				DataTable _dtBarBox = this.GetBoxData(_whereBox);
				DataTable _dtBarRec = this.GetBarRecord(_where,false);
				DataTable _dtBarRec1 = this.GetBarRecord(_whereBox,false);
				//检查序列号是否存在
				System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
				//检查箱条码是否存在
				if (_sbBox.Length > 0)
				{
					string[] _aryBoxList = _sbBox.ToString().Split(new char[] {','});
					for (int i = 0 ; i < _aryBoxList.Length;i++)
					{
						if (_dtBarRec1.Select("BOX_NO='" + _aryBoxList[i] + "'").Length == 0)
						{
							_sbError.Append("箱条码" + _aryBoxList[i] + "不存在！\n");
						}
					}
				}
				//检查序列号是否存在
				if (_sbBar.Length > 0)
				{
					string[] _aryBarList = _sbBar.ToString().Split(new char[] {','});
					for (int i=0;i<_aryBarList.Length;i++)
					{
						if (_dtBarRec.Select("BAR_NO='" + _aryBarList[i] + "'").Length == 0)
						{
							_sbError.Append("序列号" + _aryBarList[i] + "不存在！\n");
						}
					}
				}
				if (_sbError.Length == 0 )
				{
					string _prdNo = "";
					string _prdMark  = "";
					//序列号收集
					for (int i = 0 ; i < _dtBarRec.Rows.Count; i++)
					{
						_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
						if (_sbBar.ToString().IndexOf(_barCode) > -1)
						{
							//拆出货号、特征
							_prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
							_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
							_prdMark = "";
							if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
							{
								_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
							}
							DataRow[] _aryHasBar = _barTable.Select("PRD_NO='"+_prdNo+"' AND PRD_MARK='"+_prdMark+"'");
							if (_aryHasBar.Length > 0 )
							{
								decimal _qtyBar = 0;
								if (!String.IsNullOrEmpty(_aryHasBar[0]["QTY"].ToString()))
								{
									_qtyBar = Convert.ToDecimal(_aryHasBar[0]["QTY"]);
								}
								_aryHasBar[0]["QTY"] = _qtyBar + 1 ;
							}
							else
							{
								DataRow _drBar = _barTable.NewRow();
								_drBar["PRD_NO"] = _prdNo;
								_drBar["PRD_MARK"] = _prdMark;
								_drBar["QTY"] = 1;	
								_barTable.Rows.Add(_drBar);
							}
						}
					}
					//箱条码收集
					for (int i= 0 ; i < _dtBarRec1.Rows.Count;i++)
					{
						_boxNo = _dtBarRec1.Rows[i]["BOX_NO"].ToString();
						DataRow[] _aryBoxSelect = _dtBarBox.Select(" BOX_NO='"+_boxNo+"'");
						if (_aryBoxSelect.Length > 0 )
						{
							_prdNo =_aryBoxSelect[0]["PRD_NO"].ToString();
						}
						if (_sbBox.ToString().IndexOf(_boxNo) > -1)
						{
							string _content = _dtBarBox.Select("BOX_NO='" + _boxNo + "'")[0]["CONTENT"].ToString();						
							DataRow[] _aryHasBox = _boxTable.Select("PRD_NO='"+_prdNo+"' AND CONTENT='"+_content+"'");
							if (_aryHasBox.Length > 0 )
							{
								decimal _qtyBox = 0;
								if (!String.IsNullOrEmpty(_aryHasBox[0]["QTY"].ToString()))
								{
									_qtyBox = Convert.ToDecimal(_aryHasBox[0]["QTY"]);
								}
								_aryHasBox[0]["QTY"] = _qtyBox + 1 ;
							}
							else
							{
								DataRow _drBox = _boxTable.NewRow();
								_drBox["PRD_NO"] = _prdNo;
								_drBox["CONTENT"] = _content;
								_drBox["QTY"] = 1;
								_boxTable.Rows.Add(_drBox);
							}
						}
					}

				}
				errorMessage = _sbError.ToString();
			}
			catch (Exception _ex)
			{
				errorMessage = _ex.Message.ToString();
			}
			
			_coalitionBarDS.Tables.Add(_boxTable);
			_coalitionBarDS.Tables.Add(_barTable);
			return _coalitionBarDS;
		}
		#endregion

		#region 整理单据序列号得出箱及件的数量（分仓库）
		/// <summary>
		/// 整理单据序列号得出箱及件的数量（分仓库）
		/// </summary>
		/// <param name="controlBarCode">序列号是否管制</param>
		/// <param name="barCollect">收集的序列号</param>	
		/// <param name="hasBarNo">考虑条码</param>		
		/// <returns></returns>
		public SunlikeDataSet CollectBarCodeWH(bool controlBarCode,DataTable barCollect,bool hasBarNo)
		{
			SunlikeDataSet _coalitionBarDS = new SunlikeDataSet();
			#region 表结构
			//序列号表
			DataTable _barTable = new DataTable("BARCODE");
			if (hasBarNo)
			{
				_barTable.Columns.Add("BAT_NO");
			}
			_barTable.Columns.Add("PRD_NO");
			_barTable.Columns.Add("PRD_MARK");
			_barTable.Columns.Add("WH");
			_barTable.Columns.Add("QTY");

			DataColumn[] _dcaBar = null;
			if (hasBarNo)
			{
				_dcaBar = new DataColumn[4];
				_dcaBar[0] = _barTable.Columns["BAT_NO"];
				_dcaBar[1] = _barTable.Columns["PRD_NO"];
				_dcaBar[2] = _barTable.Columns["PRD_MARK"];
				_dcaBar[3] = _barTable.Columns["WH"];

			}
			else
			{
				_dcaBar = new DataColumn[3];				
				_dcaBar[0] = _barTable.Columns["PRD_NO"];
				_dcaBar[1] = _barTable.Columns["PRD_MARK"];
				_dcaBar[2] = _barTable.Columns["WH"];
			}
			_barTable.PrimaryKey = _dcaBar;
			//箱条码表
			DataTable _boxTable = new DataTable("BOX");
			_boxTable.Columns.Add("PRD_NO");
			_boxTable.Columns.Add("WH");
			_boxTable.Columns.Add("CONTENT");
			_boxTable.Columns.Add("QTY");
			DataColumn[] _dcaBox = new DataColumn[3];
			_dcaBox[0] = _boxTable.Columns["PRD_NO"];
			_dcaBox[1] = _boxTable.Columns["WH"];
			_dcaBox[2] = _boxTable.Columns["CONTENT"];
			_boxTable.PrimaryKey = _dcaBox;
			#endregion
			bool controlBoxQty = false;
			if (Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "T")
				controlBoxQty = true;
			//把箱条码和序列号分开
			System.Text.StringBuilder _sbBox = new System.Text.StringBuilder();
			System.Text.StringBuilder _sbBar = new System.Text.StringBuilder();
			string _barCode;			
			try
			{
				#region 分开箱条码和序列号
				for (int i = 0 ; i < barCollect.Rows.Count;i++)
				{
					_barCode = barCollect.Rows[i]["BOX_NO"].ToString();
					if (String.IsNullOrEmpty(_barCode))
						_barCode = barCollect.Rows[i]["BAR_CODE"].ToString();
					//箱条码
					if (_barCode.Substring(BarRole.BoxPos,1) == BarRole.BoxFlag)
					{
						if (controlBoxQty)  //箱量控管
						{
							if (_sbBox.ToString().IndexOf(_barCode) < 0)
							{
								if (_sbBox.Length > 0)
								{
									_sbBox.Append(",");
								}
								_sbBox.Append(_barCode);
							}
						}
						else
						{							
							DataTable _barRecTable = GetBarRecord("BOX_NO='"+_barCode+"'",false);
							if (_barRecTable.Rows.Count > 0 )
							{
								for (int j = 0 ; j < _barRecTable.Rows.Count;j++)
								{
									if (_sbBar.Length > 0)
									{
										_sbBar.Append(",");
									}
									_sbBar.Append(_barRecTable.Rows[j]["BAR_NO"].ToString());
								}
							}

						}
					}
					else//序列号
					{
						if (_sbBar.Length > 0)
						{
							_sbBar.Append(",");
						}
						_sbBar.Append(_barCode);
					}
				}
				#endregion
				//查找序列号资料
				string _where = "";
				string _whereBox = "";
				#region 定义条件
				if (_sbBox.Length > 0)
				{
					_whereBox = "BOX_NO in ('" + _sbBox.ToString().Replace(",","','") + "')";
				}
				else
				{
					_whereBox = "1<>1";
				}
				if (_sbBar.Length > 0)
				{
					_where += "BAR_NO in ('" + _sbBar.ToString().Replace(",","','") + "')";
				}
				if (String.IsNullOrEmpty(_where.Trim()))
				{
					_where = "1<>1";
				}
				#endregion

				#region 把条件语句分段
				//把条件语句分段
				ArrayList _alBox = new ArrayList();
				ArrayList _alBar = new ArrayList();
				int _maxWhereLength = 1024;
				string _subWhere;
				int _pos;
				while (true)
				{
					if (_whereBox.Length > _maxWhereLength)
					{
						_subWhere = _whereBox.Substring(0,_maxWhereLength-1);
						_pos = _subWhere.LastIndexOf(",");
						_alBox.Add(_subWhere.Substring(0,_pos) + ")");
						_whereBox = "BOX_NO in (" + _whereBox.Substring(_pos+1,_whereBox.Length-_pos-1);
					}
					else
					{
						_alBox.Add(_whereBox);
						break;
					}
				}
				_maxWhereLength = 10240;
				while (true)
				{
					if (_where.Length > _maxWhereLength)
					{
						_subWhere = _where.Substring(0,_maxWhereLength-1);
						_pos = _subWhere.LastIndexOf(",");
						_alBar.Add(_subWhere.Substring(0,_pos) + ")");
						_where = "BAR_NO in (" + _where.Substring(_pos+1,_where.Length-_pos-1);
					}
					else
					{
						_alBar.Add(_where);
						break;
					}
				}
				#endregion

				//从资料库读取序列号资料
				//DataTable _dtBarBox = this.GetBoxData(_whereBox);
				//DataTable _dtBarRec = this.GetBarRecord(_where,false);
				//DataTable _dtBarRec1 = this.GetBarRecord(_whereBox,false);
				//定义错误表
				#region 定义错误表
				DataTable _dtBarError = null;//ChangedDS.Tables["BAR_ERROR"];
				DataRow _drBarError;	
				//DataTable _dtBarError = new DataTable();
				if (_dtBarError == null)
				{
					_dtBarError = new DataTable("BAR_ERROR");
					_dtBarError.Columns.Add("UNUSE");
					DataColumn _dc = new DataColumn("BAR_NO");
					_dtBarError.Columns.Add(_dc);
					_dc = new DataColumn("BOX_NO");
					_dtBarError.Columns.Add(_dc);
					_dc = new DataColumn("REM");
					_dtBarError.Columns.Add(_dc);
					_coalitionBarDS.Tables.Add(_dtBarError);
				}
				else
				{
					_dtBarError.Clear();
				}
				#endregion

				
				#region 从资料库读取序列号
				//从资料库读取序列号
				SunlikeDataSet _dsBar = new SunlikeDataSet();
				DataTable _dtBarBox = null;
				for (int i=0;i<_alBox.Count;i++)
				{
					_dtBarBox = this.GetBoxData(_alBox[i].ToString());
					if (_dsBar.Tables["BAR_BOX"] == null)
					{
						_dsBar.Tables.Add(_dtBarBox.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBarBox,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBarBox = _dsBar.Tables["BAR_BOX"];
				DataTable _dtBarRec;
				for (int i=0;i<_alBar.Count;i++)
				{
					_dtBarRec = this.GetBarRecord(_alBar[i].ToString(),false);
					if (_dsBar.Tables["BAR_REC"] == null)
					{
						_dsBar.Tables.Add(_dtBarRec.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBarRec,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBarRec = _dsBar.Tables["BAR_REC"];
				DataTable _dtBarRec1 = null;
				for (int i=0;i<_alBox.Count;i++)
				{
					_dtBarRec1 = this.GetBarRecord(_alBox[i].ToString(),false);
					_dtBarRec1.TableName = "BAR_REC1";
					if (_dsBar.Tables["BAR_REC1"] == null)
					{
						_dsBar.Tables.Add(_dtBarRec1.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBarRec1,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBarRec1 = _dsBar.Tables["BAR_REC1"];
				#endregion

				//检查箱条码是否存在
				if (_sbBox.Length > 0)
				{
					string[] _aryBoxList = _sbBox.ToString().Split(new char[] {','});
					for (int i = 0 ; i < _aryBoxList.Length;i++)
					{
						if (_dtBarRec1.Select("BOX_NO='" + _aryBoxList[i] + "'").Length == 0)
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["BOX_NO"] = _aryBoxList[i];
							_drBarError["REM"] = "RCID=INV.HINT.BOXNOEXISTS";//箱条码不存在！
							_dtBarError.Rows.Add(_drBarError);								
						}
					}
				}
				//检查序列号是否存在
				if (_sbBar.Length > 0)
				{
					string[] _aryBarList = _sbBar.ToString().Split(new char[] {','});
					for (int i=0;i<_aryBarList.Length;i++)
					{
						if (_dtBarRec.Select("BAR_NO='" + _aryBarList[i] + "'").Length == 0)
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["BAR_NO"] = _aryBarList[i];
                            _drBarError["REM"] = "RCID=INV.HINT.BARCODENOEXIST";//序列号不存在或已停用！
							_dtBarError.Rows.Add(_drBarError);															
						}
					}
				}
				//
				if (_dtBarError.Rows.Count == 0)
				{
					string _prdNo = "";
					string _prdMark  = "";
					string _wh = "";
					string _batNo = "";
					DataRow[] _aryDrCollect = null;
					#region 条码
					for (int i = 0 ; i < _dtBarRec.Rows.Count;i++)
					{
						_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
						//拆出货号、特征
						_prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
						_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
						_prdMark = "";
						if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
						{
							_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
						}
						_wh = _dtBarRec.Rows[i]["WH"].ToString();
						string _whNo = "";
						_aryDrCollect = barCollect.Select("BAR_CODE='" + _barCode + "'");
						if (_aryDrCollect != null && _aryDrCollect.Length > 0)
						{
							_whNo = _aryDrCollect[0]["WH1"].ToString();
						}
						//是否有强制库位
						if ( _whNo.Length > 0)
						{
							//是否管制
							if (controlBarCode)
							{
								if (_wh != _whNo)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BAR_NO"] =_barCode;							
									_drBarError["REM"] = "RCID=INV.HINT.CONTROLBARCOE,PARAM="+_barCode+",PARAM="+_wh+",PARAM="+_whNo;//序列号[{0}]的库位是[{1}],不在所选库位[{2}]，序列号已管制不允许输入
									_dtBarError.Rows.Add(_drBarError);	
									continue;
								}
							}
							else
							{
								_wh = _whNo;
							}
						}
						if (hasBarNo)
						{
							_batNo = _dtBarRec.Rows[i]["BAT_NO"].ToString();
						}
						DataRow _drBarSel = null;
						if (hasBarNo)
						{
							_drBarSel = _barTable.Rows.Find(new object[4]{_batNo,_prdNo,_prdMark,_wh});
						}
						else
						{
							_drBarSel = _barTable.Rows.Find(new object[3] {_prdNo,_prdMark,_wh});
						}

						if (_drBarSel == null)
						{
							DataRow _drBarNew = _barTable.NewRow();
							if (hasBarNo)
							{
								_drBarNew["BAT_NO"] = _batNo;
							}
							_drBarNew["PRD_NO"] = _prdNo;
							_drBarNew["PRD_MARK"] = _prdMark;
							_drBarNew["WH"] = _wh;
							_drBarNew["QTY"] = 1;
							_barTable.Rows.Add(_drBarNew);
						}
						else
						{
							_drBarSel["QTY"] = Convert.ToDecimal(_drBarSel["QTY"])+1;
						}
					}
					#endregion

					#region 箱条码
					for (int i = 0 ; i < _dtBarBox.Rows.Count;i++)
					{
						string _whNo = "";
						_aryDrCollect = barCollect.Select("BOX_NO='" + _dtBarBox.Rows[i]["BOX_NO"].ToString() + "'");
						if (_aryDrCollect != null && _aryDrCollect.Length > 0)
						{
							_whNo = _aryDrCollect[0]["WH1"].ToString();
						}
						//是否有强制库位
						if ( _whNo.Length > 0)
						{
							//是否管制
							if (controlBarCode)
							{
								if (_dtBarBox.Rows[i]["WH"].ToString() != _whNo)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BOX_NO"] = _dtBarBox.Rows[i]["BOX_NO"].ToString();							
									_drBarError["REM"] = "RCID=INV.HINT.CONTROLBARCOE,PARAM="+_dtBarBox.Rows[i]["BOX_NO"].ToString()+",PARAM="+_dtBarBox.Rows[i]["WH"].ToString()+",PARAM="+_whNo;//序列号[{0}]的库位是[{1}],不在所选库位[{2}]，序列号已管制不允许输入
									_dtBarError.Rows.Add(_drBarError);	
									continue;
								}
							}
							else
							{
								_wh = _whNo;
							}
						}
						DataRow _drBoxSel = _boxTable.Rows.Find(new object[3] {_dtBarBox.Rows[i]["PRD_NO"],_dtBarBox.Rows[i]["WH"],_dtBarBox.Rows[i]["CONTENT"]});
						if (_drBoxSel == null)
						{
							DataRow _drBoxNew = _boxTable.NewRow();
							_drBoxNew["PRD_NO"] = _dtBarBox.Rows[i]["PRD_NO"];
							_drBoxNew["WH"] = _dtBarBox.Rows[i]["WH"];
							_drBoxNew["CONTENT"] = _dtBarBox.Rows[i]["CONTENT"];
							_drBoxNew["QTY"] = 1;
							_boxTable.Rows.Add(_drBoxNew);
						}
						else
						{
							_drBoxSel["QTY"] = Convert.ToDecimal(_drBoxSel["QTY"])+1;
						}
					}
					#endregion

					#region 条码1
					if (!controlBoxQty)
					{
						
						for (int i = 0 ; i < _dtBarRec1.Rows.Count;i++)
						{
							_barCode = _dtBarRec1.Rows[i]["BAR_NO"].ToString();
							//拆出货号、特征
							_prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
							_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
							_prdMark = "";
							if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
							{
								_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
							}
							_wh = _dtBarRec1.Rows[i]["WH"].ToString();
							string _whNo = "";
							_aryDrCollect = barCollect.Select("BAR_CODE='" + _barCode + "'");
							if (_aryDrCollect != null && _aryDrCollect.Length > 0)
							{
								_whNo = _aryDrCollect[0]["WH1"].ToString();
							}
							//是否有强制库位
							if ( _whNo.Length > 0)
							{
								//是否管制
								if (controlBarCode)
								{
									if (_wh != _whNo)
									{
										_drBarError = _dtBarError.NewRow();
										_drBarError["BAR_NO"] =_barCode;							
										_drBarError["REM"] = "RCID=INV.HINT.CONTROLBARCOE,PARAM="+_barCode+",PARAM="+_wh+",PARAM="+_whNo;//序列号[{0}]的库位是[{1}],不在所选库位[{2}]，序列号已管制不允许输入
										_dtBarError.Rows.Add(_drBarError);	
										continue;
									}
								}
								else
								{
									_wh = _whNo;
								}
							}
							DataRow _drBarBoxSel = _barTable.Rows.Find(new object[3] {_prdNo,_prdMark,_wh});
							if (_drBarBoxSel == null)
							{
								DataRow _drBarBoxNew = _barTable.NewRow();
								_drBarBoxNew["PRD_NO"] = _prdNo;
								_drBarBoxNew["PRD_MARK"] = _prdMark;
								_drBarBoxNew["WH"] = _wh;
								_drBarBoxNew["QTY"] = 1;
								_barTable.Rows.Add(_drBarBoxNew);
							}
							else
							{
								_drBarBoxSel["QTY"] = Convert.ToDecimal(_drBarBoxSel["QTY"])+1;
							}
						}						
					}
					#endregion

					if (_dtBarError.Rows.Count > 0)
					{
						_coalitionBarDS.Merge(_dtBarError);
					}
					else
					{
						_coalitionBarDS.Merge(_barTable);
						_coalitionBarDS.Merge(_boxTable);
						_coalitionBarDS.Merge(_barTable);
					}
				}
				else
				{
					_coalitionBarDS.Merge(_dtBarError);
				}

			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _coalitionBarDS;
		}
		#endregion

		#region 根据葙码得到BAR_REC中的明细资料 by db
		/// <summary>
		/// 根据葙码得到BAR_REC中的明细资料 by db
		/// </summary>
		/// <param name="Box_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarRecByBoxNo(string Box_No)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			SunlikeDataSet _ds = _bar.GetBarRecByBoxNo(Box_No);
			return _ds;
		}
		#endregion

		#region 检测产品规格是否存在
		/// <summary>
		/// 检测产品规格是否存在
		/// </summary>
		/// <param name="SpcNo">规格</param>
		/// <returns></returns>
		public bool IsSpcExist(string SpcNo)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.IsSpcExist(SpcNo);
		}
		#endregion

		#region 判断序列号是否存在
		/// <summary>
		/// 判断序列号是否存在
		/// </summary>
		/// <param name="BarCode">条码</param>
		/// <returns></returns>
		public bool IsExistBarCode(string BarCode)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.IsExistBarCode(BarCode);
		}
		#endregion

        #region 更新序列号PH_FLAG
        
        /// <summary>
        /// 更新序列号PH_FLAG
        /// </summary>
        /// <param name="barCodes">序列号集合</param>
        /// <param name="cfmSw">true:T,false:F</param>
        public void UpdatePhFlag(string barCodes, bool cfmSw)
        {
            try
            {
                DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
                _dbBarCode.UpdatePhFlag(barCodes, cfmSw);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        #endregion
    }
}
/*
 * Bug tag 1:序列号管制，销货退回时输入一个停用的条码，库位打对了还是报“箱码库位不合法”
 * */