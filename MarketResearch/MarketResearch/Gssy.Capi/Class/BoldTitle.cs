using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Gssy.Capi.BIZ;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.Class
{
	// Token: 0x0200006C RID: 108
	public class BoldTitle : Page
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00004026 File Offset: 0x00002226
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0000402E File Offset: 0x0000222E
		public string FullTitle { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00004037 File Offset: 0x00002237
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0000403F File Offset: 0x0000223F
		public int BoldCount { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00004048 File Offset: 0x00002248
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00004050 File Offset: 0x00002250
		public string SpanString1 { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00004059 File Offset: 0x00002259
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x00004061 File Offset: 0x00002261
		public string BoldString1 { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000406A File Offset: 0x0000226A
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x00004072 File Offset: 0x00002272
		public string SpanString2 { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0000407B File Offset: 0x0000227B
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x00004083 File Offset: 0x00002283
		public string BoldString2 { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000408C File Offset: 0x0000228C
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x00004094 File Offset: 0x00002294
		public string SpanString3 { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000409D File Offset: 0x0000229D
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x000040A5 File Offset: 0x000022A5
		public string BoldString3 { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000040AE File Offset: 0x000022AE
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x000040B6 File Offset: 0x000022B6
		public string SpanString4 { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000040BF File Offset: 0x000022BF
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x000040C7 File Offset: 0x000022C7
		public List<classHtmlText> lSpan { get; set; }

		// Token: 0x06000687 RID: 1671 RVA: 0x0009FB00 File Offset: 0x0009DD00
		public void SpanTitle(string string_0 = "", string string_1 = "", string string_2 = "", string string_3 = "")
		{
			string text = string_1.Replace(global::GClass0.smethod_0("$ŝɆ͍ї՛ٜ"), SurveyHelper.SurveyCity);
			text = text.Replace(global::GClass0.smethod_0("-Ŗɟ͞ј՟ٍݞ࡙ॆੋେే൜"), SurveyHelper.SurveyID);
			this.lSpan = new LogicEngine
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
			}.listShowText(text);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0009FBB4 File Offset: 0x0009DDB4
		public string ReplaceABTitle(string string_0)
		{
			string text = string_0.Replace(global::GClass0.smethod_0("$ŝɆ͍ї՛ٜ"), SurveyHelper.SurveyCity);
			text = text.Replace(global::GClass0.smethod_0("-Ŗɟ͞ј՟ٍݞ࡙ॆੋେే൜"), SurveyHelper.SurveyID);
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

		// Token: 0x06000689 RID: 1673 RVA: 0x0009FC60 File Offset: 0x0009DE60
		public void SetTextBlock(TextBlock textBlock_0, string string_0, int int_0 = 0, string string_1 = "", bool bool_0 = true)
		{
			textBlock_0.Text = global::GClass0.smethod_0("");
			if (string_0 != global::GClass0.smethod_0(""))
			{
				if (int_0 != 0)
				{
					textBlock_0.FontSize = (double)int_0;
				}
				if (this.oFunc.LEFT(string_0, 1) == global::GClass0.smethod_0("=") && this.oFunc.LEFT(string_0, 3) != global::GClass0.smethod_0("?ŀȿ"))
				{
					int num = string_0.IndexOf(global::GClass0.smethod_0("?"));
					if (num > -1)
					{
						string text = this.oFunc.MID(string_0, 1, num - 1).ToUpper();
						string_0 = this.oFunc.MID(string_0, num + 1, -9999);
						if (string_0 != global::GClass0.smethod_0(""))
						{
							string text2 = this.oFunc.MID(text, 1, -9999);
							text = this.oFunc.LEFT(text, 1);
							if (text == global::GClass0.smethod_0("M"))
							{
								textBlock_0.HorizontalAlignment = HorizontalAlignment.Left;
							}
							else if (text == global::GClass0.smethod_0("S"))
							{
								textBlock_0.HorizontalAlignment = HorizontalAlignment.Right;
							}
							else if (text == global::GClass0.smethod_0("B"))
							{
								textBlock_0.HorizontalAlignment = HorizontalAlignment.Center;
							}
							else if (text == global::GClass0.smethod_0("U"))
							{
								textBlock_0.VerticalAlignment = VerticalAlignment.Top;
							}
							else if (text == global::GClass0.smethod_0("L"))
							{
								textBlock_0.VerticalAlignment = VerticalAlignment.Bottom;
							}
							else
							{
								text2 = text + text2;
							}
							double num2 = this.oFunc.StringToDouble(text2);
							if (num2 != 0.0)
							{
								textBlock_0.FontSize = num2;
							}
						}
					}
				}
				if (string_0 != global::GClass0.smethod_0(""))
				{
					this.SpanTitle(global::GClass0.smethod_0(""), string_0, global::GClass0.smethod_0(""), global::GClass0.smethod_0(""));
					foreach (classHtmlText classHtmlText in this.lSpan)
					{
						if (classHtmlText.TitleTextType == global::GClass0.smethod_0("?ŀȿ"))
						{
							Span span = new Span();
							span.Inlines.Add(new Run(classHtmlText.TitleText));
							span.Foreground = (Brush)base.FindResource(global::GClass0.smethod_0("\\Źɯͺѻբ٢݇ࡶॶੱ୩"));
							span.FontWeight = FontWeights.Bold;
							textBlock_0.Inlines.Add(span);
						}
						else
						{
							Span span2 = new Span();
							span2.Inlines.Add(new Run(classHtmlText.TitleText));
							textBlock_0.Inlines.Add(span2);
						}
					}
					textBlock_0.Visibility = Visibility.Visible;
					if (string_1 != global::GClass0.smethod_0(""))
					{
						double num3 = this.oFunc.StringToDouble(string_1);
						if (num3 != 0.0)
						{
							textBlock_0.Width = num3;
						}
					}
				}
			}
			if (string_0 == global::GClass0.smethod_0("") && bool_0)
			{
				textBlock_0.Height = 0.0;
				textBlock_0.Width = 0.0;
				textBlock_0.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0009FF9C File Offset: 0x0009E19C
		public int TakeFontSize(string string_0)
		{
			int result = 0;
			if (string_0 != global::GClass0.smethod_0("") && this.oFunc.LEFT(string_0, 1) == global::GClass0.smethod_0("="))
			{
				int num = string_0.IndexOf(global::GClass0.smethod_0("?"));
				if (num > -1)
				{
					string string_ = this.oFunc.MID(string_0, 1, num - 1).ToUpper();
					result = this.oFunc.StringToInt(string_);
				}
			}
			return result;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000A0014 File Offset: 0x0009E214
		public string TakeText(string string_0)
		{
			string text = global::GClass0.smethod_0("");
			List<string> list = this.ParaToList(string_0, global::GClass0.smethod_0("?"));
			bool flag = true;
			foreach (string text2 in list)
			{
				if (flag & this.oFunc.LEFT(text2, 1) == global::GClass0.smethod_0("="))
				{
					string text3 = this.oFunc.MID(text2, 1, -9999).ToUpper();
					if (!this.oFunc.isNumberic(text3) && !(text3 == global::GClass0.smethod_0("B")) && !(text3 == global::GClass0.smethod_0("M")) && !(text3 == global::GClass0.smethod_0("S")) && !(text3 == global::GClass0.smethod_0("U")) && !(text3 == global::GClass0.smethod_0("L")))
					{
						flag = false;
						text = text + text2 + global::GClass0.smethod_0("?");
					}
				}
				else
				{
					flag = false;
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000A014C File Offset: 0x0009E34C
		public void DealBoldString(string string_0)
		{
			this.FullTitle = string_0;
			this.BoldCount = 0;
			this.SpanString1 = global::GClass0.smethod_0("");
			this.BoldString1 = global::GClass0.smethod_0("");
			this.SpanString2 = global::GClass0.smethod_0("");
			this.BoldString2 = global::GClass0.smethod_0("");
			this.SpanString3 = global::GClass0.smethod_0("");
			this.BoldString3 = global::GClass0.smethod_0("");
			this.SpanString4 = global::GClass0.smethod_0("");
			if (string_0.IndexOf(global::GClass0.smethod_0("?ŀȿ")) > -1)
			{
				this.BoldCount = 1;
				int num = string_0.IndexOf(global::GClass0.smethod_0("?ŀȿ"));
				int num2 = string_0.IndexOf(global::GClass0.smethod_0("8Ĭɀ̿"));
				this.SpanString1 = string_0.Substring(0, num);
				this.BoldString1 = string_0.Substring(num + 3, num2 - num - 3);
				this.SpanString2 = string_0.Substring(num2 + 4, string_0.Length - num2 - 4);
				string_0.Substring(0, num);
				string_0.Substring(num + 3, num2 - num - 3);
				string_0.Substring(num2 + 4, string_0.Length - num2 - 4);
				string text = this.SpanString2;
				if (text.IndexOf(global::GClass0.smethod_0("8Łȳ̿")) > -1)
				{
					this.BoldCount = 2;
					num = text.IndexOf(global::GClass0.smethod_0("8Łȳ̿"));
					num2 = text.IndexOf(global::GClass0.smethod_0("9īɁ̳п"));
					this.SpanString2 = text.Substring(0, num);
					this.BoldString2 = text.Substring(num + 4, num2 - num - 4);
					this.SpanString3 = text.Substring(num2 + 5, text.Length - num2 - 5);
					text = this.SpanString3;
					if (text.IndexOf(global::GClass0.smethod_0("8ŁȰ̿")) > -1)
					{
						this.BoldCount = 3;
						num = text.IndexOf(global::GClass0.smethod_0("8ŁȰ̿"));
						num2 = text.IndexOf(global::GClass0.smethod_0("9īɁ̰п"));
						this.SpanString3 = text.Substring(0, num);
						this.BoldString3 = text.Substring(num + 4, num2 - num - 4);
						this.SpanString4 = text.Substring(num2 + 5, text.Length - num2 - 5);
					}
				}
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000A037C File Offset: 0x0009E57C
		public string ReplaceTitle(string string_0)
		{
			if (string_0 == null)
			{
				return global::GClass0.smethod_0("");
			}
			string text = string_0;
			string text2 = global::GClass0.smethod_0("BŚ");
			string text3 = global::GClass0.smethod_0("\\");
			if (text.IndexOf(text2 + global::GClass0.smethod_0("GŊɖ͘") + text3) > -1)
			{
				text = text.Replace(text2 + global::GClass0.smethod_0("GŊɖ͘") + text3, SurveyHelper.SurveyCity);
			}
			if (text.IndexOf(global::GClass0.smethod_0("8Łɐ̿")) > -1)
			{
				text = text.Replace(global::GClass0.smethod_0("8Łɐ̿"), Environment.NewLine);
			}
			string text4 = text;
			int num = (text4.Length - text4.Replace(text2, global::GClass0.smethod_0("")).Length) / 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = text.IndexOf(text2);
				if (num2 > -1)
				{
					int num3 = text.IndexOf(text3);
					string text5 = text.Substring(num2 + 2, num3 - (num2 + 2));
					if (text5.IndexOf(global::GClass0.smethod_0("ZŇɌ͆ф")) > -1)
					{
						text5 = text5.Replace(global::GClass0.smethod_0("ZŇɌ͆ф"), global::GClass0.smethod_0(""));
						using (List<VEAnswer>.Enumerator enumerator = SurveyHelper.SurveyExtend.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								VEAnswer veanswer = enumerator.Current;
								if (veanswer.QUESTION_NAME == text5)
								{
									text = text.Replace(text2 + text5 + global::GClass0.smethod_0("ZŇɌ͆ф") + text3, veanswer.CODE);
									break;
								}
							}
							goto IL_213;
						}
					}
					foreach (VEAnswer veanswer2 in SurveyHelper.SurveyExtend)
					{
						if (veanswer2.QUESTION_NAME == text5)
						{
							if (!(veanswer2.CODE_TEXT == global::GClass0.smethod_0("")) && veanswer2.CODE_TEXT != null)
							{
								text = text.Replace(text2 + text5 + text3, veanswer2.CODE_TEXT);
								break;
							}
							text = text.Replace(text2 + text5 + text3, veanswer2.CODE);
							break;
						}
					}
				}
				IL_213:;
			}
			return text;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000A05C4 File Offset: 0x0009E7C4
		public List<string> ParaToList(string string_0, string string_1 = ",")
		{
			string text = global::GClass0.smethod_0("Z");
			string string_2 = global::GClass0.smethod_0("\\");
			string text2 = global::GClass0.smethod_0(")");
			string string_3 = global::GClass0.smethod_0("(");
			string text3 = global::GClass0.smethod_0("z");
			string string_4 = global::GClass0.smethod_0("|");
			string b = global::GClass0.smethod_0("Yģ");
			string value = global::GClass0.smethod_0(" Ŝ");
			List<string> list = new List<string>();
			if (string_0 != null && !(string_0 == global::GClass0.smethod_0("")))
			{
				int length = string_1.Length;
				while (this.oFunc.LEFT(string_0, length) == string_1)
				{
					list.Add(global::GClass0.smethod_0(""));
					string_0 = this.oFunc.MID(string_0, length, -9999);
				}
				int num = 0;
				int num2 = 0;
				do
				{
					string a = this.oFunc.MID(string_0, num, 2);
					if (a == b)
					{
						num = string_0.IndexOf(value, num + 2);
						if (num < 0)
						{
							num = string_0.Length;
						}
						else
						{
							num++;
						}
					}
					else
					{
						a = this.oFunc.MID(string_0, num, 1);
						string a2 = this.oFunc.MID(string_0, num, length);
						if (a == text)
						{
							num = this.RightBrackets(string_0, num, text, string_2);
						}
						else if (a == text2)
						{
							num = this.RightBrackets(string_0, num, text2, string_3);
						}
						else if (a == text3)
						{
							num = this.RightBrackets(string_0, num, text3, string_4);
						}
						else if (a2 == string_1)
						{
							list.Add(this.oFunc.SubStringFromStartToEnd(string_0, num2, num - 1));
							if (num + length == string_0.Length)
							{
								list.Add(global::GClass0.smethod_0(""));
							}
							num2 = num + length;
							num = num2 - 1;
						}
					}
					num++;
				}
				while (num < string_0.Length);
				if (num2 < string_0.Length)
				{
					list.Add(this.oFunc.SubStringFromStartToEnd(string_0, num2, string_0.Length - 1));
				}
				return list;
			}
			list.Add(global::GClass0.smethod_0(""));
			return list;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000A07F8 File Offset: 0x0009E9F8
		public int RightBrackets(string string_0, int int_0, string string_1 = "(", string string_2 = ")")
		{
			string b = global::GClass0.smethod_0("Yģ");
			string value = global::GClass0.smethod_0(" Ŝ");
			int length = string_0.Length;
			int num = int_0;
			int num2 = 0;
			do
			{
				if (this.oFunc.MID(string_0, num, 2) == b)
				{
					num = string_0.IndexOf(value, num + 2);
					if (num < 0)
					{
						goto IL_CF;
					}
					num += 2;
				}
				else if (this.oFunc.MID(string_0, num, string_1.Length) == string_1)
				{
					if (num2 == 0)
					{
						num2 = 2;
					}
					else
					{
						num2++;
					}
					num += string_1.Length;
				}
				else if (this.oFunc.MID(string_0, num, string_2.Length) == string_2)
				{
					num2--;
					if (num2 <= 1)
					{
						return num;
					}
					num += string_2.Length;
				}
				else
				{
					num++;
				}
			}
			while (num < string_0.Length);
			return length;
			IL_CF:
			return string_0.Length;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000A08E0 File Offset: 0x0009EAE0
		public string AlignmentPara(string string_0, ref HorizontalAlignment horizontalAlignment_0, ref VerticalAlignment verticalAlignment_0)
		{
			if (string_0 != global::GClass0.smethod_0(""))
			{
				string a = global::GClass0.smethod_0("");
				string a2 = global::GClass0.smethod_0("");
				string text = this.LEFT(string_0, 2);
				if (text.Contains(global::GClass0.smethod_0("M")))
				{
					a = global::GClass0.smethod_0("M");
				}
				else if (text.Contains(global::GClass0.smethod_0("S")))
				{
					a = global::GClass0.smethod_0("S");
				}
				if (text.Contains(global::GClass0.smethod_0("U")))
				{
					a2 = global::GClass0.smethod_0("U");
				}
				else if (text.Contains(global::GClass0.smethod_0("C")))
				{
					a2 = global::GClass0.smethod_0("C");
				}
				if (text.Contains(global::GClass0.smethod_0("B")) && (string_0.Contains(global::GClass0.smethod_0("U")) || text.Contains(global::GClass0.smethod_0("C"))))
				{
					a = global::GClass0.smethod_0("B");
				}
				if (text.Contains(global::GClass0.smethod_0("B")) && (string_0.Contains(global::GClass0.smethod_0("S")) || text.Contains(global::GClass0.smethod_0("M"))))
				{
					a2 = global::GClass0.smethod_0("B");
				}
				if (text == global::GClass0.smethod_0("B") || text == global::GClass0.smethod_0("Ał"))
				{
					a = global::GClass0.smethod_0("B");
					a2 = global::GClass0.smethod_0("B");
				}
				if (a == global::GClass0.smethod_0("B"))
				{
					horizontalAlignment_0 = HorizontalAlignment.Center;
				}
				else if (a == global::GClass0.smethod_0("S"))
				{
					horizontalAlignment_0 = HorizontalAlignment.Right;
				}
				else if (a == global::GClass0.smethod_0("M"))
				{
					horizontalAlignment_0 = HorizontalAlignment.Left;
				}
				if (a2 == global::GClass0.smethod_0("C"))
				{
					verticalAlignment_0 = VerticalAlignment.Bottom;
				}
				else if (a2 == global::GClass0.smethod_0("U"))
				{
					verticalAlignment_0 = VerticalAlignment.Top;
				}
				else if (a2 == global::GClass0.smethod_0("B"))
				{
					verticalAlignment_0 = VerticalAlignment.Center;
				}
				if (!global::GClass0.smethod_0(":ĸȺ̴в԰زܴ࠺स").Contains(this.LEFT(string_0, 1)))
				{
					string_0 = this.MID(string_0, 1, -9999);
					if (!global::GClass0.smethod_0(":ĸȺ̴в԰زܴ࠺स").Contains(this.LEFT(string_0, 1)))
					{
						string_0 = this.MID(string_0, 1, -9999);
					}
				}
			}
			return string_0;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000A0B2C File Offset: 0x0009ED2C
		public string LEFT(string string_0, int int_0 = 1)
		{
			if (string_0 == null)
			{
				return global::GClass0.smethod_0("");
			}
			if (string_0.Length == 0)
			{
				return global::GClass0.smethod_0("");
			}
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000A0B7C File Offset: 0x0009ED7C
		public string MID(string string_0, int int_0, int int_1 = -9999)
		{
			if (string_0 == null)
			{
				return global::GClass0.smethod_0("");
			}
			if (string_0.Length == 0)
			{
				return global::GClass0.smethod_0("");
			}
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

		// Token: 0x04000C3F RID: 3135
		private UDPX oFunc = new UDPX();
	}
}
