using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000017 RID: 23
	public class QBPTO
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000291D File Offset: 0x00000B1D
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00002925 File Offset: 0x00000B25
		public string QuestionName { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000292E File Offset: 0x00000B2E
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00002936 File Offset: 0x00000B36
		public string CircleAQuestionName { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000293F File Offset: 0x00000B3F
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00002947 File Offset: 0x00000B47
		public string CircleBQuestionName { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00002950 File Offset: 0x00000B50
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00002958 File Offset: 0x00000B58
		public SurveyDefine QDefine { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00002961 File Offset: 0x00000B61
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00002969 File Offset: 0x00000B69
		public SurveyDefine QCircleADefine { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00002972 File Offset: 0x00000B72
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000297A File Offset: 0x00000B7A
		public SurveyDefine QCircleBDefine { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00002983 File Offset: 0x00000B83
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000298B File Offset: 0x00000B8B
		public List<SurveyDetail> QCircleADetails { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00002994 File Offset: 0x00000B94
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000299C File Offset: 0x00000B9C
		public List<SurveyDetail> QCircleBDetails { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000029A5 File Offset: 0x00000BA5
		// (set) Token: 0x06000259 RID: 601 RVA: 0x000029AD File Offset: 0x00000BAD
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000029B6 File Offset: 0x00000BB6
		// (set) Token: 0x0600025B RID: 603 RVA: 0x000029BE File Offset: 0x00000BBE
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000029C7 File Offset: 0x00000BC7
		// (set) Token: 0x0600025D RID: 605 RVA: 0x000029CF File Offset: 0x00000BCF
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000029D8 File Offset: 0x00000BD8
		// (set) Token: 0x0600025F RID: 607 RVA: 0x000029E0 File Offset: 0x00000BE0
		public DateTime QInitDateTime { get; set; }

		// Token: 0x06000261 RID: 609 RVA: 0x00020060 File Offset: 0x0001E260
		public void Init(string string_0, int int_0, bool GetDetail = true)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.CircleAQuestionName = this.QDefine.GROUP_CODEA;
			this.CircleBQuestionName = this.QDefine.GROUP_CODEB;
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0("") && GetDetail)
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
			}
			this.QCircleADefine = this.oSurveyDefineDal.GetByName(this.CircleAQuestionName);
			this.QCircleBDefine = this.oSurveyDefineDal.GetByName(this.CircleBQuestionName);
			if (this.QCircleADefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QCircleADetails = this.oSurveyDetailDal.GetDetails(this.QCircleADefine.DETAIL_ID);
			}
			if (this.QCircleBDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QCircleBDetails = this.oSurveyDetailDal.GetDetails(this.QCircleBDefine.DETAIL_ID);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00020198 File Offset: 0x0001E398
		public void InitDetailID(string string_0, int int_0)
		{
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00002A24 File Offset: 0x00000C24
		public void BeforeSave(List<SurveyAnswer> list_0)
		{
			this.QAnswers = list_0;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000201E0 File Offset: 0x0001E3E0
		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> result = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			return result;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00002A2D File Offset: 0x00000C2D
		public void BeforeSavebyCode()
		{
			this.QAnswers = this.method_1();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000201E0 File Offset: 0x0001E3E0
		private List<SurveyAnswer> method_1()
		{
			List<SurveyAnswer> result = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00002A3B File Offset: 0x00000C3B
		public void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00002A5D File Offset: 0x00000C5D
		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000201FC File Offset: 0x0001E3FC
		public string ReadAnswerByQuestionName(string string_0, string string_1)
		{
			string text = global::GClass0.smethod_0("");
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			text = one.CODE;
			if (text == null)
			{
				text = global::GClass0.smethod_0("");
			}
			return text;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00020240 File Offset: 0x0001E440
		public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
		{
			if (list_0.Count > 1)
			{
				RandomEngine randomEngine = new RandomEngine();
				list_0 = randomEngine.RandomDetails(list_0);
			}
			return list_0;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00002A72 File Offset: 0x00000C72
		public void RandomDetails()
		{
			this.QDetails = this.RandomDetails(this.QDetails);
		}

		// Token: 0x04000102 RID: 258
		public List<string> SelectedCode = new List<string>();

		// Token: 0x04000103 RID: 259
		public int SelectedCodeCount = -1;

		// Token: 0x04000104 RID: 260
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x04000105 RID: 261
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x04000106 RID: 262
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
