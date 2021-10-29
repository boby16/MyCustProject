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
    public partial class RPTPrdtHisList : Form
    {
        private DataSet _ds = null;
        public RPTPrdtHisList()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 6;

            string _sqlWhere = "";
            if (lstPrdtKnd.SelectedIndex == 1)
                _sqlWhere += " AND LEN(REPLACE(BR.BAR_NO,'#',''))=22 ";
            if (lstPrdtKnd.SelectedIndex == 2)
                _sqlWhere += " AND LEN(REPLACE(BR.BAR_NO,'#',''))=24 ";
            if (lstPrdtKnd.SelectedIndex == 3)
                _sqlWhere += " AND LEN(REPLACE(BR.BAR_NO,'#',''))>24 ";

            if (txtBatNoB.Text != "")
                _sqlWhere += " AND REPLACE(substring(BR.BAR_NO,13,10),'#','')>='" + txtBatNoB.Text + "' ";
            if (txtBatNoE.Text != "")
                _sqlWhere += " AND REPLACE(substring(BR.BAR_NO,13,10),'#','')<='" + txtBatNoE.Text + "' ";
            if (txtBarNoB.Text != "")
                _sqlWhere += " AND BR.BAR_NO>='" + txtBarNoB.Text + "' ";
            if (txtBarNoE.Text != "")
                _sqlWhere += " AND BR.BAR_NO<='" + txtBarNoE.Text + "' ";
            if (txtBoxNoB.Text != "")
                _sqlWhere += " AND BR.BOX_NO>='" + txtBoxNoB.Text + "' ";
            if (txtBoxNoE.Text != "")
                _sqlWhere += " AND BR.BOX_NO<='" + txtBoxNoE.Text + "' ";

            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            DataSet _dsTemp = new DataSet();

            _frmProcess.progressBar1.Value++;

            try
            {
                _dsTemp = _bps.GetPrdtHisList(_sqlWhere);
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

            if (_dsTemp != null && _dsTemp.Tables.Count > 0 && _dsTemp.Tables.Contains("BAR_QC"))
            {
                _dsTemp.Tables[0].Columns["USR_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["USR_QC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["USR_BOX_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["USR_UNBOX_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["USR_SJ_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["USR_UNSJ_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["USR_WHBOX_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["USR_WHUNBOX_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["MM_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["SA_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["SB_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["ML_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["M2_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["M3_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["XB_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["M4_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["M4_CUS_NAME"].ReadOnly = false;
                foreach (DataRow dr in _dsTemp.Tables[0].Rows)
                {
                    dr["USR_NAME"] = Strings.StrConv(dr["USR_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["USR_QC_NAME"] = Strings.StrConv(dr["USR_QC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["USR_BOX_NAME"] = Strings.StrConv(dr["USR_BOX_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["USR_UNBOX_NAME"] = Strings.StrConv(dr["USR_UNBOX_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["USR_SJ_NAME"] = Strings.StrConv(dr["USR_SJ_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["USR_UNSJ_NAME"] = Strings.StrConv(dr["USR_UNSJ_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["USR_WHBOX_NAME"] = Strings.StrConv(dr["USR_WHBOX_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["USR_WHUNBOX_NAME"] = Strings.StrConv(dr["USR_WHUNBOX_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["MM_NAME"] = Strings.StrConv(dr["MM_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["SA_NAME"] = Strings.StrConv(dr["SA_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["SB_NAME"] = Strings.StrConv(dr["SB_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["ML_NAME"] = Strings.StrConv(dr["ML_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["M2_NAME"] = Strings.StrConv(dr["M2_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["M3_NAME"] = Strings.StrConv(dr["M3_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["XB_NAME"] = Strings.StrConv(dr["XB_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["M4_NAME"] = Strings.StrConv(dr["M4_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["M4_CUS_NAME"] = Strings.StrConv(dr["M4_CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                }
            }

            _frmProcess.progressBar1.Value++;

            _ds = _dsTemp.Copy();
            _dsTemp.Dispose();

            _frmProcess.progressBar1.Value++;

            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables.Contains("BAR_QC"))
            {
                dataGridView1.DataMember = "BAR_QC";
                dataGridView1.DataSource = _ds;
                dataGridView1.Refresh();
            }

            _frmProcess.progressBar1.Value++;

            foreach (DataGridViewColumn _dc in dataGridView1.Columns)
            {
                if (_dc.Name != "BAR_NO" && _dc.Name != "BAT_NO")
                    _dc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();
            GC.Collect();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BAR_NO"].HeaderText = "序列号";
            dataGridView1.Columns["PRD_NO"].HeaderText = "品名";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["USR_NAME"].HeaderText = "打印人员";
            dataGridView1.Columns["SYS_DATE"].HeaderText = "打印日期";
            dataGridView1.Columns["SYS_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_QC_NAME"].HeaderText = "检验人员";
            dataGridView1.Columns["QC_DATE"].HeaderText = "检验时间";
            dataGridView1.Columns["QC_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_BOX_NAME"].HeaderText = "装箱人员";
            dataGridView1.Columns["BOX_DATE"].HeaderText = "装箱时间";
            dataGridView1.Columns["BOX_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_UNBOX_NAME"].HeaderText = "拆箱人员";
            dataGridView1.Columns["UNBOX_DATE"].HeaderText = "拆箱时间";
            dataGridView1.Columns["UNBOX_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_SJ_NAME"].HeaderText = "送缴人员";
            dataGridView1.Columns["SJ_DATE"].HeaderText = "送缴时间";
            dataGridView1.Columns["SJ_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_UNSJ_NAME"].HeaderText = "送缴撤销人员";
            dataGridView1.Columns["UNSJ_DATE"].HeaderText = "送缴撤销时间";
            dataGridView1.Columns["UNSJ_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_WHBOX_NAME"].HeaderText = "仓库装箱人员";
            dataGridView1.Columns["WHBOX_DATE"].HeaderText = "仓库装箱时间";
            dataGridView1.Columns["WHBOX_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_WHUNBOX_NAME"].HeaderText = "仓库拆箱人员";
            dataGridView1.Columns["WHUNBOX_DATE"].HeaderText = "仓库拆箱时间";
            dataGridView1.Columns["WHUNBOX_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["MM_NO"].HeaderText = "缴库单号";
            dataGridView1.Columns["MM_NAME"].HeaderText = "缴库人员";
            dataGridView1.Columns["MM_DD"].HeaderText = "缴库时间";
            dataGridView1.Columns["MM_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["SA_NO"].HeaderText = "销货单号";
            dataGridView1.Columns["SA_NAME"].HeaderText = "销货人员";
            dataGridView1.Columns["SA_DD"].HeaderText = "销货日期";
            dataGridView1.Columns["SA_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["SB_NO"].HeaderText = "退货单号";
            dataGridView1.Columns["SB_NAME"].HeaderText = "退货人员";
            dataGridView1.Columns["SB_DD"].HeaderText = "退货时间";
            dataGridView1.Columns["SB_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["ML_NO"].HeaderText = "领料单号";
            dataGridView1.Columns["ML_NAME"].HeaderText = "领料人员";
            dataGridView1.Columns["ML_DD"].HeaderText = "领料时间";
            dataGridView1.Columns["ML_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["M2_NO"].HeaderText = "退料单号";
            dataGridView1.Columns["M2_NAME"].HeaderText = "退料人员";
            dataGridView1.Columns["M2_DD"].HeaderText = "退料时间";
            dataGridView1.Columns["M2_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["M3_NO"].HeaderText = "补料单号";
            dataGridView1.Columns["M3_NAME"].HeaderText = "补料人员";
            dataGridView1.Columns["M3_DD"].HeaderText = "补料时间";
            dataGridView1.Columns["M3_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["XB_NO"].HeaderText = "非生产领料单号";
            dataGridView1.Columns["XB_NAME"].HeaderText = "非生产领料人员";
            dataGridView1.Columns["XB_DD"].HeaderText = "非生产领料时间";
            dataGridView1.Columns["XB_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["M4_NO"].HeaderText = "托工领料单号";
            dataGridView1.Columns["M4_NAME"].HeaderText = "托工领料人员";
            dataGridView1.Columns["M4_DD"].HeaderText = "托工领料时间";
            dataGridView1.Columns["M4_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["M4_CUS_NAME"].HeaderText = "托工厂商";

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
                _dict["PRD_NO"] = "品号";
                _dict["SPC"] = "规格";
                _dict["IDX_NAME"] = "中类";
                _dict["BAT_NO"] = "批号";
                _dict["USR_NAME"] = "打印人员";
                _dict["SYS_DATE"] = "打印日期";
                _dict["USR_QC_NAME"] = "检验人员";
                _dict["QC_DATE"] = "检验时间";
                _dict["USR_BOX_NAME"] = "装箱人员";
                _dict["BOX_DATE"] = "装箱时间";
                _dict["USR_UNBOX_NAME"] = "拆箱人员";
                _dict["UNBOX_DATE"] = "拆箱时间";
                _dict["USR_SJ_NAME"] = "送缴人员";
                _dict["SJ_DATE"] = "送缴时间";
                _dict["USR_UNSJ_NAME"] = "送缴撤销人员";
                _dict["UNSJ_DATE"] = "送缴撤销时间";
                _dict["USR_WHBOX_NAME"] = "仓库装箱人员";
                _dict["WHBOX_DATE"] = "仓库装箱时间";
                _dict["USR_WHUNBOX_NAME"] = "仓库拆箱人员";
                _dict["WHUNBOX_DATE"] = "仓库拆箱时间";
                _dict["MM_NO"] = "缴库单号";
                _dict["MM_NAME"] = "缴库人员";
                _dict["MM_DD"] = "缴库时间";
                _dict["SA_NO"] = "销货单号";
                _dict["SA_NAME"] = "销货人员";
                _dict["SA_DD"] = "销货日期";
                _dict["SB_NO"] = "退货单号";
                _dict["SB_NAME"] = "退货人员";
                _dict["SB_DD"] = "退货时间";
                _dict["ML_NO"] = "领料单号";
                _dict["ML_NAME"] = "领料人员";
                _dict["ML_DD"] = "领料时间";
                _dict["M2_NO"] = "退料单号";
                _dict["M2_NAME"] = "退料人员";
                _dict["M2_DD"] = "退料时间";
                _dict["M3_NO"] = "补料单号";
                _dict["M3_NAME"] = "补料人员";
                _dict["M3_DD"] = "补料时间";
                _dict["XB_NO"] = "非生产领料单号";
                _dict["XB_NAME"] = "非生产领料人员";
                _dict["XB_DD"] = "非生产领料时间";
                _dict["M4_NO"] = "托工领料单号";
                _dict["M4_NAME"] = "托工领料人员";
                _dict["M4_DD"] = "托工领料时间";
                _dict["M4_CUS_NAME"] = "托工厂商";
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

        private void RPTPrdtHisList_Load(object sender, EventArgs e)
        {
            lstPrdtKnd.SelectedIndex = 0;
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

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "BAR_NO")
            //{
            //    if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = true;
            //    else
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = false;
            //}
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "PRD_NO")
            //{
            //    if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = true;
            //    else
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = false;
            //}
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "SPC")
            //{
            //    if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = true;
            //    else
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = false;
            //}
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "IDX_NAME")
            //{
            //    if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = true;
            //    else
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = false;
            //}
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "BAT_NO")
            //{
            //    if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            //    {
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = true;
            //    }
            //    else
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = false;
            //}
            if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            {
                dataGridView1.Columns[e.ColumnIndex].Frozen = true;
                dataGridView1.Columns[e.ColumnIndex].DisplayIndex = 0;
            }
            else
                dataGridView1.Columns[e.ColumnIndex].Frozen = false;
        }

        private void txtBoxNoE_Enter(object sender, EventArgs e)
        {
            if (txtBoxNoE.Text == "")
                txtBoxNoE.Text = txtBoxNoB.Text;
        }
    }
}