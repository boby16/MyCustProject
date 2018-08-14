using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000010 RID: 16
	public class SurveyRandomDal
	{
		// Token: 0x06000114 RID: 276 RVA: 0x0000C79C File Offset: 0x0000A99C
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬᭆᱲᵼṵ὿⁢℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		public SurveyRandom GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑆᕲᙼ᝵᡿ᥢᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000C808 File Offset: 0x0000AA08
		public SurveyRandom GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyRandom surveyRandom = new SurveyRandom();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyRandom.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					surveyRandom.SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString();
					surveyRandom.QUESTION_SET = dataReader[global::GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॐੇ୕")].ToString();
					surveyRandom.QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					surveyRandom.CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString();
					surveyRandom.PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
					surveyRandom.RANDOM_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^ŊɄ͍чՊٙ݌ࡊेੇ୙")]);
					surveyRandom.RANDOM_SET1 = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਰ")]);
					surveyRandom.RANDOM_SET2 = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਲ਼")]);
					surveyRandom.RANDOM_SET3 = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਲ")]);
					surveyRandom.IS_FIX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("OŖɛͅыՙ")]);
					surveyRandom.SURVEY_GUID = dataReader[global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
				}
			}
			return surveyRandom;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public List<SurveyRandom> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyRandom> list = new List<SurveyRandom>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyRandom
					{
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString(),
						QUESTION_SET = dataReader[global::GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॐੇ୕")].ToString(),
						QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString(),
						PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString(),
						RANDOM_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^ŊɄ͍чՊٙ݌ࡊेੇ୙")]),
						RANDOM_SET1 = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਰ")]),
						RANDOM_SET2 = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਲ਼")]),
						RANDOM_SET3 = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਲ")]),
						IS_FIX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("OŖɛͅыՙ")]),
						SURVEY_GUID = dataReader[global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000CB60 File Offset: 0x0000AD60
		public List<SurveyRandom> GetList()
		{
			string string_ = global::GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑀᕰᙾᝫᡡᥠᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public void Add(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u009dƝʁΔ҂֛ۮބࢂটઅ௩ಛ඲ິླႡᆺነᎠᒮᗛᛑ័ᢔ᧨᫯ᯫᳮᷲữῪ⃽⇷⊞⏠ⓥ◪⛽⟹⣥⧤⫤⯶⳻ⷢ⻲⾉ヵㇶ㋧㏲㓴㗖㛑㟓㣃㧕㫛㯔㳝㶻㻕㿚䃐䇖䊾䏁䓑䗝䛋䟃䣘䧔䫉䯆䳌䷂亪俗僅凍勆収响唠嘷圳堸夾娢孕尪崶常弱总愾戭挢搵攻晟杁栾椪樤欭氧洪渹漶瀡焷牐獍琲甞瘐眙砓礖稅笊簝紃繥罹耝脀舍茗萙蔗虢蜞蠙褙訜謌谑贘踁輐逍鄇鉫鍡鐖镾陲靨项饨騒鬞鱃鴇鹋鼒ꀘꄔꉉꌀꑍꔈꘂ꜊ꡗꤙ꩗ꬎ간관깝꼖끙넄눎댆둛딫뙣뜺렰률먯뭤밴뵬븠뽨쀸셨숥썬쐼앴옶읰젠쥰쨳쭴찤촠칽켴퀴텾툥패"), new object[]
			{
				surveyRandom_0.SURVEY_ID,
				surveyRandom_0.QUESTION_SET,
				surveyRandom_0.QUESTION_NAME,
				surveyRandom_0.CODE,
				surveyRandom_0.PARENT_CODE,
				surveyRandom_0.RANDOM_INDEX,
				surveyRandom_0.RANDOM_SET1,
				surveyRandom_0.RANDOM_SET2,
				surveyRandom_0.RANDOM_SET3,
				surveyRandom_0.IS_FIX,
				surveyRandom_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000CC34 File Offset: 0x0000AE34
		public void Update(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("¥ƿʪάҸ֮ۊ޺࢝কઐ஀ಝන຃ྏႄᆰኳᏽᒏᖞᚎ៹ᢋᦂ᪄ᮃᲑᶊẍᾘₔ⇯⋳⏭⓫▰⛻➴⣯⧫⪗⮐ⲁⶐ⺖⾈わㇱ㋡㏮㓹㗯㚚㞄㢘㦐㫍㮇㳉㶔㺞㿠䃥䇪䋽䏹䓥䗤䛤䟶䣦䧦䫫䯠䲄䶞亂來僛冬勣厺咰嗘囕埝壝妷媫宵岳巨度忬悷憣拞揌擞旎曄柝棗槄櫉毁況涣溿澡炧焄牋猀瑛畗瘨眸砶礳稹笸簫紺縼缵耵脷艎荐葌蔐虜蜔衄褵訧謫谠贬踯輾逳鄚鈊鍬鑼镦険霢顯餪驺鬇鰕鴝鸖鼞ꀝꄐꈝꌈꐘꕹꙪꝴꡨꤼꩾꬸ걨광긃꼏뀄녰뉳덢둯땾뙮뜊렘뤊먖뭎밍뵎븞뽸쁣셰쉨썤쑴씋옗윉졓줖쨖쭘찈쵰칷콳큶텚퉇퍂푛핎홓흝\ud838\ud92a\uda36\udb32\udc6f\udd22\ude23\udf6c"), new object[]
			{
				surveyRandom_0.ID,
				surveyRandom_0.SURVEY_ID,
				surveyRandom_0.QUESTION_SET,
				surveyRandom_0.QUESTION_NAME,
				surveyRandom_0.CODE,
				surveyRandom_0.PARENT_CODE,
				surveyRandom_0.RANDOM_INDEX,
				surveyRandom_0.RANDOM_SET1,
				surveyRandom_0.RANDOM_SET2,
				surveyRandom_0.RANDOM_SET3,
				surveyRandom_0.IS_FIX,
				surveyRandom_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public void Delete(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬቆ፲ᑼᕵᙿᝢᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyRandom_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾቔ፤ᑪᕧ᙭ᝬ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000C398 File Offset: 0x0000A598
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 12;
			string[] array = new string[12];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("丼鐨ȥ苮嚫稔嗶");
				array[1] = global::GClass0.smethod_0("闪剴純僶");
				array[2] = global::GClass0.smethod_0("颜韅樅裇");
				array[3] = global::GClass0.smethod_0("颚勶");
				array[4] = global::GClass0.smethod_0("缔礀");
				array[5] = global::GClass0.smethod_0("与群純笀");
				array[6] = global::GClass0.smethod_0("隋昹缠尔");
				array[7] = global::GClass0.smethod_0("隅昳糌̧缪ԥصܣ呀瞦");
				array[8] = global::GClass0.smethod_0("隅昳糌̧缪ԥضܣ呀瞦");
				array[9] = global::GClass0.smethod_0("隅昳糌̧缪ԥطܣ呀瞦");
				array[10] = global::GClass0.smethod_0("图媙樅裇");
				array[11] = global::GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
				array[2] = global::GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॐੇ୕");
				array[3] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[4] = global::GClass0.smethod_0("GŌɆ̈́");
				array[5] = global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
				array[6] = global::GClass0.smethod_0("^ŊɄ͍чՊٙ݌ࡊेੇ୙");
				array[7] = global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਰ");
				array[8] = global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਲ਼");
				array[9] = global::GClass0.smethod_0("Yŋɇ͌шՋٚݗࡆॖਲ");
				array[10] = global::GClass0.smethod_0("OŖɛͅыՙ");
				array[11] = global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
			}
			return array;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000C500 File Offset: 0x0000A700
		public string[,] ExcelContent(int int_0, List<SurveyRandom> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyRandom surveyRandom in list_0)
			{
				array[num, 0] = surveyRandom.ID.ToString();
				array[num, 1] = surveyRandom.SURVEY_ID;
				array[num, 2] = surveyRandom.QUESTION_SET;
				array[num, 3] = surveyRandom.QUESTION_NAME;
				array[num, 4] = surveyRandom.CODE;
				array[num, 5] = surveyRandom.PARENT_CODE;
				array[num, 6] = surveyRandom.RANDOM_INDEX.ToString();
				array[num, 7] = surveyRandom.RANDOM_SET1.ToString();
				array[num, 8] = surveyRandom.RANDOM_SET2.ToString();
				array[num, 9] = surveyRandom.RANDOM_SET3.ToString();
				array[num, 10] = surveyRandom.IS_FIX.ToString();
				array[num, 11] = surveyRandom.SURVEY_GUID;
				num++;
			}
			return array;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000CD54 File Offset: 0x0000AF54
		public SurveyRandom GetById(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("Zōɋ̓цՐ؃܈ࠁॆ੭ୱ౰഼่཯ၫᅮቲ፯ᑇᕵᙽ᝶᡾᥽ᨯ᭹ᱥᵩṹὯ\u2029⅁≃⌻␢╿☳❿⠦"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000CD80 File Offset: 0x0000AF80
		public void AddList(string string_0, List<SurveyRandom> list_0)
		{
			foreach (SurveyRandom surveyRandom in list_0)
			{
				surveyRandom.SURVEY_ID = string_0;
				this.Add(surveyRandom);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		public void DeleteRandom(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("KŋɁ͉џՏ؉ݎࡕॉੈ଄౰ൗ๓བၺᅧ቏፽ᑵᕾᙶ᝵ᠷᥡ᩽᭱ᱡᵷḱὣ⁺ⅼ≻⍩⑲╕♠❬⠧⤻⨢⭿ⰳ⵿⸦"), string_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000021D0 File Offset: 0x000003D0
		public void CopyRandom(string string_0, List<SurveyRandom> list_0)
		{
			this.AddList(string_0, list_0);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000CE04 File Offset: 0x0000B004
		public bool CheckBaseCopyOK(string string_0, int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("dųɹͱѰզؑݳࡠॻ੣୸ఃഀ฀༈ၡᅴቪ፩ᐃᕱᙔᝒᡩ᥻ᩤ᭎ᱺᵴṽί⁺ℶ≂⍜⑖╀♔✰⡜⥛⩟⭚ⱎⵓ⹖⽁ぃㄻ㈢㍿㐳㕿㘦"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num == int_0;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000CE40 File Offset: 0x0000B040
		public List<SurveyRandom> GetList(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0(".Ĺȷ̿кԬٷݼࡵलਡଽ఼൰ผ༻ဿᄺሮጳᐛᔩᘩᜢᠪᤩᩣᬵᰩᴥṍὛ”Ⅿ≮⍨⑯╽♮❩⡼⥰⨎⬕ⱊⴀ⹒⼉」ㅭ㉥㍮㐉㕹㙲㝣㡶㥰㩪㭭㱯㵿㹌㽛䁉䄡䈼䍡䐨䕥䘰䜶䡺䥦䩷䭷䱣䴰乭佷倭兾剪卤呭啧噪坙塬奪婧孧屹"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000CE68 File Offset: 0x0000B068
		public List<SurveyRandom> GetListNoFix(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("'ĶȾ̴гԻٮݧ࡬भਸଦథ൧ต༰ံᄵሧጸᐒᕞᙐ᝙ᡓᥖᨚ᭎᱐ᵒṄὐ—Ⅰ≧⍣⑦╪♷❲⡥⥯⨗⬎ⱓⴗ⹛⼂〄ㅢ㉬㍥㐀㕎㙋㝘㡏㥏㩓㭖㱖㵈㹅㽐䁀䄮䈵䍪䐡䕲䘩䜭䡍䥅䩎䬩䱁䵔乙佃偍兛刿匱"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000CE90 File Offset: 0x0000B090
		public void UpdateRandom(List<SurveyRandom> list_0)
		{
			foreach (SurveyRandom surveyRandom in list_0)
			{
				string string_ = string.Format(global::GClass0.smethod_0("\u0010Ĕȇ̃Еԅٿ܍ࠨमਭିఠഊึ༸ေᄻሾ፲ᐢᔵᘻᝮ᠟ᤍᨅᬎᰆᴅḘἏ​℀∆⌚⑼┻☏❃⠑⥮⩺⭴ⱽ⵷⹺⽩てㅱ㉧㌃㐌㕋㘞㝓㠁㥾㩪㭤㱭㵧㹪㽹䁶䅡䉷䌐䐜䕛䘭䝣䠱䥎䩚䭔䱝䵗乚佉偆兑則匡听啫嘼坳堭奻婣孯屻嵭帧彏恁愹扸挶摼"), new object[]
				{
					surveyRandom.RANDOM_INDEX,
					surveyRandom.RANDOM_SET1,
					surveyRandom.RANDOM_SET2,
					surveyRandom.RANDOM_SET3,
					surveyRandom.ID
				});
				this.dbprovider_0.ExecuteNonQuery(string_);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000CF44 File Offset: 0x0000B144
		public List<SurveyRandom> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0(" ķȽ̵ЬԺ٭ݦ࡫ब਻ଧప൦ถ༱ေᄴሤጹᑭᕟᙓ᝘ᡔᥗᨙ᭏ᱟᵓṇὑ–Ⅱ≤⍢⑹╫♴❳⡢⥮⨔⬏ⱜⴖ⹘⼃〃ㅍ㉓㍄㑺㕬㘽㝾㡢㤺㩈㭍㱒㵅㹁㽝䁜䅜䉎䍃䑊䕚䘡䝞䡊䥄䩍䭇䱊䵙乌佊假兇剙"), string_0);
			return this.GetListBySql(string_);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000CF70 File Offset: 0x0000B170
		public string GetOneCode(string string_0, string string_1, int int_0, int int_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0(",Ļȱ̹иԮٹܛ࠘ऒਐ୴వഠ฾༽ၯᄝሸጾᐽᔯᘰ᜚ᠦᤨᨡᬫᰮᵢḶἨ⁚⅌≘⌜⑨╯♫❮⡲⥯⩪⭽ⱷⴏ⸖⽋〟ㅓ㈊㌌㑪㕤㙭㜈㡶㥳㩠㭷㱷㵫㹮㽮䁀䅍䉘䍈䐦䔽䙢䜩䡪䤱䨵䭕䱝䵖丱佂偎兀剉千呆啕噀坆塃奃婝嬹屸崰幼"), string_0, string_1, int_1);
			return this.dbprovider_0.ExecuteScalarString(string_2);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		public int GetGroupCount(string string_0, string string_1, int int_0)
		{
			string string_2 = global::GClass0.smethod_0("");
			switch (int_0)
			{
			case 1:
				string_2 = string.Format(global::GClass0.smethod_0("\u0011ĄȌ̺нԩټܖࠛँੰଅగഛฐ༜ဟᄎሃጊᐚᕼᙥᝫ᠋ᤚᩨᬊᰇᴝḛἐ\u2007ℕ≠⍙⑌╒♑✛⡩⥌⩊⭁ⱓⵌ⹦⽒ぜㅕ㉟㍂㐎㕚㙄㝎㡘㥌㨈㭴㱳㵷㹲㽦䁻䅾䉩䍛䐣䔺䙧䜫䡧䤾䨸䭖䱘䵑临佂假兔剃卛呇啂噂坔塙奌婜嬺尡嵾帵彾急愡"), string_0, string_1);
				break;
			case 2:
				string_2 = string.Format(global::GClass0.smethod_0("\u0011ĄȌ̺нԩټܖࠛँੰଅగഛฐ༜ဟᄎሃጊᐚᕿᙥᝫ᠋ᤚᩨᬊᰇᴝḛἐ\u2007ℕ≠⍙⑌╒♑✛⡩⥌⩊⭁ⱓⵌ⹦⽒ぜㅕ㉟㍂㐎㕚㙄㝎㡘㥌㨈㭴㱳㵷㹲㽦䁻䅾䉩䍛䐣䔺䙧䜫䡧䤾䨸䭖䱘䵑临佂假兔剃卛呇啂噂坔塙奌婜嬺尡嵾帵彾急愡"), string_0, string_1);
				break;
			case 3:
				string_2 = string.Format(global::GClass0.smethod_0("\u0011ĄȌ̺нԩټܖࠛँੰଅగഛฐ༜ဟᄎሃጊᐚᕾᙥᝫ᠋ᤚᩨᬊᰇᴝḛἐ\u2007ℕ≠⍙⑌╒♑✛⡩⥌⩊⭁ⱓⵌ⹦⽒ぜㅕ㉟㍂㐎㕚㙄㝎㡘㥌㨈㭴㱳㵷㹲㽦䁻䅾䉩䍛䐣䔺䙧䜫䡧䤾䨸䭖䱘䵑临佂假兔剃卛呇啂噂坔塙奌婜嬺尡嵾帵彾急愡"), string_0, string_1);
				break;
			}
			string s = this.dbprovider_0.ExecuteScalarString(string_2);
			return int.Parse(s);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000D028 File Offset: 0x0000B228
		public int GetGroupCountNoFix(string string_0, string string_1, int int_0)
		{
			string string_2 = global::GClass0.smethod_0("");
			switch (int_0)
			{
			case 1:
				string_2 = string.Format(global::GClass0.smethod_0("\u001dĈȀ̎Љԝوܪࠧऽੌ଱ణയฤ༐ဓᄂሏጞᐎᕨᙱ᝷᠗ᤆᩴᬞᰓᴉḏἜ​ℙ≬⌭␸┦☥❧⠕⤰⨶⬵Ⱗⴸ⸒⽞ぐㅙ㉓㍖㐚㕎㙐㝒㡄㥐㨔㭠㱧㵣㹦㽪䁷䅲䉥䍯䐗䔎䙓䜗䡛䤂䨄䭢䱬䵥一低偋兘剏协呓啖噖坈塅奐婀嬮尵嵪帡彲怩愭才捅摎攩晁杔桙楃橍歛氿洱"), string_0, string_1);
				break;
			case 2:
				string_2 = string.Format(global::GClass0.smethod_0("\u001dĈȀ̎Љԝوܪࠧऽੌ଱ణയฤ༐ဓᄂሏጞᐎᕫᙱ᝷᠗ᤆᩴᬞᰓᴉḏἜ​ℙ≬⌭␸┦☥❧⠕⤰⨶⬵Ⱗⴸ⸒⽞ぐㅙ㉓㍖㐚㕎㙐㝒㡄㥐㨔㭠㱧㵣㹦㽪䁷䅲䉥䍯䐗䔎䙓䜗䡛䤂䨄䭢䱬䵥一低偋兘剏协呓啖噖坈塅奐婀嬮尵嵪帡彲怩愭才捅摎攩晁杔桙楃橍歛氿洱"), string_0, string_1);
				break;
			case 3:
				string_2 = string.Format(global::GClass0.smethod_0("\u001dĈȀ̎Љԝوܪࠧऽੌ଱ణയฤ༐ဓᄂሏጞᐎᕪᙱ᝷᠗ᤆᩴᬞᰓᴉḏἜ​ℙ≬⌭␸┦☥❧⠕⤰⨶⬵Ⱗⴸ⸒⽞ぐㅙ㉓㍖㐚㕎㙐㝒㡄㥐㨔㭠㱧㵣㹦㽪䁷䅲䉥䍯䐗䔎䙓䜗䡛䤂䨄䭢䱬䵥一低偋兘剏协呓啖噖坈塅奐婀嬮尵嵪帡彲怩愭才捅摎攩晁杔桙楃橍歛氿洱"), string_0, string_1);
				break;
			}
			string s = this.dbprovider_0.ExecuteScalarString(string_2);
			return int.Parse(s);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000021DA File Offset: 0x000003DA
		public void RunSQL(string string_0)
		{
			this.dbprovider_0.ExecuteNonQuery(string_0);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		public SurveyRandom GetCircleOne(string string_0, string string_1, int int_0)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("/ľȶ̼лԣٶݿࡴवਠାఽ൯ฝ༸ှᄽሯጰᐚᔦᘨᜡᠫ᤮ᩢᬶᰨᵚṌ὘“Ⅸ≯⍫⑮╲♯❪⡽⥷⨏⬖ⱋⴟ⹓⼊「ㅪ㉤㍭㐈㕶㙳㝠㡷㥷㩫㭮㱮㵀㹍㽘䁈䄦䈽䍢䐩䕪䘱䜵䡕䥝䩖䬱䱂䵎乀佉偃兆剕區呆啃噃坝堹奸娰孼"), string_0, string_1, int_0);
			return this.GetBySql(string_2);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		public List<SurveyRandom> GetListNoJump(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("\u0012ąȳ̻оԨٻݰࡹाਥହస൴฀༧ဣᄦሪጷᐟᔭᘥᜮᠦᤥᩧᬱᰭᴡḱἧ⁡ℓ≪⍬⑫╹♢❥⡰⥼⨊⬑ⱎⴄ⹎⼕】ㅱ㉡㍪㐍㕽㙾㝯㡺㥼㩮㭩㱫㵻㹰㽧䁵䄝䈸䍥䐬䕡䘼䜺䡘䥖䩓䬶䱄䵁乖佁偅兙剀區呒啂噊均塌头娹嬡屏嵑幎归怦"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000D104 File Offset: 0x0000B304
		public int GetCircleCount(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("\u0001ĔȜ̊Ѝԙٌܨࠅजਆଓ౎൏ํགྷဃᄒቀ጑ᐝᔲᘩ᜵ᠮ᥹ᨾᬥ᰹ᴸṴἀ‧℣∦〉␷┟☭✥⠮⤦⨥⭧ⰱⴭ⸡⼱〧ㅡ㈓㍪㑬㕫㙹㝢㡥㥰㩼㬊㰑㵎㸄㽎䀕䄑䉱䍡䑪䔍䙽䝾䡯䥺䩼䭮䱩䵫乻佰偧兵初匸呥唬噡圼堺奘婖孓尶嵄幁彖恁慅扙捀摀敒時杊桇楌樴欹氡浏湑潎灒焦"), string_0, string_1);
			return this.dbprovider_0.ExecuteScalarInt(string_2);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000D134 File Offset: 0x0000B334
		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("Jňɀ͎ўՌ؈݁ࡔॊ੉ଃ౱ൔ๒ཀྵၻᅤ቎፺ᑴᕽᙷ᝺ᠶᥢ᩼᭶ᱠᵴḰ὜⁛⅟≚⍎⑓╖♁❃⠻⤢⩿⬳Ɀ⴦"), string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		// Token: 0x04000016 RID: 22
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
