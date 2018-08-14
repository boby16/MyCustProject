using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x0200001D RID: 29
	public class V_WebOneDal
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x00012F44 File Offset: 0x00011144
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("xůɥͭѤղ؅ݧ࡬ॷ੯୴షഴิ༼ၝᅈቖፕᐷᕀᙊᝃᡶᥰᩞ᭾ᱪᴮṚὄ⁎⅘≌⌨⑎╂☥✹⡸⤲⩼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00012F84 File Offset: 0x00011184
		public V_WebOne GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷเཊ၃ᅶተ፞ᑾᕪᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00012FB0 File Offset: 0x000111B0
		public V_WebOne GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_WebOne v_WebOne = new V_WebOne();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_WebOne.SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString();
					v_WebOne.URI_DOMAIN = dataReader[global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ")].ToString();
					v_WebOne.STAY_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ")]);
				}
			}
			return v_WebOne;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00013050 File Offset: 0x00011250
		public List<V_WebOne> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_WebOne> list = new List<V_WebOne>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_WebOne
					{
						SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString(),
						URI_DOMAIN = dataReader[global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ")].ToString(),
						STAY_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ")])
					});
				}
			}
			return list;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000130FC File Offset: 0x000112FC
		public List<V_WebOne> GetList()
		{
			string string_ = global::GClass0.smethod_0("\u007fŮɦͬѫճ؆܏ࠄ॥ੰ୮౭ി่ག။ᅾቸፖᑶᕲᘶ᝚ᡆᥗᩗᭃᰰᵍṗἭ⁍ⅅ≙⍞⑍╕♙❊⡖⥇⩇⭓");
			return this.GetListBySql(string_);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00013120 File Offset: 0x00011320
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 3;
			string[] array = new string[3];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("闪剴純僶");
				array[1] = global::GClass0.smethod_0("丄群嗝圌");
				array[2] = global::GClass0.smethod_0("偛瑟柳陻У糐捱");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
				array[1] = global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ");
				array[2] = global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ");
			}
			return array;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00013190 File Offset: 0x00011390
		public string[,] ExcelContent(int int_0, List<V_WebOne> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_WebOne v_WebOne in list_0)
			{
				array[num, 0] = v_WebOne.SURVEY_ID;
				array[num, 1] = v_WebOne.URI_DOMAIN;
				array[num, 2] = v_WebOne.STAY_TIME.ToString();
				num++;
			}
			return array;
		}

		// Token: 0x04000026 RID: 38
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
