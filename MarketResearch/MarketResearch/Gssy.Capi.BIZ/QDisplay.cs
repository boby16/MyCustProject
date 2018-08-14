using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000019 RID: 25
	public class QDisplay
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00002B91 File Offset: 0x00000D91
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00002B99 File Offset: 0x00000D99
		public string QuestionName { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00002BA2 File Offset: 0x00000DA2
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00002BAA File Offset: 0x00000DAA
		public string QuestionTitle { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00002BB3 File Offset: 0x00000DB3
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00002BBB File Offset: 0x00000DBB
		public SurveyDefine QDefine { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00002BC4 File Offset: 0x00000DC4
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00002BCC File Offset: 0x00000DCC
		public SurveyDefine QCircleDefine { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00002BD5 File Offset: 0x00000DD5
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00002BDD File Offset: 0x00000DDD
		public List<SurveyDetail> QCircleDetails { get; set; }

		// Token: 0x06000295 RID: 661 RVA: 0x00002C14 File Offset: 0x00000E14
		public void Init(string string_0, int int_0)
		{
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.QuestionTitle = this.QDefine.QUESTION_TITLE;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00020514 File Offset: 0x0001E714
		public void InitCircle()
		{
			this.QChildQuestion = ((this.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C")) ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.QCircleDefine = this.oSurveyDefineDal.GetByName(this.QChildQuestion);
			if (this.QCircleDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
			}
		}

		// Token: 0x0400011D RID: 285
		private string QChildQuestion = global::GClass0.smethod_0("");

		// Token: 0x0400011E RID: 286
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x0400011F RID: 287
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
