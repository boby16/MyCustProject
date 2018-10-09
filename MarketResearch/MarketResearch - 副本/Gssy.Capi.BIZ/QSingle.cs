using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.BIZ
{
    public class QSingle
    {
        [CompilerGenerated]
        private sealed class Class2
        {
            public string Level1Code;

            internal bool method_0(SurveyDetail surveyDetail_0)
            {
                return surveyDetail_0.PARENT_CODE == Level1Code;
            }
        }

        [Serializable]
        [CompilerGenerated]
        private sealed class Class3
        {
            public static readonly Class3 _003C_003E9 = new Class3();

            public static Func<SurveyDetail, int> _003C_003E9__79_1;

            internal int method_0(SurveyDetail surveyDetail_0)
            {
                return surveyDetail_0.INNER_ORDER;
            }
        }

        private string QChildQuestion = "";

        public string OtherCode = "";

        public string FillText = "";

        public string NoneCode = "";

        public string NoneCodeText = "";

        public string SelectedCode = "";

        private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

        private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

        private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

        public string QuestionName
        {
            get;
            set;
        }

        public string QuestionTitle
        {
            get;
            set;
        }

        public string ParentCode
        {
            get;
            set;
        }

        public List<SurveyDetail> QDetails
        {
            get;
            set;
        }

        public List<SurveyDetail> QCircleDetails
        {
            get;
            set;
        }

        public List<SurveyDetail> QGroupDetails
        {
            get;
            set;
        }

        public List<SurveyDetail> QDetailsParent
        {
            get;
            set;
        }

        public SurveyAnswer OneAnswer
        {
            get;
            set;
        }

        public List<SurveyAnswer> QAnswers
        {
            get;
            set;
        }

        public List<SurveyAnswer> QAnswersRead
        {
            get;
            set;
        }

        public SurveyDefine QDefine
        {
            get;
            set;
        }

        public SurveyDefine QCircleDefine
        {
            get;
            set;
        }

        public DateTime QInitDateTime
        {
            get;
            set;
        }

        public void Init(string string_0, int int_0, bool GetDetail = true)
        {
            QInitDateTime = DateTime.Now;
            string _003F40_003F = "";
            QDefine = oSurveyDefineDal.GetByPageId(string_0, int_0);
            QuestionName = QDefine.QUESTION_NAME;
            ParentCode = QDefine.PARENT_CODE;
            if (QDefine.DETAIL_ID != "" && GetDetail)
            {
                if (QDefine.PARENT_CODE == "" || int_0 == 0)
                {
                    QDetails = oSurveyDetailDal.GetDetails(QDefine.DETAIL_ID, out _003F40_003F);
                    OtherCode = _003F40_003F;
                }
                else if (!(QDefine.PARENT_CODE == "DYNAMIC") && int_0 > 0)
                {
                    QDetails = oSurveyDetailDal.GetList(QDefine.DETAIL_ID, QDefine.PARENT_CODE);
                }
            }
        }

        public void InitCircle()
        {
            QChildQuestion = ((QDefine.GROUP_LEVEL == "B") ? QDefine.GROUP_CODEB : QDefine.GROUP_CODEA);
            QCircleDefine = oSurveyDefineDal.GetByName(QChildQuestion);
            if (QCircleDefine.DETAIL_ID != "")
            {
                QCircleDetails = oSurveyDetailDal.GetDetails(QCircleDefine.DETAIL_ID);
            }
        }

        public string GetParentCode(string string_0)
        {
            foreach (SurveyDetail qDetail in QDetails)
            {
                if (qDetail.CODE == string_0)
                {
                    ParentCode = qDetail.PARENT_CODE;
                    return ParentCode;
                }
            }
            ParentCode = "";
            return "";
        }

        public void BeforeSave()
        {
            QAnswers = method_0();
        }

        private List<SurveyAnswer> method_0()
        {
            SurveyAnswer surveyAnswer = new SurveyAnswer();
            surveyAnswer.QUESTION_NAME = QuestionName;
            surveyAnswer.CODE = SelectedCode;
            surveyAnswer.MULTI_ORDER = 0;
            surveyAnswer.BEGIN_DATE = QInitDateTime;
            surveyAnswer.MODIFY_DATE = DateTime.Now;
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            list.Add(surveyAnswer);
            if (FillText != "")
            {
                SurveyAnswer surveyAnswer2 = new SurveyAnswer();
                surveyAnswer2.QUESTION_NAME = QuestionName + GClass0.smethod_0("[Ōɖ\u0349");
                surveyAnswer2.CODE = FillText;
                surveyAnswer2.MULTI_ORDER = 0;
                surveyAnswer2.BEGIN_DATE = QInitDateTime;
                surveyAnswer2.MODIFY_DATE = DateTime.Now;
                list.Add(surveyAnswer2);
            }
            return list;
        }

        public void Save(string string_0, int int_0, bool bool_0 = true)
        {
            if (bool_0)
            {
                oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
            }
            oSurveyAnswerDal.UpdateList(string_0, int_0, QAnswers);
        }

        public void ReadAnswer(string string_0, int int_0)
        {
            QAnswersRead = oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
        }

        public string ReadAnswerByQuestionName(string string_0, string string_1)
        {
            string text = "";
            SurveyAnswer one = oSurveyAnswerDal.GetOne(string_0, string_1);
            return one.CODE;
        }

        public virtual void GetDynamicDetails()
        {
            QDetails = oSurveyDetailDal.GetList(QDefine.DETAIL_ID, ParentCode);
        }

        public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
        {
            if (list_0.Count > 1)
            {
                RandomEngine randomEngine = new RandomEngine();
                list_0 = randomEngine.RandomDetails(list_0);
            }
            return list_0;
        }

        public void RandomDetails()
        {
            QDetails = RandomDetails(QDetails);
        }

        public string GetInnerCodeText(string string_0 = "")
        {
            string result = "";
            if (string_0 == "")
            {
                string_0 = SelectedCode;
            }
            foreach (SurveyDetail qDetail in QDetails)
            {
                if (qDetail.CODE == string_0)
                {
                    result = qDetail.CODE_TEXT;
                    break;
                }
            }
            return result;
        }

        public void GetOriginalDetails()
        {
            QDetails = oSurveyDetailDal.GetDetails(QDefine.DETAIL_ID);
        }

        public void ResetOtherCode()
        {
            OtherCode = "";
            foreach (SurveyDetail qDetail in QDetails)
            {
                if (qDetail.IS_OTHER == 1)
                {
                    OtherCode = qDetail.CODE;
                    break;
                }
            }
        }

        public void GetGroupDetails()
        {
            QGroupDetails = oSurveyDetailDal.GetDetails(QDefine.PARENT_CODE);
        }

        public void InitDetailID(string string_0, int int_0)
        {
            string _003F40_003F = "";
            if (QDefine.DETAIL_ID != "")
            {
                if (QDefine.PARENT_CODE == "" || int_0 == 0)
                {
                    QDetails = oSurveyDetailDal.GetDetails(QDefine.DETAIL_ID, out _003F40_003F);
                }
                else if (!(QDefine.PARENT_CODE == "DYNAMIC") && int_0 > 0)
                {
                    QDetails = oSurveyDetailDal.GetList(QDefine.DETAIL_ID, QDefine.PARENT_CODE);
                }
            }
        }

        public void InitRelation(string string_0, int int_0)
        {
            QInitDateTime = DateTime.Now;
            string _003F40_003F = "";
            QDefine = oSurveyDefineDal.GetByPageId(string_0, int_0);
            QuestionName = QDefine.QUESTION_NAME;
            ParentCode = QDefine.PARENT_CODE;
            QDetailsParent = oSurveyDetailDal.GetDetails(QDefine.PARENT_CODE);
            QDetails = oSurveyDetailDal.GetDetails(QDefine.DETAIL_ID, out _003F40_003F);
            OtherCode = _003F40_003F;
            int num = 0;
            while (true)
            {
                if (num >= QDetails.Count)
                {
                    return;
                }
                if (QDetails[num].IS_OTHER == 2)
                {
                    break;
                }
                num++;
            }
            NoneCode = QDetails[num].CODE;
            NoneCodeText = QDetails[num].CODE_TEXT;
            QDetails.RemoveAt(num);
        }

        public void GetRelation2(string string_0)
        {
            Class2 @class = new Class2();
            @class.Level1Code = string_0;
            IOrderedEnumerable<SurveyDetail> source = QDetails.Where(@class.method_0).OrderBy(Class3._003C_003E9.method_0);
            QGroupDetails = source.ToList();
            OtherCode = "";
            foreach (SurveyDetail qGroupDetail in QGroupDetails)
            {
                if (qGroupDetail.IS_OTHER == 1)
                {
                    OtherCode = qGroupDetail.CODE;
                    break;
                }
            }
        }
    }
}
