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
	// Token: 0x02000024 RID: 36
	public partial class MultipleText : Page
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00043AC8 File Offset: 0x00041CC8
		public MultipleText()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00043B9C File Offset: 0x00041D9C
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0, false);
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
			string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add(global::GClass0.smethod_0(""));
			if (show_LOGIC != global::GClass0.smethod_0(""))
			{
				list2 = this.oBoldTitle.ParaToList(show_LOGIC, global::GClass0.smethod_0("-Į"));
				if (list2.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list2[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list3[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list3.Count > 1) ? list3[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
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
				if (this.oQuestion.QDefine.SHOW_LOGIC == global::GClass0.smethod_0("") && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list4.Sort(new Comparison<SurveyDetail>(MultipleText.Class25.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.FIX_LOGIC != global::GClass0.smethod_0(""))
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
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
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
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int l = 0; l < this.oQuestion.QDetails.Count<SurveyDetail>(); l++)
				{
					this.oQuestion.QDetails[l].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[l].CODE_TEXT);
				}
			}
			if (list2[0].Trim() != global::GClass0.smethod_0(""))
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
			List<string> list6 = this.oFunc.StringToList(this.oQuestion.QDefine.CONTROL_TOOLTIP, global::GClass0.smethod_0("-Į"));
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
				List<string> list7 = this.oFunc.StringToList(list6[1], global::GClass0.smethod_0("-"));
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
				if (this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0(""))
				{
					this.txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					string_ = this.oQuestion.QDefine.NOTE;
					list3 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
					string_ = list3[0];
					this.oBoldTitle.SetTextBlock(this.txtFillTitle, string_, 0, global::GClass0.smethod_0(""), true);
					if (list3.Count > 1)
					{
						string_ = list3[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter, string_, 0, global::GClass0.smethod_0(""), true);
					}
				}
				if (this.IsFixOther)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
				}
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapPanel1;
			bool flag = false;
			bool flag2 = false;
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")))
			{
				if (!(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")))
				{
					if (!(navOperation == global::GClass0.smethod_0("NŶɯͱ")))
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
							this.txtFill.Text = this.oAutoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
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
					if (this.method_10(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ")).Length) == this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ"))
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
					if (this.ExistTextFill && surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"))
					{
						if (surveyAnswer.CODE != global::GClass0.smethod_0(""))
						{
							this.txtFill.Text = surveyAnswer.CODE;
						}
					}
					else if (this.ExistCodeFills && this.method_10(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + global::GClass0.smethod_0("YŊɐ͋ѝՂ")).Length) == this.oQuestion.QuestionName + global::GClass0.smethod_0("YŊɐ͋ѝՂ") && surveyAnswer.CODE != global::GClass0.smethod_0(""))
					{
						string b = this.method_10(surveyAnswer.QUESTION_NAME, (this.oQuestion.QuestionName + global::GClass0.smethod_0("YŊɐ͋ѝՂ")).Length, -9999);
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

		// Token: 0x0600021D RID: 541 RVA: 0x000451C4 File Offset: 0x000433C4
		private void method_1()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Brush brush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
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
						wrapPanel2.Orientation = (this.oQuestion.QDefine.CONTROL_MASK.ToUpper().Contains(global::GClass0.smethod_0("I")) ? Orientation.Horizontal : Orientation.Vertical);
					}
					wrapPanel2.Margin = new Thickness(0.0, 0.0, 13.0, 13.0);
					wrapPanel.Children.Add(wrapPanel2);
					Button button = new Button();
					button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail2.CODE;
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
						if (!flag2 || SurveyHelper.FillMode != global::GClass0.smethod_0("2"))
						{
							this.listButton.Add(button);
						}
					}
					if (surveyDetail2.IS_OTHER > 10)
					{
						TextBox textBox = new TextBox();
						textBox.Name = global::GClass0.smethod_0("vŞ") + surveyDetail2.CODE;
						textBox.Text = global::GClass0.smethod_0("");
						textBox.Tag = surveyDetail2.IS_OTHER;
						textBox.ToolTip = global::GClass0.smethod_0("诰嘮跜鋈屨咛ٚ") + surveyDetail2.CODE_TEXT + global::GClass0.smethod_0("T瞌觡緀迱挊掄怮㠃");
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

		// Token: 0x0600021E RID: 542 RVA: 0x0004576C File Offset: 0x0004396C
		private void method_2(object sender, RoutedEventArgs e = null)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
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
						if (textBox.Name != global::GClass0.smethod_0(""))
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
										if (textBox.Text == global::GClass0.smethod_0(""))
										{
											textBox.Text = this.oAutoFill.CommonOther(this.oQuestion.QDefine, text);
										}
									}
									else if (textBox.Text == global::GClass0.smethod_0(""))
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
					if (flag && this.txtFill.Text == global::GClass0.smethod_0(""))
					{
						this.txtFill.Focus();
					}
				}
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00045B58 File Offset: 0x00043D58
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
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			if (this.txtFill.IsEnabled)
			{
				this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : global::GClass0.smethod_0(""));
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
									if (text == global::GClass0.smethod_0(""))
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
									if (this._isValue && this._strMask != global::GClass0.smethod_0(""))
									{
										string text2 = this._strMask;
										if (text2.IndexOf(global::GClass0.smethod_0("-")) == -1)
										{
											text2 += global::GClass0.smethod_0(".ı");
										}
										string arg = text2.Replace(global::GClass0.smethod_0("-"), SurveyMsg.MsgFillFitReplace);
										if (this.oLogicEngine.Result(string.Concat(new string[]
										{
											global::GClass0.smethod_0(",ŉɩͱъնٯܩ"),
											text,
											global::GClass0.smethod_0("-"),
											text2,
											global::GClass0.smethod_0("(")
										})))
										{
											MessageBox.Show(string.Format(SurveyMsg.MsgFillFit, arg), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
											textBox.Focus();
											return true;
										}
									}
									if (this.oQuestion.QDefine.CONTROL_MASK.ToUpper().Contains(global::GClass0.smethod_0("B")) && num == 0)
									{
										MessageBox.Show(SurveyMsg.MsgFillPrveText, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
										textBox.Focus();
										return true;
									}
									string key = textBox.Name.Substring(2);
									this.oQuestion.FillTexts.Add(key, textBox.IsEnabled ? text : global::GClass0.smethod_0(""));
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

		// Token: 0x06000220 RID: 544 RVA: 0x00046054 File Offset: 0x00044254
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
			SurveyHelper.Answer = global::GClass0.smethod_0("");
			for (int i = 0; i < this.oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ") + (i + 1).ToString();
				veanswer.CODE = this.oQuestion.SelectedValues[i].ToString();
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					veanswer.CODE
				});
			}
			SurveyHelper.Answer = this.method_10(SurveyHelper.Answer, 1, -9999);
			if (this.oQuestion.FillText != global::GClass0.smethod_0(""))
			{
				VEAnswer veanswer2 = new VEAnswer();
				veanswer2.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
				veanswer2.CODE = this.oQuestion.FillText;
				list.Add(veanswer2);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer2.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					this.oQuestion.FillText
				});
			}
			foreach (string text in this.oQuestion.FillTexts.Keys)
			{
				VEAnswer veanswer3 = new VEAnswer();
				veanswer3.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("YŊɐ͋ѝՂ") + text;
				veanswer3.CODE = this.oQuestion.FillTexts[text];
				list.Add(veanswer3);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer3.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					veanswer3.CODE
				});
			}
			return list;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0004633C File Offset: 0x0004453C
		private void method_5(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				this.oPageNav.PageDataLog(SurveyMsg.DelaySeconds, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00046390 File Offset: 0x00044590
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

		// Token: 0x06000223 RID: 547 RVA: 0x00046488 File Offset: 0x00044688
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

		// Token: 0x06000224 RID: 548 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private void method_6(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000464F0 File Offset: 0x000446F0
		private void method_7(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			TextChange[] array = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				bool flag;
				if (textBox.Text.Substring(offset, array[0].AddedLength).Trim() == global::GClass0.smethod_0(""))
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

		// Token: 0x06000228 RID: 552 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x06000229 RID: 553 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x0600022B RID: 555 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_11(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00002B18 File Offset: 0x00000D18
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x0400041A RID: 1050
		private string MySurveyId;

		// Token: 0x0400041B RID: 1051
		private string CurPageId;

		// Token: 0x0400041C RID: 1052
		private NavBase MyNav = new NavBase();

		// Token: 0x0400041D RID: 1053
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400041E RID: 1054
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400041F RID: 1055
		private AutoFill oAutoFill = new AutoFill();

		// Token: 0x04000420 RID: 1056
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000421 RID: 1057
		private UDPX oFunc = new UDPX();

		// Token: 0x04000422 RID: 1058
		private QMultiple oQuestion = new QMultiple();

		// Token: 0x04000423 RID: 1059
		private bool ExistTextFill;

		// Token: 0x04000424 RID: 1060
		private bool ExistCodeFills;

		// Token: 0x04000425 RID: 1061
		private List<string> listPreSet = new List<string>();

		// Token: 0x04000426 RID: 1062
		private List<string> listFix = new List<string>();

		// Token: 0x04000427 RID: 1063
		private bool IsFixOther;

		// Token: 0x04000428 RID: 1064
		private bool IsFixNone;

		// Token: 0x04000429 RID: 1065
		private List<Button> listAllButton = new List<Button>();

		// Token: 0x0400042A RID: 1066
		private List<Button> listButton = new List<Button>();

		// Token: 0x0400042B RID: 1067
		private int Button_Type;

		// Token: 0x0400042C RID: 1068
		private int Button_Height;

		// Token: 0x0400042D RID: 1069
		private int Button_Width;

		// Token: 0x0400042E RID: 1070
		private int Button_FontSize;

		// Token: 0x0400042F RID: 1071
		private bool _isValue;

		// Token: 0x04000430 RID: 1072
		private string _strMask = global::GClass0.smethod_0("");

		// Token: 0x04000431 RID: 1073
		private int _MinValue = -1;

		// Token: 0x04000432 RID: 1074
		private int _MaxValue = -1;

		// Token: 0x04000433 RID: 1075
		private int _MaxLen = 250;

		// Token: 0x04000434 RID: 1076
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000435 RID: 1077
		private int SecondsWait;

		// Token: 0x04000436 RID: 1078
		private int SecondsCountDown;

		// Token: 0x04000437 RID: 1079
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x02000097 RID: 151
		[CompilerGenerated]
		[Serializable]
		private sealed class Class25
		{
			// Token: 0x0600072D RID: 1837 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CE7 RID: 3303
			public static readonly MultipleText.Class25 instance = new MultipleText.Class25();

			// Token: 0x04000CE8 RID: 3304
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
