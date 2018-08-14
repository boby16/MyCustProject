using System;
using System.Collections.Generic;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x0200001B RID: 27
	public class QMatrixFill
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00002D68 File Offset: 0x00000F68
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00002D70 File Offset: 0x00000F70
		public SurveyDefine QDefine { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00002D79 File Offset: 0x00000F79
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00002D81 File Offset: 0x00000F81
		public SurveyDefine QCircleDefine { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00002D8A File Offset: 0x00000F8A
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00002D92 File Offset: 0x00000F92
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00002D9B File Offset: 0x00000F9B
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00002DA3 File Offset: 0x00000FA3
		public List<SurveyDetail> QCircleDetails { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00002DAC File Offset: 0x00000FAC
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00002DBD File Offset: 0x00000FBD
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00002DC5 File Offset: 0x00000FC5
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00002DCE File Offset: 0x00000FCE
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00002DD6 File Offset: 0x00000FD6
		public DateTime QInitDateTime { get; set; }

		// Token: 0x060002CC RID: 716 RVA: 0x00020914 File Offset: 0x0001EB14
		public void Init(string string_0, int int_0)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.CircleQuestionName = ((this.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("@")) ? this.QDefine.GROUP_CODEA : this.QDefine.GROUP_CODEB);
			this.QInitDateTime = DateTime.Now;
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
			}
			this.QChildQuestion = ((this.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C")) ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.QCircleDefine = this.oSurveyDefineDal.GetByName(this.QChildQuestion);
			if (this.QCircleDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00020A58 File Offset: 0x0001EC58
		public void BeforeSave(int int_0 = 1)
		{
			if (int_0 == 1)
			{
				this.QAnswers = this.method_0();
			}
			else if (int_0 == 2)
			{
				this.QAnswers = this.method_1();
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00020A8C File Offset: 0x0001EC8C
		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			DateTime now = DateTime.Now;
			int num = 0;
			foreach (string text in this.FillTexts)
			{
				string question_NAME = this.QuestionName + global::GClass0.smethod_0("]ł") + this.QCircleDetails[num].CODE;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = text,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				dictionary.Add(this.QCircleDetails[num].CODE, this.oFunc.StringToDouble(text));
				question_NAME = this.CircleQuestionName + global::GClass0.smethod_0("]œ") + (num + 1).ToString();
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
			Dictionary<string, string> dictionary2 = this.oFunc.SortDictSDbyValue(dictionary, true, false, global::GClass0.smethod_0("-"));
			num = 1;
			foreach (string key in dictionary2.Keys)
			{
				string question_NAME = this.QuestionName + global::GClass0.smethod_0("]œ") + num.ToString();
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = dictionary2[key],
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				num++;
			}
			return list;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00020CF8 File Offset: 0x0001EEF8
		private List<SurveyAnswer> method_1()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			DateTime now = DateTime.Now;
			int num = 0;
			foreach (string text in this.FillTexts)
			{
				string question_NAME = this.QuestionName + global::GClass0.smethod_0("]œ") + this.QCircleDetails[num].CODE;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = text,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				dictionary.Add(this.QCircleDetails[num].CODE, this.oFunc.StringToDouble(text));
				question_NAME = this.CircleQuestionName + global::GClass0.smethod_0("]œ") + (num + 1).ToString();
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

		// Token: 0x060002D0 RID: 720 RVA: 0x00002DDF File Offset: 0x00000FDF
		public void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00002E01 File Offset: 0x00001001
		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00020E88 File Offset: 0x0001F088
		public string ReadAnswerByQuestionName(string string_0, string string_1)
		{
			string text = global::GClass0.smethod_0("");
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00020240 File Offset: 0x0001E440
		public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
		{
			if (list_0.Count > 1)
			{
				RandomEngine randomEngine = new RandomEngine();
				list_0 = randomEngine.RandomDetails(list_0);
			}
			return list_0;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00002E16 File Offset: 0x00001016
		public void RandomCircleDetails()
		{
			this.QCircleDetails = this.RandomDetails(this.QCircleDetails);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		public string ConvertText(string string_0, string string_1 = "")
		{
			string text = string_0;
			if (string_1 == global::GClass0.smethod_0("0"))
			{
				text = this.CleanString(text).ToUpper();
			}
			else if (string_1 == global::GClass0.smethod_0("3"))
			{
				text = this.CleanString(text).ToLower();
			}
			else if (string_1 == global::GClass0.smethod_0("2"))
			{
				text = this.CleanString(text);
			}
			else if (string_1 == global::GClass0.smethod_0("5"))
			{
				text = text.Trim();
			}
			return text;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000CE34 File Offset: 0x0000B034
		private string method_2(string string_0, int int_0, int int_1 = -9999)
		{
			int num = int_1;
			if (num == -9999)
			{
				num = int_0;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (int_0 < 0) ? 0 : int_0;
			int num3 = (num2 < num) ? num2 : num;
			int num4 = (num2 < num) ? num : num2;
			int num5 = (num2 > string_0.Length) ? string_0.Length : num2;
			num = ((int_1 > string_0.Length) ? (string_0.Length - 1) : int_1);
			return string_0.Substring(num5, num - num5 + 1);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00007124 File Offset: 0x00005324
		private string method_3(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00007158 File Offset: 0x00005358
		private string method_4(string string_0, int int_0, int int_1 = -9999)
		{
			int num = int_1;
			if (num == -9999)
			{
				num = string_0.Length;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = (int_0 > string_0.Length) ? string_0.Length : int_0;
			return string_0.Substring(num2, (num2 + num > string_0.Length) ? (string_0.Length - num2) : num);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000071B4 File Offset: 0x000053B4
		private string method_5(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001C028 File Offset: 0x0001A228
		public string CleanString(string string_0)
		{
			string text = string_0.Replace(global::GClass0.smethod_0("、"), global::GClass0.smethod_0("!"));
			text = text.Trim();
			text = text.Replace('\n', ' ');
			text = text.Replace('\r', ' ');
			text = text.Replace('\t', ' ');
			text = text.Replace(global::GClass0.smethod_0("Ａ"), global::GClass0.smethod_0("A"));
			text = text.Replace(global::GClass0.smethod_0("１"), global::GClass0.smethod_0("1"));
			text = text.Replace(global::GClass0.smethod_0("０"), global::GClass0.smethod_0("0"));
			text = text.Replace(global::GClass0.smethod_0("３"), global::GClass0.smethod_0("3"));
			text = text.Replace(global::GClass0.smethod_0("２"), global::GClass0.smethod_0("2"));
			text = text.Replace(global::GClass0.smethod_0("５"), global::GClass0.smethod_0("5"));
			text = text.Replace(global::GClass0.smethod_0("４"), global::GClass0.smethod_0("4"));
			text = text.Replace(global::GClass0.smethod_0("７"), global::GClass0.smethod_0("7"));
			text = text.Replace(global::GClass0.smethod_0("６"), global::GClass0.smethod_0("6"));
			text = text.Replace(global::GClass0.smethod_0("９"), global::GClass0.smethod_0("9"));
			text = text.Replace(global::GClass0.smethod_0("８"), global::GClass0.smethod_0("8"));
			text = text.Replace(global::GClass0.smethod_0("＠"), global::GClass0.smethod_0("@"));
			text = text.Replace(global::GClass0.smethod_0("Ｃ"), global::GClass0.smethod_0("C"));
			text = text.Replace(global::GClass0.smethod_0("Ｂ"), global::GClass0.smethod_0("B"));
			text = text.Replace(global::GClass0.smethod_0("Ｅ"), global::GClass0.smethod_0("E"));
			text = text.Replace(global::GClass0.smethod_0("Ｄ"), global::GClass0.smethod_0("D"));
			text = text.Replace(global::GClass0.smethod_0("Ｇ"), global::GClass0.smethod_0("G"));
			text = text.Replace(global::GClass0.smethod_0("Ｆ"), global::GClass0.smethod_0("F"));
			text = text.Replace(global::GClass0.smethod_0("Ｉ"), global::GClass0.smethod_0("I"));
			text = text.Replace(global::GClass0.smethod_0("Ｈ"), global::GClass0.smethod_0("H"));
			text = text.Replace(global::GClass0.smethod_0("Ｋ"), global::GClass0.smethod_0("K"));
			text = text.Replace(global::GClass0.smethod_0("Ｊ"), global::GClass0.smethod_0("J"));
			text = text.Replace(global::GClass0.smethod_0("Ｍ"), global::GClass0.smethod_0("M"));
			text = text.Replace(global::GClass0.smethod_0("Ｌ"), global::GClass0.smethod_0("L"));
			text = text.Replace(global::GClass0.smethod_0("Ｏ"), global::GClass0.smethod_0("O"));
			text = text.Replace(global::GClass0.smethod_0("Ｎ"), global::GClass0.smethod_0("N"));
			text = text.Replace(global::GClass0.smethod_0("Ｑ"), global::GClass0.smethod_0("Q"));
			text = text.Replace(global::GClass0.smethod_0("Ｐ"), global::GClass0.smethod_0("P"));
			text = text.Replace(global::GClass0.smethod_0("Ｓ"), global::GClass0.smethod_0("S"));
			text = text.Replace(global::GClass0.smethod_0("Ｒ"), global::GClass0.smethod_0("R"));
			text = text.Replace(global::GClass0.smethod_0("Ｕ"), global::GClass0.smethod_0("U"));
			text = text.Replace(global::GClass0.smethod_0("Ｔ"), global::GClass0.smethod_0("T"));
			text = text.Replace(global::GClass0.smethod_0("Ｗ"), global::GClass0.smethod_0("W"));
			text = text.Replace(global::GClass0.smethod_0("Ｖ"), global::GClass0.smethod_0("V"));
			text = text.Replace(global::GClass0.smethod_0("Ｙ"), global::GClass0.smethod_0("Y"));
			text = text.Replace(global::GClass0.smethod_0("Ｘ"), global::GClass0.smethod_0("X"));
			text = text.Replace(global::GClass0.smethod_0("［"), global::GClass0.smethod_0("["));
			text = text.Replace(global::GClass0.smethod_0("｀"), global::GClass0.smethod_0("`"));
			text = text.Replace(global::GClass0.smethod_0("ｃ"), global::GClass0.smethod_0("c"));
			text = text.Replace(global::GClass0.smethod_0("ｂ"), global::GClass0.smethod_0("b"));
			text = text.Replace(global::GClass0.smethod_0("ｅ"), global::GClass0.smethod_0("e"));
			text = text.Replace(global::GClass0.smethod_0("ｄ"), global::GClass0.smethod_0("d"));
			text = text.Replace(global::GClass0.smethod_0("ｇ"), global::GClass0.smethod_0("g"));
			text = text.Replace(global::GClass0.smethod_0("ｆ"), global::GClass0.smethod_0("f"));
			text = text.Replace(global::GClass0.smethod_0("ｉ"), global::GClass0.smethod_0("i"));
			text = text.Replace(global::GClass0.smethod_0("ｈ"), global::GClass0.smethod_0("h"));
			text = text.Replace(global::GClass0.smethod_0("ｋ"), global::GClass0.smethod_0("k"));
			text = text.Replace(global::GClass0.smethod_0("ｊ"), global::GClass0.smethod_0("j"));
			text = text.Replace(global::GClass0.smethod_0("ｍ"), global::GClass0.smethod_0("m"));
			text = text.Replace(global::GClass0.smethod_0("ｌ"), global::GClass0.smethod_0("l"));
			text = text.Replace(global::GClass0.smethod_0("ｏ"), global::GClass0.smethod_0("o"));
			text = text.Replace(global::GClass0.smethod_0("ｎ"), global::GClass0.smethod_0("n"));
			text = text.Replace(global::GClass0.smethod_0("ｑ"), global::GClass0.smethod_0("q"));
			text = text.Replace(global::GClass0.smethod_0("ｐ"), global::GClass0.smethod_0("p"));
			text = text.Replace(global::GClass0.smethod_0("ｓ"), global::GClass0.smethod_0("s"));
			text = text.Replace(global::GClass0.smethod_0("ｒ"), global::GClass0.smethod_0("r"));
			text = text.Replace(global::GClass0.smethod_0("ｕ"), global::GClass0.smethod_0("u"));
			text = text.Replace(global::GClass0.smethod_0("ｔ"), global::GClass0.smethod_0("t"));
			text = text.Replace(global::GClass0.smethod_0("ｗ"), global::GClass0.smethod_0("w"));
			text = text.Replace(global::GClass0.smethod_0("ｖ"), global::GClass0.smethod_0("v"));
			text = text.Replace(global::GClass0.smethod_0("ｙ"), global::GClass0.smethod_0("y"));
			text = text.Replace(global::GClass0.smethod_0("ｘ"), global::GClass0.smethod_0("x"));
			return text.Replace(global::GClass0.smethod_0("｛"), global::GClass0.smethod_0("{"));
		}

		// Token: 0x0400012F RID: 303
		public string QuestionName = global::GClass0.smethod_0("");

		// Token: 0x04000130 RID: 304
		public string CircleQuestionName = global::GClass0.smethod_0("");

		// Token: 0x04000131 RID: 305
		public string ParentCode = global::GClass0.smethod_0("");

		// Token: 0x04000134 RID: 308
		private string QChildQuestion = global::GClass0.smethod_0("");

		// Token: 0x04000137 RID: 311
		public List<string> FillTexts = new List<string>();

		// Token: 0x0400013B RID: 315
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x0400013C RID: 316
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x0400013D RID: 317
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x0400013E RID: 318
		private UDPX oFunc = new UDPX();
	}
}
