using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x0200000A RID: 10
	public class SurveyLogicDal
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("}Ũɠͮѩս؈ݤࡩ॰੪୷ఊഋฉ༿ၘᅏቓፖᐺᕊ᙭ᝥᡠᥰᩭ᭟ᱽᵶṹὬ‮⅚≄⍎⑘╌☨❎⡂⤥⨹⭸ⰲ⵼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00008B30 File Offset: 0x00006D30
		public SurveyLogic GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("tţɩ͡Ѡն؁܊࠿क़੏୓ౖഺ๊཭ၥᅠተ፭ᑟᕽᙶ᝹ᡬ᤮ᩚ᭄ᱎᵘṌἨ⁎⅂∥⌹⑸┲♼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00008B5C File Offset: 0x00006D5C
		public SurveyLogic GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyLogic surveyLogic = new SurveyLogic();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyLogic.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					surveyLogic.PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					surveyLogic.LOGIC_TYPE = dataReader[global::GClass0.smethod_0("Fņɏ͎х՚ِݚࡒॄ")].ToString();
					surveyLogic.INNER_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Bńɇ͍ѕՙٌ݊ࡇेਖ਼")]);
					surveyLogic.FORMULA = dataReader[global::GClass0.smethod_0("Aŉɗ͉іՎـ")].ToString();
					surveyLogic.RECODE_ANSWER = dataReader[global::GClass0.smethod_0("_ŉɈͅэՍ٘݇ࡋॗ੔େ౓")].ToString();
					surveyLogic.LOGIC_MESSAGE = dataReader[global::GClass0.smethod_0("AŃɌ̓ъ՗ي݃ࡖॗੂ୅ౄ")].ToString();
					surveyLogic.NOTE = dataReader[global::GClass0.smethod_0("JŌɖ̈́")].ToString();
					surveyLogic.IS_ALLOW_PASS = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Dşɔ͋хՄوݑ࡚॔ੂ୑౒")]);
				}
			}
			return surveyLogic;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public List<SurveyLogic> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyLogic> list = new List<SurveyLogic>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyLogic
					{
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						LOGIC_TYPE = dataReader[global::GClass0.smethod_0("Fņɏ͎х՚ِݚࡒॄ")].ToString(),
						INNER_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Bńɇ͍ѕՙٌ݊ࡇेਖ਼")]),
						FORMULA = dataReader[global::GClass0.smethod_0("Aŉɗ͉іՎـ")].ToString(),
						RECODE_ANSWER = dataReader[global::GClass0.smethod_0("_ŉɈͅэՍ٘݇ࡋॗ੔େ౓")].ToString(),
						LOGIC_MESSAGE = dataReader[global::GClass0.smethod_0("AŃɌ̓ъ՗ي݃ࡖॗੂ୅ౄ")].ToString(),
						NOTE = dataReader[global::GClass0.smethod_0("JŌɖ̈́")].ToString(),
						IS_ALLOW_PASS = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Dşɔ͋хՄوݑ࡚॔ੂ୑౒")])
					});
				}
			}
			return list;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00008E10 File Offset: 0x00007010
		public List<SurveyLogic> GetList()
		{
			string string_ = global::GClass0.smethod_0("všɯͧѢմؿܴ࠽ग़੉୕౔സไལၧᅢቶ፫ᑝᕿᙨᝧᡮ᤬ᩄ᭘ᱍᵍṕἦ⁇⅝∣⍋⑅");
			return this.GetListBySql(string_);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008E34 File Offset: 0x00007034
		public void Add(SurveyLogic surveyLogic_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("ëǯ˳Ϛӌ׉ڼߒ্ࣔ૗ஷ೅෠໦࿥ჷᇨዜᏠᓩᗤᛯឣᣚᧈ᫏ᯂ᳙᷌Ềᾯ⃎⇎⋇⌶␽┢☨✢⠪⤼⩔⬾ⰸⴻ⸱⼡〭ㄸ㈾㌫㐫㔵㙀㜭㠥㤻㨥㬲㰪㴤㹈㼱䀧䄢䈯䌛䐛䔂䘝䜕䠉䤎䨝䬅䱺䴙丛伔倛儒刏匂吋唞嘟圊堍夌婤嬉尉崑币彯怋愒戟捾摲敱晳杬桥楩橹此汥洜渔潥灳煽牥獪瑽甅瘋睐砚祔稏笋簁絞縕罞者脍艛茭董蔱蘻蝠蠩襤訿謻谱赮踠轮逵鄽鈷鍴鐻镰阫霧頭饲騾魺鰡鴩鹿鼴ꁿꄨ"), new object[]
			{
				surveyLogic_0.PAGE_ID,
				surveyLogic_0.LOGIC_TYPE,
				surveyLogic_0.INNER_INDEX,
				surveyLogic_0.FORMULA,
				surveyLogic_0.RECODE_ANSWER,
				surveyLogic_0.LOGIC_MESSAGE,
				surveyLogic_0.NOTE,
				surveyLogic_0.IS_ALLOW_PASS
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008EB8 File Offset: 0x000070B8
		public void Update(SurveyLogic surveyLogic_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("âǦ˱ϵӧ׷ڑߣࣚড়૛௉೒෦ໆ࿏჎ᇅኅ᏷ᓦᗶᚁ៰ᣞ᧙᫘ᯃ᳒ᷞẹᾥ₷↱⋮⎥⓮▵⚽⟜⣀⧉⫄⯏Ⳕⷞ⻐⿘ヂㆦ㊸㎤㒤㗹㚳㟽㡘㥒㨴㬲㰵㴿㸫㼧䀾䄸䈱䌱䐫䕒䙌䝐䠔䥝䨐䭀䰭䴥主伥倲優判卄呞啂噆圛填夣婺孰尉崟帚弗怓愓戊挕搝攁昆朕栝楮橰歬汬洱湼漵灠煪爉猋琄甋瘂真硲祻穮筯籺絽繼缘耊脖舒荏萅蕏蘖蜜衡襡詹譩谋贗踉輏遜鄑鉘錃鐏镫陲靿類饒驑魓鱌鵅鹉齙ꁄꅅꈵꌩꐳꕩ꘩ꝭ꠯꥙ꩅꭉ걙굏긩꽁끃넦눸댤둸딲뙼"), new object[]
			{
				surveyLogic_0.ID,
				surveyLogic_0.PAGE_ID,
				surveyLogic_0.LOGIC_TYPE,
				surveyLogic_0.INNER_INDEX,
				surveyLogic_0.FORMULA,
				surveyLogic_0.RECODE_ANSWER,
				surveyLogic_0.LOGIC_MESSAGE,
				surveyLogic_0.NOTE,
				surveyLogic_0.IS_ALLOW_PASS
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00008F4C File Offset: 0x0000714C
		public void Delete(SurveyLogic surveyLogic_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("ašɯͧѵեؿݘࡏ॓੖଺ొ൭๥འၰᅭ቟፽ᑶᕹᙬᜮᡚ᥄ᩎ᭘᱌ᴨṎὂ‥ℹ≸⌲⑼"), surveyLogic_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00008F84 File Offset: 0x00007184
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("Sœə͑ч՗رݖ࡝ुੀବౘൿ๻ཾၢᅿ቉፫ᑤᕫᙢ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00008FAC File Offset: 0x000071AC
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 9;
			string[] array = new string[9];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("臮厫純僶");
				array[1] = global::GClass0.smethod_0("顶縔凶");
				array[2] = global::GClass0.smethod_0("逿躒繹咊");
				array[3] = global::GClass0.smethod_0("逿躒驸嶎");
				array[4] = global::GClass0.smethod_0("逽躔卨尌徘䭈");
				array[5] = global::GClass0.smethod_0("[ōɄ͉сՁأ籖恉");
				array[6] = global::GClass0.smethod_0("揔砹䷣据");
				array[7] = global::GClass0.smethod_0("逿躒懍賱");
				array[8] = global::GClass0.smethod_0("先誰畤壺籫躠刍霘蟆");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("WŇɂ́ќՋم");
				array[2] = global::GClass0.smethod_0("Fņɏ͎х՚ِݚࡒॄ");
				array[3] = global::GClass0.smethod_0("Bńɇ͍ѕՙٌ݊ࡇेਖ਼");
				array[4] = global::GClass0.smethod_0("Aŉɗ͉іՎـ");
				array[5] = global::GClass0.smethod_0("_ŉɈͅэՍ٘݇ࡋॗ੔େ౓");
				array[6] = global::GClass0.smethod_0("AŃɌ̓ъ՗ي݃ࡖॗੂ୅ౄ");
				array[7] = global::GClass0.smethod_0("JŌɖ̈́");
				array[8] = global::GClass0.smethod_0("Dşɔ͋хՄوݑ࡚॔ੂ୑౒");
			}
			return array;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000090B8 File Offset: 0x000072B8
		public string[,] ExcelContent(int int_0, List<SurveyLogic> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyLogic surveyLogic in list_0)
			{
				array[num, 0] = surveyLogic.ID.ToString();
				array[num, 1] = surveyLogic.PAGE_ID;
				array[num, 2] = surveyLogic.LOGIC_TYPE;
				array[num, 3] = surveyLogic.INNER_INDEX.ToString();
				array[num, 4] = surveyLogic.FORMULA;
				array[num, 5] = surveyLogic.RECODE_ANSWER;
				array[num, 6] = surveyLogic.LOGIC_MESSAGE;
				array[num, 7] = surveyLogic.NOTE;
				array[num, 8] = surveyLogic.IS_ALLOW_PASS.ToString();
				num++;
			}
			return array;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000091BC File Offset: 0x000073BC
		public List<SurveyLogic> GetCheckLogic(string string_0)
		{
			string arg = global::GClass0.smethod_0("HłɌ͋ьՙى݋ࡄोੂ");
			string string_ = string.Format(global::GClass0.smethod_0("$ĳȹ̱аԦٱݺ࡯नਿଣద൪บ༽ဵᄰሠጽᐏᔭᘦᜩᡜᤞᩊ᭔ᱞᵈṜἘ⁧ⅷ≲⍱⑬╻♵✍⠈⥕⨝⭑Ⰼⴊ⹈⽆ぃㄆ㉩㍫㑤㕫㙢㝿㡋㥇㩍㭙㰦㴽㹢㼩䁪䄱䈵䍻䑡䕶䙴䝢䠯䥬䩴䬬䱂䵄乇位偕兙剌半呇啇噙"), string_0, arg);
			return this.GetListBySql(string_);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000091F0 File Offset: 0x000073F0
		public List<SurveyLogic> GetReCodeLogic(string string_0, int int_0 = 1, int int_1 = 0, int int_2 = 99999)
		{
			string text = global::GClass0.smethod_0("HłɌ͋ьՙى݋ࡄोੂ");
			string string_ = global::GClass0.smethod_0("");
			if (int_0 == 1)
			{
				if (int_2 >= 5000)
				{
					int_2 = 4999;
				}
			}
			else if (int_1 <= 2000)
			{
				int_1 = 2001;
			}
			string_ = string.Format(global::GClass0.smethod_0("ñǤˬ̚Нԉٜݑ࡚टਊଘఛൕว༆ကᄇሕ጖ᐢᔂᘋᜂ᠉᥉᨟ᬏᰃᴗḁὃ′℠∧⌚␁└☘❦⡽⤢⩨⬪ⱱ⵵⸵⼽〶ㅱ㈜㌀㐉㔄㘏㜔㠞㤐㨘㬂㱺㵻㹣㼸䁳䄼䉧䌟䑟䕓䙘䜛䡳䥷䩶䭲䱤䵪乽佽偶兴剨匑吓啖嘞坖堊奈婆孃将嵬幪彭恧慳承捖摐教晙权栦椤橣欤汫洵湻潡灶煴牢猯瑬畴瘬睂硄祇積筕籙経繊罇聇腙"), new object[]
			{
				string_0,
				text,
				int_1.ToString(),
				int_2.ToString()
			});
			return this.GetListBySql(string_);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00009284 File Offset: 0x00007484
		public List<SurveyLogic> GetRecodeList(int int_0 = 2)
		{
			string arg = global::GClass0.smethod_0("HłɌ͋ьՙى݋ࡄोੂ");
			string string_ = global::GClass0.smethod_0("");
			if (int_0 == 1)
			{
				string_ = string.Format(global::GClass0.smethod_0("/ľȶ̼лԣٶݿࡴवਠାఽ൯ฝ༸ှᄽሯጰᐄᔨᘡᜬᠧᥣᨵᬩᰥᵍṛἝ⁰ⅴ≽⍰⑻╨♢❬⡤⥶⨎⬏Ⱇⵔ⸞⽐》ㄋ㈊㍈㑆㕃㘆㝬㡪㥭㩧㭳㱿㵖㹐㽙䁙䅃䈦䌬䐨䔧䘦䜵䡻䥡䩶䭴䱢䴯乬佴倬兂剄升呍啕噙坌塊奇婇孙"), arg);
			}
			else
			{
				string_ = string.Format(global::GClass0.smethod_0("/ľȶ̼лԣٶݿࡴवਠାఽ൯ฝ༸ှᄽሯጰᐄᔨᘡᜬᠧᥣᨵᬩᰥᵍṛἝ⁰ⅴ≽⍰⑻╨♢❬⡤⥶⨎⬏Ⱇⵔ⸞⽐》ㄋ㈊㍈㑆㕃㘆㝬㡪㥭㩧㭳㱿㵖㹐㽙䁙䅃䈤䌫䐨䔧䘦䜵䡻䥡䩶䭴䱢䴯乬佴倬兂剄升呍啕噙坌塊奇婇孙"), arg);
			}
			return this.GetListBySql(string_);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000092DC File Offset: 0x000074DC
		public List<SurveyLogic> GetCircleGuideLogic(string string_0, int int_0 = 1)
		{
			string arg = global::GClass0.smethod_0("QŘɂ͌тՈٓ݌࡟ीੌୂౙ൉๋ང။ᅂ");
			string string_ = string.Format(global::GClass0.smethod_0("$ĳȹ̱аԦٱݺ࡯नਿଣద൪บ༽ဵᄰሠጽᐏᔭᘦᜩᡜᤞᩊ᭔ᱞᵈṜἘ⁧ⅷ≲⍱⑬╻♵✍⠈⥕⨝⭑Ⰼⴊ⹈⽆ぃㄆ㉩㍫㑤㕫㙢㝿㡋㥇㩍㭙㰦㴽㹢㼩䁪䄱䈵䍻䑡䕶䙴䝢䠯䥬䩴䬬䱂䵄乇位偕兙剌半呇啇噙"), string_0, arg);
			if (int_0 == 1)
			{
				string_ = string.Format(global::GClass0.smethod_0("\u001eĉȇ̏ЊԜه݌ࡅं਑଍ఌീฌ༫ုᄪሾጣᐕᔷᘰ᜿ᠶᥴᨤᬺᰴᴢḪὮ”ℍ∌⌏␖━☃❻⡢⤿⩳⬿ⱦⵠ⹞⽐すㄜ㉷㍵㑾㕱㙴㝩㡡㥭㩣㭷㰌㴗㹔㼟䁐䄋䈋䍋䑇䕌䘇䝯䡫䥪䩦䭰䱾䵩乑佚偘兄刧匯吩唨嘧圶堵奻婡孶屴嵢帯彬恴愬扂捄摇敍晕杙桌楊橇歇汙"), string_0, arg);
			}
			else
			{
				string_ = string.Format(global::GClass0.smethod_0("\u001eĉȇ̏ЊԜه݌ࡅं਑଍ఌീฌ༫ုᄪሾጣᐕᔷᘰ᜿ᠶᥴᨤᬺᰴᴢḪὮ”ℍ∌⌏␖━☃❻⡢⤿⩳⬿ⱦⵠ⹞⽐すㄜ㉷㍵㑾㕱㙴㝩㡡㥭㩣㭷㰌㴗㹔㼟䁐䄋䈋䍋䑇䕌䘇䝯䡫䥪䩦䭰䱾䵩乑佚偘兄別匨吩唨嘧圶堵奻婡孶屴嵢帯彬恴愬扂捄摇敍晕杙桌楊橇歇汙"), string_0, arg);
			}
			return this.GetListBySql(string_);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000933C File Offset: 0x0000753C
		public List<SurveyLogic> GetPageInfo(int int_0 = 1)
		{
			string arg = global::GClass0.smethod_0("HłɌ͋ьՙى݋ࡄोੂ");
			string string_ = global::GClass0.smethod_0("");
			if (int_0 == 1)
			{
				string_ = string.Format(global::GClass0.smethod_0("©Ƽʴβҵ֡۴ߣࣲরણ௯ಇඉ໠࿫ႚᆈ኏ᎂᒙᖌ\u1680៯ᣢᦍ᪏᯸᳷᷾ợ`⃣⇩⋽⎛⒖◶⛛⟆⣜⧅⪘⮅ⲇⶍ⻍⿘り㇠㋦㏩㓣㗷㛻㟪㣬㧥㫥㯇㲲㶽㺻㾼䂺䇸䋫䎷䓐䗚䛆䟞䣇䧝䫑䮣䲮䶪享侫僫出动叕哃嗆囋埇壇姞嫁嬱尭崪帹弩恖慙扟捐摖攔昇杓栾椾樷欦氭洲渡漮瀹焺爩猠琣畉癄睄硅祁稁第籾紓縓缏耟腵艸荧葶蔴蘧蝳蠛褂訏謎谂贁踃輜逕鄙鈉錔鐕镥阢霱頭餬驠魬鱋鵏鹊齞ꁃꅵꉗꍐꑟꕖꘔꝄꡚ꥔ꩂꭊ갎굡깣꽬끣녪뉷덳둿땵뙡뜞렅륚먐뭢밹봽빛뽉쁕셌쉈쌷쑔알옴읃졓쥖쩕쭐챇쵉츠켫큆텆퉏퍎푅핚홐흚\ud852\ud944"), arg);
			}
			else
			{
				string_ = string.Format(global::GClass0.smethod_0("¨ƿʵνҴ֢۵ߤࣳ঳ઢ௰ಆඊ໡࿬ႛᆋ኎ᎍᒘᖏᚁ៨ᣣᦎ᪎ᮇᳶ᷽ỢῨ⃢⇪⋼⎔⒗◵⛚⟁⣝⧆⪙⮚Ⲇⶎ⻌⿟る㇣㋧㏦㓢㗴㛺㟭㣭㧦㫤㯸㲳㶾㺺㾻䂻䇻䋪䎸䓑䗙䛇䟙䣆䧞䫐䮼䲯䶩亪侬僪凹助叚哂嗅囊埀壆姝嫀寎尬崩常弮恗慚扞损摗攗昆杔栿椽樶欹氬洱渠漩瀸焹爨猯琢畊癅睃硄祂稀笓籿紐縒缈耞腶艹荨葷蔷蘦蝴蠚褁討謑调贂踂輛途鄚鈈錛鐔镦阣霶頬餯驡鬓鱊鵌鹋齙ꁂꅶꉖꍟꑞꕕꘕꝃꡛ꥗ꩃꭕ갏굢깢꽫끢녩뉶덼둾땶뙠뜘렝뤅멚묐뱢봹븽뽛쁉셕쉌썈쐷암왌윴졃쥓쩖쭕챐쵇칉켠퀫텆퉆퍏푎핅홚흐\ud85a\ud952\uda44"), arg);
			}
			return this.GetListBySql(string_);
		}

		// Token: 0x0400000D RID: 13
		private DBProvider dbprovider_0 = new DBProvider(1);

		// Token: 0x0400000E RID: 14
		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
