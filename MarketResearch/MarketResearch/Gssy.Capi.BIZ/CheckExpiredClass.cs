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

        public string _StartDate = GClass0.smethod_0("8Ĺȹ\u0332ЩԵؼܬ࠲र");

        public string _UseDays = GClass0.smethod_0("1");

        private DateTime _dtNone = DateTime.Parse(GClass0.smethod_0("9ľȶ\u0334ЩԲد\u0730")).Date;

        public string _strTimeFile = GClass0.smethod_0("");

        private string _ntStart = GClass0.smethod_0("7Şɺ\u036dѩճ٣ݡ\u0824६੬ଡ");

        private string _ntEnd = GClass0.smethod_0("\"ĺ");

        private string _FlagGetOK = GClass0.smethod_0("0ŉɻ\u036dѦղ٠ܤ\u0862ॶਡ");

        public string _strExpiredFile = Environment.CurrentDirectory + GClass0.smethod_0("NŽɹ\u036dђի٥ݹ\u086f५\u0a61୵\u0c62ഫ\u0e67ཬ\u106cᅧ");

        public string _strExpiredKey = GClass0.smethod_0("+ħɒ\u036dѡԣءܡ");

        public string _strExpiredFlag = GClass0.smethod_0("\u001fŝɕ\u035dщ՛\u065a\u074cࡒ\u0944ਕଢ଼\u0c40ഒไགྷ၊ᅊልፊᑄᕘᘉᝋᡈ᥋ᩈ\u1b41ᱍ\u1d56Ṓἀ⁾ⅰ≹⌼⑸╻♷✸⡵⥳⨵⭤Ɀ\u2d73\u2e72⽵に\u312e㉬㍢㑲㕽㙡㝭㡵㥣㨥㭫㱭㴢㹠");

        public string _strUnexpiredFlag = GClass0.smethod_0("]ŕɝ\u0349ћ՚\u064cݒࡄक\u0a5d\u0b40ఒ\u0d44ใཊ၊ᄍቊፄᑘᔉᙋᝈᡋ᥈ᩁ\u1b4d᱖\u1d52Ḁ\u1f7e⁰ⅹ∼⍸⑻╷☸❵⡳⤵⩤⭿ⱳ\u2d72\u2e75⽫\u302eㅬ㉢㍲㑽㕡㙭㝵㡣㤥㩫㭭㰢㵠");

        private static readonly ThreadLocal<Random> appRandom = new ThreadLocal<Random>(Class1._003C_003E9.method_0);

        private const int INTERNET_CONNECTION_MODEM = 1;

        private const int INTERNET_CONNECTION_LAN = 2;

        public string ExpiredFlag(string string_0 = "", string string_1 = "", string string_2 = "", string string_3 = "", string ExpiredFlag = "", string string_4 = "", string string_5 = "", string string_6 = "", string string_7 = "", int int_0 = 500)
        {
            string text = GClass0.smethod_0("");
            string value = (string_0 == GClass0.smethod_0("")) ? _StartDate : string_0;
            string text2 = (string_1 == GClass0.smethod_0("")) ? _UseDays : string_1;
            DateTime date = Convert.ToDateTime(value).Date;
            if (string_6 != GClass0.smethod_0(""))
            {
                _ntStart = string_6;
            }
            if (string_7 != GClass0.smethod_0(""))
            {
                _ntEnd = string_7;
            }
            _strTimeFile = method_3(string_5);
            if (string_2 != GClass0.smethod_0("Głɒ\u0348"))
            {
                if (string_2 != GClass0.smethod_0(""))
                {
                    _strExpiredFile = string_2;
                }
                if (string_3 != GClass0.smethod_0(""))
                {
                    _strExpiredFile = string_3;
                }
                if (ExpiredFlag != GClass0.smethod_0(""))
                {
                    _strExpiredFlag = ExpiredFlag;
                }
                if (string_4 != GClass0.smethod_0(""))
                {
                    _strUnexpiredFlag = string_4;
                }
            }
            if (!(text2 == GClass0.smethod_0("1")))
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
                        text = GClass0.smethod_0("D");
                    }
                    else
                    {
                        dtNone = method_1(_strTimeFile, _ntStart, _ntEnd);
                        if (date2 < dtNone)
                        {
                            text += GClass0.smethod_0("C");
                        }
                        else
                        {
                            dtNone = date2;
                            method_2(dtNone, _strTimeFile, _ntStart, _ntEnd);
                        }
                        if (dtNone < date)
                        {
                            text += GClass0.smethod_0("E");
                        }
                        else if (dtNone > t)
                        {
                            text += GClass0.smethod_0("@");
                        }
                        if (text != GClass0.smethod_0(""))
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
                        text += GClass0.smethod_0("E");
                    }
                    else if (dtNone > t)
                    {
                        text += GClass0.smethod_0("@");
                    }
                    if (text == GClass0.smethod_0(""))
                    {
                        if (flag && text == GClass0.smethod_0(""))
                        {
                            method_6(_strExpiredFile, _strExpiredKey, _strUnexpiredFlag);
                        }
                        if (dateTime != date2)
                        {
                            text += GClass0.smethod_0("B");
                        }
                    }
                    else if (flag)
                    {
                        text += GClass0.smethod_0("D");
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
            if (string_0 == GClass0.smethod_0(""))
            {
                string_0 = _strTimeFile;
            }
            if (string_1 == GClass0.smethod_0(""))
            {
                string_1 = _ntStart;
            }
            if (string_2 == GClass0.smethod_0(""))
            {
                string_2 = _ntEnd;
            }
            DateTime result = default(DateTime);
            try
            {
                string text = GClass0.smethod_0("");
                text = method_20(string_0, string_1, 0, GClass0.smethod_0(""), GClass0.smethod_0("Cţɣ\u0365Ѷծٵ"));
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
            if (string_0 == GClass0.smethod_0(""))
            {
                string_0 = _strTimeFile;
            }
            if (string_1 == GClass0.smethod_0(""))
            {
                string_1 = _ntStart;
            }
            if (string_2 == GClass0.smethod_0(""))
            {
                string_2 = _ntEnd;
            }
            try
            {
                string str = dateTime_0.ToString(GClass0.smethod_0("sŰɱ;ЩՈىܬ\u0866॥"));
                File.SetAttributes(string_0, FileAttributes.Normal);
                method_18(string_0, string_1, str + string_2, true, 0, GClass0.smethod_0(""), true, GClass0.smethod_0("Cţɣ\u0365Ѷծٵ"));
                File.SetAttributes(string_0, FileAttributes.Hidden);
                File.SetAttributes(string_0, FileAttributes.System);
            }
            catch (Exception)
            {
            }
        }

        private string method_3(string string_0 = "")
        {
            string string_ = method_12(GClass0.smethod_0("Hťɤ\u0365Ѩըوݱ\u0870५\u0a62")) + GClass0.smethod_0("Ušɩ\u0360ѪԪٯݭ\u0866");
            string string_2 = method_12(GClass0.smethod_0("DųɷͶѦլٵ")) + GClass0.smethod_0("QŠɢ\u0368ѕա٩ݠ\u086aप੪୬౨");
            string string_3 = method_12(GClass0.smethod_0("DųɷͶѦլٵ")) + GClass0.smethod_0("Ušɩ\u0360ѪԪ٪ݬ\u0868");
            string text = method_4(string_0);
            if (text == GClass0.smethod_0(""))
            {
                text = method_4(string_);
            }
            if (text == GClass0.smethod_0(""))
            {
                text = method_4(string_2);
            }
            if (text == GClass0.smethod_0(""))
            {
                text = method_4(string_3);
            }
            return text;
        }

        private string method_4(string string_0)
        {
            string result = GClass0.smethod_0("");
            if (string_0 != GClass0.smethod_0(""))
            {
                try
                {
                    if (File.Exists(string_0))
                    {
                        File.SetAttributes(string_0, FileAttributes.Normal);
                        method_16(method_15(string_0, GClass0.smethod_0("\u000fċ"), GClass0.smethod_0("Cţɣ\u0365Ѷծٵ")), string_0, false, GClass0.smethod_0("Cţɣ\u0365Ѷծٵ"), GClass0.smethod_0("\u000fċ"));
                        File.SetAttributes(string_0, FileAttributes.System);
                        File.SetAttributes(string_0, FileAttributes.Hidden);
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(_ntStart + DateTime.Today.ToString(GClass0.smethod_0("sŰɱ;ЩՈىܬ\u0866॥")) + _ntEnd);
                        method_16(list, string_0, false, GClass0.smethod_0("Cţɣ\u0365Ѷծٵ"), GClass0.smethod_0("\u000fċ"));
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
            string result = GClass0.smethod_0("");
            if (string_0 != GClass0.smethod_0("") && string_1 != GClass0.smethod_0("") && File.Exists(string_0))
            {
                result = method_20(string_0, string_1, 0, GClass0.smethod_0(""), GClass0.smethod_0("Cţɣ\u0365Ѷծٵ"));
            }
            return result;
        }

        private void method_6(string string_0, string string_1, string string_2)
        {
            if (string_0 != GClass0.smethod_0("") && string_2 != GClass0.smethod_0("") && string_1 != GClass0.smethod_0("") && File.Exists(string_0))
            {
                method_18(string_0, string_1, string_2, true, 0, GClass0.smethod_0(""), true, GClass0.smethod_0("Cţɣ\u0365Ѷծٵ"));
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
            string result = GClass0.smethod_0("");
            if (Directory.Exists(string_0))
            {
                string[] files = Directory.GetFiles(string_0);
                foreach (string text in files)
                {
                    if ((text + GClass0.smethod_0("\"")).IndexOf(string_1 + GClass0.smethod_0("\"")) > 0)
                    {
                        return text;
                    }
                }
            }
            return result;
        }

        public string GetIDFromKeyFile(string string_0, string string_1, int int_0)
        {
            string text = GClass0.smethod_0("");
            StreamReader streamReader = new StreamReader(string_0, Encoding.Default);
            string string_2 = streamReader.ReadToEnd();
            streamReader.Close();
            string_2 = JieMi(string_2);
            if (method_21(string_2, string_1.Length) == string_1)
            {
                text = method_22(string_2, string_1.Length, int_0);
                Regex regex = new Regex(GClass0.smethod_0("[Řɧ\u0329Х"));
                if (!regex.IsMatch(text))
                {
                    text = GClass0.smethod_0("");
                }
            }
            return text;
        }

        public string JiaMi(string string_0)
        {
            int millisecond = DateTime.Now.Millisecond;
            int num = millisecond / 39;
            millisecond -= num * 39;
            string str = method_23(GClass0.smethod_0("2ı") + millisecond.ToString(), 3) + method_11(0, 9).ToString();
            num = 0;
            foreach (char char_ in string_0)
            {
                millisecond = method_10(num, millisecond);
                int int_ = method_8(char_) + millisecond;
                str = str + method_11(0, 9).ToString() + method_23(GClass0.smethod_0("2ı") + int_.ToString(), 3);
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
            return method_23(GClass0.smethod_0("7Ķȵ\u0334гԲر") + num2.ToString(), 8) + str;
        }

        public string JieMi(string string_0)
        {
            Regex regex = new Regex(GClass0.smethod_0("[Řɧ\u0329Х"));
            string result = GClass0.smethod_0("");
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
                        text4 = GClass0.smethod_0("");
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
                return GClass0.smethod_0("");
            }
            return GClass0.smethod_0("");
        }

        private int method_8(char char_0)
        {
            string text = GClass0.smethod_0("\u007fſɿͿѿտٿݿ\u087fॿ\u0a7f\u0b7f౿ൿ\u0e7f\u0f7fၿᅿቿ\u137fᑿᕿᙿ\u177f\u187f\u197fᨔᬒᰐᴒḔἪ\u2028K∤⌢⑻╻♻❳⡳⥳⩳⭻ⱻ\u2d7b\u2e7b⽣っㅣ㉣㍻㑻㕻㙻㝳㡳㥳㩳㭻㱻㵻");
            string text2 = GClass0.smethod_0("pŵɯ\u030bѕԁ\u064eݰࡏऄ\u0a41\u0b44అ൵โཆ၈ᅝሞ፠ᑸᕅᙢᝉᡍᥝᩉ᭙᱀\u1d60Ṻή⁏Ⅹ≕⍪\u244d╪♰✣⡛⥴⩰⭐ⱞⵈ\u2e44⽍えㅕ㉯㌾㐼㕌㙝㝠㡖㥊㨽㭕㰱㵤");
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
            string text = GClass0.smethod_0("\u007fſɿͿѿտٿݿ\u087fॿ\u0a7f\u0b7f౿ൿ\u0e7f\u0f7fၿᅿቿ\u137fᑿᕿᙿ\u177f\u187f\u197fᨔᬒᰐᴒḔἪ\u2028K∤⌢⑻╻♻❳⡳⥳⩳⭻ⱻ\u2d7b\u2e7b⽣っㅣ㉣㍻㑻㕻㙻㝳㡳㥳㩳㭻㱻㵻");
            string text2 = GClass0.smethod_0("pŵɯ\u030bѕԁ\u064eݰࡏऄ\u0a41\u0b44అ൵โཆ၈ᅝሞ፠ᑸᕅᙢᝉᡍᥝᩉ᭙᱀\u1d60Ṻή⁏Ⅹ≕⍪\u244d╪♰✣⡛⥴⩰⭐ⱞⵈ\u2e44⽍えㅕ㉯㌾㐼㕌㙝㝠㡖㥊㨽㭕㰱㵤");
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
            string result = GClass0.smethod_0("");
            string a = string_0.ToUpper();
            if (a == GClass0.smethod_0("Dœɗ\u0356цՌ\u0655"))
            {
                result = Environment.CurrentDirectory;
            }
            else if (a == GClass0.smethod_0("Pŏɋ\u0340ьՕ\u0652"))
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            }
            else if (a == GClass0.smethod_0("[Şɕ\u0351сՎر\u0733"))
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.System);
            }
            else if (a == GClass0.smethod_0("CŃɖ\u034fїՍ\u0651"))
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            else if (a == GClass0.smethod_0("FŅɀ\u034aф"))
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            }
            else if (a == GClass0.smethod_0("UŜɗ\u0357чՌ"))
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + GClass0.smethod_0("[ŕɼͷѷէ٬");
            }
            else if (a == GClass0.smethod_0("Fœɍ\u0347фՓو\u0741ࡍ\u0956\u0a52"))
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            else if (a == GClass0.smethod_0("LŁɀ\u0341фՄ\u064d\u0747ࡄ\u0953\u0a48\u0b41\u0c4d\u0d56๒"))
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            else if (a == GClass0.smethod_0("UŚə\u035eѝ՟\u0654\u074a\u085d\u0946\u0a58\u0b44ౚ\u0d4dแཕ၃ᅆቐፌᑐᕘ"))
            {
                result = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
            }
            else if (a == GClass0.smethod_0("HŅɄ\u0345шՈوݑࡐ\u094b\u0a42"))
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
            DateTime result = DateTime.Parse(GClass0.smethod_0("9ľȶ\u0334ЩԲد\u0730"));
            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem_ = new HttpItem
            {
                URL = GClass0.smethod_0("GŚə\u035cБԅ؆ݟࡐ\u0951\u0a0b\u0b4c\u0c48\u0d4dฏཇၰᅨሳ፴ᑰᔵᙺ\u177f\u187e\u193b\u1a77\u1b7d\u1c7d\u1d3dṶὤ⁼℡≹⍥⑦╯☼❩⠩⥶⩷⬻Ɫⴿ⸳"),
                Method = GClass0.smethod_0("DŇɕ")
            };
            HttpResult html = httpHelper.GetHtml(httpItem_);
            Regex regex = new Regex(GClass0.smethod_0(")ĥȿ\u0329Щ\u0560ٺݿ\u0874\u0963\u0a7b୯ౠർ\u0e35བ\u106dᅳሶጶᑸᔭᙟᝦᠪ"));
            Match match = regex.Match(html.Html);
            if (match.Success)
            {
                result = method_14(match.Groups[GClass0.smethod_0("}šɪ\u0363Ѷհ٢ݯ\u0871")].Value);
            }
            return result;
        }

        private DateTime method_14(string string_0)
        {
            DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long ticks = long.Parse(string_0 + GClass0.smethod_0("7Ķȵ\u0334гԲر"));
            TimeSpan value = new TimeSpan(ticks);
            return dateTime.Add(value);
        }

        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int int_0, int int_1);

        private List<string> method_15(string string_0, string string_1 = "\r\n", string string_2 = "Default")
        {
            string_2 = string_2.ToUpper();
            Encoding encoding = (string_2 == GClass0.smethod_0("CŃɃ\u0345іՎ\u0655")) ? Encoding.Default : ((string_2 == GClass0.smethod_0("Dŗɀ\u034bш")) ? Encoding.ASCII : ((string_2 == GClass0.smethod_0("RňɌ\u0347ьՆل")) ? Encoding.Unicode : ((string_2 == GClass0.smethod_0("PŐɅ\u0331г")) ? Encoding.UTF32 : ((!(string_2 == GClass0.smethod_0("QŗɄ\u0339"))) ? Encoding.Default : Encoding.UTF8))));
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
            Encoding encoding = (string_1 == GClass0.smethod_0("CŃɃ\u0345іՎ\u0655")) ? Encoding.Default : ((string_1 == GClass0.smethod_0("Dŗɀ\u034bш")) ? Encoding.ASCII : ((string_1 == GClass0.smethod_0("RňɌ\u0347ьՆل")) ? Encoding.Unicode : ((string_1 == GClass0.smethod_0("PŐɅ\u0331г")) ? Encoding.UTF32 : ((!(string_1 == GClass0.smethod_0("QŗɄ\u0339"))) ? Encoding.Default : Encoding.UTF8))));
            string value = string.Join(string_2, list_0.ToArray());
            StreamWriter streamWriter = new StreamWriter(string_0, bool_0, encoding);
            streamWriter.WriteLine(value);
            streamWriter.Close();
        }

        private string method_17(List<string> list_0, string string_0, string string_1, bool bool_0 = true, int int_0 = 0, string string_2 = "", bool bool_1 = true)
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
            string text = GClass0.smethod_0("");
            List<string> list_ = method_15(string_0, GClass0.smethod_0("\u000fċ"), string_4);
            text = method_17(list_, string_1, string_2, bool_0, int_0, string_3, bool_1);
            method_16(list_, string_0, false, string_4, GClass0.smethod_0("\u000fċ"));
            return text;
        }

        private string method_19(List<string> list_0, string string_0, int int_0 = 0, string string_1 = "")
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
            string text = GClass0.smethod_0("");
            List<string> list_ = method_15(string_0, GClass0.smethod_0("\u000fċ"), string_3);
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
            if (!(string_0 == GClass0.smethod_0("")))
            {
                if (!(string_0 == GClass0.smethod_0("1")))
                {
                    if (!(string_0 == GClass0.smethod_0("/ı")))
                    {
                        if (!(string_0.Trim().ToUpper() == GClass0.smethod_0("CŅɏ\u0351ф")))
                        {
                            if (!(string_0.Trim().ToUpper() == GClass0.smethod_0("Pőɗ\u0344")))
                            {
                                Regex regex = new Regex(GClass0.smethod_0("Kļɏ\u033fѭՌؤܧ࠲ॐ੯ଡడ\u0d54ษཚၡᄯሪጽᐥ"));
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
            Regex regex = new Regex(GClass0.smethod_0("+Ŝȗ\u0309р՜\u065dܓࠑढ़ਏ\u0b0eక\u0d5a\u0e3cབ၈ᅐሾጆᐝᕒᙪᜅᡭᥱ\u1a6eᬇᱰᵶṿἲ\u202eⅥ≿⍠\u242c┬♾✪⠩⤰⩹⬑ⱹⵥ\u2e73⼛〡ㄸ㉱㍷㐚㕰㘒㜋㡠㤕㨕㬒㱝㵃㸆㼚䀇䅉䉏䌃䑕䕔䙓䜜䡶䤜䨆䬞䱴䵌乛伔倐兿刓匏吔啽嘶地堵奸婠嬫尵崪幪彪怤慰扷据搣敋昿朣根楑橯歶氻洽湜漶瀨焱牞猫琥"));
            return regex.IsMatch(string_0);
        }

        public string CHR(int int_0)
        {
            if (int_0 < 0 || int_0 > 255)
            {
                return GClass0.smethod_0("");
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
