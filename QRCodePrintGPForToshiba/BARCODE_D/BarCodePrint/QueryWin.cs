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
	public partial class QueryWin : Form
	{
		#region property
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
		private string _pgm = "";
		/// <summary>
		/// TableName
		/// </summary>
		public string PGM
		{
			get { return this._pgm; }
			set { this._pgm = value; }
        }
        private string _where = "";
        /// <summary>
        /// 查询条件
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
		#endregion

		private DataSet _ds = new DataSet();
        string _no = "";
        string _name = "";
        string _others = "";

		public QueryWin()
		{
			InitializeComponent();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			GetData();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1)
			{
                if (MultiSelect == true)
                {
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        this.NO_RT = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        this.NAME_RTN = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                        if (dataGridView1.CurrentRow.Cells.Count > 2)
                        {
                            this.OTHER_RTN = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow _dr in dataGridView1.SelectedRows)
                        {
                            if (!String.IsNullOrEmpty(this.NO_RT))
                            {
                                this.NO_RT += "/";
                                this.NAME_RTN += "/";
                            }
                            this.NO_RT += _dr.Cells[0].Value.ToString();
                            this.NAME_RTN += _dr.Cells[1].Value.ToString();
                        }
                    }
                }
                else
                {
                    this.NO_RT = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    this.NAME_RTN = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells.Count > 2)
                    {
                        this.OTHER_RTN = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    }
                }
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}
		private void GetData()
		{
			onlineService.BarPrintServer _drs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
			_drs.UseDefaultCredentials = true;
			string _sqlWhere = "";
			switch (PGM)
			{
				case "DEPT":
					_no = "DEP";
					_name = "NAME";
					break;
				case "PRDT":
					_no = "PRD_NO";
					_name = "NAME";
					_others = ",SPC";
					break;
				case "BAT_NO":
					_no = "BAT_NO";
					_name = "NAME";
					break;
                case "INDX":
                    _no = "IDX_NO";
                    _name = "NAME";
                    break;
                case "MF_MO":
                    _no = "MO_NO";
                    _name = "MO_DD";
                    _sqlWhere += SQLWhere;
                    break;
                case "CUST":
                    _no = "CUS_NO";
                    _name = "NAME";
                    break;
                case "SALM":
                    _no = "SAL_NO";
                    _name = "NAME";
                    break;
                case "SPC_LST":
                    _no = "SPC_NO";
                    _name = "NAME";
                    //btnAdd.Visible = true;
                    //btnSave.Visible = true;
                    break;
                case "MY_WH":
                    _no = "WH";
                    _name = "NAME";
                    break;
                case "PSWD":
                    _no = "USR";
                    _name = "NAME";
                    break;
                case "BAR_IDX":
                    _no = "IDX_NO";
                    _name = "NAME";
                    break;
                case "MF_PSS":
                    _no = "PS_NO";
                    _name = "PS_DD";
                    break;
			}
            if (SQLWhere != "")
                _sqlWhere += SQLWhere;
			if (txtNO.Text.Trim() != "")
				_sqlWhere += " AND " + _no + " LIKE '" + txtNO.Text.Trim() + "%' ";
			if (txtName.Text.Trim() != "")
				_sqlWhere += " AND " + _name + " LIKE '%" + txtName.Text.Trim() + "%' ";

            if (PGM == "MF_PSS")
                _sqlWhere += " ORDER BY " + _name + " DESC ";
            else
                _sqlWhere += " ORDER BY " + _no;
			if (!String.IsNullOrEmpty(PGM))
			{
                _ds = _drs.QueryWinData(_no, _name, _others, PGM, _sqlWhere);
			}
			if (_ds != null && _ds.Tables.Count > 0)
			{
				this.dataGridView1.DataMember = PGM;
            }
			this.dataGridView1.DataSource = _ds;
			this.dataGridView1.Refresh();

            foreach (DataGridViewColumn _dc in dataGridView1.Columns)
            {
                _dc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
		}

		private void QueryWin_Load(object sender, EventArgs e)
		{
            if (this.MultiSelect)
                dataGridView1.MultiSelect = true;
            else
                dataGridView1.MultiSelect = false;
			GetData();
		}

		private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
            if (PGM == "MF_MO")
            {
                dataGridView1.Columns[0].HeaderText = "制令单号";
                dataGridView1.Columns[1].HeaderText = "制单日期";
            }
            else if (PGM == "MF_PSS")
            {
                dataGridView1.Columns[0].HeaderText = "销货单号";
                dataGridView1.Columns[1].HeaderText = "销货日期";
            }
            else
            {
                dataGridView1.Columns[0].HeaderText = "代号";
                dataGridView1.Columns[1].HeaderText = "名称";
            }
			dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			for (int i = 2; i < dataGridView1.Columns.Count; i++)
			{
				dataGridView1.Columns[i].Visible = false;
			}
		}

		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				btnOK_Click(null, null);
			}
		}

        private void txtNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNO.Text != "")
            {
                DataRow[] dr = _ds.Tables[0].Select(_no + " like '" + txtNO.Text + "%'");
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
                DataRow[] dr = _ds.Tables[0].Select(_name + " like '" + txtName.Text + "%'");
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

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    dataGridView1.AllowUserToAddRows = true;
        //    dataGridView1.ReadOnly = false;
        //}

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    DataSet _ds = ((DataSet)dataGridView1.DataSource);
        //    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
        //    _bps.UseDefaultCredentials = true;
        //    _bps.UpdataDataSPC(_ds);
        //}
	}
}