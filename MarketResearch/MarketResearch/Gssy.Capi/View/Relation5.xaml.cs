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
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x0200003A RID: 58
	public partial class Relation5 : Page
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x00072F30 File Offset: 0x00071130
		public Relation5()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00072FC0 File Offset: 0x000711C0
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
				this.oQSingle[i].OtherCode = global::GClass0.smethod_0("");
			}
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
			if (this.oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == global::GClass0.smethod_0("\""))
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
			string str = global::GClass0.smethod_0("哃獍");
			string str2 = global::GClass0.smethod_0("轤嚊");
			if (this.oQuestion.QDefine.NOTE != global::GClass0.smethod_0("") && this.oQuestion.QDefine.NOTE != null)
			{
				string_ = this.oQuestion.QDefine.NOTE;
				list2 = this.oBoldTitle.ParaToList(string_, global::GClass0.smethod_0("-Į"));
				str = list2[0];
				if (list2.Count > 1)
				{
					str2 = list2[1];
				}
			}
			this.textBlock1_1.Text = str + global::GClass0.smethod_0("0");
			this.textBlock2_1.Text = str2 + global::GClass0.smethod_0("0");
			this.cmbSelect1_1.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_1.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect1_1.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			this.ListCmbParent.Add(this.cmbSelect1_1);
			this.ListCmbCode.Add(this.cmbSelect2_1);
			this.ListTxtFill.Add(this.txtFill1);
			this.ListCmbParent[0].Tag = 0;
			this.ListCmbCode[0].Tag = 0;
			this.textBlock1_2.Text = str + global::GClass0.smethod_0("3");
			this.textBlock2_2.Text = str2 + global::GClass0.smethod_0("3");
			this.cmbSelect1_2.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_2.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect1_2.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			this.ListCmbParent.Add(this.cmbSelect1_2);
			this.ListCmbCode.Add(this.cmbSelect2_2);
			this.ListTxtFill.Add(this.txtFill2);
			this.ListCmbParent[1].Tag = 1;
			this.ListCmbCode[1].Tag = 1;
			this.textBlock1_3.Text = str + global::GClass0.smethod_0("2");
			this.textBlock2_3.Text = str2 + global::GClass0.smethod_0("2");
			this.cmbSelect1_3.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_3.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect1_3.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			this.ListCmbParent.Add(this.cmbSelect1_3);
			this.ListCmbCode.Add(this.cmbSelect2_3);
			this.ListTxtFill.Add(this.txtFill3);
			this.ListCmbParent[2].Tag = 2;
			this.ListCmbCode[2].Tag = 2;
			this.textBlock1_4.Text = str + global::GClass0.smethod_0("5");
			this.textBlock2_4.Text = str2 + global::GClass0.smethod_0("5");
			this.cmbSelect1_4.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_4.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect1_4.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			this.ListCmbParent.Add(this.cmbSelect1_4);
			this.ListCmbCode.Add(this.cmbSelect2_4);
			this.ListTxtFill.Add(this.txtFill4);
			this.ListCmbParent[3].Tag = 3;
			this.ListCmbCode[3].Tag = 3;
			this.textBlock1_5.Text = str + global::GClass0.smethod_0("4");
			this.textBlock2_5.Text = str2 + global::GClass0.smethod_0("4");
			this.cmbSelect1_5.ItemsSource = this.oQuestion.QDetailsParent;
			this.cmbSelect1_5.DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
			this.cmbSelect1_5.SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
			this.ListCmbParent.Add(this.cmbSelect1_5);
			this.ListCmbCode.Add(this.cmbSelect2_5);
			this.ListTxtFill.Add(this.txtFill5);
			this.ListCmbParent[4].Tag = 4;
			this.ListCmbCode[4].Tag = 4;
			this.btnNone.Content = this.oQuestion.NoneCodeText;
			this.btnNone.Tag = this.oQuestion.NoneCode;
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
				for (int m = 0; m < this.nTotal; m++)
				{
					this.oQSingle[m].SelectedCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle[m].QuestionName);
					this.oQSingle[m].OtherCode = this.oQuestion.ReadAnswerByQuestionName(this.MySurveyId, this.oQSingle[m].QuestionName + global::GClass0.smethod_0("[Ōɖ͉"));
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

		// Token: 0x060003EC RID: 1004 RVA: 0x00073EDC File Offset: 0x000720DC
		private string method_1(int int_0, string string_0, string string_1)
		{
			if (string_0 != null && string_0 != global::GClass0.smethod_0(""))
			{
				this.oQSingle[int_0].SelectedCode = string_0;
				this.ListCmbParent[int_0].SelectedValue = this.oQuestion.GetParentCode(string_0);
				this.oQuestion.GetRelation2(this.oQuestion.ParentCode);
				this.ListCmbCode[int_0].ItemsSource = this.oQuestion.QGroupDetails;
				this.ListCmbCode[int_0].DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
				this.ListCmbCode[int_0].SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
				this.ListCmbCode[int_0].SelectedValue = string_0;
				this.oQSingle[int_0].SelectedCode = string_0;
				if (string_0 != this.oQSingle[int_0].OtherCode)
				{
					this.oQSingle[int_0].OtherCode = global::GClass0.smethod_0("");
					this.ListTxtFill[int_0].Background = Brushes.LightGray;
					this.ListTxtFill[int_0].IsEnabled = false;
					this.ListTxtFill[int_0].Text = global::GClass0.smethod_0("");
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
			return global::GClass0.smethod_0("");
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000740A8 File Offset: 0x000722A8
		private void cmbSelect1_5_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = (int)((ComboBox)sender).Tag;
			if (this.ListCmbParent[index].SelectedValue != null)
			{
				string text = this.ListCmbParent[index].SelectedValue.ToString();
				if (text != null && text != global::GClass0.smethod_0(""))
				{
					this.oQuestion.GetRelation2(text);
					this.ListCmbCode[index].ItemsSource = this.oQuestion.QGroupDetails;
					this.ListCmbCode[index].DisplayMemberPath = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
					this.ListCmbCode[index].SelectedValuePath = global::GClass0.smethod_0("GŌɆ̈́");
					this.oQSingle[index].SelectedCode = global::GClass0.smethod_0("");
					this.oQSingle[index].OtherCode = this.oQuestion.OtherCode;
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000741A4 File Offset: 0x000723A4
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
				if (this.ListTxtFill[index].Text == global::GClass0.smethod_0(""))
				{
					this.ListTxtFill[index].Focus();
				}
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000742A0 File Offset: 0x000724A0
		private void btnNone_Click(object sender, RoutedEventArgs e)
		{
			if (this.oQuestion.SelectedCode == this.oQuestion.NoneCode)
			{
				this.oQuestion.SelectedCode = global::GClass0.smethod_0("");
				this.btnNone.Style = (Style)base.FindResource(global::GClass0.smethod_0("XŢɘͯѥՊٳݨࡖ॰੺୮౤"));
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
			this.btnNone.Style = (Style)base.FindResource(global::GClass0.smethod_0("Xůɥ͊ѳըٖݰࡺ८੤"));
			for (int j = 0; j < this.nTotal; j++)
			{
				this.ListCmbParent[j].IsEnabled = false;
				this.ListCmbCode[j].IsEnabled = false;
				this.ListTxtFill[j].Background = Brushes.LightGray;
				this.ListTxtFill[j].IsEnabled = false;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000743F4 File Offset: 0x000725F4
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
				this.oQSingle[0].FillText = global::GClass0.smethod_0("");
				for (int i = 1; i < this.nTotal; i++)
				{
					this.oQSingle[i].SelectedCode = global::GClass0.smethod_0("");
					this.oQSingle[i].FillText = global::GClass0.smethod_0("");
				}
			}
			else
			{
				int num = 0;
				for (int j = 0; j < this.nTotal; j++)
				{
					if (this.ListCmbParent[j].Text.Trim() != global::GClass0.smethod_0("") && (this.ListCmbCode[j].SelectedValue == global::GClass0.smethod_0("") || this.ListCmbCode[j].SelectedValue == null))
					{
						MessageBox.Show(SurveyMsg.MsgNotSelectClassB, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						this.btnNav.Content = this.btnNav_Content;
						this.ListCmbCode[j].Focus();
						return;
					}
					if ((string)this.ListCmbCode[j].SelectedValue != global::GClass0.smethod_0("") && this.ListCmbCode[j].SelectedValue != null)
					{
						this.oQSingle[num].SelectedCode = this.ListCmbCode[j].SelectedValue.ToString();
						this.oQSingle[num].FillText = this.ListTxtFill[j].Text;
						if (this.ListTxtFill[j].IsEnabled && this.ListTxtFill[j].Text == global::GClass0.smethod_0(""))
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
					this.oQSingle[k].SelectedCode = global::GClass0.smethod_0("");
					this.oQSingle[k].FillText = global::GClass0.smethod_0("");
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

		// Token: 0x060003F1 RID: 1009 RVA: 0x00074820 File Offset: 0x00072A20
		private bool method_2()
		{
			if (this.oQSingle[0].SelectedCode == global::GClass0.smethod_0(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			int i = 0;
			while (i < this.nTotal - 1)
			{
				if (!(this.oQSingle[i].SelectedCode == global::GClass0.smethod_0("")))
				{
					int num = i + 1;
					while (num < this.nTotal && !(this.oQSingle[num].SelectedCode == global::GClass0.smethod_0("")))
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
					while (num3 < this.nTotal - 1 && !(this.oQSingle[num3].SelectedCode == global::GClass0.smethod_0("")))
					{
						num2++;
						num3++;
					}
					if (num2 < this.oQuestion.QDefine.MIN_COUNT && this.oQuestion.QDefine.MIN_COUNT > 0)
					{
						MessageBox.Show(string.Format(global::GClass0.smethod_0("臼崟锍認鐂柣ةݳ࠷ॻਥ䔮鰊镻㸃"), this.oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					if (num2 > this.oQuestion.QDefine.MAX_COUNT && this.oQuestion.QDefine.MAX_COUNT > 0)
					{
						MessageBox.Show(string.Format(global::GClass0.smethod_0("朏堔凢䷩鐂柣ةݳ࠷ॻਥ䔮鰊镻㸃"), this.oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
					return false;
				}
			}
            return false;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00074A24 File Offset: 0x00072C24
		private List<VEAnswer> method_3()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = global::GClass0.smethod_0("");
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
					global::GClass0.smethod_0("-"),
					qsingle.QuestionName,
					global::GClass0.smethod_0("<"),
					qsingle.SelectedCode
				});
				if (qsingle.FillText != global::GClass0.smethod_0(""))
				{
					VEAnswer veanswer = new VEAnswer();
					veanswer.QUESTION_NAME = qsingle.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
					veanswer.CODE = qsingle.FillText;
					list.Add(veanswer);
					SurveyHelper.Answer = string.Concat(new string[]
					{
						SurveyHelper.Answer,
						global::GClass0.smethod_0("-"),
						veanswer.QUESTION_NAME,
						global::GClass0.smethod_0("<"),
						qsingle.FillText
					});
				}
			}
			SurveyHelper.Answer = this.method_10(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00074BAC File Offset: 0x00072DAC
		private void method_4()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.PageStartTime = SurveyHelper.PageStartTime;
			this.MyNav.RecordFileName = SurveyHelper.RecordFileName;
			this.MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			if (this.MyNav.GroupLevel == global::GClass0.smethod_0(""))
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
					SurveyHelper.CircleACode = global::GClass0.smethod_0("");
					SurveyHelper.CircleACodeText = global::GClass0.smethod_0("");
				}
				if (this.MyNav.GroupLevel == global::GClass0.smethod_0("C"))
				{
					SurveyHelper.CircleBCount = this.MyNav.CircleBCount;
					SurveyHelper.CircleBCurrent = this.MyNav.CircleBCurrent;
					if (this.MyNav.IsLastB && (this.MyNav.GroupPageType == 10 || this.MyNav.GroupPageType == 12 || this.MyNav.GroupPageType == 30 || this.MyNav.GroupPageType == 32))
					{
						SurveyHelper.CircleBCode = global::GClass0.smethod_0("");
						SurveyHelper.CircleBCodeText = global::GClass0.smethod_0("");
					}
				}
			}
			string text = this.oLogicEngine.Route(this.MyNav.RoadMap.FORM_NAME);
			SurveyHelper.RoadMapVersion = this.MyNav.RoadMap.VERSION_ID.ToString();
			string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), text);
			if (text.Substring(0, 1) == global::GClass0.smethod_0("@"))
			{
				uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), text);
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
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			SurveyHelper.NavLoad = 0;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00074E50 File Offset: 0x00073050
		private void method_5(int int_0, List<VEAnswer> list_0)
		{
			this.btnNav.IsEnabled = false;
			this.btnNav.Content = global::GClass0.smethod_0("歨嘢䷔塐Ы軱簈圝࠭बਯ");
			foreach (VEAnswer veanswer in list_0)
			{
				Logging.Data.WriteLog(this.MySurveyId, veanswer.QUESTION_NAME + global::GClass0.smethod_0("-") + veanswer.CODE);
			}
			Thread.Sleep(int_0);
			this.btnNav.Content = this.btnNav_Content;
			this.btnNav.IsEnabled = true;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00074F08 File Offset: 0x00073108
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

		// Token: 0x060003F6 RID: 1014 RVA: 0x00002581 File Offset: 0x00000781
		private void method_6(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000259E File Offset: 0x0000079E
		private void method_7(object sender, RoutedEventArgs e)
		{
			if (SurveyHelper.IsTouch == global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤"))
			{
				SurveyTaptip.ShowInputPanel();
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C878 File Offset: 0x0000AA78
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

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private string method_9(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000C918 File Offset: 0x0000AB18
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

		// Token: 0x060003FB RID: 1019 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private string method_11(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00074F70 File Offset: 0x00073170
		private int method_12(string string_0)
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
			if (!this.method_13(string_0))
			{
				return 0;
			}
			return Convert.ToInt32(string_0);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000025BC File Offset: 0x000007BC
		private bool method_13(string string_0)
		{
			return new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ")).IsMatch(string_0);
		}

		// Token: 0x0400076D RID: 1901
		private string MySurveyId;

		// Token: 0x0400076E RID: 1902
		private string CurPageId;

		// Token: 0x0400076F RID: 1903
		private NavBase MyNav = new NavBase();

		// Token: 0x04000770 RID: 1904
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000771 RID: 1905
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x04000772 RID: 1906
		private int nTotal = 5;

		// Token: 0x04000773 RID: 1907
		private QSingle oQuestion = new QSingle();

		// Token: 0x04000774 RID: 1908
		private List<QSingle> oQSingle = new List<QSingle>();

		// Token: 0x04000775 RID: 1909
		private List<ComboBox> ListCmbParent = new List<ComboBox>();

		// Token: 0x04000776 RID: 1910
		private List<ComboBox> ListCmbCode = new List<ComboBox>();

		// Token: 0x04000777 RID: 1911
		private List<TextBox> ListTxtFill = new List<TextBox>();

		// Token: 0x04000778 RID: 1912
		private bool ExistTextFill;

		// Token: 0x04000779 RID: 1913
		private bool PageLoaded;

		// Token: 0x0400077A RID: 1914
		private int Button_Type;

		// Token: 0x0400077B RID: 1915
		private int Button_Height;

		// Token: 0x0400077C RID: 1916
		private int Button_Width;

		// Token: 0x0400077D RID: 1917
		private int Button_FontSize;

		// Token: 0x0400077E RID: 1918
		private DispatcherTimer timer = new DispatcherTimer();

		// Token: 0x0400077F RID: 1919
		private int SecondsWait;

		// Token: 0x04000780 RID: 1920
		private int SecondsCountDown;

		// Token: 0x04000781 RID: 1921
		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		// Token: 0x020000A8 RID: 168
		[CompilerGenerated]
		[Serializable]
		private sealed class Class42
		{
			// Token: 0x06000766 RID: 1894 RVA: 0x00004347 File Offset: 0x00002547
			internal int method_0(SurveyDetail surveyDetail_0, SurveyDetail surveyDetail_1)
			{
				return Comparer<int>.Default.Compare(surveyDetail_0.INNER_ORDER, surveyDetail_1.INNER_ORDER);
			}

			// Token: 0x04000D0F RID: 3343
			public static readonly Relation5.Class42 instance = new Relation5.Class42();

			// Token: 0x04000D10 RID: 3344
			public static Comparison<SurveyDetail> compare0;
		}
	}
}
