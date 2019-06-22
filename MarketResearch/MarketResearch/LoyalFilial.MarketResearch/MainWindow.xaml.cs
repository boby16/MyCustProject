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
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.QEdit;
using Microsoft.DirectX.DirectSound;
using WawaSoft.Media;

namespace LoyalFilial.MarketResearch
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
			base.Topmost = true;
			base.Hide();
			base.Show();
			this.method_0();
			this.MyNav.GetJump();
			this.cmbJump.ItemsSource = this.MyNav.NavQJump;
			this.cmbJump.DisplayMemberPath = "PAGE_TEXT";
			this.cmbJump.SelectedValuePath = "PAGE_VALUE";
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true")
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
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				this.btnTaptip.Visibility = Visibility.Hidden;
			}
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true")
			{
				this.btnRecord.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnRecord.Visibility = Visibility.Hidden;
			}
			if (SurveyMsg.ReadIsOn == "ReadIsOn_true")
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
				if (this.oFunc.LEFT(SurveyMsg.VersionID, num) == "测试版")
				{
					this.btnCapture.Visibility = Visibility.Visible;
				}
				else
				{
					this.btnCapture.Visibility = Visibility.Collapsed;
				}
			}
			if (File.Exists(SurveyHelper.DebugFlagFile) && SurveyHelper.ShowAutoFill == "ShowAutoFill_true")
			{
				this.btnCheck.Visibility = Visibility.Visible;
				this.btnAutoFill.Visibility = Visibility.Visible;
				this.btnFillMode.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				this.DockPanel1.Visibility = Visibility.Visible;
				this.btnGoBack.Visibility = Visibility.Visible;
				this.btnExit.Visibility = Visibility.Visible;
				this.btnTaptip.Visibility = Visibility.Visible;
			}
			else if (SurveyMsg.ShowToolBar == "ShowToolBar_true")
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

		private void method_0()
		{
			this.CurPageId = SurveyHelper.SurveyStart;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.StartPage(this.CurPageId, roadMapVersion);
			string uriString = string.Format("pack://application:,,,/View/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
			this.frame1.NavigationService.Navigate(new Uri(uriString));
			if (SurveyHelper.NavLoad == 0)
			{
				SurveyHelper.SurveySequence = 1;
			}
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
		}

		internal void btnGoBack_Click(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			int surveySequence = SurveyHelper.SurveySequence;
			SurveyHelper.AutoFill = false;
			this.btnAutoFill.Style = (Style)base.FindResource("UnSelBtnStyle");
			if (SurveyHelper.NavCurPage == "SurveyQuery")
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (!SurveyHelper.TestVersion && (SurveyHelper.NavCurPage == "EndSummary" || SurveyHelper.NavCurPage == "EndTerminate"))
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (this.MySurveyId == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotBack, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.CurPageId == SurveyHelper.SurveyFirstPage)
			{
				MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.oFunc.LEFT(this.CurPageId, 7) == "SURVEY_")
			{
				if (!SurveyHelper.TestVersion)
				{
					MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					return;
				}
				MessageBox.Show(SurveyMsg.MsgFirstPageInfo, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			SurveyHelper.NavOperation = "Back";
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
				string uriString = string.Format("pack://application:,,,/View/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
				if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == "A")
				{
					uriString = string.Format("pack://application:,,,/ViewProject/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
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
				this.oLogicExplain.OutputResult(text, "MarketResearchDebug.Log", true);
				this.btnExit_Click(sender, e);
			}
		}

		private void btnGo_Click(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			Style style = (Style)base.FindResource("UnSelBtnStyle");
			SurveyHelper.AutoFill = false;
			this.btnAutoFill.Style = style;
			if (SurveyHelper.NavCurPage == "EndSummary" || SurveyHelper.NavCurPage == "EndTerminate" || SurveyHelper.NavCurPage == "SurveyQuery")
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (this.MySurveyId == "")
			{
				MessageBox.Show(SurveyMsg.MsgNoID, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.CurPageId == SurveyHelper.SurveyFirstPage)
			{
				MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.cmbJump.SelectedValue != null && !((string)this.cmbJump.SelectedValue == ""))
			{
				SurveyHelper.NavOperation = "Jump";
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
					string uriString = string.Format("pack://application:,,,/View/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
					if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == "A")
					{
						uriString = string.Format("pack://application:,,,/ViewProject/{0}.xaml", this.MyNav.RoadMap.FORM_NAME);
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
					SurveyHelper.NavOperation = "Normal";
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
					this.oLogicExplain.OutputResult(text, "MarketResearchDebug.Log", true);
				}
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNotGoPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			int surveySequence = SurveyHelper.SurveySequence;
			if (SurveyHelper.NavCurPage == "EndSummary" || SurveyHelper.NavCurPage == "EndTerminate" || SurveyHelper.NavCurPage == "SurveyQuery")
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (MessageBox.Show(string.Format(SurveyMsg.MsgExitBreak, SurveyHelper.SurveyID), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
			{
				if (this.MySurveyId != "" && this.oSurveybiz.CloseSurveyByExit(this.MySurveyId, 3))
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

		private void method_1(object sender, KeyEventArgs e)
		{
			bool debug = SurveyHelper.Debug;
			bool testVersion = SurveyHelper.TestVersion;
			bool flag = false;
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true")
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
					MessageBox.Show("Alt+F4", SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
				if (!testVersion && (SurveyHelper.NavCurPage == "EndSummary" || SurveyHelper.NavCurPage == "EndTerminate" || SurveyHelper.NavCurPage == "SurveyQuery"))
				{
					this.btnGoBack.Visibility = Visibility.Collapsed;
					return;
				}
				if (this.MySurveyId != "")
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
			this.oSurveyConfigBiz.Save("RecordIsOn", "RecordIsOn_false");
			MessageBox.Show(SurveyMsg.MsgNoDevice, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			return false;
		}

		public void BeginRecording()
		{
			string currentDirectory = Environment.CurrentDirectory;
			int recordHz = SurveyHelper.RecordHz;
			try
			{
				WaveFormat waveFormat = DirectSoundManager.CreateWaveFormat(recordHz, 16, 1);
				Capture device = new Capture(this.deviceGuid);
				this.record = new CaptureSound(device, waveFormat);
				string text = SurveyHelper.SurveyID + string.Format("_{0:yyy-MM-dd_HH-mm-ss}", DateTime.Now) + ".wav";
				this.waveFileName = Path.Combine(currentDirectory + "\\Record", text);
				this.record.FileName = this.waveFileName;
				this.record.Start();
				SurveyHelper.RecordStartTime = DateTime.Now;
				this.btnRecord.IsEnabled = false;
				this.btnRecord.Content = SurveyMsg.MsgRecordStart;
				this.btnRecord.Visibility = Visibility.Hidden;
				this.oSurveyConfigBiz.Save("RecordIsRunning", "true");
				this.oSurveyConfigBiz.Save("RecordStartTime", (DateTime.Now.Hour * 60 + DateTime.Now.Minute).ToString());
				SurveyHelper.RecordIsRunning = true;
				SurveyHelper.RecordFileName = text;
				MessageBox.Show(SurveyMsg.MsgRecordStartInfo, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			catch (Exception ex)
			{
				this.oSurveyConfigBiz.Save("RecordIsOn", "RecordIsOn_false");
				Style style = (Style)base.FindResource("SelBtnStyle");
				this.btnRecord.Style = style;
				this.btnRecord.Content = SurveyMsg.MsgRecordError;
				MessageBox.Show(SurveyMsg.MsgErrorRecordStart + Environment.NewLine + ex.Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		public void Stop()
		{
			if (SurveyHelper.RecordIsRunning)
			{
				this.record.Stop();
				SurveyHelper.RecordIsRunning = false;
				this.oSurveyConfigBiz.Save("RecordIsRunning", "false");
				bool flag = true;
				string text = this.oSurveyConfigBiz.GetByCodeText("MP3Minutes");
				if (text == null || text == "")
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
					int num2 = Convert.ToInt32(this.oSurveyConfigBiz.GetByCodeText("RecordStartTime"));
					int num3 = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
					if (num < num3 - num2)
					{
						string text2 = this.oSurveyConfigBiz.GetByCodeText("MP3Mode");
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
					string string_ = Path.Combine(Environment.CurrentDirectory + "\\Library", "lame.exe");
					string text3 = this.waveFileName.Replace("\\Record", "\\MP3");
					text3 = text3.Replace(".wav", ".mp3");
					this.method_2(string_, this.waveFileName, text3);
				}
			}
		}

		private void method_2(string string_0, string string_1, string string_2)
		{
			string arguments = string.Concat(new string[]
			{
				"-V2 \"",
				string_1,
				"\" \"",
				string_2,
				"\""
			});
			ProcessStartInfo processStartInfo = new ProcessStartInfo(string_0, arguments);
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.WorkingDirectory = Environment.CurrentDirectory + "\\mp3";
			Process process = new Process();
			process.StartInfo = processStartInfo;
			process.StartInfo.UseShellExecute = true;
			process.Start();
			process.WaitForExit();
		}

		private void btnRecord_Click(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.NavCurPage == "SurveyQuery")
			{
				this.DockPanel1.Visibility = Visibility.Collapsed;
				return;
			}
			if (!(SurveyHelper.SurveyID != ""))
			{
				MessageBox.Show(SurveyMsg.MsgRecordNoSurveyId, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true" && this.RecordInit())
			{
				this.BeginRecording();
				return;
			}
		}

		private void method_3(object sender, EventArgs e)
		{
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true")
			{
				this.Stop();
			}
		}

		private void btnCheck_Click(object sender, RoutedEventArgs e)
		{
			if (File.Exists(SurveyHelper.DebugFlagFile))
			{
				new DebugCheck().ShowDialog();
				return;
			}
			MessageBox.Show("" + Environment.NewLine + SurveyMsg.MsgPrePageAnswer + this.oSurveybiz.GetInfoBySequenceId(SurveyHelper.SurveyID, SurveyHelper.SurveySequence - 1) + Environment.NewLine + SurveyHelper.ShowInfo(), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void btnTaptip_Click(object sender, RoutedEventArgs e)
		{
			SurveyTaptip.ShowInputPanel();
		}

		private void method_4(object sender, RoutedEventArgs e)
		{
			if (this.DockPanel1.Visibility == Visibility.Collapsed)
			{
				this.DockPanel1.Visibility = Visibility.Visible;
				return;
			}
			this.DockPanel1.Visibility = Visibility.Collapsed;
		}

		private void method_5(object sender, ExecutedRoutedEventArgs e)
		{
		}

		private void frame1_Navigated(object sender, NavigationEventArgs e)
		{
			if (SurveyHelper.SurveyID != "" && SurveyMsg.OutputHistory == "OutputHistory_true")
			{
				string text = "";
				if (SurveyHelper.Answer != "")
				{
					text = "          Answer : [" + SurveyHelper.Answer + "]" + Environment.NewLine;
				}
				text = string.Concat(new object[]
				{
					text,
					DateTime.Now.ToString(),
					": Case=",
					SurveyHelper.SurveyID,
					", SeqID=",
					SurveyHelper.SurveySequence,
					", Ver=",
					SurveyHelper.RoadMapVersion,
					", Qn=",
					SurveyHelper.NavCurPage,
					", FormName=",
					SurveyHelper.CurPageName
				});
				this.oLogicExplain.OutputResult(text, "History_" + SurveyHelper.SurveyID + ".Log", true);
				SurveyHelper.Answer = "";
			}
		}

		private void btnAutoFill_Click(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			if (SurveyHelper.NavCurPage == "EndSummary" || SurveyHelper.NavCurPage == "EndTerminate" || SurveyHelper.NavCurPage == "SurveyQuery")
			{
				MessageBox.Show(SurveyMsg.MsgNotDoIt, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if (this.MySurveyId == "")
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

		private void btnFillMode_Click(object sender, RoutedEventArgs e)
		{
			new FillMode().ShowDialog();
		}

		private void btnAutoCapture_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			if (SurveyHelper.AutoCapture)
			{
				SurveyHelper.AutoCapture = false;
				this.btnAutoCapture.Style = style2;
				return;
			}
			SurveyHelper.AutoCapture = true;
			this.btnAutoCapture.Style = style;
		}

		private void btnCapture_Click(object sender, RoutedEventArgs e)
		{
			string text = string.Concat(new string[]
			{
				SurveyHelper.SurveyID,
				"_",
				SurveyHelper.NavCurPage,
				(SurveyHelper.CircleACode == "") ? "" : ("_A" + SurveyHelper.CircleACode),
				(SurveyHelper.CircleBCode == "") ? "" : ("_B" + SurveyHelper.CircleBCode),
				".jpg"
			});
			text = Directory.GetCurrentDirectory() + "\\Photo\\" + text;
			if (File.Exists(text))
			{
				text = string.Concat(new object[]
				{
					SurveyHelper.SurveyID,
					"_",
					SurveyHelper.NavCurPage,
					(SurveyHelper.CircleACode == "") ? "" : ("_A" + SurveyHelper.CircleACode),
					(SurveyHelper.CircleBCode == "") ? "" : ("_B" + SurveyHelper.CircleBCode),
					"_",
					DateTime.Now.Hour,
					DateTime.Now.Minute,
					DateTime.Now.Second,
					".jpg"
				});
				text = Directory.GetCurrentDirectory() + "\\Photo\\" + text;
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

		private void method_6(object sender, EventArgs e)
		{
			if (this.l1stUpdated)
			{
				SurveyHelper.Screen_LeftTop = this.DockPanel1.ActualHeight;
				this.l1stUpdated = false;
			}
		}

		private void btnRead_Click(object sender, RoutedEventArgs e)
		{
			string text = this.ReadMp3Path + SurveyHelper.NavCurPage + ".mp3";
			if (File.Exists(text))
			{
				this.mediaElement.Source = new Uri(text, UriKind.Relative);
				this.mediaElement.Play();
				return;
			}
			MessageBox.Show(SurveyMsg.MsgPageNoMP3, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private string MySurveyId = "";

		private string CurPageId = "";

		private NavBase MyNav = new NavBase();

		public SurveyBiz oSurveybiz = new SurveyBiz();

		private LogicExplain oLogicExplain = new LogicExplain();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private bool l1stUpdated;

		private UDPX oFunc = new UDPX();

		private string ReadMp3Path = Environment.CurrentDirectory + "\\Media\\";

		private CaptureSound record;

		private Guid deviceGuid = Guid.Empty;

		private string waveFileName = "";
	}
}
