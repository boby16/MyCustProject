using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x0200001A RID: 26
	public class QFill
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00002C4B File Offset: 0x00000E4B
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00002C53 File Offset: 0x00000E53
		public string QuestionName { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00002C5C File Offset: 0x00000E5C
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00002C64 File Offset: 0x00000E64
		public string QuestionTitle { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00002C6D File Offset: 0x00000E6D
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00002C75 File Offset: 0x00000E75
		public SurveyDefine QDefine { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00002C7E File Offset: 0x00000E7E
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00002C86 File Offset: 0x00000E86
		public SurveyDefine QCircleDefine { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00002C8F File Offset: 0x00000E8F
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00002C97 File Offset: 0x00000E97
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00002CA0 File Offset: 0x00000EA0
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public List<SurveyDetail> QCircleDetails { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00002CB1 File Offset: 0x00000EB1
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00002CB9 File Offset: 0x00000EB9
		public string FillText { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00002CC2 File Offset: 0x00000EC2
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00002CCA File Offset: 0x00000ECA
		public SurveyAnswer OneAnswer { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00002CD3 File Offset: 0x00000ED3
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00002CDB File Offset: 0x00000EDB
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00002CE4 File Offset: 0x00000EE4
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00002CEC File Offset: 0x00000EEC
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00002CF5 File Offset: 0x00000EF5
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00002CFD File Offset: 0x00000EFD
		private DateTime QInitDateTime { get; set; }

		// Token: 0x060002AE RID: 686 RVA: 0x000205AC File Offset: 0x0001E7AC
		public virtual void Init(string string_0, int int_0)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.QuestionTitle = this.QDefine.QUESTION_TITLE;
			this.QInitDateTime = DateTime.Now;
			string text = global::GClass0.smethod_0("");
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out text);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0002064C File Offset: 0x0001E84C
		public void InitCircle()
		{
			this.QChildQuestion = ((this.QDefine.GROUP_LEVEL == global::GClass0.smethod_0("C")) ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.QCircleDefine = this.oSurveyDefineDal.GetByName(this.QChildQuestion);
			if (this.QCircleDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000024F1 File Offset: 0x000006F1
		public void BeforeSave()
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000206E4 File Offset: 0x0001E8E4
		public void Save(string string_0, int int_0)
		{
			this.OneAnswer = new SurveyAnswer
			{
				SURVEY_ID = string_0,
				SEQUENCE_ID = int_0,
				QUESTION_NAME = this.QuestionName,
				CODE = this.FillText,
				MULTI_ORDER = 0,
				BEGIN_DATE = new DateTime?(this.QInitDateTime),
				MODIFY_DATE = new DateTime?(DateTime.Now)
			};
			this.oSurveyAnswerDal.SaveOneAnswer(this.OneAnswer);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00002D3F File Offset: 0x00000F3F
		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00020760 File Offset: 0x0001E960
		public string ReadAnswerByQuestionName(string string_0, string string_1)
		{
			string text = global::GClass0.smethod_0("");
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00020790 File Offset: 0x0001E990
		public void Save(string string_0, int int_0, string string_1, string string_2, DateTime dateTime_0)
		{
			this.OneAnswer = new SurveyAnswer
			{
				SURVEY_ID = string_0,
				SEQUENCE_ID = int_0,
				QUESTION_NAME = string_1,
				CODE = string_2,
				MULTI_ORDER = 0,
				BEGIN_DATE = new DateTime?(dateTime_0),
				MODIFY_DATE = new DateTime?(DateTime.Now)
			};
			this.oSurveyAnswerDal.SaveOneAnswer(this.OneAnswer);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000207FC File Offset: 0x0001E9FC
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

		// Token: 0x060002B6 RID: 694 RVA: 0x00020240 File Offset: 0x0001E440
		public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
		{
			if (list_0.Count > 1)
			{
				RandomEngine randomEngine = new RandomEngine();
				list_0 = randomEngine.RandomDetails(list_0);
			}
			return list_0;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00002D54 File Offset: 0x00000F54
		public void RandomDetails()
		{
			this.QDetails = this.RandomDetails(this.QDetails);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000CE34 File Offset: 0x0000B034
		private string method_0(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x060002B9 RID: 697 RVA: 0x00007124 File Offset: 0x00005324
		private string method_1(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00007158 File Offset: 0x00005358
		private string method_2(string string_0, int int_0, int int_1 = -9999)
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

		// Token: 0x060002BB RID: 699 RVA: 0x000071B4 File Offset: 0x000053B4
		private string method_3(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001C028 File Offset: 0x0001A228
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

		// Token: 0x04000126 RID: 294
		private string QChildQuestion = global::GClass0.smethod_0("");

		// Token: 0x0400012C RID: 300
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x0400012D RID: 301
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x0400012E RID: 302
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();
	}
}
