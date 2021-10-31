using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Sunlike.Business
{
    /// <summary>
    /// 维修项目制程规划
    /// </summary>
    public class MTNWBom : BizObject, IAuditing
    {
        #region Variablue
        private bool _isRunAuditing;
        private string _usr = String.Empty;
        #endregion

        #region Structure
        /// <summary>
        /// 
        /// </summary>
        public MTNWBom()
        {
        }
        #endregion

        #region GetData
        /// <summary>
        /// 取得配方数据
        /// </summary>
        /// <param name="usr">制单人</param>
        /// <param name="bom_No">BOM配方</param>
        /// <param name="onlyFillSchema">是否只读取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string bom_No, bool onlyFillSchema)
        {
            SunlikeDataSet _ds = this.GetData(bom_No, onlyFillSchema);

            if (!string.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //if (_ds.Tables.Contains("TF_WBOM"))
            //{
            //    DataTable _dtTf = _ds.Tables["TF_WBOM"];
            //    //整理货品规格
            //    _dtTf.Columns.Add("PRD_SPC");
            //    Prdt _prdt = new Prdt();
            //    foreach (DataRow _dr in _dtTf.Rows)
            //    {
            //        DataTable _dtPrdt = _prdt.GetPrdt(_dr["PRD_NO"].ToString());
            //        _dr["PRD_SPC"] = _dtPrdt.Rows[0]["SPC"].ToString();
            //    }
            //}
            //if (_ds.Tables.Contains("MF_WBOM"))
            //{
            //    DataTable _dtMf = _ds.Tables["MF_WBOM"];
            //    //带出表头货品规格
            //    _dtMf.Columns.Add("PRD_SPC");
            //    if (_dtMf.Rows.Count > 0)
            //    {
            //        Prdt _prdt = new Prdt();
            //        DataTable _dtPrdt = _prdt.GetPrdt(_dtMf.Rows[0]["PRD_NO"].ToString());
            //        _dtMf.Rows[0]["PRD_SPC"] = _dtPrdt.Rows[0]["SPC"].ToString();
            //    }
            //}
            this.SetCanModify(_ds, usr, true);
            return _ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bom_No"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bom_No, bool onlyFillSchema)
        {
            DbMTNWBom _dbMTN_BOM = new DbMTNWBom(Comp.Conn_DB);
            return _dbMTN_BOM.GetData(bom_No, onlyFillSchema);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bom_no"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bom_no)
        {
            DbMTNWBom _dbMTN_BOM = new DbMTNWBom(Comp.Conn_DB);
            return _dbMTN_BOM.GetData(bom_no);
        }
        /// <summary>
        /// 通过货品代号取得配方数据
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromMF(string prdNo)
        {
            DbMTNWBom _dbMTNBom = new DbMTNWBom(Comp.Conn_DB);
            return _dbMTNBom.GetDataFromMF(prdNo);
        }
        /// <summary>
        /// 通过货品代号取得配方数据
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromTF(string prdNo)
        {
            DbMTNWBom _dbMTNBom = new DbMTNWBom(Comp.Conn_DB);
            return _dbMTNBom.GetDataFromTF(prdNo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByIDNO(string idNo)
        {
            DbMTNWBom _dbMTN_BOM = new DbMTNWBom(Comp.Conn_DB);
            return _dbMTN_BOM.GetDataByIDNO(idNo);
        }
        /// <summary>
        /// 根据货品代号取标准配方
        /// </summary>
        /// <param name="prdNo">货品代号</param>
        /// <param name="isTop1">是否只取TOP 1</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByPrdNo(string prdNo, bool isTop1)
        {
            DbMTNWBom _dbMTN_BOM = new DbMTNWBom(Comp.Conn_DB);
            return _dbMTN_BOM.GetDataByPrdNo(prdNo, isTop1);
        }
        /// <summary>
        /// 从BOM表取数据
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBomByPrdt(string prdNo)
        {
            DbMTNWBom _dbMTN_BOM = new DbMTNWBom(Comp.Conn_DB);
            return _dbMTN_BOM.GetDataBomByPrdt(prdNo);
        }
        /// <summary>
        /// 从BOM表取数据
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBomByBomNo(string bomNo)
        {
            DbMTNWBom _dbMTN_BOM = new DbMTNWBom(Comp.Conn_DB);
            return _dbMTN_BOM.GetDataBomByBomNo(bomNo);
        }
        /// <summary>
        /// 从WBOM表中去数据
        /// </summary>
        /// <param name="sql">以and开头的SQLWHERE语句</param>
        /// <param name="sp">参数</param>
        /// <returns>ds</returns>
        public SunlikeDataSet GetDataBySqlWhere(string sql, SqlParameter[] sp)
        {
            DbMTNWBom _db = new DbMTNWBom(Comp.Conn_DB);
            return _db.GetDataBySqlWhere(sql, sp);
        }

        /// <summary>
        /// 从WBOM表中去数据
        /// </summary>
        /// <param name="bomNo">BOMNO</param>
        /// <param name="sql">以and开头的SQLWHERE语句</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBySqlWhere(string bomNo, string sql)
        {
            DbMTNWBom _db = new DbMTNWBom(Comp.Conn_DB);
            return _db.GetDataBySqlWhere(bomNo, sql);
        }
        #endregion

        #region Function
        /// <summary>
        /// 判断配方号是否存在
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public bool ChkExistsByPrdt(string prdNo)
        {
            SunlikeDataSet _ds = GetDataFromMF(prdNo);
            if (_ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断配方号是否存在 FOR ID_NO
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public bool ChkExistsForIdNo(string bomNo)
        {
            SunlikeDataSet _ds = GetDataByIDNO(bomNo);
            if (_ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断配方号是否存在 FOR BOM_NO
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public bool ChkExistsByBomNo(string bomNo)
        {
            DbMTNWBom _dbMTN_BOM = new DbMTNWBom(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMTN_BOM.GetDataMF(bomNo);
            if (_ds.Tables["MF_WBOM"].Rows.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bomNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="operType"></param>
        private void ModifyIDNO(string bomNo, string prdNo, string operType)
        {
            SunlikeDataSet _ds = GetDataFromTF(prdNo);
            if (operType == "ADD")
            {
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    if (string.IsNullOrEmpty(_dr["ID_NO"].ToString()))
                    {
                        _dr["ID_NO"] = bomNo;
                    }
                }
            }
            else
            {
                DataRow[] _drAry = _ds.Tables[0].Select(" ID_NO = '" + bomNo + "' AND BOM_NO <> '" + bomNo + "'");
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    if (_dr["ID_NO"].ToString() == bomNo)
                    {
                        _dr["ID_NO"] = DBNull.Value;
                    }
                }
            }
            if (_ds.GetChanges() != null)
            {
                Hashtable _ht = new Hashtable();
                _ht["TF_WBOM"] = "BOM_NO,ITM,PRD_NO,PRD_MARK,ID_NO,WH_NO,UNIT,QTY,REM";
                this.UpdateDataSet(_ds.GetChanges());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changedDS"></param>
        /// <param name="usr"></param>
        /// <param name="isCheckAuditing"></param>
        private void SetCanModify(SunlikeDataSet changedDS, string usr, bool isCheckAuditing)
        {
            if (changedDS.Tables["MF_WBOM"].Rows.Count > 0)
            {
                DataTable _dtMf = changedDS.Tables["MF_WBOM"];
                // 增加权限控管
                if (usr != null)
                {
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        Hashtable _billRight = Users.GetBillRight("MTNWBOM", usr, _bill_Dep, _bill_Usr);
                        changedDS.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDS.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDS.ExtendedProperties["PRN"] = _billRight["PRN"];
                    }
                }
                // 是否可以修改
                bool _canModify = true;
                Auditing _auditing = new Auditing();
                if (isCheckAuditing)
                    _canModify = _canModify && !_auditing.GetIfEnterAuditing("ZW", changedDS.Tables["MF_WBOM"].Rows[0]["BOM_NO"].ToString());
                changedDS.ExtendedProperties["CAN_MODIFY"] = _canModify.ToString().Substring(0, 1).ToUpper();
            }
        }
        /// <summary>
        /// 保存前的数据检测
        /// </summary>
        /// <param name="dr">datarow 数据行</param>
        /// <param name="status">状态</param>
        private void ChkDrData(DataRow dr, ref UpdateStatus status)
        {
            #region 检测
            //产品检测(必添)
            Prdt SunlikePrdt = new Prdt();
            string _prd_no = dr["PRD_NO"].ToString();
            if (!SunlikePrdt.IsExist(_usr, _prd_no))
            {
                dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prd_no);
                status = UpdateStatus.SkipAllRemainingRows;
            }
            //仓库(必添)
            string _wh = dr["WH_NO"].ToString();
            if (!string.IsNullOrEmpty(_wh))
            {
                WH SunlikeWH = new WH();
                if (!SunlikeWH.IsExist(_usr, _wh))
                {
                    dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                    status = UpdateStatus.SkipAllRemainingRows;
                }
            }
            //PMARK
            string _mark = dr["PRD_MARK"].ToString();
            int _prdMod = SunlikePrdt.CheckPrdtMod(_prd_no, _mark);
            if (_prdMod == 1)
            {
                dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _mark);//货品特征[{0}]不存在
                status = UpdateStatus.SkipAllRemainingRows;
            }
            else if (_prdMod == 2)
            {
                PrdMark _prd_Mark = new PrdMark();
                if (_prd_Mark.RunByPMark(_usr))
                {
                    string[] _prd_markAry = _prd_Mark.BreakPrdMark(_mark);
                    DataTable _markTable = _prd_Mark.GetSplitData("");
                    for (int i = 0; i < _markTable.Rows.Count; i++)
                    {
                        string _markName = _markTable.Rows[i]["FLDNAME"].ToString();
                        if (!_prd_Mark.IsExist(_markName, dr["PRD_NO"].ToString(), _prd_markAry[i]))
                        {
                            dr.SetColumnError("PRD_MARK",/*货品特征[{0}]不存在,请检查*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _mark);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region Save
        /// <summary>
        /// 保存维修项目制程规划
        /// </summary>
        /// <param name="changeDs"></param>
        /// <param name="bubbleException">是否希望抛出异常</param>
        public DataTable UpdateData(SunlikeDataSet changeDs, bool bubbleException)
        {
            DataTable _dtErr = null;
            Hashtable _ht = new Hashtable();
            _ht["MF_WBOM"] = "BOM_NO,PRD_NO,NAME,PRD_MARK,PF_NO,WH_NO,DEP,VALID_DD,END_DD,CST_EXP,CST_PRD,CST_MAN,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,SYS_DATE";
            _ht["TF_WBOM"] = "BOM_NO,ITM,PRD_NO,PRD_MARK,ID_NO,WH_NO,UNIT,QTY,REM,CST";
            //判断是否走审核流程
            DataRow _dr = changeDs.Tables["MF_WBOM"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
                _usr = _dr["USR", DataRowVersion.Original].ToString();
            else
                _usr = _dr["USR"].ToString();
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
            //_isRunAuditing = _auditing.IsRunAuditing("ZW", _usr, _bilType, _mobID);


            this.UpdateDataSet(changeDs, _ht);
            //判断单据能否修改
            if (!changeDs.HasErrors)
                this.SetCanModify(changeDs, _usr, true);
            else
            {
                _dtErr = GetAllErrors(changeDs);
                if (bubbleException)
                    throw new System.Exception(BizObject.GetErrorsString(changeDs));
            }
            return _dtErr;
        }

        /// <summary>
        /// 保存单据之前的动作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            #region SAVE处理
            if (tableName == "MF_WBOM")
            {
                if (statementType == StatementType.Insert)
                {
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    ChkDrData(dr, ref status);
                    //生成BOM_NO
                    string _bomNo = dr["PRD_NO"].ToString() + "->" + dr["PF_NO"].ToString();
                    //if (string.IsNullOrEmpty(dr["BOM_NO"].ToString()))
                    dr["BOM_NO"] = _bomNo;

                    //判断配方代号是否存在
                    if (ChkExistsByBomNo(dr["BOM_NO"].ToString()))
                    {
                        dr.SetColumnError("BOM_NO", "RCID=MTN.HINT.BOMNOEXISTS");//"此配方已经存在"
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //回写上层节点
                    ModifyIDNO(_bomNo, dr["PRD_NO"].ToString(), "ADD");
                }
                else if (statementType == StatementType.Update)
                {
                    ChkDrData(dr, ref status);
                }
                else if (statementType == StatementType.Delete)
                {
                    ModifyIDNO(dr["BOM_NO", DataRowVersion.Original].ToString(), dr["PRD_NO", DataRowVersion.Original].ToString(), "DEL");
                }
                #region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = DateTime.Now;
                //    _aps.BIL_ID = "ZW";
                //    _aps.BIL_NO = dr["BOM_NO"].ToString();
                //    _aps.BIL_TYPE = "";// "FX";
                //    _aps.CUS_NO = "";
                //    _aps.DEP = "";
                //    _aps.SAL_NO = "";
                //    _aps.USR = _usr;
                //    //_aps.MOB_ID = ""; //新加的部分，对应审核模板
                //}
                //else
                //    //_aps = new AudParamStruct("ZW", Convert.ToString(dr["BOM_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                #endregion
            }
            #endregion

        }

        protected override void BeforeDsSave(DataSet ds)
        {
            #region 单据追踪
            //DataTable _dt = ds.Tables["MF_WBOM"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();

            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "ZW");
            //}
            #endregion

            base.BeforeDsSave(ds);
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
            string _error = String.Empty;
            try
            {
                DbMTNWBom _dbMTNBom = new DbMTNWBom(Comp.Conn_DB);
                _dbMTNBom.UpdateChkMan(bil_no, chk_man, cls_dd);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
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
            string _error = String.Empty;
            try
            {
                DbMTNWBom _dbMTNBom = new DbMTNWBom(Comp.Conn_DB);
                _dbMTNBom.UpdateChkMan(bil_no, "", DateTime.Now);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }

        #endregion
    }
}
