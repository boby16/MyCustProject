using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class SurveyBiz
	{
		public SurveyMain MySurvey { get; set; }

		public List<SurveyAnswer> QAnswers { get; set; }

		public List<V_SurveyQC> QVSurveyQC { get; set; }

		public List<V_Summary> QVSummary { get; set; }

		public List<SurveyMain> QMain { get; set; }

		public string RecordFileName { get; set; }

		public bool ExistSurvey(string string_0)
		{
			return this.oSurveyMainDal.Exists(string_0);
		}

		public void GetSurveyMain(int int_0)
		{
			this.QMain = this.oSurveyMainDal.GetSurveyMain(int_0);
		}

		public List<SurveyMain> GetSurveyMainList(int int_0)
		{
			return this.oSurveyMainDal.GetSurveyMain(int_0);
		}

		public void GetBySurveyId(string string_0)
		{
			this.MySurvey = this.oSurveyMainDal.GetBySurveyId(string_0);
		}

		public SurveyMain GetSurveyMainListBySurveyId(string string_0)
		{
			return this.oSurveyMainDal.GetBySurveyId(string_0);
		}

		public List<SurveyAnswer> GetBySysAudio(string string_0)
		{
			return this.oSurveyAnswerDal.GetListRecord(string_0);
		}

		public SurveySequence GetAudioByPageId(string string_0, int int_0, string string_1)
		{
			return this.oSurveySequenceDal.GetAudioByPageId(string_0, int_0, string_1);
		}

		public List<SurveyAnswer> GetBySysPhoto(string string_0)
		{
			return this.oSurveyAnswerDal.GetListByCode(string_0, "SURVEY_PHOTO");
		}

		public void GetSurveyyQC(string string_0)
		{
			this.QVSurveyQC = this.oVSurveyQCDal.GetListBySurveyId(string_0);
			for (int i = 0; i < this.QVSurveyQC.Count; i++)
			{
				if (this.QVSurveyQC[i].DETAIL_ID != "" && this.QVSurveyQC[i].CODE != "")
				{
					this.QVSurveyQC[i].CODE_TEXT = this.oSurveyDetailDal.GetCodeText(this.QVSurveyQC[i].DETAIL_ID, this.QVSurveyQC[i].CODE);
				}
				if (this.QVSurveyQC[i].DETAIL_ID == "" && this.QVSurveyQC[i].CODE != "")
				{
					this.QVSurveyQC[i].CODE_TEXT = this.QVSurveyQC[i].CODE;
				}
				this.QVSurveyQC[i].QUESTION_TITLE = this.QVSurveyQC[i].QUESTION_TITLE.Replace("<B>", "");
				this.QVSurveyQC[i].QUESTION_TITLE = this.QVSurveyQC[i].QUESTION_TITLE.Replace("</B>", "");
				this.QVSurveyQC[i].QUESTION_TITLE = this.QVSurveyQC[i].QUESTION_TITLE.Replace("<BR>", "");
			}
		}

		public void GetSummary(string string_0)
		{
			this.QVSummary = this.oVSummaryDal.GetListBySurveyId(string_0);
			for (int i = 0; i < this.QVSummary.Count; i++)
			{
				if (this.QVSummary[i].DETAIL_ID != "" && this.QVSummary[i].CODE != "")
				{
					this.QVSummary[i].CODE_TEXT = this.oSurveyDetailDal.GetCodeText(this.QVSummary[i].DETAIL_ID, this.QVSummary[i].CODE);
				}
				if (this.QVSummary[i].DETAIL_ID == "" && this.QVSummary[i].CODE != "")
				{
					this.QVSummary[i].CODE_TEXT = this.QVSummary[i].CODE;
				}
			}
		}

		public void UpdateSurvey()
		{
			this.oSurveyMainDal.Update(this.MySurvey);
		}

		public void UpdateSurveyLastTime(string string_0)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			bySurveyId.LAST_START_TIME = new DateTime?(DateTime.Now);
			bySurveyId.LAST_END_TIME = new DateTime?(DateTime.Now);
			this.oSurveyMainDal.Update(bySurveyId);
		}

		public void AddSurvey(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
		{
			this.oSurveyMainDal.Add(string_0, string_1, string_2, string_3, string_4);
			this.oSurveyAnswerDal.AddOne(string_0, "SURVEY_DATE", string.Format("{0:yyyy/MM/dd}", DateTime.Now), 0);
			this.oSurveyAnswerDal.AddOne(string_0, "SURVEY_FIRST_START", string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now), 0);
			this.oSurveyAnswerDal.AddOne(string_0, "SURVEY_VERSION_ID", string_1, 0);
			string byCodeText = this.oSurveyConfigDal.GetByCodeText("PCCode");
			this.oSurveyAnswerDal.AddOne(string_0, "PC_ID", byCodeText, 0);
		}

		public int GetEndType(string string_0, int int_0, string string_1)
		{
			int int_ = int_0 - 1;
			SurveySequence bySequenceID = this.oSurveySequenceDal.GetBySequenceID(string_0, int_);
			int result;
			if (bySequenceID.PAGE_ID == string_1)
			{
				result = 1;
			}
			else
			{
				result = 2;
			}
			return result;
		}

		public bool CloseSurveyByExit(string string_0, int int_0)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			bySurveyId.IS_FINISH = int_0;
			bySurveyId.END_TIME = new DateTime?(DateTime.Now);
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, "SURVEY_USER");
			bySurveyId.USER_ID = one.CODE;
			this.oSurveyMainDal.Update(bySurveyId);
			if (this.RecordFileName != "" && this.RecordFileName != null)
			{
				this.oSurveyAnswerDal.AddOneNoUpdate(string_0, "SURVEY_AUDIO", this.RecordFileName, 0);
			}
			return true;
		}

		public void CloseSurvey(string string_0, int int_0)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			bySurveyId.IS_FINISH = int_0;
			if (bySurveyId.START_TIME == bySurveyId.END_TIME)
			{
				bySurveyId.END_TIME = new DateTime?(DateTime.Now);
			}
			bySurveyId.LAST_END_TIME = new DateTime?(DateTime.Now);
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, "SURVEY_USER");
			bySurveyId.USER_ID = one.CODE;
			this.oSurveyMainDal.Update(bySurveyId);
			if (int_0 != 3)
			{
				this.method_0(bySurveyId);
				if (this.RecordFileName != "")
				{
					this.oSurveyAnswerDal.AddOneNoUpdate(string_0, "SURVEY_AUDIO", this.RecordFileName, 0);
				}
				List<SurveyRandom> listBySurveyId = this.oSurveyRandomDal.GetListBySurveyId(string_0);
				bool flag = false;
				string a = "";
				using (List<SurveyRandom>.Enumerator enumerator = listBySurveyId.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SurveyRandom surveyRandom = enumerator.Current;
						if (a != surveyRandom.QUESTION_SET)
						{
							flag = this.oSurveyAnswerDal.Exists(string_0, surveyRandom.QUESTION_SET, "_R");
							a = surveyRandom.QUESTION_SET;
						}
						if (!flag)
						{
							SurveyAnswer surveyAnswer = new SurveyAnswer();
							surveyAnswer.SURVEY_ID = string_0;
							surveyAnswer.QUESTION_NAME = surveyRandom.QUESTION_SET + "_R" + surveyRandom.RANDOM_INDEX.ToString();
							surveyAnswer.CODE = surveyRandom.CODE;
							surveyAnswer.MULTI_ORDER = surveyRandom.RANDOM_INDEX;
							surveyAnswer.MODIFY_DATE = new DateTime?(DateTime.Now);
							surveyAnswer.SEQUENCE_ID = 90000;
							surveyAnswer.SURVEY_GUID = surveyRandom.QUESTION_SET;
							surveyAnswer.BEGIN_DATE = new DateTime?(DateTime.Now);
							this.oSurveyAnswerDal.Add(surveyAnswer);
						}
					}
					return;
				}
			}
			if (this.RecordFileName != "")
			{
				this.oSurveyAnswerDal.AddOneNoUpdate(string_0, "SURVEY_AUDIO", this.RecordFileName, 0);
			}
		}

		private void method_0(SurveyMain surveyMain_0)
		{
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "SURVEY_ID", surveyMain_0.SURVEY_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "VERSION_ID", surveyMain_0.VERSION_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "USER_ID", surveyMain_0.USER_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "CITY_ID", surveyMain_0.CITY_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "START_TIME", surveyMain_0.START_TIME.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "END_TIME", surveyMain_0.END_TIME.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "LAST_START_TIME", surveyMain_0.LAST_START_TIME.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "LAST_END_TIME", surveyMain_0.LAST_END_TIME.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "CUR_PAGE_ID", surveyMain_0.CUR_PAGE_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "CIRCLE_A_CURRENT", surveyMain_0.CIRCLE_A_CURRENT.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "CIRCLE_A_COUNT", surveyMain_0.CIRCLE_A_COUNT.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "CIRCLE_B_CURRENT", surveyMain_0.CIRCLE_B_CURRENT.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "CIRCLE_B_COUNT", surveyMain_0.CIRCLE_B_COUNT.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "IS_FINISH", surveyMain_0.IS_FINISH.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "SEQUENCE_ID", surveyMain_0.SEQUENCE_ID.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "SURVEY_GUID", surveyMain_0.SURVEY_GUID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "CLIENT_ID", surveyMain_0.CLIENT_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "PROJECT_ID", surveyMain_0.PROJECT_ID, 0);
			string arg = surveyMain_0.END_TIME.ToString();
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "SURVEY_FIRST_END", string.Format("{0:yyyy/MM/dd HH:mm:ss}", arg), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "SURVEY_LAST_END", string.Format("{0:yyyy/MM/dd HH:mm:ss}", arg), 0);
			string text = surveyMain_0.SURVEY_GUID.ToString();
			if (surveyMain_0.VERSION_ID.IndexOf("正式版") > -1)
			{
				text += "正式版";
			}
			else
			{
				text += "测试版";
			}
			string string_ = EncryptTool.Encrypt(text, "MEncrypt");
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "SURVEY_INNER_CODE", string_, 0);
			TimeSpan timeSpan = surveyMain_0.END_TIME.Value - surveyMain_0.START_TIME.Value;
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, "SURVEY_COST_TIME", ((int)timeSpan.TotalMinutes).ToString(), 0);
		}

		public string GetUserID(string string_0)
		{
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, "SURVEY_USER");
			return one.CODE;
		}

		public void UpdateSurvey(string string_0, string string_1)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			bySurveyId.CUR_PAGE_ID = string_1;
			this.oSurveyMainDal.Update(bySurveyId);
		}

		public void SaveAnswer(string string_0, int int_0, string[,] string_1, int int_1)
		{
			this.oSurveyAnswerDal.SaveByArray(string_0, int_0, string_1, int_1);
		}

		public void SaveOneAnswer(string string_0, int int_0, string string_1, string string_2)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.SURVEY_ID = string_0;
			surveyAnswer.QUESTION_NAME = string_1;
			surveyAnswer.CODE = string_2;
			surveyAnswer.MULTI_ORDER = 0;
			surveyAnswer.MODIFY_DATE = new DateTime?(DateTime.Now);
			surveyAnswer.SEQUENCE_ID = int_0;
			surveyAnswer.SURVEY_GUID = "";
			surveyAnswer.BEGIN_DATE = new DateTime?(DateTime.Now);
			this.oSurveyAnswerDal.SaveOneAnswer(surveyAnswer);
		}

		public void ClearPageAnswer(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveySequenceDal.DeleteBySequenceID(string_0, int_0);
		}

		public string GetOneAnswerCode(string string_0, string string_1, string string_2, int int_0)
		{
			string result = "";
			try
			{
				SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
				if (int_0 == 1)
				{
					result = one.CODE;
				}
				else
				{
					string code = one.CODE;
					result = this.oSurveyDetailDal.GetCodeText(string_2, code);
				}
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		public List<SurveyAnswer> GetListBySequenceId(string string_0, int int_0)
		{
			return this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		public string GetInfoBySequenceId(string string_0, int int_0)
		{
			string text = "";
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
			foreach (SurveyAnswer surveyAnswer in list)
			{
				text = text + Environment.NewLine + string.Format("题号编码:{0}   答案：{1}", surveyAnswer.QUESTION_NAME, surveyAnswer.CODE);
			}
			return text;
		}

		public string GetNextSurveyId(string string_0, int int_0)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			text = this.oSurveyMainDal.GetMaxSurveyIDByCity(string_0);
			for (int i = 0; i < int_0 - 1; i++)
			{
				text2 += "0";
			}
			text2 += "1";
			for (int j = 0; j < int_0; j++)
			{
				text3 += "9";
			}
			if (text == "")
			{
				text = string_0 + text2;
			}
			else if (text.Substring(1, int_0) == text3)
			{
				text = "";
			}
			else
			{
				text = (int.Parse(text) + 1).ToString();
			}
			return text;
		}

		public string GetCityCode(string string_0)
		{
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, "CITY");
			return one.CODE;
		}

		public void AddSurvyeSync(string string_0, string string_1, int int_0)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			SurveySync surveySync = new SurveySync();
			surveySync.SURVEY_ID = string_0;
			surveySync.SURVEY_GUID = bySurveyId.SURVEY_GUID;
			surveySync.SYNC_DATE = new DateTime?(DateTime.Now);
			surveySync.SYNC_NOTE = string_1;
			surveySync.SYNC_STATE = int_0;
			SurveySyncDal surveySyncDal = new SurveySyncDal();
			surveySyncDal.Add(surveySync);
		}

		public bool CheckUploadSync(string string_0)
		{
			bool result = false;
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			SurveySyncDal surveySyncDal = new SurveySyncDal();
			if (surveySyncDal.CheckObjectExist(string_0, bySurveyId.SURVEY_GUID))
			{
				SurveySync bySuveyID_GUID = surveySyncDal.GetBySuveyID_GUID(string_0, bySurveyId.SURVEY_GUID);
				if (bySuveyID_GUID.SYNC_STATE == 1)
				{
					result = true;
				}
			}
			return result;
		}

		public string GetCodeFromAnswerList(List<SurveyAnswer> list_0, string string_0)
		{
			SurveyBiz.Class9 @class = new SurveyBiz.Class9();
			@class.QuestionName = string_0;
			string result = "";
			int num = list_0.FindIndex(new Predicate<SurveyAnswer>(@class.method_0));
			if (num > -1)
			{
				result = list_0[num].CODE;
			}
			return result;
		}

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveySequenceDal oSurveySequenceDal = new SurveySequenceDal();

		private V_SurveyQCDal oVSurveyQCDal = new V_SurveyQCDal();

		private V_SummaryDal oVSummaryDal = new V_SummaryDal();

		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		[CompilerGenerated]
		private sealed class Class9
		{
			internal bool method_0(SurveyAnswer surveyAnswer_0)
			{
				return surveyAnswer_0.QUESTION_NAME == this.QuestionName;
			}

			public string QuestionName;
		}
	}
}
