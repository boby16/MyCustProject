using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;

namespace Gssy.Capi.QEdit
{
	// Token: 0x0200005B RID: 91
	public partial class FillMode : Window
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00003C4B File Offset: 0x00001E4B
		public FillMode()
		{
			this.InitializeComponent();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0009AA6C File Offset: 0x00098C6C
		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("NŮɪͩщլ٦ݤ"));
			string byCodeText2 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("_ſɥ͹юծ٪ݩࡔॢ੥୤"));
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			this.listBtn.Add(this.btn1);
			this.listBtn.Add(this.btn2);
			this.listBtn.Add(this.btn3);
			foreach (Button button in this.listBtn)
			{
				button.Style = style2;
			}
			if (byCodeText == global::GClass0.smethod_0("0"))
			{
				this.btn1.Style = style;
			}
			else if (byCodeText == global::GClass0.smethod_0("3"))
			{
				this.btn2.Style = style;
			}
			else
			{
				this.btn3.Style = style;
			}
			this.StopFillPageId.Text = byCodeText2;
			this.Capture.IsChecked = new bool?(SurveyHelper.AutoCapture);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00003B5C File Offset: 0x00001D5C
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0009ABCC File Offset: 0x00098DCC
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			if (this.StopFillPageId.Text != global::GClass0.smethod_0("") && (this.oFunc.LEFT(this.StopFillPageId.Text, 1) != global::GClass0.smethod_0("\"") || this.oFunc.RIGHT(this.StopFillPageId.Text, 1) != global::GClass0.smethod_0("\"")))
			{
				MessageBox.Show(SurveyMsg.MsgFillMode_NotJING, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.StopFillPageId.Focus();
				return;
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			if (this.btn1.Style == style)
			{
				SurveyHelper.FillMode = global::GClass0.smethod_0("0");
			}
			else if (this.btn2.Style == style)
			{
				SurveyHelper.FillMode = global::GClass0.smethod_0("3");
			}
			else
			{
				SurveyHelper.FillMode = global::GClass0.smethod_0("2");
			}
			SurveyHelper.StopFillPage = this.StopFillPageId.Text;
			SurveyHelper.AutoCapture = (this.Capture.IsChecked == true);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("NŮɪͩщլ٦ݤ"), SurveyHelper.FillMode);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("_ſɥ͹юծ٪ݩࡔॢ੥୤"), SurveyHelper.StopFillPage);
			base.Close();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0009AD50 File Offset: 0x00098F50
		private void btn3_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Button button = (Button)sender;
			foreach (Button button2 in this.listBtn)
			{
				button2.Style = style2;
			}
			button.Style = style;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00003C7A File Offset: 0x00001E7A
		private void StopFillPageId_GotFocus(object sender, RoutedEventArgs e)
		{
			if (this.StopFillPageId.Text == global::GClass0.smethod_0(""))
			{
				this.StopFillPageId.Text = global::GClass0.smethod_0(" ĳȢ");
				this.StopFillPageId.SelectAll();
			}
		}

		// Token: 0x04000ADE RID: 2782
		private List<Button> listBtn = new List<Button>();

		// Token: 0x04000ADF RID: 2783
		private UDPX oFunc = new UDPX();

		// Token: 0x04000AE0 RID: 2784
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();
	}
}
