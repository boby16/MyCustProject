using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x02000039 RID: 57
	public partial class Relation1 : Page
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x00071A28 File Offset: 0x0006FC28
		public Relation1()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00071A90 File Offset: 0x0006FC90
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.txtSurvey.Text = this.MySurveyId;
			this.btnNav.Content = this.btnNav_Content;
			this.oQuestion.InitRelation(this.CurPageId, 0);
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
				list3.Sort(new Comparison<SurveyDetail>(Relation1.Class41.instance.method_0));
				this.oQuestion.QDetails = list3;
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
			{
				for (int j = 0; j < this.oQuestion.QDetails.Count<SurveyDetail>(); j++)
				{
					this.oQuestion.QDetails[j].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[j].CODE_TEXT);
				}
			}
			if (this.oQuestion.QDefine.IS_RANDOM > 0)
			{
				this.oQuestion.RandomDetails();
			}
			this.Button_Height = SurveyHelper.BtnHeight;
			this.Button_FontSize = SurveyHelper.BtnFontSize;
			this.Button_Width = SurveyHelper.BtnWidth;
			this.Button_Type = this.oQuestion.QDefine.CONTROL_TYPE;
			if (this.oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				this.Button_Height = this.oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.Button_Width = this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (this.oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				this.Button_FontSize = this.oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			this.textBlock1.Text = global::GClass0.smethod_0("");
			this.textBlock2.Text = global::GClass0.smethod_0("");
			this.cmbSelect1.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect1.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			this.cmbSelect1.IsEditable = false;
			this.cmbSelect2.IsEditable = false;
			this.btnNone.Content = this.oQuestion.NoneCodeText;
			this.btnNone.Tag = this.oQuestion.NoneCode;
			if (this.oQuestion.OtherCode != global::GClass0.smethod_0(""))
			{
				this.txtFill.Visibility = Visibility.Visible;
				if (this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0(""))
				{
					this.txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					string_ = this.oQuestion.QDefine.NOTE;
					list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
					string_ = list2[0];
					this.oBoldTitle.SetTextBlock(this.txtFillTitle, string_, 0, global::GClass0.smethod_0(""), true);
					if (list2.Count > 1)
					{
						string_ = list2[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter, string_, 0, global::GClass0.smethod_0(""), true);
					}
				}
			}
			else
			{
				this.txtFill.Height = 0.0;
				this.txtFillTitle.Height = 0.0;
				this.txtAfter.Height = 0.0;
			}
			if (this.oQuestion.QDefine.CONTROL_MASK == global::GClass0.smethod_0("0"))
			{
				this.btnNone.Visibility = Visibility.Collapsed;
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				this.cmbSelect1.SelectedValue = autoFill.SingleDetail(this.oQuestion.QDefine, this.oQuestion.QDetailsParent).CODE;
				this.cmbSelect1_SelectionChanged(this.cmbSelect1, null);
				this.cmbSelect2.SelectedValue = autoFill.SingleDetail(this.oQuestion.QDefine, this.oQuestion.QGroupDetails).CODE;
				this.cmbSelect2_SelectionChanged(this.cmbSelect2, null);
				if (this.txtFill.IsEnabled)
				{
					this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
				}
				if (autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
				}
			}
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Style style2 = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == global::GClass0.smethod_0("FŢɡͪ")))
			{
				if (!(navOperation == global::GClass0.smethod_0("HŪɶͮѣխ")) && !(navOperation == global::GClass0.smethod_0("NŶɯͱ")))
				{
				}
			}
			else
			{
				this.oQuestion.SelectedCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
				this.cmbSelect1.SelectedValue = this.oQuestion.GetParentCode(this.oQuestion.SelectedCode);
				this.oQuestion.GetRelation2(this.oQuestion.ParentCode);
				this.cmbSelect2.ItemsSource = this.oQuestion.QGroupDetails;
				this.cmbSelect2.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
				this.cmbSelect2.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
				this.cmbSelect2.SelectedValue = this.oQuestion.SelectedCode;
				this.txtFill.Text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"));
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
			this.PageLoaded = true;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00072748 File Offset: 0x00070948
		private bool method_1()
		{
			if (this.oQuestion.SelectedCode == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			if (this.txtFill.IsEnabled)
			{
				this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : global::GClass0.smethod_0(""));
			}
			return false;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00072818 File Offset: 0x00070A18
		private List<VEAnswer> method_2()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.SelectedCode
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.SelectedCode;
			if (this.oQuestion.FillText != global::GClass0.smethod_0(""))
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
				veanswer.CODE = this.oQuestion.FillText;
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0(".ġ"),
					veanswer.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					this.oQuestion.FillText
				});
			}
			return list;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0007292C File Offset: 0x00070B2C
		private void cmbSelect1_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			if (this.cmbSelect1.SelectedValue != null)
			{
				string text = this.cmbSelect1.SelectedValue.ToString();
				if (text != null && text != global::GClass0.smethod_0(""))
				{
					this.oQuestion.GetRelation2(text);
					this.cmbSelect2.ItemsSource = this.oQuestion.QGroupDetails;
					this.cmbSelect2.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
					this.cmbSelect2.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
					this.oQuestion.SelectedCode = global::GClass0.smethod_0("");
				}
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00003022 File Offset: 0x00001222
		private void cmbSelect2_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			this.method_3();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000729D0 File Offset: 0x00070BD0
		private void method_3()
		{
			if (this.cmbSelect2.SelectedValue != null)
			{
				string text = this.cmbSelect2.SelectedValue.ToString();
				this.oQuestion.SelectedCode = text;
				if (text != this.oQuestion.OtherCode)
				{
					this.txtFill.Background = Brushes.LightGray;
					this.txtFill.IsEnabled = false;
					return;
				}
				this.txtFill.IsEnabled = true;
				this.txtFill.Background = Brushes.White;
				if (this.txtFill.Text == global::GClass0.smethod_0(""))
				{
					this.txtFill.Focus();
				}
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00072A80 File Offset: 0x00070C80
		private void btnNone_Click(object sender, RoutedEventArgs e)
		{
			if (this.oQuestion.SelectedCode == this.oQuestion.NoneCode)
			{
				this.oQuestion.SelectedCode = global::GClass0.smethod_0("");
				this.btnNone.Style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
				this.cmbSelect1.IsEnabled = true;
				this.cmbSelect2.IsEnabled = true;
				this.method_3();
				return;
			}
			this.oQuestion.SelectedCode = this.oQuestion.NoneCode;
			this.btnNone.Style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			this.cmbSelect1.IsEnabled = false;
			this.cmbSelect2.IsEnabled = false;
			this.txtFill.Background = Brushes.LightGray;
			this.txtFill.IsEnabled = false;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000302A File Offset: 0x0000122A
		private void method_4()
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00072B68 File Offset: 0x00070D68
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_1())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_2();
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
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00072C5C File Offset: 0x00070E5C
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

		// Token: 0x060003DF RID: 991 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x060003E2 RID: 994 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_6(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x060003E4 RID: 996 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_8(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00072CC4 File Offset: 0x00070EC4
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

		// Token: 0x060003E6 RID: 998 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_10(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000304E File Offset: 0x0000124E
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x0400074C RID: 1868
		private string MySurveyId;

		// Token: 0x0400074D RID: 1869
		private string CurPageId;

		// Token: 0x0400074E RID: 1870
		private NavBase MyNav = new NavBase();

		// Token: 0x0400074F RID: 1871
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000750 RID: 1872
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000751 RID: 1873
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000752 RID: 1874
		private QSingle oQuestion = new QSingle();

		// Token: 0x04000753 RID: 1875
		private bool ExistTextFill;

		// Token: 0x04000754 RID: 1876
		private bool PageLoaded;

		// Token: 0x04000755 RID: 1877
		private int Button_Type;

		// Token: 0x04000756 RID: 1878
		private int Button_Height;

		// Token: 0x04000757 RID: 1879
		private int Button_Width;

		// Token: 0x04000758 RID: 1880
		private int Button_FontSize;

		// Token: 0x04000759 RID: 1881
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x0400075A RID: 1882
		private int SecondsWait;

		// Token: 0x0400075B RID: 1883
		private int SecondsCountDown;

		// Token: 0x0400075C RID: 1884
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000A7 RID: 167
		[CompilerGenerated]
		[Serializable]
		private sealed class Class41
		{
			// Token: 0x06000763 RID: 1891 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D0D RID: 3341
			public static readonly Relation1.Class41 instance = new Relation1.Class41();

			// Token: 0x04000D0E RID: 3342
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
