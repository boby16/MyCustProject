using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x0200001E RID: 30
	public class V_WebTwoDal
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00013228 File Offset: 0x00011428
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("xůɥͭѤղ؅ݧ࡬ॷ੯୴షഴิ༼ၝᅈቖፕᐷᕀᙊᝃᡶᥰᩅ᭧ᱠᴮṚὄ⁎⅘≌⌨⑎╂☥✹⡸⤲⩼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00013268 File Offset: 0x00011468
		public V_WebTwo GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷเཊ၃ᅶተፅᑧᕠᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00013294 File Offset: 0x00011494
		public V_WebTwo GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_WebTwo v_WebTwo = new V_WebTwo();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_WebTwo.SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString();
					v_WebTwo.URI_DOMAIN = dataReader[global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ")].ToString();
					v_WebTwo.URI_DOMAIN_TWO = dataReader[global::GClass0.smethod_0("[şɅ͔юՆم݆ࡏोਜ਼ୗౕൎ")].ToString();
					v_WebTwo.STAY_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ")]);
				}
			}
			return v_WebTwo;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00013350 File Offset: 0x00011550
		public List<V_WebTwo> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_WebTwo> list = new List<V_WebTwo>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_WebTwo
					{
						SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString(),
						URI_DOMAIN = dataReader[global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ")].ToString(),
						URI_DOMAIN_TWO = dataReader[global::GClass0.smethod_0("[şɅ͔юՆم݆ࡏोਜ਼ୗౕൎ")].ToString(),
						STAY_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ")])
					});
				}
			}
			return list;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0001341C File Offset: 0x0001161C
		public List<V_WebTwo> GetList()
		{
			string string_ = global::GClass0.smethod_0("\u007fŮɦͬѫճ؆܏ࠄ॥ੰ୮౭ി่ག။ᅾቸፍᑯᕸᘶ᝚ᡆᥗᩗᭃᰰᵍṗἭ⁍ⅅ≙⍞⑍╕♙❊⡖⥇⩇⭓");
			return this.GetListBySql(string_);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00013440 File Offset: 0x00011640
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 4;
			string[] array = new string[4];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("闪剴純僶");
				array[1] = global::GClass0.smethod_0("丄群嗝圌");
				array[2] = global::GClass0.smethod_0("予群嗝圌");
				array[3] = global::GClass0.smethod_0("偛瑟柳陻У糐捱");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
				array[1] = global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ");
				array[2] = global::GClass0.smethod_0("[şɅ͔юՆم݆ࡏोਜ਼ୗౕൎ");
				array[3] = global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ");
			}
			return array;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000134C8 File Offset: 0x000116C8
		public string[,] ExcelContent(int int_0, List<V_WebTwo> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_WebTwo v_WebTwo in list_0)
			{
				array[num, 0] = v_WebTwo.SURVEY_ID;
				array[num, 1] = v_WebTwo.URI_DOMAIN;
				array[num, 2] = v_WebTwo.URI_DOMAIN_TWO;
				array[num, 3] = v_WebTwo.STAY_TIME.ToString();
				num++;
			}
			return array;
		}

		// Token: 0x04000027 RID: 39
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
