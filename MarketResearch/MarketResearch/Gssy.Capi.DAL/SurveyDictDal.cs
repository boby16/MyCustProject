using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyDictDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("~ũɧͯѪռ؇ݥࡪॱ੭୶ఉഊึ༾ၛᅎቔፗᐹᕋᙢᝤᡣᥱᩪ᭖ᱸᵳṻἮ⁚⅄≎⍘⑌┨♎❂⠥⤹⩸⬲ⱼ"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyDict GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑖᕸᙳ᝻ᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyDict GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyDict surveyDict = new SurveyDict();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyDict.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyDict.CODE = dataReader["CODE"].ToString();
					surveyDict.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					surveyDict.CODE_TYPE = dataReader[GClass0.smethod_0("JŇɃ̓њՐٚݒࡄ")].ToString();
					surveyDict.PARENT_ID = dataReader[GClass0.smethod_0("Yŉɕ̓ыՐٜ݋ࡅ")].ToString();
					surveyDict.INNER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("Bńɇ͍ѕՙيݖࡇे੓")]);
					surveyDict.CODE_NOTE = dataReader[GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString();
					surveyDict.IS_ENABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ̓ыՅفݎࡄ")]);
				}
			}
			return surveyDict;
		}

		public List<SurveyDict> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyDict> list = new List<SurveyDict>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyDict
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						CODE_TYPE = dataReader[GClass0.smethod_0("JŇɃ̓њՐٚݒࡄ")].ToString(),
						PARENT_ID = dataReader[GClass0.smethod_0("Yŉɕ̓ыՐٜ݋ࡅ")].ToString(),
						INNER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("Bńɇ͍ѕՙيݖࡇे੓")]),
						CODE_NOTE = dataReader[GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString(),
						IS_ENABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ̓ыՅفݎࡄ")])
					});
				}
			}
			return list;
		}

		public List<SurveyDict> GetList()
		{
			string string_ = GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷๅའၦᅥቷ፨ᑔᕦ᙭᝹ᠬ᥄ᩘ᭍ᱍᵕḦ὇⁝℣≋⍅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyDict surveyDict_0)
		{
			string string_ = string.Format(GClass0.smethod_0("Áǉ˕πӖחڢ߈࣎फ਱ଢ଼యഎจ༏ဝᄎሲጜᐗᔇᙚᜲᠿᤫᨫᭁᰯᴤḮἬ‷ℳ∣⌽␰╏☡✮⠤⤚⨁⬉Ⰵⴋ⸟⽵〈ㄖ㈄㌐㐚㔇㘍㜘㠔㥣㨇㬃㰂㴎㸘㼖䀇䄕䈂䌀䐖䕯䘁䜎䠄䥺䩡䭳䱳䵯乿伕偱兤剩印呺啲噰坽塵夆娎孻屭嵧广彬恻意戁捞搔敞昅服标楤樯歠氻洷渽潢瀪煪爱猹琳畨瘡睬砷礣穵笹籱紧縭署耽腺舡茩葿蔵虿蜨"), new object[]
			{
				surveyDict_0.CODE,
				surveyDict_0.CODE_TEXT,
				surveyDict_0.CODE_TYPE,
				surveyDict_0.PARENT_ID,
				surveyDict_0.INNER_ORDER,
				surveyDict_0.CODE_NOTE,
				surveyDict_0.IS_ENABLE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyDict surveyDict_0)
		{
			string string_ = string.Format(GClass0.smethod_0("ÎǊ˝ϙӃדڵ߇ࣦৠ૧௵೶්໤࿯ჿᆪዚᏍᓓᖦᛆ់ᣇᧇ᪡ᮽᱟᵙḆὍ\u2006⅝≕⌻␸┲☰✫⠧⤷⨩⬤ⱏⵓ⹍⽋【ㅘ㈔㍏㑋㔥㘪㜠㠦㤽㨵㬹㰏㴛㹽㽡䁻䅽䈢䍫䐪䕱䙹䜄䠒䤀䨔䬞䰛䴑丄伈偫具剩卯吼啲嘸坣塯夋娏嬎屺嵬幢彳恩慾扼捪搗攋昕杏栆楏樝歳池浪湨潳灥煥牽獭琇甛瘅眃硘礔穜笇簳絗繎罃聞腔艘荚葛蕓蘵蜩蠳襩訦譭谯赙蹅轉遙酏鈩鍁鑃锦阸霤顸餲驼"), new object[]
			{
				surveyDict_0.ID,
				surveyDict_0.CODE,
				surveyDict_0.CODE_TEXT,
				surveyDict_0.CODE_TYPE,
				surveyDict_0.PARENT_ID,
				surveyDict_0.INNER_ORDER,
				surveyDict_0.CODE_NOTE,
				surveyDict_0.IS_ENABLE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyDict surveyDict_0)
		{
			string string_ = string.Format(GClass0.smethod_0("`ŦɮͤѴ՚ؾݛࡎ॔੗ହోൢ๤ལၱᅪቖ፸ᑳᕻᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), surveyDict_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("RŐɘ͖цՔذ݉࡜ूੁଫౙർ๺ཱၣᅼቀ፪ᑡᕵ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 8;
			string[] array = new string[8];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("丼鐨ȥ苮嚫稔嗶");
				array[1] = GClass0.smethod_0("臯媞䱊䷡簀");
				array[2] = GClass0.smethod_0("吏磱");
				array[3] = GClass0.smethod_0("籹嚊");
				array[4] = GClass0.smethod_0("爲ģɫͥ");
				array[5] = GClass0.smethod_0("円抐岎");
				array[6] = GClass0.smethod_0("冁媺觶攏");
				array[7] = GClass0.smethod_0("濂氺");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = "CODE";
				array[2] = "CODE_TEXT";
				array[3] = GClass0.smethod_0("JŇɃ̓њՐٚݒࡄ");
				array[4] = GClass0.smethod_0("Yŉɕ̓ыՐٜ݋ࡅ");
				array[5] = GClass0.smethod_0("Bńɇ͍ѕՙيݖࡇे੓");
				array[6] = GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ");
				array[7] = GClass0.smethod_0("@śɘ̓ыՅفݎࡄ");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyDict> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyDict surveyDict in list_0)
			{
				array[num, 0] = surveyDict.ID.ToString();
				array[num, 1] = surveyDict.CODE;
				array[num, 2] = surveyDict.CODE_TEXT;
				array[num, 3] = surveyDict.CODE_TYPE;
				array[num, 4] = surveyDict.PARENT_ID;
				array[num, 5] = surveyDict.INNER_ORDER.ToString();
				array[num, 6] = surveyDict.CODE_NOTE;
				array[num, 7] = surveyDict.IS_ENABLE.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
