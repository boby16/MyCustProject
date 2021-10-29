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
    public partial class RptSAStat : Form
    {
        DataSet _dsTmp = null;
        public RptSAStat()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.Show();
            _frmProcess.progressBar1.Value = 0;
            _frmProcess.progressBar1.Maximum = 5;

            StringBuilder _sb = new StringBuilder();
            _sb.Append(" SELECT ")
                .Append(" A.CUS_NO,D.SNM,A.TAX_ID,B.TAX,B.PS_ID,B.PRD_NO,B.QTY,B.QTY1,B.AMTN_NET,B.AMT,E.KND,E.UT")
                .Append(" FROM TF_PSS B")
                .Append(" Left Outer Join PRDT E On E.PRD_NO = B.PRD_NO,MF_PSS A")
                .Append(" Left Outer Join CUST D On D.CUS_NO=A.CUS_NO")
                .Append(" Where B.PS_ID In ('SD','SB','SA') AND B.PS_ID=A.PS_ID AND B.PS_NO=A.PS_NO")
                .Append(" And A.PS_DD >='" + txtSaDDB.Text + "' And A.PS_DD <='" + txtSaDDE.Text + "'");
            if (txtCusNoB.Text != "")
                _sb.Append(" AND A.CUS_NO>='" + txtCusNoB.Text + "'");
            if (txtCusNoE.Text != "")
                _sb.Append(" AND A.CUS_NO<='" + txtCusNoE.Text + "'");

            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;

            _frmProcess.progressBar1.Value++;

            DataSet _ds = _bps.GetSAStat(_sb.ToString());

            _frmProcess.progressBar1.Value++;

            _dsTmp = new DataSet();
            DataTable _dtTmp = new DataTable("SA_STAT");
            DataColumn _pkColumn = new DataColumn("CUS_NO", typeof(System.String));//客户代号
            _dtTmp.Columns.Add(_pkColumn);
            _dtTmp.Columns.Add("SNM", typeof(System.String));//客户简称
            _dtTmp.Columns.Add("SUM_SA_QTY", typeof(System.Decimal));//总销货数量
            _dtTmp.Columns.Add("SA_QTY", typeof(System.Decimal));//销货数量
            _dtTmp.Columns.Add("SA_QTY_RTN", typeof(System.Decimal));//退货数量
            _dtTmp.Columns.Add("AMT_SA", typeof(System.Decimal));//总销货金额
            _dtTmp.Columns.Add("AMTN_SA", typeof(System.Decimal));//总未税金额
            _dtTmp.Columns.Add("BL", typeof(System.Decimal));//比率
            _dtTmp.PrimaryKey = new DataColumn[1] { _pkColumn };
            _dsTmp.Merge(_dtTmp);

            DataRow[] _drArr;
            DataRow _drNew;

            decimal _qty = 0;//主单位数量
            decimal _qty1 = 0;//副单位数量
            decimal _sa_qty = 0;//销货数量
            decimal _sa_qty1 = 0;//退货数量
            decimal _sa_qty_sum = 0;//总销货数量

            decimal _amtn = 0;//未税金额
            decimal _amt = 0;//金额
            decimal _tax = 0;//税额
            decimal _amtn_sa = 0;//总未税金额
            decimal _amt_sa = 0;//总销货金额

            decimal _total_amtn = 0;
            decimal _total_amt = 0;

            foreach (DataRow _dr in _ds.Tables[0].Rows)
            {
                _drArr = _dsTmp.Tables[0].Select("CUS_NO='" + _dr["CUS_NO"].ToString() + "'");
                if (_drArr.Length > 0)
                {
                    _drNew = _drArr[0];
                }
                else
                {
                    _drNew = _dsTmp.Tables[0].NewRow();
                }
                _drNew["CUS_NO"] = _dr["CUS_NO"];
                _drNew["SNM"] = _dr["SNM"];


                _qty = 0;
                _qty1 = 0;
                _sa_qty = 0;
                _sa_qty1 = 0;
                _sa_qty_sum = 0;
                if (_dr["QTY"].ToString() != "")
                    _qty = Convert.ToDecimal(_dr["QTY"]);
                if (_dr["QTY1"].ToString() != "")
                    _qty1 = Convert.ToDecimal(_dr["QTY1"]);
                if (_drNew["SA_QTY"].ToString() != "")
                    _sa_qty = Convert.ToDecimal(_drNew["SA_QTY"]);
                if (_drNew["SA_QTY_RTN"].ToString() != "")
                    _sa_qty1 = Convert.ToDecimal(_drNew["SA_QTY_RTN"]);
                if (_drNew["SUM_SA_QTY"].ToString() != "")
                    _sa_qty_sum = Convert.ToDecimal(_drNew["SUM_SA_QTY"]);

                if (_dr["PS_ID"].ToString() == "SA")
                {
                    //if ((_dr["KND"].ToString() == "4" || _dr["PRD_NO"].ToString().Substring(0, 2) == "DG"))
                    if (_dr["UT"].ToString().ToUpper()=="KG")
                    {
                        _drNew["SA_QTY"] = _sa_qty + _qty;//原料和代号以‘DG’开头取：主单位数量
                        _drNew["SUM_SA_QTY"] = _sa_qty_sum + _qty;//总销货数量
                    }
                    else
                    {
                        _drNew["SA_QTY"] = _sa_qty + _qty1;//副单位数量
                        _drNew["SUM_SA_QTY"] = _sa_qty_sum + _qty1;
                    }
                }
                if (_dr["PS_ID"].ToString() == "SB")
                {
                    //if ((_dr["KND"].ToString() == "4" || _dr["PRD_NO"].ToString().Substring(0, 2) == "DG"))
                    if (_dr["UT"].ToString().ToUpper() == "KG")
                    {
                        _drNew["SA_QTY_RTN"] = _sa_qty1 + _qty;//原料和代号以‘DG’开头取：主单位数量
                        _drNew["SUM_SA_QTY"] = _sa_qty_sum - _qty;
                    }
                    else
                    {
                        _drNew["SA_QTY_RTN"] = _sa_qty1 + _qty1;//副单位数量
                        _drNew["SUM_SA_QTY"] = _sa_qty_sum - _qty1;
                    }
                }
                _amtn = 0;
                _amt = 0;
                _tax = 0;
                _amtn_sa = 0;
                _amt_sa = 0;
                if (_dr["AMTN_NET"].ToString() != "")
                    _amtn = Convert.ToDecimal(_dr["AMTN_NET"]);
                if (_dr["AMT"].ToString() != "")
                    _amt = Convert.ToDecimal(_dr["AMT"]);
                if (_dr["TAX"].ToString() != "")
                    _tax = Convert.ToDecimal(_dr["TAX"]);
                if (_drNew["AMTN_SA"].ToString() != "")
                    _amtn_sa = Convert.ToDecimal(_drNew["AMTN_SA"]);
                if (_drNew["AMT_SA"].ToString() != "")
                    _amt_sa = Convert.ToDecimal(_drNew["AMT_SA"]);

                if (_dr["PS_ID"].ToString() == "SA")
                {
                    _drNew["AMTN_SA"] = _amtn_sa + _amtn;
                    //if (_dr["TAX_ID"].ToString() == "3")
                    //{
                        if (_dr["TAX"].ToString() != "")
                            _drNew["AMT_SA"] = _amt_sa + _amtn + Convert.ToDecimal(_dr["TAX"]);
                        else
                            _drNew["AMT_SA"] = _amt_sa + _amtn;
                    //}
                    //else
                        //_drNew["AMT_SA"] = _amt_sa + _amt;
                    _total_amtn += _amtn;
                    _total_amt += _amt;
                }
                if (_dr["PS_ID"].ToString() == "SB")
                {
                    _drNew["AMTN_SA"] = _amtn_sa - _amtn;
                    //if (_dr["TAX_ID"].ToString() == "3")
                    //{
                        if (_dr["TAX"].ToString() != "")
                            _drNew["AMT_SA"] = _amt_sa - _amtn - Convert.ToDecimal(_dr["TAX"]);
                        else
                            _drNew["AMT_SA"] = _amt_sa - _amtn;
                    //}
                    //else
                    //    _drNew["AMT_SA"] = _amt_sa - _amt;
                    _total_amtn -= _amtn;
                    _total_amt -= _amt;
                }
                if (_drArr.Length <= 0)
                    _dsTmp.Tables[0].Rows.Add(_drNew);
            }

            _frmProcess.progressBar1.Value++;

            decimal _totalSaSum = 0;
            decimal _totalSa = 0;
            decimal _totalSb = 0;
            decimal _totalAtm = 0;
            foreach (DataRow _dr in _dsTmp.Tables[0].Rows)
            {
                if (_dr["AMTN_SA"].ToString() != "")
                    _dr["BL"] = Math.Round(Convert.ToDecimal(_dr["AMTN_SA"])*100 / _total_amtn,2);
                if (_dr["SUM_SA_QTY"].ToString() != "")
                    _totalSaSum += Convert.ToDecimal(_dr["SUM_SA_QTY"]);
                if (_dr["SA_QTY"].ToString() != "")
                    _totalSa += Convert.ToDecimal(_dr["SA_QTY"]);
                if (_dr["SA_QTY_RTN"].ToString() != "")
                    _totalSb += Convert.ToDecimal(_dr["SA_QTY_RTN"]);
                if (_dr["AMT_SA"].ToString() != "")
                    _totalAtm += Convert.ToDecimal(_dr["AMT_SA"]);
                _dr["SNM"] = Strings.StrConv(_dr["SNM"].ToString(), VbStrConv.SimplifiedChinese, 0);
            }

            _frmProcess.progressBar1.Value++;

            if (_ds != null && _ds.Tables.Count > 0)
            {
                dataGridView1.DataMember = "SA_STAT";
                dataGridView1.DataSource = _dsTmp;
                dataGridView1.Refresh();
            }
            txtTotalAmtn.Text = String.Format("{0:,#,##0.00}", _total_amtn);
            txtTotalSASum.Text = String.Format("{0:#,##0.00}", _totalSaSum);
            txtToTalSa.Text = String.Format("{0:#,##0.00}", _totalSa);
            txtTotalSb.Text = String.Format("{0:#,##0.00}", _totalSb); ;
            txtTotalAmt.Text = String.Format("{0:#,##0.00}", _totalAtm);

            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "zh-TW")
            dataGridView1.Columns["CUS_NO"].HeaderText = "客户代号";
            dataGridView1.Columns["SNM"].HeaderText = "客户简称";
            dataGridView1.Columns["SUM_SA_QTY"].HeaderText = "总销货数量(KG)";
            dataGridView1.Columns["SA_QTY"].HeaderText = "销货数量(KG)";
            dataGridView1.Columns["SA_QTY_RTN"].HeaderText = "退货数量(KG)";
            dataGridView1.Columns["AMT_SA"].HeaderText = "总销货金额";
            dataGridView1.Columns["AMTN_SA"].HeaderText = "总未税金额";
            dataGridView1.Columns["BL"].HeaderText = "比率";

            dataGridView1.Columns["SUM_SA_QTY"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["SUM_SA_QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns["SA_QTY"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["SA_QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["SA_QTY_RTN"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["SA_QTY_RTN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["AMT_SA"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["AMT_SA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["AMTN_SA"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["AMTN_SA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["BL"].DefaultCellStyle.Format = "#,##0.00";
            dataGridView1.Columns["BL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_dsTmp != null && _dsTmp.Tables.Count > 0 && _dsTmp.Tables[0].Rows.Count > 0)
            {
                FileExport _fx = new FileExport();
                _fx._sourceDataSet = _dsTmp;
                Dictionary<string, string> _dict = new Dictionary<string,string>();
                _dict["CUS_NO"] = "客户代号";
                _dict["SNM"] = "客户简称";
                _dict["SUM_SA_QTY"] = "总销货数量(KG)";
                _dict["SA_QTY"] = "销货数量(KG)";
                _dict["SA_QTY_RTN"] = "退货数量(KG)";
                _dict["AMT_SA"] = "总销货金额";
                _dict["AMTN_SA"] = "总未税金额";
                _dict["BL"] = "比率";
                _fx._columnLst = _dict;
                _fx.ShowDialog();
            }
        }

        private void qryCusNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "CUST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtCusNoB.Text = _qw.NO_RT;
            }
        }

        private void qryCusNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "CUST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtCusNoE.Text = _qw.NO_RT;
            }
        }

        private void txtCusNoE_Enter(object sender, EventArgs e)
        {
            if (txtCusNoE.Text == "")
                txtCusNoE.Text = txtCusNoB.Text;
        }
    }
}