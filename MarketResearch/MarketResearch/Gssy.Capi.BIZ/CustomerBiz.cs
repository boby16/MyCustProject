using System;
using System.Collections.Generic;
using System.Linq;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x0200000F RID: 15
	public class CustomerBiz
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000023BF File Offset: 0x000005BF
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000023C7 File Offset: 0x000005C7
		public SurveyRoadMap RoadMap { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000023D0 File Offset: 0x000005D0
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000023D8 File Offset: 0x000005D8
		public string RouteResult { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000023E1 File Offset: 0x000005E1
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000023E9 File Offset: 0x000005E9
		public List<string> RouteResultValues { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000023F2 File Offset: 0x000005F2
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000023FA File Offset: 0x000005FA
		public List<SurveyAnswer> AnswerValues { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002403 File Offset: 0x00000603
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000240B File Offset: 0x0000060B
		public List<SurveyDetail> QuestionDetails { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002414 File Offset: 0x00000614
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000241C File Offset: 0x0000061C
		public string SingleResult { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000242D File Offset: 0x0000062D
		public string SurveyExtend1 { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002436 File Offset: 0x00000636
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000243E File Offset: 0x0000063E
		public string SurveyExtend2 { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002447 File Offset: 0x00000647
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000244F File Offset: 0x0000064F
		public string SurveyExtend3 { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002458 File Offset: 0x00000658
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002460 File Offset: 0x00000660
		public string SurveyExtend4 { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002469 File Offset: 0x00000669
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002471 File Offset: 0x00000671
		public string SurveyExtend5 { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000247A File Offset: 0x0000067A
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00002482 File Offset: 0x00000682
		public string SurveyExtend6 { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000248B File Offset: 0x0000068B
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00002493 File Offset: 0x00000693
		public string SurveyExtend7 { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000249C File Offset: 0x0000069C
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000024A4 File Offset: 0x000006A4
		public string SurveyExtend8 { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000024AD File Offset: 0x000006AD
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000024B5 File Offset: 0x000006B5
		public string SurveyExtend9 { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000024BE File Offset: 0x000006BE
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000024C6 File Offset: 0x000006C6
		public string SurveyExtend10 { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000024CF File Offset: 0x000006CF
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000024D7 File Offset: 0x000006D7
		public string SurveyExtend11 { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000024E0 File Offset: 0x000006E0
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000024E8 File Offset: 0x000006E8
		public List<string> CircleQuestionList { get; set; }

		// Token: 0x060000B4 RID: 180 RVA: 0x000075A0 File Offset: 0x000057A0
		public CustomerBiz()
		{
			this.RouteResultValues = new List<string>();
			this.CircleQuestionList = new List<string>();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000024F1 File Offset: 0x000006F1
		public virtual void Init(string string_0)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000024F3 File Offset: 0x000006F3
		public void GetPageRoadMap(string string_0, string string_1)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000024F1 File Offset: 0x000006F1
		public void PageLoad(string string_0)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002508 File Offset: 0x00000708
		public void SaveToAnswer(string string_0, string string_1, string string_2, int int_0)
		{
			this.oSurveyAnswerDal.AddOne(string_0, string_1, string_2, int_0);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000760C File Offset: 0x0000580C
		public string GetOneAnswer(string string_0, string string_1)
		{
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007630 File Offset: 0x00005830
		public string GetOPageAnswer(string string_0, int int_0)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
			string text = global::GClass0.smethod_0("");
			foreach (SurveyAnswer surveyAnswer in list)
			{
				text = text + string.Format(global::GClass0.smethod_0("颕勻ȧͱйյث籒恍न੸ଳ౼"), surveyAnswer.QUESTION_NAME, surveyAnswer.CODE) + Environment.NewLine;
			}
			return text;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000076C4 File Offset: 0x000058C4
		public bool PageAddOnCheck(string string_0, string string_1, out string string_2)
		{
			string_2 = global::GClass0.smethod_0("");
			string text = global::GClass0.smethod_0("");
			string a = global::GClass0.smethod_0("");
			bool flag = false;
			SurveyDetail surveyDetail = new SurveyDetail();
			bool result = true;
			if (!(string_1 == global::GClass0.smethod_0("Aķ")))
			{
				if (!(string_1 == global::GClass0.smethod_0("@Ĳ")))
				{
					if (string_1 == global::GClass0.smethod_0("@ĵ") && this.SurveyExtend1 == global::GClass0.smethod_0("3"))
					{
						text = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Gıɥ͜僃睍"));
						string codeText = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Dİɜ埃癍"), text);
						a = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Gıɦ͜僃睍"));
						string codeText2 = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Dİɜ埃癍"), text);
						for (int i = 0; i < this.RouteResultValues.Count; i++)
						{
							surveyDetail = this.oSurveyDetailDal.GetOne(global::GClass0.smethod_0("@Ĳ"), this.RouteResultValues[i].ToString());
							string extend_ = surveyDetail.EXTEND_1;
							if (text == extend_)
							{
								string_2 = string.Format(global::GClass0.smethod_0("佶伮讕茐艃玕勑畃࠮थ੷଻౷ഠศ䄊互髠骯茊盫┃"), codeText);
								return false;
							}
							if (a == extend_)
							{
								string_2 = string.Format(global::GClass0.smethod_0("佶伮讕茐艃玕勑畃࠮थ੷଻౷ഠศ䄊互髠骯茊盫┃"), codeText2);
								return false;
							}
						}
					}
				}
				else if (this.SurveyExtend1 == global::GClass0.smethod_0("3"))
				{
					text = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Vıɜ埃癍"));
					string codeText = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Vıɜ埃癍"), text);
					int extendCount = this.oSurveyDetailDal.GetExtendCount(global::GClass0.smethod_0("@Ĳ"), text, 1);
					if (extendCount > 0)
					{
						for (int j = 0; j < this.RouteResultValues.Count; j++)
						{
							surveyDetail = this.oSurveyDetailDal.GetOne(global::GClass0.smethod_0("@Ĳ"), this.RouteResultValues[j].ToString());
							string extend_ = surveyDetail.EXTEND_1;
							if (text == extend_)
							{
								flag = true;
							}
						}
						if (!flag)
						{
							string_2 = string.Format(global::GClass0.smethod_0("佳脑葀疔僎睂حݷ࠻ॷ਩喜蟢䈜蚮鼍狪䌲∃"), codeText);
							return false;
						}
					}
				}
			}
			else
			{
				text = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Aİ"));
				string codeText = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Aİ"), text);
				if (Convert.ToInt32(this.SingleResult) < Convert.ToInt32(text))
				{
					string_2 = string.Format(global::GClass0.smethod_0("RĦ䳸欲媙軩弬䦄獀䞆੄ଷభൿำཿဨ"), codeText);
					return false;
				}
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007988 File Offset: 0x00005B88
		public bool PageAddOnCheckCircle(string string_0, string string_1, string string_2, out string string_3)
		{
			string_3 = global::GClass0.smethod_0("");
			bool result = true;
			SurveyDetail surveyDetail = new SurveyDetail();
			if (!(string_1 == global::GClass0.smethod_0("@İ")))
			{
				if (string_1 == global::GClass0.smethod_0("@ĳ") && this.SurveyExtend1 == global::GClass0.smethod_0("3"))
				{
					string oneAnswer = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Vıɜ埃癍"));
					string codeText = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Vıɜ埃癍"), oneAnswer);
					string oneAnswer2 = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Gıɥ͜僃睍"));
					string codeText2 = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Dİɜ埃癍"), oneAnswer2);
					string oneAnswer3 = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Gıɦ͜僃睍"));
					string codeText3 = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Dİɜ埃癍"), oneAnswer3);
					for (int i = 0; i < this.RouteResultValues.Count<string>(); i++)
					{
						if (this.RouteResultValues[i].ToString() == global::GClass0.smethod_0("0") || this.RouteResultValues[i].ToString() == global::GClass0.smethod_0("3"))
						{
							string_2 = this.CircleQuestionList[i].ToString().Replace(global::GClass0.smethod_0("Fıɝ͓"), global::GClass0.smethod_0(""));
							surveyDetail = this.oSurveyDetailDal.GetOne(global::GClass0.smethod_0("CŜɀ̳"), string_2);
							string extend_ = surveyDetail.EXTEND_1;
							if (oneAnswer == extend_)
							{
								string_3 = string.Format(global::GClass0.smethod_0("佴伨讓茒艁王勏畁ࠬ॰਺୴䌊傒蓠帉䒟礠⌃"), codeText);
								return false;
							}
							if (oneAnswer2 == extend_)
							{
								string_3 = string.Format(global::GClass0.smethod_0("佴伨讓茒艁王勏畁ࠬ॰਺୴䌊傒蓠帉䒟礠⌃"), codeText2);
								return false;
							}
							if (oneAnswer3 == extend_)
							{
								string_3 = string.Format(global::GClass0.smethod_0("佴伨讓茒艁王勏畁ࠬ॰਺୴䌊傒蓠帉䒟礠⌃"), codeText3);
								return false;
							}
						}
					}
				}
			}
			else
			{
				string oneAnswer = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Vıɜ埃癍"));
				string codeText = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Vıɜ埃癍"), oneAnswer);
				string oneAnswer2 = this.GetOneAnswer(string_0, global::GClass0.smethod_0("DĶɜ埃癍"));
				string codeText2 = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Dİɜ埃癍"), oneAnswer2);
				string oneAnswer3 = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Gıɥ͜僃睍"));
				string codeText3 = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Dİɜ埃癍"), oneAnswer3);
				string oneAnswer4 = this.GetOneAnswer(string_0, global::GClass0.smethod_0("Gıɦ͜僃睍"));
				string codeText4 = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Dİɜ埃癍"), oneAnswer4);
				surveyDetail = this.oSurveyDetailDal.GetOne(global::GClass0.smethod_0("CŜɀ̰"), string_2);
				string extend_ = surveyDetail.EXTEND_1;
				if (oneAnswer != global::GClass0.smethod_0("3İȵ") && (oneAnswer == extend_ && (this.SingleResult == global::GClass0.smethod_0("0") || this.SingleResult == global::GClass0.smethod_0("3"))))
				{
					string_3 = string.Format(global::GClass0.smethod_0("迃朶䵸吿хԠ䠹霚櫻羕廑祃మ൶฼ྲྀ弄䲜飢俿了堉妅釡⤃"), codeText);
					return false;
				}
				if (oneAnswer2 != global::GClass0.smethod_0("3İȵ") && (oneAnswer2 == extend_ && (this.SingleResult == global::GClass0.smethod_0("0") || this.SingleResult == global::GClass0.smethod_0("3"))))
				{
					string_3 = string.Format(global::GClass0.smethod_0("迃朶䵸吿їԧ䠹霚櫻羕廑祃మ൶฼ྲྀ弄䲜飢俿了堉妅釡⤃"), codeText2);
					return false;
				}
				if (oneAnswer3 != global::GClass0.smethod_0("3İȵ") && (oneAnswer3 == extend_ && this.SingleResult == global::GClass0.smethod_0("0")))
				{
					string_3 = string.Format(global::GClass0.smethod_0("迂朵䵹吰іԢ䠸霝櫺羖凨惄壎罂๶༼ၶ射䶜鿢爬䈩鳰韄䊁⨃"), codeText3);
					return false;
				}
				if (oneAnswer4 != global::GClass0.smethod_0("3İȵ") && (oneAnswer4 == extend_ && this.SingleResult == global::GClass0.smethod_0("0")))
				{
					string_3 = string.Format(global::GClass0.smethod_0("迂朵䵹吰іԢ䠸霝櫺羖凨惄壎罂๶༼ၶ射䶜鿢爬䈩鳰韄䊁⨃"), codeText4);
					return false;
				}
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public string PageAddOnNav(string string_0, string string_1)
		{
			string result = global::GClass0.smethod_0("");
			global::GClass0.smethod_0("");
			global::GClass0.smethod_0("");
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007E24 File Offset: 0x00006024
		public string PageAddOnNavCircle(string string_0, string string_1)
		{
			string result = global::GClass0.smethod_0("");
			global::GClass0.smethod_0("");
			global::GClass0.smethod_0("");
			global::GClass0.smethod_0("");
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007E64 File Offset: 0x00006064
		public string PageAddOnNavCircleGroup(string string_0, string string_1)
		{
			string result = global::GClass0.smethod_0("");
			global::GClass0.smethod_0("");
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007E8C File Offset: 0x0000608C
		public bool PageAddOnDeal(string string_0, string string_1, out string string_2)
		{
			string_2 = global::GClass0.smethod_0("");
			bool result = true;
			if (!(string_1 == global::GClass0.smethod_0("QĴ")))
			{
				if (string_1 == global::GClass0.smethod_0("Qķ"))
				{
					DateTime t = Convert.ToDateTime(this.RouteResult + global::GClass0.smethod_0(".ĲȰ"));
					DateTime t2 = Convert.ToDateTime(global::GClass0.smethod_0("8Ĺȹ̴ЫԵضܮ࠰ह"));
					if (t > t2)
					{
						this.oSurveyAnswerDal.AddOne(string_0, global::GClass0.smethod_0("Uĳɛ曵釶溴"), global::GClass0.smethod_0("3"), 0);
					}
					else
					{
						this.oSurveyAnswerDal.AddOne(string_0, global::GClass0.smethod_0("Uĳɛ曵釶溴"), global::GClass0.smethod_0("0"), 0);
					}
				}
			}
			else
			{
				foreach (SurveyDetail surveyDetail in this.QuestionDetails)
				{
					if (surveyDetail.CODE == this.SingleResult)
					{
						this.oSurveyAnswerDal.AddOne(string_0, global::GClass0.smethod_0("Vıɜ譂竞"), surveyDetail.EXTEND_1, 0);
						this.oSurveyAnswerDal.AddOne(string_0, global::GClass0.smethod_0("Vıɜ綥嘪"), surveyDetail.EXTEND_2, 0);
						string_2 = this.oSurveyDetailDal.GetCodeText(global::GClass0.smethod_0("Dİɜ綥嘪"), surveyDetail.EXTEND_2);
					}
				}
			}
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00008004 File Offset: 0x00006204
		public string GetSurveyExtend(string string_0, int int_0)
		{
			string result = global::GClass0.smethod_0("");
			string text = global::GClass0.smethod_0("^řə͜ьՑ٘݃࡝ॐ੆ୌ౅") + int_0.ToString();
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

		// Token: 0x04000053 RID: 83
		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		// Token: 0x04000054 RID: 84
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x04000055 RID: 85
		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		// Token: 0x04000056 RID: 86
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x04000057 RID: 87
		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		// Token: 0x04000058 RID: 88
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();
	}
}
