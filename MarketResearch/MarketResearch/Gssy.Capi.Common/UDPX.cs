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
	public class UDPX
	{
		public bool isNumberic(string string_0)
		{
			Regex regex = new Regex("^\\d+$");
			return regex.IsMatch(string_0);
		}

		public bool isValue(string string_0)
		{
			Regex regex = new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$");
			return regex.IsMatch(string_0);
		}

		public bool isLetter(string string_0)
		{
			Regex regex = new Regex("^[A-Za-z]+$");
			return regex.IsMatch(string_0);
		}

		public bool isEmail(string string_0)
		{
			Regex regex = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
			return regex.IsMatch(string_0);
		}

		public bool isHttp(string string_0)
		{
			string input = (this.MID(string_0, 7, -9999).ToLower() == "http://") ? string_0 : ("http://" + string_0);
			Regex regex = new Regex("http://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?");
			return regex.IsMatch(input);
		}

		public bool isChinese(string string_0)
		{
			Regex regex = new Regex("^[\u4E00-\u9FA5]+$");
			return regex.IsMatch(string_0);
		}

		public bool isWord(string string_0)
		{
			Regex regex = new Regex("^[A-Za-z\\d\u4E00-\u9FA5]+$");
			return regex.IsMatch(string_0);
		}

		public bool isIP(string string_0)
		{
			Regex regex = new Regex("^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$");
			return regex.IsMatch(string_0);
		}

		public bool isMatch(string string_0, string string_1)
		{
			Regex regex = new Regex(string_1);
			return regex.IsMatch(string_0);
		}

		public void WriteListAppendToFile(List<string> list_0, string string_0, string string_1 = "Default", string string_2 = "\r\n")
		{
			string_1 = string_1.ToUpper();
			Encoding encoding;
			if (string_1 == "DEFAULT")
			{
				encoding = Encoding.Default;
			}
			else if (string_1 == "ASCII")
			{
				encoding = Encoding.ASCII;
			}
			else if (string_1 == "UNICODE")
			{
				encoding = Encoding.Unicode;
			}
			else if (string_1 == "UTF32")
			{
				encoding = Encoding.UTF32;
			}
			else if (string_1 == "UTF8")
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

		public void WriteListToFile(List<string> list_0, string string_0, bool bool_0 = false, string string_1 = "Default", string string_2 = "\r\n")
		{
			string_1 = string_1.ToUpper();
			Encoding encoding;
			if (string_1 == "DEFAULT")
			{
				encoding = Encoding.Default;
			}
			else if (string_1 == "ASCII")
			{
				encoding = Encoding.ASCII;
			}
			else if (string_1 == "UNICODE")
			{
				encoding = Encoding.Unicode;
			}
			else if (string_1 == "UTF32")
			{
				encoding = Encoding.UTF32;
			}
			else if (string_1 == "UTF8")
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

		public List<string> ReadTextFileToList(string string_0, string string_1 = "\r\n", string string_2 = "Default")
		{
			string_2 = string_2.ToUpper();
			Encoding encoding;
			if (string_2 == "DEFAULT")
			{
				encoding = Encoding.Default;
			}
			else if (string_2 == "ASCII")
			{
				encoding = Encoding.ASCII;
			}
			else if (string_2 == "UNICODE")
			{
				encoding = Encoding.Unicode;
			}
			else if (string_2 == "UTF32")
			{
				encoding = Encoding.UTF32;
			}
			else if (string_2 == "UTF8")
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

		public void WriteStringAppendToFile(string string_0, string string_1, string string_2 = "Default")
		{
			string_2 = string_2.ToUpper();
			Encoding encoding;
			if (string_2 == "DEFAULT")
			{
				encoding = Encoding.Default;
			}
			else if (string_2 == "ASCII")
			{
				encoding = Encoding.ASCII;
			}
			else if (string_2 == "UNICODE")
			{
				encoding = Encoding.Unicode;
			}
			else if (string_2 == "UTF32")
			{
				encoding = Encoding.UTF32;
			}
			else if (string_2 == "UTF8")
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

		public void AppendWrite(string string_0, string string_1, string string_2 = "Default")
		{
			string_2 = string_2.ToUpper();
			Encoding encoding;
			if (string_2 == "DEFAULT")
			{
				encoding = Encoding.Default;
			}
			else if (string_2 == "ASCII")
			{
				encoding = Encoding.ASCII;
			}
			else if (string_2 == "UNICODE")
			{
				encoding = Encoding.Unicode;
			}
			else if (string_2 == "UTF32")
			{
				encoding = Encoding.UTF32;
			}
			else if (string_2 == "UTF8")
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

		public void WriteNew(string string_0, string string_1, string string_2 = "Default")
		{
			string_2 = string_2.ToUpper();
			Encoding encoding;
			if (string_2 == "DEFAULT")
			{
				encoding = Encoding.Default;
			}
			else if (string_2 == "ASCII")
			{
				encoding = Encoding.ASCII;
			}
			else if (string_2 == "UNICODE")
			{
				encoding = Encoding.Unicode;
			}
			else if (string_2 == "UTF32")
			{
				encoding = Encoding.UTF32;
			}
			else if (string_2 == "UTF8")
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

		public string SetValueOfKeyAtList(List<string> list_0, string string_0, string string_1, bool bool_0 = true, int int_0 = 0, string string_2 = "", bool bool_1 = true)
		{
			string result = "";
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

		public string SetValueOfKeyAtFile(string string_0, string string_1, string string_2, bool bool_0 = true, int int_0 = 0, string string_3 = "", bool bool_1 = true, string string_4 = "Default")
		{
			string result = "";
			List<string> list_ = this.ReadTextFileToList(string_0, "\r\n", string_4);
			result = this.SetValueOfKeyAtList(list_, string_1, string_2, bool_0, int_0, string_3, bool_1);
			this.WriteListToFile(list_, string_0, false, string_4, "\r\n");
			return result;
		}

		public string GetValueOfKeyAtList(List<string> list_0, string string_0, int int_0 = 0, string string_1 = "")
		{
			string result = "";
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

		public string GetValueOfKeyAtFile(string string_0, string string_1, int int_0 = 0, string string_2 = "", string string_3 = "Default")
		{
			string text = "";
			List<string> list_ = this.ReadTextFileToList(string_0, "\r\n", string_3);
			return this.GetValueOfKeyAtList(list_, string_1, int_0, string_2);
		}

		public string ReadTextFileToString(string string_0, string string_1 = "Default")
		{
			string_1 = string_1.ToUpper();
			Encoding encoding;
			if (string_1 == "ASCII")
			{
				encoding = Encoding.ASCII;
			}
			else if (string_1 == "UNICODE")
			{
				encoding = Encoding.Unicode;
			}
			else if (string_1 == "UTF32")
			{
				encoding = Encoding.UTF32;
			}
			else if (string_1 == "UTF8")
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

		public double INT(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 0)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

		public double Floor(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 0)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

		public double RoundDown(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 0)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

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

		public double Top(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 1)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

		public double RoundUp(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 1)
		{
			return this.Round(double_0, int_0, int_1, int_2);
		}

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

		private double method_0(double double_0)
		{
			string text = double_0.ToString();
			int num = text.IndexOf(".");
			if (num > -1)
			{
				text = "0." + text.Substring(num + 1, text.Length - num - 1);
			}
			else
			{
				text = "0";
			}
			return Convert.ToDouble(text);
		}

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

		public string ArrayToString(string[] string_0, string string_1 = "", bool bool_0 = false, string string_2 = "")
		{
			string text = "";
			foreach (string text2 in string_0)
			{
				if (string_2 == "")
				{
					if (bool_0 || text2 != "")
					{
						text = text + string_1 + text2;
					}
				}
				else
				{
					text = text + string_1 + this.NumberFormat(text2, string_2, "0");
				}
			}
			if (text.Length > string_1.Length && string_1.Length > 0)
			{
				text = this.MID(text, string_1.Length, -9999);
			}
			return text;
		}

		public string ListToString(List<string> list_0, string string_0 = "", bool bool_0 = false, string string_1 = "")
		{
			string text = "";
			foreach (string text2 in list_0)
			{
				if (string_1 == "")
				{
					if (bool_0 || text2 != "")
					{
						text = text + string_0 + text2;
					}
				}
				else
				{
					text = text + string_0 + this.NumberFormat(text2, string_1, "0");
				}
			}
			if (text.Length > string_0.Length && string_0.Length > 0)
			{
				text = this.MID(text, string_0.Length, -9999);
			}
			return text;
		}

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
			return "";
		}

		public Dictionary<string, string> SortDictSDbyValue(Dictionary<string, double> dictionary_0, bool bool_0 = false, bool bool_1 = false, string string_0 = ",")
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<int> list = new List<int>();
			for (int i = 1; i <= dictionary_0.Count; i++)
			{
				list.Add(1);
				dictionary.Add(i.ToString(), "");
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
				if (dictionary[key3] == "")
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

		public List<string> StringToList(string string_0, string string_1 = "\r\n")
		{
			return new List<string>(string_0.Split(new string[]
			{
				string_1
			}, StringSplitOptions.RemoveEmptyEntries));
		}

		public string[] StringToArray(string string_0, char char_0 = ',')
		{
			return string_0.Split(new char[]
			{
				char_0
			});
		}

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
				result = "";
			}
			return result;
		}

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

		public string SubStringFromStartToEnd(string string_0, int int_0, int int_1 = -9999)
		{
			string result;
			if (string_0 == null)
			{
				result = "";
			}
			else if (string_0.Length == 0)
			{
				result = "";
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
					result = "";
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

		public string LEFT(string string_0, int int_0 = 1)
		{
			string result;
			if (string_0 == null)
			{
				result = "";
			}
			else if (string_0.Length == 0)
			{
				result = "";
			}
			else
			{
				int num = (int_0 < 0) ? 0 : int_0;
				result = string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
			}
			return result;
		}

		public string MID(string string_0, int int_0, int int_1 = -9999)
		{
			string result;
			if (string_0 == null)
			{
				result = "";
			}
			else if (string_0.Length == 0)
			{
				result = "";
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

		public string RIGHT(string string_0, int int_0 = 1)
		{
			string result;
			if (string_0 == null)
			{
				result = "";
			}
			else if (string_0.Length == 0)
			{
				result = "";
			}
			else
			{
				int num = (int_0 < 0) ? 0 : int_0;
				result = string_0.Substring((num > string_0.Length) ? 0 : (string_0.Length - num));
			}
			return result;
		}

		public string DupString(string string_0, int int_0)
		{
			string result;
			if (string_0 == null)
			{
				result = "";
			}
			else
			{
				string text = "";
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

		public string FillString(string string_0, string string_1, int int_0, bool bool_0 = true)
		{
			string text = string_0;
			string result;
			if (string_1 == null || string_1 == "")
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

		public string DelPre0(string string_0)
		{
			string text = "";
			bool flag = true;
			for (int i = 0; i < string_0.Length; i++)
			{
				string text2 = string_0[i].ToString();
				if (flag && (text2 == "0" || text2 == " "))
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

		public string NumberFormat(string string_0, string string_1 = "1.0", string string_2 = "0")
		{
			string text = "";
			List<string> list = new List<string>(string_1.Split(new char[]
			{
				'.'
			}));
			int num = Convert.ToInt32(list[0]);
			if (num <= 0)
			{
				num = 1;
			}
			string text2 = (list.Count<string>() > 1) ? list[1] : "0";
			int num2 = Convert.ToInt32(text2);
			string text3 = (this.LEFT(string_0, 1) == "-") ? "-" : "";
			string text4 = (text3 == "") ? string_0 : this.MID(string_0, 1, -9999);
			if (num2 > 0)
			{
				List<string> list2 = new List<string>(text4.Split(new char[]
				{
					'.'
				}));
				string text5 = list2[0];
				text5 = this.FillString(text5, string_2, num, true);
				text2 = ((list2.Count<string>() > 1) ? list2[1] : "");
				text2 = this.FillString(text2, string_2, num2, false);
				text = ((text2.Length > 0) ? (text5 + "." + text2) : text5);
				text = text3 + text;
			}
			else
			{
				text = text3 + this.FillString(text4, string_2, num, true);
			}
			return text;
		}

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
					string text = string_0.Replace(string_1, "");
					int num2 = text.Length - string_0.Length;
					num = num2 / string_1.Length;
				}
				result = num;
			}
			return result;
		}

		private string method_1(string string_0)
		{
			string text = string_0.Trim();
			if (text.Length > 0)
			{
				int num = text.IndexOf(".");
				if (num > -1)
				{
					text = text.Substring(num + 1, text.Length - num - 1);
				}
				else
				{
					text = "";
				}
			}
			return text;
		}

		private string method_2(double double_0)
		{
			string text = (double_0 == 0.0) ? "" : double_0.ToString();
			if (text.Length > 0)
			{
				int num = text.IndexOf(".");
				if (num > -1)
				{
					text = text.Substring(num + 1, text.Length - num - 1);
				}
				else
				{
					text = "";
				}
			}
			return text;
		}

		public string WEEKDAY(DateTime dateTime_0, string string_0 = "C1")
		{
			string[] array = new string[]
			{
				"日",
				"一",
				"二",
				"三",
				"四",
				"五",
				"六",
				"日"
			};
			int num = (int)Convert.ToInt16(dateTime_0.DayOfWeek);
			string text = array[num];
			string a = string_0.ToUpper();
			if (a == "N7")
			{
				text = ((num == 0) ? "7" : num.ToString());
			}
			else if (a == "N0")
			{
				text = num.ToString();
			}
			else if (a == "C2")
			{
				text = "周" + text;
			}
			else if (a == "C3")
			{
				text = "星期" + text;
			}
			return text;
		}

		public bool DateTimeIsValid(string string_0)
		{
			DateTime dateTime;
			return DateTime.TryParse(string_0, out dateTime);
		}

		public string TimeSpanToString(TimeSpan timeSpan_0, string string_0 = "C", string string_1 = "")
		{
			string text = "";
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
			if (string_0 == "C" || string_0 == "c")
			{
				if (string_1 == "")
				{
					string_1 = "YMDhms";
				}
				if (string_1.Contains("Y"))
				{
					text = str2 + "年";
				}
				if (string_1.Contains("M"))
				{
					text = text + (string_1.Contains("Y") ? text3 : text2) + "个月";
				}
				if (string_1.Contains("D"))
				{
					if (string_1.Contains("M"))
					{
						text = text + str3 + "天";
					}
					else if (string_1.Contains("Y"))
					{
						text = text + (timeSpan_0.Days % 365).ToString() + "天";
					}
					else
					{
						text = text + str + "天";
					}
				}
				if (string_1.Contains("h"))
				{
					if (string_1.Contains("D"))
					{
						text = text + text4 + "小时";
					}
					else if (string_1.Contains("M"))
					{
						text = text + (num4 * 24 + hours).ToString() + "小时";
					}
					else if (string_1.Contains("Y"))
					{
						text = text + (timeSpan_0.Days % 365 * 24 + hours).ToString() + "小时";
					}
					else
					{
						text = text + this.Round(timeSpan_0.TotalHours, 0, 0, 45).ToString() + "小时";
					}
				}
				if (string_1.Contains("m"))
				{
					if (string_1.Contains("h"))
					{
						text = text + str4 + "分钟";
					}
					else if (string_1.Contains("D"))
					{
						text = text + (hours * 60 + minutes).ToString() + "分钟";
					}
					else if (string_1.Contains("M"))
					{
						text = text + (num4 * 1440 + hours * 60 + minutes).ToString() + "分钟";
					}
					else if (string_1.Contains("Y"))
					{
						text = text + (timeSpan_0.Days % 365 * 1440 + hours * 60 + minutes).ToString() + "分钟";
					}
					else
					{
						text = text + this.Round(timeSpan_0.TotalMinutes, 0, 0, 45).ToString() + "分钟";
					}
				}
				if (string_1.Contains("s"))
				{
					if (string_1.Contains("m"))
					{
						text = text + str5 + "秒";
					}
					else if (string_1.Contains("h"))
					{
						text = text + (minutes * 60 + seconds).ToString() + "秒";
					}
					else if (string_1.Contains("D"))
					{
						text = text + (hours * 3600 + minutes * 60 + seconds).ToString() + "秒";
					}
					else if (string_1.Contains("M"))
					{
						text = text + (num4 * 86400 + hours * 3600 + minutes * 60 + seconds).ToString() + "秒";
					}
					else if (string_1.Contains("Y"))
					{
						text = text + (timeSpan_0.Days % 365 * 86400 + hours * 60 + minutes * 60 + seconds).ToString() + "秒";
					}
					else
					{
						text = text + this.Round(timeSpan_0.TotalSeconds, 0, 0, 45).ToString() + "秒";
					}
				}
			}
			else if (string_0 == "E" || string_0 == "e")
			{
				if (string_1 == "")
				{
					string_1 = "YMDhms";
				}
				text = "/";
				if (string_1.Contains("Y"))
				{
					text = str2 + "/";
				}
				if (string_1.Contains("M"))
				{
					text += (string_1.Contains("Y") ? text3 : text2);
				}
				text += "/";
				if (string_1.Contains("D"))
				{
					if (string_1.Contains("M"))
					{
						text += str3;
					}
					else if (string_1.Contains("Y"))
					{
						text += (timeSpan_0.Days % 365).ToString();
					}
					else
					{
						text += str;
					}
				}
				text = ((text == "//") ? "" : text);
				string text5 = "00:";
				if (string_1.Contains("h"))
				{
					if (string_1.Contains("D"))
					{
						text5 = text4 + ":";
					}
					else if (string_1.Contains("M"))
					{
						text5 = (num4 * 24 + hours).ToString() + ":";
					}
					else if (string_1.Contains("Y"))
					{
						text5 = (timeSpan_0.Days % 365 * 24 + hours).ToString() + ":";
					}
					else
					{
						text5 = this.Round(timeSpan_0.TotalHours, 0, 0, 45).ToString() + ":";
					}
				}
				if (string_1.Contains("m"))
				{
					if (string_1.Contains("h"))
					{
						text5 = text5 + str4 + ":";
					}
					else if (string_1.Contains("D"))
					{
						text5 = text5 + (hours * 60 + minutes).ToString() + ":";
					}
					else if (string_1.Contains("M"))
					{
						text5 = text5 + (num4 * 1440 + hours * 60 + minutes).ToString() + ":";
					}
					else if (string_1.Contains("Y"))
					{
						text5 = text5 + (timeSpan_0.Days % 365 * 1440 + hours * 60 + minutes).ToString() + ":";
					}
					else
					{
						text5 = text5 + this.Round(timeSpan_0.TotalMinutes, 0, 0, 45).ToString() + ":";
					}
				}
				else
				{
					text5 += "00:";
				}
				if (string_1.Contains("s"))
				{
					if (string_1.Contains("m"))
					{
						text5 += str5;
					}
					else if (string_1.Contains("h"))
					{
						text5 += (minutes * 60 + seconds).ToString();
					}
					else if (string_1.Contains("D"))
					{
						text5 += (hours * 3600 + minutes * 60 + seconds).ToString();
					}
					else if (string_1.Contains("M"))
					{
						text5 += (num4 * 86400 + hours * 3600 + minutes * 60 + seconds).ToString();
					}
					else if (string_1.Contains("Y"))
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
					text5 += "00";
				}
				text = ((text5 == "00:00:00") ? text : (text + " " + text5));
			}
			else
			{
				int int_ = this.isValue(string_1) ? Convert.ToInt32(string_1) : 1;
				if (string_0 == "Y")
				{
					text = this.Round((double)timeSpan_0.Days / 365.0, int_, 0, 45).ToString();
				}
				else if (string_0 == "M")
				{
					text = ((double)(num * 12) + this.Round(((double)timeSpan_0.Days % 365.0 - (double)(num2 / 2)) / 30.0, int_, 0, 45)).ToString();
				}
				else if (string_0 == "D")
				{
					text = this.Round(timeSpan_0.TotalDays, int_, 0, 45).ToString();
				}
				else if (string_0 == "W")
				{
					text = this.Round(timeSpan_0.TotalDays / 7.0, int_, 0, 45).ToString();
				}
				else if (string_0 == "h")
				{
					text = this.Round(timeSpan_0.TotalHours, int_, 0, 45).ToString();
				}
				else if (string_0 == "m")
				{
					text = this.Round(timeSpan_0.TotalMinutes, int_, 0, 45).ToString();
				}
				else if (string_0 == "s")
				{
					text = this.Round(timeSpan_0.TotalSeconds, int_, 0, 45).ToString();
				}
			}
			return text;
		}

		public bool StringToBool(string string_0)
		{
			bool result;
			if (string_0 == "")
			{
				result = false;
			}
			else if (string_0 == "0")
			{
				result = false;
			}
			else if (string_0 == "-0")
			{
				result = false;
			}
			else if (string_0.Trim().ToUpper() == "FALSE")
			{
				result = false;
			}
			else if (string_0.Trim().ToUpper() == "TRUE")
			{
				result = true;
			}
			else
			{
				Regex regex = new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$");
				result = (!regex.IsMatch(string_0) || Convert.ToDouble(string_0) != 0.0);
			}
			return result;
		}

		public double BoolToDouble(bool bool_0)
		{
			return (double)(bool_0 ? 1 : 0);
		}

		public double StringToDouble(string string_0)
		{
			double result;
			if (string_0 == "")
			{
				result = 0.0;
			}
			else if (string_0 == "0")
			{
				result = 0.0;
			}
			else if (string_0 == "-0")
			{
				result = 0.0;
			}
			else
			{
				Regex regex = new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$");
				result = (regex.IsMatch(string_0) ? Convert.ToDouble(string_0) : 0.0);
			}
			return result;
		}

		public int StringToInt(string string_0)
		{
			int result;
			if (string_0 == "")
			{
				result = 0;
			}
			else if (string_0 == "0")
			{
				result = 0;
			}
			else if (string_0 == "-0")
			{
				result = 0;
			}
			else
			{
				Regex regex = new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$");
				result = (regex.IsMatch(string_0) ? Convert.ToInt32(string_0) : 0);
			}
			return result;
		}

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
				MessageBox.Show("启动应用程序时出错！\r\r原因：" + ex.Message, "启动应用程序", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return false;
		}

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowThreadProcessId(IntPtr intptr_0, out int int_0);

		public void KillExcelProcess(Microsoft.Office.Interop.Excel.Application application_0)
		{
			IntPtr intptr_ = new IntPtr(application_0.Hwnd);
			int processId = 0;
			UDPX.GetWindowThreadProcessId(intptr_, out processId);
			Process processById = Process.GetProcessById(processId);
			processById.Kill();
		}

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

		[DllImport("winInet.dll")]
		private static extern bool InternetGetConnectedState(ref int int_0, int int_1);

		public DateTime GetStandardTime(int int_0 = 8)
		{
			DateTime result = DateTime.Parse("1901-1-1");
			try
			{
				string string_ = "202.120.2.101";
				string string_2 = "123";
				SNTPTimeClient sntptimeClient = new SNTPTimeClient(string_, string_2);
				int num = 0;
				if (UDPX.InternetGetConnectedState(ref num, 0) && this.ConnetIP("www.baidu.com"))
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

		private string method_3(string string_0)
		{
			string text = "";
			string[] array = string_0.Split(new char[]
			{
				Convert.ToChar("\\n")
			}, 100);
			int num = -1;
			for (int i = 0; i < array.Length - 1; i++)
			{
				text = array[i];
				if (text.Substring(0, 10) == "Local time")
				{
					num = 1;
					if (num < 0)
					{
						text = "";
					}
					else
					{
						text = text.Substring(11);
					}
					return text;
				}
				Console.Write(text.Substring(0, 10) + "\\n");
			}
            return text;
		}

		public DateTime GetStandardTimeUSA(int int_0 = 8)
		{
			DateTime result = DateTime.Parse("1901-1-1");
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
					array[0, 0] = "time-a.nist.gov";
					array[0, 1] = "129.6.15.28";
					array[1, 0] = "time-b.nist.gov";
					array[1, 1] = "129.6.15.29";
					array[2, 0] = "time-a.timefreq.bldrdoc.gov";
					array[2, 1] = "132.163.4.101";
					array[3, 0] = "time-b.timefreq.bldrdoc.gov";
					array[3, 1] = "132.163.4.102";
					array[4, 0] = "time-c.timefreq.bldrdoc.gov";
					array[4, 1] = "132.163.4.103";
					array[5, 0] = "utcnist.colorado.edu";
					array[5, 1] = "128.138.140.44";
					array[6, 0] = "time.nist.gov";
					array[6, 1] = "192.43.244.18";
					array[7, 0] = "time-nw.nist.gov";
					array[7, 1] = "131.107.1.10";
					array[8, 0] = "nist1.symmetricom.com";
					array[8, 1] = "69.25.96.13";
					array[9, 0] = "nist1-dc.glassey.com";
					array[9, 1] = "216.200.93.8";
					array[10, 0] = "nist1-ny.glassey.com";
					array[10, 1] = "208.184.49.9";
					array[11, 0] = "nist1-sj.glassey.com";
					array[11, 1] = "207.126.98.204";
					array[12, 0] = "nist1.aol-ca.truetime.com";
					array[12, 1] = "207.200.81.113";
					array[13, 0] = "nist1.aol-va.truetime.com";
					array[13, 1] = "64.236.96.53";
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
						result = DateTime.Parse(array4[1] + " " + array4[2]);
						result = result.AddHours((double)int_0);
					}
					else
					{
						result = DateTime.Parse("1901-1-1");
					}
				}
			}
			catch (Exception)
			{
				result = DateTime.Parse("1901-1-1");
			}
			return result;
		}

		[STAThread]
		public bool ExportResource(string string_0, string string_1)
		{
			bool result;
			try
			{
				string str = Assembly.GetExecutingAssembly().GetName().Name.ToString() + ".Resources.";
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

		[STAThread]
		public bool RunDotNetOfResource(string string_0, string[] string_1)
		{
			bool flag = true;
			string str = Assembly.GetExecutingAssembly().GetName().Name.ToString() + ".Resources.";
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

		public string GetSystemPath(string string_0)
		{
			string result = "";
			string a = string_0.ToUpper();
			if (a == "CURRENT" || a == "")
			{
				result = Environment.CurrentDirectory;
			}
			else if (a == "SYSTEM32")
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.System);
			}
			else if (a == "SYSTEM")
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\System";
			}
			else if (a == "WINDOWS")
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
			}
			else if (a == "DESKTOP")
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			}
			else if (a == "CACHE")
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
			}
			else if (a == "MYDOCUMENTS")
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			}
			else if (a == "COMMONDOCUMENTS")
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
			}
			else if (a == "COMMONDESKTOP")
			{
				result = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
			}
			else if (a == "COMMONMUSIC")
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

		[DllImport("Kernel32.dll")]
		private static extern bool Beep(int int_0, int int_1);

		public void BEEP(int int_0 = 500, int int_1 = 700)
		{
			UDPX.Beep(int_0, int_1);
		}

		public void RefreshUI()
		{
		}

		private const int INTERNET_CONNECTION_MODEM = 1;

		private const int INTERNET_CONNECTION_LAN = 2;
	}
}
