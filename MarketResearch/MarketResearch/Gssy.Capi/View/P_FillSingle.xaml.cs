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
using System.Windows.Input;
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
	// Token: 0x02000034 RID: 52
	public partial class P_FillSingle : Page
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00067EAC File Offset: 0x000660AC
		public P_FillSingle()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00067F34 File Offset: 0x00066134
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0);
			this.oQFill.Init(this.CurPageId, 1);
			this.oQSingle.Init(this.CurPageId, 2, true);
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
				this.oQFill.QuestionName = this.oQFill.QuestionName + this.MyNav.QName_Add;
				this.oQSingle.QuestionName = this.oQSingle.QuestionName + this.MyNav.QName_Add;
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
			if (this.oQFill.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill.MaxLength = this.oQFill.QDefine.CONTROL_TYPE;
			}
			if (this.oQFill.QDefine.CONTROL_HEIGHT != 0)
			{
				this.txtFill.Height = (double)this.oQFill.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQFill.QDefine.CONTROL_WIDTH != 0)
			{
				this.txtFill.Width = (double)this.oQFill.QDefine.CONTROL_WIDTH;
			}
			if (this.oQFill.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.txtFill.FontSize = (double)this.oQFill.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQFill.QDefine.CONTROL_TOOLTIP != global::GClass0.smethod_0(""))
			{
				string_ = this.oQFill.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore, string_, this.oQFill.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
				if (list2.Count > 1)
				{
					string_ = list2[1];
					this.oBoldTitle.SetTextBlock(this.txtAfter, string_, this.oQFill.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
				}
			}
			this.txtFill.Focus();
			if (this.oQFill.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				string_ = this.oQFill.QDefine.NOTE;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, global::GClass0.smethod_0(""), true);
				if (list2.Count > 1)
				{
					string text = global::GClass0.smethod_0("");
					int num = list2[1].IndexOf(global::GClass0.smethod_0("?"));
					if (num > 0)
					{
						text = this.method_11(list2[1], num + 1, -9999);
						num = this.method_13(this.method_9(list2[1], 1, num - 1));
					}
					else
					{
						text = list2[1];
					}
					if (this.oQFill.QDefine.GROUP_LEVEL != global::GClass0.smethod_0("") && num > 0)
					{
						this.oQFill.InitCircle();
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
							foreach (SurveyDetail surveyDetail in this.oQFill.QCircleDetails)
							{
								if (surveyDetail.CODE == text2)
								{
									text = surveyDetail.EXTEND_1;
									break;
								}
							}
						}
					}
					if (text != global::GClass0.smethod_0(""))
					{
						string text3 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + text;
						if (this.method_10(text, 1) == global::GClass0.smethod_0("\""))
						{
							text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + this.method_11(text, 1, -9999);
						}
						else if (!File.Exists(text3))
						{
							text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + text;
						}
						Image image = new Image();
						if (num > 0)
						{
							image.Height = (double)num;
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
							bitmapImage.UriSource = new Uri(text3, UriKind.RelativeOrAbsolute);
							bitmapImage.EndInit();
							image.Source = bitmapImage;
							this.NoteArea.Children.Add(image);
							this.wrapButton.Orientation = Orientation.Horizontal;
						}
						catch (Exception)
						{
						}
					}
				}
			}
			if (this.oQSingle.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
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
				string[] array = this.oLogicEngine.aryCode(this.oQSingle.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail2 in this.oQSingle.QDetails)
					{
						if (surveyDetail2.CODE == array[i].ToString())
						{
							list3.Add(surveyDetail2);
							break;
						}
					}
				}
				list3.Sort(new Comparison<SurveyDetail>(P_FillSingle.Class37.instance.method_0));
				this.oQSingle.QDetails = list3;
			}
			if (this.oQSingle.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int j = 0; j < this.oQSingle.QDetails.Count<SurveyDetail>(); j++)
				{
					this.oQSingle.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQSingle.QDetails[j].CODE_TEXT);
				}
			}
			if (this.oQSingle.QDefine.IS_RANDOM > 0)
			{
				this.oQSingle.RandomDetails();
			}
			this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
			this.Button_FontSize = ((this.oQuestion.QDefine.CONTROL_FONTSIZE == 0) ? SurveyHelper.BtnFontSize : this.oQuestion.QDefine.CONTROL_FONTSIZE);
			if (this.Button_FontSize == -1)
			{
				this.Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			this.Button_FontSize = Math.Abs(this.Button_FontSize);
			this.Button_Height = ((this.oQuestion.QDefine.CONTROL_HEIGHT == 0) ? SurveyHelper.BtnHeight : this.oQuestion.QDefine.CONTROL_HEIGHT);
			if (this.oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (this.Button_Type != 2)
				{
					if (this.Button_Type != 4)
					{
						this.Button_Width = (double)SurveyHelper.BtnWidth;
						goto IL_CFE;
					}
				}
				this.Button_Width = 440.0;
			}
			else
			{
				this.Button_Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			IL_CFE:
			this.method_2();
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				this.txtFill.Text = autoFill.Fill(this.oQFill.QDefine);
				Button button = autoFill.SingleButton(this.oQSingle.QDefine, this.listButton);
				if (button != null)
				{
					this.method_3(button, new RoutedEventArgs());
					if (autoFill.AutoNext(this.oQuestion.QDefine))
					{
						this.btnNav_Click(this, e);
					}
				}
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")))
			{
				if (!(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")) && !(navOperation == global::GClass0.smethod_0("NŶɯͱ")))
				{
				}
			}
			else
			{
				this.txtFill.Text = this.oQFill.ReadAnswerByQuestionName(this.MySurveyId, this.oQFill.QuestionName);
				this.oQSingle.SelectedCode = this.oQSingle.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle.QuestionName);
				foreach (object obj in this.wrapButton.Children)
				{
					Button button2 = (Button)obj;
					if ((string)button2.Tag == this.oQSingle.SelectedCode)
					{
						button2.Style = style;
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

		// Token: 0x06000375 RID: 885 RVA: 0x00068EDC File Offset: 0x000670DC
		private void method_1(object sender, EventArgs e)
		{
			if (this.oQuestion.QDetails != null && this.oQuestion.QDetails.Count != 0 && this.PageLoaded)
			{
				WrapPanel wrapPanel = this.wrapButton;
				ScrollViewer scrollViewer = this.scrollNote;
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
							goto IL_1F1;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					IL_1F1:
					if (this.Button_Type != 3)
					{
						if (this.Button_Type != 4)
						{
							scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
							goto IL_223;
						}
					}
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
					IL_223:
					this.PageLoaded = false;
				}
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00069128 File Offset: 0x00067328
		private void method_2()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapButton;
			foreach (SurveyDetail surveyDetail in this.oQSingle.QDetails)
			{
				Button button = new Button();
				button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = surveyDetail.CODE;
				button.Click += this.method_3;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				wrapPanel.Children.Add(button);
				this.listButton.Add(button);
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0006926C File Offset: 0x0006746C
		private void method_3(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			string selectedCode = (string)button.Tag;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				this.oQSingle.SelectedCode = selectedCode;
				foreach (object obj in this.wrapButton.Children)
				{
					Button button2 = (Button)obj;
					button2.Style = ((button2.Tag == button.Tag) ? style : style2);
				}
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00002F84 File Offset: 0x00001184
		private void method_4(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000464F0 File Offset: 0x000446F0
		private void method_5(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			TextChange[] array = new TextChange[e.Changes.Count];
			e.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				bool flag;
				if (textBox.Text.Substring(offset, array[0].AddedLength).Trim() == global::GClass0.smethod_0(""))
				{
					flag = true;
				}
				else
				{
					double num = 0.0;
					flag = !double.TryParse(textBox.Text, out num);
				}
				if (flag)
				{
					textBox.Text = textBox.Text.Remove(offset, array[0].AddedLength);
					textBox.Select(offset, 0);
				}
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00069340 File Offset: 0x00067540
		private bool method_6()
		{
			if (this.oQSingle.SelectedCode == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			string text = this.txtFill.Text;
			if (text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			text = this.oQFill.ConvertText(text, this.oQFill.QDefine.CONTROL_MASK);
			this.txtFill.Text = text;
			this.oQFill.FillText = text;
			return false;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000693F4 File Offset: 0x000675F4
		private List<VEAnswer> method_7()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQFill.QuestionName,
				CODE = this.oQFill.FillText
			});
			SurveyHelper.Answer = this.oQFill.QuestionName + global::GClass0.smethod_0("<") + this.oQFill.FillText;
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQSingle.QuestionName,
				CODE = this.oQSingle.SelectedCode
			});
			SurveyHelper.Answer = string.Concat(new string[]
			{
				SurveyHelper.Answer,
				global::GClass0.smethod_0("-"),
				this.oQSingle.QuestionName,
				global::GClass0.smethod_0("<"),
				this.oQSingle.SelectedCode
			});
			return list;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000694E0 File Offset: 0x000676E0
		private void method_8(List<VEAnswer> list_0)
		{
			this.oQSingle.BeforeSave();
			this.oQSingle.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
			this.oQFill.BeforeSave();
			this.oQFill.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00069530 File Offset: 0x00067730
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

		// Token: 0x0600037E RID: 894 RVA: 0x00069628 File Offset: 0x00067828
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

		// Token: 0x0600037F RID: 895 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x06000382 RID: 898 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x06000384 RID: 900 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_12(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00069690 File Offset: 0x00067890
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

		// Token: 0x06000386 RID: 902 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_14(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00002FA4 File Offset: 0x000011A4
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQFill.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x0400069E RID: 1694
		private string MySurveyId;

		// Token: 0x0400069F RID: 1695
		private string CurPageId;

		// Token: 0x040006A0 RID: 1696
		private NavBase MyNav = new NavBase();

		// Token: 0x040006A1 RID: 1697
		private PageNav oPageNav = new PageNav();

		// Token: 0x040006A2 RID: 1698
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040006A3 RID: 1699
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040006A4 RID: 1700
		private QBase oQuestion = new QBase();

		// Token: 0x040006A5 RID: 1701
		private QFill oQFill = new QFill();

		// Token: 0x040006A6 RID: 1702
		private QSingle oQSingle = new QSingle();

		// Token: 0x040006A7 RID: 1703
		private List<Button> listButton = new List<Button>();

		// Token: 0x040006A8 RID: 1704
		private bool PageLoaded;

		// Token: 0x040006A9 RID: 1705
		private int Button_Type;

		// Token: 0x040006AA RID: 1706
		private int Button_Height;

		// Token: 0x040006AB RID: 1707
		private double Button_Width;

		// Token: 0x040006AC RID: 1708
		private int Button_FontSize;

		// Token: 0x040006AD RID: 1709
		private double w_Height;

		// Token: 0x040006AE RID: 1710
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040006AF RID: 1711
		private int SecondsWait;

		// Token: 0x040006B0 RID: 1712
		private int SecondsCountDown;

		// Token: 0x040006B1 RID: 1713
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		[Serializable]
		private sealed class Class37
		{
			// Token: 0x06000754 RID: 1876 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D02 RID: 3330
			public static readonly P_FillSingle.Class37 instance = new P_FillSingle.Class37();

			// Token: 0x04000D03 RID: 3331
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
