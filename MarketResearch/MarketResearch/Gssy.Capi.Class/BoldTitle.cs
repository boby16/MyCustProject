using Gssy.Capi.BIZ;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Gssy.Capi.Class
{
	public class BoldTitle : Page
	{
		private UDPX oFunc = new UDPX();

		public string FullTitle
		{
			get;
			set;
		}

		public int BoldCount
		{
			get;
			set;
		}

		public string SpanString1
		{
			get;
			set;
		}

		public string BoldString1
		{
			get;
			set;
		}

		public string SpanString2
		{
			get;
			set;
		}

		public string BoldString2
		{
			get;
			set;
		}

		public string SpanString3
		{
			get;
			set;
		}

		public string BoldString3
		{
			get;
			set;
		}

		public string SpanString4
		{
			get;
			set;
		}

		public List<classHtmlText> lSpan
		{
			get;
			set;
		}

		public void SpanTitle(string _003F397_003F = "", string _003F452_003F = "", string _003F453_003F = "", string _003F454_003F = "")
		{
			string text = _003F452_003F.Replace(_003F487_003F._003F488_003F("$ŝɆ\u034dї՛\u065c"), SurveyHelper.SurveyCity);
			text = text.Replace(_003F487_003F._003F488_003F("-Ŗɟ\u035eј՟\u064dݞ\u0859\u0946\u0a4b\u0b47\u0c47\u0d5c"), SurveyHelper.SurveyID);
			LogicEngine logicEngine = new LogicEngine();
			logicEngine.SurveyID = SurveyHelper.SurveyID;
			logicEngine.CircleACode = SurveyHelper.CircleACode;
			logicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
			logicEngine.CircleACount = SurveyHelper.CircleACount;
			logicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			logicEngine.CircleBCode = SurveyHelper.CircleBCode;
			logicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
			logicEngine.CircleBCount = SurveyHelper.CircleBCount;
			logicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			lSpan = logicEngine.listShowText(text);
		}

		public string ReplaceABTitle(string _003F452_003F)
		{
			string text = _003F452_003F.Replace(_003F487_003F._003F488_003F("$ŝɆ\u034dї՛\u065c"), SurveyHelper.SurveyCity);
			text = text.Replace(_003F487_003F._003F488_003F("-Ŗɟ\u035eј՟\u064dݞ\u0859\u0946\u0a4b\u0b47\u0c47\u0d5c"), SurveyHelper.SurveyID);
			return new LogicEngine
			{
				SurveyID = SurveyHelper.SurveyID,
				CircleACode = SurveyHelper.CircleACode,
				CircleACodeText = SurveyHelper.CircleACodeText,
				CircleACount = SurveyHelper.CircleACount,
				CircleACurrent = SurveyHelper.CircleACurrent,
				CircleBCode = SurveyHelper.CircleBCode,
				CircleBCodeText = SurveyHelper.CircleBCodeText,
				CircleBCount = SurveyHelper.CircleBCount,
				CircleBCurrent = SurveyHelper.CircleBCurrent
			}.strShowText(text, true);
		}

		public void SetTextBlock(TextBlock _003F455_003F, string _003F452_003F, int _003F456_003F = 0, string _003F457_003F = "", bool _003F458_003F = true)
		{
			_003F455_003F.Text = _003F487_003F._003F488_003F("");
			if (_003F452_003F != _003F487_003F._003F488_003F(""))
			{
				if (_003F456_003F != 0)
				{
					_003F455_003F.FontSize = (double)_003F456_003F;
				}
				if (oFunc.LEFT(_003F452_003F, 1) == _003F487_003F._003F488_003F("=") && oFunc.LEFT(_003F452_003F, 3) != _003F487_003F._003F488_003F("?ŀȿ"))
				{
					int num = _003F452_003F.IndexOf(_003F487_003F._003F488_003F("?"));
					if (num > -1)
					{
						string _003F90_003F = oFunc.MID(_003F452_003F, 1, num - 1).ToUpper();
						_003F452_003F = oFunc.MID(_003F452_003F, num + 1, -9999);
						if (_003F452_003F != _003F487_003F._003F488_003F(""))
						{
							string text = oFunc.MID(_003F90_003F, 1, -9999);
							_003F90_003F = oFunc.LEFT(_003F90_003F, 1);
							if (_003F90_003F == _003F487_003F._003F488_003F("M"))
							{
								_003F455_003F.HorizontalAlignment = HorizontalAlignment.Left;
							}
							else if (_003F90_003F == _003F487_003F._003F488_003F("S"))
							{
								_003F455_003F.HorizontalAlignment = HorizontalAlignment.Right;
							}
							else if (_003F90_003F == _003F487_003F._003F488_003F("B"))
							{
								_003F455_003F.HorizontalAlignment = HorizontalAlignment.Center;
							}
							else if (_003F90_003F == _003F487_003F._003F488_003F("U"))
							{
								_003F455_003F.VerticalAlignment = VerticalAlignment.Top;
							}
							else if (_003F90_003F == _003F487_003F._003F488_003F("L"))
							{
								_003F455_003F.VerticalAlignment = VerticalAlignment.Bottom;
							}
							else
							{
								text = _003F90_003F + text;
							}
							double num2 = oFunc.StringToDouble(text);
							if (num2 != 0.0)
							{
								_003F455_003F.FontSize = num2;
							}
						}
					}
				}
				if (_003F452_003F != _003F487_003F._003F488_003F(""))
				{
					SpanTitle(_003F487_003F._003F488_003F(""), _003F452_003F, _003F487_003F._003F488_003F(""), _003F487_003F._003F488_003F(""));
					foreach (classHtmlText item in lSpan)
					{
						if (item.TitleTextType == _003F487_003F._003F488_003F("?ŀȿ"))
						{
							Span span = new Span();
							span.Inlines.Add(new Run(item.TitleText));
							span.Foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
							span.FontWeight = FontWeights.Bold;
							_003F455_003F.Inlines.Add(span);
						}
						else
						{
							Span span2 = new Span();
							span2.Inlines.Add(new Run(item.TitleText));
							_003F455_003F.Inlines.Add(span2);
						}
					}
					_003F455_003F.Visibility = Visibility.Visible;
					if (_003F457_003F != _003F487_003F._003F488_003F(""))
					{
						double num3 = oFunc.StringToDouble(_003F457_003F);
						if (num3 != 0.0)
						{
							_003F455_003F.Width = num3;
						}
					}
				}
			}
			if (_003F452_003F == _003F487_003F._003F488_003F("") && _003F458_003F)
			{
				_003F455_003F.Height = 0.0;
				_003F455_003F.Width = 0.0;
				_003F455_003F.Visibility = Visibility.Collapsed;
			}
		}

		public int TakeFontSize(string _003F452_003F)
		{
			//IL_0053: Incompatible stack heights: 0 vs 1
			//IL_006d: Incompatible stack heights: 0 vs 1
			//IL_007e: Incompatible stack heights: 0 vs 2
			int result = 0;
			if (_003F452_003F != _003F487_003F._003F488_003F(""))
			{
				UDPX oFunc2 = oFunc;
				if (((UDPX)/*Error near IL_001e: Stack underflow*/).LEFT(_003F452_003F, 1) == _003F487_003F._003F488_003F("="))
				{
					_003F452_003F.IndexOf(_003F487_003F._003F488_003F("?"));
					int num = (int)/*Error near IL_0033: Stack underflow*/;
					if (num > -1)
					{
						UDPX oFunc3 = oFunc;
						string _003F90_003F = ((UDPX)/*Error near IL_0083: Stack underflow*/).MID((string)/*Error near IL_0083: Stack underflow*/, 1, num - 1).ToUpper();
						result = oFunc.StringToInt(_003F90_003F);
					}
				}
			}
			return result;
		}

		public string TakeText(string _003F452_003F)
		{
			string text = _003F487_003F._003F488_003F("");
			List<string> list = ParaToList(_003F452_003F, _003F487_003F._003F488_003F("?"));
			bool flag = true;
			foreach (string item in list)
			{
				if (flag & (oFunc.LEFT(item, 1) == _003F487_003F._003F488_003F("=")))
				{
					string text2 = oFunc.MID(item, 1, -9999).ToUpper();
					if (!oFunc.isNumberic(text2) && !(text2 == _003F487_003F._003F488_003F("B")) && !(text2 == _003F487_003F._003F488_003F("M")) && !(text2 == _003F487_003F._003F488_003F("S")) && !(text2 == _003F487_003F._003F488_003F("U")) && !(text2 == _003F487_003F._003F488_003F("L")))
					{
						flag = false;
						text = text + item + _003F487_003F._003F488_003F("?");
					}
				}
				else
				{
					flag = false;
					text += item;
				}
			}
			return text;
		}

		public void DealBoldString(string _003F452_003F)
		{
			//IL_01d9: Incompatible stack heights: 0 vs 2
			//IL_01eb: Incompatible stack heights: 0 vs 1
			FullTitle = _003F452_003F;
			BoldCount = 0;
			SpanString1 = _003F487_003F._003F488_003F("");
			BoldString1 = _003F487_003F._003F488_003F("");
			SpanString2 = _003F487_003F._003F488_003F("");
			BoldString2 = _003F487_003F._003F488_003F("");
			SpanString3 = _003F487_003F._003F488_003F("");
			BoldString3 = _003F487_003F._003F488_003F("");
			SpanString4 = _003F487_003F._003F488_003F("");
			if (_003F452_003F.IndexOf(_003F487_003F._003F488_003F("?ŀȿ")) > -1)
			{
				((BoldTitle)/*Error near IL_0099: Stack underflow*/).BoldCount = (int)/*Error near IL_0099: Stack underflow*/;
				int num = _003F452_003F.IndexOf(_003F487_003F._003F488_003F("?ŀȿ"));
				int num2 = _003F452_003F.IndexOf(_003F487_003F._003F488_003F("8Ĭɀ\u033f"));
				SpanString1 = _003F452_003F.Substring(0, num);
				BoldString1 = _003F452_003F.Substring(num + 3, num2 - num - 3);
				SpanString2 = _003F452_003F.Substring(num2 + 4, _003F452_003F.Length - num2 - 4);
				_003F452_003F.Substring(0, num);
				_003F452_003F.Substring(num + 3, num2 - num - 3);
				_003F452_003F.Substring(num2 + 4, _003F452_003F.Length - num2 - 4);
				string spanString = SpanString2;
				if (spanString.IndexOf(_003F487_003F._003F488_003F("8Łȳ\u033f")) > -1)
				{
					BoldCount = 2;
					string value = _003F487_003F._003F488_003F("8Łȳ\u033f");
					num = ((string)/*Error near IL_014e: Stack underflow*/).IndexOf(value);
					num2 = spanString.IndexOf(_003F487_003F._003F488_003F("9īɁ\u0333п"));
					SpanString2 = spanString.Substring(0, num);
					BoldString2 = spanString.Substring(num + 4, num2 - num - 4);
					SpanString3 = spanString.Substring(num2 + 5, spanString.Length - num2 - 5);
					spanString = SpanString3;
					if (spanString.IndexOf(_003F487_003F._003F488_003F("8ŁȰ\u033f")) > -1)
					{
						BoldCount = 3;
						num = spanString.IndexOf(_003F487_003F._003F488_003F("8ŁȰ\u033f"));
						num2 = spanString.IndexOf(_003F487_003F._003F488_003F("9īɁ\u0330п"));
						SpanString3 = spanString.Substring(0, num);
						BoldString3 = spanString.Substring(num + 4, num2 - num - 4);
						SpanString4 = spanString.Substring(num2 + 5, spanString.Length - num2 - 5);
					}
				}
			}
		}

		public string ReplaceTitle(string _003F452_003F)
		{
			if (_003F452_003F == null)
			{
				return _003F487_003F._003F488_003F("");
			}
			string text = _003F452_003F;
			string text2 = _003F487_003F._003F488_003F("BŚ");
			string text3 = _003F487_003F._003F488_003F("\\");
			if (text.IndexOf(text2 + _003F487_003F._003F488_003F("GŊɖ\u0358") + text3) > -1)
			{
				text = text.Replace(text2 + _003F487_003F._003F488_003F("GŊɖ\u0358") + text3, SurveyHelper.SurveyCity);
			}
			if (text.IndexOf(_003F487_003F._003F488_003F("8Łɐ\u033f")) > -1)
			{
				text = text.Replace(_003F487_003F._003F488_003F("8Łɐ\u033f"), Environment.NewLine);
			}
			string text4 = text;
			int num = (text4.Length - text4.Replace(text2, _003F487_003F._003F488_003F("")).Length) / 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = text.IndexOf(text2);
				if (num2 > -1)
				{
					int num3 = text.IndexOf(text3);
					string text5 = text.Substring(num2 + 2, num3 - (num2 + 2));
					List<VEAnswer>.Enumerator enumerator;
					if (text5.IndexOf(_003F487_003F._003F488_003F("ZŇɌ\u0346ф")) > -1)
					{
						text5 = text5.Replace(_003F487_003F._003F488_003F("ZŇɌ\u0346ф"), _003F487_003F._003F488_003F(""));
						enumerator = SurveyHelper.SurveyExtend.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								VEAnswer current = enumerator.Current;
								if (current.QUESTION_NAME == text5)
								{
									text = text.Replace(text2 + text5 + _003F487_003F._003F488_003F("ZŇɌ\u0346ф") + text3, current.CODE);
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
					}
					else
					{
						enumerator = SurveyHelper.SurveyExtend.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								VEAnswer current2 = enumerator.Current;
								if (current2.QUESTION_NAME == text5)
								{
									text = ((!(current2.CODE_TEXT == _003F487_003F._003F488_003F("")) && current2.CODE_TEXT != null) ? text.Replace(text2 + text5 + text3, current2.CODE_TEXT) : text.Replace(text2 + text5 + text3, current2.CODE));
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
					}
				}
			}
			return text;
		}

		public List<string> ParaToList(string _003F459_003F, string _003F460_003F = ",")
		{
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Expected I4, but got Unknown
			//IL_021e: Incompatible stack heights: 0 vs 2
			//IL_023b: Incompatible stack heights: 0 vs 1
			//IL_024a: Incompatible stack heights: 0 vs 3
			//IL_0274: Incompatible stack heights: 0 vs 3
			//IL_0284: Incompatible stack heights: 0 vs 1
			//IL_0294: Incompatible stack heights: 0 vs 1
			//IL_02ae: Incompatible stack heights: 0 vs 4
			//IL_02bf: Incompatible stack heights: 0 vs 2
			//IL_02cc: Incompatible stack heights: 0 vs 2
			//IL_02d1: Invalid comparison between Unknown and I4
			//IL_02de: Incompatible stack heights: 0 vs 2
			string text = _003F487_003F._003F488_003F("Z");
			string _003F463_003F = _003F487_003F._003F488_003F("\\");
			string text2 = _003F487_003F._003F488_003F(")");
			string _003F463_003F2 = _003F487_003F._003F488_003F("(");
			string text3 = _003F487_003F._003F488_003F("z");
			string _003F463_003F3 = _003F487_003F._003F488_003F("|");
			string b = _003F487_003F._003F488_003F("Yģ");
			string text4 = _003F487_003F._003F488_003F(" Ŝ");
			List<string> list = new List<string>();
			if (_003F459_003F != null)
			{
				string b2 = _003F487_003F._003F488_003F((string)/*Error near IL_006e: Stack underflow*/);
				if (!((string)/*Error near IL_0073: Stack underflow*/ == b2))
				{
					int length = _003F460_003F.Length;
					while (oFunc.LEFT(_003F459_003F, length) == _003F460_003F)
					{
						list.Add(_003F487_003F._003F488_003F(""));
						_003F459_003F = oFunc.MID(_003F459_003F, length, -9999);
					}
					int num = 0;
					int num2 = (int)/*Error near IL_00da: Stack underflow*/;
					do
					{
						string a = oFunc.MID(_003F459_003F, num, 2);
						if (a == b)
						{
							_003F val = /*Error near IL_00fb: Stack underflow*/+ 2;
							num = ((string)/*Error near IL_0100: Stack underflow*/).IndexOf((string)/*Error near IL_0100: Stack underflow*/, (int)val);
							num = ((num < 0) ? _003F459_003F.Length : (num + 1));
						}
						else
						{
							a = oFunc.MID(_003F459_003F, num, 1);
							string a2 = oFunc.MID(_003F459_003F, num, length);
							if (a == text)
							{
								num = ((BoldTitle)/*Error near IL_0151: Stack underflow*/).RightBrackets((string)/*Error near IL_0151: Stack underflow*/, (int)/*Error near IL_0151: Stack underflow*/, text, _003F463_003F);
							}
							else if (a == text2)
							{
								num = ((BoldTitle)/*Error near IL_016f: Stack underflow*/).RightBrackets(_003F459_003F, num, text2, _003F463_003F2);
							}
							else if (a == text3)
							{
								num = ((BoldTitle)/*Error near IL_0190: Stack underflow*/).RightBrackets(_003F459_003F, num, text3, _003F463_003F3);
							}
							else if (a2 == _003F460_003F)
							{
								UDPX oFunc2 = oFunc;
								string item = ((UDPX)/*Error near IL_01ad: Stack underflow*/).SubStringFromStartToEnd((string)/*Error near IL_01ad: Stack underflow*/, (int)/*Error near IL_01ad: Stack underflow*/, num - 1);
								((List<string>)/*Error near IL_01b2: Stack underflow*/).Add(item);
								if (num + length == _003F459_003F.Length)
								{
									string item2 = _003F487_003F._003F488_003F((string)/*Error near IL_01c7: Stack underflow*/);
									((List<string>)/*Error near IL_01cc: Stack underflow*/).Add(item2);
								}
								num2 = num + length;
								num = num2 - 1;
							}
						}
						num++;
					}
					while (num < _003F459_003F.Length);
					int length2 = ((string)/*Error near IL_01f1: Stack underflow*/).Length;
					if ((int)/*Error near IL_02d1: Stack underflow*/ < length2)
					{
						UDPX oFunc3 = oFunc;
						string _003F90_003F = _003F459_003F;
						int _003F99_003F = num2;
						int _003F125_003F = _003F459_003F.Length - 1;
						string item3 = ((UDPX)/*Error near IL_0206: Stack underflow*/).SubStringFromStartToEnd(_003F90_003F, _003F99_003F, _003F125_003F);
						((List<string>)/*Error near IL_020b: Stack underflow*/).Add(item3);
					}
					return list;
				}
			}
			list.Add(_003F487_003F._003F488_003F(""));
			return list;
		}

		public int RightBrackets(string _003F461_003F, int _003F363_003F, string _003F462_003F = "(", string _003F463_003F = ")")
		{
			//IL_00e5: Incompatible stack heights: 0 vs 2
			//IL_0107: Incompatible stack heights: 0 vs 1
			//IL_012c: Incompatible stack heights: 0 vs 1
			//IL_0149: Incompatible stack heights: 1 vs 0
			string b = _003F487_003F._003F488_003F("Yģ");
			string text = _003F487_003F._003F488_003F(" Ŝ");
			int length = _003F461_003F.Length;
			int num = _003F363_003F;
			int num2 = 0;
			do
			{
				bool num3 = oFunc.MID(_003F461_003F, num, 2) == b;
				goto IL_00d9;
				IL_00d9:
				if (num3)
				{
					num = ((string)/*Error near IL_0043: Stack underflow*/).IndexOf((string)/*Error near IL_0043: Stack underflow*/, num + 2);
					if (num < 0)
					{
						return _003F461_003F.Length;
					}
					goto IL_004b;
				}
				if (oFunc.MID(_003F461_003F, num, _003F462_003F.Length) == _003F462_003F)
				{
					num2 = (((int)/*Error near IL_010c: Stack underflow*/ == 0) ? 2 : (num2 + 1));
					num += _003F462_003F.Length;
				}
				else
				{
					if (oFunc.MID(_003F461_003F, num, _003F463_003F.Length) == _003F463_003F)
					{
						num2 = (int)/*Error near IL_00b2: Stack underflow*/;
						if (num2 <= 1)
						{
							return num;
						}
						goto IL_00ba;
					}
					num++;
				}
				continue;
				IL_0133:
				goto IL_00ba;
				IL_004b:
				num += 2;
				continue;
				IL_00ba:
				num += _003F463_003F.Length;
				continue;
				IL_0144:
				goto IL_00d9;
				IL_00f1:
				goto IL_004b;
			}
			while (num < _003F461_003F.Length);
			return length;
		}

		public unsafe string AlignmentPara(string _003F464_003F, ref HorizontalAlignment _003F465_003F, ref VerticalAlignment _003F466_003F)
		{
			//IL_021d: Incompatible stack heights: 0 vs 1
			//IL_028b: Incompatible stack heights: 0 vs 2
			//IL_0296: Incompatible stack heights: 0 vs 1
			//IL_02ba: Incompatible stack heights: 0 vs 1
			//IL_02ca: Incompatible stack heights: 0 vs 2
			//IL_02ee: Incompatible stack heights: 0 vs 1
			//IL_0347: Incompatible stack heights: 0 vs 2
			//IL_0365: Incompatible stack heights: 0 vs 2
			string a;
			string a2;
			string text;
			if (_003F464_003F != _003F487_003F._003F488_003F(""))
			{
				_003F487_003F._003F488_003F("");
				a = (string)/*Error near IL_0016: Stack underflow*/;
				a2 = _003F487_003F._003F488_003F("");
				text = LEFT(_003F464_003F, 2);
				if (!text.Contains(_003F487_003F._003F488_003F("M")))
				{
					if (text.Contains(_003F487_003F._003F488_003F("S")))
					{
						a = _003F487_003F._003F488_003F("S");
					}
				}
				else
				{
					a = _003F487_003F._003F488_003F("M");
				}
				if (!text.Contains(_003F487_003F._003F488_003F("U")))
				{
					if (text.Contains(_003F487_003F._003F488_003F("C")))
					{
						a2 = _003F487_003F._003F488_003F("C");
					}
				}
				else
				{
					a2 = _003F487_003F._003F488_003F("U");
				}
				if (text.Contains(_003F487_003F._003F488_003F("B")))
				{
					string value = _003F487_003F._003F488_003F((string)/*Error near IL_00a2: Stack underflow*/);
					if (!((string)/*Error near IL_00a7: Stack underflow*/).Contains(value))
					{
						string value2 = _003F487_003F._003F488_003F("C");
						if (!((string)/*Error near IL_00bb: Stack underflow*/).Contains(value2))
						{
							goto IL_00cb;
						}
					}
					a = _003F487_003F._003F488_003F("B");
				}
				goto IL_00cb;
			}
			goto IL_0207;
			IL_0207:
			return _003F464_003F;
			IL_012f:
			if (!(a == _003F487_003F._003F488_003F("B")))
			{
				if (!(a == _003F487_003F._003F488_003F("S")))
				{
					if (a == _003F487_003F._003F488_003F("M"))
					{
						_003F465_003F = HorizontalAlignment.Left;
					}
				}
				else
				{
					_003F465_003F = HorizontalAlignment.Right;
				}
			}
			else
			{
				_003F465_003F = HorizontalAlignment.Center;
			}
			if (!(a2 == _003F487_003F._003F488_003F("C")))
			{
				if (a2 == _003F487_003F._003F488_003F("U"))
				{
					*(int*)(long)(IntPtr)(void*)/*Error near IL_01a8: Stack underflow*/ = (int)/*Error near IL_01a8: Stack underflow*/;
				}
				else if (a2 == _003F487_003F._003F488_003F("B"))
				{
					_003F466_003F = VerticalAlignment.Center;
				}
			}
			else
			{
				_003F466_003F = VerticalAlignment.Bottom;
			}
			if (!_003F487_003F._003F488_003F(":ĸȺ\u0334в\u0530ز\u0734࠺स").Contains(LEFT(_003F464_003F, 1)))
			{
				_003F464_003F = ((BoldTitle)/*Error near IL_01e9: Stack underflow*/).MID((string)/*Error near IL_01e9: Stack underflow*/, 1, -9999);
				if (!_003F487_003F._003F488_003F(":ĸȺ\u0334в\u0530ز\u0734࠺स").Contains(LEFT(_003F464_003F, 1)))
				{
					_003F464_003F = MID(_003F464_003F, 1, -9999);
				}
			}
			goto IL_0207;
			IL_00cb:
			if (text.Contains(_003F487_003F._003F488_003F("B")))
			{
				_003F464_003F.Contains(_003F487_003F._003F488_003F("S"));
				if ((int)/*Error near IL_02bf: Stack underflow*/ == 0)
				{
					string value3 = _003F487_003F._003F488_003F((string)/*Error near IL_00ea: Stack underflow*/);
					if (!((string)/*Error near IL_00ef: Stack underflow*/).Contains(value3))
					{
						goto IL_00ff;
					}
				}
				a2 = _003F487_003F._003F488_003F("B");
			}
			goto IL_00ff;
			IL_00ff:
			if (!(text == _003F487_003F._003F488_003F("B")))
			{
				bool flag = text == _003F487_003F._003F488_003F("Ał");
				if ((int)/*Error near IL_02f3: Stack underflow*/ == 0)
				{
					goto IL_012f;
				}
			}
			a = _003F487_003F._003F488_003F("B");
			a2 = _003F487_003F._003F488_003F("B");
			goto IL_012f;
		}

		public string LEFT(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0053: Incompatible stack heights: 0 vs 1
			//IL_0073: Incompatible stack heights: 0 vs 1
			//IL_0078: Incompatible stack heights: 1 vs 0
			//IL_007d: Incompatible stack heights: 0 vs 2
			//IL_0083: Incompatible stack heights: 0 vs 1
			//IL_0088: Incompatible stack heights: 1 vs 0
			if (_003F362_003F == null)
			{
				_003F487_003F._003F488_003F("");
				return (string)/*Error near IL_0007: Stack underflow*/;
			}
			if (_003F362_003F.Length == 0)
			{
				return _003F487_003F._003F488_003F("");
			}
			goto IL_0012;
			IL_0063:
			goto IL_0012;
			IL_0012:
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int length;
			if (num > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			return ((string)/*Error near IL_003e: Stack underflow*/).Substring((int)/*Error near IL_003e: Stack underflow*/, length);
		}

		public string MID(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_006c: Incompatible stack heights: 0 vs 1
			//IL_00a9: Incompatible stack heights: 0 vs 1
			//IL_00ae: Incompatible stack heights: 1 vs 0
			//IL_00b3: Incompatible stack heights: 0 vs 2
			//IL_00b9: Incompatible stack heights: 0 vs 1
			//IL_00c6: Incompatible stack heights: 0 vs 1
			if (_003F362_003F == null)
			{
				_003F487_003F._003F488_003F("");
				return (string)/*Error near IL_0007: Stack underflow*/;
			}
			if (_003F362_003F.Length == 0)
			{
				return _003F487_003F._003F488_003F("");
			}
			goto IL_0012;
			IL_00c6:
			return ((string)/*Error near IL_00cb: Stack underflow*/).Substring((int)/*Error near IL_00cb: Stack underflow*/, (int)/*Error near IL_00cb: Stack underflow*/);
			IL_007c:
			goto IL_0012;
			IL_00be:
			int num;
			int num3 = _003F362_003F.Length - num;
			goto IL_00c6;
			IL_0012:
			int num2 = _003F365_003F;
			if (num2 == -9999)
			{
				num2 = _003F362_003F.Length;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			int length;
			if (_003F363_003F > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			num = length;
			if (num + num2 > _003F362_003F.Length)
			{
				goto IL_00be;
			}
			goto IL_00c6;
			IL_0053:
			goto IL_00be;
		}
	}
}
