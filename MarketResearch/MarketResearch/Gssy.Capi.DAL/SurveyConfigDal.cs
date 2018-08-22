using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyConfigDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬ᭗ᱼᵼṷό⁨℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_1.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyConfig GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑗᕼᙼ᝷᡹ᥨᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyConfig GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_1.ExecuteReader(string_0);
			SurveyConfig surveyConfig = new SurveyConfig();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyConfig.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyConfig.CODE = dataReader["CODE"].ToString();
					surveyConfig.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					surveyConfig.CODE_NOTE = dataReader[GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString();
					surveyConfig.PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
				}
			}
			return surveyConfig;
		}

		public List<SurveyConfig> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_1.ExecuteReader(string_0);
			List<SurveyConfig> list = new List<SurveyConfig>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyConfig
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						CODE_NOTE = dataReader[GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString(),
						PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyConfig> GetList()
		{
			string string_ = GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑑᕾᙾᝩᡧᥪᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u0017ēȏ̞Јԍٸܞ࠘ँਛ୳ఁതย༹ါᄴሏጤᐤᔯᘡᜠᡮᤆᨋᬇᰇᵭḃὰ⁺ⅸ≣⍯⑿╡♬✛⡵⥺⩰⭶Ɑ⵿⹿⽻に㄁㉼㍪㑸㕬㙦㝳㡹㥦㩫㭧㱧㴈㸀㽉䁟䅑䉉䍞䑉䔱䘿䝬䠦䥨䨳䬿䰵䵪両佲倩儡别印吸啴嘯圫堡奾娷孾尥崨"), new object[]
			{
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void Update(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(GClass0.smethod_0(">ĺȭ̩гԣمܷࠖऐਗଅదഝา༲ွᄳሾ፸ᐄᔓᘁ᝴᠐ᤝᨕᬕᱯᵳṭὫ‰ⅻ∴⍯⑫┅☊✀⠆⤝⨕⬅Ⱨ⵪⸝⼁〛ㄝ㉂㌊㑊㔑㘙㝷㡼㥶㩴㭯㱡㵡㹹㽩䀋䄗䈉䌏䑜䔕䙘䜃䠏䥲䩠䭲䱚䵐义佃偘兕剝卝吷唫嘵圳塨夦婬嬷尯嵙幅彉恙慏戩捁摃攦昸朤桸椲橼"), new object[]
			{
				surveyConfig_0.ID,
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬ቗፼ᑼᕷᙹᝨᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyConfig_0.ID);
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾቅ፪ᑪᕥᙫᝦ");
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 5;
			string[] array = new string[5];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("丼鐨ȥ苮嚫稔嗶");
				array[1] = GClass0.smethod_0("酅繩ȩ熳搅ԣ礔缀");
				array[2] = GClass0.smethod_0("酄繦Ȩ熰搄Ԥ飛貦堽");
				array[3] = GClass0.smethod_0("酅繩ȩ熳搅ԣ跶意");
				array[4] = GClass0.smethod_0("与群純笀");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = "CODE";
				array[2] = "CODE_TEXT";
				array[3] = GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ");
				array[4] = GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyConfig> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyConfig surveyConfig in list_0)
			{
				array[num, 0] = surveyConfig.ID.ToString();
				array[num, 1] = surveyConfig.CODE;
				array[num, 2] = surveyConfig.CODE_TEXT;
				array[num, 3] = surveyConfig.CODE_NOTE;
				array[num, 4] = surveyConfig.PARENT_CODE;
				num++;
			}
			return array;
		}

		public bool Exists(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("AŔɜ͊эՙ،݈ࡅड़੆୓ఎഏญ༃၄ᅓ቏፲ᐾᕎᙩᝩᡬ᥼ᩡ᭔ᱹᵻṲὺ⁵ℱ≧⍧⑫╿♩✫⡉⥆⩌⭂ⰻⴢ⹿⼳みㄦ"), string_0);
			int num = this.dbprovider_1.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyConfig GetByCode(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("XŏɅ͍фՒ؅܎ࠃॄ੓୏౲ാ๎ཀྵၩᅬቼ፡ᑔᕹᙻᝲ᡺᥵ᨱ᭧ᱧᵫṿὩ‫ⅉ≆⍌⑂┻☢❿⠳⥿⨦"), string_0);
			return this.GetBySql(string_);
		}

		public string GetByCodeText(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("@ŗɝ͕ь՚؍ݯࡤ८੬୷౳ൣ๽཰ဃᅄቓፏᑲᔾᙎᝩᡩᥬ᩼᭡᱔ᵹṻὲ⁺ⅵ∱⍧⑧╫♿❩⠫⥉⩆⭌ⱂⴻ⸢⽿〳ㅿ㈦"), string_0);
			return this.dbprovider_1.ExecuteScalarString(string_);
		}

		public void UpdateByCode(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(GClass0.smethod_0("Oŉɜ͖тՐؔݠࡇृ੆୊౗൮ใཅ၌ᅀ቏ጇᑕᕀᙐᜃᡡ᥮ᩤ᭚᱁ᵉṙὃ⁎ℹ∥⌷␱╮☤❮⠵⤱⩧⭧Ⱬ⵿⹩⼫ぉㅆ㉌㍂㐻㔢㙿㜲㡿㤦"), surveyConfig_0.CODE_TEXT, surveyConfig_0.CODE);
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void SaveByKey(string string_0, string string_1)
		{
			SurveyConfig surveyConfig = new SurveyConfig();
			surveyConfig.CODE = string_0;
			surveyConfig.CODE_TEXT = string_1;
			if (this.Exists(string_0))
			{
				this.UpdateByCode(surveyConfig);
			}
			else
			{
				this.Add(surveyConfig);
			}
			if (this.ExistsRead(string_0))
			{
				this.UpdateByCodeRead(surveyConfig);
			}
			else
			{
				this.AddRead(surveyConfig);
			}
		}

		public void SaveDataFilePath(string string_0, string string_1)
		{
			SurveyConfig surveyConfig = new SurveyConfig();
			surveyConfig.CODE = string_0;
			surveyConfig.CODE_TEXT = string_1;
			if (this.Exists(string_0))
			{
				this.UpdateByCode(surveyConfig);
			}
			else
			{
				this.Add(surveyConfig);
			}
		}

		public List<SurveyConfig> GetQuotaConfig()
		{
			string string_ = GClass0.smethod_0("!Ĵȼ̪ЭԹ٬ݡࡪय਺ନఫ൥ท༶ူᄷሥፆᑽᕒᙒ᝝ᡓᥞᨘᭀᱞᵐṆὖ‒Ⅱ≱⍽⑫╣♸❴⡩⥦⩬⭢Ⱋⴂ⹵⽖きㅕ㉁㌸㐾㕼㙲㝿㠺㥺㩷㭳㱳㵊㹠㽶䁪䅥䈬䌱䐩䔪䘬䝤䡸䥭䩭䭵䰦䵧乽伣偫入");
			return this.GetListBySql(string_);
		}

		public bool ExistsRead(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("AŔɜ͊эՙ،݈ࡅड़੆୓ఎഏญ༃၄ᅓ቏፲ᐾᕎᙩᝩᡬ᥼ᩡ᭔ᱹᵻṲὺ⁵ℱ≧⍧⑫╿♩✫⡉⥆⩌⭂ⰻⴢ⹿⼳みㄦ"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public void UpdateByCodeRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(GClass0.smethod_0("Oŉɜ͖тՐؔݠࡇृ੆୊౗൮ใཅ၌ᅀ቏ጇᑕᕀᙐᜃᡡ᥮ᩤ᭚᱁ᵉṙὃ⁎ℹ∥⌷␱╮☤❮⠵⤱⩧⭧Ⱬ⵿⹩⼫ぉㅆ㉌㍂㐻㔢㙿㜲㡿㤦"), surveyConfig_0.CODE_TEXT, surveyConfig_0.CODE);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void AddRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u0017ēȏ̞Јԍٸܞ࠘ँਛ୳ఁതย༹ါᄴሏጤᐤᔯᘡᜠᡮᤆᨋᬇᰇᵭḃὰ⁺ⅸ≣⍯⑿╡♬✛⡵⥺⩰⭶Ɑ⵿⹿⽻に㄁㉼㍪㑸㕬㙦㝳㡹㥦㩫㭧㱧㴈㸀㽉䁟䅑䉉䍞䑉䔱䘿䝬䠦䥨䨳䬿䰵䵪両佲倩儡别印吸啴嘯圫堡奾娷孾尥崨"), new object[]
			{
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string GetByCodeTextRead(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("@ŗɝ͕ь՚؍ݯࡤ८੬୷౳ൣ๽཰ဃᅄቓፏᑲᔾᙎᝩᡩᥬ᩼᭡᱔ᵹṻὲ⁺ⅵ∱⍧⑧╫♿❩⠫⥉⩆⭌ⱂⴻ⸢⽿〳ㅿ㈦"), string_0);
			return this.dbprovider_0.ExecuteScalarString(string_);
		}

		public List<SurveyConfig> GetSyncList()
		{
			string string_ = GClass0.smethod_0("/ľȶ̼лԣٶݿࡴवਠାఽ൯ฝ༸ှᄽሯጰᐋᔨᘨᜣᠭᤤᩢᬶᰨᵚṌ὘“Ⅻ≻⍫⑽╹♢❪⡷⥼⩶⭴Ⰽⴈ⹽⽔あㅈ㉸㍌㑉㕃㙲㝊㡳㥑㩋㭕㱅㴸㸾㽼䁲䅿䈺䍺䑷䕳䙳䝊䡠䥶䩪䭥䰭䴨丿伪倬兤剸卭呭啵嘦坧塽夣婫孥");
			return this.GetReadListBySql(string_);
		}

		public void UpdateToRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(GClass0.smethod_0(">ĺȭ̩гԣمܷࠖऐਗଅదഝา༲ွᄳሾ፸ᐄᔓᘁ᝴᠐ᤝᨕᬕᱯᵳṭὫ‰ⅻ∴⍯⑫┅☊✀⠆⤝⨕⬅Ⱨ⵪⸝⼁〛ㄝ㉂㌊㑊㔑㘙㝷㡼㥶㩴㭯㱡㵡㹹㽩䀋䄗䈉䌏䑜䔕䙘䜃䠏䥲䩠䭲䱚䵐义佃偘兕剝卝吷唫嘵圳塨夦婬嬷尯嵙幅彉恙慏戩捁摃攦昸朤桸椲橼"), new object[]
			{
				surveyConfig_0.ID,
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public List<SurveyConfig> GetReadListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyConfig> list = new List<SurveyConfig>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyConfig
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						CODE_NOTE = dataReader[GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString(),
						PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyConfig> GetListRead()
		{
			string string_ = GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑑᕾᙾᝩᡧᥪᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListReadBySql(string_);
		}

		public List<SurveyConfig> GetListReadBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyConfig> list = new List<SurveyConfig>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyConfig
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						CODE_NOTE = dataReader[GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString(),
						PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString()
					});
				}
			}
			return list;
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
