/*
 * modify: cjc 090805 
 * 审核流程的改动(单据别和审核模板)
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for DRPIO.
	/// </summary>
	public class DRPIO : BizObject , IAuditing, ICloseBill
    {
        #region Variable
        /// <summary>
        /// 是否重新切制凭证
        /// </summary>
        private bool _reBuildVohNo = false;

        private bool _isRunAuditing;
		private bool _auditBarCode;
        private bool m_UpdateWhByCfmSw;
        private string m_CfmSwOrg;
        private Dictionary<string, object> m_UpdateList = new Dictionary<string, object>();
		private int _bodyNo ;
		private int _barCodeNo ;
		private System.Collections.ArrayList _alOsNo = new System.Collections.ArrayList();
		private System.Collections.ArrayList _alOsItm = new System.Collections.ArrayList();
		private System.Collections.ArrayList _alPrdNo = new System.Collections.ArrayList();
		private System.Collections.ArrayList _alUnit = new System.Collections.ArrayList();
		private System.Collections.ArrayList _alQty = new System.Collections.ArrayList();

        #endregion

        #region Constructor

        /// <summary>
		/// 配送单
		/// </summary>
		public DRPIO()
		{
        }

        #endregion

        #region GetData

        /// <summary>
		/// 取得单据资料
		/// </summary>
		/// <param name="usr">当前操作用户</param>
		/// <param name="IcID">调拨类别（IO：配送单 IB：配送退回 IC：调拨单 IM：DRP调货单）</param>
		/// <param name="IcNo">配送单号</param>
		/// <param name="OnlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string usr, string IcID,string IcNo,bool OnlyFillSchema)
		{
			return this.GetData(usr, IcID, IcNo, "", OnlyFillSchema);
		}
		/// <summary>
		/// 取得单据资料
		/// </summary>
		/// <param name="usr">当前操作用户</param>
		/// <param name="IcID">调拨类别（IO：配送单 IB：配送退回 IC：调拨单 IM：DRP调货单）</param>
		/// <param name="IcNo">配送单号</param>
		/// <param name="YhNo">配送退回的退回申请单号</param>
		/// <param name="OnlyFillSchema">是否只读取Schema</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string usr, string IcID,string IcNo,string YhNo,bool OnlyFillSchema)
		{
			DbDRPIO _io = new DbDRPIO(Comp.Conn_DB);
			SunlikeDataSet _ds = _io.GetData(IcID,IcNo,YhNo,OnlyFillSchema);
			if (usr != null && !String.IsNullOrEmpty(usr))
			{
				Users _users = new Users();
				_ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
			}
			_ds.Tables["TF_IC"].Columns.Add(new DataColumn("QTY_SO_ORG", typeof(System.Decimal)));
			_ds.Tables["TF_IC_BOX"].Columns.Add(new DataColumn("QTY_SO_ORG", typeof(System.Decimal)));
			DrpSO _drpSo = new DrpSO();
			foreach (DataRow dr in _ds.Tables["TF_IC"].Rows)
			{
				string _billId = dr["BIL_ID"].ToString();
				string _billNo = dr["BIL_NO"].ToString();
				if (_billId == "SO")
				{
					int _bilItm = Convert.ToInt32(dr["BIL_ITM"]);
					SunlikeDataSet _dsSo = _drpSo.GetBody(_billId, _billNo,"ITM", _bilItm,true);
					if (_dsSo.Tables[0].Rows.Count > 0)
					{
						dr["QTY_SO_ORG"] = _dsSo.Tables[0].Rows[0]["QTY"];
					}
				}
			}
			//增加单据权限
			if (!OnlyFillSchema)
			{
				if (usr != null && !String.IsNullOrEmpty(usr))
				{
					string _pgm = "DRP" + IcID;
					DataTable _dtMf = _ds.Tables["MF_IC"];
					if (_dtMf.Rows.Count > 0 )
					{
						string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
						string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
						System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr,_bill_Dep,_bill_Usr);
						_ds.ExtendedProperties["UPD"] = _billRight["UPD"];
						_ds.ExtendedProperties["DEL"] = _billRight["DEL"];
						_ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
					}
				}
			}
			//创建存放序列号的暂存表
			DataTable _dt = new DataTable("BAR_COLLECT");
			_dt.Columns.Add("ITEM",typeof(int));
			_dt.Columns.Add("BAR_CODE");
			_dt.Columns.Add("BAT_NO");
			_dt.Columns.Add("BOX_NO");
			_dt.Columns.Add("PRD_NO");
			_dt.Columns.Add("PRD_MARK");
			//表身出货、入货库位
			_dt.Columns.Add("WH1");
			_dt.Columns.Add("WH2");
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
			//设定表身KeyItm为自动递增
			DataTable _dtBody = _ds.Tables["TF_IC"];
			_dtBody.Columns["KEY_ITM"].AutoIncrement = true;
			_dtBody.Columns["KEY_ITM"].AutoIncrementSeed = _dtBody.Rows.Count > 0 ? Convert.ToInt32(_dtBody.Select("","KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
			_dtBody.Columns["KEY_ITM"].AutoIncrementStep = 1;
			_dtBody.Columns["KEY_ITM"].Unique = true;
			//设定箱内容KeyItm为自动递增
			DataTable _dtBox = _ds.Tables["TF_IC_BOX"];
			_dtBox.Columns["KEY_ITM"].AutoIncrement = true;
			_dtBox.Columns["KEY_ITM"].AutoIncrementSeed = _dtBox.Rows.Count > 0 ? Convert.ToInt32(_dtBox.Select("","KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
			_dtBox.Columns["KEY_ITM"].AutoIncrementStep = 1;
			_dtBox.Columns["KEY_ITM"].Unique = true;
			_dtBody.Columns.Add("PRD_NO_NO", typeof(System.String));
			_dtBox.Columns.Add("PRD_NO_NO", typeof(System.String));

			//判断单据能否修改
			this.SetCanModify(_ds, usr, true,false);
			//表身加库存量栏位
			DataColumn _dc = new DataColumn("WH_QTY");
			_dtBody.Columns.Add(_dc);
            _dtBody.Columns.Add("UNIT_DP");
			//表身加单位标准成本
			_dtBody.Columns.Add(new DataColumn("CST_STD_UNIT",typeof(decimal)));
			//给单位标准成本赋值
			for (int i=0;i<_ds.Tables["TF_IC"].Rows.Count;i++)
			{
				if (!String.IsNullOrEmpty(_ds.Tables["TF_IC"].Rows[i]["CST_STD"].ToString())
					&& !String.IsNullOrEmpty(_ds.Tables["TF_IC"].Rows[i]["QTY"].ToString())
					&& Convert.ToDecimal(_ds.Tables["TF_IC"].Rows[i]["QTY"]) != 0)
				{
					_ds.Tables["TF_IC"].Rows[i]["CST_STD_UNIT"] = Convert.ToDecimal(_ds.Tables["TF_IC"].Rows[i]["CST_STD"])/Convert.ToDecimal(_ds.Tables["TF_IC"].Rows[i]["QTY"]);
				}
				else
				{
					_ds.Tables["TF_IC"].Rows[i]["CST_STD_UNIT"] = 0;
				}
				_ds.Tables["TF_IC"].Rows[i]["PRD_NO_NO"] = _ds.Tables["TF_IC"].Rows[i]["PRD_NO"];
			}
			//取得箱信息
			_dc = new DataColumn("CONTENT_DSC");
			_dtBox.Columns.Add(_dc);
			_dc = new DataColumn("WH_QTY");
			_dtBox.Columns.Add(_dc);
			BarBox _box = new BarBox();
			for (int i=0;i<_dtBox.Rows.Count;i++)
			{
				DataRow _drBox = _dtBox.Rows[i];
				_drBox["PRD_NO_NO"] = _drBox["PRD_NO"];
				_drBox["CONTENT_DSC"] = _box.GetBar_BoxDsc(_drBox["PRD_NO"].ToString(),_drBox["CONTENT"].ToString());
			}

			//把序列号的记录转入暂存表中
			DataView _dv = new DataView(_ds.Tables["TF_IC_BAR"]);
			_dv.Sort = "BOX_NO,BAR_CODE";
			for (int i=0;i<_dv.Count;i++)
			{
				string _barCode = _dv[i]["BAR_CODE"].ToString();
				DataRow _dr = _dt.NewRow();
				_dr["ITEM"] = _dt.Rows.Count + 1;
				_dr["BAR_CODE"] = _barCode;
				if (!String.IsNullOrEmpty(_dv[i]["BOX_NO"].ToString()))
				{
					_dr["BOX_NO"] = _dv[i]["BOX_NO"];
				}
				//取库位、批号信息
				BarCode.BarInfo _barInfo = BarCode.GetBarInfo(_barCode);
				_dr["PRD_NO"] = _barInfo.Prd_No;
				_dr["PRD_MARK"] = _barInfo.Prd_Mark;
				_dr["SERIAL_NO"] = _barInfo.Serial_No;
				_dt.Rows.Add(_dr);
			}
			//取序列号库位、批号
			foreach (DataRow dr in _dt.Rows)
			{
				DataRow[] _aryDrBar = _ds.Tables["TF_IC_BAR"].Select("BAR_CODE='"+dr["BAR_CODE"].ToString()+"'");
				if (_aryDrBar.Length > 0)
				{
					DataRow[] _aryDr = _ds.Tables["TF_IC"].Select("KEY_ITM=" + _aryDrBar[0]["IC_ITM"].ToString());
					if (_aryDr.Length > 0)
					{
						dr["WH1"] = _aryDr[0]["WH1"];
						dr["WH2"] = _aryDr[0]["WH2"];
						dr["BAT_NO"] = _aryDr[0]["BAT_NO"];
					}
				}
			}
			_dv.Dispose();
			GC.Collect(GC.GetGeneration(_dv));
			_ds.AcceptChanges();

            //获取到货确认信息
            string _userCfmSwFlag = Users.GetSpcPswdString(usr, "CFM_SW_FLAG");//特殊密码-到货确认管控否
            if (!String.IsNullOrEmpty(_userCfmSwFlag) && _userCfmSwFlag == "T")
            {
                string _userCfmSwControl = Users.GetSpcPswdString(usr, "CFM_SW_CONTROL");//特殊密码-到货管控方式
                if (!String.IsNullOrEmpty(_userCfmSwControl))
                {
                    _ds.ExtendedProperties["CFM_SW_CONTROL"] = _userCfmSwControl;//参数设定-到货管控方式
                }
            }
            else
            {
                _ds.ExtendedProperties["CFM_SW_CONTROL"] = Comp.DRP_Prop["CFM_SW_CONTROL"];
            }

			return _ds;
        }

        //判断单据能否修改
		private void SetCanModify(SunlikeDataSet ChangedDS, string usr, bool IsCheckAuditing, bool IsRollBack)
		{
			if (ChangedDS.Tables["MF_IC"].Rows.Count > 0)
			{
				DataTable _dtMf = ChangedDS.Tables["MF_IC"];
				DataTable _dtBody = ChangedDS.Tables["TF_IC"];
				string _bilId = _dtMf.Rows[0]["IC_ID"].ToString();
				bool _canModify = true;
                if (_dtMf.Rows[0]["CLS_ID"].ToString() == "T" || _dtMf.Rows[0]["IZ_CLS_ID"].ToString() == "T")
				{
                    _canModify = false;
                    //Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_MODIFY");
				}
				if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["IC_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
				{
                    _canModify = false;
                    //Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_CLS");
				}
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["EP_NO"].ToString()))
                {
                    Arp _arp = new Arp();
                    MonEX _monEx = new MonEX();
                    DataSet _dsEp = _monEx.GetData(_dtMf.Rows[0]["EP_ID"].ToString(), _dtMf.Rows[0]["EP_NO"].ToString(), false);
                    foreach (DataRow drEp in _dsEp.Tables["TF_EXP"].Rows)
                    {
                        if (!String.IsNullOrEmpty(drEp["ARP_NO"].ToString()))
                        {
                            if (_arp.HasReceiveDollar(drEp["ARP_NO"].ToString()))
                            {
                                //Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_LZ_MODIFY");
                                _canModify = false;
                                break;
                            }
                        }
                    }
                }
				Auditing _auditing = new Auditing();
				if (IsCheckAuditing)
				{
					string _icID = ChangedDS.Tables["MF_IC"].Rows[0]["IC_ID"].ToString();
					string _icNo = ChangedDS.Tables["MF_IC"].Rows[0]["IC_NO"].ToString();
                    if (_auditing.GetIfEnterAuditing(_icID,_icNo))
                    {
                        _canModify = false;
                        //Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
				}
                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    //Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_LOCK");
                    _canModify = false;
                }
                #region 凭证的对应
                DataRow _drMf = ChangedDS.Tables["MF_IC"].Rows[0];
                if (_canModify && !String.IsNullOrEmpty(_drMf["VOH_NO"].ToString()))
                {
                    //判断凭证
                    string _acNo = "";
                    DrpVoh _drpVoh = new DrpVoh();
                    string _updUsr = "";
                    if (ChangedDS.ExtendedProperties.ContainsKey("UPD_USR"))
                    {
                        _updUsr = ChangedDS.ExtendedProperties["UPD_USR"].ToString();
                    }
                    else
                    {
                        _updUsr = _drMf["USR"].ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_drMf["VOH_NO"].ToString(), _updUsr, ref _acNo);
                    if (_resVoh == 0 || _resVoh == 1)
                    {
                        ChangedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = true;
                        ChangedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                    else if (_resVoh == 2)
                    {
                        ChangedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = false;
                        ChangedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                }
                #endregion
				decimal _qty,_qtyFa;
				//如果只修改表头就更改属性设置
				if (_dtBody.Rows.Count >0 )
				{
                    //if (_canModify)
					{
                        int _faCount = 0;
						for (int i=0;i<_dtBody.Rows.Count;i++)
						{
							if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY"].ToString()))
							{
								_qty = 0;
							}
							else
							{
								_qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY"]);
							}
							if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY_FA"].ToString()))
							{
								_qtyFa = 0;
							}
							else
							{
								_qtyFa = Convert.ToDecimal(_dtBody.Rows[i]["QTY_FA"]);
							}
                            if (_qty == _qtyFa)
                            {
                                _faCount++;
                            }
							//判断开发票
							if (!String.IsNullOrEmpty(_dtBody.Rows[i]["AMTN_NET_FP"].ToString()) && Convert.ToDecimal(_dtBody.Rows[i]["AMTN_NET_FP"]) != 0)
							{
								_canModify = false;
								break;
							}
							if (_bilId == "IM" && !String.IsNullOrEmpty(_dtBody.Rows[i]["AMTN_NET_FP2"].ToString()))
							{
								_canModify = false;
								break;
							}
                            if (IsRollBack)
                            {
                                decimal _qtyCfm = 0;
                                decimal _qtyLost = 0;
                                if (!String.IsNullOrEmpty(_dtBody.Rows[i]["QTY_CFM"].ToString()))
                                {
                                    _qtyCfm = Convert.ToDecimal(_dtBody.Rows[i]["QTY_CFM"]);
                                }
                                if (!String.IsNullOrEmpty(_dtBody.Rows[i]["QTY_LOST"].ToString()))
                                {
                                    _qtyLost = Convert.ToDecimal(_dtBody.Rows[i]["QTY_LOST"]);
                                }
                                if (_qtyCfm + _qtyLost > 0)
                                {
                                    _canModify = false;
                                    break;
                                }
                            }
						}
                        if (_faCount == _dtBody.Rows.Count)
                        {
                            _canModify = false;
                        }
					}
				}
//				if (_auditing.IsRunAuditing(_bilId, _bilUsr))
				{
					//已终审的调拨相关单据可以修改金额和备注
					if (!String.IsNullOrEmpty(ChangedDS.Tables["MF_IC"].Rows[0]["CHK_MAN"].ToString()))
					{
						Users _users = new Users();
						DataTable _dtSpcPswd = _users.GetSpcPswd(usr, "IC_MODIFY_REM");
						if (_dtSpcPswd.Rows.Count > 0)
						{
							if (_dtSpcPswd.Rows[0]["SPC_ID"].ToString() == "T")
							{
								_canModify = true;
								ChangedDS.ExtendedProperties["IC_MODIFY_REM"] = "";
							}
						}
					}
                }
                if (_canModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["VOH_NO"].ToString()))
                {
                    //判断凭证
                    string _acNo = "";
                    DrpVoh _drpVoh = new DrpVoh();
                    int _resVoh = _drpVoh.CheckBillVohAc(_dtMf.Rows[0]["VOH_NO"].ToString(), _dtMf.Rows[0]["USR"].ToString(), ref _acNo);
                    if (_resVoh == 0 || _resVoh == 1)
                    {
                        ChangedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = false;
                        ChangedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                    else if (_resVoh == 2)
                    {
                        ChangedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = true;
                        ChangedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                }
				ChangedDS.ExtendedProperties["CAN_MODIFY"] = _canModify.ToString().Substring(0,1).ToUpper();
			}
        }

        #endregion

		#region 保存
		/// <summary>
		/// 保存单据资料
		/// </summary>
		/// <param name="ChangedDS"></param>
		public void UpdateData(SunlikeDataSet ChangedDS)
		{
			ChangedDS.Tables["TF_IC_BAR"].TableName = "TF_IC1";
			ChangedDS.Tables["TF_IC_BOX"].TableName = "TF_IC2";
            //是否重建凭证号码
            if (ChangedDS.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", ChangedDS.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_IC"] = "IC_ID,IC_NO,IC_DD,FIX_CST,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,DEP,BIL_TYPE,SAL_NO,CUS_NO1,CUS_NO2,CLS_ID,"
                + "SYS_DATE,TOT_BOX,TOT_QTY,BIL_ID,BIL_NO,OUTDEP,SAL_NO2,EP_ID,EP_NO,BAT_NO,VOH_ID,VOH_NO,CFM_SW,IZ_CLS_ID,IZ_BACK_ID,"
                + "TURN_ID2,LZ_CLS_ID2,AMT_CLS2,AMTN_NET_CLS2,TAX_CLS2,QTY_CLS2,"
                + "TURN_ID,LZ_CLS_ID,AMT_CLS,AMTN_NET_CLS,TAX_CLS,QTY_CLS,MOB_ID";
            _ht["TF_IC"] = "IC_ID,IC_NO,ITM,IC_DD,PRD_NO,PRD_MARK,UNIT,QTY,WH1,WH2,CST,FIX_CST,CST_STD,QTY_FA,KEY_ITM,UP,"
                + "AMTN_NET,RTN_ID,BOX_ITM,BIL_ID,BIL_NO,BIL_ITM,UP_CST,DIS_CNT,PRE_ITM,BAT_NO,BAT_NO2,VALID_DD,AMTN_EP,QTY_CFM,"
                + "QTY_LOST,EST_DD,REM,AMTN_NET_FP2,AMT_FP2,TAX_FP2,QTY_FP2,AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,QTY1,UP_QTY1,UP_QTY1_CST,PRD_MARK2";
//			_ht["TF_IC1"] = "IC_ID,IC_NO,IC_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
			_ht["TF_IC2"] = "IC_ID,IC_NO,ITM,PRD_NO,CONTENT,QTY,QTY_FA,KEY_ITM,WH1,WH2,BIL_ID,BIL_NO,BIL_ITM";
			//判断是否走审核流程
			string _icID,_usr;
			DataRow _dr = ChangedDS.Tables["MF_IC"].Rows[0];
			if (_dr.RowState == DataRowState.Deleted)
			{
				_icID = _dr["IC_ID",DataRowVersion.Original].ToString();
				_usr = _dr["USR",DataRowVersion.Original].ToString();
			}
			else
			{
				_icID = _dr["IC_ID"].ToString();
				_usr = _dr["USR"].ToString();
			}
			Auditing _auditing = new Auditing();

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
            //_isRunAuditing = _auditing.IsRunAuditing(_icID, _usr, bilType, _mobID);


            this.UpdateDataSet(ChangedDS,_ht);
			//判断单据能否修改
			if (!ChangedDS.HasErrors)
			{

                //增加单据权限
                string _UpdUsr = "";
                if (ChangedDS.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = ChangedDS.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "DRP" + _icID;
                    DataTable _dtMf = ChangedDS.Tables["MF_IC"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        ChangedDS.ExtendedProperties["UPD"] = _billRight["UPD"];
                        ChangedDS.ExtendedProperties["DEL"] = _billRight["DEL"];
                        ChangedDS.ExtendedProperties["PRN"] = _billRight["PRN"];
                       ChangedDS.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
				this.SetCanModify(ChangedDS, "", true,false);
			}
			ChangedDS.Tables["TF_IC1"].TableName = "TF_IC_BAR";
			ChangedDS.Tables["TF_IC2"].TableName = "TF_IC_BOX";
			//取得配码比描述
			if (!ChangedDS.HasErrors)
			{
				BarBox _box = new BarBox();
				DataTable _dtBox = ChangedDS.Tables["TF_IC_BOX"];
				for (int i=0;i<_dtBox.Rows.Count;i++)
				{
					DataRow _drBox = _dtBox.Rows[i];
					if (String.IsNullOrEmpty(_drBox["CONTENT_DSC"].ToString()))
					{
						_drBox["CONTENT_DSC"] = _box.GetBar_BoxDsc(_drBox["PRD_NO"].ToString(),_drBox["CONTENT"].ToString());
					}
				}
				_dtBox.AcceptChanges();
			}
		}

		/// <summary>
		/// BeforeDsSave
		/// </summary>
		/// <param name="ds"></param>
		protected override void BeforeDsSave(DataSet ds)
		{
            //#region 单据追踪
            //DataTable _dtMf = ds.Tables["MF_IC"];
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["IC_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["IC_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}

            //#endregion
			if (ds.ExtendedProperties["IC_MODIFY_REM"] == null)
			{
				if (ds.Tables["MF_IC"].Rows.Count > 0
					&& ds.Tables["MF_IC"].Rows[0].RowState == DataRowState.Modified
					&& this._isRunAuditing
					&& !String.IsNullOrEmpty(ds.Tables["MF_IC"].Rows[0]["CHK_MAN"].ToString()))
				{
					string _rbError = this.RollBack(ds.Tables["MF_IC"].Rows[0]["IC_ID"].ToString(),
						ds.Tables["MF_IC"].Rows[0]["IC_NO"].ToString(), false);
					if (!String.IsNullOrEmpty(_rbError))
					{
						throw new SunlikeException(_rbError);
					}
				}
				base.BeforeDsSave (ds);
			}
		}


		/// <summary>
		/// 保存单据之前的动作
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="status"></param>
		protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
		{
			if (dr.Table.DataSet.ExtendedProperties["IC_MODIFY_REM"] == null)
			{
				if (tableName == "MF_IC")
				{
					string _usr,_icID,_icNO;
					if (statementType == StatementType.Delete)
					{
						_icID = dr["IC_ID",DataRowVersion.Original].ToString();
                        _icNO = dr["IC_NO", DataRowVersion.Original].ToString();
						_usr = dr["USR",DataRowVersion.Original].ToString();
					}
					else
					{
						_icID = dr["IC_ID"].ToString();
                        _icNO = dr["IC_NO"].ToString();
						_usr = dr["USR"].ToString();
					}
                    if (statementType != StatementType.Insert)
                    {
                        //判断是否锁单，如果已经锁单则不让修改。
                        Users _Users = new Users();
                        string _whereStr = "IC_ID = '" + _icID + "' AND IC_NO = '" + _icNO + "'";
                        if (_Users.IsLocked("MF_IC", _whereStr))
                        {
                            throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                        }
                    }
					//检查资料正确否
					if (statementType != StatementType.Delete)
					{
                        if (statementType == StatementType.Update)
                        {
                            m_UpdateWhByCfmSw = dr["CFM_SW"].ToString() != dr["CFM_SW", DataRowVersion.Original].ToString();
                            m_CfmSwOrg = dr["CFM_SW", DataRowVersion.Original].ToString();
                        }

						//新增时判断关账日期
						if (statementType != StatementType.Delete)
						{
							if (Comp.HasCloseBill(Convert.ToDateTime(dr["IC_DD"]), dr["DEP"].ToString(), "CLS_INV"))
							{
								throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
							}
						}
						else
						{
							if (Comp.HasCloseBill(Convert.ToDateTime(dr["IC_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
							{
								throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
							}
						}
						//检查出货客户
						Cust _cust = new Cust();
						if (_icID != "IO" && _icID != "IC" && !_cust.IsExist(_usr,dr["CUS_NO1"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
						{
							dr.SetColumnError("CUS_NO1","RCID=COMMON.HINT.CUS_NO1_NOTEXIST,PARAM=" + dr["CUS_NO1"].ToString());//出货客户[{0}]不存在或没有对其操作的权限，请检查
							status = UpdateStatus.SkipAllRemainingRows;
						}
						//检查入货客户
						if (_icID != "IB" && _icID != "IC" && !_cust.IsExist(_usr,dr["CUS_NO2"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
						{
							dr.SetColumnError("CUS_NO2","RCID=INV.HINT.CUS_NO2_NOTEXIST,PARAM=" + dr["CUS_NO2"].ToString());//入货客户[{0}]不存在或没有对其操作的权限，请检查
							status = UpdateStatus.SkipAllRemainingRows;
						}
						//检查业务员
						Salm _salm = new Salm();
						if (!_salm.IsExist(_usr,dr["SAL_NO"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
						{
							dr.SetColumnError("SAL_NO","RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
							status = UpdateStatus.SkipAllRemainingRows;
						}
						if (_icID == "IM")
						{
							if (!_salm.IsExist(_usr,dr["SAL_NO2"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
							{
								dr.SetColumnError("SAL_NO2","RCID=INV.HINT.SAL_NO2_NOTEXIST,PARAM=" + dr["SAL_NO2"].ToString());//入货客户业务[{0}]不存在或没有对其操作的权限，请检查
								status = UpdateStatus.SkipAllRemainingRows;
							}
						}
						//检查拨出部门
						Dept _dept = new Dept();
						if (!_dept.IsExist(_usr,dr["DEP"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
						{
							dr.SetColumnError("DEP","RCID=INV.DEPT.HINT,PARAM=" + dr["DEP"].ToString());//拨出部门[{0}]不存在或没有对其操作的权限，请检查
							status = UpdateStatus.SkipAllRemainingRows;
						}
						//检查拨入部门
						if (!_dept.IsExist(_usr,dr["OUTDEP"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
						{
							dr.SetColumnError("OUTDEP","RCID=INV.HINT.OUTDEP_NOTEXIST,PARAM=" + dr["OUTDEP"].ToString());//拨入部门[{0}]不存在或没有对其操作的权限，请检查
							status = UpdateStatus.SkipAllRemainingRows;
						}
					}
					SQNO _sq = new SQNO();
					if (statementType == StatementType.Insert)
					{
						//取得保存单号
						dr["IC_NO"] = _sq.Set(_icID,_usr,dr["DEP"].ToString(),Convert.ToDateTime(dr["IC_DD"]),dr["BIL_TYPE"].ToString());
						//写入默认栏位值
						//dr["FIX_CST"] = "1";
						dr["PRT_SW"] = "N";
						dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
					}
					else if (statementType == StatementType.Delete)
					{
						string _error = _sq.Delete(dr["IC_NO",DataRowVersion.Original].ToString(),dr["USR",DataRowVersion.Original].ToString());
						if (!String.IsNullOrEmpty(_error))
						{
							throw new SunlikeException("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
						}
					}
                    ////判断是否走审核流程
                    //if (_isRunAuditing && dr.RowState == DataRowState.Deleted)
                    //{
                    //    Auditing _auditing = new Auditing();
                    //    _auditing.DelBillWaitAuditing("DRP",_icID,dr["IC_NO",DataRowVersion.Original].ToString());
                    //}

                    //#region 审核相关
                    //AudParamStruct _aps;
                    //if (dr.RowState != DataRowState.Deleted)
                    //{
                    //    _aps.BIL_DD = Convert.ToDateTime(dr["IC_DD"]);
                    //    _aps.BIL_ID = dr["IC_ID"].ToString();
                    //    _aps.BIL_NO = dr["IC_NO"].ToString();
                    //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                    //    if (dr["IC_ID"].ToString() == "IB")
                    //    {
                    //        _aps.CUS_NO = dr["CUS_NO1"].ToString();
                    //    }
                    //    else
                    //    {
                    //        _aps.CUS_NO = dr["CUS_NO2"].ToString();
                    //    }
                    //    _aps.DEP = dr["DEP"].ToString();
                    //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                    //    _aps.USR = dr["USR"].ToString();
                    //    if (dr["IC_ID"].ToString() == "IM")
                    //    {
                    //        _aps.CUS_NO = "";
                    //        _aps.SAL_NO = "";
                    //    }
                    //    _aps.MOB_ID = "";
                    //}
                    //else
                    //{
                    //    _aps = new AudParamStruct(Convert.ToString(dr["IC_ID", DataRowVersion.Original]), Convert.ToString(dr["IC_NO", DataRowVersion.Original]));
                    //}

                    //Auditing _auditing = new Auditing();
                    //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                    //if (!string.IsNullOrEmpty(_auditErr))
                    //{
                    //    throw new SunlikeException(_auditErr);
                    //}
                    //#endregion 

                    //产生凭证
                    if (!this._isRunAuditing)
                    {
                        this.UpdateVohNo(dr, statementType);
                    }
				}
				else if (tableName == "TF_IC")
				{
                    if (statementType != StatementType.Delete)
                    {
                        //写表身的日期栏位
                        dr["IC_DD"] = dr.Table.DataSet.Tables["MF_IC"].Rows[0]["IC_DD"];
                        string _usr = dr.Table.DataSet.Tables["MF_IC"].Rows[0]["USR"].ToString();
                        //检查货品代号
                        Prdt _prdt = new Prdt();
                        if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
                        {
                            dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + dr["PRD_NO"].ToString());//货品代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //检查特征分段
                        string _prdMark = dr["PRD_MARK"].ToString();
                        int _prdMod = _prdt.CheckPrdtMod(dr["PRD_NO"].ToString(), _prdMark);
                        if (_prdMod == 1)
                        {
                            dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prdMark);//货品特征[{0}]不存在
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        else if (_prdMod == 2)
                        {
                            PrdMark _mark = new PrdMark();
                            if (_mark.RunByPMark(_usr))
                            {
                                string[] _aryMark = _mark.BreakPrdMark(_prdMark);
                                DataTable _dtMark = _mark.GetSplitData("");
                                for (int i = 0; i < _dtMark.Rows.Count; i++)
                                {
                                    string _fldName = _dtMark.Rows[i]["FLDNAME"].ToString();
                                    if (!_mark.IsExist(_fldName, dr["PRD_NO"].ToString(), _aryMark[i]))
                                    {
                                        dr.SetColumnError(_fldName, "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _aryMark[i].Trim());//货品特征[{0}]不存在
                                        status = UpdateStatus.SkipAllRemainingRows;
                                    }
                                }
                            }
                        }
                        dr["PRD_MARK2"] = dr["PRD_MARK"];

                        //检查出货库位
                        string _cusNo1 = dr.Table.DataSet.Tables["MF_IC"].Rows[0]["CUS_NO1"].ToString();
                        string _cusNo2 = dr.Table.DataSet.Tables["MF_IC"].Rows[0]["CUS_NO2"].ToString();
                        WH _wh = new WH();
                        if (dr["IC_ID"].ToString() == "IO" || dr["IC_ID"].ToString() == "IC")
                        {
                            if (!_wh.IsExist(_usr, dr["WH1"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
                            {
                                dr.SetColumnError("WH1", "RCID=INV.HINT.WH1_NOTEXIST,PARAM=" + dr["WH1"].ToString());//出货库位[{0}]不存在或没有对其操作的权限，请检查
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        else
                        {
                            if (!_wh.IsExist(_usr, dr["WH1"].ToString(), Convert.ToDateTime(dr["IC_DD"]), _cusNo1))
                            {
                                dr.SetColumnError("WH1", "RCID=INV.HINT.WH1_NOTEXIST,PARAM=" + dr["WH1"].ToString());//出货库位[{0}]不存在或没有对其操作的权限，请检查
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        //检查入货库位
                        if (dr["IC_ID"].ToString() == "IB" || dr["IC_ID"].ToString() == "IC")
                        {
                            if (!_wh.IsExist(_usr, dr["WH2"].ToString(), Convert.ToDateTime(dr["IC_DD"])))
                            {
                                dr.SetColumnError("WH2", "RCID=INV.HINT.WH2_NOTEXIST,PARAM=" + dr["WH2"].ToString());//入货库位[{0}]不存在或没有对其操作的权限，请检查
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        else
                        {
                            if (!_wh.IsExist(_usr, dr["WH2"].ToString(), Convert.ToDateTime(dr["IC_DD"]), _cusNo2))
                            {
                                dr.SetColumnError("WH2", "RCID=INV.HINT.WH2_NOTEXIST,PARAM=" + dr["WH2"].ToString());//入货库位[{0}]不存在或没有对其操作的权限，请检查
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        if (dr["BIL_ID"].ToString() == "SO")
                        {
                            DrpSO _drpSo = new DrpSO();
                            DataTable _dt = _drpSo.GetDataMf("SO", dr["BIL_NO"].ToString());
                            if (_dt.Rows.Count == 0)
                            {
                                dr.SetColumnError("BIL_NO", "RCID=INV.DRPIO.SONOTEXIST");//受定单号不存在
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                        {
                            Bat _bat = new Bat();
                            if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                            {
                                dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM=" + dr["BAT_NO"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        dr["FIX_CST"] = dr.Table.DataSet.Tables["MF_IC"].Rows[0]["FIX_CST"];
                        if (statementType == StatementType.Insert)
                        {
                            dr["PRE_ITM"] = dr["KEY_ITM"];
                        }
                        //自动新增批号
                        if (!String.IsNullOrEmpty(dr["BAT_NO2"].ToString()))
                        {
                            Bat _bat = new Bat();
                            if (!_bat.IsExist(dr["BAT_NO2"].ToString()) && Users.GetSpcPswdString(_usr, "AUTO_NEW_BATNO") == "F")
                            {
                                throw new Exception("RCID=COMMON.HINT.AUTO_NEW_BATNO,PARAM=" + dr["BAT_NO2"].ToString());
                            }
                        }
                    }
                    else
                    {
                        //判断是否有补开发票则不允许删除
                        checktflz(dr);

                    }

                    #region 检查到货量

                    decimal _qtyCfm = 0;
                    decimal _qtyLost = 0;
                    decimal _qty = 0;
                    if (statementType == StatementType.Update)
                    {
                        if (!String.IsNullOrEmpty(dr["QTY_CFM"].ToString()))
                        {
                            _qtyCfm = Convert.ToDecimal(dr["QTY_CFM"]);
                        }
                        if (!String.IsNullOrEmpty(dr["QTY_LOST"].ToString()))
                        {
                            _qtyLost = Convert.ToDecimal(dr["QTY_LOST"]);
                        }
                        if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                        {
                            _qty = Convert.ToDecimal(dr["QTY"]);
                        }
                        if (_qty < _qtyCfm + _qtyLost)
                        {
                            dr.SetColumnError("QTY", "RCID=INV.DRPIO.MODIFY_CFM");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        if (!String.IsNullOrEmpty(dr["QTY_CFM", DataRowVersion.Original].ToString()))
                        {
                            _qtyCfm = Convert.ToDecimal(dr["QTY_CFM", DataRowVersion.Original]);
                        }
                        if (!String.IsNullOrEmpty(dr["QTY_LOST", DataRowVersion.Original].ToString()))
                        {
                            _qtyLost = Convert.ToDecimal(dr["QTY_LOST", DataRowVersion.Original]);
                        }
                        if (_qtyCfm + _qtyLost > 0)
                        {
                            dr.SetColumnError("QTY", "RCID=INV.DRPIO.DELETE_CFM");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    #endregion
                }
				if (_barCodeNo == 0)
				{
					DataRow _dr = dr.Table.DataSet.Tables["MF_IC"].Rows[0];
					string _icID;
					if (_dr.RowState == DataRowState.Deleted)
					{
						_icID = _dr["IC_ID",DataRowVersion.Original].ToString();
					}
					else
					{
						_icID = _dr["IC_ID"].ToString();
					}
					if (!this._isRunAuditing)
					{
						//更新序列号记录
						try
						{
							this.UpdateBarCode(dr.Table.DataSet, false);
						}
						catch(Exception _ex)
						{
							status = UpdateStatus.SkipAllRemainingRows;
							throw new SunlikeException(_ex.Message, _ex);
						}
					}
					//更新退货费用
					this.UpdateExp(SunlikeDataSet.ConvertTo(dr.Table.DataSet),_isRunAuditing);

					string _fieldList = "IC_ID,IC_NO,IC_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
					SQLBatchUpdater _sbu = new SQLBatchUpdater(Comp.Conn_DB);
					_sbu.BatchUpdateSize = 50;
					_sbu.BatchUpdate(dr.Table.DataSet.Tables["TF_IC1"],_fieldList);
				}
				_barCodeNo ++;
			}
		}

		/// <summary>
		/// 保存单据之后的动作
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="status"></param>
		/// <param name="recordsAffected"></param>
		protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
		{
			if (dr.Table.DataSet.ExtendedProperties["IC_MODIFY_REM"] == null)
			{
				//判断是否走审核流程
				if (!_isRunAuditing)
				{
					if (tableName == "TF_IC")
                    {
                        if (statementType != StatementType.Insert)
                        {
                            decimal _qtyFp = 0;
                            decimal _amtnNetFp = 0;
                            if (statementType == StatementType.Delete)
                            {
                                if (!String.IsNullOrEmpty(dr["QTY_FP", DataRowVersion.Original].ToString()))
                                {
                                    _qtyFp = Convert.ToDecimal(dr["QTY_FP", DataRowVersion.Original]);
                                }
                                if (!String.IsNullOrEmpty(dr["AMTN_NET_FP", DataRowVersion.Original].ToString()))
                                {
                                    _amtnNetFp = Convert.ToDecimal(dr["AMTN_NET_FP", DataRowVersion.Original]);
                                }
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(dr["QTY_FP"].ToString()))
                                {
                                    _qtyFp = Convert.ToDecimal(dr["QTY_FP"]);
                                }
                                if (!String.IsNullOrEmpty(dr["AMTN_NET_FP"].ToString()))
                                {
                                    _amtnNetFp = Convert.ToDecimal(dr["AMTN_NET_FP"]);
                                }
                            }
                            if (_qtyFp != 0 || _amtnNetFp != 0)
                            {
                                throw new Exception("已开票的单据不能修改或删除");
                            }
                        }
                        if (statementType != StatementType.Delete)
                        {
                            //记录更新过的DataRow
                            m_UpdateList.Add(dr["KEY_ITM"].ToString(), "");
                        }
						if (_bodyNo == 0)
						{
							//更新SARP
							this.UpdateSarp(dr,false);
						}
						//修改分仓存量、受订单配送数量
						try
						{
							if (statementType == StatementType.Insert)
							{
								this.UpdateWh(dr,true);
								this.UpdateBilNo(dr,true);
							}
							else if (statementType == StatementType.Delete)
							{
								this.UpdateWh(dr,false);
								this.UpdateBilNo(dr,false);
							}
							else if (statementType == StatementType.Update)
							{
								this.UpdateWh(dr,false);
								this.UpdateWh(dr,true);
								this.UpdateBilNo(dr,false);
								this.UpdateBilNo(dr,true);
							}
						}
						catch(Exception _ex)
						{
							dr.SetColumnError("QTY",_ex.Message);
							status = UpdateStatus.SkipAllRemainingRows;
						}
						_bodyNo ++;
					}
					else if (tableName == "TF_IC2")
					{
						try
						{
							string _bilNo,_bilID,_bilItm;
							decimal _qty = 0;
							DrpSO _so = new DrpSO();
							if (statementType == StatementType.Insert)
							{
								this.UpdateBoxWh(dr,true);
								_bilID = dr["BIL_ID"].ToString();
								_bilNo = dr["BIL_NO"].ToString();
								_bilItm = dr["BIL_ITM"].ToString();
								_qty = 0;
								if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
								{
									_qty = Convert.ToDecimal(dr["QTY"]);
								}
								if (!String.IsNullOrEmpty(_bilNo) && _bilID == "SO" && !String.IsNullOrEmpty(_bilItm))
								{
									_so.UpdateBoxQty(_bilNo,_bilItm,_qty);
								}
							}
							else if (statementType == StatementType.Delete)
							{
								this.UpdateBoxWh(dr,false);
								_bilID = dr["BIL_ID",DataRowVersion.Original].ToString();
								_bilNo = dr["BIL_NO",DataRowVersion.Original].ToString();
								_bilItm = dr["BIL_ITM",DataRowVersion.Original].ToString();
								if (!String.IsNullOrEmpty(dr["QTY",DataRowVersion.Original].ToString()))
								{
									_qty = Convert.ToDecimal(dr["QTY",DataRowVersion.Original]);
								}
								_qty *= -1;
								if (!String.IsNullOrEmpty(_bilNo) && _bilID == "SO" && !String.IsNullOrEmpty(_bilItm))
								{
									_so.UpdateBoxQty(_bilNo,_bilItm,_qty);
								}
							}
							else if (statementType == StatementType.Update)
							{
								this.UpdateBoxWh(dr,false);
								this.UpdateBoxWh(dr,true);
								_bilID = dr["BIL_ID",DataRowVersion.Original].ToString();
								_bilNo = dr["BIL_NO",DataRowVersion.Original].ToString();
								_bilItm = dr["BIL_ITM",DataRowVersion.Original].ToString();
								if (!String.IsNullOrEmpty(dr["QTY",DataRowVersion.Original].ToString()))
								{
									_qty = Convert.ToDecimal(dr["QTY",DataRowVersion.Original]);
								}
								_qty *= -1;
								if (!String.IsNullOrEmpty(_bilNo) && _bilID == "SO" && !String.IsNullOrEmpty(_bilItm))
								{
									_so.UpdateBoxQty(_bilNo,_bilItm,_qty);
								}
								_bilID = dr["BIL_ID"].ToString();
								_bilNo = dr["BIL_NO"].ToString();
								_bilItm = dr["BIL_ITM"].ToString();
								if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
								{
									_qty = Convert.ToDecimal(dr["QTY"]);
								}
								if (!String.IsNullOrEmpty(_bilNo) && _bilID == "SO" && !String.IsNullOrEmpty(_bilItm))
								{
									_so.UpdateBoxQty(_bilNo,_bilItm,_qty);
								}
							}
						}
						catch(Exception _ex)
						{
							dr.SetColumnError("QTY","RCID=INV.HINT.UPDATEBOXWHERROR,PARAM=" + _ex.Message);//修改箱库存、受订单箱配送数量失败，原因：
							status = UpdateStatus.SkipAllRemainingRows;
						}
					}
				}
			}
		}

		/// <summary>
		/// 完全保存之后
		/// </summary>
		/// <param name="ds"></param>
		protected override void AfterDsSave(DataSet ds)
		{
			if (ds.ExtendedProperties["IC_MODIFY_REM"] == null)
			{
				if (this._alOsNo.Count > 0)
				{
					DrpSO _so = new DrpSO();
					_so.UpdateQtyIc(this._alOsNo,this._alOsItm,_alPrdNo,_alUnit,this._alQty);
				}
                //如果CFM_QTY发生变化，根据没有更新的表身更新库存
                if (!_isRunAuditing && ds.Tables["MF_IC"].Rows.Count > 0 && m_UpdateWhByCfmSw)
                {
                    string _usr = ds.Tables["MF_IC"].Rows[0]["USR"].ToString();
                    string _icId = ds.Tables["MF_IC"].Rows[0]["IC_ID"].ToString();
                    string _icNo = ds.Tables["MF_IC"].Rows[0]["IC_NO"].ToString();
                    DataSet _ds = this.GetData(_usr, _icId, _icNo, false);
                    if (_ds.Tables["MF_IC"].Rows.Count > 0)
                    {
                        _ds.Tables["MF_IC"].Rows[0]["CFM_SW"] = m_CfmSwOrg;
                        _ds.AcceptChanges();
                        _ds.Tables["MF_IC"].Rows[0]["CFM_SW"] = m_CfmSwOrg == "T" ? "F" : "T";
                        foreach (DataRow dr in _ds.Tables["TF_IC"].Rows)
                        {
                            if (!m_UpdateList.ContainsKey(dr["KEY_ITM"].ToString()))
                            {
                                this.UpdateWh(dr, false);
                                this.UpdateWh(dr, true);
                            }
                        }
                        StringBuilder _sb = new StringBuilder();
                        foreach (DataRow dr in _ds.Tables["TF_IC_BAR"].Rows)
                        {
                            if (!String.IsNullOrEmpty(_sb.ToString()))
                            {
                                _sb.Append(",");
                            }
                            _sb.Append("'" + dr["BAR_CODE"].ToString() + "'");
                        }
                        BarCode _barCode = new BarCode();
                        try
                        {
                            _barCode.UpdatePhFlag(_sb.ToString(), m_CfmSwOrg != "T");
                        }
                        catch (Exception _ex)
                        {
                            throw _ex;
                        }
                    }
                }
			}
		}

		#region 审核流程
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
		/// 审核通过
		/// </summary>
		/// <param name="bil_id">单据识别码</param>
		/// <param name="bil_no">单据号码</param>
		/// <param name="chk_man">审核人</param>
		/// <param name="cls_dd">审核日期</param>
		/// <returns>错误信息</returns>
		public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
		{
			string _error = "";
			try
			{
				SunlikeDataSet _ds = this.GetData(chk_man,bil_id,bil_no,false);
				_ds.Tables["TF_IC_BAR"].TableName = "TF_IC1";
				_ds.Tables["TF_IC_BOX"].TableName = "TF_IC2";
				DataRow _drHead = _ds.Tables["MF_IC"].Rows[0];
				DataTable _dtBody = _ds.Tables["TF_IC"];
				DataTable _dtBar = _ds.Tables["TF_IC1"];
				DataTable _dtBox = _ds.Tables["TF_IC2"];
				//更新SARP
				this.UpdateSarp(_drHead,true);
                //更新凭证
                string _vohNo = this.UpdateVohNo(_drHead, StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);
				//更新序列号记录
				DataTable _dtBarCopy = _dtBar.Copy();
				_dtBar.Clear();
				for (int i=0;i<_dtBarCopy.Rows.Count;i++)
				{
					DataRow _drBar = _dtBar.NewRow();
					for (int j=0;j<_dtBarCopy.Columns.Count;j++)
					{
						_drBar[j] = _dtBarCopy.Rows[i][j];
					}
					_dtBar.Rows.Add(_drBar);
				}
				_dtBarCopy.Dispose();
				GC.Collect(GC.GetGeneration(_dtBarCopy));
				this._auditBarCode = true;
				this.UpdateBarCode(_ds, false);
				//更新退货费用
                if (!string.IsNullOrEmpty(_drHead["EP_NO"].ToString()))
                {
                    MonEX _exp = new MonEX();
                    _error = _exp.Approve(_drHead["EP_ID"].ToString(), _drHead["EP_NO"].ToString(), chk_man, cls_dd);
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException(_error);
                    }
                }
				//修改分仓存量、受订单配送数量
				for (int i=0;i<_dtBody.Rows.Count;i++)
				{
					this.UpdateWh(_dtBody.Rows[i],true);
					this.UpdateBilNo(_dtBody.Rows[i],true);
				}
				if (this._alOsNo.Count > 0)
				{
					DrpSO _so = new DrpSO();
					_so.UpdateQtyIc(this._alOsNo,this._alOsItm,_alPrdNo,_alUnit,this._alQty);
				}
				//修改箱数量、受订单箱数量
				for (int i=0;i<_dtBox.Rows.Count;i++)
				{
					this.UpdateBoxWh(_dtBox.Rows[i],true);
					string _bilID = _dtBox.Rows[i]["BIL_ID"].ToString();
					string _bilNo = _dtBox.Rows[i]["BIL_NO"].ToString();
					string _bilItm = _dtBox.Rows[i]["BIL_ITM"].ToString();
					if (!String.IsNullOrEmpty(_bilNo) && _bilID == "SO" && !String.IsNullOrEmpty(_bilItm))
					{
						decimal _qty = 0;
						DrpSO _so = new DrpSO();
						if (!String.IsNullOrEmpty(_dtBox.Rows[i]["QTY"].ToString()))
						{
							_qty = Convert.ToDecimal(_dtBox.Rows[i]["QTY"]);
						}
						_so.UpdateBoxQty(_bilNo,_bilItm,_qty);
					}
				}
				//设定审核人
				DbDRPIO _io = new DbDRPIO(Comp.Conn_DB);
				_io.UpdateChkMan(bil_id,bil_no,chk_man,cls_dd);
			}
			catch(Exception _ex)
			{
				_error = _ex.Message;
			}
			return _error;
		}

		/// <summary>
		/// 审核错误回退
		/// </summary>
		/// <param name="bil_id">单据识别码</param>
		/// <param name="bil_no">单据号码</param>
		/// <returns>错误信息</returns>
		public string RollBack(string bil_id, string bil_no)
		{
			return this.RollBack(bil_id, bil_no, true);
		}

		/// <summary>
		/// 反审核
		/// </summary>
		/// <param name="bil_id">单据识别码</param>
		/// <param name="bil_no">单据号码</param>
		/// <param name="isUpdateHead">更新表头审核信息</param>
		/// <returns></returns>
		public string RollBack(string bil_id, string bil_no, bool isUpdateHead)
		{
			string _error = "";
			try
			{
				SunlikeDataSet _ds = this.GetData("",bil_id,bil_no,false);
				_ds.Tables["TF_IC_BAR"].TableName = "TF_IC1";
				_ds.Tables["TF_IC_BOX"].TableName = "TF_IC2";
				DataRow _drHead = _ds.Tables["MF_IC"].Rows[0];
				DataTable _dtBody = _ds.Tables["TF_IC"];
				DataTable _dtBar = _ds.Tables["TF_IC1"];
				DataTable _dtBox = _ds.Tables["TF_IC2"];
				this.SetCanModify(_ds,"",false,true);
				if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
				{
					throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.NOTALLOW");//此单据不能修改，无法反审核！
				}
				//修改箱数量、受订单箱数量
				for (int i=0;i<_dtBox.Rows.Count;i++)
				{
					this.UpdateBoxWh(_dtBox.Rows[i],false);
					string _bilID = _dtBox.Rows[i]["BIL_ID"].ToString();
					string _bilNo = _dtBox.Rows[i]["BIL_NO"].ToString();
					string _bilItm = _dtBox.Rows[i]["BIL_ITM"].ToString();
					if (!String.IsNullOrEmpty(_bilNo) && _bilID == "SO" && !String.IsNullOrEmpty(_bilItm))
					{
						decimal _qty = 0;
						DrpSO _so = new DrpSO();
						if (!String.IsNullOrEmpty(_dtBox.Rows[i]["QTY"].ToString()))
						{
							_qty = Convert.ToDecimal(_dtBox.Rows[i]["QTY"]);
						}
						_qty *= -1;
						_so.UpdateBoxQty(_bilNo,_bilItm,_qty);
					}
				}
				//修改分仓存量、受订单配送数量
				for (int i=0;i<_dtBody.Rows.Count;i++)
				{
					this.UpdateWh(_dtBody.Rows[i],false);
					this.UpdateBilNo(_dtBody.Rows[i],false);
				}
				if (this._alOsNo.Count > 0)
				{
					DrpSO _so = new DrpSO();
					_so.UpdateQtyIc(this._alOsNo,this._alOsItm,_alPrdNo,_alUnit,this._alQty);
				}
				//更新序列号记录
				for (int i=0;i<_dtBar.Rows.Count;i++)
				{
					_dtBar.Rows[i].Delete();
				}
				this._auditBarCode = true;
				this.UpdateBarCode(_ds, true);
				//更新退货费用
                if (!string.IsNullOrEmpty(_drHead["EP_NO"].ToString()))
                {
                    MonEX _exp = new MonEX();
                    _error = _exp.RollBack(_drHead["EP_ID"].ToString(), _drHead["EP_NO"].ToString());
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException(_error);
                    }
                }
				DbDRPIO _io = new DbDRPIO(Comp.Conn_DB);
				//设定审核人
				if (isUpdateHead)
				{
					_io.UpdateChkMan(bil_id,bil_no,"",DateTime.Now);
				}
				//更新SARP
				_drHead.Delete();
				this.UpdateSarp(_drHead,true);
                //更新凭证
                this.UpdateVohNo(_drHead, StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");
			}
			catch(Exception _ex)
			{
				_error = _ex.Message;
			}
			return _error;
		}
		#endregion

		private void UpdateWh(DataRow dr,bool IsAdd)
		{
			string _batNo = "";
			string _batNo2 = "";
			string _validDd = "";
			string _prdNo = "";
			string _prdMark = "";
			string _whNo1 = "";
            string _whNo2 = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
			string _ut = "";
			decimal _cst = 0;
			decimal _amtnNet = 0;
            string _cfmSw = "";
			if (IsAdd)
			{
				_batNo = dr["BAT_NO"].ToString();
				_batNo2 = dr["BAT_NO2"].ToString();
				if (!String.IsNullOrEmpty(dr["VALID_DD"].ToString()))
				{
					_validDd = Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat);
				}
				_prdNo = dr["PRD_NO"].ToString();
				_prdMark = dr["PRD_MARK"].ToString();
				_whNo1 = dr["WH1"].ToString();
				_whNo2 = dr["WH2"].ToString();
                _ut = dr["UNIT"].ToString();
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1"].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1"]);
                }
				if (!String.IsNullOrEmpty(dr["CST"].ToString()))
				{
					_cst = Convert.ToDecimal(dr["CST"]);
				}
				if (!String.IsNullOrEmpty(dr["AMTN_NET"].ToString()))
				{
					_amtnNet = Convert.ToDecimal(dr["AMTN_NET"]);
				}
                _cfmSw = dr.Table.DataSet.Tables["MF_IC"].Rows[0]["CFM_SW"].ToString();
			}
			else
			{
				_batNo = dr["BAT_NO",DataRowVersion.Original].ToString();
				_batNo2 = dr["BAT_NO2",DataRowVersion.Original].ToString();
				if (!String.IsNullOrEmpty(dr["VALID_DD",DataRowVersion.Original].ToString()))
				{
					_validDd = Convert.ToDateTime(dr["VALID_DD",DataRowVersion.Original]).ToString(Comp.SQLDateFormat);
				}
				_prdNo = dr["PRD_NO",DataRowVersion.Original].ToString();
				_prdMark = dr["PRD_MARK",DataRowVersion.Original].ToString();
				_whNo1 = dr["WH1",DataRowVersion.Original].ToString();
				_whNo2 = dr["WH2",DataRowVersion.Original].ToString();
                _ut = dr["UNIT", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                }
                _qty *= -1;
                _qty1 *= -1;
				if (!String.IsNullOrEmpty(dr["CST",DataRowVersion.Original].ToString()))
				{
					_cst = Convert.ToDecimal(dr["CST",DataRowVersion.Original]);
				}
				_cst *= -1;
				if (!String.IsNullOrEmpty(dr["AMTN_NET",DataRowVersion.Original].ToString()))
				{
					_amtnNet = Convert.ToDecimal(dr["AMTN_NET",DataRowVersion.Original]);
				}
                _amtnNet *= -1;
                _cfmSw = dr.Table.DataSet.Tables["MF_IC"].Rows[0]["CFM_SW", DataRowVersion.Original].ToString();
			}
			WH _wh = new WH();
			System.Collections.Hashtable _ht1 = new System.Collections.Hashtable();

			System.Collections.Hashtable _ht2 = new System.Collections.Hashtable();
			if (!String.IsNullOrEmpty(_batNo))
            {
                _ht1[WH.QtyTypes.QTY_OUT] = _qty;
                _ht1[WH.QtyTypes.QTY1_OUT] = _qty1;
				_ht1[WH.QtyTypes.CST] = _amtnNet * -1;
                if (_cfmSw == "T")
                {
                    _ht2[WH.QtyTypes.QTY_ON_WAY] = _qty;
                }
                else
                {
                    _ht2[WH.QtyTypes.QTY_IN] = _qty;
                    _ht2[WH.QtyTypes.QTY1_IN] = _qty1;
                    _ht2[WH.QtyTypes.CST] = _amtnNet;
                }

				_wh.UpdateQty(_batNo,_prdNo,_prdMark,_whNo1,
					"",_ut,_ht1);
				Prdt _prdt = new Prdt();
				SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo2);
				if (!String.IsNullOrEmpty(_validDd))
				{
					if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
					{
						TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
						if (_timeSpan.Days > 0)
						{
							_wh.UpdateQty(_batNo2,_prdNo,_prdMark,_whNo2,
								"",_ut,_ht2);
						}
						else
						{
							_wh.UpdateQty(_batNo2,_prdNo,_prdMark,_whNo2,
								_validDd,_ut,_ht2);
						}
					}
					else
					{
						_wh.UpdateQty(_batNo2,_prdNo,_prdMark,_whNo2,
							_validDd,_ut,_ht2);
					}
				}
				else
				{
					_wh.UpdateQty(_batNo2,_prdNo,_prdMark,_whNo2,
						"",_ut,_ht2);
				}
			}
			else
            {
                _ht1[WH.QtyTypes.QTY] = _qty * (-1);
                _ht1[WH.QtyTypes.QTY1] = _qty1 * (-1);
                _ht1[WH.QtyTypes.AMT_CST] = _cst * (-1);
                if (_cfmSw == "T")
                {
                    _ht2[WH.QtyTypes.QTY_ON_WAY] = _qty;
                }
                else
                {
                    _ht2[WH.QtyTypes.QTY] = _qty;
                    _ht2[WH.QtyTypes.QTY1] = _qty1;
                    _ht2[WH.QtyTypes.AMT_CST] = _amtnNet;
                }

				_wh.UpdateQty(_prdNo,_prdMark,_whNo1,_ut,_ht1);
				_wh.UpdateQty(_prdNo,_prdMark,_whNo2,_ut,_ht2);
			}
		}

        private void UpdateWhClose(DataRow dr, bool isClose)
        {
            string _batNo2 = dr["BAT_NO2"].ToString();
            string _prdNo = dr["PRD_NO"].ToString();
            string _prdMark = dr["PRD_MARK"].ToString();
            string _whNo2 = dr["WH2"].ToString();
            decimal _qty = Convert.ToDecimal(dr["QTY"]);
            if (!String.IsNullOrEmpty(dr["QTY_CFM"].ToString()))
            {
                _qty -= Convert.ToDecimal(dr["QTY_CFM"]);
            }
            if (!String.IsNullOrEmpty(dr["QTY_LOST"].ToString()))
            {
                _qty -= Convert.ToDecimal(dr["QTY_LOST"]);
            }
            if (isClose)
            {
                _qty *= -1;
            }
            string _ut = dr["UNIT"].ToString();
            if (_qty != 0)
            {
                WH _wh = new WH();
                System.Collections.Hashtable _ht = new System.Collections.Hashtable();
                if (!String.IsNullOrEmpty(_batNo2))
                {
                    _ht[WH.QtyTypes.QTY_ON_WAY] = _qty;
                    _wh.UpdateQty(_batNo2, _prdNo, _prdMark, _whNo2,
                        "", _ut, _ht);
                }
                else
                {
                    _ht[WH.QtyTypes.QTY_ON_WAY] = _qty;
                    _wh.UpdateQty(_prdNo, _prdMark, _whNo2,
                        _ut, _ht);
                }
            }
        }

		private void UpdateBoxWh(DataRow dr,bool IsAdd)
		{
			string _prdNo = "";
			string _content = "";
			string _whNo1 = "";
			string _whNo2 = "";
			decimal _qty = 0;
			if (IsAdd)
			{
				_prdNo = dr["PRD_NO"].ToString();
				_content = dr["CONTENT"].ToString();
				_whNo1 = dr["WH1"].ToString();
				_whNo2 = dr["WH2"].ToString();
				if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
				{
					_qty = Convert.ToDecimal(dr["QTY"]);
				}
			}
			else
			{
				_prdNo = dr["PRD_NO",DataRowVersion.Original].ToString();
				_content = dr["CONTENT",DataRowVersion.Original].ToString();
				_whNo1 = dr["WH1",DataRowVersion.Original].ToString();
				_whNo2 = dr["WH2",DataRowVersion.Original].ToString();
				if (!String.IsNullOrEmpty(dr["QTY",DataRowVersion.Original].ToString()))
				{
					_qty = Convert.ToDecimal(dr["QTY",DataRowVersion.Original]);
				}
				_qty *= -1;
				
			}
			WH _wh = new WH();
			_wh.UpdateBoxQty(_prdNo,_whNo1,_content,WH.BoxQtyTypes.QTY,_qty * (-1) );
			_wh.UpdateBoxQty(_prdNo,_whNo2,_content,WH.BoxQtyTypes.QTY,_qty);
		}

		private void UpdateBarCode(DataSet ChangedDS, bool isRollBack)
		{
			DataRow _drHead = ChangedDS.Tables["MF_IC"].Rows[0];
			DataTable _dtBody = ChangedDS.Tables["TF_IC"];
			//查找表身有修改过库位的记录
			System.Collections.Hashtable _htKeyItm = new System.Collections.Hashtable();
			for (int i=0;i<_dtBody.Rows.Count;i++)
			{
				if (_dtBody.Rows[i].RowState == DataRowState.Modified
					&& (_dtBody.Rows[i]["WH1"].ToString() != _dtBody.Rows[i]["WH1",DataRowVersion.Original].ToString()
					|| _dtBody.Rows[i]["WH2"].ToString() != _dtBody.Rows[i]["WH2",DataRowVersion.Original].ToString()
					|| _dtBody.Rows[i]["BAT_NO"].ToString() != _dtBody.Rows[i]["BAT_NO",DataRowVersion.Original].ToString()))
				{
					_htKeyItm[_dtBody.Rows[i]["KEY_ITM"].ToString()] = "";
				}
			}
			//查找BAR_CODE
			DataTable _dtBarCode;
			if ((_drHead.RowState == DataRowState.Modified || _drHead.RowState == DataRowState.Unchanged) && !this._auditBarCode)
			{
				string _sql = "select IC_ID,IC_NO,IC_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO from TF_IC1"
					+ " where IC_ID='" + _drHead["IC_ID"].ToString() + "' and IC_NO='" + _drHead["IC_NO"].ToString() + "'";
				Query _query = new Query();
				SunlikeDataSet _dsQuery = _query.DoSQLString(_sql);
				_dsQuery.Tables[0].TableName = "TF_IC1";
				_dsQuery.Merge(ChangedDS.Tables["TF_IC1"],false,MissingSchemaAction.AddWithKey);
				_dtBarCode = _dsQuery.Tables["TF_IC1"];
			}
			else
			{
				_dtBarCode = ChangedDS.Tables["TF_IC1"];
			}
			System.Text.StringBuilder _sb = new System.Text.StringBuilder();
			for (int i=0;i<_dtBarCode.Rows.Count;i++)
			{
				if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["IC_ITM"].ToString()] != null)
				{
					if (_sb.Length > 0)
					{
						_sb.Append(",");
					}
					_sb.Append("'");
					if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
					{
						_sb.Append(_dtBarCode.Rows[i]["BAR_CODE",DataRowVersion.Original].ToString());
					}
					else
					{
						_sb.Append(_dtBarCode.Rows[i]["BAR_CODE"].ToString());
					}
					_sb.Append("'");
				}
			}
			//有新增/修改过BAR_CODE，则修改BAR_REC表
			if (_sb.Length > 0)
			{
				_sb.Insert(0,"BAR_NO in (");
				_sb.Append(")");
				//把条件语句分段
				System.Collections.ArrayList _alBar = new System.Collections.ArrayList();
				int _maxWhereLength = 10240;
				string _subWhere;
				int _pos;
				string _where = _sb.ToString();
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
				int _splitCount = 20;
				BarCode _bar = new BarCode();
				DataSet _dsBar = new DataSet();
				try
				{
					int j=0;
					int k=0;
					Hashtable _ht = new Hashtable();
					DataSet _dsTemp = null;
					while (j < _alBar.Count)
					{
						_dsTemp = new DataSet();
						int _end = j + _splitCount;
						if (_end >= _alBar.Count - 1)
						{
							_end = _alBar.Count;
						}
						for (;j<_end;j++)
						{
							using (DataTable dataTableBarRec = _bar.GetBarRecord(_alBar[j].ToString(),true))
							{
								if (_dsTemp.Tables["BAR_REC"] == null)
								{
									_dsTemp.Tables.Add(dataTableBarRec.Copy());
								}
								else
								{
									_dsTemp.Merge(dataTableBarRec,true,MissingSchemaAction.AddWithKey);
								}
							}
						}
						_ht["DataSet"+k.ToString()] = _dsTemp;
						k++;
					}
					IDictionaryEnumerator _ide = _ht.GetEnumerator();
					while (_ide.MoveNext())
					{
						_dsBar.Merge((DataSet)_ide.Value, true, MissingSchemaAction.AddWithKey);
					}
				}
				catch (Exception _ex)
				{
					throw _ex;
				}
				DataTable _dtBarRec = _dsBar.Tables["BAR_REC"];
				string[] _aryBoxNo = new string[_dtBarCode.Rows.Count];
				//取表头资料
				string _cusNo,_bilID,_bilNo,_usr;
				DateTime _bilDd = DateTime.Today;
                bool _cfmSw;
				if (_drHead.RowState == DataRowState.Deleted)
				{
					_cusNo = _drHead["CUS_NO1",DataRowVersion.Original].ToString();
					_bilID = _drHead["IC_ID",DataRowVersion.Original].ToString();
					_bilNo = _drHead["IC_NO",DataRowVersion.Original].ToString();
					_bilDd = Convert.ToDateTime(_drHead["IC_DD",DataRowVersion.Original]);
					_usr = _drHead["USR",DataRowVersion.Original].ToString();
                    _cfmSw = _drHead["CFM_SW", DataRowVersion.Original].ToString() == "T";
				}
				else
				{
					_cusNo = _drHead["CUS_NO2"].ToString();
					_bilID = _drHead["IC_ID"].ToString();
					_bilNo = _drHead["IC_NO"].ToString();
					_bilDd = Convert.ToDateTime(_drHead["IC_DD"]);
                    _usr = _drHead["USR"].ToString();
                    _cfmSw = _drHead["CFM_SW"].ToString() == "T";
				}
				//取得该单据做过的change
				DataTable _dtBarChange = null;
				if (_drHead.RowState != DataRowState.Added)
				{
					_dtBarChange = _bar.GetBarChange(_bilID,_bilNo);
				}
				//更新BAR_REC表
				System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
				System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
				System.Collections.ArrayList _alBoxNo = new System.Collections.ArrayList();
				System.Collections.ArrayList _alWhNo = new System.Collections.ArrayList();
				System.Collections.ArrayList _alStop = new System.Collections.ArrayList();
				System.Text.StringBuilder _sbChange = new System.Text.StringBuilder();
				for (int i=0;i<_dtBarCode.Rows.Count;i++)
				{
					if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["IC_ITM"].ToString()] != null)
					{
						string _barCode,_boxNo,_keyItm,_whNo1,_whNo2,_batNo;
						string _oldWhNo1 = "";
						string _oldWhNo2 = "";
						string _oldBatNo = "";
						DataRow[] _dra;
						if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
						{
							_barCode = _dtBarCode.Rows[i]["BAR_CODE",DataRowVersion.Original].ToString();
							_boxNo = _dtBarCode.Rows[i]["BOX_NO",DataRowVersion.Original].ToString();
							_keyItm = _dtBarCode.Rows[i]["IC_ITM",DataRowVersion.Original].ToString();
							_dra = _dtBody.Select("KEY_ITM=" + _keyItm,"",DataViewRowState.CurrentRows|DataViewRowState.OriginalRows);
							if (_dra[0].RowState == DataRowState.Deleted)
							{
								_whNo1 = _dra[0]["WH1",DataRowVersion.Original].ToString();
								_whNo2 = _dra[0]["WH2",DataRowVersion.Original].ToString();
								_batNo = _dra[0]["BAT_NO",DataRowVersion.Original].ToString();
							}
							else
							{
								_whNo1 = _dra[0]["WH1"].ToString();
								_whNo2 = _dra[0]["WH2"].ToString();
								_batNo = _dra[0]["BAT_NO"].ToString();
							}
						}
						else
						{
							_barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
							_boxNo = _dtBarCode.Rows[i]["BOX_NO"].ToString();
							_keyItm = _dtBarCode.Rows[i]["IC_ITM"].ToString();
							_dra = _dtBody.Select("KEY_ITM=" + _keyItm);
							_whNo1 = _dra[0]["WH1"].ToString();
							_whNo2 = _dra[0]["WH2"].ToString();
							_batNo = _dra[0]["BAT_NO"].ToString();
							if (_dra[0].RowState == DataRowState.Modified)
							{
								_oldWhNo1 = _dra[0]["WH1",DataRowVersion.Original].ToString();
								_oldWhNo2 = _dra[0]["WH2",DataRowVersion.Original].ToString();
								_oldBatNo = _dra[0]["BAT_NO",DataRowVersion.Original].ToString();
							}
						}
						DataRow _drBarRec = _dtBarRec.Rows.Find(new string[1]{_barCode});
						if (_drBarRec != null)
						{
                            //根据装箱日期和用户设定，判断是否拆箱
							bool _canUpdate = true;
							Sunlike.Business.UserProperty _userProp = new UserProperty();
							string _pgm = "DRP"+_bilID;
							if (!String.IsNullOrEmpty(_drBarRec["BOX_NO"].ToString()) && _userProp.GetData(_usr, _pgm, "CONTROL_BARCODE") != "0"
								&& Comp.BarcodeUpdCheck && Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "F")
							{
								if (!String.IsNullOrEmpty(_drBarRec["UPDDATE"].ToString()))
								{
									DateTime _barDd = Convert.ToDateTime(_drBarRec["UPDDATE"]);
									TimeSpan _timeSpan = _barDd.Subtract(_bilDd);
									if (_timeSpan.TotalMilliseconds > 0)
									{
										_canUpdate = false;
									}
								}
							}
							if (_canUpdate)
							{
								//如果出货库位与序列号所在的库位不同，则要写入BAR_CHANGE
								if (_dtBarCode.Rows[i].RowState == DataRowState.Added)
								{
                                    if (_drBarRec["WH"].ToString() != _whNo1 || _drBarRec["BAT_NO"].ToString() != _batNo || _drBarRec["PH_FLAG"].ToString() == "T")
									{
                                        _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo1, _bilID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
									}
								}
								else if (_dtBarCode.Rows[i].RowState == DataRowState.Unchanged)
								{
									if (_oldWhNo2 != _drBarRec["WH"].ToString() || _oldBatNo != _drBarRec["BAT_NO"].ToString() || _drBarRec["STOP_ID"].ToString() == "T")
									{
										_sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM="+_barCode);//序列号_barCode已不在当前库位，不允许删除！
									}
									else
									{
										DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
										if (_draBarChange.Length > 0)
										{
											string _changeWh1 = _draBarChange[0]["WH1"].ToString();
											string _changeBatNo1 = _draBarChange[0]["BAT_NO1"].ToString();
											string _changePhFlag1 = _draBarChange[0]["PH_FLAG1"].ToString();
											_sbChange.Append(_bar.DeleteBarChange(_barCode,_bilID,_bilNo,true));
                                            if (_changeWh1 != _whNo1 || _changeBatNo1 != _batNo || _changePhFlag1 == "T")
											{
                                                _sbChange.Append(_bar.InsertBarChange(_barCode, _changeWh1, _whNo1, _bilID, _bilNo, _usr, _changeBatNo1, _batNo, _changePhFlag1, "F", true));
											}
										}
                                        else if (_whNo1 != _oldWhNo1 || _batNo != _oldBatNo || _drBarRec["PH_FLAG"].ToString() == "T")
										{
                                            _sbChange.Append(_bar.InsertBarChange(_barCode, _oldWhNo1, _whNo1, _bilID, _bilNo, _usr, _oldBatNo, _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
										}
									}
								}
								_drBarRec["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
								//如果有做过bar_change，则库位回到做bar_change之前的地方，且把bar_change的记录删掉
								if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
								{
									//BAR_REC.WH必须和单据库位一致，且BAR_REC.STOP_ID='F'，否则不允许删除
									if (_whNo2 == _drBarRec["WH"].ToString() && _batNo == _drBarRec["BAT_NO"].ToString() && _drBarRec["STOP_ID"].ToString() != "T")
									{
										DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
										if (_draBarChange.Length > 0)
										{
                                            _drBarRec["WH"] = _draBarChange[0]["WH1"];
                                            _drBarRec["BAT_NO"] = _draBarChange[0]["BAT_NO1"];
                                            //到货确认标记
                                            _drBarRec["PH_FLAG"] = _draBarChange[0]["PH_FLAG1"];
											_sbChange.Append(_bar.DeleteBarChange(_barCode,_bilID,_bilNo,true));
										}
										else
										{
                                            _drBarRec["WH"] = _whNo1;
                                            _drBarRec["BAT_NO"] = _batNo;
                                            _drBarRec["PH_FLAG"] = "F";
										}
									}
									else
									{
										_sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM="+_barCode);//序列号_barCode已不在当前库位，不允许删除！
									}
								}
								else
								{
                                    _drBarRec["WH"] = _whNo2;
                                    _drBarRec["BAT_NO"] = _batNo;
                                    _drBarRec["PH_FLAG"] = _cfmSw ? "T" : "F";
								}
								//如果B0X_NO不一样，则要做自动拆箱
								if (_drBarRec["BOX_NO"].ToString() != _boxNo)
								{
									if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
									{
										_aryBoxNo[i] = _drBarRec["BOX_NO"].ToString().Trim();
									}
									else
									{
										if (!String.IsNullOrEmpty(_sbError.ToString()))
										{
											_sbError.Append(";");
										}
										_sbError.Append("RCID=COMMON.HINT.DOUBLEBOX,PARAM=" + _barCode);//序列号 _barCode 已经装箱，你不能拆箱录入！
									}
								}
								else if (!String.IsNullOrEmpty(_boxNo))
								{
									if (_htBoxNo[_boxNo] == null)
									{
										_htBoxNo[_boxNo] = "";
										_alBoxNo.Add(_boxNo);
									}
								}
							}
							else
							{
								_dtBarRec.Rows.Remove(_drBarRec);
							}
						}
						else if (_dtBarCode.Rows[i].RowState != DataRowState.Deleted)
						{
							DataRow _dr = _dtBarRec.NewRow();
							_dr["BAR_NO"] = _barCode;
							_dr["WH"] = _whNo2;
							_dr["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
							_dr["STOP_ID"] = "F";
							_dr["PRD_NO"] = _barCode.Substring(BarCode.BarRole.SPrdt,BarCode.BarRole.EPrdt - BarCode.BarRole.SPrdt + 1).Replace(BarCode.BarRole.TrimChar,"");
							if (!(BarCode.BarRole.BPMark == BarCode.BarRole.EPMark && BarCode.BarRole.EPMark == 0))
							{
								_dr["PRD_MARK"] = _barCode.Substring(BarCode.BarRole.BPMark,BarCode.BarRole.EPMark - BarCode.BarRole.BPMark + 1);
							}
							_dr["BAT_NO"] = _batNo;
							_dtBarRec.Rows.Add(_dr);

                            //序列号不存在，写BAR_CHANGE
                            _sbChange.Append(_bar.InsertBarChange(_barCode, "", _whNo1, _bilID, _bilNo, _usr, "", _batNo, "", "", true));
						}
					}
				}
				if (String.IsNullOrEmpty(_sbError.ToString()))
				{
					//写入序列号变更记录
					if (_sbChange.Length > 0)
					{
						Query _query = new Query();
						string _sql = _sbChange.ToString();
						int _maxSqlLength = 10240;
						while (true)
						{
							if (_sql.Length > _maxSqlLength)
							{
								string _subSql = _sql.Substring(0,_maxSqlLength-1);
								_pos = _subSql.LastIndexOf("\n");
								_subSql = _subSql.Substring(0,_pos+1);
								_sql = _sql.Substring(_pos+1,_sql.Length-_pos-1);
								_query.RunSql(_subSql);
							}
							else
							{
								_query.RunSql(_sql);
								break;
							}
						}
					}
                    //if (_drHead.RowState != DataRowState.Deleted && !isRollBack)
                    //{
                    //    //到货确认标记
                    //    _dtBarRec.DataSet.ExtendedProperties["IC_CFM_SW"] = _cfmSw.ToString().ToUpper().Substring(0, 1);
                    //}
					//修改BAR_REC
					DataTable _dtError = _bar.UpdateRec(_dtBarRec.DataSet);
					if (_dtError.Rows.Count > 0)
					{
						throw new SunlikeException("RCID=COMMON.HINT.BOXERROR");//更新序列号记录失败！
					}
					_alWhNo.Clear();
					_alStop.Clear();
					foreach (string boxNo in _alBoxNo)
					{
						DataRow[] _draBoxInfo = _dtBarRec.Select("BOX_NO='" + boxNo + "'");
						if (_draBoxInfo.Length > 0)
						{
							_alWhNo.Add(_draBoxInfo[0]["WH"].ToString());
							_alStop.Add(_draBoxInfo[0]["STOP_ID"].ToString());
						}
						else
						{
							_alWhNo.Add(null);
							_alStop.Add(null);
						}
					}
					//修改BAR_BOX
					_bar.UpdateBarBox(_alBoxNo,_alWhNo,_alStop);
					//拆箱
					_bar.DeleteBox(_aryBoxNo,_usr,_bilID,_bilNo);
				}
				else
				{
					throw new SunlikeException(_sbError.ToString());
				}
			}
		}

		private void UpdateBilNo(DataRow dr,bool IsAdd)
		{
			string _icID = "";
			string _bilNo = "";
			string _prdNo = "";
			string _unit = "";
			int _bilItm = 0;
			decimal _qty = 0;
			if (IsAdd)
			{
				_icID = dr["IC_ID"].ToString();
				_bilNo = dr["BIL_NO"].ToString();
				_prdNo = dr["PRD_NO"].ToString();
				_unit = dr["UNIT"].ToString();
				if (!String.IsNullOrEmpty(dr["BIL_ITM"].ToString()))
				{
					_bilItm = Convert.ToInt32(dr["BIL_ITM"]);
				}
				_qty = Convert.ToDecimal(dr["QTY"]);
			}
			else
			{
				_icID = dr["IC_ID",DataRowVersion.Original].ToString();
				_bilNo = dr["BIL_NO",DataRowVersion.Original].ToString();
				_prdNo = dr["PRD_NO",DataRowVersion.Original].ToString();
				_unit = dr["UNIT",DataRowVersion.Original].ToString();
				if (!String.IsNullOrEmpty(dr["BIL_ITM",DataRowVersion.Original].ToString()))
				{
					_bilItm = Convert.ToInt32(dr["BIL_ITM",DataRowVersion.Original]);
				}
				_qty = Convert.ToDecimal(dr["QTY",DataRowVersion.Original]);
				_qty *= -1;
			}
			if (!String.IsNullOrEmpty(_bilNo) && _bilItm > 0)
			{
				if (_icID == "IO" || _icID == "IM")
				{
					this._alOsNo.Add(_bilNo);
					this._alOsItm.Add(_bilItm);
					this._alPrdNo.Add(_prdNo);
					this._alUnit.Add(_unit);
					this._alQty.Add(_qty);
				}
				else if (_icID == "IB")
				{
					DRPYI _yi = new DRPYI();
					_yi.UpdateQtyRtn(_bilNo,_bilItm,_qty);
				}
			}
		}

		private void UpdateSarp(DataRow dr,bool isAuditing)
		{
			DataRow _dr = dr.Table.DataSet.Tables["MF_IC"].Rows[0];
			DataTable _dtBody = _dr.Table.DataSet.Tables["TF_IC"];
			int _year,_month;
            string _icID, _cusNo1, _cusNo1Old, _cusNo2, _cusNo2Old;
			if (_dr.RowState == DataRowState.Deleted)
			{
				_year = Convert.ToDateTime(_dr["IC_DD",DataRowVersion.Original]).Year;
				_month = Convert.ToDateTime(_dr["IC_DD",DataRowVersion.Original]).Month;
				_icID = _dr["IC_ID",DataRowVersion.Original].ToString();
				_cusNo1 = _dr["CUS_NO1",DataRowVersion.Original].ToString();
                _cusNo2 = _dr["CUS_NO2", DataRowVersion.Original].ToString();
                _cusNo1Old = _cusNo1;
                _cusNo2Old = _cusNo2;
			}
			else
			{
				_year = Convert.ToDateTime(_dr["IC_DD"]).Year;
				_month = Convert.ToDateTime(_dr["IC_DD"]).Month;
				_icID = _dr["IC_ID"].ToString();
				_cusNo1 = _dr["CUS_NO1"].ToString();
				_cusNo2 = _dr["CUS_NO2"].ToString();
                if (_dr.RowState == DataRowState.Added)
                {
                    _cusNo1Old = _cusNo1;
                    _cusNo2Old = _cusNo2;
                }
                else
                {
                    _cusNo1Old = _dr["CUS_NO1", DataRowVersion.Original].ToString();
                    _cusNo2Old = _dr["CUS_NO2", DataRowVersion.Original].ToString();
                }
			}
			if (_icID != "IC")
			{
                decimal _amtn = 0;
                decimal _amtnOld = 0;
                decimal _up, _qty;
                foreach (DataRow drBody in _dtBody.Rows)
                {
                    if (drBody.RowState != DataRowState.Deleted)
                    {
                        if (String.IsNullOrEmpty(drBody["UP"].ToString()))
                        {
                            _up = 0;
                        }
                        else
                        {
                            _up = Convert.ToDecimal(drBody["UP"]);
                        }
                        if (String.IsNullOrEmpty(drBody["QTY"].ToString()))
                        {
                            _qty = 0;
                        }
                        else
                        {
                            _qty = Convert.ToDecimal(drBody["QTY"]);
                        }
                        _amtn += _up * _qty;
                    }
                    if (drBody.RowState != DataRowState.Added)
                    {
                        if (String.IsNullOrEmpty(drBody["UP", DataRowVersion.Original].ToString()))
                        {
                            _up = 0;
                        }
                        else
                        {
                            _up = Convert.ToDecimal(drBody["UP", DataRowVersion.Original]);
                        }
                        if (String.IsNullOrEmpty(drBody["QTY", DataRowVersion.Original].ToString()))
                        {
                            _qty = 0;
                        }
                        else
                        {
                            _qty = Convert.ToDecimal(drBody["QTY", DataRowVersion.Original]);
                        }
                        _amtnOld += _up * _qty;
                    }
                }
				Arp _arp = new Arp();
				Cust _cust =new Cust();
				if (_icID == "IO")
				{
                    if (_cust.IsDrp_id(_cusNo2Old) && _amtnOld != 0)
                    {
                        _arp.UpdateSarp("1", _year, _cusNo2Old, _month, "", "AMTN_INV", _amtnOld * -1);
                    }
                    if (_cust.IsDrp_id(_cusNo2) && _amtn != 0)
                    {
                        _arp.UpdateSarp("1", _year, _cusNo2, _month, "", "AMTN_INV", _amtn);
                    }
				}
				else if (_icID == "IB")
				{
                    if (_cust.IsDrp_id(_cusNo1Old) && _amtnOld != 0)
                    {
                        _arp.UpdateSarp("1", _year, _cusNo1Old, _month, "", "AMTN_INV", _amtnOld);
                    }
                    if (_cust.IsDrp_id(_cusNo1) && _amtn != 0)
                    {
                        _arp.UpdateSarp("1", _year, _cusNo1, _month, "", "AMTN_INV", _amtn * -1);
                    }
				}
				else if (_icID == "IM")
				{
                    if (_cust.IsDrp_id(_cusNo2Old) && _amtnOld != 0)
                    {
                        _arp.UpdateSarp("1", _year, _cusNo2Old, _month, "", "AMTN_INV", _amtnOld * -1);
                    }
                    if (_cust.IsDrp_id(_cusNo2) && _amtn != 0)
                    {
                        _arp.UpdateSarp("1", _year, _cusNo2, _month, "", "AMTN_INV", _amtn);
                    }
                    if (_cust.IsDrp_id(_cusNo1Old) && _amtnOld != 0)
                    {
                        _arp.UpdateSarp("1", _year, _cusNo1Old, _month, "", "AMTN_INV", _amtnOld);
                    }
                    if (_cust.IsDrp_id(_cusNo1) && _amtn != 0)
                    {
                        _arp.UpdateSarp("1", _year, _cusNo1, _month, "", "AMTN_INV", _amtn * -1);
                    }
				}
			}
		}

		private void UpdateExp(SunlikeDataSet changedDS,bool isRunAuditing)
		{
			DataRow _drHead = changedDS.Tables["MF_IC"].Rows[0];
			string _icID,_icNo,_usr,_dep,_bilType,_cusNo,_salNo,_epID,_epNo;
			DateTime _icDD;
			if (_drHead.RowState == DataRowState.Deleted)
			{
				_icID = _drHead["IC_ID",DataRowVersion.Original].ToString();
				_icNo = _drHead["IC_NO",DataRowVersion.Original].ToString();
				_icDD = Convert.ToDateTime(_drHead["IC_DD",DataRowVersion.Original]);
				_usr = _drHead["USR",DataRowVersion.Original].ToString();
				_dep = _drHead["DEP",DataRowVersion.Original].ToString();
				_bilType = _drHead["BIL_TYPE",DataRowVersion.Original].ToString();
				_cusNo = _drHead["CUS_NO1",DataRowVersion.Original].ToString();
				_salNo = _drHead["SAL_NO",DataRowVersion.Original].ToString();
				_epID = _drHead["EP_ID",DataRowVersion.Original].ToString();
				_epNo = _drHead["EP_NO",DataRowVersion.Original].ToString();
			}
			else
			{
				_icID = _drHead["IC_ID"].ToString();
				_icNo = _drHead["IC_NO"].ToString();
				_icDD = Convert.ToDateTime(_drHead["IC_DD"]);
				_usr = _drHead["USR"].ToString();
				_dep = _drHead["DEP"].ToString();
				_bilType = _drHead["BIL_TYPE"].ToString();
				_cusNo = _drHead["CUS_NO1"].ToString();
				_salNo = _drHead["SAL_NO"].ToString();
				_epID = _drHead["EP_ID"].ToString();
				_epNo = _drHead["EP_NO"].ToString();
			}
			if (_icID == "IB")
			{
				MonEX _exp = new MonEX();
				if (_drHead.RowState == DataRowState.Deleted && !String.IsNullOrEmpty(_epNo))
				{
					SunlikeDataSet _dsExp = _exp.GetData(_epID,_epNo,false);
					_dsExp.Tables["MF_EXP"].Rows[0].Delete();
					_dsExp.ExtendedProperties["SAVE_ID"] = "T";
					_exp.UpdateData(null,_dsExp,true);
				}
				else if (changedDS.Tables.Contains("IB_EXP"))
				{
					for (int i=0;i<changedDS.Tables["IB_EXP"].Rows.Count;i++)
					{
						SunlikeDataSet _dsExp = new SunlikeDataSet();
						DataRow _drMf = null;
						DataRow _drTf = null;
						if (changedDS.Tables["IB_EXP"].Rows[i].RowState == DataRowState.Added)
						{
							_dsExp = _exp.GetData(_epID,"",true);
							//增加表头
							_drMf = _dsExp.Tables["MF_EXP"].NewRow();
							_drMf["EP_ID"] = _epID;
							_drMf["EP_NO"] = _epNo;
							_drMf["EP_DD"] = _icDD;
							_drMf["USR"] = _usr;
							_dsExp.Tables["MF_EXP"].Rows.Add(_drMf);
							//增加表身
							_drTf = _dsExp.Tables["TF_EXP"].NewRow();
							_drTf["EP_ID"] = _epID;
							_drTf["EP_NO"] = _epNo;
							_drTf["ITM"] = 1;
							_dsExp.Tables["TF_EXP"].Rows.Add(_drTf);
							_dsExp.ExtendedProperties["SAVE_ID"] = (!isRunAuditing).ToString().ToUpper().Substring(0,1);
						}
						else if (changedDS.Tables["IB_EXP"].Rows[i].RowState == DataRowState.Modified)
						{
							_dsExp = _exp.GetData(_epID,_epNo,false);
							_drMf = _dsExp.Tables["MF_EXP"].Rows[0];
							_drTf = _dsExp.Tables["TF_EXP"].Rows[0];
							_dsExp.ExtendedProperties["SAVE_ID"] = "T";
						}
						else if (changedDS.Tables["IB_EXP"].Rows[i].RowState == DataRowState.Deleted)
						{
							_dsExp = _exp.GetData("ER",changedDS.Tables["IB_EXP"].Rows[i]["EP_NO",DataRowVersion.Original].ToString(),false);
							_dsExp.Tables["MF_EXP"].Rows[0].Delete();
							_dsExp.ExtendedProperties["SAVE_ID"] = "T";
							_exp.UpdateData(null,_dsExp,true);
						}
						if (_drMf != null)
						{
							//表头
							_drMf["BIL_ID"] = _icID;
							_drMf["PC_NO"] = _icNo;
							_drMf["DEP"] = _dep;
							_drMf["REM"] = changedDS.Tables["IB_EXP"].Rows[i]["REM"];
							_drMf["BIL_TYPE"] = _bilType;
							//表身
							_drTf["IDX_NO"] = changedDS.Tables["IB_EXP"].Rows[i]["IDX_NO"];
							_drTf["CUS_NO"] = _cusNo;
							_drTf["CUR_ID"] = changedDS.Tables["IB_EXP"].Rows[i]["CUR_ID"];
							_drTf["EXC_RTO"] = changedDS.Tables["IB_EXP"].Rows[i]["EXC_RTO"];
							_drTf["TAX_ID"] = changedDS.Tables["IB_EXP"].Rows[i]["TAX_ID"];
							_drTf["AMT"] = changedDS.Tables["IB_EXP"].Rows[i]["AMT"];
							_drTf["AMTN_NET"] = changedDS.Tables["IB_EXP"].Rows[i]["AMTN_NET"];
							_drTf["TAX"] = changedDS.Tables["IB_EXP"].Rows[i]["TAX"];
							if (changedDS.Tables["IB_EXP"].Rows[i]["SHARE_MTH"].ToString() == "2")
							{
								_drTf["SHARE_MTH"] = DBNull.Value;
							}
							else
							{
								_drTf["SHARE_MTH"] = changedDS.Tables["IB_EXP"].Rows[i]["SHARE_MTH"];
							}
							_drTf["DEP"] = _dep;
							_drTf["SAL_NO"] = _salNo;
							_drTf["BIL_ID"] = _icID;
							_drTf["BIL_NO"] = _icNo;
							_drTf["RTO_TAX"] = changedDS.Tables["IB_EXP"].Rows[i]["RTO_TAX"];
							_exp.UpdateData(null,_dsExp,true);
							if (_drHead.RowState != DataRowState.Deleted)
							{
								_drHead["EP_NO"] = _dsExp.Tables["MF_EXP"].Rows[0]["EP_NO"];
							}
							if (changedDS.Tables["IB_EXP"].Rows[i].RowState == DataRowState.Added)
							{
								changedDS.Tables["IB_EXP"].Rows[i]["EP_NO"] = _dsExp.Tables["MF_EXP"].Rows[0]["EP_NO"];
							}
						}
					}
				}
			}
		}


        /// <summary>
        /// 更新凭证号码
        /// </summary>
        /// <param name="dr">MF_IJ的数据行</param>
        /// <param name="statementType"></param>
        private string UpdateVohNo(DataRow dr, StatementType statementType)
        {
            string _vohNo = "";
            string _vohNoError = "";
            string _updUsr = "";
            if (dr != null && dr.Table.DataSet != null)
            {
                if (dr.Table.DataSet.ExtendedProperties.ContainsKey("UPD_USR"))
                {
                    _updUsr = dr.Table.DataSet.ExtendedProperties["UPD_USR"].ToString();
                }
            }
            if (statementType == StatementType.Update)
            {
                DrpVoh _voh = new DrpVoh();
                string _icId = dr["IC_ID"].ToString();
                if (this._reBuildVohNo)
                {
                    if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_updUsr = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("IC", _icId) == 0 || string.Compare("IO", _icId) == 0 || string.Compare("IM", _icId) == 0 || string.Compare("IB", _icId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.IO;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _icId, dr["IC_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _icId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("IC", _icId) == 0 || string.Compare("IO", _icId) == 0 || string.Compare("IM", _icId) == 0 || string.Compare("IB", _icId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.IO;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _icId, dr["IC_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _icId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                    else if (string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && !string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_updUsr = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                }
            }
            else if (statementType == StatementType.Insert)
            {
                string _icId = dr["IC_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("IC", _icId) == 0 || string.Compare("IO", _icId) == 0 || string.Compare("IM", _icId) == 0 || string.Compare("IB", _icId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.IO;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _icId, out _vohNoError);
                    _vohNo = dr["VOH_NO"].ToString();
                }
            }
            else if (statementType == StatementType.Delete)
            {
                if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                }
            }
            if (dr.Table.DataSet.ExtendedProperties.ContainsKey("DRPVOH_ERROR"))
                dr.Table.DataSet.ExtendedProperties.Remove("DRPVOH_ERROR");
            if (!string.IsNullOrEmpty(_vohNoError))
            {
                dr.Table.DataSet.ExtendedProperties.Add("DRPVOH_ERROR", _vohNoError);
            }
            return _vohNo;
        }
        /// <summary>
        /// 更新销货单凭证号码
        /// </summary>
        /// <param name="ijId"></param>
        /// <param name="ijNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string icId, string icNo, string vohNo)
        {
            DbDRPIO _io = new DbDRPIO(Comp.Conn_DB);
            _io.UpdateVohNo(icId, icNo, vohNo);
        }
		#endregion
		
		#region 托运
		/// <summary>
		/// 取得表身记录
		/// </summary>
		/// <param name="pIcId"></param>
		public DataTable GetBodyData(string pIcId)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			return _dbio.GetBodyData(pIcId);
		}

		/// <summary>
		/// GetBodyData
		/// </summary>
		/// <param name="pIcId"></param>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public DataTable GetBodyData(string pIcId, string pIcNo, string pItm)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			return _dbio.GetBodyData(pIcId, pIcNo, pItm);
		}

		/// <summary>
		/// GetBodyData2
		/// </summary>
		/// <param name="pIcId"></param>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public DataTable GetBodyData2(string pIcId, string pIcNo, string pItm)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			return _dbio.GetBodyData2(pIcId, pIcNo, pItm);
		}
        /// <summary>
        /// 取得表身记录
        /// </summary>
        /// <param name="icId"></param>
        /// <param name="icNo"></param>
        /// <param name="keyItm"></param>
        /// <returns></returns>
        public DataSet GetBodyData3(string icId, string icNo, string keyItm)
        {
            DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
            return _dbio.GetBodyData3(icId, icNo, keyItm);
        }

		/// <summary>
		/// 取得项次中的产品编号
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetPrdNo(string pIcNo, string pItm)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			return _dbio.GetPrdNo(pIcNo, pItm);
		}

		/// <summary>
		/// GetQty
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetQty(string pIcNo, string pItm)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			return _dbio.GetQty(pIcNo, pItm);
		}

		/// <summary>
		/// GetQty
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetQty2(string pIcNo, string pItm)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			return _dbio.GetQty2(pIcNo, pItm);
		}
		
		/// <summary>
		/// GetQtySum
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetQtySum(string pIcNo, string pItm)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			return _dbio.GetQtySum(pIcNo, pItm);
		}
		
		/// <summary>
		/// GetQtySum2
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetQtySum2(string pIcNo, string pItm)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			return _dbio.GetQtySum2(pIcNo, pItm);
		}

		/// <summary>
		/// 反写已托运量(增加)
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pKeyItm"></param>
		/// <param name="pQty"></param>
		public void SetQtyFx(string pIcNo, string pKeyItm, string pQty)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			_dbio.SetQtyFx(pIcNo, pKeyItm, pQty);
		}
		/// <summary>
		/// 反写已托运量(删除)
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pKeyItm"></param>
		/// <param name="pQty"></param>
		public void SetQtyFxd(string pIcNo, string pKeyItm, string pQty)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			_dbio.SetQtyFxd(pIcNo, pKeyItm, pQty);
		}

		/// <summary>
		/// 反写已托运量(修改)
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pKeyItm"></param>
		/// <param name="pOldQty"></param>
		/// <param name="pQty"></param>
		public void SetQtyFxu(string pIcNo, string pKeyItm,string pOldQty, string pQty)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			_dbio.SetQtyFxu(pIcNo, pKeyItm, pOldQty, pQty);
		}
		/// <summary>
		/// 配送单结案
		/// </summary>
		/// <param name="pIcNo"></param>
		public void ChkEnd(string pIcNo)
		{
			DbDRPIO _dbio = new DbDRPIO(Comp.Conn_DB);
			_dbio.ChkEnd(pIcNo);
		}

		/// <summary>
		/// 修改配送单的箱已托运数量
		/// </summary>
		/// <param name="IcID">调拨类别（IO：配送单 IB：配送退回 IC：调拨单 IM：客户调货单）</param>
		/// <param name="IcNo">配送单号</param>
		/// <param name="KeyItm">追踪箱条码</param>
		/// <param name="Qty">托运数量</param>
		public void UpdateBoxQty(string IcID,string IcNo,string KeyItm,decimal Qty)
		{
			try
			{
				DbDRPIO _io = new DbDRPIO(Comp.Conn_DB);
				string _error = _io.UpdateBoxQty(IcID,IcNo,KeyItm,Qty);
				if (_error == "1")
				{
					throw new SunlikeException("RCID=COMMON.HINT.BOXNOTEXIST");//找不到此配送单的箱内容！
				}
				else if (_error == "2")
				{
					throw new SunlikeException("RCID=COMMON.HINT.QTYFATANTO");//已托运数量大于配送数量！
				}
			}
			catch
			{
				throw new SunlikeException("RCID=COMMON.HINT.BOXNOTEXIST");//找不到此配送单的箱内容！
			}
		}
		#endregion

		#region 重整箱数量
		/// <summary>
		/// 重整箱数量
		/// </summary>
		/// <param name="BeginDate">开始日期</param>
		/// <param name="EndDate">结束日期</param>
		public void ResetBoxQty(DateTime BeginDate,DateTime EndDate)
		{
			DbDRPIO _io = new DbDRPIO(Comp.Conn_DB);
			_io.ResetBoxQty(BeginDate.ToString(Comp.SQLDateFormat),EndDate.ToString(Comp.SQLDateFormat));
		}

		/// <summary>
		/// 重整箱数量
		/// </summary>
		/// <param name="IcNo">配送单号</param>
		/// <param name="PrdMark">产品特征</param>
		/// <param name="BoxItm">箱序号</param>
		/// <param name="Qty">数量</param>
		public void ResetBoxQty(string IcNo,string PrdMark,int BoxItm,decimal Qty)
		{
			DbDRPIO _io = new DbDRPIO(Comp.Conn_DB);
			_io.ResetBoxQty(IcNo,PrdMark,BoxItm,Qty);
		}
		#endregion

		#region 得出配送单据不同配码比在prdt1中的第一笔数据
		/// <summary>
		///  得出配送单据不同配码比在prdt1中的第一笔数据
		/// </summary>
		/// <param name="BeginDate">开始日期</param>
		/// <param name="EndDate">结束日期</param>
		/// <returns></returns>
		public DataTable GetFirstPrdt1ByBox(DateTime BeginDate,DateTime EndDate)
		{
			DbDRPIO _io = new DbDRPIO(Comp.Conn_DB);
			return _io.GetFirstPrdt1ByBox(BeginDate.ToString(Comp.SQLDateFormat),EndDate.ToString(Comp.SQLDateFormat));
		}
		#endregion

        #region ICloseBill Members
        /// <summary>
        /// DoCloseBill
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            DataSet _ds = this.GetData("", bil_id, bil_no, false);
            if (_ds.Tables["MF_IC"].Rows.Count > 0)
            {
                bool _isCloseIt = false;
                if (_ds.Tables["MF_IC"].Rows[0]["IZ_CLS_ID"].ToString() == "T")
                    _isCloseIt = true;

                if (_isCloseIt)
                {
                    return "RCID=COMMON.HINT.CLOSEERROR,PARAM=" + _ds.Tables["MF_IC"].Rows[0]["IC_NO"].ToString();//该单据[{0}]已结案,结案动作不能完成!
                }
            }
            foreach (DataRow dr in _ds.Tables["TF_IC"].Rows)
            {
                this.UpdateWhClose(dr, true);
            }
            DbDRPIO _dbDrpIo = new DbDRPIO(Comp.Conn_DB);
            return _dbDrpIo.CloseBill(bil_id, bil_no, cls_name, true);
        }
        /// <summary>
        /// UndoCloseBill
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            DataSet _ds = this.GetData("", bil_id, bil_no, false);
            if (_ds.Tables["MF_IC"].Rows.Count > 0)
            {
                bool _isCloseIt = false;
                if (_ds.Tables["MF_IC"].Rows[0]["IZ_CLS_ID"].ToString() == "T")
                    _isCloseIt = true;

                if (!_isCloseIt)
                {
                    return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + _ds.Tables["MF_IC"].Rows[0]["IC_NO"].ToString();//该单据[{0}]已结案,结案动作不能完成!
                }
            }
            foreach (DataRow dr in _ds.Tables["TF_IC"].Rows)
            {
                this.UpdateWhClose(dr, false);
            }
            DbDRPIO _dbDrpIo = new DbDRPIO(Comp.Conn_DB);
            return _dbDrpIo.CloseBill(bil_id, bil_no, cls_name, false);
        }

        #endregion
        #region  判断单据表身是否补开发票则不允许删除
        /// <summary>
        /// 判断单据表身是否补开发票则不允许删除
        /// </summary>
        /// <param name="dr">tf_ck.row</param>
        private void checktflz(DataRow dr)
        {
                InvIK _invik = new InvIK();

                string bilId = dr["IC_ID", DataRowVersion.Original].ToString();
                string ckNo = dr["IC_NO", DataRowVersion.Original].ToString();

                SunlikeDataSet _ds = _invik.GetInTfLz(bilId, ckNo);
                if (_ds.Tables["TF_LZ"].Rows.Count > 0)
                {
                    throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + ",PARAM=" + _ds.Tables["TF_LZ"].Rows[0]["LZ_NO"].ToString());//无法删除单号，原因：{0}
                }

        }
        #endregion

    }
}
