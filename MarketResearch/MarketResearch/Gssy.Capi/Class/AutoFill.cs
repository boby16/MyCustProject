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
	// Token: 0x02000063 RID: 99
	public class AutoFill : Page
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0009BAA8 File Offset: 0x00099CA8
		public bool AutoNext(SurveyDefine surveyDefine_0)
		{
			bool result = SurveyHelper.AutoFill;
			if (SurveyHelper.StopFillPage.Contains(global::GClass0.smethod_0("\"") + surveyDefine_0.PAGE_ID + global::GClass0.smethod_0("\"")))
			{
				result = false;
			}
			if (SurveyHelper.StopFillPage.Contains(global::GClass0.smethod_0(" ĳȢ")))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0009BB04 File Offset: 0x00099D04
		public string Fill(SurveyDefine surveyDefine_0)
		{
			string text = global::GClass0.smethod_0("");
			if (!(surveyDefine_0.FILLDATA == global::GClass0.smethod_0("")) && !(this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$")))
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

		// Token: 0x06000629 RID: 1577 RVA: 0x0009BB94 File Offset: 0x00099D94
		public string FillDec(SurveyDefine surveyDefine_0)
		{
			string text = global::GClass0.smethod_0("");
			if (!(surveyDefine_0.FILLDATA == global::GClass0.smethod_0("")) && !(this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$")))
			{
				string control_MASK = surveyDefine_0.CONTROL_MASK;
				text = this.oLogicEngine.stringResult(surveyDefine_0.FILLDATA);
				text = this.method_9(text, control_MASK, false);
			}
			else
			{
				int min_COUNT = surveyDefine_0.MIN_COUNT;
				int int_ = (surveyDefine_0.CONTROL_TYPE > 0) ? surveyDefine_0.CONTROL_TYPE : 9;
				int num = (surveyDefine_0.MAX_COUNT == 0) ? this.oFunc.StringToInt(this.oFunc.DupString(global::GClass0.smethod_0("8"), int_)) : surveyDefine_0.MAX_COUNT;
				string control_MASK2 = surveyDefine_0.CONTROL_MASK;
				text = this.oRandomEngine.RND((double)min_COUNT, (double)num).ToString();
				text = this.method_9(text, control_MASK2, true);
			}
			return text;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0009BC88 File Offset: 0x00099E88
		public string FillInt(SurveyDefine surveyDefine_0)
		{
			string text = global::GClass0.smethod_0("");
			if (!(surveyDefine_0.FILLDATA == global::GClass0.smethod_0("")) && !(this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$")))
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
				string string_2 = (max_COUNT == 0) ? this.oFunc.DupString(global::GClass0.smethod_0("8"), num) : max_COUNT.ToString();
				string control_MASK2 = surveyDefine_0.CONTROL_MASK;
				if (surveyDefine_0.MAX_COUNT == 0 && surveyDefine_0.MAX_COUNT == 0 && num == 11 && control_MASK2 == global::GClass0.smethod_0(""))
				{
					text = this.oRandomEngine.strRND(global::GClass0.smethod_0(":Ĺȹ̸зԶصܴ࠳ल਱"), global::GClass0.smethod_0(":ĲȰ̱оԿؼܽ࠺ऻਸ"));
				}
				else
				{
					text = this.oRandomEngine.strRND(string_, string_2);
					text = this.method_9(text, control_MASK2, true);
				}
			}
			return text;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0009BDC4 File Offset: 0x00099FC4
		public SurveyDetail SingleDetail(SurveyDefine surveyDefine_0, List<SurveyDetail> list_0)
		{
			SurveyDetail surveyDetail;
			if (surveyDefine_0.FILLDATA == global::GClass0.smethod_0(""))
			{
				surveyDetail = this.method_0(list_0);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$"))
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list = this.oTitle.ParaToList(string_, global::GClass0.smethod_0(";"));
				int int_ = (list.Count == 0 || list[0] == global::GClass0.smethod_0("")) ? 1 : ((int)this.oLogicEngine.doubleResult(list[0]));
				string_ = ((list.Count < 2 || list[1] == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄") : list[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<SurveyDetail>(), int_) - 1;
				surveyDetail = list_0[num];
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_8(list_0, string_3);
				text = this.oLogicEngine.stringCode(global::GClass0.smethod_0("\"ŗɅ͍цԩ") + text + global::GClass0.smethod_0("("));
				surveyDetail = this.method_5(list_0, text);
			}
			if (surveyDetail == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return surveyDetail;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0009BF60 File Offset: 0x0009A160
		public Button SingleButton(SurveyDefine surveyDefine_0, List<Button> list_0)
		{
			Button button;
			if (surveyDefine_0.FILLDATA == global::GClass0.smethod_0(""))
			{
				button = this.method_2(list_0);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$"))
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list = this.oTitle.ParaToList(string_, global::GClass0.smethod_0(";"));
				int int_ = (list.Count == 0 || list[0] == global::GClass0.smethod_0("")) ? 1 : ((int)this.oLogicEngine.doubleResult(list[0]));
				string_ = ((list.Count < 2 || list[1] == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄") : list[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<Button>(), int_) - 1;
				button = list_0[num];
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_7(list_0, string_3);
				text = this.oLogicEngine.stringCode(global::GClass0.smethod_0("\"ŗɅ͍цԩ") + text + global::GClass0.smethod_0("("));
				button = this.FindButton(list_0, text);
			}
			if (button == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return button;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0009C0FC File Offset: 0x0009A2FC
		public Rectangle SingleRectangle(SurveyDefine surveyDefine_0, List<Rectangle> list_0)
		{
			Rectangle rectangle;
			if (surveyDefine_0.FILLDATA == global::GClass0.smethod_0(""))
			{
				rectangle = this.method_11(list_0);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$"))
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list = this.oTitle.ParaToList(string_, global::GClass0.smethod_0(";"));
				int int_ = (list.Count == 0 || list[0] == global::GClass0.smethod_0("")) ? 1 : ((int)this.oLogicEngine.doubleResult(list[0]));
				string_ = ((list.Count < 2 || list[1] == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄") : list[1]);
				string string_2 = this.oLogicEngine.stringCode(string_);
				int num = this.oFunc.StringToInt(string_2);
				num = this.oFunc.MOD(num, list_0.Count<Rectangle>(), int_) - 1;
				rectangle = list_0[num];
			}
			else
			{
				string[] string_3 = this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
				string text = this.method_15(list_0, string_3);
				text = this.oLogicEngine.stringCode(global::GClass0.smethod_0("\"ŗɅ͍цԩ") + text + global::GClass0.smethod_0("("));
				rectangle = this.method_13(list_0, text);
			}
			if (rectangle == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return rectangle;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0009C298 File Offset: 0x0009A498
		public string CommonOther(SurveyDefine surveyDefine_0, string string_0 = "")
		{
			string result;
			if (string_0 == global::GClass0.smethod_0(""))
			{
				result = surveyDefine_0.QUESTION_NAME + global::GClass0.smethod_0("YŊɰͫѧճ");
			}
			else
			{
				result = surveyDefine_0.QUESTION_NAME + global::GClass0.smethod_0("]ł") + string_0 + global::GClass0.smethod_0("YŊɰͫѧճ");
			}
			return result;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0009C2F4 File Offset: 0x0009A4F4
		public List<Button> MultiButton(SurveyDefine surveyDefine_0, List<Button> list_0, List<Button> list_1, int int_0 = 0)
		{
			List<Button> list = new List<Button>();
			int int_ = this.method_10(surveyDefine_0.MIN_COUNT, surveyDefine_0.MAX_COUNT, list_0.Count, int_0, surveyDefine_0.FILLDATA == global::GClass0.smethod_0(""));
			if (surveyDefine_0.FILLDATA == global::GClass0.smethod_0(""))
			{
				list = this.method_3(list_0, int_);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$"))
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list2 = this.oTitle.ParaToList(string_, global::GClass0.smethod_0(";"));
				int int_2 = (list2.Count == 0 || list2[0] == global::GClass0.smethod_0("")) ? 1 : ((int)this.oLogicEngine.doubleResult(list2[0]));
				string_ = ((list2.Count < 2 || list2[1] == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄") : list2[1]);
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
					global::GClass0.smethod_0("\"ŗɅ͍цԩ"),
					text,
					global::GClass0.smethod_0(";"),
					int_.ToString(),
					global::GClass0.smethod_0("(")
				}));
				list = this.method_4(list_1, text);
			}
			if (list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return list;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0009C4F8 File Offset: 0x0009A6F8
		public List<Rectangle> MultiRectangle(SurveyDefine surveyDefine_0, List<Rectangle> list_0, int int_0 = 0)
		{
			List<Rectangle> list = new List<Rectangle>();
			int int_ = this.method_10(surveyDefine_0.MIN_COUNT, surveyDefine_0.MAX_COUNT, list_0.Count, int_0, true);
			if (surveyDefine_0.FILLDATA == global::GClass0.smethod_0(""))
			{
				list = this.method_12(list_0, int_);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$"))
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list2 = this.oTitle.ParaToList(string_, global::GClass0.smethod_0(";"));
				int int_2 = (list2.Count == 0 || list2[0] == global::GClass0.smethod_0("")) ? 1 : ((int)this.oLogicEngine.doubleResult(list2[0]));
				string_ = ((list2.Count < 2 || list2[1] == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄") : list2[1]);
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
					global::GClass0.smethod_0("\"ŗɅ͍цԩ"),
					text,
					global::GClass0.smethod_0(";"),
					int_.ToString(),
					global::GClass0.smethod_0("(")
				}));
				list = this.method_14(list_0, text);
			}
			if (list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return list;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0009C6E8 File Offset: 0x0009A8E8
		public List<SurveyDetail> MultiDetail(SurveyDefine surveyDefine_0, List<SurveyDetail> list_0, int int_0 = 0)
		{
			List<SurveyDetail> list = null;
			int int_ = this.method_10(surveyDefine_0.MIN_COUNT, surveyDefine_0.MAX_COUNT, list_0.Count, int_0, true);
			if (surveyDefine_0.FILLDATA == global::GClass0.smethod_0(""))
			{
				list = this.method_1(list_0, int_);
			}
			else if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$"))
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list2 = this.oTitle.ParaToList(string_, global::GClass0.smethod_0(";"));
				int int_2 = (list2.Count == 0 || list2[0] == global::GClass0.smethod_0("")) ? 1 : ((int)this.oLogicEngine.doubleResult(list2[0]));
				string_ = ((list2.Count < 2 || list2[1] == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄") : list2[1]);
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
					global::GClass0.smethod_0("\"ŗɅ͍цԩ"),
					text,
					global::GClass0.smethod_0(";"),
					int_.ToString(),
					global::GClass0.smethod_0("(")
				}));
				list = this.method_6(list_0, text);
			}
			if (list == null || list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, surveyDefine_0.QUESTION_NAME);
			}
			return list;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00003DD1 File Offset: 0x00001FD1
		public string[] RecodeFill(SurveyDefine surveyDefine_0)
		{
			return this.oLogicEngine.aryCode(surveyDefine_0.FILLDATA, ',');
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0009C8D4 File Offset: 0x0009AAD4
		public string[] RecodeSingle(SurveyDefine surveyDefine_0, List<SurveyDetail> list_0)
		{
			string[] result;
			if (this.oFunc.LEFT(surveyDefine_0.FILLDATA, 1) == global::GClass0.smethod_0("$"))
			{
				string string_ = this.oFunc.MID(surveyDefine_0.FILLDATA, 1, -9999);
				List<string> list = this.oTitle.ParaToList(string_, global::GClass0.smethod_0(";"));
				int int_ = (list.Count == 0 || list[0] == global::GClass0.smethod_0("")) ? 1 : ((int)this.oLogicEngine.doubleResult(list[0]));
				string_ = ((list.Count < 2 || list[1] == global::GClass0.smethod_0("")) ? global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄") : list[1]);
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

		// Token: 0x06000634 RID: 1588 RVA: 0x0009CA0C File Offset: 0x0009AC0C
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

		// Token: 0x06000635 RID: 1589 RVA: 0x0009CA54 File Offset: 0x0009AC54
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

		// Token: 0x06000636 RID: 1590 RVA: 0x0009CAE8 File Offset: 0x0009ACE8
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

		// Token: 0x06000637 RID: 1591 RVA: 0x0009CB2C File Offset: 0x0009AD2C
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

		// Token: 0x06000638 RID: 1592 RVA: 0x0009CBC0 File Offset: 0x0009ADC0
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

		// Token: 0x06000639 RID: 1593 RVA: 0x0009CC24 File Offset: 0x0009AE24
		private List<Button> method_4(List<Button> list_0, string string_0)
		{
			List<Button> list = new List<Button>();
			List<string> list2 = this.oFunc.StringToList(string_0, global::GClass0.smethod_0("-"));
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

		// Token: 0x0600063A RID: 1594 RVA: 0x0009CCAC File Offset: 0x0009AEAC
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

		// Token: 0x0600063B RID: 1595 RVA: 0x0009CD0C File Offset: 0x0009AF0C
		private List<SurveyDetail> method_6(List<SurveyDetail> list_0, string string_0)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			List<string> list2 = this.oFunc.StringToList(string_0, global::GClass0.smethod_0("-"));
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

		// Token: 0x0600063C RID: 1596 RVA: 0x0009CD8C File Offset: 0x0009AF8C
		private string method_7(List<Button> list_0, string[] string_0)
		{
			string text = global::GClass0.smethod_0("");
			foreach (string text2 in string_0)
			{
				using (List<Button>.Enumerator enumerator = list_0.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Name.Substring(2) == text2)
						{
							text = text + global::GClass0.smethod_0("-") + text2;
							break;
						}
					}
				}
			}
			if (text != global::GClass0.smethod_0(""))
			{
				text = this.oFunc.MID(text, 1, -9999);
			}
			return text;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0009CE40 File Offset: 0x0009B040
		private string method_8(List<SurveyDetail> list_0, string[] string_0)
		{
			string text = global::GClass0.smethod_0("");
			foreach (string text2 in string_0)
			{
				using (List<SurveyDetail>.Enumerator enumerator = list_0.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.CODE == text2)
						{
							text = text + global::GClass0.smethod_0("-") + text2;
							break;
						}
					}
				}
			}
			if (text != global::GClass0.smethod_0(""))
			{
				text = this.oFunc.MID(text, 1, -9999);
			}
			return text;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0009CEF0 File Offset: 0x0009B0F0
		private string method_9(string string_0, string string_1, bool bool_0 = false)
		{
			string text = string_0;
			if (string_1 != global::GClass0.smethod_0(""))
			{
				List<string> list = this.oTitle.ParaToList(string_1, global::GClass0.smethod_0("-"));
				List<string> list2 = this.oTitle.ParaToList(string_0, global::GClass0.smethod_0("/"));
				string text2 = list[0];
				string text3 = list2[0];
				if (text2.Contains(global::GClass0.smethod_0("\u007f")))
				{
					List<string> list3 = this.oTitle.ParaToList(text2, global::GClass0.smethod_0("\u007f"));
					int num = this.oFunc.StringToInt(list3[0]);
					int num2 = this.oFunc.StringToInt(list3[1]);
					if (num > text3.Length)
					{
						text3 = this.oFunc.FillString(text3, global::GClass0.smethod_0("1"), num, true);
					}
					else if (num2 < text3.Length && bool_0)
					{
						text3 = this.oFunc.RIGHT(text3, num2);
					}
				}
				else
				{
					string a = this.oFunc.LEFT(text2, 2);
					if (a == global::GClass0.smethod_0(">ļ"))
					{
						if (bool_0)
						{
							int int_ = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999));
							text3 = this.oFunc.RIGHT(text3, int_);
						}
					}
					else if (a == global::GClass0.smethod_0("<ļ"))
					{
						int int_2 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999));
						text3 = this.oFunc.FillString(text3, global::GClass0.smethod_0("1"), int_2, true);
					}
					else
					{
						a = this.oFunc.LEFT(text2, 1);
						if (a == global::GClass0.smethod_0("="))
						{
							if (bool_0)
							{
								int int_3 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999)) - 1;
								text3 = this.oFunc.RIGHT(text3, int_3);
							}
						}
						else if (a == global::GClass0.smethod_0("?"))
						{
							int int_4 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999)) + 1;
							text3 = this.oFunc.FillString(text3, global::GClass0.smethod_0("1"), int_4, true);
						}
						else
						{
							int int_5 = this.oFunc.StringToInt(text2);
							text3 = this.oFunc.FillString(text3, global::GClass0.smethod_0("1"), int_5, true);
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
					text3 = ((list2.Count > 1) ? list2[1] : global::GClass0.smethod_0(""));
					if (text2.Contains(global::GClass0.smethod_0("\u007f")))
					{
						List<string> list4 = this.oTitle.ParaToList(text2, global::GClass0.smethod_0("\u007f"));
						int num3 = this.oFunc.StringToInt(list4[0]);
						int num4 = this.oFunc.StringToInt(list4[1]);
						if (num3 > text3.Length)
						{
							text3 = this.oFunc.FillString(text3, global::GClass0.smethod_0("1"), num3, false);
						}
						else if (num4 < text3.Length && bool_0)
						{
							text3 = this.oFunc.LEFT(text3, num4);
						}
					}
					else
					{
						string a2 = this.oFunc.LEFT(text2, 2);
						if (a2 == global::GClass0.smethod_0(">ļ"))
						{
							if (bool_0)
							{
								int int_6 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999));
								text3 = this.oFunc.LEFT(text3, int_6);
							}
						}
						else if (a2 == global::GClass0.smethod_0("<ļ"))
						{
							int int_7 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999));
							text3 = this.oFunc.FillString(text3, global::GClass0.smethod_0("1"), int_7, false);
						}
						else
						{
							a2 = this.oFunc.LEFT(text2, 1);
							if (a2 == global::GClass0.smethod_0("="))
							{
								if (bool_0)
								{
									int int_8 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999)) - 1;
									text3 = this.oFunc.LEFT(text3, int_8);
								}
							}
							else if (a2 == global::GClass0.smethod_0("?"))
							{
								int int_9 = this.oFunc.StringToInt(this.oFunc.MID(text2, 1, -9999)) + 1;
								text3 = this.oFunc.FillString(text3, global::GClass0.smethod_0("1"), int_9, false);
							}
							else
							{
								int int_10 = this.oFunc.StringToInt(text2);
								text3 = this.oFunc.FillString(text3, global::GClass0.smethod_0("1"), int_10, false);
								if (bool_0)
								{
									text3 = this.oFunc.LEFT(text3, int_10);
								}
							}
						}
					}
					if (text3 != global::GClass0.smethod_0(""))
					{
						text = text + global::GClass0.smethod_0("/") + text3;
					}
				}
				else if (list2.Count > 1)
				{
					text = text + global::GClass0.smethod_0("/") + list2[1];
				}
			}
			return text;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0009D468 File Offset: 0x0009B668
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
				if (SurveyHelper.FillMode == global::GClass0.smethod_0("0"))
				{
					result = (bool_0 ? num : num2);
				}
				else if (SurveyHelper.FillMode == global::GClass0.smethod_0("3"))
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

		// Token: 0x06000640 RID: 1600 RVA: 0x0009D4E8 File Offset: 0x0009B6E8
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

		// Token: 0x06000641 RID: 1601 RVA: 0x0009D52C File Offset: 0x0009B72C
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

		// Token: 0x06000642 RID: 1602 RVA: 0x0009D5C0 File Offset: 0x0009B7C0
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

		// Token: 0x06000643 RID: 1603 RVA: 0x0009D624 File Offset: 0x0009B824
		private List<Rectangle> method_14(List<Rectangle> list_0, string string_0)
		{
			List<Rectangle> list = new List<Rectangle>();
			List<string> list2 = this.oFunc.StringToList(string_0, global::GClass0.smethod_0("-"));
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

		// Token: 0x06000644 RID: 1604 RVA: 0x0009D6AC File Offset: 0x0009B8AC
		private string method_15(List<Rectangle> list_0, string[] string_0)
		{
			string text = global::GClass0.smethod_0("");
			foreach (string text2 in string_0)
			{
				using (List<Rectangle>.Enumerator enumerator = list_0.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Name.Substring(2) == text2)
						{
							text = text + global::GClass0.smethod_0("-") + text2;
							break;
						}
					}
				}
			}
			if (text != global::GClass0.smethod_0(""))
			{
				text = this.oFunc.MID(text, 1, -9999);
			}
			return text;
		}

		// Token: 0x04000B02 RID: 2818
		private UDPX oFunc = new UDPX();

		// Token: 0x04000B03 RID: 2819
		public LogicEngine oLogicEngine = new LogicEngine();

		// Token: 0x04000B04 RID: 2820
		private RandomEngine oRandomEngine = new RandomEngine();

		// Token: 0x04000B05 RID: 2821
		private BoldTitle oTitle = new BoldTitle();
	}
}
