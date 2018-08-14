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
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x0200003B RID: 59
	public partial class SingleEntry : Page
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x000753D8 File Offset: 0x000735D8
		public SingleEntry()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00075478 File Offset: 0x00073678
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
			if (this.oQuestion.QDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count<string>(); i++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[i].ToString())
						{
							list4.Add(surveyDetail);
							break;
						}
					}
				}
				list4.Sort(new Comparison<SurveyDetail>(SingleEntry.Class43.instance.method_0));
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
					foreach (SurveyDetail surveyDetail2 in this.oQuestion.QDetails)
					{
						if (surveyDetail2.CODE == array3[l].ToString())
						{
							list5.Add(surveyDetail2);
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
			for (int m = 0; m < this.oQuestion.QDetails.Count<SurveyDetail>(); m++)
			{
				if (this.CodeMaxLen < this.oQuestion.QDetails[m].CODE.Length)
				{
					this.CodeMaxLen = this.oQuestion.QDetails[m].CODE.Length;
				}
			}
			this.txtSearch.MaxLength = this.CodeMaxLen;
			this.ExistTextFill = false;
			foreach (SurveyDetail surveyDetail3 in this.oQuestion.QDetails)
			{
				int is_OTHER = surveyDetail3.IS_OTHER;
				if (is_OTHER != 1 && is_OTHER != 3 && is_OTHER != 5 && is_OTHER != 11 && is_OTHER != 13)
				{
					if (is_OTHER != 14)
					{
						continue;
					}
				}
				this.ExistTextFill = true;
				break;
			}
			if (this.ExistTextFill)
			{
				this.txtFill.Visibility = Visibility.Visible;
				if (this.oQuestion.QDefine.NOTE == global::GClass0.smethod_0(""))
				{
					this.txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					string_ = this.oQuestion.QDefine.NOTE;
					list3 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
					string_ = list3[0];
					this.oBoldTitle.SetTextBlock(this.txtFillTitle, string_, 0, global::GClass0.smethod_0(""), true);
					if (list3.Count > 1)
					{
						string_ = list3[1];
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
			if (this.oQuestion.QDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
			{
				string_ = this.oQuestion.QDefine.CONTROL_MASK;
				this.oBoldTitle.SetTextBlock(this.txtSelectTitle, string_, 0, global::GClass0.smethod_0(""), true);
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != global::GClass0.smethod_0(""))
			{
				string_ = this.oQuestion.QDefine.CONTROL_TOOLTIP;
				this.oBoldTitle.SetTextBlock(this.txtSearchTitle, string_, 0, global::GClass0.smethod_0(""), true);
			}
			this.oListSource = this.oQuestion.QDetails;
			this.method_11();
			this.txtSearch.Focus();
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				SurveyDetail surveyDetail4 = autoFill.SingleDetail(this.oQuestion.QDefine, this.oQuestion.QDetails);
				if (surveyDetail4 != null)
				{
					if (this.listPreSet.Count == 0)
					{
						this.oQuestion.SelectedCode = surveyDetail4.CODE;
					}
					this.txtSelect.Text = surveyDetail4.CODE_TEXT;
					this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
					if (autoFill.AutoNext(this.oQuestion.QDefine))
					{
						this.btnNav_Click(this, e);
					}
				}
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
						foreach (SurveyDetail surveyDetail5 in this.oQuestion.QDetails)
						{
							if (surveyDetail5.CODE == this.oQuestion.SelectedCode)
							{
								this.txtSelect.Text = surveyDetail5.CODE_TEXT;
								int is_OTHER2 = surveyDetail5.IS_OTHER;
								if (is_OTHER2 != 1 && is_OTHER2 != 3 && is_OTHER2 != 5 && !(is_OTHER2 == 11 | is_OTHER2 == 13))
								{
									if (is_OTHER2 != 14)
									{
										break;
									}
								}
								flag = true;
								break;
							}
						}
						if (flag)
						{
							this.txtFill.IsEnabled = true;
							this.txtFill.Background = Brushes.White;
							this.txtFill.Focus();
						}
					}
					if (this.oQuestion.QDetails.Count == 1)
					{
						if (this.listPreSet.Count == 0 && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
						{
							this.ListOption.SelectedValue = this.oQuestion.QDetails[0].CODE_TEXT;
							this.ListOption_SelectionChanged(this.ListOption, null);
						}
						if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (this.txtFill.IsEnabled)
							{
								this.txtFill.Focus();
							}
							else if (!SurveyHelper.AutoFill)
							{
								this.btnNav_Click(this, e);
							}
						}
					}
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedCode != global::GClass0.smethod_0(""))
					{
						if (this.txtFill.IsEnabled)
						{
							this.txtFill.Focus();
						}
						else if (!SurveyHelper.AutoFill)
						{
							this.btnNav_Click(this, e);
						}
					}
				}
			}
			else
			{
				this.oQuestion.SelectedCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName);
				foreach (SurveyDetail surveyDetail6 in this.oQuestion.QDetails)
				{
					if (surveyDetail6.CODE == this.oQuestion.SelectedCode)
					{
						this.txtSelect.Text = surveyDetail6.CODE_TEXT;
						this.txtSearch.Text = surveyDetail6.CODE;
						int is_OTHER3 = surveyDetail6.IS_OTHER;
						if (is_OTHER3 != 1 && is_OTHER3 != 3 && is_OTHER3 != 5 && !(is_OTHER3 == 11 | is_OTHER3 == 13))
						{
							if (is_OTHER3 != 14)
							{
								break;
							}
						}
						flag = true;
						break;
					}
				}
				this.txtFill.Text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"));
				if (flag)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
				}
				this.txtSearch.Focus();
				this.txtSearch.SelectAll();
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

		// Token: 0x06000402 RID: 1026 RVA: 0x00076548 File Offset: 0x00074748
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
			this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : global::GClass0.smethod_0(""));
			return false;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0007660C File Offset: 0x0007480C
		private List<VEAnswer> method_2()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.SelectedCode
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + global::GClass0.smethod_0("<") + this.oQuestion.SelectedCode;
			if (this.oQuestion.FillText != global::GClass0.smethod_0("") && this.txtFill.IsEnabled)
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

		// Token: 0x06000404 RID: 1028 RVA: 0x0000308D File Offset: 0x0000128D
		private void method_3()
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00076730 File Offset: 0x00074930
		private void btnNav_Click(object sender = null, RoutedEventArgs e = null)
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
			this.method_3();
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00076824 File Offset: 0x00074A24
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

		// Token: 0x06000407 RID: 1031 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000C878 File Offset: 0x0000AA78
		private string method_4(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x0600040A RID: 1034 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_5(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000C918 File Offset: 0x0000AB18
		private string method_6(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x0600040C RID: 1036 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_7(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0007688C File Offset: 0x00074A8C
		private int method_8(string string_0)
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
			if (!this.method_9(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_9(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000768E8 File Offset: 0x00074AE8
		private void method_10(object sender = null, RoutedEventArgs e = null)
		{
			SingleEntry.Class44 @class = new SingleEntry.Class44();
			@class.instance = this;
			string text = this.txtSearch.Text;
			if (text == global::GClass0.smethod_0("+"))
			{
				text = global::GClass0.smethod_0("");
			}
			@class.nSearch = this.method_8(text);
			if (this.SearchKey != text)
			{
				if (text == global::GClass0.smethod_0(""))
				{
					this.oListSource = this.oQuestion.QDetails;
				}
				else
				{
					IOrderedEnumerable<SurveyDetail> source = this.oQuestion.QDetails.Where(new Func<SurveyDetail, bool>(@class.method_0)).OrderBy(new Func<SurveyDetail, int>(SingleEntry.Class43.instance.method_1));
					this.oListSource = source.ToList<SurveyDetail>();
				}
				this.method_11();
				this.SearchKey = text;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000769C8 File Offset: 0x00074BC8
		private void btnSearch_Click(object sender = null, RoutedEventArgs e = null)
		{
			this.txtSelect.Text = global::GClass0.smethod_0("");
			this.oQuestion.SelectedCode = global::GClass0.smethod_0("");
			this.method_10(null, null);
			this.txtSearch.Focus();
			this.txtSearch.SelectAll();
			if (this.ListOption.Items.Count == 0)
			{
				this.oFunc.BEEP(500, 700);
				return;
			}
			if (this.txtSearch.Text == global::GClass0.smethod_0("+"))
			{
				this.txtSearch.Text = global::GClass0.smethod_0("");
				return;
			}
			if (this.txtSearch.Text != global::GClass0.smethod_0(""))
			{
				this.ListOption.SelectedValue = this.ListOption.Items[0];
			}
			if (!((string)this.ListOption.SelectedValue == global::GClass0.smethod_0("")) && (string)this.ListOption.SelectedValue != null)
			{
				string selectedCode = global::GClass0.smethod_0("");
				string text = global::GClass0.smethod_0("");
				foreach (SurveyDetail surveyDetail in this.oListSource)
				{
					if (surveyDetail.CODE + global::GClass0.smethod_0("/") + surveyDetail.CODE_TEXT == (string)this.ListOption.SelectedValue)
					{
						selectedCode = surveyDetail.CODE;
						this.oQuestion.SelectedCode = selectedCode;
						text = surveyDetail.CODE_TEXT;
						this.txtSelect.Text = text;
						int is_OTHER = surveyDetail.IS_OTHER;
						if (is_OTHER != 1 && is_OTHER != 3 && is_OTHER != 5 && is_OTHER != 11 && is_OTHER != 13)
						{
							if (is_OTHER != 14)
							{
								this.txtFill.IsEnabled = false;
								this.txtFill.Background = Brushes.LightGray;
								break;
							}
						}
						this.txtFill.IsEnabled = true;
						this.txtFill.Background = Brushes.White;
						this.txtFill.Focus();
						return;
					}
				}
				this.oQuestion.SelectedCode = selectedCode;
				this.txtSearch.Focus();
				this.btnNav_Click(null, null);
				return;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00076C30 File Offset: 0x00074E30
		private void method_11()
		{
			this.ListOption.Items.Clear();
			foreach (SurveyDetail surveyDetail in this.oListSource)
			{
				this.ListOption.Items.Add(surveyDetail.CODE + global::GClass0.smethod_0("/") + surveyDetail.CODE_TEXT);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00076CBC File Offset: 0x00074EBC
		private void ListOption_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			this.txtSelect.Text = (string)this.ListOption.SelectedValue;
			this.oQuestion.SelectedCode = global::GClass0.smethod_0("");
			foreach (SurveyDetail surveyDetail in this.oListSource)
			{
				if (surveyDetail.CODE + global::GClass0.smethod_0("/") + surveyDetail.CODE_TEXT == this.txtSelect.Text)
				{
					this.oQuestion.SelectedCode = surveyDetail.CODE;
					int is_OTHER = surveyDetail.IS_OTHER;
					if (is_OTHER != 1 && is_OTHER != 3 && is_OTHER != 5 && is_OTHER != 11 && is_OTHER != 13)
					{
						if (is_OTHER != 14)
						{
							this.txtFill.IsEnabled = false;
							this.txtFill.Background = Brushes.LightGray;
							break;
						}
					}
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
					this.txtFill.Focus();
					break;
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00076DE8 File Offset: 0x00074FE8
		private void txtFill_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (this.txtFill.IsEnabled)
				{
					if (this.txtFill.Text.Trim() == global::GClass0.smethod_0(""))
					{
						this.txtFill.Focus();
						return;
					}
					this.btnNav_Click(null, null);
					return;
				}
				else if (this.txtSearch.Text != global::GClass0.smethod_0(""))
				{
					this.btnSearch_Click(null, null);
				}
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000030B1 File Offset: 0x000012B1
		private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.txtSearch.Text.Length == this.CodeMaxLen && this.PageLoaded)
			{
				this.btnSearch_Click(null, null);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000030DB File Offset: 0x000012DB
		private void ListOption_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.btnNav_Click(null, null);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000030E5 File Offset: 0x000012E5
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x040007A9 RID: 1961
		private string MySurveyId;

		// Token: 0x040007AA RID: 1962
		private string CurPageId;

		// Token: 0x040007AB RID: 1963
		private NavBase MyNav = new NavBase();

		// Token: 0x040007AC RID: 1964
		private PageNav oPageNav = new PageNav();

		// Token: 0x040007AD RID: 1965
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040007AE RID: 1966
		private UDPX oFunc = new UDPX();

		// Token: 0x040007AF RID: 1967
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040007B0 RID: 1968
		private QSingle oQuestion = new QSingle();

		// Token: 0x040007B1 RID: 1969
		private bool ExistTextFill;

		// Token: 0x040007B2 RID: 1970
		private List<string> listPreSet = new List<string>();

		// Token: 0x040007B3 RID: 1971
		private bool PageLoaded;

		// Token: 0x040007B4 RID: 1972
		private int Button_Type;

		// Token: 0x040007B5 RID: 1973
		private int Button_Height;

		// Token: 0x040007B6 RID: 1974
		private int Button_Width;

		// Token: 0x040007B7 RID: 1975
		private int Button_FontSize;

		// Token: 0x040007B8 RID: 1976
		private int CodeMaxLen = 1;

		// Token: 0x040007B9 RID: 1977
		private List<SurveyDetail> oListSource = new List<SurveyDetail>();

		// Token: 0x040007BA RID: 1978
		private string SearchKey = global::GClass0.smethod_0("");

		// Token: 0x040007BB RID: 1979
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x040007BC RID: 1980
		private int SecondsWait;

		// Token: 0x040007BD RID: 1981
		private int SecondsCountDown;

		// Token: 0x040007BE RID: 1982
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000A9 RID: 169
		[CompilerGenerated]
		[Serializable]
		private sealed class Class43
		{
			// Token: 0x06000769 RID: 1897 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x0600076A RID: 1898 RVA: 0x00004413 File Offset: 0x00002613
			internal int method_1(SurveyDetail surveyDetail_0)
			{
				return surveyDetail_0.INNER_ORDER;
			}

			// Token: 0x04000D11 RID: 3345
			public static readonly SingleEntry.Class43 instance = new SingleEntry.Class43();

			// Token: 0x04000D12 RID: 3346
			public static Comparison<SurveyDetail> compare0;

			// Token: 0x04000D13 RID: 3347
			public static Func<SurveyDetail, int> compare1;
		}

		// Token: 0x020000AA RID: 170
		[CompilerGenerated]
		private sealed class Class44
		{
			// Token: 0x0600076C RID: 1900 RVA: 0x0000459F File Offset: 0x0000279F
			internal bool method_0(SurveyDetail surveyDetail_0)
			{
				return this.instance.method_8(surveyDetail_0.CODE) == this.nSearch;
			}

			// Token: 0x04000D14 RID: 3348
			public int nSearch;

			// Token: 0x04000D15 RID: 3349
			public SingleEntry instance;
		}
	}
}
