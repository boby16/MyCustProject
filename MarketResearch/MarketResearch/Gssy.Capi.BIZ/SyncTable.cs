using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000029 RID: 41
	public class SyncTable
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x00027648 File Offset: 0x00025848
		public bool SyncReadToWrite()
		{
			bool flag = true;
			this.oSyncList = this.oSurveyConfigDal.GetSyncList();
			foreach (SurveyConfig surveyConfig in this.oSyncList)
			{
				string code = surveyConfig.CODE;
				if (!(code == global::GClass0.smethod_0("[Řɂ͠Ѣժ٬ݤ")))
				{
					if (code == global::GClass0.smethod_0("_žɸͿѭվقݠࡢ४੬୤"))
					{
						flag = this.oSurveyDefineDal.SyncReadToWrite();
					}
				}
				else
				{
					flag = this.oS_DefineDal.SyncReadToWrite();
				}
				if (flag)
				{
					surveyConfig.CODE_TEXT = global::GClass0.smethod_0("3");
					surveyConfig.CODE_NOTE = global::GClass0.smethod_0("吉橡怓冝С") + DateTime.Now.ToString();
					this.oSurveyConfigDal.UpdateToRead(surveyConfig);
				}
			}
			return flag;
		}

		// Token: 0x040001DD RID: 477
		private List<SurveyConfig> oSyncList;

		// Token: 0x040001DE RID: 478
		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();

		// Token: 0x040001DF RID: 479
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x040001E0 RID: 480
		private S_DefineDal oS_DefineDal = new S_DefineDal();
	}
}
