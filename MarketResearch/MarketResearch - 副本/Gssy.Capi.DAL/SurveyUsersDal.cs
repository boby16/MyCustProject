using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyUsersDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("}Ũɠͮѩս؈ݤࡩ॰੪୷ఊഋฉ༿ၘᅏቓፖᐺᕊ᙭ᝥᡠᥰᩭᭆᱡᵴṢὼ‮⅚≄⍎⑘╌☨❎⡂⤥⨹⭸ⰲ⵼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyUsers GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("tţɩ͡Ѡն؁܊࠿क़੏୓ౖഺ๊཭ၥᅠተ፭ᑆᕡᙴᝢ᡼᤮ᩚ᭄ᱎᵘṌἨ⁎⅂∥⌹⑸┲♼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyUsers GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyUsers surveyUsers = new SurveyUsers();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyUsers.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyUsers.USER_ID = dataReader[GClass0.smethod_0("Rŕɀ͖ќՋم")].ToString();
					surveyUsers.USER_NAME = dataReader[GClass0.smethod_0("\\śɂ͔њՊقݏࡄ")].ToString();
					surveyUsers.IS_ENABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ̓ыՅفݎࡄ")]);
					surveyUsers.PSW = dataReader[GClass0.smethod_0("Sőɖ")].ToString();
					surveyUsers.ROLE_ID = dataReader[GClass0.smethod_0("Uŉɉ́ќՋم")].ToString();
				}
			}
			return surveyUsers;
		}

		public List<SurveyUsers> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyUsers> list = new List<SurveyUsers>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyUsers
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						USER_ID = dataReader[GClass0.smethod_0("Rŕɀ͖ќՋم")].ToString(),
						USER_NAME = dataReader[GClass0.smethod_0("\\śɂ͔њՊقݏࡄ")].ToString(),
						IS_ENABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ̓ыՅفݎࡄ")]),
						PSW = dataReader[GClass0.smethod_0("Sőɖ")].ToString(),
						ROLE_ID = dataReader[GClass0.smethod_0("Uŉɉ́ќՋم")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyUsers> GetList()
		{
			string string_ = GClass0.smethod_0("všɯͧѢմؿܴ࠽ग़੉୕౔സไལၧᅢቶ፫ᑄᕣᙪ᝼᡾᤬ᩄ᭘ᱍᵍṕἦ⁇⅝∣⍋⑅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyUsers surveyUsers_0)
		{
			string string_ = string.Format(GClass0.smethod_0("-ĭȱ̤вԋپܔࠒएਕ୹ఋഢฤ༣ေᄪሇጢᐵᔽᘽᝥ᠙ᤘᨏᬛᰗᴎḂὩ‑ℐ∇⌓␟╱♿❰⡹⤗⩳⭪Ⱨ⵲⹸⽴ぶㅿ㉷㌝㑠㕼㙹㜁㡾㥤㩦㭬㱷㵮㹢㼌䀄䅵䉣䍭䑵䕚䙍䜵䠻䥠䨪䭤䰿䴻丱佮倥兮刵匽呫唽噳圡堫奰娹孴尯崫帡彾怰慾戥挨"), new object[]
			{
				surveyUsers_0.USER_ID,
				surveyUsers_0.USER_NAME,
				surveyUsers_0.IS_ENABLE,
				surveyUsers_0.PSW,
				surveyUsers_0.ROLE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyUsers surveyUsers_0)
		{
			string string_ = string.Format(GClass0.smethod_0("&Ģȵ̱лԫٍܿࠞघਟ଍ఞളถ༁ထᄑቁጳᐚᔊᙽᜉ᠈᤟ᨋᬇᰞᴒṵὩ⁳ⅵ∪⍡␲╩♡✙⠘⤏⨛⬗Ⰹⴇ⸈⼁っㅿ㉡㍧㑄㔌㙀㜛㠗㥳㩪㭧㱲㵸㹴㽶䁿䅷䈑䌍䐏䕕䘞䝑䠇䥺䩺䭿䰇䴛丅伃偘儖剜匇吳啌噒坐塞奅婐孜尷崫帵弳恨愧扬挷搯教晅杉桙楏権歁汃洦游漤灸焲牼"), new object[]
			{
				surveyUsers_0.ID,
				surveyUsers_0.USER_ID,
				surveyUsers_0.USER_NAME,
				surveyUsers_0.IS_ENABLE,
				surveyUsers_0.PSW,
				surveyUsers_0.ROLE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyUsers surveyUsers_0)
		{
			string string_ = string.Format(GClass0.smethod_0("ašɯͧѵեؿݘࡏ॓੖଺ొ൭๥འၰᅭቆ፡ᑴᕢᙼᜮᡚ᥄ᩎ᭘᱌ᴨṎὂ‥ℹ≸⌲⑼"), surveyUsers_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("Sœə͑ч՗رݖ࡝ुੀବౘൿ๻ཾၢᅿቐ፷ᑦᕰᙲ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 6;
			string[] array = new string[6];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("丼鐨ȥ苮嚫稔嗶");
				array[1] = GClass0.smethod_0("甠挰ȩ袺釪兛礔缀");
				array[2] = GClass0.smethod_0("甫挵嘌");
				array[3] = GClass0.smethod_0("濂氺");
				array[4] = GClass0.smethod_0("寄礀");
				array[5] = GClass0.smethod_0("觖荱ɋͅ");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = GClass0.smethod_0("Rŕɀ͖ќՋم");
				array[2] = GClass0.smethod_0("\\śɂ͔њՊقݏࡄ");
				array[3] = GClass0.smethod_0("@śɘ̓ыՅفݎࡄ");
				array[4] = GClass0.smethod_0("Sőɖ");
				array[5] = GClass0.smethod_0("Uŉɉ́ќՋم");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyUsers> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyUsers surveyUsers in list_0)
			{
				array[num, 0] = surveyUsers.ID.ToString();
				array[num, 1] = surveyUsers.USER_ID;
				array[num, 2] = surveyUsers.USER_NAME;
				array[num, 3] = surveyUsers.IS_ENABLE.ToString();
				array[num, 4] = surveyUsers.PSW;
				array[num, 5] = surveyUsers.ROLE_ID;
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
