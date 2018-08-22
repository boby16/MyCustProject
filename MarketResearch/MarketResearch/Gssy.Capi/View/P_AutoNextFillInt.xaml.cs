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
	public partial class P_AutoNextFillInt : Page
	{
		public P_AutoNextFillInt()
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
			this.oQuestion.Init(this.CurPageId, 0);
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
				this.oQuestion.CircleQuestionName = ((this.oQuestion.QDefine.GROUP_LEVEL == "A") ? this.oQuestion.QDefine.GROUP_CODEA : this.oQuestion.QDefine.GROUP_CODEB);
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
			string_ = ((list.Count > 1) ? list[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QCircleDefine.LIMIT_LOGIC != "")
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
			if (this.oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int j = 0; j < this.oQuestion.QCircleDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QCircleDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QCircleDetails[j].CODE_TEXT);
				}
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != "")
			{
				string_ = this.oQuestion.QDefine.CONTROL_TOOLTIP;
				list = this.oBoldTitle.ParaToList(string_, "//");
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
			if (this.oQuestion.QDefine.DETAIL_ID != "")
			{
				if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
				{
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
				if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
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
			if (this.oQuestion.QDefine.NOTE != "")
			{
				string_ = this.oQuestion.QDefine.NOTE;
				list = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list[0];
				this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, "", true);
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				if (this.listFills.Count > 0)
				{
					foreach (TextBox textBox in this.listFills)
					{
						if (textBox.Text == "")
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
			Style style = (Style)base.FindResource("SelBtnStyle");
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back") && !(navOperation == "Normal"))
			{
				navOperation = "Jump";
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
				if (this.listFills[this._nLastTextBox - 1].Text != "")
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

		private void method_2()
		{
			Style style = (Style)base.FindResource("UnSelBtnStyle");
			WrapPanel wrapPanel = this.wrapOther;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				if (surveyDetail.IS_OTHER != 0)
				{
					Button button = new Button();
					button.Name = "b_" + surveyDetail.CODE;
					button.Content = surveyDetail.CODE_TEXT;
					button.Margin = new Thickness(0.0, 10.0, 15.0, 10.0);
					button.Style = style;
					button.Tag = ((surveyDetail.EXTEND_1 == "") ? surveyDetail.CODE : surveyDetail.EXTEND_1);
					this.listOtherValue.Add(button.Tag.ToString());
					button.Click += this.method_3;
					button.FontSize = (double)this.Button_FontSize;
					button.MinWidth = this.Button_Width;
					button.MinHeight = (double)this.Button_Height;
					wrapPanel.Children.Add(button);
				}
			}
		}

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

		private void method_4()
		{
			Style style = (Style)base.FindResource("ContentMediumStyle");
			Brush brush = (Brush)base.FindResource("NormalBorderBrush");
			Brush foreground = (Brush)new BrushConverter().ConvertFromString("White");
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
				string text = "";
				if (SurveyHelper.NavOperation == "Back")
				{
					string string_ = this.oQuestion.QuestionName + "_R" + code;
					text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_);
					if (text != null && !(text == ""))
					{
						text = this.method_17(text).ToString();
					}
					else
					{
						text = "";
					}
				}
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapPanel2.Orientation = ((this.oQuestion.QCircleDefine.CONTROL_TOOLTIP.Trim().ToUpper() == "V") ? Orientation.Vertical : Orientation.Horizontal);
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
				else if (text != "")
				{
					wrapPanel2.Visibility = Visibility.Visible;
					this._nLastTextBox = num;
				}
				else if (this.listFills[this._nLastTextBox].Text != "")
				{
					wrapPanel2.Visibility = Visibility.Visible;
					this._nLastTextBox = num;
				}
				num++;
			}
		}

		private void method_5(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				int num = (int)textBox.Tag;
				if (textBox.Text.Trim() == "")
				{
					if (num > this._nLastTextBox - 2 && this._nLastTextBox > 1)
					{
						if (num >= this.listW.Count - 1)
						{
							this.listFills[num - 1].Focus();
							this.listTexts[num - 1].BringIntoView();
							return;
						}
						if (this.listFills[num + 1].Text == "")
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
				if (!(a == "") && !(a == "."))
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

		private bool method_7(string string_0)
		{
			if (string_0.Length > 0 && string_0.Substring(string_0.Length - 1, 1) == ".")
			{
				string_0 = string_0.Substring(0, string_0.Length - 1);
			}
			if (string_0 == "")
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
			if (this.oQuestion.QDefine.CONTROL_MASK != "")
			{
				string text = this.oQuestion.QDefine.CONTROL_MASK;
				if (text.IndexOf(",") == -1)
				{
					text += ",0";
				}
				string arg = text.Replace(",", SurveyMsg.MsgFillFitReplace);
				if (this.oLogicEngine.Result(string.Concat(new string[]
				{
					"$NotNum(",
					string_0,
					",",
					text,
					")"
				})))
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillIntFit, arg), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			return false;
		}

		private bool method_8()
		{
			new Dictionary<string, int>();
			this.oQuestion.FillTexts = new List<string>();
			int num = 0;
			int num2 = -1;
			foreach (TextBox textBox in this.listFills)
			{
				if (this.listW[num].Visibility == Visibility.Visible && (!(textBox.Text == "") || this._nLastTextBox != num || this._nLastTextBox <= 0))
				{
					string text = textBox.Text;
					int num3 = this.method_17(text);
					if (text == "")
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
						if (this.oQuestion.QDefine.CONTROL_MASK != "")
						{
							string text2 = this.oQuestion.QDefine.CONTROL_MASK;
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
								MessageBox.Show(string.Format(SurveyMsg.MsgFillIntFit, arg), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								textBox.Focus();
								return true;
							}
						}
						if (num2 > -1)
						{
							if (this.oQuestion.QCircleDefine.CONTROL_MASK == "1")
							{
								if (num2 < num3)
								{
									MessageBox.Show(SurveyMsg.MsgFromBigToSmall, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									textBox.Focus();
									return true;
								}
							}
							else if (this.oQuestion.QCircleDefine.CONTROL_MASK == "3")
							{
								if (num2 > num3)
								{
									MessageBox.Show(SurveyMsg.MsgFromSmallToBig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									textBox.Focus();
									return true;
								}
							}
							else if (this.oQuestion.QCircleDefine.CONTROL_MASK == "2" && this.CheckChipsLogic)
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
							else if (this.oQuestion.QCircleDefine.CONTROL_MASK == "4" && this.CheckChipsLogic && num2 > num3)
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

		private List<VEAnswer> method_9()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			string str = (this.oQuestion.QDefine.GROUP_LEVEL == "A") ? this.oQuestion.QDefine.GROUP_CODEA : this.oQuestion.QDefine.GROUP_CODEB;
			str += this.MyNav.QName_Add;
			SurveyHelper.Answer = "";
			int num = 0;
			foreach (string text in this.oQuestion.FillTexts)
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
				dictionary.Add(this.oQuestion.QCircleDetails[num].CODE, this.oFunc.StringToDouble(text));
				text2 = str + "_R" + (num + 1).ToString();
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

		private void method_10(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave(2);
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

		private void method_11(object sender, RoutedEventArgs e = null)
		{
			Brush foreground = (Brush)new BrushConverter().ConvertFromString("White");
			TextBox textBox = (TextBox)sender;
			int num = (int)textBox.Tag;
			this._CurrentTextBox = num;
			textBox.Text = textBox.Text.Trim();
			this.listTexts[num].Foreground = foreground;
			this.listLeftTexts[num].Foreground = foreground;
			this.listRightTexts[num].Foreground = foreground;
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
			if (this._nLastTextBox > 1 && this.listFills[this._nLastTextBox].Text == "" && this.listFills[this._nLastTextBox - 1].Text == "")
			{
				this.listW[this._nLastTextBox].Visibility = Visibility.Collapsed;
				this._nLastTextBox--;
			}
		}

		private void method_12(object sender, RoutedEventArgs e = null)
		{
			Brush foreground = (Brush)base.FindResource("PressedBrush");
			TextBox textBox = (TextBox)sender;
			int num = (int)textBox.Tag;
			this.listTexts[num].Foreground = foreground;
			this.listLeftTexts[num].Foreground = foreground;
			this.listRightTexts[num].Foreground = foreground;
			textBox.SelectAll();
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
			if (this._nLastTextBox < this.listW.Count - 1)
			{
				if (this.listFills[this._nLastTextBox].Text != "")
				{
					this._nLastTextBox++;
					this.listW[this._nLastTextBox].Visibility = Visibility.Visible;
				}
				if (num > 0 && num == this._nLastTextBox && this.listFills[num - 1].Text != "")
				{
					this._nLastTextBox++;
					this.listW[this._nLastTextBox].Visibility = Visibility.Visible;
				}
			}
		}

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

		private string method_14(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

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

		private string method_16(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_17(string string_0)
		{
			if (string_0 == "")
			{
				return 0;
			}
			if (string_0 == "0")
			{
				return 0;
			}
			if (string_0 == "-0")
			{
				return 0;
			}
			if (!this.method_18(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		private bool method_18(string string_0)
		{
			return new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$").IsMatch(string_0);
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QMatrixFill oQuestion = new QMatrixFill();

		private List<WrapPanel> listW = new List<WrapPanel>();

		private List<TextBlock> listTexts = new List<TextBlock>();

		private List<TextBlock> listLeftTexts = new List<TextBlock>();

		private List<TextBlock> listRightTexts = new List<TextBlock>();

		private List<TextBox> listFills = new List<TextBox>();

		private int _nMin;

		private int _nMax = 20;

		private string _txtLeft = "";

		private string _txtRight = "";

		private int _nLastTextBox;

		private int _CurrentTextBox;

		private int BrandText_Width;

		private int Fill_Height = 60;

		private double Fill_Width = 80.0;

		private int Fill_FontSize = 45;

		private int Fill_Length = 2;

		private List<string> listOtherValue = new List<string>();

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private double w_Height;

		private bool PageLoaded;

		private bool CheckChipsLogic = true;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class30
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly P_AutoNextFillInt.Class30 instance = new P_AutoNextFillInt.Class30();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
