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
    /// <summary>
    /// 验收单
    /// </summary>
    public class MRPTY : BizObject, IAuditing
    {
        #region Variable
        private string _tyId;
        private string _loginUsr;
        private bool _isRunAuditing;
        #endregion
        /// <summary>
        /// 验收单
        /// </summary>
        public MRPTY()
        {
        }

        #region 取数据
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="tyId"></param>
        /// <param name="tyNo"></param>        
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>        
        public SunlikeDataSet GetUpdateData(string pgm, string usr, string tyId, string tyNo, bool onlyFillSchema)
        {
            DbMRPTY _dbTy = new DbMRPTY(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbTy.GetData(tyId, tyNo, onlyFillSchema);

            if (_ds.Tables["MF_TY"].Rows.Count > 0)
            {
                string _bill_Dep = _ds.Tables["MF_TY"].Rows[0]["DEP"].ToString();
                string _bill_Usr = _ds.Tables["MF_TY"].Rows[0]["USR"].ToString();
                System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                this.SetCanModify(_ds, true, false);
            }

            DataTable _dtTf = _ds.Tables["TF_TY"];
            _dtTf.Columns["PRE_ITM"].AutoIncrement = true;
            _dtTf.Columns["PRE_ITM"].AutoIncrementSeed = _dtTf.Rows.Count > 0 ? Convert.ToInt32(_dtTf.Select("", "PRE_ITM DESC")[0]["PRE_ITM"]) + 1 : 1;

            return _ds;
        }
        /// <summary>
        /// 取得检验单信息
        /// </summary>
        /// <param name="tyId"></param>
        /// <param name="tyNo"></param>
        /// <param name="itmField"></param>
        /// <param name="bilItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string tyId, string tyNo,string itmField,int bilItm)
        {
            DbMRPTY _ty = new DbMRPTY(Comp.Conn_DB);
            return _ty.GetBody(tyId, tyNo, itmField, bilItm);
        }
        /// <summary>
        /// 取得转异常通知单记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        public SunlikeDataSet GetExportData(string sqlWhere)
        {
            DbMRPTY _dbTy = new DbMRPTY(Comp.Conn_DB);
            return _dbTy.GetExportData(sqlWhere);
        }
        #endregion

        #region 检查单据是否可以修改
        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="isCheckAuditing"></param>
        /// <param name="isRollBack"></param>
        public void SetCanModify(DataSet dataSet, bool isCheckAuditing, bool isRollBack)
        {
            DataTable _dtMf = dataSet.Tables["MF_TY"];
            DataTable _dtTf = dataSet.Tables["TF_TY"];
            if (_dtMf.Rows.Count > 0)
            {
                DataRow _drMf = _dtMf.Rows[0];
                bool _bCanModify = true;
                if ((!String.IsNullOrEmpty(_drMf["CLS_ID_OK"].ToString()) && _drMf["CLS_ID_OK"].ToString() == "T") 
                    || (!String.IsNullOrEmpty(_drMf["CLS_ID_LOST"].ToString())  && _drMf["CLS_ID_LOST"].ToString() == "T"))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(dataSet, "RCID=MNU.TF_TY.QTY_OK_RTN");
                }
                //判断关帐日
                if (Comp.HasCloseBill(Convert.ToDateTime(_drMf["TY_DD"]), _drMf["DEP"].ToString(), "CLS_MNU"))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(dataSet, "RCID=COMMON.HINT.CLOSE_CLS");
                }
                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(dataSet, "RCID=COMMON.HINT.CLOSE_LOCK");
                } 
                //是否存在异常单
               
                    MRPTR _tr = new MRPTR();
                    SunlikeDataSet _dsTr = _tr.GetDataByTy(_drMf["TY_ID"].ToString(), _drMf["TY_NO"].ToString());
                    if (_dsTr.Tables["TZERR"].Rows.Count > 0)
                    {
                        _bCanModify = false;
                        ////Common.SetCanModifyRem(dataSet, "RCID=MTN.HINT.TR_NOEXISTS");
                    }

               
                //是否存在转缴库单
                if (isCheckAuditing)
                {
                    DataRow[] _drSelBody = _dtTf.Select("ISNULL(QTY_OK_RTN,0) <> 0 ");
                    if (_drSelBody.Length > 0)                    
                    {
                        _bCanModify = false;
                        ////Common.SetCanModifyRem(dataSet, "RCID=MTN.HINT.MTN.HINT.QTY_OK_RTNEXISTS");
                    }
                }
                dataSet.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            }
        }
        #endregion        

        #region 保存验收单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="changedDs"></param>
        /// <param name="bubbleException"></param>        
        public DataTable UpdateData(string pgm, SunlikeDataSet changedDs, bool bubbleException)
        {
            DataTable _dtHead = changedDs.Tables["MF_TY"];
            DataTable _dtBody = changedDs.Tables["TF_TY"];
            #region 取得单据的审核状态
            if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = _dtHead.Rows[0]["USR"].ToString();
                _tyId = _dtHead.Rows[0]["TY_ID"].ToString();
            }
            else
            {
                _loginUsr = _dtHead.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
                _tyId = _dtHead.Rows[0]["TY_ID", System.Data.DataRowVersion.Original].ToString();
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
            //_isRunAuditing = _auditing.IsRunAuditing(_tyId, _loginUsr, _bilType,_mobID);
            #endregion

            Hashtable _ht = new Hashtable();
            _ht["MF_TY"] = "TY_ID,TY_NO,TY_DD,SAL_NO,CLOSE_ID,CUS_NO,TI_NO,REM,USR,CHK_MAN,PRT_SW,CPY_SW,BIL_NO,BIL_ID,CLS_DATE,BIL_TYPE,DEP,SYS_DATE,TI_ID,VOH_ID,VOH_NO";
            _ht["TF_TY"] = "TY_ID,TY_NO,ITM,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY_CHK,QTY_OK,QTY_LOST,REM,BIL_NO,TI_NO,TI_ITM,BAT_NO,MO_NO,CPY_SW,SPC_NO,PRC_ID,EST_ITM,PRE_ITM,QTY_PRE,ID_NO,QTY1_CHK,QTY1_OK,QTY1_LOST,CK_ITM,KEY_ITM";
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
                        _pgm = "MTNTPTR";
                    }
                    if (!string.IsNullOrEmpty(pgm))
                    {
                        _pgm = pgm;
                    }
                    DataTable _dtMf = changedDs.Tables["MF_TY"];
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
                this.SetCanModify(changedDs, true, false);
                changedDs.AcceptChanges();
                #endregion
            }
            else
            {
                #region 发生错误
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDs);
                    throw new SunlikeException("RCID=MRPTY.UpdateData() Error;" + _errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changedDs);
                }
                #endregion
            }
            return Sunlike.Business.BizObject.GetAllErrors(changedDs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            #region 单据追踪
            //DataTable _dt = ds.Tables["MF_TY"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["TY_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["TY_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            #endregion


        }
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
            string _tyNo = "", _tyID = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _tyNo = dr["TY_NO", DataRowVersion.Original].ToString();
                    _tyID = dr["TY_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    _tyNo = dr["TY_NO"].ToString();
                    _tyID = dr["TY_ID"].ToString();
                }
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "TY_ID = '" + _tyID + "' AND TY_NO = '" + _tyNo + "'";
                if (_Users.IsLocked("MF_TY", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            if (tableName == "MF_TY")
            {
                #region 关账
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["TY_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["TY_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion

                if (statementType == StatementType.Insert)
                {
                    #region --取单号
                    DateTime _dtTyDd = System.DateTime.Now;
                    if (dr["TY_DD"] is System.DBNull)
                    {
                        _dtTyDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _dtTyDd = Convert.ToDateTime(dr["TY_DD"]);
                    }
                    SQNO _sq = new SQNO();
                    dr["TY_NO"] = _sq.Set(dr["TY_ID"].ToString(), dr["USR"].ToString(), dr["DEP"].ToString(), _dtTyDd, dr["BIL_TYPE"].ToString());
                    dr["TY_DD"] = _dtTyDd.ToString(Comp.SQLDateFormat);
                    #endregion

                    #region 缺省设置
                    dr["PRT_SW"] = "N";
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    #endregion
                }
                if (statementType != StatementType.Delete)
                {
                    //业务员
                    string _salNo = dr["SAL_NO"].ToString();
                    if (!String.IsNullOrEmpty(_salNo))
                    {
                        Salm _salm = new Salm();
                        if (_salm.IsExist(_loginUsr, _salNo, Convert.ToDateTime(dr["TY_DD"])) == false)
                        {
                            dr.SetColumnError("SAL_NO",/*验货员不存在！*/"RCID=MTN.HINT.SAL_NO_EXIST,PARAM=" + _salNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
                if (statementType == StatementType.Delete)
                {
                    #region 删除时，判断是否转入异常单
                    MRPTR _tr = new MRPTR();
                    SunlikeDataSet _dsTr = _tr.GetDataByTy(dr["TY_ID",DataRowVersion.Original].ToString(), dr["TY_NO",DataRowVersion.Original].ToString());
                    if (_dsTr.Tables["TZERR"].Rows.Count > 0)
                    {
                        throw new SunlikeException(/*已产生单据，不能删除!*/ "RCID=MTN.HINT.HASNEXTBIL");
                    }
                    #endregion

                    #region 删除时，判断合格量和不合格量是否结案
                    if ((!String.IsNullOrEmpty(dr["CLS_ID_OK", DataRowVersion.Original].ToString()) && dr["CLS_ID_OK", DataRowVersion.Original].ToString() == "T")
                    || (!String.IsNullOrEmpty(dr["CLS_ID_LOST", DataRowVersion.Original].ToString()) && dr["CLS_ID_LOST", DataRowVersion.Original].ToString() == "T"))
                    {
                        throw new SunlikeException(/*已产生单据，不能删除!*/ "RCID=MTN.HINT.HASNEXTBIL");
                    }
                    #endregion
                }
                #region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["TY_DD"]);
                //    _aps.BIL_ID = dr["TY_ID"].ToString();
                //    _aps.BIL_NO = dr["TY_NO"].ToString();
                //    _aps.BIL_TYPE = "FX";
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = ""; //新加的部分，对应审核模板
                //}
                //else
                //    _aps = new AudParamStruct(Convert.ToString(dr["TY_ID", DataRowVersion.Original]), Convert.ToString(dr["TY_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                #endregion
            }
            else if (tableName == "TF_TY")
            {
                #region 缺省值
                if (statementType == StatementType.Insert)
                {
                    if (dr["PRD_MARK"] == System.DBNull.Value)
                    {
                        dr["PRD_MARK"] = "";
                    }
                }
                #endregion
            }

        }
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
            if (tableName == "MF_TY")
            {
                #region 删除单号
                if (statementType == StatementType.Delete)
                {
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(dr["TY_NO", DataRowVersion.Original].ToString(), _loginUsr);//删除时在BILD中插入一笔数据
                }
                #endregion
            }
            if (tableName == "TF_TY")
            {               
                if (!_isRunAuditing)
                {
                    this.UpdateQtyRtn(dr, false);
                }
            }
        }
       
        #endregion

        #region 回写送检单中的已验货量
        /// <summary>
        /// 回写送检单中的已验货量及制令单中的检验合格量及不合格量
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isRollBack"></param>
        public void UpdateQtyRtn(DataRow dr, bool isRollBack)
        {
            string _tiNo, _tiItm, _moNo,_unit;
            decimal _qty = 0, _qty1 = 0, _qtyOk = 0, _qtyLost = 0;
            if (dr.RowState != DataRowState.Deleted)
            {
                _tiNo = dr["TI_NO"].ToString();
                _tiItm = dr["TI_ITM"].ToString();
                _moNo = dr["BIL_NO"].ToString();
                _unit = dr["UNIT"].ToString();

                if (!String.IsNullOrEmpty(dr["QTY_LOST"].ToString()) && !String.IsNullOrEmpty(dr["PRC_ID"].ToString())
                    && (dr["PRC_ID"].ToString() == "1" || dr["PRC_ID"].ToString() == "2"))
                {
                    _qtyLost = Convert.ToDecimal(dr["QTY_LOST"]);
                }
                _qty = Convert.ToDecimal(dr["QTY_CHK"]);
                if (!String.IsNullOrEmpty(dr["QTY1_CHK"].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1_CHK"]);
                }
                _qtyOk = Convert.ToDecimal(dr["QTY_OK"]);

                if (dr.RowState == DataRowState.Modified)
                {
                    if (!String.IsNullOrEmpty(dr["QTY_CHK", DataRowVersion.Original].ToString()))
                    {
                        _qty -= Convert.ToDecimal(dr["QTY_CHK", DataRowVersion.Original]);
                    }
                    if (!String.IsNullOrEmpty(dr["QTY1_CHK", DataRowVersion.Original].ToString()))
                    {
                        _qty1 -= Convert.ToDecimal(dr["QTY1_CHK", DataRowVersion.Original]);
                    }
                    if (!String.IsNullOrEmpty(dr["QTY_OK", DataRowVersion.Original].ToString()))
                    {
                        _qtyOk -= Convert.ToDecimal(dr["QTY_OK", DataRowVersion.Original]);
                    }
                    if (!String.IsNullOrEmpty(dr["QTY_LOST", DataRowVersion.Original].ToString())
                        && (dr["PRC_ID", DataRowVersion.Original].ToString() == "1" || dr["PRC_ID", DataRowVersion.Original].ToString() == "2"))
                    {
                        _qtyLost -= Convert.ToDecimal(dr["QTY_LOST", DataRowVersion.Original]);
                    }
                }
                if (isRollBack)
                {
                    _qty *= -1;
                    _qty1 *= -1;
                    _qtyOk *= -1;
                    _qtyLost *= -1;
                }
            }
            else
            {
                _tiNo = dr["TI_NO", DataRowVersion.Original].ToString();
                _tiItm = dr["TI_ITM", DataRowVersion.Original].ToString();
                _moNo = dr["BIL_NO", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();

                if (!String.IsNullOrEmpty(dr["QTY_CHK", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY_CHK", DataRowVersion.Original]) * -1;
                }
                if (!String.IsNullOrEmpty(dr["QTY1_CHK", DataRowVersion.Original].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1_CHK", DataRowVersion.Original]) * -1;
                }
                if (!String.IsNullOrEmpty(dr["QTY_OK", DataRowVersion.Original].ToString()))
                {
                    _qtyOk = Convert.ToDecimal(dr["QTY_OK", DataRowVersion.Original]) * -1;
                }
                if (!String.IsNullOrEmpty(dr["QTY_LOST", DataRowVersion.Original].ToString()) && !String.IsNullOrEmpty(dr["PRC_ID", DataRowVersion.Original].ToString())
                    && (dr["PRC_ID", DataRowVersion.Original].ToString() == "1" || dr["PRC_ID", DataRowVersion.Original].ToString() == "2"))
                {
                    _qtyLost = Convert.ToDecimal(dr["QTY_LOST", DataRowVersion.Original]) * -1;
                }
            }
            //回写MF_MO的QTY_CHK,QTY_LOST
            MRPMO _mo = new MRPMO();
            _mo.UpdateQtyChk(_moNo,_unit, _qtyOk, _qtyLost);            

            //回写TF_TI.QTY_RTN
           DRPTI _ti = new DRPTI();
           _ti.UpdateQtyRtn("T6", _tiNo, _tiItm, _qty, _qty1);
        }
        #endregion

        #region 回写不合格数量的单据
        /// <summary>
        /// 产生不合格数量的单据
        /// </summary>
        /// <param name="tyId"></param>
        /// <param name="tyNo"></param>
        /// <param name="bilItm"></param>
        /// <param name="trNo"></param>
        /// <param name="qtyLostRtn"></param>
        public void UpdateBuildBil(string tyId, string tyNo, string bilItm, string trNo,decimal qtyLostRtn)
        {
            DbMRPTY _ty = new DbMRPTY(Comp.Conn_DB);
            //产生不合格数量
            _ty.UpdateBuildBil(tyId, tyNo, bilItm, trNo, qtyLostRtn);
        }
        #endregion

        #region 回写检验单的缴库单号及合格量
        /// <summary>
        /// 回写检验单的缴库单号及合格量
        /// </summary>
        /// <param name="tyId">检验单据别</param>
        /// <param name="tyNo">检验单号</param>
        /// <param name="bilItm">项次</param>
        /// <param name="mmNo">缴库单号</param>
        /// <param name="unit">已转合格量数量</param>
        /// <param name="qtyOkRtn">已转合格量</param>
        public void UpdateMmNo(string tyId, string tyNo, string bilItm, string mmId, string mmNo, string unit, decimal qtyOkRtn)
        {
            DbMRPTY _ty = new DbMRPTY(Comp.Conn_DB);
            _ty.UpdateMmNo(tyId, tyNo, bilItm, mmId, mmNo, qtyOkRtn);
        }
        #endregion

        #region 转单

        /// <summary>
        /// 转异常通知单
        /// </summary>
        /// <param name="dataSet"></param>
        public void UpdateFaultData(SunlikeDataSet dataSet)
        {
            MRPTR _mrpTr = new MRPTR();
            SunlikeDataSet _ds = _mrpTr.GetUpdateData("", "", "", true);
            if (!_ds.Tables["TZERR"].Columns.Contains("BIL_ITM"))
                _ds.Tables["TZERR"].Columns.Add("BIL_ITM");
            if (!_ds.Tables["TZERR"].Columns.Contains("BAR_NO"))
                _ds.Tables["TZERR"].Columns.Add("BAR_NO", System.Type.GetType("System.String"));
            if (!_ds.Tables["TZERR"].Columns.Contains("SPC_NO_LST"))
                _ds.Tables["TZERR"].Columns.Add("SPC_NO_LST", System.Type.GetType("System.String"));
            if (!_ds.Tables["TZERR"].Columns.Contains("REM_LST"))
                _ds.Tables["TZERR"].Columns.Add("REM_LST", System.Type.GetType("System.String"));

            int _count = 0;
            DataRow _dr;
            string _trNo = "";
            Query _query = new Query();
            SunlikeDataSet _dsFormula = new SunlikeDataSet();
            decimal _qty1 = 0;
            string _formula = "";
            foreach (DataRow dr in dataSet.Tables["TF_TY_EXPORT"].Rows)
            {
                _ds.Clear();
                _trNo = "TR" + Convert.ToString(_count++);
                _dr = _ds.Tables["TZERR"].NewRow();
                _dr["TR_NO"] = _trNo;
                _dr["TR_DD"] = DateTime.Today;
                _dr["QTY"] = dr["QTY_LOST"];

                #region 计算副单位数量
                if (dr["QTY_LOST"].ToString() != "")
                    _qty1 = Convert.ToDecimal(dr["QTY_LOST"]);

                try
                {
                    _dsFormula = _query.DoSQLString("select formula from prdt where prd_no='" + dr["PRD_NO"].ToString() + "'");
                    if (_dsFormula != null && _dsFormula.Tables.Count > 0 && _dsFormula.Tables[0].Rows.Count > 0)
                        _formula = _dsFormula.Tables[0].Rows[0][0].ToString();
                    string[] _str = _formula.Split(';');
                    if (_str.Length > 4)
                        _formula = _str[5].Remove(0, 5);
                    if (_formula.Length > 0)
                    {
                        _dsFormula = _query.DoSQLString("select " + _qty1.ToString() + _formula);
                        if (_dsFormula != null && _dsFormula.Tables.Count > 0 && _dsFormula.Tables[0].Rows.Count > 0)
                            _qty1 = Convert.ToDecimal(_dsFormula.Tables[0].Rows[0][0]);
                    }
                }
                catch
                {
                    throw new SunlikeException("主副单位换算公式错误。");
                }

                CompInfo _compInfo = Comp.GetCompInfo("");
                _dr["QTY1"] = Math.Round(_qty1, _compInfo.DecimalDigitsInfo.System.POI_QTY);
                #endregion

                _dr["MRP_NO"] = dr["PRD_NO"];
                _dr["PRD_MARK"] = dr["PRD_MARK"];
                _dr["UNIT"] = dr["UNIT"];
                _dr["DEP"] = dr["DEP"];
                _dr["SPC_NO"] = dr["SPC_NO"];                
                _dr["PRC_ID"] = dr["PRC_ID"];                
                _dr["PRC_ID2"] = dr["PRC_ID2"];
                _dr["BIL_ID"] = dr["TY_ID"];
                _dr["BIL_NO"] = dr["TY_NO"];
                _dr["MO_NO"] = dr["MO_NO"];
                _dr["ID_NO"] = dr["ID_NO"];
                _dr["USR_NO"] = dr["USR_NO"];
                _dr["WH"] = dr["WH"];
                _dr["PRT_SW"] = "F";
                _dr["CPY_SW"] = "F";
                _dr["USR"] = dr["USR"];
                _dr["CHK_MAN"] = dr["USR"];
                _dr["CLS_DATE"] = DateTime.Today;
                _dr["SYS_DATE"] = Convert.ToDateTime(DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                _dr["BIL_ITM"] = dr["PRE_ITM"];
                _dr["BAR_NO"] = dr["BAR_NO"];
                _dr["REM1"] = dr["REM"];
                _dr["SPC_NO_LST"] = dr["SPC_NO_LST"];
                _dr["REM_LST"] = dr["REM_LST"];
                _ds.Tables["TZERR"].Rows.Add(_dr);

                _dr = _ds.Tables["TF_TZERR"].NewRow();
                _dr["TR_NO"] = _trNo;
                if (_dr.Table.Columns.Contains("ITM"))
                {
                    _dr["ITM"] = _ds.Tables["TF_TZERR"].Rows.Count + 1;
                }
                if (_dr.Table.Columns.Contains("PRC_ID"))
                {
                    _dr["PRC_ID"] = dr["PRC_ID"];
                }
                _dr["ZC_NO"] = "";
                _dr["DEP"] = dr["DEP"];
                _dr["QTY"] = dr["QTY_LOST"];
                _dr["PRD_NO"] = dr["PRD_NO"];
                _dr["PRD_MARK"] = dr["PRD_MARK"];
                _dr["WH"] = dr["WH"];
                _dr["BAT_NO"] = dr["BAT_NO"];
                _ds.Tables["TF_TZERR"].Rows.Add(_dr);
                _mrpTr.UpdateData("",_ds, true);
            }
        }

        /// <summary>
        /// 转完修缴库单
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="containTime"></param>
        public void UpdatePassData(SunlikeDataSet dataSet, bool containTime)
        {
            Query _query = new Query();
            MRPMM _mrpMm = new MRPMM();
            SunlikeDataSet _ds = _mrpMm.GetUpdateData("", "MM", "", true);
            string _mmNo = "";

            //固品专案
            decimal _qty1 = 0;//副单位数量
            string _formula = "";//主副单位换算公式
            SunlikeDataSet _dsFormula = new SunlikeDataSet();
            //---------

            if (dataSet.Tables["TF_TY_EXPORT"].Rows.Count > 0)
            {
                DataRow _drFirst = dataSet.Tables["TF_TY_EXPORT"].Rows[0];
                
                #region 新增缴库单表头
                DataRow _drHead = _ds.Tables["MF_MM0"].NewRow();
                _drHead["MM_ID"] = "MM";
                _drHead["MM_NO"] = _mmNo;
                if (containTime)
                {
                    _drHead["MM_DD"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                }
                else
                {
                    _drHead["MM_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                }
                //_drHead["BIL_TYPE"] = _drFirst["BIL_TYPE"];
                _drHead["DEP"] = _drFirst["DEP"];
                _drHead["BIL_ID"] = _drFirst["TY_ID"];
                _drHead["BIL_NO"] = _drFirst["TY_NO"];
                if (dataSet.Tables["TF_TY_EXPORT"].Rows.Count == 1)
                {
                    _drHead["MO_NO"] = _drFirst["MO_NO"];
                }
                //固品专案一次只会缴一张制令单
                //else
                //{
                //    _drHead["MO_NO"] = _drFirst["MO_NO"].ToString() + "-" + dataSet.Tables["TF_TY_EXPORT"].Rows.Count.ToString();
                //}
                _drHead["PRT_SW"] = "N";
                _drHead["CPY_SW"] = "F";
                _drHead["USR"] = _drFirst["USR"];
                _drHead["USR_NO"] = _drFirst["USR_NO"];
                _drHead["CHK_MAN"] = _drFirst["USR"];
                _drHead["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                _drHead["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                _drHead["REM"] = _drFirst["REM"];
                _ds.Tables["MF_MM0"].Rows.Add(_drHead);
                #endregion

                #region 新增缴库单表身
                DataRow _drBody = null;
                foreach (DataRow dr in dataSet.Tables["TF_TY_EXPORT"].Rows)
                {
                    _drBody = _ds.Tables["TF_MM0"].NewRow();
                    _drBody["MM_ID"] = "MM";
                    _drBody["MM_NO"] = _mmNo;
                    _drBody["ITM"] = _ds.Tables["TF_MM0"].Rows.Count + 1;
                    _drBody["MM_DD"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                    _drBody["MO_NO"] = dr["MO_NO"];

                    //设置送修单号
                    //if (!string.IsNullOrEmpty(dr["MO_NO"].ToString()))
                    //{
                    //    MRPMO _mo = new MRPMO();
                    //    SunlikeDataSet _dsMo = _mo.GetUpdateData(dr["MO_NO"].ToString(), false);
                    //    if (_dsMo.Tables["MF_MO"].Rows.Count > 0)
                    //    {
                    //        _drBody["SO_NO"] = _dsMo.Tables["MF_MO"].Rows[0]["SO_NO"];
                    //    }
                    //}
                    _drBody["DEP"] = dr["DEP"];
                    _drBody["PRD_NO"] = dr["PRD_NO"];
                    _drBody["PRD_MARK"] = dr["PRD_MARK"];
                    _drBody["PRD_NAME"] = dr["PRD_NAME"];
                    _drBody["BAT_NO"] = dr["BAT_NO"];
                    _drBody["UNIT"] = dr["UNIT"];
                    _drBody["WH"] = dr["WH"];
                    _drBody["BIL_ID"] = dr["TY_ID"];
                    _drBody["BIL_NO"] = dr["TY_NO"];
                    _drBody["BIL_ITM"] = dr["PRE_ITM"];
                    _drBody["PRE_ITM"] = _drBody["ITM"];
                    _drBody["QTY"] = dr["QTY_OK"];

                    #region 副单位数量
                    if (dr["QTY_OK"].ToString() != "")
                        _qty1 = Convert.ToDecimal(dr["QTY_OK"]);

                    #region 通过主单位及公式推算副单位的值
                    try
                    {
                        _dsFormula = _query.DoSQLString("select formula from prdt where prd_no='" + dr["PRD_NO"].ToString() + "'");
                        if (_dsFormula != null && _dsFormula.Tables.Count > 0 && _dsFormula.Tables[0].Rows.Count > 0)
                            _formula = _dsFormula.Tables[0].Rows[0][0].ToString();
                        string[] _str = _formula.Split(';');
                        if (_str.Length > 4)
                            _formula = _str[5].Remove(0, 5);
                        if (_formula.Length > 0)
                        {
                            _dsFormula = _query.DoSQLString("select " + _qty1.ToString() + _formula);
                            if (_dsFormula != null && _dsFormula.Tables.Count > 0 && _dsFormula.Tables[0].Rows.Count > 0)
                                _qty1 = Convert.ToDecimal(_dsFormula.Tables[0].Rows[0][0]);
                        }
                    }
                    catch
                    {
                        throw new SunlikeException("主副单位换算公式错误。");
                    }
                    #endregion

                    CompInfo _compInfo = Comp.GetCompInfo("");
                    _drBody["QTY1"] = Math.Round(_qty1, _compInfo.DecimalDigitsInfo.System.POI_QTY);
                    #endregion

                    _drBody["ID_NO"] = dr["ID_NO"];
                    _drBody["REM"] = dr["REM"];
                    _ds.Tables["TF_MM0"].Rows.Add(_drBody);

                    //序列号记录
                    DataRow _drBar = null;
                    int _itm = 1;
                    //DataRow[] _drArrBar = dataSet.Tables["TOMM_BAR"].Select("MM_NO='" + dr["MM_NO"].ToString() + "'");
                    DataRow[] _drArrBar = dataSet.Tables["TOMM_BAR"].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "' AND ISNULL(PRD_MARK,'')='" + dr["PRD_MARK"].ToString() + "' ");
                    foreach (DataRow drTf in _drArrBar)
                    {
                        _drBar = _ds.Tables["TF_MM0_B"].NewRow();
                        _drBar["MM_ID"] = "MM";
                        _drBar["MM_NO"] = _mmNo;
                        _drBar["MM_ITM"] = _drBody["PRE_ITM"];//缴库单表身项次
                        _drBar["ITM"] = _itm++;//缴库序列号表项次
                        _drBar["PRD_NO"] = drTf["PRD_NO"];
                        _drBar["PRD_MARK"] = drTf["PRD_MARK"].ToString();
                        _drBar["BAR_CODE"] = drTf["BAR_CODE"];
                        _drBar["BOX_NO"] = drTf["BOX_NO"];
                        _ds.Tables["TF_MM0_B"].Rows.Add(_drBar);
                    }
                }
                #endregion

                _mrpMm.UpdateData("",_ds, true);
            }
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
            string _msg = "";
            try
            {
                DbMRPTY _dbMrpTy = new DbMRPTY(Comp.Conn_DB);
                _dbMrpTy.Approve(bil_id, bil_no, chk_man, cls_dd);

                //回写数量
                SunlikeDataSet _ds =this.GetUpdateData("", "", bil_id, bil_no, false);
                if (_ds.Tables["TF_TY"].Rows.Count > 0)
                {
                    foreach (DataRow dr in _ds.Tables["TF_TY"].Rows)
                    {
                        this.UpdateQtyRtn(dr, false);
                    }
                }
            }
            catch (Exception _ex)
            {
                _msg = _ex.Message;
            }
            return _msg;
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
        {//302079
            string _msg = "";
            try
            {
                DbMRPTY _dbMrpTy = new DbMRPTY(Comp.Conn_DB);
                _dbMrpTy.Rollback(bil_id, bil_no);

                //回写采购单数量                
                SunlikeDataSet _ds =this.GetUpdateData("", "", bil_id, bil_no, false);
                this.SetCanModify(_ds, false, true);
                if (_ds.Tables["TF_TY"].Rows.Count > 0)
                {
                    foreach (DataRow dr in _ds.Tables["TF_TY"].Rows)
                    {
                        this.UpdateQtyRtn(dr, true);
                    }
                }
            }
            catch (Exception _ex)
            {
                _msg = _ex.Message;
            }
            return _msg;
        }

        #endregion
    }
}