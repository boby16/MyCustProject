using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using System.Collections.Generic;

namespace Gssy.Capi.BIZ
{
    public class LimitBiz
    {
        private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

        private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

        public List<SurveyDetail> QDetails
        {
            get;
            set;
        }

        public List<string> LimitCodes
        {
            get;
            set;
        }

        public string LimitFirstINQName
        {
            get;
            set;
        }

        public string LimitOtherCodeText
        {
            get;
            set;
        }

        public string LimitAddFillCodeText
        {
            get;
            set;
        }

        public LimitBiz()
        {
            LimitCodes = new List<string>();
        }

        public bool LimitDetails(string string_0, int int_0, string string_1, string string_2, out string string_3)
        {
            bool result = true;
            string_3 = "";
            switch (int_0)
            {
                case 22:
                    result = method_1(string_0, int_0, string_1, string_2);
                    break;
                case 2:
                    result = method_0(string_0, int_0, string_1, string_2, out string_3);
                    break;
                case 1:
                    result = method_0(string_0, int_0, string_1, string_2, out string_3);
                    break;
                case 32:
                    result = method_3(string_0, int_0, string_1, string_2, out string_3);
                    break;
                case 30:
                    result = method_2(string_0, int_0, string_1, string_2, out string_3);
                    break;
                case 91:
                    result = method_5(string_0, int_0, string_1, string_2, out string_3);
                    break;
                case 92:
                    result = method_6(string_0, int_0, string_1, string_2);
                    break;
                case 93:
                    result = method_7(string_0, int_0, string_1, string_2);
                    break;
                case 42:
                    result = method_4(string_0, int_0, string_1, string_2, out string_3);
                    break;
            }
            return result;
        }

        private bool method_0(string string_0, int int_0, string string_1, string string_2, out string string_3)
        {
            string _003F10_003F = "";
            string text = "";
            string_3 = "";
            LimitCodes.Clear();
            string[] array = string_1.Split('|');
            foreach (string arg in array)
            {
                _003F10_003F = string.Format(GClass0.smethod_0("ûǢ\u02eaϠӧ\u05f7ڂދ\u0880\u09d9\u0acc\u0bd2\u0cd1ර\u0ec9\u0fecცᇡዳᏬᓕᗽᛡ៦ᣵ᧽\u1aaeᯚ᳄\u1dceỘῌ₨⇔⋓⏗ⓒ◆⛛⟞⣉⤻⩞⭀ⱛⴀ\u2e4a⼄たㅗ㈷㌻㐰㕓㙚㜠㠥㤪㨽㬹㰥㴤㸤㼶䀦䄦䈫䌠䑙䕄䘙䝐䠝䥸䩾䬒䰎䵻下伌倝億刂匜君唝嘍土堑夂娋孭尀崂币弌恨慠戽捴搹攜昐杤桧椟橱歯氜浪湯潼火煣牿獺瑺畬發睰硽祪税筡籥絠繯缉耏腜舗荘葻蕢蘇蜆蠀褶訾謽豝赕蹞輹遉酂鉓鍆鑀镚陝靟顏饁驏魀鱉鴷鸴鼮ꁳꄶꉻꍚꑋꕗꙊꜦ"), string_0, arg);
                List<SurveyAnswer> list = new List<SurveyAnswer>();
                list = oSurveyAnswerDal.GetListBySql(_003F10_003F);
                if (list.Count != 0)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        LimitCodes.Add(list[j].CODE.ToString());
                    }
                }
            }
            if (LimitCodes.Count != 0)
            {
                for (int k = 0; k < LimitCodes.Count; k++)
                {
                    text = ((k != LimitCodes.Count - 1) ? (text + GClass0.smethod_0("&") + LimitCodes[k].ToString() + GClass0.smethod_0("%ĭ")) : (text + GClass0.smethod_0("&") + LimitCodes[k].ToString() + GClass0.smethod_0("&")));
                }
                switch (int_0)
                {
                    case 2:
                        _003F10_003F = string.Format(GClass0.smethod_0("(Ŀȵ\u033dдԢٵݾ\u0873ऴਣ\u0b3fఢ൮พ\u0f39\u1039ᄼሬጱᐃᔣᘱᜥᠪ\u192e\u1a61\u1b37᱗\u1d5bṏὙ‛ⅾ≼⍬⑶╿♹❫⡺⥶⨌⬗ⱔⴞ\u2e50⼋》ㅫ㉧㍬㐇㕥㙪㝠㡦㤂㩯㭯㱋㴾㹔㽒䀻䄲䉢䌩䑪䔿䘵䝛䡁䥖䩔䭂䰯䵌乔伬偂兄則卍呕啙噊坖塇奇婓"), string_2, text);
                        break;
                    case 1:
                        _003F10_003F = string.Format(GClass0.smethod_0("$ĳȹ\u0331аԦٱݺ\u086fन\u0a3fଣద൪บ༽\u1035ᄰሠጽᐇᔧᘵᜡᡖᥒ\u1a1dᭋ᱓\u1d5fṋὝ‗ⅲ≰⍠⑲╻♽❯⡦⥪⨐⬋ⱐⴚ\u2e54⼏〇ㅧ㉫㍠㐃㕡㙮㝤㡚㤾㩔㭒㰻㴲㹢㼩䁪䄿䈵䍛䑁䕖䙔䝂䠯䥌䩔䬬䱂䵄乇位偕兙削卖呇啇噓"), string_2, text);
                        break;
                }
                QDetails = oSurveyDetailDal.GetListBySql(_003F10_003F);
                string _003F20_003F = array[0] + GClass0.smethod_0("[Ōɖ\u0349");
                string_3 = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F);
                return true;
            }
            return false;
        }

        private bool method_1(string string_0, int int_0, string string_1, string string_2)
        {
            string text = "";
            LimitCodes.Clear();
            string[] array = string_1.Split('|');
            foreach (string _003F20_003F in array)
            {
                text = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F);
                if (text != "")
                {
                    LimitCodes.Add(text);
                }
            }
            if (LimitCodes.Count != 0)
            {
                return true;
            }
            return false;
        }

        private bool method_2(string string_0, int int_0, string string_1, string string_2, out string string_3)
        {
            string _003F9_003F = "G_" + string_1;
            string text = "";
            string text2 = "";
            string_3 = "";
            List<SurveyDetail> details = oSurveyDetailDal.GetDetails(_003F9_003F);
            LimitCodes.Clear();
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            List<string> list4 = new List<string>();
            string _003F20_003F;
            string oneCode;
            for (int i = 0; i < details.Count; i++)
            {
                _003F20_003F = string_1 + "_R" + details[i].CODE;
                oneCode = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F);
                string a = oneCode;
                if (!(a == GClass0.smethod_0("4")))
                {
                    if (!(a == GClass0.smethod_0("5")))
                    {
                        if (!(a == GClass0.smethod_0("2")))
                        {
                            if (a == GClass0.smethod_0("3"))
                            {
                                list4.Add(details[i].CODE);
                            }
                        }
                        else
                        {
                            list3.Add(details[i].CODE);
                        }
                    }
                    else
                    {
                        list2.Add(details[i].CODE);
                    }
                }
                else
                {
                    list.Add(details[i].CODE);
                }
            }
            _003F20_003F = string_1 + GClass0.smethod_0("[őȻ\u0336");
            oneCode = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F);
            string a2 = oneCode;
            if (!(a2 == GClass0.smethod_0("4")))
            {
                if (!(a2 == GClass0.smethod_0("5")))
                {
                    if (!(a2 == GClass0.smethod_0("2")))
                    {
                        if (a2 == GClass0.smethod_0("3"))
                        {
                            list4.Add(GClass0.smethod_0(";Ķ"));
                        }
                    }
                    else
                    {
                        list3.Add(GClass0.smethod_0(";Ķ"));
                    }
                }
                else
                {
                    list2.Add(GClass0.smethod_0(";Ķ"));
                }
            }
            else
            {
                list.Add(GClass0.smethod_0(";Ķ"));
            }
            if (list.Count > 0)
            {
                LimitCodes = list;
            }
            if (LimitCodes.Count == 0 && list2.Count > 0)
            {
                LimitCodes = list2;
            }
            if (LimitCodes.Count == 0 && list3.Count > 0)
            {
                LimitCodes = list3;
            }
            if (LimitCodes.Count == 0 && list4.Count > 0)
            {
                LimitCodes = list4;
            }
            if (LimitCodes.Count != 0)
            {
                for (int j = 0; j < LimitCodes.Count; j++)
                {
                    text2 = ((j != LimitCodes.Count - 1) ? (text2 + GClass0.smethod_0("&") + LimitCodes[j].ToString() + GClass0.smethod_0("%ĭ")) : (text2 + GClass0.smethod_0("&") + LimitCodes[j].ToString() + GClass0.smethod_0("&")));
                }
                text = string.Format(GClass0.smethod_0("$ĳȹ\u0331аԦٱݺ\u086fन\u0a3fଣద൪บ༽\u1035ᄰሠጽᐇᔧᘵᜡᡖᥒ\u1a1dᭋ᱓\u1d5fṋὝ‗ⅲ≰⍠⑲╻♽❯⡦⥪⨐⬋ⱐⴚ\u2e54⼏〇ㅧ㉫㍠㐃㕡㙮㝤㡚㤾㩔㭒㰻㴲㹢㼩䁪䄿䈵䍛䑁䕖䙔䝂䠯䥌䩔䬬䱂䵄乇位偕兙削卖呇啇噓"), string_2, text2);
                QDetails = oSurveyDetailDal.GetListBySql(text);
                string _003F20_003F2 = string_1 + GClass0.smethod_0("Wŕȿ\u0332ћՌ\u0656\u0749");
                string_3 = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F2);
                return true;
            }
            return false;
        }

        private bool method_3(string string_0, int int_0, string string_1, string string_2, out string string_3)
        {
            string[] array = string_1.Split(':');
            string _003F9_003F = "G_" + array[0].ToString();
            string b = array[1].ToString();
            string str = array[0].ToString();
            string text = "";
            string text2 = "";
            string_3 = "";
            List<SurveyDetail> details = oSurveyDetailDal.GetDetails(_003F9_003F);
            LimitCodes.Clear();
            for (int i = 0; i < details.Count; i++)
            {
                string _003F20_003F = str + "_R" + details[i].CODE;
                string oneCode = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F);
                if (oneCode == b)
                {
                    LimitCodes.Add(details[i].CODE);
                }
            }
            if (LimitCodes.Count != 0)
            {
                for (int j = 0; j < LimitCodes.Count; j++)
                {
                    text2 = ((j != LimitCodes.Count - 1) ? (text2 + GClass0.smethod_0("&") + LimitCodes[j].ToString() + GClass0.smethod_0("%ĭ")) : (text2 + GClass0.smethod_0("&") + LimitCodes[j].ToString() + GClass0.smethod_0("&")));
                }
                text = string.Format(GClass0.smethod_0("(Ŀȵ\u033dдԢٵݾ\u0873ऴਣ\u0b3fఢ൮พ\u0f39\u1039ᄼሬጱᐃᔣᘱᜥᠪ\u192e\u1a61\u1b37᱗\u1d5bṏὙ‛ⅾ≼⍬⑶╿♹❫⡺⥶⨌⬗ⱔⴞ\u2e50⼋》ㅫ㉧㍬㐇㕥㙪㝠㡦㤂㩯㭯㱋㴾㹔㽒䀻䄲䉢䌩䑪䔿䘵䝛䡁䥖䩔䭂䰯䵌乔伬偂兄則卍呕啙噊坖塇奇婓"), string_2, text2);
                QDetails = oSurveyDetailDal.GetListBySql(text);
                string _003F20_003F2 = str + GClass0.smethod_0("Wŕȿ\u0332ћՌ\u0656\u0749");
                string_3 = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F2);
                return true;
            }
            return false;
        }

        private bool method_4(string string_0, int int_0, string string_1, string string_2, out string string_3)
        {
            string[] array = string_1.Split(':');
            string _003F9_003F = "G_" + array[0].ToString();
            string b = array[1].ToString();
            string str = array[0].ToString();
            string_3 = "";
            List<SurveyDetail> details = oSurveyDetailDal.GetDetails(_003F9_003F);
            LimitCodes.Clear();
            for (int i = 0; i < details.Count; i++)
            {
                string _003F20_003F = str + "_R" + details[i].CODE;
                string oneCode = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F);
                if (oneCode == b)
                {
                    LimitCodes.Add(details[i].CODE);
                }
            }
            if (LimitCodes.Count != 0)
            {
                return true;
            }
            return false;
        }

        private bool method_5(string string_0, int int_0, string string_1, string string_2, out string string_3)
        {
            string[] array = string_1.Split(':');
            string _003F9_003F = "G_" + array[0].ToString();
            array[1].ToString();
            string str = array[0].ToString();
            string text = "";
            string text2 = "";
            string_3 = "";
            List<SurveyDetail> details = oSurveyDetailDal.GetDetails(_003F9_003F);
            LimitCodes.Clear();
            for (int i = 0; i < details.Count; i++)
            {
                string _003F20_003F = str + "_R" + details[i].CODE;
                string oneCode = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F);
                string a = oneCode;
                if (!(a == GClass0.smethod_0("4")))
                {
                    if (!(a == GClass0.smethod_0("5")))
                    {
                        if (a == GClass0.smethod_0("2"))
                        {
                            LimitCodes.Add(details[i].CODE);
                        }
                    }
                    else
                    {
                        LimitCodes.Add(details[i].CODE);
                    }
                }
                else
                {
                    LimitCodes.Add(details[i].CODE);
                }
            }
            LimitCodes.Add(GClass0.smethod_0("3Ĺ"));
            if (LimitCodes.Count != 0)
            {
                for (int j = 0; j < LimitCodes.Count; j++)
                {
                    text2 = ((j != LimitCodes.Count - 1) ? (text2 + GClass0.smethod_0("&") + LimitCodes[j].ToString() + GClass0.smethod_0("%ĭ")) : (text2 + GClass0.smethod_0("&") + LimitCodes[j].ToString() + GClass0.smethod_0("&")));
                }
                text = string.Format(GClass0.smethod_0("$ĳȹ\u0331аԦٱݺ\u086fन\u0a3fଣద൪บ༽\u1035ᄰሠጽᐇᔧᘵᜡᡖᥒ\u1a1dᭋ᱓\u1d5fṋὝ‗ⅲ≰⍠⑲╻♽❯⡦⥪⨐⬋ⱐⴚ\u2e54⼏〇ㅧ㉫㍠㐃㕡㙮㝤㡚㤾㩔㭒㰻㴲㹢㼩䁪䄿䈵䍛䑁䕖䙔䝂䠯䥌䩔䬬䱂䵄乇位偕兙削卖呇啇噓"), string_2, text2);
                QDetails = oSurveyDetailDal.GetListBySql(text);
                string _003F20_003F2 = str + GClass0.smethod_0("Wŕȿ\u0332ћՌ\u0656\u0749");
                string_3 = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F2);
                return true;
            }
            return false;
        }

        private bool method_6(string string_0, int int_0, string string_1, string string_2)
        {
            string text = "";
            string text2 = "";
            LimitCodes.Clear();
            string[] array = string_1.Split('|');
            foreach (string text3 in array)
            {
                if (text3 == GClass0.smethod_0("@Ĳ"))
                {
                    text2 = string.Format(GClass0.smethod_0("\u0002ĕȃ\u030bЎԘ٫ݠ\u0869ऎਕଉఈ\u0d64ฐ\u0f37\u1033ᄶቚፇᑼᕒᙈᝍᡜ᥊\u1a17᭡\u1c7dᵱṡί‑Ⅳ≺⍼⑻╩♲❵⡠⥬⨇⬛Ⰲⵟ⸓⽟〆\u3100㉞㍐㑙㔼㙊㝏㡜㥋㩃㭟㱚㵚㹌㽜䁐䅝䉊䌮䑁䕅䙀䝏䠩䤯䩼䬷䱸䵛乂伧倦"), string_0, text3);
                    List<SurveyAnswer> list = new List<SurveyAnswer>();
                    list = oSurveyAnswerDal.GetListBySql(text2);
                    if (list.Count != 0)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            SurveyDetail surveyDetail = new SurveyDetail();
                            surveyDetail = oSurveyDetailDal.GetOne(text3, list[j].CODE.ToString());
                            text = surveyDetail.EXTEND_1;
                            if (text != "" && text != GClass0.smethod_0("3Ĺ") && text != null)
                            {
                                LimitCodes.Add(text);
                            }
                        }
                    }
                }
                else
                {
                    text = oSurveyAnswerDal.GetOneCode(string_0, text3);
                    if (text != "" && text != GClass0.smethod_0("3Ĺ") && text != null)
                    {
                        LimitCodes.Add(text);
                    }
                }
            }
            if (LimitCodes.Count != 0)
            {
                return true;
            }
            return false;
        }

        private bool method_7(string string_0, int int_0, string string_1, string string_2)
        {
            string text = "";
            string _003F10_003F = "";
            string oneCode = oSurveyAnswerDal.GetOneCode(string_0, GClass0.smethod_0("Vıɜ埃癍"));
            text = oSurveyAnswerDal.GetOneCode(string_0, string_1);
            if (text == GClass0.smethod_0("3") || text == GClass0.smethod_0("5") || text == GClass0.smethod_0("7"))
            {
                _003F10_003F = string.Format(GClass0.smethod_0("'ĶȾ\u0334гԻٮݧ\u086cभਸଦథ൧ต༰\u1036ᄵሧጸᐄᕚᙊ\u175cᡕᥗ\u1a1a\u1b4e᱐\u1d52Ṅὐ—ⅷ≷⍥⑱╦♢❲⡥⥯⨗⬎ⱓⴗ\u2e5b⼂〄ㅢ㉬㍥㐀㕜㙑㝙㡙㤦㨽㭢㰩㵪㸱㼵䁛䅁䉖䍔䑂䔯䙌䝔䠬䥂䩄䭇䱍䵕乙佊偖兇則卓"), string_2, oneCode);
            }
            if (text == GClass0.smethod_0("2") || text == GClass0.smethod_0("4") || text == GClass0.smethod_0("6"))
            {
                _003F10_003F = string.Format(GClass0.smethod_0("&ıȿ\u0337вԤٯݤ\u086dपਹଥత൨ด༳\u1037ᄲሦጻᐅᔥᙋ\u175fᡔᥐ\u1a1b\u1b4d᱑\u1d5dṅὓ―ⅰ≶⍦⑰╹♣❱⡤⥨⨖⬍ⱒⴘ\u2e5a⼁\u3005ㅥ㉭㍦㐁㕣㙐㝚㡘㤠㨥㬽㱢㴩㹪㼱䀵䅛䉁䍖䑔䕂䘯䝌䡔䤬䩂䭄䱇䵍乕余偊兖則升呓"), string_2, oneCode);
            }
            if (!(text == GClass0.smethod_0("8")) && !(text == GClass0.smethod_0("9")))
            {
                QDetails = oSurveyDetailDal.GetListBySql(_003F10_003F);
                return true;
            }
            return false;
        }

        public bool ReBuildDetails(string string_0, string string_1, string string_2, string string_3)
        {
            bool result = true;
            if (!(string_2 == GClass0.smethod_0("Kŏ")))
            {
                if (!(string_2 == GClass0.smethod_0("Lŗɕ")))
                {
                    if (!(string_2 == GClass0.smethod_0("Oŋɸ\u034cїՕ")))
                    {
                        if (string_2 == GClass0.smethod_0("CŇɴ\u0348ѓՑٸ\u074cࡗ\u0955"))
                        {
                            result = method_10(string_0, string_1, string_2, string_3);
                        }
                    }
                    else
                    {
                        result = method_9(string_0, string_1, string_2, string_3);
                    }
                }
                else
                {
                    result = method_8(string_0, string_1, string_2, string_3);
                    LimitFirstINQName = "";
                }
            }
            else
            {
                result = method_8(string_0, string_1, string_2, string_3);
                LimitFirstINQName = string_1;
            }
            return result;
        }

        private bool method_8(string string_0, string string_1, string string_2, string string_3)
        {
            string _003F10_003F = string.Format(GClass0.smethod_0("ûǢ\u02eaϠӧ\u05f7ڂދ\u0880\u09d9\u0acc\u0bd2\u0cd1ර\u0ec9\u0fecცᇡዳᏬᓕᗽᛡ៦ᣵ᧽\u1aaeᯚ᳄\u1dceỘῌ₨⇔⋓⏗ⓒ◆⛛⟞⣉⤻⩞⭀ⱛⴀ\u2e4a⼄たㅗ㈷㌻㐰㕓㙚㜠㠥㤪㨽㬹㰥㴤㸤㼶䀦䄦䈫䌠䑙䕄䘙䝐䠝䥸䩾䬒䰎䵻下伌倝億刂匜君唝嘍土堑夂娋孭尀崂币弌恨慠戽捴搹攜昐杤桧椟橱歯氜浪湯潼火煣牿獺瑺畬發睰硽祪税筡籥絠繯缉耏腜舗荘葻蕢蘇蜆蠀褶訾謽豝赕蹞輹遉酂鉓鍆鑀镚陝靟顏饁驏魀鱉鴷鸴鼮ꁳꄶꉻꍚꑋꕗꙊꜦ"), string_0, string_1);
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            list = oSurveyAnswerDal.GetListBySql(_003F10_003F);
            if (list.Count != 0)
            {
                string text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    text = ((i != list.Count - 1) ? (text + GClass0.smethod_0("&") + list[i].CODE.ToString() + GClass0.smethod_0("%ĭ")) : (text + GClass0.smethod_0("&") + list[i].CODE.ToString() + GClass0.smethod_0("&")));
                }
                if (!(string_2 == GClass0.smethod_0("Kŏ")))
                {
                    if (string_2 == GClass0.smethod_0("Lŗɕ"))
                    {
                        _003F10_003F = string.Format(GClass0.smethod_0("(Ŀȵ\u033dдԢٵݾ\u0873ऴਣ\u0b3fఢ൮พ\u0f39\u1039ᄼሬጱᐃᔣᘱᜥᠪ\u192e\u1a61\u1b37᱗\u1d5bṏὙ‛ⅾ≼⍬⑶╿♹❫⡺⥶⨌⬗ⱔⴞ\u2e50⼋》ㅫ㉧㍬㐇㕥㙪㝠㡦㤂㩯㭯㱋㴾㹔㽒䀻䄲䉢䌩䑪䔿䘵䝛䡁䥖䩔䭂䰯䵌乔伬偂兄則卍呕啙噊坖塇奇婓"), string_3, text);
                    }
                }
                else
                {
                    _003F10_003F = string.Format(GClass0.smethod_0("$ĳȹ\u0331аԦٱݺ\u086fन\u0a3fଣద൪บ༽\u1035ᄰሠጽᐇᔧᘵᜡᡖᥒ\u1a1dᭋ᱓\u1d5fṋὝ‗ⅲ≰⍠⑲╻♽❯⡦⥪⨐⬋ⱐⴚ\u2e54⼏〇ㅧ㉫㍠㐃㕡㙮㝤㡚㤾㩔㭒㰻㴲㹢㼩䁪䄿䈵䍛䑁䕖䙔䝂䠯䥌䩔䬬䱂䵄乇位偕兙削卖呇啇噓"), string_3, text);
                }
                QDetails = oSurveyDetailDal.GetListBySql(_003F10_003F);
                return true;
            }
            return false;
        }

        private bool method_9(string string_0, string string_1, string string_2, string string_3)
        {
            string[] array = string_1.Split('|');
            LimitFirstINQName = array[0];
            string _003F10_003F = string.Format(GClass0.smethod_0("ûǢ\u02eaϠӧ\u05f7ڂދ\u0880\u09d9\u0acc\u0bd2\u0cd1ර\u0ec9\u0fecცᇡዳᏬᓕᗽᛡ៦ᣵ᧽\u1aaeᯚ᳄\u1dceỘῌ₨⇔⋓⏗ⓒ◆⛛⟞⣉⤻⩞⭀ⱛⴀ\u2e4a⼄たㅗ㈷㌻㐰㕓㙚㜠㠥㤪㨽㬹㰥㴤㸤㼶䀦䄦䈫䌠䑙䕄䘙䝐䠝䥸䩾䬒䰎䵻下伌倝億刂匜君唝嘍土堑夂娋孭尀崂币弌恨慠戽捴搹攜昐杤桧椟橱歯氜浪湯潼火煣牿獺瑺畬發睰硽祪税筡籥絠繯缉耏腜舗荘葻蕢蘇蜆蠀褶訾謽豝赕蹞輹遉酂鉓鍆鑀镚陝靟顏饁驏魀鱉鴷鸴鼮ꁳꄶꉻꍚꑋꕗꙊꜦ"), string_0, array[0]);
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            list = oSurveyAnswerDal.GetListBySql(_003F10_003F);
            if (list.Count != 0)
            {
                string text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    text = ((i != list.Count - 1) ? (text + GClass0.smethod_0("&") + list[i].CODE.ToString() + GClass0.smethod_0("%ĭ")) : (text + GClass0.smethod_0("&") + list[i].CODE.ToString() + GClass0.smethod_0("&")));
                }
                _003F10_003F = string.Format(GClass0.smethod_0("$ĳȹ\u0331аԦٱݺ\u086fन\u0a3fଣద൪บ༽\u1035ᄰሠጽᐇᔧᘵᜡᡖᥒ\u1a1dᭋ᱓\u1d5fṋὝ‗ⅲ≰⍠⑲╻♽❯⡦⥪⨐⬋ⱐⴚ\u2e54⼏〇ㅧ㉫㍠㐃㕡㙮㝤㡚㤾㩔㭒㰻㴲㹢㼩䁪䄿䈵䍛䑁䕖䙔䝂䠯䥌䩔䬬䱂䵄乇位偕兙削卖呇啇噓"), string_3, text);
                List<SurveyDetail> list2 = new List<SurveyDetail>();
                list2 = oSurveyDetailDal.GetListBySql(_003F10_003F);
                string oneCode = oSurveyAnswerDal.GetOneCode(string_0, array[1]);
                bool flag = false;
                int index = 0;
                for (int j = 0; j < list2.Count; j++)
                {
                    if (oneCode == list2[j].CODE)
                    {
                        index = j;
                        flag = true;
                    }
                }
                if (flag)
                {
                    list2.RemoveAt(index);
                }
                QDetails = list2;
                return true;
            }
            return false;
        }

        private bool method_10(string string_0, string string_1, string string_2, string string_3)
        {
            string[] array = string_1.Split('|');
            LimitFirstINQName = array[0];
            string _003F10_003F = string.Format(GClass0.smethod_0("ûǢ\u02eaϠӧ\u05f7ڂދ\u0880\u09d9\u0acc\u0bd2\u0cd1ර\u0ec9\u0fecცᇡዳᏬᓕᗽᛡ៦ᣵ᧽\u1aaeᯚ᳄\u1dceỘῌ₨⇔⋓⏗ⓒ◆⛛⟞⣉⤻⩞⭀ⱛⴀ\u2e4a⼄たㅗ㈷㌻㐰㕓㙚㜠㠥㤪㨽㬹㰥㴤㸤㼶䀦䄦䈫䌠䑙䕄䘙䝐䠝䥸䩾䬒䰎䵻下伌倝億刂匜君唝嘍土堑夂娋孭尀崂币弌恨慠戽捴搹攜昐杤桧椟橱歯氜浪湯潼火煣牿獺瑺畬發睰硽祪税筡籥絠繯缉耏腜舗荘葻蕢蘇蜆蠀褶訾謽豝赕蹞輹遉酂鉓鍆鑀镚陝靟顏饁驏魀鱉鴷鸴鼮ꁳꄶꉻꍚꑋꕗꙊꜦ"), string_0, array[0]);
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            list = oSurveyAnswerDal.GetListBySql(_003F10_003F);
            if (list.Count != 0)
            {
                string text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    text = ((i != list.Count - 1) ? (text + GClass0.smethod_0("&") + list[i].CODE.ToString() + GClass0.smethod_0("%ĭ")) : (text + GClass0.smethod_0("&") + list[i].CODE.ToString() + GClass0.smethod_0("&")));
                }
                _003F10_003F = string.Format(GClass0.smethod_0("$ĳȹ\u0331аԦٱݺ\u086fन\u0a3fଣద൪บ༽\u1035ᄰሠጽᐇᔧᘵᜡᡖᥒ\u1a1dᭋ᱓\u1d5fṋὝ‗ⅲ≰⍠⑲╻♽❯⡦⥪⨐⬋ⱐⴚ\u2e54⼏〇ㅧ㉫㍠㐃㕡㙮㝤㡚㤾㩔㭒㰻㴲㹢㼩䁪䄿䈵䍛䑁䕖䙔䝂䠯䥌䩔䬬䱂䵄乇位偕兙削卖呇啇噓"), string_3, text);
                List<SurveyDetail> list2 = new List<SurveyDetail>();
                list2 = oSurveyDetailDal.GetListBySql(_003F10_003F);
                string oneCode = oSurveyAnswerDal.GetOneCode(string_0, array[1]);
                bool flag = false;
                int index = 0;
                for (int j = 0; j < list2.Count; j++)
                {
                    if (oneCode == list2[j].CODE)
                    {
                        index = j;
                        flag = true;
                    }
                }
                if (flag)
                {
                    list2.RemoveAt(index);
                }
                string oneCode2 = oSurveyAnswerDal.GetOneCode(string_0, array[2]);
                bool flag2 = false;
                int index2 = 0;
                for (int k = 0; k < list2.Count; k++)
                {
                    if (oneCode2 == list2[k].CODE)
                    {
                        index2 = k;
                        flag2 = true;
                    }
                }
                if (flag2)
                {
                    list2.RemoveAt(index2);
                }
                QDetails = list2;
                return true;
            }
            return false;
        }

        public bool ReBuildDetailsCondition(string string_0, string string_1, string string_2, string string_3, string string_4)
        {
            bool result = true;
            string oneCode = oSurveyAnswerDal.GetOneCode(string_0, string_2);
            string text = "";
            string text2 = "";
            if (string_1 == GClass0.smethod_0("Rİȳ"))
            {
                if (oneCode == GClass0.smethod_0("0"))
                {
                    text = GClass0.smethod_0("4");
                    text2 = string.Format(GClass0.smethod_0("&ıȿ\u0337вԤٯݤ\u086dपਹଥత൨ด༳\u1037ᄲሦጻᐅᔥᙋ\u175fᡔᥐ\u1a1b\u1b4d᱑\u1d5dṅὓ―ⅰ≶⍦⑰╹♣❱⡤⥨⨖⬍ⱒⴘ\u2e5a⼁\u3005ㅥ㉭㍦㐁㕣㙐㝚㡘㤠㨥㬽㱢㴩㹪㼱䀵䅛䉁䍖䑔䕂䘯䝌䡔䤬䩂䭄䱇䵍乕余偊兖則升呓"), string_4, text);
                }
                else
                {
                    text = GClass0.smethod_0("0");
                    text2 = string.Format(GClass0.smethod_0("&ıȿ\u0337вԤٯݤ\u086dपਹଥత൨ด༳\u1037ᄲሦጻᐅᔥᙋ\u175fᡔᥐ\u1a1b\u1b4d᱑\u1d5dṅὓ―ⅰ≶⍦⑰╹♣❱⡤⥨⨖⬍ⱒⴘ\u2e5a⼁\u3005ㅥ㉭㍦㐁㕣㙐㝚㡘㤠㨥㬽㱢㴩㹪㼱䀵䅛䉁䍖䑔䕂䘯䝌䡔䤬䩂䭄䱇䵍乕余偊兖則升呓"), string_4, text);
                }
                QDetails = oSurveyDetailDal.GetListBySql(text2);
                result = true;
            }
            return result;
        }

        public bool CheckHaveOther(string string_0)
        {
            bool flag = false;
            if (!(LimitFirstINQName == ""))
            {
                foreach (SurveyDetail qDetail in QDetails)
                {
                    if (qDetail.IS_OTHER == 1)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    LimitOtherCodeText = oSurveyAnswerDal.GetOneCode(string_0, LimitFirstINQName + GClass0.smethod_0("[Ōɖ\u0349"));
                }
                return flag;
            }
            return false;
        }

        public bool CheckHaveAddFill(string string_0)
        {
            bool flag = false;
            if (!(LimitFirstINQName == ""))
            {
                string str = "";
                foreach (SurveyDetail qDetail in QDetails)
                {
                    if (qDetail.IS_OTHER == 3)
                    {
                        str = qDetail.CODE;
                        flag = true;
                    }
                }
                if (flag)
                {
                    LimitAddFillCodeText = oSurveyAnswerDal.GetOneCode(string_0, LimitFirstINQName + "_A" + str + GClass0.smethod_0("[Ōɖ\u0349"));
                }
                return flag;
            }
            return false;
        }

        public string GetFixOther(string string_0, string string_1, string string_2)
        {
            string _003F10_003F = string.Format(GClass0.smethod_0("\u001ačȋ\u0303ІԐ٣ݨ\u0861आ੭ୱ\u0c70ജ\u0e68ཏ။ᅎቒፏᑴᕚᙀᝅᡔ\u1942ᨏ᭹ᱥ\u1d69ṹὯ\u2009ⅻ≲⍴⑳╡♺❽⡨⥤⨿⬣ⰺⵧ⸫⽧〾ㄸ㉖㍘㑑㔴㙂㝇㡔㥃㩛㭇㱂㵂㹔㽄䁈䅅䉂䌻䐢䕿䘲䝿䠦"), string_0, string_1);
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            list = oSurveyAnswerDal.GetListBySql(_003F10_003F);
            if (list.Count != 1)
            {
                return "";
            }
            return list[0].CODE.ToString();
        }

        public string ReplaceFullTitle(string string_0, string string_1, string string_2, string string_3)
        {
            string result = string_0;
            if (string_3 == GClass0.smethod_0("3"))
            {
                result = string_1;
            }
            return result;
        }
    }
}
