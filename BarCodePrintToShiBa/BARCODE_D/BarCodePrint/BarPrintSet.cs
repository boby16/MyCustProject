using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;

namespace BarCodePrintTSB
{
	public partial class BarPrintSet : Form
	{
        private string _filename = Application.ExecutablePath + ".config";

		public BarPrintSet()
		{
			InitializeComponent();
		}

		private void BarPrintSet_Load(object sender, EventArgs e)
        {
            BarSet _bs = new BarSet();
            txtPrintSetPath.Text = _bs.GetPrintSet(_filename);
            txtRemoteServer.Text = _bs.GetRemoteServer(_filename);
		}

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                BarSet _bs = new BarSet();
                _bs.SetRemoteServer(txtRemoteServer.Text, _filename);
                _bs.SetPrintSet("PrintSetPath", txtPrintSetPath.Text, _filename);
                this.Close();
                if (MessageBox.Show("�趨�ɹ���  ��������������޸Ĳſ���Ч��\n�Ƿ�����������","��ʾ",MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Application.Restart();
            }
            catch (Exception _ex)
            {
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("�޷�д��config�ļ���ԭ��\n" + _err);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}