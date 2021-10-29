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
    /// �˻�������
    /// </summary>
    public class DRPYI : Sunlike.Business.BizObject, IAuditing
    {
        private Sunlike.Business.Users _user;
        private Sunlike.Business.PrdMark _prdMark;
        private Sunlike.Business.Data.DbDRPYI _dbDrpyi;
        private Sunlike.Business.SQNO _sqNo;
        private bool _ifEnterAuditing;
        private Sunlike.Business.Auditing _auditing;
        private bool bMemorySave;
        private bool bSaved;

        /// <summary>
        /// �˻�������
        /// </summary>
        public DRPYI()
        {
            _dbDrpyi = new Sunlike.Business.Data.DbDRPYI(Comp.Conn_DB);
            _sqNo = new Sunlike.Business.SQNO();
            _user = new Sunlike.Business.Users();
        }

        #region ȡ�˻�����
        /// <summary>
        /// ȡ�˻�����
        /// </summary>
        /// <param name="cus_No">�û����</param>
        /// <param name="dateTime">��ʱ��</param>
        /// <returns>�˻�����</returns>
        public string GetYI_NO(string cus_No, DateTime dateTime)
        {
            string _yi_No = "";
            try
            {
                string _dept = _user.GetUserDepNo(cus_No);	//ȡ�û�����
                _yi_No = _sqNo.Get("YI", cus_No, _dept, dateTime, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _yi_No;
        }
        #endregion

        #region ȡ�����ֶ�
        /// <summary>
        /// ȡ�����ֶ�
        /// </summary>
        /// <returns>�û��ı�������</returns>
        public System.Data.DataTable PageSetting()
        {
            _prdMark = new Sunlike.Business.PrdMark();
            System.Data.DataTable _dt = null;
            try
            {
                _dt = _prdMark.GetSplitData("");	//ȡ�û��ֶ�
            }
            catch (Exception ex)
            {
                _dt = null;
                throw ex;
            }
            return _dt;
        }
        #endregion

        #region	ȡ��Ʒ����
        /// <summary>
        ///	ȡ��Ӧ��Ʒ����
        /// </summary>
        /// <param name="prd_No">��Ʒ���</param>
        /// <returns>��Ʒ���ݱ�</returns>
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

        #region �����˻���
        /// <summary>
        /// �����˻���
        /// </summary>
        /// <param name="cus_No">�û����</param>
        /// <param name="dateTime">�Ƶ�����</param>
        /// <param name="tf_Dyh">�˻�����</param>
        /// <param name="error">���������Ĳ�Ʒ���źͿ�λ</param>
        /// <param name="fx_wh">������λ</param>
        /// <param name="usr">�Ƶ���</param>
        /// <param name="fuzzy"></param>
        /// <param name="saveId"></param>
        /// <param name="dtError"></param>
        /// <param name="rem">�˻�ԭ��</param>
        public string Insert(string cus_No, string usr, string fx_wh, DateTime dateTime, System.Data.DataTable tf_Dyh, string rem, string fuzzy, bool saveId, out string error, out DataTable dtError)
        {
            bMemorySave = saveId;
            Sunlike.Business.WH _clsWH = new WH();
            StringBuilder _errorQTY = new StringBuilder();	//���������Ĳ�Ʒ���źͿ�λ
            string _yi_NO = "";
            DataTable _dtError = null;
            try
            {
                _auditing = new Auditing();
                string bilType = "";
                //_ifEnterAuditing = _auditing.IsRunAuditing("YI", usr, bilType,"");


                bool _isOk = true;		//������Ƿ��㹻
                bool _isContinue = true;//�Ƿ��������
                System.Data.DataSet _ds = new System.Data.DataSet("DB_" + Comp.CompNo);
                string _dept = _user.GetUserDepNo(cus_No);	//ȡ�û�����
                int _tf_DyhColn = tf_Dyh.Columns.Count;
                int _markColn = _tf_DyhColn - 15;
                _ds = _dbDrpyi.GetMT();
                _yi_NO = _sqNo.Get("YI", usr, _dept, dateTime, "");
                System.Data.DataTable _dtT = _ds.Tables["TF_DYH"];
                System.Data.DataTable _dtM = _ds.Tables["MF_DYH"];
                if (!_ifEnterAuditing)
                {
                    bool _isReturn = false;	//�Ƿ�����
                    foreach (DataRow _dr in tf_Dyh.Rows)
                    {
                        string _prd_No = _dr["PRD_NO"].ToString();
                        _isReturn = this.IsReturn(cus_No, _prd_No, Convert.ToDecimal(_dr["QTY"]));
                        if (!_isReturn)
                        {
                            _isContinue = false;
                            _errorQTY.Append(_prd_No + ";");
                        }
                    }
                }
                if (_isContinue)
                {
                    #region //**MF_DYH��������**//
                    System.Data.DataRow _drM = _dtM.NewRow();
                    _drM["YH_ID"] = "YI";
                    _drM["YH_NO"] = _yi_NO;
                    _drM["DEP"] = _dept;
                    _drM["YH_DD"] = dateTime;
                    _drM["CUS_NO"] = cus_No;
                    _drM["USR"] = usr;
                    _drM["REM"] = Sunlike.Business.CodeConv.ChangeToDBFont(rem);
                    _drM["FX_WH"] = fx_wh;
                    _drM["FUZZY_ID"] = fuzzy;
                    if (bMemorySave)
                    {
                        _drM["SAVE_ID"] = "F";
                    }
                    else
                    {
                        _drM["SAVE_ID"] = "T";
                    }
                    #region �������
                    if (!_ifEnterAuditing)
                    {
                        //_drM["CLS_ID"] = "T";
                        _drM["CLS_DATE"] = dateTime.ToString();
                        _drM["CHK_MAN"] = usr;
                    }
                    #endregion
                    _dtM.Rows.Add(_drM);
                    #endregion
                    #region //**TF_DYH��������**//
                    foreach (DataRow _tf_DyhDr in tf_Dyh.Rows)
                    {
                        System.Data.DataRow _drT = _dtT.NewRow();
                        _drT["YH_ID"] = "YI";
                        _drT["YH_NO"] = _yi_NO;
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
                        _drT["UNIT"] = _tf_DyhDr["UNIT"].ToString().Trim();
                        _drT["AMTN"] = Convert.ToDecimal(_tf_DyhDr["AMTN"]);
                        _drT["UP"] = Convert.ToDecimal(_tf_DyhDr["UP"]);
                        _dtT.Rows.Add(_drT);
                    }
                    #endregion
                    #region �жϿ����
                    if (Comp.DRP_Prop["DRPYI_CHK_QTY"].ToString() != "T")
                    {
                        foreach (DataRow _drTRow in _dtT.Rows)
                        {
                            string _prd_No = _drTRow["PRD_NO"].ToString();
                            string _wh = fx_wh;
                            string _prd_Mark = _drTRow["PRD_MARK"].ToString();
                            decimal _wh_QTY = 0;
                            if (fuzzy == "T")
                            {
                                //_wh_QTY = _clsWH.GetQty(true,_prd_No , _wh);
                                _wh_QTY = _clsWH.GetQty(true, _prd_No, _wh, String.Empty);
                            }
                            else
                            {
                                //_wh_QTY = _clsWH.GetQty(true,_prd_No , _prd_Mark , _wh);
                                _wh_QTY = _clsWH.GetQty(true, _prd_No, _prd_Mark, _wh, String.Empty);
                            }
                            decimal _yi_QTY = Convert.ToDecimal(_drTRow["QTY"]);
                            if (_wh_QTY < _yi_QTY)
                            {
                                _isOk = false;
                                _errorQTY = new StringBuilder(_prd_No + ":" + _prd_Mark + ":" + _wh + ";");
                            }
                        }
                    }
                    #endregion
                    if (_isOk)
                    {
                        _yi_NO = _sqNo.Set("YI", usr, _dept, dateTime, "FX");
                        foreach (DataRow _dr in _dtM.Rows)
                        {
                            _dr["YH_NO"] = _yi_NO;
                        }
                        foreach (DataRow _dr in _dtT.Rows)
                        {
                            _dr["YH_NO"] = _yi_NO;
                        }
                        this.UpdateDataSet(_ds);
                        _dtError = Sunlike.Business.BizObject.GetAllErrors(_ds);
                    }
                }
                else
                {
                    _errorQTY = new StringBuilder("RCID=COMMON.HINT.NOT_RTN;" + _errorQTY);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            dtError = _dtError;
            error = _errorQTY.ToString();
            return _yi_NO;
        }
        #endregion

        #region ȡ��Ʒ��λ
        /// <summary>
        /// ȡ��Ʒ��λ
        /// </summary>
        /// <param name="prd_No">��Ʒ����</param>
        /// <param name="dfu_Ut">Ĭ�ϵ�λ</param>
        /// <returns>��Ʒ��λ��ID����ţ�DSC��������VALUE����ֵ��</returns>
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

        #region ɾ���˻���
        /// <summary>
        /// ɾ���˻���
        /// </summary>
        /// <param name="yi_ID_NO">�˻����ţ�YI;YI43120001��</param>
        public void DeleteYI(string[] yi_ID_NO)
        {
            try
            {
                for (int _i = 0; _i < yi_ID_NO.Length; _i++)
                {
                    string[] _yi = yi_ID_NO[_i].ToString().Split(new char[] { ';' });
                    System.Data.SqlClient.SqlConnection _cn = new System.Data.SqlClient.SqlConnection(Comp.Conn_DB);
                    System.Data.DataSet _ds = new System.Data.DataSet("DB_" + Comp.CompNo);
                    System.Data.DataTable _dtT;
                    System.Data.DataTable _dtM;
                    System.Data.SqlClient.SqlDataAdapter _da = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM TF_DYH WHERE YH_ID = '" + _yi[0] + "' AND YH_NO = '" + _yi[1] + "';SELECT * FROM MF_DYH WHERE YH_ID = '" + _yi[0] + "' AND YH_NO = '" + _yi[1] + "'", _cn);
                    _da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    _da.TableMappings.Add("Table", "TF_DYH");
                    _da.TableMappings.Add("Table1", "MF_DYH");
                    _da.Fill(_ds);
                    _dtM = _ds.Tables["MF_DYH"];
                    _dtT = _ds.Tables["TF_DYH"];
                    foreach (DataRow _dr in _dtT.Rows)
                    {
                        _dr.Delete();
                    }
                    foreach (DataRow _dr in _dtM.Rows)
                    {
                        _dr.Delete();
                    }
                    this.UpdateDataSet(_ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ȡ�˻�������
        /// <summary>
        /// ȡ�˻�������
        /// </summary>
        /// <param name="yi_ID">�˻����</param>
        /// <param name="yi_NO">�˻�����</param>
        /// <param name="prdMarkDt">�����</param>
        /// <returns>ȡ�˻�������</returns>
        public System.Data.DataTable GetYI_Detail(string yi_ID, string yi_NO, DataTable prdMarkDt)
        {
            Sunlike.Business.Prdt _prdt = new Prdt();
            Sunlike.Business.PrdMark _prdMark = new PrdMark();
            string _ut_Name = "";
            int _ut_Qty = 1;
            int _markRow = 0;
            DataTable _yh_DetailDt = new DataTable("TF_DYH");
            #region �����˻�����ϸ��(_yh_DetailDt)
            _yh_DetailDt.Columns.Add("ITM", System.Type.GetType("System.Int32"));
            _yh_DetailDt.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("NAME", System.Type.GetType("System.String"));
            if (prdMarkDt != null)
            {
                _markRow = prdMarkDt.Rows.Count;
                foreach (System.Data.DataRow _dr in prdMarkDt.Rows)
                {
                    _yh_DetailDt.Columns.Add(_dr["FLDNAME"].ToString(), System.Type.GetType("System.String"));
                    _yh_DetailDt.Columns.Add(_dr["FLDNAME"].ToString() + "_SPC", System.Type.GetType("System.String"));
                }
            }
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
            _yh_DetailDt.Columns.Add("REM", System.Type.GetType("System.String"));
            _yh_DetailDt.Columns.Add("DEL_ID", System.Type.GetType("System.String"));
            #endregion
            #region ȡ�˻���TF_DYH��
            DataTable _tf_DyhDt = _dbDrpyi.GetTF_DYH(yi_ID, yi_NO);
            #endregion
            #region �����¼��_yh_DetailDt��
            foreach (DataRow _dr in _tf_DyhDt.Rows)
            {
                DataRow _yh_DetailDr = _yh_DetailDt.NewRow();
                _yh_DetailDr["ITM"] = _dr["ITM"].ToString();
                _yh_DetailDr["PRD_NO"] = _dr["PRD_NO"].ToString();
                _yh_DetailDr["NAME"] = _dr["NAME"].ToString();
                _yh_DetailDr["SPC"] = _dr["SPC"].ToString();
                _yh_DetailDr["WH"] = _dr["WH"].ToString();
                _yh_DetailDr["WH_NAME"] = _dr["WH_NAME"].ToString();
                _yh_DetailDr["EST_DD"] = _dr["EST_DD"].ToString();
                _yh_DetailDr["QTY"] = _dr["QTY"].ToString();
                _yh_DetailDr["QTY_OLD"] = _dr["QTY_OLD"];
                _yh_DetailDr["UNIT"] = _dr["UNIT"].ToString();
                #region ȡ��Ʒ��λ���ƺ͵�λ����
                _prdt.GetUnitDetail(_dr["PRD_NO"].ToString(), _dr["UNIT"].ToString(), out _ut_Name, out _ut_Qty);
                #endregion
                _yh_DetailDr["UNITDSC"] = _ut_Name;
                _yh_DetailDr["UNITQTY"] = _ut_Qty.ToString();
                _yh_DetailDr["AMTN"] = Convert.ToDouble(_dr["AMTN"].ToString());
                _yh_DetailDr["UP"] = Convert.ToDouble(_dr["UP"].ToString());
                _yh_DetailDr["REM"] = _dr["REM"].ToString();
                string _prd_Mark = _dr["PRD_MARK"].ToString();
                if (!String.IsNullOrEmpty(_prd_Mark))
                {
                    int _stratLocation = 0;
                    DataTable _prdMarkDt = null;
                    for (int _i = 0; _i < _markRow; _i++)
                    {
                        _prdMarkDt = _prdMark.GetSplitDetail(prdMarkDt.Rows[_i]["FLDNAME"].ToString(), _dr["PRD_NO"].ToString());
                        string _fldname_Value = _prd_Mark.Substring(_stratLocation, Convert.ToInt32(prdMarkDt.Rows[_i]["SIZE"].ToString()));
                        _yh_DetailDr[prdMarkDt.Rows[_i]["FLDNAME"].ToString()] = _fldname_Value;
                        DataRow[] _drs = _prdMarkDt.Select("VALUE = '" + _fldname_Value + "'");
                        string _fldname_DSC = "";
                        foreach (DataRow _drDSC in _drs)
                        {
                            _fldname_DSC = _drDSC["DSC"].ToString();
                            break;
                        }
                        _yh_DetailDr[prdMarkDt.Rows[_i]["FLDNAME"].ToString() + "_SPC"] = _fldname_DSC;
                        _stratLocation += Convert.ToInt32(prdMarkDt.Rows[_i]["SIZE"].ToString());
                    }
                }
                _yh_DetailDt.Rows.Add(_yh_DetailDr);
            }
            _yh_DetailDt.AcceptChanges();
            #endregion
            return _yh_DetailDt;
        }
        /// <summary>
        /// ȡ�˻�������
        /// </summary>
        /// <param name="yi_ID">�˻����</param>
        /// <param name="yi_NO">�˻�����</param>
        /// <returns>ȡ�˻�������</returns>
        public System.Data.DataTable GetYI_Detail(string yi_ID, string yi_NO)
        {
            DataTable _dt = null;
            try
            {
                _dt = _dbDrpyi.GetTF_DYH(yi_ID, yi_NO);
            }
            catch (Exception ex)
            {
                throw new SunlikeException(ex.Message, ex);
            }
            return _dt;
        }
        #endregion

        #region ȡ�˻�����ͷ
        /// <summary>
        /// ȡ�˻�����ͷ
        /// </summary>
        /// <param name="yi_ID">�˻����</param>
        /// <param name="yi_NO">�˻�����</param>
        /// <param name="cls_ID">�Ƿ����</param>
        /// <returns>ȡ�˻�����ͷ</returns>
        public DataTable GetYI(string yi_ID, string yi_NO, out bool cls_ID)
        {
            bool _cls_ID = false;
            DataTable _dt = new DataTable("MF_DYH");
            try
            {
                _dt = _dbDrpyi.GetMF_DYH(yi_ID, yi_NO, Comp.CompNo);
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        if (_dr["CLS_DATE"] != System.DBNull.Value && !String.IsNullOrEmpty(_dr["CLS_DATE"].ToString()))
                            _cls_ID = true;
                        else
                            _cls_ID = false;
                        break;
                        //						if(_dr["CLS_ID"].ToString() == "T")
                        //							_cls_ID = true;
                        //						else
                        //							_cls_ID = false;
                        //						break;
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

        #region �����˻���
        /// <summary>
        /// �����˻���
        /// </summary>
        /// <param name="yi_ID">�˻����</param>
        /// <param name="yi_NO">�˻�����</param>
        /// <param name="tf_Dyh">�˻�������</param>
        /// <param name="saveId"></param>
        /// <param name="fx_wh">������λ</param>
        /// <param name="errorDt"></param>
        public string Update(string yi_ID, string yi_NO, string fx_wh, DataTable tf_Dyh, bool saveId, out DataTable errorDt)
        {
            bMemorySave = saveId;
            DataTable _errorDt = null;
            string _errorQTY = "";	//���������Ĳ�Ʒ���źͿ�λ
            try
            {
                bool _isOk = true;		//������Ƿ��㹻
                System.Data.DataSet _ds = _dbDrpyi.GetYI(yi_NO);
                System.Data.DataTable _dtM = _ds.Tables["MF_DYH"];
                System.Data.DataTable _dtT = _ds.Tables["TF_DYH"];
                _auditing = new Auditing();
                DataRow dr = _dtM.Rows[0];
                string _bilType = "";
                string _mobID = "";//֧��ֱ������mobID=@@ �򵥾ݲ����������
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
                //_ifEnterAuditing = _auditing.IsRunAuditing("YI", _dtM.Rows[0]["USR"].ToString(), _bilType,_mobID);


                if (bMemorySave)
                {
                    _dtM.Rows[0]["SAVE_ID"] = "F";
                }
                else
                {
                    _dtM.Rows[0]["SAVE_ID"] = "T";
                    #region �������
                    if (!_ifEnterAuditing)
                    {
                        //_drM["CLS_ID"] = "T";
                        _dtM.Rows[0]["CLS_DATE"] = DateTime.Today;
                        _dtM.Rows[0]["CHK_MAN"] = _dtM.Rows[0]["USR"];
                    }
                    #endregion
                }
                foreach (DataRow _dr in _dtM.Rows)
                {
                    _dr["FX_WH"] = fx_wh;
                }
                foreach (DataRow _dr in _dtT.Rows)
                {
                    _dr.Delete();
                }
                #region //**TF_DYH��������**//
                int _tf_DyhColn = tf_Dyh.Columns.Count;
                int _markColn = _tf_DyhColn - 15;
                foreach (DataRow _tf_DyhDr in tf_Dyh.Rows)
                {
                    System.Data.DataRow _drT = _dtT.NewRow();
                    _drT["YH_ID"] = yi_ID;
                    _drT["YH_NO"] = yi_NO;
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
                    _drT["QTY_OLD"] = _tf_DyhDr["QTY_OLD"];
                    _drT["UNIT"] = _tf_DyhDr["UNIT"].ToString().Trim();
                    _drT["AMTN"] = Convert.ToDecimal(_tf_DyhDr["AMTN"]);
                    _drT["UP"] = Convert.ToDecimal(_tf_DyhDr["UP"]);
                    _dtT.Rows.Add(_drT);
                }
                #endregion
                #region �жϿ����
                //				DataRow[] _drTRows = _dtT.Select("","",System.Data.DataViewRowState.Added);
                //				foreach (DataRow _drTRow in _drTRows)
                //				{
                //					string _prd_No = _drTRow["PRD_NO"].ToString();
                //					string _wh = _drTRow["WH"].ToString();
                //					string _prd_Mark = _drTRow["PRD_MARK"].ToString();
                //					int _wh_QTY = 0;
                //					decimal _yi_QTY = Convert.ToDecimal(_drTRow["QTY"]);
                //					if(_drTRow["QTY_OLD"] == System.DBNull.Value)
                //					{
                //						_wh_QTY = _clsWH.GetQty(true,_prd_No , _prd_Mark , fx_wh);
                //						if (_wh_QTY < _yi_QTY)
                //						{
                //							_isOk = false;
                //							_errorQTY = _prd_No + ":" + _wh + ";";
                //						}
                //					}
                //					else
                //					{
                //						_wh_QTY = Convert.ToInt32(_drTRow["QTY_OLD"]);
                //						if (_wh_QTY < _yi_QTY)
                //						{
                //							_isOk = false;
                //							_errorQTY = _prd_No + ":" + _wh + ";";
                //						}
                //					}
                //				}
                #endregion

                if (_isOk)
                {
                    this.UpdateDataSet(_ds);
                    _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            errorDt = _errorDt;
            return _errorQTY;
        }
        #endregion

        #region ��˽ӿ�
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
        /// �˻����᰸
        /// </summary>
        /// <param name="yi_ID">���ݴ���</param>
        /// <param name="yi_No">���ݱ��</param>
        /// <param name="chk_Man">�����</param>
        /// <param name="chk_DD">�������</param>
        /// <returns>������Ϣ</returns>
        public string Approve(string yi_ID, string yi_No, string chk_Man, System.DateTime chk_DD)
        {
            StringBuilder _error = new StringBuilder();
            try
            {
                bool _cls_ID = false;
                bool _isContinue = true;//�Ƿ��������
                string _cus_No = "";
                DataTable _dtM = this.GetYI(yi_ID, yi_No, out _cls_ID);
                DataTable _dtT = this.GetYI_Detail(yi_ID, yi_No);
                foreach (DataRow _dr in _dtM.Rows)
                {
                    _cus_No = _dr["CUS_NO"].ToString();
                    break;
                }
                bool _isReturn = false;	//�Ƿ�����
                foreach (DataRow _dr in _dtT.Rows)
                {
                    if (_dr["DEL_ID"].ToString() != "T")
                    {
                        string _prd_No = _dr["PRD_NO"].ToString();
                        _isReturn = this.IsReturn(_cus_No, _prd_No, Convert.ToDecimal(_dr["QTY"]));
                        if (!_isReturn)
                        {
                            _isContinue = false;
                            _error.Append(_prd_No + ",");
                        }
                    }
                }
                if (_isContinue)
                {
                    //Ҫ�����᰸
                    Sunlike.Business.Data.DbDRPYHut _dbYHut = new DbDRPYHut(Comp.Conn_DB);
                    _dbYHut.Approve(yi_ID, yi_No, chk_Man, chk_DD);
                    //��дԭ������
                    if (_dtM.Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(_dtM.Rows[0]["YH_NO"].ToString()))
                        {
                            foreach (DataRow _dr in _dtT.Rows)
                            {
                                if (_dr["QTY_OLD"] == DBNull.Value)
                                {
                                    _dbYHut.UpdateQtyOld(_dr["YH_ID"].ToString(), _dr["YH_NO"].ToString(), _dr["ITM"].ToString(), "TF_DYH");
                                }
                            }
                        }
                    }
                }
                else
                {
                    _error = new StringBuilder("RCID=COMMON.HINT.NOT_RTN,PARAM=" + _error.ToString());
                }
            }
            catch (Exception ex)
            {
                _error = new StringBuilder(ex.Message.ToString());
            }
            return _error.ToString();
        }
        /// <summary>
        /// ����˻����ݴ������
        /// </summary>
        /// <param name="yi_Id">���ݱ��</param>
        /// <param name="yi_No">���ݱ�</param>
        public string RollBack(string yi_Id, string yi_No)
        {
            return RollBack(yi_Id, yi_No, true);
        }
        /// <summary>
        /// ����˻����ݴ������
        /// </summary>
        /// <param name="yi_Id">���ݱ��</param>
        /// <param name="yi_No">���ݱ�</param>
        public string RollBack(string yi_Id, string yi_No, bool canChangeDS)
        {
            string _error = "";
            try
            {
                DbDRPYI _dbYi = new DbDRPYI(Comp.Conn_DB);
                SunlikeDataSet _ds = _dbYi.GetYI(yi_No);
                this.SetCanModify(_ds, false);
                if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                {
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";
                }
                if (!_dbDrpyi.CheckQty_Rtn(yi_No) && canChangeDS)
                {
                    //Ҫ��������
                    _dbDrpyi.RollBackYI(yi_No);
                }
                else
                {
                    _error = "RCID=INV.HINT.CLOSE_ALERT";
                }
            }
            catch (Exception ex)
            {
                _error = _error + ex.Message.ToString();
            }
            return _error;
        }

        #endregion

        #region �᰸�ӿ�

        /// <summary>
        /// ����Ƿ��Ѿ��᰸
        /// </summary>
        /// <param name="yiNo"></param>
        /// <returns></returns>
        public bool CheckClose(string yiNo)
        {
            DbDRPYI _dbYi = new DbDRPYI(Comp.Conn_DB);
            return _dbYi.CheckClose(yiNo);
        }

        /// <summary>
        /// ���ݽ᰸
        /// </summary>
        /// <param name="bilNo"></param>
        /// <param name="bFlag"></param>
        public void CloseBil(string bilNo, bool bFlag)
        {
            DbDRPYI _drpYi = new DbDRPYI(Comp.Conn_DB);
            _drpYi.CloseBil(bilNo, bFlag);
        }
        #endregion

        #region �жϲ�Ʒ���˷���˻���
        /// <summary>
        ///	�жϲ�Ʒ���˷���˻���(true�������˻���false��������)
        /// </summary>
        /// <param name="cusNo">�ͻ�����</param>
        /// <param name="prdNo">��Ʒ����</param>
        /// <param name="qty"></param>
        /// <returns>true�������˻���false��������</returns>
        public bool IsReturn(string cusNo, string prdNo, decimal qty)
        {
            try
            {
                Cust _cust = new Cust();
                Prdt _prdt = new Prdt();
                int _returnRate = -1;
                decimal _curReturnRate = 0;
                bool _notReturn = false;
                string _prdName = "";
                _prdt.GetPrdt_Rtn(prdNo, out _prdName, out _returnRate, out _notReturn);
                if (_notReturn)
                {
                    return false;
                }
                _curReturnRate = _cust.GetCust_ReturnRate(cusNo, prdNo, qty);
                if (_returnRate > 0)
                {
                    if (Convert.ToDecimal(_returnRate) < (_curReturnRate * 100))
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SunlikeException(ex.Message, ex);
            }
            return true;
        }
        #endregion

        #region	ȡ�˻ص���ϸ����
        /// <summary>
        ///	ȡҪ��/�˻ص���ϸ����
        /// </summary>
        /// <param name="Yh_Id">���ݱ�</param>
        /// <param name="Yh_No">����</param>
        /// <returns>Ҫ��/�˻ص���ϸ����</returns>
        public SunlikeDataSet GetTableYI(string Yh_Id, string Yh_No)
        {
            DbDRPYI _dbDrpYi = new DbDRPYI(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpYi.GetTableYI(Yh_Id, Yh_No);
            return _ds;
        }
        #endregion

        #region	ȡ�˻ص���ϸ����(�����Ź���)
        /// <summary>
        ///	ȡҪ��/�˻ص���ϸ����
        /// </summary>
        /// <param name="Yh_Id">���ݱ�</param>
        /// <param name="Yh_No">����</param>
        /// <returns>Ҫ��/�˻ص���ϸ����</returns>
        public SunlikeDataSet GetTableYI1(string Yh_Id, string Yh_No)
        {
            #region ȡ��DataSet
            DbDRPYI _dbDrpYi = new DbDRPYI(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpYi.GetTableYI1(Yh_Id, Yh_No);
            #endregion

            #region �����������DataSet
            DataTable _dt = _ds.Tables["TF_DYH"];
            foreach (DataRow _dr in _dt.Rows)
            {
                string _whNo = _dr["WH"].ToString();
                string _fxWhNo = _dr["FX_WH"].ToString();
                string _prdNo = _dr["PRD_NO"].ToString();
                string _prdMark = _dr["PRD_MARK"].ToString();
                decimal _whQty = this.GetWhQty(_whNo, _prdNo, _prdMark);
                decimal _cusWhQty = this.GetWhQty(_fxWhNo, _prdNo, _prdMark);
                #region ��ֵ
                _dr["WH_QTY"] = _whQty;
                _dr["CUST_WH_QTY"] = _cusWhQty;
                #endregion
            }
            #endregion
            return _ds;
        }
        #endregion

        #region ����޸��˻�����
        /// <summary>
        /// ����޸��˻�����
        /// </summary>
        /// <param name="Yh_Id"></param>
        /// <param name="Yh_No"></param>
        /// <param name="Itm"></param>
        /// <param name="Qty"></param>
        /// <param name="Est_Dd"></param>
        /// <param name="Wh"></param>
        /// <param name="Change"></param>
        public void UpdateYi(string Yh_Id, string Yh_No, int Itm, decimal Qty, string Est_Dd, string Wh, bool Change)
        {
            DbDRPYI _dbDrpYi = new DbDRPYI(Comp.Conn_DB);
            try
            {
                _dbDrpYi.UpdateYi(Yh_Id, Yh_No, Itm, Qty, Est_Dd, Wh, Change);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion

        #region ����޸��˻�����
        /// <summary>
        /// ����޸��˻�����
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="Yh_Id"></param>
        /// <param name="Yh_No"></param>
        /// <param name="Itm"></param>
        public void DelYi(string tableName, string Yh_Id, string Yh_No, int Itm)
        {
            DbDRPYI _dbDrpYi = new DbDRPYI(Comp.Conn_DB);
            try
            {
                _dbDrpYi.DelYi(tableName, Yh_Id, Yh_No, Itm);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            if (tableName == "TF_DYH" && statementType == StatementType.Insert)
            {
                dr["KEY_ITM"] = dr["ITM"];
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
            if (tableName == "MF_DYH" && !bMemorySave)
            {
                //#region ��˹���
                //AudParamStruct _aps ;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["YH_DD"]);
                //    _aps.BIL_ID = "YI";
                //    _aps.BIL_NO = dr["YH_NO"].ToString();
                //    _aps.BIL_TYPE = "FX";
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = "";
                //    _aps.USR = dr["USR"].ToString();
                //    //_aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
                //}
                //else
                //    _aps = new AudParamStruct("YI", Convert.ToString(dr["YH_NO", DataRowVersion.Original]));            

                //_auditing = new Auditing();
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
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
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
            if (!bSaved && statementType != StatementType.Delete)
            {
                string _yi_No = dr.Table.DataSet.Tables["MF_DYH"].Rows[0]["YH_NO"].ToString();
                string _dataTime = dr.Table.DataSet.Tables["MF_DYH"].Rows[0]["YH_DD"].ToString();
                string _cus_No = dr.Table.DataSet.Tables["MF_DYH"].Rows[0]["USR"].ToString();
                string _dept = dr.Table.DataSet.Tables["MF_DYH"].Rows[0]["DEP"].ToString();
                string _cus = dr.Table.DataSet.Tables["MF_DYH"].Rows[0]["CUS_NO"].ToString();
               
                bSaved = true;
            }
            if (tableName == "MF_DYH" && statementType == StatementType.Delete)
            {
                Sunlike.Business.SQNO _sqNo = new SQNO();
                _sqNo.Delete(dr["YH_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        #endregion

        #region �޸��˻�����
        /// <summary>
        /// �޸��˻�����
        /// </summary>
        /// <param name="YhNo">Ҫ���˻ص���</param>
        /// <param name="KeyItm">Ψһ���</param>
        /// <param name="QtyRtn">�˻�����</param>
        public void UpdateQtyRtn(string YhNo, int KeyItm,decimal QtyRtn)
        {
            DbDRPYI _yi = new DbDRPYI(Comp.Conn_DB);
            _yi.UpdateQtyRtn(YhNo, KeyItm, QtyRtn);
        }
        #endregion

        /// <summary>
        /// �����˻ش��˻����뵥��ȡ
        /// </summary>
        /// <param name="yiNo">�˻����뵥����</param>
        /// <param name="sPrdNo">�D���؛Ʒ</param>
        /// <returns></returns>
        public SunlikeDataSet GetYi4Ib(string yiNo, string sPrdNo)
        {
            DbDRPYI _dbYi = new DbDRPYI(Comp.Conn_DB);
            return _dbYi.GetYi4Ib(yiNo, sPrdNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yiNo"></param>
        /// <returns></returns>
        public bool CheckYiFuzzy(string yiNo)
        {
            DbDRPYI _dbYi = new DbDRPYI(Comp.Conn_DB);
            DataTable _dt = _dbYi.GetMfYi(yiNo).Tables[0];
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["FUZZY_ID"] != DBNull.Value)
                {
                    if (_dt.Rows[0]["FUZZY_ID"].ToString() == "T")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ȡ���˻���ϸ
        /// </summary>
        /// <param name="sYiNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetYiList(string sYiNo)
        {
            DbDRPYI _dbYi = new DbDRPYI(Comp.Conn_DB);
            return _dbYi.GetYiList(sYiNo);
        }

        /// <summary>
        /// ȡ���˻���Ϣ
        /// </summary>
        /// <param name="yiNo">����</param>
        /// <param name="pgm">�������</param>
        /// <param name="usr">�Ƶ���</param>
        /// <returns></returns>
        public SunlikeDataSet GetYI(string yiNo, string pgm, string usr)
        {
            DbDRPYI _dbYi = new DbDRPYI(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbYi.GetYI(yiNo);
            DataTable _dtMf = _ds.Tables["MF_DYH"];
            if (_dtMf.Rows.Count > 0)
            {
                string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                this.SetCanModify(_ds, true);
            }
            return _ds;
        }

        #region SetCanModify
        /// <summary>
        /// ��鵥���Ƿ�����޸�
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">�Ƿ��ж��������</param>
        public void SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            DataTable _dtMf = ds.Tables["MF_DYH"];
            DataTable _dtTf = ds.Tables["TF_DYH"];
            bool _bCanModify = true;
            if (_dtMf.Rows.Count > 0)
            {
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["YH_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
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
                if (_bCanModify)
                {
                    DataRow[] _aryDr = _dtTf.Select("QTY_RTN > 0");
                    if (_aryDr.Length > 0)
                    {
                        _bCanModify = false;
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
        }
        #endregion
        /// <summary>
        /// ȡ�ÿ����
        /// </summary>
        /// <param name="whNo">��λ</param>
        /// <param name="prdNo">��Ʒ��</param>
        /// <param name="prdMark">��Ʒ����</param>
        /// <returns>�����</returns>
        public decimal GetWhQty(string whNo, string prdNo,string prdMark)
        {
            bool _isSubStock = true;//�Ƿ�����
            bool _isBatNo = true;//�Ƿ����Ź���
            Sunlike.Business.WH _wh = new WH();
            string[] _whAry = whNo.Trim().Split(new char[] { ';' });
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
            if (_isBatNo)
            {
                Prdt _prdt = new Prdt();
                _isBatNo = _prdt.CheckBat(prdNo);
            }
            decimal _whQty = 0;
            SunlikeDataSet _dsWh = _wh.GetData(_whStr.ToString());
            DataTable _dtWh = _dsWh.Tables[0];
            if (_dtWh.Rows.Count > 0)
            {
                for (int i = 0; i < _dtWh.Rows.Count; i++)
                {
                    if (_dtWh.Rows[i]["CHK_WH"] == DBNull.Value)
                    {
                        _isSubStock = false;
                    }
                    else if (Comp.DRP_Prop["DRPYH_WH_VIEWUNDER"].ToString() == "F")
                    {
                        _isSubStock = false;
                    }
                    if (Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
                    {
                        _whQty += _wh.GetSumQty(false, prdNo, prdMark, _dtWh.Rows[i]["WH"].ToString(), _isSubStock, null, _isBatNo);
                    }
                    else
                    {
                        _whQty += _wh.GetSumQty(true, prdNo, prdMark, _dtWh.Rows[i]["WH"].ToString(), _isSubStock, null, _isBatNo);
                    }
                    _isSubStock = true;
                }
            }
            return _whQty;
        }



    }
}
