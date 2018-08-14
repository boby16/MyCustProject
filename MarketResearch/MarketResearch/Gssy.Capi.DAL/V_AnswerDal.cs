using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000019 RID: 25
	public class V_AnswerDal
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00011590 File Offset: 0x0000F790
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("xůɥͭѤղ؅ݧ࡬ॷ੯୴షഴิ༼ၝᅈቖፕᐷᕀᙊ᝕᡽ᥡᩦ᭵ᱽᴮṚὄ⁎⅘≌⌨⑎╂☥✹⡸⤲⩼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000115D0 File Offset: 0x0000F7D0
		public V_Answer GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷเཊၕᅽቡ፦ᑵᕽᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000115FC File Offset: 0x0000F7FC
		public V_Answer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_Answer v_Answer = new V_Answer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_Answer.ANSWER_ORDER = Convert.ToInt32(dataReader[global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]);
					v_Answer.QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					v_Answer.CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString();
					v_Answer.SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString();
					v_Answer.SPSS_VARIABLE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]);
				}
			}
			return v_Answer;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000116D8 File Offset: 0x0000F8D8
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
						ANSWER_ORDER = Convert.ToInt32(dataReader[global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]),
						QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString(),
						SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString(),
						SPSS_VARIABLE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")])
					});
				}
			}
			return list;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public List<V_Answer> GetList()
		{
			string string_ = global::GClass0.smethod_0("\u007fŮɦͬѫճ؆܏ࠄ॥ੰ୮౭ി่གၝᅵቩ፮ᑽᕥᘶ᝚ᡆᥗᩗᭃᰰᵍṗἭ⁍ⅅ≙⍞⑍╕♙❊⡖⥇⩇⭓");
			return this.GetListBySql(string_);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 5;
			string[] array = new string[5];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("颞矫趗勹鱸宎");
				array[1] = global::GClass0.smethod_0("闦馟紐僲﬌庝遇");
				array[2] = global::GClass0.smethod_0("缐礄ﴌ硗汊行");
				array[3] = global::GClass0.smethod_0("闪剴純僶");
				array[4] = global::GClass0.smethod_0("ZŘɔ͕Х囜韌筹徊");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓");
				array[1] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[2] = global::GClass0.smethod_0("GŌɆ̈́");
				array[3] = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
				array[4] = global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ");
			}
			return array;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00011888 File Offset: 0x0000FA88
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

		// Token: 0x04000022 RID: 34
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
