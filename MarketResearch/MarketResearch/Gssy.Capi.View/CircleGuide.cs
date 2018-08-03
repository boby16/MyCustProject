using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Gssy.Capi.View
{
	public class CircleGuide : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		public LogicEngine oLogicEngine = new LogicEngine();

		private bool _contentLoaded;

		public CircleGuide()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Expected I4, but got Unknown
			//IL_028e: Incompatible stack heights: 0 vs 2
			//IL_02a3: Incompatible stack heights: 0 vs 1
			//IL_02b8: Incompatible stack heights: 0 vs 1
			//IL_02bd: Invalid comparison between Unknown and I4
			//IL_02d4: Incompatible stack heights: 0 vs 3
			//IL_02e9: Incompatible stack heights: 0 vs 1
			//IL_02fa: Incompatible stack heights: 0 vs 2
			//IL_0305: Incompatible stack heights: 0 vs 1
			//IL_031b: Incompatible stack heights: 0 vs 3
			//IL_0345: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			QDisplay qDisplay = new QDisplay();
			qDisplay.Init(CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			string navOperation = SurveyHelper.NavOperation;
			MyNav.GroupLevel = qDisplay.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				NavBase myNav = MyNav;
				int gROUP_PAGE_TYPE = ((QDisplay)/*Error near IL_0088: Stack underflow*/).QDefine.GROUP_PAGE_TYPE;
				((NavBase)/*Error near IL_0092: Stack underflow*/).GroupPageType = gROUP_PAGE_TYPE;
				MyNav.GroupCodeA = qDisplay.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				if (navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
				{
					string groupLevel = MyNav.GroupLevel;
					string b = _003F487_003F._003F488_003F("@");
					if ((string)/*Error near IL_00ec: Stack underflow*/ == b)
					{
						int circleACurrent = MyNav.CircleACurrent;
						if ((int)/*Error near IL_02bd: Stack underflow*/ > 1)
						{
							NavBase myNav2 = MyNav;
							int circleACurrent2 = MyNav.CircleACurrent;
							_003F val = /*Error near IL_00f8: Stack underflow*/- /*Error near IL_00f8: Stack underflow*/;
							((NavBase)/*Error near IL_00fd: Stack underflow*/).CircleACurrent = (int)val;
						}
					}
				}
				else if (MyNav.GroupLevel == _003F487_003F._003F488_003F("@"))
				{
					NavBase myNav3 = MyNav;
					if (((NavBase)/*Error near IL_0126: Stack underflow*/).CircleACurrent == 0)
					{
						NavBase myNav4 = MyNav;
						((NavBase)/*Error near IL_0130: Stack underflow*/).CircleACurrent = (int)/*Error near IL_0130: Stack underflow*/;
					}
				}
				MyNav.GetCircleInfo(MySurveyId);
			}
			LogicEngine logicEngine = new LogicEngine();
			logicEngine.SurveyID = MySurveyId;
			logicEngine.CircleACode = MyNav.CircleACode;
			logicEngine.CircleBCode = MyNav.CircleBCode;
			logicEngine.CircleACode = MyNav.CircleACode;
			logicEngine.CircleACodeText = MyNav.CircleCodeTextA;
			logicEngine.CircleACount = MyNav.CircleACount;
			logicEngine.CircleACurrent = MyNav.CircleACurrent;
			logicEngine.CircleBCode = MyNav.CircleBCode;
			logicEngine.CircleBCodeText = MyNav.CircleCodeTextB;
			logicEngine.CircleBCount = MyNav.CircleBCount;
			logicEngine.CircleBCurrent = MyNav.CircleBCurrent;
			string[] array = logicEngine.CircleGuideLogic(CurPageId, 1);
			if (array.Count() > 0 && ((object[])/*Error near IL_0217: Stack underflow*/)[0].ToString() != _003F487_003F._003F488_003F(""))
			{
				new RandomBiz();
				string mySurveyId = MySurveyId;
				string qUESTION_NAME = ((QDisplay)/*Error near IL_0235: Stack underflow*/).QDefine.QUESTION_NAME;
				string[] _003F498_003F = array;
				int iS_RANDOM = qDisplay.QDefine.IS_RANDOM;
				((RandomBiz)/*Error near IL_024b: Stack underflow*/).RebuildCircleGuide((string)/*Error near IL_024b: Stack underflow*/, qUESTION_NAME, _003F498_003F, iS_RANDOM);
			}
			int pAGE_COUNT_DOWN = qDisplay.QDefine.PAGE_COUNT_DOWN;
			if (pAGE_COUNT_DOWN > 0)
			{
				Thread.Sleep(pAGE_COUNT_DOWN);
			}
			if (navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				_003F82_003F();
				_003F487_003F._003F488_003F("FŢɡ\u036a");
				SurveyHelper.NavOperation = (string)/*Error near IL_0278: Stack underflow*/;
			}
			else
			{
				_003F81_003F();
			}
		}

		private void _003F81_003F()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			try
			{
				MyNav.NextPage(MySurveyId, surveySequence, CurPageId, roadMapVersion);
				string text = oLogicEngine.Route(MyNav.RoadMap.FORM_NAME);
				SurveyHelper.RoadMapVersion = MyNav.RoadMap.VERSION_ID.ToString();
				string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), text);
				if (text.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
				{
					uriString = string.Format(_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭"), text);
				}
				if (text == SurveyHelper.CurPageName)
				{
					base.NavigationService.Refresh();
					goto IL_023f;
				}
				if (oPageNav.FormIsOK(MyNav.RoadMap.FORM_NAME))
				{
					base.NavigationService.RemoveBackEntry();
					base.NavigationService.Navigate(new Uri(uriString));
					goto IL_023f;
				}
				string text2 = string.Format(SurveyMsg.MsgErrorJump, MySurveyId, CurPageId, MyNav.RoadMap.VERSION_ID, MyNav.RoadMap.PAGE_ID, MyNav.RoadMap.FORM_NAME);
				MessageBox.Show(SurveyMsg.MsgErrorRoadmap + Environment.NewLine + Environment.NewLine + text2 + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				oLogicEngine.OutputResult(text2, _003F487_003F._003F488_003F("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"));
				if (!(CurPageId == SurveyHelper.SurveyFirstPage))
				{
					_003F82_003F();
					SurveyHelper.NavOperation = _003F487_003F._003F488_003F("FŢɡ\u036a");
					goto IL_023f;
				}
				MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				goto end_IL_000c;
				IL_023f:
				SurveyHelper.SurveySequence = surveySequence + 1;
				SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
				SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
				SurveyHelper.NavGoBackTimes = 0;
				SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
				SurveyHelper.NavLoad = 0;
				end_IL_000c:;
			}
			catch (Exception)
			{
				string text3 = string.Format(SurveyMsg.MsgErrorJump, MySurveyId, CurPageId, MyNav.RoadMap.VERSION_ID, MyNav.RoadMap.PAGE_ID, MyNav.RoadMap.FORM_NAME);
				MessageBox.Show(SurveyMsg.MsgErrorRoadmap + Environment.NewLine + Environment.NewLine + text3 + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				oLogicEngine.OutputResult(text3, _003F487_003F._003F488_003F("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"));
				if (CurPageId == SurveyHelper.SurveyFirstPage)
				{
					MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				else
				{
					_003F82_003F();
					SurveyHelper.NavOperation = _003F487_003F._003F488_003F("FŢɡ\u036a");
				}
			}
		}

		private void _003F82_003F()
		{
			//IL_016b: Incompatible stack heights: 0 vs 2
			//IL_0189: Incompatible stack heights: 0 vs 1
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(MySurveyId == _003F487_003F._003F488_003F("")))
			{
				string curPageId = CurPageId;
				string surveyFirstPage = SurveyHelper.SurveyFirstPage;
				if (!((string)/*Error near IL_0025: Stack underflow*/ == (string)/*Error near IL_0025: Stack underflow*/))
				{
					string roadMapVersion = SurveyHelper.RoadMapVersion;
					MyNav.PrePage(MySurveyId, surveySequence, roadMapVersion);
					SurveyHelper.CircleACount = MyNav.Sequence.CIRCLE_A_COUNT;
					SurveyHelper.CircleACurrent = MyNav.Sequence.CIRCLE_A_CURRENT;
					SurveyHelper.CircleBCount = MyNav.Sequence.CIRCLE_B_COUNT;
					SurveyHelper.CircleBCurrent = MyNav.Sequence.CIRCLE_B_CURRENT;
					string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), MyNav.RoadMap.FORM_NAME);
					if (MyNav.RoadMap.FORM_NAME.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
					{
						_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭");
						uriString = string.Format(arg0: MyNav.RoadMap.FORM_NAME, format: (string)/*Error near IL_0120: Stack underflow*/);
					}
					if (!(MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName))
					{
						base.NavigationService.RemoveBackEntry();
						base.NavigationService.Navigate(new Uri(uriString));
					}
					else
					{
						base.NavigationService.Refresh();
					}
					SurveyHelper.SurveySequence = surveySequence - 1;
					SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
					SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
					return;
				}
			}
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			_003F81_003F();
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a73᭦\u1c7cᵮṠὮ\u206dⅼ≡⍣④┫♼❢⡯⥭"), UriKind.Relative);
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
			//IL_002f: Incompatible stack heights: 0 vs 2
			if (_003F349_003F == 1)
			{
				CircleGuide circleGuide = (CircleGuide)_003F350_003F;
				RoutedEventHandler value = ((CircleGuide)/*Error near IL_0012: Stack underflow*/)._003F80_003F;
				((FrameworkElement)/*Error near IL_0017: Stack underflow*/).Loaded += value;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
