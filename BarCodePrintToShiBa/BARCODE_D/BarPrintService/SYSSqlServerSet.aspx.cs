using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Sunlike.Business;
using Sunlike.Common.Utility;
using System.Configuration;
using System.IO;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;

namespace Sunlike.Web.DRP
{
	/// <summary>
	/// Summary description for SYSSqlServerSet.
	/// </summary>
    public class SYSSqlServerSet : Sunlike.Web.UI.SunlikePage
	{
		#region Fields
		protected System.Web.UI.WebControls.Label lblServer;
		protected System.Web.UI.WebControls.TextBox txtServer;
		protected System.Web.UI.WebControls.Label lblServer_User;
		protected System.Web.UI.WebControls.TextBox txtSqlUser;
		protected System.Web.UI.WebControls.Label lblServer_Pswd;
		protected System.Web.UI.WebControls.TextBox txtSqlPswd;
		protected System.Web.UI.WebControls.Label lblDataBase;
		protected System.Web.UI.WebControls.Button btnConn;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.TextBox txtPwdHide;
		protected System.Web.UI.WebControls.Button btnPri;
		protected System.Web.UI.WebControls.Label lblHelp;
		protected System.Web.UI.WebControls.Button btnRef;
		protected System.Web.UI.WebControls.CheckBox chkSetRpt;
		protected System.Web.UI.WebControls.CheckBox chkSetKM;
		protected System.Web.UI.WebControls.CheckBox chkSetRS;
        protected System.Web.UI.WebControls.DropDownList ddlCompNo;
        protected System.Web.UI.WebControls.CheckBox chkSetPop3;
		#endregion
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
                // ����������Ϣ
				LoadSqlServerSettings();
			}
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
			this.btnRef.Click += new System.EventHandler(this.btnRef_Click);
			this.btnPri.Click += new System.EventHandler(this.btnPri_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LoadSqlServerSettings()
		{
			string _sqlServer = String.Empty;
			string _sqlUser = String.Empty;
			string _sqlPwd = String.Empty;
			string _sqlComp = String.Empty;
			if (Session["SYSSqlServerSet"] != null)
			{
				// ��Session����������Ϣ�Ļ���ֱ��ȡSession����Ϣ
				if (Session["SYSSqlServerSet"] != null)
					_sqlServer = Session["SYSSqlServerSet"].ToString();
				if (Session["SetSqlUser"] != null)
					_sqlUser = Session["SetSqlUser"].ToString();
				if(Session["SetSqlPwd"] != null)
					_sqlPwd = Session["SetSqlPwd"].ToString();
				if(Session["SetSqlComp"] != null)
					_sqlComp = Session["SetSqlComp"].ToString();
			}
			else if (!string.IsNullOrEmpty(Comp.Conn_SunSystem ))
			{
				// �����������ַ��������ݲ�Ϊ�յĻ�������ȡ�ö�������Ϣ
				_sqlServer = Comp.GetConnectionStringField(Comp.Conn_SunSystem,"server");
				_sqlUser = Comp.GetConnectionStringField(Comp.Conn_SunSystem,"uid");
				_sqlPwd = Comp.GetConnectionStringField(Comp.Conn_SunSystem,"pwd");
				_sqlComp = ConfigurationManager.AppSettings["DatabaseName"];
			}
			else
			{
				// ��Session�����������ַ�����Ϊ�յĻ�����ˢ�º���һ����ť���óɲ�����״̬
				this.btnRef.Enabled = false;
				this.btnNext.Visible = false;
			}
			// �����ݿ�������Ϣ�����Ļ������������б��ʼ��
			if (_sqlServer.Length > 0 && _sqlUser.Length > 0)
			{
				try
				{
					string _sunConn = "server=" + _sqlServer + ";uid=" + _sqlUser + ";pwd=" + _sqlPwd + ";database=sunsystem";
					Query _query = new Query();	
					string _compSqlString = "select COMPNO,NAME from COMP";
					if (Comp.EnterModeValue != "2")
						_compSqlString += " WHERE DEP = '########'";
					DataTable _dt = _query.DoSQLString(_sunConn, _compSqlString).Tables[0];					
					this.ddlCompNo.DataSource = _dt;
					this.ddlCompNo.DataTextField = "NAME";
					this.ddlCompNo.DataValueField = "COMPNO";
					this.ddlCompNo.DataBind();
					if (_sqlComp != null && _sqlComp.Length > 0)
					{
						ListItem _compLi = ddlCompNo.Items.FindByValue(_sqlComp);
						if (_compLi != null)
							_compLi.Selected = true;
					}
					this.txtServer.Text = _sqlServer;
					this.txtSqlUser.Text = _sqlUser;
					this.txtSqlPswd.Text = _sqlPwd;
					this.txtPwdHide.Text = _sqlPwd;
				}
				catch
				{
					this.btnRef.Enabled = false;
					this.btnNext.Visible = false;
                    this.Alert("�޷��������Ͽ⣡");//���ݿ����������ʧ�ܣ�����WEB.CONFIG�ļ��Ƿ���ȷ
				}
			}			
		}

		private void btnConn_Click(object sender, System.EventArgs e)
		{
			this.SetConn();
		}

		private void SetConn()
		{
			this.lblMsg.Text = "";

			try
			{
				Query _query = new Query();

				#region ������ݿ�������Ƿ�SQL SERVER 2000 SP3���ϻ�SQL SERVER 2005

				string _sqlName = this.txtServer.Text;
				string _sqlUser = this.txtSqlUser.Text;
				string _sqlPwd = this.txtSqlPswd.Text;
				if (string.IsNullOrEmpty(_sqlName.Trim() ))
				{
                    lblMsg.Text = "����д���Ͽ����������!";//����д���ݿ����������!
					return;
				}

				string _connStr = "server="+_sqlName+";uid="+_sqlUser+";pwd="+_sqlPwd+";database=master";
				string _version = "";
				_version = _query.DoSQLString(_connStr, "select @@version").Tables[0].Rows[0][0].ToString();
			
				if (_version.IndexOf("9.00.") < 0)
				{
					if (_version.IndexOf("8.00.") >= 0)
					{				
						int _verPlace = _version.IndexOf("8.00.") + 5;
						int _verEnd = _version.IndexOf("(");
				  
						_version = _version.Substring(_verPlace,_verEnd-_verPlace-1);
						try
						{
							_verPlace = Convert.ToInt32(_version);
							if (_verPlace < 760)
							{
                                this.lblMsg.Text = "���鱾��SQL SERVER�Ƿ�װ���Ƿ�ΪSQL SERVER WITH SP3";//���鱾��SQL SERVER�Ƿ�װ���Ƿ�ΪSQL SERVER WITH SP3"
								return;
							}
						}
						catch
						{
                            this.lblMsg.Text = "���鱾��SQL SERVER�Ƿ�װ���Ƿ�ΪSQL SERVER WITH SP3";//���鱾��SQL SERVER�Ƿ�װ���Ƿ�ΪSQL SERVER WITH SP3"					
						}
					}
					else
					{
                        this.lblMsg.Text = "���鱾��SQL SERVER�Ƿ�װ���Ƿ�ΪSQL SERVER WITH SP3";//���鱾��SQL SERVER�Ƿ�װ���Ƿ�ΪSQL SERVER WITH SP3"
						return;
					}
				}

				#endregion

				bool _isPass = true;
				string _sunConn = "server=" + this.txtServer.Text + ";uid=" + this.txtSqlUser.Text
					+ ";pwd=" + this.txtSqlPswd.Text + ";database=sunsystem";
				try
				{
					_query.RunSql(_sunConn,"select COMPNO from COMP where 1<>1");
				}
				catch
				{
					_isPass = false;
				}
				if (_isPass)
				{
					string _compSqlString = "select COMPNO,NAME from COMP";
					if (Comp.EnterModeValue != "2")
						_compSqlString += " WHERE DEP = '########'";
					DataTable _dt = _query.DoSQLString(_sunConn,_compSqlString).Tables[0];					

					this.ddlCompNo.DataSource = _dt;
					this.ddlCompNo.DataTextField = "NAME";
					this.ddlCompNo.DataValueField = "COMPNO";
					this.ddlCompNo.DataBind();
					
					this.txtPwdHide.Text = this.txtSqlPswd.Text;	
					this.btnNext.Visible = true;
					this.btnRef.Enabled = true;
				}
				else
				{
					this.ddlCompNo.DataSource = null;
					this.btnNext.Visible = false;
					this.btnRef.Enabled = false;
                    this.lblMsg.Text = "�޷��������Ͽ⣡";
				}
			}
			catch(Exception _ex)
			{
				this.ddlCompNo.DataSource = null;
				this.btnNext.Visible = false;
				this.btnRef.Enabled = false;
                this.lblMsg.Text = "�޷��������Ͽ⣡<br>" + _ex.Message;//"�޷��������Ͽ�!�����û����������Ƿ���ȷ!<br>"
			}
		}
		private void btnNext_Click(object sender, System.EventArgs e)
		{
			//���COMP�������������Ϣ�Ƿ���ȷ
            Query _query = new Query();
            string _sunConn = "server=" + this.txtServer.Text + ";uid=" + this.txtSqlUser.Text
                    + ";pwd=" + this.txtPwdHide.Text + ";database=sunsystem";
            SunlikeDataSet _dsChk = null;
            string _compNo = ddlCompNo.SelectedItem.Value;
            try
            {
                _dsChk = _query.DoSQLString(_sunConn, "select * from COMP where COMPNO='" + _compNo + "'");
            }
            catch (Exception _ex)
            {
                lblMsg.Text = "����SunSystemʧ��<br>" + _ex.Message;
                //this.btnNext.Visible = false;
                return;
            }

            if (_dsChk.Tables[0].Rows.Count > 0)
            {
                string _host = this.txtServer.Text;
                string _db = "DB_"+_compNo;
                string _usr = this.txtSqlUser.Text;
                string _pwd = this.txtPwdHide.Text;
                if (string.Compare("bLaNk",_pwd) == 0 )
                {
                    _pwd = "";
                }
                try
                {
                    using (SqlConnection _conn = new SqlConnection())
                    {                        
                        _conn.ConnectionString = String.Format("server={0};uid={1};pwd={2};database={3}", _host, _usr, _pwd, _db);
                        _conn.Open();
                        if (_conn.State == ConnectionState.Open)
                        {
                            _conn.Close();
                        }
                    }
                }
                catch (Exception _ex)
                {
                    lblMsg.Text = "�����������ݿ�ʧ��<br>" + _ex.Message;
                    //this.btnNext.Visible = false;
                    return;
                }
            }
            
            Session["SYSSqlServerSet"] = this.txtServer.Text;
			Session["SetSqlUser"] = this.txtSqlUser.Text;
			Session["SetSqlPwd"] = this.txtPwdHide.Text;
            Session["SetSqlComp"] = _compNo;
            Session["SetSqlBarPrint"] = "DB_BARPRINT_" + _compNo;
            Response.Redirect("SYSRepServerSet.aspx");
		}

		private void btnPri_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("SYSUserSet.aspx");
		}

        private void btnRef_Click(object sender, System.EventArgs e)
        {
            #region �������к�ϵͳ���ݿ⼰��ر��
            string _compNo = "";
            if (ddlCompNo.SelectedItem != null)
            {
                _compNo = ddlCompNo.SelectedValue.ToString();
            }
            string _server = txtServer.Text;
            string _uid = txtSqlUser.Text;
            string _pwd = txtPwdHide.Text;
            string _barPrintDB = "DB_BARPRINT_" + _compNo;
            string _conn = "server=" + _server + ";uid=" + _uid + ";pwd=" + _pwd + ";";
            try
            {
                //����BarPrint���ݿ�
                string _sqlStr = "USE [DB_" + _compNo + "];select databasepropertyex('DB_" + _compNo + "','Collation')";
                using (SqlConnection _sqlConn = new SqlConnection(_conn))
                {
                    //ȡ���������ݿ���������Collation
                    _sqlConn.Open();
                    SqlCommand _sqlComm = _sqlConn.CreateCommand();
                    _sqlComm.CommandType = CommandType.Text;
                    _sqlComm.CommandText = _sqlStr;
                    SqlDataAdapter _sda = new SqlDataAdapter(_sqlComm);
                    DataSet _ds = new DataSet();
                    _sda.Fill(_ds);
                    string _langStr = _ds.Tables[0].Rows[0][0].ToString();
                    //�������ݿ�
                    System.Text.StringBuilder _sqlCreateDB = new System.Text.StringBuilder();
                    _sqlCreateDB.Append("USE master; IF DB_ID (N'" + _barPrintDB + "') IS NULL\n");

                    if (_langStr.Length > 0)
                        _sqlCreateDB.Append("CREATE DATABASE " + _barPrintDB + " COLLATE " + _langStr + " \n");
                    else
                        _sqlCreateDB.Append("CREATE DATABASE " + _barPrintDB + " \n");

                    _sqlComm = _sqlConn.CreateCommand();
                    _sqlComm.CommandType = CommandType.Text;
                    _sqlComm.CommandText = _sqlCreateDB.ToString();
                    _sqlComm.ExecuteNonQuery();

                    //�������к�Ʒ����Ϣ��
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
                        //��������Ϣ��
                        .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_BOX_BAT',N'U') IS NULL\n")
                        .Append("BEGIN\n")
                        .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_BOX_BAT]\n")
                        .Append("(\n")
                        .Append("[BOX_NO] VARCHAR(90) PRIMARY KEY CLUSTERED,\n")//������
                        .Append("[PRD_NO] VARCHAR(30),\n")//��¼�������Ӧ��Ʒ��
                        .Append("[PRD_MARK] VARCHAR(40),\n")//��¼�������Ӧ������
                        .Append("[BAT_NO] VARCHAR(100),\n")//��¼�������Ӧ������
                        .Append("[IDX_NAME] VARCHAR(50),\n")//��¼�����Ӧ��������������
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
                        //Ʒ�š���ӡ���ƶ�Ӧ��
                        .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_IDX',N'U') IS NULL\n")
                        .Append("BEGIN\n")
                        .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_IDX]\n")
                        .Append("(\n")
                        .Append("[ID] int identity(0,1) PRIMARY KEY CLUSTERED,\n")//�Զ����
                        .Append("[IDX_NO] VARCHAR(20),\n")//�������
                        .Append("[NAME] VARCHAR(50)\n")//������������
                        .Append(") ON [PRIMARY] END \n")
                        //���������滻
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
            }
            catch (Exception _ex)
            {
                this.Alert("���ݿ�ˢ��ʧ�ܣ�" + _ex.Message);
            }
            #endregion

            this.Alert("���ݿ�ˢ�����!");
        }

		#region ///����SQL by db
		private void RunSql(string fileName)
		{
			string _sqlPath = Server.MapPath(Request.ApplicationPath);
			string _sqlRepPath = _sqlPath + @"\PROCEDURES\"+fileName+".SQL";
                
			StreamReader _srRep = new StreamReader(_sqlRepPath, System.Text.Encoding.UTF8);
				
			string _sqlRep = _srRep.ReadToEnd();

			_srRep.Close();

			Query _query = new Query();
			string _setSqlComp = ddlCompNo.SelectedItem.Value;
			string _setSqlServer = this.txtServer.Text;//���ݿ����������
			string _setSqlUser = this.txtSqlUser.Text;//���ݿ��ʺ�
			string _setSqlPwd = this.txtPwdHide.Text;//���ݿ�����			
			string _conn = "server=" + _setSqlServer + ";uid=" + _setSqlUser + ";pwd=" + _setSqlPwd + ";database=DB_" + _setSqlComp;
            if (fileName == "SP_GetUserRight_YZB")
                _conn = "server=" + _setSqlServer + ";uid=" + _setSqlUser + ";pwd=" + _setSqlPwd + ";database=sunsystem";
			_query.RunSql(_conn,_sqlRep);
        }

        private void createProcedureYZB(string sqlFileName)
        {
            string _sqlPath = Server.MapPath(Request.ApplicationPath);
            string _sqlRepPath = _sqlPath + @"\PROCEDURES\" + sqlFileName + ".SQL";

            StreamReader _srRep = new StreamReader(_sqlRepPath, System.Text.Encoding.UTF8);

            string _sqlRep = _srRep.ReadToEnd();
            _srRep.Close();

            Query _query = new Query();
            string _setSqlComp = ddlCompNo.SelectedItem.Value;
            string _setSqlServer = this.txtServer.Text;//���ݿ����������
            string _setSqlUser = this.txtSqlUser.Text;//���ݿ��ʺ�
            string _setSqlPwd = this.txtPwdHide.Text;//���ݿ�����			
            string _conn = "server=" + _setSqlServer + ";uid=" + _setSqlUser + ";pwd=" + _setSqlPwd + ";database=DB_" + _setSqlComp;
            if (sqlFileName == "SP_GetUserRight_YZB")
                _conn = "server=" + _setSqlServer + ";uid=" + _setSqlUser + ";pwd=" + _setSqlPwd + ";database=sunsystem";
            string[] _arySql = _sqlRep.Split(';');
            for (int i = 0; i < _arySql.Length; i++)
            {
                if (_arySql[i].Trim() != "")
                {
                    _query.RunSql(_conn, _arySql[i]);
                }
            }
        }

        private void createWebParts(string sqlFileName)
        {
            string _sqlPath = Server.MapPath(Request.ApplicationPath);
            string _sqlRepPath = _sqlPath + @"\PROCEDURES\" + sqlFileName + ".SQL";

            StreamReader _srRep = new StreamReader(_sqlRepPath, System.Text.Encoding.UTF8);

            string _sqlRep = _srRep.ReadToEnd();
            _srRep.Close();

            Query _query = new Query();
            string _setSqlComp = ddlCompNo.SelectedItem.Value;
            string _setSqlServer = this.txtServer.Text;//���ݿ����������
            string _setSqlUser = this.txtSqlUser.Text;//���ݿ��ʺ�
            string _setSqlPwd = this.txtPwdHide.Text;//���ݿ�����			
            string _conn = "server=" + _setSqlServer + ";uid=" + _setSqlUser + ";pwd=" + _setSqlPwd + ";database=DB_" + _setSqlComp;
            string[] _arySql = _sqlRep.Split(';');
            for (int i = 0; i < _arySql.Length; i++)
            {
                if (_arySql[i].Trim() != "")
                {
                    _query.RunSql(_conn, _arySql[i]);
                }
            }
        }

        private void InsertSystemGroup()
        {
            string _langStr = this.GetDatabaseLanguage();
            string[] _aryGroupNo = new string[] { "EVERYONE", "SUNLIKE.GROU", "SUNLIKE.SALM", "SUNLIKE.CUST", "SUNLIKE.YG", "SUNLIKE.CSYG" };
            string[] _aryName = new string[] { "Everyone", "All Group's User", "All Salesman", "All Customer", "All Staffs", "All Customer's Staff" };
            if (_langStr == "zh-cn")
            {
                _aryName = new string[] { "�����û�", "���м����û�", "����ҵ��", "���пͻ�", "����Ա��", "���пͻ�Ա��" };
            }
            else if (_langStr == "zh-tw")
            {
                _aryName = new string[] { "�����Ñ�", "���м��F�Ñ�", "���ИI��", "���п͑�", "���ІT��", "���п͑�T��" };
            }
            string _sql = "";
            for (int i = 0; i < _aryGroupNo.Length; i++)
            {
                _sql += "if exists (select GROUP_NO from MY_GROUP where COMPNO='" + ddlCompNo.SelectedValue
                    + "' and GROUP_NO='" + _aryGroupNo[i] + "') \n"
                    + " update MY_GROUP set NAME='" + _aryName[i].Replace("'","''") + "',SYS_ID='T' where COMPNO='"
                    + ddlCompNo.SelectedValue + "' and GROUP_NO='" + _aryGroupNo[i] + "' \n"
                    + "else \n"
                    + " insert into MY_GROUP values ('" + ddlCompNo.SelectedValue + "','" + _aryGroupNo[i]
                    + "','" + _aryName[i].Replace("'", "''") + "',null,'T') \n";
            }
            Query _query = new Query();
            string _conn = "server=" + txtServer.Text + ";uid=" + txtSqlUser.Text
                + ";pwd=" + txtPwdHide.Text + ";database=sunsystem";
            _query.RunSql(_conn, _sql);
        }

        private void InsertFXPswd()
        {
            string _dbName = "DB_" + ddlCompNo.SelectedValue + "..";
            string _sql = "";
            _sql += "begin tran "
                //����һ����ɫ
                + "if exists (select top 1 ROLENO from ROLE where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ')  \n"
                + "     update ROLE set PUBLIC_ID='T',SUB_ID='F' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ'; \n"
                + " else \n"
                + "     insert into ROLE (COMPNO,ROLENO,[NAME],DEP,USR,PUBLIC_ID,SUB_ID) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','001_ROLE_MZ','00000000','','T','F'); \n"
                //����һ�ʵ�����Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='DRPIJ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='DRPIJ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','DRPIJ','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ��ԭ���±�Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_ORG_PRICE_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_ORG_PRICE_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','REP_ORG_PRICE_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ��ԭ���±�--����������ϸ��Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_OP_MZ_ML_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_OP_MZ_ML_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','REP_OP_MZ_ML_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ��ԭ���±�--�����ձ���ϸ��Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_OP_MZ_WR_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_OP_MZ_WR_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','REP_OP_MZ_WR_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ�ʲ��������±�Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MAT_INC_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MAT_INC_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MAT_INC_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ�ʲ�������--����������ϸ��Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_MZ_ML_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_MZ_ML_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MI_MZ_ML_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ�ʲ�������--�����ձ���ϸ��Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_MZ_WR_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_MZ_WR_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MI_MZ_WR_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ��ԭ�������루�ֿ⣩Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_WH_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_WH_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MI_WH_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ��ԭ�������루���䣩Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_WORK_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_WORK_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MI_WORK_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ���ڿ������Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_WH_STA_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_WH_STA_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_WH_STA_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ�ʵ��Ų������±�Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_BLV_MZ_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_BLV_MZ_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_BLV_MZ_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ���������±���Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_AW_MZ_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_AW_MZ_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_AW_MZ_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ�ʳɾ��ڿⱨ��Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_BAT_REC1_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_BAT_REC1_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_BAT_REC1_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //����һ���ڿ�Ʒͳ�Ʊ���Ȩ��
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_CJ_STA_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_CJ_STA_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_CJ_STA_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //������ɫ��Ա
                + " if exists (select top 1 ROLENO from PSWD_ROLE where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ') \n"
                + " begin \n"
                + "     delete from PSWD_ROLE where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ'; \n"
                + " end \n"
                + " insert into PSWD_ROLE (COMPNO,ROLENO,USR) select '" + ddlCompNo.SelectedValue + "','001_ROLE_MZ',USR from PSWD where COMPNO='" + ddlCompNo.SelectedValue + "'; \n"
                //�����������루�Զ��������ţ�
                + " if exists (select top 1 USR from " + _dbName + "FX_SPC_PSWD WHERE CTRL_ID='AUTO_NEW_BATNO') \n"
                + " begin \n"
                + "     delete from " + _dbName + "FX_SPC_PSWD WHERE CTRL_ID='AUTO_NEW_BATNO'; \n"
                + " end \n"
                + " insert into " + _dbName + "FX_SPC_PSWD (USR,CTRL_ID,SPC_ID,REM) select USR,'AUTO_NEW_BATNO','T','�Զ���������' from PSWD where COMPNO='" + ddlCompNo.SelectedValue + "'; \n"
                + " if @@error<>0 \n"
                + "     rollback tran \n"
                + " else \n"
                + "     commit tran ";
            Query _query = new Query();
            string _conn = "server=" + txtServer.Text + ";uid=" + txtSqlUser.Text
                + ";pwd=" + txtPwdHide.Text + ";database=sunsystem";
            _query.RunSql(_conn, _sql);
        }
		#endregion

		#region ȡ���ݿ����Ա�

		public string GetDatabaseLanguage()
		{
			string _langStr = "zh-cn";
			try
			{
				string _setSqlComp = ddlCompNo.SelectedItem.Value;
				string _setSqlServer = this.txtServer.Text;//���ݿ����������
				string _setSqlUser = this.txtSqlUser.Text;//���ݿ��ʺ�
				string _setSqlPwd = this.txtPwdHide.Text;//���ݿ�����			
				string _conn = "server=" + _setSqlServer + ";uid=" + _setSqlUser + ";pwd=" + _setSqlPwd + ";database=DB_" + _setSqlComp;
				Query _query = new Query();
				string _sqlString = "select databasepropertyex('DB_" + _setSqlComp + "','Collation')";
				DataSet _ds = _query.DoSQLString(_conn, _sqlString);
				_langStr = _ds.Tables[0].Rows[0][0].ToString().ToUpper();
				if (_langStr.IndexOf("CHINESE_PRC") >= 0 || _langStr.IndexOf("COMPATIBILITY_198") >= 0)
				{
					_langStr = "zh-cn";
				}
				else if (_langStr.IndexOf("CHINESE_TAIWAN") >= 0 || _langStr.IndexOf("COMPATIBILITY_196") >= 0)
				{
					_langStr = "zh-tw";
				}
				else
				{
					_langStr = "en-us";
				}
			}
			catch
			{
			}
			return _langStr;
		}

		#endregion
	}
}
