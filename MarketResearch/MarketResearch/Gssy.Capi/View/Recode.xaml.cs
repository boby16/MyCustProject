using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	// Token: 0x02000047 RID: 71
	public partial class Recode : Page
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x00087A24 File Offset: 0x00085C24
		public Recode()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00087AA8 File Offset: 0x00085CA8
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			this.oQuestion.Init(this.CurPageId, 0);
			new SurveyBiz().ClearPageAnswer(this.MySurveyId, SurveyHelper.SurveySequence);
			this.MyNav.GroupLevel = this.oQuestion.QDefine.GROUP_LEVEL;
			if (!(this.MyNav.GroupLevel == global::GClass0.smethod_0("@")) && !(this.MyNav.GroupLevel == global::GClass0.smethod_0("C")))
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
			else
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
			this.oAutoFill.oLogicEngine = this.oLogicEngine;
			if (SurveyHelper.NavOperation == global::GClass0.smethod_0("FŢɡͪ"))
			{
				SurveyHelper.AutoFill = false;
				int surveySequence = SurveyHelper.SurveySequence;
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, surveySequence);
				this.method_1();
				SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
				return;
			}
			this.method_2();
			bool flag = false;
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.CheckLogic(this.CurPageId))
			{
				flag = true;
			}
			if (flag)
			{
				SurveyHelper.AutoFill = false;
				int surveySequence2 = SurveyHelper.SurveySequence;
				new SurveyBiz().ClearPageAnswer(this.MySurveyId, surveySequence2);
				this.method_1();
				SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
				return;
			}
			this.oPageNav.oLogicEngine = this.oLogicEngine;
			if (!this.oPageNav.NextPage(this.MyNav, base.NavigationService))
			{
				if (this.CurPageId == SurveyHelper.SurveyFirstPage)
				{
					MessageBox.Show(SurveyMsg.MsgErrorRoadmapTip + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				this.method_1();
				SurveyHelper.NavOperation = global::GClass0.smethod_0("FŢɡͪ");
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00087FF4 File Offset: 0x000861F4
		private void method_1()
		{
			int surveySequence = SurveyHelper.SurveySequence;
			if (!(this.MySurveyId == global::GClass0.smethod_0("")) && !(this.CurPageId == SurveyHelper.SurveyFirstPage))
			{
				string roadMapVersion = SurveyHelper.RoadMapVersion;
				this.MyNav.PrePage(this.MySurveyId, surveySequence, roadMapVersion);
				SurveyHelper.CircleACount = this.MyNav.Sequence.CIRCLE_A_COUNT;
				SurveyHelper.CircleACurrent = this.MyNav.Sequence.CIRCLE_A_CURRENT;
				SurveyHelper.CircleBCount = this.MyNav.Sequence.CIRCLE_B_COUNT;
				SurveyHelper.CircleBCurrent = this.MyNav.Sequence.CIRCLE_B_CURRENT;
				string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), this.MyNav.RoadMap.FORM_NAME);
				if (this.MyNav.RoadMap.FORM_NAME.Substring(0, 1) == global::GClass0.smethod_0("@"))
				{
					uriString = string.Format(global::GClass0.smethod_0("[ŋɊ̓Нԉ؊݅ࡓ੍॒୉౼ൿ๩ཱུၴᅴሣጴᐻᔺᘺᝂ᡺᥷ᩦᭀᱽᵡṧὩ⁨ⅾ∦⍳␷╻☫❼⡢⥯⩭"), this.MyNav.RoadMap.FORM_NAME);
				}
				if (this.MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
				{
					base.NavigationService.Refresh();
				}
				else
				{
					base.NavigationService.RemoveBackEntry();
					base.NavigationService.Navigate(new Uri(uriString));
				}
				SurveyHelper.SurveySequence = surveySequence - 1;
				SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
				SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
				return;
			}
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			MessageBox.Show(SurveyMsg.MsgFirstPage, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			this.oPageNav.NextPage(this.MyNav, base.NavigationService);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000881C0 File Offset: 0x000863C0
		private void method_2()
		{
			new SurveyAnswerDal().ClearBySequenceId(this.MySurveyId, SurveyHelper.SurveySequence);
			if (this.oQuestion.QDefine.QUESTION_TYPE == 60)
			{
				this.method_3();
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
				PAGE_ID = global::GClass0.smethod_0("!"),
				INNER_INDEX = 0
			});
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			string text = global::GClass0.smethod_0("");
			string str = global::GClass0.smethod_0("");
			foreach (SurveyLogic surveyLogic in reCodeLogic)
			{
				if (surveyLogic.PAGE_ID + surveyLogic.LOGIC_MESSAGE == text + str)
				{
					num2 = num3;
				}
				else
				{
					if (text != global::GClass0.smethod_0(""))
					{
						string string_ = this.oQuestion.QuestionName;
						SurveyDefine surveyDefine = this.oQuestion.QDefine;
						if (reCodeLogic[num].LOGIC_MESSAGE != global::GClass0.smethod_0(""))
						{
							string text2 = reCodeLogic[num].LOGIC_MESSAGE;
							if (this.oFunc.LEFT(text2, 1) == global::GClass0.smethod_0("_"))
							{
								text2 = this.oFunc.MID(text2, 1, -9999);
								string_ = text2 + this.MyNav.QName_Add;
							}
							else
							{
								string_ = text2;
							}
							surveyDefine = this.oSurveyDefineDal.GetByName(text2);
							if (surveyDefine.PAGE_ID == null || surveyDefine.PAGE_ID == global::GClass0.smethod_0(""))
							{
								surveyDefine = this.oQuestion.QDefine;
							}
						}
						if (reCodeLogic[num].LOGIC_TYPE == global::GClass0.smethod_0("^Ŏɉ͆ьՂٙ݉ࡋॄੋୂ"))
						{
							this.method_4(string_, surveyDefine, reCodeLogic[num].INNER_INDEX, reCodeLogic[num2].INNER_INDEX);
						}
						else
						{
							this.method_5(string_, surveyDefine, reCodeLogic, num, num2);
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

		// Token: 0x060004C9 RID: 1225 RVA: 0x00088454 File Offset: 0x00086654
		private void method_3()
		{
			try
			{
				string text = global::GClass0.smethod_0("");
				string[] array = this.oLogicEngine.RecodeAddonLogic(this.CurPageId, out text, SurveyMsg.MsgProgramType);
				if (this.MyNav.QName_Add != global::GClass0.smethod_0(""))
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
				string text2 = global::GClass0.smethod_0("");
				SurveyHelper.Answer = global::GClass0.smethod_0("");
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
						global::GClass0.smethod_0("-"),
						array[j],
						global::GClass0.smethod_0("<"),
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

		// Token: 0x060004CA RID: 1226 RVA: 0x000886A0 File Offset: 0x000868A0
		private void method_4(string string_0, SurveyDefine surveyDefine_0, int int_0 = 0, int int_1 = 4999)
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
							if (SurveyHelper.AutoFill && surveyDefine_0.FILLDATA != global::GClass0.smethod_0(""))
							{
								array = this.oAutoFill.RecodeFill(surveyDefine_0);
							}
							else
							{
								array = this.oLogicEngine.RecodeLogic(this.CurPageId, SurveyMsg.MsgProgramType, question_TYPE, int_0, int_1);
							}
							SurveyHelper.Answer = qfill.QuestionName + global::GClass0.smethod_0("<") + array[0].ToString();
							qfill.FillText = array[0].ToString();
							qfill.Save(this.MySurveyId, SurveyHelper.SurveySequence);
							return;
						}
					}
					QSingle qsingle = new QSingle();
					qsingle.Init(surveyDefine_0.PAGE_ID, surveyDefine_0.COMBINE_INDEX, true);
					qsingle.QuestionName = string_0;
					string[] array2;
					if (SurveyHelper.AutoFill && surveyDefine_0.FILLDATA != global::GClass0.smethod_0(""))
					{
						if (surveyDefine_0.DETAIL_ID != global::GClass0.smethod_0(""))
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
					SurveyHelper.Answer = qsingle.QuestionName + global::GClass0.smethod_0("<") + array2[0].ToString();
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
			if (SurveyHelper.AutoFill && surveyDefine_0.FILLDATA != global::GClass0.smethod_0(""))
			{
				array3 = this.oAutoFill.RecodeFill(surveyDefine_0);
			}
			else
			{
				array3 = this.oLogicEngine.RecodeLogic(this.CurPageId, SurveyMsg.MsgProgramType, question_TYPE, int_0, int_1);
			}
			string show_LOGIC = surveyDefine_0.SHOW_LOGIC;
			if (show_LOGIC != global::GClass0.smethod_0(""))
			{
				List<string> list = this.oBoldTitle.ParaToList(show_LOGIC, global::GClass0.smethod_0("-Į"));
				if (list[0].Trim() != global::GClass0.smethod_0("") && surveyDefine_0.IS_RANDOM == 0)
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
			SurveyHelper.Answer = global::GClass0.smethod_0("");
			for (int l = 0; l < array3.Count<string>(); l++)
			{
				qmultiple.SelectedValues.Add(array3[l].ToString());
				SurveyHelper.Answer = string.Concat(new string[]
				{
					SurveyHelper.Answer,
					global::GClass0.smethod_0("-"),
					qmultiple.QuestionName,
					global::GClass0.smethod_0("<"),
					array3[l].ToString()
				});
			}
			SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
			qmultiple.BeforeSave();
			qmultiple.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00088B54 File Offset: 0x00086D54
		private void method_5(string string_0, SurveyDefine surveyDefine_0, List<SurveyLogic> list_0, int int_0, int int_1)
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
						if (surveyLogic.NOTE == global::GClass0.smethod_0("["))
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
						string string_ = this.method_6(surveyLogic.RECODE_ANSWER, text);
						if (question_TYPE != 1)
						{
							if (question_TYPE != 7)
							{
								string[] array = this.oLogicEngine.aryCode(string_, ',');
								string text2 = global::GClass0.smethod_0("");
								if (dictionary.ContainsKey(text))
								{
									text2 = this.oFunc.ArrayToString(array, global::GClass0.smethod_0("-"), false, global::GClass0.smethod_0(""));
									dictionary[text] = dictionary[text] + global::GClass0.smethod_0("-") + text2;
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
								text2 = this.oFunc.ArrayToString(array, global::GClass0.smethod_0("-"), false, global::GClass0.smethod_0(""));
								dictionary.Add(text, text2);
								IL_170:
								if (this.ErrorPageId != this.CurPageId && !this.method_8(text, surveyDefine_0.GROUP_LEVEL, this.MySurveyId, surveyLogic, global::GClass0.smethod_0("S")))
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
						if (this.ErrorPageId != this.CurPageId && !this.method_8(text, surveyDefine_0.GROUP_LEVEL, this.MySurveyId, surveyLogic, global::GClass0.smethod_0("S")))
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
									SurveyHelper.Answer = qfill.QuestionName + global::GClass0.smethod_0("<") + text4;
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
						SurveyHelper.Answer = qsingle.QuestionName + global::GClass0.smethod_0("<") + text6;
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
			if (surveyDefine_0.SHOW_LOGIC != global::GClass0.smethod_0(""))
			{
				List<string> list = this.oBoldTitle.ParaToList(surveyDefine_0.SHOW_LOGIC, global::GClass0.smethod_0("-Į"));
				if (list[0].Trim() != global::GClass0.smethod_0("") && surveyDefine_0.IS_RANDOM == 0)
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
							goto IL_687;
						}
						break;
						IL_687:;
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
				SurveyHelper.Answer = global::GClass0.smethod_0("");
				for (int m = 0; m < array3.Count<string>(); m++)
				{
					qmultiple.SelectedValues.Add(array3[m].ToString());
					SurveyHelper.Answer = string.Concat(new string[]
					{
						SurveyHelper.Answer,
						global::GClass0.smethod_0("-"),
						qmultiple.QuestionName,
						global::GClass0.smethod_0("<"),
						array3[m].ToString()
					});
				}
				SurveyHelper.Answer = this.oFunc.MID(SurveyHelper.Answer, 1, -9999);
			}
			qmultiple.BeforeSave();
			qmultiple.Save(this.MySurveyId, SurveyHelper.SurveySequence);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000892B0 File Offset: 0x000874B0
		private string method_6(string string_0, string string_1)
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("_");
			string text3 = global::GClass0.smethod_0("");
			bool flag = false;
			string text4 = string_0 + global::GClass0.smethod_0("!");
			for (int i = 0; i < text4.Length; i++)
			{
				string text5 = text4[i].ToString();
				if (text5 == text2)
				{
					if (flag)
					{
						if (text3 == global::GClass0.smethod_0(""))
						{
							text += text2;
						}
						else
						{
							text = text + text3 + string_1;
						}
					}
					flag = true;
					text3 = global::GClass0.smethod_0("");
				}
				else if (flag)
				{
					string text6 = this.method_7(text5, text3);
					if (text6 == global::GClass0.smethod_0(""))
					{
						if (text3 == global::GClass0.smethod_0(""))
						{
							text += text2;
						}
						else
						{
							text = text + text3 + string_1 + text5;
						}
						flag = false;
						text3 = global::GClass0.smethod_0("");
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

		// Token: 0x060004CD RID: 1229 RVA: 0x00071758 File Offset: 0x0006F958
		private string method_7(string string_0, string string_1)
		{
			string result = global::GClass0.smethod_0("");
			string text = global::GClass0.smethod_0("\u0002žɣ̸ѽԶذܷ࠶त਩ଫహ൴ึཬၑᅋቒጦᐤᕷᙶᜰᠲᤦᨦᬢᰧᴣṟἽ‡");
			string text2 = global::GClass0.smethod_0(":ĸȺ̴в԰زܴ࠺स");
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

		// Token: 0x060004CE RID: 1230 RVA: 0x000893CC File Offset: 0x000875CC
		private bool method_8(string string_0, string string_1, string string_2, SurveyLogic surveyLogic_0, string string_3 = "R")
		{
			string string_4 = global::GClass0.smethod_0("");
			if (string_1 == global::GClass0.smethod_0("@"))
			{
				string_4 = global::GClass0.smethod_0("^") + string_3 + global::GClass0.smethod_0("_ŦȪ");
			}
			else
			{
				if (!(string_1 == global::GClass0.smethod_0("C")))
				{
					return true;
				}
				string_4 = string.Concat(new string[]
				{
					global::GClass0.smethod_0("^"),
					string_3,
					global::GClass0.smethod_0("Xŧȩ͞"),
					string_3,
					global::GClass0.smethod_0("_ŦȪ")
				});
			}
			if (!this.oFunc.isMatch(string_0, string_4))
			{
				MessageBox.Show(string.Format(string.Concat(new string[]
				{
					global::GClass0.smethod_0("间剮ɣ̧ѫ刽妾璼邊㤁੫ା౳㴜๞ཎ၉ᅆቌፂ燰䛔挛舚鏭"),
					Environment.NewLine,
					global::GClass0.smethod_0("叶臥搸袨筻䮒錊賽纕嚺禠君埱閔㸛内䏾ᅳስ፻␔仸韷憆⠃"),
					Environment.NewLine,
					Environment.NewLine,
					global::GClass0.smethod_0("诺掆跒䴠䯨敧嗊麎盜猏和彚")
				}), string_2, surveyLogic_0.PAGE_ID, surveyLogic_0.INNER_INDEX.ToString()), global::GClass0.smethod_0("ZłɅ͊рՆ錛賮"), MessageBoxButton.OK, MessageBoxImage.Hand);
				return false;
			}
			return true;
		}

		// Token: 0x040008F1 RID: 2289
		private string MySurveyId;

		// Token: 0x040008F2 RID: 2290
		private string CurPageId;

		// Token: 0x040008F3 RID: 2291
		private string ErrorPageId = global::GClass0.smethod_0("");

		// Token: 0x040008F4 RID: 2292
		private NavBase MyNav = new NavBase();

		// Token: 0x040008F5 RID: 2293
		private PageNav oPageNav = new PageNav();

		// Token: 0x040008F6 RID: 2294
		private LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x040008F7 RID: 2295
		private AutoFill oAutoFill = new AutoFill();

		// Token: 0x040008F8 RID: 2296
		private BoldTitle oBoldTitle = new BoldTitle();

		// Token: 0x040008F9 RID: 2297
		private UDPX oFunc = new UDPX();

		// Token: 0x040008FA RID: 2298
		private QFill oQuestion = new QFill();

		// Token: 0x040008FB RID: 2299
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();
	}
}
