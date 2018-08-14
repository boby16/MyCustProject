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
	// Token: 0x02000049 RID: 73
	public partial class RelationList : Page
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x0008AACC File Offset: 0x00088CCC
		public RelationList()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0008AB1C File Offset: 0x00088D1C
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.oQuestion.Init(this.CurPageId, 0);
			this.oQSingle1.Init(this.CurPageId, 1, true);
			this.oQSingle2.Init(this.CurPageId, 2, true);
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
			if (this.oQSingle1.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				this.oLogicEngine.SurveyID = this.MySurveyId;
				string[] array = this.oLogicEngine.aryCode(this.oQSingle1.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQSingle1.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list.Add(surveyDetail);
							break;
						}
					}
				}
				list.Sort(new Comparison<SurveyDetail>(RelationList.Class57.instance.method_0));
				this.oQSingle1.QDetails = list;
			}
			this.textBlock1.Text = this.oQSingle1.QDefine.QUESTION_TITLE;
			this.textBlock2.Text = this.oQSingle2.QDefine.QUESTION_TITLE;
			this.cmbSelect1.ItemsSource = this.oQSingle1.QDetails;
			this.cmbSelect1.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect1.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			if (this.oQSingle2.QDefine.PARENT_CODE == global::GClass0.smethod_0(""))
			{
				this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
				this.cmbSelect2.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
				this.cmbSelect2.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			}
			this.cmbSelect1.IsEditable = false;
			this.cmbSelect2.IsEditable = false;
			this.txtOther1.Visibility = Visibility.Hidden;
			this.txtOther2.Visibility = Visibility.Hidden;
			this.txtFillOther1.Visibility = Visibility.Hidden;
			this.txtFillOther2.Visibility = Visibility.Hidden;
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				this.cmbSelect1.SelectedValue = autoFill.SingleDetail(this.oQSingle1.QDefine, this.oQSingle1.QDetails).CODE;
				this.cmbSelect1_SelectionChanged(this.cmbSelect1, null);
				this.cmbSelect2.SelectedValue = autoFill.SingleDetail(this.oQSingle2.QDefine, this.oQSingle2.QDetails).CODE;
				this.cmbSelect2_SelectionChanged(this.cmbSelect2, null);
				if (this.txtFillOther1.IsEnabled)
				{
					this.txtFillOther1.Text = autoFill.CommonOther(this.oQSingle1.QDefine, global::GClass0.smethod_0(""));
				}
				if (this.txtFillOther2.IsEnabled)
				{
					this.txtFillOther2.Text = autoFill.CommonOther(this.oQSingle2.QDefine, global::GClass0.smethod_0(""));
				}
				if (autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
			{
				this.cmbSelect1.SelectedValue = this.oQSingle1.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle1.QuestionName);
				this.cmbSelect2.SelectedValue = this.oQSingle2.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle2.QuestionName);
				this.txtFillOther1.Text = this.oQSingle1.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle1.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"));
				this.txtFillOther2.Text = this.oQSingle2.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle2.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"));
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0008B140 File Offset: 0x00089340
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.cmbSelect1.SelectedValue == null || (string)this.cmbSelect1.SelectedValue == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (this.cmbSelect2.SelectedValue != null && !((string)this.cmbSelect2.SelectedValue == global::GClass0.smethod_0("")))
			{
				this.oQSingle1.FillText = global::GClass0.smethod_0("");
				this.oQSingle2.FillText = global::GClass0.smethod_0("");
				if (this.oQSingle1.OtherCode != null && this.oQSingle1.OtherCode != global::GClass0.smethod_0("") && this.cmbSelect1.SelectedValue.ToString() == this.oQSingle1.OtherCode)
				{
					if (this.txtFillOther1.Text == global::GClass0.smethod_0(""))
					{
						MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.txtFillOther1.Focus();
						return;
					}
					this.oQSingle1.FillText = this.txtFillOther1.Text;
				}
				if (this.oQSingle2.OtherCode != null && this.oQSingle2.OtherCode != global::GClass0.smethod_0("") && this.cmbSelect2.SelectedValue.ToString() == this.oQSingle2.OtherCode)
				{
					if (this.txtFillOther2.Text == global::GClass0.smethod_0(""))
					{
						MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.txtFillOther2.Focus();
						return;
					}
					this.oQSingle2.FillText = this.txtFillOther2.Text;
				}
				this.oQSingle1.SelectedCode = this.cmbSelect1.SelectedValue.ToString();
				this.oQSingle2.SelectedCode = this.cmbSelect2.SelectedValue.ToString();
				List<VEAnswer> list = new List<VEAnswer>();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQSingle1.QuestionName,
					CODE = this.oQSingle1.SelectedCode
				});
				SurveyHelper.Answer = this.oQSingle1.QuestionName + global::GClass0.smethod_0("<") + this.oQSingle1.SelectedCode;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQSingle2.QuestionName,
					CODE = this.oQSingle2.SelectedCode
				});
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0(".ġ"),
					this.oQSingle2.QuestionName,
					global::GClass0.smethod_0("<"),
					this.oQSingle2.SelectedCode
				});
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
				this.oQSingle1.BeforeSave();
				this.oQSingle1.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
				this.oQSingle2.BeforeSave();
				this.oQSingle2.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
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

		// Token: 0x060004E1 RID: 1249 RVA: 0x0008B568 File Offset: 0x00089768
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

		// Token: 0x060004E2 RID: 1250 RVA: 0x0008B6C4 File Offset: 0x000898C4
		private void cmbSelect1_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect1.SelectedValue != null)
			{
				string text = this.cmbSelect1.SelectedValue.ToString();
				if (text != null && text != global::GClass0.smethod_0(""))
				{
					if (this.oQSingle2.QDefine.PARENT_CODE != global::GClass0.smethod_0("") && text != null && text != global::GClass0.smethod_0(""))
					{
						this.oQSingle2.ParentCode = text;
						this.oQSingle2.GetDynamicDetails();
						this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
						this.cmbSelect2.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
						this.cmbSelect2.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
					}
					this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
					this.cmbSelect2.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
					this.cmbSelect2.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
				}
				if (this.oQSingle1.OtherCode != global::GClass0.smethod_0(""))
				{
					if (text == this.oQSingle1.OtherCode)
					{
						this.txtOther1.Visibility = Visibility.Visible;
						this.txtFillOther1.Visibility = Visibility.Visible;
						return;
					}
					this.txtOther1.Visibility = Visibility.Hidden;
					this.txtFillOther1.Visibility = Visibility.Hidden;
				}
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0008B83C File Offset: 0x00089A3C
		private void cmbSelect2_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect2.SelectedValue != null)
			{
				string a = this.cmbSelect2.SelectedValue.ToString();
				if (this.oQSingle2.OtherCode != global::GClass0.smethod_0(""))
				{
					if (a == this.oQSingle2.OtherCode)
					{
						this.txtOther2.Visibility = Visibility.Visible;
						this.txtFillOther2.Visibility = Visibility.Visible;
						return;
					}
					this.txtOther2.Visibility = Visibility.Hidden;
					this.txtFillOther2.Visibility = Visibility.Hidden;
				}
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFillOther2_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFillOther2_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00003603 File Offset: 0x00001803
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQSingle1.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000917 RID: 2327
		private string MySurveyId;

		// Token: 0x04000918 RID: 2328
		private string CurPageId;

		// Token: 0x04000919 RID: 2329
		private NavBase MyNav = new NavBase();

		// Token: 0x0400091A RID: 2330
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400091B RID: 2331
		private QDisplay oQuestion = new QDisplay();

		// Token: 0x0400091C RID: 2332
		private QSingle oQSingle1 = new QSingle();

		// Token: 0x0400091D RID: 2333
		private QSingle oQSingle2 = new QSingle();

		// Token: 0x020000B8 RID: 184
		[CompilerGenerated]
		[Serializable]
		private sealed class Class57
		{
			// Token: 0x06000795 RID: 1941 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D35 RID: 3381
			public static readonly RelationList.Class57 instance = new RelationList.Class57();

			// Token: 0x04000D36 RID: 3382
			public static Comparison<SurveyDetail> Compare;
		}
	}
}
