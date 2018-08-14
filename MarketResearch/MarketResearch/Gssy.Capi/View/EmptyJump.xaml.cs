using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x0200000E RID: 14
	public partial class EmptyJump : Page
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000024DF File Offset: 0x000006DF
		public EmptyJump()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000AEF8 File Offset: 0x000090F8
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.oQuestion.Init(this.CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
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
				this.MyNav.GroupCodeA = global::GClass0.smethod_0("");
				this.MyNav.CircleACurrent = 0;
				this.MyNav.CircleACount = 0;
				this.MyNav.GroupCodeB = global::GClass0.smethod_0("");
				this.MyNav.CircleBCurrent = 0;
				this.MyNav.CircleBCount = 0;
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
				List<VEAnswer> list = new List<VEAnswer>();
				list.Add(new VEAnswer
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
					list.Add(new VEAnswer
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
			if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
			{
				SurveyHelper.AutoFill = false;
				this.method_2();
				return;
			}
			this.method_1();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000B33C File Offset: 0x0000953C
		private void method_1()
		{
			bool flag = true;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				flag = false;
			}
			if (flag)
			{
				if (!this.oPageNav.NextPage(this.MyNav, base.NavigationService))
				{
					if (this.CurPageId == SurveyHelper.SurveyFirstPage)
					{
						MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
					this.method_2();
					SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
					return;
				}
			}
			else
			{
				this.method_2();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000B3DC File Offset: 0x000095DC
		private void method_2()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(this.MySurveyId == global::GClass0.smethod_0("")) && !(this.CurPageId == SurveyHelper.SurveyFirstPage))
			{
				SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
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
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
		}

		// Token: 0x040000AC RID: 172
		private string MySurveyId;

		// Token: 0x040000AD RID: 173
		private string CurPageId;

		// Token: 0x040000AE RID: 174
		private NavBase MyNav = new NavBase();

		// Token: 0x040000AF RID: 175
		private PageNav oPageNav = new PageNav();

		// Token: 0x040000B0 RID: 176
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040000B1 RID: 177
		private QDisplay oQuestion = new QDisplay();
	}
}
