using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
	// Token: 0x0200002D RID: 45
	public partial class P_ChipGame : Page
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x00057844 File Offset: 0x00055A44
		public P_ChipGame()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00057918 File Offset: 0x00055B18
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
			if (this.oQuestion.QCircleDefine.SHOW_LOGIC.Trim() != global::GClass0.smethod_0(""))
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QCircleDefine.SHOW_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count<string>(); k++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail2.CODE == array2[k].ToString())
						{
							list3.Add(surveyDetail2);
							break;
						}
					}
				}
				this.oQuestion.QCircleDetails = list3;
			}
			else if (this.oQuestion.QCircleDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomCircleDetails();
			}
			if (this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("0") || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("2") || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("/İ") || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("/Ĳ"))
			{
				this.used.Visibility = Visibility.Visible;
				if (this.oQuestion.QCircleDefine.NOTE != global::GClass0.smethod_0(""))
				{
					string_ = this.oQuestion.QCircleDefine.NOTE;
					list = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
					string_ = list[0];
					this.oBoldTitle.SetTextBlock(this.txtBefore1, string_, 0, global::GClass0.smethod_0(""), true);
					if (list.Count > 1)
					{
						string_ = list[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter1, string_, 0, global::GClass0.smethod_0(""), true);
					}
				}
			}
			if (this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("3") || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("2") || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("/ĳ") || this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("/Ĳ"))
			{
				this.unused.Visibility = Visibility.Visible;
				if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0(""))
				{
					string_ = this.oQuestion.QDefine.NOTE;
					list = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
					string_ = list[0];
					this.oBoldTitle.SetTextBlock(this.txtBefore, string_, 0, global::GClass0.smethod_0(""), true);
					if (list.Count > 1)
					{
						string_ = list[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter, string_, 0, global::GClass0.smethod_0(""), true);
					}
				}
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
			{
				this.oQuestion.QDefine.CONTROL_TOOLTIP = this.oLogicEngine.doubleResult(this.oQuestion.QDefine.CONTROL_TOOLTIP).ToString();
				this.nTotalChips = this.method_17(this.oQuestion.QDefine.CONTROL_TOOLTIP);
				this.nMaxChips = this.nTotalChips;
				this.txtFill.Text = this.nTotalChips.ToString();
			}
			if (this.oQuestion.QDefine.MIN_COUNT > 0)
			{
				this.nMin = this.oQuestion.QDefine.MIN_COUNT;
			}
			this.nMax = ((this.oQuestion.QDefine.MAX_COUNT > 0) ? this.oQuestion.QDefine.MAX_COUNT : this.nMaxChips);
			this.Fill_Length = this.oQuestion.QDefine.CONTROL_TOOLTIP.Length;
			this.Fill_Width = (double)this.Fill_Length * this.txtFill.FontSize * Math.Pow(0.955, (double)this.Fill_Length);
			if (this.Fill_Width < 20.0)
			{
				this.Fill_Width = 20.0;
			}
			if (this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == global::GClass0.smethod_0("I"))
			{
				this.BrandText_Width = 250;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_WIDTH != 0)
			{
				this.BrandText_Width = this.oQuestion.QCircleDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.Fill_Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_HEIGHT != 0)
			{
				this.Fill_Height = this.oQuestion.QCircleDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE != 0)
			{
				this.Fill_FontSize = this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
			}
			this.method_2();
			this.nTotalChips = this.nMaxChips - this.nUseChips;
			this.txtUsed.Text = this.nUseChips.ToString();
			this.txtFill.Text = this.nTotalChips.ToString();
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				if (this.ChipFills.Count > 0)
				{
					int num = this.nTotalChips;
					foreach (TextBox textBox in this.ChipFills)
					{
						if (num <= this.nMax)
						{
							textBox.Text = num.ToString();
							this.method_6(textBox);
							break;
						}
						textBox.Text = this.nMax.ToString();
						num -= this.nMax;
						this.method_6(textBox);
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
			this.PageLoaded = true;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00058608 File Offset: 0x00056808
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				WrapPanel wrapPanel = this.wrapPanel1;
				this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
				if (this.Button_Type == 0)
				{
					if (this.scrollmain.ComputedVerticalScrollBarVisibility == Visibility.Collapsed)
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

		// Token: 0x060002D8 RID: 728 RVA: 0x000586A4 File Offset: 0x000568A4
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
			Brush brush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			WrapPanel wrapPanel = this.wrapPanel1;
			bool flag = this.method_17(this.oQuestion.QCircleDefine.CONTROL_MASK) > 0;
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
			{
				string code = surveyDetail.CODE;
				string text = surveyDetail.CODE_TEXT;
				if (flag)
				{
					text = global::GClass0.smethod_0(")") + (num + 1).ToString() + global::GClass0.smethod_0("+ġ") + text;
				}
				string text2 = this.nMin.ToString();
				if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
				{
					string string_ = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ł") + code;
					text2 = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_);
					int num2 = this.method_17(text2);
					text2 = num2.ToString();
					this.nUseChips += num2;
				}
				else
				{
					this.nUseChips += this.nMin;
				}
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapPanel2.Orientation = ((this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == global::GClass0.smethod_0("I")) ? Orientation.Horizontal : Orientation.Vertical);
				wrapPanel2.Margin = new Thickness(20.0, 20.0, 20.0, 20.0);
				wrapPanel.Children.Add(wrapPanel2);
				WrapPanel wrapPanel3 = new WrapPanel();
				wrapPanel3.VerticalAlignment = VerticalAlignment.Center;
				if (this.BrandText_Width > 0)
				{
					wrapPanel3.Width = (double)this.BrandText_Width;
				}
				wrapPanel2.Children.Add(wrapPanel3);
				TextBlock textBlock = new TextBlock();
				textBlock.Text = text;
				textBlock.Style = style2;
				textBlock.Foreground = foreground;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.FontSize = (double)this.Fill_FontSize;
				wrapPanel3.Children.Add(textBlock);
				this.ChipTexts.Add(textBlock);
				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;
				wrapPanel2.Children.Add(stackPanel);
				Button button = new Button();
				button.Content = global::GClass0.smethod_0("#įȡ");
				button.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				button.Style = style;
				button.Tag = num;
				button.Click += this.method_3;
				button.FontSize = (double)this.Fill_FontSize;
				button.Height = (double)this.Fill_Height;
				stackPanel.Children.Add(button);
				TextBox textBox = new TextBox();
				textBox.Text = text2;
				textBox.Tag = num;
				textBox.ToolTip = global::GClass0.smethod_0("诽嘡跑鋋屭呠吂陎盛ग़") + text + global::GClass0.smethod_0("X瞀杳鋍㐃");
				textBox.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBox.FontSize = (double)this.Fill_FontSize;
				textBox.Width = this.Fill_Width;
				textBox.Height = (double)this.Fill_Height;
				textBox.MaxLength = this.Fill_Length;
				textBox.HorizontalAlignment = HorizontalAlignment.Right;
				textBox.GotFocus += this.method_12;
				textBox.LostFocus += this.method_11;
				textBox.KeyDown += this.method_5;
				textBox.TextChanged += this.method_7;
				stackPanel.Children.Add(textBox);
				this.ChipFills.Add(textBox);
				button = new Button();
				button.Content = global::GClass0.smethod_0("#ĩȡ");
				button.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				button.Style = style;
				button.Tag = num;
				button.Click += this.method_4;
				button.FontSize = (double)this.Fill_FontSize;
				button.Height = (double)this.Fill_Height;
				stackPanel.Children.Add(button);
				this.oQuestion.FillTexts.Add(global::GClass0.smethod_0(""));
				num++;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00058BE4 File Offset: 0x00056DE4
		private void method_3(object sender, RoutedEventArgs e)
		{
			int index = (int)((Button)sender).Tag;
			int num = this.method_17(this.ChipFills[index].Text);
			if (num > this.nMin)
			{
				num--;
				this.ChipFills[index].Text = num.ToString();
				this.nUseChips--;
				this.txtUsed.Text = this.nUseChips.ToString();
				if (this.nTotalChips < this.nMaxChips)
				{
					this.nTotalChips++;
					this.txtFill.Text = this.nTotalChips.ToString();
				}
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00058C98 File Offset: 0x00056E98
		private void method_4(object sender, RoutedEventArgs e)
		{
			int index = (int)((Button)sender).Tag;
			if (this.nTotalChips > 0)
			{
				int num = this.method_17(this.ChipFills[index].Text);
				if (num < this.nMax)
				{
					num++;
					this.ChipFills[index].Text = num.ToString();
					this.nUseChips++;
					this.txtUsed.Text = this.nUseChips.ToString();
				}
				this.nTotalChips--;
				this.txtFill.Text = this.nTotalChips.ToString();
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00058D48 File Offset: 0x00056F48
		private void method_5(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = (TextBox)sender;
				this.method_6(textBox);
				int num = (int)textBox.Tag;
				if (num < this.ChipFills.Count - 1)
				{
					this.ChipFills[num + 1].Focus();
					return;
				}
				this.ChipFills[0].Focus();
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00058DB0 File Offset: 0x00056FB0
		private void method_6(TextBox textBox_0)
		{
			if (textBox_0.Text == global::GClass0.smethod_0(""))
			{
				textBox_0.Text = global::GClass0.smethod_0("1");
			}
			this.nUseChips = 0;
			foreach (TextBox textBox in this.ChipFills)
			{
				this.nUseChips += this.method_17(textBox.Text);
			}
			this.nTotalChips = this.nMaxChips - this.nUseChips;
			this.txtUsed.Text = this.nUseChips.ToString();
			this.txtFill.Text = this.nTotalChips.ToString();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0005436C File Offset: 0x0005256C
		private void method_7(object sender, TextChangedEventArgs e)
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

		// Token: 0x060002DE RID: 734 RVA: 0x00058E84 File Offset: 0x00057084
		private bool method_8()
		{
			int num = 0;
			if (this.oQuestion.QCircleDefine.CONTROL_TYPE > 0)
			{
				num = this.oQuestion.QCircleDefine.CONTROL_TYPE;
			}
			else if (this.oQuestion.QCircleDefine.CONTROL_TYPE < 0)
			{
				num = -this.oQuestion.QCircleDefine.CONTROL_TYPE;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (TextBox textBox in this.ChipFills)
			{
				if (textBox.Text == global::GClass0.smethod_0(""))
				{
					textBox.Text = global::GClass0.smethod_0("1");
				}
				string text = textBox.Text;
				int num5 = this.method_17(text);
				if (this.oQuestion.QCircleDefine.CONTROL_TYPE != 0 && num5 > 0)
				{
					if (dictionary.ContainsKey(text))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string key = text;
						int num6 = dictionary2[key];
						dictionary2[key] = num6 + 1;
						if (dictionary[text] >= num)
						{
							if (this.oQuestion.QCircleDefine.CONTROL_TYPE > 0)
							{
								MessageBox.Show(string.Format(SurveyMsg.MsgNotOverOption, dictionary[text].ToString(), text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								textBox.Focus();
								return true;
							}
							if (this.oQuestion.QCircleDefine.CONTROL_TYPE < 0 && this.CheckSameChips)
							{
								if (MessageBox.Show(string.Format(SurveyMsg.MsgNotOverOptionWeak, dictionary[text], text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
								{
									textBox.Focus();
									return true;
								}
								this.CheckSameChips = false;
							}
						}
					}
					else
					{
						dictionary.Add(text, 1);
					}
				}
				if (num5 < this.nMin)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, this.nMin.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					textBox.Focus();
					return true;
				}
				if (num5 > this.nMax)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, this.nMax.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					textBox.Focus();
					return true;
				}
				if (num2 > 0 && this.oQuestion.QCircleDefine.CONTROL_MASK == global::GClass0.smethod_0("3"))
				{
					if (num4 < num5)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgNeedMore, this.ChipTexts[num2].Text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						textBox.Focus();
						return true;
					}
				}
				else if (num2 > 0 && this.oQuestion.QCircleDefine.CONTROL_MASK == global::GClass0.smethod_0("2") && this.CheckChipsLogic && num4 < num5)
				{
					if (MessageBox.Show(string.Format(SurveyMsg.MsgNeedMoreWeak, this.ChipTexts[num2].Text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
					{
						textBox.Focus();
						return true;
					}
					this.CheckChipsLogic = false;
				}
				num4 = num5;
				num3 += num5;
				this.oQuestion.FillTexts[num2] = text;
				num2++;
			}
			if (!(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("1")) && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("")) && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("/İ")) && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("/ĳ")) && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("/Ĳ")) && !(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP == global::GClass0.smethod_0("/ĵ")))
			{
				this.nUseChips = num3;
				this.nTotalChips = this.nMaxChips - this.nUseChips;
				this.txtUsed.Text = this.nUseChips.ToString();
				this.txtFill.Text = this.nTotalChips.ToString();
				if (this.nTotalChips > 0)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgNeedFinishNum, this.txtFill.Text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.ChipFills[0].Focus();
					return true;
				}
				if (this.nTotalChips < 0)
				{
					MessageBox.Show(SurveyMsg.MsgNeedFinishRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.ChipFills[0].Focus();
					return true;
				}
			}
			else if (num3 == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNotOptionZero, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.ChipFills[0].Focus();
				return true;
			}
			return false;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000593C8 File Offset: 0x000575C8
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
				string text2 = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ł") + this.oQuestion.QCircleDetails[num].CODE;
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
			Dictionary<string, string> dictionary2 = this.oFunc.SortDictSDbyValue(dictionary, true, false, global::GClass0.smethod_0("-"));
			num = 1;
			foreach (string key in dictionary2.Keys)
			{
				string text2 = this.oQuestion.QuestionName + global::GClass0.smethod_0("]œ") + num.ToString();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = dictionary2[key]
				});
				num++;
			}
			return list;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00059680 File Offset: 0x00057880
		private void method_10(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave(1);
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QCircleDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QCircleDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000596EC File Offset: 0x000578EC
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

		// Token: 0x060002E2 RID: 738 RVA: 0x000597E4 File Offset: 0x000579E4
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

		// Token: 0x060002E3 RID: 739 RVA: 0x0005984C File Offset: 0x00057A4C
		private void method_11(object sender, RoutedEventArgs e)
		{
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			int index = (int)((TextBox)sender).Tag;
			this.ChipTexts[index].Foreground = foreground;
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
			this.method_6((TextBox)sender);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000598C0 File Offset: 0x00057AC0
		private void method_12(object sender, RoutedEventArgs e)
		{
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			TextBox textBox = (TextBox)sender;
			int index = (int)textBox.Tag;
			this.ChipTexts[index].Foreground = foreground;
			if (textBox.Text == global::GClass0.smethod_0("1"))
			{
				textBox.Text = global::GClass0.smethod_0("");
			}
			else
			{
				textBox.SelectAll();
			}
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_14(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_16(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00059954 File Offset: 0x00057B54
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

		// Token: 0x060002EA RID: 746 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_18(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x0400057E RID: 1406
		private string MySurveyId;

		// Token: 0x0400057F RID: 1407
		private string CurPageId;

		// Token: 0x04000580 RID: 1408
		private NavBase MyNav = new NavBase();

		// Token: 0x04000581 RID: 1409
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000582 RID: 1410
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000583 RID: 1411
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000584 RID: 1412
		private UDPX oFunc = new UDPX();

		// Token: 0x04000585 RID: 1413
		private QMatrixFill oQuestion = new QMatrixFill();

		// Token: 0x04000586 RID: 1414
		private List<TextBlock> ChipTexts = new List<TextBlock>();

		// Token: 0x04000587 RID: 1415
		private List<TextBox> ChipFills = new List<TextBox>();

		// Token: 0x04000588 RID: 1416
		private int nTotalChips = 20;

		// Token: 0x04000589 RID: 1417
		private int nMaxChips = 20;

		// Token: 0x0400058A RID: 1418
		private int nUseChips;

		// Token: 0x0400058B RID: 1419
		private int nMin;

		// Token: 0x0400058C RID: 1420
		private int nMax = 20;

		// Token: 0x0400058D RID: 1421
		private bool CheckChipsLogic = true;

		// Token: 0x0400058E RID: 1422
		private bool CheckSameChips = true;

		// Token: 0x0400058F RID: 1423
		private bool PageLoaded;

		// Token: 0x04000590 RID: 1424
		private int BrandText_Width;

		// Token: 0x04000591 RID: 1425
		private int Fill_Height = 60;

		// Token: 0x04000592 RID: 1426
		private double Fill_Width = 80.0;

		// Token: 0x04000593 RID: 1427
		private int Fill_FontSize = 45;

		// Token: 0x04000594 RID: 1428
		private int Fill_Length = 2;

		// Token: 0x04000595 RID: 1429
		private int Button_Type;

		// Token: 0x04000596 RID: 1430
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000597 RID: 1431
		private int SecondsWait;

		// Token: 0x04000598 RID: 1432
		private int SecondsCountDown;

		// Token: 0x04000599 RID: 1433
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;
	}
}
