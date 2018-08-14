using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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
	// Token: 0x02000055 RID: 85
	public partial class Multiple : Page
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00095310 File Offset: 0x00093510
		public Multiple()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000953B0 File Offset: 0x000935B0
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
			string text = global::GClass0.smethod_0("");
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
			{
				text = this.oLogicEngine.Route(this.oQuestion.QDefine.CONTROL_TOOLTIP);
			}
			else if (this.oQuestion.QDefine.GROUP_LEVEL != global::GClass0.smethod_0("") && this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
			{
				this.oQuestion.InitCircle();
				string text2 = global::GClass0.smethod_0("");
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("@"))
				{
					text2 = this.MyNav.CircleACode;
				}
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					text2 = this.MyNav.CircleBCode;
				}
				if (text2 != global::GClass0.smethod_0(""))
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
			if (text != global::GClass0.smethod_0(""))
			{
				string text3 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + text;
				if (this.oFunc.LEFT(text, 1) == global::GClass0.smethod_0("\""))
				{
					text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + this.oFunc.MID(text, 1, -9999);
				}
				else if (!File.Exists(text3))
				{
					text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + text;
				}
				Image image = new Image();
				if (!(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("+")) && !(this.oQuestion.QDefine.CONTROL_MASK.Trim() == global::GClass0.smethod_0("")) && this.oQuestion.QDefine.CONTROL_MASK != null)
				{
					string string_2 = this.oQuestion.QDefine.CONTROL_MASK;
					if (this.oFunc.LEFT(string_2, 1) == global::GClass0.smethod_0("\""))
					{
						string_2 = this.oFunc.MID(string_2, 1, -9999);
						this.scrollPic.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						this.scrollPic.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						int num = this.oFunc.StringToInt(string_2);
						if (num > 0)
						{
							this.scrollPic.Width = (double)num;
						}
					}
					else
					{
						int num2 = this.oFunc.StringToInt(string_2);
						if (num2 > 0)
						{
							image.Width = (double)num2;
						}
					}
				}
				else
				{
					this.PicWidth.Width = new GridLength(1.0, GridUnitType.Star);
					this.ButtonWidth.Width = GridLength.Auto;
				}
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 10.0, 20.0, 10.0);
				image.SetValue(Grid.ColumnProperty, 0);
				image.SetValue(Grid.RowProperty, 0);
				image.HorizontalAlignment = HorizontalAlignment.Center;
				image.VerticalAlignment = VerticalAlignment.Center;
				try
				{
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.UriSource = new Uri(text3, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					this.scrollPic.Content = image;
				}
				catch (Exception)
				{
				}
			}
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						if (surveyDetail2.CODE == array[i].ToString())
						{
							list4.Add(surveyDetail2);
							break;
						}
					}
				}
				if (this.oQuestion.QDefine.SHOW_LOGIC == global::GClass0.smethod_0("") && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list4.Sort(new Comparison<SurveyDetail>(Multiple.Class61.instance.method_0));
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
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QDetails)
					{
						if (surveyDetail3.CODE == array4[m].ToString())
						{
							list5.Add(surveyDetail3);
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
			this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
			this.Button_FontSize = ((this.oQuestion.QDefine.CONTROL_FONTSIZE == 0) ? SurveyHelper.BtnFontSize : this.oQuestion.QDefine.CONTROL_FONTSIZE);
			if (this.Button_FontSize == -1)
			{
				this.Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			this.Button_FontSize = Math.Abs(this.Button_FontSize);
			this.Button_Height = ((this.oQuestion.QDefine.CONTROL_HEIGHT == 0) ? SurveyHelper.BtnHeight : this.oQuestion.QDefine.CONTROL_HEIGHT);
			if (this.oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (this.Button_Type != 2)
				{
					if (this.Button_Type != 4)
					{
						this.Button_Width = SurveyHelper.BtnWidth;
						goto IL_DE3;
					}
				}
				this.Button_Width = 440;
			}
			else
			{
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			IL_DE3:
			this.method_2();
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
					foreach (string text4 in this.listPreSet)
					{
						if (!this.listFix.Contains(text4))
						{
							this.oQuestion.SelectedValues.Add(text4);
							foreach (Button button in this.listBtnNormal)
							{
								if (button.Name.Substring(2) == text4)
								{
									button.Style = style;
									int num3 = (int)button.Tag;
									if (num3 == 1 || num3 == 3 || num3 == 5 || (num3 == 11 | num3 == 13) || num3 == 14)
									{
										flag = true;
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
					if (this.oQuestion.QDetails.Count == 1 || this.listBtnNormal.Count == 0)
					{
						if (this.listBtnNormal.Count > 0 && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)) && this.listBtnNormal[0].Style == style2)
						{
							this.method_3(this.listBtnNormal[0], new RoutedEventArgs());
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
					if (SurveyHelper.AutoFill && !flag2)
					{
						AutoFill autoFill = new AutoFill();
						autoFill.oLogicEngine = this.oLogicEngine;
						this.listButton = autoFill.MultiButton(this.oQuestion.QDefine, this.listButton, this.listBtnNormal, 0);
						foreach (Button button2 in this.listButton)
						{
							if (button2.Style == style2)
							{
								this.method_3(button2, null);
							}
						}
						if (this.txtFill.IsEnabled)
						{
							this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
						}
						if (this.listButton.Count > 0 && autoFill.AutoNext(this.oQuestion.QDefine))
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
					if (this.oFunc.MID(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ")).Length) == this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ"))
					{
						if (this.listFix.Contains(surveyAnswer.CODE))
						{
							continue;
						}
						this.oQuestion.SelectedValues.Add(surveyAnswer.CODE);
						using (List<Button>.Enumerator enumerator3 = this.listBtnNormal.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								Button button3 = enumerator3.Current;
								if (button3.Name.Substring(2) == surveyAnswer.CODE)
								{
									button3.Style = style;
									int num4 = (int)button3.Tag;
									if (num4 == 1 || num4 == 3 || num4 == 5 || (num4 == 11 | num4 == 13) || num4 == 14)
									{
										flag = true;
									}
								}
							}
							continue;
						}
					}
					if (this.ExistTextFill && surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉") && surveyAnswer.CODE != global::GClass0.smethod_0(""))
					{
						this.txtFill.Text = surveyAnswer.CODE;
					}
				}
				if (flag)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
				}
			}
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
			this.PageLoaded = true;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00096A20 File Offset: 0x00094C20
		private void method_1(object sender, EventArgs e)
		{
			if (this.oQuestion.QDetails != null && this.oQuestion.QDetails.Count != 0 && this.PageLoaded)
			{
				WrapPanel wrapPanel = this.wrapPanel1;
				ScrollViewer scrollViewer = this.scrollmain;
				if (this.Button_Type < -20)
				{
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					wrapPanel.Orientation = Orientation.Horizontal;
					wrapPanel.Width = (double)(-(double)this.Button_Type);
					this.PageLoaded = false;
				}
				else if (this.Button_Type < 1)
				{
					if (this.Button_Type == 0)
					{
						if (scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Collapsed)
						{
							this.Button_Type = 2;
							this.PageLoaded = false;
						}
						else
						{
							int num = Convert.ToInt32(scrollViewer.ActualHeight / (double)(this.Button_Height + 15));
							int num2 = Convert.ToInt32((double)(this.oQuestion.QDetails.Count / num) + 0.99999999);
							int num3 = Convert.ToInt32(Convert.ToInt32(num * num2 - this.oQuestion.QDetails.Count) / num2);
							this.w_Height = wrapPanel.Height;
							wrapPanel.Height = (double)((num - num3) * (this.Button_Height + 15));
							scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
							scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
							wrapPanel.Orientation = Orientation.Vertical;
							this.Button_Type = -1;
						}
					}
					else if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Collapsed)
					{
						this.Button_Type = 4;
						this.PageLoaded = false;
					}
					else
					{
						wrapPanel.Height = this.w_Height;
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
						wrapPanel.Orientation = Orientation.Horizontal;
						this.Button_Type = 1;
						this.PageLoaded = false;
					}
				}
				else if (this.Button_Type > 20)
				{
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					wrapPanel.Orientation = Orientation.Vertical;
					wrapPanel.Height = (double)this.Button_Type;
					this.PageLoaded = false;
				}
				else
				{
					if (this.Button_Type != 2)
					{
						if (this.Button_Type != 4)
						{
							wrapPanel.Orientation = Orientation.Horizontal;
							goto IL_1F1;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					IL_1F1:
					if (this.Button_Type != 3)
					{
						if (this.Button_Type != 4)
						{
							scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
							goto IL_223;
						}
					}
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					IL_223:
					this.PageLoaded = false;
				}
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00096C6C File Offset: 0x00094E6C
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapPanel1;
			if (this.listFix.Count > 0)
			{
				foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
				{
					if (this.listFix.Contains(surveyDetail.CODE))
					{
						if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3 || (surveyDetail.IS_OTHER == 11 | surveyDetail.IS_OTHER == 5) || surveyDetail.IS_OTHER == 13 || surveyDetail.IS_OTHER == 14)
						{
							this.IsFixOther = true;
						}
						if (surveyDetail.IS_OTHER == 2 || surveyDetail.IS_OTHER == 3 || (surveyDetail.IS_OTHER == 13 | surveyDetail.IS_OTHER == 5) || surveyDetail.IS_OTHER == 4 || surveyDetail.IS_OTHER == 14)
						{
							this.IsFixNone = true;
						}
					}
				}
			}
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
			{
				bool flag = false;
				if (surveyDetail2.IS_OTHER == 1 || surveyDetail2.IS_OTHER == 3 || (surveyDetail2.IS_OTHER == 11 | surveyDetail2.IS_OTHER == 5) || surveyDetail2.IS_OTHER == 13 || surveyDetail2.IS_OTHER == 14)
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
					Button button = new Button();
					button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail2.CODE;
					button.Content = surveyDetail2.CODE_TEXT;
					button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
					button.Style = (flag3 ? style : style2);
					if (flag3)
					{
						button.Opacity = 0.5;
					}
					button.Tag = surveyDetail2.IS_OTHER;
					if (!flag3)
					{
						button.Click += this.method_3;
					}
					button.FontSize = (double)this.Button_FontSize;
					button.MinWidth = (double)this.Button_Width;
					button.MinHeight = (double)this.Button_Height;
					wrapPanel.Children.Add(button);
					if (!flag3)
					{
						this.listBtnNormal.Add(button);
						if (!flag2 || SurveyHelper.FillMode != global::GClass0.smethod_0("2"))
						{
							this.listButton.Add(button);
						}
					}
				}
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00097010 File Offset: 0x00095210
		private void method_3(object sender, RoutedEventArgs e = null)
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
			if (num == 1 || num == 3 || num == 5 || num == 11 || num == 13 || num == 14)
			{
				num3 = 1;
			}
			int num4 = 0;
			if (button.Style == style)
			{
				num4 = 1;
			}
			int num5 = 0;
			if (num4 == 0)
			{
				if (num2 == 1)
				{
					this.oQuestion.SelectedValues.Clear();
					num5 = 1;
				}
				else
				{
					num5 = 2;
				}
				this.oQuestion.SelectedValues.Add(text);
				button.Style = style;
			}
			else if (num2 == 1)
			{
				num4 = 2;
			}
			else
			{
				this.oQuestion.SelectedValues.Remove(text);
				button.Style = style2;
				if (num3 == 0)
				{
					num4 = 2;
				}
			}
			if (num4 < 2)
			{
				bool flag = true;
				bool flag2 = true;
				foreach (Button button2 in this.listBtnNormal)
				{
					int num6 = (int)button2.Tag;
					string text2 = button2.Name.Substring(2);
					if (!(text2 == text))
					{
						if (num5 == 1)
						{
							button2.Style = style2;
						}
						else if (num5 == 2 && flag2 && (num6 == 2 || num6 == 3 || num6 == 5 || num6 == 13 || num6 == 4 || num6 == 14) && button2.Style == style)
						{
							button2.Style = style2;
							this.oQuestion.SelectedValues.Remove(text2);
							flag2 = false;
						}
					}
					if (!this.IsFixOther && flag && button2.Style == style && (num6 == 1 || num6 == 3 || num6 == 5 || num6 == 11 || num6 == 13 || num6 == 14))
					{
						flag = false;
					}
				}
				if (!this.IsFixOther)
				{
					if (flag)
					{
						this.txtFill.Background = Brushes.LightGray;
						this.txtFill.IsEnabled = false;
						return;
					}
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
					if (this.txtFill.Text == global::GClass0.smethod_0(""))
					{
						this.txtFill.Focus();
					}
				}
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000972AC File Offset: 0x000954AC
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
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : global::GClass0.smethod_0(""));
			return false;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00097458 File Offset: 0x00095658
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
			SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
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
			return list;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00097668 File Offset: 0x00095868
		private void method_6(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				this.oPageNav.PageDataLog(SurveyMsg.DelaySeconds, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000976BC File Offset: 0x000958BC
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
			this.method_6(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000977B4 File Offset: 0x000959B4
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

		// Token: 0x060005A1 RID: 1441 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00003A1E File Offset: 0x00001C1E
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000A2D RID: 2605
		private string MySurveyId;

		// Token: 0x04000A2E RID: 2606
		private string CurPageId;

		// Token: 0x04000A2F RID: 2607
		private NavBase MyNav = new NavBase();

		// Token: 0x04000A30 RID: 2608
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000A31 RID: 2609
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000A32 RID: 2610
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000A33 RID: 2611
		private UDPX oFunc = new UDPX();

		// Token: 0x04000A34 RID: 2612
		private QMultiple oQuestion = new QMultiple();

		// Token: 0x04000A35 RID: 2613
		private bool ExistTextFill;

		// Token: 0x04000A36 RID: 2614
		private List<string> listPreSet = new List<string>();

		// Token: 0x04000A37 RID: 2615
		private List<string> listFix = new List<string>();

		// Token: 0x04000A38 RID: 2616
		private List<Button> listBtnNormal = new List<Button>();

		// Token: 0x04000A39 RID: 2617
		private bool IsFixOther;

		// Token: 0x04000A3A RID: 2618
		private bool IsFixNone;

		// Token: 0x04000A3B RID: 2619
		private List<Button> listButton = new List<Button>();

		// Token: 0x04000A3C RID: 2620
		private bool PageLoaded;

		// Token: 0x04000A3D RID: 2621
		private int Button_Type;

		// Token: 0x04000A3E RID: 2622
		private int Button_Height;

		// Token: 0x04000A3F RID: 2623
		private int Button_Width;

		// Token: 0x04000A40 RID: 2624
		private int Button_FontSize;

		// Token: 0x04000A41 RID: 2625
		private double w_Height;

		// Token: 0x04000A42 RID: 2626
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000A43 RID: 2627
		private int SecondsWait;

		// Token: 0x04000A44 RID: 2628
		private int SecondsCountDown;

		// Token: 0x04000A45 RID: 2629
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000BC RID: 188
		[CompilerGenerated]
		[Serializable]
		private sealed class Class61
		{
			// Token: 0x0600079E RID: 1950 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D42 RID: 3394
			public static readonly Multiple.Class61 instance = new Multiple.Class61();

			// Token: 0x04000D43 RID: 3395
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
