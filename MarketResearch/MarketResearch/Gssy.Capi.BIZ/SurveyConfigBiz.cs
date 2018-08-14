using System;
using Gssy.Capi.DAL;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000025 RID: 37
	public class SurveyConfigBiz
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x000261E4 File Offset: 0x000243E4
		public string GetByCodeText(string string_0)
		{
			return this.oSurveyConfigDal.GetByCodeText(string_0);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00026200 File Offset: 0x00024400
		public string GetByCodeTextRead(string string_0)
		{
			return this.oSurveyConfigDal.GetByCodeTextRead(string_0);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00003715 File Offset: 0x00001915
		public void Save(string string_0, string string_1)
		{
			this.oSurveyConfigDal.SaveByKey(string_0, string_1);
		}

		// Token: 0x040001C6 RID: 454
		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();
	}
}
