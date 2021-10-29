using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections.Generic;
namespace Sunlike.Business
{
	/// <summary>
	/// Pact ��ժҪ˵����
	/// </summary>
	public class Pact : BizObject
	{
        private string _loginUsr = "";        
		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public Pact()
		{            
		}
		#endregion

		#region GetData
		/// <summary>
		/// ȡ��Լ��Ϣ
		/// </summary>
		/// <param name="usr">�����û�����</param>
		/// <param name="pactID">��Լ����</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string usr, string pactID)
		{
			DbPact _pact = new DbPact(Comp.Conn_DB);
            SunlikeDataSet _ds = new SunlikeDataSet();
            _ds = _pact.GetData(usr, pactID);
            if (!string.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                string _billDep = "";
                string _billUsr = "";
                if (_ds.Tables.Count > 0 && _ds.Tables.Contains("CUS_PACT") && _ds.Tables["CUS_PACT"].Rows.Count > 0)
                {
                    _billDep = _ds.Tables["CUS_PACT"].Rows[0]["DEP"].ToString();
                    _billUsr = _ds.Tables["CUS_PACT"].Rows[0]["USR"].ToString();
                }
                Hashtable _right = Users.GetBillRight("PACT_INDX", usr, _billDep, _billUsr);
                _ds.ExtendedProperties["UPD"] = _right["UPD"];
                _ds.ExtendedProperties["DEL"] = _right["DEL"];
                _ds.ExtendedProperties["PRN"] = _right["PRN"];
                _ds.ExtendedProperties["LCK"] = _right["LCK"];
            }
            return _ds;
		}

        /// <summary>
        /// ȡ��Լ����
        /// </summary>
        /// <param name="KndNo">�������</param>
        /// <param name="isTree">��������</param>
        /// <returns></returns>
        public SunlikeDataSet GetPACTKnd(string KndNo, bool isTree)
        {
            DbPact _pact = new DbPact(Comp.Conn_DB);
            return _pact.GetPACTKnd(KndNo, isTree);
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dyCondtion"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(Dictionary<string,object> dyCondtion)
        {
            DbPact _pact = new DbPact(Comp.Conn_DB);
            return _pact.GetData(dyCondtion);
        }
		#endregion

		#region GetFACT
		/// <summary>
		/// ȡ����һ�����ϵ�ʵǩԼ���
		/// </summary>
		/// <param name="CusNo">�ͻ�����</param>
		/// <returns></returns>
		public SunlikeDataSet GetFACT(string CusNo)
		{
			DbPact _pact = new DbPact(Comp.Conn_DB);
			return _pact.GetFACT(CusNo);
		}
		#endregion

        #region GetPrdt
        public SunlikeDataSet GetPrdt(string WC_NO)
        {
            DbPact _pact = new DbPact(Comp.Conn_DB);
            return _pact.GetPrdt(WC_NO);
            
        }
        #endregion

        #region  Update Ma
        /// <summary>
        /// ����ά�����뵥
        /// </summary>
        /// <param name="dsPact">��Լ��Ϣ</param>
        /// <param name="maDd">ά�����뵥����</param>
        /// <param name="errorMsg">������Ϣ</param>
        /// <param name="maNos">���������뵥��</param>
        /// <returns></returns>
        public bool UpdateMa(SunlikeDataSet dsPact, DateTime maDd, out string errorMsg,out ArrayList maNos)
        {
            bool _result = false;
            errorMsg = "";
            maNos = new ArrayList();
            if (dsPact == null)
                return false;
            if (dsPact.Tables["CUS_PACT"].Rows.Count == 0)
                return false;
            DataRow _drHead = dsPact.Tables["CUS_PACT"].Rows[0];
            DataRow[] _drBodys = dsPact.Tables["TMP_CUSPACT1"].Select();
            Dictionary<string, string> _dtyCust = new Dictionary<string, string>();
            //���ܿͻ�
            if (!string.IsNullOrEmpty(_drHead["CUS_NO"].ToString()))
                _dtyCust.Add(_drHead["CUS_NO"].ToString(), "HEAD");
            foreach (DataRow drBody in _drBodys)
            {
                if (!string.IsNullOrEmpty(drBody["CUS_NO"].ToString()))
                {
                    if (!_dtyCust.ContainsKey(drBody["CUS_NO"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(drBody["CUS_NO"].ToString()))
                            _dtyCust.Add(drBody["CUS_NO"].ToString(), "");
                    }
                }
            }
            try
            {
                foreach (KeyValuePair<string, string> _kvp in _dtyCust)
                {
                    string _cusNo = "";
                    _cusNo = _kvp.Key;
                    #region ����ά�����뵥
                    SunlikeDataSet _dsMa = null;
                    MTNMa _ma = new MTNMa();
                    _dsMa = _ma.GetUpdateData("", "", "MA", "", true);

                    DataRow _drNew = _dsMa.Tables["MF_MA"].NewRow();
                    //��ӱ�ͷ
                    _drNew["MA_ID"] = "MA";
                    _drNew["MA_NO"] = "";
                    _drNew["MA_DD"] = maDd.ToString(Comp.SQLDateFormat);
                    _drNew["CUS_NO"] = _cusNo;
                    _drNew["SAL_NO"] = _drHead["SAL_NO"];
                    _drNew["DEP"] = _drHead["DEP"];
                    //_drNew["CNT_NO"] = "";
                    //_drNew["CNT_REM"] = "";
                    _drNew["BIL_TYPE"] = "";
                    _drNew["REM"] = "";
                    _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    _drNew["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    _drNew["USR"] = _drHead["USR"];
                    _drNew["CHK_MAN"] = _drHead["USR"];
                    _drNew["MTN_FLOW"] = "2";
                    _drNew["EST_DD"] = maDd.ToString(Comp.SQLDateFormat);
                    _drNew["REM"] = "CUS_PACT";
                    _drNew["BIL_ID"] = "KH";
                    _drNew["BIL_NO"] = _drHead["PAC_NO"];
                    _dsMa.Tables["MF_MA"].Rows.Add(_drNew);                    
                    //��ӱ���
                    DataRow[] _drBodySel = null;
                    if (string.Compare("HEAD", _kvp.Value) == 0)
                    {
                        _drBodySel = dsPact.Tables["TMP_CUSPACT1"].Select("ISNULL(CUS_NO,'')='' OR CUS_NO='" + _cusNo + "'");
                    }
                    else
                    {
                        _drBodySel = dsPact.Tables["TMP_CUSPACT1"].Select("CUS_NO='" + _cusNo + "'");
                    }
                    foreach (DataRow drBody in _drBodySel)
                    {
                        _drNew = _dsMa.Tables["TF_MA"].NewRow();
                        _drNew["MA_ID"] = "MA";
                        _drNew["MA_NO"] = "";
                        _drNew["ITM"] = _dsMa.Tables["TF_MA"].Rows.Count + 1;
                        _drNew["PRD_NO"] = drBody["PRD_NO"];
                        _drNew["PRD_MARK"] = drBody["PRD_MARK"];
                        _drNew["WC_NO"] = drBody["WC_NO"];
                        if (!string.IsNullOrEmpty(drBody["WC_NO"].ToString()))
                        {
                            WC _wc = new WC();
                            SunlikeDataSet _dsWc = _wc.GetData(drBody["WC_NO"].ToString());
                            if (_dsWc.Tables["MF_WC"].Rows.Count > 0)
                            {
                                //д�����ñ��̣�������,
                                _drNew["RTN_DD"] = _dsWc.Tables["MF_WC"].Rows[0]["RETURN_DD"];//������
                                _drNew["MTN_ALL_ID"] = _dsWc.Tables["MF_WC"].Rows[0]["MTN_ALL_ID"];//���ñ���
                            }
                            
                            
                        }
                        _drNew["UNIT"] = "1";
                        //_drNew["SA_NO"] = "";
                        //_drNew["SA_ITM"] = "";
                        _drNew["QTY"] = "1";
                        //_drNew["MTN_DD"] = "";
                        _drNew["EST_DD"] = maDd.ToString(Comp.SQLDateFormat);
                        //_drNew["RTN_DD"] = "";
                        //_drNew["REM"] = "";
                        _drNew["MTN_TYPE"] = "";
                        //_drNew["MTN_ALL_ID"] = "";                
                        _dsMa.Tables["TF_MA"].Rows.Add(_drNew);
                    }

                    _ma.UpdateData("",_dsMa, true);
                    if (_dsMa.Tables["MF_MA"].Rows.Count > 0)
                    {
                        maNos.Add(_dsMa.Tables["MF_MA"].Rows[0]["MA_NO"].ToString());
                    }
                    #endregion
                }
                _result = true;
            }
            catch (Exception _ex)
            {
                errorMsg = _ex.Message.ToString();
                _result = false;
            }
            return _result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pacNo"></param>
        /// <param name="strEndDdCurrent"></param>
        /// <param name="onlyOne"></param>
        /// <param name="hasSatday"></param>
        /// <param name="hasSunday"></param>
        /// <param name="maNos"></param>
        public string UpdateMa(string pacNo, string strEndDdCurrent, bool onlyOne, bool hasSatday, bool hasSunday,out ArrayList maNos)
        {
            string _errorMsg = "";
            maNos = new ArrayList();
            SunlikeDataSet _dsPact = this.GetData("", pacNo);
            DateTime _dateEndDdCurrent = System.DateTime.Now;
            DateTime _startDd = System.DateTime.Now;
            DateTime _endDd = System.DateTime.Now;
            DateTime _maDd = System.DateTime.Now;
            bool _hasMaDd = false;
            bool _hasMakeMa = false;
            DateTime _lastUpdatedMa = System.DateTime.Now;
            int _srvMth = 1;
            DataTable _dtHead = _dsPact.Tables["CUS_PACT"];
            if (_dtHead.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["START_DD"].ToString()))
                {
                    _startDd = Convert.ToDateTime(_dtHead.Rows[0]["START_DD"]);
                }
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["END_DD"].ToString()))
                {
                    _endDd = Convert.ToDateTime(_dtHead.Rows[0]["END_DD"]);
                }
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["MA_DD"].ToString()))
                {
                    _maDd = Convert.ToDateTime(_dtHead.Rows[0]["MA_DD"]);
                    _hasMaDd = true;
                }
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["SRV_MTH"].ToString()))
                {
                    _srvMth = Convert.ToInt32(_dtHead.Rows[0]["SRV_MTH"]);
                }
            }
            //�жϵ�ǰ��ֹ����
            if (!string.IsNullOrEmpty(strEndDdCurrent))
            {
                _dateEndDdCurrent = Convert.ToDateTime(strEndDdCurrent);
                //�жϱ��ν�ֹ�����Ƿ���ڽ�ֹ����
                if (_dateEndDdCurrent > _endDd)
                {
                    _dateEndDdCurrent = _endDd;
                }
            }
            else
            {
                _dateEndDdCurrent = _endDd;
            }
            //�жϱ��ο�ʼ����
            if (_hasMaDd)
            {
                if (_maDd > _startDd)
                {
                    DateTime _dateEndDdCurrentTmp = _maDd;
                    _startDd = _maDd.AddMonths(_srvMth);
                }
            }            
            DateTime _tmpDateTime = _startDd;
            if (_srvMth > 0)
            {
                while (_tmpDateTime <= _dateEndDdCurrent)
                {
                    //�ж��Ƿ�����Ϊ��ά����
                    if (_tmpDateTime.DayOfWeek == DayOfWeek.Saturday)
                    {
                        if (!hasSatday)
                        {
                            _tmpDateTime = _tmpDateTime.AddDays(1);
                        }

                    }
                    //�ж��Ƿ�����Ϊ��ά����
                    if (_tmpDateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (!hasSunday)
                        {

                            _tmpDateTime = _tmpDateTime.AddDays(1);
                        }
                    }
                    string _tmpErrorMsg = "";
                    ArrayList _tmpMaNos = new ArrayList();
                    bool _updated = UpdateMa(_dsPact, _tmpDateTime,  out _tmpErrorMsg,out _tmpMaNos);
                    if (!string.IsNullOrEmpty(_errorMsg))
                        _errorMsg += ";";
                    _errorMsg += _tmpErrorMsg;
                    if (!string.IsNullOrEmpty(_errorMsg))
                    {
                        break;
                    }
                    if (_updated)
                    {
                        _lastUpdatedMa = _tmpDateTime;
                        if (!_hasMakeMa)
                        {
                            _hasMakeMa = true;
                        }
                        //������ɵ����뵥��
                        if (_tmpMaNos.Count > 0)
                        {
                            maNos.AddRange(_tmpMaNos);
                        }
                        if (onlyOne)
                        {
                            break;
                        }
                    }
                    _tmpDateTime = _tmpDateTime.AddMonths(_srvMth);
                    
                }
            }
            //���¿ͻ���Լ��������
            if (_hasMakeMa)
            {
                if (_dtHead.Rows.Count > 0)
                {
                    _dtHead.Rows[0]["MA_DD"] = _lastUpdatedMa;
                    this.UpdateData(_dsPact);
                }
            }
            return _errorMsg;
        }

        /// <summary>
        /// ���������ⵥ����ά�����뵥
        /// </summary>
        /// <param name="dsQus">�������ⵥ��DS</param>
        /// <param name="errorMsg">����ѶϢ</param>
        /// <param name="MA_NO">���뵥��</param>
        /// <returns></returns>
        public bool UpdateMa(SunlikeDataSet dsQus, out string errorMsg, out string MA_NO)
        {
            errorMsg = "";
            MA_NO = "";
            bool _result = false;
            errorMsg = "";
            if (dsQus == null)
                return false;
            if (dsQus.Tables["MF_QA"].Rows.Count == 0)
                return false;
            DataRow _drHead = dsQus.Tables["MF_QA"].Rows[0];

            SunlikeDataSet _dsMa = null;
            MTNMa _ma = new MTNMa();
            _dsMa = _ma.GetUpdateData("", "", "MA", "", true);
            try
            {
                DataRow _drNew = _dsMa.Tables["MF_MA"].NewRow();
                //��ӱ�ͷ
                _drNew["MA_ID"] = "MA";
                _drNew["MA_NO"] = "";
                _drNew["MA_DD"] = _drHead["QA_DD"];
                _drNew["CUS_NO"] = _drHead["CUS_NO"];
                _drNew["SAL_NO"] = _drHead["SAL_NO"];
                _drNew["DEP"] = _drHead["DEP"];
                //_drNew["CNT_NO"] = "";
                //_drNew["CNT_REM"] = "";
                _drNew["BIL_TYPE"] = "";
                _drNew["REM"] = "";
                _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                _drNew["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                _drNew["USR"] = _drHead["USR"];
                _drNew["CHK_MAN"] = _drHead["USR"];
                _drNew["MTN_FLOW"] = "2";
                _drNew["EST_DD"] = Convert.ToDateTime(_drHead["QA_DD"]).ToString(Comp.SQLDateFormat);                
                _drNew["REM"] = _drHead["QA_NO"];
                _dsMa.Tables["MF_MA"].Rows.Add(_drNew);
                //��ӱ���
                DataRow[] _drBodys = dsQus.Tables["MF_QA"].Select();
                foreach (DataRow drBody in _drBodys)
                {
                    if (!string.IsNullOrEmpty(drBody["WC_NO"].ToString()))
                    {
                        _drNew = _dsMa.Tables["TF_MA"].NewRow();
                        _drNew["MA_ID"] = "MA";
                        _drNew["MA_NO"] = "";
                        _drNew["ITM"] = _dsMa.Tables["TF_MA"].Rows.Count + 1;
                        _drNew["PRD_NO"] = drBody["PRD_NO"];
                        _drNew["PRD_MARK"] = drBody["PRD_MARK"];
                        _drNew["WC_NO"] = drBody["WC_NO"];
                        _drNew["UNIT"] = "1";
                        //_drNew["SA_NO"] = "";
                        //_drNew["SA_ITM"] = "";
                        _drNew["QTY"] = "1";
                        //_drNew["MTN_DD"] = "";
                        _drNew["EST_DD"] = Convert.ToDateTime(_drHead["QA_DD"]).ToString(Comp.SQLDateFormat);
                        //_drNew["RTN_DD"] = "";
                        _drNew["REM"] = drBody["QA_REM"];
                        if (Convert.ToDateTime(_drHead["QA_DD"]).CompareTo(Convert.ToDateTime(drBody["MTN_DD"])) < 0)  //���ڱ�������-->�����շ�
                        {
                            _drNew["MTN_TYPE"] = "1";  //�����շ�
                        }
                        else if ((Convert.ToDateTime(_drHead["QA_DD"]).CompareTo(Convert.ToDateTime(drBody["MTN_DD"])) <= 0) &&
                            (Convert.ToDateTime(_drHead["QA_DD"]).CompareTo(Convert.ToDateTime(drBody["RETURN_DD"])) >= 0))  //С�ڱ�������,���ڰ�������-->���ڲ��շ�
                        {
                            _drNew["MTN_TYPE"] = "4";  //���ڲ��շ�
                        }
                        else if (Convert.ToDateTime(_drHead["QA_DD"]).CompareTo(Convert.ToDateTime(drBody["RETURN_DD"])) < 0)  //С�ڰ�������-->����
                        {
                            _drNew["MTN_TYPE"] = "5";  //����
                        }
                        //_drNew["MTN_TYPE"] = "";
                        //_drNew["MTN_ALL_ID"] = "";                
                        _dsMa.Tables["TF_MA"].Rows.Add(_drNew);
                    }
                }
                _ma.UpdateData("",_dsMa, true);
                _result = true;
            }
            catch (Exception _ex)
            {
                errorMsg = _ex.Message.ToString();
                _result = false;
            }
            if (_result == true)
                MA_NO = _dsMa.Tables[0].Rows[0]["MA_NO"].ToString();
            return _result;

        }

       
        #endregion

        #region Update Pact
        /// <summary>
        /// �����µĺ�Լ
        /// </summary>
        /// <param name="pacNo"></param>
        /// <param name="endYears"></param>
        /// <param name="endMonths"></param>
        public string UpdatePactNew(string pacNo, string _pacDD, int endYears, int endMonths)
        {
            string _result = "";
            SunlikeDataSet _dsPact = this.GetData("", pacNo);
            SunlikeDataSet _dsPactNew = this.GetData("", "");
            if (_dsPact == null)
                return "";
            if (_dsPact.Tables["CUS_PACT"].Rows.Count == 0)
                return "";
            DateTime _startDd = System.DateTime.Now;
            DateTime _tmpDd = System.DateTime.Now;
            DateTime _endDd = System.DateTime.Now;
            DataRow _drHead = _dsPact.Tables["CUS_PACT"].Rows[0];
            DataRow[] _drBodys = _dsPact.Tables["CUS_PACT1"].Select();            
            if (!string.IsNullOrEmpty(_drHead["END_DD"].ToString()))
            {
                _startDd = Convert.ToDateTime(_drHead["END_DD"]);
            }
            DataRow _drNew = null;
            //�����Լ��ͷ         
            _drNew = _dsPactNew.Tables["CUS_PACT"].NewRow();
            foreach (DataColumn dc in _drHead.Table.Columns)
            {
                if (_drNew.Table.Columns.Contains(dc.ColumnName))
                {
                    _drNew[dc.ColumnName] = _drHead[dc.ColumnName];
                }
            }            
            _drNew["REM"] = _drNew["PAC_NO"];
            _drNew["PAC_NO"] = "";
            _drNew["PAC_DD"] = Convert.ToDateTime(_pacDD);

            //��ֹ��һ��
            _tmpDd = _startDd;
            _startDd = _tmpDd.AddDays(1);
            //��ֹ����
            _tmpDd = _startDd;
            _endDd = _tmpDd.AddYears(endYears).AddMonths(endMonths).AddDays(-1);
            _drNew["START_DD"] = _startDd;
            _drNew["END_DD"] = _endDd;
            _dsPactNew.Tables["CUS_PACT"].Rows.Add(_drNew);
            //�����Լ����            
            foreach (DataRow dr in _drBodys)
            {
                _drNew = _dsPactNew.Tables["CUS_PACT1"].NewRow();
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    if (string.Compare("PAC_NO", dc.ColumnName) == 0)
                    {
                        _drNew["PAC_NO"] = "";
                    }
                    else
                    {
                        if (_drNew.Table.Columns.Contains(dc.ColumnName))
                        {
                            _drNew[dc.ColumnName] = dr[dc.ColumnName];
                        }
                    }
                }
                _dsPactNew.Tables["CUS_PACT1"].Rows.Add(_drNew);
                //���ı��޿��Ľ�ֹ����
                if (!string.IsNullOrEmpty(dr["WC_NO"].ToString()))
                {
                    WC _wc = new WC();
                    _wc.UpdateStopDd(dr["WC_NO"].ToString(), _endDd);
                }
            }
            //�����Լ
            UpdateData(_dsPactNew);
            if (_dsPactNew.Tables.Count > 0 && _dsPactNew.Tables.Contains("CUS_PACT") && _dsPactNew.Tables["CUS_PACT"].Rows.Count > 0)
            {
                _result = _dsPactNew.Tables["CUS_PACT"].Rows[0]["PAC_NO"].ToString();
            }
            return _result;
        }
        /// <summary>
        /// ���º�Լ��MA_DD��λ
        /// </summary>
        /// <param name="pacNo"></param>
        /// <returns></returns>
        public bool UpdatePactOld(string pacNo)
        {
            bool _Result = false;
            SunlikeDataSet _dsPact = this.GetData("", pacNo);
            DataRow _drHead = _dsPact.Tables["CUS_PACT"].Rows[0];
            _drHead["MA_DD"] = DBNull.Value;
            //�����Լ
            UpdateData(_dsPact);
            _Result = true;
            return _Result;

        }
        #endregion

        #region UpdateData
        /// <summary>
		/// ���º�Լ
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public DataTable UpdateData(SunlikeDataSet ds)
		{
            //ȡ¼������Ϣ
            if (ds != null && ds.Tables.Contains("CUS_PACT") && ds.Tables["CUS_PACT"].Rows.Count > 0)
            {
                if (ds.Tables["CUS_PACT"].Rows[0].RowState == DataRowState.Deleted)
                {
                    this._loginUsr = ds.Tables["CUS_PACT"].Rows[0]["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    this._loginUsr = ds.Tables["CUS_PACT"].Rows[0]["USR"].ToString();
                }
            }

			Hashtable _ht = new Hashtable();
            _ht["CUS_PACT"] = "PAC_NO,PAC_DD,TITLE,MASTER_ID,CUS_NO,STATUS,START_DD,END_DD,AMTN_BG,AMTN_FACT,SAL_NO,SRV_NO,FILE1,FILE_ID1,FILE_NAME1,FILE2,FILE_ID2,FILE_NAME2,REM,USR,DEP,SRV_MTH,MA_DD,IDX_NO,PAC_NOSELF";
            _ht["CUS_PACT1"] = "PAC_NO,ITM,WC_NO,REM,PRD_NO,PRD_MARK,QTY";
            _ht["CUS_PACT2"] = "PAC_NO,NET_USER,SYS_DATE,SAL_NO,REM_MOD,REM," +
                "CHK1,CHK2,CHK3,CHK4,CHK5,CHK6,CHK7,CHK8,CHK9,CHK10," +
                "CHK11,CHK12,CHK13,CHK14,CHK15,CHK16,CHK17,CHK18,CHK19,CHK20," +
                "CHK21,CHK22,CHK23,CHK24,CHK25,CHK26,CHK27,CHK28,CHK29,CHK30," +
                "CHK31,CHK32,CHK33,CHK34,CHK35,CHK36,CHK37,CHK38,CHK39,CHK40," +
                "CHK41,CHK42,CHK43,CHK44,CHK45,CHK46,CHK47,CHK48,CHK49,CHK50," +
                "CHK51,CHK52,CHK53,CHK54,CHK55,CHK56,CHK57,CHK58,CHK59,CHK60," +
                "CHK61,CHK62,CHK63,CHK64";
            //_ht["TMP_CUSPACT1"] = "PAC_NO,ITM,WC_NO,REM,PRD_NO,PRD_NAME,PRD_MARK";
			this.UpdateDataSet(ds,_ht);
            ds.AcceptChanges();
			return Sunlike.Business.BizObject.GetAllErrors(ds);
		}

        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables[0];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();

            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "KH");
            //}
            //#endregion

            base.BeforeDsSave(ds);
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
            if (string.Equals(tableName, "CUS_PACT"))
            {
                #region ȡ����
                if (statementType == StatementType.Insert)
                {
                    #region ȡ����
                    SQNO _sqNo = new SQNO();
                    string _depNo = dr["DEP"].ToString();//����
                    DateTime _pacDd = System.DateTime.Now;
                    if (dr["PAC_DD"] is System.DBNull)
                    {
                        _pacDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _pacDd = Convert.ToDateTime(dr["PAC_DD"]);
                    }
                    string _bilType = "";
                    string _bilNo = _sqNo.Set("KH", _loginUsr, _depNo, _pacDd, _bilType);
                    dr["PAC_NO"] = _bilNo;
                    dr["PAC_DD"] = _pacDd.ToString(Comp.SQLDateFormat);
                    #endregion
                }
                #endregion

                Query _query = new Query();
                if (statementType == StatementType.Delete)
                {
                    if (dr["MASTER_ID", DataRowVersion.Original].ToString() == "T")
                    {
                        SunlikeDataSet _ds = _query.DoSQLString("select 1 from CUS_PACT where CUS_NO='"
                            + dr["CUS_NO", DataRowVersion.Original].ToString() + "' and PAC_NO<>'"
                            + dr["PAC_NO", DataRowVersion.Original].ToString() + "'");
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            throw new Exception("RCID=CWK.PACT.DEL_MASTER");//ɾ����Լǰ����ָ��������ԼΪ��Լ��
                        }
                    }
                }
                else if (dr["MASTER_ID"].ToString() == "T")
                {
                    _query.RunSql("update CUS_PACT set MASTER_ID='F' where CUS_NO='" + dr["CUS_NO"].ToString()
                        + "' and PAC_NO<>'" + dr["PAC_NO"].ToString() + "'");
                }

            }
            else if (string.Equals(tableName, "CUS_PACT_I") )
            {
                if (statementType == StatementType.Delete)
                {
                    string _kndNo = dr["KND_NO", DataRowVersion.Original].ToString();
                    Query _query = new Query();
                    SunlikeDataSet _ds = _query.DoSQLString("select top 1 1 from CUS_PACT where IDX_NO='" + _kndNo + "'");
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        dr.SetColumnError("KND_NO", "RCID=SYS.QA_KND.HINT_DEL1");//�÷����ѱ�ʹ��,����ɾ��
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else
                    {
                        _ds = GetPACTKnd(_kndNo, true);
                        if (_ds.Tables["CUS_PACT_I"].Rows.Count > 1)
                        {
                            dr.SetColumnError("KND_NO", "RCID=SYS.QA_KND.HINT_DEL2");//�÷����»����ӷ��࣬�������ɾ��!
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
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
            #region CUS_PACT
            if (tableName == "CUS_PACT")
            {
                if (statementType == StatementType.Delete)
                {
                    #region ɾ������
                    SQNO _sqNo = new SQNO();
                    _sqNo.Delete(dr["PAC_NO", DataRowVersion.Original].ToString(), this._loginUsr);//ɾ��ʱ��BILD�в���һ������

                    #endregion
                }
            }
            #endregion
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="ds">��������</param>
        /// <returns></returns>
        public DataTable UpdatePactKnd(SunlikeDataSet ds)
        {
            Hashtable _ht = new Hashtable();            
            _ht["CUS_PACT_I"] = "KND_NO,NAME,UP,USR";
            UpdateDataSet(ds, _ht);
            return Sunlike.Business.BizObject.GetAllErrors(ds);
        }        
		#endregion
		
        #region ȡ�ú�Լ����        
        /// <summary>
        /// ȡ�ú�Լ����
        /// </summary>
        /// <returns></returns>
        public string GetPactNo(string pacId, string userId, DateTime dateTime)
        {
            string _pacNo = "KH00000000";
            try
            {
                SQNO _sqlno = new SQNO();
                Users _users = new Users();
                _pacNo = _sqlno.Get(pacId, userId, _users.GetUserDepNo(userId), dateTime, "KH");
            }
            catch { }
            return _pacNo;
        }
        #endregion	
	
        #region �Ƿ����
        /// <summary>
        /// �Ƿ����
        /// </summary>
        /// <param name="wcNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByWcNo(string wcNo)
        {
            DbPact _pact = new DbPact(Comp.Conn_DB);
            return _pact.GetDataByWcNo(wcNo);
        }
        #endregion

        #region ȡ�������]��ӍϢ
        public SunlikeDataSet GetAttnReg(object cus_no)
        {
            string _ConfigFile1 = System.Web.HttpContext.Current.Server.MapPath("~/CASE40/app.xml");
            Sunlike.Common.CommonVar.XmlConfig.LoadXML(_ConfigFile1);
            string _RegisterConnString = Sunlike.Common.CommonVar.XmlConfig.AppSettings["RegisterConnectionString"];

            DbPact _dbpact = new DbPact(_RegisterConnString);
            return _dbpact.GetAttnReg(cus_no);
        }
        #endregion               
    
        public string UpdateMa(string pacNo, string strEndDdCurrent, string BIL_TYPE, bool onlyOne, bool hasSatday, bool hasSunday, out ArrayList maNos)
        {
            string _errorMsg = "";
            maNos = new ArrayList();
            SunlikeDataSet _dsPact = this.GetData("", pacNo);
            DateTime _dateEndDdCurrent = System.DateTime.Now;
            DateTime _dateRtnDdCurrent = System.DateTime.Now;  //��������
            DateTime _startDd = System.DateTime.Now;
            DateTime _endDd = System.DateTime.Now;
            DateTime _maDd = System.DateTime.Now;
            bool _hasMaDd = false;
            bool _hasMakeMa = false;
            DateTime _lastUpdatedMa = System.DateTime.Now;
            int _srvMth = 1;
            DataTable _dtHead = _dsPact.Tables["CUS_PACT"];
            if (_dtHead.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["START_DD"].ToString()))
                {
                    _startDd = Convert.ToDateTime(_dtHead.Rows[0]["START_DD"]);
                }
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["END_DD"].ToString()))
                {
                    _endDd = Convert.ToDateTime(_dtHead.Rows[0]["END_DD"]);
                }
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["MA_DD"].ToString()))
                {
                    _maDd = Convert.ToDateTime(_dtHead.Rows[0]["MA_DD"]);
                    _hasMaDd = true;
                }
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["SRV_MTH"].ToString()))
                {
                    _srvMth = Convert.ToInt32(_dtHead.Rows[0]["SRV_MTH"]);
                }
            }
            //�жϵ�ǰ��ֹ����
            if (!string.IsNullOrEmpty(strEndDdCurrent))
            {
                _dateEndDdCurrent = Convert.ToDateTime(strEndDdCurrent);
                //�жϱ��ν�ֹ�����Ƿ���ڽ�ֹ����
                if (_dateEndDdCurrent > _endDd)
                {
                    _dateEndDdCurrent = _endDd;
                }
            }
            else
            {
                _dateEndDdCurrent = _endDd;
            }

            //�Д൱ǰ��������
            //if (!string.IsNullOrEmpty(strRtnDdCurrent))
            //{
            //    _dateRtnDdCurrent = Convert.ToDateTime(strRtnDdCurrent);
            //}
            //�жϱ��ο�ʼ����
            if (_hasMaDd)
            {
                if (_maDd > _startDd)
                {
                    DateTime _dateEndDdCurrentTmp = _maDd;
                    _startDd = _maDd.AddMonths(_srvMth);
                }
            }
            DateTime _tmpDateTime = _startDd;
            if (_srvMth > 0)
            {
                while (_tmpDateTime <= _dateEndDdCurrent)
                {
                    //�ж��Ƿ�����Ϊ��ά����
                    if (_tmpDateTime.DayOfWeek == DayOfWeek.Saturday)
                    {
                        if (!hasSatday)
                        {
                            _tmpDateTime = _tmpDateTime.AddDays(1);
                        }

                    }
                    //�ж��Ƿ�����Ϊ��ά����
                    if (_tmpDateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (!hasSunday)
                        {

                            _tmpDateTime = _tmpDateTime.AddDays(1);
                        }
                    }
                    string _tmpErrorMsg = "";
                    ArrayList _tmpMaNos = new ArrayList();
                    bool _updated = UpdateMa(_dsPact, BIL_TYPE, _tmpDateTime, out _tmpErrorMsg, out _tmpMaNos);
                    if (!string.IsNullOrEmpty(_errorMsg))
                        _errorMsg += ";";
                    _errorMsg += _tmpErrorMsg;
                    if (!string.IsNullOrEmpty(_errorMsg))
                    {
                        break;
                    }
                    if (_updated)
                    {
                        _lastUpdatedMa = _tmpDateTime;
                        if (!_hasMakeMa)
                        {
                            _hasMakeMa = true;
                        }
                        //������ɵ����뵥��
                        if (_tmpMaNos.Count > 0)
                        {
                            maNos.AddRange(_tmpMaNos);
                        }
                        if (onlyOne)
                        {
                            break;
                        }
                    }
                    _tmpDateTime = _tmpDateTime.AddMonths(_srvMth);

                }
            }
            //���¿ͻ���Լ��������
            if (_hasMakeMa)
            {
                if (_dtHead.Rows.Count > 0)
                {
                    _dtHead.Rows[0]["MA_DD"] = _lastUpdatedMa;
                    this.UpdateData(_dsPact);
                }
            }
            return _errorMsg;
        }

        private bool UpdateMa(SunlikeDataSet dsPact,string BIL_TYPE, DateTime maDd, out string errorMsg, out ArrayList maNos)
        {
            bool _result = false;
            errorMsg = "";
            maNos = new ArrayList();
            if (dsPact == null)
                return false;
            if (dsPact.Tables["CUS_PACT"].Rows.Count == 0)
                return false;
            DataRow _drHead = dsPact.Tables["CUS_PACT"].Rows[0];
            DataRow[] _drBodys = dsPact.Tables["TMP_CUSPACT1"].Select();
            Dictionary<string, string> _dtyCust = new Dictionary<string, string>();
            //���ܿͻ�
            if (!string.IsNullOrEmpty(_drHead["CUS_NO"].ToString()))
                _dtyCust.Add(_drHead["CUS_NO"].ToString(), "HEAD");
            foreach (DataRow drBody in _drBodys)
            {
                if (!string.IsNullOrEmpty(drBody["CUS_NO"].ToString()))
                {
                    if (!_dtyCust.ContainsKey(drBody["CUS_NO"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(drBody["CUS_NO"].ToString()))
                            _dtyCust.Add(drBody["CUS_NO"].ToString(), "");
                    }
                }
            }
            try
            {
                foreach (KeyValuePair<string, string> _kvp in _dtyCust)
                {
                    string _cusNo = "";
                    _cusNo = _kvp.Key;
                    #region ����ά�����뵥
                    SunlikeDataSet _dsMa = null;
                    MTNMa _ma = new MTNMa();
                    _dsMa = _ma.GetUpdateData("", "", "MA", "", true);

                    DataRow _drNew = _dsMa.Tables["MF_MA"].NewRow();
                    //��ӱ�ͷ
                    _drNew["MA_ID"] = "MA";
                    _drNew["MA_NO"] = "";
                    _drNew["MA_DD"] = maDd.ToString(Comp.SQLDateFormat);
                    _drNew["CUS_NO"] = _cusNo;
                    _drNew["SAL_NO"] = _drHead["SAL_NO"];
                    _drNew["DEP"] = _drHead["DEP"];
                    //_drNew["CNT_NO"] = "";
                    //_drNew["CNT_REM"] = "";
                    _drNew["BIL_TYPE"] = BIL_TYPE;
                    _drNew["REM"] = "";
                    _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    _drNew["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    _drNew["USR"] = _drHead["USR"];
                    _drNew["CHK_MAN"] = _drHead["USR"];
                    _drNew["MTN_FLOW"] = "2";
                    _drNew["EST_DD"] = maDd.ToString(Comp.SQLDateFormat);
                    _drNew["REM"] = _drHead["PAC_NO"];
                    _drNew["BIL_ID"] = "KH";
                    _drNew["BIL_NO"] = _drHead["PAC_NO"];
                    _dsMa.Tables["MF_MA"].Rows.Add(_drNew);
                    //��ӱ���
                    DataRow[] _drBodySel = null;
                    if (string.Compare("HEAD", _kvp.Value) == 0)
                    {
                        _drBodySel = dsPact.Tables["TMP_CUSPACT1"].Select("ISNULL(CUS_NO,'')='' OR CUS_NO='" + _cusNo + "'");
                    }
                    else
                    {
                        _drBodySel = dsPact.Tables["TMP_CUSPACT1"].Select("CUS_NO='" + _cusNo + "'");
                    }
                    foreach (DataRow drBody in _drBodySel)
                    {
                        _drNew = _dsMa.Tables["TF_MA"].NewRow();
                        _drNew["MA_ID"] = "MA";
                        _drNew["MA_NO"] = "";
                        _drNew["ITM"] = _dsMa.Tables["TF_MA"].Rows.Count + 1;
                        _drNew["PRD_NO"] = drBody["PRD_NO"];
                        _drNew["PRD_MARK"] = drBody["PRD_MARK"];
                        _drNew["WC_NO"] = drBody["WC_NO"];
                        if (!string.IsNullOrEmpty(drBody["WC_NO"].ToString()))
                        {
                            WC _wc = new WC();
                            SunlikeDataSet _dsWc = _wc.GetData(drBody["WC_NO"].ToString());
                            if (_dsWc.Tables["MF_WC"].Rows.Count > 0)
                            {
                                //д�����ñ��̣�������,
                                _drNew["RTN_DD"] = _dsWc.Tables["MF_WC"].Rows[0]["RETURN_DD"];//������
                                //if (string.IsNullOrEmpty(_drNew["RTN_DD"].ToString()))
                                    //_drNew["RTN_DD"] = rtnDd;
                                _drNew["MTN_ALL_ID"] = _dsWc.Tables["MF_WC"].Rows[0]["MTN_ALL_ID"];//���ñ���
                            }


                        }
                        _drNew["UNIT"] = "1";
                        //_drNew["SA_NO"] = "";
                        //_drNew["SA_ITM"] = "";
                        if (string.IsNullOrEmpty(drBody["QTY"].ToString()))
                            _drNew["QTY"] = "1";
                        else
                            _drNew["QTY"] = drBody["QTY"];
                        //_drNew["MTN_DD"] = "";
                        _drNew["EST_DD"] = maDd.ToString(Comp.SQLDateFormat);
                        //_drNew["RTN_DD"] = "";
                        //_drNew["REM"] = "";
                        _drNew["MTN_TYPE"] = "";
                        //_drNew["MTN_ALL_ID"] = "";                
                        _dsMa.Tables["TF_MA"].Rows.Add(_drNew);
                    }

                    _ma.UpdateData("",_dsMa, true);
                    if (_dsMa.Tables["MF_MA"].Rows.Count > 0)
                    {
                        maNos.Add(_dsMa.Tables["MF_MA"].Rows[0]["MA_NO"].ToString());
                    }
                    #endregion
                }
                _result = true;
            }
            catch (Exception _ex)
            {
                errorMsg = _ex.Message.ToString();
                _result = false;
            }
            return _result;

        }
    }
}
