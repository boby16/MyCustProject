using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
	// Token: 0x0200002C RID: 44
	public partial class P_BPTO : Page
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0005522C File Offset: 0x0005342C
		public P_BPTO()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000552E0 File Offset: 0x000534E0
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.btnReturn.Content = global::GClass0.smethod_0("Fœɖ͎") + this.btnReturn.Content;
			this.oQuestion.Init(this.CurPageId, 0, false);
			this.MyNav.GroupLevel = global::GClass0.smethod_0("");
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
			this.oLogicEngine.SurveyID = this.MySurveyId;
			if (this.MyNav.GroupLevel != global::GClass0.smethod_0(""))
			{
				this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			}
			string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
			List<string> list = new List<string>();
			list.Add(global::GClass0.smethod_0(""));
			if (show_LOGIC != global::GClass0.smethod_0(""))
			{
				list = this.oBoldTitle.ParaToList(show_LOGIC, global::GClass0.smethod_0("-Į"));
				if (list.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int i = 0; i < this.oQuestion.QDetails.Count<SurveyDetail>(); i++)
				{
					this.oQuestion.QDetails[i].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[i].CODE_TEXT);
				}
			}
			if (list[0].Trim() != global::GClass0.smethod_0(""))
			{
				string[] array = this.oLogicEngine.aryCode(list[0], ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int j = 0; j < array.Count<string>(); j++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[j].ToString())
						{
							list3.Add(surveyDetail);
							break;
						}
					}
				}
				this.oQuestion.QDetails = list3;
			}
			else if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails();
			}
			this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
			this.Button_FontSize = ((this.oQuestion.QDefine.CONTROL_FONTSIZE == 0) ? SurveyHelper.BtnFontSize : this.oQuestion.QDefine.CONTROL_FONTSIZE);
			this.Button_Height = ((this.oQuestion.QDefine.CONTROL_HEIGHT == 0) ? SurveyHelper.BtnHeight : this.oQuestion.QDefine.CONTROL_HEIGHT);
			this.Button_Width = 280;
			if (this.oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (this.Button_Type == 2 || this.Button_Type == 4)
				{
					this.Button_Width = 440;
				}
			}
			else
			{
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.Button_FontSize == -1)
			{
				this.Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			this.Button_Hide = (this.Button_FontSize < 0);
			this.Button_FontSize = Math.Abs(this.Button_FontSize);
			this.method_4();
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")))
			{
				if (!(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")))
				{
					if (!(navOperation == global::GClass0.smethod_0("NŶɯͱ")))
					{
						goto IL_72A;
					}
					goto IL_72A;
				}
				else
				{
					if (!SurveyHelper.AutoFill)
					{
						goto IL_72A;
					}
					foreach(var child in this.wrapPanel1.Children)
					{
						{
							Border border = (Border)child;
							if (this.IsFinish)
							{
								break;
							}
							this.method_3();
						}
						goto IL_72A;
					}
				}
			}
			this.oQuestion.ReadAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			List<string> list4 = new List<string>();
			foreach (SurveyAnswer surveyAnswer in this.oQuestion.QAnswersRead)
			{
				if (this.oFunc.MID(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ")).Length) == this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ"))
				{
					list4.Add(surveyAnswer.CODE);
				}
			}
			int num = 0;
			foreach (string string_2 in list4)
			{
				num++;
				if (num < list4.Count)
				{
					this.method_1(string_2);
				}
				else
				{
					this.method_2(string_2);
				}
			}
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			IL_72A:
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			this.SecondsWait = this.oQuestion.QCircleADefine.PAGE_COUNT_DOWN;
			if (this.SecondsWait > 0)
			{
				this.SecondsCountDown = this.SecondsWait;
				this.btnNav.Foreground = Brushes.LightGray;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0("2") && this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0("5"))
			{
				this.btnNav.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00055B3C File Offset: 0x00053D3C
		private void method_1(string string_0)
		{
			foreach (Button button in this.listButton)
			{
				string b = button.Name.Substring(2);
				if (string_0 == b)
				{
					this.method_7(button, null);
					break;
				}
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00055BAC File Offset: 0x00053DAC
		private void method_2(string string_0)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			double opacity = 0.2;
			bool flag = false;
			foreach (object obj in this.wrapPanel1.Children)
			{
				foreach (object obj2 in ((WrapPanel)((Border)obj).Child).Children)
				{
					UIElement uielement = (UIElement)obj2;
					if (uielement is Button)
					{
						Button button = (Button)uielement;
						string b = button.Name.Substring(2);
						if (string_0 == b)
						{
							button.Style = style;
							flag = true;
						}
					}
					if (uielement is Image)
					{
						Image image = (Image)uielement;
						string b2 = image.Name.Substring(2);
						if (string_0 == b2)
						{
							image.Opacity = opacity;
							flag = true;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00055D0C File Offset: 0x00053F0C
		private void method_3()
		{
			foreach (object obj in this.wrapPanel1.Children)
			{
				Border border = (Border)obj;
				if (border.Visibility == Visibility.Visible)
				{
					foreach (object obj2 in ((WrapPanel)border.Child).Children)
					{
						UIElement uielement = (UIElement)obj2;
						if (uielement is Button)
						{
							this.method_7((Button)uielement, null);
							return;
						}
					}
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00055DD8 File Offset: 0x00053FD8
		private void method_4()
		{
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleADetails)
			{
				this.matrixButton.Add(new classListBorder());
				this.listCurrent.Add(0);
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Brush borderBrush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
			WrapPanel wrapPanel = this.wrapPanel1;
			wrapPanel.Orientation = ((this.Button_Type == 2 || this.Button_Type == 4) ? Orientation.Vertical : Orientation.Horizontal);
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
			{
				Border border = new Border();
				border.BorderThickness = ((this.oQuestion.QDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("")) ? new Thickness(1.0) : new Thickness(0.0));
				border.BorderBrush = borderBrush;
				wrapPanel.Children.Add(border);
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapPanel2.Orientation = ((this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("2") || this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("5")) ? Orientation.Horizontal : Orientation.Vertical);
				List<string> list = this.oFunc.StringToList(this.oQuestion.QDefine.CONTROL_TOOLTIP, global::GClass0.smethod_0("-"));
				int num = 5;
				int num2 = 5;
				int num3 = 5;
				int num4 = 3;
				if (list.Count == 1)
				{
					num3 = (num4 = (num2 = (num = this.oFunc.StringToInt(list[0]))));
				}
				else if (list.Count == 4)
				{
					num = this.oFunc.StringToInt(list[0]);
					num2 = this.oFunc.StringToInt(list[1]);
					num3 = this.oFunc.StringToInt(list[2]);
					num4 = this.oFunc.StringToInt(list[3]);
				}
				wrapPanel2.Margin = new Thickness((double)num, (double)num2, (double)num3, (double)num4);
				border.Child = wrapPanel2;
				this.matrixButton[surveyDetail2.RANDOM_SET - 1].Borders.Add(border);
				if (this.matrixButton[surveyDetail2.RANDOM_SET - 1].Borders.Count > 1)
				{
					border.Visibility = Visibility.Collapsed;
				}
				Button button = new Button();
				button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail2.CODE;
				button.Content = surveyDetail2.CODE_TEXT;
				button.Tag = surveyDetail2.RANDOM_SET;
				button.Margin = new Thickness(10.0, 10.0, 10.0, 10.0);
				button.Style = style;
				button.Click += this.method_7;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = (double)this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				if (this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0("3") && this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0("5"))
				{
					wrapPanel2.Children.Add(button);
				}
				this.listButton.Add(button);
				string text = this.oLogicEngine.Route(surveyDetail2.EXTEND_1);
				if (text != global::GClass0.smethod_0(""))
				{
					Image image = new Image();
					image.Name = global::GClass0.smethod_0("rŞ") + surveyDetail2.CODE;
					image.Tag = surveyDetail2.RANDOM_SET;
					if (!(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("2")) && !(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("5")))
					{
						image.MinHeight = 46.0;
						image.Width = (double)this.Button_Width;
					}
					else
					{
						image.MinWidth = 46.0;
						image.Height = (double)this.Button_Height;
					}
					image.Stretch = Stretch.Uniform;
					image.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
					try
					{
						string text2 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + text;
						if (this.method_15(text, 1) == global::GClass0.smethod_0("\""))
						{
							text2 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + this.method_16(text, 1, -9999);
						}
						else if (!File.Exists(text2))
						{
							text2 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + text;
						}
						BitmapImage bitmapImage = new BitmapImage();
						bitmapImage.BeginInit();
						bitmapImage.UriSource = new Uri(text2, UriKind.RelativeOrAbsolute);
						bitmapImage.EndInit();
						image.Source = bitmapImage;
						image.MouseLeftButtonUp += new MouseButtonEventHandler(this.method_8);
						image.MouseEnter += this.method_5;
						image.MouseLeave += this.method_6;
						wrapPanel2.Children.Add(image);
						if (this.Button_Hide)
						{
							button.Visibility = Visibility.Collapsed;
						}
						goto IL_65D;
					}
					catch (Exception)
					{
						goto IL_65D;
					}
					goto IL_5D3;
				}
				goto IL_65D;
				IL_603:
				if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("3"))
				{
					wrapPanel2.VerticalAlignment = VerticalAlignment.Bottom;
				}
				if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("5"))
				{
					wrapPanel2.HorizontalAlignment = HorizontalAlignment.Right;
					continue;
				}
				continue;
				IL_5D3:
				if (!(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("5")))
				{
					goto IL_603;
				}
				IL_5F4:
				wrapPanel2.Children.Add(button);
				goto IL_603;
				IL_65D:
				if (!(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("3")))
				{
					goto IL_5D3;
				}
				goto IL_5F4;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00002CC0 File Offset: 0x00000EC0
		private void method_5(object sender, MouseEventArgs e)
		{
			((Image)sender).Opacity = 0.4;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00002CD6 File Offset: 0x00000ED6
		private void method_6(object sender, MouseEventArgs e)
		{
			((Image)sender).Opacity = 1.0;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000564C8 File Offset: 0x000546C8
		private void method_7(object sender, RoutedEventArgs e = null)
		{
			Button button = (Button)sender;
			int int_ = (int)button.Tag;
			string string_ = button.Name.Substring(2);
			this.method_9(int_, string_);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000564FC File Offset: 0x000546FC
		private void method_8(object sender, RoutedEventArgs e = null)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			double opacity = 0.2;
			double opacity2 = 1.0;
			Image image = (Image)sender;
			image.Opacity = opacity;
			int int_ = (int)image.Tag;
			string string_ = image.Name.Substring(2);
			this.method_9(int_, string_);
			image.Opacity = opacity2;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00056580 File Offset: 0x00054780
		private void method_9(int int_0, string string_0)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			double opacity = 1.0;
			foreach (object obj in this.wrapPanel1.Children)
			{
				Border border = (Border)obj;
				if (border.Visibility == Visibility.Visible)
				{
					foreach (object obj2 in ((WrapPanel)border.Child).Children)
					{
						UIElement uielement = (UIElement)obj2;
						if (uielement is Button)
						{
							((Button)uielement).Style = style2;
						}
						if (uielement is Image)
						{
							((Image)uielement).Opacity = opacity;
						}
					}
				}
			}
			foreach (object obj3 in ((WrapPanel)this.matrixButton[int_0 - 1].Borders[this.listCurrent[int_0 - 1]].Child).Children)
			{
				UIElement uielement2 = (UIElement)obj3;
				if (uielement2 is Button)
				{
					this.listAnswerButton.Add((Button)uielement2);
				}
			}
			this.btnReturn.Visibility = Visibility.Visible;
			this.matrixButton[int_0 - 1].Borders[this.listCurrent[int_0 - 1]].Visibility = Visibility.Collapsed;
			this.listHideBorder.Add(this.matrixButton[int_0 - 1].Borders[this.listCurrent[int_0 - 1]]);
			List<int> list = this.listCurrent;
			int index = int_0 - 1;
			int num = list[index];
			list[index] = num + 1;
			if (this.listCurrent[int_0 - 1] < this.matrixButton[int_0 - 1].Borders.Count)
			{
				this.matrixButton[int_0 - 1].Borders[this.listCurrent[int_0 - 1]].Visibility = Visibility.Visible;
				this.listShowBorder.Add(this.matrixButton[int_0 - 1].Borders[this.listCurrent[int_0 - 1]]);
			}
			else if (!(this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0("3")) && !(this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0("5")))
			{
				this.method_13(false);
			}
			else if (this.listAnswerButton.Count >= this.oQuestion.QCircleADetails.Count * this.oQuestion.QCircleBDetails.Count)
			{
				this.method_13(false);
			}
			if ((this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0("3") || this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0("5")) && SurveyHelper.NavOperation != global::GClass0.smethod_0("FŢɡͪ"))
			{
				int num2 = 0;
				Button sender = new Button();
				foreach (object obj4 in this.wrapPanel1.Children)
				{
					Border border2 = (Border)obj4;
					if (border2.Visibility == Visibility.Visible)
					{
						num2++;
						foreach (object obj5 in ((WrapPanel)border2.Child).Children)
						{
							UIElement uielement3 = (UIElement)obj5;
							if (uielement3 is Button)
							{
								sender = (Button)uielement3;
								break;
							}
						}
					}
				}
				if (num2 == 1)
				{
					this.method_7(sender, null);
				}
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00056A00 File Offset: 0x00054C00
		private void btnReturn_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			double opacity = 0.2;
			double opacity2 = 1.0;
			int num = (int)this.listAnswerButton[this.listAnswerButton.Count - 1].Tag;
			if (this.listCurrent[num - 1] < this.oQuestion.QCircleBDetails.Count)
			{
				this.listShowBorder[this.listShowBorder.Count - 1].Visibility = Visibility.Collapsed;
				this.listShowBorder.RemoveAt(this.listShowBorder.Count - 1);
			}
			List<int> list = this.listCurrent;
			int index = num - 1;
			int num2 = list[index];
			list[index] = num2 - 1;
			Border border = this.listHideBorder[this.listHideBorder.Count - 1];
			border.Visibility = Visibility.Visible;
			foreach (object obj in this.wrapPanel1.Children)
			{
				Border border2 = (Border)obj;
				if (border2.Visibility == Visibility.Visible)
				{
					foreach (object obj2 in ((WrapPanel)border2.Child).Children)
					{
						UIElement uielement = (UIElement)obj2;
						if (uielement is Button)
						{
							((Button)uielement).Style = style2;
						}
						if (uielement is Image)
						{
							((Image)uielement).Opacity = opacity2;
						}
					}
				}
			}
			foreach (object obj3 in ((WrapPanel)border.Child).Children)
			{
				UIElement uielement2 = (UIElement)obj3;
				if (uielement2 is Button)
				{
					((Button)uielement2).Style = style;
				}
				if (uielement2 is Image)
				{
					((Image)uielement2).Opacity = opacity;
				}
			}
			this.listHideBorder.RemoveAt(this.listHideBorder.Count - 1);
			this.listAnswerButton.RemoveAt(this.listAnswerButton.Count - 1);
			if (this.listHideBorder.Count == 0)
			{
				this.btnReturn.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00056CB8 File Offset: 0x00054EB8
		private bool method_10()
		{
			return MessageBox.Show(SurveyMsg.MsgBPTO_NotFinish + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00056D08 File Offset: 0x00054F08
		private List<VEAnswer> method_11()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = global::GClass0.smethod_0("");
			List<int> list2 = new List<int>();
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleADetails)
			{
				list2.Add(0);
			}
			int num = 1;
			foreach (Button button in this.listAnswerButton)
			{
				string text = button.Name.Substring(2);
				string text2 = ((int)button.Tag).ToString();
				int num2 = (int)button.Tag - 1;
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ") + num.ToString();
				veanswer.CODE = text;
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					veanswer.CODE
				});
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ł") + text,
					CODE = num.ToString()
				});
				veanswer = new VEAnswer();
				List<int> list3 = list2;
				int index = num2;
				int num3 = list3[index];
				list3[index] = num3 + 1;
				veanswer.QUESTION_NAME = string.Concat(new object[]
				{
					this.oQuestion.QuestionName,
					global::GClass0.smethod_0("]œ"),
					text2,
					global::GClass0.smethod_0("]œ"),
					list2[num2]
				});
				veanswer.CODE = num.ToString();
				list.Add(veanswer);
				num++;
			}
			num = 1;
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleADetails)
			{
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQuestion.CircleAQuestionName + global::GClass0.smethod_0("]œ") + num.ToString(),
					CODE = surveyDetail2.CODE
				});
				int num4 = 1;
				foreach (SurveyDetail surveyDetail3 in this.oQuestion.QCircleBDetails)
				{
					list.Add(new VEAnswer
					{
						QUESTION_NAME = string.Concat(new string[]
						{
							this.oQuestion.CircleBQuestionName,
							global::GClass0.smethod_0("]œ"),
							num.ToString(),
							global::GClass0.smethod_0("]œ"),
							num4.ToString()
						}),
						CODE = surveyDetail3.CODE
					});
					num4++;
				}
				num++;
			}
			SurveyHelper.Answer = this.method_16(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000570D8 File Offset: 0x000552D8
		private void method_12(List<VEAnswer> list_0)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			List<int> list2 = new List<int>();
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleADetails)
			{
				list2.Add(0);
			}
			DateTime now = DateTime.Now;
			int num = 1;
			foreach (Button button in this.listAnswerButton)
			{
				string text = button.Name.Substring(2);
				string text2 = ((int)button.Tag).ToString();
				int num2 = (int)button.Tag - 1;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ") + num.ToString(),
					CODE = text,
					MULTI_ORDER = num,
					BEGIN_DATE = new DateTime?(this.oQuestion.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ł") + text,
					CODE = num.ToString(),
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.oQuestion.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				SurveyAnswer surveyAnswer = new SurveyAnswer();
				List<int> list3 = list2;
				int index = num2;
				int num3 = list3[index];
				list3[index] = num3 + 1;
				surveyAnswer.QUESTION_NAME = string.Concat(new object[]
				{
					this.oQuestion.QuestionName,
					global::GClass0.smethod_0("]œ"),
					text2,
					global::GClass0.smethod_0("]œ"),
					list2[num2]
				});
				surveyAnswer.CODE = num.ToString();
				surveyAnswer.MULTI_ORDER = 0;
				surveyAnswer.BEGIN_DATE = new DateTime?(this.oQuestion.QInitDateTime);
				surveyAnswer.MODIFY_DATE = new DateTime?(now);
				list.Add(surveyAnswer);
				num++;
			}
			num = 1;
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleADetails)
			{
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.oQuestion.CircleAQuestionName + global::GClass0.smethod_0("]œ") + num.ToString(),
					CODE = surveyDetail2.CODE,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.oQuestion.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				int num4 = 1;
				foreach (SurveyDetail surveyDetail3 in this.oQuestion.QCircleBDetails)
				{
					list.Add(new SurveyAnswer
					{
						QUESTION_NAME = string.Concat(new string[]
						{
							this.oQuestion.CircleBQuestionName,
							global::GClass0.smethod_0("]œ"),
							num.ToString(),
							global::GClass0.smethod_0("]œ"),
							num4.ToString()
						}),
						CODE = surveyDetail3.CODE,
						MULTI_ORDER = 0,
						BEGIN_DATE = new DateTime?(this.oQuestion.QInitDateTime),
						MODIFY_DATE = new DateTime?(now)
					});
					num4++;
				}
				num++;
			}
			this.oQuestion.BeforeSave(list);
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00002CEC File Offset: 0x00000EEC
		private void btnNav_Click(object sender = null, RoutedEventArgs e = null)
		{
			this.method_13(true);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00057580 File Offset: 0x00055780
		private void method_13(bool bool_0 = false)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			this.oPageNav.Refresh();
			if (bool_0 && this.method_10())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_11();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.IsFinish = true;
			this.method_12(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0005768C File Offset: 0x0005588C
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

		// Token: 0x060002CE RID: 718 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_14(string string_0, int int_0, int int_1 = -9999)
		{
			int num = int_1;
			if (num == -9999)
			{
				num = int_0;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (int_0 < 0) ? 0 : int_0;
			int num3 = (num2 < num) ? num2 : num;
			int num4 = (num2 < num) ? num : num2;
			int num5 = (num2 > string_0.Length) ? string_0.Length : num2;
			num = ((int_1 > string_0.Length) ? (string_0.Length - 1) : int_1);
			return string_0.Substring(num5, num - num5 + 1);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_15(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_16(string string_0, int int_0, int int_1 = -9999)
		{
			int num = int_1;
			if (num == -9999)
			{
				num = string_0.Length;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (int_0 > string_0.Length) ? string_0.Length : int_0;
			return string_0.Substring(num2, (num2 + num > string_0.Length) ? (string_0.Length - num2) : num);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_17(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00002CF5 File Offset: 0x00000EF5
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x0400055C RID: 1372
		private string MySurveyId;

		// Token: 0x0400055D RID: 1373
		private string CurPageId;

		// Token: 0x0400055E RID: 1374
		private NavBase MyNav = new NavBase();

		// Token: 0x0400055F RID: 1375
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000560 RID: 1376
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000561 RID: 1377
		private UDPX oFunc = new UDPX();

		// Token: 0x04000562 RID: 1378
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000563 RID: 1379
		private QBPTO oQuestion = new QBPTO();

		// Token: 0x04000564 RID: 1380
		private List<Button> listButton = new List<Button>();

		// Token: 0x04000565 RID: 1381
		private List<classListBorder> matrixButton = new List<classListBorder>();

		// Token: 0x04000566 RID: 1382
		private List<int> listCurrent = new List<int>();

		// Token: 0x04000567 RID: 1383
		private List<Border> listHideBorder = new List<Border>();

		// Token: 0x04000568 RID: 1384
		private List<Border> listShowBorder = new List<Border>();

		// Token: 0x04000569 RID: 1385
		private List<Button> listAnswerButton = new List<Button>();

		// Token: 0x0400056A RID: 1386
		private bool IsFinish;

		// Token: 0x0400056B RID: 1387
		private int Button_Type;

		// Token: 0x0400056C RID: 1388
		private int Button_Height;

		// Token: 0x0400056D RID: 1389
		private int Button_Width;

		// Token: 0x0400056E RID: 1390
		private int Button_FontSize;

		// Token: 0x0400056F RID: 1391
		private bool Button_Hide;

		// Token: 0x04000570 RID: 1392
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000571 RID: 1393
		private int SecondsWait;

		// Token: 0x04000572 RID: 1394
		private int SecondsCountDown;

		// Token: 0x04000573 RID: 1395
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;
	}
}
