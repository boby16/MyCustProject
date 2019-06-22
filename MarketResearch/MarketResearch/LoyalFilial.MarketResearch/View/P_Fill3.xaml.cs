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
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch.View
{
	public partial class P_Fill3 : Page
	{
		public P_Fill3()
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
			this.oQFill1.Init(this.CurPageId, 1);
			this.oQFill2.Init(this.CurPageId, 2);
			this.oQFill3.Init(this.CurPageId, 3);
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
				this.oQFill1.QuestionName = this.oQFill1.QuestionName + this.MyNav.QName_Add;
				this.oQFill2.QuestionName = this.oQFill2.QuestionName + this.MyNav.QName_Add;
				this.oQFill3.QuestionName = this.oQFill3.QuestionName + this.MyNav.QName_Add;
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
			if (this.oQFill1.QDefine.CONTROL_TOOLTIP != "")
			{
				string_ = this.oQFill1.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore1, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, "", true);
				string_ = ((list2.Count > 1) ? list2[1] : "");
				this.oBoldTitle.SetTextBlock(this.txtAfter1, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, "", true);
			}
			if (this.oQFill1.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill1.MaxLength = this.oQFill1.QDefine.CONTROL_TYPE;
				this.txtFill1.Width = (double)this.oQFill1.QDefine.CONTROL_TYPE * this.txtFill1.FontSize * Math.Pow(0.955, (double)this.oQFill1.QDefine.CONTROL_TYPE);
			}
			if (this.oQFill1.QDefine.CONTROL_HEIGHT != 0)
			{
				this.txtFill1.Height = (double)this.oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQFill1.QDefine.CONTROL_WIDTH != 0)
			{
				this.txtFill1.Width = (double)this.oQFill1.QDefine.CONTROL_WIDTH;
			}
			if (this.oQFill1.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.txtFill1.FontSize = (double)this.oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQFill2.QDefine.CONTROL_TOOLTIP != "")
			{
				string_ = this.oQFill2.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore2, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, "", true);
				string_ = ((list2.Count > 1) ? list2[1] : "");
				this.oBoldTitle.SetTextBlock(this.txtAfter2, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, "", true);
			}
			if (this.oQFill2.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill2.MaxLength = this.oQFill2.QDefine.CONTROL_TYPE;
				this.txtFill2.Width = (double)this.oQFill2.QDefine.CONTROL_TYPE * this.txtFill2.FontSize * Math.Pow(0.955, (double)this.oQFill2.QDefine.CONTROL_TYPE);
			}
			if (this.oQFill2.QDefine.CONTROL_HEIGHT != 0)
			{
				this.txtFill2.Height = (double)this.oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQFill2.QDefine.CONTROL_WIDTH != 0)
			{
				this.txtFill2.Width = (double)this.oQFill2.QDefine.CONTROL_WIDTH;
			}
			if (this.oQFill2.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.txtFill2.FontSize = (double)this.oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQFill3.QDefine.CONTROL_TOOLTIP != "")
			{
				string_ = this.oQFill3.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore3, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, "", true);
				string_ = ((list2.Count > 1) ? list2[1] : "");
				this.oBoldTitle.SetTextBlock(this.txtAfter3, string_, this.oQFill1.QDefine.CONTROL_FONTSIZE, "", true);
			}
			if (this.oQFill3.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill3.MaxLength = this.oQFill3.QDefine.CONTROL_TYPE;
				this.txtFill3.Width = (double)this.oQFill3.QDefine.CONTROL_TYPE * this.txtFill3.FontSize * Math.Pow(0.955, (double)this.oQFill3.QDefine.CONTROL_TYPE);
			}
			if (this.oQFill3.QDefine.CONTROL_HEIGHT != 0)
			{
				this.txtFill3.Height = (double)this.oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQFill3.QDefine.CONTROL_WIDTH != 0)
			{
				this.txtFill3.Width = (double)this.oQFill3.QDefine.CONTROL_WIDTH;
			}
			if (this.oQFill3.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.txtFill3.FontSize = (double)this.oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper() == "V")
			{
				this.wrapFill.Orientation = Orientation.Vertical;
			}
			if (this.oQFill1.QDefine.PRESET_LOGIC != "")
			{
				this.txtFill1.Text = this.oLogicEngine.stringResult(this.oQFill1.QDefine.PRESET_LOGIC);
				this.txtFill1.SelectAll();
			}
			if (this.oQFill2.QDefine.PRESET_LOGIC != "")
			{
				this.txtFill2.Text = this.oLogicEngine.stringResult(this.oQFill2.QDefine.PRESET_LOGIC);
				this.txtFill2.SelectAll();
			}
			if (this.oQFill3.QDefine.PRESET_LOGIC != "")
			{
				this.txtFill3.Text = this.oLogicEngine.stringResult(this.oQFill3.QDefine.PRESET_LOGIC);
				this.txtFill3.SelectAll();
			}
			this.txtFill1.Focus();
			if (this.oQFill1.QDefine.NOTE != "")
			{
				string_ = this.oQFill1.QDefine.NOTE;
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
					if (this.oQFill1.QDefine.GROUP_LEVEL != "" && num > 0)
					{
						this.oQFill1.InitCircle();
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
							foreach (SurveyDetail surveyDetail in this.oQFill1.QCircleDetails)
							{
								if (surveyDetail.CODE == text3)
								{
									text = surveyDetail.EXTEND_1;
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
					string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list3 = new List<SurveyDetail>();
					for (int i = 0; i < array.Count<string>(); i++)
					{
						foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
						{
							if (surveyDetail2.CODE == array[i].ToString())
							{
								list3.Add(surveyDetail2);
								break;
							}
						}
					}
					list3.Sort(new Comparison<SurveyDetail>(P_Fill3.Class33.instance.method_0));
					this.oQuestion.QDetails = list3;
				}
				if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
				{
					for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
					{
						this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
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
				if (this.txtFill1.Text == "")
				{
					this.txtFill1.Text = autoFill.FillDec(this.oQFill1.QDefine);
				}
				if (this.txtFill2.Text == "")
				{
					this.txtFill2.Text = autoFill.FillDec(this.oQFill2.QDefine);
				}
				if (this.txtFill3.Text == "")
				{
					this.txtFill3.Text = autoFill.FillDec(this.oQFill3.QDefine);
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
				else if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.txtFill1.Text != "" && this.txtFill2.Text != "" && this.txtFill3.Text != "" && !SurveyHelper.AutoFill)
				{
					this.btnNav_Click(this, e);
				}
			}
			else
			{
				this.txtFill1.Text = this.oQFill1.ReadAnswerByQuestionName(this.MySurveyId, this.oQFill1.QuestionName);
				this.txtFill2.Text = this.oQFill2.ReadAnswerByQuestionName(this.MySurveyId, this.oQFill2.QuestionName);
				this.txtFill3.Text = this.oQFill3.ReadAnswerByQuestionName(this.MySurveyId, this.oQFill3.QuestionName);
				foreach (object obj in this.wrapButton.Children)
				{
					Button button = (Button)obj;
					list2 = this.oBoldTitle.ParaToList((string)button.Tag, "~");
					string b = list2[0];
					string b2 = (list2.Count > 1) ? list2[1] : "";
					string b3 = (list2.Count > 2) ? list2[2] : "";
					if (this.txtFill1.Text == b && this.txtFill2.Text == b2 && this.txtFill3.Text == b3)
					{
						button.Style = style;
						this.txtFill1.Background = Brushes.LightGray;
						this.txtFill1.Foreground = Brushes.LightGray;
						this.txtFill2.Background = Brushes.LightGray;
						this.txtFill2.Foreground = Brushes.LightGray;
						this.txtFill3.Background = Brushes.LightGray;
						this.txtFill3.Foreground = Brushes.LightGray;
						this.txtFill1.IsEnabled = false;
						this.txtFill2.IsEnabled = false;
						this.txtFill3.IsEnabled = false;
						break;
					}
				}
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
			this.PageLoaded = true;
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
				button.Tag = string.Concat(new string[]
				{
					surveyDetail.EXTEND_1,
					"~",
					surveyDetail.EXTEND_2,
					" － ",
					surveyDetail.EXTEND_3
				});
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
			List<string> list = this.oBoldTitle.ParaToList((string)button.Tag, " － ");
			string text = list[0];
			string text2 = (list.Count > 1) ? list[1] : "";
			string text3 = (list.Count > 2) ? list[2] : "";
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				if (this.txtFill1.IsEnabled)
				{
					this.txtFill1.Tag = this.txtFill1.Text;
					this.txtFill1.Background = Brushes.LightGray;
					this.txtFill1.Foreground = Brushes.LightGray;
					this.txtFill1.IsEnabled = false;
					this.txtFill2.Tag = this.txtFill2.Text;
					this.txtFill2.Background = Brushes.LightGray;
					this.txtFill2.Foreground = Brushes.LightGray;
					this.txtFill2.IsEnabled = false;
					this.txtFill3.Tag = this.txtFill3.Text;
					this.txtFill3.Background = Brushes.LightGray;
					this.txtFill3.Foreground = Brushes.LightGray;
					this.txtFill3.IsEnabled = false;
				}
				this.txtFill1.Text = text;
				this.txtFill2.Text = text2;
				this.txtFill3.Text = text3;
				foreach(var child in this.wrapButton.Children)
				{
					{
						Button button2 = (Button)child;
						button2.Style = ((button2.Name == button.Name) ? style : style2);
					}
					return;
				}
			}
			this.txtFill1.Text = (string)this.txtFill1.Tag;
			this.txtFill1.IsEnabled = true;
			this.txtFill1.Background = Brushes.White;
			this.txtFill1.Foreground = Brushes.Black;
			this.txtFill2.Text = (string)this.txtFill2.Tag;
			this.txtFill2.IsEnabled = true;
			this.txtFill2.Background = Brushes.White;
			this.txtFill2.Foreground = Brushes.Black;
			this.txtFill3.Text = (string)this.txtFill3.Tag;
			this.txtFill3.IsEnabled = true;
			this.txtFill3.Background = Brushes.White;
			this.txtFill3.Foreground = Brushes.Black;
			button.Style = style2;
			this.txtFill1.Focus();
		}

		private void txtFill3_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
		}

		private bool method_4()
		{
			string text = this.txtFill1.Text;
			if (this.txtFill1.IsEnabled)
			{
				if (text == "")
				{
					MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill1.Focus();
					return true;
				}
				text = this.oQFill1.ConvertText(text, this.oQFill1.QDefine.CONTROL_MASK);
				this.txtFill1.Text = text;
			}
			this.oQFill1.FillText = text;
			text = this.txtFill2.Text;
			if (this.txtFill2.IsEnabled)
			{
				if (text == "")
				{
					MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill2.Focus();
					return true;
				}
				text = this.oQFill2.ConvertText(text, this.oQFill2.QDefine.CONTROL_MASK);
				this.txtFill2.Text = text;
			}
			this.oQFill2.FillText = text;
			text = this.txtFill3.Text;
			if (this.txtFill3.IsEnabled)
			{
				if (text == "")
				{
					MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill3.Focus();
					return true;
				}
				text = this.oQFill3.ConvertText(text, this.oQFill3.QDefine.CONTROL_MASK);
				this.txtFill3.Text = text;
			}
			this.oQFill3.FillText = text;
			return false;
		}

		private List<VEAnswer> method_5()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQFill1.QuestionName,
				CODE = this.oQFill1.FillText
			});
			SurveyHelper.Answer = this.oQFill1.QuestionName + "=" + this.oQFill1.FillText;
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQFill2.QuestionName,
				CODE = this.oQFill2.FillText
			});
			SurveyHelper.Answer = string.Concat(new string[]
			{
				SurveyHelper.Answer,
				",",
				this.oQFill2.QuestionName,
				"=",
				this.oQFill2.FillText
			});
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQFill3.QuestionName,
				CODE = this.oQFill3.FillText
			});
			SurveyHelper.Answer = string.Concat(new string[]
			{
				SurveyHelper.Answer,
				",",
				this.oQFill3.QuestionName,
				"=",
				this.oQFill3.FillText
			});
			return list;
		}

		private void method_6(List<VEAnswer> list_0)
		{
			this.oQFill1.BeforeSave();
			this.oQFill1.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			this.oQFill2.BeforeSave();
			this.oQFill2.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			this.oQFill3.BeforeSave();
			this.oQFill3.Save(this.MySurveyId, SurveyHelper.SurveySequence);
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

		private void txtFill3_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void txtFill3_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
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
			SurveyHelper.AttachQName = this.oQFill1.QuestionName;
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

		private QFill oQFill1 = new QFill();

		private QFill oQFill2 = new QFill();

		private QFill oQFill3 = new QFill();

		private string SelectedValue;

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class33
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly P_Fill3.Class33 instance = new P_Fill3.Class33();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
