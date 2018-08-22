using System;
using System.Collections.Generic;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class QMatrixFill
	{
		public SurveyDefine QDefine { get; set; }

		public SurveyDefine QCircleDefine { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public List<SurveyDetail> QCircleDetails { get; set; }

		public List<SurveyAnswer> QAnswers { get; set; }

		public List<SurveyAnswer> QAnswersRead { get; set; }

		public DateTime QInitDateTime { get; set; }

		public void Init(string string_0, int int_0)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
			this.CircleQuestionName = ((this.QDefine.GROUP_LEVEL == "A") ? this.QDefine.GROUP_CODEA : this.QDefine.GROUP_CODEB);
			this.QInitDateTime = DateTime.Now;
			if (this.QDefine.DETAIL_ID != "")
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID);
			}
			this.QChildQuestion = ((this.QDefine.GROUP_LEVEL == "B") ? this.QDefine.GROUP_CODEB : this.QDefine.GROUP_CODEA);
			this.QCircleDefine = this.oSurveyDefineDal.GetByName(this.QChildQuestion);
			if (this.QCircleDefine.DETAIL_ID != "")
			{
				this.QCircleDetails = this.oSurveyDetailDal.GetDetails(this.QCircleDefine.DETAIL_ID);
			}
		}

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

		private List<SurveyAnswer> method_0()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			DateTime now = DateTime.Now;
			int num = 0;
			foreach (string text in this.FillTexts)
			{
				string question_NAME = this.QuestionName + "_C" + this.QCircleDetails[num].CODE;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = text,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				dictionary.Add(this.QCircleDetails[num].CODE, this.oFunc.StringToDouble(text));
				question_NAME = this.CircleQuestionName + "_R" + (num + 1).ToString();
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
			Dictionary<string, string> dictionary2 = this.oFunc.SortDictSDbyValue(dictionary, true, false, ",");
			num = 1;
			foreach (string key in dictionary2.Keys)
			{
				string question_NAME = this.QuestionName + "_R" + num.ToString();
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

		private List<SurveyAnswer> method_1()
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			DateTime now = DateTime.Now;
			int num = 0;
			foreach (string text in this.FillTexts)
			{
				string question_NAME = this.QuestionName + "_R" + this.QCircleDetails[num].CODE;
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = question_NAME,
					CODE = text,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(now)
				});
				dictionary.Add(this.QCircleDetails[num].CODE, this.oFunc.StringToDouble(text));
				question_NAME = this.CircleQuestionName + "_R" + (num + 1).ToString();
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

		public void RandomCircleDetails()
		{
			this.QCircleDetails = this.RandomDetails(this.QCircleDetails);
		}

		public string ConvertText(string string_0, string string_1 = "")
		{
			string text = string_0;
			if (string_1 == "1")
			{
				text = this.CleanString(text).ToUpper();
			}
			else if (string_1 == "2")
			{
				text = this.CleanString(text).ToLower();
			}
			else if (string_1 == "3")
			{
				text = this.CleanString(text);
			}
			else if (string_1 == "4")
			{
				text = text.Trim();
			}
			return text;
		}

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

		private string method_3(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

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

		private string method_5(string string_0, int int_0 = 1)
		{
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
		}

		public string CleanString(string string_0)
		{
			string text = string_0.Replace("　", " ");
			text = text.Trim();
			text = text.Replace('\n', ' ');
			text = text.Replace('\r', ' ');
			text = text.Replace('\t', ' ');
			text = text.Replace("＠", "@");
			text = text.Replace("０", "0");
			text = text.Replace("１", "1");
			text = text.Replace("２", "2");
			text = text.Replace("３", "3");
			text = text.Replace("４", "4");
			text = text.Replace("５", "5");
			text = text.Replace("６", "6");
			text = text.Replace("７", "7");
			text = text.Replace("８", "8");
			text = text.Replace("９", "9");
			text = text.Replace("Ａ", "A");
			text = text.Replace("Ｂ", "B");
			text = text.Replace("Ｃ", "C");
			text = text.Replace("Ｄ", "D");
			text = text.Replace("Ｅ", "E");
			text = text.Replace("Ｆ", "F");
			text = text.Replace("Ｇ", "G");
			text = text.Replace("Ｈ", "H");
			text = text.Replace("Ｉ", "I");
			text = text.Replace("Ｊ", "J");
			text = text.Replace("Ｋ", "K");
			text = text.Replace("Ｌ", "L");
			text = text.Replace("Ｍ", "M");
			text = text.Replace("Ｎ", "N");
			text = text.Replace("Ｏ", "O");
			text = text.Replace("Ｐ", "P");
			text = text.Replace("Ｑ", "Q");
			text = text.Replace("Ｒ", "R");
			text = text.Replace("Ｓ", "S");
			text = text.Replace("Ｔ", "T");
			text = text.Replace("Ｕ", "U");
			text = text.Replace("Ｖ", "V");
			text = text.Replace("Ｗ", "W");
			text = text.Replace("Ｘ", "X");
			text = text.Replace("Ｙ", "Y");
			text = text.Replace("Ｚ", "Z");
			text = text.Replace("ａ", "a");
			text = text.Replace("Ｂ", "B");
			text = text.Replace("Ｃ", "C");
			text = text.Replace("Ｄ", "D");
			text = text.Replace("Ｅ", "E");
			text = text.Replace("Ｆ", "F");
			text = text.Replace("Ｇ", "G");
			text = text.Replace("Ｈ", "H");
			text = text.Replace("Ｉ", "I");
			text = text.Replace("Ｊ", "J");
			text = text.Replace("Ｋ", "K");
			text = text.Replace("Ｌ", "L");
			text = text.Replace("Ｍ", "M");
			text = text.Replace("Ｎ", "N");
			text = text.Replace("Ｏ", "O");
			text = text.Replace("Ｐ", "P");
			text = text.Replace("Ｑ", "Q");
			text = text.Replace("Ｒ", "R");
			text = text.Replace("Ｓ", "S");
			text = text.Replace("Ｔ", "T");
			text = text.Replace("Ｕ", "U");
			text = text.Replace("Ｖ", "V");
			text = text.Replace("Ｗ", "W");
			text = text.Replace("Ｘ", "X");
			text = text.Replace("Ｙ", "Y");
			return text.Replace("ｚ", "z");
		}

		public string QuestionName = "";

		public string CircleQuestionName = "";

		public string ParentCode = "";

		private string QChildQuestion = "";

		public List<string> FillTexts = new List<string>();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private UDPX oFunc = new UDPX();
	}
}
