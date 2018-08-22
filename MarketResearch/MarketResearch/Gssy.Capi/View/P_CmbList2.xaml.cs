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
	public partial class P_CmbList2 : Page
	{
		public P_CmbList2()
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
			this.oQSingle1.Init(this.CurPageId, 1, true);
			this.oQSingle2.Init(this.CurPageId, 2, true);
			this.cmbList2Detail = this.oQSingle2.QDetails;
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (this.MyNav.GroupLevel != "")
			{
				this.MyNav.GroupPageType = this.oQuestion.QDefine.GROUP_PAGE_TYPE;
				this.MyNav.GroupCodeA = this.oQuestion.QDefine.GROUP_CODEA;
				this.MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				this.MyNav.CircleACount = SurveyHelper.CircleACount;
				if (this.MyNav.GroupLevel == "B")
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
				if (this.MyNav.GroupLevel == "B")
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
				this.oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				this.oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				this.oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				this.oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQSingle1.QDefine.CONTROL_TOOLTIP != "")
			{
				string_ = this.oQSingle1.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore1, string_, this.oQSingle1.QDefine.CONTROL_FONTSIZE, "", true);
				string_ = ((list2.Count > 1) ? list2[1] : "");
				this.oBoldTitle.SetTextBlock(this.txtAfter1, string_, this.oQSingle1.QDefine.CONTROL_FONTSIZE, "", true);
			}
			if (this.oQSingle2.QDefine.CONTROL_TOOLTIP != "")
			{
				string_ = this.oQSingle2.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore2, string_, this.oQSingle1.QDefine.CONTROL_FONTSIZE, "", true);
				string_ = ((list2.Count > 1) ? list2[1] : "");
				this.oBoldTitle.SetTextBlock(this.txtAfter2, string_, this.oQSingle1.QDefine.CONTROL_FONTSIZE, "", true);
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
			if (this.oQSingle1.QDefine.LIMIT_LOGIC != "")
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
				if (this.oQSingle1.QDefine.DETAIL_ID.Substring(0, 1) == "#")
				{
					for (int j = 0; j < this.oQSingle1.QDetails.Count<SurveyDetail>(); j++)
					{
						this.oQSingle1.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQSingle1.QDetails[j].CODE_TEXT);
					}
				}
			}
			this.cmbSelect1.ItemsSource = this.oQSingle1.QDetails;
			this.cmbSelect1.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect1.SelectedValuePath = "CODE";
			if (this.oQSingle2.QDefine.LIMIT_LOGIC != "")
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
				if (this.oQSingle2.QDefine.DETAIL_ID.Substring(0, 1) == "#")
				{
					for (int l = 0; l < this.oQSingle2.QDetails.Count<SurveyDetail>(); l++)
					{
						this.oQSingle2.QDetails[l].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQSingle2.QDetails[l].CODE_TEXT);
					}
				}
			}
			this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
			this.cmbSelect2.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect2.SelectedValuePath = "CODE";
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper() == "V")
			{
				this.wrapFill.Orientation = Orientation.Vertical;
			}
			if (this.oQSingle1.QDefine.PRESET_LOGIC != "")
			{
				this.cmbSelect1.SelectedValue = this.oLogicEngine.stringResult(this.oQSingle1.QDefine.PRESET_LOGIC);
			}
			if (this.oQSingle2.QDefine.PRESET_LOGIC != "")
			{
				this.cmbSelect2.SelectedValue = this.oLogicEngine.stringResult(this.oQSingle2.QDefine.PRESET_LOGIC);
			}
			else
			{
				this.cmbSelect1_SelectionChanged(this.cmbSelect1, null);
			}
			this.cmbSelect1.Focus();
			if (this.oQSingle1.QDefine.NOTE != "")
			{
				string_ = this.oQSingle1.QDefine.NOTE;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, "", true);
				if (list2.Count > 1)
				{
					string text = "";
					string text2 = "";
					int num = list2[1].IndexOf(">");
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
					if (this.oQSingle1.QDefine.GROUP_LEVEL != "" && num > 0)
					{
						this.oQSingle1.InitCircle();
						string text3 = "";
						if (this.MyNav.GroupLevel == "A")
						{
							text3 = this.MyNav.CircleACode;
						}
						if (this.MyNav.GroupLevel == "B")
						{
							text3 = this.MyNav.CircleBCode;
						}
						if (text3 != "")
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
					if (text != "")
					{
						string text4 = Environment.CurrentDirectory + "\\Media\\" + text;
						if (this.method_8(text, 1) == "#")
						{
							text4 = "..\\Resources\\Pic\\" + this.method_9(text, 1, -9999);
						}
						else if (!File.Exists(text4))
						{
							text4 = "..\\Resources\\Pic\\" + text;
						}
						Image image = new Image();
						if (num > 0)
						{
							this.scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
							this.scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							image.Height = (double)num;
						}
						else if (text2 == "#")
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
				if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
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
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
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
			Style style = (Style)base.FindResource("SelBtnStyle");
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back"))
			{
				if (!(navOperation == "Normal"))
				{
					if (!(navOperation == "Jump"))
					{
					}
				}
				else if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.cmbSelect1.SelectedValue != "" && this.cmbSelect2.SelectedValue != "" && !SurveyHelper.AutoFill)
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

		private void cmbSelect1_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			this.cmbSelect2.Focus();
			if (this.cmbSelect1.SelectedValue == null)
			{
				this.Answer1 = "";
				return;
			}
			this.Answer1 = (string)this.cmbSelect1.SelectedValue;
			if (this.oQSingle2.QDefine.LIMIT_LOGIC != "")
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
				if (this.oQSingle2.QDefine.DETAIL_ID.Substring(0, 1) == "#")
				{
					for (int j = 0; j < this.oQSingle2.QDetails.Count<SurveyDetail>(); j++)
					{
						this.oQSingle2.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQSingle2.QDetails[j].CODE_TEXT);
					}
				}
				this.cmbSelect2.ItemsSource = null;
				this.cmbSelect2.ItemsSource = this.oQSingle2.QDetails;
				this.cmbSelect2.DisplayMemberPath = "CODE_TEXT";
				this.cmbSelect2.SelectedValuePath = "CODE";
			}
		}

		private void cmbSelect2_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect2.SelectedValue == null)
			{
				this.Answer2 = "";
				return;
			}
			this.Answer2 = (string)this.cmbSelect2.SelectedValue;
		}

		private void method_2()
		{
			Style style = (Style)base.FindResource("UnSelBtnStyle");
			WrapPanel wrapPanel = this.wrapButton;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = "b_" + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = surveyDetail.EXTEND_1 + "~" + surveyDetail.EXTEND_2;
				button.Click += this.method_3;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				wrapPanel.Children.Add(button);
			}
		}

		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			List<string> list = this.oBoldTitle.ParaToList((string)button.Tag, "~");
			string answer = list[0];
			string answer2 = (list.Count > 1) ? list[1] : "";
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
			this.Answer1 = "";
			this.Answer2 = "";
			this.cmbSelect1.Focus();
		}

		private bool method_4()
		{
			if (this.Answer1 == "" && (this.cmbSelect1.SelectedValue == null || (string)this.cmbSelect1.SelectedValue == ""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.cmbSelect1.Focus();
				return true;
			}
			if (this.Answer2 == "" && (this.cmbSelect2.SelectedValue == null || (string)this.cmbSelect2.SelectedValue == ""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.cmbSelect2.Focus();
				return true;
			}
			if (this.Answer1 == "" && this.oQuestion.QDefine.CONTROL_MASK == "8")
			{
				DateTime date = DateTime.Now.Date;
				if (Convert.ToDateTime(this.cmbSelect1.SelectedValue.ToString() + "-" + this.cmbSelect2.SelectedValue.ToString() + "-01") > date)
				{
					MessageBox.Show(SurveyMsg.MsgNotAfterYM, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.cmbSelect1.Focus();
					return true;
				}
			}
			else if (this.Answer1 == "" && this.oQuestion.QDefine.CONTROL_MASK == "9")
			{
				DateTime date2 = DateTime.Now.Date;
				string text = date2.Year.ToString();
				if (Convert.ToDateTime(string.Concat(new string[]
				{
					text,
					"-",
					this.cmbSelect1.SelectedValue.ToString(),
					"-",
					this.cmbSelect2.SelectedValue.ToString()
				})) > date2)
				{
					MessageBox.Show(SurveyMsg.MsgNotAfterDate, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.cmbSelect1.Focus();
					return true;
				}
			}
			if (this.Answer1 == "")
			{
				this.Answer1 = (string)this.cmbSelect1.SelectedValue;
			}
			if (this.Answer2 == "")
			{
				this.Answer2 = (string)this.cmbSelect2.SelectedValue;
			}
			this.oQSingle1.SelectedCode = this.Answer1;
			this.oQSingle2.SelectedCode = this.Answer2;
			return false;
		}

		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQSingle1.QuestionName,
				CODE = this.oQSingle1.SelectedCode
			});
			SurveyHelper.Answer = this.oQSingle1.QuestionName + "=" + this.oQSingle1.SelectedCode;
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQSingle2.QuestionName,
				CODE = this.oQSingle2.SelectedCode
			});
			SurveyHelper.Answer = string.Concat(new string[]
			{
				SurveyHelper.Answer,
				",",
				this.oQSingle2.QuestionName,
				"=",
				this.oQSingle2.SelectedCode
			});
			return list;
		}

		private void method_6(List<VEAnswer> list_0)
		{
			this.oQSingle1.BeforeSave();
			this.oQSingle1.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
			this.oQSingle2.BeforeSave();
			this.oQSingle2.Save(this.MySurveyId, SurveyHelper.SurveySequence, false);
		}

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

		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

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

		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_11(string string_0)
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
			if (!this.method_12(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		private bool method_12(string string_0)
		{
			return new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$").IsMatch(string_0);
		}

		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQSingle1.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QBase oQuestion = new QBase();

		private QSingle oQSingle1 = new QSingle();

		private QSingle oQSingle2 = new QSingle();

		private string Answer1 = "";

		private string Answer2 = "";

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		private List<SurveyDetail> cmbList2Detail = new List<SurveyDetail>();

		[CompilerGenerated]
		[Serializable]
		private sealed class Class31
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			internal int method_1(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			internal int method_2(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			internal int method_3(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly P_CmbList2.Class31 instance = new P_CmbList2.Class31();

			public static Comparison<SurveyDetail> compare0;

			public static Comparison<SurveyDetail> compare1;

			public static Comparison<SurveyDetail> compare2;

			public static Comparison<SurveyDetail> compare3;
		}
	}
}
