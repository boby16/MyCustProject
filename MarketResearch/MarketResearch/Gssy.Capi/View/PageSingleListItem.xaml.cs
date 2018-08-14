using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x02000043 RID: 67
	public partial class PageSingleListItem : Page
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00003430 File Offset: 0x00001630
		public PageSingleListItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000842EC File Offset: 0x000824EC
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.oQuestion.Init(this.CurPageId, 0, true);
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
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				this.oLogicEngine.SurveyID = this.MySurveyId;
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list.Add(surveyDetail);
							break;
						}
					}
				}
				list.Sort(new Comparison<SurveyDetail>(PageSingleListItem.Class53.instance.method_0));
				this.oQuestion.QDetails = list;
				this.oQuestion.ResetOtherCode();
			}
			if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails();
			}
			if (this.oQuestion.QDefine.CONTROL_TYPE != 0)
			{
				if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
				{
					this.cmbSelect.Height = (double)this.oQuestion.QDefine.CONTROL_HEIGHT;
				}
				if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
				{
					this.cmbSelect.Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
				}
				if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
				{
					this.cmbSelect.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				}
			}
			this.cmbSelect.ItemsSource = this.oQuestion.QDetails;
			this.cmbSelect.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			if (this.oQuestion.OtherCode != null && this.oQuestion.OtherCode != global::GClass0.smethod_0(""))
			{
				this.txtFill.Visibility = Visibility.Visible;
				this.txtFillTitle.Visibility = Visibility.Visible;
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
			{
				this.cmbSelect.SelectedValue = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00084768 File Offset: 0x00082968
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.cmbSelect.SelectedValue != null && !((string)this.cmbSelect.SelectedValue == global::GClass0.smethod_0("")))
			{
				this.SelectedValue = this.cmbSelect.SelectedValue.ToString();
				this.oQuestion.SelectedCode = this.SelectedValue;
				if (this.oQuestion.OtherCode != null && this.oQuestion.OtherCode != global::GClass0.smethod_0("") && this.SelectedValue == this.oQuestion.OtherCode)
				{
					if (this.txtFill.Text == global::GClass0.smethod_0(""))
					{
						MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.txtFill.Focus();
						return;
					}
					this.oQuestion.FillText = this.txtFill.Text;
				}
				List<VEAnswer> list = new List<VEAnswer>();
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName;
				veanswer.CODE = this.oQuestion.SelectedCode;
				SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.SelectedCode;
				list.Add(veanswer);
				VEAnswer veanswer2 = new VEAnswer();
				veanswer2.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
				veanswer2.CODE = this.oQuestion.FillText;
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer2.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					this.oQuestion.FillText
				});
				list.Add(veanswer2);
				this.oLogicEngine.PageAnswer = list;
				this.oLogicEngine.SurveyID = this.MySurveyId;
				if (!this.oLogicEngine.CheckLogic(this.CurPageId))
				{
					if (this.oLogicEngine.IS_ALLOW_PASS == 0)
					{
						MessageBox.Show(this.oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return;
					}
					if (MessageBox.Show(this.oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return;
					}
				}
				this.oQuestion.BeforeSave();
				this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
				if (SurveyHelper.Debug)
				{
					MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				}
				this.MyNav.PageAnswer = list;
				this.method_1();
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00084A4C File Offset: 0x00082C4C
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

		// Token: 0x060004A3 RID: 1187 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000345F File Offset: 0x0000165F
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x040008AD RID: 2221
		private string MySurveyId;

		// Token: 0x040008AE RID: 2222
		private string CurPageId;

		// Token: 0x040008AF RID: 2223
		private NavBase MyNav = new NavBase();

		// Token: 0x040008B0 RID: 2224
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040008B1 RID: 2225
		private QSingle oQuestion = new QSingle();

		// Token: 0x040008B2 RID: 2226
		private string SelectedValue;

		// Token: 0x020000B4 RID: 180
		[CompilerGenerated]
		[Serializable]
		private sealed class Class53
		{
			// Token: 0x06000789 RID: 1929 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D2D RID: 3373
			public static readonly PageSingleListItem.Class53 instance = new PageSingleListItem.Class53();

			// Token: 0x04000D2E RID: 3374
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
