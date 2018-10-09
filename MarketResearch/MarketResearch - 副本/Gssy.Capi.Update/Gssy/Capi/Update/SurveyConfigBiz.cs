using System;

namespace Gssy.Capi.Update
{
	public class SurveyConfigBiz
	{
		public string GetByCodeText(string string_0)
		{
			return this.oSurveyConfigDal.GetByCodeText(string_0);
		}

		public string GetByCodeTextRead(string string_0)
		{
			return this.oSurveyConfigDal.GetByCodeTextRead(string_0);
		}

		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();
	}
}
