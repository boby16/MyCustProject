using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000013 RID: 19
	public class SurveySyncDal
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000E774 File Offset: 0x0000C974
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("~ũɧͯѪռ؇ݥࡪॱ੭୶ఉഊึ༾ၛᅎቔፗᐹᕋᙢᝤᡣᥱᩪᭁᱨᵾṬἮ⁚⅄≎⍘⑌┨♎❂⠥⤹⩸⬲ⱼ"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
		public SurveySync GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑁᕨᙾᝬᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		public SurveySync GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveySync surveySync = new SurveySync();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveySync.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					surveySync.SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString();
					surveySync.SURVEY_GUID = dataReader[global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
					surveySync.SYNC_STATE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("YŐɆ̈́љՖِ݂ࡖॄ")]);
					surveySync.SYNC_DATE = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("ZőɉͅњՀقݖࡄ")].ToString()));
					surveySync.SYNC_NOTE = dataReader[global::GClass0.smethod_0("ZőɉͅњՊٌݖࡄ")].ToString();
				}
			}
			return surveySync;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		public List<SurveySync> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveySync> list = new List<SurveySync>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveySync
					{
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString(),
						SURVEY_GUID = dataReader[global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString(),
						SYNC_STATE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("YŐɆ̈́љՖِ݂ࡖॄ")]),
						SYNC_DATE = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("ZőɉͅњՀقݖࡄ")].ToString())),
						SYNC_NOTE = dataReader[global::GClass0.smethod_0("ZőɉͅњՊٌݖࡄ")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		public List<SurveySync> GetList()
		{
			string string_ = global::GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷๅའၦᅥቷ፨ᑃᕶᙠᝮᠬ᥄ᩘ᭍ᱍᵕḦ὇⁝℣≋⍅");
			return this.GetListBySql(string_);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000EA14 File Offset: 0x0000CC14
		public void Add(SurveySync surveySync_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("9ġȽ̨оԿيܠࠦळ਩୅షഖฐ༗စᄦልጤᐲᔸᙲᜊ᠍ᤅᨀᬐᰍᴌḛἕ⁼ℜ∛⌟␚┎☓✖⠏⤒⨏⬁ⱨⴐ⸛⼏〃ㅠ㉭㍩㑽㕯㙿㜕㡫㥮㩸㭶㱫㵷㹳㽥䁵䄃䉽䍴䑢䕨䙵䝧䡧䥳䩣䬌䰄䵵乣佭偵党前匵吻啠嘪坤堿夻娱孮尥嵮帵弽恫愽扳挡搫数昹杴栯椫模歾氰浾渥漨"), new object[]
			{
				surveySync_0.SURVEY_ID,
				surveySync_0.SURVEY_GUID,
				surveySync_0.SYNC_STATE,
				surveySync_0.SYNC_DATE,
				surveySync_0.SYNC_NOTE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		public void Update(SurveySync surveySync_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("*Įȹ̽ЯԿٙܫࠂऄਃ଑ఊഡจ༞ဌᅎሾጩᐿᕊᘺ᜽ᠵᤰᨠᬽ᰼ᴫḥὀ⁢ⅾ≺⌧⑪┧♾❴⠄⤃⨇⬂Ⱆⴋ⸎⼗〚ㄇ㈉㍬㑶㕪㙮㜳㡵㤻㩢㭨㰐㴛㸏㼃䁠䅭䉩䍽䑯䕿䘙䜅䠗䥍䨆䭉䰟䵡乨佾偬共剩卭呿啯嘉圕堇夁婞嬐属崅帍彳恆慐扞捃摕敕晍杝样椫樵欳汨洧湬漷瀯煙牅獉瑙畏瘩睁硃礦稸笤籸紲繼"), new object[]
			{
				surveySync_0.ID,
				surveySync_0.SURVEY_ID,
				surveySync_0.SURVEY_GUID,
				surveySync_0.SYNC_STATE,
				surveySync_0.SYNC_DATE,
				surveySync_0.SYNC_NOTE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		public void Delete(SurveySync surveySync_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("`ŦɮͤѴ՚ؾݛࡎ॔੗ହోൢ๤ལၱᅪቁ፨ᑾᕬᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), surveySync_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("RŐɘ͖цՔذ݉࡜ूੁଫౙർ๺ཱၣᅼ቗፺ᑬᕢ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 6;
			string[] array = new string[6];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("臮厫純僶");
				array[1] = global::GClass0.smethod_0("闪剴純僶");
				array[2] = global::GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
				array[3] = global::GClass0.smethod_0("合橦炴挀");
				array[4] = global::GClass0.smethod_0("合橦柴雵");
				array[5] = global::GClass0.smethod_0("吊橠拁劶迶挏");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
				array[2] = global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
				array[3] = global::GClass0.smethod_0("YŐɆ̈́љՖِ݂ࡖॄ");
				array[4] = global::GClass0.smethod_0("ZőɉͅњՀقݖࡄ");
				array[5] = global::GClass0.smethod_0("ZőɉͅњՊٌݖࡄ");
			}
			return array;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public string[,] ExcelContent(int int_0, List<SurveySync> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveySync surveySync in list_0)
			{
				array[num, 0] = surveySync.ID.ToString();
				array[num, 1] = surveySync.SURVEY_ID;
				array[num, 2] = surveySync.SURVEY_GUID;
				array[num, 3] = surveySync.SYNC_STATE.ToString();
				array[num, 4] = surveySync.SYNC_DATE.ToString();
				array[num, 5] = surveySync.SYNC_NOTE;
				num++;
			}
			return array;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public bool CheckObjectExist(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("\u001fĎȆ̌Ћԓ٦܆ࠋख਌କ౨കท༝ၺᅩት፴ᐘᕤᙃᝇᡂᥖᩋ᭢᱉ᵁṍἍ⁻Ⅳ≯⍻⑭┇♵❰⡶⥵⩧⭸Ɀⵖ⹚⼽〡ㄼ㉡㌩㑥㔰㘶㝴㡺㥷㨲㭂㱅㵝㹘㽈䁕䅔䉍䍜䑁䕃䘻䜢䡿䤲䩿䬦"), string_0, string_1);
			int num = this.dbprovider_0.ExecuteScalarInt(string_2);
			return num > 0;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		public SurveySync GetBySuveyID_GUID(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("\u0016āȏ̇ЂԔ؟ܔࠝॺ੩୵౴ഘ๤གྷ၇ᅂቖፋᑢᕉᙁᝍ᠍᥻ᩣ᭯ᱻᵭḇή⁰ⅶ≵⍧⑸╿♖❚⠽⤡⨼⭡Ⱙⵥ⸰⼶ぴㅺ㉷㌲㑂㕅㙝㝘㡈㥕㩔㭍㱜㵁㹃㼻䀢䅿䈲䍿䐦"), string_0, string_1);
			return this.GetBySql(string_2);
		}

		// Token: 0x0400001A RID: 26
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
