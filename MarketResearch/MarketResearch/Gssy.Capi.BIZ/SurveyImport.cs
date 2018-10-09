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
            @class.strQName = "";
            @class.strQName = "SURVEY_ID";
            num = list_0.FindIndex(@class.method_0);
            if (num > -1)
            {
                surveyMain.SURVEY_ID = list_0[num].CODE;
            }
            @class.strQName = "VERSION_ID";
            num = list_0.FindIndex(@class.method_1);
            if (num > -1)
            {
                surveyMain.VERSION_ID = list_0[num].CODE;
            }
            @class.strQName = "USER_ID";
            num = list_0.FindIndex(@class.method_2);
            if (num > -1)
            {
                surveyMain.USER_ID = list_0[num].CODE;
            }
            @class.strQName = "CITY_ID";
            num = list_0.FindIndex(@class.method_3);
            if (num > -1)
            {
                surveyMain.CITY_ID = list_0[num].CODE;
            }
            @class.strQName = "START_TIME";
            num = list_0.FindIndex(@class.method_4);
            if (num > -1)
            {
                surveyMain.START_TIME = Convert.ToDateTime(list_0[num].CODE.ToString());
            }
            @class.strQName = "END_TIME";
            num = list_0.FindIndex(@class.method_5);
            if (num > -1)
            {
                surveyMain.END_TIME = Convert.ToDateTime(list_0[num].CODE.ToString());
            }
            @class.strQName = "LAST_START_TIME";
            num = list_0.FindIndex(@class.method_6);
            if (num > -1)
            {
                surveyMain.LAST_START_TIME = Convert.ToDateTime(list_0[num].CODE.ToString());
            }
            @class.strQName = "LAST_END_TIME";
            num = list_0.FindIndex(@class.method_7);
            if (num > -1)
            {
                surveyMain.LAST_END_TIME = Convert.ToDateTime(list_0[num].CODE.ToString());
            }
            @class.strQName = "CUR_PAGE_ID";
            num = list_0.FindIndex(@class.method_8);
            if (num > -1)
            {
                surveyMain.CUR_PAGE_ID = list_0[num].CODE;
            }
            @class.strQName = "CIRCLE_A_CURRENT";
            num = list_0.FindIndex(@class.method_9);
            if (num > -1)
            {
                surveyMain.CIRCLE_A_CURRENT = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = "CIRCLE_A_COUNT";
            num = list_0.FindIndex(@class.method_10);
            if (num > -1)
            {
                surveyMain.CIRCLE_A_COUNT = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = "CIRCLE_B_CURRENT";
            num = list_0.FindIndex(@class.method_11);
            if (num > -1)
            {
                surveyMain.CIRCLE_B_CURRENT = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = "CIRCLE_B_COUNT";
            num = list_0.FindIndex(@class.method_12);
            if (num > -1)
            {
                surveyMain.CIRCLE_B_COUNT = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = "IS_FINISH";
            num = list_0.FindIndex(@class.method_13);
            if (num > -1)
            {
                surveyMain.IS_FINISH = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = "SEQUENCE_ID";
            num = list_0.FindIndex(@class.method_14);
            if (num > -1)
            {
                surveyMain.SEQUENCE_ID = Convert.ToInt32(list_0[num].CODE);
            }
            @class.strQName = "SURVEY_GUID";
            num = list_0.FindIndex(@class.method_15);
            if (num > -1)
            {
                surveyMain.SURVEY_GUID = list_0[num].CODE;
            }
            @class.strQName = "CLIENT_ID";
            num = list_0.FindIndex(@class.method_16);
            if (num > -1)
            {
                surveyMain.CLIENT_ID = list_0[num].CODE;
            }
            @class.strQName = "PROJECT_ID";
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
            var enumerable = list_0.Where(p=>p.SEQUENCE_ID == 90000);
            foreach (var item in enumerable)
            {
                SurveyRandom surveyRandom = new SurveyRandom();
                surveyRandom.SURVEY_ID = item.SURVEY_ID;
                surveyRandom.QUESTION_SET = item.QUESTION_NAME;
                surveyRandom.QUESTION_NAME = item.QUESTION_NAME;
                surveyRandom.CODE = item.CODE;
                surveyRandom.PARENT_CODE = "";
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
                    if (!(a == "SURVEY_GUID"))
                    {
                        if (!(a == "SURVEY_ID"))
                        {
                            if (a == "SURVEY_CODE")
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
                if (item.QUESTION_NAME.ToUpper() == "VERSION_ID")
                {
                    item.SURVEY_GUID = string_0;
                    item.QUESTION_NAME = "FILE_PATH";
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
                if (item.QUESTION_NAME == string_1 + "_MapMark")
                {
                    flag = true;
                    num++;
                }
                if (item.QUESTION_NAME == string_1 + "_MapLng")
                {
                    flag = true;
                    num++;
                }
                if (item.QUESTION_NAME == string_1 + "_MapLat")
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
