using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class V_AnswerDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("xůɥͭѤղ؅ݧ࡬ॷ੯୴షഴิ༼ၝᅈቖፕᐷᕀᙊ᝕᡽ᥡᩦ᭵ᱽᴮṚὄ⁎⅘≌⌨⑎╂☥✹⡸⤲⩼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_Answer GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷเཊၕᅽቡ፦ᑵᕽᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), int_0);
			return this.GetBySql(string_);
		}

		public V_Answer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_Answer v_Answer = new V_Answer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_Answer.ANSWER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]);
					v_Answer.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					v_Answer.CODE = dataReader["CODE"].ToString();
					v_Answer.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_Answer.SPSS_VARIABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]);
				}
			}
			return v_Answer;
		}

		public List<V_Answer> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_Answer> list = new List<V_Answer>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_Answer
					{
						ANSWER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]),
						QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						CODE = dataReader["CODE"].ToString(),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SPSS_VARIABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")])
					});
				}
			}
			return list;
		}

		public List<V_Answer> GetList()
		{
			string string_ = GClass0.smethod_0("\u007fŮɦͬѫճ؆܏ࠄ॥ੰ୮౭ി่གၝᅵቩ፮ᑽᕥᘶ᝚ᡆᥗᩗᭃᰰᵍṗἭ⁍ⅅ≙⍞⑍╕♙❊⡖⥇⩇⭓");
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 5;
			string[] array = new string[5];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("颞矫趗勹鱸宎");
				array[1] = GClass0.smethod_0("闦馟紐僲﬌庝遇");
				array[2] = GClass0.smethod_0("缐礄ﴌ硗汊行");
				array[3] = "问卷编号";
				array[4] = GClass0.smethod_0("ZŘɔ͕Х囜韌筹徊");
			}
			else
			{
				array[0] = GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓");
				array[1] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[2] = "CODE";
				array[3] = "SURVEY_ID";
				array[4] = GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<V_Answer> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_Answer v_Answer in list_0)
			{
				array[num, 0] = v_Answer.ANSWER_ORDER.ToString();
				array[num, 1] = v_Answer.QUESTION_NAME;
				array[num, 2] = v_Answer.CODE;
				array[num, 3] = v_Answer.SURVEY_ID;
				array[num, 4] = v_Answer.SPSS_VARIABLE.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
