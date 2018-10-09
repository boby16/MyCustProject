using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
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
