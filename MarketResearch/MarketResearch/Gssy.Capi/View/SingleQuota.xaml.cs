using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
using Newtonsoft.Json;

namespace Gssy.Capi.View
{
	// Token: 0x0200003C RID: 60
	public partial class SingleQuota : Page
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x000770FC File Offset: 0x000752FC
		public SingleQuota()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00077170 File Offset: 0x00075370
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
				string uriString = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + text;
				Image image = new Image();
				if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("+"))
				{
					this.PicWidth.Width = new GridLength(1.0, GridUnitType.Star);
					this.ButtonWidth.Width = GridLength.Auto;
				}
				else
				{
					int num = this.method_12(this.oQuestion.QDefine.CONTROL_MASK);
					if (num > 0)
					{
						image.Width = (double)num;
					}
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
					bitmapImage.UriSource = new Uri(uriString, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					this.gridContent.Children.Add(image);
				}
				catch (Exception)
				{
				}
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
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
					list4.Sort(new Comparison<SurveyDetail>(SingleQuota.Class45.instance.method_0));
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
				string[] array3 = this.oLogicEngine.aryCode(list2[0], ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count<string>(); l++)
				{
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QDetails)
					{
						if (surveyDetail3.CODE == array3[l].ToString())
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
						goto IL_C6D;
					}
				}
				this.Button_Width = 440;
			}
			else
			{
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			IL_C6D:
			this.method_2();
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
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				Button button = autoFill.SingleButton(this.oQuestion.QDefine, this.listButton);
				if (button != null)
				{
					if (this.listPreSet.Count == 0)
					{
						this.method_3(button, new RoutedEventArgs());
					}
					if (this.txtFill.IsEnabled)
					{
						this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
					}
					if (autoFill.AutoNext(this.oQuestion.QDefine))
					{
						this.btnNav_Click(this, e);
					}
				}
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			bool flag = false;
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
						foreach (object obj in this.wrapPanel1.Children)
						{
							Button button2 = (Button)obj;
							if (button2.Name.Substring(2) == this.oQuestion.SelectedCode)
							{
								button2.Style = style;
								int num2 = (int)button2.Tag;
								if (num2 != 1 && num2 != 3 && num2 != 5 && !(num2 == 11 | num2 == 13))
								{
									if (num2 != 14)
									{
										break;
									}
								}
								flag = true;
								break;
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
							this.method_3(this.listButton[0], new RoutedEventArgs());
						}
						if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (this.txtFill.IsEnabled)
							{
								this.txtFill.Focus();
							}
							else if (!SurveyHelper.AutoFill)
							{
								this.btnNav_Click(this, e);
							}
						}
					}
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedCode != global::GClass0.smethod_0(""))
					{
						if (this.txtFill.IsEnabled)
						{
							this.txtFill.Focus();
						}
						else if (!SurveyHelper.AutoFill)
						{
							this.btnNav_Click(this, e);
						}
					}
				}
			}
			else
			{
				this.oQuestion.SelectedCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
				foreach (object obj2 in this.wrapPanel1.Children)
				{
					Button button3 = (Button)obj2;
					if (button3.Name.Substring(2) == this.oQuestion.SelectedCode)
					{
						button3.Style = style;
						int num3 = (int)button3.Tag;
						if (num3 != 1 && num3 != 3 && num3 != 5 && !(num3 == 11 | num3 == 13))
						{
							if (num3 != 14)
							{
								break;
							}
						}
						flag = true;
						break;
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
				this.btnNav.Foreground = Brushes.Gray;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
			this.PageLoaded = true;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00078454 File Offset: 0x00076654
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				WrapPanel wrapPanel = this.wrapPanel1;
				ScrollViewer scrollViewer = this.scrollmain;
				if (this.Button_Type < 1)
				{
					if (this.Button_Type == 0)
					{
						if (scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Collapsed)
						{
							this.Button_Type = 2;
							this.PageLoaded = false;
							return;
						}
						int num = Convert.ToInt32(scrollViewer.ActualHeight / (double)(this.Button_Height + 15));
						int num2 = Convert.ToInt32((double)(this.oQuestion.QDetails.Count / num) + 0.99999999);
						int num3 = Convert.ToInt32(Convert.ToInt32(num * num2 - this.oQuestion.QDetails.Count) / num2);
						this.w_Height = wrapPanel.Height;
						wrapPanel.Height = (double)((num - num3) * (this.Button_Height + 15));
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						wrapPanel.Orientation = Orientation.Vertical;
						this.Button_Type = -1;
						return;
					}
					else
					{
						if (scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Collapsed)
						{
							this.Button_Type = 4;
							this.PageLoaded = false;
							return;
						}
						wrapPanel.Height = this.w_Height;
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
						wrapPanel.Orientation = Orientation.Horizontal;
						this.Button_Type = 1;
						this.PageLoaded = false;
						return;
					}
				}
				else
				{
					if (this.Button_Type > 20)
					{
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						wrapPanel.Orientation = Orientation.Vertical;
						wrapPanel.Height = (((double)this.Button_Type > scrollViewer.ActualHeight) ? scrollViewer.ActualHeight : ((double)this.Button_Type));
						this.PageLoaded = false;
						return;
					}
					if (this.Button_Type != 2)
					{
						if (this.Button_Type != 4)
						{
							wrapPanel.Orientation = Orientation.Horizontal;
							goto IL_199;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					IL_199:
					if (this.Button_Type != 3)
					{
						if (this.Button_Type != 4)
						{
							scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
							goto IL_1CB;
						}
					}
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					IL_1CB:
					this.PageLoaded = false;
				}
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00078634 File Offset: 0x00076834
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapPanel1;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = surveyDetail.IS_OTHER;
				if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3 || (surveyDetail.IS_OTHER == 11 | surveyDetail.IS_OTHER == 5) || surveyDetail.IS_OTHER == 13 || surveyDetail.IS_OTHER == 14)
				{
					this.ExistTextFill = true;
				}
				button.Click += this.method_3;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = (double)this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				wrapPanel.Children.Add(button);
				this.listButton.Add(button);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000787CC File Offset: 0x000769CC
		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			int num = (int)button.Tag;
			string text = button.Name.Substring(2);
			int num2 = 0;
			if (num == 1 || num == 3 || num == 5 || num == 11 || num == 13 || num == 14)
			{
				num2 = 1;
			}
			int num3 = 0;
			if (button.Style == style)
			{
				num3 = 1;
			}
			if (num3 == 0)
			{
				this.oQuestion.SelectedCode = text;
				foreach (object obj in this.wrapPanel1.Children)
				{
					Button button2 = (Button)obj;
					string a = button2.Name.Substring(2);
					button2.Style = ((a == text) ? style : style2);
				}
				if (num2 == 0)
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

		// Token: 0x0600041E RID: 1054 RVA: 0x00078998 File Offset: 0x00076B98
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
			if (this.txtFill.IsEnabled)
			{
				this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : global::GClass0.smethod_0(""));
			}
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00078A68 File Offset: 0x00076C68
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

		// Token: 0x06000420 RID: 1056 RVA: 0x00078B7C File Offset: 0x00076D7C
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
			if (!this.oLogicEngine.CheckLogic(this.CurPageId))
			{
				if (this.oLogicEngine.IS_ALLOW_PASS == 0)
				{
					MessageBox.Show(this.oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.btnNav.Content = this.btnNav_Content;
					return;
				}
				if (MessageBox.Show(this.oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
				{
					this.btnNav.Content = this.btnNav_Content;
					return;
				}
			}
			if (SurveyMsg.FunctionQuotaManager == global::GClass0.smethod_0("_ŭɹ͵ѡսټݼࡀ॥੠୺౬ു๪ཤၨᅯቢ፴ᑚᕰᙱ᝷ᡤ") && !this.method_14())
			{
				return;
			}
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.method_6();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00078CF8 File Offset: 0x00076EF8
		private void method_6()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.PageStartTime = SurveyHelper.PageStartTime;
			this.MyNav.RecordFileName = SurveyHelper.RecordFileName;
			this.MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			if (this.MyNav.GroupLevel == global::GClass0.smethod_0(""))
			{
				this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
			}
			else
			{
				this.MyNav.NextCirclePage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
				SurveyHelper.CircleACount = this.MyNav.CircleACount;
				SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
				if (this.MyNav.IsLastA && (this.MyNav.GroupPageType == 0 || this.MyNav.GroupPageType == 2))
				{
					SurveyHelper.CircleACode = global::GClass0.smethod_0("");
					SurveyHelper.CircleACodeText = global::GClass0.smethod_0("");
				}
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					SurveyHelper.CircleBCount = this.MyNav.CircleBCount;
					SurveyHelper.CircleBCurrent = this.MyNav.CircleBCurrent;
					if (this.MyNav.IsLastB && (this.MyNav.GroupPageType == 10 || this.MyNav.GroupPageType == 12 || this.MyNav.GroupPageType == 30 || this.MyNav.GroupPageType == 32))
					{
						SurveyHelper.CircleBCode = global::GClass0.smethod_0("");
						SurveyHelper.CircleBCodeText = global::GClass0.smethod_0("");
					}
				}
			}
			string text = this.oLogicEngine.Route(this.MyNav.RoadMap.FORM_NAME);
			SurveyHelper.RoadMapVersion = this.MyNav.RoadMap.VERSION_ID.ToString();
			string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), text);
			if (text.Substring(0, 1) == global::GClass0.smethod_0("@"))
			{
				uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), text);
			}
			if (text == SurveyHelper.CurPageName)
			{
				base.NavigationService.Refresh();
			}
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			SurveyHelper.NavLoad = 0;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00078F9C File Offset: 0x0007719C
		private void method_7(int int_0, List<VEAnswer> list_0)
		{
			this.btnNav.IsEnabled = false;
			this.btnNav.Content = global::GClass0.smethod_0("歨嘢䷔塐Ы軱簈圝࠭बਯ");
			foreach (VEAnswer veanswer in list_0)
			{
				Logging.Data.WriteLog(this.MySurveyId, veanswer.QUESTION_NAME + global::GClass0.smethod_0("-") + veanswer.CODE);
			}
			Thread.Sleep(int_0);
			this.btnNav.Content = this.btnNav_Content;
			this.btnNav.IsEnabled = true;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00079054 File Offset: 0x00077254
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

		// Token: 0x06000424 RID: 1060 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x06000427 RID: 1063 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_11(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000790BC File Offset: 0x000772BC
		private int method_12(string string_0)
		{
			if (string_0 == global::GClass0.smethod_0(""))
			{
				return 0;
			}
			if (string_0 == global::GClass0.smethod_0("1"))
			{
				return 0;
			}
			if (string_0 == global::GClass0.smethod_0("/ı"))
			{
				return 0;
			}
			if (!this.method_13(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_13(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00003124 File Offset: 0x00001324
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00003163 File Offset: 0x00001363
		private bool method_14()
		{
			return true;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00079118 File Offset: 0x00077318
		private async void method_15(List<jquotaanswer> list_0, string string_0)
		{
			Uri requestUri = new Uri(string_0);
			HttpClient httpClient = new HttpClient();
			new MediaTypeHeaderValue(global::GClass0.smethod_0("qſɾ͡ѥը٫ݽࡡ२੨ପ౮൰๭཯"));
			JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
			HttpContent httpContent = new ObjectContent<List<jquotaanswer>>(list_0, formatter);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue(global::GClass0.smethod_0("qſɾ͡ѥը٫ݽࡡ२੨ପ౮൰๭཯"));
			try
			{
				Task<HttpResponseMessage> taskAwaiter = httpClient.PostAsync(requestUri, httpContent);
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
				}
				HttpResponseMessage result = taskAwaiter.Result;
				result.EnsureSuccessStatusCode();
				Task<string> taskAwaiter3 = result.Content.ReadAsStringAsync();
				if (!taskAwaiter3.IsCompleted)
				{
					await taskAwaiter3;
				}
				string result2 = taskAwaiter3.Result;
				JsonConvert.DeserializeObject<jresponse>(result2);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0007915C File Offset: 0x0007735C
		private List<jquotaanswer> method_16()
		{
			List<jquotaanswer> list = new List<jquotaanswer>();
			jquotaanswer jquotaanswer = new jquotaanswer();
			jquotaanswer.surveyid = global::GClass0.smethod_0("5ĳȲ̰");
			jquotaanswer.surveyguid = global::GClass0.smethod_0("");
			jquotaanswer.pageid = global::GClass0.smethod_0("SĲ");
			jquotaanswer.projectid = global::GClass0.smethod_0("6İȶ̴");
			jquotaanswer.isfinish = global::GClass0.smethod_0("1");
			janswer janswer = new janswer();
			janswer.questionname = global::GClass0.smethod_0("SĲ");
			janswer.code = global::GClass0.smethod_0("2");
			jquotaanswer.qanswers.Add(janswer);
			list.Add(jquotaanswer);
			return list;
		}

		// Token: 0x040007D0 RID: 2000
		private string MySurveyId;

		// Token: 0x040007D1 RID: 2001
		private string CurPageId;

		// Token: 0x040007D2 RID: 2002
		private NavBase MyNav = new NavBase();

		// Token: 0x040007D3 RID: 2003
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040007D4 RID: 2004
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040007D5 RID: 2005
		private QSingle oQuestion = new QSingle();

		// Token: 0x040007D6 RID: 2006
		private bool ExistTextFill;

		// Token: 0x040007D7 RID: 2007
		private List<string> listPreSet = new List<string>();

		// Token: 0x040007D8 RID: 2008
		private List<Button> listButton = new List<Button>();

		// Token: 0x040007D9 RID: 2009
		private bool PageLoaded;

		// Token: 0x040007DA RID: 2010
		private int Button_Type;

		// Token: 0x040007DB RID: 2011
		private int Button_Height;

		// Token: 0x040007DC RID: 2012
		private int Button_Width;

		// Token: 0x040007DD RID: 2013
		private int Button_FontSize;

		// Token: 0x040007DE RID: 2014
		private double w_Height;

		// Token: 0x040007DF RID: 2015
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040007E0 RID: 2016
		private int SecondsWait;

		// Token: 0x040007E1 RID: 2017
		private int SecondsCountDown;

		// Token: 0x040007E2 RID: 2018
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000AB RID: 171
		[CompilerGenerated]
		[Serializable]
		private sealed class Class45
		{
			// Token: 0x0600076F RID: 1903 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D16 RID: 3350
			public static readonly SingleQuota.Class45 instance = new SingleQuota.Class45();

			// Token: 0x04000D17 RID: 3351
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
