using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class V_WebOneDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("xůɥͭѤղ؅ݧ࡬ॷ੯୴షഴิ༼ၝᅈቖፕᐷᕀᙊᝃᡶᥰᩞ᭾ᱪᴮṚὄ⁎⅘≌⌨⑎╂☥✹⡸⤲⩼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_WebOne GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷเཊ၃ᅶተ፞ᑾᕪᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), int_0);
			return this.GetBySql(string_);
		}

		public V_WebOne GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_WebOne v_WebOne = new V_WebOne();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_WebOne.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_WebOne.URI_DOMAIN = dataReader[GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ")].ToString();
					v_WebOne.STAY_TIME = Convert.ToInt32(dataReader[GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ")]);
				}
			}
			return v_WebOne;
		}

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
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						URI_DOMAIN = dataReader[GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ")].ToString(),
						STAY_TIME = Convert.ToInt32(dataReader[GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ")])
					});
				}
			}
			return list;
		}

		public List<V_WebOne> GetList()
		{
			string string_ = GClass0.smethod_0("\u007fŮɦͬѫճ؆܏ࠄ॥ੰ୮౭ി่ག။ᅾቸፖᑶᕲᘶ᝚ᡆᥗᩗᭃᰰᵍṗἭ⁍ⅅ≙⍞⑍╕♙❊⡖⥇⩇⭓");
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 3;
			string[] array = new string[3];
			if (bool_0)
			{
				array[0] = "问卷编号";
				array[1] = GClass0.smethod_0("丄群嗝圌");
				array[2] = GClass0.smethod_0("偛瑟柳陻У糐捱");
			}
			else
			{
				array[0] = "SURVEY_ID";
				array[1] = GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ");
				array[2] = GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ");
			}
			return array;
		}

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

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
