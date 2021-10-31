using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business
{
    /// <summary>
    /// 入库单
    /// </summary>
    public class DRPTI : BizObject, IAuditing
    {
        /// <summary>
        /// 入库单
        /// </summary>
        public DRPTI()
        {
        }

        #region Variable
        bool bRunAuditing;
        bool bInAuditing;
        #endregion

        #region GetData

        /// <summary>
        /// 取得出库单信息
        /// </summary>
        /// <param name="tiId">单据类别</param>
        /// <param name="tiNo">单据号</param>
        /// <param name="usr">用户代号</param>
        /// <param name="pgm">程序代号</param>
        /// <param name="isSchema">是否只取结构</param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string pgm, string tiId, string tiNo, string usr, bool isSchema)
        {
            DbDRPTI _dbTi = new DbDRPTI(Comp.Conn_DB);
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            if (isSchema)
            {
                _ds = _dbTi.GetData("", "");
            }
            else
            {
                _ds = _dbTi.GetData(tiId, tiNo);
            }
            if (_ds.Tables["MF_TI"].Rows.Count > 0)
            {
                string _bill_Dep = _ds.Tables["MF_TI"].Rows[0]["DEP"].ToString();
                string _bill_Usr = _ds.Tables["MF_TI"].Rows[0]["USR"].ToString();
                System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
            }
            DataTable _dtTf = _ds.Tables["TF_TI"];
            _dtTf.Columns["EST_ITM"].AutoIncrement = true;
            _dtTf.Columns["EST_ITM"].AutoIncrementSeed = _dtTf.Rows.Count > 0 ? Convert.ToInt32(_dtTf.Select("", "EST_ITM DESC")[0]["EST_ITM"]) + 1 : 1;
            _dtTf.Columns["PRE_ITM"].AutoIncrement = true;
            _dtTf.Columns["PRE_ITM"].AutoIncrementSeed = _dtTf.Rows.Count > 0 ? Convert.ToInt32(_dtTf.Select("", "PRE_ITM DESC")[0]["PRE_ITM"]) + 1 : 1;
            _dtTf.Columns["CK_ITM"].AutoIncrement = true;
            _dtTf.Columns["CK_ITM"].AutoIncrementSeed = _dtTf.Rows.Count > 0 ? Convert.ToInt32(_dtTf.Select("", "CK_ITM DESC")[0]["CK_ITM"]) + 1 : 1;

            SetCanModify(_ds, true);
            return _ds;
        }
        /// <summary>
        /// 入库免检转进货
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataTiHl(string sqlWhere)
        {
            DbDRPTI _dbTi = new DbDRPTI(Comp.Conn_DB);
            return _dbTi.GetDataTiHl(sqlWhere);
        }
        /// <summary>
        /// 取得出库单信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetT6Data(string sqlWhere)
        {
            DbDRPTI _dbTi = new DbDRPTI(Comp.Conn_DB);
            return _dbTi.GetT6Data(sqlWhere);
        }
        /// <summary>
        /// 取得出库单信息
        /// </summary>
        /// <param name="tiId">单据类别</param>
        /// <param name="tiNo">单据号</param>
        /// <param name="usr">用户代号</param>
        /// <param name="pgm">程序代号</param>
        /// <param name="isSchema">是否只取结构</param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string tiId, string tiNo, string keyField, int keyValue)
        {
            DbDRPTI _dbTi = new DbDRPTI(Comp.Conn_DB);
            SunlikeDataSet _ds = new SunlikeDataSet();
            return _ds = _dbTi.GetBody(tiId, tiNo, keyField, keyValue);
        }
        /// <summary>
        /// 获取厂商可送货的最大的数量
        /// </summary>
        /// <param name="type">TW|PO|SL</param>
        /// <param name="bilNo">原单号</param>
        /// <param name="estItm">项次</param>
        /// <param name="slNo">收料转入时的收料单号</param>
        /// <param name="ratio">允许超交比率</param>
        /// <returns>最大的可送货数量</returns>
        public string GetMaxQty(string type, string bilNo, string estItm, string slNo, decimal ratio)
        {
            DbDRPTI _db = new DbDRPTI(Comp.Conn_DB);
            return _db.GetMaxQty(type, bilNo, estItm, slNo, ratio);
        }

        /// <summary>
        /// 检查是否可以修改
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="bCheckAuditing">是否检查审核流程</param>
        private void SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            DataTable _dtMf = ds.Tables["MF_TI"];
            DataTable _dtTf = ds.Tables["TF_TI"];
            if (_dtMf.Rows.Count > 0)
            {
                bool _bCanModify = true;
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["TI_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_MNU"))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                }

                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_dtMf.Rows[0]["TI_ID"].ToString(), _dtMf.Rows[0]["TI_NO"].ToString()))
                    {
                        _bCanModify = false;
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");
                    }
                }
                //判断是否结案
                
                    if (_dtMf.Rows[0]["CLOSE_ID"].ToString() == "T")
                    {
                        _bCanModify = false;
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");
                    }
                
                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
                //如果可以修改，则判断是否已经有表身进行了品检，如有一笔表身已经品检过了，则不允许删除此单据
                //如果已做了免检缴回，则MM_NO被回写，这种状况不允许删除此单
                if (_bCanModify)
                {
                    if (_dtTf.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dtTf.Rows.Count; i++)
                        {
                            string _qtyRtn = _dtTf.Rows[i]["QTY_RTN"].ToString();
                            string _qtyPs = _dtTf.Rows[i]["QTY_PS"].ToString();
                            string _mmno = Convert.ToString(_dtTf.Rows[i]["MM_NO"]);
                            if (!String.IsNullOrEmpty(_qtyRtn))
                            {
                                if (Convert.ToDecimal(_qtyRtn) != 0)
                                {
                                    ds.ExtendedProperties["DEL"] = "F";
                                    break;
                                }
                            }
                            if (!String.IsNullOrEmpty(_qtyPs))
                            {
                                if (Convert.ToDecimal(_qtyPs) != 0)
                                {
                                    ds.ExtendedProperties["DEL"] = "F";
                                    break;
                                }
                            }
                            if (!String.IsNullOrEmpty(_mmno))
                            {
                                if (!string.IsNullOrEmpty(_mmno.Trim()))
                                {
                                    ds.ExtendedProperties["DEL"] = "F";
                                    break;
                                }
                            }
                        }
                    }

                }

                ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            }
        }

        /// <summary>
        /// 根据货号和厂商号，返回货品的检验标志
        /// </summary>
        /// <param name="prdt">货号</param>
        /// <param name="cus">厂商号</param>
        /// <param name="isTW">采购 or 托外</param>
        /// <returns>chkty_id</returns>
        public string GetChkId(string prdt, string cus, bool isTW)
        {
            DbDRPTI _ti = new DbDRPTI(Comp.Conn_DB);
            string chkId = _ti.GetChkId(prdt, cus, isTW);
            if (string.IsNullOrEmpty(chkId))
            {
                CompInfo _info = Comp.GetCompInfo("");
                chkId = isTW ? _info.CompanyInfo.CHK_TW : _info.CompanyInfo.CHK_PC;
            }
            return chkId;
        }
        #endregion

        #region SetData

        #region 表头
        /// <summary>
        /// 设置表头数据
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="tiId">单据类别</param>
        /// <param name="usr">填单人</param>
        /// <param name="tiDd">单据日期</param>
        /// <param name="cusNo">客户代号</param>
        /// <param name="salNo">经办人</param>
        /// <param name="cusOsNo">客户订单号</param>
        /// <param name="batNo">批号</param>
        /// <param name="bilType">单据类别</param>
        /// <param name="rem">备注</param>
        /// <param name="mobid">sh模板</param>
        public void SetHeadData(SunlikeDataSet ds, string tiId, DateTime tiDd, string usr, string cusNo, string salNo, string cusOsNo, string batNo, string bilType, string rem, string mobid)
        {
            try
            {
                DataTable _dtMf = ds.Tables["MF_TI"];
                Users _users = new Users();
                string _dep = _users.GetUserDepNo(usr);
                Cust _cust = new Cust();
                Salm _salm = new Salm();
                if (_dtMf.Rows.Count > 0)
                {
                    DataRow _dr = _dtMf.Rows[0];
                    _dr["TI_DD"] = tiDd;
                    _dr["CUS_NO"] = cusNo;
                    _dr["CUS_NAME"] = _cust.GetCusName(cusNo);
                    _dr["USR"] = usr;
                    if (String.IsNullOrEmpty(_dr["DEP"].ToString()))
                    {
                        _dr["DEP"] = _dep;
                    }
                    _dr["BAT_NO"] = batNo;
                    _dr["SAL_NO"] = salNo;
                    _dr["SAL_NAME"] = _salm.GetSalmName(salNo);
                    _dr["CUS_OS_NO"] = cusOsNo;
                    _dr["BIL_TYPE"] = bilType;
                    _dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    _dr["REM"] = rem;
                    _dr["MOB_ID"] = mobid;
                }
                else
                {
                    SQNO _sqNo = new SQNO();
                    DataRow _dr = _dtMf.NewRow();
                    _dr["TI_ID"] = tiId;
                    _dr["TI_NO"] = _sqNo.Get(tiId, usr, _dep, tiDd, bilType);
                    _dr["TI_DD"] = tiDd;
                    _dr["CUS_NO"] = cusNo;
                    _dr["CUS_NAME"] = _cust.GetCusName(cusNo);
                    _dr["USR"] = usr;
                    _dr["DEP"] = _dep;
                    _dr["BAT_NO"] = batNo;
                    _dr["SAL_NO"] = salNo;
                    _dr["SAL_NAME"] = _salm.GetSalmName(salNo);
                    _dr["CUS_OS_NO"] = cusOsNo;
                    _dr["BIL_TYPE"] = bilType;
                    _dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    _dr["REM"] = rem;
                    _dr["MOB_ID"] = mobid;
                    _dtMf.Rows.Add(_dr);
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="ds">DataSet</param>
        public void Delete(SunlikeDataSet ds)
        {
            try
            {
                DataTable _dtMf = ds.Tables["MF_TI"];
                DataTable _dtTf = ds.Tables["TF_TI"];
                //_dtTf.AcceptChanges();
                foreach (DataRow dr in _dtTf.Rows)
                {
                    if (dr.RowState != DataRowState.Added && dr.RowState != DataRowState.Deleted)
                        dr.Delete();
                }
                //_dtMf.AcceptChanges();
                _dtMf.Rows[0].Delete();
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion

        #region 表身
        /// <summary>
        /// 新增表身
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="prdNo">品号</param>
        /// <param name="supPrdNo">对方品号</param>
        /// <param name="prdMark">特征</param>
        /// <param name="wh">库位</param>
        /// <param name="unit">单位</param>
        /// <param name="qty">数量</param>
        /// <param name="qtyCus">厂商送货量</param>
        /// <param name="bilId">来源单据类别</param>
        /// <param name="bilNo">来源单号</param>
        /// <param name="estItm">跟踪项次</param>
        /// <param name="slNo">收料单号</param>
        /// <param name="slItm">收料单项次</param>
        /// <param name="gfNo">关封号</param>
        /// <param name="rem">备注</param>
        /// <param name="cusOsNo">客户订单号</param>
        /// <param name="bDd">最早交货日期</param>
        /// <param name="eDd">最晚交货日期</param>
        /// <param name="shno">送货单号</param>
        /// <param name="isChk">是否检验</param>
        public void InserBodyData(SunlikeDataSet ds, string prdNo, string supPrdNo, string prdMark, string wh, string unit,
            decimal qty, decimal qtyCus, string bilId, string bilNo, string estItm, string slNo, string slItm,
            string gfNo, string rem, string cusOsNo, string bDd, string eDd, string shno, string isChk)
        {
            try
            {
                DataTable _dtTf = ds.Tables["TF_TI"];
                DataRow _dr = _dtTf.NewRow();
                _dr["TI_ID"] = ds.Tables["MF_TI"].Rows[0]["TI_ID"];
                _dr["TI_NO"] = ds.Tables["MF_TI"].Rows[0]["TI_NO"];
                _dr["ITM"] = GetMaxItm(ds);
                _dr["SH_NO_CUS"] = shno;
                _dr["PRD_NO"] = prdNo;
                Prdt _prdt = new Prdt();
                DataTable _dtPrdt = _prdt.GetPrdt(prdNo);
                if (_dtPrdt.Rows.Count > 0)
                {
                    _dr["PRD_NAME"] = _dtPrdt.Rows[0]["NAME"];
                    _dr["SPC"] = _dtPrdt.Rows[0]["SPC"];
                }
                _dr["SUP_PRD_NO"] = supPrdNo;
                _dr["PRD_MARK"] = prdMark;
                _dr["WH"] = wh;
                _dr["WH_NAME"] = wh;
                _dr["UNIT"] = unit;
                _dr["QTY"] = qty;
                _dr["QTY_CUS"] = qtyCus;
                _dr["BIL_ID"] = bilId;
                _dr["BIL_NO"] = bilNo;
                _dr["CHKTY_ID"] = isChk;
                if (!String.IsNullOrEmpty(estItm))
                {
                    _dr["EST_ITM"] = Convert.ToInt32(estItm);
                }
                _dr["SL_NO"] = slNo;
                if (!String.IsNullOrEmpty(slItm))
                {
                    _dr["SL_ITM"] = Convert.ToInt32(slItm);
                }
                _dr["GF_NO"] = gfNo;
                _dr["REM"] = rem;
                _dr["CUS_OS_NO"] = cusOsNo;
                if (!String.IsNullOrEmpty(bDd))
                {
                    _dr["B_DD"] = Convert.ToDateTime(bDd);
                }
                if (!String.IsNullOrEmpty(eDd))
                {
                    _dr["E_DD"] = Convert.ToDateTime(eDd);
                }
                _dtTf.Rows.Add(_dr);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// 取得最大项次
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        private int GetMaxItm(SunlikeDataSet ds)
        {
            try
            {
                DataTable _dtTf = ds.Tables["TF_TI"];
                if (_dtTf.Rows.Count > 0)
                {
                    if (_dtTf.Select("", "ITM DESC", DataViewRowState.CurrentRows).Length > 0)
                    {
                        return Convert.ToInt32(_dtTf.Select("", "ITM DESC", DataViewRowState.CurrentRows)[0]["ITM"]) + 1;
                    }
                }
                return 1;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// 更新表身
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="itm">项次</param>
        /// <param name="gfNo">关封编号</param>
        /// <param name="prdMark">特征</param>
        /// <param name="wh">库位</param>
        /// <param name="supPrdNo">对方品号</param>
        /// <param name="qty">数量</param>
        /// <param name="qtyCus"></param>
        /// <param name="rem">备注</param>
        public void UpdateBodyData(SunlikeDataSet ds, string itm, string prdMark, string wh, string gfNo, string supPrdNo, decimal qty, decimal qtyCus, string rem)
        {
            try
            {
                DataRow[] _aryDr = ds.Tables["TF_TI"].Select("ITM = " + itm);
                if (_aryDr.Length > 0)
                {
                    _aryDr[0]["PRD_MARK"] = prdMark;
                    _aryDr[0]["WH"] = wh;
                    _aryDr[0]["GF_NO"] = gfNo;
                    _aryDr[0]["REM"] = rem;
                    _aryDr[0]["SUP_PRD_NO"] = supPrdNo;
                    _aryDr[0]["QTY"] = qty;
                    _aryDr[0]["QTY_CUS"] = qtyCus;
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// 删除表身
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="itm">项次</param>
        public bool DeleteBodyData(SunlikeDataSet ds, string itm)
        {
            bool _del = true;//是否可以删除
            try
            {
                DataRow[] _aryDr = ds.Tables["TF_TI"].Select("ITM = " + itm);
                if (_aryDr.Length > 0)
                {
                    if (!String.IsNullOrEmpty(_aryDr[0]["MM_NO"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(_aryDr[0]["MM_NO"]).Trim()))
                        {
                            _del = false;
                        }
                    }
                    if (!String.IsNullOrEmpty(_aryDr[0]["QTY_RTN"].ToString()))
                    {
                        if (Convert.ToDecimal(_aryDr[0]["QTY_RTN"]) != 0)
                        {
                            _del = false;
                        }
                    }
                    if (!String.IsNullOrEmpty(_aryDr[0]["QTY_PS"].ToString()))
                    {
                        if (Convert.ToDecimal(_aryDr[0]["QTY_PS"]) != 0)
                        {
                            _del = false;
                        }
                    }
                    if (_del)
                    {
                        _aryDr[0].Delete();
                    }
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _del;
        }
        #endregion

        #region 重整ITM
        /// <summary>
        /// 重整ITM
        /// </summary>
        /// <param name="ds"></param>
        public void SettleItem(SunlikeDataSet ds)
        {
            DataTable _dtMf = ds.Tables["MF_TI"];
            DataTable _dtTf = ds.Tables["TF_TI"];
            int _itm = 1;
            if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState == DataRowState.Added)
            {
                foreach (DataRow dr in _dtTf.Select("", "PRE_ITM"))
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        dr["PRE_ITM"] = _itm++;
                    }
                }
            }
            _itm = 1;
            foreach (DataRow dr in _dtTf.Select("", "ITM"))
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    dr["ITM"] = _itm++;
                }
            }
            _itm = 1;
            foreach (DataRow dr in _dtTf.Select("", "CK_ITM"))
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    dr["CK_ITM"] = _itm++;
                }
            }
        }
        #endregion

        #endregion

        #region Save
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="changedDs"></param>
        /// <param name="bubbleException"></param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, SunlikeDataSet dataSet, bool bubbleException)
        {
            DataTable _dtMf = dataSet.Tables["MF_TI"];
            string _tiId = "";
            string _tiNo = "";
            string _usr = "";
            if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            {
                _tiId = _dtMf.Rows[0]["TI_ID"].ToString();
                _tiNo = _dtMf.Rows[0]["TI_NO"].ToString();
                _usr = _dtMf.Rows[0]["USR"].ToString();
            }
            else
            {
                _tiId = _dtMf.Rows[0]["TI_ID", DataRowVersion.Original].ToString();
                _tiNo = _dtMf.Rows[0]["TI_NO", DataRowVersion.Original].ToString();
                _usr = _dtMf.Rows[0]["USR", DataRowVersion.Original].ToString();
            }
            Auditing _aud = new Auditing();
            DataRow _dr = _dtMf.Rows[0];
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
            //this.bRunAuditing = _aud.IsRunAuditing(_tiId, _usr, _bilType, _mobID);

            this.bInAuditing = _aud.GetIfEnterAuditing(_tiId, _tiNo);

            SettleItem(dataSet);

            Hashtable _ht = new Hashtable();
            _ht["MF_TI"] = "TI_ID, TI_NO, TI_DD, SAL_NO, CUS_NO, DEP, BIL_ID, BIL_NO, OS_ID, OS_NO, BIL_TYPE, CUS_OS_NO, BAT_NO, REM, USR, CHK_MAN, CLS_DATE, PRT_SW, CLOSE_ID, CHKTY_ID, SYS_DATE,MOB_ID";
            _ht["TF_TI"] = "TI_ID, TI_NO, ITM, PRD_NO, PRD_NAME, PRD_MARK, WH, UNIT, QTY, REM, BIL_ID, BIL_NO, ID_NO, CUS_NO, SUP_PRD_NO, EST_ITM, SL_NO, SL_ITM, CK_ITM, GF_NO, PRE_ITM, B_DD, E_DD, QTY_CUS,SH_NO_CUS,CHKTY_ID,RK_DD,BAT_NO,QTY_RK";
            this.UpdateDataSet(dataSet, _ht);
            if (!dataSet.HasErrors)
            {
                #region 未发生错误
                //增加单据权限
                string _UpdUsr = "";
                if (dataSet.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = dataSet.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "DRP" + _tiId;
                    if (!string.IsNullOrEmpty(pgm))
                    {
                        _pgm = pgm;
                    }
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
                    dataSet.AcceptChanges();
                #endregion
                }
                else
                {
                    #region 发生错误
                    if (bubbleException)
                    {
                        string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(dataSet);
                        throw new SunlikeException("RCID=DRPTI.UpdateData() Error;" + _errorMsg);
                    }
                    else
                    {
                        return Sunlike.Business.BizObject.GetAllErrors(dataSet);
                    }
                    #endregion
                }
            }
            return Sunlike.Business.BizObject.GetAllErrors(dataSet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dtMf = ds.Tables["MF_TI"];
            //if (_dtMf.Rows.Count > 0)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _dtMf.Rows[0]["TI_ID"].ToString());
            //    }
            //    else
            //    {
            //        _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _dtMf.Rows[0]["TI_ID", DataRowVersion.Original].ToString());
            //    }
            //}
            //#endregion
        }
        /// <summary>
        /// 行保存前
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            #region 判断是否锁单
            string _tiNo = "", _tiId = "";
            if (statementType == StatementType.Delete)
            {
                _tiNo = dr["TI_NO", DataRowVersion.Original].ToString();
                _tiId = dr["TI_ID", DataRowVersion.Original].ToString();
            }
            else
            {
                _tiNo = dr["TI_NO"].ToString();
                _tiId = dr["TI_ID"].ToString();
            }
            if (statementType != StatementType.Insert)
            {
                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "TI_ID = '" + _tiId + "' AND TI_NO = '" + _tiNo + "'";
                if (_Users.IsLocked("MF_TI", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            if (tableName == "MF_TI")
            {
                 #region 关账
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["TI_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["TI_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion

                #region Variable
                string _dep = "";
                string _usr = "";
                string _salNo = "";
                DateTime _tiDd;
                if (statementType != StatementType.Delete)
                {
                    _tiId = dr["TI_ID"].ToString();
                    _tiNo = dr["TI_NO"].ToString();
                    _dep = dr["DEP"].ToString();
                    _salNo = dr["SAL_NO"].ToString();
                    _usr = dr["USR"].ToString();
                    _tiDd = Convert.ToDateTime(dr["TI_DD"]);
                }
                else
                {
                    _tiId = dr["TI_ID", DataRowVersion.Original].ToString();
                    _tiNo = dr["TI_NO", DataRowVersion.Original].ToString();
                    _dep = dr["DEP", DataRowVersion.Original].ToString();
                    _salNo = dr["SAL_NO", DataRowVersion.Original].ToString();
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                    _tiDd = Convert.ToDateTime(dr["TI_DD", DataRowVersion.Original]);
                }

                #endregion

                #region 检查基础资料

                if (statementType != StatementType.Delete)
                {
                    Salm _salm = new Salm();
                    if (_salNo.Trim() != "")
                    {
                        if (!_salm.IsExist(_usr, _salNo, Convert.ToDateTime(dr["TI_DD"])))
                        {
                            throw new SunlikeException("RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + _salNo);
                        }
                    }
                }

                #endregion

                #region 判断关帐

                if (Comp.HasCloseBill(_tiDd, _dep, "CLS_INV"))
                {
                    throw new SunlikeException("RCID=COMMON.HINT.ACCCLOSE");
                }

                #endregion

                #region 基本信息

                if (statementType != StatementType.Delete)
                {
                    //写BIL_ID,BIL_NO,OS_ID,OS_NO
                    DataTable _dtTf = dr.Table.DataSet.Tables["TF_TI"];
                    if (_dtTf.Select().Length > 0)
                    {
                        if (_dtTf.Rows[0]["SL_NO"].ToString() != "")
                        {
                            //从收料转入
                            dr["OS_ID"] = "SL";
                            dr["OS_NO"] = _dtTf.Rows[0]["SL_NO"];
                        }
                        else
                        {
                            //从采购转入,维修单转入
                            dr["OS_ID"] = _dtTf.Rows[0]["BIL_ID"];
                            dr["OS_NO"] = _dtTf.Rows[0]["BIL_NO"];
                        }
                    }
                    //检索表身的单据号
                    string _sBilNo = "";
                    string _bilId = "";
                    string _bilNo = "";
                    foreach (DataRow drTf in _dtTf.Rows)
                    {
                        if (drTf.RowState != DataRowState.Deleted)
                        {
                            if (_bilId == "")
                            {
                                _bilId = drTf["BIL_ID"].ToString();
                                _bilNo = drTf["BIL_NO"].ToString();
                            }
                            if (_sBilNo.IndexOf(drTf["BIL_NO"].ToString()) < 0)
                            {
                                if (_sBilNo != "")
                                {
                                    _sBilNo += ",";
                                }
                                _sBilNo += drTf["BIL_NO"].ToString();
                            }
                        }
                    }
                    if (_sBilNo.IndexOf(",") >= 0)
                    {
                        string[] _aryBilNo = _sBilNo.Split(',');
                        _bilNo += "-" + _aryBilNo.Length.ToString();
                    }
                    if (!string.IsNullOrEmpty(_bilId))
                        dr["BIL_ID"] = _bilId;
                    if (!string.IsNullOrEmpty(_bilNo))
                        dr["BIL_NO"] = _bilNo;
                    if (_bilId == "TW")
                    {
                        _tiId = "T7";
                        dr["TI_ID"] = "T7";
                    }

                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);

                    if (statementType == StatementType.Insert)
                    {
                        SQNO _sqNo = new SQNO();
                        dr["TI_NO"] = _sqNo.Set(dr["TI_ID"].ToString(), dr["USR"].ToString(), dr["DEP"].ToString(), Convert.ToDateTime(dr["TI_DD"]), "");
                        _tiNo = dr["TI_NO"].ToString();

                        dr["CLOSE_ID"] = "F";
                        dr["PRT_SW"] = "N";

                        if (dr["TI_ID"].ToString() == "T6")
                        {
                            dr["CHKTY_ID"] = "T";
                        }
                    }
                }

                #endregion

                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_TYPE = "";
                //    _aps.BIL_ID = _tiId;
                //    _aps.BIL_NO = _tiNo;
                //    _aps.BIL_DD = _tiDd;
                //    _aps.USR = _usr;
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = _dep;
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //    _aps = new AudParamStruct(_tiId, _tiNo);
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(bRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

            }
            if (tableName == "TF_TI")
            {
                #region Variable
                string _usr = "";
                string _prdNo = "";
                string _wh = "";

                if (dr.Table.DataSet.Tables["MF_TI"].Rows[0].RowState != DataRowState.Deleted)
                {
                    _usr = dr.Table.DataSet.Tables["MF_TI"].Rows[0]["USR"].ToString();
                }
                else
                {
                    _usr = dr.Table.DataSet.Tables["MF_TI"].Rows[0]["USR", DataRowVersion.Original].ToString();
                }
                if (statementType != StatementType.Delete)
                {
                    _prdNo = dr["PRD_NO"].ToString();
                    _wh = dr["WH"].ToString();
                }
                else
                {
                    _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                    _wh = dr["WH", DataRowVersion.Original].ToString();
                }
                #endregion

                #region 缺省值
                if (statementType == StatementType.Insert)
                {
                    if (dr["PRD_MARK"] == System.DBNull.Value)
                    {
                        dr["PRD_MARK"] = "";
                    }
                }
                #endregion

                #region 检查基础资料
                if (statementType != StatementType.Delete)
                {
                    Prdt _prdt = new Prdt();
                    if (!_prdt.IsExist(_usr, _prdNo, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_TI"].Rows[0]["TI_DD"])) && _prdNo != "")
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prdNo);
                    }
                    WH _whClass = new WH();
                    if (!_whClass.IsExist(_usr, _wh, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_TI"].Rows[0]["TI_DD"])) && _wh != "")
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                    }
                }
                #endregion

                #region 基本信息
                //如果是托工单转入，写配方号
                if (statementType != StatementType.Delete)
                {
                    if (dr["BIL_ID"].ToString() == "TW")
                    {
                        DRPTW _drpTw = new DRPTW();
                        dr["ID_NO"] = _drpTw.GetIdNo(dr["BIL_NO"].ToString());
                    }

                    if (statementType == StatementType.Insert && dr["TI_ID"].ToString() == "T6")
                    {
                        dr["CHKTY_ID"] = "T";
                        dr["RK_DD"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                    }
                }
                #endregion

                #region 修改入库量
                if (statementType != StatementType.Delete)
                {
                    dr["QTY_RK"] = dr["QTY"];
                }
                #endregion
            }
        }

        /// <summary>
        /// 行保存后
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            if (tableName == "MF_TI")
            {
                if (statementType == StatementType.Delete)
                {
                    SQNO _sqNo = new SQNO();
                    _sqNo.Delete(dr["TI_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());

                    //删除对应的收料计划单中的转出单号,这个地方怪怪的，多次转入收料计划到入库单，只会记录第一笔转入的入库单号
                    //不知道用来干什么，情何以堪呀
                    //对啊，我也觉得，为什么多张单据只记录第一张呢，有什么用？---CXY
                    //我觉得转入多张情况下应该记录为XXXXXXX-N，isn't it? ---CXY
                    DRPSL _sl = new DRPSL();
                    _sl.SetBilNoNull(dr["TI_NO", DataRowVersion.Original].ToString());
                }
            }
            else if (tableName == "TF_TI")
            {
                if (!this.bRunAuditing)
                {
                    this.UpdateQtyRk(dr, false);
                    this.UpdateWh(dr, false);
                }
            }
        }

        #endregion

        #region Function
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="isRollBack"></param>
        public void UpdateQtyRk(DataRow dataRow, bool isRollBack)
        {
            if (dataRow.RowState != DataRowState.Deleted)
            {
                decimal _qty = 0;
                if (dataRow.RowState == DataRowState.Added || dataRow.RowState == DataRowState.Unchanged)
                {
                    _qty = Convert.ToDecimal(dataRow["QTY"]);
                }
                else
                {
                    _qty = Convert.ToDecimal(dataRow["QTY"]) - Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]);
                }
                if (isRollBack)
                {
                    _qty *= -1;
                }
                if (dataRow["BIL_ID"].ToString() == "PO")
                {
                    DRPPO _drpPo = new DRPPO();
                    _drpPo.SetFromTi(dataRow["BIL_ID"].ToString(), dataRow["BIL_NO"].ToString(), dataRow["EST_ITM"].ToString(), _qty);
                }
                if (dataRow["BIL_ID"].ToString() == "TW")
                {
                    DRPTW _drpTw = new DRPTW();
                    _drpTw.SetFromTi(dataRow["BIL_NO"].ToString(), _qty);
                }
                if (dataRow["SL_NO"].ToString() != "")
                {
                    DRPSL _drpSl = new DRPSL();
                    _drpSl.SetFromTi(dataRow["SL_NO"].ToString(), dataRow["SL_ITM"].ToString(), dataRow["TI_ID"].ToString(), dataRow["TI_NO"].ToString());
                }
                if (dataRow["BIL_ID"].ToString() == "MO")
                {
                    DbDRPTI _dbTi = new DbDRPTI(Comp.Conn_DB);
                    _dbTi.UpdateQtyRk(dataRow["BIL_NO"].ToString(), _qty);
                    //强制退货控制
                    MRPMO _mo = new MRPMO();
                    DataTable _dtChkRtn = _mo.GetChkRtnInfoTI(dataRow["BIL_NO"].ToString());
                    if (_dtChkRtn.Rows.Count > 0)
                    {
                        CompInfo _compInfo = Comp.GetCompInfo("");
                        StringBuilder _errorMsg = new StringBuilder();
                        for (int i = 0; i < _dtChkRtn.Rows.Count; i++)
                        {
                            if (i != 0)
                                _errorMsg.Append(";");
                            //_errorMsg.Append(String.Format("单据[{0}]项次[{1}]产品[{2}],有未退回数量[{3}]",, , _dtChkRtn.Rows[0]["QTY"]));
                            _errorMsg.Append("RCID=MTN.HINT.CHECKRTN,PARAM=" + _dtChkRtn.Rows[i]["MO_NO"].ToString() + ",PARAM=" + _dtChkRtn.Rows[i]["PRE_ITM"].ToString() + ",PARAM=" + _dtChkRtn.Rows[i]["PRD_NAME"].ToString() + ",PARAM=" + string.Format("{0:F" + _compInfo.DecimalDigitsInfo.System.POI_QTY + "}", Convert.ToDecimal(_dtChkRtn.Rows[i]["QTY"])));

                        }
                        if (!string.IsNullOrEmpty(_errorMsg.ToString()))
                            throw new SunlikeException(_errorMsg.ToString());
                    }
                }
            }
            else
            {
                if (dataRow["BIL_ID", DataRowVersion.Original].ToString() == "PO")
                {
                    DRPPO _drpPo = new DRPPO();
                    _drpPo.SetFromTi(dataRow["BIL_ID", DataRowVersion.Original].ToString(), dataRow["BIL_NO", DataRowVersion.Original].ToString(), dataRow["EST_ITM", DataRowVersion.Original].ToString(), Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]) * -1);
                }
                if (dataRow["BIL_ID", DataRowVersion.Original].ToString() == "TW")
                {
                    DRPTW _drpTw = new DRPTW();
                    _drpTw.SetFromTi(dataRow["BIL_NO", DataRowVersion.Original].ToString(), Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]) * -1);
                }
                if (dataRow["SL_NO", DataRowVersion.Original].ToString() != "")
                {
                    DRPSL _drpSl = new DRPSL();
                    _drpSl.SetFromTi(dataRow["SL_NO", DataRowVersion.Original].ToString(), dataRow["SL_ITM", DataRowVersion.Original].ToString(), "", "");
                }
                if (dataRow["BIL_ID", DataRowVersion.Original].ToString() == "MO")
                {
                    DbDRPTI _dbTi = new DbDRPTI(Comp.Conn_DB);
                    _dbTi.UpdateQtyRk(dataRow["BIL_NO", DataRowVersion.Original].ToString(), Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]) * -1);

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="isRollBack"></param>
        public void UpdateWh(DataRow dataRow, bool isRollBack)
        {
            string _bilId;
            if (dataRow.RowState != DataRowState.Deleted)
            {
                _bilId = dataRow["BIL_ID"].ToString();
            }
            else
            {
                _bilId = dataRow["BIL_ID", DataRowVersion.Original].ToString();
            }
            if (_bilId == "MO")
            {
                string _prdNo, _prdMark, _whNo, _unit, _batNo;
                decimal _qty;
                Hashtable _ht = new Hashtable();
                WH _wh = new WH();
                if (dataRow.RowState != DataRowState.Added && dataRow.RowState != DataRowState.Unchanged)
                {
                    _prdNo = dataRow["PRD_NO", DataRowVersion.Original].ToString();
                    _prdMark = dataRow["PRD_MARK", DataRowVersion.Original].ToString();
                    _whNo = dataRow["WH", DataRowVersion.Original].ToString();
                    _unit = dataRow["UNIT", DataRowVersion.Original].ToString();
                    _batNo = dataRow["BAT_NO", DataRowVersion.Original].ToString();
                    _qty = Convert.ToDecimal(dataRow["QTY", DataRowVersion.Original]);

                    _ht[WH.QtyTypes.QTY_RK] = _qty * -1;
                    if (!String.IsNullOrEmpty(_batNo))
                    {
                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                    }
                    else
                    {
                        _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
                    }
                }
                if (dataRow.RowState != DataRowState.Deleted)
                {
                    _prdNo = dataRow["PRD_NO"].ToString();
                    _prdMark = dataRow["PRD_MARK"].ToString();
                    _whNo = dataRow["WH"].ToString();
                    _unit = dataRow["UNIT"].ToString();
                    _batNo = dataRow["BAT_NO"].ToString();
                    _qty = Convert.ToDecimal(dataRow["QTY"]);
                    if (isRollBack)
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
        }

        /// <summary>
        /// 回写入库单（TF_TI）的已验货量QTY_RTN
        /// </summary>
        /// <param name="tiId"></param>
        /// <param name="tiNo"></param>
        /// <param name="tiItm"></param>
        /// <param name="qtyRtn"></param>
        /// <param name="qty1Rtn"></param>
        public void UpdateQtyRtn(string tiId, string tiNo, string tiItm, decimal qtyRtn, decimal qty1Rtn)
        {
            DbDRPTI _ti = new DbDRPTI(Comp.Conn_DB);
            _ti.UpdateQtyRtn(tiId, tiNo, tiItm, qtyRtn, qty1Rtn);
        }

        /// <summary>
        /// 更新转进货量
        /// </summary>
        /// <param name="tiId"></param>
        /// <param name="tiNo"></param>
        /// <param name="preItm"></param>
        /// <param name="qtyPs"></param>
        public void UpdateQtyPs(string tiId, string tiNo, string preItm, decimal qtyPs)
        {
            DbDRPTI _dbDrpTi = new DbDRPTI(Comp.Conn_DB);
            _dbDrpTi.UpdateQtyPs(tiId, tiNo, preItm, qtyPs);
        }

        #endregion

        #region IAuditing 成员

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="bil_id">单据类别</param>
        /// <param name="bil_no">单号</param>
        /// <param name="chk_man">审核人</param>
        /// <param name="cls_dd">审核日期</param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            // TODO:  添加 DRPTI.Approve 实现
            string _msg = "";
            try
            {
                DbDRPTI _dbDrpTi = new DbDRPTI(Comp.Conn_DB);
                _dbDrpTi.Approve(bil_id, bil_no, chk_man, cls_dd);

                //回写采购单数量
                SunlikeDataSet _ds = _dbDrpTi.GetData(bil_id, bil_no);
                if (_ds.Tables["TF_TI"].Rows.Count > 0)
                {
                    foreach (DataRow dr in _ds.Tables["TF_TI"].Rows)
                    {
                        this.UpdateQtyRk(dr, false);
                        if (dr["BIL_ID"].ToString() == "MO")
                        {
                            this.UpdateWh(dr, false);
                        }
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
        /// 审核不同意
        /// </summary>
        /// <param name="bil_id">单据类别</param>
        /// <param name="bil_no">单号</param>
        /// <param name="chk_man">审核人</param>
        /// <param name="cls_dd">审核日期</param>
        /// <returns></returns>
        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            // TODO:  添加 DRPTI.Deny 实现
            return "";
        }

        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="bil_id">单据类别</param>
        /// <param name="bil_no">单号</param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            // TODO:  添加 DRPTI.RollBack 实现
            string _msg = "";
            try
            {
                DbDRPTI _dbDrpTi = new DbDRPTI(Comp.Conn_DB);
                _dbDrpTi.Rollback(bil_id, bil_no);

                //回写采购单数量
                SunlikeDataSet _ds = _dbDrpTi.GetData(bil_id, bil_no);
                if (_ds.Tables["TF_TI"].Rows.Count > 0)
                {
                    foreach (DataRow dr in _ds.Tables["TF_TI"].Rows)
                    {
                        this.UpdateQtyRk(dr, true);
                        this.UpdateWh(dr, true);
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


        #region 入库转进货

        /// <summary>
        /// 入库转进货
        /// </summary>
        /// <param name="dataSet"></param>
        public void UpdatePc(SunlikeDataSet dataSet)
        {
            DRPPC _drpPc = new DRPPC();
            Cust _cust = new Cust();
            SunlikeDataSet _ds = _drpPc.GetData("", "", "", true, false);

            DataRow _dr = _ds.Tables["MF_PSS"].NewRow();
            _dr["PS_ID"] = "PC";
            _dr["PS_NO"] = "PCXXXX0001";
            _dr["PS_DD"] = dataSet.Tables["TF_TI"].Rows[0]["TI_DD"];
            _dr["DEP"] = dataSet.Tables["TF_TI"].Rows[0]["DEP"];
            _dr["CUS_NO"] = dataSet.Tables["TF_TI"].Rows[0]["CUS_NO_MF"];
            _dr["OS_ID"] = dataSet.Tables["TF_TI"].Rows[0]["TI_ID"];
            _dr["OS_NO"] = dataSet.Tables["TF_TI"].Rows[0]["TI_NO"];
            //_dr["CUR_ID"] = dataSet.Tables["TF_TI"].Rows[0]["CUR_ID"];
            //_dr["EXC_RTO"] = dataSet.Tables["TF_TI"].Rows[0]["EXC_RTO"];
            _dr["SAL_NO"] = dataSet.Tables["TF_TI"].Rows[0]["SAL_NO"];
            _dr["REM"] = dataSet.Tables["TF_TI"].Rows[0]["MFREM"];
            _dr["USR"] = dataSet.Tables["TF_TI"].Rows[0]["USR"];

            Hashtable _ht = _cust.GetPAYInfo(_dr["CUS_NO"].ToString(), _dr["PS_DD"].ToString());
            if (_ht != null)
            {
                _dr["PAY_DD"] = _ht["PAY_DD"];
                _dr["CHK_DD"] = _ht["CHK_DD"];
                _dr["SEND_MTH"] = _ht["SEND_MTH"];
                _dr["SEND_WH"] = _ht["SEND_WH"];
                _dr["PAY_MTH"] = _ht["PAY_MTH"];
                _dr["PAY_DAYS"] = _ht["PAY_DAYS"];
                _dr["CHK_DAYS"] = _ht["CHK_DAYS"];
                _dr["INT_DAYS"] = _ht["INT_DAYS"];
                _dr["PAY_REM"] = _ht["PAY_REM"];
            }
            _ds.Tables["MF_PSS"].Rows.Add(_dr);

            for (int i = 0; i < dataSet.Tables["TF_TI"].Rows.Count; i++)
            {
                _dr = _ds.Tables["TF_PSS"].NewRow();
                _dr["PS_ID"] = "PC";
                _dr["PS_NO"] = "PCXXXX0001";
                _dr["ITM"] = i + 1;
                _dr["PS_DD"] = dataSet.Tables["TF_TI"].Rows[i]["TI_DD"];
                _dr["WH"] = dataSet.Tables["TF_TI"].Rows[i]["WH"];
                _dr["BAT_NO"] = dataSet.Tables["TF_TI"].Rows[i]["BAT_NO"];
                _dr["OS_ID"] = dataSet.Tables["TF_TI"].Rows[i]["TI_ID"];
                _dr["OS_NO"] = dataSet.Tables["TF_TI"].Rows[i]["TI_NO"];
                _dr["EST_ITM"] = dataSet.Tables["TF_TI"].Rows[i]["PRE_ITM"];
                _dr["PRD_NO"] = dataSet.Tables["TF_TI"].Rows[i]["PRD_NO"];
                _dr["PRD_NAME"] = dataSet.Tables["TF_TI"].Rows[i]["PRD_NAME"];
                _dr["PRD_MARK"] = dataSet.Tables["TF_TI"].Rows[i]["PRD_MARK"];
                _dr["UNIT"] = dataSet.Tables["TF_TI"].Rows[i]["UNIT"];
                _dr["QTY"] = dataSet.Tables["TF_TI"].Rows[i]["QTY"];
                _dr["UP"] = 0;
                _dr["AMTN_NET"] = 0;
                _dr["AMT"] = 0;
                _dr["TAX"] = 0;
                _dr["DIS_CNT"] = DBNull.Value;
                _dr["REM"] = dataSet.Tables["TF_TI"].Rows[i]["REM"];

                _ds.Tables["TF_PSS"].Rows.Add(_dr);
            }

            _drpPc.UpdateData(_ds, true);
        }

        #endregion
    }
}
