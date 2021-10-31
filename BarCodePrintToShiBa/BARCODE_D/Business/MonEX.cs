using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections;

namespace Sunlike.Business
{
    /// <summary>
    /// 费用主库.
    /// </summary>e
    public class MonEX : Sunlike.Business.BizObject, Sunlike.Business.IAuditing, Sunlike.Business.IAuditingInfo
    {
        #region 费用类型
        /// <summary>
        /// 费用类型
        /// </summary>
        public enum ExpType
        {
            /// <summary>
            /// 客户费用
            /// </summary>
            CUSTER,
            /// <summary>
            /// 其他费用收入
            /// </summary>
            MONEXR,
            /// <summary>
            /// 其他费用支出 
            /// </summary>
            MONEXP
        }
        #endregion

        #region variable
        private bool _isRunAuditing;//是否走审核流程
        private bool _reBuildVohNo = false;//是否重建凭证号码
        private bool _makeVohNo = false;//是否强制切凭证
        #endregion

        #region 构造函数
        /// <summary>
        /// MonEX
        /// </summary>
        public MonEX()
        {
        }
        #endregion

        #region GetData
        /// <summary>
        ///	 取得费用单（有取PGM权限）InsertSpcPswd
        /// </summary>
        /// <param name="expType">费用类型</param>
        /// <param name="usr">录入人</param>
        /// <param name="epId">单据别</param>
        /// <param name="epNo">单据号</param>
        /// <param name="isSchema">是否取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm, ExpType expType, string usr, string epId, string epNo, bool isSchema)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _expDataSet = _dbMonEX.GetData(epId, epNo, isSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _expDataSet.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            if (!String.IsNullOrEmpty(usr))
            {
                DataTable _mf_expTable = _expDataSet.Tables["MF_EXP"];
                if (string.IsNullOrEmpty(pgm))
                {
                    switch (expType)
                    {
                        case ExpType.CUSTER://客户费用
                            pgm = "CUSTER";
                            break;
                        case ExpType.MONEXR://	其他费用收入	
                            pgm = "MONEXR";
                            break;
                        case ExpType.MONEXP://其他费用支出	
                            pgm = "MONEXP";
                            break;
                        default:
                            pgm = "MONEXR";
                            break;
                    }
                }

                if (_mf_expTable.Rows.Count > 0)
                {
                    string _bill_Dep = _mf_expTable.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _mf_expTable.Rows[0]["USR"].ToString();
                    System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                    _expDataSet.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _expDataSet.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _expDataSet.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _expDataSet.ExtendedProperties["LCK"] = _billRight["LCK"];
                }
            }
            this.SetCanModify(_expDataSet, true);
            if (_expDataSet.Tables.Contains("TF_EXP"))
            {
                //if (!string.IsNullOrEmpty(epNo))
                //{
                    _expDataSet.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrement = true;
                    _expDataSet.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementSeed = _expDataSet.Tables["TF_EXP"].Rows.Count > 0 ? Convert.ToInt32(_expDataSet.Tables["TF_EXP"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
                    _expDataSet.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementStep = 1;
                    _expDataSet.Tables["TF_EXP"].Columns["KEY_ITM"].Unique = true;
               // }

            }
            return _expDataSet;
        }
        /// <summary>
        /// 取得费用单（无权限）
        /// </summary>
        /// <param name="epId">单据类别</param>
        /// <param name="epNo">单据号</param>
        /// <param name="isSchema">是否取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string epId, string epNo, bool isSchema)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMonEX.GetData(epId, epNo, isSchema);
            this.SetCanModify(_ds, true);
            //if (_ds.Tables.Contains("TF_EXP"))
            //{
            //    _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrement = true;
            //    _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_EXP"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_EXP"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            //    _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            //    _ds.Tables["TF_EXP"].Columns["KEY_ITM"].Unique = true;
            //}
            return _ds;
        }
        #endregion

        #region 新增时基本数据
        /// <summary>
        /// 新增时基本数据
        /// </summary>
        /// <param name="MonEX">DataSet</param>
        /// <param name="epId">单据别</param>
        /// <param name="usr">登陆人</param>
        public void AddNewData(SunlikeDataSet MonEX, string epId, string usr)
        {
            DataTable _mf_expTable = MonEX.Tables["MF_EXP"];
            DataRow _dr = _mf_expTable.NewRow();
            try
            {
                _dr["EP_ID"] = epId;
                SQNO _sqlNo = new SQNO();
                _dr["EP_NO"] = _sqlNo.Get(epId, usr, "", System.DateTime.Now, "FX");
                _dr["EP_DD"] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                _dr["USR"] = usr;
                _dr["OPN_ID"] = "2";
                _dr["PRT_SW"] = "N";
                _dr["BIL_TYPE"] = "FX";
                _dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                _mf_expTable.Rows.Add(_dr);
                //MonEX.AcceptChanges();
            }
            catch (Exception _ex)
            {
                throw new Sunlike.Common.Utility.SunlikeException(_ex.Message);
            }

        }
        #endregion

        #region 检查DataSet是否可以修改
        /// <summary>
        /// 检查DataSet是否可以修改
        /// 返回是否能修改的错误代码(0:能修改 1:已进入审核流程 2:已产生应收账款3:已有来源单号4:锁单)
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        /// <returns>返回是否能修改的错误代码(0:能修改 1:已进入审核流程 2:已产生应收账款3:已有来源单号4:锁单)</returns>
        private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)/*MODIFY LWH 090907 添加不能修改提示*/
        {
            bool _bCanModify = true;
            string _result = "0";
            DataTable _dtMf = ds.Tables["MF_EXP"];
            DataTable _dtTf = ds.Tables["TF_EXP"];



            if (_dtMf.Rows.Count > 0)
            {
                #region 判断关帐日

                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["EP_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_MON"))
                {
                    _bCanModify = false;
                    _result = "RCID=COMMON.HINT.HASCLOSEBILL";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");/*已关账不能修改*/
                }
                #endregion

                #region 判断是否需要审核
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_dtMf.Rows[0]["EP_ID"].ToString(), _dtMf.Rows[0]["EP_NO"].ToString()))
                    {
                        _bCanModify = false;
                        _result = "1";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");/*已进入审核流程不能修改*/

                    }
                }
                #endregion

                #region 判断是否锁单
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    _result = "4";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");/*已锁单不能修改*/
                }
                #endregion

                #region 判断是否已开发票
                if (true)
                {
                    Arp _arp = new Arp();
                    foreach (DataRow dr in _dtTf.Rows)
                    {
                        Decimal _amtnRcv = 0;
                        if (!String.IsNullOrEmpty(dr["ARP_NO"].ToString()))
                        {
                            if (!string.IsNullOrEmpty(dr["AMT_RP"].ToString()))
                            {
                                _amtnRcv = Convert.ToDecimal(dr["AMT_RP"]);
                            }
                            if (Convert.ToDecimal(dr["AMT"]) != 0)
                            {
                                if (_arp.HasReceiveDollar(dr["ARP_NO"].ToString(), _amtnRcv))
                                {
                                    _bCanModify = false;
                                    // _result = "2";
                                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LZ_MODIFY");/* 已开立发票立账不能修改*/
                                    break;
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 判断是否有来源单
                //if (_bCanModify)
                //{

                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["PC_NO"].ToString()))
                {
                    _bCanModify = false;
                    _result = "3";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CANNOTMODIFY");/*存在来源单号，不能修改！*/
                }
                // }
                #endregion

                #region 判断是否冲账
                //foreach (DataRow _dr in _dtTf.Rows)
                //{
                //    if (_dr.RowState != DataRowState.Deleted && _dr.RowState != DataRowState.Detached)
                //    {
                //        string _arpNO = _dr["ARP_NO"].ToString();
                //        if (!string.IsNullOrEmpty(_arpNO))
                //        {
                //            Arp _arp = new Arp();
                //            try
                //            {
                //                if (_arp.HasReceiveDollar(_arpNO))
                //                {
                //                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_WRITEUP");
                //                    _bCanModify = false;
                //                    break;
                //                }
                //            }
                //            catch (Exception ex) { throw new Exception(ex.ToString()); }
                //        }
                //    }
                //}

                #endregion

                #region 判断凭证
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["VOH_NO"].ToString()))
                {
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
                #endregion
            }


            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return _result;
        }
        #endregion

        #region 保存前后操作



        /// <summary>
        /// 扩展属性:
        /// MAKE_VOH_NO  是否强制产生凭证号: T:强制生成凭证号码
        /// </summary>
        /// <param name="changedDs">DataSet</param>
        /// <param name="pgm" >PGM</param>
        /// <param name="bubbleException">是否抛出异常（true为直接抛出异常，false返回ErrorTable）</param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, SunlikeDataSet changedDs, bool bubbleException)
        {
            string _epId, _usr;
            DataRow _dr = changedDs.Tables["MF_EXP"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _usr = _dr["USR", DataRowVersion.Original].ToString();
                _epId = _dr["EP_ID", DataRowVersion.Original].ToString();
            }
            else
            {
                _usr = _dr["USR"].ToString();
                _epId = _dr["EP_ID"].ToString();
            }

            //是否重建凭证号码
            if (changedDs.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (String.Compare("True", changedDs.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            //是否强制产生凭证号
            if (changedDs.ExtendedProperties.ContainsKey("MAKE_VOH_NO"))
            {
                if (String.Compare("T", changedDs.ExtendedProperties["MAKE_VOH_NO"].ToString()) == 0)
                {
                    this._makeVohNo = true;
                }
            }

            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht.Add("MF_EXP", "EP_DD, EP_NO, REM, USR, PRT_SW, EP_ID, USR_NO, OPN_ID, SYS_DATE,BIL_TYPE,DEP,CHK_MAN,PC_NO,CLS_DATE,BIL_ID,VOH_ID,VOH_NO");
            _ht.Add("TF_EXP", "ITM, IDX_NO, CUS_NO, CUR_ID, EXC_RTO, TAX_ID, AMT, AMTN_NET, TAX, ARP_NO, ACC_NO, DEP, INV_NO, BAT_NO, REM, PAY_REM, AMT_RP, RP_NO, SAL_NO, PAY_DD, EP_NO, EP_ID, SHARE_MTH, CHK_DD, CHK_DAYS, CLOSE_FT, AMTN_FT_TOT, PAY_MTH, PAY_DAYS, CLS_REM, INT_DAYS, METH_ID, COMPOSE_IDNO, INV_DD, INV_YM, TITLE_BUY, AMT_INV, TAX_INV,RTO_TAX,BIL_ID,BIL_NO,KEY_ITM,AMTN_NET_FP,AMT_FP,TAX_FP,LZ_CLS_ID,CLSLZ");

            //判断是否走审核流程
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
            //_isRunAuditing = _auditing.IsRunAuditing(_epId, _usr, _bilType, _mobID);


            this.UpdateDataSet(changedDs, _ht);

            //判断单据能否修改
            if (!changedDs.HasErrors)
            {
                //增加单据权限
                string _UpdUsr = "";
                if (changedDs.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changedDs.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    if (pgm == null || pgm == string.Empty)
                    {
                        pgm = "DRP" + _epId;
                        switch (_epId)
                        {
                            case "ER"://其他费用收入	
                                pgm = "MONEXR";
                                break;
                            case "EP"://其他费用支出	
                                pgm = "MONEXP";
                                break;
                            default://客户费用
                                pgm = "CUSTER";
                                break;
                        }
                    }
                    DataTable _dtMf = changedDs.Tables["MF_EXP"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changedDs.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDs.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDs.ExtendedProperties["PRN"] = _billRight["PRN"];
                        changedDs.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
                this.SetCanModify(changedDs, true);
            }
            else
            {
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDs);
                    throw new SunlikeException("RCID=MonEX.UpdateData() Error:;" + _errorMsg);
                }
            }
            return GetAllErrors(changedDs);
        }
        /// <summary>
        /// BeforeUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            string _ep_No = ""; //单据号码
            string _ep_Id = "";//单据ID
            string _usr = "";//登陆用户
            if (dr.RowState != DataRowState.Deleted)
            {
                _ep_No = dr["EP_NO"].ToString();
                _ep_Id = dr["EP_ID"].ToString();
                _usr = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR"].ToString();
            }
            else
            {
                _ep_No = dr["EP_NO", DataRowVersion.Original].ToString();
                _ep_Id = dr["EP_ID", DataRowVersion.Original].ToString();
                _usr = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR", DataRowVersion.Original].ToString();
            }
            if (statementType != StatementType.Insert)
            {
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "EP_ID = '" + _ep_Id + "' AND EP_NO = '" + _ep_No + "'";
                if (_Users.IsLocked("MF_EXP", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }

                Auditing _auditing = new Auditing();
                if (_auditing.GetIfEnterAuditing(_ep_Id, _ep_No))//如果进去审核了就不能修改和新增删除
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");
                }
            }
            #region 新增关帐判断
            if (tableName == "MF_EXP")
            {
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["EP_DD"]), dr["DEP"].ToString(), "CLS_MON"))
                    {
                        CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL1,PARAM=" + _compInfo.CloseAccountDateInfo.CLS_MON.ToString(_compInfo.SystemInfo.DATEPATTERN));
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["EP_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MON"))
                    {
                        CompInfo _compInfo = Comp.GetCompInfo(dr["DEP", DataRowVersion.Original].ToString());
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL1,PARAM=" + _compInfo.CloseAccountDateInfo.CLS_MON.ToString(_compInfo.SystemInfo.DATEPATTERN));
                    }
                }
            }
            #endregion


            #region 数据检测
            if (tableName == "MF_EXP" && statementType != StatementType.Delete)
            {
                //检查经办人
                Salm _salm = new Salm();
                if (!_salm.IsExist(_usr, dr["USR_NO"].ToString()))
                {
                    dr.SetColumnError("USR_NO",/*经办人[{0}]不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.MAN_NO_NOTEXIST,PARAM=" + dr["USR_NO"].ToString());
                    status = UpdateStatus.SkipAllRemainingRows;
                }

                // 部门(必填)
                Dept _dept = new Dept();
                if (!_dept.IsExist(_usr, dr["DEP"].ToString()))
                {
                    dr.SetColumnError("DEP",/*部门代号[{0}]不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                    status = UpdateStatus.SkipAllRemainingRows;
                }
            }

            if (tableName == "TF_EXP" && statementType != StatementType.Delete)
            {
                if (!String.IsNullOrEmpty(dr["AMT_RP"].ToString()))
                {
                    if (dr["AMT"] == null || dr["AMT"] == DBNull.Value)
                    {
                        dr["AMT"] = 0;
                    }
                    if (dr["AMT_RP"] == null || dr["AMT_RP"] == DBNull.Value)
                    {
                        dr["AMT_RP"] = 0;
                    }
                    if (Convert.ToDecimal(dr["AMT"]) < Convert.ToDecimal(dr["AMT_RP"]))
                    {
                        dr.SetColumnError("AMT_RP", /*即收付金额不可大于立账金额！*/"RCID=MON.HINT.AMTRP,PARAM=" + dr["AMT_RP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }

                //检查费用项目(必填)
                Indx1 _indx1 = new Indx1();
                if (!_indx1.IsExists(dr["IDX_NO"].ToString()))
                {
                    dr.SetColumnError("IDX_NO",/*费用科目[{0}]不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.NOIDX1,PARAM=" + dr["IDX_NO"].ToString());
                    status = UpdateStatus.SkipAllRemainingRows;
                }

                //检查客户资料 (必填)
                Cust _cust = new Cust();
                if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString()))
                {
                    dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//客户代号不存在或没有对其操作的权限,请检查
                    status = UpdateStatus.SkipAllRemainingRows;
                }

                //检查会计科目
                BTMng _bTMng = new BTMng();
                if (!String.IsNullOrEmpty(dr["ACC_NO"].ToString()))
                {
                    if (!_bTMng.IsExistsAccNo(dr["ACC_NO"].ToString()))
                    {
                        dr.SetColumnError("ACC_NO",/*会计科目[{0}]不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.ACCNOERROR,PARAM=" + dr["ACC_NO"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else
                    {
                        Accn _accn = new Accn();
                        if (_accn.CheckCls(dr["ACC_NO"].ToString()))
                        {
                            dr.SetColumnError("ACC_NO",/*含有统治科目*/"RCID=MON.HINT.NOCLS");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
                //部门
                Dept _dept = new Dept();
                if (!String.IsNullOrEmpty(dr["DEP"].ToString()))
                {
                    if (!_dept.IsExist(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR"].ToString(), dr["DEP"].ToString()))
                    {
                        dr.SetColumnError("DEP",/*部门代号[{0}]不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }

                //分摊方式
                if (String.IsNullOrEmpty(dr["SHARE_MTH"].ToString()))
                {
                    dr["SHARE_MTH"] = "1";
                }
            }

            #endregion

            string _saveId = "T";
            string _billAuditing = "";
            //判断来源单是否需要缓存 SAVE_ID=F为缓存(不需要写审核流程并且不添审核人)　否则　为不走审核流程
            if (SunlikeDataSet.ConvertTo(dr.Table.DataSet).ExtendedProperties.Contains("SAVE_ID"))
            {
                _saveId = SunlikeDataSet.ConvertTo(dr.Table.DataSet).ExtendedProperties["SAVE_ID"].ToString();
            }
            if (SunlikeDataSet.ConvertTo(dr.Table.DataSet).ExtendedProperties.Contains("BILL_AUDITING"))
            {
                _billAuditing = SunlikeDataSet.ConvertTo(dr.Table.DataSet).ExtendedProperties.Contains("BILL_AUDITING").ToString();
            }
            if (tableName == "MF_EXP")
            {
                //新增时判断关账日期
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["EP_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                    {
                        throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["EP_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                    {
                        throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                SQNO _sqNo = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //取单号
                    dr["EP_NO"] = _sqNo.Set(_ep_Id, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["EP_DD"]), dr["BIL_TYPE"].ToString());
                    //写入默认栏位值
                    dr["PRT_SW"] = "N";
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    dr["OPN_ID"] = "2";
                }

                //产生凭证
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }
                // if (String.IsNullOrEmpty(_billAuditing) && string.IsNullOrEmpty(_saveId))
                if (String.IsNullOrEmpty(_billAuditing) && _saveId == "T")//MODIFY LWH 090914 不走审核保存也没有写入CHK_MAN的值修改。
                {
                    //#region 审核关联
                    //AudParamStruct _aps = new AudParamStruct();
                    //if (statementType != StatementType.Delete)
                    //{
                    //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                    //    _aps.BIL_ID = dr["EP_ID"].ToString();
                    //    _aps.BIL_NO = dr["EP_NO"].ToString();
                    //    _aps.BIL_DD = Convert.ToDateTime(dr["EP_DD"]);
                    //    _aps.USR = dr["USR"].ToString();
                    //    _aps.DEP = dr["DEP"].ToString();
                    //    _aps.SAL_NO = dr["USR_NO"].ToString();
                    //    //_aps.MOB_ID = ""; //新加的部分，对应审核模板
                    //}
                    //else
                    //    _aps = new AudParamStruct(Convert.ToString(dr["EP_ID", DataRowVersion.Original]), Convert.ToString(dr["EP_NO", DataRowVersion.Original]));
                    //Auditing _auditing = new Auditing();
                    //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                    //if (!string.IsNullOrEmpty(_auditErr))
                    //{
                    //    throw new SunlikeException(_auditErr);
                    //}
                    //#endregion
                }
            }
            else if ((tableName == "TF_EXP"))
            {
                if (!_isRunAuditing && (_saveId == "T" || _billAuditing == "T"))
                {
                    if (statementType == StatementType.Delete)
                    {
                        //删除的时候，先删除收款单，抵冲销后删除立账单
                        //收款操作
                        this.UpdateMon(dr, false);
                        //立账操作
                        this.UpdateArp(dr);
                    }
                    else
                    {
                        //立账操作
                        this.UpdateArp(dr);
                        //收款操作
                        this.UpdateMon(dr, false);
                    }
                }
                else
                {
                    this.UpdateMon(dr, true);
                }
                if (dr.RowState == DataRowState.Deleted)
                {
                    //判断是否有补开发票则不允许删除
                    checktflz(dr);

                }
            }
            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }

        private object SunlikeException(string _auditErr)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// BeforeDsSave
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dtMf = ds.Tables["MF_EXP"];
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["EP_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["EP_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion
        }
        /// <summary>
        /// AfterUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            if (tableName == "MF_EXP" && statementType == StatementType.Delete)
            {
                SQNO _sqNo = new SQNO();
                _sqNo.Delete(dr["EP_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());

            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        #endregion

        #region 立账
        /// <summary>
        /// 立账
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        public void SetArp(string epId, string epNo)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMonEX.GetData(epId, epNo, false);
            DataTable _mf_expTable = _ds.Tables["MF_EXP"];
            DataTable _tf_expTable = _ds.Tables["TF_EXP"];
            if (_mf_expTable.Rows.Count > 0 && (!Convert.IsDBNull(_mf_expTable.Rows[0]["USR"])))
            {
                Arp _arp = new Arp();
                string _bilDd = _mf_expTable.Rows[0]["EP_DD"].ToString();
                string _dep = _mf_expTable.Rows[0]["DEP"].ToString();
                string _usr = _mf_expTable.Rows[0]["USR"].ToString();

                string _curId = "";
                string _excrto = ""; //带表身记录
                string _amtn = ""; //带表身记录
                string _amtn_net = ""; //带表身记录
                string _amt = ""; //带表身记录
                string _tax = ""; //带表身记录
                string _arpNo = "";
                string _arpId = "";

                for (int i = 0; i < _tf_expTable.Rows.Count; i++)
                {
                    Cust _cust = new Cust();
                    string _cls2 = _cust.GetCustCls2(_tf_expTable.Rows[0]["CUS_NO"].ToString());
                    if (_cls2 == "1")
                    {
                        if (Convert.IsDBNull(_tf_expTable.Rows[i]["ARP_NO"]))
                        {
                            System.Collections.Hashtable _ht = _cust.GetPAYInfo(_tf_expTable.Rows[0]["CUS_NO"].ToString(), _mf_expTable.Rows[0]["EP_DD"].ToString());
                            if (_ht["PAY_DD"] != null)
                            {
                                _tf_expTable.Rows[i]["PAY_DD"] = _ht["PAY_DD"].ToString();
                            }
                            if (_ht["PAY_REM"] != null)
                            {
                                _tf_expTable.Rows[i]["PAY_REM"] = _ht["PAY_REM"].ToString();
                            }
                            if (_ht["CHK_DD"] != null)
                            {
                                _tf_expTable.Rows[i]["CHK_DD"] = _ht["CHK_DD"].ToString();
                            }
                            if (_ht["CHK_DAYS"] != null)
                            {
                                _tf_expTable.Rows[i]["CHK_DAYS"] = _ht["CHK_DAYS"].ToString();
                            }
                            if (_ht["PAY_DAYS"] != null)
                            {
                                _tf_expTable.Rows[i]["PAY_DAYS"] = _ht["PAY_DAYS"].ToString();
                            }
                            if (_ht["INT_DAYS"] != null)
                            {
                                _tf_expTable.Rows[i]["INT_DAYS"] = _ht["INT_DAYS"].ToString();
                            }
                            if (_ht["PAY_MTH"] != null)
                            {
                                _tf_expTable.Rows[i]["PAY_MTH"] = _ht["PAY_MTH"].ToString();
                            }
                            _excrto = _tf_expTable.Rows[i]["EXC_RTO"].ToString();
                            _amtn = Convert.ToString(Convert.ToDecimal(_tf_expTable.Rows[i]["AMTN_NET"].ToString()) + Convert.ToDecimal(_tf_expTable.Rows[i]["TAX"].ToString()));
                            _amtn_net = _tf_expTable.Rows[i]["AMTN_NET"].ToString();
                            if (!Convert.IsDBNull(_tf_expTable.Rows[i]["AMT"].ToString()))
                                _amt = _tf_expTable.Rows[i]["AMT"].ToString();
                            else
                                _amt = null;
                            if (!Convert.IsDBNull(_tf_expTable.Rows[i]["TAX"].ToString()))
                                _tax = _tf_expTable.Rows[i]["TAX"].ToString();
                            else
                                _tax = null;
                            if (Convert.IsDBNull(_tf_expTable.Rows[i]["CUR_ID"]))
                                _curId = _tf_expTable.Rows[i]["CUR_ID"].ToString();
                            else
                                _curId = "";
                            if (epId == "EP")
                                _arpId = "2";
                            else
                                _arpId = "1";
                            _arpNo = _arp.UpdateMfArp(_arpId, "2", epId, epNo, Convert.ToDateTime(_bilDd), "", _dep, _usr, _curId, Convert.ToDecimal(_excrto), Convert.ToDecimal(_amtn), Convert.ToDecimal(_amtn_net), Convert.ToDecimal(_amt), Convert.ToDecimal(_tax), _tf_expTable.Rows[i], "");
                            _tf_expTable.Rows[i]["ARP_NO"] = _arpNo;
                        }
                    }
                }
                this.UpdateDataSet(_ds);
            }
        }

        /// <summary>
        /// 立账操作
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isAdd"></param>
        private string UpdateArp(DataRow dr)
        {
            decimal _amtn = 0;//未税本位币
            decimal _amt = 0;//外币
            decimal _tax = 0;//税额
            string _arpId = "1";//应收付注记（1应收；2应付）
            decimal _rtoTax = 0;//税率RTO_TAX
            if (dr.RowState != DataRowState.Deleted)
            {
                if (!String.IsNullOrEmpty(dr["RTO_TAX"].ToString()))
                {
                    _rtoTax = Convert.ToDecimal(dr["RTO_TAX"]) / 100;
                }
                else
                {
                    _rtoTax = 1;
                }
                if (!String.IsNullOrEmpty(dr["AMT"].ToString()))
                {
                    _amt += Convert.ToDecimal(dr["AMT"]);
                    if (dr["TAX_ID"].ToString() == "3")
                    {
                        _amt += _amt * _rtoTax;
                    }
                }
                if (!String.IsNullOrEmpty(dr["AMTN_NET"].ToString()))
                {
                    _amtn += Convert.ToDecimal(dr["AMTN_NET"]);
                }
                if (!String.IsNullOrEmpty(dr["TAX"].ToString()))
                {
                    _tax += Convert.ToDecimal(dr["TAX"]);
                }
                if (dr["EP_ID"].ToString() == "EP")
                {
                    _arpId = "2";
                }
            }
            Arp _arp = new Arp();
            string _arpNo = "";

            //修改和删除操作前判断立账单是否可删除
            if (dr.RowState != DataRowState.Added && dr.RowState != DataRowState.Unchanged)
            {
                _arpNo = dr["ARP_NO", DataRowVersion.Original].ToString();
                //取得本单据收款单信息
                Bills _bills = new Bills();
                string _rpId = "1";
                if (dr["EP_ID", DataRowVersion.Original].ToString() == "EP")
                {
                    _rpId = "2";
                }
                string _rpNo = "";
                _rpNo = dr["RP_NO", DataRowVersion.Original].ToString();
                SunlikeDataSet _dsMon = _bills.GetData(_rpId, _rpNo, false);
                if (!_arp.DeleteMfArp(_arpNo, _dsMon.Tables["TF_MON"], true))
                {
                    throw new SunlikeException("无法删除立帐单:" + _arpNo);
                }
            }
            //新增和修改时，产生新的立账单
            if (dr.RowState != DataRowState.Deleted && _amt != 0)
            {
                decimal _excRto = 1;
                string _usr = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR"].ToString();
                string _bilType = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["BIL_TYPE"].ToString();
                if (!String.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                    _excRto = Convert.ToDecimal(dr["EXC_RTO"]);

                Indx1 indx1 = new Indx1();
                SunlikeDataSet indxDs = indx1.GetData(dr["IDX_NO"].ToString());
                string _rem = indxDs.Tables[0].Rows[0]["NAME"].ToString() + ";" + dr["REM"].ToString();

                _arpNo = _arp.UpdateMfArp(_arpId, "2", dr["EP_ID"].ToString(), dr["EP_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["EP_DD"]), _bilType,
                    dr["DEP"].ToString(), _usr, dr["CUR_ID"].ToString(), _excRto, _amtn + _tax, _amtn, _amt, _tax, dr, _rem);
                if (_arpNo != dr["ARP_NO"].ToString())
                {
                    dr["ARP_NO"] = _arpNo;
                }
            }
            return _arpNo;
        }

        /// <summary>
        /// 更新立账单号
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        /// <param name="arpNo"></param>
        /// <param name="keyItm"></param>
        private void UpdataArpNo(string epId, string epNo, string arpNo, int keyItm)
        {
            DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
            _exp.UpdateArpNo(epId, epNo, arpNo, keyItm);
        }
        #endregion

        #region 更新收付款

        /// <summary>
        /// 更新收付款
        /// </summary>
        /// <param name="dr">其他费用收入/支出表身</param>
        /// <param name="chk">是否走审核流程</param>
        private void UpdateMon(DataRow dr, bool chk)
        {
            DateTime _epDd = DateTime.Now;
            DateTime _clsDd = DateTime.Now;
            string _rpId = "2";
            string _usr = "";
            string _chkMan = "";
            string _usrNo = "";
            string _salNo = "";
            string _dep = "";
            string _mobId = "";

            Bills _bills = new Bills();
            MonStruct _mon = new MonStruct();

            #region 收付款单据别rpId
            if (dr.RowState != DataRowState.Deleted)
            {
                if (string.Compare("ER", dr["EP_ID"].ToString()) == 0)
                {
                    _rpId = "1";      //其他费用收入
                }
                else
                {
                    _rpId = "2";     //其他费用支出
                }
            }
            else
            {
                if (string.Compare("ER", dr["EP_ID", DataRowVersion.Original].ToString()) == 0)
                {
                    _rpId = "1";      //其他费用收入
                }
                else
                {
                    _rpId = "2";     //其他费用支出
                }
            }
            #endregion

            if (dr.Table.DataSet.Tables["MF_EXP"].Rows[0].RowState != DataRowState.Deleted)
            {
                if (chk)
                {
                    _mon.AddTcMon = false;//走审核时不写入TC_MON
                    _chkMan = "";
                }
                else
                {
                    _mon.AddTcMon = true;
                    _chkMan = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["CHK_MAN"].ToString();
                    if (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["CLS_DATE"].ToString()))
                    {
                        _clsDd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["CLS_DATE"]);
                    }
                }

                if (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["EP_DD"].ToString()))
                {
                    _epDd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["EP_DD"]);//收付款日期
                }
                _mobId = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["MOB_ID"].ToString();
                _usrNo = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR_NO"].ToString();
                _usr = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR"].ToString();
                if (dr.RowState != DataRowState.Deleted)
                {
                    _salNo = dr["SAL_NO"].ToString();
                }
                else
                {
                    _salNo = dr["SAL_NO", DataRowVersion.Original].ToString();
                }
                _dep = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["DEP"].ToString();
            }

            if (dr.RowState == DataRowState.Deleted)
            {
                _bills.DelRcvPay(_rpId, dr["RP_NO", DataRowVersion.Original].ToString());
            }
            else
            {
                bool rebill = true;
                if (dr.RowState == DataRowState.Modified && String.IsNullOrEmpty(dr["AMT_RP"].ToString()))
                {
                    rebill = false;
                    _bills.DelRcvPay(_rpId, dr["RP_NO", DataRowVersion.Original].ToString());
                    dr["RP_NO"] = DBNull.Value;
                }
                if (!String.IsNullOrEmpty(dr["AMT_RP"].ToString()) && rebill)
                {
                    //生成预收款单据
                    _mon.Usr = _usr;
                    _mon.ChkMan = _chkMan;
                    _mon.MobId = _mobId;
                    _mon.UsrNo = _usrNo;
                    //_mon.SalNo = _salNo;
                    _mon.Dep = _dep;
                    _mon.RpDd = _epDd;
                    _mon.ClsDate = _clsDd;
                    _mon.RpId = _rpId;
                    _mon.RpNo = dr["RP_NO"].ToString();
                    _mon.BilId = dr["EP_ID"].ToString();
                    _mon.BilNo = dr["EP_NO"].ToString();
                    _mon.IrpId = "F";
                    _mon.JsfId = "T";
                    _mon.CusNo = dr["CUS_NO"].ToString();
                    _mon.CurId = dr["CUR_ID"].ToString();
                    _mon.BaccNo = dr["BACC_NO"].ToString();
                    _mon.CaccNo = dr["CACC_NO"].ToString();
                    if (string.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                        _mon.ExcRto = 1;
                    else
                        _mon.ExcRto = Convert.ToDecimal(dr["EXC_RTO"].ToString());

                    #region 银行
                    if (string.IsNullOrEmpty(dr["AMT_BB"].ToString()))
                    {
                        _mon.AmtBb = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto == 1)
                        {
                            _mon.AmtBb = Convert.ToDecimal(dr["AMTN_BB"]);//取本位币
                        }
                        else
                        {
                            _mon.AmtBb = Convert.ToDecimal(dr["AMT_BB"].ToString());
                        }
                    }

                    if (string.IsNullOrEmpty(dr["AMTN_BB"].ToString()))
                    {
                        _mon.AmtnBb = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto != 1)
                        {
                            _mon.AmtnBb = Convert.ToDecimal(dr["AMT_BB"].ToString()) * _mon.ExcRto;
                        }
                        else
                        {
                            _mon.AmtnBb = Convert.ToDecimal(dr["AMTN_BB"].ToString());
                        }
                    }
                    #endregion

                    #region 现金
                    if (string.IsNullOrEmpty(dr["AMT_BC"].ToString()))
                    {
                        _mon.AmtBc = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto == 1)
                        {
                            _mon.AmtBc = Convert.ToDecimal(dr["AMTN_BC"]);
                        }
                        else
                        {
                            _mon.AmtBc = Convert.ToDecimal(dr["AMT_BC"].ToString());
                        }
                    }

                    if (string.IsNullOrEmpty(dr["AMTN_BC"].ToString()))
                    {
                        _mon.AmtnBc = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto != 1)
                        {
                            _mon.AmtnBc = Convert.ToDecimal(dr["AMT_BC"].ToString()) * _mon.ExcRto;
                        }
                        else
                        {
                            _mon.AmtnBc = Convert.ToDecimal(dr["AMTN_BC"].ToString());
                        }
                    }
                    #endregion

                    #region 票据

                    if (string.IsNullOrEmpty(dr["AMT_CHK"].ToString()))
                    {
                        _mon.AmtChk = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto == 1)
                        {
                            _mon.AmtChk = Convert.ToDecimal(dr["AMTN_CHK"]);
                        }
                        else
                        {
                            _mon.AmtChk = Convert.ToDecimal(dr["AMT_CHK"].ToString());
                        }
                    }

                    if (string.IsNullOrEmpty(dr["AMTN_CHK"].ToString()))
                    {
                        _mon.AmtnChk = 0;
                        //该属性为TRUE生成TF_MON4的表身
                        _mon.AddMon4 = false;
                    }
                    else
                    {
                        if (_mon.ExcRto != 1)
                        {
                            _mon.AmtnChk = Convert.ToDecimal(dr["AMT_CHK"].ToString()) * _mon.ExcRto;
                        }
                        else
                        {
                            _mon.AmtnChk = Convert.ToDecimal(dr["AMTN_CHK"].ToString());
                        }

                        _mon.ChkNo = dr["CHK_NO"].ToString();
                        _mon.BankNo = dr["BANK_NO"].ToString();
                        _mon.BaccNoChk = dr["BACC_NO_CHK"].ToString();
                        if (string.IsNullOrEmpty(dr["END_DD"].ToString()))
                        {
                            _mon.EndDd = DateTime.Now;
                        }
                        else
                        {
                            _mon.EndDd = Convert.ToDateTime(dr["END_DD"].ToString());
                        }
                        _mon.ChkKnd = dr["CHK_KND"].ToString();
                        //该属性为TRUE生成TF_MON4的表身
                        if (_mon.AmtnChk != 0)
                            _mon.AddMon4 = true;
                    }

                    #endregion
                    //_mon.AmtnCls = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
                    //_mon.AmtCls = _mon.AmtBb + _mon.AmtBc + _mon.AmtChk + _mon.AmtOther;
                    _mon.Amtn = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
                    _mon.ArpNo = dr["ARP_NO"].ToString();
                    dr["RP_NO"] = _bills.AddRcvPay(_mon);
                }
            }
        }

        /// <summary>
        /// 更新收款单号
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        /// <param name="rpNo"></param>
        /// <param name="keyItm"></param>
        private void UpdateMonRpNo(string epId, string epNo, string rpNo, int keyItm)
        {
            DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
            _exp.UpdateRpNo(epId, epNo, rpNo, keyItm);
        }
        #endregion

        #region 更新凭证号
        /// <summary>
        /// 更新凭证号
        /// </summary>
        /// <param name="dr">MF数据行</param>
        /// <param name="statementType"></param>
        /// <returns></returns>
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
            if (statementType == StatementType.Insert)
            {
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                _getVoh = _compInfo.VoucherInfo.GenVoh.EXP;
                if ((_getVoh || this._makeVohNo) && !String.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, dr["EP_ID"].ToString(), out _vohNoError);
                    _vohNo = dr["VOH_NO"].ToString();
                }
            }
            else if (statementType == StatementType.Update)
            {
                DrpVoh _voh = new DrpVoh();
                if (this._reBuildVohNo)
                {
                    if (!String.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_vohNo = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                    if (!String.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                    {
                        CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                        bool _getVoh = false;
                        _getVoh = _compInfo.VoucherInfo.GenVoh.EXP;
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData(dr["EP_ID"].ToString(), dr["EP_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, dr["EP_ID"].ToString(), out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(dr["VOH_ID"].ToString()) && String.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                        bool _getVoh = false;
                        _getVoh = _compInfo.VoucherInfo.GenVoh.EXP;
                        if (_getVoh || this._makeVohNo)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData(dr["EP_ID"].ToString(), dr["EP_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, dr["EP_ID"].ToString(), out _vohNoError);
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
            else if (statementType == StatementType.Delete)
            {
                if (!String.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
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
        /// 更新凭证号码
        /// </summary>
        /// <param name="epId">费用别</param>
        /// <param name="epNo">费用号码</param>
        /// <param name="vohNo">凭证号码</param>
        public void UpdateVohNo(string epId, string epNo, string vohNo)
        {
            DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
            _exp.UpdateVohNo(epId, epNo, vohNo);
        }
        #endregion

        #region 反审核后删除立账记录
        /// <summary>
        /// 反审核后删除立账记录
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        public void DeleteArp(string epId, string epNo)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMonEX.GetData(epId, epNo, false);
            DataTable _mf_expTable = _ds.Tables["TF_EXP"];
            Arp _arp = new Arp();
            try
            {
                for (int i = 0; i < _mf_expTable.Rows.Count; i++)
                {
                    if (_mf_expTable.Rows[i]["ARP_NO"] != null)
                    {
                        if (!_arp.DeleteMfArp(_mf_expTable.Rows[i]["ARP_NO"].ToString()))//删除立账单
                        {
                            throw new SunlikeException(/*已冲帐,不能删除该笔数据*/"RCID=MON.HINT.ARP_NO");
                        }
                    }
                }
                DeleteArpNo(epId, epNo);//SetArpNull(pEpId,pEpNo);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        #endregion

        #region 反审核后删除费用项目中的立账单据信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        public void DeleteArpNo(string epId, string epNo)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMonEX.GetData(epId, epNo, false);
            DataTable _tf_expTable = _ds.Tables["TF_EXP"];
            for (int i = 0; i < _tf_expTable.Rows.Count; i++)
            {
                _tf_expTable.Rows[i]["ARP_NO"] = System.DBNull.Value;
            }
            this.UpdateDataSet(_ds);
        }

        #endregion

        #region 单据调用生成费用单
        /// <summary>
        /// 单据调用生成费用单
        /// </summary>
        /// <param name="epId">费用或支出</param>
        /// <param name="bilId">单据类型</param>
        /// <param name="bilNo">单据号</param>
        /// <param name="amtn">金额</param>
        /// <param name="idxNo">中类代号</param>
        /// <param name="cusNo">客户代号</param>
        /// <param name="usr"></param>
        /// <param name="accNo"></param>
        /// <param name="dep"></param>
        /// <param name="usrNo"></param>
        public string SetExpForBill(string epId, string bilId, string bilNo, decimal amtn, string idxNo, string cusNo, string usr, string dep, string accNo, string usrNo)
        {
            DbMonEX _dbMon = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMon.GetData(bilId, bilNo);
            DataTable _dtMf = _ds.Tables["MF_EXP"];
            DataTable _dtTf = _ds.Tables["TF_EXP"];
            if (_dtMf.Rows.Count > 0)
            {
                // SetCanModify(_ds, false);
                string _modiyCode = SetCanModify(_ds, false);/*MODIFY LWH 090907*/
                if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F" && _modiyCode != "3")
                {
                    if (_modiyCode == "1")
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.INTOAUT");
                    }
                    else if (_modiyCode == "2")
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.INARP");
                    }
                    else if (_modiyCode == "4")
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.LOCKED");
                    }
                    else
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.INTOAUT");
                    }
                }
                _dtMf.Rows[0]["EP_ID"] = epId;
                _dtMf.Rows[0]["USR"] = usr;
                _dtMf.Rows[0]["USR_NO"] = usr;
                _dtTf.Rows[0]["AMT"] = amtn;
                _dtTf.Rows[0]["AMTN_NET"] = amtn;
                _dtTf.Rows[0]["TAX"] = 0;
                _dtTf.Rows[0]["IDX_NO"] = idxNo;
                _dtTf.Rows[0]["BIL_ID"] = bilId;
                _dtTf.Rows[0]["BIL_NO"] = bilNo;
                _dtTf.Rows[0]["CUS_NO"] = cusNo;
            }
            else
            {
                this.AddNewData(_ds, epId, usr);
                _ds.Tables["MF_EXP"].Rows[0]["DEP"] = dep;
                DataRow _dr = _dtTf.NewRow();
                _dr["EP_ID"] = epId;
                _dr["EP_NO"] = _dtMf.Rows[0]["EP_NO"];
                _dr["ITM"] = 1;
                _dr["AMT"] = amtn;
                _dr["AMTN_NET"] = amtn;
                _dr["TAX"] = 0;
                _dr["IDX_NO"] = idxNo;
                _dr["ACC_NO"] = accNo;
                _dr["BIL_ID"] = bilId;
                _dr["BIL_NO"] = bilNo;
                _dr["CUS_NO"] = cusNo;
                _dtTf.Rows.Add(_dr);
            }
            this.UpdateData(null, _ds, true);
            return _ds.Tables["MF_EXP"].Rows[0]["EP_NO"].ToString();
        }
        /// <summary>
        /// 删除单据时删除费用单
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="bilNo"></param>
        public void DelExpForBill(string bilId, string bilNo)
        {
            DbMonEX _dbMon = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMon.GetData(bilId, bilNo);
            DataTable _dtMf = _ds.Tables["MF_EXP"];
            DataTable _dtTf = _ds.Tables["TF_EXP"];

            // SetCanModify(_ds, false);
            string _modiyCode = SetCanModify(_ds, false);/*MODIFY LWH 090907*/
            if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F" && _modiyCode != "3")
            {
                if (_modiyCode == "1")
                {
                    throw new SunlikeException("RCID=COMMON.HINT.INTOAUT");
                }
                else if (_modiyCode == "2")
                {
                    throw new SunlikeException("RCID=COMMON.HINT.INARP");
                }
                else if (_modiyCode == "4")
                {
                    throw new SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
                else
                {
                    throw new SunlikeException("RCID=COMMON.HINT.INTOAUT");
                }
            }
            foreach (DataRow dr in _dtTf.Rows)
            {
                dr.Delete();
            }
            foreach (DataRow dr in _dtMf.Rows)
            {
                dr.Delete();
            }
            this.UpdateDataSet(_ds);
        }
        #endregion

        #region 判断单据表身是否补开发票则不允许删除
        /// <summary>
        /// 判断单据表身是否补开发票则不允许删除
        /// </summary>
        /// <param name="dr">tf_ck.row</param>
        private void checktflz(DataRow dr)
        {
            string _blId = dr["EP_ID", DataRowVersion.Original].ToString();
            if (_blId.Equals("ER"))
            {
                InvIK _invik = new InvIK();

                string bilId = dr["EP_ID", DataRowVersion.Original].ToString();
                string ckNo = dr["EP_NO", DataRowVersion.Original].ToString();

                SunlikeDataSet _ds = _invik.GetInTfLz(bilId, ckNo);
                if (_ds.Tables["TF_LZ"].Rows.Count > 0)
                {
                    throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + "PARAM=" + _ds.Tables["TF_LZ"].Rows[0]["LZ_NO"].ToString());//无法删除单号，原因：{0}
                }
            }
            else
            {
                InvLI _invli = new InvLI();
                string bilId = dr["EP_ID", DataRowVersion.Original].ToString();
                string ckNo = dr["EP_NO", DataRowVersion.Original].ToString();

                SunlikeDataSet _ds = _invli.GetInTfLz(bilId, ckNo);
                if (_ds.Tables["TF_LZ1"].Rows.Count > 0)
                {
                    throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + ",PARAM=" + _ds.Tables["TF_LZ1"].Rows[0]["LZ_NO"].ToString());//无法删除单号，原因：{0}
                }
            }

        }

        #endregion

        #region IAuditing Members
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
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData(bil_id, bil_no, false);
                DataRow _drMf = _ds.Tables["MF_EXP"].Rows[0];
                DataTable _dtTf = _ds.Tables["TF_EXP"];

                //更新审核人信息
                DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
                _exp.UpdateCheck(bil_id, bil_no, chk_man, cls_dd);
                _drMf["CHK_MAN"] = chk_man;
                _drMf["CLS_DATE"] = cls_dd;

                //更新凭证
                string _vohNo = this.UpdateVohNo(_drMf, StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);

                foreach (DataRow dr in _dtTf.Rows)
                {
                    //立账
                    string _arpNo = this.UpdateArp(dr);
                    this.UpdataArpNo(bil_id, bil_no, _arpNo, Convert.ToInt32(dr["KEY_ITM"]));
                    //收款
                    this.UpdateMon(dr, false);
                }
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }

        /// <summary>
        /// 审核不同意
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            // TODO:  Add MonEX.Deny implementation
            return null;
        }

        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData(bil_id, bil_no, false);
                DataRow _drMf = _ds.Tables["MF_EXP"].Rows[0];
                DataTable _dtTf = _ds.Tables["TF_EXP"];
                SetCanModify(_ds, false);
                string _modiyCode = SetCanModify(_ds, false); /*MODIFY LWH 090907*/
                if (_modiyCode == "0")
                //if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "T")
                {
                    //更新审核人信息
                    DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
                    _exp.UpdateCheck(bil_id, bil_no, "", DateTime.Now);

                    //更新凭证号码
                    this.UpdateVohNo(_drMf, StatementType.Delete);
                    this.UpdateVohNo(bil_id, bil_no, "");

                    foreach (DataRow dr in _dtTf.Rows)
                    {
                        //更新收款单
                        this.UpdateMon(dr, true);
                        dr.Delete();
                        //更新立账单号
                        this.UpdateArp(dr);
                        this.UpdataArpNo(bil_id, bil_no, "", Convert.ToInt32(dr["KEY_ITM", DataRowVersion.Original]));
                    }
                }
                //else
                //{
                //    throw new Exception("" + _modiyCode + "");
                //}
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
            }
            return _error;
        }

        #endregion

        #region IAuditingInfo Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bil_Id"></param>
        /// <param name="Bil_No"></param>
        /// <param name="RejectSH"></param>
        /// <returns></returns>
        public string GetBillInfo(string Bil_Id, string Bil_No, ref bool RejectSH)
        {
            // TODO:  Add MonEX.GetBillInfo implementation
            return null;
        }

        #endregion
    }
}
