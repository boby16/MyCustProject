using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x0200001D RID: 29
	public class QMatrixSingle
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00002F77 File Offset: 0x00001177
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00002F7F File Offset: 0x0000117F
		public string QuestionName { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00002F88 File Offset: 0x00001188
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00002F90 File Offset: 0x00001190
		public string CircleQuestionName { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00002F99 File Offset: 0x00001199
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00002FA1 File Offset: 0x000011A1
		public string ParentCode { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00002FAA File Offset: 0x000011AA
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00002FB2 File Offset: 0x000011B2
		public SurveyDefine QDefine { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00002FBB File Offset: 0x000011BB
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00002FC3 File Offset: 0x000011C3
		public SurveyDefine QCircleDefine { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00002FCC File Offset: 0x000011CC
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00002FD4 File Offset: 0x000011D4
		public List<SurveyDetail> QCircleDetails { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00002FDD File Offset: 0x000011DD
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00002FE5 File Offset: 0x000011E5
		public List<SurveyDetail> QCircleGroupDetails { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00002FEE File Offset: 0x000011EE
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00002FF6 File Offset: 0x000011F6
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00002FFF File Offset: 0x000011FF
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00003007 File Offset: 0x00001207
		public List<SurveyDetail> QGroupDetails { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00003010 File Offset: 0x00001210
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00003018 File Offset: 0x00001218
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00003021 File Offset: 0x00001221
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00003029 File Offset: 0x00001229
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00003032 File Offset: 0x00001232
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000303A File Offset: 0x0000123A
		public DateTime QInitDateTime { get; set; }

		// Token: 0x06000318 RID: 792 RVA: 0x00021494 File Offset: 0x0001F694
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

		// Token: 0x06000319 RID: 793 RVA: 0x00003043 File Offset: 0x00001243
		public void BeforeSave()
		{
			this.QAnswers = this.method_0();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00021624 File Offset: 0x0001F824
		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			int num = 0;
			foreach (string code in this.SelectedCode)
			{
				string question_NAME = this.QuestionName + global::GClass0.smethod_0("]œ") + this.QCircleDetails[num].CODE;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = code,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				question_NAME = this.CircleQuestionName + global::GClass0.smethod_0("]œ") + this.QCircleDetails[num].CODE;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = this.QCircleDetails[num].CODE,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				num++;
			}
			return list;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00003051 File Offset: 0x00001251
		public void BeforeSavebyCode(string string_0 = "99")
		{
			this.QAnswers = this.method_1(string_0);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0002178C File Offset: 0x0001F98C
		private List<SurveyAnswer> method_1(string string_0)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			if (this.SelectedCodeCount == 0)
			{
				string question_NAME = this.QuestionName + global::GClass0.smethod_0("\\ŐȰ");
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = string_0,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				question_NAME = this.QuestionName + global::GClass0.smethod_0("]ł") + string_0;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = global::GClass0.smethod_0("0"),
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				question_NAME = this.CircleQuestionName + global::GClass0.smethod_0("\\ŐȰ");
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = this.QCircleDetails[0].CODE,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
			}
			else
			{
				int num = 0;
				foreach (string text in this.SelectedCode)
				{
					if (text != global::GClass0.smethod_0(""))
					{
						string question_NAME2 = this.QuestionName + global::GClass0.smethod_0("]œ") + this.QCircleDetails[num].CODE;
						list.Add(new SurveyAnswer
						{
							QUESTION_NAME = question_NAME2,
							CODE = text,
							MULTI_ORDER = 0,
							BEGIN_DATE = new DateTime?(this.QInitDateTime),
							MODIFY_DATE = new DateTime?(now)
						});
						question_NAME2 = this.QuestionName + global::GClass0.smethod_0("]ł") + text;
						list.Add(new SurveyAnswer
						{
							QUESTION_NAME = question_NAME2,
							CODE = this.QCircleDetails[num].CODE,
							MULTI_ORDER = 0,
							BEGIN_DATE = new DateTime?(this.QInitDateTime),
							MODIFY_DATE = new DateTime?(now)
						});
						question_NAME2 = this.CircleQuestionName + global::GClass0.smethod_0("]œ") + (num + 1).ToString();
						list.Add(new SurveyAnswer
						{
							QUESTION_NAME = question_NAME2,
							CODE = this.QCircleDetails[num].CODE,
							MULTI_ORDER = 0,
							BEGIN_DATE = new DateTime?(this.QInitDateTime),
							MODIFY_DATE = new DateTime?(now)
						});
					}
					num++;
				}
			}
			return list;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00003060 File Offset: 0x00001260
		public void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00003082 File Offset: 0x00001282
		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00021ABC File Offset: 0x0001FCBC
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

		// Token: 0x06000320 RID: 800 RVA: 0x00020240 File Offset: 0x0001E440
		public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
		{
			if (list_0.Count > 1)
			{
				RandomEngine randomEngine = new RandomEngine();
				list_0 = randomEngine.RandomDetails(list_0);
			}
			return list_0;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00021B00 File Offset: 0x0001FD00
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

		// Token: 0x06000322 RID: 802 RVA: 0x00021B74 File Offset: 0x0001FD74
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

		// Token: 0x06000323 RID: 803 RVA: 0x00003097 File Offset: 0x00001297
		public void GetGroupDetails()
		{
			this.QGroupDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.PARENT_CODE);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000030B5 File Offset: 0x000012B5
		public void GetCircleGroupDetails()
		{
			this.QCircleGroupDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.PARENT_CODE);
		}

		// Token: 0x04000155 RID: 341
		private string QChildQuestion = global::GClass0.smethod_0("");

		// Token: 0x0400015D RID: 349
		public List<string> SelectedCode = new List<string>();

		// Token: 0x0400015E RID: 350
		public int SelectedCodeCount = -1;

		// Token: 0x0400015F RID: 351
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x04000160 RID: 352
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x04000161 RID: 353
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
