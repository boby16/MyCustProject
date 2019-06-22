﻿using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
	public partial class MultipleText : Page
	{
		public MultipleText()
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
			string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add("");
			if (show_LOGIC != "")
			{
				list2 = this.oBoldTitle.ParaToList(show_LOGIC, "//");
				if (list2.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list2[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list3[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list3.Count > 1) ? list3[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list4.Add(surveyDetail);
							break;
						}
					}
				}
				if (this.oQuestion.QDefine.SHOW_LOGIC == "" && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list4.Sort(new Comparison<SurveyDetail>(MultipleText.Class25.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.FIX_LOGIC != "")
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.FIX_LOGIC, ',');
				for (int j = 0; j < array2.Count<string>(); j++)
				{
					using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								this.listFix.Add(array2[j]);
								break;
							}
						}
					}
				}
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != "")
			{
				string[] array3 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int k = 0; k < array3.Count<string>(); k++)
				{
					using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array3[k])
							{
								this.listPreSet.Add(array3[k]);
								break;
							}
						}
					}
				}
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int l = 0; l < this.oQuestion.QDetails.Count<SurveyDetail>(); l++)
				{
					this.oQuestion.QDetails[l].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[l].CODE_TEXT);
				}
			}
			if (list2[0].Trim() != "")
			{
				string[] array4 = this.oLogicEngine.aryCode(list2[0], ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int m = 0; m < array4.Count<string>(); m++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						if (surveyDetail2.CODE == array4[m].ToString())
						{
							list5.Add(surveyDetail2);
							break;
						}
					}
				}
				this.oQuestion.QDetails = list5;
			}
			else if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails();
			}
			List<string> list6 = this.oFunc.StringToList(this.oQuestion.QDefine.CONTROL_TOOLTIP, "//");
			if (list6.Count > 0)
			{
				this._MaxLen = this.oFunc.StringToInt(list6[0]);
				if (this._MaxLen < 1)
				{
					this._MaxLen = 1;
				}
			}
			if (list6.Count > 1)
			{
				List<string> list7 = this.oFunc.StringToList(list6[1], ",");
				if (list7.Count > 1)
				{
					this._MaxValue = this.oFunc.StringToInt(list7[1]);
				}
				if (list7.Count > 0)
				{
					this._MinValue = this.oFunc.StringToInt(list7[0]);
				}
				this._isValue = true;
			}
			if (list6.Count > 2)
			{
				this._strMask = list6[2];
			}
			this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
			this.Button_FontSize = ((this.oQuestion.QDefine.CONTROL_FONTSIZE == 0) ? SurveyHelper.BtnFontSize : this.oQuestion.QDefine.CONTROL_FONTSIZE);
			if (this.Button_FontSize == -1)
			{
				this.Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			this.Button_FontSize = Math.Abs(this.Button_FontSize);
			this.Button_Height = ((this.oQuestion.QDefine.CONTROL_HEIGHT == 0) ? SurveyHelper.BtnHeight : this.oQuestion.QDefine.CONTROL_HEIGHT);
			this.Button_Width = 440;
			if (this.oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (this.Button_Type == 1 || this.Button_Type == 3)
				{
					this.Button_Width = 280;
				}
			}
			else
			{
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			this.method_1();
			if (!this.ExistTextFill && !this.IsFixOther)
			{
				this.txtFill.Height = 0.0;
				this.txtFillTitle.Height = 0.0;
				this.txtAfter.Height = 0.0;
			}
			else
			{
				this.txtFill.Visibility = Visibility.Visible;
				if (this.oQuestion.QDefine.NOTE == "")
				{
					this.txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					string_ = this.oQuestion.QDefine.NOTE;
					list3 = this.oBoldTitle.ParaToList(string_, "//");
					string_ = list3[0];
					this.oBoldTitle.SetTextBlock(this.txtFillTitle, string_, 0, "", true);
					if (list3.Count > 1)
					{
						string_ = list3[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter, string_, 0, "", true);
					}
				}
				if (this.IsFixOther)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
				}
			}
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			WrapPanel wrapPanel = this.wrapPanel1;
			bool flag = false;
			bool flag2 = false;
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back"))
			{
				if (!(navOperation == "Normal"))
				{
					if (!(navOperation == "Jump"))
					{
					}
				}
				else
				{
					foreach (string text in this.listPreSet)
					{
						if (!this.listFix.Contains(text))
						{
							this.oQuestion.SelectedValues.Add(text);
							foreach (object obj in wrapPanel.Children)
							{
								foreach (object obj2 in ((WrapPanel)((UIElement)obj)).Children)
								{
									UIElement uielement = (UIElement)obj2;
									if (uielement is Button)
									{
										Button button = (Button)uielement;
										if (button.Name.Substring(2) == text)
										{
											button.Style = style;
											int num = (int)button.Tag;
											if (num == 1 || num == 3 || num == 5)
											{
												flag = true;
											}
										}
									}
									else if (uielement is TextBox)
									{
										TextBox textBox = (TextBox)uielement;
										if (textBox.Name.Substring(2) == text)
										{
											textBox.IsEnabled = true;
											textBox.Background = Brushes.White;
										}
									}
								}
							}
						}
					}
					if (flag)
					{
						this.txtFill.IsEnabled = true;
						this.txtFill.Background = Brushes.White;
					}
					if (this.oQuestion.QDetails.Count == 1 || this.listAllButton.Count == 0)
					{
						if (this.listAllButton.Count > 0 && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)) && this.listAllButton[0].Style == style2)
						{
							this.method_2(this.listAllButton[0], null);
						}
						if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (this.txtFill.IsEnabled)
							{
								this.txtFill.Focus();
							}
							else if (!SurveyHelper.AutoFill)
							{
								flag2 = true;
							}
						}
					}
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedValues.Count > 0)
					{
						if (this.txtFill.IsEnabled)
						{
							this.txtFill.Focus();
						}
						else if (!SurveyHelper.AutoFill)
						{
							flag2 = true;
						}
					}
					this.oAutoFill.oLogicEngine = this.oLogicEngine;
					if (SurveyHelper.AutoFill && !flag2)
					{
						this.listButton = this.oAutoFill.MultiButton(this.oQuestion.QDefine, this.listButton, this.listAllButton, 0);
						foreach (Button button2 in this.listButton)
						{
							if (button2.Style == style2)
							{
								this.method_2(button2, null);
							}
						}
						if (this.txtFill.IsEnabled)
						{
							this.txtFill.Text = this.oAutoFill.CommonOther(this.oQuestion.QDefine, "");
						}
						if (this.listButton.Count > 0 && this.oAutoFill.AutoNext(this.oQuestion.QDefine))
						{
							flag2 = true;
						}
					}
					if (flag2)
					{
						this.btnNav_Click(this, e);
					}
				}
			}
			else
			{
				this.oQuestion.ReadAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer surveyAnswer in this.oQuestion.QAnswersRead)
				{
					if (this.method_10(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + "_A").Length) == this.oQuestion.QuestionName + "_A")
					{
						if (this.listFix.Contains(surveyAnswer.CODE))
						{
							continue;
						}
						this.oQuestion.SelectedValues.Add(surveyAnswer.CODE);
						foreach(var child in wrapPanel.Children)
						{
							{
								foreach (object obj4 in ((WrapPanel)((UIElement)child)).Children)
								{
									UIElement uielement2 = (UIElement)obj4;
									if (uielement2 is Button)
									{
										Button button3 = (Button)uielement2;
										if (button3.Name.Substring(2) == surveyAnswer.CODE)
										{
											button3.Style = style;
											int num2 = (int)button3.Tag;
											if (num2 == 1 || num2 == 3 || num2 == 5)
											{
												flag = true;
											}
										}
									}
								}
							}
							continue;
						}
					}
					if (this.ExistTextFill && surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + "_OTH")
					{
						if (surveyAnswer.CODE != "")
						{
							this.txtFill.Text = surveyAnswer.CODE;
						}
					}
					else if (this.ExistCodeFills && this.method_10(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + "_OTH_C").Length) == this.oQuestion.QuestionName + "_OTH_C" && surveyAnswer.CODE != "")
					{
						string b = this.method_10(surveyAnswer.QUESTION_NAME, (this.oQuestion.QuestionName + "_OTH_C").Length, -9999);
						foreach (object obj5 in wrapPanel.Children)
						{
							foreach (object obj6 in ((WrapPanel)obj5).Children)
							{
								UIElement uielement3 = (UIElement)obj6;
								if (uielement3 is TextBox)
								{
									TextBox textBox2 = (TextBox)uielement3;
									if (textBox2.Name.Substring(2) == b)
									{
										textBox2.Text = surveyAnswer.CODE;
										textBox2.IsEnabled = true;
										textBox2.Background = Brushes.White;
										break;
									}
								}
							}
						}
					}
				}
				if (flag)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
				}
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
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
		}

		private void method_1()
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Brush brush = (Brush)base.FindResource("NormalBorderBrush");
			WrapPanel wrapPanel = this.wrapPanel1;
			wrapPanel.Orientation = ((this.Button_Type == 1 || this.Button_Type == 3) ? Orientation.Horizontal : Orientation.Vertical);
			if (this.listFix.Count > 0)
			{
				foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
				{
					if (this.listFix.Contains(surveyDetail.CODE))
					{
						if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3 || surveyDetail.IS_OTHER == 5)
						{
							this.IsFixOther = true;
						}
						if (surveyDetail.IS_OTHER == 2 || surveyDetail.IS_OTHER == 3 || (surveyDetail.IS_OTHER == 13 | surveyDetail.IS_OTHER == 4) || surveyDetail.IS_OTHER == 5 || surveyDetail.IS_OTHER == 14)
						{
							this.IsFixNone = true;
						}
					}
				}
			}
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
			{
				bool flag = false;
				if (surveyDetail2.IS_OTHER == 1 || surveyDetail2.IS_OTHER == 3 || surveyDetail2.IS_OTHER == 5)
				{
					flag = true;
				}
				if (flag)
				{
					this.ExistTextFill = true;
				}
				bool flag2 = false;
				if (surveyDetail2.IS_OTHER == 2 || surveyDetail2.IS_OTHER == 3 || (surveyDetail2.IS_OTHER == 13 | surveyDetail2.IS_OTHER == 4) || surveyDetail2.IS_OTHER == 5 || surveyDetail2.IS_OTHER == 14)
				{
					flag2 = true;
				}
				bool flag3 = this.listFix.Contains(surveyDetail2.CODE);
				bool flag4 = true;
				if (!flag3)
				{
					if (this.IsFixNone)
					{
						flag4 = false;
					}
					else if (flag2 && this.listFix.Count > 0)
					{
						flag4 = false;
					}
				}
				if (flag4)
				{
					WrapPanel wrapPanel2 = new WrapPanel();
					if (this.Button_Type == 0)
					{
						wrapPanel2.Orientation = Orientation.Horizontal;
					}
					else
					{
						wrapPanel2.Orientation = (this.oQuestion.QDefine.CONTROL_MASK.ToUpper().Contains("H") ? Orientation.Horizontal : Orientation.Vertical);
					}
					wrapPanel2.Margin = new Thickness(0.0, 0.0, 13.0, 13.0);
					wrapPanel.Children.Add(wrapPanel2);
					Button button = new Button();
					button.Name = "b_" + surveyDetail2.CODE;
					button.Content = surveyDetail2.CODE_TEXT;
					button.Margin = new Thickness(0.0, 0.0, 2.0, 2.0);
					button.Style = (flag3 ? style : style2);
					if (flag3)
					{
						button.Opacity = 0.5;
					}
					button.Tag = surveyDetail2.IS_OTHER;
					if (!flag3)
					{
						button.Click += this.method_2;
					}
					button.FontSize = (double)this.Button_FontSize;
					button.MinWidth = (double)this.Button_Width;
					button.MinHeight = (double)this.Button_Height;
					wrapPanel2.Children.Add(button);
					if (!flag3)
					{
						this.listAllButton.Add(button);
						if (!flag2 || SurveyHelper.FillMode != "3")
						{
							this.listButton.Add(button);
						}
					}
					if (surveyDetail2.IS_OTHER > 10)
					{
						TextBox textBox = new TextBox();
						textBox.Name = "t_" + surveyDetail2.CODE;
						textBox.Text = "";
						textBox.Tag = surveyDetail2.IS_OTHER;
						textBox.ToolTip = "请在这里填写[" + surveyDetail2.CODE_TEXT + "]的详细说明文本。";
						if (wrapPanel2.Orientation == Orientation.Horizontal)
						{
							textBox.Margin = new Thickness(0.0, 0.0, 15.0, 2.0);
						}
						else
						{
							textBox.Margin = new Thickness(0.0, 0.0, 2.0, 2.0);
						}
						textBox.FontSize = (double)this.Button_FontSize;
						textBox.Width = button.MinWidth;
						textBox.Height = (double)this.Button_Height;
						textBox.MaxLength = this._MaxLen;
						textBox.GotFocus += this.txtFill_GotFocus;
						textBox.LostFocus += this.txtFill_LostFocus;
						if (this._isValue)
						{
							textBox.TextChanged += this.method_7;
						}
						wrapPanel2.Children.Add(textBox);
						textBox.Background = (flag3 ? Brushes.White : Brushes.LightGray);
						textBox.IsEnabled = flag3;
						this.ExistCodeFills = true;
					}
				}
			}
		}

		private void method_2(object sender, RoutedEventArgs e = null)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			int num = (int)button.Tag;
			string text = button.Name.Substring(2);
			int num2 = 0;
			if (num == 2 || num == 3 || num == 5 || num == 13 || num == 4 || num == 14)
			{
				num2 = 1;
			}
			int num3 = 0;
			if (button.Style == style)
			{
				num3 = 1;
			}
			int num4 = 0;
			bool flag = true;
			if (num3 == 0)
			{
				if (num2 == 1)
				{
					this.oQuestion.SelectedValues.Clear();
					num4 = 1;
				}
				else
				{
					num4 = 2;
				}
				this.oQuestion.SelectedValues.Add(text);
				button.Style = style;
			}
			else if (num2 == 1)
			{
				num3 = 2;
			}
			else
			{
				this.oQuestion.SelectedValues.Remove(text);
				button.Style = style2;
			}
			if (num3 < 2)
			{
				bool flag2 = true;
				bool flag3 = true;
				foreach (object obj in this.wrapPanel1.Children)
				{
					Panel panel = (WrapPanel)obj;
					Button button2 = new Button();
					TextBox textBox = new TextBox();
					foreach (object obj2 in panel.Children)
					{
						UIElement uielement = (UIElement)obj2;
						if (uielement is Button)
						{
							button2 = (Button)uielement;
						}
						else if (uielement is TextBox)
						{
							textBox = (TextBox)uielement;
						}
					}
					string text2 = button2.Name.Substring(2);
					if (!this.listFix.Contains(text2))
					{
						int num5 = (int)button2.Tag;
						if (!(text2 == text))
						{
							if (num4 == 1)
							{
								button2.Style = style2;
							}
							else if (num4 == 2 && flag3 && (num5 == 2 || num5 == 3 || num5 == 5 || num5 == 13 || num5 == 4 || num5 == 14) && button2.Style == style)
							{
								button2.Style = style2;
								this.oQuestion.SelectedValues.Remove(text2);
								flag3 = false;
							}
						}
						if (flag2 && button2.Style == style && (num5 == 1 || num5 == 3 || num5 == 5))
						{
							flag2 = false;
						}
						bool flag4 = button2.Style == style;
						if (textBox.Name != "")
						{
							if (flag4)
							{
								textBox.IsEnabled = true;
								textBox.Background = Brushes.White;
								if (textBox.Name.Substring(2) == text)
								{
									flag = false;
									if (SurveyHelper.AutoFill)
									{
										if (textBox.Text == "")
										{
											textBox.Text = this.oAutoFill.CommonOther(this.oQuestion.QDefine, text);
										}
									}
									else if (textBox.Text == "")
									{
										textBox.Focus();
									}
								}
							}
							else
							{
								textBox.Background = Brushes.LightGray;
								textBox.IsEnabled = false;
							}
						}
					}
				}
				if (!this.IsFixOther)
				{
					if (flag2)
					{
						this.txtFill.Background = Brushes.LightGray;
						this.txtFill.IsEnabled = false;
						return;
					}
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
					if (flag && this.txtFill.Text == "")
					{
						this.txtFill.Focus();
					}
				}
			}
		}

		private bool method_3()
		{
			if (this.listFix.Count == 0 && this.oQuestion.SelectedValues.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.oQuestion.QDefine.MIN_COUNT != 0 && this.listFix.Count + this.oQuestion.SelectedValues.Count < this.oQuestion.QDefine.MIN_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAless, this.oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.oQuestion.QDefine.MAX_COUNT != 0 && this.listFix.Count + this.oQuestion.SelectedValues.Count > this.oQuestion.QDefine.MAX_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAmore, this.oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			if (this.txtFill.IsEnabled)
			{
				this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : "");
			}
			if (this.ExistCodeFills)
			{
				this.oQuestion.FillTexts.Clear();
				Panel panel = this.wrapPanel1;
				int num = -1;
				foreach(var child in panel.Children)
				{
					{
						foreach (object obj2 in ((WrapPanel)child).Children)
						{
							UIElement uielement = (UIElement)obj2;
							if (uielement is TextBox)
							{
								TextBox textBox = (TextBox)uielement;
								if (textBox.IsEnabled)
								{
									string text = textBox.Text.Trim();
									if (text == "")
									{
										MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
										textBox.Focus();
										return true;
									}
									if (this._MinValue > 0 && Convert.ToDouble(text) < (double)this._MinValue)
									{
										MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, this._MinValue.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
										textBox.Focus();
										return true;
									}
									if (this._MaxValue > 0 && Convert.ToDouble(text) > (double)this._MaxValue)
									{
										MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, this._MaxValue.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
										textBox.Focus();
										return true;
									}
									if (this._isValue && this._strMask != "")
									{
										string text2 = this._strMask;
										if (text2.IndexOf(",") == -1)
										{
											text2 += ",0";
										}
										string arg = text2.Replace(",", SurveyMsg.MsgFillFitReplace);
										if (this.oLogicEngine.Result(string.Concat(new string[]
										{
											"$NotNum(",
											text,
											",",
											text2,
											")"
										})))
										{
											MessageBox.Show(string.Format(SurveyMsg.MsgFillFit, arg), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
											textBox.Focus();
											return true;
										}
									}
									if (this.oQuestion.QDefine.CONTROL_MASK.ToUpper().Contains("C") && num == 0)
									{
										MessageBox.Show(SurveyMsg.MsgFillPrveText, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
										textBox.Focus();
										return true;
									}
									string key = textBox.Name.Substring(2);
									this.oQuestion.FillTexts.Add(key, textBox.IsEnabled ? text : "");
									num = 1;
								}
								else
								{
									num = 0;
								}
							}
						}
					}
					return false;
				}
			}
			return false;
		}

		private List<VEAnswer> method_4()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			foreach (string item in this.listFix)
			{
				if (!this.oQuestion.SelectedValues.Contains(item))
				{
					this.oQuestion.SelectedValues.Add(item);
				}
			}
			SurveyHelper.Answer = "";
			for (int i = 0; i < this.oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + "_A" + (i + 1).ToString();
				veanswer.CODE = this.oQuestion.SelectedValues[i].ToString();
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer.QUESTION_NAME,
					"=",
					veanswer.CODE
				});
			}
			SurveyHelper.Answer = this.method_10(SurveyHelper.Answer, 1, -9999);
			if (this.oQuestion.FillText != "")
			{
				VEAnswer veanswer2 = new VEAnswer();
				veanswer2.QUESTION_NAME = this.oQuestion.QuestionName + "_OTH";
				veanswer2.CODE = this.oQuestion.FillText;
				list.Add(veanswer2);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer2.QUESTION_NAME,
					"=",
					this.oQuestion.FillText
				});
			}
			foreach (string text in this.oQuestion.FillTexts.Keys)
			{
				VEAnswer veanswer3 = new VEAnswer();
				veanswer3.QUESTION_NAME = this.oQuestion.QuestionName + "_OTH_C" + text;
				veanswer3.CODE = this.oQuestion.FillTexts[text];
				list.Add(veanswer3);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer3.QUESTION_NAME,
					"=",
					veanswer3.CODE
				});
			}
			return list;
		}

		private void method_5(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				this.oPageNav.PageDataLog(SurveyMsg.DelaySeconds, list_0, this.btnNav, this.MySurveyId);
			}
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_3())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_4();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_5(list);
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

		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		private void method_6(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
		}

		private void method_7(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			TextChange[] array = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				bool flag;
				if (textBox.Text.Substring(offset, array[0].AddedLength).Trim() == "")
				{
					flag = true;
				}
				else
				{
					double num = 0.0;
					flag = !double.TryParse(textBox.Text, out num);
				}
				if (flag)
				{
					textBox.Text = textBox.Text.Remove(offset, array[0].AddedLength);
					textBox.Select(offset, 0);
				}
			}
		}

		private string method_8(string string_0, int int_0, int int_1 = -9999)
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

		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		private string method_10(string string_0, int int_0, int int_1 = -9999)
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

		private string method_11(string string_0, int int_0 = 1)
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

		private AutoFill oAutoFill = new AutoFill();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QMultiple oQuestion = new QMultiple();

		private bool ExistTextFill;

		private bool ExistCodeFills;

		private List<string> listPreSet = new List<string>();

		private List<string> listFix = new List<string>();

		private bool IsFixOther;

		private bool IsFixNone;

		private List<Button> listAllButton = new List<Button>();

		private List<Button> listButton = new List<Button>();

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private bool _isValue;

		private string _strMask = "";

		private int _MinValue = -1;

		private int _MaxValue = -1;

		private int _MaxLen = 250;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class25
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly MultipleText.Class25 instance = new MultipleText.Class25();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
