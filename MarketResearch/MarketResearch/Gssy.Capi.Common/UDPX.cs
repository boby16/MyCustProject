using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Gssy.Capi.Common
{
	// Token: 0x02000019 RID: 25
	public class UDPX
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x000052A4 File Offset: 0x000034A4
		public bool isNumberic(string string_0)
		{
			Regex regex = new Regex(GClass0.smethod_0("[Řɧ̩Х"));
			return regex.IsMatch(string_0);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000052C8 File Offset: 0x000034C8
		public bool isValue(string string_0)
		{
			Regex regex = new Regex(GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ"));
			return regex.IsMatch(string_0);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000052EC File Offset: 0x000034EC
		public bool isLetter(string string_0)
		{
			Regex regex = new Regex(GClass0.smethod_0("UőɈ̥ѝէبݾ࡞ऩਥ"));
			return regex.IsMatch(string_0);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005310 File Offset: 0x00003510
		public bool isEmail(string string_0)
		{
			Regex regex = new Regex(GClass0.smethod_0("wŝȂ̀Ѽԋ؎܊ࡾॾ੖ଋశഴ๝ཀၬᄱሱፃᐺᔸᙈᝈᡤ᤹ᨸᬺ᱓ᴠṑύ†™≒⌥␩╛♙❳⠨⤫⨫"));
			return regex.IsMatch(string_0);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005334 File Offset: 0x00003534
		public bool isHttp(string string_0)
		{
			string input = (this.MID(string_0, 7, -9999).ToLower() == GClass0.smethod_0("oŲɱʹйԭخ")) ? string_0 : (GClass0.smethod_0("oŲɱʹйԭخ") + string_0);
			Regex regex = new Regex(GClass0.smethod_0("AŜɓ͖Пԋ،܊ࡺॼ੨ଳీഷ็༴ူᄳቌፊᑢᔹᙎ᜹ᠹ᤿ᩔ᭒ᱺᴡḫἤ…ℷ∢⌠␸╙☩✫⠾"));
			return regex.IsMatch(input);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005390 File Offset: 0x00003590
		public bool isChinese(string string_0)
		{
			Regex regex = new Regex(GClass0.smethod_0("LŊɌͺкՈؼܻࠧॕ੽ାీൄัཞဩᄥ"));
			return regex.IsMatch(string_0);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000053B4 File Offset: 0x000035B4
		public bool isWord(string string_0)
		{
			Regex regex = new Regex(GClass0.smethod_0("Dłə̺ьմعݩࡎॵੌ୺఺ൈ฼༻ဧᅕችጾᑀᕄᘱ᝞ᠩᤥ"));
			return regex.IsMatch(string_0);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000053D8 File Offset: 0x000035D8
		public bool isIP(string string_0)
		{
			Regex regex = new Regex(GClass0.smethod_0("+Ŝȗ̉р՜ٝܓࠑढ़ਏ଎క൚฼བ၈ᅐሾጆᐝᕒᙪᜅᡭᥱᩮᬇᱰᵶṿἲ‮Ⅵ≿⍠␬┬♾✪⠩⤰⩹⬑ⱹⵥ⹳⼛〡ㄸ㉱㍷㐚㕰㘒㜋㡠㤕㨕㬒㱝㵃㸆㼚䀇䅉䉏䌃䑕䕔䙓䜜䡶䤜䨆䬞䱴䵌乛伔倐兿刓匏吔啽嘶地堵奸婠嬫尵崪幪彪怤慰扷据搣敋昿朣根楑橯歶氻洽湜漶瀨焱牞猫琥"));
			return regex.IsMatch(string_0);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000053FC File Offset: 0x000035FC
		public bool isMatch(string string_0, string string_1)
		{
			Regex regex = new Regex(string_1);
			return regex.IsMatch(string_0);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005418 File Offset: 0x00003618
		public void WriteListAppendToFile(List<string> list_0, string string_0, string string_1 = "Default", string string_2 = "\r\n")
		{
			string_1 = string_1.ToUpper();
			Encoding encoding;
			if (string_1 == GClass0.smethod_0("CŃɃͅіՎٕ"))
			{
				encoding = Encoding.Default;
			}
			else if (string_1 == GClass0.smethod_0("Dŗɀ͋ш"))
			{
				encoding = Encoding.ASCII;
			}
			else if (string_1 == GClass0.smethod_0("RňɌ͇ьՆل"))
			{
				encoding = Encoding.Unicode;
			}
			else if (string_1 == GClass0.smethod_0("PŐɅ̱г"))
			{
				encoding = Encoding.UTF32;
			}
			else if (string_1 == GClass0.smethod_0("QŗɄ̹"))
			{
				encoding = Encoding.UTF8;
			}
			else
			{
				encoding = Encoding.Default;
			}
			string value = string.Join(string_2, list_0.ToArray());
			StreamWriter streamWriter = new StreamWriter(string_0, true, encoding);
			streamWriter.WriteLine(value);
			streamWriter.Close();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000054DC File Offset: 0x000036DC
		public void WriteListToFile(List<string> list_0, string string_0, bool bool_0 = false, string string_1 = "Default", string string_2 = "\r\n")
		{
			string_1 = string_1.ToUpper();
			Encoding encoding;
			if (string_1 == GClass0.smethod_0("CŃɃͅіՎٕ"))
			{
				encoding = Encoding.Default;
			}
			else if (string_1 == GClass0.smethod_0("Dŗɀ͋ш"))
			{
				encoding = Encoding.ASCII;
			}
			else if (string_1 == GClass0.smethod_0("RňɌ͇ьՆل"))
			{
				encoding = Encoding.Unicode;
			}
			else if (string_1 == GClass0.smethod_0("PŐɅ̱г"))
			{
				encoding = Encoding.UTF32;
			}
			else if (string_1 == GClass0.smethod_0("QŗɄ̹"))
			{
				encoding = Encoding.UTF8;
			}
			else
			{
				encoding = Encoding.Default;
			}
			string value = string.Join(string_2, list_0.ToArray());
			StreamWriter streamWriter = new StreamWriter(string_0, bool_0, encoding);
			streamWriter.WriteLine(value);
			streamWriter.Close();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000055A4 File Offset: 0x000037A4
		public List<string> ReadTextFileToList(string string_0, string string_1 = "\r\n", string string_2 = "Default")
		{
			string_2 = string_2.ToUpper();
			Encoding encoding;
			if (string_2 == GClass0.smethod_0("CŃɃͅіՎٕ"))
			{
				encoding = Encoding.Default;
			}
			else if (string_2 == GClass0.smethod_0("Dŗɀ͋ш"))
			{
				encoding = Encoding.ASCII;
			}
			else if (string_2 == GClass0.smethod_0("RňɌ͇ьՆل"))
			{
				encoding = Encoding.Unicode;
			}
			else if (string_2 == GClass0.smethod_0("PŐɅ̱г"))
			{
				encoding = Encoding.UTF32;
			}
			else if (string_2 == GClass0.smethod_0("QŗɄ̹"))
			{
				encoding = Encoding.UTF8;
			}
			else
			{
				encoding = Encoding.Default;
			}
			StreamReader streamReader = new StreamReader(string_0, encoding);
			string text = streamReader.ReadToEnd();
			streamReader.Close();
			return new List<string>(text.Split(new string[]
			{
				string_1
			}, StringSplitOptions.RemoveEmptyEntries));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005670 File Offset: 0x00003870
		public void WriteStringAppendToFile(string string_0, string string_1, string string_2 = "Default")
		{
			string_2 = string_2.ToUpper();
			Encoding encoding;
			if (string_2 == GClass0.smethod_0("CŃɃͅіՎٕ"))
			{
				encoding = Encoding.Default;
			}
			else if (string_2 == GClass0.smethod_0("Dŗɀ͋ш"))
			{
				encoding = Encoding.ASCII;
			}
			else if (string_2 == GClass0.smethod_0("RňɌ͇ьՆل"))
			{
				encoding = Encoding.Unicode;
			}
			else if (string_2 == GClass0.smethod_0("PŐɅ̱г"))
			{
				encoding = Encoding.UTF32;
			}
			else if (string_2 == GClass0.smethod_0("QŗɄ̹"))
			{
				encoding = Encoding.UTF8;
			}
			else
			{
				encoding = Encoding.Default;
			}
			StreamWriter streamWriter = new StreamWriter(string_1, true, encoding);
			streamWriter.WriteLine(string_0);
			streamWriter.Close();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005670 File Offset: 0x00003870
		public void AppendWrite(string string_0, string string_1, string string_2 = "Default")
		{
			string_2 = string_2.ToUpper();
			Encoding encoding;
			if (string_2 == GClass0.smethod_0("CŃɃͅіՎٕ"))
			{
				encoding = Encoding.Default;
			}
			else if (string_2 == GClass0.smethod_0("Dŗɀ͋ш"))
			{
				encoding = Encoding.ASCII;
			}
			else if (string_2 == GClass0.smethod_0("RňɌ͇ьՆل"))
			{
				encoding = Encoding.Unicode;
			}
			else if (string_2 == GClass0.smethod_0("PŐɅ̱г"))
			{
				encoding = Encoding.UTF32;
			}
			else if (string_2 == GClass0.smethod_0("QŗɄ̹"))
			{
				encoding = Encoding.UTF8;
			}
			else
			{
				encoding = Encoding.Default;
			}
			StreamWriter streamWriter = new StreamWriter(string_1, true, encoding);
			streamWriter.WriteLine(string_0);
			streamWriter.Close();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005724 File Offset: 0x00003924
		public void WriteNew(string string_0, string string_1, string string_2 = "Default")
		{
			string_2 = string_2.ToUpper();
			Encoding encoding;
			if (string_2 == GClass0.smethod_0("CŃɃͅіՎٕ"))
			{
				encoding = Encoding.Default;
			}
			else if (string_2 == GClass0.smethod_0("Dŗɀ͋ш"))
			{
				encoding = Encoding.ASCII;
			}
			else if (string_2 == GClass0.smethod_0("RňɌ͇ьՆل"))
			{
				encoding = Encoding.Unicode;
			}
			else if (string_2 == GClass0.smethod_0("PŐɅ̱г"))
			{
				encoding = Encoding.UTF32;
			}
			else if (string_2 == GClass0.smethod_0("QŗɄ̹"))
			{
				encoding = Encoding.UTF8;
			}
			else
			{
				encoding = Encoding.Default;
			}
			StreamWriter streamWriter = new StreamWriter(string_1, false, encoding);
			streamWriter.WriteLine(string_0);
			streamWriter.Close();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000057D8 File Offset: 0x000039D8
		public string SetValueOfKeyAtList(List<string> list_0, string string_0, string string_1, bool bool_0 = true, int int_0 = 0, string string_2 = "", bool bool_1 = true)
		{
			string result = GClass0.smethod_0("");
			int num = int_0;
			if (string_2.Length > 0)
			{
				for (int i = num; i < list_0.Count; i++)
				{
					if (string_2 == list_0[i])
					{
						num = i++;
						break;
					}
				}
			}
			for (int j = num; j < list_0.Count; j++)
			{
				string text = this.LEFT(list_0[j], string_0.Length);
				if (string_0 == text)
				{
					result = (bool_0 ? this.MID(list_0[j], string_0.Length, -9999) : list_0[j]);
					list_0[j] = (bool_0 ? (text + string_1) : string_1);
					return result;
				}
			}
			if (bool_1)
			{
				list_0.Add(bool_0 ? (string_0 + string_1) : string_1);
			}
			return result;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000058C4 File Offset: 0x00003AC4
		public string SetValueOfKeyAtFile(string string_0, string string_1, string string_2, bool bool_0 = true, int int_0 = 0, string string_3 = "", bool bool_1 = true, string string_4 = "Default")
		{
			string result = GClass0.smethod_0("");
			List<string> list_ = this.ReadTextFileToList(string_0, GClass0.smethod_0("\u000fċ"), string_4);
			result = this.SetValueOfKeyAtList(list_, string_1, string_2, bool_0, int_0, string_3, bool_1);
			this.WriteListToFile(list_, string_0, false, string_4, GClass0.smethod_0("\u000fċ"));
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000591C File Offset: 0x00003B1C
		public string GetValueOfKeyAtList(List<string> list_0, string string_0, int int_0 = 0, string string_1 = "")
		{
			string result = GClass0.smethod_0("");
			int num = int_0;
			if (string_1.Length > 0)
			{
				for (int i = num; i < list_0.Count; i++)
				{
					if (string_1 == list_0[i])
					{
						num = i++;
						break;
					}
				}
			}
			for (int j = num; j < list_0.Count; j++)
			{
				string b = this.LEFT(list_0[j], string_0.Length);
				if (string_0 == b)
				{
					return this.MID(list_0[j], string_0.Length, -9999);
				}
			}
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000059C8 File Offset: 0x00003BC8
		public string GetValueOfKeyAtFile(string string_0, string string_1, int int_0 = 0, string string_2 = "", string string_3 = "Default")
		{
			string text = GClass0.smethod_0("");
			List<string> list_ = this.ReadTextFileToList(string_0, GClass0.smethod_0("\u000fċ"), string_3);
			return this.GetValueOfKeyAtList(list_, string_1, int_0, string_2);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005A04 File Offset: 0x00003C04
		public string ReadTextFileToString(string string_0, string string_1 = "Default")
		{
			string_1 = string_1.ToUpper();
			Encoding encoding;
			if (string_1 == GClass0.smethod_0("Dŗɀ͋ш"))
			{
				encoding = Encoding.ASCII;
			}
			else if (string_1 == GClass0.smethod_0("RňɌ͇ьՆل"))
			{
				encoding = Encoding.Unicode;
			}
			else if (string_1 == GClass0.smethod_0("PŐɅ̱г"))
			{
				encoding = Encoding.UTF32;
			}
			else if (string_1 == GClass0.smethod_0("QŗɄ̹"))
			{
				encoding = Encoding.UTF8;
			}
			else
			{
				encoding = Encoding.Default;
			}
			StreamReader streamReader = new StreamReader(string_0, encoding);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public double INT(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 0)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public double Floor(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 0)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public double RoundDown(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 0)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public double Round(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 45)
		{
			double num = double_0;
			double num2 = 0.5;
			if (int_2 == 0)
			{
				num2 = 0.0;
			}
			if (int_2 == 1)
			{
				num2 = 1.0;
			}
			bool flag = false;
			if (double_0 < 0.0)
			{
				flag = true;
				num = -double_0;
			}
			double num3 = (int_1 > 0) ? Math.Pow(10.0, (double)int_1) : Math.Pow(10.0, (double)int_0);
			num = ((num3 == 0.0) ? 0.0 : (Math.Truncate(num / num3 + num2) * num3));
			if (flag)
			{
				num = -num;
			}
			return num;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public double Top(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 1)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public double RoundUp(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 1)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005B68 File Offset: 0x00003D68
		public int MOD(int int_0, int int_1, int int_2 = 1)
		{
			int result;
			if (int_1 == 0)
			{
				result = 0;
			}
			else if (int_0 == 0)
			{
				result = int_1;
			}
			else
			{
				int num = (int)Math.Truncate((double)int_0 / (double)int_2 + 0.99999999999999);
				int num2 = num % int_1;
				if (num2 == 0)
				{
					num2 = int_1;
				}
				result = num2;
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005BB0 File Offset: 0x00003DB0
		private double method_0(double double_0)
		{
			string text = double_0.ToString();
			int num = text.IndexOf(GClass0.smethod_0("/"));
			if (num > -1)
			{
				text = GClass0.smethod_0("2į") + text.Substring(num + 1, text.Length - num - 1);
			}
			else
			{
				text = GClass0.smethod_0("1");
			}
			return Convert.ToDouble(text);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005C18 File Offset: 0x00003E18
		public List<string> StringArrayToList(string[] string_0)
		{
			List<string> list = new List<string>();
			foreach (string item in string_0)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005C58 File Offset: 0x00003E58
		public string[] StringArrayDeleteDuplicate(string[] string_0)
		{
			List<string> list = new List<string>();
			foreach (string item in string_0)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public string ArrayToString(string[] string_0, string string_1 = "", bool bool_0 = false, string string_2 = "")
		{
			string text = GClass0.smethod_0("");
			foreach (string text2 in string_0)
			{
				if (string_2 == GClass0.smethod_0(""))
				{
					if (bool_0 || text2 != GClass0.smethod_0(""))
					{
						text = text + string_1 + text2;
					}
				}
				else
				{
					text = text + string_1 + this.NumberFormat(text2, string_2, GClass0.smethod_0("1"));
				}
			}
			if (text.Length > string_1.Length && string_1.Length > 0)
			{
				text = this.MID(text, string_1.Length, -9999);
			}
			return text;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005D58 File Offset: 0x00003F58
		public string ListToString(List<string> list_0, string string_0 = "", bool bool_0 = false, string string_1 = "")
		{
			string text = GClass0.smethod_0("");
			foreach (string text2 in list_0)
			{
				if (string_1 == GClass0.smethod_0(""))
				{
					if (bool_0 || text2 != GClass0.smethod_0(""))
					{
						text = text + string_0 + text2;
					}
				}
				else
				{
					text = text + string_0 + this.NumberFormat(text2, string_1, GClass0.smethod_0("1"));
				}
			}
			if (text.Length > string_0.Length && string_0.Length > 0)
			{
				text = this.MID(text, string_0.Length, -9999);
			}
			return text;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005E30 File Offset: 0x00004030
		public string StringInArray(string string_0, string[] string_1, bool bool_0 = true)
		{
			string a = bool_0 ? string_0.ToUpper() : string_0;
			foreach (string text in string_1)
			{
				if (a == (bool_0 ? text.ToUpper() : text))
				{
					return text;
				}
			}
			return GClass0.smethod_0("");
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005E88 File Offset: 0x00004088
		public Dictionary<string, string> SortDictSDbyValue(Dictionary<string, double> dictionary_0, bool bool_0 = false, bool bool_1 = false, string string_0 = ",")
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<int> list = new List<int>();
			for (int i = 1; i <= dictionary_0.Count; i++)
			{
				list.Add(1);
				dictionary.Add(i.ToString(), GClass0.smethod_0(""));
			}
			for (int j = 0; j < dictionary_0.Count - 1; j++)
			{
				double num = dictionary_0.Values.ElementAt(j);
				for (int k = j + 1; k < dictionary_0.Count; k++)
				{
					double num2 = dictionary_0.Values.ElementAt(k);
					if (bool_0)
					{
						if (num > num2)
						{
							List<int> list2 = list;
							int num3 = k;
							int num4 = list2[num3];
							list2[num3] = num4 + 1;
						}
						if (num < num2)
						{
							List<int> list3 = list;
							int num4 = j;
							int num3 = list3[num4];
							list3[num4] = num3 + 1;
						}
					}
					else
					{
						if (num < num2)
						{
							List<int> list4 = list;
							int num3 = k;
							int num4 = list4[num3];
							list4[num3] = num4 + 1;
						}
						if (num > num2)
						{
							List<int> list5 = list;
							int num4 = j;
							int num3 = list5[num4];
							list5[num4] = num3 + 1;
						}
					}
				}
			}
			if (bool_1)
			{
				dictionary.Clear();
				int i = 0;
				using (Dictionary<string, double>.KeyCollection.Enumerator enumerator = dictionary_0.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						dictionary.Add(key, list[i++].ToString());
					}
					return dictionary;
				}
			}
			for (int i = 0; i < list.Count<int>(); i++)
			{
				string key2 = list[i].ToString();
				dictionary[key2] = dictionary[key2] + string_0 + dictionary_0.Keys.ElementAt(i);
			}
			for (int i = 1; i <= dictionary.Count<KeyValuePair<string, string>>(); i++)
			{
				string key3 = i.ToString();
				if (dictionary[key3] == GClass0.smethod_0(""))
				{
					dictionary[key3] = dictionary[(i - 1).ToString()];
				}
				if (this.LEFT(dictionary[key3], 1) == string_0.ToString())
				{
					dictionary[key3] = this.MID(dictionary[key3], 1, -9999);
				}
			}
			return dictionary;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006108 File Offset: 0x00004308
		public List<string> StringToList(string string_0, string string_1 = "\r\n")
		{
			return new List<string>(string_0.Split(new string[]
			{
				string_1
			}, StringSplitOptions.RemoveEmptyEntries));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006130 File Offset: 0x00004330
		public string[] StringToArray(string string_0, char char_0 = ',')
		{
			return string_0.Split(new char[]
			{
				char_0
			});
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006150 File Offset: 0x00004350
		public int ASC(string string_0)
		{
			int result;
			if (string_0.Length == 1)
			{
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				int num = (int)asciiencoding.GetBytes(string_0)[0];
				result = num;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006180 File Offset: 0x00004380
		public string CHR(int int_0)
		{
			string result;
			if (int_0 >= 0 && int_0 <= 255)
			{
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				byte[] bytes = new byte[]
				{
					(byte)int_0
				};
				string @string = asciiencoding.GetString(bytes);
				result = @string;
			}
			else
			{
				result = GClass0.smethod_0("");
			}
			return result;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000061D0 File Offset: 0x000043D0
		public int RightBrackets(string string_0, int int_0, string string_1 = "(", string string_2 = ")")
		{
			int length = string_0.Length;
			int length2 = string_1.Length;
			int length3 = string_2.Length;
			int num = int_0;
			int num2 = 0;
			do
			{
				this.MID(string_0, num, length2);
				if (this.MID(string_0, num, length2) == string_1)
				{
					if (num2 == 0)
					{
						num2 = 2;
					}
					else
					{
						num2++;
					}
					num = num + length2 - 1;
				}
				else if (this.MID(string_0, num, length3) == string_2)
				{
					num2--;
					if (num2 <= 1)
					{
						goto IL_98;
					}
					num = num + length3 - 1;
				}
				num++;
			}
			while (num < string_0.Length);
			return length;
			IL_98:
			return num;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006280 File Offset: 0x00004480
		public string SubStringFromStartToEnd(string string_0, int int_0, int int_1 = -9999)
		{
			string result;
			if (string_0 == null)
			{
				result = GClass0.smethod_0("");
			}
			else if (string_0.Length == 0)
			{
				result = GClass0.smethod_0("");
			}
			else
			{
				int num = (int_0 < 0) ? 0 : int_0;
				if (num >= string_0.Length)
				{
					num = string_0.Length - 1;
				}
				int num2 = (int_1 == -9999) ? num : int_1;
				if (num2 < 0)
				{
					num2 = 0;
				}
				else if (num2 >= string_0.Length)
				{
					num2 = string_0.Length - 1;
				}
				if (num > num2)
				{
					result = GClass0.smethod_0("");
				}
				else
				{
					int num3 = (num < num2) ? num : num2;
					int num4 = (num < num2) ? num2 : num;
					int length = num4 - num3 + 1;
					string text = string_0.Substring(num3, length);
					result = text;
				}
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000634C File Offset: 0x0000454C
		public string LEFT(string string_0, int int_0 = 1)
		{
			string result;
			if (string_0 == null)
			{
				result = GClass0.smethod_0("");
			}
			else if (string_0.Length == 0)
			{
				result = GClass0.smethod_0("");
			}
			else
			{
				int num = (int_0 < 0) ? 0 : int_0;
				result = string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
			}
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000063A8 File Offset: 0x000045A8
		public string MID(string string_0, int int_0, int int_1 = -9999)
		{
			string result;
			if (string_0 == null)
			{
				result = GClass0.smethod_0("");
			}
			else if (string_0.Length == 0)
			{
				result = GClass0.smethod_0("");
			}
			else
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
				result = string_0.Substring(num2, (num2 + num > string_0.Length) ? (string_0.Length - num2) : num);
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006430 File Offset: 0x00004630
		public string RIGHT(string string_0, int int_0 = 1)
		{
			string result;
			if (string_0 == null)
			{
				result = GClass0.smethod_0("");
			}
			else if (string_0.Length == 0)
			{
				result = GClass0.smethod_0("");
			}
			else
			{
				int num = (int_0 < 0) ? 0 : int_0;
				result = string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006490 File Offset: 0x00004690
		public string DupString(string string_0, int int_0)
		{
			string result;
			if (string_0 == null)
			{
				result = GClass0.smethod_0("");
			}
			else
			{
				string text = GClass0.smethod_0("");
				if (string_0.Length > 0)
				{
					for (int i = 0; i < int_0; i++)
					{
						text += string_0;
					}
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000064E4 File Offset: 0x000046E4
		public string FillString(string string_0, string string_1, int int_0, bool bool_0 = true)
		{
			string text = string_0;
			string result;
			if (string_1 == null || string_1 == GClass0.smethod_0(""))
			{
				result = text;
			}
			else
			{
				if (text.Length < int_0)
				{
					for (int i = 0; i < int_0; i++)
					{
						text += string_1;
					}
					if (bool_0)
					{
						text = this.RIGHT(text + string_0, int_0);
					}
					else
					{
						text = this.LEFT(string_0 + text, int_0);
					}
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000655C File Offset: 0x0000475C
		public string DelPre0(string string_0)
		{
			string text = GClass0.smethod_0("");
			bool flag = true;
			for (int i = 0; i < string_0.Length; i++)
			{
				string text2 = string_0[i].ToString();
				if (flag && (text2 == GClass0.smethod_0("1") || text2 == GClass0.smethod_0("!")))
				{
					flag = false;
				}
				else
				{
					flag = false;
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000065E0 File Offset: 0x000047E0
		public string NumberFormat(string string_0, string string_1 = "1.0", string string_2 = "0")
		{
			string text = GClass0.smethod_0("");
			List<string> list = new List<string>(string_1.Split(new char[]
			{
				'.'
			}));
			int num = Convert.ToInt32(list[0]);
			if (num <= 0)
			{
				num = 1;
			}
			string text2 = (list.Count<string>() > 1) ? list[1] : GClass0.smethod_0("1");
			int num2 = Convert.ToInt32(text2);
			string text3 = (this.LEFT(string_0, 1) == GClass0.smethod_0(",")) ? GClass0.smethod_0(",") : GClass0.smethod_0("");
			string text4 = (text3 == GClass0.smethod_0("")) ? string_0 : this.MID(string_0, 1, -9999);
			if (num2 > 0)
			{
				List<string> list2 = new List<string>(text4.Split(new char[]
				{
					'.'
				}));
				string text5 = list2[0];
				text5 = this.FillString(text5, string_2, num, true);
				text2 = ((list2.Count<string>() > 1) ? list2[1] : GClass0.smethod_0(""));
				text2 = this.FillString(text2, string_2, num2, false);
				text = ((text2.Length > 0) ? (text5 + GClass0.smethod_0("/") + text2) : text5);
				text = text3 + text;
			}
			else
			{
				text = text3 + this.FillString(text4, string_2, num, true);
			}
			return text;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000674C File Offset: 0x0000494C
		public int CountSubString(string string_0, string string_1)
		{
			int num = 0;
			int result;
			if (string_0 == null)
			{
				result = 0;
			}
			else
			{
				if (string_1.Length > 0 && string_0.Length > 0)
				{
					string text = string_0.Replace(string_1, GClass0.smethod_0(""));
					int num2 = text.Length - string_0.Length;
					num = num2 / string_1.Length;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000067AC File Offset: 0x000049AC
		private string method_1(string string_0)
		{
			string text = string_0.Trim();
			if (text.Length > 0)
			{
				int num = text.IndexOf(GClass0.smethod_0("/"));
				if (num > -1)
				{
					text = text.Substring(num + 1, text.Length - num - 1);
				}
				else
				{
					text = GClass0.smethod_0("");
				}
			}
			return text;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006808 File Offset: 0x00004A08
		private string method_2(double double_0)
		{
			string text = (double_0 == 0.0) ? GClass0.smethod_0("") : double_0.ToString();
			if (text.Length > 0)
			{
				int num = text.IndexOf(GClass0.smethod_0("/"));
				if (num > -1)
				{
					text = text.Substring(num + 1, text.Length - num - 1);
				}
				else
				{
					text = GClass0.smethod_0("");
				}
			}
			return text;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006880 File Offset: 0x00004A80
		public string WEEKDAY(DateTime dateTime_0, string string_0 = "C1")
		{
			string[] array = new string[]
			{
				GClass0.smethod_0("旤"),
				GClass0.smethod_0("丁"),
				GClass0.smethod_0("亍"),
				GClass0.smethod_0("丈"),
				GClass0.smethod_0("囚"),
				GClass0.smethod_0("井"),
				GClass0.smethod_0("公"),
				GClass0.smethod_0("旤")
			};
			int num = (int)Convert.ToInt16(dateTime_0.DayOfWeek);
			string text = array[num];
			string a = string_0.ToUpper();
			if (a == GClass0.smethod_0("LĶ"))
			{
				text = ((num == 0) ? GClass0.smethod_0("6") : num.ToString());
			}
			else if (a == GClass0.smethod_0("Lı"))
			{
				text = num.ToString();
			}
			else if (a == GClass0.smethod_0("Aĳ"))
			{
				text = GClass0.smethod_0("呩") + text;
			}
			else if (a == GClass0.smethod_0("AĲ"))
			{
				text = GClass0.smethod_0("昝昞") + text;
			}
			return text;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000069B0 File Offset: 0x00004BB0
		public bool DateTimeIsValid(string string_0)
		{
			DateTime dateTime;
			return DateTime.TryParse(string_0, out dateTime);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000069C8 File Offset: 0x00004BC8
		public string TimeSpanToString(TimeSpan timeSpan_0, string string_0 = "C", string string_1 = "")
		{
			string text = GClass0.smethod_0("");
			int days = timeSpan_0.Days;
			int num = days / 365;
			int num2 = days % 365 / 30;
			int num3 = num * 12 + num2;
			int num4 = (timeSpan_0.Days % 365 - num2 / 2) % 30;
			int hours = timeSpan_0.Hours;
			int minutes = timeSpan_0.Minutes;
			int seconds = timeSpan_0.Seconds;
			string text2 = num3.ToString();
			string str = days.ToString();
			string str2 = num.ToString();
			string text3 = num2.ToString();
			string str3 = num4.ToString();
			string text4 = hours.ToString();
			string str4 = minutes.ToString();
			string str5 = seconds.ToString();
			if (string_0 == GClass0.smethod_0("B") || string_0 == GClass0.smethod_0("b"))
			{
				if (string_1 == GClass0.smethod_0(""))
				{
					string_1 = GClass0.smethod_0("_ňɀͫѯղ");
				}
				if (string_1.Contains(GClass0.smethod_0("X")))
				{
					text = str2 + GClass0.smethod_0("幵");
				}
				if (string_1.Contains(GClass0.smethod_0("L")))
				{
					text = text + (string_1.Contains(GClass0.smethod_0("X")) ? text3 : text2) + GClass0.smethod_0("丨昉");
				}
				if (string_1.Contains(GClass0.smethod_0("E")))
				{
					if (string_1.Contains(GClass0.smethod_0("L")))
					{
						text = text + str3 + GClass0.smethod_0("夨");
					}
					else if (string_1.Contains(GClass0.smethod_0("X")))
					{
						text = text + (timeSpan_0.Days % 365).ToString() + GClass0.smethod_0("夨");
					}
					else
					{
						text = text + str + GClass0.smethod_0("夨");
					}
				}
				if (string_1.Contains(GClass0.smethod_0("i")))
				{
					if (string_1.Contains(GClass0.smethod_0("E")))
					{
						text = text + text4 + GClass0.smethod_0("對擷");
					}
					else if (string_1.Contains(GClass0.smethod_0("L")))
					{
						text = text + (num4 * 24 + hours).ToString() + GClass0.smethod_0("對擷");
					}
					else if (string_1.Contains(GClass0.smethod_0("X")))
					{
						text = text + (timeSpan_0.Days % 365 * 24 + hours).ToString() + GClass0.smethod_0("對擷");
					}
					else
					{
						text = text + this.Round(timeSpan_0.TotalHours, 0, 0, 45).ToString() + GClass0.smethod_0("對擷");
					}
				}
				if (string_1.Contains(GClass0.smethod_0("l")))
				{
					if (string_1.Contains(GClass0.smethod_0("i")))
					{
						text = text + str4 + GClass0.smethod_0("刄閞");
					}
					else if (string_1.Contains(GClass0.smethod_0("E")))
					{
						text = text + (hours * 60 + minutes).ToString() + GClass0.smethod_0("刄閞");
					}
					else if (string_1.Contains(GClass0.smethod_0("L")))
					{
						text = text + (num4 * 1440 + hours * 60 + minutes).ToString() + GClass0.smethod_0("刄閞");
					}
					else if (string_1.Contains(GClass0.smethod_0("X")))
					{
						text = text + (timeSpan_0.Days % 365 * 1440 + hours * 60 + minutes).ToString() + GClass0.smethod_0("刄閞");
					}
					else
					{
						text = text + this.Round(timeSpan_0.TotalMinutes, 0, 0, 45).ToString() + GClass0.smethod_0("刄閞");
					}
				}
				if (string_1.Contains(GClass0.smethod_0("r")))
				{
					if (string_1.Contains(GClass0.smethod_0("l")))
					{
						text = text + str5 + GClass0.smethod_0("秓");
					}
					else if (string_1.Contains(GClass0.smethod_0("i")))
					{
						text = text + (minutes * 60 + seconds).ToString() + GClass0.smethod_0("秓");
					}
					else if (string_1.Contains(GClass0.smethod_0("E")))
					{
						text = text + (hours * 3600 + minutes * 60 + seconds).ToString() + GClass0.smethod_0("秓");
					}
					else if (string_1.Contains(GClass0.smethod_0("L")))
					{
						text = text + (num4 * 86400 + hours * 3600 + minutes * 60 + seconds).ToString() + GClass0.smethod_0("秓");
					}
					else if (string_1.Contains(GClass0.smethod_0("X")))
					{
						text = text + (timeSpan_0.Days % 365 * 86400 + hours * 60 + minutes * 60 + seconds).ToString() + GClass0.smethod_0("秓");
					}
					else
					{
						text = text + this.Round(timeSpan_0.TotalSeconds, 0, 0, 45).ToString() + GClass0.smethod_0("秓");
					}
				}
			}
			else if (string_0 == GClass0.smethod_0("D") || string_0 == GClass0.smethod_0("d"))
			{
				if (string_1 == GClass0.smethod_0(""))
				{
					string_1 = GClass0.smethod_0("_ňɀͫѯղ");
				}
				text = GClass0.smethod_0(".");
				if (string_1.Contains(GClass0.smethod_0("X")))
				{
					text = str2 + GClass0.smethod_0(".");
				}
				if (string_1.Contains(GClass0.smethod_0("L")))
				{
					text += (string_1.Contains(GClass0.smethod_0("X")) ? text3 : text2);
				}
				text += GClass0.smethod_0(".");
				if (string_1.Contains(GClass0.smethod_0("E")))
				{
					if (string_1.Contains(GClass0.smethod_0("L")))
					{
						text += str3;
					}
					else if (string_1.Contains(GClass0.smethod_0("X")))
					{
						text += (timeSpan_0.Days % 365).ToString();
					}
					else
					{
						text += str;
					}
				}
				text = ((text == GClass0.smethod_0("-Į")) ? GClass0.smethod_0("") : text);
				string text5 = GClass0.smethod_0("3ĲȻ");
				if (string_1.Contains(GClass0.smethod_0("i")))
				{
					if (string_1.Contains(GClass0.smethod_0("E")))
					{
						text5 = text4 + GClass0.smethod_0(";");
					}
					else if (string_1.Contains(GClass0.smethod_0("L")))
					{
						text5 = (num4 * 24 + hours).ToString() + GClass0.smethod_0(";");
					}
					else if (string_1.Contains(GClass0.smethod_0("X")))
					{
						text5 = (timeSpan_0.Days % 365 * 24 + hours).ToString() + GClass0.smethod_0(";");
					}
					else
					{
						text5 = this.Round(timeSpan_0.TotalHours, 0, 0, 45).ToString() + GClass0.smethod_0(";");
					}
				}
				if (string_1.Contains(GClass0.smethod_0("l")))
				{
					if (string_1.Contains(GClass0.smethod_0("i")))
					{
						text5 = text5 + str4 + GClass0.smethod_0(";");
					}
					else if (string_1.Contains(GClass0.smethod_0("E")))
					{
						text5 = text5 + (hours * 60 + minutes).ToString() + GClass0.smethod_0(";");
					}
					else if (string_1.Contains(GClass0.smethod_0("L")))
					{
						text5 = text5 + (num4 * 1440 + hours * 60 + minutes).ToString() + GClass0.smethod_0(";");
					}
					else if (string_1.Contains(GClass0.smethod_0("X")))
					{
						text5 = text5 + (timeSpan_0.Days % 365 * 1440 + hours * 60 + minutes).ToString() + GClass0.smethod_0(";");
					}
					else
					{
						text5 = text5 + this.Round(timeSpan_0.TotalMinutes, 0, 0, 45).ToString() + GClass0.smethod_0(";");
					}
				}
				else
				{
					text5 += GClass0.smethod_0("3ĲȻ");
				}
				if (string_1.Contains(GClass0.smethod_0("r")))
				{
					if (string_1.Contains(GClass0.smethod_0("l")))
					{
						text5 += str5;
					}
					else if (string_1.Contains(GClass0.smethod_0("i")))
					{
						text5 += (minutes * 60 + seconds).ToString();
					}
					else if (string_1.Contains(GClass0.smethod_0("E")))
					{
						text5 += (hours * 3600 + minutes * 60 + seconds).ToString();
					}
					else if (string_1.Contains(GClass0.smethod_0("L")))
					{
						text5 += (num4 * 86400 + hours * 3600 + minutes * 60 + seconds).ToString();
					}
					else if (string_1.Contains(GClass0.smethod_0("X")))
					{
						text5 += (timeSpan_0.Days % 365 * 86400 + hours * 60 + minutes * 60 + seconds).ToString();
					}
					else
					{
						text5 += this.Round(timeSpan_0.TotalSeconds, 0, 0, 45).ToString();
					}
				}
				else
				{
					text5 += GClass0.smethod_0("2ı");
				}
				text = ((text5 == GClass0.smethod_0("8ķȼ̵дԹزܱ")) ? text : (text + GClass0.smethod_0("!") + text5));
			}
			else
			{
				int int_ = this.isValue(string_1) ? Convert.ToInt32(string_1) : 1;
				if (string_0 == GClass0.smethod_0("X"))
				{
					text = this.Round((double)timeSpan_0.Days / 365.0, int_, 0, 45).ToString();
				}
				else if (string_0 == GClass0.smethod_0("L"))
				{
					text = ((double)(num * 12) + this.Round(((double)timeSpan_0.Days % 365.0 - (double)(num2 / 2)) / 30.0, int_, 0, 45)).ToString();
				}
				else if (string_0 == GClass0.smethod_0("E"))
				{
					text = this.Round(timeSpan_0.TotalDays, int_, 0, 45).ToString();
				}
				else if (string_0 == GClass0.smethod_0("V"))
				{
					text = this.Round(timeSpan_0.TotalDays / 7.0, int_, 0, 45).ToString();
				}
				else if (string_0 == GClass0.smethod_0("i"))
				{
					text = this.Round(timeSpan_0.TotalHours, int_, 0, 45).ToString();
				}
				else if (string_0 == GClass0.smethod_0("l"))
				{
					text = this.Round(timeSpan_0.TotalMinutes, int_, 0, 45).ToString();
				}
				else if (string_0 == GClass0.smethod_0("r"))
				{
					text = this.Round(timeSpan_0.TotalSeconds, int_, 0, 45).ToString();
				}
			}
			return text;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000075F8 File Offset: 0x000057F8
		public bool StringToBool(string string_0)
		{
			bool result;
			if (string_0 == GClass0.smethod_0(""))
			{
				result = false;
			}
			else if (string_0 == GClass0.smethod_0("1"))
			{
				result = false;
			}
			else if (string_0 == GClass0.smethod_0("/ı"))
			{
				result = false;
			}
			else if (string_0.Trim().ToUpper() == GClass0.smethod_0("CŅɏ͑ф"))
			{
				result = false;
			}
			else if (string_0.Trim().ToUpper() == GClass0.smethod_0("Pőɗ̈́"))
			{
				result = true;
			}
			else
			{
				Regex regex = new Regex(GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ"));
				result = (!regex.IsMatch(string_0) || Convert.ToDouble(string_0) != 0.0);
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000076C0 File Offset: 0x000058C0
		public double BoolToDouble(bool bool_0)
		{
			return (double)(bool_0 ? 1 : 0);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000076D8 File Offset: 0x000058D8
		public double StringToDouble(string string_0)
		{
			double result;
			if (string_0 == GClass0.smethod_0(""))
			{
				result = 0.0;
			}
			else if (string_0 == GClass0.smethod_0("1"))
			{
				result = 0.0;
			}
			else if (string_0 == GClass0.smethod_0("/ı"))
			{
				result = 0.0;
			}
			else
			{
				Regex regex = new Regex(GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ"));
				result = (regex.IsMatch(string_0) ? Convert.ToDouble(string_0) : 0.0);
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000776C File Offset: 0x0000596C
		public int StringToInt(string string_0)
		{
			int result;
			if (string_0 == GClass0.smethod_0(""))
			{
				result = 0;
			}
			else if (string_0 == GClass0.smethod_0("1"))
			{
				result = 0;
			}
			else if (string_0 == GClass0.smethod_0("/ı"))
			{
				result = 0;
			}
			else
			{
				Regex regex = new Regex(GClass0.smethod_0("Kļɏ̿ѭՌؤܧ࠲ॐ੯ଡడൔษཚၡᄯሪጽᐥ"));
				result = (regex.IsMatch(string_0) ? Convert.ToInt32(string_0) : 0);
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000077E0 File Offset: 0x000059E0
		public bool StartProcess(string string_0, string string_1, string string_2, ProcessWindowStyle processWindowStyle_0, bool bool_0 = false, int int_0 = 0, bool bool_1 = false)
		{
			try
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo(string_0, string_2);
				processStartInfo.WindowStyle = processWindowStyle_0;
				processStartInfo.WorkingDirectory = string_1;
				Process process = new Process();
				process.StartInfo = processStartInfo;
				process.StartInfo.UseShellExecute = bool_1;
				process.StartInfo.CreateNoWindow = (processWindowStyle_0 == ProcessWindowStyle.Hidden);
				process.Start();
				if (bool_0)
				{
					if (!bool_1 && processWindowStyle_0 != ProcessWindowStyle.Hidden)
					{
						process.WaitForInputIdle();
					}
					if (int_0 > 0)
					{
						process.WaitForExit(int_0);
						if (!process.HasExited)
						{
							if (process.Responding)
							{
								process.CloseMainWindow();
							}
							else
							{
								process.Kill();
							}
						}
					}
					else
					{
						process.WaitForExit();
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(GClass0.smethod_0("吠厦岙瘤縀宅揿囲鴞ਈଉ徜寢") + ex.Message, GClass0.smethod_0("吩厭岐瘫縉宎"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return false;
		}

		// Token: 0x060000DF RID: 223
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowThreadProcessId(IntPtr intptr_0, out int int_0);

		// Token: 0x060000E0 RID: 224 RVA: 0x000078D4 File Offset: 0x00005AD4
		public void KillExcelProcess(Microsoft.Office.Interop.Excel.Application application_0)
		{
			IntPtr intptr_ = new IntPtr(application_0.Hwnd);
			int processId = 0;
			UDPX.GetWindowThreadProcessId(intptr_, out processId);
			Process processById = Process.GetProcessById(processId);
			processById.Kill();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007908 File Offset: 0x00005B08
		public void KillProcess(string string_0)
		{
			Process[] processes = Process.GetProcesses();
			foreach (Process process in processes)
			{
				if (process.ProcessName == string_0)
				{
					process.Kill();
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007944 File Offset: 0x00005B44
		public Bitmap ResizeImage(Bitmap bitmap_0, int int_0, int int_1)
		{
			Bitmap result;
			try
			{
				Bitmap bitmap = new Bitmap(int_0, int_1);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.DrawImage(bitmap_0, new System.Drawing.Rectangle(0, 0, int_0, int_1), new System.Drawing.Rectangle(0, 0, bitmap_0.Width, bitmap_0.Height), GraphicsUnit.Pixel);
				graphics.Dispose();
				result = bitmap;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000079AC File Offset: 0x00005BAC
		public bool ConnetIP(string string_0 = "www.baidu.com")
		{
			bool result = false;
			try
			{
				Ping ping = new Ping();
				PingOptions pingOptions = new PingOptions();
				pingOptions.DontFragment = true;
				string empty = string.Empty;
				byte[] bytes = Encoding.ASCII.GetBytes(empty);
				int timeout = 1200;
				IPAddress address;
				if (this.isIP(string_0))
				{
					address = IPAddress.Parse(string_0);
				}
				else
				{
					IPHostEntry hostByName;
					try
					{
						hostByName = Dns.GetHostByName(string_0);
					}
					catch (Exception)
					{
						return result;
					}
					address = hostByName.AddressList[0];
				}
				PingReply pingReply = ping.Send(address, timeout, bytes, pingOptions);
				if (pingReply.Status == IPStatus.Success)
				{
					result = true;
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228
		[DllImport("winInet.dll")]
		private static extern bool InternetGetConnectedState(ref int int_0, int int_1);

		// Token: 0x060000E5 RID: 229 RVA: 0x00007A60 File Offset: 0x00005C60
		public DateTime GetStandardTime(int int_0 = 8)
		{
			DateTime result = DateTime.Parse(GClass0.smethod_0("9ľȶ̴ЩԲدܰ"));
			try
			{
				string string_ = GClass0.smethod_0("?ļȹ̤иԺطܨ࠷पਲଲర");
				string string_2 = GClass0.smethod_0("2İȲ");
				SNTPTimeClient sntptimeClient = new SNTPTimeClient(string_, string_2);
				int num = 0;
				if (UDPX.InternetGetConnectedState(ref num, 0) && this.ConnetIP(GClass0.smethod_0("zŻɼ̤ѫթٮݢࡰप੠୭౬")))
				{
					sntptimeClient.Connect();
					string text = sntptimeClient.ToString();
					text = this.method_3(text);
					result = DateTime.Parse(text);
					result = result.AddHours((double)(int_0 - 8));
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007B00 File Offset: 0x00005D00
		private string method_3(string string_0)
		{
			string text = GClass0.smethod_0("");
			string[] array = string_0.Split(new char[]
			{
				Convert.ToChar(GClass0.smethod_0("\v"))
			}, 100);
			int num = -1;
			for (int i = 0; i < array.Length - 1; i++)
			{
				text = array[i];
				if (text.Substring(0, 10) == GClass0.smethod_0("FŦɫͦѪԥٰݪ࡯।"))
				{
					num = 1;
					if (num < 0)
					{
						text = GClass0.smethod_0("");
					}
					else
					{
						text = text.Substring(11);
					}
					return text;
				}
				Console.Write(text.Substring(0, 10) + GClass0.smethod_0("\v"));
			}
            return text;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public DateTime GetStandardTimeUSA(int int_0 = 8)
		{
			DateTime result = DateTime.Parse(GClass0.smethod_0("9ľȶ̴ЩԲدܰ"));
			try
			{
				int num = 0;
				if (UDPX.InternetGetConnectedState(ref num, 0))
				{
					string[,] array = new string[14, 2];
					int[] array2 = new int[]
					{
						3,
						2,
						4,
						8,
						9,
						6,
						11,
						5,
						10,
						0,
						1,
						7,
						12,
						13
					};
					array[0, 0] = GClass0.smethod_0("{ŧɠͩЦիاݦ࡮ॵੱପ౤൭๷");
					array[0, 1] = GClass0.smethod_0(":ĸȰ̦бԨشܱ࠭रਹ");
					array[1, 0] = GClass0.smethod_0("{ŧɠͩЦըاݦ࡮ॵੱପ౤൭๷");
					array[1, 1] = GClass0.smethod_0(":ĸȰ̦бԨشܱ࠭रਸ");
					array[2, 0] = GClass0.smethod_0("oųɴͽкշػݠࡺॿੴ୶౽൫๼༢ၩᅦቭ፺ᑣᕩᙦᜪᡤᥭ᩷");
					array[2, 1] = GClass0.smethod_0("<Ŀȹ̤иԾشܨ࠱पਲଲర");
					array[3, 0] = GClass0.smethod_0("oųɴͽкմػݠࡺॿੴ୶౽൫๼༢ၩᅦቭ፺ᑣᕩᙦᜪᡤᥭ᩷");
					array[3, 1] = GClass0.smethod_0("<Ŀȹ̤иԾشܨ࠱पਲଲళ");
					array[4, 0] = GClass0.smethod_0("oųɴͽкյػݠࡺॿੴ୶౽൫๼༢ၩᅦቭ፺ᑣᕩᙦᜪᡤᥭ᩷");
					array[4, 1] = GClass0.smethod_0("<Ŀȹ̤иԾشܨ࠱पਲଲల");
					array[5, 0] = GClass0.smethod_0("aŧɱͿѹռٺܣ࡯।੦୦౺൦๢ཪဪᅦቦ፴");
					array[5, 1] = GClass0.smethod_0("?Ŀȴ̥лԺذܩ࠷ऱ਴ଭశവ");
					array[6, 0] = GClass0.smethod_0("yťɦͯЧզٮݵࡱप੤୭౷");
					array[6, 1] = GClass0.smethod_0("<ĵȹ̤нԻةܴ࠱रਭଳహ");
					array[7, 0] = GClass0.smethod_0("dŦɣͨСեٽܧࡦ८ੵୱప൤๭ཷ");
					array[7, 1] = GClass0.smethod_0("=ĸȻ̧йԷرܫ࠵भਲ਼଱");
					array[8, 0] = GClass0.smethod_0("{ŽɠͦРԾټݷࡠॡ੮୾౻ൡ๤ཀྵၨᄪበ፭ᑬ");
					array[8, 1] = GClass0.smethod_0("=ĳȧ̺вԨؼܲ࠭ळਲ");
					array[9, 0] = GClass0.smethod_0("zźɡͥСԢ٪ݮࠢ६੦୨౻൴๣ོဪᅠቭ፬");
					array[9, 1] = GClass0.smethod_0(">ĺȼ̧кԷضܫ࠽रਬହ");
					array[10, 0] = GClass0.smethod_0("zźɡͥСԢ٠ݴࠢ६੦୨౻൴๣ོဪᅠቭ፬");
					array[10, 1] = GClass0.smethod_0(">ĻȲ̧йԿزܫ࠰ऺਬସ");
					array[11, 0] = GClass0.smethod_0("zźɡͥСԢٽݧࠢ६੦୨౻൴๣ོဪᅠቭ፬");
					array[11, 1] = GClass0.smethod_0("<ĽȻ̥лԻؾܩ࠿ऽਪ଱లവ");
					array[12, 0] = GClass0.smethod_0("wűɤ͢ФԺٲݽࡽऽ੬୯ణ൸๹ཿၬᅼቮ፫ᑠᔪᙠ᝭ᡬ");
					array[12, 1] = GClass0.smethod_0("<ĽȻ̥иԹظܩ࠾ऴਪଲళല");
					array[13, 0] = GClass0.smethod_0("wűɤ͢ФԺٲݽࡽऽ੹୯ణ൸๹ཿၬᅼቮ፫ᑠᔪᙠ᝭ᡬ");
					array[13, 1] = GClass0.smethod_0(":ĿȤ̻лԱبܼ࠲भ਷ଲ");
					int port = 13;
					byte[] array3 = new byte[1024];
					int count = 0;
					TcpClient tcpClient = new TcpClient();
					for (int i = 0; i < 14; i++)
					{
						string text = array[array2[i], 1];
						if (this.ConnetIP(text))
						{
							try
							{
								tcpClient.Connect(text, port);
								NetworkStream stream = tcpClient.GetStream();
								count = stream.Read(array3, 0, array3.Length);
								tcpClient.Close();
								break;
							}
							catch (Exception)
							{
							}
						}
					}
					char[] separator = new char[]
					{
						' '
					};
					result = default(DateTime);
					string @string = Encoding.ASCII.GetString(array3, 0, count);
					string[] array4 = @string.Split(separator);
					if (array4.Length >= 2)
					{
						result = DateTime.Parse(array4[1] + GClass0.smethod_0("!") + array4[2]);
						result = result.AddHours((double)int_0);
					}
					else
					{
						result = DateTime.Parse(GClass0.smethod_0("9ľȶ̴ЩԲدܰ"));
					}
				}
			}
			catch (Exception)
			{
				result = DateTime.Parse(GClass0.smethod_0("9ľȶ̴ЩԲدܰ"));
			}
			return result;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007F3C File Offset: 0x0000613C
		[STAThread]
		public bool ExportResource(string string_0, string string_1)
		{
			bool result;
			try
			{
				string str = Assembly.GetExecutingAssembly().GetName().Name.ToString() + GClass0.smethod_0("%ŘɬͻѨճٷݧࡦॱਯ");
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(str + string_0);
				if (manifestResourceStream == null)
				{
					result = false;
				}
				else
				{
					byte[] array = new byte[manifestResourceStream.Length];
					manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
					File.WriteAllBytes(string_1, array);
					result = true;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007FCC File Offset: 0x000061CC
		[STAThread]
		public bool RunDotNetOfResource(string string_0, string[] string_1)
		{
			bool flag = true;
			string str = Assembly.GetExecutingAssembly().GetName().Name.ToString() + GClass0.smethod_0("%ŘɬͻѨճٷݧࡦॱਯ");
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(str + string_0);
			bool result;
			if (manifestResourceStream == null)
			{
				result = false;
			}
			else
			{
				byte[] array = new byte[manifestResourceStream.Length];
				manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
				Assembly assembly = Assembly.Load(array);
				MethodInfo entryPoint = assembly.EntryPoint;
				ParameterInfo[] parameters = entryPoint.GetParameters();
				if (parameters != null && parameters.Length != 0)
				{
					entryPoint.Invoke(null, string_1);
				}
				else
				{
					entryPoint.Invoke(null, null);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008080 File Offset: 0x00006280
		public string GetSystemPath(string string_0)
		{
			string result = GClass0.smethod_0("");
			string a = string_0.ToUpper();
			if (a == GClass0.smethod_0("Dœɗ͖цՌٕ") || a == GClass0.smethod_0(""))
			{
				result = Environment.CurrentDirectory;
			}
			else if (a == GClass0.smethod_0("[Şɕ͑сՎرܳ"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.System);
			}
			else if (a == GClass0.smethod_0("UŜɗ͗чՌ"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + GClass0.smethod_0("[ŕɼͷѷէ٬");
			}
			else if (a == GClass0.smethod_0("Pŏɋ̀ьՕْ"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
			}
			else if (a == GClass0.smethod_0("CŃɖ͏їՍّ"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			}
			else if (a == GClass0.smethod_0("FŅɀ͊ф"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
			}
			else if (a == GClass0.smethod_0("Fœɍ͇фՓو݁ࡍॖ੒"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			}
			else if (a == GClass0.smethod_0("LŁɀ́фՄٍ݇ࡄ॓ੈୁ్ൖ๒"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
			}
			else if (a == GClass0.smethod_0("NŃɆ͇цՆك݃ࡖॏ੗୍౑"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
			}
			else if (a == GClass0.smethod_0("HŅɄͅшՈوݑࡐोੂ"))
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
			}
			else
			{
				string environmentVariable = Environment.GetEnvironmentVariable(string_0);
				if (environmentVariable != null)
				{
					result = environmentVariable;
				}
			}
			return result;
		}

		// Token: 0x060000EB RID: 235
		[DllImport("Kernel32.dll")]
		private static extern bool Beep(int int_0, int int_1);

		// Token: 0x060000EC RID: 236 RVA: 0x0000226F File Offset: 0x0000046F
		public void BEEP(int int_0 = 500, int int_1 = 700)
		{
			UDPX.Beep(int_0, int_1);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002279 File Offset: 0x00000479
		public void RefreshUI()
		{
		}

		// Token: 0x04000063 RID: 99
		private const int INTERNET_CONNECTION_MODEM = 1;

		// Token: 0x04000064 RID: 100
		private const int INTERNET_CONNECTION_LAN = 2;
	}
}
