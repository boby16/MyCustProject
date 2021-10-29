using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
    /// <summary>
    /// Summary description for JF.
    /// </summary>
    public class JF : BizObject, IAuditing, Sunlike.Business.ICloseBill
    {
        private bool _isRunAuditing;
        /// <summary>
        /// usr
        /// </summary>
        public string _usr = "";

        /// <summary>
        /// 积分
        /// </summary>
        public JF()
        {
        }

        /// <summary>
        /// 取得积分
        /// </summary>
        /// <param name="bilDD">单据日期</param>
        /// <param name="prdNo">品号</param>
        /// <param name="cardNo">会员卡号</param>
        /// <param name="amtnNet">金额</param>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public decimal GetJF(DateTime bilDD, string prdNo, string cusNo, string cardNo, decimal amtnNet)
        {
            DbJF _jf = new DbJF(Comp.Conn_DB);
            decimal _score = _jf.GetJF(bilDD, prdNo, cusNo, cardNo, amtnNet);
            return _score;
        }

        /// <summary>
        /// 取得积分规则
        /// </summary>
        /// <param name="jfID">规则代号</param>
        /// <param name="onlyFillSchema">是否只读取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetRule(int jfID, bool onlyFillSchema)
        {
            DbJF _jf = new DbJF(Comp.Conn_DB);
            SunlikeDataSet _ds = _jf.GetRule(jfID, onlyFillSchema);
            return _ds;
        }

        /// <summary>
        /// 更新积分规则
        /// </summary>
        /// <param name="ruleDS">积分规则资料</param>
        /// <param name="bubbleException">是否抛出异常（true为直接抛出异常，false返回ErrorTable）</param>
        public DataTable UpdateRule(SunlikeDataSet ruleDS, bool bubbleException)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["JF_RULE"] = "JF_ID,REM,F_DD,E_DD,AMTN_NET,JF_SCR";
            _ht["JF_RULE1"] = "JF_ID,ITM,IDX_NO,PRD_NO,CARD_CLS,CUS_NO,AREA_NO";
            this.UpdateDataSet(ruleDS, _ht);
            if (ruleDS.HasErrors && bubbleException)
            {
                string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(ruleDS);
                throw new SunlikeException("RCID=JF.UpdateRule() Error:" + _errorMsg);
            }
            DataTable _dtError = BizObject.GetAllErrors(ruleDS);
            return _dtError;
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

            if (tableName == "JF_RULE")
            {
                if (statementType != StatementType.Delete)
                {
                    if (statementType == StatementType.Insert)
                    {
                        UdfNo _udfNo = new UdfNo();
                        dr["JF_ID"] = Convert.ToInt32(_udfNo.Set("JF_RULE->", 10));
                    }
                    //检查规则是否重复
                    if (dr.Table.DataSet.Tables["JF_RULE1"].Select().Length == 0)
                    {
                        string _sql = "select 1 from JF_RULE A"
                            + " left join JF_RULE1 B on B.JF_ID=A.JF_ID"
                            + " where A.JF_ID<>" + dr["JF_ID"].ToString()
                            + " and ((('" + dr["F_DD"].ToString() + "'>=A.F_DD and '" + dr["F_DD"].ToString()
                            + "'<isnull(A.E_DD,'9999-12-30')+1)";
                        if (String.IsNullOrEmpty(dr["E_DD"].ToString()))
                        {
                            _sql += " or ('9999-12-30'>=A.F_DD and '9999-12-30'<isnull(A.E_DD,'9999-12-30')+1)";
                        }
                        else
                        {
                            _sql += " or ('" + dr["E_DD"].ToString() + "'>=A.F_DD and '" + dr["E_DD"].ToString()
                                + "'<isnull(A.E_DD,'9999-12-30')+1)";
                        }
                        _sql += ") or (('" + dr["F_DD"].ToString() + "'<=A.F_DD and '" + dr["F_DD"].ToString()
                            + "'<isnull(A.E_DD,'9999-12-30')+1)";
                        if (String.IsNullOrEmpty(dr["E_DD"].ToString()))
                        {
                            _sql += " and ('9999-12-30'>=A.F_DD and '9999-12-30'<isnull(A.E_DD,'9999-12-30')+1)";
                        }
                        else
                        {
                            _sql += " and ('" + dr["E_DD"].ToString() + "'>=A.F_DD and '" + dr["E_DD"].ToString()
                                + "'>=isnull(A.E_DD,'9999-12-30')+1)";
                        }
                        _sql += ")) and isnull(B.IDX_NO,'')='' and isnull(B.PRD_NO,'')='' and isnull(B.AREA_NO,'')='' and isnull(B.CUS_NO,'')='' and isnull(B.CARD_CLS,'')=''";
                        Query _query = new Query();
                        SunlikeDataSet _ds = _query.DoSQLString(_sql);
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            throw new Sunlike.Common.Utility.SunlikeException("RCID=INV.HINT.REPEAT");//积分规则重复设定！
                        }
                    }
                }
            }
            else if (tableName == "JF_RULE1")
            {
                if (statementType != StatementType.Delete)
                {
                    //检查中类和品号
                    if (!String.IsNullOrEmpty(dr["IDX_NO"].ToString()) && !String.IsNullOrEmpty(dr["PRD_NO"].ToString()))
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=INV.HINT.ONLY_PRD_NO");//中类代号和品号不能同时输入！
                    }
                    //检查规则是否重复
                    DataRow _drHead = dr.Table.DataSet.Tables["JF_RULE"].Rows[0];
                    string _sql = "select 1 from JF_RULE A"
                        + " left join JF_RULE1 B on B.JF_ID=A.JF_ID"
                        + " where A.JF_ID<>" + dr["JF_ID"].ToString() + " and ((('" + _drHead["F_DD"].ToString()
                        + "'>=A.F_DD and '" + _drHead["F_DD"].ToString() + "'<isnull(A.E_DD,'9999-12-30')+1)";
                    if (String.IsNullOrEmpty(_drHead["E_DD"].ToString()))
                    {
                        _sql += " or ('9999-12-30'>=A.F_DD and '9999-12-30'<isnull(A.E_DD,'9999-12-30')+1)";
                    }
                    else
                    {
                        _sql += " or ('" + _drHead["E_DD"].ToString() + "'>=A.F_DD and '" + _drHead["E_DD"].ToString()
                            + "'<isnull(A.E_DD,'9999-12-30')+1)";
                    }
                    _sql += ") or (('" + _drHead["F_DD"].ToString()
                        + "'<=A.F_DD and '" + _drHead["F_DD"].ToString() + "'<isnull(A.E_DD,'9999-12-30')+1)";
                    if (String.IsNullOrEmpty(_drHead["E_DD"].ToString()))
                    {
                        _sql += " and ('9999-12-30'>=A.F_DD and '9999-12-30'<isnull(A.E_DD,'9999-12-30')+1)";
                    }
                    else
                    {
                        _sql += " and ('" + _drHead["E_DD"].ToString() + "'>=A.F_DD and '" + _drHead["E_DD"].ToString()
                            + "'>=isnull(A.E_DD,'9999-12-30')+1)";
                    }
                    _sql += ")) and isnull(B.IDX_NO,'')='" + dr["IDX_NO"].ToString()
                        + "' and isnull(B.PRD_NO,'')='" + dr["PRD_NO"].ToString()
                        + "' and isnull(B.AREA_NO,'')='" + dr["AREA_NO"].ToString()
                        + "' and isnull(B.CUS_NO,'')='" + dr["CUS_NO"].ToString()
                        + "' and isnull(B.CARD_CLS,'')='" + dr["CARD_CLS"].ToString() + "'";
                    Query _query = new Query();
                    SunlikeDataSet _ds = _query.DoSQLString(_sql);
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=INV.HINT.REPEAT");//积分规则重复设定！
                    }
                }
            }
            else if (tableName == "MF_JF")
            {
                string _jfNo = "";
                string _usr;
                #region 判断是否锁单
                if (statementType != StatementType.Insert)
                {
                    if (statementType == StatementType.Delete)
                    {
                        _jfNo = dr["JF_NO", DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        _jfNo = dr["JF_NO"].ToString();
                    }
                    //判断是否锁单，如果已经锁单则不让修改。
                    Users _Users = new Users();
                    string _whereStr = "JF_NO = '" + _jfNo + "'";
                    if (_Users.IsLocked("MF_JF", _whereStr))
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                    }
                }
                #endregion

                if (statementType == StatementType.Delete)
                {
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                    _jfNo = dr["JF_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = dr["USR"].ToString();
                    _jfNo = dr["JF_NO"].ToString();
                }
                //检查资料正确否
                if (statementType != StatementType.Delete)
                {
                    //检查部门
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_usr, dr["DEP"].ToString()))
                    {
                        dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());//部门[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(dr["JF_CLS", DataRowVersion.Original].ToString()) && dr["JF_CLS", DataRowVersion.Original].ToString() != "0")
                    {
                        dr.SetColumnError("JF_CLS", "RCID=INV.HINT.CANNOTDEL");//积分已使用，不能删除！
                    }
                }
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //取得保存单号
                    dr["JF_NO"] = _sq.Set("JF", _usr, dr["DEP"].ToString(), DateTime.Today, "FX");
                    //写入默认栏位值
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                }
                else
                {
                    if (dr["CLS_ID", DataRowVersion.Original].ToString() == "T" && String.IsNullOrEmpty(dr["BIL_NO", DataRowVersion.Original].ToString()))
                    {
                        throw new SunlikeException("RCID=INV.HINT.HASEND");/*此单已做结案变更，请先删除结案变更单。*/
                    }
                }
                if (statementType == StatementType.Delete)
                {
                    string _error = _sq.Delete(dr["JF_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
                    }
                }


                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["JF_DD"]);
                //    _aps.BIL_ID = "JF";
                //    _aps.BIL_NO = dr["JF_NO"].ToString();
                //    _aps.BIL_TYPE = "";
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = "";
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = ""; //新加的部分，对应审核模板
                //}
                //else
                //    _aps = new AudParamStruct("JF", Convert.ToString(dr["JF_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
        }

        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dtMf = ds.Tables["MF_JF"];
            //if (_dtMf != null && _dtMf.Rows.Count > 0)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "JF");
            //}
            //#endregion
        }
        /// <summary>
        /// 取得积分单
        /// </summary>
        /// <param name="pgm">PGM</param>
        /// <param name="jfNo">积分单号</param>
        /// <param name="onlyFillSchema">是否只读取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm, string jfNo, bool onlyFillSchema)
        {
            DbJF _jf = new DbJF(Comp.Conn_DB);
            SunlikeDataSet _ds = _jf.GetData(jfNo, onlyFillSchema);
            if (_usr != null)
            {
                DataTable _dtMf = _ds.Tables["MF_JF"];
                if (_dtMf.Rows.Count > 0)
                {
                    string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                    System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _usr, _bill_Dep, _bill_Usr);
                    _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                }
            }
            //判断单据能否修改
            this.SetCanModify(_ds);

            return _ds;
        }

        /// <summary>
        /// 根据来源单信息取得积分单
        /// </summary>
        /// <param name="bilId">来源单据类别</param>
        /// <param name="bilNo">来源单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bilId, string bilNo)
        {
            DbJF _jf = new DbJF(Comp.Conn_DB);
            SunlikeDataSet _ds = _jf.GetData(bilId, bilNo);
            return _ds;
        }

        /// <summary>
        /// 保存结案标记
        /// </summary>
        /// <param name="clsId"></param>
        /// <param name="jfNo"></param>
        public void SaveClsID(string clsId, string jfNo)
        {
            DbJF _jf = new DbJF(Comp.Conn_DB);
            _jf.SaveClsID(clsId, jfNo);
        }

        /// <summary>
        /// 取得积分单
        /// </summary>
        /// <param name="cardNo">会员代号</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string cardNo)
        {
            DbJF _jf = new DbJF(Comp.Conn_DB);
            SunlikeDataSet _ds = _jf.GetData(cardNo);

            return _ds;
        }

        /// <summary>
        /// 更新积分单
        /// </summary>
        /// <param name="pgm">PGM</param>
        /// <param name="changedDS">积分单资料</param>
        /// <param name="bubbleException">是否抛出异常（true为直接抛出异常，false返回ErrorTable）</param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, SunlikeDataSet changedDS, bool bubbleException)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_JF"] = "JF_NO,JF_DD,CUS_NO,CARD_NO,END_DD,JF_NET,JF_CLS,USR,DEP,CHK_MAN,CLS_DATE,BIL_ID,BIL_NO,SYS_DATE,REM,BACK_ID,MOB_ID";
            //判断是否走审核流程
            string _usr;
            DataRow _dr = changedDS.Tables["MF_JF"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _usr = _dr["USR", DataRowVersion.Original].ToString();
            }
            else
            {
                _usr = _dr["USR"].ToString();
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
            //_isRunAuditing = _auditing.IsRunAuditing("JF", _usr, _bilType, _mobID);


            //更新资料
            this.UpdateDataSet(changedDS, _ht);
            //判断单据能否修改
            if (!changedDS.HasErrors)
            {
                //增加单据权限
                string _UpdUsr = "";
                if (changedDS.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changedDS.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    DataTable _dtMf = changedDS.Tables["MF_JF"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changedDS.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDS.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDS.ExtendedProperties["PRN"] = _billRight["PRN"];
                        changedDS.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
                this.SetCanModify(changedDS);
            }

            if (changedDS.HasErrors && bubbleException)
            {
                string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDS);
                throw new SunlikeException("RCID=JF.UpdateData() Error:;" + _errorMsg);
            }
            DataTable _dtError = BizObject.GetAllErrors(changedDS);
            return _dtError;
        }

        //判断单据能否修改
        private void SetCanModify(SunlikeDataSet ChangedDS)
        {
            if (ChangedDS.Tables["MF_JF"].Rows.Count > 0)
            {
                DataTable _dt = ChangedDS.Tables["MF_JF"];
                bool _canModify = true;
                if (_dt.Rows[0]["BIL_NO"].ToString() != "")
                {
                    _canModify = false;
                    ////Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CANNOTMODIFY");//存在来源单号,不能修改!
                }
                if (_dt.Rows[0]["CLS_ID"].ToString() == "T")	//已结案不能修改
                {
                    _canModify = false;
                    ////Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_MODIFY");//结案不能修改
                }
                //判断是否锁单
                if (_canModify && !String.IsNullOrEmpty(_dt.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _canModify = false;
                    ////Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_LOCK");//已锁单不能修改
                }
                ChangedDS.ExtendedProperties["CAN_MODIFY"] = _canModify.ToString().Substring(0, 1).ToUpper();
            }
        }

        #region 审核流程
        /// <summary>
        /// 审核同意
        /// </summary>
        /// <param name="bil_id">单据识别码</param>
        /// <param name="bil_no">单据号码</param>
        /// <param name="chk_man">审核人</param>
        /// <param name="cls_dd">审核日期</param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = "";
            try
            {
                //设定审核人
                DbJF _jf = new DbJF(Comp.Conn_DB);
                _jf.UpdateChkMan(bil_no, chk_man, cls_dd.ToString(Comp.SQLDateTimeFormat));
            }
            catch (Exception _ex)
            {
                _error = _ex.Message;
            }
            return _error;
        }

        /// <summary>
        /// 审核不同意
        /// </summary>
        /// <param name="bil_id">单据识别码</param>
        /// <param name="bil_no">单据号码</param>
        /// <param name="chk_man">审核人</param>
        /// <param name="cls_dd">审核日期</param>
        /// <returns></returns>
        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            return null;
        }

        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="bil_id">单据识别码</param>
        /// <param name="bil_no">单据号码</param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            string _error = "";
            try
            {
                //设定审核人
                DbJF _jf = new DbJF(Comp.Conn_DB);
                _jf.UpdateChkMan(bil_no, "", DateTime.Now.ToString(Comp.SQLDateTimeFormat));
            }
            catch (Exception _ex)
            {
                _error = _ex.Message;
            }
            return _error;
        }
        #endregion

        #region ICloseBill Members
        /// <summary>
        /// 反结案
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "CLS_ID")
            {
                DbJF _jf = new DbJF(Comp.Conn_DB);
                _error = _jf.CloseBill(bil_no, false);
            }
            return _error;
        }

        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "CLS_ID")
            {
                DbJF _jf = new DbJF(Comp.Conn_DB);
                _error = _jf.CloseBill(bil_no, true);
            }
            return _error;
        }
        #endregion
    }
}
