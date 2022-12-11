using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class FTInitAutoBox : Form
    {
        DataSet _dsBarCode = new DataSet();
        DataSet _dsBarBox = new DataSet();
        DataSet _dsUnBox;
        private string _whNameTem = "";

        /// <summary>
        /// װ��
        /// </summary>
        public FTInitAutoBox()
        {
            InitializeComponent();
        }

        private void FTInitAutoBox_Load(object sender, EventArgs e)
        {
            InitData("");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string _barCode = "";
            if (_dsBarCode != null && _dsBarCode.Tables.Count > 0 && _dsBarCode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in _dsBarCode.Tables[0].Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        if (_barCode != "")
                            _barCode += "','";
                        else
                            _barCode += "('";
                        _barCode += dr["BAR_NO"].ToString();
                    }
                }
                if (_barCode != "")
                {
                    _barCode += "')";
                }
            }
            InitData(_barCode);
        }

        private void InitData(string _barNo)
        {
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _dsUnBox = new DataSet();
            string _sql = " AND ISNULL(A.STOP_ID,'F')<>'T' AND ISNULL(B.QC,'')='N' AND ISNULL(A.BOX_NO,'')='' ";
            if (!String.IsNullOrEmpty(_barNo))
            {
                _sql+= " AND B.BAR_NO NOT IN " + _barNo;
            }
            _dsUnBox = _bps.GetBoxBarRec(_sql);

            if (_dsBarCode == null || _dsBarCode.Tables.Count <= 0)
                _dsBarCode = _dsUnBox.Clone();

            lstUnBoxBar.DataSource = _dsUnBox.Tables[0];
            lstUnBoxBar.DisplayMember = "BAR_NO_DIS";
            lstUnBoxBar.ValueMember = "BAR_NO";
            lstUnBoxBar.Refresh();
        }

        #region ����
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtBoxNo.Text == "")
            {
                MessageBox.Show("�����벻��Ϊ�գ�");
                return;
            }
            else if (lstBarCode.Items.Count <= 0)
            {
                MessageBox.Show("��Ʒ���벻��Ϊ�գ�");
                return;
            }
            else if (txtWH.Text.Trim() == "")
            {
                MessageBox.Show("��λ����Ϊ�գ�");
                return;
            }
            else
            {
                if (_dsBarCode != null && _dsBarCode.Tables.Count > 0 && _dsBarCode.Tables[0].Rows.Count > 0)
                {
                    string _barCode = "";
                    foreach (DataRow dr in _dsBarCode.Tables[0].Rows)
                    {
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            if (_barCode != "")
                                _barCode += "','";
                            else
                                _barCode += "('";
                            _barCode += dr["BAR_NO"].ToString();
                        }
                    }
                    if (_barCode != "")
                    {
                        _barCode += "')";
                        try
                        {
                            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                            _bps.UseDefaultCredentials = true;
                            string _err = _bps.InitBarBoxing(txtBoxNo.Text, _barCode,txtWH.Text, BarRole.ConvertToDBLanguage(txtRem.Text), BarRole.USR_NO, BarRole.DEP);//װ��
                            if (!String.IsNullOrEmpty(_err))
                            {
                                MessageBox.Show(_err);
                            }
                            else
                            {
                                txtBoxNo.Text = "";
                                txtPrdNo.Text = "";
                                txtSpc.Text = "";
                                txtIdx.Text = "";
                                txtBatNo.Text = "";
                                txtQty.Text = "";
                                txtWH.Text = "";
                                txtWhName.Text = "";
                                txtRem.Text = "";
                                _dsBarCode.Clear();

                                lstBarCode.DataSource = _dsBarCode.Tables[0];
                                lstBarCode.DisplayMember = "BAR_NO_DIS";
                                lstBarCode.ValueMember = "BAR_NO";
                                lstBarCode.Refresh();

                                MessageBox.Show("װ��ɹ���");
                            }
                        }
                        catch (Exception _ex)
                        {
                            string _err = "";
                            if (_ex.Message.Length > 500)
                                _err = _ex.Message.Substring(0, 500);
                            else
                                _err = _ex.Message;
                            MessageBox.Show("װ��ʧ�ܣ�ԭ��\n" + _err);
                        }
                    }
                }
            }
        }
        #endregion

        #region ������
        private void btnSearch_Click(object sender, EventArgs e)
        {
            QueryBox _box = new QueryBox();
            _box.SQLWhere = " AND ISNULL(A.STOP_ID,'F')<>'T' AND ISNULL(A.CONTENT,'')='' AND (A.VALID_DD IS NULL OR A.VALID_DD>GETDATE() OR A.VALID_DD='1899-12-30')";
            if (_box.ShowDialog() == DialogResult.OK)
            {
                _dsBarBox = _box.BoxDS;
                if (_dsBarBox == null || _dsBarBox.Tables.Count <= 0 || _dsBarBox.Tables[0].Rows.Count <= 0)
                {
                    txtBoxNo.Text = "";
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    txtIdx.Text = "";
                    txtBatNo.Text = "";
                    txtQty.Text = "";
                    MessageBox.Show("�����벻���ڣ�");
                    return;
                }
                else
                {
                    txtBoxNo.Text = _dsBarBox.Tables[0].Rows[0]["BOX_NO"].ToString();
                    txtPrdNo.Text = _dsBarBox.Tables[0].Rows[0]["PRD_NO"].ToString();
                    txtSpc.Text = _dsBarBox.Tables[0].Rows[0]["SPC"].ToString();
                    txtIdx.Text = _dsBarBox.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    txtBatNo.Text = _dsBarBox.Tables[0].Rows[0]["BAT_NO"].ToString();
                    txtQty.Text = _dsBarBox.Tables[0].Rows[0]["QTY"].ToString();
                }
            }
        }

        private void txtBoxNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == BarRole.EndChar)
            {
                this.txtBoxNo_Leave(sender, e);
            }
        }

        private void txtBoxNo_Leave(object sender, EventArgs e)
        {
            if (txtBoxNo.Text != "")
            {
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                _dsBarBox = _bps.GetBoxBar(txtBoxNo.Text, "F");//ȡ��δװ���������
                if (_dsBarBox == null || _dsBarBox.Tables.Count <= 0 || _dsBarBox.Tables[0].Rows.Count <= 0)
                {
                    txtBoxNo.Text = "";
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    txtIdx.Text = "";
                    txtBatNo.Text = "";
                    txtQty.Text = "";
                    MessageBox.Show("�����벻���ڣ�");
                    return;
                }
                else
                {
                    txtPrdNo.Text = _dsBarBox.Tables[0].Rows[0]["PRD_NO"].ToString();
                    txtSpc.Text = _dsBarBox.Tables[0].Rows[0]["SPC"].ToString();
                    txtIdx.Text = _dsBarBox.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    txtBatNo.Text = _dsBarBox.Tables[0].Rows[0]["BAT_NO"].ToString();
                    txtQty.Text = _dsBarBox.Tables[0].Rows[0]["QTY"].ToString();
                }
            }
            else
            {
                txtBoxNo.Text = "";
                txtPrdNo.Text = "";
                txtSpc.Text = "";
                txtIdx.Text = "";
                txtBatNo.Text = "";
                txtQty.Text = ""; 
            }
        }
        #endregion

        private void lstBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                DataRowView _drv;
                string _selectNo = "";
                for (int i = 0; i < lstBarCode.SelectedItems.Count; i++)
                {
                    _drv = (DataRowView)lstBarCode.SelectedItems[i];
                    if (_selectNo != "")
                        _selectNo += ",";
                    _selectNo += "'" + _drv["BAR_NO"] + "'";
                }
                if (_selectNo != "")
                {
                    DataRow[] _drArrUn = _dsUnBox.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                    DataRow[] _drArr = _dsBarCode.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                    foreach (DataRow _dr in _drArrUn)
                    {
                        _dr.Delete();
                    }
                    DataRow _drNew;
                    foreach (DataRow _dr in _drArr)
                    {
                        _drNew = _dsUnBox.Tables[0].NewRow();
                        _drNew["BAR_NO"] = _dr["BAR_NO"];
                        _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                        _drNew["BOX_NO"] = _dr["BOX_NO"];
                        _drNew["STOP_ID"] = _dr["STOP_ID"];

                        _drNew["PRD_NO"] = _dr["PRD_NO"];
                        _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                        _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                        _drNew["SPC"] = _dr["SPC"];
                        _drNew["BAT_NO"] = _dr["BAT_NO"];
                        _drNew["REM"] = _dr["REM"];
                        _drNew["QC"] = _dr["QC"];
                        _dsUnBox.Tables[0].Rows.Add(_drNew);

                        _dr.Delete();
                    }
                    _dsBarCode.AcceptChanges();
                    _dsUnBox.AcceptChanges();

                    lstBarCode.DataSource = _dsBarCode.Tables[0];
                    lstBarCode.DisplayMember = "BAR_NO_DIS";
                    lstBarCode.ValueMember = "BAR_NO";
                    lstBarCode.Refresh();

                    lstUnBoxBar.DataSource = _dsUnBox.Tables[0];
                    lstUnBoxBar.DisplayMember = "BAR_NO_DIS";
                    lstUnBoxBar.ValueMember = "BAR_NO";
                    lstUnBoxBar.Refresh();
                }
            }
        }

        private void btnWH_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MY_WH";//����
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtWH.Text = _qw.NO_RT;
                txtWhName.Text = _qw.NAME_RTN;
            }
        }

        private void txtWhName_Enter(object sender, EventArgs e)
        {
            _whNameTem = txtWhName.Text;
            txtWhName.Text = txtWH.Text;
        }

        private void txtWhName_Leave(object sender, EventArgs e)
        {
            if (txtWhName.Text != txtWH.Text)
            {
                if (txtWhName.Text != "")
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    DataSet _dsWh = _bps.GetWh(BarRole.USR_NO, txtWhName.Text);
                    if (_dsWh != null && _dsWh.Tables.Count > 0 && _dsWh.Tables[0].Rows.Count > 0)
                    {
                        txtWH.Text = _dsWh.Tables[0].Rows[0]["WH"].ToString();
                        txtWhName.Text = _dsWh.Tables[0].Rows[0]["NAME"].ToString();
                    }
                    else
                    {
                        txtWH.Text = "";
                        txtWhName.Text = "";
                        MessageBox.Show("���Ų����ڣ�");
                    }
                }
                else
                {
                    txtWH.Text = "";
                    txtWhName.Text = "";
                }
            }
            else
            {
                txtWhName.Text = _whNameTem;
            }
            _whNameTem = "";
        }

        private void btnToBox_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnBoxBar.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnBoxBar.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnBox.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrBox = _dsBarCode.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrBox)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsBarCode.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["BOX_NO"] = _dr["BOX_NO"];
                    _drNew["STOP_ID"] = _dr["STOP_ID"];

                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["REM"] = _dr["REM"];
                    _drNew["QC"] = _dr["QC"];

                    _dsBarCode.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnBox.AcceptChanges();
                _dsBarCode.AcceptChanges();

                lstUnBoxBar.DataSource = _dsUnBox.Tables[0];
                lstUnBoxBar.DisplayMember = "BAR_NO_DIS";
                lstUnBoxBar.ValueMember = "BAR_NO";

                lstBarCode.DataSource = _dsBarCode.Tables[0];
                lstBarCode.DisplayMember = "BAR_NO_DIS";
                lstBarCode.ValueMember = "BAR_NO";
            }
        }

        private void btnUnToBox_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstBarCode.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstBarCode.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnBox.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsBarCode.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnBox.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["BOX_NO"] = _dr["BOX_NO"];
                    _drNew["STOP_ID"] = _dr["STOP_ID"];

                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["REM"] = _dr["REM"];
                    _drNew["QC"] = _dr["QC"];

                    _dsUnBox.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnBox.AcceptChanges();
                _dsBarCode.AcceptChanges();

                lstUnBoxBar.DataSource = _dsUnBox.Tables[0];
                lstUnBoxBar.DisplayMember = "BAR_NO_DIS";
                lstUnBoxBar.ValueMember = "BAR_NO";

                lstBarCode.DataSource = _dsBarCode.Tables[0];
                lstBarCode.DisplayMember = "BAR_NO_DIS";
                lstBarCode.ValueMember = "BAR_NO";
            }
        }
    }
}