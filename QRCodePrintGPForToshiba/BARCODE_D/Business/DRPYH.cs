using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business
{
    /// <summary>
    /// 要货管理
    /// </summary>
    public class DRPYH : Sunlike.Business.BizObject, IAuditing, IAuditingInfo
    {
        private Sunlike.Business.Users _user;
        private Sunlike.Business.PrdMark _prdMark;
        private Sunlike.Business.Data.DbDRPYH _dbDrpyh;
        private Sunlike.Business.SQNO _sqNo;
        private Sunlike.Business.Auditing _auditing;
        private bool _ifEnterAuditing;
        private bool _isUpdate;
        private bool _isDelete;
        private string _YH_No = "";

        /// <summary>
        /// 要货管理
        /// </summary>
        public DRPYH()
        {
            _dbDrpyh = new Sunlike.Business.Data.DbDRPYH(Comp.Conn_DB);
            _sqNo = new Sunlike.Business.SQNO();
            _user = new Sunlike.Business.Users();
        }

        #region 取要货单号
        /// <summary>
        /// 取要货单号
        /// </summary>
        /// <param name="cus_No">用户编号</param>
        /// <param name="dateTime">打单时间</param>
        /// <returns>要货单号</returns>
        public string GetYH_NO(string cus_No, DateTime dateTime)
        {
            string _yh_No = "";
            try
            {
                string _dept = _user.GetUserDepNo(cus_No);	//取用户部门
                _yh_No = _sqNo.Get("YH", cus_No, _dept, dateTime, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _yh_No;
        }
        #endregion

        #region 取特征分段
        /// <summary>
        /// 取特征分段
        /// </summary>
        /// <returns>用户的编码规则表</returns>
        public System.Data.DataTable PageSetting()
        {
            _prdMark = new Sunlike.Business.PrdMark();
            System.Data.DataTable _dt = null;
            try
            {
                _dt = _prdMark.GetSplitData("");	//取用户分段
            }
            catch (Exception ex)
            {
                _dt = null;
                throw ex;
            }
            return _dt;
        }
        #endregion

        #region	取产品内容
        /// <summary>
        ///	取对应产品内容
        /// </summary>
        /// <param name="prd_No">产品编号</param>
        /// <returns>产品内容表</returns>
        public System.Data.DataTable GetPrdt(string prd_No)
        {
            System.Data.DataTable _dt;
            Sunlike.Business.Prdt _prdt = new Prdt();
            try
            {
                _dt = _prdt.GetPrdt(prd_No);
            }
            catch (Exception ex)
            {
                _dt = null;
                throw ex;
            }
            return _dt;
        }
        #endregion

        #region 按件生成要货单
        /// <summary>
        /// 按件生成要货单
        /// </summary>
        /// <param name="cus_No">用户编号</param>
        /// <param name="usr">制单人</param>
        /// <param name="fx_wh">分销库位</param>
        /// <param name="dateTime">制单日期</param>
        /// <param name="tf_Dyh">要货内容</param>
        /// <param name="est_DD">要货日期</param>
        /// <param name="error"></param>
        /// <param name="dtError"></param>
        public string Insert(string cus_No, string usr, string fx_wh, DateTime dateTime, DateTime est_DD, System.Data.DataTable tf_Dyh, out string error, out DataTable dtError)
        {
            string _yh_No = "";
            DataTable _dtError = null;
            string _error = "";
            _yh_No = this.InsertByBox(cus_No, usr, fx_wh, dateTime, est_DD, tf_Dyh, null, out _error, out _dtError);
            error = _error;
            dtError = _dtError;
            return _yh_No;
        }

        #endregion

        #region 按箱生成要货单
        /// <summary>
        /// 按箱生成要货单
        /// </summary>
        /// <param name="cus_No">用户编号</param>
        /// <param name="usr">制单人</param>
        /// <param name="fx_wh">分销库位</param>
        /// <param name="dateTime">制单日期</param>
        /// <param name="tf_Dyh">要货内容</param>
        /// <param name="tf_DyhBox">箱内容</param>
        /// <param name="est_DD"></param>
        /// <param name="error"></param>
        /// <param name="dtError"></param>
        public string InsertByBox(string cus_No, string usr, string fx_wh, DateTime dateTime, DateTime est_DD, System.Data.DataTable tf_Dyh, System.Data.DataTable tf_DyhBox, out string error, out DataTable dtError)
        {
            for (int itm = 0; itm < tf_Dyh.Rows.Count; itm++)
            {
                tf_Dyh.Rows[itm]["ITM"] = itm + 1;
            }
            if (tf_DyhBox != null)
            {
                for (int itm = 0; itm < tf_DyhBox.Rows.Count; itm++)
                {
                    tf_DyhBox.Rows[itm]["ITM"] = itm + 1;
                }
            }
            Sunlike.Business.WH _clsWH = new WH();
            DataTable _errorDt = null;
            System.Data.DataSet _ds = new System.Data.DataSet("DB_" + Comp.CompNo);
            string _yh_No = "";
            string _errorQTY = "";	//库存量不足的产品代号和库位
            bool _isByBox = false;
            if (tf_DyhBox != null)
                _isByBox = true;
            try
            {
                bool _isOk = true;		//库存量是否足够
                string _dept = _user.GetUserDepNo(usr);	//取用户部门
                _yh_No = _sqNo.Get("YH", usr, _dept, dateTime, "");
                int _tf_DyhColn = tf_Dyh.Columns.Count;
                int _markColn = _tf_DyhColn - 16;
                _ds = _dbDrpyh.GetMT();
                System.Data.DataTable _dtT = _ds.Tables["TF_DYH"];
                System.Data.DataTable _dtT1 = _ds.Tables["TF_DYH1"];
                System.Data.DataTable _dtM = _ds.Tables["MF_DYH"];
                #region //**MF_DYH插入数据**//
                System.Data.DataRow _drM = _dtM.NewRow();
                _drM["YH_ID"] = "YH";
                _drM["YH_NO"] = _yh_No;
                _drM["DEP"] = _dept;
                _drM["YH_DD"] = dateTime;
                _drM["CUS_NO"] = cus_No;
                _drM["USR"] = usr;
                _drM["FX_WH"] = fx_wh;
                _drM["EST_DD"] = est_DD;
                if (tf_DyhBox != null)
                {
                    _drM["BYBOX"] = "T";
                }
                else
                {
                    _drM["BYBOX"] = "F";
                }
                #region 审核流程
                _auditing = new Auditing();
                bool _runAuding = false;
                string _bilType = "";
                string _mobID = "";//支持直接终审mobID=@@ 则单据不跑审核流程
                if (_drM.RowState == DataRowState.Deleted)
                {
                    if (_drM.Table.Columns.Contains("BIL_TYPE"))
                        _bilType = _drM["BIL_TYPE", DataRowVersion.Original].ToString();
                    if (_drM.Table.Columns.Contains("MOB_ID"))
                        _mobID = _drM["MOB_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    if (_drM.Table.Columns.Contains("BIL_TYPE"))
                        _bilType = _drM["BIL_TYPE"].ToString();
                    if (_drM.Table.Columns.Contains("MOB_ID"))
                        _mobID = _drM["MOB_ID"].ToString();
                }
                //_runAuding = _auditing.IsRunAuditing("YH", usr, _bilType,_mobID);

                if (_runAuding)
                    _ifEnterAuditing = true;
                else
                    _ifEnterAuditing = false;
                if (!_ifEnterAuditing)
                {
                    _drM["CLS_ID"] = "F";
                    _drM["CLS_DATE"] = dateTime.ToString();
                    _drM["CHK_MAN"] = usr;
                }
                #endregion
                _dtM.Rows.Add(_drM);
                #endregion
                #region //**TF_DYH插入数据**//
                foreach (DataRow _tf_DyhDr in tf_Dyh.Rows)
                {
                    System.Data.DataRow _drT = _dtT.NewRow();
                    _drT["YH_ID"] = "YH";
                    _drT["YH_NO"] = _yh_No;
                    _drT["ITM"] = Convert.ToInt32(_tf_DyhDr["ITM"]);
                    _drT["PRD_NO"] = _tf_DyhDr["PRD_NO"].ToString();
                    StringBuilder _prd_Mark = new StringBuilder();
                    for (int _i = 0; _i < _markColn; _i = _i + 2)
                    {
                        _prd_Mark.Append(_tf_DyhDr[(_i + 3)].ToString());
                    }
                    _drT["PRD_MARK"] = _prd_Mark.ToString();
                    _drT["WH"] = _tf_DyhDr["WH"].ToString();
                    _drT["EST_DD"] = ((System.DateTime)_tf_DyhDr["EST_DD"]).ToShortDateString();
                    _drT["QTY"] = Convert.ToDecimal(_tf_DyhDr["QTY"]);
                    _drT["UNIT"] = _tf_DyhDr["UNIT"].ToString().Trim();
                    _drT["AMTN"] = Convert.ToDecimal(_tf_DyhDr["AMTN"]);
                    _drT["UP"] = Convert.ToDecimal(_tf_DyhDr["UP"]);
                    if (_isByBox)
                        _drT["BOX_ITM"] = Convert.ToInt32(_tf_DyhDr["BOX_ITM"]);
                    //else
                    //	_drT["BOX_ITM"] = 0;
                    _dtT.Rows.Add(_drT);
                }
                #endregion
                #region //**TF_DYH1插入数据**//
                if (_isByBox)
                {
                    foreach (DataRow _tf_DyhBoxDr in tf_DyhBox.Rows)
                    {
                        DataRow _drT1 = _dtT1.NewRow();
                        _drT1["YH_ID"] = "YH";
                        _drT1["YH_NO"] = _yh_No;
                        _drT1["ITM"] = Convert.ToInt32(_tf_DyhBoxDr["ITM"]);
                        _drT1["PRD_NO"] = _tf_DyhBoxDr["PRD_NO"].ToString();
                        _drT1["CONTENT"] = _tf_DyhBoxDr["CONTENT"].ToString();
                        _drT1["WH"] = _tf_DyhBoxDr["WH"].ToString();
                        _drT1["QTY"] = Convert.ToDecimal(_tf_DyhBoxDr["QTY"]);
                        _drT1["EST_DD"] = ((System.DateTime)_tf_DyhBoxDr["EST_DD"]).ToShortDateString();
                        if (_isByBox)
                            _drT1["KEY_ITM"] = Convert.ToInt32(_tf_DyhBoxDr["KEY_ITM"]);
                        _dtT1.Rows.Add(_drT1);
                    }
                }
                #endregion
                if (Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString() == "F")
                {
                    #region 判断库存量
                    if (_isByBox)
                    {
                        #region 按箱要货
                        bool _whIsOk = false;
                        bool _checkPrdt1 = false;
                        #region 取要货库位
                        string[] _aryWh = null;
                        string _whStr = "";
                        foreach (DataRow _dr in _dtT.Rows)
                        {
                            _whStr = _dr["WH"].ToString();
                            break;
                        }
                        _aryWh = _whStr.Split(new char[] { ';' });
                        #endregion
                        #region 生成tmpTF_DYH
                        DataTable _tmpTF_DYH = new DataTable("tmpTF_DYH");
                        _tmpTF_DYH.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
                        _tmpTF_DYH.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
                        _tmpTF_DYH.Columns.Add("QTY", System.Type.GetType("System.Decimal"));
                        for (int i = 0; i < _aryWh.Length; i++)
                        {
                            _tmpTF_DYH.Columns.Add("WH" + i, System.Type.GetType("System.Decimal"));
                            _tmpTF_DYH.Columns[3 + i].DefaultValue = 0;
                        }
                        #endregion
                        #region 生成tmpBox_Qty(箱的库存量)
                        DataTable _tmpBox_Qty = new DataTable("tmpBox_Qty");
                        _tmpBox_Qty.Columns.Add("BOX_ITM", System.Type.GetType("System.String"));
                        _tmpBox_Qty.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
                        _tmpBox_Qty.Columns.Add("CONTENT", System.Type.GetType("System.String"));
                        _tmpBox_Qty.Columns.Add("QTY", System.Type.GetType("System.Decimal"));
                        _tmpBox_Qty.Columns.Add("OLD_QTY", System.Type.GetType("System.Decimal"));
                        for (int i = 0; i < _aryWh.Length; i++)
                        {
                            _tmpBox_Qty.Columns.Add("WH" + i, System.Type.GetType("System.Decimal"));
                            _tmpBox_Qty.Columns.Add("BOX_WH" + i, System.Type.GetType("System.Decimal"));
                            _tmpBox_Qty.Columns[5 + i * 2].DefaultValue = 0;
                            _tmpBox_Qty.Columns[6 + i * 2].DefaultValue = 0;
                        }
                        #endregion
                        #region 判断库存
                        #region 把货品写入tmpTF_DYH
                        foreach (DataRow _drT1 in _dtT1.Rows)
                        {
                            int _box_Itm = Convert.ToInt32(_drT1["KEY_ITM"]);
                            DataRow[] _drsT = _dtT.Select("BOX_ITM = " + _box_Itm);
                            foreach (DataRow _drT in _drsT)
                            {
                                string _prd_No = _drT["PRD_NO"].ToString();
                                string _prd_Mark = _drT["PRD_MARK"].ToString();
                                DataRow[] _tmpDrs = _tmpTF_DYH.Select("PRD_NO = '" + _prd_No + "' AND PRD_MARK = '" + _prd_Mark + "'");
                                if (_tmpDrs.Length > 0)
                                {
                                    foreach (DataRow _dr in _tmpDrs)
                                    {
                                        _dr["QTY"] = Convert.ToDecimal(_drT["QTY"]) + Convert.ToDecimal(_dr["QTY"]);
                                    }
                                }
                                else
                                {
                                    DataRow _newDr = _tmpTF_DYH.NewRow();
                                    _newDr["PRD_NO"] = _drT["PRD_NO"];
                                    _newDr["PRD_MARK"] = _drT["PRD_MARK"];
                                    _newDr["QTY"] = _drT["QTY"];
                                    _tmpTF_DYH.Rows.Add(_newDr);
                                }
                            }
                            _tmpTF_DYH.AcceptChanges();
                        }
                        #endregion
                        #region 把货品库存里写入tmpTF_DYH
                        if (_checkPrdt1)
                        {
                            foreach (DataRow _drT in _tmpTF_DYH.Rows)
                            {
                                for (int i = 0; i < _aryWh.Length; i++)
                                {
                                    if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                                    {
                                        //_drT["WH" + i.ToString()] = _clsWH.GetQty(false,_drT["PRD_NO"].ToString() , _drT["PRD_MARK"].ToString() , _aryWh[i]);
                                        _drT["WH" + i.ToString()] = _clsWH.GetQty(false, _drT["PRD_NO"].ToString(), _drT["PRD_MARK"].ToString(), _aryWh[i], String.Empty);
                                    }
                                    else
                                    {
                                        //_drT["WH" + i.ToString()] = _clsWH.GetQty(true,_drT["PRD_NO"].ToString() , _drT["PRD_MARK"].ToString() , _aryWh[i]);
                                        _drT["WH" + i.ToString()] = _clsWH.GetQty(true, _drT["PRD_NO"].ToString(), _drT["PRD_MARK"].ToString(), _aryWh[i], String.Empty);
                                    }
                                }
                            }
                            _tmpTF_DYH.AcceptChanges();
                        }
                        #endregion
                        #region 写入箱的库存量(写入箱内容)
                        foreach (DataRow _dr in tf_DyhBox.Rows)
                        {
                            DataRow _newDr = _tmpBox_Qty.NewRow();
                            //_newDr["BOX_ITM"] = _dr["ITM"].ToString();
                            _newDr["BOX_ITM"] = _dr["KEY_ITM"].ToString();
                            _newDr["PRD_NO"] = _dr["PRD_NO"].ToString();
                            _newDr["CONTENT"] = _dr["CONTENT"].ToString();
                            _newDr["QTY"] = _dr["QTY"].ToString();
                            _newDr["OLD_QTY"] = _dr["QTY"].ToString();
                            int _box_Itm = Convert.ToInt32(_dr["KEY_ITM"]);
                            decimal _qty = Convert.ToDecimal(_dr["QTY"]);
                            #region 取库位箱的库存量
                            for (int i = 0; i < _aryWh.Length; i++)
                            {
                                if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                                    _newDr["BOX_WH" + i.ToString()] = _clsWH.GetBoxQty(false, _dr["PRD_NO"].ToString(), _aryWh[i], _dr["CONTENT"].ToString());
                                else
                                    _newDr["BOX_WH" + i.ToString()] = _clsWH.GetBoxQty(true, _dr["PRD_NO"].ToString(), _aryWh[i], _dr["CONTENT"].ToString());
                            }
                            #endregion
                            if (_checkPrdt1)
                            {
                                #region 取库位件的箱的库存量
                                bool _isFirst = true;
                                DataRow[] _drsT = _dtT.Select("BOX_ITM = " + _box_Itm);
                                foreach (DataRow _drT in _drsT)
                                {
                                    decimal _prd_Qty = Convert.ToDecimal(_drT["QTY"]) / _qty;	//比率
                                    string _prd_No = _drT["PRD_NO"].ToString();
                                    string _prd_Mark = _drT["PRD_MARK"].ToString();
                                    decimal _box_Wh_Qty = 0;
                                    DataRow[] _prd_Wh_QtyDrs = _tmpTF_DYH.Select("PRD_NO = '" + _prd_No + "' AND PRD_MARK = '" + _prd_Mark + "'");
                                    foreach (DataRow _prd_Wh_QtyDr in _prd_Wh_QtyDrs)
                                    {
                                        for (int i = 0; i < _aryWh.Length; i++)
                                        {
                                            _box_Wh_Qty = Convert.ToInt32(Convert.ToInt32(_prd_Wh_QtyDr["WH" + i.ToString()]) / Convert.ToInt32(_prd_Qty));
                                            if (_isFirst)
                                            {
                                                _newDr["WH" + i.ToString()] = _box_Wh_Qty;
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(_newDr["WH" + i.ToString()]) > _box_Wh_Qty)
                                                {
                                                    _newDr["WH" + i.ToString()] = _box_Wh_Qty;
                                                }
                                            }
                                            _prd_Wh_QtyDr["WH" + i.ToString()] = Convert.ToDecimal(_prd_Wh_QtyDr["WH" + i.ToString()]) - Convert.ToDecimal(_drT["QTY"]);
                                            _tmpTF_DYH.AcceptChanges();
                                        }
                                        _isFirst = false;
                                    }
                                }
                                #endregion
                            }
                            _tmpBox_Qty.Rows.Add(_newDr);
                        }
                        _tmpBox_Qty.AcceptChanges();
                        #endregion
                        #region 判断要货的库位(重写TF_DYH)
                        #region 清空TF_DYH
                        DataRow[] _drTAdd = _dtT.Select("", "", System.Data.DataViewRowState.Added);
                        foreach (DataRow _drT in _drTAdd)
                        {
                            _drT.Delete();
                        }
                        _dtT.AcceptChanges();
                        #endregion
                        //foreach(DataRow _tmpBox_QtyDr in _tmpBox_Qty.Rows)
                        DataRow[] _tmpBox_QtyDrs = _tmpBox_Qty.Select();
                        foreach (DataRow _tmpBox_QtyDr in _tmpBox_QtyDrs)
                        {
                            decimal _total_Box_Wh_Qty = 0;
                            bool _boxIsEnough = false;
                            string _box_Itm = _tmpBox_QtyDr["BOX_ITM"].ToString();
                            string _prd_No = _tmpBox_QtyDr["PRD_NO"].ToString();
                            string _content = _tmpBox_QtyDr["CONTENT"].ToString();
                            #region 判断是否箱库存足够
                            for (int i = 0; i < _aryWh.Length; i++)
                            {
                                _total_Box_Wh_Qty += Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]);
                            }
                            if (_total_Box_Wh_Qty >= Convert.ToDecimal(_tmpBox_QtyDr["QTY"]))
                                _boxIsEnough = true;
                            #endregion
                            if (_boxIsEnough)
                            {
                                #region 按箱来取库位
                                string _wh = "";
                                for (int i = 0; i < _aryWh.Length; i++)
                                {
                                    _wh = _aryWh[i].ToString();
                                    if (Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) <= Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]))
                                    {
                                        #region 修改TF_DYH1的库位
                                        DataRow[] _update_drsT1 = _dtT1.Select("KEY_ITM = " + _box_Itm);
                                        foreach (DataRow _update_drT1 in _update_drsT1)
                                        {
                                            _update_drT1["WH"] = _wh;
                                        }
                                        #endregion
                                        #region 写入要货货品内容
                                        DataRow[] _prdtDrs = tf_Dyh.Select("BOX_ITM = '" + _tmpBox_QtyDr["BOX_ITM"].ToString() + "'");
                                        foreach (DataRow _prdtDr in _prdtDrs)
                                        {
                                            DataRow _drTNew = _dtT.NewRow();
                                            _drTNew["YH_ID"] = "YH";
                                            _drTNew["YH_NO"] = _yh_No;
                                            _drTNew["ITM"] = _dtT.Rows.Count + 1;
                                            _drTNew["PRD_NO"] = _prdtDr["PRD_NO"].ToString();
                                            StringBuilder _prd_Mark = new StringBuilder();
                                            for (int _i = 0; _i < _markColn; _i = _i + 2)
                                            {
                                                _prd_Mark.Append(_prdtDr[(_i + 3)].ToString());
                                            }
                                            _drTNew["PRD_MARK"] = _prd_Mark.ToString();
                                            _drTNew["WH"] = _wh;
                                            _drTNew["EST_DD"] = ((System.DateTime)_prdtDr["EST_DD"]).ToShortDateString();
                                            _drTNew["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"]));
                                            _drTNew["UNIT"] = _prdtDr["UNIT"].ToString().Trim();
                                            _drTNew["AMTN"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"])) * Convert.ToDecimal(_prdtDr["UP"]);
                                            _drTNew["UP"] = Convert.ToDecimal(_prdtDr["UP"]);
                                            _drTNew["BOX_ITM"] = Convert.ToInt32(_prdtDr["BOX_ITM"]);
                                            _dtT.Rows.Add(_drTNew);
                                        }
                                        break;
                                        #endregion
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]) > 0)
                                        {
                                            string _new_Box_Itm = "";
                                            #region 修改TF_DYH1的库位和数量
                                            DataRow[] _update_drsT1 = _dtT1.Select("KEY_ITM = " + _box_Itm);
                                            foreach (DataRow _update_drT1 in _update_drsT1)
                                            {
                                                _update_drT1["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) - Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]);
                                                #region 新增TF_DYH1箱内容
                                                _new_Box_Itm = Convert.ToString(_dtT1.Select("", "", System.Data.DataViewRowState.Added).Length + 1);
                                                DataRow _new_drT1 = _dtT1.NewRow();
                                                _new_drT1["YH_ID"] = "YH";
                                                _new_drT1["YH_NO"] = _update_drT1["YH_NO"];
                                                _new_drT1["ITM"] = _dtT1.Select("", "", System.Data.DataViewRowState.Added).Length + 1;
                                                _new_drT1["PRD_NO"] = _update_drT1["PRD_NO"];
                                                _new_drT1["CONTENT"] = _update_drT1["CONTENT"];
                                                _new_drT1["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]);
                                                _new_drT1["KEY_ITM"] = _dtT1.Select("", "", System.Data.DataViewRowState.Added).Length + 1;
                                                _new_drT1["WH"] = _wh;
                                                _dtT1.Rows.Add(_new_drT1);
                                                #endregion
                                            }
                                            #endregion
                                            _tmpBox_QtyDr["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) - Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]);
                                            #region 写入要货货品内容
                                            DataRow[] _prdtDrs = tf_Dyh.Select("BOX_ITM = '" + _tmpBox_QtyDr["BOX_ITM"].ToString() + "'");
                                            foreach (DataRow _prdtDr in _prdtDrs)
                                            {
                                                DataRow _drTNew = _dtT.NewRow();
                                                _drTNew["YH_ID"] = "YH";
                                                _drTNew["YH_NO"] = _yh_No;
                                                _drTNew["ITM"] = _dtT.Rows.Count + 1;
                                                _drTNew["PRD_NO"] = _prdtDr["PRD_NO"].ToString();
                                                StringBuilder _prd_Mark = new StringBuilder();
                                                for (int _i = 0; _i < _markColn; _i = _i + 2)
                                                {
                                                    _prd_Mark.Append(_prdtDr[(_i + 3)].ToString());
                                                }
                                                _drTNew["PRD_MARK"] = _prd_Mark;
                                                _drTNew["WH"] = _wh;
                                                _drTNew["EST_DD"] = ((System.DateTime)_prdtDr["EST_DD"]).ToShortDateString();
                                                _drTNew["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"]));
                                                _drTNew["UNIT"] = _prdtDr["UNIT"].ToString().Trim();
                                                _drTNew["AMTN"] = Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"])) * Convert.ToDecimal(_prdtDr["UP"]);
                                                _drTNew["UP"] = Convert.ToDecimal(_prdtDr["UP"]);
                                                _drTNew["BOX_ITM"] = Convert.ToInt32(_new_Box_Itm);
                                                _dtT.Rows.Add(_drTNew);
                                            }
                                            #endregion
                                            _tmpBox_Qty.AcceptChanges();
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                if (!_checkPrdt1)
                                {
                                    _errorQTY += _prd_No + ":" + _content + ";";
                                }
                                else
                                {
                                    #region 按件来取库位
                                    _total_Box_Wh_Qty = 0;
                                    for (int i = 0; i < _aryWh.Length; i++)
                                    {
                                        _total_Box_Wh_Qty += Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]);
                                    }
                                    if (_total_Box_Wh_Qty >= Convert.ToDecimal(_tmpBox_QtyDr["QTY"]))
                                    {
                                        string _wh = "";
                                        for (int i = 0; i < _aryWh.Length; i++)
                                        {
                                            _wh = _aryWh[i].ToString();
                                            if (Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) <= Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]))
                                            {
                                                #region 修改TF_DYH1的库位
                                                DataRow[] _update_drsT1 = _dtT1.Select("ITM = " + _box_Itm);
                                                foreach (DataRow _update_drT1 in _update_drsT1)
                                                {
                                                    _update_drT1["WH"] = _wh;
                                                }
                                                #endregion
                                                #region 写入要货货品内容
                                                DataRow[] _prdtDrs = tf_Dyh.Select("BOX_ITM = '" + _tmpBox_QtyDr["BOX_ITM"].ToString() + "'");
                                                foreach (DataRow _prdtDr in _prdtDrs)
                                                {
                                                    DataRow _drTNew = _dtT.NewRow();
                                                    _drTNew["YH_ID"] = "YH";
                                                    _drTNew["YH_NO"] = _yh_No;
                                                    _drTNew["ITM"] = _dtT.Rows.Count + 1;
                                                    _drTNew["PRD_NO"] = _prdtDr["PRD_NO"].ToString();
                                                    string _prd_Mark = "";
                                                    for (int _i = 0; _i < _markColn; _i = _i + 2)
                                                    {
                                                        _prd_Mark = _prd_Mark + _prdtDr[(_i + 3)].ToString();
                                                    }
                                                    _drTNew["PRD_MARK"] = _prd_Mark;
                                                    _drTNew["WH"] = _wh;
                                                    _drTNew["EST_DD"] = ((System.DateTime)_prdtDr["EST_DD"]).ToShortDateString();
                                                    _drTNew["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"]));
                                                    _drTNew["UNIT"] = _prdtDr["UNIT"].ToString().Trim();
                                                    _drTNew["AMTN"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"])) * Convert.ToDecimal(_prdtDr["UP"]);
                                                    _drTNew["UP"] = Convert.ToDecimal(_prdtDr["UP"]);
                                                    _drTNew["BOX_ITM"] = Convert.ToInt32(_prdtDr["BOX_ITM"]);
                                                    _dtT.Rows.Add(_drTNew);
                                                }
                                                break;
                                                #endregion
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]) > 0)
                                                {
                                                    string _new_Box_Itm = "";
                                                    #region 修改TF_DYH1的库位和数量
                                                    DataRow[] _update_drsT1 = _dtT1.Select("ITM = " + _box_Itm);
                                                    foreach (DataRow _update_drT1 in _update_drsT1)
                                                    {
                                                        _update_drT1["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) - Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]);
                                                        #region 新增TF_DYH1箱内容
                                                        _new_Box_Itm = Convert.ToString(_dtT1.Select("", "", System.Data.DataViewRowState.Added).Length + 1);
                                                        DataRow _new_drT1 = _dtT1.NewRow();
                                                        _new_drT1["YH_ID"] = "YH";
                                                        _new_drT1["YH_NO"] = _update_drT1["YH_NO"];
                                                        _new_drT1["ITM"] = _dtT1.Select("", "", System.Data.DataViewRowState.Added).Length + 1;
                                                        _new_drT1["PRD_NO"] = _update_drT1["PRD_NO"];
                                                        _new_drT1["CONTENT"] = _update_drT1["CONTENT"];
                                                        _new_drT1["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]);
                                                        _new_drT1["KEY_ITM"] = _dtT1.Select("", "", System.Data.DataViewRowState.Added).Length + 1;
                                                        _new_drT1["WH"] = _wh;
                                                        _dtT1.Rows.Add(_new_drT1);
                                                        #endregion
                                                    }
                                                    #endregion
                                                    _tmpBox_QtyDr["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) - Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]);
                                                    #region 写入要货货品内容
                                                    DataRow[] _prdtDrs = tf_Dyh.Select("BOX_ITM = '" + _tmpBox_QtyDr["BOX_ITM"].ToString() + "'");
                                                    foreach (DataRow _prdtDr in _prdtDrs)
                                                    {
                                                        DataRow _drTNew = _dtT.NewRow();
                                                        _drTNew["YH_ID"] = "YH";
                                                        _drTNew["YH_NO"] = _yh_No;
                                                        _drTNew["ITM"] = _dtT.Rows.Count + 1;
                                                        _drTNew["PRD_NO"] = _prdtDr["PRD_NO"].ToString();
                                                        string _prd_Mark = "";
                                                        for (int _i = 0; _i < _markColn; _i = _i + 2)
                                                        {
                                                            _prd_Mark = _prd_Mark + _prdtDr[(_i + 3)].ToString();
                                                        }
                                                        _drTNew["PRD_MARK"] = _prd_Mark;
                                                        _drTNew["WH"] = _wh;
                                                        _drTNew["EST_DD"] = ((System.DateTime)_prdtDr["EST_DD"]).ToShortDateString();
                                                        _drTNew["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"]));
                                                        _drTNew["UNIT"] = _prdtDr["UNIT"].ToString().Trim();
                                                        _drTNew["AMTN"] = Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"])) * Convert.ToDecimal(_prdtDr["UP"]);
                                                        _drTNew["UP"] = Convert.ToDecimal(_prdtDr["UP"]);
                                                        _drTNew["BOX_ITM"] = Convert.ToInt32(_new_Box_Itm);
                                                        _dtT.Rows.Add(_drTNew);
                                                    }
                                                    #endregion
                                                    _tmpBox_Qty.AcceptChanges();
                                                }
                                            }
                                        }
                                        _whIsOk = true;
                                    }
                                    else
                                    {
                                        _whIsOk = false;
                                    }
                                    if (!_whIsOk)
                                    {
                                        _errorQTY += _prd_No + ":" + _content + ";";
                                    }
                                    #endregion
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(_errorQTY))
                            _isOk = false;
                        else
                            _isOk = true;
                        #endregion
                        #region end
                        /*
						foreach(DataRow _drT1 in _dtT1.Rows)
						{
							bool _whIsOk = false;
							bool[,] _aryWhIsOk = null;
							string[] _aryWH = null;
							string _prd_No = _drT1["PRD_NO"].ToString();
							string _content = _drT1["CONTENT"].ToString();
							string _whs = "";
							int _box_Itm = Convert.ToInt32(_drT1["KEY_ITM"]);
							DataRow[] _drsT = _dtT.Select("BOX_ITM = " + _box_Itm);
							int _prd_Itm = 0;
							int _wh_Itm = 0;
							foreach(DataRow _drT in _drsT)
							{
						#region 要货库位数量
								_aryWH = _drT["WH"].ToString().Split(new char[]{';'});
								decimal[] _aryWH_QTY = new decimal[_aryWH.Length];
								if(_aryWhIsOk == null)
									_aryWhIsOk = new bool[_drsT.Length,_aryWH.Length];
								int _i = 0;
								foreach(string _wh in _aryWH)
								{
									_aryWH_QTY[_i] = _clsWH.GetWH_QTY(_drT["PRD_NO"].ToString() , _drT["PRD_MARK"].ToString() , _wh);
									_i++;
								}
								for(int j=0;j<_aryWH.Length;j++)
								{
									if(_aryWH_QTY[j] >= Convert.ToDecimal(_drT["QTY"]))
									{
										_aryWhIsOk[_prd_Itm , j] = true;
										//_whIsOk = true;
										//_drT["WH"] = _aryWH[j];
									}
									else
									{
										_aryWhIsOk[_prd_Itm , j] = false;
										//_whIsOk = false;
										//_whs = _drT["WH"].ToString();
									}
								}
								_prd_Itm++;
						#endregion
							}
						#region 判断那个库位数量足够
							_whIsOk = false;
							for(int wh_Itm=0;wh_Itm<_aryWH.Length;wh_Itm++)
							{
								for(int prd_Itm=0;prd_Itm<_drsT.Length;prd_Itm++)
								{
									if(((bool)_aryWhIsOk[prd_Itm , wh_Itm]))
									{
										_whIsOk = true;
									}
									else
									{
										_whIsOk = false;
										break;
									}
								}
								if(_whIsOk)
								{
									_wh_Itm = wh_Itm;
									break;
								}
							}
						#endregion
						#region 更新TF_DYH
							foreach(DataRow _drT in _drsT)
							{
								_drT["WH"] = _aryWH[_wh_Itm];
							}
						#endregion
							if(! _whIsOk)
							{
								_errorQTY += _prd_No + ":" + _content + ":" + _whs + ";";
							}
						}
						if(!String.IsNullOrEmpty(_errorQTY))
							_isOk = false;
						else
							_isOk = true;*/
                        #endregion
                        #endregion
                        #endregion
                    }
                    else
                    {
                        #region 按件要货
                        //foreach (DataRow _drTRow in _dtT.Rows)
                        int _intDtTRow = _dtT.Rows.Count;
                        for (int idtT = 0; idtT < _intDtTRow; idtT++)
                        {
                            DataRow _drTRow = _dtT.Rows[idtT];
                            bool _whIsOk = false;
                            string[] _aryWH = _drTRow["WH"].ToString().Split(new char[] { ';' });
                            decimal[] _aryWH_QTY = new decimal[_aryWH.Length];
                            string _prd_No = _drTRow["PRD_NO"].ToString();
                            string _prd_Mark = _drTRow["PRD_MARK"].ToString();
                            DateTime _est_DD = Convert.ToDateTime(_drTRow["EST_DD"]);
                            string _unit = _drTRow["UNIT"].ToString();
                            decimal _up = Convert.ToDecimal(_drTRow["UP"]);
                            decimal _yh_QTY = Convert.ToDecimal(_drTRow["QTY"]);
                            decimal _wh_QTY = 0;
                            StringBuilder _whs = new StringBuilder();
                            int _i = 0;
                            #region 要货库位数量
                            foreach (string _wh in _aryWH)
                            {
                                if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                                {
                                    //_aryWH_QTY[_i] = _clsWH.GetQty(false,_prd_No , _prd_Mark , _wh);
                                    _aryWH_QTY[_i] = _clsWH.GetQty(false, _prd_No, _prd_Mark, _wh, String.Empty);
                                }
                                else
                                {
                                    //_aryWH_QTY[_i] = _clsWH.GetQty(false,_prd_No , _prd_Mark , _wh);
                                    _aryWH_QTY[_i] = _clsWH.GetQty(false, _prd_No, _prd_Mark, _wh, String.Empty);
                                }
                                _wh_QTY += _aryWH_QTY[_i];
                                _i++;
                            }
                            #endregion
                            if (_wh_QTY >= _yh_QTY)
                            {
                                #region 数量足够
                                if (_aryWH_QTY[0] >= _yh_QTY)
                                {
                                    _drTRow["WH"] = _aryWH[0];
                                }
                                else
                                {
                                    if (_aryWH_QTY[0] > 0)
                                    {
                                        #region 更新
                                        _drTRow["WH"] = _aryWH[0];
                                        _drTRow["QTY"] = _aryWH_QTY[0];
                                        _drTRow["AMTN"] = _aryWH_QTY[0] * _up;
                                        #endregion
                                        #region 新增
                                        DataRow _newRow = _dtT.NewRow();
                                        _newRow["YH_ID"] = "YH";
                                        _newRow["YH_NO"] = _yh_No;
                                        _newRow["ITM"] = _dtT.Rows.Count + 1;
                                        _newRow["PRD_NO"] = _prd_No;
                                        _newRow["PRD_MARK"] = _prd_Mark;
                                        _newRow["WH"] = _aryWH[1];
                                        _newRow["EST_DD"] = _est_DD;
                                        _newRow["QTY"] = _yh_QTY - _aryWH_QTY[0];
                                        _newRow["UNIT"] = _unit;
                                        _newRow["AMTN"] = Convert.ToDecimal(_yh_QTY - _aryWH_QTY[0]) * _up;
                                        _newRow["UP"] = _up;
                                        _dtT.Rows.Add(_newRow);
                                        #endregion
                                    }
                                    else
                                    {
                                        #region 更新
                                        _drTRow["WH"] = _aryWH[1];
                                        #endregion
                                    }
                                }
                                _whIsOk = true;
                                #endregion
                            }
                            else
                            {
                                #region 数量不足
                                _whIsOk = false;
                                _whs.Append(_drTRow["WH"].ToString());
                                #endregion
                            }
                            if (!_whIsOk)
                            {
                                _isOk = false;
                                _errorQTY += _prd_No + ":" + _prd_Mark + ":" + _whs + ";";
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 库存量
                    foreach (DataRow _drTRow in _dtT.Rows)
                    {
                        string[] _aryWH = _drTRow["WH"].ToString().Split(new char[] { ';' });
                        _drTRow["WH"] = _aryWH[0].ToString();
                    }
                    foreach (DataRow _drT1Row in _dtT1.Rows)
                    {
                        string[] _aryWH = _drT1Row["WH"].ToString().Split(new char[] { ';' });
                        _drT1Row["WH"] = _aryWH[0].ToString();
                    }
                    #endregion
                }
                if (_isOk)
                {
                    _yh_No = _sqNo.Set("YH", usr, _dept, dateTime, "FX");
                    foreach (DataRow _dr in _dtM.Rows)
                    {
                        _dr["YH_NO"] = _yh_No;
                    }
                    foreach (DataRow _dr in _dtT.Rows)
                    {
                        _dr["YH_NO"] = _yh_No;
                    }
                    foreach (DataRow _dr in _dtT1.Rows)
                    {
                        _dr["YH_NO"] = _yh_No;
                    }
                    this.UpdateDataSet(_ds);
                    _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
                }
            }
            catch//(Exception ex)
            {
                //throw ex;
                //				_errorQTY = ex.Message.ToString();
            }
            finally
            {
                _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
            }
            dtError = _errorDt;
            error = _errorQTY;
            return _yh_No;
        }
        #endregion

        #region 更新按箱要货单
        /// <summary>
        /// 更新按箱要货单
        /// </summary>
        /// <param name="fx_wh"></param>
        /// <param name="errorDt"></param>
        /// <param name="yh_ID">要货编号</param>
        /// <param name="yh_NO">要货单号</param>
        /// <param name="tf_Dyh">要货单内容</param>
        /// <param name="tf_DyhBox">箱内容</param>
        /// <param name="est_DD"></param>
        public string UpdateByBox(string yh_ID, string yh_NO, string fx_wh, DateTime est_DD, DataTable tf_Dyh, DataTable tf_DyhBox, out DataTable errorDt)
        {
            if (tf_Dyh != null)
            {
                for (int itm = 0; itm < tf_Dyh.Rows.Count; itm++)
                {
                    tf_Dyh.Rows[itm]["ITM"] = itm + 1;
                }
            }
            if (tf_DyhBox != null)
            {
                for (int itm = 0; itm < tf_DyhBox.Rows.Count; itm++)
                {
                    tf_DyhBox.Rows[itm]["ITM"] = itm + 1;
                }
            }
            DataTable _errorDt = null;
            DataTable _tmp_tf_Dyh = tf_Dyh.Copy();
            DataTable _tmp_tf_DyhBox = null;
            if (tf_DyhBox != null)
                _tmp_tf_DyhBox = tf_DyhBox.Copy();
            Sunlike.Business.WH _clsWH = new WH();
            Sunlike.Business.PrdMark _prdMark = new PrdMark();
            DataTable _dtMark = _prdMark.GetSplitData("");
            bool _isByBox = false;
            if (tf_DyhBox != null)
                _isByBox = true;
            string _errorQTY = "";	//库存量不足的产品代号和库位
            try
            {
                bool _isOk = true;		//库存量是否足够
                if (Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString() == "F")
                {
                    int _tf_DyhColn = tf_Dyh.Columns.Count;
                    int _markColn = _tf_DyhColn - 17;
                    #region 判断库存量
                    if (_isByBox)
                    {
                        #region 按箱要货
                        bool _whIsOk = false;
                        bool _checkPrdt1 = false;
                        #region 取要货库位
                        string[] _aryWh = null;
                        string _whStr = "";
                        foreach (DataRow _dr in tf_Dyh.Rows)
                        {
                            _whStr = _dr["WH"].ToString();
                            break;
                        }
                        _aryWh = _whStr.Split(new char[] { ';' });
                        #endregion
                        #region 生成tmpTF_DYH
                        DataTable _tmpTF_DYH = new DataTable("tmpTF_DYH");
                        _tmpTF_DYH.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
                        _tmpTF_DYH.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
                        _tmpTF_DYH.Columns.Add("QTY", System.Type.GetType("System.Decimal"));
                        for (int i = 0; i < _aryWh.Length; i++)
                        {
                            _tmpTF_DYH.Columns.Add("WH" + i, System.Type.GetType("System.Decimal"));
                            _tmpTF_DYH.Columns[3 + i].DefaultValue = 0;
                        }
                        #endregion
                        #region 生成tmpBox_Qty(箱的库存量)
                        DataTable _tmpBox_Qty = new DataTable("tmpBox_Qty");
                        _tmpBox_Qty.Columns.Add("BOX_ITM", System.Type.GetType("System.String"));
                        _tmpBox_Qty.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
                        _tmpBox_Qty.Columns.Add("CONTENT", System.Type.GetType("System.String"));
                        _tmpBox_Qty.Columns.Add("QTY", System.Type.GetType("System.Decimal"));
                        _tmpBox_Qty.Columns.Add("OLD_QTY", System.Type.GetType("System.Decimal"));
                        for (int i = 0; i < _aryWh.Length; i++)
                        {
                            _tmpBox_Qty.Columns.Add("WH" + i, System.Type.GetType("System.Decimal"));
                            _tmpBox_Qty.Columns.Add("BOX_WH" + i, System.Type.GetType("System.Decimal"));
                            _tmpBox_Qty.Columns[5 + i * 2].DefaultValue = 0;
                            _tmpBox_Qty.Columns[6 + i * 2].DefaultValue = 0;
                        }
                        #endregion
                        #region 把货品写入tmpTF_DYH
                        foreach (DataRow _drT1 in tf_DyhBox.Rows)
                        {
                            int _box_Itm = Convert.ToInt32(_drT1["KEY_ITM"]);
                            DataRow[] _drsT = tf_Dyh.Select("BOX_ITM = " + _box_Itm);
                            foreach (DataRow _drT in _drsT)
                            {
                                string _prd_No = _drT["PRD_NO"].ToString();
                                StringBuilder _prd_Mark = new StringBuilder();
                                for (int _j = 0; _j < _markColn; _j = _j + 2)
                                {
                                    _prd_Mark.Append(_drT[(_j + 3)].ToString());
                                }
                                DataRow[] _tmpDrs = _tmpTF_DYH.Select("PRD_NO = '" + _prd_No + "' AND PRD_MARK = '" + _prd_Mark + "'");
                                if (_tmpDrs.Length > 0)
                                {
                                    foreach (DataRow _dr in _tmpDrs)
                                    {
                                        _dr["QTY"] = Convert.ToDecimal(_drT["QTY"]) + Convert.ToDecimal(_dr["QTY"]);
                                    }
                                }
                                else
                                {
                                    DataRow _newDr = _tmpTF_DYH.NewRow();
                                    _newDr["PRD_NO"] = _drT["PRD_NO"];
                                    _newDr["PRD_MARK"] = _prd_Mark;
                                    _newDr["QTY"] = _drT["QTY"];
                                    _tmpTF_DYH.Rows.Add(_newDr);
                                }
                            }
                            _tmpTF_DYH.AcceptChanges();
                        }
                        #endregion
                        #region 把货品库存里写入tmpTF_DYH
                        if (_checkPrdt1)
                        {
                            foreach (DataRow _drT in _tmpTF_DYH.Rows)
                            {
                                for (int i = 0; i < _aryWh.Length; i++)
                                {
                                    if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                                    {
                                        //_drT["WH" + i.ToString()] = _clsWH.GetQty(false,_drT["PRD_NO"].ToString() , _drT["PRD_MARK"].ToString() , _aryWh[i]);
                                        _drT["WH" + i.ToString()] = _clsWH.GetQty(false, _drT["PRD_NO"].ToString(), _drT["PRD_MARK"].ToString(), _aryWh[i], String.Empty);
                                    }
                                    else
                                    {
                                        //_drT["WH" + i.ToString()] = _clsWH.GetQty(false,_drT["PRD_NO"].ToString() , _drT["PRD_MARK"].ToString() , _aryWh[i]);
                                        _drT["WH" + i.ToString()] = _clsWH.GetQty(false, _drT["PRD_NO"].ToString(), _drT["PRD_MARK"].ToString(), _aryWh[i], String.Empty);
                                    }
                                }
                            }
                            _tmpTF_DYH.AcceptChanges();
                        }
                        #endregion
                        #region 写入箱的库存量(写入箱内容)
                        foreach (DataRow _dr in tf_DyhBox.Rows)
                        {
                            DataRow _newDr = _tmpBox_Qty.NewRow();
                            //_newDr["BOX_ITM"] = _dr["ITM"].ToString();
                            _newDr["BOX_ITM"] = _dr["KEY_ITM"].ToString();
                            _newDr["PRD_NO"] = _dr["PRD_NO"].ToString();
                            _newDr["CONTENT"] = _dr["CONTENT"].ToString();
                            _newDr["QTY"] = _dr["QTY"].ToString();
                            _newDr["OLD_QTY"] = _dr["QTY"].ToString();
                            int _box_Itm = Convert.ToInt32(_dr["KEY_ITM"]);
                            decimal _qty = Convert.ToDecimal(_dr["QTY"]);
                            #region 取库位箱的库存量
                            for (int i = 0; i < _aryWh.Length; i++)
                            {
                                if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                                    _newDr["BOX_WH" + i.ToString()] = _clsWH.GetBoxQty(false, _dr["PRD_NO"].ToString(), _aryWh[i], _dr["CONTENT"].ToString());
                                else
                                    _newDr["BOX_WH" + i.ToString()] = _clsWH.GetBoxQty(true, _dr["PRD_NO"].ToString(), _aryWh[i], _dr["CONTENT"].ToString());
                            }
                            #endregion
                            if (_checkPrdt1)
                            {
                                #region 取库位件的箱库存量
                                bool _isFirst = true;
                                DataRow[] _drsT = tf_Dyh.Select("BOX_ITM = " + _box_Itm);
                                foreach (DataRow _drT in _drsT)
                                {
                                    decimal _prd_Qty = Convert.ToDecimal(_drT["QTY"]) / _qty;	//比率
                                    string _prd_No = _drT["PRD_NO"].ToString();
                                    string _prd_Mark = "";
                                    for (int _j = 0; _j < _markColn; _j = _j + 2)
                                    {
                                        _prd_Mark = _prd_Mark + _drT[(_j + 3)].ToString();
                                    }
                                    decimal _box_Wh_Qty = 0;
                                    DataRow[] _prd_Wh_QtyDrs = _tmpTF_DYH.Select("PRD_NO = '" + _prd_No + "' AND PRD_MARK = '" + _prd_Mark + "'");
                                    foreach (DataRow _prd_Wh_QtyDr in _prd_Wh_QtyDrs)
                                    {
                                        for (int i = 0; i < _aryWh.Length; i++)
                                        {
                                            _box_Wh_Qty = Convert.ToInt32(Convert.ToInt32(_prd_Wh_QtyDr["WH" + i.ToString()]) / Convert.ToInt32(_prd_Qty));
                                            if (_isFirst)
                                            {
                                                _newDr["WH" + i.ToString()] = _box_Wh_Qty;
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(_newDr["WH" + i.ToString()]) > _box_Wh_Qty)
                                                {
                                                    _newDr["WH" + i.ToString()] = _box_Wh_Qty;
                                                }
                                            }
                                            _prd_Wh_QtyDr["WH" + i.ToString()] = Convert.ToDecimal(_prd_Wh_QtyDr["WH" + i.ToString()]) - Convert.ToDecimal(_drT["QTY"]);
                                            _tmpTF_DYH.AcceptChanges();
                                        }
                                        _isFirst = false;
                                    }
                                }
                                #endregion
                            }
                            _tmpBox_Qty.Rows.Add(_newDr);
                        }
                        _tmpBox_Qty.AcceptChanges();
                        #endregion
                        #region 判断要货的库位(重写TF_DYH)
                        #region 清空TF_DYH
                        DataTable _tf_Dyh_Tmp = tf_Dyh.Copy();
                        DataRow[] _drTAdd = tf_Dyh.Select();
                        foreach (DataRow _drT in _drTAdd)
                        {
                            _drT.Delete();
                        }
                        tf_Dyh.AcceptChanges();
                        #endregion
                        foreach (DataRow _tmpBox_QtyDr in _tmpBox_Qty.Rows)
                        {
                            decimal _total_Box_Wh_Qty = 0;
                            bool _boxIsEnough = false;
                            string _box_Itm = _tmpBox_QtyDr["BOX_ITM"].ToString();
                            string _prd_No = _tmpBox_QtyDr["PRD_NO"].ToString();
                            string _content = _tmpBox_QtyDr["CONTENT"].ToString();
                            #region 判断是否箱库存足够
                            for (int i = 0; i < _aryWh.Length; i++)
                            {
                                _total_Box_Wh_Qty += Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]);
                            }
                            if (_total_Box_Wh_Qty >= Convert.ToDecimal(_tmpBox_QtyDr["QTY"]))
                                _boxIsEnough = true;
                            #endregion
                            if (_boxIsEnough)
                            {
                                #region 按箱来取库位
                                string _wh = "";
                                for (int i = 0; i < _aryWh.Length; i++)
                                {
                                    _wh = _aryWh[i].ToString();
                                    if (Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) <= Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]))
                                    {
                                        #region 修改TF_DYH1的库位
                                        DataRow[] _update_drsT1 = tf_DyhBox.Select("ITM = " + _box_Itm);
                                        foreach (DataRow _update_drT1 in _update_drsT1)
                                        {
                                            _update_drT1["WH"] = _wh;
                                        }
                                        #endregion
                                        #region 写入要货货品内容
                                        DataRow[] _prdtDrs = _tf_Dyh_Tmp.Select("BOX_ITM = '" + _tmpBox_QtyDr["BOX_ITM"].ToString() + "'");
                                        foreach (DataRow _prdtDr in _prdtDrs)
                                        {
                                            DataRow _drTNew = tf_Dyh.NewRow();
                                            _drTNew["ITM"] = tf_Dyh.Rows.Count + 1;
                                            _drTNew["PRD_NO"] = _prdtDr["PRD_NO"].ToString();
                                            _drTNew["NAME"] = _prdtDr["NAME"].ToString();
                                            _drTNew["SPC"] = _prdtDr["SPC"].ToString();
                                            _drTNew["WH"] = _wh;
                                            _drTNew["WH_NAME"] = _wh;
                                            if (_prdtDr["EST_DD"] != System.DBNull.Value)
                                                _drTNew["EST_DD"] = Convert.ToDateTime(_prdtDr["EST_DD"].ToString());
                                            _drTNew["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"]));
                                            _drTNew["UNIT"] = _prdtDr["UNIT"].ToString().Trim();
                                            _drTNew["UNITDSC"] = _prdtDr["UNITDSC"].ToString().Trim();
                                            _drTNew["UNITQTY"] = Convert.ToDecimal(_prdtDr["UNITQTY"].ToString());
                                            _drTNew["AMTN"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"])) * Convert.ToDecimal(_prdtDr["UP"]);
                                            _drTNew["UP"] = Convert.ToDecimal(_prdtDr["UP"]);
                                            _drTNew["BOX_ITM"] = _prdtDr["BOX_ITM"].ToString();
                                            _drTNew["REM"] = _prdtDr["REM"].ToString();
                                            foreach (System.Data.DataRow _drMark in _dtMark.Rows)
                                            {
                                                _drTNew[_drMark["FLDNAME"].ToString()] = _prdtDr[_drMark["FLDNAME"].ToString()].ToString();
                                                _drTNew[_drMark["FLDNAME"].ToString() + "_SPC"] = _prdtDr[_drMark["FLDNAME"].ToString() + "_SPC"].ToString();
                                            }
                                            tf_Dyh.Rows.Add(_drTNew);
                                        }
                                        break;
                                        #endregion
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]) > 0)
                                        {
                                            string _new_Box_Itm = "";
                                            #region 修改TF_DYH1的库位和数量
                                            DataRow[] _update_drsT1 = tf_DyhBox.Select("ITM = " + _box_Itm);
                                            foreach (DataRow _update_drT1 in _update_drsT1)
                                            {
                                                _update_drT1["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) - Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]);
                                                #region 新增TF_DYH1箱内容
                                                _new_Box_Itm = Convert.ToString(tf_DyhBox.Rows.Count + 1);
                                                DataRow _new_drT1 = tf_DyhBox.NewRow();
                                                _new_drT1["ITM"] = tf_DyhBox.Rows.Count + 1;
                                                _new_drT1["PRD_NO"] = _update_drT1["PRD_NO"];
                                                _new_drT1["CONTENT"] = _update_drT1["CONTENT"];
                                                _new_drT1["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]);
                                                _new_drT1["KEY_ITM"] = tf_DyhBox.Rows.Count + 1;
                                                _new_drT1["WH"] = _wh;
                                                tf_DyhBox.Rows.Add(_new_drT1);
                                                tf_DyhBox.AcceptChanges();
                                                #endregion
                                            }
                                            #endregion
                                            _tmpBox_QtyDr["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) - Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]);
                                            #region 写入要货货品内容
                                            DataRow[] _prdtDrs = _tf_Dyh_Tmp.Select("BOX_ITM = '" + _tmpBox_QtyDr["BOX_ITM"].ToString() + "'");
                                            foreach (DataRow _prdtDr in _prdtDrs)
                                            {
                                                DataRow _drTNew = tf_Dyh.NewRow();
                                                _drTNew["ITM"] = tf_Dyh.Rows.Count + 1;
                                                _drTNew["PRD_NO"] = _prdtDr["PRD_NO"].ToString();
                                                _drTNew["NAME"] = _prdtDr["NAME"].ToString();
                                                _drTNew["SPC"] = _prdtDr["SPC"].ToString();
                                                _drTNew["WH"] = _wh;
                                                _drTNew["WH_NAME"] = _wh;
                                                if (_prdtDr["EST_DD"] != System.DBNull.Value)
                                                    _drTNew["EST_DD"] = Convert.ToDateTime(_prdtDr["EST_DD"].ToString());
                                                _drTNew["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"]));
                                                _drTNew["UNIT"] = _prdtDr["UNIT"].ToString().Trim();
                                                _drTNew["UNITDSC"] = _prdtDr["UNITDSC"].ToString().Trim();
                                                _drTNew["UNITQTY"] = Convert.ToDecimal(_prdtDr["UNITQTY"].ToString());
                                                _drTNew["AMTN"] = Convert.ToDecimal(_tmpBox_QtyDr["BOX_WH" + i.ToString()]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"])) * Convert.ToDecimal(_prdtDr["UP"]);
                                                _drTNew["UP"] = Convert.ToDecimal(_prdtDr["UP"]);
                                                _drTNew["BOX_ITM"] = _new_Box_Itm;
                                                _drTNew["REM"] = _prdtDr["REM"].ToString();
                                                foreach (System.Data.DataRow _drMark in _dtMark.Rows)
                                                {
                                                    _drTNew[_drMark["FLDNAME"].ToString()] = _prdtDr[_drMark["FLDNAME"].ToString()].ToString();
                                                    _drTNew[_drMark["FLDNAME"].ToString() + "_SPC"] = _prdtDr[_drMark["FLDNAME"].ToString() + "_SPC"].ToString();
                                                }
                                                tf_Dyh.Rows.Add(_drTNew);
                                            }
                                            #endregion
                                            _tmpBox_Qty.AcceptChanges();
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                if (!_checkPrdt1)
                                {
                                    _errorQTY += _prd_No + ":" + _content + ";";
                                }
                                else
                                {
                                    #region 按件来取库位
                                    for (int i = 0; i < _aryWh.Length; i++)
                                    {
                                        _total_Box_Wh_Qty += Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]);
                                    }
                                    if (_total_Box_Wh_Qty >= Convert.ToDecimal(_tmpBox_QtyDr["QTY"]))
                                    {
                                        string _wh = "";
                                        for (int i = 0; i < _aryWh.Length; i++)
                                        {
                                            _wh = _aryWh[i].ToString();
                                            if (Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) <= Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]))
                                            {
                                                #region 修改TF_DYH1的库位
                                                DataRow[] _update_drsT1 = tf_DyhBox.Select("ITM = " + _box_Itm);
                                                foreach (DataRow _update_drT1 in _update_drsT1)
                                                {
                                                    _update_drT1["WH"] = _wh;
                                                }
                                                #endregion
                                                #region 写入要货货品内容
                                                DataRow[] _prdtDrs = _tf_Dyh_Tmp.Select("BOX_ITM = '" + _tmpBox_QtyDr["BOX_ITM"].ToString() + "'");
                                                foreach (DataRow _prdtDr in _prdtDrs)
                                                {
                                                    DataRow _drTNew = tf_Dyh.NewRow();
                                                    _drTNew["ITM"] = tf_Dyh.Rows.Count + 1;
                                                    _drTNew["PRD_NO"] = _prdtDr["PRD_NO"].ToString();
                                                    _drTNew["NAME"] = _prdtDr["NAME"].ToString();
                                                    _drTNew["SPC"] = _prdtDr["SPC"].ToString();
                                                    _drTNew["WH"] = _wh;
                                                    _drTNew["WH_NAME"] = _wh;
                                                    if (_prdtDr["EST_DD"] != System.DBNull.Value)
                                                        _drTNew["EST_DD"] = Convert.ToDateTime(_prdtDr["EST_DD"].ToString());
                                                    _drTNew["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"]));
                                                    _drTNew["UNIT"] = _prdtDr["UNIT"].ToString().Trim();
                                                    _drTNew["UNITDSC"] = _prdtDr["UNITDSC"].ToString().Trim();
                                                    _drTNew["UNITQTY"] = Convert.ToDecimal(_prdtDr["UNITQTY"].ToString());
                                                    _drTNew["AMTN"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"])) * Convert.ToDecimal(_prdtDr["UP"]);
                                                    _drTNew["UP"] = Convert.ToDecimal(_prdtDr["UP"]);
                                                    _drTNew["BOX_ITM"] = _prdtDr["BOX_ITM"].ToString();
                                                    _drTNew["REM"] = _prdtDr["REM"].ToString();
                                                    foreach (System.Data.DataRow _drMark in _dtMark.Rows)
                                                    {
                                                        _drTNew[_drMark["FLDNAME"].ToString()] = _prdtDr[_drMark["FLDNAME"].ToString()].ToString();
                                                        _drTNew[_drMark["FLDNAME"].ToString() + "_SPC"] = _prdtDr[_drMark["FLDNAME"].ToString() + "_SPC"].ToString();
                                                    }
                                                    tf_Dyh.Rows.Add(_drTNew);
                                                }
                                                break;
                                                #endregion
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]) > 0)
                                                {
                                                    string _new_Box_Itm = "";
                                                    #region 修改TF_DYH1的库位和数量
                                                    DataRow[] _update_drsT1 = tf_DyhBox.Select("ITM = " + _box_Itm);
                                                    foreach (DataRow _update_drT1 in _update_drsT1)
                                                    {
                                                        _update_drT1["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) - Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]);
                                                        #region 新增TF_DYH1箱内容
                                                        _new_Box_Itm = Convert.ToString(tf_DyhBox.Rows.Count + 1);
                                                        DataRow _new_drT1 = tf_DyhBox.NewRow();
                                                        _new_drT1["ITM"] = tf_DyhBox.Rows.Count + 1;
                                                        _new_drT1["PRD_NO"] = _update_drT1["PRD_NO"];
                                                        _new_drT1["CONTENT"] = _update_drT1["CONTENT"];
                                                        _new_drT1["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]);
                                                        _new_drT1["KEY_ITM"] = tf_DyhBox.Rows.Count + 1;
                                                        _new_drT1["WH"] = _wh;
                                                        tf_DyhBox.Rows.Add(_new_drT1);
                                                        tf_DyhBox.AcceptChanges();
                                                        #endregion
                                                    }
                                                    #endregion
                                                    _tmpBox_QtyDr["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["QTY"]) - Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]);
                                                    #region 写入要货货品内容
                                                    DataRow[] _prdtDrs = _tf_Dyh_Tmp.Select("BOX_ITM = '" + _tmpBox_QtyDr["BOX_ITM"].ToString() + "'");
                                                    foreach (DataRow _prdtDr in _prdtDrs)
                                                    {
                                                        DataRow _drTNew = tf_Dyh.NewRow();
                                                        _drTNew["ITM"] = tf_Dyh.Rows.Count + 1;
                                                        _drTNew["PRD_NO"] = _prdtDr["PRD_NO"].ToString();
                                                        _drTNew["NAME"] = _prdtDr["NAME"].ToString();
                                                        _drTNew["SPC"] = _prdtDr["SPC"].ToString();
                                                        _drTNew["WH"] = _wh;
                                                        _drTNew["WH_NAME"] = _wh;
                                                        if (_prdtDr["EST_DD"] != System.DBNull.Value)
                                                            _drTNew["EST_DD"] = Convert.ToDateTime(_prdtDr["EST_DD"].ToString());
                                                        _drTNew["QTY"] = Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"]));
                                                        _drTNew["UNIT"] = _prdtDr["UNIT"].ToString().Trim();
                                                        _drTNew["UNITDSC"] = _prdtDr["UNITDSC"].ToString().Trim();
                                                        _drTNew["UNITQTY"] = Convert.ToDecimal(_prdtDr["UNITQTY"].ToString());
                                                        _drTNew["AMTN"] = Convert.ToDecimal(_tmpBox_QtyDr["WH" + i.ToString()]) * (Convert.ToDecimal(_prdtDr["QTY"]) / Convert.ToDecimal(_tmpBox_QtyDr["OLD_QTY"])) * Convert.ToDecimal(_prdtDr["UP"]);
                                                        _drTNew["UP"] = Convert.ToDecimal(_prdtDr["UP"]);
                                                        _drTNew["BOX_ITM"] = _new_Box_Itm;
                                                        _drTNew["REM"] = _prdtDr["REM"].ToString();
                                                        foreach (System.Data.DataRow _drMark in _dtMark.Rows)
                                                        {
                                                            _drTNew[_drMark["FLDNAME"].ToString()] = _prdtDr[_drMark["FLDNAME"].ToString()].ToString();
                                                            _drTNew[_drMark["FLDNAME"].ToString() + "_SPC"] = _prdtDr[_drMark["FLDNAME"].ToString() + "_SPC"].ToString();
                                                        }
                                                        tf_Dyh.Rows.Add(_drTNew);
                                                    }
                                                    #endregion
                                                    _tmpBox_Qty.AcceptChanges();
                                                }
                                            }
                                        }
                                        _whIsOk = true;
                                    }
                                    else
                                    {
                                        _whIsOk = false;
                                    }
                                    if (!_whIsOk)
                                    {
                                        _errorQTY += _prd_No + ":" + _content + ";";
                                    }
                                    #endregion
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(_errorQTY))
                            _isOk = false;
                        else
                            _isOk = true;
                        #endregion
                        #endregion
                        #region 按箱要货（不用的）
                        /*
						foreach(DataRow _drT1 in tf_DyhBox.Rows)
						{
							bool _whIsOk = false;
							bool[,] _aryWhIsOk = null;
							string[] _aryWH = null;
							string _prd_No = _drT1["PRD_NO"].ToString();
							string _content = _drT1["CONTENT"].ToString();
							string _whs = "";
							int _box_Itm = Convert.ToInt32(_drT1["KEY_ITM"]);
							DataRow[] _drsT = tf_Dyh.Select("BOX_ITM = " + _box_Itm);
							int _prd_Itm = 0;
							int _wh_Itm = 0;
							foreach(DataRow _drT in _drsT)
							{
								#region 要货库位数量
								_aryWH = _drT["WH"].ToString().Split(new char[]{';'});
								decimal[] _aryWH_QTY = new decimal[_aryWH.Length];
								string _prd_Mark = "";
								for(int _j = 0 ; _j < _markColn ; _j=_j+2)
								{
									_prd_Mark = _prd_Mark + _drT[(_j+3)].ToString();
								}
								if(_aryWhIsOk == null)
									_aryWhIsOk = new bool[_drsT.Length,_aryWH.Length];
								int _i = 0;
								foreach(string _wh in _aryWH)
								{
									_aryWH_QTY[_i] = _clsWH.GetWH_QTY(_drT["PRD_NO"].ToString() , _prd_Mark , _wh);
									_i++;
								}
								for(int j=0;j<_aryWH.Length;j++)
								{
									if(_aryWH_QTY[j] >= Convert.ToDecimal(_drT["QTY"]))
									{
										_aryWhIsOk[_prd_Itm , j] = true;
										//_whIsOk = true;
										//_drT["WH"] = _aryWH[j];
									}
									else
									{
										_aryWhIsOk[_prd_Itm , j] = false;
										//_whIsOk = false;
										//_whs = _drT["WH"].ToString();
									}
								}
								_prd_Itm++;
								#endregion
							}
							#region 判断那个库位数量足够
							_whIsOk = false;
							for(int wh_Itm=0;wh_Itm<_aryWH.Length;wh_Itm++)
							{
								for(int prd_Itm=0;prd_Itm<_drsT.Length;prd_Itm++)
								{
									if(((bool)_aryWhIsOk[prd_Itm , wh_Itm]))
									{
										_whIsOk = true;
									}
									else
									{
										_whIsOk = false;
										break;
									}
								}
								if(_whIsOk)
								{
									_wh_Itm = wh_Itm;
									break;
								}
							}
							#endregion
							#region 更新TF_DYH
							foreach(DataRow _drT in _drsT)
							{
								_drT["WH"] = _aryWH[_wh_Itm];
							}
							#endregion
							if(! _whIsOk)
							{
								_errorQTY += _prd_No + ":" + _content + ":" + _whs + ";";
							}
						}
						if(!String.IsNullOrEmpty(_errorQTY))
							_isOk = false;
						else
							_isOk = true;
						*/
                        #endregion
                    }
                    else
                    {
                        #region 按件要货
                        //foreach (DataRow _drTRow in _dtT.Rows)
                        int _intDtTRow = tf_Dyh.Rows.Count;
                        for (int idtT = 0; idtT < _intDtTRow; idtT++)
                        {
                            DataRow _drTRow = tf_Dyh.Rows[idtT];
                            bool _whIsOk = false;
                            string[] _aryWH = _drTRow["WH"].ToString().Split(new char[] { ';' });
                            decimal[] _aryWH_QTY = new decimal[_aryWH.Length];
                            string _prd_No = _drTRow["PRD_NO"].ToString();
                            StringBuilder _prd_Mark = new StringBuilder();
                            for (int _j = 0; _j < _markColn; _j = _j + 2)
                            {
                                _prd_Mark.Append(_drTRow[(_j + 3)].ToString());
                            }
                            DateTime _est_DD = Convert.ToDateTime(_drTRow["EST_DD"]);
                            string _unit = _drTRow["UNIT"].ToString();
                            decimal _up = Convert.ToDecimal(_drTRow["UP"]);
                            decimal _yh_QTY = Convert.ToDecimal(_drTRow["QTY"]);
                            decimal _wh_QTY = 0;
                            StringBuilder _whs = new StringBuilder();
                            string _yh_No = yh_NO;
                            int _i = 0;
                            #region 要货库位数量
                            foreach (string _wh in _aryWH)
                            {
                                if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                                {
                                    //_aryWH_QTY[_i] = _clsWH.GetQty(false,_prd_No , _prd_Mark , _wh);
                                    _aryWH_QTY[_i] = _clsWH.GetQty(false, _prd_No, _prd_Mark.ToString(), _wh, String.Empty);
                                }
                                else
                                {
                                    //_aryWH_QTY[_i] = _clsWH.GetQty(false,_prd_No , _prd_Mark , _wh);
                                    _aryWH_QTY[_i] = _clsWH.GetQty(false, _prd_No, _prd_Mark.ToString(), _wh, String.Empty);
                                }
                                _wh_QTY += _aryWH_QTY[_i];
                                _i++;
                            }
                            #endregion
                            if (_wh_QTY >= _yh_QTY)
                            {
                                #region 数量足够
                                if (_aryWH_QTY[0] >= _yh_QTY)
                                {
                                    _drTRow["WH"] = _aryWH[0];
                                }
                                else
                                {
                                    if (_aryWH_QTY[0] > 0)
                                    {
                                        #region 更新
                                        _drTRow["WH"] = _aryWH[0];
                                        _drTRow["QTY"] = _aryWH_QTY[0];
                                        _drTRow["AMTN"] = _aryWH_QTY[0] * _up;
                                        #endregion
                                        #region 新增
                                        DataRow _newRow = tf_Dyh.NewRow();
                                        _newRow["YH_ID"] = "YH";
                                        _newRow["YH_NO"] = _yh_No;
                                        _newRow["ITM"] = tf_Dyh.Rows.Count + 1;
                                        _newRow["PRD_NO"] = _prd_No;
                                        _newRow["PRD_MARK"] = _prd_Mark;
                                        _newRow["WH"] = _aryWH[1];
                                        _newRow["EST_DD"] = _est_DD;
                                        _newRow["QTY"] = _yh_QTY - _aryWH_QTY[0];
                                        _newRow["UNIT"] = _unit;
                                        _newRow["AMTN"] = Convert.ToDecimal(_yh_QTY - _aryWH_QTY[0]) * _up;
                                        _newRow["UP"] = _up;
                                        tf_Dyh.Rows.Add(_newRow);
                                        #endregion
                                    }
                                    else
                                    {
                                        #region 更新
                                        _drTRow["WH"] = _aryWH[1];
                                        #endregion
                                    }
                                }
                                _whIsOk = true;
                                #endregion
                            }
                            else
                            {
                                #region 数量不足
                                _whIsOk = false;
                                _whs.Append(_drTRow["WH"].ToString());
                                #endregion
                            }
                            tf_Dyh.AcceptChanges();
                            if (tf_DyhBox != null)
                                tf_DyhBox.AcceptChanges();
                            if (!_whIsOk)
                            {
                                _isOk = false;
                                _errorQTY += _prd_No + ":" + _prd_Mark + ":" + _whs + ";";
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 库存量
                    foreach (DataRow _drTRow in tf_Dyh.Rows)
                    {
                        string[] _aryWH = _drTRow["WH"].ToString().Split(new char[] { ';' });
                        _drTRow["WH"] = _aryWH[0].ToString();
                    }
                    if (tf_DyhBox != null)
                    {
                        foreach (DataRow _drT1Row in tf_DyhBox.Rows)
                        {
                            string[] _aryWH = _drT1Row["WH"].ToString().Split(new char[] { ';' });
                            _drT1Row["WH"] = _aryWH[0].ToString();
                        }
                    }
                    #endregion
                }
                if (_isOk)
                {
                    System.Data.DataSet _ds = _dbDrpyh.GetYH(yh_ID, yh_NO);
                    System.Data.DataTable _dtM = _ds.Tables["MF_DYH"];
                    System.Data.DataTable _dtT = _ds.Tables["TF_DYH"];
                    System.Data.DataTable _dtT1 = _ds.Tables["TF_DYH1"];
                    foreach (DataRow _dr in _dtM.Rows)
                    {
                        _dr["FX_WH"] = fx_wh;
                        _dr["EST_DD"] = est_DD;
                    }
                    foreach (DataRow _dr in _dtT.Rows)
                    {
                        _dr.Delete();
                    }
                    #region //**TF_DYH插入数据**//
                    int _tf_DyhColn = tf_Dyh.Columns.Count;
                    int _markColn = _tf_DyhColn - 17;
                    foreach (DataRow _tf_DyhDr in tf_Dyh.Rows)
                    {
                        System.Data.DataRow _drT = _dtT.NewRow();
                        _drT["YH_ID"] = yh_ID;
                        _drT["YH_NO"] = yh_NO;
                        _drT["ITM"] = Convert.ToInt32(_tf_DyhDr["ITM"]);
                        _drT["PRD_NO"] = _tf_DyhDr["PRD_NO"].ToString();
                        StringBuilder _prd_Mark = new StringBuilder();
                        for (int _i = 0; _i < _markColn; _i = _i + 2)
                        {
                            _prd_Mark.Append(_tf_DyhDr[(_i + 3)].ToString());
                        }
                        _drT["PRD_MARK"] = _prd_Mark;
                        _drT["WH"] = _tf_DyhDr["WH"].ToString();
                        _drT["EST_DD"] = ((System.DateTime)_tf_DyhDr["EST_DD"]).ToShortDateString();
                        _drT["QTY"] = Convert.ToDecimal(_tf_DyhDr["QTY"]);
                        _drT["QTY_OLD"] = _tf_DyhDr["QTY_OLD"];
                        _drT["UNIT"] = _tf_DyhDr["UNIT"].ToString().Trim();
                        _drT["AMTN"] = Convert.ToDecimal(_tf_DyhDr["AMTN"]);
                        _drT["UP"] = Convert.ToDecimal(_tf_DyhDr["UP"]);
                        if (_isByBox)
                            _drT["BOX_ITM"] = Convert.ToInt32(_tf_DyhDr["BOX_ITM"]);
                        //else
                        //	_drT["BOX_ITM"] = 0;
                        _dtT.Rows.Add(_drT);
                    }
                    #endregion
                    if (tf_DyhBox != null)
                    {
                        foreach (DataRow _dr in _dtT1.Rows)
                        {
                            _dr.Delete();
                        }
                        #region //**TF_DYH1插入数据**//
                        foreach (DataRow _tf_DyhBoxDr in tf_DyhBox.Rows)
                        {
                            DataRow _drT1 = _dtT1.NewRow();
                            _drT1["YH_ID"] = "YH";
                            _drT1["YH_NO"] = yh_NO;
                            _drT1["ITM"] = Convert.ToInt32(_tf_DyhBoxDr["ITM"]);
                            _drT1["PRD_NO"] = _tf_DyhBoxDr["PRD_NO"].ToString();
                            _drT1["CONTENT"] = _tf_DyhBoxDr["CONTENT"].ToString();
                            _drT1["QTY"] = Convert.ToDecimal(_tf_DyhBoxDr["QTY"]);
                            _drT1["QTY_OLD"] = _tf_DyhBoxDr["QTY_OLD"];
                            _drT1["KEY_ITM"] = _tf_DyhBoxDr["KEY_ITM"];
                            _drT1["WH"] = _tf_DyhBoxDr["WH"];
                            if (_tf_DyhBoxDr["EST_DD"] != System.DBNull.Value)
                                _drT1["EST_DD"] = ((System.DateTime)_tf_DyhBoxDr["EST_DD"]).ToShortDateString();
                            _dtT1.Rows.Add(_drT1);
                        }

                        #endregion
                    }
                    _isUpdate = true;
                    this.UpdateDataSet(_ds);
                    _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
                }
                else
                {
                    tf_Dyh = _tmp_tf_Dyh;
                    if (_tmp_tf_DyhBox != null)
                        tf_DyhBox = _tmp_tf_DyhBox;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                _errorQTY = ex.Message.ToString();
                tf_Dyh = _tmp_tf_Dyh;
                if (_tmp_tf_DyhBox != null)
                    tf_DyhBox = _tmp_tf_DyhBox;
            }
            errorDt = _errorDt;
            return _errorQTY;
        }
        #endregion

        #region 更新按件要货单
        /// <summary>
        /// 更新按件要货单
        /// </summary>
        /// <param name="fx_wh"></param>
        /// <param name="errorDt"></param>
        /// <param name="yh_ID">要货编号</param>
        /// <param name="yh_NO">要货单号</param>
        /// <param name="tf_Dyh">要货单内容</param>
        /// <param name="est_DD">预交货日</param>
        public string Update(string yh_ID, string yh_NO, string fx_wh, DateTime est_DD, DataTable tf_Dyh, out DataTable errorDt)
        {
            DataTable _dtError = null;
            string _errorQTY = "";
            _errorQTY = this.UpdateByBox(yh_ID, yh_NO, fx_wh, est_DD, tf_Dyh, null, out _dtError);
            errorDt = _dtError;
            return _errorQTY;
        }
        #endregion

        #region 取产品单位
        /// <summary>
        /// 取产品单位
        /// </summary>
        /// <param name="prd_No">产品货号</param>
        /// <param name="dfu_Ut">默认单位</param>
        /// <returns>产品单位表（ID：编号，DSC：描述，VALUE：数值）</returns>
        public System.Data.DataTable GetUnit(string prd_No, out string dfu_Ut)
        {
            System.Data.DataTable _dt;
            string _dfu_Ut = "";
            try
            {
                Sunlike.Business.Prdt _prdt = new Prdt();
                _dt = _prdt.GetUnit(prd_No, out _dfu_Ut);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            dfu_Ut = _dfu_Ut;
            return _dt;
        }
        #endregion

        #region 删除要货单
        /// <summary>
        /// 删除要货单
        /// </summary>
        /// <param name="yh_ID_NO">要货单号（YH;YH43120001）</param>
        public void DeleteYH(string[] yh_ID_NO)
        {
            try
            {
                for (int _i = 0; _i < yh_ID_NO.Length; _i++)
                {
                    string[] _yh = yh_ID_NO[_i].ToString().Split(new char[] { ';' });
                    //_dbDrpyh.DeleteYH(_yh[0] , _yh[1]);
                    System.Data.SqlClient.SqlConnection _cn = new System.Data.SqlClient.SqlConnection(Comp.Conn_DB);
                    System.Data.DataSet _ds = new System.Data.DataSet("DB_" + Comp.CompNo);
                    System.Data.DataTable _dtT;
                    System.Data.DataTable _dtT1;
                    System.Data.DataTable _dtM;
                    System.Data.SqlClient.SqlDataAdapter _da = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM TF_DYH WHERE YH_ID = '" + _yh[0] + "' AND YH_NO = '" + _yh[1] + "';SELECT * FROM TF_DYH1 WHERE YH_ID = '" + _yh[0] + "' AND YH_NO = '" + _yh[1] + "';SELECT * FROM MF_DYH WHERE YH_ID = '" + _yh[0] + "' AND YH_NO = '" + _yh[1] + "'", _cn);
                    _da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    _da.TableMappings.Add("Table", "TF_DYH");
                    _da.TableMappings.Add("Table1", "TF_DYH1");
                    _da.TableMappings.Add("Table2", "MF_DYH");
                    _da.Fill(_ds);
                    _dtM = _ds.Tables["MF_DYH"];
                    _dtT = _ds.Tables["TF_DYH"];
                    _dtT1 = _ds.Tables["TF_DYH1"];
                    foreach (DataRow _dr in _dtT.Rows)
                    {
                        _dr.Delete();
                    }
                    foreach (DataRow _dr in _dtT1.Rows)
                    {
                        _dr.Delete();
                    }
                    foreach (DataRow _dr in _dtM.Rows)
                    {
                        _dr.Delete();
                    }
                    _isDelete = true;
                    _YH_No = _yh[1];
                    this.UpdateDataSet(_ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 取要货单内容
        /// <summary>
        /// 取要货单内容
        /// </summary>
        /// <param name="yh_ID">要货编号</param>
        /// <param name="yh_NO">要货单号</param>
        /// <param name="prdMarkDt">编码表</param>
        /// <returns>取要货单内容</returns>
        public System.Data.DataTable GetYH_Detail(string yh_ID, string yh_NO, DataTable prdMarkDt)
        {
            Sunlike.Business.Prdt _prdt = new Prdt();
            Sunlike.Business.PrdMark _prdMark = new PrdMark();
            string _ut_Name = "";
            int _ut_Qty = 1;
            DataTable _yh_DetailDt = new DataTable("TF_DYH");
            #region 创建要货单明细表(_yh_DetailDt)
            _yh_DetailDt.Columns.Add("ITM", System.Type.GetType("System.Int32"));
            _yh_DetailDt.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("NAME", System.Type.GetType("System.String"));
            if (prdMarkDt != null)
            {
                foreach (System.Data.DataRow _dr in prdMarkDt.Rows)
                {
                    _yh_DetailDt.Columns.Add(_dr["FLDNAME"].ToString(), System.Type.GetType("System.String"));
                    _yh_DetailDt.Columns.Add(_dr["FLDNAME"].ToString() + "_SPC", System.Type.GetType("System.String"));
                }
            }
            _yh_DetailDt.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("SPC", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("WH", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("WH_NAME", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("EST_DD", System.Type.GetType("System.DateTime"));
            _yh_DetailDt.Columns.Add("QTY", System.Type.GetType("System.Decimal"));
            _yh_DetailDt.Columns.Add("QTY_OLD", System.Type.GetType("System.Decimal"));
            _yh_DetailDt.Columns.Add("UNIT", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("UNITDSC", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("UNITQTY", System.Type.GetType("System.Decimal"));
            _yh_DetailDt.Columns.Add("AMTN", System.Type.GetType("System.Decimal"));
            _yh_DetailDt.Columns.Add("UP", System.Type.GetType("System.Decimal"));
            _yh_DetailDt.Columns.Add("BOX_ITM", System.Type.GetType("System.Int32"));
            _yh_DetailDt.Columns.Add("REM", System.Type.GetType("System.String"));
            #endregion
            #region 取要货单TF_DYH表
            DataTable _tf_DyhDt = _dbDrpyh.GetTF_DYH(yh_ID, yh_NO);
            #endregion
            #region 插入记录到_yh_DetailDt表
            foreach (DataRow _dr in _tf_DyhDt.Rows)
            {
                DataRow _yh_DetailDr = _yh_DetailDt.NewRow();
                _yh_DetailDr["ITM"] = _dr["ITM"].ToString();
                _yh_DetailDr["PRD_NO"] = _dr["PRD_NO"].ToString();
                _yh_DetailDr["NAME"] = _dr["NAME"].ToString();
                _yh_DetailDr["PRD_MARK"] = _dr["PRD_MARK"].ToString();
                _yh_DetailDr["SPC"] = _dr["SPC"].ToString();
                _yh_DetailDr["WH"] = _dr["WH"].ToString();
                _yh_DetailDr["WH_NAME"] = _dr["WH_NAME"].ToString();
                _yh_DetailDr["EST_DD"] = _dr["EST_DD"].ToString();
                _yh_DetailDr["QTY"] = _dr["QTY"].ToString();
                _yh_DetailDr["QTY_OLD"] = _dr["QTY_OLD"];
                _yh_DetailDr["UNIT"] = _dr["UNIT"].ToString();
                #region 取货品单位名称和单位数量
                _prdt.GetUnitDetail(_dr["PRD_NO"].ToString(), _dr["UNIT"].ToString(), out _ut_Name, out _ut_Qty);
                #endregion
                _yh_DetailDr["UNITDSC"] = _ut_Name;
                _yh_DetailDr["UNITQTY"] = _ut_Qty.ToString();
                _yh_DetailDr["AMTN"] = Convert.ToDouble(_dr["AMTN"].ToString());
                _yh_DetailDr["UP"] = Convert.ToDouble(_dr["UP"].ToString());
                _yh_DetailDr["BOX_ITM"] = Convert.ToInt32(_dr["BOX_ITM"].ToString());
                _yh_DetailDr["REM"] = _dr["REM"].ToString();
                //string _prd_Mark = _dr["PRD_MARK"].ToString();
                //				int _stratLocation = 0;
                //				DataTable _prdMarkDt = null;
                //				for(int _i = 0 ; _i < _markRow ; _i++)
                //				{
                //					_prdMarkDt = _prdMark.GetSplitDetail(prdMarkDt.Rows[_i]["FLDNAME"].ToString() , _dr["PRD_NO"].ToString());
                //					string _fldname_Value = _prd_Mark.Substring(_stratLocation , Convert.ToInt32(prdMarkDt.Rows[_i]["SIZE"].ToString()));
                //					_yh_DetailDr[prdMarkDt.Rows[_i]["FLDNAME"].ToString()] = _fldname_Value;
                //					DataRow[] _drs = _prdMarkDt.Select("VALUE = '"+_fldname_Value+"'");
                //					string _fldname_DSC = "";
                //					foreach(DataRow _drDSC in _drs)
                //					{
                //						_fldname_DSC = _drDSC["DSC"].ToString();
                //						break;
                //					}
                //					_yh_DetailDr[prdMarkDt.Rows[_i]["FLDNAME"].ToString() + "_SPC"] = _fldname_DSC;
                //					_stratLocation += Convert.ToInt32(prdMarkDt.Rows[_i]["SIZE"].ToString()); 
                //				}
                _yh_DetailDt.Rows.Add(_yh_DetailDr);
            }
            _prdMark.BreakDownPrd_Mark(_yh_DetailDt);
            _yh_DetailDt.AcceptChanges();
            #endregion
            return _yh_DetailDt;
        }
        #endregion

        #region 取要货单表头
        /// <summary>
        /// 取要货单表头
        /// </summary>
        /// <param name="yh_ID">要货编号</param>
        /// <param name="yh_NO">要货单号</param>
        /// <param name="cls_ID">是否审核</param>
        /// <returns>取要货单表头</returns>
        public DataTable GetYH(string yh_ID, string yh_NO, out bool cls_ID)
        {
            bool _cls_ID = false;
            DataTable _dt = new DataTable("MF_DYH");
            try
            {
                _dt = _dbDrpyh.GetMF_DYH(yh_ID, yh_NO, Comp.CompNo);
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        //						if(_dr["CLS_ID"].ToString() == "T")
                        //							_cls_ID = true;
                        //						else
                        //							_cls_ID = false;
                        //						break;
                        if (_dr["CLS_DATE"] != System.DBNull.Value && !String.IsNullOrEmpty(_dr["CLS_DATE"].ToString()))
                            _cls_ID = true;
                        else
                            _cls_ID = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cls_ID = _cls_ID;
            return _dt;
        }
        #endregion

        #region 取按箱要货的箱内容
        /// <summary>
        /// 取按箱要货的箱内容
        /// </summary>
        /// <param name="yh_No">要货单号</param>
        /// <returns></returns>
        public DataTable GetYH_Box(string yh_No)
        {
            DataTable _dt = null;
            try
            {
                Sunlike.Business.Data.DbDRPYH _dbDrpyh = new DbDRPYH(Comp.Conn_DB);
                _dt = _dbDrpyh.GetYH_Box(yh_No);
            }
            catch (Exception ex)
            {
                throw new SunlikeException(ex.Message, ex);
            }
            return _dt;
        }
        #endregion

        #region 结案
        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="yhNo">单号</param>
        public void CheckForSO(string yhNo)
        {
            string yhId = yhNo.Substring(0, 2).ToString();
            DbDRPYH _dbYh = new DbDRPYH(Comp.Conn_DB);
            DataTable _dt = _dbYh.GetTF_DYH(yhId, yhNo);
            bool _canCheck = true;
            foreach (DataRow _dr in _dt.Rows)
            {
                decimal _qtySo = Convert.ToDecimal(_dr["QTY_SO"]);
                decimal _qty = Convert.ToDecimal(_dr["QTY"]);
                if (_qtySo < _qty && (_dr["DEL_ID"].ToString() == "F" || String.IsNullOrEmpty(_dr["DEL_ID"].ToString())))
                {
                    _canCheck = false;
                }
            }
            if (_canCheck)
            {
                _dbDrpyh.CheckForSO(yhNo);
            }
        }

        /// <summary>
        /// 检查是否已经结案
        /// </summary>
        /// <param name="yh_No"></param>
        /// <returns></returns>
        public bool CheckClose(string yh_No)
        {
            return _dbDrpyh.CheckClose(yh_No);
        }
        #endregion

        #region 反结案
        /// <summary>
        /// 反结案
        /// </summary>
        /// <param name="yh_No">单号</param>
        public void UnCheckForSO(string yh_No)
        {
            _dbDrpyh.UnCheckForSO(yh_No);
        }
        #endregion

        #region 审核接口
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
        /// 生成受订单
        /// </summary>
        /// <param name="yh_ID">单据代号</param>
        /// <param name="yh_No">单据编号</param>
        /// <param name="chk_Man">审核人</param>
        /// <param name="chk_DD">审核日期</param>
        /// <returns>错误信息</returns>
        public string Approve(string yh_ID, string yh_No, string chk_Man, System.DateTime chk_DD)
        {
            string _error = "";
            try
            {
                //*要货单结案*//
                _dbDrpyh.CheckYH(yh_No, chk_Man, chk_DD);
                SunlikeDataSet _ds = _dbDrpyh.GetYH(yh_ID, yh_No);
                try
                {
                    //*调用受订单方法*//
                    Sunlike.Business.DrpSO _drpso = new DrpSO();
                    _drpso.InsertDrpSO(_ds);
                }
                catch (Exception ex)
                {
                    _dbDrpyh.RollBackYH(yh_No);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
            }
            return _error;
        }
        /// <summary>
        /// 审核要货单据错误回退
        /// </summary>
        /// <param name="yh_No">单据编号</param>
        /// <param name="yh_Id">单据别</param>
        public string RollBack(string yh_Id, string yh_No)
        {
            return RollBack(yh_Id, yh_No, true);
        }
        /// <summary>
        /// 审核要货单据错误回退
        /// </summary>
        /// <param name="yh_No">单据编号</param>
        /// <param name="yh_Id">单据别</param>
        public string RollBack(string yh_Id, string yh_No, bool canChangeDS)
        {
            string _error = "";
            try
            {
                //如果有受订单的话，回退受订单。
                Sunlike.Business.DrpSO _drpso = new DrpSO();
                _error = _drpso.DeleteDrpSO(yh_Id, yh_No);
                if (String.IsNullOrEmpty(_error) && canChangeDS)
                {
                    //要货单回退
                    _dbDrpyh.RollBackYH(yh_No);
                }
            }
            catch (Exception ex)
            {
                _error = _error + ex.Message.ToString();
            }
            return _error;
        }
        #endregion


        #region 修改货品数量(For Auditing)
        /// <summary>
        /// 修改货品数量(For Auditing)
        /// </summary>
        /// <param name="usr">修改人</param>
        /// <param name="yh_id">要货单据类别</param>
        /// <param name="yh_No">要货单号</param>
        /// <param name="itm">表身货品ITM编号</param>
        /// <param name="wh">库位代号</param>
        /// <param name="qty">数量</param>
        /// <param name="est_DD">预交货日</param>
        /// <returns></returns>
        public string UpdateQty(string usr,string yh_id, string yh_No, string itm , string wh , decimal qty , DateTime est_DD)
        {
            StringBuilder _error = new StringBuilder();
            try
            {
                SunlikeDataSet _ds = _dbDrpyh.GetYH(yh_id, yh_No);
                DataTable _dtT = _ds.Tables["TF_DYH"];
                string _wh_Old = "";
                decimal _qty_Old = 0;
                DateTime _est_DD_Old = System.DateTime.Now;
                DataRow[] _drs = _dtT.Select("ITM = " + itm);
                foreach (DataRow _dr in _drs)
                {
                    _wh_Old = _dr["WH"].ToString();
                    _qty_Old = Convert.ToDecimal(_dr["QTY"]);
                    _est_DD_Old = Convert.ToDateTime(_dr["EST_DD"]);
                    if (String.IsNullOrEmpty(wh))
                    {
                        _dr["WH"] = System.DBNull.Value;
                    }
                    else
                    {
                        _dr["WH"] = wh;
                    }

                    _dr["QTY"] = qty;
                    _dr["AMTN"] = qty * Convert.ToDecimal(_dr["UP"]);
                    _dr["EST_DD"] = est_DD;
                    if (_dr["WH_OLD"] == System.DBNull.Value)
                        _dr["WH_OLD"] = _wh_Old;
                    if (_dr["QTY_OLD"] == System.DBNull.Value)
                        _dr["QTY_OLD"] = _qty_Old;
                    if (_dr["EST_OLD"] == System.DBNull.Value)
                        _dr["EST_OLD"] = _est_DD_Old;
                }
                _ds.ExtendedProperties["UPD_USR"] = usr;
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

        #region 修改箱货品数量(For Auditing)
        /// <summary>
        /// 修改箱货品数量(For Auditing)
        /// </summary>
        /// <param name="yh_id">要货单据类别</param>
        /// <param name="yh_No">要货单号</param>
        /// <param name="itm">表身箱ITM编号</param>
        /// <param name="wh">库位代号</param>
        /// <param name="qty">数量</param>
        /// <param name="est_DD">预交货日</param>
        /// <returns></returns>
        public string UpdateQtyForBox(string yh_id, string yh_No, string itm , string wh , decimal qty , DateTime est_DD)
        {
            StringBuilder _error = new StringBuilder();
            try
            {
                SunlikeDataSet _ds = _dbDrpyh.GetYH(yh_id, yh_No);
                DataTable _dtT = _ds.Tables["TF_DYH"];
                DataTable _dtT1 = _ds.Tables["TF_DYH1"];
                DataTable _dtM = _ds.Tables["MF_DYH"];
                bool _isChg = false;
                string _box_WH_Old = "";
                decimal _box_Qty_Old = 0;
                string _wh_Old = "";
                decimal _qty_Old = 0;
                DateTime _est_DD_Old = System.DateTime.Now;
                int _box_Itm = 0;
                #region 箱内容
                DataRow[] _drsT1 = _dtT1.Select("ITM = " + itm);
                foreach (DataRow _drT1 in _drsT1)
                {
                    _box_WH_Old = _drT1["WH"].ToString();
                    if (_drT1["QTY_OLD"] != System.DBNull.Value)
                        _box_Qty_Old = Convert.ToDecimal(_drT1["QTY_OLD"]);
                    else
                        _box_Qty_Old = Convert.ToDecimal(_drT1["QTY"]);

                    if (_drT1["WH_OLD"] == System.DBNull.Value)
                        _drT1["WH_OLD"] = _box_WH_Old;
                    _drT1["QTY_OLD"] = _box_Qty_Old;
                    _drT1["WH"] = wh;
                    _drT1["QTY"] = qty;
                    _drT1["EST_DD"] = est_DD;
                    _box_Itm = Convert.ToInt16(_drT1["KEY_ITM"]);
                    break;
                }
                #region 取预交货日
                DataRow[] _drsT = _dtT.Select("BOX_ITM = " + _box_Itm);
                foreach (DataRow _drT in _drsT)
                {
                    _est_DD_Old = Convert.ToDateTime(_drT["EST_DD"]);
                    break;
                }
                #endregion
                if (_box_WH_Old != wh) _isChg = true;
                if (_box_Qty_Old != qty) _isChg = true;
                if (_est_DD_Old != est_DD) _isChg = true;
                #endregion
                #region 货品内容
                if (_isChg)
                {
                    foreach (DataRow _drT in _drsT)
                    {
                        _wh_Old = _drT["WH"].ToString();
                        if (_drT["QTY_OLD"] != System.DBNull.Value)
                            _qty_Old = Convert.ToDecimal(_drT["QTY_OLD"]);
                        else
                            _qty_Old = Convert.ToDecimal(_drT["QTY"]);

                        if (_drT["WH_OLD"] == System.DBNull.Value)
                            _drT["WH_OLD"] = _wh_Old;
                        _drT["QTY_OLD"] = _qty_Old;
                        if (_drT["EST_OLD"] == System.DBNull.Value)
                            _drT["EST_OLD"] = _est_DD_Old;
                        int _prdt_Rate = Convert.ToInt32(_qty_Old / _box_Qty_Old);
                        _drT["QTY"] = _prdt_Rate * qty;
                        _drT["WH"] = wh;
                        _drT["AMTN"] = _prdt_Rate * qty * Convert.ToDecimal(_drT["UP"]);
                        _drT["EST_DD"] = est_DD;
                        _drT["EST_OLD"] = _drT["EST_DD", System.Data.DataRowVersion.Original];
                    }
                }
                #endregion
                if (_isChg)
                {
                    foreach (DataRow _drM in _dtM.Rows)
                    {
                        _drM["CLS_ID"] = "T";
                    }
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

        /// <summary>
        /// 修改单据类别(For Auditing)
        /// </summary>
        /// <param name="yh_id"></param>
        /// <param name="yh_No"></param>
        /// <param name="bilType"></param>
        /// <returns></returns>
        public string UpdateBilType(string yh_id, string yh_No, string bilType)
        {
            SunlikeDataSet _ds = _dbDrpyh.GetYH(yh_id, yh_No);
            DataTable _dtMF = _ds.Tables["MF_DYH"];
            string _error = "";
            if (_dtMF.Rows.Count > 0)
            {
                try
                {
                    _dtMF.Rows[0]["BIL_TYPE"] = bilType;
                    this.UpdateDataSet(_ds);
                    DataTable _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
                    if (_errorDt.Rows.Count > 0)
                    {
                        foreach (DataRow _dr in _errorDt.Rows)
                        {
                            _error += _dr["TableName"].ToString() + ":" + _dr["REM"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _error += ex.Message;
                }
            }
            return _error;
        }

        #region Update
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            if (tableName == "MF_DYH")
            {
                //#region 审核关联
                //AudParamStruct _aps ;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["YH_DD"]);
                //    _aps.BIL_ID = "YH";
                //    _aps.BIL_NO = dr["YH_NO"].ToString();
                //    _aps.BIL_TYPE = "FX";
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = "";
                //    _aps.USR = dr["USR"].ToString();
                //    //_aps.MOB_ID = ""; //新加的部分，对应审核模板
                //}
                //else
                //    _aps = new AudParamStruct("YH", Convert.ToString(dr["YH_NO", DataRowVersion.Original]));

                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_ifEnterAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
            
            base.BeforeUpdate(tableName, statementType, dr, ref status);
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
            #region 新增
            if (tableName == "MF_DYH" && statementType == StatementType.Insert)
            {
                string _yh_No = dr["YH_NO"].ToString();
                string _dataTime = dr["YH_DD"].ToString();
                string _cus_No = dr["USR"].ToString();
                string _dept = dr["DEP"].ToString();
                if (!_ifEnterAuditing)
                {                   
                    Sunlike.Business.DrpSO _drpso = new DrpSO();
                    try
                    {
                        _drpso.InsertDrpSO(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                    }
                    catch (Exception _ex)
                    {
                        //						dr.SetColumnError("YH_NO","受订失败！");
                        //						status = UpdateStatus.SkipCurrentRow;
                        //dr.RowError = "受订失败！";
                        throw _ex;
                    }
                }
            }
            #endregion
            #region 更新
            if (_isUpdate)
            {
                SunlikeDataSet _ds = SunlikeDataSet.ConvertTo(dr.Table.DataSet).Copy();
                _ds.AcceptChanges();
                string _yh_No = _ds.Tables["MF_DYH"].Rows[0]["YH_NO"].ToString();
                string _usr = _ds.Tables["MF_DYH"].Rows[0]["USR"].ToString();
                _auditing = new Auditing();
                bool _runAuding = false;
                DataRow _dr = _ds.Tables["MF_DYH"].Rows[0];
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
                //_runAuding = _auditing.IsRunAuditing("YH", _usr, _bilType,_mobID);

                if (!_runAuding)
                {
                    Sunlike.Business.DrpSO _drpso = new DrpSO();
                    string _error = "";
                    _error = _drpso.DeleteDrpSO("YH", _yh_No);
                    if (!String.IsNullOrEmpty(_error))
                        throw new SunlikeException(_error);
                    try
                    {
                        _drpso.InsertDrpSO(_ds);
                    }
                    catch //(Exception _ex)
                    {
                        status = UpdateStatus.SkipAllRemainingRows;
                        dr.RowError = "受订失败！";
                    }
                }
                _isUpdate = false;
            }
            #endregion
            #region 删除
            if (_isDelete)
            {
                string _error = "";
                Sunlike.Business.DrpSO _drpso = new DrpSO();
                _error = _drpso.DeleteDrpSO("YH", _YH_No);
                if (!String.IsNullOrEmpty(_error))
                    throw new SunlikeException(_error);
                _isDelete = false;
            }
            #endregion
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        protected override void BeforeDsSave(DataSet ds)
        {
            //#region 单据追踪
            //DataTable _dt = ds.Tables[0];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["YH_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["YH_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}

            //#endregion

        }
        #endregion

        #region IAuditingInfo Members
        /// <summary>
        /// 审核数量
        /// </summary>
        /// <param name="Bil_Id"></param>
        /// <param name="Bil_No"></param>
        /// <param name="RejectSH"></param>
        /// <returns></returns>
        public string GetBillInfo(string Bil_Id, string Bil_No, ref bool RejectSH)
        {
            StringBuilder _error = new StringBuilder("RCID=");
            RejectSH = false;
            string _cus_No = "";
            Sunlike.Business.Cust _cust = new Cust();
            Sunlike.Business.Users _usr = new Users();
            DateTime _est_DD = System.DateTime.Now;
            try
            {
                int POI_AMT = 2;
                decimal _arp_AMTN = 0;
                DataTable _dtM = _dbDrpyh.GetMF_DYH(Bil_Id, Bil_No, Comp.CompNo);
                foreach (DataRow _drM in _dtM.Rows)
                {
                    _cus_No = _drM["CUS_NO"].ToString();
                    break;
                }

                //前期应收款
                DataTable _dtHis = _cust.GetCust_History(_cus_No, System.DateTime.Now, System.DateTime.Now);
                foreach (DataRow _dr in _dtHis.Rows)
                {
                    _arp_AMTN = Convert.ToDecimal(_dr["ARP_AMTN"].ToString());
                }
                _error.Append("CUSTINFO.PREWOM_AMTN;:;" + String.Format("{0:F" + POI_AMT + "}", _arp_AMTN) + ";");
                //金额位数
                CompInfo _compInfo = Comp.GetCompInfo(_usr.GetUserDepNo(_cus_No));
                POI_AMT = _compInfo.DecimalDigitsInfo.System.POI_AMT;
                DataTable _dtT = _dbDrpyh.GetTF_DYH(Bil_Id, Bil_No);
                decimal _amtn = 0;
                foreach (DataRow _drT in _dtT.Rows)
                {
                    _amtn += Convert.ToDecimal(_drT["AMTN"].ToString());
                    _est_DD = Convert.ToDateTime(_drT["EST_DD"]);
                }

                #region 客户信息额度
                decimal _lim_NR = _cust.GetLim_NR(_cus_No);
                _error.Append("CUST.CAN_USE_LIM_NR;");
                _error.Append(":" + String.Format("{0:F" + POI_AMT + "}", Convert.ToDecimal(_lim_NR - _amtn)) + ";");

                if ((_lim_NR - _amtn) < 0)
                {
                    _error.Append("CUST.LIM_NR_ALERT;");
                }
                #endregion

                #region 判断是否停止向客户发货
                bool _stop_Order = _cust.GetStop_Order(_cus_No);
                if (_stop_Order)
                {
                    _error.Append("CUST.STOP_ORDER_ALERT;");
                }
                #endregion
                #region 可用库存量
                DataTable _dtT1 = _dbDrpyh.GetYH_Box(Bil_No);
                if (_dtT1.Rows.Count > 0)
                {
                    Sunlike.Business.BarBox _barbox = new BarBox();
                    foreach (DataRow _dr in _dtT1.Rows)
                    {
                        string _prd_No = _dr["PRD_NO"].ToString();
                        string _prd_Name = _dr["NAME"].ToString();
                        string _content = _dr["CONTENT"].ToString();
                        string _wh = _dr["WH"].ToString();
                        decimal _qty = Convert.ToDecimal(_dr["QTY"].ToString());
                        decimal _wh_Qty = 0;
                        if (!_barbox.CheckQty(_prd_No, _content, _wh, _qty, out _wh_Qty))
                        {
                            if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                            {
                                if (_error.ToString().IndexOf("INV.HINT.WH_QTY_ALERT0;:;") == -1)
                                    _error.Append("INV.HINT.WH_QTY_ALERT0;");
                            }
                            else
                            {
                                if (_error.ToString().IndexOf("INV.HINT.WH_QTY_ALERT1;:;") == -1)
                                    _error.Append("INV.HINT.WH_QTY_ALERT1;");
                            }
                            _error.Append("COMMON.HINT.PRD_NO;:;" + _prd_No + "-" + _prd_Name + "; ;INV.BAR_BOX.CONTENT;:;" + _content + "; ;INV.PRDT1.QTY;:;" + _wh_Qty.ToString() + ";INV.TF_DYH.EST_DD;:;" + _est_DD.ToShortDateString() + ";");
                        }
                    }
                }
                else
                {
                    Sunlike.Business.WH _clswh = new WH();
                    foreach (DataRow _dr in _dtT.Rows)
                    {
                        string _prd_No = _dr["PRD_NO"].ToString();
                        string _prd_Name = _dr["NAME"].ToString();
                        string _prd_Mark = _dr["PRD_MARK"].ToString();
                        string _wh = _dr["WH"].ToString();
                        decimal _qty = Convert.ToDecimal(_dr["QTY"].ToString());
                        decimal _wh_Qty = 0;
                        if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                        {
                            //_wh_Qty = Convert.ToDecimal(_clswh.GetQty(false,_prd_No , _prd_Mark , _wh));
                            _wh_Qty = Convert.ToDecimal(_clswh.GetQty(false, _prd_No, _prd_Mark, _wh, String.Empty));
                        }
                        else
                        {
                            //_wh_Qty = Convert.ToDecimal(_clswh.GetQty(true,_prd_No , _prd_Mark , _wh));
                            _wh_Qty = Convert.ToDecimal(_clswh.GetQty(true, _prd_No, _prd_Mark, _wh, String.Empty));
                        }
                        if (_qty > _wh_Qty)
                        {
                            if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                            {
                                if (_error.ToString().IndexOf("INV.HINT.WH_QTY_ALERT0;:;") == -1)
                                    _error.Append("INV.HINT.WH_QTY_ALERT0;");
                            }
                            else
                            {
                                if (_error.ToString().IndexOf("INV.HINT.WH_QTY_ALERT1;:;") == -1)
                                    _error.Append("INV.HINT.WH_QTY_ALERT1;");
                            }
                            _error.Append("COMMON.HINT.PRD_NO;:;" + _prd_No + "-" + _prd_Name + "; ;INV.PRDT1.QTY;:;" + _wh_Qty.ToString() + ";INV.TF_DYH.EST_DD;:;" + Convert.ToDateTime(_dr["EST_DD"]).ToShortDateString() + ";");
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                _error = new StringBuilder(ex.Message.ToString());
            }
            return _error.ToString();
        }

        #endregion
    }
}
