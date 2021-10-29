using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class QuerySA : Form
    {
        private string _where = "";
        /// <summary>
        /// sqlwhere条件
        /// </summary>
        public string SQLWhere
        {
            get { return this._where; }
            set { this._where = value; }
        }

        private string _saNo = "";
        /// <summary>
        /// 销货单号
        /// </summary>
        public string SaNo
        {
            get { return this._saNo; }
            set { this._saNo = value; }
        }


        private DataSet _ds = new DataSet();
        public QuerySA()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            string _sqlWhere = "";
            onlineService.BarPrintServer _drs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
            _drs.UseDefaultCredentials = true;
            _sqlWhere += " AND A.PS_ID='SA' ";
            if (this.txtSADdB.Text.Trim() != "")
                _sqlWhere += " AND A.PS_DD >= '" + this.txtSADdB.Text.Trim() + "' ";
            if (this.txtSADdE.Text.Trim() != "")
                _sqlWhere += " AND A.PS_DD <= '" + this.txtSADdE.Text.Trim() + "' ";
            if (this.txtCusNoB.Text.Trim() != "")
                _sqlWhere += " AND A.CUS_NO >= '" + this.txtCusNoB.Text.Trim() + "' ";
            if (this.txtCusNoE.Text.Trim() != "")
                _sqlWhere += " AND A.CUS_NO <= '" + this.txtCusNoE.Text.Trim() + "' ";
            //if (txtPrdNoB.Text != "")
            //    _sqlWhere += " AND B.PRD_NO >= '" + txtPrdNoB.Text + "' ";
            //if (txtPrdNoE.Text != "")
            //    _sqlWhere += " AND B.PRD_NO <= '" + txtPrdNoE.Text + "' ";

            _ds = _drs.QuerySAData(_sqlWhere + this.SQLWhere);

            dataGridView1.DataMember = "MF_PSS";
            dataGridView1.DataSource = _ds;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnOK_Click(null, null);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.CurrentRow.Index > -1)
                {
                    SaNo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
                MessageBox.Show("请选择销货单！");
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["PS_NO"].HeaderText = "销货单号";
            dataGridView1.Columns["PS_DD"].HeaderText = "销货日期";
            dataGridView1.Columns["CUS_NO"].HeaderText = "客户代号";
            dataGridView1.Columns["CUS_NAME"].HeaderText = "客户名称";
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

        private void txtPrdNoE_Enter(object sender, EventArgs e)
        {
            if (txtPrdNoE.Text == "")
                txtPrdNoE.Text = txtPrdNoB.Text;
        }

        private void txtCusNoE_Enter(object sender, EventArgs e)
        {
            if (txtCusNoE.Text == "")
                txtCusNoE.Text = txtCusNoB.Text;
        }

        private void qryCusNOB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "CUST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtCusNoB.Text = _qw.NO_RT;
            }
        }

        private void qryCusNOE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "CUST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtCusNoE.Text = _qw.NO_RT;
            }
        }
    }
}