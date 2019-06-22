using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Gssy.Capi.BIZ;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.Class
{
	public class AutoFill : Page
	{
		public bool AutoNext(SurveyDefine surveyDefine_0)
		{
			bool result = SurveyHelper.AutoFill;
			if (SurveyHelper.StopFillPage.Contains("#" + surveyDefine_0.PAGE_ID + "#"))
			{
				result = false;
			}
			if (SurveyHelper.StopFillPage.Contains("#1#"))
			{
				result = false;
			}
			return result;
		}

		public string Fill(SurveyDefine surveyDefine_0)
		{
			string text = "";
			if (!(surveyDefine_0.FILLDATA == "") && !(this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%"))
			{
				text = this.oLogicEngine.stringResult(surveyDefine_0.FILLDATA);
			}
			else
			{
				text = surveyDefine_0.QUESTION_NAME;
			}
			int control_TYPE = surveyDefine_0.CONTROL_TYPE;
			if (control_TYPE > 0 && text.Length > control_TYPE)
			{
				text = this.oFunc.LEFT(text, control_TYPE);
			}
			return text;
		}

		public string FillDec(SurveyDefine surveyDefine_0)
		{
			string text = "";
			if (!(surveyDefine_0.FILLDATA == "") && !(this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%"))
			{
				string control_MASK = surveyDefine_0.CONTROL_MASK;
				text = this.oLogicEngine.stringResult(surveyDefine_0.FILLDATA);
				text = this.method_9(text, control_MASK, false);
			}
			else
			{
				int min_COUNT = surveyDefine_0.MIN_COUNT;
				int int_ = (surveyDefine_0.CONTROL_TYPE > 0) ? surveyDefine_0.CONTROL_TYPE : 9;
				int num = (surveyDefine_0.MAX_COUNT == 0) ? this.oFunc.StringToInt(this.oFunc.DupString("9", int_)) : surveyDefine_0.MAX_COUNT;
				string control_MASK2 = surveyDefine_0.CONTROL_MASK;
				text = this.oRandomEngine.RND((double)min_COUNT, (double)num).ToString();
				text = this.method_9(text, control_MASK2, true);
			}
			return text;
		}

		public string FillInt(SurveyDefine surveyDefine_0)
		{
			string text = "";
			if (!(surveyDefine_0.FILLDATA == "") && !(this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%"))
			{
				string control_MASK = surveyDefine_0.CONTROL_MASK;
				text = this.oLogicEngine.stringResult(surveyDefine_0.FILLDATA);
				text = this.method_9(text, control_MASK, false);
			}
			else
			{
				int min_COUNT = surveyDefine_0.MIN_COUNT;
				int max_COUNT = surveyDefine_0.MAX_COUNT;
				int num = (surveyDefine_0.CONTROL_TYPE > 0) ? surveyDefine_0.CONTROL_TYPE : 11;
				string string_ = min_COUNT.ToString();
				string string_2 = (max_COUNT == 0) ? this.oFunc.DupString("9", num) : max_COUNT.ToString();
				string control_MASK2 = surveyDefine_0.CONTROL_MASK;
				if (surveyDefine_0.MAX_COUNT == 0 && surveyDefine_0.MAX_COUNT == 0 && num == 11 && control_MASK2 == "")
				{
					text = this.oRandomEngine.strRND("13000000000", "18999999999");
				}
				else
				{
					text = this.oRandomEngine.strRND(string_, string_2);
					text = this.method_9(text, control_MASK2, true);
				}
			}
			return text;
		}

		public SurveyDetail SingleDetail(SurveyDefine surveyDefine_0, List<SurveyDetail> list_0)
		{
			SurveyDetail surveyDetail;
			if (surveyDefine_0.FILLDATA == "")
			{
				surveyDetail = this.method_0(list_0);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%")
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list = this.oTitle.ParaToList(string_, ":");
				int int_ = (list.Count == 0 || list[0] == "") ? 1 : ((int)this.oLogicEngine.doubleResult(list[0]));
				string_ = ((list.Count < 2 || list[1] == "") ? "SURVEY_CODE" : list[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<SurveyDetail>(), int_) - 1;
				surveyDetail = list_0[num];
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_8(list_0, string_3);
				text = this.oLogicEngine.stringCode("$RAND(" + text + ")");
				surveyDetail = this.method_5(list_0, text);
			}
			if (surveyDetail == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return surveyDetail;
		}

		public Button SingleButton(SurveyDefine surveyDefine_0, List<Button> list_0)
		{
			Button button;
			if (surveyDefine_0.FILLDATA == "")
			{
				button = this.method_2(list_0);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%")
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list = this.oTitle.ParaToList(string_, ":");
				int int_ = (list.Count == 0 || list[0] == "") ? 1 : ((int)this.oLogicEngine.doubleResult(list[0]));
				string_ = ((list.Count < 2 || list[1] == "") ? "SURVEY_CODE" : list[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<Button>(), int_) - 1;
				button = list_0[num];
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_7(list_0, string_3);
				text = this.oLogicEngine.stringCode("$RAND(" + text + ")");
				button = this.FindButton(list_0, text);
			}
			if (button == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return button;
		}

		public Rectangle SingleRectangle(SurveyDefine surveyDefine_0, List<Rectangle> list_0)
		{
			Rectangle rectangle;
			if (surveyDefine_0.FILLDATA == "")
			{
				rectangle = this.method_11(list_0);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%")
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list = this.oTitle.ParaToList(string_, ":");
				int int_ = (list.Count == 0 || list[0] == "") ? 1 : ((int)this.oLogicEngine.doubleResult(list[0]));
				string_ = ((list.Count < 2 || list[1] == "") ? "SURVEY_CODE" : list[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<Rectangle>(), int_) - 1;
				rectangle = list_0[num];
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_15(list_0, string_3);
				text = this.oLogicEngine.stringCode("$RAND(" + text + ")");
				rectangle = this.method_13(list_0, text);
			}
			if (rectangle == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return rectangle;
		}

		public string CommonOther(SurveyDefine surveyDefine_0, string string_0 = "")
		{
			string result;
			if (string_0 == "")
			{
				result = surveyDefine_0.QUESTION_NAME + "_Other";
			}
			else
			{
				result = surveyDefine_0.QUESTION_NAME + "_C" + string_0 + "_Other";
			}
			return result;
		}

		public List<Button> MultiButton(SurveyDefine surveyDefine_0, List<Button> list_0, List<Button> list_1, int int_0 = 0)
		{
			List<Button> list = new List<Button>();
			int int_ = this.method_10(surveyDefine_0.MIN_COUNT, surveyDefine_0.MAX_COUNT, list_0.Count, int_0, surveyDefine_0.FILLDATA == "");
			if (surveyDefine_0.FILLDATA == "")
			{
				list = this.method_3(list_0, int_);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%")
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list2 = this.oTitle.ParaToList(string_, ":");
				int int_2 = (list2.Count == 0 || list2[0] == "") ? 1 : ((int)this.oLogicEngine.doubleResult(list2[0]));
				string_ = ((list2.Count < 2 || list2[1] == "") ? "SURVEY_CODE" : list2[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_1.Count<Button>(), int_2) - 1;
				list.Add(list_1[num]);
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_7(list_1, string_3);
				text = this.oLogicEngine.stringCode(string.Concat(new string[]
				{
					"$RAND(",
					text,
					":",
					int_.ToString(),
					")"
				}));
				list = this.method_4(list_1, text);
			}
			if (list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return list;
		}

		public List<Rectangle> MultiRectangle(SurveyDefine surveyDefine_0, List<Rectangle> list_0, int int_0 = 0)
		{
			List<Rectangle> list = new List<Rectangle>();
			int int_ = this.method_10(surveyDefine_0.MIN_COUNT, surveyDefine_0.MAX_COUNT, list_0.Count, int_0, true);
			if (surveyDefine_0.FILLDATA == "")
			{
				list = this.method_12(list_0, int_);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%")
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list2 = this.oTitle.ParaToList(string_, ":");
				int int_2 = (list2.Count == 0 || list2[0] == "") ? 1 : ((int)this.oLogicEngine.doubleResult(list2[0]));
				string_ = ((list2.Count < 2 || list2[1] == "") ? "SURVEY_CODE" : list2[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<Rectangle>(), int_2) - 1;
				list.Add(list_0[num]);
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_15(list_0, string_3);
				text = this.oLogicEngine.stringCode(string.Concat(new string[]
				{
					"$RAND(",
					text,
					":",
					int_.ToString(),
					")"
				}));
				list = this.method_14(list_0, text);
			}
			if (list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return list;
		}

		public List<SurveyDetail> MultiDetail(SurveyDefine surveyDefine_0, List<SurveyDetail> list_0, int int_0 = 0)
		{
			List<SurveyDetail> list = null;
			int int_ = this.method_10(surveyDefine_0.MIN_COUNT, surveyDefine_0.MAX_COUNT, list_0.Count, int_0, true);
			if (surveyDefine_0.FILLDATA == "")
			{
				list = this.method_1(list_0, int_);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%")
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list2 = this.oTitle.ParaToList(string_, ":");
				int int_2 = (list2.Count == 0 || list2[0] == "") ? 1 : ((int)this.oLogicEngine.doubleResult(list2[0]));
				string_ = ((list2.Count < 2 || list2[1] == "") ? "SURVEY_CODE" : list2[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<SurveyDetail>(), int_2) - 1;
				list.Add(list_0[num]);
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_8(list_0, string_3);
				text = this.oLogicEngine.stringCode(string.Concat(new string[]
				{
					"$RAND(",
					text,
					":",
					int_.ToString(),
					")"
				}));
				list = this.method_6(list_0, text);
			}
			if (list == null || list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return list;
		}

		public string[] RecodeFill(SurveyDefine surveyDefine_0)
		{
			return this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
		}

		public string[] RecodeSingle(SurveyDefine surveyDefine_0, List<SurveyDetail> list_0)
		{
			string[] result;
			if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == "%")
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list = this.oTitle.ParaToList(string_, ":");
				int int_ = (list.Count == 0 || list[0] == "") ? 1 : ((int)this.oLogicEngine.doubleResult(list[0]));
				string_ = ((list.Count < 2 || list[1] == "") ? "SURVEY_CODE" : list[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<SurveyDetail>(), int_) - 1;
				result = list_0[num].CODE.Split(new char[]
				{
					','
				});
			}
			else
			{
				result = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
			}
			return result;
		}

		private SurveyDetail method_0(List<SurveyDetail> list_0)
		{
			SurveyDetail result = new SurveyDetail();
			try
			{
				int index = this.oRandomEngine.intRND(0, list_0.Count - 1);
				result = list_0[index];
			}
			catch (Exception)
			{
			}
			return result;
		}

		private List<SurveyDetail> method_1(List<SurveyDetail> list_0, int int_0)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			try
			{
				if (list_0.Count < int_0)
				{
					int_0 = list_0.Count;
				}
				foreach (int num in this.oRandomEngine.intRandomList(int_0, 1, list_0.Count))
				{
					list.Add(list_0[num - 1]);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		private Button method_2(List<Button> list_0)
		{
			Button result = null;
			try
			{
				int index = this.oRandomEngine.intRND(0, list_0.Count - 1);
				result = list_0[index];
			}
			catch (Exception)
			{
			}
			return result;
		}

		private List<Button> method_3(List<Button> list_0, int int_0)
		{
			List<Button> list = new List<Button>();
			try
			{
				if (list_0.Count < int_0)
				{
					int_0 = list_0.Count;
				}
				foreach (int num in this.oRandomEngine.intRandomList(int_0, 1, list_0.Count))
				{
					list.Add(list_0[num - 1]);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		public Button FindButton(List<Button> list_0, string string_0)
		{
			foreach (Button button in list_0)
			{
				if (button.Name.Substring(2) == string_0)
				{
					return button;
				}
			}
			return null;
		}

		private List<Button> method_4(List<Button> list_0, string string_0)
		{
			List<Button> list = new List<Button>();
			List<string> list2 = this.oFunc.StringToList(string_0, ",");
			foreach (Button button in list_0)
			{
				string item = button.Name.Substring(2);
				if (list2.Contains(item))
				{
					list.Add(button);
				}
			}
			return list;
		}

		private SurveyDetail method_5(List<SurveyDetail> list_0, string string_0)
		{
			foreach (SurveyDetail surveyDetail in list_0)
			{
				if (surveyDetail.CODE == string_0)
				{
					return surveyDetail;
				}
			}
			return null;
		}

		private List<SurveyDetail> method_6(List<SurveyDetail> list_0, string string_0)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			List<string> list2 = this.oFunc.StringToList(string_0, ",");
			foreach (SurveyDetail surveyDetail in list_0)
			{
				string code = surveyDetail.CODE;
				if (list2.Contains(code))
				{
					list.Add(surveyDetail);
				}
			}
			return list;
		}

		private string method_7(List<Button> list_0, string[] string_0)
		{
			string text = "";
			foreach (string text2 in string_0)
			{
				using (List<Button>.Enumerator enumerator = list_0.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Name.Substring(2) == text2)
						{
							text = text + "," + text2;
							break;
						}
					}
				}
			}
			if (text != "")
			{
				text = this.oFunc.MID(text, 1, -9999);
			}
			return text;
		}

		private string method_8(List<SurveyDetail> list_0, string[] string_0)
		{
			string text = "";
			foreach (string text2 in string_0)
			{
				using (List<SurveyDetail>.Enumerator enumerator = list_0.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.CODE == text2)
						{
							text = text + "," + text2;
							break;
						}
					}
				}
			}
			if (text != "")
			{
				text = this.oFunc.MID(text, 1, -9999);
			}
			return text;
		}

		private string method_9(string string_0, string string_1, bool bool_0 = false)
		{
			string text = string_0;
			if (string_1 != "")
			{
				List<string> list = this.oTitle.ParaToList(string_1, ",");
				List<string> list2 = this.oTitle.ParaToList(string_0, ".");
				string text2 = list[0];
				string text3 = list2[0];
				if (text2.Contains("~"))
				{
					List<string> list3 = this.oTitle.ParaToList(text2, "~");
					int num = this.oFunc.StringToInt(list3[0]);
					int num2 = this.oFunc.StringToInt(list3[1]);
					if (num > text3.Length)
					{
						text3 = this.oFunc.FillString(text3, "0", num, true);
					}
					else if (num2 < text3.Length && bool_0)
					{
						text3 = this.oFunc.RIGHT(text3, num2);
					}
				}
				else
				{
					string a = this.oFunc.LEFT(text2, 2);
					if (a == "<=")
					{
						if (bool_0)
						{
							int int_ = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999));
							text3 = this.oFunc.RIGHT(text3, int_);
						}
					}
					else if (a == ">=")
					{
						int int_2 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999));
						text3 = this.oFunc.FillString(text3, "0", int_2, true);
					}
					else
					{
						a = this.oFunc.LEFT(text2, 1);
						if (a == "<")
						{
							if (bool_0)
							{
								int int_3 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999)) - 1;
								text3 = this.oFunc.RIGHT(text3, int_3);
							}
						}
						else if (a == ">")
						{
							int int_4 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999)) + 1;
							text3 = this.oFunc.FillString(text3, "0", int_4, true);
						}
						else
						{
							int int_5 = this.oFunc.StringToInt(text2);
							text3 = this.oFunc.FillString(text3, "0", int_5, true);
							if (bool_0)
							{
								text3 = this.oFunc.RIGHT(text3, int_5);
							}
						}
					}
				}
				text = text3;
				if (list.Count > 1)
				{
					text2 = list[1];
					text3 = ((list2.Count > 1) ? list2[1] : "");
					if (text2.Contains("~"))
					{
						List<string> list4 = this.oTitle.ParaToList(text2, "~");
						int num3 = this.oFunc.StringToInt(list4[0]);
						int num4 = this.oFunc.StringToInt(list4[1]);
						if (num3 > text3.Length)
						{
							text3 = this.oFunc.FillString(text3, "0", num3, false);
						}
						else if (num4 < text3.Length && bool_0)
						{
							text3 = this.oFunc.LEFT(text3, num4);
						}
					}
					else
					{
						string a2 = this.oFunc.LEFT(text2, 2);
						if (a2 == "<=")
						{
							if (bool_0)
							{
								int int_6 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999));
								text3 = this.oFunc.LEFT(text3, int_6);
							}
						}
						else if (a2 == ">=")
						{
							int int_7 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999));
							text3 = this.oFunc.FillString(text3, "0", int_7, false);
						}
						else
						{
							a2 = this.oFunc.LEFT(text2, 1);
							if (a2 == "<")
							{
								if (bool_0)
								{
									int int_8 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999)) - 1;
									text3 = this.oFunc.LEFT(text3, int_8);
								}
							}
							else if (a2 == ">")
							{
								int int_9 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999)) + 1;
								text3 = this.oFunc.FillString(text3, "0", int_9, false);
							}
							else
							{
								int int_10 = this.oFunc.StringToInt(text2);
								text3 = this.oFunc.FillString(text3, "0", int_10, false);
								if (bool_0)
								{
									text3 = this.oFunc.LEFT(text3, int_10);
								}
							}
						}
					}
					if (text3 != "")
					{
						text = text + "." + text3;
					}
				}
				else if (list2.Count > 1)
				{
					text = text + "." + list2[1];
				}
			}
			return text;
		}

		private int method_10(int int_0, int int_1, int int_2, int int_3 = 0, bool bool_0 = true)
		{
			int result = 0;
			int num = (int_0 > 1) ? int_0 : 1;
			int num2 = int_3;
			if (int_3 == 0)
			{
				num2 = int_1;
				if (num2 == 0 || num2 > int_2)
				{
					num2 = int_2;
				}
			}
			if (num2 > 0)
			{
				if (SurveyHelper.FillMode == "1")
				{
					result = (bool_0 ? num : num2);
				}
				else if (SurveyHelper.FillMode == "2")
				{
					result = (bool_0 ? this.oRandomEngine.intRND(num, num2) : num2);
				}
				else
				{
					result = num2;
				}
			}
			return result;
		}

		private Rectangle method_11(List<Rectangle> list_0)
		{
			Rectangle result = null;
			try
			{
				int index = this.oRandomEngine.intRND(0, list_0.Count - 1);
				result = list_0[index];
			}
			catch (Exception)
			{
			}
			return result;
		}

		private List<Rectangle> method_12(List<Rectangle> list_0, int int_0)
		{
			List<Rectangle> list = new List<Rectangle>();
			try
			{
				if (list_0.Count < int_0)
				{
					int_0 = list_0.Count;
				}
				foreach (int num in this.oRandomEngine.intRandomList(int_0, 1, list_0.Count))
				{
					list.Add(list_0[num - 1]);
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		private Rectangle method_13(List<Rectangle> list_0, string string_0)
		{
			foreach (Rectangle rectangle in list_0)
			{
				if (rectangle.Name.Substring(2) == string_0)
				{
					return rectangle;
				}
			}
			return null;
		}

		private List<Rectangle> method_14(List<Rectangle> list_0, string string_0)
		{
			List<Rectangle> list = new List<Rectangle>();
			List<string> list2 = this.oFunc.StringToList(string_0, ",");
			foreach (Rectangle rectangle in list_0)
			{
				string item = rectangle.Name.Substring(2);
				if (list2.Contains(item))
				{
					list.Add(rectangle);
				}
			}
			return list;
		}

		private string method_15(List<Rectangle> list_0, string[] string_0)
		{
			string text = "";
			foreach (string text2 in string_0)
			{
				using (List<Rectangle>.Enumerator enumerator = list_0.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Name.Substring(2) == text2)
						{
							text = text + "," + text2;
							break;
						}
					}
				}
			}
			if (text != "")
			{
				text = this.oFunc.MID(text, 1, -9999);
			}
			return text;
		}

		private UDPX oFunc = new UDPX();

		public LogicEngine oLogicEngine = new LogicEngine();

		private RandomEngine oRandomEngine = new RandomEngine();

		private BoldTitle oTitle = new BoldTitle();
	}
}
