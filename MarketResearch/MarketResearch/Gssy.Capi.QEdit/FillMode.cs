using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Gssy.Capi.QEdit
{
	public class FillMode : Window, IComponentConnector
	{
		private List<Button> listBtn = new List<Button>();

		private UDPX oFunc = new UDPX();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtTitle;

		internal WrapPanel w;

		internal Button btn1;

		internal Button btn2;

		internal Button btn3;

		internal TextBox StopFillPageId;

		internal CheckBox Capture;

		internal Button btnSave;

		internal Button btnCancel;

		private bool _contentLoaded;

		public FillMode()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			base.Topmost = true;
			Hide();
			Show();
			string byCodeText = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("NŮɪ\u0369щլ٦ݤ"));
			string byCodeText2 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("_ſɥ\u0379юծ٪ݩࡔ\u0962\u0a65\u0b64"));
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			listBtn.Add(btn1);
			listBtn.Add(btn2);
			listBtn.Add(btn3);
			foreach (Button item in listBtn)
			{
				item.Style = style2;
			}
			if (byCodeText == _003F487_003F._003F488_003F("0"))
			{
				btn1.Style = style;
			}
			else if (byCodeText == _003F487_003F._003F488_003F("3"))
			{
				btn2.Style = style;
			}
			else
			{
				btn3.Style = style;
			}
			StopFillPageId.Text = byCodeText2;
			Capture.IsChecked = SurveyHelper.AutoCapture;
		}

		private void _003F128_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Close();
		}

		private void _003F211_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_013c: Incompatible stack heights: 0 vs 2
			//IL_0152: Incompatible stack heights: 0 vs 2
			//IL_01a3: Incompatible stack heights: 0 vs 1
			//IL_01a8: Incompatible stack heights: 1 vs 0
			if (StopFillPageId.Text != _003F487_003F._003F488_003F(""))
			{
				UDPX oFunc2 = oFunc;
				string text2 = StopFillPageId.Text;
				if (!(((UDPX)/*Error near IL_0025: Stack underflow*/).LEFT((string)/*Error near IL_0025: Stack underflow*/, 1) != _003F487_003F._003F488_003F("\"")))
				{
					UDPX oFunc3 = oFunc;
					TextBox stopFillPageId = StopFillPageId;
					string text = ((TextBox)/*Error near IL_003e: Stack underflow*/).Text;
					if (!(((UDPX)/*Error near IL_0044: Stack underflow*/).RIGHT(text, 1) != _003F487_003F._003F488_003F("\"")))
					{
						goto IL_0078;
					}
				}
				MessageBox.Show(SurveyMsg.MsgFillMode_NotJING, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				StopFillPageId.Focus();
				return;
			}
			goto IL_0078;
			IL_0078:
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			if (btn1.Style != style)
			{
				if (btn2.Style != style)
				{
					SurveyHelper.FillMode = _003F487_003F._003F488_003F("2");
				}
				else
				{
					SurveyHelper.FillMode = _003F487_003F._003F488_003F("3");
				}
			}
			else
			{
				SurveyHelper.FillMode = _003F487_003F._003F488_003F("0");
			}
			SurveyHelper.StopFillPage = StopFillPageId.Text;
			bool? isChecked = Capture.IsChecked;
			bool flag = true;
			bool hasValue;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue = isChecked.HasValue;
			}
			SurveyHelper.AutoCapture = hasValue;
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("NŮɪ\u0369щլ٦ݤ"), SurveyHelper.FillMode);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("_ſɥ\u0379юծ٪ݩࡔ\u0962\u0a65\u0b64"), SurveyHelper.StopFillPage);
			Close();
		}

		private void _003F263_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Button button = (Button)_003F347_003F;
			foreach (Button item in listBtn)
			{
				item.Style = style2;
			}
			button.Style = style;
		}

		private void _003F264_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (StopFillPageId.Text == _003F487_003F._003F488_003F(""))
			{
				StopFillPageId.Text = _003F487_003F._003F488_003F(" ĳȢ");
				StopFillPageId.SelectAll();
			}
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\aŠɕ\u0356ѝԍ١\u0740ࡐॶਥ\u0b7e\u0c73\u0d76\u0e6a\u0f76ၶᅲቸ፡ᐻᕢᙷ\u1775\u1879\u197bᨡ\u1b6bᱥ\u1d67Ṧὤ\u2067Ⅳ≣⌫⑼╢♯❭"), UriKind.Relative);
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
				((FillMode)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				w = (WrapPanel)_003F350_003F;
				break;
			case 5:
				btn1 = (Button)_003F350_003F;
				btn1.Click += _003F263_003F;
				break;
			case 6:
				btn2 = (Button)_003F350_003F;
				btn2.Click += _003F263_003F;
				break;
			case 7:
				btn3 = (Button)_003F350_003F;
				btn3.Click += _003F263_003F;
				break;
			case 8:
				StopFillPageId = (TextBox)_003F350_003F;
				StopFillPageId.GotFocus += _003F264_003F;
				break;
			case 9:
				Capture = (CheckBox)_003F350_003F;
				break;
			case 10:
				btnSave = (Button)_003F350_003F;
				btnSave.Click += _003F211_003F;
				break;
			case 11:
				btnCancel = (Button)_003F350_003F;
				btnCancel.Click += _003F128_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
