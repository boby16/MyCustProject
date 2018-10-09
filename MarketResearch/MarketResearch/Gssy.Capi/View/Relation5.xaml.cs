using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.View
{
	public partial class Relation5 : Page
	{
		public Relation5()
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
			this.oQuestion.InitRelation(this.CurPageId, 0);
			for (int i = 0; i < this.nTotal; i++)
			{
				this.oQSingle.Add(new QSingle());
				this.oQSingle[i].Init(this.CurPageId, i + 1, true);
				this.oQSingle[i].OtherCode = "";
			}
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
				for (int j = 0; j < this.nTotal; j++)
				{
					this.oQSingle[j].QuestionName = this.oQSingle[j].QuestionName + this.MyNav.QName_Add;
				}
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
			string string_ = this.oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = this.oBoldTitle.ParaToList(string_, "//");
			string_ = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, string_, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			string_ = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, string_, 0, "", true);
			if (this.oQuestion.QDefine.LIMIT_LOGIC != "")
			{
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
				string[] array = this.oLogicEngine.aryCode(this.oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int k = 0; k < array.Count<string>(); k++)
				{
					foreach (SurveyDetail surveyDetail in this.oQuestion.QDetails)
					{
						if (surveyDetail.CODE == array[k].ToString())
						{
							list3.Add(surveyDetail);
							break;
						}
					}
				}
				list3.Sort(new Comparison<SurveyDetail>(Relation5.Class42.instance.method_0));
				this.oQuestion.QDetails = list3;
			}
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == "#")
			{
				for (int l = 0; l < this.oQuestion.QDetails.Count<SurveyDetail>(); l++)
				{
					this.oQuestion.QDetails[l].CODE_TEXT = this.oBoldTitle.ReplaceABTitle(this.oQuestion.QDetails[l].CODE_TEXT);
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
			string str = "品牌";
			string str2 = "车型";
			if (this.oQuestion.QDefine.NOTE != "" && this.oQuestion.QDefine.NOTE != null)
			{
				string_ = this.oQuestion.QDefine.NOTE;
				list2 = this.oBoldTitle.ParaToList(string_, "//");
				str = list2[0];
				if (list2.Count > 1)
				{
					str2 = list2[1];
				}
			}
			this.textBlock1_1.Text = str + "1";
			this.textBlock2_1.Text = str2 + "1";
			this.cmbSelect1_1.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_1.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect1_1.SelectedValuePath = "CODE";
			this.ListCmbParent.Add(this.cmbSelect1_1);
			this.ListCmbCode.Add(this.cmbSelect2_1);
			this.ListTxtFill.Add(this.txtFill1);
			this.ListCmbParent[0].Tag = 0;
			this.ListCmbCode[0].Tag = 0;
			this.textBlock1_2.Text = str + "2";
			this.textBlock2_2.Text = str2 + "2";
			this.cmbSelect1_2.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_2.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect1_2.SelectedValuePath = "CODE";
			this.ListCmbParent.Add(this.cmbSelect1_2);
			this.ListCmbCode.Add(this.cmbSelect2_2);
			this.ListTxtFill.Add(this.txtFill2);
			this.ListCmbParent[1].Tag = 1;
			this.ListCmbCode[1].Tag = 1;
			this.textBlock1_3.Text = str + "3";
			this.textBlock2_3.Text = str2 + "3";
			this.cmbSelect1_3.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_3.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect1_3.SelectedValuePath = "CODE";
			this.ListCmbParent.Add(this.cmbSelect1_3);
			this.ListCmbCode.Add(this.cmbSelect2_3);
			this.ListTxtFill.Add(this.txtFill3);
			this.ListCmbParent[2].Tag = 2;
			this.ListCmbCode[2].Tag = 2;
			this.textBlock1_4.Text = str + "4";
			this.textBlock2_4.Text = str2 + "4";
			this.cmbSelect1_4.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_4.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect1_4.SelectedValuePath = "CODE";
			this.ListCmbParent.Add(this.cmbSelect1_4);
			this.ListCmbCode.Add(this.cmbSelect2_4);
			this.ListTxtFill.Add(this.txtFill4);
			this.ListCmbParent[3].Tag = 3;
			this.ListCmbCode[3].Tag = 3;
			this.textBlock1_5.Text = str + "5";
			this.textBlock2_5.Text = str2 + "5";
			this.cmbSelect1_5.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_5.DisplayMemberPath = "CODE_TEXT";
			this.cmbSelect1_5.SelectedValuePath = "CODE";
			this.ListCmbParent.Add(this.cmbSelect1_5);
			this.ListCmbCode.Add(this.cmbSelect2_5);
			this.ListTxtFill.Add(this.txtFill5);
			this.ListCmbParent[4].Tag = 4;
			this.ListCmbCode[4].Tag = 4;
			this.btnNone.Content = this.oQuestion.NoneCodeText;
			this.btnNone.Tag = this.oQuestion.NoneCode;
			Style style = (Style)base.FindResource("SelBtnStyle");
			Style style2 = (Style)base.FindResource("UnSelBtnStyle");
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == "Back"))
			{
				if (!(navOperation == "Normal") && !(navOperation == "Jump"))
				{
				}
			}
			else
			{
				for (int m = 0; m < this.nTotal; m++)
				{
					this.oQSingle[m].SelectedCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle[m].QuestionName);
					this.oQSingle[m].OtherCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle[m].QuestionName + "_OTH");
					this.method_1(m, this.oQSingle[m].SelectedCode, this.oQSingle[m].OtherCode);
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
			this.PageLoaded = true;
		}

		private string method_1(int int_0, string string_0, string string_1)
		{
			if (string_0 != null && string_0 != "")
			{
				this.oQSingle[int_0].SelectedCode = string_0;
				this.ListCmbParent[int_0].SelectedValue = this.oQuestion.GetParentCode(string_0);
				this.oQuestion.GetRelation2(this.oQuestion.ParentCode);
				this.ListCmbCode[int_0].ItemsSource = this.oQuestion.QGroupDetails;
				this.ListCmbCode[int_0].DisplayMemberPath = "CODE_TEXT";
				this.ListCmbCode[int_0].SelectedValuePath = "CODE";
				this.ListCmbCode[int_0].SelectedValue = string_0;
				this.oQSingle[int_0].SelectedCode = string_0;
				if (string_0 != this.oQSingle[int_0].OtherCode)
				{
					this.oQSingle[int_0].OtherCode = "";
					this.ListTxtFill[int_0].Background = Brushes.LightGray;
					this.ListTxtFill[int_0].IsEnabled = false;
					this.ListTxtFill[int_0].Text = "";
				}
				else
				{
					this.oQSingle[int_0].OtherCode = string_1;
					this.ListTxtFill[int_0].IsEnabled = true;
					this.ListTxtFill[int_0].Background = Brushes.White;
					this.ListTxtFill[int_0].Text = string_1;
				}
				return this.ListCmbParent[int_0].SelectedValue.ToString();
			}
			return "";
		}

		private void cmbSelect1_5_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = (int)((ComboBox)sender).Tag;
			if (this.ListCmbParent[index].SelectedValue != null)
			{
				string text = this.ListCmbParent[index].SelectedValue.ToString();
				if (text != null && text != "")
				{
					this.oQuestion.GetRelation2(text);
					this.ListCmbCode[index].ItemsSource = this.oQuestion.QGroupDetails;
					this.ListCmbCode[index].DisplayMemberPath = "CODE_TEXT";
					this.ListCmbCode[index].SelectedValuePath = "CODE";
					this.oQSingle[index].SelectedCode = "";
					this.oQSingle[index].OtherCode = this.oQuestion.OtherCode;
				}
			}
		}

		private void cmbSelect2_5_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = (int)((ComboBox)sender).Tag;
			if (this.ListCmbCode[index].SelectedValue != null)
			{
				string text = this.ListCmbCode[index].SelectedValue.ToString();
				this.oQSingle[index].SelectedCode = text;
				if (text != this.oQSingle[index].OtherCode)
				{
					this.ListTxtFill[index].Background = Brushes.LightGray;
					this.ListTxtFill[index].IsEnabled = false;
					return;
				}
				this.ListTxtFill[index].IsEnabled = true;
				this.ListTxtFill[index].Background = Brushes.White;
				if (this.ListTxtFill[index].Text == "")
				{
					this.ListTxtFill[index].Focus();
				}
			}
		}

		private void btnNone_Click(object sender, RoutedEventArgs e)
		{
			if (this.oQuestion.SelectedCode == this.oQuestion.NoneCode)
			{
				this.oQuestion.SelectedCode = "";
				this.btnNone.Style = (Style)base.FindResource("UnSelBtnStyle");
				for (int i = 0; i < this.nTotal; i++)
				{
					this.ListCmbParent[i].IsEnabled = true;
					this.ListCmbCode[i].IsEnabled = true;
					this.ListTxtFill[i].IsEnabled = true;
					this.ListTxtFill[i].Background = Brushes.White;
				}
				return;
			}
			this.oQuestion.SelectedCode = this.oQuestion.NoneCode;
			this.btnNone.Style = (Style)base.FindResource("SelBtnStyle");
			for (int j = 0; j < this.nTotal; j++)
			{
				this.ListCmbParent[j].IsEnabled = false;
				this.ListCmbCode[j].IsEnabled = false;
				this.ListTxtFill[j].Background = Brushes.LightGray;
				this.ListTxtFill[j].IsEnabled = false;
			}
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			if ((string)this.btnNav.Content != this.btnNav_Content)
			{
				return;
			}
			this.btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (this.oQuestion.SelectedCode == this.oQuestion.NoneCode)
			{
				this.oQSingle[0].SelectedCode = this.oQuestion.NoneCode;
				this.oQSingle[0].FillText = "";
				for (int i = 1; i < this.nTotal; i++)
				{
					this.oQSingle[i].SelectedCode = "";
					this.oQSingle[i].FillText = "";
				}
			}
			else
			{
				int num = 0;
				for (int j = 0; j < this.nTotal; j++)
				{
					if (this.ListCmbParent[j].Text.Trim() != "" && (this.ListCmbCode[j].SelectedValue == "" || this.ListCmbCode[j].SelectedValue == null))
					{
						MessageBox.Show(SurveyMsg.MsgNotSelectClassB, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.btnNav.Content = this.btnNav_Content;
						this.ListCmbCode[j].Focus();
						return;
					}
					if ((string)this.ListCmbCode[j].SelectedValue != "" && this.ListCmbCode[j].SelectedValue != null)
					{
						this.oQSingle[num].SelectedCode = this.ListCmbCode[j].SelectedValue.ToString();
						this.oQSingle[num].FillText = this.ListTxtFill[j].Text;
						if (this.ListTxtFill[j].IsEnabled && this.ListTxtFill[j].Text == "")
						{
							MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							this.btnNav.Content = this.btnNav_Content;
							this.ListTxtFill[j].Focus();
							return;
						}
						num++;
					}
				}
				for (int k = num; k < this.nTotal; k++)
				{
					this.oQSingle[k].SelectedCode = "";
					this.oQSingle[k].FillText = "";
				}
			}
			if (this.method_2())
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			List<VEAnswer> list = this.method_3();
			this.oLogicEngine.PageAnswer = list;
			if (!this.oLogicEngine.CheckLogic(this.CurPageId))
			{
				if (this.oLogicEngine.IS_ALLOW_PASS == 0)
				{
					MessageBox.Show(this.oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					this.btnNav.Content = this.btnNav_Content;
					return;
				}
				if (MessageBox.Show(this.oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
				{
					this.btnNav.Content = this.btnNav_Content;
					return;
				}
			}
			foreach (QSingle qsingle in this.oQSingle)
			{
				qsingle.BeforeSave();
				qsingle.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
			}
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			this.MyNav.PageAnswer = list;
			this.method_4();
		}

		private bool method_2()
		{
			if (this.oQSingle[0].SelectedCode == "")
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			int i = 0;
			while (i < this.nTotal - 1)
			{
				if (!(this.oQSingle[i].SelectedCode == ""))
				{
					int num = i + 1;
					while (num < this.nTotal && !(this.oQSingle[num].SelectedCode == ""))
					{
						if (this.oQSingle[i].SelectedCode == this.oQSingle[num].SelectedCode)
						{
							MessageBox.Show(SurveyMsg.MsgSelectRepeat, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							this.ListCmbCode[num].Focus();
							return true;
						}
						num++;
					}
					i++;
				}
				else
				{
					int num2 = 0;
					int num3 = 0;
					while (num3 < this.nTotal - 1 && !(this.oQSingle[num3].SelectedCode == ""))
					{
						num2++;
						num3++;
					}
					if (num2 < this.oQuestion.QDefine.MIN_COUNT && this.oQuestion.QDefine.MIN_COUNT > 0)
					{
						MessageBox.Show(string.Format("至少需要选择 {0} 个选项。", this.oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					if (num2 > this.oQuestion.QDefine.MAX_COUNT && this.oQuestion.QDefine.MAX_COUNT > 0)
					{
						MessageBox.Show(string.Format("最多可以选择 {0} 个选项。", this.oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					return false;
				}
			}
            return false;
		}

		private List<VEAnswer> method_3()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = "";
			foreach (QSingle qsingle in this.oQSingle)
			{
				list.Add(new VEAnswer
				{
					QUESTION_NAME = qsingle.QuestionName,
					CODE = qsingle.SelectedCode
				});
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					qsingle.QuestionName,
					"=",
					qsingle.SelectedCode
				});
				if (qsingle.FillText != "")
				{
					VEAnswer veanswer = new VEAnswer();
					veanswer.QUESTION_NAME = qsingle.QuestionName + "_OTH";
					veanswer.CODE = qsingle.FillText;
					list.Add(veanswer);
					SurveyHelper.Answer = string.Concat(new string[]
					{
						SurveyHelper.Answer,
						",",
						veanswer.QUESTION_NAME,
						"=",
						qsingle.FillText
					});
				}
			}
			SurveyHelper.Answer = this.method_10(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		private void method_4()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.PageStartTime = SurveyHelper.PageStartTime;
			this.MyNav.RecordFileName = SurveyHelper.RecordFileName;
			this.MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			if (this.MyNav.GroupLevel == "")
			{
				this.MyNav.NextPage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
			}
			else
			{
				this.MyNav.NextCirclePage(this.MySurveyId, surveySequence, this.CurPageId, roadMapVersion);
				SurveyHelper.CircleACount = this.MyNav.CircleACount;
				SurveyHelper.CircleACurrent = this.MyNav.CircleACurrent;
				if (this.MyNav.IsLastA && (this.MyNav.GroupPageType == 0 || this.MyNav.GroupPageType == 2))
				{
					SurveyHelper.CircleACode = "";
					SurveyHelper.CircleACodeText = "";
				}
				if (this.MyNav.GroupLevel == "B")
				{
					SurveyHelper.CircleBCount = this.MyNav.CircleBCount;
					SurveyHelper.CircleBCurrent = this.MyNav.CircleBCurrent;
					if (this.MyNav.IsLastB && (this.MyNav.GroupPageType == 10 || this.MyNav.GroupPageType == 12 || this.MyNav.GroupPageType == 30 || this.MyNav.GroupPageType == 32))
					{
						SurveyHelper.CircleBCode = "";
						SurveyHelper.CircleBCodeText = "";
					}
				}
			}
			string text = this.oLogicEngine.Route(this.MyNav.RoadMap.FORM_NAME);
			SurveyHelper.RoadMapVersion = this.MyNav.RoadMap.VERSION_ID.ToString();
			string uriString = string.Format("pack://application:,,,/View/{0}.xaml", text);
			if (text.Substring(0, 1) == "A")
			{
				uriString = string.Format("pack://application:,,,/ViewProject/{0}.xaml", text);
			}
			if (text == SurveyHelper.CurPageName)
			{
				base.NavigationService.Refresh();
			}
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = "Normal";
			SurveyHelper.NavLoad = 0;
		}

		private void method_5(int int_0, List<VEAnswer> list_0)
		{
			this.btnNav.IsEnabled = false;
			this.btnNav.Content = "正在保存,请稍候...";
			foreach (VEAnswer veanswer in list_0)
			{
				Logging.Data.WriteLog(this.MySurveyId, veanswer.QUESTION_NAME + "," + veanswer.CODE);
			}
			Thread.Sleep(int_0);
			this.btnNav.Content = this.btnNav_Content;
			this.btnNav.IsEnabled = true;
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

		private void method_6(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == "IsTouch_true")
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void method_7(object sender, RoutedEventArgs e)
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

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private int nTotal = 5;

		private QSingle oQuestion = new QSingle();

		private List<QSingle> oQSingle = new List<QSingle>();

		private List<ComboBox> ListCmbParent = new List<ComboBox>();

		private List<ComboBox> ListCmbCode = new List<ComboBox>();

		private List<TextBox> ListTxtFill = new List<TextBox>();

		private bool ExistTextFill;

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class42
		{
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			public static readonly Relation5.Class42 instance = new Relation5.Class42();

			public static Comparison<SurveyDetail> compare0;
		}
	}
}
