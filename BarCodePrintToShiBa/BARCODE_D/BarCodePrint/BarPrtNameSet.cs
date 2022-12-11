//类别（0：板材；1：卷材（成品）；2：箱码；3：半成品）
/*
 * 打印版式（LH\TS\BASEC,BASIC\SAMPLE\SAMLL\FOXCONN\OTHER\HQ\SBB）
 *      当类别为：0（板材）时：
 *          LH：列印料号（跟品号关联），即打印的【料号】资料来源于表BAR_PRTNAME中PRD_NO=品号的NAME字段；
 *          TS：特殊版（跟中类关联），即打印的【打印品名】资料来源于表BAR_PRTNAME中IDX_NO=中类的NAME字段；
 *          BASIC：标准版，即打印的【品名】资料来源于货品中类的上层中类代号（此处不做维护，因为标准版打印的信息全部来自系统）
 *      当类别为：2（箱码）时：
 *          BASIC：标准版，即打印的【品名】资料来源于货品中类的上层中类代号（此处不做维护，因为标准版打印的信息全部来自系统）
 *          SAMPLE：简约版（类似标准版）
 *          SMALL：列印编号（类似标准版）
 *          FOXCONN：富士康版（供应商：固定为【固品】可手工修改；品名固定为【片材】可手工修改；版次、富士康料号、单品重量：分别来源于货品基础资料自定义字段【BC_GP】、【LH_FOXCONN】、【QTY_PER_GP】；材质：来源于中类基础资料自定义字段【CZ_GP】；）
 *          OTHER：特殊版（跟中类关联），即打印的【品名】资料来源于表BAR_PRTNAME中IDX_NO=中类的NAME字段；
 *          HQ：瀚荃版（同特殊版）；
 *          SBB：塑柏碧版（跟品号关联），即打印的【产品编号】资料来源于表BAR_PRTNAME中PRD_NO=品号的NAME字段；
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BarPrtNameSet : Form
    {
        DataSet EditDataSetBC = new DataSet();
        DataSet EditDataSetBox = new DataSet();

        public BarPrtNameSet()
        {
            InitializeComponent();
        }

        private void BarIdxSet_Load(object sender, EventArgs e)
        {
            cbBCIdx.SelectedIndex = 0;//特殊版
            if (tcBCPrt.TabPages.Contains(tpBCPrtPrd))
                tcBCPrt.TabPages.Remove(tpBCPrtPrd);
            if (tcBCPrt.TabPages.Contains(tpBCPrtIdx))
                tcBCPrt.TabPages.Remove(tpBCPrtIdx);
            tcBCPrt.TabPages.Add(tpBCPrtIdx);
            cbBoxIdx.SelectedIndex = 0;//特殊版
            if (tcBoxPrt.TabPages.Contains(tpBoxPrtPrd))
                tcBoxPrt.TabPages.Remove(tpBoxPrtPrd);
            if (tcBoxPrt.TabPages.Contains(tpBoxPrtIdx))
                tcBoxPrt.TabPages.Remove(tpBoxPrtIdx);
            tcBoxPrt.TabPages.Add(tpBoxPrtIdx);
        }

        #region 板材

        private void dbBCPrtPrd_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBCPrtPrd.Columns["TYPE_ID"].Visible = false;
            dgBCPrtPrd.Columns["IDX"].HeaderText = "打印版式";
            dgBCPrtPrd.Columns["IDX"].ReadOnly = true;
            dgBCPrtPrd.Columns["IDX_NO"].Visible = false;
            dgBCPrtPrd.Columns["IDX_NAME"].Visible = false;
            dgBCPrtPrd.Columns["PRD_NO"].HeaderText = "品号";
            dgBCPrtPrd.Columns["PRD_NO"].ToolTipText = "双击品号单元格选择";
            dgBCPrtPrd.Columns["PRD_NAME"].HeaderText = "货品名称";
            dgBCPrtPrd.Columns["PRD_NAME"].ReadOnly = true;
            dgBCPrtPrd.Columns["SPC"].HeaderText = "货品规格";
            dgBCPrtPrd.Columns["SPC"].ReadOnly = true;
            dgBCPrtPrd.Columns["NAME"].HeaderText = "打印名称";
            dgBCPrtPrd.Columns["NAME"].Width = 200;
        }

        private void dgBCPrtIdx_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBCPrtIdx.Columns["TYPE_ID"].Visible = false;
            dgBCPrtIdx.Columns["IDX"].HeaderText = "打印版式";
            dgBCPrtIdx.Columns["IDX"].ReadOnly = true;
            dgBCPrtIdx.Columns["PRD_NO"].Visible = false;
            dgBCPrtIdx.Columns["PRD_NAME"].Visible = false;
            dgBCPrtIdx.Columns["SPC"].Visible = false;
            dgBCPrtIdx.Columns["IDX_NO"].HeaderText = "中类代号";
            dgBCPrtIdx.Columns["IDX_NO"].ToolTipText = "双击中类代号单元格选择";
            dgBCPrtIdx.Columns["IDX_NAME"].HeaderText = "中类名称";
            dgBCPrtIdx.Columns["IDX_NAME"].ReadOnly = true;
            dgBCPrtIdx.Columns["NAME"].HeaderText = "打印名称";
            dgBCPrtIdx.Columns["NAME"].Width = 200;
        }

        private void btnBCSearch_Click(object sender, EventArgs e)
        {
            //板材
            string _where = " AND A.TYPE_ID='0' ";
            //1.特殊版
            //2.料号
            string _idx = "TS";
            if (cbBCIdx.SelectedIndex == 1)
            {
                _where += " AND A.IDX='LH' ";
                _idx = "LH";
            }
            else
            {
                _where += " AND A.IDX='TS' ";
                _idx = "TS";
            }
            if (cbBCFind.Checked)
            {
                //模糊查询
                if (txtBCPrdNo.Text != "")
                    _where += " AND A.PRD_NO LIKE '%" + txtBCPrdNo.Text + "%'";
                if (txtBCPrdName.Text != "")
                    _where += " AND P.NAME LIKE '%" + txtBCPrdName.Text + "%'";
                if (txtBCIdxNo.Text != "")
                    _where += " AND A.IDX_NO LIKE '%" + txtBCIdxNo.Text + "%'";
                if (txtBCIdxName.Text != "")
                    _where += " AND I.NAME LIKE '%" + txtBCIdxName.Text + "%'";
            }
            else
            {
                if (txtBCPrdNo.Text != "")
                    _where += " AND A.PRD_NO='" + txtBCPrdNo.Text + "'";
                if (txtBCPrdName.Text != "")
                    _where += " AND P.NAME='" + txtBCPrdName.Text + "'";
                if (txtBCIdxNo.Text != "")
                    _where += " AND A.IDX_NO='" + txtBCIdxNo.Text + "'";
                if (txtBCIdxName.Text != "")
                    _where += " AND I.NAME='" + txtBCIdxName.Text + "'";
            }
            onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
            _brs.UseDefaultCredentials = true;
            EditDataSetBC = new DataSet();
            EditDataSetBC = _brs.GetBarPrtName("0",_idx, _where);

            dgBCPrtPrd.DataMember = "BAR_PRTNAME";
            dgBCPrtPrd.DataSource = EditDataSetBC;
            dgBCPrtPrd.Refresh();

            dgBCPrtIdx.DataMember = "BAR_PRTNAME";
            dgBCPrtIdx.DataSource = EditDataSetBC;
            dgBCPrtIdx.Refresh();
        }

        private void btnBCSave_Click(object sender, EventArgs e)
        {
            onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
            _brs.UseDefaultCredentials = true;
            string _err = _brs.UpdateBarPrtName(EditDataSetBC);
            if (!String.IsNullOrEmpty(_err))
                MessageBox.Show(_err);
            else
                MessageBox.Show("保存成功！");

            EditDataSetBC.AcceptChanges();
        }

        private void btnBCCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbBCIdx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBCIdx.SelectedIndex == 1)
            {
                if (tcBCPrt.TabPages.Contains(tpBCPrtPrd))
                    tcBCPrt.TabPages.Remove(tpBCPrtPrd);
                if (tcBCPrt.TabPages.Contains(tpBCPrtIdx))
                    tcBCPrt.TabPages.Remove(tpBCPrtIdx);
                tcBCPrt.TabPages.Add(tpBCPrtPrd);
            }
            else
            {
                if (tcBCPrt.TabPages.Contains(tpBCPrtPrd))
                    tcBCPrt.TabPages.Remove(tpBCPrtPrd);
                if (tcBCPrt.TabPages.Contains(tpBCPrtIdx))
                    tcBCPrt.TabPages.Remove(tpBCPrtIdx);
                tcBCPrt.TabPages.Add(tpBCPrtIdx);
            }
            btnBCSearch_Click(null, null);
        }

        private void dgBCPrtIdx_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBCPrtIdx.Columns[e.ColumnIndex].Name == "IDX_NO" && e.RowIndex >= 0)
            {
                QueryWin _qw = new QueryWin();
                _qw.PGM = "INDX";//表名
                _qw.SQLWhere = "";
                if (_qw.ShowDialog() == DialogResult.OK)
                {
                    dgBCPrtIdx.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _qw.NO_RT;
                    dgBCPrtIdx.Rows[e.RowIndex].Cells["IDX_NAME"].Value = _qw.NAME_RTN;
                }
            }
        }

        private void dgBCPrtPrd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBCPrtPrd.Columns[e.ColumnIndex].Name == "PRD_NO" && e.RowIndex >= 0)
            {
                QueryWin _qw = new QueryWin();
                _qw.PGM = "PRDT";//表名
                _qw.SQLWhere = "";
                if (_qw.ShowDialog() == DialogResult.OK)
                {
                    dgBCPrtPrd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _qw.NO_RT;
                    dgBCPrtPrd.Rows[e.RowIndex].Cells["PRD_NAME"].Value = _qw.NAME_RTN;
                    dgBCPrtPrd.Rows[e.RowIndex].Cells["SPC"].Value = _qw.OTHER_RTN;
                }
            }
        }
        #endregion

        #region 箱条码

        private void dgBoxPrtPrd_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBoxPrtPrd.Columns["TYPE_ID"].Visible = false;
            dgBoxPrtPrd.Columns["IDX"].HeaderText = "打印版式";
            dgBoxPrtPrd.Columns["IDX"].ReadOnly = true;
            dgBoxPrtPrd.Columns["IDX_NO"].Visible = false;
            dgBoxPrtPrd.Columns["IDX_NAME"].Visible = false;
            dgBoxPrtPrd.Columns["PRD_NO"].HeaderText = "品号";
            dgBoxPrtPrd.Columns["PRD_NO"].ToolTipText = "双击品号单元格选择";
            dgBoxPrtPrd.Columns["PRD_NAME"].HeaderText = "货品名称";
            dgBoxPrtPrd.Columns["PRD_NAME"].ReadOnly = true;
            dgBoxPrtPrd.Columns["SPC"].HeaderText = "货品规格";
            dgBoxPrtPrd.Columns["SPC"].ReadOnly = true;
            dgBoxPrtPrd.Columns["NAME"].HeaderText = "打印名称";
            dgBoxPrtPrd.Columns["NAME"].Width = 200;
        }

        private void dgBoxPrtIdx_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBoxPrtIdx.Columns["TYPE_ID"].Visible = false;
            dgBoxPrtIdx.Columns["IDX"].HeaderText = "打印版式";
            dgBoxPrtIdx.Columns["IDX"].ReadOnly = true;
            dgBoxPrtIdx.Columns["PRD_NO"].Visible = false;
            dgBoxPrtIdx.Columns["PRD_NAME"].Visible = false;
            dgBoxPrtIdx.Columns["SPC"].Visible = false;
            dgBoxPrtIdx.Columns["IDX_NO"].HeaderText = "中类代号";
            dgBoxPrtIdx.Columns["IDX_NO"].ToolTipText = "双击中类代号单元格选择";
            dgBoxPrtIdx.Columns["IDX_NAME"].HeaderText = "中类名称";
            dgBoxPrtIdx.Columns["IDX_NAME"].ReadOnly = true;
            dgBoxPrtIdx.Columns["NAME"].HeaderText = "打印名称";
            dgBoxPrtIdx.Columns["NAME"].Width = 200;
        }

        private void btnBoxSearch_Click(object sender, EventArgs e)
        {
            //箱码
            string _where = " AND A.TYPE_ID='2' ";
            //1.特殊版
            //2.瀚荃版
            //3.塑柏碧版
            string _idx = "OTHER";
            if (cbBoxIdx.SelectedIndex == 1)
            {
                _where += " AND A.IDX='HQ' ";
                _idx = "HQ";
            }
            else if (cbBoxIdx.SelectedIndex == 2)
            {
                _where += " AND A.IDX='SBB' ";
                _idx = "SBB";
            }
            else
            {
                _where += " AND A.IDX='OTHER' ";
                _idx = "OTHER";
            }
            if (cbBoxFind.Checked)
            {
                //模糊查询
                if (txtBoxPrdNo.Text != "")
                    _where += " AND A.PRD_NO LIKE '%" + txtBoxPrdNo.Text + "%'";
                if (txtBoxPrdName.Text != "")
                    _where += " AND P.NAME LIKE '%" + txtBoxPrdName.Text + "%'";
                if (txtBoxIdxNo.Text != "")
                    _where += " AND A.IDX_NO LIKE '%" + txtBoxIdxNo.Text + "%'";
                if (txtBoxIdxName.Text != "")
                    _where += " AND I.NAME LIKE '%" + txtBoxIdxName.Text + "%'";
            }
            else
            {
                if (txtBoxPrdNo.Text != "")
                    _where += " AND A.PRD_NO='" + txtBoxPrdNo.Text + "'";
                if (txtBoxPrdName.Text != "")
                    _where += " AND P.NAME='" + txtBoxPrdName.Text + "'";
                if (txtBoxIdxNo.Text != "")
                    _where += " AND A.IDX_NO='" + txtBoxIdxNo.Text + "'";
                if (txtBoxIdxName.Text != "")
                    _where += " AND I.NAME='" + txtBoxIdxName.Text + "'";
            }
            onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
            _brs.UseDefaultCredentials = true;
            EditDataSetBox = new DataSet();
            EditDataSetBox = _brs.GetBarPrtName("2",_idx, _where);
            dgBoxPrtPrd.DataMember = "BAR_PRTNAME";
            dgBoxPrtPrd.DataSource = EditDataSetBox;
            dgBoxPrtPrd.Refresh();

            dgBoxPrtIdx.DataMember = "BAR_PRTNAME";
            dgBoxPrtIdx.DataSource = EditDataSetBox;
            dgBoxPrtIdx.Refresh();
        }

        private void btnBoxSave_Click(object sender, EventArgs e)
        {
            onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
            _brs.UseDefaultCredentials = true;
            string _err = _brs.UpdateBarPrtName(EditDataSetBox);
            if (!String.IsNullOrEmpty(_err))
                MessageBox.Show(_err);
            else
                MessageBox.Show("保存成功！");

            EditDataSetBox.AcceptChanges();
        }

        private void btnBoxCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbBoxIdx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoxIdx.SelectedIndex == 2)
            {
                if (tcBoxPrt.TabPages.Contains(tpBoxPrtPrd))
                    tcBoxPrt.TabPages.Remove(tpBoxPrtPrd);
                if (tcBoxPrt.TabPages.Contains(tpBoxPrtIdx))
                    tcBoxPrt.TabPages.Remove(tpBoxPrtIdx);
                tcBoxPrt.TabPages.Add(tpBoxPrtPrd);
            }
            else
            {
                if (tcBoxPrt.TabPages.Contains(tpBoxPrtPrd))
                    tcBoxPrt.TabPages.Remove(tpBoxPrtPrd);
                if (tcBoxPrt.TabPages.Contains(tpBoxPrtIdx))
                    tcBoxPrt.TabPages.Remove(tpBoxPrtIdx);
                tcBoxPrt.TabPages.Add(tpBoxPrtIdx);
            }
            btnBoxSearch_Click(null, null);
        }

        private void dgBoxPrtPrd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBoxPrtPrd.Columns[e.ColumnIndex].Name == "PRD_NO" && e.RowIndex >= 0)
            {
                QueryWin _qw = new QueryWin();
                _qw.PGM = "PRDT";//表名
                _qw.SQLWhere = "";
                if (_qw.ShowDialog() == DialogResult.OK)
                {
                    dgBoxPrtPrd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _qw.NO_RT;
                    dgBoxPrtPrd.Rows[e.RowIndex].Cells["PRD_NAME"].Value = _qw.NAME_RTN;
                    dgBoxPrtPrd.Rows[e.RowIndex].Cells["SPC"].Value = _qw.OTHER_RTN;
                }
            }
        }

        private void dgBoxPrtIdx_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBoxPrtIdx.Columns[e.ColumnIndex].Name == "IDX_NO" && e.RowIndex >= 0)
            {
                QueryWin _qw = new QueryWin();
                _qw.PGM = "INDX";//表名
                _qw.SQLWhere = "";
                if (_qw.ShowDialog() == DialogResult.OK)
                {
                    dgBoxPrtIdx.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _qw.NO_RT;
                    dgBoxPrtIdx.Rows[e.RowIndex].Cells["IDX_NAME"].Value = _qw.NAME_RTN;
                }
            }
        }

        #endregion
    }
}