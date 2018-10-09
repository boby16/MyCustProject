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
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;
using LoyalFilial.MarketResearch.QEdit;

namespace LoyalFilial.MarketResearch.View
{
	public partial class SingleEntry : Page
	{
		public SingleEntry()
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
			this.oQuestion.Init(this.CurPageId, 0, false);
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
			string show_LOGIC = this.oQuestion.QDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add("");
			if (show_LOGIC != "")
			{
				list2 = this.oBoldTitle.ParaToList(show_LOGIC, "//");
				if (list2.Count > 1)
				{
					this.oQuestion.QDefine.DETAIL_ID = this.oLogicEngine.Route(list2[1]);
				}
			}
			this.oQuestion.InitDetailID(this.CurPageId, 0);
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list3[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list3.Count > 1) ? list3[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
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
			if (this.oQuestion.QDefine.PRESET_LOGIC != "" && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == "3")))
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
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int k = 0; k < this.oQuestion.QDetails.Count<SurveyDetail>(); k++)
				{
					this.oQuestion.QDetails[k].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[k].CODE_TEXT);
				}
			}
			if (list2[0].Trim() != "")
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
				if (this.oQuestion.QDefine.NOTE == "")
				{
					this.txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					string_ = this.oQuestion.QDefine.NOTE;
					list3 = this.oBoldTitle.ParaToList(string_, "//");
					string_ = list3[0];
					this.oBoldTitle.SetTextBlock(this.txtFillTitle, string_, 0, "", true);
					if (list3.Count > 1)
					{
						string_ = list3[1];
						this.oBoldTitle.SetTextBlock(this.txtAfter, string_, 0, "", true);
					}
				}
			}
			else
			{
				this.txtFill.Height = 0.0;
				this.txtFillTitle.Height = 0.0;
				this.txtAfter.Height = 0.0;
			}
			if (this.oQuestion.QDefine.CONTROL_MASK != "")
			{
				string_ = this.oQuestion.QDefine.CONTROL_MASK;
				this.oBoldTitle.SetTextBlock(this.txtSelectTitle, string_, 0, "", true);
			}
			if (this.oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != "")
			{
				string_ = this.oQuestion.QDefine.CONTROL_TOOLTIP;
				this.oBoldTitle.SetTextBlock(this.txtSearchTitle, string_, 0, "", true);
			}
			this.oListSource = this.oQuestion.QDetails;
			this.method_11();
			this.txtSearch.Focus();
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
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
					this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, "");
					if (autoFill.AutoNext(this.oQuestion.QDefine))
					{
						this.btnNav_Click(this, e);
					}
				}
			}
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			bool flag = false;
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back"))
			{
				if (!(navOperation == "Normal"))
				{
					if (!(navOperation == "Jump"))
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
					if (this.oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && this.oQuestion.SelectedCode != "")
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
				this.txtFill.Text = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQuestion.QuestionName + "_OTH");
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

		private bool method_1()
		{
			if (this.oQuestion.SelectedCode == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : "");
			return false;
		}

		private List<VEAnswer> method_2()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			list.Add(new VEAnswer
			{
				QUESTION_NAME = this.oQuestion.QuestionName,
				CODE = this.oQuestion.SelectedCode
			});
			SurveyHelper.Answer = this.oQuestion.QuestionName + "=" + this.oQuestion.SelectedCode;
			if (this.oQuestion.FillText != "" && this.txtFill.IsEnabled)
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + "_OTH";
				veanswer.CODE = this.oQuestion.FillText;
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					", ",
					veanswer.QUESTION_NAME,
					"=",
					this.oQuestion.FillText
				});
			}
			return list;
		}

		private void method_3()
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
		}

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

		private void txtFill_LostFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void txtFill_GotFocus(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

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

		private string method_5(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

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

		private string method_7(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_8(string string_0)
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
			if (!this.method_9(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		private bool method_9(string string_0)
		{
			return new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$").IsMatch(string_0);
		}

		private void method_10(object sender = null, RoutedEventArgs e = null)
		{
			SingleEntry.Class44 @class = new SingleEntry.Class44();
			@class.instance = this;
			string text = this.txtSearch.Text;
			if (text == "*")
			{
				text = "";
			}
			@class.nSearch = this.method_8(text);
			if (this.SearchKey != text)
			{
				if (text == "")
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

		private void btnSearch_Click(object sender = null, RoutedEventArgs e = null)
		{
			this.txtSelect.Text = "";
			this.oQuestion.SelectedCode = "";
			this.method_10(null, null);
			this.txtSearch.Focus();
			this.txtSearch.SelectAll();
			if (this.ListOption.Items.Count == 0)
			{
				this.oFunc.BEEP(500, 700);
				return;
			}
			if (this.txtSearch.Text == "*")
			{
				this.txtSearch.Text = "";
				return;
			}
			if (this.txtSearch.Text != "")
			{
				this.ListOption.SelectedValue = this.ListOption.Items[0];
			}
			if (!((string)this.ListOption.SelectedValue == "") && (string)this.ListOption.SelectedValue != null)
			{
				string selectedCode = "";
				string text = "";
				foreach (SurveyDetail surveyDetail in this.oListSource)
				{
					if (surveyDetail.CODE + "." + surveyDetail.CODE_TEXT == (string)this.ListOption.SelectedValue)
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

		private void method_11()
		{
			this.ListOption.Items.Clear();
			foreach (SurveyDetail surveyDetail in this.oListSource)
			{
				this.ListOption.Items.Add(surveyDetail.CODE + "." + surveyDetail.CODE_TEXT);
			}
		}

		private void ListOption_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
			this.txtSelect.Text = (string)this.ListOption.SelectedValue;
			this.oQuestion.SelectedCode = "";
			foreach (SurveyDetail surveyDetail in this.oListSource)
			{
				if (surveyDetail.CODE + "." + surveyDetail.CODE_TEXT == this.txtSelect.Text)
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

		private void txtFill_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (this.txtFill.IsEnabled)
				{
					if (this.txtFill.Text.Trim() == "")
					{
						this.txtFill.Focus();
						return;
					}
					this.btnNav_Click(null, null);
					return;
				}
				else if (this.txtSearch.Text != "")
				{
					this.btnSearch_Click(null, null);
				}
			}
		}

		private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.txtSearch.Text.Length == this.CodeMaxLen && this.PageLoaded)
			{
				this.btnSearch_Click(null, null);
			}
		}

		private void ListOption_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.btnNav_Click(null, null);
		}

		private void btnAttach_Click(object sender, RoutedEventArgs e)
		{
			SurveyHelper.AttachSurveyId = this.MySurveyId;
			SurveyHelper.AttachQName = this.oQuestion.QuestionName;
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

		private UDPX oFunc = new UDPX();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QSingle oQuestion = new QSingle();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private int CodeMaxLen = 1;

		private List<SurveyDetail> oListSource = new List<SurveyDetail>();

		private string SearchKey = "";

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class43
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			internal int method_1(SurveyDetail surveyDetail_0)
			{
				return surveyDetail_0.INNER_ORDER;
			}

			public static readonly SingleEntry.Class43 instance = new SingleEntry.Class43();

			public static Comparison<SurveyDetail> compare0;

			public static Func<SurveyDetail, int> compare1;
		}

		[CompilerGenerated]
		private sealed class Class44
		{
			internal bool method_0(SurveyDetail surveyDetail_0)
			{
				return this.instance.method_8(surveyDetail_0.CODE) == this.nSearch;
			}

			public int nSearch;

			public SingleEntry instance;
		}
	}
}
