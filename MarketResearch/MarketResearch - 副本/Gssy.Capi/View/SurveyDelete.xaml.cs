using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.DAL;

namespace Gssy.Capi.View
{
	public partial class SurveyDelete : Window
	{
		public SurveyDelete()
		{
			this.InitializeComponent();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			new CStart().Show();
			base.Close();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			if (SurveyMsg.SurveyRangePsw == SurveyMsg.SurveyRangeDemoPsw || SurveyMsg.VersionID.IndexOf("测试版") >= 0 || SurveyMsg.VersionID.IndexOf("演示版") >= 0 || SurveyMsg.VersionID.IndexOf("Demo") >= 0)
			{
				this.PasswordMsg.Visibility = Visibility.Visible;
			}
			this.txtSurveyId.MaxLength = SurveyMsg.SurveyId_Length;
			this.txtSurveyId.Focus();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtSurveyId.Text;
			if (text == "")
			{
				MessageBox.Show(SurveyMsg.MsgDeleteMissSurveyId, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtSurveyId.Focus();
				return;
			}
			if (text.Length != SurveyMsg.SurveyId_Length)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgRangeLength, SurveyMsg.SurveyId_Length.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.txtSurveyId.Focus();
				return;
			}
			if (!this.oSurveyMainDal.ExistsBySurveyID(text))
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgDeleteNotExist, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.txtSurveyId.Focus();
				return;
			}
			string password = this.passwordBox1.Password;
			if (SurveyMsg.VersionID.IndexOf("演示版") < 0 && SurveyMsg.VersionID.IndexOf("测试版") < 0 && SurveyMsg.VersionID.IndexOf("Demo") < 0)
			{
				if (password == "" || password != SurveyMsg.SurveyRangePsw)
				{
					MessageBox.Show(SurveyMsg.MsgRangeMissPsw, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.passwordBox1.Focus();
					return;
				}
			}
			else if (password != SurveyMsg.SurveyRangeDemoPsw)
			{
				MessageBox.Show(SurveyMsg.MsgRangeMissPsw, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.passwordBox1.Focus();
				return;
			}
			string arg = Environment.CurrentDirectory + "\\Output";
			string arg2 = Environment.CurrentDirectory + "\\Data";
			string string_ = string.Format("{0}\\S{1}A.dat", arg, text);
			string string_2 = string.Format("{0}\\BK{1:yyy-MM-dd_HH-mm-ss}_S{2}A.dat", arg2, DateTime.Now, text);
			this.method_1(string_, string_2);
			string_ = string.Format("{0}\\S{1}S.dat", arg, text);
			string_2 = string.Format("{0}\\BK{1:yyy-MM-dd_HH-mm-ss}_S{2}S.dat", arg2, DateTime.Now, text);
			this.method_1(string_, string_2);
			string_ = string.Format("{0}\\S{1}H.dat", arg, text);
			string_2 = string.Format("{0}\\BK{1:yyy-MM-dd_HH-mm-ss}_S{2}H.dat", arg2, DateTime.Now, text);
			this.method_1(string_, string_2);
			this.oSurveyImport.DeleteOneSurvey(text, "");
			MessageBox.Show(string.Format(SurveyMsg.MsgDeleteOk, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void method_1(string string_0, string string_1)
		{
			if (File.Exists(string_0))
			{
				File.Copy(string_0, string_1);
				File.Delete(string_0);
			}
		}

		private void passwordBox1_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void passwordBox1_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private SurveyImport oSurveyImport = new SurveyImport();

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();
	}
}
