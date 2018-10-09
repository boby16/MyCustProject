using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class QSingleExtend
	{
		public string QuestionName { get; set; }

		public string QuestionTitle { get; set; }

		public string ParentCode { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public SurveyAnswer OneAnswer { get; set; }

		public List<SurveyAnswer> QAnswers { get; set; }

		public List<SurveyAnswer> QAnswersRead { get; set; }

		public SurveyDefine QDefine { get; set; }

		public string OtherCode { get; set; }

		public string FillText { get; set; }

		public DateTime QInitDateTime { get; set; }

		public string SelectedCode { get; set; }

		public void Init(string string_0, int int_0)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
		}

		public void GetExtendDetails(string string_0)
		{
			this.ExtendCodePlace = this.QDefine.MIN_COUNT;
			string string_ = string.Format("Select * from SurveyDetail Where DETAIL_ID = '{0}' and code in ( select distinct extend_{1} from  ( Select extend_{1} from SurveyDetail Where DETAIL_ID = '{2}' And  CODE = '{3}' ) a )", new object[]
			{
				this.QDefine.DETAIL_ID,
				this.ExtendCodePlace.ToString(),
				this.QDefine.PARENT_CODE,
				string_0
			});
			this.QDetails = this.oSurveyDetailDal.GetListBySql(string_);
			foreach (SurveyDetail surveyDetail in this.QDetails)
			{
				if (surveyDetail.IS_OTHER == 1)
				{
					this.OtherCode = surveyDetail.CODE;
				}
			}
		}

		public void BeforeSave()
		{
			this.QAnswers = this.method_0();
		}

		private List<SurveyAnswer> method_0()
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.QUESTION_NAME = this.QuestionName;
			surveyAnswer.CODE = this.SelectedCode;
			surveyAnswer.MULTI_ORDER = 0;
			surveyAnswer.BEGIN_DATE = new DateTime?(this.QInitDateTime);
			surveyAnswer.MODIFY_DATE = new DateTime?(DateTime.Now);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list.Add(surveyAnswer);
			if (this.OtherCode != null && this.OtherCode != "")
			{
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.QuestionName + "_OTH",
					CODE = this.FillText,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(DateTime.Now)
				});
			}
			return list;
		}

		public void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		public string ReadAnswerByQuestionName(string string_0, string string_1)
		{
			string text = "";
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		public virtual void GetDynamicDetails()
		{
			this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.ParentCode);
		}

		public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
		{
			if (list_0.Count > 1)
			{
				RandomEngine randomEngine = new RandomEngine();
				list_0 = randomEngine.RandomDetails(list_0);
			}
			return list_0;
		}

		public void RandomDetails()
		{
			this.QDetails = this.RandomDetails(this.QDetails);
		}

		public string GetInnerCodeText()
		{
			string result = "";
			foreach (SurveyDetail surveyDetail in this.QDetails)
			{
				if (surveyDetail.CODE == this.SelectedCode)
				{
					result = surveyDetail.CODE_TEXT;
					break;
				}
			}
			return result;
		}

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private int ExtendCodePlace;
	}
}
