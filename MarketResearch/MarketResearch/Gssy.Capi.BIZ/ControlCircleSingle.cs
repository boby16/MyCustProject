using System;
using System.Collections.Generic;
using LoyalFilial.MarketResearch.DAL;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.BIZ
{
	public class ControlCircleSingle
	{
		public string QuestionName { get; set; }

		public SurveyDetail InfoDetail { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public SurveyDefine QDefine { get; set; }

		public void Init(string string_0)
		{
			string text = "";
			this.QDefine = this.oSurveyDefineDal.GetByName(string_0);
			this.QuestionName = string_0;
			if (this.QDefine.DETAIL_ID != "")
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out text);
			}
		}

		public int GetCircleCount(string string_0)
		{
			return this.oSurveyDetailDal.GetDetailCount(string_0);
		}

		public void GetCurrentInfo(string string_0, int int_0, string string_1, int int_1)
		{
			if (int_1 == 1)
			{
				string oneCode = this.oSurveyRandomDal.GetOneCode(string_0, string_1, 1, int_0);
				this.InfoDetail = this.oSurveyDetailDal.GetOne(string_1, oneCode);
			}
			else
			{
				this.InfoDetail = this.oSurveyDetailDal.GetOneByOrder(string_1, int_0);
			}
		}

		public int LimitDetailsCount(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format("SELECT * FROM SurveyAnswer WHERE SURVEY_ID ='{0}' AND (QUESTION_NAME='{1}' OR QUESTION_NAME LIKE '{1}_%' OR QUESTION_NAME LIKE '{1}_A%' )  AND QUESTION_NAME<>'{1}_OTH'", string_0, string_1);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySql(string_3);
			return list.Count;
		}

		public void GetCurrentLimitInfo(string string_0, string string_1, string string_2, string string_3, int int_0)
		{
			string string_4 = string.Format("SELECT * FROM SurveyAnswer WHERE SURVEY_ID ='{0}' AND (QUESTION_NAME='{1}' OR QUESTION_NAME LIKE '{1}_%' OR QUESTION_NAME LIKE '{1}_A%' )  AND QUESTION_NAME<>'{1}_OTH' ORDER BY ID", string_0, string_1);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySql(string_4);
			for (int i = 0; i < list.Count; i++)
			{
				if (i == int_0 - 1)
				{
					this.InfoDetail = this.oSurveyDetailDal.GetOne(string_3, list[i].CODE.ToString());
				}
			}
		}

		public SurveyDetail GetOneInfo(string string_0, string string_1, string string_2)
		{
			return this.oSurveyDetailDal.GetOne(string_1, string_2);
		}

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();
	}
}
