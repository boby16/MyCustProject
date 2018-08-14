﻿using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
	// Token: 0x02000031 RID: 49
	public partial class P_FillDec2 : Page
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00060864 File Offset: 0x0005EA64
		public P_FillDec2()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000608E0 File Offset: 0x0005EAE0
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0);
			this.oQFill1.Init(this.CurPageId, 1);
			this.oQFill2.Init(this.CurPageId, 2);
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
				this.oQFill1.QuestionName = this.oQFill1.QuestionName + this.MyNav.QName_Add;
				this.oQFill2.QuestionName = this.oQFill2.QuestionName + this.MyNav.QName_Add;
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
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQFill1.QDefine.CONTROL_TOOLTIP != global::GClass0.smethod_0(""))
			{
				string_ = this.oQFill1.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore1, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
				string_ = ((list2.Count > 1) ? list2[1] : global::GClass0.smethod_0(""));
				this.oBoldTitle.SetTextBlock(this.txtAfter1, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
			}
			if (this.oQFill1.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill1.MaxLength = this.oQFill1.QDefine.CONTROL_TYPE;
				this.txtFill1.Width = (double)this.oQFill1.QDefine.CONTROL_TYPE * this.txtFill1.FontSize * Math.Pow(0.955, (double)this.oQFill1.QDefine.CONTROL_TYPE);
			}
			if (this.oQFill1.QDefine.CONTROL_HEIGHT != 0)
			{
				this.txtFill1.Height = (double)this.oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQFill1.QDefine.CONTROL_WIDTH != 0)
			{
				this.txtFill1.Width = (double)this.oQFill1.QDefine.CONTROL_WIDTH;
			}
			if (this.oQFill1.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.txtFill1.FontSize = (double)this.oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQFill2.QDefine.CONTROL_TOOLTIP != global::GClass0.smethod_0(""))
			{
				string_ = this.oQFill2.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore2, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
				string_ = ((list2.Count > 1) ? list2[1] : global::GClass0.smethod_0(""));
				this.oBoldTitle.SetTextBlock(this.txtAfter2, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
			}
			if (this.oQFill2.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill2.MaxLength = this.oQFill2.QDefine.CONTROL_TYPE;
				this.txtFill2.Width = (double)this.oQFill2.QDefine.CONTROL_TYPE * this.txtFill2.FontSize * Math.Pow(0.955, (double)this.oQFill2.QDefine.CONTROL_TYPE);
			}
			if (this.oQFill2.QDefine.CONTROL_HEIGHT != 0)
			{
				this.txtFill2.Height = (double)this.oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQFill2.QDefine.CONTROL_WIDTH != 0)
			{
				this.txtFill2.Width = (double)this.oQFill2.QDefine.CONTROL_WIDTH;
			}
			if (this.oQFill2.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.txtFill2.FontSize = (double)this.oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper() == global::GClass0.smethod_0("W"))
			{
				this.wrapFill.Orientation = Orientation.Vertical;
			}
			if (this.oQFill1.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
			{
				this.txtFill1.Text = this.oLogicEngine.stringResult(this.oQFill1.QDefine.PRESET_LOGIC);
				this.txtFill1.SelectAll();
			}
			if (this.oQFill2.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
			{
				this.txtFill2.Text = this.oLogicEngine.stringResult(this.oQFill2.QDefine.PRESET_LOGIC);
				this.txtFill2.SelectAll();
			}
			this.txtFill1.Focus();
			if (this.oQFill1.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				string_ = this.oQFill1.QDefine.NOTE;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, global::GClass0.smethod_0(""), true);
				if (list2.Count > 1)
				{
					string text = global::GClass0.smethod_0("");
					string text2 = global::GClass0.smethod_0("");
					int num = list2[1].IndexOf(global::GClass0.smethod_0("?"));
					if (num > 0)
					{
						text = this.method_9(list2[1], num + 1, -9999);
						text2 = this.method_7(list2[1], 1, num - 1);
						num = this.method_11(text2);
					}
					else
					{
						text = list2[1];
					}
					if (this.oQFill1.QDefine.GROUP_LEVEL != global::GClass0.smethod_0("") && num > 0)
					{
						this.oQFill1.InitCircle();
						string text3 = global::GClass0.smethod_0("");
						if (this.MyNav.GroupLevel == global::GClass0.smethod_0("@"))
						{
							text3 = this.MyNav.CircleACode;
						}
						if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
						{
							text3 = this.MyNav.CircleBCode;
						}
						if (text3 != global::GClass0.smethod_0(""))
						{
							foreach (SurveyDetail surveyDetail in this.oQFill1.QCircleDetails)
							{
								if (surveyDetail.CODE == text3)
								{
									text = surveyDetail.EXTEND_1;
									break;
								}
							}
						}
					}
					if (text != global::GClass0.smethod_0(""))
					{
						string text4 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + text;
						if (this.method_8(text, 1) == global::GClass0.smethod_0("\""))
						{
							text4 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + this.method_9(text, 1, -9999);
						}
						else if (!File.Exists(text4))
						{
							text4 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + text;
						}
						Image image = new Image();
						if (num > 0)
						{
							this.scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
							this.scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							image.Height = (double)num;
						}
						else if (text2 == global::GClass0.smethod_0("\""))
						{
							this.scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
							this.scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						}
						else
						{
							this.scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
							this.scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
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
							bitmapImage.UriSource = new Uri(text4, UriKind.RelativeOrAbsolute);
							bitmapImage.EndInit();
							image.Source = bitmapImage;
							this.scrollNote.Content = image;
						}
						catch (Exception)
						{
						}
					}
				}
			}
			if (this.oQuestion.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
				{
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
					string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list3 = new List<SurveyDetail>();
					for (int i = 0; i < array.Count<string>(); i++)
					{
						foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
						{
							if (surveyDetail2.CODE == array[i].ToString())
							{
								list3.Add(surveyDetail2);
								break;
							}
						}
					}
					list3.Sort(new Comparison<SurveyDetail>(P_FillDec2.Class34.instance.method_0));
					this.oQuestion.QDetails = list3;
				}
				if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
				{
					for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
					{
						this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
					}
				}
				this.Button_Height = SurveyHelper.BtnHeight;
				this.Button_FontSize = SurveyHelper.BtnFontSize;
				this.Button_Width = (double)SurveyHelper.BtnWidth;
				int control_TYPE = this.oQuestion.QDefine.CONTROL_TYPE;
				if (control_TYPE <= 2)
				{
					if (control_TYPE != 1)
					{
						if (control_TYPE == 2)
						{
							this.Button_Width = 440.0;
							this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
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
						}
					}
					else
					{
						this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
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
					}
				}
				else if (control_TYPE != 20)
				{
					if (control_TYPE == 30)
					{
						this.Button_Height = SurveyHelper.BtnSmallHeight;
						this.Button_FontSize = SurveyHelper.BtnSmallFontSize;
						this.Button_Width = (double)SurveyHelper.BtnSmallWidth;
					}
				}
				else
				{
					this.Button_Height = SurveyHelper.BtnMediumHeight;
					this.Button_FontSize = SurveyHelper.BtnMediumFontSize;
					this.Button_Width = (double)SurveyHelper.BtnMediumWidth;
				}
				this.method_2();
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				if (this.txtFill1.Text == global::GClass0.smethod_0(""))
				{
					this.txtFill1.Text = autoFill.FillDec(this.oQFill1.QDefine);
				}
				if (this.txtFill2.Text == global::GClass0.smethod_0(""))
				{
					this.txtFill2.Text = autoFill.FillDec(this.oQFill2.QDefine);
				}
				if (autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")))
			{
				if (!(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")))
				{
					if (!(navOperation == global::GClass0.smethod_0("NŶɯͱ")))
					{
					}
				}
				else if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.txtFill1.Text != global::GClass0.smethod_0("") && this.txtFill2.Text != global::GClass0.smethod_0("") && !SurveyHelper.AutoFill)
				{
					this.btnNav_Click(this, e);
				}
			}
			else
			{
				this.txtFill1.Text = this.oQFill1.ReadAnswerByQuestionName(this.MySurveyId, this.oQFill1.QuestionName);
				this.txtFill2.Text = this.oQFill2.ReadAnswerByQuestionName(this.MySurveyId, this.oQFill2.QuestionName);
				foreach (object obj in this.wrapButton.Children)
				{
					Button button = (Button)obj;
					list2 = this.oBoldTitle.ParaToList((string)button.Tag, global::GClass0.smethod_0("\u007f"));
					string b = list2[0];
					string b2 = (list2.Count > 1) ? list2[1] : global::GClass0.smethod_0("");
					if (this.txtFill1.Text == b && this.txtFill2.Text == b2)
					{
						button.Style = style;
						this.txtFill1.Background = Brushes.LightGray;
						this.txtFill1.Foreground = Brushes.LightGray;
						this.txtFill2.Background = Brushes.LightGray;
						this.txtFill2.Foreground = Brushes.LightGray;
						this.txtFill1.IsEnabled = false;
						this.txtFill2.IsEnabled = false;
						break;
					}
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
			this.txtFill1.SelectAll();
			this.txtFill1.Focus();
			this.PageLoaded = true;
			this.PageEntry = true;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00061E30 File Offset: 0x00060030
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				WrapPanel wrapPanel = this.wrapButton;
				if (this.Button_Type == 0)
				{
					if (this.scrollNote.ComputedVerticalScrollBarVisibility == Visibility.Collapsed)
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

		// Token: 0x06000331 RID: 817 RVA: 0x00061EB4 File Offset: 0x000600B4
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapButton;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = surveyDetail.EXTEND_1 + global::GClass0.smethod_0("\u007f") + surveyDetail.EXTEND_2;
				button.Click += this.method_3;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				wrapPanel.Children.Add(button);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00062000 File Offset: 0x00060200
		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			List<string> list = this.oBoldTitle.ParaToList((string)button.Tag, global::GClass0.smethod_0("\u007f"));
			string text = list[0];
			string text2 = (list.Count > 1) ? list[1] : global::GClass0.smethod_0("");
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				if (this.txtFill1.IsEnabled)
				{
					this.txtFill1.Tag = this.txtFill1.Text;
					this.txtFill1.Background = Brushes.LightGray;
					this.txtFill1.Foreground = Brushes.LightGray;
					this.txtFill1.IsEnabled = false;
					this.txtFill2.Tag = this.txtFill2.Text;
					this.txtFill2.Background = Brushes.LightGray;
					this.txtFill2.Foreground = Brushes.LightGray;
					this.txtFill2.IsEnabled = false;
				}
				this.txtFill1.Text = text;
				this.txtFill2.Text = text2;
                IEnumerator enumerator = this.wrapButton.Children.GetEnumerator();
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Button button2 = (Button)obj;
						button2.Style = ((button2.Name == button.Name) ? style : style2);
					}
					return;
				}
			}
			this.txtFill1.Text = (string)this.txtFill1.Tag;
			this.txtFill1.IsEnabled = true;
			this.txtFill1.Background = Brushes.White;
			this.txtFill1.Foreground = Brushes.Black;
			this.txtFill2.Text = (string)this.txtFill2.Tag;
			this.txtFill2.IsEnabled = true;
			this.txtFill2.Background = Brushes.White;
			this.txtFill2.Foreground = Brushes.Black;
			button.Style = style2;
			this.txtFill1.Focus();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00002E67 File Offset: 0x00001067
		private void txtFill2_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00062260 File Offset: 0x00060460
		private void txtFill2_TextChanged(object sender, TextChangedEventArgs e)
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
					return;
				}
				if (this.PageEntry && this.oQuestion.QDefine.PAGE_COUNT_DOWN == -1)
				{
					if (textBox.Name == global::GClass0.smethod_0("|ſɲ̓ѭկٮܰ") && textBox.Text.Length == this.oQFill1.QDefine.CONTROL_TYPE)
					{
						this.txtFill2.SelectAll();
						this.txtFill2.Focus();
						return;
					}
					if (textBox.Name == global::GClass0.smethod_0("|ſɲ̓ѭկٮܳ") && textBox.Text.Length == this.oQFill2.QDefine.CONTROL_TYPE)
					{
						this.btnNav_Click(null, null);
					}
				}
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000623C4 File Offset: 0x000605C4
		private bool method_4()
		{
			string text = this.txtFill1.Text;
			double num = 0.0;
			if (text.Length > 0 && text.Substring(text.Length - 1, 1) == global::GClass0.smethod_0("/"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			if (text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill1.SelectAll();
				this.txtFill1.Focus();
				return true;
			}
			if (this.txtFill1.IsEnabled)
			{
				num = Convert.ToDouble(text);
				if (this.oQFill1.QDefine.MIN_COUNT > 0 && num < (double)this.oQFill1.QDefine.MIN_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, this.oQFill1.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill1.SelectAll();
					this.txtFill1.Focus();
					return true;
				}
				if (this.oQFill1.QDefine.MAX_COUNT > 0 && num > (double)this.oQFill1.QDefine.MAX_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, this.oQFill1.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill1.SelectAll();
					this.txtFill1.Focus();
					return true;
				}
				if (this.oQFill1.QDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
				{
					string text2 = this.oQFill1.QDefine.CONTROL_MASK;
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
						this.txtFill1.SelectAll();
						this.txtFill1.Focus();
						return true;
					}
				}
			}
			this.oQFill1.FillText = text;
			text = this.txtFill2.Text;
			double num2 = 0.0;
			if (text.Length > 0 && text.Substring(text.Length - 1, 1) == global::GClass0.smethod_0("/"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			if (text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill2.SelectAll();
				this.txtFill2.Focus();
				return true;
			}
			if (this.txtFill2.IsEnabled)
			{
				num2 = Convert.ToDouble(text);
				if (this.oQFill2.QDefine.MIN_COUNT > 0 && num2 < (double)this.oQFill2.QDefine.MIN_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, this.oQFill2.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill2.SelectAll();
					this.txtFill2.Focus();
					return true;
				}
				if (this.oQFill2.QDefine.MAX_COUNT > 0 && num2 > (double)this.oQFill2.QDefine.MAX_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, this.oQFill2.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill2.SelectAll();
					this.txtFill2.Focus();
					return true;
				}
				if (this.oQFill2.QDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
				{
					string text3 = this.oQFill2.QDefine.CONTROL_MASK;
					if (text3.IndexOf(global::GClass0.smethod_0("-")) == -1)
					{
						text3 += global::GClass0.smethod_0(".ı");
					}
					string arg2 = text3.Replace(global::GClass0.smethod_0("-"), SurveyMsg.MsgFillFitReplace);
					if (this.oLogicEngine.Result(string.Concat(new string[]
					{
						global::GClass0.smethod_0(",ŉɩͱъնٯܩ"),
						text,
						global::GClass0.smethod_0("-"),
						text3,
						global::GClass0.smethod_0("(")
					})))
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgFillFit, arg2), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.txtFill2.SelectAll();
						this.txtFill2.Focus();
						return true;
					}
				}
			}
			this.oQFill2.FillText = text;
			if (this.txtFill1.IsEnabled && this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0("8"))
			{
				if (!(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("")) && !(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("1")))
				{
					if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("0"))
					{
						if (num > num2)
						{
							MessageBox.Show(SurveyMsg.MsgRightNotSmallLeft, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							this.txtFill1.SelectAll();
							this.txtFill1.Focus();
							return true;
						}
					}
					else if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("3"))
					{
						if (num <= num2)
						{
							MessageBox.Show(SurveyMsg.MsgRightSmallLeft, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							this.txtFill1.SelectAll();
							this.txtFill1.Focus();
							return true;
						}
					}
					else if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("2"))
					{
						if (num < num2)
						{
							MessageBox.Show(SurveyMsg.MsgLeftNotSmallRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							this.txtFill1.SelectAll();
							this.txtFill1.Focus();
							return true;
						}
					}
					else if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("9"))
					{
						DateTime date = DateTime.Now.Date;
						if (Convert.ToDateTime(this.txtFill1.Text + global::GClass0.smethod_0(",") + this.txtFill2.Text + global::GClass0.smethod_0(".ĲȰ")) > date)
						{
							MessageBox.Show(SurveyMsg.MsgNotAfterYM, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							this.txtFill1.SelectAll();
							this.txtFill1.Focus();
							return true;
						}
					}
				}
				else if (num >= num2)
				{
					MessageBox.Show(SurveyMsg.MsgLeftSmallRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill1.SelectAll();
					this.txtFill1.Focus();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00062B18 File Offset: 0x00060D18
		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQFill1.QuestionName,
				CODE = this.oQFill1.FillText
			});
			SurveyHelper.Answer = this.oQFill1.QuestionName + global::GClass0.smethod_0("<") + this.oQFill1.FillText;
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQFill2.QuestionName,
				CODE = this.oQFill2.FillText
			});
			SurveyHelper.Answer = string.Concat(new string[]
			{
				SurveyHelper.Answer,
				global::GClass0.smethod_0("-"),
				this.oQFill2.QuestionName,
				global::GClass0.smethod_0("<"),
				this.oQFill2.FillText
			});
			return list;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00062C04 File Offset: 0x00060E04
		private void method_6(List<VEAnswer> list_0)
		{
			this.oQFill1.BeforeSave();
			this.oQFill1.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			this.oQFill2.BeforeSave();
			this.oQFill2.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00062C54 File Offset: 0x00060E54
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

		// Token: 0x06000339 RID: 825 RVA: 0x00062D4C File Offset: 0x00060F4C
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

		// Token: 0x0600033A RID: 826 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill2_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill2_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x0600033D RID: 829 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x0600033F RID: 831 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00062DB4 File Offset: 0x00060FB4
		private int method_11(string string_0)
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
			if (!this.method_12(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_12(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00002E87 File Offset: 0x00001087
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQFill1.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000626 RID: 1574
		private string MySurveyId;

		// Token: 0x04000627 RID: 1575
		private string CurPageId;

		// Token: 0x04000628 RID: 1576
		private NavBase MyNav = new NavBase();

		// Token: 0x04000629 RID: 1577
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400062A RID: 1578
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400062B RID: 1579
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400062C RID: 1580
		private QBase oQuestion = new QBase();

		// Token: 0x0400062D RID: 1581
		private QFill oQFill1 = new QFill();

		// Token: 0x0400062E RID: 1582
		private QFill oQFill2 = new QFill();

		// Token: 0x0400062F RID: 1583
		private string SelectedValue;

		// Token: 0x04000630 RID: 1584
		private bool PageLoaded;

		// Token: 0x04000631 RID: 1585
		private int Button_Type;

		// Token: 0x04000632 RID: 1586
		private int Button_Height;

		// Token: 0x04000633 RID: 1587
		private double Button_Width;

		// Token: 0x04000634 RID: 1588
		private int Button_FontSize;

		// Token: 0x04000635 RID: 1589
		private bool PageEntry;

		// Token: 0x04000636 RID: 1590
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000637 RID: 1591
		private int SecondsWait;

		// Token: 0x04000638 RID: 1592
		private int SecondsCountDown;

		// Token: 0x04000639 RID: 1593
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000A0 RID: 160
		[CompilerGenerated]
		[Serializable]
		private sealed class Class34
		{
			// Token: 0x0600074B RID: 1867 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CFC RID: 3324
			public static readonly P_FillDec2.Class34 instance = new P_FillDec2.Class34();

			// Token: 0x04000CFD RID: 3325
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
