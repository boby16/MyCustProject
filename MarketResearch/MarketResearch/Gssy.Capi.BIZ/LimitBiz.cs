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
                _003F10_003F = string.Format("SELECT * FROM SurveyAnswer WHERE SURVEY_ID ='{0}' AND (QUESTION_NAME='{1}' OR QUESTION_NAME LIKE '{1}_R%' OR QUESTION_NAME LIKE '{1}_A%' )  AND QUESTION_NAME<>'{1}_OTH'", string_0, arg);
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
                    text = ((k != LimitCodes.Count - 1) ? (text + "'" + LimitCodes[k].ToString() + "',") : (text + "'" + LimitCodes[k].ToString() + "'"));
                }
                switch (int_0)
                {
                    case 2:
                        _003F10_003F = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE NOT IN ({1}) ORDER BY INNER_ORDER", string_2, text);
                        break;
                    case 1:
                        _003F10_003F = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE IN ({1}) ORDER BY INNER_ORDER", string_2, text);
                        break;
                }
                QDetails = oSurveyDetailDal.GetListBySql(_003F10_003F);
                string _003F20_003F = array[0] + "_OTH";
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
                if (!(a == "5"))
                {
                    if (!(a == "4"))
                    {
                        if (!(a == "3"))
                        {
                            if (a == "2")
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
            _003F20_003F = string_1 + "_R97";
            oneCode = oSurveyAnswerDal.GetOneCode(string_0, _003F20_003F);
            string a2 = oneCode;
            if (!(a2 == "5"))
            {
                if (!(a2 == "4"))
                {
                    if (!(a2 == "3"))
                    {
                        if (a2 == "2")
                        {
                            list4.Add("97");
                        }
                    }
                    else
                    {
                        list3.Add("97");
                    }
                }
                else
                {
                    list2.Add("97");
                }
            }
            else
            {
                list.Add("97");
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
                    text2 = ((j != LimitCodes.Count - 1) ? (text2 + "'" + LimitCodes[j].ToString() + "',") : (text2 + "'" + LimitCodes[j].ToString() + "'"));
                }
                text = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE IN ({1}) ORDER BY INNER_ORDER", string_2, text2);
                QDetails = oSurveyDetailDal.GetListBySql(text);
                string _003F20_003F2 = string_1 + "_R97_OTH";
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
                    text2 = ((j != LimitCodes.Count - 1) ? (text2 + "'" + LimitCodes[j].ToString() + "',") : (text2 + "'" + LimitCodes[j].ToString() + "'"));
                }
                text = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE NOT IN ({1}) ORDER BY INNER_ORDER", string_2, text2);
                QDetails = oSurveyDetailDal.GetListBySql(text);
                string _003F20_003F2 = str + "_R97_OTH";
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
                if (!(a == "5"))
                {
                    if (!(a == "4"))
                    {
                        if (a == "3")
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
            LimitCodes.Add("18");
            if (LimitCodes.Count != 0)
            {
                for (int j = 0; j < LimitCodes.Count; j++)
                {
                    text2 = ((j != LimitCodes.Count - 1) ? (text2 + "'" + LimitCodes[j].ToString() + "',") : (text2 + "'" + LimitCodes[j].ToString() + "'"));
                }
                text = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE IN ({1}) ORDER BY INNER_ORDER", string_2, text2);
                QDetails = oSurveyDetailDal.GetListBySql(text);
                string _003F20_003F2 = str + "_R97_OTH";
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
                if (text3 == "B3")
                {
                    text2 = string.Format("SELECT * FROM SurveyAnswer WHERE SURVEY_ID ='{0}' AND QUESTION_NAME LIKE '{1}_A%'", string_0, text3);
                    List<SurveyAnswer> list = new List<SurveyAnswer>();
                    list = oSurveyAnswerDal.GetListBySql(text2);
                    if (list.Count != 0)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            SurveyDetail surveyDetail = new SurveyDetail();
                            surveyDetail = oSurveyDetailDal.GetOne(text3, list[j].CODE.ToString());
                            text = surveyDetail.EXTEND_1;
                            if (text != "" && text != "18" && text != null)
                            {
                                LimitCodes.Add(text);
                            }
                        }
                    }
                }
                else
                {
                    text = oSurveyAnswerDal.GetOneCode(string_0, text3);
                    if (text != "" && text != "18" && text != null)
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
            string oneCode = oSurveyAnswerDal.GetOneCode(string_0, "S5_品牌");
            text = oSurveyAnswerDal.GetOneCode(string_0, string_1);
            if (text == "2" || text == "4" || text == "6")
            {
                _003F10_003F = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE='{1}' ORDER BY INNER_ORDER", string_2, oneCode);
            }
            if (text == "3" || text == "5" || text == "7")
            {
                _003F10_003F = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE<>'{1}' ORDER BY INNER_ORDER", string_2, oneCode);
            }
            if (!(text == "9") && !(text == "8"))
            {
                QDetails = oSurveyDetailDal.GetListBySql(_003F10_003F);
                return true;
            }
            return false;
        }

        public bool ReBuildDetails(string string_0, string string_1, string string_2, string string_3)
        {
            bool result = true;
            if (!(string_2 == "IN"))
            {
                if (!(string_2 == "OUT"))
                {
                    if (!(string_2 == "IN|OUT"))
                    {
                        if (string_2 == "IN|OUT|OUT")
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
            string _003F10_003F = string.Format("SELECT * FROM SurveyAnswer WHERE SURVEY_ID ='{0}' AND (QUESTION_NAME='{1}' OR QUESTION_NAME LIKE '{1}_R%' OR QUESTION_NAME LIKE '{1}_A%' )  AND QUESTION_NAME<>'{1}_OTH'", string_0, string_1);
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            list = oSurveyAnswerDal.GetListBySql(_003F10_003F);
            if (list.Count != 0)
            {
                string text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    text = ((i != list.Count - 1) ? (text + "'" + list[i].CODE.ToString() + "',") : (text + "'" + list[i].CODE.ToString() + "'"));
                }
                if (!(string_2 == "IN"))
                {
                    if (string_2 == "OUT")
                    {
                        _003F10_003F = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE NOT IN ({1}) ORDER BY INNER_ORDER", string_3, text);
                    }
                }
                else
                {
                    _003F10_003F = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE IN ({1}) ORDER BY INNER_ORDER", string_3, text);
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
            string _003F10_003F = string.Format("SELECT * FROM SurveyAnswer WHERE SURVEY_ID ='{0}' AND (QUESTION_NAME='{1}' OR QUESTION_NAME LIKE '{1}_R%' OR QUESTION_NAME LIKE '{1}_A%' )  AND QUESTION_NAME<>'{1}_OTH'", string_0, array[0]);
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            list = oSurveyAnswerDal.GetListBySql(_003F10_003F);
            if (list.Count != 0)
            {
                string text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    text = ((i != list.Count - 1) ? (text + "'" + list[i].CODE.ToString() + "',") : (text + "'" + list[i].CODE.ToString() + "'"));
                }
                _003F10_003F = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE IN ({1}) ORDER BY INNER_ORDER", string_3, text);
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
            string _003F10_003F = string.Format("SELECT * FROM SurveyAnswer WHERE SURVEY_ID ='{0}' AND (QUESTION_NAME='{1}' OR QUESTION_NAME LIKE '{1}_R%' OR QUESTION_NAME LIKE '{1}_A%' )  AND QUESTION_NAME<>'{1}_OTH'", string_0, array[0]);
            List<SurveyAnswer> list = new List<SurveyAnswer>();
            list = oSurveyAnswerDal.GetListBySql(_003F10_003F);
            if (list.Count != 0)
            {
                string text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    text = ((i != list.Count - 1) ? (text + "'" + list[i].CODE.ToString() + "',") : (text + "'" + list[i].CODE.ToString() + "'"));
                }
                _003F10_003F = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE IN ({1}) ORDER BY INNER_ORDER", string_3, text);
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
            if (string_1 == "Q22")
            {
                if (oneCode == "1")
                {
                    text = "5";
                    text2 = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE<>'{1}' ORDER BY INNER_ORDER", string_4, text);
                }
                else
                {
                    text = "1";
                    text2 = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND CODE<>'{1}' ORDER BY INNER_ORDER", string_4, text);
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
                    LimitOtherCodeText = oSurveyAnswerDal.GetOneCode(string_0, LimitFirstINQName + "_OTH");
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
                    LimitAddFillCodeText = oSurveyAnswerDal.GetOneCode(string_0, LimitFirstINQName + "_A" + str + "_OTH");
                }
                return flag;
            }
            return false;
        }

        public string GetFixOther(string string_0, string string_1, string string_2)
        {
            string _003F10_003F = string.Format("SELECT * FROM SurveyAnswer WHERE SURVEY_ID ='{0}' AND QUESTION_NAME='{1}'", string_0, string_1);
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
            if (string_3 == "2")
            {
                result = string_1;
            }
            return result;
        }
    }
}
