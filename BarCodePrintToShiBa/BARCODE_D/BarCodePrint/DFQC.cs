using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class DFQC : Form
    {
        private DataSet _dsUnQC;//未检验半成品条码
        private DataSet _dsEligibleA;//合格A类条码
        private DataSet _dsEligibleB;//合格B类条码
        private DataSet _dsReject;//不合格条码
        private string _spcNameTem = "";

        public DFQC()
        {
            InitializeComponent();
        }

        #region 转异常通知单
        private void btnToTR_Click(object sender, EventArgs e)
        {
            if (lstReject.Items.Count <= 0)
            {
                MessageBox.Show("不合格半成品条码为空！");
                return;
            }
            else
            {
                BarToTR _toTR = new BarToTR();
                _toTR.InitDS = _dsReject;
                if (_toTR.ShowDialog() == DialogResult.OK)
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    _bps.UpdataDataQC(_dsReject);

                    _dsReject.Tables[0].Clear();
                    _dsReject.AcceptChanges();
                    lstReject.DataSource = _dsReject.Tables[0];
                    lstReject.DisplayMember = "BAR_NO_DIS";
                    lstReject.ValueMember = "BAR_NO";
                    txtRejectRem.Text = "";
                    txtSpcName.Text = "";
                    txtSpcNo.Text = "";
                }
            }
        }
        #endregion

        #region 合格A类
        //选中
        private void btnEligibleA_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnQC.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnQC.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnQC.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsEligibleA.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrE)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsEligibleA.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = "A";//合格
                    _drNew["PRC_ID"] = DBNull.Value;

                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _dsEligibleA.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnQC.AcceptChanges();
                _dsEligibleA.AcceptChanges();

                lstUnQC.DataSource = _dsUnQC.Tables[0];
                lstUnQC.DisplayMember = "BAR_NO_DIS";
                lstUnQC.ValueMember = "BAR_NO";

                lstEligibleA.DataSource = _dsEligibleA.Tables[0];
                lstEligibleA.DisplayMember = "BAR_NO_DIS";
                lstEligibleA.ValueMember = "BAR_NO";

                for (int i = 0; i < lstEligibleA.Items.Count; i++)
                {
                    lstEligibleA.SetSelected(i, true);
                }
            }
        }
        //取消
        private void btnUnEligibleA_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstEligibleA.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstEligibleA.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnQC.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsEligibleA.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnQC.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];

                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _dsUnQC.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnQC.AcceptChanges();
                _dsEligibleA.AcceptChanges();

                lstUnQC.DataSource = _dsUnQC.Tables[0];
                lstUnQC.DisplayMember = "BAR_NO_DIS";
                lstUnQC.ValueMember = "BAR_NO";

                lstEligibleA.DataSource = _dsEligibleA.Tables[0];
                lstEligibleA.DisplayMember = "BAR_NO_DIS";
                lstEligibleA.ValueMember = "BAR_NO";
            }
        }

        private void lstEligibleA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEligibleA.SelectedIndex >= 0)
            {
                string _barNo = lstEligibleA.SelectedValue.ToString();
                DataRow _dr = _dsEligibleA.Tables[0].Rows.Find(_barNo);
                if (_dr != null)
                    txtEligibleRemA.Text = _dr["REM"].ToString();
            }
        }

        private void txtEligibleRemA_Leave(object sender, EventArgs e)
        {
            if (lstEligibleA.SelectedIndex >= 0)
            {
                DataRowView _drv;
                string _selectNo = "";
                for (int i = 0; i < lstEligibleA.SelectedItems.Count; i++)
                {
                    _drv = (DataRowView)lstEligibleA.SelectedItems[i];
                    if (_selectNo != "")
                        _selectNo += ",";
                    _selectNo += "'" + _drv["BAR_NO"] + "'";
                }
                if (_selectNo != "")
                {
                    DataRow[] _drArr = _dsEligibleA.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                    foreach (DataRow _dr in _drArr)
                    {
                        _dr["REM"] = txtEligibleRemA.Text;
                    }
                }
                //_dsEligibleA.AcceptChanges();
            }
        }

        private void lstEligibleA_DoubleClick(object sender, EventArgs e)
        {
            btnUnEligibleA_Click(sender, e);
        }

        private void btnOKA_Click(object sender, EventArgs e)
        {
            _dsEligibleA.AcceptChanges();
            foreach (DataRow _dr in _dsEligibleA.Tables[0].Rows)
            {
                _dr["STATUS"] = "QC";
                _dr["REM"] = BarRole.ConvertToDBLanguage(_dr["REM"].ToString());
                _dr["USR_QC"] = BarRole.USR_NO;
                _dr["QC_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _bps.UpdataDataQC(_dsEligibleA);

            txtEligibleRemA.Text = "";

            _dsEligibleA.Clear();
            _dsEligibleA.AcceptChanges();
            lstEligibleA.DataSource = _dsEligibleA.Tables[0];
            lstEligibleA.DisplayMember = "BAR_NO_DIS";
            lstEligibleA.ValueMember = "BAR_NO";
        }

        #endregion

        #region 合格B类
        private void btnEligibleB_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnQC.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnQC.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnQC.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsEligibleB.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrE)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsEligibleB.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = "B";//合格
                    _drNew["PRC_ID"] = DBNull.Value;

                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["SPC_NO"] = _dr["SPC_NO"];
                    _drNew["SPC_NAME"] = _dr["SPC_NAME"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _dsEligibleB.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnQC.AcceptChanges();
                _dsEligibleB.AcceptChanges();

                lstUnQC.DataSource = _dsUnQC.Tables[0];
                lstUnQC.DisplayMember = "BAR_NO_DIS";
                lstUnQC.ValueMember = "BAR_NO";

                lstEligibleB.DataSource = _dsEligibleB.Tables[0];
                lstEligibleB.DisplayMember = "BAR_NO_DIS";
                lstEligibleB.ValueMember = "BAR_NO";

                for (int i = 0; i < lstEligibleB.Items.Count; i++)
                {
                    lstEligibleB.SetSelected(i, true);
                }
            }
        }

        private void btnUnEligibleB_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstEligibleB.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstEligibleB.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnQC.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsEligibleB.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnQC.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];

                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["SPC_NO"] = DBNull.Value;
                    _drNew["SPC_NAME"] = DBNull.Value;
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _dsUnQC.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnQC.AcceptChanges();
                _dsEligibleB.AcceptChanges();

                lstUnQC.DataSource = _dsUnQC.Tables[0];
                lstUnQC.DisplayMember = "BAR_NO_DIS";
                lstUnQC.ValueMember = "BAR_NO";

                lstEligibleB.DataSource = _dsEligibleB.Tables[0];
                lstEligibleB.DisplayMember = "BAR_NO_DIS";
                lstEligibleB.ValueMember = "BAR_NO";
            }
        }

        private void lstEligibleB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEligibleB.SelectedIndex >= 0)
            {
                string _barNo = lstEligibleB.SelectedValue.ToString();
                DataRow _dr = _dsEligibleB.Tables[0].Rows.Find(_barNo);
                if (_dr != null)
                {
                    txtEligibleRemB.Text = _dr["REM"].ToString();
                    txtSpcNoB.Text = _dr["SPC_NO"].ToString();
                    txtSpcNameB.Text = _dr["SPC_NAME"].ToString();
                }
            }
        }

        private void txtEligibleRemB_Leave(object sender, EventArgs e)
        {
            if (lstEligibleB.SelectedIndex >= 0)
            {
                DataRowView _drv;
                string _selectNo = "";
                for (int i = 0; i < lstEligibleB.SelectedItems.Count; i++)
                {
                    _drv = (DataRowView)lstEligibleB.SelectedItems[i];
                    if (_selectNo != "")
                        _selectNo += ",";
                    _selectNo += "'" + _drv["BAR_NO"] + "'";
                }
                if (_selectNo != "")
                {
                    DataRow[] _drArr = _dsEligibleB.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                    foreach (DataRow _dr in _drArr)
                    {
                        _dr["REM"] = txtEligibleRemB.Text;
                    }
                }
                //_dsEligibleB.AcceptChanges();
            }
        }

        #region B类原因
        private void txtSpcNameB_Enter(object sender, EventArgs e)
        {
            _spcNameTem = txtSpcNameB.Text;
            txtSpcNameB.Text = txtSpcNoB.Text;
        }

        private void txtSpcNameB_Leave(object sender, EventArgs e)
        {
            string _spcNo = txtSpcNameB.Text;
            string _spcName = _spcNameTem;
            if (txtSpcNameB.Text != txtSpcNoB.Text)
            {
                if (txtSpcNameB.Text != "")
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    DataSet _dsSpc = _bps.GetSpcLst(txtSpcNameB.Text);
                    if (_dsSpc != null && _dsSpc.Tables.Count > 0 && _dsSpc.Tables[0].Rows.Count > 0)
                    {
                        _spcNo = _dsSpc.Tables[0].Rows[0]["SPC_NO"].ToString();
                        _spcName = _dsSpc.Tables[0].Rows[0]["NAME"].ToString();
                    }
                    else
                    {
                        _spcNo = "";
                        _spcName = "";
                        MessageBox.Show("代号不存在！");
                    }
                }
                else
                {
                    _spcNo = "";
                    _spcName = "";
                }
            }
            else
            {
                _spcName = _spcNameTem;
            }
            _spcNameTem = "";

            if (lstEligibleB.SelectedIndex >= 0)
            {
                DataRowView _drv;
                string _selectNo = "";
                for (int i = 0; i < lstEligibleB.SelectedItems.Count; i++)
                {
                    _drv = (DataRowView)lstEligibleB.SelectedItems[i];
                    if (_selectNo != "")
                        _selectNo += ",";
                    _selectNo += "'" + _drv["BAR_NO"] + "'";
                }
                if (_selectNo != "")
                {
                    DataRow[] _drArr = _dsEligibleB.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                    foreach (DataRow _dr in _drArr)
                    {
                        _dr["SPC_NO"] = _spcNo;
                        _dr["SPC_NAME"] = _spcName;
                    }
                }
            }
            //_dsEligibleB.AcceptChanges();
            txtSpcNoB.Text = _spcNo;
            txtSpcNameB.Text = _spcName;
        }

        private void btnSpcNoB_Click(object sender, EventArgs e)
        {
            string _spcNo = txtSpcNoB.Text;
            string _spcName = txtSpcNameB.Text;
            QueryWin _qw = new QueryWin();
            _qw.PGM = "SPC_LST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                _spcNo = _qw.NO_RT;
                _spcName = _qw.NAME_RTN;

                if (lstEligibleB.SelectedIndex >= 0)
                {
                    DataRowView _drv;
                    string _selectNo = "";
                    for (int i = 0; i < lstEligibleB.SelectedItems.Count; i++)
                    {
                        _drv = (DataRowView)lstEligibleB.SelectedItems[i];
                        if (_selectNo != "")
                            _selectNo += ",";
                        _selectNo += "'" + _drv["BAR_NO"] + "'";
                    }
                    if (_selectNo != "")
                    {
                        DataRow[] _drArr = _dsEligibleB.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                        foreach (DataRow _dr in _drArr)
                        {
                            _dr["SPC_NO"] = _spcNo;
                            _dr["SPC_NAME"] = _spcName;
                        }
                    }
                }
                //_dsEligibleB.AcceptChanges();
                txtSpcNoB.Text = _spcNo;
                txtSpcNameB.Text = _spcName;
            }
        }
        #endregion

        private void lstEligibleB_DoubleClick(object sender, EventArgs e)
        {
            btnUnEligibleB_Click(sender, e);
        }

        private void btnOKB_Click(object sender, EventArgs e)
        {
            _dsEligibleB.AcceptChanges();
            bool _hasError = false;
            foreach (DataRow _dr in _dsEligibleB.Tables[0].Rows)
            {
                _dr["STATUS"] = "QC";
                _dr["REM"] = BarRole.ConvertToDBLanguage(_dr["REM"].ToString());
                _dr["USR_QC"] = BarRole.USR_NO;
                _dr["QC_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (_dr["SPC_NO"].ToString() == "")
                {
                    _hasError = true;
                }
            }
            if (_hasError)
            {
                MessageBox.Show("原因不能为空！");
                return;
            }
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _bps.UpdataDataQC(_dsEligibleB);

            txtEligibleRemB.Text = "";
            txtSpcNoB.Text = "";
            txtSpcNameB.Text = "";

            _dsEligibleB.Clear();
            _dsEligibleB.AcceptChanges();
            lstEligibleB.DataSource = _dsEligibleB.Tables[0];
            lstEligibleB.DisplayMember = "BAR_NO_DIS";
            lstEligibleB.ValueMember = "BAR_NO";
        }

        #endregion

        #region 不合格
        //选中
        private void btnReject_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnQC.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnQC.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnQC.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrR = _dsReject.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrR)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsReject.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = "2";//不合格
                    //PRC_ID:  1:强制缴库,2:报废　4:重开制令
                    //lstPrcId.SelectedIndex：0:报废；1:重开制令；2:强制缴库
                    int _prcId = 2;
                    if (lstPrcId.SelectedIndex == 1)
                        _prcId = 4;
                    else if (lstPrcId.SelectedIndex == 2)
                        _prcId = 1;
                    else
                        _prcId = 2;
                    _drNew["PRC_ID"] = _prcId;

                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["SPC_NO"] = _dr["SPC_NO"];
                    _drNew["SPC_NAME"] = _dr["SPC_NAME"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _dsReject.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnQC.AcceptChanges();
                _dsReject.AcceptChanges();

                lstUnQC.DataSource = _dsUnQC.Tables[0];
                lstUnQC.DisplayMember = "BAR_NO_DIS";
                lstUnQC.ValueMember = "BAR_NO";

                lstReject.DataSource = _dsReject.Tables[0];
                lstReject.DisplayMember = "BAR_NO_DIS";
                lstReject.ValueMember = "BAR_NO";

                for (int i = 0; i < lstReject.Items.Count; i++)
                {
                    lstReject.SetSelected(i, true);
                }
                
            }
        }

        //取消
        private void btnUnReject_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstReject.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstReject.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnQC.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrR = _dsReject.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrR)
                {
                    _drNew = _dsUnQC.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];

                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["SPC_NO"] = DBNull.Value;
                    _drNew["SPC_NAME"] = DBNull.Value;
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _dsUnQC.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnQC.AcceptChanges();
                _dsReject.AcceptChanges();

                lstUnQC.DataSource = _dsUnQC.Tables[0];
                lstUnQC.DisplayMember = "BAR_NO_DIS";
                lstUnQC.ValueMember = "BAR_NO";

                lstReject.DataSource = _dsReject.Tables[0];
                lstReject.DisplayMember = "BAR_NO_DIS";
                lstReject.ValueMember = "BAR_NO";
            }
        }

        private void lstReject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReject.SelectedIndex >= 0)
            {
                string _barNo = lstReject.SelectedValue.ToString();
                DataRow _dr = _dsReject.Tables[0].Rows.Find(_barNo);
                if (_dr != null)
                {
                    txtRejectRem.Text = _dr["REM"].ToString();
                    txtSpcNo.Text = _dr["SPC_NO"].ToString();
                    txtSpcName.Text = _dr["SPC_NAME"].ToString();

                    lstPrcId.SelectedIndex = 0;//报废
                    if (_dr["PRC_ID"].ToString() != "")
                    {
                        //PRC_ID:  1:强制缴库,2:报废　4:重开制令
                        //lstPrcId.SelectedIndex：0:报废；1:重开制令；2:强制缴库
                        if (_dr["PRC_ID"].ToString() == "4")
                            lstPrcId.SelectedIndex = 1;//重开制令
                        if (_dr["PRC_ID"].ToString() == "1")
                            lstPrcId.SelectedIndex = 2;//强制缴库
                    }
                }
            }
        }

        private void txtRejectRem_Leave(object sender, EventArgs e)
        {
            if (lstReject.SelectedIndex >= 0)
            {
                DataRowView _drv;
                string _selectNo = "";
                for (int i = 0; i < lstReject.SelectedItems.Count; i++)
                {
                    _drv = (DataRowView)lstReject.SelectedItems[i];
                    if (_selectNo != "")
                        _selectNo += ",";
                    _selectNo += "'" + _drv["BAR_NO"] + "'";
                }
                if (_selectNo != "")
                {
                    DataRow[] _drArr = _dsReject.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                    foreach (DataRow _dr in _drArr)
                    {
                        _dr["REM"] = txtRejectRem.Text;
                    }
                }
                //_dsReject.AcceptChanges();
            }
        }

        #region 报废原因
        private void btnSpcNo_Click(object sender, EventArgs e)
        {
            string _spcNo = txtSpcNo.Text;
            string _spcName = txtSpcName.Text;
            QueryWin _qw = new QueryWin();
            _qw.PGM = "SPC_LST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                _spcNo = _qw.NO_RT;
                _spcName = _qw.NAME_RTN;

                if (lstReject.SelectedIndex >= 0)
                {
                    DataRowView _drv;
                    string _selectNo = "";
                    for (int i = 0; i < lstReject.SelectedItems.Count; i++)
                    {
                        _drv = (DataRowView)lstReject.SelectedItems[i];
                        if (_selectNo != "")
                            _selectNo += ",";
                        _selectNo += "'" + _drv["BAR_NO"] + "'";
                    }
                    if (_selectNo != "")
                    {
                        DataRow[] _drArr = _dsReject.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                        foreach (DataRow _dr in _drArr)
                        {
                            _dr["SPC_NO"] = _spcNo;
                            _dr["SPC_NAME"] = _spcName;
                        }
                    }
                }
                //_dsReject.AcceptChanges();
                txtSpcNo.Text = _spcNo;
                txtSpcName.Text = _spcName;
            }
        }

        private void txtSpcName_Enter(object sender, EventArgs e)
        {
            _spcNameTem = txtSpcName.Text;
            txtSpcName.Text = txtSpcNo.Text;
        }

        private void txtSpcName_Leave(object sender, EventArgs e)
        {
            string _spcNo = txtSpcName.Text;
            string _spcName = _spcNameTem;
            if (txtSpcName.Text != txtSpcNo.Text)
            {
                if (txtSpcName.Text != "")
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    DataSet _dsSpc = _bps.GetSpcLst(txtSpcName.Text);
                    if (_dsSpc != null && _dsSpc.Tables.Count > 0 && _dsSpc.Tables[0].Rows.Count > 0)
                    {
                        _spcNo = _dsSpc.Tables[0].Rows[0]["SPC_NO"].ToString();
                        _spcName = _dsSpc.Tables[0].Rows[0]["NAME"].ToString();
                    }
                    else
                    {
                        _spcNo = "";
                        _spcName = "";
                        MessageBox.Show("代号不存在！");
                    }
                }
                else
                {
                    _spcNo = "";
                    _spcName = "";
                }
            }
            else
            {
                _spcName = _spcNameTem;
            }
            _spcNameTem = "";

            if (lstReject.SelectedIndex >= 0)
            {
                DataRowView _drv;
                string _selectNo = "";
                for (int i = 0; i < lstReject.SelectedItems.Count; i++)
                {
                    _drv = (DataRowView)lstReject.SelectedItems[i];
                    if (_selectNo != "")
                        _selectNo += ",";
                    _selectNo += "'" + _drv["BAR_NO"] + "'";
                }
                if (_selectNo != "")
                {
                    DataRow[] _drArr = _dsReject.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                    foreach (DataRow _dr in _drArr)
                    {
                        _dr["SPC_NO"] = _spcNo;
                        _dr["SPC_NAME"] = _spcName;
                    }
                }
            }
            //_dsReject.AcceptChanges();
            txtSpcNo.Text = _spcNo;
            txtSpcName.Text = _spcName;
        }
        #endregion

        private void lstReject_DoubleClick(object sender, EventArgs e)
        {
            btnUnReject_Click(sender, e);
        }

        private void txtRejectRem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ';')
                e.KeyChar = (char)Keys.None;
        }

        private void lstPrcId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (lstReject.SelectedIndex >= 0)
            {
                DataRowView _drv;
                string _selectNo = "";
                for (int i = 0; i < lstReject.SelectedItems.Count; i++)
                {
                    _drv = (DataRowView)lstReject.SelectedItems[i];
                    if (_selectNo != "")
                        _selectNo += ",";
                    _selectNo += "'" + _drv["BAR_NO"] + "'";
                }
                if (_selectNo != "")
                {
                    DataRow[] _drArr = _dsReject.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                    foreach (DataRow _dr in _drArr)
                    {
                        //PRC_ID:  1:强制缴库,2:报废　4:重开制令
                        //lstPrcId.SelectedIndex：0:报废；1:重开制令；2:强制缴库
                        int _prcId = 2;
                        if (lstPrcId.SelectedIndex == 1)
                            _prcId = 4;
                        else if (lstPrcId.SelectedIndex == 2)
                            _prcId = 1;
                        else
                            _prcId = 2;
                        _dr["PRC_ID"] = _prcId;
                    }
                }
                //_dsReject.AcceptChanges();
            }
        }

        private void btnOk1_Click(object sender, EventArgs e)
        {
            _dsReject.AcceptChanges();
            bool _hasError = false;
            foreach (DataRow _dr in _dsReject.Tables[0].Rows)
            {
                _dr["STATUS"] = "QC";
                _dr["REM"] = BarRole.ConvertToDBLanguage(_dr["REM"].ToString());
                _dr["USR_QC"] = BarRole.USR_NO;
                _dr["QC_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (_dr["SPC_NO"].ToString() == "")
                {
                    _hasError = true;
                }
            }
            if (_hasError)
            {
                MessageBox.Show("原因不能为空！");
                return;
            }
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _bps.UpdataDataQC(_dsReject);

            txtRejectRem.Text = "";
            txtSpcNo.Text = "";
            txtSpcName.Text = "";
            lstPrcId.SelectedIndex = 0;

            _dsReject.Clear();
            _dsReject.AcceptChanges();
            lstReject.DataSource = _dsReject.Tables[0];
            lstReject.DisplayMember = "BAR_NO_DIS";
            lstReject.ValueMember = "BAR_NO";
        }

        #endregion

        #region page_load
        private void DFQC_Load(object sender, EventArgs e)
        {
            InitDataSet();
        }

        private void InitDataSet()
        {
            if (_dsEligibleA == null)
            {
                _dsEligibleA = new DataSet();
                DataTable _dtEligibleA = new DataTable("BAR_ELIGIBLE");
                DataColumn _pkColumn = new DataColumn("BAR_NO", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add(_pkColumn);
                _dtEligibleA.Columns.Add("BAR_NO_DIS", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("QC", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("STATUS", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("BIL_ID", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("BIL_NO", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("REM", System.Type.GetType("System.String"));

                _dtEligibleA.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("PRD_NAME", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("SPC", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("SPC_NO", System.Type.GetType("System.String"));//报废原因代号
                _dtEligibleA.Columns.Add("SPC_NAME", System.Type.GetType("System.String"));//报废原因
                _dtEligibleA.Columns.Add("BAT_NO", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("PRC_ID", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("USR_QC", System.Type.GetType("System.String"));
                _dtEligibleA.Columns.Add("QC_DATE", System.Type.GetType("System.DateTime"));
                _dtEligibleA.PrimaryKey = new DataColumn[1] { _pkColumn };
                _dsEligibleA.Tables.Add(_dtEligibleA);
            }
            if (_dsEligibleB == null)
            {
                _dsEligibleB = new DataSet();
                DataTable _dtEligibleB = new DataTable("BAR_ELIGIBLEB");
                DataColumn _pkColumn = new DataColumn("BAR_NO", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add(_pkColumn);
                _dtEligibleB.Columns.Add("BAR_NO_DIS", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("QC", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("STATUS", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("BIL_ID", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("BIL_NO", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("REM", System.Type.GetType("System.String"));

                _dtEligibleB.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("PRD_NAME", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("SPC", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("SPC_NO", System.Type.GetType("System.String"));//报废原因代号
                _dtEligibleB.Columns.Add("SPC_NAME", System.Type.GetType("System.String"));//报废原因
                _dtEligibleB.Columns.Add("BAT_NO", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("PRC_ID", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("USR_QC", System.Type.GetType("System.String"));
                _dtEligibleB.Columns.Add("QC_DATE", System.Type.GetType("System.DateTime"));
                _dtEligibleB.PrimaryKey = new DataColumn[1] { _pkColumn };
                _dsEligibleB.Tables.Add(_dtEligibleB);
            }
            if (_dsReject == null)
            {
                _dsReject = new DataSet();
                DataTable _dtReject = new DataTable("BAR_REJECT");
                DataColumn _pkColumn = new DataColumn("BAR_NO", System.Type.GetType("System.String"));
                _dtReject.Columns.Add(_pkColumn);
                _dtReject.Columns.Add("BAR_NO_DIS", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("QC", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("STATUS", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("BIL_ID", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("BIL_NO", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("REM", System.Type.GetType("System.String"));

                _dtReject.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("PRD_NAME", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("SPC", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("SPC_NO", System.Type.GetType("System.String"));//报废原因代号
                _dtReject.Columns.Add("SPC_NAME", System.Type.GetType("System.String"));//报废原因
                _dtReject.Columns.Add("BAT_NO", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("PRC_ID", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("USR_QC", System.Type.GetType("System.String"));
                _dtReject.Columns.Add("QC_DATE", System.Type.GetType("System.DateTime"));
                _dtReject.PrimaryKey = new DataColumn[1] { _pkColumn };
                _dsReject.Tables.Add(_dtReject);
            }
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _dsUnQC = new DataSet();
            _dsUnQC = _bps.GetBarUnQCLst("2");//半成品条码

            //未检验的条码
            lstUnQC.DataSource = _dsUnQC.Tables[0];
            lstUnQC.DisplayMember = "BAR_NO_DIS";
            lstUnQC.ValueMember = "BAR_NO";
            lstUnQC.Refresh();


            //已检验未缴库或未转异常的条码
            lstUnMM.DataSource = _dsUnQC.Tables[1];
            lstUnMM.DisplayMember = "BAR_NO_DIS";
            lstUnMM.ValueMember = "BAR_NO";
            lstUnMM.Refresh();

            lstPrcId.SelectedIndex = 0;
        }
        #endregion

        #region 条码扫描
        private void txtBarNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == BarRole.EndChar)
            {
                this.txtBarNo_Leave(sender, e);
            }
        }

        private void txtBarNo_Leave(object sender, EventArgs e)
        {
            DataSet _dsTemp = new DataSet();
            string _barNo = txtBarNo.Text.Trim();
            if (!String.IsNullOrEmpty(_barNo))
            {
                try
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    _dsTemp = _bps.GetBarQC(_barNo,"2");
                    if (_dsTemp != null && _dsTemp.Tables.Count > 0 && _dsTemp.Tables[0].Rows.Count > 0)
                    {
                        if (_dsUnQC != null && _dsUnQC.Tables.Count > 0)
                        {
                            DataRow[] _drArr = _dsUnQC.Tables[1].Select("BAR_NO='" + _barNo + "'");
                            if (_drArr.Length > 0)
                                _drArr[0].Delete();
                            try
                            {
                                _dsUnQC.Tables[0].ImportRow(_dsTemp.Tables[0].Rows[0]);
                            }
                            catch
                            {
                                txtBarNo.Text = "";
                                txtBarNo.Focus();
                            }
                        }
                        else
                            _dsUnQC.Merge(_dsTemp);
                        _bps.UndoBarQC(_barNo);
                    }
                    else
                    {
                        MessageBox.Show("不能修改此序列号品质信息！");
                    }
                    if (_dsUnQC != null && _dsUnQC.Tables.Count > 0)
                    {
                        _dsUnQC.AcceptChanges();

                        //未检验的条码
                        lstUnQC.DataSource = _dsUnQC.Tables[0];
                        lstUnQC.DisplayMember = "BAR_NO_DIS";
                        lstUnQC.ValueMember = "BAR_NO";
                        lstUnQC.Refresh();

                        //已检验未缴库的条码
                        lstUnMM.DataSource = _dsUnQC.Tables[1];
                        lstUnMM.DisplayMember = "BAR_NO_DIS";
                        lstUnMM.ValueMember = "BAR_NO";
                        lstUnMM.Refresh();
                    }

                    txtBarNo.Text = "";
                    txtBarNo.Focus();
                }
                catch (Exception _ex)
                {
                    string _err = "";
                    if (_ex.Message.Length > 500)
                        _err = _ex.Message.Substring(0, 500);
                    else
                        _err = _ex.Message;
                    MessageBox.Show("不能修改此序列号品质信息！原因：\n" + _err);
                    return;
                }
            }
        }
        #endregion

        #region 删除未检条码
        private void lstUnQC_KeyDown(object sender, KeyEventArgs e)
        {
            //删除
            //if (e.KeyValue == 46)
            //{
            //    DataRowView _drv;
            //    string _selectNo = "";
            //    for (int i = 0; i < lstUnQC.SelectedItems.Count; i++)
            //    {
            //        _drv = (DataRowView)lstUnQC.SelectedItems[i];
            //        if (_selectNo != "")
            //            _selectNo += ",";
            //        _selectNo += "'" + _drv["BAR_NO"] + "'";
            //    }
            //    if (_selectNo != "")
            //    {
            //        DataRow[] _drArr = _dsUnQC.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
            //        foreach (DataRow _dr in _drArr)
            //        {
            //            _dr.Delete();
            //        }
            //        _dsUnQC.AcceptChanges();

            //        lstUnQC.DataSource = _dsUnQC.Tables[0];
            //        lstUnQC.DisplayMember = "BAR_NO_DIS";
            //        lstUnQC.ValueMember = "BAR_NO";
            //    }
            //}
        }
        #endregion

        #region 刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitDataSet();

            //清空数据
            txtEligibleRemA.Text = "";
            _dsEligibleA.Clear();
            _dsEligibleA.AcceptChanges();
            lstEligibleA.DataSource = _dsEligibleA.Tables[0];
            lstEligibleA.DisplayMember = "BAR_NO_DIS";
            lstEligibleA.ValueMember = "BAR_NO";

            txtEligibleRemB.Text = "";
            txtSpcNoB.Text = "";
            txtSpcNameB.Text = "";

            _dsEligibleB.Clear();
            _dsEligibleB.AcceptChanges();
            lstEligibleB.DataSource = _dsEligibleB.Tables[0];
            lstEligibleB.DisplayMember = "BAR_NO_DIS";
            lstEligibleB.ValueMember = "BAR_NO";

            txtRejectRem.Text = "";
            txtSpcName.Text = "";
            txtSpcNo.Text = "";

            _dsReject.Tables[0].Clear();
            _dsReject.AcceptChanges();
            lstReject.DataSource = _dsReject.Tables[0];
            lstReject.DisplayMember = "BAR_NO_DIS";
            lstReject.ValueMember = "BAR_NO";
        }
        #endregion
    }
}