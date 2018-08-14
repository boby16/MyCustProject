using System;

namespace Gssy.Capi.Update
{
	// Token: 0x02000008 RID: 8
	public class SurveyConfigBiz
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000021D6 File Offset: 0x000003D6
		public string GetByCodeText(string string_0)
		{
			return this.oSurveyConfigDal.GetByCodeText(string_0);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000021E4 File Offset: 0x000003E4
		public string GetByCodeTextRead(string string_0)
		{
			return this.oSurveyConfigDal.GetByCodeTextRead(string_0);
		}

		// Token: 0x0400003B RID: 59
		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();
	}
}
