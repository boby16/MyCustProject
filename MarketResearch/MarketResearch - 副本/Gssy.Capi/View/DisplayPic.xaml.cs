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
	public partial class DisplayPic : Page
	{
		public DisplayPic()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0);
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel != "")
			{
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				if (this.MyNav.GroupLevel == "B")
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
				if (this.MyNav.GroupLevel == "B")
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
				SurveyHelper.CircleACode = "";
				SurveyHelper.CircleACodeText = "";
				SurveyHelper.CircleACurrent = 0;
				SurveyHelper.CircleACount = 0;
				SurveyHelper.CircleBCode = "";
				SurveyHelper.CircleBCodeText = "";
				SurveyHelper.CircleBCurrent = 0;
				SurveyHelper.CircleBCount = 0;
				this.MyNav.GroupCodeA = "";
				this.MyNav.CircleACurrent = 0;
				this.MyNav.CircleACount = 0;
				this.MyNav.GroupCodeB = "";
				this.MyNav.CircleBCurrent = 0;
				this.MyNav.CircleBCount = 0;
			}
			this.oLogicEngine.SurveyID = this.MySurveyId;
			if (this.MyNav.GroupLevel != "")
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
			List<string> list2 = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list2.Count > 1) ? list2[1] : "");
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != "")
			{
				string text = "";
				if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != "")
				{
					text = this.oLogicEngine.Route(this.oQuestion.QDefine.CONTROL_TOOLTIP);
				}
				else if (this.oQuestion.QDefine.GROUP_LEVEL != "")
				{
					this.oQuestion.InitCircle();
					string text2 = "";
					if (this.MyNav.GroupLevel == "A")
					{
						text2 = this.MyNav.CircleACode;
					}
					if (this.MyNav.GroupLevel == "B")
					{
						text2 = this.MyNav.CircleBCode;
					}
					if (text2 != "")
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
				string text3 = Environment.CurrentDirectory + "\\Media\\" + text;
				if (this.oFunc.LEFT(this.oQuestion.QDefine.CONTROL_TOOLTIP, 1) == "#")
				{
					text3 = "..\\Resources\\Pic\\" + this.oFunc.MID(text, 1, -9999);
				}
				else if (!File.Exists(text3))
				{
					text3 = "..\\Resources\\Pic\\" + text;
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
				if (a == "L")
				{
					text4 = this.oFunc.MID(text4, 1, -9999);
					this.oBoldTitle.SetTextBlock(this.txtLeftTitle, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, text4, true);
					this.oBoldTitle.SetTextBlock(this.txtRightTitle, "", 0, "", true);
				}
				else
				{
					if (a == "R")
					{
						text4 = this.oFunc.MID(text4, 1, -9999);
					}
					this.oBoldTitle.SetTextBlock(this.txtRightTitle, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, text4, true);
					this.oBoldTitle.SetTextBlock(this.txtLeftTitle, "", 0, "", true);
				}
			}
			else
			{
				string_ = this.oQuestion.QDefine.QUESTION_CONTENT;
				string text5 = this.oQuestion.QDefine.CONTROL_MASK;
				string a2 = this.oFunc.LEFT(text5, 1).ToUpper();
				if (a2 == "L" || a2 == "R")
				{
					text5 = this.oFunc.MID(text5, 1, -9999);
				}
				TextBlock textBlock = new TextBlock();
				textBlock.HorizontalAlignment = HorizontalAlignment.Center;
				textBlock.Style = (Style)base.FindResource("TitleTextStyle");
				textBlock.FontSize = 32.0;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.SetValue(Grid.ColumnProperty, 1);
				textBlock.SetValue(Grid.RowProperty, 2);
				this.gridContent.Children.Add(textBlock);
				this.oBoldTitle.SetTextBlock(textBlock, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, text5, true);
				this.oBoldTitle.SetTextBlock(this.txtLeftTitle, "", 0, "", true);
				this.oBoldTitle.SetTextBlock(this.txtRightTitle, "", 0, "", true);
			}
			string_ = this.oQuestion.QDefine.NOTE;
			this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, "", true);
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
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

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QDisplay oQuestion = new QDisplay();

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;
	}
}
