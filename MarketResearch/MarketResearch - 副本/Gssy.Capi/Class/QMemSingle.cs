using Gssy.Capi.BIZ;
using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Class
{
    public class QMemSingle : QSingle
    {
        [CompilerGenerated]
        private sealed class Class71
        {
            public string PageId;

            public int CombineIndex;

            internal bool method_0(SurveyDefine surveyDefine_0)
            {
                if (surveyDefine_0.PAGE_ID == PageId)
                {
                    return surveyDefine_0.COMBINE_INDEX == CombineIndex;
                }
                return false;
            }
        }

        [Serializable]
        [CompilerGenerated]
        private sealed class Class72
        {
            public static readonly Class72 _003C_003E9 = new Class72();

            public static Func<SurveyDetail, int> _003C_003E9__1_1;

            public static Func<SurveyDetail, int> _003C_003E9__1_3;

            internal int method_0(SurveyDetail surveyDetail_0)
            {
                return surveyDetail_0.INNER_ORDER;
            }

            internal int method_1(SurveyDetail surveyDetail_0)
            {
                return surveyDetail_0.INNER_ORDER;
            }
        }

        public void Init(string string_0, int int_0, List<SurveyDefine> list_0, List<SurveyDetail> list_1)
        {
            Class71 @class = new Class71();
            @class.PageId = string_0;
            @class.CombineIndex = int_0;
            base.QInitDateTime = DateTime.Now;
            List<SurveyDefine> list = list_0.Where(@class.method_0).ToList();
            base.QDefine = list[0];
            base.QuestionName = base.QDefine.QUESTION_NAME;
        }

        public void InitDetailID(string string_0, int int_0, List<SurveyDefine> list_0, List<SurveyDetail> list_1)
        {
            if (!(base.QDefine.PARENT_CODE == "") && int_0 != 0)
            {
                if (!(base.QDefine.PARENT_CODE == "DYNAMIC") && int_0 > 0)
                {
                    IOrderedEnumerable<SurveyDetail> source = list_1.Where(method_1).OrderBy(Class72._003C_003E9.method_1);
                    base.QDetails = source.ToList();
                }
            }
            else
            {
                IOrderedEnumerable<SurveyDetail> source2 = list_1.Where(method_0).OrderBy(Class72._003C_003E9.method_0);
                base.QDetails = source2.ToList();
            }
        }

        [CompilerGenerated]
        private bool method_0(SurveyDetail surveyDetail_0)
        {
            return surveyDetail_0.DETAIL_ID == base.QDefine.DETAIL_ID;
        }

        [CompilerGenerated]
        private bool method_1(SurveyDetail surveyDetail_0)
        {
            if (surveyDetail_0.DETAIL_ID == base.QDefine.DETAIL_ID)
            {
                return surveyDetail_0.PARENT_CODE == base.QDefine.PARENT_CODE;
            }
            return false;
        }
    }
}
