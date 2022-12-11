using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Data;
using System.Collections;

namespace Sunlike.Business
{
    /// <summary>
    /// WC
    /// </summary>
    public class WC : BizObject
    {
        #region Variable
        private string _loginUsr;
        #endregion

        /// <summary>
        /// WC
        /// </summary>
        public WC()
        {
        }

        #region 取数据
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="wcNo">WC_NO</param>
        /// <param name="onlyFillSchema">BOOL</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string wcNo, bool onlyFillSchema)
        {
            DbWC _dbWc = new DbWC(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbWc.GetData(wcNo, onlyFillSchema);
            DataTable _dtMf = _ds.Tables["MF_WC"];
            DataTable _dtTf = _ds.Tables["TF_WC"];

            //if (!_dtTf.Columns.Contains("UNIT"))
            //{
            //    _dtTf.Columns.Add("UNIT");
            //    _dtTf.Columns["UNIT"].ReadOnly = false;
            //}

            string _cusNo = "";
            string _prdNo = "";

            if (_dtMf.Rows.Count > 0)
            {
                _cusNo = _dtMf.Rows[0]["CUS_NO"].ToString();
                _prdNo = _dtMf.Rows[0]["PRD_NO"].ToString();
            }
            //基本资料
            Cust _cust = new Cust();
            SunlikeDataSet _dsCust = new SunlikeDataSet();
            _dsCust.Merge(_cust.GetData(_cusNo));
            DataTable _dtCust = _dsCust.Tables["CUST"];
            _ds.Merge(_dtCust);
            Prdt _prdt = new Prdt();
            DataTable _dtPrdt = _prdt.GetPrdt(_prdNo);
            _dtPrdt.TableName = "PRDT";
            _ds.Merge(_dtPrdt);
            //维修记录
            DataTable wc_h = _dbWc.GetDataWcH(wcNo);
            _ds.Merge(wc_h);
            DataTable _dtPact = _dbWc.GetDataPact(wcNo);
            _ds.Merge(_dtPact);
            //合约记录
            return _ds;
        }

        /// <summary>
        /// 去缴库单资料
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromMM(string sqlWhere)
        {
            DbWC _dbWc = new DbWC(Comp.Conn_DB);
            return _dbWc.GetDataFromMM(sqlWhere);
        }
        /// <summary>
        /// 取得产品保修卡信息　取的字段有:WC_NO,PRD_NO,SPC,PRD_MARK,BAR_CODE,CUS_NO,BUY_DD,MTN_DD,RETURN_DD,NEED_DAYS[货品基础资料的前置天数]
        /// </summary>
        /// <param name="wcNo">产品保修卡</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForMA(string wcNo)
        {
            DbWC _wc = new DbWC(Comp.Conn_DB);
            return _wc.GetData(wcNo);
        }
        /// <summary>
        /// 取得产品保修卡信息　取的字段有:WC_NO,PRD_NO,SPC,PRD_MARK,BAR_CODE,CUS_NO,BUY_DD,MTN_DD,RETURN_DD,NEED_DAYS[货品基础资料的前置天数]
        /// </summary>
        /// <param name="wcNo">产品保修卡</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string wcNo)
        {
            DbWC _wc = new DbWC(Comp.Conn_DB);
            return _wc.GetData(wcNo);
        }
        /// <summary>
        /// GetDataByBarCodeList
        /// </summary>
        /// <param name="barCodeList"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByBarCodeList(string barCodeList)
        {
            DbWC _dbWc = new DbWC(Comp.Conn_DB);
            return _dbWc.GetDataByBarCodeList(barCodeList);
        }
        /// <summary>
        /// GetDataByBarCode
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByBarCode(string barCode, bool head)
        {
            DbWC _dbWc = new DbWC(Comp.Conn_DB);
            return _dbWc.GetDataByBarCode(barCode, head);
        }

        public SunlikeDataSet GetDataWcH(string wcNo)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            DbWC _dbWc = new DbWC(Comp.Conn_DB);
            using (DataTable _dt = _dbWc.GetDataWcH(wcNo))
            {
                using (DataColumn _dc = _dt.Columns["ITM"])
                {
                    _dc.AutoIncrement = true;
                    _dc.AutoIncrementSeed = _dc.AutoIncrementStep = 1;
                }
                _ds.Merge(_dt);
            }
            return _ds;
        }
        #endregion

        /// <summary>
        /// 取得保修卡的维修记录
        /// </summary>
        /// <param name="_bilID"></param>
        /// <param name="_bilNO"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataWcH(string _bilID, string _bilNO)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            DbWC _dbWc = new DbWC(Comp.Conn_DB);
            _ds = _dbWc.GetDataWcH(_bilID, _bilNO);
            return _ds;
        }

        #region 保存
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="throwException"></param>
        public void UpdateData(SunlikeDataSet dataSet, bool throwException)
        {
            DataTable _dtHead = dataSet.Tables["MF_WC"];
            DataTable _dtBody = dataSet.Tables["TF_WC"];
            #region 取得单据的录入人
            if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = _dtHead.Rows[0]["USR"].ToString();
            }
            else
            {
                _loginUsr = _dtHead.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
            }
            #endregion
            Hashtable _ht = new Hashtable();
            _ht["MF_WC"] = "WC_NO,PRD_NO,PRD_MARK,BAR_CODE,CUS_NO,BUY_DD,FILE_ID,MTN_ALL_ID,MTN_DD,RETURN_DD,WC_ID,REM,USR,SYS_DATE,CHK_MAN,CLS_DATE,LOCK_MAN,PRT_SW,PRT_USR,CPY_SW,DEP,SA_NO,SA_ITM,BIL_ID,STOP_DD";
            _ht["TF_WC"] = "WC_NO,ITM,PRD_NO,PRD_MARK,MTN_ALL_ID,STOP_DD,STOP_ID,BAR_CODE,REM";
            this.UpdateDataSet(dataSet, _ht);
            if (dataSet.HasErrors)
            {
                if (dataSet.ExtendedProperties.ContainsKey("BATCH_ERROR"))
                {
                    dataSet.ExtendedProperties["BATCH_ERROR"] = GetErrorsString(dataSet);
                }
                if (throwException)
                {
                    throw new SunlikeException(GetErrorsString(dataSet));
                }
            }
            else
            {
                dataSet.AcceptChanges();
            }
        }
        /// <summary>
        /// BeforeDsSave
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
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
            if (tableName == "MF_WC")
            {
                #region 表头信息检测
                if (statementType != System.Data.StatementType.Delete)
                {
                    //特征
                    Prdt _prdt = new Prdt();
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
                        if (_prd_Mark.RunByPMark(dr["USR"].ToString()))
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

                }
                else
                {
                    //删除判断保修卡是否已经使用
                    if (!string.IsNullOrEmpty(dr["WC_NO",DataRowVersion.Original].ToString()))
                    {
                        Pact _pact = new Pact();
                        SunlikeDataSet _dsPact = _pact.GetDataByWcNo(dr["WC_NO", DataRowVersion.Original].ToString());
                        if (_dsPact.Tables["CUS_PACT1"].Rows.Count > 0 )
                        {
                            string _pacNos =  "";
                            foreach(DataRow drPact in _dsPact.Tables["CUS_PACT1"].Rows)
                            {
                                if (!string.IsNullOrEmpty(_pacNos))
                                {
                                    _pacNos = ",";
                                }
                                _pacNos += drPact["PAC_NO"].ToString();
                            }
                            throw new SunlikeException("RCID=MTN.HINT.PACTEXISTS,PARAM=" + _pacNos);
                        }
                    }
                }
                #endregion
                if (statementType == StatementType.Insert)
                {
                    dr["CHK_MAN"] = dr["USR"];
                    dr["CLS_DATE"] = DateTime.Today;
                }
                if (statementType != StatementType.Delete)
                {
                    if (!String.IsNullOrEmpty(dr["BUY_DD"].ToString()))
                    {
                        dr["BUY_DD"] = Convert.ToDateTime(dr["BUY_DD"]).ToString(Comp.SQLDateFormat);
                    }
                    if (!String.IsNullOrEmpty(dr["MTN_DD"].ToString()))
                    {
                        dr["MTN_DD"] = Convert.ToDateTime(dr["MTN_DD"]).ToString(Comp.SQLDateFormat);
                    }
                    dr["SYS_DATE"] = Convert.ToDateTime(DateTime.Now.ToString(Comp.SQLDateTimeFormat));

                    #region 检查销货单号与PRD_NO是否匹配
                    string _bilId = dr["BIL_ID"].ToString();
                    string prd_no = Convert.ToString(dr["PRD_NO"]);
                    string saNo = Convert.ToString(dr["SA_NO"]);
                    string usr = Convert.ToString(dr["USR"]);
                    string cusNo = Convert.ToString(dr["CUS_NO"]);
                    if (string.Compare("SA",_bilId) == 0 && !string.IsNullOrEmpty(saNo))
                    {
                        bool isPass = false;
                        DRPSA sa = new DRPSA();
                        using (SunlikeDataSet ds = sa.GetData("", usr, "SA", saNo))
                        {
                            DataRow[] _drs = ds.Tables["TF_PSS"].Select("PRD_NO='" + prd_no + "'");
                            isPass = _drs.Length > 0;
                            if (isPass && !string.IsNullOrEmpty(cusNo))
                                isPass = ds.Tables["MF_PSS"].Select("CUS_NO='" + cusNo + "'").Length > 0;
                        }
                        if (!isPass)
                        {
                            dr.SetColumnError("SA_NO",/*销货单货品[{0}]与当前货品[{1}]不符*/"RCID=MTN.HINT.SAPRDTERROR1");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion
                }
                if (statementType == StatementType.Delete || statementType == StatementType.Update)
                {
                    if (dr["WC_ID", DataRowVersion.Original].ToString() != "0")
                    {
                        dr.SetColumnError("WC_ID", "RCID=COMMON.HINT.SA_NO_DELETE");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
            }
            else if (tableName == "TF_WC")
            {
                #region 表身信息检测
                if (statementType != System.Data.StatementType.Delete)
                {
                    //特征
                    Prdt _prdt = new Prdt();
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
                                    dr.SetColumnError(_markName,/*货品特征[{0}]不存在,请检查*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[i].Trim());
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 保存维修记录ds到数据库
        /// </summary>
        /// <param name="_ds"></param>
        public void UpdateDataWcH(SunlikeDataSet _ds) 
        {
            this.UpdateDataSet(_ds);
            if (_ds.HasErrors)
                throw new SunlikeException(GetErrorsString(_ds));
            else
                _ds.AcceptChanges();
        }
        #endregion

        #region 更新保修卡的截止日期
        /// <summary>
        /// 更新保修卡的截止日期
        /// </summary>
        /// <param name="wcNo"></param>
        /// <param name="stopDd"></param>
        public void UpdateStopDd(string wcNo,DateTime stopDd)
        {
            SunlikeDataSet _dsWc = this.GetData(wcNo, false);
            if (_dsWc.Tables["MF_WC"].Rows.Count == 0)
                return;
            DataRow[] _drBodys = _dsWc.Tables["TF_WC"].Select();
            foreach (DataRow drBody in _drBodys)
            {
                drBody["STOP_DD"] = stopDd;
            }
            this.UpdateData(_dsWc, true);
        }
        #endregion

    }
}