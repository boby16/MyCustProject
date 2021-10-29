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
    public partial class RPTSABarQCLst : Form
    {
        private DataSet _ds = null;
        public RPTSABarQCLst()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 5;

            string _sqlWhere = " AND A.PS_ID='SA' ";
            if (txtDateB.Checked)
                _sqlWhere += " AND C.CLS_DATE>='" + txtDateB.Text + "' ";
            if (txtDateE.Checked)
                _sqlWhere += " AND C.CLS_DATE<='" + txtDateE.Text + "' ";
            if (txtSANOB.Text != "")
                _sqlWhere += " AND A.PS_NO>='" + txtSANOB.Text + "' ";
            if (txtSANOE.Text != "")
                _sqlWhere += " AND A.PS_NO<='" + txtSANOE.Text + "' ";
            if (txtCusNoB.Text != "")
                _sqlWhere += " AND C.CUS_NO>='" + txtCusNoB.Text + "' ";
            if (txtCusNoE.Text != "")
                _sqlWhere += " AND C.CUS_NO>='" + txtCusNoE.Text + "' ";
            _sqlWhere += " AND C.PS_DD>='" + txtSysDateB.Text + "' AND C.PS_DD<='" + txtSysDateE.Text + "'";
            if (lstIsChg.SelectedIndex == 1)//未替换
                _sqlWhere += " AND ISNULL(L.BAR_CODE,'')<>'' AND L.BAR_CODE=A.BAR_CODE ";
            else if (lstIsChg.SelectedIndex == 2)//已替换
                _sqlWhere += " AND ISNULL(L.BAR_CODE,'')<>'' AND L.BAR_CODE<>A.BAR_CODE ";

            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            DataSet _dsTemp = new DataSet();

            _frmProcess.progressBar1.Value++;

            try
            {
                _dsTemp = _bps.GetRptSABarQClist(_sqlWhere);
            }
            catch (Exception _ex)
            {
                _frmProcess.Close();
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("数据读取出错，请重试。原因：\n" + _err);
                return;
            }

            _frmProcess.progressBar1.Value++;

            if (_dsTemp != null && _dsTemp.Tables.Count > 0 && _dsTemp.Tables.Contains("TF_PSS3"))
            {
                _dsTemp.Tables[0].Columns["CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["QC_REM"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["IS_CHANGED"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["IS_CHANGED"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["CHG_SPC_NAME"].ReadOnly = false;
                foreach (DataRow dr in _dsTemp.Tables[0].Rows)
                {
                    dr["CUS_NAME"] = Strings.StrConv(dr["CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["WH_NAME"] = Strings.StrConv(dr["WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["SPC_NAME"] = Strings.StrConv(dr["SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["CHG_SPC_NAME"] = Strings.StrConv(dr["CHG_SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
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
                    if (dr["CHG_BAR_CODE"].ToString() != "")
                    {
                        if (dr["CHG_BAR_CODE"].ToString() == dr["BAR_CODE"].ToString())
                            dr["IS_CHANGED"] = "未替换";
                        else
                            dr["IS_CHANGED"] = "已替换";
                    }
                }
            }

            _frmProcess.progressBar1.Value++;

            _ds = _dsTemp.Copy();
            _dsTemp.Dispose();

            _frmProcess.progressBar1.Value++;

            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables.Contains("TF_PSS3"))
            {
                dataGridView1.DataMember = "TF_PSS3";
                dataGridView1.DataSource = _ds;
                dataGridView1.Refresh();
            }

            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["CLS_DATE"].HeaderText = "销货时间";
            dataGridView1.Columns["PS_NO"].HeaderText = "销货单号";
            dataGridView1.Columns["CUS_NAME"].HeaderText = "销货客户";
            dataGridView1.Columns["WH_NAME"].HeaderText = "销货库位";
            dataGridView1.Columns["PRD_NO"].HeaderText = "品名";
            dataGridView1.Columns["BAR_CODE"].HeaderText = "序列号";
            dataGridView1.Columns["BOX_NO"].HeaderText = "箱号";
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["QTY"].HeaderText = "销货数量";
            dataGridView1.Columns["QTY1"].HeaderText = "销货副数量";
            dataGridView1.Columns["QTY"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["QTY1"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["QC"].HeaderText = "品质信息";
            dataGridView1.Columns["SPC_NAME"].HeaderText = "不合格原因";
            dataGridView1.Columns["QC_REM"].HeaderText = "品质备注";
            dataGridView1.Columns["CHG_BAR_CODE"].HeaderText = "替换条码";
            dataGridView1.Columns["CHG_SPC_NAME"].HeaderText = "替换原因";
            dataGridView1.Columns["USR_QC"].HeaderText = "出货品检人员";
            dataGridView1.Columns["QC_DATE"].HeaderText = "出货品检时间";
            dataGridView1.Columns["QC_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["IS_CHANGED"].HeaderText = "替换否";
            dataGridView1.Columns["USR_CHG"].HeaderText = "替换人员";
            dataGridView1.Columns["CHG_DATE"].HeaderText = "替换时间";
            dataGridView1.Columns["CHG_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

            dataGridView1.Columns["SB_DD"].HeaderText = "退货日期";
            dataGridView1.Columns["SB_NO"].HeaderText = "退货单号";
            dataGridView1.Columns["SB_WH_NAME"].HeaderText = "退货库位";
            dataGridView1.Columns["SB_CUS_NAME"].HeaderText = "退货客户";
            dataGridView1.Columns["SB_SPC"].HeaderText = "退货规格";
            dataGridView1.Columns["SB_IDX_NAME"].HeaderText = "退货中类";
            dataGridView1.Columns["SB_QTY"].HeaderText = "退货数量";
            dataGridView1.Columns["SB_QTY1"].HeaderText = "退货副数量";
            dataGridView1.Columns["SB_QTY"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["SB_QTY1"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["SB_REM"].HeaderText = "退货备注";

            dataGridView1.Columns["PS_ID"].Visible = false;
            dataGridView1.Columns["PS_DD"].Visible = false;
            dataGridView1.Columns["PS_ITM"].Visible = false;
            dataGridView1.Columns["ITM"].Visible = false;
            dataGridView1.Columns["SB_ID"].Visible = false;
            dataGridView1.Columns["SB_ITM"].Visible = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                FileExport _fx = new FileExport();
                _fx._sourceDataSet = _ds;
                Dictionary<string, string> _dict = new Dictionary<string, string>();
                _dict["CLS_DATE"] = "销货时间";
                _dict["PS_NO"] = "销货单号";
                _dict["CUS_NAME"] = "销货客户";
                _dict["WH_NAME"] = "销货库位";
                _dict["PRD_NO"] = "品名";
                _dict["BAR_CODE"] = "序列号";
                _dict["BOX_NO"] = "箱号";
                _dict["IDX_NAME"] = "中类";
                _dict["SPC"] = "规格";
                _dict["BAT_NO"] = "批号";
                _dict["QTY"] = "销货数量";
                _dict["QTY1"] = "销货副数量";
                _dict["QC"] = "品质信息";
                _dict["SPC_NAME"] = "不合格原因";
                _dict["QC_REM"] = "品质备注";
                _dict["CHG_BAR_CODE"] = "替换条码";
                _dict["CHG_SPC_NAME"] = "替换原因";
                _dict["USR_QC"] = "出货品检人员";
                _dict["QC_DATE"] = "出货品检时间";
                _dict["USR_CHG"] = "替换人员";
                _dict["CHG_DATE"] = "替换时间";
                _dict["IS_CHANGED"] = "替换否";

                _dict["SB_DD"] = "退货日期";
                _dict["SB_NO"] = "退货单号";
                _dict["SB_WH_NAME"] = "退货库位";
                _dict["SB_CUS_NAME"] = "退货客户";
                _dict["SB_SPC"] = "退货规格";
                _dict["SB_IDX_NAME"] = "退货中类";
                _dict["SB_QTY"] = "退货数量";
                _dict["SB_QTY1"] = "退货副数量";
                _dict["SB_REM"] = "退货备注";
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

        private void qryCusNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "CUST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtCusNoB.Text = _qw.NO_RT;
            }
        }

        private void qryCusNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "CUST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtCusNoE.Text = _qw.NO_RT;
            }
        }

        private void txtCusNoE_Enter(object sender, EventArgs e)
        {
            if (txtCusNoE.Text == "")
                txtCusNoE.Text = txtCusNoB.Text;
        }

        private void txtSANOE_Enter(object sender, EventArgs e)
        {
            if (txtSANOE.Text == "")
                txtSANOE.Text = txtSANOB.Text;
        }

        private void qrySANOB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MF_PSS";//表名
            _qw.SQLWhere += " AND MF_PSS.PS_ID='SA' ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtSANOB.Text = _qw.NO_RT;
            }
        }

        private void qrySANOE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MF_PSS";//表名
            _qw.SQLWhere += " AND MF_PSS.PS_ID='SA' ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtSANOE.Text = _qw.NO_RT;
            }
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

        private void RPTSABarQCLst_Load(object sender, EventArgs e)
        {
            txtDateB.Checked = false;
            txtDateE.Checked = false;
            lstIsChg.SelectedIndex = 0;
            txtDateB.Text = System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00";
            txtDateE.Text = System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59";
        }
    }
}