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

                // �������ݿ������
                string _setSqlServer = Session["SYSSqlServerSet"].ToString();	//���ݿ����������
                string _setSqlUser = Session["SetSqlUser"].ToString();		//���ݿ��ʺ�
                string _setSqlPwd = Session["SetSqlPwd"].ToString();		//���ݿ�����
                string _setSqlComp = Session["SetSqlComp"].ToString();		//������
                string _setSqlBarPrint = Session["SetSqlBarPrint"].ToString();//�������ݿ�
                string _connStr = "Application Name=ONLINE;server=" + _setSqlServer + ";uid=" + _setSqlUser + ";pwd=" + Security.EncodingPswd(_setSqlPwd) + ";";
                _webConfig.SetReport("ConnectionString", _connStr);
                _webConfig.SetReport("DatabaseName", _setSqlComp);
                _webConfig.SetReport("BarPrintDB", _setSqlBarPrint);

                //д��LoginInfo�ĳ�ʱʱ�䣨��λ�����ӣ�
                _webConfig.SetReport("LoginInfoTimeout", LoginInfo.SpacingMinute.ToString());

                // ���渨����Ϣ
                _webConfig.SetReport("AltTime", "900000");
                _webConfig.SetReport("DataDynamicsARLic", "DRP,SUNLIKE,Lo-t-us-LX,w4U7WHVWMFEO48K99WH4");
                _webConfig.SetReport("BarcodeUpdCheck", "F");

                // �޸��ļ�
                _webConfig.Save(Comp.ImpersonateUsr, Comp.ImpersonateUsrPswd, Comp.ImpersonateUsrDomain);

                UpdateAccessDateTime();
                // ���Session
                Session.Clear();
                FormsAuthentication.SignOut();
                ConfigurationManager.RefreshSection("AppSettings");
                //Response.Redirect("Login.aspx",false);
                Alert("����ɹ���");
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
            //�ж��Ƿ��ѳ�ʱ
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
            // �O��[�Ĺ�����]��
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

        #region ����SQL by db
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
