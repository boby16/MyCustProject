using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class SBUndoBox : Form
    {
        DataSet _dsBarCode = new DataSet();

        DataSet _dsBarBox = new DataSet();

        /// <summary>
        /// 退货拆箱
        /// </summary>
        public SBUndoBox()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            QueryBox _box = new QueryBox();
            _box.SQLWhere = " AND (A.VALID_DD IS NULL OR A.VALID_DD>GETDATE() OR A.VALID_DD='1899-12-30') AND ISNULL(A.CONTENT,'')<>'' ";

            if (_box.ShowDialog() == DialogResult.OK)
            {
                _dsBarBox = _box.BoxDS;
                if (_dsBarBox != null && _dsBarBox.Tables.Count > 0 && _dsBarBox.Tables[0].Rows.Count > 0)
                {
                    txtBoxNo.Text = _dsBarBox.Tables[0].Rows[0]["BOX_NO"].ToString();
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    _dsBarCode = _bps.GetBoxBarRec(" AND A.BOX_NO='" + txtBoxNo.Text + "'");
                    if (_dsBarCode != null && _dsBarCode.Tables.Count > 0 && _dsBarCode.Tables[0].Rows.Count > 0)
                    {
                        _dsBarCode.Tables[0].Columns["QC"].ReadOnly = false;
                        _dsBarCode.Tables[0].Columns["QC"].MaxLength = 10;
                        foreach (DataRow dr in _dsBarCode.Tables[0].Rows)
                        {
                            if (dr["QC"].ToString() == "1")
                                dr["QC"] = "合格";
                            else if (dr["QC"].ToString() == "A")
                                dr["QC"] = "A级品";
                            else if (dr["QC"].ToString() == "B")
                                dr["QC"] = "B级品";
                            else if (dr["QC"].ToString() == "2")
                                dr["QC"] = "不合格";
                            else if (dr["QC"].ToString() == "N")
                                dr["QC"] = "期初库存";
                        }
                        dataGridView1.DataMember = "BAR_REC";
                        dataGridView1.DataSource = _dsBarCode;
                    }
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
                dataGridView1.Columns["BAR_NO"].Visible = false;
                dataGridView1.Columns["BAR_NO_DIS"].HeaderText = "序列号";
                dataGridView1.Columns["BOX_NO"].Visible = false;
                dataGridView1.Columns["STOP_ID"].Visible = false;
                dataGridView1.Columns["PRD_NO"].HeaderText = "货品代号";
                dataGridView1.Columns["PRD_NAME"].Visible = false;
                dataGridView1.Columns["WH"].Visible = false; ;
                dataGridView1.Columns["WH_NAME"].Visible = false;
                dataGridView1.Columns["SPC"].HeaderText = "规格";
                dataGridView1.Columns["PRD_MARK"].Visible = false;
                dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
                dataGridView1.Columns["QC"].HeaderText = "品质信息";
                dataGridView1.Columns["PRC_ID"].Visible = false;
                dataGridView1.Columns["REM"].HeaderText = "备注";
        }

        private void btnUndoBox_Click(object sender, EventArgs e)
        {
            if (txtBoxNo.Text != "" && dataGridView1.RowCount > 0)
            {
                if (MessageBox.Show("确定进行拆箱？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                        _bps.UseDefaultCredentials = true;
                        _bps.UnDoBox(txtBoxNo.Text, BarRole.USR_NO, "3");
                        MessageBox.Show("拆箱成功！");
                        _dsBarCode.Clear();

                        dataGridView1.DataMember = "BAR_REC";
                        dataGridView1.DataSource = _dsBarCode;
                        dataGridView1.Refresh();
                    }
                    catch (Exception _ex)
                    {
                        string _err = "";
                        if (_ex.Message.Length > 500)
                            _err = _ex.Message.Substring(0, 500);
                        else
                            _err = _ex.Message;
                        MessageBox.Show("拆箱失败！原因：\n" + _err);
                    }
                }
            }
            else
            {
                MessageBox.Show("箱条码或箱明细不存在！");
            }
        }

        private void txtBoxNo_Leave(object sender, EventArgs e)
        {
            if (txtBoxNo.Text != "")
            {
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                _dsBarCode = _bps.GetBoxBarRec(" AND A.BOX_NO='" + txtBoxNo.Text + "'");
                if (_dsBarCode != null && _dsBarCode.Tables.Count > 0 && _dsBarCode.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataMember = "BAR_REC";
                    dataGridView1.DataSource = _dsBarCode;
                    dataGridView1.Refresh();
                }
                else
                {
                    MessageBox.Show("箱条码或箱明细不存在！");

                    _dsBarCode.Clear();
                    dataGridView1.DataMember = "BAR_REC";
                    dataGridView1.DataSource = _dsBarCode;
                    dataGridView1.Refresh();
                    txtBoxNo.Text = "";
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
    }
}