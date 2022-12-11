/*
 * CREATE : CXY
 */

using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using System.Data;
using Sunlike.Common.Utility;
using System.Collections;

namespace Sunlike.Business
{
    /// <summary>
    /// 暂估单
    /// </summary>
    public class DRPZG : BizObject, IAuditing
    {
        #region Property & Fields

        private bool _isRunAuditing;
        private bool _auditBarCode;
        private int _barCodeNo;
        private string _bilID = "";
        /// <summary>
        /// 是否重新切制凭证
        /// </summary>
        private bool _reBuildVohNo = false;
        private Hashtable _htPrdNo = new Hashtable();
        private Hashtable _htWh = new Hashtable();

        #endregion

        #region GetData

        /// <summary>
        /// 取DataSet
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="zgId"></param>
        /// <param name="zgNo"></param>
        /// <param name="onlyFillSchema"></param>
        public SunlikeDataSet GetData(string usr, string zgId, string zgNo, bool onlyFillSchema)
        {
            DbDRPZG _dbDrpZg = new DbDRPZG(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpZg.GetData(zgId, zgNo, onlyFillSchema);
            if (!String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;

                //管制立帐方式
                if (INVCommon.IsControlZhangId(usr, zgId))
                {
                    _ds.ExtendedProperties["CTRL_ZHANG_ID"] = "T";
                }
            }
            string _pgm = "DRP" + zgId;
            DataTable _dtMfPss = _ds.Tables["MF_ZG"];
            DataTable _dtTfPss = _ds.Tables["TF_ZG"];
            //增加单据权限
            if (!onlyFillSchema)
            {
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    if (_dtMfPss.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMfPss.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMfPss.Rows[0]["USR"].ToString();
                        Hashtable _billRight = Users.GetBillRight(_pgm, usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                        this.SetCanModify(_ds, usr, true, false);
                    }
                }
            }

            //设定表身的PreItm为自动递增
            _dtTfPss.Columns["PRE_ITM"].AutoIncrement = true;
            _dtTfPss.Columns["PRE_ITM"].AutoIncrementSeed = _dtTfPss.Rows.Count > 0 ? Convert.ToInt32(_dtTfPss.Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _dtTfPss.Columns["PRE_ITM"].AutoIncrementStep = 1;
            _dtTfPss.Columns["PRE_ITM"].Unique = true;

            if (zgId == "ZG")
            {
                _dtTfPss.Columns["OTH_ITM"].AutoIncrement = true;
                _dtTfPss.Columns["OTH_ITM"].AutoIncrementSeed = _dtTfPss.Rows.Count > 0 ? Convert.ToInt32(_dtTfPss.Select("", "OTH_ITM desc")[0]["OTH_ITM"]) + 1 : 1;
                _dtTfPss.Columns["OTH_ITM"].AutoIncrementStep = 1;
                _dtTfPss.Columns["OTH_ITM"].Unique = true;
            }
            _dtTfPss.Columns.Add("PRD_NO_NO", typeof(System.String));
            foreach (DataRow _drTfPss in _dtTfPss.Rows)
            {
                _drTfPss["PRD_NO_NO"] = _drTfPss["PRD_NO"];
            }
            //转入单据的数量
            _dtTfPss.Columns.Add(new DataColumn("QTY_SO_ORG", typeof(System.Decimal)));
            _dtTfPss.Columns.Add("UNIT_DP", typeof(String));
            //单位标准成本 
            _dtTfPss.Columns.Add(new DataColumn("CST_STD_UNIT", typeof(decimal)));
            //表身加库存量栏位
            _dtTfPss.Columns.Add(new DataColumn("WH_QTY"));
            _dtTfPss.Columns.Add(new DataColumn("UP_CST", typeof(decimal)));
            DRPPO _drpPo = new DRPPO();
            for (int i = 0; i < _ds.Tables["TF_ZG"].Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(_ds.Tables["TF_ZG"].Rows[i]["CST_STD"].ToString())
                    && !String.IsNullOrEmpty(_ds.Tables["TF_ZG"].Rows[i]["QTY"].ToString())
                    && Convert.ToDecimal(_ds.Tables["TF_ZG"].Rows[i]["QTY"]) != 0)
                    _ds.Tables["TF_ZG"].Rows[i]["CST_STD_UNIT"] = Convert.ToDecimal(_ds.Tables["TF_ZG"].Rows[i]["CST_STD"]) / Convert.ToDecimal(_ds.Tables["TF_ZG"].Rows[i]["QTY"]);
                else
                    _ds.Tables["TF_ZG"].Rows[i]["CST_STD_UNIT"] = 0;

                //取采购量
                if (_ds.Tables["TF_ZG"].Rows[i]["OS_ID"].ToString() == "PO")
                {
                    using (DataSet _dsPo = _drpPo.GetBody("PO", _ds.Tables["TF_ZG"].Rows[i]["OS_NO"].ToString(), "PRE_ITM", Convert.ToInt32(_ds.Tables["TF_ZG"].Rows[i]["EST_ITM"]), true))
                    {
                        if (_dsPo.Tables["TF_POS"].Rows.Count > 0)
                        {
                            _ds.Tables["TF_ZG"].Rows[i]["QTY_SO_ORG"] = _dsPo.Tables["TF_POS"].Rows[0]["QTY"];
                        }
                    }
                }
                else if (_ds.Tables["TF_ZG"].Rows[i]["OS_ID"].ToString() == "ZG")
                {
                    using (DataSet _dsZg = _dbDrpZg.GetBody("ZG", _ds.Tables["TF_ZG"].Rows[i]["OS_NO"].ToString(), Convert.ToInt32(_ds.Tables["TF_ZG"].Rows[i]["OTH_ITM"])))
                    {
                        if (_dsZg.Tables["TF_ZG"].Rows.Count > 0)
                        {
                            _ds.Tables["TF_ZG"].Rows[i]["QTY_SO_ORG"] = _dsZg.Tables["TF_ZG"].Rows[0]["QTY"];
                        }
                    }
                }
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
            DataView _dv = _ds.Tables["TF_ZG_B"].DefaultView;
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
                DataRow[] _aryDrBar = _ds.Tables["TF_ZG_B"].Select("BAR_CODE='" + dr["BAR_CODE"].ToString() + "'");
                if (_aryDrBar.Length > 0)
                {
                    DataRow[] _aryDr = _ds.Tables["TF_ZG"].Select("PRE_ITM=" + _aryDrBar[0]["ZG_ITM"].ToString());
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
        /// 取暂估回冲DataSet
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        public SunlikeDataSet GetZBData(string osId, string osNo)
        {
            DbDRPZG _dbDrpZg = new DbDRPZG(Comp.Conn_DB);
            return _dbDrpZg.GetZBData(osId, osNo);
        }

        /// <summary>
        /// 根据来源单取暂估回冲
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string osId, string osNo)
        {
            DbDRPZG _dbDrpZg = new DbDRPZG(Comp.Conn_DB);
            return _dbDrpZg.GetData(osId, osNo);
        }

        /// <summary>
        /// 取表身
        /// </summary>
        /// <param name="zgId"></param>
        /// <param name="zgNo"></param>
        /// <param name="othItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string zgId, string zgNo, int othItm)
        {
            DbDRPZG _dbDrpZg = new DbDRPZG(Comp.Conn_DB);
            return _dbDrpZg.GetBody(zgId, zgNo, othItm);
        }

        #endregion

        #region Function

        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="usr"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        /// <param name="IsRollBack"></param>
        private void SetCanModify(SunlikeDataSet ds, string usr, bool bCheckAuditing, bool IsRollBack)
        {
            DataTable _dtMf = ds.Tables["MF_ZG"];
            DataTable _dtTf = ds.Tables["TF_ZG"];
            if (_dtMf.Rows.Count > 0)
            {
                bool _bCanModify = true;
                if (_dtMf.Rows[0]["CLS_ID"].ToString() == "T")	//结案不能修改
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");
                }
                if (_dtMf.Rows[0]["ZHANG_ID"].ToString() == "3")
                {
                    DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
                    SunlikeDataSet _dsInv = _dbSa.GetInvBill(_dtMf.Rows[0]["ZG_ID"].ToString(), _dtMf.Rows[0]["ZG_NO"].ToString());
                    if (_dsInv.Tables[0].Rows.Count > 0)
                    {
                        _bCanModify = false;
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LZ_MODIFY");
                    }
                }
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["ZG_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                }
                if (bCheckAuditing)
                {
                    string _zgId = _dtMf.Rows[0]["ZG_ID"].ToString();
                    string _zgNo = _dtMf.Rows[0]["ZG_NO"].ToString();
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_zgId, _zgNo))
                    {
                        _bCanModify = false;
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
                if (IsRollBack)
                {
                    //转进货
                    if (_dtTf.Select("ISNULL(QTY_PC, 0) + ISNULL(QTY_PC_UNSH, 0) > 0").Length > 0)
                    {
                        _bCanModify = false;
                    }
                    //转暂估退回
                    if (_dtTf.Select("ISNULL(QTY_RTN, 0) + ISNULL(QTY_RTN_UNSH, 0) > 0").Length > 0)
                    {
                        _bCanModify = false;
                    }
                }
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["VOH_NO"].ToString()))
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
                        _updUsr = _dtMf.Rows[0]["USR"].ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_dtMf.Rows[0]["VOH_NO"].ToString(), _updUsr, ref _acNo);
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
                ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="qz">是否取整</param>
        /// <param name="flag">是否重新取单位名称</param>
        public static void CalculatePak(DataRow dr, bool qz, bool flag)
        {
            if (dr.RowState == System.Data.DataRowState.Deleted)
                return;
            Prdt _prdt = new Prdt();
            DataRow _drPrdt = _prdt.GetPrdt(dr["PRD_NO"].ToString()).Rows.Find(dr["PRD_NO"]);
            if (_drPrdt != null)
            {
                //包装换算
                decimal _pakExc = 0;
                //数量
                decimal _qty = 0;
                if (!String.IsNullOrEmpty(dr["PAK_EXC"].ToString()) && !flag)
                {
                    _pakExc = Convert.ToDecimal(dr["PAK_EXC"]);
                }
                if (_pakExc == 0 && !String.IsNullOrEmpty(_drPrdt["PAK_EXC"].ToString()))
                {
                    _pakExc = Convert.ToDecimal(_drPrdt["PAK_EXC"]);
                }

                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                    string _unit = dr["UNIT"].ToString();
                    if ((_unit == "2" || _unit == "3"))
                    {
                        if (!String.IsNullOrEmpty(_drPrdt["PK" + _unit + "_QTY"].ToString()))
                        {
                            _qty *= Convert.ToDecimal(_drPrdt["PK" + _unit + "_QTY"]);
                        }
                    }
                }

                if (_qty != 0 && _pakExc != 0)
                {
                    if (String.IsNullOrEmpty(dr["PAK_WEIGHT_UNIT"].ToString()) || flag)
                    {
                        dr["PAK_WEIGHT_UNIT"] = _drPrdt["PAK_WEIGHT_UNIT"];
                    }

                    if (String.IsNullOrEmpty(dr["PAK_MEAST_UNIT"].ToString()) || flag)
                    {
                        dr["PAK_MEAST_UNIT"] = _drPrdt["PAK_MEAST_UNIT"];
                    }

                    dr["PAK_EXC"] = _pakExc;
                    decimal _pakUnit = 0;
                    if (qz)
                    {
                        _pakUnit = Math.Ceiling(_qty / _pakExc);//向上取整
                        dr["PAK_UNIT"] = _pakUnit.ToString() + _drPrdt["PAK_UNIT"].ToString();
                    }
                    else
                    {
                        CompInfo _compInfo = Comp.GetCompInfo("");
                        _pakUnit = Math.Round(_qty / _pakExc, _compInfo.DecimalDigitsInfo.System.POI_QTY);//取comp小数位
                        dr["PAK_UNIT"] = String.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _pakUnit) + _drPrdt["PAK_UNIT"].ToString();
                    }
                    //大小
                    if (!String.IsNullOrEmpty(_drPrdt["PAK_MEAST"].ToString()))
                    {
                        dr["PAK_MEAST"] = _pakUnit * Convert.ToDecimal(_drPrdt["PAK_MEAST"]);
                    }
                    else
                    {
                        dr["PAK_MEAST"] = DBNull.Value;
                    }
                    //净重
                    if (!String.IsNullOrEmpty(_drPrdt["PAK_GW"].ToString()))
                    {
                        dr["PAK_GW"] = _pakUnit * Convert.ToDecimal(_drPrdt["PAK_GW"]);
                    }
                    else
                    {
                        dr["PAK_GW"] = DBNull.Value;
                    }
                    //毛重
                    if (!String.IsNullOrEmpty(_drPrdt["PAK_NW"].ToString()))
                    {
                        dr["PAK_NW"] = _pakUnit * Convert.ToDecimal(_drPrdt["PAK_NW"]);
                    }
                    else
                    {
                        dr["PAK_NW"] = DBNull.Value;
                    }
                }
                else
                {
                    dr["PAK_UNIT"] = DBNull.Value;
                    dr["PAK_WEIGHT_UNIT"] = DBNull.Value;
                    dr["PAK_MEAST"] = DBNull.Value;
                    dr["PAK_MEAST_UNIT"] = DBNull.Value;
                    dr["PAK_GW"] = DBNull.Value;
                    dr["PAK_EXC"] = DBNull.Value;
                    dr["PAK_NW"] = DBNull.Value;
                }
            }
        }

        #endregion

        #region 保存

        /// <summary>
        /// 保存单据内容
        /// </summary>
        /// <param name="pgm">pgm</param>
        /// <param name="ChangedDS">DataSet</param>
        /// <param name="bubbleException">是否抛出异常（true为直接抛出异常，false返回ErrorTable）</param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, SunlikeDataSet ChangedDS, string usr, bool bubbleException)
        {
            string _psID, _usr, _chkMan;
            string _bilType;
            string _mobID = "";
            DataRow _dr = ChangedDS.Tables["MF_ZG"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _psID = _dr["ZG_ID", DataRowVersion.Original].ToString();
                _usr = _dr["USR", DataRowVersion.Original].ToString();
                _bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
                _chkMan = _dr["CHK_MAN", DataRowVersion.Original].ToString();
                _mobID = _dr["MOB_ID", DataRowVersion.Original].ToString();
            }
            else
            {
                _psID = _dr["ZG_ID"].ToString();
                _usr = _dr["USR"].ToString();
                _bilType = _dr["BIL_TYPE"].ToString();
                _chkMan = _dr["CHK_MAN"].ToString();
                _mobID = _dr["MOB_ID"].ToString();
            }
            _bilID = _psID;
            //是否重建凭证号码
            if (ChangedDS.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", ChangedDS.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            Hashtable _ht = new Hashtable();
            _ht["MF_ZG"] = "ZG_ID,ZG_NO,ZG_DD,TRAD_MTH,BAT_NO,CUS_NO,VOH_ID,VOH_NO,DEP,TAX_ID,OS_ID,OS_NO,SEND_MTH,SEND_WH,ZHANG_ID,CUR_ID,EXC_RTO,SAL_NO,"
                + "ADR,REM,PAY_MTH,PAY_DAYS,CHK_DAYS,PAY_REM,PAY_DD,CHK_DD,INT_DAYS,CLS_REM,PRT_SW,CPY_SW,DIS_CNT,CONTRACT,AMT,CLS_DATE,CUS_OS_NO,BIL_TYPE,"
                + "CAS_NO,CLSID,USR,CHK_MAN,ZB_NO,HC_ID,BIL_ID,BIL_NO,MOB_ID,LOCK_MAN,FJ_NUM,SYS_DATE,TASK_ID,PRT_USR,CNTT_NO,CANCEL_ID,PO_ID,CLS_ID";
            _ht["TF_ZG"] = "ZG_ID,ZG_NO,ZG_DD,ITM,WH,BAT_NO,OS_NO,PRD_NO,PRD_NAME,PRD_MARK,UNIT,QTY,QTY1,UP,AMTN_NET,AMT,TAX,DIS_CNT,QTY_PC,QTY_PC_UNSH,"
                + "PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,VALID_DD,REM,TAX_RTO,CST_STD,UP_QTY1,EST_ITM,OS_ID,SEND_WH,"
                + "QTY_LOSS,QTY1_LOSS,ID_NO,BZ_KND,OTH_ITM,CUS_OS_NO,CHK_TAX,SUP_PRD_NO,FREE_ID,AMTN_COM,QTY_PS,RK_NO,QTY_RTN,QTY_RTN_UNSH,TI_ITM,"
                + "CK_NO,SL_NO,SH_NO_CUS,PRE_ITM,RK_DD,UP_MAIN,CST";
            //_ht["TF_ZG_B"] = "ZG_ID,ZG_NO,ZG_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BIL_FLAG,BOX_NO";
            //判断是否走审核流程
            if (_psID != "ZB")
            {
                Auditing _auditing = new Auditing();
                //_isRunAuditing = _auditing.IsRunAuditing(_psID, _usr, _bilType, _mobID);
            }
            else
            {
                _isRunAuditing = String.IsNullOrEmpty(_chkMan);
            }

            this.UpdateDataSet(ChangedDS, _ht);
            //判断单据能否修改
            if (!ChangedDS.HasErrors)
            {
                this.SetCanModify(ChangedDS, usr, true, false);
                ChangedDS.AcceptChanges();
            }
            else if (bubbleException)
            {
                string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(ChangedDS);
                throw new SunlikeException("DRPPC.UpdateData() Error:" + _errorMsg);
            }
            return GetAllErrors(ChangedDS);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪

            //DataTable _dtMf = ds.Tables["MF_ZG"];
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["ZG_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["ZG_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}

            //#endregion

            if (ds.Tables["MF_ZG"].Rows.Count > 0
                && ds.Tables["MF_ZG"].Rows[0].RowState == DataRowState.Modified
                && this._isRunAuditing
                && !String.IsNullOrEmpty(ds.Tables["MF_ZG"].Rows[0]["CHK_MAN"].ToString()))
            {
                string _rbError = this.RollBack(ds.Tables["MF_ZG"].Rows[0]["ZG_ID"].ToString(),
                    ds.Tables["MF_ZG"].Rows[0]["ZG_NO"].ToString());
                if (!String.IsNullOrEmpty(_rbError))
                {
                    throw new SunlikeException(_rbError);
                }
            }
            base.BeforeDsSave(ds);
        }

        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            string _zgId = "", _zgNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _zgId = dr["ZG_ID"].ToString();
                _zgNo = dr["ZG_NO"].ToString();
            }
            else
            {
                _zgId = dr["ZG_ID", DataRowVersion.Original].ToString();
                _zgNo = dr["ZG_NO", DataRowVersion.Original].ToString();
            }
            if (statementType != StatementType.Insert)
            {
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "ZG_ID = '" + _zgId + "' AND ZG_NO = '" + _zgNo + "'";
                if (_Users.IsLocked("MF_ZG", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            if (tableName == "MF_ZG")
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
                    if (string.IsNullOrEmpty(dr["CUR_ID"].ToString()))
                    {
                        dr["EXC_RTO"] = 1;
                    }
                    dr["SYS_DATE"] = Convert.ToDateTime(DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    //新增时判断关账日期
                    if (statementType != StatementType.Delete)
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["ZG_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    else
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["ZG_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    //检查进货厂商
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString(), Convert.ToDateTime(dr["ZG_DD"])))
                    {
                        dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//客户代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //检查业务员
                    Salm _salm = new Salm();
                    if (!_salm.IsExist(_usr, dr["SAL_NO"].ToString(), Convert.ToDateTime(dr["ZG_DD"])))
                    {
                        dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //检查部门
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["ZG_DD"])))
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
                    dr["ZG_NO"] = _sq.Set(_zgId, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["ZG_DD"]), dr["BIL_TYPE"].ToString());
                    //写入默认栏位值
                    dr["PRT_SW"] = "N";

                    //取得交易方式
                    if (String.IsNullOrEmpty(dr["PAY_MTH"].ToString()))
                    {
                        Cust _cust = new Cust();
                        Hashtable _ht = _cust.GetPAYInfo(dr["CUS_NO"].ToString(), dr["ZG_DD"].ToString());
                        if (_ht != null)
                        {
                            dr["PAY_DD"] = _ht["PAY_DD"];
                            dr["CHK_DD"] = _ht["CHK_DD"];
                            dr["SEND_MTH"] = _ht["SEND_MTH"];
                            dr["SEND_WH"] = _ht["SEND_WH"];
                            dr["PAY_MTH"] = _ht["PAY_MTH"];
                            dr["PAY_DAYS"] = _ht["PAY_DAYS"];
                            dr["CHK_DAYS"] = _ht["CHK_DAYS"];
                            dr["INT_DAYS"] = _ht["INT_DAYS"];
                            dr["PAY_REM"] = _ht["PAY_REM"];
                            dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        }
                        else
                        {
                            throw new Exception("RCID=INV.HINT.GETPAYINFO_ERROR");//无法取得客户交易方式
                        }
                    }

                    #endregion
                }
                else
                {
                    if (statementType == StatementType.Delete)
                    {
                        #region 删除

                        string _error = _sq.Delete(dr["ZG_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                        if (!String.IsNullOrEmpty(_error))
                        {
                            throw new Exception("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
                        }
                        //判断是否走审核流程
                        if (_isRunAuditing && _zgId != "ZB")
                        {
                            Auditing _auditing = new Auditing();
                            _auditing.DelBillWaitAuditing("DRP", _zgId, dr["ZG_NO", DataRowVersion.Original].ToString());
                        }

                        #endregion
                    }
                }
                if (_zgId != "ZB")
                {
                    #region 更新凭证

                    if (!this._isRunAuditing)
                    {
                        this.UpdateVohNo(dr, statementType);
                    }

                    #endregion

                    //#region 审核关联
                    //AudParamStruct _aps;
                    //if (statementType != StatementType.Delete)
                    //{
                    //    _aps.BIL_DD = Convert.ToDateTime(dr["ZG_DD"]);
                    //    _aps.BIL_ID = dr["ZG_ID"].ToString();
                    //    _aps.BIL_NO = dr["ZG_NO"].ToString();
                    //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                    //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                    //    _aps.DEP = dr["DEP"].ToString();
                    //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                    //    _aps.USR = dr["USR"].ToString();
                    //    _aps.MOB_ID = "";
                    //}
                    //else
                    //    _aps = new AudParamStruct(Convert.ToString(dr["ZG_ID", DataRowVersion.Original]), Convert.ToString(dr["ZG_NO", DataRowVersion.Original]));
                    //Auditing _auditing = new Auditing();
                    //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                    //if (!string.IsNullOrEmpty(_auditErr))
                    //{
                    //    throw new SunlikeException(_auditErr);
                    //}
                    //#endregion
                }
            }
            else if (tableName == "TF_ZG")
            {
                #region 新增或者修改时
                if (statementType != StatementType.Delete)
                {
                    string _usr = dr.Table.DataSet.Tables["MF_ZG"].Rows[0]["USR"].ToString();
                    Prdt _prdt = new Prdt();
                    //检查货品代号
                    if (_htPrdNo[dr["PRD_NO"].ToString()] == null)
                    {
                        if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_ZG"].Rows[0]["ZG_DD"])))
                        {
                            dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + dr["PRD_NO"].ToString());//货品代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        _htPrdNo[dr["PRD_NO"].ToString()] = "";
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
                    if (_htWh[dr["WH"].ToString()] == null)
                    {
                        if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_ZG"].Rows[0]["ZG_DD"])))
                        {
                            dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//仓库代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        _htWh[dr["WH"].ToString()] = "";
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

                    //自动新增批号
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        Bat _bat = new Bat();
                        if (!_bat.IsExist(dr["BAT_NO"].ToString()) && Users.GetSpcPswdString(_usr, "AUTO_NEW_BATNO") == "F")
                        {
                            throw new Exception("RCID=COMMON.HINT.AUTO_NEW_BATNO,PARAM=" + dr["BAT_NO"].ToString());
                        }
                        _bat.AutoInsertData(dr["BAT_NO"].ToString(), dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["ZG_DD"]));
                    }
                    if (dr["OS_ID"].ToString() == "PO")
                    {
                        //赋值回冲量
                        dr["QTY_PS"] = dr["QTY"];
                    }
                }
                #endregion
            }
            if (_barCodeNo == 0 && _zgId != "ZB")
            {
                #region 更新序列号记录

                if (!_isRunAuditing)
                {
                    this.UpdateBarCode(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                }
                if (dr.Table.DataSet.Tables["MF_ZG"].Rows[0].RowState == DataRowState.Deleted)
                {
                    Query _query = new Query();
                    _query.RunSql("delete from TF_ZG_B where ZG_ID='" + dr["ZG_ID", DataRowVersion.Original].ToString()
                        + "' and ZG_NO='" + dr["ZG_NO", DataRowVersion.Original].ToString() + "'");
                }
                else
                {
                    string _fieldList = "ZG_ID,ZG_NO,ZG_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
                    SQLBatchUpdater _sbu = new SQLBatchUpdater(Comp.Conn_DB);
                    _sbu.BatchUpdateSize = 50;
                    _sbu.BatchUpdate(dr.Table.DataSet.Tables["TF_ZG_B"], _fieldList);
                }

                #endregion
            }
            _barCodeNo++;
            //判断是否走审核流程
            if (!_isRunAuditing)
            {
                if (tableName == "TF_ZG")
                {
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateZgByZr(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateZgByZr(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateZgByZr(dr, false);
                        this.UpdateZgByZr(dr, true);
                    }
                }
            }
        }

        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            string _psId = "";
            string _psNo = "";
            string _osId = "";
            string _osNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _psId = dr["ZG_ID"].ToString();
                _psNo = dr["ZG_NO"].ToString();
            }
            else
            {
                _psId = dr["ZG_ID", DataRowVersion.Original].ToString();
                _psNo = dr["ZG_NO", DataRowVersion.Original].ToString();
            }
            //判断是否走审核流程
            if (!_isRunAuditing)
            {
                if (tableName == "TF_ZG")
                {
                    #region 更新产品库存
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateWh(dr, true, false);
                        this.UpdateQtyRtn(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateWh(dr, false, false);
                        this.UpdateQtyRtn(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateWh(dr, false, false);
                        this.UpdateQtyRtn(dr, false);
                        this.UpdateWh(dr, true, false);
                        this.UpdateQtyRtn(dr, true);
                    }
                    #endregion
                }
            }
        }

        #endregion

        #region 更新凭证

        /// <summary>
        /// 更新凭证号码
        /// </summary>
        /// <param name="dr">MF_ZG的数据行</param>
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
                string _zgId = dr["ZG_ID"].ToString();
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
                        if (string.Compare("ZG", _zgId) == 0 || string.Compare("ZR", _zgId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.ZG;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _zgId, dr["ZG_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _zgId, out _vohNoError);
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
                        if (string.Compare("ZG", _zgId) == 0 || string.Compare("ZR", _zgId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.ZG;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _zgId, dr["ZG_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _zgId, out _vohNoError);
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
                string _zgId = dr["ZG_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("ZG", _zgId) == 0 || string.Compare("ZR", _zgId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.ZG;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _zgId, out _vohNoError);
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
        /// <summary>
        /// 更新暂估凭证号码
        /// </summary>
        /// <param name="zgId"></param>
        /// <param name="zgNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string zgId, string zgNo, string vohNo)
        {

            DbDRPZG _zg = new DbDRPZG(Comp.Conn_DB);
            _zg.UpdateVohNo(zgId, zgNo, vohNo);
        }
        #endregion

        #region 更新序列号

        private void UpdateBarCode(SunlikeDataSet ChangedDS)
        {
            DataRow _drHead = ChangedDS.Tables["MF_ZG"].Rows[0];
            DataTable _dtBody = ChangedDS.Tables["TF_ZG"];
            //查找表身有修改过库位的记录
            Hashtable _htKeyItm = new Hashtable();
            for (int i = 0; i < _dtBody.Rows.Count; i++)
            {
                if (_dtBody.Rows[i].RowState == DataRowState.Modified
                    && (_dtBody.Rows[i]["WH"].ToString() != _dtBody.Rows[i]["WH", DataRowVersion.Original].ToString()
                    || _dtBody.Rows[i]["BAT_NO"].ToString() != _dtBody.Rows[i]["BAT_NO", DataRowVersion.Original].ToString()))
                {
                    _htKeyItm[_dtBody.Rows[i]["PRE_ITM"].ToString()] = "";
                }
            }
            //查找BAR_CODE
            DataTable _dtBarCode;
            if ((_drHead.RowState == DataRowState.Modified || _drHead.RowState == DataRowState.Unchanged) && !this._auditBarCode)
            {
                string _sql = "select ZG_ID,ZG_NO,ZG_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO from TF_ZG_B"
                    + " where ZG_ID='" + _drHead["ZG_ID"].ToString() + "' and ZG_NO='" + _drHead["ZG_NO"].ToString() + "'";
                Query _query = new Query();
                SunlikeDataSet _dsQuery = _query.DoSQLString(_sql);
                _dsQuery.Tables[0].TableName = "TF_ZG_B";
                ChangedDS.Merge(_dsQuery.Tables["TF_ZG_B"], true, MissingSchemaAction.AddWithKey);
                _dtBarCode = ChangedDS.Tables["TF_ZG_B"];
            }
            else
            {
                _dtBarCode = ChangedDS.Tables["TF_ZG_B"];
            }
            if (_dtBarCode == null)
            {
                return;
            }
            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            for (int i = 0; i < _dtBarCode.Rows.Count; i++)
            {
                if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["ZG_ITM"].ToString()] != null)
                {
                    if (_sb.Length > 0)
                    {
                        _sb.Append(",");
                    }
                    _sb.Append("'");
                    if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                    {
                        _sb.Append(_dtBarCode.Rows[i]["BAR_CODE", DataRowVersion.Original].ToString());
                    }
                    else
                    {
                        _sb.Append(_dtBarCode.Rows[i]["BAR_CODE"].ToString());
                    }
                    _sb.Append("'");
                }
            }
            //有新增/修改过BAR_CODE，则修改BAR_REC表
            if (_sb.Length > 0)
            {
                _sb.Insert(0, "BAR_NO in (");
                _sb.Append(")");
                //把条件语句分段
                ArrayList _alBar = new ArrayList();
                int _maxWhereLength = 10240;
                string _subWhere;
                int _pos;
                string _where = _sb.ToString();
                while (true)
                {
                    if (_where.Length > _maxWhereLength)
                    {
                        _subWhere = _where.Substring(0, _maxWhereLength - 1);
                        _pos = _subWhere.LastIndexOf(",");
                        _alBar.Add(_subWhere.Substring(0, _pos) + ")");
                        _where = "BAR_NO in (" + _where.Substring(_pos + 1, _where.Length - _pos - 1);
                    }
                    else
                    {
                        _alBar.Add(_where);
                        break;
                    }
                }
                //从资料库读取序列号资料
                BarCode _bar = new BarCode();
                SunlikeDataSet _dsBar = new SunlikeDataSet();
                DataTable _dtBarRec;
                for (int i = 0; i < _alBar.Count; i++)
                {
                    _dtBarRec = _bar.GetBarRecord(_alBar[i].ToString(), true);
                    if (_dsBar.Tables["BAR_REC"] == null)
                    {
                        _dsBar.Tables.Add(_dtBarRec.Copy());
                    }
                    else
                    {
                        _dsBar.Merge(_dtBarRec, true, MissingSchemaAction.AddWithKey);
                    }
                }
                _dtBarRec = _dsBar.Tables["BAR_REC"];
                string[] _aryBoxNo = new string[_dtBarCode.Rows.Count];
                //取表头资料
                string _cusNo, _bilID, _bilNo, _usr;
                DateTime _bilDd = DateTime.Today;
                if (_drHead.RowState == DataRowState.Deleted)
                {
                    _cusNo = _drHead["CUS_NO", DataRowVersion.Original].ToString();
                    _bilID = _drHead["ZG_ID", DataRowVersion.Original].ToString();
                    _bilNo = _drHead["ZG_NO", DataRowVersion.Original].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["ZG_DD", DataRowVersion.Original]);
                    _usr = _drHead["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _cusNo = _drHead["CUS_NO"].ToString();
                    _bilID = _drHead["ZG_ID"].ToString();
                    _bilNo = _drHead["ZG_NO"].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["ZG_DD"]);
                    _usr = _drHead["USR"].ToString();
                }
                //取得该单据做过的change
                DataTable _dtBarChange = null;
                if (_drHead.RowState != DataRowState.Added)
                {
                    _dtBarChange = _bar.GetBarChange(_bilID, _bilNo);
                }
                //更新BAR_REC表
                System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
                Hashtable _htBoxNo = new Hashtable();
                ArrayList _alBoxNo = new ArrayList();
                ArrayList _alWhNo = new ArrayList();
                ArrayList _alStop = new ArrayList();
                System.Text.StringBuilder _sbChange = new System.Text.StringBuilder();
                double _total = 0;
                for (int i = 0; i < _dtBarCode.Rows.Count; i++)
                {
                    if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["ZG_ITM"].ToString()] != null)
                    {
                        string _barCode, _boxNo, _keyItm, _whNo, _batNo;
                        string _oldWhNo = "";
                        string _oldBatNo = "";
                        DataRow[] _dra;
                        bool _isPlus = true;
                        if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                        {
                            DateTime _dt1 = DateTime.Now;
                            _barCode = _dtBarCode.Rows[i]["BAR_CODE", DataRowVersion.Original].ToString();
                            _boxNo = _dtBarCode.Rows[i]["BOX_NO", DataRowVersion.Original].ToString();
                            _keyItm = _dtBarCode.Rows[i]["ZG_ITM", DataRowVersion.Original].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _keyItm, "", DataViewRowState.CurrentRows | DataViewRowState.OriginalRows);
                            if (_dra[0].RowState == DataRowState.Deleted)
                            {
                                _whNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _batNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                                if (_dra[0]["ZG_ID", DataRowVersion.Original].ToString() == "ZR")
                                {
                                    _isPlus = false;
                                }
                            }
                            else
                            {
                                _batNo = _dra[0]["BAT_NO"].ToString();
                                _whNo = _dra[0]["WH"].ToString();
                                if (_dra[0]["ZG_ID"].ToString() == "ZR")
                                {
                                    _isPlus = false;
                                }
                            }
                            DateTime _dt2 = DateTime.Now;
                            TimeSpan _ts = _dt2 - _dt1;
                            _total += _ts.TotalSeconds;
                        }
                        else
                        {
                            _barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
                            _boxNo = _dtBarCode.Rows[i]["BOX_NO"].ToString();
                            _keyItm = _dtBarCode.Rows[i]["ZG_ITM"].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _keyItm);
                            _whNo = _dra[0]["WH"].ToString();
                            _batNo = _dra[0]["BAT_NO"].ToString();
                            if (_dra[0].RowState == DataRowState.Modified)
                            {
                                _oldWhNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _oldBatNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                            }
                            if (_dra[0]["ZG_ID"].ToString() == "ZR")
                            {
                                _isPlus = false;
                            }
                        }
                        DataRow _drBarRec = _dtBarRec.Rows.Find(new string[1] { _barCode });
                        if (_drBarRec != null)
                        {
                            bool _canUpdate = true;
                            Sunlike.Business.UserProperty _userProp = new UserProperty();
                            string _pgm = "DRP" + _bilID;
                            if (!String.IsNullOrEmpty(_drBarRec["BOX_NO"].ToString()) && _userProp.GetData(_usr, _pgm, "CONTROL_BARCODE") != "0"
                                && Comp.BarcodeUpdCheck && Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "F")
                            {
                                if (!String.IsNullOrEmpty(_drBarRec["UPDDATE"].ToString()))
                                {
                                    DateTime _barDd = Convert.ToDateTime(_drBarRec["UPDDATE"]);
                                    TimeSpan _timeSpan = _barDd.Subtract(_bilDd);
                                    if (_timeSpan.TotalMilliseconds > 0)
                                    {
                                        _canUpdate = false;
                                    }
                                }
                            }
                            if (_canUpdate)
                            {
                                //如果出货库位与序列号所在的库位不同，则要写入BAR_CHANGE
                                //调增：只要bar_rec里有此序列号，且库位不为空，都要写入bar_change
                                //调减：只要bar_rec里有此序列号，且库位不为空，且调减的库位不等于当前序列号所在库位，则写入bar_change
                                if (_dtBarCode.Rows[i].RowState == DataRowState.Added)
                                {
                                    if ((_isPlus && (!String.IsNullOrEmpty(_drBarRec["WH"].ToString()) || !String.IsNullOrEmpty(_drBarRec["BAT_NO"].ToString())))
                                        || (!_isPlus && (_drBarRec["WH"].ToString() != _whNo || _drBarRec["BAT_NO"].ToString() != _batNo)) || _drBarRec["PH_FLAG"].ToString() == "T")
                                    {
                                        _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo, _bilID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
                                    }
                                }
                                else if (_dtBarCode.Rows[i].RowState == DataRowState.Unchanged)
                                {
                                    if ((_isPlus && (_oldWhNo != _drBarRec["WH"].ToString() || _oldBatNo != _drBarRec["BAT_NO"].ToString() || _drBarRec["STOP_ID"].ToString() == "T"))
                                        || (!_isPlus && (_oldWhNo != _drBarRec["WH"].ToString() || _oldBatNo != _drBarRec["BAT_NO"].ToString() || _drBarRec["STOP_ID"].ToString() != "T")))
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
                                    }
                                    else
                                    {
                                        DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
                                        if (_draBarChange.Length > 0)
                                        {
                                            string _changeWh1 = _draBarChange[0]["WH1"].ToString();
                                            string _changeBatNo1 = _draBarChange[0]["BAT_NO1"].ToString();
                                            string _changePhFlag1 = _draBarChange[0]["PH_FLAG1"].ToString();
                                            _sbChange.Append(_bar.DeleteBarChange(_barCode, _bilID, _bilNo, true));
                                            if (_isPlus || (!_isPlus && (_changeWh1 != _whNo || _changeBatNo1 != _batNo)))
                                            {
                                                _sbChange.Append(_bar.InsertBarChange(_barCode, _changeWh1, _whNo, _bilID, _bilNo, _usr, _changeBatNo1, _batNo, _changePhFlag1, "", true));
                                            }
                                        }
                                        else if ((!_isPlus && (_drBarRec["WH"].ToString() != _whNo || _drBarRec["BAT_NO"].ToString() != _batNo)) || _drBarRec["PH_FLAG"].ToString() == "T")
                                        {
                                            _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo, _bilID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
                                        }
                                    }
                                }
                                string _boxWh = "";
                                if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                                {
                                    DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
                                    if (_isPlus)
                                    {
                                        //BAR_REC.WH必须和单据库位一致，且BAR_REC.STOP_ID='F'，否则不允许删除
                                        if (_batNo == _drBarRec["BAT_NO"].ToString() && _whNo == _drBarRec["WH"].ToString() && _drBarRec["STOP_ID"].ToString() != "T")
                                        {
                                            if (_draBarChange.Length == 0)
                                            {
                                                _drBarRec["WH"] = System.DBNull.Value;
                                                _drBarRec["BAT_NO"] = System.DBNull.Value;
                                            }
                                        }
                                        else
                                        {
                                            _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
                                        }
                                    }
                                    else
                                    {
                                        if (_batNo == _drBarRec["BAT_NO"].ToString() && _whNo == _drBarRec["WH"].ToString() && _drBarRec["STOP_ID"].ToString() == "T")
                                        {
                                            _drBarRec["STOP_ID"] = "F";
                                        }
                                        else
                                        {
                                            _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
                                        }
                                    }
                                    //如果有做过bar_change，则库位回到做bar_change之前的地方，且把bar_change的记录删掉
                                    if (_draBarChange.Length > 0)
                                    {
                                        _boxWh = _draBarChange[0]["WH1"].ToString();
                                        _drBarRec["WH"] = _boxWh;
                                        _drBarRec["BAT_NO"] = _draBarChange[0]["BAT_NO1"].ToString();
                                        _drBarRec["STOP_ID"] = "T";
                                        //到货确认标记
                                        _drBarRec["PH_FLAG"] = _draBarChange[0]["PH_FLAG1"];
                                        _sbChange.Append(_bar.DeleteBarChange(_barCode, _bilID, _bilNo, true));
                                    }
                                }
                                else
                                {
                                    if (_isPlus)
                                    {
                                        _drBarRec["WH"] = _whNo;
                                        _drBarRec["BAT_NO"] = _batNo;
                                        _drBarRec["STOP_ID"] = "F";
                                    }
                                    else
                                    {
                                        if (_drBarRec["WH"].ToString() != _whNo)
                                        {
                                            _drBarRec["WH"] = _whNo;
                                            _boxWh = _whNo;
                                        }
                                        if (_drBarRec["BAT_NO"].ToString() != _batNo)
                                        {
                                            _drBarRec["BAT_NO"] = _batNo;
                                        }
                                        _drBarRec["STOP_ID"] = "T";
                                    }
                                    _drBarRec["PH_FLAG"] = "F";
                                }
                                _drBarRec["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                                //如果B0X_NO不一样，则要做自动拆箱
                                if (_drBarRec["BOX_NO"].ToString() != _boxNo)
                                {
                                    if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
                                    {
                                        _aryBoxNo[i] = _drBarRec["BOX_NO"].ToString().Trim();
                                    }
                                    else
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.DOUBLEBOX,PARAM=" + _barCode);//序列号" + _barCode + "已经装箱，你不能拆箱录入！
                                    }
                                }
                                else if (!String.IsNullOrEmpty(_boxNo))
                                {
                                    if (_htBoxNo[_boxNo] == null)
                                    {
                                        _htBoxNo[_boxNo] = "";
                                        _alBoxNo.Add(_boxNo);
                                        if (_isPlus)
                                        {
                                            if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                                            {
                                                _alWhNo.Add(_boxWh);
                                            }
                                            else
                                            {
                                                _alWhNo.Add(_whNo);
                                            }
                                            _alStop.Add("F");
                                        }
                                        else
                                        {
                                            _alWhNo.Add(null);
                                            if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                                            {
                                                _alStop.Add("F");
                                            }
                                            else
                                            {
                                                _alStop.Add("T");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _dtBarRec.Rows.Remove(_drBarRec);
                            }
                        }
                        else if (_dtBarCode.Rows[i].RowState != DataRowState.Deleted)
                        {
                            DataRow _dr = _dtBarRec.NewRow();
                            _dr["BAR_NO"] = _barCode;
                            _dr["WH"] = _whNo;
                            _dr["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            _dr["STOP_ID"] = (!_isPlus).ToString().ToUpper().Substring(0, 1);
                            _dr["PRD_NO"] = _barCode.Substring(BarCode.BarRole.SPrdt, BarCode.BarRole.EPrdt - BarCode.BarRole.SPrdt + 1).Replace(BarCode.BarRole.TrimChar, "");
                            if (!(BarCode.BarRole.BPMark == BarCode.BarRole.EPMark && BarCode.BarRole.EPMark == 0))
                            {
                                _dr["PRD_MARK"] = _barCode.Substring(BarCode.BarRole.BPMark, BarCode.BarRole.EPMark - BarCode.BarRole.BPMark + 1);
                            }
                            _dr["BAT_NO"] = _batNo;
                            _dtBarRec.Rows.Add(_dr);
                        }
                    }
                }
                if (String.IsNullOrEmpty(_sbError.ToString()))
                {
                    //写入序列号变更记录
                    if (_sbChange.Length > 0)
                    {
                        Query _query = new Query();
                        string _sql = _sbChange.ToString();
                        int _maxSqlLength = 10240;
                        while (true)
                        {
                            if (_sql.Length > _maxSqlLength)
                            {
                                string _subSql = _sql.Substring(0, _maxSqlLength - 1);
                                _pos = _subSql.LastIndexOf("\n");
                                _subSql = _subSql.Substring(0, _pos + 1);
                                _sql = _sql.Substring(_pos + 1, _sql.Length - _pos - 1);
                                _query.RunSql(_subSql);
                            }
                            else
                            {
                                _query.RunSql(_sql);
                                break;
                            }
                        }
                    }
                    DataTable _dtError = _bar.UpdateRec(_dsBar);
                    if (_dtError.Rows.Count > 0)
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.BOXERROR");//更新序列号记录失败！
                    }
                    _alWhNo.Clear();
                    _alStop.Clear();
                    foreach (string boxNo in _alBoxNo)
                    {
                        DataRow[] _draBoxInfo = _dtBarRec.Select("BOX_NO='" + boxNo + "'");
                        if (_draBoxInfo.Length > 0)
                        {
                            _alWhNo.Add(_draBoxInfo[0]["WH"].ToString());
                            _alStop.Add(_draBoxInfo[0]["STOP_ID"].ToString());
                        }
                        else
                        {
                            _alWhNo.Add(null);
                            _alStop.Add(null);
                        }
                    }
                    //修改BAR_BOX
                    _bar.UpdateBarBox(_alBoxNo, _alWhNo, _alStop);
                    //拆箱
                    _bar.DeleteBox(_aryBoxNo, _usr, _bilID, _bilNo);
                }
                else
                {
                    throw new SunlikeException(_sbError.ToString());
                }
            }
        }

        #endregion

        #region 更新库存

        private void UpdateWh(DataRow dr, bool IsAdd, bool onlyOnWay)
        {
            string _batNo = "";
            string _validDd = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _unit = "";
            string _osId = "";
            string _osNo = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
            decimal _cst = 0;
            int _estItm = 0;
            if (IsAdd)
            {
                _bilID = dr["ZG_ID"].ToString();
                _batNo = dr["BAT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat);
                }
                _prdNo = dr["PRD_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _whNo = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                _osId = dr["OS_ID"].ToString();
                _osNo = dr["OS_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1"].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1"]);
                }
                if (!String.IsNullOrEmpty(dr["AMTN_NET"].ToString()))
                {
                    _cst += Convert.ToDecimal(dr["AMTN_NET"]);
                }
                if (!String.IsNullOrEmpty(dr["EST_ITM"].ToString()))
                {
                    _estItm = Convert.ToInt32(dr["EST_ITM"]);
                }
            }
            else
            {
                _bilID = dr["ZG_ID", DataRowVersion.Original].ToString();
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD", DataRowVersion.Original].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD", DataRowVersion.Original]).ToString(Comp.SQLDateFormat);
                }
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                _osId = dr["OS_ID", DataRowVersion.Original].ToString();
                _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty -= Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                {
                    _qty1 -= Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["AMTN_NET", DataRowVersion.Original].ToString()))
                {
                    _cst -= Convert.ToDecimal(dr["AMTN_NET", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["EST_ITM", DataRowVersion.Original].ToString()))
                {
                    _estItm = Convert.ToInt32(dr["EST_ITM", DataRowVersion.Original]);
                }
            }
            if (_bilID == "ZR" || _bilID == "ZB")
            {
                _qty *= -1;
                _qty1 *= -1;
                _cst *= -1;
            }
            WH _wh = new WH();
            Hashtable _ht = new Hashtable();
            if (_osId == "PO")
            {
                _ht[WH.QtyTypes.QTY_ON_WAY] = _qty * -1;
            }
            if (!String.IsNullOrEmpty(_batNo))
            {
                if (onlyOnWay)
                {
                    _ht[WH.QtyTypes.QTY_ON_WAY] = _qty;
                }
                else
                {
                    _ht[WH.QtyTypes.QTY_IN] = _qty;
                    _ht[WH.QtyTypes.QTY1_IN] = _qty1;
                    _ht[WH.QtyTypes.QTY_ZG] = _qty;
                    _ht[WH.QtyTypes.CST] = _cst;
                    _ht[WH.QtyTypes.LST_IND] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                }
                if ((IsAdd && _qty < 0) || (!IsAdd && _qty > 0))
                {
                    //进货退回不需要写有效日期
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                }
                else
                {
                    Hashtable _ht1 = new Hashtable();
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
                                if (_ht1.Count > 0)
                                {
                                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht1);
                                }
                            }
                            else
                            {
                                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht);
                                if (_ht1.Count > 0)
                                {
                                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht1);
                                }
                            }
                        }
                        else
                        {
                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht);
                            if (_ht1.Count > 0)
                            {
                                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht1);
                            }
                        }
                    }
                    else
                    {
                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                        if (_ht1.Count > 0)
                        {
                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht1);
                        }
                    }
                }
            }
            else
            {
                if (onlyOnWay)
                {
                    _ht[WH.QtyTypes.QTY_ON_WAY] = _qty;
                }
                else
                {
                    _ht[WH.QtyTypes.QTY] = _qty;
                    _ht[WH.QtyTypes.QTY_ZG] = _qty;
                    _ht[WH.QtyTypes.QTY1] = _qty1;
                    _ht[WH.QtyTypes.AMT_CST] = _cst;
                    _ht[WH.QtyTypes.LST_IND] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                }
                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
            }
        }

        #endregion

        #region 回写原单退回数

        private void UpdateQtyRtn(DataRow dr, bool IsAdd)
        {
            string _osID, _osNo;
            int _preItm = 0;
            decimal _qty = 0;
            int _unit = 1;
            string _prdNo = "";
            if (IsAdd)
            {
                _osID = dr["OS_ID"].ToString();
                _osNo = dr["OS_NO"].ToString();
                _prdNo = dr["PRD_NO"].ToString();
                if (_osID == "ZG")
                {
                    if (!String.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["OTH_ITM"]);
                    }
                }
                else if (_osID == "PO")
                {
                    if (!String.IsNullOrEmpty(dr["EST_ITM"].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["EST_ITM"]);
                    }
                }
                else
                {
                    return;
                }
                if (!String.IsNullOrEmpty(_osNo))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                {
                    _unit = Convert.ToInt32(dr["UNIT"]);
                }
            }
            else
            {
                _osID = dr["OS_ID", DataRowVersion.Original].ToString();
                _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (_osID == "ZG")
                {
                    if (!String.IsNullOrEmpty(dr["OTH_ITM", DataRowVersion.Original].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original]);
                    }
                }
                else if (_osID == "PO")
                {
                    if (!String.IsNullOrEmpty(dr["EST_ITM", DataRowVersion.Original].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["EST_ITM", DataRowVersion.Original]);
                    }
                }
                else
                {
                    return;
                }
                if (!String.IsNullOrEmpty(_osNo))
                {
                    _qty -= Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                {
                    _unit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
                }
            }
            if (!String.IsNullOrEmpty(_osNo) && _preItm > 0)
            {
                Hashtable _ht = new Hashtable();
                if (_osID == "PO")
                {
                    _ht["TableName"] = "TF_POS";
                    _ht["IdName"] = "OS_ID";
                    _ht["NoName"] = "OS_NO";
                }
                else
                {
                    _ht["TableName"] = "TF_ZG";
                    _ht["IdName"] = "ZG_ID";
                    _ht["NoName"] = "ZG_NO";
                }
                _ht["ItmName"] = "PRE_ITM";
                _ht["OsID"] = _osID;
                _ht["OsNO"] = _osNo;
                _ht["KeyItm"] = _preItm;

                _qty = INVCommon.GetRtnQty(_prdNo, _qty, _unit, _ht);
                DbDRPZG _drpPc = new DbDRPZG(Comp.Conn_DB);
                string _errorID = _drpPc.UpdateQtyRtn(_osID, _osNo, _preItm, _qty);
                if (_errorID == "1")
                {
                    dr.SetColumnError("QTY", "RCID=INV.HINT.QTY_RTN_TANTO");
                    throw new Exception("RCID=INV.HINT.QTY_RTN_TANTO");		//退回数量不能大于进货数量  
                }
            }
        }

        #endregion

        #region 回写暂估单、生成暂估回冲单

        #region Summary

        //--当传入的dataRow为进货单表头时，根据第二个参数选择新增暂估回冲单或删除暂估回冲单。
        //--当传入的dataRow为进货单表身时，根据第二个参数选择回写暂估单的已交量为正值或负值。
        //--在进货单的AfterUpdate里面调用

        #endregion

        /// <summary>
        /// 回写暂估单、生成暂估回冲单
        /// </summary>
        /// <param name="dataRow">进货单表头或表身行</param>
        /// <param name="isAdd">是否为新增</param>
        /// <param name="isRollBack">是否为RollBack</param>
        public void UpdateZgByPc(DataRow dataRow, bool isAdd, bool isRollBack)
        {
            DataSet _dsPc = dataRow.Table.DataSet;
            DataTable _dtMfPc = _dsPc.Tables["MF_PSS"];
            DataTable _dtTfPc = _dsPc.Tables["TF_PSS"];
            if (_dtMfPc.Rows.Count > 0)
            {
                if (dataRow.Table.TableName == "MF_PSS")
                {
                    #region 暂估回冲单

                    if (isAdd)
                    {
                        string _usr = dataRow["USR"].ToString();

                        SunlikeDataSet _dsZb = this.GetZBData(_dtMfPc.Rows[0]["PS_ID"].ToString(), _dtMfPc.Rows[0]["PS_NO"].ToString());
                        if (_dsZb.Tables["MF_ZG"].Rows.Count > 0)
                        {
                            foreach (DataRow dr in _dsZb.Tables["MF_ZG"].Rows)
                            {
                                dr["CHK_MAN"] = _dtMfPc.Rows[0]["CHK_MAN"];
                                dr["CLS_DATE"] = _dtMfPc.Rows[0]["CLS_DATE"];
                            }
                        }
                        else
                        {
                            _dsZb = this.GetData("", "ZB", "", true);
                            List<string> _listPcField = new List<string>();
                            _listPcField.AddRange(new string[] { "ZG_ID", "ZG_NO", "ITM", "ZG_DD", "OS_ID", "OS_NO", "OTH_ITM", "EST_ITM", "QTY", "QTY1", "PRE_ITM" });
                            Hashtable _htUp = new Hashtable();
                            _htUp["TableName"] = "TF_ZG";
                            _htUp["IdName"] = "ZG_ID";
                            _htUp["NoName"] = "ZG_NO";
                            _htUp["ItmName"] = "PRE_ITM";

                            string _zgNo = "";
                            foreach (DataRow dr in _dtTfPc.Rows)
                            {
                                DataSet _dsZg = this.GetData("", dr["OS_ID"].ToString(), dr["OS_NO"].ToString(), false);
                                DataTable _dtMfZg = _dsZg.Tables["MF_ZG"];
                                DataTable _dtTfZg = _dsZg.Tables["TF_ZG"];

                                if (_dtMfZg.Rows.Count > 0)
                                {
                                    if (_zgNo != dr["OS_NO"].ToString())
                                    {
                                        _zgNo = dr["OS_NO"].ToString();

                                        DataRow _drMf = _dsZb.Tables["MF_ZG"].NewRow();
                                        foreach (DataColumn dc in _dsZb.Tables["MF_ZG"].Columns)
                                        {
                                            if (_dtMfPc.Columns.Contains(dc.ColumnName))
                                            {
                                                _drMf[dc] = _dtMfPc.Rows[0][dc.ColumnName];
                                            }
                                        }
                                        _drMf["ZG_ID"] = "ZB";
                                        _drMf["ZG_NO"] = _zgNo;
                                        _drMf["ZG_DD"] = _dtMfPc.Rows[0]["PS_DD"];
                                        _drMf["OS_ID"] = _dtMfPc.Rows[0]["PS_ID"];
                                        _drMf["OS_NO"] = _dtMfPc.Rows[0]["PS_NO"];
                                        _drMf["BIL_ID"] = _dtMfZg.Rows[0]["OS_ID"];
                                        _drMf["BIL_NO"] = _dtMfZg.Rows[0]["OS_NO"];
                                        _drMf["TAX_ID"] = _dtMfPc.Rows[0]["TAX_ID"];
                                        _drMf["CHK_MAN"] = _dtMfPc.Rows[0]["CHK_MAN"];
                                        _drMf["CLS_DATE"] = _dtMfPc.Rows[0]["CLS_DATE"];
                                        _drMf["VOH_ID"] = _dtMfZg.Rows[0]["VOH_ID"];
                                        _drMf["VOH_NO"] = "";
                                        _drMf["CUS_NO"] = _dtMfZg.Rows[0]["CUS_NO"];
                                        _drMf["CUR_ID"] = _dtMfZg.Rows[0]["CUR_ID"];
                                        _drMf["EXC_RTO"] = _dtMfZg.Rows[0]["EXC_RTO"];
                                        _drMf["DEP"] = _dtMfZg.Rows[0]["DEP"];
                                        _drMf["SAL_NO"] = _dtMfZg.Rows[0]["SAL_NO"];
                                        _drMf["ZB_NO"] = _dtMfZg.Rows[0]["ZG_NO"];

                                        _dsZb.Tables["MF_ZG"].Rows.Add(_drMf);
                                    }

                                    DataRow _drTf = _dsZb.Tables["TF_ZG"].NewRow();
                                    _drTf["ZG_ID"] = "ZB";
                                    _drTf["ZG_NO"] = _zgNo;
                                    _drTf["ITM"] = dr["ITM"];
                                    _drTf["ZG_DD"] = _dtMfPc.Rows[0]["PS_DD"];
                                    _drTf["OS_ID"] = dr["PS_ID"];
                                    _drTf["OS_NO"] = dr["PS_NO"];
                                    _drTf["OTH_ITM"] = dr["OTH_ITM"];
                                    _drTf["EST_ITM"] = dr["PRE_ITM"];

                                    DataRow[] _dra = _dtTfZg.Select(String.Format("ZG_ID = '{0}' AND ZG_NO = '{1}' AND OTH_ITM = {2}",
                                        dr["OS_ID"].ToString(), dr["OS_NO"].ToString(), dr["OTH_ITM"].ToString()));
                                    if (_dra.Length > 0)
                                    {
                                        foreach (DataColumn dc in _drTf.Table.Columns)
                                        {
                                            if (!_listPcField.Contains(dc.ColumnName))
                                            {
                                                _drTf[dc.ColumnName] = _dra[0][dc.ColumnName];
                                            }
                                        }
                                    }

                                    _htUp["OsID"] = dr["OS_ID"].ToString();
                                    _htUp["OsNO"] = dr["OS_NO"].ToString();
                                    _htUp["KeyItm"] = Convert.ToInt32(dr["OTH_ITM"]);
                                    _drTf["QTY"] = INVCommon.GetRtnQty(_drTf["PRD_NO"].ToString(), Convert.ToDecimal(dr["QTY"]), Convert.ToInt32(dr["UNIT"]), _htUp);
                                    if (!String.IsNullOrEmpty(dr["QTY1"].ToString()))
                                    {
                                        _drTf["QTY1"] = INVCommon.GetRtnQty(_drTf["PRD_NO"].ToString(), Convert.ToDecimal(dr["QTY1"]), Convert.ToInt32(_drTf["UNIT"]), _htUp);
                                    }

                                    //重新计算金额
                                    decimal _qty = 0, _up = 0, _taxRto = 0, _excRto = 1, _disCnt = 100;
                                    if (!String.IsNullOrEmpty(_drTf["QTY"].ToString()))
                                    {
                                        _qty = Convert.ToDecimal(_drTf["QTY"]);
                                    }
                                    if (!String.IsNullOrEmpty(_drTf["UP"].ToString()))
                                    {
                                        _up = Convert.ToDecimal(_drTf["UP"]);
                                    }
                                    if (!String.IsNullOrEmpty(_drTf["TAX_RTO"].ToString()))
                                    {
                                        _taxRto = Convert.ToDecimal(_drTf["TAX_RTO"]);
                                    }
                                    if (!String.IsNullOrEmpty(_dtMfZg.Rows[0]["EXC_RTO"].ToString()))
                                    {
                                        _excRto = Convert.ToDecimal(_dtMfZg.Rows[0]["EXC_RTO"]);
                                    }
                                    if (!String.IsNullOrEmpty(_drTf["DIS_CNT"].ToString()) && Convert.ToDecimal(_drTf["DIS_CNT"]) != 0)
                                    {
                                        _disCnt = Convert.ToDecimal(_drTf["DIS_CNT"]);
                                    }

                                    _drTf["AMT"] = _qty * _up * _disCnt / 100;
                                    Users _users = new Users();
                                    string _depUser = _users.GetUserDepNo(_usr);
                                    CompInfo _compInfo = Comp.GetCompInfo(_depUser);
                                    string _format = "{0:F" + _compInfo.DecimalDigitsInfo.System.POI_AMT + "}";
                                    switch (_dtMfPc.Rows[0]["TAX_ID"].ToString())
                                    {
                                        case "1":
                                            _drTf["AMTN_NET"] = String.Format(_format, Convert.ToDecimal(_drTf["AMT"]) * _excRto);
                                            _drTf["TAX"] = 0;
                                            break;
                                        case "2":
                                            _drTf["AMTN_NET"] = String.Format(_format, Convert.ToDecimal(_drTf["AMT"]) * _excRto * (100 / Convert.ToDecimal((100 + _taxRto))));
                                            _drTf["TAX"] = String.Format(_format, Convert.ToDecimal(_drTf["AMT"]) * _excRto - Convert.ToDecimal(_drTf["AMTN_NET"]));
                                            break;
                                        case "3":
                                            _drTf["AMTN_NET"] = String.Format(_format, Convert.ToDecimal(_drTf["AMT"]) * _excRto);
                                            _drTf["TAX"] = String.Format(_format, Convert.ToDecimal(_drTf["AMT"]) * _excRto * _taxRto / 100);
                                            break;
                                        default:
                                            _drTf["AMTN_NET"] = String.Format(_format, Convert.ToDecimal(_drTf["AMT"]));
                                            _drTf["TAX"] = 0;
                                            break;
                                    }
                                    bool _qz = false;
                                    if (dataRow.Table.DataSet.ExtendedProperties.ContainsKey("PAK_UNIT_QZ"))
                                    {
                                        _qz = (dataRow.Table.DataSet.ExtendedProperties["PAK_UNIT_QZ"].ToString() == "T");
                                    }
                                    _drTf["UP_MAIN"] = _drTf["UP"];
                                    _drTf["CST"] = _drTf["AMTN_NET"];

                                    CalculatePak(_drTf, _qz, false);

                                    _dsZb.Tables["TF_ZG"].Rows.Add(_drTf);
                                }
                            }
                        }
                        if (_dsZb.Tables["MF_ZG"].Rows.Count > 0)
                        {
                            this.UpdateData("DRPZB", _dsZb, _usr, true);
                        }
                    }
                    else
                    {
                        SunlikeDataSet _dsZg = this.GetData(dataRow["PS_ID", DataRowVersion.Original].ToString(),
                            dataRow["PS_NO", DataRowVersion.Original].ToString());
                        if (_dsZg.Tables["MF_ZG"].Rows.Count > 0)
                        {
                            if (isRollBack)
                            {
                                foreach (DataRow dr in _dsZg.Tables["MF_ZG"].Rows)
                                {
                                    this.RollBack(dr["ZG_ID"].ToString(), dr["ZG_NO"].ToString());
                                }
                            }
                            else
                            {
                                foreach (DataRow dr in _dsZg.Tables["MF_ZG"].Rows)
                                {
                                    dr.Delete();
                                }
                                this.UpdateData("DRPZB", _dsZg, dataRow["USR", DataRowVersion.Original].ToString(), true);
                            }
                        }
                    }

                    #endregion
                }
                else if (dataRow.Table.TableName == "TF_PSS")
                {
                    #region 回写暂估单

                    decimal _qty = 0;
                    string _zgId = "", _zgNo = "", _othItm = "", _chkMan = "";
                    DbDRPZG _dbDrpZg = new DbDRPZG(Comp.Conn_DB);
                    if (isAdd)
                    {
                        _zgId = dataRow["OS_ID"].ToString();
                        _zgNo = dataRow["OS_NO"].ToString();
                        _othItm = dataRow["OTH_ITM"].ToString();
                        _chkMan = _dtMfPc.Rows[0]["CHK_MAN"].ToString();
                        _qty = Convert.ToDecimal(dataRow["QTY"]);
                    }
                    else
                    {
                        _zgId = dataRow["OS_ID", DataRowVersion.Original].ToString();
                        _zgNo = dataRow["OS_NO", DataRowVersion.Original].ToString();
                        _othItm = dataRow["OTH_ITM", DataRowVersion.Original].ToString();
                        _chkMan = _dtMfPc.Rows[0]["CHK_MAN", DataRowVersion.Original].ToString();
                        _qty = Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]) * -1;
                    }
                    if (!String.IsNullOrEmpty(_chkMan)
                        && !String.IsNullOrEmpty(_zgId)
                        && !String.IsNullOrEmpty(_zgNo)
                        && !String.IsNullOrEmpty(_othItm))
                    {
                        _dbDrpZg.UpdateQtyPc(_zgId, _zgNo, Convert.ToInt32(_othItm), _qty);
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// 进货退回回写已交量
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="isAdd"></param>
        public void UpdateZgByPb(DataRow dataRow, bool isAdd)
        {
            DRPPC _drpPc = new DRPPC();
            DataSet _dsPc = dataRow.Table.DataSet;
            DataTable _dtMfPb = _dsPc.Tables["MF_PSS"];
            DataTable _dtTfPb = _dsPc.Tables["TF_PSS"];
            if (_dtMfPb.Rows.Count > 0)
            {
                string _poId = "", _osId = "", _osNo = "", _othItm = "";
                decimal _qty = 0;
                if (isAdd)
                {
                    _poId = _dtMfPb.Rows[0]["PO_ID"].ToString();
                    _osId = dataRow["OS_ID"].ToString();
                    _osNo = dataRow["OS_NO"].ToString();
                    _othItm = dataRow["OTH_ITM"].ToString();
                    _qty = Convert.ToDecimal(dataRow["QTY"]);
                }
                else
                {
                    _poId = _dtMfPb.Rows[0]["PO_ID", DataRowVersion.Original].ToString();
                    _osId = dataRow["OS_ID", DataRowVersion.Original].ToString();
                    _osNo = dataRow["OS_NO", DataRowVersion.Original].ToString();
                    _othItm = dataRow["OTH_ITM", DataRowVersion.Original].ToString();
                    _qty = Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]);
                }
                if (_osId == "PC" && _poId == "T")
                {
                    //取出进货单
                    using (DataSet _dsPcBody = _drpPc.GetBody(_osId, _osNo, "PRE_ITM", Convert.ToInt32(_othItm), true))
                    {
                        if (_dsPcBody.Tables["TF_PSS"].Rows.Count > 0)
                        {
                            DataRow _dr = _dsPcBody.Tables["TF_PSS"].Rows[0];
                            _osId = _dr["OS_ID"].ToString();
                            _osNo = _dr["OS_NO"].ToString();
                            _othItm = _dr["OTH_ITM"].ToString();
                            //根据进货单表身，逐笔取出暂估单
                            if (_osId == "ZG" && !String.IsNullOrEmpty(_osId)
                                && !String.IsNullOrEmpty(_osNo)
                                && !String.IsNullOrEmpty(_othItm))
                            {
                                using (SunlikeDataSet _dsZgBody = this.GetBody(_osId, _osNo, Convert.ToInt32(_othItm)))
                                {
                                    if (_dsZgBody.Tables["TF_ZG"].Rows.Count > 0)
                                    {
                                        _dr = _dsZgBody.Tables["TF_ZG"].Rows[0];
                                        _dr["QTY"] = _qty;
                                        _dr.AcceptChanges();
                                        _dr.SetModified();
                                        //根据暂估单回写采购单
                                        this.UpdateQtyRtn(_dr, !isAdd);
                                        this.UpdateWh(_dr, !isAdd, true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 暂估退回回写已交量
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="isAdd"></param>
        public void UpdateZgByZr(DataRow dataRow, bool isAdd)
        {
            DRPZG _drpPc = new DRPZG();
            DataSet _dsPc = dataRow.Table.DataSet;
            DataTable _dtMfPb = _dsPc.Tables["MF_ZG"];
            DataTable _dtTfPb = _dsPc.Tables["TF_ZG"];
            if (_dtMfPb.Rows.Count > 0)
            {
                string _poId = "", _osId = "", _osNo = "", _othItm = "";
                decimal _qty = 0;
                if (isAdd)
                {
                    _poId = _dtMfPb.Rows[0]["PO_ID"].ToString();
                    _osId = dataRow["OS_ID"].ToString();
                    _osNo = dataRow["OS_NO"].ToString();
                    _othItm = dataRow["OTH_ITM"].ToString();
                    _qty = Convert.ToDecimal(dataRow["QTY"]);
                }
                else
                {
                    _poId = _dtMfPb.Rows[0]["PO_ID", DataRowVersion.Original].ToString();
                    _osId = dataRow["OS_ID", DataRowVersion.Original].ToString();
                    _osNo = dataRow["OS_NO", DataRowVersion.Original].ToString();
                    _othItm = dataRow["OTH_ITM", DataRowVersion.Original].ToString();
                    _qty = Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]);
                }
                if (_osId == "ZG" && _poId == "T")
                {
                    //取出暂估单
                    using (DataSet _dsPcBody = _drpPc.GetBody(_osId, _osNo, Convert.ToInt32(_othItm)))
                    {
                        if (_dsPcBody.Tables["TF_ZG"].Rows.Count > 0)
                        {
                            DataRow _dr = _dsPcBody.Tables["TF_ZG"].Rows[0];
                            _dr["QTY"] = _qty;
                            _dr.AcceptChanges();
                            _dr.SetModified();
                            //根据暂估单回写采购单
                            this.UpdateQtyRtn(_dr, !isAdd);
                            this.UpdateWh(_dr, !isAdd, true);
                        }
                    }
                }
            }
        }

        #endregion

        #region 进货回冲回写

        /// <summary>
        /// 更新HC_ID
        /// </summary>
        /// <param name="zgId"></param>
        /// <param name="zgNo"></param>
        /// <param name="hcId"></param>
        public void UpdateHcId(string zgId, string zgNo, string hcId)
        {
            DbDRPZG _dbDrpZg = new DbDRPZG(Comp.Conn_DB);
            _dbDrpZg.UpdateHcId(zgId, zgNo, hcId);
        }

        #endregion

        #region IAuditing Members

        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData(chk_man, bil_id, bil_no, false);
                DataRow _drHead = _ds.Tables["MF_ZG"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_ZG"];
                DataTable _dtBar = _ds.Tables["TF_ZG_B"];
                _drHead["CHK_MAN"] = chk_man;
                _drHead["CLS_DATE"] = cls_dd;
                DbDRPZG _sa = new DbDRPZG(Comp.Conn_DB);
                //更新序列号记录
                DataTable _dtBarCopy = _dtBar.Copy();
                _dtBar.Clear();
                for (int i = 0; i < _dtBarCopy.Rows.Count; i++)
                {
                    DataRow _drBar = _dtBar.NewRow();
                    for (int j = 0; j < _dtBarCopy.Columns.Count; j++)
                    {
                        _drBar[j] = _dtBarCopy.Rows[i][j];
                    }
                    _dtBar.Rows.Add(_drBar);
                }
                _dtBarCopy.Dispose();
                GC.Collect(GC.GetGeneration(_dtBarCopy));
                _auditBarCode = true;
                this.UpdateBarCode(_ds);
                //修改库存
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    //修改分仓存量
                    this.UpdateWh(_dtBody.Rows[i], true, false);
                    //修改原进货单已退数量
                    this.UpdateQtyRtn(_dtBody.Rows[i], true);
                    //回写暂估
                    this.UpdateZgByZr(_dtBody.Rows[i], true);
                }

                //凭证模板不为空且单张立账
                string _vohNo = this.UpdateVohNo(_drHead, StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);
                //设定审核人
                DbDRPZG _drpPc = new DbDRPZG(Comp.Conn_DB);
                _drpPc.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd, _vohNo);
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }

        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            return "";
        }

        public string RollBack(string bil_id, string bil_no)
        {
            return this.RollBack(bil_id, bil_no, true);
        }

        /// <summary>
        /// 审核错误回退
        /// </summary>
        /// <param name="bil_id">单据识别码</param>
        /// <param name="bil_no">单据号码</param>
        /// <param name="isUpdateHead">更新表头审核信息</param>
        /// <returns>错误信息</returns>
        public string RollBack(string bil_id, string bil_no, bool isUpdateHead)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData("", bil_id, bil_no, false);
                //单据是否有销退数量或冲账
                this.SetCanModify(_ds, "", false, true);
                if (_ds.ExtendedProperties.Contains("CAN_MODIFY"))
                {
                    if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                    {
                        return "RCID=INV.HINT.CANMODIFY";
                    }
                }
                DataRow _drHead = _ds.Tables["MF_ZG"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_ZG"];
                DataTable _dtBar = _ds.Tables["TF_ZG_B"];
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    //修改分仓存量
                    this.UpdateWh(_dtBody.Rows[i], false, false);
                    //修改原单已退数量
                    this.UpdateQtyRtn(_dtBody.Rows[i], false);
                    //回写暂估
                    this.UpdateZgByZr(_dtBody.Rows[i], false);
                }

                if (_drHead["OS_ID"].ToString() == "SA")
                {
                    //回写销货单
                    //DbDRPPC _dbDrpPc = new DbDRPPC(Comp.Conn_DB);
                    //if (!_dbDrpPc.UpdatePoNo("", "", _drHead["OS_NO"].ToString()))
                    //{
                    //    return "更新销货单错误！";
                    //}
                }
                //更新序列号记录
                for (int i = 0; i < _dtBar.Rows.Count; i++)
                {
                    _dtBar.Rows[i].Delete();
                }
                _auditBarCode = true;
                this.UpdateBarCode(_ds);
                //更新凭证
                this.UpdateVohNo(_drHead, StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");
                //设定审核人
                if (isUpdateHead)
                {
                    DbDRPZG _drpPc = new DbDRPZG(Comp.Conn_DB);
                    _drpPc.UpdateChkMan(bil_id, bil_no, "", DateTime.Now, "");
                }
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }

        #endregion
    }
}
