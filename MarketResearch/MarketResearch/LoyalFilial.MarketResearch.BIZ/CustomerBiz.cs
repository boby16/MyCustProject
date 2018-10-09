using System;
using System.Collections.Generic;
using System.Linq;
using LoyalFilial.MarketResearch.DAL;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.BIZ
{
	public class CustomerBiz
	{
		public SurveyRoadMap RoadMap { get; set; }

		public string RouteResult { get; set; }

		public List<string> RouteResultValues { get; set; }

		public List<SurveyAnswer> AnswerValues { get; set; }

		public List<SurveyDetail> QuestionDetails { get; set; }

		public string SingleResult { get; set; }

		public string SurveyExtend1 { get; set; }

		public string SurveyExtend2 { get; set; }

		public string SurveyExtend3 { get; set; }

		public string SurveyExtend4 { get; set; }

		public string SurveyExtend5 { get; set; }

		public string SurveyExtend6 { get; set; }

		public string SurveyExtend7 { get; set; }

		public string SurveyExtend8 { get; set; }

		public string SurveyExtend9 { get; set; }

		public string SurveyExtend10 { get; set; }

		public string SurveyExtend11 { get; set; }

		public List<string> CircleQuestionList { get; set; }

		public CustomerBiz()
		{
			this.RouteResultValues = new List<string>();
			this.CircleQuestionList = new List<string>();
		}

		public virtual void Init(string string_0)
		{
		}

		public void GetPageRoadMap(string string_0, string string_1)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
		}

		public void PageLoad(string string_0)
		{
		}

		public void SaveToAnswer(string string_0, string string_1, string string_2, int int_0)
		{
			this.oSurveyAnswerDal.AddOne(string_0, string_1, string_2, int_0);
		}

		public string GetOneAnswer(string string_0, string string_1)
		{
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		public string GetOPageAnswer(string string_0, int int_0)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
			string text = "";
			foreach (SurveyAnswer surveyAnswer in list)
			{
				text = text + string.Format("题号,{0},答案,{1}", surveyAnswer.QUESTION_NAME, surveyAnswer.CODE) + Environment.NewLine;
			}
			return text;
		}

		public bool PageAddOnCheck(string string_0, string string_1, out string string_2)
		{
			string_2 = "";
			string text = "";
			string a = "";
			bool flag = false;
			SurveyDetail surveyDetail = new SurveyDetail();
			bool result = true;
			if (!(string_1 == "C6"))
			{
				if (!(string_1 == "B3"))
				{
					if (string_1 == "B4" && this.SurveyExtend1 == "2")
					{
						text = this.GetOneAnswer(string_0, "A4a_品牌");
						string codeText = this.oSurveyDetailDal.GetCodeText("A4_品牌", text);
						a = this.GetOneAnswer(string_0, "A4b_品牌");
						string codeText2 = this.oSurveyDetailDal.GetCodeText("A4_品牌", text);
						for (int i = 0; i < this.RouteResultValues.Count; i++)
						{
							surveyDetail = this.oSurveyDetailDal.GetOne("B3", this.RouteResultValues[i].ToString());
							string extend_ = surveyDetail.EXTEND_1;
							if (text == extend_)
							{
								string_2 = string.Format("你主要考虑的品牌 ({0}) 不应该被选择。", codeText);
								return false;
							}
							if (a == extend_)
							{
								string_2 = string.Format("你主要考虑的品牌 ({0}) 不应该被选择。", codeText2);
								return false;
							}
						}
					}
				}
				else if (this.SurveyExtend1 == "2")
				{
					text = this.GetOneAnswer(string_0, "S5_品牌");
					string codeText = this.oSurveyDetailDal.GetCodeText("S5_品牌", text);
					int extendCount = this.oSurveyDetailDal.GetExtendCount("B3", text, 1);
					if (extendCount > 0)
					{
						for (int j = 0; j < this.RouteResultValues.Count; j++)
						{
							surveyDetail = this.oSurveyDetailDal.GetOne("B3", this.RouteResultValues[j].ToString());
							string extend_ = surveyDetail.EXTEND_1;
							if (text == extend_)
							{
								flag = true;
							}
						}
						if (!flag)
						{
							string_2 = string.Format("你考虑的品牌 {0} 应该会被选择到。", codeText);
							return false;
						}
					}
				}
			}
			else
			{
				text = this.GetOneAnswer(string_0, "C1");
				string codeText = this.oSurveyDetailDal.GetCodeText("C1", text);
				if (Convert.ToInt32(this.SingleResult) < Convert.ToInt32(text))
				{
					string_2 = string.Format("C6价格应该大于等于C1({0})", codeText);
					return false;
				}
			}
			return result;
		}

		public bool PageAddOnCheckCircle(string string_0, string string_1, string string_2, out string string_3)
		{
			string_3 = "";
			bool result = true;
			SurveyDetail surveyDetail = new SurveyDetail();
			if (!(string_1 == "B1"))
			{
				if (string_1 == "B2" && this.SurveyExtend1 == "2")
				{
					string oneAnswer = this.GetOneAnswer(string_0, "S5_品牌");
					string codeText = this.oSurveyDetailDal.GetCodeText("S5_品牌", oneAnswer);
					string oneAnswer2 = this.GetOneAnswer(string_0, "A4a_品牌");
					string codeText2 = this.oSurveyDetailDal.GetCodeText("A4_品牌", oneAnswer2);
					string oneAnswer3 = this.GetOneAnswer(string_0, "A4b_品牌");
					string codeText3 = this.oSurveyDetailDal.GetCodeText("A4_品牌", oneAnswer3);
					for (int i = 0; i < this.RouteResultValues.Count<string>(); i++)
					{
						if (this.RouteResultValues[i].ToString() == "1" || this.RouteResultValues[i].ToString() == "2")
						{
							string_2 = this.CircleQuestionList[i].ToString().Replace("B2_R", "");
							surveyDetail = this.oSurveyDetailDal.GetOne("G_B2", string_2);
							string extend_ = surveyDetail.EXTEND_1;
							if (oneAnswer == extend_)
							{
								string_3 = string.Format("你主要考虑的品牌 {0}，不应该不喜欢。", codeText);
								return false;
							}
							if (oneAnswer2 == extend_)
							{
								string_3 = string.Format("你主要考虑的品牌 {0}，不应该不喜欢。", codeText2);
								return false;
							}
							if (oneAnswer3 == extend_)
							{
								string_3 = string.Format("你主要考虑的品牌 {0}，不应该不喜欢。", codeText3);
								return false;
							}
						}
					}
				}
			}
			else
			{
				string oneAnswer = this.GetOneAnswer(string_0, "S5_品牌");
				string codeText = this.oSurveyDetailDal.GetCodeText("S5_品牌", oneAnswer);
				string oneAnswer2 = this.GetOneAnswer(string_0, "A2_品牌");
				string codeText2 = this.oSurveyDetailDal.GetCodeText("A4_品牌", oneAnswer2);
				string oneAnswer3 = this.GetOneAnswer(string_0, "A4a_品牌");
				string codeText3 = this.oSurveyDetailDal.GetCodeText("A4_品牌", oneAnswer3);
				string oneAnswer4 = this.GetOneAnswer(string_0, "A4b_品牌");
				string codeText4 = this.oSurveyDetailDal.GetCodeText("A4_品牌", oneAnswer4);
				surveyDetail = this.oSurveyDetailDal.GetOne("G_B1", string_2);
				string extend_ = surveyDetail.EXTEND_1;
				if (oneAnswer != "024" && (oneAnswer == extend_ && (this.SingleResult == "1" || this.SingleResult == "2")))
				{
					string_3 = string.Format("这是你在S5中选择的品牌 {0}，不应该对它不了解。", codeText);
					return false;
				}
				if (oneAnswer2 != "024" && (oneAnswer2 == extend_ && (this.SingleResult == "1" || this.SingleResult == "2")))
				{
					string_3 = string.Format("这是你在A2中选择的品牌 {0}，不应该对它不了解。", codeText2);
					return false;
				}
				if (oneAnswer3 != "024" && (oneAnswer3 == extend_ && this.SingleResult == "1"))
				{
					string_3 = string.Format("这是你在A4中选择的对比品牌{0}，不应该未听说过它。", codeText3);
					return false;
				}
				if (oneAnswer4 != "024" && (oneAnswer4 == extend_ && this.SingleResult == "1"))
				{
					string_3 = string.Format("这是你在A4中选择的对比品牌{0}，不应该未听说过它。", codeText4);
					return false;
				}
			}
			return result;
		}

		public bool PageAddOnDeal(string string_0, string string_1, out string string_2)
		{
			string_2 = "";
			bool result = true;
			if (!(string_1 == "S5"))
			{
				if (string_1 == "S6")
				{
					DateTime t = Convert.ToDateTime(this.RouteResult + "-01");
					DateTime t2 = Convert.ToDateTime("2013-02-28");
					if (t > t2)
					{
						this.oSurveyAnswerDal.AddOne(string_0, "S6_时间段", "2", 0);
					}
					else
					{
						this.oSurveyAnswerDal.AddOne(string_0, "S6_时间段", "1", 0);
					}
				}
			}
			else
			{
				foreach (SurveyDetail surveyDetail in this.QuestionDetails)
				{
					if (surveyDetail.CODE == this.SingleResult)
					{
						this.oSurveyAnswerDal.AddOne(string_0, "S5_血统", surveyDetail.EXTEND_1, 0);
						this.oSurveyAnswerDal.AddOne(string_0, "S5_级别", surveyDetail.EXTEND_2, 0);
						string_2 = this.oSurveyDetailDal.GetCodeText("A4_级别", surveyDetail.EXTEND_2);
					}
				}
			}
			return result;
		}

		public string GetSurveyExtend(string string_0, int int_0)
		{
			string result = "";
			string text = "SURVEY_EXTEND" + int_0.ToString();
			SurveyDefine byPageId = this.oSurveyDefineDal.GetByPageId(text);
			if (byPageId.PAGE_ID == text && byPageId.QUESTION_TYPE == 88)
			{
				if (byPageId.COMBINE_INDEX == 0)
				{
					string oneCode = this.oSurveyAnswerDal.GetOneCode(string_0, byPageId.QUESTION_NAME);
					result = this.oSurveyDetailDal.GetCodeText(byPageId.DETAIL_ID, oneCode);
				}
				if (byPageId.COMBINE_INDEX == 1)
				{
					result = this.oSurveyAnswerDal.GetOneCode(string_0, byPageId.QUESTION_NAME);
				}
			}
			return result;
		}

		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();
	}
}
