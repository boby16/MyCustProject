using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class SyncTable
	{
		public bool SyncReadToWrite()
		{
			bool flag = true;
			this.oSyncList = this.oSurveyConfigDal.GetSyncList();
			foreach (SurveyConfig surveyConfig in this.oSyncList)
			{
				string code = surveyConfig.CODE;
				if (!(code == "S_Define"))
				{
					if (code == "SurveyDefine")
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
					surveyConfig.CODE_TEXT = "2";
					surveyConfig.CODE_NOTE = GClass0.smethod_0("吉橡怓冝С") + DateTime.Now.ToString();
					this.oSurveyConfigDal.UpdateToRead(surveyConfig);
				}
			}
			return flag;
		}

		private List<SurveyConfig> oSyncList;

		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private S_DefineDal oS_DefineDal = new S_DefineDal();
	}
}
