using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class QMatrixSingle
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
			foreach (string code in this.SelectedCode)
			{
				string question_NAME = this.QuestionName + "_R" + this.QCircleDetails[num].CODE;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = code,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				question_NAME = this.CircleQuestionName + "_R" + this.QCircleDetails[num].CODE;
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

		public void BeforeSavebyCode(string string_0 = "99")
		{
			this.QAnswers = this.method_1(string_0);
		}

		private List<SurveyAnswer> method_1(string string_0)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			DateTime now = DateTime.Now;
			if (this.SelectedCodeCount == 0)
			{
				string question_NAME = this.QuestionName + "_R1";
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = string_0,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				question_NAME = this.QuestionName + "_C" + string_0;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = "1",
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				question_NAME = this.CircleQuestionName + "_R1";
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
					if (text != "")
					{
						string question_NAME2 = this.QuestionName + "_R" + this.QCircleDetails[num].CODE;
						list.Add(new SurveyAnswer
						{
							QUESTION_NAME = question_NAME2,
							CODE = text,
							MULTI_ORDER = 0,
							BEGIN_DATE = new DateTime?(this.QInitDateTime),
							MODIFY_DATE = new DateTime?(now)
						});
						question_NAME2 = this.QuestionName + "_C" + text;
						list.Add(new SurveyAnswer
						{
							QUESTION_NAME = question_NAME2,
							CODE = this.QCircleDetails[num].CODE,
							MULTI_ORDER = 0,
							BEGIN_DATE = new DateTime?(this.QInitDateTime),
							MODIFY_DATE = new DateTime?(now)
						});
						question_NAME2 = this.CircleQuestionName + "_R" + (num + 1).ToString();
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

		public List<string> SelectedCode = new List<string>();

		public int SelectedCodeCount = -1;

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
