using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business
{
	/// <summary>
	/// 调价单
	/// </summary>
	public class DRPTJ : Sunlike.Business.BizObject
	{

		private DataTable _ToUpr4Dt;
		private DataTable _ToFXUpDt;
		private DataTable _ToFX_AreaUpDt;
		private string _chg_Up_Type = "";	//A:经销批发价 B:区域分销价 C:一般定价 D:特殊定价

		#region 构造函数
		/// <summary>
		/// 调价单
		/// </summary>
		public DRPTJ()
		{
		}
		#endregion

		#region 生成调价单
		/// <summary>
		/// 得到需调价的客户
		/// </summary>
		/// <param name="TtNo">批次调价单号</param>
		public string[] GetTjCusts(string TtNo)
		{
			Sunlike.Business.Prdt _prdt = new Prdt();
			DRPTT _drpTt = new DRPTT();

			SunlikeDataSet _ds = _drpTt.GetDRPTT(null,TtNo,false);
			DataTable _ttDtMF = _ds.Tables["MF_TT"];
			DataTable _ttDtTF = _ds.Tables["TF_TT"];
			DataTable _ttDtTF1 = _ds.Tables["TF_TT1"];

			string[] _aryCus_No = null;
			string _sel_ID = "";
			string _Area_No = "";

			if(_ttDtMF.Rows.Count > 0)
			{
				_sel_ID = _ttDtMF.Rows[0]["SEL_ID"].ToString();
				_Area_No = _ttDtMF.Rows[0]["AREA_NO"].ToString();

				#region 找客户代号
				DataTable _saleCustDt = null;
				DataTable _saleCustBatDt = null;
				StringBuilder _filterPrd_No = new StringBuilder();
				switch(_sel_ID)
				{
					case "A":		//全部客户
						foreach(DataRow _ttDrTF in _ttDtTF.Rows)
						{
							if(String.IsNullOrEmpty(_filterPrd_No.ToString()))
								_filterPrd_No.Append("'" + _ttDrTF["PRD_NO"].ToString() + "'");
							else
								_filterPrd_No.Append(",'" + _ttDrTF["PRD_NO"].ToString() + "'");
						}
						_saleCustDt = _prdt.GetCustBasePrdNo(_filterPrd_No.ToString());
						_saleCustBatDt = _prdt.GetCustBaseBatPrdNo(_filterPrd_No.ToString());
						if(_saleCustBatDt.Rows.Count > 0)
						{
							for(int i = 0;i < _saleCustBatDt.Rows.Count;i++)
							{
								if(_saleCustDt.Select("CUS_NO='"+_saleCustBatDt.Rows[i]["CUS_NO"].ToString()+"'").Length == 0)
								{
									DataRow _rowNew = _saleCustDt.NewRow();
									_rowNew["CUS_NO"] = _saleCustBatDt.Rows[i]["CUS_NO"];
									_saleCustDt.Rows.Add(_rowNew);
								}
							}
							_saleCustDt.AcceptChanges();
						}
						if(_saleCustDt.Rows.Count > 0)
						{
							_aryCus_No = new string[_saleCustDt.Rows.Count];
							for(int i=0;i<_aryCus_No.Length;i++)
							{
								_aryCus_No[i] = _saleCustDt.Rows[i]["CUS_NO"].ToString();
							}
						}
						break;
					case "B"://区域客户
						foreach(DataRow _ttDrTF in _ttDtTF.Rows)
						{
							if(String.IsNullOrEmpty(_filterPrd_No.ToString()))
								_filterPrd_No.Append("'" + _ttDrTF["PRD_NO"].ToString() + "'");
							else
								_filterPrd_No.Append(",'" + _ttDrTF["PRD_NO"].ToString() + "'");
						}
						_saleCustDt = _prdt.GetAreaCustBasePrdNo(_Area_No,_filterPrd_No.ToString());
                        _saleCustBatDt = _prdt.GetAreaCustBaseBatPrdNo(_Area_No, _filterPrd_No.ToString());
						if(_saleCustBatDt.Rows.Count > 0)
						{
							for(int i = 0;i < _saleCustBatDt.Rows.Count;i++)
							{
								if(_saleCustDt.Select("CUS_NO='"+_saleCustBatDt.Rows[i]["CUS_NO"].ToString()+"'").Length == 0)
								{
									DataRow _rowNew = _saleCustDt.NewRow();
									_rowNew["CUS_NO"] = _saleCustBatDt.Rows[i]["CUS_NO"];
									_saleCustDt.Rows.Add(_rowNew);
								}
							}
							_saleCustDt.AcceptChanges();
						}
						if(_saleCustDt.Rows.Count > 0)
						{
							_aryCus_No = new string[_saleCustDt.Rows.Count];
							for(int i=0;i<_aryCus_No.Length;i++)
							{
								_aryCus_No[i] = _saleCustDt.Rows[i]["CUS_NO"].ToString();
							}
						}
						break;
					case "C"://客户
						if(_ttDtTF1.Rows.Count > 0)
						{
							_aryCus_No = new string[_ttDtTF1.Rows.Count];
							for(int i=0;i<_aryCus_No.Length;i++)
							{
								_aryCus_No[i] = _ttDtTF1.Rows[i]["CUS_NO"].ToString();
							}
						}
						break;
				}
				#endregion
			}
			return _aryCus_No;
		}
		/// <summary>
		/// 生成客户调价单
		/// </summary>
		/// <param name="DrpTtDs"></param>
		/// <param name="CustNo"></param>
		/// <returns></returns>
		public DataTable CreateCustTj(SunlikeDataSet DrpTtDs,string CustNo)
		{
			DataTable _errorDt = null;
			string _errorStr = "";
			Sunlike.Business.Data.DbDRPTJ _dbDrptj = new DbDRPTJ(Comp.Conn_DB);
			Sunlike.Business.Upr4_def _upr4 = new Upr4_def();
			Sunlike.Business.SQNO _sqno = new SQNO();
			Sunlike.Business.Users _user = new Users();
			Sunlike.Business.Cust _cust = new Cust();
			Sunlike.Business.Prdt _prdt = new Prdt();
			try
			{
				#region 取批次调价单
				SunlikeDataSet _ttDs = null;				
				string _usr = "";
				string _sel_ID = "";
				string _Area_No = "";
				string _chk_Man = "";
				string _cls_DD = "";
				string tt_No = "";
				string _chg_Type = "";
				_ttDs = DrpTtDs;
				DataTable _ttDtMF = _ttDs.Tables["MF_TT"];
				DataTable _ttDtTF = _ttDs.Tables["TF_TT"];
				#endregion
				if(_ttDtMF.Rows.Count > 0)
				{
					foreach(DataRow _ttDrMF in _ttDtMF.Rows)
					{
						_usr = _ttDrMF["usr"].ToString();
						_sel_ID = _ttDrMF["SEL_ID"].ToString();
						_chg_Type = _ttDrMF["CHG_TYPE"].ToString();
						_Area_No = _ttDrMF["AREA_NO"].ToString();
						_chk_Man = _ttDrMF["CHK_MAN"].ToString();
						_cls_DD = _ttDrMF["CLS_DATE"].ToString();
						tt_No = _ttDrMF["TT_NO"].ToString();
						break;
					}
					#region 调价范围
					if(_chg_Type == "A" && _sel_ID == "A")
						_chg_Up_Type = "A";
					if(_chg_Type == "A" && _sel_ID == "B")
						_chg_Up_Type = "B";
					if(_chg_Type == "B" && _sel_ID == "C")
						_chg_Up_Type = "C";
					if(_chg_Type == "C" && _sel_ID == "C")
						_chg_Up_Type = "D";
					#endregion
					
					string _tj_No = "";
					string start_DD = "";
					string end_DD = "";
					string _dep = _user.GetUserDepNo(_usr);
					bool _isOk = false;					
					DataTable _prdQtyDt = null;
					DateTime _dateTime = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateFormat));
					SunlikeDataSet _ds = _dbDrptj.GetTJ_Comp();
					DataTable _dtM = _ds.Tables["MF_TJ"];
					DataTable _dtT = _ds.Tables["TF_TJ"];
					foreach(DataRow _drTT in _ttDtTF.Rows)
					{
						if(String.IsNullOrEmpty(start_DD))
						{
							start_DD = _drTT["S_DD"].ToString();
							_dateTime = Convert.ToDateTime((Convert.ToDateTime(start_DD)).ToString(Comp.SQLDateFormat));
						}
						if(String.IsNullOrEmpty(end_DD))
						{
							end_DD = _drTT["E_DD"].ToString();
						}
						break;
					}
					if(_chg_Up_Type == "A")
					{
						#region 修改经销批发价
						#region 取货品经销批发价
						#region 建临时表
						_ToFXUpDt = new DataTable("FXUP");
						_ToFXUpDt.Columns.Add("PRD_NO" , System.Type.GetType("System.String"));
						_ToFXUpDt.Columns.Add("OLD_UP" , System.Type.GetType("System.Decimal"));
						_ToFXUpDt.Columns.Add("UP" , System.Type.GetType("System.Decimal"));
						_ToFXUpDt.Columns.Add("USR" , System.Type.GetType("System.String"));
						#endregion
						bool _fxUpOk = false;
						foreach(DataRow _ttDrTF in _ttDtTF.Rows)
						{
							decimal _upNew = _prdt.GetPrdt_FX_UP(_ttDrTF["PRD_NO"].ToString());
							decimal _up = _upr4.GetHisPrice(_ttDrTF["PRD_NO"].ToString(),"",tt_No);
							if(_up >= 0)
							{
								DataRow _newDr = _ToFXUpDt.NewRow();
								_newDr["PRD_NO"] = _ttDrTF["PRD_NO"].ToString();
								_newDr["OLD_UP"] = _up;
								_newDr["UP"] = _upNew;
								_newDr["USR"] = _usr;
								_ToFXUpDt.Rows.Add(_newDr);
								_fxUpOk = true;
							}
							else
							{
								_fxUpOk = false;
								break;
							}
						}
						_ToFXUpDt.AcceptChanges();
						#endregion
						if(_fxUpOk)
						{
								
							_prdQtyDt = _cust.GetCust_WH_Prd_Qty(CustNo);
							string _sal_No = _cust.GetCustSal_no(CustNo);
							_tj_No = _sqno.Set("TJ" , _usr , _dep , _dateTime , "FX");
							#region MF_TJ
							DataRow _drM = _dtM.NewRow();
							_drM["TJ_NO"] = _tj_No;
							_drM["TJ_DD"] = start_DD;
							_drM["CUS_NO"] = CustNo;
							_drM["TT_NO"] = tt_No;
							_drM["SAL_NO"] = _sal_No;
							_drM["CHK_MAN"] = _chk_Man;
							_drM["CLS_DATE"] = _cls_DD;
							_drM["USR"] = _usr;
							_drM["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
							_drM["BIL_TYPE"] = "FX";
							_dtM.Rows.Add(_drM);
							#endregion
							#region TF_TJ
							int _itm = 1;
							foreach(DataRow _drTT in _ttDtTF.Rows)
							{
								bool _hasPrd_No = false;
								decimal _value = 1;
								string _v_Type = "0";
								string _prd_Mark = "";
								string _prd_No = _drTT["PRD_NO"].ToString();
								_prd_Mark = _drTT["PRD_MARK"].ToString();
								_v_Type = _drTT["V_TYPE"].ToString();
								_value = Convert.ToDecimal(_drTT["VALUE"].ToString());
								#region 查找产品信息
								foreach(DataRow _prdQtyDr in _prdQtyDt.Rows)
								{
									if(_prd_No == _prdQtyDr["PRD_NO"].ToString())
									{
										_hasPrd_No = true;
										break;
									}
								}
								#endregion
								if(_hasPrd_No)
								{
//									decimal _up = Convert.ToDecimal(_ToFXUpDt.Select("PRD_NO = '" + _prd_No + "'")[0]["OLD_UP"]);
//									decimal _upNew = Convert.ToDecimal(_ToFXUpDt.Select("PRD_NO = '" + _prd_No + "'")[0]["UP"]);

									#region 得到原价格和新价格
									//_value = Convert.ToDecimal(_drTT["VALUE"].ToString());
									//原价格
									decimal _up = _upr4.GetCustPrice(_prd_No,CustNo,Convert.ToDateTime(start_DD));
									decimal _upNew = Convert.ToDecimal(_ToFXUpDt.Select("PRD_NO = '" + _prd_No + "'")[0]["UP"]);
									string _vTypeCust = "";
									decimal _valueCust = 0;
									_upr4.GetCustPriceUprAndValue(_prd_No,_prd_Mark,CustNo,Comp.CompNo,Convert.ToDateTime(start_DD),out _vTypeCust,out _valueCust);
									if(!String.IsNullOrEmpty(_vTypeCust))
									{
										if(_vTypeCust == "0")//固定值
										{
											_upNew = _upNew + _valueCust;
										}
										if(_vTypeCust == "1")//比率值
										{
											_upNew = _upNew*_valueCust;
										}
									}
									#endregion

									decimal _qty = 0;
									#region 取货品库存量
									DataRow[] _prdQtyDrs = _prdQtyDt.Select("PRD_NO = '" + _prd_No + "'");
									foreach(DataRow _prdQtyDr in _prdQtyDrs)
									{
										_qty += Convert.ToDecimal(_prdQtyDr["QTY"]);												
									}
									#endregion
									if(_qty != 0)
									{
										DataRow _drT = _dtT.NewRow();
										_drT["TJ_NO"] = _tj_No;
										_drT["ITM"] = _itm;
										_drT["PRD_NO"] = _prd_No;
										_drT["PRD_MARK"] = _prd_Mark;
										_drT["UPR4_OLD"] = _up;
										_drT["UPR4"] = _upNew;
										_drT["QTY"] = _qty;
										//_drT["KEY_ITM"] = 1;
										_drT["AMTN_NET"] = (_upNew-_up) * _qty;
										_dtT.Rows.Add(_drT);
										_itm++;
									}
								}
							}
							if(_itm == 1)
							{
								DataRow _delRow = _dtM.Rows.Find(new object[1]{_tj_No});
								if(_delRow != null)
								{
									_delRow.Delete();
								}
							}
							#endregion
							_isOk = true;
						}
						else
						{
							_isOk = false;
							_errorStr += "RCID=INV.HINT.FXUPISNULL";
						}
						#endregion					
					}
					else if(_chg_Up_Type == "B")
					{
						#region 修改区域分销价
						#region 取区域分销价
						#region 建临时表
						_ToFX_AreaUpDt = new DataTable("FX_AREAUP");
						_ToFX_AreaUpDt.Columns.Add("PRD_NO" , System.Type.GetType("System.String"));
						_ToFX_AreaUpDt.Columns.Add("OLD_UP" , System.Type.GetType("System.Decimal"));
						_ToFX_AreaUpDt.Columns.Add("UP" , System.Type.GetType("System.Decimal"));
						_ToFX_AreaUpDt.Columns.Add("USR" , System.Type.GetType("System.String"));
						_ToFX_AreaUpDt.Columns.Add("AREA" , System.Type.GetType("System.String"));
						#endregion
						bool _fxUpOk = false;
						foreach(DataRow _ttDrTF in _ttDtTF.Rows)
						{
							decimal _upNew = _prdt.GetPrdt_Area_FX_UP(_ttDrTF["PRD_NO"].ToString() , _Area_No);
							decimal _up = _upr4.GetHisPrice(_ttDrTF["PRD_NO"].ToString(),_Area_No,tt_No);
							if(_up >= 0)
							{
								DataRow _newDr = _ToFX_AreaUpDt.NewRow();
								_newDr["PRD_NO"] = _ttDrTF["PRD_NO"].ToString();
								_newDr["OLD_UP"] = _up;
								_newDr["UP"] = _upNew;
								_newDr["USR"] = _usr;
								_newDr["AREA"] = _Area_No;
								_ToFX_AreaUpDt.Rows.Add(_newDr);
								_fxUpOk = true;
							}
							else
							{
								_fxUpOk = false;
								break;
							}
						}
						_ToFX_AreaUpDt.AcceptChanges();
						#endregion
						if(_fxUpOk)
						{
							_prdQtyDt = _cust.GetCust_WH_Prd_Qty(CustNo);
							string _sal_No = _cust.GetCustSal_no(CustNo);
							_tj_No = _sqno.Set("TJ" , _usr , _dep , _dateTime , "FX");
							#region MF_TJ
							DataRow _drM = _dtM.NewRow();
							_drM["TJ_NO"] = _tj_No;
							_drM["TJ_DD"] = start_DD;
							_drM["CUS_NO"] = CustNo;
							_drM["TT_NO"] = tt_No;
							_drM["SAL_NO"] = _sal_No;
							_drM["CHK_MAN"] = _chk_Man;
							_drM["CLS_DATE"] = _cls_DD;
							_drM["USR"] = _usr;
							_drM["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
							_drM["BIL_TYPE"] = "FX";
							_dtM.Rows.Add(_drM);
							#endregion
							#region TF_TJ
							int _itm = 1;
							foreach(DataRow _drTT in _ttDtTF.Rows)
							{
								bool _hasPrd_No = false;
								decimal _value = 1;
								string _v_Type = "0";
								string _prd_Mark = "";
								string _prd_No = _drTT["PRD_NO"].ToString();
								_prd_Mark = _drTT["PRD_MARK"].ToString();
								_v_Type = _drTT["V_TYPE"].ToString();
								_value = Convert.ToDecimal(_drTT["VALUE"].ToString());
								#region 查找产品信息
								foreach(DataRow _prdQtyDr in _prdQtyDt.Rows)
								{
									if(_prd_No == _prdQtyDr["PRD_NO"].ToString())
									{
										_hasPrd_No = true;
										break;
									}
								}
								#endregion
								if(_hasPrd_No)
								{
//									decimal _up = Convert.ToDecimal(_ToFX_AreaUpDt.Select("PRD_NO = '" + _prd_No + "'")[0]["OLD_UP"]);
//									decimal _upNew = Convert.ToDecimal(_ToFX_AreaUpDt.Select("PRD_NO = '" + _prd_No + "'")[0]["UP"]);

									#region 得到原价格和新价格
									//_value = Convert.ToDecimal(_drTT["VALUE"].ToString());
									//原价格
									decimal _up = _upr4.GetCustPrice(_prd_No,CustNo,(Convert.ToDateTime(start_DD)));
									decimal _upNew = Convert.ToDecimal(_ToFX_AreaUpDt.Select("PRD_NO = '" + _prd_No + "'")[0]["UP"]);
									string _vTypeCust = "";
									decimal _valueCust = 0;
									_upr4.GetCustPriceUprAndValue(_prd_No,_prd_Mark,CustNo,Comp.CompNo,Convert.ToDateTime(start_DD),out _vTypeCust,out _valueCust);
									if(!String.IsNullOrEmpty(_vTypeCust))
									{
										if(_vTypeCust == "0")//固定值
										{
											_upNew = _upNew + _valueCust;
										}
										if(_vTypeCust == "1")//比率值
										{
											_upNew = _upNew*_valueCust;
										}
									}
									#endregion

									decimal _qty = 0;
									#region 取货品库存量
									DataRow[] _prdQtyDrs = _prdQtyDt.Select("PRD_NO = '" + _prd_No + "'");
									foreach(DataRow _prdQtyDr in _prdQtyDrs)
									{
										_qty += Convert.ToDecimal(_prdQtyDr["QTY"]);													
									}
									#endregion
									if(_qty != 0)
									{
										DataRow _drT = _dtT.NewRow();
										_drT["TJ_NO"] = _tj_No;
										_drT["ITM"] = _itm;
										_drT["PRD_NO"] = _prd_No;
										_drT["PRD_MARK"] = _prd_Mark;
										_drT["UPR4_OLD"] = _up;
										_drT["UPR4"] = _upNew;
										_drT["QTY"] = _qty;
										//_drT["KEY_ITM"] = 1;
										_drT["AMTN_NET"] = Convert.ToDecimal(_upNew-_up) * _qty;										
										_dtT.Rows.Add(_drT);
										_itm++;
									}
								}
							}
							if(_itm == 1)
							{
								DataRow _delRow = _dtM.Rows.Find(new object[1]{_tj_No});
								if(_delRow != null)
								{
									_delRow.Delete();
								}
							}
							#endregion
							_isOk = true;
						}
						else
						{
							_isOk = false;
							_errorStr += "RCID=INV.HINT.FXAREAUPISNULL";
						}
						#endregion
					}
					else
					{
						#region 一般定价和特殊定价
						if(_chg_Up_Type == "C" || _chg_Up_Type == "D")
						{
							_isOk = true;

							#region 创建临时表
							//-------------------用于新增一般/特殊定价政策---------------------------
							_ToUpr4Dt = new DataTable();
							_ToUpr4Dt.Columns.Add("CUS_NO" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("PRD_NO" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("PRD_MARK" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("START_DD" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("END_DD" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("USR" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("CHK_MAN" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("CLS_DD" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("V_TYPE" , System.Type.GetType("System.String"));
							_ToUpr4Dt.Columns.Add("VALUE" , System.Type.GetType("System.Decimal"));
							//-----------------------------------------------------------------------
							#endregion
							#region 判断客户是否具有产品
							bool _custHasPrdt = false;
							_prdQtyDt = _cust.GetCust_WH_Prd_Qty(CustNo);
							//DataRow[] _prdQtyDrs = _prdQtyDt.Select("CUS_NO = '"+ CustNo +"'");
							if(_prdQtyDt.Rows.Count > 0)
							{
								foreach(DataRow _drTT in _ttDtTF.Rows)
								{
									string _prd_No = _drTT["PRD_NO"].ToString();
									string _prd_Mark = _drTT["PRD_MARK"].ToString();
									if(String.IsNullOrEmpty(start_DD))
										start_DD = _drTT["S_DD"].ToString();
									if(String.IsNullOrEmpty(end_DD))
										end_DD = _drTT["E_DD"].ToString();
									foreach(DataRow _prdQtyDr in _prdQtyDt.Rows)
									{
										if(_prd_No == _prdQtyDr["PRD_NO"].ToString() && _prd_Mark == _prdQtyDr["PRD_MARK"].ToString())
										{
											_custHasPrdt = true;
											break;
										}
									}
									if(_custHasPrdt)
									{										
										break;
									}
								}
							}
							else
							{
								_custHasPrdt = false;
							}
							#endregion
							if(_custHasPrdt)
							{
								string _sal_No = _cust.GetCustSal_no(CustNo);
								_tj_No = _sqno.Set("TJ" , _usr , _dep , _dateTime , "FX");
								#region MF_TJ
								DataRow _drM = _dtM.NewRow();
								_drM["TJ_NO"] = _tj_No;
								_drM["TJ_DD"] = start_DD;
								_drM["CUS_NO"] = CustNo;
								_drM["TT_NO"] = tt_No;
								_drM["SAL_NO"] = _sal_No;
								_drM["CHK_MAN"] = _chk_Man;
								_drM["CLS_DATE"] = _cls_DD;
								_drM["USR"] = _usr;
								_drM["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
								_drM["BIL_TYPE"] = "FX";
								_dtM.Rows.Add(_drM);
								#endregion
								#region TF_TJ
								int _itm = 1;
								foreach(DataRow _drTT in _ttDtTF.Rows)
								{
									bool _hasPrd_No = false;
									decimal _value = 1;
									string _v_Type = "0";
									string _prd_Mark = "";
									string _prd_No = _drTT["PRD_NO"].ToString();
									_prd_Mark = _drTT["PRD_MARK"].ToString();
									_v_Type = _drTT["V_TYPE"].ToString();
									_value = Convert.ToDecimal(_drTT["VALUE"].ToString());
									#region 查找产品信息
									foreach(DataRow _prdQtyDr in _prdQtyDt.Rows)
									{
										if(_prd_No == _prdQtyDr["PRD_NO"].ToString() && _prd_Mark == _prdQtyDr["PRD_MARK"].ToString())
										{
											_hasPrd_No = true;
											break;
										}
									}
									#endregion
									if(_hasPrd_No)
									{
										decimal _up = _upr4.GetCustPrice(_prd_No , _prd_Mark , CustNo , Convert.ToDateTime(start_DD));
										decimal _qty = 0;
										#region 取货品库存量
										DataRow[] _prdQtyDrs = _prdQtyDt.Select("PRD_NO = '" + _prd_No + "' AND PRD_MARK = '" + _prd_Mark + "'");
										foreach(DataRow _prdQtyDr in _prdQtyDrs)
										{
											_qty = Convert.ToDecimal(_prdQtyDr["QTY"]);
											break;
										}
										#endregion
										if(_qty != 0)
										{
											DataRow _drT = _dtT.NewRow();
											_drT["TJ_NO"] = _tj_No;
											_drT["ITM"] = _itm;
											_drT["PRD_NO"] = _prd_No;
											_drT["PRD_MARK"] = _prd_Mark;
											_drT["UPR4_OLD"] = _up;
											//_drT["KEY_ITM"] = 1;
											if(_v_Type == "0")
											{
												_drT["UPR4"] = Convert.ToDecimal(_up + _value);
											}
											else
											{
												_drT["UPR4"] = Convert.ToDecimal(_up + (_up * _value));
											}
											_drT["QTY"] = _qty;
											if(_v_Type == "0")
											{
												_drT["AMTN_NET"] = Convert.ToDecimal(_value) * _qty;
											}
											else
											{
												_drT["AMTN_NET"] = Convert.ToDecimal(_up * _value) * _qty;
											}
											_dtT.Rows.Add(_drT);
											_itm++;
											#region 增加内容
											DataRow _ToUpr4Dr = _ToUpr4Dt.NewRow();
											_ToUpr4Dr["CUS_NO"] = CustNo;
											_ToUpr4Dr["PRD_NO"] = _prd_No;
											_ToUpr4Dr["PRD_MARK"] = _prd_Mark;
											_ToUpr4Dr["START_DD"] = start_DD;
											if(!String.IsNullOrEmpty(end_DD))
												_ToUpr4Dr["END_DD"] = end_DD;
											else
												_ToUpr4Dr["END_DD"] = "";
											_ToUpr4Dr["USR"] = _usr;
											_ToUpr4Dr["V_TYPE"] = _v_Type;
											if(_v_Type == "0")
												_ToUpr4Dr["VALUE"] = Convert.ToDecimal(_value);
											else
											{
												if(_value > 0)
													_ToUpr4Dr["VALUE"] = Convert.ToDecimal(1 + _value);
												else
													_ToUpr4Dr["VALUE"] = System.Math.Abs(Convert.ToDecimal(1 + _value));
											}
											_ToUpr4Dr["CHK_MAN"] = _chk_Man;
											_ToUpr4Dr["CLS_DD"] = _cls_DD;
											_ToUpr4Dt.Rows.Add(_ToUpr4Dr);
											#endregion
										}
									}
								}
								if(_itm == 1)
								{
									DataRow _delRow = _dtM.Rows.Find(new object[1]{_tj_No});
									if(_delRow != null)
									{
										_delRow.Delete();
									}
								}
								#endregion
							}
							_ToUpr4Dt.AcceptChanges();							
						}
						#endregion
					}
					if(_isOk)
					{
						#region 产生调价单
						try
						{
							this.EnterTransaction();

							#region 产生客户调价单
							if(_ds.Tables[0].Rows.Count > 0)
							{
								this.ServerUpdateData(_ds);
								_errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
							}
							#endregion
						}
						catch(Exception _ex)
						{
							this.SetAbort();
							throw new Exception(_ex.Message);
						}
						finally
						{
							this.LeaveTransaction();
						}
						#endregion
					}
					else
					{
						throw new SunlikeException(_errorStr);
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return _errorDt;
		}
		/// <summary>
		/// 更新调价单
		/// </summary>
		/// <param name="Ds"></param>
		/// <returns></returns>
		public DataTable UpdateData(SunlikeDataSet Ds)
		{
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
			_ht["MF_TJ"] = "TJ_NO,TJ_DD,CUS_NO,TT_NO,SAL_NO,PRT_SW,AUD_FLAG,LZ_CLS_ID,BIL_TYPE,SYS_DATE,USR,DEP,CHK_MAN,CLS_DATE,REM,TURN_ID,AMT_CLS,AMTN_NET_CLS,TAX_CLS,QTY_CLS";
			_ht["TF_TJ"] = "TJ_NO,ITM,PRD_NO,PRD_MARK,UPR4_OLD,UPR4,REM,KEY_ITM,AMTN_NET,QTY,AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP";
			this.UpdateDataSet(Ds,_ht);
			DataTable _dtErr = GetAllErrors(Ds);
			return _dtErr;
		}
		/// <summary>
		/// 更新调价单
		/// </summary>
		/// <param name="Ds"></param>
		/// <returns></returns>
		public DataTable ServerUpdateData(SunlikeDataSet Ds)
		{
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
			_ht["MF_TJ"] = "TJ_NO,TJ_DD,CUS_NO,TT_NO,SAL_NO,SYS_DATE,USR,CHK_MAN,CLS_DATE,BIL_TYPE";
			_ht["TF_TJ"] = "TJ_NO,ITM,PRD_NO,PRD_MARK,UPR4_OLD,UPR4,KEY_ITM,QTY,AMTN_NET";
			this.UpdateDataSet(Ds,_ht);
			DataTable _dtErr = GetAllErrors(Ds);
			return _dtErr;
		}
		#endregion

		#region 删除调价单
		/// <summary>
		/// 删除调价单
		/// </summary>
		/// <param name="tt_No">批次调价单号</param>
		public void UpdateMfTt(string tt_No)
		{
			try
			{
				Sunlike.Business.Data.DbDRPTJ _dbDrptj = new DbDRPTJ(Comp.Conn_DB);
				_dbDrptj.UpdateMfTt(tt_No);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region 取调价单内容
		/// <summary>
		/// 取调价单内容(WinForm)
		/// </summary>		
		/// <param name="TjNo"></param>
		/// <param name="OnlyFillSchema">是否只取Schema。Ture:是;False:否</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string TjNo,bool OnlyFillSchema)
		{
			DbDRPTJ _dbDrptj = new DbDRPTJ(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrptj.GetData(TjNo, OnlyFillSchema);
            _ds.Tables["TF_TJ"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_TJ"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_TJ"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_TJ"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_TJ"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_TJ"].Columns["KEY_ITM"].Unique = true;
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
			DbDRPTJ _dbDrptj = new DbDRPTJ(Comp.Conn_DB);
			SunlikeDataSet _ds = _dbDrptj.GetData(TjNo,Chk);
			return _ds;
		}
		/// <summary>
		/// 取调价单内容
		/// </summary>
		/// <param name="tt_No"></param>
		/// <returns></returns>
		public DataTable GetTJFromTT(string tt_No)
		{
			DataTable _dt = null;
			try
			{
				Sunlike.Business.Data.DbDRPTJ _dbDrptj = new DbDRPTJ(Comp.Conn_DB);
				_dt = _dbDrptj.GetTJFromTT(tt_No);
			}
			catch(Exception ex)
			{
				throw ex;
			}
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
			try
			{
				Sunlike.Business.Data.DbDRPTJ _dbDrptj = new DbDRPTJ(Comp.Conn_DB);
				_dt = _dbDrptj.GetMF_TJ(tt_No);
			}
			catch(Exception ex)
			{
				throw new SunlikeException(ex.Message, ex);
			}
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
			Sunlike.Business.Data.DbDRPTJ _dbDrptj = new DbDRPTJ(Comp.Conn_DB);
			return _dbDrptj.GetAud_FlagForTT(tt_No);
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
			if(tableName == "MF_TJ" && statementType == StatementType.Insert)
			{
				//更新MF_TT中的TJ_CUS字段
				string _sql = "update mf_tt set tj_cus='"+dr["CUS_NO"].ToString()+"' where tt_no='"+dr["TT_NO"].ToString()+"'";
				Query _query = new Query();
				_query.RunSql(_sql);
				//更新信用额度
//				Arp _arp = new Arp();
//				Cust _cust =new Cust();
//				string _cusNo = dr["CUS_NO"].ToString();
//				DateTime _date = Convert.ToDateTime(dr["TJ_DD"]);
//				if (_cust.IsDrp_id(_cusNo))
//				{
//					decimal _amtn = 0;
//					for(int i = 0; i < dr.Table.DataSet.Tables["TF_TJ"].Rows.Count;i++)
//					{
//						if(dr.Table.DataSet.Tables["TF_TJ"].Rows[i]["AMTN_NET"] != System.DBNull.Value)
//						{
//							_amtn += Convert.ToDecimal(dr.Table.DataSet.Tables["TF_TJ"].Rows[i]["AMTN_NET"]);
//						}
//					}
//					_arp.UpdateSarp("1",_date.Year,_cusNo,_date.Month,"","AMTN_INV",_amtn);
//				}
			}			
		}
		#endregion

        #region BeforeDsSave
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dtMf = ds.Tables["MF_TJ"];
            //if (_dtMf.Rows.Count > 0)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "TJ");
            //}
            //#endregion
        }
        #endregion

        #region 得到重算历史记录
        /// <summary>
		/// 得到重算历史记录
		/// </summary>
		/// <param name="PssNo">销货单号</param>
		/// <returns></returns>
		public DataTable GetTjHistory(string PssNo)
		{
			Sunlike.Business.Data.DbDRPTJ _dbTj = new DbDRPTJ(Comp.Conn_DB);
			return _dbTj.GetTjHistory(PssNo);
		}
		#endregion

		#region 根据批次调价单进行调价作业，但不生成调价单
		/// <summary>
		/// 根据批次调价单进行调价作业，但不生成调价单
		/// </summary>
		/// <param name="ChangedDS">批次调价单数据集</param>
		public void UpdatePrice(SunlikeDataSet ChangedDS)
		{
			string _err = String.Empty;
			Sunlike.Business.Prdt _prdt = new Prdt();
			Sunlike.Business.Upr4_def _upr4 = new Upr4_def();

			//取出ChangedDS中的批次调价单
			DataTable _dtMt = ChangedDS.Tables["MF_TT"];
			DataTable _dtTt = ChangedDS.Tables["TF_TT"];
			DataTable _dtTt1 = ChangedDS.Tables["TF_TT1"];

			//调价范围 A:经销批发价 B:区域分销价 C:一般定价 D:特殊定价
			string _tjRange = String.Empty;
			string _ttNo = _dtMt.Rows[0]["TT_NO"].ToString();//批次调价单号
			string _areaNo = _dtMt.Rows[0]["AREA_NO"].ToString();//区域代号
			string _usr = _dtMt.Rows[0]["USR"].ToString();//制单人
			string _selId = _dtMt.Rows[0]["SEL_ID"].ToString();//价格影响范围
			string _chgType = _dtMt.Rows[0]["CHG_TYPE"].ToString();//调价范围
			string _chkMan = _dtMt.Rows[0]["CHK_MAN"].ToString();//审核人
			string _clsDate = _dtMt.Rows[0]["CLS_DATE"].ToString();//审核日期

			#region 调价范围
			if(_chgType == "A" && _selId == "A")
			{
				_tjRange = "A";
			}
			if(_chgType == "A" && _selId == "B")
			{
				_tjRange = "B";
			}
			if(_chgType == "B" && _selId == "C")
			{
				_tjRange = "C";
			}
			if(_chgType == "C" && _selId == "C")
			{
				_tjRange = "D";
			}
			#endregion

			for(int i = 0;i < _dtTt.Rows.Count;i++)
			{
				string _prdNo = _dtTt.Rows[i]["PRD_NO"].ToString();
				string _prdMark = _dtTt.Rows[i]["PRD_MARK"].ToString();
				string _sDD = _dtTt.Rows[i]["S_DD"].ToString();
				string _eDD = _dtTt.Rows[i]["E_DD"].ToString();
				string _vType = _dtTt.Rows[i]["V_TYPE"].ToString();
				decimal _value = Convert.ToDecimal(_dtTt.Rows[i]["VALUE"]);
				decimal _oldPrice = 0;
				decimal _newPrice = 0;
				switch (_tjRange)
				{
					#region 更新价格
					case "A":
						_oldPrice = _prdt.GetPrdt_FX_UP(_prdNo);
						if(_vType == "0")
						{
							_newPrice = Convert.ToDecimal(_oldPrice + _value);
						}
						else
						{
							_newPrice = Convert.ToDecimal(_oldPrice + (_oldPrice * _value/100));
						}
						//更新经销批发价
						_err = _prdt.UpdateUpr(_prdNo,_usr,_newPrice,Convert.ToDateTime(_sDD),_ttNo);
						break;
					case "B":
						_oldPrice = _prdt.GetPrdt_Area_FX_UP(_prdNo,_areaNo);
						if(_vType == "0")
						{
							_newPrice = Convert.ToDecimal(_oldPrice + _value);
						}
						else
						{
							_newPrice = Convert.ToDecimal(_oldPrice + (_oldPrice * _value/100));
						}
						//更新区域分销价
						_err = _prdt.UpdateUpr(_prdNo,_usr,_areaNo,_newPrice,Convert.ToDateTime(_sDD),_ttNo);
						break;
					case "C":
						for(int j = 0;j < _dtTt1.Rows.Count;j++)
						{
							Sunlike.Business.DV_type _dvType;
							if(_vType == "0")
							{
								_dvType = Sunlike.Business.DV_type.fix;
							}
							else
							{
								_dvType = Sunlike.Business.DV_type.percent;
							}
							//更新一般定价
							_err = _upr4.UpdateUpr4_def(_prdNo,"",_prdMark,_dtTt1.Rows[j]["CUS_NO"].ToString(),"",Convert.ToDateTime(_sDD),_eDD,_chkMan,_clsDate,Sunlike.Business.DType.type,_dvType,_value,_usr,_ttNo);
						}
						break;
					case "D":
						for(int j = 0;j < _dtTt1.Rows.Count;j++)
						{
							Sunlike.Business.DV_type _dvType;
							if(_vType == "0")
							{
								_dvType = Sunlike.Business.DV_type.fix;
							}
							else
							{
								_dvType = Sunlike.Business.DV_type.percent;
							}
							//更新特殊定价
							_err = _upr4.UpdateUpr4_def1(_prdNo,"",_prdMark,_dtTt1.Rows[j]["CUS_NO"].ToString(),"",Convert.ToDateTime(_sDD),_eDD,_chkMan,_clsDate,Sunlike.Business.DType.type,_dvType,_value,_usr,_ttNo);
						}
						break;
					#endregion
				}
				if(!String.IsNullOrEmpty(_err))
				{
					throw new SunlikeException(_err);
				}
			}
		}
		#endregion

		#region 通过服务生成调价单
		/// <summary>
		/// 通过服务生成调价单
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet CreateBatchCustTjBill()
		{
			Event_Log _log = new Event_Log();
			SunlikeDataSet _dsErr = new SunlikeDataSet();
			DataTable _dtErr = new DataTable();			
			bool _vic1 = true;
			string _cusNo = String.Empty;
			DRPTT _drptt = new DRPTT();
			Query _query = new Query();
			string _sql = "select distinct A.TT_NO from MF_TT A,TF_TT B "
				+" where A.TT_NO=B.TT_NO and ISNULL(A.CHK_MAN,'')<>'' and ISNULL(A.MANU_TJ,'')='T' "
				+" and year(getdate())=year(B.S_DD) and month(getdate())=month(B.S_DD) and day(getdate())=day(B.S_DD)";
			SunlikeDataSet _ds = _query.DoSQLString(_sql);
			DataTable _dt = _ds.Tables[0];
			if(_dt.Rows.Count > 0)
			{
				for(int i = 0;i < _dt.Rows.Count;i++)
				{
					SunlikeDataSet _ttDs = _drptt.GetDRPTT(null,_dt.Rows[i][0].ToString(),false);
					try
					{
						string[] _cusAry = this.GetTjCusts(_dt.Rows[i][0].ToString());
						if(_cusAry != null)
						{
							if(_cusAry.Length > 0)
							{
								int _sta = 0;
								if(!String.IsNullOrEmpty(_cusNo))
								{
									for(int j =0;j < _cusAry.Length;j++)
									{
										if(_cusNo == _cusAry[j])
										{
											_sta = j;
											break;
										}
									}
								}
								for(int j = _sta;j < _cusAry.Length;j++)
								{
									_dtErr = this.CreateCustTj(_ttDs,_cusAry[j]);
									if(_dtErr != null)
									{
										if(_dtErr.Rows.Count > 0)
										{
											//错误信息处理
                                            if (Comp.DataBaseLanguage == "en-us")
                                            {
                                                _log.InsertData("BATCH_TJ", "批次调价作业自动调价", "1", "客户" + _cusAry[j] + "在批次调价单" + _dt.Rows[i][0].ToString() + "调价失败,请检查:" + _dt.Rows[i]["REM"].ToString());
                                            }
                                            else if (Comp.DataBaseLanguage == "zh-tw")
                                            {
                                                _log.InsertData("BATCH_TJ", "批次調價作業自動調價", "1", "客戶" + _cusAry[j] + "在批次調價單" + _dt.Rows[i][0].ToString() + "調價失敗,請檢查:" + _dt.Rows[i]["REM"].ToString());
                                            }
                                            else
                                            {
                                                _log.InsertData("BATCH_TJ", "批次调价作业自动调价", "1", "客户" + _cusAry[j] + "在批次调价单" + _dt.Rows[i][0].ToString() + "调价失败,请检查:" + _dt.Rows[i]["REM"].ToString());
                                            }
											_vic1 = false;
											break;
										}
										else
										{
											//成功信息处理
											if (Comp.DataBaseLanguage == "en-us")
											{
												_log.InsertData("BATCH_TJ","批次调价作业自动调价","0","客户"+_cusAry[j]+"在批次调价单"+_dt.Rows[i][0].ToString()+"调价成功");
											}
											else if(Comp.DataBaseLanguage == "zh-tw")
											{
												_log.InsertData("BATCH_TJ","批次調價作業自動調價","0","客戶"+_cusAry[j]+"在批次調價單"+_dt.Rows[i][0].ToString()+"調價成功");
											}
											else
											{
												_log.InsertData("BATCH_TJ","批次调价作业自动调价","0","客户"+_cusAry[j]+"在批次调价单"+_dt.Rows[i][0].ToString()+"调价成功");
											}
										}
									}
								}
								if(_vic1)
								{
									_drptt.SetTjState(_dt.Rows[i][0].ToString(),"F");							
								}
								else
								{
									continue;
								}
							}
						}
					}
					catch(Exception _ex)
					{
						throw new Exception(_ex.Message);
					}
				}
			}
			_dsErr.Tables.Add(_dtErr);
			return _dsErr;
		}
		#endregion
	}
}
