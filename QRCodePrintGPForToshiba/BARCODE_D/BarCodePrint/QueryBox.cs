using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class QueryBox : Form
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

        private DataSet _boxDS = new DataSet();
        /// <summary>
        /// 
        /// </summary>
        public DataSet BoxDS
        {
            get { return this._boxDS; }
            set { this._boxDS = value; }
        }


        private DataSet _ds = new DataSet();
        public QueryBox()
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

            if (this.txtBoxNoB.Text.Trim() != "")
                _sqlWhere += " AND A.BOX_NO >= '" + this.txtBoxNoB.Text.Trim() + "' ";
            if (this.txtBoxNoE.Text.Trim() != "")
                _sqlWhere += " AND A.BOX_NO <= '" + this.txtBoxNoE.Text.Trim() + "' ";
            if (this.txtPrdNoB.Text.Trim() != "")
                _sqlWhere += " AND A.PRD_NO >= '" + this.txtPrdNoB.Text.Trim() + "' ";
            if (this.txtPrdNoE.Text.Trim() != "")
                _sqlWhere += " AND A.PRD_NO <= '" + this.txtPrdNoE.Text.Trim() + "' ";

            _ds = _drs.GetBoxBar1(_sqlWhere + this.SQLWhere);

            if (_ds != null && _ds.Tables.Count > 0)
            {
                _ds.Tables[0].Columns["FORMAT"].ReadOnly = false;
                _ds.Tables[0].Columns["FORMAT"].MaxLength = 10;
                #region 打印版式
                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
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

                #endregion

                dataGridView1.DataMember = "BAR_BOX";
                dataGridView1.DataSource = _ds;
                dataGridView1.Refresh();
            }
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
                    string _boxNo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    if (_ds != null && _ds.Tables.Count > 0)
                    {
                        DataRow[] _drArr = _ds.Tables[0].Select("BOX_NO='" + _boxNo + "'");
                        if (_drArr.Length > 0)
                        {
                            BoxDS = _ds.Clone();
                            BoxDS.Merge(_drArr);
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
                MessageBox.Show("请选择箱条码！");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (this.BoxDS != null && BoxDS.Tables.Count > 0 && BoxDS.Tables[0].Rows.Count > 0)
            {
                BoxDS.Clear();
                BoxDS.AcceptChanges();
            }
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BOX_NO"].Visible = false;
            dataGridView1.Columns["BOX_NO_DIS"].HeaderText = "箱条码";
            dataGridView1.Columns["PRD_NO"].HeaderText = "品号";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["QTY"].HeaderText = "数量";
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["FORMAT"].HeaderText = "打印版式";
            dataGridView1.Columns["WH_SJ"].Visible = false;
            dataGridView1.Columns["BOX_DD"].Visible = false;
            dataGridView1.Columns["CONTENT"].Visible = false;
            dataGridView1.Columns["DEP"].Visible = false;
            dataGridView1.Columns["PH_FLAG"].Visible = false;
            dataGridView1.Columns["USR"].Visible = false;
            dataGridView1.Columns["WH"].Visible = false;
            dataGridView1.Columns["STOP_ID"].Visible = false;
            dataGridView1.Columns["VALID_DD"].Visible = false;
            dataGridView1.Columns["IDX1"].Visible = false;
            dataGridView1.Columns["IDX_UP"].Visible = false;
            dataGridView1.Columns["QC"].Visible = false;
            dataGridView1.Columns["STATUS"].Visible = false;
            dataGridView1.Columns["BIL_ID"].Visible = false;
            dataGridView1.Columns["BIL_NO"].Visible = false;
            dataGridView1.Columns["REM"].Visible = false;
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

        private void qryPrdNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNoB.Text = _qw.NO_RT;
            }
        }

        private void txtPrdNoE_Enter(object sender, EventArgs e)
        {
            if (txtPrdNoE.Text == "")
                txtPrdNoE.Text = txtPrdNoB.Text;
        }

        private void txtBoxNoE_Enter(object sender, EventArgs e)
        {
            if (txtBoxNoE.Text == "")
                txtBoxNoE.Text = txtBoxNoB.Text;
        }
    }
}