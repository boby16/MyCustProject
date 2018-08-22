using System;
using System.Collections.Generic;
using System.Linq;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class CustomerBiz
	{
		public SurveyRoadMap RoadMap { get; set; }

		public string RouteResult { get; set; }

		public List<string> RouteResultValues { get; set; }

		public List<SurveyAnswer> AnswerValues { get; set; }

		public List<SurveyDetail> QuestionDetails { get; set; }

		public string SingleResult { get; set; }

		public string SurveyExtend1 { get; set; }

		public string SurveyExtend2 { get; set; }

		public string SurveyExtend3 { get; set; }

		public string SurveyExtend4 { get; set; }

		public string SurveyExtend5 { get; set; }

		public string SurveyExtend6 { get; set; }

		public string SurveyExtend7 { get; set; }

		public string SurveyExtend8 { get; set; }

		public string SurveyExtend9 { get; set; }

		public string SurveyExtend10 { get; set; }

		public string SurveyExtend11 { get; set; }

		public List<string> CircleQuestionList { get; set; }

		public CustomerBiz()
		{
			this.RouteResultValues = new List<string>();
			this.CircleQuestionList = new List<string>();
		}

		public virtual void Init(string string_0)
		{
		}

		public void GetPageRoadMap(string string_0, string string_1)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
		}

		public void PageLoad(string string_0)
		{
		}

		public void SaveToAnswer(string string_0, string string_1, string string_2, int int_0)
		{
			this.oSurveyAnswerDal.AddOne(string_0, string_1, string_2, int_0);
		}

		public string GetOneAnswer(string string_0, string string_1)
		{
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		public string GetOPageAnswer(string string_0, int int_0)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
			string text = "";
			foreach (SurveyAnswer surveyAnswer in list)
			{
				text = text + string.Format(GClass0.smethod_0("颕勻ȧͱйյث籒恍न੸ଳ౼"), surveyAnswer.QUESTION_NAME, surveyAnswer.CODE) + Environment.NewLine;
			}
			return text;
		}

		public bool PageAddOnCheck(string string_0, string string_1, out string string_2)
		{
			string_2 = "";
			string text = "";
			string a = "";
			bool flag = false;
			SurveyDetail surveyDetail = new SurveyDetail();
			bool result = true;
			if (!(string_1 == GClass0.smethod_0("Aķ")))
			{
				if (!(string_1 == GClass0.smethod_0("@Ĳ")))
				{
					if (string_1 == GClass0.smethod_0("@ĵ") && this.SurveyExtend1 == "2")
					{
						text = this.GetOneAnswer(string_0, GClass0.smethod_0("Gıɥ͜僃睍"));
						string codeText = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Dİɜ埃癍"), text);
						a = this.GetOneAnswer(string_0, GClass0.smethod_0("Gıɦ͜僃睍"));
						string codeText2 = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Dİɜ埃癍"), text);
						for (int i = 0; i < this.RouteResultValues.Count; i++)
						{
							surveyDetail = this.oSurveyDetailDal.GetOne(GClass0.smethod_0("@Ĳ"), this.RouteResultValues[i].ToString());
							string extend_ = surveyDetail.EXTEND_1;
							if (text == extend_)
							{
								string_2 = string.Format(GClass0.smethod_0("佶伮讕茐艃玕勑畃࠮थ੷଻౷ഠศ䄊互髠骯茊盫┃"), codeText);
								return false;
							}
							if (a == extend_)
							{
								string_2 = string.Format(GClass0.smethod_0("佶伮讕茐艃玕勑畃࠮थ੷଻౷ഠศ䄊互髠骯茊盫┃"), codeText2);
								return false;
							}
						}
					}
				}
				else if (this.SurveyExtend1 == "2")
				{
					text = this.GetOneAnswer(string_0, GClass0.smethod_0("Vıɜ埃癍"));
					string codeText = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Vıɜ埃癍"), text);
					int extendCount = this.oSurveyDetailDal.GetExtendCount(GClass0.smethod_0("@Ĳ"), text, 1);
					if (extendCount > 0)
					{
						for (int j = 0; j < this.RouteResultValues.Count; j++)
						{
							surveyDetail = this.oSurveyDetailDal.GetOne(GClass0.smethod_0("@Ĳ"), this.RouteResultValues[j].ToString());
							string extend_ = surveyDetail.EXTEND_1;
							if (text == extend_)
							{
								flag = true;
							}
						}
						if (!flag)
						{
							string_2 = string.Format(GClass0.smethod_0("佳脑葀疔僎睂حݷ࠻ॷ਩喜蟢䈜蚮鼍狪䌲∃"), codeText);
							return false;
						}
					}
				}
			}
			else
			{
				text = this.GetOneAnswer(string_0, GClass0.smethod_0("Aİ"));
				string codeText = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Aİ"), text);
				if (Convert.ToInt32(this.SingleResult) < Convert.ToInt32(text))
				{
					string_2 = string.Format(GClass0.smethod_0("RĦ䳸欲媙軩弬䦄獀䞆੄ଷభൿำཿဨ"), codeText);
					return false;
				}
			}
			return result;
		}

		public bool PageAddOnCheckCircle(string string_0, string string_1, string string_2, out string string_3)
		{
			string_3 = "";
			bool result = true;
			SurveyDetail surveyDetail = new SurveyDetail();
			if (!(string_1 == GClass0.smethod_0("@İ")))
			{
				if (string_1 == GClass0.smethod_0("@ĳ") && this.SurveyExtend1 == "2")
				{
					string oneAnswer = this.GetOneAnswer(string_0, GClass0.smethod_0("Vıɜ埃癍"));
					string codeText = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Vıɜ埃癍"), oneAnswer);
					string oneAnswer2 = this.GetOneAnswer(string_0, GClass0.smethod_0("Gıɥ͜僃睍"));
					string codeText2 = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Dİɜ埃癍"), oneAnswer2);
					string oneAnswer3 = this.GetOneAnswer(string_0, GClass0.smethod_0("Gıɦ͜僃睍"));
					string codeText3 = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Dİɜ埃癍"), oneAnswer3);
					for (int i = 0; i < this.RouteResultValues.Count<string>(); i++)
					{
						if (this.RouteResultValues[i].ToString() == "1" || this.RouteResultValues[i].ToString() == "2")
						{
							string_2 = this.CircleQuestionList[i].ToString().Replace(GClass0.smethod_0("Fıɝ͓"), "");
							surveyDetail = this.oSurveyDetailDal.GetOne(GClass0.smethod_0("CŜɀ̳"), string_2);
							string extend_ = surveyDetail.EXTEND_1;
							if (oneAnswer == extend_)
							{
								string_3 = string.Format(GClass0.smethod_0("佴伨讓茒艁王勏畁ࠬ॰਺୴䌊傒蓠帉䒟礠⌃"), codeText);
								return false;
							}
							if (oneAnswer2 == extend_)
							{
								string_3 = string.Format(GClass0.smethod_0("佴伨讓茒艁王勏畁ࠬ॰਺୴䌊傒蓠帉䒟礠⌃"), codeText2);
								return false;
							}
							if (oneAnswer3 == extend_)
							{
								string_3 = string.Format(GClass0.smethod_0("佴伨讓茒艁王勏畁ࠬ॰਺୴䌊傒蓠帉䒟礠⌃"), codeText3);
								return false;
							}
						}
					}
				}
			}
			else
			{
				string oneAnswer = this.GetOneAnswer(string_0, GClass0.smethod_0("Vıɜ埃癍"));
				string codeText = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Vıɜ埃癍"), oneAnswer);
				string oneAnswer2 = this.GetOneAnswer(string_0, GClass0.smethod_0("DĶɜ埃癍"));
				string codeText2 = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Dİɜ埃癍"), oneAnswer2);
				string oneAnswer3 = this.GetOneAnswer(string_0, GClass0.smethod_0("Gıɥ͜僃睍"));
				string codeText3 = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Dİɜ埃癍"), oneAnswer3);
				string oneAnswer4 = this.GetOneAnswer(string_0, GClass0.smethod_0("Gıɦ͜僃睍"));
				string codeText4 = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Dİɜ埃癍"), oneAnswer4);
				surveyDetail = this.oSurveyDetailDal.GetOne(GClass0.smethod_0("CŜɀ̰"), string_2);
				string extend_ = surveyDetail.EXTEND_1;
				if (oneAnswer != GClass0.smethod_0("3İȵ") && (oneAnswer == extend_ && (this.SingleResult == "1" || this.SingleResult == "2")))
				{
					string_3 = string.Format(GClass0.smethod_0("迃朶䵸吿хԠ䠹霚櫻羕廑祃మ൶฼ྲྀ弄䲜飢俿了堉妅釡⤃"), codeText);
					return false;
				}
				if (oneAnswer2 != GClass0.smethod_0("3İȵ") && (oneAnswer2 == extend_ && (this.SingleResult == "1" || this.SingleResult == "2")))
				{
					string_3 = string.Format(GClass0.smethod_0("迃朶䵸吿їԧ䠹霚櫻羕廑祃మ൶฼ྲྀ弄䲜飢俿了堉妅釡⤃"), codeText2);
					return false;
				}
				if (oneAnswer3 != GClass0.smethod_0("3İȵ") && (oneAnswer3 == extend_ && this.SingleResult == "1"))
				{
					string_3 = string.Format(GClass0.smethod_0("迂朵䵹吰іԢ䠸霝櫺羖凨惄壎罂๶༼ၶ射䶜鿢爬䈩鳰韄䊁⨃"), codeText3);
					return false;
				}
				if (oneAnswer4 != GClass0.smethod_0("3İȵ") && (oneAnswer4 == extend_ && this.SingleResult == "1"))
				{
					string_3 = string.Format(GClass0.smethod_0("迂朵䵹吰іԢ䠸霝櫺羖凨惄壎罂๶༼ၶ射䶜鿢爬䈩鳰韄䊁⨃"), codeText4);
					return false;
				}
			}
			return result;
		}

		public bool PageAddOnDeal(string string_0, string string_1, out string string_2)
		{
			string_2 = "";
			bool result = true;
			if (!(string_1 == GClass0.smethod_0("QĴ")))
			{
				if (string_1 == GClass0.smethod_0("Qķ"))
				{
					DateTime t = Convert.ToDateTime(this.RouteResult + "-01");
					DateTime t2 = Convert.ToDateTime(GClass0.smethod_0("8Ĺȹ̴ЫԵضܮ࠰ह"));
					if (t > t2)
					{
						this.oSurveyAnswerDal.AddOne(string_0, GClass0.smethod_0("Uĳɛ曵釶溴"), "2", 0);
					}
					else
					{
						this.oSurveyAnswerDal.AddOne(string_0, GClass0.smethod_0("Uĳɛ曵釶溴"), "1", 0);
					}
				}
			}
			else
			{
				foreach (SurveyDetail surveyDetail in this.QuestionDetails)
				{
					if (surveyDetail.CODE == this.SingleResult)
					{
						this.oSurveyAnswerDal.AddOne(string_0, GClass0.smethod_0("Vıɜ譂竞"), surveyDetail.EXTEND_1, 0);
						this.oSurveyAnswerDal.AddOne(string_0, GClass0.smethod_0("Vıɜ綥嘪"), surveyDetail.EXTEND_2, 0);
						string_2 = this.oSurveyDetailDal.GetCodeText(GClass0.smethod_0("Dİɜ綥嘪"), surveyDetail.EXTEND_2);
					}
				}
			}
			return result;
		}

		public string GetSurveyExtend(string string_0, int int_0)
		{
			string result = "";
			string text = GClass0.smethod_0("^řə͜ьՑ٘݃࡝ॐ੆ୌ౅") + int_0.ToString();
			SurveyDefine byPageId = this.oSurveyDefineDal.GetByPageId(text);
			if (byPageId.PAGE_ID == text && byPageId.QUESTION_TYPE == 88)
			{
				if (byPageId.COMBINE_INDEX == 0)
				{
					string oneCode = this.oSurveyAnswerDal.GetOneCode(string_0, byPageId.QUESTION_NAME);
					result = this.oSurveyDetailDal.GetCodeText(byPageId.DETAIL_ID, oneCode);
				}
				if (byPageId.COMBINE_INDEX == 1)
				{
					result = this.oSurveyAnswerDal.GetOneCode(string_0, byPageId.QUESTION_NAME);
				}
			}
			return result;
		}

		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();
	}
}
