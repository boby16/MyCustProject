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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x0200004D RID: 77
	public partial class PageSingleOther : Page
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x000036B7 File Offset: 0x000018B7
		public PageSingleOther()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0008D638 File Offset: 0x0008B838
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
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				this.chkOther.Content = boldTitle.ReplaceABTitle(this.oQuestion.QDefine.NOTE);
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
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
			{
				this.oQuestion.ReadAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer surveyAnswer in this.oQuestion.QAnswersRead)
				{
					if (surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName && surveyAnswer.CODE != global::GClass0.smethod_0(""))
					{
						this.SelectedValue = surveyAnswer.CODE;
						this.chkOther.IsChecked = new bool?(true);
						this.txtFill.IsEnabled = true;
						this.wrapPanel1.Visibility = Visibility.Visible;
					}
					if (surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"))
					{
						this.txtFill.Text = surveyAnswer.CODE;
					}
				}
				foreach (object obj in this.wrapPanel1.Children)
				{
					Button button = (Button)obj;
					if (button.Tag.ToString() == this.SelectedValue)
					{
						button.Style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
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

		// Token: 0x0600050B RID: 1291 RVA: 0x0008DC44 File Offset: 0x0008BE44
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if (this.chkOther.IsChecked == true)
			{
				string text = this.txtFill.Text;
				text = text.Trim();
				this.oQuestion.FillText = text;
				this.oQuestion.OtherCode = global::GClass0.smethod_0(";Ķ");
				this.oQuestion.SelectedCode = this.SelectedValue;
			}
			else
			{
				this.oQuestion.SelectedCode = global::GClass0.smethod_0("");
				this.oQuestion.FillText = global::GClass0.smethod_0("");
			}
			if (this.chkOther.IsChecked == true)
			{
				if (this.SelectedValue == global::GClass0.smethod_0("") || this.SelectedValue == null)
				{
					MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				if (this.oQuestion.FillText == global::GClass0.smethod_0(""))
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
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0008DF4C File Offset: 0x0008C14C
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

		// Token: 0x0600050D RID: 1293 RVA: 0x0008E0A8 File Offset: 0x0008C2A8
		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.SecondsCountDown == 0)
			{
				this.btnNav.Content = global::GClass0.smethod_0("绤Ģ糬");
				this.btnNav.IsEnabled = true;
				this.btnNav.Style = (Style)base.FindResource(global::GClass0.smethod_0("Eūɿ͊ѳըٖݰࡺ८੤"));
				this.timer.Stop();
				return;
			}
			this.SecondsCountDown--;
			this.btnNav.Content = this.SecondsCountDown.ToString();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000036F1 File Offset: 0x000018F1
		private void chkOther_Checked(object sender, RoutedEventArgs e)
		{
			this.txtFill.IsEnabled = true;
			this.txtFill.Background = Brushes.White;
			this.wrapPanel1.Visibility = Visibility.Visible;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000371B File Offset: 0x0000191B
		private void chkOther_Unchecked(object sender, RoutedEventArgs e)
		{
			this.txtFill.IsEnabled = false;
			this.txtFill.Background = Brushes.LightGray;
			this.wrapPanel1.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0008E130 File Offset: 0x0008C330
		private void method_2()
		{
			int num = 1;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Content = surveyDetail.CODE_TEXT;
				button.Tag = surveyDetail.CODE;
				button.Margin = new Thickness(8.0, 8.0, 8.0, 8.0);
				button.Name = global::GClass0.smethod_0("aŶɯ") + num.ToString();
				button.Style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
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

		// Token: 0x06000511 RID: 1297 RVA: 0x0008E2B8 File Offset: 0x0008C4B8
		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			if (this.SelectedValue == button.Tag.ToString())
			{
				this.SelectedValue = global::GClass0.smethod_0("");
				button.Style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			}
			else
			{
				foreach (object obj in this.wrapPanel1.Children)
				{
					Button button2 = (Button)obj;
					if (button2.Tag.ToString() == this.SelectedValue)
					{
						button2.Style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
					}
				}
				button.Style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
				this.SelectedValue = button.Tag.ToString();
			}
			if (this.oQuestion.OtherCode != null && this.oQuestion.OtherCode != global::GClass0.smethod_0(""))
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

		// Token: 0x06000512 RID: 1298 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00003745 File Offset: 0x00001945
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000964 RID: 2404
		private string MySurveyId;

		// Token: 0x04000965 RID: 2405
		private string CurPageId;

		// Token: 0x04000966 RID: 2406
		private NavBase MyNav = new NavBase();

		// Token: 0x04000967 RID: 2407
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000968 RID: 2408
		private QSingle oQuestion = new QSingle();

		// Token: 0x04000969 RID: 2409
		private string SelectedValue;

		// Token: 0x0400096A RID: 2410
		private int Button_Type;

		// Token: 0x0400096B RID: 2411
		private int Button_Height;

		// Token: 0x0400096C RID: 2412
		private int Button_Width;

		// Token: 0x0400096D RID: 2413
		private int Button_FontSize;

		// Token: 0x0400096E RID: 2414
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x0400096F RID: 2415
		private int SecondsWait;

		// Token: 0x04000970 RID: 2416
		private int SecondsCountDown;

		// Token: 0x020000B9 RID: 185
		[CompilerGenerated]
		[Serializable]
		private sealed class Class58
		{
			// Token: 0x06000798 RID: 1944 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D37 RID: 3383
			public static readonly PageSingleOther.Class58 instance = new PageSingleOther.Class58();

			// Token: 0x04000D38 RID: 3384
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
