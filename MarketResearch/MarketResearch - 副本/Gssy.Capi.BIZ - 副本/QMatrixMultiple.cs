using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class QMatrixMultiple
	{
		public string QuestionName { get; set; }

		public string CircleQuestionName { get; set; }

		public string ParentCode { get; set; }

		public SurveyDefine QDefine { get; set; }

		public SurveyDefine QCircleDefine { get; set; }

		public List<SurveyDetail> QCircleDetails { get; set; }

		public List<SurveyDetail> QCircleGroupDetails { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public List<SurveyDetail> QGroupDetails { get; set; }

		public List<SurveyAnswer> QAnswers { get; set; }

		public List<SurveyAnswer> QAnswersRead { get; set; }

		public DateTime QInitDateTime { get; set; }

		public void Init(string string_0, int int_0, bool GetDetail = true)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.CircleQuestionName = ((this.QDefine.GROUP_LEVEL == "B") ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.ParentCode = this.QDefine.PARENT_CODE;
			if (this.QDefine.DETAIL_ID != "" && GetDetail)
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
				if (this.QDefine.PARENT_CODE != "")
				{
					this.GetGroupDetails();
				}
			}
			this.QChildQuestion = ((this.QDefine.GROUP_LEVEL == "B") ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.QCircleDefine = this.oSurveyDefineDal.GetByName(this.QChildQuestion);
			if (this.QCircleDefine.DETAIL_ID != "" && GetDetail)
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
				if (this.QCircleDefine.PARENT_CODE != "")
				{
					this.GetCircleGroupDetails();
				}
			}
		}

		public void BeforeSave()
		{
			this.QAnswers = this.method_0();
		}

		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			int num = 0;
			foreach (classMultipleAnswers classMultipleAnswers in this.SelectedCodes)
			{
				string text = this.QuestionName + "_R" + this.QCircleDetails[num].CODE;
				SurveyAnswer surveyAnswer = new SurveyAnswer();
				for (int i = 0; i < classMultipleAnswers.Answers.Count; i++)
				{
					list.Add(new SurveyAnswer
					{
						QUESTION_NAME = text + "_A" + (i + 1).ToString(),
						CODE = classMultipleAnswers.Answers[i].ToString(),
						MULTI_ORDER = i + 1,
						BEGIN_DATE = new DateTime?(this.QInitDateTime),
						MODIFY_DATE = new DateTime?(now)
					});
				}
				text = this.CircleQuestionName + "_R" + this.QCircleDetails[num].CODE;
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

		public void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		public List<SurveyAnswer> ReadAnswerByQuestionName(string string_0, string string_1, int int_0)
		{
			return this.oSurveyAnswerDal.GetListByMultipleBySequenceId(string_0, string_1, int_0);
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

		public void InitDetailID(string string_0, int int_0)
		{
			if (this.QDefine.DETAIL_ID != "")
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
				if (this.QDefine.PARENT_CODE != "")
				{
					this.GetGroupDetails();
				}
			}
			if (this.QCircleDefine.DETAIL_ID != "")
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
				if (this.QCircleDefine.PARENT_CODE != "")
				{
					this.GetCircleGroupDetails();
				}
			}
		}

		public void GetGroupDetails()
		{
			this.QGroupDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.PARENT_CODE);
		}

		public void GetCircleGroupDetails()
		{
			this.QCircleGroupDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.PARENT_CODE);
		}

		private string QChildQuestion = "";

		public List<classMultipleAnswers> SelectedCodes = new List<classMultipleAnswers>();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
