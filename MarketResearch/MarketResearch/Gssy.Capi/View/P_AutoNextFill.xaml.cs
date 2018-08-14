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
	// Token: 0x02000029 RID: 41
	public partial class P_AutoNextFill : Page
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0004E23C File Offset: 0x0004C43C
		public P_AutoNextFill()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0004E338 File Offset: 0x0004C538
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0);
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
			{
				this.MyNav.GroupLevel = global::GClass0.smethod_0("@");
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				this.MyNav.GetCircleInfo(this.MySurveyId);
				this.oQuestion.QuestionName = this.oQuestion.QuestionName + this.MyNav.QName_Add;
				this.oQuestion.CircleQuestionName = ((this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("@")) ? this.oQuestion.QDefine.GROUP_CODEA : this.oQuestion.QDefine.GROUP_CODEB);
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
				this.MyNav.GroupLevel = global::GClass0.smethod_0("");
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
			}
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list.Count > 1) ? list[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QCircleDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
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
			if (this.oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int j = 0; j < this.oQuestion.QCircleDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QCircleDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QCircleDetails[j].CODE_TEXT);
				}
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
			{
				string_ = this.oQuestion.QDefine.CONTROL_TOOLTIP;
				list = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				this._txtLeft = list[0];
				if (list.Count > 1)
				{
					this._txtRight = list[1];
				}
			}
			this.Fill_Length = this.oQuestion.QDefine.CONTROL_TYPE;
			if (this.Fill_Length == 0)
			{
				this.Fill_Length = 250;
			}
			this.Fill_Width = 400.0;
			if (this.oQuestion.QCircleDefine.CONTROL_WIDTH != 0)
			{
				this.BrandText_Width = this.oQuestion.QCircleDefine.TITLE_FONTSIZE;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.Fill_Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				this.Fill_Height = this.oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				this.Fill_FontSize = this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			this.method_4();
			this.Button_Type = this.oQuestion.QCircleDefine.CONTROL_TYPE;
			if (this.Button_Type == 0)
			{
				this.Button_Type = 4;
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
					string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list3 = new List<SurveyDetail>();
					for (int k = 0; k < array2.Count<string>(); k++)
					{
						foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
						{
							if (surveyDetail2.CODE == array2[k].ToString())
							{
								list3.Add(surveyDetail2);
								break;
							}
						}
					}
					list3.Sort(new Comparison<SurveyDetail>(P_AutoNextFill.Class28.instance.method_0));
					this.oQuestion.QDetails = list3;
				}
				if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
				{
					for (int l = 0; l < this.oQuestion.QDetails.Count<SurveyDetail>(); l++)
					{
						this.oQuestion.QDetails[l].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[l].CODE_TEXT);
					}
				}
				this.Button_Width = 200.0;
				this.Button_Height = SurveyHelper.BtnHeight;
				this.Button_FontSize = SurveyHelper.BtnFontSize;
				if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
				{
					this.Button_Height = this.oQuestion.QCircleDefine.CONTROL_HEIGHT;
				}
				if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
				{
					this.Button_Width = (double)this.oQuestion.QCircleDefine.CONTROL_WIDTH;
				}
				if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
				{
					this.Button_FontSize = this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
				}
				this.method_2();
			}
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				string_ = this.oQuestion.QDefine.NOTE;
				list = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list[0];
				this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, global::GClass0.smethod_0(""), true);
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				if (this.listFills.Count > 0)
				{
					foreach (TextBox textBox in this.listFills)
					{
						if (textBox.Text == global::GClass0.smethod_0(""))
						{
							textBox.Text = autoFill.FillInt(this.oQuestion.QDefine);
						}
					}
					if (autoFill.AutoNext(this.oQuestion.QDefine))
					{
						this.btnNav_Click(this, e);
					}
				}
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")) && !(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")))
			{
				navOperation = global::GClass0.smethod_0("NŶɯͱ");
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
			if (this._nLastTextBox > 0)
			{
				if (this.listFills[this._nLastTextBox - 1].Text != global::GClass0.smethod_0(""))
				{
					this.listFills[this._nLastTextBox - 1].Focus();
				}
				else if (this.listFills.Count > 0)
				{
					this.listFills[0].Focus();
				}
			}
			else if (this.listFills.Count > 0)
			{
				this.listFills[0].Focus();
			}
			this.PageLoaded = true;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0004EF98 File Offset: 0x0004D198
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
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
							goto IL_1CC;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					IL_1CC:
					if (this.Button_Type != 3)
					{
						if (this.Button_Type != 4)
						{
							scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
							goto IL_1FE;
						}
					}
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					IL_1FE:
					this.PageLoaded = false;
				}
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0004F1C0 File Offset: 0x0004D3C0
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapOther;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				if (surveyDetail.IS_OTHER != 0)
				{
					Button button = new Button();
					button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
					button.Content = surveyDetail.CODE_TEXT;
					button.Margin = new Thickness(0.0, 10.0, 15.0, 10.0);
					button.Style = style;
					button.Tag = ((surveyDetail.EXTEND_1 == global::GClass0.smethod_0("")) ? surveyDetail.CODE : surveyDetail.EXTEND_1);
					this.listOtherValue.Add(button.Tag.ToString());
					button.Click += this.method_3;
					button.FontSize = (double)this.Button_FontSize;
					button.MinWidth = this.Button_Width;
					button.MinHeight = (double)this.Button_Height;
					wrapPanel.Children.Add(button);
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0004F344 File Offset: 0x0004D544
		private void method_3(object sender, RoutedEventArgs e)
		{
			string text = (string)((Button)sender).Content;
			this.listFills[this._CurrentTextBox].Text = text;
			if (this.listFills.Count == this._CurrentTextBox + 1)
			{
				this.listFills[this._CurrentTextBox].Focus();
				return;
			}
			this.method_10(this.listFills[this._CurrentTextBox], null);
			this._CurrentTextBox++;
			this.listFills[this._CurrentTextBox].Focus();
			this.method_11(this.listFills[this._CurrentTextBox], null);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0004F3FC File Offset: 0x0004D5FC
		private void method_4()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
			Brush brush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			WrapPanel wrapPanel = this.wrapPanel1;
			if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 1)
			{
				wrapPanel.Orientation = Orientation.Horizontal;
			}
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
			{
				string code = surveyDetail.CODE;
				string code_TEXT = surveyDetail.CODE_TEXT;
				string text = global::GClass0.smethod_0("");
				if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
				{
					string string_ = this.oQuestion.QuestionName + global::GClass0.smethod_0("]œ") + code;
					text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_);
					if (text == null || text == global::GClass0.smethod_0(""))
					{
						text = global::GClass0.smethod_0("");
					}
				}
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapPanel2.Orientation = ((this.oQuestion.QCircleDefine.CONTROL_TOOLTIP.Trim().ToUpper() == global::GClass0.smethod_0("W")) ? Orientation.Vertical : Orientation.Horizontal);
				wrapPanel2.Margin = new Thickness(20.0, 20.0, 20.0, 20.0);
				wrapPanel2.Visibility = Visibility.Collapsed;
				wrapPanel.Children.Add(wrapPanel2);
				this.listW.Add(wrapPanel2);
				WrapPanel wrapPanel3 = new WrapPanel();
				wrapPanel3.VerticalAlignment = VerticalAlignment.Center;
				if (this.BrandText_Width > 0)
				{
					wrapPanel3.Width = (double)this.BrandText_Width;
				}
				wrapPanel2.Children.Add(wrapPanel3);
				TextBlock textBlock = new TextBlock();
				textBlock.Text = code_TEXT;
				textBlock.Style = style;
				textBlock.Foreground = foreground;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.FontSize = (double)this.Fill_FontSize;
				textBlock.Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
				textBlock.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel3.Children.Add(textBlock);
				this.listTexts.Add(textBlock);
				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;
				stackPanel.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel2.Children.Add(stackPanel);
				TextBlock textBlock2 = new TextBlock();
				textBlock2.Text = this._txtLeft;
				textBlock2.Style = style;
				textBlock2.Foreground = foreground;
				textBlock2.TextWrapping = TextWrapping.Wrap;
				textBlock2.FontSize = (double)this.Fill_FontSize;
				textBlock2.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBlock2.VerticalAlignment = VerticalAlignment.Center;
				stackPanel.Children.Add(textBlock2);
				this.listLeftTexts.Add(textBlock2);
				TextBox textBox = new TextBox();
				textBox.Text = text;
				textBox.Tag = num;
				textBox.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBox.FontSize = (double)this.Fill_FontSize;
				textBox.Width = this.Fill_Width;
				textBox.Height = (double)this.Fill_Height;
				textBox.MaxLength = this.Fill_Length;
				textBox.VerticalAlignment = VerticalAlignment.Center;
				textBox.HorizontalAlignment = HorizontalAlignment.Right;
				textBox.GotFocus += this.method_11;
				textBox.LostFocus += this.method_10;
				textBox.KeyDown += this.method_5;
				stackPanel.Children.Add(textBox);
				this.listFills.Add(textBox);
				TextBlock textBlock3 = new TextBlock();
				textBlock3.Text = this._txtRight;
				textBlock3.Style = style;
				textBlock3.Foreground = foreground;
				textBlock3.TextWrapping = TextWrapping.Wrap;
				textBlock3.FontSize = (double)this.Fill_FontSize;
				textBlock3.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBlock3.VerticalAlignment = VerticalAlignment.Center;
				stackPanel.Children.Add(textBlock3);
				this.listRightTexts.Add(textBlock3);
				if (num < 2)
				{
					wrapPanel2.Visibility = Visibility.Visible;
					this._nLastTextBox = num;
				}
				else if (text != global::GClass0.smethod_0(""))
				{
					wrapPanel2.Visibility = Visibility.Visible;
					this._nLastTextBox = num;
				}
				else if (this.listFills[this._nLastTextBox].Text != global::GClass0.smethod_0(""))
				{
					wrapPanel2.Visibility = Visibility.Visible;
					this._nLastTextBox = num;
				}
				num++;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0004F93C File Offset: 0x0004DB3C
		private void method_5(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				int num = (int)textBox.Tag;
				if (textBox.Text.Trim() == global::GClass0.smethod_0(""))
				{
					if (num > this._nLastTextBox - 2 && this._nLastTextBox > 1)
					{
						if (num >= this.listW.Count - 1)
						{
							this.listFills[num - 1].Focus();
							this.listTexts[num - 1].BringIntoView();
							return;
						}
						if (this.listFills[num + 1].Text == global::GClass0.smethod_0(""))
						{
							this.listFills[num - 1].Focus();
							this.listTexts[num - 1].BringIntoView();
							return;
						}
					}
				}
				else if (!this.method_6(textBox.Text.Trim()))
				{
					if (num < this.listFills.Count - 1)
					{
						this.listFills[num + 1].Focus();
						return;
					}
					this.listFills[0].Focus();
					this.listTexts[0].BringIntoView();
				}
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00002C96 File Offset: 0x00000E96
		private bool method_6(string string_0)
		{
			if (string_0 == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			return false;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0004FA84 File Offset: 0x0004DC84
		private bool method_7()
		{
			new Dictionary<string, int>();
			this.oQuestion.FillTexts = new List<string>();
			int num = 0;
			foreach (TextBox textBox in this.listFills)
			{
				if (this.listW[num].Visibility == Visibility.Visible && (!(textBox.Text == global::GClass0.smethod_0("")) || this._nLastTextBox != num || this._nLastTextBox <= 0))
				{
					string text = textBox.Text;
					if (text == global::GClass0.smethod_0(""))
					{
						MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						textBox.Focus();
						return true;
					}
					text = this.oQuestion.ConvertText(text, this.oQuestion.QDefine.CONTROL_MASK);
					this.oQuestion.FillTexts.Add(text);
					num++;
				}
			}
			return false;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0004FB9C File Offset: 0x0004DD9C
		private List<VEAnswer> method_8()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			string str = (this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("@")) ? this.oQuestion.QDefine.GROUP_CODEA : this.oQuestion.QDefine.GROUP_CODEB;
			str += this.MyNav.QName_Add;
			SurveyHelper.Answer = global::GClass0.smethod_0("");
			int num = 0;
			foreach (string text in this.oQuestion.FillTexts)
			{
				string text2 = this.oQuestion.QuestionName + global::GClass0.smethod_0("]œ") + this.oQuestion.QCircleDetails[num].CODE;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = text
				});
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					text2,
					global::GClass0.smethod_0("<"),
					text
				});
				dictionary.Add(this.oQuestion.QCircleDetails[num].CODE, this.oFunc.StringToDouble(text));
				text2 = str + global::GClass0.smethod_0("]œ") + (num + 1).ToString();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = this.oQuestion.QCircleDetails[num].CODE
				});
				num++;
			}
			SurveyHelper.Answer = this.method_14(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0004FDA0 File Offset: 0x0004DFA0
		private void method_9(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave(2);
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0004FE0C File Offset: 0x0004E00C
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_7())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_8();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_9(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0004FF04 File Offset: 0x0004E104
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

		// Token: 0x06000282 RID: 642 RVA: 0x0004FF6C File Offset: 0x0004E16C
		private void method_10(object sender, RoutedEventArgs e = null)
		{
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			TextBox textBox = (TextBox)sender;
			int num = (int)textBox.Tag;
			this._CurrentTextBox = num;
			textBox.Text = textBox.Text.Trim();
			this.listTexts[num].Foreground = foreground;
			this.listLeftTexts[num].Foreground = foreground;
			this.listRightTexts[num].Foreground = foreground;
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
			if (this._nLastTextBox > 1 && this.listFills[this._nLastTextBox].Text == global::GClass0.smethod_0("") && this.listFills[this._nLastTextBox - 1].Text == global::GClass0.smethod_0(""))
			{
				this.listW[this._nLastTextBox].Visibility = Visibility.Collapsed;
				this._nLastTextBox--;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0005008C File Offset: 0x0004E28C
		private void method_11(object sender, RoutedEventArgs e = null)
		{
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			TextBox textBox = (TextBox)sender;
			int num = (int)textBox.Tag;
			this.listTexts[num].Foreground = foreground;
			this.listLeftTexts[num].Foreground = foreground;
			this.listRightTexts[num].Foreground = foreground;
			textBox.SelectAll();
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
			if (this._nLastTextBox < this.listW.Count - 1)
			{
				if (this.listFills[this._nLastTextBox].Text != global::GClass0.smethod_0(""))
				{
					this._nLastTextBox++;
					this.listW[this._nLastTextBox].Visibility = Visibility.Visible;
				}
				if (num > 0 && num == this._nLastTextBox && this.listFills[num - 1].Text != global::GClass0.smethod_0(""))
				{
					this._nLastTextBox++;
					this.listW[this._nLastTextBox].Visibility = Visibility.Visible;
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_12(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x06000285 RID: 645 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_13(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_14(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x06000287 RID: 647 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_15(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000501D4 File Offset: 0x0004E3D4
		private int method_16(string string_0)
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
			if (!this.method_17(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_17(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x040004D8 RID: 1240
		private string MySurveyId;

		// Token: 0x040004D9 RID: 1241
		private string CurPageId;

		// Token: 0x040004DA RID: 1242
		private NavBase MyNav = new NavBase();

		// Token: 0x040004DB RID: 1243
		private PageNav oPageNav = new PageNav();

		// Token: 0x040004DC RID: 1244
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040004DD RID: 1245
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040004DE RID: 1246
		private UDPX oFunc = new UDPX();

		// Token: 0x040004DF RID: 1247
		private QMatrixFill oQuestion = new QMatrixFill();

		// Token: 0x040004E0 RID: 1248
		private List<WrapPanel> listW = new List<WrapPanel>();

		// Token: 0x040004E1 RID: 1249
		private List<TextBlock> listTexts = new List<TextBlock>();

		// Token: 0x040004E2 RID: 1250
		private List<TextBlock> listLeftTexts = new List<TextBlock>();

		// Token: 0x040004E3 RID: 1251
		private List<TextBlock> listRightTexts = new List<TextBlock>();

		// Token: 0x040004E4 RID: 1252
		private List<TextBox> listFills = new List<TextBox>();

		// Token: 0x040004E5 RID: 1253
		private string _txtLeft = global::GClass0.smethod_0("");

		// Token: 0x040004E6 RID: 1254
		private string _txtRight = global::GClass0.smethod_0("");

		// Token: 0x040004E7 RID: 1255
		private int _nLastTextBox;

		// Token: 0x040004E8 RID: 1256
		private int _CurrentTextBox;

		// Token: 0x040004E9 RID: 1257
		private int BrandText_Width;

		// Token: 0x040004EA RID: 1258
		private int Fill_Height = 60;

		// Token: 0x040004EB RID: 1259
		private double Fill_Width = 80.0;

		// Token: 0x040004EC RID: 1260
		private int Fill_FontSize = 45;

		// Token: 0x040004ED RID: 1261
		private int Fill_Length = 2;

		// Token: 0x040004EE RID: 1262
		private List<string> listOtherValue = new List<string>();

		// Token: 0x040004EF RID: 1263
		private int Button_Type;

		// Token: 0x040004F0 RID: 1264
		private int Button_Height;

		// Token: 0x040004F1 RID: 1265
		private double Button_Width;

		// Token: 0x040004F2 RID: 1266
		private int Button_FontSize;

		// Token: 0x040004F3 RID: 1267
		private double w_Height;

		// Token: 0x040004F4 RID: 1268
		private bool PageLoaded;

		// Token: 0x040004F5 RID: 1269
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040004F6 RID: 1270
		private int SecondsWait;

		// Token: 0x040004F7 RID: 1271
		private int SecondsCountDown;

		// Token: 0x040004F8 RID: 1272
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x0200009A RID: 154
		[CompilerGenerated]
		[Serializable]
		private sealed class Class28
		{
			// Token: 0x06000736 RID: 1846 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CED RID: 3309
			public static readonly P_AutoNextFill.Class28 instance = new P_AutoNextFill.Class28();

			// Token: 0x04000CEE RID: 3310
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
