using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class PageMedia : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private UDPX oFunc = new UDPX();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private QDisplay oQuestion = new QDisplay();

		private string strFullName = _003F487_003F._003F488_003F("");

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		private DispatcherTimer timerMedia = new DispatcherTimer();

		internal TextBlock txtQuestionTitle;

		internal Border mediaBorder;

		internal MediaElement mediaElement;

		internal RotateTransform mediaAngle;

		internal Slider volumeSlider;

		internal Slider ProgressSlider;

		internal Button openBtn;

		internal Button StartBtn;

		internal Button playBtn;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public PageMedia()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				MyNav.GroupPageType = oQuestion.QDefine.GROUP_PAGE_TYPE;
				MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					MyNav.GroupCodeB = oQuestion.QDefine.GROUP_CODEB;
					MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
					MyNav.CircleBCount = SurveyHelper.CircleBCount;
				}
				MyNav.GetCircleInfo(MySurveyId);
				oQuestion.QuestionName += MyNav.QName_Add;
				List<VEAnswer> list = new List<VEAnswer>();
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = MyNav.GroupCodeA;
				vEAnswer.CODE = MyNav.CircleACode;
				vEAnswer.CODE_TEXT = MyNav.CircleCodeTextA;
				list.Add(vEAnswer);
				SurveyHelper.CircleACode = MyNav.CircleACode;
				SurveyHelper.CircleACodeText = MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
				SurveyHelper.CircleACount = MyNav.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					VEAnswer vEAnswer2 = new VEAnswer();
					vEAnswer2.QUESTION_NAME = MyNav.GroupCodeB;
					vEAnswer2.CODE = MyNav.CircleBCode;
					vEAnswer2.CODE_TEXT = MyNav.CircleCodeTextB;
					list.Add(vEAnswer2);
					SurveyHelper.CircleBCode = MyNav.CircleBCode;
					SurveyHelper.CircleBCodeText = MyNav.CircleCodeTextB;
					SurveyHelper.CircleBCurrent = MyNav.CircleBCurrent;
					SurveyHelper.CircleBCount = MyNav.CircleBCount;
				}
			}
			else
			{
				SurveyHelper.CircleACode = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleACodeText = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleACurrent = 0;
				SurveyHelper.CircleACount = 0;
				SurveyHelper.CircleBCode = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleBCodeText = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleBCurrent = 0;
				SurveyHelper.CircleBCount = 0;
				MyNav.GroupCodeA = _003F487_003F._003F488_003F("");
				MyNav.CircleACurrent = 0;
				MyNav.CircleACount = 0;
				MyNav.GroupCodeB = _003F487_003F._003F488_003F("");
				MyNav.CircleBCurrent = 0;
				MyNav.CircleBCount = 0;
			}
			oLogicEngine.SurveyID = MySurveyId;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			if (SurveyHelper.AutoFill && new AutoFill().AutoNext(oQuestion.QDefine))
			{
				_003F58_003F(this, _003F348_003F);
			}
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			txtQuestionTitle.Text = _003F487_003F._003F488_003F("");
			BoldTitle boldTitle = new BoldTitle();
			boldTitle.SpanTitle(MySurveyId, qUESTION_TITLE, SurveyHelper.CircleACode, SurveyHelper.CircleBCode);
			foreach (classHtmlText item in boldTitle.lSpan)
			{
				if (item.TitleTextType == _003F487_003F._003F488_003F("?ŀȿ"))
				{
					Span span = new Span();
					span.Inlines.Add(new Run(item.TitleText));
					span.Foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
					span.FontWeight = FontWeights.Bold;
					txtQuestionTitle.Inlines.Add(span);
				}
				else
				{
					Span span2 = new Span();
					span2.Inlines.Add(new Run(item.TitleText));
					txtQuestionTitle.Inlines.Add(span2);
				}
			}
			if (oQuestion.QDefine.TITLE_FONTSIZE != 0)
			{
				txtQuestionTitle.FontSize = (double)oQuestion.QDefine.TITLE_FONTSIZE;
			}
			if (oQuestion.QDefine.TITLE_FONTSIZE != 0)
			{
				txtQuestionTitle.FontSize = (double)oQuestion.QDefine.TITLE_FONTSIZE;
			}
			if (oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				mediaAngle.Angle = (double)oQuestion.QDefine.CONTROL_TYPE;
			}
			if (oQuestion.QDefine.CONTROL_TYPE == 90 || oQuestion.QDefine.CONTROL_TYPE == 270)
			{
				if (oQuestion.QDefine.CONTROL_HEIGHT > 0)
				{
					mediaElement.Height = (double)oQuestion.QDefine.CONTROL_HEIGHT;
					mediaElement.Width = (double)oQuestion.QDefine.CONTROL_HEIGHT;
				}
				else if (oQuestion.QDefine.CONTROL_WIDTH > 0)
				{
					mediaElement.Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
					mediaElement.Height = (double)oQuestion.QDefine.CONTROL_WIDTH;
				}
			}
			else
			{
				if (oQuestion.QDefine.CONTROL_WIDTH > 0)
				{
					mediaElement.Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
				}
				if (oQuestion.QDefine.CONTROL_HEIGHT > 0)
				{
					mediaElement.Height = (double)oQuestion.QDefine.CONTROL_HEIGHT;
				}
			}
			if (oQuestion.QDefine.CONTROL_HEIGHT == 0 && oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				mediaElement.Height = mediaBorder.ActualHeight;
			}
			string str = Environment.CurrentDirectory + _003F487_003F._003F488_003F("Zňɡ\u0367ѫ\u0560");
			string text = oQuestion.QDefine.CONTROL_TOOLTIP;
			if (oFunc.LEFT(text, 2) == _003F487_003F._003F488_003F("$ź") || text.IndexOf(_003F487_003F._003F488_003F("Yģ")) > -1)
			{
				text = oLogicEngine.strShowText(text, true);
			}
			strFullName = str + _003F487_003F._003F488_003F("]") + text;
			if (File.Exists(strFullName))
			{
				mediaElement.Source = new Uri(strFullName, UriKind.Relative);
				playBtn.IsEnabled = true;
				StartBtn.IsEnabled = true;
				playBtn.Content = SurveyMsg.MsgPause;
				mediaElement.ToolTip = SurveyMsg.MsgPauseTip;
				mediaElement.Play();
				openBtn.Visibility = Visibility.Collapsed;
			}
			else if (!SurveyHelper.AutoFill)
			{
				MessageBox.Show(SurveyMsg.MsgNotMedia, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				btnNav.Foreground = Brushes.Gray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			oPageNav.oLogicEngine = oLogicEngine;
			if (!oPageNav.CheckLogic(CurPageId))
			{
				btnNav.Content = btnNav_Content;
				return;
			}
			goto IL_0083;
			IL_0063:
			goto IL_0020;
			IL_0083:
			oPageNav.NextPage(MyNav, base.NavigationService);
			btnNav.Content = btnNav_Content;
			return;
			IL_0058:
			goto IL_0083;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				btnNav.Foreground = Brushes.Black;
				btnNav.Content = btnNav_Content;
			}
			else
			{
				SecondsCountDown--;
				btnNav.Content = SecondsCountDown.ToString();
			}
		}

		private void _003F171_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Expected O, but got Unknown
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Invalid comparison between Unknown and I4
			//IL_00e4: Incompatible stack heights: 0 vs 2
			ShellContainer val = null;
			val = (KnownFolders.get_SampleVideos() as ShellContainer);
			CommonOpenFileDialog val2 = new CommonOpenFileDialog();
			val2.set_InitialDirectoryShellContainer(val);
			val2.set_EnsureReadOnly(true);
			((Collection<CommonFileDialogFilter>)val2.get_Filters()).Add(new CommonFileDialogFilter(_003F487_003F._003F488_003F("^Ņɑ\u0326уխٯݧ\u0872"), _003F487_003F._003F488_003F("/Īɴ\u036fѷ")));
			((Collection<CommonFileDialogFilter>)val2.get_Filters()).Add(new CommonFileDialogFilter(_003F487_003F._003F488_003F("HŞɎ\u0326уխٯݧ\u0872"), _003F487_003F._003F488_003F("/Īɢ\u0374Ѩ")));
			((Collection<CommonFileDialogFilter>)val2.get_Filters()).Add(new CommonFileDialogFilter(_003F487_003F._003F488_003F("DŘȴ\u0326уխٯݧ\u0872"), _003F487_003F._003F488_003F("/ĪɮͲв")));
			((Collection<CommonFileDialogFilter>)val2.get_Filters()).Add(new CommonFileDialogFilter(_003F487_003F._003F488_003F("^ŉɑ\u0326уխٯݧ\u0872"), _003F487_003F._003F488_003F("/Īɴ\u0363ѷ")));
			if ((int)val2.ShowDialog() == 1)
			{
				val2.get_FileName();
				((PageMedia)/*Error near IL_00c2: Stack underflow*/).strFullName = (string)/*Error near IL_00c2: Stack underflow*/;
				mediaElement.Source = new Uri(strFullName, UriKind.Relative);
				playBtn.IsEnabled = true;
				StartBtn.IsEnabled = true;
			}
		}

		private void _003F172_003F()
		{
			if (playBtn.Content.ToString() == SurveyMsg.MsgContinue)
			{
				mediaElement.Play();
				playBtn.Content = SurveyMsg.MsgPause;
				mediaElement.ToolTip = SurveyMsg.MsgPauseTip;
			}
			else
			{
				mediaElement.Pause();
				playBtn.Content = SurveyMsg.MsgContinue;
				mediaElement.ToolTip = SurveyMsg.MsgPlayTip;
			}
		}

		private void _003F173_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			_003F172_003F();
		}

		private void _003F174_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			mediaElement.Source = new Uri(strFullName, UriKind.Relative);
			mediaElement.Play();
			playBtn.Content = SurveyMsg.MsgPause;
			mediaElement.ToolTip = SurveyMsg.MsgPauseTip;
		}

		private void _003F175_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			_003F172_003F();
		}

		private void _003F176_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			double totalSeconds = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
			ProgressSlider.Maximum = totalSeconds;
			ProgressSlider.Value = 0.0;
			double num = totalSeconds / ProgressSlider.Maximum;
			timerMedia.Interval = new TimeSpan(0, 0, 1);
			timerMedia.Tick += _003F177_003F;
			timerMedia.Start();
		}

		private void _003F177_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			ProgressSlider.ValueChanged -= _003F178_003F;
			ProgressSlider.Value = mediaElement.Position.TotalSeconds;
			ProgressSlider.ValueChanged += _003F178_003F;
		}

		private void _003F178_003F(object _003F347_003F, RoutedPropertyChangedEventArgs<double> _003F348_003F)
		{
		}

		private void _003F85_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyHelper.AttachSurveyId = MySurveyId;
			SurveyHelper.AttachQName = oQuestion.QuestionName;
			SurveyHelper.AttachPageId = CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_001b;
			IL_001b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\aŠɕ\u0356ѝԍ١\u0740ࡐॶਥ\u0b7e\u0c73\u0d76\u0e6a\u0f76ၶᅲቸ፡ᐻᕥᙻ\u1774ᡧ\u1920\u1a7e\u1b6cᱫᵮṧὬ\u206cⅮ≧⌫⑼╢♯❭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0016:
			goto IL_001b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((PageMedia)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				mediaBorder = (Border)_003F350_003F;
				break;
			case 4:
				mediaElement = (MediaElement)_003F350_003F;
				mediaElement.MediaOpened += _003F176_003F;
				mediaElement.MouseLeftButtonUp += _003F175_003F;
				break;
			case 5:
				mediaAngle = (RotateTransform)_003F350_003F;
				break;
			case 6:
				volumeSlider = (Slider)_003F350_003F;
				break;
			case 7:
				ProgressSlider = (Slider)_003F350_003F;
				break;
			case 8:
				openBtn = (Button)_003F350_003F;
				openBtn.Click += _003F171_003F;
				break;
			case 9:
				StartBtn = (Button)_003F350_003F;
				StartBtn.Click += _003F174_003F;
				break;
			case 10:
				playBtn = (Button)_003F350_003F;
				playBtn.Click += _003F173_003F;
				break;
			case 11:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 12:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 13:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
