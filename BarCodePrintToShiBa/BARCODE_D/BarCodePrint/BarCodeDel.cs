using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BarCodeDel : Form
    {
        private DataSet _ds;
        public BarCodeDel()
        {
            InitializeComponent();
        }

        private void txtBarNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == BarRole.EndChar)
            {
                txtBarNo_Leave(sender, e);
            }
        }

        private void txtBarNo_Leave(object sender, EventArgs e)
        {
            string _barNo = txtBarNo.Text.Trim();
            if (!String.IsNullOrEmpty(_barNo))
            {
                string _selectNo = "";
                for (int i = 0; i < this.lstBarCode.Items.Count; i++)
                {
                    if (_selectNo != "")
                        _selectNo += ";";
                    _selectNo += lstBarCode.Items[i].ToString();
                }
                if (_selectNo.IndexOf(_barNo) < 0)
                    lstBarCode.Items.Insert(0, _barNo);

                txtBarNo.Text = "";
                txtBarNo.Focus();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (lstBarCode.Items.Count > 0)
            {
                StringBuilder _barNoStr = new StringBuilder();
                StringBuilder _boxNoStr = new StringBuilder();

                for (int i = 0; i < lstBarCode.Items.Count; i++)
                {
                    if (lstBarCode.Items[i].ToString() != "")
                    {
                        if (_barNoStr.Length > 0)
                            _barNoStr.Append(",");
                        else
                            _barNoStr.Append("(");
                        _barNoStr.Append("'").Append(lstBarCode.Items[i].ToString()).Append("'");
                    }
                }
                if (_barNoStr.Length > 0)
                    _barNoStr.Append(")");

                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                string _where = "";
                if (_barNoStr.Length > 0)
                    _where += " AND A.BAR_NO IN " + _barNoStr.ToString() + " AND ISNULL(A.WH,'')=''  AND ISNULL(A.BOX_NO,'')='' AND (ISNULL(E.BAR_NO,'')='' OR ISNULL(E.QC,'')='')";
                else
                    _where += " AND 1<>1 ";

                _ds = _bps.GetBarRecCanDel(_where);

                dataGridView1.DataMember = "BAR_REC";
                dataGridView1.DataSource = _ds;
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BAR_NO"].HeaderText = "序列号";
            dataGridView1.Columns["BOX_NO"].Visible = false;
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["PRD_NO"].HeaderText = "品号";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["USR_NAME"].HeaderText = "打印人员";
            dataGridView1.Columns["SYS_DATE"].HeaderText = "打印日期";
            dataGridView1.Columns["STOP_ID"].HeaderText = "停用否";
        }

        private void lstBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            //删除
            if (e.KeyValue == 46)
            {
                for (int i = lstBarCode.SelectedItems.Count-1; i >= 0; i--)
                {
                    this.lstBarCode.Items.Remove(lstBarCode.SelectedItems[i]);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_ds.Tables[0].Rows.Count > 0)
            {
                DialogResult _result;
                _result = MessageBox.Show("确认要删除序列号？", "提示", MessageBoxButtons.YesNo);
                if (_result == DialogResult.Yes)
                {
                    string _barCode = "";
                    foreach (DataRow _dr in _ds.Tables[0].Rows)
                    {
                        if (_barCode != "")
                            _barCode += "','";
                        _barCode += _dr["BAR_NO"].ToString();
                    }
                    string _where = "";

                    if (_barCode != "")
                        _where += " AND BAR_NO IN ('" + _barCode + "')";
                    else
                        _where += " AND 1<>1 ";
                    try
                    {
                        onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                        _bps.UseDefaultCredentials = true;
                        _bps.DeleteBarCode(_where);
                        MessageBox.Show("删除成功！");

                        _ds.Clear();
                        _ds.AcceptChanges();
                        dataGridView1.DataMember = "BAR_REC";
                        dataGridView1.DataSource = _ds;
                        dataGridView1.Refresh();
                        lstBarCode.Items.Clear();
                    }
                    catch (Exception _ex)
                    {
                        string _err = "";
                        if (_ex.Message.Length > 500)
                            _err = _ex.Message.Substring(0, 500);
                        else
                            _err = _ex.Message;
                        MessageBox.Show("删除失败！原因：\n" + _err);
                    }
                }
            }
        }
    }
}