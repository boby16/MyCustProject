using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Gssy.Capi.BIZ
{
    public class RandomEngine
    {
        [Serializable]
        [CompilerGenerated]
        private sealed class Class8
        {
            public static readonly Class8 _003C_003E9 = new Class8();

            internal Random method_0()
            {
                return new Random(DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Day);
            }
        }

        public Random _Random = new Random(DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Day);

        private List<string> _ID = new List<string>();

        private List<int> _Index = new List<int>();

        private List<string> _Group1 = new List<string>();

        private List<string> _Group2 = new List<string>();

        private List<string> _Group3 = new List<string>();

        private List<bool> _Fix = new List<bool>();

        private List<int> _GroupIndex1 = new List<int>();

        private List<int> _GroupIndex2 = new List<int>();

        private List<int> _GroupIndex3 = new List<int>();

        private int _Count = 0;

        private int _MaxIndex = 0;

        private static readonly ThreadLocal<Random> appRandom = new ThreadLocal<Random>(Class8._003C_003E9.method_0);

        public List<SurveyRandom> RandomOption(List<SurveyRandom> list_0)
        {
            method_4(list_0);
            method_0();
            return method_5(list_0);
        }

        public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
        {
            method_7(list_0);
            method_0();
            return method_8(list_0);
        }

        public double RND(double double_0 = 0.0, double double_1 = 1.0)
        {
            if (double_0 > double_1)
            {
                double num = double_1;
                double_1 = double_0;
                double_0 = num;
            }
            double num2 = appRandom.Value.NextDouble();
            return num2 * (double_1 - double_0) + double_0;
        }

        public int intRND(int int_0 = 0, int int_1 = 999999999)
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

        public string strRND(string string_0 = "13000000000", string string_1 = "18999999999")
        {
            string text = "";
            int length = string_1.Length;
            long num = Convert.ToInt64(string_0);
            long num2 = Convert.ToInt64(string_1);
            if (num > num2)
            {
                long num3 = num2;
                num2 = num;
                num = num3;
            }
            double num4 = appRandom.Value.NextDouble();
            num4 = num4 * (double)(num2 - num + 1L) + (double)num;
            if (num4 > (double)num2)
            {
                num4 = (double)num2;
            }
            if (num4 < (double)num)
            {
                num4 = (double)num;
            }
            text = ((long)Math.Truncate(num4)).ToString();
            return FillString(text, GClass0.smethod_0("1"), length, true);
        }

        public List<double> RandomList(int int_0 = 1, double double_0 = 0.0, double double_1 = 1.0)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < int_0; i++)
            {
                list.Add(RND(double_0, double_1));
            }
            return list;
        }

        public List<int> intRandomList(int int_0 = 1, int int_1 = 1, int int_2 = -1)
        {
            List<int> list = new List<int>();
            int num = (int_2 == -1) ? int_0 : int_2;
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            int num2 = (int_0 > num) ? int_0 : num;
            for (int i = int_1; i <= num2; i++)
            {
                dictionary.Add(i, intRND(0, 999999999));
            }
            Dictionary<int, int> dictionary2 = method_9(dictionary, false, false);
            if (int_0 > num)
            {
                for (int j = 0; j < int_0; j++)
                {
                    int num3 = dictionary2.Values.ElementAt(j) % num;
                    list.Add((num3 < int_1) ? num : num3);
                }
            }
            else
            {
                for (int k = 0; k < int_0; k++)
                {
                    list.Add(dictionary2.Values.ElementAt(k));
                }
            }
            return list;
        }

        public Dictionary<string, int> RandomDictSI(Dictionary<string, int> dictionary_0, int int_0 = 1, int int_1 = -1)
        {
            int count = dictionary_0.Count;
            int num = (int_1 == -1) ? count : int_1;
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            int num2 = (count > num) ? count : num;
            for (int i = int_0; i <= num2; i++)
            {
                dictionary.Add(i, intRND(0, 999999999));
            }
            Dictionary<int, int> dictionary2 = method_9(dictionary, false, false);
            if (count > num)
            {
                for (int j = 0; j < count; j++)
                {
                    int num3 = dictionary2.Values.ElementAt(j) % num;
                    string key = dictionary_0.Keys.ElementAt(j);
                    dictionary_0[key] = ((num3 < int_0) ? num : num3);
                }
            }
            else
            {
                for (int k = 0; k < count; k++)
                {
                    string key2 = dictionary_0.Keys.ElementAt(k);
                    dictionary_0[key2] = dictionary2.Values.ElementAt(k);
                }
            }
            return dictionary_0;
        }

        public Dictionary<int, int> RandomDictII(Dictionary<int, int> dictionary_0, int int_0 = 1, int int_1 = -1)
        {
            int count = dictionary_0.Count;
            int num = (int_1 == -1) ? count : int_1;
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            int num2 = (count > num) ? count : num;
            for (int i = int_0; i <= num2; i++)
            {
                dictionary.Add(i, intRND(0, 999999999));
            }
            Dictionary<int, int> dictionary2 = method_9(dictionary, false, false);
            if (count > num)
            {
                for (int j = 0; j < count; j++)
                {
                    int num3 = dictionary2.Values.ElementAt(j) % num;
                    int key = dictionary_0.Keys.ElementAt(j);
                    dictionary_0[key] = ((num3 < int_0) ? num : num3);
                }
            }
            else
            {
                for (int k = 0; k < count; k++)
                {
                    int key2 = dictionary_0.Keys.ElementAt(k);
                    dictionary_0[key2] = dictionary2.Values.ElementAt(k);
                }
            }
            return dictionary_0;
        }

        public List<string> StringListRandom(List<string> list_0)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < list_0.Count; i++)
            {
                list.Add("");
            }
            List<int> list2 = intRandomList(list_0.Count, 1, -1);
            for (int j = 0; j < list_0.Count; j++)
            {
                list[list2[j] - 1] = list_0[j];
            }
            return list;
        }

        public List<int> IntListRandom(List<int> list_0)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < list_0.Count; i++)
            {
                list.Add(0);
            }
            List<int> list2 = intRandomList(list_0.Count, 1, -1);
            for (int j = 0; j < list_0.Count; j++)
            {
                list[list2[j] - 1] = list_0[j];
            }
            return list;
        }

        public List<double> DoubleListRandom(List<double> list_0)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < list_0.Count; i++)
            {
                list.Add(0.0);
            }
            List<int> list2 = intRandomList(list_0.Count, 1, -1);
            for (int j = 0; j < list_0.Count; j++)
            {
                list[list2[j] - 1] = list_0[j];
            }
            return list;
        }

        private void method_0()
        {
            _MaxIndex = 0;
            List<int> list = new List<int>();
            for (int i = 0; i < _Group3.Count; i++)
            {
                list.Add(i);
            }
            _GroupIndex3 = method_1(list, _Group3, _GroupIndex3);
            for (int j = 1; j <= _GroupIndex3[_Count]; j++)
            {
                list.Clear();
                for (int k = 0; k < _Group2.Count; k++)
                {
                    if (_GroupIndex3[k] == j)
                    {
                        list.Add(k);
                    }
                }
                _GroupIndex2 = method_1(list, _Group2, _GroupIndex2);
                for (int l = 1; l <= _GroupIndex2[_Count]; l++)
                {
                    list.Clear();
                    for (int m = 0; m < _Group1.Count; m++)
                    {
                        if (_GroupIndex2[m] == l)
                        {
                            list.Add(m);
                        }
                    }
                    _GroupIndex1 = method_1(list, _Group1, _GroupIndex1);
                    for (int n = 1; n <= _GroupIndex1[_Count]; n++)
                    {
                        list.Clear();
                        for (int num = 0; num < _Index.Count; num++)
                        {
                            if (_GroupIndex1[num] == n)
                            {
                                list.Add(num);
                            }
                        }
                        method_2(list);
                    }
                }
            }
        }

        private List<int> method_1(List<int> list_0, List<string> list_1, List<int> list_2)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
            string text = "";
            int num = 0;
            foreach (int item in list_0)
            {
                text = list_1[item];
                if (!dictionary.Keys.Contains(text))
                {
                    dictionary.Add(text, ++num);
                    if (method_10(text, 1) != GClass0.smethod_0(","))
                    {
                        dictionary2.Add(num, intRND(0, 999999999));
                    }
                }
            }
            list_2[_Count] = dictionary.Count;
            if (dictionary2.Count > 1)
            {
                Dictionary<int, int> dictionary3 = method_9(dictionary2, false, false);
                int num2 = 0;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    string text2 = dictionary.Keys.ElementAt(i);
                    if (method_10(text2, 1) != GClass0.smethod_0(","))
                    {
                        dictionary[text2] = dictionary3.Values.ElementAt(num2++);
                    }
                }
            }
            for (int j = 0; j < _Count; j++)
            {
                if (list_0.Contains(j))
                {
                    text = list_1[j];
                    list_2[j] = dictionary[text];
                }
                else
                {
                    list_2[j] = -1;
                }
            }
            return list_2;
        }

        private void method_2(List<int> list_0)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            foreach (int item in list_0)
            {
                _Index[item] = ++_MaxIndex;
                if (!_Fix[item])
                {
                    dictionary.Add(_MaxIndex, intRND(0, 999999999));
                }
            }
            if (dictionary.Count() > 1)
            {
                Dictionary<int, int> dictionary2 = method_9(dictionary, false, false);
                int num = 0;
                foreach (int item2 in list_0)
                {
                    if (!_Fix[item2])
                    {
                        _Index[item2] = dictionary2.Values.ElementAt(num++);
                    }
                }
            }
        }

        private int method_3(List<SurveyRandom> list_0, string string_0)
        {
            int num = 0;
            while (true)
            {
                if (num >= list_0.Count)
                {
                    return -1;
                }
                if (list_0[num].ID.ToString() == string_0)
                {
                    break;
                }
                num++;
            }
            return num;
        }

        private void method_4(List<SurveyRandom> list_0)
        {
            int count = list_0.Count;
            for (int i = 0; i < count; i++)
            {
                _ID.Add(list_0[i].ID.ToString());
                _Index.Add(i + 1);
                _Group1.Add(list_0[i].RANDOM_SET1.ToString());
                _Group2.Add(list_0[i].RANDOM_SET2.ToString());
                _Group3.Add(list_0[i].RANDOM_SET3.ToString());
                _Fix.Add((list_0[i].IS_FIX != 0) ? true : false);
                _GroupIndex1.Add(-1);
                _GroupIndex2.Add(-1);
                _GroupIndex3.Add(-1);
            }
            _Count = _Index.Count();
            _GroupIndex1.Add(_Count);
            _GroupIndex2.Add(_Count);
            _GroupIndex3.Add(_Count);
        }

        private List<SurveyRandom> method_5(List<SurveyRandom> list_0)
        {
            int count = _Count;
            for (int i = 0; i < count; i++)
            {
                int num = method_3(list_0, _ID[i]);
                if (num > -1)
                {
                    list_0[num].RANDOM_INDEX = Convert.ToInt32(_Index[i]);
                }
                else
                {
                    SurveyRandom surveyRandom = new SurveyRandom();
                    surveyRandom.ID = Convert.ToInt32(_ID[i]);
                    surveyRandom.RANDOM_INDEX = Convert.ToInt32(_Index[i]);
                    surveyRandom.RANDOM_SET1 = Convert.ToInt32(_Group1[i]);
                    surveyRandom.RANDOM_SET2 = Convert.ToInt32(_Group2[i]);
                    surveyRandom.RANDOM_SET3 = Convert.ToInt32(_Group3[i]);
                    surveyRandom.IS_FIX = (_Fix[i] ? 1 : 0);
                    list_0.Add(surveyRandom);
                }
            }
            return list_0;
        }

        private int method_6(List<SurveyDetail> list_0, string string_0)
        {
            int num = 0;
            while (true)
            {
                if (num >= list_0.Count)
                {
                    return -1;
                }
                if (list_0[num].ID.ToString() == string_0)
                {
                    break;
                }
                num++;
            }
            return num;
        }

        private void method_7(List<SurveyDetail> list_0)
        {
            int count = list_0.Count;
            for (int i = 0; i < count; i++)
            {
                _ID.Add(list_0[i].ID.ToString());
                _Index.Add(i + 1);
                _Group1.Add(list_0[i].RANDOM_SET.ToString());
                _Group2.Add(list_0[i].EXTEND_9.ToString());
                _Group3.Add(list_0[i].EXTEND_10.ToString());
                _Fix.Add((list_0[i].RANDOM_FIX != 0) ? true : false);
                _GroupIndex1.Add(-1);
                _GroupIndex2.Add(-1);
                _GroupIndex3.Add(-1);
            }
            _Count = _Index.Count();
            _GroupIndex1.Add(_Count);
            _GroupIndex2.Add(_Count);
            _GroupIndex3.Add(_Count);
        }

        private List<SurveyDetail> method_8(List<SurveyDetail> list_0)
        {
            List<SurveyDetail> list = new List<SurveyDetail>();
            for (int i = 1; i <= _Count; i++)
            {
                int num = -1;
                for (int j = 0; j < _Index.Count; j++)
                {
                    if (_Index[j] == i)
                    {
                        num = j;
                        break;
                    }
                }
                if (num > -1)
                {
                    list.Add(list_0[num]);
                }
            }
            return list;
        }

        private Dictionary<int, int> method_9(Dictionary<int, int> dictionary_0, bool bool_0 = false, bool bool_1 = false)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 1; i <= dictionary_0.Count; i++)
            {
                list.Add(1);
                dictionary.Add(i, 0);
                list2.Add(0);
            }
            for (int j = 0; j < dictionary_0.Count - 1; j++)
            {
                int num = dictionary_0.Values.ElementAt(j);
                for (int k = j + 1; k < dictionary_0.Count; k++)
                {
                    int num2 = dictionary_0.Values.ElementAt(k);
                    if (bool_1)
                    {
                        if (num > num2)
                        {
                            List<int> list3 = list;
                            int index = k;
                            list3[index]++;
                        }
                        if (num < num2)
                        {
                            List<int> list4 = list;
                            int index2 = j;
                            list4[index2]++;
                        }
                    }
                    else
                    {
                        if (num < num2)
                        {
                            List<int> list5 = list;
                            int index = k;
                            list5[index]++;
                        }
                        if (num > num2)
                        {
                            List<int> list6 = list;
                            int index2 = j;
                            list6[index2]++;
                        }
                    }
                }
            }
            if (bool_0)
            {
                dictionary.Clear();
                int i = 0;
                foreach (int key in dictionary_0.Keys)
                {
                    dictionary.Add(key, list[i++]);
                }
            }
            else
            {
                int num4 = 0;
                for (int i = 0; i < list.Count(); i++)
                {
                    int num5 = list[i];
                    List<int> list7 = list2;
                    int index = num5 - 1;
                    dictionary[num5 + list7[index]++] = dictionary_0.Keys.ElementAt(i);
                }
            }
            return dictionary;
        }

        private string method_10(string string_0, int int_0 = 1)
        {
            int num = (int_0 >= 0) ? int_0 : 0;
            return string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
        }

        private string method_11(string string_0, int int_0, int int_1 = -9999)
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

        public string RIGHT(string string_0, int int_0 = 1)
        {
            if (string_0.Length != 0)
            {
                int num = (int_0 >= 0) ? int_0 : 0;
                return string_0.Substring((num <= string_0.Length) ? (string_0.Length - num) : 0);
            }
            return "";
        }

        public string FillString(string string_0, string string_1, int int_0, bool bool_0 = true)
        {
            string text = string_0;
            if (text.Length < int_0)
            {
                for (int i = 0; i < int_0; i++)
                {
                    text += string_1;
                }
                text = ((!bool_0) ? method_10(string_0 + text, int_0) : RIGHT(text + string_0, int_0));
            }
            return text;
        }
    }
}
