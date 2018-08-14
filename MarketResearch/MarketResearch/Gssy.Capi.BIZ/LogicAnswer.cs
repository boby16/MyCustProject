using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000012 RID: 18
	public class LogicAnswer
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000025A0 File Offset: 0x000007A0
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000025A8 File Offset: 0x000007A8
		public string SurveyID { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000025B1 File Offset: 0x000007B1
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x000025B9 File Offset: 0x000007B9
		public string CircleACode { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000025C2 File Offset: 0x000007C2
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000025CA File Offset: 0x000007CA
		public string CircleBCode { get; set; }

		// Token: 0x060000EA RID: 234 RVA: 0x00009834 File Offset: 0x00007A34
		public LogicAnswer()
		{
			this.SeparatorA = '\u0011'.ToString();
			this.SeparatorB = '\u0012'.ToString();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000098B0 File Offset: 0x00007AB0
		public string GetAnswer(string string_0)
		{
			return this.method_1(string_0);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000098C8 File Offset: 0x00007AC8
		public string GetText(string string_0, string string_1)
		{
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			return this.oSurveyDetailDal.GetCodeText(byName.DETAIL_ID, string_1);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000098F8 File Offset: 0x00007AF8
		public string GetDetailsText(string string_0, string string_1)
		{
			return this.oSurveyDetailDal.GetCodeText(string_0, string_1);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00009914 File Offset: 0x00007B14
		public string GetOtherText(string string_0, string string_1 = "")
		{
			string text = global::GClass0.smethod_0("");
			string text2 = (string_1 == global::GClass0.smethod_0("")) ? (string_0 + global::GClass0.smethod_0("[Ōɖ͉")) : (string_0 + global::GClass0.smethod_0("YŊɐ͋ѝՂ") + string_1);
			foreach (VEAnswer veanswer in this.PageAnswer)
			{
				if ((veanswer.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text2 + global::GClass0.smethod_0("^")) == 0)
				{
					text = veanswer.CODE;
				}
			}
			if (text == global::GClass0.smethod_0(""))
			{
				text = this.oSurveyAnswerDal.GetOneCode(this.SurveyID, text2);
			}
			return text;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009A04 File Offset: 0x00007C04
		public string GetCurrentCircleAnswer(string string_0)
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			string text4 = global::GClass0.smethod_0("");
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				text4 = string_0 + global::GClass0.smethod_0("]œ") + this.CircleACode;
			}
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				text4 = string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("]œ"),
					this.CircleBCode
				});
			}
			List<SurveyAnswer> circleListByConbine = this.oSurveyAnswerDal.GetCircleListByConbine(this.SurveyID, text4, false);
			string value = string_0 + global::GClass0.smethod_0("^");
			foreach (VEAnswer veanswer in this.PageAnswer)
			{
				if ((veanswer.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(value) == 0)
				{
					foreach (SurveyAnswer surveyAnswer in circleListByConbine)
					{
						if (surveyAnswer.QUESTION_NAME == veanswer.QUESTION_NAME)
						{
							circleListByConbine.Remove(surveyAnswer);
							break;
						}
					}
					circleListByConbine.Add(new SurveyAnswer
					{
						QUESTION_NAME = veanswer.QUESTION_NAME,
						CODE = veanswer.CODE,
						SURVEY_ID = this.SurveyID
					});
				}
			}
			bool flag = true;
			bool flag2 = false;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				bool flag3 = true;
				text2 = global::GClass0.smethod_0("");
				foreach (SurveyAnswer surveyAnswer2 in circleListByConbine)
				{
					if (surveyAnswer2.QUESTION_NAME == text4)
					{
						text2 = surveyAnswer2.CODE;
						flag3 = false;
						flag2 = true;
					}
					if (!flag2 && (surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("]ŀ")) == 0)
					{
						if (flag3)
						{
							text2 = surveyAnswer2.CODE;
							flag3 = false;
						}
						else if (text2 == global::GClass0.smethod_0(""))
						{
							text2 = surveyAnswer2.CODE;
						}
						else if (surveyAnswer2.CODE != global::GClass0.smethod_0(""))
						{
							text2 = text2 + this.MulitpleSeparator + surveyAnswer2.CODE;
						}
					}
				}
				if (flag)
				{
					text = text2;
					flag = false;
				}
				else
				{
					text = text + this.SeparatorA + text2;
				}
			}
			bool flag4 = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				text3 = global::GClass0.smethod_0("");
				flag4 = true;
				bool flag3 = true;
				text2 = global::GClass0.smethod_0("");
				foreach (SurveyAnswer surveyAnswer3 in circleListByConbine)
				{
					if (surveyAnswer3.QUESTION_NAME == text4)
					{
						text2 = surveyAnswer3.CODE;
						flag3 = false;
						flag2 = true;
					}
					if (!flag2 && (surveyAnswer3.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("]ŀ")) == 0)
					{
						if (flag3)
						{
							text2 = surveyAnswer3.CODE;
							flag3 = false;
						}
						else if (text2 == global::GClass0.smethod_0(""))
						{
							text2 = surveyAnswer3.CODE;
						}
						else if (surveyAnswer3.CODE != global::GClass0.smethod_0(""))
						{
							text2 = text2 + this.MulitpleSeparator + surveyAnswer3.CODE;
						}
					}
				}
				if (flag4)
				{
					text3 = text2;
					flag4 = false;
				}
				else
				{
					text3 = text3 + this.SeparatorB + text2;
				}
				if (flag)
				{
					text = text3;
					flag = false;
				}
				else
				{
					text = text + this.SeparatorA + text3;
				}
			}
			return text;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009E94 File Offset: 0x00008094
		public string GetCircleAnswer(string string_0)
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			string text4 = global::GClass0.smethod_0("");
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			List<SurveyRandom> list2 = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEA);
			List<SurveyRandom> list3 = new List<SurveyRandom>();
			foreach (SurveyRandom surveyRandom in list2)
			{
				if (surveyRandom.CODE == this.CircleACode)
				{
					break;
				}
				if (surveyRandom.QUESTION_NAME != global::GClass0.smethod_0("NŖɏ͑"))
				{
					list3.Add(surveyRandom);
				}
			}
			List<SurveyRandom> list4 = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEB);
			List<SurveyRandom> list5 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				foreach (SurveyRandom surveyRandom2 in list4)
				{
					if (surveyRandom2.CODE == this.CircleBCode)
					{
						break;
					}
					if (surveyRandom2.QUESTION_NAME != global::GClass0.smethod_0("NŖɏ͑"))
					{
						list5.Add(surveyRandom2);
					}
				}
			}
			if (list.Count == 0)
			{
				list = this.oSurveyAnswerDal.GetCircleList(this.SurveyID, string_0, false);
			}
			if (this.PageAnswer.Count > 0 && list.Count == 0)
			{
				text4 = string_0 + global::GClass0.smethod_0("]œ");
				foreach (VEAnswer veanswer in this.PageAnswer)
				{
					if (veanswer.QUESTION_NAME.IndexOf(text4) == 0)
					{
						list.Add(new SurveyAnswer
						{
							CODE = veanswer.CODE,
							QUESTION_NAME = veanswer.QUESTION_NAME
						});
					}
				}
			}
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				text4 = string_0 + global::GClass0.smethod_0("]œ") + this.CircleACode + global::GClass0.smethod_0("^");
			}
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				text4 = string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("]œ"),
					this.CircleBCode,
					global::GClass0.smethod_0("^")
				});
			}
			for (int i = list.Count<SurveyAnswer>() - 1; i >= 0; i--)
			{
				if ((list[i].QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
				{
					list.Remove(list[i]);
				}
			}
			bool flag = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				foreach (SurveyRandom surveyRandom3 in list3)
				{
					text4 = string_0 + global::GClass0.smethod_0("]œ") + surveyRandom3.CODE + global::GClass0.smethod_0("^");
					bool flag2 = true;
					text2 = global::GClass0.smethod_0("");
					foreach (SurveyAnswer surveyAnswer in list)
					{
						if ((surveyAnswer.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer.CODE;
								flag2 = false;
							}
							else if ((surveyAnswer.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("@")) == 0)
							{
								if (text2 == global::GClass0.smethod_0(""))
								{
									text2 = surveyAnswer.CODE;
								}
								else if (surveyAnswer.CODE != global::GClass0.smethod_0(""))
								{
									text2 = text2 + this.MulitpleSeparator + surveyAnswer.CODE;
								}
							}
						}
					}
					if (flag)
					{
						text = text2;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text2;
					}
				}
			}
			bool flag3 = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				foreach (SurveyRandom surveyRandom4 in list3)
				{
					text3 = global::GClass0.smethod_0("");
					flag3 = true;
					foreach (SurveyRandom surveyRandom5 in list5)
					{
						text4 = string.Concat(new string[]
						{
							string_0,
							global::GClass0.smethod_0("]œ"),
							surveyRandom4.CODE,
							global::GClass0.smethod_0("]œ"),
							surveyRandom5.CODE,
							global::GClass0.smethod_0("^")
						});
						bool flag2 = true;
						text2 = global::GClass0.smethod_0("");
						foreach (SurveyAnswer surveyAnswer2 in list)
						{
							if ((surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer2.CODE;
									flag2 = false;
								}
								else if ((surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("@")) == 0)
								{
									if (text2 == global::GClass0.smethod_0(""))
									{
										text2 = surveyAnswer2.CODE;
									}
									else if (surveyAnswer2.CODE != global::GClass0.smethod_0(""))
									{
										text2 = text2 + this.MulitpleSeparator + surveyAnswer2.CODE;
									}
								}
							}
						}
						if (flag3)
						{
							text3 = text2;
							flag3 = false;
						}
						else
						{
							text3 = text3 + this.SeparatorB + text2;
						}
					}
					if (flag)
					{
						text = text3;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text3;
					}
				}
			}
			return text;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000A5FC File Offset: 0x000087FC
		public string GetAllCircleAnswer(string string_0)
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			string text4 = global::GClass0.smethod_0("");
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			List<SurveyRandom> list2 = this.method_0(this.SurveyID, byName.GROUP_CODEA);
			List<SurveyRandom> list3 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				list3 = this.method_0(this.SurveyID, byName.GROUP_CODEB);
			}
			if (list.Count == 0)
			{
				list = this.oSurveyAnswerDal.GetCircleList(this.SurveyID, string_0, false);
			}
			if (this.PageAnswer.Count > 0)
			{
				text4 = string_0 + global::GClass0.smethod_0("]œ");
				foreach (VEAnswer veanswer in this.PageAnswer)
				{
					if (veanswer.QUESTION_NAME.IndexOf(text4) == 0)
					{
						list.Add(new SurveyAnswer
						{
							CODE = veanswer.CODE,
							QUESTION_NAME = veanswer.QUESTION_NAME
						});
					}
				}
			}
			bool flag = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				foreach (SurveyRandom surveyRandom in list2)
				{
					text4 = string_0 + global::GClass0.smethod_0("]œ") + surveyRandom.CODE + global::GClass0.smethod_0("^");
					bool flag2 = true;
					text2 = global::GClass0.smethod_0("");
					foreach (SurveyAnswer surveyAnswer in list)
					{
						if ((surveyAnswer.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer.CODE;
								flag2 = false;
							}
							else if ((surveyAnswer.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("@")) == 0)
							{
								if (text2 == global::GClass0.smethod_0(""))
								{
									text2 = surveyAnswer.CODE;
								}
								else if (surveyAnswer.CODE != global::GClass0.smethod_0(""))
								{
									text2 = text2 + this.MulitpleSeparator + surveyAnswer.CODE;
								}
							}
						}
					}
					if (flag)
					{
						text = text2;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text2;
					}
				}
			}
			bool flag3 = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				foreach (SurveyRandom surveyRandom2 in list2)
				{
					text3 = global::GClass0.smethod_0("");
					flag3 = true;
					foreach (SurveyRandom surveyRandom3 in list3)
					{
						text4 = string.Concat(new string[]
						{
							string_0,
							global::GClass0.smethod_0("]œ"),
							surveyRandom2.CODE,
							global::GClass0.smethod_0("]œ"),
							surveyRandom3.CODE,
							global::GClass0.smethod_0("^")
						});
						bool flag2 = true;
						text2 = global::GClass0.smethod_0("");
						foreach (SurveyAnswer surveyAnswer2 in list)
						{
							if ((surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer2.CODE;
									flag2 = false;
								}
								else if ((surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("@")) == 0)
								{
									if (text2 == global::GClass0.smethod_0(""))
									{
										text2 = surveyAnswer2.CODE;
									}
									else if (surveyAnswer2.CODE != global::GClass0.smethod_0(""))
									{
										text2 = text2 + this.MulitpleSeparator + surveyAnswer2.CODE;
									}
								}
							}
						}
						if (flag3)
						{
							text3 = text2;
							flag3 = false;
						}
						else
						{
							text3 = text3 + this.SeparatorB + text2;
						}
					}
					if (flag)
					{
						text = text3;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text3;
					}
				}
			}
			return text;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000AB60 File Offset: 0x00008D60
		public string GetCircleAnswer_C(string string_0, string string_1 = "C")
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			string text4 = global::GClass0.smethod_0("");
			string string_2 = global::GClass0.smethod_0("");
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			List<SurveyAnswer> list2 = new List<SurveyAnswer>();
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			string_2 = ((byName.GROUP_LEVEL == global::GClass0.smethod_0("@")) ? byName.GROUP_CODEA : byName.GROUP_CODEB);
			if (list.Count == 0)
			{
				list = this.oSurveyAnswerDal.GetCircleList_C(this.SurveyID, string_0, string_1);
			}
			if (this.PageAnswer.Count > 0 && list.Count == 0)
			{
				foreach (VEAnswer veanswer in this.PageAnswer)
				{
					if (this.method_9(veanswer.QUESTION_NAME, string_0, byName.GROUP_LEVEL, string_1))
					{
						list.Add(new SurveyAnswer
						{
							CODE = veanswer.CODE,
							QUESTION_NAME = veanswer.QUESTION_NAME
						});
					}
					else if (this.method_9(veanswer.QUESTION_NAME, string_2, byName.GROUP_LEVEL, global::GClass0.smethod_0("S")))
					{
						list2.Add(new SurveyAnswer
						{
							CODE = veanswer.CODE,
							QUESTION_NAME = veanswer.QUESTION_NAME
						});
					}
				}
			}
			List<SurveyRandom> list3 = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEA);
			List<SurveyRandom> list4 = new List<SurveyRandom>();
			foreach (SurveyRandom surveyRandom in list3)
			{
				if (surveyRandom.CODE == this.CircleACode)
				{
					break;
				}
				if (surveyRandom.QUESTION_NAME != global::GClass0.smethod_0("NŖɏ͑"))
				{
					list4.Add(surveyRandom);
				}
			}
			List<SurveyAnswer> list5 = new List<SurveyAnswer>();
			foreach (SurveyAnswer surveyAnswer in list2)
			{
				if (surveyAnswer.CODE == this.CircleBCode)
				{
					break;
				}
				if (surveyAnswer.QUESTION_NAME != global::GClass0.smethod_0("NŖɏ͑"))
				{
					list5.Add(surveyAnswer);
				}
			}
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				text4 = string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("^"),
					string_1,
					this.CircleACode,
					global::GClass0.smethod_0("^")
				});
			}
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				text4 = string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("^"),
					string_1,
					this.CircleBCode,
					global::GClass0.smethod_0("^")
				});
			}
			for (int i = list.Count<SurveyAnswer>() - 1; i >= 0; i--)
			{
				if ((list[i].QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
				{
					list.Remove(list[i]);
				}
			}
			bool flag = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				foreach (SurveyRandom surveyRandom2 in list4)
				{
					text4 = string.Concat(new string[]
					{
						string_0,
						global::GClass0.smethod_0("^"),
						string_1,
						surveyRandom2.CODE,
						global::GClass0.smethod_0("^")
					});
					bool flag2 = true;
					text2 = global::GClass0.smethod_0("");
					foreach (SurveyAnswer surveyAnswer2 in list)
					{
						if ((surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer2.CODE;
								flag2 = false;
							}
							else if ((surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("@")) == 0)
							{
								if (text2 == global::GClass0.smethod_0(""))
								{
									text2 = surveyAnswer2.CODE;
								}
								else if (surveyAnswer2.CODE != global::GClass0.smethod_0(""))
								{
									text2 = text2 + this.MulitpleSeparator + surveyAnswer2.CODE;
								}
							}
						}
					}
					if (flag)
					{
						text = text2;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text2;
					}
				}
			}
			bool flag3 = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				foreach (SurveyRandom surveyRandom3 in list4)
				{
					text3 = global::GClass0.smethod_0("");
					flag3 = true;
					foreach (SurveyAnswer surveyAnswer3 in list5)
					{
						text4 = string.Concat(new string[]
						{
							string_0,
							global::GClass0.smethod_0("]œ"),
							surveyRandom3.CODE,
							global::GClass0.smethod_0("^"),
							string_1,
							surveyAnswer3.CODE,
							global::GClass0.smethod_0("^")
						});
						bool flag2 = true;
						text2 = global::GClass0.smethod_0("");
						foreach (SurveyAnswer surveyAnswer4 in list)
						{
							if ((surveyAnswer4.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer4.CODE;
									flag2 = false;
								}
								else if ((surveyAnswer4.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("@")) == 0)
								{
									if (text2 == global::GClass0.smethod_0(""))
									{
										text2 = surveyAnswer4.CODE;
									}
									else if (surveyAnswer4.CODE != global::GClass0.smethod_0(""))
									{
										text2 = text2 + this.MulitpleSeparator + surveyAnswer4.CODE;
									}
								}
							}
						}
						if (flag3)
						{
							text3 = text2;
							flag3 = false;
						}
						else
						{
							text3 = text3 + this.SeparatorB + text2;
						}
					}
					if (flag)
					{
						text = text3;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text3;
					}
				}
			}
			if (byName.QUESTION_TYPE == 10)
			{
				bool flag2 = true;
				foreach (SurveyAnswer surveyAnswer5 in list)
				{
					if (flag2)
					{
						text = surveyAnswer5.CODE;
						flag2 = false;
					}
					else
					{
						text = text + this.SeparatorA + surveyAnswer5.CODE;
					}
				}
			}
			return text;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public string GetAllCircleAnswer_C(string string_0, string string_1 = "C")
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			string text4 = global::GClass0.smethod_0("");
			global::GClass0.smethod_0("");
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			if (byName.GROUP_CODEB != global::GClass0.smethod_0(""))
			{
				byName.GROUP_LEVEL = global::GClass0.smethod_0("C");
			}
			else if (byName.GROUP_CODEA != global::GClass0.smethod_0(""))
			{
				byName.GROUP_LEVEL = global::GClass0.smethod_0("@");
			}
			string text5 = (byName.GROUP_LEVEL == global::GClass0.smethod_0("@")) ? byName.GROUP_CODEA : byName.GROUP_CODEB;
			if (list.Count == 0)
			{
				list = this.oSurveyAnswerDal.GetCircleList_C(this.SurveyID, string_0, string_1);
			}
			if (this.PageAnswer.Count > 0)
			{
				foreach (VEAnswer veanswer in this.PageAnswer)
				{
					if (this.method_9(veanswer.QUESTION_NAME, string_0, byName.GROUP_LEVEL, string_1))
					{
						list.Add(new SurveyAnswer
						{
							CODE = veanswer.CODE,
							QUESTION_NAME = veanswer.QUESTION_NAME
						});
					}
				}
			}
			List<SurveyRandom> list2 = this.method_0(this.SurveyID, byName.GROUP_CODEA);
			List<SurveyRandom> list3 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				list3 = this.method_0(this.SurveyID, byName.GROUP_CODEB);
			}
			bool flag = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				foreach (SurveyRandom surveyRandom in list2)
				{
					text4 = string.Concat(new string[]
					{
						string_0,
						global::GClass0.smethod_0("^"),
						string_1,
						surveyRandom.CODE,
						global::GClass0.smethod_0("^")
					});
					bool flag2 = true;
					text2 = global::GClass0.smethod_0("");
					foreach (SurveyAnswer surveyAnswer in list)
					{
						if ((surveyAnswer.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer.CODE;
								flag2 = false;
							}
							else if ((surveyAnswer.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("@")) == 0)
							{
								if (text2 == global::GClass0.smethod_0(""))
								{
									text2 = surveyAnswer.CODE;
								}
								else if (surveyAnswer.CODE != global::GClass0.smethod_0(""))
								{
									text2 = text2 + this.MulitpleSeparator + surveyAnswer.CODE;
								}
							}
						}
					}
					if (flag)
					{
						text = text2;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text2;
					}
				}
			}
			bool flag3 = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				foreach (SurveyRandom surveyRandom2 in list2)
				{
					text3 = global::GClass0.smethod_0("");
					flag3 = true;
					foreach (SurveyRandom surveyRandom3 in list3)
					{
						text4 = string.Concat(new string[]
						{
							string_0,
							global::GClass0.smethod_0("]œ"),
							surveyRandom2.CODE,
							global::GClass0.smethod_0("^"),
							string_1,
							surveyRandom3.CODE,
							global::GClass0.smethod_0("^")
						});
						bool flag2 = true;
						text2 = global::GClass0.smethod_0("");
						foreach (SurveyAnswer surveyAnswer2 in list)
						{
							if ((surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text4) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer2.CODE;
									flag2 = false;
								}
								else if ((surveyAnswer2.QUESTION_NAME + global::GClass0.smethod_0("]ŀ")).IndexOf(text4 + global::GClass0.smethod_0("@")) == 0)
								{
									if (text2 == global::GClass0.smethod_0(""))
									{
										text2 = surveyAnswer2.CODE;
									}
									else if (surveyAnswer2.CODE != global::GClass0.smethod_0(""))
									{
										text2 = text2 + this.MulitpleSeparator + surveyAnswer2.CODE;
									}
								}
							}
						}
						if (flag3)
						{
							text3 = text2;
							flag3 = false;
						}
						else
						{
							text3 = text3 + this.SeparatorB + text2;
						}
					}
					if (flag)
					{
						text = text3;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text3;
					}
				}
			}
			if (byName.QUESTION_TYPE == 10)
			{
				bool flag2 = true;
				foreach (SurveyAnswer surveyAnswer3 in list)
				{
					if (flag2)
					{
						text = surveyAnswer3.CODE;
						flag2 = false;
					}
					else
					{
						text = text + this.SeparatorA + surveyAnswer3.CODE;
					}
				}
			}
			return text;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000BA44 File Offset: 0x00009C44
		public string GetCurrentCircleOtherText(string string_0, string string_1 = "")
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				text2 = ((string_1 == global::GClass0.smethod_0("")) ? (string_0 + global::GClass0.smethod_0("]œ") + this.CircleACode + global::GClass0.smethod_0("[Ōɖ͉")) : string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("YŊɐ͋ѝՂ"),
					string_1
				}));
			}
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				text2 = ((string_1 == global::GClass0.smethod_0("")) ? string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("]œ"),
					this.CircleBCode,
					global::GClass0.smethod_0("[Ōɖ͉")
				}) : string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("]œ"),
					this.CircleBCode,
					global::GClass0.smethod_0("YŊɐ͋ѝՂ"),
					string_1
				}));
			}
			foreach (VEAnswer veanswer in this.PageAnswer)
			{
				if ((veanswer.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(text2 + global::GClass0.smethod_0("^")) == 0)
				{
					text = veanswer.CODE;
				}
			}
			if (text == global::GClass0.smethod_0(""))
			{
				text = this.oSurveyAnswerDal.GetOneCode(this.SurveyID, text2);
			}
			return text;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000BC60 File Offset: 0x00009E60
		public string GetCircleOtherText(string string_0, string string_1 = "")
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			string value = global::GClass0.smethod_0("");
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			List<SurveyRandom> list = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEA);
			List<SurveyRandom> list2 = new List<SurveyRandom>();
			foreach (SurveyRandom surveyRandom in list)
			{
				if (surveyRandom.CODE == this.CircleACode)
				{
					break;
				}
				if (surveyRandom.QUESTION_NAME != global::GClass0.smethod_0("NŖɏ͑"))
				{
					list2.Add(surveyRandom);
				}
			}
			List<SurveyRandom> list3 = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEB);
			List<SurveyRandom> list4 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				foreach (SurveyRandom surveyRandom2 in list3)
				{
					if (surveyRandom2.CODE == this.CircleBCode)
					{
						break;
					}
					if (surveyRandom2.QUESTION_NAME != global::GClass0.smethod_0("NŖɏ͑"))
					{
						list4.Add(surveyRandom2);
					}
				}
			}
			List<SurveyAnswer> circleListOther = this.oSurveyAnswerDal.GetCircleListOther(this.SurveyID, string_0);
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				value = ((string_1 == global::GClass0.smethod_0("")) ? (string_0 + global::GClass0.smethod_0("]œ") + this.CircleACode + global::GClass0.smethod_0("[Ōɖ͉")) : string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("YŊɐ͋ѝՂ"),
					string_1
				}));
			}
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				value = ((string_1 == global::GClass0.smethod_0("")) ? string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("]œ"),
					this.CircleBCode,
					global::GClass0.smethod_0("[Ōɖ͉")
				}) : string.Concat(new string[]
				{
					string_0,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("]œ"),
					this.CircleBCode,
					global::GClass0.smethod_0("YŊɐ͋ѝՂ"),
					string_1
				}));
			}
			for (int i = circleListOther.Count<SurveyAnswer>() - 1; i >= 0; i--)
			{
				if (circleListOther[i].QUESTION_NAME.IndexOf(value) == 0)
				{
					circleListOther.Remove(circleListOther[i]);
				}
			}
			bool flag = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				foreach (SurveyRandom surveyRandom3 in list2)
				{
					value = string_0 + global::GClass0.smethod_0("]œ") + surveyRandom3.CODE + global::GClass0.smethod_0("[Ōɖ͉");
					bool flag2 = true;
					text2 = global::GClass0.smethod_0("");
					foreach (SurveyAnswer surveyAnswer in circleListOther)
					{
						if (surveyAnswer.QUESTION_NAME.IndexOf(value) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer.CODE;
								flag2 = false;
							}
							else
							{
								text2 = text2 + this.MulitpleSeparator + surveyAnswer.CODE;
							}
						}
					}
					if (flag)
					{
						text = text2;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text2;
					}
				}
			}
			bool flag3 = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				foreach (SurveyRandom surveyRandom4 in list2)
				{
					text3 = global::GClass0.smethod_0("");
					flag3 = true;
					foreach (SurveyRandom surveyRandom5 in list4)
					{
						value = string.Concat(new string[]
						{
							string_0,
							global::GClass0.smethod_0("]œ"),
							surveyRandom4.CODE,
							global::GClass0.smethod_0("]œ"),
							surveyRandom5.CODE,
							global::GClass0.smethod_0("[Ōɖ͉")
						});
						bool flag2 = true;
						text2 = global::GClass0.smethod_0("");
						foreach (SurveyAnswer surveyAnswer2 in circleListOther)
						{
							if (surveyAnswer2.QUESTION_NAME.IndexOf(value) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer2.CODE;
									flag2 = false;
								}
								else
								{
									text2 = text2 + this.MulitpleSeparator + surveyAnswer2.CODE;
								}
							}
						}
						if (flag3)
						{
							text3 = text2;
							flag3 = false;
						}
						else
						{
							text3 = text3 + this.SeparatorB + text2;
						}
					}
					if (flag)
					{
						text = text3;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text3;
					}
				}
			}
			return text;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		public string GetAllCircleOtherText(string string_0, string string_1 = "")
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			string value = global::GClass0.smethod_0("");
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			List<SurveyRandom> list = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEA);
			for (int i = list.Count<SurveyRandom>() - 1; i >= 0; i--)
			{
				if (list[i].QUESTION_NAME == global::GClass0.smethod_0("NŖɏ͑"))
				{
					list.Remove(list[i]);
				}
			}
			List<SurveyRandom> list2 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				list2 = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEB);
				for (int j = list2.Count<SurveyRandom>() - 1; j >= 0; j--)
				{
					if (list2[j].QUESTION_NAME == global::GClass0.smethod_0("NŖɏ͑"))
					{
						list2.Remove(list2[j]);
					}
				}
			}
			List<SurveyAnswer> circleListOther = this.oSurveyAnswerDal.GetCircleListOther(this.SurveyID, string_0);
			bool flag = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				foreach (SurveyRandom surveyRandom in list)
				{
					value = ((string_1 == global::GClass0.smethod_0("")) ? (string_0 + global::GClass0.smethod_0("]œ") + surveyRandom.CODE + global::GClass0.smethod_0("[Ōɖ͉")) : string.Concat(new string[]
					{
						string_0,
						global::GClass0.smethod_0("]œ"),
						surveyRandom.CODE,
						global::GClass0.smethod_0("YŊɐ͋ѝՂ"),
						string_1
					}));
					bool flag2 = true;
					text2 = global::GClass0.smethod_0("");
					foreach (SurveyAnswer surveyAnswer in circleListOther)
					{
						if (surveyAnswer.QUESTION_NAME.IndexOf(value) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer.CODE;
								flag2 = false;
							}
							else
							{
								text2 = text2 + this.MulitpleSeparator + surveyAnswer.CODE;
							}
						}
					}
					if (flag)
					{
						text = text2;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text2;
					}
				}
			}
			bool flag3 = true;
			if (byName.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				foreach (SurveyRandom surveyRandom2 in list)
				{
					text3 = global::GClass0.smethod_0("");
					flag3 = true;
					foreach (SurveyRandom surveyRandom3 in list2)
					{
						value = ((string_1 == global::GClass0.smethod_0("")) ? string.Concat(new string[]
						{
							string_0,
							global::GClass0.smethod_0("]œ"),
							surveyRandom2.CODE,
							global::GClass0.smethod_0("]œ"),
							surveyRandom3.CODE,
							global::GClass0.smethod_0("[Ōɖ͉")
						}) : string.Concat(new string[]
						{
							string_0,
							global::GClass0.smethod_0("]œ"),
							surveyRandom2.CODE,
							global::GClass0.smethod_0("]œ"),
							surveyRandom3.CODE,
							global::GClass0.smethod_0("YŊɐ͋ѝՂ"),
							string_1
						}));
						bool flag2 = true;
						text2 = global::GClass0.smethod_0("");
						foreach (SurveyAnswer surveyAnswer2 in circleListOther)
						{
							if (surveyAnswer2.QUESTION_NAME.IndexOf(value) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer2.CODE;
									flag2 = false;
								}
								else
								{
									text2 = text2 + this.MulitpleSeparator + surveyAnswer2.CODE;
								}
							}
						}
						if (flag3)
						{
							text3 = text2;
							flag3 = false;
						}
						else
						{
							text3 = text3 + this.SeparatorB + text2;
						}
					}
					if (flag)
					{
						text = text3;
						flag = false;
					}
					else
					{
						text = text + this.SeparatorA + text3;
					}
				}
			}
			return text;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public string[] GetDetailsByQName(string string_0)
		{
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			List<SurveyDetail> details = this.oSurveyDetailDal.GetDetails(byName.DETAIL_ID);
			string[] result;
			if (details.Count > 0)
			{
				string[] array = new string[details.Count];
				for (int i = 0; i < details.Count; i++)
				{
					array[i] = details[i].CODE.ToString();
				}
				result = array;
			}
			else
			{
				string[] array2 = new string[1];
				result = array2;
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000C828 File Offset: 0x0000AA28
		private List<SurveyRandom> method_0(string string_0, string string_1)
		{
			List<SurveyRandom> list = this.oSurveyRandomDal.GetList(string_0, string_1);
			if (list.Count > 0)
			{
				for (int i = list.Count<SurveyRandom>() - 1; i >= 0; i--)
				{
					if (list[i].QUESTION_NAME == global::GClass0.smethod_0("NŖɏ͑"))
					{
						list.Remove(list[i]);
					}
				}
			}
			else
			{
				SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_1);
				List<SurveyDetail> details = this.oSurveyDetailDal.GetDetails(byName.DETAIL_ID);
				foreach (SurveyDetail surveyDetail in details)
				{
					list.Add(new SurveyRandom
					{
						CODE = surveyDetail.CODE
					});
				}
			}
			return list;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000C918 File Offset: 0x0000AB18
		public string[] GetOption(string string_0)
		{
			LogicEngine logicEngine = new LogicEngine();
			logicEngine.SurveyID = this.SurveyID;
			logicEngine.CircleACode = this.CircleACode;
			logicEngine.CircleBCode = this.CircleBCode;
			logicEngine.PageAnswer = this.PageAnswer;
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			string[] result;
			if (byName.LIMIT_LOGIC == global::GClass0.smethod_0(""))
			{
				string[] detailsByQName = this.GetDetailsByQName(string_0);
				result = detailsByQName;
			}
			else
			{
				string[] array = logicEngine.aryCode(byName.LIMIT_LOGIC, ',');
				result = array;
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		private string method_1(string string_0)
		{
			string text = global::GClass0.smethod_0("");
			bool flag = true;
			string text2 = string_0 + global::GClass0.smethod_0("]ŀ");
			int length = text2.Length;
			if (this.PageAnswer.Count > 0)
			{
				foreach (VEAnswer veanswer in this.PageAnswer)
				{
					if (veanswer.QUESTION_NAME == string_0)
					{
						if (flag)
						{
							text = veanswer.CODE;
							flag = false;
						}
						else
						{
							text = text + this.MulitpleSeparator + veanswer.CODE;
						}
						break;
					}
				}
				if (text == global::GClass0.smethod_0(""))
				{
					foreach (VEAnswer veanswer2 in this.PageAnswer)
					{
						if (veanswer2.QUESTION_NAME.Length >= length && veanswer2.QUESTION_NAME.Substring(0, length) == text2)
						{
							if (flag)
							{
								text = veanswer2.CODE;
								flag = false;
							}
							else
							{
								text = text + this.MulitpleSeparator + veanswer2.CODE;
							}
						}
					}
				}
			}
			if (text == global::GClass0.smethod_0(""))
			{
				List<SurveyAnswer> listByCode = this.oSurveyAnswerDal.GetListByCode(this.SurveyID, string_0);
				foreach (SurveyAnswer surveyAnswer in listByCode)
				{
					if (surveyAnswer.QUESTION_NAME == string_0)
					{
						if (flag)
						{
							text = surveyAnswer.CODE;
							flag = false;
						}
						else
						{
							text = text + this.MulitpleSeparator + surveyAnswer.CODE;
						}
						break;
					}
				}
				if (text == global::GClass0.smethod_0(""))
				{
					string text3 = string_0 + global::GClass0.smethod_0("]ŀ");
					foreach (SurveyAnswer surveyAnswer2 in listByCode)
					{
						if (surveyAnswer2.QUESTION_NAME.Length >= length && (this.method_6(surveyAnswer2.QUESTION_NAME, 4) != global::GClass0.smethod_0("[Ōɖ͉") && this.method_4(surveyAnswer2.QUESTION_NAME, text3.Length) == text3))
						{
							if (flag)
							{
								text = surveyAnswer2.CODE;
								flag = false;
							}
							else
							{
								text = text + this.MulitpleSeparator + surveyAnswer2.CODE;
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000CC74 File Offset: 0x0000AE74
		private string method_2(string string_0)
		{
			string text = global::GClass0.smethod_0("");
			bool flag = true;
			string text2 = global::GClass0.smethod_0("[Ōɖ͉");
			int num = string_0.Length + text2.Length;
			if (this.PageAnswer.Count > 0)
			{
				foreach (VEAnswer veanswer in this.PageAnswer)
				{
					if ((veanswer.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(string_0 + global::GClass0.smethod_0("^")) == 0 && veanswer.QUESTION_NAME.IndexOf(text2) > 0)
					{
						if (flag)
						{
							text = veanswer.CODE;
							flag = false;
						}
						else
						{
							text = text + this.MulitpleSeparator + veanswer.CODE;
						}
						break;
					}
				}
			}
			if (text == global::GClass0.smethod_0(""))
			{
				List<SurveyAnswer> listByCode = this.oSurveyAnswerDal.GetListByCode(this.SurveyID, string_0);
				foreach (SurveyAnswer surveyAnswer in listByCode)
				{
					if ((surveyAnswer.QUESTION_NAME + global::GClass0.smethod_0("^")).IndexOf(string_0 + global::GClass0.smethod_0("^")) == 0 && surveyAnswer.QUESTION_NAME.IndexOf(text2) > 0)
					{
						if (flag)
						{
							text = surveyAnswer.CODE;
							flag = false;
						}
						else
						{
							text = text + this.MulitpleSeparator + surveyAnswer.CODE;
						}
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000CE34 File Offset: 0x0000B034
		private string method_3(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x060000FD RID: 253 RVA: 0x00007124 File Offset: 0x00005324
		private string method_4(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007158 File Offset: 0x00005358
		private string method_5(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x060000FF RID: 255 RVA: 0x000071B4 File Offset: 0x000053B4
		private string method_6(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		private int method_7(string string_0)
		{
			int result;
			if (string_0 == global::GClass0.smethod_0(""))
			{
				result = 0;
			}
			else if (string_0 == global::GClass0.smethod_0("1"))
			{
				result = 0;
			}
			else if (string_0 == global::GClass0.smethod_0("/ı"))
			{
				result = 0;
			}
			else
			{
				result = (this.method_8(string_0) ? Convert.ToInt32(string_0) : 0);
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000CF10 File Offset: 0x0000B110
		private bool method_8(string string_0)
		{
			Regex regex = new Regex(global::GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ"));
			return regex.IsMatch(string_0);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000CF34 File Offset: 0x0000B134
		private bool method_9(string string_0, string string_1, string string_2 = "A", string string_3 = "R")
		{
			bool flag = false;
			string a = this.method_4(string_0, string_1.Length + 1);
			bool result;
			if (a != string_1 && a != string_1 + global::GClass0.smethod_0("^"))
			{
				result = flag;
			}
			else
			{
				if (string_2 == global::GClass0.smethod_0("@"))
				{
					string pattern = string_1 + global::GClass0.smethod_0("^") + string_3 + global::GClass0.smethod_0("1ńɳͭФԸٮܻ࡭स੓୪౶ഽวཷၖᅉቛ።ᑾᔵᘯ᝿ᠨ");
					Regex regex = new Regex(pattern);
					flag = regex.IsMatch(string_0);
				}
				else if (string_2 == global::GClass0.smethod_0("C"))
				{
					string pattern2 = string_1 + global::GClass0.smethod_0("VŚɛ͢ѾԵدݿ࡞") + string_3 + global::GClass0.smethod_0("1ńɳͭФԸٮܻ࡭स੓୪౶ഽวཷၖᅉቛ።ᑾᔵᘯ᝿ᠨ");
					Regex regex2 = new Regex(pattern2);
					flag = regex2.IsMatch(string_0);
				}
				else if (string_0 == string_1)
				{
					flag = true;
				}
				else
				{
					string pattern3 = string_1 + global::GClass0.smethod_0("Wņɚ͡ѿԲخݼ");
					Regex regex3 = new Regex(pattern3);
					flag = regex3.IsMatch(string_0);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x04000074 RID: 116
		private string MulitpleSeparator = global::GClass0.smethod_0("-");

		// Token: 0x04000075 RID: 117
		private string SeparatorA;

		// Token: 0x04000076 RID: 118
		private string SeparatorB;

		// Token: 0x04000077 RID: 119
		public List<VEAnswer> PageAnswer = new List<VEAnswer>();

		// Token: 0x04000078 RID: 120
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x04000079 RID: 121
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x0400007A RID: 122
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x0400007B RID: 123
		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();
	}
}
