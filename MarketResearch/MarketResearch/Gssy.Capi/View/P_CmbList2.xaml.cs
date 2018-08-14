using System;
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
	// Token: 0x0200002E RID: 46
	public partial class P_CmbList2 : Page
	{
		// Token: 0x060002ED RID: 749 RVA: 0x00059B50 File Offset: 0x00057D50
		public P_CmbList2()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00059BF8 File Offset: 0x00057DF8
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0);
			this.oQSingle1.Init(this.CurPageId, 1, true);
			this.oQSingle2.Init(this.CurPageId, 2, true);
			this.cmbList2Detail = this.oQSingle2.QDetails;
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
				this.oQSingle1.QuestionName = this.oQSingle1.QuestionName + this.MyNav.QName_Add;
				this.oQSingle2.QuestionName = this.oQSingle2.QuestionName + this.MyNav.QName_Add;
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
			if (this.oQSingle1.QDefine.CONTROL_TOOLTIP != global::GClass0.smethod_0(""))
			{
				string_ = this.oQSingle1.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore1, string_, this.oQSingle1.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
				string_ = ((list2.Count > 1) ? list2[1] : global::GClass0.smethod_0(""));
				this.oBoldTitle.SetTextBlock(this.txtAfter1, string_, this.oQSingle1.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
			}
			if (this.oQSingle2.QDefine.CONTROL_TOOLTIP != global::GClass0.smethod_0(""))
			{
				string_ = this.oQSingle2.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore2, string_, this.oQSingle1.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
				string_ = ((list2.Count > 1) ? list2[1] : global::GClass0.smethod_0(""));
				this.oBoldTitle.SetTextBlock(this.txtAfter2, string_, this.oQSingle1.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
			}
			if (this.oQSingle1.QDefine.CONTROL_HEIGHT != 0)
			{
				this.cmbSelect1.Height = (double)this.oQSingle1.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQSingle1.QDefine.CONTROL_WIDTH != 0)
			{
				this.cmbSelect1.Width = (double)this.oQSingle1.QDefine.CONTROL_WIDTH;
			}
			if (this.oQSingle1.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.cmbSelect1.FontSize = (double)this.oQSingle1.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQSingle2.QDefine.CONTROL_HEIGHT != 0)
			{
				this.cmbSelect2.Height = (double)this.oQSingle1.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQSingle2.QDefine.CONTROL_WIDTH != 0)
			{
				this.cmbSelect2.Width = (double)this.oQSingle2.QDefine.CONTROL_WIDTH;
			}
			if (this.oQSingle2.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.cmbSelect2.FontSize = (double)this.oQSingle1.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQSingle1.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array = this.oLogicEngine.aryCode(this.oQSingle1.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQSingle1.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list3.Add(surveyDetail);
							break;
						}
					}
				}
				list3.Sort(new Comparison<SurveyDetail>(P_CmbList2.Class31.instance.method_0));
				this.oQSingle1.QDetails = list3;
				if (this.oQSingle1.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
				{
					for (int j = 0; j < this.oQSingle1.QDetails.Count<SurveyDetail>(); j++)
					{
						this.oQSingle1.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQSingle1.QDetails[j].CODE_TEXT);
					}
				}
			}
			this.cmbSelect1.ItemsSource = this.oQSingle1.QDetails;
			this.cmbSelect1.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect1.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			if (this.oQSingle2.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQSingle2.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count<string>(); k++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQSingle2.QDetails)
					{
						if (surveyDetail2.CODE == array2[k].ToString())
						{
							list4.Add(surveyDetail2);
							break;
						}
					}
				}
				list4.Sort(new Comparison<SurveyDetail>(P_CmbList2.Class31.instance.method_1));
				this.oQSingle2.QDetails = list4;
				if (this.oQSingle2.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
				{
					for (int l = 0; l < this.oQSingle2.QDetails.Count<SurveyDetail>(); l++)
					{
						this.oQSingle2.QDetails[l].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQSingle2.QDetails[l].CODE_TEXT);
					}
				}
			}
			this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
			this.cmbSelect2.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect2.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper() == global::GClass0.smethod_0("W"))
			{
				this.wrapFill.Orientation = Orientation.Vertical;
			}
			if (this.oQSingle1.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
			{
				this.cmbSelect1.SelectedValue = this.oLogicEngine.stringResult(this.oQSingle1.QDefine.PRESET_LOGIC);
			}
			if (this.oQSingle2.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
			{
				this.cmbSelect2.SelectedValue = this.oLogicEngine.stringResult(this.oQSingle2.QDefine.PRESET_LOGIC);
			}
			else
			{
				this.cmbSelect1_SelectionChanged(this.cmbSelect1, null);
			}
			this.cmbSelect1.Focus();
			if (this.oQSingle1.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				string_ = this.oQSingle1.QDefine.NOTE;
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
					if (this.oQSingle1.QDefine.GROUP_LEVEL != global::GClass0.smethod_0("") && num > 0)
					{
						this.oQSingle1.InitCircle();
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
							foreach (SurveyDetail surveyDetail3 in this.oQSingle1.QCircleDetails)
							{
								if (surveyDetail3.CODE == text3)
								{
									text = surveyDetail3.EXTEND_1;
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
					string[] array3 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list5 = new List<SurveyDetail>();
					for (int m = 0; m < array3.Count<string>(); m++)
					{
						foreach (SurveyDetail surveyDetail4 in this.oQuestion.QDetails)
						{
							if (surveyDetail4.CODE == array3[m].ToString())
							{
								list5.Add(surveyDetail4);
								break;
							}
						}
					}
					list5.Sort(new Comparison<SurveyDetail>(P_CmbList2.Class31.instance.method_2));
					this.oQuestion.QDetails = list5;
				}
				if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
				{
					for (int n = 0; n < this.oQuestion.QDetails.Count<SurveyDetail>(); n++)
					{
						this.oQuestion.QDetails[n].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[n].CODE_TEXT);
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
				if (this.cmbSelect1.SelectedValue == null)
				{
					this.cmbSelect1.SelectedValue = autoFill.SingleDetail(this.oQSingle1.QDefine, this.oQSingle1.QDetails).CODE;
				}
				if (this.cmbSelect2.SelectedValue == null)
				{
					this.cmbSelect1_SelectionChanged(this.cmbSelect1, null);
					this.cmbSelect2.SelectedValue = autoFill.SingleDetail(this.oQSingle2.QDefine, this.oQSingle2.QDetails).CODE;
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
				else if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.cmbSelect1.SelectedValue != global::GClass0.smethod_0("") && this.cmbSelect2.SelectedValue != global::GClass0.smethod_0("") && !SurveyHelper.AutoFill)
				{
					this.btnNav_Click(this, e);
				}
			}
			else
			{
				string text5 = this.oQSingle1.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle1.QuestionName);
				string text6 = this.oQSingle2.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle2.QuestionName);
				this.cmbSelect1.SelectedValue = text5;
				this.cmbSelect2.SelectedValue = text6;
				this.cmbSelect1.Text = this.oQSingle1.GetInnerCodeText(text5);
				this.cmbSelect2.Text = this.oQSingle2.GetInnerCodeText(text6);
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
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0005B2C8 File Offset: 0x000594C8
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

		// Token: 0x060002F0 RID: 752 RVA: 0x0005B34C File Offset: 0x0005954C
		private void cmbSelect1_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			this.cmbSelect2.Focus();
			if (this.cmbSelect1.SelectedValue == null)
			{
				this.Answer1 = global::GClass0.smethod_0("");
				return;
			}
			this.Answer1 = (string)this.cmbSelect1.SelectedValue;
			if (this.oQSingle2.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				this.oQSingle2.QDetails = this.cmbList2Detail;
				this.oQSingle1.SelectedCode = this.cmbSelect1.SelectedValue.ToString();
				List<VEAnswer> list = new List<VEAnswer>();
				list.Add(new VEAnswer
				{
					QUESTION_NAME = this.oQSingle1.QuestionName,
					CODE = this.oQSingle1.SelectedCode
				});
				this.oLogicEngine.PageAnswer = list;
				string[] array = this.oLogicEngine.aryCode(this.oQSingle2.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list2 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQSingle2.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list2.Add(surveyDetail);
							break;
						}
					}
				}
				list2.Sort(new Comparison<SurveyDetail>(P_CmbList2.Class31.instance.method_3));
				this.oQSingle2.QDetails = list2;
				if (this.oQSingle2.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
				{
					for (int j = 0; j < this.oQSingle2.QDetails.Count<SurveyDetail>(); j++)
					{
						this.oQSingle2.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQSingle2.QDetails[j].CODE_TEXT);
					}
				}
				this.cmbSelect2.ItemsSource = null;
				this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
				this.cmbSelect2.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
				this.cmbSelect2.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00002D34 File Offset: 0x00000F34
		private void cmbSelect2_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect2.SelectedValue == null)
			{
				this.Answer2 = global::GClass0.smethod_0("");
				return;
			}
			this.Answer2 = (string)this.cmbSelect2.SelectedValue;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0005B5C8 File Offset: 0x000597C8
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

		// Token: 0x060002F3 RID: 755 RVA: 0x0005B714 File Offset: 0x00059914
		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			List<string> list = this.oBoldTitle.ParaToList((string)button.Tag, global::GClass0.smethod_0("\u007f"));
			string answer = list[0];
			string answer2 = (list.Count > 1) ? list[1] : global::GClass0.smethod_0("");
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				if (this.cmbSelect1.IsEnabled)
				{
					this.cmbSelect1.Tag = this.cmbSelect1.SelectedValue;
					this.cmbSelect1.Background = Brushes.LightGray;
					this.cmbSelect1.Foreground = Brushes.LightGray;
					this.cmbSelect1.IsEnabled = false;
					this.cmbSelect2.Tag = this.cmbSelect2.SelectedValue;
					this.cmbSelect2.Background = Brushes.LightGray;
					this.cmbSelect2.Foreground = Brushes.LightGray;
					this.cmbSelect2.IsEnabled = false;
				}
				this.Answer1 = answer;
				this.Answer2 = answer2;
				foreach(var child in this.wrapButton.Children)
				{
					{
						Button button2 = (Button)child;
						button2.Style = ((button2.Name == button.Name) ? style : style2);
					}
					return;
				}
			}
			this.cmbSelect1.SelectedValue = (string)this.cmbSelect1.Tag;
			this.cmbSelect1.IsEnabled = true;
			this.cmbSelect1.Background = Brushes.White;
			this.cmbSelect1.Foreground = Brushes.Black;
			this.cmbSelect2.SelectedValue = (string)this.cmbSelect2.Tag;
			this.cmbSelect2.IsEnabled = true;
			this.cmbSelect2.Background = Brushes.White;
			this.cmbSelect2.Foreground = Brushes.Black;
			button.Style = style2;
			this.Answer1 = global::GClass0.smethod_0("");
			this.Answer2 = global::GClass0.smethod_0("");
			this.cmbSelect1.Focus();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0005B988 File Offset: 0x00059B88
		private bool method_4()
		{
			if (this.Answer1 == global::GClass0.smethod_0("") && (this.cmbSelect1.SelectedValue == null || (string)this.cmbSelect1.SelectedValue == global::GClass0.smethod_0("")))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.cmbSelect1.Focus();
				return true;
			}
			if (this.Answer2 == global::GClass0.smethod_0("") && (this.cmbSelect2.SelectedValue == null || (string)this.cmbSelect2.SelectedValue == global::GClass0.smethod_0("")))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.cmbSelect2.Focus();
				return true;
			}
			if (this.Answer1 == global::GClass0.smethod_0("") && this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("9"))
			{
				DateTime date = DateTime.Now.Date;
				if (Convert.ToDateTime(this.cmbSelect1.SelectedValue.ToString() + global::GClass0.smethod_0(",") + this.cmbSelect2.SelectedValue.ToString() + global::GClass0.smethod_0(".ĲȰ")) > date)
				{
					MessageBox.Show(SurveyMsg.MsgNotAfterYM, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.cmbSelect1.Focus();
					return true;
				}
			}
			else if (this.Answer1 == global::GClass0.smethod_0("") && this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("8"))
			{
				DateTime date2 = DateTime.Now.Date;
				string text = date2.Year.ToString();
				if (Convert.ToDateTime(string.Concat(new string[]
				{
					text,
					global::GClass0.smethod_0(","),
					this.cmbSelect1.SelectedValue.ToString(),
					global::GClass0.smethod_0(","),
					this.cmbSelect2.SelectedValue.ToString()
				})) > date2)
				{
					MessageBox.Show(SurveyMsg.MsgNotAfterDate, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.cmbSelect1.Focus();
					return true;
				}
			}
			if (this.Answer1 == global::GClass0.smethod_0(""))
			{
				this.Answer1 = (string)this.cmbSelect1.SelectedValue;
			}
			if (this.Answer2 == global::GClass0.smethod_0(""))
			{
				this.Answer2 = (string)this.cmbSelect2.SelectedValue;
			}
			this.oQSingle1.SelectedCode = this.Answer1;
			this.oQSingle2.SelectedCode = this.Answer2;
			return false;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0005BC6C File Offset: 0x00059E6C
		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQSingle1.QuestionName,
				CODE = this.oQSingle1.SelectedCode
			});
			SurveyHelper.Answer = this.oQSingle1.QuestionName + global::GClass0.smethod_0("<") + this.oQSingle1.SelectedCode;
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQSingle2.QuestionName,
				CODE = this.oQSingle2.SelectedCode
			});
			SurveyHelper.Answer = string.Concat(new string[]
			{
				SurveyHelper.Answer,
				global::GClass0.smethod_0("-"),
				this.oQSingle2.QuestionName,
				global::GClass0.smethod_0("<"),
				this.oQSingle2.SelectedCode
			});
			return list;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0005BD58 File Offset: 0x00059F58
		private void method_6(List<VEAnswer> list_0)
		{
			this.oQSingle1.BeforeSave();
			this.oQSingle1.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
			this.oQSingle2.BeforeSave();
			this.oQSingle2.Save(this.MySurveyId, SurveyHelper.SurveySequence, false);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0005BDAC File Offset: 0x00059FAC
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

		// Token: 0x060002F8 RID: 760 RVA: 0x0005BEA4 File Offset: 0x0005A0A4
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

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x060002FA RID: 762 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x060002FC RID: 764 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0005BF0C File Offset: 0x0005A10C
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

		// Token: 0x060002FE RID: 766 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_12(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00002D6A File Offset: 0x00000F6A
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQSingle1.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x040005AA RID: 1450
		private string MySurveyId;

		// Token: 0x040005AB RID: 1451
		private string CurPageId;

		// Token: 0x040005AC RID: 1452
		private NavBase MyNav = new NavBase();

		// Token: 0x040005AD RID: 1453
		private PageNav oPageNav = new PageNav();

		// Token: 0x040005AE RID: 1454
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040005AF RID: 1455
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040005B0 RID: 1456
		private QBase oQuestion = new QBase();

		// Token: 0x040005B1 RID: 1457
		private QSingle oQSingle1 = new QSingle();

		// Token: 0x040005B2 RID: 1458
		private QSingle oQSingle2 = new QSingle();

		// Token: 0x040005B3 RID: 1459
		private string Answer1 = global::GClass0.smethod_0("");

		// Token: 0x040005B4 RID: 1460
		private string Answer2 = global::GClass0.smethod_0("");

		// Token: 0x040005B5 RID: 1461
		private bool PageLoaded;

		// Token: 0x040005B6 RID: 1462
		private int Button_Type;

		// Token: 0x040005B7 RID: 1463
		private int Button_Height;

		// Token: 0x040005B8 RID: 1464
		private double Button_Width;

		// Token: 0x040005B9 RID: 1465
		private int Button_FontSize;

		// Token: 0x040005BA RID: 1466
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040005BB RID: 1467
		private int SecondsWait;

		// Token: 0x040005BC RID: 1468
		private int SecondsCountDown;

		// Token: 0x040005BD RID: 1469
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x040005BE RID: 1470
		private List<SurveyDetail> cmbList2Detail = new List<SurveyDetail>();

		// Token: 0x0200009D RID: 157
		[CompilerGenerated]
		[Serializable]
		private sealed class Class31
		{
			// Token: 0x0600073F RID: 1855 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_1(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_2(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_3(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CF3 RID: 3315
			public static readonly P_CmbList2.Class31 instance = new P_CmbList2.Class31();

			// Token: 0x04000CF4 RID: 3316
			public static Comparison<SurveyDetail> compare0;

			// Token: 0x04000CF5 RID: 3317
			public static Comparison<SurveyDetail> compare1;

			// Token: 0x04000CF6 RID: 3318
			public static Comparison<SurveyDetail> compare2;

			// Token: 0x04000CF7 RID: 3319
			public static Comparison<SurveyDetail> compare3;
		}
	}
}
