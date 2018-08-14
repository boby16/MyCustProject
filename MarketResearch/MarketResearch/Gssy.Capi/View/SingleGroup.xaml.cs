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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x0200003E RID: 62
	public partial class SingleGroup : Page
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0007B368 File Offset: 0x00079568
		public SingleGroup()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0007B3E4 File Offset: 0x000795E4
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
					list4.Sort(new Comparison<SurveyDetail>(SingleGroup.Class47.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0("") && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == global::GClass0.smethod_0("2"))))
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int j = 0; j < array2.Count<string>(); j++)
				{
					using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								this.listPreSet.Add(array2[j]);
								break;
							}
						}
					}
				}
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int k = 0; k < this.oQuestion.QDetails.Count<SurveyDetail>(); k++)
				{
					this.oQuestion.QDetails[k].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[k].CODE_TEXT);
				}
			}
			if (list2[0].Trim() != global::GClass0.smethod_0(""))
			{
				this.ShowLogic = true;
				string[] array3 = this.oLogicEngine.aryCode(list2[0], ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count<string>(); l++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						if (surveyDetail2.CODE == array3[l].ToString())
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
			if (this.ExistTextFill)
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
			}
			else
			{
				this.txtFill.Height = 0.0;
				this.txtFillTitle.Height = 0.0;
				this.txtAfter.Height = 0.0;
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			double opacity = 0.2;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Grid gridContent = this.GridContent;
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
					if (this.listPreSet.Count > 0)
					{
						this.oQuestion.SelectedCode = this.listPreSet[0];
						foreach(var child in gridContent.Children)
						{
							{
								WrapPanel wrapPanel = (WrapPanel)((Border)child).Child;
								if (wrapPanel.Name.Substring(1, 1) == global::GClass0.smethod_0("S"))
								{
									foreach (object obj in wrapPanel.Children)
									{
										foreach (object obj2 in ((WrapPanel)obj).Children)
										{
											UIElement uielement = (UIElement)obj2;
											if (uielement is Button)
											{
												Button button = (Button)uielement;
												if (button.Name.Substring(2) == this.oQuestion.SelectedCode)
												{
													button.Style = style;
													flag3 = true;
													int num = (int)button.Tag;
													if (num == 1 || num == 3 || num == 5 || num == 11 || (num == 13 | num == 14))
													{
														flag = true;
													}
												}
											}
											else if (uielement is Image)
											{
												Image image = (Image)uielement;
												if (image.Name.Substring(2) == this.oQuestion.SelectedCode)
												{
													image.Opacity = opacity;
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
						}
						if (flag)
						{
							this.txtFill.IsEnabled = true;
							this.txtFill.Background = Brushes.White;
						}
					}
					if (this.oQuestion.QDetails.Count == 1)
					{
						if (this.listPreSet.Count == 0 && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
						{
							this.method_2(this.listButton[0], null);
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
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedCode != global::GClass0.smethod_0(""))
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
					if (SurveyHelper.AutoFill)
					{
						AutoFill autoFill = new AutoFill();
						autoFill.oLogicEngine = this.oLogicEngine;
						if (this.oQuestion.SelectedCode == global::GClass0.smethod_0(""))
						{
							Button button2 = autoFill.SingleButton(this.oQuestion.QDefine, this.listButton);
							if (button2 != null)
							{
								if (this.listPreSet.Count == 0 && button2.Style == style2)
								{
									this.method_2(button2, null);
								}
								if (this.txtFill.IsEnabled)
								{
									this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
								}
							}
						}
						if (this.oQuestion.SelectedCode != global::GClass0.smethod_0("") && !flag2 && autoFill.AutoNext(this.oQuestion.QDefine))
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
				this.oQuestion.SelectedCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
				foreach (var child in gridContent.Children)
				{
					{
						WrapPanel wrapPanel2 = (WrapPanel)((Border)child).Child;
						if (wrapPanel2.Name.Substring(1, 1) == global::GClass0.smethod_0("S"))
						{
							foreach (object obj3 in wrapPanel2.Children)
							{
								foreach (object obj4 in ((WrapPanel)obj3).Children)
								{
									UIElement uielement2 = (UIElement)obj4;
									if (uielement2 is Button)
									{
										Button button3 = (Button)uielement2;
										if (button3.Name.Substring(2) == this.oQuestion.SelectedCode)
										{
											button3.Style = style;
											flag3 = true;
											int num2 = (int)button3.Tag;
											if (num2 == 1 || num2 == 3 || num2 == 5 || num2 == 11 || (num2 == 13 | num2 == 14))
											{
												flag = true;
											}
										}
									}
									else if (uielement2 is Image)
									{
										Image image2 = (Image)uielement2;
										if (image2.Name.Substring(2) == this.oQuestion.SelectedCode)
										{
											image2.Opacity = opacity;
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
				}
				this.txtFill.Text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"));
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

		// Token: 0x06000449 RID: 1097 RVA: 0x0007C6EC File Offset: 0x0007A8EC
		private void method_1()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
			Brush borderBrush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			string text = this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim();
			string text2 = global::GClass0.smethod_0("S");
			string text3 = global::GClass0.smethod_0("B");
			int num = 0;
			if (text != global::GClass0.smethod_0(""))
			{
				text2 = this.method_8(text, 1);
				if (global::GClass0.smethod_0(":ĸȺ̴в԰زܴ࠺स").Contains(text2))
				{
					text2 = global::GClass0.smethod_0("S");
					if (text != global::GClass0.smethod_0(""))
					{
						num = Convert.ToInt32(text);
					}
				}
				else
				{
					text = this.method_9(text, 1, -9999);
					text3 = this.method_8(text, 1);
					if (global::GClass0.smethod_0(":ĸȺ̴в԰زܴ࠺स").Contains(text3))
					{
						if (text2 != global::GClass0.smethod_0("U") && text2 != global::GClass0.smethod_0("C"))
						{
							text3 = global::GClass0.smethod_0("B");
						}
						if (text != global::GClass0.smethod_0(""))
						{
							num = Convert.ToInt32(text);
						}
					}
					else if (this.method_9(text, 1, -9999) != global::GClass0.smethod_0(""))
					{
						num = Convert.ToInt32(this.method_9(text, 1, -9999));
					}
				}
				text = text2 + text3;
				if (text.Contains(global::GClass0.smethod_0("M")))
				{
					text2 = global::GClass0.smethod_0("M");
				}
				else if (text.Contains(global::GClass0.smethod_0("S")))
				{
					text2 = global::GClass0.smethod_0("S");
				}
				if (text.Contains(global::GClass0.smethod_0("U")))
				{
					text3 = global::GClass0.smethod_0("U");
				}
				else if (text.Contains(global::GClass0.smethod_0("C")))
				{
					text3 = global::GClass0.smethod_0("C");
				}
				if (text.Contains(global::GClass0.smethod_0("B")) && (text.Contains(global::GClass0.smethod_0("U")) || text.Contains(global::GClass0.smethod_0("C"))))
				{
					text2 = global::GClass0.smethod_0("B");
				}
				if (text.Contains(global::GClass0.smethod_0("B")) && (text.Contains(global::GClass0.smethod_0("S")) || text.Contains(global::GClass0.smethod_0("M"))))
				{
					text3 = global::GClass0.smethod_0("B");
				}
				if (text == global::GClass0.smethod_0("B") || text == global::GClass0.smethod_0("Ał"))
				{
					text2 = global::GClass0.smethod_0("B");
					text3 = global::GClass0.smethod_0("B");
				}
			}
			HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
			if (text2 == global::GClass0.smethod_0("B"))
			{
				horizontalAlignment = HorizontalAlignment.Center;
			}
			else if (text2 == global::GClass0.smethod_0("M"))
			{
				horizontalAlignment = HorizontalAlignment.Left;
			}
			VerticalAlignment verticalAlignment = VerticalAlignment.Center;
			if (text3 == global::GClass0.smethod_0("U"))
			{
				verticalAlignment = VerticalAlignment.Top;
			}
			else if (text3 == global::GClass0.smethod_0("C"))
			{
				verticalAlignment = VerticalAlignment.Bottom;
			}
			Orientation orientation = (this.Button_Type == 2 || this.Button_Type == 4) ? Orientation.Vertical : Orientation.Horizontal;
			Orientation orientation2 = (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("I")) ? Orientation.Horizontal : Orientation.Vertical;
			Math.Abs(this.Button_FontSize);
			Grid gridContent = this.GridContent;
			this.oQuestion.GetGroupDetails();
			if (this.ShowLogic)
			{
				List<SurveyDetail> list = new List<SurveyDetail>();
				foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
				{
					bool flag = true;
					foreach (SurveyDetail surveyDetail2 in list)
					{
						if (surveyDetail.PARENT_CODE == surveyDetail2.CODE)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						foreach (SurveyDetail surveyDetail3 in this.oQuestion.QGroupDetails)
						{
							if (surveyDetail.PARENT_CODE == surveyDetail3.CODE)
							{
								list.Add(surveyDetail3);
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
			int num2 = 0;
			foreach (SurveyDetail surveyDetail4 in this.oQuestion.QGroupDetails)
			{
				SingleGroup.Class48 @class = new SingleGroup.Class48();
				@class.strGroupCode = surveyDetail4.CODE;
				string code_TEXT = surveyDetail4.CODE_TEXT;
				IEnumerable<Class0<string, string, int, string>> enumerable = this.oQuestion.QDetails.Where(new Func<SurveyDetail, bool>(@class.method_0)).Select(new Func<SurveyDetail, Class0<string, string, int, string>>(SingleGroup.Class47.instance.method_1));
				bool flag2 = false;
				foreach (Class0<string, string, int, string> class2 in enumerable)
				{
					flag2 = true;
				}
				if (flag2)
				{
					gridContent.RowDefinitions.Add(new RowDefinition
					{
						Height = GridLength.Auto
					});
					Border border = new Border();
					border.BorderThickness = new Thickness((double)((code_TEXT == global::GClass0.smethod_0("") | this.oQuestion.QDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("1")) ? 0 : 1));
					border.BorderBrush = borderBrush;
					border.SetValue(Grid.RowProperty, num2);
					border.SetValue(Grid.ColumnProperty, 0);
					gridContent.Children.Add(border);
					WrapPanel wrapPanel = new WrapPanel();
					wrapPanel.VerticalAlignment = verticalAlignment;
					wrapPanel.HorizontalAlignment = horizontalAlignment;
					wrapPanel.Name = global::GClass0.smethod_0("uō") + @class.strGroupCode;
					border.Child = wrapPanel;
					TextBlock textBlock = new TextBlock();
					textBlock.Text = ((this.oQuestion.QDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("1")) ? global::GClass0.smethod_0("") : code_TEXT);
					textBlock.Style = style2;
					textBlock.Foreground = foreground;
					if (num > 0)
					{
						textBlock.Width = (double)num;
					}
					textBlock.TextWrapping = TextWrapping.Wrap;
					textBlock.Margin = new Thickness(15.0, 0.0, 15.0, 0.0);
					textBlock.VerticalAlignment = verticalAlignment;
					wrapPanel.Children.Add(textBlock);
					border = new Border();
					border.BorderThickness = new Thickness((double)((this.oQuestion.QDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("1")) ? 0 : 1));
					border.BorderBrush = borderBrush;
					border.SetValue(Grid.RowProperty, num2);
					border.SetValue(Grid.ColumnProperty, 1);
					gridContent.Children.Add(border);
					WrapPanel wrapPanel2 = new WrapPanel();
					wrapPanel2.Orientation = orientation;
					wrapPanel2.Margin = new Thickness(15.0, 0.0, 0.0, 13.0);
					wrapPanel2.Name = global::GClass0.smethod_0("uœ") + @class.strGroupCode;
					border.Child = wrapPanel2;
					foreach (Class0<string, string, int, string> class3 in enumerable)
					{
						WrapPanel wrapPanel3 = new WrapPanel();
						wrapPanel3.Margin = new Thickness(0.0, 15.0, 15.0, 0.0);
						wrapPanel3.Orientation = orientation2;
						wrapPanel2.Children.Add(wrapPanel3);
						Button button = new Button();
						button.Name = global::GClass0.smethod_0("`Ş") + class3.CODE;
						button.Content = class3.CODE_TEXT;
						button.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
						button.Style = style;
						button.Tag = class3.IS_OTHER;
						if (class3.IS_OTHER == 1 || class3.IS_OTHER == 3 || class3.IS_OTHER == 5 || (class3.IS_OTHER == 11 | class3.IS_OTHER == 13) || class3.IS_OTHER == 14)
						{
							this.ExistTextFill = true;
						}
						button.Click += this.method_2;
						button.FontSize = (double)this.Button_FontSize;
						button.MinWidth = (double)this.Button_Width;
						button.MinHeight = (double)this.Button_Height;
						wrapPanel3.Children.Add(button);
						this.listButton.Add(button);
						string extend_ = class3.EXTEND_1;
						if (extend_ != global::GClass0.smethod_0(""))
						{
							Image image = new Image();
							image.Name = global::GClass0.smethod_0("rŞ") + class3.CODE;
							image.Tag = class3.IS_OTHER;
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
							try
							{
								string text4 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + extend_;
								if (this.method_8(extend_, 1) == global::GClass0.smethod_0("\""))
								{
									text4 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + this.method_9(extend_, 1, -9999);
								}
								else if (!File.Exists(text4))
								{
									text4 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + extend_;
								}
								BitmapImage bitmapImage = new BitmapImage();
								bitmapImage.BeginInit();
								bitmapImage.UriSource = new Uri(text4, UriKind.RelativeOrAbsolute);
								bitmapImage.EndInit();
								image.Source = bitmapImage;
								image.MouseLeftButtonUp += new MouseButtonEventHandler(this.method_3);
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
					num2++;
				}
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0007D34C File Offset: 0x0007B54C
		private void method_2(object sender, RoutedEventArgs e = null)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			double num = 0.2;
			double num2 = 1.0;
			Button button = (Button)sender;
			int num3 = (int)button.Tag;
			string text = button.Name.Substring(2);
			int num4 = 0;
			if (num3 == 1 || num3 == 3 || num3 == 5 || num3 == 11 || num3 == 13 || num3 == 14)
			{
				num4 = 1;
			}
			int num5 = 0;
			if (button.Style == style)
			{
				num5 = 1;
			}
			if (num5 == 0)
			{
				this.oQuestion.SelectedCode = text;
				foreach (object obj in this.GridContent.Children)
				{
					WrapPanel wrapPanel = (WrapPanel)((Border)obj).Child;
					if (wrapPanel.Name.Substring(1, 1) == global::GClass0.smethod_0("S"))
					{
						foreach (object obj2 in wrapPanel.Children)
						{
							foreach (object obj3 in ((WrapPanel)obj2).Children)
							{
								UIElement uielement = (UIElement)obj3;
								if (uielement is Button)
								{
									Button button2 = (Button)uielement;
									string a = button2.Name.Substring(2);
									button2.Style = ((a == text) ? style : style2);
								}
								if (uielement is Image)
								{
									Image image = (Image)uielement;
									string a2 = image.Name.Substring(2);
									image.Opacity = ((a2 == text) ? num : num2);
								}
							}
						}
					}
				}
				if (num4 == 0)
				{
					this.txtFill.Background = Brushes.LightGray;
					this.txtFill.IsEnabled = false;
				}
				else
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
					if (this.txtFill.Text == global::GClass0.smethod_0(""))
					{
						this.txtFill.Focus();
					}
				}
			}
			if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode4) && this.oQuestion.SelectedCode != global::GClass0.smethod_0(""))
			{
				if (this.txtFill.IsEnabled)
				{
					this.txtFill.Focus();
					return;
				}
				this.btnNav_Click(this, e);
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0007D668 File Offset: 0x0007B868
		private void method_3(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			double num = 0.2;
			double num2 = 1.0;
			Image image = (Image)sender;
			int num3 = (int)image.Tag;
			string text = image.Name.Substring(2);
			int num4 = 0;
			if (num3 == 1 || num3 == 3 || num3 == 5 || num3 == 11 || num3 == 13 || num3 == 14)
			{
				num4 = 1;
			}
			int num5 = 0;
			if (image.Opacity == num)
			{
				num5 = 1;
			}
			if (num5 == 0)
			{
				this.oQuestion.SelectedCode = text;
				foreach (object obj in this.GridContent.Children)
				{
					WrapPanel wrapPanel = (WrapPanel)((Border)obj).Child;
					if (wrapPanel.Name.Substring(1, 1) == global::GClass0.smethod_0("S"))
					{
						foreach (object obj2 in wrapPanel.Children)
						{
							foreach (object obj3 in ((WrapPanel)obj2).Children)
							{
								UIElement uielement = (UIElement)obj3;
								if (uielement is Button)
								{
									Button button = (Button)uielement;
									string a = button.Name.Substring(2);
									button.Style = ((a == text) ? style : style2);
								}
								if (uielement is Image)
								{
									Image image2 = (Image)uielement;
									string a2 = image2.Name.Substring(2);
									image2.Opacity = ((a2 == text) ? num : num2);
								}
							}
						}
					}
				}
				if (num4 == 0)
				{
					this.txtFill.Background = Brushes.LightGray;
					this.txtFill.IsEnabled = false;
					return;
				}
				this.txtFill.IsEnabled = true;
				this.txtFill.Background = Brushes.White;
				this.txtFill.Focus();
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0007D910 File Offset: 0x0007BB10
		private bool method_4()
		{
			if (this.oQuestion.SelectedCode == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : global::GClass0.smethod_0(""));
			return false;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0007D9D4 File Offset: 0x0007BBD4
		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.SelectedCode
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.SelectedCode;
			if (this.oQuestion.FillText != global::GClass0.smethod_0(""))
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
				veanswer.CODE = this.oQuestion.FillText;
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0(".ġ"),
					veanswer.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					this.oQuestion.FillText
				});
			}
			return list;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000031C9 File Offset: 0x000013C9
		private void method_6()
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0007DAE8 File Offset: 0x0007BCE8
		private void btnNav_Click(object sender = null, RoutedEventArgs e = null)
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
			this.method_6();
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0007DBDC File Offset: 0x0007BDDC
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

		// Token: 0x06000451 RID: 1105 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x06000456 RID: 1110 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000031ED File Offset: 0x000013ED
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000816 RID: 2070
		private string MySurveyId;

		// Token: 0x04000817 RID: 2071
		private string CurPageId;

		// Token: 0x04000818 RID: 2072
		private NavBase MyNav = new NavBase();

		// Token: 0x04000819 RID: 2073
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400081A RID: 2074
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400081B RID: 2075
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400081C RID: 2076
		private QSingle oQuestion = new QSingle();

		// Token: 0x0400081D RID: 2077
		private bool ExistTextFill;

		// Token: 0x0400081E RID: 2078
		private List<string> listPreSet = new List<string>();

		// Token: 0x0400081F RID: 2079
		private List<Button> listButton = new List<Button>();

		// Token: 0x04000820 RID: 2080
		private bool ShowLogic;

		// Token: 0x04000821 RID: 2081
		private int Button_Type;

		// Token: 0x04000822 RID: 2082
		private int Button_Height;

		// Token: 0x04000823 RID: 2083
		private int Button_Width;

		// Token: 0x04000824 RID: 2084
		private int Button_FontSize;

		// Token: 0x04000825 RID: 2085
		private bool Button_Hide;

		// Token: 0x04000826 RID: 2086
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000827 RID: 2087
		private int SecondsWait;

		// Token: 0x04000828 RID: 2088
		private int SecondsCountDown;

		// Token: 0x04000829 RID: 2089
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000AE RID: 174
		[CompilerGenerated]
		[Serializable]
		private sealed class Class47
		{
			// Token: 0x06000777 RID: 1911 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x06000778 RID: 1912 RVA: 0x00004442 File Offset: 0x00002642
			internal Class0<string, string, int, string> method_1(SurveyDetail surveyDetail_0)
			{
				return new Class0<string, string, int, string>(surveyDetail_0.CODE, surveyDetail_0.CODE_TEXT, surveyDetail_0.IS_OTHER, surveyDetail_0.EXTEND_1);
			}

			// Token: 0x04000D20 RID: 3360
			public static readonly SingleGroup.Class47 instance = new SingleGroup.Class47();

			// Token: 0x04000D21 RID: 3361
			public static Comparison<SurveyDetail> compare0;

			// Token: 0x04000D22 RID: 3362
			public static Func<SurveyDetail, Class0<string, string, int, string>> compare1;
		}

		// Token: 0x020000AF RID: 175
		[CompilerGenerated]
		private sealed class Class48
		{
			// Token: 0x0600077A RID: 1914 RVA: 0x000045EC File Offset: 0x000027EC
			internal bool method_0(SurveyDetail surveyDetail_0)
			{
				return surveyDetail_0.PARENT_CODE == this.strGroupCode;
			}

			// Token: 0x04000D23 RID: 3363
			public string strGroupCode;
		}
	}
}
