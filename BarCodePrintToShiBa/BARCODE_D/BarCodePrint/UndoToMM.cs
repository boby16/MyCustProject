using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrint
{
    public partial class UndoToMM : Form
    {
        private DataSet _dsToMM = new DataSet();//存储送缴条码
        private DataSet _dsUnToMM = new DataSet();//存储待送缴条码
        private DataSet _dsMo = new DataSet();

        /// <summary>
        /// 装箱
        /// </summary>
        public UndoToMM()
        {
            InitializeComponent();
        }

        private void UndoToMM_Load(object sender, EventArgs e)
        {
            cbPrdType.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //已送缴未缴库的条码
            onlineService.BarPrintServer _bps = new BarCodePrint.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _dsToMM = new DataSet();
            if (cbPrdType.SelectedIndex == 1)
                _dsToMM = _bps.GetBCUnToMMLst(" AND LEN(REPLACE(A.BAR_NO,'#',''))=24 AND ISNULL(A.STATUS, '')='SW' ");//半成品
            else if (cbPrdType.SelectedIndex == 2)
                _dsToMM = _bps.GetBoxUnMM("");
            else
                _dsToMM = _bps.GetBCUnToMMLst(" AND LEN(REPLACE(A.BAR_NO,'#',''))=22 AND ISNULL(A.STATUS, '')='SW' ");//板材

            _dsUnToMM = _dsToMM.Clone();

            lstToMM.DataSource = _dsToMM.Tables[0];
            lstToMM.DisplayMember = "BAR_NO_DIS";
            lstToMM.ValueMember = "BAR_NO";
            lstToMM.Refresh();
        }

        //反选
        private void btnTomm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnToMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnToMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnToMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrMM = _dsToMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrMM)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsToMM.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["REM"] = _dr["REM"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["WH"] = _dr["WH"];

                    _dsToMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnToMM.AcceptChanges();
                _dsToMM.AcceptChanges();

                lstUnToMM.DataSource = _dsUnToMM.Tables[0];
                lstUnToMM.DisplayMember = "BAR_NO_DIS";
                lstUnToMM.ValueMember = "BAR_NO";

                lstToMM.DataSource = _dsToMM.Tables[0];
                lstToMM.DisplayMember = "BAR_NO_DIS";
                lstToMM.ValueMember = "BAR_NO";
            }
        }

        //选择
        private void btnUnmm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstToMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstToMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnToMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsToMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnToMM.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = "QC";
                    _drNew["BIL_ID"] = DBNull.Value;
                    _drNew["BIL_NO"] = DBNull.Value;
                    _drNew["REM"] = _dr["REM"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["WH"] = _dr["WH"];

                    _dsUnToMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnToMM.AcceptChanges();
                _dsToMM.AcceptChanges();

                lstUnToMM.DataSource = _dsUnToMM.Tables[0];
                lstUnToMM.DisplayMember = "BAR_NO_DIS";
                lstUnToMM.ValueMember = "BAR_NO";

                lstToMM.DataSource = _dsToMM.Tables[0];
                lstToMM.DisplayMember = "BAR_NO_DIS";
                lstToMM.ValueMember = "BAR_NO";
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_dsUnToMM != null && _dsUnToMM.Tables.Count > 0)
            {
                StringBuilder _barNoLst = new StringBuilder();
                StringBuilder _boxNoList = new StringBuilder();
                foreach (DataRow _dr in _dsUnToMM.Tables[0].Rows)
                {
                    if (_barNoLst.Length > 0)
                        _barNoLst.Append(",");
                    _barNoLst.Append("'" + _dr["BAR_NO"].ToString() + "'");
                    if (_dr["BAR_NO"].ToString().Length == 13)
                    {
                        if (_boxNoList.Length > 0)
                            _boxNoList.Append(",");
                        _boxNoList.Append("'" + _dr["BAR_NO"].ToString() + "'");
                    }
                }
                if (_barNoLst.Length > 0)
                {
                    onlineService.BarPrintServer _bps = new BarCodePrint.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    string _result = _bps.UndoToMM(_barNoLst.ToString(), _boxNoList.ToString(), BarRole.USR_NO);
                    if (String.IsNullOrEmpty(_result))
                    {
                        MessageBox.Show("送缴撤销成功！");
                        _dsUnToMM.Clear();
                        _dsUnToMM.AcceptChanges();

                        lstUnToMM.DataSource = _dsUnToMM.Tables[0];
                        lstUnToMM.DisplayMember = "BAR_NO_DIS";
                        lstUnToMM.ValueMember = "BAR_NO";
                    }
                    else
                        MessageBox.Show("撤销送缴失败！原因：" + _result);
                }
            }
        }
    }
}