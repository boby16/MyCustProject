using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BarUndoToMM : Form
    {
        private DataSet _dsToMM = new DataSet();//�洢�ͽ�����
        private DataSet _dsUnToMM = new DataSet();//�洢���ͽ�����

        /// <summary>
        /// װ��
        /// </summary>
        public BarUndoToMM()
        {
            InitializeComponent();
        }

        private void BarUndoToMM_Load(object sender, EventArgs e)
        {
            cbPrdType.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 2;

            //���ͽ�δ�ɿ������
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            string _where = "";
            if (txtPrdNo.Text.Trim() != "")
                _where += " AND B.PRD_NO='" + txtPrdNo.Text + "'";
            if (txtBatNo.Text.Trim() != "")
                _where += " AND A.BAT_NO='" + txtBatNo.Text + "'";
            _dsToMM = new DataSet();
            if (cbPrdType.SelectedIndex == 1)
            {
                _where += " AND LEN(REPLACE(A.BAR_NO,'#',''))=24 AND ISNULL(A.STATUS, '')='SW' ";
                _dsToMM = _bps.GetBCUnToMMLst(_where);//���Ʒ
            }
            else if (cbPrdType.SelectedIndex == 2)
            {
                _dsToMM = _bps.GetBoxUnMM(_where);//������
            }
            else
            {
                _where += " AND LEN(REPLACE(A.BAR_NO,'#',''))=22 AND ISNULL(A.STATUS, '')='SW' ";
                _dsToMM = _bps.GetBCUnToMMLst(_where);//���
            }

            _frmProcess.progressBar1.Value++;

            _dsUnToMM = _dsToMM.Clone();

            lstToMM.DataSource = _dsToMM.Tables[0];
            lstToMM.DisplayMember = "BAR_NO_DIS";
            lstToMM.ValueMember = "BAR_NO";

            lstUnToMM.DataSource = _dsUnToMM.Tables[0];
            lstUnToMM.DisplayMember = "BAR_NO_DIS";
            lstUnToMM.ValueMember = "BAR_NO";

            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();
        }

        //��ѡ
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

        //ѡ��
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
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    string _result = _bps.UndoToMM(_barNoLst.ToString(), _boxNoList.ToString(), BarRole.USR_NO);
                    if (String.IsNullOrEmpty(_result))
                    {
                        MessageBox.Show("�ͽɳ����ɹ���");
                        _dsUnToMM.Clear();
                        _dsUnToMM.AcceptChanges();

                        lstUnToMM.DataSource = _dsUnToMM.Tables[0];
                        lstUnToMM.DisplayMember = "BAR_NO_DIS";
                        lstUnToMM.ValueMember = "BAR_NO";
                    }
                    else
                        MessageBox.Show("�����ͽ�ʧ�ܣ�ԭ��" + _result);
                }
            }
        }

        private void btn4Prdt_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//����
            _qw.SQLWhere = "";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNo.Text = _qw.NO_RT;
            }
        }

        private void btn4BatNo_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//����
            _qw.SQLWhere = "";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNo.Text = _qw.NO_RT;
            }
        }

        private void cbPrdType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //btnRefresh_Click(null, null);
            _dsToMM.Clear();
            _dsToMM.AcceptChanges();
            _dsUnToMM.Clear();
            _dsUnToMM.AcceptChanges();
        }
    }
}