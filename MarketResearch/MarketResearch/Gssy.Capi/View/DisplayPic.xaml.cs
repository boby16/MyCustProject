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
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x0200000D RID: 13
	public partial class DisplayPic : Page
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00009F84 File Offset: 0x00008184
		public DisplayPic()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00009FF8 File Offset: 0x000081F8
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
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list2.Count > 1) ? list2[1] : global::GClass0.smethod_0(""));
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
			{
				string text = global::GClass0.smethod_0("");
				if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
				{
					text = this.oLogicEngine.Route(this.oQuestion.QDefine.CONTROL_TOOLTIP);
				}
				else if (this.oQuestion.QDefine.GROUP_LEVEL != global::GClass0.smethod_0(""))
				{
					this.oQuestion.InitCircle();
					string text2 = global::GClass0.smethod_0("");
					if (this.MyNav.GroupLevel == global::GClass0.smethod_0("@"))
					{
						text2 = this.MyNav.CircleACode;
					}
					if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
					{
						text2 = this.MyNav.CircleBCode;
					}
					if (text2 != global::GClass0.smethod_0(""))
					{
						foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
						{
							if (surveyDetail.CODE == text2)
							{
								text = this.oLogicEngine.Route(surveyDetail.EXTEND_1);
								break;
							}
						}
					}
				}
				string text3 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + text;
				if (this.oFunc.LEFT(this.oQuestion.QDefine.CONTROL_TOOLTIP, 1) == global::GClass0.smethod_0("\""))
				{
					text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + this.oFunc.MID(text, 1, -9999);
				}
				else if (!File.Exists(text3))
				{
					text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + text;
				}
				Image image = new Image();
				image.MinHeight = (double)((this.oQuestion.QDefine.CONTROL_HEIGHT == 0) ? 100 : this.oQuestion.QDefine.CONTROL_HEIGHT);
				if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
				{
					image.Height = (double)this.oQuestion.QDefine.CONTROL_HEIGHT;
				}
				if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
				{
					image.Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
				}
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 5.0, 0.0, 10.0);
				image.SetValue(Grid.ColumnProperty, 1);
				image.SetValue(Grid.RowProperty, 2);
				image.HorizontalAlignment = HorizontalAlignment.Center;
				image.VerticalAlignment = VerticalAlignment.Center;
				if (this.oQuestion.QDefine.CONTROL_TYPE == 1)
				{
					image.HorizontalAlignment = HorizontalAlignment.Left;
				}
				else if (this.oQuestion.QDefine.CONTROL_TYPE == 2)
				{
					image.HorizontalAlignment = HorizontalAlignment.Right;
				}
				else if (this.oQuestion.QDefine.CONTROL_TYPE == 3)
				{
					image.VerticalAlignment = VerticalAlignment.Top;
				}
				else if (this.oQuestion.QDefine.CONTROL_TYPE == 4)
				{
					image.VerticalAlignment = VerticalAlignment.Bottom;
				}
				else if (this.oQuestion.QDefine.CONTROL_TYPE == 13)
				{
					image.HorizontalAlignment = HorizontalAlignment.Left;
					image.VerticalAlignment = VerticalAlignment.Top;
				}
				else if (this.oQuestion.QDefine.CONTROL_TYPE == 14)
				{
					image.HorizontalAlignment = HorizontalAlignment.Left;
					image.VerticalAlignment = VerticalAlignment.Bottom;
				}
				else if (this.oQuestion.QDefine.CONTROL_TYPE == 23)
				{
					image.HorizontalAlignment = HorizontalAlignment.Right;
					image.VerticalAlignment = VerticalAlignment.Top;
				}
				else if (this.oQuestion.QDefine.CONTROL_TYPE == 24)
				{
					image.HorizontalAlignment = HorizontalAlignment.Right;
					image.VerticalAlignment = VerticalAlignment.Bottom;
				}
				try
				{
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.UriSource = new Uri(text3, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					this.gridContent.Children.Add(image);
				}
				catch (Exception)
				{
				}
				string_ = this.oQuestion.QDefine.QUESTION_CONTENT;
				string text4 = this.oQuestion.QDefine.CONTROL_MASK;
				string a = this.oFunc.LEFT(text4, 1).ToUpper();
				if (a == global::GClass0.smethod_0("M"))
				{
					text4 = this.oFunc.MID(text4, 1, -9999);
					this.oBoldTitle.SetTextBlock(this.txtLeftTitle, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, text4, true);
					this.oBoldTitle.SetTextBlock(this.txtRightTitle, global::GClass0.smethod_0(""), 0, global::GClass0.smethod_0(""), true);
				}
				else
				{
					if (a == global::GClass0.smethod_0("S"))
					{
						text4 = this.oFunc.MID(text4, 1, -9999);
					}
					this.oBoldTitle.SetTextBlock(this.txtRightTitle, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, text4, true);
					this.oBoldTitle.SetTextBlock(this.txtLeftTitle, global::GClass0.smethod_0(""), 0, global::GClass0.smethod_0(""), true);
				}
			}
			else
			{
				string_ = this.oQuestion.QDefine.QUESTION_CONTENT;
				string text5 = this.oQuestion.QDefine.CONTROL_MASK;
				string a2 = this.oFunc.LEFT(text5, 1).ToUpper();
				if (a2 == global::GClass0.smethod_0("M") || a2 == global::GClass0.smethod_0("S"))
				{
					text5 = this.oFunc.MID(text5, 1, -9999);
				}
				TextBlock textBlock = new TextBlock();
				textBlock.HorizontalAlignment = HorizontalAlignment.Center;
				textBlock.Style = (Style)base.FindResource(global::GClass0.smethod_0("ZŤɸͧѯ՝٭ݿࡲॖੰ୺౮൤"));
				textBlock.FontSize = 32.0;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.SetValue(Grid.ColumnProperty, 1);
				textBlock.SetValue(Grid.RowProperty, 2);
				this.gridContent.Children.Add(textBlock);
				this.oBoldTitle.SetTextBlock(textBlock, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, text5, true);
				this.oBoldTitle.SetTextBlock(this.txtLeftTitle, global::GClass0.smethod_0(""), 0, global::GClass0.smethod_0(""), true);
				this.oBoldTitle.SetTextBlock(this.txtRightTitle, global::GClass0.smethod_0(""), 0, global::GClass0.smethod_0(""), true);
			}
			string_ = this.oQuestion.QDefine.NOTE;
			this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, global::GClass0.smethod_0(""), true);
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

		// Token: 0x0600007A RID: 122 RVA: 0x0000246E File Offset: 0x0000066E
		private void method_1(object sender, KeyEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			if (e.Key == Key.Next)
			{
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000ACBC File Offset: 0x00008EBC
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

		// Token: 0x0600007C RID: 124 RVA: 0x0000AD58 File Offset: 0x00008F58
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

		// Token: 0x0600007D RID: 125 RVA: 0x000024A0 File Offset: 0x000006A0
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000096 RID: 150
		private string MySurveyId;

		// Token: 0x04000097 RID: 151
		private string CurPageId;

		// Token: 0x04000098 RID: 152
		private NavBase MyNav = new NavBase();

		// Token: 0x04000099 RID: 153
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400009A RID: 154
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400009B RID: 155
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400009C RID: 156
		private UDPX oFunc = new UDPX();

		// Token: 0x0400009D RID: 157
		private QDisplay oQuestion = new QDisplay();

		// Token: 0x0400009E RID: 158
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x0400009F RID: 159
		private int SecondsWait;

		// Token: 0x040000A0 RID: 160
		private int SecondsCountDown;

		// Token: 0x040000A1 RID: 161
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;
	}
}
