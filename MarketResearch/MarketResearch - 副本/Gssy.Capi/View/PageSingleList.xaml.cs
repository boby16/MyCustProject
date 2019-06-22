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
	public partial class PageSingleList : Page
	{
		public PageSingleList()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.oQuestion.Init(this.CurPageId, 0, true);
			string question_TITLE = this.oQuestion.QDefine.QUESTION_TITLE;
			this.txtQuestionTitle.Text = "";
			BoldTitle boldTitle = new BoldTitle();
			boldTitle.SpanTitle(this.MySurveyId, question_TITLE, SurveyHelper.CircleACode, SurveyHelper.CircleBCode);
			foreach (classHtmlText classHtmlText in boldTitle.lSpan)
			{
				if (classHtmlText.TitleTextType == "<B>")
				{
					Span span = new Span();
					span.Inlines.Add(new Run(classHtmlText.TitleText));
					span.Foreground = (Brush)base.FindResource("PressedBrush");
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
			if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
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
				list.Sort(new Comparison<SurveyDetail>(PageSingleList.Class55.instance.method_0));
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
			this.cmbSelect.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect.SelectedValuePath = "CODE";
			if (this.oQuestion.OtherCode != null && this.oQuestion.OtherCode != "")
			{
				this.txtFill.Visibility = Visibility.Visible;
				this.txtFillTitle.Visibility = Visibility.Visible;
			}
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.NavOperation == "Back")
			{
				this.SelectedValue = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
				this.txtFill.Text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + "_OTH");
				this.cmbSelect.SelectedValue = this.SelectedValue;
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.cmbSelect.SelectedValue != null && !((string)this.cmbSelect.SelectedValue == ""))
			{
				this.SelectedValue = this.cmbSelect.SelectedValue.ToString();
				this.oQuestion.SelectedCode = this.SelectedValue;
				if (this.oQuestion.OtherCode != null && this.oQuestion.OtherCode != "" && this.SelectedValue == this.oQuestion.OtherCode)
				{
					if (this.txtFill.Text == "")
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
				SurveyHelper.Answer = this.oQuestion.QuestionName + "=" + this.oQuestion.SelectedCode;
				list.Add(veanswer);
				VEAnswer veanswer2 = new VEAnswer();
				veanswer2.QUESTION_NAME = this.oQuestion.QuestionName + "_OTH";
				veanswer2.CODE = this.oQuestion.FillText;
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer2.QUESTION_NAME,
					"=",
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
			string uriString = string.Format("pack://application:,,,/View/{0}.xaml", text);
			if (text.Substring(0, 1) == "A")
			{
				uriString = string.Format("pack://application:,,,/ViewProject/{0}.xaml", text);
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
			SurveyHelper.NavOperation = "Normal";
			SurveyHelper.NavLoad = 0;
		}

		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private QSingle oQuestion = new QSingle();

		private string SelectedValue;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class55
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly PageSingleList.Class55 instance = new PageSingleList.Class55();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
