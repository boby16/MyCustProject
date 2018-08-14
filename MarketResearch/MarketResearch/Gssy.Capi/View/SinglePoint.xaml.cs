using System;
using System.CodeDom.Compiler;
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
	// Token: 0x02000040 RID: 64
	public partial class SinglePoint : Page
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00080054 File Offset: 0x0007E254
		public SinglePoint()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000800D0 File Offset: 0x0007E2D0
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0, false);
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
			string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add(global::GClass0.smethod_0(""));
			if (show_LOGIC != global::GClass0.smethod_0(""))
			{
				list2 = this.oBoldTitle.ParaToList(show_LOGIC, global::GClass0.smethod_0("-Į"));
				if (list2.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list2[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list3[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list3.Count > 1) ? list3[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QDefine.GROUP_LEVEL != global::GClass0.smethod_0(""))
			{
				this.oQuestion.InitCircle();
			}
			string text = global::GClass0.smethod_0("");
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
			{
				text = this.oLogicEngine.Route(this.oQuestion.QDefine.CONTROL_TOOLTIP);
			}
			else if (this.oQuestion.QDefine.GROUP_LEVEL != global::GClass0.smethod_0("") && this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
			{
				string text2 = global::GClass0.smethod_0("");
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("@"))
				{
					text2 = this.MyNav.CircleACode;
				}
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					text2 = this.MyNav.CircleBCode;
				}
				if (text2 != global::GClass0.smethod_0(""))
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail.CODE == text2)
						{
							text = this.oLogicEngine.Route(surveyDetail.EXTEND_1);
							break;
						}
					}
				}
			}
			if (text != global::GClass0.smethod_0(""))
			{
				string text3 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + text;
				if (this.method_9(text, 1) == global::GClass0.smethod_0("\""))
				{
					text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + this.method_10(text, 1, -9999);
				}
				else if (!File.Exists(text3))
				{
					text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + text;
				}
				Image image = new Image();
				if (!(this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("+")) && !(this.oQuestion.QDefine.CONTROL_MASK.Trim() == global::GClass0.smethod_0("")) && this.oQuestion.QDefine.CONTROL_MASK != null)
				{
					string string_2 = this.oQuestion.QDefine.CONTROL_MASK;
					if (this.method_9(string_2, 1) == global::GClass0.smethod_0("\""))
					{
						string_2 = this.method_10(string_2, 1, -9999);
						this.scrollPic.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						this.scrollPic.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						int num = this.method_12(string_2);
						if (num > 0)
						{
							this.scrollPic.Height = (double)num;
						}
					}
					else
					{
						int num2 = this.method_12(string_2);
						if (num2 > 0)
						{
							image.Height = (double)num2;
						}
					}
				}
				else
				{
					this.PicHeight.Height = new GridLength(1.0, GridUnitType.Star);
					this.ButtonHeight.Height = GridLength.Auto;
				}
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				image.HorizontalAlignment = HorizontalAlignment.Center;
				image.VerticalAlignment = VerticalAlignment.Center;
				try
				{
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.UriSource = new Uri(text3, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					this.scrollPic.Content = image;
				}
				catch (Exception)
				{
				}
			}
			string text4 = this.oQuestion.QDefine.NOTE;
			if (text4 == global::GClass0.smethod_0(""))
			{
				string text5 = global::GClass0.smethod_0("");
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("@"))
				{
					text5 = this.MyNav.CircleACode;
				}
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					text5 = this.MyNav.CircleBCode;
				}
				if (text5 != global::GClass0.smethod_0(""))
				{
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleDetails)
					{
						if (surveyDetail2.CODE == text5)
						{
							text4 = surveyDetail2.EXTEND_2;
							break;
						}
					}
				}
			}
			list3 = new List<string>(text4.Split(new string[]
			{
				global::GClass0.smethod_0("-Į")
			}, StringSplitOptions.RemoveEmptyEntries));
			this.txt1.Text = global::GClass0.smethod_0("");
			this.txt3.Text = global::GClass0.smethod_0("");
			this.txt4.Text = global::GClass0.smethod_0("");
			this.txt5.Text = global::GClass0.smethod_0("");
			this.txt6.Text = global::GClass0.smethod_0("");
			this.txt7.Text = global::GClass0.smethod_0("");
			this.txt9.Text = global::GClass0.smethod_0("");
			if (list3.Count == 2)
			{
				this.txt1.Text = list3[0];
				this.txt9.Text = list3[1];
			}
			else if (list3.Count == 3)
			{
				this.txt1.Text = list3[0];
				this.txt5.Text = list3[1];
				this.txt9.Text = list3[2];
			}
			else if (list3.Count == 4)
			{
				this.txt1.Text = list3[0];
				this.txt4.Text = list3[1];
				this.txt6.Text = list3[2];
				this.txt9.Text = list3[3];
			}
			else if (list3.Count == 5)
			{
				this.txt1.Text = list3[0];
				this.txt3.Text = list3[1];
				this.txt5.Text = list3[2];
				this.txt7.Text = list3[3];
				this.txt9.Text = list3[4];
			}
			if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				this.txt1.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				this.txt3.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				this.txt4.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				this.txt5.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				this.txt6.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				this.txt7.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
				this.txt9.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail3 in this.oQuestion.QDetails)
					{
						if (surveyDetail3.CODE == array[i].ToString())
						{
							list4.Add(surveyDetail3);
							break;
						}
					}
				}
				if (this.oQuestion.QDefine.SHOW_LOGIC == global::GClass0.smethod_0("") && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list4.Sort(new Comparison<SurveyDetail>(SinglePoint.Class50.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0("") && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == global::GClass0.smethod_0("2"))))
			{
				string[] array2 = this.oLogicEngine.aryCode(this.oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int j = 0; j < array2.Count<string>(); j++)
				{
					using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								this.listPreSet.Add(array2[j]);
								break;
							}
						}
					}
				}
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int k = 0; k < this.oQuestion.QDetails.Count<SurveyDetail>(); k++)
				{
					this.oQuestion.QDetails[k].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[k].CODE_TEXT);
				}
			}
			if (list2[0].Trim() != global::GClass0.smethod_0(""))
			{
				string[] array3 = this.oLogicEngine.aryCode(list2[0], ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count<string>(); l++)
				{
					foreach (SurveyDetail surveyDetail4 in this.oQuestion.QDetails)
					{
						if (surveyDetail4.CODE == array3[l].ToString())
						{
							list5.Add(surveyDetail4);
							break;
						}
					}
				}
				this.oQuestion.QDetails = list5;
			}
			else if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails();
			}
			double num3 = (double)((this.oQuestion.QDetails.Count - 1) * 2 + 80) / 1024.0;
			this.Button_Height = SurveyHelper.BtnHeight;
			this.Button_FontSize = SurveyHelper.BtnFontSize;
			int num4 = 0;
			using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IS_OTHER == 0)
					{
						num4++;
					}
				}
			}
			if (num4 > 0)
			{
				this.Button_Width = (this.GridContent.ActualWidth - (double)(num4 * 4)) / (double)num4;
			}
			else
			{
				this.Button_Width = this.GridContent.ActualWidth;
			}
			int control_TYPE = this.oQuestion.QDefine.CONTROL_TYPE;
			if (control_TYPE != 1)
			{
				if (control_TYPE != 20)
				{
					if (control_TYPE != 30)
					{
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
					else
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
			if (this.Button_FontSize == -1)
			{
				this.Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			this.Button_FontSize = Math.Abs(this.Button_FontSize);
			this.method_1();
			if (this.wrapOther.Children.Count == 0)
			{
				this.wrapOther.Height = 0.0;
				this.wrapOther.Visibility = Visibility.Collapsed;
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			bool flag = false;
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")))
			{
				if (!(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")))
				{
					if (!(navOperation == global::GClass0.smethod_0("NŶɯͱ")))
					{
					}
				}
				else
				{
					if (this.listPreSet.Count > 0)
					{
						this.oQuestion.SelectedCode = this.listPreSet[0];
						foreach (object obj in this.wrapPanel1.Children)
						{
							Button button = (Button)obj;
							if ((string)button.Tag == this.oQuestion.SelectedCode)
							{
								button.Style = style;
								break;
							}
						}
						foreach (object obj2 in this.wrapOther.Children)
						{
							Button button2 = (Button)obj2;
							if ((string)button2.Tag == this.oQuestion.SelectedCode)
							{
								button2.Style = style;
								break;
							}
						}
					}
					if (this.oQuestion.QDetails.Count == 1)
					{
						if (this.listPreSet.Count == 0 && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
						{
							this.method_2(this.listButton[0], null);
						}
						if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							flag = true;
						}
					}
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedCode != global::GClass0.smethod_0(""))
					{
						flag = true;
					}
					if (SurveyHelper.AutoFill)
					{
						AutoFill autoFill = new AutoFill();
						autoFill.oLogicEngine = this.oLogicEngine;
						if (this.oQuestion.SelectedCode == global::GClass0.smethod_0(""))
						{
							Button button3 = autoFill.SingleButton(this.oQuestion.QDefine, this.listButton);
							if (button3 != null && this.listPreSet.Count == 0 && button3.Style == style2)
							{
								this.method_2(button3, null);
							}
						}
						if (this.oQuestion.SelectedCode != global::GClass0.smethod_0("") && !flag && autoFill.AutoNext(this.oQuestion.QDefine))
						{
							flag = true;
						}
					}
					if (flag)
					{
						this.btnNav_Click(this, e);
					}
				}
			}
			else
			{
				this.oQuestion.SelectedCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
				foreach (object obj3 in this.wrapPanel1.Children)
				{
					Button button4 = (Button)obj3;
					if ((string)button4.Tag == this.oQuestion.SelectedCode)
					{
						button4.Style = style;
						break;
					}
				}
				foreach (object obj4 in this.wrapOther.Children)
				{
					Button button5 = (Button)obj4;
					if ((string)button5.Tag == this.oQuestion.SelectedCode)
					{
						button5.Style = style;
						break;
					}
				}
			}
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
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

		// Token: 0x0600046F RID: 1135 RVA: 0x000818C0 File Offset: 0x0007FAC0
		private void method_1()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapPanel1;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(2.0, 2.0, 2.0, 2.0);
				button.Style = style;
				button.Tag = surveyDetail.CODE;
				button.Click += this.method_2;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				if (surveyDetail.IS_OTHER == 0)
				{
					wrapPanel.Children.Add(button);
				}
				else
				{
					this.wrapOther.Children.Add(button);
				}
				this.listButton.Add(button);
				if (this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("@") || this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C"))
				{
					string item = global::GClass0.smethod_0("");
					if (this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("@"))
					{
						item = SurveyHelper.CircleACode;
					}
					else if (this.oQuestion.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C"))
					{
						item = SurveyHelper.CircleBCode;
					}
					if (surveyDetail.IS_OTHER == 2)
					{
						button.Visibility = Visibility.Hidden;
						if (surveyDetail.EXTEND_2 != global::GClass0.smethod_0("") && this.oBoldTitle.ParaToList(surveyDetail.EXTEND_2, global::GClass0.smethod_0("-Į")).Contains(item))
						{
							button.Visibility = Visibility.Visible;
						}
					}
					else if (surveyDetail.IS_OTHER == 3 && surveyDetail.EXTEND_3 != global::GClass0.smethod_0("") && this.oBoldTitle.ParaToList(surveyDetail.EXTEND_3, global::GClass0.smethod_0("-Į")).Contains(item))
					{
						button.Visibility = Visibility.Hidden;
					}
				}
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00081B70 File Offset: 0x0007FD70
		private void method_2(object sender, RoutedEventArgs e = null)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			string text = (string)button.Tag;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				this.oQuestion.SelectedCode = text;
				foreach (object obj in this.wrapPanel1.Children)
				{
					Button button2 = (Button)obj;
					string a = (string)button2.Tag;
					button2.Style = ((a == text) ? style : style2);
				}
				foreach (object obj2 in this.wrapOther.Children)
				{
					Button button3 = (Button)obj2;
					string a2 = (string)button3.Tag;
					button3.Style = ((a2 == text) ? style : style2);
				}
			}
			if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode4) && this.oQuestion.SelectedCode != global::GClass0.smethod_0(""))
			{
				this.btnNav_Click(this, e);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000328F File Offset: 0x0000148F
		private bool method_3()
		{
			if (this.oQuestion.SelectedCode == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			return false;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00081CF8 File Offset: 0x0007FEF8
		private List<VEAnswer> method_4()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.SelectedCode
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.SelectedCode;
			return list;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000032C3 File Offset: 0x000014C3
		private void method_5()
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00081D64 File Offset: 0x0007FF64
		private void btnNav_Click(object sender = null, RoutedEventArgs e = null)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_3())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_4();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_5();
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00081E58 File Offset: 0x00080058
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

		// Token: 0x06000476 RID: 1142 RVA: 0x00002581 File Offset: 0x00000781
		private void method_6(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000259E File Offset: 0x0000079E
		private void method_7(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_8(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x06000479 RID: 1145 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_10(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x0600047B RID: 1147 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_11(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00081EC0 File Offset: 0x000800C0
		private int method_12(string string_0)
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
			if (!this.method_13(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_13(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000032E7 File Offset: 0x000014E7
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000856 RID: 2134
		private string MySurveyId;

		// Token: 0x04000857 RID: 2135
		private string CurPageId;

		// Token: 0x04000858 RID: 2136
		private NavBase MyNav = new NavBase();

		// Token: 0x04000859 RID: 2137
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400085A RID: 2138
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400085B RID: 2139
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400085C RID: 2140
		private QSingle oQuestion = new QSingle();

		// Token: 0x0400085D RID: 2141
		private List<string> listPreSet = new List<string>();

		// Token: 0x0400085E RID: 2142
		private List<Button> listButton = new List<Button>();

		// Token: 0x0400085F RID: 2143
		private int Button_Type;

		// Token: 0x04000860 RID: 2144
		private int Button_Height;

		// Token: 0x04000861 RID: 2145
		private double Button_Width;

		// Token: 0x04000862 RID: 2146
		private int Button_FontSize;

		// Token: 0x04000863 RID: 2147
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000864 RID: 2148
		private int SecondsWait;

		// Token: 0x04000865 RID: 2149
		private int SecondsCountDown;

		// Token: 0x04000866 RID: 2150
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		[Serializable]
		private sealed class Class50
		{
			// Token: 0x06000780 RID: 1920 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D26 RID: 3366
			public static readonly SinglePoint.Class50 instance = new SinglePoint.Class50();

			// Token: 0x04000D27 RID: 3367
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
