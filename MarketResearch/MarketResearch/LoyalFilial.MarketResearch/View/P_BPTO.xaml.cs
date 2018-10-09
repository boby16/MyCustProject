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
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch.View
{
	public partial class P_BPTO : Page
	{
		public P_BPTO()
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
			this.btnReturn.Content = "BPTO" + this.btnReturn.Content;
			this.oQuestion.Init(this.CurPageId, 0, false);
			this.MyNav.GroupLevel = "";
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
			this.oLogicEngine.SurveyID = this.MySurveyId;
			if (this.MyNav.GroupLevel != "")
			{
				this.oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				this.oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				this.oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				this.oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			}
			string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
			List<string> list = new List<string>();
			list.Add("");
			if (show_LOGIC != "")
			{
				list = this.oBoldTitle.ParaToList(show_LOGIC, "//");
				if (list.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int i = 0; i < this.oQuestion.QDetails.Count<SurveyDetail>(); i++)
				{
					this.oQuestion.QDetails[i].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[i].CODE_TEXT);
				}
			}
			if (list[0].Trim() != "")
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
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back"))
			{
				if (!(navOperation == "Normal"))
				{
					if (!(navOperation == "Jump"))
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
				if (this.oFunc.MID(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + "_A").Length) == this.oQuestion.QuestionName + "_A")
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
			SurveyHelper.NavOperation = "Normal";
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
			if (this.oQuestion.QDefine.NOTE != "3" && this.oQuestion.QDefine.NOTE != "4")
			{
				this.btnNav.Visibility = Visibility.Hidden;
			}
		}

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

		private void method_2(string string_0)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
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

		private void method_4()
		{
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleADetails)
			{
				this.matrixButton.Add(new classListBorder());
				this.listCurrent.Add(0);
			}
			Style style = (Style)base.FindResource("UnSelBtnStyle");
			Brush borderBrush = (Brush)base.FindResource("NormalBorderBrush");
			WrapPanel wrapPanel = this.wrapPanel1;
			wrapPanel.Orientation = ((this.Button_Type == 2 || this.Button_Type == 4) ? Orientation.Vertical : Orientation.Horizontal);
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
			{
				Border border = new Border();
				border.BorderThickness = ((this.oQuestion.QDefine.CONTROL_TOOLTIP == "") ? new Thickness(1.0) : new Thickness(0.0));
				border.BorderBrush = borderBrush;
				wrapPanel.Children.Add(border);
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapPanel2.Orientation = ((this.oQuestion.QDefine.CONTROL_MASK == "3" || this.oQuestion.QDefine.CONTROL_MASK == "4") ? Orientation.Horizontal : Orientation.Vertical);
				List<string> list = this.oFunc.StringToList(this.oQuestion.QDefine.CONTROL_TOOLTIP, ",");
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
				button.Name = "b_" + surveyDetail2.CODE;
				button.Content = surveyDetail2.CODE_TEXT;
				button.Tag = surveyDetail2.RANDOM_SET;
				button.Margin = new Thickness(10.0, 10.0, 10.0, 10.0);
				button.Style = style;
				button.Click += this.method_7;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = (double)this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				if (this.oQuestion.QDefine.CONTROL_MASK != "2" && this.oQuestion.QDefine.CONTROL_MASK != "4")
				{
					wrapPanel2.Children.Add(button);
				}
				this.listButton.Add(button);
				string text = this.oLogicEngine.Route(surveyDetail2.EXTEND_1);
				if (text != "")
				{
					Image image = new Image();
					image.Name = "p_" + surveyDetail2.CODE;
					image.Tag = surveyDetail2.RANDOM_SET;
					if (!(this.oQuestion.QDefine.CONTROL_MASK == "3") && !(this.oQuestion.QDefine.CONTROL_MASK == "4"))
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
						string text2 = Environment.CurrentDirectory + "\\Media\\" + text;
						if (this.method_15(text, 1) == "#")
						{
							text2 = "..\\Resources\\Pic\\" + this.method_16(text, 1, -9999);
						}
						else if (!File.Exists(text2))
						{
							text2 = "..\\Resources\\Pic\\" + text;
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
				if (this.oQuestion.QDefine.CONTROL_MASK == "2")
				{
					wrapPanel2.VerticalAlignment = VerticalAlignment.Bottom;
				}
				if (this.oQuestion.QDefine.CONTROL_MASK == "4")
				{
					wrapPanel2.HorizontalAlignment = HorizontalAlignment.Right;
					continue;
				}
				continue;
				IL_5D3:
				if (!(this.oQuestion.QDefine.CONTROL_MASK == "4"))
				{
					goto IL_603;
				}
				IL_5F4:
				wrapPanel2.Children.Add(button);
				goto IL_603;
				IL_65D:
				if (!(this.oQuestion.QDefine.CONTROL_MASK == "2"))
				{
					goto IL_5D3;
				}
				goto IL_5F4;
			}
		}

		private void method_5(object sender, MouseEventArgs e)
		{
			((Image)sender).Opacity = 0.4;
		}

		private void method_6(object sender, MouseEventArgs e)
		{
			((Image)sender).Opacity = 1.0;
		}

		private void method_7(object sender, RoutedEventArgs e = null)
		{
			Button button = (Button)sender;
			int int_ = (int)button.Tag;
			string string_ = button.Name.Substring(2);
			this.method_9(int_, string_);
		}

		private void method_8(object sender, RoutedEventArgs e = null)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			double opacity = 0.2;
			double opacity2 = 1.0;
			Image image = (Image)sender;
			image.Opacity = opacity;
			int int_ = (int)image.Tag;
			string string_ = image.Name.Substring(2);
			this.method_9(int_, string_);
			image.Opacity = opacity2;
		}

		private void method_9(int int_0, string string_0)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
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
			else if (!(this.oQuestion.QDefine.NOTE == "2") && !(this.oQuestion.QDefine.NOTE == "4"))
			{
				this.method_13(false);
			}
			else if (this.listAnswerButton.Count >= this.oQuestion.QCircleADetails.Count * this.oQuestion.QCircleBDetails.Count)
			{
				this.method_13(false);
			}
			if ((this.oQuestion.QDefine.NOTE == "2" || this.oQuestion.QDefine.NOTE == "4") && SurveyHelper.NavOperation != "Back")
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

		private void btnReturn_Click(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
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

		private bool method_10()
		{
			return MessageBox.Show(SurveyMsg.MsgBPTO_NotFinish + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No);
		}

		private List<VEAnswer> method_11()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = "";
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
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + "_A" + num.ToString();
				veanswer.CODE = text;
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer.QUESTION_NAME,
					"=",
					veanswer.CODE
				});
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQuestion.QuestionName + "_C" + text,
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
					"_R",
					text2,
					"_R",
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
					QUESTION_NAME = this.oQuestion.CircleAQuestionName + "_R" + num.ToString(),
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
							"_R",
							num.ToString(),
							"_R",
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
					QUESTION_NAME = this.oQuestion.QuestionName + "_A" + num.ToString(),
					CODE = text,
					MULTI_ORDER = num,
					BEGIN_DATE = new DateTime?(this.oQuestion.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.oQuestion.QuestionName + "_C" + text,
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
					"_R",
					text2,
					"_R",
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
					QUESTION_NAME = this.oQuestion.CircleAQuestionName + "_R" + num.ToString(),
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
							"_R",
							num.ToString(),
							"_R",
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

		private void btnNav_Click(object sender = null, RoutedEventArgs e = null)
		{
			this.method_13(true);
		}

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

		private string method_15(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

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

		private string method_17(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
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

		private UDPX oFunc = new UDPX();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QBPTO oQuestion = new QBPTO();

		private List<Button> listButton = new List<Button>();

		private List<classListBorder> matrixButton = new List<classListBorder>();

		private List<int> listCurrent = new List<int>();

		private List<Border> listHideBorder = new List<Border>();

		private List<Border> listShowBorder = new List<Border>();

		private List<Button> listAnswerButton = new List<Button>();

		private bool IsFinish;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private bool Button_Hide;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;
	}
}
