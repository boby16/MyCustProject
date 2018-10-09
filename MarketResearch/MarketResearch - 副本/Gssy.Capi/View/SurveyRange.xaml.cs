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
	public partial class SurveyRange : Window
	{
		public SurveyRange()
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
			this.CITY_Code = this.oSurveyConfigBiz.GetByCodeText("CityCode");
			this.SurveyStart = this.oSurveyConfigBiz.GetByCodeText("SurveyIDBegin");
			this.SurveyEnd = this.oSurveyConfigBiz.GetByCodeText("SurveyIDEnd");
			this.TouchPad = this.oSurveyConfigBiz.GetByCodeText("TouchPad");
			this.PC_Code = this.oSurveyConfigBiz.GetByCodeText("PCCode");
			this.MP3Minutes = this.oSurveyConfigBiz.GetByCodeText("MP3Minutes");
			this.MP3Mode = this.oSurveyConfigBiz.GetByCodeText("MP3Mode");
			this.QDetails = this.oSurveyDetailDal.GetDetails("CITY");
			if (SurveyMsg.AllowClearCaseNumber == "AllowClearCaseNumber_true")
			{
				SurveyDetail surveyDetail = new SurveyDetail();
				surveyDetail.CODE = "xx";
				surveyDetail.CODE_TEXT = SurveyMsg.NoCity;
				this.QDetails.Add(surveyDetail);
			}
			this.cmbCITY.ItemsSource = this.QDetails;
			this.cmbCITY.DisplayMemberPath = "CODE_TEXT";
			this.cmbCITY.SelectedValuePath = "CODE";
			if (this.CITY_Code != "" && this.CITY_Code != null && this.CITY_Code != "xx")
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
				if (SurveyMsg.AllowClearCaseNumber == "AllowClearCaseNumber_true")
				{
					this.cmbCITY.SelectedValue = "xx";
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
			if (SurveyMsg.SetPCNumber == "SetPCNumber_true" && this.SelectCity)
			{
				this.PCcode1st.Text = this.method_5("00000" + this.CITY_Code, SurveyMsg.CITY_Length);
				this.txtPCCode.Text = this.method_5(this.PC_Code, this.nPcCodeLen);
				this.PCcodeMsg.Text = "(" + this.nPcCodeLen.ToString() + SurveyMsg.MsgBit3;
				this.txtPCCode.MaxLength = this.nPcCodeLen;
				this.PCcode1st.Width = this.txtBegin1st.Width;
				this.txtPCCode.Width = this.txtBegin.Width;
			}
			else
			{
				this.PCcode1st.Text = "";
				this.txtPCCode.Text = this.PC_Code;
				this.PCcodeMsg.Text = "(" + SurveyMsg.PcCode_Length.ToString() + SurveyMsg.MsgBit3;
				this.txtPCCode.MaxLength = SurveyMsg.PcCode_Length;
				this.txtPCCode.Width = 180.0;
				this.PCcode1st.Width = 0.0;
			}
			if (SurveyMsg.SurveyRangePsw == SurveyMsg.SurveyRangeDemoPsw || SurveyMsg.VersionID.IndexOf("测试版") >= 0 || SurveyMsg.VersionID.IndexOf("演示版") >= 0 || SurveyMsg.VersionID.IndexOf("Demo") >= 0)
			{
				this.PasswordMsg.Visibility = Visibility.Visible;
			}
			if (this.TouchPad == "1")
			{
				this.ChkTouchPad.IsChecked = new bool?(true);
			}
			else
			{
				this.ChkTouchPad.IsChecked = new bool?(false);
			}
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true")
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

		private void btnMP3Setup_Click(object sender, RoutedEventArgs e)
		{
			this.spMP3Setup.Visibility = Visibility.Collapsed;
			this.txtMP3.Visibility = Visibility.Visible;
			this.spMP3.Visibility = Visibility.Visible;
		}

		private void btnMP3_Click(object sender, RoutedEventArgs e)
		{
			if (this.btnMP3.Content.ToString() == SurveyMsg.MP3Mode1)
			{
				this.btnMP3.Content = SurveyMsg.MP3Mode2;
				return;
			}
			this.btnMP3.Content = SurveyMsg.MP3Mode1;
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			string text = "";
			string text2 = "";
			this.CITY_Code = "";
			string string_ = "";
			string text3 = "";
			if (this.cmbCITY.SelectedValue != null)
			{
				text3 = this.cmbCITY.SelectedValue.ToString();
				if (text3 != "xx")
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
			string string_2 = "0";
			string text4 = this.txtPCCode.Text;
			string text5 = this.txtMP3Len.Text;
			string string_3 = this.btnMP3.Content.ToString();
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
			if (text3 == "")
			{
				MessageBox.Show(SurveyMsg.MsgSelectOne, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				this.cmbCITY.Focus();
				return;
			}
			if (SurveyMsg.SetPCNumber == "SetPCNumber_true" && this.SelectCity)
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
			if (!(text == "") || !(text2 == "") || !(SurveyMsg.AllowClearCaseNumber == "AllowClearCaseNumber_true"))
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
				string_2 = "1";
				SurveyHelper.IsTouch = "IsTouch_true";
			}
			else
			{
				string_2 = "0";
				SurveyHelper.IsTouch = "IsTouch_false";
			}
			text4 = this.txtPCCode.Text;
			this.oSurveyConfigBiz.Save("SurveyIDBegin", text);
			this.oSurveyConfigBiz.Save("SurveyIDEnd", text2);
			this.oSurveyConfigBiz.Save("TouchPad", string_2);
			if (SurveyMsg.SetPCNumber == "SetPCNumber_true" && text3 != "xx")
			{
				text4 = this.method_5("00000" + this.CITY_Code, SurveyMsg.CITY_Length) + this.method_5("00000" + text4, SurveyMsg.PcCode_Length - SurveyMsg.CITY_Length);
			}
			this.oSurveyConfigBiz.Save("PCCode", text4);
			this.oSurveyConfigBiz.Save("CityCode", this.CITY_Code);
			this.oSurveyConfigBiz.Save("CityText", string_);
			this.oSurveyConfigBiz.Save("MP3Minutes", text5);
			this.oSurveyConfigBiz.Save("MP3Mode", string_3);
			if (text.Length > 0)
			{
				this.method_1("");
			}
			MessageBox.Show(SurveyMsg.MsgSetSaveOk, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			this.btnExit_Click(sender, e);
		}

		private void cmbCITY_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.cmbCITY.SelectedValue != null)
			{
				string text = this.cmbCITY.SelectedValue.ToString();
				if (text == "xx")
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
					this.PCcode1st.Text = "";
					this.PCcodeMsg.Text = "(" + SurveyMsg.PcCode_Length.ToString() + SurveyMsg.MsgBit3;
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
				if (SurveyMsg.SetPCNumber == "SetPCNumber_true")
				{
					this.txtPCCode.Text = this.method_5(this.txtPCCode.Text, this.nPcCodeLen);
					this.PCcode1st.Text = this.method_5("00000" + this.CITY_Code, SurveyMsg.CITY_Length);
					this.PCcodeMsg.Text = "(" + this.nPcCodeLen.ToString() + SurveyMsg.MsgBit3;
					this.txtPCCode.MaxLength = this.nPcCodeLen;
					this.PCcode1st.Width = this.txtBegin1st.Width;
					this.txtPCCode.Width = this.txtBegin.Width;
				}
				this.txtBegin.Focus();
			}
		}

		private void txtMP3Len_LostFocus(object sender, RoutedEventArgs e)
		{
			if (this.ChkTouchPad.IsChecked.Value)
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void txtMP3Len_GotFocus(object sender, RoutedEventArgs e)
		{
			if (this.ChkTouchPad.IsChecked.Value)
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void passwordBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSave_Click(sender, e);
			}
		}

		private void txtMP3Len_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				if (textBox.Name == "txtBegin")
				{
					this.txtEnd.Focus();
				}
				else if (textBox.Name == "txtEnd")
				{
					this.txtPCCode.Focus();
				}
				else if (textBox.Name == "txtPCCode")
				{
					this.passwordBox1.Focus();
				}
			}
			TextBox textBox2 = sender as TextBox;
			if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
			{
				if (textBox2.Text.Contains(".") && e.Key == Key.Decimal)
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
				if (textBox2.Text.Contains(".") && e.Key == Key.OemPeriod)
				{
					e.Handled = true;
					return;
				}
				e.Handled = false;
				return;
			}
		}

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

		private void method_1(string string_0)
		{
			this.oSurveyConfigBiz.Save("LastSurveyId", string_0);
		}

		private string method_2(string string_0, int int_0, int int_1 = -9999)
		{
			if (string_0 == null)
			{
				return "";
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

		private string method_3(string string_0, int int_0 = 1)
		{
			if (string_0 == null)
			{
				return "";
			}
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		private string method_4(string string_0, int int_0, int int_1 = -9999)
		{
			if (string_0 == null)
			{
				return "";
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

		private string method_5(string string_0, int int_0 = 1)
		{
			if (string_0 == null)
			{
				return "";
			}
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_6(string string_0)
		{
			if (string_0 == null)
			{
				return 0;
			}
			if (string_0 == "")
			{
				return 0;
			}
			if (string_0 == "0")
			{
				return 0;
			}
			if (string_0 == "-0")
			{
				return 0;
			}
			if (!this.method_7(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		private bool method_7(string string_0)
		{
			return new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$").IsMatch(string_0);
		}

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private string CITY_Code = "";

		private string SurveyStart = "";

		private string SurveyEnd = "";

		private string PC_Code = "";

		private string TouchPad = "";

		private string MP3Minutes = "";

		private string MP3Mode = "";

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private List<SurveyDetail> QDetails = new List<SurveyDetail>();

		private int nFontSize = 32;

		private int nPcCodeLen = 3;

		private bool SelectCity;
	}
}
