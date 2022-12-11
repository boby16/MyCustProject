using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Sunlike.Common.Utility;

namespace SystemModify
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class datamanage : System.Windows.Forms.Form
    {
        #region Controls
        private System.Windows.Forms.Label lblHost;
		private System.Windows.Forms.Label lblUser;
		private System.Windows.Forms.Label lblPwd;
		private System.Windows.Forms.Label lblComp;
		private System.Windows.Forms.TextBox txtHost;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.TextBox txtPwd;
		private System.Windows.Forms.Button btnConnTest;
		private System.Windows.Forms.ComboBox combComp;
		private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtUsr;
		private System.Windows.Forms.Label lblUsr;
		private System.Windows.Forms.TextBox txtPswd;
		private System.Windows.Forms.Label lblPswd;
		private System.Windows.Forms.TextBox txtDomain;
		private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.Button btnWriteUsr;
        private TextBox txtPath;
        private GroupBox groupBox3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ComboBox lstOldDB;
        private ComboBox lstNewDB;
        private Button btnConnBarDB;
        private Label label2;
        private Label label1;
        private Button btnBarImport;
        private GroupBox groupBox4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public datamanage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
        }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblHost = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblComp = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.btnConnTest = new System.Windows.Forms.Button();
            this.combComp = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnWriteUsr = new System.Windows.Forms.Button();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.lblDomain = new System.Windows.Forms.Label();
            this.txtPswd = new System.Windows.Forms.TextBox();
            this.lblPswd = new System.Windows.Forms.Label();
            this.txtUsr = new System.Windows.Forms.TextBox();
            this.lblUsr = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnConnBarDB = new System.Windows.Forms.Button();
            this.lstOldDB = new System.Windows.Forms.ComboBox();
            this.btnBarImport = new System.Windows.Forms.Button();
            this.lstNewDB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHost
            // 
            this.lblHost.Location = new System.Drawing.Point(13, 16);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(89, 24);
            this.lblHost.TabIndex = 10;
            this.lblHost.Text = "SQL��������:";
            this.lblHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUser
            // 
            this.lblUser.Location = new System.Drawing.Point(13, 51);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(89, 22);
            this.lblUser.TabIndex = 12;
            this.lblUser.Text = "ʹ��������:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPwd
            // 
            this.lblPwd.Location = new System.Drawing.Point(13, 82);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(89, 23);
            this.lblPwd.TabIndex = 14;
            this.lblPwd.Text = "��    ��:";
            this.lblPwd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblComp
            // 
            this.lblComp.Location = new System.Drawing.Point(13, 119);
            this.lblComp.Name = "lblComp";
            this.lblComp.Size = new System.Drawing.Size(89, 24);
            this.lblComp.TabIndex = 17;
            this.lblComp.Text = "������:";
            this.lblComp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(107, 19);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(183, 20);
            this.txtHost.TabIndex = 11;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(107, 51);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(183, 20);
            this.txtUser.TabIndex = 13;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(107, 82);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(183, 20);
            this.txtPwd.TabIndex = 15;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // btnConnTest
            // 
            this.btnConnTest.Location = new System.Drawing.Point(296, 79);
            this.btnConnTest.Name = "btnConnTest";
            this.btnConnTest.Size = new System.Drawing.Size(75, 23);
            this.btnConnTest.TabIndex = 16;
            this.btnConnTest.Text = "���Ӳ���";
            this.btnConnTest.Click += new System.EventHandler(this.btnConnTest_Click);
            // 
            // combComp
            // 
            this.combComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combComp.Location = new System.Drawing.Point(107, 119);
            this.combComp.Name = "combComp";
            this.combComp.Size = new System.Drawing.Size(183, 21);
            this.combComp.TabIndex = 18;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(134, 368);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "����";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(222, 368);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "�ر�";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(296, 17);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.Text = "��ȡ��Ϣ";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnWriteUsr);
            this.groupBox1.Controls.Add(this.txtDomain);
            this.groupBox1.Controls.Add(this.lblDomain);
            this.groupBox1.Controls.Add(this.txtPswd);
            this.groupBox1.Controls.Add(this.lblPswd);
            this.groupBox1.Controls.Add(this.txtUsr);
            this.groupBox1.Controls.Add(this.lblUsr);
            this.groupBox1.Location = new System.Drawing.Point(18, 231);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 120);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "�趨Windowsģ���û�";
            // 
            // btnWriteUsr
            // 
            this.btnWriteUsr.Location = new System.Drawing.Point(296, 88);
            this.btnWriteUsr.Name = "btnWriteUsr";
            this.btnWriteUsr.Size = new System.Drawing.Size(75, 23);
            this.btnWriteUsr.TabIndex = 26;
            this.btnWriteUsr.Text = "����";
            this.btnWriteUsr.Click += new System.EventHandler(this.btnWriteUsr_Click);
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(107, 89);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(183, 20);
            this.txtDomain.TabIndex = 25;
            // 
            // lblDomain
            // 
            this.lblDomain.Location = new System.Drawing.Point(13, 88);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(89, 22);
            this.lblDomain.TabIndex = 24;
            this.lblDomain.Text = "��    ��:";
            this.lblDomain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPswd
            // 
            this.txtPswd.Location = new System.Drawing.Point(107, 55);
            this.txtPswd.Name = "txtPswd";
            this.txtPswd.Size = new System.Drawing.Size(183, 20);
            this.txtPswd.TabIndex = 23;
            this.txtPswd.UseSystemPasswordChar = true;
            // 
            // lblPswd
            // 
            this.lblPswd.Location = new System.Drawing.Point(13, 52);
            this.lblPswd.Name = "lblPswd";
            this.lblPswd.Size = new System.Drawing.Size(89, 23);
            this.lblPswd.TabIndex = 22;
            this.lblPswd.Text = "��    ��:";
            this.lblPswd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUsr
            // 
            this.txtUsr.Location = new System.Drawing.Point(107, 22);
            this.txtUsr.Name = "txtUsr";
            this.txtUsr.Size = new System.Drawing.Size(183, 20);
            this.txtUsr.TabIndex = 21;
            // 
            // lblUsr
            // 
            this.lblUsr.Location = new System.Drawing.Point(13, 20);
            this.lblUsr.Name = "lblUsr";
            this.lblUsr.Size = new System.Drawing.Size(89, 23);
            this.lblUsr.TabIndex = 20;
            this.lblUsr.Text = "�û���:";
            this.lblUsr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblUser);
            this.groupBox2.Controls.Add(this.combComp);
            this.groupBox2.Controls.Add(this.btnConnTest);
            this.groupBox2.Controls.Add(this.txtPwd);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.lblComp);
            this.groupBox2.Controls.Add(this.lblPwd);
            this.groupBox2.Controls.Add(this.lblHost);
            this.groupBox2.Controls.Add(this.txtHost);
            this.groupBox2.Location = new System.Drawing.Point(18, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 151);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "�趨����";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(16, 19);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(274, 20);
            this.txtPath.TabIndex = 1;
            this.txtPath.Text = "C:\\Inetpub\\wwwroot\\BarPrintService";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtPath);
            this.groupBox3.Controls.Add(this.btnLoad);
            this.groupBox3.Location = new System.Drawing.Point(18, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(384, 46);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "����ϵͳ����������ַ:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(436, 433);
            this.tabControl1.TabIndex = 30;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.btnRefresh);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(428, 407);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "�����趨";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(428, 407);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "�¿�����������ʷ���ϵ���";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnConnBarDB);
            this.groupBox4.Controls.Add(this.lstOldDB);
            this.groupBox4.Controls.Add(this.btnBarImport);
            this.groupBox4.Controls.Add(this.lstNewDB);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(422, 401);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "** ˵�������������׺�ͨ���˲���������������׵���������";
            // 
            // btnConnBarDB
            // 
            this.btnConnBarDB.Location = new System.Drawing.Point(136, 60);
            this.btnConnBarDB.Name = "btnConnBarDB";
            this.btnConnBarDB.Size = new System.Drawing.Size(114, 23);
            this.btnConnBarDB.TabIndex = 21;
            this.btnConnBarDB.Text = "�������ݿ�";
            this.btnConnBarDB.UseVisualStyleBackColor = true;
            this.btnConnBarDB.Click += new System.EventHandler(this.btnConnBarDB_Click);
            // 
            // lstOldDB
            // 
            this.lstOldDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstOldDB.Location = new System.Drawing.Point(136, 102);
            this.lstOldDB.Name = "lstOldDB";
            this.lstOldDB.Size = new System.Drawing.Size(183, 21);
            this.lstOldDB.TabIndex = 19;
            // 
            // btnBarImport
            // 
            this.btnBarImport.Location = new System.Drawing.Point(136, 202);
            this.btnBarImport.Name = "btnBarImport";
            this.btnBarImport.Size = new System.Drawing.Size(114, 23);
            this.btnBarImport.TabIndex = 24;
            this.btnBarImport.Text = "�������ϵ���";
            this.btnBarImport.UseVisualStyleBackColor = true;
            this.btnBarImport.Click += new System.EventHandler(this.btnBarImport_Click);
            // 
            // lstNewDB
            // 
            this.lstNewDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstNewDB.Location = new System.Drawing.Point(136, 140);
            this.lstNewDB.Name = "lstNewDB";
            this.lstNewDB.Size = new System.Drawing.Size(183, 21);
            this.lstNewDB.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "�������������Ͽ�";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "�������������Ͽ�";
            // 
            // datamanage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(436, 433);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "datamanage";
            this.Text = "�����趨";
            this.Load += new System.EventHandler(this.datamanage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new datamanage());
		}

        #endregion

        #region ���appsettings
        public string check_appSetting(string nodeName,string webPath)
		{
			XmlDocument xd = new XmlDocument();
			xd.Load( webPath );
			string _para = "N";
			try
			{
                XmlNode nodeApp = xd.DocumentElement.SelectSingleNode("//appSettings/add[@key='" + nodeName + "']");
				if (nodeApp != null)
				{
                    _para = nodeApp.Attributes["value"].Value;
				}
			}
			catch 
			{
				return "N";
			}
			return _para;
		}
		#endregion

		#region ѡ������
		private void ChooseDataBase(ComboBox combComp,string _conn)
		{
			try
			{
				string _sql = "SELECT distinct A.COMPNO,(select top 1 NAME from COMP where COMPNO=A.COMPNO) NAME FROM COMP A";
				SqlDataAdapter sda = new SqlDataAdapter(_sql,_conn);
				DataSet ds = new DataSet();
				sda.Fill(ds);
				for (int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					ds.Tables[0].Rows[i]["NAME"] = ds.Tables[0].Rows[i]["NAME"].ToString();
				}
				combComp.DataSource = ds.Tables[0].DefaultView;
				combComp.DisplayMember = "NAME";
				combComp.ValueMember = "COMPNO";
			}
			catch(Exception _ex)
			{
				combComp.DataSource = null;
				MessageBox.Show(_ex.Message);
			}
		}
		#endregion

		#region ��ȡconfig�ļ�·��
		public string Get_Path()
		{
			string _path = txtPath.Text.Trim();
            if (string.IsNullOrEmpty(_path))
            {
                MessageBox.Show("����������ϵͳ����������ַ��");
            }
            else
            {
                _path += @"\web.config";
                if (System.IO.File.Exists(_path))
                {
                }
                else
                {
                    _path = "";
                    MessageBox.Show("web.config�ļ������ڡ�\n����·���Ƿ���ȷ��");
                }
            }
            return _path;
		}
		#endregion

		private void btnConnTest_Click(object sender, System.EventArgs e)
		{
			try
			{
				btnRefresh.Enabled = false;
				string _server = txtHost.Text.Trim();
				string _uid = txtUser.Text.Trim();
				string _pwd = txtPwd.Text;
				string _connection = "server=" + _server + ";uid= " + _uid + ";pwd=" + _pwd + ";database=SunSystem";
				ChooseDataBase(combComp,_connection);
				btnRefresh.Enabled = true;
			}
			catch(Exception _ex)
			{
				btnRefresh.Enabled = false;
				MessageBox.Show("���Ӳ��Դ���ԭ��" + _ex.Message);
			}
		}

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			string _conn = "";
			string[] _para = new String[3];
			string _path = this.Get_Path();
            if (!string.IsNullOrEmpty(_path))
            {
                XmlDocument xd = new XmlDocument();
                try
                {
                    xd.Load(_path);
                }
                catch (Exception _ex)
                {
                    MessageBox.Show(_ex.Message);
                    return;
                }
                if (this.check_appSetting("ConnectionString", _path) != "N")
                {
                    _conn = this.check_appSetting("ConnectionString", _path);
                    int x = _conn.IndexOf(";");
                    int i = 0;
                    try
                    {
                        while (x > 0)
                        {
                            _para[i] = _conn.Substring(0, x);
                            _conn = _conn.Substring(x + 1, _conn.Length - x - 1);
                            x = _conn.IndexOf(";");
                            i = i + 1;
                        }
                        if (_para[0].Trim() != "")
                        {
                            int y = _para[0].IndexOf("=");
                            txtHost.Text = _para[0].Substring(y + 1, _para[0].Length - y - 1);
                        }
                        if (_para[1].Trim() != "")
                        {
                            int y = _para[1].IndexOf("=");
                            txtUser.Text = _para[1].Substring(y + 1, _para[1].Length - y - 1);
                        }
                        if (_para[2].Trim() != "")
                        {
                            int y = _para[2].IndexOf("=");
                            this.txtPwd.Text = Security.DecodingPswd(_para[2].Substring(y + 1, _para[2].Length - y - 1));
                        }
                        this.btnConnTest_Click(sender, e);
                        string _compNo = this.check_appSetting("DatabaseName", _path);
                        this.combComp.SelectedValue = _compNo;
                        if (this.check_appSetting("Usr", _path) != "N")
                        {
                            txtUsr.Text = check_appSetting("Usr", _path);
                            txtUsr.Text = Security.DecodingPswd(txtUsr.Text);
                        }
                        if (this.check_appSetting("Pwd", _path) != "N")
                        {
                            txtPswd.Text = check_appSetting("Pwd", _path);
                            txtPswd.Text = Security.DecodingPswd(txtPswd.Text);
                        }
                        if (this.check_appSetting("Domain", _path) != "N")
                        {
                            txtDomain.Text = check_appSetting("Domain", _path);
                            txtDomain.Text = Security.DecodingPswd(txtDomain.Text);
                        }
                    }
                    catch (Exception _ex)
                    {
                        MessageBox.Show(_ex.Message);
                    }
                }
                else
                {
                    txtHost.Text = System.Environment.MachineName.ToString();
                }
            }
		}

		private void btnWriteUsr_Click(object sender, System.EventArgs e)
		{
            if (CheckUsr())
            {
                MessageBox.Show("ģ��ɹ���");
            }
		}

        private bool CheckUsr()
        {
            bool _result = false;
            if (this.txtPswd.Text == "")
            {
                MessageBox.Show("����дģ���û�����");
            }
            else
            {
                bool _imperUsr = false;
                string _path = Get_Path();
                if (!string.IsNullOrEmpty(_path))
                {
                    ConfigSetting SunlikeConfig = new ConfigSetting(_path);
                    _imperUsr = SunlikeConfig.CheckWindowUser(txtUsr.Text, txtPswd.Text, txtDomain.Text);
                    if (_imperUsr)
                    {
                        System.Security.Principal.IPrincipal wp = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent());
                        if (wp.IsInRole("BUILTIN\\Administrators"))
                        {
                            _result = true;
                        }
                        else
                        {
                            MessageBox.Show("�û������ǹ���Ա�����û�Ȩ�޲��㣡");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ģ��ʧ�ܣ������û���������������Ƿ���ȷ��");
                    }
                }
            }
            return _result;
        }

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
			Application.Exit();
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
            if (CheckUsr())
            {
                string _path = this.Get_Path();
                string _server = txtHost.Text.Trim();
                if (_server.Equals("."))
                {
                    _server = "LocalHost";
                }
                string _uid = txtUser.Text.Trim();
                string _pwd = txtPwd.Text.Trim();
                _pwd = Security.EncodingPswd(_pwd);
                string _databasename = "";
                if (combComp.SelectedItem != null)
                {
                    _databasename = combComp.SelectedValue.ToString();
                }
                if ((_server != "") && (_uid != "") && (_databasename != ""))
                {
                    string _connection = "server=" + _server + ";uid=" + _uid + ";pwd=" + _pwd + ";";
                    string _conn = "server=" + _server + ";uid=" + _uid + ";pwd=" + txtPwd.Text.Trim() + ";";
                    ConfigSetting _config = new ConfigSetting(_path);
                    try
                    {
                        string _barPrintDB = "DB_BARPRINT_" + _databasename;

                        _config.SetReport("ConnectionString", _connection);
                        _config.SetReport("DatabaseName", _databasename);
                        _config.SetReport("Usr", Security.EncodingPswd(txtUsr.Text));
                        _config.SetReport("Pwd", Security.EncodingPswd(txtPswd.Text));
                        _config.SetReport("Domain", Security.EncodingPswd(txtDomain.Text));
                        _config.SetReport("BarPrintDB", _barPrintDB);
                        _config.Save(txtUsr.Text, txtPswd.Text, txtDomain.Text);

                        #region �������кż����

                        //����BarPrint���ݿ�
                        string _sqlStr = "USE [DB_" + _databasename + "];select databasepropertyex('DB_" + _databasename + "','Collation')";
                        using (SqlConnection _sqlConn = new SqlConnection(_conn))
                        {
                            _sqlConn.Open();
                            SqlCommand _sqlComm = _sqlConn.CreateCommand();
                            _sqlComm.CommandType = CommandType.Text;
                            _sqlComm.CommandText = _sqlStr;
                            SqlDataAdapter _sda = new SqlDataAdapter(_sqlComm);
                            DataSet _ds = new DataSet();
                            _sda.Fill(_ds);
                            string _langStr = _ds.Tables[0].Rows[0][0].ToString();

                            System.Text.StringBuilder _sqlCreateDB = new System.Text.StringBuilder();
                            _sqlCreateDB.Append("USE master; IF DB_ID (N'" + _barPrintDB + "') IS NULL\n");

                            if (_langStr.Length > 0)
                            {
                                _sqlCreateDB.Append("CREATE DATABASE " + _barPrintDB + " COLLATE " + _langStr + " \n");
                            }
                            else
                            {
                                _sqlCreateDB.Append("CREATE DATABASE " + _barPrintDB + " \n");
                            }

                            _sqlComm = _sqlConn.CreateCommand();
                            _sqlComm.CommandType = CommandType.Text;
                            _sqlComm.CommandText = _sqlCreateDB.ToString();
                            _sqlComm.ExecuteNonQuery();

                        //�������к���Ϣ��
                        System.Text.StringBuilder _sqlCreateTab = new System.Text.StringBuilder();
                        _sqlCreateTab.Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_QC',N'U') IS NULL\n")
                            .Append("BEGIN\n")
                            .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_QC]\n")
                            .Append("(\n")
                            .Append("[BAR_NO] VARCHAR(90) PRIMARY KEY CLUSTERED,\n")//���к�
                            .Append("[OS_BAR_NO] VARCHAR(90),\n")//��Դ��Ʒ���к�
                            .Append("[QC] VARCHAR(2),\n")//����״̬��1���ϸ�A:A��ϸ�B:B��ϸ�2:���ϸ�N:�ڳ����
                            .Append("[STATUS] VARCHAR(2),\n")//���к�״̬:QC:���飻ML:���ϣ�FT:����; SW���ͽɣ�TY��ת�쳣��ST��ʾ����Ʒ,��������������̣�HH������
                            .Append("[SPC_NO] VARCHAR(10),\n")//���ϸ�ԭ��
                            .Append("[BAT_NO] VARCHAR(20),\n")//����
                            .Append("[BIL_ID] VARCHAR(2),\n")//���ݱ�
                            .Append("[BIL_NO] VARCHAR(20),\n")//����
                            .Append("[WH_SJ] VARCHAR(12),\n")//�ͽɿ�λ
                            .Append("[PRC_ID] VARCHAR(1),\n")//�쳣����ʽ
                            .Append("[PRD_TYPE] VARCHAR(1),\n")//��Ʒ���1����ģ�2�����Ʒ��3����ģ�
                            .Append("[REM] VARCHAR(500),\n")//��ע
                            .Append("[FORMAT] VARCHAR(1),\n")//��ӡ��ʽ
                            .Append("[LH] VARCHAR(100),\n")//��Ĵ�ӡ�Ϻ�
                            .Append("[PRT_NAME] VARCHAR(100),\n")//��ӡƷ��
                            .Append("[USR] VARCHAR(12),\n")//��ӡ��Ա
                            .Append("[SYS_DATE] Datetime,\n")//��ӡʱ��
                            .Append("[USR_QC] VARCHAR(12),\n")//Ʒ����Ա
                            .Append("[QC_DATE] Datetime,\n")//Ʒ��ʱ��
                            .Append("[USR_BOX] VARCHAR(12),\n")//װ����Ա
                            .Append("[BOX_DATE] Datetime,\n")//װ��ʱ��
                            .Append("[USR_UNBOX] VARCHAR(12),\n")//������Ա
                            .Append("[UNBOX_DATE] Datetime,\n")//����ʱ��
                            .Append("[USR_SJ] VARCHAR(12),\n")//�ͽ���Ա
                            .Append("[SJ_DATE] Datetime,\n")//�ͽ�ʱ��
                            .Append("[USR_UNSJ] VARCHAR(12),\n")//�����ͽ���Ա
                            .Append("[UNSJ_DATE] Datetime,\n")//�����ͽ�ʱ��
                            .Append("[USR_WHBOX] VARCHAR(12),\n")//�ֿ�װ����Ա
                            .Append("[WHBOX_DATE] Datetime,\n")//�ֿ�װ��ʱ��
                            .Append("[USR_WHUNBOX] VARCHAR(12),\n")//�ֿ������Ա
                            .Append("[WHUNBOX_DATE] Datetime, \n")//�ֿ����ʱ��
                            .Append("[TO_CUST] VARCHAR(12) \n")//����ͻ�
                            .Append(") ON [PRIMARY] END \n")
                            //������Ϣ��
                            .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_BOX_BAT',N'U') IS NULL\n")
                            .Append("BEGIN\n")
                            .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_BOX_BAT]\n")
                            .Append("(\n")
                            .Append("[BOX_NO] VARCHAR(90) PRIMARY KEY CLUSTERED,\n")//������
                            .Append("[PRD_NO] VARCHAR(30),\n")//��¼�������Ӧ��Ʒ��
                            .Append("[PRD_MARK] VARCHAR(40),\n")//��¼�������Ӧ������
                            .Append("[BAT_NO] VARCHAR(100),\n")//��¼�������Ӧ������
                            .Append("[LH] VARCHAR(100),\n")//��¼�����Ӧ������Ʒ��
                            .Append("[IDX_NAME] VARCHAR(100),\n")//��¼�����Ӧ��������������
                            .Append("[FORMAT] VARCHAR(1),\n")//��ӡ��ʽ 
                            .Append("[QC] VARCHAR(2),\n")//����״̬��1���ϸ�A:A��ϸ�B:B��ϸ�2:���ϸ�N:�ڳ����
                            .Append("[STATUS] VARCHAR(2),\n")//״̬:SW���ͽ�
                            .Append("[SPC_NO] VARCHAR(10),\n")//���ϸ�ԭ��
                            .Append("[BIL_ID] VARCHAR(2),\n")//���ݱ�
                            .Append("[BIL_NO] VARCHAR(20),\n")//����
                            .Append("[WH_SJ] VARCHAR(12),\n")//�ͽɿ�λ
                            .Append("[REM] VARCHAR(500),\n")//��ע
                            .Append("[USR_PRT] VARCHAR(12)\n")//��ӡ��Ա
                            .Append(") ON [PRIMARY] END \n")

                            //��ӡ���ƾ�����
                            .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_IDX',N'U') IS NULL\n")
                            .Append("BEGIN\n")
                            .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_IDX]\n")
                            .Append("(\n")
                            .Append("[ID] int identity(0,1) PRIMARY KEY CLUSTERED,\n")//�Զ����
                            .Append("[IDX_NO] VARCHAR(20),\n")//�������
                            .Append("[NAME] VARCHAR(50)\n")//������������
                            .Append(") ON [PRIMARY] END \n")
                            //��ӡ����������
                            .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_PRTNAME',N'U') IS NULL\n")
                            .Append("BEGIN\n")
                            .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_PRTNAME]\n")
                            .Append("(\n")
                            .Append("[TYPE_ID] VARCHAR(1),\n")//���0����ģ�1����ģ���Ʒ����2�����룻3�����Ʒ��
                            /*
                             * ��ӡ��ʽ��LH\TS\BASEC,BASIC\SAMPLE\SAMLL\FOXCONN\OTHER\HQ\SBB��
                             *      �����Ϊ��0����ģ�ʱ��
                             *          LH����ӡ�Ϻţ���Ʒ�Ź�����������ӡ�ġ��Ϻš�������Դ�ڱ�BAR_PRTNAME��PRD_NO=Ʒ�ŵ�NAME�ֶΣ�
                             *          TS������棨�����������������ӡ�ġ���ӡƷ����������Դ�ڱ�BAR_PRTNAME��IDX_NO=�����NAME�ֶΣ�
                             *          BASIC����׼�棬����ӡ�ġ�Ʒ����������Դ�ڻ�Ʒ������ϲ�������ţ��˴�����ά������Ϊ��׼���ӡ����Ϣȫ������ϵͳ��
                             *      �����Ϊ��2�����룩ʱ��
                             *          BASIC����׼�棬����ӡ�ġ�Ʒ����������Դ�ڻ�Ʒ������ϲ�������ţ��˴�����ά������Ϊ��׼���ӡ����Ϣȫ������ϵͳ��
                             *          SAMPLE����Լ�棨���Ʊ�׼�棩
                             *          SMALL����ӡ��ţ����Ʊ�׼�棩
                             *          FOXCONN����ʿ���棨��Ӧ�̣��̶�Ϊ����Ʒ�����ֹ��޸ģ�Ʒ���̶�Ϊ��Ƭ�ġ����ֹ��޸ģ���Ρ���ʿ���Ϻš���Ʒ�������ֱ���Դ�ڻ�Ʒ���������Զ����ֶΡ�BC_GP������LH_FOXCONN������QTY_PER_GP�������ʣ���Դ��������������Զ����ֶΡ�CZ_GP������
                             *          OTHER������棨�����������������ӡ�ġ�Ʒ����������Դ�ڱ�BAR_PRTNAME��IDX_NO=�����NAME�ֶΣ�
                             *          HQ������棨ͬ����棩��
                             *          SBB���ܰر̰棨��Ʒ�Ź�����������ӡ�ġ���Ʒ��š�������Դ�ڱ�BAR_PRTNAME��PRD_NO=Ʒ�ŵ�NAME�ֶΣ�
                             */
                            .Append("[IDX] VARCHAR(10),\n")//��ӡ��ʽ
                            .Append("[IDX_NO] VARCHAR(10),\n")//�������
                            .Append("[PRD_NO] VARCHAR(30),\n")//��Ʒ����
                            .Append("[NAME] VARCHAR(50),\n")//������������
                            .Append(" constraint PK_BAR_PRTNAME primary key clustered(TYPE_ID,IDX,IDX_NO,PRD_NO)\n")
                            .Append(") ON [PRIMARY] END \n")
                            //��������Ʒ���
                            .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.SA_BAR_CHG',N'U') IS NULL\n")
                            .Append("BEGIN\n")
                            .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[SA_BAR_CHG]\n")
                            .Append("(\n")
                            .Append("[PS_ID] VARCHAR(2),\n")//���ݱ�
                            .Append("[PS_NO] VARCHAR(20),\n")//���ݺ���
                            .Append("[PS_ITM] INT,\n")//��/��/��/�����
                            .Append("[ITM] INT,\n")//���
                            .Append("[BAR_CODE] VARCHAR(90),\n")//���к�
                            .Append("[BOX_NO] VARCHAR(90),\n")//������
                            .Append("[USR_QC] VARCHAR(12),\n")//Ʒ����Ա
                            .Append("[QC_DATE] Datetime,\n")//Ʒ��ʱ��
                            .Append("[USR_CHG] VARCHAR(12),\n")//�滻��Ա
                            .Append("[CHG_DATE] Datetime,\n")//�滻ʱ��
                            .Append("[CHG_ID] VARCHAR(1),\n")//�Ƿ����滻
                            .Append("[CHG_SPC_NO] VARCHAR(10),\n")//�滻ԭ��
                            .Append("[REM] VARCHAR(500),\n")//��ע
                            .Append(" constraint PK_SA_BAR_CH primary key clustered(PS_ID,PS_NO,PS_ITM,ITM)\n")
                            .Append(") ON [PRIMARY] END");

                            _sqlComm = _sqlConn.CreateCommand();
                            _sqlComm.CommandType = CommandType.Text;
                            _sqlComm.CommandText = _sqlCreateTab.ToString();
                            _sqlComm.ExecuteNonQuery();
                        }
                        #endregion

                        MessageBox.Show("д��ɹ�");
                    }
                    catch (Exception _ex)
                    {
                        MessageBox.Show("�޷�д��Web.Config�ļ���ԭ��" + _ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("����д����");
                }
            }
		}

        private void btnBarImport_Click(object sender, EventArgs e)
        {
            if (lstOldDB.SelectedIndex >= 0 || lstNewDB.SelectedIndex >= 0)
            {
                string _newBarPrintDB = lstNewDB.SelectedValue.ToString();
                string _newCompNo = _newBarPrintDB.Substring(12);
                string _newDB = "DB_" + _newCompNo;
                string _oldBarPrintDB = lstOldDB.SelectedValue.ToString();
                string _oldCompNo = _oldBarPrintDB.Substring(12);
                string _oldDB = "DB_" + _oldCompNo;
                if (MessageBox.Show("���ڼ��������ݿ⡾" + _oldBarPrintDB + "���е���������\n���뵽���ݿ⡾" + _newBarPrintDB + "���С�\n�Ƿ�ȷ����", "ȷ��", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    string sqlScript = "begin tran \n"
                        //�����
                            + " insert into " + _newDB + "..BAR_BOX"
                            + " select A.* from " + _oldDB + "..BAR_BOX A"
                            + " left join " + _newDB + "..BAR_BOX B on B.BOX_NO=A.BOX_NO"
                            + " where B.BOX_NO is null AND A.BOX_NO in (select BOX_NO from " + _newDB + "..BAR_REC); \n"
                        //BAR_SQNO��
                            + " insert into " + _newDB + "..BAR_SQNO"
                            + " select A.* from " + _oldDB + "..BAR_SQNO A"
                            + " left join " + _newDB + "..BAR_SQNO B on B.PRD_NO=A.PRD_NO and A.PRD_MARK=B.PRD_MARK "
                            + " where B.PRD_NO is null; \n"
                        //Ȩ��FX_PSWD
                            + " insert into SUNSYSTEM..FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,[PROPERTY],U_AMT,R_CST,RANGE,LCK,ALLOW_ID,FLD,UPR,QTY,EPT)"
                            + " SELECT '" + _newCompNo + "' as COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,[PROPERTY],U_AMT,R_CST,RANGE,LCK,ALLOW_ID,FLD,UPR,QTY,EPT "
                            + " FROM SUNSYSTEM..FX_PSWD WHERE ISNULL(COMPNO,'')='" + _oldCompNo + "'; \n"
                        //Ʒ����Ϣ��
                            + " insert into " + _newBarPrintDB + "..BAR_QC (BAR_NO,OS_BAR_NO,QC,STATUS,SPC_NO,BAT_NO,BIL_ID,BIL_NO,WH_SJ,PRC_ID,PRD_TYPE,REM,FORMAT,LH,PRT_NAME,USR,SYS_DATE,USR_QC,QC_DATE,"
                            + " USR_BOX,BOX_DATE,USR_UNBOX,UNBOX_DATE,USR_SJ,SJ_DATE,USR_UNSJ,UNSJ_DATE,USR_WHBOX,WHBOX_DATE,USR_WHUNBOX,WHUNBOX_DATE,TO_CUST) "
                            + " select A.BAR_NO,A.OS_BAR_NO,A.QC,A.STATUS,A.SPC_NO,A.BAT_NO,A.BIL_ID,A.BIL_NO,A.WH_SJ,A.PRC_ID,A.PRD_TYPE,A.REM,A.FORMAT,A.LH,A.PRT_NAME,A.USR,A.SYS_DATE,A.USR_QC,A.QC_DATE,"
                            + " A.USR_BOX,A.BOX_DATE,A.USR_UNBOX,A.UNBOX_DATE,A.USR_SJ,A.SJ_DATE,A.USR_UNSJ,A.UNSJ_DATE,A.USR_WHBOX,A.WHBOX_DATE,A.USR_WHUNBOX,A.WHUNBOX_DATE,A.TO_CUST"
                            + " from " + _oldBarPrintDB + "..BAR_QC A"
                            + " inner join " + _newDB + "..BAR_REC B on A.BAR_NO=B.BAR_NO "
                            + " left join " + _newBarPrintDB + "..BAR_QC C on A.BAR_NO=C.BAR_NO"
                            + " where C.BAR_NO is null; \n"
                        //������Ϣ��
                            + " insert into " + _newBarPrintDB + "..BAR_BOX_BAT (BOX_NO,PRD_NO,PRD_MARK,BAT_NO,LH,IDX_NAME,FORMAT,QC,STATUS,SPC_NO,BIL_ID,BIL_NO,WH_SJ,REM,USR_PRT)"
                            + " select A.BOX_NO,A.PRD_NO,A.PRD_MARK,A.BAT_NO,A.LH,A.IDX_NAME,A.FORMAT,A.QC,A.STATUS,A.SPC_NO,A.BIL_ID,A.BIL_NO,A.WH_SJ,A.REM,A.USR_PRT"
                            + " from " + _oldBarPrintDB + "..BAR_BOX_BAT A"
                            + " inner join " + _newDB + "..BAR_BOX B on A.BOX_NO=B.BOX_NO"
                            + " left join " + _newBarPrintDB + "..BAR_BOX_BAT C on C.BOX_NO=A.BOX_NO"
                            + " where C.BOX_NO is null; \n"
                        //��ӡ���ƾ�����
                            + " if not exists (select top 1 IDX_NO from " + _newBarPrintDB + "..BAR_IDX) \n"
                            + "     insert into " + _newBarPrintDB + "..BAR_IDX (IDX_NO,NAME) select IDX_NO,NAME from " + _oldBarPrintDB + "..BAR_IDX; \n"
                        //��ӡ����������
                            + " insert into " + _newBarPrintDB + "..BAR_PRTNAME (TYPE_ID,IDX,IDX_NO,PRD_NO,NAME)"
                            + " select A.TYPE_ID,A.IDX,A.IDX_NO,A.PRD_NO,A.NAME from " + _oldBarPrintDB + "..BAR_PRTNAME A"
                            + " left join " + _newBarPrintDB + "..BAR_PRTNAME B on A.TYPE_ID=B.TYPE_ID and A.IDX=B.IDX and A.IDX_NO=B.IDX_NO and A.PRD_NO=B.PRD_NO"
                            + " where B.TYPE_ID is null; \n"
                        //����Ʒ���
                            + " insert into " + _newBarPrintDB + "..SA_BAR_CHG (PS_ID,PS_NO,PS_ITM,ITM,BAR_CODE,BOX_NO,USR_QC,QC_DATE,USR_CHG,CHG_DATE,CHG_ID,CHG_SPC_NO,REM)"
                            + " select A.PS_ID,A.PS_NO,A.PS_ITM,A.ITM,A.BAR_CODE,A.BOX_NO,A.USR_QC,A.QC_DATE,A.USR_CHG,A.CHG_DATE,A.CHG_ID,A.CHG_SPC_NO,A.REM from " + _oldBarPrintDB + "..SA_BAR_CHG A"
                            + " inner join " + _newDB + "..TF_PSS3 B on A.PS_ID=B.PS_ID AND A.PS_NO=B.PS_NO AND A.PS_ITM=B.PS_ITM AND A.ITM=B.ITM"
                            + " left join " + _newBarPrintDB + "..SA_BAR_CHG C on A.PS_ID=C.PS_ID AND A.PS_NO=C.PS_NO AND A.PS_ITM=C.PS_ITM AND A.ITM=C.ITM"
                            + " where C.PS_ID is null; \n"

                            + " if @@error<>0 \n"
                            + " rollback tran \n"
                            + " else \n"
                            + " commit tran ";
                    if (!String.IsNullOrEmpty(sqlScript))
                    {
                        string _server = txtHost.Text.Trim();
                        if (_server.Equals("."))
                        {
                            _server = "LocalHost";
                        }
                        string _uid = txtUser.Text.Trim();
                        string _conn = "server=" + _server + ";uid=" + _uid + ";pwd=" + txtPwd.Text.Trim() + ";";
                        using (SqlConnection sConnection = new SqlConnection(_conn))
                        {
                            try
                            {
                                sConnection.Open();
                                SqlCommand _cm = sConnection.CreateCommand();
                                _cm.CommandTimeout = 300;
                                _cm.CommandType = CommandType.Text;
                                _cm.CommandText = sqlScript;
                                _cm.ExecuteNonQuery();
                                MessageBox.Show("����ɹ���");
                            }
                            catch (Exception _ex)
                            {
                                string _error = _ex.Message;
                                if (_error.Length > 200)
                                    _error = _error.Substring(0, 200);
                                MessageBox.Show("����ʧ�ܣ�\n" + _error);
                            }
                        }
                    }
                }
            }
        }

        private void btnConnBarDB_Click(object sender, EventArgs e)
        {
            try
            {
                string _server = txtHost.Text.Trim();
                if (_server.Equals("."))
                {
                    _server = "LocalHost";
                }
                string _uid = txtUser.Text.Trim();
                string _conn = "server=" + _server + ";uid=" + _uid + ";pwd=" + txtPwd.Text.Trim() + ";";
                string _sql = "select [name] from master.dbo.sysdatabases where [name] like 'DB_BARPRINT%'";

                SqlDataAdapter _sda = new SqlDataAdapter(_sql, _conn);
                DataSet _ds = new DataSet();
                _sda.Fill(_ds);

                lstOldDB.DataSource = _ds.Tables[0];
                lstOldDB.ValueMember = "name";
                lstOldDB.DisplayMember = "name";
                if (lstOldDB.Items.Count > 0)
                    lstOldDB.SelectedIndex = 0;

                DataSet _ds1 = _ds.Copy();
                lstNewDB.DataSource = _ds1.Tables[0];
                lstNewDB.ValueMember = "name";
                lstNewDB.DisplayMember = "name";
                if (lstNewDB.Items.Count > 0)
                    lstNewDB.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("����ʧ�ܣ�");
            }
        }

        private void datamanage_Load(object sender, EventArgs e)
        {
            txtPath.Text = Application.StartupPath;
        }
	}
}
