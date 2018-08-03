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
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class Display : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private LogicExplain oLogicExplain = new LogicExplain();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QDisplay oQuestion = new QDisplay();

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal Grid g;

		internal Grid gridContent;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ScrollViewer scrollNote;

		internal TextBlock txtQuestionNote;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public Display()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_05b0: Incompatible stack heights: 0 vs 2
			//IL_05c9: Incompatible stack heights: 0 vs 2
			//IL_05ee: Incompatible stack heights: 0 vs 1
			//IL_0603: Incompatible stack heights: 0 vs 2
			//IL_0630: Incompatible stack heights: 0 vs 2
			//IL_064a: Incompatible stack heights: 0 vs 1
			//IL_064f: Incompatible stack heights: 1 vs 0
			//IL_065f: Incompatible stack heights: 0 vs 1
			//IL_0674: Incompatible stack heights: 0 vs 1
			//IL_0684: Incompatible stack heights: 0 vs 1
			//IL_06a0: Incompatible stack heights: 0 vs 1
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
					NavBase myNav = MyNav;
					string gROUP_CODEB = ((Display)/*Error near IL_00ed: Stack underflow*/).oQuestion.QDefine.GROUP_CODEB;
					((NavBase)/*Error near IL_00fc: Stack underflow*/).GroupCodeB = gROUP_CODEB;
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
					NavBase myNav2 = MyNav;
					string groupCodeB = ((NavBase)/*Error near IL_01fd: Stack underflow*/).GroupCodeB;
					((VEAnswer)/*Error near IL_0202: Stack underflow*/).QUESTION_NAME = groupCodeB;
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
				LogicEngine oLogicEngine2 = oLogicEngine;
				string circleACodeText = SurveyHelper.CircleACodeText;
				((LogicEngine)/*Error near IL_035b: Stack underflow*/).CircleACodeText = circleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			if (SurveyHelper.AutoFill)
			{
				new AutoFill();
				QDisplay oQuestion2 = oQuestion;
				SurveyDefine qDefine = ((QDisplay)/*Error near IL_03ca: Stack underflow*/).QDefine;
				if (((AutoFill)/*Error near IL_03cf: Stack underflow*/).AutoNext(qDefine))
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			if (oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				Grid gridContent2 = gridContent;
				SurveyDefine qDefine2 = oQuestion.QDefine;
				double width = (double)((SurveyDefine)/*Error near IL_03ee: Stack underflow*/).CONTROL_WIDTH;
				((FrameworkElement)/*Error near IL_03f4: Stack underflow*/).Width = width;
			}
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list2[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			string text;
			if (list2.Count > 1)
			{
				text = list2[1];
			}
			else
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			qUESTION_TITLE = text;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			qUESTION_TITLE = oQuestion.QDefine.NOTE;
			oBoldTitle.SetTextBlock(txtQuestionNote, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oFunc.LEFT(oQuestion.QDefine.CONTROL_TOOLTIP, 1) == _003F487_003F._003F488_003F("\""))
			{
				QDisplay oQuestion3 = oQuestion;
				string cONTROL_TOOLTIP = ((QDisplay)/*Error near IL_04ec: Stack underflow*/).QDefine.CONTROL_TOOLTIP;
				g.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(cONTROL_TOOLTIP));
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ"))
			{
				SurveyDefine qDefine3 = oQuestion.QDefine;
				if (((SurveyDefine)/*Error near IL_052d: Stack underflow*/).IS_ATTACH == 1)
				{
					Button btnAttach2 = btnAttach;
					((UIElement)/*Error near IL_0539: Stack underflow*/).Visibility = Visibility.Visible;
				}
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				Button btnNav2 = btnNav;
				SolidColorBrush gray = Brushes.Gray;
				((System.Windows.Controls.Control)/*Error near IL_06a5: Stack underflow*/).Foreground = gray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
		}

		private void _003F83_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
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
			}
			else
			{
				oPageNav.NextPage(MyNav, base.NavigationService);
				btnNav.Content = btnNav_Content;
			}
			return;
			IL_0069:
			goto IL_0020;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\tŢɗ\u0350ћԏ٣ݾ\u086eॴਧ\u0b78\u0c75൴\u0e68\u0f78ၸᅰቺ፧ᐽᕧᙹᝪ\u1879\u1922\u1a68᭢\u1c79ᵹṤὦ\u207fÅ≼⍢⑯╭"), UriKind.Relative);
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
				((Display)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				g = (Grid)_003F350_003F;
				break;
			case 3:
				gridContent = (Grid)_003F350_003F;
				break;
			case 4:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 5:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 6:
				scrollNote = (ScrollViewer)_003F350_003F;
				break;
			case 7:
				txtQuestionNote = (TextBlock)_003F350_003F;
				break;
			case 8:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 9:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 10:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0035:
			goto IL_003f;
		}
	}
}
