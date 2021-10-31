using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Business.Data;
using Sunlike.Common.CommonVar;
using System.Collections;
using System.Data;
using Sunlike.Common.Utility;
namespace Sunlike.Business
{
    /// <summary>
    ///缴库单
    /// </summary>
    public class MRPMM : BizObject, IAuditing
    {
        private string _mmId = "";
        private string _loginUsr = "";
        private bool _isRunAuditing;
        /// <summary>
        /// 是否重新切制凭证
        /// </summary>
        private bool _reBuildVohNo = false;

        private StringBuilder _chkRtnErrorMsg = new StringBuilder();
        /// <summary>
        /// 
        /// </summary>
        public MRPMM()
        {

        }

        #region 取数据
        /// <summary>
        /// 取缴库单资料
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="mmId"></param>
        /// <param name="mmNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string usr, string mmId, string mmNo, bool onlyFillSchema)
        {
            return this.GetUpdateData("MRPMM", usr, mmId, mmNo, onlyFillSchema);
        }
        /// <summary>
        /// 取缴库单资料
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="mmId"></param>
        /// <param name="mmNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string pgm, string usr, string mmId, string mmNo, bool onlyFillSchema)
        {
            DbMRPMM _mm = new DbMRPMM(Comp.Conn_DB);
            SunlikeDataSet _ds = _mm.GetData(mmId, mmNo, onlyFillSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;

                DataTable _dtHead = _ds.Tables["MF_MM0"];
                if (_dtHead.Rows.Count > 0)
                {
                    string _pgm = "";
                    if (string.IsNullOrEmpty(pgm))
                    {
                        _pgm = "MRPMM";
                    }
                    else
                    {
                        _pgm = pgm;
                    }
                    string _bill_Dep = _dtHead.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _dtHead.Rows[0]["USR"].ToString();
                    System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                    _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                }
            }
            this.SetCanModify(_ds, true);
            return _ds;
        }

        #endregion

        #region 保存
        #region 保存缴库单
        /// <summary>
        /// 保存缴库单
        /// </summary>
        /// <param name="changedDs"></param>
        /// <param name="bubbleException"></param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, SunlikeDataSet changedDs, bool bubbleException)
        {
            DataTable _dtHead = changedDs.Tables["MF_MM0"];
            #region 取得单据的审核状态
            if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = _dtHead.Rows[0]["USR"].ToString();
                _mmId = _dtHead.Rows[0]["MM_ID"].ToString();
            }
            else
            {
                _loginUsr = _dtHead.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
                _mmId = _dtHead.Rows[0]["MM_ID", System.Data.DataRowVersion.Original].ToString();
            }
            Auditing _auditing = new Auditing();
            DataRow _dr = _dtHead.Rows[0];
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
            //_isRunAuditing = _auditing.IsRunAuditing(_mmId, _loginUsr, _bilType, _mobID);
            if (changedDs.ExtendedProperties.Contains("SAVE_ID"))
            {
                _isRunAuditing = string.Compare(changedDs.ExtendedProperties["SAVE_ID"].ToString(), "F") == 0;
            }

            #endregion
            //是否重建凭证号码
            if (changedDs.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", changedDs.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            if (_chkRtnErrorMsg.Length > 0)
                _chkRtnErrorMsg.Remove(0, _chkRtnErrorMsg.Length);
            System.Collections.Hashtable _ht = new Hashtable();
            _ht["MF_MM0"] = "MM_ID,MM_NO,MM_DD,BIL_TYPE,DEP,BIL_ID,BIL_NO,MO_NO,VOH_ID,VOH_NO,FJ_NUM,"
                        + " USR,USR_NO,CHK_MAN,LOCK_MAN,PRT_USR,SYS_DATE,CLS_DATE,MOB_ID,PRT_SW,CPY_SW,FIN_ID,REM";
            _ht["TF_MM0"] = "MM_ID,MM_NO,ITM,MM_DD,MO_NO,TW_NO,SO_NO,CUS_OS_NO,DEP,BAT_NO,PRD_NO,ID_NO,"
                        + " PRD_MARK,PRD_NAME,UNIT,WH,VALID_DD,FREE_ID,PRE_ITM,EST_ITM,QTY,QTY1,QTY_RTN,QTY_SA,AMTN_VAL,"
                        + " CST_MAKE,CST_PRD,CST_OUT,CST_MAN,CST,USED_TIME,TIME_CNT,CST_SMAKE,CST_SPRD,CST_SOUT,CST_SMAN,"
                        + " CST_STD,USED_STIME,TIME_SCNT,OLD_MM_ID,OLD_MM_NO,MM_ITM,CALC_ID,ZC_FLAG,CNTT_NO,CAS_NO,TASK_ID,"
                        + " RK_DD,DEP_RK,REM,ML_NO,BIL_ID,BIL_NO,BIL_ITM,QC_FLAG,UP_MAIN";
            _ht["TF_MM0_B"] = "MM_ID,MM_NO,MM_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
            this.UpdateDataSet(changedDs, _ht);
            if (!changedDs.HasErrors)
            {
                #region 未发生错误
                //增加单据权限
                string _UpdUsr = "";
                if (changedDs.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changedDs.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "";
                    //取得PGM
                    if (changedDs.ExtendedProperties.Contains("PGM"))
                    {
                        _pgm = changedDs.ExtendedProperties["PGM"].ToString();
                    }
                    if (string.IsNullOrEmpty(_pgm))
                    {
                        _pgm = "MRPMM";
                    }
                    if (!string.IsNullOrEmpty(pgm))
                    {
                        _pgm = pgm;
                    }
                    DataTable _dtMf = changedDs.Tables["MF_MM0"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changedDs.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDs.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDs.ExtendedProperties["PRN"] = _billRight["PRN"];
                        changedDs.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
                this.SetCanModify(changedDs, true);
                #endregion
            }
            else
            {
                #region 发生错误
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDs);
                    throw new SunlikeException(_errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changedDs);
                }
                #endregion
            }
            return Sunlike.Business.BizObject.GetAllErrors(changedDs);

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
            #region 判断是否锁单
            string _mmNo = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _mmNo = dr["MM_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _mmNo = dr["MM_NO"].ToString();
                }
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "MM_ID='" + _mmId + "' AND MM_NO = '" + _mmNo + "'";
                if (_Users.IsLocked("MF_MM0", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            if (tableName == "MF_MM0")
            {
                #region 关账
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["MM_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["MM_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion

                if (statementType != System.Data.StatementType.Delete)
                {
                    #region 计算表头信息是否正确
                    //部门（必填）
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_loginUsr, dr["DEP"].ToString(), Convert.ToDateTime(dr["MM_DD"])))
                    {
                        dr.SetColumnError("DEP",/*部门代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }

                    //经办人
                    if (!String.IsNullOrEmpty(dr["USR_NO"].ToString()))
                    {
                        //固品专案：不需检验，系统系统自动带出的员工代号
                        //Salm _salm = new Salm();
                        //if (_salm.IsExist(_loginUsr, dr["USR_NO"].ToString(), Convert.ToDateTime(dr["MM_DD"])) == false)
                        //{
                        //    dr.SetColumnError("USR_NO",/*经办人[{0}]不存在没有对其操作的权限,请检查*/"RCID=MTN.HINT.USR_NO_NOTEXIST,PARAM=" + dr["USR_NO"].ToString());
                        //    status = UpdateStatus.SkipAllRemainingRows;
                        //}
                    }
                    #endregion
                }

                if (statementType == StatementType.Insert)
                {
                    #region --生成单号

                    SQNO SunlikeSqNo = new SQNO();
                    DateTime _dtMmDd = System.DateTime.Now;
                    if (dr["MM_DD"] is System.DBNull)
                    {
                        _dtMmDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _dtMmDd = Convert.ToDateTime(dr["MM_DD"]);
                    }
                    string _mlNo = SunlikeSqNo.Set("MM", _loginUsr, dr["DEP"].ToString(), _dtMmDd, dr["BIL_TYPE"].ToString());
                    dr["MM_NO"] = _mlNo;
                    dr["MM_DD"] = _dtMmDd.ToString(Comp.SQLDateFormat);
                    #endregion

                    #region 缺省设置
                    dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    #endregion
                }

                //#region 审核关联
                //if (!dr.Table.DataSet.ExtendedProperties.Contains("SAVE_ID"))
                //{
                //    AudParamStruct _aps;
                //    if (statementType != StatementType.Delete)
                //    {
                //        _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //        _aps.BIL_ID = _mmId;
                //        _aps.BIL_NO = dr["MM_NO"].ToString();
                //        if (string.IsNullOrEmpty(dr["MM_DD"].ToString()))
                //            _aps.BIL_DD = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                //        else
                //            _aps.BIL_DD = Convert.ToDateTime(dr["MM_DD"].ToString());
                //        _aps.USR = _loginUsr;
                //        _aps.CUS_NO = "";
                //        _aps.DEP = dr["DEP"].ToString();
                //        _aps.SAL_NO = "";
                //        //_aps.MOB_ID = "";
                //    }
                //    else
                //        //_aps = new AudParamStruct(_mmId, Convert.ToString(dr["MM_NO", DataRowVersion.Original]));
                //    Auditing _auditing = new Auditing();
                //    string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //    if (!string.IsNullOrEmpty(_auditErr))
                //    {
                //        throw new SunlikeException(_auditErr);
                //    }
                //}
                //#endregion

                #region 产生凭证
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }
                #endregion
            }
            else if (tableName == "TF_MM0")
            {
                #region 缺省设置
                if (statementType == StatementType.Insert)
                {
                    if (string.IsNullOrEmpty(dr["UNIT"].ToString()))
                        dr["UNIT"] = "1";
                    if (dr.Table.DataSet.Tables["MF_MM0"].Rows.Count > 0)
                    {
                        dr["MM_DD"] = dr.Table.DataSet.Tables["MF_MM0"].Rows[0]["MM_DD"];
                    }
                    else
                    {
                        dr["MM_DD"] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                    }
                    if (dr["PRD_MARK"] == System.DBNull.Value)
                    {
                        dr["PRD_MARK"] = "";
                    }
                }
                #endregion

                #region 判断表身信息是否正确
                if (statementType != System.Data.StatementType.Delete)
                {
                    //仓库检测（必填）
                    WH _wh = new WH();
                    if (_wh.IsExist(_loginUsr, dr["WH"].ToString(), Convert.ToDateTime(dr["MM_DD"])) == false)
                    {
                        dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM=" + dr["WH"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    if (!string.IsNullOrEmpty(dr["MO_NO"].ToString()))
                    {
                        //由于缴库的时候可以没有制令单（固品特殊需求）

                        //维修单号检测（必填）
                        MRPMO _mo = new MRPMO();
                        if (!_mo.IsExists(dr["MO_NO"].ToString()) && string.IsNullOrEmpty(dr["TW_NO"].ToString()))
                        {
                            dr.SetColumnError("MO_NO",/*单号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.NOERROR,PARAM=" + dr["MO_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //产品检测（必填）
                    Prdt _prdt = new Prdt();
                    if (!_prdt.IsExist(_loginUsr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["MM_DD"])))
                    {
                        dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + dr["MRP_NO"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //特征
                    string _mark = dr["PRD_MARK"].ToString();
                    int _prdMod = _prdt.CheckPrdtMod(dr["PRD_NO"].ToString(), _mark);
                    if (_prdMod == 1)
                    {
                        dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _mark);//货品特征[{0}]不存在
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else if (_prdMod == 2)
                    {

                        PrdMark _prd_Mark = new PrdMark();
                        if (_prd_Mark.RunByPMark(_loginUsr))
                        {
                            string[] _prd_markAry = _prd_Mark.BreakPrdMark(_mark);
                            DataTable _markTable = _prd_Mark.GetSplitData("");
                            for (int i = 0; i < _markTable.Rows.Count; i++)
                            {
                                string _markName = _markTable.Rows[i]["FLDNAME"].ToString();
                                if (!_prd_Mark.IsExist(_markName, dr["PRD_NO"].ToString(), _prd_markAry[i]))
                                {
                                    dr.SetColumnError(dr.Table.Columns["PRD_MARK"],/*货品特征[{0}]不存在,请检查*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[i].Trim());
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }

                    #region //
                    ////配方号
                    //MRPBom _bom = new MRPBom();
                    //MTNWBom _wBom = new MTNWBom();
                    //bool _isExistsBom = false;
                    ////查找维修配方
                    //if (!string.IsNullOrEmpty(dr["ID_NO"].ToString()))
                    //{
                    //    if (_wBom.ChkExistsByBomNo(dr["ID_NO"].ToString()))
                    //    {
                    //        _isExistsBom = true;
                    //    }
                    //    if (!_isExistsBom)
                    //    {
                    //        //查找标准配方
                    //        if (_bom.IsExists(dr["ID_NO"].ToString()))
                    //        {
                    //            _isExistsBom = true;
                    //        }
                    //    }
                    //    if (!_isExistsBom)
                    //    {
                    //        dr.SetColumnError("ID_NO",/*配方号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.BOMERROR,PARAM=" + dr["ID_NO"].ToString());
                    //        status = UpdateStatus.SkipAllRemainingRows;
                    //    }
                    //}
                    #endregion

                    #region 检测完修数量不能小于0
                    if (string.IsNullOrEmpty(dr["QTY"].ToString())
                        || (!string.IsNullOrEmpty(dr["QTY"].ToString()) && Convert.ToDecimal(dr["QTY"].ToString()) <= 0))
                    {
                        dr.SetColumnError("QTY",/*缴库数量不能小于零,请检查*/"RCID=MTN.HINT.QTYMMERROR");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    #endregion
                }
                #endregion

                #region 表头的CHK_MAN发生变更,母单终审
                if (dr.Table.DataSet.ExtendedProperties.Contains("SAVE_ID"))
                {
                    if (string.Compare(Convert.ToString(dr.Table.DataSet.ExtendedProperties["SAVE_ID"]), "T") == 0)
                        UpdateWhQty(dr, false);
                }
                #endregion


            }
            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }
        #endregion

        #region BeforeDsSave
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dt = ds.Tables["MF_MM0"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["MM_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["MM_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion


            base.BeforeDsSave(ds);
        }
        #endregion

        #region AfterUpdate
        /// <summary>
        /// /
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            if (tableName == "MF_MM0")
            {

                #region 删除单号
                if (statementType == StatementType.Delete)
                {
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(dr["MM_NO", DataRowVersion.Original].ToString(), _loginUsr);//删除时在BILD中插入一笔数据
                }
                #endregion

            }
            else if (tableName == "TF_MM0")
            {
                string _bilId = "";
                if (statementType != StatementType.Delete)
                {
                    _bilId = dr["BIL_ID"].ToString();
                }
                else
                {
                    _bilId = dr["BIL_ID", DataRowVersion.Original].ToString();
                }
                
                #region 增加表头领料量
                bool _skipUpdateWh = false;
                if (dr.Table.DataSet.ExtendedProperties.Contains("SAVE_ID"))
                {
                    if (string.Compare(Convert.ToString(dr.Table.DataSet.ExtendedProperties["SAVE_ID"]), "T") == 0)
                        _skipUpdateWh = true;//BEFORUPDATE时处理
                }
                if (!_isRunAuditing && !_skipUpdateWh)
                {
                    if (statementType == StatementType.Insert)
                    {
                        UpdateWhQty(dr, false);
                        UpdateQtyFin(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        UpdateWhQty(dr, true);
                        UpdateWhQty(dr, false);
                        UpdateQtyFin(dr, true);
                        UpdateQtyFin(dr, false);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        UpdateWhQty(dr, true);
                        UpdateQtyFin(dr, true);
                    }
                }
                #endregion

                #region 回写检验单

                if (_bilId == "TP")
                {
                    if (statementType != StatementType.Delete)
                    {
                        UpdateQtyOkRtn(dr, false);
                    }
                    else
                    {
                        UpdateQtyOkRtn(dr, true);
                    }
                }

                #endregion

            }
            else if (tableName == "TF_MM0_B")
            {
                if (statementType == StatementType.Insert)
                {
                    //缴库时，把BAR_QC表中的STATUS字段值改为MM，说明该条码已缴库
                    UpdateBarQCMM(dr["BAR_CODE"].ToString(),dr["BOX_NO"].ToString(), "MM");
                }
            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        #endregion

        #region AfterDsSave
        protected override void AfterDsSave(DataSet ds)
        {
            if (!string.IsNullOrEmpty(_chkRtnErrorMsg.ToString()))
                throw new SunlikeException(_chkRtnErrorMsg.ToString());
            base.AfterDsSave(ds);
        }
        #endregion

        #region 修改库存量
        /// <summary>
        /// 修改库存量
        /// </summary>
        /// <param name="dr">TF_MM0行信息</param>
        /// <param name="isDel"></param>
        private void UpdateWhQty(DataRow dr, bool isDel)
        {
            string _batNo = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _validDd = "";
            string _unit = "";
            string _bilId = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
            decimal _cst = 0;

            if (isDel)
            {
                _bilId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    _qty += (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                if (!string.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                    _qty1 += (-1) * Convert.ToDecimal(dr["QTY1", DataRowVersion.Original].ToString());
                //制造费用
                if (!string.IsNullOrEmpty(dr["CST_MAKE", DataRowVersion.Original].ToString()))
                    _cst += (-1) * Convert.ToDecimal(dr["CST_MAKE", DataRowVersion.Original].ToString());
                //耗用物料
                if (!string.IsNullOrEmpty(dr["CST_PRD", DataRowVersion.Original].ToString()))
                    _cst += (-1) * Convert.ToDecimal(dr["CST_PRD", DataRowVersion.Original].ToString());
                //直接人工
                if (!string.IsNullOrEmpty(dr["CST_MAN", DataRowVersion.Original].ToString()))
                    _cst += (-1) * Convert.ToDecimal(dr["CST_MAN", DataRowVersion.Original].ToString());
                //拖工费用
                if (!string.IsNullOrEmpty(dr["CST_OUT", DataRowVersion.Original].ToString()))
                    _cst += (-1) * Convert.ToDecimal(dr["CST_OUT", DataRowVersion.Original].ToString());
                //直接材料
                if (!string.IsNullOrEmpty(dr["CST", DataRowVersion.Original].ToString()))
                    _cst += (-1) * Convert.ToDecimal(dr["CST", DataRowVersion.Original].ToString());
            }
            else
            {
                _bilId = dr["BIL_ID"].ToString();
                _prdNo = dr["PRD_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _batNo = dr["BAT_NO"].ToString();
                _whNo = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qty += Convert.ToDecimal(dr["QTY"].ToString());
                if (!string.IsNullOrEmpty(dr["QTY1"].ToString()))
                    _qty1 += Convert.ToDecimal(dr["QTY1"].ToString());
                //制造费用
                if (!string.IsNullOrEmpty(dr["CST_MAKE"].ToString()))
                    _cst += Convert.ToDecimal(dr["CST_MAKE"].ToString());
                //耗用物料
                if (!string.IsNullOrEmpty(dr["CST_PRD"].ToString()))
                    _cst += Convert.ToDecimal(dr["CST_PRD"].ToString());
                //直接人工
                if (!string.IsNullOrEmpty(dr["CST_MAN"].ToString()))
                    _cst += Convert.ToDecimal(dr["CST_MAN"].ToString());
                //拖工费用
                if (!string.IsNullOrEmpty(dr["CST_OUT"].ToString()))
                    _cst += Convert.ToDecimal(dr["CST_OUT"].ToString());
                //直接材料
                if (!string.IsNullOrEmpty(dr["CST"].ToString()))
                    _cst += Convert.ToDecimal(dr["CST"].ToString());
            }
            WH _wh = new WH();
            System.Collections.Hashtable _fields = new System.Collections.Hashtable();
            if (string.IsNullOrEmpty(_batNo))
            {
                _fields[WH.QtyTypes.QTY] = _qty;
                _fields[WH.QtyTypes.QTY1] = _qty1;
                _fields[WH.QtyTypes.AMT_CST] = _cst;
                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _fields);
            }
            else
            {
                _fields[WH.QtyTypes.QTY_IN] = _qty;
                _fields[WH.QtyTypes.QTY1_IN] = _qty1;
                _fields[WH.QtyTypes.CST] = _cst;
                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _fields);
            }
            if (_bilId == "TP")
            {
                //更新入库量
                _fields = new Hashtable();
                _fields[WH.QtyTypes.QTY_RK] = _qty * -1;
                if (string.IsNullOrEmpty(_batNo))
                {
                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _fields);
                }
                else
                {
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _fields);
                }
            }
            if (_bilId == "TB")
            {
                //更新在制量
                _fields = new Hashtable();
                _fields[WH.QtyTypes.QTY_ON_PRC] = _qty * -1;
                if (string.IsNullOrEmpty(_batNo))
                {
                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _fields);
                }
                else
                {
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _fields);
                }
            }
        }
        #endregion

        #region 修改制令单中已缴库量
        /// <summary>
        /// 修改制令单中已缴库量
        /// </summary>
        /// <param name="dr">TF_MM0行信息</param>
        /// <param name="isDel"></param>
        private void UpdateQtyFin(DataRow dr, bool isDel)
        {
            string _moNo = "";
            string _unit = "1";
            decimal _qty = 0;
            if (isDel)
            {
                _moNo = dr["MO_NO", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    _qty = (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
            }
            else
            {
                _moNo = dr["MO_NO"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qty = Convert.ToDecimal(dr["QTY"].ToString());
            }
            MRPMO _mo = new MRPMO();
            //修改已缴库量
            _mo.UpdateQtyFin(_moNo, _unit, _qty);
            //强制退货控制
            //DataTable _dtChkRtn = _mo.GetChkRtnInfo(_moNo);
            //if (_dtChkRtn.Rows.Count > 0)
            //{
            //    CompInfo _compInfo = Comp.GetCompInfo("");
            //    for (int i = 0; i < _dtChkRtn.Rows.Count; i++)
            //    {
            //        if (_chkRtnErrorMsg.Length > 0)
            //            _chkRtnErrorMsg.Append(";");
            //        _chkRtnErrorMsg.Append("RCID=MTN.HINT.CHECKRTN,PARAM=" + _dtChkRtn.Rows[i]["MO_NO"].ToString() + ",PARAM=" + _dtChkRtn.Rows[i]["PRE_ITM"].ToString() + ",PARAM=" + _dtChkRtn.Rows[i]["PRD_NAME"].ToString() + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", Convert.ToDecimal(_dtChkRtn.Rows[i]["QTY"])));
            //    }
            //}
        }
        #endregion

        #region 回写检验单的缴库单号及合格量
        /// <summary>
        /// 回写检验单的缴库单号及合格量
        /// </summary>
        /// <param name="dr">表身行</param>
        /// <param name="isDel"></param>
        private void UpdateQtyOkRtn(DataRow dr, bool isDel)
        {
            MRPTY _ty = new MRPTY();
            decimal _qtyOkRto = 0;
            if (isDel)
            {

                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qtyOkRto = Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                }
                _ty.UpdateMmNo(dr["BIL_ID", DataRowVersion.Original].ToString(), dr["BIL_NO", DataRowVersion.Original].ToString(), "", dr["MM_ID", DataRowVersion.Original].ToString(), dr["MM_NO", DataRowVersion.Original].ToString(), dr["UNIT", DataRowVersion.Original].ToString(), (-1) * _qtyOkRto);
            }
            else
            {
                if (dr.Table.Columns.Contains("BIL_ITM"))
                {
                    if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    {
                        _qtyOkRto = Convert.ToDecimal(dr["QTY"].ToString());
                    }
                    if (dr.RowState == DataRowState.Modified)
                    {
                        if (!string.IsNullOrEmpty(dr["BIL_ITM", DataRowVersion.Original].ToString()))
                        {
                            _ty.UpdateMmNo(dr["BIL_ID", DataRowVersion.Original].ToString(), dr["BIL_NO", DataRowVersion.Original].ToString(), dr["BIL_ITM", DataRowVersion.Original].ToString(), dr["MM_ID", DataRowVersion.Original].ToString(), dr["MM_NO", DataRowVersion.Original].ToString(), dr["UNIT", DataRowVersion.Original].ToString(), (-1) * _qtyOkRto);
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["BIL_ITM"].ToString()))
                    {
                        _ty.UpdateMmNo(dr["BIL_ID"].ToString(), dr["BIL_NO"].ToString(), dr["BIL_ITM"].ToString(), dr["MM_ID"].ToString(), dr["MM_NO"].ToString(), dr["UNIT"].ToString(), _qtyOkRto);
                    }
                }
            }
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
                string _mmId = dr["MM_ID"].ToString();
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
                        if (string.Compare("MM", _mmId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.MM;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetUpdateData("", _mmId, dr["MM_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _mmId, out _vohNoError);
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
                        if (string.Compare("MM", _mmId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.MM;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetUpdateData("", _mmId, dr["MM_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _mmId, out _vohNoError);
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
                string _mmId = dr["MM_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("MM", _mmId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.MM;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _mmId, out _vohNoError);
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
        /// <param name="mlNo"></param>
        /// <param name="mlNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string mlId, string mlNo, string vohNo)
        {
            DbMRPMM _ml = new DbMRPMM(Comp.Conn_DB);
            _ml.UpdateVohNo(mlId, mlNo, vohNo);
        }
        #endregion
        #endregion

        #region  检查单据是否可以修改
        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            DataTable _dtHead = ds.Tables["MF_MM0"];
            string errorMsg = "";
            bool _bCanModify = true;
            if (_dtHead.Rows.Count > 0)
            {
                //判断关帐日
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtHead.Rows[0]["MM_DD"]), _dtHead.Rows[0]["DEP"].ToString(), "CLS_MNU"))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.CLOSE_CLS";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                }
                //判断审核流程
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing("MM", _dtHead.Rows[0]["MM_NO"].ToString()))
                    {
                        _bCanModify = false;
                        errorMsg += "COMMON.HINT.INTOAUT";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtHead.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.CLOSE_LOCK";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
                if (!String.IsNullOrEmpty(_dtHead.Rows[0]["VOH_NO"].ToString()))
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
                        _updUsr = _dtHead.Rows[0]["USR"].ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_dtHead.Rows[0]["VOH_NO"].ToString(), _updUsr, ref _acNo);
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
                //判断是否有来源单号
                if (_bCanModify)
                {
                    if (!string.IsNullOrEmpty(_dtHead.Rows[0]["BIL_ID"].ToString()) && !string.IsNullOrEmpty(_dtHead.Rows[0]["BIL_NO"].ToString()))
                    {
                        if (string.Compare(_dtHead.Rows[0]["BIL_ID"].ToString(), "TR") == 0)
                        {
                            ds.ExtendedProperties["DEL"] = "N";
                            errorMsg += "MTN.HINT.HASBILNO";
                            //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASBILNO");
                        }
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return errorMsg;
        }
        #endregion

        #region 修改审核人
        /// <summary>
        /// 修改审核人
        /// </summary>
        /// <param name="bilId">单据别</param>
        /// <param name="bilNo">单号</param>
        /// <param name="chkMan">审核人</param>
        /// <param name="clsDd">审核日</param>
        /// <returns></returns>
        public bool UpdateChkMan(string bilId, string bilNo, string chkMan, DateTime clsDd)
        {
            DbMRPMM _mm = new DbMRPMM(Comp.Conn_DB);
            return _mm.UpdateChkMan(bilId, bilNo, chkMan, clsDd);
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
            SunlikeDataSet _dsMm = this.GetUpdateData(null, bil_id, bil_no, false);
            DataTable _dtHead = _dsMm.Tables["MF_MM0"];
            DataTable _dtBody = _dsMm.Tables["TF_MM0"];
            try
            {
                if (_dtHead.Rows.Count > 0)
                {
                    //更新凭证
                    string _vohNo = this.UpdateVohNo(_dtHead.Rows[0], StatementType.Insert);
                    this.UpdateVohNo(bil_id, bil_no, _vohNo);
                }
                this.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
                if (_dtBody.Rows.Count > 0)
                {
                    foreach (DataRow drBody in _dtBody.Rows)
                    {
                        UpdateWhQty(drBody, false);
                        UpdateQtyFin(drBody, false);
                    }
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
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            string _errmsg = "";
            SunlikeDataSet _dsMm = this.GetUpdateData(null, bil_id, bil_no, false);
            string _errorMsg = this.SetCanModify(_dsMm, false);
            if (_dsMm.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
            {
                if (_errorMsg.Length > 0)
                {
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;RCID=" + _errorMsg;
                }
                else
                {
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";
                }
                if (_dsMm.ExtendedProperties.ContainsKey("BILL_VOH_AC_CONTROL"))
                {
                    return "RCID=INV.HINT.BILL_VOH_CONTRL3,PARAM=" + _dsMm.ExtendedProperties["VOH_AC_NO"].ToString();
                }
            }
            DataTable _dtHead = _dsMm.Tables["MF_MM0"];
            DataTable _dtBody = _dsMm.Tables["TF_MM0"];
            try
            {
                if (_dtHead.Rows.Count > 0)
                {
                    _dtHead.Rows[0].Delete();
                    //更新凭证
                    this.UpdateVohNo(_dtHead.Rows[0], StatementType.Delete);
                    this.UpdateVohNo(bil_id, bil_no, "");
                }
                if (_dtBody.Rows.Count > 0)
                {
                    foreach (DataRow drBody in _dtBody.Rows)
                    {
                        UpdateWhQty(drBody, true);
                        UpdateQtyFin(drBody, true);
                    }
                }
                this.UpdateChkMan(bil_id, bil_no, "", Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat)));
            }
            catch (Exception _ex)
            {
                _errmsg = _ex.Message.ToString();
            }
            return _errmsg;
        }

        #endregion

        #region 修改序列号BAR_QC表资料
        /// <summary>
        /// 修改序列号BAR_QC表资料STATUS字段值
        /// 缴库时，把BAR_QC表中的STATUS字段值改为MM，说明该条码已缴库
        /// 撤销缴库时，把BAR_QC表中的STATUS字段值改为SW，说明该条码已送缴，处于待缴库状态
        /// </summary>
        /// <param name="_barNo">序列号</param>
        /// <param name="_boxNo">箱条码</param>
        /// <param name="_status">缴库时传入：MM；撤销缴库时传入：SW</param>
        private void UpdateBarQCMM(string _barNo,string _boxNo, string _status)
        {
            string _qc = "";
            if (_barNo.Replace("#", "").Length == 22)
                _qc = "1";
            else
                _qc = "A";
            if (_status == "MM")
            {
                //缴库时，把BAR_QC表中的STATUS字段值改为MM，说明该条码已缴库
                string _sql = "begin tran \n"
                            + " if (select count(*) from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC where BAR_NO='" + _barNo + "')=0 \n"
                            + "     INSERT INTO " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC (BAR_NO,QC,STATUS) VALUES ('" + _barNo + "','" + _qc + "','MM') \n"
                            + " else \n"
                            + "     UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET STATUS='MM' WHERE BAR_NO='" + _barNo + "' \n";
                if (_boxNo != "")
                {
                    _sql += " if (select count(*) from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT where BOX_NO='" + _boxNo + "')=0 \n"
                         + "     INSERT INTO " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT (BOX_NO,STATUS) VALUES ('" + _boxNo + "','MM') \n"
                         + " else \n"
                         + "     UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT SET STATUS='MM' WHERE BOX_NO='" + _boxNo + "' \n";
                }
                       _sql += " if @@error<>0 \n"
                            + "     rollback tran \n"
                            + " else \n"
                            + "     commit tran ";
                Query _query = new Query();
                _query.RunSql(_sql);
            }
            else if (_status == "SW")
            {
                //撤销缴库时，把BAR_QC表中的STATUS字段值改为SW，说明该条码已送缴，处于待缴库状态
                //撤销缴库时，序列号必须满足以下3点才能撤销缴库：
                //  1、在BAR_QC表中STATUS为MM
                //  2、在缴库单序列号表TF_MM0_B中不存在（即条码系统中撤销缴库之前先到sunlike中删除缴库单）
                //  3、在BAR_REC表中库位WH要为空
                string _sql = "begin tran \n"
                            + "if (exists(select BAR_NO from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC where BAR_NO='" + _barNo + "' and ISNULL([STATUS],'')='MM')"
                            + "     and not exists(select BAR_CODE from TF_MM0_B where BAR_CODE='" + _barNo + "')"
                            + "     and (select isnull(WH,'') from BAR_REC where BAR_NO='" + _barNo + "')='' ) \n"
                            + "begin \n"
                            + " if not exists(select BAR_NO from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC where BAR_NO='" + _barNo + "') \n"
                            + "     INSERT INTO " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC (BAR_NO,QC,STATUS) VALUES ('" + _barNo + "','" + _qc + "','SW') \n"
                            + " else \n "
                            + " begin \n"
                            + "     UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET STATUS='SW' WHERE BAR_NO='" + _barNo + "' \n"
                            + "     UPDATE BAR_REC SET STOP_ID='',UPDDATE='" + DateTime.Now.ToString(Comp.SQLDateTimeFormat) + "' WHERE BAR_NO='" + _barNo + "' \n"
                            + " end \n"
                            + "end \n";
                if (_boxNo != "")
                {
                    _sql += "if (exists(select BOX_NO from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT where BOX_NO='" + _boxNo + "' and ISNULL([STATUS],'')='MM')"
                            + "     and not exists(select BOX_NO from TF_MM0_B where BOX_NO='" + _boxNo + "')"
                            + "     and (select isnull(WH,'') from BAR_BOX where BOX_NO='" + _boxNo + "')='' ) \n"
                            + "begin \n"
                            + " if not exists(select BOX_NO from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT where BOX_NO='" + _boxNo + "') \n"
                            + "     INSERT INTO " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT (BOX_NO,STATUS) VALUES ('" + _boxNo + "','SW') \n"
                            + " else \n "
                            + " begin \n"
                            + "     UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_BOX_BAT SET STATUS='SW' WHERE BOX_NO='" + _boxNo + "' \n"
                            + "     UPDATE BAR_BOX SET STOP_ID='' WHERE BOX_NO='" + _boxNo + "' \n"
                            + " end \n"
                            + "end \n";
                }
                       _sql += " if @@error<>0 \n"
                            + "     rollback tran \n"
                            + " else \n"
                            + "     commit tran ";
                Query _query = new Query();
                _query.RunSql(_sql);
            }
        }
        #endregion

        /// <summary>
        /// 取缴库单资料
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            DbMRPMM _mm = new DbMRPMM(Comp.Conn_DB);
            return _mm.GetData(sqlWhere);
        }

    }
}
