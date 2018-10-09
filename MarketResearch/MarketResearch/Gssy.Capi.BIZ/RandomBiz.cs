using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Gssy.Capi.BIZ
{
    public class RandomBiz
    {
        [CompilerGenerated]
        private sealed class Class4
        {
            public int iCurGroup;

            internal bool method_0(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET1 == iCurGroup;
            }
        }

        [Serializable]
        [CompilerGenerated]
        private sealed class Class5
        {
            public static readonly Class5 _003C_003E9 = new Class5();

            public static Func<SurveyRandom, bool> _003C_003E9__16_1;

            public static Func<SurveyRandom, int> _003C_003E9__17_0;

            public static Func<SurveyRandom, int> _003C_003E9__17_1;

            public static Func<SurveyRandom, int> _003C_003E9__18_0;

            public static Func<SurveyRandom, int> _003C_003E9__18_1;

            public static Func<SurveyRandom, int> _003C_003E9__19_0;

            public static Func<SurveyRandom, int> _003C_003E9__19_1;

            internal bool method_0(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.IS_FIX == 0;
            }

            internal int method_1(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET1;
            }

            internal int method_2(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET2;
            }

            internal int method_3(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET1;
            }

            internal int method_4(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET2;
            }

            internal int method_5(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET1;
            }

            internal int method_6(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET2;
            }
        }

        [CompilerGenerated]
        private sealed class Class6
        {
            public int iCurGroup;

            internal bool method_0(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET1 == iCurGroup;
            }
        }

        [CompilerGenerated]
        private sealed class Class7
        {
            public int iCurGroup;

            internal bool method_0(SurveyRandom surveyRandom_0)
            {
                return surveyRandom_0.RANDOM_SET2 == iCurGroup;
            }
        }

        private List<SurveyRandom> oRandomList;

        private List<SurveyRandom> oRandomSet;

        private int BaseCount;

        private SurveyRandomBaseDal oSurveyRandomBaseDal = new SurveyRandomBaseDal();

        private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

        private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

        private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

        public void CopyRandom(string string_0)
        {
            List<SurveyRandom> list = oSurveyRandomBaseDal.GetList();
            BaseCount = list.Count();
            oSurveyRandomDal.CopyRandom(string_0, list);
        }

        public bool CheckBaseCopyOK(string string_0, int int_0)
        {
            return oSurveyRandomDal.CheckBaseCopyOK(string_0, int_0);
        }

        public void DeleteRandom(string string_0)
        {
            oSurveyRandomDal.DeleteRandom(string_0);
        }

        public bool RandomSurveyMain(string string_0)
        {
            List<SurveyRandom> list = new List<SurveyRandom>();
            list = oSurveyRandomBaseDal.GetGroupInfo();
            int num = 0;
            foreach (SurveyRandom item in list)
            {
                List<SurveyRandom> list2 = new List<SurveyRandom>();
                list2 = oSurveyRandomBaseDal.GetList(item.QUESTION_SET);
                num += item.RANDOM_INDEX;
                oSurveyRandomDal.AddList(string_0, list2);
            }
            Thread.Sleep(2000);
            bool result = CheckBaseCopyOK(string_0, num);
            foreach (SurveyRandom item2 in list)
            {
                SurveyDefine byPageId = oSurveyDefineDal.GetByPageId(item2.QUESTION_SET, 0);
                if (byPageId.IS_RANDOM == 1)
                {
                    RandomIndex(string_0, item2.QUESTION_SET);
                }
            }
            return result;
        }

        public void RandomIndex(string string_0, string string_1)
        {
            List<SurveyRandom> list = oSurveyRandomDal.GetList(string_0, string_1);
            RandomEngine randomEngine = new RandomEngine();
            List<SurveyRandom> _003F74_003F = randomEngine.RandomOption(list);
            oSurveyRandomDal.UpdateRandom(_003F74_003F);
        }

        public void RebuildCircleGuide(string string_0, string string_1, string[] string_2, int int_0)
        {
            string _003F10_003F = string.Format("UPDATE SurveyRandom SET QUESTION_NAME='JUMP',RANDOM_INDEX=0 WHERE SURVEY_ID='{0}' AND QUESTION_SET='{1}'", string_0, string_1);
            oSurveyRandomDal.RunSQL(_003F10_003F);
            for (int i = 0; i < string_2.Count(); i++)
            {
                _003F10_003F = string.Format("UPDATE SurveyRandom SET QUESTION_NAME=QUESTION_SET, RANDOM_INDEX={3}  WHERE SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND CODE='{2}'", string_0, string_1, string_2[i].ToString(), (i + 1).ToString());
                oSurveyRandomDal.RunSQL(_003F10_003F);
            }
            if (int_0 == 1)
            {
                List<SurveyRandom> listNoJump = oSurveyRandomDal.GetListNoJump(string_0, string_1);
                RandomEngine randomEngine = new RandomEngine();
                List<SurveyRandom> _003F74_003F = randomEngine.RandomOption(listNoJump);
                oSurveyRandomDal.UpdateRandom(_003F74_003F);
            }
        }

        public void RandomIndexTwo(string string_0, string string_1)
        {
            oRandomSet = oSurveyRandomDal.GetListNoFix(string_0, string_1);
            if (DateTime.Now.Second % 2 == 0)
            {
                oRandomSet[0].RANDOM_INDEX = 2;
                oRandomSet[1].RANDOM_INDEX = 1;
                oSurveyRandomDal.UpdateRandom(oRandomSet);
            }
        }

        public void RandomSet1_FixIndex(string string_0, string string_1)
        {
            List<SurveyRandom> listNoFix = oSurveyRandomDal.GetListNoFix(string_0, string_1);
            int groupCountNoFix = oSurveyRandomDal.GetGroupCountNoFix(string_0, string_1, 1);
            Random random = new Random((int)DateTime.Now.Ticks);
            int[] array = new int[groupCountNoFix];
            for (int i = 0; i < groupCountNoFix; i++)
            {
                array[i] = i + 1;
            }
            int num = 0;
            int num2 = 0;
            for (int j = 1; j < groupCountNoFix; j++)
            {
                num = random.Next(groupCountNoFix - j);
                num2 = array[groupCountNoFix - j];
                array[groupCountNoFix - j] = array[num];
                array[num] = num2;
            }
            for (int k = 1; k <= groupCountNoFix; k++)
            {
                for (int l = 0; l < listNoFix.Count(); l++)
                {
                    if (listNoFix[l].RANDOM_SET1 == k)
                    {
                        listNoFix[l].RANDOM_SET1 = 1000 + array[k - 1];
                    }
                }
            }
            for (int m = 0; m < listNoFix.Count(); m++)
            {
                listNoFix[m].RANDOM_SET1 = listNoFix[m].RANDOM_SET1 - 1000;
            }
            oSurveyRandomDal.UpdateRandom(listNoFix);
        }

        public void RandomIndex_FixSet1(string string_0, string string_1)
        {
            oRandomList = oSurveyRandomDal.GetListNoFix(string_0, string_1);
            int groupCountNoFix = oSurveyRandomDal.GetGroupCountNoFix(string_0, string_1, 1);
            Class4 @class = new Class4();
            @class.iCurGroup = 1;
            while (@class.iCurGroup <= groupCountNoFix)
            {
                List<SurveyRandom> source = oRandomList.Where(@class.method_0).ToList();
                source = source.Where(Class5._003C_003E9.method_0).ToList();
                oRandomSet = source.ToList();
                Random random = new Random((int)DateTime.Now.Ticks);
                int count = oRandomSet.Count;
                int num = 0;
                for (int i = 1; i < count; i++)
                {
                    num = random.Next(count - i);
                    int rANDOM_INDEX = oRandomSet[count - i].RANDOM_INDEX;
                    oRandomSet[count - i].RANDOM_INDEX = oRandomSet[num].RANDOM_INDEX;
                    oRandomSet[num].RANDOM_INDEX = rANDOM_INDEX;
                }
                oSurveyRandomDal.UpdateRandom(oRandomSet);
                @class.iCurGroup++;
            }
        }

        public void RandomLevel2(string string_0, string string_1, int int_0, int int_1)
        {
            oRandomList = oSurveyRandomDal.GetList(string_0, string_1);
            List<SurveyRandom> list = new List<SurveyRandom>();
            int[] array = new int[int_0];
            for (int i = 0; i < int_0; i++)
            {
                array[i] = i + 1;
            }
            Random random = new Random((int)DateTime.Now.Ticks);
            int num = 0;
            int num2 = 0;
            for (int j = 1; j < int_0; j++)
            {
                num = random.Next(int_0 - j);
                num2 = array[int_0 - j];
                array[int_0 - j] = array[num];
                array[num] = num2;
            }
            for (int k = 1; k <= int_0; k++)
            {
                for (int l = 0; l < oRandomList.Count(); l++)
                {
                    if (oRandomList[l].RANDOM_SET1 == k)
                    {
                        oRandomList[l].RANDOM_SET1 = 1000 + array[k - 1];
                    }
                }
            }
            IOrderedEnumerable<SurveyRandom> source = oRandomList.OrderBy(Class5._003C_003E9.method_1).ThenBy(Class5._003C_003E9.method_2);
            list = source.ToList();
            for (int m = 0; m < list.Count(); m++)
            {
                list[m].RANDOM_SET1 = list[m].RANDOM_SET1 - 1000;
                list[m].RANDOM_INDEX = m + 1;
            }
            oRandomList = list;
            Class6 @class = new Class6();
            @class.iCurGroup = 1;
            while (@class.iCurGroup <= int_0)
            {
                IEnumerable<SurveyRandom> source2 = oRandomList.Where(@class.method_0);
                oRandomSet = source2.ToList();
                int count = oRandomSet.Count;
                num = 0;
                num2 = 0;
                for (int n = 1; n < count; n++)
                {
                    num = random.Next(count - n);
                    num2 = oRandomSet[count - n].RANDOM_INDEX;
                    oRandomSet[count - n].RANDOM_INDEX = oRandomSet[num].RANDOM_INDEX;
                    oRandomSet[num].RANDOM_INDEX = num2;
                }
                oSurveyRandomDal.UpdateRandom(oRandomSet);
                @class.iCurGroup++;
            }
        }

        public void RandomLevel3(string string_0, string string_1, int int_0, int int_1, int int_2)
        {
            List<SurveyRandom> list = oSurveyRandomDal.GetList(string_0, string_1);
            List<SurveyRandom> list2 = new List<SurveyRandom>();
            List<SurveyRandom> list3 = new List<SurveyRandom>();
            new List<SurveyRandom>();
            Random random = new Random((int)DateTime.Now.Ticks);
            int[] array = new int[int_0];
            for (int i = 0; i < int_0; i++)
            {
                array[i] = i + 1;
            }
            int num = 0;
            int num2 = 0;
            for (int j = 1; j < int_0; j++)
            {
                num = random.Next(int_0 - j);
                num2 = array[int_0 - j];
                array[int_0 - j] = array[num];
                array[num] = num2;
            }
            for (int k = 1; k <= int_0; k++)
            {
                for (int l = 0; l < list.Count(); l++)
                {
                    if (list[l].RANDOM_SET1 == k)
                    {
                        list[l].RANDOM_SET1 = 1000 + array[k - 1];
                    }
                }
            }
            for (int m = 0; m < list.Count(); m++)
            {
                list[m].RANDOM_SET1 = list[m].RANDOM_SET1 - 1000;
            }
            int[] array2 = new int[int_1];
            for (int n = 0; n < int_1; n++)
            {
                array2[n] = n + 1;
            }
            num = 0;
            num2 = 0;
            for (int num3 = 1; num3 < int_1; num3++)
            {
                num = random.Next(int_1 - num3);
                num2 = array2[int_1 - num3];
                array2[int_1 - num3] = array2[num];
                array2[num] = num2;
            }
            for (int num4 = 1; num4 <= int_1; num4++)
            {
                for (int num5 = 0; num5 < list.Count(); num5++)
                {
                    if (list[num5].RANDOM_SET2 == num4)
                    {
                        list[num5].RANDOM_SET2 = 1000 + array2[num4 - 1];
                    }
                }
            }
            for (int num6 = 0; num6 < list.Count(); num6++)
            {
                list[num6].RANDOM_SET2 = list[num6].RANDOM_SET2 - 1000;
            }
            IOrderedEnumerable<SurveyRandom> source = list.OrderBy(Class5._003C_003E9.method_3).ThenBy(Class5._003C_003E9.method_4);
            list2 = source.ToList();
            for (int num7 = 0; num7 < list2.Count(); num7++)
            {
                list2[num7].RANDOM_INDEX = num7 + 1;
            }
            Class7 @class = new Class7();
            @class.iCurGroup = 1;
            while (@class.iCurGroup <= int_1)
            {
                IEnumerable<SurveyRandom> source2 = list2.Where(@class.method_0);
                list3 = source2.ToList();
                int count = list3.Count;
                if (count > 1)
                {
                    num = 0;
                    num2 = 0;
                    for (int num8 = 1; num8 < count; num8++)
                    {
                        num = random.Next(count - num8);
                        num2 = list3[count - num8].RANDOM_INDEX;
                        list3[count - num8].RANDOM_INDEX = list3[num].RANDOM_INDEX;
                        list3[num].RANDOM_INDEX = num2;
                    }
                }
                oSurveyRandomDal.UpdateRandom(list3);
                @class.iCurGroup++;
            }
        }

        public void RandomLevel2Fix3(string string_0, string string_1, int int_0, int int_1, int int_2)
        {
            List<SurveyRandom> list = oSurveyRandomDal.GetList(string_0, string_1);
            List<SurveyRandom> list2 = new List<SurveyRandom>();
            new List<SurveyRandom>();
            new List<SurveyRandom>();
            Random random = new Random((int)DateTime.Now.Ticks);
            int[] array = new int[int_0];
            for (int i = 0; i < int_0; i++)
            {
                array[i] = i + 1;
            }
            int num = 0;
            int num2 = 0;
            for (int j = 1; j < int_0; j++)
            {
                num = random.Next(int_0 - j);
                num2 = array[int_0 - j];
                array[int_0 - j] = array[num];
                array[num] = num2;
            }
            for (int k = 1; k <= int_0; k++)
            {
                for (int l = 0; l < list.Count(); l++)
                {
                    if (list[l].RANDOM_SET1 == k)
                    {
                        list[l].RANDOM_SET1 = 1000 + array[k - 1];
                    }
                }
            }
            for (int m = 0; m < list.Count(); m++)
            {
                list[m].RANDOM_SET1 = list[m].RANDOM_SET1 - 1000;
            }
            int[] array2 = new int[int_1];
            for (int n = 0; n < int_1; n++)
            {
                array2[n] = n + 1;
            }
            num = 0;
            num2 = 0;
            for (int num3 = 1; num3 < int_1; num3++)
            {
                num = random.Next(int_1 - num3);
                num2 = array2[int_1 - num3];
                array2[int_1 - num3] = array2[num];
                array2[num] = num2;
            }
            for (int num4 = 1; num4 <= int_1; num4++)
            {
                for (int num5 = 0; num5 < list.Count(); num5++)
                {
                    if (list[num5].RANDOM_SET2 == num4)
                    {
                        list[num5].RANDOM_SET2 = 1000 + array2[num4 - 1];
                    }
                }
            }
            for (int num6 = 0; num6 < list.Count(); num6++)
            {
                list[num6].RANDOM_SET2 = list[num6].RANDOM_SET2 - 1000;
            }
            IOrderedEnumerable<SurveyRandom> source = list.OrderBy(Class5._003C_003E9.method_5).ThenBy(Class5._003C_003E9.method_6);
            list2 = source.ToList();
            for (int num7 = 0; num7 < list2.Count(); num7++)
            {
                list2[num7].RANDOM_INDEX = num7 + 1;
            }
            oSurveyRandomDal.UpdateRandom(list2);
        }
    }
}
