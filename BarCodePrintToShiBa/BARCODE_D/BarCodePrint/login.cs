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
	public partial class login : Form
	{
		public login()
		{
			InitializeComponent();
		}
        DataSet _dsUsr = new DataSet();

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                onlineService.BarPrintServer _barSer = new global::BarCodePrintTSB.onlineService.BarPrintServer();
                _barSer.UseDefaultCredentials = true;
                if (lstUserNo.SelectedIndex >= 0)
                {
                    string _usr = lstUserNo.SelectedValue.ToString();
                    if (_barSer.LoginValidate(_usr, txtPwd.Text))
                    {
                        this.Hide();
                        BarRole.USR_NO = _usr;
                        if (_dsUsr != null && _dsUsr.Tables.Count > 0 && _dsUsr.Tables[0].Rows.Count > 0)
                        {
                            DataRow[] _drArr= _dsUsr.Tables[0].Select("USR='" + _usr + "'");
                            if (_drArr.Length > 0)
                                BarRole.DEP = _drArr[0]["DEP"].ToString();
                            if (BarRole.DEP == "")
                                BarRole.DEP = _dsUsr.Tables[0].Rows[0]["TOP_DEP"].ToString();
                            BarRole.COMP_NO = _dsUsr.Tables[0].Rows[0]["COMPNO"].ToString();
                        }
                        
                        string _chkMonull = _barSer.GetUserRight(_usr, "CHK_MMNULL");
                        if (!String.IsNullOrEmpty(_chkMonull) && _chkMonull == "Y")
                            BarRole.CHK_MONULL = true;
                        else
                            BarRole.CHK_MONULL = false;
                        Main _main = new Main();
                        _main.Show();
                    }
                    else
                    {
                        MessageBox.Show("��¼ʧ�ܣ������û����������Ƿ���ȷ��");
                    }
                }
                else
                {
                    MessageBox.Show("�û�������Ϊ�գ�");
                    lstUserNo.SelectedIndex = 0;
                }
            }
            catch
            {
                MessageBox.Show("��¼ʧ�ܣ������û����������Ƿ���ȷ��"); 
            }
        }

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
				txtPwd.Focus();
		}

		private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
				btnOk.Focus();
		}

        private void login_Load(object sender, EventArgs e)
        {
            onlineService.BarPrintServer _barSer = new BarCodePrintTSB.onlineService.BarPrintServer();
            
            _barSer.UseDefaultCredentials = true;
            try
            {
                _dsUsr = _barSer.GetUsers();
                if (_dsUsr != null && _dsUsr.Tables.Count > 0)
                {
                    string _usr = "";
                    foreach (DataRow dr in _dsUsr.Tables[0].Rows)
                    {
                        _usr = dr["USR"].ToString();
                        for (int i = dr["USR"].ToString().Length; i < 20; i++)
                        {
                            _usr += " ";
                        }
                            dr["NAME"] = _usr + Strings.StrConv(dr["NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    }
                    lstUserNo.ValueMember = "USR";
                    lstUserNo.DisplayMember = "NAME";
                    lstUserNo.DataSource = _dsUsr.Tables[0];
                    lstUserNo.SelectedIndex = 0;
                }
            }
            catch(Exception _ex)
            {
                MessageBox.Show("�޷�����Զ�̷����������趨��"+ _ex.Message);
                BarPrintSet _bpSet = new BarPrintSet();
                _bpSet.StartPosition = FormStartPosition.CenterParent;
                _bpSet.MaximizeBox = false;
                _bpSet.MinimizeBox = false;
                _bpSet.ShowDialog();
                this.Close();
            }
            BarRole _barRole = new BarRole();
            try
            {
                _barRole.GetBarRole();
            }
            catch(Exception ex)
            {
                MessageBox.Show("û���趨����ı���ԭ��" + ex.Message);
                this.Close();
                Application.Exit();
            }
        }
	}
}