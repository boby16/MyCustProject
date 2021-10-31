using System;
using System.Data;
using Sunlike.Business.Data;
using System.Collections;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;
using System.Collections.Generic;

namespace Sunlike.Business
{
    /// <summary>
    /// Summary description for DRPYHut.
    /// </summary>
    public class DRPYHut : Sunlike.Business.BizObject, IAuditing, IAuditingInfo, ICloseBill
    {
        #region Variable
        private DbDRPYHut dbYh;
        private bool bMemorySave;
        private bool bRunAuditing;
        private string yhId4Del = "";
        private string yhNo4Del = "";
        private System.Data.StatementType _state = new StatementType();
        #endregion

        #region Create & Free
        /// <summary>
        /// Create
        /// </summary>
        public DRPYHut()
        {
            dbYh = new DbDRPYHut(Comp.Conn_DB);
        }

        #endregion

        #region GetData
        /// <summary>
        /// ȡ��DataSet
        /// </summary>
        /// <param name="yhId">���ݱ�</param>
        /// <param name="yhNo">����</param>
        /// <param name="usr">�û���</param>
        /// <param name="pgm">�������</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string yhId, string yhNo, string usr, string pgm)
        {
            SunlikeDataSet _yhDS = dbYh.GetData(yhId, yhNo);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _yhDS.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            if (_yhDS.Tables["MF_DYH"].Rows.Count > 0)
            {
                string _bill_Dep = _yhDS.Tables["MF_DYH"].Rows[0]["DEP"].ToString();
                string _bill_Usr = _yhDS.Tables["MF_DYH"].Rows[0]["USR"].ToString();
                System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                _yhDS.ExtendedProperties["UPD"] = _billRight["UPD"];
                _yhDS.ExtendedProperties["DEL"] = _billRight["DEL"];
                _yhDS.ExtendedProperties["PRN"] = _billRight["PRN"];
            }
            DataTable _dtBody = _yhDS.Tables["TF_DYH"];
            _dtBody.Columns["KEY_ITM"].AutoIncrement = true;
            _dtBody.Columns["KEY_ITM"].AutoIncrementSeed = _dtBody.Rows.Count > 0 ? Convert.ToInt32(_dtBody.Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _dtBody.Columns["KEY_ITM"].AutoIncrementStep = 1;
            _dtBody.Columns["KEY_ITM"].Unique = true;
            DataTable _dtBox = _yhDS.Tables["TF_DYH1"];
            _dtBox.Columns["KEY_ITM"].AutoIncrement = true;
            _dtBox.Columns["KEY_ITM"].AutoIncrementSeed = _dtBox.Rows.Count > 0 ? Convert.ToInt32(_dtBox.Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _dtBox.Columns["KEY_ITM"].AutoIncrementStep = 1;
            _dtBox.Columns["KEY_ITM"].Unique = true;
            SetCanModify(_yhDS, false, true);
            return _yhDS;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yhId">���ݱ�</param>
        /// <param name="yhNo">����</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string yhId, string yhNo)
        {
            return dbYh.GetData(yhId, yhNo);
        }
        /// <summary>
        /// ����Ƿ�����޸�
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isRollback"></param>
        /// <param name="bCheckAuditing"></param>
        private void SetCanModify(SunlikeDataSet ds, bool isRollback, bool bCheckAuditing)
        {
            DataTable _dtMf = ds.Tables["MF_DYH"];
            DataTable _dtTf = ds.Tables["TF_DYH"];
            if (_dtMf.Rows.Count > 0)
            {
                bool _bCanModify = true;
                if (_bCanModify && Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["YH_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                }
                if (_bCanModify && bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_dtMf.Rows[0]["YH_ID"].ToString(), _dtMf.Rows[0]["YH_NO"].ToString()))
                    {
                        _bCanModify = false;
                    }
                }

                if (_bCanModify && _dtMf.Rows[0]["YH_ID"].ToString() == "YH")
                {
                    DrpSO _drpSo = new DrpSO();
                    _bCanModify = _drpSo.CheckOsModify(_dtMf.Rows[0]["YH_NO"].ToString());
                }
                if (_bCanModify && String.IsNullOrEmpty(_dtMf.Rows[0]["YH_ID"].ToString()))
                {
                    if (_dtTf.Select("ISNULL(QTY_SO, 0) + ISNULL(QTY_SO_UNSH, 0) > 0").Length > 0)
                    {
                        _bCanModify = false;
                    }
                }
                if (_dtMf.Rows[0]["CLS_ID"].ToString() == "T")
                {
                    _bCanModify = false;
                }
                ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            }
        }
        /// <summary>
        /// ��ѯ�ܶ�����
        /// </summary>
        /// <param name="yhNo">Ҫ������</param>
        /// <returns></returns>
        public string GetOsNo(string yhNo)
        {
            DrpSO _drpSo = new DrpSO();
            SunlikeDataSet _ds = _drpSo.GetOsNo4YH(yhNo);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                return _ds.Tables[0].Rows[0]["OS_NO"].ToString();
            }
            return "";
        }
        #endregion

        #region SetData
        #region ��ͷ
        /// <summary>
        /// �޸ı�ͷ
        /// </summary>
        /// <param name="ds">�����DataSet</param>
        /// <param name="yhId">���ݱ�</param>
        /// <param name="yhNo">Ҫ������</param>
        /// <param name="yhDd">Ҫ������</param>
        /// <param name="cusNo">�ͻ�����</param>
        /// <param name="estDd">Ԥ����</param>
        /// <param name="fxWh">������λ</param>
        /// <param name="wh">Ҫ����λ</param>
        /// <param name="rem">��ע���˻�ԭ��</param>
        /// <param name="usr">���</param>
        /// <param name="fuzzyId">�Ƿ�������</param>
        /// <param name="mobid">SHģ��</param>
        public void SetHeaderData(DataSet ds, string yhId, string yhNo, DateTime yhDd, string cusNo, DateTime estDd, string fxWh,
            string wh, string rem, string usr, string fuzzyId, string mobid)
        {
            try
            {
                if (fxWh == wh && !String.IsNullOrEmpty(fxWh) && !String.IsNullOrEmpty(wh))
                {
                    if (yhId == "YI")
                    {
                        throw new SunlikeException("RCID=INV.HINT.THESAMEWH");
                    }
                    else
                    {
                        throw new SunlikeException("RCID=INV.HINT.THESAMEWH1");
                    }
                }
                if (!String.IsNullOrEmpty(cusNo) && String.IsNullOrEmpty(fxWh))
                {
                    throw new SunlikeException("RCID=INV.HINT.NOTFXCUST");
                }
                Users _users = new Users();
                DataTable _mfDt = ds.Tables["MF_DYH"];
                if (_mfDt.Rows.Count > 0)
                {
                    DataRow _dr = _mfDt.Rows[0];
                    _dr["YH_ID"] = yhId;
                    _dr["YH_NO"] = yhNo;
                    _dr["YH_DD"] = yhDd;
                    _dr["CUS_NO"] = cusNo;
                    _dr["EST_DD"] = estDd;
                    _dr["FX_WH"] = fxWh;
                    _dr["WH"] = wh;
                    _dr["DEP"] = _users.GetUserDepNo(usr);
                    _dr["REM"] = Sunlike.Common.CommonVar.StringHelper.Substring(rem, 0, 50);
                    _dr["FUZZY_ID"] = fuzzyId;
                    _dr["MOB_ID"] = mobid;
                }
                else
                {
                    DataRow _dr = _mfDt.NewRow();
                    _dr["YH_ID"] = yhId;
                    _dr["YH_NO"] = yhNo;
                    _dr["YH_DD"] = DateTime.Today;
                    _dr["CUS_NO"] = cusNo;
                    _dr["EST_DD"] = DateTime.Today;
                    _dr["FX_WH"] = fxWh;
                    _dr["WH"] = wh;
                    _dr["USR"] = usr;
                    _dr["DEP"] = _users.GetUserDepNo(usr);
                    _dr["REM"] = Sunlike.Common.CommonVar.StringHelper.Substring(rem, 0, 50);
                    _dr["FUZZY_ID"] = fuzzyId;
                    _dr["SYS_DATE"] = System.DateTime.Now.ToShortDateString();
                    _dr["MOB_ID"] = mobid;
                    _mfDt.Rows.Add(_dr);
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion

        #region ��Ʒ����
        /// <summary>
        /// ����Ҫ��(����)
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="estDd"></param>
        /// <param name="qty"></param>
        /// <param name="ut"></param>
        /// <param name="rem"></param>
        /// <param name="qtyFact">ʵ�ʿ��</param>
        public void InsertYHByPrdt(DataSet ds, string yhNo, string prdNo, string prdMark, DateTime estDd,
            decimal qty, string ut, string rem, decimal qtyFact)
        {
            DataTable _mfDt = ds.Tables["MF_DYH"];
            DataTable _tfDt = ds.Tables["TF_DYH"];
            if (qty == 0)
            {
                throw new Exception("RCID=INV.HINT.QTY_RALERT");
            }
            if (Convert.ToDateTime(_mfDt.Rows[0]["YH_DD"]) > estDd)
            {
                throw new Exception("RCID=INV.HINT.EST_DD_ERROR");
            }
            //ȡ��Ҫ����λ��Ϣ
            DataTable _dtWhInfo = new DataTable();
            string[] _aryWh = null;
            string _cusNo = _mfDt.Rows[0]["CUS_NO"].ToString();
            //�ӱ�ͷȡ��Ҫ����λ
            string _wh = _mfDt.Rows[0]["WH"].ToString();
            if (String.IsNullOrEmpty(_wh))
            {
                throw new SunlikeException("RCID=COMMON.HINT.WHNULL");
            }
            DataTable _dtWh = GetMfWh(_wh);
            _aryWh = new string[_dtWh.Rows.Count];
            for (int i = 0; i < _dtWh.Rows.Count; i++)
            {
                _aryWh[i] = _dtWh.Rows[i]["WH"].ToString();
            }
            _dtWhInfo = GetPrdtQtyInfo(_aryWh, prdNo, prdMark);
            //ȡ�����Ҫ����
            decimal _totalQty = 0;
            foreach (DataRow dr in _dtWhInfo.Rows)
            {
                _totalQty += Convert.ToDecimal(dr["QTY"]);
            }

            DbDRPYHut _dbYh = new DbDRPYHut(Comp.Conn_DB);
            decimal _chkQty = _dbYh.ChkQtyByPrdt(_tfDt, "YH", yhNo, prdNo, prdMark, "", qty);

            DataRow[] _aryDr = _tfDt.Select("PRD_NO='" + prdNo + "' AND PRD_MARK='" + prdMark + "' AND EST_DD='" + estDd.ToString(Comp.SQLDateFormat) + "' AND REM='" + rem + "'  AND ISNULL(DEL_ID,'') <> 'T'");

            //ȡ�۸�
            Sunlike.Business.UP_DEF _upDef = new UP_DEF();
            string _order = "";
            decimal _qty = 1;
            decimal _up = 0;
            decimal _disCnt = 0;
            string _upType = "";
            if (!string.IsNullOrEmpty(Comp.DRP_Prop["DRPYH_GETPRICE"].ToString()))
            {
                _order = Comp.DRP_Prop["DRPYH_GETPRICE"].ToString();
            }
            if (string.IsNullOrEmpty(_order))
            {
                _order = "2";
            }

            Cust _cust = new Cust();
            string _cusAre = "", _knd = "";
            SunlikeDataSet _dsCust = new SunlikeDataSet();
            _dsCust.Merge(_cust.GetData(_mfDt.Rows[0]["CUS_NO"].ToString()));
            DataTable _dtCust = _dsCust.Tables["CUST"];
            if (_dtCust.Rows.Count > 0)
            {
                _cusAre = _dtCust.Rows[0]["CUS_ARE"].ToString();
            }
            Prdt _prdt = new Prdt();
            DataTable _dtPrdt = _prdt.GetPrdt(prdNo);
            if (_dtPrdt.Rows.Count > 0)
            {
                _knd = _dtPrdt.Rows[0]["IDX1"].ToString();
            }



            //ԭ�������ϣ����ۼ�������Ȼ��ɾ��
            decimal _sumQty = qty;
            foreach (DataRow dr in _aryDr)
            {
                _sumQty += Convert.ToDecimal(dr["QTY"]);
            }

            if (Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString() == "F")
            {
                //��Ҫ�жϿ��
                if (_chkQty > _totalQty)
                {
                    if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                    {
                        throw new SunlikeException("RCID=INV.HINT.YHQTYUP");//���ÿ�治��
                    }
                    else
                    {
                        throw new SunlikeException("RCID=INV.HINT.YHQTYUP1");//ʵ�ʿ�治��
                    }
                }
                //������ӣ�ɾ��ԭ������
                foreach (DataRow dr in _aryDr)
                {
                    dr.Delete();
                }
                foreach (DataRow dr in _dtWhInfo.Rows)
                {
                    decimal _currentQty = Convert.ToDecimal(dr["QTY"]);
                    //��ȥ�Ѿ��Ӹÿ�λҪ�˵�����
                    DataRow[] _aryDrWhQty = _tfDt.Select("PRD_NO='" + prdNo + "' AND PRD_MARK='" + prdMark + "' AND WH='" + dr["WH"].ToString() + "' AND ISNULL(DEL_ID,'') <> 'T'");
                    foreach (DataRow drWhQty in _aryDrWhQty)
                    {
                        _currentQty -= Convert.ToDecimal(drWhQty["QTY"]);
                    }
                    if (_currentQty != 0)
                    {
                        if (_currentQty >= _sumQty)
                        {
                            //_upDef.GetSale(_order, "YH", Convert.ToDateTime(_mfDt.Rows[0]["YH_DD"]), "", 0, _mfDt.Rows[0]["CUS_NO"].ToString(), null, 1, _cusAre, dr["WH"].ToString(),
                                //prdNo, prdMark, null, ut, null, _knd, _sumQty, out _up, out _disCnt, out _upType);
                            //Ŀǰ��λ����㹻��ȫ����Ŀǰ��λҪ��
                            InsertBodyData(ds, "YH", yhNo, prdNo, prdMark, dr["WH"].ToString(), estDd, _sumQty, ut,
                                _sumQty * _up, _up, rem, 0);
                            _sumQty = 0;
                        }
                        else
                        {
                            if (_currentQty > 0)
                            {
                                //_upDef.GetSale(_order, "YH", Convert.ToDateTime(_mfDt.Rows[0]["YH_DD"]), "", 0, _mfDt.Rows[0]["CUS_NO"].ToString(), null, 1, _cusAre, dr["WH"].ToString(),
                                //    prdNo, prdMark, null, ut, null, _knd, _currentQty, out _up, out _disCnt, out _upType);
                                //Ŀǰ��λ��治���㹻���ж�Ŀǰ��λ����Ƿ�Ϊ0��������ǣ���Ŀǰ��λҪһ����
                                InsertBodyData(ds, "YH", yhNo, prdNo, prdMark, dr["WH"].ToString(), estDd, _currentQty, ut,
                                    _currentQty * _up, _up, rem, 0);
                                _sumQty -= _currentQty;
                            }
                        }
                    }
                    if (_sumQty <= 0)
                    {
                        break;
                    }
                }
            }
            else
            {
                //������ӣ�ɾ��ԭ������
                foreach (DataRow dr in _aryDr)
                {
                    dr.Delete();
                }
                //_upDef.GetSale(_order, "YH", Convert.ToDateTime(_mfDt.Rows[0]["YH_DD"]), "", 0, _mfDt.Rows[0]["CUS_NO"].ToString(), null, 1, _cusAre, _dtWhInfo.Rows[0]["WH"].ToString(),
                //    prdNo, prdMark, null, ut, null, _knd, qty, out _up, out _disCnt, out _upType);
                //����Ҫ�жϿ��,ֱ�Ӵӵ�һ����λҪ��
                InsertBodyData(ds, "YH", yhNo, prdNo, prdMark, _dtWhInfo.Rows[0]["WH"].ToString(), estDd, _sumQty, ut,
                    qty * _up, _up, rem, 0);
            }

        }
        /// <summary>
        /// ����Ҫ��(�޸�)
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhNo"></param>
        /// <param name="itm"></param>
        /// <param name="qty"></param>
        /// <param name="ut"></param>
        /// <param name="wh"></param>
        /// <param name="estDd"></param>
        /// <param name="rem"></param>
        /// <param name="qtyFact">ʵ�ʿ��</param>
        public void UpdateYHByPrdt(DataSet ds, string yhNo, int itm, decimal qty, string ut, string wh, DateTime estDd, string rem, decimal qtyFact)
        {
            try
            {
                DataTable _mfDt = ds.Tables["MF_DYH"];
                DataTable _tfDt = ds.Tables["TF_DYH"];
                if (qty == 0)
                {
                    throw new Exception("RCID=INV.HINT.QTY_RALERT");
                }
                if (Convert.ToDateTime(_mfDt.Rows[0]["YH_DD"]) > estDd)
                {
                    throw new Exception("RCID=INV.HINT.EST_DD_ERROR");
                }
                DataRow[] _arytfDr = _tfDt.Select("YH_NO='" + yhNo + "' AND ITM=" + itm);
                if (_arytfDr.Length > 0)
                {
                    DataRow _tfDr = _arytfDr[0];
                    string _prdNo = _tfDr["PRD_NO"].ToString();
                    string _prdMark = _tfDr["PRD_MARK"].ToString();
                    decimal _totalQty = qtyFact;

                    DbDRPYHut _dbYh = new DbDRPYHut(Comp.Conn_DB);
                    decimal _chkQty = _dbYh.ChkQtyByPrdt(_tfDt, "YH", yhNo, _prdNo, _prdMark, itm.ToString(), qty);

                    if (Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString() == "F")
                    {
                        //��Ҫ�жϿ��
                        if (_chkQty > _totalQty)
                        {
                            if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                            {
                                throw new SunlikeException("RCID=INV.HINT.YHQTYUP");//���ÿ�治��
                            }
                            else
                            {
                                throw new SunlikeException("RCID=INV.HINT.YHQTYUP1");//ʵ�ʿ�治��
                            }
                        }
                    }
                    //�޸������ݺͲ�Ʒ����
                    UpdateBodyData(ds, "YH", yhNo, itm, _prdNo, _prdMark, wh, estDd, qty, ut, 0, 0, rem, 0);
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// �����˻�(����)
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="yhNo">�˻�����</param>
        /// <param name="prdNo">Ʒ��</param>
        /// <param name="prdMark">����</param>
        /// <param name="qty">����</param>
        /// <param name="ut">��λ</param>
        /// <param name="wh">��λ</param>
        /// <param name="estDd">Ԥ����</param>
        /// <param name="rem">��ע</param>
        public void InsertYIByPrdt(SunlikeDataSet ds, string yhNo, string prdNo, string prdMark,
            decimal qty, string ut, string wh, DateTime estDd, string rem)
        {
            if (qty == 0)
            {
                throw new Exception("RCID=INV.HINT.QTY_RALERT");
            }
            DataTable _mfDt = ds.Tables["MF_DYH"];
            DataTable _tfDt = ds.Tables["TF_DYH"];

            string _cusNo = _mfDt.Rows[0]["CUS_NO"].ToString();
            string _fxWh = _mfDt.Rows[0]["FX_WH"].ToString();

            //ȡ�۸�
            Sunlike.Business.UP_DEF _upDef = new UP_DEF();
            string _order = "";
            decimal _qty = 1;
            decimal _up = 0;
            decimal _disCnt = 0;
            if (!string.IsNullOrEmpty(Comp.DRP_Prop["DRPYH_GETPRICE"].ToString()))
            {
                _order = Comp.DRP_Prop["DRPYH_GETPRICE"].ToString();
            }
            if (string.IsNullOrEmpty(_order))
            {
                _order = "2";
            }

            string _sqlPrdMark = "";
            if (prdMark != null)//��PRD_MARK�˻�
            {
                _sqlPrdMark = " AND PRD_MARK='" + prdMark + "'";
            }
            DataRow[] _aryDr = _tfDt.Select("PRD_NO='" + prdNo + "' AND EST_DD='" + estDd.ToString(Comp.SQLDateFormat) + "'" + _sqlPrdMark + " AND WH='" + wh + "'");
            decimal _sumQty = qty;
            foreach (DataRow dr in _aryDr)
            {
                _sumQty += Convert.ToDecimal(dr["QTY"]);
            }

            DbDRPYHut _dbYh = new DbDRPYHut(Comp.Conn_DB);
            decimal _chkQty = _dbYh.ChkQtyByPrdt(_tfDt, "YI", yhNo, prdNo, prdMark, "", qty);

            //�ж�����
            Sunlike.Business.DRPYI _yi = new DRPYI();
            if (!_yi.IsReturn(_cusNo, prdNo, _sumQty))
            {
                throw new SunlikeException("RCID=INV.HINT.YIQTYUP");
            }

            //�жϿ��
            if (Comp.DRP_Prop["DRPYI_CHK_QTY"].ToString() == "F")
            {
                decimal _totalQty = this.GetWhQty(_fxWh, prdNo, prdMark);
                if (_chkQty > _totalQty)
                {
                    throw new SunlikeException("RCID=INV.HINT.NORETURN");
                }
            }
            //ɾ��ԭ������
            foreach (DataRow dr in _aryDr)
            {
                dr.Delete();
            }
            //���������
            InsertBodyData(ds, "YI", yhNo, prdNo, prdMark, wh, estDd, _sumQty, ut, _up * _sumQty, _up, rem, 0);
        }
        /// <summary>
        /// �����˻�(�޸�)
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="yhNo">�˻�����</param>
        /// <param name="itm">���</param>
        /// <param name="fuzzyId"></param>
        /// <param name="qty">����</param>
        /// <param name="ut">��λ</param>
        /// <param name="wh">��λ</param>
        /// <param name="estDd">Ԥ����</param>
        /// <param name="rem">��ע</param>
        public void UpdateYIByPrdt(SunlikeDataSet ds, string yhNo, int itm, string fuzzyId, decimal qty, string ut, string wh, DateTime estDd, string rem)
        {
            DataTable _mfDt = ds.Tables["MF_DYH"];
            DataTable _tfDt = ds.Tables["TF_DYH"];

            if (qty == 0)
            {
                throw new Exception("RCID=INV.HINT.QTY_RALERT");
            }
            string _cusNo = _mfDt.Rows[0]["CUS_NO"].ToString();
            string _fxWh = _mfDt.Rows[0]["FX_WH"].ToString();

            DataRow[] _aryDr = _tfDt.Select("ITM=" + itm.ToString());
            if (_aryDr.Length > 0)
            {
                string _prdNo = _aryDr[0]["PRD_NO"].ToString();
                string _prdMark = null;
                if (fuzzyId != "T")
                {
                    _prdMark = _aryDr[0]["PRD_MARK"].ToString();
                }

                //�ж�����
                Sunlike.Business.DRPYI _yi = new DRPYI();
                if (!_yi.IsReturn(_cusNo, _prdNo, qty))
                {
                    throw new SunlikeException("RCID=INV.HINT.YIQTYUP");
                }

                DbDRPYHut _dbYh = new DbDRPYHut(Comp.Conn_DB);
                decimal _chkQty = _dbYh.ChkQtyByPrdt(_tfDt, "YI", yhNo, _prdNo, _prdMark, itm.ToString(), qty);
                //�жϿ��
                if (Comp.DRP_Prop["DRPYI_CHK_QTY"].ToString() == "F")
                {
                    decimal _totalQty = this.GetWhQty(_fxWh, _prdNo, _prdMark);
                    if (_chkQty > _totalQty)
                    {
                        throw new SunlikeException("RCID=INV.HINT.NORETURN");
                    }
                }

                //��������
                UpdateBodyData(ds, "YI", yhNo, itm, _prdNo, _prdMark, wh, estDd, qty, ut, 0, 0, rem, 0);
            }
        }
        /// <summary>
        /// ������ӵ��ʲ�Ʒ����
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="wh"></param>
        /// <param name="estDd"></param>
        /// <param name="qty"></param>
        /// <param name="ut"></param>
        /// <param name="amtn"></param>
        /// <param name="up"></param>
        /// <param name="rem"></param>
        /// <param name="boxItm"></param>
        public void InsertBodyData(DataSet ds, string yhId, string yhNo, string prdNo, string prdMark, string wh, DateTime estDd,
            decimal qty, string ut, decimal amtn, decimal up, string rem, int boxItm)
        {
            try
            {
                if (Comp.DRP_Prop["DRPYH_CHK_UPNULL"].ToString() == "T")
                {
                    if (up == 0)
                    {
                        DeleteBodyData1(ds, boxItm);
                        throw new SunlikeException("RCID=INV.HINT.UPNULL");
                    }
                }
                //ȡ�ò�Ʒ��λ
                string _unit = "";
                if (String.IsNullOrEmpty(ut))
                {
                    _unit = GetPrdtUnit(prdNo);
                }
                else
                {
                    _unit = ut;
                }

                DataTable _tfDt = ds.Tables["TF_DYH"];
                DataRow _dr = _tfDt.NewRow();
                _dr["YH_ID"] = yhId;
                _dr["YH_NO"] = yhNo;
                _dr["ITM"] = GetMaxItm(_tfDt, "ITM");
                _dr["PRD_NO"] = prdNo;
                _dr["PRD_MARK"] = prdMark;
                _dr["WH"] = wh;
                _dr["EST_DD"] = estDd;
                _dr["QTY"] = qty;
                _dr["UNIT"] = _unit;
                _dr["AMTN"] = amtn;
                _dr["UP"] = up;
                if (rem.Length > 20)
                {
                    rem = rem.Substring(0, 20);
                }
                _dr["REM"] = rem;
                if (boxItm == 0)
                {
                    //��boxItmΪ��,���ǰ���Ҫ��
                    _dr["BOX_ITM"] = DBNull.Value;
                }
                else
                {
                    _dr["BOX_ITM"] = boxItm;
                }
                _tfDt.Rows.Add(_dr);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// ������ӵ��ʲ�Ʒ����(������������)
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="wh"></param>
        /// <param name="estDd"></param>
        /// <param name="qty"></param>
        /// <param name="qty_rtn"></param>
        /// <param name="ut"></param>
        /// <param name="amtn"></param>
        /// <param name="up"></param>
        /// <param name="rem"></param>
        /// <param name="boxItm"></param>
        public void InsertBodyData(SunlikeDataSet ds, string yhId, string yhNo, string prdNo, string prdMark, string wh, DateTime estDd,
            decimal qty, decimal qty_rtn, string ut, decimal amtn, decimal up, string rem, int boxItm)
        {
            try
            {
                if (Comp.DRP_Prop["DRPYH_CHK_UPNULL"].ToString() == "T")
                {
                    if (up == 0)
                    {
                        DeleteBodyData1(ds, boxItm);
                        throw new SunlikeException("RCID=INV.HINT.UPNULL");
                    }
                }
                //ȡ�ò�Ʒ��λ
                string _unit = "";
                if (String.IsNullOrEmpty(ut))
                {
                    _unit = GetPrdtUnit(prdNo);
                }
                else
                {
                    _unit = ut;
                }

                DataTable _tfDt = ds.Tables["TF_DYH"];
                DataRow _dr = _tfDt.NewRow();
                _dr["YH_ID"] = yhId;
                _dr["YH_NO"] = yhNo;
                _dr["ITM"] = GetMaxItm(_tfDt, "ITM");
                _dr["PRD_NO"] = prdNo;
                _dr["PRD_MARK"] = prdMark;
                _dr["WH"] = wh;
                _dr["EST_DD"] = estDd;
                _dr["QTY"] = qty;
                _dr["UNIT"] = _unit;
                _dr["AMTN"] = amtn;
                _dr["UP"] = up;
                _dr["QTY_RTN"] = qty_rtn;
                if (rem.Length > 20)
                {
                    rem = rem.Substring(0, 20);
                }
                _dr["REM"] = rem;
                if (boxItm == 0)
                {
                    //��boxItmΪ��,���ǰ���Ҫ��
                    _dr["BOX_ITM"] = DBNull.Value;
                }
                else
                {
                    _dr["BOX_ITM"] = boxItm;
                }
                _tfDt.Rows.Add(_dr);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// �޸ı�����
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="itm"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="wh"></param>
        /// <param name="estDd"></param>
        /// <param name="qty"></param>
        /// <param name="ut"></param>
        /// <param name="amtn"></param>
        /// <param name="up"></param>
        /// <param name="rem"></param>
        /// <param name="boxItm"></param>
        public void UpdateBodyData(DataSet ds, string yhId, string yhNo, int itm, string prdNo, string prdMark, string wh, DateTime estDd,
            decimal qty, string ut, decimal amtn, decimal up, string rem, int boxItm)
        {
            try
            {
                //ȡ�ò�Ʒ��λ
                string _unit = "";
                if (String.IsNullOrEmpty(ut))
                {
                    _unit = GetPrdtUnit(prdNo);
                }
                else
                {
                    _unit = ut;
                }

                DataTable _tfDt = ds.Tables["TF_DYH"];
                DataRow[] _aryDr = _tfDt.Select("YH_NO='" + yhNo + "' AND ITM=" + itm);
                if (_aryDr.Length > 0)
                {
                    DataRow _dr = _aryDr[0];
                    _dr["YH_ID"] = yhId;
                    _dr["YH_NO"] = yhNo;
                    _dr["PRD_NO"] = prdNo;
                    _dr["PRD_MARK"] = prdMark;
                    _dr["WH"] = wh;
                    _dr["EST_DD"] = estDd;
                    _dr["QTY"] = qty;
                    _dr["UNIT"] = _unit;
                    _dr["AMTN"] = amtn;
                    _dr["UP"] = up;
                    if (rem.Length > 20)
                    {
                        rem = rem.Substring(0, 20);
                    }
                    _dr["REM"] = rem;
                    if (boxItm == 0)
                    {
                        _dr["BOX_ITM"] = DBNull.Value;
                    }
                    else
                    {
                        _dr["BOX_ITM"] = boxItm;
                    }
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="itm"></param>
        public void DeleteBodyData(DataSet ds, int itm)
        {
            try
            {
                DataTable _tfDt = ds.Tables["TF_DYH"];
                DataRow[] _aryDr = _tfDt.Select("ITM=" + itm.ToString());
                if (_aryDr.Length > 0)
                {
                    _aryDr[0].Delete();
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion

        #region ������
        /// <summary>
        /// ����Ҫ��(����)
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="content"></param>
        /// <param name="qty"></param>
        /// <param name="estDd"></param>
        /// <param name="rem"></param>
        public void InserYHByBox(SunlikeDataSet ds, string yhNo, string prdNo, string content, decimal qty,
            DateTime estDd, string rem)
        {
            try
            {
                if (!this.ChkPmarkIsExist(prdNo, content))
                {
                    throw new SunlikeException("RCID=INV.HINT.PMARKNOTEXIST");
                }
                qty = Convert.ToDecimal(Convert.ToInt32(qty));
                if (qty == 0)
                {
                    throw new Exception("RCID=INV.HINT.QTY_RALERT");
                }
                DataTable _mfDt = ds.Tables["MF_DYH"];
                DataTable _tfDt1 = ds.Tables["TF_DYH1"];
                //ȡ��Ҫ����λ��Ϣ
                DataTable _dtWhInfo = new DataTable();
                string[] _aryWh = null;
                //�ӱ�ͷȡ��Ҫ����λ
                string _wh = _mfDt.Rows[0]["WH"].ToString();
                if (String.IsNullOrEmpty(_wh))
                {
                    throw new SunlikeException("RCID=COMMON.HINT.WHNULL");
                }
                DataTable _dtWh = GetMfWh(_wh);
                _aryWh = new string[_dtWh.Rows.Count];
                for (int i = 0; i < _dtWh.Rows.Count; i++)
                {
                    _aryWh[i] = _dtWh.Rows[i]["WH"].ToString();
                }
                _dtWhInfo = GetBoxQtyInfo(_aryWh, prdNo, content);
                //ȡ�����Ҫ����
                decimal _totalQty = 0;
                foreach (DataRow dr in _dtWhInfo.Rows)
                {
                    _totalQty += Convert.ToDecimal(dr["QTY"]);
                }

                DbDRPYHut _dbYh = new DbDRPYHut(Comp.Conn_DB);
                decimal _chkQty = _dbYh.ChkQtyByBox(_tfDt1, "YH", yhNo, prdNo, content, "", qty);

                DataRow[] _aryChkDr = _tfDt1.Select("PRD_NO='" + prdNo + "' AND CONTENT='" + content + "' AND EST_DD='" + estDd.ToString(Comp.SQLDateFormat) + "'");

                //��ͬ�����ź������,ͬʱ�ۼ�����
                decimal _sumQty = qty;
                foreach (DataRow dr in _aryChkDr)
                {
                    _sumQty += Convert.ToDecimal(dr["QTY"]);
                }

                if (Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString() == "F")
                {
                    //��Ҫ�жϿ��
                    if (_chkQty > _totalQty)
                    {
                        if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                        {
                            throw new SunlikeException("RCID=INV.HINT.YHQTYUP");//���ÿ�治��
                        }
                        else
                        {
                            throw new SunlikeException("RCID=INV.HINT.YHQTYUP1");//ʵ�ʿ�治��
                        }
                    }
                    //������ӣ�ɾ��ԭ������
                    foreach (DataRow dr in _aryChkDr)
                    {
                        this.DeleteBodyData1(ds, Convert.ToInt32(dr["KEY_ITM"]));
                    }
                    foreach (DataRow dr in _dtWhInfo.Rows)
                    {
                        decimal _currentQty = Convert.ToDecimal(dr["QTY"]);
                        if (_currentQty != 0)
                        {
                            if (_currentQty >= _chkQty)
                            {
                                //Ŀǰ��λ����㹻��ȫ����Ŀǰ��λҪ��
                                InsertBodyData1(ds, "YH", yhNo, prdNo, content, _sumQty, dr["WH"].ToString(), estDd, rem);
                                _sumQty = 0;
                            }
                            else
                            {
                                if (_currentQty > 0)
                                {
                                    //Ŀǰ��λ��治���㹻,�ж�Ŀǰ��λ����Ƿ����0��������ǣ����Ŀǰ��λҪһ����
                                    InsertBodyData1(ds, "YH", yhNo, prdNo, content, _currentQty, dr["WH"].ToString(), estDd, rem);
                                    _sumQty -= _currentQty;
                                }
                            }
                        }
                        if (_sumQty <= 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    //������ӣ�ɾ��ԭ������
                    foreach (DataRow dr in _aryChkDr)
                    {
                        this.DeleteBodyData1(ds, Convert.ToInt32(dr["KEY_ITM"]));
                    }
                    //����Ҫ�жϿ�棬ֱ��ȡ��һ����λҪ��
                    InsertBodyData1(ds, "YH", yhNo, prdNo, content, _sumQty, _aryWh[0], estDd, rem);
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// ����Ҫ��(�޸�)
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhNo"></param>
        /// <param name="itm"></param>
        /// <param name="qty"></param>
        /// <param name="wh"></param>
        /// <param name="estDd"></param>
        /// <param name="rem"></param>
        public void UpdateYHByBox(SunlikeDataSet ds, string yhNo, int itm, decimal qty, string wh, DateTime estDd, string rem)
        {
            try
            {
                qty = Convert.ToDecimal(Convert.ToInt32(qty));
                if (qty == 0)
                {
                    throw new Exception("RCID=INV.HINT.QTY_RALERT");
                }
                DataTable _tfDt1 = ds.Tables["TF_DYH1"];
                DataRow[] _arytfDr1 = _tfDt1.Select("YH_NO='" + yhNo + "' AND ITM=" + itm);
                if (_arytfDr1.Length > 0)
                {
                    DataRow _tfDr1 = _arytfDr1[0];
                    string[] _aryWh = new string[1] { wh };
                    string _prdNo = _tfDr1["PRD_NO"].ToString();
                    string _content = _tfDr1["CONTENT"].ToString();
                    DataTable _dtWhInfo = GetBoxQtyInfo(_aryWh, _prdNo, _content);
                    decimal _totalQty = 0;//���ÿ����
                    foreach (DataRow dr in _dtWhInfo.Rows)
                    {
                        _totalQty += Convert.ToDecimal(dr["QTY"]);
                    }

                    //�������ܶ���
                    if (!_tfDt1.Columns.Contains("QTY_SO"))
                    {
                        _tfDt1.Columns.Add(new DataColumn("QTY_SO", typeof(System.String)));
                    }
                    DataTable _dtTf = ds.Tables["TF_DYH"];
                    foreach (DataRow _drTf1 in _tfDt1.Rows)
                    {
                        if (_drTf1.RowState != DataRowState.Deleted)
                        {
                            DataRow[] _drTfAry = _dtTf.Select("BOX_ITM = '" + _drTf1["KEY_ITM"].ToString() + "'");
                            if (!String.IsNullOrEmpty(_drTfAry[0]["QTY_SO"].ToString()) && !String.IsNullOrEmpty(_drTfAry[0]["QTY"].ToString()))
                            {
                                _drTf1["QTY_SO"] = Convert.ToInt32(Convert.ToDecimal(_drTfAry[0]["QTY_SO"]) * (Convert.ToDecimal(_drTf1["QTY"]) / Convert.ToDecimal(_drTfAry[0]["QTY"])));
                            }
                            else
                            {
                                _drTf1["QTY_SO"] = 0;
                            }
                        }
                    }

                    DbDRPYHut _dbYh = new DbDRPYHut(Comp.Conn_DB);
                    decimal _chkQty = _dbYh.ChkQtyByBox(_tfDt1, "YH", yhNo, _prdNo, _content, itm.ToString(), qty);

                    if (Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString() == "F")
                    {
                        //��Ҫ�жϿ��
                        if (_chkQty > 0 && _chkQty > _totalQty)
                        {
                            if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                            {
                                throw new SunlikeException("RCID=INV.HINT.YHQTYUP");//���ÿ�治��
                            }
                            else
                            {
                                throw new SunlikeException("RCID=INV.HINT.YHQTYUP1");//ʵ�ʿ�治��
                            }
                        }
                    }
                    //�޸������ݺͲ�Ʒ����
                    UpdateBodyData1(ds, "YH", yhNo, itm, _prdNo, _content, qty, wh, estDd, rem);
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// ��������ݺͲ�Ʒ����
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="content"></param>
        /// <param name="qty"></param>
        /// <param name="wh"></param>
        /// <param name="estDd"></param>
        /// <param name="rem"></param>
        public void InsertBodyData1(SunlikeDataSet ds, string yhId, string yhNo, string prdNo, string content, decimal qty, string wh,
            DateTime estDd, string rem)
        {
            try
            {
                //����������
                DataTable _tfDt1 = ds.Tables["TF_DYH1"];
                int _itm = GetMaxItm(_tfDt1, "ITM");
                DataRow _dr = _tfDt1.NewRow();
                _dr["YH_ID"] = "YH";
                _dr["YH_NO"] = yhNo;
                _dr["ITM"] = _itm;
                _dr["PRD_NO"] = prdNo;
                _dr["CONTENT"] = content;
                _dr["QTY"] = qty;
                _dr["WH"] = wh;
                _dr["EST_DD"] = estDd;
                _tfDt1.Rows.Add(_dr);

                //������Ʒ����
                DataTable _dtCntInfo = GetMarkByContent(content);
                string _cusNo = ds.Tables["MF_DYH"].Rows[0]["CUS_NO"].ToString();
                DateTime _yhDd = Convert.ToDateTime(ds.Tables["MF_DYH"].Rows[0]["YH_DD"]);
                Sunlike.Business.UP_DEF _upDef = new UP_DEF();
                string _order = "";
                decimal _qty = 1;
                decimal _up = 0;
                decimal _disCnt = 0;
                if (!string.IsNullOrEmpty(Comp.DRP_Prop["DRPYH_GETPRICE"].ToString()))
                {
                    _order = Comp.DRP_Prop["DRPYH_GETPRICE"].ToString();
                }
                if (string.IsNullOrEmpty(_order))
                {
                    _order = "2";
                }
                foreach (DataRow dr in _dtCntInfo.Rows)
                {
                    //���������� ������*������
                    decimal _totalQty = Convert.ToInt32(dr["QTY"]) * qty;
                    if (_totalQty != 0)
                    {
                        InsertBodyData(ds, yhId, yhNo, prdNo, dr["PRD_MARK"].ToString(), wh, estDd, _totalQty, "",
                            _up * _totalQty, _up, rem, Convert.ToInt32(_dr["KEY_ITM"]));
                    }
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// �޸������ݺͲ�Ʒ����
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="itm"></param>
        /// <param name="prdNo"></param>
        /// <param name="content"></param>
        /// <param name="qty"></param>
        /// <param name="wh"></param>
        /// <param name="estDd"></param>
        /// <param name="rem"></param>
        public void UpdateBodyData1(SunlikeDataSet ds, string yhId, string yhNo, int itm, string prdNo, string content, decimal qty, string wh,
            DateTime estDd, string rem)
        {
            try
            {
                DataTable _tfDt1 = ds.Tables["TF_DYH1"];
                DataRow[] _aryBoxDr = _tfDt1.Select("YH_NO='" + yhNo + "' AND ITM=" + itm);
                if (_aryBoxDr.Length > 0)
                {
                    //�޸�������
                    DataRow _drBox = _aryBoxDr[0];
                    _drBox["PRD_NO"] = prdNo;
                    _drBox["CONTENT"] = content;
                    _drBox["QTY"] = qty;
                    _drBox["WH"] = wh;
                    _drBox["EST_DD"] = estDd;

                    //�޸Ĳ�Ʒ����
                    DataTable _tfDt = ds.Tables["TF_DYH"];
                    //��ı�ʶ
                    int _keyItm = Convert.ToInt32(_drBox["KEY_ITM"]);

                    //������Ʒ����
                    DataTable _dtCntInfo = GetMarkByContent(content);
                    string _cusNo = ds.Tables["MF_DYH"].Rows[0]["CUS_NO"].ToString();
                    DateTime _yhDd = Convert.ToDateTime(ds.Tables["MF_DYH"].Rows[0]["YH_DD"]);
                    Sunlike.Business.Upr4_def _upr4 = new Sunlike.Business.Upr4_def();
                    foreach (DataRow dr in _dtCntInfo.Rows)
                    {
                        //����������	������*������
                        decimal _totalQty = Convert.ToInt32(dr["QTY"]) * qty;
                        if (_totalQty != 0)
                        {
                            //ȡ�ò�ƷITM
                            DataRow[] _aryPrdtDr = _tfDt.Select("YH_NO='" + yhNo + "' AND BOX_ITM=" + _keyItm + " AND PRD_MARK = '" + dr["PRD_MARK"].ToString() + "'");

                            UpdateBodyData(ds, yhId, yhNo, Convert.ToInt32(_aryPrdtDr[0]["ITM"]), prdNo, dr["PRD_MARK"].ToString(), wh, estDd, _totalQty, "", 0, 0, rem, _keyItm);
                        }
                    }
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="itm"></param>
        public void DeleteBodyData1(DataSet ds, int itm)
        {
            try
            {
                DataTable _tfDt = ds.Tables["TF_DYH"];
                DataTable _tfDt1 = ds.Tables["TF_DYH1"];
                DataRow[] _aryDr1 = _tfDt1.Select("KEY_ITM=" + itm.ToString());
                if (_aryDr1.Length > 0)
                {
                    DataRow _tfDr1 = _aryDr1[0];
                    int _keyItm = Convert.ToInt32(_tfDr1["KEY_ITM"]);

                    //ɾ����Ʒ����
                    DataRow[] _aryDr = _tfDt.Select("BOX_ITM=" + _keyItm.ToString());
                    foreach (DataRow dr in _aryDr)
                    {
                        dr.Delete();
                    }

                    //ɾ��������
                    _tfDr1.Delete();
                }
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="ds"></param>
        public DataTable Delete(DataSet ds)
        {
            DataTable _mfDt = ds.Tables["MF_DYH"];
            DataTable _tfDt = ds.Tables["TF_DYH"];
            DataTable _tfDt1 = ds.Tables["TF_DYH1"];

            //ɾ����Ʒ����
            foreach (DataRow dr in _tfDt.Rows)
            {
                if (dr.RowState != DataRowState.Added && dr.RowState != DataRowState.Deleted)
                    dr.Delete();
            }
            //ɾ��������
            foreach (DataRow dr in _tfDt1.Rows)
            {
                if (dr.RowState != DataRowState.Added && dr.RowState != DataRowState.Deleted)
                    dr.Delete();
            }
            //ɾ����ͷ
            foreach (DataRow dr in _mfDt.Rows)
            {
                if (dr.RowState != DataRowState.Added && dr.RowState != DataRowState.Deleted)
                    dr.Delete();
            }
            return this.Save(ds, null);
        }
        #endregion
        #endregion

        #region Function
        /// <summary>
        /// ȡ��Ҫ������
        /// </summary>
        /// <returns></returns>
        public string GetYhNo(string yhId, string userId, DateTime dateTime)
        {
            string _yhNo = "YH00000000";
            if (yhId == "YI")
            {
                _yhNo = "YI00000000";
            }
            else if (yhId == "YC")
            {
                _yhNo = "YC00000000";
            }
            try
            {
                SQNO _sqlno = new SQNO();
                Users _users = new Users();
                _yhNo = _sqlno.Get(yhId, userId, _users.GetUserDepNo(userId), dateTime, "FX");
            }
            catch { }
            return _yhNo;
        }

        /// <summary>
        /// ȡ��������
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

        public void GetPrice(DataSet dataSet)
        {
            DataRow _drMf = dataSet.Tables["MF_DYH"].Rows[0];
            if (_drMf.RowState != DataRowState.Deleted)
            {
                DataTable _dt = dataSet.Tables["TF_DYH"];
                UP_DEF _upDef = new UP_DEF();
                decimal _qty = 0, _up = 0, _disCnt;
                string _cusAre = "", _knd = "", _order = "", _upType = "";
                if (!string.IsNullOrEmpty(Comp.DRP_Prop["DRPYH_GETPRICE"].ToString()))
                {
                    _order = Comp.DRP_Prop["DRPYH_GETPRICE"].ToString();
                }
                if (string.IsNullOrEmpty(_order))
                {
                    _order = "2";
                }
                Cust _cust = new Cust();
                Prdt _prdt = new Prdt();
                DataTable _dtPrdt = null;
                SunlikeDataSet _dsCust = new SunlikeDataSet();
                _dsCust.Merge(_cust.GetData(_drMf["CUS_NO"].ToString()));
                DataTable _dtCust = _dsCust.Tables["CUST"];
                if (_dtCust.Rows.Count > 0)
                {
                    _cusAre = _dtCust.Rows[0]["CUS_ARE"].ToString();
                }
                foreach (DataRow dr in _dt.Select())
                {
                    if (dr.RowState != DataRowState.Unchanged)
                    {
                        if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                        {
                            _qty = Convert.ToDecimal(dr["QTY"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                        _dtPrdt = _prdt.GetPrdt(dr["PRD_NO"].ToString());
                        if (_dtPrdt.Rows.Count > 0)
                        {
                            _knd = _dtPrdt.Rows[0]["IDX1"].ToString();
                        }
                        //_upDef.GetSale(_order, "YH", Convert.ToDateTime(_drMf["YH_DD"]), "", 0, _drMf["CUS_NO"].ToString(), null, 1, _cusAre, dr["WH"].ToString(),
                        //    dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString(), null, dr["UNIT"].ToString(), null, _knd, _qty, out _up, out _disCnt, out _upType);
                        dr["UP"] = _up;
                        //������
                        dr["AMTN"] = _qty * _up;
                    }
                }
            }
        }

        /// <summary>
        /// ����Contentȡ�ò�Ʒ����
        /// </summary>
        public DataTable GetMarkByContent(string content)
        {
            DataTable _resDt = new DataTable();
            _resDt.Columns.Add(new DataColumn("PRD_MARK", typeof(System.String)));
            _resDt.Columns.Add(new DataColumn("QTY", typeof(System.Decimal)));
            //ȥ��ĩβ�ķֺ�
            content = content.Substring(0, content.Length - 1);
            //ȡ��A1=2
            string[] _aryCnt = content.Split(';');
            foreach (string strCnt in _aryCnt)
            {
                string[] _aryPrdt = strCnt.Split('=');
                DataRow _dr = _resDt.NewRow();
                _dr["PRD_MARK"] = _aryPrdt[0];
                _dr["QTY"] = Convert.ToInt32(_aryPrdt[1]);
                _resDt.Rows.Add(_dr);
            }
            //����
            _resDt.AcceptChanges();
            return _resDt;
        }
        /// <summary>
        /// ȡ�ò�Ʒ��λ
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public string GetPrdtUnit(string prdNo)
        {
            Prdt _prdt = new Prdt();
            string _defUnit = "";
            _prdt.GetUnit(prdNo, out _defUnit);
            return _defUnit;
        }
        /// <summary>
        /// ȡ��Ĭ��Ҫ����λ
        /// </summary>
        /// <param name="cusNo">�ͻ�����</param>
        public DataTable GetDefWh(string cusNo)
        {
            Cust _cust = new Cust();
            //_cust.GetCust_YH_WH(cusNo, out _aryWh, out _aryWhName);
            SunlikeDataSet _ds = _cust.GetCustYhWh(cusNo);
            DataTable _dt = new DataTable("DT_DEFWH");
            _dt.Columns.Add(new DataColumn("WH", typeof(System.String)));
            _dt.Columns.Add(new DataColumn("NAME", typeof(System.String)));
            if (_ds.Tables["YH_WH"].Rows.Count > 0)
            {
                DataRow _drNew1 = _dt.NewRow();
                _drNew1["WH"] = _ds.Tables["YH_WH"].Rows[0]["YH_WH1"];
                _drNew1["NAME"] = _ds.Tables["YH_WH"].Rows[0]["YH_WH_NAME1"];

                DataRow _drNew2 = _dt.NewRow();
                _drNew2["WH"] = _ds.Tables["YH_WH"].Rows[0]["YH_WH2"];
                _drNew2["NAME"] = _ds.Tables["YH_WH"].Rows[0]["YH_WH_NAME2"];

                _dt.Rows.Add(_drNew1);
                _dt.Rows.Add(_drNew2);
            }
            _dt.AcceptChanges();
            return _dt;
        }
        /// <summary>
        /// ȡ��Ĭ��Ҫ����λ
        /// </summary>
        /// <param name="whs">��λ��</param>
        public DataTable GetMfWh(string whs)
        {
            DataTable _dt = new DataTable("DT_DEFWH");
            _dt.Columns.Add(new DataColumn("WH", typeof(System.String)));
            _dt.Columns.Add(new DataColumn("NAME", typeof(System.String)));
            WH _wh = new WH();
            string[] _aryWhNo = whs.Split(';');
            foreach (string wh in _aryWhNo)
            {
                DataRow _dr = _dt.NewRow();
                _dr["WH"] = wh;
                DataTable _dtWh = _wh.GetData(null, wh);
                if (_dtWh.Rows.Count > 0)
                {
                    _dr["NAME"] = _dtWh.Rows[0]["NAME"];
                }
                _dt.Rows.Add(_dr);
            }
            return _dt;
        }
        /// <summary>
        /// ȡ�ò�Ʒ���
        /// </summary>
        /// <param name="wh"></param>
        /// <param name="prdNo"></param>
        /// <param name="pmark"></param>
        /// <returns></returns>
        public decimal GetPrdtQty(string wh, string prdNo, string pmark)
        {
            WH _wh = new WH();
            if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
            {
                //���ÿ��
                //return _wh.GetQty(false, prdNo, pmark, wh);
                return _wh.GetQty(false, prdNo, pmark, wh, String.Empty);
            }
            else
            {
                //ʵ�ʿ��
                //return _wh.GetQty(true, prdNo, pmark, wh);
                return _wh.GetQty(true, prdNo, pmark, wh, String.Empty);
            }
        }
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="wh">��λ</param>
        /// <param name="prdNo">����</param>
        /// <param name="content">�����</param>
        /// <returns></returns>
        public DataTable GetBoxQtyInfo(string[] wh, string prdNo, string content)
        {
            //��ſ�λ�ź�����
            DataTable _dt = new DataTable("DT_WHQTY");
            _dt.Columns.Add(new DataColumn("WH", typeof(System.String)));
            _dt.Columns.Add(new DataColumn("QTY", typeof(System.Decimal)));
            WH _wh = new WH();
            //ʵ������
            bool bFact = true;
            if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
            {
                //��������
                bFact = false;
            }
            foreach (string strWh in wh)
            {
                decimal _qty = Convert.ToDecimal(_wh.GetBoxQty(bFact, prdNo, strWh, content));
                DataRow _dr = _dt.NewRow();
                _dr["WH"] = strWh;
                _dr["QTY"] = _qty;
                _dt.Rows.Add(_dr);
            }
            _dt.AcceptChanges();
            return _dt;
        }
        /// <summary>
        /// ȡ�ÿͻ�����
        /// </summary>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public string GetCustName(string cusNo)
        {
            Cust _cust = new Cust();
            return _cust.GetCusName(cusNo);
        }
        /// <summary>
        /// ����ITM
        /// </summary>
        /// <param name="ds"></param>
        public void SettleItem(DataSet ds)
        {
            DataTable _tfDt = ds.Tables["TF_DYH"];
            DataTable _tfDt1 = ds.Tables["TF_DYH1"];
            int _itm = 1;
            foreach (DataRow dr in _tfDt.Select("", "ITM"))
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    dr["ITM"] = _itm++;
                }
            }
            _itm = 1;
            foreach (DataRow dr in _tfDt1.Select("", "ITM"))
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    dr["ITM"] = _itm++;
                }
            }
        }
        /// <summary>
        /// ȡ�õ���
        /// </summary>
        /// <param name="yhId"></param>
        /// <param name="usr"></param>
        /// <param name="bilDd"></param>
        /// <returns></returns>
        public string SetNo(string yhId, string usr, DateTime bilDd)
        {
            try
            {
                Users _users = new Users();
                string _strDep = _users.GetUserDepNo(usr);
                SQNO _sqNo = new SQNO();
                return _sqNo.Set(yhId, usr, _strDep, bilDd, "FX");
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        /// <summary>
        /// �ı�ͻ��Ժ����д��Ʒ����
        /// </summary>
        /// <param name="ds"></param>
        public void ResetUp(DataSet ds)
        {
            DataTable _tfDt = ds.Tables["TF_DYH"];
            string _cusNo = ds.Tables["MF_DYH"].Rows[0]["CUS_NO"].ToString();
            DateTime _yhDd = Convert.ToDateTime(ds.Tables["MF_DYH"].Rows[0]["YH_DD"]);
            Sunlike.Business.Upr4_def _upr4 = new Sunlike.Business.Upr4_def();
            foreach (DataRow dr in _tfDt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    decimal _up = _upr4.GetCustPrice(dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString(), _cusNo, _yhDd);
                    dr["UP"] = _up;
                    dr["AMTN"] = _up * Convert.ToDecimal(dr["QTY"]);
                }
            }
        }

        /// <summary>
        /// �ж���˵����Ƿ��ܶ�
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public bool isQtySo(string bil_id, string bil_no)
        {
            bool isSo = false;
            SunlikeDataSet _yhDS = dbYh.GetData(bil_id, bil_no);
            foreach (DataRow _dr in _yhDS.Tables["TF_DYH"].Rows)
            {
                if (!String.IsNullOrEmpty(_dr["QTY_SO"].ToString()))
                {
                    isSo = true;
                    break;
                }
            }
            return isSo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="YhId"></param>
        /// <param name="YhNo"></param>
        /// <returns></returns>
        public string getCusNo(string YhId, string YhNo)
        {
            DbDRPYHut _dbYh = new DbDRPYHut(Comp.Conn_DB);
            return _dbYh.getCusNo(YhId, YhNo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="close"></param>
        /// <returns></returns>
        public string CloseBill(string yhId, string yhNo, bool close)
        {
            string _backId = "";
            string _result = "";
            Sunlike.Business.Data.DBDrpYN _dbYn = new DBDrpYN(Comp.Conn_DB);
            SunlikeDataSet _dsYh = _dbYn.GetData(yhId, yhNo);
            DataTable _tbMf = _dsYh.Tables["MF_DYH"];
            if (_tbMf.Rows.Count > 0)
            {
                _backId = _tbMf.Rows[0]["BACK_ID"].ToString();
                if (_backId.Length > 0)
                    return "RCID=COMMON.HINT.CLOSEERROR2,PARAM=" + yhNo;//�õ���[{0}]�Ƿ��ֹ��᰸��
                DbDRPYHut _yh = new DbDRPYHut(Comp.Conn_DB);
                _result = _yh.CloseBill(yhId, yhNo, close);
            }
            return _result;
        }

        /// <summary>
        /// ���PRD_MARK�Ƿ����
        /// </summary>
        /// <param name="prdNo"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool ChkPmarkIsExist(string prdNo, string content)
        {
            bool _isExist = true;
            string _trimChar = "";
            if (!String.IsNullOrEmpty(content.ToString()))
            {
                string[] _pMarkArr = content.Split(';');
                BarBox _barBox = new BarBox();
                PrdMark _pMark = new PrdMark();
                DataTable _dtMark = _pMark.GetSplitData("");
                int _pmarkLen = 0;
                foreach (DataRow _dr in _dtMark.Rows)
                {
                    _pmarkLen += Convert.ToInt32(_dr["SIZE"]);
                }
                DataTable _dtBarRole = _barBox.SelectBar_role();
                if (_dtBarRole.Rows.Count > 0)
                {
                    _trimChar = _dtBarRole.Rows[0]["TRIM_CHAR"].ToString();
                }
                for (int i = 0; i < _pMarkArr.Length - 1; i++)
                {
                    if (_isExist)
                    {
                        string _prdMark = _pMarkArr[i].ToString().Substring(0, _pMarkArr[i].ToString().LastIndexOf("="));
                        _prdMark = _prdMark.Replace(_trimChar, " ");
                        if (_prdMark.Length > _pmarkLen)
                        {
                            _isExist = false;
                            break;
                        }
                        int _pos = 0;
                        for (int j = 0; j < _dtMark.Rows.Count; j++)
                        {
                            string _fldName = _dtMark.Rows[j]["FLDNAME"].ToString();
                            int _size = Convert.ToInt32(_dtMark.Rows[j]["SIZE"]);
                            if (j > 0)
                            {
                                _pos += Convert.ToInt32(_dtMark.Rows[j - 1]["SIZE"]);
                            }
                            string _fldValue = _prdMark.Substring(_pos, _size);
                            if (!_pMark.IsExist(_fldName, prdNo, _fldValue))
                            {
                                _isExist = false;
                                break;
                            }
                        }
                    }
                }
            }
            return _isExist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="itm"></param>
        /// <param name="tableName"></param>
        public void UpdateQtyOld(string yhId, string yhNo, string itm, string tableName)
        {
            dbYh.UpdateQtyOld(yhId, yhNo, itm, tableName);
        }
        /// <summary>
        /// ��д��ɾ��ע��
        /// </summary>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="itm"></param>
        /// <param name="tableName"></param>
        public void UpdateDelId(string yhId, string yhNo, string itm, string tableName)
        {
            dbYh.UpdateDelId(yhId, yhNo, itm, tableName);

        }
        #endregion

        #region Save
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            DataTable _dtMf = ds.Tables["MF_DYH"];
            DataTable _dtTf = ds.Tables["TF_DYH"];
            DataTable _dtTf1 = ds.Tables["TF_DYH1"];
            //#region ����׷��

            //if (ds.Tables.Contains("MF_DYH"))
            //{
            //    if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState !=DataRowState.Added)
            //    {
            //        Sunlike.Business.DataTrace _dataTrace = new DataTrace();
            //        string _bilId = "";
            //        if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //        {
            //            _bilId = _dtMf.Rows[0]["YH_ID"].ToString();
            //        }
            //        else
            //        {
            //            _bilId = _dtMf.Rows[0]["YH_ID", DataRowVersion.Original].ToString();
            //        }
            //        _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //    }
            //}
            //#endregion

            if (_dtMf.Rows.Count > 0
                && _dtMf.Rows[0].RowState != DataRowState.Deleted
                && _dtMf.Rows[0]["YH_ID"].ToString() == "YH")
            {
                #region ���Ԥ������
                if (Convert.ToDateTime(_dtMf.Rows[0]["YH_DD"]) > Convert.ToDateTime(_dtMf.Rows[0]["EST_DD"]))
                {
                    throw new Exception("RCID=INV.HINT.EST_DD_ERROR");//Ԥ�����ڲ��ܴ���Ҫ�����ڣ����飡
                }
                #endregion

                #region ����ǰ����Ʒ���
                if (Comp.DRP_Prop["DRPYH_ORDER_LOWQTY"].ToString() == "F")
                {
                    foreach (DataRow dr in _dtTf.Rows)
                    {
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            DataTable _dtWhInfo = this.GetPrdtQtyInfo(new string[1] { dr["WH"].ToString() }, dr["PRD_NO"].ToString(), dr["PRD_MARK"].ToString());
                            if (_dtWhInfo.Rows.Count > 0)
                            {
                                decimal _chkQty = Convert.ToDecimal(dr["QTY"]);
                                if (!String.IsNullOrEmpty(dr["QTY_SO"].ToString()))
                                {
                                    _chkQty = _chkQty - Convert.ToDecimal(dr["QTY_SO"]);
                                }
                                if (Convert.ToDecimal(_dtWhInfo.Rows[0]["QTY"]) < _chkQty)//�жϼ�����Ƿ��㹻
                                {
                                    if (_dtTf1.Rows.Count > 0)
                                    {
                                        DataRow[] _aryDr = _dtTf1.Select("YH_ID = '" + dr["YH_ID"].ToString()
                                            + "' AND YH_NO = '" + dr["YH_NO"].ToString()
                                            + "' AND KEY_ITM = " + dr["BOX_ITM"].ToString());
                                        if (_aryDr.Length > 0 && _aryDr[0].GetColumnError("QTY") == "")
                                        {
                                            //dr.SetColumnError("QTY","RCID=INV.HINT.QTYNOTIN,PARAM="+dr["PRD_NO"].ToString()+",PARAM="+dr["PRD_MARK"].ToString()+",PARAM="+dr["WH"].ToString());
                                            throw new Exception("RCID=INV.HINT.QTYNOTENOUGH,PARAM=" + dr["ITM"].ToString() + ",PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + dr["PRD_MARK"].ToString() + ",PARAM=" + dr["WH"].ToString());//��治��
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                #endregion


            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            if (_state != StatementType.Delete)
            {
                if (!bMemorySave)
                {
                    if (!this.bRunAuditing)
                    {
                        //��������������
                        DataRow dr = ds.Tables["MF_DYH"].Rows[0];
                        if (dr["YH_ID"].ToString() == "YH")
                        {
                            //����Ƿ���Ҫ��
                            Sunlike.Business.DrpSO _drpso = new DrpSO();
                            try
                            {
                                //�жϸõ��Ƿ��Ѿ��ܶ�,������ھ�ɾ��
                                Sunlike.Business.DrpSO _drpSo = new DrpSO();
                                SunlikeDataSet _dsOs = _drpSo.GetOsNo4YH(ds.Tables["MF_DYH"].Rows[0]["YH_NO"].ToString());
                                if (_dsOs.Tables["MF_POS"].Rows.Count > 0)
                                {
                                    if (_drpSo.DeleteDrpSO(ds.Tables["MF_DYH"].Rows[0]["YH_ID"].ToString(), ds.Tables["MF_DYH"].Rows[0]["YH_NO"].ToString()) != "")
                                    {
                                        throw new Exception("RCID=INV.HINT.DELDRPSO");
                                    }
                                }
                                if (Comp.DRP_Prop["AUTO_INSERTSOYH"].ToString() == "T")
                                {
                                    //�����ܶ�
                                    _drpso.InsertDrpSO(SunlikeDataSet.ConvertTo(ds));
                                }
                            }
                            catch (Exception _ex)
                            {
                                throw new SunlikeException(_ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                if (!this.bRunAuditing)
                {
                    try
                    {
                        //��������������
                        //�жϸõ��Ƿ��Ѿ��ܶ�,������ھ�ɾ��
                        Sunlike.Business.DrpSO _drpSo = new DrpSO();
                        SunlikeDataSet _dsOs = _drpSo.GetOsNo4YH(this.yhNo4Del.ToString());
                        if (_dsOs.Tables["MF_POS"].Rows.Count > 0)
                        {
                            if (_drpSo.DeleteDrpSO(this.yhId4Del.ToString(), this.yhNo4Del.ToString()) != "")
                            {
                                throw new Exception("RCID=INV.HINT.DELDRPSO");
                            }
                        }
                    }
                    catch (Exception _ex)
                    {
                        throw new SunlikeException(_ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Before
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            Auditing _aud = new Auditing();
            if (tableName == "MF_DYH")
            {
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["YH_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.ACCCLOSE");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["YH_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.ACCCLOSE");
                    }
                }
                #region ����
                if (statementType == StatementType.Insert)
                {
                    _state = StatementType.Insert;
                    //���õ���
                    string _yhNo = SetNo(dr["YH_ID"].ToString(), dr["USR"].ToString(), Convert.ToDateTime(dr["YH_DD"]));
                    dr["YH_NO"] = _yhNo;
                    //�ж��Ƿ�Ϊ����Ҫ��
                    if (dr.Table.DataSet.Tables["TF_DYH1"].Rows.Count > 0)
                    {
                        dr["BYBOX"] = "T";
                    }
                }
                #endregion

                #region �޸�
                if (statementType == StatementType.Update)
                {
                    _state = StatementType.Update;
                }
                #endregion

                #region ɾ��
                if (statementType == StatementType.Delete)
                {
                    _state = StatementType.Delete;
                    this.yhId4Del = dr["YH_ID", DataRowVersion.Original].ToString();
                    this.yhNo4Del = dr["YH_NO", DataRowVersion.Original].ToString();
                    //ɾ������
                    SQNO _sqNo = new SQNO();
                    string _sDelNoEor = _sqNo.Delete(dr["YH_NO", DataRowVersion.Original].ToString(),
                        dr["USR", DataRowVersion.Original].ToString());
                    if (!String.IsNullOrEmpty(_sDelNoEor))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.DEL_FAILED");
                    }
                }
                #endregion

                //#region ��˹���
                //if (!bMemorySave)
                //{
                //    AudParamStruct _aps;
                //    if (statementType != StatementType.Delete)
                //    {
                //        Users _users = new Users();
                //        string _strDep = _users.GetUserDepNo(dr["USR"].ToString());                           
                //        _aps.BIL_DD = Convert.ToDateTime(dr["YH_DD"]);
                //        _aps.BIL_ID = dr["YH_ID"].ToString();
                //        _aps.BIL_NO = dr["YH_NO"].ToString();
                //        _aps.BIL_TYPE = "FX";
                //        _aps.CUS_NO = dr["CUS_NO"].ToString();
                //        _aps.DEP = _strDep;
                //        _aps.SAL_NO = "";
                //        _aps.USR = dr["USR"].ToString();
                //        _aps.MOB_ID = "";
                //    }
                //    else
                //        _aps = new AudParamStruct(Convert.ToString(dr["YH_ID", DataRowVersion.Original]), Convert.ToString(dr["YH_NO", DataRowVersion.Original]));
                //    Auditing _auditing = new Auditing();
                //    string _auditErr = _auditing.AuditingBill(bRunAuditing, _aps, statementType, dr);
                //    if (!string.IsNullOrEmpty(_auditErr))
                //    {
                //        throw new SunlikeException(_auditErr);
                //    }
                //}
                //#endregion

            }
            if (tableName == "TF_DYH")
            {
                if (statementType != StatementType.Delete)
                {
                    //ԭҪ����
                    dr["QTY_OLD"] = dr["QTY"];
                }
            }
            if (tableName == "TF_DYH1")
            {
                if (statementType != StatementType.Delete)
                {
                    //ԭҪ����
                    dr["QTY_OLD"] = dr["QTY"];
                }
            }
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataTable Save(DataSet ds, System.Web.UI.Page page)
        {
            DataTable _dtErr = new DataTable();
            bMemorySave = Convert.ToBoolean(ds.ExtendedProperties["MemorySave"]);
            if (bMemorySave)
            {
                if (ds.Tables["MF_DYH"].Rows[0].RowState != DataRowState.Deleted)
                {
                    ds.Tables["MF_DYH"].Rows[0]["SAVE_ID"] = "F";
                }
            }
            else
            {
                if (ds.Tables["MF_DYH"].Rows[0].RowState != DataRowState.Deleted)
                {
                    ds.Tables["MF_DYH"].Rows[0]["SAVE_ID"] = "T";
                }
            }
            //�жϵ����Ƿ���Ҫ��˺��Ѿ������������
            string _yhId, _usr;
            if (ds.Tables["MF_DYH"].Rows[0].RowState != DataRowState.Deleted)
            {
                _yhId = ds.Tables["MF_DYH"].Rows[0]["YH_ID"].ToString();
                _usr = ds.Tables["MF_DYH"].Rows[0]["USR"].ToString();
            }
            else
            {
                _yhId = ds.Tables["MF_DYH"].Rows[0]["YH_ID", DataRowVersion.Original].ToString();
                _usr = ds.Tables["MF_DYH"].Rows[0]["USR", DataRowVersion.Original].ToString();
            }
            Auditing _aud = new Auditing();

            DataRow _dr = ds.Tables["MF_DYH"].Rows[0];
            string _bilType = "";
            string _mobID = "";//֧��ֱ������mobID=@@ �򵥾ݲ����������
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
            //this.bRunAuditing = _aud.IsRunAuditing(_yhId, _usr, _bilType, _mobID);


            //����ITM
            SettleItem(ds);
            Hashtable _ht = new Hashtable();
            _ht["MF_DYH"] = "YH_ID,YH_NO,DEP,YH_DD,CLS_ID,CLS_DATE,CHK_MAN,CUS_NO,REM,USR,FX_WH,WH,BYBOX,EST_DD,FUZZY_ID,SAVE_ID,SYS_DATE";
            _ht["TF_DYH"] = "YH_ID,YH_NO,ITM,PRD_NO,PRD_MARK,WH,EST_DD,QTY,UNIT,AMTN,UP,REM,BOX_ITM,KEY_ITM,QTY_OLD,WH_OLD,EST_OLD,QTY_RTN";
            _ht["TF_DYH1"] = "YH_ID,YH_NO,ITM,PRD_NO,CONTENT,QTY,KEY_ITM,WH,QTY_OLD,WH_OLD,EST_OLD,EST_DD";
            this.UpdateDataSet(ds, _ht);
            _dtErr = GetAllErrors(ds);
            return _dtErr;
        }
        /// <summary>
        /// After
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {

        }


        #endregion

        #region IAuditing
        /// <summary>
        /// ��˲�ͬ��
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
        /// ���ͬ��
        /// </summary>
        /// <param name="bil_id">����ID</param>
        /// <param name="bil_no">���ݱ��</param>
        /// <param name="chk_man">�����</param>
        /// <param name="cls_dd">�������</param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, System.DateTime cls_dd)
        {
            try
            {
                SunlikeDataSet _ds = this.GetData(bil_id, bil_no);
                DataRow _drHead = _ds.Tables["MF_DYH"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_DYH"];
                #region �ͻ����ö�ȼ��
                if (bil_id == "YH" || bil_id == "YC")
                {
                    Cust _cust = new Cust();
                    if (_cust.GetCrdId(_drHead["CUS_NO"].ToString()) == "2")
                    {
                        decimal _totalLimNr = 0;
                        //���ӿͻ����ö��
                        _totalLimNr += _cust.GetLim_NR(_drHead["CUS_NO"].ToString());
                        //�����ܶ������ܽ��
                        for (int i = 0; i < _dtBody.Rows.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["AMTN"].ToString()))
                                _totalLimNr -= Convert.ToDecimal(_dtBody.Rows[i]["AMTN"]);
                        }
                        if (_totalLimNr < 0)
                        {
                            throw new SunlikeException("RCID=SYS.CUST.LIM_NR_ALERT");//���ö�Ȳ��㡣
                        }
                    }
                }
                #endregion

                //�޸��������Ϣ
                dbYh.Approve(bil_id, bil_no, chk_man, cls_dd);
                _drHead["CHK_MAN"] = chk_man;
                _drHead["CLS_DATE"] = cls_dd;
                if (bil_id == "YH")
                {
                    //SunlikeDataSet _ds = dbYh.GetData(bil_id, bil_no);
                    //#region ��дҪ��ԭ������	
                    ////����
                    //foreach (DataRow _dr in _ds.Tables["TF_DYH"].Rows)
                    //{
                    //    if ( _dr["QTY_OLD"] == DBNull.Value)
                    //    {
                    //        this.UpdateQtyOld(_dr["YH_ID"].ToString(),_dr["YH_NO"].ToString(),_dr["ITM"].ToString(),"TF_DYH");
                    //    }
                    //}
                    ////����
                    //foreach (DataRow _dr in _ds.Tables["TF_DYH1"].Rows)
                    //{
                    //    if ( _dr["QTY_OLD"] == DBNull.Value)
                    //    {
                    //        this.UpdateQtyOld(_dr["YH_ID"].ToString(),_dr["YH_NO"].ToString(),_dr["ITM"].ToString(),"TF_DYH1");
                    //    }
                    //}
                    //#endregion
                    #region ���������Ƿ����
                    foreach (DataRow dr in _ds.Tables["TF_DYH1"].Rows)//����Ҫ��
                    {
                        if (!this.ChkPmarkIsExist(dr["PRD_NO"].ToString(), dr["CONTENT"].ToString()))
                        {
                            throw new SunlikeException("RCID=INV.HINT.PMARKNOTEXIST");
                        }
                    }
                    #endregion
                    #region �����ܶ�
                    Prdt _prdt = new Prdt();
                    if (!_ds.Tables["TF_DYH"].Columns.Contains("PRD_NAME"))
                    {
                        _ds.Tables["TF_DYH"].Columns.Add(new DataColumn("PRD_NAME", typeof(System.String)));
                    }
                    if (!_ds.Tables["TF_DYH"].Columns.Contains("WH_NAME"))
                    {
                        _ds.Tables["TF_DYH"].Columns.Add(new DataColumn("WH_NAME", typeof(System.String)));
                    }
                    if (!_ds.Tables["TF_DYH"].Columns.Contains("UT_NAME"))
                    {
                        _ds.Tables["TF_DYH"].Columns.Add(new DataColumn("UT_NAME", typeof(System.String)));
                    }

                    _prdt.AddBilPrdName(_ds.Tables["TF_DYH"], "PRD_NAME", "", "WH", "WH_NAME", false, "", "", "", DateTime.Today, "");
                    if (Comp.DRP_Prop["AUTO_INSERTSOYH"].ToString() == "T")
                    {
                        Sunlike.Business.DrpSO _drpSo = new DrpSO();
                        _drpSo.InsertDrpSO(_ds);
                    }
                    #endregion
                }
                return "";
            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="bil_id">����ID</param>
        /// <param name="bil_no">���ݱ��</param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            try
            {
                SunlikeDataSet _ds = this.GetData(bil_id, bil_no);
                DataRow _drHead = _ds.Tables["MF_DYH"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_DYH"];
                #region �ͻ����ö�ȼ��
                if (bil_id == "YH" || bil_id == "YC")
                {
                    Cust _cust = new Cust();
                    if (_cust.GetCrdId(_drHead["CUS_NO"].ToString()) == "2")
                    {
                        decimal _totalLimNr = 0;
                        //���ӿͻ����ö��
                        _totalLimNr += _cust.GetLim_NR(_drHead["CUS_NO"].ToString());
                        //�����ܶ������ܽ��
                        for (int i = 0; i < _dtBody.Rows.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["AMTN"].ToString()))
                                _totalLimNr -= Convert.ToDecimal(_dtBody.Rows[i]["AMTN"]);
                        }
                        if (_totalLimNr < 0)
                        {
                            throw new SunlikeException("RCID=SYS.CUST.LIM_NR_ALERT");//���ö�Ȳ��㡣
                        }
                    }
                }
                #endregion

                this.SetCanModify(_ds, true, false);
                string _strEor = "";
                if (_ds.ExtendedProperties.Contains("CAN_MODIFY"))
                {
                    if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                    {
                        return "RCID=INV.HINT.CANMODIFY2";
                    }
                }
                if (bil_id == "YH")
                {
                    Sunlike.Business.DrpSO _drpSo = new DrpSO();
                    _strEor = _drpSo.DeleteDrpSO(bil_id, bil_no);
                }
                dbYh.RollBack(bil_id, bil_no);
                return _strEor;
            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }
        #endregion

        #region IAuditingInfo Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bil_Id"></param>
        /// <param name="Bil_No"></param>
        /// <param name="RejectSH"></param>
        /// <returns></returns>
        public string GetBillInfo(string Bil_Id, string Bil_No, ref bool RejectSH)
        {
            // TODO:  Add DRPYHut.GetBillInfo implementation
            DbDRPYH _dbDrpyh = new DbDRPYH(Comp.Conn_DB);
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

                //ǰ��Ӧ�տ�
                DataTable _dtHis = _cust.GetCust_History(_cus_No, System.DateTime.Now, System.DateTime.Now);
                foreach (DataRow _dr in _dtHis.Rows)
                {
                    _arp_AMTN = Convert.ToDecimal(_dr["ARP_AMTN"].ToString());
                }
                _error.Append("CUSTINFO.PREWOM_AMTN;:;" + String.Format("{0:F" + POI_AMT + "}", _arp_AMTN) + ";");
                //���λ��
                DataTable _usrTable = _usr.GetData(_cus_No);
                if (_usrTable.Rows.Count > 0)
                    POI_AMT = Comp.GetCompInfo(_usr.GetUserDepNo(_cus_No)).DecimalDigitsInfo.System.POI_AMT;

                DataTable _dtT = _dbDrpyh.GetTF_DYH(Bil_Id, Bil_No);
                decimal _amtn = 0;
                foreach (DataRow _drT in _dtT.Rows)
                {
                    _amtn += Convert.ToDecimal(_drT["AMTN"].ToString());
                    _est_DD = Convert.ToDateTime(_drT["EST_DD"]);
                }

                #region �ͻ���Ϣ���
                decimal _lim_NR = _cust.GetLim_NR(_cus_No);
                _error.Append("CUST.CAN_USE_LIM_NR;");
                _error.Append(":" + String.Format("{0:F" + POI_AMT + "}", Convert.ToDecimal(_lim_NR - _amtn)) + ";");

                if ((_lim_NR - _amtn) < 0)
                {
                    _error.Append("CUST.LIM_NR_ALERT;");
                }
                #endregion

                #region �ж��Ƿ�ֹͣ��ͻ�����
                bool _stop_Order = _cust.GetStop_Order(_cus_No);
                if (_stop_Order)
                {
                    _error.Append("CUST.STOP_ORDER_ALERT;");
                }
                #endregion
                #region ���ÿ����
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

        #region �޸��˻�����
        /// <summary>
        /// �޸��˻�����
        /// </summary>
        /// <param name="YhId">���ݱ�</param>
        /// <param name="YhNo">Ҫ������</param>
        /// <param name="Itm">Ψһ���</param>
        /// <param name="Qty">��д����</param>
        public void UpdateQtySo(string YhId, string YhNo, int Itm, decimal Qty)
        {
            DbDRPYHut _yh = new DbDRPYHut(Comp.Conn_DB);
            _yh.UpdateQtySo(YhId, YhNo, Itm, Qty);
        }
        /// <summary>
        /// �޸��˻�����
        /// </summary>
        /// <param name="YhId">Ҫ���˻ص���</param>
        /// <param name="YhNo">Ψһ���</param>
        /// <param name="Itm">�˻�����</param>
        /// <param name="Qty">��д����</param>
        /// <param name="QtyHis">ԭ����</param>
        /// <param name="State">״̬ 1��������2���޸ģ�3��ɾ��</param>
        /// <param name="isAuditing">�ܶ��Ƿ������</param>
        public void UpdateQtySo(string YhId, string YhNo, int Itm, decimal Qty, decimal QtyHis, int State, bool isAuditing)
        {
            DbDRPYHut _yh = new DbDRPYHut(Comp.Conn_DB);
            _yh.UpdateQtySo(YhId, YhNo, Itm, Qty, QtyHis, State, isAuditing);
        }



        /// <summary>
        /// ����δ����ܶ��� hy 20090720 modify 
        /// </summary>
        /// <param name="usr">������</param>
        /// <param name="yhId">Ҫ�����ݱ�</param>
        /// <param name="yhNo">Ҫ���˻ص���</param>
        /// <param name="keyItm">Ψһ���</param>
        /// <param name="qty">��д����</param>
        public void UpdateQtySoUnSh(string usr, string yhId, string yhNo, int keyItm, decimal qty)
        {
            DbDRPYHut _yh = new DbDRPYHut(Comp.Conn_DB);
            _yh.UpdateQtySoUnSh(yhId, yhNo, keyItm, qty);

            //#region ����׷��
            //DataTrace objCon = new DataTrace();
            //if (objCon.HasDatatrace(yhId, "TF_DYH"))
            //{
            //    DataRow dr = _yh.GetTFDYHITM(yhId, yhNo, keyItm);
            //    if (dr != null)
            //    {
            //        List<ChangeInfo> lst = new List<ChangeInfo>();
            //        ChangeInfo obj = new ChangeInfo();
            //        obj.FieldName = "QTY_SO_UNSH";
            //        obj.OldValue = dr["QTY_SO_UNSH"].ToString();
            //        obj.NewValue = ((string.IsNullOrEmpty(dr["QTY_SO_UNSH"].ToString()) ? 0 : Convert.ToDecimal(dr["QTY_SO_UNSH"]))
            //            + (qty)).ToString();
            //        obj.ITM = keyItm;
            //        lst.Add(obj);
            //        objCon.SetDataHistory(usr,yhId,yhNo, "TF_DYH", lst);
            //    }
            //}
            //#endregion
        }

        #endregion

        #region ȡ��Ҫ����λ���
        /// <summary>
        /// ȡ�ò�Ʒ���
        /// </summary>
        /// <param name="wh">��λ</param>
        /// <param name="prdNo">Ʒ��</param>
        /// <param name="prdMark">����</param>
        /// <returns></returns>
        public DataTable GetPrdtQtyInfo(string[] wh, string prdNo, string prdMark)
        {
            //��ſ�λ�ź�����
            DataTable _dt = new DataTable("DT_WHQTY");
            _dt.Columns.Add(new DataColumn("WH", typeof(System.String)));
            _dt.Columns.Add(new DataColumn("QTY", typeof(System.Decimal)));
            WH _wh = new WH();

            //ȡ�����			
            bool _isSubStock = true;//�Ƿ�����
            bool _isBatNo = true;

            string[] _whAry = wh;
            StringBuilder _whStr = new StringBuilder(" and wh in ( ");
            if (_whAry.Length > 0)
            {
                for (int i = 0; i < _whAry.Length; i++)
                {
                    if (i == 0)
                    {
                        _whStr.Append("'" + _whAry[i] + "'");
                    }
                    else
                    {
                        _whStr.Append(",'" + _whAry[i] + "'");
                    }
                }
            }
            _whStr.Append(")");
            if (_whAry.Length == 0)
            {
                _whStr = new StringBuilder(" and 1<>1 ");
            }
            if (Comp.DRP_Prop["BAT_USE"].ToString() == "F")
            {
                _isBatNo = false;
            }
            Prdt _prdt = new Prdt();
            _isBatNo = _prdt.CheckBat(prdNo);
            decimal _whQty;
            SunlikeDataSet _dsWh = _wh.GetData(_whStr.ToString());
            DataTable _dtWh = _dsWh.Tables[0];
            if (_dtWh.Rows.Count > 0)
            {
                for (int i = 0; i < _dtWh.Rows.Count; i++)
                {
                    _whQty = 0;
                    if (_dtWh.Rows[i]["CHK_WH"] == DBNull.Value || _dtWh.Rows[i]["CHK_WH"].ToString() == "F")
                    {
                        _isSubStock = false;
                    }
                    else if (Comp.DRP_Prop["DRPYH_WH_VIEWUNDER"].ToString() == "F")
                    {
                        _isSubStock = false;
                    }
                    if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                    {
                        _whQty = _wh.GetSumQty(false, prdNo, prdMark, _dtWh.Rows[i]["WH"].ToString(), _isSubStock, null, _isBatNo);
                    }
                    else
                    {
                        _whQty = _wh.GetSumQty(true, prdNo, prdMark, _dtWh.Rows[i]["WH"].ToString(), _isSubStock, null, _isBatNo);
                    }
                    _isSubStock = true;
                    DataRow _dr = _dt.NewRow();
                    _dr["WH"] = _dtWh.Rows[i]["WH"].ToString();
                    _dr["QTY"] = _whQty;
                    _dt.Rows.Add(_dr);
                }
                _dt.AcceptChanges();
            }
            return _dt;
        }
        /// <summary>
        /// ȡ��Ҫ����λ���
        /// </summary>
        /// <param name="PrdNo"></param>
        /// <param name="PrdMark"></param>
        /// <param name="Wh">��λ</param>
        /// <returns></returns>
        public decimal GetWhQty(string Wh, string PrdNo, string PrdMark)
        {
            bool _isSubStock = true;//�Ƿ�����
            bool _isBatNo = true;//�Ƿ����Ź��ƻ�Ʒ
            decimal _whQty = 0;
            if (Comp.DRP_Prop["BAT_USE"].ToString() == "F")
            {
                _isBatNo = false;
            }
            if (_isBatNo)
            {
                Prdt _prdt = new Prdt();
                _isBatNo = _prdt.CheckBat(PrdNo);
            }
            Sunlike.Business.WH _wh = new Sunlike.Business.WH();
            DataTable _dtWh = _wh.GetData(null, Wh);
            if (_dtWh.Rows.Count > 0)
            {
                if (_dtWh.Rows[0]["CHK_WH"] == DBNull.Value)
                {
                    _isSubStock = false;
                }
                else if (Comp.DRP_Prop["DRPYH_WH_VIEWUNDER"].ToString() == "F")
                {
                    _isSubStock = false;
                }
                if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                {
                    _whQty += _wh.GetSumQty(false, PrdNo, PrdMark, Wh, _isSubStock, null, _isBatNo);
                }
                else
                {
                    _whQty += _wh.GetSumQty(true, PrdNo, PrdMark, Wh, _isSubStock, null, _isBatNo);
                }
            }
            return _whQty;
        }
        #endregion

        #region	ȡ��Ҫ��������
        /// <summary>
        /// ȡ��Ҫ��������
        /// </summary>
        /// <param name="yhId">�������</param>
        /// <param name="yhNo">����</param>
        /// <param name="keyItm">KEY_ITM</param>
        /// <param name="isPrimaryUnit">�Ƿ��������λ</param>
        /// <returns></returns>
        public decimal GetOrgYhQty(string yhId, string yhNo, string keyItm, bool isPrimaryUnit)
        {
            DbDRPYHut _dbYh = new DbDRPYHut(Comp.Conn_DB);
            return _dbYh.GetOrgYhQty(yhId, yhNo, keyItm, isPrimaryUnit);
        }
        #endregion

        /// <summary>
        /// ���½᰸ע��
        /// </summary>
        /// <param name="yhId">YH_ID</param>
        /// <param name="yhNo">YH_NO</param>
        public void UpdateClsId(string yhId, string yhNo)
        {
            DbDRPYHut _dbDrpYh = new DbDRPYHut(Comp.Conn_DB);
            _dbDrpYh.UpdateClsId(yhId, yhNo);
        }

        #region ICloseBill Members
        /// <summary>
        /// ���᰸
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
                _error = this.CloseBill(bil_id, bil_no, false);
            }
            return _error;
        }
        /// <summary>
        /// �᰸
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "CLS_ID")
            {
                _error = this.CloseBill(bil_id, bil_no, true);
            }
            return _error;
        }

        #endregion


    }
}
