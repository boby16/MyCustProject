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
                MessageBox.Show("���ݶ�ȡ���������ԡ�ԭ��\n" + _err);
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
            dataGridView1.Columns["BAR_NO"].HeaderText = "���к�";
            dataGridView1.Columns["PRD_NO"].HeaderText = "Ʒ��";
            dataGridView1.Columns["SPC"].HeaderText = "���";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "����";
            dataGridView1.Columns["BAT_NO"].HeaderText = "����";
            dataGridView1.Columns["USR_NAME"].HeaderText = "��ӡ��Ա";
            dataGridView1.Columns["SYS_DATE"].HeaderText = "��ӡ����";
            dataGridView1.Columns["SYS_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_QC_NAME"].HeaderText = "������Ա";
            dataGridView1.Columns["QC_DATE"].HeaderText = "����ʱ��";
            dataGridView1.Columns["QC_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_BOX_NAME"].HeaderText = "װ����Ա";
            dataGridView1.Columns["BOX_DATE"].HeaderText = "װ��ʱ��";
            dataGridView1.Columns["BOX_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_UNBOX_NAME"].HeaderText = "������Ա";
            dataGridView1.Columns["UNBOX_DATE"].HeaderText = "����ʱ��";
            dataGridView1.Columns["UNBOX_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_SJ_NAME"].HeaderText = "�ͽ���Ա";
            dataGridView1.Columns["SJ_DATE"].HeaderText = "�ͽ�ʱ��";
            dataGridView1.Columns["SJ_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_UNSJ_NAME"].HeaderText = "�ͽɳ�����Ա";
            dataGridView1.Columns["UNSJ_DATE"].HeaderText = "�ͽɳ���ʱ��";
            dataGridView1.Columns["UNSJ_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_WHBOX_NAME"].HeaderText = "�ֿ�װ����Ա";
            dataGridView1.Columns["WHBOX_DATE"].HeaderText = "�ֿ�װ��ʱ��";
            dataGridView1.Columns["WHBOX_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["USR_WHUNBOX_NAME"].HeaderText = "�ֿ������Ա";
            dataGridView1.Columns["WHUNBOX_DATE"].HeaderText = "�ֿ����ʱ��";
            dataGridView1.Columns["WHUNBOX_DATE"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["MM_NO"].HeaderText = "�ɿⵥ��";
            dataGridView1.Columns["MM_NAME"].HeaderText = "�ɿ���Ա";
            dataGridView1.Columns["MM_DD"].HeaderText = "�ɿ�ʱ��";
            dataGridView1.Columns["MM_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["SA_NO"].HeaderText = "��������";
            dataGridView1.Columns["SA_NAME"].HeaderText = "������Ա";
            dataGridView1.Columns["SA_DD"].HeaderText = "��������";
            dataGridView1.Columns["SA_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["SB_NO"].HeaderText = "�˻�����";
            dataGridView1.Columns["SB_NAME"].HeaderText = "�˻���Ա";
            dataGridView1.Columns["SB_DD"].HeaderText = "�˻�ʱ��";
            dataGridView1.Columns["SB_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["ML_NO"].HeaderText = "���ϵ���";
            dataGridView1.Columns["ML_NAME"].HeaderText = "������Ա";
            dataGridView1.Columns["ML_DD"].HeaderText = "����ʱ��";
            dataGridView1.Columns["ML_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["M2_NO"].HeaderText = "���ϵ���";
            dataGridView1.Columns["M2_NAME"].HeaderText = "������Ա";
            dataGridView1.Columns["M2_DD"].HeaderText = "����ʱ��";
            dataGridView1.Columns["M2_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["M3_NO"].HeaderText = "���ϵ���";
            dataGridView1.Columns["M3_NAME"].HeaderText = "������Ա";
            dataGridView1.Columns["M3_DD"].HeaderText = "����ʱ��";
            dataGridView1.Columns["M3_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["XB_NO"].HeaderText = "���������ϵ���";
            dataGridView1.Columns["XB_NAME"].HeaderText = "������������Ա";
            dataGridView1.Columns["XB_DD"].HeaderText = "����������ʱ��";
            dataGridView1.Columns["XB_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["M4_NO"].HeaderText = "�й����ϵ���";
            dataGridView1.Columns["M4_NAME"].HeaderText = "�й�������Ա";
            dataGridView1.Columns["M4_DD"].HeaderText = "�й�����ʱ��";
            dataGridView1.Columns["M4_DD"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView1.Columns["M4_CUS_NAME"].HeaderText = "�й�����";

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
                _dict["BAR_NO"] = "���к�";
                _dict["PRD_NO"] = "Ʒ��";
                _dict["SPC"] = "���";
                _dict["IDX_NAME"] = "����";
                _dict["BAT_NO"] = "����";
                _dict["USR_NAME"] = "��ӡ��Ա";
                _dict["SYS_DATE"] = "��ӡ����";
                _dict["USR_QC_NAME"] = "������Ա";
                _dict["QC_DATE"] = "����ʱ��";
                _dict["USR_BOX_NAME"] = "װ����Ա";
                _dict["BOX_DATE"] = "װ��ʱ��";
                _dict["USR_UNBOX_NAME"] = "������Ա";
                _dict["UNBOX_DATE"] = "����ʱ��";
                _dict["USR_SJ_NAME"] = "�ͽ���Ա";
                _dict["SJ_DATE"] = "�ͽ�ʱ��";
                _dict["USR_UNSJ_NAME"] = "�ͽɳ�����Ա";
                _dict["UNSJ_DATE"] = "�ͽɳ���ʱ��";
                _dict["USR_WHBOX_NAME"] = "�ֿ�װ����Ա";
                _dict["WHBOX_DATE"] = "�ֿ�װ��ʱ��";
                _dict["USR_WHUNBOX_NAME"] = "�ֿ������Ա";
                _dict["WHUNBOX_DATE"] = "�ֿ����ʱ��";
                _dict["MM_NO"] = "�ɿⵥ��";
                _dict["MM_NAME"] = "�ɿ���Ա";
                _dict["MM_DD"] = "�ɿ�ʱ��";
                _dict["SA_NO"] = "��������";
                _dict["SA_NAME"] = "������Ա";
                _dict["SA_DD"] = "��������";
                _dict["SB_NO"] = "�˻�����";
                _dict["SB_NAME"] = "�˻���Ա";
                _dict["SB_DD"] = "�˻�ʱ��";
                _dict["ML_NO"] = "���ϵ���";
                _dict["ML_NAME"] = "������Ա";
                _dict["ML_DD"] = "����ʱ��";
                _dict["M2_NO"] = "���ϵ���";
                _dict["M2_NAME"] = "������Ա";
                _dict["M2_DD"] = "����ʱ��";
                _dict["M3_NO"] = "���ϵ���";
                _dict["M3_NAME"] = "������Ա";
                _dict["M3_DD"] = "����ʱ��";
                _dict["XB_NO"] = "���������ϵ���";
                _dict["XB_NAME"] = "������������Ա";
                _dict["XB_DD"] = "����������ʱ��";
                _dict["M4_NO"] = "�й����ϵ���";
                _dict["M4_NAME"] = "�й�������Ա";
                _dict["M4_DD"] = "�й�����ʱ��";
                _dict["M4_CUS_NAME"] = "�й�����";
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
            _qw.PGM = "BAT_NO";//����
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNoB.Text = _qw.NO_RT;
            }
        }

        private void qryBatNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//����
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