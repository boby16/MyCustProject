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
    /// MTNOut
    /// </summary>
    public class MTNOut : BizObject, IAuditing, ICloseBill
    {
        private bool _isRunAuditing;
        private string _usr;
        /// <summary>
        /// 是否重新切制凭证
        /// </summary>
        private bool _reBuildVohNo = false;
        /// <summary>
        /// 构造器
        /// </summary>
        public MTNOut() { }

        /// <summary>
        /// 取数据



        /// </summary>
        ///  <param name="usr">usr</param>
        /// <param name="otId">OT</param>
        /// <param name="otNo">外派维修单号</param>
        /// <returns>DS数据</returns>
        public SunlikeDataSet GetData(string pgm, string usr, string otId, string otNo)
        {
            DbMTNOut _db = new DbMTNOut(Comp.Conn_DB);
            SunlikeDataSet _ds = _db.GetData(otId, otNo);
            if (!string.IsNullOrEmpty(usr))
            {
                Users _usrs = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_usrs.GetUserDepNo(usr)).DecimalDigitsInfo.System;

                if (INVCommon.IsControlZhangId(usr, otId))
                {
                    _ds.ExtendedProperties["CTRL_ZHANG_ID"] = "T";
                }
            }
            _ds.Tables["TF_MOUT"].Columns["QTY_OS_ORG"].ReadOnly = false;

            //自动增长
            _ds.Tables["TF_MOUT"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_MOUT"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_MOUT"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_MOUT"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_MOUT"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_MOUT"].Columns["KEY_ITM"].Unique = true;
            //自动增长
            _ds.Tables["TF_MOUT_CL"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_MOUT_CL"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_MOUT_CL"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_MOUT_CL"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_MOUT_CL"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_MOUT_CL"].Columns["KEY_ITM"].Unique = true;

            this.SetCanModify(pgm, usr, _ds, true);
            return _ds;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="_ds">dataset数据</param>
        public DataTable UpdateData(string pgm, string usr, SunlikeDataSet _ds)
        {
            DataTable _dtErr = null;
            _ds.Tables["TF_MOUT_CL"].TableName = "TF_MOUT1";

            #region //判断是否走审核流程

            if (_ds.Tables.Contains("MF_MOUT"))
            {
                DataRow _dr = _ds.Tables["MF_MOUT"].Rows[0];
                string _otid = string.Empty;
                if (_dr.RowState == DataRowState.Deleted)
                {
                    _otid = _dr["OT_ID", DataRowVersion.Original].ToString();
                    _usr = _dr["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = _dr["USR"].ToString();
                    _otid = _dr["OT_ID"].ToString();
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
                //_isRunAuditing = _auditing.IsRunAuditing(_otid, _usr, _bilType, _mobID);
            }

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
            _ht["MF_MOUT"] = "OT_ID, OT_NO, OT_DD, CUS_NO, DEP, SAL_NO, BIL_TYPE, CLS_ID, CLS_AUTO, CUR_ID, EXC_RTO, TAX_ID, ZHANG_ID, REM, MA_ID, MA_NO, SYS_DATE, CLS_DATE, PRT_SW, USR, CHK_MAN, CNT_NO, CNT_REM, DIS_CNT, INV_NO, RP_NO, PAY_MTH, PAY_DAYS, CHK_DAYS, PAY_REM, PAY_DD, CHK_DD, INT_DAYS, CLS_REM, CLS_LJ_ID, CLS_LJ_AUTO, HS_ID, AMTN_IRP, AMT_IRP, TAX_IRP,AMTN_YJBX,AMTN_BX,VOH_ID,VOH_NO,FJ_NUM,MOB_ID,CNT_NAME,OTH_NAME,TEL_NO,CELL_NO,CNT_ADR";
            _ht["TF_MOUT"] = "OT_ID, OT_NO, ITM, PRD_NO,PRD_NAME, PRD_MARK, QTY, UNIT, WC_NO, MA_ID, MA_NO, MA_ITM, SA_NO, SA_ITM, MTN_DD, EST_DD, RTN_DD, MTN_TYPE, QTY_FIN, MTN_ALL_ID, REM,KEY_ITM";
            _ht["TF_MOUT1"] = "OT_ID, OT_NO, OT_ITM, ITM, WH, PRD_NO,PRD_NAME, PRD_MARK, QTY, UNIT, TAX_RTO, UP, AMTN_NET, AMT, TAX, DIS_CNT,  QTY_OT, BAT_NO, KEY_ITM,VALID_DD,CHK_RTN";

            this.UpdateDataSet(_ds, _ht);
            _ds.Tables["TF_MOUT1"].TableName = "TF_MOUT_CL";
            //判断单据能否修改
            if (!_ds.HasErrors)
            {
                string _UpdUsr = "";
                if (_ds.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = _ds.ExtendedProperties["UPD_USR"].ToString();
                this.SetCanModify(pgm, _UpdUsr, _ds, true);
            }
            else
            {
                _dtErr = GetAllErrors(_ds);
            }
            return _dtErr;
        }

        /// <summary>
        /// 设置是否可更新数据





        /// </summary>
        /// <param name="changedDS"></param>
        /// <param name="usr"></param>
        /// <param name="isCheckAuditing"></param>
        private string SetCanModify(string pgm, string usr, SunlikeDataSet changedDS, bool isCheckAuditing)
        {
            string errorMsg = "";
            if (changedDS.Tables.Contains("MF_MOUT"))
            {
                if (changedDS.Tables["MF_MOUT"].Rows.Count > 0)
                {
                    DataRow _drMf = changedDS.Tables["MF_MOUT"].Rows[0];
                    DataTable _dtTf = changedDS.Tables["TF_MOUT"];
                    DataTable _dtTf1 = changedDS.Tables["TF_MOUT_CL"];
                    // 增加权限控管
                    if (!string.IsNullOrEmpty(usr))
                    {
                        string _bill_Dep = _drMf["DEP"].ToString();
                        string _bill_Usr = _drMf["USR"].ToString();
                        string _bill_Id = _drMf["OT_ID"].ToString();
                        Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                        changedDS.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDS.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDS.ExtendedProperties["PRN"] = _billRight["PRN"];
                        changedDS.ExtendedProperties["LCK"] = _billRight["LCK"];

                    }
                    bool _canModify = true;// 默认可以修改
                    #region /**满足以下条件的，不能修改**/

                    Auditing _auditing = new Auditing();
                    //进入审核流程
                    if (isCheckAuditing)
                    {
                        if (_auditing.GetIfEnterAuditing(_drMf["OT_ID"].ToString(), _drMf["OT_NO"].ToString()))
                        {
                            _canModify = false;
                            errorMsg = "RCID=COMMON.HINT.CLOSE_AUDIT;";
                            //Common.SetCanModifyRem(changedDS, errorMsg);
                        }
                    }
                    //关帐
                    if (Comp.HasCloseBill(Convert.ToDateTime(_drMf["OT_DD"]), _drMf["DEP"].ToString(), "CLS_MNU"))
                    {
                        errorMsg = "RCID=COMMON.HINT.CLOSE_CLS";//关帐
                        _canModify = false;
                        //Common.SetCanModifyRem(changedDS, errorMsg);
                    }
                    //已经结案
                    if (CaseInsensitiveComparer.Default.Compare(_drMf["CLS_ID"].ToString(), "T") == 0)
                    {
                        _canModify = false;
                        errorMsg = "RCID=COMMON.HINT.CLOSE_MODIFY;";
                        //Common.SetCanModifyRem(changedDS, errorMsg);
                    }
                    //判断是否锁单
                    if (!String.IsNullOrEmpty(_drMf["LOCK_MAN"].ToString()))
                    {
                        _canModify = false;
                        errorMsg = "RCID=COMMON.HINT.CLOSE_LOCK;";
                        //Common.SetCanModifyRem(changedDS, errorMsg);
                    }
                    //已经回写已完工量
                    if (null != _dtTf)
                    {
                        foreach (DataRow drTf in _dtTf.Rows)
                        {
                            string qtyFin = drTf["QTY_FIN"].ToString();
                            if (!string.IsNullOrEmpty(qtyFin) && Convert.ToDecimal(qtyFin) > 0)
                            {
                                changedDS.ExtendedProperties["DEL"] = "N";   //已回写完工量的不可以再删除  
                                errorMsg = "RCID=MTN.HINT.RB_MOUT;";
                                break;
                            }
                        }
                    }
                    //已经领料的 不能删除
                    if (null != _dtTf1)
                    {
                        foreach (DataRow _drTf in _dtTf1.Rows)
                        {
                            string qtyOt = _drTf["QTY_OT"].ToString();
                            if (!string.IsNullOrEmpty(qtyOt) && Convert.ToDecimal(qtyOt) > 0)
                            {
                                changedDS.ExtendedProperties["DEL"] = "N";   //已经领料的不可以再删除  
                                errorMsg = "RCID=MTN.HINT.RB_MOUT1;";
                                break;
                            }
                        }
                    }
                    //已转报销单不能删除

                    if (_canModify)
                    {
                        MONBX _bx = new MONBX();
                        SunlikeDataSet _dsBx = _bx.GetBxBody(_drMf["OT_ID"].ToString(), _drMf["OT_NO"].ToString());
                        if (_dsBx.Tables["TF_BX"].Rows.Count > 0)
                        {
                            changedDS.ExtendedProperties["DEL"] = "F";
                            errorMsg += "MON.HINT.EXIST_BX";//已转报销单

                        }
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
                            changedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = true;
                            changedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                        }
                        else if (_resVoh == 2)
                        {
                            changedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = false;
                            changedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                        }
                    }
                    #endregion
                    changedDS.ExtendedProperties["CAN_MODIFY"] = _canModify ? "T" : "F";
                }
            }

            return errorMsg;
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
            #region 判断是否锁单
            string _otNo = "", _otId = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _otNo = dr["OT_NO", DataRowVersion.Original].ToString();
                    _otId = dr["OT_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    _otNo = dr["OT_NO"].ToString();
                    _otId = dr["OT_ID"].ToString();
                }
                //判断是否锁单，如果已经锁单则不让修改。

                Users _Users = new Users();
                string _whereStr = "OT_ID = '" + _otId + "' AND OT_NO = '" + _otNo + "'";
                if (_Users.IsLocked("MF_MOUT", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            DbMTNOut _db = new DbMTNOut(Comp.Conn_DB);
            SQNO _sq = new SQNO();
            #region 表头
            if (tableName == "MF_MOUT")
            {
                #region 关账
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["OT_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["OT_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
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
                        dr["OT_NO"] = _sq.Set(dr["OT_ID"].ToString(), dr["USR"].ToString(), dr["DEP"].ToString(), Convert.ToDateTime(dr["OT_DD"]), dr["BIL_TYPE"].ToString());
                        if (dr["CLS_ID"] is System.DBNull)
                            dr["CLS_ID"] = "F";
                        if (dr["HS_ID"] is System.DBNull)
                            dr["HS_ID"] = "F";
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
                    if (_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["OT_DD"])) == false)
                    {
                        dr.SetColumnError("DEP",/*部门代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //业务员


                    string _salNo = dr["SAL_NO"].ToString();
                    if (!String.IsNullOrEmpty(_salNo))
                    {
                        Salm _salm = new Salm();
                        if (_salm.IsExist(_usr, _salNo, Convert.ToDateTime(dr["OT_DD"])) == false)
                        {
                            dr.SetColumnError("SAL_NO",/*业务员代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + _salNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    #endregion

                    #region 产生凭证
                    if (!this._isRunAuditing)
                    {
                        this.UpdateVohNo(dr, statementType);
                    }
                    #endregion
                }
                else
                {
                    string _error = _sq.Delete(dr["OT_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
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
                //    _aps.BIL_ID = dr["OT_ID"].ToString();
                //    _aps.BIL_NO = dr["OT_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = ""; //新加的部分，对应审核模板
                //}
                //else
                //    _aps = new AudParamStruct(_otId, _otNo);
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

                #region 当有订金时


                Bills _bills = new Bills();
                if (statementType == StatementType.Delete)
                {
                    if (!string.IsNullOrEmpty(dr["RP_NO", DataRowVersion.Original].ToString()))
                        _bills.DelRcvPay("1", dr["RP_NO", DataRowVersion.Original].ToString());
                }
                else
                {
                    decimal _amtn = 0;
                    if (!String.IsNullOrEmpty(dr["AMTN_IRP"].ToString()))
                    {
                        _amtn = Convert.ToDecimal(dr["AMTN_IRP"]);
                    }
                    if (_amtn != 0)
                    {
                        //订金含税否；税率取“营业人资料”里的本业税率，如果为零取5
                        if (dr["TAX_ID"].ToString() != "1" && dr["HS_ID"].ToString() == "T")
                        {
                            CompInfo _compInfo = Comp.GetCompInfo("");
                            decimal _psTax = _compInfo.SystemInfo.PS1_TAX;
                            //POI_WBA外位币小数位数；POI_AMT本位币小数位数


                            int _poiAmt = _compInfo.DecimalDigitsInfo.System.POI_AMT;
                            if (_psTax == 0)
                                _psTax = 5;
                            decimal _amtnNet = _amtn / (1 + _psTax / 100);
                            _amtnNet = Math.Round(_amtnNet, _poiAmt);
                            dr["TAX_IRP"] = _amtn - _amtnNet;
                        }
                        else
                            dr["TAX_IRP"] = System.DBNull.Value;

                        UpdateMon(dr);
                    }
                    else if (!String.IsNullOrEmpty(dr["RP_NO"].ToString()))
                    {
                        _bills.DelRcvPay("1", dr["RP_NO"].ToString());
                    }
                }
                #endregion
            }
            #endregion
            #region 表身产品
            if (tableName == "TF_MOUT")
            {
                if (statementType != StatementType.Delete)
                {
                    #region 货品检测









                    //产品检测(必添)
                    Prdt SunlikePrdt = new Prdt();
                    string _prd_no = dr["PRD_NO"].ToString();
                    DateTime otdd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MOUT"].Rows[0]["OT_DD"]);
                    if (!SunlikePrdt.IsExist(_usr, _prd_no, otdd))
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
                    //维修卡








                    if (dr["WC_NO"].ToString().Length > 0)
                    {
                        WC _wc = new WC();
                        SunlikeDataSet _dsWc = _wc.GetDataForMA(dr["WC_NO"].ToString());
                        if (_dsWc != null && _dsWc.Tables.Contains("MF_WC") && _dsWc.Tables["MF_WC"].Rows.Count > 0)
                        {
                            if (_dsWc.Tables["MF_WC"].Rows[0]["CUS_NO"].ToString().Length > 0)
                            {
                                string _cusNoMa = "";
                                string _prdNoWc = "";
                                _prdNoWc = _dsWc.Tables["MF_WC"].Rows[0]["PRD_NO"].ToString();
                                //货品判断
                                if (_prdNoWc != dr["PRD_NO"].ToString())
                                {
                                    dr.SetColumnError("WC_NO",/*维修卡货品[{0}]与当前货品[{1}]不符*/"RCID=MTN.HINT.MTNPRDTERROR,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + _prdNoWc);
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                        else
                        {
                            dr.SetColumnError("WC_NO", "RCID=MTN.HINT.WC_NO_NOT_EXIST;");//保修卡号不存在!
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //销货单判断
                    if (!string.IsNullOrEmpty(dr["SA_NO"].ToString()) && !string.IsNullOrEmpty(dr["SA_ITM"].ToString()))
                    {
                        DRPSA _sa = new DRPSA();
                        SunlikeDataSet _dsSa = _sa.GetDataPreItm("SA", dr["SA_NO"].ToString(), dr["SA_ITM"].ToString());
                        string _prdNoSa = "";
                        string _cusNoSa = "";
                        string _cusNoMa = "";
                        if (_dsSa != null && _dsSa.Tables.Contains("TF_PSS") && _dsSa.Tables["TF_PSS"].Rows.Count > 0)
                        {
                            _prdNoSa = _dsSa.Tables["TF_PSS"].Rows[0]["PRD_NO"].ToString();
                        }
                        //货品判断
                        if (_prdNoSa != dr["PRD_NO"].ToString())
                        {
                            dr.SetColumnError("SA_NO",/*销货单货品[{0}]与当前货品[{1}]不符*/"RCID=MTN.HINT.SAPRDTERROR,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + _prdNoSa);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                    }

                    #endregion
                    #region 日期值格式化
                    if (!string.IsNullOrEmpty(dr["MTN_DD"].ToString()))
                        dr["MTN_DD"] = Convert.ToDateTime(dr["MTN_DD"]).ToString(Comp.SQLDateFormat);
                    if (!string.IsNullOrEmpty(dr["RTN_DD"].ToString()))
                        dr["RTN_DD"] = Convert.ToDateTime(dr["RTN_DD"]).ToString(Comp.SQLDateFormat);
                    if (!string.IsNullOrEmpty(dr["EST_DD"].ToString()))
                        dr["EST_DD"] = Convert.ToDateTime(dr["EST_DD"]).ToString(Comp.SQLDateFormat);
                    #endregion

                    if (statementType == StatementType.Update)
                    {
                        UpdateOS(dr, true, false);
                        UpdateWCH(dr, true, false);
                    }
                    UpdateOS(dr, false, false);
                    UpdateWCH(dr, false, false);
                }
                else
                {
                    UpdateOS(dr, true, false);
                    UpdateWCH(dr, true, false);
                }
            }
            #endregion
            #region 表身材料
            if (tableName == "TF_MOUT1")
            {
                if (statementType != StatementType.Delete)
                {
                    #region 货品检测









                    //产品检测(必添)
                    Prdt SunlikePrdt = new Prdt();
                    string _prd_no = dr["PRD_NO"].ToString();
                    DateTime otdd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MOUT"].Rows[0]["OT_DD"]);
                    if (!SunlikePrdt.IsExist(_usr, _prd_no, otdd))
                    {
                        dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prd_no);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //仓库(必添)
                    string _wh = dr["WH"].ToString();
                    string _cusNo = dr.Table.DataSet.Tables["MF_MOUT"].Rows[0]["CUS_NO"].ToString();
                    WH SunlikeWH = new WH();
                    if (!SunlikeWH.IsExist(_usr, _wh, otdd))
                    {
                        dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //if (!String.IsNullOrEmpty(_cusNo))
                    //{
                    //    if (!SunlikeWH.IsExist(_usr, _wh, otdd,_cusNo))
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

                    if (statementType == StatementType.Update)
                    {

                    }

                }
                else { }

            }
            #endregion

            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }

        #region 回写维修申请单||回写销售单
        private void UpdateOS(DataRow dr, bool isDel, bool isByAuditPass)
        {
            if (!_isRunAuditing || isByAuditPass) //被审核通过或者不需要跑审核流程
            {
                DbMTNOut _db = new DbMTNOut(Comp.Conn_DB);
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
                    osId = Convert.ToString(dr["MA_ID"]);
                    osNo = Convert.ToString(dr["MA_NO"]);
                    osItm = Convert.ToString(dr["MA_ITM"]);
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                else
                {
                    _prdNo = Convert.ToString(dr["PRD_NO", DataRowVersion.Original]);
                    _unit = Convert.ToString(dr["UNIT", DataRowVersion.Original]);
                    osId = Convert.ToString(dr["MA_ID", DataRowVersion.Original]);
                    osNo = Convert.ToString(dr["MA_NO", DataRowVersion.Original]);
                    osItm = Convert.ToString(dr["MA_ITM", DataRowVersion.Original]);
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]) * (-1);
                }
                if (!String.IsNullOrEmpty(osId) && !String.IsNullOrEmpty(osNo) && !String.IsNullOrEmpty(osItm))
                {
                    if (CaseInsensitiveComparer.Default.Compare(osId, "MA") == 0)//来源于申请单ma                            
                    {
                        Hashtable _ht = new Hashtable();
                        _ht["TableName"] = "TF_MA";
                        _ht["IdName"] = "MA_ID";
                        _ht["NoName"] = "MA_NO";
                        _ht["ItmName"] = "KEY_ITM";
                        _ht["OsID"] = osId;
                        _ht["OsNO"] = osNo;
                        _ht["KeyItm"] = osItm;
                        _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_unit), _ht);
                        _db.UpdateMA(osId, osNo, osItm, _qty);
                    }
                    else//来源于销售单 os_id=sa
                    {
                        Hashtable _ht = new Hashtable();
                        _ht["TableName"] = "TF_PSS";
                        _ht["IdName"] = "PS_ID";
                        _ht["NoName"] = "PS_NO";
                        _ht["ItmName"] = "PRE_ITM";
                        _ht["OsID"] = osId;
                        _ht["OsNO"] = osNo;
                        _ht["KeyItm"] = osItm;
                        _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_unit), _ht);
                        _db.UpdateSA(osId, osNo, osItm, _qty);
                    }
                }
            }
        }
        #endregion

        private void UpdateMon(DataRow dr)
        {
            Bills _bills = new Bills();
            //生成预收款单据


            MonStruct _mon = new MonStruct();
            _mon.RpId = "1";//收钱1;出钱2
            _mon.RpNo = dr["RP_NO"].ToString();
            _mon.RpDd = Convert.ToDateTime(dr["OT_DD"].ToString());
            _mon.BilId = dr["OT_ID"].ToString();
            _mon.BilNo = dr["OT_NO"].ToString();
            _mon.Usr = dr["USR"].ToString();
            _mon.ChkMan = dr["CHK_MAN"].ToString();
            if (!string.IsNullOrEmpty(dr["CLS_DATE"].ToString()))
                _mon.ClsDate = Convert.ToDateTime(dr["CLS_DATE"].ToString());
            _mon.VohId = dr["VOH_ID"].ToString();//**凭证|模板**//
            _mon.VohNo = dr["VOH_NO"].ToString();//**凭证|模板**//         
            _mon.MobId = "";// dr["MOB_ID"].ToString();//**凭证|模板**//
            _mon.UsrNo = dr["SAL_NO"].ToString();
            _mon.Dep = dr["DEP"].ToString();
            _mon.CurId = dr["CUR_ID"].ToString();
            if (string.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                _mon.ExcRto = 1;
            else
                _mon.ExcRto = Convert.ToDecimal(dr["EXC_RTO"].ToString());
            _mon.BaccNo = dr["BACC_NO"].ToString();
            _mon.CaccNo = dr["CACC_NO"].ToString();
            #region amt_bb,amtn_bb,amtn_bc,amt_bc,amt_chk,amtn_chk
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
            _mon.IrpId = "T";
            _mon.CusNo = dr["CUS_NO"].ToString();
            if (string.IsNullOrEmpty(dr["TAX_IRP"].ToString()))
            {
                _mon.AmtOther = 0;
                _mon.AmtnOther = 0;
                _mon.AddMon3 = false;
            }
            else
            {
                _mon.AmtnOther = (-1) * Convert.ToDecimal(dr["TAX_IRP"].ToString());
                CompInfo _compInfo = Comp.GetCompInfo("");
                int _poiAmt = _compInfo.DecimalDigitsInfo.System.POI_AMT;
                if (_mon.ExcRto != 0)
                    _mon.AmtOther = (-1) * Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()) / _mon.ExcRto, _poiAmt); ;
                //该属性为TRUE生成TF_MON3的表身









                _mon.AddMon3 = true;
            }
            _mon.Amtn = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
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
                _mon.CrecardNo = dr["CRECARD_NO"].ToString();

                //该属性为TRUE生成TF_MON4的表身









                _mon.AddMon4 = true;
            }
            string _rpNo = _bills.AddRcvPay(_mon);
            dr["RP_NO"] = _rpNo;
        }

        /// <summary>
        /// 写在保存前




        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dt = ds.Tables["MF_MOUT"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["OT_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["OT_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion



            UpdateMoutCls(ds, false);
            base.BeforeDsSave(ds);
        }

        private void UpdateMoutCls(DataSet ds, bool isByAuditPass)
        {
            if (!_isRunAuditing || isByAuditPass) //被审核通过或者不需要跑审核流程
            {
                if (ds.Tables.Count > 2 && ds.Tables["MF_MOUT"].Select().Length > 0)
                {
                    DataRow _dr = ds.Tables["MF_MOUT"].Rows[0];
                    DataTable _dt = ds.Tables["TF_MOUT"];
                    DataTable _dt1 = ds.Tables[2];
                    DataRow[] drs = _dt.Select("ISNULL(QTY,0)-ISNULL(QTY_FIN,0)>0");
                    DataRow[] drs1 = _dt1.Select("ISNULL(QTY,0)-ISNULL(QTY_OT,0)>0");
                    string isCls = drs1.Length > 0 ? "F" : "T";
                    string oldVal = Convert.ToString(_dr["CLS_ID"]);
                    if (string.IsNullOrEmpty(oldVal))
                        oldVal = "F";
                    //1 产品结案//2 领料结案
                    if (drs.Length > 0)
                    {
                        _dr["CLS_LJ_ID"] = isCls;
                        _dr["CLS_LJ_AUTO"] = isCls;
                        _dr["CLS_ID"] = "F";
                        _dr["CLS_AUTO"] = "F";
                    }
                    else
                    {
                        _dr["CLS_LJ_ID"] = "T";
                        _dr["CLS_LJ_AUTO"] = "T";
                        _dr["CLS_ID"] = "T";
                        _dr["CLS_AUTO"] = "T";
                    }
                    //3 未发量处理 只有在结案标志变更的情况才做下面的对应






                    if (CaseInsensitiveComparer.Default.Compare(oldVal, _dr["CLS_ID"].ToString()) != 0)
                    {
                        ClsMoutLjRsv(_dt1);//平整未发量               
                    }
                }
            }
        }

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
            if (tableName == "TF_MOUT1")
            {
                UpdateTf(dr, statementType, _isRunAuditing, false);
            }
            #endregion
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        /// <summary>
        /// 保存后,不可以再修改DS中的数据
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            base.AfterDsSave(ds);
        }

        /// <summary>
        /// 更改库存 [货品分仓存量,批号分仓档的未发量]
        /// </summary>
        /// <param name="dr">一行材料</param>
        /// <param name="statementType">行状态</param>
        /// <param name="_isRunAuditing"></param>
        /// <param name="IsRollBack"></param>
        private void UpdateTf(DataRow dr, StatementType statementType, bool _isRunAuditing, bool IsRollBack)
        {
            #region 定义变量
            string _prdNo;
            string _prdMark;
            string _wh;
            string _validDd;
            string _batNo;
            string _unit;
            decimal _qty = 0;

            string _prdNo_old;
            string _prdMark_old;
            string _wh_old;
            string _validDd_old;
            string _batNo_old;
            string _unit_old;
            decimal _qty_old = 0;
            _prdNo = _prdMark = _wh = _validDd = _batNo = _unit = _prdNo_old = _prdMark_old = _wh_old = _validDd_old = _batNo_old = _unit_old = "";
            #endregion
            #region 取数据,设置变量的值






            switch (statementType)
            {
                case StatementType.Insert:
                    _prdNo = dr["PRD_NO"].ToString();
                    _prdMark = dr["PRD_MARK"].ToString();
                    _wh = dr["WH"].ToString();
                    _validDd = dr["VALID_DD"].ToString();
                    _batNo = dr["BAT_NO"].ToString();
                    _unit = dr["UNIT"].ToString();
                    _qty = dr["QTY"] is DBNull ? 0 : Convert.ToDecimal(dr["QTY"]);
                    break;
                case StatementType.Update:
                    {
                        _prdNo = dr["PRD_NO", DataRowVersion.Current].ToString();
                        _prdMark = dr["PRD_MARK", DataRowVersion.Current].ToString();
                        _wh = dr["WH", DataRowVersion.Current].ToString();
                        _validDd = dr["VALID_DD", DataRowVersion.Current].ToString();
                        _batNo = dr["BAT_NO", DataRowVersion.Current].ToString();
                        _unit = dr["UNIT", DataRowVersion.Current].ToString();
                        //_qty = dr["QTY", DataRowVersion.Current] is DBNull ? 0 : Convert.ToDecimal(dr["QTY", DataRowVersion.Current]);
                        _qty = Convert2Decimal(dr["QTY", DataRowVersion.Current]) - Convert2Decimal(dr["QTY_OT", DataRowVersion.Current]);

                        _prdNo_old = dr["PRD_NO", DataRowVersion.Original].ToString();
                        _prdMark_old = dr["PRD_MARK", DataRowVersion.Original].ToString();
                        _wh_old = dr["WH", DataRowVersion.Original].ToString();
                        _validDd_old = dr["VALID_DD", DataRowVersion.Original].ToString();
                        _batNo_old = dr["BAT_NO", DataRowVersion.Original].ToString();
                        _unit_old = dr["UNIT", DataRowVersion.Original].ToString();
                        //_qty_old = dr["QTY", DataRowVersion.Original] is DBNull ? 0 : Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                        _qty_old = Convert2Decimal(dr["QTY", DataRowVersion.Original]) - Convert2Decimal(dr["QTY_OT", DataRowVersion.Original]);
                        using (DataTable _mfdt = dr.Table.DataSet.Tables[0])
                        {
                            if (null != _mfdt)
                            {
                                if (CaseInsensitiveComparer.Default.Compare("T", Convert.ToString(_mfdt.Rows[0]["CLS_LJ_ID", DataRowVersion.Original])) == 0)
                                    _qty_old = 0;//已做手动领料结案的记录，删除时不需要再扣除未发量





                            }
                        }
                    }
                    break;
                case StatementType.Delete:
                    {
                        _prdNo_old = dr["PRD_NO", DataRowVersion.Original].ToString();
                        _prdMark_old = dr["PRD_MARK", DataRowVersion.Original].ToString();
                        _wh_old = dr["WH", DataRowVersion.Original].ToString();
                        _validDd_old = dr["VALID_DD", DataRowVersion.Original].ToString();
                        _batNo_old = dr["BAT_NO", DataRowVersion.Original].ToString();
                        _unit_old = dr["UNIT", DataRowVersion.Original].ToString();
                        //_qty_old = dr["QTY", DataRowVersion.Original] is DBNull ? 0 : Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                        _qty_old = Convert2Decimal(dr["QTY", DataRowVersion.Original]) - Convert2Decimal(dr["QTY_OT", DataRowVersion.Original]);
                        using (DataTable _mfdt = dr.Table.DataSet.Tables[0])
                        {
                            if (null != _mfdt)
                            {
                                if (CaseInsensitiveComparer.Default.Compare("T", Convert.ToString(_mfdt.Rows[0]["CLS_LJ_ID", DataRowVersion.Original])) == 0)
                                    _qty_old = 0;//已做手动领料结案的记录，删除时不需要再扣除未发量





                            }
                        }
                    }
                    break;
            }
            _qty = _qty < 0 ? 0 : _qty;
            _qty_old = _qty_old < 0 ? 0 : _qty_old;
            #endregion
            #region 修改库存 如果没有审核流程就直接修改库存






            WH wh = new WH();
            Prdt _prdt = new Prdt();
            if (!_isRunAuditing)
            {
                //增加外派维修量






                if (!String.IsNullOrEmpty(_prdNo))
                {
                    #region 修改货品分仓存量
                    if (String.IsNullOrEmpty(_batNo))//无批号

                    {
                        wh.UpdateQty(_prdNo, _prdMark, _wh, _unit, WH.QtyTypes.QTY_ON_RSV, _qty);
                    }
                    else//批号控管
                    {
                        SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _wh);
                        Hashtable _ht = new Hashtable();
                        _ht[WH.QtyTypes.QTY_ON_RSV] = _qty;
                        if (!string.IsNullOrEmpty(_validDd))
                        {
                            if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                            {
                                TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                                if (_timeSpan.Days > 0)//未过期                            
                                    _validDd = "";
                            }
                        }
                        wh.UpdateQty(_batNo, _prdNo, _prdMark, _wh, _validDd, _unit, _ht);
                    }
                    #endregion
                }
                //减少外派维修量





                if (!String.IsNullOrEmpty(_prdNo_old))
                {
                    #region 修改货品分仓存量
                    if (String.IsNullOrEmpty(_batNo_old))
                    {
                        wh.UpdateQty(_prdNo_old, _prdMark_old, _wh_old, _unit_old, WH.QtyTypes.QTY_ON_RSV, (-1) * _qty_old);
                    }
                    else
                    {
                        SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo_old, _prdNo_old, _prdMark_old, _wh_old);
                        Hashtable _ht = new Hashtable();
                        _ht[WH.QtyTypes.QTY_ON_RSV] = (-1) * _qty_old;
                        if (!String.IsNullOrEmpty(_validDd_old))
                        {
                            if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                            {
                                TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd_old));
                                if (_timeSpan.Days > 0)
                                    _validDd_old = "";
                            }
                        }
                        wh.UpdateQty(_batNo_old, _prdNo_old, _prdMark_old, _wh_old, _validDd_old, _unit_old, _ht);
                    }
                    #endregion
                }
            }
            #endregion

        }


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
                DbMTNOut _db = new DbMTNOut(Comp.Conn_DB);
                _db.UpdateChkMan(bil_no, chk_man, cls_dd);

                SunlikeDataSet _ds = this.GetData("",chk_man, bil_id, bil_no);
                DataTable _mfTable = _ds.Tables["MF_MOUT"];
                DataTable _tfTable = _ds.Tables["TF_MOUT"];
                DataTable _tfTable1 = _ds.Tables["TF_MOUT_CL"];
                DataRow mfdr = _mfTable.Rows[0];

                #region 修改库存
                foreach (DataRow dr in _tfTable1.Rows)
                {
                    UpdateTf(dr, StatementType.Insert, false, false);
                }
                UpdateMoutCls(_ds, true);
                #endregion

                #region 回写原单
                foreach (DataRow dr in _tfTable.Rows)
                {
                    UpdateOS(dr, false, true);
                    UpdateWCH(dr, false, true);
                }
                #endregion

                #region 预收付款
                decimal _amtn = 0;
                if (!String.IsNullOrEmpty(mfdr["AMTN_IRP"].ToString()))
                {
                    _amtn = Convert.ToDecimal(mfdr["AMTN_IRP"]);
                }
                if (_amtn != 0)
                {
                    //订金含税否；税率取“营业人资料”里的本业税率，如果为零取5
                    if (mfdr["TAX_ID"].ToString() != "1" && mfdr["HS_ID"].ToString() == "T")
                    {
                        CompInfo _compInfo = Comp.GetCompInfo("");
                        decimal _psTax = _compInfo.SystemInfo.PS1_TAX;
                        //POI_WBA外位币小数位数；POI_AMT本位币小数位数


                        int _poiAmt = _compInfo.DecimalDigitsInfo.System.POI_AMT;
                        if (_psTax == 0)
                            _psTax = 5;
                        decimal _amtnNet = _amtn / (1 + _psTax / 100);
                        _amtnNet = Math.Round(_amtnNet, _poiAmt);
                        mfdr["TAX_IRP"] = _amtn - _amtnNet;
                    }
                    else
                        mfdr["TAX_IRP"] = System.DBNull.Value;

                    UpdateMon(mfdr);
                }
                #endregion


                //更新凭证
                string _vohNo = this.UpdateVohNo(_mfTable.Rows[0], StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);
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
            string _error = String.Empty;
            try
            {
                DbMTNOut _db = new DbMTNOut(Comp.Conn_DB);

                SunlikeDataSet _ds = _db.GetData(bil_id, bil_no);
                DataTable _mfTable = _ds.Tables["MF_MOUT"];
                DataTable _tfTable = _ds.Tables["TF_MOUT"];
                DataTable _tfTable1 = _ds.Tables["TF_MOUT_CL"];

                #region 检查是否可以反审核
                if (null != _mfTable && _mfTable.Rows.Count > 0)
                {
                    //单据已结案，不能反审核                        
                    string _errorMsg = this.SetCanModify("", _usr, _ds, false);
                    if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F" || Convert.ToString(_ds.ExtendedProperties["DEL"]) == "N")
                    {
                        if (_errorMsg.Length > 0)
                        {
                            return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;" + _errorMsg;//反审核失败


                        }
                        else
                        {
                            return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";//反审核失败


                        }
                    }
                }
                #endregion

                _db.UpdateChkMan(bil_no, "", DateTime.Now);

                //更新凭证
                this.UpdateVohNo(_mfTable.Rows[0], StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");

                #region 修改库存
                foreach (DataRow dr in _tfTable1.Rows)
                {
                    UpdateTf(dr, StatementType.Delete, false, true);
                }
                #endregion

                #region 回写原单
                foreach (DataRow dr in _tfTable.Rows)
                {
                    UpdateOS(dr, true, true);
                    UpdateWCH(dr, true, true);
                }
                UpdateMoutCls(_ds, true);
                #endregion

                string _rpNo = _mfTable.Rows[0]["RP_NO"].ToString();
                if (!String.IsNullOrEmpty(_rpNo))
                {
                    Bills _bills = new Bills();
                    //删除预收付款
                    _bills.DelRcvPay("1", _rpNo);
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }

        #endregion


        #region ///ICloseBill 成员

        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            return CloseBill(bil_id, bil_no, cls_name, true);
        }
        /// <summary>
        /// 反结案




        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            return CloseBill(bil_id, bil_no, cls_name, false);
        }

        private string CloseBill(string osId, string osNo, string cls_name, bool close)
        {
            DbMTNOut _dbMtn = new DbMTNOut(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMtn.GetData(osId, osNo);
            DataTable _Mf = _ds.Tables["MF_MOUT"];
            DataTable _Tf = _ds.Tables["TF_MOUT"];
            DataTable _Tf1 = _ds.Tables["TF_MOUT_CL"];
            if (_Mf.Rows.Count > 0)
            {
                DataRow _dr = _Mf.Rows[0];
                bool clsid = Convert.ToString(_dr[cls_name]) == "T";
                bool cls_id = Convert.ToString(_dr["CLS_ID"]) == "T";
                bool cls_ljid = Convert.ToString(_dr["CLS_LJ_ID"]) == "T";//用来判断最后是否要处理违法量




                bool cbid = CaseInsensitiveComparer.Default.Compare(Convert.ToString(_dr["CB_ID"]), "T") == 0;//已切成本凭证

                bool isCheck = string.IsNullOrEmpty(Convert.ToString(_dr["CHK_MAN"]));
                if (close && clsid)
                    return "RCID=COMMON.HINT.CLOSEERROR,PARAM=" + osNo;//该单据[{0}]已结案,结案动作不能完成!
                if (!close && !clsid)
                    return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + osNo;//该单据[{0}]未结案,未结案动作不能完成!
                if (!close && cbid)
                    return "RCID=COMMON.HINT.CBIDIST,PARAM=" + osNo;//该单据[{0}]维修成本已切凭证,不能反结案!

                //已经完工结案的，不能进行领料的反结案
                if (cls_id && CaseInsensitiveComparer.Default.Compare(cls_name, "CLS_ID") != 0)
                    return "RCID=COMMON.HINT.CLOSEERROR3,PARAM=" + osNo;//该单据[{0}已经完工结案,不可以再做领料反结案!

                #region 结案
                _dbMtn.DoCloseBill(osId, osNo, cls_name, close);
                if (CaseInsensitiveComparer.Default.Compare(cls_name, "CLS_ID") == 0)//手动结案的要判断领料结案标记
                {
                    _dbMtn.ClsMoutLj(osId, osNo, false);
                }
                if (cls_ljid != _dbMtn.isClosed(osId, osNo, true))//根据领料结案标记处理未发量

                {
                    ClsMoutLjRsv(_Tf1);
                }
                #endregion

                #region 数量整理old 已经注释
                //if (isCheck && close)
                //{
                //    foreach (DataRow _drTf1 in _Tf1.Rows)
                //    {
                //        decimal _qty = _drTf1["QTY"] is DBNull ? 0 : Convert.ToDecimal(_drTf1["QTY"]);
                //        decimal _qtyOt = _drTf1["QTY_OT"] is DBNull ? 0 : Convert.ToDecimal(_drTf1["QTY_OT"]);
                //        if (_qty > _qtyOt) //维修单材料量>已领量







                //        {
                //            _qty = _qtyOt - _qty; //差额[负值]
                //            string _prdNo = _drTf1["PRD_NO"].ToString();
                //            string _prdMark = _drTf1["PRD_MARK"].ToString();
                //            string _wh = _drTf1["WH"].ToString();
                //            string _batNo = _drTf1["BAT_NO"].ToString();
                //            string _validDd = _drTf1["VALID_DD"].ToString();
                //            string _unit = _drTf1["UNIT"].ToString();
                //            #region 修改货品分仓存量
                //            WH wh = new WH();
                //            Prdt _prdt = new Prdt();
                //            if (String.IsNullOrEmpty(_batNo))//无批号







                //            {
                //                wh.UpdateQty(_prdNo, _prdMark, _wh, _unit, WH.QtyTypes.QTY_ON_RSV, _qty);
                //            }
                //            else//批号控管
                //            {
                //                SunlikeDataSet _dsBat = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _wh);
                //                Hashtable _ht = new Hashtable();
                //                _ht[WH.QtyTypes.QTY_ON_RSV] = _qty;
                //                if (!string.IsNullOrEmpty(_validDd))
                //                {
                //                    if (_dsBat.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_dsBat.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                //                    {
                //                        TimeSpan _timeSpan = Convert.ToDateTime(_dsBat.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                //                        if (_timeSpan.Days > 0)//未过期                            
                //                            _validDd = "";
                //                    }
                //                }
                //                wh.UpdateQty(_batNo, _prdNo, _prdMark, _wh, _validDd, _unit, _ht);
                //            }
                //            #endregion
                //        }
                //    }
                //}
                #endregion
            }
            return "";
        }
        #endregion

        #region 回写tf_mout1单的已领料量qty_ot
        /// <summary>
        /// 回写来源单的已领料量
        /// </summary>
        /// <param name="_sqID">_sqID</param>
        /// <param name="_sqNO">_sqNO</param>
        /// <param name="_sqITM">_sqITM</param>
        /// <param name="_qty">回写库存</param>
        public void UpdateMout(string _sqID, string _sqNO, string _sqITM, decimal _qty)
        {
            DbMTNOut _db = new DbMTNOut(Comp.Conn_DB);
            _db.UpdateMoutTf1(_sqID, _sqNO, _sqITM, _qty * (-1));
            _db.ClsMoutLj(_sqID, _sqNO);

            #region 扣减仓库未发量






            using (DataTable _dt = _db.GetDataTf1(_sqID, _sqNO).Tables[0])
            {
                foreach (DataRow _dr in _dt.Select("KEY_ITM='" + _sqITM + "'")) //只有一笔记录

                {
                    string _prdNo = Convert.ToString(_dr["PRD_NO"]);
                    string _prdMark = Convert.ToString(_dr["PRD_MARK"]);
                    string _whNo = Convert.ToString(_dr["WH"]);
                    string _ut = Convert.ToString(_dr["UNIT"]);
                    string _batNo = Convert.ToString(_dr["BAT_NO"]);
                    string _validDd = Convert.ToString(_dr["VALID_DD"]);
                    decimal _qtyo = Convert2Decimal(_dr["QTY"]);
                    decimal _qtyot = Convert2Decimal(_dr["QTY_OT"]);
                    decimal _qtyold = Convert2DecimalPlus(_qtyo - (_qty + _qtyot));//旧的未发量






                    decimal _qtynew = Convert2DecimalPlus(_qtyo - _qtyot);//新的未发量






                    decimal _qtyOnRsv = _qtynew - _qtyold;

                    WH _wh = new WH();
                    if (String.IsNullOrEmpty(_batNo))//无批号

                    {
                        _wh.UpdateQty(_prdNo, _prdMark, _whNo, _ut, WH.QtyTypes.QTY_ON_RSV, _qtyOnRsv);
                    }
                    else//批号控管
                    {
                        Prdt _p = new Prdt();
                        SunlikeDataSet _ds = _p.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                        Hashtable _ht = new Hashtable();
                        _ht[WH.QtyTypes.QTY_ON_RSV] = _qtyOnRsv;
                        if (!string.IsNullOrEmpty(_validDd))
                        {
                            if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                            {
                                TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                                if (_timeSpan.Days > 0)//未过期                            
                                    _validDd = "";
                            }
                        }
                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _ut, _ht);
                    }
                }
            }
            #endregion
        }
        #endregion

        #region 判断是否需要强制退回








        /// <summary>
        /// 判断是否需要强制退回




        /// 返回表结构




        /// OT_ID,OT_NO,KEY_ITM,PRD_NO,PRD_NAME,UNIT,QTY
        /// </summary>
        /// <param name="otId">单据别</param>
        /// <param name="otId">外派维修单号</param>
        /// <returns></returns>
        public DataTable GetChkRtnInfo(string otId, string otNo)
        {
            DataTable _result = new DataTable();
            _result.Columns.Add("OT_ID", typeof(System.String));
            _result.Columns.Add("OT_NO", typeof(System.String));
            _result.Columns.Add("KEY_ITM", typeof(System.Int32));
            _result.Columns.Add("PRD_NO", typeof(System.String));
            _result.Columns.Add("PRD_NAME", typeof(System.String));
            _result.Columns.Add("UNIT", typeof(System.String));
            _result.Columns.Add("QTY", typeof(System.Decimal));
            SunlikeDataSet _dsMout = this.GetData("","",otId, otNo);
            if (_dsMout.Tables["MF_MOUT"].Rows.Count > 0 && string.Compare("T", _dsMout.Tables["MF_MOUT"].Rows[0]["CLS_ID"].ToString()) == 0)
            {
                DataRow[] _drSel = _dsMout.Tables["TF_MOUT_CL"].Select("ISNULL(CHK_RTN,'F')='T' AND ISNULL(QTY_OT,0) > 0 ");
                if (_drSel.Length > 0)
                {
                    for (int i = 0; i < _drSel.Length; i++)
                    {
                        DataRow _dr = _result.NewRow();
                        _dr["OT_ID"] = _drSel[i]["OT_ID"];
                        _dr["OT_NO"] = _drSel[i]["OT_NO"];
                        _dr["KEY_ITM"] = _drSel[i]["KEY_ITM"];
                        _dr["PRD_NO"] = _drSel[i]["PRD_NO"];
                        _dr["PRD_NAME"] = _drSel[i]["PRD_NAME"];
                        _dr["UNIT"] = _drSel[i]["UNIT"];
                        _dr["QTY"] = _drSel[i]["QTY_OT"];
                        _result.Rows.Add(_dr);
                    }
                }
            }
            return _result;
        }
        #endregion

        #region 单据结案处理未发量







        /// <summary>
        /// 单据结案处理未发量




        /// </summary>
        /// <param name="_sqID">单据别</param>
        /// <param name="_sqNO">单据号码</param>
        public void ClsMoutLjRsv(DataTable _dt)
        {
            if (_dt != null)
            {
                #region 调整仓库未发量







                DbMTNOut _db = new DbMTNOut(Comp.Conn_DB);
                foreach (DataRow _dr in _dt.Select())
                {
                    decimal _qtyOnRsv = Convert2Decimal(_dr["QTY"]) - Convert2Decimal(_dr["QTY_OT"]);//单据差量 
                    if (_qtyOnRsv > 0)
                    {
                        if (_db.isClosed(Convert.ToString(_dr["OT_ID"]), Convert.ToString(_dr["OT_NO"]), true))
                        {
                            _qtyOnRsv = (-1) * _qtyOnRsv;
                        }
                        string _prdNo = Convert.ToString(_dr["PRD_NO"]);
                        string _prdMark = Convert.ToString(_dr["PRD_MARK"]);
                        string _whNo = Convert.ToString(_dr["WH"]);
                        string _ut = Convert.ToString(_dr["UNIT"]);
                        string _batNo = Convert.ToString(_dr["BAT_NO"]);
                        string _validDd = Convert.ToString(_dr["VALID_DD"]);
                        WH _wh = new WH();
                        if (String.IsNullOrEmpty(_batNo))//无批号

                        {
                            _wh.UpdateQty(_prdNo, _prdMark, _whNo, _ut, WH.QtyTypes.QTY_ON_RSV, _qtyOnRsv);
                        }
                        else//批号控管
                        {
                            Prdt _p = new Prdt();
                            SunlikeDataSet _ds = _p.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                            Hashtable _ht = new Hashtable();
                            _ht[WH.QtyTypes.QTY_ON_RSV] = _qtyOnRsv;
                            if (!string.IsNullOrEmpty(_validDd))
                            {
                                if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                                {
                                    TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                                    if (_timeSpan.Days > 0)//未过期                            
                                        _validDd = "";
                                }
                            }
                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _ut, _ht);
                        }
                    }
                }

                #endregion
            }
        }
        private decimal Convert2Decimal(object p)
        {
            decimal _d = 0;
            if (!decimal.TryParse(Convert.ToString(p), out _d))
                _d = 0;
            return _d;
        }
        private decimal Convert2DecimalPlus(object obj)
        {
            return Convert2Decimal(obj) > 0 ? Convert2Decimal(obj) : 0;
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
                    bilNo = Convert.ToString(dr["OT_NO", DataRowVersion.Original]);
                }
                else
                {
                    wcNo = Convert.ToString(dr["WC_NO"]);
                    bilNo = Convert.ToString(dr["OT_NO"]);
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
                                _dr["BIL_ID"] = dr["OT_ID"];
                                _dr["BIL_DD"] = dr.Table.DataSet.Tables["MF_MOUT"].Rows[0]["OT_DD"];
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

        #region 回写维修成本切制标记
        /// <summary>
        /// 回写维修成本切制标记
        /// </summary>
        /// <param name="otId"></param>
        /// <param name="otNo"></param>
        /// <param name="cbId"></param>
        public void UpdateCbId(string otId, string otNo, bool cbId)
        {
            DbMTNOut _out = new DbMTNOut(Comp.Conn_DB);
            _out.UpdateCbId(otId, otNo, cbId);
        }
        #endregion


        #region 修改凭证
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
                string _otId = dr["OT_ID"].ToString();
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
                        if (string.Compare("OI", _otId) == 0 || string.Compare("OT", _otId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.MTN;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("","", _otId, dr["OT_NO"].ToString()), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _otId, out _vohNoError);
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
                        if (string.Compare("OI", _otId) == 0 || string.Compare("OT", _otId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.MTN;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("","", _otId, dr["OT_NO"].ToString()), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _otId, out _vohNoError);
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
                string _otId = dr["OT_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("OI", _otId) == 0 || string.Compare("OT", _otId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.MTN;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _otId, out _vohNoError);
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

        #region 更新销货单凭证号码
        /// <summary>
        /// 更新销货单凭证号码
        /// </summary>
        /// <param name="otId"></param>
        /// <param name="otNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string otId, string otNo, string vohNo)
        {

            DbMTNOut _out = new DbMTNOut(Comp.Conn_DB);
            _out.UpdateVohNo(otId, otNo, vohNo);
        }
        #endregion
    }
}
