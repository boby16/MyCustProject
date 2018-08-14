using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.BIZ
{
    public class SurveyImport
    {
        [CompilerGenerated]
        private sealed class Class10
        {
            public string strQName;

            internal bool method_0(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_1(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_2(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_3(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_4(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_5(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_6(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_7(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_8(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_9(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_10(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_11(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_12(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_13(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_14(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_15(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_16(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }

            internal bool method_17(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.QUESTION_NAME == strQName;
            }
        }

        [Serializable]
        [CompilerGenerated]
        private sealed class Class11
        {
            public static readonly Class11 _003C_003E9 = new Class11();

            public static Func<SurveyAnswer, bool> _003C_003E9__8_0;

            public static Func<SurveyAnswer, Class0<string, string, int, string, string>> _003C_003E9__8_1;

            internal bool method_0(SurveyAnswer surveyAnswer_0)
            {
                return surveyAnswer_0.SEQUENCE_ID == 90000;
            }

            internal Class0<string, string, int, string, string> method_1(SurveyAnswer surveyAnswer_0)
            {
                return new Class0<string, string, int, string, string>(surveyAnswer_0.QUESTION_NAME, surveyAnswer_0.CODE, surveyAnswer_0.MULTI_ORDER, surveyAnswer_0.SURVEY_GUID, surveyAnswer_0.SURVEY_ID);
            }
        }

        private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

        private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

        private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

        private SurveySequenceDal oSurveySequenceDal = new SurveySequenceDal();

        private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();

        public void ImportSurveyAnswer(List<SurveyAnswer> list_0, string string_0)
        {
            foreach (SurveyAnswer item in list_0)
            {
                item.SURVEY_GUID = string_0;
                if (!item.BEGIN_DATE.HasValue)
                {
                    item.BEGIN_DATE = item.MODIFY_DATE;
                }
                oSurveyAnswerDal.AddByModel(item);
            }
        }

        public bool ImportSurveyMain(List<SurveyAnswer> list_0)
        {
            Class10 @class = new Class10();
            SurveyMain surveyMain = new SurveyMain();
            int num = 0;
            @class.strQName = GClass0.smethod_0("");
            @class.strQName = GClass0.smethod_0("Zŝɕ\u0350р՝\u065c\u074bࡅ");
            num = list_0.FindIndex(@class.method_0);
            if (num > -1)
            {
                surveyMain.SURVEY_ID = list_0[num].CODE;
            }
            @class.strQName = GClass0.smethod_0("\\Ōɚ\u0354яՊيݜࡋ\u0945");
            num = list_0.FindIndex(@class.method_1);
            if (num > -1)
            {
                surveyMain.VERSION_ID = list_0[num].CODE;
            }
            @class.strQName = GClass0.smethod_0("Rŕɀ\u0356ќՋم");
            num = list_0.FindIndex(@class.method_2);
            if (num > -1)
            {
                surveyMain.USER_ID = list_0[num].CODE;
            }
            @class.strQName = GClass0.smethod_0("Dŏɑ\u035dќՋم");
            num = list_0.FindIndex(@class.method_3);
            if (num > -1)
            {
                surveyMain.CITY_ID = list_0[num].CODE;
            }
            @class.strQName = GClass0.smethod_0("Yŝɉ\u0355ђ՚\u0650\u074aࡏ\u0944");
            num = list_0.FindIndex(@class.method_4);
            if (num > -1)
            {
                surveyMain.START_TIME = Convert.ToDateTime(list_0[num].CODE.ToString());
            }
            @class.strQName = GClass0.smethod_0("Mŉɂ\u035aѐՊ\u064f\u0744");
            num = list_0.FindIndex(@class.method_5);
            if (num > -1)
            {
                surveyMain.END_TIME = Convert.ToDateTime(list_0[num].CODE.ToString());
            }
            @class.strQName = GClass0.smethod_0("Cŏɞ\u0358є\u0559\u065d\u0749ࡕ\u0952ਗ਼\u0b50\u0c4a\u0d4fไ");
            num = list_0.FindIndex(@class.method_6);
            if (num > -1)
            {
                surveyMain.LAST_START_TIME = Convert.ToDateTime(list_0[num].CODE.ToString());
            }
            @class.strQName = GClass0.smethod_0("Aōɘ\u035eіՍى\u0742\u085aॐ\u0a4a\u0b4f\u0c44");
            num = list_0.FindIndex(@class.method_7);
            if (num > -1)
            {
                surveyMain.LAST_END_TIME = Convert.ToDateTime(list_0[num].CODE.ToString());
            }
            @class.strQName = GClass0.smethod_0("Hşɛ\u0357їՇق\u0741\u085c\u094b\u0a45");
            num = list_0.FindIndex(@class.method_8);
            if (num > -1)
            {
                surveyMain.CUR_PAGE_ID = list_0[num].CODE;
            }
            @class.strQName = GClass0.smethod_0("Sņɜ\u034eрՎ\u0655\u0748ࡗ\u0944\u0a53\u0b57\u0c56\u0d46\u0e4cཕ");
            num = list_0.FindIndex(@class.method_9);
            if (num > -1)
            {
                surveyMain.CIRCLE_A_CURRENT = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = GClass0.smethod_0("Mńɞ\u0348цՌ\u0657\u0746\u0859\u0946\u0a4b\u0b56\u0c4c\u0d55");
            num = list_0.FindIndex(@class.method_10);
            if (num > -1)
            {
                surveyMain.CIRCLE_A_COUNT = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = GClass0.smethod_0("Sņɜ\u034eрՎ\u0655\u074bࡗ\u0944\u0a53\u0b57\u0c56\u0d46\u0e4cཕ");
            num = list_0.FindIndex(@class.method_11);
            if (num > -1)
            {
                surveyMain.CIRCLE_B_CURRENT = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = GClass0.smethod_0("Mńɞ\u0348цՌ\u0657\u0745\u0859\u0946\u0a4b\u0b56\u0c4c\u0d55");
            num = list_0.FindIndex(@class.method_12);
            if (num > -1)
            {
                surveyMain.CIRCLE_B_COUNT = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = GClass0.smethod_0("@śɘ\u0340ьՊيݑࡉ");
            num = list_0.FindIndex(@class.method_13);
            if (num > -1)
            {
                surveyMain.IS_FINISH = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = GClass0.smethod_0("Xŏɘ\u035dтՈن\u0741\u085c\u094b\u0a45");
            num = list_0.FindIndex(@class.method_14);
            if (num > -1)
            {
                surveyMain.SEQUENCE_ID = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = GClass0.smethod_0("Xşɛ\u035eт՟\u065a\u0743ࡖ\u094b\u0a45");
            num = list_0.FindIndex(@class.method_15);
            if (num > -1)
            {
                surveyMain.SURVEY_GUID = list_0[num].CODE;
            }
            @class.strQName = GClass0.smethod_0("JńɎ\u0343ыՐ\u065c\u074bࡅ");
            num = list_0.FindIndex(@class.method_16);
            if (num > -1)
            {
                surveyMain.CLIENT_ID = list_0[num].CODE;
            }
            @class.strQName = GClass0.smethod_0("Zśɇ\u034dуՆ\u0650ݜࡋ\u0945");
            num = list_0.FindIndex(@class.method_17);
            if (num > -1)
            {
                surveyMain.PROJECT_ID = list_0[num].CODE;
            }
            if (!oSurveyMainDal.ExistsByGUID(surveyMain.SURVEY_GUID))
            {
                oSurveyMainDal.Add(surveyMain);
                return true;
            }
            return false;
        }

        public void ImportSurveyRandom(List<SurveyAnswer> list_0)
        {
            IEnumerable<Class0<string, string, int, string, string>> enumerable = list_0.Where(Class11._003C_003E9.method_0).Select(Class11._003C_003E9.method_1);
            foreach (Class0<string, string, int, string, string> item in enumerable)
            {
                SurveyRandom surveyRandom = new SurveyRandom();
                surveyRandom.SURVEY_ID = item.SURVEY_ID;
                surveyRandom.QUESTION_SET = item.QUESTION_NAME;
                surveyRandom.QUESTION_NAME = item.QUESTION_NAME;
                surveyRandom.CODE = item.CODE;
                surveyRandom.PARENT_CODE = GClass0.smethod_0("");
                surveyRandom.RANDOM_INDEX = item.MULTI_ORDER;
                surveyRandom.RANDOM_SET1 = 1;
                surveyRandom.RANDOM_SET2 = 0;
                surveyRandom.RANDOM_SET3 = 0;
                surveyRandom.IS_FIX = 0;
                surveyRandom.SURVEY_GUID = item.SURVEY_GUID;
                oSurveyRandomDal.Add(surveyRandom);
            }
        }

        public void ImportSurveySequence(List<SurveySequence> list_0, string string_0)
        {
            foreach (SurveySequence item in list_0)
            {
                item.SURVEY_GUID = string_0;
                oSurveySequenceDal.Add(item);
            }
        }

        public void ImportSurveyAttach(List<SurveyAttach> list_0, string string_0)
        {
            foreach (SurveyAttach item in list_0)
            {
                oSurveyAttachDal.Add(item);
            }
        }

        public void DeleteOneSurvey(string string_0, string string_1)
        {
            oSurveyAnswerDal.DeleteOneSurvey(string_0, string_1);
            oSurveyMainDal.DeleteOneSurvey(string_0, string_1);
            oSurveyRandomDal.DeleteOneSurvey(string_0, string_1);
            oSurveySequenceDal.DeleteOneSurvey(string_0, string_1);
            oSurveyAttachDal.DeleteOneSurvey(string_0, string_1);
        }

        public void DeleteAllSurvey()
        {
            oSurveyAnswerDal.Truncate();
            oSurveyMainDal.Truncate();
            oSurveyRandomDal.Truncate();
            oSurveySequenceDal.Truncate();
            oSurveyAttachDal.Truncate();
        }

        public void ImportSurveyAnswer_Key(List<SurveyAnswer> list_0, string string_0, string string_1)
        {
            bool flag = false;
            SurveyExport surveyExport = new SurveyExport();
            surveyExport.QCAdvanceConfig(0);
            foreach (SurveyAnswer item in list_0)
            {
                flag = false;
                foreach (SurveyDefine qQuotum in surveyExport.QQuota)
                {
                    if (item.QUESTION_NAME.ToUpper() == qQuotum.QUESTION_TITLE.ToUpper())
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    string a = item.QUESTION_NAME.ToUpper();
                    if (!(a == GClass0.smethod_0("Xşɛ\u035eт՟\u065a\u0743ࡖ\u094b\u0a45")))
                    {
                        if (!(a == GClass0.smethod_0("Zŝɕ\u0350р՝\u065c\u074bࡅ")))
                        {
                            if (a == GClass0.smethod_0("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44"))
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    item.SURVEY_GUID = string_0;
                    if (!item.BEGIN_DATE.HasValue)
                    {
                        item.BEGIN_DATE = item.MODIFY_DATE;
                    }
                    oSurveyAnswerDal.AddByModel(item);
                }
                if (item.QUESTION_NAME.ToUpper() == GClass0.smethod_0("\\Ōɚ\u0354яՊيݜࡋ\u0945"))
                {
                    item.SURVEY_GUID = string_0;
                    item.QUESTION_NAME = GClass0.smethod_0("OŁɋ\u0343њՔقݖࡉ");
                    item.CODE = string_1;
                    if (!item.BEGIN_DATE.HasValue)
                    {
                        item.BEGIN_DATE = item.MODIFY_DATE;
                    }
                    oSurveyAnswerDal.AddByModel(item);
                }
            }
        }

        public void DeleteOneSurveyMap(string string_0, string string_1)
        {
            oSurveyAnswerDal.DeleteOneSurvey(string_0, string_1);
            oSurveyMainDal.DeleteOneSurvey(string_0, string_1);
        }

        public void ImportSurveyAnswerforMap(List<SurveyAnswer> list_0, string string_0, string string_1)
        {
            int num = 0;
            bool flag = false;
            foreach (SurveyAnswer item in list_0)
            {
                flag = false;
                if (item.QUESTION_NAME == string_1)
                {
                    flag = true;
                    num++;
                }
                if (item.QUESTION_NAME == string_1 + GClass0.smethod_0("WŊɧ\u0375щբ\u0670ݪ"))
                {
                    flag = true;
                    num++;
                }
                if (item.QUESTION_NAME == string_1 + GClass0.smethod_0("Xŋɤ\u0374яլ٦"))
                {
                    flag = true;
                    num++;
                }
                if (item.QUESTION_NAME == string_1 + GClass0.smethod_0("Xŋɤ\u0374яգٵ"))
                {
                    flag = true;
                    num++;
                }
                if (num > 4)
                {
                    break;
                }
                if (flag)
                {
                    item.SURVEY_GUID = string_0;
                    if (!item.BEGIN_DATE.HasValue)
                    {
                        item.BEGIN_DATE = item.MODIFY_DATE;
                    }
                    oSurveyAnswerDal.AddByModel(item);
                }
            }
        }
    }
}
