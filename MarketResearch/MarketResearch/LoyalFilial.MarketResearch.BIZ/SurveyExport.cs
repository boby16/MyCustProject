using System;
using System.Collections.Generic;
using LoyalFilial.MarketResearch.DAL;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.BIZ
{
	public class SurveyExport
	{
		public List<SurveyDefine> QQuota { get; set; }

		public List<SurveyDefine> QCFilterFill { get; set; }

		public SurveyDefine QQuotaOne { get; set; }

		public List<SurveyAnswer> QAnswer { get; set; }

		public List<V_DefineAnswer> GetList()
		{
			return this.oV_DefineAnswerDal.GetList();
		}

		public List<V_DefineAnswer> GetListByQuestionType(string string_0)
		{
			return this.oV_DefineAnswerDal.GetListByQuestionType(string_0);
		}

		public List<V_DefineAnswer> GetListByTime()
		{
			return this.oV_DefineAnswerDal.GetListByTime();
		}

		public List<SurveyDetail> GetDetail(string string_0)
		{
			return this.oSurveyDetailDal.GetList(string_0, "");
		}

		public string GetCodeText(string string_0, string string_1)
		{
			return this.oSurveyDetailDal.GetCodeText(string_0, string_1);
		}

		public void QuotaConfig()
		{
			this.QQuota = this.oSurveyDefineDal.GetQuotaConfig();
		}

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

		public string GetCodeTextExtend(string string_0, string string_1, int int_0)
		{
			return this.oSurveyDetailDal.GetCodeTextExtend(string_0, string_1, int_0);
		}

		public string QuotaText(string string_0, string string_1, string string_2)
		{
			string result = "";
			if (string_0 != "" && string_1 != "")
			{
				if (string_2 == "")
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

		public SurveyDefine SPSSFindMain(string string_0)
		{
			string string_ = string.Format("select * from SurveyDefine where page_id = '{0}' and combine_index =0", string_0);
			return this.oSurveyDefineDal.GetBySql(string_);
		}

		public List<SurveyDetail> GetDetails(string string_0)
		{
			return this.oSurveyDetailDal.GetDetails(string_0);
		}

		public string GetDataFilePath()
		{
			return this.oSurveyConfigDal.GetByCodeText("ToolDataPath");
		}

		public void SaveDataFilePath(string string_0)
		{
			SurveyConfig surveyConfig = new SurveyConfig();
			surveyConfig.CODE = "ToolDataPath";
			surveyConfig.CODE_TEXT = string_0;
			this.oSurveyConfigDal.UpdateByCode(surveyConfig);
		}

		public string GetRecodePath()
		{
			return this.oSurveyConfigDal.GetByCodeText("RecodePath");
		}

		public void SaveRecodePath(string string_0)
		{
			SurveyConfig surveyConfig = new SurveyConfig();
			surveyConfig.CODE = "RecodePath";
			surveyConfig.CODE_TEXT = string_0;
			this.oSurveyConfigDal.UpdateByCode(surveyConfig);
		}

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

		public void SaveSurveyAnswer(SurveyAnswer surveyAnswer_0)
		{
			this.oSurveyAnswerDal.Add(surveyAnswer_0);
		}

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private V_DefineAnswerDal oV_DefineAnswerDal = new V_DefineAnswerDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();
	}
}
