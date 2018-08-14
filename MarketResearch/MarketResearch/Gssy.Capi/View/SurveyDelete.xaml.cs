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
	// Token: 0x02000052 RID: 82
	public partial class SurveyDelete : Window
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x0000390E File Offset: 0x00001B0E
		public SurveyDelete()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000020CF File Offset: 0x000002CF
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			new CStart().Show();
			base.Close();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00092DF8 File Offset: 0x00090FF8
		private void method_0(object sender, RoutedEventArgs e)
		{
			if (SurveyMsg.SurveyRangePsw == SurveyMsg.SurveyRangeDemoPsw || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("浈諗灉")) >= 0 || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("漗砸灉")) >= 0 || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("@Ŧɯͮ")) >= 0)
			{
				this.PasswordMsg.Visibility = Visibility.Visible;
			}
			this.txtSurveyId.MaxLength = SurveyMsg.SurveyId_Length;
			this.txtSurveyId.Focus();
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00092E84 File Offset: 0x00091084
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = this.txtSurveyId.Text;
			if (text == global::GClass0.smethod_0(""))
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
			if (SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("漗砸灉")) < 0 && SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("浈諗灉")) < 0 && SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("@Ŧɯͮ")) < 0)
			{
				if (password == global::GClass0.smethod_0("") || password != SurveyMsg.SurveyRangePsw)
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
			string arg = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŉɰͰѳշٵ");
			string arg2 = Environment.CurrentDirectory + global::GClass0.smethod_0("YŀɢͶѠ");
			string string_ = string.Format(global::GClass0.smethod_0("vļɶ͖њճضݻࡄप੧ୣ౵"), arg, text);
			string string_2 = string.Format(global::GClass0.smethod_0("]ĕəͿѠժٛܮࠤ।੥ୢషൔ๕༺ၲᅱቋ፛ᑚᔼᙽᝢᠣ᥾᩿᭶᱕ᵚṳἵ⁻⅄∪⍧④╵"), arg2, DateTime.Now, text);
			this.method_1(string_, string_2);
			string_ = string.Format(global::GClass0.smethod_0("vļɶ͖њճضݻࡖप੧ୣ౵"), arg, text);
			string_2 = string.Format(global::GClass0.smethod_0("]ĕəͿѠժٛܮࠤ।੥ୢషൔ๕༺ၲᅱቋ፛ᑚᔼᙽᝢᠣ᥾᩿᭶᱕ᵚṳἵ⁻⅖∪⍧④╵"), arg2, DateTime.Now, text);
			this.method_1(string_, string_2);
			string_ = string.Format(global::GClass0.smethod_0("vļɶ͖њճضݻࡍप੧ୣ౵"), arg, text);
			string_2 = string.Format(global::GClass0.smethod_0("]ĕəͿѠժٛܮࠤ।੥ୢషൔ๕༺ၲᅱቋ፛ᑚᔼᙽᝢᠣ᥾᩿᭶᱕ᵚṳἵ⁻⅍∪⍧④╵"), arg2, DateTime.Now, text);
			this.method_1(string_, string_2);
			this.oSurveyImport.DeleteOneSurvey(text, global::GClass0.smethod_0(""));
			MessageBox.Show(string.Format(SurveyMsg.MsgDeleteOk, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000393D File Offset: 0x00001B3D
		private void method_1(string string_0, string string_1)
		{
			if (File.Exists(string_0))
			{
				File.Copy(string_0, string_1);
				File.Delete(string_0);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00002581 File Offset: 0x00000781
		private void passwordBox1_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000259E File Offset: 0x0000079E
		private void passwordBox1_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x040009E6 RID: 2534
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x040009E7 RID: 2535
		private SurveyImport oSurveyImport = new SurveyImport();

		// Token: 0x040009E8 RID: 2536
		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();
	}
}
