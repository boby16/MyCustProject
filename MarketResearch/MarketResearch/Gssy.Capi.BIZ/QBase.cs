using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000016 RID: 22
	public class QBase
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000027EB File Offset: 0x000009EB
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000027F3 File Offset: 0x000009F3
		public string QuestionName { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000027FC File Offset: 0x000009FC
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00002804 File Offset: 0x00000A04
		public string QuestionTitle { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000280D File Offset: 0x00000A0D
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00002815 File Offset: 0x00000A15
		public string ParentCode { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000281E File Offset: 0x00000A1E
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00002826 File Offset: 0x00000A26
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000282F File Offset: 0x00000A2F
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00002837 File Offset: 0x00000A37
		public SurveyAnswer OneAnswer { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00002840 File Offset: 0x00000A40
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00002848 File Offset: 0x00000A48
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00002851 File Offset: 0x00000A51
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00002859 File Offset: 0x00000A59
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00002862 File Offset: 0x00000A62
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000286A File Offset: 0x00000A6A
		public SurveyDefine QDefine { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00002873 File Offset: 0x00000A73
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000287B File Offset: 0x00000A7B
		public string OtherCode { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00002884 File Offset: 0x00000A84
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000288C File Offset: 0x00000A8C
		public string FillText { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00002895 File Offset: 0x00000A95
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000289D File Offset: 0x00000A9D
		public DateTime QInitDateTime { get; set; }

		// Token: 0x06000242 RID: 578 RVA: 0x0001FF30 File Offset: 0x0001E130
		public virtual void Init(string string_0, int int_0)
		{
			this.QInitDateTime = DateTime.Now;
			string otherCode = global::GClass0.smethod_0("");
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.ParentCode = this.QDefine.PARENT_CODE;
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				if (this.QDefine.PARENT_CODE == global::GClass0.smethod_0(""))
				{
					this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out otherCode);
					this.OtherCode = otherCode;
				}
				else if (!(this.QDefine.PARENT_CODE == global::GClass0.smethod_0("CşɋͅюՋق")))
				{
					this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.QDefine.PARENT_CODE);
				}
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000024F1 File Offset: 0x000006F1
		public virtual void BeforeSave()
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000028CF File Offset: 0x00000ACF
		public virtual void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000028E4 File Offset: 0x00000AE4
		public virtual void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00020030 File Offset: 0x0001E230
		public virtual string ReadAnswerByQuestionName(string string_0, string string_1)
		{
			string text = global::GClass0.smethod_0("");
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000028F9 File Offset: 0x00000AF9
		public virtual void GetDynamicDetails()
		{
			this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.ParentCode);
		}

		// Token: 0x040000F3 RID: 243
		public SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x040000F4 RID: 244
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x040000F5 RID: 245
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
