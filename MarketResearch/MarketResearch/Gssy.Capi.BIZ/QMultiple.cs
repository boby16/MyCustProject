using System;
using System.Collections.Generic;
using System.Linq;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x0200001E RID: 30
	public class QMultiple
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000030D3 File Offset: 0x000012D3
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000030DB File Offset: 0x000012DB
		public string ParentCode { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000030E4 File Offset: 0x000012E4
		// (set) Token: 0x06000328 RID: 808 RVA: 0x000030EC File Offset: 0x000012EC
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000030F5 File Offset: 0x000012F5
		// (set) Token: 0x0600032A RID: 810 RVA: 0x000030FD File Offset: 0x000012FD
		public SurveyAnswer OneAnswer { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00003106 File Offset: 0x00001306
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000310E File Offset: 0x0000130E
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00003117 File Offset: 0x00001317
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000311F File Offset: 0x0000131F
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00003128 File Offset: 0x00001328
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00003130 File Offset: 0x00001330
		public SurveyDefine QDefine { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00003139 File Offset: 0x00001339
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00003141 File Offset: 0x00001341
		public SurveyDefine QCircleDefine { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000314A File Offset: 0x0000134A
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00003152 File Offset: 0x00001352
		public List<SurveyDetail> QCircleDetails { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000315B File Offset: 0x0000135B
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00003163 File Offset: 0x00001363
		public Dictionary<string, string> FillTexts { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000316C File Offset: 0x0000136C
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00003174 File Offset: 0x00001374
		public string ExclusiveCode { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000317D File Offset: 0x0000137D
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00003185 File Offset: 0x00001385
		public string OtherCode { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000318E File Offset: 0x0000138E
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00003196 File Offset: 0x00001396
		public string OtherCodeTitle { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000319F File Offset: 0x0000139F
		// (set) Token: 0x0600033E RID: 830 RVA: 0x000031A7 File Offset: 0x000013A7
		public string AddFillCode { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600033F RID: 831 RVA: 0x000031B0 File Offset: 0x000013B0
		// (set) Token: 0x06000340 RID: 832 RVA: 0x000031B8 File Offset: 0x000013B8
		public string AddFillCodeTitle { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000031C1 File Offset: 0x000013C1
		// (set) Token: 0x06000342 RID: 834 RVA: 0x000031C9 File Offset: 0x000013C9
		public string AddFillText { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000031D2 File Offset: 0x000013D2
		// (set) Token: 0x06000344 RID: 836 RVA: 0x000031DA File Offset: 0x000013DA
		public DateTime QInitDateTime { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000031E3 File Offset: 0x000013E3
		// (set) Token: 0x06000346 RID: 838 RVA: 0x000031EB File Offset: 0x000013EB
		public List<string> SelectedValues { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000031F4 File Offset: 0x000013F4
		// (set) Token: 0x06000348 RID: 840 RVA: 0x000031FC File Offset: 0x000013FC
		public List<SurveyDetail> QGroupDetails { get; set; }

		// Token: 0x0600034A RID: 842 RVA: 0x00021CAC File Offset: 0x0001FEAC
		public void Init(string string_0, int int_0, bool GetDetail = true)
		{
			string otherCode = global::GClass0.smethod_0("");
			string otherCodeTitle = global::GClass0.smethod_0("");
			string exclusiveCode = global::GClass0.smethod_0("");
			string addFillCode = global::GClass0.smethod_0("");
			string addFillCodeTitle = global::GClass0.smethod_0("");
			this.SelectedValues = new List<string>();
			this.FillTexts = new Dictionary<string, string>();
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.ParentCode = this.QDefine.PARENT_CODE;
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0("") && GetDetail)
			{
				if (this.QDefine.PARENT_CODE == global::GClass0.smethod_0("") || int_0 == 0)
				{
					this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out otherCode, out otherCodeTitle, out exclusiveCode, out addFillCode, out addFillCodeTitle);
					this.OtherCode = otherCode;
					this.OtherCodeTitle = otherCodeTitle;
					this.ExclusiveCode = exclusiveCode;
					this.AddFillCode = addFillCode;
					this.AddFillCodeTitle = addFillCodeTitle;
				}
				else if (!(this.QDefine.PARENT_CODE == global::GClass0.smethod_0("CşɋͅюՋق")) && int_0 > 0)
				{
					this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.QDefine.PARENT_CODE);
				}
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00021E24 File Offset: 0x00020024
		public void InitCircle()
		{
			this.QChildQuestion = ((this.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C")) ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.QCircleDefine = this.oSurveyDefineDal.GetByName(this.QChildQuestion);
			if (this.QCircleDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00003205 File Offset: 0x00001405
		public void BeforeSave()
		{
			this.QAnswers = this.method_0();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00021EBC File Offset: 0x000200BC
		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			for (int i = 0; i < this.SelectedValues.Count; i++)
			{
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.QuestionName + global::GClass0.smethod_0("]ŀ") + (i + 1).ToString(),
					CODE = this.SelectedValues[i].ToString(),
					MULTI_ORDER = i + 1,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(DateTime.Now)
				});
			}
			if (this.FillText != global::GClass0.smethod_0(""))
			{
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"),
					CODE = this.FillText,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(DateTime.Now)
				});
			}
			if (this.FillTexts.Count<KeyValuePair<string, string>>() > 0)
			{
				foreach (string text in this.FillTexts.Keys)
				{
					if (this.FillTexts[text] != global::GClass0.smethod_0(""))
					{
						list.Add(new SurveyAnswer
						{
							QUESTION_NAME = this.QuestionName + global::GClass0.smethod_0("YŊɐ͋ѝՂ") + text,
							CODE = this.FillTexts[text],
							MULTI_ORDER = 0,
							BEGIN_DATE = new DateTime?(this.QInitDateTime),
							MODIFY_DATE = new DateTime?(DateTime.Now)
						});
					}
				}
			}
			return list;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00003213 File Offset: 0x00001413
		public void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00003235 File Offset: 0x00001435
		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000220C8 File Offset: 0x000202C8
		public string ReadAnswerByQuestionName(string string_0, string string_1)
		{
			string text = global::GClass0.smethod_0("");
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00020240 File Offset: 0x0001E440
		public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
		{
			if (list_0.Count > 1)
			{
				RandomEngine randomEngine = new RandomEngine();
				list_0 = randomEngine.RandomDetails(list_0);
			}
			return list_0;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000324A File Offset: 0x0000144A
		public void RandomDetails()
		{
			this.QDetails = this.RandomDetails(this.QDetails);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000325E File Offset: 0x0000145E
		public void GetDynamicDetails()
		{
			this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.ParentCode);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000220F8 File Offset: 0x000202F8
		public void ResetOtherCode()
		{
			this.OtherCode = global::GClass0.smethod_0("");
			foreach (SurveyDetail surveyDetail in this.QDetails)
			{
				if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3)
				{
					this.OtherCode = surveyDetail.CODE;
					break;
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0002217C File Offset: 0x0002037C
		public string GetInnerCodeText(string string_0)
		{
			string result = global::GClass0.smethod_0("");
			foreach (SurveyDetail surveyDetail in this.QDetails)
			{
				if (surveyDetail.CODE == string_0)
				{
					result = surveyDetail.CODE_TEXT;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00003282 File Offset: 0x00001482
		public void GetGroupDetails()
		{
			this.QGroupDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.PARENT_CODE);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000221F0 File Offset: 0x000203F0
		public void InitDetailID(string string_0, int int_0)
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			string text4 = global::GClass0.smethod_0("");
			string text5 = global::GClass0.smethod_0("");
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				if (this.QDefine.PARENT_CODE == global::GClass0.smethod_0("") || int_0 == 0)
				{
					this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out text, out text2, out text3, out text4, out text5);
				}
				else if (!(this.QDefine.PARENT_CODE == global::GClass0.smethod_0("CşɋͅюՋق")) && int_0 > 0)
				{
					this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.QDefine.PARENT_CODE);
				}
			}
		}

		// Token: 0x04000162 RID: 354
		public string QuestionName = global::GClass0.smethod_0("");

		// Token: 0x04000163 RID: 355
		public string QuestionTitle = global::GClass0.smethod_0("");

		// Token: 0x0400016B RID: 363
		private string QChildQuestion = global::GClass0.smethod_0("");

		// Token: 0x0400016D RID: 365
		public string FillText = global::GClass0.smethod_0("");

		// Token: 0x04000176 RID: 374
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x04000177 RID: 375
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x04000178 RID: 376
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
