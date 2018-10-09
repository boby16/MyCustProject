using System;
using System.Collections.Generic;
using System.Linq;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class QMultiple
	{
		public string ParentCode { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public SurveyAnswer OneAnswer { get; set; }

		public List<SurveyAnswer> QAnswers { get; set; }

		public List<SurveyAnswer> QAnswersRead { get; set; }

		public SurveyDefine QDefine { get; set; }

		public SurveyDefine QCircleDefine { get; set; }

		public List<SurveyDetail> QCircleDetails { get; set; }

		public Dictionary<string, string> FillTexts { get; set; }

		public string ExclusiveCode { get; set; }

		public string OtherCode { get; set; }

		public string OtherCodeTitle { get; set; }

		public string AddFillCode { get; set; }

		public string AddFillCodeTitle { get; set; }

		public string AddFillText { get; set; }

		public DateTime QInitDateTime { get; set; }

		public List<string> SelectedValues { get; set; }

		public List<SurveyDetail> QGroupDetails { get; set; }

		public void Init(string string_0, int int_0, bool GetDetail = true)
		{
			string otherCode = "";
			string otherCodeTitle = "";
			string exclusiveCode = "";
			string addFillCode = "";
			string addFillCodeTitle = "";
			this.SelectedValues = new List<string>();
			this.FillTexts = new Dictionary<string, string>();
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.ParentCode = this.QDefine.PARENT_CODE;
			if (this.QDefine.DETAIL_ID != "" && GetDetail)
			{
				if (this.QDefine.PARENT_CODE == "" || int_0 == 0)
				{
					this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out otherCode, out otherCodeTitle, out exclusiveCode, out addFillCode, out addFillCodeTitle);
					this.OtherCode = otherCode;
					this.OtherCodeTitle = otherCodeTitle;
					this.ExclusiveCode = exclusiveCode;
					this.AddFillCode = addFillCode;
					this.AddFillCodeTitle = addFillCodeTitle;
				}
				else if (!(this.QDefine.PARENT_CODE == "DYNAMIC") && int_0 > 0)
				{
					this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.QDefine.PARENT_CODE);
				}
			}
		}

		public void InitCircle()
		{
			this.QChildQuestion = ((this.QDefine.GROUP_LEVEL == "B") ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.QCircleDefine = this.oSurveyDefineDal.GetByName(this.QChildQuestion);
			if (this.QCircleDefine.DETAIL_ID != "")
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
			}
		}

		public void BeforeSave()
		{
			this.QAnswers = this.method_0();
		}

		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			for (int i = 0; i < this.SelectedValues.Count; i++)
			{
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.QuestionName + "_A" + (i + 1).ToString(),
					CODE = this.SelectedValues[i].ToString(),
					MULTI_ORDER = i + 1,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(DateTime.Now)
				});
			}
			if (this.FillText != "")
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
			if (this.FillTexts.Count<KeyValuePair<string, string>>() > 0)
			{
				foreach (string text in this.FillTexts.Keys)
				{
					if (this.FillTexts[text] != "")
					{
						list.Add(new SurveyAnswer
						{
							QUESTION_NAME = this.QuestionName + "_OTH_C" + text,
							CODE = this.FillTexts[text],
							MULTI_ORDER = 0,
							BEGIN_DATE = new DateTime?(this.QInitDateTime),
							MODIFY_DATE = new DateTime?(DateTime.Now)
						});
					}
				}
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

		public void GetDynamicDetails()
		{
			this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.ParentCode);
		}

		public void ResetOtherCode()
		{
			this.OtherCode = "";
			foreach (SurveyDetail surveyDetail in this.QDetails)
			{
				if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3)
				{
					this.OtherCode = surveyDetail.CODE;
					break;
				}
			}
		}

		public string GetInnerCodeText(string string_0)
		{
			string result = "";
			foreach (SurveyDetail surveyDetail in this.QDetails)
			{
				if (surveyDetail.CODE == string_0)
				{
					result = surveyDetail.CODE_TEXT;
					break;
				}
			}
			return result;
		}

		public void GetGroupDetails()
		{
			this.QGroupDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.PARENT_CODE);
		}

		public void InitDetailID(string string_0, int int_0)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			if (this.QDefine.DETAIL_ID != "")
			{
				if (this.QDefine.PARENT_CODE == "" || int_0 == 0)
				{
					this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out text, out text2, out text3, out text4, out text5);
				}
				else if (!(this.QDefine.PARENT_CODE == "DYNAMIC") && int_0 > 0)
				{
					this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.QDefine.PARENT_CODE);
				}
			}
		}

		public string QuestionName = "";

		public string QuestionTitle = "";

		private string QChildQuestion = "";

		public string FillText = "";

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
