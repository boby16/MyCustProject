using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Business.Data;
using Sunlike.Common.CommonVar;
using System.Data;
using System.Collections;
using Sunlike.Common.Utility;

namespace Sunlike.Business
{
    public class DRPHC : BizObject
    {
        private bool _reBuildVohNo = false;
        private string _loginUsr = string.Empty;
        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="usr">制单人</param>
        /// <param name="hcNo">单号</param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string hcNo, bool onlyFillSchema)
        {
            DbDRPHC _dbDrpHc = new DbDRPHC(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpHc.GetData(hcNo, onlyFillSchema);
            SetCanModify(_ds, usr, false, false);
            return _ds;
        }
        /// <summary>
        /// 根据PC取单
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="bilNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bilId, string bilNo)
        {
            DbDRPHC _dbDrpHc = new DbDRPHC(Comp.Conn_DB);
            return _dbDrpHc.GetData(bilId, bilNo);
        }
        private void SetCanModify(SunlikeDataSet ds, string usr, bool bCheckAuditing, bool IsRollBack)
        {
            DataTable _dtMf = ds.Tables["MF_HC"];
            DataTable _dtTf = ds.Tables["TF_HC"];
            if (_dtMf.Rows.Count > 0)
            {
                string _bill_Dep = "";
                string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                Hashtable _billRight = Users.GetBillRight("DRPHC", usr, _bill_Dep, _bill_Usr);
                ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                ds.ExtendedProperties["LCK"] = _billRight["LCK"];

                bool _bCanModify = true;

                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["VOH_NO"].ToString()))
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["HC_DD"]), "", "CLS_ACC"))
                    {
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                        _bCanModify = false;
                    }
                    //判断是否锁单
                    if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                    {
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                        _bCanModify = false;
                    }
                    //判断凭证
                    string _acNo = "";
                    DrpVoh _drpVoh = new DrpVoh();
                    string _updUsr = "";
                    if (ds.ExtendedProperties.ContainsKey("UPD_USR"))
                    {
                        _updUsr = ds.ExtendedProperties["UPD_USR"].ToString();
                    }
                    else
                    {
                        _updUsr = _dtMf.Rows[0]["USR"].ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_dtMf.Rows[0]["VOH_NO"].ToString(), _updUsr, ref _acNo);
                    if (_resVoh == 0 || _resVoh == 1)
                    {
                        ds.ExtendedProperties["BILL_VOH_AC_CONTROL"] = false;
                        ds.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                    else if (_resVoh == 2)
                    {
                        ds.ExtendedProperties["BILL_VOH_AC_CONTROL"] = true;
                        ds.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                }
                ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="throwException"></param>
        public void UpdateData(SunlikeDataSet dataSet, bool throwException)
        {
            DataTable _dtHead = dataSet.Tables["MF_HC"];
            #region 取得录入人信息
            if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = _dtHead.Rows[0]["USR"].ToString();
            }
            else
            {
                _loginUsr = _dtHead.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
            }
            #endregion
            //是否重建凭证号码
            if (dataSet.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", dataSet.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            Hashtable _ht = new Hashtable();
            _ht["MF_HC"] = "HC_NO,HC_DD,HC_TYPE,VOH_NO,USR,CHK_MAN,CLS_DATE,PRT_SW,CPY_SW,REM,MOB_ID,LOCK_MAN,SYS_DATE,PRT_USR,CANCEL_ID";
            _ht["TF_HC"] = "HC_NO,ITM,BIL_ID,PC_NO,VOH_NO,ZB_NO";
            this.UpdateDataSet(dataSet, _ht);
            if (dataSet.HasErrors)
            {
                if (throwException)
                {
                    throw new Exception(GetErrorsString(dataSet));
                }
            }
            else
            {
                dataSet.AcceptChanges();
            }
        }
        #region BeforeDsSave
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            DataTable _dtHead = ds.Tables["MF_HC"];
            DataTable _dtBody = ds.Tables["TF_HC"];
            if (_dtHead != null && _dtHead.Rows.Count > 0)
            {
                if (_dtHead.Rows[0].RowState == DataRowState.Deleted)
                {
                    UpdateVohNo( _dtHead, _dtBody, false);
                }
                else if (_dtHead.Rows[0].RowState == DataRowState.Modified)
                {
                    if (this._reBuildVohNo)
                    {
                        if (!string.IsNullOrEmpty(_dtHead.Rows[0]["VOH_NO", DataRowVersion.Original].ToString())
                            || (_dtBody.Rows.Count > 0 && !string.IsNullOrEmpty(_dtBody.Rows[0]["VOH_NO", DataRowVersion.Original].ToString()))
                            )
                        {
                            UpdateVohNo(_dtHead, _dtBody, false);
                        }
                        UpdateVohNo( _dtHead, _dtBody, true);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(_dtHead.Rows[0]["VOH_NO", DataRowVersion.Original].ToString())
                            && (_dtBody.Select().Length > 0 && string.Compare(_dtBody.Select()[0]["VOH_NO"].ToString(), _dtBody.Select()[0]["VOH_NO", DataRowVersion.Original].ToString()) != 0)
                            )
                        {
                            UpdateVohNo( _dtHead, _dtBody, false);
                            UpdateVohNo( _dtHead, _dtBody, true);
                        }
                        UpdateVohNo( _dtHead, _dtBody, false);
                    }
                }
                else if (_dtHead.Rows[0].RowState == DataRowState.Added)
                {
                    string _usr;
                    _usr = _dtHead.Rows[0]["USR"].ToString();                    
                    #region 生成单号
                    SQNO _sq = new SQNO();
                    //取得保存单号
                    _dtHead.Rows[0]["HC_NO"] = _sq.Set("HC", _usr, "", Convert.ToDateTime(_dtHead.Rows[0]["HC_DD"]), "");
                    #endregion

                    UpdateVohNo(_dtHead, _dtBody, true);
                }
            }

           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtHead"></param>
        /// <param name="dtBody"></param>
        /// <param name="isAdd"></param>
        private void UpdateVohNo(DataTable dtHead, DataTable dtBody, bool isAdd)
        {
            //错误信息报告
            DataTable _dtError = new DataTable("DT_ERROR");
            _dtError.Columns.Add("BIL_ID", typeof(System.String));
            _dtError.Columns.Add("BIL_NO", typeof(System.String));
            _dtError.Columns.Add("REM", typeof(System.String));
            _dtError.PrimaryKey = new DataColumn[2] { _dtError.Columns["BIL_ID"], _dtError.Columns["BIL_NO"] };
            //切制方式
            string _batId = "1";//1汇总切2逐单切
            string _vohId = "3";//1.转帐凭证2.现金收款3.现金付款4.银行收款5.银行付款
            DateTime _mkDd = System.DateTime.Now;//凭证日期
            string _bilNoMsg = "";//凭证的来源单号
            string _vohDepNo = "";//凭证部门
            string _vohNo = "";
            DataSet _dsBill = null;
            DataSet _dsVoh = null;
            DataRow _drNew = null;
            DrpVoh _voh = new DrpVoh();
            DrpVoh.BillSchema _billSchema = new DrpVoh.BillSchema();
            if (dtHead.DataSet.ExtendedProperties.ContainsKey("SET_VOH_DEPT"))
            {
                _vohDepNo = dtHead.DataSet.ExtendedProperties["SET_VOH_DEPT"].ToString();
            }
            if (dtHead.Rows.Count == 0)
                return;
            if (isAdd)
            {
                _batId = dtHead.Rows[0]["HC_TYPE"].ToString();
                _mkDd = Convert.ToDateTime(dtHead.Rows[0]["HC_DD"]);                
                _bilNoMsg = "HC&" + dtHead.Rows[0]["HC_NO"].ToString();
            }
            else
            {
                _batId = dtHead.Rows[0]["HC_TYPE", DataRowVersion.Original].ToString();                
            }
            if (string.IsNullOrEmpty(_vohDepNo))
            {
                if (dtBody.Rows.Count > 0)
                {
                    if (dtBody.Rows[0].RowState == DataRowState.Deleted)
                        _vohDepNo = dtBody.Rows[0]["DEP", DataRowVersion.Original].ToString();
                    else
                        _vohDepNo = dtBody.Rows[0]["DEP"].ToString();
                }
            }
            if (isAdd)
            {
                if (String.Compare("1", _batId) == 0)//1汇总切
                {
                    #region 1汇总切
                    DataRow[] _drSel = dtBody.Select();
                    _billSchema = _voh.GetBillSchema("ZG");
                    foreach (DataRow _drBody in _drSel)
                    {
                        DRPZG _zg = new DRPZG();
                        _dsBill = _zg.GetData("", "ZB", _drBody["ZB_NO"].ToString(), false);
                        foreach (DataRow drZb in _dsBill.Tables["TF_ZG"].Rows)
                        {
                            if (!string.IsNullOrEmpty(drZb["AMT"].ToString()))
                            {
                                drZb["AMT"] = (-1) * Convert.ToDecimal(drZb["AMT"].ToString());
                            }
                            if (!string.IsNullOrEmpty(drZb["AMTN_NET"].ToString()))
                            {
                                drZb["AMTN_NET"] = (-1) * Convert.ToDecimal(drZb["AMTN_NET"].ToString());
                            }
                            if (!string.IsNullOrEmpty(drZb["TAX"].ToString()))
                            {
                                drZb["TAX"] = (-1) * Convert.ToDecimal(drZb["TAX"].ToString());
                            }
                        }
                        bool _makeVohNo = _voh.BuildVoucher(ref _dsVoh, _dsBill, _billSchema);
                        if (!_makeVohNo)
                        {
                            if (!string.IsNullOrEmpty(_drBody["PC_NO"].ToString()))
                            {
                                _drNew = _dtError.NewRow();
                                _drNew["BIL_ID"] = _drBody["BIL_ID"];
                                _drNew["BIL_NO"] = _drBody["PC_NO"];
                                _dtError.Rows.Add(_drNew);
                            }
                        }
                    }
                    if (_dsVoh.Tables["VDTL"].Rows.Count > 0)
                    {
                        if (_dsVoh != null)
                        {
                            #region 指定制单日期、部门、凭证日期
                            foreach (DataRow drVhed in _dsVoh.Tables["VHED"].Rows)
                            {
                                //设置凭证类型

                                _vohId =  drVhed["VOH_ID"].ToString();

                                //设置制单日期
                                drVhed["MAK_DAT"] = _mkDd;
                                //设置凭证日期

                                //drVhed["VOH_DAT"] = _mkDd;
                                if (!string.IsNullOrEmpty(_vohDepNo))
                                    drVhed["DEP"] = _vohDepNo;
                                //录入人
                                drVhed["USR"] = this._loginUsr;
                                drVhed["REM"] = _bilNoMsg;
                            }
                            foreach (DataRow drVdtl in _dsVoh.Tables["VDTL"].Rows)
                            {
                                //设置凭证类型
                                drVdtl["VOH_ID"] = _vohId;
                                //设置凭证日期
                                //drVdtl["VOH_DAT"] = _mkDd;
                                if (!string.IsNullOrEmpty(_vohDepNo))
                                    drVdtl["DEP"] = _vohDepNo;
                                //来源单信息
                                drVdtl["BIL_NO"] = _bilNoMsg;
                            }
                            #endregion
                        }

                        _voh.SortVohcher(_dsVoh);
                        #region 汇总凭证
                        string[] _primarkFields = null;
                        string[] _groupFields = null;
                        _primarkFields = new string[] { "DC", "ACC_NO", "ARP_DAT", "DEP", "OBJ", "CLS_ID1", "CLS_ID2", "CLS_ID3", "CLS_ID4" };
                        _groupFields = new string[] { "AMT", "AMTN" };
                        this.DataGroup(_dsVoh, "VHED", new string[1] { "MAK_NO" }, new string[1] { "AMT" });
                        this.DataGroup(_dsVoh, "VDTL", _primarkFields, _groupFields);
                        #endregion
                        Users _users = new Users();
                        DataTable _dtUsers = _users.GetData(this._loginUsr);
                        string _usrDepNo = "";
                        if (_dtUsers.Rows.Count > 0)
                        {
                            _usrDepNo = _dtUsers.Rows[0]["DEP"].ToString();
                        }
                        CompInfo _compInfo = Comp.GetCompInfo(_usrDepNo);
                        if (_compInfo.VoucherInfo.VOH_POS)
                        {
                            _dsVoh.ExtendedProperties["VH_TYPE"] = "1";
                            
                        }
                        else
                        {
                            _dsVoh.ExtendedProperties["VH_TYPE"] = "2";
                           
                        }
                        if (_compInfo.VoucherInfo.CHK_UNSH_VOH)
                        {
                            _dsVoh.ExtendedProperties["CHK_UNSH_VOH"] = "T";
                        }
                        else
                        {
                            _dsVoh.ExtendedProperties["CHK_UNSH_VOH"] = "F";
                        }
                        _dsVoh.ExtendedProperties["BAT_ID"] = _batId;
                        _voh.UpdateData(_dsVoh, true);
                        string _makNo = "";
                        if (_dsVoh.Tables["VHED"].Rows.Count > 0)
                        {
                            _makNo = _dsVoh.Tables["VHED"].Rows[0]["MAK_NO"].ToString();
                        }
                        if (dtHead.Rows.Count > 0)
                        {
                            dtHead.Rows[0]["VOH_NO"] = _makNo;
                        }
                    }
                    else
                    {
                        dtHead.Rows[0]["VOH_NO"] = "";
                    }

                    #endregion
                }
                else if (String.Compare("2", _batId) == 0)//2逐单切
                {
                    #region 2逐单切
                    DataRow[] _drSel = dtBody.Select();
                    foreach (DataRow _drBody in _drSel)
                    {
                        _vohDepNo = _drBody["DEP"].ToString();
                        _billSchema = _voh.GetBillSchema("ZG");
                        _dsVoh = null;
                        DRPZG _zg = new DRPZG();
                        _dsBill = _zg.GetData("", "ZB", _drBody["ZB_NO"].ToString(), false);
                        foreach (DataRow drZb in _dsBill.Tables["TF_ZG"].Rows)
                        {
                            if (!string.IsNullOrEmpty(drZb["AMT"].ToString()))
                            {
                                drZb["AMT"] = (-1) * Convert.ToDecimal(drZb["AMT"].ToString());
                            }
                            if (!string.IsNullOrEmpty(drZb["AMTN_NET"].ToString()))
                            {
                                drZb["AMTN_NET"] = (-1) * Convert.ToDecimal(drZb["AMTN_NET"].ToString());
                            }
                            if (!string.IsNullOrEmpty(drZb["TAX"].ToString()))
                            {
                                drZb["TAX"] = (-1) * Convert.ToDecimal(drZb["TAX"].ToString());
                            }
                        }
                        bool _makeVohNo = _voh.BuildVoucher(ref _dsVoh, _dsBill, _billSchema);
                        if (_makeVohNo)
                        {
                            if (_dsVoh != null)
                            {
                                #region 指定制单日期、部门、凭证日期 
                                foreach (DataRow drVhed in _dsVoh.Tables["VHED"].Rows)
                                {
                                    //设置凭证类型

                                    _vohId = drVhed["VOH_ID"].ToString();

                                    //设置制单日期
                                    drVhed["MAK_DAT"] = _mkDd;
                                    //设置凭证日期
                                    //drVhed["VOH_DAT"] = _mkDd;
                                    if (!string.IsNullOrEmpty(_vohDepNo))
                                        drVhed["DEP"] = _vohDepNo;
                                    //录入人
                                    drVhed["USR"] = this._loginUsr;
                                    drVhed["REM"] = _bilNoMsg;
                                }
                                foreach (DataRow drVdtl in _dsVoh.Tables["VDTL"].Rows)
                                {
                                    //设置凭证类型
                                    drVdtl["VOH_ID"] = _vohId;
                                    //设置凭证日期
                                    //drVdtl["VOH_DAT"] = _mkDd;
                                    if (!string.IsNullOrEmpty(_vohDepNo))
                                        drVdtl["DEP"] = _vohDepNo;
                                    //来源单信息
                                    drVdtl["BIL_NO"] = _bilNoMsg;
                                }
                                #endregion
                            }
                            _voh.SortVohcher(_dsVoh);
                            #region 汇总凭证
                            string[] _primarkFields = null;
                            string[] _groupFields = null;
                            //3.依科目+部门
                            _primarkFields = new string[] { "DC", "ACC_NO", "ARP_DAT", "DEP", "OBJ", "CLS_ID1", "CLS_ID2", "CLS_ID3", "CLS_ID4" };
                            _groupFields = new string[] { "AMT", "AMTN" };
                            this.DataGroup(_dsVoh, "VHED", new string[1] { "MAK_NO" }, new string[1] { "AMT" });
                            this.DataGroup(_dsVoh, "VDTL", _primarkFields, _groupFields);
                            #endregion
                            Users _users = new Users();
                            DataTable _dtUsers = _users.GetData(this._loginUsr);
                            string _usrDepNo = "";
                            if (_dtUsers.Rows.Count > 0)
                            {
                                _usrDepNo = _dtUsers.Rows[0]["DEP"].ToString();
                            }
                            CompInfo _compInfo = Comp.GetCompInfo(_usrDepNo);
                            if (_compInfo.VoucherInfo.VOH_POS)
                            {
                                _dsVoh.ExtendedProperties["VH_TYPE"] = "1";
                              
                            }
                            else
                            {
                                _dsVoh.ExtendedProperties["VH_TYPE"] = "2";
                               
                            }
                            if (_compInfo.VoucherInfo.CHK_UNSH_VOH)
                            {
                                _dsVoh.ExtendedProperties["CHK_UNSH_VOH"] = "T";
                            }
                            else
                            {
                                _dsVoh.ExtendedProperties["CHK_UNSH_VOH"] = "F";
                            }
                            _dsVoh.ExtendedProperties["BAT_ID"] = _batId;
                            _voh.UpdateData(_dsVoh, true);
                            string _makNo = "";
                            if (_dsVoh.Tables["VHED"].Rows.Count > 0)
                            {
                                _makNo = _dsVoh.Tables["VHED"].Rows[0]["MAK_NO"].ToString();
                            }
                            _drBody["VOH_NO"] = _makNo;
                        }
                        else
                        {
                            if (!_makeVohNo)
                            {
                                if (!string.IsNullOrEmpty(_drBody["PC_NO"].ToString()))
                                {
                                    _drNew = _dtError.NewRow();
                                    _drNew["BIL_ID"] = _drBody["BIL_ID"];
                                    _drNew["BIL_NO"] = _drBody["PC_NO"];
                                    _dtError.Rows.Add(_drNew);
                                }
                            }
                        }
                    }
                    #endregion
                }
                if (dtHead.DataSet.Tables.Contains("DT_ERROR"))
                {
                    dtHead.DataSet.Tables.Remove("DT_ERROR");
                }
                dtHead.DataSet.Tables.Add(_dtError);
                #region 表身无数据时则不允许保存
                if (dtBody.Select().Length == 0)
                {
                    string _errorMsg = "";
                    _errorMsg = "RCID=COMMON.HINT.NOBODY";
                    foreach (DataRow dr in _dtError.Rows)
                    {
                        _errorMsg += ";RCID=COMMON.HINT.DCEQUAL2,PARAM=" + dr["BIL_NO"].ToString();
                    }
                    throw new SunlikeException(_errorMsg);
                }
                #endregion
            }
            else
            {
                if (String.Compare("1", _batId) == 0)//1汇总切
                {
                    _vohNo = dtHead.Rows[0]["VOH_NO", DataRowVersion.Original].ToString();
                    if (!string.IsNullOrEmpty(_vohNo))
                    {
                        _voh.DeleteVoucher(_vohNo);
                    }
                }
                else if (String.Compare("2", _batId) == 0)//2逐单切
                {
                    foreach (DataRow drDel in dtBody.Rows)
                    {
                        if (drDel.RowState == DataRowState.Added)
                            continue;
                        _vohNo = drDel["VOH_NO", DataRowVersion.Original].ToString();
                        if (!string.IsNullOrEmpty(_vohNo))
                        {
                            _voh.DeleteVoucher(_vohNo);
                        }
                    }
                }
            }
        }
        #endregion

        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            if (tableName == "MF_HC")
            {
                string _usr;
                if (statementType == StatementType.Delete)
                {
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = dr["USR"].ToString();
                }
                if (statementType == StatementType.Insert)
                {
                    dr["CHK_MAN"] = _usr;
                    dr["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                }
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //取得保存单号
                    dr["HC_NO"] = _sq.Set("HC", _usr, "", Convert.ToDateTime(dr["HC_DD"]), "");
                }
                else if (statementType == StatementType.Delete)
                {
                    string _error = _sq.Delete(dr["HC_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                }
            }
            else if (tableName == "TF_HC")
            {
                #region 回写暂估退回单
                if (statementType == StatementType.Insert)
                {
                    UpdateVohNo(dr, false);
                }
                else if (statementType == StatementType.Update)
                {
                    UpdateVohNo(dr, true);
                    UpdateVohNo(dr, false);
                }
                else if (statementType == StatementType.Delete)
                {
                    UpdateVohNo(dr, true);
                }
                #endregion
            }
        }

        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            if (tableName == "TF_HC")
            {
                DRPZG _drpZg = new DRPZG();
                if (statementType == StatementType.Insert)
                {
                    _drpZg.UpdateHcId("ZB", dr["ZB_NO"].ToString(), "T");
                }
                else if (statementType == StatementType.Delete)
                {
                    _drpZg.UpdateHcId("ZB", dr["ZB_NO", DataRowVersion.Original].ToString(), "F");
                }
            }
        }
        #region 回写单据凭证号码
        /// <summary>
        /// 回写单据凭证号码
        /// </summary>
        /// <param name="dr">TF_HC表身行</param>
        /// <param name="isDel">是否删除</param>
        private void UpdateVohNo(DataRow dr, bool isDel)
        {
            string _bilId = "";
            string _bilNo = "";
            string _vohNo = "";
            if (dr.Table.DataSet.Tables["MF_HC"].Rows.Count > 0 )
            {
                if (dr.Table.DataSet.Tables["MF_HC"].Rows[0].RowState == DataRowState.Deleted)
                {
                    if (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_HC"].Rows[0]["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        _vohNo = dr.Table.DataSet.Tables["MF_HC"].Rows[0]["VOH_NO", DataRowVersion.Original].ToString();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_HC"].Rows[0]["VOH_NO"].ToString()))
                    {
                        _vohNo = dr.Table.DataSet.Tables["MF_HC"].Rows[0]["VOH_NO"].ToString();
                    }
                }
            }
            if (isDel)
            {
                _bilId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _bilNo = dr["ZB_NO", DataRowVersion.Original].ToString();
                _vohNo = "";
            }
            else
            {
                _bilId = dr["BIL_ID"].ToString();
                _bilNo = dr["ZB_NO"].ToString();
                if (!string.IsNullOrEmpty(dr["VOH_NO"].ToString()))
                    _vohNo = dr["VOH_NO"].ToString();
            }
            if (string.Compare("PC", _bilId) == 0
                
            )
            {
                DRPZG _zg = new DRPZG();
                _zg.UpdateVohNo("ZB", _bilNo, _vohNo);
            }
        }
        #endregion

        #region 根据条件汇总表数据
        /// <summary>
        /// 根据条件汇总表数据
        /// </summary>
        /// <param name="originalData"></param>
        /// <param name="dataMember"></param>        
        /// <param name="primaryFields"></param>
        /// <param name="GroupFields"></param>
        private void DataGroup(DataSet originalData, string dataMember, string[] primaryFields, string[] GroupFields)
        {
            if (originalData == null)
                return;
            if (primaryFields == null)
                return;
            string[] _repeatValue = new string[primaryFields.Length];
            bool _clearRepeatValue = true;
            bool _isRepeatValue = false;
            DataRow _drFirstRow = null;
            DataRow[] _drSel = originalData.Tables[dataMember].Select();
            for (int selectCount = 0; selectCount < _drSel.Length; selectCount++)
            {
                if (_clearRepeatValue)
                {
                    //载入主键值
                    for (int i = 0; i < _repeatValue.Length; i++)
                    {
                        if (_drSel[selectCount].Table.Columns.Contains(primaryFields[i]))
                            _repeatValue[i] = _drSel[selectCount][primaryFields[i]].ToString();
                    }
                    _drFirstRow = _drSel[selectCount];
                    _clearRepeatValue = false;
                }
                else
                {
                    _isRepeatValue = true;
                    for (int i = 0; i < _repeatValue.Length; i++)
                    {
                        if (_drSel[selectCount].Table.Columns.Contains(primaryFields[i]))
                        {
                            //判断主键是否重复
                            if (string.Compare(_repeatValue[i], _drSel[selectCount][primaryFields[i]].ToString()) != 0)
                            {
                                _isRepeatValue = false;
                                break;
                            }
                        }
                    }
                    if (_isRepeatValue)
                    {
                        //汇总数据
                        for (int i = 0; i < GroupFields.Length; i++)
                        {
                            if (_drFirstRow != null && _drSel[selectCount].Table.Columns.Contains(GroupFields[i]))
                            {
                                decimal _group = 0;
                                if (!string.IsNullOrEmpty(_drFirstRow[GroupFields[i]].ToString()))
                                {
                                    _group = Convert.ToDecimal(_drFirstRow[GroupFields[i]]);
                                }
                                if (!string.IsNullOrEmpty(_drSel[selectCount][GroupFields[i]].ToString()))
                                {
                                    _group += Convert.ToDecimal(_drSel[selectCount][GroupFields[i]]);
                                }
                                _drFirstRow[GroupFields[i]] = _group;
                            }
                        }
                        _drSel[selectCount].Delete();
                    }
                    else
                    {
                        //载入主键值
                        for (int i = 0; i < _repeatValue.Length; i++)
                        {
                            if (_drSel[selectCount].Table.Columns.Contains(primaryFields[i]))
                                _repeatValue[i] = _drSel[selectCount][primaryFields[i]].ToString();
                        }
                        _drFirstRow = _drSel[selectCount];
                        _clearRepeatValue = false;
                    }
                }

            }
        }
        #endregion
    }
}
