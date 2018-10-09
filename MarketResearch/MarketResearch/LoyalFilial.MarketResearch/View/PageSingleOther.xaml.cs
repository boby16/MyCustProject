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
using System.Windows.Threading;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch.View
{
	public partial class PageSingleOther : Page
	{
		public PageSingleOther()
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
			if (this.oQuestion.QDefine.NOTE != "")
			{
				this.chkOther.Content = boldTitle.ReplaceABTitle(this.oQuestion.QDefine.NOTE);
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
				list.Sort(new Comparison<SurveyDetail>(PageSingleOther.Class58.instance.method_0));
				this.oQuestion.QDetails = list;
				this.oQuestion.ResetOtherCode();
			}
			if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails();
			}
			if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
				this.Button_Height = this.oQuestion.QDefine.CONTROL_HEIGHT;
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
				this.Button_FontSize = this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			this.wrapPanel1.Visibility = Visibility.Hidden;
			this.method_2();
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.NavOperation == "Back")
			{
				this.oQuestion.ReadAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer surveyAnswer in this.oQuestion.QAnswersRead)
				{
					if (surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName && surveyAnswer.CODE != "")
					{
						this.SelectedValue = surveyAnswer.CODE;
						this.chkOther.IsChecked = new bool?(true);
						this.txtFill.IsEnabled = true;
						this.wrapPanel1.Visibility = Visibility.Visible;
					}
					if (surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + "_OTH")
					{
						this.txtFill.Text = surveyAnswer.CODE;
					}
				}
				foreach (object obj in this.wrapPanel1.Children)
				{
					Button button = (Button)obj;
					if (button.Tag.ToString() == this.SelectedValue)
					{
						button.Style = (Style)base.FindResource("SelBtnStyle");
					}
				}
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
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

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.chkOther.IsChecked == true)
			{
				string text = this.txtFill.Text;
				text = text.Trim();
				this.oQuestion.FillText = text;
				this.oQuestion.OtherCode = "97";
				this.oQuestion.SelectedCode = this.SelectedValue;
			}
			else
			{
				this.oQuestion.SelectedCode = "";
				this.oQuestion.FillText = "";
			}
			if (this.chkOther.IsChecked == true)
			{
				if (this.SelectedValue == "" || this.SelectedValue == null)
				{
					MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				if (this.oQuestion.FillText == "")
				{
					MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill.Focus();
					return;
				}
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

		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.SecondsCountDown == 0)
			{
				this.btnNav.Content = "继 续";
				this.btnNav.IsEnabled = true;
				this.btnNav.Style = (Style)base.FindResource("NavBtnStyle");
				this.timer.Stop();
				return;
			}
			this.SecondsCountDown--;
			this.btnNav.Content = this.SecondsCountDown.ToString();
		}

		private void chkOther_Checked(object sender, RoutedEventArgs e)
		{
			this.txtFill.IsEnabled = true;
			this.txtFill.Background = Brushes.White;
			this.wrapPanel1.Visibility = Visibility.Visible;
		}

		private void chkOther_Unchecked(object sender, RoutedEventArgs e)
		{
			this.txtFill.IsEnabled = false;
			this.txtFill.Background = Brushes.LightGray;
			this.wrapPanel1.Visibility = Visibility.Hidden;
		}

		private void method_2()
		{
			int num = 1;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Content = surveyDetail.CODE_TEXT;
				button.Tag = surveyDetail.CODE;
				button.Margin = new Thickness(8.0, 8.0, 8.0, 8.0);
				button.Name ="btn" + num.ToString();
				button.Style = (Style)base.FindResource("UnSelBtnStyle");
				button.Click += this.method_3;
				button.FontSize = 28.0;
				button.Width = 260.0;
				button.Height = 50.0;
				if (this.Button_Type != 0)
				{
					if (this.Button_FontSize != 0)
					{
						button.FontSize = (double)this.Button_FontSize;
					}
					if (this.Button_Width != 0)
					{
						button.Width = (double)this.Button_Width;
					}
					if (this.Button_Height != 0)
					{
						button.Height = (double)this.Button_Height;
					}
				}
				this.wrapPanel1.Children.Add(button);
				num++;
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			if (this.SelectedValue == button.Tag.ToString())
			{
				this.SelectedValue = "";
				button.Style = (Style)base.FindResource("UnSelBtnStyle");
			}
			else
			{
				foreach (object obj in this.wrapPanel1.Children)
				{
					Button button2 = (Button)obj;
					if (button2.Tag.ToString() == this.SelectedValue)
					{
						button2.Style = (Style)base.FindResource("UnSelBtnStyle");
					}
				}
				button.Style = (Style)base.FindResource("SelBtnStyle");
				this.SelectedValue = button.Tag.ToString();
			}
			if (this.oQuestion.OtherCode != null && this.oQuestion.OtherCode != "")
			{
				if (this.SelectedValue == this.oQuestion.OtherCode)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
					return;
				}
				this.txtFill.IsEnabled = false;
				this.txtFill.Background = Brushes.LightGray;
			}
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

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class58
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly PageSingleOther.Class58 instance = new PageSingleOther.Class58();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
