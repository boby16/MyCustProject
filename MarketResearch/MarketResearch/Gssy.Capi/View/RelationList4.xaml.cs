using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch.View
{
	public partial class RelationList4 : Page
	{
		public RelationList4()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.oQuestion.Init(this.CurPageId, 0);
			this.oQSingle1.Init(this.CurPageId, 1, true);
			this.oQSingle2.Init(this.CurPageId, 2, true);
			this.oQSingle3.Init(this.CurPageId, 3, true);
			this.oQSingle4.Init(this.CurPageId, 4, true);
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
			if (this.oQSingle1.QDefine.LIMIT_LOGIC != "")
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
				list.Sort(new Comparison<SurveyDetail>(RelationList4.Class54.instance.method_0));
				this.oQSingle1.QDetails = list;
			}
			this.textBlock1.Text = this.oQSingle1.QDefine.QUESTION_TITLE;
			this.textBlock2.Text = this.oQSingle2.QDefine.QUESTION_TITLE;
			this.textBlock3.Text = this.oQSingle3.QDefine.QUESTION_TITLE;
			this.textBlock4.Text = this.oQSingle4.QDefine.QUESTION_TITLE;
			this.cmbSelect1.ItemsSource = this.oQSingle1.QDetails;
			this.cmbSelect1.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect1.SelectedValuePath = "CODE";
			if (this.oQSingle2.QDefine.PARENT_CODE == "")
			{
				this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
				this.cmbSelect2.DisplayMemberPath = "CODE_TEXT";
				this.cmbSelect2.SelectedValuePath = "CODE";
			}
			if (this.oQSingle3.QDefine.PARENT_CODE == "")
			{
				this.cmbSelect3.ItemsSource = this.oQSingle3.QDetails;
				this.cmbSelect3.DisplayMemberPath = "CODE_TEXT";
				this.cmbSelect3.SelectedValuePath = "CODE";
			}
			if (this.oQSingle4.QDefine.PARENT_CODE == "")
			{
				this.cmbSelect4.ItemsSource = this.oQSingle4.QDetails;
				this.cmbSelect4.DisplayMemberPath = "CODE_TEXT";
				this.cmbSelect4.SelectedValuePath = "CODE";
			}
			this.cmbSelect1.IsEditable = false;
			this.cmbSelect2.IsEditable = false;
			this.cmbSelect3.IsEditable = false;
			this.cmbSelect4.IsEditable = false;
			this.txtOther1.Visibility = Visibility.Hidden;
			this.txtOther2.Visibility = Visibility.Hidden;
			this.txtOther3.Visibility = Visibility.Hidden;
			this.txtOther4.Visibility = Visibility.Hidden;
			this.txtFillOther1.Visibility = Visibility.Hidden;
			this.txtFillOther2.Visibility = Visibility.Hidden;
			this.txtFillOther3.Visibility = Visibility.Hidden;
			this.txtFillOther4.Visibility = Visibility.Hidden;
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
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
				this.cmbSelect3.SelectedValue = autoFill.SingleDetail(this.oQSingle3.QDefine, this.oQSingle3.QDetails).CODE;
				this.cmbSelect3_SelectionChanged(this.cmbSelect3, null);
				this.cmbSelect4.SelectedValue = autoFill.SingleDetail(this.oQSingle4.QDefine, this.oQSingle4.QDetails).CODE;
				this.cmbSelect4_SelectionChanged(this.cmbSelect4, null);
				if (this.txtFillOther1.IsEnabled)
				{
					this.txtFillOther1.Text = autoFill.CommonOther(this.oQSingle1.QDefine, "");
				}
				if (this.txtFillOther2.IsEnabled)
				{
					this.txtFillOther2.Text = autoFill.CommonOther(this.oQSingle2.QDefine, "");
				}
				if (this.txtFillOther3.IsEnabled)
				{
					this.txtFillOther3.Text = autoFill.CommonOther(this.oQSingle3.QDefine, "");
				}
				if (this.txtFillOther4.IsEnabled)
				{
					this.txtFillOther4.Text = autoFill.CommonOther(this.oQSingle4.QDefine, "");
				}
				if (autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			if (SurveyHelper.NavOperation == "Back")
			{
				this.cmbSelect1.SelectedValue = this.oQSingle1.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle1.QuestionName);
				this.cmbSelect2.SelectedValue = this.oQSingle2.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle2.QuestionName);
				this.cmbSelect3.SelectedValue = this.oQSingle3.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle3.QuestionName);
				this.cmbSelect4.SelectedValue = this.oQSingle3.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle4.QuestionName);
				this.txtFillOther1.Text = this.oQSingle1.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle1.QuestionName + "_OTH");
				this.txtFillOther2.Text = this.oQSingle2.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle2.QuestionName + "_OTH");
				this.txtFillOther3.Text = this.oQSingle3.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle3.QuestionName + "_OTH");
				this.txtFillOther4.Text = this.oQSingle4.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle4.QuestionName + "_OTH");
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.cmbSelect1.SelectedValue == null || (string)this.cmbSelect1.SelectedValue == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (this.cmbSelect2.SelectedValue == null || (string)this.cmbSelect2.SelectedValue == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (this.cmbSelect3.SelectedValue == null || (string)this.cmbSelect3.SelectedValue == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (this.cmbSelect4.SelectedValue != null && !((string)this.cmbSelect4.SelectedValue == ""))
			{
				this.oQSingle1.FillText = "";
				this.oQSingle2.FillText = "";
				this.oQSingle3.FillText = "";
				this.oQSingle4.FillText = "";
				if (this.oQSingle1.OtherCode != null && this.oQSingle1.OtherCode != "" && this.cmbSelect1.SelectedValue.ToString() == this.oQSingle1.OtherCode)
				{
					if (this.txtFillOther1.Text == "")
					{
						MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.txtFillOther1.Focus();
						return;
					}
					this.oQSingle1.FillText = this.txtFillOther1.Text;
				}
				if (this.oQSingle2.OtherCode != null && this.oQSingle2.OtherCode != "" && this.cmbSelect2.SelectedValue.ToString() == this.oQSingle2.OtherCode)
				{
					if (this.txtFillOther2.Text == "")
					{
						MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.txtFillOther2.Focus();
						return;
					}
					this.oQSingle2.FillText = this.txtFillOther2.Text;
				}
				if (this.oQSingle3.OtherCode != null && this.oQSingle3.OtherCode != "" && this.cmbSelect3.SelectedValue.ToString() == this.oQSingle3.OtherCode)
				{
					if (this.txtFillOther3.Text == "")
					{
						MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.txtFillOther3.Focus();
						return;
					}
					this.oQSingle3.FillText = this.txtFillOther3.Text;
				}
				if (this.oQSingle4.OtherCode != null && this.oQSingle4.OtherCode != "" && this.cmbSelect4.SelectedValue.ToString() == this.oQSingle4.OtherCode)
				{
					if (this.txtFillOther4.Text == "")
					{
						MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.txtFillOther4.Focus();
						return;
					}
					this.oQSingle4.FillText = this.txtFillOther4.Text;
				}
				this.oQSingle1.SelectedCode = this.cmbSelect1.SelectedValue.ToString();
				this.oQSingle2.SelectedCode = this.cmbSelect2.SelectedValue.ToString();
				this.oQSingle3.SelectedCode = this.cmbSelect3.SelectedValue.ToString();
				this.oQSingle4.SelectedCode = this.cmbSelect4.SelectedValue.ToString();
				List<VEAnswer> list = new List<VEAnswer>();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQSingle1.QuestionName,
					CODE = this.oQSingle1.SelectedCode
				});
				SurveyHelper.Answer = this.oQSingle1.QuestionName + "=" + this.oQSingle1.SelectedCode;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQSingle2.QuestionName,
					CODE = this.oQSingle2.SelectedCode
				});
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					", ",
					this.oQSingle2.QuestionName,
					"=",
					this.oQSingle2.SelectedCode
				});
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQSingle3.QuestionName,
					CODE = this.oQSingle3.SelectedCode
				});
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					", ",
					this.oQSingle3.QuestionName,
					"=",
					this.oQSingle3.SelectedCode
				});
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQSingle4.QuestionName,
					CODE = this.oQSingle4.SelectedCode
				});
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					", ",
					this.oQSingle4.QuestionName,
					"=",
					this.oQSingle4.SelectedCode
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
				this.oQSingle3.BeforeSave();
				this.oQSingle3.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
				this.oQSingle4.BeforeSave();
				this.oQSingle4.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
				this.method_2(SurveyMsg.DelaySeconds);
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

		private void method_2(int int_0)
		{
			if (int_0 == 0)
			{
				return;
			}
			this.btnNav.IsEnabled = false;
			this.btnNav.Content = "正在保存,请稍候...";
			Logging.Data.WriteLog(this.MySurveyId, this.oQSingle1.QuestionName + "," + this.cmbSelect1.SelectedValue.ToString());
			Logging.Data.WriteLog(this.MySurveyId, this.oQSingle2.QuestionName + "," + this.cmbSelect2.SelectedValue.ToString());
			Logging.Data.WriteLog(this.MySurveyId, this.oQSingle3.QuestionName + "," + this.cmbSelect3.SelectedValue.ToString());
			Thread.Sleep(int_0);
			this.btnNav.Content = "继  续";
			this.btnNav.IsEnabled = true;
		}

		private void cmbSelect1_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect1.SelectedValue != null)
			{
				string text = this.cmbSelect1.SelectedValue.ToString();
				if (this.oQSingle2.QDefine.PARENT_CODE != "" && text != null && text != "")
				{
					this.oQSingle2.ParentCode = text;
					this.oQSingle2.GetDynamicDetails();
					this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
					this.cmbSelect2.DisplayMemberPath = "CODE_TEXT";
					this.cmbSelect2.SelectedValuePath = "CODE";
				}
				if (this.oQSingle1.OtherCode != "")
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

		private void cmbSelect2_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect2.SelectedValue != null)
			{
				string text = this.cmbSelect2.SelectedValue.ToString();
				if (this.oQSingle3.QDefine.PARENT_CODE != "" && text != null && text != "")
				{
					this.oQSingle3.ParentCode = text;
					this.oQSingle3.GetDynamicDetails();
					this.cmbSelect3.ItemsSource = this.oQSingle3.QDetails;
					this.cmbSelect3.DisplayMemberPath = "CODE_TEXT";
					this.cmbSelect3.SelectedValuePath = "CODE";
				}
				if (this.oQSingle2.OtherCode != "")
				{
					if (text == this.oQSingle2.OtherCode)
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

		private void cmbSelect3_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect3.SelectedValue != null)
			{
				string text = this.cmbSelect3.SelectedValue.ToString();
				if (this.oQSingle4.QDefine.PARENT_CODE != "" && text != null && text != "")
				{
					this.oQSingle4.ParentCode = text;
					this.oQSingle4.GetDynamicDetails();
					this.cmbSelect4.ItemsSource = this.oQSingle4.QDetails;
					this.cmbSelect4.DisplayMemberPath = "CODE_TEXT";
					this.cmbSelect4.SelectedValuePath = "CODE";
				}
				if (this.oQSingle3.OtherCode != "")
				{
					if (text == this.oQSingle3.OtherCode)
					{
						this.txtOther3.Visibility = Visibility.Visible;
						this.txtFillOther3.Visibility = Visibility.Visible;
						return;
					}
					this.txtOther3.Visibility = Visibility.Hidden;
					this.txtFillOther3.Visibility = Visibility.Hidden;
				}
			}
		}

		private void cmbSelect4_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect4.SelectedValue != null)
			{
				string a = this.cmbSelect4.SelectedValue.ToString();
				if (this.oQSingle4.OtherCode != "")
				{
					if (a == this.oQSingle4.OtherCode)
					{
						this.txtOther4.Visibility = Visibility.Visible;
						this.txtFillOther4.Visibility = Visibility.Visible;
						return;
					}
					this.txtOther4.Visibility = Visibility.Hidden;
					this.txtFillOther4.Visibility = Visibility.Hidden;
				}
			}
		}

		private void txtFillOther4_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void txtFillOther4_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQSingle1.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private QBase oQuestion = new QBase();

		private QSingle oQSingle1 = new QSingle();

		private QSingle oQSingle2 = new QSingle();

		private QSingle oQSingle3 = new QSingle();

		private QSingle oQSingle4 = new QSingle();

		[CompilerGenerated]
		[Serializable]
		private sealed class Class54
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly RelationList4.Class54 instance = new RelationList4.Class54();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
