using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;

namespace Gssy.Capi.View
{
	// Token: 0x02000045 RID: 69
	public partial class CircleGuide : Page
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x000034DD File Offset: 0x000016DD
		public CircleGuide()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00086784 File Offset: 0x00084984
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			QDisplay qdisplay = new QDisplay();
			qdisplay.Init(this.CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			string navOperation = SurveyHelper.NavOperation;
			this.MyNav.GroupLevel = qdisplay.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel != global::GClass0.smethod_0(""))
			{
				this.MyNav.GroupPageType = qdisplay.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = qdisplay.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				if (navOperation == global::GClass0.smethod_0("FŢɡͪ"))
				{
					if (this.MyNav.GroupLevel == global::GClass0.smethod_0("@") && this.MyNav.CircleACurrent > 1)
					{
						this.MyNav.CircleACurrent = this.MyNav.CircleACurrent - 1;
					}
				}
				else if (this.MyNav.GroupLevel == global::GClass0.smethod_0("@") && this.MyNav.CircleACurrent == 0)
				{
					this.MyNav.CircleACurrent = 1;
				}
				this.MyNav.GetCircleInfo(this.MySurveyId);
			}
			string[] array = new LogicEngine
			{
				SurveyID = this.MySurveyId,
				CircleBCode = this.MyNav.CircleBCode,
				CircleACode = this.MyNav.CircleACode,
				CircleACodeText = this.MyNav.CircleCodeTextA,
				CircleACount = this.MyNav.CircleACount,
				CircleACurrent = this.MyNav.CircleACurrent,
				CircleBCodeText = this.MyNav.CircleCodeTextB,
				CircleBCount = this.MyNav.CircleBCount,
				CircleBCurrent = this.MyNav.CircleBCurrent
			}.CircleGuideLogic(this.CurPageId, 1);
			if (array.Count<string>() > 0 && array[0].ToString() != global::GClass0.smethod_0(""))
			{
				new RandomBiz().RebuildCircleGuide(this.MySurveyId, qdisplay.QDefine.QUESTION_NAME, array, qdisplay.QDefine.IS_RANDOM);
			}
			int page_COUNT_DOWN = qdisplay.QDefine.PAGE_COUNT_DOWN;
			if (page_COUNT_DOWN > 0)
			{
				Thread.Sleep(page_COUNT_DOWN);
			}
			if (navOperation == global::GClass0.smethod_0("FŢɡͪ"))
			{
				this.method_2();
				SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
				return;
			}
			this.method_1();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00086A54 File Offset: 0x00084C54
		private void method_1()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			try
			{
				this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
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

		// Token: 0x060004B9 RID: 1209 RVA: 0x00086DC8 File Offset: 0x00084FC8
		private void method_2()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(this.MySurveyId == global::GClass0.smethod_0("")) && !(this.CurPageId == SurveyHelper.SurveyFirstPage))
			{
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

		// Token: 0x040008DB RID: 2267
		private string MySurveyId;

		// Token: 0x040008DC RID: 2268
		private string CurPageId;

		// Token: 0x040008DD RID: 2269
		private NavBase MyNav = new NavBase();

		// Token: 0x040008DE RID: 2270
		private PageNav oPageNav = new PageNav();

		// Token: 0x040008DF RID: 2271
		public LogicEngine oLogicEngine = new LogicEngine();
	}
}
