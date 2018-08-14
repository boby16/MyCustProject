using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x0200001F RID: 31
	public partial class MatrixSingleExt : Page
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x000356E8 File Offset: 0x000338E8
		public MatrixSingleExt()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000357C8 File Offset: 0x000339C8
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0, true);
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
			if (this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				string_ = ((list.Count > 1) ? list[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			}
			else
			{
				string_ = ((list.Count > 1) ? list[1] : global::GClass0.smethod_0(""));
			}
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			string text = this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim();
			if (text != global::GClass0.smethod_0(""))
			{
				this.CL_TA = this.method_8(text, 1);
				if (global::GClass0.smethod_0(":ĸȺ̴в԰زܴ࠺स").Contains(this.CL_TA))
				{
					this.CL_TA = global::GClass0.smethod_0("S");
					if (text != global::GClass0.smethod_0(""))
					{
						this.CL_Width = Convert.ToInt32(text);
					}
				}
				else
				{
					text = this.method_9(text, 1, -9999);
					this.CL_VA = this.method_8(text, 1);
					if (global::GClass0.smethod_0(":ĸȺ̴в԰زܴ࠺स").Contains(this.CL_VA))
					{
						if (this.CL_TA != global::GClass0.smethod_0("U") && this.CL_TA != global::GClass0.smethod_0("C"))
						{
							this.CL_VA = global::GClass0.smethod_0("B");
						}
						if (text != global::GClass0.smethod_0(""))
						{
							this.CL_Width = Convert.ToInt32(text);
						}
					}
					else if (this.method_9(text, 1, -9999) != global::GClass0.smethod_0(""))
					{
						this.CL_Width = Convert.ToInt32(this.method_9(text, 1, -9999));
					}
				}
				text = this.CL_TA + this.CL_VA;
				if (text.Contains(global::GClass0.smethod_0("M")))
				{
					this.CL_TA = global::GClass0.smethod_0("M");
				}
				else if (text.Contains(global::GClass0.smethod_0("S")))
				{
					this.CL_TA = global::GClass0.smethod_0("S");
				}
				if (text.Contains(global::GClass0.smethod_0("U")))
				{
					this.CL_VA = global::GClass0.smethod_0("U");
				}
				else if (text.Contains(global::GClass0.smethod_0("C")))
				{
					this.CL_VA = global::GClass0.smethod_0("C");
				}
				if (text.Contains(global::GClass0.smethod_0("B")) && (text.Contains(global::GClass0.smethod_0("U")) || text.Contains(global::GClass0.smethod_0("C"))))
				{
					this.CL_TA = global::GClass0.smethod_0("B");
				}
				if (text.Contains(global::GClass0.smethod_0("B")) && (text.Contains(global::GClass0.smethod_0("S")) || text.Contains(global::GClass0.smethod_0("M"))))
				{
					this.CL_VA = global::GClass0.smethod_0("B");
				}
				if (text == global::GClass0.smethod_0("B") || text == global::GClass0.smethod_0("Ał"))
				{
					this.CL_TA = global::GClass0.smethod_0("B");
					this.CL_VA = global::GClass0.smethod_0("B");
				}
			}
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list2 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list2.Add(surveyDetail);
							break;
						}
					}
				}
				list2.Sort(new Comparison<SurveyDetail>(MatrixSingleExt.Class17.instance.method_0));
				this.oQuestion.QDetails = list2;
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
				}
			}
			if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails(1);
			}
			if (this.oQuestion.QCircleDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
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
				list3.Sort(new Comparison<SurveyDetail>(MatrixSingleExt.Class17.instance.method_1));
				this.oQuestion.QCircleDetails = list3;
			}
			if (this.oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int l = 0; l < this.oQuestion.QCircleDetails.Count<SurveyDetail>(); l++)
				{
					this.oQuestion.QCircleDetails[l].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QCircleDetails[l].CODE_TEXT);
				}
			}
			if (this.oQuestion.QCircleDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails(2);
			}
			else if (this.oQuestion.QCircleDefine.SHOW_LOGIC != global::GClass0.smethod_0("") && this.oQuestion.QCircleDefine.IS_RANDOM == 0)
			{
				string show_LOGIC = this.oQuestion.QCircleDefine.SHOW_LOGIC;
				string[] array3 = this.oLogicEngine.aryCode(show_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int m = 0; m < array3.Count<string>(); m++)
				{
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail3.CODE == array3[m].ToString())
						{
							list4.Add(surveyDetail3);
							break;
						}
					}
				}
				this.oQuestion.QCircleDetails = list4;
			}
			this.Button_Height = SurveyHelper.BtnHeight;
			this.Button_FontSize = SurveyHelper.BtnFontSize;
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
			if (this.Button_Width < 2.0)
			{
				this.CR_Width = base.ActualWidth - 63.0;
				if (this.CL_Width > 0)
				{
					this.CR_Width -= (double)this.CL_Width;
				}
				if (this.Button_Width != 1.0 && this.oQuestion.QDetails.Count <= 7)
				{
					if (this.CL_Width == 0)
					{
						this.CL_Width = (int)(this.CR_Width / 16.0 * 4.0);
					}
					this.CR_Width = this.CR_Width / 16.0 * 10.0 - 8.0;
				}
				else
				{
					if (this.CL_Width == 0)
					{
						this.CL_Width = (int)(this.CR_Width / 14.0 * 4.0);
					}
					this.CR_Width = this.CR_Width / 14.0 * 10.0 - 8.0;
				}
				this.Button_Width = (this.CR_Width - (double)((this.oQuestion.QDetails.Count - 1) * 4) - 43.0) / (double)this.oQuestion.QDetails.Count;
				if (this.Button_Width < 20.0)
				{
					this.Button_Width = 20.0;
				}
			}
			this.method_2();
			foreach (SurveyDetail surveyDetail4 in this.oQuestion.QDetails)
			{
				if (surveyDetail4.IS_OTHER > 0)
				{
					this.listOther.Add(surveyDetail4.CODE);
				}
			}
			this.oBoldTitle.SetTextBlock(this.OptionTitleHeader, this.oQuestion.QCircleDefine.QUESTION_CONTENT, 0, global::GClass0.smethod_0(""), true);
			int num = this.listOther.Count<string>();
			int num2 = this.oQuestion.QDetails.Count<SurveyDetail>() - num;
			double value = 10.0 * (double)num / (double)num2;
			this.ExtColumn.Width = new GridLength(value, GridUnitType.Star);
			this.txt1.Text = global::GClass0.smethod_0("");
			this.txt3.Text = global::GClass0.smethod_0("");
			this.txt4.Text = global::GClass0.smethod_0("");
			this.txt5.Text = global::GClass0.smethod_0("");
			this.txt6.Text = global::GClass0.smethod_0("");
			this.txt7.Text = global::GClass0.smethod_0("");
			this.txt9.Text = global::GClass0.smethod_0("");
			string note = this.oQuestion.QDefine.NOTE;
			if (note == global::GClass0.smethod_0(""))
			{
				this.txt1.Height = 0.0;
				this.txt3.Height = 0.0;
				this.txt4.Height = 0.0;
				this.txt5.Height = 0.0;
				this.txt6.Height = 0.0;
				this.txt7.Height = 0.0;
				this.txt9.Height = 0.0;
			}
			else
			{
				list = new List<string>(note.Split(new string[]
				{
					global::GClass0.smethod_0("-Į")
				}, StringSplitOptions.RemoveEmptyEntries));
				int num3 = this.oBoldTitle.TakeFontSize(list[0]);
				list[0] = this.oBoldTitle.TakeText(list[0]);
				if (list.Count == 1)
				{
					this.txt5.Text = list[0];
				}
				else if (list.Count == 2)
				{
					this.txt1.Text = list[0];
					this.txt9.Text = list[1];
				}
				else if (list.Count == 3)
				{
					this.txt1.Text = list[0];
					this.txt5.Text = list[1];
					this.txt9.Text = list[2];
				}
				else if (list.Count == 4)
				{
					this.txt1.Text = list[0];
					this.txt4.Text = list[1];
					this.txt6.Text = list[2];
					this.txt9.Text = list[3];
				}
				else if (list.Count == 5)
				{
					this.txt1.Text = list[0];
					this.txt3.Text = list[1];
					this.txt5.Text = list[2];
					this.txt7.Text = list[3];
					this.txt9.Text = list[4];
				}
				if (num3 != 0)
				{
					this.txt1.FontSize = (double)num3;
					this.txt3.FontSize = (double)num3;
					this.txt4.FontSize = (double)num3;
					this.txt5.FontSize = (double)num3;
					this.txt6.FontSize = (double)num3;
					this.txt7.FontSize = (double)num3;
					this.txt9.FontSize = (double)num3;
				}
				else if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
				{
					this.txt1.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt3.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt4.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt5.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt6.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt7.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
					this.txt9.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				}
			}
			if (SurveyHelper.AutoFill && new AutoFill
			{
				oLogicEngine = this.oLogicEngine
			}.AutoNext(this.oQuestion.QDefine))
			{
				this.btnNav_Click(this, e);
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
				else if (this.oQuestion.QDetails.Count == 1 && this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2) && !SurveyHelper.AutoFill)
				{
					this.btnNav_Click(this, e);
				}
			}
			this.PageLoaded = true;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00036A80 File Offset: 0x00034C80
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
			{
				if (this.CL_Width > 0)
				{
					this.TitleLeft.Width = new GridLength((double)this.CL_Width);
					this.GridLeft.Width = new GridLength((double)this.CL_Width);
				}
				int num = (this.ScrollContent.ComputedVerticalScrollBarVisibility == Visibility.Collapsed) ? 3 : 43;
				this.GridTitle.Margin = new Thickness(0.0, 0.0, (double)num, 0.0);
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded = false;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00036B2C File Offset: 0x00034D2C
		private void method_2()
		{
			AutoFill autoFill = new AutoFill();
			autoFill.oLogicEngine = this.oLogicEngine;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Style style3 = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
			Brush borderBrush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
			if (this.CL_TA == global::GClass0.smethod_0("B"))
			{
				horizontalAlignment = HorizontalAlignment.Center;
			}
			else if (this.CL_TA == global::GClass0.smethod_0("M"))
			{
				horizontalAlignment = HorizontalAlignment.Left;
			}
			VerticalAlignment verticalAlignment = VerticalAlignment.Center;
			if (this.CL_VA == global::GClass0.smethod_0("U"))
			{
				verticalAlignment = VerticalAlignment.Top;
			}
			else if (this.CL_VA == global::GClass0.smethod_0("C"))
			{
				verticalAlignment = VerticalAlignment.Bottom;
			}
			Grid gridContent = this.GridContent;
			int num = 0;
			string text = this.method_8(this.oQuestion.QDefine.CONTROL_MASK, 1);
			if (text == global::GClass0.smethod_0("\""))
			{
				num = 1;
			}
			else if (text.ToUpper() == global::GClass0.smethod_0("F"))
			{
				num = 2;
			}
			else if (this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0("") && this.oQuestion.QDefine.CONTROL_MASK != null)
			{
				num = 0;
				this.iNoOfInterval = (int)Convert.ToInt16(this.oQuestion.QDefine.CONTROL_MASK.ToString());
				if (this.iNoOfInterval < 1)
				{
					this.iNoOfInterval = 9999;
				}
			}
			int num2 = 0;
			int num3 = 0;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
			{
				string code = surveyDetail.CODE;
				string code_TEXT = surveyDetail.CODE_TEXT;
				string text2 = global::GClass0.smethod_0("");
				if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
				{
					string string_ = this.oQuestion.QuestionName + global::GClass0.smethod_0("]œ") + code;
					text2 = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_);
				}
				gridContent.RowDefinitions.Add(new RowDefinition
				{
					Height = GridLength.Auto
				});
				Border border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
				bool flag = false;
				if (num == 1)
				{
					if (this.oQuestion.QDefine.CONTROL_MASK.Contains(global::GClass0.smethod_0("\"") + code + global::GClass0.smethod_0("\"")))
					{
						flag = true;
					}
				}
				else if (num > 1)
				{
					if (num3 == 0)
					{
						num3 = surveyDetail.RANDOM_SET;
					}
					else if (num3 != surveyDetail.RANDOM_SET)
					{
						if (num == 2)
						{
							num = 3;
							flag = true;
						}
						else
						{
							num = 2;
						}
						num3 = surveyDetail.RANDOM_SET;
					}
					else if (num == 3)
					{
						flag = true;
					}
				}
				else if (num2 / this.iNoOfInterval % 2 > 0)
				{
					flag = true;
				}
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.BackgroudColor));
				}
				border.SetValue(Grid.RowProperty, num2);
				border.SetValue(Grid.ColumnProperty, 0);
				gridContent.Children.Add(border);
				WrapPanel wrapPanel = new WrapPanel();
				wrapPanel.VerticalAlignment = verticalAlignment;
				wrapPanel.HorizontalAlignment = horizontalAlignment;
				border.Child = wrapPanel;
				TextBlock textBlock = new TextBlock();
				textBlock.Text = code_TEXT;
				textBlock.Style = style3;
				textBlock.Foreground = foreground;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
				textBlock.VerticalAlignment = verticalAlignment;
				if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
				{
					textBlock.FontSize = (double)this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
				}
				wrapPanel.Children.Add(textBlock);
				border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
				border.SetValue(Grid.RowProperty, num2);
				border.SetValue(Grid.ColumnProperty, 1);
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.BackgroudColor));
				}
				gridContent.Children.Add(border);
				WrapPanel wrapPanel2 = new WrapPanel();
				this.wrapSingle.Add(wrapPanel2);
				wrapPanel2.Orientation = Orientation.Horizontal;
				wrapPanel2.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel2.HorizontalAlignment = HorizontalAlignment.Center;
				wrapPanel2.Margin = new Thickness(2.0, 5.0, 2.0, 5.0);
				wrapPanel2.Name = global::GClass0.smethod_0("uœ") + code;
				wrapPanel2.Tag = code;
				border.Child = wrapPanel2;
				this.oQuestion.SelectedCode.Add(text2);
				this.listButton = new List<Button>();
				foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
				{
					Button button = new Button();
					button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail2.CODE;
					button.Content = surveyDetail2.CODE_TEXT;
					button.Margin = new Thickness(2.0, 0.0, 2.0, 0.0);
					button.Style = ((surveyDetail2.CODE == text2) ? style : style2);
					if (flag)
					{
						button.Opacity = 0.85;
					}
					button.Tag = num2;
					button.Click += this.method_3;
					button.FontSize = (double)this.Button_FontSize;
					button.MinWidth = this.Button_Width;
					button.MinHeight = (double)this.Button_Height;
					wrapPanel2.Children.Add(button);
					if (surveyDetail2.IS_OTHER == 2)
					{
						button.Visibility = Visibility.Hidden;
						if (surveyDetail2.EXTEND_2 != global::GClass0.smethod_0("") && this.oBoldTitle.ParaToList(surveyDetail2.EXTEND_2, global::GClass0.smethod_0("-Į")).Contains(code))
						{
							button.Visibility = Visibility.Visible;
						}
					}
					else if (surveyDetail2.IS_OTHER == 3)
					{
						if (surveyDetail2.EXTEND_3 != global::GClass0.smethod_0("") && this.oBoldTitle.ParaToList(surveyDetail2.EXTEND_3, global::GClass0.smethod_0("-Į")).Contains(code))
						{
							button.Visibility = Visibility.Hidden;
						}
					}
					else
					{
						this.listButton.Add(button);
					}
				}
				int num4 = 0;
				if ((!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == global::GClass0.smethod_0("2"))) && SurveyHelper.NavOperation != global::GClass0.smethod_0("FŢɡͪ"))
				{
					string extend_ = surveyDetail.EXTEND_4;
					if (extend_ != global::GClass0.smethod_0(""))
					{
						string[] array = this.oLogicEngine.aryCode(extend_, ',');
						for (int i = 0; i < array.Count<string>(); i++)
						{
							using (List<Button>.Enumerator enumerator3 = this.listButton.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									Button button2 = enumerator3.Current;
									if (button2.Name == global::GClass0.smethod_0("`Ş") + array[i])
									{
										num4 = 1;
										this.method_3(button2, new RoutedEventArgs());
										break;
									}
								}
								goto IL_923;
							}
							break;
							IL_923:;
						}
					}
				}
				if (num4 == 0 && this.oQuestion.QDetails.Count == 1 && !SurveyHelper.AutoFill && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
				{
					this.method_3(this.listButton[0], new RoutedEventArgs());
				}
				if (SurveyHelper.AutoFill)
				{
					Button button3;
					if (this.oQuestion.QDefine.CONTROL_TYPE == 0)
					{
						button3 = autoFill.SingleButton(this.oQuestion.QDefine, this.listButton);
					}
					else
					{
						if (this.AutoFillButton == -1)
						{
							this.AutoFillButton = Convert.ToInt32(this.oFunc.INT((double)(this.listButton.Count<Button>() / 2), 0, 0, 0));
						}
						button3 = this.listButton[this.AutoFillButton];
					}
					if (button3 != null && num4 == 0)
					{
						this.method_3(button3, new RoutedEventArgs());
					}
				}
				num2++;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000374C4 File Offset: 0x000356C4
		private void method_3(object sender, RoutedEventArgs e)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Button button = (Button)sender;
			int index = (int)button.Tag;
			string text = button.Name.Substring(2);
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				Panel panel = this.wrapSingle[index];
				this.oQuestion.SelectedCode[index] = text;
				foreach (object obj in panel.Children)
				{
					Button button2 = (Button)obj;
					string a = button2.Name.Substring(2);
					button2.Style = ((a == text) ? style : style2);
				}
				if (this.oQuestion.QDefine.MAX_COUNT > 0 && this.SameClickCheck && !SurveyHelper.AutoFill)
				{
					if (text == this.LastClickCode)
					{
						this.SameClickCount++;
						if (this.SameClickCount >= this.oQuestion.QDefine.MAX_COUNT && MessageBox.Show(string.Format(SurveyMsg.MsgMXSA_info3, this.SameClickCount.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.Yes).Equals(MessageBoxResult.No))
						{
							this.SameClickCheck = false;
							return;
						}
					}
					else
					{
						this.LastClickCode = text;
						this.SameClickCount = 1;
					}
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0003766C File Offset: 0x0003586C
		private bool method_4()
		{
			int num = 0;
			using (List<string>.Enumerator enumerator = this.oQuestion.SelectedCode.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == global::GClass0.smethod_0(""))
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, this.oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.wrapSingle[num].Focus();
						return true;
					}
					num++;
				}
			}
			if (this.oQuestion.QDefine.MIN_COUNT > 0 && !SurveyHelper.AutoFill)
			{
				int num2 = this.oQuestion.QDefine.MIN_COUNT;
				if (num2 > this.oQuestion.QCircleDetails.Count && this.oQuestion.QCircleDefine.MAX_COUNT == 1)
				{
					num2 = this.oQuestion.QCircleDetails.Count;
				}
				string text = global::GClass0.smethod_0("");
				int num3 = 0;
				num = 0;
				foreach (string text2 in this.oQuestion.SelectedCode)
				{
					if (text == text2)
					{
						num3++;
					}
					else
					{
						if (num3 >= num2 && !this.listOther.Contains(text))
						{
							num--;
							this.wrapSingle[num].Focus();
							string text3 = string.Format(SurveyMsg.MsgMXSA_info1, this.oQuestion.QCircleDetails[num].CODE_TEXT, num3);
							if (this.oQuestion.QCircleDefine.MIN_COUNT > 0)
							{
								MessageBox.Show(text3, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								return true;
							}
							text3 = text3 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
							if (MessageBox.Show(text3, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
							{
								return true;
							}
						}
						num3 = 1;
						text = text2;
					}
					num++;
				}
				if (num3 >= num2 && !this.listOther.Contains(text))
				{
					num--;
					this.wrapSingle[num].Focus();
					string text4 = string.Format(SurveyMsg.MsgMXSA_info1, this.oQuestion.QCircleDetails[num].CODE_TEXT, num3);
					if (this.oQuestion.QCircleDefine.MIN_COUNT > 0)
					{
						MessageBox.Show(text4, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					text4 = text4 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
					if (MessageBox.Show(text4, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
			}
			else if (this.oQuestion.QDefine.MIN_COUNT < 0)
			{
				int num4 = -this.oQuestion.QDefine.MIN_COUNT;
				if (num4 > this.oQuestion.QCircleDetails.Count && this.oQuestion.QCircleDefine.MAX_COUNT == 1)
				{
					num4 = this.oQuestion.QCircleDetails.Count;
				}
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				num = 0;
				foreach (string text5 in this.oQuestion.SelectedCode)
				{
					if (dictionary.ContainsKey(text5))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string key = text5;
						int num5 = dictionary2[key];
						dictionary2[key] = num5 + 1;
					}
					else
					{
						dictionary.Add(text5, 1);
					}
					num++;
				}
				foreach (string text6 in dictionary.Keys)
				{
					if (dictionary[text6] >= num4 && !this.listOther.Contains(text6))
					{
						string arg = global::GClass0.smethod_0("");
						using (List<SurveyDetail>.Enumerator enumerator3 = this.oQuestion.QDetails.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								SurveyDetail surveyDetail = enumerator3.Current;
								if (surveyDetail.CODE == text6)
								{
									arg = surveyDetail.CODE_TEXT;
									break;
								}
							}
							goto IL_4AC;
						}
						IL_468:
						string text7 = text7 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
						if (MessageBox.Show(text7, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
						{
							return true;
						}
						continue;
						IL_4AC:
						text7 = string.Format(SurveyMsg.MsgMXSA_info2, arg, dictionary[text6]);
						if (this.oQuestion.QCircleDefine.MIN_COUNT > 0)
						{
							MessageBox.Show(text7, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							return true;
						}
						goto IL_468;
					}
				}
			}
			if (this.oQuestion.QDefine.CONTROL_TYPE != 0)
			{
				string b = Math.Abs(this.oQuestion.QDefine.CONTROL_TYPE).ToString();
				int num6 = 0;
				int num7 = 9999999;
				int num8 = 0;
				int index = 0;
				num = 0;
				foreach (string text8 in this.oQuestion.SelectedCode)
				{
					if (!this.listOther.Contains(text8))
					{
						int num9 = this.oFunc.StringToInt(text8);
						if (this.oQuestion.QCircleDetails[num].CODE == b)
						{
							num6 = num9;
							index = num;
						}
						else
						{
							if (num8 < num9)
							{
								num8 = num9;
							}
							if (num7 > num9)
							{
								num7 = num9;
							}
						}
					}
					num++;
				}
				if (num6 < num7 && num6 > 0)
				{
					this.wrapSingle[index].Focus();
					if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgPointSmall, this.oQuestion.QCircleDetails[index].CODE_TEXT, num6.ToString(), num7.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					if (MessageBox.Show(string.Format(SurveyMsg.MsgPointSmall, this.oQuestion.QCircleDetails[index].CODE_TEXT, num6.ToString(), num7.ToString()) + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
				else if (num6 > num8)
				{
					this.wrapSingle[index].Focus();
					if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgPointBig, this.oQuestion.QCircleDetails[index].CODE_TEXT, num6.ToString(), num8.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					if (MessageBox.Show(string.Format(SurveyMsg.MsgPointBig, this.oQuestion.QCircleDetails[index].CODE_TEXT, num6.ToString(), num8.ToString()) + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00037EB4 File Offset: 0x000360B4
		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = global::GClass0.smethod_0("");
			int num = 0;
			foreach (string text in this.oQuestion.SelectedCode)
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
				text2 = this.oQuestion.CircleQuestionName + global::GClass0.smethod_0("]œ") + this.oQuestion.QCircleDetails[num].CODE;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text2,
					CODE = this.oQuestion.QCircleDetails[num].CODE
				});
				num++;
			}
			SurveyHelper.Answer = this.method_9(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00038040 File Offset: 0x00036240
		private void method_6(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000380AC File Offset: 0x000362AC
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			this.oPageNav.Refresh();
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

		// Token: 0x060001BA RID: 442 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x060001BB RID: 443 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x060001BD RID: 445 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x0400034C RID: 844
		private string MySurveyId;

		// Token: 0x0400034D RID: 845
		private string CurPageId;

		// Token: 0x0400034E RID: 846
		private NavBase MyNav = new NavBase();

		// Token: 0x0400034F RID: 847
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000350 RID: 848
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000351 RID: 849
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000352 RID: 850
		private UDPX oFunc = new UDPX();

		// Token: 0x04000353 RID: 851
		private QMatrixSingle oQuestion = new QMatrixSingle();

		// Token: 0x04000354 RID: 852
		private List<Button> listButton = new List<Button>();

		// Token: 0x04000355 RID: 853
		private int AutoFillButton = -1;

		// Token: 0x04000356 RID: 854
		private List<WrapPanel> wrapSingle = new List<WrapPanel>();

		// Token: 0x04000357 RID: 855
		private List<string> listOther = new List<string>();

		// Token: 0x04000358 RID: 856
		private string LastClickCode = global::GClass0.smethod_0("");

		// Token: 0x04000359 RID: 857
		private int SameClickCount;

		// Token: 0x0400035A RID: 858
		private bool SameClickCheck = true;

		// Token: 0x0400035B RID: 859
		private string BackgroudColor = global::GClass0.smethod_0("*Ļȴ̀уՂم݄ࡇ");

		// Token: 0x0400035C RID: 860
		private int iNoOfInterval = 9999;

		// Token: 0x0400035D RID: 861
		private string CL_TA = global::GClass0.smethod_0("S");

		// Token: 0x0400035E RID: 862
		private string CL_VA = global::GClass0.smethod_0("B");

		// Token: 0x0400035F RID: 863
		private int CL_Width;

		// Token: 0x04000360 RID: 864
		private double CR_Width;

		// Token: 0x04000361 RID: 865
		private bool PageLoaded;

		// Token: 0x04000362 RID: 866
		private int Button_Height;

		// Token: 0x04000363 RID: 867
		private double Button_Width;

		// Token: 0x04000364 RID: 868
		private int Button_FontSize;

		// Token: 0x04000365 RID: 869
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x0200008F RID: 143
		[CompilerGenerated]
		[Serializable]
		private sealed class Class17
		{
			// Token: 0x06000714 RID: 1812 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_1(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CD4 RID: 3284
			public static readonly MatrixSingleExt.Class17 instance = new MatrixSingleExt.Class17();

			// Token: 0x04000CD5 RID: 3285
			public static Comparison<SurveyDetail> compare0;

			// Token: 0x04000CD6 RID: 3286
			public static Comparison<SurveyDetail> compare1;
		}
	}
}
