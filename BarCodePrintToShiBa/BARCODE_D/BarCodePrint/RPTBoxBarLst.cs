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
    public partial class RPTBoxBarLst : Form
    {
        private DataSet _ds = null;
        public RPTBoxBarLst()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 4;

            string _sqlWhere = "";
            if (txtDateB.Checked)
                _sqlWhere += " AND G.PRN_DD>='" + txtDateB.Text + " 00:00:00' ";
            if (txtDateE.Checked)
                _sqlWhere += " AND G.PRN_DD<='" + txtDateE.Text + " 23:59:59' ";
            if (txtBoxNoB.Text != "")
                _sqlWhere += " AND A.BOX_NO>='" + txtBoxNoB.Text + "' ";
            if (txtBoxNoE.Text != "")
                _sqlWhere += " AND A.BOX_NO<='" + txtBoxNoE.Text + "' ";
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;

            _frmProcess.progressBar1.Value++;

            _ds = _bps.GetBoxBarRpt(_sqlWhere);

            _frmProcess.progressBar1.Value++;

            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables.Contains("BOXBAR_RPT"))
            {
                _ds.Tables[0].Columns["WH_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["USR_PRT_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["STOP_ID"].ReadOnly = false;
                _ds.Tables[0].Columns["STOP_ID"].MaxLength = 10;
                _ds.Tables[0].Columns["FORMAT"].ReadOnly = false;
                _ds.Tables[0].Columns["FORMAT"].MaxLength = 10;
                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    dr["WH_NAME"] = Strings.StrConv(dr["WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["USR_PRT_NAME"] = Strings.StrConv(dr["USR_PRT_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    if (dr["STOP_ID"].ToString() == "T")
                        dr["STOP_ID"] = "领料/销货";
                    else if (dr["WH"].ToString() == "")
                        dr["STOP_ID"] = "在制";
                    else
                        dr["STOP_ID"] = "在库";
                    if (dr["FORMAT"].ToString() == "1")
                    {
                        dr["FORMAT"] = "标准版";
                    }
                    else if (dr["FORMAT"].ToString() == "2")
                    {
                        dr["FORMAT"] = "简约版";
                    }
                    else if (dr["FORMAT"].ToString() == "3")
                    {
                        dr["FORMAT"] = "列印编号";
                    }
                    else if (dr["FORMAT"].ToString() == "4")
                    {
                        dr["FORMAT"] = "特殊版";
                    }
                    else if (dr["FORMAT"].ToString() == "5")
                    {
                        dr["FORMAT"] = "富士康";
                    }
                    else if (dr["FORMAT"].ToString() == "6")
                    {
                        dr["FORMAT"] = "塑柏碧";
                    }
                    else if (dr["FORMAT"].ToString() == "7")
                    {
                        dr["FORMAT"] = "瀚荃";
                    }
                }
            }

            _frmProcess.progressBar1.Value++;

            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables.Contains("BOXBAR_RPT"))
            {
                dataGridView1.DataMember = "BOXBAR_RPT";
                dataGridView1.DataSource = _ds;
                dataGridView1.Refresh();
            }
            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BOX_NO"].HeaderText = "箱号";
            dataGridView1.Columns["QTY"].HeaderText = "装箱数量";
            dataGridView1.Columns["CONTENT"].HeaderText = "箱内容";
            dataGridView1.Columns["WH"].HeaderText = "库位代号";
            dataGridView1.Columns["WH_NAME"].HeaderText = "库位";
            dataGridView1.Columns["PRD_NO"].HeaderText = "品名";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["PRT_NAME"].HeaderText = "打印品名";
            dataGridView1.Columns["FORMAT"].HeaderText = "打印版式";
            dataGridView1.Columns["PRN_DD"].HeaderText = "打印时间";
            dataGridView1.Columns["USR_PRT"].HeaderText = "打印人员代号";
            dataGridView1.Columns["USR_PRT_NAME"].HeaderText = "打印人员";
            dataGridView1.Columns["STOP_ID"].HeaderText = "状态";
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dataGridView1.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
              dataGridView1.RowHeadersDefaultCellStyle.Font,
              rectangle,
              dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                FileExport _fx = new FileExport();
                _fx._sourceDataSet = _ds;
                Dictionary<string, string> _dict = new Dictionary<string, string>();
                _dict["BOX_NO"] = "箱号";
                _dict["QTY"] = "装箱数量";
                _dict["CONTENT"] = "箱内容";
                _dict["WH"] = "库位代号";
                _dict["WH_NAME"] = "库位";
                _dict["PRD_NO"] = "品名";
                _dict["SPC"] = "规格";
                _dict["IDX_NAME"] = "中类";
                _dict["BAT_NO"] = "批号";
                _dict["PRT_NAME"] = "打印品名";
                _dict["FORMAT"] = "打印版式";
                _dict["PRN_DD"] = "打印时间";
                _dict["USR_PRT"] = "打印人员代号";
                _dict["USR_PRT_NAME"] = "打印人员";
                _dict["STOP_ID"] = "状态";
                _fx._columnLst = _dict;
                _fx.ShowDialog();
            }
        }

        private void txtBoxNoE_Enter(object sender, EventArgs e)
        {
            if (txtBoxNoE.Text == "")
                txtBoxNoE.Text = txtBoxNoB.Text;
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            {
                dataGridView1.Columns[e.ColumnIndex].Frozen = true;
                dataGridView1.Columns[e.ColumnIndex].DisplayIndex = 0;
            }
            else
                dataGridView1.Columns[e.ColumnIndex].Frozen = false;
        }

        private void RPTBoxBarLst_Load(object sender, EventArgs e)
        {
            txtDateB.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txtDateE.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            txtDateB.Checked = false;
            txtDateE.Checked = false;
        }
    }
}