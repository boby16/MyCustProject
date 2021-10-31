using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Common.Utility;
using Sunlike.Business.Data;
using System.Collections;
using System.Data;

namespace Sunlike.Business
{
    /// <summary>
    /// MTNFin
    /// </summary>
    public class MTNFin : BizObject, IAuditing
    {
        private bool _isRunAuditing;
        private string _usr;
        private bool _updateAmtnInvDelete;//不晓得是做什么用的

        private bool _updateAmtnInvAdd;//一样

        private bool _clsLJid;//这个是在BeforeUpdate,AfterUpdate时候用，确定是否要变更未发量

        private string _clsOtid;//这个是在BeforeUpdate,AfterUpdate时候用
        private string _clsOtno;//这个是在BeforeUpdate,AfterUpdate时候用
        private bool _reBuildVohNo = false;// 是否重新切制凭证

        #region Function 预收，立账单号,月余额库
        //预收款

        private void UpdateMon(DataRow dr)
        {
            Bills _bills = new Bills();
            //生成预收款单据



            MonStruct _mon = new MonStruct();
            _mon.RpId = "1";
            _mon.RpNo = dr["RP_NO"].ToString();
            _mon.RpDd = Convert.ToDateTime(dr["OW_DD"].ToString());
            _mon.BilId = dr["OW_ID"].ToString();
            _mon.BilNo = dr["OW_NO"].ToString();
            _mon.Usr = dr["USR"].ToString();
            _mon.ChkMan = dr["CHK_MAN"].ToString();
            if (!string.IsNullOrEmpty(dr["CLS_DATE"].ToString()))
                _mon.ClsDate = Convert.ToDateTime(dr["CLS_DATE"].ToString());
            //_mon.VohId = dr["VOH_ID"].ToString();//**凭证|模板**//
            //_mon.VohNo = dr["VOH_NO"].ToString();//**凭证|模板**//
            _mon.MobId = "";// dr["MOB_ID"].ToString();//**凭证|模板**//
            _mon.UsrNo = dr["SAL_NO"].ToString();
            _mon.Dep = dr["DEP"].ToString();
            _mon.IrpId = "F";
            _mon.CusNo = dr["CUS_NO"].ToString();
            _mon.CurId = dr["CUR_ID"].ToString();
            if (string.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                _mon.ExcRto = 1;
            else
                _mon.ExcRto = Convert.ToDecimal(dr["EXC_RTO"].ToString());
            _mon.BaccNo = dr["BACC_NO"].ToString();
            _mon.CaccNo = dr["CACC_NO"].ToString();
            #region 银行
            if (string.IsNullOrEmpty(dr["AMT_BB"].ToString()))
            {
                _mon.AmtBb = 0;
            }
            else
            {
                _mon.AmtBb = Convert.ToDecimal(dr["AMT_BB"].ToString());
            }
            if (string.IsNullOrEmpty(dr["AMTN_BB"].ToString()))
            {
                _mon.AmtnBb = 0;
            }
            else
            {
                _mon.AmtnBb = Convert.ToDecimal(dr["AMTN_BB"].ToString());
            }
            #endregion

            #region 现金
            if (string.IsNullOrEmpty(dr["AMT_BC"].ToString()))
            {
                _mon.AmtBc = 0;
            }
            else
            {
                _mon.AmtBc = Convert.ToDecimal(dr["AMT_BC"].ToString());
            }
            if (string.IsNullOrEmpty(dr["AMTN_BC"].ToString()))
            {
                _mon.AmtnBc = 0;
            }
            else
            {
                _mon.AmtnBc = Convert.ToDecimal(dr["AMTN_BC"].ToString());
            }
            #endregion

            #region 票据
            if (string.IsNullOrEmpty(dr["AMT_CHK"].ToString()))
            {
                _mon.AmtChk = 0;
            }
            else
            {
                _mon.AmtChk = Convert.ToDecimal(dr["AMT_CHK"].ToString());
            }
            if (string.IsNullOrEmpty(dr["AMTN_CHK"].ToString()))
            {
                _mon.AmtnChk = 0;
                //该属性为TRUE生成TF_MON4的表身





                _mon.AddMon4 = false;
            }
            else
            {
                _mon.AmtnChk = Convert.ToDecimal(dr["AMTN_CHK"].ToString());
                _mon.ChkNo = dr["CHK_NO"].ToString();
                _mon.BankNo = dr["BANK_NO"].ToString();
                _mon.BaccNoChk = dr["BACC_NO_CHK"].ToString();
                if (string.IsNullOrEmpty(dr["END_DD"].ToString()))
                {
                    _mon.EndDd = System.DateTime.Now;
                }
                else
                {
                    _mon.EndDd = Convert.ToDateTime(dr["END_DD"].ToString());
                }
                _mon.ChkKnd = dr["CHK_KND"].ToString();
                //该属性为TRUE生成TF_MON4的表身





                _mon.AddMon4 = true;
            }
            #endregion

            #region 其他
            if (string.IsNullOrEmpty(dr["TAX_IRP"].ToString()))
            {
                _mon.AmtnOther = 0;
                //该属性为TRUE生成TF_MON3的表身





                _mon.AddMon3 = false;
            }
            else
            {
                bool _idxTax = false;
                Cust _cust = new Cust();
                SunlikeDataSet _dsCust = new SunlikeDataSet();
                _dsCust.Merge(_cust.GetData(_mon.CusNo));
                DataTable _custDt = _dsCust.Tables["CUST"];
                if (_custDt.Rows.Count > 0
                    && _custDt.Rows[0]["ID2_TAX"].ToString() == "T"
                    )
                {
                    _idxTax = true;
                }
                CompInfo _compInfo = Comp.GetCompInfo("");
                int _poiTax = _compInfo.DecimalDigitsInfo.System.POI_TAX;
                if (_idxTax && !string.IsNullOrEmpty(_mon.CurId))
                {
                    _mon.AmtOther = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()), _poiTax);
                    _mon.AmtnOther = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()) * _mon.ExcRto, _poiTax);
                }
                else
                {
                    _mon.AmtnOther = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()), _poiTax);
                    if (_mon.ExcRto != 1)
                    {
                        _mon.AmtOther = Math.Round((Convert.ToDecimal(dr["TAX_IRP"].ToString()) / _mon.ExcRto), _poiTax);
                    }
                    else
                        _mon.AmtOther = 0;
                }
                //该属性为TRUE生成TF_MON3的表身





                _mon.AddMon3 = true;
            }
            #endregion

            #region 预收款





            if (string.IsNullOrEmpty(dr["AMT_IRP"].ToString()))
                _mon.AmtIrp = 0;
            else
                _mon.AmtIrp = Convert.ToDecimal(dr["AMT_IRP"].ToString());
            if (string.IsNullOrEmpty(dr["AMTN_IRP"].ToString()))
            {
                _mon.AmtnIrp = 0;
                _mon.AddMon1 = false;
            }
            else
            {
                _mon.AddMon1 = true;
                _mon.AmtnIrp = Convert.ToDecimal(dr["AMTN_IRP"].ToString());
                _mon.TfMon1 = dr.Table.DataSet.Tables["TF_MON1"];

            }
            #endregion

            _mon.Amtn = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
            _mon.AmtnCls = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
            _mon.AddTcMon = true;
            _mon.ArpNo = dr["ARP_NO"].ToString();
            string _rpNo = _bills.AddRcvPay(_mon);

            dr["RP_NO"] = _rpNo;
        }
        //更新立帐单号
        private void UpdateMfArp(DataRow dr, bool updateArpNo)
        {
            dr = dr.Table.DataSet.Tables["MF_MFIN"].Rows[0];
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_MFIN1"];
            if (null == _dtBody || _dtBody.Rows.Count == 0)
                return;
            decimal _amtn = 0;
            decimal _amt = 0;
            decimal _tax = 0;
            if (dr.RowState != DataRowState.Deleted)
            {
                DataRow[] _darBody = _dtBody.Select();
                for (int i = 0; i < _darBody.Length; i++)
                {
                    if (!String.IsNullOrEmpty(_darBody[i]["AMT"].ToString()))
                    {
                        _amt += Convert.ToDecimal(_darBody[i]["AMT"]);
                    }
                    if (!String.IsNullOrEmpty(_darBody[i]["AMTN_NET"].ToString()))
                    {
                        _amtn += Convert.ToDecimal(_darBody[i]["AMTN_NET"]);
                    }
                    if (!String.IsNullOrEmpty(_darBody[i]["TAX"].ToString()))
                    {
                        _tax += Convert.ToDecimal(_darBody[i]["TAX"]);
                    }
                }
                if (dr["TAX_ID"].ToString() == "3")
                {
                    _amt += _tax;
                }
            }
            Arp _arp = new Arp();
            string _arpNo = "";
            //如果是删除或者是立帐方式由“单张立帐”改成“不立帐”或“收到发票后立账”

            //又或者立帐的情况下改变产商或立帐方式，要先删除原来的帐款
            if (dr.RowState != DataRowState.Added && dr["ZHANG_ID", DataRowVersion.Original].ToString() == "1")
            {
                _arpNo = dr["ARP_NO", DataRowVersion.Original].ToString();
                //取得本单据收款单信息
                Bills _bills = new Bills();
                string _rpId = "1";
                string _rpNo = "";
                _rpNo = dr["RP_NO", DataRowVersion.Original].ToString();
                SunlikeDataSet _dsMon = _bills.GetData(_rpId, _rpNo, false);
                if (!_arp.DeleteMfArp(_arpNo, _dsMon.Tables["TF_MON"], false))
                {
                    throw new SunlikeException("无法删除立帐单:" + _arpNo);
                }
            }
            if (dr.RowState != DataRowState.Deleted && dr["ZHANG_ID"].ToString() == "1")
            {
                decimal _excRto = 1;
                if (!String.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                    _excRto = Convert.ToDecimal(dr["EXC_RTO"]);
                _arpNo = _arp.UpdateMfArp("1", "2", dr["OW_ID"].ToString(), dr["OW_NO"].ToString(), Convert.ToDateTime(dr["OW_DD"]), dr["BIL_TYPE"].ToString(),
                    dr["DEP"].ToString(), dr["USR"].ToString(), dr["CUR_ID"].ToString(), _excRto, _amtn + _tax, _amtn, _amt, _tax, dr, dr["REM"].ToString());
                if (_arpNo != dr["ARP_NO"].ToString())
                {
                    if (updateArpNo)
                    {
                        DbMTNFin _db = new DbMTNFin(Comp.Conn_DB);
                        _db.UpdateArpNo(dr["OW_ID"].ToString(), dr["OW_NO"].ToString(), _arpNo);
                    }
                    dr["ARP_NO"] = _arpNo;
                }
            }
        }
        //应收付款月余额库
        private void UpdateSarp(DataRow dr)
        {
            DataRow _dr = dr.Table.DataSet.Tables["MF_MFIN"].Rows[0];
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_MFIN1"];
            int _year = System.DateTime.Now.Year;
            int _month = System.DateTime.Now.Month;
            string _psID = "";
            string _cusNo = "";
            decimal _up, _qty;
            decimal _amtn = 0;
            Arp _arp = new Arp();
            Cust _cust = new Cust();

            //如果是删除或者是立帐方式由“单张立帐”改成“不立帐”





            //又或者立帐的情况下改变厂商或立帐方式，要先删除原来的帐款
            if (_dr.RowState != DataRowState.Added && _dr["ZHANG_ID", DataRowVersion.Original].ToString() == "1")
            {
                _year = Convert.ToDateTime(_dr["OW_DD", DataRowVersion.Original]).Year;
                _month = Convert.ToDateTime(_dr["OW_DD", DataRowVersion.Original]).Month;
                _psID = _dr["OW_ID", DataRowVersion.Original].ToString();
                _cusNo = _dr["CUS_NO", DataRowVersion.Original].ToString();
                #region 更新AMTN_INV
                //是分销商更新AMTN_INV
                if (_cust.IsDrp_id(_cusNo))
                {
                    if (_updateAmtnInvDelete)
                    {
                        for (int i = 0; i < _dtBody.Rows.Count; i++)
                        {
                            if (_dtBody.Rows[i].RowState == DataRowState.Added)
                            {
                                continue;
                            }
                            if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP", DataRowVersion.Original].ToString()))
                            {
                                _up = 0;
                            }
                            else
                            {
                                _up = Convert.ToDecimal(_dtBody.Rows[i]["UP", DataRowVersion.Original]);
                            }
                            if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY", DataRowVersion.Original].ToString()))
                            {
                                _qty = 0;
                            }
                            else
                            {
                                _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY", DataRowVersion.Original]);
                            }
                            _amtn += _up * _qty;
                            if (_dr["TAX_ID", DataRowVersion.Original].ToString() != "1" && !String.IsNullOrEmpty(_dtBody.Rows[i]["TAX", DataRowVersion.Original].ToString()))
                            {
                                _amtn += Convert.ToDecimal(_dtBody.Rows[i]["TAX", DataRowVersion.Original]);
                            }
                        }
                        if (_psID == "SB" || _psID == "SD")
                        {
                            _amtn *= -1;
                        }
                        _arp.UpdateSarp("1", _year, _cusNo, _month, "", "AMTN_INV", _amtn);
                    }
                }
                #endregion
            }
            if (_dr.RowState != DataRowState.Deleted && _dr["ZHANG_ID"].ToString() == "1")
            {
                _up = 0;
                _qty = 0;
                _amtn = 0;
                _year = Convert.ToDateTime(_dr["OW_DD"]).Year;
                _month = Convert.ToDateTime(_dr["OW_DD"]).Month;
                _psID = _dr["OW_ID"].ToString();
                _cusNo = _dr["CUS_NO"].ToString();
                #region 更新AMTN_INV
                if (_cust.IsDrp_id(_cusNo))
                {
                    if (_updateAmtnInvAdd)
                    {
                        for (int i = 0; i < _dtBody.Rows.Count; i++)
                        {
                            if (_dtBody.Rows[i].RowState == DataRowState.Deleted)
                            {
                                if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP", DataRowVersion.Original].ToString()))
                                {
                                    _up = 0;
                                }
                                else
                                {
                                    _up = Convert.ToDecimal(_dtBody.Rows[i]["UP", DataRowVersion.Original]);
                                }
                                if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY", DataRowVersion.Original].ToString()))
                                {
                                    _qty = 0;
                                }
                                else
                                {
                                    _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY", DataRowVersion.Original]);
                                }
                                if (_dr["TAX_ID", DataRowVersion.Original].ToString() != "1" && !String.IsNullOrEmpty(_dtBody.Rows[i]["TAX", DataRowVersion.Original].ToString()))
                                {
                                    _amtn -= Convert.ToDecimal(_dtBody.Rows[i]["TAX", DataRowVersion.Original]);
                                }
                                _amtn -= _up * _qty;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP"].ToString()))
                                {
                                    _up = 0;
                                }
                                else
                                {
                                    _up = Convert.ToDecimal(_dtBody.Rows[i]["UP"]);
                                }
                                if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY"].ToString()))
                                {
                                    _qty = 0;
                                }
                                else
                                {
                                    _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY"]);
                                }
                                if (_dr["TAX_ID"].ToString() != "1" && !String.IsNullOrEmpty(_dtBody.Rows[i]["TAX"].ToString()))
                                {
                                    _amtn += Convert.ToDecimal(_dtBody.Rows[i]["TAX"]);
                                }
                                _amtn += _up * _qty;
                            }
                        }
                        if (_psID != "SB" && _psID != "SD")
                        {
                            _amtn *= -1;
                        }
                        _arp.UpdateSarp("1", _year, _cusNo, _month, "", "AMTN_INV", _amtn);
                    }
                }
                #endregion
            }
        }
        #endregion

        #region 构造



        /// <summary>
        /// 构造器
        /// </summary>
        public MTNFin() { }
        #endregion

        #region 取数据




        /// <summary>
        /// 取数据







        /// </summary>
        ///  <param name="usr">usr</param>
        /// <param name="owId">OW</param>
        /// <param name="owNo">完工单号</param>
        /// <returns>DS数据</returns>
        public SunlikeDataSet GetData(string pgm, string usr, string owId, string owNo)
        {
            DbMTNFin _db = new DbMTNFin(Comp.Conn_DB);
            SunlikeDataSet _ds = _db.GetData(owId, owNo);
            if (!string.IsNullOrEmpty(usr))
            {
                Users _usrs = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_usrs.GetUserDepNo(usr)).DecimalDigitsInfo.System;

                //管制立帐方式
                if (INVCommon.IsControlZhangId(usr, owId))
                {
                    _ds.ExtendedProperties["CTRL_ZHANG_ID"] = "T";
                }
            }
            _ds.Tables["TF_MFIN"].Columns["QTY_OS_ORG"].ReadOnly = false;

            //自动增长
            _ds.Tables["TF_MFIN"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_MFIN"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_MFIN"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_MFIN"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_MFIN"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_MFIN"].Columns["KEY_ITM"].Unique = true;
            //自动增长
            _ds.Tables["TF_MFIN_CL"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_MFIN_CL"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_MFIN_CL"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_MFIN_CL"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_MFIN_CL"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_MFIN_CL"].Columns["KEY_ITM"].Unique = true;

            //取预收



            string _rpNo = "";
            if (_ds.Tables["MF_MFIN"].Rows.Count > 0)
            {
                _rpNo = _ds.Tables["MF_MFIN"].Rows[0]["RP_NO"].ToString();
            }
            Bills _bills = new Bills();
            SunlikeDataSet _dsMon = _bills.GetData("1", _rpNo, false);
            _ds.Merge(_dsMon.Tables["TF_MON1"]);
            this.SetCanModify(pgm, _ds, usr, true);
            return _ds;
        }

        /// <summary>
        /// 设置是否可更新数据







        /// </summary>
        /// <param name="changedDS"></param>
        /// <param name="usr"></param>
        /// <param name="isCheckAuditing"></param>
        private void SetCanModify(string pgm, SunlikeDataSet changedDS, string usr, bool isCheckAuditing)
        {
            if (changedDS.Tables["MF_MFIN"].Rows.Count > 0)
            {
                DataRow _drMf = changedDS.Tables["MF_MFIN"].Rows[0];
                DataTable _dtTf1 = changedDS.Tables["TF_MFIN_CL"];
                // 增加权限控管
                if (usr != null)
                {
                    string _bill_Dep = _drMf["DEP"].ToString();
                    string _bill_Usr = _drMf["USR"].ToString();
                    string _bill_Id = _drMf["OW_ID"].ToString();
                    Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                    changedDS.ExtendedProperties["UPD"] = _billRight["UPD"];
                    changedDS.ExtendedProperties["DEL"] = _billRight["DEL"];
                    changedDS.ExtendedProperties["PRN"] = _billRight["PRN"];
                    changedDS.ExtendedProperties["LCK"] = _billRight["LCK"];
                }
                bool _canModify = true;// 默认可以修改
                string errorMsg = "";
                #region /**满足以下条件的，不能修改**/
                Auditing _auditing = new Auditing();
                //进入审核流程
                if (isCheckAuditing)
                {
                    if (_auditing.GetIfEnterAuditing(_drMf["OW_ID"].ToString(), _drMf["OW_NO"].ToString()))
                    {
                        _canModify = false;
                        errorMsg = "RCID=COMMON.HINT.CLOSE_AUDIT;";
                        //Common.SetCanModifyRem(changedDS, errorMsg);
                    }
                }
                //关帐
                if (Comp.HasCloseBill(Convert.ToDateTime(_drMf["OW_DD"]), _drMf["DEP"].ToString(), "CLS_MNU"))
                {
                    errorMsg = "RCID=COMMON.HINT.CLOSE_CLS";//关帐
                    _canModify = false;
                    //Common.SetCanModifyRem(changedDS, errorMsg);
                }
                //已经结案
                if (CaseInsensitiveComparer.Default.Compare(_drMf["CLS_LZ_ID"].ToString(), "T") == 0)
                {
                    _canModify = false;
                    errorMsg = "RCID=COMMON.HINT.CLOSE_MODIFY;";
                    //Common.SetCanModifyRem(changedDS, errorMsg);
                }
                //已切维修成本凭证

                DbMTNFin _db = new DbMTNFin(Comp.Conn_DB);
                if (_db.IsCbidT(_drMf["OW_ID"].ToString(), _drMf["OW_NO"].ToString()))
                {
                    _canModify = false;
                    errorMsg = "RCID=COMMON.HINT.CBIDIST;";
                    //Common.SetCanModifyRem(changedDS, errorMsg);
                }

                //判断是否锁单
                if (!String.IsNullOrEmpty(_drMf["LOCK_MAN"].ToString()))
                {
                    _canModify = false;
                    errorMsg = "RCID=COMMON.HINT.CLOSE_LOCK;";
                    //Common.SetCanModifyRem(changedDS, errorMsg);
                }
                #endregion
                #region 凭证的对应





                if (_canModify && !String.IsNullOrEmpty(_drMf["VOH_NO"].ToString()))
                {
                    //判断凭证
                    string _acNo = "";
                    DrpVoh _drpVoh = new DrpVoh();
                    string _updUsr = "";
                    if (changedDS.ExtendedProperties.ContainsKey("UPD_USR"))
                    {
                        _updUsr = changedDS.ExtendedProperties["UPD_USR"].ToString();
                    }
                    else
                    {
                        _updUsr = _drMf["USR"].ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_drMf["VOH_NO"].ToString(), _updUsr, ref _acNo);
                    if (_resVoh == 0 || _resVoh == 1)
                    {
                        changedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = false;
                        changedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                    else if (_resVoh == 2)
                    {
                        changedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = true;
                        changedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                }
                #endregion
                changedDS.ExtendedProperties["CAN_MODIFY"] = _canModify ? "T" : "F";
            }
        }

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="_ds">dataset数据</param>
        public DataTable UpdateData(string pgm, SunlikeDataSet _ds)
        {
            DataTable _dtErr = null;
            _ds.Tables["TF_MFIN_CL"].TableName = "TF_MFIN1";

            #region //判断是否走审核流程








            DataRow _dr = _ds.Tables["MF_MFIN"].Rows[0];
            string _owid = string.Empty;
            if (_dr.RowState == DataRowState.Deleted)
            {
                _owid = _dr["OW_ID", DataRowVersion.Original].ToString();
                _usr = _dr["USR", DataRowVersion.Original].ToString();
            }
            else
            {
                _usr = _dr["USR"].ToString();
                _owid = _dr["OW_ID"].ToString();
            }
            Auditing _auditing = new Auditing();
            string _bilType = "";
            string _mobID = "";//支持直接终审mobID=@@ 则单据不跑审核流程




            if (_dr.RowState == DataRowState.Deleted)
            {
                if (_dr.Table.Columns.Contains("BIL_TYPE"))
                    _bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
                if (_dr.Table.Columns.Contains("MOB_ID"))
                    _mobID = _dr["MOB_ID", DataRowVersion.Original].ToString();
            }
            else
            {
                if (_dr.Table.Columns.Contains("BIL_TYPE"))
                    _bilType = _dr["BIL_TYPE"].ToString();
                if (_dr.Table.Columns.Contains("MOB_ID"))
                    _mobID = _dr["MOB_ID"].ToString();
            }
            //_isRunAuditing = _auditing.IsRunAuditing(_owid, _usr, _bilType, _mobID);



            #endregion

            //是否重建凭证号码
            if (_ds.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", _ds.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            Hashtable _ht = new Hashtable();
            _ht["MF_MFIN"] = "OW_ID, OW_NO, OW_DD, CUS_NO, DEP, SAL_NO, BIL_TYPE, CLS_LZ_ID, CLS_LZ_AUTO, CUR_ID, EXC_RTO, TAX_ID, ZHANG_ID, REM, OT_ID,OT_NO, SYS_DATE, CLS_DATE, PRT_SW, USR, CHK_MAN, CNT_NO, CNT_REM, DIS_CNT, AMTN_NET, TAX, AMTN_INT, AMT_INT, INV_NO, RP_NO,PAY_MTH, PAY_DAYS, CHK_DAYS, PAY_REM, PAY_DD, CHK_DD, INT_DAYS, CLS_REM, AMT_CLS, AMTN_NET_CLS, TAX_CLS, QTY_CLS, KP_ID,TURN_ID, ARP_NO, AMTN_IRP, AMT_IRP, TAX_IRP,KIND_NO,REM_RUN,REM_CUST,CHK_CUST,VOH_ID,VOH_NO,MOB_ID,CNT_NAME,OTH_NAME,TEL_NO,CELL_NO,CNT_ADR";
            _ht["TF_MFIN"] = "OW_ID, OW_NO, ITM, PRD_NO,PRD_NAME, PRD_MARK,  QTY, UNIT, WC_NO, OT_ID, OT_NO, OT_ITM, SA_NO, SA_ITM, MTN_DD, RTN_DD, MTN_TYPE, MTN_ALL_ID, REM,KEY_ITM";
            _ht["TF_MFIN1"] = "OW_ID, OW_NO, OW_ITM, ITM, WH, PRD_NO,PRD_NAME, PRD_MARK, QTY, UNIT, TAX_RTO, UP, AMTN_NET, AMT, TAX, DIS_CNT, MTN_DD, BAT_NO, AMT_FP, AMTN_NET_FP, TAX_FP, QTY_FP, VALID_DD, KEY_ITM";

            this.UpdateDataSet(_ds, _ht);
            _ds.Tables["TF_MFIN1"].TableName = "TF_MFIN_CL";
            //判断单据能否修改
            if (!_ds.HasErrors)
            {
                //UPD_USR前端没有赋值，导致取不到权限，故屏蔽掉，并取录入人权限
                //string _UpdUsr = "";
                //if (_ds.ExtendedProperties.Contains("UPD_USR"))
                //    _UpdUsr = _ds.ExtendedProperties["UPD_USR"].ToString();
                //this.SetCanModify(_ds, _UpdUsr, true);
                this.SetCanModify(pgm, _ds, _usr, true);
            }
            else
            {
                _dtErr = GetAllErrors(_ds);
            }
            return _dtErr;
        }

        /// <summary>
        /// 数据保存前的操作
        /// </summary>
        /// <param name="ds">ds数据</param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dt = ds.Tables["MF_MFIN"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["OW_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["OW_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion

            #region 数据检查

            Cust _cust = new Cust();
            DataRow[] _drSel = null;
            if (ds.Tables["TF_MFIN1"].Rows.Count > 0)
            {
                //string _cusNo = "";
                //if (ds.Tables["MF_MFIN"].Rows[0].RowState == System.Data.DataRowState.Deleted)
                //{
                //    _cusNo = ds.Tables["MF_MFIN"].Rows[0]["CUS_NO", System.Data.DataRowVersion.Original].ToString();
                //}
                //else
                //{
                //    _cusNo = ds.Tables["MF_MFIN"].Rows[0]["CUS_NO"].ToString();
                //}
                //DataTable _whDt = _cust.GetCus_WH(_cusNo);
                //for (int i = 0; i < _whDt.Rows.Count; i++)
                //{
                //    _drSel = ds.Tables["TF_MFIN1"].Select("ITM=1", "", System.Data.DataViewRowState.Deleted);
                //    if (_drSel != null && _drSel.Length > 0)
                //    {
                //        if (_drSel[0]["WH", DataRowVersion.Original].ToString() == _whDt.Rows[i]["WH"].ToString())
                //        {
                //            _updateAmtnInvDelete = true;//只有当表身第一笔数据的库位是该客户的库位时才更改

                //            break;
                //        }
                //    }
                //}
                //for (int i = 0; i < _whDt.Rows.Count; i++)
                //{
                //    _drSel = ds.Tables["TF_MFIN1"].Select("ITM=1", "", System.Data.DataViewRowState.Added);
                //    if (_drSel != null && _drSel.Length > 0)
                //    {
                //        if (_drSel[0]["WH"].ToString() == _whDt.Rows[i]["WH"].ToString())
                //        {
                //            _updateAmtnInvAdd = true;//只有当表身第一笔数据的库位是该客户的库位时才更改

                //            break;
                //        }
                //    }
                //}
            }
            #region modified时候更新立账





            if (ds.Tables["MF_MFIN"].Rows.Count > 0
                && ds.Tables["MF_MFIN"].Rows[0].RowState == DataRowState.Modified
                && this._isRunAuditing
                && !String.IsNullOrEmpty(ds.Tables["MF_MFIN"].Rows[0]["CHK_MAN"].ToString()))
            {
                string _rbError = this.RollBack(ds.Tables["MF_MFIN"].Rows[0]["OW_ID"].ToString(),
                    ds.Tables["MF_MFIN"].Rows[0]["OW_NO"].ToString(), false);
                if (!String.IsNullOrEmpty(_rbError))
                {
                    throw new SunlikeException(_rbError);
                }
                ds.Tables["MF_MFIN"].Rows[0]["ARP_NO"] = "";
            }
            #endregion
            //*************************删除时才起作用*****************************
            //预收款





            if (ds.Tables.Contains("MF_MFIN") && ds.Tables["MF_MFIN"].Rows.Count > 0 && ds.Tables["MF_MFIN"].Rows[0].RowState == DataRowState.Deleted)
            {
                DataRow _dr = ds.Tables["MF_MFIN"].Rows[0];
                Bills _bills = new Bills();  //by zb 2007-05-14
                string prNo = _dr["RP_NO", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(prNo))
                    _bills.DelRcvPay("1", prNo);
                this.UpdateMfArp(_dr, true);//立账
                // this.UpdateSarp(_dr); //更新SARP
            }
            //********************************************************************
            #endregion

            #region  获取更新前的领料结案标记 注销///
            //if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    if (ds.Tables[0].Rows[0].RowState == System.Data.DataRowState.Deleted)
            //    {
            //        _clsOtid = Convert.ToString(ds.Tables[0].Rows[0]["OT_ID", DataRowVersion.Original]);
            //        _clsOtno = Convert.ToString(ds.Tables[0].Rows[0]["OT_NO", DataRowVersion.Original]);
            //    }
            //    else
            //    {
            //        _clsOtid = Convert.ToString(ds.Tables[0].Rows[0]["OT_ID"]);
            //        _clsOtno = Convert.ToString(ds.Tables[0].Rows[0]["OT_NO"]);
            //    }

            //    DbMTNOut _out = new DbMTNOut(Comp.Conn_DB);
            //    _clsLJid = _out.isClosed(_clsOtid, _clsOtno, true);
            //}
            #endregion

            base.BeforeDsSave(ds);
        }

        /// <summary>
        /// 更新数据前







        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            DbMTNFin _db = new DbMTNFin(Comp.Conn_DB);
            #region 判断是否锁单 && 判断是否已经切凭证





            string _owNo = "", _owId = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _owNo = dr["OW_NO", DataRowVersion.Original].ToString();
                    _owId = dr["OW_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    _owNo = dr["OW_NO"].ToString();
                    _owId = dr["OW_ID"].ToString();
                }
                //判断是否锁单，如果已经锁单则不让修改。





                Users _Users = new Users();
                string _whereStr = "OW_ID = '" + _owId + "' AND OW_NO = '" + _owNo + "'";
                if (_Users.IsLocked("MF_MFIN", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
                #region 判断是否已经切凭证








                if (_db.IsCbidT(_owId, _owNo))
                    throw new SunlikeException("RCID=COMMON.HINT.CBIDIST");//已切凭证的记录不能被修改             

                #endregion
            }

            #endregion
            SQNO _sq = new SQNO();
            #region 表头
            if (tableName == "MF_MFIN")
            {
                #region 关账
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["OW_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["OW_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion

                if (statementType != StatementType.Delete)
                {
                    if (statementType == StatementType.Insert)
                    {
                        #region 取得保存单号 填写必要字段
                        dr["OW_NO"] = _sq.Set(dr["OW_ID"].ToString(), dr["USR"].ToString(), dr["DEP"].ToString(), Convert.ToDateTime(dr["OW_DD"]), dr["BIL_TYPE"].ToString());
                        if (dr["CLS_LZ_ID"] is System.DBNull)
                            dr["CLS_LZ_ID"] = "F";
                        if (dr["CLS_LZ_AUTO"] is System.DBNull)
                            dr["CLS_LZ_AUTO"] = "F";
                        dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        #endregion
                    }
                    #region 检测

                    //检测客户(必添)
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString()))
                    {
                        dr.SetColumnError("CUS_NO",/*客户代号不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString() + "");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //部门(必添)
                    Dept _dept = new Dept();
                    if (_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["OW_DD"])) == false)
                    {
                        dr.SetColumnError("DEP",/*部门代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //业务员

                    string _salNo = dr["SAL_NO"].ToString();
                    if (!String.IsNullOrEmpty(_salNo))
                    {
                        Salm _salm = new Salm();
                        if (_salm.IsExist(_usr, _salNo, Convert.ToDateTime(dr["OW_DD"])) == false)
                        {
                            dr.SetColumnError("SAL_NO",/*业务员代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + _salNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    #endregion               
                }
                else
                {
                    string _error = _sq.Delete(dr["OW_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
                    }
                }

                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = DateTime.Now;
                //    _aps.BIL_ID = dr["OW_ID"].ToString();
                //    _aps.BIL_NO = dr["OW_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = ""; //新加的部分，对应审核模板
                //}
                //else
                //    _aps = new AudParamStruct(Convert.ToString(dr["OW_ID", DataRowVersion.Original]), Convert.ToString(dr["OW_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

                #region 受审核影响

                if (statementType != StatementType.Delete)
                {
                    #region 立账方式变更
                    //如果立帐方式由“单张立帐”改为“不立帐”时，清空立帐单号

                    if (dr.RowState == DataRowState.Modified && dr["ZHANG_ID"].ToString() != dr["ZHANG_ID", DataRowVersion.Original].ToString())
                        dr["ARP_NO"] = System.DBNull.Value;
                    //如果立帐方式非“单张立帐”，清除预冲金额
                    if (dr["ZHANG_ID"].ToString() != "1" && dr.Table.DataSet.Tables.Contains("TF_MON"))
                    {
                        foreach (DataRow _tfMonDr in dr.Table.DataSet.Tables["TF_MON"].Select())
                            _tfMonDr.Delete();
                    }
                    #endregion
                    if (!_isRunAuditing)//走审核流程

                    {
                        //立账
                        this.UpdateMfArp(dr, false);//因预收款要取得该立账单故放此处

                    }
                    #region 立账，应收款 最复杂部分

                    //预收款

                    decimal _amtnRcv = 0;
                    if (dr["AMTN_BC"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_BC"]);
                    }
                    if (dr["AMTN_BB"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_BB"]);
                    }
                    if (dr["AMTN_CHK"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_CHK"]);
                    }
                    if (dr["AMTN_OTHER"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_OTHER"]);
                    }
                    if (dr["AMTN_IRP"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_IRP"]);
                    }
                    if (_amtnRcv != 0)
                    {
                        UpdateMon(dr);
                    }
                    else if (_amtnRcv == 0 && !string.IsNullOrEmpty(dr["RP_NO"].ToString()))
                    {
                        Bills _bills = new Bills();
                        _bills.DelRcvPay("1", dr["RP_NO"].ToString());
                    }
                    #endregion                   
                }

                #region  更新开发票
                //try
                //{
                //    DrpTaxAa _drpTaxAa = new DrpTaxAa();
                //    _drpTaxAa.UpdateTaxAa(dr.Table.DataSet, _owId, _owNo);
                //}
                //catch (Exception _ex)
                //{
                //    dr.SetColumnError("INV_NO", _ex.Message);
                //    status = UpdateStatus.SkipAllRemainingRows;
                //}
                #endregion

                #region 凭证
                //产生凭证
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }
                #endregion

            }
            #endregion
            #endregion
            #region 表身产品
            if (tableName == "TF_MFIN")
            {
                if (statementType != StatementType.Delete)
                {
                    if (statementType == StatementType.Update)
                    {
                        UpdateOS(dr, true, false);
                        UpdateWCH(dr, true, false);
                    }
                    UpdateOS(dr, false, false);
                    UpdateWCH(dr, false, false);

                    #region 货品检测












                    //产品检测(必添)
                    Prdt SunlikePrdt = new Prdt();
                    string _prd_no = dr["PRD_NO"].ToString();
                    DateTime owdd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MFIN"].Rows[0]["OW_DD"]);
                    if (!SunlikePrdt.IsExist(_usr, _prd_no, owdd))
                    {
                        dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prd_no);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //PMARK
                    string _mark = dr["PRD_MARK"].ToString();
                    int _prdMod = SunlikePrdt.CheckPrdtMod(dr["PRD_NO"].ToString(), _mark);
                    if (_prdMod == 1)
                    {
                        dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _mark);//货品特征[{0}]不存在












                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else if (_prdMod == 2)
                    {
                        PrdMark _prd_Mark = new PrdMark();
                        if (_prd_Mark.RunByPMark(_usr))
                        {
                            string[] _prd_markAry = _prd_Mark.BreakPrdMark(_mark);
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
                    #region 日期值格式化
                    if (!string.IsNullOrEmpty(dr["MTN_DD"].ToString()))
                        dr["MTN_DD"] = Convert.ToDateTime(dr["MTN_DD"]).ToString(Comp.SQLDateFormat);
                    if (!string.IsNullOrEmpty(dr["RTN_DD"].ToString()))
                        dr["RTN_DD"] = Convert.ToDateTime(dr["RTN_DD"]).ToString(Comp.SQLDateFormat);
                    //if (!string.IsNullOrEmpty(dr["EST_DD"].ToString()))
                    // dr["EST_DD"] = Convert.ToDateTime(dr["EST_DD"]).ToString(Comp.SQLDateFormat);
                    #endregion
                }
                else
                {
                    #region 回写外派维修单









                    UpdateOS(dr, true, false);
                    UpdateWCH(dr, true, false);
                    #endregion
                }
            }
            #endregion
            #region 表身材料
            if (tableName == "TF_MFIN1")
            {
                if (statementType != StatementType.Delete)
                {
                    if (statementType == StatementType.Insert)
                    {

                    }
                    #region 货品检测












                    //产品检测(必添)
                    Prdt SunlikePrdt = new Prdt();
                    string _prd_no = dr["PRD_NO"].ToString();
                    DateTime owdd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MFIN"].Rows[0]["OW_DD"]);
                    if (!SunlikePrdt.IsExist(_usr, _prd_no, owdd))
                    {
                        dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prd_no);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //仓库(必添)
                    string _wh = dr["WH"].ToString();
                    string _cusNo = dr.Table.DataSet.Tables["MF_MFIN"].Rows[0]["CUS_NO"].ToString();
                    WH SunlikeWH = new WH();
                    if (!SunlikeWH.IsExist(_usr, _wh, owdd))
                    {
                        dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //if (!String.IsNullOrEmpty(_cusNo))
                    //{
                    //    if (!SunlikeWH.IsExist(_usr, _wh, owdd, _cusNo))
                    //    {
                    //        dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                    //        status = UpdateStatus.SkipAllRemainingRows;
                    //    }
                    //}
                    //PMARK
                    string _mark = dr["PRD_MARK"].ToString();
                    int _prdMod = SunlikePrdt.CheckPrdtMod(dr["PRD_NO"].ToString(), _mark);
                    if (_prdMod == 1)
                    {
                        dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _mark);//货品特征[{0}]不存在












                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else if (_prdMod == 2)
                    {
                        PrdMark _prd_Mark = new PrdMark();
                        if (_prd_Mark.RunByPMark(_usr))
                        {
                            string[] _prd_markAry = _prd_Mark.BreakPrdMark(_mark);
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
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        Bat _bat = new Bat();
                        if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                        {
                            dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM=" + dr["BAT_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    #endregion
                }
                else { }

            }
            #endregion

            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }

        #region 回写外派维修单||回写安装单





        private void UpdateOS(DataRow dr, bool isDel, bool isByAuditPass)
        {
            if (!_isRunAuditing || isByAuditPass) //被审核通过或者不需要跑审核流程
            {
                DbMTNFin _db = new DbMTNFin(Comp.Conn_DB);
                string _prdNo = "";
                string _unit = "";
                string osId = "";
                string osNo = "";
                string osItm = "";
                decimal _qty = 0;
                if (!isDel)
                {
                    _prdNo = Convert.ToString(dr["PRD_NO"]);
                    _unit = Convert.ToString(dr["UNIT"]);
                    osId = Convert.ToString(dr["OT_ID"]);
                    osNo = Convert.ToString(dr["OT_NO"]);
                    osItm = Convert.ToString(dr["OT_ITM"]);
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                else
                {
                    _prdNo = Convert.ToString(dr["PRD_NO", DataRowVersion.Original]);
                    _unit = Convert.ToString(dr["UNIT", DataRowVersion.Original]);
                    osId = Convert.ToString(dr["OT_ID", DataRowVersion.Original]);
                    osNo = Convert.ToString(dr["OT_NO", DataRowVersion.Original]);
                    osItm = Convert.ToString(dr["OT_ITM", DataRowVersion.Original]);
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]) * (-1);
                }
                if (!String.IsNullOrEmpty(osId) && !String.IsNullOrEmpty(osNo) && !String.IsNullOrEmpty(osItm))
                {
                    #region 设置_clsLJid
                    _clsOtid = osId;
                    _clsOtno = osNo;
                    DbMTNOut _out = new DbMTNOut(Comp.Conn_DB);
                    _clsLJid = _out.isClosed(_clsOtid, _clsOtno, true);
                    #endregion

                    Hashtable _ht = new Hashtable();
                    _ht["TableName"] = "TF_MOUT";
                    _ht["IdName"] = "OT_ID";
                    _ht["NoName"] = "OT_NO";
                    _ht["ItmName"] = "KEY_ITM";
                    _ht["OsID"] = osId;
                    _ht["OsNO"] = osNo;
                    _ht["KeyItm"] = osItm;
                    _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_unit), _ht);
                    DbMTNOut _dbout = new DbMTNOut(Comp.Conn_DB);
                    _dbout.UpdateMoutTf(osId, osNo, osItm, _qty);
                }
            }
        }

        private void UpdateMout(string _otid, string _otno, bool isByAuditPass)
        {
            if (!_isRunAuditing || isByAuditPass) //被审核通过或者不需要跑审核流程
            {
                DbMTNOut _out = new DbMTNOut(Comp.Conn_DB);
                //1 产品结案
                _out.ClsMout(_otid, _otno);
                //2 领料结案
                _out.ClsMoutLj(_otid, _otno);
                //3 未发量处理





                if (_clsLJid != _out.isClosed(_otid, _otno, true))
                {
                    MTNOut mout = new MTNOut();
                    mout.ClsMoutLjRsv(_out.GetDataTf1(_otid, _otno).Tables[0]);
                }
            }
        }
        #endregion

        /// <summary>
        /// 更新数据后







        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            #region 表身材料
            if (tableName == "TF_MFIN")
            {
                #region 结案判断
                if (!string.IsNullOrEmpty(_clsOtid))
                {
                    UpdateMout(_clsOtid, _clsOtno, false);
                }
                #endregion
            }
            #endregion
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        protected override void AfterDsSave(DataSet ds)
        {
            #region 结案判断///注销
            //if (!string.IsNullOrEmpty(_clsOtid))
            //{
            //    UpdateMout(_clsOtid, _clsOtno, false);
            //}
            #endregion

            //强制退货控制            
            MTNOut _out = new MTNOut();
            DataTable _dtChkRtn = _out.GetChkRtnInfo(_clsOtid, _clsOtno);
            if (_dtChkRtn.Rows.Count > 0)
            {
                CompInfo _compInfo = Comp.GetCompInfo("");
                StringBuilder _errorMsg = new StringBuilder();
                for (int i = 0; i < _dtChkRtn.Rows.Count; i++)
                {
                    if (i != 0)
                        _errorMsg.Append(";");
                    _errorMsg.Append("RCID=MTN.HINT.CHECKRTN,PARAM=" + _dtChkRtn.Rows[i]["OT_NO"].ToString() + ",PARAM=" + _dtChkRtn.Rows[i]["KEY_ITM"].ToString() + ",PARAM=" + _dtChkRtn.Rows[i]["PRD_NAME"].ToString() + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", Convert.ToDecimal(_dtChkRtn.Rows[i]["QTY"])));

                }
                if (!string.IsNullOrEmpty(_errorMsg.ToString()))
                    throw new SunlikeException(_errorMsg.ToString());
            }
            base.AfterDsSave(ds);
        }

        #region  更新凭证号码
        /// <summary>
        /// 更新凭证号码
        /// </summary>
        /// <param name="dr">MF_PSS的数据行</param>
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
                string _owId = dr["OW_ID"].ToString();
                if (this._reBuildVohNo)
                {
                    if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_vohNo = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0)
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("OW", _owId) == 0 || string.Compare("OD", _owId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.MTN;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            SunlikeDataSet _dsGetData = this.GetData("", "", _owId, dr["OW_NO"].ToString());
                            _dsGetData.Tables["TF_MFIN_CL"].TableName = "TF_MFIN1";
                            _dsBills.Merge(_dsGetData, true);
                            //_dsBills.Merge(this.GetData("", "", _owId, dr["OW_NO"].ToString()), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _owId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0 && string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("OW", _owId) == 0 || string.Compare("OD", _owId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.MTN;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            SunlikeDataSet _dsGetData = this.GetData("", "", _owId, dr["OW_NO"].ToString());
                            _dsGetData.Tables["TF_MFIN_CL"].TableName = "TF_MFIN1";
                            _dsBills.Merge(_dsGetData, true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _owId, out _vohNoError);
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
                string _owId = dr["OW_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("OW", _owId) == 0 || string.Compare("OD", _owId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.MTN;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0)
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _owId, out _vohNoError);
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
        #endregion

        #region 更新维修完工单凭证号码






        /// <summary>
        /// 更新维修完工单凭证号码






        /// </summary>
        /// <param name="owId"></param>
        /// <param name="owNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string owId, string owNo, string vohNo)
        {
            DbMTNFin _fin = new DbMTNFin(Comp.Conn_DB);
            _fin.UpdateVohNo(owId, owNo, vohNo);
        }
        #endregion
        #endregion

        #region ///IAuditing 成员
        /// <summary>
        /// 审核同意
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = String.Empty;
            try
            {
                DbMTNFin _db = new DbMTNFin(Comp.Conn_DB);
                _db.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);

                SunlikeDataSet _ds = this.GetData("", chk_man, bil_id, bil_no);
                DataTable _mfTable = _ds.Tables["MF_MFIN"];
                DataTable _tfTable = _ds.Tables["TF_MFIN"];
                DataTable _tfTable1 = _ds.Tables["TF_MFIN_CL"];
                _tfTable1.TableName = "TF_MFIN1";

                DataRow _mfdr = _mfTable.Rows[0];

                #region 回写原单
                foreach (DataRow dr in _tfTable.Rows)
                {
                    UpdateOS(dr, false, true);
                    UpdateWCH(dr, false, true);
                    UpdateMout(dr["OT_ID"].ToString(), dr["OT_NO"].ToString(), true);//跟新mout数据的领料标记 未发量





                    //强制退货控制(****注意：该动作必须在完工结案后做*****)
                    MTNOut _ot = new MTNOut();
                    DataTable _dtChkRtn = _ot.GetChkRtnInfo(dr["OT_ID"].ToString(), dr["OT_NO"].ToString());
                    if (_dtChkRtn.Rows.Count > 0)
                    {
                        CompInfo _compInfo = Comp.GetCompInfo("");
                        StringBuilder _errorMsg = new StringBuilder();
                        for (int i = 0; i < _dtChkRtn.Rows.Count; i++)
                        {
                            if (i != 0)
                                _errorMsg.Append(";");
                            _errorMsg.Append("RCID=MTN.HINT.CHECKRTN,PARAM=" + _dtChkRtn.Rows[i]["OT_NO"].ToString() + ",PARAM=" + _dtChkRtn.Rows[i]["KEY_ITM"].ToString() + ",PARAM=" + _dtChkRtn.Rows[i]["PRD_NAME"].ToString() + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", Convert.ToDecimal(_dtChkRtn.Rows[i]["QTY"])));

                        }
                        if (!string.IsNullOrEmpty(_errorMsg.ToString()))
                            throw new SunlikeException(_errorMsg.ToString());
                    }
                }

                #endregion
                #region 立账，应收款 最复杂部分
                //立账
                this.UpdateMfArp(_mfdr, true);//因预收款要取得该立账单故放此处





                //预收款





                decimal _amtnRcv = 0;
                if (_mfdr["AMTN_BC"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_mfdr["AMTN_BC"]);
                }
                if (_mfdr["AMTN_BB"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_mfdr["AMTN_BB"]);
                }
                if (_mfdr["AMTN_CHK"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_mfdr["AMTN_CHK"]);
                }
                if (_mfdr["AMTN_OTHER"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_mfdr["AMTN_OTHER"]);
                }
                if (_mfdr["AMTN_IRP"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_mfdr["AMTN_IRP"]);
                }
                if (_amtnRcv != 0)
                {
                    UpdateMon(_mfdr);
                }
                else if (_amtnRcv == 0 && !string.IsNullOrEmpty(_mfdr["RP_NO"].ToString()))
                {
                    Bills _bills = new Bills();
                    _bills.DelRcvPay("1", _mfdr["RP_NO"].ToString());
                }
                //this.UpdateSarp(_mfdr); //更新SARP      
                #endregion


                //更新凭证
                string _vohNo = this.UpdateVohNo(_mfdr, StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);
                _tfTable1.TableName = "TF_MFIN_CL";
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }
        /// <summary>
        /// 审核 拒绝
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            return "";
        }
        /// <summary>
        /// 审核 回滚
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            return RollBack(bil_id, bil_no, true);
        }

        /// <summary>
        /// 审核 回滚
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="isUpdate">针对modi</param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no, bool isUpdateChkMan)
        {
            string _error = String.Empty;
            try
            {
                DbMTNFin _db = new DbMTNFin(Comp.Conn_DB);
                if (isUpdateChkMan)
                {
                    _db.UpdateChkMan(bil_id, bil_no, "", DateTime.Now);
                }
                SunlikeDataSet _ds = this.GetData("", _usr, bil_id, bil_no);
                DataTable _mfTable = _ds.Tables["MF_MFIN"];
                DataTable _tfTable = _ds.Tables["TF_MFIN"];
                DataTable _tfTable1 = _ds.Tables["TF_MFIN_CL"];
                _tfTable1.TableName = "TF_MFIN1";
                DataRow _mfdr = _mfTable.Rows[0];
                #region 回写原单
                foreach (DataRow dr in _tfTable.Rows)
                {
                    UpdateOS(dr, true, true);
                    UpdateWCH(dr, true, true);
                    UpdateMout(dr["OT_ID"].ToString(), dr["OT_NO"].ToString(), true);//跟新mout数据
                }
                #endregion

                _mfdr.Delete();
                this.UpdateMfArp(_mfdr, true); //立账 
                if (isUpdateChkMan)
                    _db.UpdateArpNo(bil_id, bil_no, "");//arpno 

                //string _rpNo = _mfdr["RP_NO"].ToString();
                //if (!String.IsNullOrEmpty(_rpNo))
                //{
                //    Bills _bills = new Bills();
                //    _bills.DelRcvPay("1", _rpNo);//删除预收付款
                //}

                //更新凭证
                this.UpdateVohNo(_mfdr, StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");
                _tfTable1.TableName = "TF_MFIN_CL";


            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }

        #endregion

        #region 更新保修卡维修记录





        /// <summary>
        /// 更新保修卡维修记录







        /// </summary>
        /// <param name="dr">dr行</param>
        /// <param name="isDel">+-标识是否删除</param>
        /// <param name="isByAuditPass">是被审核通过</param>
        private void UpdateWCH(DataRow dr, bool isDel, bool isByAuditPass)
        {
            if (!_isRunAuditing || isByAuditPass) //被审核通过或者不需要跑审核流程
            {
                string wcNo = "";
                string bilNo = "";
                if (isDel)
                {
                    wcNo = Convert.ToString(dr["WC_NO", DataRowVersion.Original]);
                    bilNo = Convert.ToString(dr["OW_NO", DataRowVersion.Original]);
                }
                else
                {
                    wcNo = Convert.ToString(dr["WC_NO"]);
                    bilNo = Convert.ToString(dr["OW_NO"]);
                }
                if (!string.IsNullOrEmpty(wcNo))
                {
                    #region  更新记录表TF_WC_H

                    WC _wc = new WC();
                    SunlikeDataSet _ds = _wc.GetDataWcH(wcNo);
                    using (DataTable _dt = _ds.Tables["TF_WC_H"])
                    {
                        DataRow[] _drs = _dt.Select("BIL_NO='" + bilNo + "'");
                        if (_drs.Length == 0)
                        {
                            if (!isDel) //新增
                            {
                                DataRow _dr = _dt.NewRow();
                                _dr["WC_NO"] = wcNo;
                                _dr["BIL_ID"] = dr["OW_ID"];
                                _dr["BIL_DD"] = dr.Table.DataSet.Tables["MF_MFIN"].Rows[0]["OW_DD"];
                                _dr["BIL_NO"] = bilNo;
                                //_dr["REM"] = dr["REM"];
                                _dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                _dt.Rows.Add(_dr);
                            }
                        }
                        else
                        {
                            DataRow _dr = _drs[0];
                            if (isDel)//删除
                                _dr.Delete();
                            else//修改
                            {
                                _dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                //_dr["REM"] = dr["REM"];
                            }
                        }
                        _wc.UpdateDataWcH(_ds);
                    }

                    #endregion
                }
            }
        }
        #endregion
    }
}
