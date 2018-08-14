using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Gssy.Capi.Class;

namespace Gssy.Capi.QEdit
{
	// Token: 0x02000057 RID: 87
	public partial class CheckPsw : Window
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x00003B2D File Offset: 0x00001D2D
		public CheckPsw()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00003B3B File Offset: 0x00001D3B
		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.passwordBox1.Focus();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00003B5C File Offset: 0x00001D5C
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000991E4 File Offset: 0x000973E4
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string password = this.passwordBox1.Password;
			if (!(password == global::GClass0.smethod_0("")) && !(password != SurveyMsg.SurveyRangePsw))
			{
				SurveyMsg.SurveyRangePswOk = true;
				base.Close();
				return;
			}
			MessageBox.Show(SurveyMsg.MsgRangePswError, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			this.passwordBox1.Focus();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00003B64 File Offset: 0x00001D64
		private void passwordBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSave_Click(sender, e);
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00002581 File Offset: 0x00000781
		private void passwordBox1_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000259E File Offset: 0x0000079E
		private void passwordBox1_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000234B File Offset: 0x0000054B
		private void btnKeyboard_Click(object sender, RoutedEventArgs e)
		{
			SurveyTaptip.ShowInputPanel();
		}
	}
}
