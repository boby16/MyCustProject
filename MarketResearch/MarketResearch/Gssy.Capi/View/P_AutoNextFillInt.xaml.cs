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
	// Token: 0x0200002B RID: 43
	public partial class P_AutoNextFillInt : Page
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x00052A68 File Offset: 0x00050C68
		public P_AutoNextFillInt()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00052B70 File Offset: 0x00050D70
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
				this.Fill_Length = 1;
			}
			this.Fill_Width = (double)(this.Fill_Length * this.Fill_FontSize) * Math.Pow(0.955, (double)this.Fill_Length);
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
			if (this.oQuestion.QDefine.MIN_COUNT > 0)
			{
				this._nMin = this.oQuestion.QDefine.MIN_COUNT;
			}
			this._nMax = (int)Math.Pow(10.0, (double)this.Fill_Length) - 1;
			if (this.oQuestion.QDefine.MAX_COUNT > 0)
			{
				this._nMax = this.oQuestion.QDefine.MAX_COUNT;
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
					list3.Sort(new Comparison<SurveyDetail>(P_AutoNextFillInt.Class30.instance.method_0));
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

		// Token: 0x060002A6 RID: 678 RVA: 0x00053858 File Offset: 0x00051A58
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

		// Token: 0x060002A7 RID: 679 RVA: 0x00053A80 File Offset: 0x00051C80
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

		// Token: 0x060002A8 RID: 680 RVA: 0x00053C04 File Offset: 0x00051E04
		private void method_3(object sender, RoutedEventArgs e)
		{
			string text = (string)((Button)sender).Content;
			this.listFills[this._CurrentTextBox].Text = text;
			if (this.listFills.Count == this._CurrentTextBox + 1)
			{
				this.listFills[this._CurrentTextBox].Focus();
				return;
			}
			this.method_11(this.listFills[this._CurrentTextBox], null);
			this._CurrentTextBox++;
			this.listFills[this._CurrentTextBox].Focus();
			this.method_12(this.listFills[this._CurrentTextBox], null);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00053CBC File Offset: 0x00051EBC
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
					if (text != null && !(text == global::GClass0.smethod_0("")))
					{
						text = this.method_17(text).ToString();
					}
					else
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
				textBox.GotFocus += this.method_12;
				textBox.LostFocus += this.method_11;
				textBox.KeyDown += this.method_5;
				textBox.TextChanged += this.method_6;
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

		// Token: 0x060002AA RID: 682 RVA: 0x00054224 File Offset: 0x00052424
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
				else if (!this.method_7(textBox.Text.Trim()))
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

		// Token: 0x060002AB RID: 683 RVA: 0x0005436C File Offset: 0x0005256C
		private void method_6(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			TextChange[] array = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				string a = textBox.Text.Substring(offset, array[0].AddedLength).Trim();
				bool flag;
				if (!(a == global::GClass0.smethod_0("")) && !(a == global::GClass0.smethod_0("/")))
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

		// Token: 0x060002AC RID: 684 RVA: 0x00054440 File Offset: 0x00052640
		private bool method_7(string string_0)
		{
			if (string_0.Length > 0 && string_0.Substring(string_0.Length - 1, 1) == global::GClass0.smethod_0("/"))
			{
				string_0 = string_0.Substring(0, string_0.Length - 1);
			}
			if (string_0 == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (!this.listOtherValue.Contains(string_0))
			{
				if (this.oQuestion.QDefine.MIN_COUNT > 0 && Convert.ToDouble(string_0) < (double)this.oQuestion.QDefine.MIN_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, this.oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
				if (this.oQuestion.QDefine.MAX_COUNT > 0 && Convert.ToDouble(string_0) > (double)this.oQuestion.QDefine.MAX_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, this.oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			if (this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
			{
				string text = this.oQuestion.QDefine.CONTROL_MASK;
				if (text.IndexOf(global::GClass0.smethod_0("-")) == -1)
				{
					text += global::GClass0.smethod_0(".ı");
				}
				string arg = text.Replace(global::GClass0.smethod_0("-"), SurveyMsg.MsgFillFitReplace);
				if (this.oLogicEngine.Result(string.Concat(new string[]
				{
					global::GClass0.smethod_0(",ŉɩͱъնٯܩ"),
					string_0,
					global::GClass0.smethod_0("-"),
					text,
					global::GClass0.smethod_0("(")
				})))
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillIntFit, arg), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00054650 File Offset: 0x00052850
		private bool method_8()
		{
			new Dictionary<string, int>();
			this.oQuestion.FillTexts = new List<string>();
			int num = 0;
			int num2 = -1;
			foreach (TextBox textBox in this.listFills)
			{
				if (this.listW[num].Visibility == Visibility.Visible && (!(textBox.Text == global::GClass0.smethod_0("")) || this._nLastTextBox != num || this._nLastTextBox <= 0))
				{
					string text = textBox.Text;
					int num3 = this.method_17(text);
					if (text == global::GClass0.smethod_0(""))
					{
						MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						textBox.Focus();
						return true;
					}
					if (!this.listOtherValue.Contains(text))
					{
						if (num3 < this._nMin)
						{
							MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, this._nMin.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							textBox.Focus();
							return true;
						}
						if (num3 > this._nMax)
						{
							MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, this._nMax.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							textBox.Focus();
							return true;
						}
						if (this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
						{
							string text2 = this.oQuestion.QDefine.CONTROL_MASK;
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
								MessageBox.Show(string.Format(SurveyMsg.MsgFillIntFit, arg), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								textBox.Focus();
								return true;
							}
						}
						if (num2 > -1)
						{
							if (this.oQuestion.QCircleDefine.CONTROL_MASK == global::GClass0.smethod_0("0"))
							{
								if (num2 < num3)
								{
									MessageBox.Show(SurveyMsg.MsgFromBigToSmall, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									textBox.Focus();
									return true;
								}
							}
							else if (this.oQuestion.QCircleDefine.CONTROL_MASK == global::GClass0.smethod_0("2"))
							{
								if (num2 > num3)
								{
									MessageBox.Show(SurveyMsg.MsgFromSmallToBig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									textBox.Focus();
									return true;
								}
							}
							else if (this.oQuestion.QCircleDefine.CONTROL_MASK == global::GClass0.smethod_0("3") && this.CheckChipsLogic)
							{
								if (num2 < num3)
								{
									if (MessageBox.Show(SurveyMsg.MsgFromBigToSmall + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
									{
										textBox.Focus();
										return true;
									}
									this.CheckChipsLogic = false;
								}
							}
							else if (this.oQuestion.QCircleDefine.CONTROL_MASK == global::GClass0.smethod_0("5") && this.CheckChipsLogic && num2 > num3)
							{
								if (MessageBox.Show(SurveyMsg.MsgFromSmallToBig + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
								{
									textBox.Focus();
									return true;
								}
								this.CheckChipsLogic = false;
							}
						}
						num2 = num3;
					}
					this.oQuestion.FillTexts.Add(text);
					num++;
				}
			}
			return false;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00054A70 File Offset: 0x00052C70
		private List<VEAnswer> method_9()
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
			SurveyHelper.Answer = this.method_15(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00054C74 File Offset: 0x00052E74
		private void method_10(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave(2);
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00054CE0 File Offset: 0x00052EE0
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

		// Token: 0x060002B1 RID: 689 RVA: 0x00054DD8 File Offset: 0x00052FD8
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

		// Token: 0x060002B2 RID: 690 RVA: 0x00054E40 File Offset: 0x00053040
		private void method_11(object sender, RoutedEventArgs e = null)
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

		// Token: 0x060002B3 RID: 691 RVA: 0x00054F60 File Offset: 0x00053160
		private void method_12(object sender, RoutedEventArgs e = null)
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

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_14(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_16(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000550A8 File Offset: 0x000532A8
		private int method_17(string string_0)
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
			if (!this.method_18(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_18(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x0400052F RID: 1327
		private string MySurveyId;

		// Token: 0x04000530 RID: 1328
		private string CurPageId;

		// Token: 0x04000531 RID: 1329
		private NavBase MyNav = new NavBase();

		// Token: 0x04000532 RID: 1330
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000533 RID: 1331
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000534 RID: 1332
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000535 RID: 1333
		private UDPX oFunc = new UDPX();

		// Token: 0x04000536 RID: 1334
		private QMatrixFill oQuestion = new QMatrixFill();

		// Token: 0x04000537 RID: 1335
		private List<WrapPanel> listW = new List<WrapPanel>();

		// Token: 0x04000538 RID: 1336
		private List<TextBlock> listTexts = new List<TextBlock>();

		// Token: 0x04000539 RID: 1337
		private List<TextBlock> listLeftTexts = new List<TextBlock>();

		// Token: 0x0400053A RID: 1338
		private List<TextBlock> listRightTexts = new List<TextBlock>();

		// Token: 0x0400053B RID: 1339
		private List<TextBox> listFills = new List<TextBox>();

		// Token: 0x0400053C RID: 1340
		private int _nMin;

		// Token: 0x0400053D RID: 1341
		private int _nMax = 20;

		// Token: 0x0400053E RID: 1342
		private string _txtLeft = global::GClass0.smethod_0("");

		// Token: 0x0400053F RID: 1343
		private string _txtRight = global::GClass0.smethod_0("");

		// Token: 0x04000540 RID: 1344
		private int _nLastTextBox;

		// Token: 0x04000541 RID: 1345
		private int _CurrentTextBox;

		// Token: 0x04000542 RID: 1346
		private int BrandText_Width;

		// Token: 0x04000543 RID: 1347
		private int Fill_Height = 60;

		// Token: 0x04000544 RID: 1348
		private double Fill_Width = 80.0;

		// Token: 0x04000545 RID: 1349
		private int Fill_FontSize = 45;

		// Token: 0x04000546 RID: 1350
		private int Fill_Length = 2;

		// Token: 0x04000547 RID: 1351
		private List<string> listOtherValue = new List<string>();

		// Token: 0x04000548 RID: 1352
		private int Button_Type;

		// Token: 0x04000549 RID: 1353
		private int Button_Height;

		// Token: 0x0400054A RID: 1354
		private double Button_Width;

		// Token: 0x0400054B RID: 1355
		private int Button_FontSize;

		// Token: 0x0400054C RID: 1356
		private double w_Height;

		// Token: 0x0400054D RID: 1357
		private bool PageLoaded;

		// Token: 0x0400054E RID: 1358
		private bool CheckChipsLogic = true;

		// Token: 0x0400054F RID: 1359
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000550 RID: 1360
		private int SecondsWait;

		// Token: 0x04000551 RID: 1361
		private int SecondsCountDown;

		// Token: 0x04000552 RID: 1362
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x0200009C RID: 156
		[CompilerGenerated]
		[Serializable]
		private sealed class Class30
		{
			// Token: 0x0600073C RID: 1852 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CF1 RID: 3313
			public static readonly P_AutoNextFillInt.Class30 instance = new P_AutoNextFillInt.Class30();

			// Token: 0x04000CF2 RID: 3314
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
