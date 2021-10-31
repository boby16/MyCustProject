using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections;

namespace Sunlike.Business
{
    /// <summary>
    /// 批次调价
    /// </summary>
    public class DRPTT : Sunlike.Business.BizObject, IAuditing
    {
        private bool _isRunAuditing;		//是否走审核流程
        private bool isCreate;				//是否新增批次调价单
        Sunlike.Business.Auditing _auditing;        //审核状态

        #region 构造函数
        /// <summary>
        /// 批次调价
        /// </summary>
        public DRPTT()
        {
            _auditing = new Auditing();
        }
        #endregion

        #region GetData
        /// <summary>
        /// 得到批次调价表结构
        /// </summary>
        /// <returns></returns>
        public SunlikeDataSet GetData()
        {
            DbDRPTT _dbDrpTt = new DbDRPTT(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpTt.GetTT_Comp();
            return _ds;
        }
        /// <summary>
        /// 取批次调价单内容
        /// </summary>
        /// <param name="TtNo">批次调价单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string TtNo)
        {
            DbDRPTT _dbDrpTt = new DbDRPTT(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpTt.GetTT(TtNo);
            return _ds;
        }
        #endregion

        #region 生成/更新批次调价单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ds"></param>
        public DataTable UpdateTt(SunlikeDataSet Ds)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_TT"] = "TT_NO,TT_DD,SEL_ID,AREA_NO,CHK_MAN,CLS_DATE,USR,DEP,REM,CHG_TYPE,MANU_TJ";
            _ht["TF_TT"] = "TT_NO,ITM,PRD_NO,PRD_MARK,S_DD,E_DD,V_TYPE,VALUE";
            _ht["TF_TT1"] = "TT_NO,CUS_NO";
            this.UpdateDataSet(Ds, _ht);
            if (!Ds.HasErrors)
            {
                //增加单据权限
                string _UpdUsr = "";
                if (Ds.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = Ds.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "DRPTT";
                    DataTable _dtMf = Ds.Tables["MF_TT"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        Ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        Ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        Ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                       Ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
            }
            DataTable _dtErr = GetAllErrors(Ds);
            return _dtErr;
        }
        #endregion

        #region BeforeUpdate
        /// <summary>
        /// BeforeUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            #region 判断是否锁单
            string _ttNo = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _ttNo = dr["TT_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _ttNo = dr["TT_NO"].ToString();
                }
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "TT_NO = '" + _ttNo + "'";
                if (_Users.IsLocked("MF_TT", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            if (tableName == "MF_TT" && statementType != StatementType.Delete)
            {
                #region 重写批次调价单

                string _usr = dr["USR"].ToString();

                Sunlike.Business.SQNO _sqno = new SQNO();
                Sunlike.Business.Users _user = new Users();
                DateTime _dateTime = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateFormat));

                //判断是否走审核流程
                string bilType = "";
                if (dr.Table.Columns.Contains("BIL_TYPE"))
                {
                    if (dr.RowState == DataRowState.Deleted)
                        bilType = dr["BIL_TYPE", DataRowVersion.Original].ToString();
                    else
                        bilType = dr["BIL_TYPE"].ToString();
                }

                _isRunAuditing = _auditing.IsRunAuditing("TT", _usr, Convert.ToBoolean(dr.Table.DataSet.ExtendedProperties["IS_DTLC"]), dr.Table.DataSet.ExtendedProperties["SH_NO"].ToString());



                Area _area = new Area();
                //检查拨入部门
                if (dr["AREA_NO"].ToString() != String.Empty)
                {
                    if (!_area.IsExist(dr["AREA_NO"].ToString()))
                    {
                        dr.SetColumnError("AREA_NO", "RCID=COMMON.HINT.AREANOTEXIST,PARAM=" + dr["AREA_NO"].ToString());//区域[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
                if (statementType == StatementType.Insert)
                {
                    string _dep = _user.GetUserDepNo(_usr);
                    string _tt_No = _sqno.Set("TT", _usr, _dep, _dateTime, "FX");
                    if (statementType == StatementType.Insert)
                    {
                        dr["TT_NO"] = _tt_No;
                    }
                    dr["DEP"] = _dep;
                }
                if (statementType == StatementType.Delete)
                {
                    isCreate = false;
                }
                else
                {
                    isCreate = true;
                }
                #endregion
            }
            if (tableName == "TF_TT")
            {
                if (statementType != StatementType.Delete)
                {
                    Prdt _prdt = new Prdt();
                    if (dr.Table.DataSet.Tables.Contains("MF_TT"))
                    {
                        string _usr = dr.Table.DataSet.Tables["MF_TT"].Rows[0]["USR"].ToString();
                        //检查货品代号						
                        if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_TT"].Rows[0]["TT_DD"])))
                        {
                            dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + dr["PRD_NO"].ToString());//货品代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                        if (CaseInsensitiveComparer.Default.Compare(dr.Table.DataSet.Tables["MF_TT"].Rows[0]["SEL_ID"].ToString(), "C") == 0)
                        {
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
                                        if (dr.Table.Columns.Contains(_aryMark[i]))
                                        {
                                            string _fldName = _dtMark.Rows[i]["FLDNAME"].ToString();
                                            if (!_mark.IsExist(_fldName, dr["PRD_NO"].ToString(), _aryMark[i]))
                                            {
                                                dr.SetColumnError(_fldName, "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _aryMark[i].Trim());//货品特征[{0}]不存在
                                                status = UpdateStatus.SkipAllRemainingRows;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (tableName == "MF_TT" && statementType == StatementType.Delete)
            {

                SQNO _sq = new SQNO();
                string _error = _sq.Delete(dr["PS_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                if (!String.IsNullOrEmpty(_error))
                {
                    throw new SunlikeException("RCID=COMMON.HINT.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}

                }
            }
            if (tableName == "MF_TT")
            {
                //#region 审核关联
                //AudParamStruct _aps ;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["TT_DD"]);
                //    _aps.BIL_ID = "TT";
                //    _aps.BIL_NO = dr["TT_NO"].ToString();
                //    _aps.BIL_TYPE = "FX";
                //    _aps.CUS_NO = "";
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = "";
                //    _aps.USR = dr["USR"].ToString();
                //    //_aps.MOB_ID = ""; //新加的部分，对应审核模板
                //}
                //else
                //    //_aps = new AudParamStruct("TT", Convert.ToString(dr["TT_NO", DataRowVersion.Original]));

                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
        }

        #endregion

        #region BeforeDsSave
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dtMf = ds.Tables["MF_TT"];
            //if (_dtMf.Rows.Count > 0)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "TT");
            //}
            //#endregion
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
            if (isCreate)
            {
                if (tableName == "MF_TT")
                {
                    SunlikeDataSet _ds = new SunlikeDataSet();
                    _ds = SunlikeDataSet.ConvertTo(dr.Table.DataSet).Copy();

                    if (!_isRunAuditing)
                    {
                        #region 如果不走审核流程，则进行调价,但不生成调价单
                        Sunlike.Business.DRPTJ _drptj = new DRPTJ();
                        try
                        {
                            _drptj.UpdatePrice(_ds);
                        }
                        catch (Exception _ex)
                        {
                            dr.RowError = _ex.Message;
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        #endregion
                    }
                }
            }
        }
        #endregion

        #region 批次调价单终审
        /// <summary>
        /// 批次调价单终审
        /// </summary>
        /// <param name="tt_No">单号</param>
        /// <param name="chk_Man">审核人</param>
        /// <param name="chk_DD">审核时间</param>
        private void Auditing_TT(string tt_No, string chk_Man , DateTime chk_DD)
        {
            try
            {
                Sunlike.Business.Data.DbDRPTT _dbDrptt = new DbDRPTT(Comp.Conn_DB);
                _dbDrptt.Auditing_TT(tt_No, chk_Man, chk_DD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 取批次调价单表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usr">制单人</param>
        /// <param name="tt_No">单号</param>
        /// <param name="OnlyFillSchema">是否schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetDRPTT(string usr, string tt_No,bool OnlyFillSchema)
        {
            SunlikeDataSet _ds = null;
            try
            {
                Sunlike.Business.Data.DbDRPTT _dbDrptt = new DbDRPTT(Comp.Conn_DB);
                _ds = _dbDrptt.GetDRPTT(tt_No, OnlyFillSchema);
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    _ds.DecimalDigits = Comp.GetCompInfo(usr).DecimalDigitsInfo.System;
                }
                if (!OnlyFillSchema)
                {
                    if (usr != null && !String.IsNullOrEmpty(usr))
                    {
                        DataTable _dtMfTt = _ds.Tables["MF_TT"];
                        if (_dtMfTt.Rows.Count > 0)
                        {
                            string _bill_Dep = _dtMfTt.Rows[0]["DEP"].ToString();
                            string _bill_Usr = _dtMfTt.Rows[0]["USR"].ToString();
                            System.Collections.Hashtable _billRight = Users.GetBillRight("DRPTT", usr, _bill_Dep, _bill_Usr);
                            _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                            _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                            _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                            _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                        }
                        this.SetCanModify(_ds, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        #endregion

        #region 删除批次调价单
        /// <summary>
        /// 删除批次调价单
        /// </summary>
        /// <param name="tt_No">单号</param>
        /// <param name="Del">是否删除批次调价单</param>
        /// <param name="Del_P">是否恢复调价前价格</param>
        /// <param name="Usr">操作用户 </param>
        public string Delete(string tt_No, bool Del,bool Del_P,string Usr)
        {
            string _isVic = String.Empty;
            try
            {
                this.EnterTransaction();

                DRPTJ _tj = new DRPTJ();

                DataTable _dt = _tj.GetTJFromTT(tt_No);
                if (_dt.Rows.Count > 0)
                {
                    decimal _amtn = 0;
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        DataSet _dsTj = _tj.GetData(_dt.Rows[i]["TJ_NO"].ToString(), false);
                        if (_dsTj.Tables["TF_TJ"].Rows.Count > 0)
                        {
                            for (int j = 0; j < _dsTj.Tables["TF_TJ"].Rows.Count; j++)
                            {
                                if (_dsTj.Tables["TF_TJ"].Rows[j]["AMTN_NET"] != System.DBNull.Value)
                                {
                                    _amtn += Convert.ToDecimal(_dsTj.Tables["TF_TJ"].Rows[j]["AMTN_NET"]);
                                }
                            }
                        }
                    }
                }
                Sunlike.Business.Data.DbDRPTT _dbDrptt = new DbDRPTT(Comp.Conn_DB);
                _isVic = _dbDrptt.Delete(tt_No, Del, Del_P);
                if (_isVic == "T")
                {
                    //_auditing.DelBillWaitAuditing("DRP","TT",tt_No);
                }
                else
                {
                    this.SetAbort();
                }
            }
            catch (Exception ex)
            {
                this.SetAbort();
                throw ex;
            }
            finally
            {
                this.LeaveTransaction();
            }
            return _isVic;
        }
        #endregion

        #region IAuditing Members
        /// <summary>
        /// 审核不同意
        /// </summary>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pCHK_MAN"></param>
        /// <param name="chk_DD"></param>
        /// <returns></returns>
        public string Deny(string pBB_ID, string pBB_NO,string pCHK_MAN,System.DateTime chk_DD)
        {
            return "";
        }
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
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetDRPTT(null, bil_no, false);
                if (_ds.Tables["MF_TT"].Rows.Count > 0)
                {
                    System.DateTime _ttDate = Convert.ToDateTime(_ds.Tables["MF_TT"].Rows[0]["TT_DD"]);
                    System.DateTime _ttNow = System.DateTime.Now.Date;
                    if (_ttDate >= _ttNow)
                    {
                        this.EnterTransaction();
                        this.Auditing_TT(bil_no, chk_man, cls_dd);
                        //调整价格
                        DRPTJ _drpTj = new DRPTJ();
                        _drpTj.UpdatePrice(_ds);
                    }
                    else
                    {
                        _error = bil_no + "已过调价日期，无法调价";
                    }
                }
                else
                {
                    _error = bil_no + "已被删除，请检查";
                }
            }
            catch (Exception ex)
            {
                this.SetAbort();
                _error = ex.Message.ToString();
            }
            finally
            {
                this.LeaveTransaction();
            }
            return _error;
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
        /// 回滚处理
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no,bool canChangeDS)
        {
            string _error = String.Empty;
            try
            {
                Sunlike.Business.DRPTJ _drptj = new DRPTJ();
                if (!_drptj.GetAud_FlagForTT(bil_no))
                {
                    SunlikeDataSet _ds = this.GetDRPTT(null, bil_no, false);

                    if (_ds.Tables["MF_TT"].Rows.Count > 0 && _ds.Tables["TF_TT"].Rows.Count > 0)
                    {
                        string _bDate = _ds.Tables["TF_TT"].Rows[0]["S_DD"].ToString();
                        TimeSpan _span = System.DateTime.Now.Date - Convert.ToDateTime(_bDate);

                        if (_span.Days > 0)
                        {
                            return "反审核失败，无法反审，批次调价单已经生效";
                        }
                        else
                        {
                            try
                            {
                                this.EnterTransaction();
                                //恢复产品价格
                                this.Delete(bil_no, false, true, _ds.Tables["MF_TT"].Rows[0]["USR"].ToString());
                                if (canChangeDS)
                                {
                                    //更新审核人和终审时间
                                    _drptj.UpdateMfTt(bil_no);
                                }
                            }
                            catch (Exception _ex)
                            {
                                this.SetAbort();
                                return _ex.Message;
                            }
                            finally
                            {
                                this.LeaveTransaction();
                            }
                        }
                    }
                }
                else
                {
                    _error += "RCID=INV.HINT.AUD_FLAG_ALERT";
                }
            }
            catch (Exception ex)
            {
                _error += ex.Message.ToString();
            }
            return _error;
        }

        #endregion

        #region 审核批次调价
        /// <summary>
        /// GetTjData
        /// </summary>
        /// <param name="pTjNo"></param>
        /// <returns></returns>
        public DataTable GetTjData(string pTjNo)
        {
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            return _tt.GetTjData(pTjNo);
        }
        /// <summary>
        /// 得到稽核/待稽核记录
        /// </summary>
        /// <param name="Date1"></param>
        /// <param name="Date2"></param>
        /// <param name="pFlag"></param>
        /// <param name="CusB"></param>
        /// <param name="CusE"></param>
        /// <returns></returns>
        public DataTable GetTjData(DateTime Date1, DateTime Date2,bool pFlag,string CusB,string CusE)
        {
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            return _tt.GetTjData(Date1, Date2, pFlag, CusB, CusE);
        }
        /// <summary>
        /// 得到稽核/待稽核记录
        /// </summary>
        /// <param name="pFlag"></param>
        /// <param name="CusB"></param>
        /// <param name="CusE"></param>
        /// <returns></returns>
        public DataTable GetTjData(bool pFlag, string CusB,string CusE)
        {
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            return _tt.GetTjData(pFlag, CusB, CusE);
        }
        /// <summary>
        ///  反稽核
        /// </summary>
        /// <param name="pTjNo">待稽核单号集合</param>
        /// <param name="isAudit"></param>
        /// <param name="jhDate"></param>
        public int UnSetFlag(string pTjNo, string isAudit, string jhDate)
        {
            int _ok = 0;
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            try
            {
                this.EnterTransaction();
                _ok = _tt.UnSetFlag(pTjNo, isAudit, jhDate);
                if (_ok == 0)
                {
                    DRPTJ _tj = new DRPTJ();
                    SunlikeDataSet _ds = _tj.GetData(pTjNo, false);
                    if (_ds.Tables["MF_TJ"].Rows.Count > 0)
                    {
                        //更新信用额度
                        Arp _arp = new Arp();
                        Cust _cust = new Cust();
                        string _cusNo = _ds.Tables["MF_TJ"].Rows[0]["CUS_NO"].ToString();
                        DateTime _date = Convert.ToDateTime(_ds.Tables["MF_TJ"].Rows[0]["TJ_DD"]);
                        if (_cust.IsDrp_id(_cusNo))
                        {
                            decimal _amtn = 0;
                            for (int j = 0; j < _ds.Tables["TF_TJ"].Rows.Count; j++)
                            {
                                if (_ds.Tables["TF_TJ"].Rows[j]["AMTN_NET"] != System.DBNull.Value)
                                {
                                    _amtn += Convert.ToDecimal(_ds.Tables["TF_TJ"].Rows[j]["AMTN_NET"]);
                                }
                            }
                            _arp.UpdateSarp("1", _date.Year, _cusNo, _date.Month, "", "AMTN_INV", _amtn);
                        }
                    }
                }
            }
            catch
            {
                this.SetAbort();
            }
            finally
            {
                this.LeaveTransaction();
            }
            return _ok;
        }

        /// <summary>
        ///  稽核调价单
        /// </summary>
        /// <param name="pTjNo">待稽核单号集合</param>
        public void SetFlag(string pTjNo)
        {
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            string[] _tjAry = pTjNo.Split(new char[] { ',' });
            bool _isOk = true;
            if (_tjAry.Length > 0)
            {
                for (int i = 0; i < _tjAry.Length; i++)
                {
                    if (_tjAry[i] != String.Empty)
                    {
                        try
                        {
                            this.EnterTransaction();

                            _isOk = _tt.SetFlag(pTjNo);
                            if (_isOk)
                            {
                                DRPTJ _tj = new DRPTJ();
                                SunlikeDataSet _ds = _tj.GetData(pTjNo, false);
                                if (_ds.Tables["MF_TJ"].Rows.Count > 0)
                                {
                                    //更新信用额度
                                    Arp _arp = new Arp();
                                    Cust _cust = new Cust();
                                    string _cusNo = _ds.Tables["MF_TJ"].Rows[0]["CUS_NO"].ToString();
                                    DateTime _date = Convert.ToDateTime(_ds.Tables["MF_TJ"].Rows[0]["TJ_DD"]);
                                    if (_cust.IsDrp_id(_cusNo))
                                    {
                                        decimal _amtn = 0;
                                        for (int j = 0; j < _ds.Tables["TF_TJ"].Rows.Count; j++)
                                        {
                                            if (_ds.Tables["TF_TJ"].Rows[j]["AMTN_NET"] != System.DBNull.Value)
                                            {
                                                _amtn += Convert.ToDecimal(_ds.Tables["TF_TJ"].Rows[j]["AMTN_NET"]);
                                            }
                                        }
                                        _arp.UpdateSarp("1", _date.Year, _cusNo, _date.Month, "", "AMTN_INV", _amtn * -1);
                                    }
                                }
                            }
                        }
                        catch (Exception _ex)
                        {
                            this.SetAbort();
                        }
                        finally
                        {
                            this.LeaveTransaction();
                        }
                    }
                }
            }
        }


        #endregion

        #region SetCanModify
        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        private void SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            //可删除的批次调价单分为如下几种情况
            //1.关帐后不允许删除
            //2.走审核流程，但还没有开始审核
            //3.走审核流程，已经反审核到原始位置
            //4.不走审核流程，已经生成调价单并且已经调价，但调价单未稽核，并且调价日期为当日，可删除，各值恢复到原始状态
            //5.已审核，但没有生成调价单时，不允许删除(因为删除价格设定时，需以调价单为准，但此时调价单是不存在的)
            DataTable _dtMf = ds.Tables["MF_TT"];
            bool _bCanModify = true;
            if (_dtMf.Rows.Count > 0)
            {
                //终审后不允许修改
                if (_dtMf.Rows[0]["CHK_MAN"].ToString() != String.Empty)
                {
                    ds.ExtendedProperties["UPD"] = "N";
                }
                //1.关帐后不允许删除
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["TT_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                }
                if (_bCanModify)
                {
                    //2.走审核流程，但还没有开始审核
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing("TT", _dtMf.Rows[0]["TT_NO"].ToString()))
                    {
                        _bCanModify = false;
                    }
                }
                //判断是否锁单
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                }
                if (_bCanModify)
                {
                    //4.此批次调价单对应的调价单中已有稽核过的调价单
                    if (_dtMf.Rows[0]["TT_NO"].ToString() != "")
                    {
                        DataTable _dt = GetTjFromTT(_dtMf.Rows[0]["TT_NO"].ToString());
                        if (_dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < _dt.Rows.Count; i++)
                            {
                                if (_dt.Rows[i]["AUD_FLAG"].ToString() == "T")
                                {
                                    _bCanModify = false;
                                    break;
                                }
                                //4.如果调价日期小于当前日期，则不允许删除，因为这时候可能已经存在了调价后打的单据,
                                //因为目前的处理方式是调价的第二天才开始生效
                                if (_dt.Rows[i]["TJ_DD"].ToString() != String.Empty)
                                {
                                    if (System.DateTime.Now.Date > Convert.ToDateTime(_dt.Rows[i]["TJ_DD"]))
                                    {
                                        _bCanModify = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
        }
        #endregion

        #region 根据批次调价单取客户调价单内容
        /// <summary>
        /// 根据批次调价单取客户调价单内容
        /// </summary>
        /// <param name="ttNo">批次调价单号</param>
        /// <returns></returns>
        public DataTable GetTjFromTT(string ttNo)
        {
            DbDRPTT _dbTt = new DbDRPTT(Comp.Conn_DB);
            return _dbTt.GetTjFromTT(ttNo).Tables[0];
        }
        #endregion

        #region 调价完成后回写批次调价表
        /// <summary>
        /// 调价完成后回写批次调价表 
        /// </summary>
        /// <param name="TtNo">已经完成的批次调价单</param>
        /// <param name="Manu">是否手工调价</param>
        public void SetTjState(string TtNo, string Manu)
        {
            string _sql = "update mf_tt set TJ_CUS=NULL,MANU_TJ=NULL where tt_no='" + TtNo + "'";
            if (Manu == "T")
            {
                _sql = "update mf_tt set TJ_CUS=NULL,MANU_TJ='T' where tt_no='" + TtNo + "'";
            }
            Query _query = new Query();
            _query.DoSQLString(_sql);
        }
        #endregion

        #region 判断区域上层区域定价政策是否为含下属
        /// <summary>
        /// 判断区域上层区域定价政策是否为含下属
        /// </summary>
        /// <param name="AreaNo"></param>
        /// <param name="PrdNo"></param>
        /// <returns></returns>
        public bool IsAreaPriceIncludeUnder(string AreaNo, string PrdNo)
        {
            DbDRPTT _drpTt = new DbDRPTT(Comp.Conn_DB);
            Area _area = new Area();
            string _areaUp = _area.GetUp(AreaNo);
            if (!String.IsNullOrEmpty(_areaUp))
            {
                SunlikeDataSet _ds = _drpTt.GetAreaPriceIncludeUnder(_areaUp, PrdNo);
                if (_ds.Tables["PRDT_UPR4"].Rows.Count > 0)
                {
                    if (CaseInsensitiveComparer.Default.Compare(_ds.Tables["PRDT_UPR4"].Rows[0]["AREA_INCLUDE"].ToString(), "Y") == 0)
                    {
                        return true;
                    }
                    else
                    {
                        this.IsAreaPriceIncludeUnder(_areaUp, PrdNo);
                    }
                }
                else
                {
                    this.IsAreaPriceIncludeUnder(_areaUp, PrdNo);
                }
            }
            return false;
        }
        #endregion
    }
}
