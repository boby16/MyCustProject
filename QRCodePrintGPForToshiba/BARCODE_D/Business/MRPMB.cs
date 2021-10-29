using System;
using System.Data;
using System.Collections;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;
using System.Collections.Generic;

namespace Sunlike.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class MRPMB : BizObject, IAuditing
    {
        private bool _isRunAuditing;
        private int _barCodeNo;
        private bool _auditBarCode;
        private string _saveID = "";

        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="mbId"></param>
        /// <param name="mbNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string mbId, string mbNo, bool onlyFillSchema)
        {
            DbMRPMB _dbMb = new DbMRPMB(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMb.GetData(mbId, mbNo, onlyFillSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;

            }

            //增加单据权限
            if (!onlyFillSchema)
            {
                this.SetCanModify(usr, _ds, true, false);
            }

            //设定表身的PreItm为自动递增
            DataTable _dtTf = _ds.Tables["TF_MB"];
            DataTable _dtMf = _ds.Tables["MF_MB"];
            _dtTf.Columns["PRE_ITM"].AutoIncrement = true;
            _dtTf.Columns["PRE_ITM"].AutoIncrementSeed = _dtTf.Rows.Count > 0 ? Convert.ToInt32(_dtTf.Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _dtTf.Columns["PRE_ITM"].AutoIncrementStep = 1;
            _dtTf.Columns["PRE_ITM"].Unique = true;

            _dtTf.Columns["EST_ITM"].AutoIncrement = true;
            _dtTf.Columns["EST_ITM"].AutoIncrementSeed = _dtTf.Rows.Count > 0 ? Convert.ToInt32(_dtTf.Select("", "EST_ITM desc")[0]["EST_ITM"]) + 1 : 1;
            _dtTf.Columns["EST_ITM"].AutoIncrementStep = 1;
            _dtTf.Columns["EST_ITM"].Unique = true;

            _dtTf.Columns.Add("PRD_NO_NO", typeof(System.String));
            foreach (DataRow _drTf in _dtTf.Rows)
            {
                _drTf["PRD_NO_NO"] = _drTf["PRD_NO"];
            }

            //创建存放序列号的暂存表
            DataTable _dt = new DataTable("BAR_COLLECT");
            _dt.Columns.Add("ITEM", typeof(int));
            _dt.Columns.Add("BAR_CODE");
            _dt.Columns.Add("BAT_NO");
            _dt.Columns.Add("BOX_NO");
            _dt.Columns.Add("PRD_NO");
            _dt.Columns.Add("PRD_MARK");
            //表身库位
            _dt.Columns.Add("WH1");
            _dt.Columns.Add("SERIAL_NO");
            //序列号库位和批号
            _dt.Columns.Add("ISEXIST");
            _dt.Columns.Add("WH_REC");
            _dt.Columns.Add("BAT_NO_REC");
            _dt.Columns.Add("STOP_ID");
            _dt.Columns.Add("PH_FLAG");
            DataColumn[] _dca = new DataColumn[1];
            _dca[0] = _dt.Columns["ITEM"];
            _dt.PrimaryKey = _dca;
            _ds.Tables.Add(_dt);
            //把序列号的记录转入暂存表中
            DataView _dv = new DataView(_ds.Tables["TF_MB_B"]);
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
            //取序列号库位、批号
            foreach (DataRow dr in _dt.Rows)
            {
                DataRow[] _aryDrBar = _ds.Tables["TF_BLN_B"].Select("BAR_CODE='" + dr["BAR_CODE"].ToString() + "'");
                if (_aryDrBar.Length > 0)
                {
                    DataRow[] _aryDr = _ds.Tables["TF_BLN"].Select("PRE_ITM=" + _aryDrBar[0]["MB_ITM"].ToString());
                    if (_aryDr.Length > 0)
                    {
                        dr["WH1"] = _aryDr[0]["WH"];
                        dr["BAT_NO"] = _aryDr[0]["BAT_NO"];
                    }
                }
            }
            _dv.Dispose();
            GC.Collect(GC.GetGeneration(_dv));
            _ds.AcceptChanges();
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="bilNo"></param>
        /// <param name="bilItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBilNo(string bilId, string bilNo, int bilItm)
        {
            DbMRPMB _dbMrpMb = new DbMRPMB(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMrpMb.GetDataBilNo(bilId, bilNo, bilItm);
            SetCanModify("", _ds, true, false);
            return _ds;
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="dataSet"></param>
        /// <param name="throwException"></param>
        public DataTable UpdateData(string usr, SunlikeDataSet dataSet, bool throwException)
        {
            string _mbId, _usr;
            string _bilType;
            DataRow _dr = dataSet.Tables["MF_MB"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _mbId = _dr["MB_ID", DataRowVersion.Original].ToString();
                _usr = _dr["USR", DataRowVersion.Original].ToString();
                _bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
            }
            else
            {
                _mbId = _dr["MB_ID"].ToString();
                _usr = _dr["USR"].ToString();
                _bilType = _dr["BIL_TYPE"].ToString();
            }

            //判断是否直接保存
            if (dataSet.ExtendedProperties["SAVE_ID"] != null)
            {
                this._saveID = dataSet.ExtendedProperties["SAVE_ID"].ToString();
            }
            else
            {
                this._saveID = "";
            }

            Hashtable _ht = new Hashtable();
            _ht["MF_MB"] = "MB_ID,MB_NO,MB_DD,BIL_ID,BIL_NO,BIL_ITM,MO_NO,SO_NO,USR_NO,WH_PRD,WH_MTL,MRP_NO,PRD_MARK,PRD_NAME,"
                   + " EXP_MTH,UNIT,QTY,QTY1,BAT_NO,VALID_DD,DEP,CST,CST_MAKE,CST_PRD,CST_MAN,CST_OUT,USED_TIME,VOH_ID,VOH_NO,REM,CPY_SW,"
                   + " CST_STD,CST_SMAKE,CST_SPRD,CST_SMAN,CST_SOUT,USED_STIME,CST_DIFF,CST_SDIFF,USR,CHK_MAN,PRT_SW,MD_NO,CLS_DATE,ID_NO,"
                   + " EST_ITM,POSID,BIL_TYPE,RAT,MOB_ID,LOCK_MAN,FJ_NUM,SYS_DATE,TIME_CNT,TIME_SCNT,CAS_NO,TASK_ID,PRT_USR,RK_DD,ISSVS,DEP_RK,"
                   + " AMTN_VAL,UP_MAIN,CANCEL_ID ";

            _ht["TF_MB"] = " MB_ID,MB_NO,ITM,MB_DD,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY,QTY1,QTY_LOST,QTY1_LOST,CST,BAT_NO,REM,CST_EP,CPY_SW,CST_STD,"
                   + " CST_SEP,FT_ID,PRD_NO_CHG,ID_NO,VAL_FT,POSID,CNTT_NO,COMPOSE_IDNO,EST_ITM,PRE_ITM,USEIN_NO,LOS_RTO,QTY_STD,RK_DD,DEP_RK,CAS_NO,TASK_ID,"
                   + " OS_ID,OS_NO,UP_MAIN";

            //判断是否走审核流程
            if (string.IsNullOrEmpty(_saveID))
            {
                Auditing _auditing = new Auditing();
                //_isRunAuditing = _auditing.IsRunAuditing(_mbId, _usr, _bilType, "");
            }
            else if (string.Compare(this._saveID, "T") == 0)
            {
                _isRunAuditing = false;
            }
            else
            {
                _isRunAuditing = true;
            }

            this.UpdateDataSet(dataSet, _ht);
            if (!dataSet.HasErrors)
            {
                //增加单据权限
                string _UpdUsr = "";
                if (dataSet.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = dataSet.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "MRP" + _mbId;
                    DataTable _dtMf = dataSet.Tables["MF_MB"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        dataSet.ExtendedProperties["UPD"] = _billRight["UPD"];
                        dataSet.ExtendedProperties["DEL"] = _billRight["DEL"];
                        dataSet.ExtendedProperties["PRN"] = _billRight["PRN"];
                        dataSet.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }

                this.SetCanModify(usr, dataSet, false, false);
                dataSet.AcceptChanges();
            }
            else if (throwException)
            {
                throw new Exception(GetErrorsString(dataSet));
            }
            return GetAllErrors(dataSet);
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
            Auditing _auditing = new Auditing();
            string _mbId = "", _mbNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _mbId = dr["MB_ID"].ToString();
                _mbNo = dr["MB_NO"].ToString();
            }
            else
            {
                _mbId = dr["MB_ID", DataRowVersion.Original].ToString();
                _mbNo = dr["MB_NO", DataRowVersion.Original].ToString();
            }
            if (statementType != StatementType.Insert)
            {

                if (_auditing.GetIfEnterAuditing(_mbId, _mbNo))//如果进去审核了就不能修改和新增删除
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");
                }

                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "MB_ID = '" + _mbId + "' AND MB_NO = '" + _mbNo + "'";
                if (_Users.IsLocked("MF_MB", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            if (tableName == "MF_MB")
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
                //检查资料正确否
                if (statementType != StatementType.Delete)
                {
                    dr["SYS_DATE"] = Convert.ToDateTime(DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    //新增时判断关账日期
                    if (statementType != StatementType.Delete)
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["MB_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    else
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["MB_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }

                    //检查业务员
                    Salm _salm = new Salm();
                    if (!string.IsNullOrEmpty(dr["USR_NO"].ToString()) && !_salm.IsExist(_usr, dr["USR_NO"].ToString(), Convert.ToDateTime(dr["MB_DD"])))
                    {
                        dr.SetColumnError("USR_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["USR_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //检查部门
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["MB_DD"])))
                    {
                        dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());//部门代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    #region 新增

                    //取得保存单号
                    dr["MB_NO"] = _sq.Set(_mbId, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["MB_DD"]), dr["BIL_TYPE"].ToString());
                    //写入默认栏位值
                    dr["PRT_SW"] = "N";

                    if (String.IsNullOrEmpty(dr["EXP_MTH"].ToString()))
                    {
                        dr["EXP_MTH"] = "1";
                    }
                    #endregion
                }
                else
                {
                    if (statementType == StatementType.Delete)
                    {
                        #region 删除
                        string _error = _sq.Delete(dr["MB_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                        if (!String.IsNullOrEmpty(_error))
                        {
                            throw new Exception("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
                        }

                        #endregion
                    }
                }
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (!_isRunAuditing)
                    {
                        bool _rebuildVoh = true;
                        if (dr.Table.DataSet.ExtendedProperties["REBUILD_VOH"] != null)
                        {
                            _rebuildVoh = Convert.ToBoolean(dr.Table.DataSet.ExtendedProperties["REBUILD_VOH"]);
                        }
                    }
                }
                #region 审核关联
                if (!string.IsNullOrEmpty(this._saveID))
                {
                    if (statementType != StatementType.Delete)
                    {
                        if (_isRunAuditing)
                        {
                            dr["CHK_MAN"] = System.DBNull.Value;
                            dr["CLS_DATE"] = System.DBNull.Value;
                        }
                        else
                        {
                            dr["CHK_MAN"] = dr["USR"].ToString();
                            dr["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        }
                    }
                }
                else
                {
                    //AudParamStruct _aps = new AudParamStruct();
                    //if (statementType != StatementType.Delete)
                    //{
                    //    _aps.BIL_DD = Convert.ToDateTime(dr["MB_DD"]);
                    //    _aps.BIL_ID = dr["MB_ID"].ToString();
                    //    _aps.BIL_NO = dr["MB_NO"].ToString();
                    //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                    //    _aps.DEP = dr["DEP"].ToString();
                    //    _aps.SAL_NO = dr["USR_NO"].ToString();
                    //    _aps.USR = dr["USR"].ToString();
                    //    _aps.MOB_ID = ""; //新加的部分，对应审核模板
                    //}
                    //else
                    //{
                    //    _aps = new AudParamStruct(Convert.ToString(dr["MB_ID", DataRowVersion.Original]), Convert.ToString(dr["MB_NO", DataRowVersion.Original]));
                    //}

                    //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                    //if (!string.IsNullOrEmpty(_auditErr))
                    //{
                    //    throw new SunlikeException(_auditErr);
                    //}
                }
                #endregion
            }
            else if (tableName == "TF_MB")
            {
                #region 新增或者修改时
                if (statementType != StatementType.Delete)
                {
                    string _usr = dr.Table.DataSet.Tables["MF_MB"].Rows[0]["USR"].ToString();
                    Prdt _prdt = new Prdt();
                    //检查货品代号
                    if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MB"].Rows[0]["MB_DD"])))
                    {
                        dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + dr["PRD_NO"].ToString());//货品代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
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
                                string _fldName = _dtMark.Rows[i]["FLDNAME"].ToString();
                                if (!_mark.IsExist(_fldName, dr["PRD_NO"].ToString(), _aryMark[i]))
                                {
                                    dr.SetColumnError(_fldName, "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _aryMark[i].Trim());//货品特征[{0}]不存在
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                    //检查库位
                    WH _wh = new WH();
                    if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MB"].Rows[0]["MB_DD"])))
                    {
                        dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//仓库代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //修改PRD_NAME字段
                    if (String.IsNullOrEmpty(dr["PRD_NAME"].ToString()))
                    {
                        DataTable _dt = _prdt.GetPrdt(dr["PRD_NO"].ToString());
                        if (_dt.Rows.Count > 0)
                        {
                            dr["PRD_NAME"] = _dt.Rows[0]["NAME"];
                        }
                    }
                }
                #endregion
            }

            if (_barCodeNo == 0)
            {
                #region 更新序列号记录["TF_MB_B"]
                //if (!_isRunAuditing)
                //{
                //    if (_blId == "BN" || _blId == "BB")
                //    {
                //        this.UpdateBarCodeB(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                //    }
                //    else
                //    {
                //        this.UpdateBarCodeL(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                //    }
                //}
                //if (dr.Table.DataSet.Tables["MF_BLN"].Rows[0].RowState == DataRowState.Deleted)
                //{
                //    Query _query = new Query();
                //    _query.RunSql("delete from TF_BLN_B where BL_ID='" + dr["BL_ID", DataRowVersion.Original].ToString()
                //        + "' and  BL_NO='" + dr["BL_NO", DataRowVersion.Original].ToString() + "'");
                //}
                //else
                //{
                //    string _fieldList = "BL_ID,BL_NO,BL_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
                //    SQLBatchUpdater _sbu = new SQLBatchUpdater(Comp.Conn_DB);
                //    _sbu.BatchUpdateSize = 50;
                //    _sbu.BatchUpdate(dr.Table.DataSet.Tables["TF_BLN_B"], _fieldList);
                //}
                #endregion
            }
            _barCodeNo++;
        }

        private object SunlikeException(string _auditErr)
        {
            throw new Exception("The method or operation is not implemented.");
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
            string _mbId = "";
            string _mbNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _mbId = dr["MB_ID"].ToString();
                _mbNo = dr["MB_NO"].ToString();
            }
            else
            {
                _mbId = dr["MB_ID", DataRowVersion.Original].ToString();
                _mbNo = dr["MB_NO", DataRowVersion.Original].ToString();
            }
            //判断是否走审核流程
            if (_isRunAuditing)
            {
                if (tableName == "MF_MB")
                {
                    if (statementType != StatementType.Delete)
                    {
                        //dr["CHK_MAN"] = System.DBNull.Value;
                        //dr["CLS_DATE"] = System.DBNull.Value;
                        //AudParamStruct _aps = new AudParamStruct();
                        //_aps.BIL_DD = Convert.ToDateTime(dr["MB_DD"]);
                        //_aps.BIL_ID = dr["MB_ID"].ToString();
                        //_aps.BIL_NO = dr["MB_NO"].ToString();
                        //_aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                        //_aps.DEP = dr["DEP"].ToString();
                        //_aps.SAL_NO = dr["USR_NO"].ToString();
                        //_aps.USR = dr["USR"].ToString();
                        //Auditing _auditing = new Auditing();

                        ////新加的部分，对应审核模板
                        //_aps.MOB_ID = "";
                        //if (dr.Table.Columns.Contains("MOB_ID"))
                        //{
                        //    dr["MOB_ID"] = _aps.MOB_ID = _auditing.GetSHMobID(_aps.BIL_ID, _aps.USR, _aps.BIL_TYPE, Convert.ToString(dr["MOB_ID"]));
                        //}
                        //_auditing.SetBillToAudtingFlow("DRP", _aps, null);
                    }
                }
            }
            else
            {
                if (tableName == "MF_MB")
                {
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateMFWh(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateMFWh(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateMFWh(dr, false);
                        this.UpdateMFWh(dr, true);
                    }
                }
                else if (tableName == "TF_MB")
                {
                    #region 更新产品库存
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateTFWh(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateTFWh(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateTFWh(dr, false);
                        this.UpdateTFWh(dr, true);
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// SetCanModify
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="dataSet"></param>
        /// <param name="isChkAuditing"></param>
        /// <param name="isRollBack"></param>
        private void SetCanModify(string usr, SunlikeDataSet dataSet, bool isChkAuditing, bool isRollBack)
        {

            DataTable _dtMf = dataSet.Tables["MF_MB"];
            DataTable _dtTf = dataSet.Tables["TF_MB"];

            if (_dtMf.Rows.Count > 0)
            {
                string _mbId = _dtMf.Rows[0]["MB_ID"].ToString();
                string _pgm = "MRP" + _mbId;
                string _billDep = _dtMf.Rows[0]["DEP"].ToString();
                string _billUsr = _dtMf.Rows[0]["USR"].ToString();
                System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _billDep, _billUsr);
                dataSet.ExtendedProperties["UPD"] = _billRight["UPD"];
                dataSet.ExtendedProperties["DEL"] = _billRight["DEL"];
                dataSet.ExtendedProperties["PRN"] = _billRight["PRN"];
                dataSet.ExtendedProperties["LCK"] = _billRight["LCK"];

                bool _canModify = true;
                if (_canModify)
                {
                    _canModify = !Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["MB_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_MNU");
                }

                if (_canModify && isChkAuditing)
                {
                    string _mbID = _dtMf.Rows[0]["MB_ID"].ToString();
                    string _mbNo = _dtMf.Rows[0]["MB_NO"].ToString();
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_mbID, _mbNo))
                    {
                        _canModify = false;
                    }
                }

                //判断是否锁单
                if (_canModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _canModify = false;
                }
                dataSet.ExtendedProperties["CAN_MODIFY"] = _canModify.ToString().Substring(0, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="IsAdd"></param>
        private void UpdateMFWh(DataRow dr, bool IsAdd)
        {
            string _batNo = "";
            string _validDd = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _unit = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
            decimal _cst = 0;
            string _bilId = "";
            string _bilNo = "";
            string _chkman = "";
            int _bilItm = 0;

            if (IsAdd)
            {
                _bilId = dr["BIL_ID"].ToString();
                _bilNo = dr["BIL_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["BIL_ITM"].ToString()))
                {
                    _bilItm = Convert.ToInt32(dr["BIL_ITM"]);
                }

                _batNo = dr["BAT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat);
                }
                _prdNo = dr["MRP_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _whNo = dr["WH_PRD"].ToString();
                _unit = dr["UNIT"].ToString();
                _chkman = dr["CHK_MAN"].ToString();
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1"].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1"]);
                }
                if (!String.IsNullOrEmpty(dr["CST"].ToString()))
                {
                    _cst = Convert.ToDecimal(dr["CST"]);
                }
            }
            else
            {
                _bilId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _bilNo = dr["BIL_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["BIL_ITM", DataRowVersion.Original].ToString()))
                {
                    _bilItm = Convert.ToInt32(dr["BIL_ITM", DataRowVersion.Original]);
                }

                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD", DataRowVersion.Original].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD", DataRowVersion.Original]).ToString(Comp.SQLDateFormat);
                }

                _prdNo = dr["MRP_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = dr["WH_PRD", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                _chkman = dr["CHK_MAN", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty -= Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                {
                    _qty1 -= Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["CST", DataRowVersion.Original].ToString()))
                {
                    _cst -= Convert.ToDecimal(dr["CST", DataRowVersion.Original]);
                }
            }

            if (!IsAdd && string.IsNullOrEmpty(_chkman))
                return;

            WH _wh = new WH();
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();

            if (!String.IsNullOrEmpty(_batNo))
            {
                _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY] = _qty;
                _ht[WH.QtyTypes.QTY1] = _qty1;
                _ht[WH.QtyTypes.CST] = _cst;

                Prdt _prdt = new Prdt();
                SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                if (!String.IsNullOrEmpty(_validDd))
                {
                    if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                    {
                        TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                        if (_timeSpan.Days > 0)
                        {
                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                        }
                        else
                        {
                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht);
                        }
                    }
                    else
                    {
                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht);
                    }
                }
                else
                {
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                }
            }
            else
            {
                _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY] = _qty;
                _ht[WH.QtyTypes.QTY1] = _qty1;
                _ht[WH.QtyTypes.AMT_CST] = _cst;
                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="IsAdd"></param>
        private void UpdateTFWh(DataRow dr, bool IsAdd)
        {
            string _batNo = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _unit = "";
            string _chkMan = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
            decimal _cst = 0;

            if (IsAdd)
            {
                _batNo = dr["BAT_NO"].ToString();
                _prdNo = dr["PRD_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _whNo = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                _chkMan = dr.Table.DataSet.Tables["MF_MB"].Rows[0]["CHK_MAN"].ToString();
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty -= Convert.ToDecimal(dr["QTY"]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1"].ToString()))
                {
                    _qty1 -= Convert.ToDecimal(dr["QTY1"]);
                }
                if (!String.IsNullOrEmpty(dr["CST"].ToString()))
                {
                    _cst -= Convert.ToDecimal(dr["CST"]);
                }
            }
            else
            {
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                _chkMan = dr.Table.DataSet.Tables["MF_MB"].Rows[0]["CHK_MAN", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["CST", DataRowVersion.Original].ToString()))
                {
                    _cst = Convert.ToDecimal(dr["CST", DataRowVersion.Original]);
                }
            }

            //如果原来没有审核，则不用勾库存
            if (!IsAdd && string.IsNullOrEmpty(_chkMan))
                return;

            WH _wh = new WH();
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();

            if (!String.IsNullOrEmpty(_batNo))
            {
                _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY] = _qty;
                _ht[WH.QtyTypes.QTY1] = _qty1;
                _ht[WH.QtyTypes.CST] = _cst;

                Prdt _prdt = new Prdt();
                SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
            }
            else
            {
                _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY] = _qty;
                _ht[WH.QtyTypes.QTY1] = _qty1;
                _ht[WH.QtyTypes.AMT_CST] = _cst;
                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
            }
        }

        #region IAuditing Members

        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string RollBack(string bil_id, string bil_no)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
