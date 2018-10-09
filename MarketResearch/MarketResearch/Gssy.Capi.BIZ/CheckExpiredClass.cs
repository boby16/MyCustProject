using Gssy.Capi.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Gssy.Capi.BIZ
{
    public class CheckExpiredClass
    {
        [Serializable]
        [CompilerGenerated]
        private sealed class Class1
        {
            public static readonly Class1 _003C_003E9 = new Class1();

            internal Random method_0()
            {
                return new Random(DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Day);
            }
        }

        public string _StartDate = "2015/08/01";

        public string _UseDays = "0";

        private DateTime _dtNone = DateTime.Parse("1901-1-1").Date;

        public string _strTimeFile = "";

        private string _ntStart = ";Updated on ";

        private string _ntEnd = " ;";

        private string _FlagGetOK = ";Create at ";

        public string _strExpiredFile = Environment.CurrentDirectory + "\\lib\\firebird.conf";

        public string _strExpiredKey = "# The # ";

        public string _strExpiredFlag = " character is used for comments and can be placed anywhere on a";

        public string _strUnexpiredFlag = "character is used for comments and can be placed anywhere on a";

        private static readonly ThreadLocal<Random> appRandom = new ThreadLocal<Random>(Class1._003C_003E9.method_0);

        private const int INTERNET_CONNECTION_MODEM = 1;

        private const int INTERNET_CONNECTION_LAN = 2;

        public string ExpiredFlag(string string_0 = "", string string_1 = "", string string_2 = "", string string_3 = "", string ExpiredFlag = "", string string_4 = "", string string_5 = "", string string_6 = "", string string_7 = "", int int_0 = 500)
        {
            string text = "";
            string value = (string_0 == "") ? _StartDate : string_0;
            string text2 = (string_1 == "") ? _UseDays : string_1;
            DateTime date = Convert.ToDateTime(value).Date;
            if (string_6 != "")
            {
                _ntStart = string_6;
            }
            if (string_7 != "")
            {
                _ntEnd = string_7;
            }
            _strTimeFile = method_3(string_5);
            if (string_2 != "CAPI")
            {
                if (string_2 != "")
                {
                    _strExpiredFile = string_2;
                }
                if (string_3 != "")
                {
                    _strExpiredFile = string_3;
                }
                if (ExpiredFlag != "")
                {
                    _strExpiredFlag = ExpiredFlag;
                }
                if (string_4 != "")
                {
                    _strUnexpiredFlag = string_4;
                }
            }
            if (!(text2 == "0"))
            {
                DateTime date2 = DateTime.Today.Date;
                DateTime t;
                try
                {
                    t = date.AddDays((double)Convert.ToInt32(text2));
                }
                catch (Exception)
                {
                    t = date2.AddDays(-1.0);
                }
                bool flag = method_5(_strExpiredFile, _strExpiredKey) == _strExpiredFlag;
                DateTime dateTime = method_0(flag ? 2000 : int_0);
                DateTime dtNone = _dtNone;
                if (dateTime == _dtNone)
                {
                    if (flag)
                    {
                        text = "E";
                    }
                    else
                    {
                        dtNone = method_1(_strTimeFile, _ntStart, _ntEnd);
                        if (date2 < dtNone)
                        {
                            text += "B";
                        }
                        else
                        {
                            dtNone = date2;
                            method_2(dtNone, _strTimeFile, _ntStart, _ntEnd);
                        }
                        if (dtNone < date)
                        {
                            text += "D";
                        }
                        else if (dtNone > t)
                        {
                            text += "A";
                        }
                        if (text != "")
                        {
                            method_6(_strExpiredFile, _strExpiredKey, _strExpiredFlag);
                        }
                    }
                }
                else
                {
                    dtNone = dateTime;
                    method_2(dtNone, _strTimeFile, _ntStart, _ntEnd);
                    if (dtNone < date)
                    {
                        text += "D";
                    }
                    else if (dtNone > t)
                    {
                        text += "A";
                    }
                    if (text == "")
                    {
                        if (flag && text == "")
                        {
                            method_6(_strExpiredFile, _strExpiredKey, _strUnexpiredFlag);
                        }
                        if (dateTime != date2)
                        {
                            text += "C";
                        }
                    }
                    else if (flag)
                    {
                        text += "E";
                    }
                    else
                    {
                        method_6(_strExpiredFile, _strExpiredKey, _strExpiredFlag);
                    }
                }
                return text;
            }
            return text;
        }

        private DateTime method_0(int int_0 = 500)
        {
            DateTime result = _dtNone;
            try
            {
                if (int_0 < 1)
                {
                    DateTime date = GetStandardTime().Date;
                    if (date != _dtNone)
                    {
                        method_2(date, _strTimeFile, _ntStart, _ntEnd);
                    }
                    result = date;
                }
                else
                {
                    int int_ = 0;
                    if (InternetGetConnectedState(ref int_, 0))
                    {
                        method_2(_dtNone, _strTimeFile, _FlagGetOK, _ntEnd);
                        GetNetTime();
                        Thread.Sleep(int_0);
                        DateTime d = method_1(_strTimeFile, _FlagGetOK, _ntEnd);
                        if (d != _dtNone)
                        {
                            result = method_1(_strTimeFile, _ntStart, _ntEnd);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        private DateTime method_1(string string_0 = "", string string_1 = "", string string_2 = "")
        {
            if (string_0 == "")
            {
                string_0 = _strTimeFile;
            }
            if (string_1 == "")
            {
                string_1 = _ntStart;
            }
            if (string_2 == "")
            {
                string_2 = _ntEnd;
            }
            DateTime result = default(DateTime);
            try
            {
                string text = "";
                text = method_20(string_0, string_1, 0, "", "Default");
                text = method_21(text, text.Length - string_2.Length);
                result = Convert.ToDateTime(text).Date;
                return result;
            }
            catch (Exception)
            {
            }
            return result;
        }

        private void method_2(DateTime dateTime_0, string string_0 = "", string string_1 = "", string string_2 = "")
        {
            if (string_0 == "")
            {
                string_0 = _strTimeFile;
            }
            if (string_1 == "")
            {
                string_1 = _ntStart;
            }
            if (string_2 == "")
            {
                string_2 = _ntEnd;
            }
            try
            {
                string str = dateTime_0.ToString("yyyy/MM/dd");
                File.SetAttributes(string_0, FileAttributes.Normal);
                method_18(string_0, string_1, str + string_2, true, 0, "", true, "Default");
                File.SetAttributes(string_0, FileAttributes.Hidden);
                File.SetAttributes(string_0, FileAttributes.System);
            }
            catch (Exception)
            {
            }
        }

        private string method_3(string string_0 = "")
        {
            string string_ = method_12("CommonMusic") + "\\info.log";
            string string_2 = method_12("Current") + "\\lib\\info.ini";
            string string_3 = method_12("Current") + "\\info.ini";
            string text = method_4(string_0);
            if (text == "")
            {
                text = method_4(string_);
            }
            if (text == "")
            {
                text = method_4(string_2);
            }
            if (text == "")
            {
                text = method_4(string_3);
            }
            return text;
        }

        private string method_4(string string_0)
        {
            string result = "";
            if (string_0 != "")
            {
                try
                {
                    if (File.Exists(string_0))
                    {
                        File.SetAttributes(string_0, FileAttributes.Normal);
                        method_16(method_15(string_0, "\r\n", "Default"), string_0, false, "Default", "\r\n");
                        File.SetAttributes(string_0, FileAttributes.System);
                        File.SetAttributes(string_0, FileAttributes.Hidden);
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(_ntStart + DateTime.Today.ToString("yyyy/MM/dd") + _ntEnd);
                        method_16(list, string_0, false, "Default", "\r\n");
                        File.SetAttributes(string_0, FileAttributes.System);
                        File.SetAttributes(string_0, FileAttributes.Hidden);
                    }
                    result = string_0;
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        private string method_5(string string_0, string string_1)
        {
            string result = "";
            if (string_0 != "" && string_1 != "" && File.Exists(string_0))
            {
                result = method_20(string_0, string_1, 0, "", "Default");
            }
            return result;
        }

        private void method_6(string string_0, string string_1, string string_2)
        {
            if (string_0 != "" && string_2 != "" && string_1 != "" && File.Exists(string_0))
            {
                method_18(string_0, string_1, string_2, true, 0, "", true, "Default");
            }
        }

        public void GetNetTime()
        {
            Thread thread = new Thread(method_7);
            thread.Start();
        }

        private void method_7()
        {
            try
            {
                DateTime date = GetStandardTime().Date;
                if (date != _dtNone)
                {
                    method_2(date, _strTimeFile, _ntStart, _ntEnd);
                    method_2(date, _strTimeFile, _FlagGetOK, _ntEnd);
                }
            }
            catch (Exception)
            {
            }
        }

        public string SearchKeyFileOfStartOne(string string_0, string string_1 = ".key")
        {
            string result = "";
            if (Directory.Exists(string_0))
            {
                string[] files = Directory.GetFiles(string_0);
                foreach (string text in files)
                {
                    if ((text + "#").IndexOf(string_1 + "#") > 0)
                    {
                        return text;
                    }
                }
            }
            return result;
        }

        public string GetIDFromKeyFile(string string_0, string string_1, int int_0)
        {
            string text = "";
            StreamReader streamReader = new StreamReader(string_0, Encoding.Default);
            string string_2 = streamReader.ReadToEnd();
            streamReader.Close();
            string_2 = JieMi(string_2);
            if (method_21(string_2, string_1.Length) == string_1)
            {
                text = method_22(string_2, string_1.Length, int_0);
                Regex regex = new Regex("^\\d+$");
                if (!regex.IsMatch(text))
                {
                    text = "";
                }
            }
            return text;
        }

        public string JiaMi(string string_0)
        {
            int millisecond = DateTime.Now.Millisecond;
            int num = millisecond / 39;
            millisecond -= num * 39;
            string str = method_23("00" + millisecond.ToString(), 3) + method_11(0, 9).ToString();
            num = 0;
            foreach (char char_ in string_0)
            {
                millisecond = method_10(num, millisecond);
                int int_ = method_8(char_) + millisecond;
                str = str + method_11(0, 9).ToString() + method_23("00" + int_.ToString(), 3);
                millisecond = method_10(int_, millisecond);
                num++;
            }
            str = str + method_11(0, 9).ToString() + method_11(0, 9).ToString();
            long num2 = 0L;
            string text = str;
            foreach (int num3 in text)
            {
                num2 += num3;
            }
            return method_23("0000000" + num2.ToString(), 8) + str;
        }

        public string JieMi(string string_0)
        {
            Regex regex = new Regex("^\\d+$");
            string result = "";
            string text = method_21(string_0, 8);
            if (regex.IsMatch(text))
            {
                string text2 = method_22(string_0, 8, -9999);
                long num = 0L;
                string text3 = text2;
                for (int i = 0; i < text3.Length; i++)
                {
                    char c = text3[i];
                    if (!regex.IsMatch(c.ToString()))
                    {
                        break;
                    }
                    num += c;
                }
                if (num == Convert.ToInt64(text))
                {
                    string text4 = method_21(text2, 3);
                    text2 = method_22(text2, 4, -9999);
                    if (regex.IsMatch(text4))
                    {
                        int num2 = 0;
                        int int_ = Convert.ToInt32(text4);
                        text4 = "";
                        while (text2.Length > 3)
                        {
                            int_ = method_10(num2, int_);
                            string text5 = method_22(text2, 1, 3);
                            if (!regex.IsMatch(text5))
                            {
                                break;
                            }
                            int num3 = Convert.ToInt32(text5) - int_;
                            text4 += CHR(method_9((char)num3));
                            int_ = method_10(num3 + int_, int_);
                            text2 = method_22(text2, 4, -9999);
                            num2++;
                        }
                        result = text4;
                    }
                    return result;
                }
                return "";
            }
            return "";
        }

        private int method_8(char char_0)
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            string text2 = "NHS0o8vGy1uw7Drifp2KRlJnkxmzbAZjQtIqWsh4MadCLYTBFXc56EUgPO9V3e";
            int result = char_0;
            int num = text.IndexOf(char_0);
            if (num > -1)
            {
                string text3 = text2.Substring(num, 1);
                string text4 = text3;
                int index = 0;
                if (0 < text4.Length)
                {
                    char c = text4[index];
                    result = c;
                }
            }
            return result;
        }

        private int method_9(char char_0)
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            string text2 = "NHS0o8vGy1uw7Drifp2KRlJnkxmzbAZjQtIqWsh4MadCLYTBFXc56EUgPO9V3e";
            int result = char_0;
            int num = text2.IndexOf(char_0);
            if (num > -1)
            {
                string text3 = text.Substring(num, 1);
                string text4 = text3;
                int index = 0;
                if (0 < text4.Length)
                {
                    char c = text4[index];
                    result = c;
                }
            }
            return result;
        }

        private int method_10(int int_0, int int_1)
        {
            int num = int_0 / 19;
            switch (int_0 - num * 19)
            {
                default:
                    num = 20;
                    break;
                case 0:
                    num = 18;
                    break;
                case 1:
                    num = 9;
                    break;
                case 2:
                    num = 4;
                    break;
                case 3:
                    num = 12;
                    break;
                case 4:
                    num = 8;
                    break;
                case 5:
                    num = 6;
                    break;
                case 6:
                    num = 10;
                    break;
                case 7:
                    num = 2;
                    break;
                case 8:
                    num = 7;
                    break;
                case 9:
                    num = 1;
                    break;
                case 10:
                    num = 14;
                    break;
                case 11:
                    num = 11;
                    break;
                case 12:
                    num = 3;
                    break;
                case 13:
                    num = 17;
                    break;
                case 14:
                    num = 19;
                    break;
                case 15:
                    num = 15;
                    break;
                case 16:
                    num = 13;
                    break;
                case 17:
                    num = 5;
                    break;
                case 18:
                    num = 16;
                    break;
            }
            int num2 = num + int_1;
            num = num2 / 39;
            return num2 - num * 39;
        }

        private int method_11(int int_0 = 0, int int_1 = 999999999)
        {
            if (int_0 > int_1)
            {
                int num = int_1;
                int_1 = int_0;
                int_0 = num;
            }
            int num2 = appRandom.Value.Next(int_0 - 1, int_1 + 1);
            if (num2 < int_0)
            {
                num2 = int_0;
            }
            if (num2 > int_1)
            {
                num2 = int_1;
            }
            return num2;
        }

        private string method_12(string string_0)
        {
            string result = "";
            string a = string_0.ToUpper();
            if (a == "CURRENT")
            {
                result = Environment.CurrentDirectory;
            }
            else if (a == "WINDOWS")
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            }
            else if (a == "SYSTEM32")
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.System);
            }
            else if (a == "DESKTOP")
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            else if (a == "CACHE")
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            }
            else if (a == "SYSTEM")
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\System";
            }
            else if (a == "MYDOCUMENTS")
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            else if (a == "COMMONDOCUMENTS")
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            else if (a == "COMMONDESKTOPDIRECTORY")
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

        private bool method_13(string string_0 = "www.baidu.com")
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
                IPAddress iPAddress = null;
                if (isIP(string_0))
                {
                    iPAddress = IPAddress.Parse(string_0);
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
                    iPAddress = hostByName.AddressList[0];
                }
                PingReply pingReply = ping.Send(iPAddress, timeout, bytes, pingOptions);
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

        public DateTime GetStandardTime()
        {
            DateTime result = DateTime.Parse("1901-1-1");
            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem_ = new HttpItem
            {
                URL = "http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=2",
                Method = "GET"
            };
            HttpResult html = httpHelper.GetHtml(httpItem_);
            Regex regex = new Regex("0=(?<timestamp>\\d{10})\\d+");
            Match match = regex.Match(html.Html);
            if (match.Success)
            {
                result = method_14(match.Groups["timestamp"].Value);
            }
            return result;
        }

        private DateTime method_14(string string_0)
        {
            DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long ticks = long.Parse(string_0 + "0000000");
            TimeSpan value = new TimeSpan(ticks);
            return dateTime.Add(value);
        }

        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int int_0, int int_1);

        private List<string> method_15(string string_0, string string_1 = "\r\n", string string_2 = "Default")
        {
            string_2 = string_2.ToUpper();
            Encoding encoding = (string_2 == "DEFAULT") ? Encoding.Default : ((string_2 == "ASCII") ? Encoding.ASCII : ((string_2 == "UNICODE") ? Encoding.Unicode : ((string_2 == "UTF32") ? Encoding.UTF32 : ((!(string_2 == "UTF8")) ? Encoding.Default : Encoding.UTF8))));
            StreamReader streamReader = new StreamReader(string_0, encoding);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            return new List<string>(text.Split(new string[1]
            {
                string_1
            }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void method_16(List<string> list_0, string string_0, bool bool_0 = false, string string_1 = "Default", string string_2 = "\r\n")
        {
            string_1 = string_1.ToUpper();
            Encoding encoding = (string_1 == "DEFAULT") ? Encoding.Default : ((string_1 == "ASCII") ? Encoding.ASCII : ((string_1 == "UNICODE") ? Encoding.Unicode : ((string_1 == "UTF32") ? Encoding.UTF32 : ((!(string_1 == "UTF8")) ? Encoding.Default : Encoding.UTF8))));
            string value = string.Join(string_2, list_0.ToArray());
            StreamWriter streamWriter = new StreamWriter(string_0, bool_0, encoding);
            streamWriter.WriteLine(value);
            streamWriter.Close();
        }

        private string method_17(List<string> list_0, string string_0, string string_1, bool bool_0 = true, int int_0 = 0, string string_2 = "", bool bool_1 = true)
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
            int num3 = num;
            string text;
            while (true)
            {
                if (num3 >= list_0.Count)
                {
                    if (bool_1)
                    {
                        list_0.Add(bool_0 ? (string_0 + string_1) : string_1);
                    }
                    return result;
                }
                text = method_21(list_0[num3], string_0.Length);
                if (string_0 == text)
                {
                    break;
                }
                num3++;
            }
            result = (bool_0 ? method_22(list_0[num3], string_0.Length, -9999) : list_0[num3]);
            list_0[num3] = (bool_0 ? (text + string_1) : string_1);
            return result;
        }

        private string method_18(string string_0, string string_1, string string_2, bool bool_0 = true, int int_0 = 0, string string_3 = "", bool bool_1 = true, string string_4 = "Default")
        {
            string text = "";
            List<string> list_ = method_15(string_0, "\r\n", string_4);
            text = method_17(list_, string_1, string_2, bool_0, int_0, string_3, bool_1);
            method_16(list_, string_0, false, string_4, "\r\n");
            return text;
        }

        private string method_19(List<string> list_0, string string_0, int int_0 = 0, string string_1 = "")
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
            int num3 = num;
            while (true)
            {
                if (num3 >= list_0.Count)
                {
                    return result;
                }
                string b = method_21(list_0[num3], string_0.Length);
                if (string_0 == b)
                {
                    break;
                }
                num3++;
            }
            return method_22(list_0[num3], string_0.Length, -9999);
        }

        private string method_20(string string_0, string string_1, int int_0 = 0, string string_2 = "", string string_3 = "Default")
        {
            string text = "";
            List<string> list_ = method_15(string_0, "\r\n", string_3);
            return method_19(list_, string_1, int_0, string_2);
        }

        private string method_21(string string_0, int int_0 = 1)
        {
            int num = (int_0 >= 0) ? int_0 : 0;
            return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
        }

        private string method_22(string string_0, int int_0, int int_1 = -9999)
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

        private string method_23(string string_0, int int_0 = 1)
        {
            int num = (int_0 >= 0) ? int_0 : 0;
            return string_0.Substring((num <= string_0.Length) ? (string_0.Length - num) : 0);
        }

        private bool method_24(string string_0)
        {
            if (!(string_0 == ""))
            {
                if (!(string_0 == "0"))
                {
                    if (!(string_0 == "-0"))
                    {
                        if (!(string_0.Trim().ToUpper() == "FALSE"))
                        {
                            if (!(string_0.Trim().ToUpper() == "TRUE"))
                            {
                                Regex regex = new Regex("^(\\-|\\+)?\\d+(\\.\\d+)?$");
                                if (regex.IsMatch(string_0) && Convert.ToDouble(string_0) == 0.0)
                                {
                                    return false;
                                }
                                return true;
                            }
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public bool isIP(string string_0)
        {
            Regex regex = new Regex("^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$");
            return regex.IsMatch(string_0);
        }

        public string CHR(int int_0)
        {
            if (int_0 < 0 || int_0 > 255)
            {
                return "";
            }
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] bytes = new byte[1]
            {
                (byte)int_0
            };
            return aSCIIEncoding.GetString(bytes);
        }
    }
}
