using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Gssy.Capi.View
{
	public class CircleStart : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		public LogicEngine oLogicEngine = new LogicEngine();

		private QFill oQuestion = new QFill();

		private RandomBiz oRandomBiz = new RandomBiz();

		private bool _contentLoaded;

		public CircleStart()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0a97: Incompatible stack heights: 0 vs 1
			//IL_0aa2: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			oQuestion.Init(CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			bool flag = false;
			if (SurveyHelper.CircleACurrent == 0 && oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("@"))
			{
				flag = true;
			}
			if (SurveyHelper.CircleBCurrent == 0 && oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("C"))
			{
				flag = true;
			}
			if (oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F(""))
			{
				flag = true;
			}
			if (flag)
			{
				MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					MyNav.GroupLevel = _003F487_003F._003F488_003F("@");
					MyNav.GroupPageType = oQuestion.QDefine.GROUP_PAGE_TYPE;
					MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
					MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
					MyNav.CircleACount = SurveyHelper.CircleACount;
					MyNav.GetCircleInfo(MySurveyId);
					new List<VEAnswer>().Add(new VEAnswer
					{
						QUESTION_NAME = MyNav.GroupCodeA,
						CODE = MyNav.CircleACode,
						CODE_TEXT = MyNav.CircleCodeTextA
					});
					SurveyHelper.CircleACode = MyNav.CircleACode;
					SurveyHelper.CircleACodeText = MyNav.CircleCodeTextA;
					SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
					SurveyHelper.CircleACount = MyNav.CircleACount;
				}
				else
				{
					MyNav.GroupLevel = _003F487_003F._003F488_003F("");
				}
				oLogicEngine.SurveyID = MySurveyId;
				if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
				{
					oLogicEngine.CircleACode = SurveyHelper.CircleACode;
					oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
					oLogicEngine.CircleACount = SurveyHelper.CircleACount;
					oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				}
				string[] array = oLogicEngine.CircleGuideLogic(CurPageId, 1);
				List<SurveyDetail>.Enumerator enumerator;
				if (oQuestion.QDefine.SHOW_LOGIC != _003F487_003F._003F488_003F(""))
				{
					string sHOW_LOGIC = oQuestion.QDefine.SHOW_LOGIC;
					string[] array2 = oLogicEngine.aryCode(sHOW_LOGIC, ',');
					List<string> list = new List<string>();
					bool flag2 = oLogicEngine.IsExistCircleGuide(CurPageId);
					for (int i = 0; i < array2.Count(); i++)
					{
						if (flag2)
						{
							string[] array3 = array;
							foreach (string text in array3)
							{
								if (text == array2[i].ToString())
								{
									list.Add(text);
									break;
								}
							}
						}
						else
						{
							enumerator = oQuestion.QDetails.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									SurveyDetail current = enumerator.Current;
									if (current.CODE == array2[i].ToString())
									{
										list.Add(current.CODE);
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
						}
					}
					array = list.ToArray();
					oQuestion.QDefine.IS_RANDOM = 0;
				}
				if (array.Count() > 0 && array[0].ToString() != _003F487_003F._003F488_003F(""))
				{
					oRandomBiz.RebuildCircleGuide(MySurveyId, oQuestion.QDefine.QUESTION_NAME, array, oQuestion.QDefine.IS_RANDOM);
				}
				else if (oQuestion.QDefine.IS_RANDOM > 0)
				{
					List<string> list2 = new List<string>();
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							list2.Add(current2.CODE);
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					array = list2.ToArray();
					oRandomBiz.RebuildCircleGuide(MySurveyId, oQuestion.QDefine.QUESTION_NAME, array, oQuestion.QDefine.IS_RANDOM);
				}
			}
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel == _003F487_003F._003F488_003F("@") || MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
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
				List<VEAnswer> list3 = new List<VEAnswer>();
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = MyNav.GroupCodeA;
				vEAnswer.CODE = MyNav.CircleACode;
				vEAnswer.CODE_TEXT = MyNav.CircleCodeTextA;
				list3.Add(vEAnswer);
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
					list3.Add(vEAnswer2);
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
			int pAGE_COUNT_DOWN = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (pAGE_COUNT_DOWN > 0)
			{
				Thread.Sleep(pAGE_COUNT_DOWN);
			}
			if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				_003F82_003F();
				SurveyHelper.NavOperation = _003F487_003F._003F488_003F("FŢɡ\u036a");
			}
			else
			{
				if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
				{
					if (!(MyNav.GroupLevel == _003F487_003F._003F488_003F("C")))
					{
						string circleACode = MyNav.CircleACode;
					}
					else
					{
						string circleBCode = MyNav.CircleBCode;
					}
					string text2 = (string)/*Error near IL_0aa4: Stack underflow*/;
					SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + text2;
					oQuestion.FillText = text2;
					oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
				}
				_003F81_003F();
			}
		}

		private void _003F81_003F()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			try
			{
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F(""))
				{
					MyNav.NextPage(MySurveyId, surveySequence, CurPageId, roadMapVersion);
				}
				else
				{
					MyNav.NextCirclePage(MySurveyId, surveySequence, CurPageId, roadMapVersion);
					SurveyHelper.CircleACount = MyNav.CircleACount;
					SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
					if (MyNav.IsLastA && (MyNav.GroupPageType == 0 || MyNav.GroupPageType == 2))
					{
						SurveyHelper.CircleACode = _003F487_003F._003F488_003F("");
						SurveyHelper.CircleACodeText = _003F487_003F._003F488_003F("");
					}
					if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
					{
						SurveyHelper.CircleBCount = MyNav.CircleBCount;
						SurveyHelper.CircleBCurrent = MyNav.CircleBCurrent;
						if (MyNav.IsLastB && (MyNav.GroupPageType == 10 || MyNav.GroupPageType == 12 || MyNav.GroupPageType == 30 || MyNav.GroupPageType == 32))
						{
							SurveyHelper.CircleBCode = _003F487_003F._003F488_003F("");
							SurveyHelper.CircleBCodeText = _003F487_003F._003F488_003F("");
						}
					}
				}
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
					goto IL_040e;
				}
				if (oPageNav.FormIsOK(MyNav.RoadMap.FORM_NAME))
				{
					base.NavigationService.RemoveBackEntry();
					base.NavigationService.Navigate(new Uri(uriString));
					goto IL_040e;
				}
				string text2 = string.Format(SurveyMsg.MsgErrorJump, MySurveyId, CurPageId, MyNav.RoadMap.VERSION_ID, MyNav.RoadMap.PAGE_ID, MyNav.RoadMap.FORM_NAME);
				MessageBox.Show(SurveyMsg.MsgErrorRoadmap + Environment.NewLine + Environment.NewLine + text2 + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				oLogicEngine.OutputResult(text2, _003F487_003F._003F488_003F("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"));
				if (!(CurPageId == SurveyHelper.SurveyFirstPage))
				{
					_003F82_003F();
					SurveyHelper.NavOperation = _003F487_003F._003F488_003F("FŢɡ\u036a");
					goto IL_040e;
				}
				MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				goto end_IL_000c;
				IL_040e:
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
			//IL_0176: Incompatible stack heights: 0 vs 1
			//IL_0194: Incompatible stack heights: 0 vs 1
			//IL_01a4: Incompatible stack heights: 0 vs 1
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(MySurveyId == _003F487_003F._003F488_003F("")))
			{
				bool flag = CurPageId == SurveyHelper.SurveyFirstPage;
				if ((int)/*Error near IL_017b: Stack underflow*/ == 0)
				{
					new SurveyBiz().ClearPageAnswer(MySurveyId, surveySequence);
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
						uriString = string.Format(arg0: MyNav.RoadMap.FORM_NAME, format: (string)/*Error near IL_012c: Stack underflow*/);
					}
					if (MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
					{
						NavigationService navigationService = base.NavigationService;
						((NavigationService)/*Error near IL_0151: Stack underflow*/).Refresh();
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
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a73᭦\u1c7cᵮṠὮ⁹ⅽ≩⍵⑲┫♼❢⡯⥭"), UriKind.Relative);
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
				((CircleStart)_003F350_003F).Loaded += _003F80_003F;
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
