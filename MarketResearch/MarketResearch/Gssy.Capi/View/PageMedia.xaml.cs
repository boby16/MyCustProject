using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;

namespace Gssy.Capi.View
{
	// Token: 0x0200004C RID: 76
	public partial class PageMedia : Page
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0008C6DC File Offset: 0x0008A8DC
		public PageMedia()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0008C760 File Offset: 0x0008A960
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
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
				this.btnNav_Click(this, e);
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
			if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				this.mediaAngle.Angle = (double)this.oQuestion.QDefine.CONTROL_TYPE;
			}
			if (this.oQuestion.QDefine.CONTROL_TYPE != 90)
			{
				if (this.oQuestion.QDefine.CONTROL_TYPE != 270)
				{
					if (this.oQuestion.QDefine.CONTROL_WIDTH > 0)
					{
						this.mediaElement.Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
					}
					if (this.oQuestion.QDefine.CONTROL_HEIGHT > 0)
					{
						this.mediaElement.Height = (double)this.oQuestion.QDefine.CONTROL_HEIGHT;
						goto IL_6FD;
					}
					goto IL_6FD;
				}
			}
			if (this.oQuestion.QDefine.CONTROL_HEIGHT > 0)
			{
				this.mediaElement.Height = (double)this.oQuestion.QDefine.CONTROL_HEIGHT;
				this.mediaElement.Width = (double)this.oQuestion.QDefine.CONTROL_HEIGHT;
			}
			else if (this.oQuestion.QDefine.CONTROL_WIDTH > 0)
			{
				this.mediaElement.Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
				this.mediaElement.Height = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			IL_6FD:
			if (this.oQuestion.QDefine.CONTROL_HEIGHT == 0 && this.oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				this.mediaElement.Height = this.mediaBorder.ActualHeight;
			}
			string str = Environment.CurrentDirectory + global::GClass0.smethod_0("Zňɡͧѫՠ");
			string text = this.oQuestion.QDefine.CONTROL_TOOLTIP;
			if (this.oFunc.LEFT(text, 2) == global::GClass0.smethod_0("$ź") || text.IndexOf(global::GClass0.smethod_0("Yģ")) > -1)
			{
				text = this.oLogicEngine.strShowText(text, true);
			}
			this.strFullName = str + global::GClass0.smethod_0("]") + text;
			if (File.Exists(this.strFullName))
			{
				this.mediaElement.Source = new Uri(this.strFullName, UriKind.Relative);
				this.playBtn.IsEnabled = true;
				this.StartBtn.IsEnabled = true;
				this.playBtn.Content = SurveyMsg.MsgPause;
				this.mediaElement.ToolTip = SurveyMsg.MsgPauseTip;
				this.mediaElement.Play();
				this.openBtn.Visibility = Visibility.Collapsed;
			}
			else if (!SurveyHelper.AutoFill)
			{
				MessageBox.Show(SurveyMsg.MsgNotMedia, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			this.SecondsWait = this.oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (this.SecondsWait > 0)
			{
				this.SecondsCountDown = this.SecondsWait;
				this.btnNav.Foreground = Brushes.Gray;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0008D098 File Offset: 0x0008B298
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0008D134 File Offset: 0x0008B334
		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.SecondsCountDown == 0)
			{
				this.timer.Stop();
				this.btnNav.Foreground = Brushes.Black;
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.SecondsCountDown--;
			this.btnNav.Content = this.SecondsCountDown.ToString();
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0008D19C File Offset: 0x0008B39C
		private void openBtn_Click(object sender, RoutedEventArgs e)
		{
			ShellContainer initialDirectoryShellContainer = KnownFolders.SampleVideos as ShellContainer;
			CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
			commonOpenFileDialog.InitialDirectoryShellContainer = initialDirectoryShellContainer;
			commonOpenFileDialog.EnsureReadOnly = true;
			commonOpenFileDialog.Filters.Add(new CommonFileDialogFilter(global::GClass0.smethod_0("^Ņɑ̦уխٯݧࡲ"), global::GClass0.smethod_0("/Īɴͯѷ")));
			commonOpenFileDialog.Filters.Add(new CommonFileDialogFilter(global::GClass0.smethod_0("HŞɎ̦уխٯݧࡲ"), global::GClass0.smethod_0("/ĪɢʹѨ")));
			commonOpenFileDialog.Filters.Add(new CommonFileDialogFilter(global::GClass0.smethod_0("DŘȴ̦уխٯݧࡲ"), global::GClass0.smethod_0("/ĪɮͲв")));
			commonOpenFileDialog.Filters.Add(new CommonFileDialogFilter(global::GClass0.smethod_0("^ŉɑ̦уխٯݧࡲ"), global::GClass0.smethod_0("/Īɴͣѷ")));
			if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				this.strFullName = commonOpenFileDialog.FileName;
				this.mediaElement.Source = new Uri(this.strFullName, UriKind.Relative);
				this.playBtn.IsEnabled = true;
				this.StartBtn.IsEnabled = true;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0008D2A0 File Offset: 0x0008B4A0
		private void method_1()
		{
			if (this.playBtn.Content.ToString() == SurveyMsg.MsgContinue)
			{
				this.mediaElement.Play();
				this.playBtn.Content = SurveyMsg.MsgPause;
				this.mediaElement.ToolTip = SurveyMsg.MsgPauseTip;
				return;
			}
			this.mediaElement.Pause();
			this.playBtn.Content = SurveyMsg.MsgContinue;
			this.mediaElement.ToolTip = SurveyMsg.MsgPlayTip;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00003670 File Offset: 0x00001870
		private void playBtn_Click(object sender, RoutedEventArgs e)
		{
			this.method_1();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0008D320 File Offset: 0x0008B520
		private void StartBtn_Click(object sender, RoutedEventArgs e)
		{
			this.mediaElement.Source = new Uri(this.strFullName, UriKind.Relative);
			this.mediaElement.Play();
			this.playBtn.Content = SurveyMsg.MsgPause;
			this.mediaElement.ToolTip = SurveyMsg.MsgPauseTip;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00003670 File Offset: 0x00001870
		private void mediaElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.method_1();
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0008D370 File Offset: 0x0008B570
		private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
		{
			double totalSeconds = this.mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
			this.ProgressSlider.Maximum = totalSeconds;
			this.ProgressSlider.Value = 0.0;
			double num = totalSeconds / this.ProgressSlider.Maximum;
			this.timerMedia.Interval = new TimeSpan(0, 0, 1);
			this.timerMedia.Tick += this.timerMedia_Tick;
			this.timerMedia.Start();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0008D3FC File Offset: 0x0008B5FC
		private void timerMedia_Tick(object sender, EventArgs e)
		{
			this.ProgressSlider.ValueChanged -= this.method_2;
			this.ProgressSlider.Value = this.mediaElement.Position.TotalSeconds;
			this.ProgressSlider.ValueChanged += this.method_2;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000228F File Offset: 0x0000048F
		private void method_2(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00003678 File Offset: 0x00001878
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x0400094A RID: 2378
		private string MySurveyId;

		// Token: 0x0400094B RID: 2379
		private string CurPageId;

		// Token: 0x0400094C RID: 2380
		private NavBase MyNav = new NavBase();

		// Token: 0x0400094D RID: 2381
		private UDPX oFunc = new UDPX();

		// Token: 0x0400094E RID: 2382
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400094F RID: 2383
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000950 RID: 2384
		private QDisplay oQuestion = new QDisplay();

		// Token: 0x04000951 RID: 2385
		private string strFullName = global::GClass0.smethod_0("");

		// Token: 0x04000952 RID: 2386
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000953 RID: 2387
		private int SecondsWait;

		// Token: 0x04000954 RID: 2388
		private int SecondsCountDown;

		// Token: 0x04000955 RID: 2389
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x04000956 RID: 2390
		private DispatcherTimer timerMedia = new DispatcherTimer();
	}
}
