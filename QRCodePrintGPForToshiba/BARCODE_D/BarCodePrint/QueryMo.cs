using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class QueryMo : Form
    {
        private string _where = "";
        /// <summary>
        /// sqlwhere
        /// </summary>
        public string SQLWhere
        {
            get { return this._where; }
            set { this._where = value; }
        }

        private DataSet _moDS = new DataSet();
        /// <summary>
        /// 
        /// </summary>
        public DataSet MoDS
        {
            get { return this._moDS; }
            set { this._moDS = value; }
        }


        private DataSet _ds = new DataSet();
        public QueryMo()
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
            if (cbMOCloseId.Checked)
                _sqlWhere += "";
            else
                _sqlWhere += " AND ISNULL(A.QTY_FIN,0)<A.QTY AND ISNULL(A.CLOSE_ID,'')<>'T' ";
            if (this.txtMoDdB.Text.Trim() != "")
                _sqlWhere += " AND A.MO_DD >= '" + this.txtMoDdB.Text.Trim() + "' ";
            if (this.txtMoDdE.Text.Trim() != "")
                _sqlWhere += " AND A.MO_DD <= '" + this.txtMoDdE.Text.Trim() + "' ";
            if (this.txtMoNoB.Text.Trim() != "")
                _sqlWhere += " AND A.MO_NO >= '" + this.txtMoNoB.Text.Trim() + "' ";
            if (this.txtMoNoE.Text.Trim() != "")
                _sqlWhere += " AND A.MO_NO <= '" + this.txtMoNoE.Text.Trim() + "' ";
            if (txtPrdNoB.Text != "")
                _sqlWhere += " AND A.MRP_NO >= '" + txtPrdNoB.Text + "' ";
            if (txtPrdNoE.Text != "")
                _sqlWhere += " AND A.MRP_NO <= '" + txtPrdNoE.Text + "' ";

            _ds = _drs.QueryMOData(_sqlWhere + this.SQLWhere);

            dataGridView1.DataSource = _ds;
            dataGridView1.DataMember = "MF_MO";
            dataGridView1.Refresh();
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
                    string _moNo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    if (_ds != null && _ds.Tables.Count > 0)
                    {
                        DataRow[] _drArr = _ds.Tables[0].Select("MO_NO='" + _moNo + "'");
                        if (_drArr.Length > 0)
                        {
                            MoDS = _ds.Clone();
                            MoDS.Merge(_drArr);
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
                MessageBox.Show("请选择制令单！");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (this.MoDS != null && MoDS.Tables.Count > 0 && MoDS.Tables[0].Rows.Count > 0)
            {
                MoDS.Clear();
                MoDS.AcceptChanges();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView1.ColumnCount > 18)
            {
                dataGridView1.Columns[0].HeaderText = "制单日期";
                dataGridView1.Columns[1].HeaderText = "制令单号";
                dataGridView1.Columns[2].HeaderText = "产品代号";
                dataGridView1.Columns[3].HeaderText = "规格";
                dataGridView1.Columns[4].Visible = false;//特征
                dataGridView1.Columns[5].Visible = false;//产品名称
                dataGridView1.Columns[6].Visible = false;//库位代号
                dataGridView1.Columns[7].HeaderText = "库位";
                dataGridView1.Columns[8].Visible = false;//客户代号
                dataGridView1.Columns[9].HeaderText = "客户";

                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].HeaderText = "主单位";
                dataGridView1.Columns[12].HeaderText = "数量";
                dataGridView1.Columns[13].HeaderText = "数量(副)";
                dataGridView1.Columns[14].HeaderText = "已缴库量";
                dataGridView1.Columns[15].HeaderText = "不合格量";
                dataGridView1.Columns[12].DefaultCellStyle.Format = "F";
                dataGridView1.Columns[13].DefaultCellStyle.Format = "F";
                dataGridView1.Columns[14].DefaultCellStyle.Format = "F";
                dataGridView1.Columns[15].DefaultCellStyle.Format = "F";

                dataGridView1.Columns[16].HeaderText = "批号";
                dataGridView1.Columns[17].HeaderText = "配方号";
                dataGridView1.Columns[18].Visible = false;//制单人
                dataGridView1.Columns[19].Visible = false;//部门
                dataGridView1.Columns[20].Visible = false;//中类代号
                dataGridView1.Columns[21].HeaderText = "中类";
                dataGridView1.Columns[22].HeaderText = "备注";

                dataGridView1.Columns[9].DisplayIndex = 0;
                dataGridView1.Columns[21].DisplayIndex = 1;
                dataGridView1.Columns[3].DisplayIndex = 2;
                dataGridView1.Columns[1].DisplayIndex = 3;
                dataGridView1.Columns[17].DisplayIndex = 4;
                dataGridView1.Columns[12].DisplayIndex = 5;
                dataGridView1.Columns[14].DisplayIndex = 6;
                dataGridView1.Columns[11].DisplayIndex = 7;
            }
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

        private void qryMONOB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MF_MO";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtMoNoB.Text = _qw.NO_RT;
            }
        }

        private void qryMONOE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MF_MO";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtMoNoE.Text = _qw.NO_RT;
            }
        }

        private void txtMoNoE_Enter(object sender, EventArgs e)
        {
            if (txtMoNoE.Text == "")
                txtMoNoE.Text = txtMoNoB.Text;
        }
    }
}