using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class QuotaManager
	{
		public List<SurveyQuota> GetAllQuota()
		{
			return this.oSurveyQuotaDal.GetList();
		}

		public SurveyQuota GetQuotaByID(int int_0)
		{
			return this.oSurveyQuotaDal.GetByID(int_0);
		}

		public bool CheckQuota(string string_0, string string_1, janswer janswer_0, out string string_2)
		{
			bool flag = false;
			string_2 = "";
			if (this.oSurveyQuotaDal.ExistsByQName(janswer_0.questionname, janswer_0.code))
			{
				string string_3 = string.Format("select * from SurveyQuota where QUESTION_NAME ='{0}' and CODE='{1}'", janswer_0.questionname, janswer_0.code);
				SurveyQuota bySql = this.oSurveyQuotaDal.GetBySql(string_3);
				string_2 = bySql.QUESTION_TITLE;
				int num = bySql.SAMPLE_TARGET + bySql.SAMPLE_BACKUP - bySql.SAMPLE_FINISH;
				int num2 = bySql.SAMPLE_TARGET + bySql.SAMPLE_BACKUP - (bySql.SAMPLE_FINISH + bySql.SAMPLE_RUNNING);
				if (bySql.SAMPLE_OVER == 1 && num <= 0)
				{
					flag = true;
				}
				if (bySql.SAMPLE_OVER == 2 && num2 <= 0)
				{
					flag = true;
				}
				if (!flag)
				{
					SurveyQuotaAnswer surveyQuotaAnswer = new SurveyQuotaAnswer();
					surveyQuotaAnswer.SURVEY_ID = string_0;
					surveyQuotaAnswer.SURVEY_GUID = string_1;
					surveyQuotaAnswer.QUESTION_NAME = bySql.QUESTION_NAME;
					surveyQuotaAnswer.CODE = bySql.CODE;
					surveyQuotaAnswer.IS_FINISH = 0;
					int num3 = this.oSurveyQuotaAnswerDal.AddOne(surveyQuotaAnswer);
					if (num3 == 0)
					{
						bySql.SAMPLE_RUNNING++;
					}
					if (bySql.SAMPLE_OVER == 1)
					{
						bySql.SAMPLE_REAL = bySql.SAMPLE_FINISH;
					}
					if (bySql.SAMPLE_OVER == 2)
					{
						bySql.SAMPLE_REAL = bySql.SAMPLE_FINISH + bySql.SAMPLE_RUNNING;
					}
					bySql.SAMPLE_BALANCE = bySql.SAMPLE_TOTAL - bySql.SAMPLE_REAL;
					if (bySql.SAMPLE_BALANCE > 0)
					{
						bySql.IS_FULL = "否";
					}
					else
					{
						bySql.IS_FULL = "是";
					}
					this.oSurveyQuotaDal.Update(bySql);
				}
			}
			return flag;
		}

		public void CalcQuota()
		{
			List<SurveyQuota> list = this.oSurveyQuotaDal.GetList();
			foreach (SurveyQuota surveyQuota_ in list)
			{
				this.CalcQuotaOne(surveyQuota_);
			}
		}

		public void CalcQuotaOne(SurveyQuota surveyQuota_0)
		{
			surveyQuota_0.SAMPLE_TOTAL = surveyQuota_0.SAMPLE_TARGET + surveyQuota_0.SAMPLE_BACKUP;
			surveyQuota_0.SAMPLE_FINISH = this.oSurveyQuotaAnswerDal.GetSumByModel(1, surveyQuota_0.QUESTION_NAME, surveyQuota_0.CODE);
			surveyQuota_0.SAMPLE_RUNNING = this.oSurveyQuotaAnswerDal.GetSumByModel(0, surveyQuota_0.QUESTION_NAME, surveyQuota_0.CODE);
			if (surveyQuota_0.SAMPLE_OVER == 1)
			{
				surveyQuota_0.SAMPLE_REAL = surveyQuota_0.SAMPLE_FINISH;
			}
			if (surveyQuota_0.SAMPLE_OVER == 2)
			{
				surveyQuota_0.SAMPLE_REAL = surveyQuota_0.SAMPLE_FINISH + surveyQuota_0.SAMPLE_RUNNING;
			}
			surveyQuota_0.SAMPLE_BALANCE = surveyQuota_0.SAMPLE_TOTAL - surveyQuota_0.SAMPLE_REAL;
			if (surveyQuota_0.SAMPLE_BALANCE > 0)
			{
				surveyQuota_0.IS_FULL = "否";
			}
			else
			{
				surveyQuota_0.IS_FULL = "是";
			}
			this.oSurveyQuotaDal.Update(surveyQuota_0);
		}

		public void CalcQuotaOneNoSum(SurveyQuota surveyQuota_0)
		{
			surveyQuota_0.SAMPLE_TOTAL = surveyQuota_0.SAMPLE_TARGET + surveyQuota_0.SAMPLE_BACKUP;
			surveyQuota_0.SAMPLE_FINISH = this.oSurveyQuotaAnswerDal.GetSumByModel(1, surveyQuota_0.QUESTION_NAME, surveyQuota_0.CODE);
			surveyQuota_0.SAMPLE_RUNNING = this.oSurveyQuotaAnswerDal.GetSumByModel(0, surveyQuota_0.QUESTION_NAME, surveyQuota_0.CODE);
			if (surveyQuota_0.SAMPLE_OVER == 1)
			{
				surveyQuota_0.SAMPLE_REAL = surveyQuota_0.SAMPLE_FINISH;
			}
			if (surveyQuota_0.SAMPLE_OVER == 2)
			{
				surveyQuota_0.SAMPLE_REAL = surveyQuota_0.SAMPLE_FINISH + surveyQuota_0.SAMPLE_RUNNING;
			}
			surveyQuota_0.SAMPLE_BALANCE = surveyQuota_0.SAMPLE_TOTAL - surveyQuota_0.SAMPLE_REAL;
			if (surveyQuota_0.SAMPLE_BALANCE > 0)
			{
				surveyQuota_0.IS_FULL = "否";
			}
			else
			{
				surveyQuota_0.IS_FULL = "是";
			}
			this.oSurveyQuotaDal.Update(surveyQuota_0);
		}

		public void QuotaFinish(string string_0, string string_1, string string_2)
		{
			if (!(string_2 == "1"))
			{
				if (!(string_2 == "2"))
				{
					if (!(string_2 == "3") && string_2 == "4")
					{
						this.oSurveyQuotaAnswerDal.UpdateStatus(4, string_0, string_1);
						this.oSurveyQuotaDal.UpdateStatus(string_0, string_1, 0, 1);
					}
				}
				else
				{
					this.oSurveyQuotaAnswerDal.UpdateStatus(2, string_0, string_1);
					this.oSurveyQuotaDal.UpdateStatus(string_0, string_1, 0, 1);
				}
			}
			else
			{
				this.oSurveyQuotaAnswerDal.UpdateStatus(1, string_0, string_1);
				this.oSurveyQuotaDal.UpdateStatus(string_0, string_1, 1, 1);
			}
		}

		public List<jquotaanswer> BuildQuotaJson(string string_0, string string_1, QSingle qsingle_0, string string_2, string string_3, string string_4)
		{
			List<jquotaanswer> list = new List<jquotaanswer>();
			jquotaanswer jquotaanswer = new jquotaanswer();
			jquotaanswer.surveyid = string_0;
			jquotaanswer.surveyguid = string_3;
			jquotaanswer.pageid = string_1;
			jquotaanswer.projectid = string_2;
			jquotaanswer.isfinish = string_4;
			janswer janswer = new janswer();
			janswer.questionname = qsingle_0.QuestionName;
			janswer.code = qsingle_0.SelectedCode;
			jquotaanswer.qanswers.Add(janswer);
			list.Add(jquotaanswer);
			return list;
		}

		public bool CheckIsQuotaQuestion(string string_0, string string_1, string string_2)
		{
			return this.oSurveyQuotaDal.ExistsMatchQuota(string_0, string_1, string_2);
		}

		private SurveyQuotaDal oSurveyQuotaDal = new SurveyQuotaDal();

		private SurveyQuotaAnswerDal oSurveyQuotaAnswerDal = new SurveyQuotaAnswerDal();
	}
}
