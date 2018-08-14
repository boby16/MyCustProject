﻿using System;
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
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x02000017 RID: 23
	public partial class GridMultiple_LoopC_FixRow1 : Page
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00018D9C File Offset: 0x00016F9C
		public GridMultiple_LoopC_FixRow1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00018EDC File Offset: 0x000170DC
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
			show_LOGIC = this.oQuestion.QCircleDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add(global::GClass0.smethod_0(""));
			if (show_LOGIC != global::GClass0.smethod_0(""))
			{
				list2 = this.oBoldTitle.ParaToList(show_LOGIC, global::GClass0.smethod_0("-Į"));
				if (list2.Count > 1)
				{
					this.oQuestion.QCircleDefine.DETAIL_ID = this.oLogicEngine.Route(list2[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list3[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				string_ = ((list3.Count > 1) ? list3[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			}
			else
			{
				string_ = ((list3.Count > 1) ? list3[1] : global::GClass0.smethod_0(""));
			}
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			this.TR_Show = this.oBoldTitle.AlignmentPara(this.oQuestion.QCircleDefine.CONTROL_TOOLTIP.ToUpper().Trim(), ref this.TR_Label_HorizontalAlignment, ref this.TR_Label_VerticalAlignment);
			this.CL_Width = this.oFunc.StringToInt(this.oBoldTitle.AlignmentPara(this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim(), ref this.CL_Label_HorizontalAlignment, ref this.CL_Label_VerticalAlignment));
			bool bool_ = (this.oQuestion.QCircleDefine.CONTROL_TYPE == 0 && this.oQuestion.QDefine.PARENT_CODE == global::GClass0.smethod_0("")) || (this.oQuestion.QCircleDefine.CONTROL_TYPE == 1 && this.oQuestion.QCircleDefine.PARENT_CODE == global::GClass0.smethod_0("") && this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0(""));
			this.oBoldTitle.SetTextBlock(this.GridTopLeftText, this.oQuestion.QCircleDefine.QUESTION_CONTENT, this.oQuestion.QCircleDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), bool_);
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0("") && this.oQuestion.QCircleDefine.CONTROL_TYPE == 1)
			{
				list3 = new List<string>(this.oQuestion.QDefine.NOTE.Split(new string[]
				{
					global::GClass0.smethod_0("-Į")
				}, StringSplitOptions.RemoveEmptyEntries));
				int num = this.oBoldTitle.TakeFontSize(list3[0]);
				list3[0] = this.oBoldTitle.TakeText(list3[0]);
				Grid gridTopRight = this.GridTopRight;
				int num2 = 0;
				double num3 = (double)(list3.Count + 1) / 2.0 - 1.0;
				foreach (string text in list3)
				{
					gridTopRight.ColumnDefinitions.Add(new ColumnDefinition
					{
						Width = new GridLength(1.0, GridUnitType.Star)
					});
					TextBlock textBlock = new TextBlock();
					textBlock.SetValue(Grid.RowProperty, 0);
					textBlock.SetValue(Grid.ColumnProperty, num2);
					textBlock.Text = text;
					textBlock.Style = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
					textBlock.Foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
					textBlock.TextWrapping = TextWrapping.Wrap;
					textBlock.Margin = new Thickness(2.0, 0.0, 2.0, 5.0);
					HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center;
					if ((double)num2 < num3)
					{
						horizontalAlignment = HorizontalAlignment.Left;
					}
					else if ((double)num2 > num3)
					{
						horizontalAlignment = HorizontalAlignment.Right;
					}
					textBlock.HorizontalAlignment = horizontalAlignment;
					textBlock.VerticalAlignment = VerticalAlignment.Bottom;
					if (num > 0)
					{
						textBlock.FontSize = (double)num;
					}
					else if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
					{
						textBlock.FontSize = (double)this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
					}
					gridTopRight.Children.Add(textBlock);
					num2++;
				}
			}
			if (this.oQuestion.QCircleDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list4.Add(surveyDetail);
							break;
						}
					}
				}
				list4.Sort(new Comparison<SurveyDetail>(GridMultiple_LoopC_FixRow1.Class9.instance.method_0));
				this.oQuestion.QCircleDetails = list4;
			}
			if (this.oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int j = 0; j < this.oQuestion.QCircleDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QCircleDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QCircleDetails[j].CODE_TEXT);
				}
			}
			if (list2[0] != global::GClass0.smethod_0(""))
			{
				string string_2 = list2[0];
				string[] array2 = this.oLogicEngine.aryCode(string_2, ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count<string>(); k++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail2.CODE == array2[k].ToString())
						{
							list5.Add(surveyDetail2);
							break;
						}
					}
				}
				this.oQuestion.QCircleDetails = list5;
			}
			else if (this.oQuestion.QCircleDefine.IS_RANDOM == 1 || this.oQuestion.QCircleDefine.IS_RANDOM == 3 || (this.oQuestion.QCircleDefine.IS_RANDOM == 2 && this.oQuestion.QCircleDefine.PARENT_CODE != global::GClass0.smethod_0("")))
			{
				if (this.oQuestion.QCircleDefine.IS_RANDOM == 2 && this.oQuestion.QCircleDefine.PARENT_CODE != global::GClass0.smethod_0(""))
				{
					using (List<SurveyDetail>.Enumerator enumerator2 = this.oQuestion.QCircleDetails.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail surveyDetail3 = enumerator2.Current;
							surveyDetail3.RANDOM_FIX = 1;
						}
						goto IL_C0B;
					}
				}
				if (this.oQuestion.QCircleDefine.IS_RANDOM == 3 && this.oQuestion.QCircleDefine.PARENT_CODE != global::GClass0.smethod_0(""))
				{
					foreach (SurveyDetail surveyDetail4 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail4.RANDOM_SET > 0)
						{
							surveyDetail4.RANDOM_SET = -surveyDetail4.RANDOM_SET;
						}
					}
				}
				IL_C0B:
				this.oQuestion.RandomDetails(2);
			}
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array3 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list6 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count<string>(); l++)
				{
					foreach (SurveyDetail surveyDetail5 in this.oQuestion.QDetails)
					{
						if (surveyDetail5.CODE == array3[l].ToString())
						{
							list6.Add(surveyDetail5);
							break;
						}
					}
				}
				list6.Sort(new Comparison<SurveyDetail>(GridMultiple_LoopC_FixRow1.Class9.instance.method_1));
				this.oQuestion.QDetails = list6;
			}
			if (this.oQuestion.QDefine.FIX_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array4 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.FIX_LOGIC, ',');
				for (int m = 0; m < array4.Count<string>(); m++)
				{
					using (List<SurveyDetail>.Enumerator enumerator2 = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CODE == array4[m])
							{
								this.listFix.Add(array4[m]);
								break;
							}
						}
					}
				}
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array5 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int n = 0; n < array5.Count<string>(); n++)
				{
					using (List<SurveyDetail>.Enumerator enumerator2 = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CODE == array5[n])
							{
								this.listPreSet.Add(array5[n]);
								break;
							}
						}
					}
				}
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int num4 = 0; num4 < this.oQuestion.QDetails.Count<SurveyDetail>(); num4++)
				{
					this.oQuestion.QDetails[num4].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[num4].CODE_TEXT);
				}
			}
			else if (this.oQuestion.QDefine.SHOW_LOGIC != global::GClass0.smethod_0(""))
			{
				string string_3 = list[0];
				string[] array6 = this.oLogicEngine.aryCode(string_3, ',');
				List<SurveyDetail> list7 = new List<SurveyDetail>();
				for (int num5 = 0; num5 < array6.Count<string>(); num5++)
				{
					foreach (SurveyDetail surveyDetail6 in this.oQuestion.QDetails)
					{
						if (surveyDetail6.CODE == array6[num5].ToString())
						{
							list7.Add(surveyDetail6);
							break;
						}
					}
				}
				this.oQuestion.QDetails = list7;
			}
			else if (this.oQuestion.QDefine.IS_RANDOM == 1 || this.oQuestion.QDefine.IS_RANDOM == 3 || (this.oQuestion.QDefine.IS_RANDOM == 2 && this.oQuestion.QDefine.PARENT_CODE != global::GClass0.smethod_0("")))
			{
				if (this.oQuestion.QDefine.IS_RANDOM == 2 && this.oQuestion.QDefine.PARENT_CODE != global::GClass0.smethod_0(""))
				{
					using (List<SurveyDetail>.Enumerator enumerator2 = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							SurveyDetail surveyDetail7 = enumerator2.Current;
							surveyDetail7.RANDOM_FIX = 1;
						}
						goto IL_1108;
					}
				}
				if (this.oQuestion.QDefine.IS_RANDOM == 3 && this.oQuestion.QDefine.PARENT_CODE != global::GClass0.smethod_0(""))
				{
					foreach (SurveyDetail surveyDetail8 in this.oQuestion.QDetails)
					{
						if (surveyDetail8.RANDOM_SET > 0)
						{
							surveyDetail8.RANDOM_SET = -surveyDetail8.RANDOM_SET;
						}
					}
				}
				IL_1108:
				this.oQuestion.RandomDetails(1);
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
				double num6 = base.ActualWidth - 63.0;
				if (this.CL_Width > 0)
				{
					num6 -= (double)this.CL_Width;
				}
				if (this.Button_Width != 1.0 && this.oQuestion.QCircleDetails.Count <= 7)
				{
					if (this.CL_Width == 0 && (this.oQuestion.QCircleDefine.CONTROL_TYPE != 0 || !(this.oQuestion.QDefine.PARENT_CODE == global::GClass0.smethod_0(""))))
					{
						this.CL_Width = (int)(num6 / 16.0 * 4.0);
						num6 = num6 / 16.0 * 10.0 - 8.0;
					}
					else
					{
						num6 = num6 / 12.0 * 10.0 - 8.0;
					}
				}
				else if (this.CL_Width == 0 && (this.oQuestion.QCircleDefine.CONTROL_TYPE != 0 || !(this.oQuestion.QDefine.PARENT_CODE == global::GClass0.smethod_0(""))))
				{
					this.CL_Width = (int)(num6 / 14.0 * 4.0);
					num6 = num6 / 14.0 * 10.0 - 8.0;
				}
				this.Button_Width = (num6 - (double)((this.oQuestion.QCircleDetails.Count - 1) * 4) - 43.0) / (double)this.oQuestion.QCircleDetails.Count;
				if (this.Button_Width < 20.0)
				{
					this.Button_Width = 20.0;
				}
			}
			this.method_2();
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
			this.SecondsWait = this.oQuestion.QCircleDefine.PAGE_COUNT_DOWN;
			if (this.SecondsWait > 0)
			{
				this.SecondsCountDown = this.SecondsWait;
				this.btnNav.Foreground = Brushes.LightGray;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
			this.PageLoaded = 1;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0001A4E0 File Offset: 0x000186E0
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded == 2)
			{
				if (this.CL_Width > 0)
				{
					this.GridTopLeft.Width = (double)this.CL_Width;
					this.GridBottomLeft.Width = (double)this.CL_Width;
				}
				else
				{
					this.GridTopLeft.Width = this.GridBottomLeft.ActualWidth;
				}
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded++;
			}
			if (this.PageLoaded == 1)
			{
				if (this.GridTopLeft.ActualHeight > 0.0 || this.GridTopRight.ActualHeight > 0.0)
				{
					double num = (this.GridTopLeft.ActualHeight > this.GridTopRight.ActualHeight) ? this.GridTopLeft.ActualHeight : this.GridTopRight.ActualHeight;
					if ((double)this.oQuestion.QCircleDefine.CONTROL_HEIGHT > num)
					{
						num = (double)this.oQuestion.QCircleDefine.CONTROL_HEIGHT;
					}
					this.GridTopLeft.Height = num;
					this.GridTopRight.Height = num;
				}
				if (this.is_TR_Show)
				{
					Grid gridBottomRight = this.GridBottomRight;
					Grid gridTopRight = this.GridTopRight;
					int num2 = 0;
					foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
					{
						gridTopRight.ColumnDefinitions[num2].Width = new GridLength(gridBottomRight.ColumnDefinitions[num2].ActualWidth);
						num2++;
					}
				}
				bool flag = this.oQuestion.QCircleDefine.CONTROL_TYPE == 0 && this.oQuestion.QDefine.PARENT_CODE != global::GClass0.smethod_0("");
				if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 1 || this.oQuestion.QCircleDefine.CONTROL_TYPE == 2 || flag)
				{
					Brush borderBrush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
					Grid gridBottomRight2 = this.GridBottomRight;
					Grid gridBottomLeft = this.GridBottomLeft;
					int num3 = 0;
					Border border = new Border();
					string b = global::GClass0.smethod_0("");
					int num4 = 1;
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						string text = global::GClass0.smethod_0("");
						if (flag && surveyDetail2.PARENT_CODE != b)
						{
							using (List<SurveyDetail>.Enumerator enumerator2 = this.oQuestion.QGroupDetails.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									SurveyDetail surveyDetail3 = enumerator2.Current;
									if (surveyDetail3.CODE == surveyDetail2.PARENT_CODE)
									{
										text = surveyDetail3.CODE_TEXT;
										break;
									}
								}
								goto IL_462;
							}
							goto IL_2E7;
						}
						goto IL_462;
						IL_44B:
						if (flag)
						{
							b = surveyDetail2.PARENT_CODE;
						}
						num3++;
						continue;
						IL_2E7:
						if (surveyDetail2.PARENT_CODE == b)
						{
							num4++;
							border.SetValue(Grid.RowSpanProperty, num4);
							goto IL_44B;
						}
						IL_315:
						border = new Border();
						border.BorderThickness = new Thickness(1.0);
						border.BorderBrush = borderBrush;
						border.SetValue(Grid.RowProperty, num3);
						border.SetValue(Grid.ColumnProperty, 0);
						num4 = 1;
						gridBottomLeft.Children.Add(border);
						TextBlock textBlock = new TextBlock();
						border.Child = textBlock;
						textBlock.Text = (flag ? text : surveyDetail2.CODE_TEXT);
						textBlock.Style = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
						textBlock.Foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
						textBlock.TextWrapping = TextWrapping.Wrap;
						textBlock.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
						textBlock.HorizontalAlignment = this.CL_Label_HorizontalAlignment;
						textBlock.VerticalAlignment = this.CL_Label_VerticalAlignment;
						if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
						{
							textBlock.FontSize = (double)this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
							goto IL_44B;
						}
						goto IL_44B;
						IL_462:
						Button button = this.matixButton.Attributes[0].Buttons[num3];
						double actualHeight = gridBottomRight2.RowDefinitions[num3].ActualHeight;
						gridBottomLeft.RowDefinitions.Add(new RowDefinition
						{
							Height = new GridLength(actualHeight)
						});
						if (flag)
						{
							goto IL_2E7;
						}
						goto IL_315;
					}
				}
				this.GridTopRight.Width = this.GridBottomRight.ActualWidth;
				string a = this.oQuestion.QCircleDefine.CONTROL_MASK.Trim().ToUpper();
				if (a != global::GClass0.smethod_0("W") && a != global::GClass0.smethod_0("I"))
				{
					a = global::GClass0.smethod_0("W");
				}
				if (a == global::GClass0.smethod_0("W"))
				{
					if (this.ScrollVertical.ComputedVerticalScrollBarVisibility != Visibility.Collapsed)
					{
						this.ScrollVertical.PanningMode = PanningMode.VerticalOnly;
					}
					else
					{
						this.ScrollHorizontal.PanningMode = PanningMode.HorizontalOnly;
					}
				}
				else if (a == global::GClass0.smethod_0("I"))
				{
					if (this.ScrollHorizontal.ComputedHorizontalScrollBarVisibility != Visibility.Collapsed)
					{
						this.ScrollHorizontal.PanningMode = PanningMode.HorizontalOnly;
					}
					else
					{
						this.ScrollVertical.PanningMode = PanningMode.VerticalOnly;
					}
				}
				this.PageLoaded++;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0001AAEC File Offset: 0x00018CEC
		private void method_2()
		{
			AutoFill autoFill = new AutoFill();
			autoFill.oLogicEngine = this.oLogicEngine;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Style style3 = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
			Brush borderBrush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			if (this.listFix.Count > 0)
			{
				foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
				{
					if (this.listFix.Contains(surveyDetail.CODE))
					{
						if (!this.IsFixNone && this.CodeOfQnNone.Contains(global::GClass0.smethod_0(".") + surveyDetail.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
						{
							this.IsFixNone = true;
						}
						if (this.CodeOfGroupNone.Contains(global::GClass0.smethod_0(".") + surveyDetail.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
						{
							string str = (surveyDetail.PARENT_CODE == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0(";ĸ") : surveyDetail.PARENT_CODE;
							this.GroupOfFixNone = this.GroupOfFixNone + str + global::GClass0.smethod_0(".");
						}
						if (!this.CodeOfNone.Contains(global::GClass0.smethod_0(".") + surveyDetail.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
						{
							string str2 = (surveyDetail.PARENT_CODE == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0(";ĸ") : surveyDetail.PARENT_CODE;
							this.GroupOfFixNormal = this.GroupOfFixNormal + str2 + global::GClass0.smethod_0(".");
						}
					}
				}
			}
			Grid gridBottomRight = this.GridBottomRight;
			Grid gridTopRight = this.GridTopRight;
			int num = 0;
			bool flag = false;
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
			Border border = new Border();
			Border border2 = new Border();
			int num2 = 0;
			int num3 = 0;
			string b = global::GClass0.smethod_0("");
			int num4 = 1;
			foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleDetails)
			{
				string code = surveyDetail2.CODE;
				string code_TEXT = surveyDetail2.CODE_TEXT;
				classMultipleAnswers classMultipleAnswers = new classMultipleAnswers();
				if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
				{
					string string_ = this.oQuestion.QuestionName + global::GClass0.smethod_0("]œ") + code;
					foreach (SurveyAnswer surveyAnswer in this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, string_, SurveyHelper.SurveySequence))
					{
						classMultipleAnswers.Answers.Add(surveyAnswer.CODE);
					}
				}
				gridTopRight.ColumnDefinitions.Add(new ColumnDefinition
				{
					Width = GridLength.Auto
				});
				flag = false;
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
						num3 = surveyDetail2.RANDOM_SET;
					}
					else if (num3 != surveyDetail2.RANDOM_SET)
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
						num3 = surveyDetail2.RANDOM_SET;
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
				bool flag2 = this.oQuestion.QCircleDefine.CONTROL_TYPE == 1 && this.oQuestion.QCircleDefine.PARENT_CODE != global::GClass0.smethod_0("") && this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0("");
				if (this.oQuestion.QCircleDefine.CONTROL_TYPE != 1 || flag2)
				{
					this.is_TR_Show = true;
					string text2 = global::GClass0.smethod_0("");
					if (flag2 && surveyDetail2.PARENT_CODE != b)
					{
						using (List<SurveyDetail>.Enumerator enumerator3 = this.oQuestion.QCircleGroupDetails.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								SurveyDetail surveyDetail3 = enumerator3.Current;
								if (surveyDetail3.CODE == surveyDetail2.PARENT_CODE)
								{
									text2 = surveyDetail3.CODE_TEXT;
									break;
								}
							}
							goto IL_12FC;
						}
						goto IL_59C;
					}
					goto IL_12FC;
					IL_734:
					b = surveyDetail2.PARENT_CODE;
					goto IL_73D;
					IL_5CA:
					border2 = new Border();
					border2.BorderThickness = new Thickness((double)((this.TR_Show == global::GClass0.smethod_0("1")) ? 0 : 1));
					border2.BorderBrush = borderBrush;
					border2.SetValue(Grid.RowProperty, 0);
					border2.SetValue(Grid.ColumnProperty, num2);
					num4 = 1;
					gridTopRight.Children.Add(border2);
					if (flag2)
					{
						if (num > 1 && flag)
						{
							border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.BackgroudColor));
						}
					}
					else if (flag)
					{
						border2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.BackgroudColor));
					}
					TextBlock textBlock = new TextBlock();
					textBlock.Text = (flag2 ? text2 : code_TEXT);
					textBlock.Style = style3;
					textBlock.Foreground = foreground;
					textBlock.TextWrapping = TextWrapping.Wrap;
					textBlock.Margin = new Thickness(2.0, 5.0, 2.0, 5.0);
					textBlock.HorizontalAlignment = this.TR_Label_HorizontalAlignment;
					textBlock.VerticalAlignment = this.TR_Label_VerticalAlignment;
					if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
					{
						textBlock.FontSize = (double)this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
					}
					border2.Child = textBlock;
					goto IL_734;
					IL_12FC:
					if (!flag2)
					{
						goto IL_5CA;
					}
					IL_59C:
					if (surveyDetail2.PARENT_CODE == b)
					{
						num4++;
						border2.SetValue(Grid.ColumnSpanProperty, num4);
						goto IL_734;
					}
					goto IL_5CA;
				}
				IL_73D:
				gridBottomRight.ColumnDefinitions.Add(new ColumnDefinition
				{
					Width = GridLength.Auto
				});
				if (num2 == 0)
				{
					foreach (SurveyDetail surveyDetail4 in this.oQuestion.QDetails)
					{
						gridBottomRight.RowDefinitions.Add(new RowDefinition
						{
							Height = GridLength.Auto
						});
					}
				}
				border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
				border.SetValue(Grid.RowProperty, 0);
				border.SetValue(Grid.ColumnProperty, num2);
				border.SetValue(Grid.RowSpanProperty, this.oQuestion.QDetails.Count);
				gridBottomRight.Children.Add(border);
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.BackgroudColor));
				}
				string[] string_2 = this.oLogicEngine.aryCode(surveyDetail2.EXTEND_4, ',');
				List<string> list = new List<string>(this.listFix);
				bool flag3 = this.IsFixNone;
				string text3 = this.GroupOfFixNone;
				string text4 = this.GroupOfFixNormal;
				if (surveyDetail2.EXTEND_5 != global::GClass0.smethod_0(""))
				{
					string[] array = this.oLogicEngine.aryCode(surveyDetail2.EXTEND_5, ',');
					for (int i = 0; i < array.Count<string>(); i++)
					{
						using (List<SurveyDetail>.Enumerator enumerator3 = this.oQuestion.QDetails.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								if (enumerator3.Current.CODE == array[i])
								{
									list.Add(array[i]);
									break;
								}
							}
						}
					}
					if (list.Count<string>() > 0)
					{
						foreach (SurveyDetail surveyDetail5 in this.oQuestion.QDetails)
						{
							if (list.Contains(surveyDetail5.CODE))
							{
								if (!flag3 && this.CodeOfQnNone.Contains(global::GClass0.smethod_0(".") + surveyDetail5.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
								{
									flag3 = true;
								}
								if (this.CodeOfGroupNone.Contains(global::GClass0.smethod_0(".") + surveyDetail5.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
								{
									string str3 = (surveyDetail5.PARENT_CODE == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0(";ĸ") : surveyDetail5.PARENT_CODE;
									text3 = text3 + str3 + global::GClass0.smethod_0(".");
								}
								if (!this.CodeOfNone.Contains(global::GClass0.smethod_0(".") + surveyDetail5.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
								{
									string str4 = (surveyDetail5.PARENT_CODE == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0(";ĸ") : surveyDetail5.PARENT_CODE;
									text4 = text4 + str4 + global::GClass0.smethod_0(".");
								}
							}
						}
					}
				}
				this.listButton = new List<Button>();
				classListButton classListButton = new classListButton();
				this.matixButton.Attributes.Add(classListButton);
				int num5 = 0;
				foreach (SurveyDetail surveyDetail6 in this.oQuestion.QDetails)
				{
					string text5 = (surveyDetail6.PARENT_CODE == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0(";ĸ") : surveyDetail6.PARENT_CODE;
					WrapPanel wrapPanel = new WrapPanel();
					wrapPanel.VerticalAlignment = VerticalAlignment.Center;
					wrapPanel.HorizontalAlignment = HorizontalAlignment.Center;
					if (num5 == 0)
					{
						wrapPanel.Margin = new Thickness(5.0, 5.0, 5.0, 2.0);
					}
					else if (num5 == this.oQuestion.QDetails.Count - 1)
					{
						wrapPanel.Margin = new Thickness(5.0, 2.0, 5.0, 5.0);
					}
					else
					{
						wrapPanel.Margin = new Thickness(5.0, 2.0, 5.0, 2.0);
					}
					wrapPanel.SetValue(Grid.RowProperty, num5);
					wrapPanel.SetValue(Grid.ColumnProperty, num2);
					gridBottomRight.Children.Add(wrapPanel);
					Button button = new Button();
					button.Name = global::GClass0.smethod_0("`Ş") + num2.ToString() + global::GClass0.smethod_0("^") + surveyDetail6.CODE;
					if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 0)
					{
						button.Content = surveyDetail6.CODE_TEXT;
					}
					else if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 1)
					{
						button.Content = surveyDetail2.CODE_TEXT;
					}
					else if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 2)
					{
						button.Content = surveyDetail6.EXTEND_1;
					}
					button.Margin = new Thickness(0.0);
					button.Style = (classMultipleAnswers.Answers.Contains(surveyDetail6.CODE) ? style : style2);
					if (flag)
					{
						button.Opacity = 0.85;
					}
					button.Tag = (surveyDetail6.IS_OTHER + global::GClass0.smethod_0("!")).Substring(0, 2) + global::GClass0.smethod_0("F") + text5;
					button.FontSize = (double)this.Button_FontSize;
					button.MinWidth = this.Button_Width;
					button.MinHeight = (double)this.Button_Height;
					wrapPanel.Children.Add(button);
					this.matixButton.Attributes[num2].Buttons.Add(button);
					if (surveyDetail2.EXTEND_4 != global::GClass0.smethod_0("") && this.oFunc.StringInArray(surveyDetail6.CODE, string_2, true) == global::GClass0.smethod_0(""))
					{
						wrapPanel.Visibility = Visibility.Collapsed;
					}
					if (wrapPanel.Visibility == Visibility.Visible)
					{
						if (surveyDetail6.EXTEND_2 != global::GClass0.smethod_0(""))
						{
							wrapPanel.Visibility = Visibility.Collapsed;
							if (this.oBoldTitle.ParaToList(surveyDetail6.EXTEND_2, global::GClass0.smethod_0("-Į")).Contains(code))
							{
								wrapPanel.Visibility = Visibility.Visible;
							}
						}
						if (surveyDetail6.EXTEND_3 != global::GClass0.smethod_0("") && this.oBoldTitle.ParaToList(surveyDetail6.EXTEND_3, global::GClass0.smethod_0("-Į")).Contains(code))
						{
							wrapPanel.Visibility = Visibility.Collapsed;
						}
					}
					bool flag4 = false;
					if (wrapPanel.Visibility == Visibility.Visible && list.Count > 0)
					{
						if (list.Contains(surveyDetail6.CODE))
						{
							flag4 = true;
						}
						else if (!flag3 && !text3.Contains(global::GClass0.smethod_0(".") + text5 + global::GClass0.smethod_0(".")))
						{
							if (!list.Contains(surveyDetail6.CODE) && this.CodeOfQnNone.Contains(global::GClass0.smethod_0(".") + surveyDetail6.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
							{
								wrapPanel.Visibility = Visibility.Collapsed;
							}
							else if (text4.Contains(global::GClass0.smethod_0(".") + text5 + global::GClass0.smethod_0(".")) && this.CodeOfGroupNone.Contains(global::GClass0.smethod_0(".") + surveyDetail6.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
							{
								wrapPanel.Visibility = Visibility.Collapsed;
							}
						}
						else
						{
							wrapPanel.Visibility = Visibility.Collapsed;
						}
					}
					if (wrapPanel.Visibility == Visibility.Visible)
					{
						if (flag4)
						{
							button.Style = style;
							button.Opacity = 0.5;
							classMultipleAnswers.Answers.Add(surveyDetail6.CODE);
						}
						else
						{
							button.Click += this.method_3;
							this.listAllButton.Add(button);
							if (!this.CodeOfNone.Contains(global::GClass0.smethod_0(".") + surveyDetail6.IS_OTHER.ToString() + global::GClass0.smethod_0(".")))
							{
								this.listButton.Add(button);
							}
						}
					}
					num5++;
				}
				this.oQuestion.SelectedCodes.Add(classMultipleAnswers);
				int num6 = 0;
				if ((!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == global::GClass0.smethod_0("2"))) && SurveyHelper.NavOperation != global::GClass0.smethod_0("FŢɡͪ"))
				{
					string extend_ = surveyDetail2.EXTEND_6;
					if (extend_ != global::GClass0.smethod_0(""))
					{
						string[] array2 = this.oLogicEngine.aryCode(extend_, ',');
						for (int j = 0; j < array2.Count<string>(); j++)
						{
							foreach (Button button2 in classListButton.Buttons)
							{
								if (button2.Name == global::GClass0.smethod_0("`Ş") + num2.ToString() + global::GClass0.smethod_0("^") + array2[j])
								{
									num6++;
									this.method_3(button2, new RoutedEventArgs());
									break;
								}
							}
						}
					}
					foreach (string str5 in this.listPreSet)
					{
						foreach (Button button3 in this.listButton)
						{
							if (button3.Name == global::GClass0.smethod_0("`Ş") + num2.ToString() + global::GClass0.smethod_0("^") + str5)
							{
								num6++;
								this.method_3(button3, new RoutedEventArgs());
								break;
							}
						}
					}
				}
				if (num6 == 0 && this.oQuestion.QDetails.Count == 1 && !SurveyHelper.AutoFill && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
				{
					this.method_3(this.listButton[0], new RoutedEventArgs());
				}
				if (SurveyHelper.AutoFill)
				{
					this.listButton = autoFill.MultiButton(this.oQuestion.QDefine, this.listButton, this.listAllButton, 0);
					foreach (Button button4 in this.listButton)
					{
						if (button4.Style == style2)
						{
							this.method_3(button4, null);
						}
					}
				}
				num2++;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0001BF38 File Offset: 0x0001A138
		private void method_3(object sender, RoutedEventArgs e = null)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Button button = (Button)sender;
			string text = button.Name.Substring(2);
			List<string> list = this.oFunc.StringToList(text, global::GClass0.smethod_0("^"));
			text = list[1];
			int index = this.oFunc.StringToInt(list[0]);
			string text2 = (string)button.Tag;
			string a = text2.Substring(3);
			text2 = global::GClass0.smethod_0(".") + text2.Substring(0, 2).Trim() + global::GClass0.smethod_0(".");
			int num = 0;
			if (this.CodeOfQnNone.Contains(text2))
			{
				num = 1;
			}
			else if (this.CodeOfGroupNone.Contains(text2))
			{
				num = 2;
			}
			int num2 = (button.Style == style) ? 1 : 0;
			int num3 = 0;
			if (num2 == 0)
			{
				if (num == 1)
				{
					this.oQuestion.SelectedCodes[index].Answers.Clear();
					num3 = 1;
				}
				else if (num == 2)
				{
					num3 = 3;
				}
				else
				{
					num3 = 2;
				}
				this.oQuestion.SelectedCodes[index].Answers.Add(text);
				button.Style = style;
			}
			else if (num == 1)
			{
				num2 = 2;
			}
			else
			{
				this.oQuestion.SelectedCodes[index].Answers.Remove(text);
				button.Style = style2;
				num2 = 2;
			}
			if (num2 < 2)
			{
				bool flag = true;
				bool flag2 = true;
				foreach (Button button2 in this.matixButton.Attributes[index].Buttons)
				{
					string text3 = button2.Name.Substring(2);
					text3 = this.oFunc.StringToList(text3, global::GClass0.smethod_0("^"))[1];
					string text4 = (string)button2.Tag;
					string b = text4.Substring(3);
					text4 = global::GClass0.smethod_0(".") + text4.Substring(0, 2).Trim() + global::GClass0.smethod_0(".");
					if (button2.Opacity != 0.5 && !(text3 == text))
					{
						if (num3 == 1)
						{
							button2.Style = style2;
						}
						else if (num3 == 2 || num3 == 3)
						{
							if (flag && this.CodeOfQnNone.Contains(text4) && button2.Style == style)
							{
								button2.Style = style2;
								this.oQuestion.SelectedCodes[index].Answers.Remove(text3);
								flag = false;
							}
							if (flag2 && a == b && this.CodeOfGroupNone.Contains(text4) && button2.Style == style)
							{
								button2.Style = style2;
								this.oQuestion.SelectedCodes[index].Answers.Remove(text3);
								flag2 = false;
							}
						}
						if (num3 == 3 && a == b)
						{
							button2.Style = style2;
							this.oQuestion.SelectedCodes[index].Answers.Remove(text3);
						}
					}
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		private bool method_4()
		{
			int num = 0;
			foreach (classMultipleAnswers classMultipleAnswers in this.oQuestion.SelectedCodes)
			{
				if (classMultipleAnswers.Answers.Count == 0 && this.GridBottomRight.ColumnDefinitions[num].ActualWidth > 3.0)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, this.oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.matixButton.Attributes[num].Buttons[0].Focus();
					this.matixButton.Attributes[num].Buttons[0].BringIntoView();
					return true;
				}
				if (this.oQuestion.QDefine.MIN_COUNT != 0 && this.GridBottomRight.ColumnDefinitions[num].ActualWidth > 3.0 && classMultipleAnswers.Answers.Count < this.oQuestion.QDefine.MIN_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgMAless, this.oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.matixButton.Attributes[num].Buttons[0].Focus();
					this.matixButton.Attributes[num].Buttons[0].BringIntoView();
					return true;
				}
				if (this.oQuestion.QDefine.MAX_COUNT != 0 && classMultipleAnswers.Answers.Count > this.oQuestion.QDefine.MAX_COUNT)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgMAmore, this.oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.matixButton.Attributes[num].Buttons[0].Focus();
					this.matixButton.Attributes[num].Buttons[0].BringIntoView();
					return true;
				}
				num++;
			}
			return false;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0001C554 File Offset: 0x0001A754
		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = global::GClass0.smethod_0("");
			int num = 0;
			foreach (classMultipleAnswers classMultipleAnswers in this.oQuestion.SelectedCodes)
			{
				string text = this.oQuestion.QuestionName + global::GClass0.smethod_0("]œ") + this.oQuestion.QCircleDetails[num].CODE;
				VEAnswer veanswer = new VEAnswer();
				for (int i = 0; i < classMultipleAnswers.Answers.Count; i++)
				{
					veanswer = new VEAnswer();
					veanswer.QUESTION_NAME = text + global::GClass0.smethod_0("]ŀ") + (i + 1).ToString();
					veanswer.CODE = classMultipleAnswers.Answers[i].ToString();
					SurveyHelper.Answer = string.Concat(new string[]
					{
						SurveyHelper.Answer,
						global::GClass0.smethod_0("-"),
						veanswer.QUESTION_NAME,
						global::GClass0.smethod_0("<"),
						veanswer.CODE
					});
				}
				text = this.oQuestion.CircleQuestionName + global::GClass0.smethod_0("]œ") + this.oQuestion.QCircleDetails[num].CODE;
				list.Add(new VEAnswer
				{
					QUESTION_NAME = text,
					CODE = this.oQuestion.QCircleDetails[num].CODE
				});
				num++;
			}
			SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0001C73C File Offset: 0x0001A93C
		private void method_6(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0001C7A8 File Offset: 0x0001A9A8
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

		// Token: 0x0600013B RID: 315 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
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

		// Token: 0x0600013C RID: 316 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x0600013D RID: 317 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x0600013F RID: 319 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x040001B6 RID: 438
		private string MySurveyId;

		// Token: 0x040001B7 RID: 439
		private string CurPageId;

		// Token: 0x040001B8 RID: 440
		private NavBase MyNav = new NavBase();

		// Token: 0x040001B9 RID: 441
		private PageNav oPageNav = new PageNav();

		// Token: 0x040001BA RID: 442
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040001BB RID: 443
		private UDPX oFunc = new UDPX();

		// Token: 0x040001BC RID: 444
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040001BD RID: 445
		private QMatrixMultiple oQuestion = new QMatrixMultiple();

		// Token: 0x040001BE RID: 446
		private classMatixButton matixButton = new classMatixButton();

		// Token: 0x040001BF RID: 447
		private List<string> listPreSet = new List<string>();

		// Token: 0x040001C0 RID: 448
		private List<string> listFix = new List<string>();

		// Token: 0x040001C1 RID: 449
		private List<Button> listButton = new List<Button>();

		// Token: 0x040001C2 RID: 450
		private List<Button> listAllButton = new List<Button>();

		// Token: 0x040001C3 RID: 451
		private string CodeOfNone = global::GClass0.smethod_0(" ļȢ̿ФԻغܧ࠳ऩਰଫలശฮ");

		// Token: 0x040001C4 RID: 452
		private string CodeOfQnNone = global::GClass0.smethod_0("'ĵȩ̶ЫԲرܮ");

		// Token: 0x040001C5 RID: 453
		private string CodeOfGroupNone = global::GClass0.smethod_0("'ĳȩ̰ЫԲضܮ");

		// Token: 0x040001C6 RID: 454
		private bool IsFixNone;

		// Token: 0x040001C7 RID: 455
		private string GroupOfFixNone = global::GClass0.smethod_0(".");

		// Token: 0x040001C8 RID: 456
		private string GroupOfFixNormal = global::GClass0.smethod_0(".");

		// Token: 0x040001C9 RID: 457
		private string BackgroudColor = global::GClass0.smethod_0("*Ļȴ̀уՂم݄ࡇ");

		// Token: 0x040001CA RID: 458
		private int iNoOfInterval = 9999;

		// Token: 0x040001CB RID: 459
		private HorizontalAlignment CL_Label_HorizontalAlignment = HorizontalAlignment.Right;

		// Token: 0x040001CC RID: 460
		private VerticalAlignment CL_Label_VerticalAlignment = VerticalAlignment.Center;

		// Token: 0x040001CD RID: 461
		private int CL_Width;

		// Token: 0x040001CE RID: 462
		private HorizontalAlignment TR_Label_HorizontalAlignment = HorizontalAlignment.Center;

		// Token: 0x040001CF RID: 463
		private VerticalAlignment TR_Label_VerticalAlignment = VerticalAlignment.Bottom;

		// Token: 0x040001D0 RID: 464
		private string TR_Show = global::GClass0.smethod_0("");

		// Token: 0x040001D1 RID: 465
		private bool is_TR_Show;

		// Token: 0x040001D2 RID: 466
		private int PageLoaded;

		// Token: 0x040001D3 RID: 467
		private int Button_Height;

		// Token: 0x040001D4 RID: 468
		private double Button_Width;

		// Token: 0x040001D5 RID: 469
		private int Button_FontSize;

		// Token: 0x040001D6 RID: 470
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040001D7 RID: 471
		private int SecondsWait;

		// Token: 0x040001D8 RID: 472
		private int SecondsCountDown;

		// Token: 0x040001D9 RID: 473
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x02000087 RID: 135
		[CompilerGenerated]
		[Serializable]
		private sealed class Class9
		{
			// Token: 0x060006F4 RID: 1780 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_1(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CBC RID: 3260
			public static readonly GridMultiple_LoopC_FixRow1.Class9 instance = new GridMultiple_LoopC_FixRow1.Class9();

			// Token: 0x04000CBD RID: 3261
			public static Comparison<SurveyDetail> compare0;

			// Token: 0x04000CBE RID: 3262
			public static Comparison<SurveyDetail> compare1;
		}
	}
}
