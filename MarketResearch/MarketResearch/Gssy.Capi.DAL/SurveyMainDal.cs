using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyMainDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("~ũɧͯѪռ؇ݥࡪॱ੭୶ఉഊึ༾ၛᅎቔፗᐹᕋᙢᝤᡣᥱᩪ᭟ᱰᵹṡἮ⁚⅄≎⍘⑌┨♎❂⠥⤹⩸⬲ⱼ"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyMain GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑟᕰᙹᝡᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyMain GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyMain surveyMain = new SurveyMain();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyMain.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyMain.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyMain.VERSION_ID = dataReader[GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")].ToString();
					surveyMain.USER_ID = dataReader[GClass0.smethod_0("Rŕɀ͖ќՋم")].ToString();
					surveyMain.CITY_ID = dataReader[GClass0.smethod_0("Dŏɑ͝ќՋم")].ToString();
					surveyMain.START_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("Yŝɉ͕ђ՚ِ݊ࡏॄ")].ToString()));
					surveyMain.END_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("Mŉɂ͚ѐՊُ݄")].ToString()));
					surveyMain.LAST_START_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("Cŏɞ͘єՙٝ݉ࡕ॒ਗ਼୐ొ൏ไ")].ToString()));
					surveyMain.LAST_END_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("Aōɘ͞іՍى݂࡚ॐ੊୏ౄ")].ToString()));
					surveyMain.CUR_PAGE_ID = dataReader[GClass0.smethod_0("Hşɛ͗їՇق݁࡜ो੅")].ToString();
					surveyMain.CIRCLE_A_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ")]);
					surveyMain.CIRCLE_A_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ")]);
					surveyMain.CIRCLE_B_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ")]);
					surveyMain.CIRCLE_B_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ")]);
					surveyMain.IS_FINISH = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ̀ьՊيݑࡉ")]);
					surveyMain.SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]);
					surveyMain.SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
					surveyMain.CLIENT_ID = dataReader[GClass0.smethod_0("JńɎ̓ыՐٜ݋ࡅ")].ToString();
					surveyMain.PROJECT_ID = dataReader[GClass0.smethod_0("Zśɇ͍уՆِݜࡋॅ")].ToString();
					surveyMain.ROADMAP_VERSION_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("@Şɑ͋уՌٜݔ࡜ौਗ਼୔౏ൊ๊ཛྷ။ᅅ")]);
				}
			}
			return surveyMain;
		}

		public List<SurveyMain> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyMain> list = new List<SurveyMain>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyMain
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						VERSION_ID = dataReader[GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")].ToString(),
						USER_ID = dataReader[GClass0.smethod_0("Rŕɀ͖ќՋم")].ToString(),
						CITY_ID = dataReader[GClass0.smethod_0("Dŏɑ͝ќՋم")].ToString(),
						START_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("Yŝɉ͕ђ՚ِ݊ࡏॄ")].ToString())),
						END_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("Mŉɂ͚ѐՊُ݄")].ToString())),
						LAST_START_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("Cŏɞ͘єՙٝ݉ࡕ॒ਗ਼୐ొ൏ไ")].ToString())),
						LAST_END_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("Aōɘ͞іՍى݂࡚ॐ੊୏ౄ")].ToString())),
						CUR_PAGE_ID = dataReader[GClass0.smethod_0("Hşɛ͗їՇق݁࡜ो੅")].ToString(),
						CIRCLE_A_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ")]),
						CIRCLE_A_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ")]),
						CIRCLE_B_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ")]),
						CIRCLE_B_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ")]),
						IS_FINISH = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ̀ьՊيݑࡉ")]),
						SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]),
						SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString(),
						CLIENT_ID = dataReader[GClass0.smethod_0("JńɎ̓ыՐٜ݋ࡅ")].ToString(),
						PROJECT_ID = dataReader[GClass0.smethod_0("Zśɇ͍уՆِݜࡋॅ")].ToString(),
						ROADMAP_VERSION_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("@Şɑ͋уՌٜݔ࡜ौਗ਼୔౏ൊ๊ཛྷ။ᅅ")])
					});
				}
			}
			return list;
		}

		public List<SurveyMain> GetList()
		{
			string string_ = GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷๅའၦᅥቷ፨ᑝᕮᙧᝣᠬ᥄ᩘ᭍ᱍᵕḦ὇⁝℣≋⍅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyMain surveyMain_0)
		{
			string string_ = string.Format(GClass0.smethod_0("0ĶȤ̳ЧԠܻٓ࠿तਠ୎ాങน༜ဌᄑሪጇᐌᔊᙋᜱᠴᤲᨉᬛᰄᴃḒ἞⁵ℎ−⌄␆┝☜✜⠎⤙⨋⭢Ⱈⴟ⸎⼘〖㄁㈃㍪㐆㔍㘗㜛㠞㤉㩻㬒㱮㵨㹺㽨䁭䅧䉣䍿䑸䕱䘟䝷䡿䥴䩰䭺䱤䵡乮伆健兩剴卲呺啷噷坣塳奴婀孊屔嵑幞弶恕慙扄捂摊救晝杖桎楄橆歃汈洠湈潟灛煗牗獇瑂畁癜睋硅礬窼箷粯綿纷羿肦膹芨莵蒠薦蚡螷袿覤諃训貤趾躨辦邬醷銦鎹钦閫隶鞬颵駌骜鮗鲏鶟麗龟ꂆꆚꊈꎕꒀꖆꚁꞗꢟꦄꫣꮍ겄궞꺈꾆낌놗늅뎙뒆떋뚖람뢕맬뫶믭볢뷺뻲뿴샰쇫싿쎚쓦엱웢쟧죴짾쫬쯫쳲췥컯쾆탺퇽틵폰퓠헽훼ퟥ\ud8f4\ud9e9\udadb\udbb2\udcde\uddd0\uded2\udfdf淪墨שּׁﰮﴵ︴Ｔ&ıȳ͟ѕԢزܾࠤव਼୆ొഗ๛༗၎ᅄቀጝᑔᔙᙄᝎᡆᤛᩭᬣᱺᵰṼἡ⁪℥≰⍺⑲┯♧✯⡶⥼⩨⬵ⱸⴱ⹬⽦のㄳ㉱㌻㑢㕨㙤㜹㡶㤽㨘㬒㰚㵇㸃㽇䀞䄔䉌䌏䑈䔘䙈䜃䠁䥍䨃䭕䰜䴝乖伆偒儙刕卛吉啟嘒圑塜夌婤嬯尩嵡帷弽恢愩戢捫搲攸昴杩栠椦橲欩氡洫湰漻瀾煵爠猪瑾电瘻睿砨"), new object[]
			{
				surveyMain_0.SURVEY_ID,
				surveyMain_0.VERSION_ID,
				surveyMain_0.USER_ID,
				surveyMain_0.CITY_ID,
				surveyMain_0.START_TIME,
				surveyMain_0.END_TIME,
				surveyMain_0.LAST_START_TIME,
				surveyMain_0.LAST_END_TIME,
				surveyMain_0.CUR_PAGE_ID,
				surveyMain_0.CIRCLE_A_CURRENT,
				surveyMain_0.CIRCLE_A_COUNT,
				surveyMain_0.CIRCLE_B_CURRENT,
				surveyMain_0.CIRCLE_B_COUNT,
				surveyMain_0.IS_FINISH,
				surveyMain_0.SEQUENCE_ID,
				surveyMain_0.SURVEY_GUID,
				surveyMain_0.CLIENT_ID,
				surveyMain_0.PROJECT_ID,
				surveyMain_0.ROADMAP_VERSION_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyMain surveyMain_0)
		{
			string string_ = string.Format(GClass0.smethod_0("`ŦɮͤѴ՚ؾݛࡎ॔੗ହోൢ๤ལၱᅪ቟፰ᑹᕡᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), surveyMain_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("RŐɘ͖цՔذ݉࡜ूੁଫౙർ๺ཱၣᅼ቉።ᑫᕯ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 20;
			string[] array = new string[20];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = "问卷编号";
				array[2] = GClass0.smethod_0("牌是純僶");
				array[3] = GClass0.smethod_0("论铪噛簔埶");
				array[4] = GClass0.smethod_0("埊弁純僶");
				array[5] = GClass0.smethod_0("笥伈椦雨坲娄忈拴鷵");
				array[6] = GClass0.smethod_0("笥伈椦雨坲篗慜拴鷵");
				array[7] = GClass0.smethod_0("朊唇䰈栦釨噲处廈淴鳵");
				array[8] = GClass0.smethod_0("朊唇䰈栦釨噲磗恜淴鳵");
				array[9] = GClass0.smethod_0("闫剳嵐兏鱴");
				array[10] = GClass0.smethod_0("HĨ幅展噈玀妩璭浱");
				array[11] = GClass0.smethod_0("FĦ幇岮瞬改捱");
				array[12] = GClass0.smethod_0("KĨ幅展噈玀妩璭浱");
				array[13] = GClass0.smethod_0("EĦ幇岮瞬改捱");
				array[14] = GClass0.smethod_0("门割搫圥徎朑");
				array[15] = GClass0.smethod_0("闫剳岌儕埶");
				array[16] = GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
				array[17] = GClass0.smethod_0("宧挳ȣ͋х");
				array[18] = GClass0.smethod_0("顼矪ȣ͋х");
				array[19] = GClass0.smethod_0("跩琴灌搯笔囶");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = "SURVEY_ID";
				array[2] = GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ");
				array[3] = GClass0.smethod_0("Rŕɀ͖ќՋم");
				array[4] = GClass0.smethod_0("Dŏɑ͝ќՋم");
				array[5] = GClass0.smethod_0("Yŝɉ͕ђ՚ِ݊ࡏॄ");
				array[6] = GClass0.smethod_0("Mŉɂ͚ѐՊُ݄");
				array[7] = GClass0.smethod_0("Cŏɞ͘єՙٝ݉ࡕ॒ਗ਼୐ొ൏ไ");
				array[8] = GClass0.smethod_0("Aōɘ͞іՍى݂࡚ॐ੊୏ౄ");
				array[9] = GClass0.smethod_0("Hşɛ͗їՇق݁࡜ो੅");
				array[10] = GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ");
				array[11] = GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ");
				array[12] = GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ");
				array[13] = GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ");
				array[14] = GClass0.smethod_0("@śɘ̀ьՊيݑࡉ");
				array[15] = GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅");
				array[16] = GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
				array[17] = GClass0.smethod_0("JńɎ̓ыՐٜ݋ࡅ");
				array[18] = GClass0.smethod_0("Zśɇ͍уՆِݜࡋॅ");
				array[19] = GClass0.smethod_0("@Şɑ͋уՌٜݔ࡜ौਗ਼୔౏ൊ๊ཛྷ။ᅅ");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyMain> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyMain surveyMain in list_0)
			{
				array[num, 0] = surveyMain.ID.ToString();
				array[num, 1] = surveyMain.SURVEY_ID;
				array[num, 2] = surveyMain.VERSION_ID;
				array[num, 3] = surveyMain.USER_ID;
				array[num, 4] = surveyMain.CITY_ID;
				array[num, 5] = surveyMain.START_TIME.ToString();
				array[num, 6] = surveyMain.END_TIME.ToString();
				array[num, 7] = surveyMain.LAST_START_TIME.ToString();
				array[num, 8] = surveyMain.LAST_END_TIME.ToString();
				array[num, 9] = surveyMain.CUR_PAGE_ID;
				array[num, 10] = surveyMain.CIRCLE_A_CURRENT.ToString();
				array[num, 11] = surveyMain.CIRCLE_A_COUNT.ToString();
				array[num, 12] = surveyMain.CIRCLE_B_CURRENT.ToString();
				array[num, 13] = surveyMain.CIRCLE_B_COUNT.ToString();
				array[num, 14] = surveyMain.IS_FINISH.ToString();
				array[num, 15] = surveyMain.SEQUENCE_ID.ToString();
				array[num, 16] = surveyMain.SURVEY_GUID;
				array[num, 17] = surveyMain.CLIENT_ID;
				array[num, 18] = surveyMain.PROJECT_ID;
				array[num, 19] = surveyMain.ROADMAP_VERSION_ID.ToString();
				num++;
			}
			return array;
		}

		public void Add(string string_0, string string_1, string string_2, string string_3, string string_4)
		{
			string text = Guid.NewGuid().ToString();
			string text2 = string.Format(GClass0.smethod_0("lĦȯͭѪի٨ܿࡂृਢ୨౯പแཀွᅫቨጾᑰᕱᙼ"), DateTime.Now);
			string string_5 = string.Format(GClass0.smethod_0("\u000eĈȖ́БԖ١܉ࡱ४ੲଜ౨൏๋ཎၒᅏቸፕᑚᕜᘙᝣ᡺᥼᩻᭩ᱲᵵṠὬ​ⅰ≠⍶⑰╫♮❮⡀⥗⩙⬰ⱎⵉ⹜⽊えㅟ㉑㌸㑐㕛㙅㝉㡐㥇㩉㬠㱘㵞㹈㽚䁓䅙䉑䍍䑎䕇䘭䝅䢱䦺䪢䮨䲲䶷亼俔傻冷劦厠咬喡嚥垱墽妺媲宸岢嶧庬忄悫憧抶掰撼斧暯枤梀榊檔殑沞淶溚澍炅熉犅玕璔疗皎瞙碋秢窎箅粙綉纅羍肘膇芚莇蒖薐蚓螅裱觪誑诿賲跨軺迴郲釩鋴鏫铰闽雤韾飻馂髮鯥鳹鷩黥鿭ꃸꇤꋺꏧꓶꗰ꛳꟥꣑꧊ꪱꯟ곒귈껚꿔냒뇉닗돋듐뗝뛄럞룛릢뫄믟볔뷌뻀뿆샎쇕싍쎨쓐엇원쟕젺줰쨾쬹찤촳츽코퀤턣툧팢퐶픫혮휷\ud83a\ud927\uda29\udb40\udc28\udd26\ude20\udf2d數喝ﭼﰶﵭ﹥ｯ<ŷȸͣѯե٦ݬ࠘ॅਏୁజഖพགྷငᅋሒጘᐔᕉᘅᝍ᠈ᤂᨊ᭗ᰞᵗḎἄ\u2000⅝∓⍙␄┎☆✇⠳⤮⨱⬬ⰷ⴪⸵⼨〻ㄦ㈹㌤㐿㔵㙪㜧㡲㤩㨡㬫㱰㴲㹴㼯䀫䄡䉾䌽䑾䔥䘨"), new object[]
			{
				string_0,
				string_1,
				string_2,
				text2,
				text2,
				text2,
				text2,
				text,
				string_4,
				string_3
			});
			this.dbprovider_0.ExecuteNonQuery(string_5);
		}

		public void Update(SurveyMain surveyMain_0)
		{
			string string_ = string.Format(GClass0.smethod_0("¸ƼʯΫҽ֭ۇ޵࢐খકஇಘත຾ྷႳᇼኈ᎟ᒍᗸᚂចᢐᦆ᪌ᮛᲕᷰỲ΅⃪↷⋺⎷⓮◤⚂➈⢁⦛⪗⮋Ⲍⶅ⺟⾃ゝ㆛㋀㎃㓄㖟㚛㟵㣠㧦㫬㯢㳰㷷㻪㿱䃤䇨䊋䎗䒉䖏䛜䞔䣘䦃䪏䯡䳨䷲仜俒僘凃勚叅哚嗍囅埄壐姚嫇宲岬嶰廴徽惰憠拈揃擛旋曋柃棚槅櫜毁泎淕渱漪灝煁牛猁瑍甅癛眵砼礦稰笾簴累縭缱耮脹船茸萬蔦蘳蝆衘襄記譗谜赌踜輗透鄟鈗錟鐆锚阈霕頚餁騝鬆鱱鵭鹯鼵ꁻꄱꉧꌃꐚꔗꘁ꜏ꠋꤍꨐꬊ걡굽긟꽅뀊녁눗덩둼땩뙢띳롻륷멶뭭뱸뵴븏뼓쀍셗숓썗쐅앺왨읧졡쥩쩢쭲챾쵶칚콌큎텕퉔퍔푆핑홓휶\ud828\ud934\uda68\udb23\udc21\udd6d\ude2f\udf59"), new object[]
			{
				surveyMain_0.ID,
				surveyMain_0.USER_ID,
				surveyMain_0.CUR_PAGE_ID,
				surveyMain_0.CIRCLE_A_CURRENT,
				surveyMain_0.CIRCLE_A_COUNT,
				surveyMain_0.CIRCLE_B_CURRENT,
				surveyMain_0.CIRCLE_B_COUNT,
				surveyMain_0.IS_FINISH,
				surveyMain_0.SEQUENCE_ID,
				DateTime.Now,
				surveyMain_0.ROADMAP_VERSION_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void UpdateUserId(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("lŨɳͷѡձؓݡࡄूਖ਼ୋ౔ൡ๊གྷ၇ᄈቴ፣ᑱᔄᙶ᝱ᡤᥲᩀ᭗᱙ᴡḼὡ\u2028Ⅵ∰⌶⑂╜♖❀⡔⤰⩜⭛ⱟⵚ⹎⽓ざㅁ㉃㌻㐢㕿㘳㝿㠦"), string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public void UpdateFinishStatus(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("lŨɳͷѡձؓݡࡄूਖ਼ୋ౔ൡ๊གྷ၇ᄈቴ፣ᑱᔄᙪ᝱᡾ᥦᩖ᭐᱔ᵏṓἧ⁢℩≪⌶⑂╜♖❀⡔⤰⩜⭛ⱟⵚ⹎⽓ざㅁ㉃㌻㐢㕿㘳㝿㠦"), string_0, int_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void UpdateCurPageId(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("jŮɹͽѯտؙݫࡂॄ੃୑ొൿ๐ཙ၁ᄎቾ፩ᑿᔊᙪ᝽ᡵ᥹᩵᭥ᱤᵧṾὩ⁛ℾ∠⌼␼╡☨❥⠰⤶⩂⭜ⱖⵀ⹔⼰ぜㅛ㉟㍚㑎㕓㙖㝁㡃㤻㨢㭿㰳㵿㸦"), string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public bool Exists(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("Főɟ͗ђՄ؏ݭࡢॹ੥୾ఁംฎ༆၃ᅖቌፏᐁᕳᙪᝬᡫ᥹ᩢ᭗ᱸᵱṹἶ⁢ⅼ≶⍠⑴┰♜❛⡟⥚⩎⭓ⱖⵁ⹃⼻〢ㅿ㈳㍿㐦"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyMain GetBySurveyId(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("]ňɀ͎щ՝؈܍ࠆृ੖ୌ౏ഁ๳ཪၬᅫቹ።ᑗᕸᙱ᝹ᠶᥢ᩼᭶ᱠᵴḰ὜⁻ⅿ≺⍮⑳╖♁❣⠻⤢⩿⬳Ɀ⴦"), string_0);
			return this.GetBySql(string_);
		}

		public SurveyMain GetBySurveyGUID(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("CŊɂ͈я՟؊܃ࠈु੔୊౉ഃ๱པၒᅩቻ፤ᑑᕺᙳ᝷ᠸᥠ᩾᭰ᱦᵶḲὂ⁅⅝≘⍈⑕╔♍❜⡁⥃⨻⬢Ɀⴳ⹿⼦"), string_0);
			return this.GetBySql(string_);
		}

		public string GetMaxSurveyID()
		{
			string result = "";
			string string_ = GClass0.smethod_0("lŻɱ͹Ѹծعݛࡘृਜ਼ୀ఻സุ༰ၩᅼቢ፡ᐫᕙᙼ᝺ᡱᥣ᩼ᭉᱢᵫṯ");
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			if (num > 0)
			{
				string_ = GClass0.smethod_0("mŸɰ;ѹխظݚࡷ७਼୚ౖസะཀྵၼᅢቡጫᑙᕼᙺ᝱ᡣ᥼ᩉ᭢ᱫᵯ");
				string value = this.dbprovider_0.ExecuteScalarString(string_);
				string_ = string.Format(GClass0.smethod_0("^ŉɇ͏ъ՜؇ݵࡰॶੵ୧౸ൿ๖ཚွᅺቩ፵ᑴᔸᙄᝣᡧᥢ᩶᭫ᱜᵱṦὠ‭ⅻ≣⍯⑻╭☧❏⡁⤹⩸⬲ⱼ"), Convert.ToInt32(value));
				result = this.dbprovider_0.ExecuteScalarString(string_);
			}
			return result;
		}

		public string GetMaxSurveyIDByCity(string string_0)
		{
			string result = "";
			string string_ = string.Format(GClass0.smethod_0("Hşɕ͝єՂؕݷࡼ१੿୤ఇഄค༌၍ᅘቆፅᐇᕵᙐ᝖ᡕ᥇ᩘ᭭᱾ᵷṳἼ⁬ⅲ≼⍪⑲┶♆❁⡁⥄⩔⭉ⱐⵇ⹉⼬でㅣ㉢㍭㐧㔡㙾㜴㡾㤧㨦"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			if (num > 0)
			{
				string_ = string.Format(GClass0.smethod_0("2ĥɓ͛ўՈ؛ݷࡘीਟ୥ౠ൦๥ཷၨᅯቦ፪ᐄᔌᙍ᝘ᡆ᥅ᨇ᭵᱐ᵖṕ὇⁘Ⅽ≾⍷⑳┼♬❲⡼⥪⩲⬶ⱆⵁ⹁⽄ごㅉ㉐㍇㑉㔬㙧㝣㡢㥭㨧㬡㱾㴴㹾㼧䀦"), string_0);
				result = this.dbprovider_0.ExecuteScalarString(string_);
			}
			return result;
		}

		public List<SurveyMain> GetSurveyMain(int int_0)
		{
			List<SurveyMain> result = new List<SurveyMain>();
			string string_ = string.Format(GClass0.smethod_0("@ŗɝ͕ь՚؍ݏࡄय़ੇଡ଼ఏഌฌ༄၅ᅐ቎ፍᐿᕍᙨᝮᡭ᥿᩠᭕ᱶᵿṻἴ⁤ⅺ≴⍢⑪┮♤❿⡔⥬⩠⭦Ɱ⵵⹭⼹へㄲ㉼"), int_0);
			string a = this.dbprovider_0.ExecuteScalarString(string_);
			if (a != "0")
			{
				string_ = string.Format(GClass0.smethod_0("Lśɑ͙јՎؙܒࠗॐੇ୛౞ഒ๢ཅၝᅘቈፕᑦᕋᙀᝆ᠇ᥑᩍᭁ᱑ᵇḁὉ⁬⅁≻⍵⑵╳♪❰⠪⥭⨥⭩ⰳ⵽⹣⽴なㅼ㈭㍮㑲㔪㙺㝽㡵㥰㩠㭽㱜㵫㹥"), int_0);
				result = this.GetListBySql(string_);
			}
			return result;
		}

		public bool ExistsByGUID(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("kŲɺͰѷէؒݲࡿॺ੠୹ఄഁฃ༉ၮᅵቩ፨ᐄᕰᙗᝓᡖ᥺ᩧ᭐ᱽᵲṴἹ⁏⅟≓⍇⑑┳♁❄⡂⥙⩋⭔ⱓⵌ⹟⽀がㄧ㈻㌢㑿㔳㙿㜦"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public bool ExistsBySurveyID(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("eŰɸͶѱեؐݬࡡॸ੢୿ంഃก༇ၠᅷቫ፮ᐂᕲᙕ᝭ᡨ᥸ᩥ᭖ᱻᵰṶἷ⁁⅝≑⍁⑗┱♃❚⡜⥛⩉⭒ⱕⵀ⹌⼧〻ㄢ㉿㌳㑿㔦"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public string GetAutoSurveyId(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("1ĤȬ͚ѝՉ؜ݖ࡛ुਐ୤ౣ൧๢ྲྀၫᅮቹ፫ᐇᔍᙊ᝙ᡅ᥄ᨈ᭴ᱳᵷṲὦ⁻Ⅼ≡⍖⑐┽♫❳⡿⥫⩽⬷ⱅⵀ⹆⽅しㅈ㉏㍆㑊㔭㙠㝢㡡㥬㨨㬠㱽㴵㹹㼦䀥䄡"), string_0);
			string text = this.dbprovider_0.ExecuteScalarString(string_);
			if (text == "")
			{
				text = string_0 + GClass0.smethod_0("4ĳȳ̰");
				text = text.Substring(0, int_0);
			}
			else
			{
				text = (Convert.ToInt32(text) + 1).ToString();
			}
			return text;
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("HŎɆ͌ќՂ؆݃ࡖौ੏ଁ౳൪๬ཫၹᅢ቗፸ᑱᕹᘶᝢ᡼᥶᩠᭴ᰰᵜṛὟ⁚ⅎ≓⍖⑁╃☻✢⡿⤳⩿⬦"), string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
