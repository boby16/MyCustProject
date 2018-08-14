using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000026 RID: 38
	public class SurveyExport
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00003724 File Offset: 0x00001924
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000372C File Offset: 0x0000192C
		public List<SurveyDefine> QQuota { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00003735 File Offset: 0x00001935
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000373D File Offset: 0x0000193D
		public List<SurveyDefine> QCFilterFill { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00003746 File Offset: 0x00001946
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000374E File Offset: 0x0000194E
		public SurveyDefine QQuotaOne { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00003757 File Offset: 0x00001957
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000375F File Offset: 0x0000195F
		public List<SurveyAnswer> QAnswer { get; set; }

		// Token: 0x06000413 RID: 1043 RVA: 0x0002621C File Offset: 0x0002441C
		public List<V_DefineAnswer> GetList()
		{
			return this.oV_DefineAnswerDal.GetList();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00026238 File Offset: 0x00024438
		public List<V_DefineAnswer> GetListByQuestionType(string string_0)
		{
			return this.oV_DefineAnswerDal.GetListByQuestionType(string_0);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00026258 File Offset: 0x00024458
		public List<V_DefineAnswer> GetListByTime()
		{
			return this.oV_DefineAnswerDal.GetListByTime();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00026274 File Offset: 0x00024474
		public List<SurveyDetail> GetDetail(string string_0)
		{
			return this.oSurveyDetailDal.GetList(string_0, global::GClass0.smethod_0(""));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0002629C File Offset: 0x0002449C
		public string GetCodeText(string string_0, string string_1)
		{
			return this.oSurveyDetailDal.GetCodeText(string_0, string_1);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00003768 File Offset: 0x00001968
		public void QuotaConfig()
		{
			this.QQuota = this.oSurveyDefineDal.GetQuotaConfig();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000262B8 File Offset: 0x000244B8
		public void QCAdvanceConfig(int int_0)
		{
			if (int_0 == 101)
			{
				this.QCFilterFill = this.oSurveyDefineDal.GetQCAdvanceConfig(int_0);
			}
			else
			{
				this.QQuota = this.oSurveyDefineDal.GetQCAdvanceConfig(int_0);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000262F4 File Offset: 0x000244F4
		public string GetCodeTextExtend(string string_0, string string_1, int int_0)
		{
			return this.oSurveyDetailDal.GetCodeTextExtend(string_0, string_1, int_0);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00026314 File Offset: 0x00024514
		public string QuotaText(string string_0, string string_1, string string_2)
		{
			string result = global::GClass0.smethod_0("");
			if (string_0 != global::GClass0.smethod_0("") && string_1 != global::GClass0.smethod_0(""))
			{
				if (string_2 == global::GClass0.smethod_0(""))
				{
					result = this.oSurveyDetailDal.GetCodeText(string_0, string_1);
				}
				else
				{
					result = this.oSurveyDetailDal.GetCodeText(string_0, string_1, string_2);
				}
			}
			return result;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00026388 File Offset: 0x00024588
		public SurveyDefine SPSSFindMain(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("6ġȯ̧ТԴ؟ܔࠝग़੉୕౔ഘ๤གྷ၇ᅂቖፋᑵᕕᙉᝇᡃ᥉ᨋ᭝᱁ᵍṕὃ\u2005⅔≂⍅⑄╿♶❺⠽⤡⨻⬽Ɫ⴨⹪⼱〵ㅵ㉽㍶㐱㕳㙠㝣㡯㥥㩥㭯㱖㵡㹩㽢䁠䅼䈣䌿䐱"), string_0);
			return this.oSurveyDefineDal.GetBySql(string_);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000263B8 File Offset: 0x000245B8
		public List<SurveyDetail> GetDetails(string string_0)
		{
			return this.oSurveyDetailDal.GetDetails(string_0);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000263D4 File Offset: 0x000245D4
		public string GetDataFilePath()
		{
			return this.oSurveyConfigDal.GetByCodeText(global::GClass0.smethod_0("XŤɥͥьզٲݤࡔॢ੶୩"));
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000263F8 File Offset: 0x000245F8
		public void SaveDataFilePath(string string_0)
		{
			SurveyConfig surveyConfig = new SurveyConfig();
			surveyConfig.CODE = global::GClass0.smethod_0("XŤɥͥьզٲݤࡔॢ੶୩");
			surveyConfig.CODE_TEXT = string_0;
			this.oSurveyConfigDal.UpdateByCode(surveyConfig);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00026430 File Offset: 0x00024630
		public string GetRecodePath()
		{
			return this.oSurveyConfigDal.GetByCodeText(global::GClass0.smethod_0("XŬɫͨѢՠٔݢࡶ३"));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00026454 File Offset: 0x00024654
		public void SaveRecodePath(string string_0)
		{
			SurveyConfig surveyConfig = new SurveyConfig();
			surveyConfig.CODE = global::GClass0.smethod_0("XŬɫͨѢՠٔݢࡶ३");
			surveyConfig.CODE_TEXT = string_0;
			this.oSurveyConfigDal.UpdateByCode(surveyConfig);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0002648C File Offset: 0x0002468C
		public bool SaveSurveyMain(SurveyMain surveyMain_0)
		{
			bool result;
			if (this.oSurveyMainDal.ExistsByGUID(surveyMain_0.SURVEY_GUID))
			{
				result = false;
			}
			else
			{
				this.oSurveyMainDal.Add(surveyMain_0);
				result = true;
			}
			return result;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000377B File Offset: 0x0000197B
		public void SaveSurveyAnswer(SurveyAnswer surveyAnswer_0)
		{
			this.oSurveyAnswerDal.Add(surveyAnswer_0);
		}

		// Token: 0x040001CB RID: 459
		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		// Token: 0x040001CC RID: 460
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x040001CD RID: 461
		private V_DefineAnswerDal oV_DefineAnswerDal = new V_DefineAnswerDal();

		// Token: 0x040001CE RID: 462
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x040001CF RID: 463
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x040001D0 RID: 464
		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();
	}
}
