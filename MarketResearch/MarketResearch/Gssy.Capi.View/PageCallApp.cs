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
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class PageCallApp : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QDisplay oQuestion = new QDisplay();

		private int AppRunTimes;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		private string btnCall_Content = SurveyMsg.MsgCallApp_BtnCall;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtDisplayContext;

		internal Button btnCallApp;

		internal TextBlock txtProgram;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public PageCallApp()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_055a: Incompatible stack heights: 0 vs 1
			//IL_0570: Incompatible stack heights: 0 vs 2
			//IL_0584: Incompatible stack heights: 0 vs 2
			//IL_05c0: Incompatible stack heights: 0 vs 2
			//IL_05cc: Incompatible stack heights: 0 vs 2
			//IL_0600: Incompatible stack heights: 0 vs 1
			//IL_061b: Incompatible stack heights: 0 vs 2
			//IL_0642: Incompatible stack heights: 0 vs 2
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnCall_Content;
			oQuestion.Init(CurPageId, 0);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				NavBase myNav = MyNav;
				int gROUP_PAGE_TYPE = oQuestion.QDefine.GROUP_PAGE_TYPE;
				((NavBase)/*Error near IL_00a3: Stack underflow*/).GroupPageType = gROUP_PAGE_TYPE;
				MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					NavBase myNav2 = MyNav;
					QDisplay oQuestion2 = oQuestion;
					string gROUP_CODEB = ((QDisplay)/*Error near IL_0102: Stack underflow*/).QDefine.GROUP_CODEB;
					((NavBase)/*Error near IL_010c: Stack underflow*/).GroupCodeB = gROUP_CODEB;
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
					string groupCodeB = ((PageCallApp)/*Error near IL_0214: Stack underflow*/).MyNav.GroupCodeB;
					((VEAnswer)/*Error near IL_021e: Stack underflow*/).QUESTION_NAME = groupCodeB;
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
			if (SurveyHelper.AutoFill)
			{
				AppRunTimes = 1;
				Button btnNav2 = btnNav;
				string btnNav_Content2 = btnNav_Content;
				((ContentControl)/*Error near IL_03ed: Stack underflow*/).Content = (object)/*Error near IL_03ed: Stack underflow*/;
				if (new AutoFill().AutoNext(oQuestion.QDefine))
				{
					((PageCallApp)/*Error near IL_040d: Stack underflow*/)._003F58_003F((object)/*Error near IL_040d: Stack underflow*/, _003F348_003F);
				}
			}
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			qUESTION_TITLE = oQuestion.QDefine.QUESTION_CONTENT;
			oBoldTitle.SetTextBlock(txtDisplayContext, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			string text = oQuestion.QDefine.CONTROL_TOOLTIP;
			string nOTE = oQuestion.QDefine.NOTE;
			if (!(text == _003F487_003F._003F488_003F("")))
			{
				if (text.IndexOf(_003F487_003F._003F488_003F(";")) == -1)
				{
					string text3 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("]") + text;
					text = (string)/*Error near IL_04cd: Stack underflow*/;
				}
			}
			else
			{
				text = Environment.CurrentDirectory;
			}
			string text2 = text + _003F487_003F._003F488_003F("]") + nOTE;
			txtProgram.Text = text2;
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ"))
			{
				int iS_ATTACH = oQuestion.QDefine.IS_ATTACH;
				if (/*Error near IL_0620: Stack underflow*/ == /*Error near IL_0620: Stack underflow*/)
				{
					btnAttach.Visibility = Visibility.Visible;
				}
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				int secondsWait = SecondsWait;
				((PageCallApp)/*Error near IL_0545: Stack underflow*/).SecondsCountDown = (int)/*Error near IL_0545: Stack underflow*/;
				btnNav.IsEnabled = false;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_011f: Incompatible stack heights: 0 vs 1
			//IL_013e: Incompatible stack heights: 0 vs 2
			//IL_0153: Incompatible stack heights: 0 vs 1
			//IL_016e: Incompatible stack heights: 0 vs 3
			if ((string)btnNav.Content == btnCall_Content)
			{
				Button btnNav2 = btnNav;
				string msgCallApp_BtnNav = SurveyMsg.MsgCallApp_BtnNav;
				((ContentControl)/*Error near IL_002a: Stack underflow*/).Content = msgCallApp_BtnNav;
				_003F121_003F(_003F347_003F, _003F348_003F);
				return;
			}
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0053;
			IL_0125:
			goto IL_0053;
			IL_0185:
			goto IL_0104;
			IL_0053:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (AppRunTimes == 0)
			{
				string msgNotRunApp = SurveyMsg.MsgNotRunApp;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0076: Stack underflow*/, (string)/*Error near IL_0076: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
				btnNav.Content = btnNav_Content;
				return;
			}
			if (!oLogicEngine.CheckLogic(CurPageId))
			{
				int iS_ALLOW_PASS = oLogicEngine.IS_ALLOW_PASS;
				if ((int)/*Error near IL_0158: Stack underflow*/ == 0)
				{
					string logic_Message = oLogicEngine.Logic_Message;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_00ab: Stack underflow*/, (string)/*Error near IL_00ab: Stack underflow*/, (MessageBoxButton)/*Error near IL_00ab: Stack underflow*/, MessageBoxImage.Hand);
					btnNav.Content = btnNav_Content;
					return;
				}
				if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
				{
					btnNav.Content = btnNav_Content;
					return;
				}
			}
			goto IL_0104;
			IL_0104:
			oPageNav.NextPage(MyNav, base.NavigationService);
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0025: Incompatible stack heights: 0 vs 1
			if (SecondsCountDown == 0)
			{
				DispatcherTimer timer2 = timer;
				((DispatcherTimer)/*Error near IL_0010: Stack underflow*/).Stop();
				btnNav.Foreground = Brushes.Black;
				btnNav.Content = btnCall_Content;
			}
			else
			{
				SecondsCountDown--;
				btnNav.Content = SecondsCountDown.ToString();
			}
		}

		private void _003F83_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_0028: Incompatible stack heights: 0 vs 1
			//IL_002d: Invalid comparison between Unknown and I4
			if (btnNav.IsEnabled)
			{
				Key key = _003F348_003F.Key;
				if ((int)/*Error near IL_002d: Stack underflow*/ == 20)
				{
					_003F58_003F(_003F347_003F, _003F348_003F);
				}
			}
		}

		private void _003F121_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			btnNav.Content = btnNav_Content;
			if (!((string)btnCallApp.Content != btnCall_Content))
			{
				btnCallApp.Content = SurveyMsg.MsgCallApp_BtnNav;
				string text = oQuestion.QDefine.CONTROL_TOOLTIP;
				string nOTE = oQuestion.QDefine.NOTE;
				if (text == _003F487_003F._003F488_003F(""))
				{
					text = Environment.CurrentDirectory;
				}
				else if (text.IndexOf(_003F487_003F._003F488_003F(";")) == -1)
				{
					text = Environment.CurrentDirectory + _003F487_003F._003F488_003F("]") + text;
				}
				string text2 = text + _003F487_003F._003F488_003F("]") + nOTE;
				if (File.Exists(text2))
				{
					AppRunTimes++;
					try
					{
						if (!new RarFile().StartProcess(text2, text, _003F487_003F._003F488_003F(""), ProcessWindowStyle.Maximized, true, 0, false))
						{
							MessageBox.Show(SurveyMsg.MsgPrgNotRun, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						}
					}
					catch (Exception)
					{
						btnCallApp.Content = btnCall_Content;
						MessageBox.Show(SurveyMsg.MsgRunAppError, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
					btnCallApp.Content = btnCall_Content;
				}
				else
				{
					AppRunTimes++;
					btnCallApp.Content = btnCall_Content;
					MessageBox.Show(SurveyMsg.MsgPrgNotFound, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				}
			}
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
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a60\u1b6eᱩ\u1d68ṯὪ\u2066Ⅵ≩⍷⑶┫♼❢⡯⥭"), UriKind.Relative);
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
				((PageCallApp)_003F350_003F).Loaded += _003F80_003F;
				((PageCallApp)_003F350_003F).KeyDown += _003F83_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtDisplayContext = (TextBlock)_003F350_003F;
				break;
			case 4:
				btnCallApp = (Button)_003F350_003F;
				btnCallApp.Click += _003F121_003F;
				break;
			case 5:
				txtProgram = (TextBlock)_003F350_003F;
				break;
			case 6:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 7:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 8:
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
