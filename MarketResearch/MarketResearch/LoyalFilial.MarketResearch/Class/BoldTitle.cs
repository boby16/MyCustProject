using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.Class
{
	public class BoldTitle : Page
	{
		public string FullTitle { get; set; }

		public int BoldCount { get; set; }

		public string SpanString1 { get; set; }

		public string BoldString1 { get; set; }

		public string SpanString2 { get; set; }

		public string BoldString2 { get; set; }

		public string SpanString3 { get; set; }

		public string BoldString3 { get; set; }

		public string SpanString4 { get; set; }

		public List<classHtmlText> lSpan { get; set; }

		public void SpanTitle(string string_0 = "", string string_1 = "", string string_2 = "", string string_3 = "")
		{
			string text = string_1.Replace("#[CITY]", SurveyHelper.SurveyCity);
			text = text.Replace("#[SURVEY_CODE]", SurveyHelper.SurveyID);
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

		public string ReplaceABTitle(string string_0)
		{
			string text = string_0.Replace("#[CITY]", SurveyHelper.SurveyCity);
			text = text.Replace("#[SURVEY_CODE]", SurveyHelper.SurveyID);
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

		public void SetTextBlock(TextBlock textBlock_0, string string_0, int int_0 = 0, string string_1 = "", bool bool_0 = true)
		{
			textBlock_0.Text = "";
			if (string_0 != "")
			{
				if (int_0 != 0)
				{
					textBlock_0.FontSize = (double)int_0;
				}
				if (this.oFunc.LEFT(string_0, 1) == "<" && this.oFunc.LEFT(string_0, 3) != "<B>")
				{
					int num = string_0.IndexOf(">");
					if (num > -1)
					{
						string text = this.oFunc.MID(string_0, 1, num - 1).ToUpper();
						string_0 = this.oFunc.MID(string_0, num + 1, -9999);
						if (string_0 != "")
						{
							string text2 = this.oFunc.MID(text, 1, -9999);
							text = this.oFunc.LEFT(text, 1);
							if (text == "L")
							{
								textBlock_0.HorizontalAlignment = HorizontalAlignment.Left;
							}
							else if (text == "R")
							{
								textBlock_0.HorizontalAlignment = HorizontalAlignment.Right;
							}
							else if (text == "C")
							{
								textBlock_0.HorizontalAlignment = HorizontalAlignment.Center;
							}
							else if (text == "T")
							{
								textBlock_0.VerticalAlignment = VerticalAlignment.Top;
							}
							else if (text == "M")
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
				if (string_0 != "")
				{
					this.SpanTitle("", string_0, "", "");
					foreach (classHtmlText classHtmlText in this.lSpan)
					{
						if (classHtmlText.TitleTextType == "<B>")
						{
							Span span = new Span();
							span.Inlines.Add(new Run(classHtmlText.TitleText));
							span.Foreground = (Brush)base.FindResource("PressedBrush");
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
					if (string_1 != "")
					{
						double num3 = this.oFunc.StringToDouble(string_1);
						if (num3 != 0.0)
						{
							textBlock_0.Width = num3;
						}
					}
				}
			}
			if (string_0 == "" && bool_0)
			{
				textBlock_0.Height = 0.0;
				textBlock_0.Width = 0.0;
				textBlock_0.Visibility = Visibility.Collapsed;
			}
		}

		public int TakeFontSize(string string_0)
		{
			int result = 0;
			if (string_0 != "" && this.oFunc.LEFT(string_0, 1) == "<")
			{
				int num = string_0.IndexOf(">");
				if (num > -1)
				{
					string string_ = this.oFunc.MID(string_0, 1, num - 1).ToUpper();
					result = this.oFunc.StringToInt(string_);
				}
			}
			return result;
		}

		public string TakeText(string string_0)
		{
			string text = "";
			List<string> list = this.ParaToList(string_0, ">");
			bool flag = true;
			foreach (string text2 in list)
			{
				if (flag & this.oFunc.LEFT(text2, 1) == "<")
				{
					string text3 = this.oFunc.MID(text2, 1, -9999).ToUpper();
					if (!this.oFunc.isNumberic(text3) && !(text3 == "C") && !(text3 == "L") && !(text3 == "R") && !(text3 == "T") && !(text3 == "M"))
					{
						flag = false;
						text = text + text2 + ">";
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

		public void DealBoldString(string string_0)
		{
			this.FullTitle = string_0;
			this.BoldCount = 0;
			this.SpanString1 = "";
			this.BoldString1 = "";
			this.SpanString2 = "";
			this.BoldString2 = "";
			this.SpanString3 = "";
			this.BoldString3 = "";
			this.SpanString4 = "";
			if (string_0.IndexOf("<B>") > -1)
			{
				this.BoldCount = 1;
				int num = string_0.IndexOf("<B>");
				int num2 = string_0.IndexOf("</B>");
				this.SpanString1 = string_0.Substring(0, num);
				this.BoldString1 = string_0.Substring(num + 3, num2 - num - 3);
				this.SpanString2 = string_0.Substring(num2 + 4, string_0.Length - num2 - 4);
				string_0.Substring(0, num);
				string_0.Substring(num + 3, num2 - num - 3);
				string_0.Substring(num2 + 4, string_0.Length - num2 - 4);
				string text = this.SpanString2;
				if (text.IndexOf("<B1>") > -1)
				{
					this.BoldCount = 2;
					num = text.IndexOf("<B1>");
					num2 = text.IndexOf("</B1>");
					this.SpanString2 = text.Substring(0, num);
					this.BoldString2 = text.Substring(num + 4, num2 - num - 4);
					this.SpanString3 = text.Substring(num2 + 5, text.Length - num2 - 5);
					text = this.SpanString3;
					if (text.IndexOf("<B2>") > -1)
					{
						this.BoldCount = 3;
						num = text.IndexOf("<B2>");
						num2 = text.IndexOf("</B2>");
						this.SpanString3 = text.Substring(0, num);
						this.BoldString3 = text.Substring(num + 4, num2 - num - 4);
						this.SpanString4 = text.Substring(num2 + 5, text.Length - num2 - 5);
					}
				}
			}
		}

		public string ReplaceTitle(string string_0)
		{
			if (string_0 == null)
			{
				return "";
			}
			string text = string_0;
			string text2 = "@[";
			string text3 = "]";
			if (text.IndexOf(text2 + "CITY" + text3) > -1)
			{
				text = text.Replace(text2 + "CITY" + text3, SurveyHelper.SurveyCity);
			}
			if (text.IndexOf("<BR>") > -1)
			{
				text = text.Replace("<BR>", Environment.NewLine);
			}
			string text4 = text;
			int num = (text4.Length - text4.Replace(text2, "").Length) / 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = text.IndexOf(text2);
				if (num2 > -1)
				{
					int num3 = text.IndexOf(text3);
					string text5 = text.Substring(num2 + 2, num3 - (num2 + 2));
					if (text5.IndexOf("_CODE") > -1)
					{
						text5 = text5.Replace("_CODE", "");
						using (List<VEAnswer>.Enumerator enumerator = SurveyHelper.SurveyExtend.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								VEAnswer veanswer = enumerator.Current;
								if (veanswer.QUESTION_NAME == text5)
								{
									text = text.Replace(text2 + text5 + "_CODE" + text3, veanswer.CODE);
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
							if (!(veanswer2.CODE_TEXT == "") && veanswer2.CODE_TEXT != null)
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

		public List<string> ParaToList(string string_0, string string_1 = ",")
		{
			string text = "[";
			string string_2 = "]";
			string text2 = "(";
			string string_3 = ")";
			string text3 = "{";
			string string_4 = "}";
			string b = "[\"";
			string value = "\"]";
			List<string> list = new List<string>();
			if (string_0 != null && !(string_0 == ""))
			{
				int length = string_1.Length;
				while (this.oFunc.LEFT(string_0, length) == string_1)
				{
					list.Add("");
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
								list.Add("");
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
			list.Add("");
			return list;
		}

		public int RightBrackets(string string_0, int int_0, string string_1 = "(", string string_2 = ")")
		{
			string b = "[\"";
			string value = "\"]";
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

		public string AlignmentPara(string string_0, ref HorizontalAlignment horizontalAlignment_0, ref VerticalAlignment verticalAlignment_0)
		{
			if (string_0 != "")
			{
				string a = "";
				string a2 = "";
				string text = this.LEFT(string_0, 2);
				if (text.Contains("L"))
				{
					a = "L";
				}
				else if (text.Contains("R"))
				{
					a = "R";
				}
				if (text.Contains("T"))
				{
					a2 = "T";
				}
				else if (text.Contains("B"))
				{
					a2 = "B";
				}
				if (text.Contains("C") && (string_0.Contains("T") || text.Contains("B")))
				{
					a = "C";
				}
				if (text.Contains("C") && (string_0.Contains("R") || text.Contains("L")))
				{
					a2 = "C";
				}
				if (text == "C" || text == "CC")
				{
					a = "C";
					a2 = "C";
				}
				if (a == "C")
				{
					horizontalAlignment_0 = HorizontalAlignment.Center;
				}
				else if (a == "R")
				{
					horizontalAlignment_0 = HorizontalAlignment.Right;
				}
				else if (a == "L")
				{
					horizontalAlignment_0 = HorizontalAlignment.Left;
				}
				if (a2 == "B")
				{
					verticalAlignment_0 = VerticalAlignment.Bottom;
				}
				else if (a2 == "T")
				{
					verticalAlignment_0 = VerticalAlignment.Top;
				}
				else if (a2 == "C")
				{
					verticalAlignment_0 = VerticalAlignment.Center;
				}
				if (!"0123456789".Contains(this.LEFT(string_0, 1)))
				{
					string_0 = this.MID(string_0, 1, -9999);
					if (!"0123456789".Contains(this.LEFT(string_0, 1)))
					{
						string_0 = this.MID(string_0, 1, -9999);
					}
				}
			}
			return string_0;
		}

		public string LEFT(string string_0, int int_0 = 1)
		{
			if (string_0 == null)
			{
				return "";
			}
			if (string_0.Length == 0)
			{
				return "";
			}
			int num = (int_0 < 0) ? 0 : int_0;
			return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
		}

		public string MID(string string_0, int int_0, int int_1 = -9999)
		{
			if (string_0 == null)
			{
				return "";
			}
			if (string_0.Length == 0)
			{
				return "";
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

		private UDPX oFunc = new UDPX();
	}
}
