using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data;
using Sunlike.Business.Data;
using System.Collections;
using Sunlike.Common.Utility;
namespace Sunlike.Business
{
    /// <summary>
    /// 异常通知单
    /// </summary>
    public class MRPTR : BizObject, IAuditing
    {
        #region Variable

        private bool _isRunAuditing;
        private string _loginUsr;
        private bool _isUpdateQtyRk;

        #endregion
        /// <summary>
        /// 异常通知单
        /// </summary>
        public MRPTR()
        {
        }

        #region GetData
        /// <summary>
        ///  取数据
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="trNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string pgm, string usr, string trNo, bool onlyFillSchema)
        {
            DbMRPTR _dbTr = new DbMRPTR(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbTr.GetData(trNo, onlyFillSchema);
            if (!String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
                DataTable _dtHead = _ds.Tables["TZERR"];
                if (_dtHead.Rows.Count > 0)
                {
                    string _bill_Dep = _dtHead.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _dtHead.Rows[0]["USR"].ToString();
                    string _pgm = "";
                    if (string.IsNullOrEmpty(pgm))
                    {
                        _pgm = "MTNTZERR";
                    }
                    else
                    {
                        _pgm = pgm;
                    }
                    System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _bill_Dep, _bill_Usr);
                    _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                }
            }
            this.SetCanModify(_ds, true);
            return _ds;
        }
        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            DbMRPTR _dbTr = new DbMRPTR(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbTr.GetData(sqlWhere);
            _ds.Tables["TZERR"].Columns.Add("SLT");
            foreach (DataRow dr in _ds.Tables["TZERR"].Rows)
            {
                dr["SLT"] = "F";
            }

            StringBuilder _sb = new StringBuilder();
            foreach (DataRow dr in _ds.Tables["TZERR"].Rows)
            {
                if (!String.IsNullOrEmpty(_sb.ToString()))
                {
                    _sb.Append(",");
                }
                _sb.Append("'" + dr["TR_NO"].ToString() + "'");
            }
            if (String.IsNullOrEmpty(_sb.ToString()))
            {
                _sb.Append("''");
            }
            SunlikeDataSet _dsDetail = _dbTr.GetDetail(" AND TR_NO IN (" + _sb.ToString() + ") AND ISNULL(TR_NO, '') <> ''");
            _dsDetail.Tables["TF_TZERR"].Columns["ZC_NO"].DefaultValue = "";
            _ds.Merge(_dsDetail);
            _ds.Relations.Add("TZERRTF_TZERR", _ds.Tables["TZERR"].Columns["TR_NO"], _ds.Tables["TF_TZERR"].Columns["TR_NO"]);

            return _ds;
        }
        /// <summary>
        /// 通过验收单号取数据
        /// </summary>
        /// <param name="tyId"></param>
        /// <param name="tyNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByTy(string tyId, string tyNo)
        {
            DbMRPTR _dbTr = new DbMRPTR(Comp.Conn_DB);
            return _dbTr.GetDataByTy(tyId, tyNo);
        }

        #endregion

        #region  检查单据是否可以修改
        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            DataTable _dtHead = ds.Tables["TZERR"];
            DataTable _dtBody = ds.Tables["TF_TZERR"];
            string _usr = "";
            string errorMsg = "";
            bool _bCanModify = true;
            if (_dtHead.Rows.Count > 0)
            {
                _usr = _dtHead.Rows[0]["USR"].ToString();
                //判断关帐日
                if (_bCanModify && Comp.HasCloseBill(Convert.ToDateTime(_dtHead.Rows[0]["TR_DD"]), _dtHead.Rows[0]["DEP"].ToString(), "CLS_MNU"))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.CLOSE_CLS";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                }
                //判断审核流程
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing("TR", _dtHead.Rows[0]["TR_NO"].ToString()))
                    {
                        _bCanModify = false;
                        errorMsg += "COMMON.HINT.CLOSE_AUDIT";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtHead.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return errorMsg;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存异常通知单
        /// 扩张属性说明:
        /// UPDATE_QTY_RK 更新入库量 (是:F 否:T 缺省T)        
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="changedDs"></param>
        /// <param name="bubbleException"></param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, SunlikeDataSet changedDs, bool bubbleException)
        {
            DataTable _dtHead = changedDs.Tables["TZERR"];
            DataTable _dtBody = changedDs.Tables["TF_TZERR"];

            #region 取得单据的审核状态
            if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = _dtHead.Rows[0]["USR"].ToString();
            }
            else
            {
                _loginUsr = _dtHead.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
            }
            Auditing _auditing = new Auditing();
            DataRow dr = _dtHead.Rows[0];
            string _bilType = "";
            string _mobID = "";//支持直接终审mobID=@@ 则单据不跑审核流程
            if (dr.RowState == DataRowState.Deleted)
            {
                if (dr.Table.Columns.Contains("BIL_TYPE"))
                    _bilType = dr["BIL_TYPE", DataRowVersion.Original].ToString();
                if (dr.Table.Columns.Contains("MOB_ID"))
                    _mobID = dr["MOB_ID", DataRowVersion.Original].ToString();
            }
            else
            {
                if (dr.Table.Columns.Contains("BIL_TYPE"))
                    _bilType = dr["BIL_TYPE"].ToString();
                if (dr.Table.Columns.Contains("MOB_ID"))
                    _mobID = dr["MOB_ID"].ToString();
            }
            //_isRunAuditing = _auditing.IsRunAuditing("TR", _loginUsr, _bilType, _mobID);
            #endregion

            #region 判断是否需要修改入库量
            _isUpdateQtyRk = true;
            if (changedDs.ExtendedProperties.ContainsKey("UPDATE_QTY_RK"))
            {
                if (string.IsNullOrEmpty(changedDs.ExtendedProperties["UPDATE_QTY_RK"].ToString()) || string.Compare(changedDs.ExtendedProperties["UPDATE_QTY_RK"].ToString(), "F") == 0)
                {
                    _isUpdateQtyRk = false;
                }
            }
            #endregion

            Hashtable _ht = new Hashtable();
            _ht["TZERR"] = "TR_NO,TR_DD,TZ_NO,ZC_NO,QTY,DEP,SPC_NO,PRC_AD,PRC_ID,USR_NO,USR,"
                + "CHK_MAN,PRT_SW,CPY_SW,REM1,REM2,CLOSE_ID,MO_NO,MRP_NO,PRD_MARK,"
                + "BIL_BUILD,ID_NO,BIL_ID,BIL_NO,CLS_DATE,BAT_NO,BIL_TYPE,MOB_ID,"
                + "SYS_DATE,PRC_ID2,BIL_BUILD2,QTY1,UNIT,PRT_USR,ZC_PRD,PRD_ZC";
            _ht["TF_TZERR"] = "TR_NO,ITM,PRC_ID,ID_NO,ZC_NO,DEP,QTY,TZ_NO,PRD_NO,PRD_MARK,WH,BAT_NO,UP,STA_DD,END_DD";
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
                        _pgm = "MTNTZERR";
                    }
                    if (!string.IsNullOrEmpty(pgm))
                    {
                        _pgm = pgm;
                    }
                    DataTable _dtMf = changedDs.Tables["TZERR"];
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
                    throw new SunlikeException("RCID=MRPTR.UpdateData() Error;" + _errorMsg);
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
        /// 转异常处理单 （1、异常报废处理  2、异常重开维修单 3、异常强制缴库）
        /// </summary>
        /// <param name="tzErrDs">异常通知单信息</param>
        /// <param name="bilId"></param>
        /// <param name="exploreType">转单类型 0:按原单转 1:按配方转</param>
        /// <param name="isDelete"></param>
        public void UpdateDataBill(SunlikeDataSet tzErrDs, string bilId, string exploreType, bool isDelete)
        {
            this.EnterTransaction();
            try
            {
                SunlikeDataSet _dsBill = new SunlikeDataSet();
                Prdt _prdt = new Prdt();
                DataRow[] _dra = null;
                string _mmNo = "";
                if (bilId == "MM")
                {
                    #region 强制缴库
                    MRPMM _mrpMm = new MRPMM();
                    if (isDelete)
                    {
                        #region 删除以前缴库单
                        foreach (DataRow dr in tzErrDs.Tables["TZERR"].Select("SLT = 'T'"))
                        {
                            _dra = tzErrDs.Tables["TF_TZERR"].Select("TR_NO = '" + dr["TR_NO"].ToString() + "'");
                            _dsBill = _mrpMm.GetUpdateData("", "MM", dr["BIL_BUILD"].ToString(), false);
                            if (_dsBill.Tables["MF_MM0"].Rows.Count > 0)
                            {
                                DataRow[] _drSel = _dsBill.Tables["TF_MM0"].Select("BIL_ID='TR' AND BIL_NO='" + dr["TR_NO"].ToString() + "'");
                                if (_drSel.Length == _dsBill.Tables["TF_MM0"].Rows.Count)
                                {
                                    _dsBill.Tables["MF_MM0"].Rows[0].Delete();
                                }
                                else
                                {
                                    foreach (DataRow drBill in _drSel)
                                    {
                                        drBill.Delete();
                                    }
                                }
                                _mrpMm.UpdateData("", _dsBill, true);
                            }
                            else
                            {
                                throw new Exception("删除失败，找不到缴库单[" + dr["BIL_BUILD"].ToString() + "]");
                            }

                            dr["BIL_BUILD"] = DBNull.Value;
                            _dra[0]["TZ_NO"] = DBNull.Value;
                        }
                        #endregion
                    }
                    else
                    {
                        //新增表头
                        _dsBill = _mrpMm.GetUpdateData("", "MM", "", true);
                        //新增表头                        
                        #region 新增表头
                        DataRow[] _drHead = tzErrDs.Tables["TZERR"].Select("SLT = 'T'");
                        if (_drHead.Length > 0)
                        {
                            DataRow _drMf = _dsBill.Tables["MF_MM0"].NewRow();
                            _drMf["MM_ID"] = "MM";
                            _drMf["MM_NO"] = "";
                            _drMf["MM_DD"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                            _drMf["BIL_TYPE"] = _drHead[0]["BIL_TYPE"];
                            _drMf["DEP"] = _drHead[0]["DEP"];
                            _drMf["BIL_ID"] = "TR";
                            _drMf["BIL_NO"] = _drHead[0]["TR_NO"];
                            if (_drHead.Length == 1)
                            {
                                _drMf["MO_NO"] = _drHead[0]["MO_NO"];
                            }
                            else
                            {
                                _drMf["MO_NO"] = _drHead[0]["MO_NO"].ToString() + "-" + _drHead.Length.ToString();
                            }
                            _drMf["PRT_SW"] = "N";
                            _drMf["CPY_SW"] = "F";
                            _drMf["USR"] = _drHead[0]["USR"];
                            _drMf["USR_NO"] = _drHead[0]["USR_NO"];
                            _drMf["CHK_MAN"] = _drHead[0]["USR"];
                            _drMf["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            _drMf["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                            _drMf["REM"] = "异常强制缴库";
                            _dsBill.Tables["MF_MM0"].Rows.Add(_drMf);
                        }
                        #endregion

                        DataRow _drTf = null;
                        foreach (DataRow dr in tzErrDs.Tables["TZERR"].Select("SLT = 'T'"))
                        {
                            _dra = tzErrDs.Tables["TF_TZERR"].Select("TR_NO = '" + dr["TR_NO"].ToString() + "'");
                            if (_dra.Length > 0)
                            {
                                #region 货品特征等信息
                                string _prdMark = _dra[0]["PRD_MARK"].ToString();
                                int _prdMod = _prdt.CheckPrdtMod(_dra[0]["PRD_NO"].ToString(), _prdMark);
                                if (_prdMod == 1)
                                {
                                    throw new Exception("RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prdMark);
                                }
                                else if (_prdMod == 2)
                                {
                                    PrdMark _mark = new PrdMark();
                                    if (_mark.RunByPMark(dr["USR"].ToString()))
                                    {
                                        string[] _aryMark = _mark.BreakPrdMark(_prdMark);
                                        DataTable _dtMark = _mark.GetSplitData("");
                                        for (int i = 0; i < _dtMark.Rows.Count; i++)
                                        {
                                            string _fldName = _dtMark.Rows[i]["FLDNAME"].ToString();
                                            if (!_mark.IsExist(_fldName, _dra[0]["PRD_NO"].ToString(), _aryMark[i]))
                                            {
                                                throw new Exception("RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prdMark);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                _drTf = _dsBill.Tables["TF_MM0"].NewRow();
                                #region 新增表身
                                _drTf["MM_ID"] = "MM";
                                _drTf["MM_NO"] = "";
                                _drTf["ITM"] = _dsBill.Tables["TF_MM0"].Rows.Count + 1;
                                _drTf["MM_DD"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                _drTf["MO_NO"] = dr["MO_NO"];
                                //设置送修单号
                                if (!string.IsNullOrEmpty(dr["MO_NO"].ToString()))
                                {
                                    MRPMO _mo = new MRPMO();
                                    SunlikeDataSet _dsMo = _mo.GetUpdateData(dr["MO_NO"].ToString(), false);
                                    if (_dsMo.Tables["MF_MO"].Rows.Count > 0)
                                    {
                                        _drTf["SO_NO"] = _dsMo.Tables["MF_MO"].Rows[0]["SO_NO"];
                                    }
                                }
                                _drTf["DEP"] = dr["DEP"];
                                _drTf["PRD_NO"] = _dra[0]["PRD_NO"];
                                _drTf["PRD_MARK"] = _prdMark;
                                DataTable _dtPrdt = _prdt.GetFullPrdt(_drTf["PRD_NO"].ToString());
                                if (_dtPrdt.Rows.Count > 0)
                                {
                                    _drTf["PRD_NAME"] = _dtPrdt.Rows[0]["NAME"];
                                }
                                _drTf["BAT_NO"] = _dra[0]["BAT_NO"];
                                _drTf["UNIT"] = dr["UNIT"];
                                _drTf["WH"] = _dra[0]["WH"];
                                _drTf["BIL_ID"] = "TR";
                                _drTf["BIL_NO"] = dr["TR_NO"];
                                _drTf["QTY"] = dr["QTY"];
                                _drTf["QTY1"] = dr["QTY1"];
                                _drTf["ID_NO"] = dr["ID_NO"];
                                _drTf["REM"] = "异常强制缴库";
                                #endregion
                                _dsBill.Tables["TF_MM0"].Rows.Add(_drTf);
                            }
                        }
                        _mrpMm.UpdateData("MRPMM", _dsBill, true);
                        if (_dsBill.Tables["MF_MM0"].Rows.Count > 0)
                        {
                            _mmNo = _dsBill.Tables["MF_MM0"].Rows[0]["MM_NO"].ToString();
                        }
                    }
                    #endregion
                }
                foreach (DataRow dr in tzErrDs.Tables["TZERR"].Select("SLT = 'T'"))
                {
                    string _usr = dr["USR"].ToString();
                    _dra = tzErrDs.Tables["TF_TZERR"].Select("TR_NO = '" + dr["TR_NO"].ToString() + "'");
                    if (_dra.Length > 0)
                    {
                        string _prdMark = _dra[0]["PRD_MARK"].ToString();
                        int _prdMod = _prdt.CheckPrdtMod(_dra[0]["PRD_NO"].ToString(), _prdMark);
                        if (_prdMod == 1)
                        {
                            throw new Exception("RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prdMark);
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
                                    if (!_mark.IsExist(_fldName, _dra[0]["PRD_NO"].ToString(), _aryMark[i]))
                                    {
                                        throw new Exception("RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prdMark);
                                    }
                                }
                            }
                        }
                        if (bilId == "XF")
                        {
                            #region 报废单
                            DRPIJ _drpIj = new DRPIJ();
                            if (isDelete)
                            {
                                _dsBill = _drpIj.GetData(_usr, "XF", dr["BIL_BUILD"].ToString(), false);
                                if (_dsBill.Tables["MF_IJ"].Rows.Count > 0)
                                {
                                    _dsBill.Tables["MF_IJ"].Rows[0].Delete();
                                    _drpIj.UpdateData(null, _dsBill, true);
                                }
                                else
                                {
                                    throw new Exception("删除失败，找不到报废单[" + dr["BIL_BUILD"].ToString() + "]");
                                }
                                dr["BIL_BUILD"] = DBNull.Value;
                                _dra[0]["TZ_NO"] = DBNull.Value;
                            }
                            else
                            {
                                _dsBill = _drpIj.GetData(_usr, "XF", "", true);
                                DataRow _drMf = _dsBill.Tables["MF_IJ"].NewRow();
                                _drMf["IJ_ID"] = "XF";
                                _drMf["IJ_NO"] = "XF";
                                _drMf["IJ_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                                _drMf["DEP"] = dr["DEP"];
                                _drMf["MAN_NO"] = dr["USR_NO"];
                                _drMf["BIL_ID"] = "TR";
                                _drMf["BIL_NO"] = dr["TR_NO"];
                                _drMf["FIX_CST"] = "1";
                                _drMf["USR"] = dr["USR"];
                                _drMf["SYS_DATE"] = DateTime.Today.ToString(Comp.SQLDateTimeFormat);
                                if (tzErrDs.ExtendedProperties.ContainsKey("BILL_REM"))
                                {
                                    _drMf["REM"] = tzErrDs.ExtendedProperties["BILL_REM"];
                                }
                                _dsBill.Tables["MF_IJ"].Rows.Add(_drMf);

                                DataRow _drTf = _dsBill.Tables["TF_IJ"].NewRow();
                                _drTf["IJ_ID"] = "XF";
                                _drTf["IJ_NO"] = "XF";
                                _drTf["ITM"] = "1";
                                _drTf["IJ_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                                _drTf["PRD_NO"] = _dra[0]["PRD_NO"];
                                _drTf["PRD_MARK"] = _dra[0]["PRD_MARK"];
                                WH _wh = new WH();
                                SunlikeDataSet _dsWh = _wh.GetData(" AND WH='" + _dra[0]["WH"].ToString() + "'");
                                if (_dsWh.Tables["MY_WH"].Rows.Count > 0)
                                {
                                    if (string.Compare("T", _dsWh.Tables["MY_WH"].Rows[0]["INVALID"].ToString()) != 0)
                                    {
                                        throw new SunlikeException(/*不是废品仓,不允许保存!*/"RCID=SYS.MY_WH.ISNOTINVALID");
                                    }
                                }
                                _drTf["WH"] = _dra[0]["WH"];
                                _drTf["BAT_NO"] = _dra[0]["BAT_NO"];
                                _drTf["UNIT"] = dr["UNIT"];
                                _drTf["QTY"] = dr["QTY"];
                                _drTf["QTY1"] = dr["QTY1"];
                                _drTf["FIX_CST"] = "1";
                                _drTf["BIL_ID"] = "TR";
                                _drTf["BIL_NO"] = dr["TR_NO"];
                                StdCst _stdCst = new StdCst();
                                _drTf["CST_STD"] = Convert.ToDecimal(dr["QTY"]) * _stdCst.GetUP_STD(_dra[0]["PRD_NO"].ToString(), _dra[0]["PRD_MARK"].ToString(), _dra[0]["WH"].ToString(), DateTime.Today);
                                _drTf["CST"] = 0;
                                _dsBill.Tables["TF_IJ"].Rows.Add(_drTf);

                                _drpIj.UpdateData(null, _dsBill, true);

                                dr["BIL_BUILD"] = _dsBill.Tables["MF_IJ"].Rows[0]["IJ_NO"];
                                _dra[0]["TZ_NO"] = _dsBill.Tables["MF_IJ"].Rows[0]["IJ_NO"];
                            }

                            #endregion
                        }
                        else if (bilId == "MO")
                        {
                            #region 维修单
                            MRPMO _mo = new MRPMO();
                            string _bilBuildField = "BIL_BUILD";
                            if (dr["PRC_ID"].ToString() != "4")
                            {
                                _bilBuildField = "BIL_BUILD2";
                            }
                            if (isDelete)
                            {
                                #region 删除维修单
                                _dsBill = _mo.GetUpdateData(dr[_bilBuildField].ToString(), false);
                                if (_dsBill.Tables["MF_MO"].Rows.Count > 0)
                                {
                                    _dsBill.Tables["MF_MO"].Rows[0].Delete();
                                    _mo.UpdateData("", _dsBill, true);
                                }
                                else
                                {
                                    throw new Exception("删除失败，找不到维修单[" + dr["BIL_BUILD"].ToString() + "]");
                                }
                                #endregion

                                #region 反结案以前的制令单
                                //因检验单时已做结案动作，所以该步骤不应该做
                                //if (!string.IsNullOrEmpty(dr["MO_NO"].ToString()))
                                //{                                    
                                //    _mo.DoCloseBill("MO", dr["MO_NO"].ToString(), false, true);
                                //}
                                #endregion

                                dr[_bilBuildField] = DBNull.Value;
                                _dra[0]["TZ_NO"] = DBNull.Value;

                            }
                            else
                            {
                                #region 结案以前的制令单
                                //因检验单时已做结案动作，所以该步骤不应该做
                                //if (!string.IsNullOrEmpty(dr["MO_NO"].ToString())) 
                                //{
                                //    SunlikeDataSet _dsOldMo = _mo.GetData(dr["MO_NO"].ToString(), false);
                                //    if (_dsOldMo.Tables["MF_MO"].Rows.Count > 0)
                                //    {
                                //        DataRow[] _drSel = _dsOldMo.Tables["MF_MO"].Select("ISNULL(QTY_FIN,0)+ISNULL(QTY_LOST,0) >= ISNULL(QTY,0)");
                                //        if (_drSel.Length > 0)
                                //        {
                                //            _mo.DoCloseBill("MO", dr["MO_NO"].ToString(), true, true);
                                //        }
                                //    }                                    
                                //}
                                #endregion

                                #region 生成新的制令单
                                SunlikeDataSet _dsMo = null;
                                //0:按原单转 1:按配方转
                                if (exploreType == "1")
                                {
                                    //按配方号转
                                    #region 按配方号转
                                    MRPBom _mrpBom = new MRPBom();
                                    DataSet _dsBom = null;
                                    if (!string.IsNullOrEmpty(dr["ID_NO"].ToString()))
                                    {
                                        #region 取配方信息
                                        string _bomHeadName = "MF_BOM";
                                        string _bomBodyName = "TF_BOM";

                                        _dsBom = _mrpBom.GetData(dr["ID_NO"].ToString());
                                        if (_dsBom == null || (_dsBom != null && _dsBom.Tables["MF_BOM"].Rows.Count == 0))
                                        {
                                            _bomHeadName = "MF_WBOM";
                                            _bomBodyName = "TF_WBOM";
                                            MTNWBom _wBom = new MTNWBom();
                                            _dsBom = _wBom.GetData(dr["ID_NO"].ToString(), false);
                                        }
                                        #endregion

                                        //转换成标准配方
                                        _dsBom = _mrpBom.GetStdBom(_bomHeadName, _bomBodyName, dr["UNIT"].ToString(), _dsBom);

                                        #region 新增维修单
                                        if (_dsBom.Tables[_bomHeadName].Rows.Count > 0)
                                        {
                                            DataRow _drMfBom = _dsBom.Tables[_bomHeadName].Rows[0];
                                            _dsBill = _mo.GetUpdateData("", true);
                                            DataRow _drMf = _dsBill.Tables["MF_MO"].NewRow();
                                            _drMf["MO_NO"] = "MO";
                                            //设置送修单号
                                            if (!string.IsNullOrEmpty(dr["MO_NO"].ToString()))
                                            {
                                                _dsMo = _mo.GetUpdateData(dr["MO_NO"].ToString(), false);
                                                if (_dsMo.Tables["MF_MO"].Rows.Count > 0)
                                                {
                                                    _drMf["SO_NO"] = _dsMo.Tables["MF_MO"].Rows[0]["SO_NO"];
                                                }
                                            }
                                            _drMf["MO_DD"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                            _drMf["STA_DD"] = _dra[0]["STA_DD"];
                                            _drMf["END_DD"] = _dra[0]["END_DD"];
                                            _drMf["BIL_ID"] = "TR";
                                            _drMf["BIL_NO"] = dr["TR_NO"];
                                            _drMf["MRP_NO"] = _dra[0]["PRD_NO"];
                                            _drMf["PRD_MARK"] = _prdMark;
                                            _drMf["WH"] = _dra[0]["WH"];
                                            _drMf["UNIT"] = dr["UNIT"];
                                            _drMf["QTY"] = dr["QTY"];
                                            _drMf["QTY1"] = dr["QTY1"];
                                            _drMf["DEP"] = _dra[0]["DEP"];
                                            _drMf["CLOSE_ID"] = "F";
                                            _drMf["USR"] = dr["USR"];
                                            _drMf["BAT_NO"] = _dra[0]["BAT_NO"];
                                            if (tzErrDs.ExtendedProperties.ContainsKey("BILL_REM"))
                                            {
                                                _drMf["REM"] = tzErrDs.ExtendedProperties["BILL_REM"];
                                            }
                                            _drMf["EST_ITM"] = 0;
                                            _drMf["ID_NO"] = dr["ID_NO"];
                                            _drMf["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                                            if (_dsBom.Tables[_bomHeadName].Columns.Contains("CST_MAKE"))
                                            {
                                                _drMf["CST_MAKE"] = _drMfBom["CST_MAKE"];
                                            }
                                            if (_dsBom.Tables[_bomHeadName].Columns.Contains("CST_PRD"))
                                            {
                                                _drMf["CST_PRD"] = _drMfBom["CST_PRD"];
                                            }
                                            if (_dsBom.Tables[_bomHeadName].Columns.Contains("CST_OUT"))
                                            {
                                                _drMf["CST_OUT"] = _drMfBom["CST_OUT"];
                                            }
                                            if (_dsBom.Tables[_bomHeadName].Columns.Contains("CST_MAN"))
                                            {
                                                _drMf["CST_MAN"] = _drMfBom["CST_MAN"];
                                            }
                                            if (_dsBom.Tables[_bomHeadName].Columns.Contains("USED_TIME"))
                                            {
                                                _drMf["USED_TIME"] = _drMfBom["USED_TIME"];
                                            }
                                            if (_dsBom.Tables[_bomHeadName].Columns.Contains("CST"))
                                            {
                                                _drMf["CST"] = _drMfBom["CST"];
                                            }
                                            _drMf["CF_ID"] = "T";
                                            _drMf["QL_ID"] = "F";
                                            _drMf["Q2_ID"] = "F";
                                            _drMf["Q3_ID"] = "F";
                                            _drMf["ISSVS"] = "T";
                                            _dsBill.Tables["MF_MO"].Rows.Add(_drMf);

                                            DataRow _drTf = _dsBill.Tables["TF_MO"].NewRow();
                                            _drTf["MO_NO"] = "MO";
                                            _drTf["ITM"] = "1";
                                            _drTf["PRD_NO"] = _dra[0]["PRD_NO"];
                                            _drTf["PRD_NAME"] = "";
                                            _drTf["PRD_MARK"] = _prdMark;
                                            _drTf["WH"] = _dra[0]["WH"];
                                            _drTf["UNIT"] = dr["UNIT"];
                                            _drTf["QTY_RSV"] = dr["QTY"];
                                            _drTf["CST"] = 0.00;
                                            _drTf["BAT_NO"] = _dra[0]["BAT_NO"];
                                            _dsBill.Tables["TF_MO"].Rows.Add(_drTf);

                                            foreach (DataRow _drBom in _dsBom.Tables[_bomBodyName].Rows)
                                            {
                                                _drTf = _dsBill.Tables["TF_MO"].NewRow();
                                                _drTf["MO_NO"] = "MO";
                                                _drTf["ITM"] = _dsBill.Tables["TF_MO"].Select().Length + 1;
                                                _drTf["PRD_NO"] = _drBom["PRD_NO"];
                                                _drTf["PRD_NAME"] = _drBom["NAME"];
                                                _drTf["PRD_MARK"] = _drBom["PRD_MARK"];
                                                _drTf["WH"] = _drBom["WH_NO"];
                                                _drTf["UNIT"] = _drBom["UNIT"];
                                                _drTf["QTY_RSV"] = Convert.ToDecimal(_drBom["QTY"]) * Convert.ToDecimal(dr["QTY"]);
                                                //计算单位标准用量
                                                if (!string.IsNullOrEmpty(_drBom["QTY"].ToString()))
                                                {
                                                    //维修配方表身的单位用量/基数/表头维修数量
                                                    _drTf["QTY_STD"] = Convert.ToDecimal(_drBom["QTY"].ToString());
                                                }
                                                //维修配方表身记录的损耗率
                                                if (_drBom.Table.Columns.Contains("LOS_RTO"))
                                                {
                                                    _drTf["LOS_RTO"] = _drBom["LOS_RTO"];
                                                    decimal _deciLostRto = 0;
                                                    if (!string.IsNullOrEmpty(_drBom["LOS_RTO"].ToString()))
                                                        _deciLostRto = Convert.ToDecimal(_drBom["LOS_RTO"].ToString());
                                                    if (!string.IsNullOrEmpty(_drBom["QTY"].ToString()))
                                                    {
                                                        _drTf["QTY_LOST"] = Convert.ToDecimal(dr["QTY"]) * Convert.ToDecimal(_drBom["QTY"]) * _deciLostRto / 100;//应发量* 损耗率/100
                                                    }
                                                }
                                                if (!string.IsNullOrEmpty(_drBom["CST"].ToString()))
                                                {
                                                    _drTf["CST"] = Convert.ToDecimal(_drBom["CST"]) * Convert.ToDecimal(dr["QTY"]);
                                                }
                                                else
                                                {
                                                    _drTf["CST"] = 0.00;
                                                }
                                                _dsBill.Tables["TF_MO"].Rows.Add(_drTf);
                                            }

                                            _mo.UpdateData("", _dsBill, true);

                                            dr[_bilBuildField] = _dsBill.Tables["MF_MO"].Rows[0]["MO_NO"];
                                            _dra[0]["TZ_NO"] = _dsBill.Tables["MF_MO"].Rows[0]["MO_NO"];
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        throw new SunlikeException("RCID=MTN.HINT.TRNOBOM");
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 按原单转
                                    _dsMo = _mo.GetUpdateData(dr["MO_NO"].ToString(), false);
                                    if (_dsMo.Tables["MF_MO"].Rows.Count > 0)
                                    {
                                        string _prdNo = dr["MRP_NO"].ToString();                            //产品
                                        string _unit = dr["UNIT"].ToString();                               //产品单位
                                        string _unitMo = _dsMo.Tables["MF_MO"].Rows[0]["UNIT"].ToString();  //原单单位
                                        decimal _qtyTzErr = 0;                                              //异常数量
                                        decimal _qtyRto = 1;                                                //换算比率
                                        if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                                            _qtyTzErr = Convert.ToDecimal(dr["QTY"]);
                                        //转换成维修单单位
                                        _qtyTzErr = _prdt.GetUnitQty(_prdNo, _unit, _qtyTzErr, _unitMo);
                                        //取得换算比率
                                        if (!string.IsNullOrEmpty(_dsMo.Tables["MF_MO"].Rows[0]["QTY"].ToString()) && Convert.ToDecimal(_dsMo.Tables["MF_MO"].Rows[0]["QTY"]) != 0)
                                        {
                                            _qtyRto = _qtyTzErr / Convert.ToDecimal(_dsMo.Tables["MF_MO"].Rows[0]["QTY"]);
                                        }
                                        #region 新增维修单
                                        _dsBill = _mo.GetUpdateData("", true);
                                        DataRow _drMf = _dsBill.Tables["MF_MO"].NewRow();

                                        #region 添加表头
                                        DataRow _drMfMo = _dsMo.Tables["MF_MO"].Rows[0];
                                        _drMf["MO_NO"] = "MO";
                                        _drMf["MO_DD"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                        //设置送修单号
                                        if (!string.IsNullOrEmpty(dr["MO_NO"].ToString()))
                                        {
                                            if (_dsMo.Tables["MF_MO"].Rows.Count > 0)
                                            {
                                                _drMf["SO_NO"] = _dsMo.Tables["MF_MO"].Rows[0]["SO_NO"];
                                                _drMf["EST_ITM"] = _dsMo.Tables["MF_MO"].Rows[0]["EST_ITM"];
                                                _drMf["CUS_NO"] = _dsMo.Tables["MF_MO"].Rows[0]["CUS_NO"];
                                            }
                                        }
                                        _drMf["STA_DD"] = _dra[0]["STA_DD"];
                                        _drMf["END_DD"] = _dra[0]["END_DD"];
                                        _drMf["BIL_ID"] = "TR";
                                        _drMf["BIL_NO"] = dr["TR_NO"];
                                        _drMf["MRP_NO"] = _dra[0]["PRD_NO"];
                                        _drMf["PRD_MARK"] = _prdMark;
                                        _drMf["WH"] = _dra[0]["WH"];
                                        _drMf["UNIT"] = dr["UNIT"];
                                        _drMf["QTY"] = dr["QTY"];
                                        _drMf["QTY1"] = dr["QTY1"];
                                        _drMf["DEP"] = _dra[0]["DEP"];
                                        _drMf["CLOSE_ID"] = "F";
                                        _drMf["USR"] = dr["USR"];
                                        _drMf["BAT_NO"] = _dra[0]["BAT_NO"];
                                        if (tzErrDs.ExtendedProperties.ContainsKey("BILL_REM"))
                                        {
                                            _drMf["REM"] = tzErrDs.ExtendedProperties["BILL_REM"];
                                        }
                                        _drMf["ID_NO"] = dr["ID_NO"];
                                        _drMf["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                                        _drMf["CST_MAKE"] = _drMfMo["CST_MAKE"];
                                        _drMf["CST_PRD"] = _drMfMo["CST_PRD"];
                                        _drMf["CST_OUT"] = _drMfMo["CST_OUT"];
                                        _drMf["CST_MAN"] = _drMfMo["CST_MAN"];
                                        _drMfMo["USED_TIME"] = _drMfMo["USED_TIME"];
                                        _drMf["CST"] = _drMfMo["CST"];
                                        _drMf["CF_ID"] = _drMfMo["CF_ID"];
                                        _drMf["Q2_ID"] = _drMfMo["Q2_ID"];
                                        _drMf["Q2_ID"] = _drMfMo["Q2_ID"];
                                        _drMf["Q3_ID"] = _drMfMo["Q3_ID"];
                                        _drMf["ISSVS"] = _drMfMo["ISSVS"];
                                        _dsBill.Tables["MF_MO"].Rows.Add(_drMf);
                                        #endregion

                                        #region 添加表身
                                        DataRow _drTf = null;
                                        foreach (DataRow _drMo in _dsMo.Tables["TF_MO"].Rows)
                                        {
                                            _drTf = _dsBill.Tables["TF_MO"].NewRow();
                                            _drTf["MO_NO"] = "MO";
                                            _drTf["ITM"] = _dsBill.Tables["TF_MO"].Select().Length + 1;
                                            _drTf["PRD_NO"] = _drMo["PRD_NO"];
                                            _drTf["PRD_NAME"] = _drMo["PRD_NAME"];
                                            _drTf["PRD_MARK"] = _drMo["PRD_MARK"];
                                            _drTf["WH"] = _drMo["WH"];
                                            _drTf["UNIT"] = _drMo["UNIT"];
                                            _drTf["QTY_RSV"] = _qtyRto * Convert.ToDecimal(_drMo["QTY_RSV"]);
                                            //计算单位标准用量
                                            _drTf["QTY_STD"] = _drMo["QTY_STD"];
                                            //维修配方表身记录的损耗率
                                            _drTf["LOS_RTO"] = _drMo["LOS_RTO"];
                                            decimal _deciLostRto = 0;
                                            if (!string.IsNullOrEmpty(_drMo["LOS_RTO"].ToString()))
                                                _deciLostRto = Convert.ToDecimal(_drMo["LOS_RTO"].ToString());
                                            if (!string.IsNullOrEmpty(_drMo["QTY_LOST"].ToString()))
                                            {
                                                _drTf["QTY_LOST"] = _qtyRto * Convert.ToDecimal(_drMo["QTY_LOST"]);
                                            }
                                            if (!string.IsNullOrEmpty(_drMo["CST"].ToString()))
                                            {
                                                _drTf["CST"] = _qtyRto * Convert.ToDecimal(_drMo["CST"]);
                                            }
                                            else
                                            {
                                                _drTf["CST"] = 0.00;
                                            }
                                            _dsBill.Tables["TF_MO"].Rows.Add(_drTf);
                                        }
                                        #endregion

                                        _mo.UpdateData("", _dsBill, true);

                                        dr[_bilBuildField] = _dsBill.Tables["MF_MO"].Rows[0]["MO_NO"];
                                        _dra[0]["TZ_NO"] = _dsBill.Tables["MF_MO"].Rows[0]["MO_NO"];
                                        #endregion
                                    }
                                    else
                                    {
                                        throw new SunlikeException("RCID=MTN.HINT.TRNOMO");
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else if (bilId == "MM")
                        {
                            #region 强制缴库
                            dr["BIL_BUILD"] = _mmNo;
                            _dra[0]["TZ_NO"] = _mmNo;
                            #endregion
                        }
                    }

                    if ((dr["PRC_ID"].ToString() == "4" && !String.IsNullOrEmpty(dr["BIL_BUILD"].ToString()))
                        || (dr["PRC_ID"].ToString() != "4" && dr["PRC_ID2"].ToString() == "1" && !String.IsNullOrEmpty(dr["BIL_BUILD"].ToString()))
                        || (dr["PRC_ID"].ToString() != "4" && dr["PRC_ID2"].ToString() != "1" && !String.IsNullOrEmpty(dr["BIL_BUILD"].ToString()) && !String.IsNullOrEmpty(dr["BIL_BUILD2"].ToString())))
                    {
                        dr["CLOSE_ID"] = "T";
                    }
                    else
                    {
                        dr["CLOSE_ID"] = "F";
                    }
                }
                if (!tzErrDs.ExtendedProperties.ContainsKey("UPDATE_QTY_RK"))
                    tzErrDs.ExtendedProperties.Add("UPDATE_QTY_RK", "F");
                this.UpdateData("", tzErrDs, true);
            }
            catch (Exception _ex)
            {
                this.SetAbort();
                throw _ex;
            }
            finally
            {
                this.LeaveTransaction();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //因目前修改货品分仓存量的入库量需要取两表中数据
            //故放在此处2007-11-02
            #region 修改入库量
            if (!_isRunAuditing)
            {
                DataTable _dtHead = ds.Tables["TZERR"];
                if (_dtHead.Rows.Count > 0)
                {
                    //取来源单号
                    string _bilId = "";
                    string _tzNo = "";
                    if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
                    {
                        _bilId = _dtHead.Rows[0]["BIL_ID"].ToString();
                    }
                    else
                    {
                        _bilId = _dtHead.Rows[0]["BIL_ID", DataRowVersion.Original].ToString();
                    }
                    //修改入库量
                    if (_bilId == "TP")
                    {
                        if (_isUpdateQtyRk)
                        {
                            if (_dtHead.Rows[0].RowState == DataRowState.Deleted)
                            {
                                this.UpdateQtyRk(ds, true);
                            }
                            else if (_dtHead.Rows[0].RowState == DataRowState.Modified)
                            {
                                this.UpdateQtyRk(ds, true);
                                this.UpdateQtyRk(ds, false);
                            }
                            else if (_dtHead.Rows[0].RowState == DataRowState.Added)
                            {
                                this.UpdateQtyRk(ds, false);
                            }
                        }
                    }
                }
            }
            #endregion

            base.BeforeDsSave(ds);
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
            string _trNo = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _trNo = dr["TR_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _trNo = dr["TR_NO"].ToString();
                }
                //如果不是转异常处理单,判断是否锁单
                if (!dr.Table.DataSet.ExtendedProperties.ContainsKey("UPDATE_QTY_RK"))
                {
                    //判断是否锁单，如果已经锁单则不让修改。
                    Users _Users = new Users();
                    string _whereStr = "TR_NO = '" + _trNo + "'";
                    if (_Users.IsLocked("TZERR", _whereStr))
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                    }
                }
            }
            #endregion
            if (tableName == "TZERR")
            {
                #region --生成单号
                if (statementType == StatementType.Insert)
                {
                    SQNO SunlikeSqNo = new SQNO();
                    DateTime _dtTrDd = System.DateTime.Now;
                    if (dr["TR_DD"] is System.DBNull)
                    {
                        _dtTrDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _dtTrDd = Convert.ToDateTime(dr["TR_DD"]);
                    }
                    string _trNoAdd = SunlikeSqNo.Set("TR", _loginUsr, dr["DEP"].ToString(), _dtTrDd, dr["BIL_TYPE"].ToString());
                    dr["TR_NO"] = _trNoAdd;
                    dr["TR_DD"] = _dtTrDd.ToString(Comp.SQLDateFormat);
                    dr["PRT_SW"] = "N";

                    //记录异常序列号
                    string[] _barStr = dr["BAR_NO"].ToString().Split(';');
                    string[] _spcStr = dr["SPC_NO_LST"].ToString().Split(';');
                    string[] _remStr = dr["REM_LST"].ToString().Split(';');
                    if (_barStr.Length == _spcStr.Length && _spcStr.Length == _remStr.Length)
                    {
                        for (int i = 0; i < _barStr.Length; i++)
                        {
                            UpdateBarCode(_barStr[i], _spcStr[i], _remStr[i], _trNoAdd);
                        }
                    }
                }
                #endregion
                if (statementType != StatementType.Delete)
                {
                    dr["SYS_DATE"] = Convert.ToDateTime(DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                }

                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["TR_DD"]);
                //    _aps.BIL_ID = "TR";
                //    _aps.BIL_NO = dr["TR_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = "";
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["USR_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = ""; //新加的部分，对应审核模板
                //}
                //else
                //    _aps = new AudParamStruct("TR", Convert.ToString(dr["TR_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
            else if (tableName == "TF_TZERR")
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
            if (tableName == "TZERR")
            {
                #region 删除单号
                if (statementType == StatementType.Delete)
                {
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(dr["TR_NO", DataRowVersion.Original].ToString(), _loginUsr);//删除时在BILD中插入一笔数据
                }
                #endregion

                string _bilId = "";

                if (statementType != StatementType.Delete)
                {
                    _bilId = dr["BIL_ID"].ToString();
                }
                else
                {
                    _bilId = dr["BIL_ID", DataRowVersion.Original].ToString();
                }

                if (_bilId == "TP")
                {
                    MRPTY _ty = new MRPTY();
                    decimal _qtyLostRto = 0;
                    if (statementType != StatementType.Delete)
                    {
                        if (dr.Table.Columns.Contains("BIL_ITM"))
                        {
                            if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                            {
                                _qtyLostRto = Convert.ToDecimal(dr["QTY"].ToString());
                            }
                            _ty.UpdateBuildBil(_bilId, dr["BIL_NO"].ToString(), dr["BIL_ITM"].ToString(), dr["TR_NO"].ToString(), _qtyLostRto);
                            if (statementType == StatementType.Update)
                            {
                                if (!string.IsNullOrEmpty(dr["BIL_ITM", DataRowVersion.Original].ToString()))
                                {
                                    _ty.UpdateBuildBil(_bilId, dr["BIL_NO", DataRowVersion.Original].ToString(), dr["BIL_ITM", DataRowVersion.Original].ToString(), dr["TR_NO", DataRowVersion.Original].ToString(), (-1) * _qtyLostRto);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                        {
                            _qtyLostRto = Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                        }
                        _ty.UpdateBuildBil(_bilId, dr["BIL_NO", DataRowVersion.Original].ToString(), "", dr["TR_NO", DataRowVersion.Original].ToString(), (-1) * _qtyLostRto);
                    }
                }

                #region 回写制令单重工报废量
                DbMRPTR _tr = new DbMRPTR(Comp.Conn_DB);
                if (statementType == StatementType.Insert)
                {
                    if (!string.IsNullOrEmpty(dr["QTY"].ToString()) && !string.IsNullOrEmpty(dr["MO_NO"].ToString()))
                    {
                        string _moNo = dr["MO_NO"].ToString();
                        decimal _qtyLost = Convert.ToDecimal(dr["QTY"]);
                        _tr.UpdateMOQtyLost(_moNo, _qtyLost);
                    }
                }
                #endregion
            }
        }
        #endregion

        #region 修改入库量
        /// <summary>
        /// 修改入库量
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isRollBack"></param>
        private void UpdateWh(DataRow dr, bool isRollBack)
        {
            DataRow _drMf = dr.Table.DataSet.Tables["TZERR"].Rows[0];
            DataRow _drTf = dr.Table.DataSet.Tables["TF_TZERR"].Rows[0];
            string _prdNo, _prdMark, _whNo, _unit, _batNo;
            decimal _qty;
            Hashtable _ht = new Hashtable();
            WH _wh = new WH();
            if (dr.RowState != DataRowState.Added && dr.RowState != DataRowState.Unchanged)
            {
                _prdNo = _drMf["MRP_NO", DataRowVersion.Original].ToString();
                _prdMark = _drMf["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = _drTf["WH", DataRowVersion.Original].ToString();
                _unit = _drMf["UNIT", DataRowVersion.Original].ToString();
                _batNo = _drTf["BAT_NO", DataRowVersion.Original].ToString();
                _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);

                _ht[WH.QtyTypes.QTY_RK] = _qty;
                if (!String.IsNullOrEmpty(_batNo))
                {
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                }
                else
                {
                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
                }
            }
            if (dr.RowState != DataRowState.Deleted)
            {
                _prdNo = _drMf["MRP_NO"].ToString();
                _prdMark = _drMf["PRD_MARK"].ToString();
                _whNo = _drTf["WH"].ToString();
                _unit = _drMf["UNIT"].ToString();
                _batNo = _drTf["BAT_NO"].ToString();
                _qty = Convert.ToDecimal(_drMf["QTY"]);
                if (!isRollBack)
                {
                    _qty *= -1;
                }

                _ht[WH.QtyTypes.QTY_RK] = _qty;
                if (!String.IsNullOrEmpty(_batNo))
                {
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                }
                else
                {
                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
                }
            }
        }
        /// <summary>
        /// 修改入库量
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isDel"></param>
        private void UpdateQtyRk(DataSet ds, bool isDel)
        {
            DataRow _drHead = ds.Tables["TZERR"].Rows[0];
            DataRow _drBody = ds.Tables["TF_TZERR"].Rows[0];
            string _prdNo, _prdMark, _whNo, _unit, _batNo;
            decimal _qty = 0;
            WH _wh = new WH();
            Hashtable _ht = new Hashtable();
            if (isDel)
            {
                _prdNo = _drHead["MRP_NO", DataRowVersion.Original].ToString();
                _prdMark = _drHead["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = _drHead["WH", DataRowVersion.Original].ToString();
                _unit = _drHead["UNIT", DataRowVersion.Original].ToString();
                _batNo = _drBody["BAT_NO", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(_drHead["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(_drHead["QTY", DataRowVersion.Original]);
                }
                _ht[WH.QtyTypes.QTY_RK] = _qty;
                if (!String.IsNullOrEmpty(_batNo))
                {
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                }
                else
                {
                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
                }
            }
            else
            {
                _prdNo = _drHead["MRP_NO"].ToString();
                _prdMark = _drHead["PRD_MARK"].ToString();
                _whNo = _drHead["WH"].ToString();
                _unit = _drHead["UNIT"].ToString();
                _batNo = _drBody["BAT_NO"].ToString();
                if (!string.IsNullOrEmpty(_drHead["QTY"].ToString()))
                {
                    _qty = (-1) * Convert.ToDecimal(_drHead["QTY"]);
                }
                _ht[WH.QtyTypes.QTY_RK] = _qty;
                if (!String.IsNullOrEmpty(_batNo))
                {
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                }
                else
                {
                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
                }
            }
        }
        #endregion

        #region 记录异常序列号
        private void UpdateBarCode(string _barNo, string _spcNo, string _rem, string _bilNo)
        {
            string _sql = "if (select count(*) from " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC where BAR_NO='" + _barNo + "')=0 \n"
                        + "     INSERT INTO " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC (BAR_NO,QC,STATUS,SPC_NO,BAT_NO,BIL_ID,BIL_NO,REM) VALUES ('" + _barNo + "','2','TR','" + _spcNo + "',null,'TR','" + _bilNo + "','" + _rem + "') \n"
                        + " else \n"
                        + "     UPDATE " + Comp.DRP_Prop["BarPrintDB"].ToString() + "..BAR_QC SET QC='2',STATUS='TR',SPC_NO='" + _spcNo + "', BIL_ID='TR',BIL_NO='" + _bilNo + "',REM='" + _rem + "' WHERE BAR_NO='" + _barNo + "'";
            Query _query = new Query();
            _query.RunSql(_sql);
        }
        #endregion

        #region 修改处理标记为未处理
        /// <summary>
        /// 设置异常单的处理状态为未处理
        /// </summary>
        /// <param name="trNo">异常单号</param>
        public void UpdateUnCloseId(string trNo)
        {
            DbMRPTR _tr = new DbMRPTR(Comp.Conn_DB);
            _tr.UpdateUnCloseId(trNo);
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
                DbMRPTR _dbMrpTr = new DbMRPTR(Comp.Conn_DB);
                _dbMrpTr.Approve(bil_id, bil_no, chk_man, cls_dd);

                SunlikeDataSet _ds = _dbMrpTr.GetData(bil_no, false);
                if (_ds.Tables["TZERR"].Rows.Count > 0)
                {
                    this.UpdateWh(_ds.Tables["TZERR"].Rows[0], false);
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
        {
            string _msg = "";
            try
            {
                DbMRPTR _dbMrpTr = new DbMRPTR(Comp.Conn_DB);
                _dbMrpTr.RollBack(bil_id, bil_no);

                SunlikeDataSet _ds = _dbMrpTr.GetData(bil_no, false);
                if (_ds.Tables["TZERR"].Rows.Count > 0)
                {
                    this.UpdateWh(_ds.Tables["TZERR"].Rows[0], true);
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
