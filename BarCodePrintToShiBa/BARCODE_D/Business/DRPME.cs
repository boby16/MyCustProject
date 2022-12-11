using System;
using System.Collections;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;
namespace Sunlike.Business
{
    /// <summary>
    /// DRPME
    /// </summary>
    public class DRPME : BizObject, IAuditing
    {
        private bool _isRunAuditing;

        #region 取数据
        #region GetData
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="pgm">PGM</param>
        /// <param name="usr">登陆用户</param>
        /// <param name="meId">单据别</param>
        /// <param name="meNo">单据号码</param>
        /// <param name="onlyFillSchema">是否只取结构</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm, string usr, string meId, string meNo, bool onlyFillSchema)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpMe.GetData(meId, meNo, onlyFillSchema);

            DataTable _dtMfMe = _ds.Tables["MF_ME"];

            //增加单据权限
            if (!onlyFillSchema)
            {
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    DataTable _dtMf = _ds.Tables["MF_ME"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
            }

            //设定表身的KeyItm为自动递增
            DataTable _dtTfMe = _ds.Tables["TF_ME"];
            _dtTfMe.Columns["KEY_ITM"].AutoIncrement = true;
            _dtTfMe.Columns["KEY_ITM"].AutoIncrementSeed = _dtTfMe.Rows.Count > 0 ? Convert.ToInt32(_dtTfMe.Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _dtTfMe.Columns["KEY_ITM"].AutoIncrementStep = 1;
            _dtTfMe.Columns["KEY_ITM"].Unique = true;

            this.SetCanModify(_ds, true);

            //if (meId == "EJ")
            {
                _dtTfMe.Columns.Add(new DataColumn("AMTN_ORG", typeof(decimal)));
                if (meId == "EJ" && _dtMfMe.Rows.Count > 0)
                {
                    string _cusNo = _dtMfMe.Rows[0]["CUS_NO"].ToString();
                    //取客户可用资源
                    foreach (DataRow dr in _dtTfMe.Rows)
                    {
                        dr["AMTN_ORG"] = this.GetAmtnMe(dr["IDX_NO"].ToString(), _cusNo);
                    }
                }
            }

            return _ds;
        }
        #endregion
        #region 取得表身
        /// <summary>
        /// 取得表身
        /// </summary>
        /// <param name="meId"></param>
        /// <param name="meNo"></param>
        /// <param name="keyItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBodyData(string meId, string meNo, int keyItm)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            return _dbDrpMe.GetBodyData(meId, meNo, keyItm);
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
            DataTable _dtMf = ds.Tables["MF_ME"];
            DataTable _dtTf = ds.Tables["TF_ME"];
            string _errorMsg = "";
            bool _bCanModify = true;
            if (_dtMf.Rows.Count > 0)
            {
                //判断关帐日
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["ME_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                    _errorMsg += "COMMON.HINT.ACCCLOSE";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");/*已关账不能修改*/
                }
                //判断审核流程
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_dtMf.Rows[0]["ME_ID"].ToString(), _dtMf.Rows[0]["ME_NO"].ToString()))
                    {
                        _bCanModify = false;
                        _errorMsg += "COMMON.HINT.INTOAUT";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");/*已进入审核流程不能修改*/
                    }
                }
                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");/*锁单不能修改*/
                }
                //已转受定量
                if (_bCanModify)
                {
                    if (_dtTf.Select("ISNULL(QTY_SO, 0) > 0").Length > 0)
                    {
                        _bCanModify = false;
                        _errorMsg += "MON.HINT.QTY_SO";
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return _errorMsg;
        }
        #endregion

        #region 保存数据
        #region UpdateData
        /// <summary>
        /// UpdateData
        /// </summary>
        /// <param name="ChangedDS"></param>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public DataTable UpdateData(SunlikeDataSet ChangedDS, bool throwException)
        {
            ChangedDS.Tables["TF_ME_BILL"].TableName = "TF_ME1";

            string _meId, _usr;
            DataRow _dr = ChangedDS.Tables["MF_ME"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _meId = _dr["ME_ID", DataRowVersion.Original].ToString();
                _usr = _dr["USR", DataRowVersion.Original].ToString();
            }
            else
            {
                _meId = _dr["ME_ID"].ToString();
                _usr = _dr["USR"].ToString();
            }

            Hashtable _ht = new Hashtable();
            _ht["MF_ME"] = "ME_ID,ME_NO,ME_DD,CUS_NO,REM,DEP,USR,PS_ID,PS_NO,AMTN_BILL,CLS_DATE,CHK_MAN,SYS_DATE,BIL_TYPE,PRT_SW,PRT_USR,SAL_NO,FILE_ID,MOB_ID";
            _ht["TF_ME"] = "ME_ID,ME_NO,ITM,IDX_NO,PRD_NO,PRD_MARK,QTY,UP,AMTN_NET,AMTN_ME,AMTN_USE,KEY_ITM,REM";
            _ht["TF_ME1"] = "ME_ID,ME_NO,ME_ITM,ITM,PS_ID,PS_NO,PS_ITM";

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
            //_isRunAuditing = _auditing.IsRunAuditing(_meId, _usr, _bilType, _mobID);

            ChangedDS.Tables["TF_ME1"].TableName = "TF_ME_BILL";//modify lwh090926

            this.UpdateDataSet(ChangedDS, _ht);

            DataTable _dt = new DataTable();
            if (ChangedDS.HasErrors)
            {
                if (throwException)
                {
                    throw new Exception(GetErrorsString(ChangedDS));
                }
                else
                {
                    return _dt = GetAllErrors(ChangedDS);
                }
            }
            else
            {
                //增加单据权限
                string _UpdUsr = "";
                if (ChangedDS.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = ChangedDS.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "DRP" + _meId;
                    DataTable _dtMf = ChangedDS.Tables["MF_ME"];
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
                this.SetCanModify(ChangedDS, true);
                ChangedDS.AcceptChanges();
            }
           
            return _dt;
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
            Auditing _auditing = new Auditing();
            string _meNo = "", _meId = "", _usr = "";
            SunlikeDataSet _dsMe;
            if (statementType == StatementType.Delete)
            {
                _meId = dr["ME_ID", DataRowVersion.Original].ToString();
                _meNo = dr["ME_NO", DataRowVersion.Original].ToString();
            }
            else
            {
                _meId = dr["ME_ID"].ToString();
                _meNo = dr["ME_NO"].ToString();
            }
            if (statementType != StatementType.Insert)
            {
                if (_auditing.GetIfEnterAuditing(_meId, _meNo))//如果进去审核了就不能修改和新增删除
                {
                    //throw new Exception("进入审核的单据不能修改");
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");
                }
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "ME_ID = '" + _meId + "' AND ME_NO = '" + _meNo + "'";
                if (_Users.IsLocked("MF_ME", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            if (tableName == "MF_ME")
            {
                #region 表头
                if (statementType == StatementType.Delete)
                {
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = dr["USR"].ToString();
                }
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //取得保存单号
                    dr["ME_NO"] = _sq.Set(_meId, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["ME_DD"]), dr["BIL_TYPE"].ToString());
                    //写入默认栏位值
                    dr["PRT_SW"] = "N";
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                }
                else if (statementType == StatementType.Delete)
                {
                    string _error = _sq.Delete(dr["ME_NO", DataRowVersion.Original].ToString(), _usr);
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
                    }
                }

                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["ME_DD"]);
                //    _aps.BIL_ID = dr["ME_ID"].ToString();
                //    _aps.BIL_NO = dr["ME_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = "";
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //    _aps = new AudParamStruct(Convert.ToString(dr["ME_ID", DataRowVersion.Original]), Convert.ToString(dr["ME_NO", DataRowVersion.Original]));
                //_auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

                #region 新增关帐判断
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["ME_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["ME_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion
                #endregion


            }
            if (tableName == "TF_ME")
            {
                //如果是更新营销费用申请检查是否已转受订量，如果有就不让修改。
                if (statementType != StatementType.Insert && _meId == "EA")
                {
                    int _keyItm = 0;
                    if (statementType == StatementType.Delete)
                        _keyItm = Convert.ToInt32(dr["ITM", DataRowVersion.Original]);
                    else
                        _keyItm = Convert.ToInt32(dr["ITM"]);
                    _dsMe = GetBodyData(_meId, _meNo, _keyItm);
                    if (_dsMe.Tables["TF_ME"].Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(_dsMe.Tables["TF_ME"].Rows[0]["QTY_SO"].ToString()))
                        {
                            decimal _qty = Convert.ToDecimal(_dsMe.Tables["TF_ME"].Rows[0]["QTY_SO"]);
                            if (_qty > 0)
                            {
                                dr.SetColumnError("IDX_NO", ResourceCenter.ResourceManager["MON.HINT.QTY_SO"]);
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }

                }


                //检查基础资料
                if (statementType != StatementType.Delete)
                {
                    if (dr["ME_ID"].ToString() == "EA" || dr["ME_ID"].ToString() == "EJ")
                    {
                        //判断费用项目是否存在非集团项目且表头客户为空
                        if (!String.IsNullOrEmpty(GetCompIdxUse(dr["IDX_NO"].ToString(), " "))
                            && String.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_ME"].Rows[0]["CUS_NO"].ToString()))
                        {
                            dr.SetColumnError("IDX_NO", ResourceCenter.ResourceManager["MON.HINT.COMPIDX_NO"]);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
                if (!_isRunAuditing)
                {
                    if (tableName == "TF_ME")
                    {
                        // 更新可用费用
                        this.UpdateAmtnMe(dr, false);
                    }
                }
            }
        }
        #endregion
        #region BeforeDsSave
        protected override void BeforeDsSave(DataSet ds)
        {

            //#region 单据追踪
            //DataTable _dt = ds.Tables["MF_ME"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["ME_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["ME_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion


            base.BeforeDsSave(ds);
        }
        #endregion
        #region AfterUpdate
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
            //判断是否走审核流程
            if (!_isRunAuditing)
            {
                if (tableName == "TF_ME1")
                {
                    string _meid = "";
                    if (statementType == StatementType.Delete)
                        _meid = dr["ME_ID", DataRowVersion.Original].ToString();
                    else
                        _meid = dr["ME_ID"].ToString();
                    if (_meid == "EE")
                    {
                        //更新销货单
                        DRPSA _drpSa = new DRPSA();
                        if (statementType == StatementType.Delete)
                            _drpSa.UpdateMeRtn(dr["PS_ID", DataRowVersion.Original].ToString(), dr["PS_NO", DataRowVersion.Original].ToString(), Convert.ToInt32(dr["PS_ITM", DataRowVersion.Original]), false);
                        else
                            _drpSa.UpdateMeRtn(dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), Convert.ToInt32(dr["PS_ITM"]), true);
                    }

                }
            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        #endregion
        #endregion

        #region 审核流程
        #region 审核不同意
        /// <summary>
        /// 审核不同意
        /// </summary>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pCHK_MAN"></param>
        /// <param name="chk_DD"></param>
        /// <returns></returns>
        public string Deny(string pBB_ID, string pBB_NO, string pCHK_MAN, System.DateTime chk_DD)
        {
            return "";
        }
        #endregion
        #region 审核通过
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
                SunlikeDataSet _ds = this.GetData("DRPEE", chk_man, bil_id, bil_no, false);
                _ds.Tables["TF_ME_BILL"].TableName = "TF_ME1";
                DataRow _drHead = _ds.Tables["MF_ME"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_ME"];
                DataTable _dtBody1 = _ds.Tables["TF_ME1"];
                //修改可用费用
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    this.UpdateAmtnMe(_dtBody.Rows[i], true);
                }
                if (bil_id == "EE")
                {
                    //更新销货单
                    _dtBody1 = _dtBody1.DefaultView.ToTable(true, new string[] { "PS_ID", "PS_NO", "PS_ITM" });
                    DRPSA _drpSa = new DRPSA();
                    foreach (DataRow dr in _dtBody1.Rows)
                    {
                        _drpSa.UpdateMeRtn(dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), Convert.ToInt32(dr["PS_ITM"]), true);
                    }
                }
                //设定审核人
                DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
                _dbDrpMe.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }
        #endregion
        #region 审核错误回退
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
        #endregion
        #region 反审核
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
                SunlikeDataSet _ds = this.GetData("DRPEE", "", bil_id, bil_no, false);
                _ds.Tables["TF_ME_BILL"].TableName = "TF_ME1";
                DataRow _drHead = _ds.Tables["MF_ME"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_ME"];
                DataTable _dtBody1 = _ds.Tables["TF_ME1"];

                string _errorMsg = this.SetCanModify(_ds, false);
                if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                {
                    if (_errorMsg.Length > 0)
                    {
                        return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;RCID=" + _errorMsg;
                    }
                    else
                    {
                        return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";
                    }
                }
                if (bil_id == "EE")
                {
                    //更新销货单
                    _dtBody1 = _dtBody1.DefaultView.ToTable(true, new string[] { "PS_ID", "PS_NO", "PS_ITM" });
                    DRPSA _drpSa = new DRPSA();
                    foreach (DataRow dr in _dtBody1.Rows)
                    {
                        _drpSa.UpdateMeRtn(dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), Convert.ToInt32(dr["PS_ITM"]), false);
                    }
                }
                //修改可用费用
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    _dtBody.Rows[i].Delete();
                    this.UpdateAmtnMe(_dtBody.Rows[i], false);
                }
                DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
                //设定审核人
                if (isUpdateHead)
                {
                    _dbDrpMe.UpdateChkMan(bil_id, bil_no, "", DateTime.Now); ;
                }
                _drHead.Delete();
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }
        #endregion
        #endregion

        #region 更新可用费用
        /// <summary>
        /// 更新可用费用
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="ischk">审核通过</param>
        public void UpdateAmtnMe(DataRow dataRow, bool ischk)
        {
            string _cusNo = "", _oldCusNo = "", _psID = "", _idxNo = "", _oldIndxNo = "";
            if (dataRow.Table.DataSet.Tables["MF_ME"].Rows[0].RowState == DataRowState.Deleted)
            {
                _cusNo = dataRow.Table.DataSet.Tables["MF_ME"].Rows[0]["CUS_NO", DataRowVersion.Original].ToString();
                _oldCusNo = _cusNo;
                _psID = dataRow.Table.DataSet.Tables["MF_ME"].Rows[0]["PS_ID", DataRowVersion.Original].ToString();
            }
            else
            {
                _cusNo = dataRow.Table.DataSet.Tables["MF_ME"].Rows[0]["CUS_NO"].ToString();
                _psID = dataRow.Table.DataSet.Tables["MF_ME"].Rows[0]["PS_ID"].ToString();
                if (dataRow.Table.DataSet.Tables["MF_ME"].Rows[0].RowState == DataRowState.Modified)
                {
                    _oldCusNo = dataRow.Table.DataSet.Tables["MF_ME"].Rows[0]["CUS_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _oldCusNo = _cusNo;
                }
            }
            if (dataRow.RowState == DataRowState.Deleted)
            {
                _idxNo = dataRow["IDX_NO", DataRowVersion.Original].ToString();
                _oldIndxNo = _idxNo;
            }
            else
            {
                _idxNo = dataRow["IDX_NO"].ToString();
                if (dataRow.RowState == DataRowState.Modified)
                {
                    _oldIndxNo = dataRow["IDX_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _oldIndxNo = _idxNo;
                }
            }
            decimal _amtn = 0;
            decimal _oldAmtn = 0;
            if (dataRow.RowState == DataRowState.Added || ischk)
            {
                if (!String.IsNullOrEmpty(dataRow["AMTN_USE"].ToString()))
                {
                    _amtn = Convert.ToDecimal(dataRow["AMTN_USE"]);
                }
                if (dataRow["ME_ID"].ToString() == "EA")
                {
                    _amtn *= -1;
                }
            }
            else if (dataRow.RowState == DataRowState.Modified)
            {
                if (!String.IsNullOrEmpty(dataRow["AMTN_USE", DataRowVersion.Original].ToString()))
                {
                    _oldAmtn = Convert.ToDecimal(dataRow["AMTN_USE", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dataRow["AMTN_USE"].ToString()))
                {
                    _amtn = Convert.ToDecimal(dataRow["AMTN_USE"]);
                }
                if (_oldCusNo != _cusNo || _oldIndxNo != _idxNo)
                {
                    if (dataRow["ME_ID"].ToString() != "EA")
                    {
                        _oldAmtn *= -1;
                    }
                    else
                    {
                        _amtn *= -1;
                    }
                }
                else
                {
                    if (dataRow["ME_ID"].ToString() != "EA")
                    {
                        //如果是EJ或EE,公式为[当前值-原来值]
                        _amtn -= _oldAmtn;
                    }
                    else
                    {
                        //如果是EJ或EE,公式为[原来值-当前值]
                        _amtn = _oldAmtn - _amtn;
                    }
                }
            }
            else if (dataRow.RowState == DataRowState.Deleted)
            {
                if (!String.IsNullOrEmpty(dataRow["AMTN_USE", DataRowVersion.Original].ToString()))
                {
                    _amtn = Convert.ToDecimal(dataRow["AMTN_USE", DataRowVersion.Original]);
                }
                if (dataRow["ME_ID", DataRowVersion.Original].ToString() != "EA")
                {
                    _amtn *= -1;
                }
            }
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            if (_oldCusNo != _cusNo || _oldIndxNo != _idxNo)
            {
                //变更表头客户或表身费用项目的做法
                _dbDrpMe.UpdateAmtnMe(_oldIndxNo, GetCompIdxUse(_oldIndxNo, _oldCusNo), _oldAmtn);
            }
            _dbDrpMe.UpdateAmtnMe(_idxNo, GetCompIdxUse(_idxNo, _cusNo), _amtn);
        }
        #endregion

        #region 更新已交量
        /// <summary>
        /// 更新已交量
        /// </summary>
        /// <param name="meId"></param>
        /// <param name="meNo"></param>
        /// <param name="keyItm"></param>
        /// <param name="qtySo"></param>
        public void UpdateQtySo(string meId, string meNo, int keyItm, decimal qtySo)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            _dbDrpMe.UpdateQtySo(meId, meNo, keyItm, qtySo);
        }
        #endregion

        #region 营销费用申请（WEB）相关操作
        #region 删除表身
        /// <summary>
        /// 删除表身
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="itm"></param>
        public void DeleteBodyData(DataSet ds, int itm)
        {
            try
            {
                DataTable _tfDt = ds.Tables["TF_ME"];
                DataRow[] _aryDr = _tfDt.Select("ITM=" + itm.ToString());
                if (_aryDr.Length > 0)
                {
                    _aryDr[0].Delete();
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion
        #region 取得费用单号
        /// <summary>
        /// 取得费用单号
        /// </summary>
        /// <returns></returns>
        public string GetMeNo(string meId, string userId, DateTime dateTime)
        {
            string _meNo = "EE00000000";
            if (meId == "EA")
            {
                _meNo = "EA00000000";
            }
            else if (meId == "EJ")
            {
                _meNo = "EJ00000000";
            }
            try
            {
                SQNO _sqlno = new SQNO();
                Users _users = new Users();
                _meNo = _sqlno.Get(meId, userId, _users.GetUserDepNo(userId), dateTime, "FX");
            }
            catch { }
            return _meNo;
        }
        #endregion
        #region 删除单据
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="ds"></param>
        public DataTable Delete(SunlikeDataSet ds)
        {
            DataTable _mfDt = ds.Tables["MF_ME"];
            DataTable _tfDt = ds.Tables["TF_ME"];
            DataTable _tfDt1 = ds.Tables["TF_ME1"];

            //删除营销费用单表头
            foreach (DataRow dr in _mfDt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                    dr.Delete();
            }
            //营销费用单表身
            foreach (DataRow dr in _tfDt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                    dr.Delete();
            }
            //营销费用单表身1
            if (_tfDt1 != null)
            {
                foreach (DataRow dr in _tfDt1.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                        dr.Delete();
                }
            }
            return this.UpdateData(ds, false);
        }
        #endregion
        #region 表身添加单笔产品内容
        /// <summary>
        /// 表身添加单笔产品内容
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="meId"></param>
        /// <param name="meNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="idx_No"></param>
        /// <param name="up"></param>
        /// <param name="qty"></param>
        /// <param name="rem"></param>
        /// <param name="amtn_use"></param>
        /// <param name="amtn_net"></param>
        public void InsertBodyData(DataSet ds, string meId, string meNo, string prdNo, string prdMark, string idx_No, decimal up, decimal qty, string rem, decimal amtn_use, decimal amtn_net)
        {
            try
            {
                DataTable _tfDt = ds.Tables["TF_ME"];
                DataRow[] _aryDr = _tfDt.Select("PRD_NO='" + prdNo + "' AND PRD_MARK='" + prdMark + "' AND REM='" + rem + "' AND IDX_NO='" + idx_No + "' AND UP=" + up + "");
                //原来有资料，先累计数量然后更新。
                if (_aryDr.Length > 0)
                {
                    DataRow _dr = _aryDr[0];
                    decimal _sumQty = qty;
                    decimal _sumAmtnNet = amtn_net;
                    decimal _sumAmtnUse = amtn_use;
                    _sumQty += Convert.ToDecimal(_dr["QTY"]);
                    _sumAmtnNet += Convert.ToDecimal(_dr["AMTN_NET"]);
                    _sumAmtnUse += Convert.ToDecimal(_dr["AMTN_USE"]);
                    _dr["QTY"] = _sumQty;
                    _dr["AMTN_USE"] = _sumAmtnUse;
                    _dr["AMTN_NET"] = _sumAmtnNet;
                }
                else
                {
                    //插入表身TF_ME
                    DataRow _dr = _tfDt.NewRow();
                    _dr["ME_ID"] = meId;
                    _dr["ME_NO"] = meNo;
                    _dr["IDX_NO"] = idx_No;
                    _dr["ITM"] = GetMaxItm(_tfDt, "ITM");
                    _dr["PRD_NO"] = prdNo;
                    _dr["PRD_MARK"] = prdMark;
                    _dr["QTY"] = qty;
                    _dr["AMTN_USE"] = amtn_use;
                    _dr["UP"] = up;
                    _dr["AMTN_NET"] = amtn_net;
                    if (rem.Length > 25)
                    {
                        rem = Sunlike.Common.CommonVar.StringHelper.Substring(rem, 0, 50);
                    }
                    _dr["REM"] = rem;
                    _tfDt.Rows.Add(_dr);
                    //插入表身TF_ME1
                    DataTable _tfDt1 = ds.Tables["TF_ME_BILL"];
                    DataRow _dr1 = _tfDt1.NewRow();
                    _dr1["ME_ID"] = meId;
                    _dr1["ME_NO"] = meNo;
                    _dr1["ME_ITM"] = _dr["KEY_ITM"];
                    _dr1["ITM"] = ds.Tables["TF_ME_BILL"].Select("").Length > 0 ? Convert.ToInt32(ds.Tables["TF_ME_BILL"].Select("", "ITM DESC")[0]["ITM"]) + 1 : 1;
                    _tfDt1.Rows.Add(_dr1);
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion
        #region 取重复记录的总费用
        /// <summary>
        /// 取重复记录的总费用
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="idx_No"></param>
        /// <param name="amtn_use"></param>
        /// <param name="itm"></param>
        /// <returns></returns>
        public decimal GetAmtnUseCount(DataSet ds, string idx_No, decimal amtn_use, string itm)
        {
            DataTable _tfDt = ds.Tables["TF_ME"];
            string _filter = "IDX_NO='" + idx_No + "'";
            if (!String.IsNullOrEmpty(itm))
                _filter += " AND ITM <> " + itm + "";
            DataRow[] _aryDr = _tfDt.Select(_filter);
            if (_aryDr != null && _aryDr.Length > 0)
            {
                foreach (DataRow dr in _aryDr)
                {
                    amtn_use += Convert.ToDecimal(dr["AMTN_USE"]);
                }
            }
            return amtn_use;
        }
        #endregion
        #region 修改表身行
        /// <summary>
        /// 修改表身行
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="meId"></param>
        /// <param name="meNo"></param>
        /// <param name="itm"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="idx_no"></param>
        /// <param name="qty"></param>
        /// <param name="amtn_use"></param>
        /// <param name="up"></param>
        /// <param name="amtn_net"></param>
        /// <param name="rem"></param>
        public void UpdateBodyData(DataSet ds, string meId, string meNo, int itm, string prdNo, string prdMark, string idx_no, decimal qty, decimal amtn_use, decimal up, decimal amtn_net, string rem)
        {
            try
            {
                DataTable _tfDt = ds.Tables["TF_ME"];
                DataRow[] _aryDr = _tfDt.Select("ME_NO='" + meNo + "' AND ITM=" + itm);
                if (_aryDr.Length > 0)
                {
                    DataRow _dr = _aryDr[0];
                    _dr["ME_ID"] = meId;
                    _dr["ME_NO"] = meNo;
                    _dr["PRD_NO"] = prdNo;
                    _dr["IDX_NO"] = idx_no;
                    _dr["PRD_MARK"] = prdMark;
                    _dr["QTY"] = qty;
                    _dr["AMTN_USE"] = amtn_use;
                    _dr["AMTN_NET"] = amtn_net;
                    _dr["UP"] = up;
                    if (rem.Length > 25)
                    {
                        rem = Sunlike.Common.CommonVar.StringHelper.Substring(rem, 0, 50);
                    }
                    _dr["REM"] = rem;
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion
        #region 取得最大项次
        /// <summary>
        /// 取得最大项次
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="itmName"></param>
        /// <returns></returns>
        public int GetMaxItm(DataTable dt, string itmName)
        {
            int _itm = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (dr[itmName] != null)
                    {
                        if (Convert.ToInt32(dr[itmName]) > _itm)
                        {
                            _itm = Convert.ToInt32(dr[itmName]);
                        }
                    }
                }
            }
            return _itm + 1;
        }
        #endregion
        #region 表头
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="meId"></param>
        /// <param name="meNo"></param>
        /// <param name="meDd"></param>
        /// <param name="cusNo"></param>
        /// <param name="rem"></param>
        /// <param name="usr"></param>
        /// <param name="dep"></param>
        /// <param name="salNo"></param>
        public void SetHeaderData(DataSet ds, string meId, string meNo, DateTime meDd, string cusNo, string rem, string usr, string dep, string salNo)
        {
            SetHeaderData(ds, meId, meNo, meDd, cusNo, rem, usr, dep, salNo, "", "");
        }

        /// <summary>
        /// 修改表头
        /// </summary>
        /// <param name="ds">传入的DataSet</param>
        /// <param name="meId">单据识别</param>
        /// <param name="meNo">营销费用单号</param>
        /// <param name="meDd">单据日期</param>
        /// <param name="cusNo">客户代号</param>
        /// <param name="rem">备注</param>
        /// <param name="usr">填单人</param>
        /// <param name="dep">部门</param>
        /// <param name="salNo"></param>
        /// <param name="fileId">附件ID</param>
        /// <param name="mobid">mobid</param>
        public void SetHeaderData(DataSet ds, string meId, string meNo, DateTime meDd, string cusNo, string rem, string usr, string dep, string salNo, string fileId, string mobid)
        {
            try
            {
                Users _users = new Users();
                DataTable _mfDt = ds.Tables["MF_ME"];
                if (_mfDt.Rows.Count > 0)
                {
                    DataRow _dr = _mfDt.Rows[0];
                    _dr["ME_ID"] = meId;
                    _dr["ME_NO"] = meNo;
                    _dr["ME_DD"] = meDd;
                    _dr["CUS_NO"] = cusNo;
                    _dr["DEP"] = dep;
                    _dr["SAL_NO"] = salNo;
                    _dr["REM"] = rem;
                    _dr["FILE_ID"] = fileId;
                    _dr["MOB_ID"] = mobid;
                }
                else
                {
                    DataRow _dr = _mfDt.NewRow();
                    _dr["ME_ID"] = meId;
                    _dr["ME_NO"] = meNo;
                    _dr["ME_DD"] = meDd;
                    _dr["CUS_NO"] = cusNo;
                    _dr["USR"] = usr;
                    _dr["SAL_NO"] = salNo;
                    _dr["DEP"] = dep;
                    _dr["REM"] = rem;
                    _dr["SYS_DATE"] = System.DateTime.Now.ToShortDateString();
                    _dr["FILE_ID"] = fileId;
                    _dr["MOB_ID"] = mobid;
                    _mfDt.Rows.Add(_dr);
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion
        #endregion

        #region 取得集团项目用户
        /// <summary>
        /// 取得集团项目用户
        /// </summary>
        /// <param name="idxNo"></param>
        /// <param name="cusNo"></param>
        public string GetCompIdxUse(string idxNo, string cusNo)
        {
            Indx1 _indx1 = new Indx1();
            SunlikeDataSet _ds = _indx1.GetData(idxNo);
            if (_ds != null && _ds.Tables["INDX1"].Rows.Count > 0)
            {
                if (_ds.Tables["INDX1"].Rows[0]["CHK_COMP"].ToString() == "T")
                    return "";
            }
            return cusNo;
        }
        #endregion

        #region 取得可用资源
        #region 取得可用资源
        /// <summary>
        /// 取得可用资源
        /// </summary>
        /// <param name="idxNo"></param>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public decimal GetAmtnMe(string idxNo, string cusNo)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            return _dbDrpMe.GetAmtnMe(idxNo, cusNo);
        }
        #endregion
        #region 取得客户可用资源
        /// <summary>
        /// 取得客户可用资源
        /// </summary>
        /// <param name="cusNo">用户代号</param>
        /// <param name="usr">登录用户</param>
        /// <returns></returns>
        public SunlikeDataSet GetAmtnMeByCust(string usr, string cusNo)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            return _dbDrpMe.GetAmtnMeByCust(usr, cusNo);
        }
        #endregion
        #endregion

        #region  审核营销费用申请明细资料
        #region	取费用申请单明细资料
        /// <summary>
        ///	取费用申请单明细资料
        /// </summary>
        /// <param name="ME_Id">单据别</param>
        /// <param name="ME_No">单号</param>
        /// <returns>费用申请单明细资料</returns>
        public SunlikeDataSet GetTableEA(string ME_Id, string ME_No)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpMe.GetTableEA(ME_Id, ME_No);
            return _ds;
        }
        #endregion
        #region 审核修改费用申请数量
        /// <summary>
        /// 审核修改费用申请数量
        /// </summary>
        /// <param name="ME_Id"></param>
        /// <param name="ME_No"></param>
        /// <param name="Itm"></param>
        /// <param name="Qty"></param>
        /// <param name="AmtnUse"></param>
        public void UpdateEAQty(string ME_Id, string ME_No, int Itm, decimal Qty, decimal AmtnUse)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            _dbDrpMe.UpdateEAQty(ME_Id, ME_No, Itm, Qty, AmtnUse);
        }
        #endregion
        #region 审核删除费用申请明细
        /// <summary>
        /// 审核删除费用申请明细
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ME_Id"></param>
        /// <param name="ME_No"></param>
        /// <param name="Itm"></param>
        public void DelEa(string tableName, string ME_Id, string ME_No, int Itm)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            _dbDrpMe.DelEa(tableName, ME_Id, ME_No, Itm);
        }
        #endregion
        #endregion

        #region 检查单号是否存在
        /// <summary>
        /// 检查单号是否存在
        /// </summary>
        /// <param name="meNo">单号</param>
        /// <returns></returns>
        public bool IsExists(string meNo)
        {
            DbDRPME _dbDrpMe = new DbDRPME(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpMe.GetHeadData(meNo);
            if (_ds.Tables["MF_ME"].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 计算营销费用（用在销货相关单据自动转单）
        private DataSet dsCust, dsPrdt, dsArea, dsIndx, dsIndx1;
        private DateTime billDd;
        SunlikeDataSet dsResult = new SunlikeDataSet();
        /// <summary>
        ///UpdateDrpMe 
        /// </summary>
        /// <param name="dataSet"></param>
        public void UpdateDrpMe(DataSet dataSet)
        {
            DataTable _dtMf = dataSet.Tables["MF_PSS"];
            DataTable _dtTf = dataSet.Tables["TF_PSS"];
            if (_dtMf.Rows.Count > 0)
            {
                if (Comp.DRP_Prop["DRPPS_DRPME_" + _dtMf.Rows[0]["PS_ID"].ToString()].ToString() != "T")
                {
                    return;
                }
                billDd = Convert.ToDateTime(_dtMf.Rows[0]["PS_DD"]);
                this.CalData(_dtMf.Rows[0]["CUS_NO"].ToString(), _dtTf, true, true, true);
                if (dsResult.Tables["TF_ME"].Rows.Count > 0)
                {
                    DRPME _drpMe = new DRPME();
                    _drpMe.UpdateData(dsResult, true);
                    string _keyItmUpdate = ".";
                    foreach (DataRow dr in dsResult.Tables["TF_ME_BILL"].Rows)
                    {
                        if (_keyItmUpdate.IndexOf("." + dr["PS_ITM"].ToString() + ".") < 0)
                        {
                            DataRow[] _dra = _dtTf.Select(String.Format("PRE_ITM = {0}", dr["PS_ITM"].ToString()));
                            if (_dra.Length > 0)
                            {
                                _dra[0]["ME_FLAG"] = "T";
                            }
                            _keyItmUpdate += dr["PS_ITM"].ToString() + ".";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 计算营销费用
        /// </summary>
        /// <param name="cusNo">客户代号</param>
        /// <param name="dtBody">单据表身</param>
        /// <param name="isUpCust">客户含下属</param>
        /// <param name="isUpArea">区域含下属</param>
        /// <param name="isUpIdx">中类含下属</param>
        /// <returns></returns>
        private void CalData(string cusNo, DataTable dtBody, bool isUpCust, bool isUpArea, bool isUpIdx)
        {
            Me_Role _meRole = new Me_Role();
            DataTable dsRole = _meRole.GetData(String.Format("BDATE <= '{0}' AND (EDATE >= '{0}' OR EDATE IS NULL)", billDd.ToShortDateString())).Tables[0];

            DRPME _drpMe = new DRPME();
            dsResult = _drpMe.GetData("", "", "EE", "", true);
            DataRow _drHead = dsResult.Tables["MF_ME"].NewRow();
            _drHead["ME_ID"] = "EE";
            _drHead["ME_NO"] = "EE00000001";
            _drHead["ME_DD"] = dtBody.DataSet.Tables["MF_PSS"].Rows[0]["PS_DD"];
            _drHead["DEP"] = dtBody.DataSet.Tables["MF_PSS"].Rows[0]["DEP"];
            _drHead["CUS_NO"] = cusNo;
            _drHead["USR"] = dtBody.DataSet.Tables["MF_PSS"].Rows[0]["USR"];
            _drHead["PS_ID"] = dtBody.DataSet.Tables["MF_PSS"].Rows[0]["PS_ID"];
            _drHead["PS_NO"] = dtBody.DataSet.Tables["MF_PSS"].Rows[0]["PS_NO"];
            _drHead["REM"] = dtBody.DataSet.Tables["MF_PSS"].Rows[0]["REM"];
            dsResult.Tables["MF_ME"].Rows.Add(_drHead);

            string _sqlFilter = "";
            string _prdNo = "", _prdMark = "", _areaNo = "", _idxNo = "", _idx1 = "";
            decimal _amtnSum = 0;
            DataRow _drTemp;
            Cust _cust = new Cust();
            dsCust = new DataSet();
            dsCust.Merge(_cust.GetData(cusNo));
            if (dsCust != null)
            {
                _drTemp = dsCust.Tables[0].Rows.Find(cusNo);
                if (_drTemp != null)
                {
                    _areaNo = _drTemp["CUS_ARE"].ToString();
                }
            }
            dsCust.Dispose();
            GC.Collect();

            DataRow[] _draRole;
            Prdt _prdt = new Prdt();
            Area _area = new Area();
            PrdtIdx _prdtIdx = new PrdtIdx();
            Indx1 _indx1 = new Indx1();
            dsArea = _area.GetData("");
            dsIndx = new DataSet();
            dsIndx.Merge(_prdtIdx.GetData());
            dsIndx1 = _indx1.GetData_Indx1("");
            foreach (DataRow drBody in dtBody.Rows)
            {
                if (!String.IsNullOrEmpty(drBody["AMTN_NET"].ToString()))
                {
                    _amtnSum += Convert.ToDecimal(drBody["AMTN_NET"]);
                }
                if (!String.IsNullOrEmpty(drBody["TAX"].ToString()))
                {
                    _amtnSum += Convert.ToDecimal(drBody["TAX"]);
                }
                foreach (DataRow drIndx1 in dsIndx1.Tables[0].Select("ISNULL(CHK_SUM, 'F') <> 'T'"))
                {
                    _drTemp = null;
                    _idxNo = "";
                    _prdNo = drBody["PRD_NO"].ToString();
                    _prdMark = drBody["PRD_MARK"].ToString();
                    //费用项目
                    _idx1 = drIndx1["IDX1"].ToString();
                    dsPrdt = new DataSet();
                    dsPrdt.Merge(_prdt.GetPrdt(_prdNo));
                    if (dsPrdt != null)
                    {
                        _drTemp = dsPrdt.Tables[0].Rows.Find(_prdNo);
                        if (_drTemp != null)
                        {
                            _idxNo = _drTemp["IDX1"].ToString();
                        }
                    }
                    if (drIndx1["CHK_COMP"].ToString() == "T")//集团费用项目
                    {
                        if (!String.IsNullOrEmpty(_idxNo))
                        {
                            //中类
                            _sqlFilter = String.Format("IDX1 = '{0}' AND IDX_NO = '{1}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(CUS_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{2}' AND (EDATE >= '{2}' OR EDATE IS NULL)", _idx1, _idxNo, billDd.ToShortDateString());
                            _draRole = dsRole.Select(_sqlFilter);
                            if (_draRole.Length > 0)
                            {
                                this.FinishSearching(dsResult, drBody, _draRole[0]);
                                continue;
                            }
                            else
                            {
                                if (this.GetUpIdxRole(6, cusNo, _idx1, _areaNo, _idxNo, drBody, dsRole))
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            //品号
                            _sqlFilter = String.Format("IDX1 = '{0}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(CUS_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '{1}' AND ISNULL(PRD_MARK, '') = '{2}' AND BDATE <= '{3}' AND ((EDATE >= '{3}' OR EDATE IS NULL) OR EDATE IS NULL)", _idx1, _prdNo, _prdMark, billDd.ToShortDateString());
                            _draRole = dsRole.Select(_sqlFilter);
                            if (_draRole.Length > 0)
                            {
                                this.FinishSearching(dsResult, drBody, _draRole[0]);
                                continue;
                            }
                        }
                    }
                    else
                    {
                        //品号＋客户
                        _sqlFilter = String.Format("IDX1 = '{0}' AND CUS_NO = '{1}' AND PRD_NO = '{2}' AND PRD_MARK = '{3}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND BDATE <= '{4}' AND (EDATE >= '{4}' OR EDATE IS NULL)", _idx1, cusNo, _prdNo, _prdMark, billDd.ToShortDateString());
                        _draRole = dsRole.Select(_sqlFilter);
                        if (_draRole.Length > 0)
                        {
                            this.FinishSearching(dsResult, drBody, _draRole[0]);
                            continue;
                        }
                        else
                        {
                            //上层客户
                            if (this.GetUpCustRole(1, cusNo, _idx1, _areaNo, _idxNo, drBody, dsRole))
                            {
                                continue;
                            }
                        }

                        //品号+区域
                        if (!String.IsNullOrEmpty(_areaNo))
                        {
                            _sqlFilter = String.Format("IDX1 = '{0}' AND AREA_NO = '{1}' AND PRD_NO = '{2}' AND PRD_MARK = '{3}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND BDATE <= '{4}' AND (EDATE >= '{4}' OR EDATE IS NULL)", _idx1, _areaNo, _prdNo, _prdMark, billDd.ToShortDateString());
                            _draRole = dsRole.Select(_sqlFilter);
                            if (_draRole.Length > 0)
                            {
                                this.FinishSearching(dsResult, drBody, _draRole[0]);
                                continue;
                            }
                            else
                            {
                                //上层区域
                                if (this.GetUpAreaRole(2, cusNo, _idx1, _areaNo, _idxNo, drBody, dsRole))
                                {
                                    continue;
                                }
                            }
                        }

                        //品号
                        _sqlFilter = String.Format("IDX1 = '{0}' AND PRD_NO = '{1}' AND PRD_MARK = '{2}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(AREA_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND BDATE <= '{3}' AND (EDATE >= '{3}' OR EDATE IS NULL)", _idx1, _prdNo, _prdMark, billDd.ToShortDateString());
                        _draRole = dsRole.Select(_sqlFilter);
                        if (_draRole.Length > 0)
                        {
                            this.FinishSearching(dsResult, drBody, _draRole[0]);
                            continue;
                        }

                        //中类+客户
                        if (!String.IsNullOrEmpty(_idxNo))
                        {
                            _sqlFilter = String.Format("IDX1 = '{0}' AND CUS_NO = '{1}' AND IDX_NO = '{2}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{3}' AND (EDATE >= '{3}' OR EDATE IS NULL)", _idx1, cusNo, _idxNo, billDd.ToShortDateString());
                            _draRole = dsRole.Select(_sqlFilter);
                            if (_draRole.Length > 0)
                            {
                                this.FinishSearching(dsResult, drBody, _draRole[0]);
                                continue;
                            }
                            else
                            {
                                //上层客户
                                if (this.GetUpIdxRole(4, cusNo, _idx1, _areaNo, _idxNo, drBody, dsRole))
                                {
                                    continue;
                                }
                            }
                        }

                        //中类+区域
                        if (!String.IsNullOrEmpty(_idxNo) && !String.IsNullOrEmpty(_areaNo))
                        {
                            _sqlFilter = String.Format("IDX1 = '{0}' AND AREA_NO = '{1}' AND IDX_NO = '{2}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{3}' AND (EDATE >= '{3}' OR EDATE IS NULL)", _idx1, _areaNo, _idxNo, billDd.ToShortDateString());
                            _draRole = dsRole.Select(_sqlFilter);
                            if (_draRole.Length > 0)
                            {
                                this.FinishSearching(dsResult, drBody, _draRole[0]);
                                continue;
                            }
                            else
                            {
                                if (this.GetUpIdxRole(5, cusNo, _idx1, _areaNo, _idxNo, drBody, dsRole))
                                {
                                    continue;
                                }
                            }
                        }

                        //中类
                        if (!String.IsNullOrEmpty(_idxNo))
                        {
                            _sqlFilter = String.Format("IDX1 = '{0}' AND IDX_NO = '{1}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(CUS_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{2}' AND (EDATE >= '{2}' OR EDATE IS NULL)", _idx1, _idxNo, billDd.ToShortDateString());
                            _draRole = dsRole.Select(_sqlFilter);
                            if (_draRole.Length > 0)
                            {
                                this.FinishSearching(dsResult, drBody, _draRole[0]);
                                continue;
                            }
                            else
                            {
                                if (this.GetUpIdxRole(6, cusNo, _idx1, _areaNo, _idxNo, drBody, dsRole))
                                {
                                    continue;
                                }
                            }
                        }

                        //客户
                        _sqlFilter = String.Format("IDX1 = '{0}' AND CUS_NO = '{1}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{2}' AND (EDATE >= '{2}' OR EDATE IS NULL)", _idx1, cusNo, billDd.ToShortDateString());
                        _draRole = dsRole.Select(_sqlFilter);
                        if (_draRole.Length > 0)
                        {
                            this.FinishSearching(dsResult, drBody, _draRole[0]);
                            continue;
                        }
                        else
                        {
                            if (this.GetUpCustRole(7, cusNo, _idx1, _areaNo, _idxNo, drBody, dsRole))
                            {
                                continue;
                            }
                        }

                        //区域
                        if (!String.IsNullOrEmpty(_areaNo))
                        {
                            _sqlFilter = String.Format("IDX1 = '{0}' AND AREA_NO = '{1}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{2}' AND (EDATE >= '{2}' OR EDATE IS NULL)", _idx1, _areaNo, billDd.ToShortDateString());
                            _draRole = dsRole.Select(_sqlFilter);
                            if (_draRole.Length > 0)
                            {
                                this.FinishSearching(dsResult, drBody, _draRole[0]);
                                continue;
                            }
                            else
                            {
                                if (this.GetUpAreaRole(8, cusNo, _idx1, _areaNo, _idxNo, drBody, dsRole))
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    _sqlFilter = String.Format("IDX1 = '{0}' AND ISNULL(CUS_NO,'') = '' AND ISNULL(AREA_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{1}' AND (EDATE >= '{1}' OR EDATE IS NULL)", drIndx1["IDX1"].ToString(), billDd.ToShortDateString());
                    _draRole = dsRole.Select(_sqlFilter);
                    if (_draRole.Length > 0)
                    {
                        this.FinishSearching(dsResult, drBody, _draRole[0]);
                    }
                }
            }
            //统计项目
            foreach (DataRow drIndx1 in dsIndx1.Tables[0].Select("ISNULL(CHK_SUM, 'F') = 'T'"))
            {
                //客户
                _sqlFilter = String.Format("IDX1 = '{0}' AND CUS_NO = '{1}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{2}' AND (EDATE >= '{2}' OR EDATE IS NULL)", drIndx1["IDX1"].ToString(), cusNo, billDd.ToShortDateString());
                _draRole = dsRole.Select(_sqlFilter);
                if (_draRole.Length > 0)
                {
                    this.FinishSearching(dsResult, _amtnSum, _draRole[0], dtBody);
                    continue;
                }
                else
                {
                    //上层客户
                    if (this.GetUpCustRole(cusNo, drIndx1["IDX1"].ToString(), dsRole, dtBody, _amtnSum))
                    {
                        continue;
                    }
                }
                //区域
                if (!String.IsNullOrEmpty(_areaNo))
                {
                    _sqlFilter = String.Format("IDX1 = '{0}' AND AREA_NO = '{1}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{2}' AND (EDATE >= '{2}' OR EDATE IS NULL)", drIndx1["IDX1"].ToString(), _areaNo, billDd.ToShortDateString());
                    _draRole = dsRole.Select(_sqlFilter);
                    if (_draRole.Length > 0)
                    {
                        this.FinishSearching(dsResult, _amtnSum, _draRole[0], dtBody);
                        continue;
                    }
                    else
                    {
                        //上层区域
                        if (this.GetUpAreaRole(_areaNo, drIndx1["IDX1"].ToString(), dsRole, dtBody, _amtnSum))
                        {
                            continue;
                        }
                    }
                }
                _sqlFilter = String.Format("IDX1 = '{0}' AND ISNULL(CUS_NO,'') = '' AND ISNULL(AREA_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{1}' AND (EDATE >= '{1}' OR EDATE IS NULL)", drIndx1["IDX1"].ToString(), billDd.ToShortDateString());
                _draRole = dsRole.Select(_sqlFilter);
                if (_draRole.Length > 0)
                {
                    this.FinishSearching(dsResult, _amtnSum, _draRole[0], dtBody);
                }
            }
            //统计总资源数
            dsResult.Tables["MF_ME"].Rows[0]["AMTN_BILL"] = _amtnSum;
            decimal _amtn_use = 0;
            if (dsResult.Tables["TF_ME"].Rows.Count > 0)
            {
                foreach (DataRow dr in dsResult.Tables["TF_ME"].Rows)
                {
                    _amtn_use += Convert.ToDecimal(dr["AMTN_USE"]);
                }
            }
        }

        private bool GetUpIdxRole(int itm, string cusNo, string idx1, string areaNo, string idxNo, DataRow drBody, DataTable dtRole)
        {
            string _idxNoUp = this.GetUpIndx(idxNo);
            bool _isExist = false;
            if (!String.IsNullOrEmpty(_idxNoUp))
            {
                string _sqlFilter = "";
                DataRow[] _draRole;
                if (itm == 4)
                {
                    //中类+客户
                    _sqlFilter = String.Format("IDX1 = '{0}' AND CUS_NO = '{1}' AND IDX_NO = '{2}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{3}' AND (EDATE >= '{3}' OR EDATE IS NULL)", idx1, cusNo, _idxNoUp, billDd.ToShortDateString());
                    _draRole = dtRole.Select(_sqlFilter);
                    if (_draRole.Length > 0)
                    {
                        this.FinishSearching(dsResult, drBody, _draRole[0]);
                        _isExist = true;
                    }
                    else
                    {
                        _isExist = this.GetUpCustRole(itm, cusNo, idx1, areaNo, _idxNoUp, drBody, dtRole);
                    }
                }
                else if (itm == 5)
                {
                    //中类+区域
                    _sqlFilter = String.Format("IDX1 = '{0}' AND AREA_NO = '{1}' AND IDX_NO = '{2}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{3}' AND (EDATE >= '{3}' OR EDATE IS NULL)", idx1, areaNo, _idxNoUp, billDd.ToShortDateString());
                    _draRole = dtRole.Select(_sqlFilter);
                    if (_draRole.Length > 0)
                    {
                        this.FinishSearching(dsResult, drBody, _draRole[0]);
                        _isExist = true;
                    }
                    else
                    {
                        _isExist = this.GetUpAreaRole(itm, cusNo, idx1, areaNo, _idxNoUp, drBody, dtRole);
                    }
                }
                if (!_isExist)
                {
                    _isExist = this.GetUpIdxRole(itm, cusNo, idx1, areaNo, _idxNoUp, drBody, dtRole);
                }
            }
            return _isExist;
        }

        private bool GetUpCustRole(int itm, string cusNo, string idx1, string areaNo, string idxNo, DataRow drBody, DataTable dtRole)
        {
            string _cusNoUp = this.GetUpCust(cusNo);
            bool _isExist = false;
            if (!String.IsNullOrEmpty(_cusNoUp))
            {
                string _sqlFilter = "";
                string _prdNo, _prdMark;
                DataRow[] _draRole;
                if (itm == 1)
                {
                    //品号+客户
                    _prdNo = drBody["PRD_NO"].ToString();
                    _prdMark = drBody["PRD_MARK"].ToString();
                    _sqlFilter = String.Format("IDX1 = '{0}' AND CUS_NO = '{1}' AND PRD_NO = '{2}' AND PRD_MARK = '{3}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND BDATE <= '{4}' AND (EDATE >= '{4}' OR EDATE IS NULL)", idx1, _cusNoUp, _prdNo, _prdMark, billDd.ToShortDateString());
                    _draRole = dtRole.Select(_sqlFilter);
                    if (_draRole.Length > 0)
                    {
                        this.FinishSearching(dsResult, drBody, _draRole[0]);
                        _isExist = true;
                    }
                }
                else if (itm == 4)
                {
                    //中类+客户
                    _sqlFilter = String.Format("IDX1 = '{0}' AND CUS_NO = '{1}' AND IDX_NO = '{2}' AND ISNULL(AREA_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{3}' AND (EDATE >= '{3}' OR EDATE IS NULL)", idx1, _cusNoUp, idxNo, billDd.ToShortDateString());
                    _draRole = dtRole.Select(_sqlFilter);
                    if (_draRole.Length > 0)
                    {
                        this.FinishSearching(dsResult, drBody, _draRole[0]);
                        _isExist = true;
                    }
                }
                if (!_isExist)
                {
                    _isExist = this.GetUpCustRole(itm, _cusNoUp, idx1, areaNo, idxNo, drBody, dtRole);
                }
            }
            return _isExist;
        }

        private bool GetUpAreaRole(int itm, string cusNo, string idx1, string areaNo, string idxNo, DataRow drBody, DataTable dtRole)
        {
            string _areaNoUp = this.GetUpArea(areaNo);
            bool _isExist = false;
            if (!String.IsNullOrEmpty(_areaNoUp))
            {
                string _sqlFilter = "";
                string _prdNo, _prdMark;
                DataRow[] _draRole;
                if (itm == 2)
                {
                    //品号+区域
                    _prdNo = drBody["PRD_NO"].ToString();
                    _prdMark = drBody["PRD_MARK"].ToString();
                    _sqlFilter = String.Format("IDX1 = '{0}' AND CUS_NO = '{1}' AND AREA_NO = '{2}' AND PRD_NO = '{3}' AND PRD_MARK = '{4}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND BDATE <= '{5}' AND EDATE >= '{5}'", idx1, cusNo, _areaNoUp, _prdNo, _prdMark, billDd.ToShortDateString());
                    _draRole = dtRole.Select(_sqlFilter);
                    if (_draRole.Length > 0)
                    {
                        this.FinishSearching(dsResult, drBody, _draRole[0]);
                        _isExist = true;
                    }
                }
                else if (itm == 5)
                {
                    //中类+区域
                    _sqlFilter = String.Format("IDX1 = '{0}' AND AREA_NO = '{1}' AND IDX_NO = '{2}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{3}' AND (EDATE >= '{3}' OR EDATE IS NULL)", idx1, _areaNoUp, idxNo, billDd.ToShortDateString());
                    _draRole = dtRole.Select(_sqlFilter);
                    if (_draRole.Length > 0)
                    {
                        this.FinishSearching(dsResult, drBody, _draRole[0]);
                        _isExist = true;
                    }
                }
                if (!_isExist)
                {
                    _isExist = this.GetUpAreaRole(itm, cusNo, idx1, _areaNoUp, idxNo, drBody, dtRole);
                }
            }
            return _isExist;
        }

        private bool GetUpAreaRole(string areaNo, string idx1, DataTable dsRole, DataTable dtBody, decimal amtnSum)
        {
            string _areaNoUp = this.GetUpArea(areaNo);
            bool _isExist = false;
            if (!String.IsNullOrEmpty(_areaNoUp))
            {
                string _sqlFilter = "";
                DataRow[] _draRole;
                _sqlFilter = String.Format("IDX1 = '{0}' AND AREA_NO = '{1}' AND ISNULL(CUS_NO, '') = '' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{2}' AND (EDATE >= '{2}' OR EDATE IS NULL)", idx1, _areaNoUp, billDd.ToShortDateString());
                _draRole = dsRole.Select(_sqlFilter);
                if (_draRole.Length > 0)
                {
                    this.FinishSearching(dsResult, amtnSum, _draRole[0], dtBody);
                    _isExist = true;
                }
                if (!_isExist)
                {
                    _isExist = this.GetUpAreaRole(_areaNoUp, idx1, dsRole, dtBody, amtnSum);
                }
            }
            return _isExist;
        }

        private bool GetUpCustRole(string cusNo, string idx1, DataTable dsRole, DataTable dtBody, decimal amtnSum)
        {
            string _cusNoUp = this.GetUpCust(cusNo);
            bool _isExist = false;
            if (!String.IsNullOrEmpty(_cusNoUp))
            {
                string _sqlFilter = "";
                DataRow[] _draRole;
                _sqlFilter = String.Format("IDX1 = '{0}' AND AREA_NO = '' AND ISNULL(CUS_NO, '') = '{1}' AND ISNULL(IDX_NO, '') = '' AND ISNULL(PRD_NO, '') = '' AND ISNULL(PRD_MARK, '') = '' AND BDATE <= '{2}' AND (EDATE >= '{2}' OR EDATE IS NULL)", idx1, _cusNoUp, billDd.ToShortDateString());
                _draRole = dsRole.Select(_sqlFilter);
                if (_draRole.Length > 0)
                {
                    this.FinishSearching(dsResult, amtnSum, _draRole[0], dtBody);
                    _isExist = true;
                }
                if (!_isExist)
                {
                    _isExist = this.GetUpAreaRole(_cusNoUp, idx1, dsRole, dtBody, amtnSum);
                }
            }
            return _isExist;
        }

        private string GetUpCust(string cusNo)
        {
            if (dsCust != null)
            {
                DataRow _dr = dsCust.Tables[0].Rows.Find(cusNo);
                if (_dr != null)
                {
                    return _dr["MAS_CUS"].ToString();
                }
            }
            return "";
        }

        private string GetUpArea(string areaNo)
        {
            if (dsArea != null)
            {
                DataRow _dr = dsArea.Tables[0].Rows.Find(areaNo);
                if (_dr != null)
                {
                    return _dr["AREA_UP"].ToString();
                }
            }
            return "";
        }

        private string GetUpIndx(string idxNo)
        {
            if (dsIndx != null)
            {
                DataRow _dr = dsIndx.Tables[0].Rows.Find(idxNo);
                if (_dr != null)
                {
                    return _dr["IDX_UP"].ToString();
                }
            }
            return "";
        }

        private void FinishSearching(DataSet dsResult, DataRow drBody, DataRow drRole)
        {
            if (!String.IsNullOrEmpty(drBody["AMTN_NET"].ToString()) && Convert.ToDecimal(drBody["AMTN_NET"]) != 0)
            {
                decimal _amtn = Convert.ToDecimal(drBody["AMTN_NET"]);
                if (dsResult.Tables["MF_ME"].Rows[0]["PS_ID"].ToString() != "SA")
                {
                    _amtn *= -1;
                }
                if (!String.IsNullOrEmpty(drBody["TAX"].ToString()))
                {
                    _amtn += Convert.ToDecimal(drBody["TAX"]);
                }
                DataRow[] _drs = dsResult.Tables["TF_ME"].Select(String.Format("IDX_NO ='{0}'", drRole["IDX1"].ToString()));
                DataRow _dr;
                if (_drs.Length > 0)
                {
                    _dr = _drs[0];
                    _dr["AMTN_USE"] = _amtn * (Convert.ToDecimal(drRole["AMTN_ME"]) / 100) + Convert.ToDecimal(_dr["AMTN_USE"]);
                }
                else
                {
                    _dr = dsResult.Tables["TF_ME"].NewRow();
                    _dr["ME_ID"] = "EE";
                    _dr["ME_NO"] = "EE00000001";
                    _dr["ITM"] = dsResult.Tables["TF_ME"].Select("").Length > 0 ? Convert.ToInt32(dsResult.Tables["TF_ME"].Select("", "ITM DESC")[0]["ITM"]) + 1 : 1;
                    _dr["IDX_NO"] = drRole["IDX1"];
                    _dr["AMTN_USE"] = _amtn * (Convert.ToDecimal(drRole["AMTN_ME"]) / 100);
                    dsResult.Tables["TF_ME"].Rows.Add(_dr);
                }

                DataRow _dr1 = dsResult.Tables["TF_ME_BILL"].NewRow();
                _dr1["ME_ID"] = "EE";
                _dr1["ME_NO"] = "EE00000001";
                _dr1["ME_ITM"] = _dr["KEY_ITM"];
                _dr1["ITM"] = dsResult.Tables["TF_ME_BILL"].Select("").Length > 0 ? Convert.ToInt32(dsResult.Tables["TF_ME_BILL"].Select("", "ITM DESC")[0]["ITM"]) + 1 : 1;
                _dr1["PS_ID"] = drBody["PS_ID"];
                _dr1["PS_NO"] = drBody["PS_NO"];
                _dr1["PS_ITM"] = drBody["PRE_ITM"];
                dsResult.Tables["TF_ME_BILL"].Rows.Add(_dr1);
            }
        }

        private void FinishSearching(DataSet dsResult, decimal amt, DataRow drRole, DataTable dtBody)
        {
            if (amt != 0)
            {
                if (dsResult.Tables["MF_ME"].Rows[0]["PS_ID"].ToString() != "SA")
                {
                    amt *= -1;
                }
                DataRow _dr = dsResult.Tables["TF_ME"].NewRow();
                _dr["ME_ID"] = "EE";
                _dr["ME_NO"] = "EE00000001";
                _dr["ITM"] = dsResult.Tables["TF_ME"].Select("").Length > 0 ? Convert.ToInt32(dsResult.Tables["TF_ME"].Select("", "ITM DESC")[0]["ITM"]) + 1 : 1;
                _dr["IDX_NO"] = drRole["IDX1"];
                _dr["AMTN_USE"] = amt * (Convert.ToDecimal(drRole["AMTN_ME"]) / 100);
                dsResult.Tables["TF_ME"].Rows.Add(_dr);

                foreach (DataRow drBody in dtBody.Rows)
                {
                    DataRow _dr1 = dsResult.Tables["TF_ME_BILL"].NewRow();
                    _dr1["ME_ID"] = "EE";
                    _dr1["ME_NO"] = "EE00000001";
                    _dr1["ME_ITM"] = _dr["KEY_ITM"];
                    _dr1["ITM"] = dsResult.Tables["TF_ME_BILL"].Select("").Length > 0 ? Convert.ToInt32(dsResult.Tables["TF_ME_BILL"].Select("", "ITM DESC")[0]["ITM"]) + 1 : 1;
                    _dr1["PS_ID"] = drBody["PS_ID"];
                    _dr1["PS_NO"] = drBody["PS_NO"];
                    _dr1["PS_ITM"] = drBody["PRE_ITM"];
                    dsResult.Tables["TF_ME_BILL"].Rows.Add(_dr1);
                }
            }
        }
        #endregion
    }
}