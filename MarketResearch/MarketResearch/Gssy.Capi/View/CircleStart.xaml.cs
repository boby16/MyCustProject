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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x0200000B RID: 11
	public partial class CircleStart : Page
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00008620 File Offset: 0x00006820
		public CircleStart()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00008670 File Offset: 0x00006870
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.oQuestion.Init(this.CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			bool flag = false;
			if (SurveyHelper.CircleACurrent == 0 && this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				flag = true;
			}
			if (SurveyHelper.CircleBCurrent == 0 && this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				flag = true;
			}
			if (this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0(""))
			{
				flag = true;
			}
			if (flag)
			{
				this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					this.MyNav.GroupLevel = global::GClass0.smethod_0("@");
					this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
					this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
					this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
					this.MyNav.CircleACount = SurveyHelper.CircleACount;
					this.MyNav.GetCircleInfo(this.MySurveyId);
					new List<VEAnswer>().Add(new VEAnswer
					{
						QUESTION_NAME = this.MyNav.GroupCodeA,
						CODE = this.MyNav.CircleACode,
						CODE_TEXT = this.MyNav.CircleCodeTextA
					});
					SurveyHelper.CircleACode = this.MyNav.CircleACode;
					SurveyHelper.CircleACodeText = this.MyNav.CircleCodeTextA;
					SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
					SurveyHelper.CircleACount = this.MyNav.CircleACount;
				}
				else
				{
					this.MyNav.GroupLevel = global::GClass0.smethod_0("");
				}
				this.oLogicEngine.SurveyID = this.MySurveyId;
				if (this.MyNav.GroupLevel != global::GClass0.smethod_0(""))
				{
					this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
					this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
					this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
					this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				}
				string[] array = this.oLogicEngine.CircleGuideLogic(this.CurPageId, 1);
				if (this.oQuestion.QDefine.SHOW_LOGIC != global::GClass0.smethod_0(""))
				{
					string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
					string[] array2 = this.oLogicEngine.aryCode(show_LOGIC, ',');
					List<string> list = new List<string>();
					bool flag2 = this.oLogicEngine.IsExistCircleGuide(this.CurPageId);
					for (int i = 0; i < array2.Count<string>(); i++)
					{
						if (flag2)
						{
							foreach (string text in array)
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
							foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
							{
								if (surveyDetail.CODE == array2[i].ToString())
								{
									list.Add(surveyDetail.CODE);
									break;
								}
							}
						}
					}
					array = list.ToArray();
					this.oQuestion.QDefine.IS_RANDOM = 0;
				}
				if (array.Count<string>() > 0 && array[0].ToString() != global::GClass0.smethod_0(""))
				{
					this.oRandomBiz.RebuildCircleGuide(this.MySurveyId, this.oQuestion.QDefine.QUESTION_NAME, array, this.oQuestion.QDefine.IS_RANDOM);
				}
				else if (this.oQuestion.QDefine.IS_RANDOM > 0)
				{
					List<string> list2 = new List<string>();
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						list2.Add(surveyDetail2.CODE);
					}
					array = list2.ToArray();
					this.oRandomBiz.RebuildCircleGuide(this.MySurveyId, this.oQuestion.QDefine.QUESTION_NAME, array, this.oQuestion.QDefine.IS_RANDOM);
				}
			}
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (!(this.MyNav.GroupLevel == global::GClass0.smethod_0("@")) && !(this.MyNav.GroupLevel == global::GClass0.smethod_0("C")))
			{
				SurveyHelper.CircleACode = global::GClass0.smethod_0("");
				SurveyHelper.CircleACodeText = global::GClass0.smethod_0("");
				SurveyHelper.CircleACurrent = 0;
				SurveyHelper.CircleACount = 0;
				SurveyHelper.CircleBCode = global::GClass0.smethod_0("");
				SurveyHelper.CircleBCodeText = global::GClass0.smethod_0("");
				SurveyHelper.CircleBCurrent = 0;
				SurveyHelper.CircleBCount = 0;
			}
			else
			{
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					this.MyNav.GroupCodeB = this.oQuestion.QDefine.GROUP_CODEB;
					this.MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
					this.MyNav.CircleBCount = SurveyHelper.CircleBCount;
				}
				this.MyNav.GetCircleInfo(this.MySurveyId);
				this.oQuestion.QuestionName = this.oQuestion.QuestionName + this.MyNav.QName_Add;
				List<VEAnswer> list3 = new List<VEAnswer>();
				list3.Add(new VEAnswer
				{
					QUESTION_NAME = this.MyNav.GroupCodeA,
					CODE = this.MyNav.CircleACode,
					CODE_TEXT = this.MyNav.CircleCodeTextA
				});
				SurveyHelper.CircleACode = this.MyNav.CircleACode;
				SurveyHelper.CircleACodeText = this.MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
				SurveyHelper.CircleACount = this.MyNav.CircleACount;
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					list3.Add(new VEAnswer
					{
						QUESTION_NAME = this.MyNav.GroupCodeB,
						CODE = this.MyNav.CircleBCode,
						CODE_TEXT = this.MyNav.CircleCodeTextB
					});
					SurveyHelper.CircleBCode = this.MyNav.CircleBCode;
					SurveyHelper.CircleBCodeText = this.MyNav.CircleCodeTextB;
					SurveyHelper.CircleBCurrent = this.MyNav.CircleBCurrent;
					SurveyHelper.CircleBCount = this.MyNav.CircleBCount;
				}
			}
			this.oLogicEngine.SurveyID = this.MySurveyId;
			if (this.MyNav.GroupLevel != global::GClass0.smethod_0(""))
			{
				this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				this.oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				this.oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				this.oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				this.oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			int page_COUNT_DOWN = this.oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (page_COUNT_DOWN > 0)
			{
				Thread.Sleep(page_COUNT_DOWN);
			}
			if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
			{
				this.method_2();
				SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
				return;
			}
			if (this.MyNav.GroupLevel != global::GClass0.smethod_0(""))
			{
				string text2 = (this.MyNav.GroupLevel == global::GClass0.smethod_0("C")) ? this.MyNav.CircleBCode : this.MyNav.CircleACode;
				SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + text2;
				this.oQuestion.FillText = text2;
				this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			}
			this.method_1();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00008FB0 File Offset: 0x000071B0
		private void method_1()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			try
			{
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0(""))
				{
					this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
				}
				else
				{
					this.MyNav.NextCirclePage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
					SurveyHelper.CircleACount = this.MyNav.CircleACount;
					SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
					if (this.MyNav.IsLastA && (this.MyNav.GroupPageType == 0 || this.MyNav.GroupPageType == 2))
					{
						SurveyHelper.CircleACode = global::GClass0.smethod_0("");
						SurveyHelper.CircleACodeText = global::GClass0.smethod_0("");
					}
					if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
					{
						SurveyHelper.CircleBCount = this.MyNav.CircleBCount;
						SurveyHelper.CircleBCurrent = this.MyNav.CircleBCurrent;
						if (this.MyNav.IsLastB && (this.MyNav.GroupPageType == 10 || this.MyNav.GroupPageType == 12 || this.MyNav.GroupPageType == 30 || this.MyNav.GroupPageType == 32))
						{
							SurveyHelper.CircleBCode = global::GClass0.smethod_0("");
							SurveyHelper.CircleBCodeText = global::GClass0.smethod_0("");
						}
					}
				}
				string text = this.oLogicEngine.Route(this.MyNav.RoadMap.FORM_NAME);
				SurveyHelper.RoadMapVersion = this.MyNav.RoadMap.VERSION_ID.ToString();
				string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), text);
				if (text.Substring(0, 1) == global::GClass0.smethod_0("@"))
				{
					uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), text);
				}
				if (text == SurveyHelper.CurPageName)
				{
					base.NavigationService.Refresh();
				}
				else if (this.oPageNav.FormIsOK(this.MyNav.RoadMap.FORM_NAME))
				{
					base.NavigationService.RemoveBackEntry();
					base.NavigationService.Navigate(new Uri(uriString));
				}
				else
				{
					string text2 = string.Format(SurveyMsg.MsgErrorJump, new object[]
					{
						this.MySurveyId,
						this.CurPageId,
						this.MyNav.RoadMap.VERSION_ID,
						this.MyNav.RoadMap.PAGE_ID,
						this.MyNav.RoadMap.FORM_NAME
					});
					MessageBox.Show(string.Concat(new string[]
					{
						SurveyMsg.MsgErrorRoadmap,
						Environment.NewLine,
						Environment.NewLine,
						text2,
						SurveyMsg.MsgErrorEnd
					}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.oLogicEngine.OutputResult(text2, global::GClass0.smethod_0("Nŭɻͣэխ٥ݳࡢप੏୭౦"));
					if (this.CurPageId == SurveyHelper.SurveyFirstPage)
					{
						MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
					this.method_2();
					SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
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
				string text3 = string.Format(SurveyMsg.MsgErrorJump, new object[]
				{
					this.MySurveyId,
					this.CurPageId,
					this.MyNav.RoadMap.VERSION_ID,
					this.MyNav.RoadMap.PAGE_ID,
					this.MyNav.RoadMap.FORM_NAME
				});
				MessageBox.Show(string.Concat(new string[]
				{
					SurveyMsg.MsgErrorRoadmap,
					Environment.NewLine,
					Environment.NewLine,
					text3,
					SurveyMsg.MsgErrorEnd
				}), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.oLogicEngine.OutputResult(text3, global::GClass0.smethod_0("Nŭɻͣэխ٥ݳࡢप੏୭౦"));
				if (this.CurPageId == SurveyHelper.SurveyFirstPage)
				{
					MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				else
				{
					this.method_2();
					SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00009468 File Offset: 0x00007668
		private void method_2()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(this.MySurveyId == global::GClass0.smethod_0("")) && !(this.CurPageId == SurveyHelper.SurveyFirstPage))
			{
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, surveySequence);
				string roadMapVersion = SurveyHelper.RoadMapVersion;
				this.MyNav.PrePage(this.MySurveyId, surveySequence, roadMapVersion);
				SurveyHelper.CircleACount = this.MyNav.Sequence.CIRCLE_A_COUNT;
				SurveyHelper.CircleACurrent = this.MyNav.Sequence.CIRCLE_A_CURRENT;
				SurveyHelper.CircleBCount = this.MyNav.Sequence.CIRCLE_B_COUNT;
				SurveyHelper.CircleBCurrent = this.MyNav.Sequence.CIRCLE_B_CURRENT;
				string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), this.MyNav.RoadMap.FORM_NAME);
				if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == global::GClass0.smethod_0("@"))
				{
					uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), this.MyNav.RoadMap.FORM_NAME);
				}
				if (this.MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
				{
					base.NavigationService.Refresh();
				}
				else
				{
					base.NavigationService.RemoveBackEntry();
					base.NavigationService.Navigate(new Uri(uriString));
				}
				SurveyHelper.SurveySequence = surveySequence - 1;
				SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
				SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
				return;
			}
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			this.method_1();
		}

		// Token: 0x04000077 RID: 119
		private string MySurveyId;

		// Token: 0x04000078 RID: 120
		private string CurPageId;

		// Token: 0x04000079 RID: 121
		private NavBase MyNav = new NavBase();

		// Token: 0x0400007A RID: 122
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400007B RID: 123
		public LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400007C RID: 124
		private QFill oQuestion = new QFill();

		// Token: 0x0400007D RID: 125
		private RandomBiz oRandomBiz = new RandomBiz();
	}
}
