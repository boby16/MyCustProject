using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Sunlike.Common.Utility;
using Sunlike.AuthWS;
using Sunlike.AuthWS.RegWS;
using Sunlike.Business;

namespace Sunlike.Web.DRP
{
	/// <summary>
	/// Summary description for Register.
	/// </summary>
    public class Register : Sunlike.Web.UI.SunlikePage
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblUserID;
		protected System.Web.UI.WebControls.Label lblAlert;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnLogin;
        protected System.Web.UI.WebControls.TextBox txtRegKey;
		protected System.Web.UI.WebControls.Label lblRegKey;
		protected System.Web.UI.WebControls.Label lblServer;
		protected System.Web.UI.WebControls.Label lblPort;
		protected System.Web.UI.WebControls.Label lblUser;
		protected System.Web.UI.WebControls.Label lblDomain;
		protected System.Web.UI.WebControls.TextBox txtServer;
		protected System.Web.UI.WebControls.TextBox txtPort;
		protected System.Web.UI.WebControls.TextBox txtUser;
		protected System.Web.UI.WebControls.TextBox txtDomain;
		protected System.Web.UI.WebControls.Label lblPswd;
		protected System.Web.UI.WebControls.TextBox txtPswd;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trMAC;
		//protected System.Web.UI.WebControls.RadioButtonList rblMAC;
		protected System.Web.UI.WebControls.Label lblProxy;
		protected System.Web.UI.WebControls.Button btnSubmit;

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
			//this.rblMAC.SelectedIndexChanged += new System.EventHandler(this.rblMAC_SelectedIndexChanged);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

       
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
                Sunlike.AuthWS.Register _reg = new Sunlike.AuthWS.Register();
                _reg.UserCredential = new System.Net.NetworkCredential();
                _reg.UserCredential.Domain = Comp.ImpersonateUsrDomain;
                _reg.UserCredential.UserName = Comp.ImpersonateUsr;
                _reg.UserCredential.Password = Comp.ImpersonateUsrPswd;
                try
                {
                    if (String.IsNullOrEmpty(this.Request.QueryString["SkipLoad"]))
                        _reg.LoadRegFile(false);
                }
                catch (Exception _ex)
                {
                    if (_ex is System.IO.InvalidDataException)
                    {
                        System.Web.Security.FormsAuthentication.SetAuthCookie("sunlike", false);
                        this.Alert("注册文件读取失败，请检查");
                    }
                }
                this.txtRegKey.Text = _reg.Serial_NO;
                if (_reg.Proxy != null)
                {
                    if (_reg.Proxy.Address.OriginalString.Length != 0)
                    {
                        this.txtServer.Text = _reg.Proxy.Address.Host;
                        this.txtPort.Text = _reg.Proxy.Address.Port.ToString();
                    }
                    if (_reg.Proxy.Credentials != null)
                    {
                        System.Net.NetworkCredential nc = (System.Net.NetworkCredential)_reg.Proxy.Credentials;
                        txtUser.Text = nc.UserName;
                        txtPswd.Text = nc.Password;
                        txtDomain.Text = nc.Domain;
                    }
                }
                
			}
		}



		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
            System.Diagnostics.Trace.WriteLine("Register.aspx.btnSubmit_Click");
            System.Net.WebProxy _proxy = null;            
            if (this.txtServer.Text.Length > 0)
            {
                string _tmpServer = this.txtServer.Text;
                if (_tmpServer.IndexOf("http://") == -1)
                    _tmpServer = "http://" + _tmpServer;
                _proxy = new System.Net.WebProxy(_tmpServer + (this.txtPort.Text.Length > 0 ? ":" + this.txtPort.Text : ""));
                if (this.txtUser.Text.Length > 0)
                {
                    if (this.txtDomain.Text.Length > 0)
                        _proxy.Credentials = new System.Net.NetworkCredential(this.txtUser.Text, this.txtPswd.Text, this.txtDomain.Text);
                    else
                        _proxy.Credentials = new System.Net.NetworkCredential(this.txtUser.Text, this.txtPswd.Text);
                }
                else
                    _proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }


            string errMsg = string.Empty;

            Sunlike.AuthWS.Register _register = new Sunlike.AuthWS.Register();
            _register.UserCredential = new System.Net.NetworkCredential();
            _register.UserCredential.Domain = Comp.ImpersonateUsrDomain;
            _register.UserCredential.UserName = Comp.ImpersonateUsr;
            _register.UserCredential.Password = Comp.ImpersonateUsrPswd;
            
            Attn_REG _resultReg = null;
            try
            {
                System.Diagnostics.Trace.WriteLine("Register.aspx.CallRegWS");
                _resultReg = _register.CallRegWS(this.txtRegKey.Text.Trim(), _proxy,false);
            }
            catch (Exception _ex)
            {
                if (_ex is System.Net.WebException)
                    errMsg = _ex.Message.Replace("'", "") +
                        "\r\n" + "请检查：\r\n1.DNS 设定是否正确\r\n2.连接外网是否需要设定代理服务器(Proxy)";//res.GetResource("SYS.REGISTER.WEBEXCEPTION");  
                else
                    errMsg = _ex.Message;
                //无法解析DNS + 请检查：1。网路是否正常连接  2。DNS 设定是否正确  3。连接外网是否需要设定Proxy，如果需要请输入Proxy的资料。

                if ((_ex.Message == "IMPERSONATE_ERROR") || (_ex is System.UnauthorizedAccessException))
                {
                    errMsg = _ex.Message.Replace("'", "") + "模拟用户运行出错, 请重新设定.";
                    this.Alert(errMsg, "../SYSUserSet.aspx");
                }
                else
                    this.Alert(errMsg, false);
                return;
            }


            if (_resultReg == null)
            {
                errMsg = "返回的资料为空，请检查！！";
                this.Alert(errMsg, false);
                return;

            }

            //if (_resultReg.ResultID != 0)  //表示有錯誤發生
            //{                
            //    switch (_resultReg.ResultID)
            //    {
            //        case 1:
            //            errMsg = "序列号或客户代号已经停用";
            //            break;
            //        case 2:   //WS:傳進來的用戶數大於資料庫的用戶數
            //            errMsg = "系统注册讯息和资料库不一致";
            //            break;
            //        case 3:   //"傳進來的版本為集團版但資料庫非集團版"
            //        case 4:   //"傳進來的模組數大於資料庫的模組數";
            //        case 5:   //"傳進來的有效天數大於資料庫的有效天數";
            //        case 6:   //"資料庫中的模組沒有構買但是傳進來的模組是有購買的";
            //            errMsg = res.GetResource("SYS.REGISTER.RESULT02"/*系统注册讯息和资料库不一致*/);
            //            break;
            //        case 7:
            //            errMsg = res.GetResource("SYS.REGISTER.RESULT07"/*系統有效日已經過期*/);
            //            break;
            //        case 8:
            //            errMsg = res.GetResource("SYS.REGISTER.RESULT08"/*硬体可能變更或是註冊文件遭非法修改*/);
            //            break;
            //        case 9:
            //            errMsg = res.GetResource("SYS.REGISTER.RESULT09"/*注册失败,序列号无法找到*/);
            //            break;
            //        case 10:
            //            errMsg = res.GetResource("SYS.REGISTER.RESULT10"/*连续14天无法进行校验，请连接上网后进行注册校验*/);
            //            break;
            //        case 11:
            //            errMsg = res.GetResource("SYS.REGISTER.RESULT11"/*客户端日期和服务器段日期不一致，请检查*/);
            //            break;
            //    }
            //    this.Alert(errMsg,false);
            //}
            //else
            {
                // 清除教育版全局变量
                Application.Lock();
                Application["isEductation"] = string.Empty;
                Application.UnLock();

                Alert("注册成功");
                
            }
		}

	}
}
