using System;
using Gssy.Capi.DAL;

namespace Gssy.Capi.BIZ
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

		public void Save(string string_0, string string_1)
		{
			this.oSurveyConfigDal.SaveByKey(string_0, string_1);
		}

		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();
	}
}
