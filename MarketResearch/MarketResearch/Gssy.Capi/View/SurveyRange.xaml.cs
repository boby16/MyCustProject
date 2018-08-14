using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x02000053 RID: 83
	public partial class SurveyRange : Window
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x00093264 File Offset: 0x00091464
		public SurveyRange()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000020CF File Offset: 0x000002CF
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			new CStart().Show();
			base.Close();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00093320 File Offset: 0x00091520
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.CITY_Code = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("KŮɲͼчլ٦ݤ"));
			this.SurveyStart = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("^ŹɹͼѬձَ݂ࡇॡ੤୫౯"));
			this.SurveyEnd = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("Xſɻ;Ѣտٌ݀ࡆ६੥"));
			this.TouchPad = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("\\ŨɳͦѬՓ٣ݥ"));
			this.PC_Code = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("VņɇͬѦդ"));
			this.MP3Minutes = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("GřȻ͊ѯիٱݷࡧॲ"));
			this.MP3Mode = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("JŖȶ͉Ѭզ٤"));
			this.QDetails = this.oSurveyDetailDal.GetDetails(global::GClass0.smethod_0("GŊɖ͘"));
			if (SurveyMsg.AllowClearCaseNumber == global::GClass0.smethod_0("XŴɻ͹Ѣ՗ٿݷࡰॢੌ୯౾൩ๅཿၤᅪቢ፴ᑚᕰᙱ᝷ᡤ"))
			{
				SurveyDetail surveyDetail = new SurveyDetail();
				surveyDetail.CODE = global::GClass0.smethod_0("zŹ");
				surveyDetail.CODE_TEXT = SurveyMsg.NoCity;
				this.QDetails.Add(surveyDetail);
			}
			this.cmbCITY.ItemsSource = this.QDetails;
			this.cmbCITY.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbCITY.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			if (this.CITY_Code != global::GClass0.smethod_0("") && this.CITY_Code != null && this.CITY_Code != global::GClass0.smethod_0("zŹ"))
			{
				this.SelectCity = true;
				this.cmbCITY.SelectedValue = this.CITY_Code;
				this.OrderStart.Visibility = Visibility.Visible;
				this.OrderEnd.Visibility = Visibility.Visible;
				this.txtBegin.Visibility = Visibility.Visible;
				this.txtEnd.Visibility = Visibility.Visible;
				this.txtBegin1st.Visibility = Visibility.Visible;
				this.txtEnd1st.Visibility = Visibility.Visible;
				this.BeginBit.Visibility = Visibility.Visible;
				this.EndBit.Visibility = Visibility.Visible;
				this.OrderInfo.Visibility = Visibility.Hidden;
			}
			else
			{
				if (SurveyMsg.AllowClearCaseNumber == global::GClass0.smethod_0("XŴɻ͹Ѣ՗ٿݷࡰॢੌ୯౾൩ๅཿၤᅪቢ፴ᑚᕰᙱ᝷ᡤ"))
				{
					this.cmbCITY.SelectedValue = global::GClass0.smethod_0("zŹ");
				}
				this.OrderStart.Visibility = Visibility.Hidden;
				this.OrderEnd.Visibility = Visibility.Hidden;
				this.txtBegin.Visibility = Visibility.Hidden;
				this.txtEnd.Visibility = Visibility.Hidden;
				this.txtBegin1st.Visibility = Visibility.Hidden;
				this.txtEnd1st.Visibility = Visibility.Hidden;
				this.BeginBit.Visibility = Visibility.Hidden;
				this.EndBit.Visibility = Visibility.Hidden;
				this.OrderInfo.Visibility = Visibility.Visible;
			}
			this.OrderInfo.Text = SurveyMsg.MsgOrderInfo;
			this.BeginBit.Text = SurveyMsg.MsgBit1 + SurveyMsg.Order_Length.ToString() + SurveyMsg.MsgBit2;
			this.EndBit.Text = SurveyMsg.MsgBit1 + SurveyMsg.Order_Length.ToString() + SurveyMsg.MsgBit2;
			this.txtBegin.MaxLength = SurveyMsg.Order_Length;
			this.txtEnd.MaxLength = SurveyMsg.Order_Length;
			this.txtBegin1st.Width = (double)(this.nFontSize * SurveyMsg.CITY_Length);
			this.txtBegin.Width = 180.0 - this.txtBegin1st.Width;
			this.txtEnd1st.Width = this.txtBegin1st.Width;
			this.txtEnd.Width = this.txtBegin.Width;
			if (this.SelectCity)
			{
				this.txtBegin1st.Text = this.CITY_Code;
				this.txtEnd1st.Text = this.CITY_Code;
				this.txtBegin.Text = this.method_5(this.SurveyStart, SurveyMsg.Order_Length);
				this.txtEnd.Text = this.method_5(this.SurveyEnd, SurveyMsg.Order_Length);
			}
			this.nPcCodeLen = SurveyMsg.PcCode_Length - SurveyMsg.CITY_Length;
			if (SurveyMsg.SetPCNumber == global::GClass0.smethod_0("CŪɺ͝яՅٿݤࡪॢੴ୚౰൱๷ཤ") && this.SelectCity)
			{
				this.PCcode1st.Text = this.method_5(global::GClass0.smethod_0("5Ĵȳ̲б") + this.CITY_Code, SurveyMsg.CITY_Length);
				this.txtPCCode.Text = this.method_5(this.PC_Code, this.nPcCodeLen);
				this.PCcodeMsg.Text = global::GClass0.smethod_0(")") + this.nPcCodeLen.ToString() + SurveyMsg.MsgBit3;
				this.txtPCCode.MaxLength = this.nPcCodeLen;
				this.PCcode1st.Width = this.txtBegin1st.Width;
				this.txtPCCode.Width = this.txtBegin.Width;
			}
			else
			{
				this.PCcode1st.Text = global::GClass0.smethod_0("");
				this.txtPCCode.Text = this.PC_Code;
				this.PCcodeMsg.Text = global::GClass0.smethod_0(")") + SurveyMsg.PcCode_Length.ToString() + SurveyMsg.MsgBit3;
				this.txtPCCode.MaxLength = SurveyMsg.PcCode_Length;
				this.txtPCCode.Width = 180.0;
				this.PCcode1st.Width = 0.0;
			}
			if (SurveyMsg.SurveyRangePsw == SurveyMsg.SurveyRangeDemoPsw || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("浈諗灉")) >= 0 || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("漗砸灉")) >= 0 || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("@Ŧɯͮ")) >= 0)
			{
				this.PasswordMsg.Visibility = Visibility.Visible;
			}
			if (this.TouchPad == global::GClass0.smethod_0("0"))
			{
				this.ChkTouchPad.IsChecked = new bool?(true);
			}
			else
			{
				this.ChkTouchPad.IsChecked = new bool?(false);
			}
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤"))
			{
				this.spMP3Setup.Visibility = Visibility.Visible;
				if (this.MP3Minutes == null)
				{
					this.MP3Minutes = SurveyHelper.MP3MaxMinutes;
				}
				this.txtMP3Len.Text = this.MP3Minutes;
				if (this.MP3Mode == null)
				{
					this.MP3Mode = SurveyMsg.MP3Mode2;
				}
				this.btnMP3.Content = this.MP3Mode;
			}
			this.txtBegin.Focus();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00003954 File Offset: 0x00001B54
		private void btnMP3Setup_Click(object sender, RoutedEventArgs e)
		{
			this.spMP3Setup.Visibility = Visibility.Collapsed;
			this.txtMP3.Visibility = Visibility.Visible;
			this.spMP3.Visibility = Visibility.Visible;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000397A File Offset: 0x00001B7A
		private void btnMP3_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnMP3.Content.ToString() == SurveyMsg.MP3Mode1)
			{
				this.btnMP3.Content = SurveyMsg.MP3Mode2;
				return;
			}
			this.btnMP3.Content = SurveyMsg.MP3Mode1;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000939A4 File Offset: 0x00091BA4
		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			this.CITY_Code = global::GClass0.smethod_0("");
			string string_ = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			if (this.cmbCITY.SelectedValue != null)
			{
				text3 = this.cmbCITY.SelectedValue.ToString();
				if (text3 != global::GClass0.smethod_0("zŹ"))
				{
					this.SelectCity = true;
					string_ = this.cmbCITY.Text;
					this.CITY_Code = text3;
					text = text3 + this.txtBegin.Text.Trim();
					text2 = text3 + this.txtEnd.Text.Trim();
				}
				else
				{
					this.SelectCity = false;
				}
			}
			string password = this.passwordBox1.Password;
			string string_2 = global::GClass0.smethod_0("1");
			string text4 = this.txtPCCode.Text;
			string text5 = this.txtMP3Len.Text;
			string string_3 = this.btnMP3.Content.ToString();
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
			if (text3 == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.cmbCITY.Focus();
				return;
			}
			if (SurveyMsg.SetPCNumber == global::GClass0.smethod_0("CŪɺ͝яՅٿݤࡪॢੴ୚౰൱๷ཤ") && this.SelectCity)
			{
				if (text4.Length != SurveyMsg.PcCode_Length - SurveyMsg.CITY_Length)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgRangePCCode, (SurveyMsg.PcCode_Length - SurveyMsg.CITY_Length).ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.txtPCCode.Focus();
					return;
				}
			}
			else if (text4.Length != SurveyMsg.PcCode_Length)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgRangePCCode, SurveyMsg.PcCode_Length.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.txtPCCode.Focus();
				return;
			}
			string string_4 = text4.Substring(0, SurveyMsg.CITY_Length);
			this.method_6(string_4);
			if (!(text == global::GClass0.smethod_0("")) || !(text2 == global::GClass0.smethod_0("")) || !(SurveyMsg.AllowClearCaseNumber == global::GClass0.smethod_0("XŴɻ͹Ѣ՗ٿݷࡰॢੌ୯౾൩ๅཿၤᅪቢ፴ᑚᕰᙱ᝷ᡤ")))
			{
				if (text.Length != SurveyMsg.SurveyId_Length)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgRangeLength, SurveyMsg.Order_Length.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.txtBegin.Focus();
					return;
				}
				if (text2.Length != SurveyMsg.SurveyId_Length)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgRangeLength, SurveyMsg.Order_Length.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.txtEnd.Focus();
					return;
				}
				if (this.method_6(text) > this.method_6(text2))
				{
					MessageBox.Show(SurveyMsg.MsgRangeCompare, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					this.txtEnd.Focus();
					return;
				}
			}
			if (this.ChkTouchPad.IsChecked.Value)
			{
				string_2 = global::GClass0.smethod_0("0");
				SurveyHelper.IsTouch = global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤");
			}
			else
			{
				string_2 = global::GClass0.smethod_0("1");
				SurveyHelper.IsTouch = global::GClass0.smethod_0("DſɟͥѼիٯݙࡣ॥੯ୱ౤");
			}
			text4 = this.txtPCCode.Text;
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("^ŹɹͼѬձَ݂ࡇॡ੤୫౯"), text);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("Xſɻ;Ѣտٌ݀ࡆ६੥"), text2);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("\\ŨɳͦѬՓ٣ݥ"), string_2);
			if (SurveyMsg.SetPCNumber == global::GClass0.smethod_0("CŪɺ͝яՅٿݤࡪॢੴ୚౰൱๷ཤ") && text3 != global::GClass0.smethod_0("zŹ"))
			{
				text4 = this.method_5(global::GClass0.smethod_0("5Ĵȳ̲б") + this.CITY_Code, SurveyMsg.CITY_Length) + this.method_5(global::GClass0.smethod_0("5Ĵȳ̲б") + text4, SurveyMsg.PcCode_Length - SurveyMsg.CITY_Length);
			}
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("VņɇͬѦդ"), text4);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("KŮɲͼчլ٦ݤ"), this.CITY_Code);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("KŮɲͼѐզٺݵ"), string_);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("GřȻ͊ѯիٱݷࡧॲ"), text5);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("JŖȶ͉Ѭզ٤"), string_3);
			if (text.Length > 0)
			{
				this.method_1(global::GClass0.smethod_0(""));
			}
			MessageBox.Show(SurveyMsg.MsgSetSaveOk, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			this.btnExit_Click(sender, e);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00093EF8 File Offset: 0x000920F8
		private void cmbCITY_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.cmbCITY.SelectedValue != null)
			{
				string text = this.cmbCITY.SelectedValue.ToString();
				if (text == global::GClass0.smethod_0("zŹ"))
				{
					this.SelectCity = false;
					this.OrderStart.Visibility = Visibility.Hidden;
					this.OrderEnd.Visibility = Visibility.Hidden;
					this.txtBegin.Visibility = Visibility.Hidden;
					this.txtEnd.Visibility = Visibility.Hidden;
					this.txtBegin1st.Visibility = Visibility.Hidden;
					this.txtEnd1st.Visibility = Visibility.Hidden;
					this.BeginBit.Visibility = Visibility.Hidden;
					this.EndBit.Visibility = Visibility.Hidden;
					this.OrderInfo.Visibility = Visibility.Visible;
					this.txtPCCode.Text = this.PCcode1st.Text + this.txtPCCode.Text;
					this.PCcode1st.Text = global::GClass0.smethod_0("");
					this.PCcodeMsg.Text = global::GClass0.smethod_0(")") + SurveyMsg.PcCode_Length.ToString() + SurveyMsg.MsgBit3;
					this.txtPCCode.MaxLength = SurveyMsg.PcCode_Length;
					this.txtPCCode.Width = 180.0;
					this.PCcode1st.Width = 0.0;
					this.txtPCCode.Focus();
					return;
				}
				this.SelectCity = true;
				this.OrderStart.Visibility = Visibility.Visible;
				this.OrderEnd.Visibility = Visibility.Visible;
				this.txtBegin.Visibility = Visibility.Visible;
				this.txtEnd.Visibility = Visibility.Visible;
				this.txtBegin1st.Visibility = Visibility.Visible;
				this.txtEnd1st.Visibility = Visibility.Visible;
				this.BeginBit.Visibility = Visibility.Visible;
				this.EndBit.Visibility = Visibility.Visible;
				this.OrderInfo.Visibility = Visibility.Hidden;
				this.CITY_Code = text;
				this.txtBegin1st.Text = text;
				this.txtEnd1st.Text = text;
				if (SurveyMsg.SetPCNumber == global::GClass0.smethod_0("CŪɺ͝яՅٿݤࡪॢੴ୚౰൱๷ཤ"))
				{
					this.txtPCCode.Text = this.method_5(this.txtPCCode.Text, this.nPcCodeLen);
					this.PCcode1st.Text = this.method_5(global::GClass0.smethod_0("5Ĵȳ̲б") + this.CITY_Code, SurveyMsg.CITY_Length);
					this.PCcodeMsg.Text = global::GClass0.smethod_0(")") + this.nPcCodeLen.ToString() + SurveyMsg.MsgBit3;
					this.txtPCCode.MaxLength = this.nPcCodeLen;
					this.PCcode1st.Width = this.txtBegin1st.Width;
					this.txtPCCode.Width = this.txtBegin.Width;
				}
				this.txtBegin.Focus();
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000941C4 File Offset: 0x000923C4
		private void txtMP3Len_LostFocus(object sender, RoutedEventArgs e)
		{
			if (this.ChkTouchPad.IsChecked.Value)
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000941EC File Offset: 0x000923EC
		private void txtMP3Len_GotFocus(object sender, RoutedEventArgs e)
		{
			if (this.ChkTouchPad.IsChecked.Value)
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000039B9 File Offset: 0x00001BB9
		private void passwordBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSave_Click(sender, e);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00094214 File Offset: 0x00092414
		private void txtMP3Len_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				if (textBox.Name == global::GClass0.smethod_0("|ſɲ͇ѡդ٫ݯ"))
				{
					this.txtEnd.Focus();
				}
				else if (textBox.Name == global::GClass0.smethod_0("rŽɰ͆Ѭե"))
				{
					this.txtPCCode.Focus();
				}
				else if (textBox.Name == global::GClass0.smethod_0("}Űɳ͖цՇ٬ݦࡤ"))
				{
					this.passwordBox1.Focus();
				}
			}
			TextBox textBox2 = sender as TextBox;
			if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
			{
				if (textBox2.Text.Contains(global::GClass0.smethod_0("/")) && e.Key == Key.Decimal)
				{
					e.Handled = true;
					return;
				}
				e.Handled = false;
				return;
			}
			else
			{
				if (e.Key < Key.D0 || e.Key > Key.D9 || e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
				{
					e.Handled = true;
					return;
				}
				if (textBox2.Text.Contains(global::GClass0.smethod_0("/")) && e.Key == Key.OemPeriod)
				{
					e.Handled = true;
					return;
				}
				e.Handled = false;
				return;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00090250 File Offset: 0x0008E450
		private void txtMP3Len_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			TextChange[] array = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				double num = 0.0;
				if (!double.TryParse(textBox.Text, out num))
				{
					textBox.Text = textBox.Text.Remove(offset, array[0].AddedLength);
					textBox.Select(offset, 0);
				}
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000039CC File Offset: 0x00001BCC
		private void method_1(string string_0)
		{
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("@Ūɹͽћղٴݳࡡॺੋ୥"), string_0);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00094348 File Offset: 0x00092548
		private string method_2(string string_0, int int_0, int int_1 = -9999)
		{
			if (string_0 == null)
			{
				return global::GClass0.smethod_0("");
			}
			int num = int_1;
			if (num == -9999)
			{
				num = int_0;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (int_0 < 0) ? 0 : int_0;
			int num3 = (num2 < num) ? num2 : num;
			int num4 = (num2 < num) ? num : num2;
			int num5 = (num2 > string_0.Length) ? string_0.Length : num2;
			num = ((int_1 > string_0.Length) ? (string_0.Length - 1) : int_1);
			return string_0.Substring(num5, num - num5 + 1);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000943C4 File Offset: 0x000925C4
		private string method_3(string string_0, int int_0 = 1)
		{
			if (string_0 == null)
			{
				return global::GClass0.smethod_0("");
			}
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00094404 File Offset: 0x00092604
		private string method_4(string string_0, int int_0, int int_1 = -9999)
		{
			if (string_0 == null)
			{
				return global::GClass0.smethod_0("");
			}
			int num = int_1;
			if (num == -9999)
			{
				num = string_0.Length;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (int_0 > string_0.Length) ? string_0.Length : int_0;
			return string_0.Substring(num2, (num2 + num > string_0.Length) ? (string_0.Length - num2) : num);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00094468 File Offset: 0x00092668
		private string method_5(string string_0, int int_0 = 1)
		{
			if (string_0 == null)
			{
				return global::GClass0.smethod_0("");
			}
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000944A8 File Offset: 0x000926A8
		private int method_6(string string_0)
		{
			if (string_0 == null)
			{
				return 0;
			}
			if (string_0 == global::GClass0.smethod_0(""))
			{
				return 0;
			}
			if (string_0 == global::GClass0.smethod_0("1"))
			{
				return 0;
			}
			if (string_0 == global::GClass0.smethod_0("/ı"))
			{
				return 0;
			}
			if (!this.method_7(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_7(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x040009F0 RID: 2544
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x040009F1 RID: 2545
		private string CITY_Code = global::GClass0.smethod_0("");

		// Token: 0x040009F2 RID: 2546
		private string SurveyStart = global::GClass0.smethod_0("");

		// Token: 0x040009F3 RID: 2547
		private string SurveyEnd = global::GClass0.smethod_0("");

		// Token: 0x040009F4 RID: 2548
		private string PC_Code = global::GClass0.smethod_0("");

		// Token: 0x040009F5 RID: 2549
		private string TouchPad = global::GClass0.smethod_0("");

		// Token: 0x040009F6 RID: 2550
		private string MP3Minutes = global::GClass0.smethod_0("");

		// Token: 0x040009F7 RID: 2551
		private string MP3Mode = global::GClass0.smethod_0("");

		// Token: 0x040009F8 RID: 2552
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x040009F9 RID: 2553
		private List<SurveyDetail> QDetails = new List<SurveyDetail>();

		// Token: 0x040009FA RID: 2554
		private int nFontSize = 32;

		// Token: 0x040009FB RID: 2555
		private int nPcCodeLen = 3;

		// Token: 0x040009FC RID: 2556
		private bool SelectCity;
	}
}
