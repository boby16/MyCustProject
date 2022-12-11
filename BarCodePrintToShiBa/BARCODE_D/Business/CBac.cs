using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;

namespace Sunlike.Business
{
    /// <summary>
    /// 客户帐户变动单
    /// </summary>
    public class CBac : BizObject, IAuditing
    {
        private string _bcId = "";
        private string _loginUsr = "";
        private bool _isRunAuditing;
        private string _saveID = "";
        private bool _hasAmtnCbacSO = false;
        private bool _reBuildVohNo = false;
        private bool _noMakeVohNo = false;

        #region 构造函数
        /// <summary>
        /// 客户帐户变动单
        /// </summary>
        public CBac()
        { }
        #endregion

        #region 取得数据
        /// <summary>
        /// 取得数据
        /// </summary>
        /// <param name="bcId"></param>
        /// <param name="bcNo"></param>
        /// <param name="isSchema"></param>
        /// <returns>BC_ID,BC_NO,ADD_ID,AMTN,BACC_NO,BACC_SW,BC_DD,BIL_ID,BIL_NO,CHK_MAN,CLS_DATE,CR_NO,CUS_NO,DEP,DR_NO,MOB_ID,PAY_TYPE,PRT_USR,REM,SAL_NO,SAVE_ID,SYS_DATE,USR</returns>
        public SunlikeDataSet GetData(string bcId, string bcNo, bool isSchema)
        {
            return GetData("", bcId, bcNo, isSchema);
        }
        /// <summary>
        /// 取得数据
        /// </summary>
        /// <param name="bcId"></param>
        /// <param name="bcNo"></param>
        /// <param name="isSchema"></param>
        /// <returns>BC_ID,BC_NO,ADD_ID,AMTN,BACC_NO,BACC_SW,BC_DD,BIL_ID,BIL_NO,CHK_MAN,CLS_DATE,CR_NO,CUS_NO,DEP,DR_NO,MOB_ID,PAY_TYPE,PRT_USR,REM,SAL_NO,SAVE_ID,SYS_DATE,USR</returns>
        public SunlikeDataSet GetData(string usr, string bcId, string bcNo, bool isSchema)
        {
            SunlikeDataSet _ds = null;
            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            _ds = _cbac.GetData(bcId, bcNo, isSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //增加单据权限
            if (!isSchema)
            {
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    string _pgm = "MONCBC";
                    DataTable _dtMf = _ds.Tables["MF_CBAC"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                        this.SetCanModify(_ds, true);
                    }
                }
            }
            return _ds;
        }
        /// <summary>
        /// 取得数据
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="bilNo"></param>
        /// <returns>BC_ID,BC_NO,ADD_ID,AMTN,BACC_NO,BACC_SW,BC_DD,BIL_ID,BIL_NO,CHK_MAN,CLS_DATE,CR_NO,CUS_NO,DEP,DR_NO,MOB_ID,PAY_TYPE,PRT_USR,REM,SAL_NO,SAVE_ID,SYS_DATE,USR</returns>
        public SunlikeDataSet GetDataBilNo(string bilId, string bilNo)
        {
            SunlikeDataSet _ds = null;
            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            _ds = _cbac.GetDataBilNo(bilId, bilNo);
            SetCanModify(_ds, true);
            return _ds;
        }

        #region  检查单据是否可以修改
        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            string errorMsg = "";
            bool _bCanModify = true;
            DataRow[] _drHead = ds.Tables["MF_CBAC"].Select(); ;
            if (_drHead.Length > 0)
            {
                //有来源单号时不能修改                
                if (_bCanModify && _drHead[0]["BIL_NO"].ToString().Length > 0)
                {
                    _bCanModify = false;
                    errorMsg += "MON.BILLS.BILNOEXIST";//"有来源单号,不能更改";
                }
                //判断审核流程
                if (_bCanModify && bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_drHead[0]["BC_ID"].ToString(), _drHead[0]["BC_NO"].ToString()))
                    {
                        _bCanModify = false;
                        errorMsg += "COMMON.HINT.INTOAUT";
                    }
                }
                if (_bCanModify && !String.IsNullOrEmpty(_drHead[0]["VOH_NO"].ToString()))
                {
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
                        _updUsr = _drHead[0]["USR"].ToString().ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_drHead[0]["VOH_NO"].ToString(), _updUsr, ref _acNo);
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
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return errorMsg;
        }
        #endregion
        #endregion

        #region 保存客户帐户变动单
        #region 保存
        /// <summary>
        /// 保存客户帐户变动单
        /// 扩展属性:
        /// SAVE_ID 是否审核 T:审核，F:未审核
        /// CHK_CUST_BAC 是否判断客户储值余额 T:判断，F：不判断
        ///  NO_MAK_VOH_NO:是否产生凭证(True:不产生凭证)
        /// </summary>
        /// <param name="changeDs"></param>
        /// <param name="bubbleException"></param>
        public DataTable UpdateData(SunlikeDataSet changeDs, bool bubbleException)
        {
            if (changeDs.Tables["MF_CBAC"].Rows.Count > 0)
            {
                if (changeDs.Tables["MF_CBAC"].Rows[0].RowState == DataRowState.Deleted)
                {
                    _loginUsr = changeDs.Tables["MF_CBAC"].Rows[0]["USR", DataRowVersion.Original].ToString();
                    _bcId = changeDs.Tables["MF_CBAC"].Rows[0]["BC_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    _loginUsr = changeDs.Tables["MF_CBAC"].Rows[0]["USR"].ToString();
                    _bcId = changeDs.Tables["MF_CBAC"].Rows[0]["BC_ID"].ToString(); ;
                }
            }
            //是否重建凭证号码
            if (changeDs.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", changeDs.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            //是否产生凭证号码
            if (changeDs.ExtendedProperties.ContainsKey("NO_MAK_VOH_NO"))
            {
                if (string.Compare("True", changeDs.ExtendedProperties["NO_MAK_VOH_NO"].ToString()) == 0)
                {
                    this._noMakeVohNo = true;
                }
            }
            //是否审核
            if (changeDs.ExtendedProperties.Contains("SAVE_ID"))
            {
                _saveID = changeDs.ExtendedProperties["SAVE_ID"].ToString();
            }
            //判断是否走审核流程
            if (changeDs.ExtendedProperties.Contains("HAS_AMTN_CBAC_SO"))
            {
                if (string.Compare("T", changeDs.ExtendedProperties["HAS_AMTN_CBAC_SO"].ToString()) == 0)
                {
                    _hasAmtnCbacSO = true;
                }
            }
            //判断是否走审核流程
            Auditing _auditing = new Auditing();
            DataRow _dr = changeDs.Tables["MF_CBAC"].Rows[0];
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
            //_isRunAuditing = _auditing.IsRunAuditing(_bcId, _loginUsr, _bilType, _mobID);


            if (string.Compare(this._saveID, "T") == 0)
            {
                this._isRunAuditing = false;
            }
            else if (string.Compare(this._saveID, "F") == 0)
            {
                this._isRunAuditing = true;
            }
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht.Add("MF_CBAC", "BC_ID,BC_NO,ADD_ID,AMTN,BACC_NO,BACC_SW,BC_DD,BIL_ID,BIL_NO,CHK_MAN,CLS_DATE,CR_NO,CUS_NO,DEP,DR_NO,MOB_ID,PAY_TYPE,PRT_SW,PRT_USR,REM,SAL_NO,SAVE_ID,SYS_DATE,USR,VOH_NO");
            _ht.Add("PAY_B2C", "BIL_ID,BIL_NO,PAY_REM,PAY_NO,PAY_ID,PAY_DD,REM");
            this.UpdateDataSet(changeDs, _ht);
            if (changeDs.HasErrors)
            {
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changeDs);
                    throw new SunlikeException("RCID=CBac.UpdateData() Error:;" + _errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changeDs);
                }
            }
            return Sunlike.Business.BizObject.GetAllErrors(changeDs);
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
            //DataTable _dt = ds.Tables["MF_CBAC"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["BC_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["BC_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion


            base.BeforeDsSave(ds);
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
            if (tableName == "PAY_B2C")
            {
                if (statementType == StatementType.Delete)
                {
                    if (!String.IsNullOrEmpty(dr["PAY_NO", DataRowVersion.Original].ToString()))
                    {
                        dr.SetColumnError("PAY_NO",/*此单存在银行付款单号，不能删除。*/"RCID=INV.HINT.PAY_NO_NOTNULL");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
            }
            if (tableName == "MF_CBAC")
            {
                if (statementType == StatementType.Insert)
                {

                    #region --生成单号
                    SQNO SunlikeSqNo = new SQNO();
                    DateTime _dtBcDd = System.DateTime.Now;
                    if (dr["BC_DD"] is System.DBNull)
                    {
                        _dtBcDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _dtBcDd = Convert.ToDateTime(dr["BC_DD"]);
                    }
                    string _bcNo = SunlikeSqNo.Set("BC", _loginUsr, dr["DEP"].ToString(), _dtBcDd, "");
                    dr["BC_NO"] = _bcNo;
                    dr["BC_DD"] = _dtBcDd.ToString(Comp.SQLDateFormat);
                    #endregion
                }

                if (statementType == StatementType.Insert)
                {
                    #region 缺省值
                    dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    if (string.IsNullOrEmpty(dr["ADD_ID"].ToString()))
                    {
                        dr["ADD_ID"] = "+";
                    }

                    if (dr.Table.DataSet.ExtendedProperties.ContainsKey("RG_TRAN"))
                    {
                        dr["CR_NO"] = Comp.DRP_Prop["CHZH_CR_NO"];
                    }
                    #endregion
                }
                if (statementType != StatementType.Delete)
                {

                    #region 检测
                    //客户代号(必添)
                    string _cusNo = dr["CUS_NO"].ToString();
                    Cust SunlikeCust = new Cust();
                    if (SunlikeCust.IsExist(_loginUsr, _cusNo, Convert.ToDateTime(dr["BC_DD"])) == false)
                    {
                        dr.SetColumnError("CUS_NO",/*客户代号不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + _cusNo + "");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //收款则一定要输入企业帐户
                    if (string.Compare("T", dr["BACC_SW"].ToString()) == 0)
                    {

                        if (string.IsNullOrEmpty(dr["BACC_NO"].ToString()))
                        {
                            dr.SetColumnError("BACC_NO",/*收款方式为收款时，企业帐户必须输入*/"RCID=COMMON.HINT.BACCNOISNULL");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //增减项判断
                    if (!string.IsNullOrEmpty(dr["ADD_ID"].ToString()))
                    {
                        if (string.Compare("+", dr["ADD_ID"].ToString()) != 0 && string.Compare("-", dr["ADD_ID"].ToString()) != 0)
                        {
                            dr.SetColumnError("ADD_ID",/*符号设置错误*/"RCID=MON.HINT.ADDIDERROR");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                    }

                    #endregion

                    #region 判断缓存
                    if (statementType == StatementType.Update)
                    {
                        if (string.Compare("T", dr["SAVE_ID", DataRowVersion.Original].ToString()) == 0 && string.Compare("T", dr["SAVE_ID"].ToString()) != 0)
                        {
                            dr.SetColumnError("SAVE_ID",/*客户代号不存在或没有对其操作的权限,请检查*/"RCID=MON.HINT.SAVE_IDERROR");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion

                    #region 判断客户帐户余额

                    if (!_hasAmtnCbacSO)
                    {

                        //判断是否从受订单转入，是则先要加回受订的客户储值的量
                        string _bilId = dr["BIL_ID"].ToString();
                        string _bilNo = dr["BIL_NO"].ToString();
                        decimal _amtnCustBac = GetCustBac(false, _cusNo);
                        decimal _flag = 1;


                        if (statementType == StatementType.Insert)
                        {
                            if (!string.IsNullOrEmpty(dr["AMTN"].ToString()))
                            {
                                if (string.Compare("-", dr["ADD_ID"].ToString()) == 0)
                                {
                                    _flag = -1;
                                }
                                _amtnCustBac += _flag * Convert.ToDecimal(dr["AMTN"]);
                            }
                        }
                        else if (statementType == StatementType.Update)
                        {
                            _flag = 1;
                            if (!string.IsNullOrEmpty(dr["AMTN", DataRowVersion.Original].ToString()))
                            {
                                if (string.Compare("-", dr["ADD_ID", DataRowVersion.Original].ToString()) == 0)
                                {
                                    _flag = -1;
                                }
                                _amtnCustBac -= _flag * Convert.ToDecimal(dr["AMTN", DataRowVersion.Original]);
                            }
                            if (!string.IsNullOrEmpty(dr["AMTN"].ToString()))
                            {
                                if (string.Compare("-", dr["ADD_ID"].ToString()) == 0)
                                {
                                    _flag = -1;
                                }
                                _amtnCustBac += _flag * Convert.ToDecimal(dr["AMTN"]);
                            }
                        }
                        if (_amtnCustBac < 0)
                        {
                            dr.SetColumnError("AMTN",/*客户帐户余额不足*/"RCID=MON.HINT.CUST_BAC_LITTLE");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion
                }

                if (statementType == System.Data.StatementType.Delete)
                {
                    #region 删除判断客户帐户余额 不能为0
                    if (string.Compare("T", dr["SAVE_ID", DataRowVersion.Original].ToString()) != 0)
                    {
                        decimal _flag = 1;
                        decimal _amtnCustBac = GetCustBac(false, dr["CUS_NO", DataRowVersion.Original].ToString());
                        _flag = 1;
                        if (!string.IsNullOrEmpty(dr["AMTN", DataRowVersion.Original].ToString()))
                        {
                            if (string.Compare("-", dr["ADD_ID", DataRowVersion.Original].ToString()) == 0)
                            {
                                _flag = -1;
                            }
                            _amtnCustBac -= _flag * Convert.ToDecimal(dr["AMTN", DataRowVersion.Original]);
                        }

                        if (_amtnCustBac < 0)
                        {
                            dr.SetColumnError("AMTN",/*客户帐户余额不足*/"RCID=MON.HINT.CUST_BAC_LITTLE");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion
                }

                if (String.IsNullOrEmpty(_saveID))
                {
                    //#region 审核关联
                    //AudParamStruct _aps;
                    //if (statementType != StatementType.Delete)
                    //{
                    //    _aps.BIL_TYPE = "";
                    //    _aps.BIL_ID = _bcId;
                    //    _aps.BIL_NO = dr["BC_NO"].ToString();
                    //    _aps.BIL_DD = string.IsNullOrEmpty(dr["BC_DD"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BC_DD"]);
                    //    _aps.USR = _loginUsr;
                    //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                    //    _aps.DEP = dr["DEP"].ToString();
                    //    _aps.SAL_NO = "";
                    //    _aps.MOB_ID = ""; //新加的部分，对应审核模板
                    //}
                    //else
                    //    _aps = new AudParamStruct(_bcId, Convert.ToString(dr["BC_NO", DataRowVersion.Original]));


                    //Auditing _auditing = new Auditing();
                    //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                    //if (!string.IsNullOrEmpty(_auditErr))
                    //{
                    //    throw new SunlikeException(_auditErr);
                    //}
                    //#endregion
                }

                #region 产生凭证号码
                if (!this._noMakeVohNo && !this._isRunAuditing)
                {
                    UpdateVohNo(dr, statementType);
                }
                #endregion
            }
            base.BeforeUpdate(tableName, statementType, dr, ref status);
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
            if (tableName == "MF_CBAC")
            {
                #region 删除单号
                if (statementType == StatementType.Delete)
                {
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(dr["BC_NO", DataRowVersion.Original].ToString(), _loginUsr);//删除时在BILD中插入一笔数据
                }
                #endregion

                #region 更新客户账户

                if (statementType == StatementType.Insert)
                {
                    UpdateCustBac(dr, true, _hasAmtnCbacSO);
                }
                else if (statementType == StatementType.Update)
                {
                    UpdateCustBac(dr, false, _hasAmtnCbacSO);
                    UpdateCustBac(dr, true, _hasAmtnCbacSO);
                }
                else if (statementType == StatementType.Delete)
                {
                    UpdateCustBac(dr, false, _hasAmtnCbacSO);
                }
                #endregion

                #region 生成帐户收支单
                if (!this._isRunAuditing)
                {
                    if (statementType == StatementType.Insert)
                    {
                        MakeBac(dr, true);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        MakeBac(dr, false);
                        MakeBac(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        MakeBac(dr, false);
                    }
                }
                #endregion
            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        #endregion



        #region 更新客户账户余额
        /// <summary>
        /// 更新客户账户余额
        /// </summary>
        /// <param name="dr">MF_CBAC行信息</param>   
        /// <param name="isNew">是否新增</param>
        /// <param name="hasAmtnCbacSO">是否来源受订</param>
        private void UpdateCustBac(DataRow dr, bool isNew, bool hasAmtnCbacSO)
        {
            string _cusNo = "";
            string _chkMan = "";
            string _addId = "+";
            string _saveId = "T";
            string _bilId = "";
            string _bilNo = "";
            decimal _addFlag = 1;
            decimal _amtn = 0;
            if (isNew)
            {
                _cusNo = dr["CUS_NO"].ToString();
                _chkMan = dr["CHK_MAN"].ToString();
                if (!string.IsNullOrEmpty(dr["ADD_ID"].ToString()))
                    _addId = dr["ADD_ID"].ToString();
                _bilId = dr["BIL_ID"].ToString();
                _bilNo = dr["BIL_NO"].ToString();
                if (!string.IsNullOrEmpty(dr["SAVE_ID"].ToString()))
                    _saveId = dr["SAVE_ID"].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN"].ToString()))
                    _amtn = Convert.ToDecimal(dr["AMTN"]);

            }
            else
            {
                _cusNo = dr["CUS_NO", DataRowVersion.Original].ToString();
                _chkMan = dr["CHK_MAN", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["ADD_ID", DataRowVersion.Original].ToString()))
                    _addId = dr["ADD_ID", DataRowVersion.Original].ToString();
                _bilId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _bilNo = dr["BIL_NO", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["SAVE_ID", DataRowVersion.Original].ToString()))
                    _saveId = dr["SAVE_ID", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN", DataRowVersion.Original].ToString()))
                    _amtn = (-1) * Convert.ToDecimal(dr["AMTN", DataRowVersion.Original]);

            }
            if (_amtn == 0)
                return;
            if (string.Compare("T", _saveId) != 0)
            {
                return;
            }
            if (string.Compare("-", _addId) == 0)
            {
                _addFlag = (-1) * _addFlag;
            }
            Dictionary<string, decimal> _updateField = new Dictionary<string, decimal>();
            if (string.Compare("SO", _bilId) == 0)
            {
                if (string.IsNullOrEmpty(_chkMan))
                {
                    _updateField["AMTN_SO_UNSH"] = (-1) * _addFlag * _amtn;
                }
                else
                {
                    _updateField["AMTN_SO"] = (-1) * _addFlag * _amtn;
                }
            }
            else if (string.Compare("SA", _bilId) == 0)
            {
                if (hasAmtnCbacSO)
                {
                    _updateField["AMTN_SO"] = _addFlag * _amtn;
                }
                if (string.IsNullOrEmpty(_chkMan))
                {
                    if (_addFlag > 0)
                    {
                        _updateField["AMTN_UNSH_ADD"] = _amtn;
                    }
                    else
                    {
                        _updateField["AMTN_UNSH_SUB"] = _amtn;
                    }
                }
                else
                {
                    _updateField["AMTN"] = _addFlag * _amtn;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_chkMan))
                {
                    if (_addFlag > 0)
                    {
                        _updateField["AMTN_UNSH_ADD"] = _amtn;
                    }
                    else
                    {
                        _updateField["AMTN_UNSH_SUB"] = _amtn;
                    }
                }
                else
                {
                    _updateField["AMTN"] = _addFlag * _amtn;
                }
            }
            UpdateCustBac(_cusNo, _updateField);
        }
        /// <summary>
        /// 更新客户账户余额
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="updateField"></param>
        public void UpdateCustBac(string cusNo, Dictionary<string, decimal> updateField)
        {
            if (updateField == null)
                return;
            if (updateField.Count == 0)
                return;
            DbCBac _bac = new DbCBac(Comp.Conn_DB);
            _bac.UpdateCustBac(cusNo, updateField);
        }
        #endregion

        #region 回写受订单客户储值结案标记
        /// <summary>
        /// 回写受订单客户储值结案标记
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isNew"></param>
        private void UpdateCbacClsSo(DataRow dr, bool isNew)
        {
            string _bilId = "";
            string _bilNo = "";
            decimal _amtnCbac = 0;
            bool _cbacCls = true;
            if (isNew)
            {
                _bilId = dr["BIL_ID"].ToString();
                _bilNo = dr["BIL_NO"].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
                {
                    _amtnCbac = Convert.ToDecimal(dr["AMTN_CBAC"]);
                }
            }
            else
            {
                _bilId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _bilNo = dr["BIL_NO", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN_CBAC", DataRowVersion.Original].ToString()))
                {
                    _amtnCbac = Convert.ToDecimal(dr["AMTN_CBAC", DataRowVersion.Original]);
                }
            }
            if (_amtnCbac == 0)
                return;
            if (string.Compare("SA", _bilId) == 0)
            {
                DRPSA _sa = new DRPSA();
                SunlikeDataSet _dsSa = _sa.GetData("", "", _bilId, _bilNo);
                if (_dsSa.Tables["MF_PSS"].Rows.Count > 0)
                {
                    string _osId = _dsSa.Tables["MF_PSS"].Rows[0]["OS_ID"].ToString();
                    string _osNo = _dsSa.Tables["MF_PSS"].Rows[0]["OS_NO"].ToString();
                    if (string.Compare("SO", _osId) == 0)
                    {
                        DrpSO _so = new DrpSO();
                        SunlikeDataSet _dsSo = _so.GetData("", _osId, _osNo, false, false);
                        if (_dsSo.Tables["MF_POS"].Rows.Count > 0)
                        {
                            if (string.Compare("T", _dsSo.Tables["MF_POS"].Rows[0]["CBAC_CLS"].ToString()) != 0)
                            {
                                if (isNew)
                                {
                                    _cbacCls = true;
                                }
                                else
                                {
                                    _cbacCls = false;
                                }
                            }
                            _so.UpdateCbacCls(_osId, _osNo, _cbacCls);
                        }
                    }
                }
            }
        }
        #endregion

        #region 生成帐户收支单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr">MF_CBAC行信息</param>
        /// <param name="isNew"></param>
        private void MakeBac(DataRow dr, bool isNew)
        {
            string _bcId = "";
            string _bcNo = "";
            string _baccNo = "";
            string _drNo = "";
            string _crNo = "";
            string _dep = "";
            string _salNo = "";
            string _usr = "";
            string _chkMan = "";
            string _addId = "+";
            string _baccSw = "F";
            string _saveId = "T";
            decimal _excRto = 1;
            decimal _amtn = 0;
            DateTime _bcDd = System.DateTime.Now;
            if (isNew)
            {
                _bcId = dr["BC_ID"].ToString();
                _bcNo = dr["BC_NO"].ToString();
                _bcDd = Convert.ToDateTime(dr["BC_DD"]);
                _baccNo = dr["BACC_NO"].ToString();
                _drNo = dr["DR_NO"].ToString();
                _crNo = dr["CR_NO"].ToString();
                _dep = dr["DEP"].ToString();
                _salNo = dr["SAL_NO"].ToString();
                _saveId = dr["SAVE_ID"].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN"].ToString()))
                    _amtn = Convert.ToDecimal(dr["AMTN"]);
                _usr = dr["USR"].ToString();
                _chkMan = dr["CHK_MAN"].ToString();
                if (!string.IsNullOrEmpty(dr["ADD_ID"].ToString()))
                    _addId = dr["ADD_ID"].ToString();
                if (!string.IsNullOrEmpty(dr["BACC_SW"].ToString()))
                    _baccSw = dr["BACC_SW"].ToString();

            }
            else
            {
                _bcId = dr["BC_ID", DataRowVersion.Original].ToString();
                _bcNo = dr["BC_NO", DataRowVersion.Original].ToString();
                _bcDd = Convert.ToDateTime(dr["BC_DD", DataRowVersion.Original]);
                _baccNo = dr["BACC_NO", DataRowVersion.Original].ToString();
                _drNo = dr["DR_NO", DataRowVersion.Original].ToString();
                _crNo = dr["CR_NO", DataRowVersion.Original].ToString();
                _dep = dr["DEP", DataRowVersion.Original].ToString();
                _salNo = dr["SAL_NO", DataRowVersion.Original].ToString();
                _saveId = dr["SAVE_ID", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN", DataRowVersion.Original].ToString()))
                    _amtn = Convert.ToDecimal(dr["AMTN", DataRowVersion.Original]);
                _usr = dr["USR", DataRowVersion.Original].ToString();
                _chkMan = dr["CHK_MAN", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["ADD_ID", DataRowVersion.Original].ToString()))
                    _addId = dr["ADD_ID", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["BACC_SW", DataRowVersion.Original].ToString()))
                    _baccSw = dr["BACC_SW", DataRowVersion.Original].ToString();

            }

            //**************************判断是否生成帐户收支**************************************
            if (string.Compare("T", _saveId) != 0)
                return;
            if (string.Compare("T", _baccSw) != 0)
                return;

            if (isNew)
            {
                if (string.IsNullOrEmpty(_chkMan))
                    return;
            }
            else
            {
                if (string.IsNullOrEmpty(_chkMan))
                    return;
            }
            //**********************************完成**********************************************
            Bacc _bacc = new Bacc();
            SunlikeDataSet _dsBac = _bacc.GetBAC(_bcNo);
            if (isNew)
            {
                DataRow _drNew = null;
                //表头编辑
                if (_dsBac.Tables["MF_BAC"].Rows.Count > 0)
                {
                    _drNew = _dsBac.Tables["MF_BAC"].Rows[0];
                }
                else
                {
                    _drNew = _dsBac.Tables["MF_BAC"].NewRow();
                    _drNew["BB_ID"] = "BT";
                    _drNew["BB_NO"] = "";
                }
                _drNew["BB_DD"] = _bcDd;
                _drNew["BACC_NO"] = _baccNo;
                _drNew["ACC_NO"] = _drNo;
                _drNew["DEP"] = _dep;
                _drNew["BIL_NO"] = _bcId + _bcNo;
                _drNew["PAY_MAN"] = _salNo;
                _drNew["EXC_RTO"] = _excRto;
                _drNew["AMTN"] = _amtn;
                _drNew["USR"] = _usr;
                _drNew["CHK_MAN"] = _chkMan;
                _drNew["PRT_SW"] = "N";
                _drNew["OPN_ID"] = "F";
                _drNew["CLS_DATE"] = _bcDd;
                _drNew["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                _dsBac.Tables["MF_BAC"].Rows.Add(_drNew);
                //表身编辑
                if (_dsBac.Tables["TF_BAC"].Rows.Count > 0)
                {
                    _drNew = _dsBac.Tables["TF_BAC"].Rows[0];
                }
                else
                {
                    _drNew = _dsBac.Tables["TF_BAC"].NewRow();
                    _drNew["BB_ID"] = "BT";
                    _drNew["BB_NO"] = "";
                }
                _drNew["ITM"] = "1";
                _drNew["BB_DD"] = _bcDd;
                _drNew["ADD_ID"] = _addId;
                _drNew["EXC_RTO"] = _excRto;
                _drNew["AMTN"] = _amtn;
                _drNew["DEP"] = _dep;
                _drNew["ACC_NO"] = _crNo;
                _drNew["RCV_MAN"] = _salNo;
                _dsBac.Tables["TF_BAC"].Rows.Add(_drNew);



            }
            else
            {
                if (_dsBac.Tables["MF_BAC"].Rows.Count > 0)
                {
                    _dsBac.Tables["MF_BAC"].Rows[0].Delete();
                }
            }
            if (!_dsBac.ExtendedProperties.ContainsKey("NO_MAK_VOH_NO"))
            {
                _dsBac.ExtendedProperties.Add("NO_MAK_VOH_NO", "");
            }
            _dsBac.ExtendedProperties["NO_MAK_VOH_NO"] = "True";
            _bacc.UpdateData(_dsBac, true);
        }

        #endregion


        #region 更新凭证号码
        /// <summary>
        /// 更新凭证号码
        /// </summary>
        /// <param name="dr">MF_CBAC的数据行</param>
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
                if (this._reBuildVohNo)
                {
                    //if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                        //_updUsr = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                    string _depNo = dr["DEP"].ToString();
                    bool _getVoh = false;
                    CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                    _getVoh = _compInfo.VoucherInfo.GenVoh.BACC;
                    if (_getVoh)
                    {
                        DataSet _dsBills = dr.Table.DataSet.Copy();
                        _dsBills.Merge(this.GetData("", dr["BC_ID"].ToString(), dr["BC_NO"].ToString(), false), true);
                        _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                        dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, dr["BC_ID"].ToString(), out _vohNoError);
                        _vohNo = dr["VOH_NO"].ToString();
                    }
                }
                else
                {
                    if ((!string.IsNullOrEmpty(dr["VOH_NO"].ToString()) && string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                        || string.IsNullOrEmpty(dr["VOH_NO"].ToString())
                        )
                    {
                        string _depNo = dr["DEP"].ToString();
                        bool _getVoh = false;
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        _getVoh = _compInfo.VoucherInfo.GenVoh.BACC;
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", dr["BC_ID"].ToString(), dr["BC_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, dr["BC_ID"].ToString(), out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
            }
            else if (statementType == StatementType.Insert)
            {
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                _getVoh = _compInfo.VoucherInfo.GenVoh.BACC;
                if (_getVoh)
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, dr["BC_ID"].ToString(), out _vohNoError);
                    _vohNo = dr["VOH_NO"].ToString();
                }
            }
            else if (statementType == StatementType.Delete)
            {
                if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                {
                    _vohNo = dr["VOH_NO", DataRowVersion.Original].ToString();
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
        #endregion

        #region 更新审核人
        /// <summary>
        /// 更新审核人
        /// </summary>
        /// <param name="bcId"></param>
        /// <param name="bcNo"></param>
        /// <param name="chkMan"></param>
        /// <param name="clsDate"></param>
        private void UpdateChkMan(string bcId, string bcNo, string chkMan, DateTime clsDate)
        {
            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            _cbac.UpdateChkMan(bcId, bcNo, chkMan, clsDate);
        }
        #endregion

        #region 更新客户储值单凭证号码
        /// <summary>
        /// 更新客户储值单凭证号码
        /// </summary>
        /// <param name="bcId"></param>
        /// <param name="bcNo"></param>
        /// <param name="vohNo"></param>
        private void UpdateVohNo(string bcId, string bcNo, string vohNo)
        {
            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            _cbac.UpdateVohNo(bcId, bcNo, vohNo);
        }
        #endregion


        #region 取客户余额
        /// <summary>
        /// 取客户余额
        /// </summary>
        /// <param name="isFact"></param>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public decimal GetCustBac(bool isFact, string cusNo)
        {
            decimal _result = 0;
            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            SunlikeDataSet _dsCustBac = _cbac.GetDataCustBac(cusNo);
            if (_dsCustBac.Tables["CUST_BAC"].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(_dsCustBac.Tables["CUST_BAC"].Rows[0]["AMTN"].ToString()))
                {
                    _result = Convert.ToDecimal(_dsCustBac.Tables["CUST_BAC"].Rows[0]["AMTN"]);
                }
                if (!isFact)
                {
                    if (!string.IsNullOrEmpty(_dsCustBac.Tables["CUST_BAC"].Rows[0]["AMTN_SO_UNSH"].ToString()))
                    {
                        _result += (-1) * Convert.ToDecimal(_dsCustBac.Tables["CUST_BAC"].Rows[0]["AMTN_SO_UNSH"]);
                    }
                    if (!string.IsNullOrEmpty(_dsCustBac.Tables["CUST_BAC"].Rows[0]["AMTN_SO"].ToString()))
                    {
                        _result += (-1) * Convert.ToDecimal(_dsCustBac.Tables["CUST_BAC"].Rows[0]["AMTN_SO"]);
                    }
                    if (!string.IsNullOrEmpty(_dsCustBac.Tables["CUST_BAC"].Rows[0]["AMTN_UNSH_SUB"].ToString()))
                    {
                        _result += (-1) * Convert.ToDecimal(_dsCustBac.Tables["CUST_BAC"].Rows[0]["AMTN_UNSH_SUB"]);
                    }
                }
            }
            return _result;
        }
        #endregion

        #region 多开发票处理作业
        /// <summary>
        /// 多开发票处理作业
        /// </summary>
        /// <param name="_ds"></param>
        /// <param name="usr"></param>
        /// <param name="dep"></param>
        public DataTable UpdateInvB2c(SunlikeDataSet _ds, string usr, string dep)
        {
            string _prdtInv = Comp.DRP_Prop["DFTINV_PRDT"].ToString();//发票货品
            string ps_id = "SA";//单据别
            string ps_no = "";//单号
            decimal _amtn = 0;//多开发票需返还金额
            decimal _tax = 0;//发票货品税率
            DataTable _dtErr = null;
            //取发票货品税率
            Prdt _prdt = new Prdt();
            DataTable _dtInv = _prdt.GetFullPrdt(_prdtInv);
            if (_dtInv.Rows.Count > 0 && _dtInv.Rows[0]["SPC_TAX"].ToString() != "")
                _tax = Convert.ToDecimal(_dtInv.Rows[0]["SPC_TAX"]);
            DecimalDigitsInfo.SysDecimalDigits _digits = Comp.GetCompInfo(dep).DecimalDigitsInfo.System;
            foreach (DataRow dr in _ds.Tables[0].Rows)
            {
                _amtn = 0;
                base.EnterTransaction();
                if (ps_id == dr["PS_ID"].ToString() && ps_no == dr["PS_NO"].ToString())
                {

                }
                else
                {
                    //当销货单表身有多笔发票货品，则统计需返还的金额，生成客户账户变动单
                    ps_id = dr["PS_ID"].ToString();
                    ps_no = dr["PS_NO"].ToString();
                    DataRow[] _drArySA = _ds.Tables[0].Select("PS_ID='" + ps_id + "' AND PS_NO='" + ps_no + "' AND PRD_NO='" + _prdtInv + "'");
                    foreach (DataRow dr1 in _drArySA)
                    {
                        if (dr1["AMT"].ToString() != "")
                        {
                            /*----------------------Modified by yzb---------------------------
                               由于客户在使用中经常会出现操作失误，导致销货单的扣税类别不是应税内含，
                               所以这里不做控制，只要是返佣，全部要减掉税
                             -----------------------------------------------------------------*/

                            //if (dr1["TAX_ID"].ToString() == "2")
                            //{
                            //应税内含
                            _amtn += Convert.ToDecimal(dr1["AMT"]) * (1 - _tax / 100);
                            //}
                            //else
                            //{
                            //    //应税外加和不交税
                            //    _amtn += Convert.ToDecimal(dr1["AMT"]);
                            //}
                        }
                    }
                    CBac _cbac = new CBac();
                    SunlikeDataSet _dsCbac = _cbac.GetData("BC", "", true);
                    _dsCbac.DecimalDigits = _digits;
                    DataRow _drCbac = _dsCbac.Tables["MF_CBAC"].NewRow();
                    _drCbac["BC_ID"] = "BC";
                    _drCbac["BC_NO"] = "";
                    _drCbac["BC_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                    _drCbac["CUS_NO"] = dr["CUS_NO"];
                    _drCbac["DEP"] = dep;
                    _drCbac["USR"] = usr;
                    _drCbac["ADD_ID"] = "+";
                    _drCbac["BACC_SW"] = "F";
                    _drCbac["CR_NO"] = Comp.DRP_Prop["CHZH_CR_NO"];
                    _drCbac["AMTN"] = _amtn;
                    _drCbac["SAVE_ID"] = "T";
                    _drCbac["PAY_TYPE"] = "5";
                    _drCbac["REM"] = ResourceCenter.ResourceManager["EC.HINT.OS_RTPROFIT,PARAM=" + dr["OS_NO"].ToString()];
                    _dsCbac.Tables["MF_CBAC"].Rows.Add(_drCbac);
                    _dtErr = _cbac.UpdateData(_dsCbac, false);
                    if (_dtErr.Rows.Count > 0)
                    {
                        base.SetAbort();
                        break;
                    }
                }
                //回写销货单表身的发票货品[INV_B2C]多开发票处理否为‘T’
                DRPSA _drpsa = new DRPSA();
                SunlikeDataSet _dsSA = _drpsa.GetData("", usr, dr["PS_ID"].ToString(), dr["PS_NO"].ToString());
                if (_dsSA.Tables["TF_PSS"].Rows.Count > 0)
                {
                    foreach (DataRow _drSA in _dsSA.Tables["TF_PSS"].Rows)
                    {
                        if (_drSA["PRD_NO"].ToString() == _prdtInv)
                        {
                            _drSA["INV_B2C"] = "T";
                        }
                    }
                    _dtErr = _drpsa.UpdateData("DRPSA",_dsSA);
                    if (_dtErr.Rows.Count > 0)
                    {
                        base.SetAbort();
                        break;
                    }
                    else
                        base.SetComplete();
                }
            }
            base.LeaveTransaction();
            return _dtErr;
        }
        #endregion

        #region IAuditing Members
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
            string _errmsg = "";
            try
            {
                SunlikeDataSet _dsCbac = this.GetData(bil_id, bil_no, false);
                DataTable _dtHead = _dsCbac.Tables["MF_CBAC"];
                if (_dtHead.Rows.Count > 0)
                {

                    #region 判断客户帐户余额
                    string _cusNo = _dtHead.Rows[0]["CUS_NO"].ToString();
                    decimal _amtnCustBac = GetCustBac(false, _cusNo);
                    decimal _flag = 1;

                    if (!string.IsNullOrEmpty(_dtHead.Rows[0]["AMTN"].ToString()))
                    {
                        if (string.Compare("-", _dtHead.Rows[0]["ADD_ID"].ToString()) == 0)
                        {
                            _flag = -1;
                        }
                        _amtnCustBac += _flag * Convert.ToDecimal(_dtHead.Rows[0]["AMTN"]);
                    }
                    if (_amtnCustBac < 0)
                    {
                        throw new SunlikeException(/*客户帐户余额不足*/"RCID=MON.HINT.CUST_BAC_LITTLE");
                    }
                    #endregion
                    string _psId = "";
                    string _psNo = "";
                    string _osId = "";
                    string _osNo = "";
                    bool _hasAmtnCbac = false;
                    _psId = _dtHead.Rows[0]["BIL_ID"].ToString();
                    _psNo = _dtHead.Rows[0]["BIL_NO"].ToString();
                    DRPSA _sa = new DRPSA();
                    SunlikeDataSet _dsSa = _sa.GetData("", "", _psId, _psNo);
                    if (_dsSa.Tables["MF_PSS"].Rows.Count > 0)
                    {
                        DrpSO _so = new DrpSO();
                        SunlikeDataSet _dsSo = _so.GetData("", _osId, _osNo, false, false);
                        if (_dsSo.Tables["MF_POS"].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_dsSo.Tables["MF_POS"].Rows[0]["CBAC_CLS"].ToString())
                                && Convert.ToDecimal(_dsSo.Tables["MF_POS"].Rows[0]["CBAC_CLS"]) != 0)
                            {
                                _hasAmtnCbac = true;
                            }
                        }
                    }
                    _dtHead.Rows[0]["CHK_MAN"] = chk_man;
                    UpdateCustBac(_dtHead.Rows[0], false, _hasAmtnCbac);
                    UpdateCustBac(_dtHead.Rows[0], true, _hasAmtnCbac);
                    MakeBac(_dtHead.Rows[0], true);
                    string _vohNo = UpdateVohNo(_dtHead.Rows[0], StatementType.Insert);
                    UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
                    UpdateVohNo(bil_id, bil_no, _vohNo);

                }

            }
            catch (Exception _ex)
            {
                _errmsg = _ex.Message.ToString();
            }
            return _errmsg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _errmsg = "";
            return _errmsg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            return RollBack(bil_id, bil_no, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no, bool canChangeDS)
        {
            string _errmsg = "";
            try
            {
                SunlikeDataSet _dsCbac = this.GetData(bil_id, bil_no, false);
                DataTable _dtHead = _dsCbac.Tables["MF_CBAC"];
                if (_dtHead.Rows.Count > 0)
                {
                    string _psId = "";
                    string _psNo = "";
                    string _osId = "";
                    string _osNo = "";
                    bool _hasAmtnCbac = false;
                    _psId = _dtHead.Rows[0]["BIL_ID"].ToString();
                    _psNo = _dtHead.Rows[0]["BIL_NO"].ToString();
                    DRPSA _sa = new DRPSA();
                    SunlikeDataSet _dsSa = _sa.GetData("", "", _psId, _psNo);
                    if (_dsSa.Tables["MF_PSS"].Rows.Count > 0)
                    {
                        DrpSO _so = new DrpSO();
                        SunlikeDataSet _dsSo = _so.GetData("", _osId, _osNo, false, false);
                        if (_dsSo.Tables["MF_POS"].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_dsSo.Tables["MF_POS"].Rows[0]["CBAC_CLS"].ToString())
                                && Convert.ToDecimal(_dsSo.Tables["MF_POS"].Rows[0]["CBAC_CLS"]) != 0)
                            {
                                _hasAmtnCbac = true;
                            }
                        }
                    }

                    UpdateCustBac(_dtHead.Rows[0], false, _hasAmtnCbac);
                    _dtHead.Rows[0]["CHK_MAN"] = System.DBNull.Value;
                    UpdateCustBac(_dtHead.Rows[0], true, _hasAmtnCbac);
                    _dtHead.RejectChanges();
                    _dtHead.Rows[0].Delete();
                    MakeBac(_dtHead.Rows[0], false);
                    if (canChangeDS)
                    {
                        UpdateChkMan(bil_id, bil_no, "", System.DateTime.Now);
                        UpdateVohNo(bil_id, bil_no, "");
                    }
                }

            }
            catch (Exception _ex)
            {
                _errmsg = _ex.Message.ToString();
            }
            return _errmsg;
        }

        #endregion

        #region Cust_sbac
        /// <summary>
        /// 取客户服务余额
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataCustSBac(string cusNo, string cardNo, string subNo, string prdNo)
        {
            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            return _cbac.GetDataCustSBac(cusNo, cardNo, subNo, prdNo);
        }

        /// <summary>
        /// 更新客户服务余额
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="numUse"></param>
        public void UpdateCustSBac(string cusNo, string cardNo, string subNo, string prdNo, decimal numUse, DateTime? lastDd)
        {
            string updatePrdNo = prdNo;
            SunlikeDataSet _ds = GetDataCustSBac(cusNo, cardNo, subNo, "");
            DataRow[] _drs = _ds.Tables[0].Select("PRD_NO='" + prdNo + "'");
            if (_drs.Length > 0)
            {
                if (!string.IsNullOrEmpty(_drs[0]["GROUP_PRDT"].ToString()))
                {
                    updatePrdNo = _drs[0]["GROUP_PRDT"].ToString();
                    _drs = _ds.Tables[0].Select("PRD_NO='" + _drs[0]["GROUP_PRDT"].ToString() + "'");
                }
            }

            if (numUse > 0)
            {
                if (_drs.Length > 0)
                {
                    decimal _numSetOld = 0;
                    decimal _numUseOld = 0;
                    DateTime _lastDdOld = DateTime.MinValue;
                    int _dayZqOld = 0;
                    if (!string.IsNullOrEmpty(_drs[0]["NUM_SET"].ToString()))
                    {
                        _numSetOld = Convert.ToDecimal(_drs[0]["NUM_SET"]);
                    }
                    if (!string.IsNullOrEmpty(_drs[0]["NUM_USE"].ToString()))
                    {
                        _numUseOld = Convert.ToDecimal(_drs[0]["NUM_USE"]);
                    }
                    if (!string.IsNullOrEmpty(_drs[0]["LAST_DD"].ToString()))
                    {
                        _lastDdOld = Convert.ToDateTime(_drs[0]["LAST_DD"]);
                    }
                    if (!string.IsNullOrEmpty(_drs[0]["DAY_ZQ"].ToString()))
                    {
                        _dayZqOld = Convert.ToInt32(_drs[0]["DAY_ZQ"]);
                    }

                    if (DateTime.Now.Subtract(_lastDdOld).TotalDays < _dayZqOld)
                    {
                        throw new Exception("RCID=INV.CUST_SBAC.DAYZQ_NOT_ENOUGH,PARAM=" + prdNo);
                    }
                    if (_numSetOld > 0 && _numSetOld - _numUseOld < numUse)
                    {
                        throw new Exception("RCID=INV.CUST_SBAC.NUM_SET_NOT_ENOUGH,PARAM=" + prdNo);
                    }
                }
                else
                {
                    throw new Exception("RCID=INV.CUST_SBAC.NUM_SET_NOT_ENOUGH,PARAM=" + prdNo);
                }
            }

            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            _cbac.UpdateCustSBac(cusNo, cardNo, subNo, updatePrdNo, numUse, lastDd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="numSet"></param>
        /// <param name="dayZq"></param>
        /// <param name="upFreeze"></param>
        /// <param name="countType"></param>
        /// <param name="prdName"></param>
        /// <param name="groupPrdt"></param>
        public void InsertCustSbac(string cusNo, string cardNo, string subNo, string prdNo, decimal numSet, int dayZq, decimal upFreeze, decimal upZq, string prdName, string groupPrdt)
        {
            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            _cbac.InsertCustSbac(cusNo, cardNo, subNo, prdNo, numSet, dayZq, upFreeze, upZq, prdName, groupPrdt);
        }
        /// <summary>
        /// 删除客户服务次数
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        public void DeleteCustSBac(string cusNo, string cardNo, string subNo)
        {
            DbCBac _cbac = new DbCBac(Comp.Conn_DB);
            _cbac.DeleteCustSBac(cusNo, cardNo, subNo);
        }
        #endregion
    }
}
