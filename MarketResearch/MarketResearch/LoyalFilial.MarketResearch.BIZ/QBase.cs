using System;
using System.Collections.Generic;
using LoyalFilial.MarketResearch.DAL;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.BIZ
{
	public class QBase
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

		public virtual void Init(string string_0, int int_0)
		{
			this.QInitDateTime = DateTime.Now;
			string otherCode = "";
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.ParentCode = this.QDefine.PARENT_CODE;
			if (this.QDefine.DETAIL_ID != "")
			{
				if (this.QDefine.PARENT_CODE == "")
				{
					this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out otherCode);
					this.OtherCode = otherCode;
				}
				else if (!(this.QDefine.PARENT_CODE == "DYNAMIC"))
				{
					this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.QDefine.PARENT_CODE);
				}
			}
		}

		public virtual void BeforeSave()
		{
		}

		public virtual void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		public virtual void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		public virtual string ReadAnswerByQuestionName(string string_0, string string_1)
		{
			string text = "";
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		public virtual void GetDynamicDetails()
		{
			this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.ParentCode);
		}

		public SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
