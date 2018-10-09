using System;
using System.Collections.Generic;
using LoyalFilial.MarketResearch.DAL;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.BIZ
{
	public class LoadReadTable
	{
		public List<SurveyDefine> GetSurveyDefine()
		{
			SurveyDefineDal surveyDefineDal = new SurveyDefineDal();
			return surveyDefineDal.GetList();
		}

		public List<SurveyDetail> GetSurveyDetail()
		{
			SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
			return surveyDetailDal.GetList();
		}

		public List<SurveyLogic> GetSurveyLogic()
		{
			SurveyLogicDal surveyLogicDal = new SurveyLogicDal();
			return surveyLogicDal.GetList();
		}

		public List<SurveyRoadMap> GetSurveyRoadMap()
		{
			SurveyRoadMapDal surveyRoadMapDal = new SurveyRoadMapDal();
			return surveyRoadMapDal.GetList();
		}
	}
}
