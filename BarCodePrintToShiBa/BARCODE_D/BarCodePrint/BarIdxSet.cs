using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrint
{
    public partial class BarIdxSet : Form
    {
        public BarIdxSet()
        {
            InitializeComponent();
        }

        private void btn4Prdt_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNo.Text = _qw.NO_RT;

                #region 货品资料
                onlineService.BarPrintServer _brs = new BarCodePrint.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                this.txtLH.Text = _brs.GetIdxName(txtPrdNo.Text);
                #endregion
            }
        }

        private void txtPrdNo_Leave(object sender, EventArgs e)
        {
            if (txtPrdNo.Text.Length > 0)
            {
                onlineService.BarPrintServer _brs = new BarCodePrint.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    txtLH.Text = _ds.Tables[0].Rows[0]["LH"].ToString();
                }
                else
                {
                    MessageBox.Show(String.Format("品号[{0}]不存在！", txtPrdNo.Text));
                    txtPrdNo.Text = "";
                    txtLH.Text = "";
                    return;
                }
            }
            else
            {
                txtLH.Text = "";
            }
        }

        private void txtPrdNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtLH.Focus();
        }

        private void btn4IdxNo_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "INDX";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtIdxNo.Text = _qw.NO_RT;
                onlineService.BarPrintServer _bps = new BarCodePrint.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                this.txtPrtName.Text = _bps.GetIdxName(txtIdxNo.Text);
            }
        }

        private void txtIdxNo_Leave(object sender, EventArgs e)
        {
            if (txtIdxNo.Text.Length > 0)
            {
                onlineService.BarPrintServer _brs = new BarCodePrint.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                DataSet _ds = _brs.GetIndx(txtIdxNo.Text);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    this.txtPrtName.Text = _ds.Tables[0].Rows[0]["PRT_NAME"].ToString();
                }
                else
                {
                    MessageBox.Show(String.Format("中类代号[{0}]不存在！", txtIdxNo.Text));
                    txtIdxNo.Text = "";
                    txtPrtName.Text = "";
                    return;
                }
            }
            else
            {
                txtPrtName.Text = "";
            }
        }

        private void txtIdxNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtPrtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            onlineService.BarPrintServer _brs = new BarCodePrint.onlineService.BarPrintServer();
            _brs.UseDefaultCredentials = true;
            _brs.UpdateBarIdx(txtPrdNo.Text, txtLH.Text, txtIdxNo.Text, txtPrtName.Text);
            MessageBox.Show("保存成功！");
            txtPrdNo.Text = "";
            txtLH.Text = "";
            txtIdxNo.Text = "";
            txtPrtName.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}