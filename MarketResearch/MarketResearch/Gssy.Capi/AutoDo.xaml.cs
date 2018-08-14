using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi
{
	// Token: 0x02000003 RID: 3
	public partial class AutoDo : Window
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002095 File Offset: 0x00000295
		public AutoDo()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00004A1C File Offset: 0x00002C1C
		private void method_0(object sender, RoutedEventArgs e)
		{
			string text = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("SŤɤ͠ъբٓݘࡾ२੺୳ై൰๩ཡၧᅳ"));
			text = ((text == null) ? global::GClass0.smethod_0("") : text.Trim());
			string text2 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("Mžɾͦьը݆ٙ࡫ॶ੬୵"));
			text2 = ((text2 == null) ? global::GClass0.smethod_0("") : text2.Trim());
			this.txtStartNumber.Text = ((text == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("0") : text);
			this.txtCount.Text = ((text2 == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("0") : text2);
			this.txtProjectName.Text = SurveyMsg.MsgProjectName;
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("SŤɤ͠ъբٓݘࡾ२੺୳ై൰๩ཡၧᅳ"), this.txtStartNumber.Text);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("Mžɾͦьը݆ٙ࡫ॶ੬୵"), this.txtCount.Text);
			this.CurPageId = global::GClass0.smethod_0("GŊɖ͘");
			this.oQuestion.Init(this.CurPageId, 0, true);
			this.txtCity.Text = this.oQuestion.QDefine.QUESTION_TITLE + global::GClass0.smethod_0("；");
			SurveyHelper.StopFillPage = global::GClass0.smethod_0("");
			this.method_2();
			SurveyHelper.AutoDo_listCity.Clear();
			this.method_3(this.listButton[0], e);
			this.txtStartNumber.Focus();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020CF File Offset: 0x000002CF
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			new CStart().Show();
			base.Close();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00004BB0 File Offset: 0x00002DB0
		private void btnDo_Click(object sender, RoutedEventArgs e)
		{
			int num = SurveyHelper.AutoDo_listCity.Count * this.oFunc.StringToInt(this.txtCount.Text.Trim());
			if (num == 0)
			{
				MessageBox.Show(SurveyMsg.MsgAutoDo_NoCase, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (MessageBox.Show(string.Format(SurveyMsg.MsgAutoDo_Ask, num.ToString()), SurveyMsg.MsgAutoDo_AskTitle, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
			{
				this.btnDo.IsEnabled = false;
				SurveyHelper.AutoDo_Start = DateTime.Now;
				SurveyHelper.AutoDo = true;
				SurveyHelper.AutoFill = true;
				SurveyHelper.AutoDo_CityOrder = 0;
				SurveyHelper.AutoDo_StartOrder = this.oFunc.StringToInt(this.txtStartNumber.Text.Trim());
				SurveyHelper.AutoDo_Total = this.oFunc.StringToInt(this.txtCount.Text.Trim());
				SurveyHelper.AutoDo_Count = 0;
				string byCodeText = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("VņɇͬѦդ"));
				if (byCodeText == null || byCodeText == global::GClass0.smethod_0(""))
				{
					MessageBox.Show(SurveyMsg.MsgNeedConfig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					return;
				}
				string byCodeText2 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("^ŹɹͼѬձَ݂ࡇॡ੤୫౯"));
				string byCodeText3 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("Xſɻ;Ѣտٌ݀ࡆ६੥"));
				string byCodeText4 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("\\ŨɳͦѬՓ٣ݥ"));
				if (byCodeText2 != global::GClass0.smethod_0(""))
				{
					SurveyHelper.SurveyCity = byCodeText2.Substring(0, byCodeText2.Length - SurveyMsg.SurveyIDEnd.Length);
					SurveyMsg.SurveyIDBegin = byCodeText2;
					SurveyMsg.SurveyIDEnd = byCodeText3;
					SurveyHelper.SurveyStart = SurveyHelper.SurveyCodePage;
					if (byCodeText4 == global::GClass0.smethod_0("0"))
					{
						SurveyHelper.IsTouch = global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤");
					}
					else
					{
						SurveyHelper.IsTouch = global::GClass0.smethod_0("DſɟͥѼիٯݙࡣ॥੯ୱ౤");
					}
				}
				SurveyHelper.SurveyStart = global::GClass0.smethod_0("GŊɖ͘");
				MainWindow mainWindow = new MainWindow();
				mainWindow.ShowDialog();
				mainWindow.Close();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E1 File Offset: 0x000002E1
		private void method_1(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgRightTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00004DB0 File Offset: 0x00002FB0
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wpCity;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(15.0, 0.0, 0.0, 15.0);
				button.Style = style;
				button.Tag = surveyDetail.CODE;
				button.Click += this.method_3;
				button.FontSize = 18.0;
				button.MinWidth = 80.0;
				button.MinHeight = 30.0;
				wrapPanel.Children.Add(button);
				this.listButton.Add(button);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00004EF8 File Offset: 0x000030F8
		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			string item = button.Tag.ToString();
			if (button.Style == style)
			{
				SurveyHelper.AutoDo_listCity.Remove(item);
				button.Style = style2;
			}
			else
			{
				SurveyHelper.AutoDo_listCity.Add(item);
				button.Style = style;
			}
			this.txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00004F98 File Offset: 0x00003198
		private void btnUnSel_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			foreach (Button button in this.listButton)
			{
				button.Style = style;
			}
			SurveyHelper.AutoDo_listCity.Clear();
			this.txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00005030 File Offset: 0x00003230
		private void btnSelAll_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			SurveyHelper.AutoDo_listCity.Clear();
			foreach (Button button in this.listButton)
			{
				button.Style = style;
				string item = button.Tag.ToString();
				SurveyHelper.AutoDo_listCity.Add(item);
			}
			this.txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020F6 File Offset: 0x000002F6
		private void btnFillMode_Click(object sender, RoutedEventArgs e)
		{
			new FillMode().ShowDialog();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002103 File Offset: 0x00000303
		private void txtStartNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.txtCount.Focus();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000211A File Offset: 0x0000031A
		private void txtStartNumber_LostFocus(object sender, RoutedEventArgs e)
		{
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("SŤɤ͠ъբٓݘࡾ२੺୳ై൰๩ཡၧᅳ"), this.txtStartNumber.Text);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000213C File Offset: 0x0000033C
		private void txtCount_LostFocus(object sender, RoutedEventArgs e)
		{
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("Mžɾͦьը݆ٙ࡫ॶ੬୵"), this.txtCount.Text);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000215E File Offset: 0x0000035E
		private void txtCount_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtCount.SelectAll();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000216B File Offset: 0x0000036B
		private void txtStartNumber_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtStartNumber.SelectAll();
		}

		// Token: 0x04000005 RID: 5
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x04000006 RID: 6
		private UDPX oFunc = new UDPX();

		// Token: 0x04000007 RID: 7
		private string CurPageId;

		// Token: 0x04000008 RID: 8
		private QMultiple oQuestion = new QMultiple();

		// Token: 0x04000009 RID: 9
		private List<Button> listButton = new List<Button>();
	}
}
