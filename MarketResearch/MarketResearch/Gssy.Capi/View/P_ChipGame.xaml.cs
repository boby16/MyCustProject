using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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

namespace Gssy.Capi.View
{
	public partial class P_ChipGame : Page
	{
		public P_ChipGame()
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
			if (this.MyNav.GroupLevel == "B")
			{
				this.MyNav.GroupLevel = "A";
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				this.MyNav.GetCircleInfo(this.MySurveyId);
				this.oQuestion.QuestionName = this.oQuestion.QuestionName + this.MyNav.QName_Add;
				this.oQuestion.CircleQuestionName = ((this.oQuestion.QDefine.GROUP_LEVEL == "A") ? this.oQuestion.QDefine.GROUP_CODEA : this.oQuestion.QDefine.GROUP_CODEB);
				this.oQuestion.CircleQuestionName = this.oQuestion.CircleQuestionName + this.MyNav.QName_Add;
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
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list.Count > 1) ? list[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QCircleDefine.LIMIT_LOGIC != "")
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list2 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list2.Add(surveyDetail);
							break;
						}
					}
				}
				this.oQuestion.QCircleDetails = list2;
			}
			if (this.oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int j = 0; j < this.oQuestion.QCircleDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QCircleDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QCircleDetails[j].CODE_TEXT);
				}
			}
			if (this.oQuestion.QCircleDefine.SHOW_LOGIC.Trim() != "")
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QCircleDefine.SHOW_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count<string>(); k++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail2.CODE == array2[k].ToString())
						{
							list3.Add(surveyDetail2);
							break;
						}
					}
				}
				this.oQuestion.QCircleDetails = list3;
			}
			else if (this.oQuestion.QCircleDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomCircleDetails();
			}
			if (this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "1" || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "3" || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "-1" || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "-3")
			{
				this.used.Visibility = Visibility.Visible;
				if (this.oQuestion.QCircleDefine.NOTE != "")
				{
					string_ = this.oQuestion.QCircleDefine.NOTE;
					list = this.oBoldTitle.ParaToList(string_, "//");
					string_ = list[0];
					this.oBoldTitle.SetTextBlock(this.txtBefore1, string_, 0, "", true);
					if (list.Count > 1)
					{
						string_ = list[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter1, string_, 0, "", true);
					}
				}
			}
			if (this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "2" || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "3" || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "-2" || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "-3")
			{
				this.unused.Visibility = Visibility.Visible;
				if (this.oQuestion.QDefine.NOTE != "")
				{
					string_ = this.oQuestion.QDefine.NOTE;
					list = this.oBoldTitle.ParaToList(string_, "//");
					string_ = list[0];
					this.oBoldTitle.SetTextBlock(this.txtBefore, string_, 0, "", true);
					if (list.Count > 1)
					{
						string_ = list[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter, string_, 0, "", true);
					}
				}
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != "")
			{
				this.oQuestion.QDefine.CONTROL_TOOLTIP = this.oLogicEngine.doubleResult(this.oQuestion.QDefine.CONTROL_TOOLTIP).ToString();
				this.nTotalChips = this.method_17(this.oQuestion.QDefine.CONTROL_TOOLTIP);
				this.nMaxChips = this.nTotalChips;
				this.txtFill.Text = this.nTotalChips.ToString();
			}
			if (this.oQuestion.QDefine.MIN_COUNT > 0)
			{
				this.nMin = this.oQuestion.QDefine.MIN_COUNT;
			}
			this.nMax = ((this.oQuestion.QDefine.MAX_COUNT > 0) ? this.oQuestion.QDefine.MAX_COUNT : this.nMaxChips);
			this.Fill_Length = this.oQuestion.QDefine.CONTROL_TOOLTIP.Length;
			this.Fill_Width = (double)this.Fill_Length * this.txtFill.FontSize * Math.Pow(0.955, (double)this.Fill_Length);
			if (this.Fill_Width < 20.0)
			{
				this.Fill_Width = 20.0;
			}
			if (this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == "H")
			{
				this.BrandText_Width = 250;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_WIDTH != 0)
			{
				this.BrandText_Width = this.oQuestion.QCircleDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.Fill_Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_HEIGHT != 0)
			{
				this.Fill_Height = this.oQuestion.QCircleDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE != 0)
			{
				this.Fill_FontSize = this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
			}
			this.method_2();
			this.nTotalChips = this.nMaxChips - this.nUseChips;
			this.txtUsed.Text = this.nUseChips.ToString();
			this.txtFill.Text = this.nTotalChips.ToString();
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				if (this.ChipFills.Count > 0)
				{
					int num = this.nTotalChips;
					foreach (TextBox textBox in this.ChipFills)
					{
						if (num <= this.nMax)
						{
							textBox.Text = num.ToString();
							this.method_6(textBox);
							break;
						}
						textBox.Text = this.nMax.ToString();
						num -= this.nMax;
						this.method_6(textBox);
					}
					if (autoFill.AutoNext(this.oQuestion.QDefine))
					{
						this.btnNav_Click(this, e);
					}
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
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded = false;
			}
		}

		private void method_2()
		{
			Style style = (Style)base.FindResource("UnSelBtnStyle");
			Style style2 = (Style)base.FindResource("ContentMediumStyle");
			Brush brush = (Brush)base.FindResource("NormalBorderBrush");
			Brush foreground = (Brush)new BrushConverter().ConvertFromString("White");
			WrapPanel wrapPanel = this.wrapPanel1;
			bool flag = this.method_17(this.oQuestion.QCircleDefine.CONTROL_MASK) > 0;
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
			{
				string code = surveyDetail.CODE;
				string text = surveyDetail.CODE_TEXT;
				if (flag)
				{
					text = "(" + (num + 1).ToString() + ") " + text;
				}
				string text2 = this.nMin.ToString();
				if (SurveyHelper.NavOperation == "Back")
				{
					string string_ = this.oQuestion.QuestionName + "_C" + code;
					text2 = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_);
					int num2 = this.method_17(text2);
					text2 = num2.ToString();
					this.nUseChips += num2;
				}
				else
				{
					this.nUseChips += this.nMin;
				}
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapPanel2.Orientation = ((this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == "H") ? Orientation.Horizontal : Orientation.Vertical);
				wrapPanel2.Margin = new Thickness(20.0, 20.0, 20.0, 20.0);
				wrapPanel.Children.Add(wrapPanel2);
				WrapPanel wrapPanel3 = new WrapPanel();
				wrapPanel3.VerticalAlignment = VerticalAlignment.Center;
				if (this.BrandText_Width > 0)
				{
					wrapPanel3.Width = (double)this.BrandText_Width;
				}
				wrapPanel2.Children.Add(wrapPanel3);
				TextBlock textBlock = new TextBlock();
				textBlock.Text = text;
				textBlock.Style = style2;
				textBlock.Foreground = foreground;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.FontSize = (double)this.Fill_FontSize;
				wrapPanel3.Children.Add(textBlock);
				this.ChipTexts.Add(textBlock);
				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;
				wrapPanel2.Children.Add(stackPanel);
				Button button = new Button();
				button.Content = " - ";
				button.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				button.Style = style;
				button.Tag = num;
				button.Click += this.method_3;
				button.FontSize = (double)this.Fill_FontSize;
				button.Height = (double)this.Fill_Height;
				stackPanel.Children.Add(button);
				TextBox textBox = new TextBox();
				textBox.Text = text2;
				textBox.Tag = num;
				textBox.ToolTip = "请在这里填入分配给[" + text + "]的数量。";
				textBox.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBox.FontSize = (double)this.Fill_FontSize;
				textBox.Width = this.Fill_Width;
				textBox.Height = (double)this.Fill_Height;
				textBox.MaxLength = this.Fill_Length;
				textBox.HorizontalAlignment = HorizontalAlignment.Right;
				textBox.GotFocus += this.method_12;
				textBox.LostFocus += this.method_11;
				textBox.KeyDown += this.method_5;
				textBox.TextChanged += this.method_7;
				stackPanel.Children.Add(textBox);
				this.ChipFills.Add(textBox);
				button = new Button();
				button.Content = " + ";
				button.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				button.Style = style;
				button.Tag = num;
				button.Click += this.method_4;
				button.FontSize = (double)this.Fill_FontSize;
				button.Height = (double)this.Fill_Height;
				stackPanel.Children.Add(button);
				this.oQuestion.FillTexts.Add("");
				num++;
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			int index = (int)((Button)sender).Tag;
			int num = this.method_17(this.ChipFills[index].Text);
			if (num > this.nMin)
			{
				num--;
				this.ChipFills[index].Text = num.ToString();
				this.nUseChips--;
				this.txtUsed.Text = this.nUseChips.ToString();
				if (this.nTotalChips < this.nMaxChips)
				{
					this.nTotalChips++;
					this.txtFill.Text = this.nTotalChips.ToString();
				}
			}
		}

		private void method_4(object sender, RoutedEventArgs e)
		{
			int index = (int)((Button)sender).Tag;
			if (this.nTotalChips > 0)
			{
				int num = this.method_17(this.ChipFills[index].Text);
				if (num < this.nMax)
				{
					num++;
					this.ChipFills[index].Text = num.ToString();
					this.nUseChips++;
					this.txtUsed.Text = this.nUseChips.ToString();
				}
				this.nTotalChips--;
				this.txtFill.Text = this.nTotalChips.ToString();
			}
		}

		private void method_5(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				this.method_6(textBox);
				int num = (int)textBox.Tag;
				if (num < this.ChipFills.Count - 1)
				{
					this.ChipFills[num + 1].Focus();
					return;
				}
				this.ChipFills[0].Focus();
			}
		}

		private void method_6(TextBox textBox_0)
		{
			if (textBox_0.Text == "")
			{
				textBox_0.Text = "0";
			}
			this.nUseChips = 0;
			foreach (TextBox textBox in this.ChipFills)
			{
				this.nUseChips += this.method_17(textBox.Text);
			}
			this.nTotalChips = this.nMaxChips - this.nUseChips;
			this.txtUsed.Text = this.nUseChips.ToString();
			this.txtFill.Text = this.nTotalChips.ToString();
		}

		private void method_7(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			TextChange[] array = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				string a = textBox.Text.Substring(offset, array[0].AddedLength).Trim();
				bool flag;
				if (!(a == "") && !(a == "."))
				{
					double num = 0.0;
					flag = !double.TryParse(textBox.Text, out num);
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					textBox.Text = textBox.Text.Remove(offset, array[0].AddedLength);
					textBox.Select(offset, 0);
				}
			}
		}

		private bool method_8()
		{
			int num = 0;
			if (this.oQuestion.QCircleDefine.CONTROL_TYPE > 0)
			{
				num = this.oQuestion.QCircleDefine.CONTROL_TYPE;
			}
			else if (this.oQuestion.QCircleDefine.CONTROL_TYPE < 0)
			{
				num = -this.oQuestion.QCircleDefine.CONTROL_TYPE;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (TextBox textBox in this.ChipFills)
			{
				if (textBox.Text == "")
				{
					textBox.Text = "0";
				}
				string text = textBox.Text;
				int num5 = this.method_17(text);
				if (this.oQuestion.QCircleDefine.CONTROL_TYPE != 0 && num5 > 0)
				{
					if (dictionary.ContainsKey(text))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string key = text;
						int num6 = dictionary2[key];
						dictionary2[key] = num6 + 1;
						if (dictionary[text] >= num)
						{
							if (this.oQuestion.QCircleDefine.CONTROL_TYPE > 0)
							{
								MessageBox.Show(string.Format(SurveyMsg.MsgNotOverOption, dictionary[text].ToString(), text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								textBox.Focus();
								return true;
							}
							if (this.oQuestion.QCircleDefine.CONTROL_TYPE < 0 && this.CheckSameChips)
							{
								if (MessageBox.Show(string.Format(SurveyMsg.MsgNotOverOptionWeak, dictionary[text], text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
								{
									textBox.Focus();
									return true;
								}
								this.CheckSameChips = false;
							}
						}
					}
					else
					{
						dictionary.Add(text, 1);
					}
				}
				if (num5 < this.nMin)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, this.nMin.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					textBox.Focus();
					return true;
				}
				if (num5 > this.nMax)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, this.nMax.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					textBox.Focus();
					return true;
				}
				if (num2 > 0 && this.oQuestion.QCircleDefine.CONTROL_MASK == "2")
				{
					if (num4 < num5)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgNeedMore, this.ChipTexts[num2].Text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						textBox.Focus();
						return true;
					}
				}
				else if (num2 > 0 && this.oQuestion.QCircleDefine.CONTROL_MASK == "3" && this.CheckChipsLogic && num4 < num5)
				{
					if (MessageBox.Show(string.Format(SurveyMsg.MsgNeedMoreWeak, this.ChipTexts[num2].Text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
					{
						textBox.Focus();
						return true;
					}
					this.CheckChipsLogic = false;
				}
				num4 = num5;
				num3 += num5;
				this.oQuestion.FillTexts[num2] = text;
				num2++;
			}
			if (!(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "0") && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "") && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "-1") && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "-2") && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "-3") && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == "-4"))
			{
				this.nUseChips = num3;
				this.nTotalChips = this.nMaxChips - this.nUseChips;
				this.txtUsed.Text = this.nUseChips.ToString();
				this.txtFill.Text = this.nTotalChips.ToString();
				if (this.nTotalChips > 0)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgNeedFinishNum, this.txtFill.Text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.ChipFills[0].Focus();
					return true;
				}
				if (this.nTotalChips < 0)
				{
					MessageBox.Show(SurveyMsg.MsgNeedFinishRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.ChipFills[0].Focus();
					return true;
				}
			}
			else if (num3 == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNotOptionZero, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.ChipFills[0].Focus();
				return true;
			}
			return false;
		}

		private List<VEAnswer> method_9()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			string str = (this.oQuestion.QDefine.GROUP_LEVEL == "A") ? this.oQuestion.QDefine.GROUP_CODEA : this.oQuestion.QDefine.GROUP_CODEB;
			str += this.MyNav.QName_Add;
			SurveyHelper.Answer = "";
			int num = 0;
			foreach (string text in this.oQuestion.FillTexts)
			{
				string text2 = this.oQuestion.QuestionName + "_C" + this.oQuestion.QCircleDetails[num].CODE;
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
				dictionary.Add(this.oQuestion.QCircleDetails[num].CODE, this.oFunc.StringToDouble(text));
				text2 = str + "_R" + (num + 1).ToString();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = this.oQuestion.QCircleDetails[num].CODE
				});
				num++;
			}
			SurveyHelper.Answer = this.method_15(SurveyHelper.Answer, 1, -9999);
			Dictionary<string, string> dictionary2 = this.oFunc.SortDictSDbyValue(dictionary, true, false, ",");
			num = 1;
			foreach (string key in dictionary2.Keys)
			{
				string text2 = this.oQuestion.QuestionName + "_R" + num.ToString();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = dictionary2[key]
				});
				num++;
			}
			return list;
		}

		private void method_10(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave(1);
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
			if (this.method_8())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_9();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_10(list);
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

		private void method_11(object sender, RoutedEventArgs e)
		{
			Brush foreground = (Brush)new BrushConverter().ConvertFromString("White");
			int index = (int)((TextBox)sender).Tag;
			this.ChipTexts[index].Foreground = foreground;
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
			this.method_6((TextBox)sender);
		}

		private void method_12(object sender, RoutedEventArgs e)
		{
			Brush foreground = (Brush)base.FindResource("PressedBrush");
			TextBox textBox = (TextBox)sender;
			int index = (int)textBox.Tag;
			this.ChipTexts[index].Foreground = foreground;
			if (textBox.Text == "0")
			{
				textBox.Text = "";
			}
			else
			{
				textBox.SelectAll();
			}
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private string method_13(string string_0, int int_0, int int_1 = -9999)
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

		private string method_14(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		private string method_15(string string_0, int int_0, int int_1 = -9999)
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

		private string method_16(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_17(string string_0)
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
			if (!this.method_18(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		private bool method_18(string string_0)
		{
			return new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$").IsMatch(string_0);
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QMatrixFill oQuestion = new QMatrixFill();

		private List<TextBlock> ChipTexts = new List<TextBlock>();

		private List<TextBox> ChipFills = new List<TextBox>();

		private int nTotalChips = 20;

		private int nMaxChips = 20;

		private int nUseChips;

		private int nMin;

		private int nMax = 20;

		private bool CheckChipsLogic = true;

		private bool CheckSameChips = true;

		private bool PageLoaded;

		private int BrandText_Width;

		private int Fill_Height = 60;

		private double Fill_Width = 80.0;

		private int Fill_FontSize = 45;

		private int Fill_Length = 2;

		private int Button_Type;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;
	}
}
