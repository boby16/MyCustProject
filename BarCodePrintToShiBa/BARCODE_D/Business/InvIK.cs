using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.Utility;
using System.Collections;

namespace Sunlike.Business
{
    public class InvIK : BizObject, IAuditing
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public InvIK()
        { 

        }
        #endregion
        private bool _reBuildVohNo = false;
        private bool _isRunAuditing;
        private System.Data.StatementType _state = new StatementType();
        /// <summary>
        /// 取得补开发票的资料
        /// </summary>
        /// <param name="usr">当前操作用户</param>
        /// <param name="LzID">单据代号</param>
        /// <param name="LzNo">单据号码</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string LzID, string LzNo)
        {
            return GetData(usr, LzID, LzNo, false, true);
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
        public SunlikeDataSet GetData(string usr, string LzID, string LzNo, bool OnlyFillSchema, bool HasDsc)
        {
            DbInvIK _dbinvIK = new DbInvIK(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbinvIK.GetData(LzID, LzNo, OnlyFillSchema);

            if (usr != null && !string.IsNullOrEmpty(usr))
            {
                Users _user = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_user.GetUserDepNo(usr)).DecimalDigitsInfo.System;
                
             
            }
            //增加单据权限
            if (!OnlyFillSchema)
            {
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    
                    string _pgm = "INVIK";
                    switch (LzID)
                    { 
                        case "LO":
                            _pgm = "INVIK";
                            break;
                        case "LZ":
                            _pgm = "INVLZ";
                            break;
                        case "LI":
                            _pgm = "INVLI";
                            break;
                    }
                    DataTable _dtMf = _ds.Tables["MF_LZ"];
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

            DataTable _dtTflz = _ds.Tables["TF_LZ"];
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
                        _dtTflz.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString());
                        _dtTflz.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString() + "_DSC");
                    }
                    //整理表身特征分段栏位
                    if (_dtMark.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dtTflz.Rows.Count; i++)
                        {
                            string _prdMark = _dtTflz.Rows[i]["PRD_MARK"].ToString();
                            if (!String.IsNullOrEmpty(_prdMark))
                            {
                                string[] _aryPrdMark = _mark.BreakPrdMark(_prdMark);
                                for (int j = 0; j < _dtMark.Rows.Count; j++)
                                {
                                    string _fldName = _dtMark.Rows[j]["FLDNAME"].ToString();
                                    _dtTflz.Rows[i][_fldName] = _aryPrdMark[j].Trim();
                                }
                            }
                        }
                    }
                }
            }

            //-----加入出库金额栏位
            _dtTflz.Columns.Add(new DataColumn("AMT_CK", typeof(decimal)));
            //--应开数量
            _dtTflz.Columns.Add(new DataColumn("QTY_CK", typeof(decimal)));
            //已开票金额
            _dtTflz.Columns.Add(new DataColumn("AMT_CLS", typeof(decimal)));
            //结馀总额
            _dtTflz.Columns.Add(new DataColumn("AMT_NEW", typeof(decimal)));
            //出库单未税位币
            _dtTflz.Columns.Add(new DataColumn("AMTN_NET_CK", typeof(decimal)));
            //出库单税额
            _dtTflz.Columns.Add(new DataColumn("TAX_CK", typeof(decimal)));
            //已开发票未税本位币
            _dtTflz.Columns.Add(new DataColumn("AMTN_NET_CLS", typeof(decimal)));
            //已开发票税额
            _dtTflz.Columns.Add(new DataColumn("TAX_CLS", typeof(decimal)));

            //已开数量
            _dtTflz.Columns.Add(new DataColumn("QTY_CLS", typeof(decimal)));
            //结馀未税金额
            _dtTflz.Columns.Add(new DataColumn("AMTN_NET_NEW", typeof(decimal)));
            //结馀税额
            _dtTflz.Columns.Add(new DataColumn("TAX_NEW", typeof(decimal)));
            //结馀数量
            _dtTflz.Columns.Add(new DataColumn("QTY_NEW", typeof(decimal)));
            //费用支出
            _dtTflz.Columns.Add(new DataColumn("AMTN_EP", typeof(decimal)));
            //费用收入
            _dtTflz.Columns.Add(new DataColumn("AMTN_ER", typeof(decimal)));
            //品牌  
            _dtTflz.Columns.Add(new DataColumn("MRK", typeof(string)));
            //摘要  
            _dtTflz.Columns.Add(new DataColumn("REM1", typeof(string)));
            _dtTflz.Columns.Add(new DataColumn("AMTN_NET_OLD", typeof(decimal)));
            _dtTflz.Columns.Add(new DataColumn("AMT_OLD", typeof(decimal)));
            _dtTflz.Columns.Add(new DataColumn("TAX_OLD", typeof(decimal)));
            _dtTflz.Columns.Add(new DataColumn("QTY_OLD", typeof(decimal)));
            _dtTflz.Columns.Add(new DataColumn("EXC_RTO", typeof(decimal)));
            _dtTflz.Columns.Add(new DataColumn("CAS_NO", typeof(string)));
            _dtTflz.Columns.Add(new DataColumn("TASK_ID", typeof(string)));
            _dtTflz.Columns.Add(new DataColumn("VOH_ID", typeof(string)));
            CreateTotalTable(_ds);
            if (_ds.Tables["MF_LZ"].Rows.Count > 0)
            {
                foreach (DataRow dr in _ds.Tables["TF_LZ"].Rows)
                {
                    if ((_ds.Tables["MF_LZ"].Rows[0]["TURN_ID"].ToString() == "1") ||
                        (dr["BIL_ID"].ToString() == "ER"))
                        Load_Body_Data1(dr);
                    else
                        Load_Body_Data2(dr);
                }
            }
            
            return _ds;


        }

        private void CreateTotalTable(SunlikeDataSet ds)
        {
            DataTable dt = new DataTable();
            dt.TableName = "TF_LZ1";
            dt.Columns.Add(new DataColumn("BAT_NO", typeof(string)));
            dt.Columns.Add(new DataColumn("PRD_NO", typeof(string)));
            dt.Columns.Add(new DataColumn("PRD_MARK", typeof(string)));
            dt.Columns.Add(new DataColumn("UNIT", typeof(string)));
            dt.Columns.Add(new DataColumn("PRD_NAME", typeof(string)));
            dt.Columns.Add(new DataColumn("AMT_CK", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AMT_CLS", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AMT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AMT_NEW", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AMTN_NET", typeof(decimal)));
            dt.Columns.Add(new DataColumn("TAX", typeof(decimal)));
            dt.Columns.Add(new DataColumn("QTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("UP", typeof(decimal)));
            dt.Columns.Add(new DataColumn("QTY_CK", typeof(decimal)));
            dt.Columns.Add(new DataColumn("SUP_PRD_NO", typeof(string)));
            dt.Columns.Add(new DataColumn("REM", typeof(string)));
            dt.Columns.Add(new DataColumn("SPC", typeof(string)));
            
            dt.Columns.Add(new DataColumn("MRK", typeof(string)));

            DataColumn[]  _dca = new DataColumn[4];
            _dca[0] = dt.Columns["BAT_NO"];
            _dca[1] = dt.Columns["PRD_NO"];
            _dca[2] = dt.Columns["PRD_MARK"];
            _dca[3] = dt.Columns["UNIT"];
            dt.PrimaryKey = _dca;
            ds.Tables.Add(dt);
            
        }

        
        #region 用来更新表身来源单的栏位数据

        /// <summary>
        /// 用来更新表身来源单的栏位数据滙总输入
        /// </summary>
        /// <param name="ds"></param>
        private void Load_Body_Data2(DataRow dr)
        {
            DataTable HTable = null;
            DataTable BTable = null;
            DataRow bRow = null;
            SunlikeDataSet _sourceDs = null;
            //foreach (DataRow dr in ds.Tables["TF_LZ"].Rows)
            //{
                string _bilId = dr["BIL_ID"].ToString();
                string _ckNo = dr["CK_NO"].ToString();
                string _tblStr = "LN,LB";
                #region 借出借入单
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    DRPBN _drpbn = new DRPBN();
                    _sourceDs = _drpbn.GetData("",dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilId, _ckNo, false);
                    HTable = _sourceDs.Tables["MF_BLN"];
                    BTable = _sourceDs.Tables["TF_BLN"];
                }
                #endregion
                #region 销退折单
                _tblStr = "SA,SB,SD";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    DRPSA _drpsa = new DRPSA();

                    _sourceDs = _drpsa.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilId, _ckNo,false,false);
                    HTable = _sourceDs.Tables["MF_PSS"];
                    BTable = _sourceDs.Tables["TF_PSS"];
                }
                #endregion
                #region 配送单,配送退回,客户调拨入库
                _tblStr = "IO,IB,IM";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    DRPIO _drpio = new DRPIO();
                    _sourceDs = _drpio.GetData(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilId, _ckNo, false);
                    HTable = _sourceDs.Tables["MF_IC"];
                    BTable = _sourceDs.Tables["TF_IC"];
                }
                #endregion
                #region 调价单
                _tblStr = "TJ";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    DRPTJ _drptj = new DRPTJ();

                    _sourceDs = _drptj.GetData(_ckNo, false);
                    HTable = _sourceDs.Tables["MF_TJ"];
                    BTable = _sourceDs.Tables["TF_TJ"];
                }
                #endregion
                #region 其他费用收入
                _tblStr = "ER";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    MonEX _monex = new MonEX();

                    _sourceDs = _monex.GetData(_bilId, _ckNo, false);
                    HTable = _sourceDs.Tables["MF_EXP"];
                    BTable = _sourceDs.Tables["TF_EXP"];
                }
                #endregion
                #region 安装完工单
                _tblStr = "OD,OW";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    MTNFin _mtnfin = new MTNFin();
                    _sourceDs = _mtnfin.GetData("",dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilId, _ckNo);
                    HTable = _sourceDs.Tables["MF_MFIN"];
                    BTable = _sourceDs.Tables["TF_MFIN1"];
                }
                #endregion
                #region 托外缴回
                _tblStr = "CK";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    string _sql = "Select CK_NO,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,CUR_ID,EXC_RTO,CHK_MAN,DIS_CNT,CLSLZ,OS_ID,TAX_ID  from MF_CK"
                                    + " where CK_ID='" + _bilId + "' and CK_NO='" + _ckNo + "';";
                    _sql += " Select AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PRE_ITM,AMTN_NET,TAX,QTY,OS_ID,OS_NO,AMT From TF_CK Where CK_ID='" + _bilId + "' and  CK_NO='" + _ckNo + "'";
                    Query _query = new Query();
                    _sourceDs = _query.DoSQLString(_sql);
                    _sourceDs.Tables[0].TableName = "MF_CK";
                    _sourceDs.Tables[1].TableName = "TF_CK";

                    HTable = _sourceDs.Tables["MF_CK"];
                    BTable = _sourceDs.Tables["TF_CK"];

                }
                  #endregion
                if (_sourceDs == null)
                    return;
                decimal _rAmt = 0;
                decimal _rAmtn_Net = 0;
                decimal _rTax = 0;
                decimal _rQty = 0;
                #region 借出借入单
                if (_sourceDs.Tables.Contains("TF_BLN"))
                {
                    foreach (DataRow bldr in BTable.Rows)
                    {
                        _rAmtn_Net = _rAmtn_Net + ConvertDecimal(bldr, "AMTN_RNT_NET");
                        _rTax = _rTax + ConvertDecimal(bldr, "TAX_RNT");
                        _rQty = _rQty + ConvertDecimal(bldr, "QTY");
                        if (string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CUR_ID"].ToString()))
                            _rAmt = _rAmt + ConvertDecimal(bldr, "AMTN_RNT_NET") + ConvertDecimal(bldr, "TAX_RNT");
                        else
                            if ((dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TAX_ID"].ToString() == "3") &&
                                    (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CUR_ID"].ToString()))
                                )
                                _rAmt = _rAmt + ConvertDecimal(bRow, "AMT_RNT") + (ConvertDecimal(bRow, "AMT_RNT") * TaxRatio(bldr) / 100);
                            else
                                _rAmt = _rAmt + ConvertDecimal(bRow, "AMT_RNT");

                    }
                }
                #endregion
                #region 销货 维修 出库
                if (_sourceDs.Tables.Contains("TF_PSS") || _sourceDs.Tables.Contains("TF_MFIN1")
                     || _sourceDs.Tables.Contains("TF_CK"))
                {

                    foreach (DataRow bldr in BTable.Rows)
                    {

                        _rAmtn_Net = _rAmtn_Net + ConvertDecimal(bldr, "AMTN_NET");
                        _rTax = _rTax + ConvertDecimal(bldr, "TAX");
                        _rQty = _rQty + ConvertDecimal(bldr, "QTY");
                        if (string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CUR_ID"].ToString()))
                            _rAmt = _rAmt + ConvertDecimal(bldr, "AMTN_NET") + ConvertDecimal(bldr, "TAX");
                        else
                            if ((dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TAX_ID"].ToString() == "3") &&
                                    (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CUR_ID"].ToString()))
                                )
                                _rAmt = _rAmt + ConvertDecimal(bldr, "AMT") + (ConvertDecimal(bldr, "AMT") * TaxRatio(bldr) / 100);
                            else
                                _rAmt = _rAmt + ConvertDecimal(bldr, "AMT");

                    }

                }
                #endregion
                #region 配送单 调价单
                if (_sourceDs.Tables.Contains("TF_IC") || _sourceDs.Tables.Contains("TF_TJ"))
                {

                    foreach (DataRow bldr in BTable.Rows)
                    {
                        if ((_bilId == "IB") || (_bilId == "IM" && dr["IMTAG"].ToString() == "O"))
                        {
                            _rAmtn_Net = _rAmtn_Net + ConvertDecimal(bldr, "CST");
                            _rAmt = _rTax + ConvertDecimal(bldr, "CST");
                        }
                        else
                        {
                            _rAmtn_Net = _rAmtn_Net + ConvertDecimal(bldr, "AMTN_NET");
                            _rAmt = _rTax + ConvertDecimal(bldr, "AMTN_NET");
                        }
                        _rTax = 0;

                    }

                }
                #endregion
                if (_bilId == "SA" || _bilId == "CK")
                {
                    decimal _TempDis_Cnt = ConvertDecimal(HTable.Rows[0], "Dis_Cnt");
                    if (_TempDis_Cnt < 0)
                        _TempDis_Cnt = 100 + _TempDis_Cnt;
                    if (_TempDis_Cnt != 0 && HTable.Rows[0]["OS_NO"].ToString().IndexOf("-") == -1)
                    {
                        _TempDis_Cnt = _TempDis_Cnt / 100;
                        _rAmtn_Net = _rAmtn_Net * _TempDis_Cnt;
                        _rTax = _rTax * _TempDis_Cnt;
                        _rAmt = _rAmt * _TempDis_Cnt;
                    }
                }
                int PreNo = 1;
                string backStr = "SB,SD,IB";
                if (
                     (dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VERSION"].ToString() == "1") &&
                     ((backStr.IndexOf(_bilId) > -1) || (_bilId == "IM" && dr["IMTAG"].ToString() == "O"))
                    )
                    PreNo = -1;

                string fAMT_CLS = "";
                string fAMTN_NET_CLS = "";
                string fTAX_CLS = "";
                string fQTY_CLS = "";
                if ((_bilId == "IO") || (_bilId == "IM" && dr["IMTAG"].ToString() == "I"))
                {
                    fAMT_CLS = "AMT_CLS2";
                    fAMTN_NET_CLS = "AMTN_NET_CLS2";
                    fTAX_CLS = "TAX_CLS2";
                    fQTY_CLS = "QTY_CLS2";
                }
                else
                {
                    fAMT_CLS = "AMT_CLS";
                    fAMTN_NET_CLS = "AMTN_NET_CLS";
                    fTAX_CLS = "TAX_CLS";
                    fQTY_CLS = "QTY_CLS";
                }
                dr["AMTN_NET_CK"] = PreNo * _rAmtn_Net;
                dr["AMT_CK"] = PreNo * _rAmt;
                dr["TAX_CK"] = PreNo * _rTax;
                dr["QTY_CK"] = PreNo * _rQty;
                if (!_sourceDs.Tables.Contains("MF_TJ"))
                {
                    if (!_sourceDs.Tables.Contains("MF_IC"))
                    {
                        dr["EXC_RTO"] = ConvertDecimal(HTable.Rows[0], "EXC_RTO");
                    }
                    if (!_sourceDs.Tables.Contains("MF_MFIN"))
                    {
                        dr["CAS_NO"] = HTable.Rows[0]["CAS_NO"].ToString();
                        dr["TASK_ID"] = HTable.Rows[0]["TASK_ID"].ToString();
                    }

                }
                dr["AMTN_NET_OLD"] = ConvertDecimal(dr, "AMTN_NET");
                dr["AMT_OLD"] = ConvertDecimal(dr, "AMT");
                dr["TAX_OLD"] = ConvertDecimal(dr, "TAX");
                dr["QTY_OLD"] = ConvertDecimal(dr, "QTY");
                if (string.IsNullOrEmpty(HTable.Rows[0]["CHK_MAN"].ToString()))
                {
                    dr["AMTN_NET_CLS"] = PreNo * ConvertDecimal(HTable.Rows[0], fAMTN_NET_CLS);
                    dr["AMT_CLS"] = PreNo * ConvertDecimal(HTable.Rows[0], fAMT_CLS);
                    dr["TAX_CLS"] = PreNo * ConvertDecimal(HTable.Rows[0], fTAX_CLS);
                    dr["QTY_CLS"] = PreNo * ConvertDecimal(HTable.Rows[0], fQTY_CLS);

                }
                else
                {
                    dr["AMTN_NET_CLS"] = PreNo * ConvertDecimal(HTable.Rows[0], fAMTN_NET_CLS) - ConvertDecimal(dr, "AMTN_NET_OLD");
                    dr["AMT_CLS"] = PreNo * ConvertDecimal(HTable.Rows[0], fAMT_CLS) - ConvertDecimal(dr, "AMT_OLD");
                    dr["TAX_CLS"] = PreNo * ConvertDecimal(HTable.Rows[0], fTAX_CLS) - ConvertDecimal(dr, "TAX_OLD");
                    dr["QTY_CLS"] = PreNo * ConvertDecimal(HTable.Rows[0], fQTY_CLS) - ConvertDecimal(dr, "QTY_OLD");
                }
                //                if GetUnSHAmtn then
                //begin
                //  GetUnSHData(BIL_ID,BIL_NO,FieldByName('IMTAG').AsString,0,rAMT,rAMTN_NET,rTAX,rQTY);
                //  FieldByName('AMT_CLS').AsFloat:=FieldByName('AMT_CLS').AsFloat+rAMT;
                //  FieldByName('AMTN_NET_CLS').AsFloat:=FieldByName('AMTN_NET_CLS').AsFloat+rAMTN_NET;
                //  FieldByName('TAX_CLS').AsFloat:=FieldByName('TAX_CLS').AsFloat+rTAX;
                //  FieldByName('QTY_CLS').AsFloat:=FieldByName('QTY_CLS').AsFloat+rQTY;
                //end;
                dr["AMTN_NET_NEW"] = ConvertDecimal(dr, "AMTN_NET_CK") - ConvertDecimal(dr, "AMTN_NET_CLS") - ConvertDecimal(dr, "AMTN_NET");
                dr["AMT_NEW"] = ConvertDecimal(dr, "AMT_CK") - ConvertDecimal(dr, "AMT_CLS") - ConvertDecimal(dr, "AMT");
                dr["TAX_NEW"] = ConvertDecimal(dr, "TAX_CK") - ConvertDecimal(dr, "TAX_CLS") - ConvertDecimal(dr, "TAX");
                dr["QTY_NEW"] = ConvertDecimal(dr, "QTY_CK") - ConvertDecimal(dr, "QTY_CLS") - ConvertDecimal(dr, "QTY");

            //}
                _tblStr = "SA,CK";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    dr["AMTN_EP"] = GetAmtn_EPForQuery(dr["LZ_ID"].ToString(), dr["LZ_NO"].ToString(), "EP", Convert.ToInt32(dr["ITM"]));
                    dr["AMTN_ER"] = GetAmtn_EPForQuery(dr["LZ_ID"].ToString(), dr["LZ_NO"].ToString(), "ER", Convert.ToInt32(dr["ITM"]));
                }
        }


        /// <summary>
        /// 用来更新表身来源单的栏位数据
        /// </summary>
        /// <param name="dataTable">表身Table</param>
        private void Load_Body_Data1(DataRow dr)
        {

            DataTable HTable = null;
            DataRow bRow = null;
            SunlikeDataSet _sourceDs = null;
            //foreach (DataRow dr in ds.Tables["TF_LZ"].Rows)
            //{
                string _bilId = dr["BIL_ID"].ToString();
                string _ckNo = dr["CK_NO"].ToString();
                string _tblStr = "LN,LB";
                #region 借出借入单
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    string _sql = "Select BL_NO,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,CUR_ID,EXC_RTO,CHK_MAN,DIS_CNT,OS_ID,TAX_ID,VOH_ID,VOH_NO  from MF_BLN"
                                  + " where BL_ID='" + _bilId + "' and BL_NO='" + _ckNo + "';";
                    _sql += " Select AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PRE_ITM,AMTN_RNT_NET,TAX_RNT,QTY,OS_ID,OS_NO,AMT_RNT From TF_BLN Where BL_ID='" + _bilId + "' and  BL_NO='" + _ckNo + "' and PRE_ITM='" + dr["EST_ITM"].ToString() + "'";
                    Query _query = new Query();
                    _sourceDs = _query.DoSQLString(_sql);
                    _sourceDs.Tables[0].TableName = "MF_BLN";
                    _sourceDs.Tables[1].TableName = "TF_BLN";

//                    DRPBN _drpbn = new DRPBN();
  //                  _sourceDs = _drpbn.GetData("",dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilId, _ckNo, false);
                    DataRow[] bodyAry = _sourceDs.Tables["TF_BLN"].Select("PRE_ITM=" + dr["EST_ITM"].ToString());
                    if (bodyAry.Length > 0)
                    { 
                        HTable = _sourceDs.Tables["MF_BLN"];
                        bRow = bodyAry[0];
                    }
                }
                #endregion
                #region 销退折单
                _tblStr = "SA,SB,SD,PC,PD,PB";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    string _sql = "Select PS_NO,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,CUR_ID,EXC_RTO,CHK_MAN,DIS_CNT,CLSLZ,OS_ID,TAX_ID,VOH_ID,VOH_NO  from MF_PSS"
                                   + " where PS_ID='" + _bilId + "' and PS_NO='" + _ckNo + "';";
                    _sql += " Select AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PRE_ITM,AMTN_NET,TAX,QTY,OS_ID,OS_NO,AMT From TF_PSS Where PS_ID='" + _bilId + "' and  PS_NO='" + _ckNo + "' and PRE_ITM='" + dr["EST_ITM"].ToString() + "'";
                    Query _query = new Query();
                   _sourceDs = _query.DoSQLString(_sql);
                   _sourceDs.Tables[0].TableName = "MF_PSS";
                   _sourceDs.Tables[1].TableName = "TF_PSS";

                    //DRPSA _drpsa = new DRPSA();
                    //_sourceDs = _drpsa.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilId, _ckNo, false, false);
                    DataRow[] bodyAry = _sourceDs.Tables["TF_PSS"].Select("PRE_ITM=" + dr["EST_ITM"].ToString());
                    if (bodyAry.Length > 0)
                    {
                        HTable = _sourceDs.Tables["MF_PSS"];
                        bRow = bodyAry[0];
                    }
                }
                #endregion
                #region 配送单,配送退回,客户调拨入库
                _tblStr = "IO,IB,IM";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    string _sql = "select IC_ID,IC_NO,IC_DD,FIX_CST,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,DEP,BIL_TYPE,SAL_NO,CUS_NO1,"
                                  + " CUS_NO2,CLS_ID,SYS_DATE,TOT_BOX,TOT_QTY,BIL_ID,BIL_NO,OUTDEP,SAL_NO2,EP_ID,EP_NO,BAT_NO,VOH_ID,VOH_NO,"
                                  + " CFM_SW,IZ_CLS_ID,IZ_BACK_ID,LOCK_MAN,TURN_ID2,LZ_CLS_ID2,AMT_CLS2,AMTN_NET_CLS2,TAX_CLS2,QTY_CLS2, "
                                  + " TURN_ID,LZ_CLS_ID,AMT_CLS,AMTN_NET_CLS,TAX_CLS,QTY_CLS,CAS_NO,TASK_ID,MOB_ID From MF_IC "
                                   + " where IC_ID='" + _bilId + "' and IC_NO='" + _ckNo + "';";


                    _sql += " Select IC_ID,IC_NO,ITM,IC_DD,PRD_NO,PRD_MARK,UNIT,QTY,WH1,WH2,CST,FIX_CST,CST_STD,QTY_FA,KEY_ITM,UP, "+
                            " AMTN_NET,RTN_ID,BIL_ID,BIL_NO,BIL_ITM,UP_CST,DIS_CNT,PRE_ITM,BAT_NO,BAT_NO2,VALID_DD,AMTN_EP,QTY_CFM, "+
                            " QTY_LOST,EST_DD,REM,AMTN_NET_FP2,AMT_FP2,TAX_FP2,QTY_FP2,AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,QTY1,UP_QTY1,UP_QTY1_CST from TF_IC "+
                            "  Where IC_ID='" + _bilId + "' and  IC_NO='" + _ckNo + "' and KEY_ITM='" + dr["EST_ITM"].ToString() + "'";
                    Query _query = new Query();
                    _sourceDs = _query.DoSQLString(_sql);
                    _sourceDs.Tables[0].TableName = "MF_IC";
                    _sourceDs.Tables[1].TableName = "TF_IC";
                   // DRPIO _drpio = new DRPIO();
                   // _sourceDs = _drpio.GetData(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilId, _ckNo, false);
                    DataRow[] bodyAry = _sourceDs.Tables["TF_IC"].Select("KEY_ITM=" + dr["EST_ITM"].ToString());
                    if (bodyAry.Length > 0)
                    {
                        HTable = _sourceDs.Tables["MF_IC"];
                        bRow = bodyAry[0];
                    }
                }
                 #endregion
                #region 调价单
                _tblStr = "TJ";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    string _sql = "Select TJ_NO,TJ_DD,CUS_NO,TT_NO,SAL_NO,PRT_SW,AUD_FLAG,LZ_CLS_ID,BIL_TYPE,SYS_DATE,USR,DEP,CHK_MAN,CLS_DATE,REM,TURN_ID,AMT_CLS,AMTN_NET_CLS,TAX_CLS,QTY_CLS  from MF_TJ"
                                   + " where  and TJ_NO='" + _ckNo + "';";
                    _sql += " Select TJ_NO,ITM,PRD_NO,PRD_MARK,UPR4_OLD,UPR4,REM,KEY_ITM,AMTN_NET,QTY,AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP From TF_TJ Where   TJ_NO='" + _ckNo + "' and KEY_ITM='" + dr["EST_ITM"].ToString() + "'";
                    Query _query = new Query();
                    _sourceDs = _query.DoSQLString(_sql);
                    _sourceDs.Tables[0].TableName = "MF_TJ";
                    _sourceDs.Tables[1].TableName = "TF_TJ";

                   // DRPTJ _drptj = new DRPTJ();

                   // _sourceDs = _drptj.GetData( _ckNo, false);
                    DataRow[] bodyAry = _sourceDs.Tables["TF_TJ"].Select("KEY_ITM=" + dr["EST_ITM"].ToString());
                    if (bodyAry.Length > 0)
                    {
                        HTable = _sourceDs.Tables["MF_TJ"];
                        bRow = bodyAry[0];
                    }
                }
                #endregion
                #region 其他费用收入
                _tblStr = "ER";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    string _sql = "Select EP_DD, EP_NO, REM, USR, PRT_SW, EP_ID, USR_NO, OPN_ID, SYS_DATE,BIL_TYPE,DEP,CHK_MAN,PC_NO,CLS_DATE,BIL_ID,VOH_ID,VOH_NO,CAS_NO,TASK_ID  from MF_EXP "
                                   + " where EP_ID='" + _bilId + "' and EP_NO='" + _ckNo + "';";
                    _sql += " Select ITM, IDX_NO, CUS_NO, CUR_ID, EXC_RTO, TAX_ID, AMT, AMTN_NET, TAX, ARP_NO, ACC_NO, DEP, INV_NO, BAT_NO, REM, PAY_REM, AMT_RP, RP_NO, SAL_NO, PAY_DD, EP_NO, EP_ID, SHARE_MTH, CHK_DD, CHK_DAYS, CLOSE_FT, AMTN_FT_TOT, PAY_MTH, PAY_DAYS, CLS_REM, INT_DAYS, METH_ID, COMPOSE_IDNO, INV_DD, INV_YM, TITLE_BUY, AMT_INV, TAX_INV,RTO_TAX,BIL_ID,BIL_NO,KEY_ITM,AMTN_NET_FP,AMT_FP,TAX_FP,LZ_CLS_ID,CLSLZ From TF_EXP Where EP_ID='" + _bilId + "' and  EP_NO='" + _ckNo + "' and KEY_ITM='" + dr["EST_ITM"].ToString() + "'";
                    Query _query = new Query();
                    _sourceDs = _query.DoSQLString(_sql);
                    _sourceDs.Tables[0].TableName = "MF_EXP";
                    _sourceDs.Tables[1].TableName = "TF_EXP";

                    //MonEX _monex = new MonEX();
                    //_sourceDs = _monex.GetData(_bilId, _ckNo, false);
                    
                    DataRow[] bodyAry = _sourceDs.Tables["TF_EXP"].Select("KEY_ITM=" + dr["EST_ITM"].ToString());
                    if (bodyAry.Length > 0)
                    {
                        HTable = _sourceDs.Tables["MF_EXP"];
                        bRow = bodyAry[0];
                    }
                }
                 #endregion
                #region 安装完工单
                _tblStr = "OD,OW";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    string _sql = "Select OW_ID, OW_NO, OW_DD, CUS_NO, DEP, SAL_NO, BIL_TYPE, CLS_LZ_ID, CLS_LZ_AUTO, CUR_ID, EXC_RTO, TAX_ID, ZHANG_ID, REM, OT_ID,OT_NO, SYS_DATE, CLS_DATE, PRT_SW, USR, CHK_MAN, CNT_NO, CNT_REM, DIS_CNT, AMTN_NET, TAX, AMTN_INT, AMT_INT, INV_NO, RP_NO,PAY_MTH, PAY_DAYS, CHK_DAYS, PAY_REM, PAY_DD, CHK_DD, INT_DAYS, CLS_REM, AMT_CLS, AMTN_NET_CLS, TAX_CLS, QTY_CLS, KP_ID,TURN_ID, ARP_NO, AMTN_IRP, AMT_IRP, TAX_IRP,KIND_NO,REM_RUN,REM_CUST,CHK_CUST,VOH_ID,VOH_NO  from MF_MFIN"
                                   + " where OW_ID='" + _bilId + "' and OW_NO='" + _ckNo + "';";
                    _sql += " Select OW_ID, OW_NO, OW_ITM, ITM, WH, PRD_NO,PRD_NAME, PRD_MARK, QTY, UNIT, TAX_RTO, UP, AMTN_NET, AMT, TAX, DIS_CNT, MTN_DD, BAT_NO, AMT_FP, AMTN_NET_FP, TAX_FP, QTY_FP, VALID_DD, KEY_ITM From TF_MFIN1 Where OW_ID='" + _bilId + "' and  OW_NO='" + _ckNo + "' and PRE_ITM='" + dr["EST_ITM"].ToString() + "'";
                    Query _query = new Query();
                    _sourceDs = _query.DoSQLString(_sql);
                    _sourceDs.Tables[0].TableName = "MF_MFIN";
                    _sourceDs.Tables[1].TableName = "TF_MFIN1";

                    //MTNFin _mtnfin = new MTNFin();
                    //_sourceDs = _mtnfin.GetData("",dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilId, _ckNo);
                    DataRow[] bodyAry = _sourceDs.Tables["TF_MFIN1"].Select("PRE_ITM=" + dr["EST_ITM"].ToString());
                    if (bodyAry.Length > 0)
                    {
                        HTable = _sourceDs.Tables["MF_MFIN"];
                        bRow = bodyAry[0];
                    }
                }
                #endregion
                #region 出库
                _tblStr = "CK";
                if (_tblStr.IndexOf(_bilId) > -1)
                {
                    string _sql = "Select CK_NO,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,CUR_ID,EXC_RTO,CHK_MAN,DIS_CNT,CLSLZ,OS_ID,TAX_ID  from MF_CK"
                                    + " where CK_ID='" + _bilId + "' and CK_NO='" + _ckNo + "';";
                    _sql += " Select AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PRE_ITM,AMTN_NET,TAX,QTY,OS_ID,OS_NO,AMT From TF_CK Where CK_ID='" + _bilId + "' and  CK_NO='" + _ckNo + "' and PRE_ITM='" + dr["EST_ITM"].ToString() + "'";
                    Query _query = new Query();
                    _sourceDs = _query.DoSQLString(_sql);
                    _sourceDs.Tables[0].TableName = "MF_CK";
                    _sourceDs.Tables[1].TableName = "TF_CK";

                    DataRow[] bodyAry = _sourceDs.Tables["TF_CK"].Select("PRE_ITM=" + dr["EST_ITM"].ToString());
                    if (bodyAry.Length > 0)
                    {
                        HTable = _sourceDs.Tables["MF_CK"];
                        bRow = bodyAry[0];
                    }

                }
                  #endregion
                if (_sourceDs == null)
                    return;
                decimal _rAmtn_Net = 0;
                decimal _rTax = 0;
                decimal _rAmt=0;

                //if (HTable.Columns.Contains("VOH_ID"))
                //   dr["VOH_ID"] = HTable.Rows[0]["VOH_ID"]; //传票模版
                if (_sourceDs.Tables.Contains("TF_BLN"))
                {
                    _rAmtn_Net = ConvertDecimal(bRow, "AMTN_RNT_NET");
                    _rTax = ConvertDecimal(bRow, "TAX_RNT");
                    if (string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CUR_ID"].ToString()))
                        _rAmt = ConvertDecimal(bRow, "AMTN_RNT_NET") + ConvertDecimal(bRow, "TAX_RNT");
                    else
                        if ((dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TAX_ID"].ToString() == "3") &&
                                     (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CUR_ID"].ToString()))
                            )
                            _rAmt = ConvertDecimal(bRow, "AMT_RNT") + (ConvertDecimal(bRow, "AMT_RNT") * TaxRatio(bRow) / 100);
                        else
                            _rAmt = ConvertDecimal(bRow, "AMT_RNT");

                }
                else
                    if (_sourceDs.Tables.Contains("TF_PSS") || _sourceDs.Tables.Contains("TF_EXP") || _sourceDs.Tables.Contains("TF_MFIN1")
                          || _sourceDs.Tables.Contains("TF_CK"))
                    {
                        _rAmtn_Net = ConvertDecimal(bRow, "AMTN_NET");
                        _rTax = ConvertDecimal(bRow, "TAX");

                        if (string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CUR_ID"].ToString()))
                            _rAmt = ConvertDecimal(bRow, "AMTN_NET") + ConvertDecimal(bRow, "TAX");
                        else
                            if ((dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TAX_ID"].ToString() == "3") &&
                                         (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CUR_ID"].ToString()))
                                )
                                _rAmt = ConvertDecimal(bRow, "AMT") + (ConvertDecimal(bRow, "AMT") * TaxRatio(dr) / 100);
                            else
                                _rAmt = ConvertDecimal(bRow, "AMT");

                    }
                    else
                        if (_sourceDs.Tables.Contains("TF_IC") || _sourceDs.Tables.Contains("TF_TJ") )
                        {
                            if ((_bilId == "IB") || (_bilId == "IM" && dr["IMTAG"].ToString() == "O"))
                                _rAmtn_Net = ConvertDecimal(bRow, "CST");
                            else
                                _rAmtn_Net = ConvertDecimal(bRow, "AMTN_NET");
                            _rTax = 0;
                            _rAmt = _rAmtn_Net;

                        }
                if (_bilId == "SA" || _bilId == "CK")
                { 
                    decimal _TempDis_Cnt = ConvertDecimal(HTable.Rows[0],"Dis_Cnt");
                    if (_TempDis_Cnt < 0)
                        _TempDis_Cnt = 100 + _TempDis_Cnt;
                    if (_TempDis_Cnt != 0 && HTable.Rows[0]["OS_NO"].ToString().IndexOf("-") == -1)
                    {
                        _TempDis_Cnt = _TempDis_Cnt / 100;
                        _rAmtn_Net = _rAmtn_Net * _TempDis_Cnt;
                        _rTax = _rTax * _TempDis_Cnt;
                        _rAmt = _rAmt * _TempDis_Cnt;
                    }
                }
                int PreNo=1;
                string backStr="SB,SD,IB,PD,PB";
                if (
                     (dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VERSION"].ToString() == "1") &&
                     ((backStr.IndexOf(_bilId) > -1) || (_bilId == "IM" && dr["IMTAG"].ToString() == "O"))
                    )
                    PreNo = -1;
                string fAMT_FP="";
                string fAMTN_NET_FP="";
                string fTAX_FP="";
                string fQTY_FP="";
                if ((_bilId == "IO") || (_bilId == "IM" && dr["IMTAG"].ToString() == "I"))
                {
                    fAMT_FP = "AMT_FP2";
                    fAMTN_NET_FP = "AMTN_NET_FP2";
                    fTAX_FP = "TAX_FP2";
                    fQTY_FP = "QTY_FP2";
                }
                else
                { 
                        fAMT_FP="AMT_FP";
                        fAMTN_NET_FP="AMTN_NET_FP";
                        fTAX_FP     ="TAX_FP";
                        fQTY_FP     ="QTY_FP";
                }
                dr["AMTN_NET_CK"] = PreNo * _rAmtn_Net;
                dr["AMT_CK"] = PreNo * _rAmt;
                dr["TAX_CK"] = PreNo * _rTax;
                if (_bilId !="ER")
                    dr["QTY_CK"] = PreNo* ConvertDecimal(bRow,"QTY");
                if (!_sourceDs.Tables.Contains("MF_TJ"))
                {
                    if (!_sourceDs.Tables.Contains("MF_IC"))
                    {
                        if (_bilId == "ER")
                            dr["EXC_RTO"] = ConvertDecimal(bRow, "EXC_RTO");
                        else
                            dr["EXC_RTO"] = ConvertDecimal(HTable.Rows[0], "EXC_RTO");
                        
                    }
                    if (!_sourceDs.Tables.Contains("MF_MFIN"))
                    {
                        dr["CAS_NO"] = HTable.Rows[0]["CAS_NO"].ToString();
                        dr["TASK_ID"] = HTable.Rows[0]["TASK_ID"].ToString();

                    }

                }
                dr["AMTN_NET_OLD"] = ConvertDecimal(dr,"AMTN_NET");
                dr["AMT_OLD"] = ConvertDecimal(dr,"AMT");
                dr["TAX_OLD"] = ConvertDecimal(dr,"TAX");
                if (_bilId!="ER")
                    dr["QTY_OLD"] = ConvertDecimal(dr, "QTY");
                if (string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["CHK_MAN"].ToString()))
                {
                    dr["AMTN_NET_CLS"] = PreNo * ConvertDecimal(bRow, fAMTN_NET_FP);
                    dr["AMT_CLS"] = PreNo * ConvertDecimal(bRow, fAMT_FP);
                    dr["TAX_CLS"] = PreNo * ConvertDecimal(bRow, fTAX_FP);
                    if (_bilId != "ER")
                        dr["QTY_CLS"] = PreNo * ConvertDecimal(bRow, fQTY_FP);

                }
                else
                { 
                    dr["AMTN_NET_CLS"] = PreNo * ConvertDecimal(bRow, fAMTN_NET_FP)- ConvertDecimal(dr, "AMTN_NET_OLD");
                    dr["AMT_CLS"] = PreNo * ConvertDecimal(bRow, fAMT_FP)- ConvertDecimal(dr, "AMT_OLD");
                    dr["TAX_CLS"] = PreNo * ConvertDecimal(bRow, fTAX_FP)- ConvertDecimal(dr, "TAX_OLD");
                    if (_bilId != "ER")
                        dr["QTY_CLS"] = PreNo * ConvertDecimal(bRow, fQTY_FP) - ConvertDecimal(dr, "QTY_OLD");
                }
                
          
                dr["AMTN_NET_NEW"] = ConvertDecimal(dr,"AMTN_NET_CK") - ConvertDecimal(dr,"AMTN_NET_CLS")- ConvertDecimal(dr,"AMTN_NET");
                dr["AMT_NEW"] = ConvertDecimal(dr,"AMT_CK")-ConvertDecimal(dr,"AMT_CLS")-ConvertDecimal(dr,"AMT");
                dr["TAX_NEW"] = ConvertDecimal(dr,"TAX_CK")-ConvertDecimal(dr,"TAX_CLS")-ConvertDecimal(dr,"TAX");
                if (_bilId != "ER")
                    dr["QTY_NEW"] = ConvertDecimal(dr,"QTY_CK")-ConvertDecimal(dr,"QTY_CLS")-ConvertDecimal(dr,"QTY");

                 _tblStr = "SA,CK";
                 if (_tblStr.IndexOf(_bilId) > -1)
                 { 
                     dr["AMTN_EP"] =  GetAmtn_EPForQuery(dr["LZ_ID"].ToString(),dr["LZ_NO"].ToString(),"EP",Convert.ToInt32(dr["ITM"]));
                     dr["AMTN_ER"] =  GetAmtn_EPForQuery(dr["LZ_ID"].ToString(),dr["LZ_NO"].ToString(),"ER",Convert.ToInt32(dr["ITM"]));
                 }

            //} //foreach
        }

        private decimal GetAmtn_EPForQuery(string lzId,string lzNo, string epId, int lzItm)
        {
            DbInvIK _dbInvIk = new DbInvIK(Comp.Conn_DB);
            return _dbInvIk.GetAmtn_EPForQuery(lzId,lzNo, epId, lzItm);
        }
        #endregion

        #region 判断权限
        /// <summary>
        /// 检查单据是否可以修改
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">是否判断审核流程</param>
        /// <param name="IsRollBack"></param>
        private void SetCanModify(DataSet ds, bool bCheckAuditing, bool IsRollBack)
        {
            DataTable _dtMf = ds.Tables["MF_LZ"];
            DataTable _dtTf = ds.Tables["TF_LZ"];
            if (_dtMf.Rows.Count > 0)
            {
                bool _bCanModify = true;
                //关帐否
                if (_bCanModify && Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["LZ_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASCLOSEBILL");
                }


                if (_bCanModify && bCheckAuditing)
                {
                    string _lzID = _dtMf.Rows[0]["LZ_ID"].ToString();
                    string _lzNo = _dtMf.Rows[0]["LZ_NO"].ToString();
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_lzID, _lzNo))
                    {
                        ds.ExtendedProperties["N_MODIFY_AUDITING"] = "T";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                        _bCanModify = false;
                    }
                }
                //判断是否锁单
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                }
                //判断是否做了销货成本切制
                if (string.Compare("T", _dtMf.Rows[0]["CB_ID"].ToString()) == 0)
                {
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CB_ID");//已经切制销货成本，不能修改单据
                    _bCanModify = false;
                }
                if (_bCanModify)
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
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["BIL_NO"].ToString()))
                {
                    _bCanModify = false;
                }
                ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            }
        }
        #endregion
        #region 提供来源DataSet 前台按過濾按鍵
        ///// <summary>
        ///// 抓取
        ///// </summary>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        //public SunlikeDataSet GetDataForBil(Dictionary<string, object> parameters)
        //{
//            string _pgm = parameters["PGM"].ToString();
//            string _usr = parameters["USR"].ToString();
//            bool Chk_Up_Zero = GetProperyUpIsZero(_pgm, _usr);
            
//            DbInvIK _dbinvIK = new DbInvIK(Comp.Conn_DB);
//            _dbinvIK.UpIsZero = Chk_Up_Zero;
//            //SunlikeDataSet _ds = _dbinvIK.GetDataForBil(parameters);
//            #region  assign hash table condition
//            string _chkaud="";
//            if (parameters.ContainsKey("CHKAUD"))
//                _chkaud = parameters["CHKAUD"].ToString();
//            string _bilid = "";
//            if (parameters.ContainsKey("BIL_ID"))
//                _bilid = parameters["BIL_ID"].ToString();

//            string _chkDate = "";
//            if (parameters.ContainsKey("CHK_DATE"))
//                _chkDate = parameters["CHK_DATE"].ToString();

//            string _fDate = "";
//            if (parameters.ContainsKey("FDATE"))
//                _fDate = parameters["FDATE"].ToString();

//            string _eDate = "";
//            if (parameters.ContainsKey("EDATE"))
//                _eDate = parameters["EDATE"].ToString();

//            string _fPsNo= "";
//            if (parameters.ContainsKey("FPS_NO"))
//                _fPsNo = parameters["FPS_NO"].ToString();

//            string _ePsNo = "";
//            if (parameters.ContainsKey("EPS_NO"))
//                _ePsNo = parameters["EPS_NO"].ToString();

    

            
//            string _cusNo = "";
//            if (parameters.ContainsKey("CUS_NO"))
//                _cusNo = parameters["CUS_NO"].ToString();



//            string _incus = "";
//            if (parameters.ContainsKey("INCUS"))
//                _incus = parameters["INCUS"].ToString();

//            string _taxId = "";
//            if (parameters.ContainsKey("TAX_ID"))
//                _taxId = parameters["TAX_ID"].ToString();

//            string _turnId = "";
//            if (parameters.ContainsKey("TURN_ID"))
//                _turnId = parameters["TURN_ID"].ToString();

//            string _zhangId = "";
//            if (parameters.ContainsKey("Zhang_ID"))
//                _zhangId = parameters["Zhang_ID"].ToString();

//            string _dep = "";
//            if (parameters.ContainsKey("DEP"))
//                _dep = parameters["DEP"].ToString();

//            string _salNo = "";
//            if (parameters.ContainsKey("SAL_NO"))
//                _salNo = parameters["SAL_NO"].ToString();

//            string _curId = "";
//            if (parameters.ContainsKey("CUR_ID"))
//                _curId = parameters["CUR_ID"].ToString();

//            string _casNo = "";
//            if (parameters.ContainsKey("CAS_NO"))
//                _casNo = parameters["CAS_NO"].ToString();

//            string _taskId = "";
//            if (parameters.ContainsKey("TASK_ID"))
//                _taskId = parameters["TASK_ID"].ToString();

//            string _chkPrd = "";
//            if (parameters.ContainsKey("CHKPRD"))
//                _chkPrd = parameters["CHKPRD"].ToString();

//            string _fPrdNo = "";
//            if (parameters.ContainsKey("FPRD_NO"))
//                _fPrdNo = parameters["FPRD_NO"].ToString();


//            string _ePrdNo = "";
//            if (parameters.ContainsKey("EPRD_NO"))
//                _ePrdNo = parameters["EPRD_NO"].ToString();

//            string _chkBilType = "";
//            if (parameters.ContainsKey("CHKBilType"))
//                _chkBilType = parameters["CHKBilType"].ToString();

//            string _fBilType = "";
//            if (parameters.ContainsKey("FBILTYPE"))
//                _fBilType = parameters["FBILTYPE"].ToString();

//            string _eBilType = "";
//            if (parameters.ContainsKey("EBILTYPE"))
//                _eBilType = parameters["EBILTYPE"].ToString();

//            string _bilPgm = "";
//            if (parameters.ContainsKey("BILPGM"))
//                _bilPgm = parameters["BILPGM"].ToString();
//            string _pusr = "";
//            if (parameters.ContainsKey("USR"))
//                _pusr = parameters["USR"].ToString();
//            string _compNO = "";
//            if (parameters.ContainsKey("COMPNO"))
//                _compNO = parameters["COMPNO"].ToString();

//            string _loginDep = "";
//            if (parameters.ContainsKey("LOGINDEP"))
//                _loginDep = parameters["LOGINDEP"].ToString();
//            string _loginUpDep = "";
//            if (parameters.ContainsKey("LOGINUPDEP"))
//                _loginUpDep = parameters["LOGINUPDEP"].ToString();
//            #endregion
//            //Rights.AdvanceRights _rights = Users.GetUserRight(_usr, _pgm);
//            //string _where = Rights.GetRightsSQLWhere(_rights.Query, _rights.DeptSet, "A", Comp.CompNo, _usr, _loginDep, new Dept().GetTopDept());
//            //SunlikeDataSet _ds = _dbinvIK.GetDataForBil(_bilid,
//            //                                            _chkDate,
//            //                                            _fDate,
//            //                                            _eDate,
//            //                                            _fPsNo,
//            //                                            _ePsNo,
//            //                                            _cusNo,
//            //                                            _incus,
//            //                                            _taxId,
//            //                                            _turnId,
//            //                                            _zhangId,
//            //                                            _dep,
//            //                                            _salNo,
//            //                                            _curId,
//            //                                            _casNo,
//            //                                            _taskId,
//            //                                            _chkPrd,
//            //                                            _fPrdNo,
//            //                                            _ePrdNo,
//            //                                            _chkBilType,
//            //                                            _fBilType,
//            //                                            _eBilType,
//            //                                            _bilPgm,
//            //                                            _pusr,
//            //                                            _compNO,
//            //                                            _loginDep,
//            //                                            _loginUpDep,
//            //                                            _where,
//            //                                            _chkaud
//            //                                            );

//            DataTable _dt = CreateTmpTable();
//            _ds.Tables.Add(_dt);
//            string _cur_id = parameters["CUR_ID"].ToString();
//            string _taxID = parameters["TAX_ID"].ToString();
//            string _version = parameters["VERSION"].ToString();
//            foreach (DataRow dr in _ds.Tables["BILDATA"].Rows)
//            {
//                decimal _rAmtn_Net = 0;
//                decimal _rTax = 0;
//                decimal _rAmt = 0;
               
//                if (!string.IsNullOrEmpty(dr["AMTN_NET"].ToString()))
//                    _rAmtn_Net = Convert.ToDecimal(dr["AMTN_NET"]);
//                if (!string.IsNullOrEmpty(dr["TAX"].ToString()))
//                    _rTax = Convert.ToDecimal(dr["TAX"]);
//                if (string.IsNullOrEmpty(_cur_id))
//                    _rAmt = _rAmtn_Net + _rTax;
//                else
//                    if (_taxID == "3" && !string.IsNullOrEmpty(_cur_id))
//                    {
//                        decimal _myAmt = 0;
//                        if (!string.IsNullOrEmpty(dr["AMT"].ToString()))
//                            _myAmt = Convert.ToDecimal(dr["AMT"]);

//                        _rAmt = _myAmt + (_myAmt * Convert.ToDecimal(dr["TAX_RTO"]) / 100);
//                    }
//                    else
//                    {
//                        if (!string.IsNullOrEmpty(dr["AMT"].ToString()))
//                            _rAmt = Convert.ToDecimal(dr["AMT"]);

//                    }
//                if (dr["BIL_ID"].ToString() == "SA" || dr["BIL_ID"].ToString() == "PC" || dr["BIL_ID"].ToString() == "CK")
//                {
//                    decimal TempDis_Cnt = 0;
//                    if (!string.IsNullOrEmpty(dr["Dis_Cnt"].ToString()))
//                        TempDis_Cnt = Convert.ToDecimal(dr["Dis_Cnt"]);
//                    if (TempDis_Cnt < 0)
//                        TempDis_Cnt = 100 + TempDis_Cnt;
//                    if ((TempDis_Cnt != 0) && (dr["OS_NO"].ToString().IndexOf('-') == -1))
//                    {
//                        TempDis_Cnt = TempDis_Cnt / 100;

//                        _rAmtn_Net = _rAmtn_Net * TempDis_Cnt;
//                        _rTax = _rTax * TempDis_Cnt;
//                        _rAmt = _rAmt * TempDis_Cnt;
//                    }

//                }

//                bool CheckOK = false;
//                decimal _AMTN_NET_FP = 0;
//                decimal _TAX_FP = 0;
//                decimal _AMT_FP = 0;
//                if (!string.IsNullOrEmpty(dr["AMTN_NET_FP"].ToString()))
//                    _AMTN_NET_FP = Convert.ToDecimal(dr["AMTN_NET_FP"]);
//                if (!string.IsNullOrEmpty(dr["TAX_FP"].ToString()))
//                    _TAX_FP = Convert.ToDecimal(dr["TAX_FP"]);
//                if (!string.IsNullOrEmpty(dr["AMT_FP"].ToString()))
//                    _AMT_FP = Convert.ToDecimal(dr["AMT_FP"]);
//                if (!string.IsNullOrEmpty(_cur_id))
//                {
//                    CheckOK = ((_rAmtn_Net + _rTax > 0) && ((_rAmtn_Net + _AMTN_NET_FP + _TAX_FP) > 0)
//                               || ((_rAmtn_Net + _rTax < 0) && (_rAmtn_Net + _rTax != _AMTN_NET_FP + _TAX_FP)));
//                }
//                else
//                {
//                    CheckOK = (
//                                ((_rAmt > 0) && (_rAmt > _AMT_FP)) ||
//                                ((_rAmt < 0) && (_AMT_FP != _rAmt))

//                              );
//                }

                
///*      if (Not CheckOK) And
//         (Chk_Up_Zero) And
//         (CompareFloat(rAmtn_Net+rTax,0)=0) And
//         (CompareFloat(SurSet.FieldByName('QTY').AsFloat,SurSet.FieldByName('QTY_FP').AsFloat)<>0) then
//        CheckOK := True;*/
//                if ((!CheckOK) && (Chk_Up_Zero) &&
//                         (_rAmtn_Net + _rTax == 0) &&
//                         (ConvertDecimal(dr, "QTY") - ConvertDecimal(dr, "QTY_FP") != 0)
//                       )
//                    CheckOK = true;
//                if (!CheckOK)
//                    continue;
//                AddBodyData(dr, _ds, "1", true, "1", _rAmt, _rAmtn_Net, _rTax, ConvertDecimal(dr, "QTY"), parameters);


//            }
//            _ds.Tables.Remove("BILDATA");

//            return _ds;

//        }


        /// <summary>
        /// 判斷屬性是單價可以為零
        /// </summary>
        /// <param name="_pgm"></param>
        /// <param name="_usr"></param>
        /// <returns></returns>
        private bool GetProperyUpIsZero(string _pgm, string _usr)
        {
            Sunlike.Business.UserProperty _userProp = new UserProperty();
            return (_userProp.GetData(_usr, _pgm, "UpZeroShow").ToString().Equals("T"));

        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForBil2(Dictionary<string, object> parameters)
        {
           // string _pgm = parameters["PGM"].ToString();
           // string _usr = parameters["USR"].ToString();
           // bool Chk_Up_Zero = GetProperyUpIsZero(_pgm, _usr);

           // string _cur_id = parameters["CUR_ID"].ToString();
           // string _taxID = parameters["TAX_ID"].ToString();
           // string _version = parameters["VERSION"].ToString();
           // DbInvIK _dbinvIK = new DbInvIK(Comp.Conn_DB);
           // _dbinvIK.UpIsZero = Chk_Up_Zero;
           // #region  assign hash table condition
           // string _chkaud = "";
           // if (parameters.ContainsKey("CHKAUD"))
           //     _chkaud = parameters["CHKAUD"].ToString();
           // string _bilid = "";
           // if (parameters.ContainsKey("BIL_ID"))
           //     _bilid = parameters["BIL_ID"].ToString();

           // string _chkDate = "";
           // if (parameters.ContainsKey("CHK_DATE"))
           //     _chkDate = parameters["CHK_DATE"].ToString();

           // string _fDate = "";
           // if (parameters.ContainsKey("FDATE"))
           //     _fDate = parameters["FDATE"].ToString();

           // string _eDate = "";
           // if (parameters.ContainsKey("EDATE"))
           //     _eDate = parameters["EDATE"].ToString();

           // string _fPsNo = "";
           // if (parameters.ContainsKey("FPS_NO"))
           //     _fPsNo = parameters["FPS_NO"].ToString();

           // string _ePsNo = "";
           // if (parameters.ContainsKey("EPS_NO"))
           //     _ePsNo = parameters["EPS_NO"].ToString();

           //   string _cusNo = "";
           // if (parameters.ContainsKey("CUS_NO"))
           //     _cusNo = parameters["CUS_NO"].ToString();



           // string _incus = "";
           // if (parameters.ContainsKey("INCUS"))
           //     _incus = parameters["INCUS"].ToString();

           // string _taxId = "";
           // if (parameters.ContainsKey("TAX_ID"))
           //     _taxId = parameters["TAX_ID"].ToString();

           // string _turnId = "";
           // if (parameters.ContainsKey("TURN_ID"))
           //     _turnId = parameters["TURN_ID"].ToString();

           // string _zhangId = "";
           // if (parameters.ContainsKey("Zhang_ID"))
           //     _zhangId = parameters["Zhang_ID"].ToString();

           // string _dep = "";
           // if (parameters.ContainsKey("DEP"))
           //     _dep = parameters["DEP"].ToString();

           // string _salNo = "";
           // if (parameters.ContainsKey("SAL_NO"))
           //     _salNo = parameters["SAL_NO"].ToString();

           // string _curId = "";
           // if (parameters.ContainsKey("CUR_ID"))
           //     _curId = parameters["CUR_ID"].ToString();

           // string _casNo = "";
           // if (parameters.ContainsKey("CAS_NO"))
           //     _casNo = parameters["CAS_NO"].ToString();

           // string _taskId = "";
           // if (parameters.ContainsKey("TASK_ID"))
           //     _taskId = parameters["TASK_ID"].ToString();

           // string _chkPrd = "";
           // if (parameters.ContainsKey("CHKPRD"))
           //     _chkPrd = parameters["CHKPRD"].ToString();

           // string _fPrdNo = "";
           // if (parameters.ContainsKey("FPRD_NO"))
           //     _fPrdNo = parameters["FPRD_NO"].ToString();


           // string _ePrdNo = "";
           // if (parameters.ContainsKey("EPRD_NO"))
           //     _ePrdNo = parameters["EPRD_NO"].ToString();

           // string _chkBilType = "";
           // if (parameters.ContainsKey("CHKBilType"))
           //     _chkBilType = parameters["CHKBilType"].ToString();

           // string _fBilType = "";
           // if (parameters.ContainsKey("FBILTYPE"))
           //     _fBilType = parameters["FBILTYPE"].ToString();

           // string _eBilType = "";
           // if (parameters.ContainsKey("EBILTYPE"))
           //     _eBilType = parameters["EBILTYPE"].ToString();

           // string _bilPgm = "";
           // if (parameters.ContainsKey("BILPGM"))
           //     _bilPgm = parameters["BILPGM"].ToString();
           // string _pusr = "";
           // if (parameters.ContainsKey("USR"))
           //     _pusr = parameters["USR"].ToString();
           // string _compNO = "";
           // if (parameters.ContainsKey("COMPNO"))
           //     _compNO = parameters["COMPNO"].ToString();

           // string _loginDep = "";
           // if (parameters.ContainsKey("LOGINDEP"))
           //     _loginDep = parameters["LOGINDEP"].ToString();
           // string _loginUpDep = "";
           // if (parameters.ContainsKey("LOGINUPDEP"))
           //     _loginUpDep = parameters["LOGINUPDEP"].ToString();
           // #endregion
           // //Rights.AdvanceRights _rights = Users.GetUserRight(_usr, _pgm);
           // //string _where = Rights.GetRightsSQLWhere(_rights.Query, _rights.DeptSet, "A", Comp.CompNo, _usr, _loginDep, new Dept().GetTopDept());
           // //SunlikeDataSet _ds = _dbinvIK.GetDataForBil(_bilid,
           // //                                            _chkDate,
           // //                                            _fDate,
           // //                                            _eDate,
           // //                                            _fPsNo,
           // //                                            _ePsNo,
           // //                                            _cusNo,
           // //                                            _incus,
           // //                                            _taxId,
           // //                                            _turnId,
           // //                                            _zhangId,
           // //                                            _dep,
           // //                                            _salNo,
           // //                                            _curId,
           // //                                            _casNo,
           // //                                            _taskId,
           // //                                            _chkPrd,
           // //                                            _fPrdNo,
           // //                                            _ePrdNo,
           // //                                            _chkBilType,
           // //                                            _fBilType,
           // //                                            _eBilType,
           // //                                            _bilPgm,
           // //                                            _pusr,
           // //                                            _compNO,
           // //                                            _loginDep,
           // //                                            _loginUpDep,
           // //                                            _where,
           // //                                            _chkaud
           // //                                            );

           //// SunlikeDataSet _ds = _dbinvIK.GetDataForBil(parameters);
           // DataTable _dt = CreateTmpTable();
           // _ds.Tables.Add(_dt);
           // ArrayList _al = new ArrayList();
            
           // foreach (DataRow dr in _ds.Tables["BILDATA"].Rows)
           // { 
           //     string _bilId = dr["BIL_ID"].ToString();
           //     string _bilNo =dr["BIL_NO"].ToString();
           //     string itm = dr["ITM"].ToString();
           //     if (_bilId == "ER")
           //         _bilNo += ";"+itm;
           //     if (_al.IndexOf(_bilId + ";" + _bilNo) == -1) 
           //         _al.Add(_bilId + ";" + _bilNo);
           // }

        
           // for (int i = 0; i <= _al.Count - 1; i++)
           // {

           //      decimal  _rAmt=0;
           //      decimal  _rAmtn_Net=0;
           //      decimal  _rTax=0;
           //      decimal  _rQty=0;


           //     string[] bilIdNO = _al[i].ToString().Split(';');
           //     string _bilId = bilIdNO[0];
           //     string _bilNo = bilIdNO[1];
           //     string _itm ="";
                
           //     DataRow[] _dray =null;
             
           //     if  (_bilId!="ER")
           //         _dray = _ds.Tables["BILDATA"].Select(" BIL_ID='"+_bilId+"' and BIL_NO='"+_bilNo+"'");
           //     else
           //     {
           //         _itm = bilIdNO[2];
           //         _dray = _ds.Tables["BILDATA"].Select(" BIL_ID='" + _bilId + "' and BIL_NO='" + _bilNo + "' and itm="+_itm);
           //     }


           //     for (int pos = 0; pos <= _dray.Length - 1; pos++)
           //     {
           //          DataRow _dr = _dray[pos];
                    
           //          if (_bilId == "ER")
           //          {
           //              _rAmtn_Net = _rAmtn_Net + ConvertDecimal(_dr, "AMTN_NET");
           //              _rTax = _rTax + ConvertDecimal(_dr, "TAX");
           //              _rQty = _rQty + ConvertDecimal(_dr, "QTY");
           //              if (string.IsNullOrEmpty(_cur_id))
           //                  _rAmt = _rAmt + ConvertDecimal(_dr, "AMTN_NET") + ConvertDecimal(_dr, "TAX");
           //              else
           //                  if (_taxID == "3" && !string.IsNullOrEmpty(_cur_id))
           //                      _rAmt = _rAmt + ConvertDecimal(_dr, "AMT") + (ConvertDecimal(_dr, "AMT") * TaxRatio(_dr) / 100);
           //                  else
           //                      _rAmt = _rAmt + ConvertDecimal(_dr, "AMT");
           //          }
           //          else
           //          {
           //              _rAmtn_Net = _rAmtn_Net + ConvertDecimal(_dr,"AMTN_NET");
           //              _rTax     =_rTax+ ConvertDecimal(_dr,"TAX");
           //              _rQty     =_rQty+ConvertDecimal(_dr,"QTY");
           //              if (string.IsNullOrEmpty(_cur_id))
           //                  _rAmt = _rAmt + ConvertDecimal(_dr, "AMTN_NET") + ConvertDecimal(_dr, "TAX");
           //              else if (_taxID=="3" && !string.IsNullOrEmpty(_cur_id))
           //                  _rAmt = _rAmt + ConvertDecimal(_dr, "AMT") + (ConvertDecimal(_dr, "AMT") * TaxRatio(_dr) / 100);
           //              else
           //                  _rAmt = _rAmt + ConvertDecimal(_dr, "AMT");
           //          }
           //     }
           //     DataRow dr = _dray[_dray.Length-1];
           //     if (dr["BIL_ID"].ToString().Equals("SA") || dr["BIL_ID"].ToString().Equals("PC") || dr["BIL_ID"].ToString().Equals("CK"))
           //              {
           //                  decimal _TempDis_Cnt = ConvertDecimal(dr, "Dis_Cnt");
           //                  if (_TempDis_Cnt<0)
           //                      _TempDis_Cnt = 100 +_TempDis_Cnt;
           //                  if ((_TempDis_Cnt != 0) && (dr["OS_NO"].ToString().IndexOf('-') == -1))
           //                  {
           //                      _rAmtn_Net=_rAmtn_Net*_TempDis_Cnt;
           //                      _rTax=_rTax*_TempDis_Cnt;
           //                      _rAmt=_rAmt*_TempDis_Cnt;
           //                  }
           //              }
            
           //       bool CheckOK;
           //       if (!string.IsNullOrEmpty(_cur_id))
           //          {
           //             CheckOK = (
           //                        ((_rAmtn_Net + _rTax > 0) && (_rAmtn_Net + _rTax > ConvertDecimal(dr, "AMTN_NET_CLS") + ConvertDecimal(dr, "TAX_CLS"))) ||
           //                        ((_rAmtn_Net + _rTax < 0) && (_rAmtn_Net + _rTax != ConvertDecimal(dr, "AMTN_NET_CLS") + ConvertDecimal(dr, "TAX_CLS")))
           //                        );
           //           }
           //        else
           //          {
           //             CheckOK = (
           //                        ((_rAmt > 0) && (_rAmt > ConvertDecimal(dr, "AMT_CLS") + ConvertDecimal(dr, "TAX_CLS"))) ||
           //                               ((_rAmt < 0) && (_rAmt != ConvertDecimal(dr, "AMT_CLS"))) 
           //                             );
           //          }
                    
           //              //  if (Not CheckOK) And
           //              // (Chk_Up_Zero) And
           //              //    (CompareFloat(rAmtn_Net+rTax,0)=0) And
           //              //(CompareFloat(rQty,SurSet.FieldByName('QTY_CLS').AsFloat)<>0) then
           //              //   CheckOK := True;
           //          if ((!CheckOK) && (Chk_Up_Zero) &&
           //                (_rAmtn_Net + _rTax == 0) &&
           //                (_rQty - ConvertDecimal(dr, "QTY_CLS") != 0)
           //              )
           //              CheckOK = true;

           //          if (!CheckOK)
           //                  continue;
           //              if (!dr.Table.Columns["UP"].ReadOnly)
           //                 dr["UP"] = DBNull.Value;
           //              AddBodyData(dr, _ds, "2", true, "1", _rAmt, _rAmtn_Net, _rTax, _rQty,parameters);
                 
           //     }
           // _ds.Tables.Remove("BILDATA");
            return new SunlikeDataSet();

        }


        /// <summary>
        /// 表身新增
        /// </summary>
        /// <param name="dr">資料列</param>
        /// <param name="ds"></param>
        /// <param name="turnID"></param>
        /// <param name="IsAdd"></param>
        private void AddBodyData(DataRow dr, SunlikeDataSet ds, string turnID, bool IsAdd, string ver, decimal rAmt, decimal rAmtn_Net, decimal rTax,decimal rQty,Dictionary<string, object> ht)
        {
            int preNo = 1;
            string BackStr = "SB,SD,IB,KB,PB,PD";
            if (  (ver == "1") &&
                  ((BackStr.IndexOf(dr["BIL_ID"].ToString()) >= 0) || (dr["BIL_ID"].ToString() == "IM" && dr["IMTAG"].ToString() == "O")))
                preNo = -1;

            DataRow _destdr = ds.Tables["BILSOURCE"].NewRow();


            _destdr["YN"] = "T";
            _destdr["VOH_ID"] = dr["VOH_ID"];
            _destdr["CK_NO"] = dr["BIL_NO"];
            _destdr["BIL_ID"] = dr["BIL_ID"];

            _destdr["TAX_RTO"] = TaxRatio(dr);
            _destdr["AMT_CK"] = preNo * rAmt;
            _destdr["AMTN_NET_CK"] = preNo * rAmtn_Net;
            _destdr["TAX_CK"] = preNo * rTax;
            if (_destdr["BIL_ID"].ToString() != "ER")
                _destdr["QTY_OLD"] = 0;
            //             if not (ChkShowQty  and
            //     (Pos(SurSet.FieldByName('BIL_ID').AsString,'SD,TJ')>0)) then
         
            string _tmpStr = "'SD,TJ,PD";
//            if (_tmpStr.IndexOf(_destdr["BIL_ID"].ToString()) == -1)
  //          {
                _destdr["QTY_CK"] = preNo * rQty;
                _destdr["QTY_CLS"] = preNo * ConvertDecimal(dr, "QTY_FP") - ConvertDecimal(_destdr, "QTY_OLD");
    //        }
            _destdr["EXC_RTO"] = dr["EXC_RTO"];
            _destdr["IMTAG"] = dr["IMTAG"];
            string AmtnName = "AMTN_NET_CLS";
            string AmtName = "AMT_CLS";
            string TaxName = "TAX_CLS";
            string QtyName = "QTY_CLS";
            if (turnID == "1")
            {
                AmtnName = "AMTN_NET_FP";
                AmtName = "AMT_FP";
                TaxName = "TAX_FP";
                QtyName = "QTY_FP";
            }

            _destdr["AMTN_NET_CLS"] = preNo * ConvertDecimal(dr, AmtnName);
            _destdr["AMT_CLS"] = preNo * ConvertDecimal(dr, AmtName);
            _destdr["TAX_CLS"] = preNo * ConvertDecimal(dr, TaxName);
            if (dr.Table.Columns.Contains(QtyName))
            {
                _destdr["QTY_CLS"] = preNo * ConvertDecimal(dr, QtyName);
            }

     
            _destdr["UP"] = dr["UP"];
            _destdr["BAT_NO"] = dr["BAT_NO"];
            //if (MF_LZ_TBL.State = dsInsert) And (IsCopyAMT) then //wxl 03-5-12
            Sunlike.Business.UserProperty _userProp = new UserProperty();
            string _pgm = ht["PGM"].ToString();
            string _usr = ht["USR"].ToString();



            if (_userProp.GetData(_usr, _pgm, "IsCopyAMT").ToString().Equals("T"))
            {
                _destdr["AMTN_NET"] = ConvertDecimal(_destdr, "AMTN_NET_CK") - ConvertDecimal(_destdr, "AMTN_NET_CLS");
                _destdr["AMT"] = ConvertDecimal(_destdr, "AMT_CK") - ConvertDecimal(_destdr, "AMT_CLS");
                _destdr["TAX"] = ConvertDecimal(_destdr, "TAX_CK") - ConvertDecimal(_destdr, "TAX_CLS");
                _destdr["QTY"] = ConvertDecimal(_destdr, "QTY_CK") - ConvertDecimal(_destdr, "QTY_CLS");
                _destdr["AMTN_NET_NEW"] = 0;
                _destdr["AMT_NEW"] = 0;
                _destdr["TAX_NEW"] = 0;
                
            }
            else
            {

                _destdr["AMTN_NET_NEW"] = ConvertDecimal(_destdr, "AMTN_NET_CK") - ConvertDecimal(_destdr, "AMTN_NET_CLS");
                _destdr["AMT_NEW"] = ConvertDecimal(_destdr, "AMT_CK") - ConvertDecimal(_destdr, "AMT_CLS");
                _destdr["TAX_NEW"] = ConvertDecimal(_destdr, "TAX_CK") - ConvertDecimal(_destdr, "TAX_CLS");
            }
            _destdr["PAY_DD"] = dr["PAY_DD"];
            _destdr["CUS_NO"] = dr["CUS_NO"];
            _destdr["CAS_NO"] = dr["CAS_NO"];
            _destdr["TASK_ID"] = dr["TASK_ID"];
            if (turnID == "1")
            {
                _destdr["PRD_NO"] = dr["PRD_NO"];
                _destdr["PRD_NAME"] = dr["PRD_NAME"];
                _destdr["PRD_MARK"] = dr["PRD_MARK"];
                _destdr["EST_ITM"] = dr["PRE_ITM"];
                _destdr["BIL_ID"] = dr["BIL_ID"];
                _destdr["CHK_TAX"] = dr["CHK_TAX"];
                _destdr["SUP_PRD_NO"] = dr["SUP_PRD_NO"];
                _destdr["CUS_OS_NO"] = dr["CUS_OS_NO"];
                _destdr["UNIT"] = dr["UNIT"];
                _destdr["REM1"] = dr["REM1"];
                if (_destdr["BIL_ID"].ToString() == "SA")
                {
                    _destdr["AMTN_EP"] = dr["AMTN_EP"];
                    _destdr["AMTN_ER"] = dr["AMTN_ER"];
                }
            
            }
            else
            {
                _destdr["PRD_NO"]= "";
                if (dr["BIL_ID"].ToString() == "ER")
                    _destdr["EST_ITM"] = Convert.ToInt32(dr["PRE_ITM"]);
                else
                    _destdr["EST_ITM"] = 0;
                if (_destdr["BIL_ID"].ToString() == "SA")
                {
                    _destdr["AMTN_EP"] = dr["AMTN_EP1"];
                    _destdr["AMTN_ER"] = dr["AMTN_ER1"];
                }
            }
            if (!string.IsNullOrEmpty(_destdr["PRD_NO"].ToString()))
            {
                string _spc = "";
                Prdt _prdt = new Prdt();
                DataTable _dtPrdt1 = _prdt.GetPrdt(_destdr["PRD_NO"].ToString());
                if (_dtPrdt1.Rows.Count > 0)
                {
                    if (_dtPrdt1.Rows[0]["SPC"] != DBNull.Value)
                    {
                        _spc = _dtPrdt1.Rows[0]["SPC"].ToString();
                        _destdr["SPC"] = _spc;
                    }
                }
            }

            _destdr["REM"] = dr["REM"];

            ds.Tables["BILSOURCE"].Rows.Add(_destdr);



        }



        private decimal ConvertDecimal(DataRow dr,string FieldName)
        {
            if (!string.IsNullOrEmpty(dr[FieldName].ToString()))
                return Convert.ToDecimal(dr[FieldName]);
            else
                return 0;
        }
        private decimal ConvertDecimal(DataRow dr, string FieldName,bool IsADD)
        {
            if (!IsADD)
            {
                if (!string.IsNullOrEmpty(dr[FieldName,DataRowVersion.Original].ToString()))
                    return Convert.ToDecimal(dr[FieldName, DataRowVersion.Original]);
                else
                    return 0;
            }
            else
            return ConvertDecimal(dr, FieldName);
        }
        private DataTable CreateTmpTable()
        {
            DataTable _dt = new DataTable("BILSOURCE");
            _dt.Columns.Add(new DataColumn("YN"));
            _dt.Columns.Add(new DataColumn("CK_NO")); //来源单号
            _dt.Columns.Add(new DataColumn("AMT_CK", typeof(decimal)));  //应开金额
            _dt.Columns.Add(new DataColumn("AMT_CLS", typeof(decimal)));  //已开金额
            _dt.Columns.Add(new DataColumn("AMT_NEW", typeof(decimal)));  //结馀金额
            _dt.Columns.Add(new DataColumn("AMTN_EP", typeof(decimal))); //费用支出
            _dt.Columns.Add(new DataColumn("AMTN_ER", typeof(decimal))); //费用收入
            _dt.Columns.Add(new DataColumn("AMTN_NET", typeof(decimal))); //本次未税金额
            _dt.Columns.Add(new DataColumn("AMTN_NET_CK", typeof(decimal))); //应开未税金额
            _dt.Columns.Add(new DataColumn("AMTN_NET_CLS", typeof(decimal))); //已开未税金额
            _dt.Columns.Add(new DataColumn("AMTN_NET_NEW", typeof(decimal))); //结余未税金额
            _dt.Columns.Add(new DataColumn("QTY", typeof(decimal))); //本次数量

            _dt.Columns.Add(new DataColumn("QTY_CK", typeof(decimal))); //应开数量
            _dt.Columns.Add(new DataColumn("QTY_CLS", typeof(decimal))); //已开数量
            _dt.Columns.Add(new DataColumn("QTY_OLD", typeof(decimal))); 
            _dt.Columns.Add(new DataColumn("REM")); //备注
            

            _dt.Columns.Add(new DataColumn("TAX", typeof(decimal))); //本次税额
            _dt.Columns.Add(new DataColumn("TAX_CK", typeof(decimal))); //应开税额
            _dt.Columns.Add(new DataColumn("TAX_CLS", typeof(decimal))); //已开税额
            _dt.Columns.Add(new DataColumn("TAX_NEW", typeof(decimal))); //结余税额
            _dt.Columns.Add(new DataColumn("REM1")); //
            _dt.Columns.Add(new DataColumn("CHK_TAX")); //

            _dt.Columns.Add(new DataColumn("BAT_NO")); //批号
            _dt.Columns.Add(new DataColumn("PRD_MARK")); //特征

            _dt.Columns.Add(new DataColumn("AMT", typeof(decimal))); //本次金额

            _dt.Columns.Add(new DataColumn("CUS_NO")); //客户代号
            _dt.Columns.Add(new DataColumn("CUS_OS_NO")); //客户订单号
            _dt.Columns.Add(new DataColumn("SPC")); //货品规格
            _dt.Columns.Add(new DataColumn("PRD_NAME")); //品名
            _dt.Columns.Add(new DataColumn("PRD_NO")); //品名
            _dt.Columns.Add(new DataColumn("MRK")); //品牌
            _dt.Columns.Add(new DataColumn("SUP_PRD_NO")); //对方货号
            _dt.Columns.Add(new DataColumn("BIL_ID")); //对方货号



            _dt.Columns.Add(new DataColumn("PAY_DD", typeof(DateTime))); //来源单号
            _dt.Columns.Add(new DataColumn("UNIT")); //单位
            
            _dt.Columns.Add(new DataColumn("UP", typeof(decimal))); //单价
            _dt.Columns.Add(new DataColumn("TAX_RTO", typeof(decimal)));
            _dt.Columns.Add(new DataColumn("EXC_RTO", typeof(decimal)));

            _dt.Columns.Add(new DataColumn("EST_ITM", typeof(int))); //单价
            _dt.Columns.Add(new DataColumn("CAS_NO"));
            _dt.Columns.Add(new DataColumn("TASK_ID"));
            _dt.Columns.Add(new DataColumn("IMTAG"));
            _dt.Columns.Add(new DataColumn("VOH_ID"));

            return _dt;


        }
        #endregion
      

        private void UpdateMfArp(DataRow dr)
        {
            this.UpdateMfArp(dr, true);
        }
        private void UpdateMfArp(DataSet ds)
        {
            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_LZ") && ds.Tables["MF_LZ"].Rows.Count > 0 && ds.Tables["MF_LZ"].Rows[0].RowState == System.Data.DataRowState.Deleted)
            {
                this.UpdateMfArp(ds.Tables["MF_LZ"].Rows[0]);
            }

        }
        /// <summary>
        /// 更新后台资料
        /// </summary>
        /// <param name="ChangedDS"></param>
        /// <returns></returns>
        public DataTable UpdateData(DataSet ChangedDS)
        {
            string _psID, _usr;
            DataRow _dr = ChangedDS.Tables["MF_LZ"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _psID = _dr["LZ_ID", DataRowVersion.Original].ToString();
                _usr = _dr["USR", DataRowVersion.Original].ToString();

            }
            else
            {
                _psID = _dr["LZ_ID"].ToString();
                _usr = _dr["USR"].ToString();

            }
            if (ChangedDS.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", ChangedDS.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_LZ"] = "LZ_ID,LZ_NO,LZ_DD,PAY_DD,CUS_NO,INV_NO,CUR_ID,Exc_RTO," +
                           "REM,USR,CHK_MAN,PRT_SW,CPY_SW,AMT,AMTN_NET,TAX,DEP,VOH_ID," +
                           "VOH_NO,ARP_NO,CLS_DATE,SAL_NO,TAX_ID,PAY_MTH,PAY_DAYS,CHK_DAYS,INT_DAYS," +
                           "CHK_DD,PAY_REM,CLS_REM,ZHANG_ID,VERSION,BAT_NO,MOB_ID,LOCK_MAN," +
                           "CB_ID,FJ_NUM,SYS_DATE,BIL_ID,BIL_NO,TURN_ID,VOH_CHK,CAS_NO,TASK_ID," +
                           "RP_NO,PRT_USR,BIL_TYPE,ZC_FLAG,QC_SW";
            _ht["TF_LZ"] = "LZ_ID,LZ_NO,ITM,CK_NO,AMT,AMTN_NET,TAX,PRD_NO,EST_ITM,PAY_DD," +
                            "BIL_ID,TAX_RTO,PRD_MARK,QTY,UP,COMPOSE_IDNO,BAT_NO,CUS_OS_NO,SUP_PRD_NO," +
                            "UNIT,REM,IC_ITM,TJ_ITM,IMTAG,PRD_NAME,CUS_NO";
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
            //_isRunAuditing = _auditing.IsRunAuditing(_psID, _usr,_bilType,_mobID);


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
                    string _pgm = "INVIK";
                    switch (_psID)
                    {
                        case "LO":
                            _pgm = "INVIK";
                            break;
                        case "LZ":
                            _pgm = "INVLZ";
                            break;
                        case "LI":
                            _pgm = "INVLI";
                            break;
                    }
                    DataTable _dtMf = ChangedDS.Tables["MF_LZ"];
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
            }

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
            string _lzID = "";
            string _lzNO = "";
            string _usr = "";

            //是否重建凭证号码
          
            #region 表头部份
            if (tableName == "MF_LZ")
            {
                _state = statementType;
                if (dr.RowState != DataRowState.Deleted)
                {
                    _lzID = dr["LZ_ID"].ToString();
                    _lzNO = dr["LZ_NO"].ToString();
                    _usr = dr["USR"].ToString();
                }
                else
                {
                    _lzID = dr["LZ_ID", DataRowVersion.Original].ToString();
                    _lzNO = dr["LZ_NO", DataRowVersion.Original].ToString();
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                }

                Auditing _auditing = new Auditing();
                if (_auditing.GetIfEnterAuditing(_lzID, _lzNO))//如果进去审核了就不能修改和新增删除
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");  //resource
                }
                //新增时判断关账日期
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["LZ_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                    {
                        throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["LZ_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                    {
                        throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                //检查资料正确否
                if (statementType != StatementType.Delete)
                {
                    //检查销货客户
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString(), Convert.ToDateTime(dr["LZ_DD"])))
                    {
                        dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//客户代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    Salm _salm = new Salm();
                    if (!String.IsNullOrEmpty(dr["SAL_NO"].ToString()) && !_salm.IsExist(_usr, dr["SAL_NO"].ToString(), Convert.ToDateTime(dr["LZ_DD"])))
                    {
                        dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["LZ_DD"])))
                    {
                        dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEP_NOTEXIST,PARAM=" + dr["DEP"].ToString());//部门代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }


                }
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //取得保存单号
                    dr["LZ_NO"] = _sq.Set(_lzID, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["LZ_DD"]), dr["BIL_TYPE"].ToString());
                    //写入默认栏位值
                    dr["PRT_SW"] = "N";
                    //如果没有输入交易方式时，取客户默认交易方式
                    if (String.IsNullOrEmpty(dr["PAY_MTH"].ToString()))
                    {
                        Cust _cust = new Cust();
                        System.Collections.Hashtable _ht = _cust.GetPAYInfo(dr["CUS_NO"].ToString(), dr["LZ_DD"].ToString());
                        if (_ht != null)
                        {
                            dr["PAY_DD"] = _ht["PAY_DD"];
                            dr["CHK_DD"] = _ht["CHK_DD"];
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
                    string _error = _sq.Delete(dr["LZ_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
                    }
                    this.UpdateVohNo(dr, statementType);
                }
                if (dr.RowState != DataRowState.Deleted)
                {
                    //写输单日期
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);                    
                    if (!_isRunAuditing)
                    {                                    
                        //产生凭证           
                        // this.UpdateVohNo(dr, statementType);
                        if (_lzID == "LZ")  //立帐开立发票
                        {
                            this.UpdateMfArp(dr, false);//因预收款要取得该立账单故放此   处
                        }             
                    }
                    #region 预收款


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
              

                    if (_amtnRcv != 0)
                    {
                        UpdateMon(dr);
                    }
                    else if (_amtnRcv == 0 && !string.IsNullOrEmpty(dr["RP_NO"].ToString()))
                    {
                        Bills _bills = new Bills();
                        _bills.DelRcvPay("1", dr["RP_NO"].ToString());
                    }
                    #endregion
                    //try
                    //{
                    //    DrpTaxAa _drpTaxAa = new DrpTaxAa();
                    //    _drpTaxAa.UpdateTaxAa(dr.Table.DataSet, _lzID, _lzNO);
                    //}
                    //catch (Exception _ex)
                    //{
                    //    dr.SetColumnError("INV_NO", _ex.Message);
                    //    status = UpdateStatus.SkipAllRemainingRows;
                    //}
                 
                }
                //if (!this._isRunAuditing)
                //{
                //    this.UpdateVohNo(dr, statementType);      
                //}

                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["LZ_DD"]);
                //    _aps.BIL_ID = dr["LZ_ID"].ToString();
                //    _aps.BIL_NO = dr["LZ_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //    _aps = new AudParamStruct(Convert.ToString(dr["LZ_ID", DataRowVersion.Original]), Convert.ToString(dr["LZ_NO", DataRowVersion.Original]));
                //_auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
            #endregion
            else if (tableName == "TF_LZ" && dr.RowState!= DataRowState.Deleted)
            {
           
                _usr = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString();
                Prdt _prdt = new Prdt();
                if (dr["BIL_ID"].ToString() != "ER" && dr["BIL_ID"].ToString() != "SD")
                {
                    //检查货品代号
                    if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["LZ_DD"])) && dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID"].ToString()=="1")
                    {
                        dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + dr["PRD_NO"].ToString());//货品代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
                if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) &&
                    !string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VOH_ID"].ToString())
                    )
                {
                    throw new SunlikeException("RCID=INV.INVIK.ACCVOHID,PARAM=" + dr["CK_NO"].ToString());
                }

            }
           


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            if (ds.Tables.Contains("TF_LZ_EP"))
            { return; }

            //#region 单据追踪
            //DataTable _dt = ds.Tables["MF_LZ"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["LZ_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["LZ_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion
            //#region TF_LZ_EP
            //if (!_isRunAuditing)
            //{
            //    if (ds.Tables["MF_LZ"].Rows[0].RowState == DataRowState.Deleted)
            //    {
            //        DataRow dr = ds.Tables["MF_LZ"].Rows[0];
            //        DbInvLI _dbinvLI = new DbInvLI(Comp.Conn_DB);
            //        _dbinvLI.DeleteToTF_LZ_EP(dr["LZ_ID", DataRowVersion.Original].ToString(), dr["LZ_NO", DataRowVersion.Original].ToString());

            //    }
            //    else
            //    {
            //        foreach (DataRow dr in ds.Tables["TF_LZ"].Rows)
            //        {
            //            string _turnId = "";
            //            string _bilID;
            //            string _bilNo;

            //            if (dr.RowState == DataRowState.Deleted)
            //            {
            //                _turnId = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID", DataRowVersion.Original].ToString();
            //                _bilID = dr["BIL_ID", DataRowVersion.Original].ToString();
            //                _bilNo = dr["CK_NO", DataRowVersion.Original].ToString();

            //            }
            //            else
            //            {
            //                _turnId = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID"].ToString();
            //                _bilID = dr["BIL_ID"].ToString();
            //                _bilNo = dr["CK_NO"].ToString();

            //            }

            //            if (dr.RowState == DataRowState.Added)
            //            {
            //                AppendToTF_LZ_EP(dr, _bilID, _bilNo, _turnId);
            //            }
            //            else
            //                if (dr.RowState == DataRowState.Modified)
            //                {
            //                    DbInvLI _dbinvLI = new DbInvLI(Comp.Conn_DB);
            //                    _dbinvLI.DeleteToTF_LZ_EP(dr["LZ_ID"].ToString(), dr["LZ_NO"].ToString(), Convert.ToInt32(dr["ITM"].ToString()));

            //                    AppendToTF_LZ_EP(dr, _bilID, _bilNo, _turnId);
            //                }
            //        }



            //    }
            //}


            // #endregion

            if (ds.Tables["MF_LZ"].Rows.Count > 0 && ds.Tables["MF_LZ"].Rows[0].RowState == DataRowState.Deleted)
            {
                Bills _bills = new Bills();  //by zb 2007-05-14
                if (!string.IsNullOrEmpty(ds.Tables["MF_LZ"].Rows[0]["RP_NO", DataRowVersion.Original].ToString()))
                    _bills.DelRcvPay("1", ds.Tables["MF_LZ"].Rows[0]["RP_NO", DataRowVersion.Original].ToString());
            }
            this.UpdateMfArp(ds);
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
            _mon.RpNo = dr["RP_NO"].ToString();
            if (!string.IsNullOrEmpty(dr["LZ_DD"].ToString()))
                _mon.RpDd = Convert.ToDateTime(dr["LZ_DD"].ToString());
            if (dr["PAY_MTH"].ToString() == "3") //现金收款
                _mon.JsfId = "T";
            _mon.BilId = dr["LZ_ID"].ToString();
            _mon.BilNo = dr["LZ_NO"].ToString();
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

            //#region 其他
            //if (string.IsNullOrEmpty(dr["TAX_IRP"].ToString()) && string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
            //{
            //    _mon.AmtOther = 0;
            //    _mon.AmtnOther = 0;
            //    //该属性为TRUE生成TF_MON3的表身
            //    _mon.AddMon3 = false;
            //}
            //else
            //{
            //    decimal _amtTaxIrp = 0;
            //    decimal _amtnTaxIrp = 0;
            //    decimal _amtCbac = 0;
            //    decimal _amtnCbac = 0;
            //    bool _idxTax = false;
            //    Cust _cust = new Cust();
            //    DataTable _custDt = _cust.GetData(_mon.CusNo);
            //    if (_custDt.Rows.Count > 0
            //        && _custDt.Rows[0]["ID2_TAX"].ToString() == "T"
            //        )
            //    {
            //        _idxTax = true;
            //    }
            //    CompInfo _compInfo = Comp.GetCompInfo("");
            //    int _poiTax = _compInfo.DecimalDigitsInfo.System.POI_TAX;
            //    if (_idxTax && !string.IsNullOrEmpty(_mon.CurId))
            //    {
            //        if (!string.IsNullOrEmpty(dr["TAX_IRP"].ToString()))
            //        {
            //            _amtTaxIrp = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()), _poiTax);
            //            _amtnTaxIrp = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()) * _mon.ExcRto, _poiTax);
            //        }
            //        if (!string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
            //        {
            //            _amtCbac = Math.Round(Convert.ToDecimal(dr["AMTN_CBAC"].ToString()), _poiTax);
            //            _amtnCbac = Math.Round(Convert.ToDecimal(dr["AMTN_CBAC"].ToString()) * _mon.ExcRto, _poiTax);
            //        }

            //    }
            //    else
            //    {
            //        if (!string.IsNullOrEmpty(dr["TAX_IRP"].ToString()))
            //        {
            //            _amtnTaxIrp = Math.Round(Convert.ToDecimal(dr["TAX_IRP"].ToString()), _poiTax);
            //            if (_mon.ExcRto != 1)
            //            {
            //                _amtTaxIrp = Math.Round((Convert.ToDecimal(dr["TAX_IRP"].ToString()) / _mon.ExcRto), _poiTax);
            //            }
            //            else
            //                _amtTaxIrp = 0;
            //        }
            //        if (!string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
            //        {
            //            _amtnCbac = Math.Round(Convert.ToDecimal(dr["AMTN_CBAC"].ToString()), _poiTax);
            //            if (_mon.ExcRto != 1)
            //            {
            //                _amtCbac = Math.Round((Convert.ToDecimal(dr["AMTN_CBAC"].ToString()) / _mon.ExcRto), _poiTax);
            //            }
            //            else
            //                _amtCbac = 0;
            //        }
            //    }
            //    //取销货单凭证模版
            //    string _accNoTaxIrp = "";
            //    string _accNoCbac = "";
            //    DrpVoh _voh = new DrpVoh();
            //    DataSet _dsVhId = _voh.GetVhId(dr["PS_ID"].ToString(), dr["VOH_ID"].ToString());
            //    if (_dsVhId.Tables.Contains("TF_VHID"))
            //    {
            //        if (_dsVhId.Tables["TF_VHID"].Select("ITM='10' AND DC='C'").Length > 0)
            //        {
            //            //取客户储值会计科目
            //            _accNoCbac = _dsVhId.Tables["TF_VHID"].Select("ITM='10' AND DC='C'")[0]["ACC_NO"].ToString();
            //        }
            //        if (_dsVhId.Tables["TF_VHID"].Select("ITM='9' AND DC='D'").Length > 0)
            //        {
            //            //取销收输入科目
            //            _accNoTaxIrp = _dsVhId.Tables["TF_VHID"].Select("ITM='9' AND DC='D'")[0]["ACC_NO"].ToString();
            //        }
            //    }
            //    DataTable _dtMon3 = new DataTable();
            //    _dtMon3.Columns.Add("ACC_NO", typeof(System.String));
            //    _dtMon3.Columns.Add("AMT", typeof(System.String));
            //    _dtMon3.Columns.Add("AMTN", typeof(System.String));
            //    DataRow _drMon3 = null;
            //    if (_amtnTaxIrp != 0)
            //    {
            //        _drMon3 = _dtMon3.NewRow();
            //        _drMon3["ACC_NO"] = _accNoTaxIrp;
            //        _drMon3["AMT"] = _amtTaxIrp;
            //        _drMon3["AMTN"] = _amtnTaxIrp;
            //        _dtMon3.Rows.Add(_drMon3);
            //    }
            //    if (_amtnCbac != 0)
            //    {
            //        _drMon3 = _dtMon3.NewRow();
            //        _drMon3["ACC_NO"] = _accNoCbac;
            //        _drMon3["AMT"] = _amtCbac;
            //        _drMon3["AMTN"] = _amtnCbac;
            //        _dtMon3.Rows.Add(_drMon3);
            //    }
            //    _mon.TfMon3 = _dtMon3;

            //    _mon.AmtOther = _amtTaxIrp;
            //    _mon.AmtnOther = _amtnTaxIrp;
            //    _mon.AmtOther += _amtCbac;
            //    _mon.AmtnOther += _amtnCbac;

            //    //该属性为TRUE生成TF_MON3的表身
            //    if (_mon.AmtnOther != 0)
            //        _mon.AddMon3 = true;
            //}
            //#endregion

            //#region 预收款
            //if (string.IsNullOrEmpty(dr["AMT_IRP"].ToString()))
            //    _mon.AmtIrp = 0;
            //else
            //    _mon.AmtIrp = Convert.ToDecimal(dr["AMT_IRP"].ToString());
            //if (string.IsNullOrEmpty(dr["AMTN_IRP"].ToString()))
            //{
            //    _mon.AmtnIrp = 0;
            //    _mon.AddMon1 = false;
            //}
            //else
            //{
            //    _mon.AmtnIrp = Convert.ToDecimal(dr["AMTN_IRP"].ToString());
            //    if (_mon.AmtnIrp != 0)
            //        _mon.AddMon1 = true;
            //    _mon.TfMon1 = dr.Table.DataSet.Tables["TF_MON1"];

            //}
            //#endregion

            _mon.Amtn = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
            _mon.AmtnCls = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
            //判断收款金额是否大于表身金额
            if (dr.RowState != DataRowState.Deleted)
            {
                DataRow[] _drSel = dr.Table.DataSet.Tables["TF_LZ"].Select();
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
            _mon.AddTcMon = string.IsNullOrEmpty(dr["CHK_MAN"].ToString()) ? false : true;
            _mon.ArpNo = dr["ARP_NO"].ToString();
            string _rpNo = _bills.AddRcvPay(_mon);
            dr["RP_NO"] = _rpNo;
            return _rpNo;

        }
        private void UpdateMfArp(DataRow dr, bool updateArpNo)
        {
            dr = dr.Table.DataSet.Tables["MF_LZ"].Rows[0];
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_LZ"];

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
            }
                Arp _arp = new Arp();
                string _arpNo = "";

                //修改和删除操作前判断立账单是否可删除
                if (dr.RowState == DataRowState.Deleted)
                {
                    _arpNo = dr["ARP_NO", DataRowVersion.Original].ToString();
                    //取得本单据收款单信息
                    Bills _bills = new Bills();
                    string _rpId = "1";
                  
                    string _rpNo = "";
                    _rpNo = dr["RP_NO", DataRowVersion.Original].ToString();
                    SunlikeDataSet _dsMon = _bills.GetData(_rpId, _rpNo, false);
                    if (!_arp.DeleteMfArp(_arpNo, _dsMon.Tables["TF_MON"], false))
                    {
                        throw new SunlikeException("无法删除立帐单:" + _arpNo);
                    }
                }
                if (dr.RowState != DataRowState.Deleted && _amt != 0)
                {
                    decimal _excRto = 1;
                    if (!String.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                        _excRto = Convert.ToDecimal(dr["EXC_RTO"]);
                    //_arpNo = _arp.UpdateMfArp("1", "2", dr["LZ_ID"].ToString(), dr["LZ_NO"].ToString(), Convert.ToDateTime(dr["LZ_DD"]), dr["BIL_TYPE"].ToString(),
                    //    dr["DEP"].ToString(), dr["USR"].ToString(), dr["CUR_ID"].ToString(), _excRto, _amtn + _tax, _amtn, _amt, _tax, dr, dr["REM"].ToString(),dr["INV_NO"].ToString());
                    if (_arpNo != dr["ARP_NO"].ToString())
                    {
                        if (updateArpNo)
                        {
                            DbInvIK _dbInvik = new DbInvIK(Comp.Conn_DB);
                            _dbInvik.UpdateArpNo(dr["LZ_ID"].ToString(), dr["LZ_NO"].ToString(), _arpNo, _amtn, _tax, _amt);
                        }
                        dr["ARP_NO"] = _arpNo;
                    }
                    dr["AMTN_NET"] = _amtn;
                    dr["TAX"] = _tax;
                    dr["AMT"] = _amt;

                }


            

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr">MF_LZ行</param>
        /// <param name="statementType"></param>
        /// <returns></returns>
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
                string _psId = dr["LZ_ID"].ToString();
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
                        string _bilId = _psId;
                        if (string.Compare("2", dr["VOH_CHK"].ToString()) == 0)
                        {
                            _bilId = "L4";
                        }
                        else if (string.Compare("3", dr["VOH_CHK"].ToString()) == 0)
                        {
                            _bilId = "L5";
                        }
                        if (string.Compare("LZ", _psId) == 0 || string.Compare("LO", _psId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.SA_INVOICE;
                        }

                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _psId, dr["LZ_NO"].ToString()), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;

                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _bilId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        string _bilId = _psId;
                        if (string.Compare("2", dr["VOH_CHK"].ToString()) == 0)
                        {
                            _bilId = "L4";
                        }
                        else if (string.Compare("3", dr["VOH_CHK"].ToString()) == 0)
                        {
                            _bilId = "L5";
                        }
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("LZ", _psId) == 0 || string.Compare("LO", _psId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.SA_INVOICE;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _psId, dr["LZ_NO"].ToString()), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _bilId, out _vohNoError);
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
                string _psId = dr["LZ_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                string _bilId = _psId;
                if (string.Compare("2", dr["VOH_CHK"].ToString()) == 0)
                {
                    _bilId = "L4";
                }
                else if (string.Compare("3", dr["VOH_CHK"].ToString()) == 0)
                {
                    _bilId = "L5";
                }
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("LZ", _psId) == 0 || string.Compare("LO", _psId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.SA_INVOICE;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _bilId, out _vohNoError);
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
        #region 更新进货立帐开票凭证号码
        /// <summary>
        /// 更新进货立帐开票凭证号码
        /// </summary>
        /// <param name="lzId"></param>
        /// <param name="lzNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string lzId, string lzNo, string vohNo)
        {

            DbInvIK _ik = new DbInvIK(Comp.Conn_DB);
            _ik.UpdateVohNo(lzId, lzNo, vohNo);
        }
        #endregion

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
            string _lz_ID = "";
            if (dr.RowState != DataRowState.Deleted)
                _lz_ID = dr["LZ_ID"].ToString();
            else
            {
                _lz_ID = dr["LZ_ID", DataRowVersion.Original].ToString();
                if (tableName == "MF_LZ")
                {
                    Query _query = new Query();
                    string sqlstr = "delete from INV_NO Where INV_NO='" + dr["INV_NO", DataRowVersion.Original].ToString() + "'";
                    _query.RunSql(sqlstr);
                }
            }
            //判断是否走审核流程
            if (!_isRunAuditing)
            {
                if (tableName == "MF_LZ")
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        DbInvIK _dbinvIK = new DbInvIK(Comp.Conn_DB);
                        _dbinvIK.DeleteToTF_LZ_EP(dr["LZ_ID"].ToString(), dr["LZ_NO"].ToString());
                        
                    }
                    else
                    {
                        DbInvLI _dbinvLI = new DbInvLI(Comp.Conn_DB);
                        _dbinvLI.DeleteToTF_LZ_EP(dr["LZ_ID", DataRowVersion.Original].ToString(), dr["LZ_NO", DataRowVersion.Original].ToString());

                    }
                }
                else
                    if (tableName == "TF_LZ")
                    {
                        string _turnId="";
                        string _bilID;
                        string _bilNo;
                        if (dr.RowState == DataRowState.Deleted)
                        {
                            _turnId = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID", DataRowVersion.Original].ToString();
                            _bilID = dr["BIL_ID", DataRowVersion.Original].ToString();
                            _bilNo = dr["CK_NO", DataRowVersion.Original].ToString();
                        }
                        else
                        {
                            _turnId = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID"].ToString();
                            _bilID = dr["BIL_ID"].ToString();
                            _bilNo = dr["CK_NO"].ToString();

                        }
                        if (_turnId == "1" || _bilID=="ER")
                        {
                            if (statementType == StatementType.Insert)
                            {
                                this.UpdateBILAMTN_NET_FP(dr, true);
                                AppendToTF_LZ_EP(dr, _bilID, _bilNo, _turnId);
                            }
                            else
                                if (statementType == StatementType.Update)
                                {
                                    this.UpdateBILAMTN_NET_FP(dr, false);
                                    this.UpdateBILAMTN_NET_FP(dr, true);
                                    DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);
                                    _dbinvIk.DeleteToTF_LZ_EP(dr["LZ_ID"].ToString(), dr["LZ_NO"].ToString(), Convert.ToInt32(dr["ITM"].ToString()));

                                    AppendToTF_LZ_EP(dr, _bilID, _bilNo, _turnId);

                                }
                                else if (statementType == StatementType.Delete)
                                {
                                    this.UpdateBILAMTN_NET_FP(dr, false);
                                }
                        }
                        else
                        {
                            if (statementType == StatementType.Insert)
                            {
                                this.UpdateBILAMTN_NET_FPHead(dr, true);
                                AppendToTF_LZ_EP(dr, _bilID, _bilNo, _turnId);
                            }
                            else
                            if (statementType == StatementType.Update)
                            {
                                this.UpdateBILAMTN_NET_FPHead(dr, false);
                                this.UpdateBILAMTN_NET_FPHead(dr, true);
                                DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);
                                _dbinvIk.DeleteToTF_LZ_EP(dr["LZ_ID"].ToString(), dr["LZ_NO"].ToString(), Convert.ToInt32(dr["ITM"].ToString()));

                                AppendToTF_LZ_EP(dr, _bilID, _bilNo, _turnId);
                            }
                            else
                                if (statementType == StatementType.Delete)
                                {
                                    this.UpdateBILAMTN_NET_FPHead(dr, false);
               
                                }

                        }
                    }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            if (ds.Tables.Contains("TF_LZ_EP"))
            { return; }

            if (_state != StatementType.Delete)
            {
                DataRow dr = ds.Tables["MF_LZ"].Rows[0];
                
                if (!this._isRunAuditing)
                {
                    string _vohNo = "";
                    if (_state == StatementType.Insert)
                        _vohNo = this.UpdateVohNo(dr, StatementType.Insert);
                    else
                        _vohNo = this.UpdateVohNo(dr, StatementType.Update);

                    //string bil_id = dr["LZ_ID"].ToString();
                    //string bil_no = dr["LZ_NO"].ToString();
                    //if (!string.IsNullOrEmpty(_vohNo))
                    //    this.UpdateVohNo(bil_id, bil_no, _vohNo);


                }
            }
            
        }
       
        #region 更新来源单发票资讯

        private void UpdateBILAMTN_NET_FPHead(DataRow dr, bool IsAdd)
        {
            int _preNo2 = 1;
            string _osId = "";
            string _osNo = "";
            string _sver = "";
            string _imtag = "";
            int _preNo = 1;
            string _lz_no = "";
            string _inv_no = "";
            string _usr = "";
            string _vohId = "";
            if (IsAdd)
            {
                _sver = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VERSION"].ToString();
                _osId = dr["BIL_ID"].ToString();
                
                _osNo = dr["CK_NO"].ToString();
                _imtag = dr["IMTAG"].ToString();
                _lz_no = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["LZ_NO"].ToString();
                _inv_no = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["INV_NO"].ToString();
                _usr = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString();
                _vohId = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VOH_ID"].ToString();
            }
            else
            {
                _sver = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VERSION", DataRowVersion.Original].ToString();
                _osId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _osNo = dr["CK_NO", DataRowVersion.Original].ToString();
                _imtag = dr["IMTAG", DataRowVersion.Original].ToString();
                _preNo = -1;
                _lz_no = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["LZ_NO", DataRowVersion.Original].ToString();
                _inv_no = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["INV_NO", DataRowVersion.Original].ToString();
                _usr = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR",DataRowVersion.Original].ToString();
                _vohId = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VOH_ID", DataRowVersion.Original].ToString();
            }
            #region 栏位定义
            string fAMT_FP = "";
            string fAMTN_NET_FP = "";
            string fTAX_FP = "";
            string fQTY_FP = "";
            string fAMT_CLS = "";
            string fAMTN_NET_CLS = "";
            string fTAX_CLS = "";
            string fQTY_CLS = "";
            string fLZ_CLS_ID = "";
            string fTURN_ID = "";
            if ((_osId == "IO") || ((_osId == "IM") && (_imtag == "I")))
            {
                fAMT_FP = "AMT_FP2";
                fAMTN_NET_FP = "AMTN_NET_FP2";
                fTAX_FP = "TAX_FP2";
                fQTY_FP = "QTY_FP2";
                fAMT_CLS = "AMT_CLS2";
                fAMTN_NET_CLS = "AMTN_NET_CLS2";
                fTAX_CLS = "TAX_CLS2";
                fQTY_CLS = "QTY_CLS2";
                fLZ_CLS_ID = "LZ_CLS_ID2";
                fTURN_ID = "TURN_ID2";
            }
            else
            {
                fAMT_FP = "AMT_FP";
                fAMTN_NET_FP = "AMTN_NET_FP";
                fTAX_FP = "TAX_FP";
                fQTY_FP = "QTY_FP";
                fAMT_CLS = "AMT_CLS";
                fAMTN_NET_CLS = "AMTN_NET_CLS";
                fTAX_CLS = "TAX_CLS";
                fQTY_CLS = "QTY_CLS";
                fLZ_CLS_ID = "LZ_CLS_ID";
                fTURN_ID = "TURN_ID";
                string bilStr = "OD,OW";
                if (bilStr.IndexOf(_osId) > -1)
                    fLZ_CLS_ID = "CLS_LZ_ID";
            }
            #endregion
            string _backStr = "SB,SD,IB,PB,PD,TC,DT,KB";
            if ((_sver == "1") &&
                 ((_backStr.IndexOf(_osId) > -1) || (_osId == "IM" && _imtag == "0"))
               )
                _preNo2 = -1;
            SunlikeDataSet _ds = null;
            string mfPSSStr = "SA,SB,SD";
            DataRow mfdr = null;
            DataTable bodydt = null;
            if (mfPSSStr.IndexOf(_osId) > -1)
            {
                DRPSA _drpsa = new DRPSA();
                if (IsAdd)
                    _ds = _drpsa.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _osId, _osNo,false,false);
                else
                    _ds = _drpsa.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR", DataRowVersion.Original].ToString(), _osId, _osNo,false,false);
                mfdr = _ds.Tables["MF_PSS"].Rows[0];
                bodydt = _ds.Tables["TF_PSS"];
            }
            #region 借出,借出還入單据
            string mfBilLnStr = "LN,LB";
            if (mfBilLnStr.IndexOf(_osId) > -1)
            {
                DRPBN _drpbn = new DRPBN();
                if (IsAdd)
                    _ds = _drpbn.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _osId, _osNo, false);
                else
                    _ds = _drpbn.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR", DataRowVersion.Original].ToString(), _osId, _osNo, false);
                mfdr = _ds.Tables["MF_BLN"].Rows[0];
                bodydt = _ds.Tables["TF_BLN"];
            }
            #endregion
            #region 配送單
            mfBilLnStr = "IO,IB";

            if (mfBilLnStr.IndexOf(_osId) > -1)
            {
                DRPIO _drpio = new DRPIO();
                
                if (IsAdd)
                    _ds = _drpio.GetData(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _osId, _osNo, false);
                else
                    _ds = _drpio.GetData(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR", DataRowVersion.Original].ToString(), _osId, _osNo, false);
                if (_ds.Tables["MF_IC"].Rows.Count == 0)
                    return;
                mfdr = _ds.Tables["MF_IC"].Rows[0];
                bodydt = _ds.Tables["TF_IC"];
                
            }

                #endregion
            #region 调价单
            mfBilLnStr = "TJ";

            if (mfBilLnStr.IndexOf(_osId) > -1)
            {
                DRPTJ _drptj = new DRPTJ();

                _ds = _drptj.GetData( _osNo, false);
                if (_ds.Tables["MF_TJ"].Rows.Count == 0)
                    return;
                mfdr = _ds.Tables["MF_TJ"].Rows[0];
                bodydt = _ds.Tables["TF_TJ"];

            }

                #endregion
            #region 出库单
            mfBilLnStr = "CK";
            if (mfBilLnStr.IndexOf(_osId) > -1)
            {
                string _sql = "Select CK_NO,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,CUR_ID,EXC_RTO,CHK_MAN,DIS_CNT,CLSLZ,OS_ID,TAX_ID  from MF_CK"
                                          + " where CK_ID='" + _osId + "' and CK_NO='" + _osNo + "';";
                _sql += " Select AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PRE_ITM,AMTN_NET,TAX,QTY,OS_ID,OS_NO,AMT From TF_CK Where CK_ID='" + _osId + "' and  CK_NO='" + _osNo + "'";
                Query _query = new Query();
                _ds = _query.DoSQLString(_sql);
                _ds.Tables[0].TableName = "MF_CK";
                _ds.Tables[1].TableName = "TF_CK";
                if (_ds.Tables["MF_CK"].Rows.Count == 0)
                    return;
                mfdr = _ds.Tables["MF_CK"].Rows[0];
                bodydt = _ds.Tables["TF_CK"];

            }
            #endregion
            if (IsAdd)
            {
                if (!string.IsNullOrEmpty(_vohId) && !string.IsNullOrEmpty(mfdr["VOH_ID"].ToString()))
                {
                    throw new SunlikeException("RCID=INV.INVIK.ACCVOHID,PARAM=" + dr["CK_NO"].ToString());
                }
            }

            //--------------加其他類型的來源單

            //---------------
            mfdr[fAMTN_NET_CLS] = ConvertDecimal(mfdr, fAMTN_NET_CLS) + _preNo2 * _preNo * ConvertDecimal(dr, "AMTN_NET",IsAdd);
            mfdr[fAMT_CLS] = ConvertDecimal(mfdr, fAMT_CLS) + _preNo2 * _preNo * ConvertDecimal(dr, "AMT", IsAdd);
            mfdr[fTAX_CLS] = ConvertDecimal(mfdr, fTAX_CLS) + _preNo2 * _preNo * ConvertDecimal(dr, "TAX", IsAdd);
            mfdr[fQTY_CLS] = ConvertDecimal(mfdr, fQTY_CLS) + _preNo2 * _preNo * ConvertDecimal(dr, "QTY", IsAdd);
            if (!IsAdd)
            {
                if (ConvertDecimal(mfdr, fAMTN_NET_CLS) < 0)
                    mfdr[fAMTN_NET_CLS] = 0;
                if (ConvertDecimal(mfdr, fAMT_CLS) < 0)
                    mfdr[fAMT_CLS] = 0;
                if (ConvertDecimal(mfdr, fTAX_CLS) < 0)
                    mfdr[fTAX_CLS] = 0;
                if (ConvertDecimal(mfdr, fQTY_CLS) < 0)
                    mfdr[fQTY_CLS] = 0;
            }
            if (IsAdd)
            {
                if (mfdr.Table.DataSet.Tables.Contains("MF_PSS"))
                {
                    if ((_osId == "SA" || _osId == "PC") && (string.IsNullOrEmpty(mfdr["ACC_FP_NO"].ToString())))
                        mfdr["ACC_FP_NO"] = _lz_no;
                }
                else if (mfdr.Table.DataSet.Tables.Contains("MF_CK") && mfdr["OS_ID"].ToString() == "SA")
                {
                    DbInvIK _dbInvIk = new DbInvIK(Comp.Conn_DB);
                    _dbInvIk.UpdateInv_noForMFPss(_lz_no, _osNo);
                }
                if (needCls2(mfdr, bodydt, dr, fAMTN_NET_CLS, fTAX_CLS, fAMT_CLS))
                {
                    mfdr[fLZ_CLS_ID] = "T";
                    string _biltbl = "SA,CK,KB,PC";
                    if (_biltbl.IndexOf(_osId) > -1)
                        mfdr["CLSLZ"] = "T";
                    _biltbl = "OD,OW";
                    if (_biltbl.IndexOf(_osId) > -1)
                        mfdr["CLS_LZ_AUTO"] = "T";
                }
                mfdr[fTURN_ID] = "2";
                string _nouseInvNo = "IO,IB,TJ,CK";
                if (_nouseInvNo.IndexOf(_osId) == -1)
                    mfdr["INV_NO"] = _inv_no;
            }
            else
            {
                if (mfdr.Table.DataSet.Tables.Contains("MF_PSS"))
                {
                    string _tblstr = "SA,PC";
                    if (_tblstr.IndexOf(_osId) > -1 && (mfdr["ACC_FP_NO"].ToString() == _lz_no))
                        mfdr["ACC_FP_NO"] = "";
                }
                else
                    if (mfdr.Table.DataSet.Tables.Contains("MF_CK") && mfdr["OS_ID"].ToString() == "SA")
                    {
                        DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);
                        _dbinvIk.UpdateMFPssforACC_FP_NO(_lz_no, _osNo);
                    }
                mfdr[fLZ_CLS_ID] = "F";
                string _osStr = "SA,CK,KB,PC";
                if (_osStr.IndexOf(_osId) > -1)
                    mfdr["CLSLZ"] = "F";
                _osStr = "OD,OW";
                if (_osStr.IndexOf(_osId) > -1)
                    mfdr["CLS_LZ_AUTO"] = "F";

                mfdr[fTURN_ID] = "";
                string _nouseInvNo = "IO,IB,TJ,CK";
                if (_nouseInvNo.IndexOf(_osId) == -1)
                   mfdr["INV_NO"] = "";

            }
            if (mfPSSStr.IndexOf(_osId) > -1)
            {
                DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);

                if (IsAdd)
                    _dbinvIk.UpdateMFARPINVNO(mfdr["PS_ID"].ToString(), mfdr["PS_NO"].ToString(), "", mfdr["INV_NO"].ToString());
                else
                    _dbinvIk.UpdateMFARPINVNO(mfdr["PS_ID"].ToString(), mfdr["PS_NO"].ToString(), "D", mfdr["INV_NO"].ToString());
                DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
                _dbSa.UpdateInvIkHeadData(mfdr["TURN_ID"].ToString(), mfdr["LZ_CLS_ID"].ToString(), mfdr["CLSLZ"].ToString(),
                    mfdr["ACC_FP_NO"].ToString(), mfdr["INV_NO"].ToString(), ConvertDecimal(mfdr, "AMT_CLS"), ConvertDecimal(mfdr, "AMTN_NET_CLS"),
                    ConvertDecimal(mfdr, "TAX_CLS"), ConvertDecimal(mfdr, "QTY_CLS"), mfdr["PS_ID"].ToString(), mfdr["PS_NO"].ToString());
            }
            #region 更新借出,借還入資訊
            string tblstr = "LN,LB";
            if (tblstr.IndexOf(_osId) > -1)
            {
                
                DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);

                if (IsAdd)
                    _dbinvIk.UpdateMFARPINVNO(mfdr["BL_ID"].ToString(), mfdr["BL_NO"].ToString(), "", mfdr["INV_NO"].ToString());
                else
                    _dbinvIk.UpdateMFARPINVNO(mfdr["BL_ID"].ToString(), mfdr["BL_NO"].ToString(), "D", mfdr["INV_NO"].ToString());
                DbDRPBN _dbBn = new DbDRPBN(Comp.Conn_DB);
                _dbBn.UpdateInvIkHeadData(mfdr["TURN_ID"].ToString(), mfdr["LZ_CLS_ID"].ToString(), mfdr["INV_NO"].ToString(), ConvertDecimal(mfdr, "AMT_CLS"), ConvertDecimal(mfdr, "AMTN_NET_CLS"),
                    ConvertDecimal(mfdr, "TAX_CLS"), ConvertDecimal(mfdr, "QTY_CLS"), mfdr["BL_ID"].ToString(), mfdr["BL_NO"].ToString());
            }

            #endregion
            #region 配送单,配送退回
            tblstr = "IO,IB";
            if (tblstr.IndexOf(_osId) > -1)
            {
                DbDRPIO _dbIo = new DbDRPIO(Comp.Conn_DB);
                _dbIo.UpdateInvIkHeadData(mfdr["TURN_ID"].ToString(), mfdr["LZ_CLS_ID"].ToString(),
                    ConvertDecimal(mfdr, "AMT_CLS"), ConvertDecimal(mfdr, "AMTN_NET_CLS"), ConvertDecimal(mfdr, "TAX_CLS"), ConvertDecimal(mfdr, "QTY_CLS"),
                    mfdr["TURN_ID2"].ToString(), mfdr["LZ_CLS_ID2"].ToString(),
                    ConvertDecimal(mfdr, "AMT_CLS2"), ConvertDecimal(mfdr, "AMTN_NET_CLS2"), ConvertDecimal(mfdr, "TAX_CLS2"), ConvertDecimal(mfdr, "QTY_CLS2"),
                    mfdr["IC_ID"].ToString(), mfdr["IC_NO"].ToString());
            }
            #endregion
            #region 调价单
            tblstr = "TJ";
            if (tblstr.IndexOf(_osId) > -1)
            {
        
                DbDRPTJ _dbTj = new DbDRPTJ(Comp.Conn_DB);

                _dbTj.UpdateInvIkHeadData(mfdr["TURN_ID"].ToString(), mfdr["LZ_CLS_ID"].ToString(), ConvertDecimal(mfdr, "AMT_CLS"), ConvertDecimal(mfdr, "AMTN_NET_CLS"),
                    ConvertDecimal(mfdr, "TAX_CLS"), ConvertDecimal(mfdr, "QTY_CLS"), mfdr["TJ_NO"].ToString());

            }
            #endregion
            #region 托外缴回单
            tblstr = "CK";
            if (tblstr.IndexOf(_osId) > -1)
            {
                DbInvIK _dbinvIK = new DbInvIK(Comp.Conn_DB);
                _dbinvIK.UpDateMFCKHead2(mfdr, _osNo, _osId); //滙总合并

            }
             #endregion
        }


        /// <summary> 
        /// 更新来源单发票资讯
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="IsAdd">新增</param>
        private void UpdateBILAMTN_NET_FP(DataRow dr, bool IsAdd)
        {
            string _bilID = "";
            string _bilNO = "";
            string _est_ITM = "";
            string _turn_ID = "";
            string _inv_no = "";
            string _sver = "";
            int _preNo2 = 1;

            string _imtag = "";
            string _lzNo = "";
            string _usr = "";
            string _vohId = "";
            if (IsAdd)
            {
                _sver = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VERSION"].ToString();
                _bilID = dr["BIL_ID"].ToString();
                _bilNO = dr["CK_NO"].ToString();
                _est_ITM = dr["EST_ITM"].ToString();
                _turn_ID = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID"].ToString();
                _inv_no = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["INV_NO"].ToString();
                _imtag = dr["IMTAG"].ToString();
                _lzNo = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["LZ_NO"].ToString();
                _usr = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString();
                _vohId = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VOH_ID"].ToString();
            }
            else
            {
                _bilID = dr["BIL_ID", DataRowVersion.Original].ToString();
                _bilNO = dr["CK_NO", DataRowVersion.Original].ToString();
                _est_ITM = dr["EST_ITM", DataRowVersion.Original].ToString();
                _turn_ID = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID", DataRowVersion.Original].ToString();
                _inv_no = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["INV_NO", DataRowVersion.Original].ToString();
                _sver = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VERSION", DataRowVersion.Original].ToString();
                _imtag = dr["IMTAG", DataRowVersion.Original].ToString();
                _lzNo = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["LZ_NO", DataRowVersion.Original].ToString();
                _usr = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR", DataRowVersion.Original].ToString();
                _vohId = dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["VOH_ID", DataRowVersion.Original].ToString();
            }
            #region 栏位定义
            string fAMT_FP = "";
            string fAMTN_NET_FP = "";
            string fTAX_FP = "";
            string fQTY_FP = "";
            string fAMT_CLS = "";
            string fAMTN_NET_CLS = "";
            string fTAX_CLS = "";
            string fQTY_CLS = "";
            string fLZ_CLS_ID = "";
            string fTURN_ID = "";
            if ((_bilID == "IO") || ((_bilID == "IM") && (_imtag == "I")))
            {
                fAMT_FP = "AMT_FP2";
                fAMTN_NET_FP = "AMTN_NET_FP2";
                fTAX_FP = "TAX_FP2";
                fQTY_FP = "QTY_FP2";
                fAMT_CLS = "AMT_CLS2";
                fAMTN_NET_CLS = "AMTN_NET_CLS2";
                fTAX_CLS = "TAX_CLS2";
                fQTY_CLS = "QTY_CLS2";
                fLZ_CLS_ID = "LZ_CLS_ID2";
                fTURN_ID = "TURN_ID2";
            }
            else
            {
                fAMT_FP = "AMT_FP";
                fAMTN_NET_FP = "AMTN_NET_FP";
                fTAX_FP = "TAX_FP";
                fQTY_FP = "QTY_FP";
                fAMT_CLS = "AMT_CLS";
                fAMTN_NET_CLS = "AMTN_NET_CLS";
                fTAX_CLS = "TAX_CLS";
                fQTY_CLS = "QTY_CLS";
                fLZ_CLS_ID = "LZ_CLS_ID";
                fTURN_ID = "TURN_ID";
                string bilStr = "OD,OW";
                if (bilStr.IndexOf(_bilID) > -1)
                    fLZ_CLS_ID = "CLS_LZ_ID";
            }
            #endregion
            string _backStr = "SB,SD,IB,PB,PD,TC,DT,KB";
            if ((_sver == "1") &&
                 ((_backStr.IndexOf(_bilID) > -1) || (_bilID == "IM" && _imtag == "0"))
               )
                _preNo2 = -1;
            int _preNo = IsAdd ? 1 : -1;
            SunlikeDataSet _ds = null;
            string mfPSSStr = "SA,SB,SD,PC,PB,PD";
            DataRow mfdr = null;
            DataRow bodydr = null;
            DataTable bodyDt = null;
            #region  銷貨退折單据
            if (mfPSSStr.IndexOf(_bilID) > -1)
            {
                DRPSA _drpsa = new DRPSA();
                DataRow[] bodyAry;
                if (IsAdd)
                {
                    _ds = _drpsa.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilID, _bilNO,false,false);
                    bodyAry = _ds.Tables["TF_PSS"].Select("PRE_ITM=" + dr["EST_ITM"].ToString());

                }
                else
                {
                    _ds = _drpsa.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR", DataRowVersion.Original].ToString(), _bilID, _bilNO,false,false);
                    bodyAry = _ds.Tables["TF_PSS"].Select("PRE_ITM=" + dr["EST_ITM", DataRowVersion.Original].ToString());

                }
                if (_ds.Tables["MF_PSS"].Rows.Count == 0)
                    return;
                mfdr = _ds.Tables["MF_PSS"].Rows[0];
                bodyDt = _ds.Tables["TF_PSS"];
                if (bodyAry.Length > 0)
                    bodydr = bodyAry[0];

            }
            #endregion
            #region 借出,借出還入單据
            string mfBilLnStr = "LN,LB";
            if (mfBilLnStr.IndexOf(_bilID) > -1)
            {
                DRPBN _drpbn = new DRPBN();
                DataRow[] bodyAry;
                if (IsAdd)
                {
                    _ds = _drpbn.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilID, _bilNO, false);
                    bodyAry = _ds.Tables["TF_BLN"].Select("PRE_ITM=" + dr["EST_ITM"].ToString());

                }
                else
                {
                    _ds = _drpbn.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR", DataRowVersion.Original].ToString(), _bilID, _bilNO, false);
                    bodyAry = _ds.Tables["TF_BLN"].Select("PRE_ITM=" + dr["EST_ITM", DataRowVersion.Original].ToString());

                }
                if (_ds.Tables["MF_BLN"].Rows.Count == 0)
                    return;
                mfdr = _ds.Tables["MF_BLN"].Rows[0];
                bodyDt = _ds.Tables["TF_BLN"];
                if (bodyAry.Length > 0)
                    bodydr = bodyAry[0];
            }
            #endregion
            #region 其他費用入
            mfBilLnStr = "ER";
            if (mfBilLnStr.IndexOf(_bilID) > -1)
            {
                MonEX _monex = new MonEX();
                DataRow[] bodyAry;
                if (IsAdd)
                {
                    _ds = _monex.GetData( _bilID, _bilNO, false);
                    bodyAry = _ds.Tables["TF_EXP"].Select("KEY_ITM=" + dr["EST_ITM"].ToString());

                }
                else
                {
                    _ds = _monex.GetData( _bilID, _bilNO, false);
                    bodyAry = _ds.Tables["TF_EXP"].Select("KEY_ITM=" + dr["EST_ITM", DataRowVersion.Original].ToString());

                }
                mfdr = _ds.Tables["MF_EXP"].Rows[0];
                bodyDt = _ds.Tables["TF_EXP"];
                if (bodyAry.Length > 0)
                    bodydr = bodyAry[0];
            }
            #endregion
            #region 配送單
            mfBilLnStr = "IO;IB";
            if (mfBilLnStr.IndexOf(_bilID) > -1)
            {
                DRPIO _drpio = new DRPIO();
                DataRow[] bodyAry;
                if (IsAdd)
                {
                    _ds = _drpio.GetData(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), _bilID, _bilNO, false);
                    bodyAry = _ds.Tables["TF_IC"].Select("KEY_ITM=" + dr["EST_ITM"].ToString());

                }
                else
                {
                    _ds = _drpio.GetData(dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR", DataRowVersion.Original].ToString(), _bilID, _bilNO, false);
                    bodyAry = _ds.Tables["TF_IC"].Select("KEY_ITM=" + dr["EST_ITM", DataRowVersion.Original].ToString());

                }
                if (_ds.Tables["MF_IC"].Rows.Count == 0)
                    return;
                mfdr = _ds.Tables["MF_IC"].Rows[0];
                bodyDt = _ds.Tables["TF_IC"];
                if (bodyAry.Length > 0)
                    bodydr = bodyAry[0];
            }

            #endregion
            #region 配送單
            mfBilLnStr = "TJ";
            if (mfBilLnStr.IndexOf(_bilID) > -1)
            {
                DRPTJ _drptj = new DRPTJ();
                DataRow[] bodyAry;
                if (IsAdd)
                {
                    _ds = _drptj.GetData(_bilNO, false);
                    bodyAry = _ds.Tables["TF_TJ"].Select("KEY_ITM=" + dr["EST_ITM"].ToString());

                }
                else
                {
                    _ds = _drptj.GetData( _bilNO, false);
                    bodyAry = _ds.Tables["TF_TJ"].Select("KEY_ITM=" + dr["EST_ITM", DataRowVersion.Original].ToString());

                }
                if (_ds.Tables["MF_TJ"].Rows.Count == 0)
                    return;
                mfdr = _ds.Tables["MF_TJ"].Rows[0];
                bodyDt = _ds.Tables["TF_TJ"];
                if (bodyAry.Length > 0)
                    bodydr = bodyAry[0];
            }

               #endregion
            #region  出库單
            mfPSSStr = "CK";
            if (mfPSSStr.IndexOf(_bilID) > -1)
            {
                string _sql = "Select CK_NO,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,CUR_ID,EXC_RTO,CHK_MAN,DIS_CNT,CLSLZ,OS_ID,TAX_ID  from MF_CK"
                                + " where CK_ID='" + _bilID + "' and CK_NO='" + _bilNO + "';";
                _sql += " Select AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PRE_ITM,AMTN_NET,TAX,QTY,OS_ID,OS_NO,AMT From TF_CK Where CK_ID='" + _bilID + "' and  CK_NO='" + _bilNO + "'";
                Query _query = new Query();
                _ds = _query.DoSQLString(_sql);
                _ds.Tables[0].TableName = "MF_CK";
                _ds.Tables[1].TableName = "TF_CK";
                DataRow[] bodyAry;
                mfdr = _ds.Tables["MF_CK"].Rows[0];
                if (IsAdd)
                {
                    bodyAry = _ds.Tables["TF_CK"].Select("PRE_ITM=" + dr["EST_ITM"].ToString());
                }
                else
                {
                    bodyAry = _ds.Tables["TF_CK"].Select("PRE_ITM=" + dr["EST_ITM", DataRowVersion.Original].ToString());
                }
                bodyDt = _ds.Tables["TF_CK"];
                if (bodyAry.Length > 0)
                    bodydr = bodyAry[0];

            }
             #endregion

            if (IsAdd)
            {
                if (!string.IsNullOrEmpty(_vohId) && mfdr.Table.Columns.Contains("VOH_ID") && !string.IsNullOrEmpty(mfdr["VOH_ID"].ToString()))
                {
                    throw new SunlikeException("RCID=INV.INVIK.ACCVOHID,PARAM=" + dr["CK_NO"].ToString());
                }
            }

            if (bodydr != null)
            {

                bodydr[fAMTN_NET_FP] = ConvertDecimal(bodydr, fAMTN_NET_FP) + _preNo * _preNo2 * ConvertDecimal(dr, "AMTN_NET", IsAdd);
                bodydr[fAMT_FP] = ConvertDecimal(bodydr, fAMT_FP) + _preNo * _preNo2 * ConvertDecimal(dr, "AMT", IsAdd);
                bodydr[fTAX_FP] = ConvertDecimal(bodydr, fTAX_FP) + _preNo * _preNo2 * ConvertDecimal(dr, "TAX", IsAdd);

                string _tblstr = "'EP,ER";
                if (_tblstr.IndexOf(_bilID) < 0)
                    bodydr[fQTY_FP] = ConvertDecimal(bodydr, fQTY_FP) + _preNo * _preNo2 * ConvertDecimal(dr, "QTY", IsAdd);
                string _mfexpStr = "EP,ER";
                if (!IsAdd)
                {
                    if (ConvertDecimal(bodydr, fAMTN_NET_FP) < 0)
                        bodydr[fAMTN_NET_FP] = 0;
                    if (ConvertDecimal(bodydr, fAMT_FP) < 0)
                        bodydr[fAMT_FP] = 0;
                    if (ConvertDecimal(bodydr, fTAX_FP) < 0)
                        bodydr[fTAX_FP] = 0;

                    if (_mfexpStr.IndexOf(_bilID) == -1)
                        if (ConvertDecimal(bodydr, fQTY_FP) < 0)
                            bodydr[fQTY_FP] = 0;

                }
                #region 其他费用收入判断
                if (_mfexpStr.IndexOf(_bilID) > -1)
                {
                    if (IsAdd)
                    {
                        if (NeedClsForExp(bodydr, fAMTN_NET_FP, fTAX_FP, fAMT_FP))
                        {
                            bodydr[fLZ_CLS_ID] = "T";
                            bodydr["CLSLZ"] = "T";
                        }
                    }
                    else
                    {
                        bodydr[fLZ_CLS_ID] = "F";
                        bodydr["CLSLZ"] = "F";

                    }
                }
                #endregion

            }
            string _bilstr = "'EP,ER";
            if (_bilstr.IndexOf(_bilID) < 0)
            {
                if (IsAdd)
                {
                    if (mfdr.Table.DataSet.Tables.Contains("MF_PSS"))
                    {
                        string _tblStr = "SA,PC";
                        if (_tblStr.IndexOf(_bilID) > -1 && (string.IsNullOrEmpty(mfdr["ACC_FP_NO"].ToString())))
                            mfdr["ACC_FP_NO"] = _lzNo;

                    }
                    else
                        if (bodydr.Table.DataSet.Tables.Contains("TF_CK") && bodydr["OS_ID"].ToString() == "SA")
                        {
                            DRPSA _drpsa = new DRPSA();
                            SunlikeDataSet _saDataSet = _drpsa.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR"].ToString(), bodydr["OS_ID"].ToString(), bodydr["OS_NO"].ToString(),false,false);
                            if (_saDataSet.Tables["MF_PSS"].Rows.Count > 0)
                            {
                                DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
                                if (IsAdd)
                                {
                                    _dbSa.UpdateMFPSSACC_FP_NO(_lzNo, _inv_no, _saDataSet.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString(), _saDataSet.Tables["MF_PSS"].Rows[0]["PS_NO"].ToString());
                                }
                                else
                                {
                                    if (_saDataSet.Tables["MF_PSS"].Rows[0]["ACC_FP_NO"].ToString() == _lzNo)
                                        _dbSa.UpdateMFPSSINV_NO(_inv_no, _saDataSet.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString(), _saDataSet.Tables["MF_PSS"].Rows[0]["PS_NO"].ToString());
                                }
                            }

                        }
                    if (NeedCls1(mfdr, bodyDt, _bilID, _bilNO, fAMTN_NET_FP, fTAX_FP, fAMT_FP))
                    {
                        mfdr[fLZ_CLS_ID] = "T";
                        string _tmpstr = "SA,CK,KB,PC";
                        if (_tmpstr.IndexOf(_bilID) > -1)
                            mfdr["CLSLZ"] = "T";
                        _tmpstr = "OD,OW";
                        if (_tmpstr.IndexOf(_bilID) > -1)
                            mfdr["CLS_LZ_AUTO"] = "T";

                    }
                    mfdr[fTURN_ID] = "1";
                    string _nouseInvNo = "IO,IB,TJ,CK,KB";
                    if (_nouseInvNo.IndexOf(_bilID)==-1)
                        mfdr["INV_NO"] = _inv_no;
                }
                else  //isAdd =false
                {
                 
                        if (bodydr.Table.DataSet.Tables.Contains("TF_CK") && bodydr["OS_ID"].ToString() == "SA")
                        {
                            DRPSA _drpsa = new DRPSA();
                            
                            SunlikeDataSet _saDataSet = _drpsa.GetData("", dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["USR",DataRowVersion.Original].ToString(), bodydr["OS_ID"].ToString(), bodydr["OS_NO"].ToString(),false,false);
                            if (_saDataSet.Tables["MF_PSS"].Rows.Count > 0)
                            {
                                DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
                                _dbSa.UpdateMFPSSACC_FP_NO("", _saDataSet.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString(), _saDataSet.Tables["MF_PSS"].Rows[0]["PS_NO"].ToString());
                            }
                        }
                    mfdr[fLZ_CLS_ID] = "F";
                    string _bilStr = "SA,CK,KB,PC";
                    if (_bilStr.IndexOf(_bilID) > -1)
                        mfdr["CLSLZ"] = "F";
                    _bilStr = "OD,OW";
                    if (_bilStr.IndexOf(_bilID) > -1)
                        mfdr["CLS_LZ_AUTO"] = "F";
                    bool _clearInvN0 = false;
                    if (NeedTurn_Id1(mfdr, bodyDt, _bilID, _bilNO, fAMTN_NET_FP, fTAX_FP, fAMT_FP))
                    {
                        mfdr[fTURN_ID] = "";
                        _clearInvN0 = true;
                    }
                    string _nouseInvNo = "IO,IB,TJ,CK";
                    if ((_nouseInvNo.IndexOf(_bilID) == -1) && (_clearInvN0))
                        mfdr["INV_NO"] = "";
                    if (mfdr.Table.DataSet.Tables.Contains("MF_PSS"))
                    {
                        string _tblStr = "SA,PC";
                        if (_tblStr.IndexOf(_bilID) > -1 && (mfdr["ACC_FP_NO"].ToString() == _lzNo) && (_clearInvN0))
                            mfdr["ACC_FP_NO"] = "";

                    }
                    

                }
                #region 更新銷貨單,銷退，銷折資訊
                string tblstr = "SA,SB,SD,PC,PD,PB";
                if (tblstr.IndexOf(_bilID) > -1)
                {
                    //DRPSA _invsa = new DRPSA();
                    DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);

                    if (IsAdd)
                        _dbinvIk.UpdateMFARPINVNO(mfdr["PS_ID"].ToString(), mfdr["PS_NO"].ToString(), "", mfdr["INV_NO"].ToString());
                    else
                        _dbinvIk.UpdateMFARPINVNO(mfdr["PS_ID"].ToString(), mfdr["PS_NO"].ToString(), "D", mfdr["INV_NO"].ToString());
                    DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
                    _dbSa.UpdateInvIkHeadData(mfdr["TURN_ID"].ToString(), mfdr["LZ_CLS_ID"].ToString(), mfdr["CLSLZ"].ToString(),
                        mfdr["ACC_FP_NO"].ToString(), mfdr["INV_NO"].ToString(), ConvertDecimal(mfdr, "AMT_CLS"), ConvertDecimal(mfdr, "AMTN_NET_CLS"),
                        ConvertDecimal(mfdr, "TAX_CLS"), ConvertDecimal(mfdr, "QTY_CLS"), mfdr["PS_ID"].ToString(), mfdr["PS_NO"].ToString());
                    _dbSa.UpdateInvIkBodyData(ConvertDecimal(bodydr, "AMT_FP"), ConvertDecimal(bodydr, "AMTN_NET_FP"), ConvertDecimal(bodydr, "TAX_FP"),
                         ConvertDecimal(bodydr, "QTY_FP"), bodydr["PS_ID"].ToString(), bodydr["PS_NO"].ToString(), Convert.ToInt32(bodydr["ITM"]));
                }
                #endregion
                #region 更新借出,借還入資訊
                tblstr = "LN,LB";
                if (tblstr.IndexOf(_bilID) > -1)
                {
                    
                    DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);

                    if (IsAdd)
                        _dbinvIk.UpdateMFARPINVNO(mfdr["BL_ID"].ToString(), mfdr["BL_NO"].ToString(), "", mfdr["INV_NO"].ToString());
                    else
                        _dbinvIk.UpdateMFARPINVNO(mfdr["BL_ID"].ToString(), mfdr["BL_NO"].ToString(), "D", mfdr["INV_NO"].ToString());
                    DbDRPBN _dbBn = new DbDRPBN(Comp.Conn_DB);
                    _dbBn.UpdateInvIkHeadData(mfdr["TURN_ID"].ToString(), mfdr["LZ_CLS_ID"].ToString(),mfdr["INV_NO"].ToString(), ConvertDecimal(mfdr, "AMT_CLS"), ConvertDecimal(mfdr, "AMTN_NET_CLS"),
                        ConvertDecimal(mfdr, "TAX_CLS"), ConvertDecimal(mfdr, "QTY_CLS"), mfdr["BL_ID"].ToString(), mfdr["BL_NO"].ToString());
                    _dbBn.UpdateInvIkBodyData(ConvertDecimal(bodydr, "AMT_FP"), ConvertDecimal(bodydr, "AMTN_NET_FP"), ConvertDecimal(bodydr, "TAX_FP"),
                         ConvertDecimal(bodydr, "QTY_FP"), bodydr["BL_ID"].ToString(), bodydr["BL_NO"].ToString(), Convert.ToInt32(bodydr["ITM"]));
                }
                #endregion
                #region 配送單
                tblstr = "IO,IB";
                if (tblstr.IndexOf(_bilID) > -1)
                {
                 
                    DbDRPIO _dbIo = new DbDRPIO(Comp.Conn_DB);
                    _dbIo.UpdateInvIkHeadData(mfdr["TURN_ID"].ToString(), mfdr["LZ_CLS_ID"].ToString(),
                                   ConvertDecimal(mfdr, "AMT_CLS"), ConvertDecimal(mfdr, "AMTN_NET_CLS"), ConvertDecimal(mfdr, "TAX_CLS"), ConvertDecimal(mfdr, "QTY_CLS"),
                                   mfdr["TURN_ID2"].ToString(), mfdr["LZ_CLS_ID2"].ToString(),
                                   ConvertDecimal(mfdr, "AMT_CLS2"), ConvertDecimal(mfdr, "AMTN_NET_CLS2"), ConvertDecimal(mfdr, "TAX_CLS2"), ConvertDecimal(mfdr, "QTY_CLS2"),
                                   mfdr["IC_ID"].ToString(), mfdr["IC_NO"].ToString());
                    _dbIo.UpdateInvIkBodyData(ConvertDecimal(bodydr, "AMT_FP"), ConvertDecimal(bodydr, "AMTN_NET_FP"), ConvertDecimal(bodydr, "TAX_FP"), ConvertDecimal(bodydr, "QTY_FP"),
                                            ConvertDecimal(bodydr, "AMT_FP2"), ConvertDecimal(bodydr, "AMTN_NET_FP2"), ConvertDecimal(bodydr, "TAX_FP2"), ConvertDecimal(bodydr, "QTY_FP2"),
                                            mfdr["IC_ID"].ToString(), mfdr["IC_NO"].ToString(), Convert.ToInt32(bodydr["ITM"]));
                }
              #endregion
                #region 调价单
                tblstr = "TJ";
                if (tblstr.IndexOf(_bilID) > -1)
                {
            
                    DbDRPTJ _dbTj = new DbDRPTJ(Comp.Conn_DB);

                    _dbTj.UpdateInvIkHeadData(mfdr["TURN_ID"].ToString(), mfdr["LZ_CLS_ID"].ToString(), ConvertDecimal(mfdr, "AMT_CLS"), ConvertDecimal(mfdr, "AMTN_NET_CLS"),
                        ConvertDecimal(mfdr, "TAX_CLS"), ConvertDecimal(mfdr, "QTY_CLS"), mfdr["TJ_NO"].ToString());
                    _dbTj.UpdateInvIkBodyData(ConvertDecimal(bodydr, "AMT_FP"), ConvertDecimal(bodydr, "AMTN_NET_FP"), ConvertDecimal(bodydr, "TAX_FP"),
                         ConvertDecimal(bodydr, "QTY_FP"),  bodydr["TJ_NO"].ToString(), Convert.ToInt32(bodydr["ITM"]));

                }
                  #endregion
                #region  出库單
                tblstr = "CK";
                if (tblstr.IndexOf(_bilID) > -1)
                {


                    DbInvIK _dbinvIK = new DbInvIK(Comp.Conn_DB);
                    _dbinvIK.UpDateMFCKHead(mfdr, _bilNO,"CK");
                    _dbinvIK.UpDateTFCKBody(bodydr, _bilNO,"CK");
                    
               

                
                }
                #endregion
            }
            else
            {
                 DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);
                if (IsAdd)
                {
                    _dbinvIk.UpdateMFARPINVNOForEXP(bodydr["EP_ID"].ToString(), bodydr["EP_NO"].ToString(), Convert.ToInt32(bodydr["ITM"]), "", bodydr["INV_NO"].ToString());
                    bodydr["INV_NO"] = _inv_no;
                }
                else
                {
                    _dbinvIk.UpdateMFARPINVNOForEXP(bodydr["EP_ID"].ToString(), bodydr["EP_NO"].ToString(), Convert.ToInt32(bodydr["ITM"]), "D", bodydr["INV_NO"].ToString());
                    bodydr["INV_NO"] = "";
                }
                DbMonEX _dbEx = new DbMonEX(Comp.Conn_DB);
                _dbEx.UpdateInvIkBodyData(ConvertDecimal(bodydr, "AMT_FP"), ConvertDecimal(bodydr, "AMTN_NET_FP"), ConvertDecimal(bodydr, "TAX_FP"),
                      bodydr["LZ_CLS_ID"].ToString(), bodydr["CLSLZ"].ToString(), bodydr["INV_NO"].ToString(), bodydr["EP_ID"].ToString(), bodydr["EP_NO"].ToString(), Convert.ToInt32(bodydr["ITM"]));

               
            }

        }
        private bool needCls2(DataRow mfdr, DataTable bydt, DataRow tflzdr, string fAMTN_NET_CLS, string fTAX_CLS, string fAMT_CLS)
        {
            string _osId = tflzdr["BIL_ID"].ToString();
            string _osNo = tflzdr["CK_NO"].ToString();

            //string _tblStr = "TJ,TB,TC,DT";
            decimal _rAmt = 0;
            decimal _rAmtn_Net = 0;
            decimal _rTax = 0;
            foreach (DataRow dr in bydt.Rows)
            {
                if (bydt.DataSet.Tables.Contains("TF_BLN"))  //---借出
                {
                    _rAmtn_Net = _rAmtn_Net + ConvertDecimal(dr, "AMTN_RNT_NET");
                    _rTax = _rTax + ConvertDecimal(dr, "TAX_RNT");
                    if (string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()))
                        _rAmt = _rAmt + ConvertDecimal(dr, "AMTN_RNT_NET") + ConvertDecimal(dr, "TAX_RNT");
                    else
                    {
                        if (string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()) && mfdr["TAX_ID"].ToString() == "3")
                            _rAmt = _rAmt + ConvertDecimal(dr, "AMT_RNT") + (ConvertDecimal(dr, "AMT_RNT") * TaxRatio(dr) / 100);
                        else
                            _rAmt = _rAmt + ConvertDecimal(dr, "AMT_RNT");
                    }
                }
                else
                    if (bydt.DataSet.Tables.Contains("TF_IC") || bydt.DataSet.Tables.Contains("TF_TJ"))  //---
                    {
                        if ((_osId == "IB") || ((_osId == "IM") && (dr["IMTAG"].ToString() == "O")))
                        {
                            _rAmtn_Net = _rAmtn_Net + ConvertDecimal(dr, "CST");
                            _rAmt = _rAmt + ConvertDecimal(dr, "CST");
                        }
                        else
                        {
                            _rAmtn_Net = _rAmtn_Net + ConvertDecimal(dr, "AMTN_NET");
                            _rAmt = _rAmt + ConvertDecimal(dr, "AMTN_NET");
                        }
                        _rTax = 0;
                    }
                    else
                    {

                        _rAmtn_Net = _rAmtn_Net + ConvertDecimal(dr, "AMTN_NET");
                        _rTax = _rTax + ConvertDecimal(dr, "TAX");
                        if (string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()))
                            _rAmt = _rAmt + ConvertDecimal(dr, "AMTN_NET") + ConvertDecimal(dr, "TAX");
                        else
                            if (mfdr["TAX_ID"].ToString() == "3" && !string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()))
                                _rAmt = _rAmt + ConvertDecimal(dr, "AMT") + (ConvertDecimal(dr, "AMT") * TaxRatio(dr) / 100);
                            else
                                _rAmt = _rAmt + ConvertDecimal(dr, "AMT");

                    }
            }
            string bilStr = "SA,CK,PC";
            if (bilStr.IndexOf(_osId) > -1)
            {
                decimal TempDis_Cnt = ConvertDecimal(mfdr, "Dis_Cnt");
                if (TempDis_Cnt < 0) TempDis_Cnt = 100 + TempDis_Cnt;
                if ((TempDis_Cnt != 0) && (mfdr["OS_NO"].ToString().IndexOf("-") > -1))
                {
                    TempDis_Cnt = TempDis_Cnt / 100;
                    _rAmtn_Net = _rAmtn_Net * TempDis_Cnt;
                    _rTax = _rTax * TempDis_Cnt;
                    _rAmt = _rAmt * TempDis_Cnt;
                }
            }
            bilStr = "IO,IB,IM,TJ";
            bool CheckOK = false;
            if ((bilStr.IndexOf(_osId) > -1) || (string.IsNullOrEmpty(mfdr["CUR_ID"].ToString())))
            {
                CheckOK = (((_rAmtn_Net + _rTax > 0) && (_rAmtn_Net + _rTax > ConvertDecimal(mfdr, fAMTN_NET_CLS) + ConvertDecimal(mfdr, fTAX_CLS)))
                         ||
                        ((_rAmtn_Net + _rTax < 0) && (_rAmtn_Net + _rTax != ConvertDecimal(mfdr, fAMTN_NET_CLS) + ConvertDecimal(mfdr, fTAX_CLS))));
            }
            else
            {
                CheckOK = (
                            ((_rAmt > 0) && (_rAmt > ConvertDecimal(mfdr, fAMT_CLS))) ||
                            ((_rAmt < 0) && (_rAmt != ConvertDecimal(mfdr, fAMT_CLS)))
                          );
            }
            if (CheckOK)
                return false;
            else
                return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mfdr">来源单表头</param>
        /// <param name="bodyDt">来源单表身</param>
        /// <param name="_bilID">TF_LZ.BIL_ID</param>
        /// <param name="_bilNO">TF_IZ.CK_NO</param>
        /// <param name="fAMTN_NET_FP"></param>
        /// <param name="fTAX_FP"></param>
        /// <param name="fAMT_FP"></param>
        /// <returns></returns>
        private bool NeedCls1(DataRow mfdr, DataTable bodyDt, string _bilID, string _bilNO, string fAMTN_NET_FP, string fTAX_FP, string fAMT_FP)
        {
            decimal _rAmtn_Net = 0;
            decimal _rTax = 0;
            decimal _rAmt = 0;
            
            
            foreach (DataRow dr in bodyDt.Rows)
            {
                if (dr.Table.DataSet.Tables.Contains("TF_BLN"))
                {
                    _rAmtn_Net = ConvertDecimal(dr, "AMTN_RNT_NET");
                    _rTax = ConvertDecimal(dr, "TAX_RNT");
                    if (string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()))
                        _rAmt = _rAmtn_Net + _rTax;
                    else
                        if (mfdr["TAX_ID"].ToString() == "3" && !string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()))
                            _rAmt = ConvertDecimal(dr, "AMT_RNT") + (ConvertDecimal(dr, "AMT_RNT") * TaxRatio(dr) / 100);
                        else
                            _rAmt = ConvertDecimal(dr, "AMT_RNT");
                }
                else
                    if (dr.Table.DataSet.Tables.Contains("TF_IC") || dr.Table.DataSet.Tables.Contains("TF_TJ"))
                    {
                        if ((_bilID == "IB") ||
                              (_bilID == "IM") && (dr["IMTAG"].ToString() == "O"))

                            _rAmtn_Net = ConvertDecimal(dr, "CST");
                        else
                            _rAmtn_Net = ConvertDecimal(dr, "AMTN_NET");

                        _rTax = 0;
                        _rAmt = _rAmtn_Net;
                    }
                    else
                    {
                        _rAmtn_Net = ConvertDecimal(dr, "AMTN_NET");
                        _rTax = ConvertDecimal(dr, "TAX");
                        if (string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()))
                            _rAmt = ConvertDecimal(dr, "AMTN_NET") + ConvertDecimal(dr, "TAX");
                        else
                            if ((mfdr["TAX_ID"].ToString() == "3") && (!string.IsNullOrEmpty(mfdr["CUR_ID"].ToString())))
                                _rAmt =  ConvertDecimal(dr, "AMT") + (ConvertDecimal(dr, "AMT")*TaxRatio(dr)/100);
                            else
                                _rAmt = ConvertDecimal(dr, "AMT");
                        
                    }
                    string bilStr = "SA,CK,PC";
                   if (bilStr.IndexOf(_bilID)>-1)
                   {
                       decimal TempDis_Cnt = ConvertDecimal(mfdr,"Dis_Cnt");
                       if (TempDis_Cnt<0) TempDis_Cnt = 100+TempDis_Cnt;
                       if ((TempDis_Cnt!=0) && (mfdr["OS_NO"].ToString().IndexOf("-")>-1))
                       {
                            TempDis_Cnt = TempDis_Cnt/100;
                            _rAmtn_Net=_rAmtn_Net*TempDis_Cnt;
                            _rTax     =_rTax*TempDis_Cnt;
                            _rAmt     =_rAmt*TempDis_Cnt;
                       }
                   }
                   bilStr = "IO,IB,IM,TJ";
                   bool CheckOK=false;
                   if (bilStr.IndexOf(_bilID)>-1 || string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()))
                   {
                       CheckOK = ( ( (_rAmtn_Net+_rTax>0) && (_rAmtn_Net+_rTax>ConvertDecimal(dr,fAMTN_NET_FP)+ConvertDecimal(dr,fTAX_FP) )) ||
                                   ( (_rAmtn_Net+_rTax<0) && (_rAmtn_Net+_rTax!=ConvertDecimal(dr,fAMTN_NET_FP)+ConvertDecimal(dr,fTAX_FP)))
                                 );
                   }
                   else
                        CheckOK = ( ( (_rAmt>0) && (_rAmt>ConvertDecimal(dr,fAMT_FP) )) ||
                                   ( (_rAmt<0) && (_rAmt!=ConvertDecimal(dr,fAMT_FP) ))
                                 );
                if (CheckOK)
                {
                    return false;
                }


            }
            return true;
        }

        private bool NeedTurn_Id1(DataRow mfdr,DataTable bodyDt, string _bilID, string _bilNO, string fAMTN_NET_FP, string fTAX_FP, string fAMT_FP)
        {
            foreach (DataRow dr in bodyDt.Rows)
            {
                string bilstr = "IO,IB,IM,TJ";
                bool breakOK = false;
                if (bilstr.IndexOf(_bilID) > -1 || string.IsNullOrEmpty(mfdr["CUR_ID"].ToString()))
                    breakOK = ConvertDecimal(dr, fAMTN_NET_FP) + ConvertDecimal(dr, fTAX_FP) == 0;
                else
                    breakOK = ConvertDecimal(dr, fAMT_FP)  == 0;
                if (!breakOK)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断其他费用收单是否结案
        /// </summary>
        /// <param name="bodydr"></param>
        /// <param name="fAMTN_NET_FP">未税金额栏位</param>
        /// <param name="fTAX_FP">税额栏位</param>
        /// <param name="fAMT_FP">金额栏位</param>
        /// <returns></returns>
        private bool NeedClsForExp(DataRow bodydr, string fAMTN_NET_FP, string fTAX_FP,string fAMT_FP)
        {
            decimal _rAmtn_Net = ConvertDecimal(bodydr, "AMTN_NET");
            decimal _rTax = ConvertDecimal(bodydr, "TAX");
            decimal _rAmt =0;
            if (string.IsNullOrEmpty(bodydr["CUR_ID"].ToString()))
                _rAmt = _rAmtn_Net + _rTax;
            else
                if (bodydr["TAX_ID"].ToString() == "3" && !string.IsNullOrEmpty(bodydr["CUR_ID"].ToString()))
                    _rAmt = ConvertDecimal(bodydr, "AMT") + (ConvertDecimal(bodydr, "AMT") * TaxRatio(bodydr) / 100);
                else
                    _rAmt = ConvertDecimal(bodydr, "AMT");
            bool CheckOK = false;
            if (string.IsNullOrEmpty(bodydr["CUR_ID"].ToString()))
            {
                CheckOK = (((_rAmtn_Net + _rTax > 0) && (_rAmtn_Net + _rTax > ConvertDecimal(bodydr, fAMTN_NET_FP) + ConvertDecimal(bodydr, fTAX_FP))) ||
                              ((_rAmtn_Net + _rTax < 0) && (_rAmtn_Net + _rTax != ConvertDecimal(bodydr, fAMTN_NET_FP) + ConvertDecimal(bodydr, fTAX_FP)))
                    );

            }
            else
            {
                CheckOK = (((_rAmt > 0) && (_rAmt > ConvertDecimal(bodydr, fAMT_FP))) ||
                              ((_rAmt < 0) && (_rAmt != ConvertDecimal(bodydr, fAMT_FP)))
                    );

            }
            if (CheckOK)
                return false;
            else
                return true;
        }

        #endregion

        /// <summary>
        /// 判断来源单表头是否要结案
        /// </summary>
        /// <param name="mfdr">来源单表头</param>
        /// <param name="bydt">来源单表身</param>
        /// <param name="tflzdr">tflz Datarow</param>
        /// <param name="fAMTN_NET_CLS">未税金额栏位名称</param>
        /// <param name="fTAX_CLS">税额栏位名称</param>
        /// <param name="fAMT_CLS">金额栏位名称</param>
        /// <returns></returns>
        

        private decimal TaxRatio(DataRow dr)
        {

            if (dr.Table.Columns.Contains("TAX_RTO"))
            {
                if (!string.IsNullOrEmpty(dr["TAX_RTO"].ToString()))
                    return ConvertDecimal(dr,"TAX_RTO");
                else
                    return 0;
            }
            else
                if (dr.Table.Columns.Contains("RTO_TAX") && !string.IsNullOrEmpty(dr["RTO_TAX"].ToString()))
                {
                    return Convert.ToDecimal(dr["RTO_TAX"]);
                }
                else
                    return SPC_TAX(dr["CUS_NO"].ToString(), dr["PRD_NO"].ToString());
        }

        private decimal SPC_TAX(string cus_no, string prd_no)
        {
            //if ((string.IsNullOrEmpty(prd_no))|| () 
            return 0;
        }


        /// <summary>
        /// 删除立帐单
        /// </summary>
        /// <param name="lzId">lzid</param>
        /// <param name="lzNo">单号</param>
        public void DeleteArp(string lzId, string lzNo)
        {
            DbInvIK _invDbIk = new DbInvIK(Comp.Conn_DB);
            SunlikeDataSet _ds = _invDbIk.GetData(lzId, lzNo, false);
            DataTable _mflzTable = _ds.Tables["MF_LZ"];
            Arp _arp = new Arp();
            try
            {

                if (_mflzTable.Rows[0]["ARP_NO"] != null)
                    {
                        if (!_arp.DeleteMfArp(_mflzTable.Rows[0]["ARP_NO"].ToString()))//删除立账单
                        {
                            throw new SunlikeException(/*已冲帐,不能删除该笔数据*/"RCID=MON.HINT.ARP_NO");
                        }
                    }

                _mflzTable.Rows[0]["ARP_NO"] = System.DBNull.Value;
                this.UpdateDataSet(_ds);

            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }


        /// <summary>
        /// 判断来源单是否存在TF_LZ
        /// </summary>
        /// <param name="lzId">TF_LZ的单据别</param>
        /// <param name="bilId">来源单识ID</param>
        /// <param name="ckno">来源单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetInTfLz(string lzId, string bilId, string ckno)
        {
            DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);
            return _dbinvIk.GetInTfLz(lzId, bilId, ckno);
        
        }

        /// <summary>
        /// 判断来源单是否存在TF_LZ
        /// </summary>
        /// <param name="bilId">来源单识ID</param>
        /// <param name="ckno">来源单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetInTfLz(string bilId, string ckno)
        {
            DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);
            return _dbinvIk.GetInTfLz(bilId, ckno);

        }


        #region 新增TF_LZ_EP
        private void AppendToTF_LZ_EP(DataRow tfLzdr, string osId, string osNo, string turnId)
        {
            string bilnoStr = "SA,CK,PC,PB";
            if (bilnoStr.IndexOf(osId) < 0) return;
            DataTable HeadTable = null;
            DataTable BilTable = null;
            bool LocateOK_H = false;
            bool LocateOK_B = false;
            decimal Ratio = 0;
            decimal Ratio1 = 0;
            SunlikeDataSet _ds = null;
            decimal rAmt = 0;
            bool IsLastID = false;
            int MaxItm = 0;
            try
            {
                if (turnId == "2")
                {
                    #region turnId==2
                    if (osId == "CK")
                    {
                        string _sql = "Select CK_NO,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,CUR_ID,EXC_RTO,CHK_MAN,DIS_CNT,CLSLZ,OS_ID,DIS_CNT,EP_NO1,EP_NO,AMTN_EP,AMTN_EP1,TAX_ID  from MF_CK"
                                   + " where CK_ID='" + osId + "' and CK_NO='" + osNo + "';";
                        _sql += " Select AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PRE_ITM,AMTN_NET,TAX,QTY,OS_ID,OS_NO,AMT From TF_CK Where CK_ID='" + osId + "' and  CK_NO='" + osNo + "'";
                        Query _query = new Query();
                        _ds = _query.DoSQLString(_sql);
                        _ds.Tables[0].TableName = "MF_CK";
                        _ds.Tables[1].TableName = "TF_CK";
                        HeadTable = _ds.Tables["MF_CK"];
                        BilTable = _ds.Tables["TF_CK"];
                        if (HeadTable.Rows.Count > 0)
                            LocateOK_H = true;

                        if (HeadTable.Rows.Count > 0)
                            LocateOK_B = true;
                    }
                    else //MF_PSS
                    {

                        string _sql = "Select CUR_ID,OS_NO,Dis_Cnt,AMTN_EP,AMTN_EP1,CLSCK,PS_ID,PS_NO,EP_NO,EP_NO1,CLSLZ,VOH_ID,VOH_NO  from MF_PSS Where PS_ID='" + osId + "' and PS_NO='" + osNo + "';" +
                                      " Select AMTN_NET,AMT,TAX,OS_ID,OS_NO,QTY,PRD_NO,UNIT,AMTN_EP,ITM from TF_PSS Where PS_ID='" + osId + "' and PS_NO='" + osNo + "'";
                        Query _query = new Query();
                        _ds = _query.DoSQLString(_sql);
                        _ds.Tables[0].TableName = "MF_PSS";
                        _ds.Tables[1].TableName = "TF_PSS";
                        HeadTable = _ds.Tables["MF_PSS"];
                        BilTable = _ds.Tables["TF_PSS"];
                        if (HeadTable.Rows.Count > 0)
                            LocateOK_H = true;

                        if (BilTable.Rows.Count > 0)
                            LocateOK_B = true;
                    }
                    if ((LocateOK_H) && (LocateOK_B))
                    {
                        foreach (DataRow bodydr in BilTable.Rows)
                        {
                            if (string.IsNullOrEmpty(HeadTable.Rows[0]["CUR_ID"].ToString()))
                                rAmt = rAmt + ConvertDecimal(bodydr, "AMTN_NET") + ConvertDecimal(bodydr, "TAX");
                            else
                                if (!string.IsNullOrEmpty(HeadTable.Rows[0]["CUR_ID"].ToString()) && HeadTable.Rows[0]["TAX_ID"].ToString() == "3")
                                    rAmt = rAmt + ConvertDecimal(bodydr, "AMT") + (ConvertDecimal(bodydr, "AMT") * TaxRatio(bodydr) / 100);
                                else
                                    rAmt = rAmt + ConvertDecimal(bodydr, "AMT");
                        }


                        string _bilstr = "SA,CK,PC";
                        if (_bilstr.IndexOf(osId) > 0)
                        {
                            decimal tempDis_Cnt = ConvertDecimal(HeadTable.Rows[0], "Dis_Cnt");
                            if (tempDis_Cnt < 0)
                                tempDis_Cnt = 100 + tempDis_Cnt;
                            if (tempDis_Cnt != 0 && HeadTable.Rows[0]["OS_NO"].ToString().IndexOf("-") == -1)
                            {
                                tempDis_Cnt = tempDis_Cnt / 100;

                                rAmt = rAmt * tempDis_Cnt;
                            }
                        }
                        if (rAmt != 0)
                            Ratio = ConvertDecimal(tfLzdr, "AMT") / rAmt;
                        if (osId == "CK")
                        {
                            #region OsId="CK"
                            string _str = "Select B.OS_ID,B.OS_NO,(Sum(IsNull(B.QTY,0)*(Case when B.UNIT='2' then G.PK2_QTY when B.unit='3' then G.PK3_QTY else 1 end))) As QTY " +
                                          " From TF_CK B Left Outer Join PRDT G On B.PRD_NO=G.PRD_NO " +
                                          " Where B.OS_ID='SA' And IsNull(B.OS_NO,'')<>'' And B.CK_ID='CK' And B.CK_NO=''" + osNo + "' Group By B.OS_ID,B.OS_NO ";
                            Query _tmpQuery = new Query();
                            SunlikeDataSet _tmpDs = _tmpQuery.DoSQLString(_str);
                            foreach (DataRow dr in _tmpDs.Tables[0].Rows)
                            {
                                _str = "Select CLSCK,AMTN_EP,AMTN_EP1,EP_NO,EP_NO1 frrm MF_PSS Where PS_ID='" + osId + "' and PS_NO='" + osNo + "'";
                                SunlikeDataSet _mfPssDs = _tmpQuery.DoSQLString(_str);
                                if (ConvertDecimal(_mfPssDs.Tables[0].Rows[0], "AMTN_EP") != 0 || ConvertDecimal(_mfPssDs.Tables[0].Rows[0], "AMTN_EP1") != 0)
                                {
                                    IsLastID = _mfPssDs.Tables[0].Rows[0]["CLSCK"].ToString() == "T" && checkCK(dr);
                                    if (!IsLastID)
                                        Ratio1 = Ratio * GetMfPssRatioOfMfCK(dr["OS_ID"].ToString(), dr["OS_NO"].ToString(), ConvertDecimal(dr, "QTY"));
                                    else
                                        Ratio1 = Ratio;
                                    if (ConvertDecimal(_mfPssDs.Tables[0].Rows[0], "AMTN_EP") != 0)
                                        ToTF_LZ_EPTbl("ER", _mfPssDs.Tables[0].Rows[0]["EP_NO"].ToString(), Ratio1, IsLastID, ref MaxItm, tfLzdr, 0);
                                    if (ConvertDecimal(_mfPssDs.Tables[0].Rows[0], "AMTN_EP1") != 0)
                                        ToTF_LZ_EPTbl("EP", _mfPssDs.Tables[0].Rows[0]["EP_NO"].ToString(), Ratio1, IsLastID, ref MaxItm, tfLzdr, 0);


                                }


                            }
                            #endregion

                        }
                        else //osId != "CK"
                        {
                            #region OsId!=CK
                            IsLastID = HeadTable.Rows[0]["CLSLZ"].ToString() == "T";
                            if (ConvertDecimal(HeadTable.Rows[0], "AMTN_EP") != 0 ||
                                osId == "PC" ||
                                osId == "PB"
                                )
                            {
                                if (osId == "SA")
                                    ToTF_LZ_EPTbl("ER", HeadTable.Rows[0]["EP_NO"].ToString(), Ratio, IsLastID, ref MaxItm, tfLzdr, 0);

                                else
                                    ToTF_LZ_EPTbl("EP", HeadTable.Rows[0]["EP_NO"].ToString(), Ratio, IsLastID, ref MaxItm, tfLzdr, 0);

                            }
                            if (ConvertDecimal(HeadTable.Rows[0], "AMTN_EP1") != 0 ||
                                    osId == "SA")
                            {
                                if (osId == "SA")
                                    ToTF_LZ_EPTbl("EP", HeadTable.Rows[0]["EP_NO1"].ToString(), Ratio, IsLastID, ref MaxItm, tfLzdr, 0);
                                else
                                    ToTF_LZ_EPTbl("ER", HeadTable.Rows[0]["EP_NO1"].ToString(), Ratio, IsLastID, ref MaxItm, tfLzdr, 0);

                            }
                            #endregion

                        }
                    }
                    #endregion
                }
                else  //turn_id==1
                {

                    if (osId == "CK")
                    {
                        string _sql = "Select CK_NO,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,CUR_ID,EXC_RTO,CHK_MAN,DIS_CNT,CLSLZ,OS_ID,DIS_CNT,OS_NO,TAX_ID  from MF_CK"
                                   + " where CK_ID='" + osId + "' and CK_NO='" + osNo + "';";
                        _sql += " Select AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,PRE_ITM,AMTN_NET,TAX,QTY,OS_ID,OS_NO,AMT,OTH_ITM From TF_CK Where CK_ID='" + osId + "' and  CK_NO='" + osNo + "' and PRE_ITM=" + tfLzdr["EST_ITM"].ToString();
                        Query _query = new Query();
                        _ds = _query.DoSQLString(_sql);
                        _ds.Tables[0].TableName = "MF_CK";
                        _ds.Tables[1].TableName = "TF_CK";
                        HeadTable = _ds.Tables["MF_CK"];
                        BilTable = _ds.Tables["TF_CK"];
                        if (HeadTable.Rows.Count > 0)
                            LocateOK_H = true;

                        if (HeadTable.Rows.Count > 0)
                            LocateOK_B = true;
                    }
                    else //MF_PSS
                    {


                        string _sql = "Select CUR_ID,OS_NO,Dis_Cnt,AMTN_EP,AMTN_EP1,CLSCK,PS_ID,PS_NO,EP_NO,EP_NO1,TAX_ID,CLSLZ,VOH_NO,VOH_ID  from MF_PSS Where PS_ID='" + osId + "' and PS_NO='" + osNo + "';" +
                                      " Select AMTN_NET,AMT,TAX,OS_ID,OS_NO,QTY,PRD_NO,UNIT,AMTN_EP,ITM from TF_PSS Where PS_ID='" + osId + "' and PS_NO='" + osNo + "' and PRE_ITM=" + tfLzdr["EST_ITM"].ToString();
                        Query _query = new Query();
                        _ds = _query.DoSQLString(_sql);
                        _ds.Tables[0].TableName = "MF_PSS";
                        _ds.Tables[1].TableName = "TF_PSS";
                        HeadTable = _ds.Tables["MF_PSS"];
                        BilTable = _ds.Tables["TF_PSS"];
                        if (HeadTable.Rows.Count > 0)
                            LocateOK_H = true;

                        if (BilTable.Rows.Count > 0)
                            LocateOK_B = true;
                    }
                    if ((LocateOK_H) && (LocateOK_B))
                    {
                        DataRow bodydr = BilTable.Rows[0];
                        if (string.IsNullOrEmpty(HeadTable.Rows[0]["CUR_ID"].ToString()))
                            rAmt = rAmt + ConvertDecimal(bodydr, "AMTN_NET") + ConvertDecimal(bodydr, "TAX");
                        else
                            if (!string.IsNullOrEmpty(HeadTable.Rows[0]["CUR_ID"].ToString()) && HeadTable.Rows[0]["TAX_ID"].ToString() == "3")
                                rAmt = rAmt + ConvertDecimal(bodydr, "AMT") + (ConvertDecimal(bodydr, "AMT") * TaxRatio(_ds.Tables[0].Rows[0]) / 100);
                            else
                                rAmt = rAmt + ConvertDecimal(bodydr, "AMT");


                        string _bilstr = "SA,CK,PC";
                        if (_bilstr.IndexOf(osId) > 0)
                        {
                            decimal tempDis_Cnt = ConvertDecimal(HeadTable.Rows[0], "Dis_Cnt");
                            if (tempDis_Cnt < 0)
                                tempDis_Cnt = 100 + tempDis_Cnt;
                            if (tempDis_Cnt != 0 && HeadTable.Rows[0]["OS_NO"].ToString().IndexOf("-") == -1)
                            {
                                tempDis_Cnt = tempDis_Cnt / 100;

                                rAmt = rAmt * tempDis_Cnt;
                            }
                        }
                        if (rAmt != 0)
                            Ratio = ConvertDecimal(tfLzdr, "AMT") / rAmt;
                        if (osId == "CK")
                        {
                            if (bodydr["OS_ID"].ToString() == "SA" && !string.IsNullOrEmpty(bodydr["OS_NO"].ToString()))
                            {
                                string _tmpSql = "Select CUR_ID,OS_NO,Dis_Cnt,AMTN_EP,EP_NO1,PS_ID,PS_NO,EP_NO,EP_NO1,TAX_ID,CLSLZ,VOH_ID,VOH_NO  from MF_PSS Where PS_ID='" + bodydr["OS_ID"].ToString() + "' and PS_NO='" + bodydr["OS_NO"].ToString() + "';" +
                                  " Select AMTN_NET,AMT,TAX,OS_ID,OS_NO,AMTN_EP,PRD_NO,UNIT,QTY,ITM from TF_PSS Where PS_ID='" + bodydr["OS_ID"].ToString() + "' and PS_NO='" + bodydr["OS_NO"].ToString() + "' and ITM=" + bodydr["OTH_ITM"].ToString();
                                Query Query1 = new Query();
                                SunlikeDataSet pssDs = Query1.DoSQLString(_tmpSql);
                                pssDs.Tables[0].TableName = "MF_PSS";
                                pssDs.Tables[1].TableName = "TF_PSS";
                                if (pssDs.Tables["MF_PSS"].Rows.Count > 0 && pssDs.Tables["TF_PSS"].Rows.Count > 0)
                                {
                                    if (ConvertDecimal(pssDs.Tables["MF_PSS"].Rows[0], "AMTN_EP") != 0 || ConvertDecimal(pssDs.Tables["TF_PSS"].Rows[0], "AMTN_EP") != 0)
                                    {
                                        IsLastID = pssDs.Tables["MF_PSS"].Rows[0]["CLSCK"].ToString() == "T" && checkCK(pssDs.Tables["MF_PSS"].Rows[0]);
                                        if (!IsLastID)
                                            Ratio = Ratio * GetTfPssRatioOfTfCK(Main_Qty(pssDs.Tables["TF_PSS"].Rows[0]["PRD_NO"].ToString(),
                                                                                        pssDs.Tables["TF_PSS"].Rows[0]["UNIT"].ToString(),
                                                                                        ConvertDecimal(pssDs.Tables["TF_PSS"].Rows[0], "QTY")
                                                                                        ),
                                                                                 Main_Qty(BilTable.Rows[0]["PRD_NO"].ToString(),
                                                                                        BilTable.Rows[0]["UNIT"].ToString(),
                                                                                        ConvertDecimal(BilTable.Rows[0], "QTY")
                                                                                        )
                                                                                 );

                                        if (ConvertDecimal(pssDs.Tables["MF_PSS"].Rows[0], "AMTN_EP") != 0)
                                        {
                                            if (!IsLastID)
                                                Ratio1 = Ratio * GetPssRatioOfBody(pssDs.Tables["MF_PSS"].Rows[0]["PS_ID"].ToString(),
                                                                                   pssDs.Tables["MF_PSS"].Rows[0]["PS_NO"].ToString(),
                                                                                   ConvertDecimal(pssDs.Tables["TF_PSS"].Rows[0], "AMTN_NET") + ConvertDecimal(pssDs.Tables["TF_PSS"].Rows[0], "TAX"));
                                            else
                                                Ratio1 = Ratio;

                                            ToTF_LZ_EPTbl("ER", pssDs.Tables["MF_PSS"].Rows[0]["EP_NO"].ToString(), Ratio1, IsLastID, ref MaxItm, tfLzdr, 0);
                                        }
                                        if (ConvertDecimal(pssDs.Tables["TF_PSS"].Rows[0], "AMTN_EP") != 0)
                                        {
                                            if (!IsLastID)
                                                Ratio1 = Ratio * GetExpRatioOfPss("EP", pssDs.Tables["MF_PSS"].Rows[0]["EP_NO1"].ToString(), ConvertDecimal(pssDs.Tables["TF_PSS"].Rows[0], "AMTN_EP"));
                                            else
                                                Ratio1 = Ratio;
                                            ToTF_LZ_EPTbl("EP", pssDs.Tables["MF_PSS"].Rows[0]["EP_NO1"].ToString(), Ratio1, IsLastID, ref MaxItm, tfLzdr, ConvertDecimal(pssDs.Tables["TF_PSS"].Rows[0], "AMTN_EP"));
                                        }
                                    }
                                }

                            }

                        }  //!=CK
                        else
                        {
                            IsLastID = HeadTable.Rows[0]["CLSLZ"].ToString() == "T";
                            if ((osId == "SA" && ConvertDecimal(HeadTable.Rows[0], "AMTN_EP") != 0) ||
                                 (osId == "PC" && ConvertDecimal(HeadTable.Rows[0], "AMTN_EP1") != 0))
                            {
                                if (!IsLastID)
                                    Ratio1 = Ratio * GetPssRatioOfBody(HeadTable.Rows[0]["PS_ID"].ToString(),
                                                                        HeadTable.Rows[0]["PS_NO"].ToString(),
                                                                        ConvertDecimal(BilTable.Rows[0], "AMTN_NET") +
                                                                        ConvertDecimal(BilTable.Rows[0], "TAX"));
                                else
                                    Ratio1 = Ratio;
                                if (osId == "SA")
                                    ToTF_LZ_EPTbl("ER", HeadTable.Rows[0]["EP_NO"].ToString(), Ratio1, IsLastID, ref MaxItm, tfLzdr, 0);
                                else
                                    ToTF_LZ_EPTbl("ER", HeadTable.Rows[0]["EP_NO1"].ToString(), Ratio1, IsLastID, ref MaxItm, tfLzdr, 0);


                            }
                            string rEP_NO = "";
                            if (ConvertDecimal(BilTable.Rows[0], "AMTN_EP") != 0 || IsLastID)
                            {

                                if (osId == "SA")
                                    rEP_NO = HeadTable.Rows[0]["EP_NO1"].ToString();
                                else
                                    rEP_NO = HeadTable.Rows[0]["EP_NO"].ToString();

                                if (!IsLastID)
                                    Ratio1 = Ratio * GetExpRatioOfPss("EP", rEP_NO, ConvertDecimal(BilTable.Rows[0], "AMTN_EP"));
                                else
                                    Ratio1 = Ratio;
                                ToTF_LZ_EPTbl("EP", rEP_NO, Ratio1, IsLastID, ref MaxItm, tfLzdr, ConvertDecimal(BilTable.Rows[0], "AMTN_EP"));
                            }
                        }
                    }
                }
            }
            catch (Exception _ex)
            {
                throw _ex;

            }
        }

        private decimal GetExpRatioOfPss(string EP_ID, string EP_NO, decimal AMTN_EP)
        {
            string _str = " Declare @AMTN Numeric(28,8)" +
                 " Select @AMTN=Sum(AMTN_NET) From TF_EXP Where EP_ID='" + EP_ID + "' And EP_NO='" + EP_NO + "'  " +
                  "Select @AMTN As AMTN";

            Query tmpQuery = new Query();
            SunlikeDataSet _tmpDs = tmpQuery.DoSQLString(_str);
            if ((AMTN_EP != 0) && (ConvertDecimal(_tmpDs.Tables[0].Rows[0], "AMTN") != 0))
            {
                decimal _tmpResult = AMTN_EP / ConvertDecimal(_tmpDs.Tables[0].Rows[0], "AMTN");
                if (_tmpResult > 1)
                    return 1;
                else
                    return _tmpResult;
            }
            else
                return 1;
        }

        private decimal GetPssRatioOfBody(string psId, string psNo, decimal Amtn)
        {
            string _str = " Declare @AMTN Numeric(28,8)" +
                     " Select @AMTN=IsNull(Sum(AMTN_NET),0)+IsNull(Sum(TAX),0) From TF_PSS Where PS_ID='" + psId + "' And PS_NO='" + psNo + "' " +
                     "Select @AMTN As AMTN";

            Query tmpQuery = new Query();
            SunlikeDataSet _tmpDs = tmpQuery.DoSQLString(_str);
            if ((Amtn != 0) && (ConvertDecimal(_tmpDs.Tables[0].Rows[0], "AMTN") != 0))
            {
                decimal _tmpResult = Amtn / ConvertDecimal(_tmpDs.Tables[0].Rows[0], "AMTN");
                if (_tmpResult > 1)
                    return 1;
                else
                    return _tmpResult;
            }
            else
                return 1;
        }

        private decimal GetTfPssRatioOfTfCK(decimal PSQty, decimal CKQty)
        {
            if (PSQty != 0 && CKQty != 0)
            {
                decimal tmpQty = CKQty / PSQty;
                if (tmpQty > 1)
                    tmpQty = 1;
                return tmpQty;
            }
            else
                return 0;

        }

        private decimal Main_Qty(string sR_PRD_NO, string sR_UNIT, decimal fR_QTY)
        {
            string _str = "Select PK2_QTY,PK3_QTY from  PRDT Where PRD_NO='" + sR_PRD_NO + "'";
            Query _tmpQuery = new Query();
            SunlikeDataSet _prdtDs = _tmpQuery.DoSQLString(_str);
            if (_prdtDs.Tables[0].Rows.Count == 0)
                return fR_QTY;
            else
            {
                if (sR_UNIT == "2")
                    return fR_QTY * ConvertDecimal(_prdtDs.Tables[0].Rows[0], "PK2_QTY");
                else
                    if (sR_UNIT == "3")
                        return fR_QTY * ConvertDecimal(_prdtDs.Tables[0].Rows[0], "PK3_QTY");
                    else
                        return fR_QTY;
            }
        }
        private void ToTF_LZ_EPTbl(string sEP_ID, string sEP_NO, decimal sRatio, bool LastID, ref int maxInt, DataRow tfLzDr, decimal sTfAmtn_EP)
        {
            string sRP_ID = "";
            decimal SumAmtn_Net = 0;

            if ((!LastID) && (sRatio == 0))
                return;
            if (sEP_ID == "EP")
                sRP_ID = "2";
            else
                sRP_ID = "1";
            string _str = "Select AMT,AMTN_NET,TAX,TAX_ID,CUR_ID,EXC_RTO,RP_NO,AMT_RP,ITM from  TF_EXP Where EP_ID='" + sEP_ID + "' and EP_NO='" + sEP_NO + "'";
            Query _tmpQuery = new Query();

            SunlikeDataSet _tfExpDs = _tmpQuery.DoSQLString(_str);
            SunlikeDataSet _lzEpDs;
            string _sql = "SELECT * from TF_LZ_EP WHERE LZ_ID='" + tfLzDr["LZ_ID"].ToString() + "' and LZ_NO='" + tfLzDr["LZ_NO"].ToString() + "'";
            _lzEpDs = _tmpQuery.DoSQLString(_sql);
            _lzEpDs.Tables[0].TableName = "TF_LZ_EP";
            DataRow lzEpDr = null;


            foreach (DataRow dr in _tfExpDs.Tables[0].Rows)
            {
                _str = "SELECT Sum(AMT) As AMT, Sum(AMTN_NET) As AMTN_NET, Sum(TAX) As TAX, Sum(AMTN_BC) As AMTN_BC," +
                       "Sum(AMTN_BB) As AMTN_BB, Sum(AMTN_CHK) As AMTN_CHK, Sum(AMT_BC) As AMT_BC, Sum(AMT_BB) As AMT_BB, Sum(AMT_CHK) As AMT_CHK " +
                       " From TF_LZ_EP Where EP_ID='" + sEP_ID + "' And EP_NO='" + sEP_NO + "' And EP_ITM=" + ConvertDecimal(dr, "ITM");
                Query CommQuery1 = new Query();
                SunlikeDataSet _sumExpDs = CommQuery1.DoSQLString(_str);
                decimal commAmt = 0;
                decimal commAmtn_Net = 0;
                decimal commTax = 0;
                decimal commAmtn_BC = 0;
                decimal commAmtn_BB = 0;
                decimal commAmtn_CHK = 0;
                decimal commAmt_BC = 0;
                decimal commAmt_BB = 0;
                decimal commAmt_CHK = 0;

                if (_sumExpDs.Tables[0].Rows.Count > 0)
                {
                    commAmt = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "AMT");
                    commAmtn_Net = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "AMTN_NET");
                    commTax = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "TAX");
                    commAmtn_BC = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "AMTN_BC");
                    commAmtn_BB = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "AMTN_BB");
                    commAmtn_CHK = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "AMTN_CHK");
                    commAmt_BC = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "AMT_BC");
                    commAmt_BB = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "AMT_BB");
                    commAmt_CHK = ConvertDecimal(_sumExpDs.Tables[0].Rows[0], "AMT_CHK");



                    maxInt = maxInt + 1;
                    lzEpDr = _lzEpDs.Tables["TF_LZ_EP"].NewRow();
                    lzEpDr["LZ_ID"] = tfLzDr["LZ_ID"].ToString();
                    lzEpDr["LZ_NO"] = tfLzDr["LZ_NO"].ToString();
                    lzEpDr["LZ_ITM"] = ConvertDecimal(tfLzDr, "ITM");
                    lzEpDr["EP_ID"] = sEP_ID;
                    lzEpDr["ITM"] = _lzEpDs.Tables["TF_LZ_EP"].Rows.Count + 1;
                    lzEpDr["EP_NO"] = sEP_NO;
                    _lzEpDs.Tables["TF_LZ_EP"].Rows.Add(lzEpDr);
                    if (!string.IsNullOrEmpty(dr["CUR_ID"].ToString()))
                    {
                        lzEpDr["AMT"] = ConvertDecimal(dr, "Amt");
                        if (dr["TAX_ID"].ToString() == "2" && ConvertDecimal(dr, "EXC_RTO") != 0)
                            lzEpDr["AMT"] = ConvertDecimal(lzEpDr, "AMT") - (ConvertDecimal(dr, "TAX") / ConvertDecimal(dr, "EXC_RTO"));
                        if (LastID)
                            lzEpDr["AMT"] = ConvertDecimal(lzEpDr, "AMT") - commAmt;
                        else
                            lzEpDr["AMT"] = ConvertDecimal(lzEpDr, "AMT") * sRatio;
                    }
                    if (LastID)
                    {
                        lzEpDr["AMTN_NET"] = ConvertDecimal(dr, "AMTN_NET") - commAmtn_Net;
                        lzEpDr["TAX"] = ConvertDecimal(dr, "TAX") - commTax;
                    }
                    else
                    {
                        lzEpDr["AMTN_NET"] = ConvertDecimal(dr, "AMTN_NET") * sRatio;
                        lzEpDr["TAX"] = ConvertDecimal(dr, "TAX") * sRatio;
                    }
                    lzEpDr["EP_ITM"] = ConvertDecimal(dr, "ITM");
                    lzEpDr["CUR_ID"] = dr["CUR_ID"].ToString();
                    lzEpDr["EP_ITM"] = ConvertDecimal(dr, "ITM");
                    lzEpDr["EXC_RTO"] = ConvertDecimal(dr, "EXC_RTO");
                    lzEpDr["RP_NO"] = dr["RP_NO"].ToString();
                    _str = " Select * from TF_MON Where RP_ID='" + sRP_ID + "' and RP_NO='" + dr["RP_NO"] + "' and ITM=1";
                    SunlikeDataSet tf_monDs = CommQuery1.DoSQLString(_str);
                    tf_monDs.Tables[0].TableName = "TF_MON";
                    if (ConvertDecimal(dr, "AMT_RP") != 0 && tf_monDs.Tables["TF_MON"].Rows.Count > 0)
                    {
                        lzEpDr["CACC_NO"] = tf_monDs.Tables["TF_MON"].Rows[0]["CACC_NO"].ToString();
                        lzEpDr["BACC_NO"] = tf_monDs.Tables["TF_MON"].Rows[0]["BACC_NO"].ToString();
                        lzEpDr["CHK_NO"] = cfGetFirstChk_No(tf_monDs.Tables["TF_MON"].Rows[0]);

                        if (LastID)
                        {
                            lzEpDr["AMTN_BC"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMTN_BC") - commAmtn_BC;
                            lzEpDr["AMTN_BB"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMTN_BB") - commAmtn_BB;
                            lzEpDr["AMTN_CHK"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMTN_CHK") - commAmtn_CHK;
                        }
                        else
                        {

                            lzEpDr["AMTN_BC"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMTN_BC") * sRatio;
                            lzEpDr["AMTN_BB"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMTN_BB") * sRatio;
                            lzEpDr["AMTN_CHK"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMTN_CHK") * sRatio;
                        }

                        if (!string.IsNullOrEmpty(tf_monDs.Tables["TF_MON"].Rows[0]["CUR_ID"].ToString()))
                        {
                            if (LastID)
                            {
                                lzEpDr["AMT_BC"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMT_BC") - commAmt_BC;
                                lzEpDr["AMT_BB"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMT_BB") - commAmt_BB;
                                lzEpDr["AMT_CHK"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMT_CHK") - commAmt_CHK;
                            }
                            else
                            {

                                lzEpDr["AMT_BC"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMT_BC") * sRatio;
                                lzEpDr["AMT_BB"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMT_BB") * sRatio;
                                lzEpDr["AMT_CHK"] = ConvertDecimal(tf_monDs.Tables["TF_MON"].Rows[0], "AMT_CHK") * sRatio;
                            }

                        }
                    }
                    SumAmtn_Net += ConvertDecimal(lzEpDr, "AMTN_NET");


                }
                if ((!LastID) && (sTfAmtn_EP != 0) && (Math.Abs(sTfAmtn_EP - SumAmtn_Net) - 1 <= 0)

                   )
                {
                    lzEpDr["AMTN_NET"] = ConvertDecimal(lzEpDr, "AMTN_NET") + (sTfAmtn_EP - SumAmtn_Net);
                }
                System.Collections.Hashtable _ht = new System.Collections.Hashtable();
                _ht["TF_LZ_EP"] = "LZ_ID,LZ_NO,LZ_ITM,EP_ID,ITM,AMTN_NET,TAX,EP_NO,EP_ITM,RP_NO,CACC_NO,BACC_NO" +
                                  ",CHK_NO,AMTN_BC,AMTN_BB,AMTN_CHK,CUR_ID,EXC_RTO,AMT,AMT_BC,AMT_BB,AMT_CHK";
                this.UpdateDataSet(_lzEpDs, _ht);
            }
        }

        private string cfGetFirstChk_No(DataRow dataRow)
        {
            string _str = "Select CHK_NO from  TF_MON4 Where RP_ID='" + dataRow["RP_ID"].ToString() + "' and RP_NO='" + dataRow["RP_NO"].ToString() + "' and ITM=" + dataRow["ITM"].ToString();
            Query _tmpQuery = new Query();
            SunlikeDataSet _tfExpDs = _tmpQuery.DoSQLString(_str);
            if (_tfExpDs.Tables[0].Rows.Count == 0)
                return "";
            else
                return _tfExpDs.Tables[0].Rows[0]["CHK_NO"].ToString();
        }

        private decimal GetMfPssRatioOfMfCK(string osId, string OsNO, decimal Qty)
        {
            string _str = " Declare @QTY Numeric(28,8) " +
                          " Select @QTY=Sum(IsNull(B.QTY,0)*(case when B.UNIT='2' then G.PK2_QTY when B.unit='3' then G.PK3_QTY else 1 end)) " +
                          " From TF_PSS B Left Outer Join PRDT G On B.PRD_NO=G.PRD_NO  " +
                          " Where PS_ID='" + osId + "' And PS_NO='" + OsNO + "'" +
                          " Select @QTY As QTY";

            Query tmpQuery = new Query();
            SunlikeDataSet _tmpDs = tmpQuery.DoSQLString(_str);
            if ((Qty != 0) && (ConvertDecimal(_tmpDs.Tables[0].Rows[0], "QTY") != 0))
            {
                decimal _tmpResult = Qty / ConvertDecimal(_tmpDs.Tables[0].Rows[0], "QTY");
                if (_tmpResult > 1)
                    return 1;
                else
                    return _tmpResult;
            }
            else
                return 1;


        }

        private bool checkCK(DataRow dr)
        {
            string _str = " SELECT H.CLSLZ From MF_CK H Where (Exists(Select B.CK_NO From TF_CK B  " +
                          " Where B.CK_ID=H.CK_ID And B.CK_NO=H.CK_NO And B.OS_ID='" + dr["OS_ID"].ToString() + "' And " +
                              " B.OS_NO='" + dr["OS_NO"].ToString() + "')) And (IsNull(CLSLZ,'')<>'T')  ";
            Query tmpQuery = new Query();
            SunlikeDataSet _tmpDs = tmpQuery.DoSQLString(_str);
            if (_tmpDs.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region IAuditing Members

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
            // TODO:  Add DrpTaxAa.Approve impelementation
            try
            {

                SunlikeDataSet _ds = this.GetData("", bil_id, bil_no);
                if (_ds.Tables["MF_LZ"].Rows.Count > 0)
                {
                    DataRow _drHead = _ds.Tables["MF_LZ"].Rows[0];
                    DataTable _dtBody = _ds.Tables["TF_LZ"];

                    foreach (DataRow dr in _dtBody.Rows)  //更新来源单的资讯 扣掉原先表身金额
                    {
                        if (dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID"].ToString() == "1" || dr["BIL_ID"].ToString()=="ER")
                            this.UpdateBILAMTN_NET_FP(dr, true);
                        else
                            this.UpdateBILAMTN_NET_FPHead(dr, true);
                        AppendToTF_LZ_EP(dr, bil_id, bil_no, dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID"].ToString());
                    }
                    DbInvIK _dbinvik = new DbInvIK(Comp.Conn_DB);
                    //立账
                    _drHead["CHK_MAN"] = chk_man;
                    _drHead["CLS_DATE"] = cls_dd;
                    if (bil_id == "LZ")
                    {

                        this.UpdateMfArp(_drHead);
                        _dbinvik.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
                        #region 当交易方式为即收现金or 即收票据
                        decimal _amtnRcv = 0;
                        if (_drHead["AMTN_BC"].ToString() != "")
                        {
                            _amtnRcv += Convert.ToDecimal(_drHead["AMTN_BC"]);
                        }
                        if (_drHead["AMTN_BB"].ToString() != "")
                        {
                            _amtnRcv += Convert.ToDecimal(_drHead["AMTN_BB"]);
                        }
                        if (_drHead["AMTN_CHK"].ToString() != "")
                        {
                            _amtnRcv += Convert.ToDecimal(_drHead["AMTN_CHK"]);
                        }
                        if (_drHead["AMTN_OTHER"].ToString() != "")
                        {
                            _amtnRcv += Convert.ToDecimal(_drHead["AMTN_OTHER"]);
                        }

                        if (_amtnRcv != 0)
                        {
                            string _rpNo = UpdateMon(_drHead);
                            //更新收款单号
                            //  this.UpdateRpNo(bil_id, bil_no, _rpNo);
                            //                            UpdateMon(_drHead);
                        }
                        else if (_amtnRcv == 0 && !string.IsNullOrEmpty(_drHead["RP_NO"].ToString()))
                        {
                            Bills _bills = new Bills();
                            _bills.DelRcvPay("1", _drHead["RP_NO"].ToString());
                        }
                        #endregion

                    }
                    else
                        _dbinvik.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);


                    string _vohNo = this.UpdateVohNo(_drHead, StatementType.Insert);
                    this.UpdateVohNo(bil_id, bil_no, _vohNo);
                }
                return "";
            }
            catch (Exception _ex)
            {
                return _ex.Message.ToString();
            }
        }



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
            // TODO:  Add DrpTaxAa.Deny implementation
            return "";
        }

        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="bil_id">单据识别码</param>
        /// <param name="bil_no">单据号码</param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {

            return this.RollBack(bil_id, bil_no, true);
        }

        private string RollBack(string bil_id, string bil_no, bool isUpdateHead)
        {

            try
            {
                SunlikeDataSet _ds = this.GetData("", bil_id, bil_no);
                DataRow _drHead = _ds.Tables["MF_LZ"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_LZ"];
                this.SetCanModify(_ds, false, true);
                if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.NOTALLOW");//此单据不能修改，无法反审核！
                }

                foreach (DataRow dr in _dtBody.Rows)  //更新来源单的资讯 扣掉原先表身金额
                {
                    if (dr.Table.DataSet.Tables["MF_LZ"].Rows[0]["TURN_ID"].ToString() == "1" || dr["BIL_ID"].ToString() == "ER")
                        this.UpdateBILAMTN_NET_FP(dr, false);
                    else
                        this.UpdateBILAMTN_NET_FPHead(dr, false);
                }



                #region 当交易方式为即收现金or 即收票据
                decimal _amtnRcv = 0;
                if (_drHead["AMTN_BC"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_BC"]);
                }
                if (_drHead["AMTN_BB"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_BB"]);
                }
                if (_drHead["AMTN_CHK"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_CHK"]);
                }
                if (_drHead["AMTN_OTHER"].ToString() != "")
                {
                    _amtnRcv += Convert.ToDecimal(_drHead["AMTN_OTHER"]);
                }
                _drHead["CHK_MAN"] = DBNull.Value;
                _drHead["CLS_DATE"] = DBNull.Value;
                if (_amtnRcv != 0)
                {
                    UpdateMon(_drHead);
                }
                else if (_amtnRcv == 0 && !string.IsNullOrEmpty(_drHead["RP_NO"].ToString()))
                {
                    Bills _bills = new Bills();
                    _bills.DelRcvPay("1", _drHead["RP_NO"].ToString());
                }
                #endregion

                //删除立账
                _drHead.Delete();
                //更新立账单号
                if (bil_id == "LZ")
                    this.UpdateMfArp(_drHead);
                if (isUpdateHead)
                {
                    DbInvIK _dbinvIK = new DbInvIK(Comp.Conn_DB);
                    _dbinvIK.UpdateChkMan(bil_id, bil_no, "", DateTime.MinValue);
                    _dbinvIK.UpdateArpNo(bil_id, bil_no, "", 0, 0, 0);
                    _dbinvIK.DeleteToTF_LZ_EP(bil_id, bil_no);

                }


                //更新凭证
                this.UpdateVohNo(_drHead, StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");
                return "";
            }

            catch (Exception _ex)
            {
                return _ex.Message.ToString();
            }
        }

        #endregion

        public SunlikeDataSet GetAllInvNoForBil(Dictionary<string, object> ht)
        {
            DbInvIK _dbinvIk = new DbInvIK(Comp.Conn_DB);
            string bilId = ht["BIL_ID"].ToString();
            string bilNo = ht["BIL_NO"].ToString();
            return _dbinvIk.GetAllInvNoForBil(bilId, bilNo);
        }
    }
}

