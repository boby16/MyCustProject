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
	public partial class MultipleEntry : Page
	{
		public MultipleEntry()
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
				if (this.oQuestion.QDefine.SHOW_LOGIC == "" && this.oQuestion.QDefine.IS_RANDOM == 0)
				{
					list4.Sort(new Comparison<SurveyDetail>(MultipleEntry.Class18.instance.method_0));
				}
				this.oQuestion.QDetails = list4;
			}
			if (this.oQuestion.QDefine.PRESET_LOGIC != "")
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
			else
			{
				int is_RANDOM = this.oQuestion.QDefine.IS_RANDOM;
			}
			for (int m = 0; m < this.oQuestion.QDetails.Count<SurveyDetail>(); m++)
			{
				if (this.CodeMaxLen < this.oQuestion.QDetails[m].CODE.Length)
				{
					this.CodeMaxLen = this.oQuestion.QDetails[m].CODE.Length;
				}
			}
			this.txtSearch.MaxLength = this.CodeMaxLen;
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
			this.method_2();
			this.txtSearch.Focus();
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
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
					this.txtSearch.Text = surveyDetail4.CODE_TEXT;
					this.btnSelect_Click(this.btnSelect, new RoutedEventArgs());
				}
				if (this.txtFill.IsEnabled)
				{
					this.txtFill.Text = autoFill.CommonOther(this.oQuestion.QDefine, "");
				}
				if (list6.Count > 0 && autoFill.AutoNext(this.oQuestion.QDefine))
				{
					this.btnNav_Click(this, e);
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
					foreach (string text in this.listPreSet)
					{
						if (!this.oQuestion.SelectedValues.Contains(text))
						{
							this.oQuestion.SelectedValues.Add(text);
							foreach (SurveyDetail surveyDetail5 in this.oQuestion.QDetails)
							{
								if (surveyDetail5.CODE == text)
								{
									this.method_3(text, surveyDetail5.CODE_TEXT, surveyDetail5.IS_OTHER);
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
					if (this.method_10(surveyAnswer.QUESTION_NAME, 0, (this.oQuestion.QuestionName + "_A").Length) == this.oQuestion.QuestionName + "_A")
					{
						this.oQuestion.SelectedValues.Add(surveyAnswer.CODE);
						using (List<SurveyDetail>.Enumerator enumerator = this.oQuestion.QDetails.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail surveyDetail6 = enumerator.Current;
								if (surveyDetail6.CODE == surveyAnswer.CODE)
								{
									this.method_3(surveyAnswer.CODE, surveyDetail6.CODE_TEXT, surveyDetail6.IS_OTHER);
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
					if (this.ExistTextFill && surveyAnswer.QUESTION_NAME == this.oQuestion.QuestionName + "_OTH" && surveyAnswer.CODE != "")
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

		private void method_1(object sender = null, RoutedEventArgs e = null)
		{
			MultipleEntry.Class19 @class = new MultipleEntry.Class19();
			@class.instance = this;
			string text = this.txtSearch.Text;
			if (text == "*")
			{
				text = "";
			}
			@class.nSearch = this.method_12(text);
			if (this.SearchKey != text)
			{
				if (text == "")
				{
					this.oListSource = this.oQuestion.QDetails;
				}
				else
				{
					IOrderedEnumerable<SurveyDetail> source = this.oQuestion.QDetails.Where(new Func<SurveyDetail, bool>(@class.method_0)).OrderBy(new Func<SurveyDetail, int>(MultipleEntry.Class18.instance.method_1));
					this.oListSource = source.ToList<SurveyDetail>();
				}
				this.method_2();
				this.SearchKey = text;
			}
		}

		private void method_2()
		{
			this.ListOption.Items.Clear();
			foreach (SurveyDetail surveyDetail in this.oListSource)
			{
				this.ListOption.Items.Add(surveyDetail.CODE + "." + surveyDetail.CODE_TEXT);
			}
		}

		private void ListOption_SelectionChanged(object sender, SelectionChangedEventArgs e = null)
		{
		}

		private void txtFill_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				if (this.txtSearch.Text == "")
				{
					if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == "")
					{
						this.txtFill.Focus();
						return;
					}
					this.btnNav_Click(null, null);
					return;
				}
				else
				{
					this.btnSelect_Click(null, null);
				}
			}
		}

		private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.txtSearch.Text.Length == this.CodeMaxLen)
			{
				this.btnSelect_Click(null, null);
			}
		}

		private void btnSelect_Click(object sender = null, RoutedEventArgs e = null)
		{
			this.method_1(null, null);
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
				string text = "";
				string string_ = "";
				int num = 0;
				foreach (SurveyDetail surveyDetail in this.oListSource)
				{
					if (surveyDetail.CODE + "." + surveyDetail.CODE_TEXT == (string)this.ListOption.SelectedValue)
					{
						text = surveyDetail.CODE;
						string_ = surveyDetail.CODE_TEXT;
						num = surveyDetail.IS_OTHER;
						if (SurveyHelper.AutoFill && SurveyHelper.FillMode == "3" && this.oQuestion.QDefine.FILLDATA == "" && (num == 2 || num == 4 || num == 3 || num == 5 || num == 13 || num == 14))
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
				for (int i = 0; i < this.oQuestion.SelectedValues.Count; i++)
				{
					if (this.oQuestion.SelectedValues[i] == text)
					{
						flag = true;
						if (!flag)
						{
							this.method_3(text, string_, num);
							this.oQuestion.SelectedValues.Add(text);
						}
						this.txtSearch.Focus();
						this.txtSearch.Text = "";
						return;
					}
				}
			}
		}

		private void ListOption_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.btnSelect_Click(sender, e);
		}

		private void method_3(string string_0, string string_1, int int_0)
		{
			Style style = (Style)base.FindResource("SelBtnStyle");
			Panel panel = this.wrapPanel1;
			Button button = new Button();
			button.Name = "b_" + string_0;
			button.Content = string_0 + "." + string_1;
			button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
			button.Style = style;
			button.Tag = int_0;
			button.Click += this.method_4;
			button.FontSize = (double)this.Button_FontSize;
			button.MinWidth = (double)this.Button_Width;
			button.MinHeight = (double)this.Button_Height;
			panel.Children.Add(button);
		}

		private void method_4(object sender, RoutedEventArgs e)
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
					this.wrapPanel1.Children.Remove(button);
					this.txtSearch.Focus();
					this.txtSearch.SelectAll();
					return;
				}
			}
		}

		private bool method_5()
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
			if (this.txtFill.IsEnabled && this.txtFill.Text.Trim() == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				this.txtFill.Focus();
				return true;
			}
			if (this.txtFill.IsEnabled)
			{
				this.oQuestion.FillText = (this.txtFill.IsEnabled ? this.txtFill.Text.Trim() : "");
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

		private List<VEAnswer> method_6()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = "";
			for (int i = 0; i < this.oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer veanswer = new VEAnswer();
				veanswer.QUESTION_NAME = this.oQuestion.QuestionName + "_A" + (i + 1).ToString();
				veanswer.CODE = this.oQuestion.SelectedValues[i].ToString();
				list.Add(veanswer);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer.QUESTION_NAME,
					"=",
					veanswer.CODE
				});
			}
			SurveyHelper.Answer = this.method_10(SurveyHelper.Answer, 1, -9999);
			if (this.oQuestion.FillText != "")
			{
				VEAnswer veanswer2 = new VEAnswer();
				veanswer2.QUESTION_NAME = this.oQuestion.QuestionName + "_OTH";
				veanswer2.CODE = this.oQuestion.FillText;
				list.Add(veanswer2);
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					veanswer2.QUESTION_NAME,
					"=",
					this.oQuestion.FillText
				});
			}
			return list;
		}

		private void method_7(List<VEAnswer> list_0)
		{
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				this.oPageNav.PageDataLog(SurveyMsg.DelaySeconds, list_0, this.btnNav, this.MySurveyId);
			}
		}

		private void btnNav_Click(object sender = null, RoutedEventArgs e = null)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.method_5())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_6();
			this.oLogicEngine.PageAnswer = list;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.method_7(list);
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

		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

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

		private string method_11(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_12(string string_0)
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
			if (!this.method_13(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		private bool method_13(string string_0)
		{
			return new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$").IsMatch(string_0);
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

		private QMultiple oQuestion = new QMultiple();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

		private List<string> listOther = new List<string>();

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
		private sealed class Class18
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			internal int method_1(SurveyDetail surveyDetail_0)
			{
				return surveyDetail_0.INNER_ORDER;
			}

			public static readonly MultipleEntry.Class18 instance = new MultipleEntry.Class18();

			public static Comparison<SurveyDetail> compare0;

			public static Func<SurveyDetail, int> compare1;
		}

		[CompilerGenerated]
		private sealed class Class19
		{
			internal bool method_0(SurveyDetail surveyDetail_0)
			{
				return this.instance.method_12(surveyDetail_0.CODE) == this.nSearch;
			}

			public int nSearch;

			public MultipleEntry instance;
		}
	}
}
