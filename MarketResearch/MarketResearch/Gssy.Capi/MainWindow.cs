using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.QEdit;
using Microsoft.DirectX.DirectSound;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;
using WawaSoft.Media;

namespace Gssy.Capi
{
	public class MainWindow : Window, IComponentConnector
	{
		private string MySurveyId = _003F487_003F._003F488_003F("");

		private string CurPageId = _003F487_003F._003F488_003F("");

		private NavBase MyNav = new NavBase();

		public SurveyBiz oSurveybiz = new SurveyBiz();

		private LogicExplain oLogicExplain = new LogicExplain();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private bool l1stUpdated;

		private UDPX oFunc = new UDPX();

		private string ReadMp3Path = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŋɠ\u0360Ѫգ\u065d");

		private CaptureSound record;

		private Guid deviceGuid = Guid.Empty;

		private string waveFileName = _003F487_003F._003F488_003F("");

		internal Grid LayoutRoot;

		internal Grid PageArea;

		internal WrapPanel DockPanel1;

		internal TextBlock txtSurveyId;

		internal Button btnGoBack;

		internal Button btnRecord;

		internal Button btnTaptip;

		internal Button btnRead;

		internal Button btnCheck;

		internal Button btnAutoCapture;

		internal Button btnCapture;

		internal Button btnAutoFill;

		internal Button btnFillMode;

		internal ComboBox cmbJump;

		internal Button btnGo;

		internal Button btnExit;

		internal GroupBox GroupRecord;

		internal StackPanel StkRecord;

		internal MediaElement mediaElement;

		internal Frame frame1;

		private bool _contentLoaded;

		public MainWindow()
		{
			//IL_0326: Incompatible stack heights: 0 vs 2
			//IL_0336: Incompatible stack heights: 0 vs 1
			//IL_0376: Incompatible stack heights: 0 vs 2
			//IL_03af: Incompatible stack heights: 0 vs 1
			//IL_03c6: Incompatible stack heights: 0 vs 1
			//IL_03f1: Incompatible stack heights: 0 vs 1
			InitializeComponent();
			base.Topmost = true;
			Hide();
			Show();
			_003F63_003F();
			MyNav.GetJump();
			cmbJump.ItemsSource = MyNav.NavQJump;
			cmbJump.DisplayMemberPath = _003F487_003F._003F488_003F("Yŉɀ\u0343њՐنݚࡕ");
			cmbJump.SelectedValuePath = _003F487_003F._003F488_003F("Zňɏ\u0342љՓمݏࡗ\u0944");
			if (!(SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64")))
			{
				btnRecord.IsEnabled = false;
			}
			else
			{
				btnRecord.IsEnabled = true;
			}
			DockPanel1.Visibility = Visibility.Collapsed;
			btnGoBack.Visibility = Visibility.Hidden;
			btnTaptip.Visibility = Visibility.Hidden;
			btnCheck.Visibility = Visibility.Hidden;
			btnAutoCapture.Visibility = Visibility.Collapsed;
			btnCapture.Visibility = Visibility.Collapsed;
			btnAutoFill.Visibility = Visibility.Collapsed;
			btnFillMode.Visibility = Visibility.Hidden;
			btnGo.Visibility = Visibility.Hidden;
			btnExit.Visibility = Visibility.Hidden;
			cmbJump.Visibility = Visibility.Hidden;
			btnRecord.Visibility = Visibility.Hidden;
			btnRead.Visibility = Visibility.Hidden;
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				Button btnTaptip2 = btnTaptip;
				((UIElement)/*Error near IL_01e0: Stack underflow*/).Visibility = (Visibility)/*Error near IL_01e0: Stack underflow*/;
			}
			if (SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64"))
			{
				Button btnRecord2 = btnRecord;
				((UIElement)/*Error near IL_01ff: Stack underflow*/).Visibility = Visibility.Visible;
			}
			else
			{
				btnRecord.Visibility = Visibility.Hidden;
			}
			if (!(SurveyMsg.ReadIsOn == _003F487_003F._003F488_003F("_ũɪ\u036eрջوݨ\u085a॰\u0a71୷\u0c64")))
			{
				btnRead.Visibility = Visibility.Hidden;
			}
			else
			{
				btnRead.Visibility = Visibility.Visible;
			}
			int num = SurveyMsg.VersionID.IndexOf('v');
			if (num > -1)
			{
				oFunc.LEFT(SurveyMsg.VersionID, num);
				string b = _003F487_003F._003F488_003F((string)/*Error near IL_0253: Stack underflow*/);
				if (!((string)/*Error near IL_0258: Stack underflow*/ == b))
				{
					btnCapture.Visibility = Visibility.Collapsed;
				}
				else
				{
					btnCapture.Visibility = Visibility.Visible;
				}
			}
			if (File.Exists(SurveyHelper.DebugFlagFile))
			{
				bool flag = SurveyHelper.ShowAutoFill == _003F487_003F._003F488_003F("BŸɠ\u0379ьչٿݥࡏॡ੫୪ౚ൰\u0e71\u0f77\u1064");
				if ((int)/*Error near IL_03b4: Stack underflow*/ != 0)
				{
					btnCheck.Visibility = Visibility.Visible;
					((MainWindow)/*Error near IL_0287: Stack underflow*/).btnAutoFill.Visibility = Visibility.Visible;
					btnFillMode.Visibility = Visibility.Visible;
				}
			}
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				DockPanel1.Visibility = Visibility.Visible;
				btnGoBack.Visibility = Visibility.Visible;
				btnExit.Visibility = Visibility.Visible;
				btnTaptip.Visibility = Visibility.Visible;
			}
			else if (SurveyMsg.ShowToolBar == _003F487_003F._003F488_003F("Cŧɡ\u037aјդ٥ݥࡊ०ੴ\u0b5a\u0c70൱\u0e77ཤ"))
			{
				WrapPanel dockPanel = DockPanel1;
				((UIElement)/*Error near IL_03f6: Stack underflow*/).Visibility = Visibility.Visible;
				btnGoBack.Visibility = Visibility.Visible;
				btnExit.Visibility = Visibility.Visible;
			}
			frame1.InputBindings.Add(new InputBinding(ApplicationCommands.NotACommand, new KeyGesture(Key.Back)));
			frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseBack, _003F73_003F));
			frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.Refresh, _003F73_003F));
			frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseForward, _003F73_003F));
			frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseForward, _003F73_003F));
			frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseHome, _003F73_003F));
			frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseStop, _003F73_003F));
			l1stUpdated = true;
		}

		private void _003F63_003F()
		{
			CurPageId = SurveyHelper.SurveyStart;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			MyNav.StartPage(CurPageId, roadMapVersion);
			string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), MyNav.RoadMap.FORM_NAME);
			frame1.NavigationService.Navigate(new Uri(uriString));
			if (SurveyHelper.NavLoad == 0)
			{
				SurveyHelper.SurveySequence = 1;
			}
			SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
		}

		internal void _003F64_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			int surveySequence = SurveyHelper.SurveySequence;
			SurveyHelper.AutoFill = false;
			btnAutoFill.Style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			if (SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Xſɻ;Ѣտ\u0654ݱ\u0866॰\u0a78"))
			{
				DockPanel1.Visibility = Visibility.Collapsed;
			}
			else if (!SurveyHelper.TestVersion && (SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Oŧɬ\u0354ѳը٩ݢ\u0870ॸ") || SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Iťɮ\u035dѭյ٫ݬ\u086a\u0962\u0a76\u0b64")))
			{
				DockPanel1.Visibility = Visibility.Collapsed;
			}
			else if (MySurveyId == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotBack, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else if (CurPageId == SurveyHelper.SurveyFirstPage)
			{
				MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else
			{
				if (oFunc.LEFT(CurPageId, 7) == _003F487_003F._003F488_003F("Tœɗ\u0352ц՛\u065e"))
				{
					if (!SurveyHelper.TestVersion)
					{
						MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						return;
					}
					MessageBox.Show(SurveyMsg.MsgFirstPageInfo, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				}
				SurveyHelper.NavOperation = _003F487_003F._003F488_003F("FŢɡ\u036a");
				if (SurveyHelper.NavGoBackTimes == 0)
				{
					SurveyHelper.NavGoBackTimes = 1;
				}
				else
				{
					if (!MessageBox.Show(SurveyMsg.MsgBackTimes, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
					{
						return;
					}
					oSurveybiz.ClearPageAnswer(MySurveyId, surveySequence);
				}
				string roadMapVersion = SurveyHelper.RoadMapVersion;
				MyNav.PrePage(MySurveyId, surveySequence, roadMapVersion);
				SurveyHelper.RoadMapVersion = MyNav.Sequence.VERSION_ID.ToString();
				SurveyHelper.CircleACount = MyNav.Sequence.CIRCLE_A_COUNT;
				SurveyHelper.CircleACurrent = MyNav.Sequence.CIRCLE_A_CURRENT;
				SurveyHelper.CircleBCount = MyNav.Sequence.CIRCLE_B_COUNT;
				SurveyHelper.CircleBCurrent = MyNav.Sequence.CIRCLE_B_CURRENT;
				try
				{
					string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), MyNav.RoadMap.FORM_NAME);
					if (MyNav.RoadMap.FORM_NAME.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
					{
						uriString = string.Format(_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭"), MyNav.RoadMap.FORM_NAME);
					}
					if (MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
					{
						frame1.NavigationService.Refresh();
					}
					else
					{
						frame1.NavigationService.Navigate(new Uri(uriString));
					}
					SurveyHelper.SurveySequence = surveySequence - 1;
					SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
					SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
				}
				catch (Exception)
				{
					string text = string.Format(SurveyMsg.MsgErrorGoBack, MySurveyId, CurPageId, MyNav.Sequence.VERSION_ID, MyNav.Sequence.PAGE_ID, MyNav.RoadMap.FORM_NAME);
					MessageBox.Show(text + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					oLogicExplain.OutputResult(text, _003F487_003F._003F488_003F("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"), true);
					_003F25_003F(_003F347_003F, _003F348_003F);
				}
			}
		}

		private void _003F65_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			SurveyHelper.AutoFill = false;
			btnAutoFill.Style = style;
			if (SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Oŧɬ\u0354ѳը٩ݢ\u0870ॸ") || SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Iťɮ\u035dѭյ٫ݬ\u086a\u0962\u0a76\u0b64") || SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Xſɻ;Ѣտ\u0654ݱ\u0866॰\u0a78"))
			{
				DockPanel1.Visibility = Visibility.Collapsed;
			}
			else if (MySurveyId == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNoID, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else if (CurPageId == SurveyHelper.SurveyFirstPage)
			{
				MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else if (cmbJump.SelectedValue == null || (string)cmbJump.SelectedValue == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotGoPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else
			{
				SurveyHelper.NavOperation = _003F487_003F._003F488_003F("NŶɯͱ");
				string[] array = cmbJump.SelectedValue.ToString().Split('|');
				SurveyHelper.CircleACount = int.Parse(array[1]);
				SurveyHelper.CircleACurrent = int.Parse(array[2]);
				SurveyHelper.CircleBCount = int.Parse(array[3]);
				SurveyHelper.CircleBCurrent = int.Parse(array[4]);
				int surveySequence = SurveyHelper.SurveySequence;
				string roadMapVersion = SurveyHelper.RoadMapVersion;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleBCount = SurveyHelper.CircleBCount;
				MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
				MyNav.GoPage(MySurveyId, surveySequence, array[0], roadMapVersion);
				try
				{
					string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), MyNav.RoadMap.FORM_NAME);
					if (MyNav.RoadMap.FORM_NAME.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
					{
						uriString = string.Format(_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭"), MyNav.RoadMap.FORM_NAME);
					}
					if (MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
					{
						frame1.NavigationService.Refresh();
					}
					else
					{
						frame1.NavigationService.Navigate(new Uri(uriString));
					}
					SurveyHelper.SurveySequence = surveySequence + 1;
					SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
					SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
					SurveyHelper.NavGoBackTimes = 0;
					SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
					SurveyHelper.NavLoad = 0;
				}
				catch (Exception)
				{
					string text = string.Format(SurveyMsg.MsgErrorJump, MySurveyId, CurPageId, roadMapVersion, array[0], MyNav.RoadMap.FORM_NAME);
					MessageBox.Show(SurveyMsg.MsgWrongJump + Environment.NewLine + Environment.NewLine + text + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					oLogicExplain.OutputResult(text, _003F487_003F._003F488_003F("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"), true);
				}
			}
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_010f: Incompatible stack heights: 0 vs 1
			//IL_0128: Incompatible stack heights: 0 vs 2
			//IL_014c: Incompatible stack heights: 0 vs 2
			//IL_0168: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Oŧɬ\u0354ѳը٩ݢ\u0870ॸ")))
			{
				bool flag = SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Iťɮ\u035dѭյ٫ݬ\u086a\u0962\u0a76\u0b64");
				if ((int)/*Error near IL_0114: Stack underflow*/ == 0)
				{
					string navCurPage = SurveyHelper.NavCurPage;
					_003F487_003F._003F488_003F("Xſɻ;Ѣտ\u0654ݱ\u0866॰\u0a78");
					if (!((string)/*Error near IL_003f: Stack underflow*/ == (string)/*Error near IL_003f: Stack underflow*/))
					{
						if (MessageBox.Show(string.Format(SurveyMsg.MsgExitBreak, SurveyHelper.SurveyID), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
						{
							string mySurveyId = MySurveyId;
							_003F487_003F._003F488_003F("");
							if ((string)/*Error near IL_008b: Stack underflow*/ != (string)/*Error near IL_008b: Stack underflow*/)
							{
								oSurveybiz.CloseSurveyByExit(MySurveyId, 3);
								if ((int)/*Error near IL_016d: Stack underflow*/ != 0)
								{
									MyNav.CircleACount = SurveyHelper.CircleACount;
									MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
									MyNav.CircleBCount = SurveyHelper.CircleBCount;
									MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
									MyNav.UpdateSurveyMain(MySurveyId, surveySequence, CurPageId, Convert.ToInt32(SurveyHelper.RoadMapVersion));
								}
							}
							Close();
							Application.Current.Shutdown();
						}
						return;
					}
				}
			}
			DockPanel1.Visibility = Visibility.Collapsed;
			return;
			IL_00ec:;
		}

		private void _003F66_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_01f3: Incompatible stack heights: 0 vs 2
			//IL_01ff: Incompatible stack heights: 0 vs 2
			//IL_021b: Incompatible stack heights: 0 vs 4
			//IL_0249: Incompatible stack heights: 0 vs 1
			//IL_0269: Incompatible stack heights: 0 vs 1
			//IL_0287: Incompatible stack heights: 0 vs 1
			//IL_02a5: Incompatible stack heights: 0 vs 1
			//IL_02bf: Incompatible stack heights: 0 vs 1
			//IL_02d0: Incompatible stack heights: 0 vs 2
			//IL_02e0: Incompatible stack heights: 0 vs 1
			//IL_0328: Incompatible stack heights: 0 vs 1
			//IL_033a: Incompatible stack heights: 0 vs 2
			//IL_0345: Incompatible stack heights: 0 vs 1
			//IL_0355: Incompatible stack heights: 0 vs 1
			//IL_0360: Incompatible stack heights: 0 vs 1
			bool debug = SurveyHelper.Debug;
			bool testVersion = SurveyHelper.TestVersion;
			bool flag = false;
			if (SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64"))
			{
				flag = true;
			}
			if (_003F348_003F.Key == Key.Back)
			{
				return;
			}
			goto IL_0033;
			IL_0158:
			if (_003F348_003F.Key != Key.F12)
			{
				return;
			}
			_003F348_003F.KeyboardDevice.IsKeyDown(Key.LeftCtrl);
			if ((int)/*Error near IL_032d: Stack underflow*/ == 0)
			{
				return;
			}
			KeyboardDevice keyboardDevice = _003F348_003F.KeyboardDevice;
			if (!((KeyboardDevice)/*Error near IL_016f: Stack underflow*/).IsKeyDown((Key)/*Error near IL_016f: Stack underflow*/))
			{
				return;
			}
			((RoutedEventArgs)/*Error near IL_017a: Stack underflow*/).Handled = true;
			if (!testVersion)
			{
				return;
			}
			Button btnGo2 = btnGo;
			if (((UIElement)/*Error near IL_0185: Stack underflow*/).Visibility == Visibility.Visible)
			{
				((MainWindow)/*Error near IL_018f: Stack underflow*/).btnGo.Visibility = Visibility.Hidden;
				btnExit.Visibility = Visibility.Hidden;
				cmbJump.Visibility = Visibility.Hidden;
				if (btnGoBack.Visibility == Visibility.Hidden)
				{
					DockPanel1.Visibility = Visibility.Collapsed;
				}
				return;
			}
			goto IL_01be;
			IL_01dc:
			goto IL_0033;
			IL_0033:
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			if (_003F348_003F.Key == Key.F4)
			{
				KeyboardDevice keyboardDevice2 = _003F348_003F.KeyboardDevice;
				if (((KeyboardDevice)/*Error near IL_005b: Stack underflow*/).IsKeyDown((Key)/*Error near IL_005b: Stack underflow*/))
				{
					((RoutedEventArgs)/*Error near IL_0065: Stack underflow*/).Handled = ((byte)/*Error near IL_0065: Stack underflow*/ != 0);
					if (debug)
					{
						_003F487_003F._003F488_003F("Gũɰ\u0328фԵ");
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_0070: Stack underflow*/, (string)/*Error near IL_0070: Stack underflow*/, (MessageBoxButton)/*Error near IL_0070: Stack underflow*/, (MessageBoxImage)/*Error near IL_0070: Stack underflow*/);
					}
					if (!MessageBox.Show(string.Format(SurveyMsg.MsgExitBreak, SurveyHelper.SurveyID), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
					{
						return;
					}
					_003F25_003F(_003F347_003F, _003F348_003F);
				}
			}
			if (_003F348_003F.Key == Key.F1)
			{
				_003F348_003F.KeyboardDevice.IsKeyDown(Key.LeftShift);
				if ((int)/*Error near IL_024e: Stack underflow*/ != 0)
				{
					_003F348_003F.Handled = true;
					if (!testVersion)
					{
						string navCurPage = SurveyHelper.NavCurPage;
						string b = _003F487_003F._003F488_003F("Oŧɬ\u0354ѳը٩ݢ\u0870ॸ");
						if (!((string)/*Error near IL_00d3: Stack underflow*/ == b))
						{
							bool flag2 = SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Iťɮ\u035dѭյ٫ݬ\u086a\u0962\u0a76\u0b64");
							if ((int)/*Error near IL_028c: Stack underflow*/ == 0)
							{
								bool flag3 = SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Xſɻ;Ѣտ\u0654ݱ\u0866॰\u0a78");
								if ((int)/*Error near IL_02aa: Stack underflow*/ == 0)
								{
									goto IL_00ef;
								}
							}
						}
						btnGoBack.Visibility = Visibility.Collapsed;
						return;
					}
					goto IL_00ef;
				}
			}
			goto IL_0158;
			IL_0372:
			goto IL_01be;
			IL_00ef:
			if (MySurveyId != _003F487_003F._003F488_003F(""))
			{
				Button btnGoBack2 = btnGoBack;
				if (((UIElement)/*Error near IL_010e: Stack underflow*/).Visibility == Visibility.Visible)
				{
					Button btnGoBack3 = btnGoBack;
					((UIElement)/*Error near IL_0118: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0118: Stack underflow*/;
					if (flag)
					{
						Button btnRecord2 = btnRecord;
						((UIElement)/*Error near IL_0124: Stack underflow*/).Visibility = Visibility.Hidden;
					}
					if (btnGo.Visibility == Visibility.Hidden)
					{
						DockPanel1.Visibility = Visibility.Collapsed;
					}
				}
				else
				{
					DockPanel1.Visibility = Visibility.Visible;
					btnGoBack.Visibility = Visibility.Visible;
					if (flag)
					{
						btnRecord.Visibility = Visibility.Visible;
					}
				}
			}
			goto IL_0158;
			IL_01be:
			DockPanel1.Visibility = Visibility.Visible;
			btnGo.Visibility = Visibility.Visible;
			btnExit.Visibility = Visibility.Visible;
			cmbJump.Visibility = Visibility.Visible;
		}

		public bool RecordInit()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Incompatible stack heights: 0 vs 2
			//IL_008f: Incompatible stack heights: 0 vs 3
			CaptureDevicesCollection val = new CaptureDevicesCollection();
			int recordDevice = SurveyHelper.RecordDevice;
			if (val.get_Count() <= 0)
			{
				oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("XŬɫ\u0368Ѵա\u064dݰࡍ९"), _003F487_003F._003F488_003F("BŪɭ\u0362Ѿկكݺࡇ३ਖ਼\u0b63\u0c65൯\u0e71ཤ"));
				MessageBox.Show(SurveyMsg.MsgNoDevice, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return false;
			}
			val.get_Count();
			DeviceInformation val2;
			if (/*Error near IL_0087: Stack underflow*/ < /*Error near IL_0087: Stack underflow*/)
			{
				val2 = val.get_Item(0);
				deviceGuid = val2.get_DriverGuid();
				MessageBox.Show(SurveyMsg.MsgErrorDevice, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else
			{
				val2 = /*Error near IL_0022: Stack underflow*/.get_Item((int)/*Error near IL_0022: Stack underflow*/);
				Guid driverGuid = val2.get_DriverGuid();
				((MainWindow)/*Error near IL_002f: Stack underflow*/).deviceGuid = driverGuid;
			}
			return true;
		}

		public void BeginRecording()
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			string currentDirectory = Environment.CurrentDirectory;
			int recordHz = SurveyHelper.RecordHz;
			try
			{
				WaveFormat val = DirectSoundManager.CreateWaveFormat(recordHz, (short)16, (short)1);
				Capture val2 = new Capture(deviceGuid);
				record = new CaptureSound(val2, val);
				string text = SurveyHelper.SurveyID + string.Format(_003F487_003F._003F488_003F("Hŭȥ\u032eѪի٨\u073dࡂ\u0943ਠ୨౯\u0d55แཀဪᅫቨጩᑰᕱᙼ"), DateTime.Now) + _003F487_003F._003F488_003F("*Ŵɣͷ");
				waveFileName = Path.Combine(currentDirectory + _003F487_003F._003F488_003F("[Ŕɠ\u0367Ѭհ٥"), text);
				record.set_FileName(waveFileName);
				record.Start();
				SurveyHelper.RecordStartTime = DateTime.Now;
				btnRecord.IsEnabled = false;
				btnRecord.Content = SurveyMsg.MsgRecordStart;
				btnRecord.Visibility = Visibility.Hidden;
				oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡕॳ੫୪౪൬\u0e66"), _003F487_003F._003F488_003F("pűɷ\u0364"));
				oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u065aݼ\u0866ॴ\u0a71\u0b50౪൯\u0e64"), (DateTime.Now.Hour * 60 + DateTime.Now.Minute).ToString());
				SurveyHelper.RecordIsRunning = true;
				SurveyHelper.RecordFileName = text;
				MessageBox.Show(SurveyMsg.MsgRecordStartInfo, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			catch (Exception ex)
			{
				oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("XŬɫ\u0368Ѵա\u064dݰࡍ९"), _003F487_003F._003F488_003F("BŪɭ\u0362Ѿկكݺࡇ३ਖ਼\u0b63\u0c65൯\u0e71ཤ"));
				Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
				btnRecord.Style = style;
				btnRecord.Content = SurveyMsg.MsgRecordError;
				MessageBox.Show(SurveyMsg.MsgErrorRecordStart + Environment.NewLine + ex.Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		public void Stop()
		{
			//IL_0112: Incompatible stack heights: 0 vs 1
			//IL_0127: Incompatible stack heights: 0 vs 2
			//IL_0181: Incompatible stack heights: 0 vs 2
			//IL_019d: Incompatible stack heights: 0 vs 1
			//IL_01d6: Incompatible stack heights: 0 vs 2
			if (!SurveyHelper.RecordIsRunning)
			{
				return;
			}
			record.Stop();
			SurveyHelper.RecordIsRunning = false;
			SurveyConfigBiz oSurveyConfigBiz2 = oSurveyConfigBiz;
			string _003F386_003F = _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡕॳ੫୪౪൬\u0e66");
			string _003F523_003F = _003F487_003F._003F488_003F("cťɯͱѤ");
			((SurveyConfigBiz)/*Error near IL_0023: Stack underflow*/).Save(_003F386_003F, _003F523_003F);
			bool flag = true;
			string text = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("GřȻ\u034aѯիٱݷ\u0867ॲ"));
			if (text != null)
			{
				_003F487_003F._003F488_003F("");
				if (!((string)/*Error near IL_0046: Stack underflow*/ == (string)/*Error near IL_0046: Stack underflow*/))
				{
					goto IL_0051;
				}
			}
			text = SurveyHelper.MP3MaxMinutes;
			goto IL_0051;
			IL_0051:
			int num = Convert.ToInt16(text);
			if (num != 0)
			{
				if (num < 999)
				{
					int num2 = Convert.ToInt32(oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u065aݼ\u0866ॴ\u0a71\u0b50౪൯\u0e64")));
					int num3 = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
					if (num < num3 - num2)
					{
						SurveyConfigBiz oSurveyConfigBiz3 = oSurveyConfigBiz;
						_003F487_003F._003F488_003F("JŖȶ\u0349Ѭզ٤");
						string text2 = ((SurveyConfigBiz)/*Error near IL_009f: Stack underflow*/).GetByCodeText((string)/*Error near IL_009f: Stack underflow*/);
						if (text2 == null)
						{
							text2 = SurveyMsg.MP3Mode2;
						}
						if (text2 == SurveyMsg.MP3Mode1)
						{
							flag = ((byte)/*Error near IL_00ba: Stack underflow*/ != 0);
						}
						else if (MessageBox.Show(string.Format(SurveyMsg.MsgMP3Info, text), SurveyMsg.MsgMP3Caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
						{
							flag = false;
						}
					}
				}
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				string text4 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("Xůɫ\u0363");
				_003F487_003F._003F488_003F("dŦɫ\u0360Ъզٺݤ");
				string _003F358_003F = Path.Combine((string)/*Error near IL_00e9: Stack underflow*/, (string)/*Error near IL_00e9: Stack underflow*/);
				string text3 = waveFileName.Replace(_003F487_003F._003F488_003F("[Ŕɠ\u0367Ѭհ٥"), _003F487_003F._003F488_003F("XŎɒ\u0332"));
				text3 = text3.Replace(_003F487_003F._003F488_003F("*Ŵɣͷ"), _003F487_003F._003F488_003F("*Ůɲ\u0332"));
				_003F67_003F(_003F358_003F, waveFileName, text3);
			}
		}

		private void _003F67_003F(string _003F358_003F, string _003F359_003F, string _003F360_003F)
		{
			string arguments = _003F487_003F._003F488_003F("(Œȱ\u0322У") + _003F359_003F + _003F487_003F._003F488_003F("!Ģȣ") + _003F360_003F + _003F487_003F._003F488_003F("#");
			ProcessStartInfo processStartInfo = new ProcessStartInfo(_003F358_003F, arguments);
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.WorkingDirectory = Environment.CurrentDirectory + _003F487_003F._003F488_003F("XŮɲ\u0332");
			Process process = new Process();
			process.StartInfo = processStartInfo;
			process.StartInfo.UseShellExecute = true;
			process.Start();
			process.WaitForExit();
		}

		private void _003F68_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0088: Incompatible stack heights: 0 vs 2
			//IL_0098: Incompatible stack heights: 0 vs 1
			if (SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Xſɻ;Ѣտ\u0654ݱ\u0866॰\u0a78"))
			{
				DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			goto IL_0019;
			IL_00a4:
			goto IL_0046;
			IL_0019:
			if (SurveyHelper.SurveyID != _003F487_003F._003F488_003F(""))
			{
				string recordIsOn = SurveyMsg.RecordIsOn;
				string b = _003F487_003F._003F488_003F((string)/*Error near IL_0037: Stack underflow*/);
				if ((string)/*Error near IL_003c: Stack underflow*/ == b)
				{
					RecordInit();
					if ((int)/*Error near IL_009d: Stack underflow*/ != 0)
					{
						BeginRecording();
					}
				}
				return;
			}
			goto IL_0046;
			IL_0046:
			MessageBox.Show(SurveyMsg.MsgRecordNoSurveyId, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			return;
			IL_006f:
			goto IL_0019;
		}

		private void _003F69_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			if (SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64"))
			{
				Stop();
			}
		}

		private void _003F70_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (File.Exists(SurveyHelper.DebugFlagFile))
			{
				new DebugCheck().ShowDialog();
				return;
			}
			goto IL_002a;
			IL_0025:
			goto IL_002a;
			IL_002a:
			MessageBox.Show(_003F487_003F._003F488_003F("") + Environment.NewLine + SurveyMsg.MsgPrePageAnswer + oSurveybiz.GetInfoBySequenceId(SurveyHelper.SurveyID, SurveyHelper.SurveySequence - 1) + Environment.NewLine + SurveyHelper.ShowInfo(), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void _003F71_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyTaptip.ShowInputPanel();
		}

		private void _003F72_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (DockPanel1.Visibility == Visibility.Collapsed)
			{
				DockPanel1.Visibility = Visibility.Visible;
				return;
			}
			goto IL_002d;
			IL_0028:
			goto IL_002d;
			IL_002d:
			DockPanel1.Visibility = Visibility.Collapsed;
		}

		private void _003F73_003F(object _003F347_003F, ExecutedRoutedEventArgs _003F361_003F)
		{
		}

		private void _003F74_003F(object _003F347_003F, NavigationEventArgs _003F348_003F)
		{
			//IL_005f: Incompatible stack heights: 0 vs 1
			//IL_0088: Incompatible stack heights: 0 vs 1
			if (SurveyHelper.SurveyID != _003F487_003F._003F488_003F(""))
			{
				bool flag = SurveyMsg.OutputHistory == _003F487_003F._003F488_003F("]ŤɤͿѻչلݢ\u0879ॽ੧୵౿\u0d5a\u0e70\u0f71ၷᅤ");
				if ((int)/*Error near IL_0064: Stack underflow*/ != 0)
				{
					string text = _003F487_003F._003F488_003F("");
					if (SurveyHelper.Answer != _003F487_003F._003F488_003F(""))
					{
						_003F487_003F._003F488_003F("4ĳȲ\u0331аԯخܭ\u082cफ\u0a4b୧౻൰\u0e63\u0f77ဤᄹሢፚ");
						text = string.Concat(str1: SurveyHelper.Answer, str2: _003F487_003F._003F488_003F("\\"), str3: Environment.NewLine, str0: (string)/*Error near IL_009c: Stack underflow*/);
					}
					text = text + DateTime.Now.ToString() + _003F487_003F._003F488_003F("=ĦɆ\u0365Ѱէؼ") + SurveyHelper.SurveyID + _003F487_003F._003F488_003F("$ħɕ\u0360ѵՊن\u073c") + SurveyHelper.SurveySequence + _003F487_003F._003F488_003F("*ĥɒ\u0366ѰԼ") + SurveyHelper.RoadMapVersion + _003F487_003F._003F488_003F(")Ĥɒ\u036cм") + SurveyHelper.NavCurPage + _003F487_003F._003F488_003F("'Īɏ\u0367ѵի\u064bݥ\u086e१\u0a3c") + SurveyHelper.CurPageName;
					oLogicExplain.OutputResult(text, _003F487_003F._003F488_003F("@Ůɵͱѫձٻݞ") + SurveyHelper.SurveyID + _003F487_003F._003F488_003F("*ŏɭ\u0366"), true);
					SurveyHelper.Answer = _003F487_003F._003F488_003F("");
				}
			}
		}

		private void _003F75_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00ef: Incompatible stack heights: 0 vs 1
			//IL_0108: Incompatible stack heights: 0 vs 2
			//IL_0129: Incompatible stack heights: 0 vs 4
			//IL_0159: Incompatible stack heights: 0 vs 2
			MySurveyId = SurveyHelper.SurveyID;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			if (!(SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Oŧɬ\u0354ѳը٩ݢ\u0870ॸ")))
			{
				bool flag = SurveyHelper.NavCurPage == _003F487_003F._003F488_003F("Iťɮ\u035dѭյ٫ݬ\u086a\u0962\u0a76\u0b64");
				if ((int)/*Error near IL_00f4: Stack underflow*/ == 0)
				{
					string navCurPage = SurveyHelper.NavCurPage;
					_003F487_003F._003F488_003F("Xſɻ;Ѣտ\u0654ݱ\u0866॰\u0a78");
					if (!((string)/*Error near IL_005a: Stack underflow*/ == (string)/*Error near IL_005a: Stack underflow*/))
					{
						if (MySurveyId == _003F487_003F._003F488_003F(""))
						{
							string msgNotDoIt = SurveyMsg.MsgNotDoIt;
							string msgCaption = SurveyMsg.MsgCaption;
							MessageBox.Show((string)/*Error near IL_0092: Stack underflow*/, (string)/*Error near IL_0092: Stack underflow*/, (MessageBoxButton)/*Error near IL_0092: Stack underflow*/, (MessageBoxImage)/*Error near IL_0092: Stack underflow*/);
						}
						else if (SurveyHelper.AutoFill)
						{
							SurveyHelper.AutoFill = false;
							btnAutoFill.Style = style2;
							SurveyHelper.AutoDo = false;
						}
						else
						{
							SurveyHelper.AutoFill = true;
							btnAutoFill.Style = style;
							if (SurveyHelper.AutoDo_listCity.Count >= SurveyHelper.AutoDo_CityOrder)
							{
								int autoDo_Total = SurveyHelper.AutoDo_Total;
								int autoDo_Count = SurveyHelper.AutoDo_Count;
								if (/*Error near IL_015e: Stack underflow*/ > /*Error near IL_015e: Stack underflow*/)
								{
									SurveyHelper.AutoDo = true;
								}
							}
						}
						return;
					}
				}
			}
			MessageBox.Show(SurveyMsg.MsgNotDoIt, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void _003F32_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new FillMode().ShowDialog();
		}

		private void _003F76_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			if (SurveyHelper.AutoCapture)
			{
				SurveyHelper.AutoCapture = false;
				btnAutoCapture.Style = style2;
				return;
			}
			goto IL_0036;
			IL_005a:
			goto IL_0036;
			IL_0036:
			SurveyHelper.AutoCapture = true;
			btnAutoCapture.Style = style;
		}

		private void _003F77_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_01c2: Incompatible stack heights: 0 vs 3
			//IL_01db: Incompatible stack heights: 0 vs 1
			//IL_01e0: Incompatible stack heights: 1 vs 0
			//IL_01e5: Incompatible stack heights: 0 vs 2
			//IL_01f9: Incompatible stack heights: 0 vs 2
			//IL_020f: Incompatible stack heights: 0 vs 1
			//IL_0214: Incompatible stack heights: 0 vs 2
			//IL_022d: Incompatible stack heights: 0 vs 1
			//IL_0232: Incompatible stack heights: 1 vs 0
			//IL_0237: Incompatible stack heights: 0 vs 2
			//IL_0250: Incompatible stack heights: 0 vs 1
			//IL_0255: Incompatible stack heights: 1 vs 0
			//IL_0264: Incompatible stack heights: 0 vs 1
			//IL_027e: Incompatible stack heights: 0 vs 1
			string[] obj = new string[6]
			{
				SurveyHelper.SurveyID,
				_003F487_003F._003F488_003F("^"),
				SurveyHelper.NavCurPage,
				null,
				null,
				null
			};
			string text;
			if (SurveyHelper.CircleACode == _003F487_003F._003F488_003F(""))
			{
				text = _003F487_003F._003F488_003F("");
			}
			else
			{
				string text5 = _003F487_003F._003F488_003F("]ŀ") + SurveyHelper.CircleACode;
			}
			((object[])/*Error near IL_004e: Stack underflow*/)[(long)/*Error near IL_004e: Stack underflow*/] = text;
			/*Error near IL_004e: Stack underflow*/;
			object text2;
			if (!(SurveyHelper.CircleBCode == _003F487_003F._003F488_003F("")))
			{
				_003F487_003F._003F488_003F("]Ń");
				string circleBCode = SurveyHelper.CircleBCode;
				text2 = (string)/*Error near IL_006e: Stack underflow*/ + (string)/*Error near IL_006e: Stack underflow*/;
			}
			else
			{
				text2 = _003F487_003F._003F488_003F("");
			}
			((object[])/*Error near IL_007e: Stack underflow*/)[(long)/*Error near IL_007e: Stack underflow*/] = text2;
			((object[])/*Error near IL_007e: Stack underflow*/)[5] = _003F487_003F._003F488_003F("*ũɲ\u0366");
			string str = string.Concat((string[])/*Error near IL_0090: Stack underflow*/);
			str = Directory.GetCurrentDirectory() + _003F487_003F._003F488_003F("[Ŗɭ\u036bѷխ\u065d") + str;
			if (File.Exists(str))
			{
				object[] array = new object[10];
				((object[])/*Error near IL_00b2: Stack underflow*/)[0] = SurveyHelper.SurveyID;
				((object[])/*Error near IL_00ba: Stack underflow*/)[1] = _003F487_003F._003F488_003F("^");
				((object[])/*Error near IL_00c7: Stack underflow*/)[2] = SurveyHelper.NavCurPage;
				/*Error near IL_00cf: Stack underflow*/;
				string text3;
				if (SurveyHelper.CircleACode == _003F487_003F._003F488_003F(""))
				{
					text3 = _003F487_003F._003F488_003F("");
				}
				else
				{
					string text6 = _003F487_003F._003F488_003F("]ŀ") + SurveyHelper.CircleACode;
				}
				((object[])/*Error near IL_00fa: Stack underflow*/)[(long)/*Error near IL_00fa: Stack underflow*/] = text3;
				/*Error near IL_00fa: Stack underflow*/;
				string text4;
				if (SurveyHelper.CircleBCode == _003F487_003F._003F488_003F(""))
				{
					text4 = _003F487_003F._003F488_003F("");
				}
				else
				{
					string text7 = _003F487_003F._003F488_003F("]Ń") + SurveyHelper.CircleBCode;
				}
				((object[])/*Error near IL_0125: Stack underflow*/)[(long)/*Error near IL_0125: Stack underflow*/] = text4;
				((object[])/*Error near IL_0125: Stack underflow*/)[5] = _003F487_003F._003F488_003F("^");
				((object[])/*Error near IL_0132: Stack underflow*/)[6] = DateTime.Now.Hour;
				((object[])/*Error near IL_0147: Stack underflow*/)[7] = DateTime.Now.Minute;
				((object[])/*Error near IL_015c: Stack underflow*/)[8] = DateTime.Now.Second;
				((object[])/*Error near IL_0171: Stack underflow*/)[9] = _003F487_003F._003F488_003F("*ũɲ\u0366");
				str = string.Concat((object[])/*Error near IL_0184: Stack underflow*/);
				str = Directory.GetCurrentDirectory() + _003F487_003F._003F488_003F("[Ŗɭ\u036bѷխ\u065d") + str;
			}
			int _003F72_003F = (int)SurveyHelper.Screen_LeftTop;
			if (new ScreenCapture().Capture(str, _003F72_003F))
			{
				bool autoFill = SurveyHelper.AutoFill;
				if ((int)/*Error near IL_0269: Stack underflow*/ == 0)
				{
					string text8 = SurveyMsg.MsgScreenCaptureDone + Environment.NewLine + str;
					MessageBox.Show((string)/*Error near IL_0283: Stack underflow*/);
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgScreenCaptureFail + Environment.NewLine + str);
			}
		}

		private void _003F78_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			if (l1stUpdated)
			{
				SurveyHelper.Screen_LeftTop = DockPanel1.ActualHeight;
				l1stUpdated = false;
			}
		}

		private void _003F79_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0053: Incompatible stack heights: 0 vs 1
			string text = ReadMp3Path + SurveyHelper.NavCurPage + _003F487_003F._003F488_003F("*Ůɲ\u0332");
			if (File.Exists(text))
			{
				mediaElement.Source = new Uri(text, UriKind.Relative);
				((MainWindow)/*Error near IL_002b: Stack underflow*/).mediaElement.Play();
				return;
			}
			goto IL_0053;
			IL_0031:
			goto IL_0053;
			IL_0053:
			MessageBox.Show(SurveyMsg.MsgPageNoMP3, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\vŤɑ\u0352љԱ\u065dݼ\u086cॲਡ\u0b7a\u0c77ൺ\u0e66\u0f7aၺᅶቼ፥ᐿᕢᙯᝤᡢ\u197c\u1a63᭧ᱬ\u1d68ṱἫ⁼Ⅲ≯⍭"), UriKind.Relative);
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
				((MainWindow)_003F350_003F).KeyDown += _003F66_003F;
				((MainWindow)_003F350_003F).Closed += _003F69_003F;
				((MainWindow)_003F350_003F).LayoutUpdated += _003F78_003F;
				break;
			case 2:
				LayoutRoot = (Grid)_003F350_003F;
				break;
			case 3:
				PageArea = (Grid)_003F350_003F;
				break;
			case 4:
				DockPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 5:
				txtSurveyId = (TextBlock)_003F350_003F;
				break;
			case 6:
				btnGoBack = (Button)_003F350_003F;
				btnGoBack.Click += _003F64_003F;
				break;
			case 7:
				btnRecord = (Button)_003F350_003F;
				btnRecord.Click += _003F68_003F;
				break;
			case 8:
				btnTaptip = (Button)_003F350_003F;
				btnTaptip.Click += _003F71_003F;
				break;
			case 9:
				btnRead = (Button)_003F350_003F;
				btnRead.Click += _003F79_003F;
				break;
			case 10:
				btnCheck = (Button)_003F350_003F;
				btnCheck.Click += _003F70_003F;
				break;
			case 11:
				btnAutoCapture = (Button)_003F350_003F;
				btnAutoCapture.Click += _003F76_003F;
				break;
			case 12:
				btnCapture = (Button)_003F350_003F;
				btnCapture.Click += _003F77_003F;
				break;
			case 13:
				btnAutoFill = (Button)_003F350_003F;
				btnAutoFill.Click += _003F75_003F;
				break;
			case 14:
				btnFillMode = (Button)_003F350_003F;
				btnFillMode.Click += _003F32_003F;
				break;
			case 15:
				cmbJump = (ComboBox)_003F350_003F;
				break;
			case 16:
				btnGo = (Button)_003F350_003F;
				btnGo.Click += _003F65_003F;
				break;
			case 17:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			case 18:
				GroupRecord = (GroupBox)_003F350_003F;
				break;
			case 19:
				StkRecord = (StackPanel)_003F350_003F;
				break;
			case 20:
				mediaElement = (MediaElement)_003F350_003F;
				break;
			case 21:
				frame1 = (Frame)_003F350_003F;
				frame1.Navigated += _003F74_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0061:
			goto IL_006b;
		}
	}
}
