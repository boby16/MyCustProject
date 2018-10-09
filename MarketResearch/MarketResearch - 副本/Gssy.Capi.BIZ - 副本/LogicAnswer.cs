using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class LogicAnswer
	{
		public string SurveyID { get; set; }

		public string CircleACode { get; set; }

		public string CircleBCode { get; set; }

		public LogicAnswer()
		{
			this.SeparatorA = '\u0011'.ToString();
			this.SeparatorB = '\u0012'.ToString();
		}

		public string GetAnswer(string string_0)
		{
			return this.method_1(string_0);
		}

		public string GetText(string string_0, string string_1)
		{
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			return this.oSurveyDetailDal.GetCodeText(byName.DETAIL_ID, string_1);
		}

		public string GetDetailsText(string string_0, string string_1)
		{
			return this.oSurveyDetailDal.GetCodeText(string_0, string_1);
		}

		public string GetOtherText(string string_0, string string_1 = "")
		{
			string text = "";
			string text2 = (string_1 == "") ? (string_0 + "_OTH") : (string_0 + "_OTH_C" + string_1);
			foreach (VEAnswer veanswer in this.PageAnswer)
			{
				if ((veanswer.QUESTION_NAME + "_").IndexOf(text2 + "_") == 0)
				{
					text = veanswer.CODE;
				}
			}
			if (text == "")
			{
				text = this.oSurveyAnswerDal.GetOneCode(this.SurveyID, text2);
			}
			return text;
		}

		public string GetCurrentCircleAnswer(string string_0)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			if (byName.GROUP_LEVEL == "A")
			{
				text4 = string_0 + "_R" + this.CircleACode;
			}
			if (byName.GROUP_LEVEL == "B")
			{
				text4 = string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_R",
					this.CircleBCode
				});
			}
			List<SurveyAnswer> circleListByConbine = this.oSurveyAnswerDal.GetCircleListByConbine(this.SurveyID, text4, false);
			string value = string_0 + "_";
			foreach (VEAnswer veanswer in this.PageAnswer)
			{
				if ((veanswer.QUESTION_NAME + "_").IndexOf(value) == 0)
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
			if (byName.GROUP_LEVEL == "A")
			{
				bool flag3 = true;
				text2 = "";
				foreach (SurveyAnswer surveyAnswer2 in circleListByConbine)
				{
					if (surveyAnswer2.QUESTION_NAME == text4)
					{
						text2 = surveyAnswer2.CODE;
						flag3 = false;
						flag2 = true;
					}
					if (!flag2 && (surveyAnswer2.QUESTION_NAME + "_A").IndexOf(text4 + "_A") == 0)
					{
						if (flag3)
						{
							text2 = surveyAnswer2.CODE;
							flag3 = false;
						}
						else if (text2 == "")
						{
							text2 = surveyAnswer2.CODE;
						}
						else if (surveyAnswer2.CODE != "")
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
			if (byName.GROUP_LEVEL == "B")
			{
				text3 = "";
				flag4 = true;
				bool flag3 = true;
				text2 = "";
				foreach (SurveyAnswer surveyAnswer3 in circleListByConbine)
				{
					if (surveyAnswer3.QUESTION_NAME == text4)
					{
						text2 = surveyAnswer3.CODE;
						flag3 = false;
						flag2 = true;
					}
					if (!flag2 && (surveyAnswer3.QUESTION_NAME + "_A").IndexOf(text4 + "_A") == 0)
					{
						if (flag3)
						{
							text2 = surveyAnswer3.CODE;
							flag3 = false;
						}
						else if (text2 == "")
						{
							text2 = surveyAnswer3.CODE;
						}
						else if (surveyAnswer3.CODE != "")
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

		public string GetCircleAnswer(string string_0)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
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
				if (surveyRandom.QUESTION_NAME != GClass0.smethod_0("NŖɏ͑"))
				{
					list3.Add(surveyRandom);
				}
			}
			List<SurveyRandom> list4 = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEB);
			List<SurveyRandom> list5 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == "B")
			{
				foreach (SurveyRandom surveyRandom2 in list4)
				{
					if (surveyRandom2.CODE == this.CircleBCode)
					{
						break;
					}
					if (surveyRandom2.QUESTION_NAME != GClass0.smethod_0("NŖɏ͑"))
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
				text4 = string_0 + "_R";
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
			if (byName.GROUP_LEVEL == "A")
			{
				text4 = string_0 + "_R" + this.CircleACode + "_";
			}
			if (byName.GROUP_LEVEL == "B")
			{
				text4 = string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_R",
					this.CircleBCode,
					"_"
				});
			}
			for (int i = list.Count<SurveyAnswer>() - 1; i >= 0; i--)
			{
				if ((list[i].QUESTION_NAME + "_").IndexOf(text4) == 0)
				{
					list.Remove(list[i]);
				}
			}
			bool flag = true;
			if (byName.GROUP_LEVEL == "A")
			{
				foreach (SurveyRandom surveyRandom3 in list3)
				{
					text4 = string_0 + "_R" + surveyRandom3.CODE + "_";
					bool flag2 = true;
					text2 = "";
					foreach (SurveyAnswer surveyAnswer in list)
					{
						if ((surveyAnswer.QUESTION_NAME + "_").IndexOf(text4) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer.CODE;
								flag2 = false;
							}
							else if ((surveyAnswer.QUESTION_NAME + "_A").IndexOf(text4 + "A") == 0)
							{
								if (text2 == "")
								{
									text2 = surveyAnswer.CODE;
								}
								else if (surveyAnswer.CODE != "")
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
			if (byName.GROUP_LEVEL == "B")
			{
				foreach (SurveyRandom surveyRandom4 in list3)
				{
					text3 = "";
					flag3 = true;
					foreach (SurveyRandom surveyRandom5 in list5)
					{
						text4 = string.Concat(new string[]
						{
							string_0,
							"_R",
							surveyRandom4.CODE,
							"_R",
							surveyRandom5.CODE,
							"_"
						});
						bool flag2 = true;
						text2 = "";
						foreach (SurveyAnswer surveyAnswer2 in list)
						{
							if ((surveyAnswer2.QUESTION_NAME + "_").IndexOf(text4) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer2.CODE;
									flag2 = false;
								}
								else if ((surveyAnswer2.QUESTION_NAME + "_A").IndexOf(text4 + "A") == 0)
								{
									if (text2 == "")
									{
										text2 = surveyAnswer2.CODE;
									}
									else if (surveyAnswer2.CODE != "")
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

		public string GetAllCircleAnswer(string string_0)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			List<SurveyRandom> list2 = this.method_0(this.SurveyID, byName.GROUP_CODEA);
			List<SurveyRandom> list3 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == "B")
			{
				list3 = this.method_0(this.SurveyID, byName.GROUP_CODEB);
			}
			if (list.Count == 0)
			{
				list = this.oSurveyAnswerDal.GetCircleList(this.SurveyID, string_0, false);
			}
			if (this.PageAnswer.Count > 0)
			{
				text4 = string_0 + "_R";
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
			if (byName.GROUP_LEVEL == "A")
			{
				foreach (SurveyRandom surveyRandom in list2)
				{
					text4 = string_0 + "_R" + surveyRandom.CODE + "_";
					bool flag2 = true;
					text2 = "";
					foreach (SurveyAnswer surveyAnswer in list)
					{
						if ((surveyAnswer.QUESTION_NAME + "_").IndexOf(text4) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer.CODE;
								flag2 = false;
							}
							else if ((surveyAnswer.QUESTION_NAME + "_A").IndexOf(text4 + "A") == 0)
							{
								if (text2 == "")
								{
									text2 = surveyAnswer.CODE;
								}
								else if (surveyAnswer.CODE != "")
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
			if (byName.GROUP_LEVEL == "B")
			{
				foreach (SurveyRandom surveyRandom2 in list2)
				{
					text3 = "";
					flag3 = true;
					foreach (SurveyRandom surveyRandom3 in list3)
					{
						text4 = string.Concat(new string[]
						{
							string_0,
							"_R",
							surveyRandom2.CODE,
							"_R",
							surveyRandom3.CODE,
							"_"
						});
						bool flag2 = true;
						text2 = "";
						foreach (SurveyAnswer surveyAnswer2 in list)
						{
							if ((surveyAnswer2.QUESTION_NAME + "_").IndexOf(text4) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer2.CODE;
									flag2 = false;
								}
								else if ((surveyAnswer2.QUESTION_NAME + "_A").IndexOf(text4 + "A") == 0)
								{
									if (text2 == "")
									{
										text2 = surveyAnswer2.CODE;
									}
									else if (surveyAnswer2.CODE != "")
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

		public string GetCircleAnswer_C(string string_0, string string_1 = "C")
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string string_2 = "";
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			List<SurveyAnswer> list2 = new List<SurveyAnswer>();
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			string_2 = ((byName.GROUP_LEVEL == "A") ? byName.GROUP_CODEA : byName.GROUP_CODEB);
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
					else if (this.method_9(veanswer.QUESTION_NAME, string_2, byName.GROUP_LEVEL, "R"))
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
				if (surveyRandom.QUESTION_NAME != GClass0.smethod_0("NŖɏ͑"))
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
				if (surveyAnswer.QUESTION_NAME != GClass0.smethod_0("NŖɏ͑"))
				{
					list5.Add(surveyAnswer);
				}
			}
			if (byName.GROUP_LEVEL == "A")
			{
				text4 = string.Concat(new string[]
				{
					string_0,
					"_",
					string_1,
					this.CircleACode,
					"_"
				});
			}
			if (byName.GROUP_LEVEL == "B")
			{
				text4 = string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_",
					string_1,
					this.CircleBCode,
					"_"
				});
			}
			for (int i = list.Count<SurveyAnswer>() - 1; i >= 0; i--)
			{
				if ((list[i].QUESTION_NAME + "_").IndexOf(text4) == 0)
				{
					list.Remove(list[i]);
				}
			}
			bool flag = true;
			if (byName.GROUP_LEVEL == "A")
			{
				foreach (SurveyRandom surveyRandom2 in list4)
				{
					text4 = string.Concat(new string[]
					{
						string_0,
						"_",
						string_1,
						surveyRandom2.CODE,
						"_"
					});
					bool flag2 = true;
					text2 = "";
					foreach (SurveyAnswer surveyAnswer2 in list)
					{
						if ((surveyAnswer2.QUESTION_NAME + "_").IndexOf(text4) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer2.CODE;
								flag2 = false;
							}
							else if ((surveyAnswer2.QUESTION_NAME + "_A").IndexOf(text4 + "A") == 0)
							{
								if (text2 == "")
								{
									text2 = surveyAnswer2.CODE;
								}
								else if (surveyAnswer2.CODE != "")
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
			if (byName.GROUP_LEVEL == "B")
			{
				foreach (SurveyRandom surveyRandom3 in list4)
				{
					text3 = "";
					flag3 = true;
					foreach (SurveyAnswer surveyAnswer3 in list5)
					{
						text4 = string.Concat(new string[]
						{
							string_0,
							"_R",
							surveyRandom3.CODE,
							"_",
							string_1,
							surveyAnswer3.CODE,
							"_"
						});
						bool flag2 = true;
						text2 = "";
						foreach (SurveyAnswer surveyAnswer4 in list)
						{
							if ((surveyAnswer4.QUESTION_NAME + "_").IndexOf(text4) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer4.CODE;
									flag2 = false;
								}
								else if ((surveyAnswer4.QUESTION_NAME + "_A").IndexOf(text4 + "A") == 0)
								{
									if (text2 == "")
									{
										text2 = surveyAnswer4.CODE;
									}
									else if (surveyAnswer4.CODE != "")
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

		public string GetAllCircleAnswer_C(string string_0, string string_1 = "C")
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			if (byName.GROUP_CODEB != "")
			{
				byName.GROUP_LEVEL = "B";
			}
			else if (byName.GROUP_CODEA != "")
			{
				byName.GROUP_LEVEL = "A";
			}
			string text5 = (byName.GROUP_LEVEL == "A") ? byName.GROUP_CODEA : byName.GROUP_CODEB;
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
			if (byName.GROUP_LEVEL == "B")
			{
				list3 = this.method_0(this.SurveyID, byName.GROUP_CODEB);
			}
			bool flag = true;
			if (byName.GROUP_LEVEL == "A")
			{
				foreach (SurveyRandom surveyRandom in list2)
				{
					text4 = string.Concat(new string[]
					{
						string_0,
						"_",
						string_1,
						surveyRandom.CODE,
						"_"
					});
					bool flag2 = true;
					text2 = "";
					foreach (SurveyAnswer surveyAnswer in list)
					{
						if ((surveyAnswer.QUESTION_NAME + "_").IndexOf(text4) == 0)
						{
							if (flag2)
							{
								text2 = surveyAnswer.CODE;
								flag2 = false;
							}
							else if ((surveyAnswer.QUESTION_NAME + "_A").IndexOf(text4 + "A") == 0)
							{
								if (text2 == "")
								{
									text2 = surveyAnswer.CODE;
								}
								else if (surveyAnswer.CODE != "")
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
			if (byName.GROUP_LEVEL == "B")
			{
				foreach (SurveyRandom surveyRandom2 in list2)
				{
					text3 = "";
					flag3 = true;
					foreach (SurveyRandom surveyRandom3 in list3)
					{
						text4 = string.Concat(new string[]
						{
							string_0,
							"_R",
							surveyRandom2.CODE,
							"_",
							string_1,
							surveyRandom3.CODE,
							"_"
						});
						bool flag2 = true;
						text2 = "";
						foreach (SurveyAnswer surveyAnswer2 in list)
						{
							if ((surveyAnswer2.QUESTION_NAME + "_").IndexOf(text4) == 0)
							{
								if (flag2)
								{
									text2 = surveyAnswer2.CODE;
									flag2 = false;
								}
								else if ((surveyAnswer2.QUESTION_NAME + "_A").IndexOf(text4 + "A") == 0)
								{
									if (text2 == "")
									{
										text2 = surveyAnswer2.CODE;
									}
									else if (surveyAnswer2.CODE != "")
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

		public string GetCurrentCircleOtherText(string string_0, string string_1 = "")
		{
			string text = "";
			string text2 = "";
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			if (byName.GROUP_LEVEL == "A")
			{
				text2 = ((string_1 == "") ? (string_0 + "_R" + this.CircleACode + "_OTH") : string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_OTH_C",
					string_1
				}));
			}
			if (byName.GROUP_LEVEL == "B")
			{
				text2 = ((string_1 == "") ? string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_R",
					this.CircleBCode,
					"_OTH"
				}) : string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_R",
					this.CircleBCode,
					"_OTH_C",
					string_1
				}));
			}
			foreach (VEAnswer veanswer in this.PageAnswer)
			{
				if ((veanswer.QUESTION_NAME + "_").IndexOf(text2 + "_") == 0)
				{
					text = veanswer.CODE;
				}
			}
			if (text == "")
			{
				text = this.oSurveyAnswerDal.GetOneCode(this.SurveyID, text2);
			}
			return text;
		}

		public string GetCircleOtherText(string string_0, string string_1 = "")
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string value = "";
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			List<SurveyRandom> list = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEA);
			List<SurveyRandom> list2 = new List<SurveyRandom>();
			foreach (SurveyRandom surveyRandom in list)
			{
				if (surveyRandom.CODE == this.CircleACode)
				{
					break;
				}
				if (surveyRandom.QUESTION_NAME != GClass0.smethod_0("NŖɏ͑"))
				{
					list2.Add(surveyRandom);
				}
			}
			List<SurveyRandom> list3 = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEB);
			List<SurveyRandom> list4 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == "B")
			{
				foreach (SurveyRandom surveyRandom2 in list3)
				{
					if (surveyRandom2.CODE == this.CircleBCode)
					{
						break;
					}
					if (surveyRandom2.QUESTION_NAME != GClass0.smethod_0("NŖɏ͑"))
					{
						list4.Add(surveyRandom2);
					}
				}
			}
			List<SurveyAnswer> circleListOther = this.oSurveyAnswerDal.GetCircleListOther(this.SurveyID, string_0);
			if (byName.GROUP_LEVEL == "A")
			{
				value = ((string_1 == "") ? (string_0 + "_R" + this.CircleACode + "_OTH") : string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_OTH_C",
					string_1
				}));
			}
			if (byName.GROUP_LEVEL == "B")
			{
				value = ((string_1 == "") ? string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_R",
					this.CircleBCode,
					"_OTH"
				}) : string.Concat(new string[]
				{
					string_0,
					"_R",
					this.CircleACode,
					"_R",
					this.CircleBCode,
					"_OTH_C",
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
			if (byName.GROUP_LEVEL == "A")
			{
				foreach (SurveyRandom surveyRandom3 in list2)
				{
					value = string_0 + "_R" + surveyRandom3.CODE + "_OTH";
					bool flag2 = true;
					text2 = "";
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
			if (byName.GROUP_LEVEL == "B")
			{
				foreach (SurveyRandom surveyRandom4 in list2)
				{
					text3 = "";
					flag3 = true;
					foreach (SurveyRandom surveyRandom5 in list4)
					{
						value = string.Concat(new string[]
						{
							string_0,
							"_R",
							surveyRandom4.CODE,
							"_R",
							surveyRandom5.CODE,
							"_OTH"
						});
						bool flag2 = true;
						text2 = "";
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

		public string GetAllCircleOtherText(string string_0, string string_1 = "")
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string value = "";
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			List<SurveyRandom> list = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEA);
			for (int i = list.Count<SurveyRandom>() - 1; i >= 0; i--)
			{
				if (list[i].QUESTION_NAME == GClass0.smethod_0("NŖɏ͑"))
				{
					list.Remove(list[i]);
				}
			}
			List<SurveyRandom> list2 = new List<SurveyRandom>();
			if (byName.GROUP_LEVEL == "B")
			{
				list2 = this.oSurveyRandomDal.GetList(this.SurveyID, byName.GROUP_CODEB);
				for (int j = list2.Count<SurveyRandom>() - 1; j >= 0; j--)
				{
					if (list2[j].QUESTION_NAME == GClass0.smethod_0("NŖɏ͑"))
					{
						list2.Remove(list2[j]);
					}
				}
			}
			List<SurveyAnswer> circleListOther = this.oSurveyAnswerDal.GetCircleListOther(this.SurveyID, string_0);
			bool flag = true;
			if (byName.GROUP_LEVEL == "A")
			{
				foreach (SurveyRandom surveyRandom in list)
				{
					value = ((string_1 == "") ? (string_0 + "_R" + surveyRandom.CODE + "_OTH") : string.Concat(new string[]
					{
						string_0,
						"_R",
						surveyRandom.CODE,
						"_OTH_C",
						string_1
					}));
					bool flag2 = true;
					text2 = "";
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
			if (byName.GROUP_LEVEL == "B")
			{
				foreach (SurveyRandom surveyRandom2 in list)
				{
					text3 = "";
					flag3 = true;
					foreach (SurveyRandom surveyRandom3 in list2)
					{
						value = ((string_1 == "") ? string.Concat(new string[]
						{
							string_0,
							"_R",
							surveyRandom2.CODE,
							"_R",
							surveyRandom3.CODE,
							"_OTH"
						}) : string.Concat(new string[]
						{
							string_0,
							"_R",
							surveyRandom2.CODE,
							"_R",
							surveyRandom3.CODE,
							"_OTH_C",
							string_1
						}));
						bool flag2 = true;
						text2 = "";
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

		private List<SurveyRandom> method_0(string string_0, string string_1)
		{
			List<SurveyRandom> list = this.oSurveyRandomDal.GetList(string_0, string_1);
			if (list.Count > 0)
			{
				for (int i = list.Count<SurveyRandom>() - 1; i >= 0; i--)
				{
					if (list[i].QUESTION_NAME == GClass0.smethod_0("NŖɏ͑"))
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

		public string[] GetOption(string string_0)
		{
			LogicEngine logicEngine = new LogicEngine();
			logicEngine.SurveyID = this.SurveyID;
			logicEngine.CircleACode = this.CircleACode;
			logicEngine.CircleBCode = this.CircleBCode;
			logicEngine.PageAnswer = this.PageAnswer;
			SurveyDefine byName = this.oSurveyDefineDal.GetByName(string_0);
			string[] result;
			if (byName.LIMIT_LOGIC == "")
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

		private string method_1(string string_0)
		{
			string text = "";
			bool flag = true;
			string text2 = string_0 + "_A";
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
				if (text == "")
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
			if (text == "")
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
				if (text == "")
				{
					string text3 = string_0 + "_A";
					foreach (SurveyAnswer surveyAnswer2 in listByCode)
					{
						if (surveyAnswer2.QUESTION_NAME.Length >= length && (this.method_6(surveyAnswer2.QUESTION_NAME, 4) != "_OTH" && this.method_4(surveyAnswer2.QUESTION_NAME, text3.Length) == text3))
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

		private string method_2(string string_0)
		{
			string text = "";
			bool flag = true;
			string text2 = "_OTH";
			int num = string_0.Length + text2.Length;
			if (this.PageAnswer.Count > 0)
			{
				foreach (VEAnswer veanswer in this.PageAnswer)
				{
					if ((veanswer.QUESTION_NAME + "_").IndexOf(string_0 + "_") == 0 && veanswer.QUESTION_NAME.IndexOf(text2) > 0)
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
			if (text == "")
			{
				List<SurveyAnswer> listByCode = this.oSurveyAnswerDal.GetListByCode(this.SurveyID, string_0);
				foreach (SurveyAnswer surveyAnswer in listByCode)
				{
					if ((surveyAnswer.QUESTION_NAME + "_").IndexOf(string_0 + "_") == 0 && surveyAnswer.QUESTION_NAME.IndexOf(text2) > 0)
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

		private string method_4(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

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

		private string method_6(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		private int method_7(string string_0)
		{
			int result;
			if (string_0 == "")
			{
				result = 0;
			}
			else if (string_0 == "0")
			{
				result = 0;
			}
			else if (string_0 == "-0")
			{
				result = 0;
			}
			else
			{
				result = (this.method_8(string_0) ? Convert.ToInt32(string_0) : 0);
			}
			return result;
		}

		private bool method_8(string string_0)
		{
			Regex regex = new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$");
			return regex.IsMatch(string_0);
		}

		private bool method_9(string string_0, string string_1, string string_2 = "A", string string_3 = "R")
		{
			bool flag = false;
			string a = this.method_4(string_0, string_1.Length + 1);
			bool result;
			if (a != string_1 && a != string_1 + "_")
			{
				result = flag;
			}
			else
			{
				if (string_2 == "A")
				{
					string pattern = string_1 + "_" + string_3 + GClass0.smethod_0("1ńɳͭФԸٮܻ࡭स੓୪౶ഽวཷၖᅉቛ።ᑾᔵᘯ᝿ᠨ");
					Regex regex = new Regex(pattern);
					flag = regex.IsMatch(string_0);
				}
				else if (string_2 == "B")
				{
					string pattern2 = string_1 + GClass0.smethod_0("VŚɛ͢ѾԵدݿ࡞") + string_3 + GClass0.smethod_0("1ńɳͭФԸٮܻ࡭स੓୪౶ഽวཷၖᅉቛ።ᑾᔵᘯ᝿ᠨ");
					Regex regex2 = new Regex(pattern2);
					flag = regex2.IsMatch(string_0);
				}
				else if (string_0 == string_1)
				{
					flag = true;
				}
				else
				{
					string pattern3 = string_1 + GClass0.smethod_0("Wņɚ͡ѿԲخݼ");
					Regex regex3 = new Regex(pattern3);
					flag = regex3.IsMatch(string_0);
				}
				result = flag;
			}
			return result;
		}

		private string MulitpleSeparator = ",";

		private string SeparatorA;

		private string SeparatorB;

		public List<VEAnswer> PageAnswer = new List<VEAnswer>();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();
	}
}
