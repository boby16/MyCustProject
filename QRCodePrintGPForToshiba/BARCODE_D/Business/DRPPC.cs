/*
 * modify: cjc 090805 
 * 审核流程的改动(单据别和审核模板)
 */

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
    /// Summary description for DRPPC.
    /// </summary>
    public class DRPPC : BizObject, IAuditing, ICloseBill
    {
        private bool _isRunAuditing;
        private bool _auditBarCode;
        private int _barCodeNo;
        private string _bilID = "";
        /// <summary>
        /// 是否重新切制凭证
        /// </summary>
        private bool _reBuildVohNo = false;
        private System.Collections.Hashtable _htPrdNo = new System.Collections.Hashtable();
        private System.Collections.Hashtable _htWh = new System.Collections.Hashtable();

        private System.Collections.ArrayList _alBlNo = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alBlItm = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alPrdNo = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alUnit = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alQty = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alQty1 = new System.Collections.ArrayList();
        /// <summary>
        /// 进货单
        /// </summary>
        public DRPPC()
        {
        }

        #region GetData
        /// <summary>
        /// 取得进货单资料（WebForm审核流程用）
        /// </summary>
        /// <param name="usr">当前操作用户</param>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string PsID, string PsNo)
        {
            SunlikeDataSet _ds = GetData(usr, PsID, PsNo, false, false);
            //增加配码比描述列
            DataTable _dtBox = _ds.Tables["TF_PSS_BOX"];
            if (!_dtBox.Columns.Contains("CONTENT_DSC"))
            {
                DataColumn _dc = new DataColumn("CONTENT_DSC");
                _ds.Tables["TF_PSS_BOX"].Columns.Add(_dc);
            }
            BarBox _box = new BarBox();
            for (int i = 0; i < _dtBox.Rows.Count; i++)
            {
                _dtBox.Rows[i]["CONTENT_DSC"] = _box.GetBar_BoxDsc(_dtBox.Rows[i]["PRD_NO"].ToString(), _dtBox.Rows[i]["CONTENT"].ToString());
            }
            return _ds;
        }
        /// <summary>
        /// 取得进货单资料（WinForm）
        /// </summary>
        /// <param name="usr">当前操作用户</param>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="OnlyFillSchema">是否只读取Schema</param>
        /// <param name="HasDsc">是否处理特征分段</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string PsID, string PsNo, bool OnlyFillSchema, bool HasDsc)
        {
            DbDRPPC _drpPc = new DbDRPPC(Comp.Conn_DB);
            SunlikeDataSet _ds = _drpPc.GetData(PsID, PsNo, OnlyFillSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {

                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;

                //管制立帐方式
                if (INVCommon.IsControlZhangId(usr, PsID))
                {
                    _ds.ExtendedProperties["CTRL_ZHANG_ID"] = "T";
                }
            }
            //转入单据的数量
            _ds.Tables["TF_PSS"].Columns.Add(new DataColumn("QTY_SO_ORG", typeof(System.Decimal)));
            //增加单据权限
            if (!OnlyFillSchema)
            {
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    string _pgm = "DRP" + PsID;
                    DataTable _dtMf = _ds.Tables["MF_PSS"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                        this.SetCanModify(_ds, true, false);
                    }
                }
            }
            //设定表身的PreItm为自动递增
            DataTable _dtTfPss = _ds.Tables["TF_PSS"];
            _dtTfPss.Columns["PRE_ITM"].AutoIncrement = true;
            _dtTfPss.Columns["PRE_ITM"].AutoIncrementSeed = _dtTfPss.Rows.Count > 0 ? Convert.ToInt32(_dtTfPss.Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _dtTfPss.Columns["PRE_ITM"].AutoIncrementStep = 1;
            _dtTfPss.Columns["PRE_ITM"].Unique = true;
            _dtTfPss.Columns.Add("PRD_NO_NO", typeof(System.String));
            foreach (DataRow _drTfPss in _dtTfPss.Rows)
            {
                _drTfPss["PRD_NO_NO"] = _drTfPss["PRD_NO"];
            }
            if (HasDsc)
            {
                //取得特征分段数据
                PrdMark _mark = new PrdMark();
                DataTable _dtMark = _mark.GetSplitData("");
                //加入表身特征分段栏位
                for (int i = 0; i < _dtMark.Rows.Count; i++)
                {
                    _dtTfPss.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString());
                    _dtTfPss.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString() + "_DSC");
                }
                //整理表身特征分段栏位
                if (_dtMark.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtTfPss.Rows.Count; i++)
                    {
                        string _prdMark = _dtTfPss.Rows[i]["PRD_MARK"].ToString();
                        if (!String.IsNullOrEmpty(_prdMark))
                        {
                            string[] _aryPrdMark = _mark.BreakPrdMark(_prdMark);
                            for (int j = 0; j < _dtMark.Rows.Count; j++)
                            {
                                string _fldName = _dtMark.Rows[j]["FLDNAME"].ToString();
                                _dtTfPss.Rows[i][_fldName] = _aryPrdMark[j];
                            }
                        }
                    }
                }
            }
            else
            {
                _dtTfPss.Columns.Remove("WH_NAME");
                _dtTfPss.Columns.Remove("UNIT_NAME");
                //表身加库存量栏位
                _dtTfPss.Columns.Add(new DataColumn("WH_QTY"));
                _dtTfPss.Columns.Add(new DataColumn("UP_CST", typeof(decimal)));
            }
            //进货单中单位标准成本
            _dtTfPss.Columns.Add(new DataColumn("CST_STD_UNIT", typeof(decimal)));
            if (PsID == "PD")
            {
                _dtTfPss.Columns.Add(new DataColumn("AMT_OLD", typeof(System.Decimal)));
            }
            //给单位标准成本赋值
            DRPPO _drpPo = new DRPPO();
            DRPSA _drpSa = new DRPSA();
            for (int i = 0; i < _ds.Tables["TF_PSS"].Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(_ds.Tables["TF_PSS"].Rows[i]["CST_STD"].ToString())
                    && !String.IsNullOrEmpty(_ds.Tables["TF_PSS"].Rows[i]["QTY"].ToString())
                    && Convert.ToDecimal(_ds.Tables["TF_PSS"].Rows[i]["QTY"]) != 0)
                    _ds.Tables["TF_PSS"].Rows[i]["CST_STD_UNIT"] = Convert.ToDecimal(_ds.Tables["TF_PSS"].Rows[i]["CST_STD"]) / Convert.ToDecimal(_ds.Tables["TF_PSS"].Rows[i]["QTY"]);
                else
                    _ds.Tables["TF_PSS"].Rows[i]["CST_STD_UNIT"] = 0;

                //取采购量
                if (_ds.Tables["TF_PSS"].Rows[i]["OS_ID"].ToString() == "PO")
                {
                    using (DataSet _dsPo = _drpPo.GetBody("PO", _ds.Tables["TF_PSS"].Rows[i]["OS_NO"].ToString(), "PRE_ITM", Convert.ToInt32(_ds.Tables["TF_PSS"].Rows[i]["EST_ITM"]), true))
                    {
                        if (_dsPo.Tables["TF_POS"].Rows.Count > 0)
                        {
                            _ds.Tables["TF_PSS"].Rows[i]["QTY_SO_ORG"] = _dsPo.Tables["TF_POS"].Rows[0]["QTY"];
                        }
                    }
                }
                else if (_ds.Tables["TF_PSS"].Rows[i]["OS_ID"].ToString() == "SA")
                {//取销货单 
                    using (DataSet _dsSa = _drpSa.GetBodyData("SA", _ds.Tables["TF_PSS"].Rows[i]["OS_NO"].ToString(), Convert.ToInt32(_ds.Tables["TF_PSS"].Rows[i]["EST_ITM"])))
                    {
                        if (_dsSa.Tables["TF_PSS"].Rows.Count > 0)
                        {
                            _ds.Tables["TF_PSS"].Rows[i]["QTY_SO_ORG"] = _dsSa.Tables["TF_PSS"].Rows[0]["QTY"];
                        }
                    }
                }
                else if (_ds.Tables["TF_PSS"].Rows[i]["OS_ID"].ToString() == "ZG")
                {
                    DRPZG _drpZg = new DRPZG();
                    using (DataSet _dsZg = _drpZg.GetBody("ZG", _ds.Tables["TF_PSS"].Rows[i]["OS_NO"].ToString(),
                        Convert.ToInt32(_ds.Tables["TF_PSS"].Rows[i]["OTH_ITM"])))
                    {
                        if (_dsZg.Tables["TF_ZG"].Rows.Count > 0)
                        {
                            _ds.Tables["TF_PSS"].Rows[i]["QTY_SO_ORG"] = _dsZg.Tables["TF_ZG"].Rows[0]["QTY"];
                        }
                    }
                }
                else if (_ds.Tables["TF_PSS"].Rows[i]["OS_ID"].ToString() == "BN")
                {
                    DRPBN _drpBn = new DRPBN();
                    using (DataSet _dsBn = _drpBn.GetBodyData("BN", _ds.Tables["TF_PSS"].Rows[i]["OS_NO"].ToString(),
                        Convert.ToInt32(_ds.Tables["TF_PSS"].Rows[i]["EST_ITM"])))
                    {
                        if (_dsBn.Tables["TF_BLN"].Rows.Count > 0)
                        {
                            _ds.Tables["TF_PSS"].Rows[i]["QTY_SO_ORG"] = _dsBn.Tables["TF_BLN"].Rows[0]["QTY"];
                        }
                    }
                }
                //计算折扣前金额
                if (PsID == "PD")
                {
                    DataRow _drTfPss = _ds.Tables["TF_PSS"].Rows[i];
                    if (String.IsNullOrEmpty(_drTfPss["AMT_OLD"].ToString()) && !String.IsNullOrEmpty(_drTfPss["UP"].ToString()) && !String.IsNullOrEmpty(_drTfPss["QTY"].ToString()))
                    {
                        _drTfPss["AMT_OLD"] = Convert.ToDecimal(_drTfPss["UP"]) * Convert.ToDecimal(_drTfPss["QTY"]);
                    }
                }
            }
            _dtTfPss.Columns.Add("UNIT_DP", typeof(String));
            //为箱内容增加配码比描述列
            DataTable _dtBox = _ds.Tables["TF_PSS_BOX"];
            DataColumn _dc = new DataColumn("CONTENT_DSC");
            _dtBox.Columns.Add(_dc);
            _dtBox.Columns.Add("PRD_NO_NO", typeof(System.String));
            BarBox _box = new BarBox();
            for (int i = 0; i < _dtBox.Rows.Count; i++)
            {
                _dtBox.Rows[i]["CONTENT_DSC"] = _box.GetBar_BoxDsc(_dtBox.Rows[i]["PRD_NO"].ToString(), _dtBox.Rows[i]["CONTENT"].ToString());
                _dtBox.Rows[i]["PRD_NO_NO"] = _dtBox.Rows[i]["PRD_NO"];
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
            DataView _dv = new DataView(_ds.Tables["TF_PSS_BAR"]);
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
                DataRow[] _aryDrBar = _ds.Tables["TF_PSS_BAR"].Select("BAR_CODE='" + dr["BAR_CODE"].ToString() + "'");
                if (_aryDrBar.Length > 0)
                {
                    DataRow[] _aryDr = _ds.Tables["TF_PSS"].Select("PRE_ITM=" + _aryDrBar[0]["PS_ITM"].ToString());
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
        /// 取进货单(进货退回转单时用)
        /// </summary>
        /// <param name="psNo">进货单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForPB(string[] psNo)
        {
            StringBuilder _psNo = new StringBuilder();
            for (int i = 0; i < psNo.Length; i++)
            {
                if (!String.IsNullOrEmpty(_psNo.ToString()))
                {
                    _psNo.Append(",");
                }
                _psNo.Append("'" + psNo[i] + "'");
            }
            DbDRPPC _drpPc = new DbDRPPC(Comp.Conn_DB);
            SunlikeDataSet _ds = _drpPc.GetDataForPB(_psNo.ToString());
            BarBox _bar = new BarBox();
            DataColumn _dc = new DataColumn("CONTENT_DSC", Type.GetType("System.String"));
            _dc.MaxLength = 64 * 1024;
            _ds.Tables["TF_PSS_BOX"].Columns.Add(_dc);
            foreach (DataRow _drTF in _ds.Tables["TF_PSS_BOX"].Rows)
            {
                _drTF["CONTENT_DSC"] = _bar.GetBar_BoxDsc(_drTF["PRD_NO"].ToString(), _drTF["CONTENT"].ToString());
            }
            _ds.Tables["TF_PSS_BOX"].AcceptChanges();
            return _ds;
        }

        /// <summary>
        /// 取进货单(进货单转进货折让时用)
        /// </summary>
        /// <param name="psNo">进货单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForPD(string[] psNo)
        {
            StringBuilder _psNo = new StringBuilder();
            for (int i = 0; i < psNo.Length; i++)
            {
                if (!String.IsNullOrEmpty(_psNo.ToString()))
                {
                    _psNo.Append(",");
                }
                _psNo.Append("'" + psNo[i] + "'");
            }
            DbDRPPC _drpPc = new DbDRPPC(Comp.Conn_DB);
            SunlikeDataSet _ds = _drpPc.GetDataForPD(_psNo.ToString());
            BarBox _bar = new BarBox();
            DataColumn _dc = new DataColumn("CONTENT_DSC", Type.GetType("System.String"));
            _dc.MaxLength = 64 * 1024;
            _ds.Tables["TF_PSS_BOX"].Columns.Add(_dc);
            foreach (DataRow _drTF in _ds.Tables["TF_PSS_BOX"].Rows)
            {
                _drTF["CONTENT_DSC"] = _bar.GetBar_BoxDsc(_drTF["PRD_NO"].ToString(), _drTF["CONTENT"].ToString());
            }
            _ds.Tables["TF_PSS_BOX"].AcceptChanges();
            return _ds;
        }
        /// <summary>
        /// 取退回数
        /// </summary>
        /// <param name="PsID">单据别</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="Itm">项次</param>
        /// <returns></returns>
        public decimal GetQtyRtn(string PsID, string PsNo, int Itm)
        {
            DbDRPPC _drpPc = new DbDRPPC(Comp.Conn_DB);
            return _drpPc.GetQtyRtn(PsID, PsNo, Itm);
        }

        /// <summary>
        /// 取得表身数据
        /// </summary>
        /// <param name="psId">单据别</param>
        /// <param name="psNo">单号</param>
        /// <param name="itmColumnName">项次名</param>
        /// <param name="itm">项次值</param>
        /// <param name="isPrimaryUnit">是否转成主单位数量</param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string psId, string psNo, string itmColumnName, int itm, bool isPrimaryUnit)
        {
            Sunlike.Business.Data.DbDRPPC _pc = new DbDRPPC(Comp.Conn_DB);
            return _pc.GetBody(psId, psNo, itmColumnName, itm, isPrimaryUnit);
        }
        #endregion

        #region 检查单据是否可以修改
        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        /// <param name="IsRollBack"></param>
        private void SetCanModify(SunlikeDataSet ds, bool bCheckAuditing, bool IsRollBack)
        {
            DataTable _dtMf = ds.Tables["MF_PSS"];
            DataTable _dtTf = ds.Tables["TF_PSS"];
            if (_dtMf.Rows.Count > 0)
            {
                bool _bCanModify = true;
                if (_dtMf.Rows[0]["LZ_CLS_ID"].ToString() == "T")	//已立帐结案不能修改
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LZCLOSE_MODIFY");
                }
                if (_dtMf.Rows[0]["ZHANG_ID"].ToString() == "3")
                {
                    DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
                    SunlikeDataSet _dsInv = _dbSa.GetInvBill(_dtMf.Rows[0]["PS_ID"].ToString(), _dtMf.Rows[0]["PS_NO"].ToString());
                    if (_dsInv.Tables[0].Rows.Count > 0)
                    {
                        _bCanModify = false;
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LZ_MODIFY");
                    }
                }
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["PS_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                }
                if (bCheckAuditing)
                {
                    string _psID = _dtMf.Rows[0]["PS_ID"].ToString();
                    string _psNo = _dtMf.Rows[0]["PS_NO"].ToString();
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_psID, _psNo))
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
                    //是否有进货退回
                    DataRow[] _aryDrRtn = _dtTf.Select("QTY_RTN>0 OR QTY_RTN_UNSH>0 ");
                    if (_aryDrRtn.Length > 0)
                    {
                        _bCanModify = false;
                    }
                }
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["ARP_NO"].ToString()))
                {
                    Arp _arp = new Arp();
                    if (!String.IsNullOrEmpty(_dtMf.Rows[0]["ARP_NO"].ToString()))
                    {
                        try
                        {
                            if (_arp.HasReceiveDollar(_dtMf.Rows[0]["ARP_NO"].ToString()))
                            {
                                //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LZ_MODIFY");
                                _bCanModify = false;
                            }
                        }
                        catch { }
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
                //判断进货回冲
                if (_bCanModify)
                {
                    DRPHC _drpHc = new DRPHC();
                    SunlikeDataSet _ds = _drpHc.GetData(_dtMf.Rows[0]["PS_ID"].ToString(), _dtMf.Rows[0]["PS_NO"].ToString());
                    if (_ds.Tables["TF_HC"].Rows.Count > 0)
                    {
                        _bCanModify = false;
                    }
                }
                ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存单据内容
        /// </summary>
        /// <param name="ChangedDS">DataSet</param>
        /// <param name="bubbleException">是否抛出异常（true为直接抛出异常，false返回ErrorTable）</param>
        /// <returns></returns>
        public DataTable UpdateData(SunlikeDataSet ChangedDS, bool bubbleException)
        {
            ChangedDS.Tables["TF_PSS_BAR"].TableName = "TF_PSS3";
            ChangedDS.Tables["TF_PSS_BOX"].TableName = "TF_PSS4";
            string _psID, _usr;
            string _bilType;
            string _mobID = "";
            DataRow _dr = ChangedDS.Tables["MF_PSS"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _psID = _dr["PS_ID", DataRowVersion.Original].ToString();
                _usr = _dr["USR", DataRowVersion.Original].ToString();
                _bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
                if (_dr.Table.Columns.Contains("MOB_ID"))
                {
                    _mobID = _dr["MOB_ID", DataRowVersion.Original].ToString();
                }
            }
            else
            {
                _psID = _dr["PS_ID"].ToString();
                _usr = _dr["USR"].ToString();
                _bilType = _dr["BIL_TYPE"].ToString();
                if (_dr.Table.Columns.Contains("MOB_ID"))
                {
                    _mobID = _dr["MOB_ID"].ToString();
                }
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
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_PSS"] = "PS_ID,PS_NO,PS_DD,PAY_DD,CHK_DD,CUS_NO,DEP,TAX_ID,SEND_MTH,SEND_WH,ZHANG_ID,EXC_RTO,SAL_NO,ARP_NO,PAY_MTH,PAY_DAYS,AMT,"
                + "CHK_DAYS,INT_DAYS,PAY_REM,CLS_ID,USR,CHK_MAN,PRT_SW,CLS_DATE,CK_CLS_ID,LZ_CLS_ID,YD_ID,BIL_TYPE,PH_NO,CUST_YG,OS_ID,OS_NO,"
                + "PO_ID,SYS_DATE,BIL_NO,REM,TOT_QTY,BAT_NO,CUR_ID,DIS_CNT,VOH_ID,VOH_NO,INV_NO,KP_ID,ADR,MOB_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,TURN_ID,ACC_FP_NO,CLSLZ,EP_NO,EP_NO1,AMTN_EP,AMTN_EP1";

            _ht["TF_PSS"] = "PS_ID,PS_NO,ITM,PS_DD,WH,PRD_NO,PRD_NAME,PRD_MARK,UNIT,QTY,UP,CST_STD,DIS_CNT,AMTN_NET,AMT,UP_SALE,AMTN_SALE,PRE_ITM,OS_NO,OS_ID,QTY_RTN,CSTN_SAL,AMTN,TAX,OTH_ITM,EST_ITM,VALID_DD,BAT_NO,TAX_RTO,BOX_ITM,REM,FREE_ID,QTY_PS,PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,SH_NO_CUS,QTY1,UP_QTY1,AMTN_NET_FP,AMT_FP,TAX_FP,QTY_CK,QTY_FP,UP_MAIN,CST_SAL,AMTN_EP,RK_DD,DEP_RK";
            _ht["TF_PSS4"] = "PS_ID,PS_NO,ITM,PRD_NO,CONTENT,QTY,KEY_ITM,WH";
            //判断是否走审核流程
            Auditing _auditing = new Auditing();

            //_isRunAuditing = _auditing.IsRunAuditing(_psID, _usr, _bilType, _mobID);


            this.UpdateDataSet(ChangedDS, _ht);
            ChangedDS.Tables["TF_PSS3"].TableName = "TF_PSS_BAR";
            ChangedDS.Tables["TF_PSS4"].TableName = "TF_PSS_BOX";
            //判断单据能否修改
            if (!ChangedDS.HasErrors)
            {
                //增加单据权限
                string _UpdUsr = "";
                if (ChangedDS.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = ChangedDS.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "DRP" + _psID;
                    DataTable _dtMf = ChangedDS.Tables["MF_PSS"];
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
                this.SetCanModify(ChangedDS, true, false);
                ChangedDS.AcceptChanges();
            }
            else if (bubbleException)
            {
                string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(ChangedDS);
                throw new SunlikeException("DRPPC.UpdateData() Error:" + _errorMsg);
            }
            return GetAllErrors(ChangedDS);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dtMf = ds.Tables["MF_PSS"];
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["PS_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["PS_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion
            if (ds.Tables["MF_PSS"].Rows.Count > 0
                && ds.Tables["MF_PSS"].Rows[0].RowState == DataRowState.Modified
                && this._isRunAuditing
                && !String.IsNullOrEmpty(ds.Tables["MF_PSS"].Rows[0]["CHK_MAN"].ToString()))
            {
                string _rbError = this.RollBack(ds.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString(),
                    ds.Tables["MF_PSS"].Rows[0]["PS_NO"].ToString(), false);
                if (!String.IsNullOrEmpty(_rbError))
                {
                    throw new SunlikeException(_rbError);
                }
            }
            base.BeforeDsSave(ds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            if (this._alBlNo.Count > 0)
            {
                DRPBN _obj = new DRPBN();
                _obj.UpdateQtyRtn(this._alBlNo, this._alBlItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
            }
        }


        #region BeforeUpdate
        /// <summary>
        /// 保存单据之前的动作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            string _psId = "", _psNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _psId = dr["PS_ID"].ToString();
                _psNo = dr["PS_NO"].ToString();
            }
            else
            {
                _psId = dr["PS_ID", DataRowVersion.Original].ToString();
                _psNo = dr["PS_NO", DataRowVersion.Original].ToString();
            }
            if (statementType != StatementType.Insert)
            {
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "PS_ID = '" + _psId + "' AND PS_NO = '" + _psNo + "'";
                if (_Users.IsLocked("MF_PSS", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            if (tableName == "MF_PSS")
            {
                #region MF_PSS
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
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["PS_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    else
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["PS_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    //如果立帐方式由“单张立帐”改为“不立帐”时，清空立帐单号
                    if (dr.RowState != DataRowState.Added && dr["ZHANG_ID"].ToString() != dr["ZHANG_ID", DataRowVersion.Original].ToString())
                    {
                        dr["ARP_NO"] = System.DBNull.Value;
                    }
                    //检查进货厂商
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString(), Convert.ToDateTime(dr["PS_DD"])))
                    {
                        dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//客户代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //检查业务员
                    Salm _salm = new Salm();
                    if (!_salm.IsExist(_usr, dr["SAL_NO"].ToString(), Convert.ToDateTime(dr["PS_DD"])))
                    {
                        dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //检查部门
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["PS_DD"])))
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
                    dr["PS_NO"] = _sq.Set(_psId, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["PS_DD"]), dr["BIL_TYPE"].ToString());
                    //写入默认栏位值
                    dr["PRT_SW"] = "N";
                    if (_psId == "PC")
                    {
                        dr["CK_CLS_ID"] = "F";
                        dr["LZ_CLS_ID"] = "F";
                    }
                    //取得交易方式
                    if (String.IsNullOrEmpty(dr["PAY_MTH"].ToString()))
                    {
                        Cust _cust = new Cust();
                        System.Collections.Hashtable _ht = _cust.GetPAYInfo(dr["CUS_NO"].ToString(), dr["PS_DD"].ToString());
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
                    //					DRPEN _drpEn = new DRPEN();
                    //					if (_drpEn.IsExists("PC",_psNo))
                    //					{
                    //						throw new SunlikeException("RCID=INV.HINT.HASEND");/*此单已做结案变更，请先删除结案变更单。*/
                    //					}
                    if (statementType == StatementType.Delete)
                    {
                        #region 删除
                        string _error = _sq.Delete(dr["PS_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                        if (!String.IsNullOrEmpty(_error))
                        {
                            throw new Exception("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
                        }
                        //判断是否走审核流程
                        //if (_isRunAuditing)
                        //{
                        //    Auditing _auditing = new Auditing();
                        //    _auditing.DelBillWaitAuditing("DRP", _psId, dr["PS_NO", DataRowVersion.Original].ToString());
                        //}
                        //if (!String.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                        //{
                        //    DrpVoh _drpVoh = new DrpVoh();
                        //    _drpVoh.VohDel(dr["VOH_NO", DataRowVersion.Original].ToString());
                        //}
                        //删除费用单
                        if (!String.IsNullOrEmpty(dr["EP_NO", DataRowVersion.Original].ToString()))
                        {
                            MonEX _monEx = new MonEX();
                            try
                            {
                                _monEx.DelExpForBill(_psId, dr["PS_NO", DataRowVersion.Original].ToString());
                            }
                            catch (Exception _ex)
                            {
                                throw _ex;
                            }
                        }
                        if (!String.IsNullOrEmpty(dr["EP_NO1", DataRowVersion.Original].ToString()))
                        {
                            MonEX _monEx = new MonEX();
                            try
                            {
                                _monEx.DelExpForBill(_psId, dr["PS_NO", DataRowVersion.Original].ToString());
                            }
                            catch (Exception _ex)
                            {
                                throw _ex;
                            }
                        }
                        #endregion
                    }
                }
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (dr["PS_ID"].ToString() == "PC" || dr["PS_ID"].ToString() == "PB")
                    {
                        #region 更新其它费用单
                        if (dr.Table.DataSet.Tables["TF_EXP_ER"] != null)
                        {
                            try
                            {
                                foreach (DataRow drExp in dr.Table.DataSet.Tables["TF_EXP_ER"].Rows)
                                {
                                    if (drExp.RowState != DataRowState.Deleted)
                                    {
                                        drExp["BIL_ID"] = dr["PS_ID"];
                                        drExp["BIL_NO"] = dr["PS_NO"];
                                    }
                                }
                                if (dr.Table.DataSet.Tables["TF_EXP_ER"].Rows.Count > 0)
                                {
                                    UpdateExp(SunlikeDataSet.ConvertTo(dr.Table.DataSet), "ER", dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), _isRunAuditing);
                                }
                                decimal _amtnNetEp1 = 0;
                                foreach (DataRow drExp in dr.Table.DataSet.Tables["TF_EXP_ER"].Select())
                                {
                                    if (!String.IsNullOrEmpty(drExp["AMTN_NET"].ToString()))
                                    {
                                        _amtnNetEp1 += Convert.ToDecimal(drExp["AMTN_NET"]);
                                    }
                                }
                                if (dr.Table.DataSet.Tables["TF_EXP_ER"].Rows.Count > 0)
                                {
                                    dr["AMTN_EP1"] = _amtnNetEp1;
                                }
                            }
                            catch (Exception _ex)
                            {
                                throw _ex;
                            }
                        }
                        if (dr.Table.DataSet.Tables["TF_EXP_EP"] != null)
                        {
                            try
                            {
                                foreach (DataRow drExp in dr.Table.DataSet.Tables["TF_EXP_EP"].Rows)
                                {
                                    if (drExp.RowState != DataRowState.Deleted)
                                    {
                                        drExp["BIL_ID"] = dr["PS_ID"];
                                        drExp["BIL_NO"] = dr["PS_NO"];
                                    }
                                }
                                if (dr.Table.DataSet.Tables["TF_EXP_EP"].Rows.Count > 0)
                                {
                                    UpdateExp(SunlikeDataSet.ConvertTo(dr.Table.DataSet), "EP", dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), _isRunAuditing);
                                }
                                decimal _amtnNetEp = 0;
                                foreach (DataRow drExp in dr.Table.DataSet.Tables["TF_EXP_EP"].Select())
                                {
                                    if (!String.IsNullOrEmpty(drExp["AMTN_NET"].ToString()))
                                    {
                                        _amtnNetEp += Convert.ToDecimal(drExp["AMTN_NET"]);
                                    }
                                }
                                if (dr.Table.DataSet.Tables["TF_EXP_EP"].Rows.Count > 0)
                                {
                                    dr["AMTN_EP"] = _amtnNetEp;
                                }
                            }
                            catch (Exception _ex)
                            {
                                throw _ex;
                            }
                        }
                        #endregion
                    }
                }
                //#region 审核相关
                //AudParamStruct _aps;
                //if (dr.RowState != DataRowState.Deleted)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["PS_DD"]);
                //    _aps.BIL_ID = dr["PS_ID"].ToString();
                //    _aps.BIL_NO = dr["PS_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //{
                //    _aps = new AudParamStruct(Convert.ToString(dr["PS_ID", DataRowVersion.Original]), Convert.ToString(dr["PS_NO", DataRowVersion.Original]));
                //}
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}

                //#endregion

                #region 更新凭证
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }

                #endregion
                //更新开发票
                //try
                //{
                //    DrpTaxAa _drpTaxAa = new DrpTaxAa();
                //    _drpTaxAa.UpdateTaxAa(dr.Table.DataSet, _psId, _psNo);
                //}
                //catch (Exception _ex)
                //{
                //    dr.SetColumnError("INV_NO", _ex.Message);
                //    status = UpdateStatus.SkipAllRemainingRows;
                //}

                #endregion
            }
            else if (tableName == "TF_PSS")
            {
                #region 新增或者修改时
                if (statementType != StatementType.Delete)
                {
                    string _usr = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["USR"].ToString();
                    Prdt _prdt = new Prdt();
                    //检查货品代号
                    if (_htPrdNo[dr["PRD_NO"].ToString()] == null)
                    {
                        if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_DD"])))
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
                        if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_DD"])))
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
                        _bat.AutoInsertData(dr["BAT_NO"].ToString(), dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["PS_DD"]));
                    }
                    if (dr["OS_ID"].ToString() == "PO" || dr["OS_ID"].ToString() == "ZG" || dr["OS_ID"].ToString() == "BN")
                    {
                        //赋值回冲量
                        dr["QTY_PS"] = dr["QTY"];
                    }
                }
                else
                {
                    //判断是否有补开发票则不允许删除
                    checktflz(dr);
                }
                #endregion
            }
            if (_barCodeNo == 0)
            {
                #region 更新序列号记录["TF_PSS3"]
                if (!_isRunAuditing)
                {
                    this.UpdateBarCode(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                }
                if (dr.Table.DataSet.Tables["MF_PSS"].Rows[0].RowState == DataRowState.Deleted)
                {
                    Query _query = new Query();
                    _query.RunSql("delete from TF_PSS3 where PS_ID='" + dr["PS_ID", DataRowVersion.Original].ToString()
                        + "' and PS_NO='" + dr["PS_NO", DataRowVersion.Original].ToString() + "'");
                }
                else
                {
                    string _fieldList = "PS_ID,PS_NO,PS_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
                    SQLBatchUpdater _sbu = new SQLBatchUpdater(Comp.Conn_DB);
                    _sbu.BatchUpdateSize = 50;
                    _sbu.BatchUpdate(dr.Table.DataSet.Tables["TF_PSS3"], _fieldList);
                }
                #endregion
            }
            _barCodeNo++;

            if (!_isRunAuditing && tableName == "MF_PSS" && !dr.Table.DataSet.ExtendedProperties.ContainsKey("FROMLPLI"))
            {
                //立账
                this.UpdateMfArp(dr);
                //更新SARP
                this.UpdateSarp(dr);
            }
            //判断是否走审核流程
            if (!_isRunAuditing && _psId == "PB")
            {
                if (tableName == "TF_PSS")
                {
                    DRPZG _drpZg = new DRPZG();
                    if (statementType == StatementType.Insert)
                    {
                        _drpZg.UpdateZgByPb(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        _drpZg.UpdateZgByPb(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        _drpZg.UpdateZgByPb(dr, false);
                        _drpZg.UpdateZgByPb(dr, true);
                    }
                }
            }
        }
        #endregion

        #region AfterUpdate
        /// <summary>
        /// 保存单据之后的动作
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            string _psId = "";
            string _psNo = "";
            string _osId = "";
            string _osNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _psId = dr["PS_ID"].ToString();
                _psNo = dr["PS_NO"].ToString();
                if (tableName != "TF_PSS4")
                {
                    _osId = dr["OS_ID"].ToString();
                    _osNo = dr["OS_NO"].ToString();
                }
            }
            else
            {
                _psId = dr["PS_ID", DataRowVersion.Original].ToString();
                _psNo = dr["PS_NO", DataRowVersion.Original].ToString();
                if (tableName != "TF_PSS4")
                {
                    _osId = dr["OS_ID", DataRowVersion.Original].ToString();
                    _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                }
            }
            //判断是否走审核流程
            if (_isRunAuditing)
            {
                if (tableName == "MF_PSS")
                {
                    if (statementType != StatementType.Delete)
                    {
                        //dr["CHK_MAN"] = System.DBNull.Value;
                        //dr["CLS_DATE"] = System.DBNull.Value;							
                        //AudParamStructE _aps;
                        //_aps.BIL_DD = Convert.ToDateTime(dr["PS_DD"]);
                        //_aps.BIL_ID = dr["PS_ID"].ToString();
                        //_aps.BIL_NO = dr["PS_NO"].ToString();
                        //_aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                        //_aps.CUS_NO = dr["CUS_NO"].ToString();
                        //_aps.DEP = dr["DEP"].ToString();
                        //_aps.SAL_NO = dr["SAL_NO"].ToString();
                        //_aps.USR = dr["USR"].ToString();
                        //Auditing _auditing = new Auditing();
                        //_auditing.SetBillToAudtingFlow("DRP",_aps,null);
                    }
                }
            }
            else
            {
                if (tableName == "MF_PSS" && _osId == "SA")
                {
                    //回写销货单
                    DbDRPPC _dbDrpPc = new DbDRPPC(Comp.Conn_DB);
                    if (statementType != StatementType.Delete)
                    {
                        if (!_dbDrpPc.UpdatePoNo(_psId, _psNo, _osNo))
                        {
                            dr.SetColumnError("PS_NO", "回写销货单错误！");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    else
                    {
                        if (!_dbDrpPc.UpdatePoNo("", "", _osNo))
                        {
                            dr.SetColumnError("PS_NO", "回写销货单错误！");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
                if (tableName == "TF_PSS")//&& _psId != "PD")//lzj bugNo89879
                {
                    #region 更新产品库存

                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateWh(dr, true);
                        this.UpdateQtyRtn(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateWh(dr, false);
                        this.UpdateQtyRtn(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateWh(dr, false);
                        this.UpdateQtyRtn(dr, false);
                        this.UpdateWh(dr, true);
                        this.UpdateQtyRtn(dr, true);
                    }

                    #endregion
                }
                else if (tableName == "TF_PSS4" && _psId != "PD")
                {
                    #region 更新箱库存
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateBoxWh(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateBoxWh(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateBoxWh(dr, false);
                        this.UpdateBoxWh(dr, true);
                    }
                    #endregion
                }
            }
            if (_psId == "PC")
            {
                DRPZG _drpZg = new DRPZG();
                if (dr.RowState == DataRowState.Modified || dr.RowState == DataRowState.Deleted)
                {
                    _drpZg.UpdateZgByPc(dr, false, false);
                }
                if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified)
                {
                    _drpZg.UpdateZgByPc(dr, true, false);
                }
            }
        }
        #endregion

        #region 审核流程
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
                SunlikeDataSet _ds = this.GetData(chk_man, bil_id, bil_no);
                _ds.Tables["TF_PSS_BAR"].TableName = "TF_PSS3";
                _ds.Tables["TF_PSS_BOX"].TableName = "TF_PSS4";
                DataRow _drHead = _ds.Tables["MF_PSS"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_PSS"];
                DataTable _dtBar = _ds.Tables["TF_PSS3"];
                DataTable _dtBox = _ds.Tables["TF_PSS4"];
                _drHead["CHK_MAN"] = chk_man;
                _drHead["CLS_DATE"] = cls_dd;
                //立账
                this.UpdateMfArp(_drHead);
                DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
                _sa.UpdateArpNo(_drHead["PS_ID"].ToString(), _drHead["PS_NO"].ToString(), _drHead["ARP_NO"].ToString());
                //更新SARP
                this.UpdateSarp(_drHead);
                //更新费用单
                MonEX _monEx = new MonEX();
                //ER
                if (!String.IsNullOrEmpty(_drHead["EP_NO"].ToString()))
                {
                    SunlikeDataSet _dsEr = _monEx.GetData("ER", _drHead["EP_NO"].ToString(), false);
                    if (_dsEr.Tables["MF_EXP"].Rows.Count > 0)
                    {
                        _dsEr.Tables["MF_EXP"].Rows[0]["CHK_MAN"] = chk_man;
                        _dsEr.Tables["MF_EXP"].Rows[0]["CLS_DATE"] = cls_dd;
                        _dsEr.ExtendedProperties["BILL_AUDITING"] = "T";
                        _monEx.UpdateData(null, _dsEr, true);
                    }
                }
                //EP
                if (!String.IsNullOrEmpty(_drHead["EP_NO1"].ToString()))
                {
                    SunlikeDataSet _dsEp = _monEx.GetData("EP", _drHead["EP_NO1"].ToString(), false);
                    if (_dsEp.Tables["MF_EXP"].Rows.Count > 0)
                    {
                        _dsEp.Tables["MF_EXP"].Rows[0]["CHK_MAN"] = chk_man;
                        _dsEp.Tables["MF_EXP"].Rows[0]["CLS_DATE"] = cls_dd;
                        _dsEp.ExtendedProperties["BILL_AUDITING"] = "T";
                        _monEx.UpdateData(null, _dsEp, true);
                    }
                }
                //if (bil_id != "PD")//lzj bugNo89879
                {
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
                    //暂估回冲
                    DRPZG _drpZg = new DRPZG();
                    if (_drHead["PS_ID"].ToString() == "PC")
                    {
                        _drpZg.UpdateZgByPc(_drHead, false, false);
                        _drpZg.UpdateZgByPc(_drHead, true, false);
                    }
                    //修改库存
                    for (int i = 0; i < _dtBody.Rows.Count; i++)
                    {

                        //修改分仓存量
                        this.UpdateWh(_dtBody.Rows[i], true);
                        //修改原进货单已退数量
                        this.UpdateQtyRtn(_dtBody.Rows[i], true);
                        if (_drHead["PS_ID"].ToString() == "PC")
                        {
                            //回写暂估单
                            _drpZg.UpdateZgByPc(_dtBody.Rows[i], true, false);
                        }
                        else if (_drHead["PS_ID"].ToString() == "PB")
                        {
                            _drpZg.UpdateZgByPb(_dtBody.Rows[i], true);
                        }

                    }

                    if (this._alBlNo.Count > 0)
                    {
                        DRPBN _obj = new DRPBN();
                        _obj.UpdateQtyRtn(this._alBlNo, this._alBlItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                    }


                    if (_drHead["OS_ID"].ToString() == "SA")
                    {
                        //回写销货单
                        DbDRPPC _dbDrpPc = new DbDRPPC(Comp.Conn_DB);
                        if (!_dbDrpPc.UpdatePoNo(bil_id, bil_no, _drHead["OS_NO"].ToString()))
                        {
                            return "更新销货单错误！";
                        }
                    }
                    //修改受订单箱数量
                    for (int i = 0; i < _dtBox.Rows.Count; i++)
                    {
                        this.UpdateBoxWh(_dtBox.Rows[i], true);
                    }
                }
                //凭证模板不为空且单张立账
                //更新凭证
                string _vohNo = this.UpdateVohNo(_drHead, StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);

                //设定审核人
                DbDRPPC _drpPc = new DbDRPPC(Comp.Conn_DB);
                _drpPc.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd, _vohNo);
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }

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
                SunlikeDataSet _ds = this.GetData(null, bil_id, bil_no);
                _ds.Tables["TF_PSS_BAR"].TableName = "TF_PSS3";
                _ds.Tables["TF_PSS_BOX"].TableName = "TF_PSS4";
                //单据是否有销退数量或冲账
                this.SetCanModify(_ds, false, true);
                if (_ds.ExtendedProperties.Contains("CAN_MODIFY"))
                {
                    if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                    {
                        return "RCID=INV.HINT.CANMODIFY";
                    }
                }

                DataRow _drHead = _ds.Tables["MF_PSS"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_PSS"];
                DataTable _dtBar = _ds.Tables["TF_PSS3"];
                DataTable _dtBox = _ds.Tables["TF_PSS4"];
                //反审核费用单
                MonEX _monEx = new MonEX();
                if (!String.IsNullOrEmpty(_drHead["EP_NO"].ToString()))
                {
                    string _erError = _monEx.RollBack("ER", _drHead["EP_NO"].ToString());
                    if (!String.IsNullOrEmpty(_erError))
                    {
                        return _erError;
                    }
                }

                if (!String.IsNullOrEmpty(_drHead["EP_NO1"].ToString()))
                {
                    string _epError = _monEx.RollBack("EP", _drHead["EP_NO1"].ToString());
                    if (!String.IsNullOrEmpty(_epError))
                    {
                        return _epError;
                    }
                }

                //if (bil_id != "PD")//lzj bugNo89879
                {
                    for (int i = 0; i < _dtBody.Rows.Count; i++)
                    {
                        //修改分仓存量
                        this.UpdateWh(_dtBody.Rows[i], false);
                        //修改原单已退数量
                        this.UpdateQtyRtn(_dtBody.Rows[i], false);
                    }

                    if (this._alBlNo.Count > 0)
                    {
                        DRPBN _obj = new DRPBN();
                        _obj.UpdateQtyRtn(this._alBlNo, this._alBlItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                    }

                    if (_drHead["OS_ID"].ToString() == "SA")
                    {
                        //回写销货单
                        DbDRPPC _dbDrpPc = new DbDRPPC(Comp.Conn_DB);
                        if (!_dbDrpPc.UpdatePoNo("", "", _drHead["OS_NO"].ToString()))
                        {
                            return "更新销货单错误！";
                        }
                    }
                    //修改箱数量
                    for (int i = 0; i < _dtBox.Rows.Count; i++)
                    {
                        this.UpdateBoxWh(_dtBox.Rows[i], false);
                    }
                    //更新序列号记录
                    for (int i = 0; i < _dtBar.Rows.Count; i++)
                    {
                        _dtBar.Rows[i].Delete();
                    }
                    _auditBarCode = true;
                    this.UpdateBarCode(_ds);
                }

                //设定审核人
                if (isUpdateHead)
                {
                    DbDRPPC _drpPc = new DbDRPPC(Comp.Conn_DB);
                    _drpPc.UpdateChkMan(bil_id, bil_no, "", DateTime.Now, "");
                }
                //立账
                _drHead.Delete();
                this.UpdateMfArp(_drHead);
                DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
                _sa.UpdateArpNo(_drHead["PS_ID", DataRowVersion.Original].ToString(),
                    _drHead["PS_NO", DataRowVersion.Original].ToString(), "");
                //更新SARP
                this.UpdateSarp(_drHead);
                //更新凭证
                this.UpdateVohNo(_drHead, StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");
                DRPZG _drpZg = new DRPZG();
                if (_drHead["PS_ID", DataRowVersion.Original].ToString() == "PC")
                {
                    //暂估回冲
                    _drpZg.UpdateZgByPc(_drHead, false, true);
                    for (int i = 0; i < _dtBody.Rows.Count; i++)
                    {
                        _drpZg.UpdateZgByPc(_dtBody.Rows[i], false, true);
                    }
                }
                else if (_drHead["PS_ID", DataRowVersion.Original].ToString() == "PB")
                {
                    for (int i = 0; i < _dtBody.Rows.Count; i++)
                    {
                        _drpZg.UpdateZgByPb(_dtBody.Rows[i], false);
                    }
                }
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }
        #endregion

        #region 立帐
        /// <summary>
        /// 立帐
        /// </summary>
        /// <param name="dr">表头</param>
        private void UpdateMfArp(DataRow dr)
        {
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_PSS"];
            decimal _tax = 0;
            decimal _amtn = 0;
            decimal _amt = 0;
            if (dr.RowState != DataRowState.Deleted)
            {
                DataRow[] _darBody = _dtBody.Select();
                for (int i = 0; i < _darBody.Length; i++)
                {
                    if (!String.IsNullOrEmpty(_darBody[i]["AMTN_NET"].ToString()))
                    {
                        _amtn += Convert.ToDecimal(_darBody[i]["AMTN_NET"]);
                        if (!String.IsNullOrEmpty(_darBody[i]["TAX"].ToString()))
                        {
                            _tax += Convert.ToDecimal(_darBody[i]["TAX"]);
                        }
                    }
                    if (!String.IsNullOrEmpty(_darBody[i]["AMT"].ToString()))
                    {
                        _amt += Convert.ToDecimal(_darBody[i]["AMT"]);
                    }
                }
            }

            Arp _arp = new Arp();
            string _arpNo = "";
            //如果是删除或者是立帐方式由“单张立帐”改成“不立帐”
            //又或者立帐的情况下改变产商，要先删除原来的帐款，再新增帐款
            if (dr.RowState == DataRowState.Deleted ||
                (dr.RowState != DataRowState.Added && dr["ZHANG_ID", DataRowVersion.Original].ToString() == "1" &&
                (dr["ZHANG_ID"].ToString() != "1" || dr["CUS_NO", DataRowVersion.Original].ToString() != dr["CUS_NO"].ToString())))
            {
                _arpNo = dr["ARP_NO", DataRowVersion.Original].ToString();
                if (!_arp.DeleteMfArp(_arpNo))
                {
                    throw new Exception("无法删除立帐单 ARP_NO=" + _arpNo);
                }
            }
            if (dr.RowState != DataRowState.Deleted && dr["ZHANG_ID"].ToString() == "1")
            {
                if (dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString() == "PB" || dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString() == "PD")
                {
                    _tax *= -1;
                    _amtn *= -1;
                }
                decimal _disCnt = 100;
                if (!String.IsNullOrEmpty(dr["DIS_CNT"].ToString()))
                {
                    _disCnt = Convert.ToDecimal(dr["DIS_CNT"]);
                }
                _tax = _tax * _disCnt / 100;
                _amtn = _amtn * _disCnt / 100;
                _amt = _amt * _disCnt / 100;
                decimal _excRto = 0;
                if (!String.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                    _excRto = Convert.ToDecimal(dr["EXC_RTO"]);
                _arpNo = _arp.UpdateMfArp("2", "2", dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), Convert.ToDateTime(dr["PS_DD"]),
                    dr["BIL_TYPE"].ToString(), dr["DEP"].ToString(), dr["USR"].ToString(), dr["CUR_ID"].ToString(), _excRto,
                    _amtn + _tax, _amtn, _amt, _tax, dr, dr["REM"].ToString());
                //if (_arpNo != dr["ARP_NO"].ToString())
                {
                    //DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
                    //_sa.UpdateArpNo(dr["PS_ID"].ToString(),dr["PS_NO"].ToString(),_arpNo);
                    dr["ARP_NO"] = _arpNo;
                }
            }
        }
        #endregion

        #region 更新库存
        private void UpdateWh(DataRow dr, bool IsAdd)
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
            //RKTypes _rktype = new RKTypes();
            if (IsAdd)
            {
                _bilID = dr["PS_ID"].ToString();
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
                //_rktype.DEP = dr["DEP_RK"].ToString();
                //if (!String.IsNullOrEmpty(dr["RK_DD"].ToString()))
                //{
                //    _rktype.RK_DD = Convert.ToDateTime(dr["RK_DD"]);
                //}
                //if (!String.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                //{
                //    _rktype.VALID_DD = Convert.ToDateTime(Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat));
                //}
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
                _bilID = dr["PS_ID", DataRowVersion.Original].ToString();
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
                //_rktype.DEP = dr["DEP_RK", DataRowVersion.Original].ToString();
                //if (!String.IsNullOrEmpty(dr["RK_DD", DataRowVersion.Original].ToString()))
                //{
                //    _rktype.RK_DD = Convert.ToDateTime(dr["RK_DD", DataRowVersion.Original]);
                //}
                //if (!String.IsNullOrEmpty(dr["VALID_DD", DataRowVersion.Original].ToString()))
                //{
                //    _rktype.VALID_DD = Convert.ToDateTime(Convert.ToDateTime(dr["VALID_DD", DataRowVersion.Original]).ToString(Comp.SQLDateFormat));
                //}
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
            if (_bilID == "PB" || _bilID == "PD")
            {
                _qty *= -1;
                _qty1 *= -1;
                _cst *= -1;
                if (_bilID == "PD")
                {
                    _qty = 0;
                }
            }

            if (_osId == "BN")
            {
                _cst = 0;
            }
            WH _wh = new WH();
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            if (!String.IsNullOrEmpty(_batNo))
            {
                _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY_IN] = _qty;
                _ht[WH.QtyTypes.QTY1_IN] = _qty1;
                _ht[WH.QtyTypes.CST] = _cst;
                _ht[WH.QtyTypes.LST_IND] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                if ((IsAdd && _qty < 0) || (!IsAdd && _qty > 0))
                {
                    //进货退回不需要写有效日期
                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                }
                else
                {
                    Hashtable _ht1 = new System.Collections.Hashtable();
                    if (_osId == "TI")
                    {
                        _ht1[WH.QtyTypes.QTY_RK] = _qty * -1;
                    }
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
                _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY] = _qty;
                _ht[WH.QtyTypes.QTY1] = _qty1;
                _ht[WH.QtyTypes.AMT_CST] = _cst;
                _ht[WH.QtyTypes.LST_IND] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
            }
            if (_osId != "SA" && _osId != "BN" && _osId != "TI" && !String.IsNullOrEmpty(_osNo) && _bilID == "PC")
            {
                UpdateQtyOnWay(_osNo, _estItm, _prdNo, _unit, -_qty);
            }

        }
        //更新在途量
        private void UpdateQtyOnWay(string osNo, int estItm, string prdNo, string unit, decimal qty)
        {
            if (qty == 0)
                return;
            string _tmpStr = "";
            Prdt _prdt = new Prdt();
            DataTable _prdTb = _prdt.GetUnit(prdNo, out _tmpStr);
            DataRow[] _prdRow = _prdTb.Select("ID='" + unit + "'");
            if (!String.IsNullOrEmpty(_prdRow[0]["VALUE"].ToString()))
            {
                qty *= Convert.ToDecimal(_prdRow[0]["VALUE"]);
            }

            DRPPO _po = new DRPPO();
            SunlikeDataSet _dsPo = _po.GetData("PO", osNo, estItm);
            if (_dsPo.Tables[0].Rows.Count > 0)
            {
                DataRow _drPo = _dsPo.Tables[0].Rows[0];
                string _backId = "";
                string _clsId = "";
                _backId = _drPo["BACK_ID"].ToString();
                _clsId = _drPo["CLS_ID"].ToString();
                //手工结案
                if (_clsId == "T" && _backId.Length == 0)
                    return;
                decimal _pkQty = 1;
                _prdRow = _prdTb.Select("ID='" + _drPo["UNIT"].ToString() + "'");
                if (!String.IsNullOrEmpty(_prdRow[0]["VALUE"].ToString()))
                {
                    _pkQty *= Convert.ToDecimal(_prdRow[0]["VALUE"]);
                }
                decimal _qty = Convert.ToDecimal(_drPo["QTY"]) * _pkQty;
                decimal _qtyPs = Convert.ToDecimal(_drPo["QTY_PS"]) * _pkQty;
                decimal _qtyPre = Convert.ToDecimal(_drPo["QTY_PRE"]) * _pkQty;
                decimal _oldPs = _qtyPs;
                _qty -= _qtyPre;
                _qtyPs -= qty;

                if (_oldPs >= _qty && _qtyPs >= _qty)
                {
                    return;
                }
                else if (_oldPs > _qty && _qtyPs < _qty)
                {
                    qty = _qty - _qtyPs;
                }
                else if (_oldPs < _qty && _qtyPs > _qty)
                {
                    qty = _oldPs - _qty;
                }

                WH _wh = new WH();
                Hashtable _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY_ON_WAY] = qty;
                if (String.IsNullOrEmpty(_drPo["BAT_NO", DataRowVersion.Original].ToString()))
                {
                    _wh.UpdateQty(_drPo["PRD_NO"].ToString(), _drPo["PRD_MARK"].ToString(), _drPo["WH"].ToString(), "1", _ht);
                }
                else
                {
                    _wh.UpdateQty(_drPo["BAT_NO", DataRowVersion.Original].ToString(), _drPo["PRD_NO"].ToString(), _drPo["PRD_MARK"].ToString(), _drPo["WH"].ToString(), "", "1", _ht);
                }
            }
        }
        #endregion

        #region 更新箱库存
        private void UpdateBoxWh(DataRow dr, bool IsAdd)
        {
            string _prdNo = "";
            string _content = "";
            string _whNo = "";
            decimal _qty = 0;
            if (IsAdd)
            {
                _prdNo = dr["PRD_NO"].ToString();
                _content = dr["CONTENT"].ToString();
                _whNo = dr["WH"].ToString();
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
            }
            else
            {
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _content = dr["CONTENT", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                _qty *= -1;

            }
            if (_bilID == "PB")
            {
                _qty *= -1;
            }
            WH _wh = new WH();
            _wh.UpdateBoxQty(_prdNo, _whNo, _content, WH.BoxQtyTypes.QTY, _qty);
        }
        #endregion

        #region 更新其它费用收入
        /// <summary>
        /// 更新其它费用收入
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="bilType">EP or ER</param>
        /// <param name="bilId">单据别</param>
        /// <param name="bilNo">单号</param>
        /// <param name="isAuditing">是否走审核</param>
        private void UpdateExp(SunlikeDataSet ds, string bilType, string bilId, string bilNo, bool isAuditing)
        {
            try
            {
                string _headTableName = "MF_EXP_" + bilType;
                string _bodyTableName = "TF_EXP_" + bilType;
                DataTable _dtMfExp = ds.Tables[_headTableName];
                DataTable _dtTfExp = ds.Tables[_bodyTableName];
                if (_dtMfExp != null && _dtTfExp != null && _dtMfExp.Rows.Count > 0)
                {
                    string _epId = "";
                    string _epNo = "";
                    SunlikeDataSet _dsExp = null;
                    MonEX _monEx = new MonEX();
                    if (_dtMfExp.Rows[0].RowState == DataRowState.Added)
                    {
                        _epId = _dtMfExp.Rows[0]["EP_ID"].ToString();
                        _epNo = _dtMfExp.Rows[0]["EP_NO"].ToString();
                        _dsExp = _monEx.GetData("", "", true);
                        DataTable _dtMf = _dsExp.Tables["MF_EXP"];
                        DataRow _drMfNew = _dtMf.NewRow();
                        foreach (DataColumn dc in _dtMfExp.Columns)
                        {
                            _drMfNew[dc.ColumnName] = _dtMfExp.Rows[0][dc.ColumnName];
                        }
                        _drMfNew["BIL_TYPE"] = "";
                        _drMfNew["USR_NO"] = ds.Tables["MF_PSS"].Rows[0]["SAL_NO"];
                        _drMfNew["BIL_ID"] = bilId;
                        _drMfNew["PC_NO"] = bilNo;
                        _dtMf.Rows.Add(_drMfNew);

                        DataTable _dtTf = _dsExp.Tables["TF_EXP"];
                        for (int i = 0; i < _dtTfExp.Rows.Count; i++)
                        {
                            if (_dtTfExp.Rows[i].RowState != DataRowState.Deleted)
                            {
                                DataRow _drTfNew = _dtTf.NewRow();
                                foreach (DataColumn dc in _dtTfExp.Columns)
                                {
                                    if (dc.ColumnName == "ITM")
                                    {
                                        _drTfNew[dc.ColumnName] = i + 1;
                                        continue;
                                    }
                                    if (!dc.AutoIncrement)
                                    {
                                        _drTfNew[dc.ColumnName] = _dtTfExp.Rows[i][dc.ColumnName];
                                    }
                                }
                                _dtTf.Rows.Add(_drTfNew);
                            }
                        }
                    }
                    else
                    {
                        _epId = _dtMfExp.Rows[0]["EP_ID", DataRowVersion.Original].ToString();
                        _epNo = _dtMfExp.Rows[0]["EP_NO", DataRowVersion.Original].ToString();
                        _dsExp = _monEx.GetData(_epId, _epNo, false);
                        DataTable _dtTf = _dsExp.Tables["TF_EXP"];
                        DataTable _dtMf = _dsExp.Tables["MF_EXP"];
                        foreach (DataRow dr in _dtTfExp.Rows)
                        {
                            if (dr.RowState == DataRowState.Added)
                            {
                                DataRow _drTfNew = _dtTf.NewRow();
                                foreach (DataColumn dc in _dtTfExp.Columns)
                                {
                                    if (dc.ColumnName == "ITM")
                                    {
                                        _drTfNew[dc.ColumnName] = _dtTf.Select().Length == 0 ? 1 : Convert.ToInt32(_dtTf.Select("", "ITM DESC")[0]["ITM"]) + 1;
                                        continue;
                                    }
                                    if (!dc.AutoIncrement)
                                    {
                                        _drTfNew[dc.ColumnName] = dr[dc.ColumnName];
                                    }
                                }
                                _dtTf.Rows.Add(_drTfNew);
                            }
                            if (dr.RowState == DataRowState.Modified)
                            {
                                DataRow _dr = _dtTf.Rows.Find(new string[3] { dr["EP_ID"].ToString(), dr["EP_NO"].ToString(), dr["ITM"].ToString() });
                                if (_dr != null)
                                {
                                    foreach (DataColumn dc in _dtTfExp.Columns)
                                    {
                                        if (dc.ColumnName == "EP_ID" || dc.ColumnName == "EP_NO" || dc.ColumnName == "ITM")
                                        {
                                            continue;
                                        }
                                        _dr[dc.ColumnName] = dr[dc.ColumnName];
                                    }
                                }
                            }
                            if (dr.RowState == DataRowState.Deleted)
                            {
                                string _epIdOrg = dr["EP_ID", DataRowVersion.Original].ToString();
                                string _epNoOrg = dr["EP_NO", DataRowVersion.Original].ToString();
                                string _itmOrg = dr["ITM", DataRowVersion.Original].ToString();
                                DataRow _dr = _dtTf.Rows.Find(new string[3] { _epIdOrg, _epNoOrg, _itmOrg });
                                if (_dr != null)
                                {
                                    _dr.Delete();
                                }
                            }
                        }
                        //表身为空时删除表头
                        if (_dtTf.Select().Length == 0)
                        {
                            _dtMf.Rows[0].Delete();
                        }
                    }
                    if (_dsExp != null)
                    {
                        if (!_isRunAuditing && _dsExp.Tables["MF_EXP"].Rows[0].RowState != DataRowState.Deleted)
                        {
                            _dsExp.Tables["MF_EXP"].Rows[0]["CHK_MAN"] = _dsExp.Tables["MF_EXP"].Rows[0]["USR"];
                            _dsExp.Tables["MF_EXP"].Rows[0]["CLS_DATE"] = DateTime.Today;
                        }
                        _dsExp.ExtendedProperties["BILL_AUDITING"] = ((bool)(!isAuditing)).ToString().ToUpper().Substring(0, 1);
                    }
                    int _itm = 1;
                    foreach (DataRow dr in _dsExp.Tables["TF_EXP"].Select("", "ITM"))
                    {
                        dr["ITM"] = _itm++;
                    }
                    _monEx.UpdateData(null, _dsExp, true);
                    if (!_dsExp.HasErrors)
                    {
                        ds = _dsExp;
                        ds.AcceptChanges();
                    }
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion

        #region 更新凭证
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
                string _psId = dr["PS_ID"].ToString();
                if (this._reBuildVohNo)
                {
                    if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_updUsr = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0)
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("PC", _psId) == 0 || string.Compare("PB", _psId) == 0 || string.Compare("PD", _psId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.PC;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _psId, dr["PS_NO"].ToString()), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _psId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0 && string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("PC", _psId) == 0 || string.Compare("PB", _psId) == 0 || string.Compare("PD", _psId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.PC;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _psId, dr["PS_NO"].ToString()), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _psId, out _vohNoError);
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
                string _psId = dr["PS_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("PC", _psId) == 0 || string.Compare("PB", _psId) == 0 || string.Compare("PD", _psId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.PC;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0)
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _psId, out _vohNoError);
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
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string psId, string psNo, string vohNo)
        {
            DbDRPPC _pc = new DbDRPPC(Comp.Conn_DB);
            _pc.UpdateVohNo(psId, psNo, vohNo);
        }
        #endregion

        #region UpdateBarCode
        private void UpdateBarCode(SunlikeDataSet ChangedDS)
        {
            DataRow _drHead = ChangedDS.Tables["MF_PSS"].Rows[0];
            DataTable _dtBody = ChangedDS.Tables["TF_PSS"];
            //查找表身有修改过库位的记录
            System.Collections.Hashtable _htKeyItm = new System.Collections.Hashtable();
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
                string _sql = "select PS_ID,PS_NO,PS_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO from TF_PSS3"
                    + " where PS_ID='" + _drHead["PS_ID"].ToString() + "' and PS_NO='" + _drHead["PS_NO"].ToString() + "'";
                Query _query = new Query();
                SunlikeDataSet _dsQuery = _query.DoSQLString(_sql);
                _dsQuery.Tables[0].TableName = "TF_PSS3";
                //				_dsQuery.Merge(ChangedDS.Tables["TF_PSS3"],false,MissingSchemaAction.AddWithKey);
                //				_dtBarCode = _dsQuery.Tables["TF_PSS3"];
                ChangedDS.Merge(_dsQuery.Tables["TF_PSS3"], true, MissingSchemaAction.AddWithKey);
                _dtBarCode = ChangedDS.Tables["TF_PSS3"];
            }
            else
            {
                _dtBarCode = ChangedDS.Tables["TF_PSS3"];
            }
            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            for (int i = 0; i < _dtBarCode.Rows.Count; i++)
            {
                if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["PS_ITM"].ToString()] != null)
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
                System.Collections.ArrayList _alBar = new System.Collections.ArrayList();
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
                    _bilID = _drHead["PS_ID", DataRowVersion.Original].ToString();
                    _bilNo = _drHead["PS_NO", DataRowVersion.Original].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["PS_DD", DataRowVersion.Original]);
                    _usr = _drHead["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _cusNo = _drHead["CUS_NO"].ToString();
                    _bilID = _drHead["PS_ID"].ToString();
                    _bilNo = _drHead["PS_NO"].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["PS_DD"]);
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
                System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
                System.Collections.ArrayList _alBoxNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alWhNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alStop = new System.Collections.ArrayList();
                System.Text.StringBuilder _sbChange = new System.Text.StringBuilder();
                double _total = 0;
                for (int i = 0; i < _dtBarCode.Rows.Count; i++)
                {
                    if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["PS_ITM"].ToString()] != null)
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
                            _keyItm = _dtBarCode.Rows[i]["PS_ITM", DataRowVersion.Original].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _keyItm, "", DataViewRowState.CurrentRows | DataViewRowState.OriginalRows);
                            if (_dra[0].RowState == DataRowState.Deleted)
                            {
                                _whNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _batNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                                if (_dra[0]["PS_ID", DataRowVersion.Original].ToString() == "PB")
                                {
                                    _isPlus = false;
                                }
                            }
                            else
                            {
                                _batNo = _dra[0]["BAT_NO"].ToString();
                                _whNo = _dra[0]["WH"].ToString();
                                if (_dra[0]["PS_ID"].ToString() == "PB")
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
                            _keyItm = _dtBarCode.Rows[i]["PS_ITM"].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _keyItm);
                            _whNo = _dra[0]["WH"].ToString();
                            _batNo = _dra[0]["BAT_NO"].ToString();
                            if (_dra[0].RowState == DataRowState.Modified)
                            {
                                _oldWhNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _oldBatNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                            }
                            if (_dra[0]["PS_ID"].ToString() == "PB")
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

        #region 回写原单退回数
        private void UpdateQtyRtn(DataRow dr, bool IsAdd)
        {

            if (CaseInsensitiveComparer.Default.Compare("PD", GetStrDrValue(dr,"PS_ID")) != 0)
            {
                string _osID, _osNo;
                int _preItm = 0;
                decimal _qty = 0;
                decimal _qty1 = 0;
                int _unit = 1;
                string _prdNo = "";
                if (IsAdd)
                {
                    _osID = dr["OS_ID"].ToString();
                    _osNo = dr["OS_NO"].ToString();
                    _prdNo = dr["PRD_NO"].ToString();
                    if (_osID == "PC" || _osID == "ZG")
                    {
                        if (!String.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                        {
                            _preItm = Convert.ToInt32(dr["OTH_ITM"]);
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(dr["EST_ITM"].ToString()))
                        {
                            _preItm = Convert.ToInt32(dr["EST_ITM"]);
                        }
                    }
                    if (!String.IsNullOrEmpty(_osNo))
                    {
                        if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                        _qty = Convert.ToDecimal(dr["QTY"]);
                    if (!string.IsNullOrEmpty(dr["QTY1"].ToString()))
                        _qty1 = Convert.ToDecimal(dr["QTY1"]);
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
                    if (_osID == "PC" || _osID == "ZG")
                    {
                        if (!String.IsNullOrEmpty(dr["OTH_ITM", DataRowVersion.Original].ToString()))
                        {
                            _preItm = Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original]);
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(dr["EST_ITM", DataRowVersion.Original].ToString()))
                        {
                            _preItm = Convert.ToInt32(dr["EST_ITM", DataRowVersion.Original]);
                        }
                    }
                    if (!String.IsNullOrEmpty(_osNo))
                    {
                        if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                        {
                            _qty -= Convert2Decimal(dr["QTY", DataRowVersion.Original]);
                        }

                        if (!string.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                        {
                            _qty1 -= Convert2Decimal(dr["QTY1", DataRowVersion.Original]);
                        }
                    }
                    if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    {
                        _unit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
                    }
                }
                if (_osID != "SA" && _osID != "BN" && _osID != "TI" && !String.IsNullOrEmpty(_osNo) && _preItm > 0)
                {
                    Hashtable _ht = new Hashtable();
                    if (_osID == "PO")
                    {
                        _ht["TableName"] = "TF_POS";
                        _ht["IdName"] = "OS_ID";
                        _ht["NoName"] = "OS_NO";
                        _ht["ItmName"] = "PRE_ITM";
                    }
                    else if (_osID == "ZG")
                    {
                        _ht["TableName"] = "TF_ZG";
                        _ht["IdName"] = "ZG_ID";
                        _ht["NoName"] = "ZG_NO";
                        _ht["ItmName"] = "OTH_ITM";
                    }
                    else
                    {
                        _ht["TableName"] = "TF_PSS";
                        _ht["IdName"] = "PS_ID";
                        _ht["NoName"] = "PS_NO";
                        _ht["ItmName"] = "PRE_ITM";
                    }
                    _ht["OsID"] = _osID;
                    _ht["OsNO"] = _osNo;
                    _ht["KeyItm"] = _preItm;

                    _qty = INVCommon.GetRtnQty(_prdNo, _qty, _unit, _ht);
                    _qty1 = INVCommon.GetRtnQty(_prdNo, _qty1, _unit, _ht);
                    DbDRPPC _drpPc = new DbDRPPC(Comp.Conn_DB);
                    string _errorID = _drpPc.UpdateQtyRtn(_osID, _osNo, _preItm, _qty);
                    if (_errorID == "1")
                    {
                        dr.SetColumnError("QTY", "RCID=INV.HINT.QTY_RTN_TANTO");
                        throw new Exception("RCID=INV.HINT.QTY_RTN_TANTO");		//退回数量不能大于进货数量
                    }
                }

                if (_osID == "BN" && !String.IsNullOrEmpty(_osNo) && _preItm > 0)
                {
                    this._alBlNo.Add(_osNo);
                    this._alBlItm.Add(_preItm);
                    this._alPrdNo.Add(_prdNo);
                    this._alUnit.Add(_unit);
                    this._alQty.Add(_qty);
                    this._alQty1.Add(_qty1);
                }

                if (_osID == "TI" && !String.IsNullOrEmpty(_osNo) && _preItm > 0)
                {
                    DRPTI _drpti = new DRPTI();
                    _drpti.UpdateQtyPs(_osID, _osNo, _preItm.ToString(), _qty);
                }
            }
        }
        #endregion

        #region 修改修改进货退回单的已退量
        /// <summary>
        ///  回写已采购审核量
        /// </summary>
        /// <param name="psId">采购单据别</param>
        /// <param name="psNo">采购单号</param>
        /// <param name="unit">产品代号</param>
        /// <param name="itm">EST_ITM项次</param>
        /// <param name="qty">数量</param>
        public void UpdateQtyRtn(string psId, string psNo, int itm, string unit, decimal qty)
        {
            this.UpdateQty(psId, psNo, "PRE_ITM", itm, unit, "QTY_RTN", qty);
        }
        /// <summary>
        ///  回写未采购审核量
        /// </summary>
        /// <param name="osId">采购单据别</param>
        /// <param name="osNo">采购单号</param>
        /// <param name="unit">产品代号</param>
        /// <param name="itm">EST_ITM项次</param>
        /// <param name="qty">数量</param>
        public void UpdateQtyRtnUnsh(string psId, string psNo, int itm, string unit, decimal qty)
        {
            this.UpdateQty(psId, psNo, "PRE_ITM", itm, unit, "QTY_RTN_UNSH", qty);
        }
        /// <summary>
        /// 修改修改进货退回单的已退量
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="itmColumnName"></param>
        /// <param name="itm"></param>
        /// <param name="unit"></param>
        /// <param name="qtyColumnName"></param>
        /// <param name="qty"></param>
        private void UpdateQty(string psId, string psNo, string itmColumnName, int itm, string unit, string qtyColumnName, decimal qty)
        {
            string _prdNo = "";
            String _unitPb = "";
            SunlikeDataSet _dsPo = this.GetBody(psId, psNo, itmColumnName, itm, false);
            if (_dsPo != null && _dsPo.Tables["TF_PSS"].Rows.Count > 0)
            {
                _prdNo = _dsPo.Tables["TF_PSS"].Rows[0]["PRD_NO"].ToString();
                _unitPb = _dsPo.Tables["TF_PSS"].Rows[0]["UNIT"].ToString();
            }
            //计算
            Prdt _prdt = new Prdt();
            //换算单位
            decimal _qtyNew = _prdt.GetUnitQty(_prdNo, unit, qty, _unitPb);
            DbDRPPC _pc = new DbDRPPC(Comp.Conn_DB);
            _pc.UpdateQty(psId, psNo, itmColumnName, itm, qtyColumnName, qty);
        }
        #endregion

        #region 更新月余额档
        private void UpdateSarp(DataRow dr)
        {
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_PSS"];
            int _year = System.DateTime.Now.Year;
            int _month = System.DateTime.Now.Month;
            string _psID = "";
            string _cusNo = "";
            decimal _up, _qty;
            decimal _amtn = 0;
            decimal _disCnt = 100;
            Arp _arp = new Arp();
            //如果是删除或者是立帐方式由“单张立帐”改成“不立帐”
            //又或者立帐的情况下改变产商或立帐方式，要先删除原来的帐款
            if (dr.RowState != DataRowState.Added)
            {
                _year = Convert.ToDateTime(dr["PS_DD", DataRowVersion.Original]).Year;
                _month = Convert.ToDateTime(dr["PS_DD", DataRowVersion.Original]).Month;
                _psID = dr["PS_ID", DataRowVersion.Original].ToString();
                _cusNo = dr["CUS_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["DIS_CNT", DataRowVersion.Original].ToString()))
                {
                    _disCnt = Convert.ToDecimal(dr["DIS_CNT", DataRowVersion.Original]);
                }
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    if (_dtBody.Rows[i].RowState == DataRowState.Added)
                    {
                        continue;
                    }
                    if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP", DataRowVersion.Original].ToString()))
                    {
                        _up = 0;
                    }
                    else
                    {
                        _up = Convert.ToDecimal(_dtBody.Rows[i]["UP", DataRowVersion.Original]);
                    }
                    if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY", DataRowVersion.Original].ToString()))
                    {
                        _qty = 0;
                    }
                    else
                    {
                        _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY", DataRowVersion.Original]);
                    }
                    _amtn += _up * _qty;
                    if (!String.IsNullOrEmpty(_dtBody.Rows[i]["TAX", DataRowVersion.Original].ToString()))
                    {
                        _amtn += Convert.ToDecimal(_dtBody.Rows[i]["TAX", DataRowVersion.Original]);
                    }
                }
                if (_psID == "PC")
                {
                    _amtn *= -1;
                }
                _amtn *= _disCnt / 100;
                _arp.UpdateSarp("2", _year, _cusNo, _month, "", "AMTN_INV", _amtn);
            }
            if (dr.RowState != DataRowState.Deleted && dr["ZHANG_ID"].ToString() == "1")
            {
                _up = 0;
                _qty = 0;
                _amtn = 0;
                _year = Convert.ToDateTime(dr["PS_DD"]).Year;
                _month = Convert.ToDateTime(dr["PS_DD"]).Month;
                _psID = dr["PS_ID"].ToString();
                _cusNo = dr["CUS_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["DIS_CNT"].ToString()))
                {
                    _disCnt = Convert.ToDecimal(dr["DIS_CNT"]);
                }
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    if (_dtBody.Rows[i].RowState == DataRowState.Deleted)
                    {
                        if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP", DataRowVersion.Original].ToString()))
                        {
                            _up = 0;
                        }
                        else
                        {
                            _up = Convert.ToDecimal(_dtBody.Rows[i]["UP", DataRowVersion.Original]);
                        }
                        if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY", DataRowVersion.Original].ToString()))
                        {
                            _qty = 0;
                        }
                        else
                        {
                            _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY", DataRowVersion.Original]);
                        }
                        _amtn -= _up * _qty;
                        if (!String.IsNullOrEmpty(_dtBody.Rows[i]["TAX", DataRowVersion.Original].ToString()))
                        {
                            _amtn -= Convert.ToDecimal(_dtBody.Rows[i]["TAX", DataRowVersion.Original]);
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP"].ToString()))
                        {
                            _up = 0;
                        }
                        else
                        {
                            _up = Convert.ToDecimal(_dtBody.Rows[i]["UP"]);
                        }
                        if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY"].ToString()))
                        {
                            _qty = 0;
                        }
                        else
                        {
                            _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY"]);
                        }
                        _amtn += _up * _qty;
                        if (!String.IsNullOrEmpty(_dtBody.Rows[i]["TAX"].ToString()))
                        {
                            _amtn += Convert.ToDecimal(_dtBody.Rows[i]["TAX"]);
                        }
                    }
                }
                if (_psID == "PB")
                {
                    _amtn *= -1;
                }
                _amtn *= _disCnt / 100;
                _arp.UpdateSarp("2", _year, _cusNo, _month, "", "AMTN_INV", _amtn);
            }
        }
        #endregion

        #region 取得原单据货品价格，汇率
        /// <summary>
        /// 取得原单据货品价格
        /// </summary>
        /// <param name="osID">单据类别</param>
        /// <param name="osNo">单号</param>
        /// <param name="itm">项次</param>
        /// <param name="isUT2">是否为副单位</param>
        /// <returns></returns>
        public string GetOldPrice(string osID, string osNo, string itm, bool isUT2)
        {
            string _sql = "select UP,UP_QTY1 from ";
            if (osID == "PO")
            {
                _sql += " TF_POS where OS_ID='" + osID + "' and OS_NO='" + osNo + "' and PRE_ITM='" + itm + "' ";
            }
            else if (osID == "PC")
            {
                _sql += " TF_PSS where PS_ID='" + osID + "' and PS_NO='" + osNo + "' and PRE_ITM='" + itm + "' ";
            }
            else if (osID == "ZG")
            {
                _sql += " TF_ZG WHERE ZG_ID = '" + osID + "' AND ZG_NO = '" + osNo + "' AND OTH_ITM = '" + itm + "'";
            }
            Query _query = new Query();
            SunlikeDataSet _ds = _query.DoSQLString(Comp.Conn_DB, _sql);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                return _ds.Tables[0].Rows[0][isUT2 ? "UP_QTY1" : "UP"].ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 取得原单据货品汇率
        /// </summary>
        /// <param name="osID">单据类别</param>
        /// <param name="osNo">单号</param>
        /// <returns></returns>
        public string GetOldExcRto(string osID, string osNo)
        {
            string _sql = "SELECT EXC_RTO FROM ";
            if (osID == "PO")
            {
                _sql += " TF_POS WHERE OS_ID='" + osID + "' AND OS_NO='" + osNo + "'' ";
            }
            else if (osID == "PC")
            {
                _sql += " TF_PSS WHERE PS_ID='" + osID + "' AND PS_NO='" + osNo + "'' ";
            }
            Query _query = new Query();
            SunlikeDataSet _ds = _query.DoSQLString(Comp.Conn_DB, _sql);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                return _ds.Tables[0].Rows[0]["EXC_RTO"].ToString();
            }
            else
            {
                return "1";
            }
        }
        #endregion

        #region ICloseBill Members
        /// <summary>
        /// 反结案
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "LZ_CLS_ID")
            {
                DbDRPPC _pc = new DbDRPPC(Comp.Conn_DB);
                _error = _pc.CloseBill(bil_no, false);
            }
            return _error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "LZ_CLS_ID")
            {
                DbDRPPC _pc = new DbDRPPC(Comp.Conn_DB);
                _error = _pc.CloseBill(bil_no, true);
            }
            return _error;
        }
        #endregion

        #region function
        private decimal Convert2Decimal(object p)
        {
            decimal _d = 0;
            if (!decimal.TryParse(Convert.ToString(p), out _d))
                _d = 0;
            return _d;
        }
        private object GetDrValue(DataRow _dr, string field)
        {
            if (_dr != null && _dr.Table.Columns.Contains(field))
                return _dr.RowState == DataRowState.Deleted ? _dr[field, DataRowVersion.Original] : _dr[field];
            return null;
        }

        private string GetStrDrValue(DataRow _dr, string field)
        {
            return Convert.ToString(GetDrValue(_dr, field));
        }
        #endregion

        /// <summary>
        /// 判断单据表身是否补开发票则不允许删除
        /// </summary>
        /// <param name="dr">tf_ck.row</param>
        private void checktflz(DataRow dr)
        {

            InvLI _invli = new InvLI();
            string bilId = dr["PS_ID", DataRowVersion.Original].ToString();
            string ckNo = dr["PS_NO", DataRowVersion.Original].ToString();

            SunlikeDataSet _ds = _invli.GetInTfLz(bilId, ckNo);
            if (_ds.Tables["TF_LZ1"].Rows.Count > 0)
            {
                throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + ",PARAM=" + _ds.Tables["TF_LZ1"].Rows[0]["LZ_NO"].ToString());//无法删除单号，原因：{0}
            }

        }


    }
}