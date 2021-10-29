using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BarSQNOMng : Form
    {
        public BarSQNOMng()
        {
            InitializeComponent();
        }

        #region 设置当前流水号
        private void GetPrintSN()
        {
            //流水号总长固定3位
            int _SNMax = 0;
            int SNLength = 0;
            StringBuilder _sn = new StringBuilder();

            string _prd_no = txtPrdNo.Text;
            string _bat_no = txtBatNo.Text;
            if (!String.IsNullOrEmpty(_prd_no) && !String.IsNullOrEmpty(_bat_no))
            {
                //取流水号
                onlineService.BarPrintServer _brSer = new BarCodePrintTSB.onlineService.BarPrintServer();
                _brSer.UseDefaultCredentials = true;
                _SNMax = Convert.ToInt32(_brSer.GetMaxSN(_prd_no, _bat_no));
            }

            SNLength = _SNMax.ToString().Length;
            for (int i = 0; i < 3 - SNLength; i++)
            {
                _sn.Insert(0, "0");
            }
            txtSN.Text = _sn.ToString() + _SNMax.ToString();
        }
        #endregion

        private void btn4Prdt_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNo.Text = _qw.NO_RT;

                #region 货品资料
                onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text.Trim(), "");
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        txtSpc.Text = _ds.Tables[0].Rows[0]["SPC"].ToString();
                        txtIdx1.Text = _ds.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    }
                }
                #endregion

                GetPrintSN();
            }
        }

        private void btn4BatNo_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//表名
            _qw.SQLWhere = " AND LEN(BAT_NO)=7 ";
            if (txtPrdNo.Text != "")
                _qw.SQLWhere += " AND BAT_NO IN (SELECT PRD_MARK FROM BAR_SQNO WHERE PRD_NO='" + txtPrdNo.Text + "') ";
            else
                _qw.SQLWhere += " AND BAT_NO IN (SELECT PRD_MARK FROM BAR_SQNO) ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNo.Text = _qw.NO_RT;
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                DataSet _dsBat = _bps.GetBatNo2("AND A.BAT_NO='" + txtBatNo.Text + "'");
                if (_dsBat != null && _dsBat.Tables.Count > 0 && _dsBat.Tables[0].Rows.Count > 0)
                {
                    if (txtPrdNo.Text != _dsBat.Tables[0].Rows[0]["PRD_NO"].ToString())
                    {
                        if (txtPrdNo.Text != "")
                        {
                            if (MessageBox.Show("此批号的货品[" + _dsBat.Tables[0].Rows[0]["PRD_NO"].ToString() + "]与输入的品号不同，是否覆盖？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                txtPrdNo.Text = _dsBat.Tables[0].Rows[0]["PRD_NO"].ToString();
                                txtSpc.Text = _dsBat.Tables[0].Rows[0]["SPC"].ToString();
                            }
                            else
                                txtBatNo.Text = "";
                        }
                        else
                        {
                            txtPrdNo.Text = _dsBat.Tables[0].Rows[0]["PRD_NO"].ToString();
                            txtSpc.Text = _dsBat.Tables[0].Rows[0]["SPC"].ToString();
                        }
                    }
                }
                GetPrintSN();
            }
        }

        private void txtPrdNo_Leave(object sender, EventArgs e)
        {
            if (txtPrdNo.Text.Length > 0)
            {
                onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text,"");
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    txtSpc.Text = _ds.Tables[0].Rows[0]["SPC"].ToString();
                    txtIdx1.Text = _ds.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    GetPrintSN();
                }
                else
                {
                    MessageBox.Show(String.Format("品号[{0}]不存在！", txtPrdNo.Text));
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    txtIdx1.Text = "";
                    return;
                }
            }
            else
            {
                txtSpc.Text = "";
                txtIdx1.Text = "";
            }
        }

        private void txtPrdNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtBatNo.Focus();
        }

        private void txtBatNo_Leave(object sender, EventArgs e)
        {
            if (txtBatNo.Text != "")
            {
                if (txtBatNo.Text.Length != 7)
                {
                    MessageBox.Show("批号输入不正确！");
                    txtBatNo.Text = "";
                }
                else
                {
                    GetPrintSN();
                }
            }
        }

        private void txtBatNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtSN.Focus();
        }
    }
}