using System;
using Gssy.Capi.DAL;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000004 RID: 4
	public class AttachBiz
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00004164 File Offset: 0x00002364
		public bool ExistsByQName(string string_0, string string_1)
		{
			return this.oSurveyAttachDal.ExistsByQName(string_0, string_1);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00004180 File Offset: 0x00002380
		public bool ExistsByPageId(string string_0, string string_1)
		{
			return this.oSurveyAttachDal.ExistsByPageId(string_0, string_1);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000217E File Offset: 0x0000037E
		public void DeleteByPageId(string string_0, string string_1)
		{
			this.oSurveyAttachDal.DeleteByPageId(string_0, string_1);
		}

		// Token: 0x04000011 RID: 17
		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();
	}
}
