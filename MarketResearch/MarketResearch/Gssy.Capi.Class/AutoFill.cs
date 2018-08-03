using Gssy.Capi.BIZ;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Gssy.Capi.Class
{
	public class AutoFill : Page
	{
		private UDPX oFunc = new UDPX();

		public LogicEngine oLogicEngine = new LogicEngine();

		private RandomEngine oRandomEngine = new RandomEngine();

		private BoldTitle oTitle = new BoldTitle();

		public bool AutoNext(SurveyDefine _003F414_003F)
		{
			bool result = SurveyHelper.AutoFill;
			if (SurveyHelper.StopFillPage.Contains(_003F487_003F._003F488_003F("\"") + _003F414_003F.PAGE_ID + _003F487_003F._003F488_003F("\"")))
			{
				result = false;
			}
			if (SurveyHelper.StopFillPage.Contains(_003F487_003F._003F488_003F(" ĳȢ")))
			{
				result = false;
			}
			return result;
		}

		public string Fill(SurveyDefine _003F414_003F)
		{
			//IL_008b: Incompatible stack heights: 0 vs 3
			//IL_00ab: Incompatible stack heights: 0 vs 2
			//IL_00bb: Incompatible stack heights: 0 vs 1
			string text = _003F487_003F._003F488_003F("");
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				UDPX oFunc2 = oFunc;
				string fILLDATum = _003F414_003F.FILLDATA;
				if (!(((UDPX)/*Error near IL_002a: Stack underflow*/).LEFT((string)/*Error near IL_002a: Stack underflow*/, (int)/*Error near IL_002a: Stack underflow*/) == _003F487_003F._003F488_003F("$")))
				{
					text = oLogicEngine.stringResult(_003F414_003F.FILLDATA);
					goto IL_005c;
				}
			}
			text = _003F414_003F.QUESTION_NAME;
			goto IL_005c;
			IL_005c:
			int cONTROL_TYPE = _003F414_003F.CONTROL_TYPE;
			if (cONTROL_TYPE > 0)
			{
				int length = text.Length;
				if (/*Error near IL_00b0: Stack underflow*/ > /*Error near IL_00b0: Stack underflow*/)
				{
					UDPX oFunc3 = oFunc;
					text = ((UDPX)/*Error near IL_00c2: Stack underflow*/).LEFT(text, cONTROL_TYPE);
				}
			}
			return text;
		}

		public string FillDec(SurveyDefine _003F414_003F)
		{
			//IL_00dc: Incompatible stack heights: 0 vs 3
			//IL_00f2: Incompatible stack heights: 0 vs 1
			//IL_00f7: Incompatible stack heights: 1 vs 0
			//IL_0107: Incompatible stack heights: 0 vs 1
			//IL_010c: Incompatible stack heights: 1 vs 0
			string text = _003F487_003F._003F488_003F("");
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				UDPX oFunc2 = oFunc;
				string fILLDATum = _003F414_003F.FILLDATA;
				if (!(((UDPX)/*Error near IL_002a: Stack underflow*/).LEFT((string)/*Error near IL_002a: Stack underflow*/, (int)/*Error near IL_002a: Stack underflow*/) == _003F487_003F._003F488_003F("$")))
				{
					goto IL_0111;
				}
			}
			int mIN_COUNT = _003F414_003F.MIN_COUNT;
			int cONTROL_TYPE;
			if (_003F414_003F.CONTROL_TYPE > 0)
			{
				cONTROL_TYPE = _003F414_003F.CONTROL_TYPE;
			}
			int _003F127_003F = cONTROL_TYPE;
			int num;
			if (_003F414_003F.MAX_COUNT == 0)
			{
				num = oFunc.StringToInt(oFunc.DupString(_003F487_003F._003F488_003F("8"), _003F127_003F));
			}
			else
			{
				int mAX_COUNT = _003F414_003F.MAX_COUNT;
			}
			int num2 = num;
			string cONTROL_MASK = _003F414_003F.CONTROL_MASK;
			text = oRandomEngine.RND((double)mIN_COUNT, (double)num2).ToString();
			return _003F282_003F(text, cONTROL_MASK, true);
			IL_00c0:
			goto IL_0111;
			IL_0111:
			string cONTROL_MASK2 = _003F414_003F.CONTROL_MASK;
			text = oLogicEngine.stringResult(_003F414_003F.FILLDATA);
			return _003F282_003F(text, cONTROL_MASK2, false);
		}

		public string FillInt(SurveyDefine _003F414_003F)
		{
			//IL_010d: Incompatible stack heights: 0 vs 3
			//IL_0123: Incompatible stack heights: 0 vs 1
			//IL_0128: Incompatible stack heights: 1 vs 0
			//IL_0139: Incompatible stack heights: 0 vs 1
			//IL_013e: Incompatible stack heights: 1 vs 0
			//IL_014e: Incompatible stack heights: 0 vs 1
			//IL_015b: Incompatible stack heights: 0 vs 2
			//IL_0176: Incompatible stack heights: 0 vs 1
			//IL_0186: Incompatible stack heights: 0 vs 1
			string text = _003F487_003F._003F488_003F("");
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				UDPX oFunc2 = oFunc;
				string fILLDATum = _003F414_003F.FILLDATA;
				if (!(((UDPX)/*Error near IL_002a: Stack underflow*/).LEFT((string)/*Error near IL_002a: Stack underflow*/, (int)/*Error near IL_002a: Stack underflow*/) == _003F487_003F._003F488_003F("$")))
				{
					goto IL_0190;
				}
			}
			int mIN_COUNT = _003F414_003F.MIN_COUNT;
			int mAX_COUNT = _003F414_003F.MAX_COUNT;
			int cONTROL_TYPE;
			if (_003F414_003F.CONTROL_TYPE > 0)
			{
				cONTROL_TYPE = _003F414_003F.CONTROL_TYPE;
			}
			int _003F127_003F = cONTROL_TYPE;
			string _003F318_003F = mIN_COUNT.ToString();
			string text2;
			if (mAX_COUNT == 0)
			{
				text2 = oFunc.DupString(_003F487_003F._003F488_003F("8"), _003F127_003F);
			}
			else
			{
				mAX_COUNT.ToString();
			}
			string _003F319_003F = text2;
			string cONTROL_MASK = _003F414_003F.CONTROL_MASK;
			if (_003F414_003F.MAX_COUNT == 0)
			{
				int mAX_COUNT2 = _003F414_003F.MAX_COUNT;
				if ((int)/*Error near IL_0153: Stack underflow*/ == 0 && /*Error near IL_0160: Stack underflow*/== /*Error near IL_0160: Stack underflow*/)
				{
					bool flag = cONTROL_MASK == _003F487_003F._003F488_003F("");
					if ((int)/*Error near IL_017b: Stack underflow*/ != 0)
					{
						RandomEngine oRandomEngine2 = oRandomEngine;
						string _003F318_003F2 = _003F487_003F._003F488_003F(":Ĺȹ\u0338зԶص\u0734࠳ल\u0a31");
						string _003F319_003F2 = _003F487_003F._003F488_003F(":ĲȰ\u0331оԿؼ\u073d࠺\u093bਸ");
						return ((RandomEngine)/*Error near IL_00cb: Stack underflow*/).strRND(_003F318_003F2, _003F319_003F2);
					}
				}
			}
			text = oRandomEngine.strRND(_003F318_003F, _003F319_003F);
			return _003F282_003F(text, cONTROL_MASK, true);
			IL_00f1:
			goto IL_0190;
			IL_0190:
			string cONTROL_MASK2 = _003F414_003F.CONTROL_MASK;
			text = oLogicEngine.stringResult(_003F414_003F.FILLDATA);
			return _003F282_003F(text, cONTROL_MASK2, false);
		}

		public SurveyDetail SingleDetail(SurveyDefine _003F414_003F, List<SurveyDetail> _003F415_003F)
		{
			//IL_0192: Incompatible stack heights: 0 vs 1
			//IL_019e: Incompatible stack heights: 0 vs 2
			//IL_01ae: Incompatible stack heights: 0 vs 1
			//IL_01d3: Incompatible stack heights: 0 vs 1
			//IL_01df: Incompatible stack heights: 0 vs 2
			SurveyDetail surveyDetail = null;
			int _003F107_003F;
			object text;
			string _003F459_003F;
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				if (oFunc.LEFT(_003F414_003F.FILLDATA, 1) == _003F487_003F._003F488_003F("$"))
				{
					UDPX oFunc2 = oFunc;
					string fILLDATA = _003F414_003F.FILLDATA;
					_003F459_003F = ((UDPX)/*Error near IL_0058: Stack underflow*/).MID(fILLDATA, 1, -9999);
					List<string> list = oTitle.ParaToList(_003F459_003F, _003F487_003F._003F488_003F(";"));
					int num;
					if (list.Count != 0 && !(((List<string>)/*Error near IL_0080: Stack underflow*/)[(int)/*Error near IL_0080: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						LogicEngine oLogicEngine2 = oLogicEngine;
						string _003F374_003F = list[0];
						num = (int)((LogicEngine)/*Error near IL_00a0: Stack underflow*/).doubleResult(_003F374_003F);
					}
					else
					{
						num = 1;
					}
					_003F107_003F = num;
					if (list.Count >= 2)
					{
						bool flag = list[1] == _003F487_003F._003F488_003F("");
						if ((int)/*Error near IL_01d8: Stack underflow*/ == 0)
						{
							text = ((List<string>)/*Error near IL_00be: Stack underflow*/)[(int)/*Error near IL_00be: Stack underflow*/];
							goto IL_00cd;
						}
					}
					text = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");
					goto IL_00cd;
				}
				string[] _003F421_003F = oLogicEngine.aryCode(_003F414_003F.FILLDATA, ',');
				string str = _003F281_003F(_003F415_003F, _003F421_003F);
				str = oLogicEngine.stringCode(_003F487_003F._003F488_003F("\"ŗɅ\u034dцԩ") + str + _003F487_003F._003F488_003F("("));
				surveyDetail = _003F278_003F(_003F415_003F, str);
			}
			else
			{
				surveyDetail = _003F273_003F(_003F415_003F);
			}
			goto IL_0163;
			IL_00cd:
			_003F459_003F = (string)text;
			string _003F90_003F = oLogicEngine.stringCode(_003F459_003F);
			int _003F102_003F = oFunc.StringToInt(_003F90_003F);
			_003F102_003F = oFunc.MOD(_003F102_003F, _003F415_003F.Count(), _003F107_003F) - 1;
			surveyDetail = _003F415_003F[_003F102_003F];
			goto IL_0163;
			IL_0163:
			if (surveyDetail == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, _003F414_003F.QUESTION_NAME);
			}
			return surveyDetail;
		}

		public Button SingleButton(SurveyDefine _003F414_003F, List<Button> _003F416_003F)
		{
			//IL_0192: Incompatible stack heights: 0 vs 1
			//IL_019e: Incompatible stack heights: 0 vs 2
			//IL_01ae: Incompatible stack heights: 0 vs 1
			//IL_01d3: Incompatible stack heights: 0 vs 1
			//IL_01df: Incompatible stack heights: 0 vs 2
			Button button = null;
			int _003F107_003F;
			object text;
			string _003F459_003F;
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				if (oFunc.LEFT(_003F414_003F.FILLDATA, 1) == _003F487_003F._003F488_003F("$"))
				{
					UDPX oFunc2 = oFunc;
					string fILLDATA = _003F414_003F.FILLDATA;
					_003F459_003F = ((UDPX)/*Error near IL_0058: Stack underflow*/).MID(fILLDATA, 1, -9999);
					List<string> list = oTitle.ParaToList(_003F459_003F, _003F487_003F._003F488_003F(";"));
					int num;
					if (list.Count != 0 && !(((List<string>)/*Error near IL_0080: Stack underflow*/)[(int)/*Error near IL_0080: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						LogicEngine oLogicEngine2 = oLogicEngine;
						string _003F374_003F = list[0];
						num = (int)((LogicEngine)/*Error near IL_00a0: Stack underflow*/).doubleResult(_003F374_003F);
					}
					else
					{
						num = 1;
					}
					_003F107_003F = num;
					if (list.Count >= 2)
					{
						bool flag = list[1] == _003F487_003F._003F488_003F("");
						if ((int)/*Error near IL_01d8: Stack underflow*/ == 0)
						{
							text = ((List<string>)/*Error near IL_00be: Stack underflow*/)[(int)/*Error near IL_00be: Stack underflow*/];
							goto IL_00cd;
						}
					}
					text = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");
					goto IL_00cd;
				}
				string[] _003F421_003F = oLogicEngine.aryCode(_003F414_003F.FILLDATA, ',');
				string str = _003F280_003F(_003F416_003F, _003F421_003F);
				str = oLogicEngine.stringCode(_003F487_003F._003F488_003F("\"ŗɅ\u034dцԩ") + str + _003F487_003F._003F488_003F("("));
				button = FindButton(_003F416_003F, str);
			}
			else
			{
				button = _003F275_003F(_003F416_003F);
			}
			goto IL_0163;
			IL_00cd:
			_003F459_003F = (string)text;
			string _003F90_003F = oLogicEngine.stringCode(_003F459_003F);
			int _003F102_003F = oFunc.StringToInt(_003F90_003F);
			_003F102_003F = oFunc.MOD(_003F102_003F, _003F416_003F.Count(), _003F107_003F) - 1;
			button = _003F416_003F[_003F102_003F];
			goto IL_0163;
			IL_0163:
			if (button == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, _003F414_003F.QUESTION_NAME);
			}
			return button;
		}

		public Rectangle SingleRectangle(SurveyDefine _003F414_003F, List<Rectangle> _003F416_003F)
		{
			//IL_0192: Incompatible stack heights: 0 vs 1
			//IL_019e: Incompatible stack heights: 0 vs 2
			//IL_01ae: Incompatible stack heights: 0 vs 1
			//IL_01d3: Incompatible stack heights: 0 vs 1
			//IL_01df: Incompatible stack heights: 0 vs 2
			Rectangle rectangle = null;
			int _003F107_003F;
			object text;
			string _003F459_003F;
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				if (oFunc.LEFT(_003F414_003F.FILLDATA, 1) == _003F487_003F._003F488_003F("$"))
				{
					UDPX oFunc2 = oFunc;
					string fILLDATA = _003F414_003F.FILLDATA;
					_003F459_003F = ((UDPX)/*Error near IL_0058: Stack underflow*/).MID(fILLDATA, 1, -9999);
					List<string> list = oTitle.ParaToList(_003F459_003F, _003F487_003F._003F488_003F(";"));
					int num;
					if (list.Count != 0 && !(((List<string>)/*Error near IL_0080: Stack underflow*/)[(int)/*Error near IL_0080: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						LogicEngine oLogicEngine2 = oLogicEngine;
						string _003F374_003F = list[0];
						num = (int)((LogicEngine)/*Error near IL_00a0: Stack underflow*/).doubleResult(_003F374_003F);
					}
					else
					{
						num = 1;
					}
					_003F107_003F = num;
					if (list.Count >= 2)
					{
						bool flag = list[1] == _003F487_003F._003F488_003F("");
						if ((int)/*Error near IL_01d8: Stack underflow*/ == 0)
						{
							text = ((List<string>)/*Error near IL_00be: Stack underflow*/)[(int)/*Error near IL_00be: Stack underflow*/];
							goto IL_00cd;
						}
					}
					text = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");
					goto IL_00cd;
				}
				string[] _003F421_003F = oLogicEngine.aryCode(_003F414_003F.FILLDATA, ',');
				string str = _003F288_003F(_003F416_003F, _003F421_003F);
				str = oLogicEngine.stringCode(_003F487_003F._003F488_003F("\"ŗɅ\u034dцԩ") + str + _003F487_003F._003F488_003F("("));
				rectangle = _003F286_003F(_003F416_003F, str);
			}
			else
			{
				rectangle = _003F284_003F(_003F416_003F);
			}
			goto IL_0163;
			IL_00cd:
			_003F459_003F = (string)text;
			string _003F90_003F = oLogicEngine.stringCode(_003F459_003F);
			int _003F102_003F = oFunc.StringToInt(_003F90_003F);
			_003F102_003F = oFunc.MOD(_003F102_003F, _003F416_003F.Count(), _003F107_003F) - 1;
			rectangle = _003F416_003F[_003F102_003F];
			goto IL_0163;
			IL_0163:
			if (rectangle == null)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, _003F414_003F.QUESTION_NAME);
			}
			return rectangle;
		}

		public string CommonOther(SurveyDefine _003F414_003F, string _003F371_003F = "")
		{
			//IL_0040: Incompatible stack heights: 0 vs 1
			if (!(_003F371_003F == _003F487_003F._003F488_003F("")))
			{
				return _003F414_003F.QUESTION_NAME + _003F487_003F._003F488_003F("]ł") + _003F371_003F + _003F487_003F._003F488_003F("YŊɰ\u036bѧճ");
			}
			string text = _003F414_003F.QUESTION_NAME + _003F487_003F._003F488_003F("YŊɰ\u036bѧճ");
			return (string)/*Error near IL_0015: Stack underflow*/;
		}

		public List<Button> MultiButton(SurveyDefine _003F414_003F, List<Button> _003F416_003F, List<Button> _003F417_003F, int _003F418_003F = 0)
		{
			//IL_0201: Incompatible stack heights: 0 vs 2
			//IL_020d: Incompatible stack heights: 0 vs 2
			//IL_0229: Incompatible stack heights: 0 vs 1
			//IL_023a: Incompatible stack heights: 0 vs 2
			//IL_024b: Incompatible stack heights: 0 vs 1
			//IL_0250: Incompatible stack heights: 1 vs 0
			//IL_026f: Incompatible stack heights: 0 vs 1
			List<Button> list = new List<Button>();
			int _003F418_003F2 = _003F283_003F(_003F414_003F.MIN_COUNT, _003F414_003F.MAX_COUNT, _003F416_003F.Count, _003F418_003F, _003F414_003F.FILLDATA == _003F487_003F._003F488_003F(""));
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				if (oFunc.LEFT(_003F414_003F.FILLDATA, 1) == _003F487_003F._003F488_003F("$"))
				{
					UDPX oFunc2 = oFunc;
					string fILLDATum = _003F414_003F.FILLDATA;
					string _003F459_003F = ((UDPX)/*Error near IL_0086: Stack underflow*/).MID((string)/*Error near IL_0086: Stack underflow*/, 1, -9999);
					List<string> list2 = oTitle.ParaToList(_003F459_003F, _003F487_003F._003F488_003F(";"));
					int num;
					if (list2.Count != 0 && !(((List<string>)/*Error near IL_00ae: Stack underflow*/)[(int)/*Error near IL_00ae: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						oLogicEngine.doubleResult(list2[0]);
						num = (int)/*Error near IL_00c3: Stack underflow*/;
					}
					else
					{
						num = 1;
					}
					int _003F107_003F = num;
					string text;
					if (list2.Count >= 2 && !(((List<string>)/*Error near IL_00dc: Stack underflow*/)[(int)/*Error near IL_00dc: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						string text3 = list2[1];
					}
					else
					{
						text = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");
					}
					_003F459_003F = text;
					string _003F90_003F = oLogicEngine.stringCode(_003F459_003F);
					int _003F102_003F = oFunc.StringToInt(_003F90_003F);
					_003F102_003F = oFunc.MOD(_003F102_003F, _003F417_003F.Count(), _003F107_003F) - 1;
					list.Add(_003F417_003F[_003F102_003F]);
				}
				else
				{
					string[] _003F421_003F = oLogicEngine.aryCode(_003F414_003F.FILLDATA, ',');
					string text2 = _003F280_003F(_003F417_003F, _003F421_003F);
					text2 = oLogicEngine.stringCode(_003F487_003F._003F488_003F("\"ŗɅ\u034dцԩ") + text2 + _003F487_003F._003F488_003F(";") + _003F418_003F2.ToString() + _003F487_003F._003F488_003F("("));
					list = _003F277_003F(_003F417_003F, text2);
				}
			}
			else
			{
				list = _003F276_003F(_003F416_003F, _003F418_003F2);
			}
			if (list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, _003F414_003F.QUESTION_NAME);
			}
			return list;
		}

		public List<Rectangle> MultiRectangle(SurveyDefine _003F414_003F, List<Rectangle> _003F416_003F, int _003F418_003F = 0)
		{
			//IL_01ec: Incompatible stack heights: 0 vs 2
			//IL_01f8: Incompatible stack heights: 0 vs 2
			//IL_0214: Incompatible stack heights: 0 vs 1
			//IL_0225: Incompatible stack heights: 0 vs 2
			//IL_0236: Incompatible stack heights: 0 vs 1
			//IL_023b: Incompatible stack heights: 1 vs 0
			//IL_025a: Incompatible stack heights: 0 vs 1
			List<Rectangle> list = new List<Rectangle>();
			int _003F418_003F2 = _003F283_003F(_003F414_003F.MIN_COUNT, _003F414_003F.MAX_COUNT, _003F416_003F.Count, _003F418_003F, true);
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				if (oFunc.LEFT(_003F414_003F.FILLDATA, 1) == _003F487_003F._003F488_003F("$"))
				{
					UDPX oFunc2 = oFunc;
					string fILLDATum = _003F414_003F.FILLDATA;
					string _003F459_003F = ((UDPX)/*Error near IL_0071: Stack underflow*/).MID((string)/*Error near IL_0071: Stack underflow*/, 1, -9999);
					List<string> list2 = oTitle.ParaToList(_003F459_003F, _003F487_003F._003F488_003F(";"));
					int num;
					if (list2.Count != 0 && !(((List<string>)/*Error near IL_0099: Stack underflow*/)[(int)/*Error near IL_0099: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						oLogicEngine.doubleResult(list2[0]);
						num = (int)/*Error near IL_00ae: Stack underflow*/;
					}
					else
					{
						num = 1;
					}
					int _003F107_003F = num;
					string text;
					if (list2.Count >= 2 && !(((List<string>)/*Error near IL_00c7: Stack underflow*/)[(int)/*Error near IL_00c7: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						string text3 = list2[1];
					}
					else
					{
						text = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");
					}
					_003F459_003F = text;
					string _003F90_003F = oLogicEngine.stringCode(_003F459_003F);
					int _003F102_003F = oFunc.StringToInt(_003F90_003F);
					_003F102_003F = oFunc.MOD(_003F102_003F, _003F416_003F.Count(), _003F107_003F) - 1;
					list.Add(_003F416_003F[_003F102_003F]);
				}
				else
				{
					string[] _003F421_003F = oLogicEngine.aryCode(_003F414_003F.FILLDATA, ',');
					string text2 = _003F288_003F(_003F416_003F, _003F421_003F);
					text2 = oLogicEngine.stringCode(_003F487_003F._003F488_003F("\"ŗɅ\u034dцԩ") + text2 + _003F487_003F._003F488_003F(";") + _003F418_003F2.ToString() + _003F487_003F._003F488_003F("("));
					list = _003F287_003F(_003F416_003F, text2);
				}
			}
			else
			{
				list = _003F285_003F(_003F416_003F, _003F418_003F2);
			}
			if (list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, _003F414_003F.QUESTION_NAME);
			}
			return list;
		}

		public List<SurveyDetail> MultiDetail(SurveyDefine _003F414_003F, List<SurveyDetail> _003F415_003F, int _003F418_003F = 0)
		{
			//IL_01f1: Incompatible stack heights: 0 vs 2
			//IL_01fd: Incompatible stack heights: 0 vs 2
			//IL_0219: Incompatible stack heights: 0 vs 1
			//IL_022a: Incompatible stack heights: 0 vs 2
			//IL_023b: Incompatible stack heights: 0 vs 1
			//IL_0240: Incompatible stack heights: 1 vs 0
			//IL_0255: Incompatible stack heights: 0 vs 1
			List<SurveyDetail> list = null;
			int _003F418_003F2 = _003F283_003F(_003F414_003F.MIN_COUNT, _003F414_003F.MAX_COUNT, _003F415_003F.Count, _003F418_003F, true);
			if (!(_003F414_003F.FILLDATA == _003F487_003F._003F488_003F("")))
			{
				if (oFunc.LEFT(_003F414_003F.FILLDATA, 1) == _003F487_003F._003F488_003F("$"))
				{
					UDPX oFunc2 = oFunc;
					string fILLDATum = _003F414_003F.FILLDATA;
					string _003F459_003F = ((UDPX)/*Error near IL_006d: Stack underflow*/).MID((string)/*Error near IL_006d: Stack underflow*/, 1, -9999);
					List<string> list2 = oTitle.ParaToList(_003F459_003F, _003F487_003F._003F488_003F(";"));
					int num;
					if (list2.Count != 0 && !(((List<string>)/*Error near IL_0095: Stack underflow*/)[(int)/*Error near IL_0095: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						oLogicEngine.doubleResult(list2[0]);
						num = (int)/*Error near IL_00aa: Stack underflow*/;
					}
					else
					{
						num = 1;
					}
					int _003F107_003F = num;
					string text;
					if (list2.Count >= 2 && !(((List<string>)/*Error near IL_00c3: Stack underflow*/)[(int)/*Error near IL_00c3: Stack underflow*/] == _003F487_003F._003F488_003F("")))
					{
						string text3 = list2[1];
					}
					else
					{
						text = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");
					}
					_003F459_003F = text;
					string _003F90_003F = oLogicEngine.stringCode(_003F459_003F);
					int _003F102_003F = oFunc.StringToInt(_003F90_003F);
					_003F102_003F = oFunc.MOD(_003F102_003F, _003F415_003F.Count(), _003F107_003F) - 1;
					list.Add(_003F415_003F[_003F102_003F]);
				}
				else
				{
					string[] _003F421_003F = oLogicEngine.aryCode(_003F414_003F.FILLDATA, ',');
					string text2 = _003F281_003F(_003F415_003F, _003F421_003F);
					text2 = oLogicEngine.stringCode(_003F487_003F._003F488_003F("\"ŗɅ\u034dцԩ") + text2 + _003F487_003F._003F488_003F(";") + _003F418_003F2.ToString() + _003F487_003F._003F488_003F("("));
					list = _003F279_003F(_003F415_003F, text2);
				}
			}
			else
			{
				list = _003F274_003F(_003F415_003F, _003F418_003F2);
			}
			if (list != null)
			{
				int count = list.Count;
				if ((int)/*Error near IL_025a: Stack underflow*/ != 0)
				{
					goto IL_0265;
				}
			}
			MessageBox.Show(SurveyMsg.MsgFrmNoCodeForSelect, _003F414_003F.QUESTION_NAME);
			goto IL_0265;
			IL_0265:
			return list;
		}

		public string[] RecodeFill(SurveyDefine _003F414_003F)
		{
			return oLogicEngine.aryCode(_003F414_003F.FILLDATA, ',');
		}

		public string[] RecodeSingle(SurveyDefine _003F414_003F, List<SurveyDetail> _003F415_003F)
		{
			//IL_0116: Incompatible stack heights: 0 vs 3
			//IL_012c: Incompatible stack heights: 0 vs 2
			//IL_013c: Incompatible stack heights: 0 vs 1
			//IL_0161: Incompatible stack heights: 0 vs 1
			//IL_016d: Incompatible stack heights: 0 vs 2
			if (!(oFunc.LEFT(_003F414_003F.FILLDATA, 1) == _003F487_003F._003F488_003F("$")))
			{
				return oLogicEngine.aryCode(_003F414_003F.FILLDATA, ',');
			}
			UDPX oFunc2 = oFunc;
			string fILLDATum = _003F414_003F.FILLDATA;
			string _003F459_003F = ((UDPX)/*Error near IL_0030: Stack underflow*/).MID((string)/*Error near IL_0030: Stack underflow*/, (int)/*Error near IL_0030: Stack underflow*/, -9999);
			List<string> list = oTitle.ParaToList(_003F459_003F, _003F487_003F._003F488_003F(";"));
			int num;
			if (list.Count != 0)
			{
				string text2 = list[0];
				string b = _003F487_003F._003F488_003F((string)/*Error near IL_0058: Stack underflow*/);
				if (!((string)/*Error near IL_005d: Stack underflow*/ == b))
				{
					LogicEngine oLogicEngine2 = oLogicEngine;
					string _003F374_003F = list[0];
					num = (int)((LogicEngine)/*Error near IL_006e: Stack underflow*/).doubleResult(_003F374_003F);
					goto IL_0075;
				}
			}
			num = 1;
			goto IL_0075;
			IL_0075:
			int _003F107_003F = num;
			object text;
			if (list.Count >= 2)
			{
				bool flag = list[1] == _003F487_003F._003F488_003F("");
				if ((int)/*Error near IL_0166: Stack underflow*/ == 0)
				{
					text = ((List<string>)/*Error near IL_008c: Stack underflow*/)[(int)/*Error near IL_008c: Stack underflow*/];
					goto IL_009b;
				}
			}
			text = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");
			goto IL_009b;
			IL_009b:
			_003F459_003F = (string)text;
			string _003F90_003F = oLogicEngine.stringCode(_003F459_003F);
			int _003F102_003F = oFunc.StringToInt(_003F90_003F);
			_003F102_003F = oFunc.MOD(_003F102_003F, _003F415_003F.Count(), _003F107_003F) - 1;
			return _003F415_003F[_003F102_003F].CODE.Split(',');
		}

		private SurveyDetail _003F273_003F(List<SurveyDetail> _003F415_003F)
		{
			SurveyDetail result = new SurveyDetail();
			try
			{
				int index = oRandomEngine.intRND(0, _003F415_003F.Count - 1);
				result = _003F415_003F[index];
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}

		private List<SurveyDetail> _003F274_003F(List<SurveyDetail> _003F415_003F, int _003F418_003F)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			try
			{
				if (_003F415_003F.Count < _003F418_003F)
				{
					_003F418_003F = _003F415_003F.Count;
				}
				foreach (int item in oRandomEngine.intRandomList(_003F418_003F, 1, _003F415_003F.Count))
				{
					list.Add(_003F415_003F[item - 1]);
				}
				return list;
			}
			catch (Exception)
			{
				return list;
			}
		}

		private Button _003F275_003F(List<Button> _003F416_003F)
		{
			Button result = null;
			try
			{
				int index = oRandomEngine.intRND(0, _003F416_003F.Count - 1);
				result = _003F416_003F[index];
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}

		private List<Button> _003F276_003F(List<Button> _003F416_003F, int _003F418_003F)
		{
			List<Button> list = new List<Button>();
			try
			{
				if (_003F416_003F.Count < _003F418_003F)
				{
					_003F418_003F = _003F416_003F.Count;
				}
				foreach (int item in oRandomEngine.intRandomList(_003F418_003F, 1, _003F416_003F.Count))
				{
					list.Add(_003F416_003F[item - 1]);
				}
				return list;
			}
			catch (Exception)
			{
				return list;
			}
		}

		public Button FindButton(List<Button> _003F416_003F, string _003F419_003F)
		{
			foreach (Button item in _003F416_003F)
			{
				if (item.Name.Substring(2) == _003F419_003F)
				{
					return item;
				}
			}
			return null;
		}

		private List<Button> _003F277_003F(List<Button> _003F416_003F, string _003F420_003F)
		{
			List<Button> list = new List<Button>();
			List<string> list2 = oFunc.StringToList(_003F420_003F, _003F487_003F._003F488_003F("-"));
			foreach (Button item2 in _003F416_003F)
			{
				string item = item2.Name.Substring(2);
				if (list2.Contains(item))
				{
					list.Add(item2);
				}
			}
			return list;
		}

		private SurveyDetail _003F278_003F(List<SurveyDetail> _003F415_003F, string _003F419_003F)
		{
			foreach (SurveyDetail item in _003F415_003F)
			{
				if (item.CODE == _003F419_003F)
				{
					return item;
				}
			}
			return null;
		}

		private List<SurveyDetail> _003F279_003F(List<SurveyDetail> _003F415_003F, string _003F420_003F)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			List<string> list2 = oFunc.StringToList(_003F420_003F, _003F487_003F._003F488_003F("-"));
			foreach (SurveyDetail item in _003F415_003F)
			{
				string cODE = item.CODE;
				if (list2.Contains(cODE))
				{
					list.Add(item);
				}
			}
			return list;
		}

		private string _003F280_003F(List<Button> _003F416_003F, string[] _003F421_003F)
		{
			string text = _003F487_003F._003F488_003F("");
			foreach (string text2 in _003F421_003F)
			{
				foreach (Button item in _003F416_003F)
				{
					if (item.Name.Substring(2) == text2)
					{
						text = text + _003F487_003F._003F488_003F("-") + text2;
						break;
					}
				}
			}
			if (text != _003F487_003F._003F488_003F(""))
			{
				text = oFunc.MID(text, 1, -9999);
			}
			return text;
		}

		private string _003F281_003F(List<SurveyDetail> _003F415_003F, string[] _003F421_003F)
		{
			string text = _003F487_003F._003F488_003F("");
			foreach (string text2 in _003F421_003F)
			{
				foreach (SurveyDetail item in _003F415_003F)
				{
					if (item.CODE == text2)
					{
						text = text + _003F487_003F._003F488_003F("-") + text2;
						break;
					}
				}
			}
			if (text != _003F487_003F._003F488_003F(""))
			{
				text = oFunc.MID(text, 1, -9999);
			}
			return text;
		}

		private string _003F282_003F(string _003F422_003F, string _003F423_003F, bool _003F424_003F = false)
		{
			//IL_04f4: Incompatible stack heights: 0 vs 3
			//IL_050a: Incompatible stack heights: 0 vs 3
			//IL_051a: Incompatible stack heights: 0 vs 1
			//IL_054a: Incompatible stack heights: 0 vs 1
			//IL_055a: Incompatible stack heights: 0 vs 1
			//IL_0570: Incompatible stack heights: 0 vs 2
			//IL_0580: Incompatible stack heights: 0 vs 1
			//IL_0591: Incompatible stack heights: 0 vs 2
			//IL_05ac: Incompatible stack heights: 0 vs 2
			//IL_05ca: Incompatible stack heights: 0 vs 1
			//IL_05d6: Incompatible stack heights: 0 vs 2
			//IL_05ea: Incompatible stack heights: 0 vs 1
			//IL_05ef: Incompatible stack heights: 1 vs 0
			//IL_0605: Incompatible stack heights: 0 vs 3
			//IL_0610: Incompatible stack heights: 0 vs 1
			//IL_0627: Incompatible stack heights: 0 vs 2
			//IL_0637: Incompatible stack heights: 0 vs 1
			//IL_064d: Incompatible stack heights: 0 vs 2
			//IL_065d: Incompatible stack heights: 0 vs 1
			//IL_066d: Incompatible stack heights: 0 vs 1
			//IL_067d: Incompatible stack heights: 0 vs 1
			//IL_0693: Incompatible stack heights: 0 vs 2
			//IL_06a8: Incompatible stack heights: 0 vs 1
			//IL_06bf: Incompatible stack heights: 0 vs 3
			//IL_06da: Incompatible stack heights: 0 vs 3
			string result = _003F422_003F;
			if (_003F423_003F != _003F487_003F._003F488_003F(""))
			{
				BoldTitle oTitle2 = oTitle;
				_003F487_003F._003F488_003F("-");
				List<string> list = ((BoldTitle)/*Error near IL_001c: Stack underflow*/).ParaToList((string)/*Error near IL_001c: Stack underflow*/, (string)/*Error near IL_001c: Stack underflow*/);
				List<string> list2 = oTitle.ParaToList(_003F422_003F, _003F487_003F._003F488_003F("/"));
				string text = list[0];
				string text2 = list2[0];
				if (text.Contains(_003F487_003F._003F488_003F("\u007f")))
				{
					BoldTitle oTitle3 = oTitle;
					string _003F460_003F = _003F487_003F._003F488_003F((string)/*Error near IL_005f: Stack underflow*/);
					List<string> list3 = ((BoldTitle)/*Error near IL_0064: Stack underflow*/).ParaToList((string)/*Error near IL_0064: Stack underflow*/, _003F460_003F);
					int num = oFunc.StringToInt(list3[0]);
					int num2 = oFunc.StringToInt(list3[1]);
					if (num > text2.Length)
					{
						UDPX oFunc2 = oFunc;
						string _003F90_003F = text2;
						string _003F128_003F = _003F487_003F._003F488_003F("1");
						text2 = ((UDPX)/*Error near IL_00b2: Stack underflow*/).FillString(_003F90_003F, _003F128_003F, num, true);
					}
					else if (num2 < text2.Length && _003F424_003F)
					{
						text2 = oFunc.RIGHT(text2, num2);
					}
				}
				else
				{
					string a = oFunc.LEFT(text, 2);
					if (!(a == _003F487_003F._003F488_003F(">ļ")))
					{
						if (a == _003F487_003F._003F488_003F("<ļ"))
						{
							UDPX oFunc3 = oFunc;
							string _003F90_003F2 = ((AutoFill)/*Error near IL_0144: Stack underflow*/).oFunc.MID(text, 1, -9999);
							int _003F126_003F = ((UDPX)/*Error near IL_0155: Stack underflow*/).StringToInt(_003F90_003F2);
							text2 = oFunc.FillString(text2, _003F487_003F._003F488_003F("1"), _003F126_003F, true);
						}
						else
						{
							a = oFunc.LEFT(text, 1);
							if (!(a == _003F487_003F._003F488_003F("=")))
							{
								if (a == _003F487_003F._003F488_003F("?"))
								{
									UDPX oFunc4 = oFunc;
									UDPX oFunc5 = oFunc;
									string _003F90_003F3 = ((UDPX)/*Error near IL_01f4: Stack underflow*/).MID(text, 1, -9999);
									int _003F126_003F2 = ((UDPX)/*Error near IL_01f9: Stack underflow*/).StringToInt(_003F90_003F3) + 1;
									text2 = oFunc.FillString(text2, _003F487_003F._003F488_003F("1"), _003F126_003F2, true);
								}
								else
								{
									int _003F126_003F3 = oFunc.StringToInt(text);
									text2 = oFunc.FillString(text2, _003F487_003F._003F488_003F("1"), _003F126_003F3, true);
									if (_003F424_003F)
									{
										oFunc.RIGHT(text2, _003F126_003F3);
										text2 = (string)/*Error near IL_0250: Stack underflow*/;
									}
								}
							}
							else if ((int)/*Error near IL_0585: Stack underflow*/ != 0)
							{
								UDPX oFunc6 = oFunc;
								string _003F90_003F4 = ((AutoFill)/*Error near IL_01a7: Stack underflow*/).oFunc.MID(text, 1, -9999);
								int _003F126_003F4 = ((UDPX)/*Error near IL_01b8: Stack underflow*/).StringToInt(_003F90_003F4) - 1;
								text2 = oFunc.RIGHT(text2, _003F126_003F4);
							}
						}
					}
					else if ((int)/*Error near IL_054f: Stack underflow*/ != 0)
					{
						UDPX oFunc7 = oFunc;
						string _003F90_003F5 = oFunc.MID(text, 1, -9999);
						int _003F126_003F5 = ((UDPX)/*Error near IL_0111: Stack underflow*/).StringToInt(_003F90_003F5);
						text2 = oFunc.RIGHT(text2, _003F126_003F5);
					}
				}
				result = text2;
				if (list.Count > 1)
				{
					text = ((List<string>)/*Error near IL_0264: Stack underflow*/)[(int)/*Error near IL_0264: Stack underflow*/];
					string text3;
					if (list2.Count > 1)
					{
						text3 = list2[1];
					}
					else
					{
						_003F487_003F._003F488_003F("");
					}
					text2 = text3;
					if (text.Contains(_003F487_003F._003F488_003F("\u007f")))
					{
						BoldTitle oTitle4 = oTitle;
						string _003F460_003F2 = _003F487_003F._003F488_003F((string)/*Error near IL_0299: Stack underflow*/);
						List<string> list4 = ((BoldTitle)/*Error near IL_029e: Stack underflow*/).ParaToList((string)/*Error near IL_029e: Stack underflow*/, _003F460_003F2);
						int num3 = oFunc.StringToInt(list4[0]);
						int num4 = oFunc.StringToInt(list4[1]);
						if (num3 > text2.Length)
						{
							text2 = ((AutoFill)/*Error near IL_02dd: Stack underflow*/).oFunc.FillString(text2, _003F487_003F._003F488_003F("1"), num3, false);
						}
						else if (num4 < text2.Length && _003F424_003F)
						{
							UDPX oFunc8 = oFunc;
							text2 = ((UDPX)/*Error near IL_0311: Stack underflow*/).LEFT((string)/*Error near IL_0311: Stack underflow*/, num4);
						}
					}
					else
					{
						string a2 = oFunc.LEFT(text, 2);
						if (!(a2 == _003F487_003F._003F488_003F(">ļ")))
						{
							if (a2 == _003F487_003F._003F488_003F("<ļ"))
							{
								int _003F126_003F6 = ((AutoFill)/*Error near IL_0386: Stack underflow*/).oFunc.StringToInt(oFunc.MID(text, 1, -9999));
								text2 = oFunc.FillString(text2, _003F487_003F._003F488_003F("1"), _003F126_003F6, false);
							}
							else
							{
								a2 = oFunc.LEFT(text, 1);
								if (!(a2 == _003F487_003F._003F488_003F("=")))
								{
									if (a2 == _003F487_003F._003F488_003F("?"))
									{
										UDPX oFunc9 = oFunc;
										string _003F90_003F6 = ((AutoFill)/*Error near IL_0436: Stack underflow*/).oFunc.MID(text, 1, -9999);
										int _003F126_003F7 = ((UDPX)/*Error near IL_0447: Stack underflow*/).StringToInt(_003F90_003F6) + 1;
										text2 = oFunc.FillString(text2, _003F487_003F._003F488_003F("1"), _003F126_003F7, false);
									}
									else
									{
										int _003F126_003F8 = oFunc.StringToInt(text);
										text2 = oFunc.FillString(text2, _003F487_003F._003F488_003F("1"), _003F126_003F8, false);
										if (_003F424_003F)
										{
											UDPX oFunc10 = oFunc;
											text2 = ((UDPX)/*Error near IL_04a5: Stack underflow*/).LEFT(text2, _003F126_003F8);
										}
									}
								}
								else if ((int)/*Error near IL_0672: Stack underflow*/ != 0)
								{
									UDPX oFunc11 = oFunc;
									string _003F90_003F7 = oFunc.MID(text, 1, -9999);
									int _003F126_003F9 = ((UDPX)/*Error near IL_0401: Stack underflow*/).StringToInt(_003F90_003F7) - 1;
									text2 = oFunc.LEFT(text2, _003F126_003F9);
								}
							}
						}
						else if ((int)/*Error near IL_063c: Stack underflow*/ != 0)
						{
							UDPX oFunc12 = oFunc;
							UDPX oFunc13 = oFunc;
							string _003F90_003F8 = ((UDPX)/*Error near IL_034e: Stack underflow*/).MID(text, 1, -9999);
							int _003F126_003F10 = ((UDPX)/*Error near IL_0353: Stack underflow*/).StringToInt(_003F90_003F8);
							text2 = oFunc.LEFT(text2, _003F126_003F10);
						}
					}
					if (text2 != _003F487_003F._003F488_003F(""))
					{
						_003F487_003F._003F488_003F("/");
						result = (string)/*Error near IL_04c2: Stack underflow*/ + (string)/*Error near IL_04c2: Stack underflow*/ + (string)/*Error near IL_04c2: Stack underflow*/;
					}
				}
				else if (list2.Count > 1)
				{
					_003F487_003F._003F488_003F("/");
					result = string.Concat(str2: ((List<string>)/*Error near IL_06e0: Stack underflow*/)[1], str0: (string)/*Error near IL_06e5: Stack underflow*/, str1: (string)/*Error near IL_06e5: Stack underflow*/);
				}
			}
			return result;
		}

		private int _003F283_003F(int _003F425_003F, int _003F426_003F, int _003F427_003F, int _003F418_003F = 0, bool _003F428_003F = true)
		{
			//IL_008d: Incompatible stack heights: 0 vs 1
			//IL_0092: Incompatible stack heights: 1 vs 0
			//IL_009f: Incompatible stack heights: 0 vs 1
			//IL_00ab: Incompatible stack heights: 0 vs 2
			//IL_00ce: Incompatible stack heights: 0 vs 2
			//IL_00da: Incompatible stack heights: 0 vs 1
			//IL_00e5: Incompatible stack heights: 0 vs 1
			//IL_00ea: Incompatible stack heights: 1 vs 0
			//IL_00fb: Incompatible stack heights: 0 vs 1
			//IL_0106: Incompatible stack heights: 0 vs 1
			//IL_010b: Incompatible stack heights: 1 vs 0
			int result = 0;
			if (_003F425_003F > 1)
			{
			}
			int num = _003F418_003F;
			if (_003F418_003F == 0)
			{
				num = _003F426_003F;
				if ((int)/*Error near IL_00a4: Stack underflow*/ == 0 || /*Error near IL_00b0: Stack underflow*/> /*Error near IL_00b0: Stack underflow*/)
				{
					num = _003F427_003F;
				}
			}
			if (num > 0)
			{
				string fillMode = SurveyHelper.FillMode;
				_003F487_003F._003F488_003F("0");
				if ((string)/*Error near IL_0032: Stack underflow*/ == (string)/*Error near IL_0032: Stack underflow*/)
				{
					int num2;
					if ((int)/*Error near IL_00df: Stack underflow*/ != 0)
					{
						num2 = _003F425_003F;
					}
					result = num2;
				}
				else if (SurveyHelper.FillMode == _003F487_003F._003F488_003F("3"))
				{
					int num3;
					if ((int)/*Error near IL_0100: Stack underflow*/ != 0)
					{
						num3 = oRandomEngine.intRND(_003F425_003F, num);
					}
					result = num3;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		private Rectangle _003F284_003F(List<Rectangle> _003F416_003F)
		{
			Rectangle result = null;
			try
			{
				int index = oRandomEngine.intRND(0, _003F416_003F.Count - 1);
				result = _003F416_003F[index];
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}

		private List<Rectangle> _003F285_003F(List<Rectangle> _003F416_003F, int _003F418_003F)
		{
			List<Rectangle> list = new List<Rectangle>();
			try
			{
				if (_003F416_003F.Count < _003F418_003F)
				{
					_003F418_003F = _003F416_003F.Count;
				}
				foreach (int item in oRandomEngine.intRandomList(_003F418_003F, 1, _003F416_003F.Count))
				{
					list.Add(_003F416_003F[item - 1]);
				}
				return list;
			}
			catch (Exception)
			{
				return list;
			}
		}

		private Rectangle _003F286_003F(List<Rectangle> _003F416_003F, string _003F419_003F)
		{
			foreach (Rectangle item in _003F416_003F)
			{
				if (item.Name.Substring(2) == _003F419_003F)
				{
					return item;
				}
			}
			return null;
		}

		private List<Rectangle> _003F287_003F(List<Rectangle> _003F416_003F, string _003F420_003F)
		{
			List<Rectangle> list = new List<Rectangle>();
			List<string> list2 = oFunc.StringToList(_003F420_003F, _003F487_003F._003F488_003F("-"));
			foreach (Rectangle item2 in _003F416_003F)
			{
				string item = item2.Name.Substring(2);
				if (list2.Contains(item))
				{
					list.Add(item2);
				}
			}
			return list;
		}

		private string _003F288_003F(List<Rectangle> _003F416_003F, string[] _003F421_003F)
		{
			string text = _003F487_003F._003F488_003F("");
			foreach (string text2 in _003F421_003F)
			{
				foreach (Rectangle item in _003F416_003F)
				{
					if (item.Name.Substring(2) == text2)
					{
						text = text + _003F487_003F._003F488_003F("-") + text2;
						break;
					}
				}
			}
			if (text != _003F487_003F._003F488_003F(""))
			{
				text = oFunc.MID(text, 1, -9999);
			}
			return text;
		}
	}
}
