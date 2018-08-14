using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.Entities.BaiduJson;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x02000013 RID: 19
	public partial class FillMap : Page
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00011860 File Offset: 0x0000FA60
		public FillMap()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00011904 File Offset: 0x0000FB04
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
				this.txtFill.Text = this.oLogicEngine.stringResult(this.oQuestion.QDefine.PRESET_LOGIC);
				this.txtFill.SelectAll();
			}
			this.txtFill.Focus();
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
					this.SecondsCountDown = 2;
					this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
					this.timer.Tick += this.timer_Tick_1;
					this.timer.Start();
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
				this.lng = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + global::GClass0.smethod_0("Xŋɤʹяլ٦"));
				this.lat = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + global::GClass0.smethod_0("Xŋɤʹяգٵ"));
			}
			this.SecondsWait = this.oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (this.SecondsWait > 0 && !SurveyHelper.AutoFill)
			{
				this.SecondsCountDown = this.SecondsWait;
				this.btnNav.Foreground = Brushes.Gray;
				this.btnNav.Content = this.SecondsCountDown.ToString();
				this.timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				this.timer.Tick += this.timer_Tick;
				this.timer.Start();
			}
			this.PageLoaded = 1;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00012244 File Offset: 0x00010444
		private void method_1(object sender, EventArgs e)
		{
			if (this.PageLoaded == 2)
			{
				if (this.txtFill.Text != global::GClass0.smethod_0(""))
				{
					this.SecondsCountDownSearch = 500;
					this.timerSearch.Interval = TimeSpan.FromMilliseconds(100.0);
					this.timerSearch.Tick += this.timerSearch_Tick;
					this.timerSearch.Start();
				}
				this.PageLoaded++;
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			}
			if (this.PageLoaded == 1)
			{
				if (this.oQuestion.QDefine.DETAIL_ID == global::GClass0.smethod_0(""))
				{
					this.strCity = this.oLogicEngine.stringResult(global::GClass0.smethod_0("!Ś") + this.oQuestion.QDefine.PARENT_CODE + global::GClass0.smethod_0("\\"));
				}
				else
				{
					this.strCity = this.oLogicEngine.stringResult(string.Concat(new string[]
					{
						global::GClass0.smethod_0(".Ŋɇ̓уՑفݛࡖऩ"),
						this.oQuestion.QDefine.DETAIL_ID,
						global::GClass0.smethod_0(";"),
						this.oQuestion.QDefine.PARENT_CODE,
						global::GClass0.smethod_0("(")
					}));
				}
				this.method_12((int)this.scrollNote.ActualWidth, (int)this.RowNote.ActualHeight);
				this.PageLoaded++;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002798 File Offset: 0x00000998
		private void txtFill_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSearch_Click(sender, e);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000123E0 File Offset: 0x000105E0
		private bool method_2()
		{
			string text = this.txtFill.Text.Trim();
			if (this.txtFill.IsEnabled)
			{
				if (text == global::GClass0.smethod_0(""))
				{
					System.Windows.MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.txtFill.Focus();
					return true;
				}
				text = this.oQuestion.ConvertText(text, this.oQuestion.QDefine.CONTROL_MASK);
				this.txtFill.Text = text;
			}
			if (!this.SearchClick)
			{
				System.Windows.MessageBox.Show(SurveyMsg.MsgNotSearch, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			this.method_13();
			if ((this.lng == global::GClass0.smethod_0("") || this.lat == global::GClass0.smethod_0("")) && System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgNoLngLat, text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
			{
				this.txtFill.Focus();
				return true;
			}
			this.oQuestion.FillText = text;
			return false;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00012510 File Offset: 0x00010710
		private List<VEAnswer> method_3()
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

		// Token: 0x060000E2 RID: 226 RVA: 0x0001257C File Offset: 0x0001077C
		private void method_4()
		{
			string text = this.method_11();
			if (text == global::GClass0.smethod_0(""))
			{
				text = SurveyMsg.MsgCaptureFail;
			}
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, this.oQuestion.QuestionName + global::GClass0.smethod_0("Xŋɤʹѓի٢"), text, DateTime.Now);
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, this.oQuestion.QuestionName + global::GClass0.smethod_0("Xŋɤʹяլ٦"), this.lng, DateTime.Now);
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, this.oQuestion.QuestionName + global::GClass0.smethod_0("Xŋɤʹяգٵ"), this.lat, DateTime.Now);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00012678 File Offset: 0x00010878
		private void btnNav_Click(object sender = null, RoutedEventArgs e = null)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_2())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_3();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_4();
			if (SurveyHelper.Debug)
			{
				System.Windows.MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0001276C File Offset: 0x0001096C
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

		// Token: 0x060000E5 RID: 229 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_5(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x060000E8 RID: 232 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_6(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_7(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x060000EA RID: 234 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000127D4 File Offset: 0x000109D4
		private int method_9(string string_0)
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
			if (!this.method_10(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_10(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public string CleanString(string string_0)
		{
			return string_0.Replace(global::GClass0.smethod_0("、"), global::GClass0.smethod_0("!")).Trim().Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ').Replace(global::GClass0.smethod_0("Ａ"), global::GClass0.smethod_0("A")).Replace(global::GClass0.smethod_0("１"), global::GClass0.smethod_0("1")).Replace(global::GClass0.smethod_0("０"), global::GClass0.smethod_0("0")).Replace(global::GClass0.smethod_0("３"), global::GClass0.smethod_0("3")).Replace(global::GClass0.smethod_0("２"), global::GClass0.smethod_0("2")).Replace(global::GClass0.smethod_0("５"), global::GClass0.smethod_0("5")).Replace(global::GClass0.smethod_0("４"), global::GClass0.smethod_0("4")).Replace(global::GClass0.smethod_0("７"), global::GClass0.smethod_0("7")).Replace(global::GClass0.smethod_0("６"), global::GClass0.smethod_0("6")).Replace(global::GClass0.smethod_0("９"), global::GClass0.smethod_0("9")).Replace(global::GClass0.smethod_0("８"), global::GClass0.smethod_0("8")).Replace(global::GClass0.smethod_0("＠"), global::GClass0.smethod_0("@")).Replace(global::GClass0.smethod_0("Ｃ"), global::GClass0.smethod_0("C")).Replace(global::GClass0.smethod_0("Ｂ"), global::GClass0.smethod_0("B")).Replace(global::GClass0.smethod_0("Ｅ"), global::GClass0.smethod_0("E")).Replace(global::GClass0.smethod_0("Ｄ"), global::GClass0.smethod_0("D")).Replace(global::GClass0.smethod_0("Ｇ"), global::GClass0.smethod_0("G")).Replace(global::GClass0.smethod_0("Ｆ"), global::GClass0.smethod_0("F")).Replace(global::GClass0.smethod_0("Ｉ"), global::GClass0.smethod_0("I")).Replace(global::GClass0.smethod_0("Ｈ"), global::GClass0.smethod_0("H")).Replace(global::GClass0.smethod_0("Ｋ"), global::GClass0.smethod_0("K")).Replace(global::GClass0.smethod_0("Ｊ"), global::GClass0.smethod_0("J")).Replace(global::GClass0.smethod_0("Ｍ"), global::GClass0.smethod_0("M")).Replace(global::GClass0.smethod_0("Ｌ"), global::GClass0.smethod_0("L")).Replace(global::GClass0.smethod_0("Ｏ"), global::GClass0.smethod_0("O")).Replace(global::GClass0.smethod_0("Ｎ"), global::GClass0.smethod_0("N")).Replace(global::GClass0.smethod_0("Ｑ"), global::GClass0.smethod_0("Q")).Replace(global::GClass0.smethod_0("Ｐ"), global::GClass0.smethod_0("P")).Replace(global::GClass0.smethod_0("Ｓ"), global::GClass0.smethod_0("S")).Replace(global::GClass0.smethod_0("Ｒ"), global::GClass0.smethod_0("R")).Replace(global::GClass0.smethod_0("Ｕ"), global::GClass0.smethod_0("U")).Replace(global::GClass0.smethod_0("Ｔ"), global::GClass0.smethod_0("T")).Replace(global::GClass0.smethod_0("Ｗ"), global::GClass0.smethod_0("W")).Replace(global::GClass0.smethod_0("Ｖ"), global::GClass0.smethod_0("V")).Replace(global::GClass0.smethod_0("Ｙ"), global::GClass0.smethod_0("Y")).Replace(global::GClass0.smethod_0("Ｘ"), global::GClass0.smethod_0("X")).Replace(global::GClass0.smethod_0("［"), global::GClass0.smethod_0("[")).Replace(global::GClass0.smethod_0("｀"), global::GClass0.smethod_0("`")).Replace(global::GClass0.smethod_0("ｃ"), global::GClass0.smethod_0("c")).Replace(global::GClass0.smethod_0("ｂ"), global::GClass0.smethod_0("b")).Replace(global::GClass0.smethod_0("ｅ"), global::GClass0.smethod_0("e")).Replace(global::GClass0.smethod_0("ｄ"), global::GClass0.smethod_0("d")).Replace(global::GClass0.smethod_0("ｇ"), global::GClass0.smethod_0("g")).Replace(global::GClass0.smethod_0("ｆ"), global::GClass0.smethod_0("f")).Replace(global::GClass0.smethod_0("ｉ"), global::GClass0.smethod_0("i")).Replace(global::GClass0.smethod_0("ｈ"), global::GClass0.smethod_0("h")).Replace(global::GClass0.smethod_0("ｋ"), global::GClass0.smethod_0("k")).Replace(global::GClass0.smethod_0("ｊ"), global::GClass0.smethod_0("j")).Replace(global::GClass0.smethod_0("ｍ"), global::GClass0.smethod_0("m")).Replace(global::GClass0.smethod_0("ｌ"), global::GClass0.smethod_0("l")).Replace(global::GClass0.smethod_0("ｏ"), global::GClass0.smethod_0("o")).Replace(global::GClass0.smethod_0("ｎ"), global::GClass0.smethod_0("n")).Replace(global::GClass0.smethod_0("ｑ"), global::GClass0.smethod_0("q")).Replace(global::GClass0.smethod_0("ｐ"), global::GClass0.smethod_0("p")).Replace(global::GClass0.smethod_0("ｓ"), global::GClass0.smethod_0("s")).Replace(global::GClass0.smethod_0("ｒ"), global::GClass0.smethod_0("r")).Replace(global::GClass0.smethod_0("ｕ"), global::GClass0.smethod_0("u")).Replace(global::GClass0.smethod_0("ｔ"), global::GClass0.smethod_0("t")).Replace(global::GClass0.smethod_0("ｗ"), global::GClass0.smethod_0("w")).Replace(global::GClass0.smethod_0("ｖ"), global::GClass0.smethod_0("v")).Replace(global::GClass0.smethod_0("ｙ"), global::GClass0.smethod_0("y")).Replace(global::GClass0.smethod_0("ｘ"), global::GClass0.smethod_0("x")).Replace(global::GClass0.smethod_0("｛"), global::GClass0.smethod_0("{"));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000027AB File Offset: 0x000009AB
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00012830 File Offset: 0x00010A30
		private string method_11()
		{
			string text = this.MySurveyId + global::GClass0.smethod_0("^") + this.CurPageId + global::GClass0.smethod_0("*ũɲͦ");
			string result = text;
			text = Directory.GetCurrentDirectory() + global::GClass0.smethod_0("[Ŗɭͫѷխٝ") + text;
			if (!new ScreenCapture().Capture(text, (int)SurveyHelper.Screen_LeftTop))
			{
				System.Windows.MessageBox.Show(SurveyMsg.MsgNotCapture, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return global::GClass0.smethod_0("");
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000027EA File Offset: 0x000009EA
		private void c_webBrowser_NewWindow(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000128B0 File Offset: 0x00010AB0
		private void method_12(int int_0 = 1280, int int_1 = 600)
		{
			string urlString = Environment.CurrentDirectory + global::GClass0.smethod_0("8Œɴ͠ѲԽٸݾࡻॢਢ୊ౢ൦๥ཅၦᅶራ፬ᑷᕯ᙭");
			this.c_webBrowser.Width = int_0;
			this.c_webBrowser.Height = int_1;
			this.c_webBrowser.ScriptErrorsSuppressed = true;
			this.c_webBrowser.Navigate(urlString);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00012904 File Offset: 0x00010B04
		private void method_13()
		{
			this.lng = this.c_webBrowser.Document.All[global::GClass0.smethod_0("hŠɳͯѬզ")].GetAttribute(global::GClass0.smethod_0("sťɯͷѤ"));
			this.lat = this.c_webBrowser.Document.All[global::GClass0.smethod_0("hŠɳͯѣյ")].GetAttribute(global::GClass0.smethod_0("sťɯͷѤ"));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0001297C File Offset: 0x00010B7C
		private void btnSearch_Click(object sender = null, RoutedEventArgs e = null)
		{
			string text = global::GClass0.smethod_0("3Ĺ");
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				text = this.oQuestion.QDefine.NOTE;
			}
			this.txtFill.Text = this.oQuestion.ConvertText(this.txtFill.Text, this.oQuestion.QDefine.CONTROL_MASK);
			string text2 = this.txtFill.Text;
			if (text2.Trim() == global::GClass0.smethod_0(""))
			{
				System.Windows.MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return;
			}
			this.SearchClick = true;
			JGeocoding geocodingFromAddress = new BaiduMapHelper().GetGeocodingFromAddress(this.strCity, this.strCity + text2);
			if (geocodingFromAddress.status == 0)
			{
				this.PanelConnet.Visibility = Visibility.Collapsed;
				this.scrollNote.Visibility = Visibility.Visible;
				this.c_webBrowser.Document.InvokeScript(global::GClass0.smethod_0("|ŨɡͤѼլٗݨࡰॠ੶୯ౣ൸"));
				this.c_webBrowser.Document.InvokeScript(global::GClass0.smethod_0("Můɮ̈́ѧձ٣ݕ࡫४੬୵"), new object[]
				{
					geocodingFromAddress.result.location.lng,
					geocodingFromAddress.result.location.lat,
					text2,
					text
				});
				return;
			}
			this.PanelConnet.Visibility = Visibility.Visible;
			this.scrollNote.Visibility = Visibility.Collapsed;
			System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgMapNotFound, this.strCity + global::GClass0.smethod_0("；") + text2, geocodingFromAddress.status.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			this.txtFill.Focus();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00012B58 File Offset: 0x00010D58
		private void timerSearch_Tick(object sender, EventArgs e)
		{
			if (this.SecondsCountDownSearch != 0)
			{
				this.SecondsCountDownSearch -= 100;
				return;
			}
			this.timerSearch.Stop();
			if (!(this.lng == global::GClass0.smethod_0("")) && !(this.lat == global::GClass0.smethod_0("")))
			{
				this.method_14();
				return;
			}
			this.btnSearch_Click(null, null);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00012BC8 File Offset: 0x00010DC8
		private void method_14()
		{
			string text = global::GClass0.smethod_0("3Ĺ");
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0(""))
			{
				text = this.oQuestion.QDefine.NOTE;
			}
			string text2 = this.txtFill.Text;
			this.SearchClick = true;
			JGeocoding geocodingFromAddress = new BaiduMapHelper().GetGeocodingFromAddress(this.strCity, this.strCity + text2);
			if (geocodingFromAddress.status == 0)
			{
				this.PanelConnet.Visibility = Visibility.Collapsed;
				this.scrollNote.Visibility = Visibility.Visible;
				this.c_webBrowser.Document.InvokeScript(global::GClass0.smethod_0("|ŨɡͤѼլٗݨࡰॠ੶୯ౣ൸"));
				this.c_webBrowser.Document.InvokeScript(global::GClass0.smethod_0("Můɮ̈́ѧձ٣ݕ࡫४੬୵"), new object[]
				{
					this.lng,
					this.lat,
					text2,
					text
				});
				return;
			}
			this.PanelConnet.Visibility = Visibility.Visible;
			this.scrollNote.Visibility = Visibility.Collapsed;
			System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgMapNotFound, this.strCity + global::GClass0.smethod_0("；") + text2, geocodingFromAddress.status.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			this.txtFill.Focus();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000027F3 File Offset: 0x000009F3
		private void timer_Tick_1(object sender, EventArgs e)
		{
			if (this.SecondsCountDown == 0)
			{
				this.timer.Stop();
				this.btnNav_Click(null, null);
				return;
			}
			this.SecondsCountDown--;
		}

		// Token: 0x04000127 RID: 295
		private string MySurveyId;

		// Token: 0x04000128 RID: 296
		private string CurPageId;

		// Token: 0x04000129 RID: 297
		private NavBase MyNav = new NavBase();

		// Token: 0x0400012A RID: 298
		private PageNav oPageNav = new PageNav();

		// Token: 0x0400012B RID: 299
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x0400012C RID: 300
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x0400012D RID: 301
		private QFill oQuestion = new QFill();

		// Token: 0x0400012E RID: 302
		private int PageLoaded;

		// Token: 0x0400012F RID: 303
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000130 RID: 304
		private int SecondsWait;

		// Token: 0x04000131 RID: 305
		private int SecondsCountDown;

		// Token: 0x04000132 RID: 306
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x04000133 RID: 307
		private DispatcherTimer timerSearch = new DispatcherTimer();

		// Token: 0x04000134 RID: 308
		private int SecondsCountDownSearch;

		// Token: 0x04000135 RID: 309
		private string strCity = global::GClass0.smethod_0("");

		// Token: 0x04000136 RID: 310
		private string lng = global::GClass0.smethod_0("");

		// Token: 0x04000137 RID: 311
		private string lat = global::GClass0.smethod_0("");

		// Token: 0x04000138 RID: 312
		private bool SearchClick;
	}
}
