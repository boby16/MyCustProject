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
                // 加载连接信息
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
				// 若Session存有设置信息的话，直接取Session的信息
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
				// 若帐套连接字符串的内容不为空的话，从它取得对连接信息
				_sqlServer = Comp.GetConnectionStringField(Comp.Conn_SunSystem,"server");
				_sqlUser = Comp.GetConnectionStringField(Comp.Conn_SunSystem,"uid");
				_sqlPwd = Comp.GetConnectionStringField(Comp.Conn_SunSystem,"pwd");
				_sqlComp = ConfigurationManager.AppSettings["DatabaseName"];
			}
			else
			{
				// 若Session和帐套连接字符串都为空的话，把刷新和下一步按钮设置成不可用状态
				this.btnRef.Enabled = false;
				this.btnNext.Visible = false;
			}
			// 若数据库连接信息完整的话，进行帐套列表初始化
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
                    this.Alert("无法连接资料库！");//数据库服务器连接失败，请检查WEB.CONFIG文件是否正确
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

				#region 检查数据库服务器是否SQL SERVER 2000 SP3以上或SQL SERVER 2005

				string _sqlName = this.txtServer.Text;
				string _sqlUser = this.txtSqlUser.Text;
				string _sqlPwd = this.txtSqlPswd.Text;
				if (string.IsNullOrEmpty(_sqlName.Trim() ))
				{
                    lblMsg.Text = "请填写资料库服务器名称!";//请填写数据库服务器名称!
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
                                this.lblMsg.Text = "请检查本机SQL SERVER是否安装，是否为SQL SERVER WITH SP3";//请检查本机SQL SERVER是否安装，是否为SQL SERVER WITH SP3"
								return;
							}
						}
						catch
						{
                            this.lblMsg.Text = "请检查本机SQL SERVER是否安装，是否为SQL SERVER WITH SP3";//请检查本机SQL SERVER是否安装，是否为SQL SERVER WITH SP3"					
						}
					}
					else
					{
                        this.lblMsg.Text = "请检查本机SQL SERVER是否安装，是否为SQL SERVER WITH SP3";//请检查本机SQL SERVER是否安装，是否为SQL SERVER WITH SP3"
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
                    this.lblMsg.Text = "无法连接资料库！";
				}
			}
			catch(Exception _ex)
			{
				this.ddlCompNo.DataSource = null;
				this.btnNext.Visible = false;
				this.btnRef.Enabled = false;
                this.lblMsg.Text = "无法连接资料库！<br>" + _ex.Message;//"无法连接资料库!请检查用户名和密码是否正确!<br>"
			}
		}
		private void btnNext_Click(object sender, System.EventArgs e)
		{
			//检查COMP表的帐套连接信息是否正确
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
                lblMsg.Text = "连接SunSystem失败<br>" + _ex.Message;
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
                    lblMsg.Text = "连接帐套数据库失败<br>" + _ex.Message;
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
            #region 创建序列号系统数据库及相关表表
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
                //创建BarPrint数据库
                string _sqlStr = "USE [DB_" + _compNo + "];select databasepropertyex('DB_" + _compNo + "','Collation')";
                using (SqlConnection _sqlConn = new SqlConnection(_conn))
                {
                    //取得帐套数据库的排序规则Collation
                    _sqlConn.Open();
                    SqlCommand _sqlComm = _sqlConn.CreateCommand();
                    _sqlComm.CommandType = CommandType.Text;
                    _sqlComm.CommandText = _sqlStr;
                    SqlDataAdapter _sda = new SqlDataAdapter(_sqlComm);
                    DataSet _ds = new DataSet();
                    _sda.Fill(_ds);
                    string _langStr = _ds.Tables[0].Rows[0][0].ToString();
                    //创建数据库
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

                    //创建序列号品检信息表
                    System.Text.StringBuilder _sqlCreateTab = new System.Text.StringBuilder();
                    _sqlCreateTab.Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_QC',N'U') IS NULL\n")
                        .Append("BEGIN\n")
                        .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_QC]\n")
                        .Append("(\n")
                        .Append("[BAR_NO] VARCHAR(90) PRIMARY KEY CLUSTERED,\n")//序列号
                        .Append("[OS_BAR_NO] VARCHAR(90),\n")//来源产品序列号
                        .Append("[QC] VARCHAR(2),\n")//检验状态：1：合格；A:A类合格；B:B类合格；2:不合格；N:期初库存
                        .Append("[STATUS] VARCHAR(2),\n")//序列号状态:QC:检验；ML:领料；FT:分条; SW：送缴；TY：转异常；ST表示库存成品,这个不走正常流程；HH：换货
                        .Append("[SPC_NO] VARCHAR(10),\n")//不合格原因
                        .Append("[BAT_NO] VARCHAR(20),\n")//批号
                        .Append("[BIL_ID] VARCHAR(2),\n")//单据别
                        .Append("[BIL_NO] VARCHAR(20),\n")//单号
                        .Append("[WH_SJ] VARCHAR(12),\n")//送缴库位
                        .Append("[PRC_ID] VARCHAR(1),\n")//异常处理方式
                        .Append("[PRD_TYPE] VARCHAR(1),\n")//产品类别（1：板材；2：半成品；3：卷材）
                        .Append("[REM] VARCHAR(500),\n")//备注
                        .Append("[FORMAT] VARCHAR(1),\n")//打印版式
                        .Append("[LH] VARCHAR(100),\n")//板材打印料号
                        .Append("[PRT_NAME] VARCHAR(100),\n")//打印品名
                        .Append("[USR] VARCHAR(12),\n")//打印人员
                        .Append("[SYS_DATE] Datetime,\n")//打印时间
                        .Append("[USR_QC] VARCHAR(12),\n")//品检人员
                        .Append("[QC_DATE] Datetime,\n")//品检时间
                        .Append("[USR_BOX] VARCHAR(12),\n")//装箱人员
                        .Append("[BOX_DATE] Datetime,\n")//装箱时间
                        .Append("[USR_UNBOX] VARCHAR(12),\n")//拆箱人员
                        .Append("[UNBOX_DATE] Datetime,\n")//拆箱时间
                        .Append("[USR_SJ] VARCHAR(12),\n")//送缴人员
                        .Append("[SJ_DATE] Datetime,\n")//送缴时间
                        .Append("[USR_UNSJ] VARCHAR(12),\n")//撤销送缴人员
                        .Append("[UNSJ_DATE] Datetime,\n")//撤销送缴时间
                        .Append("[USR_WHBOX] VARCHAR(12),\n")//仓库装箱人员
                        .Append("[WHBOX_DATE] Datetime,\n")//仓库装箱时间
                        .Append("[USR_WHUNBOX] VARCHAR(12),\n")//仓库拆箱人员
                        .Append("[WHUNBOX_DATE] Datetime, \n")//仓库拆箱时间
                        .Append("[TO_CUST] VARCHAR(12) \n")//意向客户
                        .Append(") ON [PRIMARY] END \n")
                        //箱条码信息表
                        .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_BOX_BAT',N'U') IS NULL\n")
                        .Append("BEGIN\n")
                        .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_BOX_BAT]\n")
                        .Append("(\n")
                        .Append("[BOX_NO] VARCHAR(90) PRIMARY KEY CLUSTERED,\n")//箱条码
                        .Append("[PRD_NO] VARCHAR(30),\n")//记录箱条码对应的品号
                        .Append("[PRD_MARK] VARCHAR(40),\n")//记录箱条码对应的特征
                        .Append("[BAT_NO] VARCHAR(100),\n")//记录箱条码对应的批号
                        .Append("[IDX_NAME] VARCHAR(50),\n")//记录条码对应的特殊中类名称
                        .Append("[FORMAT] VARCHAR(1),\n")//打印版式 
                        .Append("[QC] VARCHAR(2),\n")//检验状态：1：合格；A:A类合格；B:B类合格；2:不合格；N:期初库存
                        .Append("[STATUS] VARCHAR(2),\n")//状态:SW：送缴
                        .Append("[SPC_NO] VARCHAR(10),\n")//不合格原因
                        .Append("[BIL_ID] VARCHAR(2),\n")//单据别
                        .Append("[BIL_NO] VARCHAR(20),\n")//单号
                        .Append("[WH_SJ] VARCHAR(12),\n")//送缴库位
                        .Append("[REM] VARCHAR(500),\n")//备注
                        .Append("[USR_PRT] VARCHAR(12)\n")//打印人员
                        .Append(") ON [PRIMARY] END \n")
                        //品号、打印名称对应表
                        .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.BAR_IDX',N'U') IS NULL\n")
                        .Append("BEGIN\n")
                        .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[BAR_IDX]\n")
                        .Append("(\n")
                        .Append("[ID] int identity(0,1) PRIMARY KEY CLUSTERED,\n")//自动编号
                        .Append("[IDX_NO] VARCHAR(20),\n")//中类代号
                        .Append("[NAME] VARCHAR(50)\n")//特殊中类名称
                        .Append(") ON [PRIMARY] END \n")
                        //销货条码替换
                        .Append("IF OBJECT_ID(N'" + _barPrintDB + ".dbo.SA_BAR_CHG',N'U') IS NULL\n")
                        .Append("BEGIN\n")
                        .Append("CREATE TABLE [" + _barPrintDB + "].[dbo].[SA_BAR_CHG]\n")
                        .Append("(\n")
                        .Append("[PS_ID] VARCHAR(2),\n")//单据别
                        .Append("[PS_NO] VARCHAR(20),\n")//单据号码
                        .Append("[PS_ITM] INT,\n")//进/销/退/折项次
                        .Append("[ITM] INT,\n")//项次
                        .Append("[BAR_CODE] VARCHAR(90),\n")//序列号
                        .Append("[BOX_NO] VARCHAR(90),\n")//箱条码
                        .Append("[USR_QC] VARCHAR(12),\n")//品检人员
                        .Append("[QC_DATE] Datetime,\n")//品检时间
                        .Append("[USR_CHG] VARCHAR(12),\n")//替换人员
                        .Append("[CHG_DATE] Datetime,\n")//替换时间
                        .Append("[CHG_ID] VARCHAR(1),\n")//是否已替换
                        .Append("[CHG_SPC_NO] VARCHAR(10),\n")//替换原因
                        .Append("[REM] VARCHAR(500),\n")//备注
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
                this.Alert("数据库刷新失败：" + _ex.Message);
            }
            #endregion

            this.Alert("数据库刷新完毕!");
        }

		#region ///运行SQL by db
		private void RunSql(string fileName)
		{
			string _sqlPath = Server.MapPath(Request.ApplicationPath);
			string _sqlRepPath = _sqlPath + @"\PROCEDURES\"+fileName+".SQL";
                
			StreamReader _srRep = new StreamReader(_sqlRepPath, System.Text.Encoding.UTF8);
				
			string _sqlRep = _srRep.ReadToEnd();

			_srRep.Close();

			Query _query = new Query();
			string _setSqlComp = ddlCompNo.SelectedItem.Value;
			string _setSqlServer = this.txtServer.Text;//数据库服务器名称
			string _setSqlUser = this.txtSqlUser.Text;//数据库帐号
			string _setSqlPwd = this.txtPwdHide.Text;//数据库密码			
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
            string _setSqlServer = this.txtServer.Text;//数据库服务器名称
            string _setSqlUser = this.txtSqlUser.Text;//数据库帐号
            string _setSqlPwd = this.txtPwdHide.Text;//数据库密码			
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
            string _setSqlServer = this.txtServer.Text;//数据库服务器名称
            string _setSqlUser = this.txtSqlUser.Text;//数据库帐号
            string _setSqlPwd = this.txtPwdHide.Text;//数据库密码			
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
                _aryName = new string[] { "所有用户", "所有集团用户", "所有业务", "所有客户", "所有员工", "所有客户员工" };
            }
            else if (_langStr == "zh-tw")
            {
                _aryName = new string[] { "所有用戶", "所有集團用戶", "所有業務", "所有客戶", "所有員工", "所有客戶員工" };
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
                //新增一个角色
                + "if exists (select top 1 ROLENO from ROLE where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ')  \n"
                + "     update ROLE set PUBLIC_ID='T',SUB_ID='F' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ'; \n"
                + " else \n"
                + "     insert into ROLE (COMPNO,ROLENO,[NAME],DEP,USR,PUBLIC_ID,SUB_ID) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','001_ROLE_MZ','00000000','','T','F'); \n"
                //新增一笔调整单权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='DRPIJ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='DRPIJ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','DRPIJ','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔原价月报权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_ORG_PRICE_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_ORG_PRICE_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','REP_ORG_PRICE_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔原价月报--生产领料明细表权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_OP_MZ_ML_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_OP_MZ_ML_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','REP_OP_MZ_ML_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔原价月报--生产日报明细表权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_OP_MZ_WR_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='REP_OP_MZ_WR_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','REP_OP_MZ_WR_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔材料收入月报权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MAT_INC_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MAT_INC_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MAT_INC_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔材料收入--生产领料明细表权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_MZ_ML_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_MZ_ML_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MI_MZ_ML_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔材料收入--生产日报明细表权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_MZ_WR_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_MZ_WR_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MI_MZ_WR_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔原材料收入（仓库）权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_WH_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_WH_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MI_WH_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔原材料收入（车间）权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_WORK_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_MI_WORK_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_MI_WORK_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔在库情况表权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_WH_STA_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_WH_STA_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_WH_STA_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔单张产成率月报权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_BLV_MZ_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_BLV_MZ_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_BLV_MZ_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔损耗面积月报表权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_AW_MZ_LST') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_AW_MZ_LST'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_AW_MZ_LST','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔成卷在库报表权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_BAT_REC1_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_BAT_REC1_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_BAT_REC1_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增一笔在库品统计报表权限
                + " if exists (select top 1 ROLENO from FX_PSWD where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_CJ_STA_MZ') \n"
                + "     update FX_PSWD set QRY='0',INS='Y',UPD='0',DEL='0',PRN='0',PROPERTY='Y',U_AMT='W',R_CST='W',LCK='0' where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ' and PGM='RPT_CJ_STA_MZ'; \n"
                + " else \n"
                + "     insert into FX_PSWD (COMPNO,ROLENO,PGM,QRY,INS,UPD,DEL,PRN,PROPERTY,U_AMT,R_CST,LCK) values ('" + ddlCompNo.SelectedValue + "','001_ROLE_MZ','RPT_CJ_STA_MZ','0','Y','0','0','0','Y','W','W','0'); \n"
                //新增角色成员
                + " if exists (select top 1 ROLENO from PSWD_ROLE where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ') \n"
                + " begin \n"
                + "     delete from PSWD_ROLE where COMPNO='" + ddlCompNo.SelectedValue + "' and ROLENO='001_ROLE_MZ'; \n"
                + " end \n"
                + " insert into PSWD_ROLE (COMPNO,ROLENO,USR) select '" + ddlCompNo.SelectedValue + "','001_ROLE_MZ',USR from PSWD where COMPNO='" + ddlCompNo.SelectedValue + "'; \n"
                //新增特殊密码（自动新增批号）
                + " if exists (select top 1 USR from " + _dbName + "FX_SPC_PSWD WHERE CTRL_ID='AUTO_NEW_BATNO') \n"
                + " begin \n"
                + "     delete from " + _dbName + "FX_SPC_PSWD WHERE CTRL_ID='AUTO_NEW_BATNO'; \n"
                + " end \n"
                + " insert into " + _dbName + "FX_SPC_PSWD (USR,CTRL_ID,SPC_ID,REM) select USR,'AUTO_NEW_BATNO','T','自动新增批号' from PSWD where COMPNO='" + ddlCompNo.SelectedValue + "'; \n"
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

		#region 取数据库语言别

		public string GetDatabaseLanguage()
		{
			string _langStr = "zh-cn";
			try
			{
				string _setSqlComp = ddlCompNo.SelectedItem.Value;
				string _setSqlServer = this.txtServer.Text;//数据库服务器名称
				string _setSqlUser = this.txtSqlUser.Text;//数据库帐号
				string _setSqlPwd = this.txtPwdHide.Text;//数据库密码			
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
