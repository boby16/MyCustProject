using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class QueryBat : Form
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
        private bool _multiSelect = false;
        /// <summary>
        /// 是否允许多选
        /// </summary>
        public bool MultiSelect
        {
            get { return this._multiSelect; }
            set { this._multiSelect = value; }
        }
        private string _no_rtn = "";
        /// <summary>
        /// 代号
        /// </summary>
        public string NO_RT
        {
            get { return this._no_rtn; }
            set { this._no_rtn = value; }
        }
        private string _name_rtn = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME_RTN
        {
            get { return this._name_rtn; }
            set { this._name_rtn = value; }
        }
        private string _other_rtn = "";
        /// <summary>
        /// 其他字段
        /// </summary>
        public string OTHER_RTN
        {
            get { return this._other_rtn; }
            set { this._other_rtn = value; }
        }

        private DataSet _ds = new DataSet();
        public QueryBat()
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
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;

            if (SQLWhere != "")
                _sqlWhere += SQLWhere;

            if (txtNo.Text.Trim() != "")
                _sqlWhere += " AND A.BAT_NO LIKE '" + txtNo.Text.Trim() + "%' ";
            if (txtName.Text.Trim() != "")
                _sqlWhere += " AND A.BAT_NO LIKE '%" + txtName.Text.Trim() + "%' ";

            _ds = _bps.GetBatNo2(_sqlWhere);

            dataGridView1.DataMember = "BAT_NO";
            dataGridView1.DataSource = _ds;
            dataGridView1.Refresh();

            foreach (DataGridViewColumn _dc in dataGridView1.Columns)
            {
                _dc.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            if (dataGridView1.RowCount > 0 && dataGridView1.CurrentRow.Index > -1)
            {
                if (MultiSelect)
                {
                    string _batNo = "";
                    string _batNo1 = "";
                    string _prd_no = "";
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        _batNo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        _batNo1 = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                        _prd_no =  dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    }
                    else
                    {
                        foreach (DataGridViewRow _dr in dataGridView1.SelectedRows)
                        {
                            if (_prd_no == "")
                                _prd_no = _dr.Cells[3].Value.ToString();
                            if (_prd_no != _dr.Cells[3].Value.ToString())
                            {
                                MessageBox.Show("选择的批号必须是同一货品！");
                                _batNo = "";
                                _prd_no = "";
                                break;
                            }

                            if (_batNo != "")
                                _batNo += "/";
                            _batNo += _dr.Cells[0].Value.ToString();
                            if (_batNo1 != "")
                                _batNo1 += "/";
                            _batNo1 += _dr.Cells[1].Value.ToString();
                        }
                    }
                    this.NO_RT = _batNo;
                    this.NAME_RTN = _batNo1;
                    this.OTHER_RTN = _prd_no;
                }
                else
                {
                    this.NO_RT = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    this.NAME_RTN = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells.Count >3)
                    {
                        this.OTHER_RTN = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    } 
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("请选择批号！");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BAT_NO_DIS"].HeaderText = "代号";
            dataGridView1.Columns["BAT_NO"].Visible = false;
            dataGridView1.Columns["NAME"].Visible = false;
            dataGridView1.Columns["PRD_NO"].HeaderText = "品号";
            dataGridView1.Columns["SPC"].Visible = false;
            dataGridView1.Columns["KND"].Visible = false;
            dataGridView1.Columns["IDX1"].Visible = false;
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
        }

        private void QueryBat_Load(object sender, EventArgs e)
        {
            if (this.MultiSelect)
                dataGridView1.MultiSelect = true;
            else
                dataGridView1.MultiSelect = false;
            GetData();
        }

        private void txtNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNo.Text != "")
            {
                DataRow[] dr = _ds.Tables[0].Select("BAT_NO like '" + txtNo.Text + "%'");
                int i = 0;
                if (dr.Length > 0)
                {
                    i = Array.IndexOf(_ds.Tables[0].Select(""), dr[0]);
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                }
            }
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtName.Text != "")
            {
                DataRow[] dr = _ds.Tables[0].Select("BAT_NO like '" + txtName.Text + "%'");
                int i = 0;
                if (dr.Length > 0)
                {
                    i = Array.IndexOf(_ds.Tables[0].Select(), dr[0]);
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.CurrentRow.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                }
            }
        }
    }
}