﻿using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gssy.Capi.BIZ
{
    public class LogicEngine
    {
        private UDPX oFunc = new UDPX();

        public string CircleACode = "";

        public string CircleACodeText = "";

        public int CircleACount = 0;

        public int CircleACurrent = 0;

        public string CircleBCode = "";

        public string CircleBCodeText = "";

        public int CircleBCount = 0;

        public int CircleBCurrent = 0;

        public string VersionId = GClass0.smethod_0("1");

        public List<VEAnswer> PageAnswer = new List<VEAnswer>();

        public string Logic_Message = "";

        public int IS_ALLOW_PASS = 0;

        public string NOTE = "";

        private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

        private SurveyLogicDal oSurveyLogicDal = new SurveyLogicDal();

        private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

        public string SurveyID
        {
            get;
            set;
        }

        public string NewPageId
        {
            get;
            set;
        }

        public bool CheckLogic(string string_0)
        {
            bool flag = true;
            List<SurveyLogic> list = new List<SurveyLogic>();
            list = oSurveyLogicDal.GetCheckLogic(string_0);
            foreach (SurveyLogic item in list)
            {
                if (flag)
                {
                    string fORMULA = item.FORMULA;
                    if (Result(fORMULA))
                    {
                        Logic_Message = strShowText(item.LOGIC_MESSAGE, true);
                        Logic_Message = strShowText(Logic_Message, false);
                        NOTE = item.NOTE;
                        IS_ALLOW_PASS = item.IS_ALLOW_PASS;
                        return false;
                    }
                }
            }
            return flag;
        }

        public string[] RecodeLogic(string string_0, int int_0 = 1, int int_1 = 9, int int_2 = 0, int int_3 = 99999)
        {
            Logic_Message = "";
            int num = int_1;
            if (int_1 == 1)
            {
                num = 7;
            }
            if (int_1 == 2)
            {
                num = 8;
            }
            if (int_1 == 3)
            {
                num = 9;
            }
            List<SurveyLogic> list = new List<SurveyLogic>();
            list = oSurveyLogicDal.GetReCodeLogic(string_0, int_0, int_2, int_3);
            if (list.Count != 0)
            {
                Logic_Message = list[0].LOGIC_MESSAGE;
                bool flag = false;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (SurveyLogic item in list)
                {
                    bool flag2 = true;
                    if (item.NOTE == GClass0.smethod_0("[") && flag)
                    {
                        flag2 = false;
                    }
                    if (flag2 && Result(item.FORMULA))
                    {
                        flag = true;
                        Logic_Message = item.LOGIC_MESSAGE;
                        string rECODE_ANSWER = item.RECODE_ANSWER;
                        if (num == 7)
                        {
                            string key = stringResult(rECODE_ANSWER);
                            dictionary.Add(key, "");
                            break;
                        }
                        string[] array = aryCode(rECODE_ANSWER, ',');
                        for (int i = 0; i < array.Count(); i++)
                        {
                            if (!dictionary.Keys.Contains(array[i]))
                            {
                                dictionary.Add(array[i], "");
                            }
                        }
                        if (num < 9)
                        {
                            break;
                        }
                    }
                }
                if (dictionary.Count != 0)
                {
                    return dictionary.Keys.ToArray();
                }
                return new string[1]
                {
                    ""
                };
            }
            return new string[1]
            {
                ""
            };
        }

        public string[] RecodeAddonLogic(string string_0, out string string_1, int int_0 = 1)
        {
            List<SurveyLogic> list = new List<SurveyLogic>();
            list = oSurveyLogicDal.GetReCodeLogic(string_0, int_0, 0, 99999);
            if (list.Count != 0)
            {
                string_1 = list[0].FORMULA;
                return list[0].RECODE_ANSWER.Split(',');
            }
            string[] result = new string[1]
            {
                ""
            };
            string_1 = "";
            return result;
        }

        public string[] CircleGuideLogic(string string_0, int int_0 = 1)
        {
            List<SurveyLogic> list = new List<SurveyLogic>();
            list = oSurveyLogicDal.GetCircleGuideLogic(string_0, int_0);
            if (list.Count != 0)
            {
                bool flag = false;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (SurveyLogic item in list)
                {
                    bool flag2 = true;
                    if (item.NOTE == GClass0.smethod_0("[") && flag)
                    {
                        flag2 = false;
                    }
                    if (flag2 && Result(item.FORMULA))
                    {
                        flag = true;
                        string[] array = aryCode(item.RECODE_ANSWER, ',');
                        for (int i = 0; i < array.Count(); i++)
                        {
                            if (!dictionary.Keys.Contains(array[i]))
                            {
                                dictionary.Add(array[i], "");
                            }
                        }
                    }
                }
                if (dictionary.Count != 0)
                {
                    return dictionary.Keys.ToArray();
                }
                return new string[1]
                {
                    ""
                };
            }
            return new string[1]
            {
                ""
            };
        }

        public bool IsExistCircleGuide(string string_0)
        {
            List<SurveyLogic> list = new List<SurveyLogic>();
            list = oSurveyLogicDal.GetCircleGuideLogic(string_0, 1);
            return list.Count > 0;
        }

        public bool IsExistRecode(string string_0, int int_0 = 1)
        {
            List<SurveyLogic> list = new List<SurveyLogic>();
            list = oSurveyLogicDal.GetReCodeLogic(string_0, int_0, 0, 99999);
            return list.Count > 0;
        }

        public bool Result(string string_0)
        {
            LogicExplain logicExplain = new LogicExplain();
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("tųɲͱѰկٮݭ\u086c५੪୩౨൧\u0e66ཥ\u1064ᅣቢ፡ᑠᕟᙞ\u175dᡜᥛ\u1a5a᭙᱘\u1d57Ṗὕ\u2054⅓≒⍑\u2450╏♎❍⡌⥋⩊⭉ⱈⵇ\u2e46⽅いㅃ㉂㍁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("xŷɶ\u0375Ѵճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㐸㕁㙐㜿"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            string text = logicExplain.CleanFormula(string_0);
            if (text.Length <= 0)
            {
                return false;
            }
            text = ReplaceSpecialFlag(text);
            logicExplain.SetData(method_0(text), true);
            return logicExplain.LoopLogicFormula(text) > 0.0;
        }

        public bool boolResult(string string_0)
        {
            return Result(string_0);
        }

        public double doubleResult(string string_0)
        {
            LogicExplain logicExplain = new LogicExplain();
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("tųɲͱѰկٮݭ\u086c५੪୩౨൧\u0e66ཥ\u1064ᅣቢ፡ᑠᕟᙞ\u175dᡜᥛ\u1a5a᭙᱘\u1d57Ṗὕ\u2054⅓≒⍑\u2450╏♎❍⡌⥋⩊⭉ⱈⵇ\u2e46⽅いㅃ㉂㍁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("xŷɶ\u0375Ѵճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㐸㕁㙐㜿"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            string text = logicExplain.CleanFormula(string_0);
            if (text.Length <= 0)
            {
                return 0.0;
            }
            text = ReplaceSpecialFlag(text);
            logicExplain.SetData(method_0(text), true);
            return logicExplain.MathCalculation(text);
        }

        public string stringResult(string string_0)
        {
            LogicExplain logicExplain = new LogicExplain();
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("tųɲͱѰկٮݭ\u086c५੪୩౨൧\u0e66ཥ\u1064ᅣቢ፡ᑠᕟᙞ\u175dᡜᥛ\u1a5a᭙᱘\u1d57Ṗὕ\u2054⅓≒⍑\u2450╏♎❍⡌⥋⩊⭉ⱈⵇ\u2e46⽅いㅃ㉂㍁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("xŷɶ\u0375Ѵճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㐸㕁㙐㜿"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            string text = logicExplain.CleanFormula(string_0);
            if (text.Length <= 0)
            {
                return "";
            }
            text = ReplaceSpecialFlag(text);
            logicExplain.SetData(method_0(text), true);
            string text2 = logicExplain.SimpleCalculation(text);
            if (text.ToUpper().IndexOf(GClass0.smethod_0("/ŉɆ\u034cтՒ\u0640ݜࡗपਢ")) > -1)
            {
                string textQuestion = logicExplain.GetTextQuestion(text2, "");
                logicExplain = new LogicExplain();
                logicExplain.SetData(method_0(textQuestion), true);
                text2 = logicExplain.TextFormula(text2);
            }
            return text2;
        }

        public List<string> listResult(string string_0, string string_1 = ",")
        {
            LogicExplain logicExplain = new LogicExplain();
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("tųɲͱѰկٮݭ\u086c५੪୩౨൧\u0e66ཥ\u1064ᅣቢ፡ᑠᕟᙞ\u175dᡜᥛ\u1a5a᭙᱘\u1d57Ṗὕ\u2054⅓≒⍑\u2450╏♎❍⡌⥋⩊⭉ⱈⵇ\u2e46⽅いㅃ㉂㍁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("xŷɶ\u0375Ѵճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㐸㕁㙐㜿"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            string text = logicExplain.CleanFormula(string_0);
            if (text.Length <= 0)
            {
                return new List<string>();
            }
            text = ReplaceSpecialFlag(text);
            logicExplain.SetData(method_0(text), true);
            string text2 = logicExplain.SimpleCalculation(text);
            return new List<string>(text2.Split(new string[1]
            {
                string_1
            }, StringSplitOptions.RemoveEmptyEntries));
        }

        public string stringCode(string string_0)
        {
            string text = "";
            LogicExplain logicExplain = new LogicExplain();
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("tųɲͱѰկٮݭ\u086c५੪୩౨൧\u0e66ཥ\u1064ᅣቢ፡ᑠᕟᙞ\u175dᡜᥛ\u1a5a᭙᱘\u1d57Ṗὕ\u2054⅓≒⍑\u2450╏♎❍⡌⥋⩊⭉ⱈⵇ\u2e46⽅いㅃ㉂㍁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("xŷɶ\u0375Ѵճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㐸㕁㙐㜿"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            string text2 = logicExplain.CleanFormula(string_0);
            if (text2.Length > 0)
            {
                text2 = ReplaceSpecialFlag(text2);
                logicExplain.SetData(method_0(text2), true);
                text = logicExplain.OptionCalculation(text2);
                if (text2.ToUpper().IndexOf(GClass0.smethod_0("/ŉɆ\u034cтՒ\u0640ݜࡗपਢ")) > -1)
                {
                    string textQuestion = logicExplain.GetTextQuestion(text, "");
                    logicExplain = new LogicExplain();
                    logicExplain.SetData(method_0(textQuestion), true);
                    text = logicExplain.TextFormula(text);
                }
            }
            return text;
        }

        public string[] aryCode(string string_0, char char_0 = ',')
        {
            string text = stringCode(string_0);
            if (text.Length <= 0)
            {
                return new string[0];
            }
            return text.Split(char_0);
        }

        public string[] aryResult(string string_0, char char_0 = ',')
        {
            return aryCode(string_0, char_0);
        }

        public string Route(string string_0)
        {
            if (string_0.Contains(GClass0.smethod_0(";")))
            {
                LogicExplain logicExplain = new LogicExplain();
                logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(GClass0.smethod_0("tųɲͱѰկٮݭ\u086c५੪୩౨൧\u0e66ཥ\u1064ᅣቢ፡ᑠᕟᙞ\u175dᡜᥛ\u1a5a᭙᱘\u1d57Ṗὕ\u2054⅓≒⍑\u2450╏♎❍⡌⥋⩊⭉ⱈⵇ\u2e46⽅いㅃ㉂㍁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(GClass0.smethod_0("xŷɶ\u0375Ѵճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㐸㕁㙐㜿"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                string text = logicExplain.CleanFormula(string_0);
                if (text.Length <= 0)
                {
                    return "";
                }
                text = ReplaceSpecialFlag(text);
                logicExplain.SetData(method_0(text), true);
                return ComplexRouteFormula(text, logicExplain);
            }
            return string_0;
        }

        public string strShowText(string string_0, bool bool_0 = true)
        {
            LogicExplain logicExplain = new LogicExplain();
            string string_ = bool_0 ? logicExplain.CleanTextFormula(string_0) : string_0;
            string_ = ReplaceSpecialFlag(string_);
            string text = string_;
            string textQuestion = logicExplain.GetTextQuestion(string_, "");
            if (textQuestion.Length > 0 || string_.IndexOf(GClass0.smethod_0("Yģ")) > -1)
            {
                logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(GClass0.smethod_0("tųɲͱѰկٮݭ\u086c५੪୩౨൧\u0e66ཥ\u1064ᅣቢ፡ᑠᕟᙞ\u175dᡜᥛ\u1a5a᭙᱘\u1d57Ṗὕ\u2054⅓≒⍑\u2450╏♎❍⡌⥋⩊⭉ⱈⵇ\u2e46⽅いㅃ㉂㍁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(string_0, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.OutputResult(GClass0.smethod_0("xŷɶ\u0375Ѵճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㐸㕁㙐㜿"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
                logicExplain.SetData(method_0(textQuestion), true);
                text = logicExplain.TextFormula(string_);
                if (string_.ToUpper().IndexOf(GClass0.smethod_0("/ŉɆ\u034cтՒ\u0640ݜࡗपਢ")) > -1)
                {
                    textQuestion = logicExplain.GetTextQuestion(text, "");
                    logicExplain = new LogicExplain();
                    logicExplain.SetData(method_0(textQuestion), true);
                    text = logicExplain.TextFormula(text);
                }
            }
            return text.Replace(GClass0.smethod_0("8Łɐ\u033f"), Environment.NewLine);
        }

        public List<classHtmlText> listShowText(string string_0)
        {
            LogicExplain logicExplain = new LogicExplain();
            string string_ = strShowText(string_0, true);
            return logicExplain.SplitTextToList(string_, GClass0.smethod_0("?ŀȿ"), GClass0.smethod_0("8Ĭɀ\u033f"));
        }

        public List<string> listLoopLevel(string string_0, string string_1)
        {
            List<string> result = new List<string>();
            LogicExplain logicExplain = new LogicExplain();
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("徯犫噾䴯\ufb1b"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("徬犪崑壿鲚福") + string_1 + GClass0.smethod_0("＜Ħ噸䴩换䯴亂") + string_0, GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("tųɲͱѰկٮݭ\u086c५੪୩౨൧\u0e66ཥ\u1064ᅣቢ፡ᑠᕟᙞ\u175dᡜᥛ\u1a5a᭙᱘\u1d57Ṗὕ\u2054⅓≒⍑\u2450╏♎❍⡌⥋⩊⭉ⱈⵇ\u2e46⽅いㅃ㉂㍁"), GClass0.smethod_0("aūɔ\u034bѧջ\u0670ݣ\u0877प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("\u0004ŵɤ\u030bѴճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㑄㕃㙂㝁"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("徯犫噾䴯\ufb1b"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("徬犪崑壿鲚福") + string_1 + GClass0.smethod_0("＜Ħ噸䴩换䯴亂") + string_0, GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            logicExplain.OutputResult(GClass0.smethod_0("xŷɶ\u0375Ѵճٲݱ\u0870९੮୭౬൫\u0e6aཀྵ\u1068ᅧቦ፥ᑤᕣᙢᝡᡠᥟ\u1a5e᭝ᱜ\u1d5bṚὙ⁘⅗≖⍕\u2454╓♒❑⡐⥏⩎⭍ⱌⵋ\u2e4a⽉えㅇ㉆㍅㐸㕁㙐㜿"), GClass0.smethod_0("aūɔ\u0358Ѭջٲݪ\u0871प\u0a4f\u0b4d\u0c46"), true);
            string text = "";
            string text2 = logicExplain.CleanFormula(string_1);
            int num = text2.IndexOf(GClass0.smethod_0("-"));
            string text3 = text2;
            string text4 = "";
            if (num > 1)
            {
                text3 = logicExplain.LEFT(text2, num);
                text4 = logicExplain.MID(text2, num + 1, -9999);
                text = text4 + GClass0.smethod_0("'") + text3 + GClass0.smethod_0("$Ś") + string_0 + GClass0.smethod_0("\\");
            }
            else
            {
                text = text3 + GClass0.smethod_0("$Ś") + string_0 + GClass0.smethod_0("\\");
            }
            string text5 = logicExplain.CleanFormula(text);
            if (text5.Length > 0)
            {
                text5 = ReplaceSpecialFlag(text5);
                logicExplain.SetData(method_0(text5), true);
                result = logicExplain.listLoopLevel(text5, text3, text4, "_R", "_R");
            }
            return result;
        }

        public string ComplexRouteFormula(string string_0, LogicExplain logicExplain_0)
        {
            string string_ = GClass0.smethod_0(";");
            string string_2 = GClass0.smethod_0(":");
            string string_3 = logicExplain_0.DeleteOuterSymbol(string_0);
            List<string> list = logicExplain_0.ParaToList(string_3, string_2, false);
            foreach (string item in list)
            {
                int formulaSplitLocation = logicExplain_0.GetFormulaSplitLocation(item, string_);
                if (formulaSplitLocation <= -1)
                {
                    return item;
                }
                string string_4 = logicExplain_0.LEFT(item, formulaSplitLocation);
                string string_5 = logicExplain_0.MID(item, formulaSplitLocation + 1, -9999);
                LogicExplain logicExplain = new LogicExplain();
                logicExplain._dictData = logicExplain_0._dictData;
                if (logicExplain.LoopLogicFormula(string_4) > 0.0)
                {
                    return strShowText(string_5, true);
                }
            }
            return "";
        }

        private Dictionary<string, string> method_0(string string_0)
        {
            string b = GClass0.smethod_0("Yģ");
            string value = GClass0.smethod_0(" Ŝ");
            string b2 = "#[";
            string b3 = GClass0.smethod_0("!ź");
            string b4 = GClass0.smethod_0("&ź");
            string value2 = GClass0.smethod_0("|");
            string[] array = new string[5]
            {
                GClass0.smethod_0("#ŉɑ\u034cцՐة"),
                GClass0.smethod_0("!ŋɗ\u034aЩ"),
                GClass0.smethod_0(".Łɉ\u0351уՃ\u064dݏࡇऩ"),
                GClass0.smethod_0(",ŉɉ\u0343эՏهܩ"),
                GClass0.smethod_0(",ŉɉ\u0351іՆفܩ")
            };
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            LogicExplain logicExplain = new LogicExplain();
            int num = 0;
            int num2 = 0;
            string text = "";
            string text2 = "";
            do
            {
                text = logicExplain.MID(string_0, num, 2);
                if (!(text == b))
                {
                    if (text == b2)
                    {
                        num += 2;
                    }
                    else if (text == b3 || text == b4)
                    {
                        if (text2.Length > 0)
                        {
                            text2 = text2.Trim();
                            if (!dictionary.ContainsKey(text2))
                            {
                                dictionary.Add(text2, method_2(text2));
                            }
                        }
                        num2 = string_0.IndexOf(value2, num);
                        if (num2 < 0)
                        {
                            num2 = string_0.Length - 1;
                        }
                        text2 = string_0.Substring(num, num2 - num).Trim();
                        if (!dictionary.ContainsKey(text2))
                        {
                            dictionary.Add(text2, method_2(text2));
                        }
                        text2 = "";
                        num = num2 + 1;
                    }
                    else
                    {
                        text = logicExplain.MID(string_0, num, 1);
                        if (text == GClass0.smethod_0("%"))
                        {
                            bool flag = false;
                            string[] array2 = array;
                            foreach (string text3 in array2)
                            {
                                string text4 = "";
                                if (string_0.Length - num > text3.Length)
                                {
                                    text4 = logicExplain.MID(string_0, num, text3.Length);
                                }
                                if (text3 == text4.ToUpper())
                                {
                                    flag = true;
                                    num2 = logicExplain.RightBrackets(string_0, num, GClass0.smethod_0(")"), GClass0.smethod_0("("));
                                    num += text3.Length;
                                    text2 = string_0.Substring(num, num2 - num).Replace(GClass0.smethod_0("!"), "").Trim();
                                    if (!dictionary.ContainsKey(text3 + text2 + GClass0.smethod_0("(")))
                                    {
                                        dictionary.Add(text3 + text2 + GClass0.smethod_0("("), method_1(text3, text2));
                                    }
                                    text2 = "";
                                    num = num2 + 2;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                continue;
                            }
                            num2 = 0;
                            if (logicExplain.isLetter(logicExplain.MID(string_0, num + 1, 1)))
                            {
                                num2 = string_0.IndexOf(GClass0.smethod_0(")"), num + 1);
                            }
                            if (num2 > 0)
                            {
                                num = num2;
                            }
                        }
                        else
                        {
                            text = logicExplain.JoinQnName(text, text2);
                            if (text == "")
                            {
                                if (text2.Length > 0)
                                {
                                    text2 = text2.Trim();
                                    if (!dictionary.ContainsKey(text2))
                                    {
                                        dictionary.Add(text2, method_2(text2));
                                    }
                                    text2 = "";
                                }
                            }
                            else
                            {
                                text2 = text;
                            }
                        }
                        num++;
                    }
                }
                else
                {
                    num = string_0.IndexOf(value, num + 1);
                    if (num < 0)
                    {
                        num = string_0.Length;
                    }
                    num += 2;
                }
            }
            while (num < string_0.Length);
            if (text2.Length > 0)
            {
                text2 = text2.Trim();
                if (!dictionary.ContainsKey(text2))
                {
                    dictionary.Add(text2, method_2(text2));
                }
            }
            return dictionary;
        }

        private string method_1(string string_0, string string_1)
        {
            string text = "";
            string b = "#";
            string b2 = GClass0.smethod_0("_");
            LogicExplain logicExplain = new LogicExplain();
            List<string> list = logicExplain.ParaToList(string_1, GClass0.smethod_0("-"), false);
            string text2 = "";
            if (string_0 == GClass0.smethod_0("#ŉɑ\u034cцՐة") || string_0 == GClass0.smethod_0("!ŋɗ\u034aЩ"))
            {
                if (list.Count > 0)
                {
                    text2 = list[0].Substring(0, 1);
                    string string_2 = (list.Count > 1) ? stringResult(list[1]) : "";
                    text = ((text2 == b) ? method_11(list[0].Substring(1), string_2) : ((!(text2 == b2)) ? GetOtherText(list[0], string_2) : method_10(list[0].Substring(1), string_2)));
                    if (string.IsNullOrEmpty(text))
                    {
                        text = "";
                    }
                }
            }
            else if (string_0 == GClass0.smethod_0(".Łɉ\u0351уՃ\u064dݏࡇऩ"))
            {
                if (list.Count > 0)
                {
                    text = method_14(list[0]);
                }
            }
            else if (string_0 == GClass0.smethod_0(",ŉɉ\u0343эՏهܩ"))
            {
                text = GClass0.smethod_0("0");
                if (list.Count > 0)
                {
                    text = ((method_14(list[0]) == GClass0.smethod_0("0")) ? GClass0.smethod_0("1") : GClass0.smethod_0("0"));
                }
            }
            else if (string_0 == GClass0.smethod_0(",ŉɉ\u0351іՆفܩ"))
            {
                text = method_15();
            }
            return text;
        }

        private string method_2(string string_0)
        {
            string text = string_0.Trim();
            string b = "#";
            string b2 = GClass0.smethod_0("_");
            string b3 = GClass0.smethod_0("!ź");
            string b4 = GClass0.smethod_0("&ź");
            string text2 = GClass0.smethod_0("-");
            string text3 = "";
            if (text.Length > 0)
            {
                string a = text.Substring(0, 1);
                string a2 = (text.Length > 1) ? text.Substring(0, 2) : "";
                text3 = ((a2 == b3) ? method_8(text.Substring(2)) : ((a2 == b4) ? method_7(text.Substring(2)) : ((a == b) ? method_4(text.Substring(1)) : ((!(a == b2)) ? method_3(text) : method_5(text.Substring(1))))));
            }
            if (string.IsNullOrEmpty(text3))
            {
                text3 = "";
            }
            else
            {
                while (text3.Contains(text2 + text2))
                {
                    text3 = text3.Replace(text2 + text2, text2);
                }
                if (text3.Length > 0 && text3.Substring(0, 1) == text2)
                {
                    text3 = text3.Substring(1);
                }
                if (text3.Length > 0 && text3.Substring(text3.Length - 1, 1) == text2)
                {
                    text3 = text3.Substring(0, text3.Length - 1);
                }
            }
            return text3;
        }

        private string method_3(string string_0)
        {
            LogicAnswer logicAnswer = new LogicAnswer();
            logicAnswer.SurveyID = SurveyID;
            logicAnswer.PageAnswer = PageAnswer;
            return logicAnswer.GetAnswer(string_0);
        }

        private string method_4(string string_0)
        {
            string b = "#";
            LogicAnswer logicAnswer = new LogicAnswer();
            logicAnswer.SurveyID = SurveyID;
            logicAnswer.PageAnswer = PageAnswer;
            logicAnswer.CircleACode = CircleACode;
            logicAnswer.CircleBCode = CircleBCode;
            string a = string_0.Substring(0, 1);
            if (!(a == b))
            {
                if (string_0.Length <= 2 || !(oFunc.LEFT(oFunc.RIGHT(string_0, 2), 1) == "#"))
                {
                    return logicAnswer.GetAllCircleAnswer(string_0);
                }
                string string_ = oFunc.RIGHT(string_0, 1);
                string string_2 = oFunc.LEFT(string_0, string_0.Length - 2);
                return logicAnswer.GetAllCircleAnswer_C(string_2, string_);
            }
            if (string_0.Length <= 2 || !(oFunc.LEFT(oFunc.RIGHT(string_0, 2), 1) == "#"))
            {
                return logicAnswer.GetCircleAnswer(string_0.Substring(1));
            }
            string string_3 = oFunc.RIGHT(string_0, 1);
            string string_4 = oFunc.SubStringFromStartToEnd(string_0, 1, string_0.Length - 3);
            return logicAnswer.GetCircleAnswer_C(string_4, string_3);
        }

        private string method_5(string string_0)
        {
            LogicAnswer logicAnswer = new LogicAnswer();
            logicAnswer.SurveyID = SurveyID;
            logicAnswer.PageAnswer = PageAnswer;
            logicAnswer.CircleACode = CircleACode;
            logicAnswer.CircleBCode = CircleBCode;
            return logicAnswer.GetCurrentCircleAnswer(string_0);
        }

        internal string method_6(string string_0, string string_1)
        {
            LogicAnswer logicAnswer = new LogicAnswer();
            return logicAnswer.GetText(string_0, string_1);
        }

        private string method_7(string string_0)
        {
            LogicAnswer logicAnswer = new LogicAnswer();
            string[] detailsByQName = logicAnswer.GetDetailsByQName(string_0);
            return string.Join(GClass0.smethod_0("-"), detailsByQName);
        }

        private string method_8(string string_0)
        {
            LogicAnswer logicAnswer = new LogicAnswer();
            logicAnswer.SurveyID = SurveyID;
            logicAnswer.CircleACode = CircleACode;
            logicAnswer.CircleBCode = CircleBCode;
            string[] option = logicAnswer.GetOption(string_0);
            return string.Join(GClass0.smethod_0("-"), option);
        }

        internal string method_9(string string_0, string string_1, string string_2 = "、")
        {
            string text = "";
            LogicAnswer logicAnswer = new LogicAnswer();
            List<string> list = oFunc.StringToList(string_1, GClass0.smethod_0("-"));
            foreach (string item in list)
            {
                string detailsText = logicAnswer.GetDetailsText(string_0, item);
                if (detailsText.Length > 0)
                {
                    text = text + string_2 + detailsText;
                }
            }
            if (oFunc.LEFT(text, 1) == string_2)
            {
                text = oFunc.MID(text, 1, -9999);
            }
            return text;
        }

        internal string GetOtherText(string string_0, string string_1 = "")
        {
            LogicAnswer logicAnswer = new LogicAnswer();
            logicAnswer.SurveyID = SurveyID;
            logicAnswer.PageAnswer = PageAnswer;
            return logicAnswer.GetOtherText(string_0, string_1);
        }

        internal string method_10(string string_0, string string_1 = "")
        {
            LogicAnswer logicAnswer = new LogicAnswer();
            logicAnswer.SurveyID = SurveyID;
            logicAnswer.PageAnswer = PageAnswer;
            logicAnswer.CircleACode = CircleACode;
            logicAnswer.CircleBCode = CircleBCode;
            return logicAnswer.GetCurrentCircleOtherText(string_0, string_1);
        }

        internal string method_11(string string_0, string string_1 = "")
        {
            string text = "";
            bool flag = false;
            if (oFunc.LEFT(string_1, 1) == "#")
            {
                flag = true;
                string_1 = oFunc.MID(string_1, 1, -9999);
            }
            LogicAnswer logicAnswer = new LogicAnswer();
            logicAnswer.SurveyID = SurveyID;
            logicAnswer.PageAnswer = PageAnswer;
            logicAnswer.CircleACode = CircleACode;
            logicAnswer.CircleBCode = CircleBCode;
            if (!((CircleACode == "" || CircleACode == null) | flag))
            {
                return logicAnswer.GetCircleOtherText(string_0, string_1);
            }
            return logicAnswer.GetAllCircleOtherText(string_0, string_1);
        }

        public string ReplaceSpecialFlag(string string_0)
        {
            string text = string_0.Replace(GClass0.smethod_0(",ŜɅ\u034aрՆكݜ"), CircleACodeText);
            text = text.Replace(GClass0.smethod_0(",ŜɅ\u034aрՆ\u0640ݜ"), CircleBCodeText);
            text = text.Replace(GClass0.smethod_0("+ŜɅ\u034aрՆكݜ"), CircleACode);
            text = text.Replace(GClass0.smethod_0("+ŜɅ\u034aрՆ\u0640ݜ"), CircleBCode);
            text = text.Replace(GClass0.smethod_0("'Řɣ\u035c"), CircleACurrent.ToString());
            text = text.Replace(GClass0.smethod_0("'Řɠ\u035c"), CircleBCurrent.ToString());
            text = text.Replace(GClass0.smethod_0("*œɄ\u0349ѐՊ\u0657\u0743\u085c"), CircleACount.ToString());
            return text.Replace(GClass0.smethod_0("*œɄ\u0349ѐՊ\u0657\u0740\u085c"), CircleBCount.ToString());
        }

        internal List<string> method_12(List<string> list_0)
        {
            RandomEngine randomEngine = new RandomEngine();
            return randomEngine.StringListRandom(list_0);
        }

        internal List<string> method_13(string string_0)
        {
            RandomEngine randomEngine = new RandomEngine();
            List<SurveyDetail> list = new List<SurveyDetail>();
            SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
            list = surveyDetailDal.GetDetails(string_0);
            list = randomEngine.RandomDetails(list);
            List<string> list2 = new List<string>();
            foreach (SurveyDetail item in list)
            {
                list2.Add(item.CODE);
            }
            return list2;
        }

        internal string method_14(string string_0)
        {
            string text = GClass0.smethod_0("1");
            string b = "#";
            string b2 = GClass0.smethod_0("_");
            LogicExplain logicExplain = new LogicExplain();
            string a = string_0.Substring(0, 1);
            string text2 = string_0;
            if (a == b2 || a == b)
            {
                text2 = logicExplain.MID(text2, 1, -9999);
                if (CircleACode != "")
                {
                    text2 = text2 + "_R" + CircleACode;
                    if (CircleBCode != "")
                    {
                        text2 = text2 + "_R" + CircleBCode;
                    }
                }
            }
            AttachBiz attachBiz = new AttachBiz();
            return attachBiz.ExistsByQName(SurveyID, text2) ? GClass0.smethod_0("0") : GClass0.smethod_0("1");
        }

        internal string method_15()
        {
            string byCodeTextRead = oSurveyConfigBiz.GetByCodeTextRead(GClass0.smethod_0("XŬɫ\u0368Ѵա\u064dݰࡍ९"));
            string byCodeTextRead2 = oSurveyConfigBiz.GetByCodeTextRead(GClass0.smethod_0("]ūɮ\u0363ѹծ\u0640ݻࡕॳ੫୪౪൬\u0e66"));
            string result = GClass0.smethod_0("1");
            if (byCodeTextRead == GClass0.smethod_0("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64") && byCodeTextRead2 != GClass0.smethod_0("pűɷ\u0364"))
            {
                result = GClass0.smethod_0("0");
            }
            return result;
        }

        public void OutputResult(string string_0, string string_1)
        {
            LogicExplain logicExplain = new LogicExplain();
            logicExplain.OutputResult(string_0, string_1, true);
        }
    }
}
