using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Gssy.Capi.BIZ
{
    public class LogicExplain
    {
        private string sySeqSplit = GClass0.smethod_0("*Ħȕ\u0311Бԕ");

        private const char syDataSplit = ',';

        private const string syOR = "|";

        private const string syAND = "&";

        private const string syXOR = "!";

        private const string syRL = "|&`";

        private const string syMATH = "*+-/";

        private const string syCMPR = "<>=";

        private const string syFUNC = "$";

        private const string syPARA = ",";

        private const char syParaToArray = '\n';

        private const string syNowLoop = "^";

        private const string syLOOP = "#";

        private const string syANY = "%";

        private const string syTO = "~";

        private const string syALL = "@";

        private const string sySTART1 = "[";

        private const string syEND1 = "]";

        private const string sySTART2 = "(";

        private const string syEND2 = ")";

        private const string syDATE = "/";

        private const string syTIME = ":";

        private const string syGOTO1 = ":";

        private const string syGOTO2 = ";";

        private const string sySTART3 = "{";

        private const string syEND3 = "}";

        private const string syStartTextFml = "&{";

        private const string syEndTextFml = "}";

        private const string syTextFml = "{}";

        private const string syStartText = "[\"";

        private const string syEndText = "\"]";

        private const string syStartShowText = "$[";

        private const string syEndShowText = "]";

        private const string syStartShowCode = "#[";

        private const string syEndShowCode = "]";

        private const string syTextSplit = "、";

        private const string syDynDetail = "#";

        private const string syStartGetOption = "#{";

        private const string syEndGetOption = "}";

        private const string syStartCodeFrame = "${";

        private const string syEndCodeFrame = "}";

        public Dictionary<string, string> _dictData = new Dictionary<string, string>();

        private string[] _aryQnFunc = new string[5]
        {
            GClass0.smethod_0("#ŉɑ\u034cцՐة"),
            GClass0.smethod_0("!ŋɗ\u034aЩ"),
            GClass0.smethod_0("-ŀɆ\u0350рՂيݎࡄ"),
            GClass0.smethod_0("#ňɊ\u0342ъՎل"),
            GClass0.smethod_0("#ňɊ\u0350ёՇق")
        };

        private Dictionary<string, string> _dictQn = new Dictionary<string, string>();

        private classLoopA _LoopQn = new classLoopA();

        private int _nTotalLoopA = 1;

        private int _nTotalLoopB = 1;

        private int _nCurrentLoopA = 0;

        private int _nCurrentLoopB = 0;

        private int _nCountResultIsTrue = 0;

        private string _ShowQuestion = "";

        private List<string> _listShowCode = new List<string>();

        private string _strTextSplit = GClass0.smethod_0("\u3000");

        private string method_0(string string_0)
        {
            string text = DeleteOuterSymbol(string_0);
            method_101(string_0, 0);
            string text2 = "";
            List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), false);
            foreach (string item in list)
            {
                text2 = text2 + GClass0.smethod_0("-") + method_17(item);
            }
            if (LEFT(text2, 1) == ','.ToString())
            {
                text2 = MID(text2, 1, -9999);
            }
            return text2;
        }

        private string method_1(string string_0, bool bool_0 = false)
        {
            string text = "";
            string text2 = string_0.Trim();
            if (text2.Length > 0)
            {
                if (bool_0 && !method_89(text2))
                {
                    text2 = "#[" + text2 + "]";
                }
                int num = 0;
                int num2 = 0;
                do
                {
                    string a = MID(text2, num2, 2);
                    string text3 = "";
                    if (!(a == GClass0.smethod_0("$ź")))
                    {
                        if (a == GClass0.smethod_0("Yģ"))
                        {
                            num = text2.IndexOf(GClass0.smethod_0(" Ŝ"), num2 + 1);
                            if (num < 0)
                            {
                                num = text2.Length;
                            }
                            text3 = method_132(text2, num2 + 2, num - 1);
                            num2 = num + 1;
                        }
                        else if (a == "#[")
                        {
                            num = text2.IndexOf("]", num2 + 1);
                            if (num < 0)
                            {
                                num = text2.Length;
                            }
                            text3 = method_132(text2, num2 + 2, num - 1);
                            text3 = method_3(text3);
                            num2 = num;
                        }
                        else if (a == "$[")
                        {
                            num = text2.IndexOf("]", num2 + 1);
                            if (num < 0)
                            {
                                num = text2.Length;
                            }
                            text3 = method_132(text2, num2 + 2, num - 1);
                            text3 = method_4(text3);
                            num2 = num;
                        }
                        else
                        {
                            num = method_91(text2, num2);
                            if (num > num2)
                            {
                                text3 = method_132(text2, num2, num);
                                text3 = method_27(text3);
                                num2 = num;
                            }
                            else
                            {
                                text3 = MID(text2, num2, 1);
                            }
                        }
                    }
                    else
                    {
                        num = RightBrackets(text2, num2, GClass0.smethod_0("$ź"), GClass0.smethod_0("|"));
                        text3 = method_132(text2, num2 + 2, num - 1);
                        text3 = method_2(text3);
                        num2 = num;
                    }
                    text += text3;
                    num2++;
                }
                while (num2 < text2.Length);
            }
            OutputResult(GClass0.smethod_0("=Ģȣ\u0320СԦاܤ\u0825पਫନ\u0c29മฯ༬"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            OutputResult(GClass0.smethod_0("辖僾构搮\ufb1b") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            return text;
        }

        private string method_2(string string_0)
        {
            string string_ = string_0.Trim();
            List<string> list = ParaToList(string_, GClass0.smethod_0(":"), false);
            foreach (string item in list)
            {
                OutputResult(GClass0.smethod_0("KľȻ\u0339捨䯾惸摤涂渨藓炕\uf31b") + item, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                int formulaSplitLocation = GetFormulaSplitLocation(item, GClass0.smethod_0(";"));
                if (formulaSplitLocation <= -1)
                {
                    return method_1(item, false);
                }
                string string_2 = CleanFormula(LEFT(item, formulaSplitLocation));
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                if (logicExplain.LoopLogicFormula(string_2) > 0.0)
                {
                    string string_3 = MID(item, formulaSplitLocation + 1, -9999);
                    return method_1(string_3, false);
                }
            }
            return "";
        }

        private string method_3(string string_0)
        {
            _ShowQuestion = "";
            _strTextSplit = GClass0.smethod_0("\u3000");
            _listShowCode.Clear();
            string text = "";
            string text2 = GClass0.smethod_0("\u3000");
            List<string> list = ParaToList(string_0, GClass0.smethod_0(";"), false);
            if (method_100(string_0) == 2)
            {
                if (LEFT(list[list.Count() - 1], 1) != "#" && LEFT(list[list.Count() - 1], 1) != GClass0.smethod_0("%"))
                {
                    text2 = list[list.Count() - 1];
                    list.RemoveAt(list.Count() - 1);
                }
                string text3 = list[0];
                string text4 = (list.Count > 1) ? list[1] : list[0];
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                if (text3 != text4)
                {
                    text3 = GClass0.smethod_0("Z") + text3 + GClass0.smethod_0("_ħ") + text4;
                }
                string string_ = CleanFormula(text3);
                if (logicExplain.LoopLogicFormula(string_) > 0.0)
                {
                    foreach (classLoopB item in logicExplain._LoopQn.A)
                    {
                        foreach (classLoopQuestion item2 in item.B)
                        {
                            if (item2.Qn[GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64")] == GClass0.smethod_0("0"))
                            {
                                text = text + GClass0.smethod_0("-") + item2.Qn[text4];
                            }
                        }
                    }
                    if (text.Length > 0)
                    {
                        text = MID(text, 1, -9999);
                        _ShowQuestion = text4;
                    }
                }
            }
            else
            {
                if (list.Count() > 1)
                {
                    text2 = list[1];
                }
                string string_ = CleanFormula(list[0]);
                text = GetAnswer(string_);
                if (text.Length > 0)
                {
                    _ShowQuestion = string_;
                }
            }
            if (text.Length > 0)
            {
                _strTextSplit = text2;
                if (method_117(_strTextSplit))
                {
                    text2 = GClass0.smethod_0("\u3000");
                }
                string[] string_2 = text.Split(',');
                _listShowCode = method_129(string_2);
                text = ((!method_117(_strTextSplit)) ? method_130(_listShowCode, text2, "") : method_130(_listShowCode, text2, _strTextSplit));
            }
            return text;
        }

        private string method_4(string string_0 = "")
        {
            string text = "";
            if (string_0 != "")
            {
                method_3(string_0);
            }
            if (_ShowQuestion.Length > 0)
            {
                bool flag;
                string text2 = (flag = method_117(_strTextSplit)) ? GClass0.smethod_0("\u3000") : _strTextSplit;
                foreach (string item in _listShowCode)
                {
                    string text3 = method_145(_ShowQuestion, item);
                    if (flag)
                    {
                        text = text + text2 + method_137(text3, _strTextSplit, GClass0.smethod_0("1"));
                    }
                    else if (text3 != "")
                    {
                        text = text + text2 + text3;
                    }
                }
                if (text2.Length > 0)
                {
                    text = MID(text, text2.Length, -9999);
                }
            }
            return text;
        }

        private string method_5(string string_0)
        {
            method_101(string_0, 0);
            return method_6(string_0);
        }

        private string method_6(string string_0)
        {
            string text = DeleteOuterSymbol(string_0);
            if (text.Length <= 0)
            {
                return "";
            }
            if (GetFormulaSplitLocation(string_0, GClass0.smethod_0("zģɤ\u033fмԼ")) <= 0)
            {
                if (GetFormulaSplitLocation(string_0, GClass0.smethod_0(".Ĩȯ\u032e")) <= 0)
                {
                    return method_14(string_0);
                }
                return method_12(string_0, GClass0.smethod_0("*"), 0.0).ToString();
            }
            return method_7(string_0).ToString();
        }

        private double method_7(string string_0)
        {
            double num = 0.0;
            string text = DeleteOuterSymbol(string_0);
            int length = text.Length;
            if (length > 0)
            {
                OutputResult(GClass0.smethod_0("2ŏɞ\u0335Щԩ昳墭箩啇璣\uf419\u0c43\uf21c") + _nTotalLoopA + GClass0.smethod_0("(ģɀ\u033c") + _nTotalLoopB, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                for (int i = 0; i < _nTotalLoopA; i++)
                {
                    _nCurrentLoopA = i;
                    for (int j = 0; j < _nTotalLoopB; j++)
                    {
                        _nCurrentLoopB = j;
                        OutputResult(GClass0.smethod_0("1Ŏə\u0334孚坅妭璩呇瞣\uf519\u0b43\uf31c") + (i + 1) + GClass0.smethod_0("(ģɀ\u033c") + (j + 1), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                        if (method_8(text, GClass0.smethod_0("}"), false))
                        {
                            _LoopQn.A[i].B[j].Qn[GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64")] = GClass0.smethod_0("0");
                            num += 1.0;
                        }
                    }
                }
            }
            _nCountResultIsTrue = (int)num;
            return num;
        }

        private bool method_8(string string_0, string string_1 = "|", bool bool_0 = false)
        {
            bool flag = false;
            bool flag2 = false;
            string text = DeleteOuterSymbol(string_0);
            int length = text.Length;
            if (length > 0)
            {
                string string_2 = GClass0.smethod_0("}");
                bool bool_ = false;
                int formulaSplitLocation;
                do
                {
                    formulaSplitLocation = GetFormulaSplitLocation(text, GClass0.smethod_0("\u007fĤɡ"));
                    if (formulaSplitLocation <= -1)
                    {
                        flag2 = ((text.Length != length) ? method_8(text, string_2, bool_) : method_10(text));
                    }
                    else
                    {
                        string string_3 = LEFT(text, formulaSplitLocation);
                        string text2 = MID(text, formulaSplitLocation, 1);
                        text = MID(text, formulaSplitLocation + 1, -9999);
                        flag2 = method_8(string_3, string_2, bool_);
                        string_2 = text2;
                        bool_ = flag2;
                    }
                }
                while (formulaSplitLocation > 0);
            }
            flag = method_9(flag2, string_1, bool_0);
            OutputResult(GClass0.smethod_0("Mĺȸ\u0325崊扄阾袕蟓犕\uf51b") + string_0 + GClass0.smethod_0("#Ŀȡ") + flag2.ToString(), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            return flag;
        }

        private bool method_9(bool bool_0, string string_0, bool bool_1)
        {
            bool result = false;
            if (!(string_0 == GClass0.smethod_0("}")))
            {
                if (!(string_0 == GClass0.smethod_0("'")))
                {
                    if (string_0 == GClass0.smethod_0(" "))
                    {
                        result = (bool_0 ^ bool_1);
                    }
                }
                else
                {
                    result = (bool_0 && bool_1);
                }
            }
            else
            {
                result = (bool_0 | bool_1);
            }
            return result;
        }

        private bool method_10(string string_0)
        {
            bool result = false;
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                int formulaSplitLocation = GetFormulaSplitLocation(text, GClass0.smethod_0(";ĸȸ\u032eШԯخ"));
                result = ((formulaSplitLocation > -1) ? method_11(text) : ((!method_117(text)) ? method_16(text) : method_123(text)));
            }
            return result;
        }

        private bool method_11(string string_0)
        {
            bool result = false;
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                string string_ = text;
                string text2 = GClass0.smethod_0("?");
                double num = 0.0;
                int formulaSplitLocation = GetFormulaSplitLocation(text, GClass0.smethod_0("?ļȼ"));
                int num2 = 1;
                if (formulaSplitLocation > -1)
                {
                    string_ = LEFT(text, formulaSplitLocation);
                    text2 = GClass0.smethod_0("?ļȼ");
                    if (text2.IndexOf(MID(text, formulaSplitLocation + 1, 1)) > -1)
                    {
                        num2 = 2;
                    }
                    text2 = MID(text, formulaSplitLocation, num2);
                    string string_2 = MID(text, formulaSplitLocation + num2, -9999);
                    num = method_12(string_2, GClass0.smethod_0("*"), 0.0);
                }
                double num3 = method_12(string_, GClass0.smethod_0("*"), 0.0);
                string a = text2;
                if (!(a == GClass0.smethod_0("?")))
                {
                    if (!(a == GClass0.smethod_0("=")))
                    {
                        if (!(a == GClass0.smethod_0("<")))
                        {
                            if (!(a == GClass0.smethod_0(">Ŀ")))
                            {
                                if (!(a == GClass0.smethod_0("<ļ")))
                                {
                                    if (a == GClass0.smethod_0(">ļ"))
                                    {
                                        result = (num3 <= num);
                                    }
                                }
                                else
                                {
                                    result = (num3 >= num);
                                }
                            }
                            else
                            {
                                result = (num3 != num);
                            }
                        }
                        else
                        {
                            result = (num3 == num);
                        }
                    }
                    else
                    {
                        result = (num3 < num);
                    }
                }
                else
                {
                    result = (num3 > num);
                }
            }
            return result;
        }

        private double method_12(string string_0, string string_1 = "+", double double_0 = 0.0)
        {
            double result = 0.0;
            double num = 0.0;
            string text = DeleteOuterSymbol(string_0);
            int length = text.Length;
            if (length != 1 || !GClass0.smethod_0(".Ĩȯ\u032e").Contains(text))
            {
                if (length > 0)
                {
                    string string_2 = GClass0.smethod_0("*");
                    double double_ = 0.0;
                    int formulaSplitLocation;
                    do
                    {
                        formulaSplitLocation = GetFormulaSplitLocation(text, GClass0.smethod_0(".Ĩȯ\u032e"));
                        if (formulaSplitLocation != 0)
                        {
                            if (formulaSplitLocation <= 0)
                            {
                                num = ((text.Length != length) ? method_12(text, string_2, double_) : method_125(method_6(text)));
                            }
                            else
                            {
                                string string_3 = LEFT(text, formulaSplitLocation);
                                string text2 = MID(text, formulaSplitLocation, 1);
                                text = MID(text, formulaSplitLocation + 1, -9999);
                                num = method_12(string_3, string_2, double_);
                                string_2 = text2;
                                double_ = num;
                            }
                        }
                        else
                        {
                            string text2 = LEFT(text, 1);
                            text = MID(text, 1, -9999);
                            num = 0.0;
                            string_2 = text2;
                            double_ = 0.0;
                        }
                    }
                    while (formulaSplitLocation > -1);
                }
                result = method_13(double_0, string_1, num);
            }
            OutputResult(GClass0.smethod_0("Mĺȿ\u0325崊扄捵圸蟓犕\uf51b") + text + GClass0.smethod_0("#Ŀȡ") + result.ToString(), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            return result;
        }

        private double method_13(double double_0, string string_0, double double_1)
        {
            double result = 0.0;
            if (!(string_0 == GClass0.smethod_0("*")))
            {
                if (!(string_0 == GClass0.smethod_0(",")))
                {
                    if (!(string_0 == GClass0.smethod_0("+")))
                    {
                        if (string_0 == GClass0.smethod_0(".") && double_1 != 0.0)
                        {
                            result = double_0 / double_1;
                        }
                    }
                    else
                    {
                        result = double_0 * double_1;
                    }
                }
                else
                {
                    result = double_0 - double_1;
                }
            }
            else
            {
                result = double_0 + double_1;
            }
            return result;
        }

        private string method_14(string string_0)
        {
            string result = "";
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                string text2 = LEFT(text, 1);
                string a = LEFT(text, 2);
                result = (method_89(text) ? method_1(text, false) : ((a == GClass0.smethod_0("&ź") || a == GClass0.smethod_0("!ź")) ? method_96(LEFT(text, text.Length - 1)) : ((text2 == GClass0.smethod_0("%")) ? method_27(text) : ((GetFormulaSplitLocation(text, GClass0.smethod_0(".Ĩȯ\u032e")) > -1) ? method_12(text, GClass0.smethod_0("*"), 0.0).ToString() : ((!method_116(text2)) ? method_15(text) : text)))));
            }
            return result;
        }

        private string method_15(string string_0)
        {
            string result = "";
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                result = ((text.IndexOf(GClass0.smethod_0(")")) <= -1) ? method_96(text) : method_124(method_16(text)).ToString());
            }
            return result;
        }

        private bool method_16(string string_0)
        {
            bool result = false;
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                if (text.IndexOf(GClass0.smethod_0(")")) <= -1)
                {
                    result = (method_89(text) ? method_123(method_1(text, false)) : ((!method_117(text)) ? method_123(method_96(text)) : method_123(text)));
                }
                else
                {
                    int num = method_88(text);
                    if (num == -1)
                    {
                        result = method_123(method_27(text));
                    }
                    else
                    {
                        string string_ = LEFT(text, num);
                        string text2 = MID(text, num + 1, -9999);
                        text2 = LEFT(text2, text2.Length - 1);
                        string_ = method_17(string_);
                        result = method_18(string_, text2, GClass0.smethod_0("}"), false);
                    }
                }
            }
            return result;
        }

        private string method_17(string string_0)
        {
            string text = "";
            string text2 = DeleteOuterSymbol(string_0);
            if (text2.Length > 0)
            {
                List<string> list = ParaToList(text2, GClass0.smethod_0("}"), false);
                foreach (string item in list)
                {
                    string text3 = "";
                    string text4 = LEFT(item, 1);
                    string a = LEFT(item, 2);
                    text = string.Concat(str2: (GetFormulaSplitLocation(item, GClass0.smethod_0(".Ĩȯ\u032e")) <= 0) ? ((!(a == GClass0.smethod_0("Yģ"))) ? ((!(a == GClass0.smethod_0("&ź")) && !(a == GClass0.smethod_0("!ź"))) ? ((!(a == GClass0.smethod_0("$ź"))) ? ((!(text4 == GClass0.smethod_0("%"))) ? ((item.IndexOf(GClass0.smethod_0("\u007f")) <= -1) ? ((!method_116(text4) && !(text4 == GClass0.smethod_0(",")) && !(text4 == GClass0.smethod_0("*"))) ? method_96(item) : method_12(item, GClass0.smethod_0("*"), 0.0).ToString()) : method_114(item)) : method_27(item)) : method_1(item, false)) : method_96(LEFT(item, item.Length - 1))) : method_1(item, false)) : method_12(item, GClass0.smethod_0("*"), 0.0).ToString(), str0: text, str1: GClass0.smethod_0("-"));
                }
            }
            if (LEFT(text, 1) == ','.ToString())
            {
                text = MID(text, 1, -9999);
            }
            return text;
        }

        private bool method_18(string string_0, string string_1, string string_2 = "|", bool bool_0 = false)
        {
            string text = DeleteOuterSymbol(string_1);
            int length = text.Length;
            bool flag = false;
            string string_3 = GClass0.smethod_0("}");
            bool bool_ = false;
            int formulaSplitLocation;
            do
            {
                formulaSplitLocation = GetFormulaSplitLocation(text, GClass0.smethod_0("\u007fĤɡ"));
                if (formulaSplitLocation <= -1)
                {
                    flag = ((text.Length != length) ? method_18(string_0, text, string_3, bool_) : method_19(string_0, text));
                }
                else
                {
                    string string_4 = LEFT(text, formulaSplitLocation);
                    string text2 = MID(text, formulaSplitLocation, 1);
                    text = MID(text, formulaSplitLocation + 1, -9999);
                    flag = method_18(string_0, string_4, string_3, bool_);
                    string_3 = text2;
                    bool_ = flag;
                }
            }
            while (formulaSplitLocation > -1);
            return method_9(flag, string_2, bool_0);
        }

        private bool method_19(string string_0, string string_1)
        {
            bool flag = false;
            string text = string_1.Trim();
            if (string_0.Length != 0 || text.Length != 0)
            {
                if (string_0.Length <= 0 || text.Length != 0)
                {
                    if (!method_89(text))
                    {
                        text = DeleteOuterSymbol(text);
                        string text2 = "";
                        if (string_0.Length != 0 || text.Length != 0)
                        {
                            if (string_0.Length <= 0 || text.Length != 0)
                            {
                                string text3 = LEFT(text, 1);
                                string a = MID(text, 1, 1);
                                int formulaSplitLocation = GetFormulaSplitLocation(text, GClass0.smethod_0("\u007f"));
                                if (formulaSplitLocation > -1)
                                {
                                    double double_ = method_12(LEFT(text, formulaSplitLocation), GClass0.smethod_0("*"), 0.0);
                                    double double_2 = method_12(MID(text, formulaSplitLocation + 1, -9999), GClass0.smethod_0("*"), 0.0);
                                    flag = method_20(string_0, double_, double_2);
                                }
                                else if (text3 == GClass0.smethod_0("$"))
                                {
                                    flag = (string_0.Length > 0);
                                }
                                else if (text3 == GClass0.smethod_0("%"))
                                {
                                    text2 = method_27(text);
                                    flag = method_24(string_0, text2, GClass0.smethod_0("}"));
                                }
                                else if (text.IndexOf(',') > -1)
                                {
                                    flag = method_24(string_0, text, GClass0.smethod_0("}"));
                                }
                                else if (method_116(text3) || text3 == GClass0.smethod_0("*") || text3 == GClass0.smethod_0(","))
                                {
                                    double double_3 = method_12(text, GClass0.smethod_0("*"), 0.0);
                                    flag = method_21(string_0, double_3);
                                }
                                else if (text3 == GClass0.smethod_0("?"))
                                {
                                    double double_3 = (!(a == GClass0.smethod_0("<"))) ? method_12(MID(text, 1, -9999), GClass0.smethod_0("*"), 0.0) : (method_12(MID(text, 2, -9999), GClass0.smethod_0("*"), 0.0) - 1.0);
                                    flag = method_22(string_0, double_3);
                                }
                                else if (text3 == GClass0.smethod_0("="))
                                {
                                    double double_3 = (!(a == GClass0.smethod_0("<"))) ? method_12(MID(text, 1, -9999), GClass0.smethod_0("*"), 0.0) : (method_12(MID(text, 2, -9999), GClass0.smethod_0("*"), 0.0) + 1.0);
                                    flag = method_23(string_0, double_3);
                                }
                                else
                                {
                                    string string_2;
                                    if (text3 == GClass0.smethod_0("A"))
                                    {
                                        string_2 = GClass0.smethod_0("'");
                                        text = MID(text, 1, -9999);
                                    }
                                    else
                                    {
                                        string_2 = GClass0.smethod_0("}");
                                    }
                                    text2 = method_6(text);
                                    flag = method_24(string_0, text2, string_2);
                                }
                                return flag;
                            }
                            return false;
                        }
                        return true;
                    }
                    return method_25(string_0, string_1, GClass0.smethod_0("@ŕɖ\u0343э"));
                }
                return false;
            }
            return true;
        }

        private bool method_20(string string_0, double double_0, double double_1)
        {
            if (string_0.Length > 0)
            {
                string[] array = string_0.Split(',');
                string[] array2 = array;
                foreach (string string_ in array2)
                {
                    double num = method_125(string_);
                    if (num >= double_0 && num <= double_1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool method_21(string string_0, double double_0)
        {
            if (string_0.Length > 0)
            {
                string[] array = string_0.Split(',');
                string[] array2 = array;
                foreach (string string_ in array2)
                {
                    double num = method_125(string_);
                    if (num == double_0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool method_22(string string_0, double double_0)
        {
            if (string_0.Length > 0)
            {
                string[] array = string_0.Split(',');
                string[] array2 = array;
                foreach (string string_ in array2)
                {
                    double num = method_125(string_);
                    if (num > double_0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool method_23(string string_0, double double_0)
        {
            if (string_0.Length > 0)
            {
                string[] array = string_0.Split(',');
                string[] array2 = array;
                foreach (string string_ in array2)
                {
                    double num = method_125(string_);
                    if (num < double_0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool method_24(string string_0, string string_1, string string_2 = "|")
        {
            bool flag = false;
            if (string_0.Length != 0 || string_1.Length != 0)
            {
                if (string_0.Length != 0 || string_1.Length <= 0)
                {
                    if (string_0.Length <= 0 || string_1.Length != 0)
                    {
                        string[] array = string_0.Split(',');
                        string[] array2 = string_1.Split(',');
                        if (!(string_2 == GClass0.smethod_0("'")))
                        {
                            string[] array3 = array;
                            foreach (string b in array3)
                            {
                                string[] array4 = array2;
                                foreach (string a in array4)
                                {
                                    if (a == b)
                                    {
                                        return true;
                                    }
                                }
                            }
                            return false;
                        }
                        string[] array5 = array2;
                        int num = 0;
                        while (true)
                        {
                            if (num >= array5.Length)
                            {
                                return true;
                            }
                            string a2 = array5[num];
                            flag = false;
                            string[] array6 = array;
                            foreach (string b2 in array6)
                            {
                                if (a2 == b2)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                break;
                            }
                            num++;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return true;
        }

        private bool method_25(string string_0, string string_1, string string_2 = "EQUAL")
        {
            bool result = false;
            string text = DeleteOuterSymbol(string_1);
            if (string_0.Length != 0 || text.Length != 0)
            {
                if (string_0.Length <= 0 || text.Length != 0)
                {
                    bool flag = false;
                    string string_3 = GClass0.smethod_0("}");
                    bool bool_ = false;
                    int length = text.Length;
                    int num = 0;
                    do
                    {
                        num = GetFormulaSplitLocation(text, GClass0.smethod_0("\u007fĤɡ"));
                        if (num <= -1)
                        {
                            if (text.Length == length)
                            {
                                result = method_26(string_0, text, string_2);
                            }
                            else
                            {
                                flag = method_25(string_0, text, string_2);
                                result = method_9(flag, string_3, bool_);
                            }
                            break;
                        }
                        string string_4 = LEFT(text, num);
                        string text2 = MID(text, num, 1);
                        text = MID(text, num + 1, -9999);
                        flag = method_25(string_0, string_4, string_2);
                        bool_ = method_9(flag, string_3, bool_);
                        string_3 = text2;
                    }
                    while (num > 0);
                    OutputResult(GClass0.smethod_0("JĹȾ\u0324崅扅掁怩提蚀\uf518\u0b7a") + string_0 + GClass0.smethod_0("\u007fġ") + string_2 + GClass0.smethod_0("\"ź") + string_1 + GClass0.smethod_0("yģȿ\u0321") + result.ToString(), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                    return result;
                }
                return false;
            }
            return true;
        }

        private bool method_26(string string_0, string string_1, string string_2 = "EQUAL")
        {
            bool result = false;
            string text = method_6(string_1);
            if (string_2 == GClass0.smethod_0("@ŕɖ\u0343э"))
            {
                result = (string_0 == text);
            }
            else if (string_2 == GClass0.smethod_0("HŊɉ\u0344"))
            {
                result = string_0.Contains(text);
            }
            else if (string_2 == GClass0.smethod_0("Kŏ"))
            {
                result = text.Contains(string_0);
            }
            return result;
        }

        private string method_27(string string_0)
        {
            string text = "";
            string text2 = DeleteOuterSymbol(string_0);
            if (text2.Length > 0 && LEFT(text2, 1) == GClass0.smethod_0("%"))
            {
                int num = text2.IndexOf(GClass0.smethod_0(")"));
                string text3 = MID(text2, 1, num - 1);
                text3 = text3.ToUpper();
                string text4 = MID(text2, num + 1, -9999);
                text4 = LEFT(text4, text4.Length - 1);
                if (text3 == GClass0.smethod_0("Mōɕ"))
                {
                    text = method_28(text4);
                }
                else if (text3 == GClass0.smethod_0("@Ōɀ") || text3 == GClass0.smethod_0("Fŋɖ\u034cѕ"))
                {
                    text = method_29(text4);
                }
                else if (text3 == GClass0.smethod_0("PŗɌ"))
                {
                    text = method_44(text4);
                }
                else if (text3 == GClass0.smethod_0("Oōɓ") || text3 == GClass0.smethod_0("@ōɓ"))
                {
                    text = method_45(text4);
                }
                else if (text3 == GClass0.smethod_0("@Ōɍ") || text3 == GClass0.smethod_0("M") || text3 == GClass0.smethod_0("HŌɍ\u0351"))
                {
                    text = method_43(text4);
                }
                else if (text3 == GClass0.smethod_0("@Ŋɖ\u034eуՕ") || text3 == GClass0.smethod_0("G"))
                {
                    text = method_33(text4);
                }
                else if (text3 == GClass0.smethod_0("Mœ") || text3 == GClass0.smethod_0("BņɅ"))
                {
                    text = method_34(text4);
                }
                else if (text3 == GClass0.smethod_0("GŇɍ"))
                {
                    text = method_38(text4);
                }
                else if (text3 == GClass0.smethod_0("Oņɇ\u0351"))
                {
                    text = method_39(text4);
                }
                else if (text3 == GClass0.smethod_0("BŌɅ"))
                {
                    text = method_35(text4);
                }
                else if (text3 == GClass0.smethod_0("[ōɓ"))
                {
                    text = method_36(text4);
                }
                else if (text3 == GClass0.smethod_0("JŐɋ\u0347ѓ") || text3 == GClass0.smethod_0("LŖɉ"))
                {
                    text = method_56(text4, GClass0.smethod_0("%") + text3 + GClass0.smethod_0(")"));
                }
                else if (text3 == GClass0.smethod_0("Kňɂ\u0340ѐՆ\u065aݕ"))
                {
                    text = method_57(text4);
                }
                else if (text3 == GClass0.smethod_0("Fōɓ") || text3 == GClass0.smethod_0("CŃɉ\u0357тՏل"))
                {
                    text = method_37(text4);
                }
                else if (text3 == GClass0.smethod_0("HŊɐ\u034dїՌ"))
                {
                    text = method_47(text4);
                }
                else if (text3 == GClass0.smethod_0("Dņɜ\u0344юՌي\u0746ࡑ\u0944"))
                {
                    text = method_49(text4);
                }
                else if (text3 == GClass0.smethod_0("Iŉɑ\u0353ьՐم"))
                {
                    text = method_48(text4);
                }
                else if (text3 == GClass0.smethod_0("Fňɒ\u0340щՂ\u064bݍ"))
                {
                    text = method_50(text4);
                }
                else if (text3 == GClass0.smethod_0("Iŉɑ\u034cїՖ\u0651"))
                {
                    text = method_51(text4);
                }
                else if (text3 == GClass0.smethod_0("Fňɒ\u0346хՑ\u064cݎ"))
                {
                    text = method_52(text4);
                }
                else if (text3 == GClass0.smethod_0("Kŋɗ\u034bх"))
                {
                    text = method_54(text4);
                }
                else if (text3 == GClass0.smethod_0("HŊɐ\u0355ыՏ"))
                {
                    text = method_53(text4);
                }
                else if (text3 == GClass0.smethod_0("@łɖ\u0344") || text3 == GClass0.smethod_0("E"))
                {
                    text = method_65(text4);
                }
                else if (text3 == GClass0.smethod_0("PŊɏ\u0344") || text3 == GClass0.smethod_0("U"))
                {
                    text = method_66(text4);
                }
                else if (text3 == GClass0.smethod_0("Lņɒ\u0340ѐՊ\u064f\u0744") || text3 == GClass0.smethod_0("Fŕ"))
                {
                    text = method_67(text4);
                }
                else if (text3 == GClass0.smethod_0("PŃɀ\u034fчՃ\u0658") || text3 == GClass0.smethod_0("UŅ"))
                {
                    text = method_79(text4);
                }
                else if (text3 == GClass0.smethod_0("Qŋɇ\u0343ј"))
                {
                    text = method_68(text4);
                }
                else if (text3 == GClass0.smethod_0("Mōɖ"))
                {
                    text = method_69(text4);
                }
                else if (text3 == GClass0.smethod_0("FŌɅ"))
                {
                    text = method_80(text4);
                }
                else if (text3 == GClass0.smethod_0("VŐɂ\u0350ѕ"))
                {
                    text = method_81(text4);
                }
                else if (text3 == GClass0.smethod_0("HŌɌ\u0346"))
                {
                    text = method_82(text4);
                }
                else if (text3 == GClass0.smethod_0("JŌɕ") || text3 == GClass0.smethod_0("@Ōɕ\u034f") || text3 == GClass0.smethod_0("[Ňɒ\u0348сՀ\u064cݕࡏ") || text3 == GClass0.smethod_0("CňɌ\u034dѓ"))
                {
                    text = method_30(text4, 0.0);
                }
                else if (text3 == GClass0.smethod_0("Wŋɖ\u034cх"))
                {
                    text = method_30(text4, 0.5);
                }
                else if (text3 == GClass0.smethod_0("Wő") || text3 == GClass0.smethod_0("Uŉɐ\u034aч\u0557\u0651"))
                {
                    text = method_30(text4, 0.999999999999999);
                }
                else if (text3 == GClass0.smethod_0("Rśɒ"))
                {
                    text = method_31(text4);
                }
                else if (text3 == GClass0.smethod_0("NōɅ"))
                {
                    text = method_32(text4);
                }
                else if (text3 == GClass0.smethod_0("NŃə"))
                {
                    text = method_40(text4, true);
                }
                else if (text3 == GClass0.smethod_0("Nŋɏ"))
                {
                    text = method_40(text4, false);
                }
                else if (text3 == GClass0.smethod_0("TńɊ\u0347эՌ") || text3 == GClass0.smethod_0("VłɌ\u0345"))
                {
                    text = method_41(text4);
                }
                else if (text3 == GClass0.smethod_0("Płɉ\u0344"))
                {
                    text = method_42(text4);
                }
                else if (text3 == GClass0.smethod_0("Wōɑ"))
                {
                    text = method_46(text4, true, false);
                }
                else if (text3 == GClass0.smethod_0("Hłɑ\u0355"))
                {
                    text = method_46(text4, false, false);
                }
                else if (text3 == GClass0.smethod_0("UŇɋ\u034fїՍ\u0651") || text3 == GClass0.smethod_0("UŇɋ\u034fюՃ\u0659"))
                {
                    text = method_46(text4, true, true);
                }
                else if (text3 == GClass0.smethod_0("ZņɈ\u034eшՂ\u0651ݕ") || text3 == GClass0.smethod_0("UŇɋ\u034fюՋ\u064f"))
                {
                    text = method_46(text4, false, true);
                }
                else if (text3 == GClass0.smethod_0("PŖɓ") || text3 == GClass0.smethod_0("R") || text3 == GClass0.smethod_0("Uőɖ\u034aьՆ"))
                {
                    text = method_55(text4);
                }
                else if (text3 == GClass0.smethod_0("OŇɏ") || text3 == GClass0.smethod_0("JŀɊ\u0344іՉ"))
                {
                    text = method_58(text4);
                }
                else if (text3 == GClass0.smethod_0("UŃɍ") || text3 == GClass0.smethod_0("SŅɏ\u0357ф"))
                {
                    text = method_59(text4);
                }
                else if (text3 == GClass0.smethod_0("HņɄ\u0355"))
                {
                    text = method_60(text4);
                }
                else if (text3 == GClass0.smethod_0("NŋɅ"))
                {
                    text = method_61(text4);
                }
                else if (text3 == GClass0.smethod_0("PŗɃ"))
                {
                    text = method_62(text4);
                }
                else if (text3 == GClass0.smethod_0("WōɄ\u034aѕ"))
                {
                    text = method_63(text4);
                }
                else if (text3 == GClass0.smethod_0("Pőɋ\u034c"))
                {
                    text = method_64(text4, "");
                }
                else if (text3 == GClass0.smethod_0("IŐɑ\u034bь"))
                {
                    text = method_64(text4, GClass0.smethod_0("M"));
                }
                else if (text3 == GClass0.smethod_0("WŐɑ\u034bь"))
                {
                    text = method_64(text4, GClass0.smethod_0("S"));
                }
                else if (text3 == GClass0.smethod_0("]ņɃ\u0353") || text3 == GClass0.smethod_0("[œ"))
                {
                    text = method_70(text4);
                }
                else if (text3 == GClass0.smethod_0("Hŋɍ\u0356щ") || text3 == GClass0.smethod_0("Oŉ"))
                {
                    text = method_71(text4);
                }
                else if (text3 == GClass0.smethod_0("GŃɘ") || text3 == GClass0.smethod_0("FŘ"))
                {
                    text = method_72(text4);
                }
                else if (text3 == GClass0.smethod_0("LŌɗ\u0353") || text3 == GClass0.smethod_0("Jœ"))
                {
                    text = method_73(text4);
                }
                else if (text3 == GClass0.smethod_0("KŌɊ\u0356іՄ") || text3 == GClass0.smethod_0("Oŏ"))
                {
                    text = method_74(text4);
                }
                else if (text3 == GClass0.smethod_0("Uŀɇ\u034cьՅ") || text3 == GClass0.smethod_0("Qł"))
                {
                    text = method_75(text4);
                }
                else if (text3 == GClass0.smethod_0("Iŉɑ\u0340тՖل"))
                {
                    text = method_76(text4);
                }
                else if (text3 == GClass0.smethod_0("Iŉɑ\u0350ъՏل"))
                {
                    text = method_77(text4);
                }
                else if (text3 == GClass0.smethod_0("EŅɝ\u034cцՒ\u0640ݐࡊ\u094f\u0a44"))
                {
                    text = method_78(text4);
                }
                else if (text3 == GClass0.smethod_0("GŐ") || text3 == GClass0.smethod_0("@ŕɖ\u0343э"))
                {
                    text = method_83(text4, GClass0.smethod_0("@ŕɖ\u0343э"));
                }
                else if (text3 == GClass0.smethod_0("HŊɉ\u0344"))
                {
                    text = method_83(text4, GClass0.smethod_0("HŊɉ\u0344"));
                }
                else if (text3 == GClass0.smethod_0("Kŏ"))
                {
                    text = method_83(text4, GClass0.smethod_0("Kŏ"));
                }
                else if (text3 == GClass0.smethod_0("@ņɐ\u0340тՊ\u064e\u0744"))
                {
                    text = method_84(text4);
                }
                else if (text3 == GClass0.smethod_0("HŊɂ\u034aюՄ"))
                {
                    text = method_85(text4);
                }
                else if (text3 == GClass0.smethod_0("HŊɐ\u0351чՂ"))
                {
                    text = method_86();
                }
                else
                {
                    text = "";
                    MessageBox.Show(GClass0.smethod_0("衬躽崍\u0321") + text2 + GClass0.smethod_0("'伫璁勹慳Ԣإ") + text3 + GClass0.smethod_0("!ĨȮ\u0326话戮夃哓㠃"), GClass0.smethod_0("凹摳霛裮"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    OutputResult(GClass0.smethod_0("Nķȿ\u0328豬誽复ܡ") + text2 + GClass0.smethod_0("'伫璁勹慳Ԣإ") + text3 + GClass0.smethod_0("!ĨȮ\u0326话戮夃哓㠃"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                }
            }
            OutputResult(GClass0.smethod_0("AĶȼ\u0329嗾恲亂") + text2 + GClass0.smethod_0("#Ŀȡ") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            return text;
        }

        private string method_28(string string_0)
        {
            bool flag = false;
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                flag = method_8(text.Trim(), GClass0.smethod_0("}"), false);
            }
            if (!flag)
            {
                return GClass0.smethod_0("0");
            }
            return GClass0.smethod_0("1");
        }

        private string method_29(string string_0)
        {
            int num = 0;
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                int formulaSplitLocation = GetFormulaSplitLocation(text, GClass0.smethod_0("-"));
                string text2;
                string string_;
                if (formulaSplitLocation > -1)
                {
                    string_ = LEFT(text, formulaSplitLocation).Trim();
                    text2 = MID(text, formulaSplitLocation + 1, -9999).Trim();
                }
                else
                {
                    string_ = text.Trim();
                    text2 = GClass0.smethod_0("$").Trim();
                }
                string_ = method_17(string_);
                if (string_ == "")
                {
                    return GClass0.smethod_0("1");
                }
                string[] array = string_.Split(',');
                if (text2 == GClass0.smethod_0("$"))
                {
                    num = array.Count();
                }
                else
                {
                    string[] array2 = array;
                    foreach (string string_2 in array2)
                    {
                        if (method_18(string_2, text2, GClass0.smethod_0("}"), false))
                        {
                            num++;
                        }
                    }
                }
            }
            return num.ToString();
        }

        private string method_30(string string_0, double double_0 = 0.5)
        {
            string result = GClass0.smethod_0("1");
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                int num = 0;
                int num2 = 0;
                int formulaSplitLocation = GetFormulaSplitLocation(text, GClass0.smethod_0("-"));
                string string_;
                if (formulaSplitLocation > -1)
                {
                    string_ = LEFT(text, formulaSplitLocation).Trim();
                    string text2 = MID(text, formulaSplitLocation + 1, -9999).Trim();
                    if (text2 == GClass0.smethod_0("/") || text2 == GClass0.smethod_0(",ı") || text2 == GClass0.smethod_0("2į"))
                    {
                        text2 = GClass0.smethod_0("1");
                    }
                    else if (LEFT(text2, 1) == GClass0.smethod_0("/"))
                    {
                        text2 = MID(text2, 1, -9999);
                    }
                    else if (MID(text2, text2.Length - 1, -9999) == GClass0.smethod_0("/"))
                    {
                        text2 = LEFT(text2, text2.Length - 1);
                    }
                    num2 = method_126(text2);
                }
                else
                {
                    string_ = text.Trim();
                }
                double num3 = method_12(string_, GClass0.smethod_0("*"), 0.0);
                bool flag = false;
                if (num3 < 0.0)
                {
                    flag = true;
                    num3 = 0.0 - num3;
                }
                double num4 = 1.0;
                num4 = ((num > 0) ? Math.Pow(10.0, (double)num) : Math.Pow(10.0, (double)num2));
                num3 = ((num4 == 0.0) ? 0.0 : (Math.Truncate(num3 / num4 + double_0) * num4));
                if (flag)
                {
                    num3 = 0.0 - num3;
                }
                result = num3.ToString();
            }
            return result;
        }

        private string method_31(string string_0)
        {
            string result = GClass0.smethod_0("1");
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text2 = list[0];
                string text3 = (list.Count > 1) ? list[1] : "";
                if (text2 == "" || text2 == GClass0.smethod_0("1") || text3 == "" || text3 == GClass0.smethod_0("1"))
                {
                    return GClass0.smethod_0("1");
                }
                double num = method_12(text2, GClass0.smethod_0("*"), 0.0);
                if (num != 0.0)
                {
                    double num2 = 0.0;
                    double num3 = method_12(text3, GClass0.smethod_0("*"), 0.0);
                    if (num3 != 0.0)
                    {
                        num2 = num % num3;
                    }
                    result = num2.ToString();
                }
            }
            return result;
        }

        private string method_32(string string_0)
        {
            string result = GClass0.smethod_0("1");
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text2 = list[0];
                string text3 = (list.Count > 1) ? list[1] : "";
                if (text3 == "" || text3 == GClass0.smethod_0("1"))
                {
                    return GClass0.smethod_0("1");
                }
                double num = method_12(text2, GClass0.smethod_0("*"), 0.0);
                double num2 = method_12(text3, GClass0.smethod_0("*"), 0.0);
                if (text2 == "" || num == 0.0)
                {
                    return num2.ToString();
                }
                if (num2 == 0.0)
                {
                    return GClass0.smethod_0("1");
                }
                double num3 = 0.0;
                num3 = num % num2;
                if (num3 == 0.0)
                {
                    num3 = num2;
                }
                result = num3.ToString();
            }
            return result;
        }

        private string method_33(string string_0)
        {
            string text = "";
            string text2 = DeleteOuterSymbol(string_0);
            if (text2.Length > 0)
            {
                string text3 = "";
                int num = 1;
                int num2 = 0;
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                text3 = method_6(list[0]);
                if (text3.Length > 0)
                {
                    if (list.Count() > 1)
                    {
                        num = method_126(list[1]);
                        if (num < 1)
                        {
                            num = 1;
                        }
                    }
                    if (list.Count() > 2)
                    {
                        num2 = method_126(list[2]);
                        if (num2 < 0)
                        {
                            num2 = 0;
                        }
                    }
                    List<string> list2 = method_131(text3, ','.ToString());
                    foreach (string item in list2)
                    {
                        double num3 = Convert.ToDouble(item);
                        if (list.Count() > 2)
                        {
                            num3 = method_139(num3, num2, 0, 45);
                            text = text + GClass0.smethod_0("-") + method_137(num3.ToString(), num.ToString() + GClass0.smethod_0("/") + num2.ToString(), GClass0.smethod_0("1"));
                        }
                        else
                        {
                            string text4 = method_137(((int)num3).ToString(), num.ToString() + GClass0.smethod_0(",ı"), GClass0.smethod_0("1"));
                            string text5 = method_143(Convert.ToDouble(item));
                            if (text5.Length > 0)
                            {
                                text4 = text4 + GClass0.smethod_0("/") + text5;
                            }
                            text = text + GClass0.smethod_0("-") + text4;
                        }
                    }
                    text = MID(text, 1, -9999);
                }
            }
            return text;
        }

        private string method_34(string string_0)
        {
            string result = "";
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                string text2 = "";
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item in list)
                {
                    string text3 = method_17(item);
                    if (text3 != "")
                    {
                        text2 = text2 + GClass0.smethod_0("-") + text3;
                    }
                }
                if (text2.Length > 0)
                {
                    if (LEFT(text2, 1) == ','.ToString())
                    {
                        text2 = MID(text2, 1, -9999);
                    }
                    string[] string_ = text2.Split(',');
                    string_ = method_127(string_);
                    result = method_128(string_, ','.ToString(), "");
                }
            }
            return result;
        }

        private string method_35(string string_0)
        {
            string result = "";
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                List<string> list = new List<string>();
                bool flag = true;
                List<string> list2 = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item in list2)
                {
                    string text2 = method_17(item);
                    string[] array = text2.Split(',');
                    if (flag)
                    {
                        result = text2;
                        list = method_129(array);
                        if (list.Count() == 0)
                        {
                            return "";
                        }
                        flag = false;
                    }
                    else
                    {
                        for (int num = list.Count - 1; num >= 0; num--)
                        {
                            string text3 = list[num];
                            bool flag2 = true;
                            string[] array2 = array;
                            foreach (string b in array2)
                            {
                                if (text3 == b)
                                {
                                    flag2 = false;
                                    break;
                                }
                            }
                            if (flag2)
                            {
                                list.Remove(text3);
                            }
                        }
                        if (list.Count() == 0)
                        {
                            return "";
                        }
                    }
                }
                if (list.Count() > 0)
                {
                    result = method_130(list, ','.ToString(), "");
                }
            }
            return result;
        }

        private string method_36(string string_0)
        {
            string result = "";
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                bool flag = true;
                List<string> list = new List<string>();
                List<string> list2 = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item2 in list2)
                {
                    string text2 = method_17(item2);
                    string[] array = text2.Split(',');
                    if (flag)
                    {
                        list = method_129(array);
                        flag = false;
                    }
                    else
                    {
                        string[] array2 = array;
                        foreach (string item in array2)
                        {
                            if (list.Contains(item))
                            {
                                list.Remove(item);
                            }
                            else
                            {
                                list.Add(item);
                            }
                        }
                    }
                }
                if (list.Count() > 0)
                {
                    result = method_130(list, ','.ToString(), "");
                }
            }
            return result;
        }

        private string method_37(string string_0)
        {
            string text = "";
            string text2 = DeleteOuterSymbol(string_0);
            if (text2.Length > 0)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item in list)
                {
                    string text3 = method_17(item);
                    string[] array = text3.Split(',');
                    string[] array2 = array;
                    foreach (string text4 in array2)
                    {
                        if (dictionary.ContainsKey(text4))
                        {
                            Dictionary<string, int> dictionary2 = dictionary;
                            string key = text4;
                            dictionary2[key]++;
                        }
                        else
                        {
                            dictionary.Add(text4, 1);
                        }
                    }
                }
                int num = list.Count();
                foreach (string key2 in dictionary.Keys)
                {
                    if (key2 != "" && dictionary[key2] < num)
                    {
                        text = text + GClass0.smethod_0("-") + key2;
                    }
                }
                if (text.Length > 1 && LEFT(text, 1) == ','.ToString())
                {
                    text = MID(text, 1, -9999);
                }
            }
            return text;
        }

        private string method_38(string string_0)
        {
            string result = "";
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                bool flag = true;
                List<string> list = new List<string>();
                List<string> list2 = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item in list2)
                {
                    string text2 = method_17(item);
                    if (flag)
                    {
                        string[] string_ = text2.Split(',');
                        list = method_129(string_);
                        if (list.Count() == 0)
                        {
                            return "";
                        }
                        flag = false;
                    }
                    else
                    {
                        for (int num = list.Count - 1; num >= 0; num--)
                        {
                            string text3 = list[num];
                            if (method_19(text3, text2))
                            {
                                list.Remove(text3);
                                if (list.Count() == 0)
                                {
                                    return "";
                                }
                            }
                        }
                    }
                }
                if (list.Count() > 0)
                {
                    result = method_130(list, ','.ToString(), "");
                }
            }
            return result;
        }

        private string method_39(string string_0)
        {
            string text = "";
            string text2 = DeleteOuterSymbol(string_0);
            if (text2.Length > 0)
            {
                bool flag = true;
                Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item in list)
                {
                    string text3 = method_17(item);
                    if (flag)
                    {
                        string[] array = text3.Split(',');
                        if (array.Count() == 0)
                        {
                            return "";
                        }
                        string[] array2 = array;
                        foreach (string key in array2)
                        {
                            if (!dictionary.ContainsKey(key))
                            {
                                dictionary.Add(key, false);
                            }
                        }
                        flag = false;
                    }
                    else
                    {
                        for (int j = 0; j < dictionary.Count(); j++)
                        {
                            string text4 = dictionary.Keys.ElementAt(j);
                            if (method_19(text4, text3))
                            {
                                dictionary[text4] = true;
                            }
                        }
                    }
                }
                foreach (string key2 in dictionary.Keys)
                {
                    if (dictionary[key2])
                    {
                        text = text + GClass0.smethod_0("-") + key2;
                    }
                }
                if (text.Length > 0)
                {
                    text = MID(text, 1, -9999);
                }
            }
            return text;
        }

        private string method_40(string string_0, bool bool_0 = true)
        {
            string text = "";
            string string_ = string_0;
            string_ = DeleteOuterSymbol(string_);
            if (string_.Length > 0)
            {
                string text2 = "";
                int num = 1;
                List<string> list = ParaToList(string_0, GClass0.smethod_0(";"), true);
                if (list.Count() > 1)
                {
                    num = method_126(list[1]);
                    if (num < 1)
                    {
                        num = 1;
                    }
                    string_0 = list[0];
                }
                List<string> list2 = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item in list2)
                {
                    string text3 = method_17(item);
                    if (text3 != "")
                    {
                        text2 = text2 + GClass0.smethod_0("-") + text3;
                    }
                }
                if (text2.Length > 0)
                {
                    if (LEFT(text2, 1) == ','.ToString())
                    {
                        text2 = MID(text2, 1, -9999);
                    }
                    List<string> list_ = method_131(text2, ','.ToString());
                    list_ = method_141(list_, bool_0);
                    if (num > list_.Count)
                    {
                        num = list_.Count;
                    }
                    for (int i = 0; i < num; i++)
                    {
                        text = text + GClass0.smethod_0("-") + list_[i];
                    }
                    if (LEFT(text, 1) == ','.ToString())
                    {
                        text = MID(text, 1, -9999);
                    }
                }
            }
            return text;
        }

        private string method_41(string string_0)
        {
            string text = "";
            string string_ = string_0;
            string_ = DeleteOuterSymbol(string_);
            if (string_.Length > 0)
            {
                string text2 = "";
                List<string> list = ParaToList(string_0, GClass0.smethod_0(";"), true);
                int num = 1;
                if (list.Count() > 1)
                {
                    num = method_126(method_6(list[1]));
                    if (num < 1)
                    {
                        return "";
                    }
                    string_0 = list[0];
                }
                string text3 = (list.Count() > 2) ? list[2] : "";
                List<string> list2 = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item in list2)
                {
                    string text4 = method_114(item);
                    if (text4 != "")
                    {
                        text2 = text2 + GClass0.smethod_0("-") + text4;
                    }
                }
                if (LEFT(text2, 1) == ','.ToString())
                {
                    text2 = MID(text2, 1, -9999);
                }
                if (text2.Length > 0)
                {
                    List<string> list_ = method_131(text2, ','.ToString());
                    list_ = method_147(list_);
                    if (num > list_.Count())
                    {
                        num = list_.Count();
                    }
                    if (text3 == "")
                    {
                        for (int i = 0; i < num; i++)
                        {
                            text = text + GClass0.smethod_0("-") + list_[i];
                        }
                        if (LEFT(text, 1) == ','.ToString())
                        {
                            text = MID(text, 1, -9999);
                        }
                    }
                    else
                    {
                        List<string> list3 = new List<string>();
                        if (list_.Count() == num)
                        {
                            list3 = list_;
                        }
                        else
                        {
                            for (int j = 0; j < num; j++)
                            {
                                list3.Add(list_[j]);
                            }
                        }
                        List<string> list4 = method_148(text3);
                        foreach (string item2 in list4)
                        {
                            if (list3.Contains(item2))
                            {
                                text = text + GClass0.smethod_0("-") + item2;
                            }
                        }
                        if (LEFT(text, 1) == ','.ToString())
                        {
                            text = MID(text, 1, -9999);
                        }
                    }
                }
            }
            return text;
        }

        private string method_42(string string_0)
        {
            string text = "";
            string text2 = DeleteOuterSymbol(string_0);
            if (text2.Length > 0)
            {
                string text3 = "";
                int num = 1;
                int num2 = 1;
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                text3 = method_6(list[0]);
                if (text3.Length > 0)
                {
                    if (list.Count() > 1)
                    {
                        num = method_126(list[1]);
                        if (num < 1)
                        {
                            num = 1;
                        }
                    }
                    if (list.Count() > 2)
                    {
                        num2 = method_126(list[2]);
                        if (num2 < 1)
                        {
                            num2 = 1;
                        }
                    }
                    List<string> list2 = method_131(text3, ','.ToString());
                    int num3 = (num + num2 - 1 > list2.Count()) ? list2.Count() : (num + num2 - 1);
                    for (int i = num - 1; i < num3; i++)
                    {
                        text = text + GClass0.smethod_0("-") + list2[i];
                    }
                    if (LEFT(text, 1) == ','.ToString())
                    {
                        text = MID(text, 1, -9999);
                    }
                }
            }
            return text;
        }

        private string method_43(string string_0)
        {
            string result = GClass0.smethod_0("1");
            string text = DeleteOuterSymbol(string_0);
            if (text.Length > 0)
            {
                double num = 0.0;
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                result = logicExplain.LoopLogicFormula(text.Trim()).ToString();
            }
            return result;
        }

        private string method_44(string string_0)
        {
            string result = GClass0.smethod_0("1");
            if (string_0.Length > 0)
            {
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                string text = method_102(string_0, logicExplain);
                if (text != "")
                {
                    double num = 0.0;
                    logicExplain._nCurrentLoopA = 0;
                    foreach (classLoopB item in logicExplain._LoopQn.A)
                    {
                        logicExplain._nCurrentLoopB = 0;
                        foreach (classLoopQuestion item2 in item.B)
                        {
                            if (item2.Qn[GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64")] == GClass0.smethod_0("0"))
                            {
                                num += logicExplain.method_12(text.Trim(), GClass0.smethod_0("*"), 0.0);
                            }
                            logicExplain._nCurrentLoopB++;
                        }
                        logicExplain._nCurrentLoopA++;
                    }
                    result = num.ToString();
                }
            }
            return result;
        }

        private string method_45(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                string text = method_102(string_0, logicExplain);
                if (text != "")
                {
                    List<string> list = new List<string>();
                    foreach (classLoopB item in logicExplain._LoopQn.A)
                    {
                        foreach (classLoopQuestion item2 in item.B)
                        {
                            if (item2.Qn[GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64")] == GClass0.smethod_0("0") && !list.Contains(item2.Qn[text]))
                            {
                                list.Add(item2.Qn[text]);
                            }
                        }
                    }
                    result = method_130(list, ','.ToString(), "");
                }
            }
            return result;
        }

        private string method_46(string string_0, bool bool_0 = true, bool bool_1 = false)
        {
            string text = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0(";"), true);
                string text2 = GClass0.smethod_0("0");
                if (list.Count() > 2)
                {
                    text2 = list[list.Count() - 1];
                }
                string text3 = list[0];
                string key = method_99(text3);
                string text4 = list[1];
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                text3 = GClass0.smethod_0("Z") + text3 + GClass0.smethod_0("_ħ") + text4;
                if (logicExplain.LoopLogicFormula(text3) > 0.0)
                {
                    Dictionary<string, double> dictionary = new Dictionary<string, double>();
                    foreach (classLoopB item in logicExplain._LoopQn.A)
                    {
                        foreach (classLoopQuestion item2 in item.B)
                        {
                            if (item2.Qn[GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64")] == GClass0.smethod_0("0"))
                            {
                                string key2 = item2.Qn[key];
                                double num = method_125(item2.Qn[text4]);
                                if (dictionary.ContainsKey(key2))
                                {
                                    if (num > dictionary[key2])
                                    {
                                        dictionary[key2] = num;
                                    }
                                }
                                else
                                {
                                    dictionary.Add(key2, num);
                                }
                            }
                        }
                    }
                    Dictionary<string, string> dictionary2 = method_103(dictionary, bool_0, bool_1);
                    logicExplain = new LogicExplain();
                    logicExplain.SetData(_dictData, false);
                    logicExplain.method_101(text2, 0);
                    List<string> list2 = ParaToList(text2.Replace(GClass0.smethod_0("}"), GClass0.smethod_0("-")), GClass0.smethod_0("-"), true);
                    foreach (string item3 in list2)
                    {
                        bool flag = true;
                        int num2 = 1;
                        int num3 = dictionary2.Count;
                        string text5 = "";
                        if (LEFT(item3, 2) == GClass0.smethod_0("<ļ"))
                        {
                            num2 = method_126(logicExplain.method_14(MID(item3, 2, -9999)));
                            num2 = ((num2 <= 0) ? 1 : num2);
                        }
                        else if (LEFT(item3, 1) == GClass0.smethod_0("?"))
                        {
                            num2 = method_126(logicExplain.method_14(MID(item3, 1, -9999))) + 1;
                            num2 = ((num2 <= 0) ? 1 : num2);
                        }
                        else if (LEFT(item3, 2) == GClass0.smethod_0(">ļ"))
                        {
                            num3 = method_126(logicExplain.method_14(MID(item3, 2, -9999)));
                            num3 = ((num3 < dictionary2.Count) ? num3 : dictionary2.Count);
                        }
                        else if (LEFT(item3, 1) == GClass0.smethod_0("="))
                        {
                            num3 = method_126(logicExplain.method_14(MID(item3, 1, -9999))) - 1;
                            num3 = ((num3 < dictionary2.Count) ? num3 : dictionary2.Count);
                        }
                        else
                        {
                            List<string> list3 = ParaToList(item3, GClass0.smethod_0("\u007f"), false);
                            if (list3.Count > 1)
                            {
                                num2 = method_126(logicExplain.method_14(list3[0]));
                                num2 = ((num2 <= 0) ? 1 : num2);
                                num3 = method_126(logicExplain.method_14(list3[1]));
                                num3 = ((num3 < dictionary2.Count) ? num3 : dictionary2.Count);
                            }
                            else
                            {
                                List<string> list4 = ParaToList(item3, GClass0.smethod_0("'"), false);
                                if (list4.Count > 1)
                                {
                                    text2 = logicExplain.method_14(list4[0]);
                                    text5 = dictionary2[text2];
                                    bool flag2 = true;
                                    foreach (string item4 in list4)
                                    {
                                        if (!dictionary2.ContainsKey(item4))
                                        {
                                            flag2 = false;
                                            break;
                                        }
                                        if (text5 != dictionary2[item4])
                                        {
                                            flag2 = false;
                                            break;
                                        }
                                    }
                                    if (flag2)
                                    {
                                        text5 = GClass0.smethod_0("-") + text5;
                                        if (text.IndexOf(text5) < 0)
                                        {
                                            text += text5;
                                        }
                                    }
                                    flag = false;
                                }
                                else
                                {
                                    text2 = logicExplain.method_17(item3);
                                    List<string> list5 = ParaToList(text2, ','.ToString(), false);
                                    foreach (string item5 in list5)
                                    {
                                        if (dictionary2.ContainsKey(item5))
                                        {
                                            text5 = GClass0.smethod_0("-") + dictionary2[item5];
                                            if (text.IndexOf(text5) < 0)
                                            {
                                                text += text5;
                                            }
                                        }
                                    }
                                    flag = false;
                                }
                            }
                        }
                        if (flag)
                        {
                            for (int i = num2; i <= num3; i++)
                            {
                                text5 = GClass0.smethod_0("-") + dictionary2[i.ToString()];
                                if (text.IndexOf(text5) < 0)
                                {
                                    text += text5;
                                }
                            }
                        }
                    }
                    if (LEFT(text, 1) == ','.ToString())
                    {
                        text = MID(text, 1, -9999);
                    }
                }
            }
            return text;
        }

        private string method_47(string string_0)
        {
            string text = GClass0.smethod_0("0");
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                if (list.Count() > 2)
                {
                    string text2 = method_6(list[0]);
                    if (!method_117(text2))
                    {
                        return GClass0.smethod_0("0");
                    }
                    string text3 = "";
                    string text4 = "";
                    List<string> list2 = new List<string>(text2.Split('.'));
                    text3 = list2[0];
                    if (list2.Count() > 1)
                    {
                        text4 = list2[1];
                    }
                    int num = 0;
                    text = GClass0.smethod_0("1");
                    int num2 = 0;
                    if (LEFT(list[1], 2) == GClass0.smethod_0("<ļ"))
                    {
                        num2 = method_126(MID(list[1], 2, -9999));
                        text = ((text3.Length < num2 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    else if (LEFT(list[1], 2) == GClass0.smethod_0(">ļ"))
                    {
                        num2 = method_126(MID(list[1], 2, -9999));
                        text = ((text3.Length > num2 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    else if (LEFT(list[1], 1) == GClass0.smethod_0("?"))
                    {
                        num2 = method_126(MID(list[1], 1, -9999));
                        text = ((text3.Length <= num2 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    else if (LEFT(list[1], 1) == GClass0.smethod_0("="))
                    {
                        num2 = method_126(MID(list[1], 1, -9999));
                        text = ((text3.Length >= num2 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    else if (list[1].IndexOf(GClass0.smethod_0("\u007f")) > -1)
                    {
                        num = list[1].IndexOf(GClass0.smethod_0("\u007f"));
                        num2 = method_126(LEFT(list[1], num));
                        text = ((text3.Length < num2 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                        if (text == GClass0.smethod_0("1"))
                        {
                            num2 = method_126(MID(list[1], num + 1, -9999));
                            text = ((text3.Length > num2 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                        }
                    }
                    else
                    {
                        num2 = method_126(list[1]);
                        text = ((text3.Length != num2 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    int num3 = 0;
                    if (LEFT(list[2], 2) == GClass0.smethod_0("<ļ"))
                    {
                        num3 = method_126(MID(list[2], 2, -9999));
                        text = ((text4.Length < num3 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    else if (LEFT(list[2], 2) == GClass0.smethod_0(">ļ"))
                    {
                        num3 = method_126(MID(list[2], 2, -9999));
                        text = ((text4.Length > num3 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    else if (LEFT(list[2], 1) == GClass0.smethod_0("?"))
                    {
                        num3 = method_126(MID(list[2], 1, -9999));
                        text = ((text4.Length <= num3 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    else if (LEFT(list[2], 1) == GClass0.smethod_0("="))
                    {
                        num3 = method_126(MID(list[2], 1, -9999));
                        text = ((text4.Length >= num3 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                    else if (list[2].IndexOf(GClass0.smethod_0("\u007f")) > -1)
                    {
                        num = list[2].IndexOf(GClass0.smethod_0("\u007f"));
                        num3 = method_126(LEFT(list[2], num));
                        text = ((text4.Length < num3 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                        if (text == GClass0.smethod_0("1"))
                        {
                            num3 = method_126(MID(list[2], num + 1, -9999));
                            text = ((text4.Length > num3 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                        }
                    }
                    else
                    {
                        num3 = method_126(list[2]);
                        text = ((text4.Length != num3 || !(text == GClass0.smethod_0("1"))) ? GClass0.smethod_0("0") : GClass0.smethod_0("1"));
                    }
                }
            }
            return text;
        }

        private string method_48(string string_0)
        {
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string a = "";
                string string_ = list[0];
                string_ = method_1(string_, true);
                if (list.Count() > 1)
                {
                    a = list[1];
                }
                if (a == GClass0.smethod_0("0"))
                {
                    string_ = CleanString(string_);
                    string_ = string_.ToUpper();
                }
                if (isEnglishWord(string_))
                {
                    return GClass0.smethod_0("1");
                }
            }
            return GClass0.smethod_0("0");
        }

        private string method_49(string string_0)
        {
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string a = "";
                string string_ = list[0];
                string_ = method_1(string_, true);
                if (list.Count() > 1)
                {
                    a = list[1];
                }
                if (a == GClass0.smethod_0("0"))
                {
                    string_ = CleanString(string_);
                    string_ = string_.ToUpper();
                }
                if (isWord(string_))
                {
                    return GClass0.smethod_0("1");
                }
            }
            return GClass0.smethod_0("0");
        }

        private string method_50(string string_0)
        {
            if (string_0.Length > 0 && method_118(method_1(string_0, true)))
            {
                return GClass0.smethod_0("1");
            }
            return GClass0.smethod_0("0");
        }

        private string method_51(string string_0)
        {
            if (string_0.Length > 0 && method_119(method_1(string_0, true)))
            {
                return GClass0.smethod_0("1");
            }
            return GClass0.smethod_0("0");
        }

        private string method_52(string string_0)
        {
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string a = "";
                string string_ = list[0];
                string_ = method_1(string_, true);
                if (list.Count() > 1)
                {
                    a = list[1];
                }
                if (a == GClass0.smethod_0("0"))
                {
                    string_ = CleanString(string_);
                    string_ = string_.ToUpper();
                }
                if (method_120(string_))
                {
                    return GClass0.smethod_0("1");
                }
            }
            return GClass0.smethod_0("0");
        }

        private string method_53(string string_0)
        {
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string a = "";
                string string_ = list[0];
                string_ = method_1(string_, true);
                if (list.Count() > 1)
                {
                    a = list[1];
                }
                if (a == GClass0.smethod_0("0"))
                {
                    string_ = CleanString(string_);
                    string_ = string_.ToUpper();
                }
                if (method_121(string_))
                {
                    return GClass0.smethod_0("1");
                }
            }
            return GClass0.smethod_0("0");
        }

        private string method_54(string string_0)
        {
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string a = "";
                string string_ = list[0];
                string_ = method_1(string_, true);
                if (list.Count() > 1)
                {
                    a = list[1];
                }
                if (a == GClass0.smethod_0("0"))
                {
                    string_ = CleanString(string_);
                    string_ = string_.ToUpper();
                }
                if (method_122(string_))
                {
                    return GClass0.smethod_0("1");
                }
            }
            return GClass0.smethod_0("0");
        }

        private string method_55(string string_0)
        {
            string text = "";
            OutputResult(GClass0.smethod_0("0ŉɘ\u0337彟縡䠴轭螺嘌\uf518\u0b7a") + string_0 + GClass0.smethod_0("|"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            string text2 = DeleteOuterSymbol(string_0);
            if (text2.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                foreach (string item in list)
                {
                    string text3 = method_6(item);
                    if (text3 != "")
                    {
                        text += text3;
                    }
                }
            }
            OutputResult(GClass0.smethod_0("6ŋɚ\u0339挆篍磗悟\uf718ॺ") + text + GClass0.smethod_0("|"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            OutputResult("", GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            return text;
        }

        private string method_56(string string_0, string string_1)
        {
            string string_2 = string_1 + string_0.Replace(GClass0.smethod_0("!"), "") + GClass0.smethod_0("(");
            return method_96(string_2);
        }

        private string method_57(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0(";"), true);
                string string_ = list[0].Trim();
                string text = (list.Count() > 1) ? list[1].Trim() : "";
                string string_2 = (list.Count() > 2) ? list[2].Trim() : GClass0.smethod_0("\u3000");
                if (text != "")
                {
                    text = method_6(text);
                    OutputResult(GClass0.smethod_0("2ŏɞ\u0335ЮՊ٧ݣ\u0863\u0951\u0a61\u0b7b\u0c76ഩ") + string_0 + GClass0.smethod_0("$僱杻疎䨤玌礑缇蟕犓瓐沞\uf31b") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                    result = method_146(string_, text, string_2);
                }
            }
            return result;
        }

        private string method_58(string string_0)
        {
            string result = GClass0.smethod_0("1");
            if (string_0.Length > 0)
            {
                result = ((!method_89(string_0)) ? method_17(string_0).ToString() : method_1(string_0, false));
                result = result.Length.ToString();
            }
            return result;
        }

        private string method_59(string string_0)
        {
            string text = method_1(string_0, true);
            if (!method_117(text))
            {
                text = GClass0.smethod_0("1");
            }
            return text;
        }

        private string method_60(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string string_2 = (list.Count() > 1) ? list[1] : GClass0.smethod_0("0");
                string_ = method_6(string_);
                int num = Convert.ToInt32(method_12(string_2, GClass0.smethod_0("*"), 0.0));
                if (num <= 0)
                {
                    num = 1;
                }
                result = LEFT(string_, num);
            }
            return result;
        }

        private string method_61(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string string_2 = (list.Count() > 1) ? list[1] : "";
                string string_3 = (list.Count() > 2) ? list[2] : "";
                string_ = method_6(string_);
                int num = Convert.ToInt32(method_12(string_2, GClass0.smethod_0("*"), 0.0)) - 1;
                if (num < 0)
                {
                    num = 0;
                }
                int num2 = Convert.ToInt32(method_12(string_3, GClass0.smethod_0("*"), 0.0));
                if (num2 <= 0)
                {
                    num2 = -9999;
                }
                result = MID(string_, num, num2);
            }
            return result;
        }

        private string method_62(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string string_2 = (list.Count() > 1) ? list[1] : "";
                string string_3 = (list.Count() > 2) ? list[2] : "";
                string_ = method_6(string_);
                int num = Convert.ToInt32(method_12(string_2, GClass0.smethod_0("*"), 0.0)) - 1;
                if (num < 0)
                {
                    num = 0;
                }
                int num2 = Convert.ToInt32(method_12(string_3, GClass0.smethod_0("*"), 0.0)) - 1;
                if (num2 <= 0)
                {
                    num2 = -9999;
                }
                result = method_132(string_, num, num2);
            }
            return result;
        }

        private string method_63(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string string_2 = (list.Count() > 1) ? list[1] : GClass0.smethod_0("0");
                string_ = method_6(string_);
                int num = Convert.ToInt32(method_12(string_2, GClass0.smethod_0("*"), 0.0));
                if (num <= 0)
                {
                    num = 1;
                }
                result = method_133(string_, num);
            }
            return result;
        }

        private string method_64(string string_0, string string_1 = "")
        {
            string result = "";
            if (string_0.Length > 0)
            {
                ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text = method_6(string_0);
                result = ((string_1 == GClass0.smethod_0("M")) ? text.TrimStart() : ((!(string_1 == GClass0.smethod_0("S"))) ? text.Trim() : text.TrimEnd()));
            }
            return result;
        }

        private string method_65(string string_0)
        {
            string result = GClass0.smethod_0("1");
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string text = (list.Count() > 1) ? list[1] : "";
                string text2 = (list.Count() > 2) ? list[2].Trim().ToUpper() : "";
                string text3 = (list.Count() > 3) ? list[3].Trim().ToUpper() : "";
                List<string> list2 = ParaToList(string_, GClass0.smethod_0("."), true);
                string text4 = list2[0];
                string text5 = (list2.Count() > 1) ? list2[1] : "";
                string text6 = (list2.Count() > 2) ? list2[2] : "";
                string text7 = "";
                string text8 = "";
                string text9 = "";
                string text10 = "";
                if (text4 != "" && text5 != "" && text6 != "" && list2.Count() == 3)
                {
                    text8 = method_113(method_6(text4));
                    text9 = method_135(method_6(text5), GClass0.smethod_0("1"), 2, true);
                    text10 = method_135(method_6(text6), GClass0.smethod_0("1"), 2, true);
                }
                else if (text4 != "" && list2.Count() == 1)
                {
                    text7 = method_6(text4);
                    text8 = method_105(text7, 4);
                    text9 = method_106(text7, 2, false);
                    text10 = method_107(text7, 2);
                }
                else if (text4 != "" && text5 == "" && text6 != "" && list2.Count() == 3)
                {
                    text7 = method_6(text4);
                    text8 = method_105(text7, 4);
                    text9 = method_106(text7, 2, false);
                    text10 = method_135(method_6(text6), GClass0.smethod_0("1"), 2, true);
                }
                else if (text4 != "" && text5 == "" && list2.Count() == 2)
                {
                    text7 = method_6(text4);
                    text8 = method_105(text7, 4);
                    text9 = method_106(text7, 2, false);
                    text10 = GClass0.smethod_0("2ı");
                }
                else if (text4 != "" && text5 != "" && text6 == "" && list2.Count() == 3)
                {
                    text8 = method_113(method_6(text4));
                    text9 = method_135(method_6(text5), GClass0.smethod_0("1"), 2, true);
                    text10 = GClass0.smethod_0("2ı");
                }
                else if (text4 != "" && text5 != "" && list2.Count() == 2)
                {
                    text7 = method_6(text5);
                    text8 = method_113(method_6(text4));
                    text9 = method_106(text7, 2, true);
                    text10 = method_107(text7, 2);
                }
                else if (text4 == "" && text5 != "" && list2.Count() == 2)
                {
                    text7 = method_6(text5);
                    text8 = "";
                    text9 = method_106(text7, 2, true);
                    text10 = method_107(text7, 2);
                }
                else if (text4 == "" && text5 != "" && text6 != "" && list2.Count() == 3)
                {
                    text8 = "";
                    text9 = method_135(method_6(text5), GClass0.smethod_0("1"), 2, true);
                    text10 = method_135(method_6(text6), GClass0.smethod_0("1"), 2, true);
                }
                text7 = "";
                if (text == GClass0.smethod_0("0"))
                {
                    if (text2.Contains(GClass0.smethod_0("X")))
                    {
                        text7 = text7 + text8 + GClass0.smethod_0("幵");
                    }
                    if (text2.Contains(GClass0.smethod_0("L")))
                    {
                        text7 = text7 + method_136(text9) + GClass0.smethod_0("有");
                    }
                    if (text2.Contains(GClass0.smethod_0("E")))
                    {
                        text7 = text7 + method_136(text10) + GClass0.smethod_0("旤");
                    }
                    if ((text7 == "") & !text2.Contains(GClass0.smethod_0("V")))
                    {
                        text7 = text8 + GClass0.smethod_0("幵") + method_136(text9) + GClass0.smethod_0("有") + method_136(text10) + GClass0.smethod_0("旤");
                    }
                    if (text3 == "")
                    {
                        text3 = GClass0.smethod_0("Aĳ");
                    }
                    if (text2.Contains(GClass0.smethod_0("V")))
                    {
                        text7 += method_79(text8 + text9 + text10 + GClass0.smethod_0("-") + text3);
                    }
                }
                else
                {
                    if (text2.Contains(GClass0.smethod_0("X")))
                    {
                        text7 = text7 + text8 + text;
                    }
                    if (text2.Contains(GClass0.smethod_0("L")))
                    {
                        text7 = text7 + text9 + text;
                    }
                    if (text2.Contains(GClass0.smethod_0("E")))
                    {
                        text7 = text7 + text10 + text;
                    }
                    text7 = LEFT(text7, text7.Length - 1);
                    if ((text7 == "") & !text2.Contains(GClass0.smethod_0("V")))
                    {
                        text7 = text8 + text + text9 + text + text10;
                    }
                    if (text3 == "")
                    {
                        text3 = GClass0.smethod_0("LĶ");
                    }
                    if (text2.Contains(GClass0.smethod_0("V")))
                    {
                        text7 += method_79(text8 + text9 + text10 + GClass0.smethod_0("-") + text3);
                    }
                }
                result = text7;
            }
            return result;
        }

        private string method_66(string string_0)
        {
            string result = GClass0.smethod_0("1");
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text = (list.Count() > 1) ? list[1].Trim() : "";
                string text2 = (list.Count() > 2) ? list[2].Trim().ToUpper() : "";
                bool flag = list.Count() > 3 && method_8(list[3], GClass0.smethod_0("}"), false);
                string a = (list.Count() > 4) ? list[4].Trim().ToUpper() : "";
                List<string> list2 = ParaToList(list[0], GClass0.smethod_0(";"), false);
                string text3 = list2[0];
                string text4 = (list2.Count() > 1) ? list2[1] : "";
                string text5 = (list2.Count() > 2) ? list2[2] : "";
                string text6 = "";
                string text7 = "";
                string text8 = "";
                string text9 = "";
                if (text3 != "" && text4 != "" && text5 != "" && list2.Count() == 3)
                {
                    text7 = method_135(method_6(text3), GClass0.smethod_0("1"), 2, true);
                    text8 = method_135(method_6(text4), GClass0.smethod_0("1"), 2, true);
                    text9 = method_135(method_6(text5), GClass0.smethod_0("1"), 2, true);
                }
                else if (text3 != "" && list2.Count() == 1)
                {
                    text6 = method_6(text3);
                    text7 = method_108(text6, 2);
                    text8 = method_109(text6, 2, false);
                    text9 = method_110(text6, 2);
                }
                else if (text3 != "" && text4 == "" && text5 != "" && list2.Count() == 3)
                {
                    text6 = method_6(text3);
                    text7 = method_108(text6, 2);
                    text8 = method_109(text6, 2, false);
                    text9 = method_135(method_6(text5), GClass0.smethod_0("1"), 2, true);
                }
                else if (text3 != "" && text4 == "" && list2.Count() == 2)
                {
                    text6 = method_6(text3);
                    text7 = method_108(text6, 2);
                    text8 = method_109(text6, 2, false);
                    text9 = GClass0.smethod_0("2ı");
                }
                else if (text3 != "" && text4 != "" && text5 == "" && list2.Count() == 3)
                {
                    text7 = method_135(method_6(text3), GClass0.smethod_0("1"), 2, true);
                    text8 = method_135(method_6(text4), GClass0.smethod_0("1"), 2, true);
                    text9 = GClass0.smethod_0("2ı");
                }
                else if (text3 != "" && text4 != "" && list2.Count() == 2)
                {
                    text6 = method_6(text4);
                    text7 = method_135(method_6(text3), GClass0.smethod_0("1"), 2, true);
                    text8 = method_109(text6, 2, true);
                    text9 = method_110(text6, 2);
                }
                else if (text3 == "" && text4 != "" && list2.Count() == 2)
                {
                    text6 = method_6(text4);
                    text7 = GClass0.smethod_0("2ı");
                    text8 = method_109(text6, 2, true);
                    text9 = method_110(text6, 2);
                }
                else if (text3 == "" && text4 != "" && text5 != "" && list2.Count() == 3)
                {
                    text7 = GClass0.smethod_0("2ı");
                    text8 = method_135(method_6(text4), GClass0.smethod_0("1"), 2, true);
                    text9 = method_135(method_6(text5), GClass0.smethod_0("1"), 2, true);
                }
                int num = method_126(text7);
                if (flag && num > 0 && num < 12)
                {
                    num += 12;
                    text7 = num.ToString();
                }
                text6 = "";
                bool flag2 = true;
                if (a != "" && num > 12)
                {
                    flag2 = false;
                    text7 = method_135((num - 12).ToString(), GClass0.smethod_0("1"), 2, true);
                }
                if (text == "")
                {
                    text6 = GClass0.smethod_0("2į") + text7 + text8 + text9;
                }
                else if (text == GClass0.smethod_0("0"))
                {
                    if (text2.Contains(GClass0.smethod_0("I")))
                    {
                        text6 = text6 + method_136(text7) + GClass0.smethod_0("旷");
                    }
                    if (text2.Contains(GClass0.smethod_0("L")))
                    {
                        text6 = text6 + method_136(text8) + GClass0.smethod_0("切");
                    }
                    if (text2.Contains(GClass0.smethod_0("R")))
                    {
                        text6 = text6 + method_136(text9) + GClass0.smethod_0("秓");
                    }
                    if (text6 == "")
                    {
                        text6 = method_136(text7) + GClass0.smethod_0("旷") + method_136(text8) + GClass0.smethod_0("切") + method_136(text9) + GClass0.smethod_0("秓");
                    }
                }
                else if (text == GClass0.smethod_0("1"))
                {
                    if (text2.Contains(GClass0.smethod_0("I")))
                    {
                        text6 += text7;
                    }
                    if (text2.Contains(GClass0.smethod_0("L")))
                    {
                        text6 += text8;
                    }
                    if (text2.Contains(GClass0.smethod_0("R")))
                    {
                        text6 += text9;
                    }
                    if (text6 == "")
                    {
                        text6 = text7 + text8 + text9;
                    }
                }
                else
                {
                    if (text2.Contains(GClass0.smethod_0("I")))
                    {
                        text6 = text6 + text7 + text;
                    }
                    if (text2.Contains(GClass0.smethod_0("L")))
                    {
                        text6 = text6 + text8 + text;
                    }
                    if (text2.Contains(GClass0.smethod_0("R")))
                    {
                        text6 = text6 + text9 + text;
                    }
                    text6 = LEFT(text6, text6.Length - 1);
                    if (text6 == "")
                    {
                        text6 = text7 + text + text8 + text + text9;
                    }
                }
                if (a != "")
                {
                    if (a == GClass0.smethod_0("B"))
                    {
                        text6 = (flag2 ? (GClass0.smethod_0("丈剉") + text6) : (GClass0.smethod_0("三剉") + text6));
                    }
                    else if (a == GClass0.smethod_0("D"))
                    {
                        text6 = (flag2 ? (text6 + GClass0.smethod_0("CŌ")) : (text6 + GClass0.smethod_0("RŌ")));
                    }
                }
                result = text6;
            }
            return result;
        }

        private string method_67(string string_0)
        {
            string text = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text2 = list[0];
                string text3 = (list.Count() > 1) ? list[1] : "";
                string str = (list.Count() > 2) ? list[2] : "";
                text = ((!(text2.ToUpper() == GClass0.smethod_0(",œɉ\u0341х՚تܨ"))) ? method_65(text2) : method_68(""));
                if (text3 != "")
                {
                    text = ((!(LEFT(text3, 5).ToUpper() == GClass0.smethod_0("!ŊɌ\u0355Щ"))) ? (text + MID(method_66(text3 + GClass0.smethod_0("/Įȭ") + str), 1, -9999)) : (text + MID(method_69(""), 1, -9999)));
                }
            }
            return text;
        }

        private string method_68(string string_0 = "")
        {
            string text = "";
            List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
            string text2 = (list.Count() > 0) ? list[0] : "";
            string text3 = (list.Count() > 1) ? list[1].Trim().ToUpper() : "";
            string text4 = (list.Count() > 2) ? list[2].Trim().ToUpper() : "";
            if (text2 == GClass0.smethod_0("0"))
            {
                if (text3.Contains(GClass0.smethod_0("X")))
                {
                    text += GClass0.smethod_0("|Žɺͻ婵");
                }
                if (text3.Contains(GClass0.smethod_0("L")))
                {
                    text += GClass0.smethod_0("O昉");
                }
                if (text3.Contains(GClass0.smethod_0("E")))
                {
                    text += GClass0.smethod_0("f擤");
                }
                if ((text == "") & !text3.Contains(GClass0.smethod_0("V")))
                {
                    text = GClass0.smethod_0("pűɾͿ婱Չ愋ݦ淤");
                }
                if (text != "")
                {
                    text = DateTime.Today.ToString(text);
                }
                if (text4 == "")
                {
                    text4 = GClass0.smethod_0("Aĳ");
                }
                if (text3.Contains(GClass0.smethod_0("V")))
                {
                    text += method_79(DateTime.Today.ToString(GClass0.smethod_0("qžɿͼщՎ٦ݥ")) + GClass0.smethod_0("-") + text4);
                }
            }
            else
            {
                if (text3.Contains(GClass0.smethod_0("X")))
                {
                    text = text + GClass0.smethod_0("}źɻ\u0378") + text2;
                }
                if (text3.Contains(GClass0.smethod_0("L")))
                {
                    text = text + GClass0.smethod_0("L") + text2;
                }
                if (text3.Contains(GClass0.smethod_0("E")))
                {
                    text = text + GClass0.smethod_0("e") + text2;
                }
                text = LEFT(text, text.Length - 1);
                if ((text == "") & !text3.Contains(GClass0.smethod_0("V")))
                {
                    text = GClass0.smethod_0("}źɻ\u0378") + text2 + GClass0.smethod_0("OŌ") + text2 + GClass0.smethod_0("fť");
                }
                if (text != "")
                {
                    text = DateTime.Today.ToString(text);
                }
                if (text4 == "")
                {
                    text4 = GClass0.smethod_0("LĶ");
                }
                if (text3.Contains(GClass0.smethod_0("V")))
                {
                    text += method_79(DateTime.Today.ToString(GClass0.smethod_0("qžɿͼщՎ٦ݥ")) + GClass0.smethod_0("-") + text4);
                }
            }
            return text;
        }

        private string method_69(string string_0 = "")
        {
            string text = "";
            string text2 = "";
            string a = "";
            string text3 = "";
            if (string_0 != "")
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                text = ((list.Count() > 0) ? list[0].Trim() : "");
                text2 = ((list.Count() > 1) ? list[1].Trim().ToUpper() : "");
                a = ((list.Count() > 2) ? list[2].Trim().ToUpper() : "");
            }
            string text4 = (a == "") ? GClass0.smethod_0("I") : GClass0.smethod_0("i");
            if (!(text == ""))
            {
                if (text == GClass0.smethod_0("0"))
                {
                    if (text2.Contains(GClass0.smethod_0("I")))
                    {
                        text3 = text3 + text4 + GClass0.smethod_0("旷");
                    }
                    if (text2.Contains(GClass0.smethod_0("L")))
                    {
                        text3 += GClass0.smethod_0("o匇");
                    }
                    if (text2.Contains(GClass0.smethod_0("R")))
                    {
                        text3 += GClass0.smethod_0("q磓");
                    }
                    if (text3 == "")
                    {
                        text3 = text4 + GClass0.smethod_0("旳ũ倅ͱ緓");
                    }
                    text3 = DateTime.Now.ToString(text3);
                }
                else if (text == GClass0.smethod_0("1"))
                {
                    if (text2.Contains(GClass0.smethod_0("I")))
                    {
                        text3 = text3 + text4 + text4;
                    }
                    if (text2.Contains(GClass0.smethod_0("L")))
                    {
                        text3 += GClass0.smethod_0("oŬ");
                    }
                    if (text2.Contains(GClass0.smethod_0("R")))
                    {
                        text3 += GClass0.smethod_0("qŲ");
                    }
                    if (text3 == "")
                    {
                        text3 = text4 + text4 + GClass0.smethod_0("iŮɱͲ");
                    }
                    text3 = DateTime.Now.ToString(text3);
                }
                else
                {
                    if (text2.Contains(GClass0.smethod_0("I")))
                    {
                        text3 = text3 + text4 + text4 + text;
                    }
                    if (text2.Contains(GClass0.smethod_0("L")))
                    {
                        text3 = text3 + GClass0.smethod_0("oŬ") + text;
                    }
                    if (text2.Contains(GClass0.smethod_0("R")))
                    {
                        text3 = text3 + GClass0.smethod_0("qŲ") + text;
                    }
                    text3 = LEFT(text3, text3.Length - 1);
                    if (text3 == "")
                    {
                        text3 = text4 + text4 + text + GClass0.smethod_0("oŬ") + text + GClass0.smethod_0("qŲ");
                    }
                    text3 = DateTime.Now.ToString(text3);
                }
                if (a != "")
                {
                    bool flag = (DateTime.Now.ToString(GClass0.smethod_0("Jŉ")) == DateTime.Now.ToString(GClass0.smethod_0("jũ"))) ? true : false;
                    if (a == GClass0.smethod_0("B"))
                    {
                        text3 = (flag ? (GClass0.smethod_0("丈剉") + text3) : (GClass0.smethod_0("三剉") + text3));
                    }
                    else if (a == GClass0.smethod_0("D"))
                    {
                        text3 = (flag ? (text3 + GClass0.smethod_0("CŌ")) : (text3 + GClass0.smethod_0("RŌ")));
                    }
                }
                return text3;
            }
            return GClass0.smethod_0("2į") + DateTime.Now.ToString(text4 + text4 + GClass0.smethod_0("iŮɱͲ"));
        }

        private string method_70(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string a = (list.Count() > 1) ? list[1] : "";
                result = method_6(string_);
                result = ((a == GClass0.smethod_0("3")) ? method_105(result, 2) : ((!(a == GClass0.smethod_0("1"))) ? method_105(result, 4) : method_105(result, 0)));
            }
            return result;
        }

        private string method_71(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string a = (list.Count() > 1) ? list[1] : "";
                result = method_6(string_);
                result = ((!(a == GClass0.smethod_0("3"))) ? method_106(result, 1, false) : method_106(result, 2, false));
            }
            return result;
        }

        private string method_72(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string a = (list.Count() > 1) ? list[1] : "";
                result = method_6(string_);
                result = ((!(a == GClass0.smethod_0("3"))) ? method_107(result, 1) : method_107(result, 2));
            }
            return result;
        }

        private string method_73(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string a = (list.Count() > 1) ? list[1] : "";
                result = method_6(string_);
                result = ((!(a == GClass0.smethod_0("3"))) ? method_108(result, 1) : method_108(result, 2));
            }
            return result;
        }

        private string method_74(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string a = (list.Count() > 1) ? list[1] : "";
                result = method_6(string_);
                result = ((!(a == GClass0.smethod_0("3"))) ? method_109(result, 1, false) : method_109(result, 2, false));
            }
            return result;
        }

        private string method_75(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_ = list[0];
                string a = (list.Count() > 1) ? list[1] : "";
                result = method_6(string_);
                result = ((!(a == GClass0.smethod_0("3"))) ? method_110(result, 1) : method_110(result, 2));
            }
            return result;
        }

        private string method_76(string string_0)
        {
            string result = GClass0.smethod_0("0");
            if (string_0.Length > 0)
            {
                string text = method_6(string_0);
                if (text.IndexOf(GClass0.smethod_0(".")) > -1)
                {
                    int num = text.IndexOf(GClass0.smethod_0("!"));
                    if (num > -1)
                    {
                        text = LEFT(text, num).Trim();
                    }
                }
                else
                {
                    if (text.IndexOf(GClass0.smethod_0(";")) > -1)
                    {
                        OutputResult(GClass0.smethod_0("凨摤ȷ\u035cѾդ\u064bݯ\u0879३ਣଣ忋桸腫汤䇿枀矦琝\ueb1b") + string_0 + GClass0.smethod_0("%Ĺȣ\u032dЮ"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                        return GClass0.smethod_0("0");
                    }
                    text = LEFT(text, 8);
                    int num2 = text.IndexOf(GClass0.smethod_0("/"));
                    if (num2 > -1)
                    {
                        text = LEFT(text, num2);
                    }
                    string text2 = LEFT(text, text.Length - 4);
                    string text3 = method_133(LEFT(text, text.Length - 2), 2);
                    string text4 = method_133(text, 2);
                    text = text2 + GClass0.smethod_0(".") + text3 + GClass0.smethod_0(".") + text4;
                }
                OutputResult(GClass0.smethod_0("凨摤ȷ\u035cѾդ\u064bݯ\u0879३ਣଣ忋桸腫汤䇿枀矦琝\ueb1b") + string_0 + GClass0.smethod_0("#Ŀȡ") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                if (method_138(text))
                {
                    result = GClass0.smethod_0("1");
                }
            }
            return result;
        }

        private string method_77(string string_0)
        {
            string result = GClass0.smethod_0("0");
            if (string_0.Length > 0)
            {
                string text = method_6(string_0);
                if (text.IndexOf(GClass0.smethod_0(";")) > -1)
                {
                    int num = text.IndexOf(GClass0.smethod_0("!"));
                    if (num > -1)
                    {
                        text = MID(text, num + 1, -9999).Trim();
                    }
                }
                else
                {
                    text = method_133(text, 7);
                    int num2 = text.IndexOf(GClass0.smethod_0("/"));
                    if (num2 <= -1)
                    {
                        OutputResult(GClass0.smethod_0("凨摤ȷ\u035cѾդ\u065bݧ\u0860३ਣଣ忋桸腫汤䇿枀矵蛶\ueb1b") + string_0 + GClass0.smethod_0("%Ĺȣ\u0338л"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                        return GClass0.smethod_0("0");
                    }
                    text = MID(text, num2 + 1, -9999);
                    string text2 = LEFT(text, 2);
                    string text3 = MID(text, 2, 2);
                    string text4 = MID(text, 4, 2);
                    text = text2 + GClass0.smethod_0(";") + text3 + GClass0.smethod_0(";") + text4;
                }
                OutputResult(GClass0.smethod_0("凨摤ȷ\u035cѾդ\u065bݧ\u0860३ਣଣ忋桸腫汤䇿枀矵蛶\ueb1b") + string_0 + GClass0.smethod_0("#Ŀȡ") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                if (method_138(text))
                {
                    result = GClass0.smethod_0("1");
                }
            }
            return result;
        }

        private string method_78(string string_0)
        {
            string result = GClass0.smethod_0("0");
            if (string_0.Length > 0)
            {
                string text = method_112(string_0);
                OutputResult(GClass0.smethod_0("処摪Ƚ\u0356Ѹբ\u0651ݵ\u0867ॷ\u0a45\u0b79\u0c62൫ล༥䏉瑺鵥灪䗽掂珠瀛緵賶\ue51b") + string_0 + GClass0.smethod_0("#Ŀȡ") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                if (method_138(text))
                {
                    result = GClass0.smethod_0("1");
                }
            }
            return result;
        }

        private string method_79(string string_0)
        {
            string text = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text2 = list[0];
                string a = (list.Count() > 1) ? list[1].Trim().ToUpper() : GClass0.smethod_0("LĶ");
                string text3 = method_6(text2);
                string text4 = "";
                string text5 = "";
                string text6 = "";
                if (text3.IndexOf(GClass0.smethod_0(".")) > -1)
                {
                    text4 = method_105(text3, 4);
                    text5 = method_106(text3, 2, false);
                    text6 = method_107(text3, 2);
                }
                else
                {
                    text3 = LEFT(text3, 8);
                    int num = text3.IndexOf(GClass0.smethod_0("/"));
                    if (num > -1)
                    {
                        text3 = LEFT(text3, num);
                    }
                    text4 = LEFT(text3, text3.Length - 4);
                    text5 = method_133(LEFT(text3, text3.Length - 2), 2);
                    text6 = method_133(text3, 2);
                }
                text3 = text4 + GClass0.smethod_0(".") + text5 + GClass0.smethod_0(".") + text6;
                int num2 = Convert.ToInt16(Convert.ToDateTime(text3).DayOfWeek);
                text = ((a == GClass0.smethod_0("LĶ")) ? ((num2 == 0) ? GClass0.smethod_0("6") : num2.ToString()) : ((a == GClass0.smethod_0("Lı")) ? num2.ToString() : ((a == GClass0.smethod_0("Aİ")) ? method_111(num2) : ((a == GClass0.smethod_0("Aĳ")) ? (GClass0.smethod_0("呩") + method_111(num2)) : ((!(a == GClass0.smethod_0("AĲ"))) ? ((num2 == 0) ? GClass0.smethod_0("6") : num2.ToString()) : (GClass0.smethod_0("昝昞") + method_111(num2)))))));
                OutputResult(GClass0.smethod_0("凨摤ȷ\u0345Ѵյ٤\u074a\u086cॵਣଣ忋桸腫汤䇿枀琜琝\ueb1b") + text2 + GClass0.smethod_0("#Ŀȡ") + text3 + GClass0.smethod_0("z") + text + GClass0.smethod_0("|"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            }
            return text;
        }

        private string method_80(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text = method_112(method_6(list[0]));
                string string_ = (list.Count() > 1) ? method_6(list[1]) : "";
                string text2 = (list.Count() > 2) ? list[2].ToUpper().Trim() : GClass0.smethod_0("Fŕ");
                OutputResult(GClass0.smethod_0("凮摢ȵ\u0355сՊإܥ寉決蕥桪巽箂儅囏痵蓶\ued1b") + list[0] + GClass0.smethod_0("#Ŀȡ") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                if (method_138(text))
                {
                    DateTime dateTime = Convert.ToDateTime(text);
                    string text3 = method_105(string_, 0);
                    string text4 = method_106(string_, 1, false);
                    string text5 = method_107(string_, 1);
                    string text6 = method_108(string_, 1);
                    string text7 = method_109(string_, 1, false);
                    string text8 = method_110(string_, 1);
                    int value = method_126(text3);
                    int months = method_126(text4);
                    int num = method_126(text5);
                    int num2 = method_126(text6);
                    int num3 = method_126(text7);
                    int num4 = method_126(text8);
                    string str = text3 + GClass0.smethod_0(".") + text4 + GClass0.smethod_0(".") + text5 + GClass0.smethod_0("!") + text6 + GClass0.smethod_0(";") + text7 + GClass0.smethod_0(";") + text8;
                    OutputResult(GClass0.smethod_0("凬摠ȫ\u034bуՈأܣ寋汸蕫桤巿简毵驽\uef1b") + list[1] + GClass0.smethod_0("#Ŀȡ") + str, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                    DateTime dateTime2 = dateTime.AddYears(value).AddMonths(months).AddDays((double)num)
                        .AddHours((double)num2)
                        .AddMinutes((double)num3)
                        .AddSeconds((double)num4);
                    result = (text2.Contains(GClass0.smethod_0("D")) ? ((text2.Contains(GClass0.smethod_0("E")) && !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("sŰɱ;ЩՈىܬ\u0866॥")) : ((text2.Contains(GClass0.smethod_0("E")) || !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("jūɨ\u0369РՃ\u0640ܣ\u086f८\u0a29\u0b40\u0c4f\u0d3c\u0e68ཀྵ\u1039ᅱቲ")) : dateTime2.ToString(GClass0.smethod_0("@ŏȼ\u0368ѩԹٱݲ")))) : (text2.Contains(GClass0.smethod_0("B")) ? ((text2.Contains(GClass0.smethod_0("E")) && !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("pűɾͿ婱Չ愋ݦ淤")) : ((text2.Contains(GClass0.smethod_0("E")) || !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("iŶɷ\u0374婸Ն愂ݭ淭\u094f濰୨౩弅\u0e71盓")) : dateTime2.ToString(GClass0.smethod_0("N擳ɩ儅ѱ糓")))) : ((text2.Contains(GClass0.smethod_0("E")) && !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("qžɿͼщՎ٦ݥ")) : ((text2.Contains(GClass0.smethod_0("E")) || !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("vŷɴ\u0375цՇ٭ݬ\u0829\u094e\u0a4d୩౮൱\u0e72")) : (GClass0.smethod_0("2į") + dateTime2.ToString(GClass0.smethod_0("Nōɩ\u036eѱղ")))))));
                }
            }
            return result;
        }

        private string method_81(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text = method_112(method_6(list[0]));
                string string_ = (list.Count() > 1) ? method_6(list[1]) : "";
                string text2 = (list.Count() > 2) ? list[2].ToUpper().Trim() : GClass0.smethod_0("Fŕ");
                OutputResult(GClass0.smethod_0("凨摤ȷ\u0341хՑ\u065dݚ\u0825थ姉湺荥湪忽禂滖癛矵蛶\ueb1b") + list[0] + GClass0.smethod_0("#Ŀȡ") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                if (method_138(text))
                {
                    DateTime dateTime = Convert.ToDateTime(text);
                    string text3 = method_105(string_, 0);
                    string text4 = method_106(string_, 1, false);
                    string text5 = method_107(string_, 1);
                    string text6 = method_108(string_, 1);
                    string text7 = method_109(string_, 1, false);
                    string text8 = method_110(string_, 1);
                    int num = method_126(text3);
                    int num2 = method_126(text4);
                    int num3 = method_126(text5);
                    int num4 = method_126(text6);
                    int num5 = method_126(text7);
                    int num6 = method_126(text8);
                    string str = text3 + GClass0.smethod_0(".") + text4 + GClass0.smethod_0(".") + text5 + GClass0.smethod_0("!") + text6 + GClass0.smethod_0(";") + text7 + GClass0.smethod_0(";") + text8;
                    OutputResult(GClass0.smethod_0("凮摢ȵ\u0343ћՏ\u065fݘ\u0823ण始湸荫湤忿禀痵葽\ued1b") + list[1] + GClass0.smethod_0("#Ŀȡ") + str, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                    DateTime dateTime2 = dateTime.AddYears(-num).AddMonths(-num2).AddDays((double)(-num3))
                        .AddHours((double)(-num4))
                        .AddMinutes((double)(-num5))
                        .AddSeconds((double)(-num6));
                    result = (text2.Contains(GClass0.smethod_0("D")) ? ((text2.Contains(GClass0.smethod_0("E")) && !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("sŰɱ;ЩՈىܬ\u0866॥")) : ((text2.Contains(GClass0.smethod_0("E")) || !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("jūɨ\u0369РՃ\u0640ܣ\u086f८\u0a29\u0b40\u0c4f\u0d3c\u0e68ཀྵ\u1039ᅱቲ")) : dateTime2.ToString(GClass0.smethod_0("@ŏȼ\u0368ѩԹٱݲ")))) : (text2.Contains(GClass0.smethod_0("B")) ? ((text2.Contains(GClass0.smethod_0("E")) && !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("pűɾͿ婱Չ愋ݦ淤")) : ((text2.Contains(GClass0.smethod_0("E")) || !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("iŶɷ\u0374婸Ն愂ݭ淭\u094f濰୨౩弅\u0e71盓")) : dateTime2.ToString(GClass0.smethod_0("N擳ɩ儅ѱ糓")))) : ((text2.Contains(GClass0.smethod_0("E")) && !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("qžɿͼщՎ٦ݥ")) : ((text2.Contains(GClass0.smethod_0("E")) || !text2.Contains(GClass0.smethod_0("U"))) ? dateTime2.ToString(GClass0.smethod_0("vŷɴ\u0375цՇ٭ݬ\u0829\u094e\u0a4d୩౮൱\u0e72")) : (GClass0.smethod_0("2į") + dateTime2.ToString(GClass0.smethod_0("Nōɩ\u036eѱղ")))))));
                }
            }
            return result;
        }

        private string method_82(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string text = method_112(method_6(list[0]));
                string text2 = (list.Count() > 1) ? method_112(method_6(list[1])) : "";
                string string_ = (list.Count() > 2) ? list[2].Trim() : GClass0.smethod_0("B");
                string string_2 = (list.Count() > 3) ? list[3].Trim() : "";
                OutputResult(GClass0.smethod_0("凩摣ȶ\u035dџՁىܥ\u0825嫉潺葥潪峽碂倅䧏瓵蟶\uec1b") + list[0] + GClass0.smethod_0("#Ŀȡ") + text, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                OutputResult(GClass0.smethod_0("凩摣ȶ\u035dџՁىܥ\u0825嫉潺葥潪峽碂燖睛瓵蟶\uec1b") + list[1] + GClass0.smethod_0("#Ŀȡ") + text2, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                if (method_138(text) && method_138(text2))
                {
                    DateTime dateTime = Convert.ToDateTime(text);
                    DateTime dateTime2 = Convert.ToDateTime(text2);
                    TimeSpan timeSpan_ = (dateTime2 > dateTime) ? (dateTime2 - dateTime) : (dateTime - dateTime2);
                    OutputResult(GClass0.smethod_0("凯摡ȴ\u0343сՃ\u064bܣ\u0823芨熟嫽窂棳魻༣ဿᄡ") + timeSpan_.ToString(), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                    result = method_140(timeSpan_, string_, string_2);
                }
            }
            return result;
        }

        private string method_83(string string_0, string string_1 = "EQUAL")
        {
            string result = GClass0.smethod_0("1");
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), true);
                string string_2 = method_6(list[0]);
                string string_3 = (list.Count() > 1) ? list[1] : "";
                if (method_25(string_2, string_3, string_1))
                {
                    return GClass0.smethod_0("0");
                }
            }
            return result;
        }

        private string method_84(string string_0)
        {
            string string_ = GClass0.smethod_0(".Łɉ\u0351уՃ\u064dݏࡇऩ") + string_0 + GClass0.smethod_0("(");
            return method_96(string_);
        }

        private string method_85(string string_0)
        {
            string string_ = GClass0.smethod_0(",ŉɉ\u0343эՏهܩ") + string_0 + GClass0.smethod_0("(");
            return method_96(string_);
        }

        private string method_86()
        {
            return method_96(GClass0.smethod_0("-ņɈ\u0352їՁ\u0640ܪ\u0828"));
        }

        private List<string> method_87(string string_0, int int_0 = 0)
        {
            List<string> list = new List<string>();
            string[] array = new string[8]
            {
                GClass0.smethod_0("!Ňɍ\u034eЩ"),
                GClass0.smethod_0("'Ŏȩ"),
                GClass0.smethod_0("\"ŉɋ\u034cђԩ"),
                GClass0.smethod_0("!ŗɖ\u034fЩ"),
                GClass0.smethod_0("!ňɌ\u0350Щ"),
                GClass0.smethod_0("!ŇɌ\u0350Щ"),
                GClass0.smethod_0("!ŐɌ\u0352Щ"),
                GClass0.smethod_0("\"ŉɅ\u0350іԩ")
            };
            string text = "";
            string text2 = (int_0 == 1) ? GetTextQuestion(string_0, "") : string_0;
            int num = 0;
            int num2 = 0;
            string text3 = "";
            string text4 = "";
            string text5 = "";
            do
            {
                bool flag = true;
                text5 = "";
                string[] array2 = array;
                foreach (string text6 in array2)
                {
                    if (text2.Length - num > text6.Length)
                    {
                        text5 = MID(text2, num, text6.Length).ToUpper();
                    }
                    if (text6 == text5)
                    {
                        num = RightBrackets(text2, num, GClass0.smethod_0(")"), GClass0.smethod_0("("));
                        flag = false;
                        break;
                    }
                }
                if (!flag)
                {
                    goto IL_04ca;
                }
                text3 = MID(text2, num, 2);
                if (text3 == GClass0.smethod_0("Yģ"))
                {
                    num = text2.IndexOf(GClass0.smethod_0(" Ŝ"), num + 1);
                    if (num < 0)
                    {
                        num = text2.Length;
                    }
                    num += 2;
                }
                else if (text3 == "#[")
                {
                    num += 2;
                }
                else
                {
                    if (!(text3 == GClass0.smethod_0("!ź")) && !(text3 == GClass0.smethod_0("&ź")))
                    {
                        text3 = MID(text2, num, 1);
                        if (text3 == GClass0.smethod_0("%"))
                        {
                            bool flag2 = false;
                            string[] aryQnFunc = _aryQnFunc;
                            foreach (string text7 in aryQnFunc)
                            {
                                text5 = "";
                                if (text2.Length - num > text7.Length)
                                {
                                    text5 = MID(text2, num, text7.Length);
                                }
                                if (text7 == text5.ToUpper())
                                {
                                    flag2 = true;
                                    num2 = RightBrackets(text2, num, GClass0.smethod_0(")"), GClass0.smethod_0("("));
                                    num += text7.Length;
                                    text4 = text2.Substring(num, num2 - num + 1).Replace(GClass0.smethod_0("!"), "").Trim();
                                    if (!list.Contains(text4))
                                    {
                                        list.Add(text7 + text4);
                                    }
                                    if (text == "" && LEFT(text4, 1) == "#")
                                    {
                                        text = text7 + text4;
                                    }
                                    text4 = "";
                                    num = num2 + 1;
                                    break;
                                }
                            }
                            if (flag2)
                            {
                                continue;
                            }
                            num2 = 0;
                            if (isLetter(MID(text2, num + 1, 1)))
                            {
                                num2 = text2.IndexOf(GClass0.smethod_0(")"), num + 1);
                            }
                            if (num2 > 0)
                            {
                                num = num2;
                            }
                        }
                        else
                        {
                            text3 = JoinQnName(text3, text4);
                            if (text3 == "")
                            {
                                if (text4.Length > 0)
                                {
                                    text4 = text4.Trim();
                                    if (!list.Contains(text4))
                                    {
                                        list.Add(text4);
                                    }
                                    if (text == "" && LEFT(text4, 1) == "#")
                                    {
                                        text = text4;
                                    }
                                    text4 = "";
                                }
                            }
                            else
                            {
                                text4 = text3;
                            }
                        }
                        goto IL_04ca;
                    }
                    if (text4.Length > 0)
                    {
                        text4 = text4.Trim();
                        if (!list.Contains(text4))
                        {
                            list.Add(text4);
                        }
                        if (text == "" && LEFT(text4, 1) == "#")
                        {
                            text = text4;
                        }
                        text4 = "";
                    }
                    num2 = text2.IndexOf(GClass0.smethod_0("|"), num);
                    if (num2 < 0)
                    {
                        num2 = text2.Length - 1;
                    }
                    text4 = text2.Substring(num, num2 - num).Trim();
                    if (!list.Contains(text4))
                    {
                        list.Add(text4);
                    }
                    text4 = "";
                    num = num2 + 1;
                }
                continue;
                IL_04ca:
                num++;
            }
            while (num < text2.Length);
            if (text4.Length > 0)
            {
                text4 = text4.Trim();
                if (!list.Contains(text4))
                {
                    list.Add(text4);
                }
                if (text == "" && LEFT(text4, 1) == "#")
                {
                    text = text4;
                }
            }
            if (text.Length > 0)
            {
                list.Remove(text);
                list.Insert(0, text);
            }
            if (list.Count > 0)
            {
                OutputResult(GClass0.smethod_0("8Łɐ\u033f") + string_0 + GClass0.smethod_0("霌袊膽僟䫭䬌麞燫纀牗扊\uf41b"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                OutputResult(method_130(list, GClass0.smethod_0(".ġ"), ""), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            }
            return list;
        }

        private string GetAnswer(string string_0)
        {
            string text = "";
            string text2 = string_0;
            if (string_0.Length > 0)
            {
                if (LEFT(text2, 1) == GClass0.smethod_0("Z"))
                {
                    text2 = MID(text2, 1, -9999);
                }
                if (method_133(text2, 1) == "]")
                {
                    text2 = LEFT(text2, text2.Length - 1);
                }
                if (_dictData.ContainsKey(text2))
                {
                    text = _dictData[text2];
                }
            }
            OutputResult(string_0 + GClass0.smethod_0("#Ŀȡ") + text, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            return text;
        }

        public string DeleteOuterSymbol(string string_0)
        {
            string text = string_0;
            if (text.Length > 1)
            {
                int length = text.Length;
                string a = LEFT(text, 1);
                string a2 = LEFT(text, 2);
                while (a2 != GClass0.smethod_0("Yģ") && (a == GClass0.smethod_0("Z") || a == GClass0.smethod_0(")") || a == GClass0.smethod_0("z")))
                {
                    if (a == GClass0.smethod_0("Z"))
                    {
                        if (RightBrackets(text, 0, GClass0.smethod_0("Z"), "]") == text.Length - 1)
                        {
                            text = MID(text, 1, text.Length - 2);
                        }
                    }
                    else if (a == GClass0.smethod_0(")"))
                    {
                        if (RightBrackets(text, 0, GClass0.smethod_0(")"), GClass0.smethod_0("(")) == text.Length - 1)
                        {
                            text = MID(text, 1, text.Length - 2);
                        }
                    }
                    else if (a == GClass0.smethod_0("z") && RightBrackets(text, 0, GClass0.smethod_0("z"), GClass0.smethod_0("|")) == text.Length - 1)
                    {
                        text = MID(text, 1, text.Length - 2);
                    }
                    if (text.Length == 0 || text.Length == length)
                    {
                        break;
                    }
                    length = text.Length;
                    a = LEFT(text, 1);
                    a2 = LEFT(text, 2);
                }
            }
            return text;
        }

        public int GetFormulaSplitLocation(string string_0, string string_1)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            do
            {
                if (!(MID(string_0, num2, 2) == GClass0.smethod_0("Yģ")))
                {
                    string text = MID(string_0, num2, 1);
                    string a = text;
                    if (a == GClass0.smethod_0("Z"))
                    {
                        switch (num)
                        {
                            case 0:
                                num = 1;
                                num3 = 1;
                                break;
                            case 1:
                                num3++;
                                break;
                        }
                    }
                    else if (!(a == "]"))
                    {
                        if (a == GClass0.smethod_0(")"))
                        {
                            switch (num)
                            {
                                case 0:
                                    num = 2;
                                    num3 = 1;
                                    break;
                                case 2:
                                    num3++;
                                    break;
                            }
                        }
                        else if (!(a == GClass0.smethod_0("(")))
                        {
                            if (a == GClass0.smethod_0("z"))
                            {
                                switch (num)
                                {
                                    case 0:
                                        num = 3;
                                        num3 = 1;
                                        break;
                                    case 3:
                                        num3++;
                                        break;
                                }
                            }
                            else if (!(a == GClass0.smethod_0("|")))
                            {
                                if (string_1.IndexOf(text) > -1 && num == 0)
                                {
                                    return num2;
                                }
                            }
                            else if (num == 3)
                            {
                                num3--;
                                if (num3 == 0)
                                {
                                    num = 0;
                                }
                            }
                        }
                        else if (num == 2)
                        {
                            num3--;
                            if (num3 == 0)
                            {
                                num = 0;
                            }
                        }
                    }
                    else if (num == 1)
                    {
                        num3--;
                        if (num3 == 0)
                        {
                            num = 0;
                        }
                    }
                    num2++;
                }
                else
                {
                    num2 = string_0.IndexOf(GClass0.smethod_0(" Ŝ"), num2 + 2);
                    if (num2 < 0)
                    {
                        return -1;
                    }
                    num2 += 2;
                }
            }
            while (num2 < string_0.Length);
            return -1;
        }

        private int method_88(string string_0)
        {
            if (string_0.Length <= 0)
            {
                return -1;
            }
            string text = DeleteOuterSymbol(string_0);
            int num = 0;
            int num2 = 0;
            int num3 = -1;
            if (!(LEFT(text, 1) == GClass0.smethod_0("%")))
            {
                return text.IndexOf(GClass0.smethod_0(")"));
            }
            for (int i = 0; i < string_0.Length; i++)
            {
                char c = string_0[i];
                num3++;
                string text2 = c.ToString();
                string a = text2;
                if (!(a == GClass0.smethod_0(")")))
                {
                    if (a == GClass0.smethod_0("(") && num == 2)
                    {
                        num2--;
                        if (num2 == 1)
                        {
                            num = 0;
                        }
                    }
                }
                else
                {
                    switch (num)
                    {
                        case 0:
                            if (num2 == 1)
                            {
                                return num3;
                            }
                            num = 2;
                            num2 = 2;
                            break;
                        case 2:
                            num2++;
                            break;
                    }
                }
            }
            return -1;
        }

        private bool method_89(string string_0)
        {
            if (!string_0.Contains(GClass0.smethod_0("+ĥ")))
            {
                if (!string_0.Contains(GClass0.smethod_0("$ź")))
                {
                    List<string> list = ParaToList(string_0, GClass0.smethod_0("\u007f"), false);
                    if (list.Count <= 1)
                    {
                        list = ParaToList(string_0, GClass0.smethod_0("?"), false);
                        if (list.Count <= 1)
                        {
                            list = ParaToList(string_0, GClass0.smethod_0("="), false);
                            if (list.Count <= 1)
                            {
                                list = ParaToList(string_0, GClass0.smethod_0("<"), false);
                                if (list.Count <= 1)
                                {
                                    list = ParaToList(string_0, GClass0.smethod_0("<ļ"), false);
                                    if (list.Count <= 1)
                                    {
                                        list = ParaToList(string_0, GClass0.smethod_0(">ļ"), false);
                                        if (list.Count <= 1)
                                        {
                                            list = ParaToList(string_0, GClass0.smethod_0(">Ŀ"), false);
                                            if (list.Count <= 1)
                                            {
                                                list = ParaToList(string_0, GClass0.smethod_0("*"), false);
                                                if (list.Count <= 1)
                                                {
                                                    list = ParaToList(string_0, GClass0.smethod_0(","), false);
                                                    if (list.Count <= 1)
                                                    {
                                                        list = ParaToList(string_0, GClass0.smethod_0("+"), false);
                                                        if (list.Count <= 1)
                                                        {
                                                            list = ParaToList(string_0, GClass0.smethod_0("."), false);
                                                            if (list.Count <= 1)
                                                            {
                                                                if (!string_0.Contains(GClass0.smethod_0("Yģ")))
                                                                {
                                                                    if (!string_0.Contains("$["))
                                                                    {
                                                                        if (!string_0.Contains("#["))
                                                                        {
                                                                            string[] array = new string[11]
                                                                            {
                                                                                GClass0.smethod_0("'őȩ"),
                                                                                GClass0.smethod_0("!ŗɗ\u0350Щ"),
                                                                                GClass0.smethod_0(",Ŕɒ\u0357эՍمܩ"),
                                                                                GClass0.smethod_0("!ŗɖ\u0340Щ"),
                                                                                GClass0.smethod_0("!ŉɊ\u0346Щ"),
                                                                                GClass0.smethod_0("#ŔɌ\u0343ыՖة"),
                                                                                GClass0.smethod_0("\"őɖ\u034aяԩ"),
                                                                                GClass0.smethod_0("#Ŋɑ\u0356ъՏة"),
                                                                                GClass0.smethod_0("#Ŕɑ\u0356ъՏة"),
                                                                                GClass0.smethod_0("#ŉɑ\u034cцՐة"),
                                                                                GClass0.smethod_0(" Ōɖ\u0349")
                                                                            };
                                                                            string[] array2 = array;
                                                                            int num = 0;
                                                                            while (true)
                                                                            {
                                                                                if (num >= array2.Length)
                                                                                {
                                                                                    return false;
                                                                                }
                                                                                string value = array2[num];
                                                                                if (string_0.Contains(value))
                                                                                {
                                                                                    break;
                                                                                }
                                                                                num++;
                                                                            }
                                                                            return true;
                                                                        }
                                                                        return true;
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
                                            return false;
                                        }
                                        return false;
                                    }
                                    return false;
                                }
                                return false;
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
                return true;
            }
            return true;
        }

        private int method_90(string string_0, int int_0)
        {
            int num = int_0;
            if (MID(string_0, int_0, 1) == GClass0.smethod_0("%"))
            {
                num = string_0.IndexOf(GClass0.smethod_0(")"), int_0);
                if (num < 0)
                {
                    return int_0;
                }
                if (MID(string_0, RightBrackets(string_0, num, GClass0.smethod_0(")"), GClass0.smethod_0("(")), 1) != GClass0.smethod_0("("))
                {
                    return int_0;
                }
                string text = method_132(string_0, int_0 + 1, num - 1);
                string text2 = text;
                for (int i = 0; i < text2.Length; i++)
                {
                    if (!isLetter(text2[i].ToString()))
                    {
                        return int_0;
                    }
                }
            }
            return num;
        }

        private int method_91(string string_0, int int_0)
        {
            int num = method_90(string_0, int_0);
            if (num <= int_0)
            {
                return num;
            }
            return RightBrackets(string_0, num, GClass0.smethod_0(")"), GClass0.smethod_0("("));
        }

        private void method_92(string string_0, string string_1)
        {
            char c = '\u0012';
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            string[] array = string_1.Split('\u0011');
            string[] array2 = array;
            foreach (string text in array2)
            {
                classLoopB item = new classLoopB();
                _LoopQn.A.Add(item);
                num3 = 0;
                string[] array3 = text.Split(c);
                if (num < array3.Count())
                {
                    num = array3.Count();
                }
                string[] array4 = array3;
                foreach (string value in array4)
                {
                    classLoopQuestion item2 = new classLoopQuestion();
                    _LoopQn.A[num2].B.Add(item2);
                    _LoopQn.A[num2].B[num3].Qn.Add(GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64"), GClass0.smethod_0("1"));
                    _LoopQn.A[num2].B[num3].Qn.Add(string_0, value);
                    num3++;
                }
                num2++;
            }
            foreach (classLoopB item4 in _LoopQn.A)
            {
                if (item4.B.Count < num)
                {
                    for (num3 = item4.B.Count; num3 < num; num3++)
                    {
                        classLoopQuestion item3 = new classLoopQuestion();
                        item4.B.Add(item3);
                        item4.B[num3].Qn.Add(GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64"), GClass0.smethod_0("1"));
                        item4.B[num3].Qn.Add(string_0, "");
                    }
                }
            }
            _nTotalLoopA = _LoopQn.A.Count;
            _nTotalLoopB = num;
        }

        private void method_93(string string_0, string string_1)
        {
            char c = '\u0011';
            char value = '\u0012';
            int num = 0;
            if (method_100(string_0) == 2)
            {
                bool bool_ = string_1.Contains(value);
                string[] array = string_1.Split(c);
                if (array.Count() >= _LoopQn.A.Count)
                {
                    num = 0;
                    foreach (classLoopB item in _LoopQn.A)
                    {
                        method_94(item, array[num], string_0, bool_);
                        num++;
                    }
                }
                else
                {
                    num = 0;
                    string[] array2 = array;
                    foreach (string string_2 in array2)
                    {
                        method_94(_LoopQn.A[num], string_2, string_0, bool_);
                        num++;
                    }
                }
            }
            else
            {
                foreach (classLoopB item2 in _LoopQn.A)
                {
                    foreach (classLoopQuestion item3 in item2.B)
                    {
                        if (!item3.Qn.ContainsKey(string_0))
                        {
                            item3.Qn.Add(string_0, string_1);
                        }
                    }
                }
            }
        }

        private void method_94(classLoopB classLoopB_0, string string_0, string string_1, bool bool_0)
        {
            char c = '\u0012';
            int num = 0;
            if (bool_0)
            {
                string[] array = string_0.Split(c);
                if (array.Count() >= classLoopB_0.B.Count)
                {
                    num = 0;
                    foreach (classLoopQuestion item in classLoopB_0.B)
                    {
                        if (!item.Qn.ContainsKey(string_1))
                        {
                            item.Qn.Add(string_1, array[num]);
                        }
                        num++;
                    }
                }
                else
                {
                    num = 0;
                    string[] array2 = array;
                    foreach (string value in array2)
                    {
                        if (!classLoopB_0.B[num].Qn.ContainsKey(string_1))
                        {
                            classLoopB_0.B[num].Qn.Add(string_1, value);
                        }
                        num++;
                    }
                }
            }
            else
            {
                foreach (classLoopQuestion item2 in classLoopB_0.B)
                {
                    if (!item2.Qn.ContainsKey(string_1))
                    {
                        item2.Qn.Add(string_1, string_0);
                    }
                }
            }
        }

        private string method_95(string string_0)
        {
            string result = "";
            if (_dictQn.ContainsKey(string_0))
            {
                result = _dictQn[string_0];
            }
            return result;
        }

        private string method_96(string string_0)
        {
            string result = "";
            if (_LoopQn.A[_nCurrentLoopA].B[_nCurrentLoopB].Qn.ContainsKey(string_0))
            {
                result = _LoopQn.A[_nCurrentLoopA].B[_nCurrentLoopB].Qn[string_0];
            }
            return result;
        }

        private string method_97(string string_0)
        {
            string text = "";
            char c = '\u0006';
            if (string_0.Length > 0)
            {
                string text2 = "";
                for (int i = 0; i < string_0.Length; i++)
                {
                    char value = string_0[i];
                    if (sySeqSplit.IndexOf(value) > -1)
                    {
                        if (text2.Length > 0)
                        {
                            if (text2 != c.ToString() && text2 != GClass0.smethod_0("!"))
                            {
                                text += method_98(text2);
                            }
                            text2 = "";
                        }
                        text += value.ToString();
                    }
                    else
                    {
                        text2 += value.ToString();
                    }
                }
                if (text2.Length > 0 && text2 != c.ToString() && text2 != GClass0.smethod_0("!"))
                {
                    text += method_98(text2);
                }
            }
            OutputResult(GClass0.smethod_0("栂僂儕圌\ufb1b") + text + GClass0.smethod_0("8Łɐ\u033f"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            return text;
        }

        private string method_98(string string_0)
        {
            string text = "";
            string text2 = string_0.Trim();
            if (method_117(text2))
            {
                double num = Convert.ToDouble(text2);
                int num2 = text2.IndexOf(GClass0.smethod_0("/"));
                text = ((text2.Length > 9) ? text2 : num.ToString());
                if (num2 > 0 && num2 < text2.Length - 1)
                {
                    string text3 = LEFT(text2, num2);
                    string text4 = MID(text2, num2 + 1, -9999);
                    num2 = text.IndexOf(GClass0.smethod_0("/"));
                    text = ((num2 != -1) ? string_0 : (method_133(method_115(GClass0.smethod_0("1"), text3.Length) + text, text3.Length) + GClass0.smethod_0("/") + method_115(GClass0.smethod_0("1"), text4.Length)));
                }
            }
            else
            {
                text = string_0;
            }
            return text;
        }

        private string method_99(string string_0)
        {
            string text = "";
            if (string_0.Length > 0)
            {
                int num = 0;
                do
                {
                    string text2 = MID(string_0, num, 1);
                    if (!(text2 == GClass0.smethod_0("%")))
                    {
                        text2 = JoinQnName(text2, text);
                        if (text2 == "")
                        {
                            return text;
                        }
                        text = text2;
                    }
                    else
                    {
                        num = string_0.IndexOf(GClass0.smethod_0(")"), num + 1);
                    }
                    num++;
                }
                while (num < string_0.Length);
            }
            return text;
        }

        private int method_100(string string_0)
        {
            int result = 0;
            string a = LEFT(string_0, 1);
            if (a == GClass0.smethod_0("%"))
            {
                string[] aryQnFunc = _aryQnFunc;
                foreach (string text in aryQnFunc)
                {
                    if (LEFT(string_0, text.Length).ToUpper() == text)
                    {
                        a = MID(string_0, text.Length, 1);
                        if (a == "#")
                        {
                            result = 2;
                        }
                        else if (a == GClass0.smethod_0("_"))
                        {
                            result = 1;
                        }
                        break;
                    }
                }
            }
            else if (a == "#")
            {
                result = 2;
            }
            else if (a == GClass0.smethod_0("_"))
            {
                result = 1;
            }
            return result;
        }

        private void method_101(string string_0, int int_0 = 0)
        {
            string text = DeleteOuterSymbol(string_0);
            int length = text.Length;
            if (length > 0)
            {
                List<string> list = new List<string>();
                list = method_87(text, int_0);
                string text2 = "";
                if (list.Count == 0)
                {
                    classLoopB item = new classLoopB();
                    _LoopQn.A.Add(item);
                    classLoopQuestion item2 = new classLoopQuestion();
                    _LoopQn.A[0].B.Add(item2);
                    _LoopQn.A[0].B[0].Qn.Add(GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64"), GClass0.smethod_0("1"));
                }
                else
                {
                    bool flag = true;
                    foreach (string item3 in list)
                    {
                        text2 = GetAnswer(item3);
                        if (flag)
                        {
                            method_92(item3, text2);
                            if (method_100(item3) != 2)
                            {
                                _dictQn.Add(item3, text2);
                            }
                            flag = false;
                        }
                        else if (method_100(item3) == 2)
                        {
                            method_93(item3, text2);
                        }
                        else
                        {
                            _dictQn.Add(item3, text2);
                            method_93(item3, text2);
                        }
                    }
                }
                OutputDictQn(_dictQn, _LoopQn);
            }
        }

        private string method_102(string string_0, LogicExplain logicExplain_0)
        {
            if (string_0.Length > 0)
            {
                List<string> list = ParaToList(string_0, GClass0.smethod_0("-"), false);
                string text;
                string string_;
                if (list.Count() > 1 && list[1].Length > 0)
                {
                    text = CleanFormula(list[1]);
                    string_ = CleanFormula(GClass0.smethod_0("Z") + list[0] + GClass0.smethod_0("_ħ") + text);
                }
                else
                {
                    string_ = CleanFormula(list[0]);
                    text = method_99(string_);
                }
                if (logicExplain_0.LoopLogicFormula(string_) > 0.0)
                {
                    return text;
                }
            }
            return "";
        }

        private Dictionary<string, string> method_103(Dictionary<string, double> dictionary_0, bool bool_0 = false, bool bool_1 = false)
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
                            int index = k;
                            list2[index]++;
                        }
                        if (num < num2)
                        {
                            List<int> list3 = list;
                            int index2 = j;
                            list3[index2]++;
                        }
                    }
                    else
                    {
                        if (num < num2)
                        {
                            List<int> list4 = list;
                            int index = k;
                            list4[index]++;
                        }
                        if (num > num2)
                        {
                            List<int> list5 = list;
                            int index2 = j;
                            list5[index2]++;
                        }
                    }
                }
            }
            if (bool_1)
            {
                dictionary.Clear();
                int i = 0;
                foreach (string key3 in dictionary_0.Keys)
                {
                    dictionary.Add(key3, list[i++].ToString());
                }
            }
            else
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    string key = list[i].ToString();
                    dictionary[key] = dictionary[key] + GClass0.smethod_0("-") + dictionary_0.Keys.ElementAt(i);
                }
                for (int i = 1; i <= dictionary.Count(); i++)
                {
                    string key2 = i.ToString();
                    if (dictionary[key2] == "")
                    {
                        dictionary[key2] = dictionary[(i - 1).ToString()];
                    }
                    if (LEFT(dictionary[key2], 1) == ','.ToString())
                    {
                        dictionary[key2] = MID(dictionary[key2], 1, -9999);
                    }
                }
            }
            return dictionary;
        }

        private string method_104(string string_0, LogicExplain logicExplain_0)
        {
            string text = "";
            List<classHtmlText> list = SplitTextToList(string_0.Trim(), GClass0.smethod_0("Yģ"), GClass0.smethod_0(" Ŝ"));
            string text2 = "";
            foreach (classHtmlText item in list)
            {
                if (item.TitleTextType == "")
                {
                    text2 = ((!(text2 == "")) ? (text2 + GClass0.smethod_0("$Ś") + item.TitleText + "]") : (GClass0.smethod_0("Z") + item.TitleText + "]"));
                }
            }
            if (logicExplain_0.LoopLogicFormula(text2) > 0.0)
            {
                foreach (classLoopB item2 in logicExplain_0._LoopQn.A)
                {
                    foreach (classLoopQuestion item3 in item2.B)
                    {
                        text = "";
                        if (item3.Qn[GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64")] == GClass0.smethod_0("0"))
                        {
                            foreach (classHtmlText item4 in list)
                            {
                                if (item4.TitleTextType == "")
                                {
                                    string string_ = method_99(item4.TitleText);
                                    text += GetAnswer(string_);
                                }
                                else
                                {
                                    text += item4.TitleText;
                                }
                            }
                            item3.Qn[GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64")] = text;
                        }
                    }
                }
            }
            return text;
        }

        public int RightBrackets(string string_0, int int_0, string string_1 = "(", string string_2 = ")")
        {
            int length = string_0.Length;
            int num = int_0;
            int num2 = 0;
            do
            {
                if (!(MID(string_0, num, 2) == GClass0.smethod_0("Yģ")))
                {
                    if (MID(string_0, num, string_1.Length) == string_1)
                    {
                        num2 = ((num2 != 0) ? (num2 + 1) : 2);
                        num += string_1.Length;
                    }
                    else if (MID(string_0, num, string_2.Length) == string_2)
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
                else
                {
                    num = string_0.IndexOf(GClass0.smethod_0(" Ŝ"), num + 2);
                    if (num < 0)
                    {
                        return string_0.Length;
                    }
                    num += 2;
                }
            }
            while (num < string_0.Length);
            return length;
        }

        private string method_105(string string_0, int int_0 = 0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                string text = "";
                if (string_0.IndexOf(GClass0.smethod_0(".")) > -1)
                {
                    string text2 = string_0.Trim();
                    int num = text2.IndexOf(GClass0.smethod_0("!"));
                    if (num > -1)
                    {
                        text2 = LEFT(string_0, num);
                    }
                    List<string> list = new List<string>(text2.Split(Convert.ToChar(GClass0.smethod_0("."))));
                    text = list[0].Trim();
                }
                else if (string_0.IndexOf(GClass0.smethod_0(";")) == -1)
                {
                    string_0 = LEFT(string_0, 8);
                    int num2 = string_0.IndexOf(GClass0.smethod_0("/"));
                    if (num2 > -1)
                    {
                        string_0 = LEFT(string_0, num2);
                    }
                    text = LEFT(string_0, string_0.Length - 4);
                }
                result = text;
                switch (int_0)
                {
                    case 2:
                        result = method_135(text, GClass0.smethod_0("1"), 2, true);
                        break;
                    case 4:
                        result = ((text.Length >= 3) ? method_133(text, 4) : method_113(text));
                        break;
                }
            }
            return result;
        }

        private string method_106(string string_0, int int_0 = 2, bool bool_0 = false)
        {
            string text = "";
            if (string_0.Length > 0)
            {
                string text2 = "";
                if (string_0.IndexOf(GClass0.smethod_0(".")) > -1)
                {
                    string text3 = string_0.Trim();
                    int num = text3.IndexOf(GClass0.smethod_0("!"));
                    if (num > -1)
                    {
                        text3 = LEFT(string_0, num);
                    }
                    List<string> list = new List<string>(text3.Split(Convert.ToChar(GClass0.smethod_0("."))));
                    text2 = ((list.Count() == 1) ? list[0].Trim() : ((!bool_0) ? list[1].Trim() : list[0].Trim()));
                }
                else if (string_0.IndexOf(GClass0.smethod_0(";")) == -1)
                {
                    string_0 = LEFT(string_0, 8);
                    int num2 = string_0.IndexOf(GClass0.smethod_0("/"));
                    if (num2 > -1)
                    {
                        string_0 = LEFT(string_0, num2);
                    }
                    text2 = method_133(LEFT(string_0, string_0.Length - 2), 2);
                }
                text = text2;
                if (int_0 == 2)
                {
                    text = method_133(GClass0.smethod_0("2ı") + text, 2);
                }
            }
            return text;
        }

        private string method_107(string string_0, int int_0 = 2)
        {
            string text = "";
            if (string_0.Length > 0)
            {
                if (string_0.IndexOf(GClass0.smethod_0(".")) > -1)
                {
                    string text2 = string_0.Trim();
                    int num = text2.IndexOf(GClass0.smethod_0("!"));
                    if (num > -1)
                    {
                        text2 = LEFT(string_0, num);
                    }
                    List<string> list = new List<string>(text2.Split(Convert.ToChar(GClass0.smethod_0("."))));
                    text = list[list.Count - 1].Trim();
                }
                else if (string_0.IndexOf(GClass0.smethod_0(";")) == -1)
                {
                    string_0 = LEFT(string_0, 8);
                    int num2 = string_0.IndexOf(GClass0.smethod_0("/"));
                    if (num2 > -1)
                    {
                        string_0 = LEFT(string_0, num2);
                    }
                    text = method_133(string_0, 2);
                }
                if (int_0 == 2)
                {
                    text = method_133(GClass0.smethod_0("2ı") + text, 2);
                }
            }
            return text;
        }

        private string method_108(string string_0, int int_0 = 2)
        {
            string text = GClass0.smethod_0("1");
            if (string_0.Length > 0)
            {
                if (string_0.IndexOf(GClass0.smethod_0(";")) > -1)
                {
                    string text2 = string_0.Trim();
                    int num = text2.IndexOf(GClass0.smethod_0("!"));
                    if (num > -1)
                    {
                        text2 = MID(string_0, num + 1, -9999).Trim();
                    }
                    List<string> list = new List<string>(text2.Split(Convert.ToChar(GClass0.smethod_0(";"))));
                    text = list[0].Trim();
                }
                else
                {
                    string_0 = method_133(string_0, 7);
                    int num2 = string_0.IndexOf(GClass0.smethod_0("/"));
                    if (num2 > -1)
                    {
                        string_0 = MID(string_0, num2 + 1, -9999);
                        text = LEFT(string_0, 2);
                    }
                }
                if (int_0 == 2)
                {
                    text = method_133(GClass0.smethod_0("2ı") + text, 2);
                }
            }
            return text;
        }

        private string method_109(string string_0, int int_0 = 2, bool bool_0 = false)
        {
            string text = GClass0.smethod_0("1");
            if (string_0.Length > 0)
            {
                if (string_0.IndexOf(GClass0.smethod_0(";")) > -1)
                {
                    string text2 = string_0.Trim();
                    int num = text2.IndexOf(GClass0.smethod_0("!"));
                    if (num > -1)
                    {
                        text2 = MID(string_0, num + 1, -9999).Trim();
                    }
                    List<string> list = new List<string>(text2.Split(Convert.ToChar(GClass0.smethod_0(";"))));
                    text = ((list.Count() == 1) ? list[0].Trim() : ((!bool_0) ? list[1].Trim() : list[0].Trim()));
                }
                else
                {
                    string_0 = method_133(string_0, 7);
                    int num2 = string_0.IndexOf(GClass0.smethod_0("/"));
                    if (num2 > -1)
                    {
                        string_0 = MID(string_0, num2 + 1, -9999);
                        text = MID(string_0, 2, 2);
                    }
                }
                if (int_0 == 2)
                {
                    text = method_133(GClass0.smethod_0("2ı") + text, 2);
                }
            }
            return text;
        }

        private string method_110(string string_0, int int_0 = 2)
        {
            string text = GClass0.smethod_0("1");
            if (string_0.Length > 0)
            {
                if (string_0.IndexOf(GClass0.smethod_0(";")) > -1)
                {
                    string text2 = string_0.Trim();
                    int num = text2.IndexOf(GClass0.smethod_0("!"));
                    if (num > -1)
                    {
                        text2 = MID(string_0, num + 1, -9999).Trim();
                    }
                    List<string> list = new List<string>(text2.Split(Convert.ToChar(GClass0.smethod_0(";"))));
                    text = list[list.Count - 1].Trim();
                }
                else
                {
                    string_0 = method_133(string_0, 7);
                    int num2 = string_0.IndexOf(GClass0.smethod_0("/"));
                    if (num2 > -1)
                    {
                        string_0 = MID(string_0, num2 + 1, -9999);
                        text = MID(string_0, 4, 2);
                    }
                }
                if (int_0 == 2)
                {
                    text = method_133(GClass0.smethod_0("2ı") + text, 2);
                }
            }
            return text;
        }

        private string method_111(int int_0)
        {
            string[] array = new string[8]
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
            return array[int_0];
        }

        private string method_112(string string_0)
        {
            string result = "";
            if (string_0.Length > 0)
            {
                string text = method_6(string_0);
                string text2 = "";
                string text3 = "";
                string text4 = "";
                string text5 = GClass0.smethod_0("2ı");
                string text6 = GClass0.smethod_0("2ı");
                string text7 = GClass0.smethod_0("2ı");
                if (text.IndexOf(GClass0.smethod_0(".")) == -1 && text.IndexOf(GClass0.smethod_0(";")) == -1)
                {
                    string text8 = LEFT(text, 8);
                    int num = text8.IndexOf(GClass0.smethod_0("/"));
                    if (num > -1)
                    {
                        text8 = LEFT(text8, num);
                    }
                    text2 = LEFT(text8, text8.Length - 4);
                    text3 = method_133(LEFT(text8, text8.Length - 2), 2);
                    text4 = method_133(text8, 2);
                    string text9 = method_133(text, 7);
                    num = text9.IndexOf(GClass0.smethod_0("/"));
                    if (num > -1)
                    {
                        text9 = MID(text9, num + 1, -9999);
                        text5 = LEFT(text9, 2);
                        text6 = MID(text9, 2, 2);
                        text7 = MID(text9, 4, 2);
                    }
                    text = text2 + GClass0.smethod_0(".") + text3 + GClass0.smethod_0(".") + text4 + GClass0.smethod_0("!") + text5 + GClass0.smethod_0(";") + text6 + GClass0.smethod_0(";") + text7;
                }
                result = text;
            }
            return result;
        }

        private string method_113(string string_0)
        {
            if (string_0.Length < 3 && method_117(string_0))
            {
                int num = Convert.ToInt32(string_0);
                return ((num < 51) ? (2000 + num) : (1900 + num)).ToString();
            }
            return string_0;
        }

        private string method_114(string string_0)
        {
            string text = "";
            List<string> list = ParaToList(string_0, GClass0.smethod_0("}"), false);
            if (list.Count > 0)
            {
                foreach (string item in list)
                {
                    List<string> list2 = ParaToList(item, GClass0.smethod_0("\u007f"), false);
                    if (list2.Count > 1)
                    {
                        string string_ = method_14(list2[0]);
                        string string_2 = method_14(list2[1]);
                        int num = method_126(string_);
                        int num2 = method_126(string_2);
                        for (int i = num; i <= num2; i++)
                        {
                            text = text + GClass0.smethod_0("-") + i.ToString();
                        }
                    }
                    else
                    {
                        text = text + GClass0.smethod_0("-") + method_14(string_0);
                    }
                }
                if (LEFT(text, 1) == ','.ToString())
                {
                    text = MID(text, 1, -9999);
                }
            }
            return text;
        }

        private string method_115(string string_0, int int_0)
        {
            string text = "";
            if (string_0.Length > 0)
            {
                for (int i = 0; i < int_0; i++)
                {
                    text += string_0;
                }
            }
            return text;
        }

        private bool method_116(string string_0)
        {
            Regex regex = new Regex(GClass0.smethod_0("[Řɧ\u0329Х"));
            return regex.IsMatch(string_0);
        }

        private bool method_117(string string_0)
        {
            Regex regex = new Regex(GClass0.smethod_0("Kļɏ\u033fѭՌؤܧ࠲ॐ੯ଡడ\u0d54ษཚၡᄯሪጽᐥ"));
            return regex.IsMatch(string_0);
        }

        public bool isLetter(string string_0)
        {
            Regex regex = new Regex(GClass0.smethod_0("UőɈ\u0325ѝէبݾ࡞ऩਥ"));
            return regex.IsMatch(string_0);
        }

        public bool isEnglishWord(string string_0)
        {
            Regex regex = new Regex(GClass0.smethod_0("PŖɍ\u0326ѐըإݽ࠶न\u0a3d\u0b5e\u0c29ഥ"));
            return regex.IsMatch(string_0);
        }

        public bool isWord(string string_0)
        {
            Regex regex = new Regex(GClass0.smethod_0("Bŀɛ\u0334тնػݯ\u0824\u093eਫ\u0b4e\u0c4cൺ\u0e3aཨ\u103cᄻሧፕᑽᔾᙠᝤᠱᥞᨩᬥ"));
            return regex.IsMatch(string_0);
        }

        private bool method_118(string string_0)
        {
            Regex regex = new Regex(GClass0.smethod_0("wŝȂ\u0300Ѽԋ؎܊\u087eॾ\u0a56ଋశഴ\u0e5dཀ\u106cᄱሱፃᐺᔸᙈᝈᡤ\u1939ᨸ\u1b3a᱓ᴠṑύ†™≒⌥\u2429╛♙❳⠨⤫⨫"));
            return regex.IsMatch(string_0);
        }

        private bool method_119(string string_0)
        {
            string input = (MID(string_0, 7, -9999).ToLower() == GClass0.smethod_0("oŲɱ\u0374йԭخ")) ? string_0 : (GClass0.smethod_0("oŲɱ\u0374йԭخ") + string_0);
            Regex regex = new Regex(GClass0.smethod_0("AŜɓ\u0356Пԋ،܊\u087aॼ੨ଳ\u0c40ഷ\u0e47༴\u1030ᄳቌፊᑢᔹᙎ\u1739ᠹ\u193fᩔ᭒\u1c7aᴡḫἤ…ℷ∢⌠\u2438╙☩✫⠾"));
            return regex.IsMatch(input);
        }

        private bool method_120(string string_0)
        {
            if (string_0.Length == 7)
            {
                if (!MID(string_0, 2, -9999).Contains(GClass0.smethod_0("H")))
                {
                    if (!MID(string_0, 2, -9999).Contains(GClass0.smethod_0("N")))
                    {
                        Regex regex = new Regex(GClass0.smethod_0("dŢ蟷廪锴瀭譗訆恰韠暚弦嶮怈桧䆇龗趨隱蛚侧艷战腶髭澷瑸纯爃酶抸冊䵃⅄≣⌦⑫╎♕✾⡈⥌⩫⬾ⱳⵖ\u2e4d⼦ぐㅖ㈸㌪㐿㕘㙿㜶㡿㤥"));
                        return regex.IsMatch(string_0);
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool method_121(string string_0)
        {
            if (string_0.Length == 17)
            {
                if (!string_0.Contains(GClass0.smethod_0("H")))
                {
                    if (!string_0.Contains(GClass0.smethod_0("N")))
                    {
                        if (!string_0.Contains(GClass0.smethod_0("P")))
                        {
                            Regex regex = new Regex(GClass0.smethod_0("UőɈ\u0325ѝԶب\u073d࡞ऩਥ"));
                            return regex.IsMatch(string_0);
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool method_122(string string_0)
        {
            if (string_0.Length != 18)
            {
                if (string_0.Length != 15)
                {
                    return false;
                }
                Regex regex = new Regex(GClass0.smethod_0("kůȂ\u031fЈխٳ\u074aࡖछ\u0a56\u0b02\u0c01ഘ\u0e7bགဌᅘላጓᑺᔐᘲᜬᡀ\u1935ᨲᬲ\u1c31\u1d43ḧὪ․Ⅸ∡⍏\u244d╴☦❲⠾⥗⨻⬧ⰸⵕ⸮⽚ちㅿ㈰㍿㐥"));
                return regex.IsMatch(string_0);
            }
            Regex regex2 = new Regex(GClass0.smethod_0("\u0016Ĝɷ\u0368ѽԞ؞ܥ࠻ऊ\u0a43୦\u0c0dഖฃཤ\u1064ᅓቍጆᑉᔛᘚᜁᡬ᥋ᨇ᭑ᰄᴚṱἙ\u2005ℕ≻⌌␍┋☊❺⠐⥣⨯⭡Ⱞⵆ\u2e46⽽\u3031ㅫ㈥㍎㐤㔾㘣㝌㠹㥓㩪㭶㰿㵶㸢㽒䀸䄪䈿䍘䑸䕛䘫䜥"));
            return regex2.IsMatch(string_0);
        }

        private bool method_123(string string_0)
        {
            if (!(string_0 == ""))
            {
                if (!(string_0 == GClass0.smethod_0("1")))
                {
                    if (!(string_0 == GClass0.smethod_0("/ı")))
                    {
                        if (method_117(string_0) && Convert.ToDouble(string_0) == 0.0)
                        {
                            return false;
                        }
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private double method_124(bool bool_0)
        {
            return (double)(bool_0 ? 1 : 0);
        }

        private double method_125(string string_0)
        {
            if (!(string_0 == ""))
            {
                if (!(string_0 == GClass0.smethod_0("1")))
                {
                    if (!(string_0 == GClass0.smethod_0("/ı")))
                    {
                        return method_117(string_0) ? Convert.ToDouble(string_0) : 0.0;
                    }
                    return 0.0;
                }
                return 0.0;
            }
            return 0.0;
        }

        private int method_126(string string_0)
        {
            if (!(string_0 == ""))
            {
                if (!(string_0 == GClass0.smethod_0("1")))
                {
                    if (!(string_0 == GClass0.smethod_0("/ı")))
                    {
                        double num = method_117(string_0) ? Convert.ToDouble(string_0) : 0.0;
                        return (int)num;
                    }
                    return 0;
                }
                return 0;
            }
            return 0;
        }

        private string[] method_127(string[] string_0)
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

        private string method_128(string[] string_0, string string_1 = "", string string_2 = "")
        {
            string text = "";
            foreach (string text2 in string_0)
            {
                if (string_2 == "")
                {
                    if (text2 != "")
                    {
                        text = text + string_1 + text2;
                    }
                }
                else
                {
                    text = text + string_1 + method_137(text2, string_2, GClass0.smethod_0("1"));
                }
            }
            if (text.Length > string_1.Length && string_1.Length > 0)
            {
                text = MID(text, string_1.Length, -9999);
            }
            return text;
        }

        private List<string> method_129(string[] string_0)
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

        private string method_130(List<string> list_0, string string_0 = "", string string_1 = "")
        {
            string text = "";
            foreach (string item in list_0)
            {
                if (string_1 == "")
                {
                    if (item != "")
                    {
                        text = text + string_0 + item;
                    }
                }
                else
                {
                    text = text + string_0 + method_137(item, string_1, GClass0.smethod_0("1"));
                }
            }
            if (text.Length > string_0.Length && string_0.Length > 0)
            {
                text = MID(text, string_0.Length, -9999);
            }
            return text;
        }

        private List<string> method_131(string string_0, string string_1 = "\r\n")
        {
            return new List<string>(string_0.Split(new string[1]
            {
                string_1
            }, StringSplitOptions.RemoveEmptyEntries));
        }

        private string method_132(string string_0, int int_0, int int_1 = -9999)
        {
            if (string_0.Length != 0)
            {
                if (int_0 < string_0.Length)
                {
                    if (int_1 != -9999)
                    {
                        if (int_0 > int_1)
                        {
                            return "";
                        }
                        if (int_1 < 0)
                        {
                            return "";
                        }
                    }
                    int num = (int_0 >= 0) ? int_0 : 0;
                    int num2 = (int_1 == -9999) ? num : int_1;
                    if (num2 >= string_0.Length)
                    {
                        num2 = string_0.Length - 1;
                    }
                    int num3 = num;
                    int num4 = num2;
                    int length = num4 - num3 + 1;
                    return string_0.Substring(num3, length);
                }
                return "";
            }
            return "";
        }

        public string LEFT(string string_0, int int_0 = 1)
        {
            if (string_0.Length != 0)
            {
                int num = (int_0 >= 0) ? int_0 : 0;
                return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
            }
            return "";
        }

        public string MID(string string_0, int int_0, int int_1 = -9999)
        {
            if (string_0.Length != 0)
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
            return "";
        }

        private string method_133(string string_0, int int_0 = 1)
        {
            if (string_0.Length != 0)
            {
                int num = (int_0 >= 0) ? int_0 : 0;
                return string_0.Substring((num <= string_0.Length) ? (string_0.Length - num) : 0);
            }
            return "";
        }

        private string method_134(string string_0, string[] string_1, bool bool_0 = true)
        {
            string a = bool_0 ? string_0.ToUpper() : string_0;
            int num = 0;
            string text;
            while (true)
            {
                if (num >= string_1.Length)
                {
                    return "";
                }
                text = string_1[num];
                if (a == (bool_0 ? text.ToUpper() : text))
                {
                    break;
                }
                num++;
            }
            return text;
        }

        private string method_135(string string_0, string string_1, int int_0, bool bool_0 = true)
        {
            string text = string_0;
            if (text.Length < int_0)
            {
                for (int i = text.Length; i < int_0; i++)
                {
                    text = ((!bool_0) ? (text + string_1) : (string_1 + text));
                }
            }
            return text;
        }

        private string method_136(string string_0)
        {
            string text = "";
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

        private string method_137(string string_0, string string_1 = "1.0", string string_2 = "0")
        {
            string text = "";
            List<string> list = new List<string>(string_1.Split('.'));
            int num = method_126(list[0]);
            if (num <= 0)
            {
                num = 1;
            }
            string string_3 = (list.Count() > 1) ? list[1] : GClass0.smethod_0("1");
            int num2 = method_126(string_3);
            string text2 = (LEFT(string_0, 1) == GClass0.smethod_0(",")) ? GClass0.smethod_0(",") : "";
            string text3 = (text2 == "") ? string_0 : MID(string_0, 1, -9999);
            if (num2 <= 0)
            {
                text3 = method_126(text3).ToString();
                return text2 + method_135(text3, string_2, num, true);
            }
            List<string> list2 = new List<string>(text3.Split('.'));
            string string_4 = method_126(list2[0]).ToString();
            string_4 = method_135(string_4, string_2, num, true);
            string_3 = ((list2.Count() > 1) ? list2[1] : "");
            string_3 = method_135(string_3, string_2, num2, false);
            text = ((string_3.Length > 0) ? (string_4 + GClass0.smethod_0("/") + string_3) : string_4);
            return text2 + text;
        }

        private bool method_138(string string_0)
        {
            DateTime result;
            return DateTime.TryParse(string_0, out result);
        }

        private double method_139(double double_0, int int_0 = 0, int int_1 = 0, int int_2 = 45)
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
                num = 0.0 - double_0;
            }
            double num3 = 1.0;
            if (int_1 > 0)
            {
                num3 = Math.Pow(10.0, (double)int_1);
                num = Math.Truncate(num / num3 + num2) * num3;
            }
            else
            {
                num3 = Math.Pow(10.0, (double)int_0);
                num = Math.Truncate(num * num3 + num2) / num3;
            }
            if (flag)
            {
                num = 0.0 - num;
            }
            return num;
        }

        private string method_140(TimeSpan timeSpan_0, string string_0 = "C", string string_1 = "")
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
            if (string_0 == GClass0.smethod_0("B") || string_0 == GClass0.smethod_0("b"))
            {
                if (string_1 == "")
                {
                    string_1 = GClass0.smethod_0("_ňɀ\u036bѯղ");
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
                    text = (string_1.Contains(GClass0.smethod_0("L")) ? (text + str3 + GClass0.smethod_0("夨")) : ((!string_1.Contains(GClass0.smethod_0("X"))) ? (text + str + GClass0.smethod_0("夨")) : (text + (timeSpan_0.Days % 365).ToString() + GClass0.smethod_0("夨"))));
                }
                if (string_1.Contains(GClass0.smethod_0("i")))
                {
                    text = (string_1.Contains(GClass0.smethod_0("E")) ? (text + text4 + GClass0.smethod_0("對擷")) : (string_1.Contains(GClass0.smethod_0("L")) ? (text + (num4 * 24 + hours).ToString() + GClass0.smethod_0("對擷")) : ((!string_1.Contains(GClass0.smethod_0("X"))) ? (text + method_139(timeSpan_0.TotalHours, 0, 0, 45).ToString() + GClass0.smethod_0("對擷")) : (text + (timeSpan_0.Days % 365 * 24 + hours).ToString() + GClass0.smethod_0("對擷")))));
                }
                if (string_1.Contains(GClass0.smethod_0("l")))
                {
                    text = (string_1.Contains(GClass0.smethod_0("i")) ? (text + str4 + GClass0.smethod_0("刄閞")) : (string_1.Contains(GClass0.smethod_0("E")) ? (text + (hours * 60 + minutes).ToString() + GClass0.smethod_0("刄閞")) : (string_1.Contains(GClass0.smethod_0("L")) ? (text + (num4 * 1440 + hours * 60 + minutes).ToString() + GClass0.smethod_0("刄閞")) : ((!string_1.Contains(GClass0.smethod_0("X"))) ? (text + method_139(timeSpan_0.TotalMinutes, 0, 0, 45).ToString() + GClass0.smethod_0("刄閞")) : (text + (timeSpan_0.Days % 365 * 1440 + hours * 60 + minutes).ToString() + GClass0.smethod_0("刄閞"))))));
                }
                if (string_1.Contains(GClass0.smethod_0("r")))
                {
                    text = (string_1.Contains(GClass0.smethod_0("l")) ? (text + str5 + GClass0.smethod_0("秓")) : (string_1.Contains(GClass0.smethod_0("i")) ? (text + (minutes * 60 + seconds).ToString() + GClass0.smethod_0("秓")) : (string_1.Contains(GClass0.smethod_0("E")) ? (text + (hours * 3600 + minutes * 60 + seconds).ToString() + GClass0.smethod_0("秓")) : (string_1.Contains(GClass0.smethod_0("L")) ? (text + (num4 * 86400 + hours * 3600 + minutes * 60 + seconds).ToString() + GClass0.smethod_0("秓")) : ((!string_1.Contains(GClass0.smethod_0("X"))) ? (text + method_139(timeSpan_0.TotalSeconds, 0, 0, 45).ToString() + GClass0.smethod_0("秓")) : (text + (timeSpan_0.Days % 365 * 86400 + hours * 60 + minutes * 60 + seconds).ToString() + GClass0.smethod_0("秓")))))));
                }
            }
            else if (string_0 == GClass0.smethod_0("D") || string_0 == GClass0.smethod_0("d"))
            {
                if (string_1 == "")
                {
                    string_1 = GClass0.smethod_0("_ňɀ\u036bѯղ");
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
                    text = (string_1.Contains(GClass0.smethod_0("L")) ? (text + str3) : ((!string_1.Contains(GClass0.smethod_0("X"))) ? (text + str) : (text + (timeSpan_0.Days % 365).ToString())));
                }
                text = ((text == GClass0.smethod_0("-Į")) ? "" : text);
                string str6 = GClass0.smethod_0("3ĲȻ");
                if (string_1.Contains(GClass0.smethod_0("i")))
                {
                    str6 = (string_1.Contains(GClass0.smethod_0("E")) ? (text4 + GClass0.smethod_0(";")) : (string_1.Contains(GClass0.smethod_0("L")) ? ((num4 * 24 + hours).ToString() + GClass0.smethod_0(";")) : ((!string_1.Contains(GClass0.smethod_0("X"))) ? (method_139(timeSpan_0.TotalHours, 0, 0, 45).ToString() + GClass0.smethod_0(";")) : ((timeSpan_0.Days % 365 * 24 + hours).ToString() + GClass0.smethod_0(";")))));
                }
                str6 = ((!string_1.Contains(GClass0.smethod_0("l"))) ? (str6 + GClass0.smethod_0("3ĲȻ")) : (string_1.Contains(GClass0.smethod_0("i")) ? (str6 + str4 + GClass0.smethod_0(";")) : (string_1.Contains(GClass0.smethod_0("E")) ? (str6 + (hours * 60 + minutes).ToString() + GClass0.smethod_0(";")) : (string_1.Contains(GClass0.smethod_0("L")) ? (str6 + (num4 * 1440 + hours * 60 + minutes).ToString() + GClass0.smethod_0(";")) : ((!string_1.Contains(GClass0.smethod_0("X"))) ? (str6 + method_139(timeSpan_0.TotalMinutes, 0, 0, 45).ToString() + GClass0.smethod_0(";")) : (str6 + (timeSpan_0.Days % 365 * 1440 + hours * 60 + minutes).ToString() + GClass0.smethod_0(";")))))));
                str6 = ((!string_1.Contains(GClass0.smethod_0("r"))) ? (str6 + GClass0.smethod_0("2ı")) : (string_1.Contains(GClass0.smethod_0("l")) ? (str6 + str5) : (string_1.Contains(GClass0.smethod_0("i")) ? (str6 + (minutes * 60 + seconds).ToString()) : (string_1.Contains(GClass0.smethod_0("E")) ? (str6 + (hours * 3600 + minutes * 60 + seconds).ToString()) : (string_1.Contains(GClass0.smethod_0("L")) ? (str6 + (num4 * 86400 + hours * 3600 + minutes * 60 + seconds).ToString()) : ((!string_1.Contains(GClass0.smethod_0("X"))) ? (str6 + method_139(timeSpan_0.TotalSeconds, 0, 0, 45).ToString()) : (str6 + (timeSpan_0.Days % 365 * 86400 + hours * 60 + minutes * 60 + seconds).ToString())))))));
                text = ((str6 == GClass0.smethod_0("8ķȼ\u0335дԹز\u0731")) ? text : (text + GClass0.smethod_0("!") + str6));
            }
            else
            {
                int int_ = (!method_117(string_1)) ? 1 : Convert.ToInt32(string_1);
                if (string_0 == GClass0.smethod_0("X"))
                {
                    text = method_139((double)timeSpan_0.Days / 365.0, int_, 0, 45).ToString();
                }
                else if (string_0 == GClass0.smethod_0("L"))
                {
                    text = ((double)(num * 12) + method_139(((double)timeSpan_0.Days % 365.0 - (double)(num2 / 2)) / 30.0, int_, 0, 45)).ToString();
                }
                else if (string_0 == GClass0.smethod_0("E"))
                {
                    text = method_139(timeSpan_0.TotalDays, int_, 0, 45).ToString();
                }
                else if (string_0 == GClass0.smethod_0("V"))
                {
                    text = method_139(timeSpan_0.TotalDays / 7.0, int_, 0, 45).ToString();
                }
                else if (string_0 == GClass0.smethod_0("i"))
                {
                    text = method_139(timeSpan_0.TotalHours, int_, 0, 45).ToString();
                }
                else if (string_0 == GClass0.smethod_0("l"))
                {
                    text = method_139(timeSpan_0.TotalMinutes, int_, 0, 45).ToString();
                }
                else if (string_0 == GClass0.smethod_0("r"))
                {
                    text = method_139(timeSpan_0.TotalSeconds, int_, 0, 45).ToString();
                }
            }
            return text;
        }

        public string CleanString(string string_0)
        {
            string text = string_0.Replace(GClass0.smethod_0("、"), GClass0.smethod_0("!"));
            text = text.Trim();
            text = text.Replace('\n', ' ');
            text = text.Replace('\r', ' ');
            text = text.Replace('\t', ' ');
            text = text.Replace(GClass0.smethod_0("Ａ"), GClass0.smethod_0("A"));
            text = text.Replace(GClass0.smethod_0("１"), GClass0.smethod_0("1"));
            text = text.Replace(GClass0.smethod_0("０"), GClass0.smethod_0("0"));
            text = text.Replace(GClass0.smethod_0("３"), GClass0.smethod_0("3"));
            text = text.Replace(GClass0.smethod_0("２"), GClass0.smethod_0("2"));
            text = text.Replace(GClass0.smethod_0("５"), GClass0.smethod_0("5"));
            text = text.Replace(GClass0.smethod_0("４"), GClass0.smethod_0("4"));
            text = text.Replace(GClass0.smethod_0("７"), GClass0.smethod_0("7"));
            text = text.Replace(GClass0.smethod_0("６"), GClass0.smethod_0("6"));
            text = text.Replace(GClass0.smethod_0("９"), GClass0.smethod_0("9"));
            text = text.Replace(GClass0.smethod_0("８"), GClass0.smethod_0("8"));
            text = text.Replace(GClass0.smethod_0("＠"), "A");
            text = text.Replace(GClass0.smethod_0("Ｃ"), "B");
            text = text.Replace(GClass0.smethod_0("Ｂ"), GClass0.smethod_0("B"));
            text = text.Replace(GClass0.smethod_0("Ｅ"), GClass0.smethod_0("E"));
            text = text.Replace(GClass0.smethod_0("Ｄ"), GClass0.smethod_0("D"));
            text = text.Replace(GClass0.smethod_0("Ｇ"), GClass0.smethod_0("G"));
            text = text.Replace(GClass0.smethod_0("Ｆ"), GClass0.smethod_0("F"));
            text = text.Replace(GClass0.smethod_0("Ｉ"), GClass0.smethod_0("I"));
            text = text.Replace(GClass0.smethod_0("Ｈ"), GClass0.smethod_0("H"));
            text = text.Replace(GClass0.smethod_0("Ｋ"), GClass0.smethod_0("K"));
            text = text.Replace(GClass0.smethod_0("Ｊ"), GClass0.smethod_0("J"));
            text = text.Replace(GClass0.smethod_0("Ｍ"), GClass0.smethod_0("M"));
            text = text.Replace(GClass0.smethod_0("Ｌ"), GClass0.smethod_0("L"));
            text = text.Replace(GClass0.smethod_0("Ｏ"), GClass0.smethod_0("O"));
            text = text.Replace(GClass0.smethod_0("Ｎ"), GClass0.smethod_0("N"));
            text = text.Replace(GClass0.smethod_0("Ｑ"), GClass0.smethod_0("Q"));
            text = text.Replace(GClass0.smethod_0("Ｐ"), GClass0.smethod_0("P"));
            text = text.Replace(GClass0.smethod_0("Ｓ"), GClass0.smethod_0("S"));
            text = text.Replace(GClass0.smethod_0("Ｒ"), GClass0.smethod_0("R"));
            text = text.Replace(GClass0.smethod_0("Ｕ"), GClass0.smethod_0("U"));
            text = text.Replace(GClass0.smethod_0("Ｔ"), GClass0.smethod_0("T"));
            text = text.Replace(GClass0.smethod_0("Ｗ"), GClass0.smethod_0("W"));
            text = text.Replace(GClass0.smethod_0("Ｖ"), GClass0.smethod_0("V"));
            text = text.Replace(GClass0.smethod_0("Ｙ"), GClass0.smethod_0("Y"));
            text = text.Replace(GClass0.smethod_0("Ｘ"), GClass0.smethod_0("X"));
            text = text.Replace(GClass0.smethod_0("［"), GClass0.smethod_0("["));
            text = text.Replace(GClass0.smethod_0("\uff40"), GClass0.smethod_0("`"));
            text = text.Replace(GClass0.smethod_0("ｃ"), GClass0.smethod_0("c"));
            text = text.Replace(GClass0.smethod_0("ｂ"), GClass0.smethod_0("b"));
            text = text.Replace(GClass0.smethod_0("ｅ"), GClass0.smethod_0("e"));
            text = text.Replace(GClass0.smethod_0("ｄ"), GClass0.smethod_0("d"));
            text = text.Replace(GClass0.smethod_0("ｇ"), GClass0.smethod_0("g"));
            text = text.Replace(GClass0.smethod_0("ｆ"), GClass0.smethod_0("f"));
            text = text.Replace(GClass0.smethod_0("ｉ"), GClass0.smethod_0("i"));
            text = text.Replace(GClass0.smethod_0("ｈ"), GClass0.smethod_0("h"));
            text = text.Replace(GClass0.smethod_0("ｋ"), GClass0.smethod_0("k"));
            text = text.Replace(GClass0.smethod_0("ｊ"), GClass0.smethod_0("j"));
            text = text.Replace(GClass0.smethod_0("ｍ"), GClass0.smethod_0("m"));
            text = text.Replace(GClass0.smethod_0("ｌ"), GClass0.smethod_0("l"));
            text = text.Replace(GClass0.smethod_0("ｏ"), GClass0.smethod_0("o"));
            text = text.Replace(GClass0.smethod_0("ｎ"), GClass0.smethod_0("n"));
            text = text.Replace(GClass0.smethod_0("ｑ"), GClass0.smethod_0("q"));
            text = text.Replace(GClass0.smethod_0("ｐ"), GClass0.smethod_0("p"));
            text = text.Replace(GClass0.smethod_0("ｓ"), GClass0.smethod_0("s"));
            text = text.Replace(GClass0.smethod_0("ｒ"), GClass0.smethod_0("r"));
            text = text.Replace(GClass0.smethod_0("ｕ"), GClass0.smethod_0("u"));
            text = text.Replace(GClass0.smethod_0("ｔ"), GClass0.smethod_0("t"));
            text = text.Replace(GClass0.smethod_0("ｗ"), GClass0.smethod_0("w"));
            text = text.Replace(GClass0.smethod_0("ｖ"), GClass0.smethod_0("v"));
            text = text.Replace(GClass0.smethod_0("ｙ"), GClass0.smethod_0("y"));
            text = text.Replace(GClass0.smethod_0("ｘ"), GClass0.smethod_0("x"));
            return text.Replace(GClass0.smethod_0("｛"), GClass0.smethod_0("{"));
        }

        private List<string> method_141(List<string> list_0, bool bool_0 = false)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < list_0.Count; i++)
            {
                list.Add(method_125(list_0[i]));
            }
            for (int j = 0; j < list_0.Count - 1; j++)
            {
                int num = j;
                for (int k = j + 1; k < list_0.Count; k++)
                {
                    if (bool_0)
                    {
                        if (list[num] < list[k])
                        {
                            num = k;
                        }
                        else if (list[num] == list[k] && list_0[num].CompareTo(list_0[k]) < 0)
                        {
                            num = k;
                        }
                    }
                    else if (list[num] > list[k])
                    {
                        num = k;
                    }
                    else if (list[num] == list[k] && list_0[num].CompareTo(list_0[k]) > 0)
                    {
                        num = k;
                    }
                }
                if (j != num)
                {
                    double value = list[j];
                    list[j] = list[num];
                    list[num] = value;
                    string value2 = list_0[j];
                    list_0[j] = list_0[num];
                    list_0[num] = value2;
                }
            }
            return list_0;
        }

        private string method_142(string string_0)
        {
            string text = string_0.Trim();
            if (text.Length > 0)
            {
                int num = text.IndexOf(GClass0.smethod_0("/"));
                text = ((num <= -1) ? "" : text.Substring(num + 1, text.Length - num - 1));
            }
            return text;
        }

        private string method_143(double double_0)
        {
            string text = (double_0 == 0.0) ? "" : double_0.ToString();
            if (text.Length > 0)
            {
                int num = text.IndexOf(GClass0.smethod_0("/"));
                text = ((num <= -1) ? "" : text.Substring(num + 1, text.Length - num - 1));
            }
            return text;
        }

        private double method_144(double double_0)
        {
            string text = double_0.ToString();
            int num = text.IndexOf(GClass0.smethod_0("/"));
            text = ((num <= -1) ? GClass0.smethod_0("1") : (GClass0.smethod_0("2į") + text.Substring(num + 1, text.Length - num - 1)));
            return Convert.ToDouble(text);
        }

        public double MathCalculation(string string_0)
        {
            string string_ = DeleteOuterSymbol(string_0);
            List<string> list = ParaToList(string_, GClass0.smethod_0(":"), false);
            foreach (string item in list)
            {
                OutputResult(GClass0.smethod_0("5Ŋɕ\u0338慵幢觓粕\uf71b") + item, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                int formulaSplitLocation = GetFormulaSplitLocation(item, GClass0.smethod_0(";"));
                if (formulaSplitLocation <= -1)
                {
                    method_101(item, 0);
                    return method_12(item, GClass0.smethod_0("*"), 0.0);
                }
                string string_2 = LEFT(item, formulaSplitLocation);
                string string_3 = MID(item, formulaSplitLocation + 1, -9999);
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                if (logicExplain.LoopLogicFormula(string_2) > 0.0)
                {
                    method_101(string_3, 0);
                    return method_12(string_3, GClass0.smethod_0("*"), 0.0);
                }
            }
            return 0.0;
        }

        public string SimpleCalculation(string string_0)
        {
            string string_ = DeleteOuterSymbol(string_0);
            List<string> list = ParaToList(string_, GClass0.smethod_0(":"), false);
            foreach (string item in list)
            {
                OutputResult(GClass0.smethod_0("6ŋɚ\u0339彑縣䠶裓玕\uf61b") + item, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                int formulaSplitLocation = GetFormulaSplitLocation(item, GClass0.smethod_0(";"));
                if (formulaSplitLocation <= -1)
                {
                    return method_5(item);
                }
                string string_2 = LEFT(item, formulaSplitLocation);
                string string_3 = MID(item, formulaSplitLocation + 1, -9999);
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                if (logicExplain.LoopLogicFormula(string_2) > 0.0)
                {
                    return method_5(string_3);
                }
            }
            return "";
        }

        public string OptionCalculation(string string_0)
        {
            string string_ = DeleteOuterSymbol(string_0);
            List<string> list = ParaToList(string_, GClass0.smethod_0(":"), false);
            foreach (string item in list)
            {
                OutputResult(GClass0.smethod_0("6ŋɚ\u0339彑縣䠶裓玕\uf61b") + item, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                int formulaSplitLocation = GetFormulaSplitLocation(item, GClass0.smethod_0(";"));
                if (formulaSplitLocation <= -1)
                {
                    return method_0(item);
                }
                string string_2 = LEFT(item, formulaSplitLocation);
                string string_3 = MID(item, formulaSplitLocation + 1, -9999);
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                if (logicExplain.LoopLogicFormula(string_2) > 0.0)
                {
                    return method_0(string_3);
                }
            }
            return "";
        }

        public string ComplexRouteFormula(string string_0)
        {
            string string_ = DeleteOuterSymbol(string_0);
            List<string> list = ParaToList(string_, GClass0.smethod_0(":"), false);
            foreach (string item in list)
            {
                OutputResult(GClass0.smethod_0("3Ōɟ\u0332эԻع\u073a儊湄蟪縵菓皕\uf11b") + item, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                int formulaSplitLocation = GetFormulaSplitLocation(item, GClass0.smethod_0(";"));
                if (formulaSplitLocation <= -1)
                {
                    return item;
                }
                string string_2 = LEFT(item, formulaSplitLocation);
                string result = MID(item, formulaSplitLocation + 1, -9999);
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                if (logicExplain.LoopLogicFormula(string_2) > 0.0)
                {
                    return result;
                }
            }
            return "";
        }

        public string TextFormula(string string_0)
        {
            method_101(string_0, 1);
            return method_1(string_0, false);
        }

        public double LoopLogicFormula(string string_0)
        {
            string string_ = DeleteOuterSymbol(string_0);
            List<string> list = ParaToList(string_, GClass0.smethod_0(":"), false);
            foreach (string item in list)
            {
                OutputResult(GClass0.smethod_0("5Ŋɕ\u0338鐾誕觓粕\uf71b") + item, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                int formulaSplitLocation = GetFormulaSplitLocation(item, GClass0.smethod_0(";"));
                if (formulaSplitLocation <= -1)
                {
                    method_101(item, 0);
                    return method_7(item);
                }
                string string_2 = LEFT(item, formulaSplitLocation);
                string string_3 = MID(item, formulaSplitLocation + 1, -9999);
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.SetData(_dictData, false);
                if (logicExplain.LoopLogicFormula(string_2) > 0.0)
                {
                    method_101(string_3, 0);
                    return method_7(string_3);
                }
            }
            return 0.0;
        }

        public List<string> listLoopLevel(string string_0, string string_1, string string_2 = "", string string_3 = "_R", string string_4 = "_R")
        {
            List<string> list = new List<string>();
            method_101(string_0, 0);
            double num = method_7(string_0);
            if (num > 0.0)
            {
                foreach (classLoopB item in _LoopQn.A)
                {
                    foreach (classLoopQuestion item2 in item.B)
                    {
                        if (item2.Qn[GClass0.smethod_0("^ŮɹͼѤճ\u064fݶࡐ\u0971\u0a77\u0b64")] == GClass0.smethod_0("0"))
                        {
                            string text = string_3 + (item2.Qn.ContainsKey(string_1) ? item2.Qn[string_1] : "");
                            if (string_2 != "")
                            {
                                text = text + string_4 + (item2.Qn.ContainsKey(string_2) ? item2.Qn[string_2] : "");
                            }
                            if (!list.Contains(text))
                            {
                                list.Add(text);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public void SetData(Dictionary<string, string> dictionary_0, bool bool_0 = true)
        {
            _dictData = dictionary_0;
            if (bool_0)
            {
                for (int num = _dictData.Count() - 1; num >= 0; num--)
                {
                    string text = _dictData.Keys.ElementAt(num);
                    string text2 = _dictData.Values.ElementAt(num);
                    if (text2 != "")
                    {
                        OutputResult(text + GClass0.smethod_0("#Ŀȡ") + text2, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                        _dictData[text] = text2;
                    }
                    else
                    {
                        _dictData.Remove(text);
                    }
                }
            }
        }

        public string CleanFormula(string string_0)
        {
            List<classHtmlText> list = SplitTextToList(string_0, GClass0.smethod_0("Yģ"), GClass0.smethod_0(" Ŝ"));
            string text = "";
            foreach (classHtmlText item in list)
            {
                if (item.TitleTextType == GClass0.smethod_0("Yģ"))
                {
                    text = text + GClass0.smethod_0("Yģ") + item.TitleText + GClass0.smethod_0(" Ŝ");
                }
                else
                {
                    text += item.TitleText.Replace(GClass0.smethod_0("!"), "").Replace(GClass0.smethod_0("、"), "");
                    text = text.Replace('\n', ' ');
                    text = text.Replace('\r', ' ');
                    text = text.Replace('\t', ' ');
                    text = text.Replace(GClass0.smethod_0("‖ℕ"), "_");
                    text = text.Replace(GClass0.smethod_0("\uff3e"), "_");
                    text = text.Replace(GClass0.smethod_0("："), GClass0.smethod_0(":"));
                    text = text.Replace(GClass0.smethod_0("〃"), GClass0.smethod_0("/"));
                    text = text.Replace(GClass0.smethod_0("／"), GClass0.smethod_0("/"));
                    text = text.Replace(GClass0.smethod_0("；"), GClass0.smethod_0(";"));
                    text = text.Replace(GClass0.smethod_0("︱"), GClass0.smethod_0(";"));
                    text = text.Replace(GClass0.smethod_0("－"), GClass0.smethod_0("-"));
                    text = text.Replace(GClass0.smethod_0("）"), GClass0.smethod_0(")"));
                    text = text.Replace(GClass0.smethod_0("（"), GClass0.smethod_0("("));
                    text = text.Replace(GClass0.smethod_0("＂"), "#");
                    text = text.Replace(GClass0.smethod_0("￤"), "#");
                    text = text.Replace(GClass0.smethod_0("＊"), GClass0.smethod_0("*"));
                    text = text.Replace(GClass0.smethod_0("，"), GClass0.smethod_0(","));
                    text = text.Replace(GClass0.smethod_0("―"), GClass0.smethod_0(","));
                    text = text.Replace(GClass0.smethod_0("＋"), GClass0.smethod_0("+"));
                    text = text.Replace(GClass0.smethod_0("．"), GClass0.smethod_0("."));
                    text = text.Replace(GClass0.smethod_0("＜"), GClass0.smethod_0("<"));
                    text = text.Replace(GClass0.smethod_0("》"), GClass0.smethod_0("="));
                    text = text.Replace(GClass0.smethod_0("《"), GClass0.smethod_0("?"));
                    text = text.Replace(GClass0.smethod_0("〉"), GClass0.smethod_0("="));
                    text = text.Replace(GClass0.smethod_0("〈"), GClass0.smethod_0("?"));
                    text = text.Replace(GClass0.smethod_0("Ｚ"), GClass0.smethod_0("Z"));
                    text = text.Replace(GClass0.smethod_0("＼"), "]");
                    text = text.Replace(GClass0.smethod_0("ｚ"), GClass0.smethod_0("z"));
                    text = text.Replace(GClass0.smethod_0("｜"), GClass0.smethod_0("|"));
                    text = text.Replace(GClass0.smethod_0("］"), GClass0.smethod_0("]"));
                    text = text.Replace(GClass0.smethod_0("＄"), GClass0.smethod_0("$"));
                    text = text.Replace(GClass0.smethod_0("Ａ"), GClass0.smethod_0("A"));
                    text = text.Replace(GClass0.smethod_0("¶"), GClass0.smethod_0("A"));
                    text = text.Replace(GClass0.smethod_0("｝"), GClass0.smethod_0("}"));
                    text = text.Replace(GClass0.smethod_0("￥"), GClass0.smethod_0("}"));
                    text = text.Replace(GClass0.smethod_0("＇"), GClass0.smethod_0("'"));
                    text = text.Replace(GClass0.smethod_0("｟"), GClass0.smethod_0("\u007f"));
                    text = text.Replace(GClass0.smethod_0("\uff3f"), GClass0.smethod_0("_"));
                    text = text.Replace(GClass0.smethod_0("＞"), GClass0.smethod_0(">"));
                    text = text.Replace(GClass0.smethod_0("’"), GClass0.smethod_0("&"));
                    text = text.Replace(GClass0.smethod_0("‘"), GClass0.smethod_0("&"));
                    text = text.Replace(GClass0.smethod_0("”"), GClass0.smethod_0("#"));
                    text = text.Replace(GClass0.smethod_0("“"), GClass0.smethod_0("#"));
                    text = text.Replace(GClass0.smethod_0("１"), GClass0.smethod_0("1"));
                    text = text.Replace(GClass0.smethod_0("０"), GClass0.smethod_0("0"));
                    text = text.Replace(GClass0.smethod_0("３"), GClass0.smethod_0("3"));
                    text = text.Replace(GClass0.smethod_0("２"), GClass0.smethod_0("2"));
                    text = text.Replace(GClass0.smethod_0("５"), GClass0.smethod_0("5"));
                    text = text.Replace(GClass0.smethod_0("４"), GClass0.smethod_0("4"));
                    text = text.Replace(GClass0.smethod_0("７"), GClass0.smethod_0("7"));
                    text = text.Replace(GClass0.smethod_0("６"), GClass0.smethod_0("6"));
                    text = text.Replace(GClass0.smethod_0("９"), GClass0.smethod_0("9"));
                    text = text.Replace(GClass0.smethod_0("８"), GClass0.smethod_0("8"));
                    text = text.Replace(GClass0.smethod_0("＠"), "A");
                    text = text.Replace(GClass0.smethod_0("Ｃ"), "B");
                    text = text.Replace(GClass0.smethod_0("Ｂ"), GClass0.smethod_0("B"));
                    text = text.Replace(GClass0.smethod_0("Ｅ"), GClass0.smethod_0("E"));
                    text = text.Replace(GClass0.smethod_0("Ｄ"), GClass0.smethod_0("D"));
                    text = text.Replace(GClass0.smethod_0("Ｇ"), GClass0.smethod_0("G"));
                    text = text.Replace(GClass0.smethod_0("Ｆ"), GClass0.smethod_0("F"));
                    text = text.Replace(GClass0.smethod_0("Ｉ"), GClass0.smethod_0("I"));
                    text = text.Replace(GClass0.smethod_0("Ｈ"), GClass0.smethod_0("H"));
                    text = text.Replace(GClass0.smethod_0("Ｋ"), GClass0.smethod_0("K"));
                    text = text.Replace(GClass0.smethod_0("Ｊ"), GClass0.smethod_0("J"));
                    text = text.Replace(GClass0.smethod_0("Ｍ"), GClass0.smethod_0("M"));
                    text = text.Replace(GClass0.smethod_0("Ｌ"), GClass0.smethod_0("L"));
                    text = text.Replace(GClass0.smethod_0("Ｏ"), GClass0.smethod_0("O"));
                    text = text.Replace(GClass0.smethod_0("Ｎ"), GClass0.smethod_0("N"));
                    text = text.Replace(GClass0.smethod_0("Ｑ"), GClass0.smethod_0("Q"));
                    text = text.Replace(GClass0.smethod_0("Ｐ"), GClass0.smethod_0("P"));
                    text = text.Replace(GClass0.smethod_0("Ｓ"), GClass0.smethod_0("S"));
                    text = text.Replace(GClass0.smethod_0("Ｒ"), GClass0.smethod_0("R"));
                    text = text.Replace(GClass0.smethod_0("Ｕ"), GClass0.smethod_0("U"));
                    text = text.Replace(GClass0.smethod_0("Ｔ"), GClass0.smethod_0("T"));
                    text = text.Replace(GClass0.smethod_0("Ｗ"), GClass0.smethod_0("W"));
                    text = text.Replace(GClass0.smethod_0("Ｖ"), GClass0.smethod_0("V"));
                    text = text.Replace(GClass0.smethod_0("Ｙ"), GClass0.smethod_0("Y"));
                    text = text.Replace(GClass0.smethod_0("Ｘ"), GClass0.smethod_0("X"));
                    text = text.Replace(GClass0.smethod_0("［"), GClass0.smethod_0("["));
                    text = text.Replace(GClass0.smethod_0("\uff40"), GClass0.smethod_0("`"));
                    text = text.Replace(GClass0.smethod_0("ｃ"), GClass0.smethod_0("c"));
                    text = text.Replace(GClass0.smethod_0("ｂ"), GClass0.smethod_0("b"));
                    text = text.Replace(GClass0.smethod_0("ｅ"), GClass0.smethod_0("e"));
                    text = text.Replace(GClass0.smethod_0("ｄ"), GClass0.smethod_0("d"));
                    text = text.Replace(GClass0.smethod_0("ｇ"), GClass0.smethod_0("g"));
                    text = text.Replace(GClass0.smethod_0("ｆ"), GClass0.smethod_0("f"));
                    text = text.Replace(GClass0.smethod_0("ｉ"), GClass0.smethod_0("i"));
                    text = text.Replace(GClass0.smethod_0("ｈ"), GClass0.smethod_0("h"));
                    text = text.Replace(GClass0.smethod_0("ｋ"), GClass0.smethod_0("k"));
                    text = text.Replace(GClass0.smethod_0("ｊ"), GClass0.smethod_0("j"));
                    text = text.Replace(GClass0.smethod_0("ｍ"), GClass0.smethod_0("m"));
                    text = text.Replace(GClass0.smethod_0("ｌ"), GClass0.smethod_0("l"));
                    text = text.Replace(GClass0.smethod_0("ｏ"), GClass0.smethod_0("o"));
                    text = text.Replace(GClass0.smethod_0("ｎ"), GClass0.smethod_0("n"));
                    text = text.Replace(GClass0.smethod_0("ｑ"), GClass0.smethod_0("q"));
                    text = text.Replace(GClass0.smethod_0("ｐ"), GClass0.smethod_0("p"));
                    text = text.Replace(GClass0.smethod_0("ｓ"), GClass0.smethod_0("s"));
                    text = text.Replace(GClass0.smethod_0("ｒ"), GClass0.smethod_0("r"));
                    text = text.Replace(GClass0.smethod_0("ｕ"), GClass0.smethod_0("u"));
                    text = text.Replace(GClass0.smethod_0("ｔ"), GClass0.smethod_0("t"));
                    text = text.Replace(GClass0.smethod_0("ｗ"), GClass0.smethod_0("w"));
                    text = text.Replace(GClass0.smethod_0("ｖ"), GClass0.smethod_0("v"));
                    text = text.Replace(GClass0.smethod_0("ｙ"), GClass0.smethod_0("y"));
                    text = text.Replace(GClass0.smethod_0("ｘ"), GClass0.smethod_0("x"));
                    text = text.Replace(GClass0.smethod_0("｛"), GClass0.smethod_0("{"));
                }
            }
            return text;
        }

        public string CleanTextFormula(string string_0)
        {
            string[] string_ = new string[1]
            {
                GClass0.smethod_0("?ŀȿ")
            };
            string[] string_2 = new string[2]
            {
                GClass0.smethod_0("8Ĭɀ\u033f"),
                GClass0.smethod_0("8Łɐ\u033f")
            };
            string[] string_3 = new string[1]
            {
                GClass0.smethod_0("!ŋɗ\u034aЩ")
            };
            string[] string_4 = new string[1]
            {
                GClass0.smethod_0("#ŉɑ\u034cцՐة")
            };
            string text = "";
            string text2 = string_0.Trim();
            char c = '\t';
            char c2 = '\n';
            char c3 = '\r';
            text2 = text2.Replace(c.ToString(), "");
            text2 = text2.Replace(c2.ToString(), "");
            text2 = text2.Replace(c3.ToString(), "");
            if (text2.Length > 0)
            {
                int num = 0;
                do
                {
                    if (text2.Length > 7)
                    {
                        string text3 = method_134(MID(text2, num, 7), string_4, true);
                        if (text3 != "")
                        {
                            text += text3;
                            num += 7;
                            continue;
                        }
                    }
                    if (text2.Length > 5)
                    {
                        string text3 = method_134(MID(text2, num, 5), string_3, true);
                        if (text3 != "")
                        {
                            text += text3;
                            num += 5;
                            continue;
                        }
                    }
                    if (text2.Length > 4)
                    {
                        string text3 = method_134(MID(text2, num, 4), string_2, true);
                        if (text3 != "")
                        {
                            text += text3;
                            num += 4;
                            continue;
                        }
                    }
                    if (text2.Length > 3)
                    {
                        string text3 = method_134(MID(text2, num, 3), string_, true);
                        if (text3 != "")
                        {
                            text += text3;
                            num += 3;
                            continue;
                        }
                    }
                    text += MID(text2, num, 1);
                    num++;
                }
                while (num < text2.Length);
            }
            return text;
        }

        public string JoinQnName(string string_0, string string_1)
        {
            string result = "";
            string text = GClass0.smethod_0("bĻɼ\u0331бԴطܫ\u0828नਸ୳ష൯๐པၓᄥሥ፰ᑷᔳᘳᜩᠧ\u1921ᨦᬤᱞ\u1d3e");
            string text2 = GClass0.smethod_0(":ĸȺ\u0334в\u0530ز\u0734࠺स");
            if (text.IndexOf(string_0) == -1)
            {
                if (text2.IndexOf(string_0) > -1)
                {
                    if (string_1.Length > 0)
                    {
                        result = string_1 + string_0;
                    }
                }
                else
                {
                    result = string_1 + string_0;
                }
            }
            return result;
        }

        public List<classHtmlText> SplitTextToList(string string_0, string string_1, string string_2)
        {
            List<classHtmlText> list = new List<classHtmlText>();
            string text = "";
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            do
            {
                text = MID(string_0, num, string_1.Length);
                if (text == string_1)
                {
                    if (num3 < num)
                    {
                        classHtmlText item = new classHtmlText();
                        list.Add(item);
                        list[num2].TitleText = method_132(string_0, num3, num - 1);
                        list[num2].TitleTextType = "";
                        num2++;
                    }
                    num3 = num + string_1.Length;
                    num = string_0.IndexOf(string_2, num3);
                    if (num < 0)
                    {
                        num = string_0.Length;
                    }
                    if (num3 < num)
                    {
                        classHtmlText item2 = new classHtmlText();
                        list.Add(item2);
                        list[num2].TitleText = method_132(string_0, num3, num - 1);
                        list[num2].TitleTextType = string_1;
                        num2++;
                    }
                    num3 = num + string_2.Length;
                    num = num3 - 1;
                }
                num++;
            }
            while (num < string_0.Length);
            if (num3 < string_0.Length)
            {
                classHtmlText item3 = new classHtmlText();
                list.Add(item3);
                list[num2].TitleText = method_132(string_0, num3, string_0.Length - 1);
                list[num2].TitleTextType = "";
            }
            return list;
        }

        public string GetTextQuestion(string string_0, string string_1 = "")
        {
            string text = "";
            if (method_89(string_0))
            {
                text = GClass0.smethod_0("!");
            }
            int num = 0;
            int num2 = 0;
            string text2 = "";
            string text3 = "";
            do
            {
                text2 = MID(string_0, num, 2);
                if (text2 == "#[" || text2 == "$[")
                {
                    num2 = RightBrackets(string_0, num, text2, "]");
                    num += 2;
                    text3 = method_132(string_0, num, num2 - 1);
                    text = text + GClass0.smethod_0("$Ś") + text3 + "]";
                    num = num2 + 1;
                }
                else if (text2 == GClass0.smethod_0("!ź") || text2 == GClass0.smethod_0("&ź"))
                {
                    num2 = RightBrackets(string_0, num, text2, GClass0.smethod_0("|"));
                    num += 2;
                    text3 = method_132(string_0, num, num2 - 1);
                    text = text + GClass0.smethod_0("$Ś") + text3 + "]";
                    num = num2 + 1;
                }
                else if (text2 == GClass0.smethod_0("$ź"))
                {
                    num2 = RightBrackets(string_0, num, text2, GClass0.smethod_0("|"));
                    num += 2;
                    text3 = method_132(string_0, num, num2 - 1);
                    num = num2 + 1;
                    string text4 = "";
                    List<string> list = ParaToList(text3, GClass0.smethod_0(":"), false);
                    foreach (string item in list)
                    {
                        num2 = item.IndexOf(GClass0.smethod_0(";"));
                        if (num2 >= 0)
                        {
                            text = text + GClass0.smethod_0("$Ś") + LEFT(item, num2) + "]";
                            text4 = GetTextQuestion(MID(item, num2 + 1, -9999), "");
                            if (text4.Length > 0)
                            {
                                text = text + GClass0.smethod_0("'") + text4;
                            }
                        }
                        else
                        {
                            text4 = GetTextQuestion(item, "");
                            if (text4.Length > 0)
                            {
                                text = text + GClass0.smethod_0("'") + text4;
                            }
                        }
                    }
                }
                else
                {
                    text2 = MID(string_0, num, 1);
                    if (text2 == GClass0.smethod_0("%"))
                    {
                        num2 = RightBrackets(string_0, num, GClass0.smethod_0(")"), GClass0.smethod_0("("));
                        text3 = method_132(string_0, num, num2);
                        text = text + GClass0.smethod_0("$Ś") + text3 + "]";
                        num = num2 + 1;
                    }
                    else
                    {
                        num++;
                    }
                }
            }
            while (num < string_0.Length);
            if (LEFT(text, 1) == GClass0.smethod_0("'"))
            {
                text = MID(text, 1, -9999);
            }
            return text;
        }

        public List<string> ParaToList(string string_0, string string_1 = ",", bool bool_0 = false)
        {
            List<string> list = new List<string>();
            int length = string_1.Length;
            string text = string_0;
            while (LEFT(text, length) == string_1)
            {
                list.Add("");
                text = MID(text, length, -9999);
            }
            int num = 0;
            int num2 = 0;
            do
            {
                string a = MID(text, num, 2);
                if (!(a == GClass0.smethod_0("Yģ")))
                {
                    a = MID(text, num, 1);
                    string a2 = MID(text, num, length);
                    if (a == GClass0.smethod_0("Z"))
                    {
                        num = RightBrackets(text, num, GClass0.smethod_0("Z"), "]");
                    }
                    else if (a == GClass0.smethod_0(")"))
                    {
                        num = RightBrackets(text, num, GClass0.smethod_0(")"), GClass0.smethod_0("("));
                    }
                    else if (a == GClass0.smethod_0("z"))
                    {
                        num = RightBrackets(text, num, GClass0.smethod_0("z"), GClass0.smethod_0("|"));
                    }
                    else if (a2 == string_1)
                    {
                        a = method_132(text, num2, num - 1);
                        list.Add(bool_0 ? a.Trim() : a);
                        if (num + length == text.Length)
                        {
                            list.Add("");
                        }
                        num2 = num + length;
                        num = num2 - 1;
                    }
                }
                else
                {
                    num = text.IndexOf(GClass0.smethod_0(" Ŝ"), num + 2);
                    num = ((num >= 0) ? (num + 1) : text.Length);
                }
                num++;
            }
            while (num < text.Length);
            if (num2 < text.Length)
            {
                string a = method_132(text, num2, text.Length - 1);
                list.Add(bool_0 ? a.Trim() : a);
            }
            return list;
        }

        public void OutputResult(string string_0, string string_1 = "lg_Result.LOG", bool bool_0 = true)
        {
            string str = Environment.CurrentDirectory + GClass0.smethod_0("ZŁɥͷѣ՝");
            if (File.Exists(GClass0.smethod_0("Jķɐ\u035fѯպټݔࡃ\u0954ਪ\u0b57ౚ\u0d55")) && LEFT(string_1, 3) == GClass0.smethod_0("oťɞ"))
            {
                str = GClass0.smethod_0("Gĸɝ");
            }
            string path = str + string_1;
            StreamWriter streamWriter = new StreamWriter(path, bool_0);
            string text = (string_0 == null) ? "" : string_0;
            text = text.Replace(GClass0.smethod_0("8Łɐ\u033f"), Environment.NewLine);
            streamWriter.WriteLine(text);
            streamWriter.Close();
        }

        public void OutputDictQn(Dictionary<string, string> dictionary_0, classLoopA classLoopA_0)
        {
            OutputResult(GClass0.smethod_0("什樨加塟犂鶝烪唔聪\uf61b"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            string text = "";
            foreach (string key in dictionary_0.Keys)
            {
                text = text + GClass0.smethod_0(".ġ") + key;
            }
            if (text.Length > 0)
            {
                text = MID(text, 2, -9999);
                OutputResult(GClass0.smethod_0("晪鄙骚ﰛ") + text, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            }
            if (classLoopA_0.A.Count > 0)
            {
                OutputResult(GClass0.smethod_0("微犬骚ﰛ"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                int num = 1;
                int num2 = 1;
                foreach (classLoopB item in classLoopA_0.A)
                {
                    num2 = 1;
                    foreach (classLoopQuestion item2 in item.B)
                    {
                        text = "";
                        foreach (string key2 in item2.Qn.Keys)
                        {
                            text = text + GClass0.smethod_0(".ġ") + key2;
                        }
                        if (text.Length > 0)
                        {
                            text = MID(text, 2, -9999);
                            OutputResult("A" + num + "B" + num2 + GClass0.smethod_0("局Ļ") + text, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                        }
                        num2++;
                    }
                    num++;
                }
            }
        }

        private string method_145(string string_0, string string_1)
        {
            string string_2 = string_0;
            string a = LEFT(string_0, 1);
            if (a == GClass0.smethod_0("_") || a == "#")
            {
                string_2 = MID(string_0, 1, -9999);
            }
            LogicEngine logicEngine = new LogicEngine();
            string_2 = logicEngine.method_6(string_2, string_1);
            if (string.IsNullOrEmpty(string_2))
            {
                string_2 = "";
            }
            OutputResult(GClass0.smethod_0("导徐构搮\ufb1b") + string_0 + GClass0.smethod_0(")") + string_1 + GClass0.smethod_0("+ļ") + string_2, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            return string_2;
        }

        private string method_146(string string_0, string string_1, string string_2 = "、")
        {
            string text = "";
            LogicEngine logicEngine = new LogicEngine();
            text = logicEngine.method_9(string_0, string_1, string_2);
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            OutputResult(GClass0.smethod_0("対徒鈌魽憄戮亂") + string_0 + GClass0.smethod_0(")") + string_1 + GClass0.smethod_0("+ļ") + text, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            return text;
        }

        private string GetOtherText(string string_0, string string_1)
        {
            string text = "";
            LogicEngine logicEngine = new LogicEngine();
            string a = LEFT(string_0, 1);
            text = ((a == GClass0.smethod_0("_")) ? logicEngine.method_10(string_0.Substring(1), string_1) : ((!(a == "#")) ? logicEngine.GetOtherText(string_0, string_1) : logicEngine.method_11(string_0.Substring(1), string_1)));
            if (string.IsNullOrEmpty(text))
            {
                text = "";
            }
            OutputResult(GClass0.smethod_0("关媇滫攌\ufb1b") + string_0 + GClass0.smethod_0(")") + string_1 + GClass0.smethod_0("+ļ") + text, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            return text;
        }

        private List<string> method_147(List<string> list_0)
        {
            LogicEngine logicEngine = new LogicEngine();
            return logicEngine.method_12(list_0);
        }

        private List<string> method_148(string string_0)
        {
            LogicEngine logicEngine = new LogicEngine();
            return logicEngine.method_13(string_0);
        }
    }
}
