using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x02000012 RID: 18
	public partial class FillLong : Page
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x0001078C File Offset: 0x0000E98C
		public FillLong()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000107F4 File Offset: 0x0000E9F4
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
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
			{
				this.txtFill.Text = this.oLogicEngine.stringResult(this.oQuestion.QDefine.PRESET_LOGIC);
				this.txtFill.SelectAll();
			}
			this.txtFill.Focus();
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
					list3.Sort(new Comparison<SurveyDetail>(FillLong.Class6.instance.method_0));
					this.oQuestion.QDetails = list3;
				}
				if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
				{
					for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
					{
						this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
					}
				}
				this.Button_Width = 280.0;
				this.Button_Height = SurveyHelper.BtnHeight;
				this.Button_FontSize = SurveyHelper.BtnFontSize;
				this.method_1();
			}
			string_ = this.oQuestion.QDefine.NOTE;
			this.oBoldTitle.SetTextBlock(this.txtQuestionNote, string_, 0, global::GClass0.smethod_0(""), true);
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				if (this.txtFill.Text == global::GClass0.smethod_0(""))
				{
					this.txtFill.Text = autoFill.Fill(this.oQuestion.QDefine);
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
				else if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.txtFill.Text != global::GClass0.smethod_0("") && !SurveyHelper.AutoFill)
				{
					this.btnNav_Click(this, e);
				}
			}
			else
			{
				this.txtFill.Text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
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

		// Token: 0x060000CB RID: 203 RVA: 0x00011270 File Offset: 0x0000F470
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

		// Token: 0x060000CC RID: 204 RVA: 0x000113AC File Offset: 0x0000F5AC
		private void method_2(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string text = (string)button.Content;
			if ((int)button.Tag == 0)
			{
				if (this.txtFill.Text == global::GClass0.smethod_0(""))
				{
					this.txtFill.Text = text;
				}
				else if (this.txtFill.Text.IndexOf(text) == -1)
				{
					TextBox textBox = this.txtFill;
					textBox.Text = textBox.Text + global::GClass0.smethod_0("－") + text;
				}
			}
			else if (this.txtFill.Text != text)
			{
				if (this.txtFill.Text.Trim() == global::GClass0.smethod_0(""))
				{
					this.txtFill.Text = text;
				}
				else if (MessageBox.Show(string.Format(SurveyMsg.MsgConfirmReplace, text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
				{
					this.txtFill.Text = text;
				}
			}
			this.txtFill.Focus();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000114B4 File Offset: 0x0000F6B4
		private bool method_3()
		{
			string text = this.txtFill.Text.Trim();
			if (this.txtFill.IsEnabled && text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			this.oQuestion.FillText = text;
			return false;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00011520 File Offset: 0x0000F720
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

		// Token: 0x060000CF RID: 207 RVA: 0x00002716 File Offset: 0x00000916
		private void method_5()
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002739 File Offset: 0x00000939
		private void method_6(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return && this.btnNav.IsEnabled)
			{
				this.btnNav_Click(sender, e);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0001158C File Offset: 0x0000F78C
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

		// Token: 0x060000D2 RID: 210 RVA: 0x00011680 File Offset: 0x0000F880
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

		// Token: 0x060000D3 RID: 211 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x060000D6 RID: 214 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x060000D8 RID: 216 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_10(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002759 File Offset: 0x00000959
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x0400010D RID: 269
		private string MySurveyId;

		// Token: 0x0400010E RID: 270
		private string CurPageId;

		// Token: 0x0400010F RID: 271
		private NavBase MyNav = new NavBase();

		// Token: 0x04000110 RID: 272
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000111 RID: 273
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000112 RID: 274
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000113 RID: 275
		private QFill oQuestion = new QFill();

		// Token: 0x04000114 RID: 276
		private int Button_Type;

		// Token: 0x04000115 RID: 277
		private int Button_Height;

		// Token: 0x04000116 RID: 278
		private double Button_Width;

		// Token: 0x04000117 RID: 279
		private int Button_FontSize;

		// Token: 0x04000118 RID: 280
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000119 RID: 281
		private int SecondsWait;

		// Token: 0x0400011A RID: 282
		private int SecondsCountDown;

		// Token: 0x0400011B RID: 283
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x02000084 RID: 132
		[CompilerGenerated]
		[Serializable]
		private sealed class Class6
		{
			// Token: 0x060006EA RID: 1770 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000CB5 RID: 3253
			public static readonly FillLong.Class6 instance = new FillLong.Class6();

			// Token: 0x04000CB6 RID: 3254
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
