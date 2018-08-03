using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
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
	public class ReloadData : Window, IComponentConnector
	{
		private List<Button> listBtn = new List<Button>();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtTitle;

		internal WrapPanel w;

		internal Button btn1;

		internal Button btn2;

		internal Button btn3;

		internal TextBox StopFillPageId;

		internal Button btnSave;

		internal Button btnCancel;

		private bool _contentLoaded;

		public ReloadData()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			base.Topmost = true;
			Hide();
			Show();
			string byCodeText = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("XŬɤ\u0368ѧա\u0640ݢ\u0876ॠ"));
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
		}

		private void _003F128_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Close();
		}

		private void _003F211_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			SurveyHelper.StopFillPage = StopFillPageId.Text;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕤᙱ\u1777\u187bᥥᨿ\u1b7dᱫ\u1d61ṣὪ\u206eⅭ≩⍳⑧┫♼❢⡯⥭"), UriKind.Relative);
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
				((ReloadData)_003F350_003F).Loaded += _003F24_003F;
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
				break;
			case 9:
				btnSave = (Button)_003F350_003F;
				btnSave.Click += _003F211_003F;
				break;
			case 10:
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
