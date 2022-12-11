using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Sunlike.Business;
using Sunlike.Common.CommonVar;
using System.Text;

namespace BarPrintService
{
	/// <summary>
	/// Summary description for BarPrint
	/// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class BarPrintServer : System.Web.Services.WebService
    {
        #region ��ġ����Ʒ�ͷ��������ӡ
        [WebMethod]
		public string GetMaxSN(string _prd_no,string _bat_no)
		{
			//ȡ�����ˮ��
			BarPrint _br = new BarPrint();
			return _br.GetMaxNo(_prd_no, _bat_no);
		}
		[WebMethod]
		public bool IsPrinted(string bar_no, string sn, int page_count)
		{
            //�ж������Ƿ��Ѵ�ӡ
            BarPrint _br = new BarPrint();
			return _br.IsPrinted(bar_no, sn, page_count);
        }
        [WebMethod]
        public bool BCIsPrinted(string _barNo, string _bat_no, string _sn)
        {
            //�޸İ������ʱ
            //�жϰ�������Ƿ��Ѵ�ӡ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.BCIsPrinted(_barNo, _bat_no, _sn);
        }
        [WebMethod]
        public bool SBIsPrinted(string _bat_no, string _sn)
        {
            //�ж��˻������Ƿ��Ѵ�ӡ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.SBIsPrinted(_bat_no, _sn);
        }
        [WebMethod]
        public bool InUse(string _batNo)
        {
            //�жϷ��������Ƿ���ʹ��
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.InUse(_batNo);
        }
        [WebMethod]
        public DataSet GetBarRec(string _barCode)
        {
            //ȡ������Ϣ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarRecord(_barCode);
        }
        [WebMethod]
        public DataSet GetBarChg(string _barCode)
        {
            //ȡ������Ϣ(����ʱ����)
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarChg(_barCode);
        }
        [WebMethod]
        public DataSet GetBarRec1(string _barCodeLst)
        {
            //ȡ������Ϣ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarRecord1(_barCodeLst);
        }
        [WebMethod]
        public DataSet GetBarRecCanDel(string _where)
        {
            //ȡ������Ϣ(ɾ������ʱ��)
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarRecCanDel(_where);
        }
		[WebMethod]
		public DataSet SavePrintData(string usr, DataSet prtHisDS)
		{
			//������ӡʱ�������ӡ��Ϣ
			BussBarPrint _br = new BussBarPrint();
            return _br.SavePrintData(usr, prtHisDS, false, false, false);
        }
        [WebMethod]
        public DataSet ModifyPrintData(string usr, DataSet prtHisDS)
        {
            //�޸�����ʱ�������ӡ��Ϣ
            BussBarPrint _br = new BussBarPrint();
            return _br.SavePrintData(usr, prtHisDS, false, false, true);
        }
        [WebMethod]
        public DataSet SaveInitPrintData(string usr, DataSet prtHisDS)
        {
            //����Ʒ�����ӡʱ�������ӡ��Ϣ
            BussBarPrint _br = new BussBarPrint();
            return _br.SavePrintData(usr, prtHisDS, true, false, false);
        }
        [WebMethod]
        public DataSet SaveChangePrintData(string usr, DataSet prtHisDS)
        {
            //���������ӡʱ�������ӡ��Ϣ
            BussBarPrint _br = new BussBarPrint();
            return _br.SavePrintData(usr, prtHisDS, false, true, false);
        }
        [WebMethod]
        public void DeleteBarCode(string _where)
        {
            //ɾ������
            BussBarPrint _br = new BussBarPrint();
            _br.DeleteBarCode(_where);
        }
        #endregion

        #region ת����ҵ
        [WebMethod]
        public void UpdatePassData(DataSet dataSet)
        {
            //ת�ɿⵥ
            BussBarPrint _bbp = new BussBarPrint();
            _bbp.TOMM(dataSet);
        }
        [WebMethod]
        public void UpdateFaultData(DataSet dataSet)
        {
            //ת�쳣֪ͨ��
            BussBarPrint _bbp = new BussBarPrint();
            _bbp.TOTY(dataSet);
        }
        [WebMethod]
        public DataSet GetDataToTY(string _sqlWhere)
        {
            //ȡ��ת�쳣�������к�
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetDataToTY(_sqlWhere);
        }
        [WebMethod]
        public DataSet GetUndoTRBar(string _prdType, string _where)
        {
            //��ת�쳣�������루ת�쳣��������ҵʱ���ã�
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetUndoTRBar(_prdType, _where);
        }
        [WebMethod]
        public string UndoTR(string _barNoLst)
        {
            //ת�쳣������
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.UndoTR(_barNoLst);
        }
        #endregion

        #region Ʒ�ʼ���
        [WebMethod]
        public DataSet GetBarUnQC(string _barNo)
        {
            //ȡ�������������Ϣ(����ɨ��)
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarUnQC(_barNo);
        }
        [WebMethod]
        public DataSet GetBarUnQCLst(string _barType)
        {
            //ȡ�������������Ϣ��ȫ����ʾ��
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarUnQCLst(_barType);
        }
        [WebMethod]
        public DataSet GetDataQC(bool _getSchema)
        {
            //ȡ����Ʒ����Ϣ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetDataQC(_getSchema);
        }
        [WebMethod]
        public DataSet GetDataQC1(string _sqlWhere)
        {
            //ȡ����Ʒ����Ϣ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetDataQC1(_sqlWhere);
        }
        [WebMethod]
        public void UpdataDataQC(DataSet changeDS)
        {
            //��������Ʒ����Ϣ
            string _barNoList = "";
            foreach (DataRow _dr in changeDS.Tables[0].Rows)
            {
                if (_barNoList != "")
                    _barNoList += "','";
                _barNoList += _dr["BAR_NO"].ToString();
            }
            DataSet _ds = GetDataQC1(" AND BAR_NO IN ('" + _barNoList + "')");
            if (changeDS != null && changeDS.Tables.Count > 0)
            {
                DataRow[] _drAry = null;
                foreach (DataRow _dr in changeDS.Tables[0].Rows)
                {
                    DataRow _drNew = null;
                    _drAry = _ds.Tables[0].Select("BAR_NO='" + _dr["BAR_NO"].ToString() + "'");
                    if (_drAry != null && _drAry.Length > 0)
                    {
                        _drNew = _drAry[0];
                        if (changeDS.Tables[0].Columns.Contains("OS_BAR_NO"))
                            _drNew["OS_BAR_NO"] = _dr["OS_BAR_NO"];
                        _drNew["QC"] = _dr["QC"];
                        if (changeDS.Tables[0].Columns.Contains("SPC_NO"))
                            _drNew["SPC_NO"] = _dr["SPC_NO"];
                        _drNew["STATUS"] = _dr["STATUS"];//״̬��QC��Ʒ�죻ML�����ϣ�FT��������SW���ͽɣ�TY��ת�쳣
                        //_drNew["BAT_NO"] = _dr["BAT_NO"];
                        _drNew["BIL_ID"] = _dr["BIL_ID"];
                        _drNew["BIL_NO"] = _dr["BIL_NO"];
                        _drNew["REM"] = _dr["REM"];
                        if (changeDS.Tables[0].Columns.Contains("WH_SJ"))//�ͽɿ�λ
                            _drNew["WH_SJ"] = _dr["WH_SJ"];
                        if (changeDS.Tables[0].Columns.Contains("PRC_ID"))//����ʽ
                            _drNew["PRC_ID"] = _dr["PRC_ID"];
                        if (changeDS.Tables[0].Columns.Contains("USR_QC"))//Ʒ����Ա
                            _drNew["USR_QC"] = _dr["USR_QC"];
                        if (changeDS.Tables[0].Columns.Contains("QC_DATE"))//Ʒ��ʱ��
                            _drNew["QC_DATE"] = _dr["QC_DATE"];
                        if (changeDS.Tables[0].Columns.Contains("USR_SJ"))//�ͽ���Ա
                            _drNew["USR_SJ"] = _dr["USR_SJ"];
                        if (changeDS.Tables[0].Columns.Contains("SJ_DATE"))//�ͽ�ʱ��
                            _drNew["SJ_DATE"] = _dr["SJ_DATE"];
                        if (changeDS.Tables[0].Columns.Contains("CUS_NO"))//�ͽ�ʱ��
                            _drNew["TO_CUST"] = _dr["CUS_NO"];
                    }
                    else
                    {
                        _drNew = _ds.Tables[0].NewRow();
                        _drNew["BAR_NO"] = _dr["BAR_NO"];
                        if (changeDS.Tables[0].Columns.Contains("OS_BAR_NO"))
                            _drNew["OS_BAR_NO"] = _dr["OS_BAR_NO"];
                        _drNew["QC"] = _dr["QC"];
                        if (changeDS.Tables[0].Columns.Contains("SPC_NO"))
                            _drNew["SPC_NO"] = _dr["SPC_NO"];
                        _drNew["STATUS"] = _dr["STATUS"];//״̬��QC��Ʒ�죻ML�����ϣ�FT��������SW���ͽ�
                        _drNew["BAT_NO"] = _dr["BAT_NO"];
                        _drNew["BIL_ID"] = _dr["BIL_ID"];
                        _drNew["BIL_NO"] = _dr["BIL_NO"];
                        _drNew["REM"] = _dr["REM"];
                        if (changeDS.Tables[0].Columns.Contains("WH_SJ"))//�ͽɿ�λ
                            _drNew["WH_SJ"] = _dr["WH_SJ"];
                        if (changeDS.Tables[0].Columns.Contains("PRC_ID"))//����ʽ
                            _drNew["PRC_ID"] = _dr["PRC_ID"];
                        if (changeDS.Tables[0].Columns.Contains("USR_QC"))//Ʒ����Ա
                            _drNew["USR_QC"] = _dr["USR_QC"];
                        if (changeDS.Tables[0].Columns.Contains("QC_DATE"))//Ʒ��ʱ��
                            _drNew["QC_DATE"] = _dr["QC_DATE"];
                        if (changeDS.Tables[0].Columns.Contains("USR_SJ"))//�ͽ���Ա
                            _drNew["USR_SJ"] = _dr["USR_SJ"];
                        if (changeDS.Tables[0].Columns.Contains("SJ_DATE"))//�ͽ�ʱ��
                            _drNew["SJ_DATE"] = _dr["SJ_DATE"];
                        if (changeDS.Tables[0].Columns.Contains("CUS_NO"))//�ͽ�ʱ��
                            _drNew["TO_CUST"] = _dr["CUS_NO"];
                        _ds.Tables[0].Rows.Add(_drNew);
                    }
                }
            }
            BussBarPrint _br = new BussBarPrint();
            _br.UpdataDataQC(_ds);
        }
        [WebMethod]
        public void UpdataDataBoxBat(DataSet changeDS)
        {
            //��Ʒ�ͽ�ʱ����
            //������������Ϣ
            BussBarPrint _br = new BussBarPrint();
            StringBuilder _boxNoList = new StringBuilder();
            foreach (DataRow _dr in changeDS.Tables[0].Rows)
            {
                if (_boxNoList.Length > 0)
                    _boxNoList.Append("','");
                _boxNoList.Append(_dr["BOX_NO"].ToString());
            }
            DataSet _ds = _br.GetDataBox(" AND BOX_NO IN ('" + _boxNoList.ToString() + "')");

            _ds.Tables[0].Columns.Add("USR_SJ", System.Type.GetType("System.String"));
            _ds.Tables[0].Columns.Add("SJ_DATE", System.Type.GetType("System.DateTime"));

            if (changeDS != null && changeDS.Tables.Count > 0)
            {
                DataRow[] _drAry = null;
                foreach (DataRow _dr in changeDS.Tables[0].Rows)
                {
                    DataRow _drNew = null;
                    _drAry = _ds.Tables[0].Select("BOX_NO='" + _dr["BOX_NO"].ToString() + "'");
                    if (_drAry != null && _drAry.Length > 0)
                    {
                        _drNew = _drAry[0];
                        _drNew["STATUS"] = _dr["STATUS"];//״̬��QC��Ʒ�죻ML�����ϣ�FT��������SW���ͽɣ�TY��ת�쳣
                        _drNew["BIL_ID"] = _dr["BIL_ID"];
                        _drNew["BIL_NO"] = _dr["BIL_NO"];
                        _drNew["WH_SJ"] = _dr["WH_SJ"];//�ͽɿ�λ
                        _drNew["REM"] = _dr["REM"];
                        _drNew["USR_SJ"] = _dr["USR_SJ"];
                        _drNew["SJ_DATE"] = _dr["SJ_DATE"];
                    }
                    else
                    {
                        _drNew = _ds.Tables[0].NewRow();
                        _drNew["BOX_NO"] = _dr["BOX_NO"];
                        _drNew["STATUS"] = _dr["STATUS"];//״̬��QC��Ʒ�죻ML�����ϣ�FT��������SW���ͽɣ�TY��ת�쳣
                        _drNew["BIL_ID"] = _dr["BIL_ID"];
                        _drNew["BIL_NO"] = _dr["BIL_NO"];
                        _drNew["WH_SJ"] = _dr["WH_SJ"];//�ͽɿ�λ
                        _drNew["REM"] = _dr["REM"];
                        _drNew["USR_SJ"] = _dr["USR_SJ"];
                        _drNew["SJ_DATE"] = _dr["SJ_DATE"];
                        _ds.Tables[0].Rows.Add(_drNew);
                    }
                }
            }
            _br.UpdataDataBoxBat(_ds);
        }
        [WebMethod]
        public void UpdataDataSPC(DataSet changeDS)
        {
            //���²��ϸ�ԭ��
            BussBarPrint _br = new BussBarPrint();
            _br.UpdataDataSPC(changeDS);
        }
        #endregion

        #region �ͽ���ҵ
        [WebMethod]
        public DataSet GetBCUnToMMLst(string _where)
        {
            //ȡ�Ѽ���δ�ͽɵİ��������Ϣ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBCUnToMMLst(_where);
        }
        [WebMethod]
        public DataSet GetFTUnToMMLst()
        {
            //ȡ�Ѽ���δ�ͽɵĳ�Ʒ������Ϣ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetFTUnToMMLst();
        }
        [WebMethod]
        public string UndoToMM(string _barNoLst, string _boxNoList, string _usr)
        {
            //�����ͽ�
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.UndoToMM(_barNoLst, _boxNoList, _usr);
        }
        #endregion

        #region �ɿ���ҵ
        [WebMethod]
        public DataSet GetBCUnMMLst(string _where)
        {
            //ȡ���ͽ�δ�ɿ��������Ϣ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBCUnMMLst(_where);
        }
        [WebMethod]
        public DataSet GetBoxUnMM(string _where)
        {
            //ȡ���ͽ�δ�ɿ��������
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBoxUnMM(_where);
        }
        [WebMethod]
        public DataSet GetBoxUnMMLst(string _where)
        {
            //ȡ���ͽ�δ�ɿ�������뼰����ϸ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBoxUnMMLst(_where);
        }
        [WebMethod]
        public DataSet GetUndoMMBar(string _prdType, string _where)
        {
            //�ѽɿ�����루�ɿ⳷����ҵʱ���ã�
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetUndoMMBar(_prdType, _where);
        }
        [WebMethod]
        public string UndoMM(string _barNoLst, string _boxNoList)
        {
            //�ɿ⳷��
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.UndoMM(_barNoLst, _boxNoList);
        }
        #endregion

        #region �޸�Ʒ����Ϣ
        [WebMethod]
        public DataSet GetBarQC(string _barNo, string _barType)
        {
            //�޸�Ʒ����Ϣʱ����
            // ȡ�ô��޸�Ʒ����Ϣ�����к�
            // _barNo�����к�
            // _barType����Ʒ���ͣ�1:��ģ�2:���Ʒ��3:��Ʒ��
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarQC(_barNo, _barType);
        }
        [WebMethod]
        public void UndoBarQC(string _barNo)
        {
            //�޸�Ʒ����Ϣʱ����
            //����Ʒ��
            BussBarPrint _bbp = new BussBarPrint();
            _bbp.UndoBarQC(_barNo);
        }
        #endregion

        #region �����

        #region ����Ϣ��ѯ
        [WebMethod]
        public DataSet GetBoxBar(string _boxBar, string _isBox)
        {
            //ȡ�Ѵ�ӡ������:_boxBar:������;_isBox:T:��װ���������;F:δװ���������;null���:����
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBar(_boxBar, _isBox, " AND ISNULL(A.STOP_ID,'F')<>'T' ");
        }
        [WebMethod]
        public DataSet GetBoxBar1(string _sqlWhere)
        {
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBar("", "", _sqlWhere);
        }
        [WebMethod]
        public DataSet GetBoxBarRec(string _where)
        {
            //ȡ�÷�������
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBarRec(_where);
        }
        [WebMethod]
        public DataSet GetBoxBarPrint(string _boxNo)
        {
            //ȡ�÷�������
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBarPrint(_boxNo);
        }
        #endregion

        #region װ����ҵ
        [WebMethod]
        public string BarBoxing(string _boxNo,string _barCode,string _usr,string _dep)
        {
            //װ��
            string _err = "";
            BussBarPrint _bc = new BussBarPrint();
            DataSet _changedDS = _bc.GetBarBoxRec(_boxNo, _barCode);
            if (_changedDS != null && _changedDS.Tables.Count == 2 && _changedDS.Tables[0].Rows.Count > 0 && _changedDS.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToInt32(_changedDS.Tables[0].Rows[0]["QTY"]) != _changedDS.Tables[1].Rows.Count)
                {
                    _err += "װ����������!\n";
                }
                else
                {
                    _changedDS.Tables[0].Columns.Add("FLAG", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("USR_BOX", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("BOX_DATE", System.Type.GetType("System.DateTime"));
                    _changedDS.Tables[0].Rows[0]["BOX_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                    _changedDS.Tables[0].Rows[0]["USR"] = _usr;
                    _changedDS.Tables[0].Rows[0]["DEP"] = _dep;
                    _changedDS.Tables[0].Rows[0]["QC"] = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    _changedDS.Tables[0].Rows[0]["CONTENT"] = "=" + _changedDS.Tables[1].Rows.Count + ";";
                    //_changedDS.Tables[0].Rows[0]["PRD_NO"] = _changedDS.Tables[1].Rows[0]["PRD_NO"];

                    string _time = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    string _prdNo = _changedDS.Tables[0].Rows[0]["PRD_NO"].ToString();//��Ʒ��
                    string _batNo = _changedDS.Tables[0].Rows[0]["BAT_NO"].ToString();//������
                    string _qc = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    string _batNo1 = "";
                    foreach (DataRow dr in _changedDS.Tables[1].Rows)
                    {
                        if (_prdNo != dr["PRD_NO"].ToString())
                        {
                            _err += "��Ʒ[" + dr["PRD_NO"].ToString() + "]����װ�����!\n";
                            break;
                        }
                        if (_batNo.IndexOf(dr["BAR_NO"].ToString().Substring(12, 7)) < 0)
                        {
                            _err += "��Ʒ�����������Ų�һ�£�����װ�����!\n";
                            break;
                        }
                        else
                        {
                            if (_batNo1 != "")
                                _batNo1 += "/";
                            _batNo1 += dr["BAR_NO"].ToString().Substring(12, 7);
                        }
                        if (_qc != dr["QC"].ToString())
                        {
                            _err += "��ƷƷ����Ϣ��һ�£�����װ��!\n";
                            break;
                        }

                        dr["BOX_NO"] = _boxNo;
                        dr["UPDDATE"] = _time;
                        dr["USR_BOX"] = _usr;
                        dr["BOX_DATE"] = _time;
                    }
                    string[] _batArr = _batNo.Split('/');
                    foreach (string _bat in _batArr)
                    {
                        if (_batNo1.IndexOf(_bat) < 0)
                            _err += "����δװȫ��";
                    }
                    if (_err == "")
                    {
                        DataTable _dtErr = _bc.BarBoxing(_changedDS);
                        if (_dtErr.Rows.Count > 0)
                            throw new Exception(_dtErr.Rows[0]["REM"].ToString());
                    }
                }
            }
            else
                _err += "��¼Ϊ�գ�������װ��!\n";
            return _err;
        }
        [WebMethod]
        public string InitBarBoxing(string _boxNo, string _barCode, string _wh, string _rem, string _usr, string _dep)
        {
            //װ��
            string _err = "";
            BussBarPrint _bc = new BussBarPrint();
            DataSet _changedDS = _bc.GetBarBoxRec(_boxNo, _barCode);
            if (_changedDS != null && _changedDS.Tables.Count == 2 && _changedDS.Tables[0].Rows.Count > 0 && _changedDS.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToInt32(_changedDS.Tables[0].Rows[0]["QTY"]) != _changedDS.Tables[1].Rows.Count)
                {
                    _err += "װ������������\n";
                }
                else
                {
                    _changedDS.Tables[0].Columns.Add("FLAG", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("USR_BOX", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("BOX_DATE", System.Type.GetType("System.DateTime"));
                    _changedDS.Tables[0].Rows[0]["FLAG"] = "T";
                    _changedDS.Tables[0].Rows[0]["BOX_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                    _changedDS.Tables[0].Rows[0]["USR"] = _usr;
                    _changedDS.Tables[0].Rows[0]["DEP"] = _dep;
                    _changedDS.Tables[0].Rows[0]["WH"] = _wh;

                    _changedDS.Tables[0].Rows[0]["CONTENT"] = "=" + _changedDS.Tables[1].Rows.Count + ";";
                    //_changedDS.Tables[0].Rows[0]["PRD_NO"] = _changedDS.Tables[1].Rows[0]["PRD_NO"];

                    string _time = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    string _prdNo = _changedDS.Tables[0].Rows[0]["PRD_NO"].ToString();
                    string _batNo = _changedDS.Tables[0].Rows[0]["BAT_NO"].ToString(); 
                    string _qc = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    string _batNo1 = "";
                    foreach (DataRow dr in _changedDS.Tables[1].Rows)
                    {
                        if (_prdNo != dr["PRD_NO"].ToString())
                        {
                            _err += "��Ʒ[" + dr["PRD_NO"].ToString() + "]����װ����䣡\n";
                            break;
                        }
                        if (_batNo.IndexOf(dr["BAR_NO"].ToString().Substring(12, 7)) < 0)
                        {
                            _err += "��Ʒ�����������Ų�ͳһ������װ����䣡\n";
                            break;
                        }
                        else
                        {
                            if (_batNo1 != "")
                                _batNo1 += "/";
                            _batNo1 += dr["BAR_NO"].ToString().Substring(12, 7);
                        }
                        if (_qc != dr["QC"].ToString())
                        {
                            _err += "��ƷƷ����Ϣ��һ�£�����װ��!\n";
                            break;
                        }

                        dr["BOX_NO"] = _boxNo;
                        dr["WH"] = _wh;
                        dr["UPDDATE"] = _time;
                        dr["REM"] = _rem;
                        dr["USR_BOX"] = _usr;
                        dr["BOX_DATE"] = _time;
                    }
                    string[] _batArr = _batNo.Split('/');
                    foreach (string _bat in _batArr)
                    {
                        if (_batNo1.IndexOf(_bat) < 0)
                            _err += "����δװȫ��";
                    }
                    if (_err == "")
                    {
                        DataTable _dtErr = _bc.BarBoxing(_changedDS);
                        if (_dtErr.Rows.Count > 0)
                            throw new Exception(_dtErr.Rows[0]["REM"].ToString());
                    }
                }
            }
            return _err;
        }
        [WebMethod]
        public string WHBarBoxing(string _boxNo, string _barCode, string _usr, string _dep, string _wh)
        {
            //װ��
            string _err = "";
            BussBarPrint _bc = new BussBarPrint();
            DataSet _changedDS = _bc.GetBarBoxRec(_boxNo, _barCode);
            if (_changedDS != null && _changedDS.Tables.Count == 2 && _changedDS.Tables[0].Rows.Count > 0 && _changedDS.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToInt32(_changedDS.Tables[0].Rows[0]["QTY"]) != _changedDS.Tables[1].Rows.Count)
                {
                    _err += "װ������������\n";
                }
                else
                {
                    _changedDS.Tables[0].Columns.Add("FLAG", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("USR_WHBOX", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("WHBOX_DATE", System.Type.GetType("System.DateTime"));
                    _changedDS.Tables[0].Rows[0]["BOX_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                    _changedDS.Tables[0].Rows[0]["USR"] = _usr;
                    _changedDS.Tables[0].Rows[0]["DEP"] = _dep;
                    _changedDS.Tables[0].Rows[0]["WH"] = _wh;
                    _changedDS.Tables[0].Rows[0]["QC"] = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    _changedDS.Tables[0].Rows[0]["CONTENT"] = "=" + _changedDS.Tables[1].Rows.Count + ";";
                    //_changedDS.Tables[0].Rows[0]["PRD_NO"] = _changedDS.Tables[1].Rows[0]["PRD_NO"];

                    string _time = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    string _prdNo = _changedDS.Tables[0].Rows[0]["PRD_NO"].ToString();
                    string _batNo = _changedDS.Tables[0].Rows[0]["BAT_NO"].ToString();
                    string _qc = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    string _batNo1 = "";
                    foreach (DataRow dr in _changedDS.Tables[1].Rows)
                    {
                        if (_prdNo != dr["PRD_NO"].ToString())
                        {
                            _err += "��Ʒ[" + dr["PRD_NO"].ToString() + "]����װ����䣡\n";
                            break;
                        }
                        if (_batNo.IndexOf(dr["BAR_NO"].ToString().Substring(12, 7)) < 0)
                        {
                            _err += "��Ʒ�����������Ų�ͳһ������װ����䣡\n";
                            break;
                        }
                        else
                        {
                            if (_batNo1 != "")
                                _batNo1 += "/";
                            _batNo1 += dr["BAR_NO"].ToString().Substring(12, 7);
                        }
                        if (_qc != dr["QC"].ToString())
                        {
                            _err += "��ƷƷ����Ϣ��ͳһ������װ��";
                            break;
                        }

                        dr["BOX_NO"] = _boxNo;
                        dr["UPDDATE"] = _time;
                        dr["USR_WHBOX"] = _usr;
                        dr["WHBOX_DATE"] = _time;
                    }
                    string[] _batArr = _batNo.Split('/');
                    foreach (string _bat in _batArr)
                    {
                        if (_batNo1.IndexOf(_bat) < 0)
                            _err += "����δװȫ��";
                    }
                    if (_err == "")
                    {
                        DataTable _dtErr = _bc.BarBoxing(_changedDS);
                        if (_dtErr.Rows.Count > 0)
                            throw new Exception(_dtErr.Rows[0]["REM"].ToString());
                    }
                }
            }
            else
                _err += "��¼Ϊ�գ�������װ��!\n";
            return _err;
        }
        [WebMethod]
        public void UnDoBox(string _boxNo, string _usr, string _type)
        {
            //����
            BussBarPrint _bc = new BussBarPrint();
            _bc.UnDoBox(_boxNo, _usr, _type);
        }
        #endregion

        #region �������ӡ
        [WebMethod]
        public DataSet SaveBoxPrintData(string usr, DataSet prtHisDS)
        {
            //�����������ӡ��Ϣ
            BussBarPrint _br = new BussBarPrint();
            return _br.SaveBoxPrintData(usr, prtHisDS);
        }
        [WebMethod]
        public DataSet UpdateBoxPrintData(DataSet _changedDS)
        {
            //�����������ӡ��Ϣ
            BussBarPrint _br = new BussBarPrint();
            DataSet _ds = new DataSet();
            _ds.Merge(_br.UpdataDataBox(_changedDS));
            return _ds;
        }
        #endregion

        #endregion

        #region ��������
        //public CredentialSoapHeader _header = new CredentialSoapHeader();
        //[SoapHeader("_header")]
        [WebMethod]
        public DataSet GetUsers()
        {
            //if (_header.UserName != "oNlInEeRp" || _header.UserPassword != "oNlInEeRp")
            //{
            //    _header.DidUnderstand = false;
            //}
            //else
            //    _header.DidUnderstand = true;
            DataSet _ds = new DataSet();
            Query _query = new Query();
            string _sql = "select USR,NAME,DEP,(SELECT TOP 1 DEP FROM DEPT) AS TOP_DEP,COMPNO from SUNSYSTEM..PSWD where COMPNO='" + Comp.CompNo + "' and isnull(ISGROUP,'')='T' and (isnull(E_DAT,getdate()+1)>getdate() OR E_DAT='1899-12-30')";
            _ds = _query.DoSQLString(Comp.Conn_DB, _sql);
            return _ds;
        }
        [WebMethod]
		public DataSet GetBarRole()
		{
			//ȡ����ֶι���
			BarCode _bc = new BarCode();
			DataSet _ds = (DataSet)(_bc.GetBarRoleData());
            _ds.ExtendedProperties["DB_LANGUAGE"] = Comp.DataBaseLanguage;
			return _ds;
        }
        [WebMethod]
        public DataSet GetBarDoc()
        {
            //ȡ����ֶι���
            BarCode _bc = new BarCode();
            DataSet _ds = new DataSet();
            _ds.Merge(_bc.GetBarDoc(""));
            return _ds;
        }
        [WebMethod]
        public string GetBarConfig(string _dep)
        {
            //�����ԭ��
            string _pat = "";
            BarBox _barBox = new BarBox();
            SunlikeDataSet _dsBoxConfig = _barBox.GetBoxConfig(_dep);
            if (_dsBoxConfig.Tables["BOX_CONFIG"].Rows.Count > 0)
            {
                _pat = _dsBoxConfig.Tables["BOX_CONFIG"].Rows[0]["PAT"].ToString();
            }
            return _pat;
        }
        [WebMethod]
        public bool LoginValidate(string _uid, string _pwd)
        {
            //��¼�û���֤
            BussBarPrint _bs = new BussBarPrint();
            return _bs.CheckUserPswd(_uid, _pwd);
        }
        [WebMethod]
        public string GetUsrDep(string _uid)
        {
            //ȡ�û���������
            string _depNo = "";
            try
            {
                Users _user = new Users();
                _depNo = _user.GetUserDepNo(_uid);
            }
            catch
            {
                Dept _dep = new Dept();
                _depNo = _dep.GetTopDept();
            }
            return _depNo;
        }
        [WebMethod]
        public DataSet GetComp()
        {
            //ȡ��˾����
            DataSet _ds = new DataSet();
            Comp _comp = new Comp();
            _ds=_comp.GetData(Comp.CompNo, false);
            return _ds;
        }
        [WebMethod]
        public DataSet GetSpcLst(string _batNo)
        {
            //ȡ�쳣ԭ��
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetSpcLst(_batNo);
        }
        [WebMethod]
        public DataSet GetCust(string _cusNo)
        {
            //ȡ�ͻ����ϵ�
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetCust(_cusNo);
        }
        [WebMethod]
        public bool UserIsExist(string _usr)
        {
            //�ж��û��Ƿ����
            Users _users = new Users();
            DataSet _ds = _users.GetDataByCondition(" AND USR='" + _usr + "' AND COMPNO='" + Comp.CompNo + "'");
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                return true;
            return false;
        }
        #endregion

        #region QueryWin ����
        [WebMethod]
		public DataSet QueryWinData(string _no, string _name, string _others, string _table, string _sqlWhere)
		{
			//��ѯ����
			DataSet _ds = new DataSet();
			if (!String.IsNullOrEmpty(_no) && !String.IsNullOrEmpty(_name) && !String.IsNullOrEmpty(_table))
			{
				Query _query = new Query();
                string _sql = "";
                if (_table == "PSWD")
                    _sql = "SELECT " + _no + "," + _name + _others + " FROM SunSystem.." + _table + " WHERE 1=1 AND ISNULL(COMP_BOSS,'')<>'T' AND COMPNO='" + Comp.CompNo + "' " + _sqlWhere;
                else if (_table == "BAR_IDX")
                    _sql = "SELECT " + _no + "," + _name + _others + " FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + ".." + _table + " WHERE 1=1 " + _sqlWhere;
                else
				    _sql = "SELECT " + _no + "," + _name + _others +" FROM " + _table + " WHERE 1=1 " + _sqlWhere;
				_ds = _query.DoSQLString(_sql);
				_ds.Tables[0].TableName = _table;
			}
			return _ds;
        }
        [WebMethod]
        public DataSet QueryMOData(string _sqlWhere)
        {
            //��ѯ�����Ϣ
            DataSet _ds = new DataSet();
            Query _query = new Query();
            StringBuilder _sql = new StringBuilder();
            _sql.Append("SELECT A.MO_DD,A.MO_NO,A.MRP_NO AS PRD_NO,B.SPC,A.PRD_MARK,B.NAME AS PRD_NAME,A.WH,C.NAME AS WH_NAME,A.CUS_NO,D.NAME AS CUS_NAME,A.UNIT,B.UT AS UT,")
                .Append("A.QTY,A.QTY1,A.QTY_FIN,A.QTY_LOST,A.BAT_NO,A.ID_NO,A.USR,A.DEP,B.IDX1,INDX.NAME AS IDX_NAME,A.REM FROM MF_MO A ")
                .Append("LEFT JOIN PRDT B ON B.PRD_NO=A.MRP_NO LEFT JOIN INDX ON B.IDX1=INDX.IDX_NO LEFT JOIN MY_WH C ON C.WH=A.WH LEFT JOIN CUST D ON D.CUS_NO=A.CUS_NO WHERE 1=1 " + _sqlWhere);
            _ds = _query.DoSQLString(_sql.ToString());
            _ds.Tables[0].TableName = "MF_MO";
            return _ds;
        }
        [WebMethod]
        public DataSet QuerySAData(string _sqlWhere)
        {
            //��ѯ��������Ϣ
            DataSet _ds = new DataSet();
            Query _query = new Query();
            string _sql = "SELECT A.PS_NO,A.PS_DD,B.NAME AS CUS_NAME,A.CUS_NO FROM MF_PSS A "
                        + "LEFT JOIN CUST B ON B.CUS_NO=A.CUS_NO WHERE 1=1 " + _sqlWhere
                        + " ORDER BY A.PS_NO DESC ";
            _ds = _query.DoSQLString(_sql.ToString());
            _ds.Tables[0].TableName = "MF_PSS";
            return _ds;
        }
        [WebMethod]
        public DataSet GetDept()
        {
            //ȡ����
            DataSet _ds = new DataSet();
            Dept _dept = new Dept();
            _ds.Merge(_dept.GetData());
            return _ds;
        }
        [WebMethod]
        public DataSet GetBatNo()
        {
            //ȡ����
            Bat _bat = new Bat();
            int _totalPage = 0;
            int _totalRow = 0;
            return (DataSet)(_bat.GetData("", 1, 10000, ref _totalPage, ref _totalRow));
        }
        [WebMethod]
        public DataSet GetBatNo1(string batNo)
        {
            //ȡ����
            Bat _bat = new Bat();
            return (DataSet)(_bat.GetData(batNo));
        }
        [WebMethod]
        public DataSet GetBatNo2(string _sqlWhere)
        {
            //ȡ�������
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBatNo(_sqlWhere);
        }
        [WebMethod]
        public DataSet GetPrdtFull(string _prd_no, string _idx)
        {
            //ȡ������Ʒ
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetPrdtFull(_prd_no, _idx);
        }
        [WebMethod]
        public DataSet GetWh(string _usr, string _whNo)
        {
            //��λ����
            DataSet _ds = new DataSet();
            WH _wh = new WH();
            _ds.Merge(_wh.GetData(_usr, _whNo));
            return _ds;
        }
        [WebMethod]
        public DataSet GetBarPrtName(string typeId,string idx, string _where)
        {
            //ȡ�����ӡ����
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarPrtName(typeId, idx,_where);
        }
        [WebMethod]
        public string UpdateBarPrtName(DataSet _changeDs)
        {
            //���������ӡ��������
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.UpdateBarPrtName(_changeDs);
        }
        #endregion

        #region Ȩ���趨
        [WebMethod]
        public DataSet GetUserRightAll(string _usr)
        {
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetUserRightAll(_usr);
        }
        [WebMethod]
        public string GetUserRight(string _usr, string _pgm)
        {
            string _right = "N";
            BussBarPrint _bbp = new BussBarPrint();
            DataSet _ds = _bbp.GetUserRight(_usr, _pgm);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                _right = _ds.Tables[0].Rows[0]["INS"].ToString();
            }
            else
            {
                Users _users = new Users();
                if (_users.IsCompBoss(_usr))
                    _right = "Y";
            }
            return _right;
        }
        [WebMethod]
        public DataSet UpdateUserRight(DataSet ChangedDS,string _usr)
        {
            DataSet _dsResult = new DataSet();
            BussBarPrint _bbp = new BussBarPrint();
            Query _query = new Query();
            _query.RunSql(Comp.Conn_SunSystem, "DELETE FROM FX_PSWD WHERE ROLENO='" + _usr + "'");

            DataSet _ds = _bbp.GetUserRightAll(_usr);
            DataRow _dr;
            string _compNo = Comp.CompNo;
            foreach (DataRow dr in ChangedDS.Tables[0].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    _dr = _ds.Tables[0].NewRow();
                    _dr["COMP_BOSS"] = "";
                    _dr["USR"] = _usr;
                    _dr["COMPNO"] = _compNo;
                    _dr["ROLENO"] = _usr;
                    _dr["PGM"] = dr["PGM"];
                    _dr["INS"] = "Y";
                    _ds.Tables[0].Rows.Add(_dr);
                }
            }
            _dsResult.Merge(_bbp.UpdateUserRight(_ds, _usr));
            return _dsResult;
        }
        #endregion

        #region ����
        [WebMethod]
        public DataSet GetSAStat(string _sql)
        {
            //����ͳ����Ϣ
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "SA_STAT";
            return _ds;
        }
        #region ȡ���к�Ʒ����Ϣ
        [WebMethod]
        public DataSet GetBarQCSta(string _where, int i)
        {
            //ȡ���к�Ʒ����Ϣ
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBarQCSta(_where, i);

        }
        [WebMethod]
        public DataSet GetRptBarQCList(string _where)
        {
            //ȡ���к�Ʒ����Ϣ
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetRptBarQCList(_where);

        }
        [WebMethod]
        public DataSet GetBoxBarRpt(string _where)
        {
            //ȡ���������к�
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBarRpt(_where);
        }
        #endregion

        #region ��Ʒ��ʷ��ѯ
        [WebMethod]
        public DataSet GetPrdtHisList(string _where)
        {
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetPrdtHisList(_where);
        }
        #endregion

        #region ��Ʒ����ͳ�Ʊ���
        [WebMethod]
        public DataSet GetPrdtStat(string _where, string _whereb)
        {
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetPrdtStat(_where, _whereb);
        }
        #endregion

        #endregion

        #region ȡ���������к�
        [WebMethod]
        public DataSet GetRptSABarQClist(string _where)
        {
            //ȡ���������к�
            BussBarPrint _bc = new BussBarPrint();
            DataSet _ds = _bc.GetRptSABarQClist(_where);
            return _ds;
        }
        [WebMethod]
        public DataSet GetSABarRec(string _where)
        {
            //ȡ���������к�
            BussBarPrint _bc = new BussBarPrint();
            DataSet _ds = _bc.GetSABarRec(_where);
            return _ds;
        }
        [WebMethod]
        public string SABarQC(DataSet _dsSA)
        {
            //���������к�Ʒ��
            BussBarPrint _bc = new BussBarPrint();
            return _bc.SABarQC(_dsSA);
        }

        #endregion

        #region ���������滻
        [WebMethod]
        public DataSet GetChgBar(string _where)
        {
            //ȡ�����滻����
            BussBarPrint _bc = new BussBarPrint();
            DataSet _ds = _bc.GetChgBar(_where);
            return _ds;
        }
        [WebMethod]
        public string ChangeSABar(DataSet changeDs)
        {
            //���������滻
            BussBarPrint _bc = new BussBarPrint();
            return _bc.ChangeSABar(changeDs);
        }
        [WebMethod]
        public int GetMaxSAItm(string saNo)
        {
            //ȡ��������TF_PSS3������
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetSAMaxItm(saNo);
        }
        [WebMethod]
        public string LockSA(string _saNo, string _lockMan)
        {
            //����
            BussBarPrint _bc = new BussBarPrint();
            return _bc.LockSA(_saNo, _lockMan);
        }
        #endregion
    }
}
