using System;
using Gssy.Capi.DAL;

namespace Gssy.Capi.BIZ
{
	public class AttachBiz
	{
		public bool ExistsByQName(string string_0, string string_1)
		{
			return this.oSurveyAttachDal.ExistsByQName(string_0, string_1);
		}

		public bool ExistsByPageId(string string_0, string string_1)
		{
			return this.oSurveyAttachDal.ExistsByPageId(string_0, string_1);
		}

		public void DeleteByPageId(string string_0, string string_1)
		{
			this.oSurveyAttachDal.DeleteByPageId(string_0, string_1);
		}

		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();
	}
}
