using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x02000042 RID: 66
	public partial class SurveyCheckRecord : Page
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x0000339C File Offset: 0x0000159C
		public SurveyCheckRecord()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00083A0C File Offset: 0x00081C0C
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.oQuestion.Init(this.CurPageId, 0);
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel != global::GClass0.smethod_0(""))
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
			else
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
			if (SurveyHelper.AutoFill && new AutoFill().AutoNext(this.oQuestion.QDefine))
			{
				this.method_1();
			}
			string question_TITLE = this.oQuestion.QDefine.QUESTION_TITLE;
			this.txtQuestionTitle.Text = global::GClass0.smethod_0("");
			BoldTitle boldTitle = new BoldTitle();
			boldTitle.SpanTitle(this.MySurveyId, question_TITLE, SurveyHelper.CircleACode, SurveyHelper.CircleBCode);
			foreach (classHtmlText classHtmlText in boldTitle.lSpan)
			{
				if (classHtmlText.TitleTextType == global::GClass0.smethod_0("?ŀȿ"))
				{
					Span span = new Span();
					span.Inlines.Add(new Run(classHtmlText.TitleText));
					span.Foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
					span.FontWeight = FontWeights.Bold;
					this.txtQuestionTitle.Inlines.Add(span);
				}
				else
				{
					Span span2 = new Span();
					span2.Inlines.Add(new Run(classHtmlText.TitleText));
					this.txtQuestionTitle.Inlines.Add(span2);
				}
			}
			if (this.oQuestion.QDefine.TITLE_FONTSIZE != 0)
			{
				this.txtQuestionTitle.FontSize = (double)this.oQuestion.QDefine.TITLE_FONTSIZE;
			}
			if (this.oQuestion.QDefine.TITLE_FONTSIZE != 0)
			{
				this.txtQuestionTitle.FontSize = (double)this.oQuestion.QDefine.TITLE_FONTSIZE;
			}
			this.SecondsWait = this.oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (this.SecondsWait > 0)
			{
				this.SecondsCountDown = this.SecondsWait;
				this.btnNav.IsEnabled = false;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000033D6 File Offset: 0x000015D6
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤") && !SurveyHelper.RecordIsRunning)
			{
				MessageBox.Show(SurveyMsg.MsgNotRecordStart, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			this.method_1();
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00084038 File Offset: 0x00082238
		private void method_1()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.PageStartTime = SurveyHelper.PageStartTime;
			this.MyNav.RecordFileName = SurveyHelper.RecordFileName;
			this.MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
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
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			SurveyHelper.NavLoad = 0;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00084194 File Offset: 0x00082394
		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.SecondsCountDown == 0)
			{
				this.btnNav.Content = SurveyMsg.MsgbtnNav_Content;
				this.btnNav.IsEnabled = true;
				this.btnNav.Style = (Style)base.FindResource(global::GClass0.smethod_0("Eūɿ͊ѳըٖݰࡺ८੤"));
				this.timer.Stop();
				return;
			}
			this.SecondsCountDown--;
			this.btnNav.Content = this.SecondsCountDown.ToString();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000340F File Offset: 0x0000160F
		private void method_2(object sender, KeyEventArgs e)
		{
			if (this.btnNav.IsEnabled && e.Key == Key.Next)
			{
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x040008A1 RID: 2209
		private string MySurveyId;

		// Token: 0x040008A2 RID: 2210
		private string CurPageId;

		// Token: 0x040008A3 RID: 2211
		private NavBase MyNav = new NavBase();

		// Token: 0x040008A4 RID: 2212
		private QDisplay oQuestion = new QDisplay();

		// Token: 0x040008A5 RID: 2213
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040008A6 RID: 2214
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040008A7 RID: 2215
		private int SecondsWait;

		// Token: 0x040008A8 RID: 2216
		private int SecondsCountDown;
	}
}
