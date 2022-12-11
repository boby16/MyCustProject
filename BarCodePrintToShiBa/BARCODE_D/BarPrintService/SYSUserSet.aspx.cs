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
using System.Web.Security;
using Sunlike.Business;
using Sunlike.Common.Utility;
using Microsoft.Win32;

namespace Sunlike.Web.DRP
{
	/// <summary>
	/// Summary description for SYSUserSet.
	/// </summary>
    public class SYSUserSet : Sunlike.Web.UI.SunlikePage
	{
		#region control

		protected System.Web.UI.WebControls.Label lblAccnGroup;
		protected System.Web.UI.WebControls.TextBox txtAccnGroup;
		protected System.Web.UI.WebControls.Label lblAccnCust;
		protected System.Web.UI.WebControls.TextBox txtAccnCust;	
		protected System.Web.UI.WebControls.Label lblUser;
		protected System.Web.UI.WebControls.TextBox txtUser;
		protected System.Web.UI.WebControls.Label lblPswd;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.Label lblDomain;
		protected System.Web.UI.WebControls.TextBox txtDomain;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.Button btnSetValue;
		protected System.Web.UI.WebControls.Label lblMsg;
		protected System.Web.UI.WebControls.TextBox txtHide;
		#endregion
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (string.IsNullOrEmpty(Comp.ImpersonateUsr))
				{
					btnNext.Visible = false;
				}
				LoadWebConfig();
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
			this.btnSetValue.Click += new System.EventHandler(this.btnSetValue_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void LoadWebConfig()
		{
			txtUser.Text = Comp.ImpersonateUsr;
			txtHide.Text = Comp.ImpersonateUsrPswd;
			txtDomain.Text = Comp.ImpersonateUsrDomain;
		}


		private void btnSetValue_Click(object sender, System.EventArgs e)
		{
			if (String.IsNullOrEmpty(this.txtPassword.Text) && String.IsNullOrEmpty(this.txtHide.Text))
			{
                this.lblMsg.Text = "密码不能为空！";
				this.btnNext.Enabled = false;
			}
			else
			{
				bool _imperUsr = false;
				bool _isOld = false;
				ConfigSetting SunlikeConfig = new ConfigSetting(Request.MapPath("")+"/web.config");
				if ((!string.IsNullOrEmpty(txtHide.Text)) && (string.IsNullOrEmpty(txtPassword.Text )) )
				{
					_imperUsr = SunlikeConfig.CheckWindowUser(txtUser.Text,txtHide.Text,txtDomain.Text);
					_isOld = true;
				}
				else
				{
					_imperUsr = SunlikeConfig.CheckWindowUser(txtUser.Text,txtPassword.Text,txtDomain.Text);
					_isOld = false;
				}			
				if (_imperUsr)
				{
					System.Security.Principal.IPrincipal wp = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent());
					if (wp.IsInRole("BUILTIN\\Administrators"))
					{
						if(!_isOld)
						{
							Comp.ImpersonateUsr= txtUser.Text;
							Comp.ImpersonateUsrPswd = txtPassword.Text;
							Comp.ImpersonateUsrDomain = txtDomain.Text;
						}

                        lblMsg.Text = "模拟成功！";
						btnNext.Visible = true;
						this.btnNext.Enabled = true;
					}
					else
					{
                        lblMsg.Text = "用户必须是管理员，该用户权限不足！";
						this.btnNext.Enabled = false;
					}		
				}
				else
				{
                    lblMsg.Text = "模拟失败！请检查用户名、密码和域名是否正确。";
					this.btnNext.Enabled = false;
				}
			}
		}


		private void btnNext_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("SYSSqlServerSet.aspx");
		}

	}
}
