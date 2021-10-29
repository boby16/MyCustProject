using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BarBoxUP : Form
    {
        DataSet _dsBarCode = new DataSet();
        DataSet _dsBarBox = new DataSet();
        DataSet _dsUnBox;

        /// <summary>
        /// 装箱
        /// </summary>
        public BarBoxUP()
        {
            InitializeComponent();
        }

        #region 成品条码
        private void txtFTBar_Leave(object sender, EventArgs e)
        {
            DataSet _dsTemp = new DataSet();
            string _barNo = txtFTBar.Text.Trim();
            if (!String.IsNullOrEmpty(_barNo))
            {
                try
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    _dsTemp = _bps.GetBoxBarRec(" AND ISNULL(A.STOP_ID,'F')<>'T' AND A.BAR_NO='" + _barNo + "' AND (ISNULL(B.QC,'')='A' OR ISNULL(B.QC,'')='B' OR (ISNULL(B.QC,'')='2' AND ISNULL(B.PRC_ID,'')='1')) AND ISNULL(A.BOX_NO,'')=''");//取得已检验合格未装箱成品条码
                    if (_dsTemp != null && _dsTemp.Tables.Count > 0 && _dsTemp.Tables[0].Rows.Count > 0)
                    {
                        {
                            if (_dsBarCode != null && _dsBarCode.Tables.Count > 0)
                            {
                                try
                                {
                                    _dsBarCode.Tables[0].ImportRow(_dsTemp.Tables[0].Rows[0]);
                                }
                                catch
                                {
                                    txtFTBar.Text = "";
                                    txtFTBar.Focus();
                                }
                            }
                            else
                                _dsBarCode.Merge(_dsTemp);

                            if (_dsUnBox.Tables.Count > 0)
                            {
                                DataRow[] _drArr = _dsUnBox.Tables[0].Select("BAR_NO='" + _barNo + "'");
                                if (_drArr.Length > 0)
                                    _drArr[0].Delete();

                                _dsUnBox.AcceptChanges();

                                DataView _dv = _dsUnBox.Tables[0].DefaultView;
                                _dv.RowFilter = "QC='A'";
                                DataTable _dt = _dv.ToTable();

                                lstUnBoxBarA.DataSource = _dt;
                                lstUnBoxBarA.DisplayMember = "BAR_NO_DIS";
                                lstUnBoxBarA.ValueMember = "BAR_NO";
                                lstUnBoxBarA.Refresh();

                                _dv = _dsUnBox.Tables[0].DefaultView;
                                _dv.RowFilter = "QC='B'";
                                _dt = _dv.ToTable();

                                lstUnBoxBarB.DataSource = _dt;
                                lstUnBoxBarB.DisplayMember = "BAR_NO_DIS";
                                lstUnBoxBarB.ValueMember = "BAR_NO";
                                lstUnBoxBarB.Refresh();

                                _dv = _dsUnBox.Tables[0].DefaultView;
                                _dv.RowFilter = "QC='2'";
                                _dt = _dv.ToTable();

                                lstUnBoxBarR.DataSource = _dt;
                                lstUnBoxBarR.DisplayMember = "BAR_NO_DIS";
                                lstUnBoxBarR.ValueMember = "BAR_NO";
                                lstUnBoxBarR.Refresh();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("序列号不存在或序列号为不合格品，请检查！");
                    }
                    if (_dsBarCode != null && _dsBarCode.Tables.Count > 0)
                    {
                        lstBarCode.DataSource = _dsBarCode.Tables[0];
                        lstBarCode.DisplayMember = "BAR_NO_DIS";
                        lstBarCode.ValueMember = "BAR_NO";
                        lstBarCode.Refresh();
                    }

                    txtFTBar.Text = "";
                    txtFTBar.Focus();

                    if (txtQty.Text != "" && _dsBarCode != null && _dsBarCode.Tables.Count > 0 && _dsBarCode.Tables[0].Rows.Count == Convert.ToInt32(txtQty.Text))
                    {
                        btnOK_Click(null, null);
                    }
                }
                catch (Exception _ex)
                {
                    string _err = "";
                    if (_ex.Message.Length > 500)
                        _err = _ex.Message.Substring(0, 500);
                    else
                        _err = _ex.Message;
                    MessageBox.Show("序列号不存在!  原因：\n" + _err);
                    return;
                }
            }
        }

        private void txtFTBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == BarRole.EndChar)
            {
                this.txtFTBar_Leave(sender, e);
            }
        }
        #endregion

        private void BarBoxUP_Load(object sender, EventArgs e)
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
            string _sql = " AND ISNULL(A.STOP_ID,'F')<>'T' AND (ISNULL(B.QC,'')='A' OR ISNULL(B.QC,'')='B' OR (ISNULL(B.QC,'')='2' AND ISNULL(B.PRC_ID,'')='1')) AND ISNULL(A.BOX_NO,'')='' AND ISNULL(A.WH,'')='' ";
            if (!String.IsNullOrEmpty(_barNo))
            {
                _sql+= " AND B.BAR_NO NOT IN " + _barNo;
            }
            _dsUnBox = _bps.GetBoxBarRec(_sql);//取得已检验合格(或不合格强制缴库)未装箱成品条码

            DataView _dv = _dsUnBox.Tables[0].DefaultView;
            _dv.RowFilter = "QC='A'";
            DataTable _dt = _dv.ToTable();

            lstUnBoxBarA.DataSource = _dt;
            lstUnBoxBarA.DisplayMember = "BAR_NO_DIS";
            lstUnBoxBarA.ValueMember = "BAR_NO";
            lstUnBoxBarA.Refresh();

            _dv = _dsUnBox.Tables[0].DefaultView;
            _dv.RowFilter = "QC='B'";
            _dt = _dv.ToTable();

            lstUnBoxBarB.DataSource = _dt;
            lstUnBoxBarB.DisplayMember = "BAR_NO_DIS";
            lstUnBoxBarB.ValueMember = "BAR_NO";
            lstUnBoxBarB.Refresh();

            _dv = _dsUnBox.Tables[0].DefaultView;
            _dv.RowFilter = "QC='2'";
            _dt = _dv.ToTable();

            lstUnBoxBarR.DataSource = _dt;
            lstUnBoxBarR.DisplayMember = "BAR_NO_DIS";
            lstUnBoxBarR.ValueMember = "BAR_NO";
            lstUnBoxBarR.Refresh();
        }

        #region 保存
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtBoxNo.Text == "")
            {
                MessageBox.Show("箱条码不能为空！");
                return;
            }
            else if (lstBarCode.Items.Count <= 0)
            {
                MessageBox.Show("成品条码不能为空！");
                return;
            }
            else if (_dsBarCode != null && _dsBarCode.Tables.Count > 0 && _dsBarCode.Tables[0].Rows.Count > 0)
            {
                if (_dsBarCode.Tables[0].Rows.Count != Convert.ToInt32(txtQty.Text))
                {
                    MessageBox.Show("装箱数量不符！");
                    return;
                }
                else
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
                            string _err = _bps.BarBoxing(txtBoxNo.Text, _barCode, BarRole.USR_NO, BarRole.DEP);//装箱
                            if (!String.IsNullOrEmpty(_err))
                            {
                                MessageBox.Show(_err);
                            }
                            else
                            {
                                txtBoxNo.Text = "";
                                txtFTBar.Text = "";
                                txtPrdNo.Text = "";
                                txtSpc.Text = "";
                                txtIdx.Text = "";
                                txtBatNo.Text = "";
                                txtQty.Text = "";
                                _dsBarCode.Clear();

                                lstBarCode.DataSource = _dsBarCode.Tables[0];
                                lstBarCode.DisplayMember = "BAR_NO_DIS";
                                lstBarCode.ValueMember = "BAR_NO";
                                lstBarCode.Refresh();

                                //MessageBox.Show("装箱成功！");
                                txtBoxNo.Focus();
                            }
                        }
                        catch(Exception _ex)
                        {
                            string _err = "";
                            if (_ex.Message.Length > 500)
                                _err = _ex.Message.Substring(0, 500);
                            else
                                _err = _ex.Message;
                            MessageBox.Show("装箱失败！原因：\n" + _err);
                        }
                    }
                }
            }
        }
        #endregion

        #region 箱条码
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
                    MessageBox.Show("箱条码不存在！");
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
                    txtFTBar.Focus();
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
                _dsBarBox = _bps.GetBoxBar(txtBoxNo.Text, "F");//取得未装箱的箱条码
                if (_dsBarBox == null || _dsBarBox.Tables.Count <= 0 || _dsBarBox.Tables[0].Rows.Count <= 0)
                {
                    txtBoxNo.Text = "";
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    txtIdx.Text = "";
                    txtBatNo.Text = "";
                    txtQty.Text = "";
                    MessageBox.Show("箱条码不存在！");
                    return;
                }
                else
                {
                    txtPrdNo.Text = _dsBarBox.Tables[0].Rows[0]["PRD_NO"].ToString();
                    txtSpc.Text = _dsBarBox.Tables[0].Rows[0]["SPC"].ToString();
                    txtIdx.Text = _dsBarBox.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    txtBatNo.Text = _dsBarBox.Tables[0].Rows[0]["BAT_NO"].ToString();
                    txtQty.Text = _dsBarBox.Tables[0].Rows[0]["QTY"].ToString();
                    txtFTBar.Focus();
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

                    DataView _dv = _dsUnBox.Tables[0].DefaultView;
                    _dv.RowFilter = "QC='A'";
                    DataTable _dt = _dv.ToTable();

                    lstUnBoxBarA.DataSource = _dt;
                    lstUnBoxBarA.DisplayMember = "BAR_NO_DIS";
                    lstUnBoxBarA.ValueMember = "BAR_NO";
                    lstUnBoxBarA.Refresh();

                    _dv = _dsUnBox.Tables[0].DefaultView;
                    _dv.RowFilter = "QC='B'";
                    _dt = _dv.ToTable();

                    lstUnBoxBarB.DataSource = _dt;
                    lstUnBoxBarB.DisplayMember = "BAR_NO_DIS";
                    lstUnBoxBarB.ValueMember = "BAR_NO";
                    lstUnBoxBarB.Refresh();

                    _dv = _dsUnBox.Tables[0].DefaultView;
                    _dv.RowFilter = "QC='2'";
                    _dt = _dv.ToTable();

                    lstUnBoxBarR.DataSource = _dt;
                    lstUnBoxBarR.DisplayMember = "BAR_NO_DIS";
                    lstUnBoxBarR.ValueMember = "BAR_NO";
                    lstUnBoxBarR.Refresh();

                }
            }
        }
    }
}