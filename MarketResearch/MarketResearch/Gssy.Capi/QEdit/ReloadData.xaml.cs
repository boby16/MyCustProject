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

namespace Gssy.Capi.QEdit
{
	// Token: 0x0200005C RID: 92
	public partial class ReloadData : Window
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public ReloadData()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0009AF80 File Offset: 0x00099180
		private void method_0(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			base.Hide();
			base.Show();
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("XŬɤͨѧաـݢࡶॠ"));
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
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00003B5C File Offset: 0x00001D5C
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0009B0CC File Offset: 0x000992CC
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			SurveyHelper.StopFillPage = this.StopFillPageId.Text;
			base.Close();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0009B11C File Offset: 0x0009931C
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

		// Token: 0x04000AEC RID: 2796
		private List<Button> listBtn = new List<Button>();

		// Token: 0x04000AED RID: 2797
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();
	}
}
