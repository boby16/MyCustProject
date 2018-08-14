using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000005 RID: 5
	public class SurveyConfigDal
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000043CC File Offset: 0x000025CC
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬ᭗ᱼᵼṷό⁨℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_1.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000440C File Offset: 0x0000260C
		public SurveyConfig GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑗᕼᙼ᝷᡹ᥨᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004438 File Offset: 0x00002638
		public SurveyConfig GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_1.ExecuteReader(string_0);
			SurveyConfig surveyConfig = new SurveyConfig();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyConfig.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					surveyConfig.CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString();
					surveyConfig.CODE_TEXT = dataReader[global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ")].ToString();
					surveyConfig.CODE_NOTE = dataReader[global::GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString();
					surveyConfig.PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
				}
			}
			return surveyConfig;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004514 File Offset: 0x00002714
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
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString(),
						CODE_TEXT = dataReader[global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ")].ToString(),
						CODE_NOTE = dataReader[global::GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString(),
						PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000045FC File Offset: 0x000027FC
		public List<SurveyConfig> GetList()
		{
			string string_ = global::GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑑᕾᙾᝩᡧᥪᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004620 File Offset: 0x00002820
		public void Add(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u0017ēȏ̞Јԍٸܞ࠘ँਛ୳ఁതย༹ါᄴሏጤᐤᔯᘡᜠᡮᤆᨋᬇᰇᵭḃὰ⁺ⅸ≣⍯⑿╡♬✛⡵⥺⩰⭶Ɑ⵿⹿⽻に㄁㉼㍪㑸㕬㙦㝳㡹㥦㩫㭧㱧㴈㸀㽉䁟䅑䉉䍞䑉䔱䘿䝬䠦䥨䨳䬿䰵䵪両佲倩儡别印吸啴嘯圫堡奾娷孾尥崨"), new object[]
			{
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004674 File Offset: 0x00002874
		public void Update(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0(">ĺȭ̩гԣمܷࠖऐਗଅదഝา༲ွᄳሾ፸ᐄᔓᘁ᝴᠐ᤝᨕᬕᱯᵳṭὫ‰ⅻ∴⍯⑫┅☊✀⠆⤝⨕⬅Ⱨ⵪⸝⼁〛ㄝ㉂㌊㑊㔑㘙㝷㡼㥶㩴㭯㱡㵡㹹㽩䀋䄗䈉䌏䑜䔕䙘䜃䠏䥲䩠䭲䱚䵐义佃偘兕剝卝吷唫嘵圳塨夦婬嬷尯嵙幅彉恙慏戩捁摃攦昸朤桸椲橼"), new object[]
			{
				surveyConfig_0.ID,
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000046D8 File Offset: 0x000028D8
		public void Delete(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬ቗፼ᑼᕷᙹᝨᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyConfig_0.ID);
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004710 File Offset: 0x00002910
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾቅ፪ᑪᕥᙫᝦ");
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004738 File Offset: 0x00002938
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 5;
			string[] array = new string[5];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("丼鐨ȥ苮嚫稔嗶");
				array[1] = global::GClass0.smethod_0("酅繩ȩ熳搅ԣ礔缀");
				array[2] = global::GClass0.smethod_0("酄繦Ȩ熰搄Ԥ飛貦堽");
				array[3] = global::GClass0.smethod_0("酅繩ȩ熳搅ԣ跶意");
				array[4] = global::GClass0.smethod_0("与群純笀");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("GŌɆ̈́");
				array[2] = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
				array[3] = global::GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ");
				array[4] = global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
			}
			return array;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000047DC File Offset: 0x000029DC
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

		// Token: 0x06000054 RID: 84 RVA: 0x00004890 File Offset: 0x00002A90
		public bool Exists(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("AŔɜ͊эՙ،݈ࡅड़੆୓ఎഏญ༃၄ᅓ቏፲ᐾᕎᙩᝩᡬ᥼ᩡ᭔ᱹᵻṲὺ⁵ℱ≧⍧⑫╿♩✫⡉⥆⩌⭂ⰻⴢ⹿⼳みㄦ"), string_0);
			int num = this.dbprovider_1.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000048CC File Offset: 0x00002ACC
		public SurveyConfig GetByCode(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("XŏɅ͍фՒ؅܎ࠃॄ੓୏౲ാ๎ཀྵၩᅬቼ፡ᑔᕹᙻᝲ᡺᥵ᨱ᭧ᱧᵫṿὩ‫ⅉ≆⍌⑂┻☢❿⠳⥿⨦"), string_0);
			return this.GetBySql(string_);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000048F4 File Offset: 0x00002AF4
		public string GetByCodeText(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("@ŗɝ͕ь՚؍ݯࡤ८੬୷౳ൣ๽཰ဃᅄቓፏᑲᔾᙎᝩᡩᥬ᩼᭡᱔ᵹṻὲ⁺ⅵ∱⍧⑧╫♿❩⠫⥉⩆⭌ⱂⴻ⸢⽿〳ㅿ㈦"), string_0);
			return this.dbprovider_1.ExecuteScalarString(string_);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004920 File Offset: 0x00002B20
		public void UpdateByCode(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("Oŉɜ͖тՐؔݠࡇृ੆୊౗൮ใཅ၌ᅀ቏ጇᑕᕀᙐᜃᡡ᥮ᩤ᭚᱁ᵉṙὃ⁎ℹ∥⌷␱╮☤❮⠵⤱⩧⭧Ⱬ⵿⹩⼫ぉㅆ㉌㍂㐻㔢㙿㜲㡿㤦"), surveyConfig_0.CODE_TEXT, surveyConfig_0.CODE);
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004958 File Offset: 0x00002B58
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

		// Token: 0x06000059 RID: 89 RVA: 0x000049AC File Offset: 0x00002BAC
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

		// Token: 0x0600005A RID: 90 RVA: 0x000049E8 File Offset: 0x00002BE8
		public List<SurveyConfig> GetQuotaConfig()
		{
			string string_ = global::GClass0.smethod_0("!Ĵȼ̪ЭԹ٬ݡࡪय਺ନఫ൥ท༶ူᄷሥፆᑽᕒᙒ᝝ᡓᥞᨘᭀᱞᵐṆὖ‒Ⅱ≱⍽⑫╣♸❴⡩⥦⩬⭢Ⱋⴂ⹵⽖きㅕ㉁㌸㐾㕼㙲㝿㠺㥺㩷㭳㱳㵊㹠㽶䁪䅥䈬䌱䐩䔪䘬䝤䡸䥭䩭䭵䰦䵧乽伣偫入");
			return this.GetListBySql(string_);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004A0C File Offset: 0x00002C0C
		public bool ExistsRead(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("AŔɜ͊эՙ،݈ࡅड़੆୓ఎഏญ༃၄ᅓ቏፲ᐾᕎᙩᝩᡬ᥼ᩡ᭔ᱹᵻṲὺ⁵ℱ≧⍧⑫╿♩✫⡉⥆⩌⭂ⰻⴢ⹿⼳みㄦ"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004A48 File Offset: 0x00002C48
		public void UpdateByCodeRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("Oŉɜ͖тՐؔݠࡇृ੆୊౗൮ใཅ၌ᅀ቏ጇᑕᕀᙐᜃᡡ᥮ᩤ᭚᱁ᵉṙὃ⁎ℹ∥⌷␱╮☤❮⠵⤱⩧⭧Ⱬ⵿⹩⼫ぉㅆ㉌㍂㐻㔢㙿㜲㡿㤦"), surveyConfig_0.CODE_TEXT, surveyConfig_0.CODE);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004A80 File Offset: 0x00002C80
		public void AddRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u0017ēȏ̞Јԍٸܞ࠘ँਛ୳ఁതย༹ါᄴሏጤᐤᔯᘡᜠᡮᤆᨋᬇᰇᵭḃὰ⁺ⅸ≣⍯⑿╡♬✛⡵⥺⩰⭶Ɑ⵿⹿⽻に㄁㉼㍪㑸㕬㙦㝳㡹㥦㩫㭧㱧㴈㸀㽉䁟䅑䉉䍞䑉䔱䘿䝬䠦䥨䨳䬿䰵䵪両佲倩儡别印吸啴嘯圫堡奾娷孾尥崨"), new object[]
			{
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public string GetByCodeTextRead(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("@ŗɝ͕ь՚؍ݯࡤ८੬୷౳ൣ๽཰ဃᅄቓፏᑲᔾᙎᝩᡩᥬ᩼᭡᱔ᵹṻὲ⁺ⅵ∱⍧⑧╫♿❩⠫⥉⩆⭌ⱂⴻ⸢⽿〳ㅿ㈦"), string_0);
			return this.dbprovider_0.ExecuteScalarString(string_);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004B00 File Offset: 0x00002D00
		public List<SurveyConfig> GetSyncList()
		{
			string string_ = global::GClass0.smethod_0("/ľȶ̼лԣٶݿࡴवਠାఽ൯ฝ༸ှᄽሯጰᐋᔨᘨᜣᠭᤤᩢᬶᰨᵚṌ὘“Ⅻ≻⍫⑽╹♢❪⡷⥼⩶⭴Ⰽⴈ⹽⽔あㅈ㉸㍌㑉㕃㙲㝊㡳㥑㩋㭕㱅㴸㸾㽼䁲䅿䈺䍺䑷䕳䙳䝊䡠䥶䩪䭥䰭䴨丿伪倬兤剸卭呭啵嘦坧塽夣婫孥");
			return this.GetReadListBySql(string_);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004B24 File Offset: 0x00002D24
		public void UpdateToRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0(">ĺȭ̩гԣمܷࠖऐਗଅదഝา༲ွᄳሾ፸ᐄᔓᘁ᝴᠐ᤝᨕᬕᱯᵳṭὫ‰ⅻ∴⍯⑫┅☊✀⠆⤝⨕⬅Ⱨ⵪⸝⼁〛ㄝ㉂㌊㑊㔑㘙㝷㡼㥶㩴㭯㱡㵡㹹㽩䀋䄗䈉䌏䑜䔕䙘䜃䠏䥲䩠䭲䱚䵐义佃偘兕剝卝吷唫嘵圳塨夦婬嬷尯嵙幅彉恙慏戩捁摃攦昸朤桸椲橼"), new object[]
			{
				surveyConfig_0.ID,
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004B88 File Offset: 0x00002D88
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
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString(),
						CODE_TEXT = dataReader[global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ")].ToString(),
						CODE_NOTE = dataReader[global::GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString(),
						PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004C70 File Offset: 0x00002E70
		public List<SurveyConfig> GetListRead()
		{
			string string_ = global::GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑑᕾᙾᝩᡧᥪᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListReadBySql(string_);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004B88 File Offset: 0x00002D88
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
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString(),
						CODE_TEXT = dataReader[global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ")].ToString(),
						CODE_NOTE = dataReader[global::GClass0.smethod_0("JŇɃ̓њՊٌݖࡄ")].ToString(),
						PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x04000004 RID: 4
		private DBProvider dbprovider_0 = new DBProvider(1);

		// Token: 0x04000005 RID: 5
		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
