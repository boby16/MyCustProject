using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BarUndoMM : Form
    {
        private DataSet _dsMM = new DataSet();//存储已缴库条码
        private DataSet _dsUnMM = new DataSet();//存储未缴库条码

        /// <summary>
        /// 缴库撤销
        /// </summary>
        public BarUndoMM()
        {
            InitializeComponent();
        }

        private void BarUndoMM_Load(object sender, EventArgs e)
        {
            cbPrdType.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 2;

            //已缴库的条码
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _dsMM = new DataSet();
            string _where = "";
            if (txtPrdNo.Text.Trim() != "")
                _where += " and A.PRD_NO='" + txtPrdNo.Text + "'";
            if (txtBatNo.Text.Trim() != "")
                _where += " and B.BAT_NO='" + txtBatNo.Text + "'";
            if (cbPrdType.SelectedIndex == 1)
                _dsMM = _bps.GetUndoMMBar("2",_where);//半成品
            else if (cbPrdType.SelectedIndex == 2)
                _dsMM = _bps.GetUndoMMBar("3", _where);//箱条码
            else
                _dsMM = _bps.GetUndoMMBar("1", _where);//板材

            _frmProcess.progressBar1.Value++;

            _dsUnMM = _dsMM.Clone();

            lstMM.DataSource = _dsMM.Tables[0];
            lstMM.DisplayMember = "BAR_NO_DIS";
            lstMM.ValueMember = "BAR_NO";
            lstMM.Refresh();

            lstUnMM.DataSource = _dsUnMM.Tables[0];
            lstUnMM.DisplayMember = "BAR_NO_DIS";
            lstUnMM.ValueMember = "BAR_NO";
            lstMM.Refresh();

            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();
        }

        //反选
        private void btnTomm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrMM = _dsMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrMM)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsMM.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _dsMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnMM.AcceptChanges();
                _dsMM.AcceptChanges();

                lstUnMM.DataSource = _dsUnMM.Tables[0];
                lstUnMM.DisplayMember = "BAR_NO_DIS";
                lstUnMM.ValueMember = "BAR_NO";

                lstMM.DataSource = _dsMM.Tables[0];
                lstMM.DisplayMember = "BAR_NO_DIS";
                lstMM.ValueMember = "BAR_NO";
            }
        }

        //选择
        private void btnUnmm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnMM.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _dsUnMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnMM.AcceptChanges();
                _dsMM.AcceptChanges();

                lstUnMM.DataSource = _dsUnMM.Tables[0];
                lstUnMM.DisplayMember = "BAR_NO_DIS";
                lstUnMM.ValueMember = "BAR_NO";

                lstMM.DataSource = _dsMM.Tables[0];
                lstMM.DisplayMember = "BAR_NO_DIS";
                lstMM.ValueMember = "BAR_NO";
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_dsUnMM != null && _dsUnMM.Tables.Count > 0)
            {
                StringBuilder _barNoLst = new StringBuilder();
                StringBuilder _boxNoList = new StringBuilder();
                foreach (DataRow _dr in _dsUnMM.Tables[0].Rows)
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
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    string _result = _bps.UndoMM(_barNoLst.ToString(), _boxNoList.ToString());
                    if (String.IsNullOrEmpty(_result))
                    {
                        MessageBox.Show("缴库撤销成功！");
                        _dsUnMM.Clear();
                        _dsUnMM.AcceptChanges();

                        lstUnMM.DataSource = _dsUnMM.Tables[0];
                        lstUnMM.DisplayMember = "BAR_NO_DIS";
                        lstUnMM.ValueMember = "BAR_NO";
                    }
                    else
                        MessageBox.Show("缴库撤销失败！原因：" + _result);
                }
            }
        }

        private void btn4Prdt_Click(object sender, EventArgs e)
        {
			QueryWin _qw = new QueryWin();
			_qw.PGM = "PRDT";//表名
            _qw.SQLWhere = "";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNo.Text = _qw.NO_RT;
            }
        }

        private void btn4BatNo_Click(object sender, EventArgs e)
        {
			QueryWin _qw = new QueryWin();
			_qw.PGM = "BAT_NO";//表名
            _qw.SQLWhere = "";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNo.Text = _qw.NO_RT;
            }
        }

        private void cbPrdType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //btnSearch_Click(null, null);
            _dsMM.Clear();
            _dsMM.AcceptChanges();
            _dsUnMM.Clear();
            _dsUnMM.AcceptChanges();
        }
    }
}