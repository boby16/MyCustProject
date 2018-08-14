using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000011 RID: 17
	public class LoadReadTable
	{
		// Token: 0x060000DF RID: 223 RVA: 0x000097C4 File Offset: 0x000079C4
		public List<SurveyDefine> GetSurveyDefine()
		{
			SurveyDefineDal surveyDefineDal = new SurveyDefineDal();
			return surveyDefineDal.GetList();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000097E0 File Offset: 0x000079E0
		public List<SurveyDetail> GetSurveyDetail()
		{
			SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
			return surveyDetailDal.GetList();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000097FC File Offset: 0x000079FC
		public List<SurveyLogic> GetSurveyLogic()
		{
			SurveyLogicDal surveyLogicDal = new SurveyLogicDal();
			return surveyLogicDal.GetList();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00009818 File Offset: 0x00007A18
		public List<SurveyRoadMap> GetSurveyRoadMap()
		{
			SurveyRoadMapDal surveyRoadMapDal = new SurveyRoadMapDal();
			return surveyRoadMapDal.GetList();
		}
	}
}
