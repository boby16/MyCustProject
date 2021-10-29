//���0����ģ�1����ģ���Ʒ����2�����룻3�����Ʒ��
/*
 * ��ӡ��ʽ��LH\TS\BASEC,BASIC\SAMPLE\SAMLL\FOXCONN\OTHER\HQ\SBB��
 *      �����Ϊ��0����ģ�ʱ��
 *          LH����ӡ�Ϻţ���Ʒ�Ź�����������ӡ�ġ��Ϻš�������Դ�ڱ�BAR_PRTNAME��PRD_NO=Ʒ�ŵ�NAME�ֶΣ�
 *          TS������棨�����������������ӡ�ġ���ӡƷ����������Դ�ڱ�BAR_PRTNAME��IDX_NO=�����NAME�ֶΣ�
 *          BASIC����׼�棬����ӡ�ġ�Ʒ����������Դ�ڻ�Ʒ������ϲ�������ţ��˴�����ά������Ϊ��׼���ӡ����Ϣȫ������ϵͳ��
 *      �����Ϊ��2�����룩ʱ��
 *          BASIC����׼�棬����ӡ�ġ�Ʒ����������Դ�ڻ�Ʒ������ϲ�������ţ��˴�����ά������Ϊ��׼���ӡ����Ϣȫ������ϵͳ��
 *          SAMPLE����Լ�棨���Ʊ�׼�棩
 *          SMALL����ӡ��ţ����Ʊ�׼�棩
 *          FOXCONN����ʿ���棨��Ӧ�̣��̶�Ϊ����Ʒ�����ֹ��޸ģ�Ʒ���̶�Ϊ��Ƭ�ġ����ֹ��޸ģ���Ρ���ʿ���Ϻš���Ʒ�������ֱ���Դ�ڻ�Ʒ���������Զ����ֶΡ�BC_GP������LH_FOXCONN������QTY_PER_GP�������ʣ���Դ��������������Զ����ֶΡ�CZ_GP������
 *          OTHER������棨�����������������ӡ�ġ�Ʒ����������Դ�ڱ�BAR_PRTNAME��IDX_NO=�����NAME�ֶΣ�
 *          HQ������棨ͬ����棩��
 *          SBB���ܰر̰棨��Ʒ�Ź�����������ӡ�ġ���Ʒ��š�������Դ�ڱ�BAR_PRTNAME��PRD_NO=Ʒ�ŵ�NAME�ֶΣ�
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
            cbBCIdx.SelectedIndex = 0;//�����
            if (tcBCPrt.TabPages.Contains(tpBCPrtPrd))
                tcBCPrt.TabPages.Remove(tpBCPrtPrd);
            if (tcBCPrt.TabPages.Contains(tpBCPrtIdx))
                tcBCPrt.TabPages.Remove(tpBCPrtIdx);
            tcBCPrt.TabPages.Add(tpBCPrtIdx);
            cbBoxIdx.SelectedIndex = 0;//�����
            if (tcBoxPrt.TabPages.Contains(tpBoxPrtPrd))
                tcBoxPrt.TabPages.Remove(tpBoxPrtPrd);
            if (tcBoxPrt.TabPages.Contains(tpBoxPrtIdx))
                tcBoxPrt.TabPages.Remove(tpBoxPrtIdx);
            tcBoxPrt.TabPages.Add(tpBoxPrtIdx);
        }

        #region ���

        private void dbBCPrtPrd_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBCPrtPrd.Columns["TYPE_ID"].Visible = false;
            dgBCPrtPrd.Columns["IDX"].HeaderText = "��ӡ��ʽ";
            dgBCPrtPrd.Columns["IDX"].ReadOnly = true;
            dgBCPrtPrd.Columns["IDX_NO"].Visible = false;
            dgBCPrtPrd.Columns["IDX_NAME"].Visible = false;
            dgBCPrtPrd.Columns["PRD_NO"].HeaderText = "Ʒ��";
            dgBCPrtPrd.Columns["PRD_NO"].ToolTipText = "˫��Ʒ�ŵ�Ԫ��ѡ��";
            dgBCPrtPrd.Columns["PRD_NAME"].HeaderText = "��Ʒ����";
            dgBCPrtPrd.Columns["PRD_NAME"].ReadOnly = true;
            dgBCPrtPrd.Columns["SPC"].HeaderText = "��Ʒ���";
            dgBCPrtPrd.Columns["SPC"].ReadOnly = true;
            dgBCPrtPrd.Columns["NAME"].HeaderText = "��ӡ����";
            dgBCPrtPrd.Columns["NAME"].Width = 200;
        }

        private void dgBCPrtIdx_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBCPrtIdx.Columns["TYPE_ID"].Visible = false;
            dgBCPrtIdx.Columns["IDX"].HeaderText = "��ӡ��ʽ";
            dgBCPrtIdx.Columns["IDX"].ReadOnly = true;
            dgBCPrtIdx.Columns["PRD_NO"].Visible = false;
            dgBCPrtIdx.Columns["PRD_NAME"].Visible = false;
            dgBCPrtIdx.Columns["SPC"].Visible = false;
            dgBCPrtIdx.Columns["IDX_NO"].HeaderText = "�������";
            dgBCPrtIdx.Columns["IDX_NO"].ToolTipText = "˫��������ŵ�Ԫ��ѡ��";
            dgBCPrtIdx.Columns["IDX_NAME"].HeaderText = "��������";
            dgBCPrtIdx.Columns["IDX_NAME"].ReadOnly = true;
            dgBCPrtIdx.Columns["NAME"].HeaderText = "��ӡ����";
            dgBCPrtIdx.Columns["NAME"].Width = 200;
        }

        private void btnBCSearch_Click(object sender, EventArgs e)
        {
            //���
            string _where = " AND A.TYPE_ID='0' ";
            //1.�����
            //2.�Ϻ�
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
                //ģ����ѯ
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
                MessageBox.Show("����ɹ���");

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
                _qw.PGM = "INDX";//����
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
                _qw.PGM = "PRDT";//����
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

        #region ������

        private void dgBoxPrtPrd_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBoxPrtPrd.Columns["TYPE_ID"].Visible = false;
            dgBoxPrtPrd.Columns["IDX"].HeaderText = "��ӡ��ʽ";
            dgBoxPrtPrd.Columns["IDX"].ReadOnly = true;
            dgBoxPrtPrd.Columns["IDX_NO"].Visible = false;
            dgBoxPrtPrd.Columns["IDX_NAME"].Visible = false;
            dgBoxPrtPrd.Columns["PRD_NO"].HeaderText = "Ʒ��";
            dgBoxPrtPrd.Columns["PRD_NO"].ToolTipText = "˫��Ʒ�ŵ�Ԫ��ѡ��";
            dgBoxPrtPrd.Columns["PRD_NAME"].HeaderText = "��Ʒ����";
            dgBoxPrtPrd.Columns["PRD_NAME"].ReadOnly = true;
            dgBoxPrtPrd.Columns["SPC"].HeaderText = "��Ʒ���";
            dgBoxPrtPrd.Columns["SPC"].ReadOnly = true;
            dgBoxPrtPrd.Columns["NAME"].HeaderText = "��ӡ����";
            dgBoxPrtPrd.Columns["NAME"].Width = 200;
        }

        private void dgBoxPrtIdx_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBoxPrtIdx.Columns["TYPE_ID"].Visible = false;
            dgBoxPrtIdx.Columns["IDX"].HeaderText = "��ӡ��ʽ";
            dgBoxPrtIdx.Columns["IDX"].ReadOnly = true;
            dgBoxPrtIdx.Columns["PRD_NO"].Visible = false;
            dgBoxPrtIdx.Columns["PRD_NAME"].Visible = false;
            dgBoxPrtIdx.Columns["SPC"].Visible = false;
            dgBoxPrtIdx.Columns["IDX_NO"].HeaderText = "�������";
            dgBoxPrtIdx.Columns["IDX_NO"].ToolTipText = "˫��������ŵ�Ԫ��ѡ��";
            dgBoxPrtIdx.Columns["IDX_NAME"].HeaderText = "��������";
            dgBoxPrtIdx.Columns["IDX_NAME"].ReadOnly = true;
            dgBoxPrtIdx.Columns["NAME"].HeaderText = "��ӡ����";
            dgBoxPrtIdx.Columns["NAME"].Width = 200;
        }

        private void btnBoxSearch_Click(object sender, EventArgs e)
        {
            //����
            string _where = " AND A.TYPE_ID='2' ";
            //1.�����
            //2.�����
            //3.�ܰر̰�
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
                //ģ����ѯ
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
                MessageBox.Show("����ɹ���");

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
                _qw.PGM = "PRDT";//����
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
                _qw.PGM = "INDX";//����
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