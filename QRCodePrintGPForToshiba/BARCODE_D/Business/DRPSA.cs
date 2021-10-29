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
    /// Summary description for DRPSA.
    /// </summary>
    public class DRPSA : BizObject, IAuditing, ICloseBill
    {
        #region Variable

        private bool _isRunAuditing;
        private bool _auditBarCode;
        private int _barCodeNo;
        private string _bil_NO = "";
        private string _saveID = "";
        private bool _updateAmtnInvDelete;
        private bool _updateAmtnInvAdd;
        private Dictionary<string, string> _itmNoNoUpdate = new Dictionary<string, string>();
        /// <summary>
        /// POS生成的销货单会写入
        /// </summary>
        private string _hangZhang = "";
        /// <summary>
        /// 是否重新切制凭证
        /// </summary>
        private bool _reBuildVohNo = false;
        private System.Collections.ArrayList _alOsNo = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alOsItm = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alPrdNo = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alUnit = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alQty = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alQty1 = new System.Collections.ArrayList();
        /// <summary>
        /// 天心在线Web Service地址
        /// </summary>
        public static string OnlineServiceUrl;

        #endregion

        /// <summary>
        /// 销货单
        /// </summary>
        public DRPSA()
        {
        }

        /// <summary>
        /// 取表身数据
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public DataSet GetBodyData(string psId, string psNo, int preItm)
        {
            DbDRPSA _dbDrpSa = new DbDRPSA(Comp.Conn_DB);
            return _dbDrpSa.GetBodyData(psId, psNo, preItm);
        }

        /// <summary>
        /// 取得销货单资料（WebForm）
        /// </summary>
        /// <param name="usr">当前操作用户</param>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm, string usr, string PsID, string PsNo)
        {
            return GetData(pgm, usr, PsID, PsNo, false, true);
        }
        /// <summary>
        /// 取得销货单资料（WinForm）
        /// </summary>
        /// <param name="usr">当前操作用户</param>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="OnlyFillSchema">是否只读取Schema</param>
        /// <param name="HasDsc">是否处理特征分段</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm, string usr, string PsID, string PsNo, bool OnlyFillSchema, bool HasDsc)
        {
            DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
            SunlikeDataSet _ds = _sa.GetData(PsID, PsNo, OnlyFillSchema);
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
            //记录原单据的数量
            _ds.Tables["TF_PSS"].Columns.Add(new DataColumn("QTY_SO_ORG", typeof(System.Decimal)));
            DrpSO _drpSo = new DrpSO();
            foreach (DataRow dr in _ds.Tables["TF_PSS"].Rows)
            {
                string _billId = dr["OS_ID"].ToString();
                string _billNo = dr["OS_NO"].ToString();
                string _ckNo = Convert.ToString(dr["CK_NO"]);
                if (!string.IsNullOrEmpty(_ckNo))
                {
                    string _othItm = Convert.ToString(dr["OTH_ITM"]);
                    DbDRPCK _db = new DbDRPCK(Comp.Conn_DB);
                    dr["QTY_SO_ORG"] = _db.GetQtySoOrg("CK", _ckNo, _othItm);
                }
                else
                {
                    if (_billNo.Length > 0)
                    {
                        if (PsID == "SA")
                        {
                            int _bilItm = Convert.ToInt32(dr["EST_ITM"]);
                            if (_billId == "SB")
                            {
                                using (DataSet _dsSb = this.GetBodyData(_billId, _billNo, _bilItm))
                                {
                                    if (_dsSb.Tables[0].Rows.Count > 0)
                                    {
                                        dr["QTY_SO_ORG"] = _dsSb.Tables[0].Rows[0]["QTY"];
                                    }
                                }
                            }
                            else if (_billId == "CK")
                            {
                                DRPCK _drpCk = new DRPCK();
                                using (DataSet _dsCk = _drpCk.GetBody("CK", dr["CK_NO"].ToString(), Convert.ToInt32(dr["OTH_ITM"])))
                                {
                                    if (_dsCk.Tables["TF_CK"].Rows.Count > 0)
                                    {
                                        dr["QTY_SO_ORG"] = _dsCk.Tables[0].Rows[0]["QTY"];
                                    }
                                }
                            }
                            else if (_billId == "SO")
                            {
                                SunlikeDataSet _dsSo = _drpSo.GetBody(_billId, _billNo, "EST_ITM", _bilItm, true);
                                if (_dsSo.Tables[0].Rows.Count > 0)
                                {
                                    dr["QTY_SO_ORG"] = _dsSo.Tables[0].Rows[0]["QTY"];
                                }
                            }
                        }
                    }
                }
            }
            //增加单据权限
            if (!OnlyFillSchema)
            {
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    DataTable _dtMf = _ds.Tables["MF_PSS"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        if (String.IsNullOrEmpty(pgm))
                        {
                            pgm = "DRPSA";
                        }
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
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
            //盘点单表身项次 by zb
            _dtTfPss.Columns.Add("PT_ITM", typeof(int));
            _dtTfPss.Columns.Add("PRD_NO_NO", typeof(System.String));
            _dtTfPss.Columns.Add("UNIT_DP", typeof(String)).MaxLength = 8;
            //销货单中单位标准成本
            _dtTfPss.Columns.Add("CST_STD_UNIT", typeof(System.Decimal));
            if (PsID == "SD")
            {
                _ds.Tables["TF_PSS"].Columns.Add(new DataColumn("AMT_OLD", typeof(System.Decimal)));
            }
            foreach (DataRow _drTfPss in _dtTfPss.Rows)
            {
                _drTfPss["PRD_NO_NO"] = _drTfPss["PRD_NO"];
                //给单位标准成本赋值
                if (!String.IsNullOrEmpty(_drTfPss["CST_STD"].ToString()) && !String.IsNullOrEmpty(_drTfPss["QTY"].ToString()) && Convert.ToDecimal(_drTfPss["QTY"]) > 0)
                {
                    _drTfPss["CST_STD_UNIT"] = Convert.ToDecimal(_drTfPss["CST_STD"]) / Convert.ToDecimal(_drTfPss["QTY"]);
                }
                else
                {
                    _drTfPss["CST_STD_UNIT"] = 0;
                }
                //计算折扣前金额
                if (PsID == "SD")
                {
                    if (String.IsNullOrEmpty(_drTfPss["AMT_OLD"].ToString()) && !String.IsNullOrEmpty(_drTfPss["UP"].ToString()) && !String.IsNullOrEmpty(_drTfPss["QTY"].ToString()))
                    {
                        _drTfPss["AMT_OLD"] = Convert.ToDecimal(_drTfPss["UP"]) * Convert.ToDecimal(_drTfPss["QTY"]);
                    }
                }

            }
            if (HasDsc)
            {
                //取得特征分段数据
                PrdMark _mark = new PrdMark();
                if (_mark.RunByPMark(usr))
                {
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
                                    _dtTfPss.Rows[i][_fldName] = _aryPrdMark[j].Trim();
                                }
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
                for (int i = 0; i < _dtTfPss.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(_dtTfPss.Rows[i]["CSTN_SAL"].ToString()) && !String.IsNullOrEmpty(_dtTfPss.Rows[i]["QTY"].ToString()) && Convert.ToDecimal(_dtTfPss.Rows[i]["QTY"]) > 0)
                    {
                        _dtTfPss.Rows[i]["UP_CST"] = Convert.ToDecimal(_dtTfPss.Rows[i]["CSTN_SAL"]) / Convert.ToDecimal(_dtTfPss.Rows[i]["QTY"]);
                    }
                }
            }
            //创建积分信息
            JF _jf = new JF();
            SunlikeDataSet _dsCard = new SunlikeDataSet();
            if (_ds.Tables["MF_PSS"].Rows.Count > 0)
            {
                _dsCard = _jf.GetData(_ds.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString(), _ds.Tables["MF_PSS"].Rows[0]["PS_NO"].ToString());
            }
            else
            {
                _dsCard = _jf.GetData("DRPJF", "", true);
            }
            _ds.Merge(_dsCard);
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
            //取预收
            string _rpNo = "";
            if (_ds.Tables["MF_PSS"].Rows.Count > 0)
            {
                _rpNo = _ds.Tables["MF_PSS"].Rows[0]["RP_NO"].ToString();
            }
            //			Accounts _acc = new Accounts();
            //			_ds.Merge(_acc.GetTfMon1("1",_rpNo));
            Bills _bills = new Bills();
            SunlikeDataSet _dsMon = _bills.GetData("1", _rpNo, false);
            _ds.Merge(_dsMon.Tables["TF_MON1"]);
            #region 现金收框
            if (PsID == "SA")
            {
                if (_ds.Tables["MF_PSS"].Rows.Count > 0)
                {
                    if (_ds.Tables["MF_PSS"].Rows[0]["ZHANG_ID"].ToString() == "4")
                    {
                        Bacc _bacc = new Bacc();
                        SunlikeDataSet _dsBac = _bacc.GetBAC(PsID + PsNo);
                        string _baccNo = "";
                        string _caccNo = "";
                        decimal _amtBB = 0;
                        decimal _amtnBB = 0;
                        decimal _amtBC = 0;
                        decimal _amtnBC = 0;
                        foreach (DataRow _drMF in _dsBac.Tables["MF_BAC"].Rows)
                        {
                            SunlikeDataSet _dsAcc = _bacc.GetData(_drMF["BACC_NO"].ToString());
                            if (_dsAcc.Tables["BACC"].Rows.Count > 0)
                            {
                                if (_dsAcc.Tables["BACC"].Rows[0]["BACC_TYPE"].ToString() == "1")
                                {
                                    _baccNo = _dsAcc.Tables["BACC"].Rows[0]["BACC_NO"].ToString();
                                    foreach (DataRow _drTF in _dsBac.Tables["TF_BAC"].Select("BB_ID='" + _drMF["BB_ID"].ToString() + "'AND BB_NO='" + _drMF["BB_NO"].ToString() + "'"))
                                    {
                                        if (!string.IsNullOrEmpty(_drTF["AMT"].ToString()))
                                            _amtBB += Convert.ToDecimal(_drTF["AMT"]);
                                        if (!string.IsNullOrEmpty(_drTF["AMTN"].ToString()))
                                            _amtnBB += Convert.ToDecimal(_drTF["AMTN"]);
                                    }
                                }
                                else if (_dsAcc.Tables["BACC"].Rows[0]["BACC_TYPE"].ToString() == "2")
                                {
                                    _caccNo = _dsAcc.Tables["BACC"].Rows[0]["BACC_NO"].ToString();
                                    foreach (DataRow _drTF in _dsBac.Tables["TF_BAC"].Select("BB_ID='" + _drMF["BB_ID"].ToString() + "'AND BB_NO='" + _drMF["BB_NO"].ToString() + "'"))
                                    {
                                        if (!string.IsNullOrEmpty(_drTF["AMT"].ToString()))
                                            _amtBC += Convert.ToDecimal(_drTF["AMT"]);
                                        if (!string.IsNullOrEmpty(_drTF["AMTN"].ToString()))
                                            _amtnBC += Convert.ToDecimal(_drTF["AMTN"]);
                                    }
                                }
                            }
                        }
                        _ds.Tables["MF_PSS"].Rows[0]["BACC_NO"] = _baccNo;
                        _ds.Tables["MF_PSS"].Rows[0]["AMTN_BB"] = _amtnBB;
                        _ds.Tables["MF_PSS"].Rows[0]["AMT_BB"] = _amtBB;
                        _ds.Tables["MF_PSS"].Rows[0]["CACC_NO"] = _caccNo;
                        _ds.Tables["MF_PSS"].Rows[0]["AMTN_BC"] = _amtnBC;
                        _ds.Tables["MF_PSS"].Rows[0]["AMT_BC"] = _amtBC;
                    }
                }
            }
            #endregion
            _dv.Dispose();
            GC.Collect(GC.GetGeneration(_dv));
            _ds.ExtendedProperties["DRPPS_SHOWPRICE"] = Comp.DRP_Prop["DRPPS_SHOWPRICE"];
            _ds.AcceptChanges();
            return _ds;
        }

        /// <summary>
        /// 取得表头信息
        /// </summary>
        /// <param name="PsID"></param>
        /// <param name="PsNO"></param>
        /// <param name="Itm"></param>
        /// <returns></returns>
        public string GetDataQty(string PsID, string PsNO, string Itm)
        {
            DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
            SunlikeDataSet _ds = _sa.GetData(PsID, PsNO, false);
            DataRow[] _dr = _ds.Tables["TF_PSS"].Select("ITM = '" + Itm.ToString() + "'");
            if (_dr.Length > 0)
            {
                return _dr[0]["QTY"].ToString();
            }
            return "";
        }
        /// <summary>
        /// 取得销货单号
        /// </summary>
        /// <returns></returns>
        public string GetSaNo(string saId, string userId, DateTime dateTime)
        {
            string _saNo = "SA00000000";
            try
            {
                SQNO _sqlno = new SQNO();
                Users _users = new Users();
                _saNo = _sqlno.Get(saId, userId, _users.GetUserDepNo(userId), dateTime, "FX");
            }
            catch { }
            return _saNo;
        }
        /// <summary>
        /// 取得销货单资料
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataPreItm(string psId, string psNo, string preItm)
        {
            DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
            return _sa.GetData(psId, psNo, preItm);
        }
        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        /// <param name="IsRollBack"></param>
        private void SetCanModify(DataSet ds, bool bCheckAuditing, bool IsRollBack)
        {
            DataTable _dtMf = ds.Tables["MF_PSS"];
            DataTable _dtTf = ds.Tables["TF_PSS"];
            if (_dtMf.Rows.Count > 0)
            {
                bool _bCanModify = true;
                //if (_dtMf.Rows[0]["BIL_TYPE"].ToString() != "FX" && !IsRollBack)
                //{
                //    _bCanModify = false;
                //}
                //结案
                if ((_dtMf.Rows[0]["CLS_ID"].ToString() == "T" || _dtMf.Rows[0]["CK_CLS_ID"].ToString() == "T"))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");
                }
                if (_dtMf.Rows[0]["LZ_CLS_ID"].ToString() == "T")
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "立账结案不能修改");
                }
                if (_dtMf.Rows[0]["ZHANG_ID"].ToString() == "3")
                {
                    DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
                    SunlikeDataSet _dsInv = _dbSa.GetInvBill(_dtMf.Rows[0]["PS_ID"].ToString(), _dtMf.Rows[0]["PS_NO"].ToString());
                    if (_dsInv.Tables[0].Rows.Count > 0)
                    {
                        ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LZ_MODIFY");
                        _bCanModify = false;
                    }
                }
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["PS_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                    _bCanModify = false;
                }
                if (bCheckAuditing)
                {
                    string _psID = _dtMf.Rows[0]["PS_ID"].ToString();
                    string _psNo = _dtMf.Rows[0]["PS_NO"].ToString();
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_psID, _psNo))
                    {
                        ds.ExtendedProperties["N_MODIFY_AUDITING"] = "T";
                        ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                        _bCanModify = false;
                    }
                }
                //判断是否走紧急放行结案
                if (_dtMf.Rows[0]["PS_ID"].ToString() == "SA")
                {
                    if (_dtMf.Rows[0]["HAS_FX"].ToString() == "T")
                    {
                        ////Common.SetCanModifyRem(ds, "紧急放行结案不能修改");
                        _bCanModify = false;
                    }
                }
                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                    _bCanModify = false;
                }
                //判断是否做了销货成本切制
                if (string.Compare("T", _dtMf.Rows[0]["CB_ID"].ToString()) == 0)
                {
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CB_ID");//已经切制销货成本，不能修改单据
                    _bCanModify = false;
                }
                //if (_bCanModify)
                {
                    DataRow[] _aryDrRtn;
                    if (_dtMf.Rows[0]["PS_ID"].ToString() != "SD" && _dtMf.Rows[0]["PS_ID"].ToString() != "SA")
                    {
                        // 是否有销退数量,已安装量
                        // 如果全部货品做了销货退回，单据不能删除及修改 已安装
                        // 如果部分货品做了销货退回，可以修改销货单，但不能修改改销货数量小于销货退回数量，而且不能删除已做销退的表身；
                        // 未进行销货退回的货品可以修改数量、金额，但销货单不能删除。
                        //_aryDrRtn = _dtTf.Select("QTY_RTN>0");
                        //if (_aryDrRtn.Length > 0)
                        //{
                        //    _bCanModify = false;
                        //}
                        //if (_bCanModify)
                        //{
                        //    _aryDrRtn = _dtTf.Select("ISNULL(QTY_SB, 0) > 0");
                        //    if (_aryDrRtn.Length > 0)
                        //    {
                        //        _bCanModify = false;
                        //    }
                        //}
                    }
                    // 是否有已安装量
                    if (_dtTf.Select("ISNULL(QTY_OI,0)>0").Length > 0)
                    {
                        ////Common.SetCanModifyRem(ds, "有已安装不能修改");
                        _bCanModify = false;
                    }
                    if (_dtTf.Select("ISNULL(AMTN_NET_FP, 0) <> 0 OR ISNULL(AMT_FP, 0) <> 0").Length > 0)
                    {
                        ////Common.SetCanModifyRem(ds, "已有发票金额不能修改");
                        _bCanModify = false;
                    }
                    //转营销费用计算
                    if (_dtTf.Select("ISNULL(ME_FLAG, 'F') = 'T'").Length > 0)
                    {
                        ////Common.SetCanModifyRem(ds, "已转营销费用计算不能修改");
                        _bCanModify = false;
                    }
                }
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["PO_NO"].ToString()))
                {
                    ////Common.SetCanModifyRem(ds, "已转采购单不能修改");
                    _bCanModify = false;
                }
                //if (_bCanModify)
                {
                    if (!String.IsNullOrEmpty(_dtMf.Rows[0]["ARP_NO"].ToString()))
                    {
                        Arp _arp = new Arp();
                        if (!String.IsNullOrEmpty(_dtMf.Rows[0]["ARP_NO"].ToString()))
                        {
                            try
                            {
                                if (_arp.HasReceiveDollar(_dtMf.Rows[0]["ARP_NO"].ToString()))
                                {
                                    //如销货单中是预收款冲的时，则允许删除 规则:立账单中的已开金额等于预收的金额则允许删除，否则不允许
                                    Bills _bills = new Bills();
                                    string _cusNo = "";
                                    string _rpId = "";
                                    string _rpNo = "";
                                    string _arpId = "";
                                    string _opnId = "";
                                    string _arpNo = "";
                                    bool _isAmt = false;    //金额的计算是否用外币
                                    decimal _amtnRcv = 0;
                                    decimal _amtnCls = 0;
                                    _cusNo = _dtMf.Rows[0]["CUS_NO"].ToString();
                                    _rpId = "1";
                                    _rpNo = _dtMf.Rows[0]["RP_NO"].ToString();
                                    _arpId = "1";
                                    _opnId = "2";
                                    _arpNo = _dtMf.Rows[0]["ARP_NO"].ToString();
                                    //客户状态
                                    if (_dtMf.Rows[0]["CUR_ID"].ToString().Length > 0)
                                    {
                                        Cust _cust = new Cust();
                                        SunlikeDataSet _dsCust = new SunlikeDataSet();
                                        _dsCust.Merge(_cust.GetData(_cusNo));
                                        DataTable _dtCust = _dsCust.Tables["CUST"];
                                        if (_dtCust.Rows.Count > 0)
                                        {
                                            if (_dtCust.Rows[0]["ID2_TAX"].ToString() == "T")
                                            {
                                                _isAmt = true;
                                            }
                                        }
                                    }
                                    SunlikeDataSet _dsMon = _bills.GetData(_rpId, _rpNo, false);
                                    if (_dsMon.Tables.Contains("TF_MON") && _dsMon.Tables["TF_MON"].Rows.Count > 0)
                                    {
                                        if (_isAmt)
                                        {
                                            if (_dtMf.Rows[0]["PS_ID"].ToString() == "SB")
                                            {
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMT_ARP"].ToString().Length > 0)
                                                    _amtnCls = Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMT_ARP"].ToString());

                                            }
                                            else
                                            {
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMT_BB"].ToString().Length > 0)
                                                    _amtnCls = Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMT_BB"].ToString());
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMT_BC"].ToString().Length > 0)
                                                    _amtnCls += Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMT_BC"].ToString());
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMT_CHK"].ToString().Length > 0)
                                                    _amtnCls += Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMT_CHK"].ToString());
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMT_OTHER"].ToString().Length > 0)
                                                    _amtnCls += Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMT_OTHER"].ToString());
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMT_IRP"].ToString().Length > 0)
                                                    _amtnCls += Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMT_IRP"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            if (_dtMf.Rows[0]["PS_ID"].ToString() == "SB")
                                            {
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMTN_ARP"].ToString().Length > 0)
                                                    _amtnCls = Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMTN_ARP"].ToString());
                                            }
                                            else
                                            {
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMTN_BB"].ToString().Length > 0)
                                                    _amtnCls = Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMTN_BB"].ToString());
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMTN_BC"].ToString().Length > 0)
                                                    _amtnCls += Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMTN_BC"].ToString());
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMTN_CHK"].ToString().Length > 0)
                                                    _amtnCls += Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMTN_CHK"].ToString());
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMTN_OTHER"].ToString().Length > 0)
                                                    _amtnCls += Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMTN_OTHER"].ToString());
                                                if (_dsMon.Tables["TF_MON"].Rows[0]["AMTN_IRP"].ToString().Length > 0)
                                                    _amtnCls += Convert.ToDecimal(_dsMon.Tables["TF_MON"].Rows[0]["AMTN_IRP"].ToString());
                                            }
                                        }

                                    }
                                    SunlikeDataSet _dsArp = _arp.GetData(_arpId, _opnId, _arpNo);
                                    if (_dsArp.Tables["MF_ARP"].Rows.Count > 0)
                                    {
                                        if (_isAmt)
                                        {
                                            if (_dsArp.Tables["MF_ARP"].Rows[0]["AMT_RCV"].ToString().Length > 0)
                                                _amtnRcv = Convert.ToDecimal(_dsArp.Tables["MF_ARP"].Rows[0]["AMT_RCV"].ToString());
                                        }
                                        else
                                        {
                                            if (_dsArp.Tables["MF_ARP"].Rows[0]["AMTN_RCV"].ToString().Length > 0)
                                                _amtnRcv = Convert.ToDecimal(_dsArp.Tables["MF_ARP"].Rows[0]["AMTN_RCV"].ToString());
                                        }
                                    }

                                    //已收款不全是从预收款来时
                                    if (_amtnCls != _amtnRcv)
                                    {
                                        ////Common.SetCanModifyRem(ds, "已收款不等于预收款不能修改");
                                        _bCanModify = false;
                                    }
                                }
                            }
                            catch { }
                        }
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
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["BIL_NO"].ToString()))
                {
                    ////Common.SetCanModifyRem(ds, "来源单号不为空，不能修改");
                    _bCanModify = false;
                }
                ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            }
        }

        /// <summary>
        /// 判断是否需要检查库存
        /// </summary>
        /// <param name="_dt"></param>
        /// <param name="psNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="itm"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public decimal ChkQty(DataTable _dt, string psNo, string prdNo, string prdMark, string itm, decimal qty)
        {
            Sunlike.Business.Data.DbDRPSA _dbSA = new DbDRPSA(Comp.Conn_DB);
            return _dbSA.ChkQty(_dt, psNo, prdNo, prdMark, itm, qty);
        }
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
        #region 保存
        /// <summary>
        /// 保存单据内容
        /// ESET_VOH_NO  True:重新创建凭证号码 否则:不创建
        /// SAVE_ID ：子母单属性。 T:直接保存，打上审核标记，等同于不走审核流程。 否则删除审核人，等同于进入待审状态
        /// </summary>
        /// <param name="ChangedDS"></param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, DataSet ChangedDS)
        {
            ChangedDS.Tables["TF_PSS_BAR"].TableName = "TF_PSS3";
            string _psID, _usr;
            DataRow _dr = ChangedDS.Tables["MF_PSS"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _psID = _dr["PS_ID", DataRowVersion.Original].ToString();
                _usr = _dr["USR", DataRowVersion.Original].ToString();
                _bil_NO = _dr["BIL_NO", DataRowVersion.Original].ToString();
            }
            else
            {
                _psID = _dr["PS_ID"].ToString();
                _usr = _dr["USR"].ToString();
                _bil_NO = _dr["BIL_NO"].ToString();
            }
            //是否重建凭证号码
            if (ChangedDS.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", ChangedDS.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            //判断是否直接保存
            if (ChangedDS.ExtendedProperties["SAVE_ID"] != null)
            {
                this._saveID = ChangedDS.ExtendedProperties["SAVE_ID"].ToString();
            }
            else
            {
                this._saveID = "";
            }

            if (ChangedDS.ExtendedProperties["HANG_ZHANG"] != null)
            {
                this._hangZhang = ChangedDS.ExtendedProperties["HANG_ZHANG"].ToString();
            }
            else
            {
                this._hangZhang = "";
                if (_dr.RowState != DataRowState.Deleted)
                {
                    DbDRPSA _dbDrpSa = new DbDRPSA(Comp.Conn_DB);
                    if (_dbDrpSa.IsExistPOSZG(_dr["DEP"].ToString(), _dr["PS_NO"].ToString(), _dr["PS_ID"].ToString()))
                    {
                        this._hangZhang = "F";
                    }
                }
            }

            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            string _sqlPlus = "";
            _sqlPlus += ",CUS_NO_POS,INST_TEAM,AMTN_DS";
            string _sqlCaseTf = "";
            //if (CompInfo.CaseID == "55")
            //{
            //    _sqlCaseTf += ",ONLINESERVICE_XML";
            //}
            _ht["MF_PSS"] = "PS_ID,PS_NO,PS_DD,PAY_DD,CHK_DD,CUS_NO,DEP,TAX_ID,SEND_MTH,SEND_WH,ZHANG_ID,EXC_RTO,SAL_NO,ARP_NO,PAY_MTH,"
                        + "PAY_DAYS,CHK_DAYS,INT_DAYS,PAY_REM,CLS_ID,USR,CHK_MAN,PRT_SW,CLS_DATE,CK_CLS_ID,LZ_CLS_ID,CLSCK,CLSLZ,YD_ID,"
                        + "BIL_TYPE,PH_NO,CUST_YG,OS_ID,OS_NO,PO_ID,SYS_DATE,BIL_NO,REM,TOT_QTY,CARD_NO,SEND_AREA,EP_NO,EP_NO1,CLS_REM,SB_CHK,"
                        + "ADR,RP_NO,AMTN_IRP,AMT_IRP,TAX_IRP,VOH_ID,MOB_ID,CUR_ID,AMTN_EP,AMTN_EP1,INV_NO,KP_ID,PO_NO,POPC_ID,ISSVS,AMTN_CBAC,VOH_NO,CB_ID,"
                        + "AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,TURN_ID,DIS_CNT,ACC_FP_NO ,SUB_NO,CUS_CARD_NO,CASH_ID,CUS_OS_NO,POS_OS_ID,POS_OS_CLS" + _sqlPlus;
            _ht["TF_PSS"] = "PS_ID,PS_NO,ITM,PS_DD,WH,PRD_NO,PRD_NAME,PRD_MARK,UNIT,QTY,UP,DIS_CNT,AMTN_NET,AMT,UP_SALE,AMTN_SALE,CST_STD,PRE_ITM,OS_NO,OS_ID,QTY_RTN,CSTN_SAL,AMTN,TAX,OTH_ITM,VALID_DD,BAT_NO,TAX_RTO,EST_ITM,AMTN_NET_FP,AMT_FP,REM,FREE_ID,CHK_RTN,QTY_PS,PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,ME_FLAG,INV_B2C,SH_NO_CUS,TAX_FP,QTY_FP,AMTN_COM,UP_QTY1,QTY1,ID_NO,SBAC_CHK,CK_NO,SAL_NO,SAL_NO1,AMTN_EP,CUS_OS_NO,QTY_RTN_UNSH,QTY_SB_UNSH,AMTN_RSV,DEP_RK,RK_DD" + _sqlCaseTf;
            //			_ht["TF_PSS3"] = "PS_ID,PS_NO,PS_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
            _ht["PAY_B2C"] = "BIL_ID,BIL_NO,PAY_REM,PAY_NO,PAY_ID,PAY_DD,REM";
            //判断是否走审核流程
            if (string.IsNullOrEmpty(_saveID))
            {
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
                //_isRunAuditing = _auditing.IsRunAuditing(_psID, _usr, _bilType, _mobID);
            }
            else if (string.Compare(this._saveID, "T") == 0)
            {
                _isRunAuditing = false;
            }
            else
            {
                _isRunAuditing = true;
            }

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
                    DataTable _dtMf = ChangedDS.Tables["MF_PSS"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        ChangedDS.ExtendedProperties["UPD"] = _billRight["UPD"];
                        ChangedDS.ExtendedProperties["DEL"] = _billRight["DEL"];
                        ChangedDS.ExtendedProperties["PRN"] = _billRight["PRN"];
                        ChangedDS.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
                this.SetCanModify(ChangedDS, true, false);
            }
            ChangedDS.Tables["TF_PSS3"].TableName = "TF_PSS_BAR";
            DataTable _dt = GetAllErrors(ChangedDS);
            return _dt;
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
            string _psId = "";
            string psNo;
            if (dr.RowState != DataRowState.Deleted)
            {
                if (tableName == "PAY_B2C")
                {
                    _psId = dr["BIL_ID"].ToString();
                    psNo = dr["BIL_NO"].ToString();
                }
                else
                {
                    _psId = dr["PS_ID"].ToString();
                    psNo = dr["PS_NO"].ToString();
                }
            }
            else
            {
                if (tableName == "PAY_B2C")
                {
                    _psId = dr["BIL_ID", DataRowVersion.Original].ToString();
                    psNo = dr["BIL_NO", DataRowVersion.Original].ToString();

                    if (!String.IsNullOrEmpty(dr["PAY_NO", DataRowVersion.Original].ToString()))
                    {
                        throw new SunlikeException("RCID=INV.HINT.PAY_NO_NOTNULL");//此单存在银行付款单号，不能删除。(B2C)
                    }
                }
                else
                {
                    _psId = dr["PS_ID", DataRowVersion.Original].ToString();
                    psNo = dr["PS_NO", DataRowVersion.Original].ToString();
                }
            }
            if (statementType != StatementType.Insert)
            {
                Auditing _auditing = new Auditing();
                if (_auditing.GetIfEnterAuditing(_psId, psNo))//如果进去审核了就不能修改和新增删除
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");
                }
                //如果不是销货放行结案作业,判断是否锁单
                if (!dr.Table.DataSet.ExtendedProperties.ContainsKey("IS_MTNFXCLOSE"))
                {
                    //判断是否锁单，如果已经锁单则不让修改。
                    Users _Users = new Users();
                    string _whereStr = "PS_ID = '" + _psId + "' AND PS_NO = 'xxx'";
                    if (_Users.IsLocked("MF_PSS", _whereStr))
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                    }
                }
            }
            if (tableName == "MF_PSS")
            {
                //新增时判断关账日期
                string _fieldNameCls = "CLS_INV";
                if (statementType != StatementType.Delete)
                {
                    if (string.Compare("T", dr["ISSVS"].ToString()) == 0)
                    {
                        _fieldNameCls = "CLS_MNU";
                    }
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["PS_DD"]), dr["DEP"].ToString(), _fieldNameCls))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (string.Compare("T", dr["ISSVS", DataRowVersion.Original].ToString()) == 0)
                    {
                        _fieldNameCls = "CLS_MNU";
                    }
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["PS_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), _fieldNameCls))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                //如果立帐方式由“单张立帐”改为“不立帐”时，清空立帐单号
                if (dr.RowState == DataRowState.Modified && dr["ZHANG_ID"].ToString() != dr["ZHANG_ID", DataRowVersion.Original].ToString())
                {
                    SQNO _sqArpNo = new SQNO();
                    if (!string.IsNullOrEmpty(dr["ARP_NO"].ToString()))
                        _sqArpNo.Delete(dr["ARP_NO"].ToString(), dr["USR"].ToString());
                    dr["ARP_NO"] = System.DBNull.Value;

                }
                string _usr;
                if (statementType == StatementType.Delete)
                {
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = dr["USR"].ToString();
                    //如果立帐方式非“单张立帐”，清除预冲金额
                    if (dr["ZHANG_ID"].ToString() != "1" && dr.Table.DataSet.Tables.Contains("TF_MON"))
                    {
                        foreach (DataRow _tfMonDr in dr.Table.DataSet.Tables["TF_MON"].Rows)
                        {
                            if (_tfMonDr.RowState != DataRowState.Deleted)
                                _tfMonDr.Delete();
                        }
                    }
                }
                //检查资料正确否
                if (statementType != StatementType.Delete)
                {
                    //检查销货客户
                    if (string.Compare(this._hangZhang, "T") != 0)
                    {
                        Cust _cust = new Cust();
                        if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString(), Convert.ToDateTime(dr["PS_DD"])))
                        {
                            dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//客户代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //检查业务员
                        //POS单中业务员可以为空
                        Salm _salm = new Salm();
                        if (!String.IsNullOrEmpty(dr["SAL_NO"].ToString()) && !_salm.IsExist(_usr, dr["SAL_NO"].ToString(), Convert.ToDateTime(dr["PS_DD"])))
                        {
                            dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //会员卡号
                        if (!String.IsNullOrEmpty(dr["CARD_NO"].ToString()))
                        {
                            CardMember _card = new CardMember();
                            SunlikeDataSet _dsCard = _card.GetData("", dr["CARD_NO"].ToString(), false);
                            if (_dsCard.Tables["POSCARD"].Rows.Count == 0)
                            {
                                dr.SetColumnError("CARD_NO", "RCID=COMMON.HINT.CARD_NO_NULL");//卡号不存在
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(_dsCard.Tables["POSCARD"].Rows[0]["EN_DD"].ToString()) && Convert.ToDateTime(_dsCard.Tables["POSCARD"].Rows[0]["EN_DD"].ToString()) < System.DateTime.Today || _dsCard.Tables["POSCARD"].Rows[0]["SYSED_ID"].ToString() == "T")
                                {
                                    dr.SetColumnError("CARD_NO", "RCID=COMMON.HINT.CARD_NO_CLOSE");//卡号已过期
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }

                        if (!String.IsNullOrEmpty(dr["CUS_CARD_NO"].ToString()))
                        {
                            #region Cust_Card检查
                            POSCustCR _custCR = new POSCustCR();
                            SunlikeDataSet _dsCR = _custCR.GetData(dr["CUS_CARD_NO"].ToString(), false);
                            if (_dsCR.Tables["CUST_CARD"].Rows.Count == 0)
                            {
                                dr.SetColumnError("CUS_CARD_NO", "RCID=COMMON.HINT.CARD_NO_NULL");//卡号不存在
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            else
                            {
                                if (_dsCR.Tables["CUST_CARD"].Rows[0]["STATUS_ID"].ToString() != "1")
                                {
                                    dr.SetColumnError("CUS_CARD_NO", "RCID=COMMON.HINT.CARD_NO_INVALID");//卡号无效
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["SUB_NO"].ToString()))
                                    {
                                        DataRow _drFind = _dsCR.Tables["CUST_CARD1"].Rows.Find(new object[] { dr["CUS_CARD_NO"], dr["SUB_NO"] });
                                        if (_drFind == null)
                                        {
                                            dr.SetColumnError("SUB_NO", "RCID=COMMON.HINT.CARD_NO_NULL");//卡号不存在
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(_drFind["END_DD"].ToString()) && Convert.ToDateTime(_drFind["END_DD"].ToString()) < DateTime.Today)
                                            {
                                                dr.SetColumnError("SUB_NO", "RCID=COMMON.HINT.CARD_NO_CLOSE");//卡号已过期
                                                status = UpdateStatus.SkipAllRemainingRows;
                                            }
                                            else
                                            {
                                                if (statementType == StatementType.Insert && string.IsNullOrEmpty(_drFind["START_DD"].ToString()))
                                                {
                                                    _custCR.EnabledCard(_drFind["CARD_NO"].ToString(), _drFind["SUB_NO"].ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        //检查部门
                        Dept _dept = new Dept();
                        if (!_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["PS_DD"])))
                        {
                            dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEP_NOTEXIST,PARAM=" + dr["DEP"].ToString());//部门代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //检查营业员
                        if (!String.IsNullOrEmpty(dr["CUST_YG"].ToString()) && !_cust.IsYG_NO(dr["CUS_NO"].ToString(), dr["CUST_YG"].ToString()))
                        {
                            dr.SetColumnError("CUST_YG", "RCID=COMMON.HINT.CUST_YG_NOTEXIST,PARAM=" + dr["CUST_YG"].ToString());//营业员代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        /* by zb
                         因可能转入多笔受订，单号格式就变为SO710090001-2，故自此处不需要判断
                        if (dr["OS_ID"].ToString() == "SO")
                        {
                            DrpSO _drpSo = new DrpSO();
                            DataTable _dsSo = _drpSo.GetDataMf("SO", dr["OS_NO"].ToString());
                            if (_dsSo.Rows.Count == 0)
                            {
                                dr.SetColumnError("OS_NO","RCID=COMMON.HINT.OS_NO_NULL");//来源单号不存在
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                         */

                        if (dr["ZHANG_ID"].ToString() == "4")
                        {
                            decimal _amtnTotal = 0;
                            foreach (DataRow _drTF in dr.Table.DataSet.Tables["TF_PSS"].Select())
                                if (!string.IsNullOrEmpty(_drTF["AMT"].ToString()))
                                {
                                    _amtnTotal += Convert.ToDecimal(string.Format("{0:F" + Comp.GetCompInfo("").DecimalDigitsInfo.System.POI_AMT + "}"
                                        , Convert.ToDecimal(_drTF["AMT"])));
                                }
                            if (string.IsNullOrEmpty(dr["CUR_ID"].ToString()))
                            {
                                if (!string.IsNullOrEmpty(dr["AMTN_BB"].ToString()))
                                    _amtnTotal -= Convert.ToDecimal(dr["AMTN_BB"]);
                                if (!string.IsNullOrEmpty(dr["AMTN_BC"].ToString()))
                                    _amtnTotal -= Convert.ToDecimal(dr["AMTN_BC"]);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(dr["AMT_BB"].ToString()))
                                    _amtnTotal -= Convert.ToDecimal(dr["AMT_BB"]);
                                if (!string.IsNullOrEmpty(dr["AMT_BC"].ToString()))
                                    _amtnTotal -= Convert.ToDecimal(dr["AMT_BC"]);
                            }
                            if (!string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
                                _amtnTotal -= Convert.ToDecimal(dr["AMTN_CBAC"]);

                            if (_amtnTotal != 0)
                            {
                                dr.SetColumnError("ZHANG_ID", "RCID=COMMON.HINT.AMTN_DIFFERENT");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }

                        //Check daily storage which have locked
                        if (!String.IsNullOrEmpty(dr["RK_DD"].ToString()))
                        {
                            if (!Prdt1Day.CheckLockID(dr["BAT_NO"].ToString(), dr["WH"].ToString(), dr["DEP_RK"].ToString(),
                                dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString(), Convert.ToDateTime(dr["RK_DD"])))
                            {
                                dr.SetColumnError("RK_DD", "该货品以封存，不能出库");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }
                }
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //取得保存单号
                    dr["PS_NO"] = _sq.Set(_psId, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["PS_DD"]), dr["BIL_TYPE"].ToString());
                    //写入默认栏位值
                    dr["PRT_SW"] = "N";
                    if (_psId == "SB")
                    {
                        dr["PO_ID"] = "F";
                    }
                    else if (_psId == "SA")
                    {
                        dr["CK_CLS_ID"] = "F";
                        dr["LZ_CLS_ID"] = "F";
                        if (string.IsNullOrEmpty(dr["YD_ID"].ToString()))
                            dr["YD_ID"] = "T";
                    }
                    //如果没有输入交易方式时，取客户默认交易方式
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
                        }
                        else
                        {
                            throw new SunlikeException("RCID=INV.HINT.GETPAYINFO_ERROR");//无法取得客户交易方式
                        }
                    }
                }
                else if (statementType == StatementType.Delete)
                {
                    if (CheckSD(dr["PS_NO", DataRowVersion.Original].ToString()))
                    {
                        throw new SunlikeException("RCID=INV.DRPSA.SDEXIST");//已转折让单,无法删除
                    }

                    string _error = _sq.Delete(dr["PS_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}

                    }
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
                    //判断是否走审核流程
                    //if (_isRunAuditing && string.IsNullOrEmpty(_saveID))
                    //{
                    //    Auditing _auditing = new Auditing();
                    //    _auditing.DelBillWaitAuditing("DRP", _psId, dr["PS_NO", DataRowVersion.Original].ToString());
                    //}
                }

                //#region 审核相关
                //if (string.IsNullOrEmpty(_saveID))
                //{
                //    AudParamStruct _aps;
                //    if (dr.RowState != DataRowState.Deleted)
                //    {
                //        _aps.BIL_DD = Convert.ToDateTime(dr["PS_DD"]);
                //        _aps.BIL_ID = dr["PS_ID"].ToString();
                //        _aps.BIL_NO = dr["PS_NO"].ToString();
                //        _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //        _aps.CUS_NO = dr["CUS_NO"].ToString();
                //        _aps.DEP = dr["DEP"].ToString();
                //        _aps.SAL_NO = dr["SAL_NO"].ToString();
                //        _aps.USR = dr["USR"].ToString();
                //        _aps.MOB_ID = "";
                //    }
                //    else
                //    {
                //        _aps = new AudParamStruct(Convert.ToString(dr["PS_ID", DataRowVersion.Original]), Convert.ToString(dr["PS_NO", DataRowVersion.Original]));
                //    }
                //    Auditing _auditing = new Auditing();
                //    string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //    if (!string.IsNullOrEmpty(_auditErr))
                //    {
                //        throw new SunlikeException(_auditErr);
                //    }
                //}
                //else
                //{
                //    if (statementType != StatementType.Delete)
                //    {
                //        if (_isRunAuditing)
                //        {
                //            dr["CHK_MAN"] = System.DBNull.Value;
                //            dr["CLS_DATE"] = System.DBNull.Value;
                //        }
                //        else
                //        {
                //            dr["CHK_MAN"] = dr["USR"].ToString();
                //            dr["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                //        }
                //    }
                //}

                //#endregion

                if (dr.RowState != DataRowState.Deleted)
                {
                    if (string.IsNullOrEmpty(dr["CUR_ID"].ToString()))
                    {
                        dr["EXC_RTO"] = 1;
                    }
                    
                    //写输单日期
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    if (dr["PS_ID"].ToString() == "SA" || dr["PS_ID"].ToString() == "SB")
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
                                    dr["AMTN_EP"] = _amtnNetEp1;
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
                                    dr["AMTN_EP1"] = _amtnNetEp;
                                }
                            }
                            catch (Exception _ex)
                            {
                                throw _ex;
                            }
                        }
                        #endregion
                    }
                    if (!_isRunAuditing)
                    {
                        if (dr.Table.DataSet.ExtendedProperties.ContainsKey("FROMLPLI")) //控制补开发反审核时可以更新有冲过帐的销货单
                        { }
                        else
                            this.UpdateMfArp(dr, false);//因预收款要取得该立账单故放此处
                    }
                    //预收款
                    decimal _amtnRcv = 0;
                    if (dr["AMTN_BC"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_BC"]);
                    }
                    if (dr["AMTN_BB"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_BB"]);
                    }
                    if (dr["AMTN_CHK"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_CHK"]);
                    }
                    if (dr["AMTN_OTHER"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_OTHER"]);
                    }
                    if (dr["AMTN_IRP"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_IRP"]);
                    }
                    if (dr["AMTN_CBAC"].ToString() != "")
                    {
                        _amtnRcv += Convert.ToDecimal(dr["AMTN_CBAC"]);
                    }

                    if (_amtnRcv != 0)
                    {
                        UpdateMon(dr);
                    }
                    else if (_amtnRcv == 0 && !string.IsNullOrEmpty(dr["RP_NO"].ToString()))
                    {
                        Bills _bills = new Bills();
                        if (dr["PS_ID"].ToString() == "SB")
                        {
                            _bills.DelRcvPay("2", dr["RP_NO"].ToString());
                        }
                        else
                        {
                            _bills.DelRcvPay("1", dr["RP_NO"].ToString());
                        }
                    }
                }
                //产生凭证
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }
                //				Accounts _acc = new Accounts();
                //				_acc.Receive(dr);

                //更新开发票
                //try
                //{
                //    //DrpTaxAa _drpTaxAa = new DrpTaxAa();
                //    //_drpTaxAa.UpdateTaxAa(dr.Table.DataSet, _psId, psNo);
                //}
                //catch (Exception _ex)
                //{
                //    dr.SetColumnError("INV_NO", _ex.Message);
                //    status = UpdateStatus.SkipAllRemainingRows;
                //}
                // UpdateOSydID(dr, false);
            }
            else if (tableName == "TF_PSS")
            {
                if (statementType != StatementType.Delete)
                {
                    #region
                    dr["PS_DD"] = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_DD"];
                    if (dr.Table.DataSet.Tables["MF_PSS"].Rows.Count > 0)
                    {
                        if (String.IsNullOrEmpty(dr["CUS_OS_NO"].ToString()))
                        {
                            dr["CUS_OS_NO"] = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_OS_NO"];
                        }
                    }
                    string _usr = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["USR"].ToString();
                    Prdt _prdt = new Prdt();
                    if (dr["PS_ID"].ToString() != "SD")
                    {
                        //检查货品代号
                        if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["PS_DD"])))
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

                                //销项单据如果货品大类为A，B，C则允许特征为空
                                bool _markCanNull = false;
                                if (dr["PS_ID"].ToString() == "SA" || dr["PS_ID"].ToString() == "SB")
                                {
                                    DataTable _dtPrdt = _prdt.GetPrdt(dr["PRD_NO"].ToString());
                                    if (_dtPrdt.Rows.Count > 0)
                                    {
                                        if (_dtPrdt.Rows[0]["KND"].ToString().IndexOfAny(new char[] { 'A', 'B', 'C' }) != -1)
                                        {
                                            _markCanNull = true;
                                        }
                                    }
                                }

                                for (int i = 0; i < _dtMark.Rows.Count; i++)
                                {
                                    if (_markCanNull && String.IsNullOrEmpty(_aryMark[i].Trim()))
                                    {
                                        continue;
                                    }

                                    string _fldName = _dtMark.Rows[i]["FLDNAME"].ToString();
                                    if (!_mark.IsExist(_fldName, dr["PRD_NO"].ToString(), _aryMark[i]))
                                    {
                                        dr.SetColumnError(_fldName, "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _aryMark[i].Trim());//货品特征[{0}]不存在
                                        status = UpdateStatus.SkipAllRemainingRows;
                                    }
                                }
                            }
                        }
                    }
                    //检查库位
                    WH _wh = new WH();
                    Cust _cust = new Cust();
                    if ((dr.Table.DataSet.ExtendedProperties["CONTROL_CUS_WH"] != null && dr.Table.DataSet.ExtendedProperties["CONTROL_CUS_WH"].ToString() == "1")
                        || !_cust.IsDrp_id(dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_NO"].ToString()))//不是经销商不需要限制库位
                    {
                        if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr["PS_DD"])))
                        {
                            dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//仓库代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    else if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr["PS_DD"]), dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_NO"].ToString()))
                    {
                        dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//仓库代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }

                    //检查业务员
                    Salm _salm = new Salm();
                    if (!String.IsNullOrEmpty(dr["SAL_NO"].ToString()) && !_salm.IsExist(_usr, dr["SAL_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_DD"])))
                    {
                        dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    if (!String.IsNullOrEmpty(dr["SAL_NO1"].ToString()) && !_salm.IsExist(_usr, dr["SAL_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_DD"])))
                    {
                        dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }

                    /*来源单号判断 by zb
                     *因可能转入多笔受订，表头单号格式就变为SO710090001-2，故放到此处*/
                    if (dr["OS_ID"].ToString() == "SO")
                    {
                        DrpSO _drpSo = new DrpSO();
                        DataTable _dsSo = _drpSo.GetDataMf("SO", dr["OS_NO"].ToString());
                        if (_dsSo.Rows.Count == 0)
                        {
                            dr.SetColumnError("OS_NO", "RCID=COMMON.HINT.OS_NO_NULL");//来源单号不存在
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                        //赋值回冲量
                        dr["QTY_PS"] = dr["QTY"];
                    }
                    if (dr["OS_ID"].ToString() == "LN")
                    {
                        //赋值回冲量
                        dr["QTY_PS"] = dr["QTY"];
                    }
                    //检查销货金额是否为空
                    if (!dr.Table.DataSet.ExtendedProperties.ContainsKey("DRPSAWIN")
                        && !dr.Table.DataSet.ExtendedProperties.ContainsKey("DRPSBWIN"))
                    {
                        if (Users.GetSpcPswdString(_usr, "SA_AMT_NOALLOW") == "T")
                        {
                            if (dr["PS_ID"].ToString() != "SD" && (String.IsNullOrEmpty(dr["QTY"].ToString()) || (!String.IsNullOrEmpty(dr["QTY"].ToString()) && Convert.ToDecimal(dr["QTY"]) <= 0)))
                            {
                                dr.SetColumnError("QTY", "RCID=COMMON.HINT.QTY_ISNULL");//数量不允许为空
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            if (dr["PS_ID"].ToString() == "SD")
                            {
                                if (String.IsNullOrEmpty(dr["AMTN_NET"].ToString()))
                                {
                                    dr.SetColumnError("AMTN_NET", "RCID=COMMON.HINT.AMT_ISNULL");//金额不允许为空
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                            else
                            {
                                if (dr["FREE_ID"] != null && dr["FREE_ID"].ToString() == "F")
                                {
                                    if ((String.IsNullOrEmpty(dr["AMTN_NET"].ToString()) || Convert.ToDecimal(dr["AMTN_NET"]) <= 0))
                                    {
                                        dr.SetColumnError("AMTN_NET", "RCID=COMMON.HINT.AMT_ISNULL");//金额不允许为空
                                        status = UpdateStatus.SkipAllRemainingRows;
                                    }
                                }
                            }
                        }
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
                    if (_psId == "SA")
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(dr["OTH_ITM"])))
                            dr["OTH_ITM"] = dr["PRE_ITM"];//不知道之前为何要这样做 lzj 090403
                        //判断批号管制
                        if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                        {
                            Bat _bat = new Bat();
                            if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                            {
                                dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM=" + dr["BAT_NO"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }
                    #endregion

                    #region 维修卡记录

                    if (statementType == StatementType.Update)
                        UpdateWCH(dr, true, false);
                    UpdateWCH(dr, false, false);

                    #endregion

                }
                else
                {
                    if (string.Compare(dr["PS_ID", DataRowVersion.Original].ToString(), "SB") == 0)
                    {
                        #region 强制退货的是否允许删除
                        string _osId = dr["OS_ID", DataRowVersion.Original].ToString();
                        string _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                        string _othItm = dr["OTH_ITM", DataRowVersion.Original].ToString();
                        if (!this.chkRtnDel(_osId, _osNo, _othItm))
                        {
                            throw new SunlikeException("RCID=MTN.HINT.CHKRTNFORBIT,PARAM=" + _osNo + ",PARAM=" + dr["PRD_NO", DataRowVersion.Original].ToString());
                        }
                        #endregion
                    }

                    #region 维修卡记录 删除
                    UpdateWCH(dr, true, false);
                    #endregion


                    //判断是否有补开发票则不允许删除
                    checktflz(dr);

                }
                if (!_isRunAuditing)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        this.UpdateCustMtn(dr, true);
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {
                        this.UpdateCustMtn(dr, false);
                        this.UpdateCustMtn(dr, true);
                    }
                    else if (dr.RowState == DataRowState.Deleted)
                    {
                        this.UpdateCustMtn(dr, false);
                    }
                }
                UpdateOS(dr, false);
            }
            //更新序列号记录
            if (_barCodeNo == 0)
            {
                if (!_isRunAuditing)
                {
                    try
                    {
                        this.UpdateBarCode(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                    }
                    catch (Exception _ex)
                    {
                        status = UpdateStatus.SkipAllRemainingRows;
                        throw new SunlikeException(_ex.Message, _ex);
                    }
                }
                string _fieldList = "PS_ID,PS_NO,PS_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
                SQLBatchUpdater _sbu = new SQLBatchUpdater(Comp.Conn_DB);
                _sbu.BatchUpdateSize = 50;
                _sbu.BatchUpdate(dr.Table.DataSet.Tables["TF_PSS3"], _fieldList);
            }
            _barCodeNo++;
        }

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
            if (dr.RowState != DataRowState.Deleted)
            {
                if (tableName == "PAY_B2C")
                {
                    _psId = dr["BIL_ID"].ToString();
                }
                else
                {
                    _psId = dr["PS_ID"].ToString();
                }
            }
            else
            {
                if (tableName == "PAY_B2C")
                {
                    _psId = dr["BIL_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    _psId = dr["PS_ID", DataRowVersion.Original].ToString();
                }
            }
            //判断是否走审核流程
            if (!_isRunAuditing)
            {
                if (tableName == "MF_PSS")
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        //更新SARP
                        this.UpdateSarp(dr);
                        //更新会员积分
                        this.UpdateJf(SunlikeDataSet.ConvertTo(dr.Table.DataSet), false);
                    }
                    else
                    {
                        //更新会员积分
                        this.UpdateJf(SunlikeDataSet.ConvertTo(dr.Table.DataSet), true);
                    }

                    #region 更新POS订单
                    if (dr.RowState == DataRowState.Added)
                    {
                        UpdatePosOs(dr, true);
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {
                        UpdatePosOs(dr, false);
                        UpdatePosOs(dr, true);
                    }
                    else if (dr.RowState == DataRowState.Deleted)
                    {
                        UpdatePosOs(dr, false);
                    }
                    #endregion
                }

                if (tableName == "TF_PSS")
                {
                    //if (_psId != "SD") //lzj bugNo89879
                    {
                        //修改分仓存量、原销货单已退数量
                        if (statementType == StatementType.Insert)
                        {
                            this.UpdateWh(dr, true);
                            this.UpdateQtyRtn(dr, true);
                            this.UpdateQtySb(dr, true);
                            this.UpdateMonSbac(dr, true);

                        }
                        else if (statementType == StatementType.Delete)
                        {
                            this.UpdateWh(dr, false);
                            this.UpdateQtyRtn(dr, false);
                            this.UpdateQtySb(dr, false);
                            this.UpdateMonSbac(dr, false);

                        }
                        else if (statementType == StatementType.Update)
                        {
                            this.UpdateWh(dr, false);
                            this.UpdateQtyRtn(dr, false);
                            this.UpdateQtySb(dr, false);
                            this.UpdateMonSbac(dr, false);

                            this.UpdateWh(dr, true);
                            this.UpdateQtyRtn(dr, true);
                            this.UpdateQtySb(dr, true);
                            this.UpdateMonSbac(dr, true);

                        }
                    }
                }
            }
            #region 更新进货退回数量
            if (tableName == "TF_PSS")
            {
                if (statementType == StatementType.Insert)
                {
                    if (dr["PS_ID"].ToString() == "SB" && dr["OS_ID"].ToString() == "PB")
                    {
                        this.UpdateQtyRtnForSB(dr, true, false);
                    }
                }
                else if (statementType == StatementType.Delete)
                {
                    if (dr["PS_ID", DataRowVersion.Original].ToString() == "SB" && dr["OS_ID", DataRowVersion.Original].ToString() == "PB")
                    {
                        UpdateQtyRtnForSB(dr, false, false);
                    }
                }
                else if (statementType == StatementType.Update)
                {
                    if (dr["PS_ID", DataRowVersion.Original].ToString() == "SB" && dr["OS_ID", DataRowVersion.Original].ToString() == "PB")
                    {
                        UpdateQtyRtnForSB(dr, false, false);
                    }
                    if (dr["PS_ID"].ToString() == "SB" && dr["OS_ID"].ToString() == "PB")
                    {
                        UpdateQtyRtnForSB(dr, true, false);
                    }
                }
            }
            #endregion

            #region 更新客户帐户
            if (tableName == "MF_PSS")
            {
                if (statementType == StatementType.Insert)
                {
                    this.UpdateMonCbac(dr, true);
                    this.UpdateBac(dr, true);
                }
                else if (statementType == StatementType.Update)
                {
                    this.UpdateMonCbac(dr, false);
                    this.UpdateBac(dr, false);
                    this.UpdateMonCbac(dr, true);
                    this.UpdateBac(dr, true);
                }
                else if (statementType == StatementType.Delete)
                {
                    this.UpdateMonCbac(dr, false);
                    this.UpdateBac(dr, false);
                }
            }
            #endregion

            #region 更新盘点单
            if (tableName == "TF_PSS" && !String.IsNullOrEmpty(_bil_NO))
            {
                DRPPI _drppi = new DRPPI();
                if (statementType == StatementType.Insert && !String.IsNullOrEmpty(dr["PT_ITM"].ToString()))
                {
                    string _errorMsg = _drppi.UpdateMF_PT(_bil_NO, dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), false);
                    if (!string.IsNullOrEmpty(_errorMsg))
                        throw new SunlikeException(_errorMsg);
                    _drppi.UpdateTF_PT(_bil_NO, Convert.ToInt32(dr["PT_ITM"]), dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), false);
                }
                else if (statementType == StatementType.Delete)
                {
                    bool _delAll = false;
                    if (dr.Table.DataSet.Tables["MF_PSS"].Rows[0].RowState == DataRowState.Deleted)
                    {
                        _delAll = true;
                    }
                    if (_delAll)
                    {
                        _drppi.DeleteDRPPI(_bil_NO, "SA", false);
                    }
                }
            }
            #endregion

            #region 更新组合单
            if (tableName == "TF_PSS")
            {
                if (_psId == "SA")
                {
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateMrpMb(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateMrpMb(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateMrpMb(dr, false);
                        this.UpdateMrpMb(dr, true);

                        if (this._itmNoNoUpdate.ContainsKey(dr["ITM"].ToString()))
                        {
                            this._itmNoNoUpdate.Remove(dr["ITM"].ToString());
                        }
                    }
                }
            }
            #endregion

            #region 更新挂帐信息
            if (tableName == "MF_PSS")
            {
                if (_psId == "SA")
                {
                    DbDRPSA _dbDrpSa = new DbDRPSA(Comp.Conn_DB);
                    if (statementType == StatementType.Delete)
                    {
                        _dbDrpSa.UpdatePOSGZ(dr["DEP", DataRowVersion.Original].ToString(),
                            dr["PS_NO", DataRowVersion.Original].ToString(),
                            dr["PS_ID", DataRowVersion.Original].ToString(), false);
                    }
                    else
                    {
                        _dbDrpSa.UpdatePOSGZ(dr["DEP"].ToString(),
                           dr["PS_NO"].ToString(), dr["PS_ID"].ToString(),
                           String.Compare(this._hangZhang, "T") == 0 ? true : false);
                    }
                }
            }
            #endregion

            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            if (this._alOsNo.Count > 0)
            {
                if (this._alOsNo[0].ToString().StartsWith("LN"))
                {//借出单转入
                    DRPBN _bn = new DRPBN();
                    _bn.UpdateQtyRtn(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                }
                else
                {
                    DrpSO _so = new DrpSO();
                    _so.UpdateQtyPs(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                }
            }
            if (!_isRunAuditing && ds.Tables["MF_PSS"].Select().Length > 0)
            {
                try
                {
                    DRPME _drpMe = new DRPME();
                    _drpMe.UpdateDrpMe(ds);
                }
                catch (Exception _ex)
                {
                    throw new Exception("营销费用计算保存错误！\n" + _ex.Message);
                }
            }
            //更新虚拟商品
            if (ds.Tables["PRDT_VIR"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["PRDT_VIR"].Rows.Count; i++)
                {
                    DataRow _dr = ds.Tables["PRDT_VIR"].Rows[i];
                    Query _query = new Query();
                    string _tplNo = "";
                    string _osNo = "";
                    string _osXml = "";
                    //if (CompInfo.CaseID == "55" && _dr.RowState == DataRowState.Added)
                    //{
                    //    DataRow _drBody = ds.Tables["TF_PSS"].Rows.Find(new object[] { "SA", _dr["PS_NO"], _dr["ITM"] });
                    //    _tplNo = _drBody["TPL_NO"].ToString();
                    //    _osNo = _drBody["OS_NO"].ToString();
                    //    _osXml = _drBody["ONLINESERVICE_XML"].ToString();
                    //}
                    //if (CompInfo.CaseID == "55" && _dr.RowState == DataRowState.Added && _tplNo == "A03")
                    //{
                    //    OnlineService.WebService1 _service = new Sunlike.Business.OnlineService.WebService1();
                    //    _service.Url = OnlineServiceUrl;
                    //    if (string.IsNullOrEmpty(_osNo))
                    //    {
                    //        _osNo = _dr["PS_NO"].ToString();
                    //    }
                    //    string _result = _service.GetVirNo(Security.EncodingCompData(DateTime.Today.ToString(Comp.SQLDateFormat), _osNo), _osNo, Convert.ToInt32(_dr["ITM"]), _osXml);
                    //    if (string.IsNullOrEmpty(_result))
                    //    {
                    //        throw new Exception("OnlineService无可用授权号！");
                    //    }
                    //    else
                    //    {
                    //        System.Xml.XmlDocument _xd = new System.Xml.XmlDocument();
                    //        _xd.LoadXml(_result);
                    //        string _sql = "insert into PRDT_VIR (PRD_NO,CARD_ID,PSWD,PS_NO,ITM) values ('" + _dr["PRD_NO"].ToString()
                    //            + "','" + Security.DecodingCompData(_xd.DocumentElement.GetAttribute("VIR_NO"), "online")
                    //            + "','" + Security.DecodingCompData(_xd.DocumentElement.GetAttribute("PWD"), "online")
                    //            + "','" + _dr["PS_NO"].ToString() + "'," + _dr["ITM"].ToString() + ")";
                    //        _query.RunSql(_sql);
                    //    }
                    //}
                    //else
                    {
                        if (_dr.RowState == DataRowState.Added || _dr.RowState == DataRowState.Modified)
                        {
                            string _sql = "select PS_NO,ITM from PRDT_VIR where PRD_NO='" + _dr["PRD_NO"].ToString()
                                + "' and CARD_ID='" + _dr["CARD_ID"].ToString() + "'";
                            SunlikeDataSet _dsPrdtVir = _query.DoSQLString(_sql);
                            if (_dsPrdtVir.Tables[0].Rows.Count == 0)
                            {
                                throw new Exception("RCID=INV.DRPSA.PRDT_VIR_NULL,PARAM=" + _dr["PRD_NO"].ToString() + ",PARAM=" + _dr["CARD_ID"].ToString());
                            }
                            else if (_dr.RowState == DataRowState.Added && !string.IsNullOrEmpty(_dsPrdtVir.Tables[0].Rows[0]["PS_NO"].ToString()))
                            {
                                throw new Exception("RCID=INV.DRPSA.PRDT_VIR_SALE,PARAM=" + _dr["PRD_NO"].ToString() + ",PARAM=" + _dr["CARD_ID"].ToString());
                            }
                            else
                            {
                                _sql = "update PRDT_VIR set PS_NO='" + _dr["PS_NO"].ToString() + "',ITM=" + _dr["ITM"].ToString()
                                    + " where PRD_NO='" + _dr["PRD_NO"].ToString() + "' and CARD_ID='" + _dr["CARD_ID"].ToString() + "'";
                                _query.RunSql(_sql);
                            }
                        }
                        else if (_dr.RowState == DataRowState.Deleted)
                        {
                            string _sql = "select PS_NO,ITM from PRDT_VIR where PRD_NO='" + _dr["PRD_NO", DataRowVersion.Original].ToString()
                                + "' and CARD_ID='" + _dr["CARD_ID", DataRowVersion.Original].ToString() + "'";
                            SunlikeDataSet _dsPrdtVir = _query.DoSQLString(_sql);
                            if (_dsPrdtVir.Tables[0].Rows.Count == 0)
                            {
                                throw new Exception("RCID=INV.DRPSA.PRDT_VIR_NULL,PARAM=" + _dr["PRD_NO"].ToString() + ",PARAM=" + _dr["CARD_ID"].ToString());
                            }
                            else
                            {
                                _sql = "update PRDT_VIR set PS_NO=null,ITM=null where PRD_NO='" + _dr["PRD_NO", DataRowVersion.Original].ToString()
                                    + "' and CARD_ID='" + _dr["CARD_ID", DataRowVersion.Original].ToString() + "'";
                                _query.RunSql(_sql);
                            }
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, string> _kvp in this._itmNoNoUpdate)
            {
                DataRow[] _drs = ds.Tables["TF_PSS"].Select("ITM='" + _kvp.Key + "'");
                if (_drs.Length > 0)
                {
                    this.UpdateMrpMb(_drs[0], false);
                    this.UpdateMrpMb(_drs[0], true);
                    this.UpdateMonSbac(_drs[0], true);
                    this.UpdateWh(_drs[0], true);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            ////#region 单据追踪
            ////DataTable _dtMf = ds.Tables["MF_PSS"];
            ////if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            ////{
            ////    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            ////    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            ////    {
            ////        _bilId = _dtMf.Rows[0]["PS_ID"].ToString();
            ////    }
            ////    else
            ////    {
            ////        _bilId = _dtMf.Rows[0]["PS_ID", DataRowVersion.Original].ToString();
            ////    }
            ////    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            ////}
            ////#endregion
            if (ds.Tables["MF_PSS"].Rows.Count > 0
                && ds.Tables["MF_PSS"].Rows[0].RowState == DataRowState.Modified
                && this._isRunAuditing
                && !String.IsNullOrEmpty(ds.Tables["MF_PSS"].Rows[0]["CHK_MAN"].ToString()))
            {
                bool _setCanModify = true;
                if (ds.ExtendedProperties.ContainsKey("IS_MTNFXCLOSE"))
                {
                    _setCanModify = false;
                }
                string _rbError = this.RollBack(ds.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString(),
                    ds.Tables["MF_PSS"].Rows[0]["PS_NO"].ToString(), false, _setCanModify);
                if (!String.IsNullOrEmpty(_rbError))
                {
                    throw new SunlikeException(_rbError);
                }
                ds.Tables["MF_PSS"].Rows[0]["ARP_NO"] = "";
            }
            //判断MF_JF是否有资料，如果没有就新增
            if (ds.Tables["MF_JF"].Rows.Count == 0)
            {
                DataRow _dr = ds.Tables["MF_JF"].NewRow();
                _dr["JF_NO"] = "JF";
                ds.Tables["MF_JF"].Rows.Add(_dr);
            }
            //
            Cust _cust = new Cust();
            DataRow[] _drSel = null;
            if (ds.Tables["TF_PSS"].Rows.Count > 0)
            {
                string _cusNo = "";
                if (ds.Tables["MF_PSS"].Rows[0].RowState == System.Data.DataRowState.Deleted)
                {
                    _cusNo = ds.Tables["MF_PSS"].Rows[0]["CUS_NO", System.Data.DataRowVersion.Original].ToString();
                }
                else
                {
                    _cusNo = ds.Tables["MF_PSS"].Rows[0]["CUS_NO"].ToString();
                }
                DataTable _whDt = _cust.GetCus_WH(_cusNo);
                for (int i = 0; i < _whDt.Rows.Count; i++)
                {
                    _drSel = ds.Tables["TF_PSS"].Select("ITM=1", "", System.Data.DataViewRowState.Deleted);
                    if (_drSel != null && _drSel.Length > 0)
                    {
                        if (_drSel[0]["WH", DataRowVersion.Original].ToString() == _whDt.Rows[i]["WH"].ToString())
                        {
                            _updateAmtnInvDelete = true;//只有当表身第一笔数据的库位是该客户的库位时才更改
                            break;
                        }
                    }
                }
                for (int i = 0; i < _whDt.Rows.Count; i++)
                {
                    _drSel = ds.Tables["TF_PSS"].Select("ITM=1", "", System.Data.DataViewRowState.Added);
                    if (_drSel != null && _drSel.Length > 0)
                    {
                        if (_drSel[0]["WH"].ToString() == _whDt.Rows[i]["WH"].ToString())
                        {
                            _updateAmtnInvAdd = true;//只有当表身第一笔数据的库位是该客户的库位时才更改
                            break;
                        }
                    }
                }
            }
            //*************************删除时才起作用*****************************
            //预收款
            if (ds.Tables["MF_PSS"].Rows.Count > 0 && ds.Tables["MF_PSS"].Rows[0].RowState == DataRowState.Deleted)
            {
                Bills _bills = new Bills();  //by zb 2007-05-14
                if (ds.Tables["MF_PSS"].Rows[0]["PS_ID", DataRowVersion.Original].ToString() == "SB")
                {
                    _bills.DelRcvPay("2", ds.Tables["MF_PSS"].Rows[0]["RP_NO", DataRowVersion.Original].ToString());
                }
                else
                {
                    _bills.DelRcvPay("1", ds.Tables["MF_PSS"].Rows[0]["RP_NO", DataRowVersion.Original].ToString());
                }
            }
            //立账
            this.UpdateMfArp(ds);
            //更新SARP
            this.UpdateSarp(ds);
            //********************************************************************

            if (String.Compare(this._hangZhang, "F") == 0)
            {
                foreach (DataRow _drTf in ds.Tables["TF_PSS"].Select(""))
                {
                    if (_drTf.RowState == DataRowState.Unchanged || _drTf.RowState == DataRowState.Modified)
                    {
                        this._itmNoNoUpdate.Add(_drTf["ITM"].ToString(), "");
                    }
                }
            }
            base.BeforeDsSave(ds);
        }

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
            //DataSet _dsSa = this.GetData("","", pBB_ID, pBB_NO, false, false);
            //foreach (DataRow dr in _dsSa.Tables["TF_PSS"].Rows)
            //{
            //    #region 回写采购单已采购量
            //    if (string.Compare("SB", pBB_ID) == 0)
            //    {
            //        if (string.Compare("PB", dr["OS_ID"].ToString()) == 0)
            //        {
            //            this.UpdateQtyRtnForSB(dr, true, false, false);
            //        }
            //    }
            //    #endregion
            //}

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
                SunlikeDataSet _ds = this.GetData("", chk_man, bil_id, bil_no);
                _ds.Tables["TF_PSS_BAR"].TableName = "TF_PSS3";
                DataRow _drHead = _ds.Tables["MF_PSS"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_PSS"];
                DataTable _dtBar = _ds.Tables["TF_PSS3"];
                //UpdateOSydID(_drHead, false, true);// 更新出库单YD_ID

                #region 维修记录
                foreach (DataRow dr in _dtBody.Select())
                    UpdateWCH(dr, false, true);
                #endregion
                //信用额度判断
                if (bil_id == "SA")
                {
                    Cust _cust = new Cust();
                    if (_cust.GetCrdId(_drHead["CUS_NO"].ToString()) == "2")
                    {
                        decimal _totalLimNr = 0;
                        //增加客户信用额度
                        _totalLimNr += _cust.GetLim_NR(_drHead["CUS_NO"].ToString());
                        //减掉受订单的总金额
                        for (int i = 0; i < _dtBody.Rows.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["AMTN_NET"].ToString()))
                                _totalLimNr -= Convert.ToDecimal(_dtBody.Rows[i]["AMTN_NET"]);
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["TAX"].ToString()))
                                _totalLimNr -= Convert.ToDecimal(_dtBody.Rows[i]["TAX"]);
                        }
                        if (_totalLimNr < 0)
                        {
                            throw new SunlikeException("RCID=SYS.CUST.LIM_NR_ALERT");//信用额度不足。
                        }
                    }
                }

                //立账
                this.UpdateMfArp(_drHead);
                //设定审核人
                DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
                _sa.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);

                _drHead["CHK_MAN"] = chk_man;
                _drHead["CLS_DATE"] = cls_dd;
                //预收款
                decimal _amtnRcv = 0;
                if (!String.IsNullOrEmpty(_drHead["AMTN_BC"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_BC"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_BB"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_BB"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_CHK"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_CHK"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_OTHER"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_OTHER"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_IRP"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_IRP"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_CBAC"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_CBAC"]);
                }
                if (_amtnRcv != 0)
                {
                    string _rpNo = UpdateMon(_drHead);
                    //更新收款单号
                    this.UpdateRpNo(bil_id, bil_no, _rpNo);
                }
                //更新SARP
                this.UpdateSarp(_drHead);
                //更新凭证
                string _vohNo = this.UpdateVohNo(_drHead, StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);
                //更新会员积分
                if (_ds.Tables["MF_JF"].Rows.Count == 0)
                {
                    DataRow _drJf = _ds.Tables["MF_JF"].NewRow();
                    _drJf["JF_NO"] = "JF";
                    _ds.Tables["MF_JF"].Rows.Add(_drJf);
                }
                this.UpdateJf(_ds, false);
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
                //if (bil_id != "SD")//lzj bugNo89879
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
                    this._auditBarCode = true;
                    this.UpdateBarCode(_ds);
                    //修改库存
                    for (int i = 0; i < _dtBody.Rows.Count; i++)
                    {
                        //修改分仓存量
                        this.UpdateWh(_dtBody.Rows[i], true);
                        //修改原销货单已退数量
                        this.UpdateQtyRtn(_dtBody.Rows[i], true);
                        this.UpdateQtySb(_dtBody.Rows[i], true);
                        this.UpdateCustMtn(_dtBody.Rows[i], true);
                        this.UpdateMonSbac(_dtBody.Rows[i], true);
                        UpdateOS(_dtBody.Rows[i], false, true);
                        #region 回写采购单已采购量
                        if (string.Compare("SB", bil_id) == 0)
                        {
                            if (string.Compare("PB", _dtBody.Rows[i]["OS_ID"].ToString()) == 0)
                            {
                                string _tmpChkMan = _drHead["CHK_MAN"].ToString();
                                _drHead["CHK_MAN"] = chk_man;
                                this.UpdateQtyRtnForSB(_dtBody.Rows[i], true, true);
                                _drHead["CHK_MAN"] = _tmpChkMan;
                            }
                        }
                        #endregion

                    }
                    if (this._alOsNo.Count > 0)
                    {
                        if (this._alOsNo[0].ToString().StartsWith("LN"))
                        {//借出单转入
                            DRPBN _bn = new DRPBN();
                            _bn.UpdateQtyRtn(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                        }
                        else
                        {
                            DrpSO _so = new DrpSO();
                            _so.UpdateQtyPs(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                        }
                    }
                }
                //更新客户储值单
                _drHead["CHK_MAN"] = chk_man;
                this.UpdateMonCbac(_drHead, true);
                this.UpdateBac(_drHead, true);
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    this.UpdateMrpMb(_dtBody.Rows[i], true);
                }

                if (!String.IsNullOrEmpty(_error))
                {
                    throw new SunlikeException(_error);
                }
                //更新营销费用
                DRPME _drpMe = new DRPME();
                _drpMe.UpdateDrpMe(_ds);
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
            return this.RollBack(bil_id, bil_no, true, true);
        }

        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="bil_id">单据识别码</param>
        /// <param name="bil_no">单据号码</param>
        /// <param name="isUpdateHead">更新表头审核信息</param>
        /// 
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no, bool isUpdateHead, bool isModify)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData("", null, bil_id, bil_no);
                _ds.Tables["TF_PSS_BAR"].TableName = "TF_PSS3";
                //单据是否有销退数量或冲账
                if (isModify)
                {
                    this.SetCanModify(_ds, false, true);
                    if (_ds.ExtendedProperties.Contains("CAN_MODIFY"))
                    {
                        if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                        {
                            return "RCID=INV.HINT.CANMODIFY";
                        }
                    }
                    if (_ds.ExtendedProperties.ContainsKey("BILL_VOH_AC_CONTROL"))
                    {
                        DrpVoh _voh = new DrpVoh();
                        return "RCID=INV.HINT.BILL_VOH_CONTRL3,PARAM=" + _ds.ExtendedProperties["VOH_AC_NO"].ToString();
                    }
                }
                DataRow _drHead = _ds.Tables["MF_PSS"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_PSS"];
                DataTable _dtBar = _ds.Tables["TF_PSS3"];

                // UpdateOSydID(_drHead, true, true);// 更新出库单YD_ID

                #region 维修卡记录 删除
                foreach (DataRow dr in _dtBody.Select())
                    UpdateWCH(dr, true, true);
                #endregion
                if (bil_id == "SB" || bil_id == "SD")
                {
                    Cust _cust = new Cust();
                    if (_cust.GetCrdId(_drHead["CUS_NO"].ToString()) == "2")
                    {
                        decimal _totalLimNr = 0;
                        //增加客户信用额度
                        _totalLimNr += _cust.GetLim_NR(_drHead["CUS_NO"].ToString());
                        //减掉受订单的总金额
                        for (int i = 0; i < _dtBody.Rows.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["AMTN_NET"].ToString()))
                                _totalLimNr -= Convert.ToDecimal(_dtBody.Rows[i]["AMTN_NET"]);
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["TAX"].ToString()))
                                _totalLimNr -= Convert.ToDecimal(_dtBody.Rows[i]["TAX"]);
                        }
                        if (_totalLimNr < 0)
                        {
                            throw new SunlikeException("RCID=SYS.CUST.LIM_NR_ALERT");//信用额度不足。
                        }
                    }
                }
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

                //if (bil_id != "SD")//lzj bugNo89879
                {
                    for (int i = 0; i < _dtBody.Rows.Count; i++)
                    {
                        //修改分仓存量
                        this.UpdateWh(_dtBody.Rows[i], false);
                        //修改原销货单已退数量
                        this.UpdateQtyRtn(_dtBody.Rows[i], false);
                        this.UpdateQtySb(_dtBody.Rows[i], false);
                        this.UpdateCustMtn(_dtBody.Rows[i], false);
                        this.UpdateMonSbac(_dtBody.Rows[i], false);
                        this.UpdateOS(_dtBody.Rows[i], true, true);
                        #region 回写采购单已采购量
                        if (string.Compare("SB", bil_id) == 0)
                        {
                            if (string.Compare("PB", _dtBody.Rows[i]["OS_ID", DataRowVersion.Original].ToString()) == 0)
                            {
                                this.UpdateQtyRtnForSB(_dtBody.Rows[i], false, true);
                            }
                        }
                        #endregion
                    }
                    if (this._alOsNo.Count > 0)
                    {
                        if (this._alOsNo[0].ToString().StartsWith("LN"))
                        {//借出单转入
                            DRPBN _bn = new DRPBN();
                            _bn.UpdateQtyRtn(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                        }
                        else
                        {
                            DrpSO _so = new DrpSO();
                            _so.UpdateQtyPs(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                        }
                    }
                    //更新序列号记录
                    for (int i = 0; i < _dtBar.Rows.Count; i++)
                    {
                        _dtBar.Rows[i].Delete();
                    }
                    this._auditBarCode = true;
                    this.UpdateBarCode(_ds);
                }
                DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
                //设定审核人
                //string _rpNo = "";
                if (isUpdateHead)
                {
                    _sa.UpdateChkMan(bil_id, bil_no, "", DateTime.Now);
                    //_rpNo = _drHead["RP_NO"].ToString();
                }
                //更新客户储值单
                _drHead["CHK_MAN"] = System.DBNull.Value;
                this.UpdateMonCbac(_drHead, true);
                this.UpdateBac(_drHead, true);
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    this.UpdateMrpMb(_dtBody.Rows[i], true);
                }

                //预收款
                decimal _amtnRcv = 0;
                if (!String.IsNullOrEmpty(_drHead["AMTN_BC"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_BC"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_BB"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_BB"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_CHK"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_CHK"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_OTHER"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_OTHER"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_IRP"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_IRP"]);
                }
                if (!String.IsNullOrEmpty(_drHead["AMTN_CBAC"].ToString()))
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_CBAC"]);
                }
                if (_amtnRcv != 0)
                {
                    string _rpNo = UpdateMon(_drHead);
                    //更新收款单号
                    this.UpdateRpNo(bil_id, bil_no, _rpNo);
                }
                //Bills _bills = new Bills();  //by zb 2007-05-14
                //if (!string.IsNullOrEmpty(_rpNo))
                //    _bills.DelRcvPay("1", _rpNo);

                _drHead.Delete();
                //立账
                this.UpdateMfArp(_drHead);
                if (isUpdateHead)
                {
                    _sa.UpdateArpNo(bil_id, bil_no, "");
                }
                //更新SARP
                this.UpdateSarp(_drHead);
                //更新凭证
                this.UpdateVohNo(_drHead, StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");
                //更新会员积分
                if (_ds.Tables["MF_JF"].Rows.Count == 0)
                {
                    DataRow _drJf = _ds.Tables["MF_JF"].NewRow();
                    _drJf["JF_NO"] = "JF";
                    _ds.Tables["MF_JF"].Rows.Add(_drJf);
                }
                this.UpdateJf(_ds, true);
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }
        #endregion


        private void UpdateMfArp(DataRow dr)
        {
            this.UpdateMfArp(dr, true);
        }

        private void UpdateMfArp(DataRow dr, bool updateArpNo)
        {
            dr = dr.Table.DataSet.Tables["MF_PSS"].Rows[0];
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_PSS"];

            decimal _amtn = 0;
            decimal _amt = 0;
            decimal _tax = 0;
            if (dr.RowState != DataRowState.Deleted)
            {
                DataRow[] _darBody = _dtBody.Select();
                for (int i = 0; i < _darBody.Length; i++)
                {
                    if (!String.IsNullOrEmpty(_darBody[i]["AMT"].ToString()))
                    {
                        _amt += Convert.ToDecimal(_darBody[i]["AMT"]);
                    }
                    if (!String.IsNullOrEmpty(_darBody[i]["AMTN_NET"].ToString()))
                    {
                        _amtn += Convert.ToDecimal(_darBody[i]["AMTN_NET"]);
                    }
                    if (!String.IsNullOrEmpty(_darBody[i]["TAX"].ToString()))
                    {
                        _tax += Convert.ToDecimal(_darBody[i]["TAX"]);
                    }
                }
                if (dr["TAX_ID"].ToString() == "3")
                {
                    _amt += _tax;
                }
                if (dr["PS_ID"].ToString() == "SB" || dr["PS_ID"].ToString() == "SD")
                {
                    _amt *= -1;
                    _amtn *= -1;
                    _tax *= -1;
                }
            }
            Arp _arp = new Arp();
            string _arpNo = "";
            //如果是删除或者是立帐方式由“单张立帐”改成“不立帐”或“收到发票后立账”
            //又或者立帐的情况下改变产商或立帐方式，要先删除原来的帐款
            if (dr.RowState != DataRowState.Added && dr["ZHANG_ID", DataRowVersion.Original].ToString() == "1")
            {
                _arpNo = dr["ARP_NO", DataRowVersion.Original].ToString();
                //取得本单据收款单信息
                Bills _bills = new Bills();
                string _rpId = "1";
                if (dr["PS_ID", DataRowVersion.Original].ToString() == "SB")
                {
                    _rpId = "2";
                }
                string _rpNo = "";
                _rpNo = dr["RP_NO", DataRowVersion.Original].ToString();
                SunlikeDataSet _dsMon = _bills.GetData(_rpId, _rpNo, false);
                if (!_arp.DeleteMfArp(_arpNo, _dsMon.Tables["TF_MON"], false))
                {
                    throw new SunlikeException("无法删除立帐单:" + _arpNo);
                }
            }
            if (dr.RowState != DataRowState.Deleted && dr["ZHANG_ID"].ToString() == "1")
            {
                decimal _excRto = 1;
                if (!String.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                    _excRto = Convert.ToDecimal(dr["EXC_RTO"]);
                _arpNo = _arp.UpdateMfArp("1", "2", dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), Convert.ToDateTime(dr["PS_DD"]), dr["BIL_TYPE"].ToString(),
                    dr["DEP"].ToString(), dr["USR"].ToString(), dr["CUR_ID"].ToString(), _excRto, _amtn + _tax, _amtn, _amt, _tax, dr, dr["REM"].ToString());
                if (_arpNo != dr["ARP_NO"].ToString())
                {
                    if (updateArpNo)
                    {
                        DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
                        _sa.UpdateArpNo(dr["PS_ID"].ToString(), dr["PS_NO"].ToString(), _arpNo);
                    }
                    dr["ARP_NO"] = _arpNo;
                }
            }
        }

        private void UpdateMfArp(DataSet ds)
        {
            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_PSS") && ds.Tables["MF_PSS"].Rows.Count > 0 && ds.Tables["MF_PSS"].Rows[0].RowState == System.Data.DataRowState.Deleted)
            {
                this.UpdateMfArp(ds.Tables["MF_PSS"].Rows[0]);
            }

        }
        private void UpdateWh(DataRow dr, bool IsAdd)
        {
            string _batNo = "";
            string _outDd = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _unit = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
            decimal _cst = 0;
            string osNo;//出库单转入？
            string _osId;
            string _posOsId = "";//POS订单标记
            //RKTypes _rktype = new RKTypes();
            if (IsAdd)
            {
                osNo = Convert.ToString(dr["CK_NO"]);
                _outDd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_DD"]).ToString(Comp.SQLDateFormat);
                _batNo = dr["BAT_NO"].ToString();
                _prdNo = dr["PRD_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _whNo = dr["WH"].ToString();
                _osId = dr["OS_ID"].ToString();
                _unit = dr["UNIT"].ToString();
                _posOsId = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["POS_OS_ID"].ToString();
                //_rktype.DEP = dr["DEP_RK"].ToString();
                //if (!String.IsNullOrEmpty(dr["RK_DD"].ToString()))
                //{
                //    _rktype.RK_DD = Convert.ToDateTime(dr["RK_DD"]).Date;
                //}
                //_rktype.LST_OTD = DateTime.Today;
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
                if (!String.IsNullOrEmpty(dr["CSTN_SAL"].ToString()))
                {
                    _cst = Convert.ToDecimal(dr["CSTN_SAL"]);
                }
                if (dr["PS_ID"].ToString() == "SA")
                {
                    _qty *= -1;
                    _qty1 *= -1;
                    _cst *= -1;
                }
                else if (dr["PS_ID"].ToString() == "SD")
                {
                    _qty = 0;
                }
            }
            else
            {
                osNo = Convert.ToString(dr["CK_NO", DataRowVersion.Original]);
                _outDd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_DD", DataRowVersion.Original]).ToString(Comp.SQLDateFormat);
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                _osId = dr["OS_ID", DataRowVersion.Original].ToString();
                _posOsId = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["POS_OS_ID", DataRowVersion.Original].ToString();
                //_rktype.DEP = dr["DEP_RK", DataRowVersion.Original].ToString();
                //if (!String.IsNullOrEmpty(dr["RK_DD", DataRowVersion.Original].ToString()))
                //{
                //    _rktype.RK_DD = Convert.ToDateTime(dr["RK_DD", DataRowVersion.Original]).Date;
                //}
                //_rktype.LST_OTD = DateTime.Today;
                //if (!String.IsNullOrEmpty(dr["VALID_DD", DataRowVersion.Original].ToString()))
                //{
                //    _rktype.VALID_DD = Convert.ToDateTime(Convert.ToDateTime(dr["VALID_DD", DataRowVersion.Original]).ToString(Comp.SQLDateFormat));
                //}
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["CSTN_SAL", DataRowVersion.Original].ToString()))
                {
                    _cst = Convert.ToDecimal(dr["CSTN_SAL", DataRowVersion.Original]);
                }
                if (dr["PS_ID", DataRowVersion.Original].ToString() == "SB")
                {
                    _qty *= -1;
                    _qty1 *= -1;
                    _cst *= -1;
                }
                else if (dr["PS_ID", DataRowVersion.Original].ToString() == "SD")
                {
                    _qty = 0;
                }
            }

            if (_osId == "LN")
            {
                _cst = 0;
            }

            if (!IsAdd && string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CHK_MAN", DataRowVersion.Original].ToString()))
                return;
            if (_posOsId == "T")
            {
                return;
            }
            WH _wh = new WH();
            if (!String.IsNullOrEmpty(_batNo))
            {
                System.Collections.Hashtable _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY_OUT] = _qty * -1;
                _ht[WH.QtyTypes.QTY1_OUT] = _qty1 * -1;
                _ht[WH.QtyTypes.CST] = _cst;
                _ht[WH.QtyTypes.LST_OTD] = _outDd;
                //if (!string.IsNullOrEmpty(osNo))//ck转入
                //    _ht[WH.QtyTypes.QTY_CK] = _qty;
                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
            }
            else
            {
                System.Collections.Hashtable _ht = new System.Collections.Hashtable();
                _ht[WH.QtyTypes.QTY] = _qty;
                _ht[WH.QtyTypes.QTY1] = _qty1;
                _ht[WH.QtyTypes.AMT_CST] = _cst;
                _ht[WH.QtyTypes.LST_OTD] = _outDd;
                //if (!string.IsNullOrEmpty(osNo))//ck转入
                //    _ht[WH.QtyTypes.QTY_CK] = _qty;
                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
            }
        }

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
                _dsQuery.Merge(ChangedDS.Tables["TF_PSS3"], false, MissingSchemaAction.AddWithKey);
                _dtBarCode = _dsQuery.Tables["TF_PSS3"];
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
                string _psID, _cusNo, _bilNo, _usr;
                DateTime _bilDd = DateTime.Today;
                if (_drHead.RowState == DataRowState.Deleted)
                {
                    _psID = _drHead["PS_ID", DataRowVersion.Original].ToString();
                    _cusNo = _drHead["CUS_NO", DataRowVersion.Original].ToString();
                    _bilNo = _drHead["PS_NO", DataRowVersion.Original].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["PS_DD", DataRowVersion.Original]);
                    _usr = _drHead["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _psID = _drHead["PS_ID"].ToString();
                    _cusNo = _drHead["CUS_NO"].ToString();
                    _bilNo = _drHead["PS_NO"].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["PS_DD"]);
                    _usr = _drHead["USR"].ToString();
                }
                //取得该单据做过的change
                DataTable _dtBarChange = null;
                if (_drHead.RowState != DataRowState.Added)
                {
                    _dtBarChange = _bar.GetBarChange(_psID, _bilNo);
                }
                //更新BAR_REC表
                System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
                System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
                System.Collections.ArrayList _alBoxNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alWhNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alStop = new System.Collections.ArrayList();
                System.Text.StringBuilder _sbChange = new System.Text.StringBuilder();
                for (int i = 0; i < _dtBarCode.Rows.Count; i++)
                {
                    if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["PS_ITM"].ToString()] != null)
                    {
                        string _barCode, _boxNo, _preItm, _whNo, _batNo;
                        string _oldWhNo = "";
                        string _oldBatNo = "";
                        string _barWh = "";
                        DataRow[] _dra;
                        if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                        {
                            _barCode = _dtBarCode.Rows[i]["BAR_CODE", DataRowVersion.Original].ToString();
                            _boxNo = _dtBarCode.Rows[i]["BOX_NO", DataRowVersion.Original].ToString();
                            _preItm = _dtBarCode.Rows[i]["PS_ITM", DataRowVersion.Original].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _preItm, "", DataViewRowState.CurrentRows | DataViewRowState.OriginalRows);
                            if (_dra[0].RowState == DataRowState.Deleted)
                            {
                                _whNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _batNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                            }
                            else
                            {
                                _whNo = _dra[0]["WH"].ToString();
                                _batNo = _dra[0]["BAT_NO"].ToString();
                            }
                        }
                        else
                        {
                            _barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
                            _boxNo = _dtBarCode.Rows[i]["BOX_NO"].ToString();
                            _preItm = _dtBarCode.Rows[i]["PS_ITM"].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _preItm);
                            _whNo = _dra[0]["WH"].ToString();
                            _batNo = _dra[0]["BAT_NO"].ToString();
                            if (_dra[0].RowState == DataRowState.Modified)
                            {
                                _oldWhNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _oldBatNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                            }
                        }
                        _barWh = _whNo;
                        DataRow _drBarRec = _dtBarRec.Rows.Find(new string[1] { _barCode });
                        string _stopID = "T";
                        if (_drBarRec != null)
                        {
                            bool _canUpdate = true;
                            Sunlike.Business.UserProperty _userProp = new UserProperty();
                            string _pgm = "DRP" + _psID;
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
                                if (_dtBarCode.Rows[i].RowState == DataRowState.Added)
                                {
                                    if (_drBarRec["WH"].ToString() != _whNo || _drBarRec["BAT_NO"].ToString() != _batNo || _drBarRec["PH_FLAG"].ToString() == "T")
                                    {
                                        _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo, _psID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
                                        _drBarRec["WH"] = _whNo;
                                        _drBarRec["BAT_NO"] = _batNo;
                                        _drBarRec["PH_FLAG"] = "F";
                                    }
                                }
                                else if (_dtBarCode.Rows[i].RowState == DataRowState.Unchanged || _dtBarCode.Rows[i].RowState == DataRowState.Modified)
                                {
                                    if (_oldWhNo != _drBarRec["WH"].ToString() || _oldBatNo != _drBarRec["BAT_NO"].ToString() || (_psID == "SA" && _drBarRec["STOP_ID"].ToString() != "T") || (_psID == "SB" && _drBarRec["STOP_ID"].ToString() == "T"))
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
                                            _sbChange.Append(_bar.DeleteBarChange(_barCode, _psID, _bilNo, true));
                                            if (_changeWh1 != _whNo || _changeBatNo1 != _batNo)
                                            {
                                                _sbChange.Append(_bar.InsertBarChange(_barCode, _changeWh1, _whNo, _psID, _bilNo, _usr, _changeBatNo1, _batNo, _changePhFlag1, "F", true));
                                            }
                                        }
                                        else if (_drBarRec["WH"].ToString() != _whNo || _drBarRec["BAT_NO"].ToString() != _batNo || _drBarRec["PH_FLAG"].ToString() == "T")
                                        {
                                            _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo, _psID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
                                        }
                                        if (_drBarRec["WH"].ToString() != _whNo)
                                        {
                                            _drBarRec["WH"] = _whNo;
                                        }
                                        if (_drBarRec["BAT_NO"].ToString() != _batNo)
                                        {
                                            _drBarRec["BAT_NO"] = _batNo;
                                        }
                                        if (_drBarRec["PH_FLAG"].ToString() != "F")
                                        {
                                            _drBarRec["PH_FLAG"] = "F";
                                        }
                                    }
                                }
                                else
                                {
                                    //BAR_REC.WH必须和单据库位一致，且BAR_REC.STOP_ID='F'，否则不允许删除
                                    if (_whNo == _drBarRec["WH"].ToString() && _batNo == _drBarRec["BAT_NO"].ToString() && (_psID == "SA" && _drBarRec["STOP_ID"].ToString() == "T") || (_psID == "SB" && _drBarRec["STOP_ID"].ToString() != "T"))
                                    {
                                        //如果有做过bar_change，则库位回到做bar_change之前的地方，且把bar_change的记录删掉
                                        DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
                                        if (_draBarChange.Length > 0)
                                        {
                                            _drBarRec["WH"] = _draBarChange[0]["WH1"].ToString();
                                            _drBarRec["BAT_NO"] = _draBarChange[0]["BAT_NO1"].ToString();
                                            //到货确认标记
                                            _drBarRec["PH_FLAG"] = _draBarChange[0]["PH_FLAG1"];
                                            _barWh = _draBarChange[0]["WH1"].ToString();
                                            _sbChange.Append(_bar.DeleteBarChange(_barCode, _psID, _bilNo, true));
                                        }
                                    }
                                    else
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
                                    }
                                }
                                _drBarRec["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                                if ((_psID == "SB" && _dtBarCode.Rows[i].RowState != DataRowState.Deleted)
                                    || (_psID == "SA" && _dtBarCode.Rows[i].RowState == DataRowState.Deleted))
                                {
                                    _stopID = "F";
                                }
                                _drBarRec["STOP_ID"] = _stopID;
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
                                        _alWhNo.Add(_barWh);
                                        _alStop.Add(_stopID);
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
                            if (_psID == "SB")
                            {
                                _stopID = "F";
                            }
                            _dr["STOP_ID"] = _stopID;
                            _dr["PRD_NO"] = _barCode.Substring(BarCode.BarRole.SPrdt, BarCode.BarRole.EPrdt - BarCode.BarRole.SPrdt + 1).Replace(BarCode.BarRole.TrimChar, "");
                            if (!(BarCode.BarRole.BPMark == BarCode.BarRole.EPMark && BarCode.BarRole.EPMark == 0))
                            {
                                _dr["PRD_MARK"] = _barCode.Substring(BarCode.BarRole.BPMark, BarCode.BarRole.EPMark - BarCode.BarRole.BPMark + 1);
                            }
                            _dr["BAT_NO"] = _batNo;
                            _dtBarRec.Rows.Add(_dr);

                            //序列号不存在，写BAR_CHANGE
                            _sbChange.Append(_bar.InsertBarChange(_barCode, "", _whNo, _psID, _bilNo, _usr, "", _batNo, "", "", true));
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
                    DataTable _dtError = _bar.UpdateRec(SunlikeDataSet.ConvertTo(_dtBarRec.DataSet));
                    if (_dtError.Rows.Count > 0)
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.BOXERROR");//更新序列号记录失败！
                    }
                    //修改BAR_BOX
                    _bar.UpdateBarBox(_alBoxNo, _alWhNo, _alStop);
                    //拆箱
                    _bar.DeleteBox(_aryBoxNo, _usr, _psID, _bilNo);
                }
                else
                {
                    throw new SunlikeException(_sbError.ToString());
                }
            }
        }

        private void UpdateQtySb(DataRow dataRow, bool isAdd)
        {
            string _psId, _osId, _osNo;
            int _preItm = 0;
            decimal _qty = 0;
            int _unit = 1;
            string _prdNo = "";
            if (isAdd)
            {
                _psId = dataRow["PS_ID"].ToString();
                _osId = dataRow["OS_ID"].ToString();
            }
            else
            {
                _psId = dataRow["PS_ID", DataRowVersion.Original].ToString();
                _osId = dataRow["OS_ID", DataRowVersion.Original].ToString();
            }
            if (_psId == "SA" && _osId == "SB")
            {
                if (isAdd)
                {
                    _psId = dataRow["PS_ID"].ToString();
                    _osId = dataRow["OS_ID"].ToString();
                    _osNo = dataRow["OS_NO"].ToString();
                    _preItm = Convert.ToInt32(dataRow["EST_ITM"]);
                    _prdNo = dataRow["PRD_NO"].ToString();
                    _qty = Convert.ToDecimal(dataRow["QTY"]);
                    if (!String.IsNullOrEmpty(dataRow["UNIT"].ToString()))
                    {
                        _unit = Convert.ToInt32(dataRow["UNIT"]);
                    }
                }
                else
                {
                    _psId = dataRow["PS_ID", DataRowVersion.Original].ToString();
                    _osId = dataRow["OS_ID", DataRowVersion.Original].ToString();
                    _osNo = dataRow["OS_NO", DataRowVersion.Original].ToString();
                    _preItm = Convert.ToInt32(dataRow["EST_ITM", DataRowVersion.Original]);
                    _prdNo = dataRow["PRD_NO", DataRowVersion.Original].ToString();
                    _qty = Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]);
                    if (!String.IsNullOrEmpty(dataRow["UNIT", DataRowVersion.Original].ToString()))
                    {
                        _unit = Convert.ToInt32(dataRow["UNIT", DataRowVersion.Original]);
                    }
                    _qty *= -1;
                }
                Hashtable _ht = new Hashtable();
                _ht["TableName"] = "TF_PSS";
                _ht["IdName"] = "PS_ID";
                _ht["NoName"] = "PS_NO";
                _ht["ItmName"] = "PRE_ITM";
                _ht["OsID"] = _osId;
                _ht["OsNO"] = _osNo;
                _ht["KeyItm"] = _preItm;
                _qty = INVCommon.GetRtnQty(_prdNo, _qty, _unit, _ht);
                DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
                string _errorID = _sa.UpdateQtySb(_osId, _osNo, _preItm, _qty);
                if (_errorID == "1")
                {
                    dataRow.SetColumnError("QTY", "RCID=INV.HINT.QTY_RTN_TANTO");
                    throw new SunlikeException("RCID=INV.HINT.QTY_RTN_TANTO");
                }
                else if (_errorID == "2")
                {
                    dataRow.SetColumnError("QTY", "RCID=INV.HINT.CANTUD_QTY_RTN");
                    throw new SunlikeException("RCID=INV.HINT.CANTUD_QTY_RTN");
                }
            }
        }

        private void UpdateQtyRtn(DataRow dr, bool IsAdd)
        {
            if (CaseInsensitiveComparer.Default.Compare("SD", GetStrDrValue(dr, "PS_ID")) != 0)
            {
                string _psID, _osID, _osNo;
                int _preItm = 0;
                decimal _qty = 0;
                decimal _qty1 = 0;
                int _unit = 1;
                string _prdNo = "";
                if (IsAdd)
                {
                    _psID = dr["PS_ID"].ToString();
                    _osID = dr["OS_ID"].ToString();
                    _osNo = dr["OS_NO"].ToString();
                    _prdNo = dr["PRD_NO"].ToString();
                    if (_psID == "SB" && !String.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["OTH_ITM"]);
                    }
                    else if (_psID == "SA" && !String.IsNullOrEmpty(dr["EST_ITM"].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["EST_ITM"]);
                    }
                    if (!String.IsNullOrEmpty(_osNo))
                    {
                        _qty = Convert2Decimal(dr["QTY"]);
                        _qty1 = Convert2Decimal(dr["QTY1"]);
                    }
                    if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    {
                        _unit = Convert.ToInt32(dr["UNIT"]);
                    }
                }
                else
                {
                    _psID = dr["PS_ID", DataRowVersion.Original].ToString();
                    _osID = dr["OS_ID", DataRowVersion.Original].ToString();
                    _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                    _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                    if (_psID == "SB" && !String.IsNullOrEmpty(dr["OTH_ITM", DataRowVersion.Original].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original]);
                    }
                    else if (_psID == "SA" && !String.IsNullOrEmpty(dr["EST_ITM", DataRowVersion.Original].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["EST_ITM", DataRowVersion.Original]);
                    }
                    if (!String.IsNullOrEmpty(_osNo))
                    {
                        _qty = Convert2Decimal(dr["QTY", DataRowVersion.Original]);
                        _qty1 = Convert2Decimal(dr["QTY1", DataRowVersion.Original]);
                    }
                    if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    {
                        _unit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
                    }
                    _qty *= -1;
                    _qty1 *= -1;
                }
                if (string.Compare("PB", _osID) == 0)
                    return;
                if (!String.IsNullOrEmpty(_osNo) && _preItm > 0)
                {
                    if (_psID == "SB")
                    {
                        Hashtable _ht = new Hashtable();
                        _ht["TableName"] = "TF_PSS";
                        _ht["IdName"] = "PS_ID";
                        _ht["NoName"] = "PS_NO";
                        _ht["ItmName"] = "PRE_ITM";
                        _ht["OsID"] = _osID;
                        _ht["OsNO"] = _osNo;
                        _ht["KeyItm"] = _preItm;
                        _qty = INVCommon.GetRtnQty(_prdNo, _qty, _unit, _ht);
                        _qty1 = INVCommon.GetRtnQty(_prdNo, _qty1, _unit, _ht);
                        DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
                        string _errorID = _sa.UpdateQtyRtn(_osID, _osNo, _preItm, _qty);
                        if (_errorID == "1")
                        {
                            dr.SetColumnError("QTY", "RCID=INV.HINT.QTY_RTN_TANTO");
                            throw new SunlikeException("RCID=INV.HINT.QTY_RTN_TANTO");
                        }
                        else if (_errorID == "2")
                        {
                            dr.SetColumnError("QTY", "RCID=INV.HINT.CANTUD_QTY_RTN");
                            throw new SunlikeException("RCID=INV.HINT.CANTUD_QTY_RTN");
                        }
                    }
                    else if (_psID == "SA" && _osID != "SB")
                    {
                        this._alOsNo.Add(_osNo);
                        this._alOsItm.Add(_preItm);
                        this._alPrdNo.Add(_prdNo);
                        this._alUnit.Add(_unit);
                        this._alQty.Add(_qty);
                        this._alQty1.Add(_qty1);
                    }
                }
            }
        }
        /// <summary>
        /// 修改计算营销费用状态
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="PreItm">追踪退回数量项次</param>
        /// <param name="flag">营销费用审核状态</param>
        public void UpdateMeRtn(string PsID, string PsNo, int PreItm, bool flag)
        {
            DbDRPSA _DbDRPSA = new DbDRPSA(Comp.Conn_DB);
            _DbDRPSA.UpdateMeRtn(PsID, PsNo, PreItm, flag);
        }
        private void UpdateSarp(DataRow dr)
        {
            DataRow _dr = dr.Table.DataSet.Tables["MF_PSS"].Rows[0];
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_PSS"];
            int _year = System.DateTime.Now.Year;
            int _month = System.DateTime.Now.Month;
            string _psID = "";
            string _cusNo = "";
            decimal _up, _qty;
            decimal _amtn = 0;
            Arp _arp = new Arp();
            Cust _cust = new Cust();

            //如果是删除或者是立帐方式由“单张立帐”改成“不立帐”
            //又或者立帐的情况下改变厂商或立帐方式，要先删除原来的帐款
            if (_dr.RowState != DataRowState.Added && _dr["ZHANG_ID", DataRowVersion.Original].ToString() == "1")
            {
                _year = Convert.ToDateTime(_dr["PS_DD", DataRowVersion.Original]).Year;
                _month = Convert.ToDateTime(_dr["PS_DD", DataRowVersion.Original]).Month;
                _psID = _dr["PS_ID", DataRowVersion.Original].ToString();
                _cusNo = _dr["CUS_NO", DataRowVersion.Original].ToString();
                #region 更新AMTN_INV
                //是分销商更新AMTN_INV
                if (_cust.IsDrp_id(_cusNo))
                {
                    if (_updateAmtnInvDelete)
                    {
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
                            if (_dr["TAX_ID", DataRowVersion.Original].ToString() != "1" && !String.IsNullOrEmpty(_dtBody.Rows[i]["TAX", DataRowVersion.Original].ToString()))
                            {
                                _amtn += Convert.ToDecimal(_dtBody.Rows[i]["TAX", DataRowVersion.Original]);
                            }
                        }
                        if (_psID == "SB" || _psID == "SD")
                        {
                            _amtn *= -1;
                        }
                        _arp.UpdateSarp("1", _year, _cusNo, _month, "", "AMTN_INV", _amtn);
                    }
                }
                #endregion
            }
            if (_dr.RowState != DataRowState.Deleted && _dr["ZHANG_ID"].ToString() == "1")
            {
                _up = 0;
                _qty = 0;
                _amtn = 0;
                _year = Convert.ToDateTime(_dr["PS_DD"]).Year;
                _month = Convert.ToDateTime(_dr["PS_DD"]).Month;
                _psID = _dr["PS_ID"].ToString();
                _cusNo = _dr["CUS_NO"].ToString();
                #region 更新AMTN_INV
                if (_cust.IsDrp_id(_cusNo))
                {
                    if (_updateAmtnInvAdd)
                    {
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
                                if (_dr["TAX_ID", DataRowVersion.Original].ToString() != "1" && !String.IsNullOrEmpty(_dtBody.Rows[i]["TAX", DataRowVersion.Original].ToString()))
                                {
                                    _amtn -= Convert.ToDecimal(_dtBody.Rows[i]["TAX", DataRowVersion.Original]);
                                }
                                _amtn -= _up * _qty;
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
                                if (_dr["TAX_ID"].ToString() != "1" && !String.IsNullOrEmpty(_dtBody.Rows[i]["TAX"].ToString()))
                                {
                                    _amtn += Convert.ToDecimal(_dtBody.Rows[i]["TAX"]);
                                }
                                _amtn += _up * _qty;
                            }
                        }
                        if (_psID != "SB" && _psID != "SD")
                        {
                            _amtn *= -1;
                        }
                        _arp.UpdateSarp("1", _year, _cusNo, _month, "", "AMTN_INV", _amtn);
                    }
                }
                #endregion
            }
        }
        private void UpdateSarp(DataSet ds)
        {
            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_PSS") && ds.Tables["MF_PSS"].Rows.Count > 0 && ds.Tables["MF_PSS"].Rows[0].RowState == System.Data.DataRowState.Deleted)
            {
                this.UpdateSarp(ds.Tables["MF_PSS"].Rows[0]);
            }
        }
        private void UpdateCustMtn(DataRow dataRow, bool isAdd)
        {
            string _sbchk = "F", _cusNo = "", _psId = "";
            decimal _amtn = 0;
            if (isAdd || dataRow.Table.DataSet.Tables["MF_PSS"].Rows[0].RowState == DataRowState.Unchanged)
            {
                _sbchk = dataRow.Table.DataSet.Tables["MF_PSS"].Rows[0]["SB_CHK"].ToString();
                _cusNo = dataRow.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_NO"].ToString();
                _psId = dataRow.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString();
            }
            else
            {
                _sbchk = dataRow.Table.DataSet.Tables["MF_PSS"].Rows[0]["SB_CHK", DataRowVersion.Original].ToString();
                _cusNo = dataRow.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_NO", DataRowVersion.Original].ToString();
                _psId = dataRow.Table.DataSet.Tables["MF_PSS"].Rows[0]["PS_ID", DataRowVersion.Original].ToString();
            }
            if (_sbchk == "T")
            {
                if (isAdd || dataRow.RowState == DataRowState.Unchanged)
                {
                    if (!String.IsNullOrEmpty(dataRow["AMTN_NET"].ToString()))
                    {
                        _amtn = Convert.ToDecimal(dataRow["AMTN_NET"]);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(dataRow["AMTN_NET", DataRowVersion.Original].ToString()))
                    {
                        _amtn = Convert.ToDecimal(dataRow["AMTN_NET", DataRowVersion.Original]);
                    }
                }
                if (_amtn != 0)
                {
                    if (!isAdd)
                    {
                        _amtn *= -1;
                    }
                    Cust _cust = new Cust();
                    //if (_psId == "SA")
                    //{
                    //    _cust.UpdateCustMtn(_cusNo, _amtn, 0);
                    //}
                    //else
                    //{
                    //    _cust.UpdateCustMtn(_cusNo, 0, _amtn);
                    //}
                }
            }
        }
        /// <summary>
        /// 更新会员积分
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="isDelete">是否删除</param>
        private void UpdateJf(SunlikeDataSet ds, bool isDelete)
        {
            DataTable _dtMf = ds.Tables["MF_PSS"];
            DataTable _dtTf = ds.Tables["TF_PSS"];
            DataTable _dtJf = ds.Tables["MF_JF"];
            string _bilId, _bilNo, _cardNo, _cusNo, _dep, _usr;
            DateTime _bilDd, _endDd;
            if (_dtMf.Rows.Count > 0 && _dtJf.Rows.Count > 0)
            {
                if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
                {
                    _bilId = _dtMf.Rows[0]["PS_ID"].ToString();
                    _bilNo = _dtMf.Rows[0]["PS_NO"].ToString();
                    _bilDd = Convert.ToDateTime(_dtMf.Rows[0]["PS_DD"]);
                    _cardNo = _dtMf.Rows[0]["CARD_NO"].ToString();
                    _cusNo = _dtMf.Rows[0]["CUS_NO"].ToString();
                    _dep = _dtMf.Rows[0]["DEP"].ToString();
                    _usr = _dtMf.Rows[0]["USR"].ToString();
                }
                else
                {
                    _bilId = _dtMf.Rows[0]["PS_ID", DataRowVersion.Original].ToString();
                    _bilNo = _dtMf.Rows[0]["PS_NO", DataRowVersion.Original].ToString();
                    _bilDd = Convert.ToDateTime(_dtMf.Rows[0]["PS_DD", DataRowVersion.Original]);
                    _cardNo = _dtMf.Rows[0]["CARD_NO", DataRowVersion.Original].ToString();
                    _cusNo = _dtMf.Rows[0]["CUS_NO", DataRowVersion.Original].ToString();
                    _dep = _dtMf.Rows[0]["DEP", DataRowVersion.Original].ToString();
                    _usr = _dtMf.Rows[0]["USR", DataRowVersion.Original].ToString();
                }
                if (!String.IsNullOrEmpty(_dtJf.Rows[0]["END_DD"].ToString()))
                {
                    _endDd = Convert.ToDateTime(_dtJf.Rows[0]["END_DD"]);
                }
                else
                {
                    _endDd = DateTime.Today.AddDays(Convert.ToInt32(Comp.DRP_Prop["JF_DAYS"]));
                }
                JF _jf = new JF();
                if (_dtJf.Rows[0].RowState != DataRowState.Added)
                {
                    ds.Tables["MF_JF"].Clear();
                    ds.Tables["MF_JF"].AcceptChanges();
                    ds.Merge(_jf.GetData(_bilId, _bilNo), false, MissingSchemaAction.AddWithKey);
                    _dtJf = ds.Tables["MF_JF"];
                    if (_dtJf.Rows.Count == 0)
                    {
                        DataRow _drNew = _dtJf.NewRow();
                        _drNew["JF_NO"] = "JF";
                        _dtJf.Rows.Add(_drNew);
                    }
                }
                if (isDelete || String.IsNullOrEmpty(_cardNo))
                {
                    //删除原有数据
                    _dtJf.Rows[0].Delete();
                }
                else
                {
                    //不是删除单句,增加会员积分
                    decimal _score = 0;
                    foreach (DataRow dr in _dtTf.Select())
                    {
                        if (dr["TAX"] != null && dr["TAX"].ToString() != "")
                            _score += _jf.GetJF(_bilDd, dr["PRD_NO"].ToString(), _cusNo, _cardNo, Convert.ToDecimal(dr["AMTN_NET"]) + Convert.ToDecimal(dr["TAX"]));
                        else
                            _score += _jf.GetJF(_bilDd, dr["PRD_NO"].ToString(), _cusNo, _cardNo, Convert.ToDecimal(dr["AMTN_NET"]));
                    }
                    if (_bilId != "SA")
                    {
                        _score *= -1;
                    }
                    //写积分单备注信息	
                    string _rem = "";
                    if (Comp.DataBaseLanguage == "zh-cn")
                    {
                        if (_bilId == "SA")
                            _rem = "交易成功";
                        else if (_bilId == "SB")
                            _rem = "商品退回扣减积分";
                        else if (_bilId == "SD")
                            _rem = "商品打折扣减积分";
                    }
                    else if (Comp.DataBaseLanguage == "zh-tw")
                    {
                        if (_bilId == "SA")
                            _rem = "交易成功";
                        else if (_bilId == "SB")
                            _rem = "商品退回扣減積分";
                        else if (_bilId == "SD")
                            _rem = "商品打折扣減積分";
                    }
                    else
                    {
                        if (_bilId == "SA")
                            _rem = "The trade succeeds";
                        else if (_bilId == "SB")
                            _rem = "The goods are returned and the accumulate marks are reduced";
                        else if (_bilId == "SD")
                            _rem = "The goods are given a discount and the accumulate marks are reduced";
                    }

                    DataRow _dr = _dtJf.Rows[0];
                    if (_score != 0)
                    {
                        //积分不为0
                        _dr["CUS_NO"] = _cusNo;
                        _dr["CARD_NO"] = _cardNo;
                        _dr["JF_DD"] = _bilDd.ToString(Comp.SQLDateFormat);
                        _dr["END_DD"] = _endDd;
                        _dr["JF_NET"] = _score;
                        _dr["USR"] = _usr;
                        _dr["DEP"] = _dep;
                        _dr["BIL_ID"] = _bilId;
                        _dr["BIL_NO"] = _bilNo;
                        _dr["REM"] = _rem;
                        _dr["SYS_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        _dr.Delete();
                    }
                }
                if (_dtJf.Rows.Count > 0)
                {
                    _jf.UpdateData("DRPJF", ds, true);
                }
            }
        }

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

        /// <summary>
        /// 更新收款单
        /// </summary>
        /// <param name="dr">销货单表头MF_PSS</param>
        private string UpdateMon(DataRow dr)
        {
            if (dr["ZHANG_ID"].ToString() == "4")
                return "";

            //有预收款
            Bills _bills = new Bills();
            //_bills.Receive(dr, dr.Table.DataSet.Tables["TF_MON1"], false); //by zb 2007-05-14
            //生成预收款单据
            MonStruct _mon = new MonStruct();
            _mon.RpId = "1";
            if (dr["PS_ID"].ToString() == "SB")
            {
                _mon.RpId = "2";
            }
            _mon.RpNo = dr["RP_NO"].ToString();
            if (!string.IsNullOrEmpty(dr["PS_DD"].ToString()))
                _mon.RpDd = Convert.ToDateTime(dr["PS_DD"].ToString());
            _mon.BilId = dr["PS_ID"].ToString();
            _mon.BilNo = dr["PS_NO"].ToString();
            _mon.Usr = dr["USR"].ToString();
            _mon.ChkMan = dr["CHK_MAN"].ToString();
            if (!string.IsNullOrEmpty(dr["CLS_DATE"].ToString()))
                _mon.ClsDate = Convert.ToDateTime(dr["CLS_DATE"].ToString());
            //_mon.VohId = dr["VOH_ID"].ToString();
            //_mon.VohNo = dr["VOH_NO"].ToString();
            _mon.MobId = dr["MOB_ID"].ToString();
            _mon.UsrNo = dr["SAL_NO"].ToString();
            _mon.Dep = dr["DEP"].ToString();
            _mon.IrpId = "F";
            _mon.CusNo = dr["CUS_NO"].ToString();
            _mon.CurId = dr["CUR_ID"].ToString();
            if (string.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                _mon.ExcRto = 1;
            else
                _mon.ExcRto = Convert.ToDecimal(dr["EXC_RTO"].ToString());
            _mon.BaccNo = dr["BACC_NO"].ToString();
            _mon.CaccNo = dr["CACC_NO"].ToString();
            #region 银行
            if (string.IsNullOrEmpty(dr["AMT_BB"].ToString()))
            {
                _mon.AmtBb = 0;
            }
            else
            {
                _mon.AmtBb = Convert.ToDecimal(dr["AMT_BB"].ToString());
            }
            if (string.IsNullOrEmpty(dr["AMTN_BB"].ToString()))
            {
                _mon.AmtnBb = 0;
            }
            else
            {
                _mon.AmtnBb = Convert.ToDecimal(dr["AMTN_BB"].ToString());
            }
            #endregion

            #region 现金
            if (string.IsNullOrEmpty(dr["AMT_BC"].ToString()))
            {
                _mon.AmtBc = 0;
            }
            else
            {
                _mon.AmtBc = Convert.ToDecimal(dr["AMT_BC"].ToString());
            }
            if (string.IsNullOrEmpty(dr["AMTN_BC"].ToString()))
            {
                _mon.AmtnBc = 0;
            }
            else
            {
                _mon.AmtnBc = Convert.ToDecimal(dr["AMTN_BC"].ToString());
            }
            #endregion

            #region 票据
            if (string.IsNullOrEmpty(dr["AMT_CHK"].ToString()))
            {
                _mon.AmtChk = 0;
            }
            else
            {
                _mon.AmtChk = Convert.ToDecimal(dr["AMT_CHK"].ToString());
            }
            if (string.IsNullOrEmpty(dr["AMTN_CHK"].ToString()))
            {
                _mon.AmtnChk = 0;
                //该属性为TRUE生成TF_MON4的表身
                _mon.AddMon4 = false;
            }
            else
            {
                _mon.AmtnChk = Convert.ToDecimal(dr["AMTN_CHK"].ToString());
                _mon.ChkNo = dr["CHK_NO"].ToString();
                _mon.BankNo = dr["BANK_NO"].ToString();
                _mon.BaccNoChk = dr["BACC_NO_CHK"].ToString();
                if (string.IsNullOrEmpty(dr["END_DD"].ToString()))
                {
                    _mon.EndDd = System.DateTime.Now;
                }
                else
                {
                    _mon.EndDd = Convert.ToDateTime(dr["END_DD"].ToString());
                }
                _mon.ChkKnd = dr["CHK_KND"].ToString();
                //该属性为TRUE生成TF_MON4的表身
                if (_mon.AmtnChk != 0)
                    _mon.AddMon4 = true;
            }
            #endregion

            #region 其他
            if (string.IsNullOrEmpty(dr["TAX_IRP"].ToString()) && string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
            {
                _mon.AmtOther = 0;
                _mon.AmtnOther = 0;
                //该属性为TRUE生成TF_MON3的表身
                _mon.AddMon3 = false;
            }
            else
            {
                decimal _amtTaxIrp = 0;
                decimal _amtnTaxIrp = 0;
                decimal _amtCbac = 0;
                decimal _amtnCbac = 0;
                bool _idxTax = false;
                Cust _cust = new Cust();
                SunlikeDataSet _dsCust = new SunlikeDataSet();
                _dsCust.Merge(_cust.GetData(_mon.CusNo));
                DataTable _custDt = _dsCust.Tables["CUST"];
                if (_custDt.Rows.Count > 0
                    && _custDt.Rows[0]["ID2_TAX"].ToString() == "T"
                    )
                {
                    _idxTax = true;
                }
                CompInfo _compInfo = Comp.GetCompInfo("");
                int _poiTax = _compInfo.DecimalDigitsInfo.System.POI_TAX;
                if (_idxTax && !string.IsNullOrEmpty(_mon.CurId))
                {
                    if (!string.IsNullOrEmpty(dr["TAX_IRP"].ToString()))
                    {
                        _amtTaxIrp = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()), _poiTax);
                        _amtnTaxIrp = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()) * _mon.ExcRto, _poiTax);
                    }
                    if (!string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
                    {
                        _amtCbac = Math.Round(Convert.ToDecimal(dr["AMTN_CBAC"].ToString()), _poiTax);
                        _amtnCbac = Math.Round(Convert.ToDecimal(dr["AMTN_CBAC"].ToString()) * _mon.ExcRto, _poiTax);
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["TAX_IRP"].ToString()))
                    {
                        _amtnTaxIrp = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()), _poiTax);
                        if (_mon.ExcRto != 1)
                        {
                            _amtTaxIrp = Math.Round((Convert.ToDecimal(dr["TAX_IRP"].ToString()) / _mon.ExcRto), _poiTax);
                        }
                        else
                            _amtTaxIrp = 0;
                    }
                    if (!string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
                    {
                        _amtnCbac = Math.Round(Convert.ToDecimal(dr["AMTN_CBAC"].ToString()), _poiTax);
                        if (_mon.ExcRto != 1)
                        {
                            _amtCbac = Math.Round((Convert.ToDecimal(dr["AMTN_CBAC"].ToString()) / _mon.ExcRto), _poiTax);
                        }
                        else
                            _amtCbac = 0;
                    }
                }
                //取销货单凭证模版
                string _accNoTaxIrp = "";
                string _accNoCbac = "";
                DrpVoh _voh = new DrpVoh();
                DataSet _dsVhId = _voh.GetVhId(dr["PS_ID"].ToString(), dr["VOH_ID"].ToString());
                if (_dsVhId.Tables.Contains("TF_VHID"))
                {
                    if (_dsVhId.Tables["TF_VHID"].Select("ITM='10' AND DC='C'").Length > 0)
                    {
                        //取客户储值会计科目
                        _accNoCbac = _dsVhId.Tables["TF_VHID"].Select("ITM='10' AND DC='C'")[0]["ACC_NO"].ToString();
                    }
                    if (_dsVhId.Tables["TF_VHID"].Select("ITM='9' AND DC='D'").Length > 0)
                    {
                        //取销收输入科目
                        _accNoTaxIrp = _dsVhId.Tables["TF_VHID"].Select("ITM='9' AND DC='D'")[0]["ACC_NO"].ToString();
                    }
                }
                DataTable _dtMon3 = new DataTable();
                _dtMon3.Columns.Add("ACC_NO", typeof(System.String));
                _dtMon3.Columns.Add("AMT", typeof(System.String));
                _dtMon3.Columns.Add("AMTN", typeof(System.String));
                DataRow _drMon3 = null;
                if (_amtnTaxIrp != 0)
                {
                    _drMon3 = _dtMon3.NewRow();
                    _drMon3["ACC_NO"] = _accNoTaxIrp;
                    _drMon3["AMT"] = _amtTaxIrp;
                    _drMon3["AMTN"] = _amtnTaxIrp;
                    _dtMon3.Rows.Add(_drMon3);
                }
                if (_amtnCbac != 0)
                {
                    _drMon3 = _dtMon3.NewRow();
                    _drMon3["ACC_NO"] = _accNoCbac;
                    _drMon3["AMT"] = _amtCbac;
                    _drMon3["AMTN"] = _amtnCbac;
                    _dtMon3.Rows.Add(_drMon3);
                }
                _mon.TfMon3 = _dtMon3;

                _mon.AmtOther = _amtTaxIrp;
                _mon.AmtnOther = _amtnTaxIrp;
                _mon.AmtOther += _amtCbac;
                _mon.AmtnOther += _amtnCbac;

                //该属性为TRUE生成TF_MON3的表身
                if (_mon.AmtnOther != 0)
                    _mon.AddMon3 = true;
            }
            #endregion

            #region 预收款
            if (string.IsNullOrEmpty(dr["AMT_IRP"].ToString()))
                _mon.AmtIrp = 0;
            else
                _mon.AmtIrp = Convert.ToDecimal(dr["AMT_IRP"].ToString());
            if (string.IsNullOrEmpty(dr["AMTN_IRP"].ToString()))
            {
                _mon.AmtnIrp = 0;
                _mon.AddMon1 = false;
            }
            else
            {
                _mon.AmtnIrp = Convert.ToDecimal(dr["AMTN_IRP"].ToString());
                if (_mon.AmtnIrp != 0)
                    _mon.AddMon1 = true;
                _mon.TfMon1 = dr.Table.DataSet.Tables["TF_MON1"];

            }
            #endregion

            _mon.Amtn = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
            _mon.AmtnCls = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;

            //判断收款金额是否大于表身金额
            if (dr.RowState != DataRowState.Deleted)
            {
                DataRow[] _drSel = dr.Table.DataSet.Tables["TF_PSS"].Select();
                decimal _amtnNetAndTax = 0;
                for (int i = 0; i < _drSel.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_drSel[i]["AMTN_NET"].ToString()))
                    {
                        _amtnNetAndTax += Convert.ToDecimal(_drSel[i]["AMTN_NET"]);
                    }
                    if (!string.IsNullOrEmpty(_drSel[i]["TAX"].ToString()))
                    {
                        _amtnNetAndTax += Convert.ToDecimal(_drSel[i]["TAX"]);
                    }
                }
                if (_mon.AmtnCls > _amtnNetAndTax)
                {
                    throw new SunlikeException("RCID=MON.HINT.AMTNCLSLARGE,PARAM=" + string.Format("{0:F2}", _amtnNetAndTax.ToString()));//金额之和不能超过{0}
                }
            }

            if (dr["PS_ID"].ToString() == "SB")
            {
                //_mon.AmtnArp = (_mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther) * -1;
                //_mon.AmtArp = (_mon.AmtBb + _mon.AmtBc + _mon.AmtChk + _mon.AmtOther) * -1;

                _mon.AddMon2 = true;
                _mon.AddTcMon = false;
            }
            else
            {
                _mon.AddMon2 = false;
                _mon.AddTcMon = string.IsNullOrEmpty(dr["CHK_MAN"].ToString()) ? false : true;
            }
            _mon.ArpNo = dr["ARP_NO"].ToString();
            string _rpNo = _bills.AddRcvPay(_mon);
            dr["RP_NO"] = _rpNo;
            return _rpNo;

        }
        /// <summary>
        /// 更新收款单号
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="rpNo"></param>
        private void UpdateRpNo(string psId, string psNo, string rpNo)
        {
            DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
            _sa.UpdateRpNo(psId, psNo, rpNo);
        }

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
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && (string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0 || string.Compare(dr["ZHANG_ID"].ToString(), "4") == 0))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("SA", _psId) == 0 || string.Compare("SB", _psId) == 0 || string.Compare("SD", _psId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.SA;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", "", _psId, dr["PS_NO"].ToString()), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _psId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && (string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0 || string.Compare(dr["ZHANG_ID"].ToString(), "4") == 0) && string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("SA", _psId) == 0 || string.Compare("SB", _psId) == 0 || string.Compare("SD", _psId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.SA;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", "", _psId, dr["PS_NO"].ToString()), true);
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
                if (string.Compare("SA", _psId) == 0 || string.Compare("SB", _psId) == 0 || string.Compare("SD", _psId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.SA;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && (string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0 || string.Compare(dr["ZHANG_ID"].ToString(), "4") == 0))
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

        #region 修改采购单的采购量
        /// <summary>
        /// 修改采购单的采购量
        /// </summary>
        /// <param name="dr">TF_PSS行信息</param>
        /// <param name="isAudit">是否走审核流程</param>
        /// <param name="isAdd">是否新增</param>
        /// <param name="flag">同时更新QTY_RTN,QTY_RNT_UNSH</param>
        private void UpdateQtyRtnForSB(DataRow dr, bool isAdd, bool flag)
        {
            string _osId = "";
            string _osNo = "";
            string _unit = "";
            string _chkMan = "";
            int _othItm = 0;
            decimal _qty = 0;
            //取表头信息
            if (dr.Table.DataSet != null && dr.Table.DataSet.Tables.Contains("MF_PSS") && dr.Table.DataSet.Tables["MF_PSS"].Rows.Count > 0)
            {
                if (dr.Table.DataSet.Tables["MF_PSS"].Rows[0].RowState == DataRowState.Deleted)
                {
                    _chkMan = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CHK_MAN", DataRowVersion.Original].ToString();
                }
                else
                {
                    _chkMan = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CHK_MAN"].ToString();
                }
            }
            if (isAdd)
            {
                _osId = dr["OS_ID"].ToString();
                _osNo = dr["OS_NO"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                {
                    _othItm = Convert.ToInt32(dr["OTH_ITM"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"].ToString());
                }
            }
            else
            {
                _osId = dr["OS_ID", DataRowVersion.Original].ToString();
                _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["OTH_ITM", DataRowVersion.Original].ToString()))
                {
                    _othItm = Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original].ToString());
                }
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                }
            }
            DRPPC _pc = new DRPPC();
            if (_othItm != 0)
            {
                if (string.IsNullOrEmpty(_chkMan))//走审核 
                {
                    _pc.UpdateQtyRtnUnsh(_osId, _osNo, _othItm, _unit, _qty);
                    if (flag)
                    {
                        _pc.UpdateQtyRtn(_osId, _osNo, _othItm, _unit, (-1) * _qty);
                    }
                }
                else//不走审核 
                {
                    _pc.UpdateQtyRtn(_osId, _osNo, _othItm, _unit, _qty);
                    if (flag)
                    {
                        _pc.UpdateQtyRtnUnsh(_osId, _osNo, _othItm, _unit, (-1) * _qty);
                    }
                }

            }
        }
        #endregion
        #endregion

        #region 结案
        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="bilId">单据别</param>
        /// <param name="bilNo">单号</param>
        /// <param name="clsName">结案字段</param>
        /// <param name="close">结案否</param>
        /// <returns></returns>
        public string DoCloseBill(string bilId, string bilNo, string clsName, bool close)
        {
            //			SunlikeDataSet _ds = this.GetData("", bilId, bilNo);
            //			DataTable _dtMf = _ds.Tables["MF_PSS"];
            //			if (_dtMf.Rows.Count > 0)
            //			{
            //				if (((_dtMf.Rows[0]["CK_CLS_ID"].ToString() == "T" && clsName == "CK_CLS_ID")
            //					|| (_dtMf.Rows[0]["LZ_CLS_ID"].ToString() == "T" && clsName == "LZ_CLS_ID")) == close)
            //				{
            //					if (close)
            //					{
            //						return "RCID=COMMON.HINT.CLOSEERROR,PARAM="+bilNo;//该单据[{0}]已结案,结案动作不能完成!
            //					}
            //					else
            //					{
            //						return "RCID=COMMON.HINT.CLOSEERROR1,PARAM="+bilNo;//该单据[{0}]未结案,未结案动作不能完成!
            //					}
            //				}
            //			}
            DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
            return _dbSa.CloseBill(bilId, bilNo, clsName, close);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="close"></param>
        /// <returns></returns>
        public string UndoCloseBill(string bil_id, string bil_no, bool close)
        {
            return this.UndoCloseBill(bil_id, bil_no, close);
        }
        #endregion

        #region 销货折让
        /// <summary>
        /// 取得已转销货折让的原销货单列表
        /// </summary>
        /// <param name="osNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetImportedSAList(string osNo)
        {
            DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
            return _dbSa.GetImportedSAList(osNo);
        }
        /// <summary>
        /// 取得销货单表身
        /// </summary>
        /// <param name="pPsNo">销货单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetSAList(string pPsNo)
        {
            DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
            if (!String.IsNullOrEmpty(pPsNo.Trim()))
            {
                return _dbSa.GetSAList(pPsNo.Remove(2, pPsNo.Length - 2), pPsNo);
            }
            else
            {
                return _dbSa.GetSAList(String.Empty, pPsNo);
            }
        }

        /// <summary>
        /// 取表身资料
        /// </summary>
        /// <param name="psNo"></param>
        /// <param name="Incorporate">是否合并</param>
        /// <returns></returns>
        public SunlikeDataSet GetBodydata4SD(string psNo, bool Incorporate)
        {
            DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbSa.GetBodydata4SD(psNo, Incorporate);
            if (!Incorporate)
            {
                _ds.Tables[0].Columns.Add("DIS_CNT", typeof(System.Decimal));
                _ds.Tables[0].Columns.Add("DIS_AMTN", typeof(System.Decimal));
                _ds.Tables[0].Columns.Add("DIS_AMT", typeof(System.Decimal));
                if (!_ds.Tables[0].Columns.Contains("FREE_ID"))
                {
                    _ds.Tables[0].Columns.Add("FREE_ID", typeof(System.String));
                }
            }
            return _ds;
        }

        /// <summary>
        /// 取得销货单资料
        /// </summary>
        /// <param name="dBdate">开始日期</param>
        /// <param name="dEdate">结束日期</param>
        /// <param name="cusNo">客户代号</param>
        /// <returns></returns>
        public SunlikeDataSet GetData4SD(DateTime dBdate, DateTime dEdate, string cusNo)
        {
            DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbSa.GetData4SD(dBdate, dEdate, cusNo);
            return _ds;
        }
        #endregion

        #region 开票
        /// <summary>
        /// 根据条件查询表头(开票)
        /// </summary>
        /// <param name="sqlWhere">条件</param>
        /// <returns></returns>
        public SunlikeDataSet GetData4TaxAa(string sqlWhere)
        {
            DbDRPSA _dbDrpSa = new DbDRPSA(Comp.Conn_DB);
            return _dbDrpSa.GetData4TaxAa(sqlWhere);
        }
        /// <summary>
        /// 更新MF_PSS发票号码
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="invNo"></param>
        /// <param name="kpId"></param>
        public void UpdateInvNo(string psId, string psNo, string invNo, string kpId)
        {
            DbDRPSA _dbDrpSa = new DbDRPSA(Comp.Conn_DB);
            _dbDrpSa.UpdateInvNo(psId, psNo, invNo, kpId);
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
        string Sunlike.Business.ICloseBill.UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "CK_CLS_ID" || cls_name == "LZ_CLS_ID")
            {
                _error = this.DoCloseBill(bil_id, bil_no, cls_name, false);
            }
            return _error;
        }

        string Sunlike.Business.ICloseBill.DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "CK_CLS_ID" || cls_name == "LZ_CLS_ID")
            {
                _error = this.DoCloseBill(bil_id, bil_no, cls_name, true);
            }
            return _error;
        }

        #endregion

        #region 修改销货紧急放行结案状态
        /// <summary>
        /// 修改销货紧急放行结案状态
        /// </summary>
        /// <param name="psId">单据别</param>
        /// <param name="psNo">单号</param>        
        /// <param name="hasFx">是否结案</param>
        /// <param name="zhangIdOld">原立账方式</param>
        /// <param name="zhangIdNew">新立账方式</param>
        /// <returns></returns>
        public bool UpdateHasFx(string psId, string psNo, bool hasFx, string zhangIdOld, string zhangIdNew)
        {
            //更新立账方式
            SunlikeDataSet _ds = this.GetData("", null, psId, psNo, false, true);
            if (_ds.Tables["MF_PSS"].Rows.Count > 0)
            {
                if (string.Compare(_ds.Tables["MF_PSS"].Rows[0]["ZHANG_ID"].ToString(), zhangIdOld) == 0)
                {
                    _ds.Tables["MF_PSS"].Rows[0]["ZHANG_ID"] = zhangIdNew;
                }
                else
                {
                    throw new SunlikeException(/*立账方式已经更改，放行结案不能成功*/"RCID=MTN.HINT.ZHANGIDCHG");
                }
            }
            else
            {
                throw new SunlikeException(/*[{0}]单号不存在,请检查!*/"RCID=MTN.HINT.BIL_NO_NULL,PARAM=" + psNo);
            }
            //DataRow[] _drChkRtn = _ds.Tables["TF_PSS"].Select("ISNULL(CHK_RTN,'F')='T'");
            //if (_drChkRtn.Length > 0)
            //{
            //    if (!_ds.ExtendedProperties.ContainsKey("MTNFXCLOSE"))
            //    {
            //        _ds.ExtendedProperties.Add("MTNFXCLOSE", "T");
            //    }
            //}
            if (!_ds.ExtendedProperties.ContainsKey("IS_MTNFXCLOSE"))
            {
                _ds.ExtendedProperties.Add("IS_MTNFXCLOSE", "T");
            }
            DataTable _dtError = this.UpdateData("DRPSA", _ds);
            string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(_ds);
            if (!string.IsNullOrEmpty(_errorMsg))
                throw new SunlikeException(_errorMsg);

            //回写紧急放行结案标记
            DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
            return _sa.UpdateHasFx(psId, psNo, hasFx);
        }
        #endregion

        #region 判断是否需要强制退回
        /// <summary>
        /// 判断是否需要强制退回
        /// 返回表结构
        /// PS_ID,PS_NO,PRE_ITM,PRD_NO,PRD_NAME,UNIT,QTY
        /// </summary>
        /// <param name="psId">销货单据别</param>
        /// <param name="psNo">销货单据号</param>
        /// <returns></returns>
        public DataTable GetChkRtnInfo(string psId, string psNo)
        {
            DataTable _result = new DataTable();
            _result.Columns.Add("PS_ID", typeof(System.String));
            _result.Columns.Add("PS_NO", typeof(System.String));
            _result.Columns.Add("PRE_ITM", typeof(System.Int32));
            _result.Columns.Add("PRD_NO", typeof(System.String));
            _result.Columns.Add("PRD_NAME", typeof(System.String));
            _result.Columns.Add("UNIT", typeof(System.String));
            _result.Columns.Add("QTY", typeof(System.Decimal));
            SunlikeDataSet _dsSa = this.GetData("", "", psId, psNo);
            DataRow[] _drSel = _dsSa.Tables["TF_PSS"].Select("ISNULL(CHK_RTN,'F')='T' AND ISNULL(QTY,0) > ISNULL(QTY_RTN,0) ");
            if (_drSel.Length > 0)
            {
                for (int i = 0; i < _drSel.Length; i++)
                {
                    DataRow _dr = _result.NewRow();
                    _dr["PS_ID"] = _drSel[i]["PS_ID"];
                    _dr["PS_NO"] = _drSel[i]["PS_NO"];
                    _dr["PRE_ITM"] = _drSel[i]["PRE_ITM"];
                    _dr["PRD_NO"] = _drSel[i]["PRD_NO"];
                    _dr["PRD_NAME"] = _drSel[i]["PRD_NAME"];
                    _dr["UNIT"] = _drSel[i]["UNIT"];
                    decimal _qtyLeft = 0;
                    if (!string.IsNullOrEmpty(_drSel[i]["QTY"].ToString()))
                        _qtyLeft = Convert.ToDecimal(_drSel[i]["QTY"].ToString());
                    if (!string.IsNullOrEmpty(_drSel[i]["QTY_RTN"].ToString()))
                        _qtyLeft -= Convert.ToDecimal(_drSel[i]["QTY_RTN"].ToString());
                    _dr["QTY"] = _qtyLeft;
                    _result.Rows.Add(_dr);
                }
            }
            return _result;
        }
        #endregion

        #region 强制退货删除检测
        /// <summary>
        /// 强制退货删除检测
        /// </summary>
        /// <param name="psId">单据别</param>
        /// <param name="psNo">单号</param>
        /// <param name="preItm">追踪项次</param>
        /// <returns></returns>
        private bool chkRtnDel(string psId, string psNo, string preItm)
        {
            if (string.Compare("SA", psId) != 0)
                return true;
            if (string.IsNullOrEmpty(psNo))
                return true;
            if (string.IsNullOrEmpty(preItm))
                return true;
            bool _result = true;

            SunlikeDataSet _dsSa = GetData("", "", psId, psNo, false, false);
            if (_dsSa != null && _dsSa.Tables.Count > 0 && _dsSa.Tables["MF_PSS"].Rows.Count > 0)
            {
                DataRow[] _drSel = _dsSa.Tables["TF_PSS"].Select("PS_ID='" + psId + "' AND PS_NO='" + psNo + "' AND PRE_ITM=" + preItm + " AND ISNULL(CHK_RTN,'F') = 'T'");
                //销货单已放行结案且需强制退货的则不允许删除
                if (_drSel != null && _drSel.Length > 0 && string.Compare("T", _dsSa.Tables["MF_PSS"].Rows[0]["HAS_FX"].ToString()) == 0)
                {
                    _result = false;
                }
            }
            return _result;
        }
        #endregion

        #region 更新保修卡维修记录
        /// <summary>
        /// 更新保修卡维修记录
        /// </summary>
        /// <param name="dr">dr行</param>
        /// <param name="isDel">+-标识是否删除</param>
        /// <param name="isByAuditPass">是被审核通过</param>
        private void UpdateWCH(DataRow dr, bool isDel, bool isByAuditPass)
        {
            if (!_isRunAuditing || isByAuditPass) //被审核通过或者不需要跑审核流程
            {
                string wcNo = "";
                string bilNo = "";
                if (dr.Table.Columns.Contains("WC_NO"))
                {
                    if (isDel)
                    {
                        wcNo = Convert.ToString(dr["WC_NO", DataRowVersion.Original]);
                        bilNo = Convert.ToString(dr["PS_NO", DataRowVersion.Original]);
                    }
                    else
                    {
                        wcNo = Convert.ToString(dr["WC_NO"]);
                        bilNo = Convert.ToString(dr["PS_NO"]);
                    }
                }
                if (!string.IsNullOrEmpty(wcNo))
                {
                    #region  更新记录表TF_WC_H

                    WC _wc = new WC();
                    SunlikeDataSet _ds = _wc.GetDataWcH(wcNo);
                    using (DataTable _dt = _ds.Tables["TF_WC_H"])
                    {
                        DataRow[] _drs = _dt.Select("BIL_NO='" + bilNo + "'");
                        if (_drs.Length == 0)
                        {
                            if (!isDel) //新增
                            {
                                DataRow _dr = _dt.NewRow();
                                _dr["WC_NO"] = wcNo;
                                _dr["BIL_ID"] = dr["PS_ID"];
                                _dr["BIL_DD"] = dr["PS_DD"];
                                _dr["BIL_NO"] = bilNo;
                                //_dr["REM"] = dr["REM"];
                                _dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                _dt.Rows.Add(_dr);
                            }
                        }
                        else
                        {
                            DataRow _dr = _drs[0];
                            if (isDel)//删除
                                _dr.Delete();
                            else//修改
                            {
                                _dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                //_dr["REM"] = dr["REM"];
                            }
                        }
                        _wc.UpdateDataWcH(_ds);
                    }

                    #endregion
                }
            }
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
            DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
            _sa.UpdateVohNo(psId, psNo, vohNo);
        }
        #endregion

        #region 是否已转折让单
        /// <summary>
        /// 是否已转折让单
        /// </summary>
        /// <param name="osNo"></param>
        /// <returns></returns>
        public bool CheckSD(string osNo)
        {
            DbDRPSA _sa = new DbDRPSA(Comp.Conn_DB);
            return _sa.CheckSD(osNo);
        }
        #endregion

        #region 生成帐户变动单
        /// <summary>
        /// 生成帐户变动单
        /// </summary>
        /// <param name="dr">MF_PSS行信息</param>
        /// <param name="isAdd"></param>
        private void UpdateMonCbac(DataRow dr, bool isAdd)
        {
            string _bilId = "";
            string _bilNo = "";
            string _osId = "";
            string _osNo = "";
            string _cusNo = "";
            string _dep = "";
            string _salNo = "";
            string _rem = "";
            string _usr = "";
            string _chkMan = "";
            String _bilType = "";
            decimal _amtn = 0;
            decimal _flag = 1;
            bool _hasAmtnCbacSO = false;
            DateTime _bilDd = System.DateTime.Now;
            DateTime _clsDd = System.DateTime.Now;
            if (isAdd)
            {
                _bilId = dr["PS_ID"].ToString();
                _bilNo = dr["PS_NO"].ToString();
                if (!string.IsNullOrEmpty(dr["PS_DD"].ToString()))
                {
                    _bilDd = Convert.ToDateTime(dr["PS_DD"]);
                }
                _osId = dr["OS_ID"].ToString();
                _osNo = dr["OS_NO"].ToString();
                _cusNo = dr["CUS_NO"].ToString();
                _dep = dr["DEP"].ToString();
                _salNo = dr["SAL_NO"].ToString();
                _bilType = dr["BIL_TYPE"].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
                {
                    _amtn = Convert.ToDecimal(dr["AMTN_CBAC"]);
                }
                _rem = dr["REM"].ToString();
                _usr = dr["USR"].ToString();
                _chkMan = dr["CHK_MAN"].ToString();
                if (!string.IsNullOrEmpty(dr["CLS_DATE"].ToString()))
                {
                    _clsDd = Convert.ToDateTime(dr["CLS_DATE"]);
                }

            }
            else
            {
                _bilId = dr["PS_ID", DataRowVersion.Original].ToString();
                _bilNo = dr["PS_NO", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["PS_DD", DataRowVersion.Original].ToString()))
                {
                    _bilDd = Convert.ToDateTime(dr["PS_DD", DataRowVersion.Original]);
                }
                _osId = dr["OS_ID", DataRowVersion.Original].ToString();
                _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                _cusNo = dr["CUS_NO", DataRowVersion.Original].ToString();
                _dep = dr["DEP", DataRowVersion.Original].ToString();
                _salNo = dr["SAL_NO", DataRowVersion.Original].ToString();
                _bilType = dr["BIL_TYPE", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN_CBAC", DataRowVersion.Original].ToString()))
                {
                    _amtn = Convert.ToDecimal(dr["AMTN_CBAC", DataRowVersion.Original]);
                }
                _rem = dr["REM", DataRowVersion.Original].ToString();
                _usr = dr["USR", DataRowVersion.Original].ToString();
                _chkMan = dr["CHK_MAN", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["CLS_DATE", DataRowVersion.Original].ToString()))
                {
                    _clsDd = Convert.ToDateTime(dr["CLS_DATE", DataRowVersion.Original]);
                }
            }
            if (_amtn == 0)
                return;
            //if (string.Compare("SA", _bilId) != 0)
            //    return;
            #region 回写受订单客户储值结案标记
            if (string.Compare("SO", _osId) == 0)
            {
                bool _cbacCls = true;
                DrpSO _so = new DrpSO();
                SunlikeDataSet _dsSo = _so.GetData("", _osId, _osNo, false, false);
                if (_dsSo.Tables["MF_POS"].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(_dsSo.Tables["MF_POS"].Rows[0]["AMTN_CBAC"].ToString())
                        && Convert.ToDecimal(_dsSo.Tables["MF_POS"].Rows[0]["AMTN_CBAC"]) != 0)
                    {
                        _hasAmtnCbacSO = true;
                        if (string.Compare("T", _dsSo.Tables["MF_POS"].Rows[0]["CBAC_CLS"].ToString()) != 0)
                        {
                            if (isAdd)
                            {
                                _cbacCls = true;
                            }
                            else
                            {
                                _cbacCls = false;
                            }
                        }
                        else
                        {
                            if (isAdd)
                            {
                                _cbacCls = true;
                            }
                            else
                            {
                                _cbacCls = false;
                            }
                        }
                        _so.UpdateCbacCls(_osId, _osNo, _cbacCls);
                    }
                }
            }
            #endregion

            #region 生成客户储值单
            CBac _cbac = new CBac();
            SunlikeDataSet _dsCbac = _cbac.GetDataBilNo(_bilId, _bilNo);
            DataTable _dtHead = _dsCbac.Tables["MF_CBAC"];
            DataRow _drNew = null;
            if (isAdd)
            {
                if (_dtHead.Rows.Count > 0)
                    _drNew = _dtHead.Rows[0];
                else
                {
                    _drNew = _dtHead.NewRow();
                    _drNew["BC_ID"] = "BC";
                    _drNew["BC_NO"] = "";
                }
                _drNew["BC_DD"] = _bilDd;
                _drNew["BIL_ID"] = _bilId;
                _drNew["BIL_NO"] = _bilNo;
                _drNew["CUS_NO"] = _cusNo;
                _drNew["DEP"] = _dep;
                _drNew["SAL_NO"] = _salNo;
                _drNew["SAVE_ID"] = "T";
                if (string.Compare("SB", _bilId) == 0)
                {
                    _drNew["ADD_ID"] = "+";
                }
                else
                {
                    _drNew["ADD_ID"] = "-";
                }
                _drNew["BACC_SW"] = "F";
                _drNew["AMTN"] = _flag * _amtn;
                _drNew["CHK_MAN"] = _chkMan;
                _drNew["USR"] = _usr;
                if (string.Compare("SB", _bilId) == 0)
                {
                    if (Comp.DataBaseLanguage.ToLower() == "zh-tw")
                    {
                        _drNew["REM"] = "退貨返回金額";
                    }
                    else if (Comp.DataBaseLanguage.ToLower() == "en-us")
                    {
                        _drNew["REM"] = "Amount of Returned-Goods";
                    }
                    else
                    {
                        _drNew["REM"] = "退货返回金额";
                    }
                }
                else
                {
                    _drNew["REM"] = _rem;
                }
                _drNew["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                _drNew["CLS_DATE"] = _clsDd;
                if (string.Compare("SB", _bilId) == 0)
                {
                    _drNew["PAY_TYPE"] = "1";
                }
                else
                {
                    _drNew["PAY_TYPE"] = "3";
                    if (Comp.DRP_Prop.ContainsKey("LEAGUE_BIL_TYPE")
                        && !string.IsNullOrEmpty(Comp.DRP_Prop["LEAGUE_BIL_TYPE"].ToString())
                        && string.Compare(_bilType, Comp.DRP_Prop["LEAGUE_BIL_TYPE"].ToString()) == 0)
                    {
                        _drNew["PAY_TYPE"] = "4";
                    }
                }

                if (_dtHead.Rows.Count == 0)
                {
                    _dtHead.Rows.Add(_drNew);
                }
            }
            else
            {
                if (_dtHead.Rows.Count > 0)
                    _dtHead.Rows[0].Delete();
            }
            if (!string.IsNullOrEmpty(_chkMan))
            {
                _dsCbac.ExtendedProperties["SAVE_ID"] = "T";
            }
            else
            {
                _dsCbac.ExtendedProperties["SAVE_ID"] = "F";
            }
            if (_hasAmtnCbacSO)
            {
                _dsCbac.ExtendedProperties["HAS_AMTN_CBAC_SO"] = "T";
            }
            else
            {
                _dsCbac.ExtendedProperties["HAS_AMTN_CBAC_SO"] = "F";
            }
            //if (!_dsCbac.ExtendedProperties.ContainsKey("NO_MAK_VOH_NO"))
            //{
            //    _dsCbac.ExtendedProperties.Add("NO_MAK_VOH_NO", "");
            //}
            _dsCbac.ExtendedProperties["NO_MAK_VOH_NO"] = "True";
            _cbac.UpdateData(_dsCbac, true);
            #endregion
        }
        #endregion

        #region 更新源单
        private void UpdateOS(DataRow dr, bool isByAuditPass)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr.RowState == DataRowState.Modified)
                    UpdateOS(dr, true, isByAuditPass);
                UpdateOS(dr, false, isByAuditPass);
            }
            else
                UpdateOS(dr, true, isByAuditPass);

        }
        private void UpdateOS(DataRow dr, bool isDel, bool isByAuditPass)
        {
            #region //回写源单
            if (!_isRunAuditing || isByAuditPass) //被审核通过或者不需要跑审核流程
            {
                DbDRPCK _db = new DbDRPCK(Comp.Conn_DB);
                string _prdNo = "";
                string _unit = "";
                string osId = "CK";
                string osNo = "";
                string osItm = "";
                decimal _qty = 0;
                if (!isDel)
                {
                    _prdNo = Convert.ToString(dr["PRD_NO"]);
                    _unit = Convert.ToString(dr["UNIT"]);
                    osNo = Convert.ToString(dr["CK_NO"]);
                    osItm = Convert.ToString(dr["OTH_ITM"]);
                    if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    {
                        _qty = Convert.ToDecimal(dr["QTY"]);
                    }
                }
                else
                {
                    _prdNo = Convert.ToString(dr["PRD_NO", DataRowVersion.Original]);
                    _unit = Convert.ToString(dr["UNIT", DataRowVersion.Original]);
                    osNo = Convert.ToString(dr["CK_NO", DataRowVersion.Original]);
                    osItm = Convert.ToString(dr["OTH_ITM", DataRowVersion.Original]);
                    if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    {
                        _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]) * (-1);
                    }
                }
                if (!string.IsNullOrEmpty(osNo)) //需要回写出库单
                {
                    Hashtable _ht = new Hashtable();
                    _ht["OsID"] = osId;
                    _ht["OsNO"] = osNo;
                    _ht["KeyItm"] = osItm;
                    _ht["TableName"] = "TF_CK";
                    _ht["IdName"] = "CK_ID";
                    _ht["NoName"] = "CK_NO";
                    _ht["ItmName"] = "PRE_ITM";
                    _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_unit), _ht);
                    _db.UpdateQtyPS(osId, osNo, osItm, _qty);
                }
            }
            #endregion
        }
        private void UpdateOSydID(DataRow dr, bool isByAuditPass)
        {
            if (dr.RowState == DataRowState.Deleted)
                UpdateOSydID(dr, true, isByAuditPass);
            if (dr.RowState == DataRowState.Added)
                UpdateOSydID(dr, false, isByAuditPass);
        }
        private void UpdateOSydID(DataRow dr, bool isDel, bool isByAuditPass)
        {
            if (!_isRunAuditing || isByAuditPass) //被审核通过或者不需要跑审核流程
            {
                string ydid = "";
                string osid = "CK";
                string osno = "";
                if (isDel)
                {
                    ydid = Convert.ToString(dr["YD_ID", DataRowVersion.Original]);
                    osno = Convert.ToString(dr["OS_NO", DataRowVersion.Original]);
                }
                else
                {
                    ydid = Convert.ToString(dr["YD_ID"]);
                    osno = Convert.ToString(dr["OS_NO"]);
                }
                if (string.Compare(ydid, "T") == 0)
                {
                    DbDRPCK _db = new DbDRPCK(Comp.Conn_DB);
                    _db.UpdateYDID(osid, osno);
                }
            }
        }
        private decimal Convert2Decimal(object p)
        {
            decimal _d = 0;
            if (!decimal.TryParse(Convert.ToString(p), out _d))
                _d = 0;
            return _d;
        }
        #endregion

        #region 更新账户服务次数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isAdd"></param>
        private void UpdateMonSbac(DataRow dr, bool isAdd)
        {
            string _cardNo = "";
            string _subNo = "";
            string _cusNo = "";
            string _idxNo = "";
            string _prdNo = "";
            string _chkMan = "";
            decimal _numUse = 0;
            DateTime? _lastDd = DateTime.Now;
            if (isAdd)
            {
                if (dr["SBAC_CHK"].ToString() != "T")
                    return;
                if (dr["FREE_ID"].ToString() == "T")
                    return;
                _cardNo = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_CARD_NO"].ToString();
                _subNo = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["SUB_NO"].ToString();
                _cusNo = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_NO"].ToString();
                _chkMan = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CHK_MAN"].ToString();
                _prdNo = dr["PRD_NO"].ToString();
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _numUse = Convert.ToDecimal(dr["QTY"]);
                }
                if (!string.IsNullOrEmpty(dr["PS_DD"].ToString()))
                {
                    _lastDd = Convert.ToDateTime(dr["PS_DD"]);
                }
            }
            else
            {
                if (dr["SBAC_CHK", DataRowVersion.Original].ToString() != "T")
                    return;
                if (dr["FREE_ID", DataRowVersion.Original].ToString() == "T")
                    return;
                _cardNo = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_CARD_NO", DataRowVersion.Original].ToString();
                _subNo = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["SUB_NO", DataRowVersion.Original].ToString();
                _cusNo = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CUS_NO", DataRowVersion.Original].ToString();
                _chkMan = dr.Table.DataSet.Tables["MF_PSS"].Rows[0]["CHK_MAN", DataRowVersion.Original].ToString();
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _numUse = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]) * -1;
                }
                if (!string.IsNullOrEmpty(dr["PS_DD", DataRowVersion.Original].ToString()))
                {
                    _lastDd = null;
                }
            }
            if (!isAdd && String.IsNullOrEmpty(_chkMan))
                return;
            CBac _cbac = new CBac();
            _cbac.UpdateCustSBac(_cusNo, _cardNo, _subNo, _prdNo, _numUse, _lastDd);
        }
        #endregion

        /// <summary>
        /// 更新BOM组合生产单
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isAdd"></param>
        private void UpdateMrpMb(DataRow dr, bool isAdd)
        {
            string _bilId = "";
            string _bilNo = "";
            int _bilItm = 0;
            string _prdNo = "";
            string _prdName = "";
            string _prdMark = "";
            string _idNo = "";
            string _wh = "";
            string _chkMan = "";
            string _unit = "";
            string _dep = "";
            string _salNo = "";
            string _usr = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
            DateTime _validDd = DateTime.Now;
            DateTime _bilDd = DateTime.Now;
            DateTime _clsDd = DateTime.Now;
            DataRow _drMF = dr.Table.DataSet.Tables["MF_PSS"].Rows[0];

            if (isAdd)
            {
                _bilId = dr["PS_ID"].ToString();
                _bilNo = dr["PS_NO"].ToString();
                if (!string.IsNullOrEmpty(dr["PRE_ITM"].ToString()))
                {
                    _bilItm = Convert.ToInt32(dr["PRE_ITM"]);
                }
                _prdNo = dr["PRD_NO"].ToString();
                _prdName = dr["PRD_NAME"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _idNo = dr["ID_NO"].ToString();
                _wh = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                _chkMan = _drMF["CHK_MAN"].ToString();
                _dep = _drMF["DEP"].ToString();
                _salNo = _drMF["SAL_NO"].ToString();
                _usr = _drMF["USR"].ToString();
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                if (!string.IsNullOrEmpty(dr["QTY1"].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1"]);
                }
                if (!string.IsNullOrEmpty(dr["PS_DD"].ToString()))
                {
                    _bilDd = Convert.ToDateTime(dr["PS_DD"]);
                }
                if (!string.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD"]);
                }
                if (!string.IsNullOrEmpty(_drMF["CLS_DATE"].ToString()))
                {
                    _clsDd = Convert.ToDateTime(_drMF["CLS_DATE"]);
                }
            }
            else
            {
                _bilId = dr["PS_ID", DataRowVersion.Original].ToString();
                _bilNo = dr["PS_NO", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["PRE_ITM", DataRowVersion.Original].ToString()))
                {
                    _bilItm = Convert.ToInt32(dr["PRE_ITM", DataRowVersion.Original]);
                }
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdName = dr["PRD_NAME", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _idNo = dr["ID_NO", DataRowVersion.Original].ToString();
                _wh = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                _chkMan = _drMF["CHK_MAN", DataRowVersion.Original].ToString();
                _dep = _drMF["DEP", DataRowVersion.Original].ToString();
                _salNo = _drMF["SAL_NO", DataRowVersion.Original].ToString();
                _usr = _drMF["USR", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!string.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                }
                if (!string.IsNullOrEmpty(dr["PS_DD", DataRowVersion.Original].ToString()))
                {
                    _bilDd = Convert.ToDateTime(dr["PS_DD", DataRowVersion.Original]);
                }
                if (!string.IsNullOrEmpty(dr["VALID_DD", DataRowVersion.Original].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD", DataRowVersion.Original]);
                }
                if (!string.IsNullOrEmpty(_drMF["CLS_DATE", DataRowVersion.Original].ToString()))
                {
                    _clsDd = Convert.ToDateTime(_drMF["CLS_DATE", DataRowVersion.Original]);
                }
            }

            if (_bilId != "SA" || string.IsNullOrEmpty(_idNo))
            {
                return;
            }
            if (_qty == 0)
            {
                return;
            }

            MRPMB _mrpMb = new MRPMB();
            SunlikeDataSet _ds = _mrpMb.GetDataBilNo(_bilId, _bilNo, _bilItm);
            if (!string.IsNullOrEmpty(this._saveID))
            {
                if (this._saveID == "T")
                {
                    _ds.ExtendedProperties["SAVE_ID"] = "T";
                    _chkMan = _usr;
                    _clsDd = _clsDd = DateTime.Now;
                }
                else
                {
                    _ds.ExtendedProperties["SAVE_ID"] = "F";
                }
            }
            DataTable _dtHead = _ds.Tables["MF_MB"];
            DataTable _dtBody = _ds.Tables["TF_MB"];
            DataRow _drNew;
            if (isAdd)
            {
                if (_dtHead.Rows.Count > 0)
                {
                    _drNew = _dtHead.Rows[0];
                }
                else
                {
                    _drNew = _dtHead.NewRow();
                    _drNew["MB_ID"] = "MB";
                    _drNew["MB_NO"] = "";
                    _dtHead.Rows.Add(_drNew);
                }
                _drNew["MB_DD"] = _bilDd;
                _drNew["BIL_ID"] = _bilId;
                _drNew["BIL_NO"] = _bilNo;
                _drNew["BIL_ITM"] = _bilItm;
                _drNew["WH_PRD"] = _wh;
                _drNew["MRP_NO"] = _prdNo;
                _drNew["PRD_NAME"] = _prdName;
                _drNew["PRD_MARK"] = _prdMark;
                _drNew["UNIT"] = _unit;
                _drNew["WH_PRD"] = _wh;
                _drNew["QTY"] = _qty;
                _drNew["QTY1"] = _qty1;
                _drNew["VALID_DD"] = _validDd;
                _drNew["DEP"] = _dep;
                _drNew["USR_NO"] = _salNo;
                _drNew["USR"] = _usr;
                _drNew["CLS_DATE"] = _clsDd;
                _drNew["CHK_MAN"] = _chkMan;
                _drNew["ID_NO"] = _idNo;
                if (_dtBody.Rows.Count == 0)
                {
                    MRPBom _mrpBom = new MRPBom();
                    DataTable _dtBom = new DataTable();
                    _mrpBom.GetBomDetail(_dtBom, _idNo, 0);
                    DataRow[] _drsBom = _dtBom.Select("IS_CHILD='T'");
                    if (_drsBom.Length <= 0)
                    {
                        return;
                    }
                    int _itm = 1;
                    Prdt _prdt = new Prdt();

                    foreach (DataRow _dr in _drsBom)
                    {
                        _drNew = _dtBody.NewRow();
                        _drNew["MB_ID"] = "MB";
                        _drNew["MB_NO"] = "";
                        _drNew["ITM"] = _itm++;
                        _drNew["MB_DD"] = _bilDd;
                        _drNew["PRD_NO"] = _dr["PRD_NO"];
                        _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                        _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                        _drNew["UNIT"] = _unit;
                        _drNew["WH"] = _dr["WH_NO"];
                        //标准用量
                        decimal _calQty = 1;
                        string _currentNo = _dr["PRD_NO"].ToString();
                        while (true)
                        {
                            DataRow[] _drs = _dtBom.Select("PRD_NO='" + _currentNo + "'");
                            if (_drs.Length == 0) break;

                            decimal _bomQty = 0;
                            decimal _upQty = 0;
                            string _bomUnit = _drs[0]["UNIT"].ToString();
                            string _upUnit = _drs[0]["UP_UNIT"].ToString();

                            if (!string.IsNullOrEmpty(_drs[0]["BOM_QTY"].ToString()))
                                _bomQty = Convert.ToDecimal(_drs[0]["BOM_QTY"]);
                            if (!string.IsNullOrEmpty(_drs[0]["UP_QTY"].ToString()))
                                _upQty = Convert.ToDecimal(_drs[0]["UP_QTY"]);

                            if (_bomQty == 0 || _upQty == 0)
                            {
                                _calQty = 0;
                                break;
                            }
                            if (_bomUnit == "2" || _bomUnit == "3")
                                _bomQty = _prdt.GetUnitQty(_dr["PRD_NO"].ToString(), _bomUnit, _bomQty, "1");
                            if (_upUnit == "2" || _upUnit == "3")
                                _upQty = _prdt.GetUnitQty(_dr["PRD_NO"].ToString(), _upUnit, _upQty, "1");

                            _calQty *= _bomQty / _upQty;

                            if (_drs[0]["UP_NO"].ToString() == _prdNo)
                            {
                                _calQty = _calQty * _qty;
                                break;
                            }
                            else
                                _currentNo = _drs[0]["UP_NO"].ToString();
                        }
                        _drNew["QTY"] = _calQty;

                        _calQty = 1;
                        _currentNo = _dr["PRD_NO"].ToString();
                        while (true)
                        {
                            DataRow[] _drs = _dtBom.Select("PRD_NO='" + _currentNo + "'");
                            if (_drs.Length == 0) break;

                            decimal _bomQty = 0;
                            decimal _upQty = 0;

                            if (!string.IsNullOrEmpty(_drs[0]["BOM_QTY1"].ToString()))
                                _bomQty = Convert.ToDecimal(_drs[0]["BOM_QTY1"]);
                            if (!string.IsNullOrEmpty(_drs[0]["UP_QTY1"].ToString()))
                                _upQty = Convert.ToDecimal(_drs[0]["UP_QTY1"]);
                            if (_bomQty == 0 || _upQty == 0)
                            {
                                _calQty = 0;
                                break;
                            }
                            _calQty *= _bomQty / _upQty;
                            if (_drs[0]["UP_NO"].ToString() == _prdNo)
                            {
                                _calQty = _calQty * (_qty1 == 0 ? _qty : _qty1);
                                break;
                            }
                            else
                                _currentNo = _drs[0]["UP_NO"].ToString();
                        }
                        _drNew["QTY1"] = _calQty;
                        _dtBody.Rows.Add(_drNew);
                    }
                }
                if (_dtBody.Rows.Count == 0)
                    return;
            }
            else
            {
                if (_dtHead.Rows.Count > 0)
                {
                    _dtHead.Rows[0].Delete();
                }
            }

            _mrpMb.UpdateData("", _ds, true);
        }

        /// <summary>
        /// 帐户收支单
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isAdd"></param>
        private void UpdateBac(DataRow dr, bool isAdd)
        {
            string _psId = "";
            string _psNo = "";
            string _baccNo = "";
            string _caccNo = "";
            string _dep = "";
            string _salNo = "";
            string _usr = "";
            string _chkMan = "";
            string _addId = "+";
            string _curId = "";
            string _zhangId = "";
            decimal _excRto = 1;
            decimal _amtnBB = 0;
            decimal _amtBB = 0;
            decimal _amtnBC = 0;
            decimal _amtBC = 0;
            DateTime _psDd = System.DateTime.Now;
            if (isAdd)
            {
                _psId = dr["PS_ID"].ToString();
                _psNo = dr["PS_NO"].ToString();
                _psDd = Convert.ToDateTime(dr["PS_DD"]);
                _baccNo = dr["BACC_NO"].ToString();
                _caccNo = dr["CACC_NO"].ToString();
                _dep = dr["DEP"].ToString();
                _salNo = dr["SAL_NO"].ToString();
                _usr = dr["USR"].ToString();
                _chkMan = dr["CHK_MAN"].ToString();
                _curId = dr["CUR_ID"].ToString();
                _zhangId = dr["ZHANG_ID"].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN_BB"].ToString()))
                    _amtnBB = Convert.ToDecimal(dr["AMTN_BB"]);
                if (!string.IsNullOrEmpty(dr["AMT_BB"].ToString()))
                    _amtBB = Convert.ToDecimal(dr["AMT_BB"]);
                if (!string.IsNullOrEmpty(dr["AMTN_BC"].ToString()))
                    _amtnBC = Convert.ToDecimal(dr["AMTN_BC"]);
                if (!string.IsNullOrEmpty(dr["AMT_BC"].ToString()))
                    _amtBC = Convert.ToDecimal(dr["AMT_BC"]);
                if (!string.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                    _excRto = Convert.ToDecimal(dr["EXC_RTO"]);
            }
            else
            {
                _psId = dr["PS_ID", DataRowVersion.Original].ToString();
                _psNo = dr["PS_NO", DataRowVersion.Original].ToString();
                _psDd = Convert.ToDateTime(dr["PS_DD", DataRowVersion.Original]);
                _baccNo = dr["BACC_NO", DataRowVersion.Original].ToString();
                _caccNo = dr["CACC_NO", DataRowVersion.Original].ToString();
                _dep = dr["DEP", DataRowVersion.Original].ToString();
                _salNo = dr["SAL_NO", DataRowVersion.Original].ToString();
                _usr = dr["USR", DataRowVersion.Original].ToString();
                _chkMan = dr["CHK_MAN", DataRowVersion.Original].ToString();
                _curId = dr["CUR_ID", DataRowVersion.Original].ToString();
                _zhangId = dr["ZHANG_ID", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN_BB", DataRowVersion.Original].ToString()))
                    _amtnBB = Convert.ToDecimal(dr["AMTN_BB", DataRowVersion.Original]);
                if (!string.IsNullOrEmpty(dr["AMT_BB", DataRowVersion.Original].ToString()))
                    _amtBB = Convert.ToDecimal(dr["AMT_BB", DataRowVersion.Original]);
                if (!string.IsNullOrEmpty(dr["AMTN_BC", DataRowVersion.Original].ToString()))
                    _amtnBC = Convert.ToDecimal(dr["AMTN_BC", DataRowVersion.Original]);
                if (!string.IsNullOrEmpty(dr["AMT_BC", DataRowVersion.Original].ToString()))
                    _amtBC = Convert.ToDecimal(dr["AMT_BC", DataRowVersion.Original]);
                if (!string.IsNullOrEmpty(dr["EXC_RTO", DataRowVersion.Original].ToString()))
                    _excRto = Convert.ToDecimal(dr["EXC_RTO", DataRowVersion.Original]);
            }

            if (string.Compare("SA", _psId) != 0)
                return;
            if (string.Compare("4", _zhangId) != 0)
                return;

            Bacc _bacc = new Bacc();
            SunlikeDataSet _dsBac = _bacc.GetBAC(_psId + _psNo);
            if (!_dsBac.ExtendedProperties.ContainsKey("NO_MAK_VOH_NO"))
            {
                _dsBac.ExtendedProperties.Add("NO_MAK_VOH_NO", "");
            }
            _dsBac.ExtendedProperties["NO_MAK_VOH_NO"] = "True";

            if (isAdd)
            {
                for (int i = 0; i <= 1; i++)
                {
                    string _accNo = (i > 0 ? _baccNo : _caccNo);
                    decimal _amtn = (i > 0 ? _amtnBB : _amtnBC);
                    decimal _amt = (i > 0 ? _amtBB : _amtBC);

                    if (string.IsNullOrEmpty(_accNo))
                        continue;

                    DataRow _drNew = null;
                    DataRow[] _drs = _dsBac.Tables["MF_BAC"].Select("BACC_NO='" + _accNo + "'");
                    if (_drs.Length > 0)
                    {
                        _drNew = _drs[0];
                    }
                    else
                    {
                        _drNew = _dsBac.Tables["MF_BAC"].NewRow();
                        _drNew["BB_ID"] = "BT";
                        _drNew["BB_NO"] = "";
                    }

                    _drNew["BB_DD"] = _psDd;
                    _drNew["BACC_NO"] = _accNo;
                    _drNew["DEP"] = _dep;
                    _drNew["BIL_NO"] = _psId + _psNo;
                    _drNew["PAY_MAN"] = _salNo;
                    _drNew["EXC_RTO"] = _excRto;
                    _drNew["CUR_ID"] = _curId;
                    _drNew["AMTN"] = _amtn;
                    _drNew["AMT"] = _amt;
                    _drNew["USR"] = _usr;
                    _drNew["CHK_MAN"] = _chkMan;
                    _drNew["PRT_SW"] = "N";
                    _drNew["OPN_ID"] = "F";
                    _drNew["CLS_DATE"] = _psDd;
                    _drNew["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    _dsBac.Tables["MF_BAC"].Rows.Add(_drNew);
                    //表身编辑
                    _drs = _dsBac.Tables["TF_BAC"].Select("BB_ID='" + _drNew["BB_ID"].ToString() + "' AND BB_NO='" + _drNew["BB_NO"].ToString() + "'");
                    if (_drs.Length > 0)
                    {
                        _drNew = _drs[0];
                    }
                    else
                    {
                        _drNew = _dsBac.Tables["TF_BAC"].NewRow();
                        _drNew["BB_ID"] = "BT";
                        _drNew["BB_NO"] = "";
                    }
                    _drNew["ITM"] = "1";
                    _drNew["BB_DD"] = _psDd;
                    _drNew["ADD_ID"] = _addId;
                    _drNew["EXC_RTO"] = _excRto;
                    _drNew["CUR_ID"] = _curId;
                    _drNew["AMTN"] = _amtn;
                    _drNew["AMT"] = _amt;
                    _drNew["DEP"] = _dep;
                    _drNew["RCV_MAN"] = _salNo;
                    _dsBac.Tables["TF_BAC"].Rows.Add(_drNew);
                    _bacc.UpdateData(_dsBac, true);
                }
            }
            else
            {
                foreach (DataRow drBac in _dsBac.Tables["MF_BAC"].Rows)
                {
                    drBac.Delete();
                }
                _bacc.UpdateData(_dsBac, true);
            }
        }

        #region function
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

            InvIK _invik = new InvIK();

            string bilId = dr["PS_ID", DataRowVersion.Original].ToString();
            string ckNo = dr["PS_NO", DataRowVersion.Original].ToString();

            SunlikeDataSet _ds = _invik.GetInTfLz(bilId, ckNo);
            if (_ds.Tables["TF_LZ"].Rows.Count > 0)
            {
                throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + ",PARAM=" + _ds.Tables["TF_LZ"].Rows[0]["LZ_NO"].ToString());//无法删除单号，原因：{0}
            }


        }

        /// <summary>
        /// 回写POS订单
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isAdd"></param>
        private void UpdatePosOs(DataRow dr, bool isAdd)
        {
            string _psNo = "";
            string _psId = "";
            string _posOsNo = "";
            string _posOsId = "";

            if (isAdd)
            {
                _psNo = dr["PS_NO"].ToString();
                _psId = dr["PS_ID"].ToString();
                _posOsNo = dr["POS_OS_CLS"].ToString();
                _posOsId = dr["POS_OS_ID"].ToString();
            }
            else
            {
                _psNo = dr["PS_NO", DataRowVersion.Original].ToString();
                _psId = dr["PS_ID", DataRowVersion.Original].ToString();
                _posOsNo = dr["POS_OS_CLS", DataRowVersion.Original].ToString();
                _posOsId = dr["POS_OS_ID", DataRowVersion.Original].ToString();
            }

            if (_posOsId == "T" || string.IsNullOrEmpty(_posOsNo))
            {
                return;
            }

            Query _query = new Query();
            if (isAdd)
            {
                string _sql = "UPDATE MF_PSS SET POS_OS_CLS='" + _psNo + "' WHERE PS_NO='" + _posOsNo + "'";
                _query.DoSQLString(_sql);
            }
            else
            {
                string _sql = "UPDATE MF_PSS SET POS_OS_CLS=null WHERE  PS_NO='" + _posOsNo + "'";
                _query.DoSQLString(_sql);
            }
        }
    }
}
