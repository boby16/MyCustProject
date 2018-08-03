using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Gssy.Capi
{
	public class AutoDo : Window, IComponentConnector
	{
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private UDPX oFunc = new UDPX();

		private string CurPageId;

		private QMultiple oQuestion = new QMultiple();

		private List<Button> listButton = new List<Button>();

		internal TextBlock txtProjectName;

		internal TextBlock txtCity;

		internal TextBlock txtSelCity;

		internal Button btnUnSel;

		internal Button btnSelAll;

		internal WrapPanel wpCity;

		internal TextBox txtStartNumber;

		internal TextBox txtCount;

		internal Button btnFillMode;

		internal Button btnDo;

		internal Button btnExit;

		private bool _contentLoaded;

		public AutoDo()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00c1: Incompatible stack heights: 0 vs 1
			//IL_00c6: Incompatible stack heights: 1 vs 0
			//IL_00d6: Incompatible stack heights: 0 vs 1
			//IL_00db: Incompatible stack heights: 1 vs 0
			//IL_00e0: Incompatible stack heights: 0 vs 1
			//IL_00e6: Incompatible stack heights: 0 vs 1
			//IL_00eb: Incompatible stack heights: 1 vs 0
			//IL_00f0: Incompatible stack heights: 0 vs 1
			//IL_00f6: Incompatible stack heights: 0 vs 1
			//IL_0100: Incompatible stack heights: 0 vs 1
			string byCodeText = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("SŤɤ\u0360ъբ\u0653ݘ\u087e२\u0a7a୳\u0c48൰\u0e69ཡ\u1067ᅳ"));
			string text;
			if (byCodeText == null)
			{
				text = _003F487_003F._003F488_003F("");
			}
			else
			{
				byCodeText.Trim();
			}
			byCodeText = text;
			string byCodeText2 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Mžɾ\u0366ьը\u0659\u0746\u086bॶ੬୵"));
			string text2;
			if (byCodeText2 == null)
			{
				text2 = _003F487_003F._003F488_003F("");
			}
			else
			{
				byCodeText2.Trim();
			}
			byCodeText2 = text2;
			TextBox txtStartNumber2 = txtStartNumber;
			string text3;
			if (byCodeText == _003F487_003F._003F488_003F(""))
			{
				text3 = _003F487_003F._003F488_003F("0");
			}
			((TextBox)/*Error near IL_0087: Stack underflow*/).Text = text3;
			TextBox txtCount2 = txtCount;
			if (byCodeText2 == _003F487_003F._003F488_003F(""))
			{
				_003F487_003F._003F488_003F("0");
			}
			((TextBox)/*Error near IL_0105: Stack underflow*/).Text = (string)/*Error near IL_0105: Stack underflow*/;
			txtProjectName.Text = SurveyMsg.MsgProjectName;
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("SŤɤ\u0360ъբ\u0653ݘ\u087e२\u0a7a୳\u0c48൰\u0e69ཡ\u1067ᅳ"), txtStartNumber.Text);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("Mžɾ\u0366ьը\u0659\u0746\u086bॶ੬୵"), txtCount.Text);
			CurPageId = _003F487_003F._003F488_003F("GŊɖ\u0358");
			oQuestion.Init(CurPageId, 0, true);
			txtCity.Text = oQuestion.QDefine.QUESTION_TITLE + _003F487_003F._003F488_003F("；");
			SurveyHelper.StopFillPage = _003F487_003F._003F488_003F("");
			_003F28_003F();
			SurveyHelper.AutoDo_listCity.Clear();
			_003F29_003F(listButton[0], _003F348_003F);
			txtStartNumber.Focus();
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new CStart().Show();
			Close();
		}

		private void _003F26_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Expected I4, but got Unknown
			//IL_01b6: Incompatible stack heights: 0 vs 2
			//IL_01d0: Incompatible stack heights: 0 vs 1
			//IL_01f6: Incompatible stack heights: 0 vs 4
			int num = SurveyHelper.AutoDo_listCity.Count * oFunc.StringToInt(txtCount.Text.Trim());
			if (num == 0)
			{
				MessageBox.Show(SurveyMsg.MsgAutoDo_NoCase, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			goto IL_002d;
			IL_0182:
			goto IL_0214;
			IL_002d:
			if (MessageBox.Show(string.Format(SurveyMsg.MsgAutoDo_Ask, num.ToString()), SurveyMsg.MsgAutoDo_AskTitle, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
			{
				Button btnDo2 = btnDo;
				((UIElement)/*Error near IL_0057: Stack underflow*/).IsEnabled = ((byte)/*Error near IL_0057: Stack underflow*/ != 0);
				SurveyHelper.AutoDo_Start = DateTime.Now;
				SurveyHelper.AutoDo = true;
				SurveyHelper.AutoFill = true;
				SurveyHelper.AutoDo_CityOrder = 0;
				SurveyHelper.AutoDo_StartOrder = oFunc.StringToInt(txtStartNumber.Text.Trim());
				SurveyHelper.AutoDo_Total = oFunc.StringToInt(txtCount.Text.Trim());
				SurveyHelper.AutoDo_Count = 0;
				string byCodeText = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Vņɇ\u036cѦդ"));
				if (byCodeText != null)
				{
					bool flag = byCodeText == _003F487_003F._003F488_003F("");
					if ((int)/*Error near IL_01d5: Stack underflow*/ == 0)
					{
						string byCodeText2 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("^ŹɹͼѬձ\u064e\u0742ࡇॡ\u0a64୫౯"));
						string byCodeText3 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Xſɻ;Ѣտ\u064c\u0740ࡆ६\u0a65"));
						string byCodeText4 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("\\Ũɳ\u0366ѬՓ٣ݥ"));
						if (byCodeText2 != _003F487_003F._003F488_003F(""))
						{
							int length = byCodeText2.Length;
							int length2 = SurveyMsg.SurveyIDEnd.Length;
							_003F val = /*Error near IL_0147: Stack underflow*/- /*Error near IL_0147: Stack underflow*/;
							SurveyHelper.SurveyCity = ((string)/*Error near IL_014c: Stack underflow*/).Substring((int)/*Error near IL_014c: Stack underflow*/, (int)val);
							SurveyMsg.SurveyIDBegin = byCodeText2;
							SurveyMsg.SurveyIDEnd = byCodeText3;
							SurveyHelper.SurveyStart = SurveyHelper.SurveyCodePage;
							if (!(byCodeText4 == _003F487_003F._003F488_003F("0")))
							{
								goto IL_0214;
							}
							SurveyHelper.IsTouch = _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64");
						}
						goto IL_0223;
					}
				}
				MessageBox.Show(SurveyMsg.MsgNeedConfig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			return;
			IL_01a0:
			goto IL_002d;
			IL_0223:
			SurveyHelper.SurveyStart = _003F487_003F._003F488_003F("GŊɖ\u0358");
			MainWindow mainWindow = new MainWindow();
			mainWindow.ShowDialog();
			mainWindow.Close();
			return;
			IL_0214:
			SurveyHelper.IsTouch = _003F487_003F._003F488_003F("Dſɟ\u0365Ѽիٯݙ\u0863॥੯ୱ\u0c64");
			goto IL_0223;
		}

		private void _003F27_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgRightTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void _003F28_003F()
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wpCity;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Margin = new Thickness(15.0, 0.0, 0.0, 15.0);
				button.Style = style;
				button.Tag = qDetail.CODE;
				button.Click += _003F29_003F;
				button.FontSize = 18.0;
				button.MinWidth = 80.0;
				button.MinHeight = 30.0;
				wrapPanel.Children.Add(button);
				listButton.Add(button);
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Button button = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			string item = button.Tag.ToString();
			if (button.Style != style)
			{
				SurveyHelper.AutoDo_listCity.Add(item);
				button.Style = style;
			}
			else
			{
				SurveyHelper.AutoDo_listCity.Remove(item);
				button.Style = style2;
			}
			txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		private void _003F30_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			foreach (Button item in listButton)
			{
				item.Style = style;
			}
			SurveyHelper.AutoDo_listCity.Clear();
			txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		private void _003F31_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			SurveyHelper.AutoDo_listCity.Clear();
			foreach (Button item2 in listButton)
			{
				item2.Style = style;
				string item = item2.Tag.ToString();
				SurveyHelper.AutoDo_listCity.Add(item);
			}
			txtSelCity.Text = string.Format(SurveyMsg.MsgAutoDo_CityCount, SurveyHelper.AutoDo_listCity.Count);
		}

		private void _003F32_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new FillMode().ShowDialog();
		}

		private void _003F33_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			if (_003F348_003F.Key == Key.Return)
			{
				txtCount.Focus();
			}
		}

		private void _003F34_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("SŤɤ\u0360ъբ\u0653ݘ\u087e२\u0a7a୳\u0c48൰\u0e69ཡ\u1067ᅳ"), txtStartNumber.Text);
		}

		private void _003F35_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("Mžɾ\u0366ьը\u0659\u0746\u086bॶ੬୵"), txtCount.Text);
		}

		private void _003F36_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			txtCount.SelectAll();
		}

		private void _003F37_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			txtStartNumber.SelectAll();
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u000fŘɭ\u036eѥԵ\u0659ݸ\u0868ॾਭ୶౻ൾ\u0e62\u0f7eၾᅪበ፹ᐣᕪᙿ\u177dᡧᥣ\u1a69ᬫ\u1c7c\u1d62ṯὭ"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0018:
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
				((AutoDo)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				txtProjectName = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtCity = (TextBlock)_003F350_003F;
				break;
			case 4:
				txtSelCity = (TextBlock)_003F350_003F;
				break;
			case 5:
				btnUnSel = (Button)_003F350_003F;
				btnUnSel.Click += _003F30_003F;
				break;
			case 6:
				btnSelAll = (Button)_003F350_003F;
				btnSelAll.Click += _003F31_003F;
				break;
			case 7:
				wpCity = (WrapPanel)_003F350_003F;
				break;
			case 8:
				txtStartNumber = (TextBox)_003F350_003F;
				txtStartNumber.KeyDown += _003F33_003F;
				txtStartNumber.LostFocus += _003F34_003F;
				txtStartNumber.GotFocus += _003F37_003F;
				break;
			case 9:
				txtCount = (TextBox)_003F350_003F;
				txtCount.LostFocus += _003F35_003F;
				txtCount.GotFocus += _003F36_003F;
				break;
			case 10:
				((Image)_003F350_003F).MouseUp += _003F27_003F;
				break;
			case 11:
				btnFillMode = (Button)_003F350_003F;
				btnFillMode.Click += _003F32_003F;
				break;
			case 12:
				btnDo = (Button)_003F350_003F;
				btnDo.Click += _003F26_003F;
				break;
			case 13:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0041:
			goto IL_004b;
		}
	}
}
