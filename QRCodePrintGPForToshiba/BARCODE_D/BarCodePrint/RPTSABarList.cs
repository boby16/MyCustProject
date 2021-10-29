using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BarCodePrint
{
    public partial class RPTSABarList : Form
    {
        private DataSet _ds;
        public RPTSABarList()
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
                ProcessForm _frmProcess = new ProcessForm();
                _frmProcess.Show();
                _frmProcess.progressBar1.Value = 0;
                _frmProcess.progressBar1.Maximum = 3;

                StringBuilder _barNoStr = new StringBuilder();
                StringBuilder _boxNoStr = new StringBuilder();

                for (int i = 0; i < lstBarCode.Items.Count; i++)
                {
                    if (lstBarCode.Items[i].ToString() != "")
                    {
                        if (lstBarCode.Items[i].ToString().IndexOf(BarRole.BoxFlag) == 0)
                        {
                            if (_boxNoStr.Length > 0)
                                _boxNoStr.Append(",");
                            else
                                _boxNoStr.Append("(");
                            _boxNoStr.Append("'").Append(lstBarCode.Items[i].ToString()).Append("'");
                        }
                        else
                        {
                            if (_barNoStr.Length > 0)
                                _barNoStr.Append(",");
                            else
                                _barNoStr.Append("(");
                            _barNoStr.Append("'").Append(lstBarCode.Items[i].ToString()).Append("'");
                        }
                    }
                }
                if (_barNoStr.Length > 0)
                    _barNoStr.Append(")");
                if (_boxNoStr.Length > 0)
                    _boxNoStr.Append(")");

                onlineService.BarPrintServer _bps = new BarCodePrint.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                string _where = " AND A.PS_ID='SA' ";
                if (_barNoStr.Length > 0)
                    _where +=  " AND (A.BAR_CODE IN " + _barNoStr.ToString();
                if (_boxNoStr.Length > 0)
                {
                    if (_barNoStr.Length > 0)
                        _where += " OR ";
                    else
                        _where += " AND ";
                    _where += " A.BOX_NO IN " + _boxNoStr.ToString();
                    if (_barNoStr.Length > 0)
                        _where += ")";
                }
                else if (_barNoStr.Length > 0)
                    _where += ")";

                _frmProcess.progressBar1.Value++;

                try
                {
                    _ds = _bps.GetSABarRec(_where);
                }
                catch (Exception _ex)
                {
                    _frmProcess.Close();
                    string _err = "";
                    if (_ex.Message.Length > 200)
                        _err = _ex.Message.Substring(0, 200);
                    else
                        _err = _ex.Message;
                    MessageBox.Show("查询出错，请重试。原因：\n" + _err);
                    return;
                }

                _frmProcess.progressBar1.Value++;

                _ds.Tables[0].Columns["CUS_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["WH_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["SPC_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["QC"].ReadOnly = false;
                _ds.Tables[0].Columns["QC"].MaxLength = 10;
                _ds.Tables[0].Columns["QC_REM"].ReadOnly = false;
                _ds.Tables[0].Columns["REM"].ReadOnly = false;

                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    dr["CUS_NAME"] = Strings.StrConv(dr["CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["WH_NAME"] = Strings.StrConv(dr["WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["SPC_NAME"] = Strings.StrConv(dr["SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
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

                    dr["QC_REM"] = Strings.StrConv(dr["QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["REM"] = Strings.StrConv(dr["REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                }

                dataGridView1.DataMember = "TF_PSS3";
                dataGridView1.DataSource = _ds;

                _frmProcess.progressBar1.Value++;
                _frmProcess.Close();
            }
        }

        private void RPTSABarList_Load(object sender, EventArgs e)
        {
        }

        private void btnBarFileOut_Click(object sender, EventArgs e)
        {
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                BarRole _barRole = new BarRole();
                bool _flag = true;
                try
                {
                    _flag = _barRole.GetBarDoc();
                }
                catch
                {
                    _flag = false;
                }
                if (_flag)
                {
                    FileOut _fileOut = new FileOut();
                    _fileOut.BilDS = _ds;
                    _fileOut.ShowDialog();
                }
                else
                {
                    MessageBox.Show("没有设定序列号转入格式！");
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["PS_ID"].Visible = false;
            dataGridView1.Columns["PS_NO"].HeaderText = "销货单号";
            dataGridView1.Columns["PS_ITM"].Visible = false;
            dataGridView1.Columns["ITM"].HeaderText = "项次";
            dataGridView1.Columns["PRD_NO"].HeaderText = "品名";
            dataGridView1.Columns["PRD_MARK"].Visible = false;
            dataGridView1.Columns["BAR_CODE"].HeaderText = "序列号";
            dataGridView1.Columns["BOX_NO"].HeaderText = "箱号";
            dataGridView1.Columns["CUS_NO"].HeaderText = "客户代号";
            dataGridView1.Columns["CUS_NAME"].HeaderText = "客户";
            dataGridView1.Columns["WH"].HeaderText = "库位代号";
            dataGridView1.Columns["WH_NAME"].HeaderText = "库位";
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["QC"].HeaderText = "品质信息";
            dataGridView1.Columns["SPC_NO"].Visible = false;
            dataGridView1.Columns["CHK_COL"].Visible = false;
            dataGridView1.Columns["SPC_NAME"].HeaderText = "异常原因";
            dataGridView1.Columns["QC_REM"].HeaderText = "品质备注";
            dataGridView1.Columns["REM"].HeaderText = "备注";


            dataGridView1.Columns["PS_NO"].DisplayIndex = 0;
            dataGridView1.Columns["ITM"].DisplayIndex = 1;
            dataGridView1.Columns["CUS_NAME"].DisplayIndex = 2;
            dataGridView1.Columns["WH_NAME"].DisplayIndex = 3;
            dataGridView1.Columns["BAR_CODE"].DisplayIndex = 4;
            dataGridView1.Columns["BOX_NO"].DisplayIndex = 5;
            dataGridView1.Columns["PRD_NO"].DisplayIndex = 6;
            dataGridView1.Columns["IDX_NAME"].DisplayIndex = 7;
            dataGridView1.Columns["SPC"].DisplayIndex = 8;
            dataGridView1.Columns["BAT_NO"].DisplayIndex = 9;
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
    }
}