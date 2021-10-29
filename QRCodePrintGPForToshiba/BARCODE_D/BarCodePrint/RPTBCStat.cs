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
    public partial class RPTBCStat : Form
    {
        private DataSet _ds = null;
        public RPTBCStat()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string _sqlWhere = "";
            if (lstPrdtKnd.SelectedIndex == 1)
                _sqlWhere += " AND LEN(REPLACE(A.BAR_NO,(SELECT TOP 1 TRIM_CHAR FROM BAR_ROLE),''))=22 ";
            if (lstPrdtKnd.SelectedIndex == 2)
                _sqlWhere += " AND LEN(REPLACE(A.BAR_NO,(SELECT TOP 1 TRIM_CHAR FROM BAR_ROLE),''))=24 ";
            if (lstPrdtKnd.SelectedIndex == 3)
                _sqlWhere += " AND LEN(REPLACE(A.BAR_NO,(SELECT TOP 1 TRIM_CHAR FROM BAR_ROLE),''))>24 ";
            if (txtPrdNoB.Text != "")
                _sqlWhere += " AND A.PRD_NO>='" + txtPrdNoB.Text + "' ";
            if (txtPrdNoE.Text != "")
                _sqlWhere += " AND A.PRD_NO<='" + txtPrdNoE.Text + "' ";
            if (txtBatNoB.Text != "")
                _sqlWhere += " AND A.BAT_NO>='" + txtBatNoB.Text + "' ";
            if (txtBatNoE.Text != "")
                _sqlWhere += " AND A.BAT_NO<='" + txtBatNoE.Text + "' ";
            if (txtBarNoB.Text != "")
                _sqlWhere += " AND A.BAR_NO>='" + txtBarNoB.Text + "' ";
            if (txtBarNoE.Text != "")
                _sqlWhere += " AND A.BAR_NO<='" + txtBarNoE.Text + "' ";
            if (txtBoxNoB.Text != "")
                _sqlWhere += " AND A.BOX_NO>='" + txtBoxNoB.Text + "' ";
            if (txtBoxNoE.Text != "")
                _sqlWhere += " AND A.BOX_NO<='" + txtBoxNoE.Text + "' ";
            onlineService.BarPrintServer _bps = new BarCodePrint.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _ds = _bps.GetBarQCRpt(_sqlWhere);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables.Contains("BARQC_RPT"))
            {
                _ds.Tables[0].Columns["WH_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["SPC_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["QC"].ReadOnly = false;
                _ds.Tables[0].Columns["QC_REM"].ReadOnly = false;
                _ds.Tables[0].Columns["REM"].ReadOnly = false;
                _ds.Tables[0].Columns["DF_QC"].ReadOnly = false;
                _ds.Tables[0].Columns["DF_SPC_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["DF_QC_REM"].ReadOnly = false;
                _ds.Tables[0].Columns["BC_QC"].ReadOnly = false;
                _ds.Tables[0].Columns["BC_SPC_NAME"].ReadOnly = false;
                _ds.Tables[0].Columns["BC_QC_REM"].ReadOnly = false;
                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    dr["WH_NAME"] = Strings.StrConv(dr["WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["SPC_NAME"] = Strings.StrConv(dr["SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["QC"] = Strings.StrConv(dr["QC"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["QC_REM"] = Strings.StrConv(dr["QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["REM"] = Strings.StrConv(dr["REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_QC"] = Strings.StrConv(dr["DF_QC"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_SPC_NAME"] = Strings.StrConv(dr["DF_SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_QC_REM"] = Strings.StrConv(dr["DF_QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_QC"] = Strings.StrConv(dr["BC_QC"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_SPC_NAME"] = Strings.StrConv(dr["BC_SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_QC_REM"] = Strings.StrConv(dr["BC_QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                }
                dataGridView1.DataMember = "BARQC_RPT";
                dataGridView1.DataSource = _ds;
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BAR_NO"].HeaderText = "序列号";
            dataGridView1.Columns["BOX_NO"].HeaderText = "箱号";
            dataGridView1.Columns["PRD_NO"].HeaderText = "品名";
            dataGridView1.Columns["WH"].HeaderText = "库位代号";
            dataGridView1.Columns["WH_NAME"].HeaderText = "库位";
            dataGridView1.Columns["JT"].HeaderText = "机台";
            dataGridView1.Columns["PRN_DD"].HeaderText = "打印时间";
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["SPC_NAME"].HeaderText = "异常原因";
            dataGridView1.Columns["SPC_NO"].Visible = false;
            dataGridView1.Columns["QC"].HeaderText = "品质信息";
            dataGridView1.Columns["QC_REM"].HeaderText = "品质备注";
            dataGridView1.Columns["BIL_NOLIST"].HeaderText = "历次单号";
            dataGridView1.Columns["REM"].HeaderText = "备注";
            dataGridView1.Columns["DF_BAR_NO"].HeaderText = "半成品序列号";
            dataGridView1.Columns["DF_QC"].HeaderText = "半成品品质信息";
            dataGridView1.Columns["DF_SPC_NAME"].HeaderText = "半成品异常原因";
            dataGridView1.Columns["DF_QC_REM"].HeaderText = "半成品品质备注";
            dataGridView1.Columns["BC_BAR_NO"].HeaderText = "板材序列号";
            dataGridView1.Columns["BC_QC"].HeaderText = "板材品质信息";
            dataGridView1.Columns["BC_SPC_NAME"].HeaderText = "板材异常原因";
            dataGridView1.Columns["BC_QC_REM"].HeaderText = "板材品质备注";
            dataGridView1.Columns["MM_NO"].HeaderText = "缴库单号";
            dataGridView1.Columns["CUS_NO"].Visible = false;
            dataGridView1.Columns["UPDDATE"].Visible = false;
            dataGridView1.Columns["PH_FLAG"].Visible = false;
            dataGridView1.Columns["STOP_ID"].Visible = false;
            dataGridView1.Columns["PRD_MARK"].Visible = false;
            dataGridView1.Columns["SA_FLAG"].Visible = false;
            dataGridView1.Columns["SPC_NO1"].Visible = false;
            dataGridView1.Columns["DF_SPC_NO1"].Visible = false;
            dataGridView1.Columns["BC_SPC_NO1"].Visible = false;

            dataGridView1.Columns["BAR_NO"].DisplayIndex = 0;
            dataGridView1.Columns["BOX_NO"].DisplayIndex = 1;
            dataGridView1.Columns["WH"].DisplayIndex = 2;
            dataGridView1.Columns["WH_NAME"].DisplayIndex = 3;
            dataGridView1.Columns["IDX_NAME"].DisplayIndex = 4;
            dataGridView1.Columns["SPC"].DisplayIndex =5;
            dataGridView1.Columns["BAT_NO"].DisplayIndex = 6;
            dataGridView1.Columns["QC"].DisplayIndex = 7;
            dataGridView1.Columns["SPC_NAME"].DisplayIndex = 8;
            dataGridView1.Columns["QC_REM"].DisplayIndex = 9;
            dataGridView1.Columns["PRD_NO"].DisplayIndex = 10;
            dataGridView1.Columns["JT"].DisplayIndex = 11;
            dataGridView1.Columns["PRN_DD"].DisplayIndex = 12;
            dataGridView1.Columns["BIL_NOLIST"].DisplayIndex = 13;
            dataGridView1.Columns["REM"].DisplayIndex = 14;
            dataGridView1.Columns["DF_BAR_NO"].DisplayIndex = 15;
            dataGridView1.Columns["DF_QC"].DisplayIndex = 16;
            dataGridView1.Columns["DF_SPC_NAME"].DisplayIndex = 17;
            dataGridView1.Columns["DF_QC_REM"].DisplayIndex = 18;
            dataGridView1.Columns["BC_BAR_NO"].DisplayIndex = 19;
            dataGridView1.Columns["BC_QC"].DisplayIndex = 20;
            dataGridView1.Columns["BC_SPC_NAME"].DisplayIndex = 21;
            dataGridView1.Columns["BC_QC_REM"].DisplayIndex = 22;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                FileExport _fx = new FileExport();
                _fx._sourceDataSet = _ds;
                Dictionary<string, string> _dict = new Dictionary<string, string>();
                _dict["BAR_NO"] = "序列号";
                _dict["BOX_NO"] = "箱号";
                _dict["WH"] = "库位代号";
                _dict["WH_NAME"] = "库位";
                _dict["IDX_NAME"] = "中类";
                _dict["SPC"] = "规格";
                _dict["PRD_NO"] = "品号";
                _dict["BAT_NO"] = "批号";
                _dict["QC"] = "品质信息";
                _dict["SPC_NAME"] = "异常原因";
                _dict["QC_REM"] = "品质备注";
                _dict["JT"] = "机台";
                _dict["PRN_DD"] = "打印时间";
                _dict["BIL_NOLIST"] = "历次单号";
                _dict["REM"] = "备注";
                _dict["DF_BAR_NO"] = "半成品序列号";
                _dict["DF_QC"] = "半成品品质信息";
                _dict["DF_SPC_NAME"] = "半成品异常原因";
                _dict["DF_QC_REM"] = "半成品品质备注";
                _dict["BC_BAR_NO"] = "板材序列号";
                _dict["BC_QC"] = "板材品质信息";
                _dict["BC_SPC_NAME"] = "板材异常原因";
                _dict["BC_QC_REM"] = "板材品质备注";
                _dict["MM_NO"] = "缴库单号";
                _fx._columnLst = _dict;
                _fx.ShowDialog();
            }
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

        private void qryPrdNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNoB.Text = _qw.NO_RT;
            }
        }

        private void qryPrdNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNoE.Text = _qw.NO_RT;
            }
        }

        private void qryBatNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNoB.Text = _qw.NO_RT;
            }
        }

        private void qryBatNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNoE.Text = _qw.NO_RT;
            }
        }

        private void RPTBCStat_Load(object sender, EventArgs e)
        {
            lstPrdtKnd.SelectedIndex = 0;
        }

        private void txtPrdNoE_Enter(object sender, EventArgs e)
        {
            if (txtPrdNoE.Text == "")
                txtPrdNoE.Text = txtPrdNoB.Text;
        }

        private void txtBatNoE_Enter(object sender, EventArgs e)
        {
            if (txtBatNoE.Text == "")
                txtBatNoE.Text = txtBatNoB.Text;
        }

        private void txtBarNoE_Enter(object sender, EventArgs e)
        {
            if (txtBarNoE.Text == "")
                txtBarNoE.Text = txtBarNoB.Text;
        }

        private void txtBoxNoE_Enter(object sender, EventArgs e)
        {
            if (txtBoxNoE.Text == "")
                txtBoxNoE.Text = txtBoxNoB.Text;
        }
    }
}