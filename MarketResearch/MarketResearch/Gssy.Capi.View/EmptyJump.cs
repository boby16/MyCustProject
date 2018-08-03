using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Gssy.Capi.View
{
	public class EmptyJump : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private QDisplay oQuestion = new QDisplay();

		private bool _contentLoaded;

		public EmptyJump()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_040d: Incompatible stack heights: 0 vs 1
			//IL_0428: Incompatible stack heights: 0 vs 2
			//IL_043f: Incompatible stack heights: 0 vs 2
			//IL_0464: Incompatible stack heights: 0 vs 1
			//IL_0475: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			oQuestion.Init(CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (!(MyNav.GroupLevel == _003F487_003F._003F488_003F("@")))
			{
				bool flag = MyNav.GroupLevel == _003F487_003F._003F488_003F("C");
				if ((int)/*Error near IL_0412: Stack underflow*/ == 0)
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
					goto IL_0331;
				}
			}
			MyNav.GroupPageType = oQuestion.QDefine.GROUP_PAGE_TYPE;
			MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
			MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
			MyNav.CircleACount = SurveyHelper.CircleACount;
			if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
			{
				NavBase myNav = MyNav;
				string gROUP_CODEB = ((EmptyJump)/*Error near IL_0100: Stack underflow*/).oQuestion.QDefine.GROUP_CODEB;
				((NavBase)/*Error near IL_010f: Stack underflow*/).GroupCodeB = gROUP_CODEB;
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
				string groupCodeB = ((NavBase)/*Error near IL_0210: Stack underflow*/).GroupCodeB;
				((VEAnswer)/*Error near IL_0215: Stack underflow*/).QUESTION_NAME = groupCodeB;
				vEAnswer2.CODE = MyNav.CircleBCode;
				vEAnswer2.CODE_TEXT = MyNav.CircleCodeTextB;
				list.Add(vEAnswer2);
				SurveyHelper.CircleBCode = MyNav.CircleBCode;
				SurveyHelper.CircleBCodeText = MyNav.CircleCodeTextB;
				SurveyHelper.CircleBCurrent = MyNav.CircleBCurrent;
				SurveyHelper.CircleBCount = MyNav.CircleBCount;
			}
			goto IL_0331;
			IL_0331:
			oLogicEngine.SurveyID = MySurveyId;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				LogicEngine oLogicEngine2 = oLogicEngine;
				string circleACodeText = SurveyHelper.CircleACodeText;
				((LogicEngine)/*Error near IL_036b: Stack underflow*/).CircleACodeText = circleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				SurveyHelper.AutoFill = false;
				((EmptyJump)/*Error near IL_047a: Stack underflow*/)._003F82_003F();
			}
			else
			{
				_003F58_003F();
			}
		}

		private void _003F58_003F()
		{
			//IL_0078: Incompatible stack heights: 0 vs 2
			//IL_008d: Incompatible stack heights: 0 vs 2
			//IL_00ac: Incompatible stack heights: 0 vs 3
			bool flag = true;
			oPageNav.oLogicEngine = oLogicEngine;
			if (!oPageNav.CheckLogic(CurPageId))
			{
				flag = false;
			}
			if (flag)
			{
				PageNav oPageNav2 = oPageNav;
				NavBase myNav = MyNav;
				NavigationService navigationService = base.NavigationService;
				if (!((PageNav)/*Error near IL_003a: Stack underflow*/).NextPage((NavBase)/*Error near IL_003a: Stack underflow*/, navigationService))
				{
					string curPageId = CurPageId;
					string surveyFirstPage = SurveyHelper.SurveyFirstPage;
					if ((string)/*Error near IL_0044: Stack underflow*/ == (string)/*Error near IL_0044: Stack underflow*/)
					{
						string text = SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd;
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_0050: Stack underflow*/, (string)/*Error near IL_0050: Stack underflow*/, (MessageBoxButton)/*Error near IL_0050: Stack underflow*/, MessageBoxImage.Hand);
					}
					else
					{
						_003F82_003F();
						SurveyHelper.NavOperation = _003F487_003F._003F488_003F("FŢɡ\u036a");
					}
				}
			}
			else
			{
				_003F82_003F();
			}
		}

		private void _003F82_003F()
		{
			//IL_0186: Incompatible stack heights: 0 vs 1
			//IL_01a4: Incompatible stack heights: 0 vs 1
			//IL_01b4: Incompatible stack heights: 0 vs 1
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(MySurveyId == _003F487_003F._003F488_003F("")))
			{
				bool flag = CurPageId == SurveyHelper.SurveyFirstPage;
				if ((int)/*Error near IL_018b: Stack underflow*/ == 0)
				{
					SurveyHelper.NavOperation = _003F487_003F._003F488_003F("FŢɡ\u036a");
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
						uriString = string.Format(arg0: MyNav.RoadMap.FORM_NAME, format: (string)/*Error near IL_013c: Stack underflow*/);
					}
					if (MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
					{
						NavigationService navigationService = base.NavigationService;
						((NavigationService)/*Error near IL_0161: Stack underflow*/).Refresh();
					}
					else
					{
						base.NavigationService.RemoveBackEntry();
						base.NavigationService.Navigate(new Uri(uriString));
					}
					SurveyHelper.SurveySequence = surveySequence - 1;
					SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
					SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
					return;
				}
			}
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			oPageNav.NextPage(MyNav, base.NavigationService);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\aŠɕ\u0356ѝԍ١\u0740ࡐॶਥ\u0b7e\u0c73\u0d76\u0e6a\u0f76ၶᅲቸ፡ᐻᕥᙻ\u1774ᡧ\u1920\u1a6b᭠\u1c7cᵿṳὣ⁽Ⅺ≶⌫⑼╢♯❭"), UriKind.Relative);
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
			if (_003F349_003F == 1)
			{
				((EmptyJump)_003F350_003F).Loaded += _003F80_003F;
				return;
			}
			goto IL_0007;
			IL_0030:
			goto IL_0007;
			IL_0007:
			_contentLoaded = true;
		}
	}
}
