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
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x0200001D RID: 29
	public partial class GridSingle_LoopR_FixRow1 : Page
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0002F1C0 File Offset: 0x0002D3C0
		public GridSingle_LoopR_FixRow1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
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
			this.CL_Width = this.oFunc.StringToInt(this.oBoldTitle.AlignmentPara(this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim(), ref this.CircleLabel_HorizontalAlignment, ref this.CircleLabel_VerticalAlignment));
			bool bool_ = (this.oQuestion.QCircleDefine.CONTROL_TYPE == 0 && this.oQuestion.QDefine.PARENT_CODE == global::GClass0.smethod_0("") && this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0("")) || (this.oQuestion.QCircleDefine.CONTROL_TYPE == 1 && this.oQuestion.QCircleDefine.PARENT_CODE == global::GClass0.smethod_0(""));
			this.oBoldTitle.SetTextBlock(this.GridTopLeftText, this.oQuestion.QCircleDefine.QUESTION_CONTENT, this.oQuestion.QCircleDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), bool_);
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0("") && this.oQuestion.QCircleDefine.CONTROL_TYPE == 0)
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
				list4.Sort(new Comparison<SurveyDetail>(GridSingle_LoopR_FixRow1.Class15.instance.method_0));
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
						goto IL_C0A;
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
				IL_C0A:
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
				list6.Sort(new Comparison<SurveyDetail>(GridSingle_LoopR_FixRow1.Class15.instance.method_1));
				this.oQuestion.QDetails = list6;
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0("") && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == global::GClass0.smethod_0("2"))))
			{
				string[] array4 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int m = 0; m < array4.Count<string>(); m++)
				{
					using (List<SurveyDetail>.Enumerator enumerator2 = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CODE == array4[m])
							{
								this.listPreSet.Add(array4[m]);
								break;
							}
						}
					}
				}
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int n = 0; n < this.oQuestion.QDetails.Count<SurveyDetail>(); n++)
				{
					this.oQuestion.QDetails[n].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[n].CODE_TEXT);
				}
			}
			else if (this.oQuestion.QDefine.SHOW_LOGIC != global::GClass0.smethod_0(""))
			{
				string string_3 = list[0];
				string[] array5 = this.oLogicEngine.aryCode(string_3, ',');
				List<SurveyDetail> list7 = new List<SurveyDetail>();
				for (int num4 = 0; num4 < array5.Count<string>(); num4++)
				{
					foreach (SurveyDetail surveyDetail6 in this.oQuestion.QDetails)
					{
						if (surveyDetail6.CODE == array5[num4].ToString())
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
						goto IL_1076;
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
				IL_1076:
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
				double num5 = base.ActualWidth - 63.0;
				if (this.CL_Width > 0)
				{
					num5 -= (double)this.CL_Width;
				}
				if (this.Button_Width != 1.0 && this.oQuestion.QDetails.Count <= 7)
				{
					if (this.CL_Width == 0)
					{
						this.CL_Width = (int)(num5 / 16.0 * 4.0);
						num5 = num5 / 16.0 * 10.0 - 8.0;
					}
					else
					{
						num5 = num5 / 12.0 * 10.0 - 8.0;
					}
				}
				else if (this.CL_Width == 0)
				{
					this.CL_Width = (int)(num5 / 14.0 * 4.0);
					num5 = num5 / 14.0 * 10.0 - 8.0;
				}
				this.Button_Width = (num5 - (double)((this.oQuestion.QDetails.Count - 1) * 4) - 43.0) / (double)this.oQuestion.QDetails.Count;
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
			this.SameClickCheck = true;
			this.PageLoaded = true;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000307A8 File Offset: 0x0002E9A8
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded)
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
				bool flag = this.oQuestion.QCircleDefine.CONTROL_TYPE == 0 && this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0("") && this.oQuestion.QDefine.PARENT_CODE != global::GClass0.smethod_0("");
				if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 1 || this.oQuestion.QCircleDefine.CONTROL_TYPE == 2 || flag)
				{
					Brush borderBrush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
					Grid gridBottomRight = this.GridBottomRight;
					Grid gridTopRight = this.GridTopRight;
					int num2 = 0;
					Border border = new Border();
					string b = global::GClass0.smethod_0("");
					int num3 = 1;
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						string text = global::GClass0.smethod_0("");
						if (flag && surveyDetail.PARENT_CODE != b)
						{
							using (List<SurveyDetail>.Enumerator enumerator2 = this.oQuestion.QGroupDetails.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									SurveyDetail surveyDetail2 = enumerator2.Current;
									if (surveyDetail2.CODE == surveyDetail.PARENT_CODE)
									{
										text = surveyDetail2.CODE_TEXT;
										break;
									}
								}
								goto IL_3A2;
							}
							goto IL_215;
						}
						goto IL_3A2;
						IL_38B:
						if (flag)
						{
							b = surveyDetail.PARENT_CODE;
						}
						num2++;
						continue;
						IL_215:
						if (surveyDetail.PARENT_CODE == b)
						{
							num3++;
							border.SetValue(Grid.ColumnSpanProperty, num3);
							goto IL_38B;
						}
						IL_243:
						border = new Border();
						border.BorderThickness = new Thickness((double)((this.TR_Show == global::GClass0.smethod_0("1")) ? 0 : 1));
						border.BorderBrush = borderBrush;
						border.SetValue(Grid.RowProperty, 0);
						border.SetValue(Grid.ColumnProperty, num2);
						num3 = 1;
						gridTopRight.Children.Add(border);
						TextBlock textBlock = new TextBlock();
						border.Child = textBlock;
						textBlock.Text = (flag ? text : surveyDetail.CODE_TEXT);
						textBlock.Style = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
						textBlock.Foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
						textBlock.TextWrapping = TextWrapping.Wrap;
						textBlock.Margin = new Thickness(2.0, 5.0, 2.0, 5.0);
						textBlock.HorizontalAlignment = this.TR_Label_HorizontalAlignment;
						textBlock.VerticalAlignment = this.TR_Label_VerticalAlignment;
						if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
						{
							textBlock.FontSize = (double)this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
							goto IL_38B;
						}
						goto IL_38B;
						IL_3A2:
						Button button = this.matixButton.Attributes[0].Buttons[num2];
						double actualWidth = gridBottomRight.ColumnDefinitions[num2].ActualWidth;
						gridTopRight.ColumnDefinitions.Add(new ColumnDefinition
						{
							Width = new GridLength(actualWidth)
						});
						if (flag)
						{
							goto IL_215;
						}
						goto IL_243;
					}
				}
				if (this.is_CL_Show)
				{
					Grid gridBottomRight2 = this.GridBottomRight;
					Grid gridBottomLeft = this.GridBottomLeft;
					int num4 = 0;
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QCircleDetails)
					{
						gridBottomLeft.RowDefinitions[num4].Height = new GridLength(gridBottomRight2.RowDefinitions[num4].ActualHeight);
						num4++;
					}
					if (this.CL_Width > 0)
					{
						this.GridTopLeft.Width = (double)this.CL_Width;
						this.GridBottomLeft.Width = (double)this.CL_Width;
					}
					else
					{
						this.GridTopLeft.Width = this.GridBottomLeft.ActualWidth;
					}
				}
				if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 0 && this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0(""))
				{
					double num5 = 0.0;
					if (this.OthButtonStartCol > 0)
					{
						Grid gridBottomRight3 = this.GridBottomRight;
						for (int i = this.OthButtonStartCol; i < gridBottomRight3.ColumnDefinitions.Count<ColumnDefinition>(); i++)
						{
							num5 += gridBottomRight3.ColumnDefinitions[i].ActualWidth;
						}
					}
					this.GridTopRight.Width = this.GridBottomRight.ActualWidth - num5;
				}
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
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				this.PageLoaded = false;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00030E58 File Offset: 0x0002F058
		private void method_2()
		{
			AutoFill autoFill = new AutoFill();
			autoFill.oLogicEngine = this.oLogicEngine;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			Style style3 = (Style)base.FindResource(global::GClass0.smethod_0("Qžɾͻѫգٸ݆࡯७੡୲౫ൖ๰ེၮᅤ"));
			Brush borderBrush = (Brush)base.FindResource(global::GClass0.smethod_0("_ſɽͣѬՠىݥࡻ६੢୴ే൶๶ཱၩ"));
			Brush foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
			Grid gridBottomRight = this.GridBottomRight;
			Grid gridBottomLeft = this.GridBottomLeft;
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
				this.oQuestion.SelectedCode.Add(text2);
				gridBottomLeft.RowDefinitions.Add(new RowDefinition
				{
					Height = GridLength.Auto
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
				bool flag2 = this.oQuestion.QCircleDefine.CONTROL_TYPE == 1 && this.oQuestion.QCircleDefine.PARENT_CODE != global::GClass0.smethod_0("");
				if (this.oQuestion.QCircleDefine.CONTROL_TYPE != 1 || flag2)
				{
					this.is_CL_Show = true;
					string text3 = global::GClass0.smethod_0("");
					if (flag2 && surveyDetail.PARENT_CODE != b)
					{
						using (List<SurveyDetail>.Enumerator enumerator2 = this.oQuestion.QCircleGroupDetails.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								SurveyDetail surveyDetail2 = enumerator2.Current;
								if (surveyDetail2.CODE == surveyDetail.PARENT_CODE)
								{
									text3 = surveyDetail2.CODE_TEXT;
									break;
								}
							}
							goto IL_CA6;
						}
						goto IL_3A6;
					}
					goto IL_CA6;
					IL_52B:
					b = surveyDetail.PARENT_CODE;
					goto IL_534;
					IL_3D4:
					border2 = new Border();
					border2.BorderThickness = new Thickness(1.0);
					border2.BorderBrush = borderBrush;
					border2.SetValue(Grid.RowProperty, num2);
					border2.SetValue(Grid.ColumnProperty, 0);
					num4 = 1;
					gridBottomLeft.Children.Add(border2);
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
					textBlock.Text = (flag2 ? text3 : code_TEXT);
					textBlock.Style = style3;
					textBlock.Foreground = foreground;
					textBlock.TextWrapping = TextWrapping.Wrap;
					textBlock.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
					textBlock.HorizontalAlignment = this.CircleLabel_HorizontalAlignment;
					textBlock.VerticalAlignment = this.CircleLabel_VerticalAlignment;
					if (this.oQuestion.QCircleDefine.CONTROL_FONTSIZE > 0)
					{
						textBlock.FontSize = (double)this.oQuestion.QCircleDefine.CONTROL_FONTSIZE;
					}
					border2.Child = textBlock;
					goto IL_52B;
					IL_CA6:
					if (!flag2)
					{
						goto IL_3D4;
					}
					IL_3A6:
					if (surveyDetail.PARENT_CODE == b)
					{
						num4++;
						border2.SetValue(Grid.RowSpanProperty, num4);
						goto IL_52B;
					}
					goto IL_3D4;
				}
				IL_534:
				gridBottomRight.RowDefinitions.Add(new RowDefinition
				{
					Height = GridLength.Auto
				});
				if (num2 == 0)
				{
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QDetails)
					{
						gridBottomRight.ColumnDefinitions.Add(new ColumnDefinition
						{
							Width = GridLength.Auto
						});
					}
				}
				border = new Border();
				border.BorderThickness = new Thickness(1.0);
				border.BorderBrush = borderBrush;
				border.SetValue(Grid.RowProperty, num2);
				border.SetValue(Grid.ColumnProperty, 0);
				border.SetValue(Grid.ColumnSpanProperty, this.oQuestion.QDetails.Count);
				gridBottomRight.Children.Add(border);
				if (flag)
				{
					border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(this.BackgroudColor));
				}
				string[] string_2 = this.oLogicEngine.aryCode(surveyDetail.EXTEND_4, ',');
				this.listButton = new List<Button>();
				this.matixButton.Attributes.Add(new classListButton());
				int num5 = 0;
				foreach (SurveyDetail surveyDetail4 in this.oQuestion.QDetails)
				{
					if (this.OthButtonStartCol == 0 && surveyDetail4.IS_OTHER > 0)
					{
						this.OthButtonStartCol = num5;
					}
					WrapPanel wrapPanel = new WrapPanel();
					wrapPanel.VerticalAlignment = VerticalAlignment.Center;
					wrapPanel.HorizontalAlignment = HorizontalAlignment.Center;
					if (num5 == 0)
					{
						wrapPanel.Margin = new Thickness(5.0, 5.0, 2.0, 5.0);
					}
					else if (num5 == this.oQuestion.QDetails.Count - 1)
					{
						wrapPanel.Margin = new Thickness(2.0, 5.0, 5.0, 5.0);
					}
					else
					{
						wrapPanel.Margin = new Thickness(2.0, 5.0, 2.0, 5.0);
					}
					wrapPanel.SetValue(Grid.RowProperty, num2);
					wrapPanel.SetValue(Grid.ColumnProperty, num5);
					gridBottomRight.Children.Add(wrapPanel);
					Button button = new Button();
					button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail4.CODE;
					if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 0)
					{
						button.Content = surveyDetail4.CODE_TEXT;
					}
					else if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 1)
					{
						button.Content = surveyDetail.CODE_TEXT;
					}
					else if (this.oQuestion.QCircleDefine.CONTROL_TYPE == 2)
					{
						button.Content = surveyDetail4.EXTEND_1;
					}
					button.Margin = new Thickness(0.0);
					button.Style = ((surveyDetail4.CODE == text2) ? style : style2);
					if (flag)
					{
						button.Opacity = 0.85;
					}
					button.Tag = num2;
					button.Click += this.method_3;
					button.FontSize = (double)this.Button_FontSize;
					button.MinWidth = this.Button_Width;
					button.MinHeight = (double)this.Button_Height;
					wrapPanel.Children.Add(button);
					this.matixButton.Attributes[num2].Buttons.Add(button);
					if (surveyDetail.EXTEND_4 != global::GClass0.smethod_0("") && this.oFunc.StringInArray(surveyDetail4.CODE, string_2, true) == global::GClass0.smethod_0(""))
					{
						wrapPanel.Visibility = Visibility.Collapsed;
					}
					if (wrapPanel.Visibility == Visibility.Visible)
					{
						if (surveyDetail4.EXTEND_2 != global::GClass0.smethod_0(""))
						{
							wrapPanel.Visibility = Visibility.Collapsed;
							if (this.oBoldTitle.ParaToList(surveyDetail4.EXTEND_2, global::GClass0.smethod_0("-Į")).Contains(code))
							{
								wrapPanel.Visibility = Visibility.Visible;
							}
						}
						if (surveyDetail4.EXTEND_3 != global::GClass0.smethod_0("") && this.oBoldTitle.ParaToList(surveyDetail4.EXTEND_3, global::GClass0.smethod_0("-Į")).Contains(code))
						{
							wrapPanel.Visibility = Visibility.Collapsed;
						}
					}
					if (wrapPanel.Visibility == Visibility.Visible)
					{
						this.listButton.Add(button);
					}
					num5++;
				}
				int num6 = 0;
				if ((!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == global::GClass0.smethod_0("2"))) && SurveyHelper.NavOperation != global::GClass0.smethod_0("FŢɡͪ"))
				{
					string extend_ = surveyDetail.EXTEND_6;
					if (extend_ != global::GClass0.smethod_0(""))
					{
						string[] array = this.oLogicEngine.aryCode(extend_, ',');
						int i = 0;
						while (i < array.Count<string>())
						{
							using (List<Button>.Enumerator enumerator3 = this.listButton.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									Button button2 = enumerator3.Current;
									if (button2.Name == global::GClass0.smethod_0("`Ş") + array[i])
									{
										num6 = 1;
										this.method_3(button2, new RoutedEventArgs());
										break;
									}
								}
								goto IL_AF7;
							}
							IL_AEF:
							i++;
							continue;
							IL_AF7:
							if (num6 > 0)
							{
								break;
							}
							goto IL_AEF;
						}
					}
					if (num6 == 0)
					{
						foreach (string str in this.listPreSet)
						{
							foreach (Button button3 in this.listButton)
							{
								if (button3.Name == global::GClass0.smethod_0("`Ş") + str)
								{
									num6 = 1;
									this.method_3(button3, new RoutedEventArgs());
									break;
								}
							}
							if (num6 > 0)
							{
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
					Button button4;
					if (this.oQuestion.QDefine.CONTROL_TYPE == 0)
					{
						button4 = autoFill.SingleButton(this.oQuestion.QDefine, this.listButton);
					}
					else
					{
						if (this.AutoFillButton == -1)
						{
							this.AutoFillButton = Convert.ToInt32(this.oFunc.INT((double)(this.listButton.Count<Button>() / 2), 0, 0, 0));
						}
						button4 = this.listButton[this.AutoFillButton];
					}
					if (button4 != null && num6 == 0)
					{
						this.method_3(button4, new RoutedEventArgs());
					}
				}
				num2++;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00031BD4 File Offset: 0x0002FDD4
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
				classListButton classListButton = this.matixButton.Attributes[index];
				this.oQuestion.SelectedCode[index] = text;
				foreach (Button button2 in classListButton.Buttons)
				{
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

		// Token: 0x06000197 RID: 407 RVA: 0x00031D74 File Offset: 0x0002FF74
		private bool method_4()
		{
			int num = 0;
			using (List<string>.Enumerator enumerator = this.oQuestion.SelectedCode.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == global::GClass0.smethod_0("") && this.GridBottomRight.RowDefinitions[num].ActualHeight > 3.0)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, this.oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.matixButton.Attributes[num].Buttons[0].Focus();
						this.matixButton.Attributes[num].Buttons[0].BringIntoView();
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
				string a = global::GClass0.smethod_0("");
				int num3 = 0;
				num = 0;
				foreach (string text in this.oQuestion.SelectedCode)
				{
					if (a == text)
					{
						num3++;
					}
					else
					{
						if (num3 >= num2)
						{
							num--;
							this.matixButton.Attributes[num].Buttons[0].Focus();
							this.matixButton.Attributes[num].Buttons[0].BringIntoView();
							string text2 = string.Format(SurveyMsg.MsgMXSA_info1, this.oQuestion.QCircleDetails[num].CODE_TEXT, num3);
							if (this.oQuestion.QCircleDefine.MIN_COUNT > 0)
							{
								MessageBox.Show(text2, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								return true;
							}
							text2 = text2 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
							if (MessageBox.Show(text2, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
							{
								return true;
							}
						}
						num3 = 1;
						a = text;
					}
					num++;
				}
				if (num3 >= num2)
				{
					num--;
					this.matixButton.Attributes[num].Buttons[0].Focus();
					this.matixButton.Attributes[num].Buttons[0].BringIntoView();
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
				foreach (string text4 in this.oQuestion.SelectedCode)
				{
					if (dictionary.ContainsKey(text4))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string key = text4;
						int num5 = dictionary2[key];
						dictionary2[key] = num5 + 1;
					}
					else
					{
						dictionary.Add(text4, 1);
					}
					num++;
				}
				foreach (string text5 in dictionary.Keys)
				{
					if (dictionary[text5] >= num4)
					{
						string arg = global::GClass0.smethod_0("");
						using (List<SurveyDetail>.Enumerator enumerator3 = this.oQuestion.QDetails.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								SurveyDetail surveyDetail = enumerator3.Current;
								if (surveyDetail.CODE == text5)
								{
									arg = surveyDetail.CODE_TEXT;
									break;
								}
							}
							goto IL_530;
						}
						IL_4EC:
						string text6 = text6 + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck;
						if (MessageBox.Show(text6, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
						{
							return true;
						}
						continue;
						IL_530:
						text6 = string.Format(SurveyMsg.MsgMXSA_info2, arg, dictionary[text5]);
						if (this.oQuestion.QCircleDefine.MIN_COUNT > 0)
						{
							MessageBox.Show(text6, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							return true;
						}
						goto IL_4EC;
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
				foreach (string string_ in this.oQuestion.SelectedCode)
				{
					int num9 = this.oFunc.StringToInt(string_);
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
					num++;
				}
				if (num6 < num7 && num6 > 0)
				{
					this.matixButton.Attributes[index].Buttons[0].Focus();
					this.matixButton.Attributes[num].Buttons[0].BringIntoView();
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
					this.matixButton.Attributes[index].Buttons[0].Focus();
					this.matixButton.Attributes[num].Buttons[0].BringIntoView();
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

		// Token: 0x06000198 RID: 408 RVA: 0x00032690 File Offset: 0x00030890
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

		// Token: 0x06000199 RID: 409 RVA: 0x0003281C File Offset: 0x00030A1C
		private void method_6(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				this.oPageNav.PageDataLog(this.oQuestion.QDefine.PAGE_COUNT_DOWN, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00032888 File Offset: 0x00030A88
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

		// Token: 0x0600019B RID: 411 RVA: 0x00032988 File Offset: 0x00030B88
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

		// Token: 0x0600019C RID: 412 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x0600019D RID: 413 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x0600019F RID: 415 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x040002EC RID: 748
		private string MySurveyId;

		// Token: 0x040002ED RID: 749
		private string CurPageId;

		// Token: 0x040002EE RID: 750
		private NavBase MyNav = new NavBase();

		// Token: 0x040002EF RID: 751
		private PageNav oPageNav = new PageNav();

		// Token: 0x040002F0 RID: 752
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040002F1 RID: 753
		private UDPX oFunc = new UDPX();

		// Token: 0x040002F2 RID: 754
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040002F3 RID: 755
		private QMatrixSingle oQuestion = new QMatrixSingle();

		// Token: 0x040002F4 RID: 756
		private classMatixButton matixButton = new classMatixButton();

		// Token: 0x040002F5 RID: 757
		private List<string> listPreSet = new List<string>();

		// Token: 0x040002F6 RID: 758
		private List<Button> listButton = new List<Button>();

		// Token: 0x040002F7 RID: 759
		private int OthButtonStartCol;

		// Token: 0x040002F8 RID: 760
		private int AutoFillButton = -1;

		// Token: 0x040002F9 RID: 761
		private string LastClickCode = global::GClass0.smethod_0("");

		// Token: 0x040002FA RID: 762
		private int SameClickCount;

		// Token: 0x040002FB RID: 763
		private bool SameClickCheck;

		// Token: 0x040002FC RID: 764
		private string BackgroudColor = global::GClass0.smethod_0("*Ļȴ̀уՂم݄ࡇ");

		// Token: 0x040002FD RID: 765
		private int iNoOfInterval = 9999;

		// Token: 0x040002FE RID: 766
		private HorizontalAlignment CircleLabel_HorizontalAlignment = HorizontalAlignment.Right;

		// Token: 0x040002FF RID: 767
		private VerticalAlignment CircleLabel_VerticalAlignment = VerticalAlignment.Center;

		// Token: 0x04000300 RID: 768
		private int CL_Width;

		// Token: 0x04000301 RID: 769
		private HorizontalAlignment TR_Label_HorizontalAlignment = HorizontalAlignment.Center;

		// Token: 0x04000302 RID: 770
		private VerticalAlignment TR_Label_VerticalAlignment = VerticalAlignment.Bottom;

		// Token: 0x04000303 RID: 771
		private string TR_Show = global::GClass0.smethod_0("");

		// Token: 0x04000304 RID: 772
		private bool is_CL_Show;

		// Token: 0x04000305 RID: 773
		private bool PageLoaded;

		// Token: 0x04000306 RID: 774
		private int Button_Height;

		// Token: 0x04000307 RID: 775
		private double Button_Width;

		// Token: 0x04000308 RID: 776
		private int Button_FontSize;

		// Token: 0x04000309 RID: 777
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x0400030A RID: 778
		private int SecondsWait;

		// Token: 0x0400030B RID: 779
		private int SecondsCountDown;

		// Token: 0x0400030C RID: 780
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x0200008D RID: 141
		[CompilerGenerated]
		[Serializable]
		private sealed class Class15
		{
			// Token: 0x0600070C RID: 1804 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x0600070D RID: 1805 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_1(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CCE RID: 3278
			public static readonly GridSingle_LoopR_FixRow1.Class15 instance = new GridSingle_LoopR_FixRow1.Class15();

			// Token: 0x04000CCF RID: 3279
			public static Comparison<SurveyDetail> compare0;

			// Token: 0x04000CD0 RID: 3280
			public static Comparison<SurveyDetail> compare1;
		}
	}
}
