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
    public partial class RPTPrdtStat : Form
    {
        private DataSet _ds = new DataSet();
        private DataTable _dtSpc = new DataTable();
        private DataTable _dtBarSpc = new DataTable();
        public RPTPrdtStat()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 5;

            string _sqlWhere = "";
            string _whereb = "";
            if (lstPrdtKnd.SelectedIndex == 1)
                _sqlWhere += " AND LEN(REPLACE(A.BAR_NO,'#',''))=22 ";
            if (lstPrdtKnd.SelectedIndex == 2)
                _sqlWhere += " AND LEN(REPLACE(A.BAR_NO,'#',''))=24 ";
            if (lstPrdtKnd.SelectedIndex == 3)
                _sqlWhere += " AND LEN(REPLACE(A.BAR_NO,'#',''))>24 ";
            if (txtSpcNoB.Text != "")
                _sqlWhere += " AND A.SPC_NO>='" + this.txtSpcNoB.Text + "' ";
            if (txtSpcNoE.Text != "")
                _sqlWhere += " AND A.SPC_NO<='" + this.txtSpcNoE.Text + "' ";

            #region 起止时间

            if (lstDateType.SelectedIndex == 1)
            {
                //品检时间
                if (txtDDB.Checked)
                    _sqlWhere += " AND A.QC_DATE>='" + this.txtDDB.Text + ":00' ";
                if (txtDDE.Checked)
                    _sqlWhere += " AND A.QC_DATE<='" + this.txtDDE.Text + ":59' ";
            }
            else if (lstDateType.SelectedIndex == 2)
            {
                //送缴时间
                if (txtDDB.Checked)
                    _sqlWhere += " AND A.SJ_DATE>='" + this.txtDDB.Text + ":00' ";
                if (txtDDE.Checked)
                    _sqlWhere += " AND A.SJ_DATE<='" + this.txtDDE.Text + ":59' ";
            }
            else
            {
                //打印时间
                if (txtDDB.Checked)
                    _sqlWhere += " AND A.SYS_DATE>='" + this.txtDDB.Text + ":00' ";
                if (txtDDE.Checked)
                    _sqlWhere += " AND A.SYS_DATE<='" + this.txtDDE.Text + ":59' ";
            }

            #endregion

            if (txtDepB.Text != "")
                _sqlWhere += " AND SUBSTRING(A.BAR_NO,16,1)>='" + this.txtDepB.Text + "' ";
            if (txtDepE.Text != "")
                _sqlWhere += " AND SUBSTRING(A.BAR_NO,16,1)<='" + this.txtDepE.Text + "' ";
            if (txtPrdNoB.Text != "")
                _sqlWhere += " AND SUBSTRING(A.BAR_NO,1,2)<='" + this.txtPrdNoB.Text + "' ";
            if (txtPrdNoE.Text != "")
                _sqlWhere += " AND SUBSTRING(A.BAR_NO,1,2)<='" + this.txtPrdNoE.Text + "' ";

            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;

            _frmProcess.progressBar1.Value++;

            DataSet _dsTemp = new DataSet();
            try
            {
                _dsTemp = _bps.GetPrdtStat(_sqlWhere, _whereb);
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

            _dtSpc = _dsTemp.Tables["SPC_LST"];
            if (_dtBarSpc.Columns.Count > 18)
            {
            }
            else
            {
                _dtBarSpc.Columns.Add("Blank", System.Type.GetType("System.String"));
                foreach (DataRow _drSpc in _dtSpc.Rows)
                {
                    if (_drSpc["SPC_NO"].ToString() != "")
                    {
                        _dtBarSpc.Columns.Add(_drSpc["SPC_NO"].ToString(), System.Type.GetType("System.String"));
                    }
                }
            }
            _ds.Tables["PRDTSTAT_RPT"].Clear();
            //foreach (DataRow _drAll in _dsTemp.Tables["BAR_ALL"].Rows)
            //{
            //    _ds.Tables["PRDTSTAT_RPT"].ImportRow(_drAll);
            //}
            string _prdNo = "";
            string _dep="";
            string _spc = "";
            string _bat_no = "";
            string _spcNo = "";
            decimal _qty1_ng = 0;
            decimal _qty1 = 0;
            DataRow _drNew;
            _dtBarSpc.Clear();

            _frmProcess.progressBar1.Value++;

            foreach (DataRow _dr in _dsTemp.Tables["BAR_SPC"].Rows)
            {
                if (_dr["SPC_NO"].ToString() != "")
                {
                    _spcNo = _dr["SPC_NO"].ToString();
                }
                else
                    _spcNo = "Blank";
                if (_prdNo == _dr["PRD_NO"].ToString() && _dep == _dr["DEP"].ToString() && _spc == _dr["SPC"].ToString() && _bat_no == _dr["BAT_NO"].ToString())
                {
                    _drNew = _dtBarSpc.Rows[_dtBarSpc.Rows.Count - 1];
                    _drNew[_spcNo] = _dr["QTY_SPC"];
                }
                else
                {
                    _drNew = _dtBarSpc.NewRow();
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["DEP"] = _dr["DEP"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["QTY"] = _dr["QTY"];
                    _drNew["QTY1"] = _dr["QTY1"];
                    if (_dr["QTY1"].ToString() != "")
                        _qty1 = Convert.ToDecimal(_dr["QTY1"]);
                    else
                        _qty1 = 0;
                    _drNew["QTY_OK"] = _dr["QTY_OK"];
                    _drNew["QTY1_OK"] = _dr["QTY1_OK"];
                    _drNew["QTY_B"] = _dr["QTY_B"];
                    _drNew["QTY1_B"] = _dr["QTY1_B"];
                    _drNew["QTY_NG"] = _dr["QTY_NG"];
                    _drNew["QTY1_NG"] = _dr["QTY1_NG"];
                    if (_dr["QTY1_NG"].ToString() != "")
                        _qty1_ng = Convert.ToDecimal(_dr["QTY1_NG"]);
                    else
                        _qty1_ng = 0;
                    if (_qty1 == 0)
                    { }
                    else
                        _drNew["RATE"] = Math.Round(_qty1_ng / _qty1, 4);
                    _drNew[_spcNo] = _dr["QTY_SPC"];
                    _dtBarSpc.Rows.Add(_drNew);
                    _prdNo = _dr["PRD_NO"].ToString();
                    _dep = _dr["DEP"].ToString();
                    _bat_no = _dr["BAT_NO"].ToString();
                    _spc = _dr["SPC"].ToString();
                }
            } 

            _frmProcess.progressBar1.Value++;

            _ds.Merge(_dtBarSpc, false, MissingSchemaAction.AddWithKey);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables.Contains("PRDTSTAT_RPT"))
            {
                dataGridView1.DataMember = "PRDTSTAT_RPT";
                dataGridView1.DataSource = _ds;
                dataGridView1.Refresh();
            }
            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["PRD_NO"].HeaderText = "品号";
            dataGridView1.Columns["DEP"].HeaderText = "机台";
            dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            dataGridView1.Columns["SPC"].HeaderText = "规格";
            dataGridView1.Columns["QTY"].HeaderText = "生产支数";
            dataGridView1.Columns["QTY1"].HeaderText = "生产重量";
            dataGridView1.Columns["QTY_OK"].HeaderText = "OK支数";
            dataGridView1.Columns["QTY1_OK"].HeaderText = "OK重量";
            dataGridView1.Columns["QTY_B"].HeaderText = "B级品支数";
            dataGridView1.Columns["QTY1_B"].HeaderText = "B级品重量";
            dataGridView1.Columns["QTY_NG"].HeaderText = "NG支数";
            dataGridView1.Columns["QTY1_NG"].HeaderText = "NG重量";
            dataGridView1.Columns["RATE"].HeaderText = "不良率";
            dataGridView1.Columns["RATE"].DefaultCellStyle.Format = "p2";
            DataRow _dr;
            foreach (DataColumn _dc in _ds.Tables["PRDTSTAT_RPT"].Columns)
            {
                _dr = _dtSpc.Rows.Find(_dc.ColumnName);
                if (_dr != null)
                    dataGridView1.Columns[_dc.ColumnName].HeaderText = Strings.StrConv(_dr["NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
            }
            dataGridView1.Columns["Blank"].Visible = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                FileExport _fx = new FileExport();
                _fx._sourceDataSet = _ds;
                Dictionary<string, string> _dict = new Dictionary<string, string>();
                _dict["PRD_NO"] = "序列号";
                _dict["DEP"] = "机台";
                _dict["BAT_NO"] = "批号";
                _dict["SPC"] = "规格";
                _dict["QTY"] = "生产支数";
                _dict["QTY1"] = "生产重量";
                _dict["QTY_OK"] = "OK支数";
                _dict["QTY1_OK"] = "OK重量";
                _dict["QTY_B"] = "B级品支数";
                _dict["QTY1_B"] = "B级品重量";
                _dict["QTY_NG"] = "NG支数";
                _dict["QTY1_NG"] = "NG重量";
                _dict["RATE"] = "不良率";

                foreach (DataGridViewColumn _dc in dataGridView1.Columns)
                {
                    if (_dc.Name != "Blank" && _dc.Name != "PRD_NO" && _dc.Name != "DEP" && _dc.Name != "SPC" && _dc.Name != "BAT_NO" && _dc.Name != "QTY" && _dc.Name != "QTY1" && _dc.Name != "QTY_OK" && _dc.Name != "QTY1_OK" && _dc.Name != "QTY_B" && _dc.Name != "QTY1_B" && _dc.Name != "QTY_NG" && _dc.Name != "QTY1_NG" && _dc.Name != "RATE")
                    {
                        _dict[_dc.Name] = _dc.HeaderText;
                    }
                }
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

        private void RPTBCStat_Load(object sender, EventArgs e)
        {
            lstPrdtKnd.SelectedIndex = 0;

            DataTable _dt = new DataTable("PRDTSTAT_RPT");
            DataColumn _pkColumn = new DataColumn("PRD_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add(_pkColumn);
            DataColumn _pkColumn1 = new DataColumn("DEP", System.Type.GetType("System.String"));
            _dt.Columns.Add(_pkColumn1);
            DataColumn _pkColumn2 = new DataColumn("BAT_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add(_pkColumn2);
            DataColumn _pkColumn3 = new DataColumn("SPC", System.Type.GetType("System.String"));
            _dt.Columns.Add(_pkColumn3);

            _dt.Columns.Add("QTY", System.Type.GetType("System.String"));
            _dt.Columns.Add("QTY1", System.Type.GetType("System.String"));
            _dt.Columns.Add("QTY_OK", System.Type.GetType("System.String"));
            _dt.Columns.Add("QTY1_OK", System.Type.GetType("System.String"));
            _dt.Columns.Add("QTY_B", System.Type.GetType("System.String"));
            _dt.Columns.Add("QTY1_B", System.Type.GetType("System.String"));
            _dt.Columns.Add("QTY_NG", System.Type.GetType("System.String"));
            _dt.Columns.Add("QTY1_NG", System.Type.GetType("System.String"));
            _dt.Columns.Add("RATE", System.Type.GetType("System.Decimal"));
            _dt.PrimaryKey = new DataColumn[4] { _pkColumn, _pkColumn1, _pkColumn2, _pkColumn3 };
            _ds.Tables.Add(_dt);

            _dtBarSpc = _dt.Copy();

            txtDDB.Checked = false;
            txtDDE.Checked = false;
            lstDateType.SelectedIndex = 0;

            txtDDB.Text = System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00";
            txtDDE.Text = System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59";
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "PRD_NO")
            //{
            //    if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = true;
            //    else
            //        dataGridView1.Columns[e.ColumnIndex].Frozen = false;
            //}
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "DEP")
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

        #region 表头

        private void qrySpcNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "SPC_LST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNoB.Text = _qw.NO_RT;
            }
        }

        private void qrySpcNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "SPC_LST";//表名
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

        private void txtSpcNoE_Enter(object sender, EventArgs e)
        {
            if (txtSpcNoE.Text == "")
                txtSpcNoE.Text = txtSpcNoB.Text;
        }

        private void txtDepE_Enter(object sender, EventArgs e)
        {
            if (txtDepE.Text == "")
                txtDepE.Text = txtDepB.Text;
        }

        private void txtDDE_Enter(object sender, EventArgs e)
        {
            if (txtDDE.Text == "")
                txtDDE.Text = txtDDB.Text;
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
        #endregion
    }
}