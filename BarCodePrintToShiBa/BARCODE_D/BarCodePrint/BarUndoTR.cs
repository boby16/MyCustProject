using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BarUndoTR : Form
    {
        private DataSet _dsTR = new DataSet();//存储已转异常单条码
        private DataSet _dsUnTR = new DataSet();//存储未转异常单条码

        /// <summary>
        /// 转异常单撤销
        /// </summary>
        public BarUndoTR()
        {
            InitializeComponent();
        }

        private void BarUndoTR_Load(object sender, EventArgs e)
        {
            cbPrdType.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 2;

            //已转异常的条码
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _dsTR = new DataSet();
            string _where = "";
            if (txtPrdNo.Text.Trim() != "")
                _where += " and A.PRD_NO='" + txtPrdNo.Text + "'";
            if (txtBatNo.Text.Trim() != "")
                _where += " and B.BAT_NO='" + txtBatNo.Text + "'";
            if (cbPrdType.SelectedIndex == 1)
                _dsTR = _bps.GetUndoTRBar("2", _where);//半成品
            else if (cbPrdType.SelectedIndex == 2)
                _dsTR = _bps.GetUndoTRBar("3", _where);//成品
            else
                _dsTR = _bps.GetUndoTRBar("1", _where);//板材

            _frmProcess.progressBar1.Value++;

            _dsUnTR = _dsTR.Clone();

            lstTR.DataSource = _dsTR.Tables[0];
            lstTR.DisplayMember = "BAR_NO_DIS";
            lstTR.ValueMember = "BAR_NO";

            lstUnTR.DataSource = _dsUnTR.Tables[0];
            lstUnTR.DisplayMember = "BAR_NO_DIS";
            lstUnTR.ValueMember = "BAR_NO";

            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();
        }

        //反选
        private void btnTomm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnTR.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnTR.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnTR.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrMM = _dsTR.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrMM)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsTR.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _dsTR.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnTR.AcceptChanges();
                _dsTR.AcceptChanges();

                lstUnTR.DataSource = _dsUnTR.Tables[0];
                lstUnTR.DisplayMember = "BAR_NO_DIS";
                lstUnTR.ValueMember = "BAR_NO";

                lstTR.DataSource = _dsTR.Tables[0];
                lstTR.DisplayMember = "BAR_NO_DIS";
                lstTR.ValueMember = "BAR_NO";
            }
        }

        //选择
        private void btnUnmm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstTR.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstTR.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnTR.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsTR.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnTR.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _dsUnTR.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnTR.AcceptChanges();
                _dsTR.AcceptChanges();

                lstUnTR.DataSource = _dsUnTR.Tables[0];
                lstUnTR.DisplayMember = "BAR_NO_DIS";
                lstUnTR.ValueMember = "BAR_NO";

                lstTR.DataSource = _dsTR.Tables[0];
                lstTR.DisplayMember = "BAR_NO_DIS";
                lstTR.ValueMember = "BAR_NO";
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_dsUnTR != null && _dsUnTR.Tables.Count > 0)
            {
                StringBuilder _barNoLst = new StringBuilder();
                foreach (DataRow _dr in _dsUnTR.Tables[0].Rows)
                {
                    if (_barNoLst.Length > 0)
                        _barNoLst.Append(",");
                    _barNoLst.Append("'" + _dr["BAR_NO"].ToString() + "'");
                }
                if (_barNoLst.Length > 0)
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    string _result = _bps.UndoTR(_barNoLst.ToString());
                    if (String.IsNullOrEmpty(_result))
                    {
                        MessageBox.Show("转异常单撤销成功！");
                        _dsUnTR.Clear();
                        _dsUnTR.AcceptChanges();

                        lstUnTR.DataSource = _dsUnTR.Tables[0];
                        lstUnTR.DisplayMember = "BAR_NO_DIS";
                        lstUnTR.ValueMember = "BAR_NO";
                    }
                    else
                        MessageBox.Show("转异常单撤销失败！原因：" + _result);
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
            _dsTR.Clear();
            _dsTR.AcceptChanges();
            _dsUnTR.Clear();
            _dsUnTR.AcceptChanges();
        }
    }
}