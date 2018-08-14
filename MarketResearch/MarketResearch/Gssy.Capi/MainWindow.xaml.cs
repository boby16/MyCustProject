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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.QEdit;
using Microsoft.DirectX.DirectSound;
using WawaSoft.Media;

namespace Gssy.Capi
{
	// Token: 0x02000008 RID: 8
	public partial class MainWindow : Window
	{
		// Token: 0x0600004A RID: 74 RVA: 0x0000676C File Offset: 0x0000496C
		public MainWindow()
		{
			this.InitializeComponent();
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.method_0();
			this.MyNav.GetJump();
			this.cmbJump.ItemsSource = this.MyNav.NavQJump;
			this.cmbJump.DisplayMemberPath = global::GClass0.smethod_0("Yŉɀ̓њՐنݚࡕ");
			this.cmbJump.SelectedValuePath = global::GClass0.smethod_0("Zňɏ͂љՓمݏࡗॄ");
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤"))
			{
				this.btnRecord.IsEnabled = true;
			}
			else
			{
				this.btnRecord.IsEnabled = false;
			}
			this.DockPanel1.Visibility = Visibility.Collapsed;
			this.btnGoBack.Visibility = Visibility.Hidden;
			this.btnTaptip.Visibility = Visibility.Hidden;
			this.btnCheck.Visibility = Visibility.Hidden;
			this.btnAutoCapture.Visibility = Visibility.Collapsed;
			this.btnCapture.Visibility = Visibility.Collapsed;
			this.btnAutoFill.Visibility = Visibility.Collapsed;
			this.btnFillMode.Visibility = Visibility.Hidden;
			this.btnGo.Visibility = Visibility.Hidden;
			this.btnExit.Visibility = Visibility.Hidden;
			this.cmbJump.Visibility = Visibility.Hidden;
			this.btnRecord.Visibility = Visibility.Hidden;
			this.btnRead.Visibility = Visibility.Hidden;
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				this.btnTaptip.Visibility = Visibility.Hidden;
			}
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤"))
			{
				this.btnRecord.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnRecord.Visibility = Visibility.Hidden;
			}
			if (SurveyMsg.ReadIsOn == global::GClass0.smethod_0("_ũɪͮрջوݨ࡚॰ੱ୷౤"))
			{
				this.btnRead.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnRead.Visibility = Visibility.Hidden;
			}
			int num = SurveyMsg.VersionID.IndexOf('v');
			if (num > -1)
			{
				if (this.oFunc.LEFT(SurveyMsg.VersionID, num) == global::GClass0.smethod_0("浈諗灉"))
				{
					this.btnCapture.Visibility = Visibility.Visible;
				}
				else
				{
					this.btnCapture.Visibility = Visibility.Collapsed;
				}
			}
			if (File.Exists(SurveyHelper.DebugFlagFile) && SurveyHelper.ShowAutoFill == global::GClass0.smethod_0("BŸɠ͹ьչٿݥࡏॡ੫୪ౚ൰๱ཷၤ"))
			{
				this.btnCheck.Visibility = Visibility.Visible;
				this.btnAutoFill.Visibility = Visibility.Visible;
				this.btnFillMode.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				this.DockPanel1.Visibility = Visibility.Visible;
				this.btnGoBack.Visibility = Visibility.Visible;
				this.btnExit.Visibility = Visibility.Visible;
				this.btnTaptip.Visibility = Visibility.Visible;
			}
			else if (SurveyMsg.ShowToolBar == global::GClass0.smethod_0("Cŧɡͺјդ٥ݥࡊ०ੴ୚౰൱๷ཤ"))
			{
				this.DockPanel1.Visibility = Visibility.Visible;
				this.btnGoBack.Visibility = Visibility.Visible;
				this.btnExit.Visibility = Visibility.Visible;
			}
			this.frame1.InputBindings.Add(new InputBinding(ApplicationCommands.NotACommand, new KeyGesture(Key.Back)));
			this.frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseBack, new ExecutedRoutedEventHandler(this.method_5)));
			this.frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.Refresh, new ExecutedRoutedEventHandler(this.method_5)));
			this.frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseForward, new ExecutedRoutedEventHandler(this.method_5)));
			this.frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseForward, new ExecutedRoutedEventHandler(this.method_5)));
			this.frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseHome, new ExecutedRoutedEventHandler(this.method_5)));
			this.frame1.CommandBindings.Add(new CommandBinding(NavigationCommands.BrowseStop, new ExecutedRoutedEventHandler(this.method_5)));
			this.l1stUpdated = true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00006BEC File Offset: 0x00004DEC
		private void method_0()
		{
			this.CurPageId = SurveyHelper.SurveyStart;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.StartPage(this.CurPageId, roadMapVersion);
			string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), this.MyNav.RoadMap.FORM_NAME);
			this.frame1.NavigationService.Navigate(new Uri(uriString));
			if (SurveyHelper.NavLoad == 0)
			{
				SurveyHelper.SurveySequence = 1;
			}
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00006C8C File Offset: 0x00004E8C
		internal void btnGoBack_Click(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			int surveySequence = SurveyHelper.SurveySequence;
			SurveyHelper.AutoFill = false;
			this.btnAutoFill.Style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			if (SurveyHelper.NavCurPage == global::GClass0.smethod_0("Xſɻ;Ѣտٔݱࡦ॰੸"))
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (!SurveyHelper.TestVersion && (SurveyHelper.NavCurPage == global::GClass0.smethod_0("Oŧɬ͔ѳը٩ݢࡰॸ") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Iťɮ͝ѭյ٫ݬࡪॢ੶୤")))
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (this.MySurveyId == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotBack, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.CurPageId == SurveyHelper.SurveyFirstPage)
			{
				MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.oFunc.LEFT(this.CurPageId, 7) == global::GClass0.smethod_0("Tœɗ͒ц՛ٞ"))
			{
				if (!SurveyHelper.TestVersion)
				{
					MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					return;
				}
				MessageBox.Show(SurveyMsg.MsgFirstPageInfo, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
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
				this.oSurveybiz.ClearPageAnswer(this.MySurveyId, surveySequence);
			}
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.PrePage(this.MySurveyId, surveySequence, roadMapVersion);
			SurveyHelper.RoadMapVersion = this.MyNav.Sequence.VERSION_ID.ToString();
			SurveyHelper.CircleACount = this.MyNav.Sequence.CIRCLE_A_COUNT;
			SurveyHelper.CircleACurrent = this.MyNav.Sequence.CIRCLE_A_CURRENT;
			SurveyHelper.CircleBCount = this.MyNav.Sequence.CIRCLE_B_COUNT;
			SurveyHelper.CircleBCurrent = this.MyNav.Sequence.CIRCLE_B_CURRENT;
			try
			{
				string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), this.MyNav.RoadMap.FORM_NAME);
				if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == global::GClass0.smethod_0("@"))
				{
					uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), this.MyNav.RoadMap.FORM_NAME);
				}
				if (this.MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
				{
					this.frame1.NavigationService.Refresh();
				}
				else
				{
					this.frame1.NavigationService.Navigate(new Uri(uriString));
				}
				SurveyHelper.SurveySequence = surveySequence - 1;
				SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
				SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
			}
			catch (Exception)
			{
				string text = string.Format(SurveyMsg.MsgErrorGoBack, new object[]
				{
					this.MySurveyId,
					this.CurPageId,
					this.MyNav.Sequence.VERSION_ID,
					this.MyNav.Sequence.PAGE_ID,
					this.MyNav.RoadMap.FORM_NAME
				});
				MessageBox.Show(text + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.oLogicExplain.OutputResult(text, global::GClass0.smethod_0("Nŭɻͣэխ٥ݳࡢप੏୭౦"), true);
				this.btnExit_Click(sender, e);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00007060 File Offset: 0x00005260
		private void btnGo_Click(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			SurveyHelper.AutoFill = false;
			this.btnAutoFill.Style = style;
			if (SurveyHelper.NavCurPage == global::GClass0.smethod_0("Oŧɬ͔ѳը٩ݢࡰॸ") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Iťɮ͝ѭյ٫ݬࡪॢ੶୤") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Xſɻ;Ѣտٔݱࡦ॰੸"))
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (this.MySurveyId == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNoID, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.CurPageId == SurveyHelper.SurveyFirstPage)
			{
				MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.cmbJump.SelectedValue != null && !((string)this.cmbJump.SelectedValue == global::GClass0.smethod_0("")))
			{
				SurveyHelper.NavOperation = global::GClass0.smethod_0("NŶɯͱ");
				string[] array = this.cmbJump.SelectedValue.ToString().Split(new char[]
				{
					'|'
				});
				SurveyHelper.CircleACount = int.Parse(array[1]);
				SurveyHelper.CircleACurrent = int.Parse(array[2]);
				SurveyHelper.CircleBCount = int.Parse(array[3]);
				SurveyHelper.CircleBCurrent = int.Parse(array[4]);
				int surveySequence = SurveyHelper.SurveySequence;
				string roadMapVersion = SurveyHelper.RoadMapVersion;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleBCount = SurveyHelper.CircleBCount;
				this.MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
				this.MyNav.GoPage(this.MySurveyId, surveySequence, array[0], roadMapVersion);
				try
				{
					string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), this.MyNav.RoadMap.FORM_NAME);
					if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == global::GClass0.smethod_0("@"))
					{
						uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), this.MyNav.RoadMap.FORM_NAME);
					}
					if (this.MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
					{
						this.frame1.NavigationService.Refresh();
					}
					else
					{
						this.frame1.NavigationService.Navigate(new Uri(uriString));
					}
					SurveyHelper.SurveySequence = surveySequence + 1;
					SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
					SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
					SurveyHelper.NavGoBackTimes = 0;
					SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
					SurveyHelper.NavLoad = 0;
				}
				catch (Exception)
				{
					string text = string.Format(SurveyMsg.MsgErrorJump, new object[]
					{
						this.MySurveyId,
						this.CurPageId,
						roadMapVersion,
						array[0],
						this.MyNav.RoadMap.FORM_NAME
					});
					MessageBox.Show(string.Concat(new string[]
					{
						SurveyMsg.MsgWrongJump,
						Environment.NewLine,
						Environment.NewLine,
						text,
						SurveyMsg.MsgErrorEnd
					}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.oLogicExplain.OutputResult(text, global::GClass0.smethod_0("Nŭɻͣэխ٥ݳࡢप੏୭౦"), true);
				}
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNotGoPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000741C File Offset: 0x0000561C
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			int surveySequence = SurveyHelper.SurveySequence;
			if (SurveyHelper.NavCurPage == global::GClass0.smethod_0("Oŧɬ͔ѳը٩ݢࡰॸ") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Iťɮ͝ѭյ٫ݬࡪॢ੶୤") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Xſɻ;Ѣտٔݱࡦ॰੸"))
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (MessageBox.Show(string.Format(SurveyMsg.MsgExitBreak, SurveyHelper.SurveyID), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
			{
				if (this.MySurveyId != global::GClass0.smethod_0("") && this.oSurveybiz.CloseSurveyByExit(this.MySurveyId, 3))
				{
					this.MyNav.CircleACount = SurveyHelper.CircleACount;
					this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
					this.MyNav.CircleBCount = SurveyHelper.CircleBCount;
					this.MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
					this.MyNav.UpdateSurveyMain(this.MySurveyId, surveySequence, this.CurPageId, Convert.ToInt32(SurveyHelper.RoadMapVersion));
				}
				base.Close();
				Application.Current.Shutdown();
				return;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00007570 File Offset: 0x00005770
		private void method_1(object sender, KeyEventArgs e)
		{
			bool debug = SurveyHelper.Debug;
			bool testVersion = SurveyHelper.TestVersion;
			bool flag = false;
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤"))
			{
				flag = true;
			}
			if (e.Key == Key.Back)
			{
				return;
			}
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			if (e.Key == Key.F4 && e.KeyboardDevice.IsKeyDown(Key.LeftAlt))
			{
				e.Handled = true;
				if (debug)
				{
					MessageBox.Show(global::GClass0.smethod_0("Gũɰ̨фԵ"), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				}
				if (!MessageBox.Show(string.Format(SurveyMsg.MsgExitBreak, SurveyHelper.SurveyID), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
				{
					return;
				}
				this.btnExit_Click(sender, e);
			}
			if (e.Key == Key.F1 && e.KeyboardDevice.IsKeyDown(Key.LeftShift))
			{
				e.Handled = true;
				if (!testVersion && (SurveyHelper.NavCurPage == global::GClass0.smethod_0("Oŧɬ͔ѳը٩ݢࡰॸ") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Iťɮ͝ѭյ٫ݬࡪॢ੶୤") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Xſɻ;Ѣտٔݱࡦ॰੸")))
				{
					this.btnGoBack.Visibility = Visibility.Collapsed;
					return;
				}
				if (this.MySurveyId != global::GClass0.smethod_0(""))
				{
					if (this.btnGoBack.Visibility == Visibility.Visible)
					{
						this.btnGoBack.Visibility = Visibility.Hidden;
						if (flag)
						{
							this.btnRecord.Visibility = Visibility.Hidden;
						}
						if (this.btnGo.Visibility == Visibility.Hidden)
						{
							this.DockPanel1.Visibility = Visibility.Collapsed;
						}
					}
					else
					{
						this.DockPanel1.Visibility = Visibility.Visible;
						this.btnGoBack.Visibility = Visibility.Visible;
						if (flag)
						{
							this.btnRecord.Visibility = Visibility.Visible;
						}
					}
				}
			}
			if (e.Key == Key.F12 && e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) && e.KeyboardDevice.IsKeyDown(Key.LeftShift))
			{
				e.Handled = true;
				if (testVersion)
				{
					if (this.btnGo.Visibility == Visibility.Visible)
					{
						this.btnGo.Visibility = Visibility.Hidden;
						this.btnExit.Visibility = Visibility.Hidden;
						this.cmbJump.Visibility = Visibility.Hidden;
						if (this.btnGoBack.Visibility == Visibility.Hidden)
						{
							this.DockPanel1.Visibility = Visibility.Collapsed;
							return;
						}
					}
					else
					{
						this.DockPanel1.Visibility = Visibility.Visible;
						this.btnGo.Visibility = Visibility.Visible;
						this.btnExit.Visibility = Visibility.Visible;
						this.cmbJump.Visibility = Visibility.Visible;
					}
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000077EC File Offset: 0x000059EC
		public bool RecordInit()
		{
			CaptureDevicesCollection captureDevicesCollection = new CaptureDevicesCollection();
			int recordDevice = SurveyHelper.RecordDevice;
			if (captureDevicesCollection.Count > 0)
			{
				if (captureDevicesCollection.Count >= recordDevice)
				{
					this.deviceGuid = captureDevicesCollection[recordDevice].DriverGuid;
				}
				else
				{
					this.deviceGuid = captureDevicesCollection[0].DriverGuid;
					MessageBox.Show(SurveyMsg.MsgErrorDevice, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				}
				return true;
			}
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("XŬɫͨѴաٍݰࡍ९"), global::GClass0.smethod_0("BŪɭ͢Ѿկكݺࡇ३ਖ਼ୣ౥൯๱ཤ"));
			MessageBox.Show(SurveyMsg.MsgNoDevice, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			return false;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000788C File Offset: 0x00005A8C
		public void BeginRecording()
		{
			string currentDirectory = Environment.CurrentDirectory;
			int recordHz = SurveyHelper.RecordHz;
			try
			{
				WaveFormat waveFormat = DirectSoundManager.CreateWaveFormat(recordHz, 16, 1);
				Capture device = new Capture(this.deviceGuid);
				this.record = new CaptureSound(device, waveFormat);
				string text = SurveyHelper.SurveyID + string.Format(global::GClass0.smethod_0("Hŭȥ̮Ѫի٨ܽࡂृਠ୨౯ൕแཀဪᅫቨጩᑰᕱᙼ"), DateTime.Now) + global::GClass0.smethod_0("*Ŵɣͷ");
				this.waveFileName = Path.Combine(currentDirectory + global::GClass0.smethod_0("[ŔɠͧѬհ٥"), text);
				this.record.FileName = this.waveFileName;
				this.record.Start();
				SurveyHelper.RecordStartTime = DateTime.Now;
				this.btnRecord.IsEnabled = false;
				this.btnRecord.Content = SurveyMsg.MsgRecordStart;
				this.btnRecord.Visibility = Visibility.Hidden;
				this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("]ūɮͣѹծـݻࡕॳ੫୪౪൬๦"), global::GClass0.smethod_0("pűɷͤ"));
				this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("]ūɮͣѹծٚݼࡦॴੱ୐౪൯๤"), (DateTime.Now.Hour * 60 + DateTime.Now.Minute).ToString());
				SurveyHelper.RecordIsRunning = true;
				SurveyHelper.RecordFileName = text;
				MessageBox.Show(SurveyMsg.MsgRecordStartInfo, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			catch (Exception ex)
			{
				this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("XŬɫͨѴաٍݰࡍ९"), global::GClass0.smethod_0("BŪɭ͢Ѿկكݺࡇ३ਖ਼ୣ౥൯๱ཤ"));
				Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
				this.btnRecord.Style = style;
				this.btnRecord.Content = SurveyMsg.MsgRecordError;
				MessageBox.Show(SurveyMsg.MsgErrorRecordStart + Environment.NewLine + ex.Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00007A78 File Offset: 0x00005C78
		public void Stop()
		{
			if (SurveyHelper.RecordIsRunning)
			{
				this.record.Stop();
				SurveyHelper.RecordIsRunning = false;
				this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("]ūɮͣѹծـݻࡕॳ੫୪౪൬๦"), global::GClass0.smethod_0("cťɯͱѤ"));
				bool flag = true;
				string text = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("GřȻ͊ѯիٱݷࡧॲ"));
				if (text == null || text == global::GClass0.smethod_0(""))
				{
					text = SurveyHelper.MP3MaxMinutes;
				}
				int num = (int)Convert.ToInt16(text);
				if (num == 0)
				{
					flag = false;
				}
				else if (num < 999)
				{
					int num2 = Convert.ToInt32(this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("]ūɮͣѹծٚݼࡦॴੱ୐౪൯๤")));
					int num3 = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
					if (num < num3 - num2)
					{
						string text2 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("JŖȶ͉Ѭզ٤"));
						if (text2 == null)
						{
							text2 = SurveyMsg.MP3Mode2;
						}
						if (text2 == SurveyMsg.MP3Mode1)
						{
							flag = false;
						}
						else if (MessageBox.Show(string.Format(SurveyMsg.MsgMP3Info, text), SurveyMsg.MsgMP3Caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					string string_ = Path.Combine(Environment.CurrentDirectory + global::GClass0.smethod_0("Xůɫͣ"), global::GClass0.smethod_0("dŦɫ͠Ъզٺݤ"));
					string text3 = this.waveFileName.Replace(global::GClass0.smethod_0("[ŔɠͧѬհ٥"), global::GClass0.smethod_0("XŎɒ̲"));
					text3 = text3.Replace(global::GClass0.smethod_0("*Ŵɣͷ"), global::GClass0.smethod_0("*Ůɲ̲"));
					this.method_2(string_, this.waveFileName, text3);
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00007C1C File Offset: 0x00005E1C
		private void method_2(string string_0, string string_1, string string_2)
		{
			string arguments = string.Concat(new string[]
			{
				global::GClass0.smethod_0("(Œȱ̢У"),
				string_1,
				global::GClass0.smethod_0("!Ģȣ"),
				string_2,
				global::GClass0.smethod_0("#")
			});
			ProcessStartInfo processStartInfo = new ProcessStartInfo(string_0, arguments);
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.WorkingDirectory = Environment.CurrentDirectory + global::GClass0.smethod_0("XŮɲ̲");
			Process process = new Process();
			process.StartInfo = processStartInfo;
			process.StartInfo.UseShellExecute = true;
			process.Start();
			process.WaitForExit();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00007CB4 File Offset: 0x00005EB4
		private void btnRecord_Click(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.NavCurPage == global::GClass0.smethod_0("Xſɻ;Ѣտٔݱࡦ॰੸"))
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (!(SurveyHelper.SurveyID != global::GClass0.smethod_0("")))
			{
				MessageBox.Show(SurveyMsg.MsgRecordNoSurveyId, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤") && this.RecordInit())
			{
				this.BeginRecording();
				return;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000232D File Offset: 0x0000052D
		private void method_3(object sender, EventArgs e)
		{
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤"))
			{
				this.Stop();
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00007D34 File Offset: 0x00005F34
		private void btnCheck_Click(object sender, RoutedEventArgs e)
		{
			if (File.Exists(SurveyHelper.DebugFlagFile))
			{
				new DebugCheck().ShowDialog();
				return;
			}
			MessageBox.Show(global::GClass0.smethod_0("") + Environment.NewLine + SurveyMsg.MsgPrePageAnswer + this.oSurveybiz.GetInfoBySequenceId(SurveyHelper.SurveyID, SurveyHelper.SurveySequence - 1) + Environment.NewLine + SurveyHelper.ShowInfo(), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000234B File Offset: 0x0000054B
		private void btnTaptip_Click(object sender, RoutedEventArgs e)
		{
			SurveyTaptip.ShowInputPanel();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002353 File Offset: 0x00000553
		private void method_4(object sender, RoutedEventArgs e)
		{
			if (this.DockPanel1.Visibility == Visibility.Collapsed)
			{
				this.DockPanel1.Visibility = Visibility.Visible;
				return;
			}
			this.DockPanel1.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000228F File Offset: 0x0000048F
		private void method_5(object sender, ExecutedRoutedEventArgs e)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00007DAC File Offset: 0x00005FAC
		private void frame1_Navigated(object sender, NavigationEventArgs e)
		{
			if (SurveyHelper.SurveyID != global::GClass0.smethod_0("") && SurveyMsg.OutputHistory == global::GClass0.smethod_0("]ŤɤͿѻչلݢࡹॽ੧୵౿൚๰ཱၷᅤ"))
			{
				string text = global::GClass0.smethod_0("");
				if (SurveyHelper.Answer != global::GClass0.smethod_0(""))
				{
					text = global::GClass0.smethod_0("4ĳȲ̱аԯخܭࠬफੋ୧౻൰๣ཷဤᄹሢፚ") + SurveyHelper.Answer + global::GClass0.smethod_0("\\") + Environment.NewLine;
				}
				text = string.Concat(new object[]
				{
					text,
					DateTime.Now.ToString(),
					global::GClass0.smethod_0("=ĦɆͥѰէؼ"),
					SurveyHelper.SurveyID,
					global::GClass0.smethod_0("$ħɕ͠ѵՊنܼ"),
					SurveyHelper.SurveySequence,
					global::GClass0.smethod_0("*ĥɒͦѰԼ"),
					SurveyHelper.RoadMapVersion,
					global::GClass0.smethod_0(")Ĥɒͬм"),
					SurveyHelper.NavCurPage,
					global::GClass0.smethod_0("'Īɏͧѵիًݥ࡮१਼"),
					SurveyHelper.CurPageName
				});
				this.oLogicExplain.OutputResult(text, global::GClass0.smethod_0("@Ůɵͱѫձٻݞ") + SurveyHelper.SurveyID + global::GClass0.smethod_0("*ŏɭͦ"), true);
				SurveyHelper.Answer = global::GClass0.smethod_0("");
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00007EFC File Offset: 0x000060FC
		private void btnAutoFill_Click(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			if (SurveyHelper.NavCurPage == global::GClass0.smethod_0("Oŧɬ͔ѳը٩ݢࡰॸ") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Iťɮ͝ѭյ٫ݬࡪॢ੶୤") || SurveyHelper.NavCurPage == global::GClass0.smethod_0("Xſɻ;Ѣտٔݱࡦ॰੸"))
			{
				MessageBox.Show(SurveyMsg.MsgNotDoIt, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.MySurveyId == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotDoIt, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (SurveyHelper.AutoFill)
			{
				SurveyHelper.AutoFill = false;
				this.btnAutoFill.Style = style2;
				SurveyHelper.AutoDo = false;
				return;
			}
			SurveyHelper.AutoFill = true;
			this.btnAutoFill.Style = style;
			if (SurveyHelper.AutoDo_listCity.Count >= SurveyHelper.AutoDo_CityOrder && SurveyHelper.AutoDo_Total > SurveyHelper.AutoDo_Count)
			{
				SurveyHelper.AutoDo = true;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000020F6 File Offset: 0x000002F6
		private void btnFillMode_Click(object sender, RoutedEventArgs e)
		{
			new FillMode().ShowDialog();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00008020 File Offset: 0x00006220
		private void btnAutoCapture_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			if (SurveyHelper.AutoCapture)
			{
				SurveyHelper.AutoCapture = false;
				this.btnAutoCapture.Style = style2;
				return;
			}
			SurveyHelper.AutoCapture = true;
			this.btnAutoCapture.Style = style;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00008088 File Offset: 0x00006288
		private void btnCapture_Click(object sender, RoutedEventArgs e)
		{
			string text = string.Concat(new string[]
			{
				SurveyHelper.SurveyID,
				global::GClass0.smethod_0("^"),
				SurveyHelper.NavCurPage,
				(SurveyHelper.CircleACode == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("") : (global::GClass0.smethod_0("]ŀ") + SurveyHelper.CircleACode),
				(SurveyHelper.CircleBCode == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("") : (global::GClass0.smethod_0("]Ń") + SurveyHelper.CircleBCode),
				global::GClass0.smethod_0("*ũɲͦ")
			});
			text = Directory.GetCurrentDirectory() + global::GClass0.smethod_0("[Ŗɭͫѷխٝ") + text;
			if (File.Exists(text))
			{
				text = string.Concat(new object[]
				{
					SurveyHelper.SurveyID,
					global::GClass0.smethod_0("^"),
					SurveyHelper.NavCurPage,
					(SurveyHelper.CircleACode == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("") : (global::GClass0.smethod_0("]ŀ") + SurveyHelper.CircleACode),
					(SurveyHelper.CircleBCode == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("") : (global::GClass0.smethod_0("]Ń") + SurveyHelper.CircleBCode),
					global::GClass0.smethod_0("^"),
					DateTime.Now.Hour,
					DateTime.Now.Minute,
					DateTime.Now.Second,
					global::GClass0.smethod_0("*ũɲͦ")
				});
				text = Directory.GetCurrentDirectory() + global::GClass0.smethod_0("[Ŗɭͫѷխٝ") + text;
			}
			int int_ = (int)SurveyHelper.Screen_LeftTop;
			if (new ScreenCapture().Capture(text, int_))
			{
				if (!SurveyHelper.AutoFill)
				{
					MessageBox.Show(SurveyMsg.MsgScreenCaptureDone + Environment.NewLine + text);
					return;
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgScreenCaptureFail + Environment.NewLine + text);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000237C File Offset: 0x0000057C
		private void method_6(object sender, EventArgs e)
		{
			if (this.l1stUpdated)
			{
				SurveyHelper.Screen_LeftTop = this.DockPanel1.ActualHeight;
				this.l1stUpdated = false;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000082B4 File Offset: 0x000064B4
		private void btnRead_Click(object sender, RoutedEventArgs e)
		{
			string text = this.ReadMp3Path + SurveyHelper.NavCurPage + global::GClass0.smethod_0("*Ůɲ̲");
			if (File.Exists(text))
			{
				this.mediaElement.Source = new Uri(text, UriKind.Relative);
				this.mediaElement.Play();
				return;
			}
			MessageBox.Show(SurveyMsg.MsgPageNoMP3, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x04000053 RID: 83
		private string MySurveyId = global::GClass0.smethod_0("");

		// Token: 0x04000054 RID: 84
		private string CurPageId = global::GClass0.smethod_0("");

		// Token: 0x04000055 RID: 85
		private NavBase MyNav = new NavBase();

		// Token: 0x04000056 RID: 86
		public SurveyBiz oSurveybiz = new SurveyBiz();

		// Token: 0x04000057 RID: 87
		private LogicExplain oLogicExplain = new LogicExplain();

		// Token: 0x04000058 RID: 88
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x04000059 RID: 89
		private bool l1stUpdated;

		// Token: 0x0400005A RID: 90
		private UDPX oFunc = new UDPX();

		// Token: 0x0400005B RID: 91
		private string ReadMp3Path = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ");

		// Token: 0x0400005C RID: 92
		private CaptureSound record;

		// Token: 0x0400005D RID: 93
		private Guid deviceGuid = Guid.Empty;

		// Token: 0x0400005E RID: 94
		private string waveFileName = global::GClass0.smethod_0("");
	}
}
