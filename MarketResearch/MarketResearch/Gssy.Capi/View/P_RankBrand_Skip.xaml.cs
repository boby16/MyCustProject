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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x02000037 RID: 55
	public partial class P_RankBrand_Skip : Page
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0006DAE0 File Offset: 0x0006BCE0
		public P_RankBrand_Skip()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0006DB94 File Offset: 0x0006BD94
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0, false);
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
			string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
			List<string> list = new List<string>();
			list.Add(global::GClass0.smethod_0(""));
			if (show_LOGIC != global::GClass0.smethod_0(""))
			{
				list = this.oBoldTitle.ParaToList(show_LOGIC, global::GClass0.smethod_0("-Į"));
				if (list.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list3.Add(surveyDetail);
							break;
						}
					}
				}
				if (this.oQuestion.QDefine.SHOW_LOGIC == global::GClass0.smethod_0("") && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list3.Sort(new Comparison<SurveyDetail>(P_RankBrand_Skip.Class40.instance.method_0));
				}
				this.oQuestion.QDetails = list3;
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
				}
			}
			if (list[0].Trim() != global::GClass0.smethod_0(""))
			{
				string[] array2 = this.oLogicEngine.aryCode(list[0], ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count<string>(); k++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						if (surveyDetail2.CODE == array2[k].ToString())
						{
							list4.Add(surveyDetail2);
							break;
						}
					}
				}
				this.oQuestion.QDetails = list4;
			}
			else if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails(1);
			}
			if (this.oQuestion.QCircleDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array3 = this.oLogicEngine.aryCode(this.oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count<string>(); l++)
				{
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail3.CODE == array3[l].ToString())
						{
							list5.Add(surveyDetail3);
							break;
						}
					}
				}
				list5.Sort(new Comparison<SurveyDetail>(P_RankBrand_Skip.Class40.instance.method_1));
				this.oQuestion.QCircleDetails = list5;
			}
			if (this.oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int m = 0; m < this.oQuestion.QCircleDetails.Count<SurveyDetail>(); m++)
				{
					this.oQuestion.QCircleDetails[m].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QCircleDetails[m].CODE_TEXT);
				}
			}
			this.Button_Width = SurveyHelper.BtnWidth;
			this.Button_Height = SurveyHelper.BtnHeight;
			this.Button_FontSize = SurveyHelper.BtnFontSize;
			this.Text_Width = ((this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == global::GClass0.smethod_0("I")) ? 100 : SurveyHelper.BtnWidth);
			this.Text_Height = SurveyHelper.BtnHeight;
			this.Text_FontSize = SurveyHelper.BtnFontSize;
			if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				this.Button_Height = this.oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				this.Button_FontSize = this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_HEIGHT != 0)
			{
				this.Text_Height = this.oQuestion.QCircleDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_WIDTH != 0)
			{
				this.Text_Width = this.oQuestion.QCircleDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE != 0)
			{
				this.Text_FontSize = this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
			}
			this.method_2();
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				string_ = this.oQuestion.QDefine.NOTE;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore, string_, 0, global::GClass0.smethod_0(""), true);
				if (list2.Count > 1)
				{
					string_ = list2[1];
					this.oBoldTitle.SetTextBlock(this.txtAfter, string_, 0, global::GClass0.smethod_0(""), true);
				}
			}
			this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				this.listButton = autoFill.MultiButton(this.oQuestion.QDefine, this.listButton, this.listButton, this.btnOrders.Count);
				foreach (Button sender2 in this.listButton)
				{
					this.method_4(sender2, new RoutedEventArgs());
				}
				if (this.listButton.Count > 0 && autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
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

		// Token: 0x060003B4 RID: 948 RVA: 0x0006E790 File Offset: 0x0006C990
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				WrapPanel wrapPanel = this.wrapPanel2;
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
				if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
				{
					int num = this.method_13(this.oQuestion.QDefine.CONTROL_TOOLTIP);
					if (num == 1)
					{
						this.BrandArea.Width = GridLength.Auto;
					}
					else if (num > 1)
					{
						this.BrandArea.Width = new GridLength((double)num);
					}
				}
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded = false;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0006E880 File Offset: 0x0006CA80
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Style style3 = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
			Brush brush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
			Brush brush2 = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			Brush brush3 = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			WrapPanel wrapPanel = this.wrapPanel2;
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				string code = surveyDetail.CODE;
				string code_TEXT = surveyDetail.CODE_TEXT;
				if (code != this.NoneCode)
				{
					Button button = new Button();
					button.Content = code_TEXT;
					button.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
					button.Style = style2;
					button.Tag = code;
					button.Click += this.method_4;
					button.FontSize = (double)this.Button_FontSize;
					button.MinHeight = (double)this.Button_Height;
					button.MinWidth = (double)this.Button_Width;
					wrapPanel.Children.Add(button);
					this.btnBrands.Add(code, button);
					this.listButton.Add(button);
					num++;
				}
			}
			wrapPanel = this.wrapPanel1;
			bool flag = true;
			num = 0;
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleDetails)
			{
				string code2 = surveyDetail2.CODE;
				string code_TEXT2 = surveyDetail2.CODE_TEXT;
				string text = global::GClass0.smethod_0("");
				string content = global::GClass0.smethod_0("");
				if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
				{
					string string_ = this.oQuestion.QuestionName + global::GClass0.smethod_0("]œ") + code2;
					text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_);
					if (text == this.NoneCode)
					{
						text = global::GClass0.smethod_0("");
					}
					if (text != global::GClass0.smethod_0(""))
					{
						if (!this.btnBrands.ContainsKey(text))
						{
							text = global::GClass0.smethod_0("");
						}
						if (text != global::GClass0.smethod_0(""))
						{
							content = (string)this.btnBrands[text].Content;
						}
					}
				}
				WrapPanel wrapPanel2 = new WrapPanel();
				wrapPanel2.Orientation = ((this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == global::GClass0.smethod_0("I")) ? Orientation.Horizontal : Orientation.Vertical);
				wrapPanel2.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
				wrapPanel2.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel2.HorizontalAlignment = HorizontalAlignment.Center;
				wrapPanel.Children.Add(wrapPanel2);
				WrapPanel wrapPanel3 = new WrapPanel();
				wrapPanel3.VerticalAlignment = VerticalAlignment.Center;
				if (this.oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == global::GClass0.smethod_0("I"))
				{
					wrapPanel3.HorizontalAlignment = HorizontalAlignment.Right;
				}
				wrapPanel3.Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
				wrapPanel3.MinWidth = (double)this.Text_Width;
				wrapPanel2.Children.Add(wrapPanel3);
				TextBlock textBlock = new TextBlock();
				textBlock.Text = code_TEXT2;
				textBlock.Style = style3;
				textBlock.Foreground = ((text == global::GClass0.smethod_0("")) ? brush3 : brush2);
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.HorizontalAlignment = HorizontalAlignment.Right;
				wrapPanel3.Children.Add(textBlock);
				this.txtOrders.Add(textBlock);
				Button button2 = new Button();
				button2.Content = content;
				button2.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				button2.Style = style2;
				button2.Tag = num;
				button2.Click += this.method_3;
				button2.FontSize = (double)this.Text_FontSize;
				button2.MinHeight = (double)this.Text_Height;
				button2.MinWidth = (double)this.Button_Width;
				wrapPanel2.Children.Add(button2);
				this.btnOrders.Add(button2);
				this.oQuestion.SelectedCode.Add(text);
				if (text != global::GClass0.smethod_0(""))
				{
					this.btnBrands[text].Style = style;
					this.btnBrands[text].Visibility = Visibility.Collapsed;
				}
				if (flag)
				{
					this.txtFill.Text = code_TEXT2;
					this.nCurrent = num;
					button2.Style = style;
					this.txtOrders[num].Foreground = brush2;
					flag = false;
				}
				num++;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0006EE58 File Offset: 0x0006D058
		private void method_3(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			Brush foreground2 = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			Button button = (Button)sender;
			int index = (int)button.Tag;
			string text = (string)button.Content;
			this.nCurrent = index;
			this.txtFill.Text = this.oQuestion.QCircleDetails[this.nCurrent].CODE_TEXT;
			foreach (Button button2 in this.btnOrders)
			{
				button2.Style = style2;
			}
			button.Style = style;
			if (this.oQuestion.SelectedCode[index] != global::GClass0.smethod_0(""))
			{
				this.btnBrands[this.oQuestion.SelectedCode[index]].Style = style2;
				this.btnBrands[this.oQuestion.SelectedCode[index]].Visibility = Visibility.Visible;
				button.Content = global::GClass0.smethod_0("");
				this.oQuestion.SelectedCode[index] = global::GClass0.smethod_0("");
			}
			int num = 0;
			foreach (TextBlock textBlock in this.txtOrders)
			{
				if (this.oQuestion.SelectedCode[num] == global::GClass0.smethod_0(""))
				{
					textBlock.Foreground = foreground2;
				}
				else
				{
					textBlock.Foreground = foreground;
				}
				num++;
			}
			this.txtOrders[this.nCurrent].Foreground = foreground;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0006F08C File Offset: 0x0006D28C
		private void method_4(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			Brush brush = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			Button button = (Button)sender;
			if (button.Style == style)
			{
				return;
			}
			button.Style = style;
			button.Visibility = Visibility.Collapsed;
			if (this.oQuestion.SelectedCode[this.nCurrent] != global::GClass0.smethod_0(""))
			{
				this.btnBrands[this.oQuestion.SelectedCode[this.nCurrent]].Visibility = Visibility.Visible;
				this.btnBrands[this.oQuestion.SelectedCode[this.nCurrent]].Style = style2;
			}
			string value = (string)button.Tag;
			this.oQuestion.SelectedCode[this.nCurrent] = value;
			this.btnOrders[this.nCurrent].Content = button.Content;
			int num = 0;
			foreach (Button button2 in this.btnOrders)
			{
				if (this.oQuestion.SelectedCode[num] == global::GClass0.smethod_0(""))
				{
					this.btnOrders[this.nCurrent].Style = style2;
					this.txtFill.Text = this.oQuestion.QCircleDetails[num].CODE_TEXT;
					this.btnOrders[num].Style = style;
					this.btnOrders[num].Focus();
					this.txtOrders[num].Foreground = foreground;
					this.nCurrent = num;
					break;
				}
				num++;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0006F2B8 File Offset: 0x0006D4B8
		private void method_5(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Brush brush = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			int i = this.nCurrent;
			for (i = this.nCurrent; i < this.btnOrders.Count; i++)
			{
				if (this.oQuestion.SelectedCode[i] != global::GClass0.smethod_0(""))
				{
					this.btnBrands[this.oQuestion.SelectedCode[i]].Style = style2;
					this.btnBrands[this.oQuestion.SelectedCode[i]].Visibility = Visibility.Visible;
					this.btnOrders[i].Content = global::GClass0.smethod_0("");
					this.oQuestion.SelectedCode[i] = global::GClass0.smethod_0("");
				}
				if (i > this.nCurrent)
				{
					this.btnOrders[i].Style = style2;
					this.txtOrders[i].Foreground = foreground;
				}
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0006F414 File Offset: 0x0006D614
		private bool method_6()
		{
			base.UpdateLayout();
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			Brush foreground2 = (Brush)new BrushConverter().ConvertFromString(global::GClass0.smethod_0("RŬɪͶѤ"));
			this.oQuestion.SelectedCodeCount = 0;
			bool flag = false;
			for (int i = this.oQuestion.SelectedCode.Count - 1; i >= 0; i--)
			{
				int index = i;
				if (this.oQuestion.SelectedCode[i] != global::GClass0.smethod_0(""))
				{
					this.oQuestion.SelectedCodeCount++;
					flag = true;
				}
				else if (flag)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, this.oQuestion.QCircleDetails[index].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.nCurrent = index;
					this.txtFill.Text = this.oQuestion.QCircleDetails[this.nCurrent].CODE_TEXT;
					foreach (Button button in this.btnOrders)
					{
						button.Style = style2;
					}
					this.btnOrders[this.nCurrent].Style = style;
					this.btnOrders[this.nCurrent].Focus();
					int num = 0;
					foreach (TextBlock textBlock in this.txtOrders)
					{
						if (this.oQuestion.SelectedCode[num] == global::GClass0.smethod_0(""))
						{
							textBlock.Foreground = foreground2;
						}
						else
						{
							textBlock.Foreground = foreground;
						}
						num++;
					}
					this.txtOrders[this.nCurrent].Foreground = foreground;
					return true;
				}
			}
			if (!flag)
			{
				return MessageBox.Show(SurveyMsg.MsgRB_info1 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No);
			}
			return this.oQuestion.SelectedCodeCount < this.oQuestion.SelectedCode.Count && MessageBox.Show(SurveyMsg.MsgRB_info2 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0006F708 File Offset: 0x0006D908
		private List<VEAnswer> method_7()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			string str = (this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("@")) ? this.oQuestion.QDefine.GROUP_CODEA : this.oQuestion.QDefine.GROUP_CODEB;
			str += this.MyNav.QName_Add;
			if (this.oQuestion.SelectedCodeCount == 0)
			{
				string text = this.oQuestion.QuestionName + global::GClass0.smethod_0("\\ŐȰ");
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text,
					CODE = this.NoneCode
				});
				SurveyHelper.Answer = text + global::GClass0.smethod_0("<") + this.NoneCode;
				text = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ł") + this.NoneCode;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text,
					CODE = global::GClass0.smethod_0("0")
				});
				text = str + global::GClass0.smethod_0("\\ŐȰ");
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text,
					CODE = this.oQuestion.QCircleDetails[0].CODE
				});
			}
			else
			{
				SurveyHelper.Answer = global::GClass0.smethod_0("");
				int num = 0;
				foreach (string text2 in this.oQuestion.SelectedCode)
				{
					if (!(text2 != global::GClass0.smethod_0("")))
					{
						break;
					}
					string text3 = this.oQuestion.QuestionName + global::GClass0.smethod_0("]œ") + this.oQuestion.QCircleDetails[num].CODE;
					list.Add(new VEAnswer
					{
						QUESTION_NAME = text3,
						CODE = text2
					});
					SurveyHelper.Answer = string.Concat(new string[]
					{
						SurveyHelper.Answer,
						global::GClass0.smethod_0("-"),
						text3,
						global::GClass0.smethod_0("<"),
						text2
					});
					text3 = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ł") + text2;
					list.Add(new VEAnswer
					{
						QUESTION_NAME = text3,
						CODE = this.oQuestion.QCircleDetails[num].CODE
					});
					text3 = str + global::GClass0.smethod_0("]œ") + (num + 1).ToString();
					list.Add(new VEAnswer
					{
						QUESTION_NAME = text3,
						CODE = this.oQuestion.QCircleDetails[num].CODE
					});
					num++;
				}
				SurveyHelper.Answer = this.method_11(SurveyHelper.Answer, 1, -9999);
			}
			return list;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0006FA3C File Offset: 0x0006DC3C
		private void method_8(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSavebyCode(global::GClass0.smethod_0(";ĸ"));
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QCircleDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QCircleDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0006FAB0 File Offset: 0x0006DCB0
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_6())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_7();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_8(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0006FBA8 File Offset: 0x0006DDA8
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

		// Token: 0x060003BE RID: 958 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_9(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x060003BF RID: 959 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_11(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_12(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0006FC10 File Offset: 0x0006DE10
		private int method_13(string string_0)
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
			if (!this.method_14(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_14(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x0400070D RID: 1805
		private string NoneCode = global::GClass0.smethod_0(";ĸ");

		// Token: 0x0400070E RID: 1806
		private string MySurveyId;

		// Token: 0x0400070F RID: 1807
		private string CurPageId;

		// Token: 0x04000710 RID: 1808
		private NavBase MyNav = new NavBase();

		// Token: 0x04000711 RID: 1809
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000712 RID: 1810
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000713 RID: 1811
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000714 RID: 1812
		private QMatrixSingle oQuestion = new QMatrixSingle();

		// Token: 0x04000715 RID: 1813
		private int nCurrent;

		// Token: 0x04000716 RID: 1814
		private List<TextBlock> txtOrders = new List<TextBlock>();

		// Token: 0x04000717 RID: 1815
		private List<Button> btnOrders = new List<Button>();

		// Token: 0x04000718 RID: 1816
		private Dictionary<string, Button> btnBrands = new Dictionary<string, Button>();

		// Token: 0x04000719 RID: 1817
		private List<Button> listButton = new List<Button>();

		// Token: 0x0400071A RID: 1818
		private bool PageLoaded;

		// Token: 0x0400071B RID: 1819
		private int Button_Height;

		// Token: 0x0400071C RID: 1820
		private int Button_Width;

		// Token: 0x0400071D RID: 1821
		private int Button_FontSize = 40;

		// Token: 0x0400071E RID: 1822
		private int Text_Height;

		// Token: 0x0400071F RID: 1823
		private int Text_Width;

		// Token: 0x04000720 RID: 1824
		private int Text_FontSize = 40;

		// Token: 0x04000721 RID: 1825
		private int Button_Type;

		// Token: 0x04000722 RID: 1826
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000723 RID: 1827
		private int SecondsWait;

		// Token: 0x04000724 RID: 1828
		private int SecondsCountDown;

		// Token: 0x04000725 RID: 1829
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		[Serializable]
		private sealed class Class40
		{
			// Token: 0x0600075F RID: 1887 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_1(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D0A RID: 3338
			public static readonly P_RankBrand_Skip.Class40 instance = new P_RankBrand_Skip.Class40();

			// Token: 0x04000D0B RID: 3339
			public static Comparison<SurveyDetail> compare0;

			// Token: 0x04000D0C RID: 3340
			public static Comparison<SurveyDetail> compare1;
		}
	}
}
