using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BarCodePrintTSB
{
    public partial class SASelBarCode : Form
    {
        private DataSet _dsSA = null;
        private bool _lockSa = false;
        public SASelBarCode()
        {
            InitializeComponent();
        }

        private void qrySA_Click(object sender, EventArgs e)
        {
            QuerySA _qw = new QuerySA();
            _qw.SQLWhere += " AND ISNULL(CLSID,'')<>'T' ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                this.txtSANO.Text = _qw.SaNo;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSANO.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ���������ţ�");
                return;
            }
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 5;

            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            DataSet _dsTemp = new DataSet();

            string _sqlWhere = " AND A.PS_ID='SA' AND A.PS_NO='" + txtSANO.Text + "' ";

            _frmProcess.progressBar1.Value++;

            try
            {
                _dsTemp = _bps.GetSABarRec(_sqlWhere);
                _frmProcess.progressBar1.Value++;
            }
            catch (Exception _ex)
            {
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("���ݶ�ȡ���������ԡ�ԭ��\n" + _err);
                return;
            }
            if (_dsTemp != null && _dsTemp.Tables.Contains("TF_PSS3"))
            {
                _dsTemp.Tables[0].Columns["CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["IS_CHANGED"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["IS_CHANGED"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["CHG_SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["QC_REM"].ReadOnly = false;

                _dsSA = _dsTemp.Clone();

                foreach (DataRow dr in _dsTemp.Tables["TF_PSS3"].Rows)
                {
                    dr["CUS_NAME"] = Strings.StrConv(dr["CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["WH_NAME"] = Strings.StrConv(dr["WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["SPC_NAME"] = Strings.StrConv(dr["SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["CHG_SPC_NAME"] = Strings.StrConv(dr["CHG_SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);

                    if (dr["IS_CHANGED"].ToString() == "T")
                        dr["IS_CHANGED"] = "�ѻ���";
                    else
                        dr["IS_CHANGED"] = "";

                    if (dr["QC"].ToString() == "1")
                        dr["QC"] = "�ϸ�";
                    else if (dr["QC"].ToString() == "A")
                        dr["QC"] = "A��Ʒ";
                    else if (dr["QC"].ToString() == "B")
                        dr["QC"] = "B��Ʒ";
                    else if (dr["QC"].ToString() == "2")
                        dr["QC"] = "���ϸ�";
                    else if (dr["QC"].ToString() == "N")
                        dr["QC"] = "�ڳ����";
                    dr["QC_REM"] = Strings.StrConv(dr["QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);

                    _dsSA.Tables["TF_PSS3"].ImportRow(dr);
                }
                _dsSA.Tables[0].Columns["CHK_COL"].ReadOnly = false;

                _frmProcess.progressBar1.Value++;

                dgSABar.DataMember = "TF_PSS3";
                dgSABar.DataSource = _dsSA;

                _frmProcess.progressBar1.Value++;

                if (_dsSA.Tables[0].Rows.Count > 0)
                {
                    if (_dsSA.Tables[0].Rows[0]["LOCK_MAN"].ToString() != "")
                    {
                        _lockSa = true;
                        btnOK.Enabled = false;
                        btnLock.Enabled = false;
                    }
                    else
                    {
                        _lockSa = false;
                        btnOK.Enabled = true;
                        btnLock.Enabled = true;
                    }
                }
            }
            _frmProcess.Close();
        }

        private void dgSABar_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgSABar.Columns["CHK_COL"].HeaderText = "ѡ��\n(˫���޸�)";
            dgSABar.Columns["CHK_COL"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgSABar.Columns["CHK_COL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgSABar.Columns["BAR_CODE"].HeaderText = "���к�";
            dgSABar.Columns["BOX_NO"].HeaderText = "���";
            dgSABar.Columns["BAT_NO"].HeaderText = "����";
            dgSABar.Columns["PRD_NO"].HeaderText = "Ʒ��";
            dgSABar.Columns["SPC"].HeaderText = "���";
            dgSABar.Columns["IDX_NAME"].HeaderText = "����";
            dgSABar.Columns["WH"].HeaderText = "��λ����";
            dgSABar.Columns["WH_NAME"].HeaderText = "��λ����";
            dgSABar.Columns["SPC_NAME"].HeaderText = "�쳣ԭ��";
            dgSABar.Columns["SPC_NO"].Visible = false;
            dgSABar.Columns["QC"].HeaderText = "Ʒ����Ϣ";
            dgSABar.Columns["QC_REM"].HeaderText = "Ʒ�ʱ�ע";
            dgSABar.Columns["CHG_SPC_NO"].HeaderText = "ԭ�����\n(˫��ѡ��)";
            dgSABar.Columns["CHG_SPC_NAME"].HeaderText = "�滻ԭ��";
            dgSABar.Columns["CUS_NO"].HeaderText = "�ͻ�����";
            dgSABar.Columns["CUS_NAME"].HeaderText = "�ͻ�����";
            dgSABar.Columns["IS_CHANGED"].HeaderText = "�ѻ�����";
            //dgSABar.Columns["BC_BAR_NO"].HeaderText = "�������";
            dgSABar.Columns["PS_ID"].Visible = false;
            dgSABar.Columns["PS_NO"].Visible = false;
            dgSABar.Columns["PS_ITM"].Visible = false;
            dgSABar.Columns["ITM"].Visible = false;
            dgSABar.Columns["REM"].Visible = false;
            dgSABar.Columns["PRD_MARK"].Visible = false;
            dgSABar.Columns["LOCK_MAN"].Visible = false;
            if (dgSABar.Columns.Contains("USR_QC"))
                dgSABar.Columns["USR_QC"].Visible = false;
            if (dgSABar.Columns.Contains("QC_DATE"))
                dgSABar.Columns["QC_DATE"].Visible = false;
            if (dgSABar.Columns.Contains("USR_CHG"))
                dgSABar.Columns["USR_CHG"].Visible = false;
            if (dgSABar.Columns.Contains("CHG_DATE"))
                dgSABar.Columns["CHG_DATE"].Visible = false;
            if (dgSABar.Columns.Contains("CHG_ID"))
                dgSABar.Columns["CHG_ID"].Visible = false;

            dgSABar.Columns["CHK_COL"].DisplayIndex = 0;
            dgSABar.Columns["BAR_CODE"].DisplayIndex = 1;
            dgSABar.Columns["IS_CHANGED"].DisplayIndex = 2;
            dgSABar.Columns["CHG_SPC_NO"].DisplayIndex = 3;
            dgSABar.Columns["CHG_SPC_NAME"].DisplayIndex = 4;
            dgSABar.Columns["PRD_NO"].DisplayIndex = 5;
            dgSABar.Columns["SPC"].DisplayIndex = 6;
            dgSABar.Columns["IDX_NAME"].DisplayIndex = 7;
            dgSABar.Columns["SPC_NAME"].DisplayIndex = 8;
            dgSABar.Columns["QC"].DisplayIndex = 9;
            dgSABar.Columns["QC_REM"].DisplayIndex = 10;
            //dgSABar.Columns["BC_BAR_NO"].DisplayIndex = 11;
            dgSABar.Columns["BAT_NO"].DisplayIndex = 11;
        }

        private void dgSABar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgSABar.Columns[e.ColumnIndex].Name == "CHK_COL" && e.RowIndex >= 0 && !_lockSa)
            {
                if (dgSABar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "T")
                {
                    //dgSABar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "F";
                    DataRowView _dr = (DataRowView)this.BindingContext[_dsSA, "TF_PSS3"].Current;
                    DataRow[] _drArr = _dsSA.Tables[0].Select("BOX_NO='" + _dr["BOX_NO"].ToString() + "'");
                    foreach (DataRow dr in _drArr)
                    {
                        dr["CHK_COL"] = "F";
                        dr["CHG_SPC_NO"] = "";
                        dr["CHG_SPC_NAME"] = "";
                    }
                }
                else
                {
                    //dgSABar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "T";
                    DataRowView _dr = (DataRowView)this.BindingContext[_dsSA, "TF_PSS3"].Current;
                    DataRow[] _drArr = _dsSA.Tables[0].Select("BOX_NO='" + _dr["BOX_NO"].ToString() + "'");
                    foreach (DataRow dr in _drArr)
                    {
                        dr["CHK_COL"] = "T";
                    }
                }
            }
            if (dgSABar.Columns[e.ColumnIndex].Name == "CHG_SPC_NO" && e.RowIndex >= 0 && !_lockSa)
            {
                if (dgSABar.Rows[e.RowIndex].Cells["CHK_COL"].Value.ToString() == "T")
                {
                    QueryWin _qw = new QueryWin();
                    _qw.PGM = "SPC_LST";//����
                    if (_qw.ShowDialog() == DialogResult.OK)
                    {
                        DataRowView _dr = (DataRowView)this.BindingContext[_dsSA, "TF_PSS3"].Current;
                        DataRow[] _drArr = _dsSA.Tables[0].Select("BOX_NO='" + _dr["BOX_NO"].ToString() + "'");
                        foreach (DataRow dr in _drArr)
                        {
                            dr["CHG_SPC_NO"] = _qw.NO_RT;
                            dr["CHG_SPC_NAME"] = _qw.NAME_RTN;
                        }
                    }
                }
                else
                {

                }
            }
        }

        private void dgSABar_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && !_lockSa)
            {
                dgSABar.Rows[e.RowIndex].Cells["CHK_COL"].ToolTipText = "˫��ѡ��";
                dgSABar.Rows[e.RowIndex].Cells["CHG_SPC_NO"].ToolTipText = "˫��ѡ���滻ԭ��";
            }
        }

        private void dgSABar_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgSABar.Columns[e.ColumnIndex].Name != "CHG_SPC_NO")
                dgSABar.Columns[e.ColumnIndex].ReadOnly = true;
            //else
            //{
            //    DataRowView _dr = (DataRowView)this.BindingContext[_dsSA, "TF_PSS3"].Current;
            //    if (_dr["CHK_COL"].ToString() == "T")
            //        dgSABar.Columns[e.ColumnIndex].ReadOnly = false;
            //    else
            //        dgSABar.Columns[e.ColumnIndex].ReadOnly = true;
            //}
        }

        private void dgSABar_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgSABar.Columns[e.ColumnIndex].Name == "CHG_SPC_NO")
            //{
            //    DataRowView _dr = (DataRowView)this.BindingContext[_dsSA, "TF_PSS3"].Current;
            //    DataRow[] _drArr = _dsSA.Tables[0].Select("BOX_NO='" + _dr["BOX_NO"].ToString() + "'");
            //    foreach (DataRow dr in _drArr)
            //    {
            //        dr["CHG_SPC_NO"] = dgSABar.CurrentCell.EditedFormattedValue;
            //    }
            //}
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_dsSA != null && _dsSA.Tables.Count > 0 && _dsSA.Tables[0].Rows.Count > 0)
            {
                ProcessForm _frmProcess = new ProcessForm();
                _frmProcess.Show();
                _frmProcess.progressBar1.Value = 0;
                _frmProcess.progressBar1.Maximum = 6;

                DataRow[] _drTemp = _dsSA.Tables[0].Select("CHK_COL='T'");
                DataRow[] _drTempModify = _dsSA.Tables[0].Select("CHK_COL='T'","", DataViewRowState.ModifiedOriginal);
                DataRow[] _drTemp1;
                if (!_dsSA.Tables[0].Columns.Contains("USR_QC"))
                    _dsSA.Tables[0].Columns.Add("USR_QC", System.Type.GetType("System.String"));
                if (!_dsSA.Tables[0].Columns.Contains("QC_DATE"))
                    _dsSA.Tables[0].Columns.Add("QC_DATE", System.Type.GetType("System.DateTime"));
                if (!_dsSA.Tables[0].Columns.Contains("USR_CHG"))
                    _dsSA.Tables[0].Columns.Add("USR_CHG", System.Type.GetType("System.String"));
                if (!_dsSA.Tables[0].Columns.Contains("CHG_DATE"))
                    _dsSA.Tables[0].Columns.Add("CHG_DATE", System.Type.GetType("System.DateTime"));
                if (!_dsSA.Tables[0].Columns.Contains("CHG_ID"))
                    _dsSA.Tables[0].Columns.Add("CHG_ID", System.Type.GetType("System.String"));

                DataSet _dsTemp = _dsSA.Clone();

                _frmProcess.progressBar1.Value++;

                //���������滻
                foreach (DataRow _dr in _drTemp)
                {
                    _drTemp1 = _dsSA.Tables[0].Select("BOX_NO='" + _dr["BOX_NO"].ToString() + "'");
                    foreach (DataRow _dr1 in _drTemp1)
                    {
                        _dr1["CHK_COL"] = "T";
                    }
                }

                _frmProcess.progressBar1.Value++;

                bool _hasErr = false;
                _drTemp = _dsSA.Tables[0].Select("CHK_COL='T'");
                foreach (DataRow _dr in _drTemp)
                {
                    _dr["USR_QC"] = BarRole.USR_NO;
                    _dr["QC_DATE"] = DateTime.Now;
                    if (_dr["CHG_SPC_NO"].ToString() == "")
                    {
                        MessageBox.Show("�滻ԭ����Ų���Ϊ�գ�");
                        _hasErr = true;
                        
                        _frmProcess.Close();
                        _dsSA.RejectChanges();
                        break;
                    }
                }
                if (!_hasErr)
                {
                    _dsTemp.Merge(_drTemp);

                    _frmProcess.progressBar1.Value++;

                    DataRow _drNew;
                    //���Ʒ�������CHK_COLֵ��T�޸�ΪF
                    foreach (DataRow _dr in _drTempModify)
                    {
                        //CHK_COL��ʷֵΪT�ļ�¼�������ֵΪF������Ҫ��SA_BAR_CHG����Ӧ����ɾ�����޸ĵ���ʱ��
                        _drTemp1 = _dsSA.Tables[0].Select("BOX_NO='" + _dr["BOX_NO"].ToString() + "' and CHK_COL='F'");
                        foreach (DataRow _dr1 in _drTemp1)
                        {
                            if (_dsTemp.Tables[0].Select("PS_ID='" + _dr1["PS_ID"].ToString() + "' AND PS_NO='" + _dr1["PS_NO"].ToString() + "' AND PS_ITM=" + _dr1["PS_ITM"].ToString() + " AND ITM=" + _dr1["ITM"].ToString() + "").Length <= 0)
                            {
                                _drNew = _dsTemp.Tables[0].NewRow();
                                _drNew["CHK_COL"] = _dr1["CHK_COL"];
                                _drNew["BAR_CODE"] = _dr1["BAR_CODE"];
                                _drNew["BOX_NO"] = _dr1["BOX_NO"];
                                _drNew["BAT_NO"] = _dr1["BAT_NO"];
                                _drNew["PRD_NO"] = _dr1["PRD_NO"];
                                _drNew["SPC"] = _dr1["SPC"];
                                _drNew["IDX_NAME"] = _dr1["IDX_NAME"];
                                _drNew["WH"] = _dr1["WH"];
                                _drNew["WH_NAME"] = _dr1["WH_NAME"];
                                _drNew["SPC_NAME"] = _dr1["SPC_NAME"];
                                _drNew["SPC_NO"] = _dr1["SPC_NO"];
                                _drNew["QC"] = _dr1["QC"];
                                _drNew["QC_REM"] = _dr1["QC_REM"];
                                _drNew["CHG_SPC_NO"] = _dr1["CHG_SPC_NO"];
                                _drNew["CHG_SPC_NAME"] = _dr1["CHG_SPC_NAME"];
                                _drNew["REM"] = _dr1["REM"];
                                _drNew["CUS_NO"] = _dr1["CUS_NO"];
                                _drNew["CUS_NAME"] = _dr1["CUS_NAME"];
                                _drNew["PS_ID"] = _dr1["PS_ID"];
                                _drNew["PS_NO"] = _dr1["PS_NO"];
                                _drNew["PS_ITM"] = _dr1["PS_ITM"];
                                _drNew["ITM"] = _dr1["ITM"];
                                _drNew["PRD_MARK"] = _dr1["PRD_MARK"];

                                _dsTemp.Tables[0].Rows.Add(_drNew);
                            }
                        }
                    }

                    _frmProcess.progressBar1.Value++;

                    if (_dsTemp.Tables.Count > 0 && _dsTemp.Tables[0].Rows.Count > 0)
                    {
                        onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                        _bps.UseDefaultCredentials = true;
                        try
                        {
                            _frmProcess.progressBar1.Value++;

                            string _msg = _bps.SABarQC(_dsTemp);

                            _frmProcess.progressBar1.Value++;

                            _frmProcess.Close();

                            if (_msg != "")
                            {
                                _dsSA.RejectChanges();
                                MessageBox.Show(_msg);
                            }
                            else
                            {
                                MessageBox.Show("����Ʒ��ɹ���");
                                _dsSA.Clear();
                            }
                        }
                        catch (Exception _ex)
                        {
                            _frmProcess.Close();
                            _dsSA.RejectChanges();
                            string _err = "";
                            if (_ex.Message.Length > 500)
                                _err = _ex.Message.Substring(0, 500);
                            else
                                _err = _ex.Message;
                            MessageBox.Show("��������Ʒ��ʧ�ܣ�ԭ��\n" + _err);
                        }
                    }
                }
            }
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (_dsSA != null && _dsSA.Tables.Count > 0 && _dsSA.Tables[0].Rows.Count > 0)
            {
                DataRow[] _drTemp = _dsSA.Tables[0].Select("CHK_COL='T'");
                DataRow[] _drTempModify = _dsSA.Tables[0].Select("CHK_COL='T'", "", DataViewRowState.ModifiedOriginal);
                if (_drTemp.Length > 0 || _drTempModify.Length > 0)
                {
                    MessageBox.Show("�����к���Ҫ�滻������������");
                }
                else
                {
                    if (MessageBox.Show("���ݽ����������Ƿ�ȷ��������", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                            _bps.UseDefaultCredentials = true;
                            _bps.LockSA(_dsSA.Tables[0].Rows[0]["PS_NO"].ToString(), BarRole.USR_NO);
                            MessageBox.Show("�����ɹ���");
                            _dsSA.Clear();
                            _dsSA.AcceptChanges();
                        }
                        catch(Exception _ex)
                        {
                            string _err = "";
                            if (_ex.Message.Length > 500)
                                _err = _ex.Message.Substring(0, 500);
                            else
                                _err = _ex.Message;
                            MessageBox.Show("����ʧ�ܣ�ԭ��\n" + _err);
                        }
                    }
                }
            }
        }
    }
}