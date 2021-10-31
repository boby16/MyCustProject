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
    public partial class RPTBarQCList : Form
    {
        private DataSet _ds = null;
        public RPTBarQCList()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 6;

            StringBuilder _sqlWhere = new StringBuilder();
            if (lstPrdtKnd.SelectedIndex == 0)
            {
                _sqlWhere.Append(" AND LEN(REPLACE(A.BAR_NO,'#',''))=22 ");
            }
            else if (lstPrdtKnd.SelectedIndex == 1)
            {
                _sqlWhere.Append(" AND LEN(REPLACE(A.BAR_NO,'#',''))=24 ");
            }
            else if (lstPrdtKnd.SelectedIndex == 2)
            {
                _sqlWhere.Append(" AND LEN(REPLACE(A.BAR_NO,'#',''))>24 ");
            }
            if (txtDateQCB.Checked)
                _sqlWhere.Append(" AND E.QC_DATE>='" + txtDateQCB.Text + ":00'");
            if (txtDateQCE.Checked)
                _sqlWhere.Append(" AND E.QC_DATE<='" + txtDateQCE.Text + ":59'");
            if (txtDateSWB.Checked)
                _sqlWhere.Append(" AND E.SJ_DATE>='" + txtDateSWB.Text + ":00'");
            if (txtDateSWE.Checked)
                _sqlWhere.Append(" AND E.SJ_DATE<='" + txtDateSWE.Text + ":59'");
            if (txtPrdNoB.Text != "")
                _sqlWhere.Append(" AND A.PRD_NO>='" + txtPrdNoB.Text + "' ");
            if (txtPrdNoE.Text != "")
                _sqlWhere.Append(" AND A.PRD_NO<='" + txtPrdNoE.Text + "' ");
            if (txtBatNoB.Text != "")
                _sqlWhere.Append(" AND REPLACE(substring(A.BAR_NO,13,10),'#','')>='" + txtBatNoB.Text + "' ");
            if (txtBatNoE.Text != "")
                _sqlWhere.Append(" AND REPLACE(substring(A.BAR_NO,13,10),'#','')<='" + txtBatNoE.Text + "' ");
            if (txtWHB.Text != "")
                _sqlWhere.Append(" AND A.WH>='" + txtWHB.Text + "' ");
            if (txtWHE.Text != "")
                _sqlWhere.Append(" AND A.WH<='" + txtWHE.Text + "' ");
            if (txtSJWHB.Text != "")
                _sqlWhere.Append(" AND E.WH_SJ>='" + txtSJWHB.Text + "' ");
            if (txtSJWHE.Text != "")
                _sqlWhere.Append(" AND E.WH_SJ<='" + txtSJWHE.Text + "' ");
            if (txtBarNoB.Text != "")
                _sqlWhere.Append(" AND A.BAR_NO>='" + txtBarNoB.Text + "' ");
            if (txtBarNoE.Text != "")
                _sqlWhere.Append(" AND A.BAR_NO<='" + txtBarNoE.Text + "' ");
            if (txtBoxNoB.Text != "")
                _sqlWhere.Append(" AND A.BOX_NO>='" + txtBoxNoB.Text + "' ");
            if (txtBoxNoE.Text != "")
                _sqlWhere.Append(" AND A.BOX_NO<='" + txtBoxNoE.Text + "' ");
            if (txtIdxB.Text != "")
                _sqlWhere.Append(" AND C.IDX1>='" + txtIdxB.Text + "' ");
            if (txtIdxE.Text != "")
                _sqlWhere.Append(" AND C.IDX1<='" + txtIdxE.Text + "' ");
            if (lstStopID.SelectedIndex == 1)
            {
                //在库
                _sqlWhere.Append(" AND (A.STOP_ID is null or A.STOP_ID='F' or A.STOP_ID='') AND isnull(A.WH,'')<>'' ");
            }
            else if (lstStopID.SelectedIndex == 2)
            {
                //出库
                _sqlWhere.Append(" AND isnull(A.STOP_ID,'F')='T' ");
            }
            else if (lstStopID.SelectedIndex == 3)
            {
                //在制
                _sqlWhere.Append(" AND (A.STOP_ID is null or A.STOP_ID='F' or A.STOP_ID='') AND isnull(A.WH,'')='' ");
            }
            if (cbCust.SelectedIndex == 1)
                _sqlWhere.Append(" AND isnull(E.TO_CUST,'')='' ");
            else if (cbCust.SelectedIndex == 2)
                _sqlWhere.Append(" AND isnull(E.TO_CUST,'')<>'' ");
            else
            {
                if (txtCust.Text != "")
                    _sqlWhere.Append(" AND E.TO_CUST='" + txtCust.Text + "' ");
            }
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            DataSet _dsTemp = new DataSet();

            _frmProcess.progressBar1.Value++;

            try
            {
                _dsTemp = _bps.GetRptBarQCList(_sqlWhere.ToString());
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

            if (_dsTemp != null && _dsTemp.Tables.Count > 0 && _dsTemp.Tables.Contains("BARQC_RPT"))
            {
                _dsTemp.Tables[0].Columns["WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["WH_NAME_SJ"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["TO_CUST_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["STOP_ID"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["STOP_ID"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["QC"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["QC_REM"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["PRC_ID"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["PRC_ID"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["REM"].ReadOnly = false;
                foreach (DataRow dr in _dsTemp.Tables[0].Rows)
                {
                    dr["WH_NAME"] = Strings.StrConv(dr["WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["WH_NAME_SJ"] = Strings.StrConv(dr["WH_NAME_SJ"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["SPC_NAME"] = Strings.StrConv(dr["SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["TO_CUST_NAME"] = Strings.StrConv(dr["TO_CUST_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    
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

                    if (dr["STOP_ID"].ToString() == "T")
                        dr["STOP_ID"] = "领料/销货";
                    else if (dr["WH"].ToString() == "")
                        dr["STOP_ID"] = "在制";
                    else
                        dr["STOP_ID"] = "在库";
                    //1:强制缴库,2:报废　4:重开制令
                    if (dr["PRC_ID"].ToString() == "1")
                        dr["PRC_ID"] = "强制缴库";
                    else if (dr["PRC_ID"].ToString() == "2")
                        dr["PRC_ID"] = "报废";
                    else if (dr["PRC_ID"].ToString() == "4")
                        dr["PRC_ID"] = "重开制令";

                    dr["QC_REM"] = Strings.StrConv(dr["QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["REM"] = Strings.StrConv(dr["REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                }
            }

            _frmProcess.progressBar1.Value++;

            _ds = _dsTemp.Copy();
            _dsTemp.Dispose();

            _frmProcess.progressBar1.Value++;

            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables.Contains("BARQC_RPT"))
            {
                dataGridView1.DataMember = "BARQC_RPT";
                dataGridView1.DataSource = _ds;
                dataGridView1.Refresh();
                txtQtySum.Text = _ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                txtQtySum.Text = "0";
            }

            _frmProcess.progressBar1.Value++;

            foreach (DataGridViewColumn _dc in dataGridView1.Columns)
            {
                if (_dc.Name != "BAR_NO" && _dc.Name != "BAT_NO")
                    _dc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BAR_NO"].HeaderText = "序列号";
            dataGridView1.Columns["BOX_NO"].HeaderText = "箱号";
            dataGridView1.Columns["PRD_NO"].HeaderText = "品名";
            dataGridView1.Columns["WH"].HeaderText = "库位代号";
            dataGridView1.Columns["WH_NAME"].HeaderText = "库位";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["QTY_PER_GP"].HeaderText = "重量";
            dataGridView1.Columns["JT"].HeaderText = "机台";
            dataGridView1.Columns["QC"].HeaderText = "品质信息";
            dataGridView1.Columns["SPC_NAME"].HeaderText = "异常原因";
            dataGridView1.Columns["QC_REM"].HeaderText = "品质备注";
            dataGridView1.Columns["PRN_USR"].HeaderText = "打印人员";
            dataGridView1.Columns["PRN_DD"].HeaderText = "打印时间";
            dataGridView1.Columns["PRN_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["QC_USR"].HeaderText = "品检人员";
            dataGridView1.Columns["QC_DATE"].HeaderText = "品检时间";
            dataGridView1.Columns["QC_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["TO_CUST"].HeaderText = "意向客户代号";
            dataGridView1.Columns["TO_CUST_NAME"].HeaderText = "意向客户";
            dataGridView1.Columns["SJ_USR"].HeaderText = "送缴人员";
            dataGridView1.Columns["SJ_DATE"].HeaderText = "送缴时间";
            dataGridView1.Columns["SJ_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["MM_NO"].HeaderText = "缴库单号";
            dataGridView1.Columns["STOP_ID"].HeaderText = "状态";
            dataGridView1.Columns["REM"].HeaderText = "备注";
            dataGridView1.Columns["LH"].HeaderText = "料号";
            dataGridView1.Columns["PRT_NAME"].HeaderText = "打印品名";
            dataGridView1.Columns["PRC_ID"].HeaderText = "处理方式";
            dataGridView1.Columns["BIL_NO"].HeaderText = "制令/异常单号";
            dataGridView1.Columns["WH_SJ"].HeaderText = "送缴库位代号";
            dataGridView1.Columns["WH_NAME_SJ"].HeaderText = "送缴库位";
            if (txtBarNoB.Text != "" || txtBarNoE.Text != "")
                dataGridView1.Columns["BAT_NO"].DisplayIndex = 0;
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
                _dict["PRD_NO"] = "品名";
                _dict["WH"] = "库位代号";
                _dict["WH_NAME"] = "库位";
                _dict["IDX_NAME"] = "中类";
                _dict["SPC"] = "规格";
                _dict["BAT_NO"] = "批号";
                _dict["QTY_PER_GP"] = "重量";
                _dict["JT"] = "机台";
                _dict["PRN_DD"] = "打印时间";
                _dict["QC"] = "品质信息";
                _dict["SPC_NAME"] = "异常原因";
                _dict["QC_REM"] = "品质备注";
                _dict["QC_USR"] = "品检人员";
                _dict["QC_DATE"] = "品检时间";
                _dict["TO_CUST_NAME"] = "意向客户";
                _dict["SJ_USR"] = "送缴人员";
                _dict["SJ_DATE"] = "送缴时间";
                _dict["WH_SJ"] = "送缴库位代号";
                _dict["WH_NAME_SJ"] = "送缴库位";
                _dict["MM_NO"] = "缴库单号";
                _dict["STOP_ID"] = "状态";
                _dict["REM"] = "备注";
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

        private void RPTBarQCList_Load(object sender, EventArgs e)
        {
            lstPrdtKnd.SelectedIndex = 0;
            lstStopID.SelectedIndex = 0;
            txtDateQCB.Checked = txtDateQCE.Checked = txtDateSWB.Checked = txtDateSWE.Checked = false;
            txtDateQCB.Text = txtDateSWB.Text = System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00";
            txtDateQCE.Text = txtDateSWE.Text = System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59";
        }

        #region 表头

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

        private void qryWHB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MY_WH";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtWHB.Text = _qw.NO_RT;
            }
        }

        private void qryWHE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MY_WH";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtWHE.Text = _qw.NO_RT;
            }
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

        private void txtWHE_Enter(object sender, EventArgs e)
        {
            if (txtWHE.Text == "")
                txtWHE.Text = txtWHB.Text;
        }

        private void qryIdxB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "INDX";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtIdxB.Text = _qw.NO_RT;
            }
        }

        private void qryIdxE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "INDX";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtIdxE.Text = _qw.NO_RT;
            }
        }

        private void txtIdxE_Enter(object sender, EventArgs e)
        {
            if (txtIdxE.Text == "")
                txtIdxE.Text = txtIdxB.Text;
        }

        private void cbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCust.SelectedIndex == 0)
            {
                txtCust.ReadOnly = false;
                btnCust.Enabled = true;
            }
            else
            {
                txtCust.ReadOnly = true;
                btnCust.Enabled = false;
            }
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "CUST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtCust.Text = _qw.NO_RT;
            }
        }

        private void qrySJWHB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MY_WH";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtSJWHB.Text = _qw.NO_RT;
            }
        }

        private void qrySJWHE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MY_WH";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtSJWHE.Text = _qw.NO_RT;
            }
        }

        private void txtSJWHE_Enter(object sender, EventArgs e)
        {
            if (txtSJWHE.Text == "")
                txtSJWHE.Text = txtSJWHB.Text;
        }

        #endregion
    }
}