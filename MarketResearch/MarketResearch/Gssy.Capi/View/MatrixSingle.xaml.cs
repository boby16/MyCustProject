using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	public partial class MatrixSingle : Page
	{
		public MatrixSingle()
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
			this.oQuestion.Init(this.CurPageId, 0, true);
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
			if (this.oQuestion.QDefine.GROUP_LEVEL == "B")
			{
				string_ = ((list.Count > 1) ? list[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			}
			else
			{
				string_ = ((list.Count > 1) ? list[1] : "");
			}
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			string text = this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim();
			if (text != "")
			{
				this.CL_TA = this.method_8(text, 1);
				if ("0123456789".Contains(this.CL_TA))
				{
					this.CL_TA = "R";
					if (text != "")
					{
						this.CL_Width = Convert.ToInt32(text);
					}
				}
				else
				{
					text = this.method_9(text, 1, -9999);
					this.CL_VA = this.method_8(text, 1);
					if ("0123456789".Contains(this.CL_VA))
					{
						if (this.CL_TA != "T" && this.CL_TA != "B")
						{
							this.CL_VA = "C";
						}
						if (text != "")
						{
							this.CL_Width = Convert.ToInt32(text);
						}
					}
					else if (this.method_9(text, 1, -9999) != "")
					{
						this.CL_Width = Convert.ToInt32(this.method_9(text, 1, -9999));
					}
				}
				text = this.CL_TA + this.CL_VA;
				if (text.Contains("L"))
				{
					this.CL_TA = "L";
				}
				else if (text.Contains("R"))
				{
					this.CL_TA = "R";
				}
				if (text.Contains("T"))
				{
					this.CL_VA = "T";
				}
				else if (text.Contains("B"))
				{
					this.CL_VA = "B";
				}
				if (text.Contains("C") && (text.Contains("T") || text.Contains("B")))
				{
					this.CL_TA = "C";
				}
				if (text.Contains("C") && (text.Contains("R") || text.Contains("L")))
				{
					this.CL_VA = "C";
				}
				if (text == "C" || text == "CC")
				{
					this.CL_TA = "C";
					this.CL_VA = "C";
				}
			}
			this.oBoldTitle.SetTextBlock(this.OptionTitleHeader, this.oQuestion.QCircleDefine.QUESTION_CONTENT, 0, "", true);
			this.txt1.Text = "";
			this.txt3.Text = "";
			this.txt4.Text = "";
			this.txt5.Text = "";
			this.txt6.Text = "";
			this.txt7.Text = "";
			this.txt9.Text = "";
			string note = this.oQuestion.QDefine.NOTE;
			if (note == "")
			{
				this.txt1.Height = 0.0;
				this.txt3.Height = 0.0;
				this.txt4.Height = 0.0;
				this.txt5.Height = 0.0;
				this.txt6.Height = 0.0;
				this.txt7.Height = 0.0;
				this.txt9.Height = 0.0;
			}
			else
			{
				list = new List<string>(note.Split(new string[]
				{
					"//"
				}, StringSplitOptions.RemoveEmptyEntries));
				int num = this.oBoldTitle.TakeFontSize(list[0]);
				list[0] = this.oBoldTitle.TakeText(list[0]);
				if (list.Count == 1)
				{
					this.txt5.Text = list[0];
				}
				else if (list.Count == 2)
				{
					this.txt1.Text = list[0];
					this.txt9.Text = list[1];
				}
				else if (list.Count == 3)
				{
					this.txt1.Text = list[0];
					this.txt5.Text = list[1];
					this.txt9.Text = list[2];
				}
				else if (list.Count == 4)
				{
					this.txt1.Text = list[0];
					this.txt4.Text = list[1];
					this.txt6.Text = list[2];
					this.txt9.Text = list[3];
				}
				else if (list.Count == 5)
				{
					this.txt1.Text = list[0];
					this.txt3.Text = list[1];
					this.txt5.Text = list[2];
					this.txt7.Text = list[3];
					this.txt9.Text = list[4];
				}
				if (num != 0)
				{
					this.txt1.FontSize = (double)num;
					this.txt3.FontSize = (double)num;
					this.txt4.FontSize = (double)num;
					this.txt5.FontSize = (double)num;
					this.txt6.FontSize = (double)num;
					this.txt7.FontSize = (double)num;
					this.txt9.FontSize = (double)num;
				}
				else if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
				{
					this.txt1.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt3.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt4.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt5.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt6.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt7.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt9.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				}
			}
			if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list2 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list2.Add(surveyDetail);
							break;
						}
					}
				}
				list2.Sort(new Comparison<SurveyDetail>(MatrixSingle.Class16.instance.method_0));
				this.oQuestion.QDetails = list2;
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
				}
			}
			if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails(1);
			}
			if (this.oQuestion.QCircleDefine.LIMIT_LOGIC != "")
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
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
				list3.Sort(new Comparison<SurveyDetail>(MatrixSingle.Class16.instance.method_1));
				this.oQuestion.QCircleDetails = list3;
			}
			if (this.oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int l = 0; l < this.oQuestion.QCircleDetails.Count<SurveyDetail>(); l++)
				{
					this.oQuestion.QCircleDetails[l].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QCircleDetails[l].CODE_TEXT);
				}
			}
			if (this.oQuestion.QCircleDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails(2);
			}
			else if (this.oQuestion.QCircleDefine.SHOW_LOGIC != "" && this.oQuestion.QCircleDefine.IS_RANDOM == 0)
			{
				string show_LOGIC = this.oQuestion.QCircleDefine.SHOW_LOGIC;
				string[] array3 = this.oLogicEngine.aryCode(show_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int m = 0; m < array3.Count<string>(); m++)
				{
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail3.CODE == array3[m].ToString())
						{
							list4.Add(surveyDetail3);
							break;
						}
					}
				}
				this.oQuestion.QCircleDetails = list4;
			}
			this.Button_Height = SurveyHelper.BtnHeight;
			this.Button_FontSize = SurveyHelper.BtnFontSize;
			if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				this.Button_Height = this.oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.Button_Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				this.Button_FontSize = this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (this.Button_Width < 2.0)
			{
				this.CR_Width = base.ActualWidth - 63.0;
				if (this.CL_Width > 0)
				{
					this.CR_Width -= (double)this.CL_Width;
				}
				if (this.Button_Width != 1.0 && this.oQuestion.QDetails.Count <= 7)
				{
					if (this.CL_Width == 0)
					{
						this.CL_Width = (int)(this.CR_Width / 16.0 * 4.0);
						this.CR_Width = this.CR_Width / 16.0 * 10.0 - 8.0;
					}
					else
					{
						this.CR_Width = this.CR_Width / 12.0 * 10.0 - 8.0;
					}
				}
				else if (this.CL_Width == 0)
				{
					this.CL_Width = (int)(this.CR_Width / 14.0 * 4.0);
					this.CR_Width = this.CR_Width / 14.0 * 10.0 - 8.0;
				}
				this.Button_Width = (this.CR_Width - (double)((this.oQuestion.QDetails.Count - 1) * 4) - 43.0) / (double)this.oQuestion.QDetails.Count;
				if (this.Button_Width < 20.0)
				{
					this.Button_Width = 20.0;
				}
			}
			this.method_2();
			if (SurveyHelper.AutoFill && new AutoFill
			{
				oLogicEngine = this.oLogicEngine
			}.AutoNext(this.oQuestion.QDefine))
			{
				this.btnNav_Click(this, e);
			}
			Style style = (Style)base.FindResource("SelBtnStyle");
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back"))
			{
				if (!(navOperation == "Normal"))
				{
					if (!(navOperation == "Jump"))
					{
					}
				}
				else if (this.oQuestion.QDetails.Count == 1 && this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2) && !SurveyHelper.AutoFill)
				{
					this.btnNav_Click(this, e);
				}
			}
			this.PageLoaded = true;
		}

		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				if (this.CL_Width > 0)
				{
					this.TitleLeft.Width = new GridLength((double)this.CL_Width);
					this.GridLeft.Width = new GridLength((double)this.CL_Width);
				}
				int num = (this.ScrollContent.ComputedVerticalScrollBarVisibility == Visibility.Collapsed) ? 3 : 43;
				this.GridTitle.Margin = new Thickness(0.0, 0.0, (double)num, 0.0);
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded = false;
			}
		}

		private void method_2()
		{
			AutoFill autoFill = new AutoFill();
			autoFill.oLogicEngine = this.oLogicEngine;
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Style style3 = (Style)base.FindResource("ContentMediumStyle");
			Brush borderBrush = (Brush)base.FindResource("NormalBorderBrush");
			Brush foreground = (Brush)base.FindResource("PressedBrush");
			HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
			if (this.CL_TA == "C")
			{
				horizontalAlignment = HorizontalAlignment.Center;
			}
			else if (this.CL_TA == "L")
			{
				horizontalAlignment = HorizontalAlignment.Left;
			}
			VerticalAlignment verticalAlignment = VerticalAlignment.Center;
			if (this.CL_VA == "T")
			{
				verticalAlignment = VerticalAlignment.Top;
			}
			else if (this.CL_VA == "B")
			{
				verticalAlignment = VerticalAlignment.Bottom;
			}
			Grid gridContent = this.GridContent;
			int num = 0;
			string text = this.method_8(this.oQuestion.QDefine.CONTROL_MASK, 1);
			if (text == "#")
			{
				num = 1;
			}
			else if (text.ToUpper() == "G")
			{
				num = 2;
			}
			else if (this.oQuestion.QDefine.CONTROL_MASK != "" && this.oQuestion.QDefine.CONTROL_MASK != null)
			{
				num = 0;
				this.iNoOfInterval = (int)Convert.ToInt16(this.oQuestion.QDefine.CONTROL_MASK.ToString());
				if (this.iNoOfInterval < 1)
				{
					this.iNoOfInterval = 9999;
				}
			}
			int num2 = 0;
			int num3 = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
			{
				string code = surveyDetail.CODE;
				string code_TEXT = surveyDetail.CODE_TEXT;
				string text2 = "";
				if (SurveyHelper.NavOperation == "Back")
				{
					string string_ = this.oQuestion.QuestionName + "_R" + code;
					text2 = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_);
				}
				gridContent.RowDefinitions.Add(new RowDefinition
				{
					Height = GridLength.Auto
				});
				Border border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
				bool flag = false;
				if (num == 1)
				{
					if (this.oQuestion.QDefine.CONTROL_MASK.Contains("#" + code + "#"))
					{
						flag = true;
					}
				}
				else if (num > 1)
				{
					if (num3 == 0)
					{
						num3 = surveyDetail.RANDOM_SET;
					}
					else if (num3 != surveyDetail.RANDOM_SET)
					{
						if (num == 2)
						{
							num = 3;
							flag = true;
						}
						else
						{
							num = 2;
						}
						num3 = surveyDetail.RANDOM_SET;
					}
					else if (num == 3)
					{
						flag = true;
					}
				}
				else if (num2 / this.iNoOfInterval % 2 > 0)
				{
					flag = true;
				}
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.BackgroudColor));
				}
				border.SetValue(Grid.RowProperty, num2);
				border.SetValue(Grid.ColumnProperty, 0);
				gridContent.Children.Add(border);
				WrapPanel wrapPanel = new WrapPanel();
				wrapPanel.VerticalAlignment = verticalAlignment;
				wrapPanel.HorizontalAlignment = horizontalAlignment;
				border.Child = wrapPanel;
				TextBlock textBlock = new TextBlock();
				textBlock.Text = code_TEXT;
				textBlock.Style = style3;
				textBlock.Foreground = foreground;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
				textBlock.VerticalAlignment = verticalAlignment;
				if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
				{
					textBlock.FontSize = (double)this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
				}
				wrapPanel.Children.Add(textBlock);
				border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
				border.SetValue(Grid.RowProperty, num2);
				border.SetValue(Grid.ColumnProperty, 1);
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.BackgroudColor));
				}
				gridContent.Children.Add(border);
				WrapPanel wrapPanel2 = new WrapPanel();
				this.wrapSingle.Add(wrapPanel2);
				wrapPanel2.Orientation = Orientation.Horizontal;
				wrapPanel2.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel2.HorizontalAlignment = HorizontalAlignment.Center;
				wrapPanel2.Margin = new Thickness(2.0, 5.0, 2.0, 5.0);
				wrapPanel2.Name = "wR" + code;
				wrapPanel2.Tag = code;
				border.Child = wrapPanel2;
				this.oQuestion.SelectedCode.Add(text2);
				this.listButton = new List<Button>();
				foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
				{
					Button button = new Button();
					button.Name = "b_" + surveyDetail2.CODE;
					button.Content = surveyDetail2.CODE_TEXT;
					button.Margin = new Thickness(2.0, 0.0, 2.0, 0.0);
					button.Style = ((surveyDetail2.CODE == text2) ? style : style2);
					if (flag)
					{
						button.Opacity = 0.85;
					}
					button.Tag = num2;
					button.Click += this.method_3;
					button.FontSize = (double)this.Button_FontSize;
					button.MinWidth = this.Button_Width;
					button.MinHeight = (double)this.Button_Height;
					wrapPanel2.Children.Add(button);
					this.listButton.Add(button);
				}
				int num4 = 0;
				if ((!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == "3")) && SurveyHelper.NavOperation != "Back")
				{
					string extend_ = surveyDetail.EXTEND_4;
					if (extend_ != "")
					{
						string[] array = this.oLogicEngine.aryCode(extend_, ',');
						for (int i = 0; i < array.Count<string>(); i++)
						{
							using (List<Button>.Enumerator enumerator3 = this.listButton.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									Button button2 = enumerator3.Current;
									if (button2.Name == "b_" + array[i])
									{
										num4 = 1;
										this.method_3(button2, new RoutedEventArgs());
										break;
									}
								}
								goto IL_876;
							}
							break;
							IL_876:;
						}
					}
				}
				if (num4 == 0 && this.oQuestion.QDetails.Count == 1 && !SurveyHelper.AutoFill && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
				{
					this.method_3(this.listButton[0], new RoutedEventArgs());
				}
				if (SurveyHelper.AutoFill)
				{
					Button button3;
					if (this.oQuestion.QDefine.CONTROL_TYPE == 0)
					{
						button3 = autoFill.SingleButton(this.oQuestion.QDefine, this.listButton);
					}
					else
					{
						if (this.AutoFillButton == -1)
						{
							this.AutoFillButton = Convert.ToInt32(this.oFunc.INT((double)(this.listButton.Count<Button>() / 2), 0, 0, 0));
						}
						button3 = this.listButton[this.AutoFillButton];
					}
					if (button3 != null && num4 == 0)
					{
						this.method_3(button3, new RoutedEventArgs());
					}
				}
				num2++;
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Button button = (Button)sender;
			int index = (int)button.Tag;
			string text = button.Name.Substring(2);
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				Panel panel = this.wrapSingle[index];
				this.oQuestion.SelectedCode[index] = text;
				foreach (object obj in panel.Children)
				{
					Button button2 = (Button)obj;
					string a = button2.Name.Substring(2);
					button2.Style = ((a == text) ? style : style2);
				}
				if (this.oQuestion.QDefine.MAX_COUNT > 0 && this.SameClickCheck && !SurveyHelper.AutoFill)
				{
					if (text == this.LastClickCode)
					{
						this.SameClickCount++;
						if (this.SameClickCount >= this.oQuestion.QDefine.MAX_COUNT && MessageBox.Show(string.Format(SurveyMsg.MsgMXSA_info3, this.SameClickCount.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.Yes).Equals(MessageBoxResult.No))
						{
							this.SameClickCheck = false;
							return;
						}
					}
					else
					{
						this.LastClickCode = text;
						this.SameClickCount = 1;
					}
				}
			}
		}

		private bool method_4()
		{
			int num = 0;
			using (List<string>.Enumerator enumerator = this.oQuestion.SelectedCode.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == "")
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, this.oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.wrapSingle[num].Focus();
						return true;
					}
					num++;
				}
			}
			if (this.oQuestion.QDefine.MIN_COUNT > 0 && !SurveyHelper.AutoFill)
			{
				int num2 = this.oQuestion.QDefine.MIN_COUNT;
				if (num2 > this.oQuestion.QCircleDetails.Count && this.oQuestion.QCircleDefine.MAX_COUNT == 1)
				{
					num2 = this.oQuestion.QCircleDetails.Count;
				}
				string a = "";
				int num3 = 0;
				num = 0;
				foreach (string text in this.oQuestion.SelectedCode)
				{
					if (a == text)
					{
						num3++;
					}
					else
					{
						if (num3 >= num2)
						{
							num--;
							this.wrapSingle[num].Focus();
							string text2 = string.Format(SurveyMsg.MsgMXSA_info1, this.oQuestion.QCircleDetails[num].CODE_TEXT, num3);
							if (this.oQuestion.QCircleDefine.MIN_COUNT > 0)
							{
								MessageBox.Show(text2, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								return true;
							}
							text2 = text2 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
							if (MessageBox.Show(text2, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
							{
								return true;
							}
						}
						num3 = 1;
						a = text;
					}
					num++;
				}
				if (num3 >= num2)
				{
					num--;
					this.wrapSingle[num].Focus();
					string text3 = string.Format(SurveyMsg.MsgMXSA_info1, this.oQuestion.QCircleDetails[num].CODE_TEXT, num3);
					if (this.oQuestion.QCircleDefine.MIN_COUNT > 0)
					{
						MessageBox.Show(text3, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					text3 = text3 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
					if (MessageBox.Show(text3, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
			}
			else if (this.oQuestion.QDefine.MIN_COUNT < 0)
			{
				int num4 = -this.oQuestion.QDefine.MIN_COUNT;
				if (num4 > this.oQuestion.QCircleDetails.Count && this.oQuestion.QCircleDefine.MAX_COUNT == 1)
				{
					num4 = this.oQuestion.QCircleDetails.Count;
				}
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				num = 0;
				foreach (string text4 in this.oQuestion.SelectedCode)
				{
					if (dictionary.ContainsKey(text4))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string key = text4;
						int num5 = dictionary2[key];
						dictionary2[key] = num5 + 1;
					}
					else
					{
						dictionary.Add(text4, 1);
					}
					num++;
				}
				foreach (string text5 in dictionary.Keys)
				{
					if (dictionary[text5] >= num4)
					{
						string arg = "";
						using (List<SurveyDetail>.Enumerator enumerator3 = this.oQuestion.QDetails.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								SurveyDetail surveyDetail = enumerator3.Current;
								if (surveyDetail.CODE == text5)
								{
									arg = surveyDetail.CODE_TEXT;
									break;
								}
							}
							goto IL_479;
						}
						IL_435:
						string text6 = text6 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
						if (MessageBox.Show(text6, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
						{
							return true;
						}
						continue;
						IL_479:
						text6 = string.Format(SurveyMsg.MsgMXSA_info2, arg, dictionary[text5]);
						if (this.oQuestion.QCircleDefine.MIN_COUNT > 0)
						{
							MessageBox.Show(text6, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							return true;
						}
						goto IL_435;
					}
				}
			}
			if (this.oQuestion.QDefine.CONTROL_TYPE != 0)
			{
				string b = Math.Abs(this.oQuestion.QDefine.CONTROL_TYPE).ToString();
				int num6 = 0;
				int num7 = 9999999;
				int num8 = 0;
				int index = 0;
				num = 0;
				foreach (string string_ in this.oQuestion.SelectedCode)
				{
					int num9 = this.oFunc.StringToInt(string_);
					if (this.oQuestion.QCircleDetails[num].CODE == b)
					{
						num6 = num9;
						index = num;
					}
					else
					{
						if (num8 < num9)
						{
							num8 = num9;
						}
						if (num7 > num9)
						{
							num7 = num9;
						}
					}
					num++;
				}
				if (num6 < num7 && num6 > 0)
				{
					this.wrapSingle[index].Focus();
					if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgPointSmall, this.oQuestion.QCircleDetails[index].CODE_TEXT, num6.ToString(), num7.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					if (MessageBox.Show(string.Format(SurveyMsg.MsgPointSmall, this.oQuestion.QCircleDetails[index].CODE_TEXT, num6.ToString(), num7.ToString()) + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
				else if (num6 > num8)
				{
					this.wrapSingle[index].Focus();
					if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgPointBig, this.oQuestion.QCircleDetails[index].CODE_TEXT, num6.ToString(), num8.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					if (MessageBox.Show(string.Format(SurveyMsg.MsgPointBig, this.oQuestion.QCircleDetails[index].CODE_TEXT, num6.ToString(), num8.ToString()) + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
			}
			return false;
		}

		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
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
				text2 = this.oQuestion.CircleQuestionName + "_R" + this.oQuestion.QCircleDetails[num].CODE;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = this.oQuestion.QCircleDetails[num].CODE
				});
				num++;
			}
			SurveyHelper.Answer = this.method_9(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		private void method_6(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			this.oPageNav.Refresh();
			if (this.method_4())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_5();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_6(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		private string method_7(string string_0, int int_0, int int_1 = -9999)
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

		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		private string method_9(string string_0, int int_0, int int_1 = -9999)
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

		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private UDPX oFunc = new UDPX();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QMatrixSingle oQuestion = new QMatrixSingle();

		private List<Button> listButton = new List<Button>();

		private int AutoFillButton = -1;

		private List<WrapPanel> wrapSingle = new List<WrapPanel>();

		private string LastClickCode = "";

		private int SameClickCount;

		private bool SameClickCheck = true;

		private string BackgroudColor = "#33FFFFFF";

		private int iNoOfInterval = 9999;

		private string CL_TA = "R";

		private string CL_VA = "C";

		private int CL_Width;

		private double CR_Width;

		private bool PageLoaded;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class16
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			internal int method_1(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly MatrixSingle.Class16 instance = new MatrixSingle.Class16();

			public static Comparison<SurveyDetail> compare0;

			public static Comparison<SurveyDetail> compare1;
		}
	}
}
