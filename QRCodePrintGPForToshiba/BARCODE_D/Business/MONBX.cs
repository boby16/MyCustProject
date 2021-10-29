using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Data;

namespace Sunlike.Business
{
    /// <summary>
    /// 报销单
    /// </summary>
    public class MONBX : BizObject, IAuditing, ICloseBill
    {
        private string _usr = "";
        private bool _isRunAuditing;

        #region constructor
        /// <summary>
        /// 报销单
        /// </summary>
        public MONBX()
        { }
        #endregion

        #region 取报销单
        /// <summary>
        /// 取报销单
        /// </summary>
        /// <param name="pgm">PGM</param>
        /// <param name="usr">用户</param>
        /// <param name="bxNo">报销单号</param>
        /// <param name="onlyFillSchema">是否取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm, string usr, string bxNo, bool onlyFillSchema)
        {
            DbMONBX _dbBX = new DbMONBX(Comp.Conn_DB);
            SunlikeDataSet _ds = new SunlikeDataSet();
            _ds = _dbBX.GetData(bxNo, Comp.CompNo, onlyFillSchema);
            if (!string.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                string _billDep = "";
                string _billUsr = "";
                if (_ds.Tables.Count > 0 && _ds.Tables.Contains("MF_BX") && _ds.Tables["MF_BX"].Rows.Count > 0)
                {
                    _billDep = _ds.Tables["MF_BX"].Rows[0]["DEP"].ToString();
                    _billUsr = _ds.Tables["MF_BX"].Rows[0]["USR"].ToString();
                }
                Hashtable _right = Users.GetBillRight(pgm, usr, _billDep, _billUsr);
                _ds.ExtendedProperties["UPD"] = _right["UPD"];
                _ds.ExtendedProperties["DEL"] = _right["DEL"];
                _ds.ExtendedProperties["PRN"] = _right["PRN"];
                _ds.ExtendedProperties["LCK"] = _right["LCK"];
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            this.SetCanModify(usr, _ds, true);
            return _ds;
        }

        /// <summary>
        ///获取待冲销的报销单
        /// </summary>
        /// <param name="bxNo">报销单号</param>
        /// <returns></returns>
        public DataTable GetMfData(string bxNo)
        {
            DbMONBX _db = new DbMONBX(Comp.Conn_DB);
            return _db.GetMfData(bxNo);
        }
        #endregion

        #region SetCanModify
        /// <summary>
        /// 是否可以更改
        /// </summary>
        /// <param name="usr">当前用户</param>
        /// <param name="ds">SunlikeDataSet</param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        private void SetCanModify(string usr, SunlikeDataSet ds, bool bCheckAuditing)
        {
            bool _bCanModify = true;
            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_BX") && ds.Tables["MF_BX"].Rows.Count > 0)
            {
                //判断审核流程
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing("BX", ds.Tables["MF_BX"].Rows[0]["BX_NO"].ToString()))
                    {
                        _bCanModify = false;
                        ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");//已进入审核流程不能修改
                    }
                }
                //判断是否关帐
                if (Comp.HasCloseBill(Convert.ToDateTime(ds.Tables["MF_BX"].Rows[0]["BX_DD"]), ds.Tables["MF_BX"].Rows[0]["DEP"].ToString(), "CLS_MON"))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");//已关账不能修改
                }
                //判断是否结案
                if (ds.Tables["MF_BX"].Rows[0]["CLS_ID"].ToString() == "T")
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");// 结案不能修改
                }
                //判断是否锁单
                if (!String.IsNullOrEmpty(ds.Tables["MF_BX"].Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");//已锁单不能修改
                }
            }
            //判断是否转帐户收支单
            if (ds.Tables.Count > 0 && ds.Tables.Contains("TF_BX") && ds.Tables["TF_BX"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["TF_BX"].Rows)
                {
                    if (!String.IsNullOrEmpty(dr["BAC_EXIST"].ToString()) && Convert.ToInt32(dr["BAC_EXIST"]) > 0)
                    {
                        _bCanModify = false;
                        ////Common.SetCanModifyRem(ds, "RCID=MON.MF_BX.INTO_MF_BAC");//已转帐户收支单!
                        break;
                    }
                }
            }
            //判断报销单中核准金额有审核权限
            ds.ExtendedProperties["AMT_BX_SH"] = "F";
            Users _users = new Users();
            if (_users.IsCompBoss(usr))
            {
                ds.ExtendedProperties["AMT_BX_SH"] = "T";
            }
            else
            {
                DataTable _spcPswdTable = _users.GetSpcPswd(usr, "AMT_BX_SH");
                if (_spcPswdTable.Rows.Count > 0)
                {
                    if (_spcPswdTable.Rows[0]["SPC_ID"].ToString() == "T")
                    {
                        ds.ExtendedProperties["AMT_BX_SH"] = "T";
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="changeDs"></param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, SunlikeDataSet changeDs)
        {
            if (changeDs.Tables.Contains("MF_BX") && changeDs.Tables["MF_BX"].Rows.Count > 0)
            {
                string _bxNo = string.Empty;
                if (changeDs.Tables["MF_BX"].Rows[0].RowState == DataRowState.Deleted)
                {
                    _usr = changeDs.Tables["MF_BX"].Rows[0]["USR", DataRowVersion.Original].ToString();
                    _bxNo = changeDs.Tables["MF_BX"].Rows[0]["BX_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = changeDs.Tables["MF_BX"].Rows[0]["USR"].ToString();
                    _bxNo = changeDs.Tables["MF_BX"].Rows[0]["BX_NO"].ToString();
                }

                #region 取得单据的审核状态

                Auditing _auditing = new Auditing();
                DataRow _dr = changeDs.Tables["MF_BX"].Rows[0];
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
                //_isRunAuditing = _auditing.IsRunAuditing("BX", _usr, _bilType, _mobID);



                #endregion
            }

            DataTable _dtErr = null;
            Hashtable _ht = new Hashtable();
            _ht["MF_BX"] = "BX_NO,BX_DD,DEP,USR_NO,PAY_ID,CUR_ID,EXC_RTO,REM,CLS_ID,USR,SYS_DATE,CHK_MAN,CLS_DATE,MOB_ID,LOCK_MAN,PRT_SW,CPY_SW,PRT_USR,OS_ID,OS_NO";
            _ht["TF_BX"] = "BX_NO,BX_DD,ITM,ACC_NO,AMT,AMTN,AMTN_SH,BX_ITM,BX_AMT,AMT_SH,SAL_NO,DEP,FEE_ID,REM,SDAY,EDAY,DAYS,FORM_CNT,CAS_NO,TASK_ID,OS_ID,OS_NO";
            this.UpdateDataSet(changeDs, _ht);
            if (!changeDs.HasErrors)
            {
                //增加单据权限
                string _UpdUsr = "";
                if (changeDs.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changeDs.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    DataTable _dtMf = changeDs.Tables["MF_BX"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changeDs.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changeDs.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changeDs.ExtendedProperties["PRN"] = _billRight["PRN"];
                        changeDs.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
                SetCanModify(_usr, changeDs, true);
            }
            _dtErr = GetAllErrors(changeDs);
            return _dtErr;
        }

        /// <summary>
        /// 保存单据之前
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            if (tableName == "MF_BX")
            {
                string _bxNo = "";
                if (dr.RowState != DataRowState.Deleted)
                {
                    _bxNo = dr["BX_NO"].ToString();
                }
                else
                {
                    _bxNo = dr["BX_NO", DataRowVersion.Original].ToString();
                }
                if (statementType != StatementType.Insert)
                {
                    //判断是否锁单，如果已经锁单则不让修改。
                    Users _Users = new Users();
                    string _whereStr = "BX_NO = '" + _bxNo + "'";
                    if (_Users.IsLocked("MF_BX", _whereStr))
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                    }
                }
            }
            switch (tableName)
            {
                case "MF_BX":
                    {
                        #region MF_BX
                        SQNO _sq = new SQNO();
                        if (statementType != StatementType.Delete)
                        {
                            #region 保存前检查数据的完整性

                            // 检查报账部门
                            Dept _dept = new Dept();
                            if (!_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["BX_DD"])))
                            {
                                dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEP_NOTEXIST,PARAM=" + dr["DEP"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            // 检查请款人
                            Salm _salm = new Salm();
                            if (!_salm.IsExist(_usr, dr["USR_NO"].ToString(), Convert.ToDateTime(dr["BX_DD"])))
                            {
                                dr.SetColumnError("USR_NO", "RCID=COMMON.HINT.PAYMAN_NOTEXIST,PARAM=" + dr["USR_NO"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }

                            #endregion

                            if (statementType == StatementType.Insert)
                            {
                                //取得保存单号
                                dr["BX_NO"] = _sq.Set("BX", _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["BX_DD"]), "");
                                //写入默认栏位值
                                dr["PRT_SW"] = "N";
                                dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            }
                        }
                        else
                        {
                            string _error = _sq.Delete(dr["BX_NO", DataRowVersion.Original].ToString(), _usr);
                            if (!string.IsNullOrEmpty(_error))
                                throw new SunlikeException("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}

                        }
                        //新增时判断关账日期
                        if (statementType != StatementType.Delete)
                        {
                            if (Comp.HasCloseBill(Convert.ToDateTime(dr["BX_DD"]), dr["DEP"].ToString(), "CLS_MON"))
                            {
                                CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                                throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL1,PARAM=" + _compInfo.CloseAccountDateInfo.CLS_MON.ToString(_compInfo.SystemInfo.DATEPATTERN));
                            }
                        }
                        else
                        {
                            if (Comp.HasCloseBill(Convert.ToDateTime(dr["BX_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MON"))
                            {
                                CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                                throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL1,PARAM=" + _compInfo.CloseAccountDateInfo.CLS_MON.ToString(_compInfo.SystemInfo.DATEPATTERN));
                            }
                        }
                        #endregion

                        #region 审核关联
                        //AudParamStruct _aps;
                        //if (statementType != StatementType.Delete)
                        //{
                        //    _aps.BIL_DD =DateTime.Now;
                        //    _aps.BIL_ID = "BX";
                        //    _aps.BIL_NO = dr["BX_NO"].ToString();
                        //    _aps.BIL_TYPE = "";
                        //    _aps.CUS_NO ="";
                        //    _aps.DEP = dr["DEP"].ToString();
                        //    _aps.SAL_NO = "";
                        //    _aps.USR = dr["USR"].ToString();
                        //    ////_aps.MOB_ID = ""; //新加的部分，对应审核模板
                        //}
                        //else
                        //    _aps = new AudParamStruct("BX", Convert.ToString(dr["BX_NO", DataRowVersion.Original]));
                        //Auditing _auditing = new Auditing();
                        //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                        //if (!string.IsNullOrEmpty(_auditErr))
                        //{
                        //    throw new SunlikeException(_auditErr);
                        //}
                        #endregion
                    }
                    break;
                case "TF_BX":
                    {
                        #region TF_BX
                        DbMTNRcv _db = new DbMTNRcv(Comp.Conn_DB);
                        if (statementType != StatementType.Delete)
                        {

                            #region 回写BX_DD
                            if (string.IsNullOrEmpty(dr["BX_DD"].ToString()))
                            {
                                if (dr.Table.DataSet.Tables["MF_BX"].Rows.Count > 0 && dr.Table.DataSet.Tables["MF_BX"].Rows[0].RowState != DataRowState.Deleted)
                                {
                                    dr["BX_DD"] = dr.Table.DataSet.Tables["MF_BX"].Rows[0]["BX_DD"];
                                }
                            }
                            #endregion

                            #region 日期值格式化

                            if (!string.IsNullOrEmpty(dr["SDAY"].ToString()))
                                dr["SDAY"] = Convert.ToDateTime(dr["SDAY"]).ToString(Comp.SQLDateFormat);
                            if (!string.IsNullOrEmpty(dr["EDAY"].ToString()))
                                dr["EDAY"] = Convert.ToDateTime(dr["EDAY"]).ToString(Comp.SQLDateFormat);

                            #endregion

                            #region 写入默认的部门
                            if (String.IsNullOrEmpty(dr["DEP"].ToString()))
                            {
                                if (dr.Table.DataSet.Tables["MF_BX"].Rows.Count > 0 && dr.Table.DataSet.Tables["MF_BX"].Rows[0].RowState != DataRowState.Deleted)
                                {
                                    dr["DEP"] = dr.Table.DataSet.Tables["MF_BX"].Rows[0]["DEP"];
                                }
                            }
                            #endregion

                            #region 走审核时，写入默认的核准金额
                            /* 
                             * modified: yzb 09.07.22(BUGNO:84212) (安信)
                             * 
                             *      1、制单人登打BX单，如果需走审核，则缺省将[报销金额]付值给[核准金额]，[核准人]为空； 
                             *      2、制人单登打BX单，不需走审核，则维持目前系统做法(不付值[核准金额])。 
                             * 
                             * remarks: 由于前台页面输入【核准金额】时，必须要输入【核准人】，而此处自动写入【核准金额】，则没有【核准人】
                             *          所以BX单修改时，为了不把核准动作时输入的【核准金额】覆盖掉，此处判断如果【核准人】为空，才赋值【核准金额】
                            */
                            if (_isRunAuditing)
                            {
                                if (String.IsNullOrEmpty(dr["SAL_NO"].ToString()))
                                {
                                    dr["AMTN_SH"] = dr["AMTN"];
                                    dr["AMT_SH"] = dr["AMT"];
                                }
                            }

                            #endregion

                            #region 回写来源单报销金额

                            if (!String.IsNullOrEmpty(dr["OS_ID"].ToString()) && !String.IsNullOrEmpty(dr["OS_NO"].ToString()))
                            {
                                if (!_isRunAuditing)
                                {
                                    UpdateAmtBx(dr, true);
                                    if (statementType == StatementType.Update)
                                    {
                                        UpdateAmtBx(dr, false);
                                    }
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            #region 回写来源单报销金额
                            if (!String.IsNullOrEmpty(dr["OS_ID", DataRowVersion.Original].ToString()) && !String.IsNullOrEmpty(dr["OS_NO", DataRowVersion.Original].ToString()))
                            {
                                if (!_isRunAuditing)
                                {
                                    UpdateAmtBx(dr, false);
                                }
                            }
                            #endregion
                        }

                        #endregion
                    }
                    break;
            }
        }

        protected override void BeforeDsSave(DataSet ds)
        {
            #region 单据追踪
            //DataTable _dt = ds.Tables[0];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();

            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "BX");
            //}
            #endregion

        }

        #endregion

        #region 更新来源单报销费用
        /// <summary>
        /// 更新来源单报销费用
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="IsAdd"></param>
        private void UpdateAmtBx(DataRow dr, bool IsAdd)
        {
            string osId = "";
            string osNo = "";
            decimal _amtBx = 0;
            if (IsAdd)
            {
                osId = dr["OS_ID"].ToString();
                osNo = dr["OS_NO"].ToString();
                if (!String.IsNullOrEmpty(osId) && !String.IsNullOrEmpty(osNo) && !String.IsNullOrEmpty(dr["AMT_SH"].ToString()))
                {
                    _amtBx = Convert.ToDecimal(dr["AMT_SH"]);
                }
            }
            else
            {
                osId = dr["OS_ID", DataRowVersion.Original].ToString();
                osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(osId) && !String.IsNullOrEmpty(osNo) && !String.IsNullOrEmpty(dr["AMT_SH", DataRowVersion.Original].ToString()))
                {
                    _amtBx = Convert.ToDecimal(dr["AMT_SH", DataRowVersion.Original]);
                }
                _amtBx *= -1;
            }
            if (!String.IsNullOrEmpty(osId) && !String.IsNullOrEmpty(osNo))
            {
                DbMONBX _dbBx = new DbMONBX(Comp.Conn_DB);
                _dbBx.UpdateAmtBx(osId, osNo, _amtBx);
            }
        }
        #endregion

        #region 取支付方式
        /// <summary>
        /// 取支付方式
        /// </summary>
        /// <returns></returns>
        public SunlikeDataSet GetPayID()
        {
            DbMONBX _dbBX = new DbMONBX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbBX.GetPayID();
            return _ds;
        }
        #endregion

        #region 取费用类别
        /// <summary>
        /// 取费用类别
        /// </summary>
        /// <param name="fee_id">费用代号</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFee(string fee_id)
        {
            DbMONBX _dbBX = new DbMONBX(Comp.Conn_DB);
            return _dbBX.GetDataFee(fee_id);
        }
        #endregion

        #region 更新费用类别
        /// <summary>
        /// 更新费用类别
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        public DataTable UpdateDataFee(SunlikeDataSet ds)
        {
            Hashtable _ht = new Hashtable();
            _ht["EXPENSE"] = "FEE_ID,NAME,ENG_NAME,ACC_NO,CTRL_ID,AMTN,OVER_LIM";
            this.UpdateDataSet(ds, _ht);
            return Sunlike.Business.BizObject.GetAllErrors(ds);
        }
        #endregion

        #region 得到费用会计科目
        /// <summary>
        /// 得到费用会计科目
        /// </summary>
        /// <param name="feeId">费用代号</param>
        /// <returns></returns>
        public string GetAccNo(string feeId)
        {
            DbMONBX _dbBx = new DbMONBX(Comp.Conn_DB);
            return _dbBx.GetAccNo(feeId);
        }

        #endregion

        #region 取来源单报销费用资料
        /// <summary>
        /// 取来源单报销费用资料
        /// </summary>
        /// <param name="osId">来源单别</param>
        /// <param name="osNo">来源单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetOsAmtBxMsg(string osId, string osNo)
        {
            DbMONBX _dbBX = new DbMONBX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbBX.GetOsAmtBxMsg(osId, osNo);
            return _ds;
        }
        #endregion

        #region 取报销单表身信息（根据来源单）
        /// <summary>
        /// 取报销单表身信息（根据来源单）
        /// </summary>
        /// <param name="osId">来源单别</param>
        /// <param name="osNo">来源单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetBxBody(string osId, string osNo)
        {
            DbMONBX _dbBX = new DbMONBX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbBX.GetBxBody(osId, osNo);
            return _ds;
        }
        #endregion

        #region 取得TF_BX最大项次
        /// <summary>
        /// 取得最大项次
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="itm"></param>
        /// <returns></returns>
        public int GetMaxItm(DataTable dt, string itm)
        {
            int _itm = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted && dr[itm] != null && Convert.ToInt32(dr[itm]) > _itm)
                {
                    _itm = Convert.ToInt32(dr[itm]);
                }
            }
            return _itm + 1;
        }
        #endregion

        #region IAuditing Members
        /// <summary>
        /// 审核同意
        /// </summary>
        /// <param name="bil_id">单据别</param>
        /// <param name="bil_no">单号</param>
        /// <param name="chk_man">审核人</param>
        /// <param name="cls_dd">审核日期</param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = String.Empty;
            try
            {
                DbMONBX _dbBx = new DbMONBX(Comp.Conn_DB);
                _dbBx.UpdateChkMan(bil_no, chk_man, cls_dd);

                SunlikeDataSet _ds = _dbBx.GetDataBX(bil_no);
                foreach (DataRow dr in _ds.Tables["TF_BX"].Rows)
                {
                    if (!String.IsNullOrEmpty(dr["OS_ID"].ToString()) && !String.IsNullOrEmpty(dr["OS_NO"].ToString()))
                    {
                        UpdateAmtBx(dr, true);
                    }
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }
        /// <summary>
        /// 审核不同意
        /// </summary>
        /// <param name="bil_id">单据别</param>
        /// <param name="bil_no">单号</param>
        /// <param name="chk_man">审核人</param>
        /// <param name="cls_dd">审核日期</param>
        /// <returns></returns>
        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            return "";

        }
        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="bil_id">单据别</param>
        /// <param name="bil_no">单号</param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            string _error = String.Empty;
            //try
            //{
            //    DbMONBX _dbBx = new DbMONBX(Comp.Conn_DB);
            //    SunlikeDataSet _ds = _dbBx.GetDataBX(bil_no);
            //    if (_ds.Tables.Contains("MF_BX") && _ds.Tables["MF_BX"].Rows.Count > 0)
            //    {
            //        //单据已结案，不能反审核
            //        _usr = _ds.Tables["MF_BX"].Rows[0]["USR"].ToString();
            //        this.SetCanModify(_usr, _ds, false);
            //        string _errorMsg = ""; Common.GetCanModifyRem(_ds);
            //        if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
            //        {
            //            if (_errorMsg.Length > 0)
            //            {
            //                return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;RCID=" + _errorMsg;//反审核失败
            //            }
            //            else
            //            {
            //                return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";//反审核失败
            //            }
            //        }

            //        //清空审核人
            //        _dbBx.UpdateChkMan(bil_no, "", DateTime.Now);

            //        foreach (DataRow dr in _ds.Tables["TF_BX"].Rows)
            //        {
            //            if (!String.IsNullOrEmpty(dr["OS_ID"].ToString()) && !String.IsNullOrEmpty(dr["OS_NO"].ToString()))
            //            {
            //                UpdateAmtBx(dr, false);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _error = ex.Message;
            //}
            return _error;
        }

        #endregion

        #region ICloseBill Members
        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="bil_id">单据别</param>
        /// <param name="bil_no">单号</param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            return CloseBill(bil_no, true);
        }
        /// <summary>
        /// 反结案
        /// </summary>
        /// <param name="bil_id">单据别</param>
        /// <param name="bil_no">单号</param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            return CloseBill(bil_no, false);
        }
        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="bxNo">单号</param>
        /// <param name="close">结案/反结案</param>
        /// <returns></returns>
        private string CloseBill(string bxNo, bool close)
        {
            SunlikeDataSet _ds = this.GetData("MONBX", null, bxNo, false);
            DataTable _Mf = _ds.Tables["MF_BX"];
            DataTable _Tf = _ds.Tables["TF_BX"];
            if (_Mf.Rows.Count > 0)
            {
                DataRow _dr = _Mf.Rows[0];
                bool cls_id = Convert.ToString(_dr["CLS_ID"]) == "T";
                bool isCheck = string.IsNullOrEmpty(Convert.ToString(_dr["CHK_MAN"]));
                if (close && cls_id)
                    return "RCID=COMMON.HINT.CLOSEERROR,PARAM=" + bxNo;//该单据[{0}]已结案,结案动作不能完成!
                if (!close && !cls_id)
                    return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + bxNo;//该单据[{0}]未结案,未结案动作不能完成!

                #region 结案
                DbMONBX _dbBx = new DbMONBX(Comp.Conn_DB);
                _dbBx.CloseBill(bxNo, close);
                #endregion

                #region 回写来源单实际报销金额
                //如果转入帐户收支单，报销单就不能进行结案和反结案动作
                if (!String.IsNullOrEmpty(_dr["OS_ID"].ToString()) && !String.IsNullOrEmpty(_dr["OS_NO"].ToString()))
                {
                    DataRow[] _dra = _Tf.Select("OS_ID='" + _dr["OS_ID"].ToString() + "' AND OS_NO='" + _dr["OS_NO"].ToString() + "'");
                    decimal _amtSh = 0;
                    foreach (DataRow drTf in _dra)
                    {
                        if (!String.IsNullOrEmpty(drTf["AMT_SH"].ToString()))
                            _amtSh += Convert.ToDecimal(drTf["AMT_SH"]);
                    }
                    if (_amtSh > 0 && !String.IsNullOrEmpty(_dr["CHK_MAN"].ToString()) && !String.IsNullOrEmpty(_dr["CLS_DATE"].ToString()))
                    {
                        //通过审核的报销单，才回写来源单实际报销金额,回写金额为:AMT_SH
                        if (close)
                            _amtSh = _amtSh * -1;
                        _dbBx.UpdateAmtBx(_dr["OS_ID"].ToString(), _dr["OS_NO"].ToString(), _amtSh);
                    }
                }
                #endregion
            }
            return "";
        }

        #endregion
    }
}
