using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch.View
{
	public partial class MultipleGroup : Page
	{
		public MultipleGroup()
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
                    list4 = list4.OrderBy(p=>p.INNER_ORDER).ToList();
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
				this.ShowLogic = true;
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
			else if (this.oQuestion.QDefine.IS_RANDOM == 1 || this.oQuestion.QDefine.IS_RANDOM == 3)
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
			double opacity = 0.2;
			Grid gridContent = this.GridContent;
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
							bool flag3 = false;
                            foreach (Border child in gridContent.Children)
                            {
                                {
                                    WrapPanel wrapPanel = (WrapPanel)child.Child;
                                    if (wrapPanel.Name.Substring(1, 1) == "R")
									{
										foreach (object obj in wrapPanel.Children)
										{
											foreach (object obj2 in ((WrapPanel)obj).Children)
											{
												UIElement uielement = (UIElement)obj2;
												if (uielement is Button)
												{
													Button button = (Button)uielement;
													if (button.Name.Substring(2) == text)
													{
														button.Style = style;
														flag3 = true;
														int num = (int)Convert.ToInt16(((string)button.Tag).Substring(0, 2).Trim());
														if (num == 1 || num == 3 || num == 5)
														{
															flag = true;
														}
													}
												}
												else if (uielement is Image)
												{
													Image image = (Image)uielement;
													if (image.Name.Substring(2) == text)
													{
														image.Opacity = opacity;
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
											if (flag3)
											{
												break;
											}
										}
										if (flag3)
										{
											break;
										}
									}
								}
								goto IL_E43;
							}
							IL_E2C:
							this.oQuestion.SelectedValues.Add(text);
							continue;
							IL_E43:
							if (flag3)
							{
								goto IL_E2C;
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
							this.method_2(this.listAllButton[0], new RoutedEventArgs());
						}
						if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (this.txtFill.IsEnabled)
							{
								this.txtFill.Focus();
							}
							else
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
						else
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
					if (this.method_9(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + "_A").Length) == this.oQuestion.QuestionName + "_A")
					{
						if (!this.listFix.Contains(surveyAnswer.CODE))
						{
							bool flag4 = false;
							foreach(var child in gridContent.Children)
							{
								{
									WrapPanel wrapPanel2 = (WrapPanel)((Border)child).Child;
									if (wrapPanel2.Name.Substring(1, 1) == "R")
									{
										foreach (object obj3 in wrapPanel2.Children)
										{
											foreach (object obj4 in ((WrapPanel)obj3).Children)
											{
												UIElement uielement2 = (UIElement)obj4;
												if (uielement2 is Button)
												{
													Button button3 = (Button)uielement2;
													if (button3.Name.Substring(2) == surveyAnswer.CODE)
													{
														button3.Style = style;
														flag4 = true;
														int num2 = (int)Convert.ToInt16(((string)button3.Tag).Substring(0, 2).Trim());
														if (num2 == 1 || num2 == 3 || num2 == 5)
														{
															flag = true;
														}
													}
												}
												else if (uielement2 is Image)
												{
													Image image2 = (Image)uielement2;
													if (image2.Name.Substring(2) == surveyAnswer.CODE)
													{
														image2.Opacity = opacity;
													}
												}
											}
											if (flag4)
											{
												break;
											}
										}
										if (flag4)
										{
											break;
										}
									}
								}
							}
							if (flag4)
							{
								this.oQuestion.SelectedValues.Add(surveyAnswer.CODE);
							}
						}
					}
					else if (this.ExistTextFill && surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + "_OTH")
					{
						if (surveyAnswer.CODE != "")
						{
							this.txtFill.Text = surveyAnswer.CODE;
						}
					}
					else if (this.ExistCodeFills && this.method_9(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + "_OTH_C").Length) == this.oQuestion.QuestionName + "_OTH_C" && surveyAnswer.CODE != "")
					{
						string b = this.method_9(surveyAnswer.QUESTION_NAME, (this.oQuestion.QuestionName + "_OTH_C").Length, -9999);
						foreach (object obj5 in gridContent.Children)
						{
							WrapPanel wrapPanel3 = (WrapPanel)((Border)obj5).Child;
							if (wrapPanel3.Name.Substring(1, 1) == "R")
							{
								foreach (object obj6 in wrapPanel3.Children)
								{
									foreach (object obj7 in ((WrapPanel)obj6).Children)
									{
										UIElement uielement3 = (UIElement)obj7;
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
			double num = 0.1;
			double num2 = 1.0;
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			Style style3 = (Style)base.FindResource("ContentMediumStyle");
			Brush borderBrush = (Brush)base.FindResource("NormalBorderBrush");
			Brush foreground = (Brush)base.FindResource("PressedBrush");
			string text = this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim();
			string text2 = "R";
			string text3 = "C";
			int num3 = 0;
			if (text != "")
			{
				text2 = this.method_8(text, 1);
				if ("0123456789".Contains(text2))
				{
					text2 = "R";
					if (text != "")
					{
						num3 = Convert.ToInt32(text);
					}
				}
				else
				{
					text = this.method_9(text, 1, -9999);
					text3 = this.method_8(text, 1);
					if ("0123456789".Contains(text3))
					{
						if (text2 != "T" && text2 != "B")
						{
							text3 = "C";
						}
						if (text != "")
						{
							num3 = Convert.ToInt32(text);
						}
					}
					else if (this.method_9(text, 1, -9999) != "")
					{
						num3 = Convert.ToInt32(this.method_9(text, 1, -9999));
					}
				}
				text = text2 + text3;
				if (text.Contains("L"))
				{
					text2 = "L";
				}
				else if (text.Contains("R"))
				{
					text2 = "R";
				}
				if (text.Contains("T"))
				{
					text3 = "T";
				}
				else if (text.Contains("B"))
				{
					text3 = "B";
				}
				if (text.Contains("C") && (text.Contains("T") || text.Contains("B")))
				{
					text2 = "C";
				}
				if (text.Contains("C") && (text.Contains("R") || text.Contains("L")))
				{
					text3 = "C";
				}
				if (text == "C" || text == "CC")
				{
					text2 = "C";
					text3 = "C";
				}
			}
			HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
			if (text2 == "C")
			{
				horizontalAlignment = HorizontalAlignment.Center;
			}
			else if (text2 == "L")
			{
				horizontalAlignment = HorizontalAlignment.Left;
			}
			VerticalAlignment verticalAlignment = VerticalAlignment.Center;
			if (text3 == "T")
			{
				verticalAlignment = VerticalAlignment.Top;
			}
			else if (text3 == "B")
			{
				verticalAlignment = VerticalAlignment.Bottom;
			}
			Orientation orientation = (this.Button_Type == 2 || this.Button_Type == 4) ? Orientation.Vertical : Orientation.Horizontal;
			Orientation orientation2 = (this.oQuestion.QDefine.CONTROL_MASK == "H") ? Orientation.Horizontal : Orientation.Vertical;
			Grid gridContent = this.GridContent;
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
						if (surveyDetail.IS_OTHER == 2 || surveyDetail.IS_OTHER == 3 || surveyDetail.IS_OTHER == 13)
						{
							this.IsFixNone = true;
						}
					}
				}
			}
			this.oQuestion.GetGroupDetails();
			if (this.ShowLogic)
			{
				List<SurveyDetail> list = new List<SurveyDetail>();
				foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
				{
					bool flag = true;
					foreach (SurveyDetail surveyDetail3 in list)
					{
						if (surveyDetail2.PARENT_CODE == surveyDetail3.CODE)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						foreach (SurveyDetail surveyDetail4 in this.oQuestion.QGroupDetails)
						{
							if (surveyDetail2.PARENT_CODE == surveyDetail4.CODE)
							{
								list.Add(surveyDetail4);
								break;
							}
						}
					}
				}
				this.oQuestion.QGroupDetails = list;
			}
			else if (this.oQuestion.QDefine.IS_RANDOM == 2 || this.oQuestion.QDefine.IS_RANDOM == 1)
			{
				this.oQuestion.QGroupDetails = this.oQuestion.RandomDetails(this.oQuestion.QGroupDetails);
			}
			int num4 = 0;
			foreach (SurveyDetail surveyDetail5 in this.oQuestion.QGroupDetails)
			{
				string code_TEXT = surveyDetail5.CODE_TEXT;
                var enumerable = this.oQuestion.QDetails.Where(p=>p.PARENT_CODE == surveyDetail5.CODE);
				List<string> list2 = new List<string>();
				bool flag2 = false;
				bool flag3 = false;
				foreach (var class2 in enumerable)
				{
					if (this.listFix.Contains(class2.CODE))
					{
						flag3 = true;
						if (class2.IS_OTHER == 2 || class2.IS_OTHER == 3 || (class2.IS_OTHER == 13 | class2.IS_OTHER == 4) || class2.IS_OTHER == 5 || class2.IS_OTHER == 14)
						{
							flag2 = true;
						}
					}
				}
				foreach (var class3 in enumerable)
				{
					if (this.listFix.Contains(class3.CODE))
					{
						list2.Add(class3.CODE);
					}
					else if (!flag2 && !this.IsFixNone)
					{
						bool flag4 = false;
						bool flag5 = false;
						if (class3.IS_OTHER == 2 || class3.IS_OTHER == 3 || class3.IS_OTHER == 13)
						{
							flag4 = true;
						}
						if (class3.IS_OTHER == 4 || class3.IS_OTHER == 5 || class3.IS_OTHER == 14)
						{
							flag5 = true;
						}
						if ((this.listFix.Count <= 0 || !flag4) && (!flag3 || !flag5))
						{
							list2.Add(class3.CODE);
						}
					}
				}
				if (list2.Count<string>() > 0)
				{
					gridContent.RowDefinitions.Add(new RowDefinition
					{
						Height = GridLength.Auto
					});
					Border border = new Border();
					border.BorderThickness = new Thickness((double)((code_TEXT == "" | this.oQuestion.QDefine.CONTROL_TOOLTIP == "0") ? 0 : 1));
					border.BorderBrush = borderBrush;
					border.SetValue(Grid.RowProperty, num4);
					border.SetValue(Grid.ColumnProperty, 0);
					gridContent.Children.Add(border);
					WrapPanel wrapPanel = new WrapPanel();
					wrapPanel.VerticalAlignment = verticalAlignment;
					wrapPanel.HorizontalAlignment = horizontalAlignment;
					wrapPanel.Name = "wL" + surveyDetail5.CODE;
					border.Child = wrapPanel;
					TextBlock textBlock = new TextBlock();
					textBlock.Text = ((this.oQuestion.QDefine.CONTROL_TOOLTIP == "0") ? "" : code_TEXT);
					textBlock.Style = style3;
					textBlock.Foreground = foreground;
					if (num3 > 0)
					{
						textBlock.Width = (double)num3;
					}
					textBlock.TextWrapping = TextWrapping.Wrap;
					textBlock.Margin = new Thickness(15.0, 0.0, 15.0, 0.0);
					textBlock.VerticalAlignment = verticalAlignment;
					wrapPanel.Children.Add(textBlock);
					border = new Border();
					border.BorderThickness = new Thickness((double)((this.oQuestion.QDefine.CONTROL_TOOLTIP == "0") ? 0 : 1));
					border.BorderBrush = borderBrush;
					border.SetValue(Grid.RowProperty, num4);
					border.SetValue(Grid.ColumnProperty, 1);
					gridContent.Children.Add(border);
					WrapPanel wrapPanel2 = new WrapPanel();
					wrapPanel2.Orientation = orientation;
					wrapPanel2.Margin = new Thickness(15.0, 0.0, 0.0, 13.0);
					wrapPanel2.Name = "wR" + surveyDetail5.CODE;
					border.Child = wrapPanel2;
					foreach (var class4 in enumerable)
					{
						if (list2.Contains(class4.CODE))
						{
							if (class4.IS_OTHER == 1 || class4.IS_OTHER == 3 || class4.IS_OTHER == 5)
							{
								this.ExistTextFill = true;
							}
							bool flag6 = this.listFix.Contains(class4.CODE);
							WrapPanel wrapPanel3 = new WrapPanel();
							wrapPanel3.Margin = new Thickness(0.0, 15.0, 15.0, 0.0);
							wrapPanel3.Orientation = orientation2;
							wrapPanel2.Children.Add(wrapPanel3);
							Button button = new Button();
							button.Name = "b_" + class4.CODE;
							button.Content = class4.CODE_TEXT;
							button.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
							button.Style = (flag6 ? style : style2);
							if (flag6)
							{
								button.Opacity = 0.5;
							}
							button.Tag = (class4.IS_OTHER + " ").Substring(0, 2) + "G" + surveyDetail5.CODE;
							if (!flag6)
							{
								button.Click += this.method_2;
							}
							button.FontSize = (double)this.Button_FontSize;
							button.MinWidth = (double)this.Button_Width;
							button.MinHeight = (double)this.Button_Height;
							wrapPanel3.Children.Add(button);
							if (!flag6)
							{
								this.listAllButton.Add(button);
								int is_OTHER = class4.IS_OTHER;
								int num5 = 0;
								if (is_OTHER == 2 || is_OTHER == 3)
								{
									goto IL_C3D;
								}
								if (is_OTHER == 13)
								{
									goto IL_C3D;
								}
								if (is_OTHER == 4 || is_OTHER == 5 || is_OTHER == 14)
								{
									num5 = 2;
								}
								IL_C40:
								if (num5 == 0 || SurveyHelper.FillMode != "3")
								{
									this.listButton.Add(button);
									goto IL_C67;
								}
								goto IL_C67;
								IL_C3D:
								num5 = 1;
								goto IL_C40;
							}
							IL_C67:
							if (class4.IS_OTHER > 10)
							{
								TextBox textBox = new TextBox();
								textBox.Name = "t_" + class4.CODE;
								textBox.Text = "";
								textBox.Tag = (class4.IS_OTHER + " ").Substring(0, 2) + "G" + surveyDetail5.CODE;
								textBox.ToolTip = "请在这里填写[" + class4.CODE_TEXT + "]的详细说明文本。";
								if (wrapPanel3.Orientation == Orientation.Horizontal)
								{
									textBox.Margin = new Thickness(0.0, 0.0, 15.0, 2.0);
								}
								else
								{
									textBox.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
								}
								textBox.FontSize = (double)this.Button_FontSize;
								textBox.Width = (double)this.Button_Width;
								textBox.Height = (double)this.Button_Height;
								textBox.MaxLength = 250;
								textBox.GotFocus += this.txtFill_GotFocus;
								textBox.LostFocus += this.txtFill_LostFocus;
								wrapPanel3.Children.Add(textBox);
								textBox.Background = (flag6 ? Brushes.White : Brushes.LightGray);
								textBox.IsEnabled = flag6;
								this.ExistCodeFills = true;
							}
							string extend_ = class4.EXTEND_1;
							if (extend_ != "")
							{
								Image image = new Image();
								image.Name = "p_" + class4.CODE;
								image.Tag = (class4.IS_OTHER + " ").Substring(0, 2) + "G" + surveyDetail5.CODE;
								image.MinHeight = 46.0;
								image.Width = (double)this.Button_Width;
								image.Stretch = Stretch.Uniform;
								if (wrapPanel3.Orientation == Orientation.Horizontal)
								{
									image.Margin = new Thickness(0.0, 0.0, 15.0, 2.0);
								}
								else
								{
									image.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
								}
								image.Opacity = (flag6 ? num : num2);
								try
								{
									string text4 = Environment.CurrentDirectory + "\\Media\\" + extend_;
									if (this.method_8(extend_, 1) == "#")
									{
										text4 = "..\\Resources\\Pic\\" + this.method_9(extend_, 1, -9999);
									}
									else if (!File.Exists(text4))
									{
										text4 = "..\\Resources\\Pic\\" + extend_;
									}
									BitmapImage bitmapImage = new BitmapImage();
									bitmapImage.BeginInit();
									bitmapImage.UriSource = new Uri(text4, UriKind.RelativeOrAbsolute);
									bitmapImage.EndInit();
									image.Source = bitmapImage;
									if (!flag6)
									{
										image.MouseLeftButtonUp += new MouseButtonEventHandler(this.method_3);
									}
									wrapPanel3.Children.Add(image);
									if (this.Button_Hide)
									{
										button.Visibility = Visibility.Collapsed;
									}
								}
								catch (Exception)
								{
								}
							}
						}
					}
					num4++;
				}
			}
		}

		private void method_2(object sender, RoutedEventArgs e = null)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			double num = 0.2;
			double num2 = 1.0;
			Button button = (Button)sender;
			int num3 = (int)Convert.ToInt16(((string)button.Tag).Substring(0, 2).Trim());
			string text = button.Name.Substring(2);
			string text2 = ((string)button.Tag).Substring(3);
			int num4 = 0;
			if (num3 != 2 && num3 != 3)
			{
				if (num3 != 13)
				{
					if (num3 == 4 || num3 == 5 || num3 == 14)
					{
						num4 = 2;
						goto IL_B7;
					}
					goto IL_B7;
				}
			}
			num4 = 1;
			IL_B7:
			int num5 = 0;
			if (button.Style == style)
			{
				num5 = 1;
			}
			int num6 = 0;
			bool flag = true;
			if (num5 == 0)
			{
				if (num4 == 1)
				{
					this.oQuestion.SelectedValues.Clear();
					num6 = 1;
				}
				else if (num4 == 2)
				{
					num6 = 3;
				}
				else
				{
					num6 = 2;
				}
				this.oQuestion.SelectedValues.Add(text);
				button.Style = style;
			}
			else if (num4 == 1)
			{
				num5 = 2;
			}
			else
			{
				this.oQuestion.SelectedValues.Remove(text);
				button.Style = style2;
			}
			if (num5 < 2)
			{
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				bool flag5 = true;
				bool flag6 = true;
				foreach (object obj in this.GridContent.Children)
				{
					WrapPanel wrapPanel = (WrapPanel)((Border)obj).Child;
					if (wrapPanel.Name.Substring(1, 1) == "R")
					{
						foreach (object obj2 in wrapPanel.Children)
						{
							foreach (object obj3 in ((WrapPanel)obj2).Children)
							{
								UIElement uielement = (UIElement)obj3;
								if (uielement is Button)
								{
									Button button2 = (Button)uielement;
									int num7 = (int)Convert.ToInt16(((string)button2.Tag).Substring(0, 2).Trim());
									string text3 = button2.Name.Substring(2);
									string b = ((string)button2.Tag).Substring(3);
									if (!this.listFix.Contains(text3))
									{
										if (!(text3 == text))
										{
											if (num6 == 1)
											{
												button2.Style = style2;
											}
											else if (num6 == 2 || num6 == 3)
											{
												if (flag3 && (num7 == 2 || num7 == 3 || num7 == 13) && button2.Style == style)
												{
													button2.Style = style2;
													this.oQuestion.SelectedValues.Remove(text3);
													flag3 = false;
												}
												if (flag4 && text2 == b && (num7 == 4 || num7 == 5 || num7 == 14) && button2.Style == style)
												{
													button2.Style = style2;
													this.oQuestion.SelectedValues.Remove(text3);
													flag4 = false;
												}
											}
											if (num6 == 3 && text2 == b)
											{
												button2.Style = style2;
												this.oQuestion.SelectedValues.Remove(text3);
											}
										}
										if (flag2 && button2.Style == style && (num7 == 1 || num7 == 3 || num7 == 5 || num7 == 3))
										{
											flag2 = false;
										}
									}
								}
								if (uielement is Image)
								{
									Image image = (Image)uielement;
									int num8 = (int)Convert.ToInt16(((string)image.Tag).Substring(0, 2).Trim());
									string text4 = image.Name.Substring(2);
									string b2 = ((string)image.Tag).Substring(3);
									if (!this.listFix.Contains(text4))
									{
										if (text4 == text)
										{
											image.Opacity = ((button.Style == style) ? num : num2);
										}
										else
										{
											if (num6 == 1)
											{
												image.Opacity = num2;
											}
											else if (num6 == 2 || num6 == 3)
											{
												if (flag5 && (num8 == 2 || num8 == 3 || num8 == 13) && image.Opacity == num)
												{
													image.Opacity = num2;
													flag5 = false;
												}
												if (flag6 && text2 == b2 && (num8 == 4 || num8 == 5 || num8 == 14) && image.Opacity == num)
												{
													image.Opacity = num2;
													flag6 = false;
												}
											}
											if (num6 == 3 && text2 == b2)
											{
												image.Opacity = num2;
											}
										}
									}
								}
								else if (uielement is TextBox)
								{
									TextBox textBox = (TextBox)uielement;
									int num9 = (int)Convert.ToInt16(((string)textBox.Tag).Substring(0, 2).Trim());
									string text5 = textBox.Name.Substring(2);
									string a = ((string)textBox.Tag).Substring(3);
									if (!this.listFix.Contains(text5))
									{
										if (text5 == text)
										{
											if (button.Style == style)
											{
												textBox.IsEnabled = true;
												textBox.Background = Brushes.White;
												if (SurveyHelper.AutoFill)
												{
													if (textBox.Text == "")
													{
														textBox.Text = this.oAutoFill.CommonOther(this.oQuestion.QDefine, text5);
													}
												}
												else if (textBox.Text == "")
												{
													textBox.Focus();
												}
												flag = false;
											}
											else
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
										}
										else
										{
											if (num6 == 1)
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
											else if ((num6 == 2 || num6 == 3) && (num9 == 13 || (num9 == 14 && a == text2)))
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
											if (num6 == 3 && a == text2)
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
										}
									}
								}
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

		private void method_3(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			double num = 0.2;
			double opacity = 1.0;
			Image image = (Image)sender;
			int num2 = (int)Convert.ToInt16(((string)image.Tag).Substring(0, 2).Trim());
			string text = image.Name.Substring(2);
			string text2 = ((string)image.Tag).Substring(3);
			int num3 = 0;
			if (num2 != 2 && num2 != 3)
			{
				if (num2 != 13)
				{
					if (num2 == 4 || num2 == 5 || num2 == 14)
					{
						num3 = 2;
						goto IL_B7;
					}
					goto IL_B7;
				}
			}
			num3 = 1;
			IL_B7:
			int num4 = 0;
			if (image.Opacity == num)
			{
				num4 = 1;
			}
			int num5 = 0;
			bool flag = true;
			if (num4 == 0)
			{
				if (num3 == 1)
				{
					this.oQuestion.SelectedValues.Clear();
					num5 = 1;
				}
				else if (num3 == 2)
				{
					num5 = 3;
				}
				else
				{
					num5 = 2;
				}
				this.oQuestion.SelectedValues.Add(text);
				image.Opacity = num;
			}
			else if (num3 == 1)
			{
				num4 = 2;
			}
			else
			{
				this.oQuestion.SelectedValues.Remove(text);
				image.Opacity = opacity;
			}
			if (num4 < 2)
			{
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				bool flag5 = true;
				bool flag6 = true;
				foreach (object obj in this.GridContent.Children)
				{
					WrapPanel wrapPanel = (WrapPanel)((Border)obj).Child;
					if (wrapPanel.Name.Substring(1, 1) == "R")
					{
						foreach (object obj2 in wrapPanel.Children)
						{
							foreach (object obj3 in ((WrapPanel)obj2).Children)
							{
								UIElement uielement = (UIElement)obj3;
								if (uielement is Button)
								{
									Button button = (Button)uielement;
									int num6 = (int)Convert.ToInt16(((string)button.Tag).Substring(0, 2).Trim());
									string text3 = button.Name.Substring(2);
									string b = ((string)button.Tag).Substring(3);
									if (!this.listFix.Contains(text3))
									{
										if (text3 == text)
										{
											if (image.Opacity == num)
											{
												button.Style = style;
											}
											else
											{
												button.Style = style2;
											}
										}
										else
										{
											if (num5 == 1)
											{
												button.Style = style2;
											}
											else if (num5 == 2 || num5 == 3)
											{
												if (flag3 && (num6 == 2 || num6 == 3 || num6 == 13) && button.Style == style)
												{
													button.Style = style2;
													this.oQuestion.SelectedValues.Remove(text3);
													flag3 = false;
												}
												if (flag4 && text2 == b && (num6 == 4 || num6 == 5 || num6 == 14) && button.Style == style)
												{
													button.Style = style2;
													this.oQuestion.SelectedValues.Remove(text3);
													flag4 = false;
												}
											}
											if (num5 == 3 && text2 == b)
											{
												button.Style = style2;
												this.oQuestion.SelectedValues.Remove(text3);
											}
										}
										if (flag2 && button.Style == style && (num6 == 1 || num6 == 3 || num6 == 5))
										{
											flag2 = false;
										}
									}
								}
								if (uielement is Image)
								{
									Image image2 = (Image)uielement;
									int num7 = (int)Convert.ToInt16(((string)image2.Tag).Substring(0, 2).Trim());
									string text4 = image2.Name.Substring(2);
									string b2 = ((string)image2.Tag).Substring(3);
									if (!this.listFix.Contains(text4) && !(text4 == text))
									{
										if (num5 == 1)
										{
											image2.Opacity = opacity;
										}
										else if (num5 == 2 || num5 == 3)
										{
											if (flag5 && (num7 == 2 || num7 == 3 || num7 == 13) && image2.Opacity == num)
											{
												image2.Opacity = opacity;
												flag5 = false;
											}
											if (flag6 && text2 == b2 && (num7 == 4 || num7 == 5 || num7 == 14) && image2.Opacity == num)
											{
												image2.Opacity = opacity;
												flag6 = false;
											}
										}
										if (num5 == 3 && text2 == b2)
										{
											image2.Opacity = opacity;
										}
									}
								}
								else if (uielement is TextBox)
								{
									TextBox textBox = (TextBox)uielement;
									int num8 = (int)Convert.ToInt16(((string)textBox.Tag).Substring(0, 2).Trim());
									string text5 = textBox.Name.Substring(2);
									string a = ((string)textBox.Tag).Substring(3);
									if (!this.listFix.Contains(text5))
									{
										if (text5 == text)
										{
											if (image.Opacity == num)
											{
												textBox.IsEnabled = true;
												textBox.Background = Brushes.White;
												if (textBox.Text == "")
												{
													textBox.Focus();
												}
												flag = false;
											}
											else
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
										}
										else
										{
											if (num5 == 1)
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
											else if ((num5 == 2 || num5 == 3) && (num8 == 13 || (num8 == 14 && a == text2)))
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
											if (num5 == 3 && a == text2)
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
										}
									}
								}
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

		private bool method_4()
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
			this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : "");
			if (this.ExistCodeFills)
			{
				this.oQuestion.FillTexts.Clear();
				foreach(var child in this.GridContent.Children)
				{
					{
						WrapPanel wrapPanel = (WrapPanel)((Border)child).Child;
						if (wrapPanel.Name.Substring(1, 1) == "R")
						{
							foreach (object obj2 in wrapPanel.Children)
							{
								foreach (object obj3 in ((WrapPanel)obj2).Children)
								{
									UIElement uielement = (UIElement)obj3;
									if (uielement is TextBox)
									{
										TextBox textBox = (TextBox)uielement;
										if (textBox.IsEnabled && textBox.Text.Trim() == "")
										{
											MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
											textBox.Focus();
											return true;
										}
										string key = textBox.Name.Substring(2);
										this.oQuestion.FillTexts.Add(key, textBox.IsEnabled ? textBox.Text.Trim() : "");
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		private List<VEAnswer> method_5()
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
			SurveyHelper.Answer = this.method_9(SurveyHelper.Answer, 1, -9999);
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

		private void method_6(List<VEAnswer> list_0)
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

		private QMultiple oQuestion = new QMultiple();

		private bool ExistTextFill;

		private bool ExistCodeFills;

		private List<string> listPreSet = new List<string>();

		private List<string> listFix = new List<string>();

		private bool IsFixOther;

		private bool IsFixNone;

		private List<Button> listAllButton = new List<Button>();

		private List<Button> listButton = new List<Button>();

		private bool ShowLogic;

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
