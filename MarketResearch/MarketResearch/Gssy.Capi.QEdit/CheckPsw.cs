using Gssy.Capi.Class;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Gssy.Capi.QEdit
{
	public class CheckPsw : Window, IComponentConnector
	{
		internal TextBlock txtTitle;

		internal Grid gridContent;

		internal PasswordBox passwordBox1;

		internal Button btnKeyboard;

		internal Button btnSave;

		internal Button btnCancel;

		private bool _contentLoaded;

		public CheckPsw()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			base.Topmost = true;
			Hide();
			Show();
			passwordBox1.Focus();
		}

		private void _003F128_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Close();
		}

		private void _003F211_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_004a: Incompatible stack heights: 0 vs 1
			string password = passwordBox1.Password;
			if (!(password == _003F487_003F._003F488_003F("")))
			{
				bool flag = password != SurveyMsg.SurveyRangePsw;
				if ((int)/*Error near IL_004f: Stack underflow*/ == 0)
				{
					SurveyMsg.SurveyRangePswOk = true;
					Close();
					return;
				}
			}
			MessageBox.Show(SurveyMsg.MsgRangePswError, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			passwordBox1.Focus();
		}

		private void _003F255_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			if (_003F348_003F.Key == Key.Return)
			{
				_003F211_003F(_003F347_003F, _003F348_003F);
			}
		}

		private void _003F90_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void _003F91_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void _003F253_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyTaptip.ShowInputPanel();
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\aŠɕ\u0356ѝԍ١\u0740ࡐॶਥ\u0b7e\u0c73\u0d76\u0e6a\u0f76ၶᅲቸ፡ᐻᕢᙷ\u1775\u1879\u197bᨡ\u1b6eᱤᵮṩὢ⁸ⅴ≱⌫⑼╢♯❭"), UriKind.Relative);
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
				((CheckPsw)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				txtTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				gridContent = (Grid)_003F350_003F;
				break;
			case 4:
				passwordBox1 = (PasswordBox)_003F350_003F;
				passwordBox1.KeyDown += _003F255_003F;
				passwordBox1.GotFocus += _003F91_003F;
				passwordBox1.LostFocus += _003F90_003F;
				break;
			case 5:
				btnKeyboard = (Button)_003F350_003F;
				btnKeyboard.Click += _003F253_003F;
				break;
			case 6:
				btnSave = (Button)_003F350_003F;
				btnSave.Click += _003F211_003F;
				break;
			case 7:
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
