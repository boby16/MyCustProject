
using System;
using System.Collections;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;
using System.Collections.Generic;
namespace Sunlike.Business
{
    /// <summary>
    /// Summary description for InvAD.
    /// </summary>	 
    public class DrpSO : BizObject, IAuditing, IAuditingInfo, Sunlike.Business.ICloseBill
    {
        private bool _isSvs = false;
        private bool _isRunAuditing;
        private bool _DrpSOHasChange;
        private string _loginUsr = "";
        private bool _isFromYH;
        private string _yh_no = "";
        private string _saveId = "T";
        private string _controlCusWh = "0";
        private string _osId = "";
        bool _isUnder;
        DRPYHut _yh = new DRPYHut();

        #region DrpSO
        /// <summary>
        /// DrpSO
        /// </summary>
        public DrpSO()
        {
        }
        #endregion

        #region 查询受订单
        /// <summary>
        /// 查询受订单
        /// </summary>
        /// <param name="usr">当前用户</param>        
        /// <param name="osId">单据别</param>
        /// <param name="osNo">单号</param>
        /// <param name="IsDataSetSchema">取受订单框架</param>
        /// <param name="isContainSR">是否包含销货退回数量和相关表的虚拟字段</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string osId, string osNo, bool IsDataSetSchema, bool isContainSR)
        {
            string _pgm = "DRP" + osId;
            return GetData(usr, _pgm, osId, osNo, IsDataSetSchema, isContainSR);
        }
        /// <summary>
        /// 查询受订单
        /// </summary>
        /// <param name="usr">当前用户</param>
        /// <param name="pgm">PGM</param>
        /// <param name="osId">单据别</param>
        /// <param name="osNo">单号</param>
        /// <param name="IsDataSetSchema">取受订单框架</param>
        /// <param name="isContainSR">是否包含销货退回数量和相关表的虚拟字段</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string pgm, string osId, string osNo, bool IsDataSetSchema, bool isContainSR)
        {
            DbDrpSO _dbDrpSO = new DbDrpSO(Comp.Conn_DB);
            SunlikeDataSet _ds = null;
            _ds = _dbDrpSO.GetDrpSO(osId, osNo, IsDataSetSchema, isContainSR);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //增加单据权限
            if (!IsDataSetSchema)
            {
                if (usr != null)
                {
                    DataTable _dtMf = _ds.Tables["MF_POS"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["PO_DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
            }
            if (_ds.Tables.Contains("TF_POS"))
            {
                DataTable _TF_POSTable = _ds.Tables["TF_POS"];
                _TF_POSTable.Columns.Add("PRD_NO_NO", typeof(System.String));
                //转入单据的数量
                _TF_POSTable.Columns.Add("QTY_YH_ORG", typeof(System.Decimal));
                //受订退回单中记录原受订单数量
                _TF_POSTable.Columns.Add("V_QTY_SO", typeof(System.Decimal));
                //受订单中单位标准成本
                _TF_POSTable.Columns.Add("CST_STD_UNIT", typeof(System.Decimal));
                //联系人代号
                _TF_POSTable.Columns.Add("CNT_NO", typeof(System.String));
                //联系人
                _TF_POSTable.Columns.Add("CNT_NAME", typeof(System.String));
                //联系人电话 
                _TF_POSTable.Columns.Add("TEL_NO", typeof(System.String));
                //联系人手机
                _TF_POSTable.Columns.Add("CELL_NO", typeof(System.String));
                //联系人住址
                _TF_POSTable.Columns.Add("CNT_ADR", typeof(System.String));
                //联系人备注
                _TF_POSTable.Columns.Add("CNT_REM", typeof(System.String));
                //联系人别名
                _TF_POSTable.Columns.Add("OTH_NAME", typeof(System.String));
                //单位（副）
                _TF_POSTable.Columns.Add("UNIT_DP", typeof(String)).MaxLength = 8;
                DRPYHut _dbYh = new DRPYHut();
                DRPME _drpMe = new DRPME();
                foreach (DataRow _TF_POSRow in _TF_POSTable.Rows)
                {
                    //给单位标准成本赋值
                    if (!String.IsNullOrEmpty(_TF_POSRow["CST_STD"].ToString()) && !String.IsNullOrEmpty(_TF_POSRow["QTY"].ToString()) && Convert.ToDecimal(_TF_POSRow["QTY"]) != 0)
                    {
                        _TF_POSRow["CST_STD_UNIT"] = Convert.ToDecimal(_TF_POSRow["CST_STD"]) / Convert.ToDecimal(_TF_POSRow["QTY"]);
                    }
                    else
                        _TF_POSRow["CST_STD_UNIT"] = 0;
                    _TF_POSRow["PRD_NO_NO"] = _TF_POSRow["PRD_NO"];
                    if (osId == "SO")
                    {
                        if (!String.IsNullOrEmpty(_TF_POSRow["QT_NO"].ToString()) && !String.IsNullOrEmpty(_TF_POSRow["OTH_ITM"].ToString()))
                        {
                            string _maId = "";
                            string _maNo = "";
                            MTNMa _ma = new MTNMa();
                            if (string.Compare("RV", _TF_POSRow["BIL_ID"].ToString()) == 0 || string.Compare("MA", _TF_POSRow["BIL_ID"].ToString()) == 0 || string.Compare("EA", _TF_POSRow["BIL_ID"].ToString()) == 0 || string.Compare("PO", _TF_POSRow["BIL_ID"].ToString()) == 0)
                            {

                                if (string.Compare("RV", _TF_POSRow["BIL_ID"].ToString()) == 0)
                                {
                                    MTNRcv _rcv = new MTNRcv();
                                    SunlikeDataSet _dsRcvBody = _rcv.GetBody(_TF_POSRow["BIL_ID"].ToString(), _TF_POSRow["QT_NO"].ToString(), Convert.ToInt32(_TF_POSRow["OTH_ITM"].ToString()));
                                    if (_dsRcvBody.Tables["TF_RCV"].Rows.Count > 0 && !string.IsNullOrEmpty(_dsRcvBody.Tables["TF_RCV"].Rows[0]["QTY"].ToString()))
                                    {
                                        _maId = _dsRcvBody.Tables["TF_RCV"].Rows[0]["MA_ID"].ToString();
                                        _maNo = _dsRcvBody.Tables["TF_RCV"].Rows[0]["MA_NO"].ToString();
                                        Prdt _prdt = new Prdt();
                                        decimal _qtyMaLeft = 0;
                                        //维修申请数
                                        _qtyMaLeft = Convert.ToDecimal(_dsRcvBody.Tables["TF_RCV"].Rows[0]["QTY"]);
                                        //增加当前单数量
                                        if (!String.IsNullOrEmpty(_TF_POSRow["QTY"].ToString()))
                                        {
                                            _qtyMaLeft += Convert.ToDecimal(_TF_POSRow["QTY"].ToString());
                                        }
                                        //减已转量
                                        if (!string.IsNullOrEmpty(_dsRcvBody.Tables["TF_RCV"].Rows[0]["QTY_SO"].ToString()))
                                            _qtyMaLeft -= Convert.ToDecimal(_dsRcvBody.Tables["TF_RCV"].Rows[0]["QTY_SO"]); ;
                                        _TF_POSRow["QTY_YH_ORG"] = _prdt.GetUnitQty(_dsRcvBody.Tables["TF_RCV"].Rows[0]["PRD_NO"].ToString(), _dsRcvBody.Tables["TF_RCV"].Rows[0]["UNIT"].ToString(), _qtyMaLeft, _TF_POSRow["UNIT"].ToString());
                                    }
                                    DataTable _dtRcvHead = _rcv.GetUpdateData("", "", "RV", _TF_POSRow["QT_NO"].ToString(), false).Tables["MF_RCV"];
                                    if (_dtRcvHead.Rows.Count > 0)
                                    {
                                        _TF_POSRow["CNT_NO"] = _dtRcvHead.Rows[0]["CNT_NO"];
                                        _TF_POSRow["CNT_NAME"] = _dtRcvHead.Rows[0]["CNT_NAME"];
                                        _TF_POSRow["TEL_NO"] = _dtRcvHead.Rows[0]["TEL_NO"];
                                        _TF_POSRow["CELL_NO"] = _dtRcvHead.Rows[0]["CELL_NO"];
                                        _TF_POSRow["CNT_ADR"] = _dtRcvHead.Rows[0]["CNT_ADR"];
                                        _TF_POSRow["CNT_REM"] = _dtRcvHead.Rows[0]["CNT_REM"];
                                        _TF_POSRow["OTH_NAME"] = _dtRcvHead.Rows[0]["OTH_NAME"];
                                    }

                                }
                                else if (string.Compare("MA", _TF_POSRow["BIL_ID"].ToString()) == 0)
                                {
                                    _maId = _TF_POSRow["BIL_ID"].ToString();
                                    _maNo = _TF_POSRow["QT_NO"].ToString();
                                    SunlikeDataSet _dsMaBody = _ma.GetData(_maId, _maNo, Convert.ToInt32(_TF_POSRow["OTH_ITM"].ToString()));
                                    if (_dsMaBody.Tables["TF_MA"].Rows.Count > 0)
                                    {
                                        Prdt _prdt = new Prdt();
                                        decimal _qtyMaLeft = 0;
                                        //维修申请数
                                        _qtyMaLeft = Convert.ToDecimal(_dsMaBody.Tables["TF_MA"].Rows[0]["QTY"]);
                                        //增加当前单数量
                                        if (!String.IsNullOrEmpty(_TF_POSRow["QTY"].ToString()))
                                        {
                                            _qtyMaLeft += Convert.ToDecimal(_TF_POSRow["QTY"].ToString());
                                        }
                                        //减已转量
                                        if (!string.IsNullOrEmpty(_dsMaBody.Tables["TF_MA"].Rows[0]["QTY_MTN"].ToString()))
                                            _qtyMaLeft -= Convert.ToDecimal(_dsMaBody.Tables["TF_MA"].Rows[0]["QTY_MTN"]); ;
                                        _TF_POSRow["QTY_YH_ORG"] = _prdt.GetUnitQty(_dsMaBody.Tables["TF_MA"].Rows[0]["PRD_NO"].ToString(), _dsMaBody.Tables["TF_MA"].Rows[0]["UNIT"].ToString(), _qtyMaLeft, _TF_POSRow["UNIT"].ToString());
                                    }
                                    if (!string.IsNullOrEmpty(_maId) && !string.IsNullOrEmpty(_maNo))
                                    {
                                        SunlikeDataSet _dsMa = _ma.GetUpdateData("", "", _maId, _maNo, false);
                                        if (_dsMa.Tables["MF_MA"].Rows.Count > 0)
                                        {
                                            _TF_POSRow["CNT_NO"] = _dsMa.Tables["MF_MA"].Rows[0]["CNT_NO"];
                                            _TF_POSRow["CNT_NAME"] = _dsMa.Tables["MF_MA"].Rows[0]["CNT_NAME"];
                                            _TF_POSRow["TEL_NO"] = _dsMa.Tables["MF_MA"].Rows[0]["TEL_NO"];
                                            _TF_POSRow["CELL_NO"] = _dsMa.Tables["MF_MA"].Rows[0]["CELL_NO"];
                                            _TF_POSRow["CNT_ADR"] = _dsMa.Tables["MF_MA"].Rows[0]["CNT_ADR"];
                                            _TF_POSRow["CNT_REM"] = _dsMa.Tables["MF_MA"].Rows[0]["CNT_REM"];
                                            _TF_POSRow["OTH_NAME"] = _dsMa.Tables["MF_MA"].Rows[0]["OTH_NAME"];
                                        }
                                    }
                                }
                                else if (_TF_POSRow["BIL_ID"].ToString() == "EA")
                                {
                                    using (SunlikeDataSet _dsEa = _drpMe.GetBodyData("EA", _TF_POSRow["QT_NO"].ToString(),
                                        Convert.ToInt32(_TF_POSRow["OTH_ITM"])))
                                    {
                                        if (_ds.Tables[0].Rows.Count > 0)
                                        {
                                            Prdt _prdt = new Prdt();
                                            decimal _qtyEaLeft = 0;
                                            //营销费用申请数
                                            _qtyEaLeft = Convert.ToDecimal(_dsEa.Tables[0].Rows[0]["QTY"]);
                                            //增加当前单数量
                                            if (!String.IsNullOrEmpty(_TF_POSRow["QTY"].ToString()))
                                            {
                                                _qtyEaLeft += Convert.ToDecimal(_TF_POSRow["QTY"].ToString());
                                            }
                                            //减已转量
                                            if (!string.IsNullOrEmpty(_dsEa.Tables[0].Rows[0]["QTY_SO"].ToString()))
                                                _qtyEaLeft -= Convert.ToDecimal(_dsEa.Tables[0].Rows[0]["QTY_SO"]);
                                            _TF_POSRow["QTY_YH_ORG"] = _prdt.GetUnitQty(_dsEa.Tables[0].Rows[0]["PRD_NO"].ToString(), "1", _qtyEaLeft, _TF_POSRow["UNIT"].ToString());
                                        }
                                    }
                                }
                                else if (_TF_POSRow["BIL_ID"].ToString() == "PO")
                                {
                                    DRPPO _po = new DRPPO();
                                    using (SunlikeDataSet _dsPo = _po.GetBody(_TF_POSRow["BIL_ID"].ToString(), _TF_POSRow["QT_NO"].ToString(), "EST_ITM", Convert.ToInt32(_TF_POSRow["OTH_ITM"].ToString()), false))
                                    {
                                        if (_dsPo.Tables[0].Rows.Count > 0)
                                        {
                                            Prdt _prdt = new Prdt();
                                            decimal _qtyPoLeft = 0;
                                            //原采购量
                                            _qtyPoLeft = Convert.ToDecimal(_dsPo.Tables[0].Rows[0]["QTY"]);
                                            //增加当前单数量
                                            if (!String.IsNullOrEmpty(_TF_POSRow["QTY"].ToString()))
                                            {
                                                _qtyPoLeft += Convert.ToDecimal(_TF_POSRow["QTY"].ToString());
                                            }
                                            //减已转量
                                            if (!string.IsNullOrEmpty(_dsPo.Tables[0].Rows[0]["QTY_PO"].ToString()))
                                                _qtyPoLeft -= Convert.ToDecimal(_dsPo.Tables[0].Rows[0]["QTY_PO"]);
                                            if (!string.IsNullOrEmpty(_dsPo.Tables[0].Rows[0]["QTY_PO_UNSH"].ToString()))
                                                _qtyPoLeft -= Convert.ToDecimal(_dsPo.Tables[0].Rows[0]["QTY_PO_UNSH"]);
                                            _TF_POSRow["QTY_YH_ORG"] = _prdt.GetUnitQty(_dsPo.Tables[0].Rows[0]["PRD_NO"].ToString(), "1", _qtyPoLeft, _TF_POSRow["UNIT"].ToString());
                                        }
                                    }
                                }
                                
                            }
                            else
                            {
                                _TF_POSRow["QTY_YH_ORG"] = _dbYh.GetOrgYhQty(_TF_POSRow["BIL_ID"].ToString(), _TF_POSRow["QT_NO"].ToString(), _TF_POSRow["OTH_ITM"].ToString(), true);
                            }
                        }
                    }
                    if (osId == "SR")
                    {
                        if (!String.IsNullOrEmpty(_TF_POSRow["QT_NO"].ToString()) && !String.IsNullOrEmpty(_TF_POSRow["PRE_ITM"].ToString()))
                        {
                            _TF_POSRow["V_QTY_SO"] = _dbDrpSO.GetOrgSoQty("SO", _TF_POSRow["QT_NO"].ToString(), "PRE_ITM", Convert.ToInt32(_TF_POSRow["PRE_ITM"]));
                        }
                    }
                }
                if (osId == "SO")
                {
                    _TF_POSTable.Columns["PRE_ITM"].AutoIncrement = true;
                    _TF_POSTable.Columns["PRE_ITM"].AutoIncrementSeed = _TF_POSTable.Rows.Count > 0 ? Convert.ToInt32(_TF_POSTable.Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
                    _TF_POSTable.Columns["PRE_ITM"].AutoIncrementStep = 1;
                    _TF_POSTable.Columns["PRE_ITM"].Unique = true;
                }
                else if (osId == "SR")
                {
                    _TF_POSTable.Columns["EST_ITM"].AutoIncrement = true;
                    _TF_POSTable.Columns["EST_ITM"].AutoIncrementSeed = _TF_POSTable.Rows.Count > 0 ? Convert.ToInt32(_TF_POSTable.Select("", "EST_ITM desc")[0]["EST_ITM"]) + 1 : 1;
                    _ds.Tables["TF_POS"].Columns["EST_ITM"].AutoIncrementStep = 1;
                    _TF_POSTable.Columns["EST_ITM"].Unique = true;
                }
            }

            //取得箱信息
            if (_ds.Tables.Contains("TF_POS1"))
            {
                DataTable _TF_POS1Table = _ds.Tables["TF_POS1"];
                //设定箱内容KeyItm为自动递增
                _TF_POS1Table.Columns["KEY_ITM"].AutoIncrement = true;
                _TF_POS1Table.Columns["KEY_ITM"].AutoIncrementSeed = _TF_POS1Table.Rows.Count > 0 ? Convert.ToInt32(_TF_POS1Table.Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
                _TF_POS1Table.Columns["KEY_ITM"].AutoIncrementStep = 1;
                _TF_POS1Table.Columns["KEY_ITM"].Unique = true;
                _TF_POS1Table.Columns.Add("PRD_NO_NO", typeof(System.String));
                DataColumn _dc = new DataColumn("CONTENT_DSC", Type.GetType("System.String"));
                _dc.MaxLength = 64 * 1024;
                _TF_POS1Table.Columns.Add(_dc);

                Sunlike.Business.BarBox _bar = new Sunlike.Business.BarBox();
                foreach (DataRow _TF_POS1Row in _TF_POS1Table.Rows)
                {
                    _TF_POS1Row["CONTENT_DSC"] = _bar.GetBar_BoxDsc(_TF_POS1Row["PRD_NO"].ToString(), _TF_POS1Row["CONTENT"].ToString());
                    _TF_POS1Row["PRD_NO_NO"] = _TF_POS1Row["PRD_NO"];
                }

            }
            SetCanModify(_ds, true);

            //获取到货确认信息
            string _userCfmSwFlag = Users.GetSpcPswdString(usr, "CFM_SW_FLAG");//特殊密码-到货确认管控否
            if (!String.IsNullOrEmpty(_userCfmSwFlag) && _userCfmSwFlag == "T")
            {
                string _userCfmSwControl = Users.GetSpcPswdString(usr, "CFM_SW_CONTROL");//特殊密码-到货管控方式
                if (!String.IsNullOrEmpty(_userCfmSwControl))
                {
                    _ds.ExtendedProperties["CFM_SW_CONTROL"] = _userCfmSwControl;//参数设定-到货管控方式
                }
            }
            else
            {
                _ds.ExtendedProperties["CFM_SW_CONTROL"] = Comp.DRP_Prop["CFM_SW_CONTROL"];
            }
            return _ds;

        }

        /// <summary>
        /// 查询受订单表头
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        /// <returns></returns>
        public DataTable GetDataMf(string os_id, string os_no)
        {
            DbDrpSO _dbDrpSO = new DbDrpSO(Comp.Conn_DB);
            return _dbDrpSO.SelectDrpMf_Pos(os_id, os_no);
        }
        /// <summary>
        /// 根据要货单号查询受订单
        /// </summary>
        /// <param name="yhNo">要货单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetOsNo4YH(string yhNo)
        {
            DbDrpSO _dbDrpSO = new DbDrpSO(Comp.Conn_DB);
            return _dbDrpSO.GetOsNo4YH(yhNo);
        }
        /// <summary>
        /// 检查要货单对应的受订单是否已经进入审核流程或结案
        /// </summary>
        /// <param name="yhNo"></param>
        /// <returns></returns>
        public bool CheckOsModify(string yhNo)
        {
            SunlikeDataSet _ds = this.GetOsNo4YH(yhNo);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                DataRow _dr = _ds.Tables[0].Rows[0];
                //是否已经进入审核流程
                Auditing _aut = new Auditing();
                bool _runAuding = false;

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
                //_runAuding = _aut.IsRunAuditing("SO", _dr["USR"].ToString(), _bilType,_mobID);

                if (_runAuding)
                {
                    if (_aut.GetIfEnterAuditing("SO", _dr["OS_NO"].ToString()))
                    {
                        return false;
                    }
                }
                //是否转配送
                if (_ds.Tables.Contains("TF_POS") && _ds.Tables["TF_POS"].Rows.Count > 0
                    && ((!String.IsNullOrEmpty(_ds.Tables["TF_POS"].Rows[0]["QTY_IC"].ToString()) && Convert.ToDecimal(_ds.Tables["TF_POS"].Rows[0]["QTY_IC"]) != 0)
                    || (!String.IsNullOrEmpty(_ds.Tables["TF_POS"].Rows[0]["QTY_RK"].ToString()) && Convert.ToDecimal(_ds.Tables["TF_POS"].Rows[0]["QTY_RK"]) != 0)
                    || (!String.IsNullOrEmpty(_ds.Tables["TF_POS"].Rows[0]["QTY_PS"].ToString()) && Convert.ToDecimal(_ds.Tables["TF_POS"].Rows[0]["QTY_PS"]) != 0)
                    || (!String.IsNullOrEmpty(_ds.Tables["TF_POS"].Rows[0]["QTY_PRE"].ToString()) && Convert.ToDecimal(_ds.Tables["TF_POS"].Rows[0]["QTY_PRE"]) != 0)
                    || (!String.IsNullOrEmpty(_ds.Tables["TF_POS"].Rows[0]["QTY_RK_UNSH"].ToString()) && Convert.ToDecimal(_ds.Tables["TF_POS"].Rows[0]["QTY_RK_UNSH"]) != 0)))
                {
                    return false;
                }
                //是否结案
                if (_ds.Tables.Contains("MF_POS") && _ds.Tables["MF_POS"].Rows[0]["CLS_ID"].ToString() == "T")
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 取得表身数据(TF_POS)
        /// 查询字段: ITM,OS_ID,OS_NO,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY,UP,DIS_CNT,AMT,AMTN,TAX_RTO,TAX,PRE_ITM,
        /// QTY_PS,EST_DD,CST_STD,QTY_PO,QTY_PRE,QTY_RK,OS_DD,BOX_ITM,REM,BIL_ID,EST_ITM,CUS_OS_NO,QT_NO,OTH_ITM
        /// </summary>
        /// <param name="osId">单据别</param>
        /// <param name="osNo">单号</param>
        /// <param name="itmColumnName">项次</param>
        /// <param name="itm">项值</param>\
        /// <param name="isPrimaryUnit">是否换算成主单位</param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string osId, string osNo, string itmColumnName, int itm, bool isPrimaryUnit)
        {
            Sunlike.Business.Data.DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            return _so.GetBody(osId, osNo, itmColumnName, itm, isPrimaryUnit);
        }

        #region 取得受订单内容
        /// <summary>
        /// 取得受订单内容(只包含SO且包含配码比描述)
        /// </summary>
        /// <param name="AryOsNo">受订单号</param>
        /// <returns></returns>
        public SunlikeDataSet SelectDrpSO(string[] AryOsNo)
        {
            StringBuilder _where = new StringBuilder();
            for (int i = 0; i < AryOsNo.Length; i++)
            {
                if (!String.IsNullOrEmpty(_where.ToString()))
                {
                    _where.Append(",");
                }
                _where.Append("'" + AryOsNo[i] + "'");
            }
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            SunlikeDataSet _ds = _so.SelectBodyDrpSO(_where.ToString());
            //取得配码比描述
            DataTable _dtBox = _ds.Tables["TF_POS1"];
            DataColumn _dc = new DataColumn("CONTENT_DSC");
            _dtBox.Columns.Add(_dc);
            BarBox _box = new BarBox();
            for (int i = 0; i < _dtBox.Rows.Count; i++)
            {
                DataRow _drBox = _dtBox.Rows[i];
                string _boxNo = _box.GetBar_Box_No(_drBox["PRD_NO"].ToString(), _drBox["CONTENT"].ToString());
                DataTable _dtDetial = _box.GetBar_BoxDetail(_boxNo);
                StringBuilder _contentDsc = new StringBuilder();
                for (int j = 0; j < _dtDetial.Rows.Count; j++)
                {
                    if (j > 0)
                    {
                        _contentDsc.Append(";");
                    }
                    DataRow _drDetail = _dtDetial.Rows[j];
                    for (int k = 8; k < _dtDetial.Columns.Count; k += 2)
                    {
                        _contentDsc.Append(_drDetail[k + 1].ToString());
                    }
                    _contentDsc.Append("=" + _drDetail["PRD_QTY"].ToString() + _drDetail["UT"].ToString());
                }
                _drBox["CONTENT_DSC"] = _contentDsc;
            }
            _dtBox.AcceptChanges();
            return _ds;
        }
        #endregion

        #region 查询银行付款单号信息
        /// <summary>
        ///  查询银行付款单号信息
        /// </summary>
        /// <param name="payNo">银行付款单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetPayB2C(string payNo)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            SunlikeDataSet _ds = _so.GetPayB2C(payNo);
            return _ds;
        }
        #endregion

        #endregion

        #region InsertDrpSO(要货)
        /// <summary>
        /// InsertDrpSO
        /// </summary>
        /// <param name="YHDataSet"></param>
        public void InsertDrpSO(SunlikeDataSet YHDataSet)
        {
            //判断是否包下属储位
            if (Comp.DRP_Prop["DRPYH_WH_VIEWUNDER"].ToString() == "F")
            {
                _isUnder = false;
            }
            else
            {
                _isUnder = true;
            }
            _isFromYH = true;
            DataTable _MF_DYHTable = YHDataSet.Tables["MF_DYH"];
            DataTable _TF_DYHTable = YHDataSet.Tables["TF_DYH"];
            DataTable _TF_DYH1Table = YHDataSet.Tables["TF_DYH1"];
            string _userId = "";
            if (_MF_DYHTable.Rows.Count <= 0)
            {
                throw new SunlikeException("MF_DYH is empty ");
            }
            _userId = _MF_DYHTable.Rows[0]["USR"].ToString();
            SunlikeDataSet DataSetSO = this.GetData(_userId, "SO", "", true, false);
            //-----------------受订单表头开始------------------
            if (_MF_DYHTable.Rows.Count <= 0)
            {
                throw new SunlikeException("MF_DYH is empty ");
            }
            _userId = _MF_DYHTable.Rows[0]["USR"].ToString();
            string _dept = "";
            Users SunlikeUsers = new Users();
            _dept = SunlikeUsers.GetUserDepNo(_userId);
            DateTime _timeOs_dd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
            _timeOs_dd = Convert.ToDateTime(_MF_DYHTable.Rows[0]["YH_DD"]);
            string _bil_type = "";
            string _os_no = "";
            string _cusNo = "";
            decimal _excRto = 1;
            //-----------------受订单表头开始------------------
            DataTable _MF_POSTable = DataSetSO.Tables["MF_POS"];
            DataRow _mf_posRow = _MF_POSTable.NewRow();
            _mf_posRow["OS_DD"] = _timeOs_dd.ToShortDateString();
            _mf_posRow["OS_ID"] = "SO";
            _mf_posRow["OS_NO"] = _os_no;
            _mf_posRow["BIL_TYPE"] = _bil_type;

            _cusNo = _MF_DYHTable.Rows[0]["CUS_NO"].ToString();
            _mf_posRow["CUS_NO"] = _cusNo;
            _mf_posRow["YH_NO"] = _MF_DYHTable.Rows[0]["YH_NO"];
            _yh_no = _MF_DYHTable.Rows[0]["YH_NO"].ToString();
            _mf_posRow["FX_WH"] = _MF_DYHTable.Rows[0]["FX_WH"];
            _mf_posRow["BYBOX"] = _MF_DYHTable.Rows[0]["BYBOX"];
            _mf_posRow["REM"] = _MF_DYHTable.Rows[0]["REM"];
            _mf_posRow["CHK_MAN"] = _MF_DYHTable.Rows[0]["CHK_MAN"];
            _mf_posRow["CLS_DATE"] = _MF_DYHTable.Rows[0]["CLS_DATE"];
            _mf_posRow["BIL_TYPE"] = _MF_DYHTable.Rows[0]["BIL_TYPE"];

            Cust SunlikeCust = new Cust();
            SunlikeDataSet _dsCust = new SunlikeDataSet();
            _dsCust.Merge(SunlikeCust.GetData(_MF_DYHTable.Rows[0]["CUS_NO"].ToString()));
            DataTable _custTable = _dsCust.Tables["CUST"];
            string _custSal_no = "";
            string _cur_id = "";
            string _id1_tax = "0";//扣税类型
            int _rto_tax = 0;
            if (_custTable.Rows.Count > 0)
            {
                _custSal_no = _custTable.Rows[0]["SAL"].ToString();
                _cur_id = _custTable.Rows[0]["CUR"].ToString();
                _id1_tax = _custTable.Rows[0]["ID1_TAX"].ToString();
            }
            //汇率计算
            if (!String.IsNullOrEmpty(_cur_id))
            {
                string _sExcRto = "";
                Currency _currency = new Sunlike.Business.Currency();
                _sExcRto = _currency.GetExcRto(_cur_id, 0);
                if (!String.IsNullOrEmpty(_sExcRto))
                    _excRto = Convert.ToDecimal(_sExcRto);
            }
            _mf_posRow["EXC_RTO"] = _excRto;
            _mf_posRow["SAL_NO"] = _custSal_no;
            _mf_posRow["PO_DEP"] = _dept;
            _mf_posRow["EST_DD"] = _MF_DYHTable.Rows[0]["EST_DD"];
            _mf_posRow["CUR_ID"] = _cur_id;
            _mf_posRow["TAX_ID"] = _id1_tax;//扣税类型
            _mf_posRow["USR"] = _MF_DYHTable.Rows[0]["USR"];
            Cust _sunlikeCust = new Cust();
            System.Collections.Hashtable _ht = _sunlikeCust.GetPAYInfo(_MF_DYHTable.Rows[0]["CUS_NO"].ToString(), _timeOs_dd.ToString(Comp.SQLDateFormat));
            _mf_posRow["PAY_DD"] = _ht["PAY_DD"];
            _mf_posRow["CHK_DD"] = _ht["CHK_DD"];
            if (_MF_DYHTable.Rows[0]["YH_ID"].ToString() != "YN")
            {
                //取客户默认的送货方式
                if (!(_ht["SEND_MTH"] is System.DBNull) && !String.IsNullOrEmpty(_ht["SEND_MTH"].ToString()))
                    _mf_posRow["SEND_MTH"] = _ht["SEND_MTH"];
                else
                    _mf_posRow["SEND_MTH"] = "1";
                _mf_posRow["SEND_WH"] = _ht["SEND_WH"];
            }
            else
            {
                _mf_posRow["SEND_MTH"] = _MF_DYHTable.Rows[0]["SEND_MTH"];
                _mf_posRow["SEND_WH"] = _MF_DYHTable.Rows[0]["SEND_WH"];
            }

            _mf_posRow["PAY_MTH"] = _ht["PAY_MTH"];
            _mf_posRow["PAY_DAYS"] = _ht["PAY_DAYS"];
            _mf_posRow["CHK_DAYS"] = _ht["CHK_DAYS"];
            _mf_posRow["INT_DAYS"] = _ht["INT_DAYS"];
            _mf_posRow["PAY_REM"] = _ht["PAY_REM"];
            _MF_POSTable.Rows.Add(_mf_posRow);

            //-----------------受订单表头结束------------------

            DataTable _TF_POSTable = DataSetSO.Tables["TF_POS"];
            //-----------------受订单表身开始------------------
            //_TF_DYHTable
            decimal _amtn = 0;
            decimal _tax = 0;
            int _itmRow = 0;
            for (int i = 0; i < _TF_DYHTable.Rows.Count; i++)
            {
                if (_TF_DYHTable.Rows[i]["DEL_ID"].ToString() != "T")
                {
                    DataRow _tf_posRow = _TF_POSTable.NewRow();
                    _tf_posRow["OS_DD"] = _timeOs_dd.ToString(Comp.SQLDateFormat);
                    _tf_posRow["OS_ID"] = "SO";
                    _tf_posRow["OS_NO"] = _os_no;
                    _tf_posRow["BIL_ID"] = _TF_DYHTable.Rows[i]["YH_ID"];
                    _tf_posRow["QT_NO"] = _TF_DYHTable.Rows[i]["YH_NO"];
                    _tf_posRow["OTH_ITM"] = _TF_DYHTable.Rows[i]["KEY_ITM"];
                    _tf_posRow["EST_ITM"] = _TF_DYHTable.Rows[i]["ITM"];
                    _itmRow++;
                    _tf_posRow["ITM"] = _itmRow;
                    _tf_posRow["PRD_NO"] = _TF_DYHTable.Rows[i]["PRD_NO"];
                    if (!String.IsNullOrEmpty(_TF_DYHTable.Rows[i]["PRD_NAME"].ToString()))
                    {
                        _tf_posRow["PRD_NAME"] = _TF_DYHTable.Rows[i]["PRD_NAME"];
                    }
                    else
                    {
                        Prdt _prdt = new Prdt();
                        DataTable _prdtTable = _prdt.GetPrdt(_TF_DYHTable.Rows[i]["PRD_NO"].ToString());
                        if (_prdtTable.Rows.Count > 0)
                        {
                            _tf_posRow["PRD_NAME"] = _prdtTable.Rows[0]["NAME"].ToString();
                        }
                    }
                    _tf_posRow["WH"] = _TF_DYHTable.Rows[i]["WH"];
                    _tf_posRow["UNIT"] = _TF_DYHTable.Rows[i]["UNIT"];
                    _tf_posRow["QTY"] = _TF_DYHTable.Rows[i]["QTY"];
                    if (!(_TF_DYHTable.Rows[i]["UP"] is System.DBNull))
                    {
                        _tf_posRow["UP"] = Convert.ToDecimal(_TF_DYHTable.Rows[i]["UP"]) / _excRto;
                    }
                    if (!(_TF_DYHTable.Rows[i]["AMTN"] is System.DBNull))
                    {

                        Sunlike.Business.Tax _drpTax = new Tax();
                        _rto_tax = Convert.ToInt32(_drpTax.GetTax(_TF_DYHTable.Rows[i]["PRD_NO"].ToString(), _dept, _cusNo));
                        GetTaxAndAmtn(_id1_tax, Convert.ToDecimal(_TF_DYHTable.Rows[i]["AMTN"]), _rto_tax, out _amtn, out _tax);
                        _tf_posRow["AMTN"] = _amtn;
                        _tf_posRow["AMT"] = _amtn / _excRto;
                        _tf_posRow["TAX"] = _tax;
                        _tf_posRow["TAX_RTO"] = _rto_tax;
                    }

                    _tf_posRow["EST_DD"] = _TF_DYHTable.Rows[i]["EST_DD"];
                    _tf_posRow["PRD_MARK"] = _TF_DYHTable.Rows[i]["PRD_MARK"];
                    _tf_posRow["BOX_ITM"] = _TF_DYHTable.Rows[i]["BOX_ITM"];
                    _tf_posRow["EST_DD"] = _TF_DYHTable.Rows[i]["EST_DD"];
                    _tf_posRow["REM"] = _TF_DYHTable.Rows[i]["REM"];

                    //标准成本
                    StdCst _std = new StdCst();
                    _tf_posRow["CST_STD_UNIT"] = _std.GetUP_STD(_tf_posRow["PRD_NO"].ToString(),
                        _tf_posRow["PRD_MARK"].ToString(), _tf_posRow["WH"].ToString());
                    if (!String.IsNullOrEmpty(_tf_posRow["CST_STD_UNIT"].ToString())
                        && !String.IsNullOrEmpty(_tf_posRow["QTY"].ToString()))
                    {
                        _tf_posRow["CST_STD"] = Convert.ToDecimal(_tf_posRow["CST_STD_UNIT"]) * Convert.ToDecimal(_tf_posRow["QTY"]);
                    }

                    #region 包装单位
                    DataTable _dtprdt = new Prdt().GetFullPrdt(_tf_posRow["PRD_NO"].ToString());
                    if (_dtprdt.Rows.Count > 0)
                    {
                        DataRow _drPrdt = _dtprdt.Rows[0];
                        decimal _pakExc = 0;
                        decimal _qty = 0;
                        if (!String.IsNullOrEmpty(_drPrdt["PAK_EXC"].ToString()))
                        {
                            _pakExc = Convert.ToDecimal(_drPrdt["PAK_EXC"]);
                        }

                        if (!String.IsNullOrEmpty(_tf_posRow["QTY"].ToString()))
                        {
                            _qty = Convert.ToDecimal(_tf_posRow["QTY"]);
                        }
                        if (_qty != 0 && _pakExc != 0)
                        {
                            _tf_posRow["PAK_WEIGHT_UNIT"] = _drPrdt["PAK_WEIGHT_UNIT"];
                            _tf_posRow["PAK_MEAST_UNIT"] = _drPrdt["PAK_MEAST_UNIT"];
                            _tf_posRow["PAK_EXC"] = _pakExc;
                            decimal _pakUnit = Math.Round(_qty / _pakExc, 8);
                            _tf_posRow["PAK_UNIT"] = _pakUnit.ToString() + _drPrdt["PAK_UNIT"].ToString();
                            //大小
                            if (!String.IsNullOrEmpty(_drPrdt["PAK_MEAST"].ToString()))
                            {
                                _tf_posRow["PAK_MEAST"] = _pakUnit * Convert.ToDecimal(_drPrdt["PAK_MEAST"]);
                            }
                            //净重
                            if (!String.IsNullOrEmpty(_drPrdt["PAK_GW"].ToString()))
                            {
                                _tf_posRow["PAK_GW"] = _pakUnit * Convert.ToDecimal(_drPrdt["PAK_GW"]);
                            }
                            //毛重
                            if (!String.IsNullOrEmpty(_drPrdt["PAK_NW"].ToString()))
                            {
                                _tf_posRow["PAK_NW"] = _pakUnit * Convert.ToDecimal(_drPrdt["PAK_NW"]);
                            }
                        }
                    }
                    #endregion

                    _TF_POSTable.Rows.Add(_tf_posRow);
                }
            }
            //-----------------受订单表身结束------------------
            DataTable _TF_POS1Table = DataSetSO.Tables["TF_POS1"];
            //-----------------受订单配码比开始------------------
            _itmRow = 0;
            if (YHDataSet.Tables.Contains("TF_DYH1"))
            {
                for (int i = 0; i < _TF_DYH1Table.Rows.Count; i++)
                {
                    if (_TF_DYH1Table.Rows[i]["DEL_ID"].ToString() != "T")
                    {
                        DataRow _tf_pos1Row = _TF_POS1Table.NewRow();

                        _tf_pos1Row["OS_ID"] = "SO";
                        _tf_pos1Row["OS_NO"] = _os_no;
                        _itmRow++;
                        _tf_pos1Row["ITM"] = _itmRow;
                        _tf_pos1Row["PRD_NO"] = _TF_DYH1Table.Rows[i]["PRD_NO"];
                        _tf_pos1Row["CONTENT"] = _TF_DYH1Table.Rows[i]["CONTENT"];
                        _tf_pos1Row["QTY"] = _TF_DYH1Table.Rows[i]["QTY"];
                        _tf_pos1Row["KEY_ITM"] = _TF_DYH1Table.Rows[i]["KEY_ITM"];
                        _tf_pos1Row["WH"] = _TF_DYH1Table.Rows[i]["WH"];
                        _tf_pos1Row["BIL_ID"] = _TF_DYH1Table.Rows[i]["YH_ID"];
                        _tf_pos1Row["EST_ITM"] = _TF_DYH1Table.Rows[i]["ITM"];
                        _tf_pos1Row["EST_DD"] = _TF_DYH1Table.Rows[i]["EST_DD"];
                        _TF_POS1Table.Rows.Add(_tf_pos1Row);
                    }
                }
            }
            //-----------------受订单配码比结束------------------

            //----------------取得箱及货品数量开始----------------
            decimal _qtyBox = 0;
            for (int i = 0; i < _TF_POS1Table.Rows.Count; i++)
            {
                if (!(_TF_POS1Table.Rows[i]["KEY_ITM"] is System.DBNull) || (_TF_POS1Table.Rows[i]["KEY_ITM"].ToString() != "0"))
                {
                    if (!(_TF_POS1Table.Rows[i]["QTY"] is System.DBNull))
                    {
                        _qtyBox += Convert.ToDecimal(_TF_POS1Table.Rows[i]["QTY"]);
                    }
                }
            }
            decimal _qtyPrd = 0;
            for (int i = 0; i < _TF_POSTable.Rows.Count; i++)
            {
                if (!(_TF_POSTable.Rows[i]["QTY"] == System.DBNull.Value))
                {
                    _qtyPrd += Convert.ToDecimal(_TF_POSTable.Rows[i]["QTY"]);
                }
            }
            if (_MF_POSTable.Rows.Count > 0)
            {
                _MF_POSTable.Rows[0]["TOT_BOX"] = _qtyBox;
                _MF_POSTable.Rows[0]["TOT_QTY"] = _qtyPrd;
            }
            //----------------取得箱及货品数量结束----------------

            #region 取得单据的审核状态
            Auditing _auditing = new Auditing();
            if (_MF_POSTable.Rows.Count > 0)
                _loginUsr = _MF_POSTable.Rows[0]["USR"].ToString();

            DataRow _dr = _MF_POSTable.Rows[0];
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
            //_isRunAuditing = _auditing.IsRunAuditing("SO", _loginUsr, _bilType,_mobID);


            if (_isRunAuditing)//如果需要审核的话就清空审核人
            {
                if (_MF_POSTable.Rows.Count > 0)
                {
                    _MF_POSTable.Rows[0]["CHK_MAN"] = System.DBNull.Value;
                    _MF_POSTable.Rows[0]["CLS_DATE"] = System.DBNull.Value;
                }
            }

            #endregion

            this.UpdateData("DRPSO",DataSetSO, true);
            _isFromYH = false;
        }

        #endregion

        #region 保存
        #region 保存受订单
        /// <summary>
        /// 保存受订单
        /// 扩张属性说明:
        /// SAVE_ID 缓存 (是:F 否:T)
        /// GET_SQNO  是否取单号 (取单号:T或没有的情况，否：F)
        /// CONTROL_CUS_WH　是否限制客户所属库位（管制:0 不管制：1）
        /// </summary>
        /// <param name="changedDs">DataSet</param>
        /// <param name="bubbleException">是否抛出异常（true为直接抛出异常，false返回ErrorTable）</param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm,SunlikeDataSet changedDs, bool bubbleException)
        {
            bool _isCusChange = false;
            DataTable MF_POSTable = changedDs.Tables["MF_POS"];
            DataTable TF_POSTable = changedDs.Tables["TF_POS"];
            #region 取得单据的审核状态
            if (MF_POSTable.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = MF_POSTable.Rows[0]["USR"].ToString();
                _osId = MF_POSTable.Rows[0]["OS_ID"].ToString();
                if (!string.IsNullOrEmpty(MF_POSTable.Rows[0]["ISSVS"].ToString()) && MF_POSTable.Rows[0]["ISSVS"].ToString() == "T")
                {
                    _isSvs = true;
                }
            }
            else
            {
                _loginUsr = MF_POSTable.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
                _osId = MF_POSTable.Rows[0]["OS_ID", System.Data.DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(MF_POSTable.Rows[0]["ISSVS", System.Data.DataRowVersion.Original].ToString()) && MF_POSTable.Rows[0]["ISSVS", System.Data.DataRowVersion.Original].ToString() == "T")
                {
                    _isSvs = true;
                }
            }
            Auditing _auditing = new Auditing();
            DataRow _dr=MF_POSTable.Rows[0];
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
            //_isRunAuditing = _auditing.IsRunAuditing(_osId, _loginUsr, _bilType,_mobID);


            #endregion
            //是否含下属库位
            Sunlike.Business.UserProperty _usrProp = new UserProperty();
            string _strChkUnder = _usrProp.GetData(_loginUsr, pgm, "CHK_UNDER");
            if (_strChkUnder == "T")
            {
                _isUnder = true;
            }
            //判断来源单是否需要缓存 SAVE_ID=F为缓存(不需要写审核流程并且不添审核人)　否则　为不走审核流程
            if (changedDs.ExtendedProperties.Contains("SAVE_ID"))
            {
                _saveId = changedDs.ExtendedProperties["SAVE_ID"].ToString();
            }
            //判断是否要限制客户所属库位
            if (changedDs.ExtendedProperties.Contains("CONTROL_CUS_WH"))
            {
                this._controlCusWh = changedDs.ExtendedProperties["CONTROL_CUS_WH"].ToString();
            }
            else
            {
                if (_usrProp.GetData(_loginUsr, pgm, "CONTROL_CUS_WH") != "")
                    this._controlCusWh = _usrProp.GetData(_loginUsr, pgm, "CONTROL_CUS_WH");
            }
            //得到要货单							
            if (changedDs.Tables.Contains("MF_POS"))
            {
                if (changedDs.Tables["MF_POS"].Rows[0].RowState != DataRowState.Deleted)
                {
                    if (!String.IsNullOrEmpty(changedDs.Tables["MF_POS"].Rows[0]["YH_NO"].ToString()))
                    {
                        _yh_no = changedDs.Tables["MF_POS"].Rows[0]["YH_NO"].ToString();
                    }
                }
                else
                {
                    _yh_no = changedDs.Tables["MF_POS"].Rows[0]["YH_NO", DataRowVersion.Original].ToString();
                }
            }
            if (this._osId == "SO")
            {
                /*
                 * 以下代码暂时找不到用意，所以先屏蔽，因为营销费用申请和要货单是同时转入的，所以要货单转入不写表头QT_NO
                 * CXY 2008-03-10
                 */

                // 检查表头客户是否改变
                //if (MF_POSTable.Rows[0].RowState == DataRowState.Modified)
                //{
                //    if (!String.IsNullOrEmpty(MF_POSTable.Rows[0]["CUS_NO"].ToString()) && !String.IsNullOrEmpty(MF_POSTable.Rows[0]["QT_NO"].ToString()) && !String.IsNullOrEmpty(MF_POSTable.Rows[0]["BIL_ID"].ToString()))
                //    {
                //        _isCusChange = this.chkCusChange(MF_POSTable.Rows[0]["CUS_NO"].ToString(), MF_POSTable.Rows[0]["BIL_ID"].ToString(), MF_POSTable.Rows[0]["QT_NO"].ToString());
                //    }
                //}
                //if (_isCusChange)
                //{
                //    if (MF_POSTable.Rows[0].RowState != DataRowState.Deleted)
                //    {
                //        if (MF_POSTable.Rows[0]["BIL_ID"].ToString() == "YH" || MF_POSTable.Rows[0]["BIL_ID"].ToString() == "YC" || MF_POSTable.Rows[0]["BIL_ID"].ToString() == "YC")
                //        {
                //            if (MF_POSTable.Rows[0]["BIL_ID"].ToString() == "YN" || MF_POSTable.Rows[0]["BIL_ID"].ToString() == "YC")
                //            {
                //                if (MF_POSTable.Rows[0]["QT_NO"].ToString() != MF_POSTable.Rows[0]["YH_NO"].ToString())
                //                    MF_POSTable.Rows[0]["QT_NO"] = MF_POSTable.Rows[0]["YH_NO"];
                //            }
                //            else
                //            {
                //                MF_POSTable.Rows[0]["QT_NO"] = DBNull.Value;
                //                MF_POSTable.Rows[0]["BIL_ID"] = DBNull.Value;
                //            }
                //            DataRow[] _drSel = TF_POSTable.Select();
                //            for (int i = 0; i < _drSel.Length; i++)
                //            {
                //                _drSel[i]["QT_NO"] = MF_POSTable.Rows[0]["QT_NO"];
                //                _drSel[i]["BIL_ID"] = MF_POSTable.Rows[0]["BIL_ID"];
                //                _drSel[i]["OTH_NO"] = DBNull.Value;
                //            }
                //        }
                //    }
                //}
            }
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            string _sqlPlus = "";
            _sqlPlus += ",CUS_NO_POS,INST_TEAM,AMTN_DS";
            string _sqlCaseTf = "";
            //if (CompInfo.CaseID == "55")
            //{
            //    _sqlCaseTf += ",ONLINESERVICE_XML";
            //}
            ht["MF_POS"] = "OS_ID,OS_NO,OS_DD,BAT_NO,CUS_NO,QT_NO,SAL_NO,USE_DEP,EST_DD,CUR_ID,TAX_ID,BIL_TYPE,"
                + "CUS_OS_NO,CNTT_NO,REM,PAY_MTH,PAY_DAYS,CHK_DAYS,PAY_REM,SEND_MTH,SEND_WH,ADR,DIS_CNT,"
                + "AMTN_NET,FX_WH,YH_NO,PO_DEP,PAY_DD,CHK_DD,INT_DAYS,CLS_REM,USR,CLS_ID,PRT_SW,BIL_ID,"
                + "BIL_NO,CLS_DATE,CHK_MAN,EXC_RTO,BYBOX,TOT_BOX,TOT_QTY,SYS_DATE,ISOVERSH,HS_ID,HIS_PRICE,"
                + "ISSVS,CHK_FX,HAS_FX,AMTN_YJBX,AMTN_BX,INV_CHK,INV_UNI_NO,INV_TITLE,INV_AMT,AMTN_CBAC,CBAC_CLS,"
                + "CARD_NO,SEND_AREA,BACK_ID,VOH_ID,VOH_NO,AMTN_INT,AMT_INT,RP_NO,TAX,FJ_NUM,INV_NO,PO_SO_NO,CONTRACT,MOB_ID,CFM_SW" + _sqlPlus;
            ht["TF_POS"] = "OS_ID,OS_NO,PRD_NO,PRD_NAME,ITM,QT_NO,WH,UNIT,QTY,UP,DIS_CNT,AMT,AMTN,TAX_RTO,TAX,"
                + "QTY_PS,EST_DD,CST_STD,QTY_PO,QTY_PRE,QTY_RK,QTY_RK_UNSH,QTY_IC,PRD_MARK,OS_DD,PRE_ITM,BOX_ITM,BIL_ID,"
                + "OTH_NO,OTH_ITM,REM,MTN_REM,EST_ITM,CUS_OS_NO,VALID_DD,BAT_NO,FREE_ID,MTN_TYPE,WC_NO,MTN_DD,RTN_DD,MTN_ALL_ID,CHK_RTN,PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,QTY1,UP_QTY1" + _sqlCaseTf;
            ht["TF_POS1"] = "OS_ID,OS_NO,ITM,PRD_NO,CONTENT,QTY,KEY_ITM,WH,BIL_ID,EST_ITM,EST_DD";
            ht["PAY_B2C"] = "BIL_ID,BIL_NO,PAY_REM,PAY_NO,PAY_ID,PAY_DD,REM";

            this.UpdateDataSet(changedDs, ht);
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
                        _pgm = "DRP" + this._osId;
                    }
                    DataTable _dtMf = changedDs.Tables["MF_POS"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["PO_DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changedDs.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDs.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDs.ExtendedProperties["PRN"] = _billRight["PRN"];
                        changedDs.ExtendedProperties["LCK"] = _billRight["LCK"];
                       
                    }
                }
                this.SetCanModify(changedDs, true);
                //取配码比描述
                if (changedDs.Tables.Contains("TF_POS1"))
                {
                    DataTable _TF_POS1Table = changedDs.Tables["TF_POS1"];
                    Sunlike.Business.BarBox _bar = new Sunlike.Business.BarBox();
                    DataColumn _dc = new DataColumn("CONTENT_DSC", Type.GetType("System.String"));
                    _dc.MaxLength = 64 * 1024;
                    if (!_TF_POS1Table.Columns.Contains("CONTENT_DSC"))
                        _TF_POS1Table.Columns.Add(_dc);
                    foreach (DataRow _TF_POS1Row in _TF_POS1Table.Rows)
                    {
                        if (_TF_POS1Row.RowState == System.Data.DataRowState.Deleted)
                            continue;
                        _TF_POS1Row["CONTENT_DSC"] = _bar.GetBar_BoxDsc(_TF_POS1Row["PRD_NO"].ToString(), _TF_POS1Row["CONTENT"].ToString());
                    }
                    _TF_POS1Table.AcceptChanges();
                }
                //DRPYH SunlikeDRPYH = new DRPYH();
                //if (_osId == "SO" && !String.IsNullOrEmpty(_yh_no))
                //{
                //    if (_DrpSOHasChange)//修改要货单的CLS_ID
                //    {
                //        SunlikeDRPYH.CheckForSO(_yh_no);
                //    }
                //    else
                //    {			     
                //        SunlikeDRPYH.UnCheckForSO(_yh_no);
                //    }
                //}	
                #endregion
            }
            if (changedDs.HasErrors)
            {
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDs);
                    throw new SunlikeException("RCID=DRPSO.UpdateData() Error:;" + _errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changedDs);
                }
            }
            return Sunlike.Business.BizObject.GetAllErrors(changedDs);
        }

        #endregion

        #region BeforeUpdate
        /// <summary>
        /// BeforeUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            string _prd_no = "";
            string _cust_no = "";
            string _depNo = "";
            string _sal_no = "";
            string _wh = "";
            string _dept = "";
            string _os_no = "";
            string _bil_type = "";
            DateTime _timeOs_dd;
            #region mf_pos;tf_pos;tf_pos1
            Auditing _auditing = new Auditing();

            #region 取得单据单号
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    if (tableName == "PAY_B2C")
                    {
                        _os_no = dr["BIL_NO", DataRowVersion.Original].ToString();

                        if (!String.IsNullOrEmpty(dr["PAY_NO", DataRowVersion.Original].ToString()))
                        {
                            throw new SunlikeException("RCID=INV.HINT.PAY_NO_NOTNULL");//此单存在银行付款单号，不能删除。(B2C)
                        }
                    }
                    else
                    {
                        _os_no = dr["OS_NO", DataRowVersion.Original].ToString();
                    }
                }
                else
                {
                    if (tableName == "PAY_B2C")
                    {
                        _os_no = dr["BIL_NO"].ToString();
                    }
                    else
                    {
                        _os_no = dr["OS_NO"].ToString();
                    }
                }
                if (_auditing.GetIfEnterAuditing(_osId, _os_no))//如果进去审核了就不能修改和新增删除
                {
                    //throw new SunlikeException("进入审核的单据不能修改");
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");
                }
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "OS_ID = '" + _osId + "' AND OS_NO = '" + _os_no + "'";
                if (_Users.IsLocked("MF_POS", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion

            #region 表头

            if (tableName == "MF_POS")
            {
                //新增时判断关账日期
                string _fieldNameCls = "CLS_INV";
                if (statementType != StatementType.Delete)
                {                    
                    if (string.Compare("T", dr["ISSVS"].ToString()) == 0)
                    {
                        _fieldNameCls = "CLS_MNU";
                    }
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["OS_DD"]), dr["PO_DEP"].ToString(), _fieldNameCls))
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
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["OS_DD", DataRowVersion.Original]), dr["PO_DEP", DataRowVersion.Original].ToString(), _fieldNameCls))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                if (statementType != StatementType.Delete)
                {
                    //如果是要货转进来的，检查要货单是否被删除。
                    if (!String.IsNullOrEmpty(dr["YH_NO"].ToString()))
                    {
                        string _yh_no = "", _yh_id = "", _cus_no = "";
                        _yh_no = dr["YH_NO"].ToString();
                        if (_yh_no.IndexOf("-") > 0)
                            _yh_no = _yh_no.Split('-')[0];
                        _yh_id = _yh_no.Substring(0, 2);
                        _cus_no = _yh.getCusNo(_yh_id, _yh_no);//根据要货单号取得用户代号，如果返回NULL代表要货单不存在。
                        if (String.IsNullOrEmpty(_cus_no))
                        {
                            dr.SetColumnError("YH_NO", "RCID=COMMON.HINT.YH_NO_NOTEXIST"); //要货单不存在
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //如果是营销费用申请转进来的，检查费用申请单是否被删除。
                    if (!String.IsNullOrEmpty(dr["QT_NO"].ToString()))
                    {
                        string _qt_no = dr["QT_NO"].ToString();
                        if (_qt_no.Substring(0, 2) == "EA")
                        {
                            DRPME _DRPME = new DRPME();
                            if (_qt_no.IndexOf("-") > 0)
                                _qt_no = _qt_no.Split('-')[0];
                            if (!_DRPME.IsExists(_qt_no))
                            {
                                dr.SetColumnError("QT_NO", "RCID=COMMON.HINT.EA_NOTEXIST"); //费用申请单不存在
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }
                    #region 如果是从要货来的就不用检测
                    if (!_isFromYH)//如果是从要货来的就不用检测
                    {

                        //客户代号(必添)
                        _cust_no = dr["CUS_NO"].ToString();
                        Cust SunlikeCust = new Cust();
                        if (SunlikeCust.IsExist(_loginUsr, _cust_no, Convert.ToDateTime(dr["OS_DD"])) == false)
                        {
                            dr.SetColumnError("CUS_NO",/*客户代号不存在或没有对其操作的权限,请检查*/"RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + _cust_no + "");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //部门(必添)
                        _depNo = dr["PO_DEP"].ToString();
                        Dept SunlikeDept = new Dept();
                        if (SunlikeDept.IsExist(_loginUsr, _depNo, Convert.ToDateTime(dr["OS_DD"])) == false)
                        {
                            dr.SetColumnError("PO_DEP",/*部门代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + _depNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //业务员
                        _sal_no = dr["SAL_NO"].ToString();
                        if (!String.IsNullOrEmpty(_sal_no))
                        {
                            Salm SunlikeSalm = new Salm();
                            if (SunlikeSalm.IsExist(_loginUsr, _sal_no, Convert.ToDateTime(dr["OS_DD"])) == false)
                            {
                                dr.SetColumnError("SAL_NO",/*业务员代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + _sal_no);
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        if (_osId == "SO")
                        {
                            //分销仓库
                            if (!string.IsNullOrEmpty(dr["FX_WH"].ToString()))
                            {
                                Cust _cust = new Cust();
                                string _fx_wh = dr["FX_WH"].ToString();
                                string _fxcust_no = dr["CUS_NO"].ToString();
                                bool _isCusWh = false;
                                bool _isDrpId = false;
                                WH SunlikeWh = new WH();
                                if (!String.IsNullOrEmpty(_fx_wh))
                                {
                                    if (SunlikeWh.IsExist(_loginUsr, _fx_wh, Convert.ToDateTime(dr["OS_DD"]), _fxcust_no) == false)
                                    {
                                        dr.SetColumnError("FX_WH",/*要货库位[{0}]不存在没有对其操作的权限,请检查*/"RCID=INV.HINT.YHNOERROR,PARAM=" + _fx_wh);
                                        status = UpdateStatus.SkipAllRemainingRows;
                                    }
                                }
                                //检测分销库位与客户是否一致
                                _isDrpId = _cust.IsDrp_id(_fxcust_no);
                                if (this._controlCusWh == "0" && _isDrpId)
                                {
                                    DataTable _myWhDt = _cust.GetCus_WH(_fxcust_no);
                                    if (_myWhDt != null && _myWhDt.Rows.Count > 0)
                                    {

                                        for (int i = 0; i < _myWhDt.Rows.Count; i++)
                                        {
                                            if (_myWhDt.Rows[i]["WH"].ToString() == _fx_wh)
                                                _isCusWh = true;
                                        }
                                    }
                                    if (!_isCusWh)
                                    {
                                        dr.SetColumnError("FX_WH", "RCID=INV.HINT.ISNOTCUSTWH,PARAM=" + dr["FX_WH"].ToString() + ",PARAM=" + _fxcust_no);//库位[{0}]不是客户[{1}]所属库位
                                        status = UpdateStatus.SkipAllRemainingRows;
                                    }
                                }
                            }

                            //会员卡号
                            if (!String.IsNullOrEmpty(dr["CARD_NO"].ToString()))
                            {
                                CardMember _card = new CardMember();
                                SunlikeDataSet _dsCard = _card.GetData("", dr["CARD_NO"].ToString(), false);
                                if (_dsCard.Tables["POSCARD"].Rows.Count == 0)
                                {
                                    dr.SetColumnError("CARD_NO", "RCID=COMMON.HINT.CARD_NO_NULL");//
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                                else
                                {
                                    if ((!String.IsNullOrEmpty(_dsCard.Tables["POSCARD"].Rows[0]["EN_DD"].ToString()) && Convert.ToDateTime(_dsCard.Tables["POSCARD"].Rows[0]["EN_DD"].ToString()) < System.DateTime.Today) || _dsCard.Tables["POSCARD"].Rows[0]["SYSED_ID"].ToString() == "T")
                                    {
                                        dr.SetColumnError("CARD_NO", "RCID=COMMON.HINT.CARD_NO_CLOSE");//
                                        status = UpdateStatus.SkipAllRemainingRows;
                                    }
                                }
                            }
                        }
                    }
                    #endregion


                    #region 必添字段
                    if (dr["CLS_ID"] is System.DBNull)
                    {
                        dr["CLS_ID"] = "F";
                    }
                    if (dr["HS_ID"] is System.DBNull)
                    {
                        dr["HS_ID"] = "F";
                    }
                    //ISOVERSH,HIS_PRICE
                    if (dr["ISOVERSH"] is System.DBNull)
                    {
                        dr["ISOVERSH"] = "F";
                    }
                    if (dr["HIS_PRICE"] is System.DBNull)
                    {
                        dr["HIS_PRICE"] = "F";
                    }
                    #endregion

                    if (statementType == StatementType.Insert)
                    {
                        #region --生成单号
                        SQNO SunlikeSqNo = new SQNO();
                        _dept = dr["PO_DEP"].ToString();//部门
                        if (dr["OS_DD"] is System.DBNull)
                        {
                            _timeOs_dd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                        }
                        else
                        {
                            _timeOs_dd = Convert.ToDateTime(dr["OS_DD"]);
                        }
                        if (dr["EST_DD"] is System.DBNull)
                        {
                            dr["EST_DD"] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                        }
                        if (dr["EXC_RTO"] is System.DBNull)
                        {
                            dr["EXC_RTO"] = "1";
                        }
                        _bil_type = dr["BIL_TYPE"].ToString();
                        if (dr.Table.DataSet.ExtendedProperties.ContainsKey("GET_SQNO") && dr.Table.DataSet.ExtendedProperties["GET_SQNO"].ToString() == "F")
                        {

                        }
                        else
                        {
                            _os_no = SunlikeSqNo.Set(_osId, _loginUsr, _dept, _timeOs_dd, _bil_type);
                            dr["OS_NO"] = _os_no;
                        }
                        dr["OS_DD"] = _timeOs_dd.ToString(Comp.SQLDateFormat);
                        dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        dr["PRT_SW"] = "N";
                        #endregion
                    }

                    if (string.IsNullOrEmpty(dr["CUR_ID"].ToString()))
                    {
                        dr["EXC_RTO"] = 1;
                    }

                    #region 判断是否走审核流程
                    //_dept = dr["PO_DEP"].ToString();//部门
                    //if (dr["OS_DD"] is System.DBNull)
                    //{
                    //    _timeOs_dd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    //}
                    //else
                    //{
                    //    _timeOs_dd = Convert.ToDateTime(dr["OS_DD"]);
                    //}
                    ////不走审核流程
                    //if (!_isRunAuditing && _saveId == "T")
                    //{
                    //    //如果有要货单审核过来的则不需要更改审核人
                    //    if ((statementType != StatementType.Insert) || (String.IsNullOrEmpty(dr["CHK_MAN"].ToString())))
                    //    {
                    //        dr["CHK_MAN"] = _loginUsr;
                    //        dr["CLS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    //    }
                    //}
                    //if ((_isRunAuditing) && (_saveId != "F"))
                    //{
                    //    //变更审核流程的情况
                    //    if (!String.IsNullOrEmpty(dr["CHK_MAN"].ToString()))
                    //    {
                    //        dr["CHK_MAN"] = System.DBNull.Value;
                    //        dr["CLS_DATE"] = System.DBNull.Value;
                    //        //回滚量
                    //        string _error = this.RollBack(_osId, _os_no, false);
                    //        if (_error.Length > 0)
                    //            throw new Exception(_error);
                    //    }
                    //    AudParamStruct _aps;
                    //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                    //    _aps.BIL_ID = this._osId;
                    //    _aps.BIL_NO = _os_no;
                    //    _aps.BIL_DD = _timeOs_dd;
                    //    _aps.USR = _loginUsr;
                    //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                    //    _aps.DEP = _dept;
                    //    _aps.SAL_NO = dr["SAL_NO"].ToString();

                    //    //新加的部分，对应审核模板
                    //    _aps.MOB_ID = "";
                    //    if (dr.Table.Columns.Contains("MOB_ID"))
                    //    {

                    //        dr["MOB_ID"] = _aps.MOB_ID = _auditing.GetSHMobID(_aps.BIL_ID, _aps.USR, _aps.BIL_TYPE, Convert.ToString(dr["MOB_ID"]));
                    //    }

                    //    _auditing.SetBillToAudtingFlow("DRP", _aps, null);
                    //}
                    #endregion
                }
                if (_saveId != "F")
                {
                    //AudParamStruct _aps;
                    //if (dr.RowState != DataRowState.Deleted)
                    //{
                    //    if (dr["OS_DD"] is System.DBNull)
                    //    {
                    //        _timeOs_dd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    //    }
                    //    else
                    //    {
                    //        _timeOs_dd = Convert.ToDateTime(dr["OS_DD"]);
                    //    }
                    //    _aps.BIL_DD = _timeOs_dd;
                    //    _aps.BIL_ID = dr["OS_ID"].ToString();
                    //    _aps.BIL_NO = dr["OS_NO"].ToString();
                    //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                    //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                    //    _aps.DEP = dr["PO_DEP"].ToString();
                    //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                    //    _aps.USR = dr["USR"].ToString();
                    //    //_aps.MOB_ID = "";
                    //}
                    //else
                    //{
                    //    _aps = new AudParamStruct(Convert.ToString(dr["OS_ID", DataRowVersion.Original]), Convert.ToString(dr["OS_NO", DataRowVersion.Original]));
                    //}
                    //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                    //if (!string.IsNullOrEmpty(_auditErr))
                    //{
                    //    throw new SunlikeException(_auditErr);
                    //}
                }

                if( statementType == StatementType.Delete) 
                {
                    //#region 删除单据同时删除审核信息
                    //_os_no = dr["OS_NO", DataRowVersion.Original].ToString();
                    //_auditing.DelBillWaitAuditing("DRP", _osId, _os_no);
                    //#endregion

                    #region 手动结案
                    if (!_isRunAuditing)
                    {
                        if (dr["CLS_ID", DataRowVersion.Original].ToString() == "T")
                        {
                            DoCloseBill(_osId, _os_no);
                        }
                    }
                    #endregion

                    #region 判断是否有结案作业
                    Sunlike.Business.DRPEN _en = new DRPEN();
                    if (_en.IsExists(_osId, _os_no))
                    {
                        dr.SetColumnError("OS_NO",/*已经结案,不能删除*/"RCID=INV.HINT.HASEN");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    #endregion

                    #region 判断是否有转制令单
                    MRPMO _mo = new MRPMO();
                    SunlikeDataSet _dsMo = _mo.GetData(" AND SO_NO='" + dr["OS_NO", DataRowVersion.Original].ToString() + "'");
                    if (_dsMo.Tables["MF_MO"].Rows.Count > 0)
                    {
                        throw new SunlikeException(/*已经配送不能删除*/"RCID=INV.HINT.UNDELETEMO");
                    }
                    #endregion

                    #region 判断是否有转客户储值金额

                    if (string.Compare("T", dr["CBAC_CLS", DataRowVersion.Original].ToString()) == 0)
                    {
                        throw new SunlikeException(/*客户储值金额已经使用，单据无法删除*/"RCID=MON.HINT.CBAC_CLS_USED");
                    }
                    #endregion
                }
                #region 当有订金时
                Bills _bills = new Bills();
                if (statementType == StatementType.Delete)
                {
                    if (!string.IsNullOrEmpty(dr["RP_NO", DataRowVersion.Original].ToString()))
                        _bills.DelRcvPay("1", dr["RP_NO", DataRowVersion.Original].ToString());
                }
                else
                {
                    decimal _amtn = 0;
                    if (!String.IsNullOrEmpty(dr["AMTN_INT"].ToString()))
                    {
                        _amtn = Convert.ToDecimal(dr["AMTN_INT"]);
                    }
                    if (_amtn != 0)
                    {
                        //订金含税否；税率取“营业人资料”里的本业税率，如果为零取5
                        _amtn = Convert.ToDecimal(dr["AMTN_INT"]);
                        if (dr["TAX_ID"].ToString() != "1" && dr["HS_ID"].ToString() == "T")
                        {
                            CompInfo _compInfo = Comp.GetCompInfo("");
                            decimal _psTax = _compInfo.SystemInfo.PS1_TAX;
                            //POI_WBA外位币小数位数；POI_AMT本位币小数位数
                            int _poiAmt = _compInfo.DecimalDigitsInfo.System.POI_AMT;
                            if (_psTax == 0)
                                _psTax = 5;
                            decimal _amtnNet = _amtn / (1 + _psTax / 100);
                            _amtnNet = Math.Round(_amtnNet, _poiAmt);
                            dr["AMTN_NET"] = _amtnNet;
                            dr["TAX"] = (_amtn - _amtnNet);
                        }
                        else
                        {
                            dr["AMTN_NET"] = dr["AMTN_INT"];
                            dr["TAX"] = System.DBNull.Value;
                        }
                        UpdateMon(dr);
                    }
                    else if (!String.IsNullOrEmpty(dr["RP_NO"].ToString()))
                    {
                        _bills.DelRcvPay("1", dr["RP_NO"].ToString());
                    }
                }
                #endregion
            }
            #endregion

            #region 表身
            if (tableName == "TF_POS")
            {
                if (statementType != StatementType.Delete)
                {
                    WH _drpWh = new WH();

                    #region 检测
                    #region --计算表身信息是否正确及库存检测
                    #region 如果是从要货来的就不用检测
                    if (!_isFromYH)//如果是从要货来的就不用检测
                    {
                        //产品检测(必添)
                        Prdt SunlikePrdt = new Prdt();
                        _prd_no = dr["PRD_NO"].ToString();
                        if (SunlikePrdt.IsExist(_loginUsr, _prd_no, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])) == false)
                        {
                            dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prd_no);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //仓库(必添)
                        _wh = dr["WH"].ToString();
                        WH SunlikeWH = new WH();

                        if (!SunlikeWH.IsExist(_loginUsr, dr["WH"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])))
                        {
                            dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }


                        if (SunlikeWH.IsExist(_loginUsr, _wh, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])) == false)
                        {
                            dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //PMARK
                        string _mark = dr["PRD_MARK"].ToString();
                        int _prdMod = SunlikePrdt.CheckPrdtMod(dr["PRD_NO"].ToString(), _mark);
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

                                //销项单据如果货品大类为A，B，C则允许特征为空
                                bool _markCanNull = false;
                                if (dr["OS_ID"].ToString() == "SO" || dr["OS_ID"].ToString() == "SR")
                                {
                                    DataTable _dtPrdt = SunlikePrdt.GetPrdt(dr["PRD_NO"].ToString());
                                    if (_dtPrdt.Rows.Count > 0)
                                    {
                                        if (_dtPrdt.Rows[0]["KND"].ToString().IndexOfAny(new char[] { 'A', 'B', 'C' }) != -1)
                                        {
                                            _markCanNull = true;
                                        }
                                    }
                                }

                                for (int i = 0; i < _markTable.Rows.Count; i++)
                                {
                                    if (_markCanNull && String.IsNullOrEmpty(_prd_markAry[i].Trim()))
                                    {
                                        continue;
                                    }

                                    string _markName = _markTable.Rows[i]["FLDNAME"].ToString();
                                    if (!_prd_Mark.IsExist(_markName, dr["PRD_NO"].ToString(), _prd_markAry[i]))
                                    {
                                        dr.SetColumnError(_markName,/*货品特征[{0}]不存在,请检查*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[i].Trim());
                                        status = UpdateStatus.SkipAllRemainingRows;
                                    }
                                }
                            }
                        }
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

                    string _batNo = "";
                    if (this._osId == "SO" && !String.IsNullOrEmpty(_yh_no))
                    {
                        #region 判断库存是否满足
                        string _lowWhQty = Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString(); //by db 04.6.1 21:10 in jinhou
                        string _chk_qty_way = Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString();
                        if (_lowWhQty == "F")
                        {
                            decimal _qty = 0;
                            decimal _originalQty = 0;//原来单据的数量
                            _batNo = dr["BAT_NO"].ToString();
                            Prdt _prdt = new Prdt();
                            bool _chkBat = false;//是否批号控制产品
                            DataTable _dtPrdt = _prdt.GetPrdt(dr["PRD_NO"].ToString());
                            if (_dtPrdt.Rows.Count > 0)
                            {
                                if (CaseInsensitiveComparer.Default.Compare(_dtPrdt.Rows[0]["CHK_BAT"].ToString(), "T") == 0)
                                {
                                    _chkBat = true;
                                }
                            }
                            if (_chk_qty_way == "1")
                            {
                                _qty = _drpWh.GetSumQty(true, dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString(), dr["WH"].ToString(), _isUnder, _batNo, _chkBat);

                            }
                            else
                            {
                                _qty = _drpWh.GetSumQty(false, dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString(), dr["WH"].ToString(), _isUnder, _batNo, _chkBat);
                            }
                            //判断是否是修改状态
                            if (statementType == StatementType.Update)
                            {
                                if ((dr["PRD_NO", DataRowVersion.Original].ToString() == dr["PRD_NO", DataRowVersion.Current].ToString()) && (dr["PRD_MARK", DataRowVersion.Original].ToString() == dr["PRD_MARK", DataRowVersion.Current].ToString()) && (dr["WH", DataRowVersion.Original].ToString() == dr["WH", DataRowVersion.Current].ToString()))
                                {

                                    //在修改的情况需要判断当前单据的原数量
                                    //如果有就减掉它
                                    if (dr["QTY", System.Data.DataRowVersion.Original] != System.DBNull.Value)
                                        _originalQty = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                                    _qty += _originalQty;
                                }
                            }
                            if (Convert.ToDecimal(dr["QTY"]) > _qty)
                            {
                                if (_chk_qty_way == "1")
                                {
                                    dr.SetColumnError("QTY", "RCID=INV.HINT.FACTPRDT1,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["WH"].ToString() + ",PARAM=" + dr["PRD_MARK"].ToString());
                                }
                                else
                                {
                                    dr.SetColumnError("QTY", "RCID=INV.HINT.UNFACTPRDT1,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["WH"].ToString() + ",PARAM=" + dr["PRD_MARK"].ToString());
                                }
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        #endregion
                    }
                    #region 检测成本

                    //if (this._osId == "SO")
                    //{
                    //    UserProperty _userProp = new UserProperty();
                    //    if (_userProp.GetData(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["USR"].ToString(), "DRPSO", "CONTROL_UP_DW_CST") == "T")
                    //    {
                    //        decimal _costWh = _drpWh.GetCost(Comp.CompNo, dr["WH"].ToString(), dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString(), DateTime.Now, dr["BAT_NO"].ToString());
                    //        decimal _qtyWh = _drpWh.GetQty(true, dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString(), dr["WH"].ToString(), dr["BAT_NO"].ToString());
                    //        if (_qtyWh != 0)
                    //        {
                    //            _costWh = _costWh / _qtyWh;
                    //        }
                    //        if (Convert.ToDecimal(dr["UP"]) < _costWh)
                    //        {
                    //            dr.SetColumnError("QTY", "");
                    //        }
                    //    }
                    //}

                    #endregion
                    #endregion

                    #region 保修卡检测
                    //维修卡
                    if (!string.IsNullOrEmpty(dr["WC_NO"].ToString()))
                    {
                        WC _wc = new WC();
                        SunlikeDataSet _dsWc = _wc.GetDataForMA(dr["WC_NO"].ToString());
                        if (_dsWc != null && _dsWc.Tables.Contains("MF_WC") && _dsWc.Tables["MF_WC"].Rows.Count > 0)
                        {
                            string _prdNoWc = "";
                            _prdNoWc = _dsWc.Tables["MF_WC"].Rows[0]["PRD_NO"].ToString();
                            //货品判断
                            if (string.Compare(_prdNoWc, dr["PRD_NO"].ToString()) != 0)
                            {
                                dr.SetColumnError("WC_NO",/*维修卡货品[{0}]与当前货品[{1}]不符*/"RCID=MTN.HINT.MTNPRDTERROR,PARAM=" + _prdNoWc + ",PARAM=" + dr["PRD_NO"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        else
                        {
                            dr.SetColumnError("WC_NO", "RCID=MTN.HINT.WC_NO_NOT_EXIST;");//保修卡号不存在!
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion
                    #endregion

                    #region 判断受订量是否小于配送量 ||  出入库累计量 || 已交量
                    if (_osId == "SO")
                    {
                        decimal _deciQty = 0;
                        decimal _deciQtyIc = 0;
                        decimal _deciQtyRk = 0;
                        decimal _deciQtyPs = 0;
                        decimal _deciQtyPre = 0;
                        if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                            _deciQty = Convert.ToDecimal(dr["QTY"]);
                        if (!String.IsNullOrEmpty(dr["QTY_IC"].ToString()))
                            _deciQtyIc = Convert.ToDecimal(dr["QTY_IC"]);
                        if (!String.IsNullOrEmpty(dr["QTY_RK"].ToString()))
                            _deciQtyRk += Convert.ToDecimal(dr["QTY_RK"]);
                        if (!String.IsNullOrEmpty(dr["QTY_RK_UNSH"].ToString()))
                            _deciQtyRk += Convert.ToDecimal(dr["QTY_RK_UNSH"]);
                        if (!String.IsNullOrEmpty(dr["QTY_PS"].ToString()))
                            _deciQtyPs = Convert.ToDecimal(dr["QTY_PS"]);
                        if (!String.IsNullOrEmpty(dr["QTY_PRE"].ToString()))
                            _deciQtyPre = Convert.ToDecimal(dr["QTY_PRE"]);
                        if (_deciQty > 0)
                        {
                            //判断受订量是否小于配送量
                            if (_deciQty < _deciQtyIc)
                            {
                                dr.SetColumnError("QTY", "RCID=INV.HINT.QTYERRORTOQTYIC");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            //判断受订量是否小于出入库累计量
                            if (_deciQty < _deciQtyRk)
                            {
                                dr.SetColumnError("QTY", "RCID=INV.HINT.QTYERRORTOQTYRK");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            //判断受订量是否小于销货量
                            if (_deciQty < _deciQtyPs)
                            {
                                dr.SetColumnError("QTY", "RCID=INV.HINT.QTYERRORTOQTYPS");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            //判断受订量是否小于已退量
                            if (_deciQty < _deciQtyPre)
                            {
                                dr.SetColumnError("QTY", "RCID=INV.HINT.QTYERRORTOQTYPRE");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }
                    #region 目前不控制

                    //					if (_osId == "SR")
                    //					{
                    //						decimal _rQty = 0;
                    //						decimal _rQtyOriginal = 0;
                    //						decimal _rQtySo = 0;
                    //						decimal _rQtyIc = 0;
                    //						decimal _rQtyRk = 0;
                    //						decimal _rQtyPs = 0;
                    //						decimal _rQtyPre = 0;						
                    //						int _preItm = -1;
                    //						if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                    //						{
                    //							_rQty = Convert.ToDecimal(dr["QTY"]);
                    //						}
                    //						if (!String.IsNullOrEmpty(dr["PRE_ITM"].ToString()))
                    //						{
                    //							_preItm = Convert.ToInt32(dr["PRE_ITM"]);
                    //						}
                    //						if (statementType == StatementType.Update)
                    //						{
                    //							if (!String.IsNullOrEmpty(dr["QTY",System.Data.DataRowVersion.Original].ToString()))
                    //							{
                    //								_rQtyOriginal = Convert.ToDecimal(dr["QTY",System.Data.DataRowVersion.Original]);
                    //							}
                    //						}
                    //						//原受订单号
                    //						string _qtNo ="";
                    //						_qtNo = dr["QT_NO"].ToString();
                    //						if (_qtNo.Length > 0 )
                    //						{
                    //							SunlikeDataSet _soDs = GetBody("SO",_qtNo,"PRE_ITM",_preItm);
                    //							if (_soDs.Tables["TF_POS"].Rows.Count > 0 )
                    //							{
                    //								if (_soDs.Tables["TF_POS"].!String.IsNullOrEmpty(Rows[0]["QTY"].ToString()))
                    //								{
                    //									_rQtySo = Convert.ToDecimal(_soDs.Tables["TF_POS"].Rows[0]["QTY"]);
                    //								}
                    //								if (_soDs.Tables["TF_POS"].!String.IsNullOrEmpty(Rows[0]["QTY_IC"].ToString()))
                    //								{
                    //									_rQtyIc = Convert.ToDecimal(_soDs.Tables["TF_POS"].Rows[0]["QTY_IC"]);
                    //								}
                    //								if (_soDs.Tables["TF_POS"].!String.IsNullOrEmpty(Rows[0]["QTY_RK"].ToString()))
                    //								{
                    //									_rQtyRk = Convert.ToDecimal(_soDs.Tables["TF_POS"].Rows[0]["QTY_RK"]);
                    //								}
                    //								if (_soDs.Tables["TF_POS"].!String.IsNullOrEmpty(Rows[0]["QTY_PS"].ToString()))
                    //								{
                    //									_rQtyPs = Convert.ToDecimal(_soDs.Tables["TF_POS"].Rows[0]["QTY_PS"]);
                    //								}
                    //								if (_soDs.Tables["TF_POS"].!String.IsNullOrEmpty(Rows[0]["QTY_PRE"].ToString()))
                    //								{
                    //									_rQtyPre = Convert.ToDecimal(_soDs.Tables["TF_POS"].Rows[0]["QTY_PRE"]);
                    //								}
                    //							}
                    //							if (_rQty  >  _rQtySo + _rQtyOriginal - _rQtyIc - _rQtyIc - _rQtyPs-_rQtyPre)//数量
                    //							{
                    //								dr.SetColumnError("QTY",/*数量超出范围*///"RCID=COMMON.HINT.SCOPEERROR");
                    //								status = UpdateStatus.SkipAllRemainingRows;						
                    //							}
                    //						}
                    //					}					
                    #endregion
                    #endregion

                    #region 必添项
                    if (_osId == "SO")
                    {
                        //EST_ITM为必添项
                        //拆分时EST_ITM不维护，会得到一样的值，转入销货回写已销量出错
                        //						if (dr["EST_ITM"] is System.DBNull)
                        {
                            dr["EST_ITM"] = dr["PRE_ITM"];
                        }
                    }
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows.Count > 0)
                    {
                        if (dr["OS_DD"] != dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])
                            dr["OS_DD"] = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"];

                        if (String.IsNullOrEmpty(dr["CUS_OS_NO"].ToString()))
                        {
                            dr["CUS_OS_NO"] = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CUS_OS_NO"];
                        }
                    }

                    #endregion

                    #region 维修卡记录

                    if (statementType == StatementType.Update)
                        UpdateWCH(dr, true, false);
                    UpdateWCH(dr, false, false);

                    #endregion
                }
                else //statementType == StatementType.Delete 删除表身记录
                {
                    #region 删除时检测 配送量 出库量 已交量 是否有值
                    if (_osId == "SO")
                    {
                        if (!(dr["QTY_IC", System.Data.DataRowVersion.Original] is System.DBNull))
                        {
                            decimal _qty_ic = Convert.ToDecimal(dr["QTY_IC", System.Data.DataRowVersion.Original]);
                            if (_qty_ic != 0)
                            {
                                throw new SunlikeException(/*已经配送不能删除*/"RCID=INV.HINT.UNDELETE");
                                //status = UpdateStatus.SkipAllRemainingRows;										
                            }
                        }
                        if (!(dr["QTY_RK", System.Data.DataRowVersion.Original] is System.DBNull))
                        {
                            decimal _qty_rk = Convert.ToDecimal(dr["QTY_RK", System.Data.DataRowVersion.Original]);
                            if (_qty_rk != 0)
                            {
                                throw new SunlikeException(/*已经出库不能删除*/"RCID=INV.HINT.UNDELETERK");
                            }
                        }
                        if (!(dr["QTY_RK_UNSH", System.Data.DataRowVersion.Original] is System.DBNull))
                        {
                            decimal _qty_rk_unsh = Convert.ToDecimal(dr["QTY_RK_UNSH", System.Data.DataRowVersion.Original]);
                            if (_qty_rk_unsh != 0)
                            {
                                throw new SunlikeException(/*已经出库不能删除*/"RCID=INV.HINT.UNDELETERK");
                            }
                        }
                        if (!(dr["QTY_PS", System.Data.DataRowVersion.Original] is System.DBNull))
                        {
                            decimal _qty_ps = Convert.ToDecimal(dr["QTY_PS", System.Data.DataRowVersion.Original]);
                            if (_qty_ps != 0)
                            {
                                throw new SunlikeException(/*已经销货不能删除*/"RCID=INV.HINT.UNDELETEPS1");
                            }
                        }
                        if (!(dr["QTY_PRE", System.Data.DataRowVersion.Original] is System.DBNull))
                        {
                            decimal _qty_pre = Convert.ToDecimal(dr["QTY_PRE", System.Data.DataRowVersion.Original]);
                            if (_qty_pre != 0)
                            {
                                throw new SunlikeException(/*已经受订退回不能删除*/"RCID=INV.HINT.UNDELETEPRE");
                            }
                        }
                    }
                    #endregion

                    #region 维修卡记录 删除
                    UpdateWCH(dr, true, false);
                    #endregion
                }
            }
            #endregion

            #region 表身（箱）
            if ((tableName == "TF_POS1"))
            {
                if (statementType != StatementType.Delete)
                {
                    #region 如果是从要货来的就不用检测
                    WH SunlikeWH = new WH();
                    if (!_isFromYH)//如果是从要货来的就不用检测
                    {
                        //产品检测(必添)
                        _prd_no = dr["PRD_NO"].ToString();
                        Prdt SunlikePrdt = new Prdt();
                        if (SunlikePrdt.IsExist(_loginUsr, _prd_no, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])) == false)
                        {
                            dr.SetColumnError("PRD_NO",/*品号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prd_no);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //仓库(必添)						
                        _wh = dr["WH"].ToString();
                        if (SunlikeWH.IsExist(_loginUsr, _wh, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])) == false)
                        {
                            dr.SetColumnError("WH",/*仓库代号[{0}]不存在没有对其操作的权限,请检查*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        if (SunlikeWH.IsBoxExist(_prd_no, _wh, dr["CONTENT"].ToString()) == false)
                        {
                            dr.SetColumnError("CONTENT",/*string.Format("产品[{0}]配码比[{1}]在仓库[{2}]不存在",_prd_no,_wh,_content)*/"RCID=INV.HINT.PRDTCONTENTERROR,PARAM=" + _prd_no + ",PARAM=" + _wh + ",PARAM=" + dr["CONTENT"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                    }

                    #endregion
                    if (this._osId == "SO" && !String.IsNullOrEmpty(_yh_no))
                    {
                        #region 判断库存是否满足
                        string _lowWhQty = Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString(); //by db 04.6.1 21:10 in jinhou
                        string _chk_qty_way = Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString();
                        if (_lowWhQty == "F")
                        {
                            decimal _boxQty = 0;
                            decimal _originalBoxQty = 0;//原来单据的箱数量
                            if (_chk_qty_way == "1")
                            {
                                _boxQty = SunlikeWH.GetBoxQty(true, dr["PRD_NO"].ToString(), dr["WH"].ToString(), dr["CONTENT"].ToString());
                            }
                            else
                            {
                                _boxQty = SunlikeWH.GetBoxQty(false, dr["PRD_NO"].ToString(), dr["WH"].ToString(), dr["CONTENT"].ToString());
                            }
                            //判断是否是修改状态
                            if (statementType == StatementType.Update)
                            {
                                if ((dr["PRD_NO", DataRowVersion.Original].ToString() == dr["PRD_NO", DataRowVersion.Current].ToString()) && (dr["CONTENT", DataRowVersion.Original].ToString() == dr["CONTENT", DataRowVersion.Current].ToString()) && (dr["WH", DataRowVersion.Original].ToString() == dr["WH", DataRowVersion.Current].ToString()))
                                {
                                    //在修改的情况需要判断当前单据的原数量
                                    //如果有就减掉它
                                    if (dr["QTY", System.Data.DataRowVersion.Original] != System.DBNull.Value)
                                        _originalBoxQty = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                                    _boxQty += _originalBoxQty;
                                }
                            }

                            if (Convert.ToDecimal(dr["QTY"]) > _boxQty)
                            {
                                if (_chk_qty_way == "1")
                                {
                                    dr.SetColumnError("QTY", "RCID=INV.HINT.FACTPRDT1,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["WH"].ToString() + ",PARAM=" + dr["CONTENT"].ToString().Replace(";", "*"));
                                }
                                else
                                {
                                    dr.SetColumnError("QTY", "RCID=INV.HINT.UNFACTPRDT1,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["WH"].ToString() + ",PARAM=" + dr["CONTENT"].ToString().Replace(";", "*"));
                                }
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        #endregion
                    }
                    //					string _os_no1 = "";
                    //					string _key_itm1 = "";
                    //					decimal qty1 = 0;
                    //					_os_no1 = dr["OS_NO"].ToString();
                    //					_key_itm1 = dr["KEY_ITM"].ToString();
                    //					if (this._osId == "SO")
                    //					{
                    //						if ((!(dr["QTY"] is System.DBNull)) && (!String.IsNullOrEmpty(dr["QTY"].ToString())))
                    //							qty1 = Convert.ToDecimal(dr["QTY"]);
                    //						try
                    //						{
                    //							if (statementType == StatementType.Update)
                    //								IsUpdateBoxQty(_os_no1,_key_itm1,qty1);
                    //						}
                    //						catch (Exception _ex)
                    //						{
                    //							dr.SetColumnError("QTY",_ex.Message.ToString());
                    //							status = UpdateStatus.SkipAllRemainingRows;
                    //						}
                    //					}
                }
            }
            #endregion


            #region 是否是更改受订单
            if (statementType == StatementType.Update)
            {
                _DrpSOHasChange = true;
            }
            if (statementType == StatementType.Insert)
            {
                if (dr.Table.DataSet.Tables.Contains("MF_POS"))
                {
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState != DataRowState.Added)
                    {
                        _DrpSOHasChange = true;
                    }
                }
                else
                {
                    _DrpSOHasChange = true;
                }
            }
            #endregion

            #endregion
            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }
        #endregion

        #region AfterUpdate
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
            string _dept = "";
            DateTime _timeOs_dd;
            string _os_no = "";
            Auditing _auditing = new Auditing();
            #region 表头
            if (tableName == "MF_POS")
            {

                if (statementType != StatementType.Delete)
                {
                    #region 读取数据
                    _os_no = dr["OS_NO"].ToString();
                    if (dr["OS_DD"] is System.DBNull)
                    {
                        _timeOs_dd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _timeOs_dd = Convert.ToDateTime(dr["OS_DD"]);
                    }
                    _dept = dr["PO_DEP"].ToString();//部门   
                    #endregion


                    #region 手动结案 (只受订)
                    if (_osId == "SO")
                    {
                        if (!_isRunAuditing)
                        {
                            if (statementType == StatementType.Update)
                            {
                                if (dr["CLS_ID"].ToString() != dr["CLS_ID", DataRowVersion.Original].ToString())
                                {
                                    DoCloseBill(_osId, _os_no);
                                }
                            }
                        }
                    }
                    #endregion

                }
                else //删除单据
                {
                    #region 读取数据
                    _os_no = dr["OS_NO", DataRowVersion.Original].ToString();
                    if (dr["OS_DD", DataRowVersion.Original] is System.DBNull)
                    {
                        _timeOs_dd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _timeOs_dd = Convert.ToDateTime(dr["OS_DD", DataRowVersion.Original]);
                    }
                    #endregion

                    #region 删除单据
                    _dept = dr["PO_DEP", DataRowVersion.Original].ToString();//部门
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(_os_no, _loginUsr);//删除时在BILD中插入一笔数据
                    #endregion
                }

                //更新客户帐户
                #region 更新客户帐户
                if (statementType == StatementType.Insert)
                {
                    this.UpdateMonCbac(dr, true);
                }
                else if (statementType == StatementType.Update)
                {
                    this.UpdateMonCbac(dr, false);
                    this.UpdateMonCbac(dr, true);
                }
                else if (statementType == StatementType.Delete)
                {
                    this.UpdateMonCbac(dr, false);
                }
                #endregion
            }
            #endregion

            #region 表身
            //新增 修改 删除 行
            if ((tableName == "TF_POS"))
            {
                //修改产品库存
                if (_saveId != "F")
                {
                    UpdateTf(_osId, dr, statementType, _isRunAuditing, false);
                    if (_osId == "SR" && !_isRunAuditing)
                    {
                        UpdateQtyPre(dr, statementType);
                    }
                }

                if (_osId == "SO" && _saveId != "F")
                {
                    if (!_isRunAuditing)
                    {
                        //回写收件单
                        if (dr.RowState == DataRowState.Added)
                        {
                            if (dr["BIL_ID"].ToString() == "RV")
                            {
                                UpdateRcv(dr, true);
                            }
                            else if (dr["BIL_ID"].ToString() == "MA")
                            {
                                UpdateMa(dr, true);
                            }
                        }
                        else if (dr.RowState == DataRowState.Modified)
                        {
                            if (dr["BIL_ID"].ToString() == "RV")
                            {
                                UpdateRcv(dr, false);
                            }
                            else if (dr["BIL_ID"].ToString() == "MA")
                            {
                                UpdateMa(dr, false);
                            }
                            if (dr["BIL_ID", DataRowVersion.Original].ToString() == "RV")
                            {
                                UpdateRcv(dr, true);
                            }
                            else if (dr["BIL_ID", DataRowVersion.Original].ToString() == "MA")
                            {
                                UpdateMa(dr, true);
                            }
                        }
                        else if (dr.RowState == DataRowState.Deleted)
                        {
                            if (dr["BIL_ID", DataRowVersion.Original].ToString() == "RV")
                            {
                                UpdateRcv(dr, false);
                            }
                            else if (dr["BIL_ID", DataRowVersion.Original].ToString() == "MA")
                            {
                                UpdateMa(dr, false);
                            }

                        }
                        //回写对应的要货单
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            if ((dr["BIL_ID"].ToString() == "YH"
                                || dr["BIL_ID"].ToString() == "YN"
                                || dr["BIL_ID"].ToString() == "YC")
                                && (dr["OTH_NO"].ToString() != String.Empty || dr["QT_NO"].ToString() != String.Empty)
                                && dr["QTY"] != System.DBNull.Value)
                            {
                                try
                                {
                                    if (dr.RowState == DataRowState.Added)
                                    {
                                        _yh.UpdateQtySo(dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(), Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]), 0, 1, _isRunAuditing);
                                    }
                                    if (dr.RowState == DataRowState.Modified)
                                    {
                                        _yh.UpdateQtySo(dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(), Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]), Convert.ToDecimal(dr["QTY", DataRowVersion.Original]), 2, _isRunAuditing);
                                    }
                                }
                                catch (Exception _ex)
                                {
                                    dr.RowError = _ex.Message;
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                        else
                        {
                            if ((dr["BIL_ID", DataRowVersion.Original].ToString() == "YH"
                                || dr["BIL_ID", DataRowVersion.Original].ToString() == "YN"
                                || dr["BIL_ID", DataRowVersion.Original].ToString() == "YC")
                                && (dr["OTH_NO", DataRowVersion.Original].ToString() != String.Empty || dr["QT_NO", DataRowVersion.Original].ToString() != String.Empty)
                                && dr["QTY", DataRowVersion.Original] != System.DBNull.Value)
                            {
                                try
                                {
                                    _yh.UpdateQtySo(dr["BIL_ID", DataRowVersion.Original].ToString(), dr["QT_NO", DataRowVersion.Original].ToString(), Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original]), Convert.ToDecimal(dr["QTY", DataRowVersion.Original]), 0, 3, _isRunAuditing);
                                }
                                catch (Exception _ex)
                                {
                                    dr.RowError = _ex.Message;
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                        //更新营销费用申请
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            if (dr["BIL_ID"].ToString() == "EA")
                            {
                                this.UpdateDrpMe(dr, false, false);
                            }
                        }
                        else
                        {
                            if (dr["BIL_ID", DataRowVersion.Original].ToString() == "EA")
                            {
                                this.UpdateDrpMe(dr, false, false);
                            }
                        }
                    }
                    else
                    {
                        string usr = "";
                        DataTable dt=dr.Table;
                        if (dt !=null && dt.DataSet !=null)
                        {
                            if (dt.DataSet.ExtendedProperties.Contains("UPD_USR"))
                            {
                                usr=dt.DataSet.ExtendedProperties["UPD_USR"].ToString();
                            }
                            else
                            {
                                usr="NO";
                            }
                        }

                        //更新未审核量
                        DRPYHut _drpYh = new DRPYHut();
                        if (dr.RowState == DataRowState.Deleted || dr.RowState == DataRowState.Modified)
                        {
                            if (dr["BIL_ID", DataRowVersion.Original].ToString() == "YH"
                                    || dr["BIL_ID", DataRowVersion.Original].ToString() == "YN"
                                    || dr["BIL_ID", DataRowVersion.Original].ToString() == "YC")
                            {
                                _drpYh.UpdateQtySoUnSh(usr,dr["BIL_ID", DataRowVersion.Original].ToString(), dr["QT_NO", DataRowVersion.Original].ToString(),
                                    Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original]), Convert.ToDecimal(dr["QTY", DataRowVersion.Original]) * -1);
                            }
                        }
                        if (dr.RowState != DataRowState.Deleted && dr.RowState != DataRowState.Unchanged)
                        {
                            if (dr["BIL_ID"].ToString() == "YH"
                                    || dr["BIL_ID"].ToString() == "YN"
                                    || dr["BIL_ID"].ToString() == "YC")
                            {
                                _drpYh.UpdateQtySoUnSh(usr,dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(),
                                    Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]));
                            }
                        }
                    }
                    #region 回写采购单
                    if (string.Compare("SO", _osId) == 0)
                    {
                        if (dr.RowState == DataRowState.Added)
                        {
                            if (dr["BIL_ID"].ToString() == "PO")
                            {
                                UpdateQtyPo(dr, true, false);
                            }
                        }
                        else if (dr.RowState == DataRowState.Modified)
                        {
                            if (dr["BIL_ID", DataRowVersion.Original].ToString() == "PO")
                            {
                                UpdateQtyPo(dr, false, false);
                            }
                            if (dr["BIL_ID"].ToString() == "PO")
                            {
                                UpdateQtyPo(dr, true, false);
                            }

                        }
                        else if (dr.RowState == DataRowState.Deleted)
                        {
                            if (dr["BIL_ID", DataRowVersion.Original].ToString() == "PO")
                            {
                                UpdateQtyPo(dr, false, false);
                            }
                        }
                    }
                    #endregion
                }


            }
            #endregion

            #region 表身（箱）
            if (tableName == "TF_POS1")
            {
                if (_saveId != "F")
                {
                    //修改相库存
                    UpdateTfBox(_osId, dr, statementType, _isRunAuditing, false);
                }
            }
            #endregion
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        #endregion

        #region BeforeDsSave
        /// <summary>
        /// 所有动作保存之前
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            DataTable _dtMf = ds.Tables["MF_POS"];
            //#region 单据追踪
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), this._osId);
            //}
            //#endregion
            if (_dtMf.Rows[0].RowState == DataRowState.Deleted)
                UpdateSarp(SunlikeDataSet.ConvertTo(ds), _isRunAuditing, true);
            else
                UpdateSarp(SunlikeDataSet.ConvertTo(ds), _isRunAuditing, false);
            base.BeforeDsSave(ds);
        }
        #endregion

        #endregion

        #region 结案
        /// <summary>
        /// 在单据上打上结案标记
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="close"></param>
        public void DoCloseSO(string osId, string osNo, bool close)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            _so.DoCloseSO(osId, osNo, close);
        }
        #endregion

        #region 修改(库存量，已配送量，已销量，月余额,结案回写受订量)
        #region 修改行
        //修改产品库存
        private void UpdateTf(string bilId, DataRow dr, StatementType statementType, bool IsRunAuditing, bool IsRollBack)
        {
            #region 变量申明
            string _prdt1prd_no = "";
            string _prdt1park_mark = "";
            string _prdt1wh = "";
            string _ut = "";
            string _prdt1batNo = "";
            string _prdt1validDd = "";
            decimal _prdt1qty_on_ord = 0;

            string _prd_noOriginal = "";
            string _park_markOriginal = "";
            string _whOriginal = "";
            string _batNoOriginal = "";
            string _validDdOriginal = "";
            string _utOriginal = "";
            decimal _qty_on_ordOriginal = 0;
            decimal _qtyFlag = 1;
            bool _isCloseBill = false;
            string _qtNo = "";			//原受订单号
            string _qtNoOriginal = "";  //原受订单号
            bool _updateCurent = true;
            bool _updateOriginal = true;


            StatementType _billStatementType = StatementType.Insert;
            #endregion
            //是否结案
            if (dr.Table.DataSet != null && dr.Table.DataSet.Tables.Contains("MF_POS") && dr.Table.DataSet.Tables["MF_POS"].Rows.Count > 0)
            {
                if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Deleted)
                {
                    _billStatementType = StatementType.Delete;
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CLS_ID", System.Data.DataRowVersion.Original].ToString() == "T")
                        _isCloseBill = true;
                }
                else
                {
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Added)
                    {
                        _billStatementType = StatementType.Insert;
                    }
                    else if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Modified)
                    {
                        _billStatementType = StatementType.Update;
                    }
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CLS_ID"].ToString() == "T")
                        _isCloseBill = true;
                }
            }
            #region 手动结案
            if (_isCloseBill && _billStatementType == StatementType.Insert)
                return;
            if (_isCloseBill && _billStatementType == StatementType.Delete)
                return;
            #endregion


            #region 取数据
            if (statementType == StatementType.Insert)
            {
                _prdt1prd_no = dr["PRD_NO"].ToString();
                _prdt1park_mark = dr["PRD_MARK"].ToString();
                _prdt1wh = dr["WH"].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                {
                    _prdt1validDd = Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat);
                }
                _prdt1batNo = dr["BAT_NO"].ToString();
                _ut = dr["UNIT"].ToString();
                if (!(dr["QTY"] is System.DBNull))
                {
                    _prdt1qty_on_ord = Convert.ToDecimal(dr["QTY"]);
                }

            }
            if (statementType == StatementType.Delete)
            {
                if (IsRollBack)//如果回滚中的删除则没有原先值
                {
                    _prd_noOriginal = dr["PRD_NO"].ToString();
                    _park_markOriginal = dr["PRD_MARK"].ToString();
                    _whOriginal = dr["WH"].ToString();
                    _batNoOriginal = dr["BAT_NO"].ToString();
                    _validDdOriginal = dr["VALID_DD"].ToString();
                    _utOriginal = dr["UNIT"].ToString();
                    if (!(dr["QTY"] is System.DBNull))
                    {
                        _qty_on_ordOriginal = Convert.ToDecimal(dr["QTY"]);
                    }
                }
                else
                {
                    _prd_noOriginal = dr["PRD_NO", DataRowVersion.Original].ToString();
                    _park_markOriginal = dr["PRD_MARK", DataRowVersion.Original].ToString();
                    _whOriginal = dr["WH", DataRowVersion.Original].ToString();
                    _batNoOriginal = dr["BAT_NO", DataRowVersion.Original].ToString();
                    _validDdOriginal = dr["VALID_DD", DataRowVersion.Original].ToString();
                    _utOriginal = dr["UNIT", DataRowVersion.Original].ToString();
                    if (!(dr["QTY", DataRowVersion.Original] is System.DBNull))
                    {
                        _qty_on_ordOriginal = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                    }

                }
            }
            if (statementType == StatementType.Update)
            {
                _prdt1prd_no = dr["PRD_NO", DataRowVersion.Current].ToString();
                _prd_noOriginal = dr["PRD_NO", DataRowVersion.Original].ToString();

                _prdt1park_mark = dr["PRD_MARK", DataRowVersion.Current].ToString();
                _park_markOriginal = dr["PRD_MARK", DataRowVersion.Original].ToString();

                _prdt1wh = dr["WH", DataRowVersion.Current].ToString();
                _whOriginal = dr["WH", DataRowVersion.Original].ToString();

                _prdt1batNo = dr["BAT_NO", DataRowVersion.Current].ToString();
                _batNoOriginal = dr["BAT_NO", DataRowVersion.Original].ToString();

                _prdt1validDd = dr["VALID_DD", DataRowVersion.Current].ToString();
                _validDdOriginal = dr["VALID_DD", DataRowVersion.Original].ToString();

                _ut = dr["UNIT", DataRowVersion.Current].ToString();
                _utOriginal = dr["UNIT", DataRowVersion.Original].ToString();

                if (!(dr["QTY", DataRowVersion.Current] is System.DBNull))
                {
                    _prdt1qty_on_ord = Convert.ToDecimal(dr["QTY", DataRowVersion.Current]);
                }

                if (!(dr["QTY", DataRowVersion.Original] is System.DBNull))
                {
                    _qty_on_ordOriginal = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
            }
            #endregion

            #region 修改库存
            if (!IsRunAuditing)//如果没有审核流程就直接修改库存
            {
                WH _wh = new WH();
                Prdt _prdt = new Prdt();
                //增加受订量
                if (!String.IsNullOrEmpty(_prdt1prd_no))
                {
                    #region 判断单据别
                    if (bilId == "SR")
                    {
                        _qtyFlag = -1;
                        //如果原先受订单已手工结案则不修改
                        DataTable _soTable = this.GetData(null, "SO", _qtNo, false, false).Tables["MF_POS"];
                        if (_soTable.Rows.Count > 0 && _soTable.Rows[0]["CLS_ID"].ToString() == "T" && _soTable.Rows[0]["BACK_ID"].ToString().Length == 0)
                        {
                            _updateCurent = false;
                        }
                    }
                    else if (bilId == "SO")
                        _qtyFlag = 1;
                    #endregion

                    #region 修改货品分仓存量
                    if (_updateCurent)
                    {
                        if (String.IsNullOrEmpty(_prdt1batNo))
                        {
                            _wh.UpdateQty(_prdt1prd_no, _prdt1park_mark, _prdt1wh, _ut.ToString(), WH.QtyTypes.QTY_ON_ODR, _qtyFlag * _prdt1qty_on_ord);
                        }
                        else
                        {

                            SunlikeDataSet _ds = _prdt.GetBatRecData(_prdt1batNo, _prdt1prd_no, _prdt1park_mark, _prdt1wh);
                            Hashtable _ht = new Hashtable();
                            _ht[WH.QtyTypes.QTY_ON_ODR] = _qtyFlag * _prdt1qty_on_ord;
                            if (!String.IsNullOrEmpty(_prdt1validDd))
                            {

                                if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                                {
                                    TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_prdt1validDd));
                                    if (_timeSpan.Days > 0)
                                    {
                                        _wh.UpdateQty(_prdt1batNo, _prdt1prd_no, _prdt1park_mark, _prdt1wh, "", _ut, _ht);
                                    }
                                    else
                                    {
                                        _wh.UpdateQty(_prdt1batNo, _prdt1prd_no, _prdt1park_mark, _prdt1wh, _prdt1validDd, _ut, _ht);
                                    }
                                }
                                else
                                {
                                    _wh.UpdateQty(_prdt1batNo, _prdt1prd_no, _prdt1park_mark, _prdt1wh, _prdt1validDd, _ut, _ht);
                                }
                            }
                            else
                            {
                                _wh.UpdateQty(_prdt1batNo, _prdt1prd_no, _prdt1park_mark, _prdt1wh, "", _ut, _ht);
                            }
                        }
                    }
                    #endregion
                }
                //减少受订量 
                if (!String.IsNullOrEmpty(_prd_noOriginal))
                {
                    #region 判断单据别
                    if (bilId == "SR")
                    {
                        _qtyFlag = -1;
                        //如果原先受订单已手工结案则不修改
                        DataTable _soTable = this.GetData(null, "SO", _qtNoOriginal, false, false).Tables["MF_POS"];
                        if (_soTable.Rows.Count > 0 && _soTable.Rows[0]["CLS_ID"].ToString() == "T" && _soTable.Rows[0]["BACK_ID"].ToString().Length == 0)
                        {
                            _updateOriginal = false;
                        }
                    }
                    else if (bilId == "SO")
                        _qtyFlag = 1;
                    #endregion

                    #region 修改货品分仓存量
                    if (_updateOriginal)
                    {
                        if (String.IsNullOrEmpty(_batNoOriginal))
                        {
                            _wh.UpdateQty(_prd_noOriginal, _park_markOriginal, _whOriginal, _utOriginal.ToString(), WH.QtyTypes.QTY_ON_ODR, (-1) * _qtyFlag * _qty_on_ordOriginal);
                        }
                        else
                        {
                            SunlikeDataSet _ds = _prdt.GetBatRecData(_batNoOriginal, _prd_noOriginal, _park_markOriginal, _whOriginal);
                            Hashtable _ht = new Hashtable();
                            _ht[WH.QtyTypes.QTY_ON_ODR] = (-1) * _qtyFlag * _qty_on_ordOriginal;
                            if (!String.IsNullOrEmpty(_validDdOriginal))
                            {
                                if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                                {
                                    TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDdOriginal));
                                    if (_timeSpan.Days > 0)
                                    {
                                        _wh.UpdateQty(_batNoOriginal, _prd_noOriginal, _park_markOriginal, _whOriginal, "", _utOriginal, _ht);
                                    }
                                    else
                                    {
                                        _wh.UpdateQty(_batNoOriginal, _prd_noOriginal, _park_markOriginal, _whOriginal, _validDdOriginal, _utOriginal, _ht);
                                    }
                                }
                                else
                                {
                                    _wh.UpdateQty(_batNoOriginal, _prd_noOriginal, _park_markOriginal, _whOriginal, _validDdOriginal, _utOriginal, _ht);
                                }
                            }
                            else
                            {
                                _wh.UpdateQty(_batNoOriginal, _prd_noOriginal, _park_markOriginal, _whOriginal, "", _utOriginal, _ht);
                            }
                        }
                    }
                    #endregion

                }

            }
            #endregion
        }
        /// <summary>
        /// 修改箱库存(不考虑箱的受订退回)
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="dr"></param>
        /// <param name="statementType"></param>
        /// <param name="IsRunAuditing"></param>
        /// <param name="IsRollBack"></param>

        private void UpdateTfBox(string bilId, DataRow dr, StatementType statementType, bool IsRunAuditing, bool IsRollBack)
        {
            string _prdt1BoxPrd_no = "";
            string _prdt1BoxWh = "";
            string _prdt1BoxContent = "";
            decimal _prdt1Boxqty_on_ord = 0;
            bool _isCloseBill = false;
            StatementType _billStatementType = StatementType.Insert;
            if (bilId == "SR")
                return;
            #region 是否结案
            if (dr.Table.DataSet != null && dr.Table.DataSet.Tables.Contains("MF_POS") && dr.Table.DataSet.Tables["MF_POS"].Rows.Count > 0)
            {
                if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Deleted)
                {
                    _billStatementType = StatementType.Delete;
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CLS_ID", System.Data.DataRowVersion.Original].ToString() == "T")
                        _isCloseBill = true;
                }
                else
                {
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Added)
                    {
                        _billStatementType = StatementType.Insert;
                    }
                    else if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Modified)
                    {
                        _billStatementType = StatementType.Update;
                    }
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CLS_ID"].ToString() == "T")
                        _isCloseBill = true;
                }
            }
            #endregion

            #region 手动结案
            if (_isCloseBill && _billStatementType == StatementType.Insert)
                return;
            #endregion

            WH SunlikeWH = new WH();
            if (statementType == StatementType.Insert)
            {
                _prdt1BoxPrd_no = dr["PRD_NO"].ToString();
                _prdt1BoxWh = dr["WH"].ToString();
                _prdt1BoxContent = dr["CONTENT"].ToString();
                if (!(dr["QTY"] is System.DBNull))
                {
                    _prdt1Boxqty_on_ord = Convert.ToDecimal(dr["QTY"]);
                }
                if (!IsRunAuditing)//如果没有审核流程就直接修改库存
                {
                    SunlikeWH.UpdateBoxQty(_prdt1BoxPrd_no, _prdt1BoxWh, _prdt1BoxContent, WH.BoxQtyTypes.QTY_ON_ODR, _prdt1Boxqty_on_ord);
                }
            }
            if (statementType == StatementType.Delete)
            {
                if (IsRollBack)//如果回滚中的删除则没有原先值
                {
                    _prdt1BoxPrd_no = dr["PRD_NO"].ToString();
                    _prdt1BoxWh = dr["WH"].ToString();
                    _prdt1BoxContent = dr["CONTENT"].ToString();
                    if (!(dr["QTY"] is System.DBNull))
                    {
                        _prdt1Boxqty_on_ord = Convert.ToDecimal(dr["QTY"]);
                    }

                }
                else
                {
                    _prdt1BoxPrd_no = dr["PRD_NO", DataRowVersion.Original].ToString();
                    _prdt1BoxWh = dr["WH", DataRowVersion.Original].ToString();
                    _prdt1BoxContent = dr["CONTENT", DataRowVersion.Original].ToString();
                    if (!(dr["QTY", DataRowVersion.Original] is System.DBNull))
                    {
                        _prdt1Boxqty_on_ord = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                    }
                }
                if (!IsRunAuditing)//如果没有审核流程就直接修改库存
                {
                    SunlikeWH.UpdateBoxQty(_prdt1BoxPrd_no, _prdt1BoxWh, _prdt1BoxContent, WH.BoxQtyTypes.QTY_ON_ODR, -_prdt1Boxqty_on_ord);
                }
            }
            if (statementType == StatementType.Update)
            {
                string _prdt1BoxPrd_noCurrent = dr["PRD_NO", DataRowVersion.Current].ToString(); ;
                string _prdt1BoxPrd_noOriginal = dr["PRD_NO", DataRowVersion.Original].ToString();

                string _prdt1BoxWhCurrent = dr["WH", DataRowVersion.Current].ToString(); ;
                string _prdt1BoxWhOriginal = dr["WH", DataRowVersion.Original].ToString();

                string _prdt1BoxContentCurrent = dr["CONTENT", DataRowVersion.Current].ToString(); ;

                decimal _prdt1Boxqty_on_ordCurrent = 0;
                decimal _prdt1Boxqty_on_ordOriginal = 0;

                if (!(dr["QTY", DataRowVersion.Current] is System.DBNull))
                {
                    _prdt1Boxqty_on_ordCurrent = Convert.ToDecimal(dr["QTY", DataRowVersion.Current]);
                }

                if (!(dr["QTY", DataRowVersion.Original] is System.DBNull))
                {
                    _prdt1Boxqty_on_ordOriginal = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }

                if (!IsRunAuditing)//如果没有审核流程就直接修改库存
                {
                    SunlikeWH.UpdateBoxQty(_prdt1BoxPrd_noOriginal, _prdt1BoxWhOriginal, _prdt1BoxContentCurrent, WH.BoxQtyTypes.QTY_ON_ODR, -_prdt1Boxqty_on_ordOriginal);
                    SunlikeWH.UpdateBoxQty(_prdt1BoxPrd_noCurrent, _prdt1BoxWhCurrent, _prdt1BoxContentCurrent, WH.BoxQtyTypes.QTY_ON_ODR, _prdt1Boxqty_on_ordCurrent);
                }
            }

        }
        #endregion

        #region 修改配送数量

        #region 箱
        /// <summary>
        /// UpdateBoxQty
        /// </summary>
        /// <param name="os_no"></param>
        /// <param name="key_itm"></param>
        /// <param name="qtyIc"></param>
        /// <returns></returns>
        public bool UpdateBoxQty(string os_no, string key_itm, decimal qtyIc)
        {
            DbDrpSO _dbDrpSO = new DbDrpSO(Comp.Conn_DB);
            string _resultCode = _dbDrpSO.UpdateBoxQty("SO", os_no, key_itm, qtyIc, "IC");
            WH _sunlikeWh = new WH();
            #region 修改箱库存的已受订量 **单件库存已在
            DataTable _tf_pos1Table = _dbDrpSO.SelectTf_pos1("SO", os_no, key_itm);
            for (int i = 0; i < _tf_pos1Table.Rows.Count; i++)
            {
                string _prd_no = _tf_pos1Table.Rows[i]["PRD_NO"].ToString();
                string _wh = _tf_pos1Table.Rows[i]["WH"].ToString();
                string _content = _tf_pos1Table.Rows[i]["CONTENT"].ToString();
                string _backId = "";
                string _clsId = "";
                decimal _qty = 0;
                _backId = _tf_pos1Table.Rows[i]["BACK_ID"].ToString();
                _clsId = _tf_pos1Table.Rows[i]["CLS_ID"].ToString();
                //手工结案
                if (_clsId == "T" && _backId.Length == 0)
                    continue;
                if (!String.IsNullOrEmpty(_tf_pos1Table.Rows[i]["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(_tf_pos1Table.Rows[i]["QTY"]);
                }
                if (System.Math.Abs(qtyIc) > _qty)
                {
                    if (qtyIc < 0)
                        qtyIc = (-1) * _qty;
                    else
                        qtyIc = _qty;
                }

                //减掉箱库存的受订量
                _sunlikeWh.UpdateBoxQty(_prd_no, _wh, _content, WH.BoxQtyTypes.QTY_ON_ODR, -qtyIc);
            }
            #endregion

            if (_resultCode == "0")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 件（配送数量,修改库存,受订量,信用额度）
        /// <summary>
        ///	 修改配送数量并且修改库存,受订量,信用额度
        /// </summary>
        /// <param name="os_no">受订单号组</param>
        /// <param name="pre_itm">单据对应序号组</param>
        /// <param name="prdNo"></param>
        /// <param name="unit"></param>
        /// <param name="qty_ic">配送数量数量组</param>
        public void UpdateQtyIc(ArrayList os_no, ArrayList pre_itm, ArrayList prdNo, ArrayList unit, ArrayList qty_ic)
        {

            if (os_no.Count != pre_itm.Count || os_no.Count != qty_ic.Count || pre_itm.Count != qty_ic.Count)
            {
                throw new Sunlike.Common.Utility.SunlikeException("RCID=INV.HINT.IOERROR");//修改配送量时,传入值不正确
            }
            //创建新表记录传入信息
            #region 创建新表记录传入信息
            DataTable _fromIO = new DataTable();
            _fromIO.Columns.Add("OS_ID", typeof(System.String));
            _fromIO.Columns.Add("OS_NO", typeof(System.String));
            _fromIO.Columns.Add("PRE_ITM", typeof(System.Int32));
            _fromIO.Columns.Add("PRD_NO", typeof(System.String));
            _fromIO.Columns.Add("UNIT", typeof(System.Int32));
            _fromIO.Columns.Add("QTY_IC", typeof(System.Decimal));
            for (int i = 0; i < os_no.Count; i++)
            {
                DataRow[] _selIo = _fromIO.Select("OS_ID='SO' AND OS_NO='" + os_no[i] + "' AND PRE_ITM='" + pre_itm[i] + "'");
                if (_selIo == null || _selIo.Length == 0)
                {
                    DataRow _newDr = _fromIO.NewRow();
                    _newDr["OS_ID"] = "SO";
                    _newDr["OS_NO"] = os_no[i];
                    _newDr["PRE_ITM"] = Convert.ToInt32(pre_itm[i]);
                    _newDr["PRD_NO"] = prdNo[i];
                    _newDr["UNIT"] = Convert.ToInt32(unit[i]);
                    _newDr["QTY_IC"] = Convert.ToDecimal(qty_ic[i]);
                    _fromIO.Rows.Add(_newDr);
                }
                else
                {
                    _selIo[0]["QTY_IC"] = Convert.ToDecimal(_selIo[0]["QTY_IC"]) + Convert.ToDecimal(qty_ic[i]);
                }
            }
            _fromIO.AcceptChanges();
            #endregion

            #region 读取数据
            ArrayList _os_ids = new ArrayList();
            for (int i = 0; i < os_no.Count; i++)
            {
                _os_ids.Add("SO");
            }
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            SunlikeDataSet _osTable = _so.GetData(_os_ids, os_no, pre_itm, "PRE_ITM");
            DataTable _mf_posTable = _osTable.Tables["MF_POS"];
            DataTable _tf_posTable = _osTable.Tables["TF_POS"];
            //			if (_tf_posTable.Rows.Count < pre_itm.Count)
            //				throw new Sunlike.Common.Utility.SunlikeException("DRPSO.PREITMCOUNTERROR");//修改配送量时,查询不到相同数量受订单
            #endregion

            //修改配送量
            #region 修改配送量
            string _os_id = "";
            string _os_no = "";
            string _prdNo = "";
            int _iUnit = 1;
            decimal _qty_ic = 0;
            decimal _qty = 0;
            for (int i = 0; i < _fromIO.Rows.Count; i++)
            {

                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["OS_NO"].ToString()))
                    _os_no = _fromIO.Rows[i]["OS_NO"].ToString();
                int _pre_itm = 0;
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["PRE_ITM"].ToString()))
                    _pre_itm = Convert.ToInt32(_fromIO.Rows[i]["PRE_ITM"]);
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["PRD_NO"].ToString()))
                    _prdNo = _fromIO.Rows[i]["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["UNIT"].ToString()))
                    _iUnit = Convert.ToInt32(_fromIO.Rows[i]["UNIT"]);
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["QTY_IC"].ToString()))
                    _qty_ic = Convert.ToDecimal(_fromIO.Rows[i]["QTY_IC"]);
                Hashtable _ht = new Hashtable();
                _ht["TableName"] = "TF_POS";
                _ht["IdName"] = "OS_ID";
                _ht["NoName"] = "OS_NO";
                _ht["ItmName"] = "PRE_ITM";
                _ht["OsID"] = "SO";
                _ht["OsNO"] = _os_no;
                _ht["KeyItm"] = _pre_itm;
                _qty_ic = INVCommon.GetRtnQty(_prdNo, _qty_ic, _iUnit, _ht);
                _so.UpdateClsId("IO", _os_no, "PRE_ITM", _pre_itm, "QTY_IC", _qty_ic);//修改配送数量
            }
            #endregion
            //修改货品分仓存量的已受订量
            WH _wh = new WH();
            #region 修改货品分仓存量的已受订量
            for (int i = 0; i < _tf_posTable.Rows.Count; i++)
            {
                string _prd_no = "";
                string _prd_mark = "";
                string _swh = "";
                string _unit = "";
                string _batNo = "";
                string _validDd = "";
                string _backId = "";
                string _clsId = "";
                _qty_ic = 0;
                _qty = 0;
                _prd_no = _tf_posTable.Rows[i]["PRD_NO"].ToString();
                _prd_mark = _tf_posTable.Rows[i]["PRD_MARK"].ToString();
                _swh = _tf_posTable.Rows[i]["WH"].ToString();
                _unit = _tf_posTable.Rows[i]["UNIT"].ToString();
                _batNo = _tf_posTable.Rows[i]["BAT_NO"].ToString();
                if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["VALID_DD"].ToString()))
                {
                    _validDd = Convert.ToDateTime(_tf_posTable.Rows[i]["VALID_DD"]).ToString(Comp.SQLDateFormat);
                }
                _backId = _tf_posTable.Rows[i]["BACK_ID"].ToString();
                _clsId = _tf_posTable.Rows[i]["CLS_ID"].ToString();
                //手工结案
                if (_clsId == "T" && _backId.Length == 0)
                    continue;
                if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY"].ToString()))
                    _qty = Convert.ToDecimal(_tf_posTable.Rows[i]["QTY"]);
                DataRow[] _selFormIO = _fromIO.Select("OS_ID='" + _tf_posTable.Rows[i]["OS_ID"].ToString() + "' AND OS_NO='" + _tf_posTable.Rows[i]["OS_NO"].ToString() + "' AND PRE_ITM='" + _tf_posTable.Rows[i]["PRE_ITM"].ToString() + "' ");

                if ((_selFormIO.Length > 0) && (!String.IsNullOrEmpty(_selFormIO[0]["QTY_IC"].ToString())))
                {
                    _qty_ic = Convert.ToDecimal(_selFormIO[0]["QTY_IC"]);
                }
                if (_qty_ic > 0)
                {
                    //扣除已配送量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_IC"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除已出库量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_RK"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_RK_UNSH"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK_UNSH"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK_UNSH"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除已销量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PS"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除已受订退回量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PRE"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                }
                else
                {
                    //扣除已出库量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_RK"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_RK_UNSH"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK_UNSH"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK_UNSH"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除已销量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PS"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除已受订退回量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PRE"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除已配送量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_IC"].ToString()))
                    {
                        _qty = System.Math.Abs(_qty_ic) - Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]) + _qty;//配送量-已配送量+受订量
                        //**************************************************
                        //当配送单表身两笔都是超交的情况，(受订量)qty就为负的情况时
                        //则受订扣减在最后一个超交的配送单表身扣减
                        //**************************************************
                        if (_qty < 0)
                        {
                            return;
                        }
                    }
                }
                if (System.Math.Abs(_qty_ic) > _qty)
                {
                    if (_qty_ic < 0)
                        _qty_ic = (-1) * _qty;
                    else
                        _qty_ic = _qty;
                }
                //修改货品分仓存量的受订量
                if (_batNo.Length == 0)
                {
                    _wh.UpdateQty(_prd_no, _prd_mark, _swh, _unit, WH.QtyTypes.QTY_ON_ODR, _qty_ic * (-1));//修改受订量
                }
                else
                {
                    System.Collections.Hashtable _ht = new Hashtable();
                    _ht[WH.QtyTypes.QTY_ON_ODR] = _qty_ic * (-1);
                    _wh.UpdateQty(_batNo, _prd_no, _prd_mark, _swh, _validDd, _unit, _ht);
                }
                //结案受订单
                UpdateClsId(_tf_posTable.Rows[i]["OS_NO"].ToString());
            }  //end for 
            #endregion
            Cust _cust = new Cust();
            //计算信用额度	
            #region 计算信用额度
            for (int i = 0; i < _mf_posTable.Rows.Count; i++)
            {
                string _cusNo = _mf_posTable.Rows[i]["CUS_NO"].ToString();

                if (_cust.IsSo_Crd(_cusNo))
                {
                    string _arpId = "1";
                    string _idxNo = "";
                    string _fieldName = "AMTN_SO";
                    string _backId = "";
                    string _clsId = "";
                    decimal _amtn = 0;
                    decimal _excRto = 0;
                    int _year = 0;
                    _year = Convert.ToDateTime(_mf_posTable.Rows[i]["OS_DD"]).Year;
                    int _month = 0;
                    _month = Convert.ToDateTime(_mf_posTable.Rows[i]["OS_DD"]).Month;
                    _os_id = "";
                    _os_no = "";
                    //取汇率
                    if (!String.IsNullOrEmpty(_mf_posTable.Rows[0]["EXC_RTO"].ToString()))
                        _excRto = Convert.ToDecimal(_mf_posTable.Rows[0]["EXC_RTO"]);
                    _os_id = _mf_posTable.Rows[i]["OS_ID"].ToString();
                    _os_no = _mf_posTable.Rows[i]["OS_NO"].ToString();
                    //取手工标记
                    _backId = _mf_posTable.Rows[i]["BACK_ID"].ToString();
                    //取结案标记
                    _clsId = _mf_posTable.Rows[i]["CLS_ID"].ToString();
                    //手工结案
                    if (_clsId == "T" && _backId.Length == 0)
                        continue;
                    DataRow[] _selBody = _tf_posTable.Select("OS_ID='" + _os_id + "' AND OS_NO='" + _os_no + "'");
                    for (int j = 0; j < _selBody.Length; j++)
                    {
                        decimal _up = 0;
                        _qty = 0;
                        _qty_ic = 0;
                        if (!String.IsNullOrEmpty(_selBody[j]["UP"].ToString()))
                        {
                            _up = Convert.ToDecimal(_selBody[j]["UP"]);
                        }
                        if (!String.IsNullOrEmpty(_selBody[j]["QTY"].ToString()))
                        {
                            _qty = Convert.ToDecimal(_selBody[j]["QTY"]);
                        }

                        DataRow[] _selQtyIc = _fromIO.Select("OS_ID='" + _selBody[j]["OS_ID"].ToString() + "' AND OS_NO='" + _selBody[j]["OS_NO"].ToString() + "' AND PRE_ITM='" + _selBody[j]["PRE_ITM"].ToString() + "' ");
                        if ((_selQtyIc.Length > 0) && (!String.IsNullOrEmpty(_selQtyIc[0]["QTY_IC"].ToString())))
                        {
                            _qty_ic = Convert.ToDecimal(_selQtyIc[0]["QTY_IC"]);
                        }
                        if (_qty_ic > 0)
                        {
                            //扣除已配送量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_IC"].ToString()))
                            {
                                if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]))
                                {
                                    _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]);
                                }
                                else
                                {
                                    _qty = 0;
                                }
                            }
                            //扣除已销量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PS"].ToString()))
                            {
                                if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]))
                                {
                                    _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]);
                                }
                                else
                                {
                                    _qty = 0;
                                }
                            }
                            //扣除受订退回量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PRE"].ToString()))
                            {
                                if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]))
                                {
                                    _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]);
                                }
                                else
                                {
                                    _qty = 0;
                                }
                            }
                        }
                        else
                        {
                            //扣除已销量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PS"].ToString()))
                            {
                                if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]))
                                {
                                    _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]);
                                }
                                else
                                {
                                    _qty = 0;
                                }
                            }
                            //扣除受订退回
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PRE"].ToString()))
                            {
                                if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]))
                                {
                                    _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]);
                                }
                                else
                                {
                                    _qty = 0;
                                }
                            }
                            //扣除已配送量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_IC"].ToString()))
                            {
                                _qty = System.Math.Abs(_qty_ic) - Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]) + _qty;//配送量-已配送量+受订量
                                //**************************************************
                                //当配送单表身两笔都是超交的情况，(受订量)qty就为负的情况时
                                //则受订扣减在最后一个超交的配送单表身扣减
                                //**************************************************
                                if (_qty < 0)
                                {
                                    return;
                                }
                            }
                        }
                        if (_qty >= System.Math.Abs(_qty_ic))
                        {
                            _qty = _qty_ic;
                        }
                        else if (_qty_ic < 0)
                        {
                            _qty *= -1;
                        }
                        _amtn += _qty * _up * _excRto;
                    }//end for j 查询每一张受订单需要修改的金额
                    Arp _arp = new Arp();
                    _arp.UpdateSarp(_arpId, _year, _cusNo, _month, _idxNo, _fieldName, (-1) * _amtn);
                }// end select IsSo_Crd			
            }//end for i
            #endregion
        }


        #endregion
        #endregion

        #region 修改已销数量
        /// <summary>
        /// 修改已销数量
        /// </summary>
        /// <param name="os_no"></param>
        /// <param name="est_itm"></param>
        /// <param name="prdNo"></param>
        /// <param name="unit"></param>
        /// <param name="qty_ps"></param>
        public void UpdateQtyPs(ArrayList os_no, ArrayList est_itm, ArrayList prdNo, ArrayList unit, ArrayList qty_ps, ArrayList qty1_ps)
        {
            if (os_no.Count != est_itm.Count || os_no.Count != qty_ps.Count || est_itm.Count != qty_ps.Count)
            {
                throw new Sunlike.Common.Utility.SunlikeException("RCID=INV.HINT.IOERROR");//修改配送量时,传入值不正确
            }
            //创建新表记录传入信息
            #region 创建新表记录传入信息
            DataTable _fromIO = new DataTable();
            _fromIO.Columns.Add("OS_ID", typeof(System.String));
            _fromIO.Columns.Add("OS_NO", typeof(System.String));
            _fromIO.Columns.Add("EST_ITM", typeof(System.Int32));
            _fromIO.Columns.Add("PRD_NO", typeof(System.String));
            _fromIO.Columns.Add("UNIT", typeof(System.Int32));
            _fromIO.Columns.Add("QTY_PS", typeof(System.Decimal));
            _fromIO.Columns.Add("QTY1_PS", typeof(System.Decimal));
            for (int i = 0; i < os_no.Count; i++)
            {
                DataRow[] _selIo = _fromIO.Select("OS_ID='SO' AND OS_NO='" + os_no[i] + "' AND EST_ITM='" + est_itm[i] + "' AND UNIT='" + unit[i] + "'");
                if (_selIo == null || _selIo.Length == 0)
                {
                    DataRow _newDr = _fromIO.NewRow();
                    _newDr["OS_ID"] = "SO";
                    _newDr["OS_NO"] = os_no[i];
                    _newDr["EST_ITM"] = Convert.ToInt32(est_itm[i]);
                    _newDr["PRD_NO"] = prdNo[i];
                    _newDr["UNIT"] = Convert.ToInt32(unit[i]);
                    _newDr["QTY_PS"] = Convert.ToDecimal(qty_ps[i]);
                    _newDr["QTY1_PS"] = Convert.ToDecimal(qty1_ps[i]);
                    _fromIO.Rows.Add(_newDr);
                }
                else
                {
                    _selIo[0]["QTY_PS"] = Convert.ToDecimal(_selIo[0]["QTY_PS"]) + Convert.ToDecimal(qty_ps[i]);
                    _selIo[0]["QTY1_PS"] = Convert.ToDecimal(_selIo[0]["QTY1_PS"]) + Convert.ToDecimal(qty1_ps[i]);
                }
            }
            _fromIO.AcceptChanges();
            #endregion

            #region 读取数据
            ArrayList _os_ids = new ArrayList();
            for (int i = 0; i < os_no.Count; i++)
            {
                _os_ids.Add("SO");
            }
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            SunlikeDataSet _osTable = _so.GetData(_os_ids, os_no, est_itm, "EST_ITM");
            DataTable _mf_posTable = _osTable.Tables["MF_POS"];
            DataTable _tf_posTable = _osTable.Tables["TF_POS"];
            #endregion

            //修改配送量 
            #region	 修改配送量
            string _os_id = "";
            string _os_no = "";
            string _prdNo = "";
            int _iUnit = 1;
            decimal _qty_ps = 0;
            decimal _qty1_ps = 0;
            decimal _qty = 0;
            for (int i = 0; i < _fromIO.Rows.Count; i++)
            {

                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["OS_NO"].ToString()))
                    _os_no = _fromIO.Rows[i]["OS_NO"].ToString();
                int _est_itm = 0;
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["EST_ITM"].ToString()))
                    _est_itm = Convert.ToInt32(_fromIO.Rows[i]["EST_ITM"]);
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["PRD_NO"].ToString()))
                    _prdNo = _fromIO.Rows[i]["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["UNIT"].ToString()))
                    _iUnit = Convert.ToInt32(_fromIO.Rows[i]["UNIT"]);
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["QTY_PS"].ToString()))
                    _qty_ps = Convert.ToDecimal(_fromIO.Rows[i]["QTY_PS"]);
                if (!String.IsNullOrEmpty(_fromIO.Rows[i]["QTY1_PS"].ToString()))
                    _qty1_ps = Convert.ToDecimal(_fromIO.Rows[i]["QTY1_PS"]);
                Hashtable _ht = new Hashtable();
                _ht["TableName"] = "TF_POS";
                _ht["IdName"] = "OS_ID";
                _ht["NoName"] = "OS_NO";
                _ht["ItmName"] = "EST_ITM";
                _ht["OsID"] = "SO";
                _ht["OsNO"] = _os_no;
                _ht["KeyItm"] = _est_itm;
                _qty_ps = INVCommon.GetRtnQty(_prdNo, _qty_ps, _iUnit, _ht);
                _qty1_ps = INVCommon.GetRtnQty(_prdNo, _qty1_ps, _iUnit, _ht);
                _so.UpdateClsId("SA", _os_no, "EST_ITM", _est_itm, "QTY_PS", _qty_ps);//修改已交量		
                _so.UpdateClsId("SA", _os_no, "EST_ITM", _est_itm, "QTY1_PS", _qty1_ps);//修改已交量				
            }
            #endregion
            //修改货品分仓存量的已受订量
            WH _wh = new WH();
            #region 修改货品分仓存量的已受订量
            for (int i = 0; i < _tf_posTable.Rows.Count; i++)
            {
                string _prd_no = "";
                string _prd_mark = "";
                string _swh = "";
                string _unit = "";
                string _batNo = "";
                string _validDd = "";
                string _backId = "";
                string _clsId = "";
                _qty_ps = 0;
                _qty = 0;
                _prd_no = _tf_posTable.Rows[i]["PRD_NO"].ToString();
                _prd_mark = _tf_posTable.Rows[i]["PRD_MARK"].ToString();
                _swh = _tf_posTable.Rows[i]["WH"].ToString();
                _unit = _tf_posTable.Rows[i]["UNIT"].ToString();
                _batNo = _tf_posTable.Rows[i]["BAT_NO"].ToString();
                if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["VALID_DD"].ToString()))
                {
                    _validDd = Convert.ToDateTime(_tf_posTable.Rows[i]["VALID_DD"]).ToString(Comp.SQLDateFormat);
                }
                _backId = _tf_posTable.Rows[i]["BACK_ID"].ToString();
                _clsId = _tf_posTable.Rows[i]["CLS_ID"].ToString();
                //手工结案
                if (_clsId == "T" && _backId.Length == 0)
                    continue;

                if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY"].ToString()))
                    _qty = Convert.ToDecimal(_tf_posTable.Rows[i]["QTY"]);
                DataRow[] _selFormIO = _fromIO.Select("OS_ID='" + _tf_posTable.Rows[i]["OS_ID"].ToString() + "' AND OS_NO='" + _tf_posTable.Rows[i]["OS_NO"].ToString() + "' AND EST_ITM='" + _tf_posTable.Rows[i]["EST_ITM"].ToString() + "' ");

                if ((_selFormIO.Length > 0) && (!String.IsNullOrEmpty(_selFormIO[0]["QTY_PS"].ToString())))
                {
                    _qty_ps = Convert.ToDecimal(_selFormIO[0]["QTY_PS"]);
                }
                if (_qty_ps > 0)
                {
                    //扣除已配送量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_IC"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除已出库量 LZJ 090316 QTY-QTY_PRE-QTY_IC-QTY_PS是受订回写量和QTY_RK无关
                    //if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_RK"].ToString()))
                    //{
                    //    if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK"]))
                    //    {
                    //        _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK"]);
                    //    }
                    //    else
                    //    {
                    //        _qty = 0;
                    //    }
                    //}
                    //扣除已销量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PS"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除受订退回
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PRE"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                }
                else
                {

                    //扣除已配送量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_IC"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //扣除已出库量  LZJ 090316 QTY-QTY_PRE-QTY_IC-QTY_PS是受订回写量和QTY_RK无关
                    //if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_RK"].ToString()))
                    //{
                    //    if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK"]))
                    //    {
                    //        _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_RK"]);
                    //    }
                    //    else
                    //    {
                    //        _qty = 0;
                    //    }
                    //}
                    //取得已受订退回
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PRE"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]))
                        {
                            _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PRE"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                    //取得已销量
                    if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PS"].ToString()))
                    {
                        _qty = System.Math.Abs(_qty_ps) - Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]) + _qty;//配送量-已销量+受订量
                        //**************************************************
                        //当表身两笔都是超交的情况，(受订量)qty就为负的情况时
                        //则受订扣减在最后一个超交的表身扣减
                        //**************************************************
                        if (_qty < 0)
                        {
                            return;
                        }

                    }
                }
                if (System.Math.Abs(_qty_ps) > _qty)
                {
                    if (_qty_ps < 0)
                        _qty_ps = (-1) * _qty;
                    else
                        _qty_ps = _qty;
                }
                //修改货品分仓存量的受订量
                if (_batNo.Length == 0)
                {
                    _wh.UpdateQty(_prd_no, _prd_mark, _swh, _unit, WH.QtyTypes.QTY_ON_ODR, _qty_ps * (-1));//修改受订量
                }
                else
                {
                    System.Collections.Hashtable _ht = new Hashtable();
                    _ht[WH.QtyTypes.QTY_ON_ODR] = _qty_ps * (-1);
                    _wh.UpdateQty(_batNo, _prd_no, _prd_mark, _swh, _validDd, _unit, _ht);
                }
                //结案受订单
                UpdateClsId(_tf_posTable.Rows[i]["OS_NO"].ToString());
            }  //end for 
            #endregion

            Cust _cust = new Cust();
            //计算信用额度
            #region 计算信用额度
            for (int i = 0; i < _mf_posTable.Rows.Count; i++)
            {
                string _cusNo = _mf_posTable.Rows[i]["CUS_NO"].ToString();

                if (_cust.IsSo_Crd(_cusNo))
                {
                    string _arpId = "1";
                    string _idxNo = "";
                    string _fieldName = "AMTN_SO";
                    decimal _amtn = 0;
                    decimal _excRto = 0;
                    int _year = 0;
                    _year = Convert.ToDateTime(_mf_posTable.Rows[i]["OS_DD"]).Year;
                    int _month = 0;
                    _month = Convert.ToDateTime(_mf_posTable.Rows[i]["OS_DD"]).Month;
                    _os_id = "";
                    _os_no = "";
                    //取汇率
                    if (!String.IsNullOrEmpty(_mf_posTable.Rows[0]["EXC_RTO"].ToString()))
                        _excRto = Convert.ToDecimal(_mf_posTable.Rows[0]["EXC_RTO"]);
                    _os_id = _mf_posTable.Rows[i]["OS_ID"].ToString();
                    _os_no = _mf_posTable.Rows[i]["OS_NO"].ToString();
                    DataRow[] _selBody = _tf_posTable.Select("OS_ID='" + _os_id + "' AND OS_NO='" + _os_no + "'");
                    for (int j = 0; j < _selBody.Length; j++)
                    {
                        decimal _up = 0;
                        _qty = 0;
                        _qty_ps = 0;
                        if (!String.IsNullOrEmpty(_selBody[j]["UP"].ToString()))
                        {
                            _up = Convert.ToDecimal(_selBody[j]["UP"]);
                        }
                        if (!String.IsNullOrEmpty(_selBody[j]["QTY"].ToString()))
                        {
                            _qty = Convert.ToDecimal(_selBody[j]["QTY"]);
                        }

                        DataRow[] _selQtyIc = _fromIO.Select("OS_ID='" + _selBody[j]["OS_ID"].ToString() + "' AND OS_NO='" + _selBody[j]["OS_NO"].ToString() + "' AND EST_ITM='" + _selBody[j]["EST_ITM"].ToString() + "' ");
                        if ((_selQtyIc.Length > 0) && (!String.IsNullOrEmpty(_selQtyIc[0]["QTY_PS"].ToString())))
                        {
                            _qty_ps = Convert.ToDecimal(_selQtyIc[0]["QTY_PS"]);
                        }
                        if (_qty_ps > 0)
                        {
                            //扣除已配送量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_IC"].ToString()))
                            {
                                if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]))
                                {
                                    _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]);
                                }
                                else
                                {
                                    _qty = 0;
                                }
                            }
                            //扣除已销量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PS"].ToString()))
                            {
                                if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]))
                                {
                                    _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]);
                                }
                                else
                                {
                                    _qty = 0;
                                }
                            }
                        }
                        else
                        {
                            //扣除已配送量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_IC"].ToString()))
                            {
                                if (_qty > Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]))
                                {
                                    _qty -= Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_IC"]);
                                }
                                else
                                {
                                    _qty = 0;
                                }
                            }
                            //取得已销量
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["QTY_PS"].ToString()))
                            {
                                _qty = System.Math.Abs(_qty_ps) - Convert.ToDecimal(_tf_posTable.Rows[i]["QTY_PS"]) + _qty;//销售量-已销量+受订量
                                //**************************************************
                                //当表身两笔都是超交的情况，(受订量)qty就为负的情况时
                                //则受订扣减在最后一个超交的表身扣减
                                //**************************************************
                                if (_qty < 0)
                                {
                                    return;
                                }

                            }
                        }
                        if (_qty >= System.Math.Abs(_qty_ps))
                        {
                            _qty = _qty_ps;
                        }
                        else if (_qty_ps < 0)
                        {
                            _qty *= -1;
                        }
                        _amtn += _qty * _up * _excRto;
                    }//end for j 查询每一张受订单需要修改的金额
                    Arp _arp = new Arp();
                    _arp.UpdateSarp(_arpId, _year, _cusNo, _month, _idxNo, _fieldName, (-1) * _amtn);
                }// end select IsSo_Crd			
            }//end for i
            #endregion
        }
        #endregion

        #region 修改已退回量
        /// <summary>
        /// 修改已退回量
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="statementType"></param>
        public void UpdateQtyPre(DataRow dr, StatementType statementType)
        {
            string _osId = "";
            string _osNo = "";
            string _osNoOriginal = "";
            int _itm = 0;
            int _itmOriginal = 0;
            decimal _qtyPre = 0;
            decimal _qtyPreOriginal = 0;
            string _prdNo = "";
            string _oldPrdNo = "";
            int _unit = 1;
            int _oldUnit = 1;
            _osId = "SO";
            if (statementType == StatementType.Insert)
            {
                _osNo = dr["QT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["PRE_ITM"].ToString()))
                    _itm = Convert.ToInt32(dr["PRE_ITM"]);
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qtyPre = Convert.ToDecimal(dr["QTY"]);
                }
                _prdNo = dr["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    _unit = Convert.ToInt32(dr["UNIT"]);
            }
            else if (statementType == StatementType.Update)
            {
                _osNo = dr["QT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["PRE_ITM"].ToString()))
                    _itm = Convert.ToInt32(dr["PRE_ITM"]);
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qtyPre = Convert.ToDecimal(dr["QTY"]);
                }
                _prdNo = dr["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    _unit = Convert.ToInt32(dr["UNIT"]);

                _osNoOriginal = dr["QT_NO", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["PRE_ITM", System.Data.DataRowVersion.Original].ToString()))
                    _itmOriginal = Convert.ToInt32(dr["PRE_ITM", System.Data.DataRowVersion.Original]);
                if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                {
                    _qtyPreOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                }
                _oldPrdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    _oldUnit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
            }
            else if (statementType == StatementType.Delete)
            {

                _osNoOriginal = dr["QT_NO", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["PRE_ITM", System.Data.DataRowVersion.Original].ToString()))
                    _itmOriginal = Convert.ToInt32(dr["PRE_ITM", System.Data.DataRowVersion.Original]);
                if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                {
                    _qtyPreOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                }
                _oldPrdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    _oldUnit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
            }
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);

            Hashtable _ht = new Hashtable();
            _ht["TableName"] = "TF_POS";
            _ht["IdName"] = "OS_ID";
            _ht["NoName"] = "OS_NO";
            _ht["ItmName"] = "PRE_ITM";
            _ht["OsID"] = _osId;
            if (_osNoOriginal.Length > 0)
            {
                _ht["OsNO"] = _osNoOriginal;
                _ht["KeyItm"] = _itmOriginal;
                _qtyPreOriginal = INVCommon.GetRtnQty(_oldPrdNo, (-1) * _qtyPreOriginal, _oldUnit, _ht);
                _so.UpdateClsId("SR", _osNoOriginal, "PRE_ITM", _itmOriginal, "QTY_PRE", _qtyPreOriginal);
            }
            if (_osNo.Length > 0)
            {
                _ht["OsNO"] = _osNo;
                _ht["KeyItm"] = _itm;
                _qtyPre = INVCommon.GetRtnQty(_prdNo, _qtyPre, _unit, _ht);
                _so.UpdateClsId("SR", _osNo, "PRE_ITM", _itm, "QTY_PRE", _qtyPre);
            }
        }
        #endregion

        #region 修改受订单已采购量
        /// <summary>
        /// 修改受订单已采购量
        /// </summary>
        /// <param name="backId"></param>
        /// <param name="osNo"></param>
        /// <param name="uniqueItemField"></param>
        /// <param name="uniqueItm"></param>
        /// <param name="updateField"></param>
        /// <param name="updateValue"></param>
        /// <returns></returns>
        public bool UpdateClsId(string backId, string osNo, string uniqueItemField, int uniqueItm, string updateField, decimal updateValue)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            return _so.UpdateClsId(backId, osNo, uniqueItemField, uniqueItm, updateField, updateValue);
        }
        #endregion

        #region 修改应收款月余额档
        /// <summary>
        ///  修改应收款月余额档
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isAuditing"></param>
        /// <param name="isDel"></param>
        private void UpdateSarp(SunlikeDataSet ds, bool isAuditing, bool isDel)
        {
            if (isAuditing)
                return;
            if (!(ds.HasErrors))
            {
                string _mOsId = "";
                string _cusNoOriginal = "";
                string _cusNoCurrent = "";
                int _yearOriginal = 0;
                int _yearCurrent = 0;
                int _monthOriginal = 0;
                int _monthCurrent = 0;
                decimal _taxOriginal = 0;
                decimal _taxCurrent = 0;
                decimal _amtnOriginal = 0;
                decimal _amtnCurrent = 0;
                decimal _amtnTaxOriginal = 0;
                decimal _amtnTaxCurrent = 0;
                decimal _qtyFlag = 1;

                DataTable _mf_posTable = ds.Tables["MF_POS"];
                DataTable _tf_posTable = ds.Tables["TF_POS"];

                if (_mf_posTable.Rows.Count > 0)
                {
                    if (_mf_posTable.Rows[0].RowState != System.Data.DataRowState.Deleted)
                    {
                        _mOsId = _mf_posTable.Rows[0]["OS_ID"].ToString();
                        _cusNoCurrent = _mf_posTable.Rows[0]["CUS_NO"].ToString();
                        _yearCurrent = Convert.ToDateTime(_mf_posTable.Rows[0]["OS_DD"]).Year;
                        _monthCurrent = Convert.ToDateTime(_mf_posTable.Rows[0]["OS_DD"]).Month;
                    }
                    if (_mf_posTable.Rows[0].RowState == System.Data.DataRowState.Modified || _mf_posTable.Rows[0].RowState == System.Data.DataRowState.Deleted)
                    {
                        _mOsId = _mf_posTable.Rows[0]["OS_ID", System.Data.DataRowVersion.Original].ToString();
                        _cusNoOriginal = _mf_posTable.Rows[0]["CUS_NO", System.Data.DataRowVersion.Original].ToString();
                        _yearOriginal = Convert.ToDateTime(_mf_posTable.Rows[0]["OS_DD", System.Data.DataRowVersion.Original]).Year;
                        _monthOriginal = Convert.ToDateTime(_mf_posTable.Rows[0]["OS_DD", System.Data.DataRowVersion.Original]).Month;
                    }
                }
                if (_mOsId == "SR")
                    _qtyFlag = -1;

                for (int i = 0; i < _tf_posTable.Rows.Count; i++)
                {
                    _taxOriginal = 0;
                    _taxCurrent = 0;
                    _amtnOriginal = 0;
                    _amtnCurrent = 0;
                    if (_tf_posTable.Rows[i].RowState != System.Data.DataRowState.Unchanged)
                    {
                        if (_tf_posTable.Rows[i].RowState != System.Data.DataRowState.Deleted)
                        {
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["AMTN"].ToString()))
                                _amtnCurrent = Convert.ToDecimal(_tf_posTable.Rows[i]["AMTN"]);
                            if (!String.IsNullOrEmpty(_tf_posTable.Rows[i]["TAX"].ToString()))
                                _taxCurrent = Convert.ToDecimal(_tf_posTable.Rows[i]["TAX"]);
                        }
                        if (_tf_posTable.Rows[i].RowState == System.Data.DataRowState.Modified || _tf_posTable.Rows[i].RowState == System.Data.DataRowState.Deleted)
                        {
                            if (_tf_posTable.Rows[i]["AMTN", System.Data.DataRowVersion.Original].ToString() != "")
                                _amtnOriginal = Convert.ToDecimal(_tf_posTable.Rows[i]["AMTN", System.Data.DataRowVersion.Original]);
                            if (_tf_posTable.Rows[i]["TAX", System.Data.DataRowVersion.Original].ToString() != "")
                                _taxOriginal = Convert.ToDecimal(_tf_posTable.Rows[i]["TAX", System.Data.DataRowVersion.Original]);
                        }
                        _amtnTaxOriginal += _amtnOriginal + _taxOriginal;
                        _amtnTaxCurrent += _amtnCurrent + _taxCurrent;
                    }
                }//end for
                Arp _arp = new Arp();
                if (!String.IsNullOrEmpty(_cusNoCurrent) && _yearCurrent != 0 && _monthCurrent != 0)
                {
                    if (isDel)
                    {
                        _arp.UpdateSarp("1", _yearCurrent, _cusNoCurrent, _monthCurrent, "", "AMTN_SO", (-1) * _qtyFlag * _amtnTaxCurrent);
                    }
                    else
                    {
                        _arp.UpdateSarp("1", _yearCurrent, _cusNoCurrent, _monthCurrent, "", "AMTN_SO", _qtyFlag * _amtnTaxCurrent);
                    }
                }
                if (!String.IsNullOrEmpty(_cusNoOriginal) && _yearOriginal != 0 && _monthOriginal != 0)
                {
                    _arp.UpdateSarp("1", _yearOriginal, _cusNoOriginal, _monthOriginal, "", "AMTN_SO", (-1) * _qtyFlag * _amtnTaxOriginal);
                }
            }
        }

        #endregion

        #region 结案
        /// <summary>
        /// 根据单据的结案及反结案，相应的加、减库存中的受订量
        /// </summary>
        /// <param name="osId">单据别</param>
        /// <param name="osNo">单据日期</param>
        public void DoCloseBill(string osId, string osNo)
        {
            bool _isCloseIt = false;
            string _backId = "";
            if (osId == "SR")
                return;
            SunlikeDataSet _soDs = this.GetData(null, osId, osNo, false, false);
            DataTable _soMf = _soDs.Tables["MF_POS"];
            DataTable _soTf = _soDs.Tables["TF_POS"];
            DataTable _soTfBox = _soDs.Tables["TF_POS1"];
            if (_soMf.Rows.Count > 0)
            {
                if (_soMf.Rows[0]["CLS_ID"].ToString() == "T")
                    _isCloseIt = true;
                _backId = _soMf.Rows[0]["BACK_ID"].ToString();
                if (_backId.Length > 0)
                    return;
                string _prdNo = "";
                string _prdMark = "";
                string _content = "";
                string _batNo = "";
                string _validDd = "";
                string _whNo = "";
                string _unit = "";
                decimal _qty = 0;
                decimal _qtyIc = 0;
                decimal _qtyPs = 0;
                decimal _qtyPre = 0;
                decimal _qtyOnOdr = 0;
                WH _wh = new WH();
                //结案					
                #region	件
                for (int i = 0; i < _soTf.Rows.Count; i++)
                {
                    // 初始化
                    _qty = 0;
                    _qtyIc = 0;
                    _qtyPs = 0;
                    _qtyPre = 0;

                    _prdNo = _soTf.Rows[i]["PRD_NO"].ToString();
                    _prdMark = _soTf.Rows[i]["PRD_MARK"].ToString();
                    _whNo = _soTf.Rows[i]["WH"].ToString();
                    _batNo = _soTf.Rows[i]["BAT_NO"].ToString();
                    _validDd = _soTf.Rows[i]["VALID_DD"].ToString();
                    _unit = _soTf.Rows[i]["UNIT"].ToString();
                    //数量
                    if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY"].ToString()))
                    {
                        _qty = Convert.ToDecimal(_soTf.Rows[i]["QTY"]);
                    }
                    //配送量
                    if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_IC"].ToString()))
                    {
                        _qtyIc = Convert.ToDecimal(_soTf.Rows[i]["QTY_IC"]);
                    }
                    //已销量
                    if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_PS"].ToString()))
                    {
                        _qtyPs = Convert.ToDecimal(_soTf.Rows[i]["QTY_PS"]);
                    }
                    //受订退回
                    if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_PRE"].ToString()))
                    {
                        _qtyPre = Convert.ToDecimal(_soTf.Rows[i]["QTY_PRE"]);
                    }
                    _qtyOnOdr = _qty - _qtyIc - _qtyPs - _qtyPre;//数量-配送量-已销量-受订退回
                    if (_qtyOnOdr < 0)
                        _qtyOnOdr = 0;

                    if (_qtyOnOdr > 0)//修改库存受订量
                    {
                        if (_isCloseIt)//扣除剩余受订量 
                        {
                            _qtyOnOdr = (-1) * _qtyOnOdr;
                        }
                        if (String.IsNullOrEmpty(_batNo))
                        {
                            _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, WH.QtyTypes.QTY_ON_ODR, _qtyOnOdr);
                        }
                        else//批号库存
                        {
                            Prdt _prdt = new Prdt();
                            SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                            Hashtable _ht = new Hashtable();
                            _ht[WH.QtyTypes.QTY_ON_ODR] = _qtyOnOdr;
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
                    }

                }
                #endregion

                #region 箱

                for (int i = 0; i < _soTfBox.Rows.Count; i++)
                {
                    // 初始化
                    _qty = 0;
                    _qtyOnOdr = 0;
                    _qtyIc = 0;
                    _prdNo = _soTfBox.Rows[i]["PRD_NO"].ToString();
                    _content = _soTfBox.Rows[i]["CONTENT"].ToString();
                    _whNo = _soTfBox.Rows[i]["WH"].ToString();
                    //受订量
                    if (!String.IsNullOrEmpty(_soTfBox.Rows[i]["QTY"].ToString()))
                    {
                        _qty = Convert.ToDecimal(_soTfBox.Rows[i]["QTY"]);
                    }
                    //已配送量
                    if (!String.IsNullOrEmpty(_soTfBox.Rows[i]["QTY_IC"].ToString()))
                    {
                        _qtyIc = Convert.ToDecimal(_soTfBox.Rows[i]["QTY_IC"]);
                    }
                    _qtyOnOdr = _qty - _qtyIc;//数量-配送量
                    if (_qtyOnOdr < 0)
                        _qtyOnOdr = 0;
                    if (_qtyOnOdr > 0)
                    {
                        if (_isCloseIt)//扣除剩余受订量 
                        {
                            _qtyOnOdr = (-1) * _qtyOnOdr;
                        }
                        _wh.UpdateBoxQty(_prdNo, _whNo, _content, WH.BoxQtyTypes.QTY_ON_ODR, _qtyOnOdr);
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="osId">单据别</param>
        /// <param name="osNo">单据日期</param>
        /// <param name="close">是否结案</param>
        /// <returns></returns>
        public string DoCloseBill(string osId, string osNo, bool close)
        {

            bool _isCloseIt = false;
            bool _isCheck = true;
            string _backId = "";
            string _result = "";
            SunlikeDataSet _soDs = this.GetData(null, osId, osNo, false, false);
            DataTable _soMf = _soDs.Tables["MF_POS"];
            DataTable _soTf = _soDs.Tables["TF_POS"];
            DataTable _soTfBox = _soDs.Tables["TF_POS1"];
            if (_soMf.Rows.Count > 0)
            {
                if (_soMf.Rows[0]["CLS_ID"].ToString() == "T")
                    _isCloseIt = true;
                if (_soMf.Rows[0]["CHK_MAN"].ToString().Length == 0)
                    _isCheck = false;

                if (close == _isCloseIt)
                {
                    if (close)
                    {
                        return "RCID=COMMON.HINT.CLOSEERROR,PARAM=" + osNo;//该单据[{0}]已结案,结案动作不能完成!
                    }
                    else
                    {
                        return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + osNo;//该单据[{0}]未结案,未结案动作不能完成!
                    }
                }
                _backId = _soMf.Rows[0]["BACK_ID"].ToString();
                if (_backId.Length > 0)
                    return "RCID=COMMON.HINT.CLOSEERROR2,PARAM=" + osNo;//该单据[{0}]是非手工结案！
                string _prdNo = "";
                string _prdMark = "";
                string _content = "";
                string _batNo = "";
                string _validDd = "";
                string _whNo = "";
                string _unit = "";
                decimal _qty = 0;
                decimal _qtyIc = 0;
                decimal _qtyPs = 0;
                decimal _qtyPre = 0;
                decimal _qtyOnOdr = 0;
                WH _wh = new WH();
                try
                {
                    if (_isCheck)
                    {
                        //结案					
                        #region	件

                        for (int i = 0; i < _soTf.Rows.Count; i++)
                        {
                            // 初始化
                            _qty = 0;
                            _qtyIc = 0;
                            _qtyPs = 0;
                            _qtyPre = 0;

                            _prdNo = _soTf.Rows[i]["PRD_NO"].ToString();
                            _prdMark = _soTf.Rows[i]["PRD_MARK"].ToString();
                            _whNo = _soTf.Rows[i]["WH"].ToString();
                            _batNo = _soTf.Rows[i]["BAT_NO"].ToString();
                            _validDd = _soTf.Rows[i]["VALID_DD"].ToString();
                            _unit = _soTf.Rows[i]["UNIT"].ToString();
                            //数量
                            if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY"].ToString()))
                            {
                                _qty = Convert.ToDecimal(_soTf.Rows[i]["QTY"]);
                            }
                            //配送量
                            if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_IC"].ToString()))
                            {
                                _qtyIc = Convert.ToDecimal(_soTf.Rows[i]["QTY_IC"]);
                            }
                            //已销量
                            if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_PS"].ToString()))
                            {
                                _qtyPs = Convert.ToDecimal(_soTf.Rows[i]["QTY_PS"]);
                            }
                            //受订退回
                            if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_PRE"].ToString()))
                            {
                                _qtyPre = Convert.ToDecimal(_soTf.Rows[i]["QTY_PRE"]);
                            }
                            _qtyOnOdr = _qty - _qtyIc - _qtyPs - _qtyPre;//数量-配送量-已销量-受订退回
                            if (_qtyOnOdr < 0)
                                _qtyOnOdr = 0;

                            if (_qtyOnOdr > 0)//修改库存受订量
                            {
                                if (close)//扣除剩余受订量 
                                {
                                    _qtyOnOdr = (-1) * _qtyOnOdr;
                                }
                                if (String.IsNullOrEmpty(_batNo))
                                {
                                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, WH.QtyTypes.QTY_ON_ODR, _qtyOnOdr);
                                }
                                else//批号库存
                                {
                                    Prdt _prdt = new Prdt();
                                    SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                                    Hashtable _ht = new Hashtable();
                                    _ht[WH.QtyTypes.QTY_ON_ODR] = _qtyOnOdr;
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
                            }

                        }
                        #endregion

                        #region 箱

                        for (int i = 0; i < _soTfBox.Rows.Count; i++)
                        {
                            // 初始化
                            _qty = 0;
                            _qtyOnOdr = 0;
                            _qtyIc = 0;
                            _prdNo = _soTfBox.Rows[i]["PRD_NO"].ToString();
                            _content = _soTfBox.Rows[i]["CONTENT"].ToString();
                            _whNo = _soTfBox.Rows[i]["WH"].ToString();
                            //受订量
                            if (!String.IsNullOrEmpty(_soTfBox.Rows[i]["QTY"].ToString()))
                            {
                                _qty = Convert.ToDecimal(_soTfBox.Rows[i]["QTY"]);
                            }
                            //已配送量
                            if (!String.IsNullOrEmpty(_soTfBox.Rows[i]["QTY_IC"].ToString()))
                            {
                                _qtyIc = Convert.ToDecimal(_soTfBox.Rows[i]["QTY_IC"]);
                            }
                            _qtyOnOdr = _qty - _qtyIc;//数量-配送量
                            if (_qtyOnOdr < 0)
                                _qtyOnOdr = 0;
                            if (_qtyOnOdr > 0)
                            {
                                if (close)//扣除剩余受订量 
                                {
                                    _qtyOnOdr = (-1) * _qtyOnOdr;
                                }
                                _wh.UpdateBoxQty(_prdNo, _whNo, _content, WH.BoxQtyTypes.QTY_ON_ODR, _qtyOnOdr);
                            }
                        }
                        #endregion

                        //回写信用额度
                        UpdateSarp(_soDs, false, close);
                    }

                    //打上结案标记
                    DoCloseSO(osId, osNo, close);
                }
                catch (Exception _ex)
                {
                    _result = _ex.Message.ToString();
                }

            }
            return _result;
        }
        #endregion

        #endregion

        #region 修改收件单已送量
        /// <summary>
        /// 修改收件单已送量
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isAdd"></param>
        private void UpdateRcv(DataRow dr, bool isAdd)
        {
            string _rvId = "";
            string _rvNo = "";
            string _unit = "";
            int _keyItm = 0;
            decimal _qtySo = 0;
            if (isAdd)
            {
                _rvId = dr["BIL_ID"].ToString();
                _rvNo = dr["QT_NO"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                {
                    _keyItm = Convert.ToInt32(dr["OTH_ITM"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qtySo = Convert.ToDecimal(dr["QTY"].ToString());
                }
            }
            else
            {
                _rvId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _rvNo = dr["QT_NO", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["OTH_ITM", DataRowVersion.Original].ToString()))
                {
                    _keyItm = Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original].ToString());
                }
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qtySo = (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                }
            }
            MTNRcv _rcv = new MTNRcv();
            if (_keyItm != 0)
            {
                _rcv.UpdateQtySo(_rvId, _rvNo, _keyItm, _unit, _qtySo);
            }

        }
        #endregion

        #region 修改收件单已送量
        /// <summary>
        /// 修改收件单已送量
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isAdd"></param>
        private void UpdateMa(DataRow dr, bool isAdd)
        {
            string _maId = "";
            string _maNo = "";
            string _unit = "";
            int _keyItm = 0;
            decimal _qty = 0;
            if (isAdd)
            {
                _maId = dr["BIL_ID"].ToString();
                _maNo = dr["QT_NO"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                {
                    _keyItm = Convert.ToInt32(dr["OTH_ITM"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"].ToString());
                }
            }
            else
            {
                _maId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _maNo = dr["QT_NO", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["OTH_ITM", DataRowVersion.Original].ToString()))
                {
                    _keyItm = Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original].ToString());
                }
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                }
            }
            MTNMa _ma = new MTNMa();
            if (_keyItm != 0)
            {
                _ma.UpdateQtyMtn(_maId, _maNo, _keyItm, _unit, _qty);
            }

        }
        #endregion

        #region 修改采购单的采购量
        /// <summary>
        /// 修改采购单的采购量
        /// </summary>
        /// <param name="dr">TF_POS行信息</param>
        /// <param name="isAdd">是否新增</param>
        /// <param name="flag">金额符号反转标记 : true:颠倒原先符号，false:维持原先符号</param>
        private void UpdateQtyPo(DataRow dr, bool isAdd, bool flag)
        {
            string _osId = "";
            string _osNo = "";
            string _unit = "";
            string _chkMan = "";
            int _othItm = 0;
            decimal _qty = 0;
            //取表头信息
            if (dr.Table.DataSet != null && dr.Table.DataSet.Tables.Contains("MF_POS") && dr.Table.DataSet.Tables["MF_POS"].Rows.Count > 0)
            {
                if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == DataRowState.Deleted)
                {
                    _chkMan = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CHK_MAN", DataRowVersion.Original].ToString();
                }
                else
                {
                    _chkMan = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CHK_MAN"].ToString();
                }
            }
            if (isAdd)
            {
                _osId = dr["BIL_ID"].ToString();
                _osNo = dr["QT_NO"].ToString();
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
                _osId = dr["BIL_ID", DataRowVersion.Original].ToString();
                _osNo = dr["QT_NO", DataRowVersion.Original].ToString();
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
            DRPPO _po = new DRPPO();
            if (_othItm != 0)
            {
                if (string.IsNullOrEmpty(_chkMan))//走审核 
                {
                    _po.UpdateQtyPoUnsh(_osId, _osNo, _othItm, _unit, _qty);
                    if (flag)
                    {
                        _po.UpdateQtyPo(_osId, _osNo, _othItm, _unit, (-1) * _qty);
                    }
                }
                else//不走审核 
                {
                    _po.UpdateQtyPo(_osId, _osNo, _othItm, _unit, _qty);
                    if (flag)
                    {
                        _po.UpdateQtyPoUnsh(_osId, _osNo, _othItm, _unit, (-1) * _qty);
                    }
                }

            }
        }
        #endregion

        #region 判断及检查(可用库存，单据是否修改，结案)
        #region 判断可用库存
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lowWhQtyAccept">库存不足是否接受</param>
        /// <param name="QtyWayFact">是否是实际库存</param>
        /// <param name="under">是否包下属储位</param>
        /// <param name="ds"></param>
        public void CheckWhUseful(bool lowWhQtyAccept, bool QtyWayFact, bool under, SunlikeDataSet ds)
        {
            CompInfo _compInfo = Comp.GetCompInfo("");
            if (lowWhQtyAccept == false)
            {
                WH _wh = new WH();
                DataTable _dtTf = ds.Tables["TF_POS"];
                DataTable _dtTf1 = ds.Tables["TF_POS1"];
                string _errorPrdNo = "";
                #region 判断产品库存
                StringBuilder _sTf = new StringBuilder();
                foreach (DataRow dr in _dtTf.Rows)
                {
                    if (_sTf.ToString().IndexOf(dr["PRD_NO"].ToString() + ":" + dr["PRD_MARK"].ToString() + ":" + dr["WH"].ToString()) < 0)
                    {
                        if (!String.IsNullOrEmpty(_sTf.ToString()))
                        {
                            _sTf.Append(";");
                        }
                        _sTf.Append(dr["PRD_NO"].ToString() + ":" + dr["PRD_MARK"].ToString() + ":" + dr["WH"].ToString());
                    }
                }
                if (!String.IsNullOrEmpty(_sTf.ToString()))
                {
                    WH _drpWh = new WH();
                    string[] _aryPrdt1 = _sTf.ToString().Split(';');
                    foreach (string str in _aryPrdt1)
                    {
                        string[] _aryPrdt2 = str.Split(':');
                        if (_aryPrdt2.Length == 3)
                        {
                            DataRow[] _aryDr = _dtTf.Select("PRD_NO='" + _aryPrdt2[0] + "' AND PRD_MARK='" + _aryPrdt2[1] + "' AND WH='" + _aryPrdt2[2] + "' AND ( BOX_ITM=0 OR BOX_ITM IS NULL) ");
                            if (_aryDr.Length > 0)
                            {
                                decimal _qty = 0;
                                decimal _qtyEq = 0;

                                foreach (DataRow drQty in _aryDr)
                                {
                                    _qty += Convert.ToDecimal(drQty["QTY"]);

                                    string _batNo = drQty["BAT_NO"].ToString();
                                    Prdt _prdt = new Prdt();
                                    bool _chkBat = false;//是否批号控制产品
                                    DataTable _dtPrdt = _prdt.GetPrdt(drQty["PRD_NO"].ToString());
                                    if (_dtPrdt.Rows.Count > 0)
                                    {
                                        if (CaseInsensitiveComparer.Default.Compare(_dtPrdt.Rows[0]["CHK_BAT"].ToString(), "T") == 0)
                                        {
                                            _chkBat = true;
                                        }
                                    }
                                    if (QtyWayFact)//实际库存
                                    {
                                        _qtyEq += _drpWh.GetSumQty(true, drQty["PRD_NO"].ToString(), drQty["PRD_MARK"].ToString(), drQty["WH"].ToString(), under, _batNo, _chkBat);

                                    }
                                    else
                                    {
                                        _qtyEq += _drpWh.GetSumQty(false, drQty["PRD_NO"].ToString(), drQty["PRD_MARK"].ToString(), drQty["WH"].ToString(), under, _batNo, _chkBat);
                                    }
                                }
                                //decimal _qtyEq = _wh.GetQty(_bType, _aryPrdt2[0], _aryPrdt2[1], _aryPrdt2[2],string.Empty);
                                if (_qty > _qtyEq)
                                {
                                    if (QtyWayFact)//实际库存
                                    {
                                        _errorPrdNo += ";RCID=INV.HINT.CHECKQTYDATA,PARAM=" + _aryPrdt2[2] + ",PARAM=" + _aryPrdt2[0] + ",PARAM=" + _aryPrdt2[1] + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _qty) + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _qtyEq);
                                    }
                                    else
                                    {
                                        _errorPrdNo += ";RCID=INV.HINT.CHECKQTYDATA,PARAM=" + _aryPrdt2[2] + ",PARAM=" + _aryPrdt2[0] + ",PARAM=" + _aryPrdt2[1] + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _qty) + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _qtyEq);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
                #region 判断箱库存
                StringBuilder _sTf1 = new StringBuilder();
                foreach (DataRow dr in _dtTf1.Rows)
                {
                    if (_sTf1.ToString().IndexOf(dr["PRD_NO"].ToString() + ":" + dr["CONTENT"].ToString() + ":" + dr["WH"].ToString()) < 0)
                    {
                        if (!String.IsNullOrEmpty(_sTf1.ToString()))
                        {
                            _sTf1.Append("|");
                        }
                        _sTf1.Append(dr["PRD_NO"].ToString() + ":" + dr["CONTENT"].ToString() + ":" + dr["WH"].ToString());
                    }
                }
                if (!String.IsNullOrEmpty(_sTf1.ToString()))
                {
                    string[] _aryBox1 = _sTf1.ToString().Split('|');
                    foreach (string str in _aryBox1)
                    {
                        string[] _aryBox2 = str.Split(':');
                        if (_aryBox2.Length == 3)
                        {
                            DataRow[] _aryDr = _dtTf1.Select("PRD_NO='" + _aryBox2[0] + "' AND CONTENT='" + _aryBox2[1] + "' AND WH='" + _aryBox2[2] + "'");
                            decimal _qty = 0;
                            foreach (DataRow drQty in _aryDr)
                            {
                                _qty += Convert.ToDecimal(drQty["QTY"]);
                            }
                            decimal _qtyEq = _wh.GetBoxQty(QtyWayFact, _aryBox2[0], _aryBox2[2], _aryBox2[1]);
                            if (_qty > _qtyEq)
                            {
                                if (QtyWayFact)
                                {
                                    _errorPrdNo += ";RCID=INV.HINT.CHECKQTYDATA,PARAM=" + _aryBox2[2] + ",PARAM=" + _aryBox2[0] + ",PARAM=" + _aryBox2[1].Replace(";", "|") + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _qty) + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _qtyEq);
                                }
                                else
                                {
                                    _errorPrdNo += ";RCID=INV.HINT.CHECKQTYDATA,PARAM=" + _aryBox2[2] + ",PARAM=" + _aryBox2[0] + ",PARAM=" + _aryBox2[1].Replace(";", "|") + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _qty) + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", _qtyEq);
                                }
                            }
                        }
                    }
                }
                #endregion
                if (_errorPrdNo.Length > 0)
                {
                    if (QtyWayFact)
                    {
                        throw new SunlikeException("RCID=INV.HINT.USEFULNOSO;RCID=INV.HINT.CHECKQTYCANUSR" + _errorPrdNo);
                    }
                    else
                    {
                        throw new SunlikeException("RCID=INV.HINT.USEFULNOSO;RCID=INV.HINT.CHECKQTYCANUSR" + _errorPrdNo);
                    }
                }

            }
        }
        /// <summary>
        /// 检查库存量
        /// </summary>
        /// <param name="dr">表身</param>
        /// <param name="isModify">是否考虑修改行</param>
        public void CheckWhUsefulTf(DataRow dr, bool isModify)
        {
            //--------------by yola 
            string _lowWhQty = Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString();//可用库存不足是否接受要货
            string _chk_qty_way = Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString();//要货/受订库存控制方式（0：按可用库存要货/受订，1：按实际库存要货/受订）
            if (_lowWhQty == "F")
            {
                Sunlike.Business.DRPYHut _drpYhut = new DRPYHut();
                decimal _qty = _drpYhut.GetWhQty(dr["WH"].ToString(), dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString());
                decimal _originalQty = 0;
                if (isModify)
                {
                    if (dr.RowState == System.Data.DataRowState.Modified)
                    {
                        if ((dr["PRD_NO", DataRowVersion.Original].ToString() == dr["PRD_NO", DataRowVersion.Current].ToString()) && (dr["PRD_MARK", DataRowVersion.Original].ToString() == dr["PRD_MARK", DataRowVersion.Current].ToString()) && (dr["WH", DataRowVersion.Original].ToString() == dr["WH", DataRowVersion.Current].ToString()))
                        {
                            //加上原数量
                            if (dr["QTY", System.Data.DataRowVersion.Original] != System.DBNull.Value)
                                _originalQty = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                            _qty += _originalQty;
                        }
                    }
                }
                if (Convert.ToDecimal(dr["QTY"]) > _qty)
                {
                    if (_chk_qty_way == "1")
                    {
                        throw new Exception("RCID=INV.HINT.FACTPRDT1,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["WH"].ToString() + ",PARAM=" + dr["PRD_MARK"].ToString());
                    }
                    else
                    {
                        throw new Exception("RCID=INV.HINT.UNFACTPRDT1,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["WH"].ToString() + ",PARAM=" + dr["PRD_MARK"].ToString());
                    }
                }
            }
        }
        /// <summary>
        /// 检查库存量
        /// </summary>
        /// <param name="dr">表身(箱)</param>
        /// <param name="isModify">是否考虑修改行</param>
        public void CheckWhUsefulTfBox(DataRow dr, bool isModify)
        {
            WH _drpWh = new WH();
            string _lowWhQty = Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString();
            string _chk_qty_way = Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString();

            if ((_lowWhQty == "F"))
            {
                decimal _boxQty = 0;
                decimal _originalBoxQty = 0;//原来单据的箱数量
                if (_chk_qty_way == "1")
                {
                    _boxQty = _drpWh.GetBoxQty(true, dr["PRD_NO"].ToString(), dr["WH"].ToString(), dr["CONTENT"].ToString());
                }
                else
                {
                    _boxQty = _drpWh.GetBoxQty(false, dr["PRD_NO"].ToString(), dr["WH"].ToString(), dr["CONTENT"].ToString());
                }
                if (isModify)
                {
                    //判断是否是修改状态
                    if (dr.RowState == System.Data.DataRowState.Modified)
                    {
                        if ((dr["PRD_NO", DataRowVersion.Original].ToString() == dr["PRD_NO", DataRowVersion.Current].ToString()) && (dr["CONTENT", DataRowVersion.Original].ToString() == dr["CONTENT", DataRowVersion.Current].ToString()) && (dr["WH", DataRowVersion.Original].ToString() == dr["WH", DataRowVersion.Current].ToString()))
                        {
                            //在修改的情况需要判断当前单据的原数量
                            //如果有就减掉它
                            if (dr["QTY", System.Data.DataRowVersion.Original] != System.DBNull.Value)
                                _originalBoxQty = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                            _boxQty += _originalBoxQty;
                        }
                    }
                }

                if (Convert.ToDecimal(dr["QTY"]) > _boxQty)
                {
                    if (_chk_qty_way == "1")
                    {
                        throw new Exception("RCID=INV.HINT.FACTPRDT1,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["WH"].ToString() + ",PARAM=" + dr["CONTENT"].ToString());
                    }
                    else
                    {
                        throw new Exception("RCID=INV.HINT.UNFACTPRDT1,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["WH"].ToString() + ",PARAM=" + dr["CONTENT"].ToString());
                    }
                }
            }
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
            DataTable _dtMf = ds.Tables["MF_POS"];
            DataTable _dtTf = ds.Tables["TF_POS"];
            string _mOsId = "";
            string errorMsg = "";
            bool _bCanModify = true;
            if (_dtMf.Rows.Count > 0)
            {
                _mOsId = _dtMf.Rows[0]["OS_ID"].ToString();
                //if (_dtMf.Rows[0]["BIL_TYPE"].ToString() != "FX")
                //{
                //    _bCanModify = false;
                //    errorMsg += "COMMON.HINT.NOFX";//不是分销单据，不能修改
                //}
                // 出库结案的单据不能修改删除lzj 200903
                if (CaseInsensitiveComparer.Default.Compare("T", _dtMf.Rows[0]["RK_CLS_ID"].ToString()) == 0)
                    _bCanModify = false;
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["AMTN_CLS"].ToString()) && Convert.ToDecimal(_dtMf.Rows[0]["AMTN_CLS"]) > 0)
                {
                    _bCanModify = false;
                }
                if (_mOsId == "SO")
                {
                    //判断是否由积分兑换操作生成
                    if (_bCanModify && _dtMf.Rows[0]["BIL_ID"].ToString() == "CH")
                    {
                        _bCanModify = false;
                        if (ds.ExtendedProperties["ISROLLBACK"] != null && ds.ExtendedProperties["ISROLLBACK"].ToString() == "T")
                            _bCanModify = true;
                    }
                }
                //判断关帐日
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["OS_DD"]), _dtMf.Rows[0]["PO_DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.ACCCLOSE";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.ACCCLOSE");
                }
                //判断审核流程
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_dtMf.Rows[0]["OS_ID"].ToString(), _dtMf.Rows[0]["OS_NO"].ToString()))
                    {
                        _bCanModify = false;
                        errorMsg += "COMMON.HINT.INTOAUT";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
                //判断是否结案
                //if (_bCanModify)
                {
                    if (_dtMf.Rows[0]["CLS_ID"].ToString() == "T")
                    {
                        _bCanModify = false;
                        errorMsg += "DRPYI.ISCLOSE";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");
                    }
                }
                //判断是否锁单
                if ( !String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
                //判断是否走紧急放行
                if (_mOsId == "SO")
                {
                    //if (_bCanModify)
                    {
                        if (_dtMf.Rows[0]["CHK_FX"].ToString() == "T" && _dtMf.Rows[0]["HAS_FX"].ToString() == "T")
                        {
                            _bCanModify = false;
                            errorMsg += "INV.MF_POS.ISFX";
                            //Common.SetCanModifyRem(ds, "RCID=INV.MF_POS.ISFX");
                        }
                    }
                }
                if (_mOsId == "SO")
                {
                    //判断是否转制令
                    //if (_bCanModify)
                    {
                        MRPMO _mo = new MRPMO();
                        SunlikeDataSet _dsMo = _mo.GetData(" AND SO_NO='" + _dtMf.Rows[0]["OS_NO"].ToString() + "'");
                        if (_dsMo.Tables["MF_MO"].Rows.Count > 0)
                        {
                            _bCanModify = false;
                            errorMsg += "MTN.HINT.MO_IN";//已经出货
                            //Common.SetCanModifyRem(ds, "RCID=MTN.HINT.MO_IN");
                        }
                    }
                    decimal _qtySo = 0;
                    //判断配送转入
                    //if (_bCanModify)
                    {

                        decimal _qtyIc = 0;
                        bool _isCompleteIc = true;
                        DataRow[] _aryDr = _dtTf.Select();
                        if (_aryDr.Length == 0)//当只修改表头资料时，表身的信息不会带到changeDS中，故应判断没有表身的情况
                            _isCompleteIc = false;
                        for (int i = 0; i < _aryDr.Length; i++)
                        {
                            _qtySo = 0;
                            _qtyIc = 0;
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY"].ToString()))
                            {
                                _qtySo = Convert.ToDecimal(_aryDr[i]["QTY"]);
                            }
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY_IC"].ToString()))
                            {
                                _qtyIc = Convert.ToDecimal(_aryDr[i]["QTY_IC"]);
                            }
                            if (_qtySo > _qtyIc || _qtyIc == 0)
                            {
                                _isCompleteIc = false;
                                break;
                            }
                        }
                        if (_isCompleteIc)
                        {
                            _bCanModify = false;
                            errorMsg += "DRPSA.PS_IN";//已经配送
                            //Common.SetCanModifyRem(ds, "RCID=INV.DRPSA.PS_IN");
                        }
                    }
                    //判断出入库转入
                    //if (_bCanModify)
                    {
                        _qtySo = 0;
                        decimal _qtyRk = 0;
                        bool _isCompleteRk = true;
                        DataRow[] _aryDr = _dtTf.Select();
                        if (_aryDr.Length == 0)//当只修改表头资料时，表身的信息不会带到changeDS中，故应判断没有表身的情况
                            _isCompleteRk = false;
                        for (int i = 0; i < _aryDr.Length; i++)
                        {
                            _qtySo = 0;
                            _qtyRk = 0;
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY"].ToString()))
                            {
                                _qtySo = Convert.ToDecimal(_aryDr[i]["QTY"]);
                            }
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY_RK"].ToString()))
                            {
                                _qtyRk = Convert.ToDecimal(_aryDr[i]["QTY_RK"]);
                            }
                            if (_qtySo > _qtyRk || _qtyRk == 0)
                            {
                                _isCompleteRk = false;
                                break;
                            }
                        }
                        if (_isCompleteRk)
                        {
                            _bCanModify = false;
                            errorMsg += "DRPSA.RK_IN";//已经出货
                            //Common.SetCanModifyRem(ds, "RCID=INV.DRPSO.RK_IN");
                        }
                    }
                    //是否已销货
                    //if (_bCanModify)
                    {
                        _qtySo = 0;
                        decimal _qtyPs = 0;
                        bool _isCompletePs = true;
                        DataRow[] _aryDr = _dtTf.Select();
                        if (_aryDr.Length == 0)//当只修改表头资料时，表身的信息不会带到changeDS中，故应判断没有表身的情况
                            _isCompletePs = false;
                        for (int i = 0; i < _aryDr.Length; i++)
                        {
                            _qtySo = 0;
                            _qtyPs = 0;
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY"].ToString()))
                            {
                                _qtySo = Convert.ToDecimal(_aryDr[i]["QTY"]);
                            }
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY_PS"].ToString()))
                            {
                                _qtyPs = Convert.ToDecimal(_aryDr[i]["QTY_PS"]);
                            }
                            if (_qtySo > _qtyPs || _qtyPs == 0)
                            {
                                _isCompletePs = false;
                                break;
                            }
                        }
                        if (_isCompletePs)
                        {
                            _bCanModify = false;
                            errorMsg += "DRPSA.PS_IN";//已经出货
                            //Common.SetCanModifyRem(ds, "已经出货不能修改");
                        }
                    }
                    //判断受订退回
                    //if (_bCanModify)
                    {
                        _qtySo = 0;
                        decimal _qtyPre = 0;
                        bool _isCompletePre = true;
                        DataRow[] _aryDr = _dtTf.Select();
                        for (int i = 0; i < _aryDr.Length; i++)
                        {
                            _qtySo = 0;
                            _qtyPre = 0;
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY"].ToString()))
                            {
                                _qtySo = Convert.ToDecimal(_aryDr[i]["QTY"]);
                            }
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY_PRE"].ToString()))
                            {
                                _qtyPre = Convert.ToDecimal(_aryDr[i]["QTY_PRE"]);
                            }
                            if (_qtySo > _qtyPre || _qtyPre == 0)
                            {
                                _isCompletePre = false;
                                break;
                            }
                        }
                        if (_isCompletePre)
                        {
                            _bCanModify = false;
                            errorMsg += "DRPSA.PRE_IN";//已经出货
                            //Common.SetCanModifyRem(ds, "已有退回量不能修改");
                        }
                    }
                    //已转报销单
                    //if (_bCanModify)
                    {
                        MONBX _bx = new MONBX();
                        SunlikeDataSet _dsBx = _bx.GetBxBody(_dtMf.Rows[0]["OS_ID"].ToString(), _dtMf.Rows[0]["OS_NO"].ToString());
                        if (_dsBx.Tables["TF_BX"].Rows.Count > 0)
                        {
                            ds.ExtendedProperties["DEL"] = "F";
                            errorMsg += "MON.HINT.EXIST_BX";//已转报销单
                            //Common.SetCanModifyRem(ds, "MON.HINT.EXIST_BX");
                        }
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return errorMsg;
        }
        #endregion

        #region 根据受订数量和配送数量判断是否结案
        /// <summary>
        /// 根据受订数量和配送数量判断是否结案：受订数量小于配送数量+出库量+销售量+受订退回量=不结案，受订数量>=配送数量+出库量+销售量+受订退回量=结案
        /// 没有回写单据数量也没有回写手工结案标记
        /// </summary>
        /// <param name="os_no"></param>
        public void UpdateClsId(string os_no)
        {
            DbDrpSO SunlikeDbDrpSO = new DbDrpSO(Comp.Conn_DB);
            SunlikeDbDrpSO.UpdateClsId("SO", os_no);
        }
        #endregion
        #endregion

        #region 取得税率及未税本位币
        /// <summary>
        /// 取得税率及未税本位币
        /// </summary>
        /// <param name="tax_type"></param>
        /// <param name="amt"></param>
        /// <param name="tax_id"></param>
        /// <param name="amtn"></param>
        /// <param name="tax"></param>
        private void GetTaxAndAmtn(string tax_type, decimal amt, int tax_id, out decimal amtn, out decimal tax)
        {
            decimal _amtn = 0;
            decimal _tax = 0;
            if (tax_type == "1")
            {
                _amtn = amt;
            }
            else if (tax_type == "2")
            {
                _amtn = (Convert.ToDecimal(amt) * 100) / (100 + tax_id);
                _tax = amt - _amtn;
            }
            else if (tax_type == "3")
            {
                _amtn = amt;
                _tax = amt * tax_id / 100;
            }
            else
            {
                _amtn = amt;
            }
            amtn = _amtn;
            tax = _tax;
        }
        #endregion

        #region 修改预收付款
        /// <summary>
        /// 修改预收付款
        /// </summary>
        /// <param name="dr"></param>
        private void UpdateMon(DataRow dr)
        {
            Bills _bills = new Bills();
            //生成预收款单据
            MonStruct _mon = new MonStruct();
            _mon.RpId = "1";
            _mon.RpNo = dr["RP_NO"].ToString();
            if (!string.IsNullOrEmpty(dr["OS_DD"].ToString()))
                _mon.RpDd = Convert.ToDateTime(dr["OS_DD"].ToString());
            _mon.BilId = dr["OS_ID"].ToString();
            _mon.BilNo = dr["OS_NO"].ToString();
            _mon.Usr = dr["USR"].ToString();
            _mon.ChkMan = dr["CHK_MAN"].ToString();
            if (!string.IsNullOrEmpty(dr["CLS_DATE"].ToString()))
                _mon.ClsDate = Convert.ToDateTime(dr["CLS_DATE"].ToString());
            //_mon.VohId = dr["VOH_ID"].ToString();
            //_mon.VohNo = dr["VOH_NO"].ToString();
            _mon.MobId = dr["MOB_ID"].ToString();
            _mon.UsrNo = dr["SAL_NO"].ToString();
            _mon.Dep = dr["PO_DEP"].ToString();
            _mon.CurId = dr["CUR_ID"].ToString();
            if (string.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                _mon.ExcRto = 1;
            else
                _mon.ExcRto = Convert.ToDecimal(dr["EXC_RTO"].ToString());
            _mon.BaccNo = dr["BACC_NO"].ToString();
            _mon.CaccNo = dr["CACC_NO"].ToString();
            #region amt_bb,amtn_bb,amtn_bc,amt_bc,amt_chk,amtn_chk
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
            _mon.IrpId = "T";
            _mon.CusNo = dr["CUS_NO"].ToString();
            if (string.IsNullOrEmpty(dr["TAX"].ToString()))
            {
                _mon.AmtOther = 0;
                _mon.AmtnOther = 0;
                _mon.AddMon3 = false;
            }
            else
            {
                _mon.AmtnOther = (-1) * Convert.ToDecimal(dr["TAX"].ToString());
                CompInfo _compInfo = Comp.GetCompInfo("");
                int _poiAmt = _compInfo.DecimalDigitsInfo.System.POI_AMT;
                if (_mon.ExcRto != 1)
                    _mon.AmtOther = (-1) * Math.Round(Convert.ToDecimal(dr["TAX"].ToString()) / _mon.ExcRto, _poiAmt);
                else
                    _mon.AmtOther = 0;

                //该属性为TRUE生成TF_MON3的表身
                _mon.AddMon3 = true;
            }

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
                _mon.CrecardNo = dr["CRECARD_NO"].ToString();

                //该属性为TRUE生成TF_MON4的表身
                _mon.AddMon4 = true;
            }
            _mon.Amtn = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
            string _rpNo = _bills.AddRcvPay(_mon);
            dr["RP_NO"] = _rpNo;
        }
        #endregion

        #region IAuditing Members
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
            try
            {
                DRPYHut _yhut = new DRPYHut();
                DrpSO _drpSo = new DrpSO();
                DataSet _dsSo = _drpSo.GetData("", pBB_ID, pBB_NO, false, false);
                foreach (DataRow dr in _dsSo.Tables["TF_POS"].Rows)
                {
                    if (!(dr["OTH_ITM"] is DBNull))
                        _yhut.UpdateQtySoUnSh(pCHK_MAN,dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(), Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]) * -1);
                }
                return "";
            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }
        /// <summary>
        /// Approve
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _errmsg = "";
            try
            {
                //DbDrpSO _dbDRPSO = new DbDrpSO(Comp.Conn_DB);
                SunlikeDataSet _drpSODS = this.GetData(null, bil_id, bil_no, false, false);//SelectDrpSO(null,bil_no,false);				

                DataTable _MF_POSTable = _drpSODS.Tables["MF_POS"];
                DataTable _TF_POSTable = _drpSODS.Tables["TF_POS"];

                string _chkType = Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString();//选择可用库存还是实际库存
                bool _bType = false;
                if (_chkType == "1")
                {
                    _bType = true;
                }

                if (_MF_POSTable.Rows[0]["YH_NO"].ToString().Length > 0)
                {
                    #region 要货单转入
                    //----by db------
                    //可用库存不足是否接受  
                    string _lowWhQty = Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString();
                    bool _lowWhQtyAccept = true;
                    if (_lowWhQty == "F")
                        _lowWhQtyAccept = false;
                    //判断是否包下属储位
                    bool _isUnde = true;
                    if (Comp.DRP_Prop["DRPYH_WH_VIEWUNDER"].ToString() == "F")
                    {
                        _isUnde = false;
                    }
                    //---------------
                    try
                    {

                        this.CheckWhUseful(_lowWhQtyAccept, _bType, _isUnde, _drpSODS);//判断可用库存或实际库存
                    }
                    catch (Exception _ex)
                    {
                        return _ex.Message;
                    }

                    #region 回写要货单受定量
                    if (_MF_POSTable.Rows.Count > 0)
                    {
                        if (_MF_POSTable.Rows[0]["YH_NO"] != DBNull.Value && _MF_POSTable.Rows[0]["YH_NO"].ToString() != null)
                        {
                            DataTable _dtTf = _drpSODS.Tables["TF_POS"];
                            //重整DataTable,避免由于折分引起的回写不正确
                            DataTable _dtTfCopy = _dtTf.Copy();
                            foreach (DataRow _dr in _dtTf.Rows)
                            {
                                if (_dr["OTH_ITM"].ToString().Length == 0)
                                    continue;
                                DataRow[] _dra = _dtTfCopy.Select("BIL_ID = '" + _dr["BIL_ID"].ToString() + "' AND QT_NO = '" + _dr["QT_NO"].ToString() + "' AND OTH_ITM = '" + _dr["OTH_ITM"].ToString() + "'");
                                if (_dra.Length > 1)
                                {
                                    int _sumQty = 0;
                                    foreach (DataRow dr in _dra)
                                    {
                                        _sumQty += Convert.ToInt32(dr["QTY"]);
                                    }
                                    _dra[0]["QTY"] = _sumQty;
                                    foreach (DataRow dr in _dra)
                                    {
                                        if (dr["ITM"].ToString() != _dra[0]["ITM"].ToString())
                                        {
                                            dr.Delete();
                                        }
                                    }
                                    _dtTfCopy.AcceptChanges();
                                }
                            }
                            foreach (DataRow dr in _dtTfCopy.Rows)
                            {
                                if ((dr["BIL_ID"].ToString() == "YH"
                                    || dr["BIL_ID"].ToString() == "YN"
                                    || dr["BIL_ID"].ToString() == "YC")
                                    && (dr["OTH_NO"].ToString() != String.Empty || dr["QT_NO"].ToString() != String.Empty)
                                    && dr["QTY"] != System.DBNull.Value)
                                {
                                    _yh.UpdateQtySo(dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(), Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]));
                                    _yh.UpdateQtySoUnSh(chk_man,dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(), Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]) * -1);
                                }
                            }
                        }
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    #region 直接登打
                    Sunlike.Business.UserProperty _usrProp = new UserProperty();
                    string _audiLoginUsr = _MF_POSTable.Rows[0]["USR"].ToString();
                    string _strPrdt1 = _usrProp.GetData(_audiLoginUsr, "DRPSO", "PRDT1");
                    if (_strPrdt1 == "0")//管制
                    {
                        string _strChkUnder = _usrProp.GetData(_audiLoginUsr, "DRP" + bil_id, "CHK_UNDER");
                        bool _soUnder = false;
                        if (_strChkUnder == "T")
                            _soUnder = true;

                        try
                        {
                            this.CheckWhUseful(false, _bType, _soUnder, _drpSODS);//判断可用库存或实际库存
                        }
                        catch (Exception _ex)
                        {
                            return _ex.Message;
                        }
                    }
                    #endregion

                }
                #region 客户信用额度判断

                if (bil_id == "SO")
                {
                    Cust _cust = new Cust();
                    if (_cust.GetCrdId(_MF_POSTable.Rows[0]["CUS_NO"].ToString()) == "2")
                    {
                        decimal _totalLimNr = 0;
                        //增加客户信用额度
                        _totalLimNr += _cust.GetLim_NR(_MF_POSTable.Rows[0]["CUS_NO"].ToString());
                        //减掉受订单的总金额
                        for (int i = 0; i < _TF_POSTable.Rows.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(_TF_POSTable.Rows[i]["AMTN"].ToString()))
                                _totalLimNr -= Convert.ToDecimal(_TF_POSTable.Rows[i]["AMTN"]);
                            if (!String.IsNullOrEmpty(_TF_POSTable.Rows[i]["TAX"].ToString()))
                                _totalLimNr -= Convert.ToDecimal(_TF_POSTable.Rows[i]["TAX"]);
                        }
                        if (_totalLimNr < 0)
                        {
                            throw new SunlikeException("RCID=SYS.CUST.LIM_NR_ALERT");//信用额度不足。
                        }
                    }
                }
                #endregion

                if (_MF_POSTable.Rows.Count > 0)
                {
                    DataTable _dtHead = _MF_POSTable.Clone();
                    DataRow _drHead = _dtHead.NewRow();
                    _drHead.ItemArray = _MF_POSTable.Rows[0].ItemArray;
                    _dtHead.Rows.Add(_drHead);
                    _drHead.AcceptChanges();
                    _drHead["CHK_MAN"] = chk_man;
                    UpdateMonCbac(_drHead, true);
                    UpdateMonCbac(_drHead, false);
                }

                DRPME _drpMe = new DRPME();
                for (int i = 0; i < _TF_POSTable.Rows.Count; i++)
                {
                    #region 修改库存
                    UpdateTf(bil_id, _TF_POSTable.Rows[i], StatementType.Insert, false, true);
                    #endregion

                    #region 修改已退回量
                    if (string.Compare("SR", bil_id) == 0)
                    {
                        UpdateQtyPre(_TF_POSTable.Rows[i], StatementType.Insert);
                    }
                    #endregion

                    #region 回写已申请量和已收件量
                    if (_TF_POSTable.Rows[i]["BIL_ID"].ToString() == "RV")
                    {
                        UpdateRcv(_TF_POSTable.Rows[i], true);
                    }
                    else if (_TF_POSTable.Rows[i]["BIL_ID"].ToString() == "MA")
                    {
                        UpdateMa(_TF_POSTable.Rows[i], true);
                    }
                    #endregion

                    #region 维修卡记录
                    UpdateWCH(_TF_POSTable.Rows[i], false, true);
                    #endregion

                    #region 回写营销费用申请已交量

                    if (_TF_POSTable.Rows[i]["BIL_ID"].ToString() == "EA")
                    {
                        this.UpdateDrpMe(_TF_POSTable.Rows[i], true, false);
                    }

                    #endregion

                    #region 回写采购单已采购量
                    if (string.Compare("SO", bil_id) == 0)
                    {
                        if (string.Compare("PO", _TF_POSTable.Rows[i]["BIL_ID"].ToString()) == 0)
                        {
                            string _tmpChkMan = _MF_POSTable.Rows[0]["CHK_MAN"].ToString();
                            _MF_POSTable.Rows[0]["CHK_MAN"] = chk_man;
                            this.UpdateQtyPo(_TF_POSTable.Rows[i], true, true);
                            _MF_POSTable.Rows[0]["CHK_MAN"] = _tmpChkMan;
                        }
                    }
                    #endregion
                }
                if (_drpSODS.Tables.Contains("TF_POS1"))
                {
                    #region 修改箱库存
                    DataTable _TF_POS1Table = _drpSODS.Tables["TF_POS1"];
                    for (int i = 0; i < _TF_POS1Table.Rows.Count; i++)
                    {
                        UpdateTfBox(bil_id, _TF_POS1Table.Rows[i], StatementType.Insert, false, true);
                    }
                    #endregion
                }


                #region  修改应收款月余额档
                UpdateSarp(_drpSODS, false, false);
                #endregion

                CloseTheSO(bil_id, bil_no, chk_man, Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat)));
                //预收付款

                decimal _amtn = 0;
                if (!String.IsNullOrEmpty(_MF_POSTable.Rows[0]["AMTN_INT"].ToString()))
                {
                    _amtn = Convert.ToDecimal(_MF_POSTable.Rows[0]["AMTN_INT"]);
                }
                if (_amtn != 0)
                {
                    //订金含税否；税率取“营业人资料”里的本业税率，如果为零取5
                    _amtn = Convert.ToDecimal(_MF_POSTable.Rows[0]["AMTN_INT"]);
                    if (_MF_POSTable.Rows[0]["TAX_ID"].ToString() != "1" && _MF_POSTable.Rows[0]["HS_ID"].ToString() == "T")
                    {
                        CompInfo _compInfo = Comp.GetCompInfo("");
                        decimal _psTax = _compInfo.SystemInfo.PS1_TAX;
                        //POI_WBA外位币小数位数；POI_AMT本位币小数位数
                        int _poiAmt = _compInfo.DecimalDigitsInfo.System.POI_AMT;
                        if (_psTax == 0)
                            _psTax = 5;
                        decimal _amtnNet = _amtn / (1 + _psTax / 100);
                        _amtnNet = Math.Round(_amtnNet, _poiAmt);
                        _MF_POSTable.Rows[0]["AMTN_NET"] = _amtnNet;
                        _MF_POSTable.Rows[0]["TAX"] = (_amtn - _amtnNet);
                    }
                    else
                    {
                        _MF_POSTable.Rows[0]["AMTN_NET"] = _MF_POSTable.Rows[0]["AMTN_INT"];
                        _MF_POSTable.Rows[0]["TAX"] = System.DBNull.Value;
                    }
                    UpdateMon(_MF_POSTable.Rows[0]);
                }
            }
            catch (Exception _ex)
            {
                //_errmsg = _ex.Message.ToString();
                throw _ex;
            }
            return _errmsg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            return RollBack(bil_id, bil_no, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="isUpdateHead"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no, bool isUpdateHead)
        {
            string _error = "";
            bool _hasQtyIc = false;
            bool _hasQtyRk = false;
            bool _hasQtyPs = false;
            bool _hasQtyPre = false;
            try
            {
                SunlikeDataSet _drpSODS = this.GetData(null, bil_id, bil_no, false, false);//SelectDrpSO(null,bil_no,false);
                _drpSODS.ExtendedProperties["ISROLLBACK"] = "T";
                string _errorMsg = this.SetCanModify(_drpSODS, false);
                if (_errorMsg != "COMMON.HINT.NOFX" && _drpSODS.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                {
                    if (_errorMsg.Length > 0)
                    {
                        return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;RCID=" + _errorMsg;
                    }
                    else
                    {
                        return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";
                    }
                }

                #region 客户信用额度判断

                if (bil_id == "SR")
                {
                    if (_drpSODS.Tables.Contains("MF_POS") && _drpSODS.Tables.Contains("TF_POS"))
                    {
                        DataTable _MF_POSTable = _drpSODS.Tables["MF_POS"];
                        DataTable _TF_POSTable = _drpSODS.Tables["TF_POS"];
                        Cust _cust = new Cust();
                        if (_cust.GetCrdId(_MF_POSTable.Rows[0]["CUS_NO"].ToString()) == "2")
                        {
                            decimal _totalLimNr = 0;
                            //增加客户信用额度
                            _totalLimNr += _cust.GetLim_NR(_MF_POSTable.Rows[0]["CUS_NO"].ToString());
                            //减掉受订单的总金额
                            for (int i = 0; i < _TF_POSTable.Rows.Count; i++)
                            {
                                if (!String.IsNullOrEmpty(_TF_POSTable.Rows[i]["AMTN"].ToString()))
                                    _totalLimNr -= Convert.ToDecimal(_TF_POSTable.Rows[i]["AMTN"]);
                                if (!String.IsNullOrEmpty(_TF_POSTable.Rows[i]["TAX"].ToString()))
                                    _totalLimNr -= Convert.ToDecimal(_TF_POSTable.Rows[i]["TAX"]);
                            }
                            if (_totalLimNr < 0)
                            {
                                throw new SunlikeException("RCID=SYS.CUST.LIM_NR_ALERT");//信用额度不足。
                            }
                        }
                    }
                }
                #endregion

                if (_drpSODS.Tables.Contains("MF_POS"))
                {
                    DataTable _MF_POSTable = _drpSODS.Tables["MF_POS"];
                    DataTable _dtHeadDel = _MF_POSTable.Clone();
                    DataRow _drHeadDel = _dtHeadDel.NewRow();
                    _drHeadDel.ItemArray = _MF_POSTable.Rows[0].ItemArray;
                    _dtHeadDel.Rows.Add(_drHeadDel);
                    _drHeadDel.AcceptChanges();
                    _drHeadDel["CHK_MAN"] = System.DBNull.Value;
                    UpdateMonCbac(_drHeadDel, false);
                    UpdateMonCbac(_drHeadDel, true);

                }

                if (_drpSODS.Tables.Contains("TF_POS"))
                {
                    DRPME _drpMe = new DRPME();
                    DataTable _TF_POSTable = _drpSODS.Tables["TF_POS"];
                    for (int i = 0; i < _TF_POSTable.Rows.Count; i++)
                    {
                        decimal _qty_ic = 0;
                        decimal _qtyrk = 0;
                        decimal _qtyPre = 0;
                        decimal _qtyPs = 0;
                        #region 受订单是否有回写及修改库存
                        UpdateTf(bil_id, _TF_POSTable.Rows[i], StatementType.Delete, false, true);
                        #endregion
                        DataTable _dtDel = _TF_POSTable.Clone();
                        DataRow _drDel = _dtDel.NewRow();
                        _drDel.ItemArray = _TF_POSTable.Rows[i].ItemArray;
                        _dtDel.Rows.Add(_drDel);
                        _dtDel.AcceptChanges();
                        _drDel.Delete();
                        #region 修改已退回量
                        if (string.Compare("SR", bil_id) == 0)
                        {

                            UpdateQtyPre(_drDel, StatementType.Delete);
                        }
                        #endregion

                        #region 回写已申请量和已收件量
                        if (_TF_POSTable.Rows[i]["BIL_ID"].ToString() == "RV")
                        {
                            UpdateRcv(_drDel, false);
                        }
                        else if (_TF_POSTable.Rows[i]["BIL_ID"].ToString() == "MA")
                        {
                            UpdateMa(_drDel, false);
                        }
                        #endregion


                        //是否有配送量
                        if (!(_TF_POSTable.Rows[i]["QTY_IC"] is System.DBNull))
                        {
                            _qty_ic = Convert.ToDecimal(_TF_POSTable.Rows[i]["QTY_IC"]);
                        }
                        if (_qty_ic > 0)
                        {
                            _hasQtyIc = true;
                        }
                        //是否有已出库量
                        if (!(_TF_POSTable.Rows[i]["QTY_RK"] is System.DBNull))
                        {
                            _qtyrk = Convert.ToDecimal(_TF_POSTable.Rows[i]["QTY_RK"]);
                        }
                        if (!(_TF_POSTable.Rows[i]["QTY_RK_UNSH"] is System.DBNull))
                        {
                            _qtyrk += Convert.ToDecimal(_TF_POSTable.Rows[i]["QTY_RK_UNSH"]);
                        }
                        if (_qtyrk > 0)
                        {
                            _hasQtyRk = true;
                        }
                        //是否有销货量
                        if (!(_TF_POSTable.Rows[i]["QTY_PS"] is System.DBNull))
                        {
                            _qtyPs = Convert.ToDecimal(_TF_POSTable.Rows[i]["QTY_PS"]);
                        }
                        if (_qtyPs > 0)
                        {
                            _hasQtyPs = true;
                        }
                        //是否有受订退回量
                        if (!(_TF_POSTable.Rows[i]["QTY_PRE"] is System.DBNull))
                        {
                            _qtyPre = Convert.ToDecimal(_TF_POSTable.Rows[i]["QTY_PRE"]);
                        }
                        if (_qtyPre > 0)
                        {
                            _hasQtyPre = true;
                        }
                        #region 维修卡记录
                        UpdateWCH(_TF_POSTable.Rows[i], true, true);
                        #endregion

                        #region 回写营销费用申请已交量

                        if (_TF_POSTable.Rows[i]["BIL_ID"].ToString() == "EA")
                        {
                            this.UpdateDrpMe(_TF_POSTable.Rows[i], false, true);
                        }

                        #endregion

                        #region 回写采购单已采购量
                        if (string.Compare("SO", bil_id) == 0)
                        {
                            if (string.Compare("PO", _drDel["BIL_ID", DataRowVersion.Original].ToString()) == 0)
                            {
                                this.UpdateQtyPo(_TF_POSTable.Rows[i], false, true);
                            }
                        }
                        #endregion
                    }
                }
                if (_hasQtyIc)
                {
                    throw new SunlikeException(/*已有配送，不能反审核！*/ "RCID=INV.HINT.UNROLLBACK");
                }
                if (_hasQtyRk)
                {
                    throw new SunlikeException(/*已有出库，不能反审核！*/ "RCID=INV.HINT.UNROLLBACKRK");
                }
                if (_hasQtyPs)
                {
                    throw new SunlikeException(/*已有销货，不能反审核！*/ "RCID=INV.HINT.UNROLLBACKPS");
                }
                if (_hasQtyPre)
                {
                    throw new SunlikeException(/*已有退回，不能反审核！*/ "RCID=INV.HINT.UNROLLBACKPRE");
                }

                #region 修改箱库存
                if (_drpSODS.Tables.Contains("TF_POS1"))
                {
                    DataTable _TF_POS1Table = _drpSODS.Tables["TF_POS1"];
                    for (int i = 0; i < _TF_POS1Table.Rows.Count; i++)
                    {
                        UpdateTfBox(bil_id, _TF_POS1Table.Rows[i], StatementType.Delete, false, true);
                    }
                }
                #endregion

                #region  修改应收款月余额档

                UpdateSarp(_drpSODS, false, true);
                #endregion

                #region 修改应收款月余额档
                if (isUpdateHead)
                {
                    RollbackTheSO(bil_id, bil_no);
                    string _rpNo = _drpSODS.Tables["MF_POS"].Rows[0]["RP_NO"].ToString();
                    if (!String.IsNullOrEmpty(_rpNo))
                    {
                        Bills _bills = new Bills();
                        //删除预收付款
                        _bills.DelRcvPay("1", _rpNo);
                    }
                }
                #endregion

                #region 修改要货单结案标记 && 回写要货单受定量
                if (_drpSODS.Tables.Contains("MF_POS"))
                {
                    DataTable _MF_POSTable = _drpSODS.Tables["MF_POS"];
                    if (_MF_POSTable.Rows.Count > 0)
                    {
                        string _yh_no = _MF_POSTable.Rows[0]["YH_NO"].ToString();
                        if (!String.IsNullOrEmpty(_yh_no))
                        {
                            DRPYH _yh = new DRPYH();
                            DRPYHut _yhut = new DRPYHut();
                            //回写要货单受定量
                            DataTable _tfPos = null;
                            if (_drpSODS.Tables.Contains("TF_POS"))
                            {
                                _tfPos = _drpSODS.Tables["TF_POS"];
                            }
                            foreach (DataRow dr in _tfPos.Rows)
                            {
                                if ((dr["BIL_ID"].ToString() == "YH"
                                    || dr["BIL_ID"].ToString() == "YN"
                                    || dr["BIL_ID"].ToString() == "YC")
                                    && (dr["OTH_NO"].ToString() != String.Empty || dr["QT_NO"].ToString() != String.Empty)
                                    && dr["QTY"] != System.DBNull.Value)
                                {
                                    _yhut.UpdateQtySo(dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(), Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]) * -1);
                                    _yhut.UpdateQtySoUnSh("CHK_MAN",dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(), Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]));
                                }
                            }
                            _yh.UnCheckForSO(_yh_no);//修改要货单结案标记
                        }
                    }
                }
                #endregion

            }
            catch (Exception _ex)
            {
                //_error = ex.Message.ToString();
                throw _ex;
            }
            return _error;
        }
        /// <summary>
        /// DeleteDrpSO
        /// </summary>
        /// <param name="yh_id"></param>
        /// <param name="yh_no"></param>
        /// <returns></returns>
        public string DeleteDrpSO(string yh_id, string yh_no)
        {
            DbDrpSO _dbDrpSO = new DbDrpSO(Comp.Conn_DB);
            DataTable _mf_posTable = _dbDrpSO.SelectDrpSOFromYH(yh_id, yh_no);
            string _os_id = "";
            string _os_no = "";
            if (_mf_posTable.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                _os_id = "SO";
                _os_no = _mf_posTable.Rows[0]["OS_NO"].ToString();
            }
            Auditing auditing = new Auditing();
            if (auditing.GetIfEnterAuditing(_os_id, _os_no))
            {
                //return _os_no + " have joined in Auditing";
                return _os_no + /* {0}已经在审核状态，无法修改/删除！*/"RCID=COMMON.HINT.ISAUDITING,PARAM=" + _os_no;
            }

            SunlikeDataSet _ds = this.GetData(null, "SO", _os_no, false, false);//this.SelectDrpSO(null,_os_no,false);
            if (_ds.Tables["MF_POS"].Rows.Count > 0)
            {
                if (_ds.Tables["MF_POS"].Rows[0]["CLS_ID"].ToString() == "T")
                {
                    throw new SunlikeException(/*string .Format("{0}已终审，不能删除! ",_os_no)*/"RCID=INV.HINT.AUITIDED,PARAM=" + _os_no);
                }
            }

            _ds.Tables["MF_POS"].Rows[0].Delete();
            this.UpdateData("DRPSO", _ds, true);
            if (_ds.HasErrors)
            {
                return "RCID=COMMON.HINT.DEL_FAILED";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 改变审核状态变成审核
        /// </summary>
        /// <param name="os_id">单据识别</param>
        /// <param name="os_no">受订单号</param>
        /// <param name="chk_man">审核人</param>
        /// <param name="cls_dd">终审日期</param>
        public void CloseTheSO(string os_id, string os_no, string chk_man, DateTime cls_dd)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            _so.UpdateDrpSO(os_id, os_no, chk_man, cls_dd);
        }
        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="os_id"></param>
        /// <param name="os_no"></param>
        private void RollbackTheSO(string os_id, string os_no)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            _so.RollbackDrpSO(os_id, os_no);
        }
        /// <summary>
        /// 检测审核人是否存在
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public bool checkChk_manIsExist(string bil_id, string bil_no)
        {
            bool _result = false;
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            DataTable _mf_posTable = _so.SelectDrpMf_Pos(bil_id, bil_no);
            if (_mf_posTable.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(_mf_posTable.Rows[0]["CHK_MAN"].ToString()) && !String.IsNullOrEmpty(_mf_posTable.Rows[0]["CLS_ID"].ToString()))
                {
                    _result = true;
                }
            }
            return _result;
        }

        #endregion

        #region IAuditingInfo Members
        /// <summary>
        /// 审核附属信息
        /// </summary>
        /// <param name="Bil_Id">单据类别</param>
        /// <param name="Bil_No">单据号码</param>
        /// <param name="RejectSH"></param>
        /// <returns></returns>
        public string GetBillInfo(string Bil_Id, string Bil_No, ref bool RejectSH)
        {
            StringBuilder _errStr = new StringBuilder("RCID=");
            RejectSH = false;
            string _chk_qty_way = Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString();
            DbDrpSO _drpSo = new DbDrpSO(Comp.Conn_DB);
            WH _wh = new WH();
            decimal _amtn = 0;
            //金额位数
            int _poi_amt = 2;
            //取得客户代号及金额位数
            DataTable _mf_posTable = this.GetDataMf(Bil_Id, Bil_No);
            Users _users = new Users();
            string _cus_no = "";
            string _soUserId = "";

            if (_mf_posTable.Rows.Count > 0)
            {
                _cus_no = _mf_posTable.Rows[0]["CUS_NO"].ToString();
                _soUserId = _mf_posTable.Rows[0]["USR"].ToString();
            }
            CompInfo _compInfo = Comp.GetCompInfo(_users.GetUserDepNo(_soUserId));

            DataTable _usrTable = _users.GetData(_cus_no);
            if (_usrTable.Rows.Count > 0)
                _poi_amt = _compInfo.DecimalDigitsInfo.System.POI_AMT;

            //判断库存信息
            #region 判断库存信息
            DataTable _soDt = _drpSo.GetTfPosForAuditing(Bil_Id, Bil_No);
            DataTable _soDt1 = _drpSo.GetTfPos1ForAuditing(Bil_Id, Bil_No);
            if (_soDt.Rows.Count > 0)
            {
                DataRow _soDr;
                for (int i = 0; i < _soDt.Rows.Count; i++)
                {
                    _soDr = _soDt.Rows[i];
                    //取得该受订单的总金额
                    if (!String.IsNullOrEmpty(_soDr["AMTN"].ToString()))
                        _amtn += Convert.ToDecimal(_soDr["AMTN"]);
                    if (!String.IsNullOrEmpty(_soDr["TAX"].ToString()))
                        _amtn += Convert.ToDecimal(_soDr["TAX"]);

                    if ((_soDr["BOX_ITM"] is System.DBNull) || (_soDr["BOX_ITM"].ToString() == "0"))//如果是单件才显示
                    {
                        DRPYHut _hut = new DRPYHut();
                        decimal _canUseQty = _hut.GetWhQty(_soDr["WH"].ToString(), _soDr["PRD_NO"].ToString(), _soDr["PRD_MARK"].ToString());
                        decimal _qty = Convert.ToDecimal(_soDr["QTY"]);
                        if (_canUseQty < _qty)
                        {
                            _errStr.Append("COMMON.HINT.PRD_NO;:;" + _soDr["PRD_NO"].ToString());
                            _errStr.Append("; ;CHKREASON.YHQTY;:;" + String.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", Convert.ToDecimal(_soDr["QTY"])));
                            if (_chk_qty_way == "1")
                            {
                                _errStr.Append("; ;INV.HINT.WH_QTY_ALERT1;:;");
                            }
                            else
                            {
                                _errStr.Append("; ;INV.HINT.WH_QTY_ALERT0;:;");
                            }
                            _errStr.Append("; ;UNKNOWN.CHKREASON.CANUSEQTY;:;" + _canUseQty.ToString() + "<br>; ;");
                        }
                    }
                }
            }
            if (_soDt1.Rows.Count > 0)
            {
                DataRow _soDr1;
                for (int i = 0; i < _soDt1.Rows.Count; i++)
                {
                    _soDr1 = _soDt1.Rows[i];
                    decimal _canUseQty1 = 0;
                    if (_chk_qty_way == "1")//得到实际库存
                    {
                        _canUseQty1 = _wh.GetBoxQty(true, _soDr1["PRD_NO"].ToString(), _soDr1["WH"].ToString(), _soDr1["CONTENT"].ToString());
                    }
                    else
                    {
                        _canUseQty1 = _wh.GetBoxQty(false, _soDr1["PRD_NO"].ToString(), _soDr1["WH"].ToString(), _soDr1["CONTENT"].ToString());
                    }
                    Int32 _qty1 = Convert.ToInt32(_soDr1["QTY"]);
                    if (_canUseQty1 < _qty1)
                    {
                        _errStr.Append("COMMON.HINT.PRD_NO;:;" + _soDr1["PRD_NO"].ToString());
                        _errStr.Append("; ;UNKNOWN.CHKREASON.YHQTY;:;" + _soDr1["QTY"].ToString());
                        if (_chk_qty_way == "1")
                        {
                            _errStr.Append("; ;INV.HINT.WH_QTY_ALERT1;:;");
                        }
                        else
                        {
                            _errStr.Append("; ;INV.HINT.WH_QTY_ALERT0;:;");
                        }
                        _errStr.Append("; ;CHKREASON.CANUSEQTY;:;" + _canUseQty1.ToString() + "<br>; ;");
                    }
                }
            }
            #endregion

            #region 客户信息额度
            Sunlike.Business.Cust _cust = new Cust();
            decimal _lim_NR = _cust.GetLim_NR(_cus_no);
            decimal _totalLim = 0;
            SunlikeDataSet _dsCust = new SunlikeDataSet();
            _dsCust.Merge(_cust.GetData(_cus_no));
            DataTable _dt = _dsCust.Tables["CUST"];
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["LIM_NR"].ToString().Length > 0)
                {
                    _totalLim = Convert.ToDecimal(_dt.Rows[0]["LIM_NR"]);
                }
            }
            _errStr.Append("<br>");
            _errStr.Append("CUST.CAN_USE_LIM_NR;");
            _errStr.Append(":" + String.Format("{0:F" + _poi_amt + "}", Convert.ToDecimal(_lim_NR - _amtn)) + ";");
            _errStr.Append("<br>");
            _errStr.Append("CUST.LIM_NR;");
            _errStr.Append(":" + String.Format("{0:F" + _poi_amt + "}", _totalLim) + ";");
            _errStr.Append("<br>");
            if ((_lim_NR - _amtn) < 0)
            {
                _errStr.Append("CUST.LIM_NR_ALERT;");
            }
            #endregion
            return _errStr.ToString();
        }

        #endregion

        #region	取受定单明细资料
        /// <summary>
        ///	取受定单明细资料
        /// </summary>
        /// <param name="Os_Id">单据别</param>
        /// <param name="Os_No">单号</param>
        /// <returns>取受定单明细资料</returns>
        public SunlikeDataSet GetTableOsForAudting(string Os_Id, string Os_No)
        {
            DbDrpSO _drpSo = new DbDrpSO(Comp.Conn_DB);
            return _drpSo.GetTableOsForAudting(Os_Id, Os_No);
        }
        #endregion
        //by db
        #region	取受定单明细资料
        /// <summary>
        ///	取受定单明细资料
        /// </summary>
        /// <param name="Os_Id">单据别</param>
        /// <param name="Os_No">单号</param>
        /// <returns>受定单明细资料</returns>
        public SunlikeDataSet GetTableOs(string Os_Id, string Os_No)
        {
            DbDrpSO _dbDrpSo = new DbDrpSO(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpSo.GetTableOs(Os_Id, Os_No);
            //取库存
            _ds.Tables["TF_POS"].Columns["WH_QTY"].ReadOnly = false;
            foreach (DataRow dr in _ds.Tables["TF_POS"].Rows)
            {
                string _prdNo = dr["PRD_NO"].ToString();
                string _prdMark = dr["PRD_MARK"].ToString();
                string _wh = dr["WH"].ToString();
                string _batNo = dr["BAT_NO"].ToString();
                WH _whClass = new WH();
                if (String.IsNullOrEmpty(_batNo))
                {
                    dr["WH_QTY"] = _whClass.GetSumQty(false, _prdNo, _prdMark, _wh, false, "", true);
                }
                else
                {
                    dr["WH_QTY"] = _whClass.GetQty(false, _prdNo, _prdMark, _wh, _batNo);
                }
            }
            return _ds;
        }
        #endregion

        #region 审核删除受定单明细
        /// <summary>
        /// 审核删除受定单明细
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="Os_Id"></param>
        /// <param name="Os_No"></param>
        /// <param name="Itm"></param>
        public void DelSo(string tableName, string Os_Id, string Os_No, int Itm)
        {
            DbDrpSO _dbDrpSo = new DbDrpSO(Comp.Conn_DB);
            try
            {
                _dbDrpSo.DelSo(tableName, Os_Id, Os_No, Itm);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion

        #region 修改受定明细(按双 For Auditing)
        /// <summary>
        /// 修改受定明细(按双 For Auditing)
        /// </summary>
        /// <param name="Os_No">要货单号</param>
        /// <param name="itm">表身货品ITM编号</param>
        /// <param name="wh">库位代号</param>
        /// <param name="qty">数量</param>
        /// <param name="est_DD">预交货日</param>
        /// <returns></returns>
        public string UpdateSoPro(string Os_No, string itm, string wh, decimal qty, DateTime est_DD)
        {
            DrpSO _drpSo = new DrpSO();
            StringBuilder _error = new StringBuilder();
            try
            {
                SunlikeDataSet _ds = _drpSo.GetTableOsForAudting("SO", Os_No);
                DataTable _dtT = _ds.Tables["TF_POS"];

                DataRow[] _drs = _dtT.Select("ITM = " + itm);
                foreach (DataRow _dr in _drs)
                {
                    _dr["WH"] = wh;
                    _dr["QTY"] = qty;
                    _dr["AMT"] = qty * Convert.ToDecimal(_dr["UP"]);
                    _dr["AMTN"] = qty * Convert.ToDecimal(_dr["UP"]);
                    _dr["EST_DD"] = est_DD;
                    //判断库存是否足够
                    _drpSo.CheckWhUsefulTf(_dr, false);
                }

                this.UpdateDataSet(_ds);

                DataTable _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
                if (_errorDt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in _errorDt.Rows)
                    {
                        _error.Append(_dr["TableName"].ToString() + ":" + _dr["REM"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _error.Append(ex.Message);
            }
            return _error.ToString();
        }
        #endregion

        #region 修改受定明细(按箱 For Auditing)
        /// <summary>
        /// 修改受定明细(按箱 For Auditing)
        /// </summary>
        /// <param name="Os_No">要货单号</param>
        /// <param name="itm">表身箱ITM编号</param>
        /// <param name="wh">库位代号</param>
        /// <param name="qty">数量</param>
        /// <param name="est_DD">预还日</param>
        /// <returns></returns>
        public string UpdateSoBox(string Os_No, string itm, string wh, decimal qty, DateTime est_DD)
        {
            DbDrpSO _dbDrpSo = new DbDrpSO(Comp.Conn_DB);
            StringBuilder _error = new StringBuilder();
            decimal _qty_Old = 0;
            decimal _box_Qty_Old = 0;
            try
            {
                SunlikeDataSet _ds = _dbDrpSo.GetTableOsForAudting("SO", Os_No);
                DataTable _dtT = _ds.Tables["TF_POS"];
                DataTable _dtT1 = _ds.Tables["TF_POS1"];
                int _box_Itm = 0;

                #region 箱内容
                DataRow[] _drsT1 = _dtT1.Select("ITM = " + itm);
                foreach (DataRow _drT1 in _drsT1)
                {
                    _box_Qty_Old = Convert.ToDecimal(_drT1["QTY"]);
                    _box_Itm = Convert.ToInt16(_drT1["KEY_ITM"]);

                    _drT1["WH"] = wh;
                    _drT1["QTY"] = qty;
                    _drT1["EST_DD"] = est_DD;
                    break;
                }
                #endregion

                DataRow[] _drsT = _dtT.Select("BOX_ITM = " + _box_Itm);

                #region 货品内容
                foreach (DataRow _drT in _drsT)
                {
                    _drT["WH"] = wh;
                    _qty_Old = Convert.ToDecimal(_drT["QTY"]);
                    int _prdt_Rate = Convert.ToInt32(Convert.ToInt32(_qty_Old) / Convert.ToInt32(_box_Qty_Old));
                    _drT["QTY"] = _prdt_Rate * qty;
                    _drT["AMTN"] = _prdt_Rate * qty * Convert.ToDecimal(_drT["UP"]);
                    _drT["EST_DD"] = est_DD;
                }
                #endregion

                this.UpdateDataSet(_ds);
                DataTable _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
                if (_errorDt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in _errorDt.Rows)
                    {
                        _error.Append(_dr["TableName"].ToString() + ":" + _dr["REM"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _error.Append(ex.Message);
            }
            return _error.ToString();
        }
        #endregion

        #region 重整箱数量
        /// <summary>
        /// 重整箱数量
        /// </summary>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        public void ResetBoxQty(DateTime BeginDate, DateTime EndDate)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            _so.ResetBoxQty(BeginDate.ToString(Comp.SQLDateFormat), EndDate.ToString(Comp.SQLDateFormat));
        }

        /// <summary>
        /// 重整箱数量
        /// </summary>
        /// <param name="OsNo">受订单号</param>
        /// <param name="PrdMark">产品特征</param>
        /// <param name="BoxItm">箱序号</param>
        /// <param name="Qty">数量</param>
        /// <param name="QtyIc">已配送数量</param>
        public void ResetBoxQty(string OsNo, string PrdMark, int BoxItm, decimal Qty, decimal QtyIc)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            _so.ResetBoxQty(OsNo, PrdMark, BoxItm, Qty, QtyIc);
        }
        #endregion

        #region 得出受订单据不同配码比在prdt1中的第一笔数据
        /// <summary>
        /// 得出受订单据不同配码比在prdt1中的第一笔数据
        /// </summary>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetFirstPrdt1ByBox(DateTime BeginDate, DateTime EndDate)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            return _so.GetFirstPrdt1ByBox(BeginDate.ToString(Comp.SQLDateFormat), EndDate.ToString(Comp.SQLDateFormat));
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
            if (cls_name == "CLS_ID")
            {
                _error = this.DoCloseBill(bil_id, bil_no, false);
            }
            return _error;
        }

        string Sunlike.Business.ICloseBill.DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "CLS_ID")
            {
                _error = this.DoCloseBill(bil_id, bil_no, true);
            }
            return _error;
        }

        #endregion

        #region 检查表头客户是否有改变
        /// <summary>
        /// 要货转受订，在回写数量及结案时，检查表头客户是否有改变。如果有改变则不能回写。
        /// </summary>
        /// <param name="CusNo"></param>
        /// <param name="YhId"></param>
        /// <param name="YhNo"></param>
        /// <returns></returns>
        private bool chkCusChange(string CusNo, string YhId,string YhNo)
        {
            DRPYHut _yh = new DRPYHut();
            string _yhCusNo = _yh.getCusNo(YhId, YhNo);
            if (CusNo == _yhCusNo)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 修改受订放行状态
        /// <summary>
        /// 修改受订放行状态
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="hasFx"></param>
        /// <returns></returns>
        public bool UpdateHasFx(string osId, string osNo,bool hasFx)
        {
            SunlikeDataSet _ds = this.GetData(null, osId, osNo, false, false);
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {

            }
            else
            {
                throw new SunlikeException(/*[{0}]单号不存在,请检查!*/"RCID=MTN.HINT.BIL_NO_NULL,PARAM=" + osNo);
            }
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            return _so.UpdateHasFx(osId, osNo, hasFx);
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
                if (isDel)
                {
                    wcNo = Convert.ToString(dr["WC_NO", DataRowVersion.Original]);
                    bilNo = Convert.ToString(dr["OS_NO", DataRowVersion.Original]);
                }
                else
                {
                    wcNo = Convert.ToString(dr["WC_NO"]);
                    bilNo = Convert.ToString(dr["OS_NO"]);
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
                                _dr["BIL_ID"] = dr["OS_ID"];
                                _dr["BIL_DD"] = dr["OS_DD"];
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

        #region 回写营销费用申请已交量

        /// <summary>
        /// 回写营销费用申请已交量
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="isApprove"></param>
        /// <param name="isRollback"></param>
        private void UpdateDrpMe(DataRow dataRow, bool isApprove, bool isRollback)
        {
            string _meId, _meNo;
            int _keyItm;
            decimal _qty;
            if (dataRow.RowState != DataRowState.Deleted)
            {
                _meId = dataRow["BIL_ID"].ToString();
                _meNo = dataRow["QT_NO"].ToString();
                _keyItm = Convert.ToInt32(dataRow["OTH_ITM"]);
                if (isApprove || isRollback)
                {
                    _qty = Convert.ToDecimal(dataRow["QTY"]);
                    if (isRollback)
                    {
                        _qty *= -1;
                    }
                }
                else if (dataRow.RowState == DataRowState.Added)
                {
                    _qty = Convert.ToDecimal(dataRow["QTY"]);
                }
                else if (dataRow.RowState == DataRowState.Modified)
                {
                    _qty = Convert.ToDecimal(dataRow["QTY"]) - Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                _meId = dataRow["BIL_ID", DataRowVersion.Original].ToString();
                _meNo = dataRow["QT_NO", DataRowVersion.Original].ToString();
                _keyItm = Convert.ToInt32(dataRow["OTH_ITM", DataRowVersion.Original]);
                _qty = Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]) * -1;
            }
            DRPME _drpMe = new DRPME();
            _drpMe.UpdateQtySo(_meId, _meNo, _keyItm, _qty);
        }

        #endregion

        #region 更新客户储值余额
        /// <summary>
        /// 生成帐户变动单
        /// </summary>
        /// <param name="dr">受订单表头行</param>
        /// <param name="isAdd">行状态</param>
        private void UpdateMonCbac(DataRow dr, bool isAdd)
        {
            string _osId = "";
            string _cusNo = "";
            string _chkMan = "";
            string _cbacCls = "";
            decimal _amtn = 0;

            if (isAdd)
            {
                _osId = dr["OS_ID"].ToString();
                _cusNo = dr["CUS_NO"].ToString();
                _chkMan = dr["CHK_MAN"].ToString();
                _cbacCls = dr["CBAC_CLS"].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN_CBAC"].ToString()))
                    _amtn = Convert.ToDecimal(dr["AMTN_CBAC"]);
            }
            else
            {
                _osId = dr["OS_ID", DataRowVersion.Original].ToString();
                _cusNo = dr["CUS_NO", DataRowVersion.Original].ToString();
                _chkMan = dr["CHK_MAN", DataRowVersion.Original].ToString();
                _cbacCls = dr["CBAC_CLS", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["AMTN_CBAC", DataRowVersion.Original].ToString()))
                    _amtn = (-1) * Convert.ToDecimal(dr["AMTN_CBAC", DataRowVersion.Original]);
            }

            CBac _cbac = new CBac();
            if (string.Compare("SO", _osId) != 0)
            {
                return;
            }
            if (_amtn == 0)
                return;
            else
            {
                //检测客户帐户余额不足
                decimal _amtnCustBac = _cbac.GetCustBac(false, _cusNo);
                if (isAdd)
                {
                    if (dr.RowState == DataRowState.Modified)
                    {
                        if (!string.IsNullOrEmpty(dr["AMTN_CBAC", DataRowVersion.Original].ToString()))
                            _amtnCustBac += Convert.ToDecimal(dr["AMTN_CBAC", DataRowVersion.Original]);
                    }
                    _amtnCustBac -= _amtn;
                    if (_amtnCustBac < 0)
                    {
                        throw new SunlikeException(/*客户帐户余额不足*/"RCID=MON.HINT.CUST_BAC_LITTLE");
                    }
                }
                //如果客户储值已经结案则不允许删除
                if (!isAdd && string.Compare("T", _cbacCls) == 0)
                {
                    throw new SunlikeException(/*客户储值已经结案,不允许修改*/"RCID=MON.HINT.CBAC_CLS");
                }
            }
            Dictionary<string, decimal> _updateField = new Dictionary<string, decimal>();
            if (string.IsNullOrEmpty(_chkMan))
            {
                _updateField["AMTN_SO_UNSH"] = _amtn;
            }
            else
            {
                _updateField["AMTN_SO"] = _amtn;
            }

            _cbac.UpdateCustBac(_cusNo, _updateField);
        }
        #endregion

        #region 回写受订单客户储值结案标记
        /// <summary>
        /// 回写受订单客户储值结案标记
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="cbacCls"></param>
        public void UpdateCbacCls(string osId, string osNo,bool cbacCls)
        {
            DbDrpSO _so = new DbDrpSO(Comp.Conn_DB);
            _so.UpdateCbacCls(osId, osNo, cbacCls);
        }
        #endregion

        #region 回写采购分析

        /// <summary>
        /// 回写采购分析
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="estItm"></param>
        /// <param name="mpId"></param>
        public void UpdateMpId(string osId, string osNo, string estItm, string mpId)
        {
            DbDrpSO _dbSo = new DbDrpSO(Comp.Conn_DB);
            _dbSo.UpdateMpId(osId, osNo, estItm, mpId);
        }

        #endregion
    }
}