using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.View
{
	public partial class P_RankBrand : Page
	{
		public P_RankBrand()
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
			this.oQuestion.Init(this.CurPageId, 0, false);
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel == "B")
			{
				this.MyNav.GroupLevel = "A";
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				this.MyNav.GetCircleInfo(this.MySurveyId);
				this.oQuestion.QuestionName = this.oQuestion.QuestionName + this.MyNav.QName_Add;
				new List<VEAnswer>().Add(new VEAnswer
				{
					QUESTION_NAME = this.MyNav.GroupCodeA,
					CODE = this.MyNav.CircleACode,
					CODE_TEXT = this.MyNav.CircleCodeTextA
				});
				SurveyHelper.CircleACode = this.MyNav.CircleACode;
				SurveyHelper.CircleACodeText = this.MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
				SurveyHelper.CircleACount = this.MyNav.CircleACount;
			}
			else
			{
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
			}
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
			if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list3.Add(surveyDetail);
							break;
						}
					}
				}
				if (this.oQuestion.QDefine.SHOW_LOGIC == "" && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list3.Sort(new Comparison<SurveyDetail>(P_RankBrand.Class38.instance.method_0));
				}
				this.oQuestion.QDetails = list3;
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
				}
			}
			if (list[0].Trim() != "")
			{
				string[] array2 = this.oLogicEngine.aryCode(list[0], ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count<string>(); k++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						if (surveyDetail2.CODE == array2[k].ToString())
						{
							list4.Add(surveyDetail2);
							break;
						}
					}
				}
				this.oQuestion.QDetails = list4;
			}
			else if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails(1);
			}
			if (this.oQuestion.QCircleDefine.LIMIT_LOGIC != "")
			{
				string[] array3 = this.oLogicEngine.aryCode(this.oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count<string>(); l++)
				{
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail3.CODE == array3[l].ToString())
						{
							list5.Add(surveyDetail3);
							break;
						}
					}
				}
				list5.Sort(new Comparison<SurveyDetail>(P_RankBrand.Class38.instance.method_1));
				this.oQuestion.QCircleDetails = list5;
			}
			if (this.oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int m = 0; m < this.oQuestion.QCircleDetails.Count<SurveyDetail>(); m++)
				{
					this.oQuestion.QCircleDetails[m].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QCircleDetails[m].CODE_TEXT);
				}
			}
			this.Button_Width = SurveyHelper.BtnWidth;
			this.Button_Height = SurveyHelper.BtnHeight;
			this.Button_FontSize = SurveyHelper.BtnFontSize;
			this.Text_Width = ((this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == "H") ? 100 : SurveyHelper.BtnWidth);
			this.Text_Height = SurveyHelper.BtnHeight;
			this.Text_FontSize = SurveyHelper.BtnFontSize;
			if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				this.Button_Height = this.oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				this.Button_FontSize = this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_HEIGHT != 0)
			{
				this.Text_Height = this.oQuestion.QCircleDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_WIDTH != 0)
			{
				this.Text_Width = this.oQuestion.QCircleDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE != 0)
			{
				this.Text_FontSize = this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
			}
			this.method_2();
			if (this.oQuestion.QDefine.NOTE != "")
			{
				string_ = this.oQuestion.QDefine.NOTE;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore, string_, 0, "", true);
				if (list2.Count > 1)
				{
					string_ = list2[1];
					this.oBoldTitle.SetTextBlock(this.txtAfter, string_, 0, "", true);
				}
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				foreach (Button sender2 in this.listButton)
				{
					this.method_4(sender2, new RoutedEventArgs());
				}
				if (this.listButton.Count > 0 && autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			Style style = (Style)base.FindResource("SelBtnStyle");
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back") && !(navOperation == "Normal"))
			{
				navOperation = "Jump";
			}
			this.SecondsWait = this.oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (this.SecondsWait > 0)
			{
				this.SecondsCountDown = this.SecondsWait;
				this.btnNav.Foreground = Brushes.LightGray;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
			this.PageLoaded = true;
		}

		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				WrapPanel wrapPanel = this.wrapPanel1;
				this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
				if (this.Button_Type == 0)
				{
					if (this.scrollmain.ComputedVerticalScrollBarVisibility == Visibility.Collapsed)
					{
						wrapPanel.Orientation = Orientation.Vertical;
						this.Button_Type = 2;
					}
					else
					{
						wrapPanel.Orientation = Orientation.Horizontal;
						this.Button_Type = 1;
					}
				}
				else
				{
					wrapPanel.Orientation = ((this.Button_Type == 2) ? Orientation.Vertical : Orientation.Horizontal);
				}
				if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != "")
				{
					int num = this.method_13(this.oQuestion.QDefine.CONTROL_TOOLTIP);
					if (num == 1)
					{
						this.BrandArea.Height = GridLength.Auto;
					}
					else if (num > 1)
					{
						this.BrandArea.Height = new GridLength((double)num);
					}
				}
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded = false;
			}
		}

		private void method_2()
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Style style3 = (Style)base.FindResource("ContentMediumStyle");
			Brush brush = (Brush)base.FindResource("NormalBorderBrush");
			Brush brush2 = (Brush)base.FindResource("PressedBrush");
			Brush brush3 = (Brush)new BrushConverter().ConvertFromString("White");
			WrapPanel wrapPanel = this.wrapPanel2;
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				string code = surveyDetail.CODE;
				string code_TEXT = surveyDetail.CODE_TEXT;
				Button button = new Button();
				button.Content = code_TEXT;
				button.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
				button.Style = style2;
				button.Tag = code;
				button.Click += this.method_4;
				button.FontSize = (double)this.Button_FontSize;
				button.MinHeight = (double)this.Button_Height;
				button.MinWidth = (double)this.Button_Width;
				wrapPanel.Children.Add(button);
				this.btnBrands.Add(code, button);
				this.listButton.Add(button);
				num++;
			}
			wrapPanel = this.wrapPanel1;
			bool flag = true;
			num = 0;
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleDetails)
			{
				string code2 = surveyDetail2.CODE;
				string code_TEXT2 = surveyDetail2.CODE_TEXT;
				string text = "";
				string content = "";
				if (SurveyHelper.NavOperation == "Back")
				{
					string string_ = this.oQuestion.QuestionName + "_R" + code2;
					text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_);
					if (text != "")
					{
						if (!this.btnBrands.ContainsKey(text))
						{
							text = "";
						}
						if (text != "")
						{
							content = (string)this.btnBrands[text].Content;
						}
					}
				}
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapPanel2.Orientation = ((this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == "H") ? Orientation.Horizontal : Orientation.Vertical);
				wrapPanel2.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
				wrapPanel2.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel2.HorizontalAlignment = HorizontalAlignment.Center;
				wrapPanel.Children.Add(wrapPanel2);
				WrapPanel wrapPanel3 = new WrapPanel();
				wrapPanel3.VerticalAlignment = VerticalAlignment.Center;
				if (this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == "H")
				{
					wrapPanel3.HorizontalAlignment = HorizontalAlignment.Right;
				}
				wrapPanel3.Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
				wrapPanel3.MinWidth = (double)this.Text_Width;
				wrapPanel2.Children.Add(wrapPanel3);
				TextBlock textBlock = new TextBlock();
				textBlock.Text = code_TEXT2;
				textBlock.Style = style3;
				textBlock.Foreground = ((text == "") ? brush3 : brush2);
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.HorizontalAlignment = HorizontalAlignment.Right;
				wrapPanel3.Children.Add(textBlock);
				this.txtOrders.Add(textBlock);
				Button button2 = new Button();
				button2.Content = content;
				button2.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				button2.Style = style2;
				button2.Tag = num;
				button2.Click += this.method_3;
				button2.FontSize = (double)this.Text_FontSize;
				button2.MinHeight = (double)this.Text_Height;
				button2.MinWidth = (double)this.Button_Width;
				wrapPanel2.Children.Add(button2);
				this.btnOrders.Add(button2);
				this.oQuestion.SelectedCode.Add(text);
				if (text != "")
				{
					this.btnBrands[text].Style = style;
					this.btnBrands[text].Visibility = Visibility.Collapsed;
				}
				if (flag)
				{
					this.txtFill.Text = code_TEXT2;
					this.nCurrent = num;
					button2.Style = style;
					this.txtOrders[num].Foreground = brush2;
					flag = false;
				}
				num++;
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Brush foreground = (Brush)base.FindResource("PressedBrush");
			Brush foreground2 = (Brush)new BrushConverter().ConvertFromString("White");
			Button button = (Button)sender;
			int index = (int)button.Tag;
			string text = (string)button.Content;
			this.nCurrent = index;
			this.txtFill.Text = this.oQuestion.QCircleDetails[this.nCurrent].CODE_TEXT;
			foreach (Button button2 in this.btnOrders)
			{
				button2.Style = style2;
			}
			button.Style = style;
			if (this.oQuestion.SelectedCode[index] != "")
			{
				this.btnBrands[this.oQuestion.SelectedCode[index]].Style = style2;
				this.btnBrands[this.oQuestion.SelectedCode[index]].Visibility = Visibility.Visible;
				button.Content = "";
				this.oQuestion.SelectedCode[index] = "";
			}
			int num = 0;
			foreach (TextBlock textBlock in this.txtOrders)
			{
				if (this.oQuestion.SelectedCode[num] == "")
				{
					textBlock.Foreground = foreground2;
				}
				else
				{
					textBlock.Foreground = foreground;
				}
				num++;
			}
			this.txtOrders[this.nCurrent].Foreground = foreground;
		}

		private void method_4(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Brush foreground = (Brush)base.FindResource("PressedBrush");
			Brush brush = (Brush)new BrushConverter().ConvertFromString("White");
			Button button = (Button)sender;
			if (button.Style == style)
			{
				return;
			}
			button.Style = style;
			button.Visibility = Visibility.Collapsed;
			if (this.oQuestion.SelectedCode[this.nCurrent] != "")
			{
				this.btnBrands[this.oQuestion.SelectedCode[this.nCurrent]].Visibility = Visibility.Visible;
				this.btnBrands[this.oQuestion.SelectedCode[this.nCurrent]].Style = style2;
			}
			string value = (string)button.Tag;
			this.oQuestion.SelectedCode[this.nCurrent] = value;
			this.btnOrders[this.nCurrent].Content = button.Content;
			int num = 0;
			foreach (Button button2 in this.btnOrders)
			{
				if (this.oQuestion.SelectedCode[num] == "")
				{
					this.btnOrders[this.nCurrent].Style = style2;
					this.txtFill.Text = this.oQuestion.QCircleDetails[num].CODE_TEXT;
					this.btnOrders[num].Style = style;
					this.btnOrders[num].Focus();
					this.txtOrders[num].Foreground = foreground;
					this.nCurrent = num;
					break;
				}
				num++;
			}
		}

		private void method_5(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Brush brush = (Brush)base.FindResource("PressedBrush");
			Brush foreground = (Brush)new BrushConverter().ConvertFromString("White");
			int i = this.nCurrent;
			for (i = this.nCurrent; i < this.btnOrders.Count; i++)
			{
				if (this.oQuestion.SelectedCode[i] != "")
				{
					this.btnBrands[this.oQuestion.SelectedCode[i]].Style = style2;
					this.btnBrands[this.oQuestion.SelectedCode[i]].Visibility = Visibility.Visible;
					this.btnOrders[i].Content = "";
					this.oQuestion.SelectedCode[i] = "";
				}
				if (i > this.nCurrent)
				{
					this.btnOrders[i].Style = style2;
					this.txtOrders[i].Foreground = foreground;
				}
			}
		}

		private bool method_6()
		{
			base.UpdateLayout();
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Brush foreground = (Brush)base.FindResource("PressedBrush");
			Brush foreground2 = (Brush)new BrushConverter().ConvertFromString("White");
			int num = 0;
			using (List<string>.Enumerator enumerator = this.oQuestion.SelectedCode.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == "")
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, this.oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.nCurrent = num;
						this.txtFill.Text = this.oQuestion.QCircleDetails[this.nCurrent].CODE_TEXT;
						foreach (Button button in this.btnOrders)
						{
							button.Style = style2;
						}
						this.btnOrders[this.nCurrent].Style = style;
						this.btnOrders[this.nCurrent].Focus();
						int num2 = 0;
						foreach (TextBlock textBlock in this.txtOrders)
						{
							if (this.oQuestion.SelectedCode[num2] == "")
							{
								textBlock.Foreground = foreground2;
							}
							else
							{
								textBlock.Foreground = foreground;
							}
							num2++;
						}
						this.txtOrders[this.nCurrent].Foreground = foreground;
						return true;
					}
					num++;
				}
			}
			return false;
		}

		private List<VEAnswer> method_7()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			string str = (this.oQuestion.QDefine.GROUP_LEVEL == "A") ? this.oQuestion.QDefine.GROUP_CODEA : this.oQuestion.QDefine.GROUP_CODEB;
			str += this.MyNav.QName_Add;
			SurveyHelper.Answer = "";
			int num = 0;
			foreach (string text in this.oQuestion.SelectedCode)
			{
				string text2 = this.oQuestion.QuestionName + "_R" + this.oQuestion.QCircleDetails[num].CODE;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = text
				});
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					text2,
					"=",
					text
				});
				text2 = this.oQuestion.QuestionName + "_C" + text;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = this.oQuestion.QCircleDetails[num].CODE
				});
				text2 = str + "_R" + (num + 1).ToString();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = this.oQuestion.QCircleDetails[num].CODE
				});
				num++;
			}
			SurveyHelper.Answer = this.method_11(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		private void method_8(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSavebyCode("99");
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QCircleDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QCircleDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_6())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_7();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_8(list);
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

		private string method_9(string string_0, int int_0, int int_1 = -9999)
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

		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		private string method_11(string string_0, int int_0, int int_1 = -9999)
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

		private string method_12(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_13(string string_0)
		{
			if (string_0 == "")
			{
				return 0;
			}
			if (string_0 == "0")
			{
				return 0;
			}
			if (string_0 == "-0")
			{
				return 0;
			}
			if (!this.method_14(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		private bool method_14(string string_0)
		{
			return new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$").IsMatch(string_0);
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QMatrixSingle oQuestion = new QMatrixSingle();

		private int nCurrent;

		private List<TextBlock> txtOrders = new List<TextBlock>();

		private List<Button> btnOrders = new List<Button>();

		private Dictionary<string, Button> btnBrands = new Dictionary<string, Button>();

		private List<Button> listButton = new List<Button>();

		private bool PageLoaded;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize = 40;

		private int Text_Height;

		private int Text_Width;

		private int Text_FontSize = 40;

		private int Button_Type;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class38
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			internal int method_1(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly P_RankBrand.Class38 instance = new P_RankBrand.Class38();

			public static Comparison<SurveyDetail> compare0;

			public static Comparison<SurveyDetail> compare1;
		}
	}
}
