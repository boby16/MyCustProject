using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x0200001C RID: 28
	public class QMatrixMultiple
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00002E2A File Offset: 0x0000102A
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00002E32 File Offset: 0x00001032
		public string QuestionName { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00002E3B File Offset: 0x0000103B
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00002E43 File Offset: 0x00001043
		public string CircleQuestionName { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00002E4C File Offset: 0x0000104C
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00002E54 File Offset: 0x00001054
		public string ParentCode { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00002E5D File Offset: 0x0000105D
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00002E65 File Offset: 0x00001065
		public SurveyDefine QDefine { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00002E6E File Offset: 0x0000106E
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00002E76 File Offset: 0x00001076
		public SurveyDefine QCircleDefine { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00002E7F File Offset: 0x0000107F
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00002E87 File Offset: 0x00001087
		public List<SurveyDetail> QCircleDetails { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00002E90 File Offset: 0x00001090
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00002E98 File Offset: 0x00001098
		public List<SurveyDetail> QCircleGroupDetails { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00002EA1 File Offset: 0x000010A1
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00002EA9 File Offset: 0x000010A9
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00002EB2 File Offset: 0x000010B2
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00002EBA File Offset: 0x000010BA
		public List<SurveyDetail> QGroupDetails { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00002EC3 File Offset: 0x000010C3
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00002ECB File Offset: 0x000010CB
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00002ED4 File Offset: 0x000010D4
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00002EDC File Offset: 0x000010DC
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00002EE5 File Offset: 0x000010E5
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00002EED File Offset: 0x000010ED
		public DateTime QInitDateTime { get; set; }

		// Token: 0x060002F4 RID: 756 RVA: 0x00020F94 File Offset: 0x0001F194
		public void Init(string string_0, int int_0, bool GetDetail = true)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.CircleQuestionName = ((this.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C")) ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.ParentCode = this.QDefine.PARENT_CODE;
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0("") && GetDetail)
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
				if (this.QDefine.PARENT_CODE != global::GClass0.smethod_0(""))
				{
					this.GetGroupDetails();
				}
			}
			this.QChildQuestion = ((this.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C")) ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.QCircleDefine = this.oSurveyDefineDal.GetByName(this.QChildQuestion);
			if (this.QCircleDefine.DETAIL_ID != global::GClass0.smethod_0("") && GetDetail)
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
				if (this.QCircleDefine.PARENT_CODE != global::GClass0.smethod_0(""))
				{
					this.GetCircleGroupDetails();
				}
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00002EF6 File Offset: 0x000010F6
		public void BeforeSave()
		{
			this.QAnswers = this.method_0();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00021124 File Offset: 0x0001F324
		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			int num = 0;
			foreach (classMultipleAnswers classMultipleAnswers in this.SelectedCodes)
			{
				string text = this.QuestionName + global::GClass0.smethod_0("]œ") + this.QCircleDetails[num].CODE;
				SurveyAnswer surveyAnswer = new SurveyAnswer();
				for (int i = 0; i < classMultipleAnswers.Answers.Count; i++)
				{
					list.Add(new SurveyAnswer
					{
						QUESTION_NAME = text + global::GClass0.smethod_0("]ŀ") + (i + 1).ToString(),
						CODE = classMultipleAnswers.Answers[i].ToString(),
						MULTI_ORDER = i + 1,
						BEGIN_DATE = new DateTime?(this.QInitDateTime),
						MODIFY_DATE = new DateTime?(now)
					});
				}
				text = this.CircleQuestionName + global::GClass0.smethod_0("]œ") + this.QCircleDetails[num].CODE;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = text,
					CODE = this.QCircleDetails[num].CODE,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				num++;
			}
			return list;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00002F04 File Offset: 0x00001104
		public void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00002F26 File Offset: 0x00001126
		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000212E4 File Offset: 0x0001F4E4
		public List<SurveyAnswer> ReadAnswerByQuestionName(string string_0, string string_1, int int_0)
		{
			return this.oSurveyAnswerDal.GetListByMultipleBySequenceId(string_0, string_1, int_0);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00020240 File Offset: 0x0001E440
		public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
		{
			if (list_0.Count > 1)
			{
				RandomEngine randomEngine = new RandomEngine();
				list_0 = randomEngine.RandomDetails(list_0);
			}
			return list_0;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00021304 File Offset: 0x0001F504
		public void RandomDetails(int int_0 = 1)
		{
			if (int_0 == 1)
			{
				this.QDetails = this.RandomDetails(this.QDetails);
			}
			else if (int_0 == 2)
			{
				this.QCircleDetails = this.RandomDetails(this.QCircleDetails);
			}
			else if (int_0 == 3)
			{
				this.QGroupDetails = this.RandomDetails(this.QGroupDetails);
			}
			else if (int_0 == 4)
			{
				this.QCircleGroupDetails = this.RandomDetails(this.QCircleGroupDetails);
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00021378 File Offset: 0x0001F578
		public void InitDetailID(string string_0, int int_0)
		{
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
				if (this.QDefine.PARENT_CODE != global::GClass0.smethod_0(""))
				{
					this.GetGroupDetails();
				}
			}
			if (this.QCircleDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
				if (this.QCircleDefine.PARENT_CODE != global::GClass0.smethod_0(""))
				{
					this.GetCircleGroupDetails();
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00002F3B File Offset: 0x0000113B
		public void GetGroupDetails()
		{
			this.QGroupDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.PARENT_CODE);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00002F59 File Offset: 0x00001159
		public void GetCircleGroupDetails()
		{
			this.QCircleGroupDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.PARENT_CODE);
		}

		// Token: 0x04000144 RID: 324
		private string QChildQuestion = global::GClass0.smethod_0("");

		// Token: 0x0400014C RID: 332
		public List<classMultipleAnswers> SelectedCodes = new List<classMultipleAnswers>();

		// Token: 0x0400014D RID: 333
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x0400014E RID: 334
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x0400014F RID: 335
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
