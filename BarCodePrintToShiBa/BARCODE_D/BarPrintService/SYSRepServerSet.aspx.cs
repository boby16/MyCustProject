using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Sunlike.Business;
using Sunlike.Common.Utility;
using System.IO;
using Sunlike.Common.CommonVar;
using System.Configuration;
using System.Net;
using System.Threading;


namespace Sunlike.Web.DRP
{
    /// <summary>
    /// Summary description for SYSRepServerSet.
    /// </summary>
    public class SYSRepServerSet : Sunlike.Web.UI.SunlikePage
    {
        #region Controls

        protected System.Web.UI.WebControls.Label lblMsg;
        protected System.Web.UI.WebControls.Button btnSave;
        protected System.Web.UI.WebControls.Label lblHelp;
        protected System.Web.UI.WebControls.Button btnPre;

        #endregion

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
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region PageEvent
        private void Page_Load(object sender, System.EventArgs e)
        {
            #region IsPostBack

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Comp.ImpersonateUsr))
                {
                    this.btnSave.Visible = false;
                }
            }

            #endregion
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            ConfigSetting _webConfig = new ConfigSetting(Request.MapPath("./") + "web.config");
            try
            {

                _webConfig.SetReport("Usr", Security.EncodingPswd(Comp.ImpersonateUsr));
                _webConfig.SetReport("Pwd", Security.EncodingPswd(Comp.ImpersonateUsrPswd));
                _webConfig.SetReport("Domain", Security.EncodingPswd(Comp.ImpersonateUsrDomain));

                // 保存数据库服务器
                string _setSqlServer = Session["SYSSqlServerSet"].ToString();	//数据库服务器名称
                string _setSqlUser = Session["SetSqlUser"].ToString();		//数据库帐号
                string _setSqlPwd = Session["SetSqlPwd"].ToString();		//数据库密码
                string _setSqlComp = Session["SetSqlComp"].ToString();		//帐套名
                string _setSqlBarPrint = Session["SetSqlBarPrint"].ToString();//条码数据库
                string _connStr = "Application Name=ONLINE;server=" + _setSqlServer + ";uid=" + _setSqlUser + ";pwd=" + Security.EncodingPswd(_setSqlPwd) + ";";
                _webConfig.SetReport("ConnectionString", _connStr);
                _webConfig.SetReport("DatabaseName", _setSqlComp);
                _webConfig.SetReport("BarPrintDB", _setSqlBarPrint);

                //写入LoginInfo的超时时间（单位：分钟）
                _webConfig.SetReport("LoginInfoTimeout", LoginInfo.SpacingMinute.ToString());

                // 保存辅助信息
                _webConfig.SetReport("AltTime", "900000");
                _webConfig.SetReport("DataDynamicsARLic", "DRP,SUNLIKE,Lo-t-us-LX,w4U7WHVWMFEO48K99WH4");
                _webConfig.SetReport("BarcodeUpdCheck", "F");

                // 修改文件
                _webConfig.Save(Comp.ImpersonateUsr, Comp.ImpersonateUsrPswd, Comp.ImpersonateUsrDomain);

                UpdateAccessDateTime();
                // 清除Session
                Session.Clear();
                FormsAuthentication.SignOut();
                ConfigurationManager.RefreshSection("AppSettings");
                //Response.Redirect("Login.aspx",false);
                Alert("保存成功！");
                btnSave.Enabled = false;
            }
            catch (Exception _ex)
            {
                base.Alert(_ex.Message.ToString());
            }
        }

        private void UpdateAccessDateTime()
        {
            LoginInfo _loginInfo = new LoginInfo();
            //判断是否已超时
            if (Request.Cookies["loginid"] != null && !_loginInfo.checkState(Request.Cookies["loginid"].Value))
            {
                if (Request.Cookies["loginid"] != null)
                {
                    _loginInfo.Logout(Request.Cookies["loginid"].Value);
                }
            }
        }

        private void btnPre_Click(object sender, System.EventArgs e)
        {
            // 設定[文管中心]否
            if (Session["SetKM"] != null && Convert.ToBoolean(Session["SetKM"]))
            {
                Response.Redirect("KMMediaServerSet.aspx");
            }
            else if (Session["SetPOP3"] != null && Convert.ToBoolean(Session["SetPOP3"]))
            {
                Response.Redirect("SYSPop3Set.aspx");
            }
            else
                Response.Redirect("SYSSqlServerSet.aspx");
        }
        #endregion

        #region 运行SQL by db
        private void RunSql(string connStr, string fileName)
        {
            string _sqlPath = Server.MapPath(Request.ApplicationPath);
            string _sqlRepPath = _sqlPath + @"\PROCEDURES\" + fileName + ".SQL";

            StreamReader _srRep = new StreamReader(_sqlRepPath, System.Text.Encoding.UTF8);

            string _sqlRep = ChineseConveter.ConvertByServerLocale(_srRep.ReadToEnd());

            _srRep.Close();

            Query _query = new Query();
            try
            {
                _query.RunSql(connStr, _sqlRep);
            }
            catch (Exception _ex)
            {
                this.Alert(_ex.Message.ToString());
            }
        }

        #endregion
    }
}
