using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x02000025 RID: 37
	public partial class PageCallApp : Page
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00046730 File Offset: 0x00044930
		public PageCallApp()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000467A4 File Offset: 0x000449A4
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnCall_Content;
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
			if (SurveyHelper.AutoFill)
			{
				this.AppRunTimes = 1;
				this.btnNav.Content = this.btnNav_Content;
				if (new AutoFill().AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = this.oQuestion.QDefine.QUESTION_CONTENT;
			this.oBoldTitle.SetTextBlock(this.txtDisplayContext, string_, 0, global::GClass0.smethod_0(""), true);
			string text = this.oQuestion.QDefine.CONTROL_TOOLTIP;
			string note = this.oQuestion.QDefine.NOTE;
			if (text == global::GClass0.smethod_0(""))
			{
				text = Environment.CurrentDirectory;
			}
			else if (text.IndexOf(global::GClass0.smethod_0(";")) == -1)
			{
				text = Environment.CurrentDirectory + global::GClass0.smethod_0("]") + text;
			}
			string text2 = text + global::GClass0.smethod_0("]") + note;
			this.txtProgram.Text = text2;
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
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

		// Token: 0x06000231 RID: 561 RVA: 0x00046DB8 File Offset: 0x00044FB8
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content == this.btnCall_Content)
			{
				this.btnNav.Content = SurveyMsg.MsgCallApp_BtnNav;
				this.btnCallApp_Click(sender, e);
				return;
			}
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.AppRunTimes == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNotRunApp, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			if (!this.oLogicEngine.CheckLogic(this.CurPageId))
			{
				if (this.oLogicEngine.IS_ALLOW_PASS == 0)
				{
					MessageBox.Show(this.oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.btnNav.Content = this.btnNav_Content;
					return;
				}
				if (MessageBox.Show(this.oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
				{
					this.btnNav.Content = this.btnNav_Content;
					return;
				}
			}
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00046F14 File Offset: 0x00045114
		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.SecondsCountDown == 0)
			{
				this.timer.Stop();
				this.btnNav.Foreground = Brushes.Black;
				this.btnNav.Content = this.btnCall_Content;
				return;
			}
			this.SecondsCountDown--;
			this.btnNav.Content = this.SecondsCountDown.ToString();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00002B57 File Offset: 0x00000D57
		private void method_1(object sender, KeyEventArgs e)
		{
			if (this.btnNav.IsEnabled && e.Key == Key.Next)
			{
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00046F7C File Offset: 0x0004517C
		private void btnCallApp_Click(object sender, RoutedEventArgs e)
		{
			this.btnNav.Content = this.btnNav_Content;
			if ((string)this.btnCallApp.Content != this.btnCall_Content)
			{
				return;
			}
			this.btnCallApp.Content = SurveyMsg.MsgCallApp_BtnNav;
			string text = this.oQuestion.QDefine.CONTROL_TOOLTIP;
			string note = this.oQuestion.QDefine.NOTE;
			if (text == global::GClass0.smethod_0(""))
			{
				text = Environment.CurrentDirectory;
			}
			else if (text.IndexOf(global::GClass0.smethod_0(";")) == -1)
			{
				text = Environment.CurrentDirectory + global::GClass0.smethod_0("]") + text;
			}
			string text2 = text + global::GClass0.smethod_0("]") + note;
			if (File.Exists(text2))
			{
				this.AppRunTimes++;
				try
				{
					if (!new RarFile().StartProcess(text2, text, global::GClass0.smethod_0(""), ProcessWindowStyle.Maximized, true, 0, false))
					{
						MessageBox.Show(SurveyMsg.MsgPrgNotRun, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					}
				}
				catch (Exception)
				{
					this.btnCallApp.Content = this.btnCall_Content;
					MessageBox.Show(SurveyMsg.MsgRunAppError, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				this.btnCallApp.Content = this.btnCall_Content;
				return;
			}
			this.AppRunTimes++;
			this.btnCallApp.Content = this.btnCall_Content;
			MessageBox.Show(SurveyMsg.MsgPrgNotFound, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00002B78 File Offset: 0x00000D78
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000444 RID: 1092
		private string MySurveyId;

		// Token: 0x04000445 RID: 1093
		private string CurPageId;

		// Token: 0x04000446 RID: 1094
		private NavBase MyNav = new NavBase();

		// Token: 0x04000447 RID: 1095
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000448 RID: 1096
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000449 RID: 1097
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400044A RID: 1098
		private QDisplay oQuestion = new QDisplay();

		// Token: 0x0400044B RID: 1099
		private int AppRunTimes;

		// Token: 0x0400044C RID: 1100
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x0400044D RID: 1101
		private int SecondsWait;

		// Token: 0x0400044E RID: 1102
		private int SecondsCountDown;

		// Token: 0x0400044F RID: 1103
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x04000450 RID: 1104
		private string btnCall_Content = SurveyMsg.MsgCallApp_BtnCall;
	}
}
