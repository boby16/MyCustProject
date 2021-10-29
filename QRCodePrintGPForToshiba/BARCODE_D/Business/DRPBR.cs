using System;
using System.Data;
using System.Collections;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business
{
    /// <summary>
    /// Summary description for DRPBR.
    /// </summary>
    public class DRPBR : BizObject, Sunlike.Business.IAuditing
    {
        private bool _isRunAuditing;
        private bool m_UpdatedBarCode;

        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public DRPBR()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string brNo, bool OnlyFillSchema)
        {
            DbDRPBR _dbDrp = new DbDRPBR(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrp.GetData(brNo, OnlyFillSchema);
            //增加单据权限
            if (!OnlyFillSchema)
            {
                string _pgm = "BARCHANGE";
                DataTable _dtMf = _ds.Tables["MF_BAR"];
                if (_dtMf.Rows.Count > 0)
                {
                    string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                    System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _bill_Dep, _bill_Usr);
                    _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                }
            }
            //判断单据能否修改
            this.SetCanModify(_ds, true);
            //创建存放序列号的暂存表
            DataTable _dt = new DataTable("BAR_COLLECT");
            _dt.Columns.Add("ITEM", typeof(int));
            _dt.Columns.Add("BAR_CODE");
            _dt.Columns.Add("BOX_NO");
            _dt.Columns.Add("PRD_NO");
            _dt.Columns.Add("PRD_MARK");
            _dt.Columns.Add("SERIAL_NO");
            _dt.Columns.Add("BAT_NO");
            _dt.Columns.Add("WH1");
            //序列号库位和批号
            _dt.Columns.Add("ISEXIST");
            _dt.Columns.Add("WH_REC");
            _dt.Columns.Add("BAT_NO_REC");
            _dt.Columns.Add("STOP_ID");
            _dt.Columns.Add("PH_FLAG");
            _dt.Columns.Add("SPC_NO");
            DataColumn[] _dca = new DataColumn[1];
            _dca[0] = _dt.Columns["ITEM"];
            _dt.PrimaryKey = _dca;
            _ds.Tables.Add(_dt);

            //写BAR_COLLECT表
            if (_ds.Tables["TF_BAR"].Rows.Count > 0)
            {
                DataView _dv = new DataView(_ds.Tables["TF_BAR"]);
                _dv.Sort = "BOX_NO,BAR_CODE";
                for (int i = 0; i < _dv.Count; i++)
                {
                    string _barCode = _dv[i]["BAR_CODE"].ToString();
                    DataRow _dr = _dt.NewRow();
                    _dr["ITEM"] = _dt.Rows.Count + 1;
                    _dr["BAR_CODE"] = _barCode;
                    if (!String.IsNullOrEmpty(_dv[i]["BOX_NO"].ToString()))
                    {
                        _dr["BOX_NO"] = _dv[i]["BOX_NO"];
                    }
                    BarCode.BarInfo _barInfo = BarCode.GetBarInfo(_barCode);
                    _dr["PRD_NO"] = _barInfo.Prd_No;
                    _dr["PRD_MARK"] = _barInfo.Prd_Mark;
                    _dr["SERIAL_NO"] = _barInfo.Serial_No;
                    _dt.Rows.Add(_dr);
                }
                _dv.Dispose();
                GC.Collect(GC.GetGeneration(_dv));
            }

            return _ds;
        }
        #endregion

        #region update
        /// <summary>
        /// 保存单据资料
        /// </summary>
        /// <param name="ChangedDS"></param>
        public void UpdateData(SunlikeDataSet ChangedDS)
        {
            Hashtable _ht = new Hashtable();
            _ht["MF_BAR"] = "BR_NO,BR_DD,DEP,SAL_NO,USR,CHK_MAN,CLS_DATE,PRT_SW,PRT_USR,REM,REF_NO";
            _ht["TF_BAR"] = "BR_NO,ITM,BAR_CODE,BOX_NO,PRD_NO,PRD_MARK,WH_OLD,WH_NEW,CLS_OLD,CLS_NEW,SPC_OLD,SPC_NEW,REM,BAT_NO_OLD,BAT_NO_NEW,PH_FLAG_OLD,PH_FLAG_NEW";
            //判断是否走审核流程
            string _usr = "";
            DataRow _dr = ChangedDS.Tables["MF_BAR"].Rows[0];
            if (_dr.RowState != DataRowState.Deleted)
            {
                _usr = _dr["USR"].ToString();
            }
            else
            {
                _usr = _dr["USR", DataRowVersion.Original].ToString();
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
            //_isRunAuditing = _auditing.IsRunAuditing("BR", _usr, _bilType, _mobID);


            this.UpdateDataSet(ChangedDS, _ht);
            //判断单据能否修改
            if (!ChangedDS.HasErrors)
            {
                //增加单据权限
                string _UpdUsr = "";
                if (ChangedDS.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = ChangedDS.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "BARCHANGE";
                    DataTable _dtMf = ChangedDS.Tables["MF_BAR"];
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
            }
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
            #region 判断是否锁单
            string _brNo = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _brNo = dr["BR_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _brNo = dr["BR_NO"].ToString();
                }
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "BR_NO = '" + _brNo + "'";
                if (_Users.IsLocked("MF_BAR", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            if (tableName == "MF_BAR")
            {
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //取得保存单号
                    dr["BR_NO"] = _sq.Set("BR", dr["USR"].ToString(), dr["DEP"].ToString(), Convert.ToDateTime(dr["BR_DD"]), "FX");
                    dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                }
                if (statementType == StatementType.Delete)
                {
                    string _error = _sq.Delete(dr["BR_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException("RCID=COMMON.COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：
                    }
                }
                else
                {
                    //检查业务员					
                    if (!String.IsNullOrEmpty(dr["SAL_NO"].ToString()))
                    {
                        Salm _salm = new Salm();
                        if (!_salm.IsExist(dr["USR"].ToString(), dr["SAL_NO"].ToString(), Convert.ToDateTime(dr["BR_DD"])))
                        {
                            dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SALMNOTEXIST");//业务员不存在！
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //检查部门
                    if (!String.IsNullOrEmpty(dr["DEP"].ToString()))
                    {
                        Dept _dept = new Dept();
                        if (!_dept.IsExist(dr["USR"].ToString(), dr["DEP"].ToString(), Convert.ToDateTime(dr["BR_DD"])))
                        {
                            dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEPNOTEXIST");//拨出部门不存在！
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
                //判断是否走审核流程
                if (_isRunAuditing && dr.RowState == DataRowState.Deleted)
                {
                    Auditing _auditing = new Auditing();
                    _auditing.DelBillWaitAuditing("DRP", "BR", dr["BR_NO", DataRowVersion.Original].ToString());
                }
                if (!_isRunAuditing && dr.RowState != DataRowState.Deleted)
                {
                    dr["CHK_MAN"] = dr["USR"].ToString();
                    dr["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                }
            }
            else if (tableName == "TF_BAR")
            {
                if (statementType != StatementType.Delete)
                {
                    string _usr = dr.Table.DataSet.Tables["MF_BAR"].Rows[0]["USR"].ToString();
                    //检查库位
                    WH _wh = new WH();
                    if (!String.IsNullOrEmpty(dr["WH_NEW"].ToString().Trim()))
                    {
                        if (!_wh.IsExist(_usr, dr["WH_NEW"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_BAR"].Rows[0]["BR_DD"])))
                        {
                            dr.SetColumnError("WH_NEW", "库位[" + dr["WH_NEW"].ToString() + "]不存在或没有对其操作的权限，请检查");//库位[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //检查规格
                    BarCode _barCode = new BarCode();
                    if (!String.IsNullOrEmpty(dr["SPC_NEW"].ToString().Trim()))
                    {
                        if (_barCode.IsSpcExist(dr["SPC_NEW"].ToString()))
                        {
                            dr.SetColumnError("SPC_NEW", "规格[" + dr["SPC_NEW"].ToString() + "]不存在或没有对其操作的权限，请检查");//规格[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //检查批号
                    Bat _bat = new Bat();
                    if (!String.IsNullOrEmpty(dr["BAT_NO_NEW"].ToString().Trim()))
                    {
                        SunlikeDataSet _ds = _bat.GetData(dr["BAT_NO_NEW"].ToString());
                        if (_ds.Tables["BAT_NO"].Rows.Count == 0)
                        {
                            dr.SetColumnError("BAT_NO_NEW", "批号[" + dr["BAT_NO_NEW"].ToString() + "]不存在或没有对其操作的权限，请检查");//批号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
            }
            if (!m_UpdatedBarCode && !_isRunAuditing)
            {
                this.UpdateBarRec(dr.Table.DataSet);
                m_UpdatedBarCode = true;
            }
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
            //判断是否走审核流程
            if (tableName == "MF_BAR")
            {
                //删除变更单时同步删除盘点单对应单号
                string _refNo = "";
                DRPPI _pi = new DRPPI();
                if (statementType == StatementType.Delete)
                {
                    _refNo = dr["REF_NO", DataRowVersion.Original].ToString();
                    if (!string.IsNullOrEmpty(_refNo))
                        _pi.DeleteDRPPI(_refNo, "BR", true);
                }
                else if (statementType == StatementType.Insert)
                {
                    _refNo = dr["REF_NO"].ToString();
                    if (!String.IsNullOrEmpty(_refNo))
                    {
                        string _errorMsg = _pi.UpdateMF_PT(_refNo, "BR", dr["BR_NO"].ToString(), true);
                        if (!string.IsNullOrEmpty(_errorMsg))
                            throw new SunlikeException(_errorMsg);
                    }
                }
                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_TYPE = "FX";
                //    _aps.BIL_ID = "BR";
                //    _aps.BIL_NO = dr["BR_NO"].ToString();
                //    _aps.BIL_DD = Convert.ToDateTime(dr["BR_DD"]);
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.CUS_NO = "";
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    //_aps.MOB_ID = "";
                //}
                //else
                //    _aps = new AudParamStruct("BR", Convert.ToString(dr["BR_NO", DataRowVersion.Original]));
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
            //DataTable _dt = ds.Tables[0];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();

            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "BR");
            //}
            //#endregion

        }
        protected override void AfterDsSave(DataSet ds)
        {
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
            BarCode _bar = new BarCode();
            string _errStr = "";
            Query _qry = new Query();
            SunlikeDataSet _ds = this.GetData("", bil_no, false);
            if (_ds.Tables["MF_BAR"].Rows.Count > 0)
            {
                string _bilNo = _ds.Tables["MF_BAR"].Rows[0]["BR_NO"].ToString();
                string _upDd = (Convert.ToDateTime(_ds.Tables["MF_BAR"].Rows[0]["BR_DD"])).ToString(Comp.SQLDateTimeFormat);
                string _usr = _ds.Tables["MF_BAR"].Rows[0]["USR"].ToString();

                DbDRPBR _dbBr = new DbDRPBR(Comp.Conn_DB);
                _dbBr.UpdateChkMan(_bilNo, chk_man, cls_dd);

                string _sqlRec = "";
                string _sqlChange = "";
                try
                {
                    DataTable _dtTf = _ds.Tables["TF_BAR"];

                    //自动拆箱
                    if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
                    {
                        StringBuilder _boxNoAll = new StringBuilder();
                        for (int i = 0; i < _dtTf.Rows.Count; i++)
                        {
                            DataTable _dtBar = _bar.GetBarRecord("BAR_NO='" + _dtTf.Rows[i]["BAR_CODE"].ToString() + "'", true);
                            if (_dtBar.Rows.Count > 0)
                            {
                                if ((!string.IsNullOrEmpty(_dtBar.Rows[0]["BOX_NO"].ToString().Trim())) && (string.IsNullOrEmpty(_dtTf.Rows[i]["BOX_NO"].ToString().Trim())))
                                {
                                    if (((_dtTf.Rows[i]["WH_OLD"].ToString().Trim() != _dtTf.Rows[i]["WH_NEW"].ToString().Trim())
                                        || (_dtTf.Rows[i]["CLS_OLD"].ToString().Trim() != _dtTf.Rows[i]["CLS_NEW"].ToString().Trim())))
                                    {
                                        if (_boxNoAll.ToString().IndexOf(_dtBar.Rows[i]["BOX_NO"].ToString() + ",") == -1)
                                        {
                                            _boxNoAll.Append(_dtBar.Rows[i]["BOX_NO"].ToString() + ",");
                                        }
                                    }
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(_boxNoAll.ToString().Trim()))
                        {
                            _boxNoAll = new StringBuilder(_boxNoAll.ToString().Substring(0, _boxNoAll.Length - 1));
                            string[] _boxAry = _boxNoAll.ToString().Split(new char[] { ',' });
                            _bar.DeleteBox(_boxAry, _usr, "BR", _bilNo);

                            #region 如果有自动拆箱的记录则删除单据表身箱码列内容
                            System.Text.StringBuilder _sbWhere = new System.Text.StringBuilder();
                            for (int i = 0; i < _boxAry.Length; i++)
                            {
                                if (_boxAry[i] != null && !String.IsNullOrEmpty(_boxAry[i]) && _sbWhere.ToString().IndexOf(_boxAry[i]) < 0)
                                {
                                    if (!String.IsNullOrEmpty(_sbWhere.ToString()))
                                    {
                                        _sbWhere.Append(",");
                                    }
                                    _sbWhere.Append(_boxAry[i]);
                                }
                            }
                            if (!String.IsNullOrEmpty(_sbWhere.ToString()))
                            {
                                string _where = " BR_NO='" + _bilNo + "' AND BOX_NO in ('" + _sbWhere.ToString().Replace(",", "','") + "')";
                                string _sql = "UPDATE TF_BAR SET BOX_NO=NULL WHERE" + _where;
                                _qry.RunSql(_sql);
                            }
                            #endregion
                        }
                    }
                    for (int i = 0; i < _dtTf.Rows.Count; i++)
                    {
                        DataRow _row = _dtTf.Rows[i];
                        string _barCode = _row["BAR_CODE"].ToString();
                        string _whOld = _row["WH_OLD"].ToString();
                        string _whNew = _row["WH_NEW"].ToString();
                        string _clsOld = _row["CLS_OLD"].ToString();
                        string _clsNew = _row["CLS_NEW"].ToString();
                        string _spcOld = _row["SPC_OLD"].ToString();
                        string _spcNew = _row["SPC_NEW"].ToString();
                        string _batOld = _row["BAT_NO_OLD"].ToString();
                        string _batNew = _row["BAT_NO_NEW"].ToString();
                        string _phFlagOld = _row["PH_FLAG_OLD"].ToString();
                        string _phFlagNew = _row["PH_FLAG_NEW"].ToString();

                        _sqlRec = " UPDATE BAR_REC SET WH='" + _whNew + "',STOP_ID='" + _clsNew + "',SPC_NO='" + _spcNew + "',BAT_NO='" + _batNew + "',PH_FLAG='" + _phFlagNew + "' "
                            + " WHERE BAR_NO='" + _barCode + "' AND ISNULL(WH,'')='" + _whOld + "' "
                            + " AND ISNULL(STOP_ID,'')='" + _clsOld + "' AND ISNULL(SPC_NO,'')='" + _spcOld + "' AND ISNULL(BAT_NO,'')='" + _batOld + "' AND ISNULL(PH_FLAG,'')='" + _phFlagOld + "'";

                        this.updateBarChange(_row, _upDd, _usr, true);

                        if (!String.IsNullOrEmpty(_sqlRec))
                        {
                            _qry.RunSql(_sqlRec);
                        }
                    }
                }
                catch (Exception _ex)
                {
                    _errStr = _ex.Message;
                }
            }
            return _errStr;
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
            // TODO:  Add DRPBR.Deny implementation
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
            string _errStr = "";
            Query _qry = new Query();
            SunlikeDataSet _ds = this.GetData("", bil_no, false);
            if (_ds.Tables["MF_BAR"].Rows.Count > 0)
            {
                this.SetCanModify(_ds, false);
                if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.NOTALLOW");//此单据不能修改，无法反审核！
                }

                string _bilNo = _ds.Tables["MF_BAR"].Rows[0]["BR_NO"].ToString();
                string _upDd = (Convert.ToDateTime(_ds.Tables["MF_BAR"].Rows[0]["BR_DD"])).ToString(Comp.SQLDateTimeFormat);

                DbDRPBR _dbBr = new DbDRPBR(Comp.Conn_DB);
                _dbBr.UpdateChkMan(_bilNo, "", DateTime.Now);
                try
                {
                    for (int i = 0; i < _ds.Tables["TF_BAR"].Rows.Count; i++)
                    {
                        DataRow _row = _ds.Tables["TF_BAR"].Rows[i];
                        string _barCode = _row["BAR_CODE"].ToString();
                        string _wh1 = _row["WH_OLD"].ToString();
                        string _cls1 = _row["CLS_OLD"].ToString();
                        string _spc1 = _row["SPC_OLD"].ToString();
                        string _wh = _row["WH_NEW"].ToString();
                        string _cls = _row["CLS_NEW"].ToString();
                        string _spc = _row["SPC_NEW"].ToString();
                        string _bat1 = _row["BAT_NO_OLD"].ToString();
                        string _bat2 = _row["BAT_NO_NEW"].ToString();
                        string _phFlag1 = _row["BAT_NO_OLD"].ToString();
                        string _phFlag2 = _row["BAT_NO_NEW"].ToString();
                        string _sqlRec = " UPDATE BAR_REC SET WH='" + _wh1 + "',STOP_ID='" + _cls1 + "',SPC_NO='" + _spc1 + "',BAT_NO='" + _bat1 + "',PH_FLAG='" + _phFlag1 + "' "
                            + " WHERE BAR_NO='" + _barCode + "' AND ISNULL(WH,'')='" + _wh + "' "
                            + " AND ISNULL(STOP_ID,'')='" + _cls + "' AND ISNULL(SPC_NO,'')='" + _spc + "' AND ISNULL(BAT_NO,'')='" + _bat2 + "' AND ISNULL(PH_FLAG,'')='" + _phFlag2 + "'";
                        string _sqlChange = "DELETE FROM BAR_CHANGE WHERE BAR_NO='" + _barCode + "' AND UPDDATE='" + _upDd + "' AND BIL_NO='" + _bilNo + "' AND BIL_ID='BR'";

                        if (!String.IsNullOrEmpty(_sqlRec))
                        {
                            _qry.RunSql(_sqlRec);
                        }
                        if (!String.IsNullOrEmpty(_sqlChange))
                        {
                            _qry.RunSql(_sqlChange);
                        }
                    }
                }
                catch (Exception _ex)
                {
                    _errStr = _ex.Message;
                }
            }
            return _errStr;
        }

        #endregion

        #region 更新BAR_REC
        private void UpdateBarRec(DataSet dataSet)
        {
            DataRow _drMf = dataSet.Tables["MF_BAR"].Rows[0];
            DateTime _upDd = DateTime.Today;
            string _bilNo = "", _usr = "";
            if (_drMf.RowState != DataRowState.Deleted)
            {
                _upDd = Convert.ToDateTime(_drMf["BR_DD"]);
                _bilNo = _drMf["BR_NO"].ToString();
                _usr = _drMf["USR"].ToString();
            }
            else
            {
                _upDd = Convert.ToDateTime(_drMf["BR_DD", DataRowVersion.Original]);
                _bilNo = _drMf["BR_NO", DataRowVersion.Original].ToString();
                _usr = _drMf["USR", DataRowVersion.Original].ToString();
            }
            BarCode _bar = new BarCode();
            DataSet _dsBarBox = new DataSet();
            DataTable _dtBarRec = new DataTable();
            DataSet _dsBarRec = new DataSet();
            StringBuilder _sbBarChange = new StringBuilder();
            ArrayList _alBoxNo = new ArrayList();
            ArrayList _alBoxWh = new ArrayList();
            ArrayList _alBoxStop = new ArrayList();
            ArrayList _alBoxDelete = new ArrayList();
            DataTable _dtBarChange = _bar.GetBarChange("BR", _bilNo);
            foreach (DataRow dr in dataSet.Tables["TF_BAR"].Rows)
            {
                string _barCode, _boxNo, _whOld = "", _whNew = "", _clsOld = "", _clsNew = "", _spcOld = "", _spcNew = "",
                    _batNoOld = "", _batNoNew = "", _phFlagOld = "", _phFlagNew = "";
                if (dr.RowState != DataRowState.Deleted)
                {
                    _barCode = dr["BAR_CODE"].ToString();
                    _boxNo = dr["BOX_NO"].ToString();
                    _whOld = dr["WH_OLD"].ToString();
                    _whNew = dr["WH_NEW"].ToString();
                    _clsOld = dr["CLS_OLD"].ToString();
                    _clsNew = dr["CLS_NEW"].ToString();
                    _spcOld = dr["SPC_OLD"].ToString();
                    _spcNew = dr["SPC_NEW"].ToString();
                    _batNoOld = dr["BAT_NO_OLD"].ToString();
                    _batNoNew = dr["BAT_NO_NEW"].ToString();
                    _phFlagOld = dr["PH_FLAG_OLD"].ToString();
                    _phFlagNew = dr["PH_FLAG_NEW"].ToString();
                }
                else
                {
                    _barCode = dr["BAR_CODE", DataRowVersion.Original].ToString();
                    _boxNo = dr["BOX_NO", DataRowVersion.Original].ToString();
                    _whOld = dr["WH_OLD", DataRowVersion.Original].ToString();
                    _whNew = dr["WH_NEW", DataRowVersion.Original].ToString();
                    _clsOld = dr["CLS_OLD", DataRowVersion.Original].ToString();
                    _clsNew = dr["CLS_NEW", DataRowVersion.Original].ToString();
                    _spcOld = dr["SPC_OLD", DataRowVersion.Original].ToString();
                    _spcNew = dr["SPC_NEW", DataRowVersion.Original].ToString();
                    _batNoOld = dr["BAT_NO_OLD", DataRowVersion.Original].ToString();
                    _batNoNew = dr["BAT_NO_NEW", DataRowVersion.Original].ToString();
                    _phFlagOld = dr["PH_FLAG_OLD", DataRowVersion.Original].ToString();
                    _phFlagNew = dr["PH_FLAG_NEW", DataRowVersion.Original].ToString();
                }
                if (!String.IsNullOrEmpty(_barCode))
                {
                    _dtBarRec = _bar.GetBarRecord("BAR_NO = '" + _barCode + "'", true);
                }
                else
                {
                    _dtBarRec = _bar.GetBarRecord("BOX_NO = '" + _boxNo + "'", true);

                    //准备资料更新BAR_BOX
                    _alBoxNo.Add(_boxNo);
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        _alBoxWh.Add(_whNew);
                        _alBoxStop.Add(_clsNew);
                    }
                    else
                    {
                        _alBoxWh.Add(_whOld);
                        _alBoxStop.Add(_clsOld);
                    }
                }
                if (_dtBarRec.Rows.Count > 0)
                {
                    foreach (DataRow drBarRec in _dtBarRec.Rows)
                    {
                        string _whNewCp = "";
                        string _clsNewCp = "F";
                        string _spcNewCp = "";
                        string _batNoNewCp = "";
                        string _phFlagNewCp = "F";
                        if (String.IsNullOrEmpty(_barCode))
                        {
                            _batNoOld = drBarRec["BAT_NO"].ToString();
                            _batNoNew = drBarRec["BAT_NO"].ToString();
                            _spcOld = drBarRec["SPC_NO"].ToString();
                            _spcNew = drBarRec["SPC_NO"].ToString();
                            _phFlagOld = drBarRec["PH_FLAG"].ToString();
                            _phFlagNew = drBarRec["PH_FLAG"].ToString();
                        }
                        if (dr.RowState != DataRowState.Added && dr.RowState != DataRowState.Unchanged)
                        {
                            _whNewCp = dr["WH_NEW", DataRowVersion.Original].ToString();
                            if (!String.IsNullOrEmpty(dr["CLS_NEW", DataRowVersion.Original].ToString()))
                            {
                                _clsNewCp = dr["CLS_NEW", DataRowVersion.Original].ToString();
                            }
                            _spcNewCp = dr["SPC_NEW", DataRowVersion.Original].ToString();
                            _batNoNewCp = dr["BAT_NO_NEW", DataRowVersion.Original].ToString();
                            if (!String.IsNullOrEmpty(dr["PH_FLAG_NEW", DataRowVersion.Original].ToString()))
                            {
                                _phFlagNewCp = dr["PH_FLAG_NEW", DataRowVersion.Original].ToString();
                            }
                            if (_whNewCp != drBarRec["WH"].ToString()
                                || _clsNewCp != drBarRec["STOP_ID"].ToString()
                                || _spcNewCp != drBarRec["SPC_NO"].ToString()
                                || _batNoNewCp != drBarRec["BAT_NO"].ToString()
                                || _phFlagNewCp != drBarRec["PH_FLAG"].ToString())
                            {
                                if (String.IsNullOrEmpty(_barCode))
                                {
                                    throw new Exception("序列号[" + _boxNo + "]原始资料错误!");
                                }
                                else
                                {
                                    throw new Exception("序列号[" + _barCode + "]原始资料错误!");
                                }
                            }
                            DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO = '" + drBarRec["BAR_NO"].ToString() + "'");
                            if (_draBarChange.Length > 0)
                            {
                                _sbBarChange.Append(_bar.DeleteBarChange(drBarRec["BAR_NO"].ToString(), "BR", _bilNo, true));
                            }
                        }
                        else
                        {
                            if (_whOld != drBarRec["WH"].ToString() || _clsOld != drBarRec["STOP_ID"].ToString()
                                || _spcOld != drBarRec["SPC_NO"].ToString() || _batNoOld != drBarRec["BAT_NO"].ToString()
                                || _phFlagOld != drBarRec["PH_FLAG"].ToString())
                            {
                                if (String.IsNullOrEmpty(_barCode))
                                {
                                    throw new Exception("序列号[" + _boxNo + "]原始资料错误!");
                                }
                                else
                                {
                                    throw new Exception("序列号[" + _barCode + "]原始资料错误!");
                                }
                            }
                            if (drBarRec["BOX_NO"].ToString() != _boxNo)
                            {
                                if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
                                {
                                    _alBoxDelete.Add(drBarRec["BOX_NO"].ToString());
                                }
                                else
                                {
                                    throw new Exception("RCID=COMMON.HINT.DOUBLEBOX,PARAM=" + _barCode);
                                }
                            }
                        }
                        if (dr.RowState != DataRowState.Deleted && _drMf.RowState != DataRowState.Deleted)
                        {
                            _sbBarChange.Append(_bar.InsertBarChange(drBarRec["BAR_NO"].ToString(), _whOld, _whNew, "BR", _bilNo,
                                _usr, _batNoOld, _batNoNew, _phFlagOld, _phFlagNew, _clsOld, _clsNew, true));
                            drBarRec["WH"] = _whNew;
                            drBarRec["UPDDATE"] = _upDd;
                            drBarRec["SPC_NO"] = _spcNew;
                            drBarRec["BAT_NO"] = _batNoNew;
                            drBarRec["PH_FLAG"] = _phFlagNew;
                            drBarRec["STOP_ID"] = _clsNew;
                        }
                        else
                        {
                            drBarRec["WH"] = _whOld;
                            drBarRec["UPDDATE"] = _upDd;
                            drBarRec["SPC_NO"] = _spcOld;
                            drBarRec["BAT_NO"] = _batNoOld;
                            drBarRec["PH_FLAG"] = _phFlagOld;
                            drBarRec["STOP_ID"] = _clsOld;
                        }
                    }
                }
                else if (!String.IsNullOrEmpty(_barCode))
                {
                    if (dr.RowState != DataRowState.Added)
                    {
                        throw new Exception("序列号[" + _barCode + "]不存在!");
                    }
                    else
                    {
                        _sbBarChange.Append(_bar.InsertBarChange(_barCode, "", _whNew, "BR", _bilNo,
                            _usr, "", _batNoNew, "", _phFlagNew, _clsOld, _clsNew, true));

                        DataRow _dr = _dtBarRec.NewRow();
                        _dr["BAR_NO"] = _barCode;
                        _dr["WH"] = _whNew;
                        _dr["UPDDATE"] = _upDd;
                        _dr["SPC_NO"] = _spcNew;
                        _dr["BAT_NO"] = _batNoNew;
                        _dr["PH_FLAG"] = _phFlagNew;
                        _dr["STOP_ID"] = _clsNew;
                    }
                }
                _dsBarRec.Merge(_dtBarRec, true, MissingSchemaAction.AddWithKey);
            }
            if (!String.IsNullOrEmpty(_sbBarChange.ToString()))
            {
                Query _query = new Query();
                _query.RunSql(_sbBarChange.ToString());
            }
            //更新BAR_REC
            _bar.UpdateRec(_dsBarRec);
            //更新BAR_BOX
            _bar.UpdateBarBox(_alBoxNo, _alBoxWh, _alBoxStop);
            //拆箱
            _bar.DeleteBox(_alBoxDelete, _usr, "BR", _bilNo);
        }
        /// <summary>
        /// 更新BAR_REC
        /// </summary>
        /// <param name="row">TF_BAR表身记录</param>
        /// <param name="upDd">更新时间</param>
        private void updateBarRec(DataRow row, string upDd)
        {
            upDd = (Convert.ToDateTime(upDd)).ToString(Comp.SQLDateTimeFormat);
            if (row != null)
            {
                Query _qry = new Query();
                string _sql = "";
                if (row.RowState == DataRowState.Added)
                {
                    string _barCode = row["BAR_CODE"].ToString();
                    string _whOld = row["WH_OLD"].ToString();
                    string _whNew = row["WH_NEW"].ToString();
                    string _clsOld = row["CLS_OLD"].ToString();
                    string _clsNew = row["CLS_NEW"].ToString();
                    string _spcOld = row["SPC_OLD"].ToString();
                    string _spcNew = row["SPC_NEW"].ToString();
                    string _batOld = row["BAT_NO_OLD"].ToString();
                    string _batNew = row["BAT_NO_NEW"].ToString();
                    string _phFlagOld = row["PH_FLAG_OLD"].ToString();
                    string _phFlagNew = row["PH_FLAG_NEW"].ToString();

                    _sql = " UPDATE BAR_REC SET WH='" + _whNew + "',STOP_ID='" + _clsNew + "',SPC_NO='" + _spcNew + "',BAT_NO='" + _batNew + "',PH_FLAG='" + _phFlagNew + "' "
                        + "  WHERE BAR_NO='" + _barCode + "' AND ISNULL(WH,'')='" + _whOld + "' AND ISNULL(STOP_ID,'')='" + _clsOld + "'"
                        + " AND ISNULL(SPC_NO,'')='" + _spcOld + "' AND ISNULL(BAT_NO,'')='" + _batOld + "' AND ISNULL(PH_FLAG,'')='" + _phFlagOld + "'";
                }
                if (row.RowState == DataRowState.Modified)
                {
                    string _barCode = row["BAR_CODE"].ToString();
                    string _whOld = row["WH_NEW", DataRowVersion.Original].ToString();
                    string _whNew = row["WH_NEW"].ToString();
                    string _clsOld = row["CLS_NEW", DataRowVersion.Original].ToString();
                    string _clsNew = row["CLS_NEW"].ToString();
                    string _spcOld = row["SPC_NEW", DataRowVersion.Original].ToString();
                    string _spcNew = row["SPC_NEW"].ToString();
                    string _batOld = row["BAT_NO_NEW", DataRowVersion.Original].ToString();
                    string _batNew = row["BAT_NO_NEW"].ToString();
                    string _phFlagOld = row["PH_FLAG_NEW", DataRowVersion.Original].ToString();
                    string _phFlagNew = row["PH_FLAG_NEW"].ToString();

                    if ((_whOld != _whNew) || (_clsOld != _clsNew) || (_spcOld != _spcNew))
                    {
                        _sql = " UPDATE BAR_REC SET WH='" + _whNew + "',STOP_ID='" + _clsNew + "',SPC_NO='" + _spcNew + "',BAT_NO='" + _batNew + "',PH_FLAG='" + _phFlagNew + "' "
                            + " WHERE BAR_NO='" + _barCode + "' AND ISNULL(WH,'')='" + _whOld + "' "
                            + " AND ISNULL(STOP_ID,'')='" + _clsOld + "' AND ISNULL(SPC_NO,'')='" + _spcOld + "' AND ISNULL(BAT_NO,'')='" + _batOld + "' AND ISNULL(PH_FLAG,'')='" + _phFlagOld + "'";
                    }
                }
                if (row.RowState == DataRowState.Deleted)
                {
                    string _barCode = row["BAR_CODE", DataRowVersion.Original].ToString();
                    string _wh1 = row["WH_OLD", DataRowVersion.Original].ToString();
                    string _cls1 = row["CLS_OLD", DataRowVersion.Original].ToString();
                    string _spc1 = row["SPC_OLD", DataRowVersion.Original].ToString();
                    string _wh = row["WH_NEW", DataRowVersion.Original].ToString();
                    string _cls = row["CLS_NEW", DataRowVersion.Original].ToString();
                    string _spc = row["SPC_NEW", DataRowVersion.Original].ToString();
                    string _batOld = row["BAT_NO_OLD", DataRowVersion.Original].ToString();
                    string _batNew = row["BAT_NO_NEW", DataRowVersion.Original].ToString();
                    string _phFlagOld = row["PH_FLAG_OLD", DataRowVersion.Original].ToString();
                    string _phFlagNew = row["PH_FLAG_NEW", DataRowVersion.Original].ToString();
                    _sql = " UPDATE BAR_REC SET WH='" + _wh1 + "',STOP_ID='" + _cls1 + "',SPC_NO='" + _spc1 + "',BAT_NO='" + _batOld + "',PH_FLAG='" + _phFlagOld + "' "
                        + " WHERE BAR_NO='" + _barCode + "' AND ISNULL(WH,'')='" + _wh + "' AND ISNULL(STOP_ID,'')='" + _cls + "'"
                        + " AND ISNULL(SPC_NO,'')='" + _spc + "' AND ISNULL(BAT_NO,'')='" + _batNew + "' AND ISNULL(PH_FLAG,'')='" + _phFlagNew + "' ";
                }
                if (!String.IsNullOrEmpty(_sql))
                {
                    _qry.RunSql(_sql);
                }
            }
        }
        #endregion

        #region 更新BAR_CHANGE
        /// <summary>
        /// 更新BAR_CHANGE
        /// </summary>
        /// <param name="row">TF_BAR表身记录</param>
        /// <param name="upDd">更新时间</param>
        /// <param name="usr">更新人</param>
        /// <param name="isAuditing">审核</param>
        private void updateBarChange(DataRow row, string upDd, string usr, bool isAuditing)
        {
            upDd = (Convert.ToDateTime(upDd)).ToString(Comp.SQLDateTimeFormat);
            if (row != null)
            {
                string _sql = "";
                if (row.RowState == DataRowState.Added || isAuditing)
                {
                    string _bilNo = row["BR_NO"].ToString();
                    string _barCode = row["BAR_CODE"].ToString();
                    string _whOld = row["WH_OLD"].ToString();
                    string _whNew = row["WH_NEW"].ToString();
                    string _batOld = row["BAT_NO_OLD"].ToString();
                    string _batNew = row["BAT_NO_NEW"].ToString();
                    string _phFlagOld = row["PH_FLAG_OLD"].ToString();
                    string _phFlagNew = row["PH_FLAG_NEW"].ToString();
                    BarCode _bar = new BarCode();
                    DataTable _dt = _bar.GetBarChange(_barCode, Convert.ToDateTime(upDd));
                    if (_dt.Rows.Count > 0)
                    {
                        _whOld = _dt.Rows[0]["WH1"].ToString();
                        _batOld = _dt.Rows[0]["BAT_NO1"].ToString();
                        _phFlagOld = _dt.Rows[0]["PH_FLAG1"].ToString();
                        if (_whNew == _whOld && _batNew == _batOld && _phFlagNew == _phFlagOld)
                        {
                            _sql = "DELETE FROM BAR_CHANGE WHERE BAR_NO='" + _barCode + "' AND UPDDATE = '" + upDd + "' ";
                        }
                        else
                        {
                            _sql = " UPDATE BAR_CHANGE SET WH1='" + _whOld + "',WH2='" + _whNew + "',BAT_NO1='" + _batOld + "',BAT_NO2='" + _batNew + "',PH_FLAG1='" + _phFlagOld + "',PH_FLAG2='" + _phFlagNew + "' "
                                + " WHERE BAR_NO='" + _barCode + "' AND UPDDATE = '" + upDd + "' ";
                        }
                    }
                    else
                    {
                        _sql = " INSERT INTO BAR_CHANGE(BAR_NO,UPDDATE,WH1,WH2,BIL_NO,USR,BIL_ID,BAT_NO1,BAT_NO2,PH_FLAG1,PH_FLAG2) "
                            + " VALUES('" + _barCode + "','" + upDd + "','" + _whOld + "','" + _whNew + "','" + _bilNo + "','" + usr + "','BR','" + _batOld + "','" + _batNew + "','" + _phFlagOld + "','" + _phFlagNew + "')";
                    }
                }
                if (row.RowState == DataRowState.Modified)
                {
                    string _bilNo = row["BR_NO"].ToString();
                    string _barCode = row["BAR_CODE"].ToString();
                    string _whOld = row["WH_OLD"].ToString();
                    string _whNew = row["WH_NEW"].ToString();
                    string _batOld = row["BAT_NO_OLD"].ToString();
                    string _batNew = row["BAT_NO_NEW"].ToString();
                    string _phFlagOld = row["PH_FLAG_OLD"].ToString();
                    string _phFlagNew = row["PH_FLAG_NEW"].ToString();
                    BarCode _bar = new BarCode();
                    DataTable _dt = _bar.GetBarChange(_barCode, Convert.ToDateTime(upDd));
                    if (_dt.Rows.Count > 0)
                    {
                        _whOld = _dt.Rows[0]["WH1"].ToString();
                        _batOld = _dt.Rows[0]["BAT_NO1"].ToString();
                        _phFlagOld = _dt.Rows[0]["PH_FLAG1"].ToString();
                    }
                    if (_whOld != _whNew
                        || _batOld != _batNew
                        || _phFlagOld != _phFlagNew)
                    {
                        _sql = " UPDATE BAR_CHANGE SET WH1='" + _whOld + "',WH2='" + _whNew + "',BAT_NO1='" + _batOld + "',BAT_NO2='" + _batNew + "',PH_FLAG1='" + _phFlagOld + "',PH_FLAG2='" + _phFlagNew + "' "
                            + " WHERE BAR_NO='" + _barCode + "' AND UPDDATE = '" + upDd + "' ";
                    }
                }
                if (row.RowState == DataRowState.Deleted)
                {
                    string _bilNo = row["BR_NO", DataRowVersion.Original].ToString();
                    string _barCode = row["BAR_CODE", DataRowVersion.Original].ToString();
                    _sql = "DELETE FROM BAR_CHANGE WHERE BAR_NO='" + _barCode + "' AND UPDDATE = '" + upDd + "' ";
                }
                if (!String.IsNullOrEmpty(_sql))
                {
                    Query _qry = new Query();
                    _qry.RunSql(_sql);
                }
            }
        }
        #endregion

        #region 判断是否可以修改
        /// <summary>
        /// 判断是否可以修改
        /// </summary>
        /// <param name="ChangedDS">抓取到的DATASET</param>
        /// <param name="IsCheckAuditing">是否判断审核流程</param>
        private void SetCanModify(SunlikeDataSet ChangedDS, bool IsCheckAuditing)
        {
            bool _modify = true;//是否可以修改
            BarCode _bar = new BarCode();
            if (ChangedDS.Tables["MF_BAR"].Rows.Count > 0)
            {
                DataTable _dt = ChangedDS.Tables["TF_BAR"];

                //在抓取资料时判断，如果已经进入审核，怎不允许修改和删除
                if (IsCheckAuditing)
                {
                    Auditing _auditing = new Auditing();
                    _modify = !_auditing.GetIfEnterAuditing("BR", ChangedDS.Tables["MF_BAR"].Rows[0]["BR_NO"].ToString());
                }
                else//如果是反审核状态，则判断条码是否已经变化，如已变更，则不允许反审核
                {
                    if (_dt.Rows.Count > 0)
                    {
                        string _barCode = "";
                        for (int i = 0; i < _dt.Rows.Count; i++)
                        {
                            DataRow _row = _dt.Rows[i];
                            _barCode = _row["BAR_CODE"].ToString();
                            DataTable _dtBarCode = _bar.GetBarRecord(" BAR_NO='" + _barCode + "' ", true);
                            if (_dt.Rows.Count > 0)
                            {
                                if (!IsCheckAuditing)
                                {
                                    if (_row["BOX_NO"].ToString() != _dtBarCode.Rows[0]["BOX_NO"].ToString())
                                    {
                                        _modify = false;
                                        break;
                                    }
                                    if (_row["WH_NEW"].ToString() != _dtBarCode.Rows[0]["WH"].ToString())
                                    {
                                        _modify = false;
                                        break;
                                    }
                                    if (_row["CLS_NEW"].ToString() != _dtBarCode.Rows[0]["STOP_ID"].ToString())
                                    {
                                        _modify = false;
                                        break;
                                    }
                                    if (_row["SPC_NEW"].ToString() != _dtBarCode.Rows[0]["SPC_NO"].ToString())
                                    {
                                        _modify = false;
                                        break;
                                    }
                                    if (_row["BAT_NO_NEW"].ToString() != _dtBarCode.Rows[0]["BAT_NO"].ToString())
                                    {
                                        _modify = false;
                                        break;
                                    }
                                    if (_row["PH_FLAG_NEW"].ToString() != _dtBarCode.Rows[0]["PH_FLAG"].ToString())
                                    {
                                        _modify = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                _modify = false;
                                break;
                            }
                        }
                    }
                }
                //判断是否锁单
                if (_modify && !String.IsNullOrEmpty(ChangedDS.Tables["MF_BAR"].Rows[0]["LOCK_MAN"].ToString()))
                {
                    _modify = false;
                }
                ChangedDS.ExtendedProperties["CAN_MODIFY"] = _modify.ToString().Substring(0, 1).ToUpper();
            }
        }
        #endregion
    }
}
