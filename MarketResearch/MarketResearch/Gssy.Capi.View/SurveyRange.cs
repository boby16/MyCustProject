using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace Gssy.Capi.View
{
	public class SurveyRange : Window, IComponentConnector
	{
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private string CITY_Code = _003F487_003F._003F488_003F("");

		private string SurveyStart = _003F487_003F._003F488_003F("");

		private string SurveyEnd = _003F487_003F._003F488_003F("");

		private string PC_Code = _003F487_003F._003F488_003F("");

		private string TouchPad = _003F487_003F._003F488_003F("");

		private string MP3Minutes = _003F487_003F._003F488_003F("");

		private string MP3Mode = _003F487_003F._003F488_003F("");

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private List<SurveyDetail> QDetails = new List<SurveyDetail>();

		private int nFontSize = 32;

		private int nPcCodeLen = 3;

		private bool SelectCity;

		internal TextBlock OrderInfo;

		internal TextBlock City;

		internal TextBlock OrderStart;

		internal TextBlock OrderEnd;

		internal TextBlock PCcode;

		internal TextBlock txtMP3;

		internal ComboBox cmbCITY;

		internal TextBlock txtBegin1st;

		internal TextBox txtBegin;

		internal TextBlock BeginBit;

		internal TextBlock txtEnd1st;

		internal TextBox txtEnd;

		internal TextBlock EndBit;

		internal TextBlock PCcode1st;

		internal TextBox txtPCCode;

		internal TextBlock PCcodeMsg;

		internal PasswordBox passwordBox1;

		internal TextBlock PasswordMsg;

		internal CheckBox ChkTouchPad;

		internal StackPanel spMP3Setup;

		internal Button btnMP3Setup;

		internal StackPanel spMP3;

		internal TextBox txtMP3Len;

		internal Button btnMP3;

		internal Button btnExit;

		internal Button btnSave;

		private bool _contentLoaded;

		public SurveyRange()
		{
			InitializeComponent();
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new CStart().Show();
			Close();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_05fd: Incompatible stack heights: 0 vs 1
			//IL_060d: Incompatible stack heights: 0 vs 1
			//IL_0627: Incompatible stack heights: 0 vs 2
			//IL_063f: Incompatible stack heights: 0 vs 2
			//IL_0654: Incompatible stack heights: 0 vs 1
			//IL_0665: Incompatible stack heights: 0 vs 2
			//IL_0670: Incompatible stack heights: 0 vs 1
			//IL_0686: Incompatible stack heights: 0 vs 3
			//IL_06a9: Incompatible stack heights: 0 vs 1
			//IL_06ae: Invalid comparison between Unknown and I4
			//IL_06bd: Incompatible stack heights: 0 vs 2
			//IL_06d6: Incompatible stack heights: 0 vs 2
			//IL_0711: Incompatible stack heights: 0 vs 2
			//IL_0731: Incompatible stack heights: 0 vs 1
			CITY_Code = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("KŮɲͼчլ٦ݤ"));
			SurveyStart = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("^ŹɹͼѬձ\u064e\u0742ࡇॡ\u0a64୫౯"));
			SurveyEnd = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Xſɻ;Ѣտ\u064c\u0740ࡆ६\u0a65"));
			TouchPad = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("\\Ũɳ\u0366ѬՓ٣ݥ"));
			PC_Code = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Vņɇ\u036cѦդ"));
			MP3Minutes = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("GřȻ\u034aѯիٱݷ\u0867ॲ"));
			MP3Mode = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("JŖȶ\u0349Ѭզ٤"));
			QDetails = oSurveyDetailDal.GetDetails(_003F487_003F._003F488_003F("GŊɖ\u0358"));
			if (SurveyMsg.AllowClearCaseNumber == _003F487_003F._003F488_003F("XŴɻ\u0379Ѣ\u0557ٿݷ\u0870\u0962\u0a4c୯౾൩ๅ\u0f7f\u1064ᅪቢ፴ᑚᕰᙱ\u1777ᡤ"))
			{
				SurveyDetail surveyDetail = new SurveyDetail();
				string cODE = _003F487_003F._003F488_003F("zŹ");
				((SurveyDetail)/*Error near IL_0100: Stack underflow*/).CODE = cODE;
				surveyDetail.CODE_TEXT = SurveyMsg.NoCity;
				QDetails.Add(surveyDetail);
			}
			cmbCITY.ItemsSource = QDetails;
			cmbCITY.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbCITY.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			if (CITY_Code != _003F487_003F._003F488_003F(""))
			{
				string cITY_Code3 = CITY_Code;
				if ((int)/*Error near IL_0612: Stack underflow*/ != 0)
				{
					string cITY_Code4 = CITY_Code;
					_003F487_003F._003F488_003F("zŹ");
					if ((string)/*Error near IL_0176: Stack underflow*/ != (string)/*Error near IL_0176: Stack underflow*/)
					{
						SelectCity = true;
						ComboBox cmbCITY2 = cmbCITY;
						string cITY_Code = ((SurveyRange)/*Error near IL_0180: Stack underflow*/).CITY_Code;
						((Selector)/*Error near IL_0185: Stack underflow*/).SelectedValue = cITY_Code;
						OrderStart.Visibility = Visibility.Visible;
						OrderEnd.Visibility = Visibility.Visible;
						txtBegin.Visibility = Visibility.Visible;
						txtEnd.Visibility = Visibility.Visible;
						txtBegin1st.Visibility = Visibility.Visible;
						txtEnd1st.Visibility = Visibility.Visible;
						BeginBit.Visibility = Visibility.Visible;
						EndBit.Visibility = Visibility.Visible;
						OrderInfo.Visibility = Visibility.Hidden;
						goto IL_028a;
					}
				}
			}
			if (SurveyMsg.AllowClearCaseNumber == _003F487_003F._003F488_003F("XŴɻ\u0379Ѣ\u0557ٿݷ\u0870\u0962\u0a4c୯౾൩ๅ\u0f7f\u1064ᅪቢ፴ᑚᕰᙱ\u1777ᡤ"))
			{
				ComboBox cmbCITY3 = cmbCITY;
				string selectedValue = _003F487_003F._003F488_003F("zŹ");
				((Selector)/*Error near IL_021e: Stack underflow*/).SelectedValue = selectedValue;
			}
			OrderStart.Visibility = Visibility.Hidden;
			OrderEnd.Visibility = Visibility.Hidden;
			txtBegin.Visibility = Visibility.Hidden;
			txtEnd.Visibility = Visibility.Hidden;
			txtBegin1st.Visibility = Visibility.Hidden;
			txtEnd1st.Visibility = Visibility.Hidden;
			BeginBit.Visibility = Visibility.Hidden;
			EndBit.Visibility = Visibility.Hidden;
			OrderInfo.Visibility = Visibility.Visible;
			goto IL_028a;
			IL_0568:
			if (!(TouchPad == _003F487_003F._003F488_003F("0")))
			{
				ChkTouchPad.IsChecked = false;
			}
			else
			{
				ChkTouchPad.IsChecked = true;
			}
			if (SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64"))
			{
				StackPanel spMP3Setup2 = spMP3Setup;
				((UIElement)/*Error near IL_05b6: Stack underflow*/).Visibility = (Visibility)/*Error near IL_05b6: Stack underflow*/;
				if (MP3Minutes == null)
				{
					MP3Minutes = SurveyHelper.MP3MaxMinutes;
				}
				txtMP3Len.Text = MP3Minutes;
				if (MP3Mode == null)
				{
					string mP3Mode = SurveyMsg.MP3Mode2;
					((SurveyRange)/*Error near IL_05e7: Stack underflow*/).MP3Mode = mP3Mode;
				}
				btnMP3.Content = MP3Mode;
			}
			txtBegin.Focus();
			return;
			IL_028a:
			OrderInfo.Text = SurveyMsg.MsgOrderInfo;
			BeginBit.Text = SurveyMsg.MsgBit1 + SurveyMsg.Order_Length.ToString() + SurveyMsg.MsgBit2;
			EndBit.Text = SurveyMsg.MsgBit1 + SurveyMsg.Order_Length.ToString() + SurveyMsg.MsgBit2;
			txtBegin.MaxLength = SurveyMsg.Order_Length;
			txtEnd.MaxLength = SurveyMsg.Order_Length;
			txtBegin1st.Width = (double)(nFontSize * SurveyMsg.CITY_Length);
			txtBegin.Width = 180.0 - txtBegin1st.Width;
			txtEnd1st.Width = txtBegin1st.Width;
			txtEnd.Width = txtBegin.Width;
			if (SelectCity)
			{
				TextBlock txtBegin1st2 = txtBegin1st;
				string cITY_Code2 = ((SurveyRange)/*Error near IL_0376: Stack underflow*/).CITY_Code;
				((TextBlock)/*Error near IL_037b: Stack underflow*/).Text = cITY_Code2;
				txtEnd1st.Text = CITY_Code;
				txtBegin.Text = _003F95_003F(SurveyStart, SurveyMsg.Order_Length);
				txtEnd.Text = _003F95_003F(SurveyEnd, SurveyMsg.Order_Length);
			}
			nPcCodeLen = SurveyMsg.PcCode_Length - SurveyMsg.CITY_Length;
			if (SurveyMsg.SetPCNumber == _003F487_003F._003F488_003F("CŪɺ\u035dяՅٿݤ\u086a\u0962ੴ\u0b5a\u0c70൱\u0e77ཤ") && ((SurveyRange)/*Error near IL_03f3: Stack underflow*/).SelectCity)
			{
				TextBlock pCcode1st = PCcode1st;
				string _003F362_003F = _003F487_003F._003F488_003F((string)/*Error near IL_03fd: Stack underflow*/) + CITY_Code;
				int cITY_Length = SurveyMsg.CITY_Length;
				string text = ((SurveyRange)/*Error near IL_0412: Stack underflow*/)._003F95_003F(_003F362_003F, cITY_Length);
				((TextBlock)/*Error near IL_0417: Stack underflow*/).Text = text;
				txtPCCode.Text = _003F95_003F(PC_Code, nPcCodeLen);
				PCcodeMsg.Text = _003F487_003F._003F488_003F(")") + nPcCodeLen.ToString() + SurveyMsg.MsgBit3;
				txtPCCode.MaxLength = nPcCodeLen;
				PCcode1st.Width = txtBegin1st.Width;
				txtPCCode.Width = txtBegin.Width;
			}
			else
			{
				PCcode1st.Text = _003F487_003F._003F488_003F("");
				txtPCCode.Text = PC_Code;
				PCcodeMsg.Text = _003F487_003F._003F488_003F(")") + SurveyMsg.PcCode_Length.ToString() + SurveyMsg.MsgBit3;
				txtPCCode.MaxLength = SurveyMsg.PcCode_Length;
				txtPCCode.Width = 180.0;
				PCcode1st.Width = 0.0;
			}
			if (!(SurveyMsg.SurveyRangePsw == SurveyMsg.SurveyRangeDemoPsw))
			{
				SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("浈諗灉"));
				if ((int)/*Error near IL_06ae: Stack underflow*/ < 0)
				{
					string versionID = SurveyMsg.VersionID;
					string value = _003F487_003F._003F488_003F((string)/*Error near IL_0546: Stack underflow*/);
					if (((string)/*Error near IL_054b: Stack underflow*/).IndexOf(value) < 0)
					{
						string versionID2 = SurveyMsg.VersionID;
						_003F487_003F._003F488_003F("@Ŧɯ\u036e");
						if (((string)/*Error near IL_0556: Stack underflow*/).IndexOf((string)/*Error near IL_0556: Stack underflow*/) < 0)
						{
							goto IL_0568;
						}
					}
				}
			}
			PasswordMsg.Visibility = Visibility.Visible;
			goto IL_0568;
		}

		private void _003F214_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			spMP3Setup.Visibility = Visibility.Collapsed;
			txtMP3.Visibility = Visibility.Visible;
			spMP3.Visibility = Visibility.Visible;
		}

		private void _003F215_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_004a: Incompatible stack heights: 0 vs 2
			if (btnMP3.Content.ToString() == SurveyMsg.MP3Mode1)
			{
				Button btnMP4 = btnMP3;
				string mP3Mode = SurveyMsg.MP3Mode2;
				((ContentControl)/*Error near IL_0024: Stack underflow*/).Content = (object)/*Error near IL_0024: Stack underflow*/;
			}
			else
			{
				btnMP3.Content = SurveyMsg.MP3Mode1;
			}
		}

		private void _003F211_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dc: Expected I4, but got Unknown
			//IL_0476: Incompatible stack heights: 0 vs 1
			//IL_04a5: Incompatible stack heights: 0 vs 2
			//IL_04c4: Incompatible stack heights: 0 vs 2
			//IL_04e2: Incompatible stack heights: 0 vs 2
			//IL_04f8: Incompatible stack heights: 0 vs 1
			//IL_0516: Incompatible stack heights: 0 vs 2
			//IL_0526: Incompatible stack heights: 0 vs 1
			//IL_0541: Incompatible stack heights: 0 vs 3
			//IL_0555: Incompatible stack heights: 0 vs 2
			//IL_056e: Incompatible stack heights: 0 vs 2
			//IL_057e: Incompatible stack heights: 0 vs 2
			//IL_059c: Incompatible stack heights: 0 vs 1
			//IL_05c9: Incompatible stack heights: 0 vs 2
			//IL_05d8: Incompatible stack heights: 0 vs 1
			//IL_05f4: Incompatible stack heights: 0 vs 1
			//IL_0638: Incompatible stack heights: 0 vs 1
			//IL_064e: Incompatible stack heights: 0 vs 3
			string text = _003F487_003F._003F488_003F("");
			string text2 = _003F487_003F._003F488_003F("");
			CITY_Code = _003F487_003F._003F488_003F("");
			string _003F523_003F = _003F487_003F._003F488_003F("");
			string text3 = _003F487_003F._003F488_003F("");
			if (cmbCITY.SelectedValue != null)
			{
				object selectedValue = cmbCITY.SelectedValue;
				text3 = ((object)/*Error near IL_0051: Stack underflow*/).ToString();
				if (text3 != _003F487_003F._003F488_003F("zŹ"))
				{
					SelectCity = true;
					_003F523_003F = cmbCITY.Text;
					CITY_Code = text3;
					text = text3 + txtBegin.Text.Trim();
					text2 = text3 + txtEnd.Text.Trim();
				}
				else
				{
					SelectCity = false;
				}
			}
			string password = passwordBox1.Password;
			string text4 = _003F487_003F._003F488_003F("1");
			string text5 = txtPCCode.Text;
			string text6 = txtMP3Len.Text;
			string _003F523_003F2 = btnMP3.Content.ToString();
			if (SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("漗砸灉")) < 0)
			{
				string versionID = SurveyMsg.VersionID;
				_003F487_003F._003F488_003F("浈諗灉");
				if (((string)/*Error near IL_0118: Stack underflow*/).IndexOf((string)/*Error near IL_0118: Stack underflow*/) < 0)
				{
					SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("@Ŧɯ\u036e"));
					if (/*Error near IL_04c9: Stack underflow*/ < /*Error near IL_04c9: Stack underflow*/)
					{
						if (!(password == _003F487_003F._003F488_003F("")))
						{
							bool flag = password != SurveyMsg.SurveyRangePsw;
							if ((int)/*Error near IL_04fd: Stack underflow*/ == 0)
							{
								goto IL_0185;
							}
						}
						MessageBox.Show(SurveyMsg.MsgRangeMissPsw, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						passwordBox1.Focus();
						return;
					}
				}
			}
			if (password != SurveyMsg.SurveyRangeDemoPsw)
			{
				string msgRangeMissPsw = SurveyMsg.MsgRangeMissPsw;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_013c: Stack underflow*/, (string)/*Error near IL_013c: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				passwordBox1.Focus();
				return;
			}
			goto IL_0185;
			IL_0185:
			if (text3 == _003F487_003F._003F488_003F(""))
			{
				string msgSelectOne = SurveyMsg.MsgSelectOne;
				string msgCaption2 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_01a2: Stack underflow*/, (string)/*Error near IL_01a2: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				cmbCITY.Focus();
				return;
			}
			if (SurveyMsg.SetPCNumber == _003F487_003F._003F488_003F("CŪɺ\u035dяՅٿݤ\u086a\u0962ੴ\u0b5a\u0c70൱\u0e77ཤ"))
			{
				bool selectCity = SelectCity;
				if ((int)/*Error near IL_052b: Stack underflow*/ != 0)
				{
					int length = text5.Length;
					int pcCode_Length = SurveyMsg.PcCode_Length;
					int cITY_Length3 = SurveyMsg.CITY_Length;
					_003F val = /*Error near IL_01cf: Stack underflow*/- /*Error near IL_01cf: Stack underflow*/;
					if (/*Error near IL_0546: Stack underflow*/ != val)
					{
						string msgRangePCCode = SurveyMsg.MsgRangePCCode;
						int pcCode_Length2 = SurveyMsg.PcCode_Length;
						int cITY_Length = SurveyMsg.CITY_Length;
						MessageBox.Show(string.Format(arg0: (/*Error near IL_01da: Stack underflow*/ - cITY_Length).ToString(), format: (string)/*Error near IL_01e8: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						txtPCCode.Focus();
						return;
					}
					goto IL_0234;
				}
			}
			if (text5.Length != SurveyMsg.PcCode_Length)
			{
				string msgRangePCCode2 = SurveyMsg.MsgRangePCCode;
				SurveyMsg.PcCode_Length.ToString();
				MessageBox.Show(string.Format((string)/*Error near IL_0219: Stack underflow*/, (object)/*Error near IL_0219: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				txtPCCode.Focus();
				return;
			}
			goto IL_0234;
			IL_0234:
			string _003F362_003F = text5.Substring(0, SurveyMsg.CITY_Length);
			_003F96_003F(_003F362_003F);
			if (text == _003F487_003F._003F488_003F(""))
			{
				string b = _003F487_003F._003F488_003F((string)/*Error near IL_0266: Stack underflow*/);
				if ((string)/*Error near IL_026b: Stack underflow*/ == b)
				{
					bool flag2 = SurveyMsg.AllowClearCaseNumber == _003F487_003F._003F488_003F("XŴɻ\u0379Ѣ\u0557ٿݷ\u0870\u0962\u0a4c୯౾൩ๅ\u0f7f\u1064ᅪቢ፴ᑚᕰᙱ\u1777ᡤ");
					if ((int)/*Error near IL_05a1: Stack underflow*/ != 0)
					{
						goto IL_02f6;
					}
				}
			}
			if (text.Length != SurveyMsg.SurveyId_Length)
			{
				string.Format(SurveyMsg.MsgRangeLength, SurveyMsg.Order_Length.ToString());
				string msgCaption3 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_028d: Stack underflow*/, (string)/*Error near IL_028d: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				txtBegin.Focus();
				return;
			}
			if (text2.Length != SurveyMsg.SurveyId_Length)
			{
				string msgRangeLength = SurveyMsg.MsgRangeLength;
				MessageBox.Show(string.Format(arg0: SurveyMsg.Order_Length.ToString(), format: (string)/*Error near IL_02ba: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				txtEnd.Focus();
				return;
			}
			if (_003F96_003F(text) > _003F96_003F(text2))
			{
				MessageBox.Show(SurveyMsg.MsgRangeCompare, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				txtEnd.Focus();
				return;
			}
			goto IL_02f6;
			IL_02f6:
			if (!ChkTouchPad.IsChecked.Value)
			{
				text4 = _003F487_003F._003F488_003F("1");
				SurveyHelper.IsTouch = _003F487_003F._003F488_003F("Dſɟ\u0365Ѽիٯݙ\u0863॥੯ୱ\u0c64");
			}
			else
			{
				text4 = _003F487_003F._003F488_003F("0");
				SurveyHelper.IsTouch = _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64");
			}
			text5 = txtPCCode.Text;
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("^ŹɹͼѬձ\u064e\u0742ࡇॡ\u0a64୫౯"), text);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("Xſɻ;Ѣտ\u064c\u0740ࡆ६\u0a65"), text2);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("\\Ũɳ\u0366ѬՓ٣ݥ"), text4);
			if (SurveyMsg.SetPCNumber == _003F487_003F._003F488_003F("CŪɺ\u035dяՅٿݤ\u086a\u0962ੴ\u0b5a\u0c70൱\u0e77ཤ"))
			{
				bool flag3 = text3 != _003F487_003F._003F488_003F("zŹ");
				if ((int)/*Error near IL_063d: Stack underflow*/ != 0)
				{
					_003F487_003F._003F488_003F("5Ĵȳ\u0332б");
					string _003F362_003F2 = string.Concat(str1: ((SurveyRange)/*Error near IL_03a2: Stack underflow*/).CITY_Code, str0: (string)/*Error near IL_03a7: Stack underflow*/);
					int cITY_Length2 = SurveyMsg.CITY_Length;
					text5 = ((SurveyRange)/*Error near IL_03b1: Stack underflow*/)._003F95_003F(_003F362_003F2, cITY_Length2) + _003F95_003F(_003F487_003F._003F488_003F("5Ĵȳ\u0332б") + text5, SurveyMsg.PcCode_Length - SurveyMsg.CITY_Length);
				}
			}
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("Vņɇ\u036cѦդ"), text5);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("KŮɲͼчլ٦ݤ"), CITY_Code);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("KŮɲͼѐզٺݵ"), _003F523_003F);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("GřȻ\u034aѯիٱݷ\u0867ॲ"), text6);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("JŖȶ\u0349Ѭզ٤"), _003F523_003F2);
			if (text.Length > 0)
			{
				_003F196_003F(_003F487_003F._003F488_003F(""));
			}
			MessageBox.Show(SurveyMsg.MsgSetSaveOk, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			_003F25_003F(_003F347_003F, _003F348_003F);
		}

		private void _003F216_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F)
		{
			//IL_0211: Incompatible stack heights: 0 vs 1
			//IL_0233: Incompatible stack heights: 0 vs 2
			if (cmbCITY.SelectedValue != null)
			{
				object selectedValue = cmbCITY.SelectedValue;
				string text = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (text == _003F487_003F._003F488_003F("zŹ"))
				{
					SelectCity = false;
					OrderStart.Visibility = Visibility.Hidden;
					OrderEnd.Visibility = Visibility.Hidden;
					txtBegin.Visibility = Visibility.Hidden;
					txtEnd.Visibility = Visibility.Hidden;
					txtBegin1st.Visibility = Visibility.Hidden;
					txtEnd1st.Visibility = Visibility.Hidden;
					BeginBit.Visibility = Visibility.Hidden;
					EndBit.Visibility = Visibility.Hidden;
					OrderInfo.Visibility = Visibility.Visible;
					txtPCCode.Text = PCcode1st.Text + txtPCCode.Text;
					PCcode1st.Text = _003F487_003F._003F488_003F("");
					PCcodeMsg.Text = _003F487_003F._003F488_003F(")") + SurveyMsg.PcCode_Length.ToString() + SurveyMsg.MsgBit3;
					txtPCCode.MaxLength = SurveyMsg.PcCode_Length;
					txtPCCode.Width = 180.0;
					PCcode1st.Width = 0.0;
					txtPCCode.Focus();
				}
				else
				{
					SelectCity = true;
					OrderStart.Visibility = Visibility.Visible;
					OrderEnd.Visibility = Visibility.Visible;
					txtBegin.Visibility = Visibility.Visible;
					txtEnd.Visibility = Visibility.Visible;
					txtBegin1st.Visibility = Visibility.Visible;
					txtEnd1st.Visibility = Visibility.Visible;
					BeginBit.Visibility = Visibility.Visible;
					EndBit.Visibility = Visibility.Visible;
					OrderInfo.Visibility = Visibility.Hidden;
					CITY_Code = text;
					txtBegin1st.Text = text;
					txtEnd1st.Text = text;
					if (SurveyMsg.SetPCNumber == _003F487_003F._003F488_003F("CŪɺ\u035dяՅٿݤ\u086a\u0962ੴ\u0b5a\u0c70൱\u0e77ཤ"))
					{
						TextBox txtPCCode2 = txtPCCode;
						string text2 = txtPCCode.Text;
						int _003F365_003F = nPcCodeLen;
						string text3 = ((SurveyRange)/*Error near IL_023d: Stack underflow*/)._003F95_003F(text2, _003F365_003F);
						((TextBox)/*Error near IL_0242: Stack underflow*/).Text = text3;
						PCcode1st.Text = _003F95_003F(_003F487_003F._003F488_003F("5Ĵȳ\u0332б") + CITY_Code, SurveyMsg.CITY_Length);
						PCcodeMsg.Text = _003F487_003F._003F488_003F(")") + nPcCodeLen.ToString() + SurveyMsg.MsgBit3;
						txtPCCode.MaxLength = nPcCodeLen;
						PCcode1st.Width = txtBegin1st.Width;
						txtPCCode.Width = txtBegin.Width;
					}
					txtBegin.Focus();
				}
			}
		}

		private void _003F90_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (ChkTouchPad.IsChecked.Value)
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void _003F91_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (ChkTouchPad.IsChecked.Value)
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void _003F217_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_001f: Incompatible stack heights: 0 vs 3
			if (_003F348_003F.Key == Key.Return)
			{
				((SurveyRange)/*Error near IL_0011: Stack underflow*/)._003F211_003F((object)/*Error near IL_0011: Stack underflow*/, (RoutedEventArgs)/*Error near IL_0011: Stack underflow*/);
			}
		}

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_0107: Incompatible stack heights: 0 vs 1
			//IL_0121: Incompatible stack heights: 0 vs 1
			//IL_014e: Incompatible stack heights: 0 vs 2
			//IL_015e: Incompatible stack heights: 0 vs 1
			//IL_0170: Incompatible stack heights: 0 vs 2
			//IL_0194: Incompatible stack heights: 0 vs 2
			//IL_01a9: Incompatible stack heights: 0 vs 1
			//IL_01ae: Invalid comparison between Unknown and I4
			//IL_01be: Incompatible stack heights: 0 vs 2
			//IL_01ce: Incompatible stack heights: 0 vs 1
			//IL_01d3: Invalid comparison between Unknown and I4
			if (_003F348_003F.Key == Key.Return)
			{
				TextBox textBox = (TextBox)_003F347_003F;
				if (!(textBox.Name == _003F487_003F._003F488_003F("|ſɲ\u0347ѡդ٫ݯ")))
				{
					if (!(textBox.Name == _003F487_003F._003F488_003F("rŽɰ\u0346Ѭե")))
					{
						if (textBox.Name == _003F487_003F._003F488_003F("}Űɳ\u0356цՇ٬ݦ\u0864"))
						{
							passwordBox1.Focus();
						}
					}
					else
					{
						txtPCCode.Focus();
					}
				}
				else
				{
					txtEnd.Focus();
				}
			}
			TextBox textBox2 = _003F347_003F as TextBox;
			if (_003F348_003F.Key >= Key.NumPad0)
			{
				Key key = _003F348_003F.Key;
				if (/*Error near IL_0153: Stack underflow*/ <= /*Error near IL_0153: Stack underflow*/)
				{
					string text = textBox2.Text;
					string value = _003F487_003F._003F488_003F("/");
					if (((string)/*Error near IL_008e: Stack underflow*/).Contains(value))
					{
						Key key2 = _003F348_003F.Key;
						if (/*Error near IL_0175: Stack underflow*/ == /*Error near IL_0175: Stack underflow*/)
						{
							_003F348_003F.Handled = true;
							return;
						}
					}
					goto IL_0098;
				}
			}
			if (_003F348_003F.Key >= Key.D0)
			{
				Key key3 = _003F348_003F.Key;
				if (/*Error near IL_0199: Stack underflow*/ <= /*Error near IL_0199: Stack underflow*/)
				{
					ModifierKeys modifier = _003F348_003F.KeyboardDevice.Modifiers;
					if ((int)/*Error near IL_01ae: Stack underflow*/ != 4)
					{
						string text2 = textBox2.Text;
						string value2 = _003F487_003F._003F488_003F((string)/*Error near IL_00bd: Stack underflow*/);
						if (((string)/*Error near IL_00c2: Stack underflow*/).Contains(value2))
						{
							Key key4 = _003F348_003F.Key;
							if ((int)/*Error near IL_01d3: Stack underflow*/ == 144)
							{
								_003F348_003F.Handled = true;
								return;
							}
						}
						goto IL_00d1;
					}
				}
			}
			_003F348_003F.Handled = true;
			return;
			IL_017d:
			goto IL_0098;
			IL_00d1:
			_003F348_003F.Handled = false;
			return;
			IL_0098:
			_003F348_003F.Handled = false;
			return;
			IL_01db:
			goto IL_00d1;
		}

		private unsafe void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_0067: Incompatible stack heights: 0 vs 2
			//IL_0079: Incompatible stack heights: 0 vs 3
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				double num = 0.0;
				string text2 = textBox.Text;
				if (!double.TryParse((string)/*Error near IL_0041: Stack underflow*/, out *(double*)/*Error near IL_0041: Stack underflow*/))
				{
					string text3 = textBox.Text;
					int addedLength = array[0].AddedLength;
					string text = ((string)/*Error near IL_0086: Stack underflow*/).Remove((int)/*Error near IL_0086: Stack underflow*/, addedLength);
					((TextBox)/*Error near IL_008b: Stack underflow*/).Text = text;
					textBox.Select(offset, 0);
				}
			}
		}

		private void _003F196_003F(string _003F397_003F)
		{
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("@Ūɹͽћղٴݳ\u0861ॺ\u0a4b\u0b65"), _003F397_003F);
		}

		private string _003F92_003F(string _003F362_003F, int _003F363_003F, int _003F364_003F = -9999)
		{
			//IL_00aa: Incompatible stack heights: 0 vs 1
			//IL_00af: Incompatible stack heights: 1 vs 0
			//IL_00ba: Incompatible stack heights: 0 vs 1
			//IL_00bf: Incompatible stack heights: 1 vs 0
			//IL_00ca: Incompatible stack heights: 0 vs 1
			//IL_00cf: Incompatible stack heights: 1 vs 0
			//IL_00da: Incompatible stack heights: 0 vs 1
			//IL_00df: Incompatible stack heights: 1 vs 0
			//IL_00ea: Incompatible stack heights: 0 vs 1
			//IL_00f7: Incompatible stack heights: 0 vs 1
			if (_003F362_003F == null)
			{
				return _003F487_003F._003F488_003F("");
			}
			goto IL_0006;
			IL_00f7:
			int num = (int)/*Error near IL_00f8: Stack underflow*/;
			int num2;
			return _003F362_003F.Substring(num2, num - num2 + 1);
			IL_0006:
			num = _003F364_003F;
			if (num == -9999)
			{
				num = _003F363_003F;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (_003F363_003F < 0)
			{
			}
			int num3 = 0;
			int num4;
			if (num3 < num)
			{
				num4 = num3;
			}
			num2 = num4;
			int num5;
			if (num3 < num)
			{
				num5 = num;
			}
			num = num5;
			int length;
			if (num3 > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			num2 = length;
			if (_003F364_003F > _003F362_003F.Length)
			{
				goto IL_00ef;
			}
			goto IL_00f7;
			IL_00ef:
			int num6 = _003F362_003F.Length - 1;
			goto IL_00f7;
			IL_0082:
			goto IL_0006;
			IL_006d:
			goto IL_00ef;
		}

		private string _003F93_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0057: Incompatible stack heights: 0 vs 1
			//IL_005c: Incompatible stack heights: 1 vs 0
			//IL_0061: Incompatible stack heights: 0 vs 2
			//IL_0067: Incompatible stack heights: 0 vs 1
			//IL_006c: Incompatible stack heights: 1 vs 0
			if (_003F362_003F == null)
			{
				return _003F487_003F._003F488_003F("");
			}
			goto IL_0006;
			IL_0047:
			goto IL_0006;
			IL_0006:
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int length;
			if (num > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			return ((string)/*Error near IL_0032: Stack underflow*/).Substring((int)/*Error near IL_0032: Stack underflow*/, length);
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_0089: Incompatible stack heights: 0 vs 1
			//IL_008e: Incompatible stack heights: 1 vs 0
			//IL_0093: Incompatible stack heights: 0 vs 2
			//IL_0099: Incompatible stack heights: 0 vs 1
			//IL_00a6: Incompatible stack heights: 0 vs 1
			if (_003F362_003F == null)
			{
				return _003F487_003F._003F488_003F("");
			}
			goto IL_0006;
			IL_009e:
			int num;
			int num3 = _003F362_003F.Length - num;
			goto IL_00a6;
			IL_0006:
			int num2 = _003F365_003F;
			if (num2 == -9999)
			{
				num2 = _003F362_003F.Length;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			int length;
			if (_003F363_003F > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			num = length;
			if (num + num2 > _003F362_003F.Length)
			{
				goto IL_009e;
			}
			goto IL_00a6;
			IL_0047:
			goto IL_009e;
			IL_00a6:
			return ((string)/*Error near IL_00ab: Stack underflow*/).Substring((int)/*Error near IL_00ab: Stack underflow*/, (int)/*Error near IL_00ab: Stack underflow*/);
			IL_005c:
			goto IL_0006;
		}

		private string _003F95_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_004b: Incompatible stack heights: 0 vs 1
			//IL_0050: Incompatible stack heights: 1 vs 0
			//IL_0055: Incompatible stack heights: 0 vs 1
			//IL_0062: Incompatible stack heights: 0 vs 1
			//IL_0068: Incompatible stack heights: 0 vs 1
			if (_003F362_003F == null)
			{
				return _003F487_003F._003F488_003F("");
			}
			goto IL_0006;
			IL_003b:
			goto IL_0006;
			IL_0006:
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				goto IL_0067;
			}
			int num2 = _003F362_003F.Length - num;
			goto IL_0068;
			IL_0068:
			return ((string)/*Error near IL_006d: Stack underflow*/).Substring((int)/*Error near IL_006d: Stack underflow*/);
			IL_0026:
			goto IL_0067;
			IL_0067:
			goto IL_0068;
		}

		private int _003F96_003F(string _003F362_003F)
		{
			if (_003F362_003F == null)
			{
				return 0;
			}
			goto IL_0006;
			IL_005d:
			goto IL_0006;
			IL_0006:
			if (_003F362_003F == _003F487_003F._003F488_003F(""))
			{
				return 0;
			}
			goto IL_001b;
			IL_0069:
			goto IL_001b;
			IL_001b:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_0030;
			IL_0075:
			goto IL_0030;
			IL_0030:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_0045;
			IL_0081:
			goto IL_0045;
			IL_0045:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_0092;
			IL_008d:
			goto IL_0092;
			IL_0092:
			return Convert.ToInt32(_003F362_003F);
		}

		private bool _003F97_003F(string _003F366_003F)
		{
			return new Regex(_003F487_003F._003F488_003F("Kļɏ\u033fѭՌؤܧ࠲ॐ੯ଡడ\u0d54ษཚၡᄯሪጽᐥ")).IsMatch(_003F366_003F);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a63᭺\u1c7cᵻṩὲ⁸Ⅸ≦⍠④┫♼❢⡯⥭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0017:
			goto IL_000b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((SurveyRange)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				OrderInfo = (TextBlock)_003F350_003F;
				break;
			case 3:
				City = (TextBlock)_003F350_003F;
				break;
			case 4:
				OrderStart = (TextBlock)_003F350_003F;
				break;
			case 5:
				OrderEnd = (TextBlock)_003F350_003F;
				break;
			case 6:
				PCcode = (TextBlock)_003F350_003F;
				break;
			case 7:
				txtMP3 = (TextBlock)_003F350_003F;
				break;
			case 8:
				cmbCITY = (ComboBox)_003F350_003F;
				cmbCITY.SelectionChanged += _003F216_003F;
				break;
			case 9:
				txtBegin1st = (TextBlock)_003F350_003F;
				break;
			case 10:
				txtBegin = (TextBox)_003F350_003F;
				txtBegin.KeyDown += _003F86_003F;
				txtBegin.TextChanged += _003F98_003F;
				txtBegin.GotFocus += _003F91_003F;
				txtBegin.LostFocus += _003F90_003F;
				break;
			case 11:
				BeginBit = (TextBlock)_003F350_003F;
				break;
			case 12:
				txtEnd1st = (TextBlock)_003F350_003F;
				break;
			case 13:
				txtEnd = (TextBox)_003F350_003F;
				txtEnd.KeyDown += _003F86_003F;
				txtEnd.TextChanged += _003F98_003F;
				txtEnd.GotFocus += _003F91_003F;
				txtEnd.LostFocus += _003F90_003F;
				break;
			case 14:
				EndBit = (TextBlock)_003F350_003F;
				break;
			case 15:
				PCcode1st = (TextBlock)_003F350_003F;
				break;
			case 16:
				txtPCCode = (TextBox)_003F350_003F;
				txtPCCode.KeyDown += _003F86_003F;
				txtPCCode.TextChanged += _003F98_003F;
				txtPCCode.GotFocus += _003F91_003F;
				txtPCCode.LostFocus += _003F90_003F;
				break;
			case 17:
				PCcodeMsg = (TextBlock)_003F350_003F;
				break;
			case 18:
				passwordBox1 = (PasswordBox)_003F350_003F;
				passwordBox1.GotFocus += _003F91_003F;
				passwordBox1.LostFocus += _003F90_003F;
				passwordBox1.KeyDown += _003F217_003F;
				break;
			case 19:
				PasswordMsg = (TextBlock)_003F350_003F;
				break;
			case 20:
				ChkTouchPad = (CheckBox)_003F350_003F;
				break;
			case 21:
				spMP3Setup = (StackPanel)_003F350_003F;
				break;
			case 22:
				btnMP3Setup = (Button)_003F350_003F;
				btnMP3Setup.Click += _003F214_003F;
				break;
			case 23:
				spMP3 = (StackPanel)_003F350_003F;
				break;
			case 24:
				txtMP3Len = (TextBox)_003F350_003F;
				txtMP3Len.KeyDown += _003F86_003F;
				txtMP3Len.TextChanged += _003F98_003F;
				txtMP3Len.GotFocus += _003F91_003F;
				txtMP3Len.LostFocus += _003F90_003F;
				break;
			case 25:
				btnMP3 = (Button)_003F350_003F;
				btnMP3.Click += _003F215_003F;
				break;
			case 26:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			case 27:
				btnSave = (Button)_003F350_003F;
				btnSave.Click += _003F211_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
