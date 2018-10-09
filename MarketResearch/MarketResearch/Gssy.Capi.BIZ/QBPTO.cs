using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class QBPTO
	{
		public string QuestionName { get; set; }

		public string CircleAQuestionName { get; set; }

		public string CircleBQuestionName { get; set; }

		public SurveyDefine QDefine { get; set; }

		public SurveyDefine QCircleADefine { get; set; }

		public SurveyDefine QCircleBDefine { get; set; }

		public List<SurveyDetail> QCircleADetails { get; set; }

		public List<SurveyDetail> QCircleBDetails { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public List<SurveyAnswer> QAnswers { get; set; }

		public List<SurveyAnswer> QAnswersRead { get; set; }

		public DateTime QInitDateTime { get; set; }

		public void Init(string string_0, int int_0, bool GetDetail = true)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.CircleAQuestionName = this.QDefine.GROUP_CODEA;
			this.CircleBQuestionName = this.QDefine.GROUP_CODEB;
			if (this.QDefine.DETAIL_ID != "" && GetDetail)
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
			}
			this.QCircleADefine = this.oSurveyDefineDal.GetByName(this.CircleAQuestionName);
			this.QCircleBDefine = this.oSurveyDefineDal.GetByName(this.CircleBQuestionName);
			if (this.QCircleADefine.DETAIL_ID != "")
			{
				this.QCircleADetails = this.oSurveyDetailDal.GetDetails(this.QCircleADefine.DETAIL_ID);
			}
			if (this.QCircleBDefine.DETAIL_ID != "")
			{
				this.QCircleBDetails = this.oSurveyDetailDal.GetDetails(this.QCircleBDefine.DETAIL_ID);
			}
		}

		public void InitDetailID(string string_0, int int_0)
		{
			if (this.QDefine.DETAIL_ID != "")
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
			}
		}

		public void BeforeSave(List<SurveyAnswer> list_0)
		{
			this.QAnswers = list_0;
		}

		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> result = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			return result;
		}

		public void BeforeSavebyCode()
		{
			this.QAnswers = this.method_1();
		}

		private List<SurveyAnswer> method_1()
		{
			List<SurveyAnswer> result = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			return result;
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
			text = one.CODE;
			if (text == null)
			{
				text = "";
			}
			return text;
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

		public List<string> SelectedCode = new List<string>();

		public int SelectedCodeCount = -1;

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
