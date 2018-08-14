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
	// Token: 0x02000014 RID: 20
	public partial class FillPassword : Page
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00012F44 File Offset: 0x00011144
		public FillPassword()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00012FAC File Offset: 0x000111AC
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.Init(this.CurPageId, 0);
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
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, global::GClass0.smethod_0(""), true);
			string_ = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, global::GClass0.smethod_0(""), true);
			if (this.oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				this.txtFill.MaxLength = this.oQuestion.QDefine.CONTROL_TYPE;
			}
			if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				this.txtFill.Height = (double)this.oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.txtFill.Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_FONTSIZE > 0)
			{
				this.txtFill.FontSize = (double)this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
			{
				string_ = this.oQuestion.QDefine.CONTROL_TOOLTIP;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtBefore, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
				if (list2.Count > 1)
				{
					string_ = list2[1];
					this.oBoldTitle.SetTextBlock(this.txtAfter, string_, this.oQuestion.QDefine.CONTROL_FONTSIZE, global::GClass0.smethod_0(""), true);
				}
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
			{
				this.txtFill.Password = this.oLogicEngine.stringResult(this.oQuestion.QDefine.PRESET_LOGIC);
				this.txtFill.SelectAll();
			}
			this.txtFill.Focus();
			if (this.oQuestion.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
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
					list3.Sort(new Comparison<SurveyDetail>(FillPassword.Class7.instance.method_0));
					this.oQuestion.QDetails = list3;
				}
				if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
				{
					for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
					{
						this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
					}
				}
				this.Button_Width = (double)SurveyHelper.BtnWidth;
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
				this.method_1();
			}
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				string_ = this.oQuestion.QDefine.NOTE;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				string_ = list2[0];
				this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, global::GClass0.smethod_0(""), true);
				if (list2.Count > 1)
				{
					string text = global::GClass0.smethod_0("");
					int num = list2[1].IndexOf(global::GClass0.smethod_0("?"));
					if (num > 0)
					{
						text = this.method_8(list2[1], num + 1, -9999);
						num = this.method_10(this.method_6(list2[1], 1, num - 1));
					}
					else
					{
						text = list2[1];
					}
					if (this.oQuestion.QDefine.GROUP_LEVEL != global::GClass0.smethod_0("") && num > 0)
					{
						this.oQuestion.InitCircle();
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
							foreach (SurveyDetail surveyDetail2 in this.oQuestion.QCircleDetails)
							{
								if (surveyDetail2.CODE == text2)
								{
									text = surveyDetail2.EXTEND_1;
									break;
								}
							}
						}
					}
					if (text != global::GClass0.smethod_0(""))
					{
						string text3 = Environment.CurrentDirectory + global::GClass0.smethod_0("[ŋɠ͠Ѫգٝ") + text;
						if (this.method_7(text, 1) == global::GClass0.smethod_0("\""))
						{
							text3 = global::GClass0.smethod_0("?ľɓ͜Ѩտ٤ݿࡻ५੢୵ౙൔ๪ཡၝ") + this.method_8(text, 1, -9999);
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
						}
						catch (Exception)
						{
						}
					}
				}
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				if (this.txtFill.Password == global::GClass0.smethod_0(""))
				{
					this.txtFill.Password = autoFill.Fill(this.oQuestion.QDefine);
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
				else if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.txtFill.Password != global::GClass0.smethod_0("") && !SurveyHelper.AutoFill)
				{
					this.btnNav_Click(this, e);
				}
			}
			else
			{
				this.txtFill.Password = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
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

		// Token: 0x060000FB RID: 251 RVA: 0x00013DC4 File Offset: 0x00011FC4
		private void method_1()
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			WrapPanel wrapPanel = this.wrapOther;
			foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = global::GClass0.smethod_0("`Ş") + surveyDetail.CODE;
				button.Content = surveyDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 10.0, 10.0, 10.0);
				button.Style = style;
				button.Tag = surveyDetail.IS_OTHER;
				button.Click += this.method_2;
				button.FontSize = (double)this.Button_FontSize;
				button.MinWidth = this.Button_Width;
				button.MinHeight = (double)this.Button_Height;
				wrapPanel.Children.Add(button);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00013F00 File Offset: 0x00012100
		private void method_2(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string text = (string)button.Content;
			if ((int)button.Tag == 0)
			{
				if (this.txtFill.Password.IndexOf(text) == -1)
				{
					PasswordBox passwordBox = this.txtFill;
					passwordBox.Password += text;
				}
			}
			else
			{
				this.txtFill.Password = text;
			}
			this.txtFill.Focus();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000281F File Offset: 0x00000A1F
		private void txtFill_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00013F70 File Offset: 0x00012170
		private bool method_3()
		{
			string text = this.txtFill.Password.Trim();
			if (this.txtFill.IsEnabled)
			{
				if (text == global::GClass0.smethod_0(""))
				{
					MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill.Focus();
					return true;
				}
				text = this.oQuestion.ConvertText(text, this.oQuestion.QDefine.CONTROL_MASK);
				this.txtFill.Password = text;
			}
			this.oQuestion.FillText = text;
			return false;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00014004 File Offset: 0x00012204
		private List<VEAnswer> method_4()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.FillText
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.FillText;
			return list;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000283F File Offset: 0x00000A3F
		private void method_5()
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00014070 File Offset: 0x00012270
		private void btnNav_Click(object sender, RoutedEventArgs e)
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
				this.txtFill.SelectAll();
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

		// Token: 0x06000102 RID: 258 RVA: 0x00014170 File Offset: 0x00012370
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

		// Token: 0x06000103 RID: 259 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_6(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x06000106 RID: 262 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_7(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_8(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x06000108 RID: 264 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000141D8 File Offset: 0x000123D8
		private int method_10(string string_0)
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
			if (!this.method_11(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_11(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public string CleanString(string string_0)
		{
			return string_0.Replace(global::GClass0.smethod_0("、"), global::GClass0.smethod_0("!")).Trim().Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ').Replace(global::GClass0.smethod_0("Ａ"), global::GClass0.smethod_0("A")).Replace(global::GClass0.smethod_0("１"), global::GClass0.smethod_0("1")).Replace(global::GClass0.smethod_0("０"), global::GClass0.smethod_0("0")).Replace(global::GClass0.smethod_0("３"), global::GClass0.smethod_0("3")).Replace(global::GClass0.smethod_0("２"), global::GClass0.smethod_0("2")).Replace(global::GClass0.smethod_0("５"), global::GClass0.smethod_0("5")).Replace(global::GClass0.smethod_0("４"), global::GClass0.smethod_0("4")).Replace(global::GClass0.smethod_0("７"), global::GClass0.smethod_0("7")).Replace(global::GClass0.smethod_0("６"), global::GClass0.smethod_0("6")).Replace(global::GClass0.smethod_0("９"), global::GClass0.smethod_0("9")).Replace(global::GClass0.smethod_0("８"), global::GClass0.smethod_0("8")).Replace(global::GClass0.smethod_0("＠"), global::GClass0.smethod_0("@")).Replace(global::GClass0.smethod_0("Ｃ"), global::GClass0.smethod_0("C")).Replace(global::GClass0.smethod_0("Ｂ"), global::GClass0.smethod_0("B")).Replace(global::GClass0.smethod_0("Ｅ"), global::GClass0.smethod_0("E")).Replace(global::GClass0.smethod_0("Ｄ"), global::GClass0.smethod_0("D")).Replace(global::GClass0.smethod_0("Ｇ"), global::GClass0.smethod_0("G")).Replace(global::GClass0.smethod_0("Ｆ"), global::GClass0.smethod_0("F")).Replace(global::GClass0.smethod_0("Ｉ"), global::GClass0.smethod_0("I")).Replace(global::GClass0.smethod_0("Ｈ"), global::GClass0.smethod_0("H")).Replace(global::GClass0.smethod_0("Ｋ"), global::GClass0.smethod_0("K")).Replace(global::GClass0.smethod_0("Ｊ"), global::GClass0.smethod_0("J")).Replace(global::GClass0.smethod_0("Ｍ"), global::GClass0.smethod_0("M")).Replace(global::GClass0.smethod_0("Ｌ"), global::GClass0.smethod_0("L")).Replace(global::GClass0.smethod_0("Ｏ"), global::GClass0.smethod_0("O")).Replace(global::GClass0.smethod_0("Ｎ"), global::GClass0.smethod_0("N")).Replace(global::GClass0.smethod_0("Ｑ"), global::GClass0.smethod_0("Q")).Replace(global::GClass0.smethod_0("Ｐ"), global::GClass0.smethod_0("P")).Replace(global::GClass0.smethod_0("Ｓ"), global::GClass0.smethod_0("S")).Replace(global::GClass0.smethod_0("Ｒ"), global::GClass0.smethod_0("R")).Replace(global::GClass0.smethod_0("Ｕ"), global::GClass0.smethod_0("U")).Replace(global::GClass0.smethod_0("Ｔ"), global::GClass0.smethod_0("T")).Replace(global::GClass0.smethod_0("Ｗ"), global::GClass0.smethod_0("W")).Replace(global::GClass0.smethod_0("Ｖ"), global::GClass0.smethod_0("V")).Replace(global::GClass0.smethod_0("Ｙ"), global::GClass0.smethod_0("Y")).Replace(global::GClass0.smethod_0("Ｘ"), global::GClass0.smethod_0("X")).Replace(global::GClass0.smethod_0("［"), global::GClass0.smethod_0("[")).Replace(global::GClass0.smethod_0("｀"), global::GClass0.smethod_0("`")).Replace(global::GClass0.smethod_0("ｃ"), global::GClass0.smethod_0("c")).Replace(global::GClass0.smethod_0("ｂ"), global::GClass0.smethod_0("b")).Replace(global::GClass0.smethod_0("ｅ"), global::GClass0.smethod_0("e")).Replace(global::GClass0.smethod_0("ｄ"), global::GClass0.smethod_0("d")).Replace(global::GClass0.smethod_0("ｇ"), global::GClass0.smethod_0("g")).Replace(global::GClass0.smethod_0("ｆ"), global::GClass0.smethod_0("f")).Replace(global::GClass0.smethod_0("ｉ"), global::GClass0.smethod_0("i")).Replace(global::GClass0.smethod_0("ｈ"), global::GClass0.smethod_0("h")).Replace(global::GClass0.smethod_0("ｋ"), global::GClass0.smethod_0("k")).Replace(global::GClass0.smethod_0("ｊ"), global::GClass0.smethod_0("j")).Replace(global::GClass0.smethod_0("ｍ"), global::GClass0.smethod_0("m")).Replace(global::GClass0.smethod_0("ｌ"), global::GClass0.smethod_0("l")).Replace(global::GClass0.smethod_0("ｏ"), global::GClass0.smethod_0("o")).Replace(global::GClass0.smethod_0("ｎ"), global::GClass0.smethod_0("n")).Replace(global::GClass0.smethod_0("ｑ"), global::GClass0.smethod_0("q")).Replace(global::GClass0.smethod_0("ｐ"), global::GClass0.smethod_0("p")).Replace(global::GClass0.smethod_0("ｓ"), global::GClass0.smethod_0("s")).Replace(global::GClass0.smethod_0("ｒ"), global::GClass0.smethod_0("r")).Replace(global::GClass0.smethod_0("ｕ"), global::GClass0.smethod_0("u")).Replace(global::GClass0.smethod_0("ｔ"), global::GClass0.smethod_0("t")).Replace(global::GClass0.smethod_0("ｗ"), global::GClass0.smethod_0("w")).Replace(global::GClass0.smethod_0("ｖ"), global::GClass0.smethod_0("v")).Replace(global::GClass0.smethod_0("ｙ"), global::GClass0.smethod_0("y")).Replace(global::GClass0.smethod_0("ｘ"), global::GClass0.smethod_0("x")).Replace(global::GClass0.smethod_0("｛"), global::GClass0.smethod_0("{"));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002862 File Offset: 0x00000A62
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x04000149 RID: 329
		private string MySurveyId;

		// Token: 0x0400014A RID: 330
		private string CurPageId;

		// Token: 0x0400014B RID: 331
		private NavBase MyNav = new NavBase();

		// Token: 0x0400014C RID: 332
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400014D RID: 333
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400014E RID: 334
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400014F RID: 335
		private QFill oQuestion = new QFill();

		// Token: 0x04000150 RID: 336
		private int Button_Height;

		// Token: 0x04000151 RID: 337
		private double Button_Width;

		// Token: 0x04000152 RID: 338
		private int Button_FontSize;

		// Token: 0x04000153 RID: 339
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000154 RID: 340
		private int SecondsWait;

		// Token: 0x04000155 RID: 341
		private int SecondsCountDown;

		// Token: 0x04000156 RID: 342
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		[Serializable]
		private sealed class Class7
		{
			// Token: 0x060006ED RID: 1773 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CB7 RID: 3255
			public static readonly FillPassword.Class7 instance = new FillPassword.Class7();

			// Token: 0x04000CB8 RID: 3256
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
