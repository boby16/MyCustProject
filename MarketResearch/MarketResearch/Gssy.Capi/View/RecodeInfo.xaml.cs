using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;

namespace Gssy.Capi.View
{
	public partial class RecodeInfo : Page
	{
		public RecodeInfo()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.oQuestion.Init(this.CurPageId, 0);
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (!(this.MyNav.GroupLevel == "A") && !(this.MyNav.GroupLevel == "B"))
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
			else
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
			this.oAutoFill.oLogicEngine = this.oLogicEngine;
			if (SurveyHelper.AutoFill && this.oAutoFill.AutoNext(this.oQuestion.QDefine))
			{
				this.btnNav_Click(this, e);
			}
			if (this.oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				this.gridContent.Width = (double)this.oQuestion.QDefine.CONTROL_WIDTH;
			}
			string text = this.oQuestion.QDefine.QUESTION_TITLE;
			if (text == "")
			{
				text = "RECODE ：<B>" + this.oQuestion.QDefine.QUESTION_NAME + "</B>";
			}
			List<string> list2 = this.oBoldTitle.ParaToList(text, "//");
			text = list2[0];
			this.oBoldTitle.SetTextBlock(this.txtQuestionTitle, text, this.oQuestion.QDefine.TITLE_FONTSIZE, "", true);
			text = ((list2.Count > 1) ? list2[1] : this.oQuestion.QDefine.QUESTION_CONTENT);
			this.oBoldTitle.SetTextBlock(this.txtCircleTitle, text, 0, "", true);
			text = this.oQuestion.QDefine.NOTE;
			this.oBoldTitle.SetTextBlock(this.txtQuestionNote, text, 0, "", true);
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true" && this.oQuestion.QDefine.IS_ATTACH == 1)
			{
				this.btnAttach.Visibility = Visibility.Visible;
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

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			this.method_1();
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				this.btnNav.Content = this.btnNav_Content;
				return;
			}
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
			this.btnNav.Content = this.btnNav_Content;
		}

		private void method_1()
		{
			new SurveyAnswerDal().ClearBySequenceId(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.QUESTION_TYPE == 60)
			{
				this.method_2();
				return;
			}
			List<SurveyLogic> reCodeLogic = new SurveyLogicDal().GetReCodeLogic(this.CurPageId, SurveyMsg.MsgProgramType, 0, 99999);
			if (reCodeLogic.Count == 0)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgNotSetRecode, this.CurPageId), SurveyMsg.MsgCaptionError, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			reCodeLogic.Add(new SurveyLogic
			{
				PAGE_ID = " ",
				INNER_INDEX = 0
			});
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text = "";
			string str = "";
			foreach (SurveyLogic surveyLogic in reCodeLogic)
			{
				if (surveyLogic.PAGE_ID + surveyLogic.LOGIC_MESSAGE == text + str)
				{
					num2 = num3;
				}
				else
				{
					if (text != "")
					{
						string string_ = this.oQuestion.QuestionName;
						SurveyDefine surveyDefine = this.oQuestion.QDefine;
						if (reCodeLogic[num].LOGIC_MESSAGE != "")
						{
							string text2 = reCodeLogic[num].LOGIC_MESSAGE;
							if (this.oFunc.LEFT(text2, 1) == "^")
							{
								text2 = this.oFunc.MID(text2, 1, -9999);
								string_ = text2 + this.MyNav.QName_Add;
							}
							else
							{
								string_ = text2;
							}
							surveyDefine = this.oSurveyDefineDal.GetByName(text2);
							if (surveyDefine.PAGE_ID == null || surveyDefine.PAGE_ID == "")
							{
								surveyDefine = this.oQuestion.QDefine;
							}
						}
						if (reCodeLogic[num].LOGIC_TYPE == "RECODE_LOGIC")
						{
							this.method_3(string_, surveyDefine, reCodeLogic[num].INNER_INDEX, reCodeLogic[num2].INNER_INDEX);
						}
						else
						{
							this.method_4(string_, surveyDefine, reCodeLogic, num, num2);
						}
					}
					num = num3;
					num2 = num3;
					text = surveyLogic.PAGE_ID;
					str = surveyLogic.LOGIC_MESSAGE;
				}
				num3++;
			}
		}

		private void method_2()
		{
			try
			{
				string text = "";
				string[] array = this.oLogicEngine.RecodeAddonLogic(this.CurPageId, out text, SurveyMsg.MsgProgramType);
				if (this.MyNav.QName_Add != "")
				{
					for (int i = 0; i < array.Count<string>(); i++)
					{
						array[i] += this.MyNav.QName_Add;
					}
					text += this.MyNav.QName_Add;
				}
				SurveyAnswerDal surveyAnswerDal = new SurveyAnswerDal();
				string oneCode = surveyAnswerDal.GetOneCode(this.MySurveyId, text);
				SurveyDetail one = new SurveyDetailDal().GetOne(this.oQuestion.QDefine.DETAIL_ID, oneCode);
				string text2 = "";
				SurveyHelper.Answer = "";
				for (int j = 0; j < array.Count<string>(); j++)
				{
					switch (j)
					{
					case 0:
						text2 = one.EXTEND_1;
						break;
					case 1:
						text2 = one.EXTEND_2;
						break;
					case 2:
						text2 = one.EXTEND_3;
						break;
					case 3:
						text2 = one.EXTEND_4;
						break;
					case 4:
						text2 = one.EXTEND_5;
						break;
					case 5:
						text2 = one.EXTEND_6;
						break;
					case 6:
						text2 = one.EXTEND_7;
						break;
					case 7:
						text2 = one.EXTEND_8;
						break;
					case 8:
						text2 = one.EXTEND_9;
						break;
					case 9:
						text2 = one.EXTEND_10;
						break;
					}
					surveyAnswerDal.AddOne(this.MySurveyId, array[j], text2, SurveyHelper.SurveySequence);
					SurveyHelper.Answer = string.Concat(new string[]
					{
						SurveyHelper.Answer,
						",",
						array[j],
						"=",
						text2
					});
				}
				SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
			}
			catch (Exception)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgNotSetDataCopy, this.oQuestion.QDefine.PAGE_ID), SurveyMsg.MsgCaptionError, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void method_3(string string_0, SurveyDefine surveyDefine_0, int int_0 = 0, int int_1 = 4999)
		{
			int question_TYPE = surveyDefine_0.QUESTION_TYPE;
			if (question_TYPE != 9)
			{
				if (question_TYPE != 3)
				{
					if (question_TYPE != 8)
					{
						if (question_TYPE != 2)
						{
							QFill qfill = new QFill();
							qfill.Init(surveyDefine_0.PAGE_ID, surveyDefine_0.COMBINE_INDEX);
							qfill.QuestionName = string_0;
							string[] array;
							if (SurveyHelper.AutoFill && surveyDefine_0.FILLDATA != "")
							{
								array = this.oAutoFill.RecodeFill(surveyDefine_0);
							}
							else
							{
								array = this.oLogicEngine.RecodeLogic(this.CurPageId, SurveyMsg.MsgProgramType, question_TYPE, int_0, int_1);
							}
							SurveyHelper.Answer = qfill.QuestionName + "=" + array[0].ToString();
							qfill.FillText = array[0].ToString();
							qfill.Save(this.MySurveyId, SurveyHelper.SurveySequence);
							return;
						}
					}
					QSingle qsingle = new QSingle();
					qsingle.Init(surveyDefine_0.PAGE_ID, surveyDefine_0.COMBINE_INDEX, true);
					qsingle.QuestionName = string_0;
					string[] array2;
					if (SurveyHelper.AutoFill && surveyDefine_0.FILLDATA != "")
					{
						if (surveyDefine_0.DETAIL_ID != "")
						{
							array2 = this.oAutoFill.RecodeSingle(surveyDefine_0, qsingle.QDetails);
						}
						else
						{
							array2 = this.oAutoFill.RecodeFill(surveyDefine_0);
						}
					}
					else
					{
						array2 = this.oLogicEngine.RecodeLogic(this.CurPageId, SurveyMsg.MsgProgramType, question_TYPE, int_0, int_1);
					}
					SurveyHelper.Answer = qsingle.QuestionName + "=" + array2[0].ToString();
					qsingle.SelectedCode = array2[0].ToString();
					qsingle.BeforeSave();
					qsingle.Save(this.MySurveyId, SurveyHelper.SurveySequence, true);
					return;
				}
			}
			QMultiple qmultiple = new QMultiple();
			qmultiple.Init(surveyDefine_0.PAGE_ID, surveyDefine_0.COMBINE_INDEX, true);
			qmultiple.QuestionName = string_0;
			string[] array3;
			if (SurveyHelper.AutoFill && surveyDefine_0.FILLDATA != "")
			{
				array3 = this.oAutoFill.RecodeFill(surveyDefine_0);
			}
			else
			{
				array3 = this.oLogicEngine.RecodeLogic(this.CurPageId, SurveyMsg.MsgProgramType, question_TYPE, int_0, int_1);
			}
			string show_LOGIC = surveyDefine_0.SHOW_LOGIC;
			if (show_LOGIC != "")
			{
				List<string> list = this.oBoldTitle.ParaToList(show_LOGIC, "//");
				if (list[0].Trim() != "" && surveyDefine_0.IS_RANDOM == 0)
				{
					string[] array4 = this.oLogicEngine.aryCode(list[0], ',');
					List<string> list2 = new List<string>();
					int i = 0;
					while (i < array4.Count<string>())
					{
						foreach (string text in array3)
						{
							if (text == array4[i].ToString())
							{
								list2.Add(text);
								i++;
							}
						}
					}
					array3 = list2.ToArray();
				}
			}
			else if (surveyDefine_0.IS_RANDOM > 0)
			{
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int k = 0; k < array3.Count<string>(); k++)
				{
					foreach (SurveyDetail surveyDetail in qmultiple.QDetails)
					{
						if (surveyDetail.CODE == array3[k].ToString())
						{
							list3.Add(surveyDetail);
							break;
						}
					}
				}
				qmultiple.QDetails = list3;
				qmultiple.RandomDetails();
				List<string> list4 = new List<string>();
				foreach (SurveyDetail surveyDetail2 in qmultiple.QDetails)
				{
					list4.Add(surveyDetail2.CODE);
				}
				array3 = list4.ToArray();
			}
			SurveyHelper.Answer = "";
			for (int l = 0; l < array3.Count<string>(); l++)
			{
				qmultiple.SelectedValues.Add(array3[l].ToString());
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					",",
					qmultiple.QuestionName,
					"=",
					array3[l].ToString()
				});
			}
			SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
			qmultiple.BeforeSave();
			qmultiple.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		private void method_4(string string_0, SurveyDefine surveyDefine_0, List<SurveyLogic> list_0, int int_0, int int_1)
		{
			int question_TYPE = surveyDefine_0.QUESTION_TYPE;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = int_0; i <= int_1; i++)
			{
				SurveyLogic surveyLogic = list_0[i];
				foreach (string text in this.oLogicEngine.listLoopLevel(surveyLogic.FORMULA, surveyLogic.LOGIC_TYPE))
				{
					bool flag = true;
					if (dictionary.ContainsKey(text))
					{
						if (surveyLogic.NOTE == "Z")
						{
							flag = false;
						}
						if (question_TYPE == 1 || question_TYPE == 2 || question_TYPE == 7 || question_TYPE == 8)
						{
							flag = false;
						}
					}
					if (flag)
					{
						string string_ = this.method_5(surveyLogic.RECODE_ANSWER, text);
						if (question_TYPE != 1)
						{
							if (question_TYPE != 7)
							{
								string[] array = this.oLogicEngine.aryCode(string_, ',');
								string text2 = "";
								if (dictionary.ContainsKey(text))
								{
									text2 = this.oFunc.ArrayToString(array, ",", false, "");
									dictionary[text] = dictionary[text] + "," + text2;
									continue;
								}
								if (question_TYPE == 2)
								{
									goto IL_156;
								}
								if (question_TYPE == 8)
								{
									goto IL_156;
								}
								text2 = this.oFunc.ArrayToString(array, ",", false, "");
								dictionary.Add(text, text2);
								IL_170:
								if (this.ErrorPageId != this.CurPageId && !this.method_7(text, surveyDefine_0.GROUP_LEVEL, this.MySurveyId, surveyLogic))
								{
									this.ErrorPageId = this.CurPageId;
									continue;
								}
								continue;
								IL_156:
								if (array.Count<string>() > 0)
								{
									text2 = array[0];
								}
								dictionary.Add(text, text2);
								goto IL_170;
							}
						}
						string value = this.oLogicEngine.stringResult(string_);
						dictionary.Add(text, value);
						if (this.ErrorPageId != this.CurPageId && !this.method_7(text, surveyDefine_0.GROUP_LEVEL, this.MySurveyId, surveyLogic))
						{
							this.ErrorPageId = this.CurPageId;
						}
					}
				}
			}
			if (question_TYPE != 9)
			{
				if (question_TYPE != 3)
				{
					if (question_TYPE != 8)
					{
						if (question_TYPE != 2)
						{
							QFill qfill = new QFill();
							qfill.Init(surveyDefine_0.PAGE_ID, surveyDefine_0.COMBINE_INDEX);
							qfill.QuestionName = string_0;
							using (Dictionary<string, string>.KeyCollection.Enumerator enumerator2 = dictionary.Keys.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string text3 = enumerator2.Current;
									qfill.QuestionName = string_0 + text3;
									string text4 = dictionary[text3];
									SurveyHelper.Answer = qfill.QuestionName + "=" + text4;
									qfill.FillText = text4;
									qfill.Save(this.MySurveyId, SurveyHelper.SurveySequence);
								}
								return;
							}
						}
					}
					QSingle qsingle = new QSingle();
					qsingle.Init(surveyDefine_0.PAGE_ID, surveyDefine_0.COMBINE_INDEX, true);
					qsingle.QuestionName = string_0;
					foreach (string text5 in dictionary.Keys)
					{
						qsingle.QuestionName = string_0 + text5;
						string text6 = dictionary[text5];
						SurveyHelper.Answer = qsingle.QuestionName + "=" + text6;
						qsingle.SelectedCode = text6;
						qsingle.BeforeSave();
						qsingle.Save(this.MySurveyId, SurveyHelper.SurveySequence, false);
					}
					return;
				}
			}
			QMultiple qmultiple = new QMultiple();
			qmultiple.Init(surveyDefine_0.PAGE_ID, surveyDefine_0.COMBINE_INDEX, true);
			string[] array2 = null;
			if (surveyDefine_0.SHOW_LOGIC != "")
			{
				List<string> list = this.oBoldTitle.ParaToList(surveyDefine_0.SHOW_LOGIC, "//");
				if (list[0].Trim() != "" && surveyDefine_0.IS_RANDOM == 0)
				{
					array2 = this.oLogicEngine.aryCode(list[0].Trim(), ',');
				}
			}
			foreach (string text7 in dictionary.Keys)
			{
				qmultiple.QuestionName = string_0 + text7;
				string string_2 = dictionary[text7];
				string[] array3 = this.oFunc.StringArrayDeleteDuplicate(this.oFunc.StringToArray(string_2, ','));
				if (array2 != null)
				{
					List<string> list2 = new List<string>();
					int j = 0;
					while (j < array2.Count<string>())
					{
						foreach (string text8 in array3)
						{
							if (text8 == array2[j].ToString())
							{
								list2.Add(text8);
								j++;
							}
						}
					}
					array3 = list2.ToArray();
				}
				else if (surveyDefine_0.IS_RANDOM > 0)
				{
					List<SurveyDetail> list3 = new List<SurveyDetail>();
					for (int l = 0; l < array3.Count<string>(); l++)
					{
						using (List<SurveyDetail>.Enumerator enumerator3 = qmultiple.QDetails.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								SurveyDetail surveyDetail = enumerator3.Current;
								if (surveyDetail.CODE == array3[l].ToString())
								{
									list3.Add(surveyDetail);
									break;
								}
							}
							goto IL_670;
						}
						break;
						IL_670:;
					}
					qmultiple.QDetails = list3;
					qmultiple.RandomDetails();
					List<string> list4 = new List<string>();
					foreach (SurveyDetail surveyDetail2 in qmultiple.QDetails)
					{
						list4.Add(surveyDetail2.CODE);
					}
					array3 = list4.ToArray();
				}
				SurveyHelper.Answer = "";
				for (int m = 0; m < array3.Count<string>(); m++)
				{
					qmultiple.SelectedValues.Add(array3[m].ToString());
					SurveyHelper.Answer = string.Concat(new string[]
					{
						SurveyHelper.Answer,
						",",
						qmultiple.QuestionName,
						"=",
						array3[m].ToString()
					});
				}
				SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
			}
			qmultiple.BeforeSave();
			qmultiple.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		private string method_5(string string_0, string string_1)
		{
			string text = "";
			string text2 = "^";
			string text3 = "";
			bool flag = false;
			string text4 = string_0 + " ";
			for (int i = 0; i < text4.Length; i++)
			{
				string text5 = text4[i].ToString();
				if (text5 == text2)
				{
					if (flag)
					{
						if (text3 == "")
						{
							text += text2;
						}
						else
						{
							text = text + text3 + string_1;
						}
					}
					flag = true;
					text3 = "";
				}
				else if (flag)
				{
					string text6 = this.method_6(text5, text3);
					if (text6 == "")
					{
						if (text3 == "")
						{
							text += text2;
						}
						else
						{
							text = text + text3 + string_1 + text5;
						}
						flag = false;
						text3 = "";
					}
					else
					{
						text3 = text6;
					}
				}
				else
				{
					text += text5;
				}
			}
			return text;
		}

		private string method_6(string string_0, string string_1)
		{
			string result = "";
			string text = "#^|&`*+-/<>=,`%~@[](){}:;.!$\"'\\? ";
			string text2 = "0123456789";
			if (text.IndexOf(string_0) == -1)
			{
				if (text2.IndexOf(string_0) > -1)
				{
					if (string_1.Length > 0)
					{
						result = string_1 + string_0;
					}
				}
				else
				{
					result = string_1 + string_0;
				}
			}
			return result;
		}

		private bool method_7(string string_0, string string_1, string string_2, SurveyLogic surveyLogic_0)
		{
			string string_3 = "";
			if (string_1 == "A")
			{
				string_3 = "_R\\d+";
			}
			else
			{
				if (!(string_1 == "B"))
				{
					return true;
				}
				string_3 = "_R\\d+_R\\d+";
			}
			if (!this.oFunc.isMatch(string_0, string_3))
			{
				MessageBox.Show(string.Format(string.Concat(new string[]
				{
					"问卷{0}在循环题【{1}】RECODE时发生错误！",
					Environment.NewLine,
					"可能是设置了错误的循环引导题【序号{2}】导致的。",
					Environment.NewLine,
					Environment.NewLine,
					"请把这个信息反馈给程序员！"
				}), string_2, surveyLogic_0.PAGE_ID, surveyLogic_0.INNER_INDEX.ToString()), "RECODE错误", MessageBoxButton.OK, MessageBoxImage.Hand);
				return false;
			}
			return true;
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

		private string ErrorPageId = "";

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private AutoFill oAutoFill = new AutoFill();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QFill oQuestion = new QFill();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private UDPX oFunc = new UDPX();

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;
	}
}
