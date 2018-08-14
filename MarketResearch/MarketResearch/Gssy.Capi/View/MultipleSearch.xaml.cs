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
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	// Token: 0x02000023 RID: 35
	public partial class MultipleSearch : Page
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00041A2C File Offset: 0x0003FC2C
		public MultipleSearch()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00041AC4 File Offset: 0x0003FCC4
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
				if (this.oQuestion.QDefine.SHOW_LOGIC == global::GClass0.smethod_0("") && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list4.Sort(new Comparison<SurveyDetail>(MultipleSearch.Class23.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != global::GClass0.smethod_0(""))
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
			this.Button_Height = SurveyHelper.BtnHeight;
			this.Button_FontSize = SurveyHelper.BtnFontSize;
			this.Button_Width = SurveyHelper.BtnWidth;
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
			this.method_10();
			this.txtSearch.Focus();
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ") && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = this.oLogicEngine;
				List<SurveyDetail> list6 = autoFill.MultiDetail(this.oQuestion.QDefine, this.oQuestion.QDetails, 10);
				foreach (SurveyDetail surveyDetail4 in list6)
				{
					this.txtSelect.Text = surveyDetail4.CODE_TEXT;
					this.btnSelect_Click(this.btnSelect, new RoutedEventArgs());
				}
				if (this.txtFill.IsEnabled)
				{
					this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, global::GClass0.smethod_0(""));
				}
				if (list6.Count > 0 && autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
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
					foreach (string text in this.listPreSet)
					{
						if (!this.oQuestion.SelectedValues.Contains(text))
						{
							this.oQuestion.SelectedValues.Add(text);
							foreach (SurveyDetail surveyDetail5 in this.oQuestion.QDetails)
							{
								if (surveyDetail5.CODE == text)
								{
									this.method_11(text, surveyDetail5.CODE_TEXT, surveyDetail5.IS_OTHER);
									int is_OTHER2 = surveyDetail5.IS_OTHER;
									if (is_OTHER2 != 1 && is_OTHER2 != 3 && is_OTHER2 != 5 && !(is_OTHER2 == 11 | is_OTHER2 == 13))
									{
										if (is_OTHER2 != 14)
										{
											break;
										}
									}
									this.listOther.Add(text);
									flag = true;
									break;
								}
							}
						}
					}
					if (flag)
					{
						this.txtFill.IsEnabled = true;
						this.txtFill.Background = Brushes.White;
						this.txtFill.Focus();
					}
					if (this.oQuestion.QDetails.Count == 1)
					{
						if (this.oQuestion.SelectedValues.Count == 0 && (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
						{
							this.ListOption.SelectedValue = this.oQuestion.QDetails[0].CODE_TEXT;
							this.ListOption_SelectionChanged(this.ListOption, null);
							this.btnSelect_Click(null, null);
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
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedValues.Count > 0)
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
				this.oQuestion.ReadAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer surveyAnswer in this.oQuestion.QAnswersRead)
				{
					if (this.method_6(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ")).Length) == this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ"))
					{
						this.oQuestion.SelectedValues.Add(surveyAnswer.CODE);
						using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail surveyDetail6 = enumerator.Current;
								if (surveyDetail6.CODE == surveyAnswer.CODE)
								{
									this.method_11(surveyAnswer.CODE, surveyDetail6.CODE_TEXT, surveyDetail6.IS_OTHER);
									int is_OTHER3 = surveyDetail6.IS_OTHER;
									if (is_OTHER3 != 1 && is_OTHER3 != 3 && is_OTHER3 != 5 && !(is_OTHER3 == 11 | is_OTHER3 == 13))
									{
										if (is_OTHER3 != 14)
										{
											break;
										}
									}
									this.listOther.Add(surveyAnswer.CODE);
									flag = true;
									break;
								}
							}
							continue;
						}
					}
					if (this.ExistTextFill && surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉") && surveyAnswer.CODE != global::GClass0.smethod_0(""))
					{
						this.txtFill.Text = surveyAnswer.CODE;
					}
				}
				if (flag)
				{
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
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

		// Token: 0x06000203 RID: 515 RVA: 0x00042D70 File Offset: 0x00040F70
		private bool method_1()
		{
			if (this.oQuestion.SelectedValues.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.oQuestion.QDefine.MIN_COUNT != 0 && this.oQuestion.SelectedValues.Count < this.oQuestion.QDefine.MIN_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAless, this.oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.oQuestion.QDefine.MAX_COUNT != 0 && this.oQuestion.SelectedValues.Count > this.oQuestion.QDefine.MAX_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAmore, this.oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
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
			foreach (object obj in this.wrapPanel1.Children)
			{
				Button button = (Button)obj;
				int num = (int)button.Tag;
				if (this.wrapPanel1.Children.Count > 1 & (num == 2 || num == 4 || num == 3 || num == 5 || num == 13 || num == 14))
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgNotSelectOther, button.Content), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00042FB4 File Offset: 0x000411B4
		private List<VEAnswer> method_2()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = global::GClass0.smethod_0("");
			for (int i = 0; i < this.oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("]ŀ") + (i + 1).ToString();
				veanswer.CODE = this.oQuestion.SelectedValues[i].ToString();
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					veanswer.CODE
				});
			}
			SurveyHelper.Answer = this.method_6(SurveyHelper.Answer, 1, -9999);
			if (this.oQuestion.FillText != global::GClass0.smethod_0(""))
			{
				VEAnswer veanswer2 = new VEAnswer();
				veanswer2.QUESTION_NAME = this.oQuestion.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
				veanswer2.CODE = this.oQuestion.FillText;
				list.Add(veanswer2);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					veanswer2.QUESTION_NAME,
					global::GClass0.smethod_0("<"),
					this.oQuestion.FillText
				});
			}
			return list;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00043154 File Offset: 0x00041354
		private void method_3(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				this.oPageNav.PageDataLog(SurveyMsg.DelaySeconds, list_0, this.btnNav, this.MySurveyId);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000431A8 File Offset: 0x000413A8
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
			this.method_3(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000432A0 File Offset: 0x000414A0
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

		// Token: 0x06000208 RID: 520 RVA: 0x00002581 File Offset: 0x00000781
		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000259E File Offset: 0x0000079E
		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x0600020B RID: 523 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_5(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x0600020D RID: 525 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_7(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00043308 File Offset: 0x00041508
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

		// Token: 0x0600020F RID: 527 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_9(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00043364 File Offset: 0x00041564
		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			MultipleSearch.Class24 @class = new MultipleSearch.Class24();
			@class.strSearch = this.txtSearch.Text;
			@class.strPinYin = @class.strSearch.ToLower();
			if (this.SearchKey != @class.strSearch)
			{
				if (@class.strSearch == global::GClass0.smethod_0(""))
				{
					this.oListSource = this.oQuestion.QDetails;
				}
				else
				{
					IOrderedEnumerable<SurveyDetail> source = this.oQuestion.QDetails.Where(new Func<SurveyDetail, bool>(@class.method_0)).OrderBy(new Func<SurveyDetail, int>(MultipleSearch.Class23.instance.method_1));
					this.oListSource = source.ToList<SurveyDetail>();
				}
				this.method_10();
				this.SearchKey = @class.strSearch;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00043438 File Offset: 0x00041638
		private void method_10()
		{
			this.ListOption.Items.Clear();
			foreach (SurveyDetail surveyDetail in this.oListSource)
			{
				this.ListOption.Items.Add(surveyDetail.CODE_TEXT);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00002A7F File Offset: 0x00000C7F
		private void ListOption_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			this.txtSelect.Text = (string)this.ListOption.SelectedValue;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00002A9C File Offset: 0x00000C9C
		private void txtSearch_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.btnSearch_Click(sender, e);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000434AC File Offset: 0x000416AC
		private void btnSelect_Click(object sender = null, RoutedEventArgs e = null)
		{
			if (this.txtSelect.Text == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			string text = global::GClass0.smethod_0("");
			string string_ = global::GClass0.smethod_0("");
			int num = 0;
			foreach (SurveyDetail surveyDetail in this.oListSource)
			{
				if (surveyDetail.CODE_TEXT == this.txtSelect.Text)
				{
					text = surveyDetail.CODE;
					string_ = surveyDetail.CODE_TEXT;
					num = surveyDetail.IS_OTHER;
					if (SurveyHelper.AutoFill && SurveyHelper.FillMode == global::GClass0.smethod_0("2") && this.oQuestion.QDefine.FILLDATA == global::GClass0.smethod_0("") && (num == 2 || num == 4 || num == 3 || num == 5 || num == 13 || num == 14))
					{
						return;
					}
					if (num != 1 && num != 3 && num != 5 && num != 11 && num != 13)
					{
						if (num != 14)
						{
							break;
						}
					}
					if (!this.listOther.Contains(text))
					{
						this.listOther.Add(text);
					}
					this.txtFill.IsEnabled = true;
					this.txtFill.Background = Brushes.White;
					this.txtFill.Focus();
					break;
				}
			}
			bool flag = false;
			int i = 0;
			while (i < this.oQuestion.SelectedValues.Count)
			{
				if (!(this.oQuestion.SelectedValues[i] == text))
				{
					i++;
				}
				else
				{
					flag = true;
					return;
				}
            }
            if (!flag)
            {
                this.method_11(text, string_, num);
                this.oQuestion.SelectedValues.Add(text);
                return;
            }
        }

		// Token: 0x06000215 RID: 533 RVA: 0x00002AAF File Offset: 0x00000CAF
		private void ListOption_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.btnSelect_Click(sender, e);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000436A0 File Offset: 0x000418A0
		private void method_11(string string_0, string string_1, int int_0)
		{
			Style style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			Panel panel = this.wrapPanel1;
			Button button = new Button();
			button.Name = global::GClass0.smethod_0("`Ş") + string_0;
			button.Content = string_1;
			button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
			button.Style = style;
			button.Tag = int_0;
			button.Click += this.method_12;
			button.FontSize = (double)this.Button_FontSize;
			button.MinWidth = (double)this.Button_Width;
			button.MinHeight = (double)this.Button_Height;
			panel.Children.Add(button);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00043774 File Offset: 0x00041974
		private void method_12(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string text = button.Name.Substring(2);
			for (int i = 0; i < this.oQuestion.SelectedValues.Count; i++)
			{
				if (this.oQuestion.SelectedValues[i] == text)
				{
					this.oQuestion.SelectedValues.RemoveAt(i);
					if (this.listOther.Contains(text))
					{
						this.listOther.Remove(text);
						if (this.listOther.Count == 0)
						{
							this.txtFill.IsEnabled = false;
							this.txtFill.Background = Brushes.LightGray;
						}
					}
					return;
				}
            }
            this.wrapPanel1.Children.Remove(button);
        }

		// Token: 0x06000218 RID: 536 RVA: 0x00002AB9 File Offset: 0x00000CB9
		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
			SurveyHelper.AttachPageId = this.CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
		}

		// Token: 0x040003F1 RID: 1009
		private string MySurveyId;

		// Token: 0x040003F2 RID: 1010
		private string CurPageId;

		// Token: 0x040003F3 RID: 1011
		private NavBase MyNav = new NavBase();

		// Token: 0x040003F4 RID: 1012
		private PageNav oPageNav = new PageNav();

		// Token: 0x040003F5 RID: 1013
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040003F6 RID: 1014
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040003F7 RID: 1015
		private QMultiple oQuestion = new QMultiple();

		// Token: 0x040003F8 RID: 1016
		private bool ExistTextFill;

		// Token: 0x040003F9 RID: 1017
		private List<string> listPreSet = new List<string>();

		// Token: 0x040003FA RID: 1018
		private List<string> listOther = new List<string>();

		// Token: 0x040003FB RID: 1019
		private int Button_Type;

		// Token: 0x040003FC RID: 1020
		private int Button_Height;

		// Token: 0x040003FD RID: 1021
		private int Button_Width;

		// Token: 0x040003FE RID: 1022
		private int Button_FontSize;

		// Token: 0x040003FF RID: 1023
		private List<SurveyDetail> oListSource = new List<SurveyDetail>();

		// Token: 0x04000400 RID: 1024
		private string SearchKey = global::GClass0.smethod_0("");

		// Token: 0x04000401 RID: 1025
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x04000402 RID: 1026
		private int SecondsWait;

		// Token: 0x04000403 RID: 1027
		private int SecondsCountDown;

		// Token: 0x04000404 RID: 1028
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x02000095 RID: 149
		[CompilerGenerated]
		[Serializable]
		private sealed class Class23
		{
			// Token: 0x06000727 RID: 1831 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x00004413 File Offset: 0x00002613
			internal int method_1(SurveyDetail surveyDetail_0)
			{
				return surveyDetail_0.INNER_ORDER;
			}

			// Token: 0x04000CE2 RID: 3298
			public static readonly MultipleSearch.Class23 instance = new MultipleSearch.Class23();

			// Token: 0x04000CE3 RID: 3299
			public static Comparison<SurveyDetail> compare0;

			// Token: 0x04000CE4 RID: 3300
			public static Func<SurveyDetail, int> compare1;
		}

		// Token: 0x02000096 RID: 150
		[CompilerGenerated]
		private sealed class Class24
		{
			// Token: 0x0600072A RID: 1834 RVA: 0x0000448C File Offset: 0x0000268C
			internal bool method_0(SurveyDetail surveyDetail_0)
			{
				return surveyDetail_0.CODE_TEXT.IndexOf(this.strSearch) >= 0 || surveyDetail_0.EXTEND_1.IndexOf(this.strPinYin) >= 0;
			}

			// Token: 0x04000CE5 RID: 3301
			public string strSearch;

			// Token: 0x04000CE6 RID: 3302
			public string strPinYin;
		}
	}
}
