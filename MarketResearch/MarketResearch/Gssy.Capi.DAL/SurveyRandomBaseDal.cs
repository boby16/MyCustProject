using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x0200000F RID: 15
	public class SurveyRandomBaseDal
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("`ŷɽ͵Ѭպ؍ݯࡤॿ੧୼ఏഌฌ༄ၥᅰቮ፭ᐿᕍᙨᝮᡭ᥿᩠ᭊᱶᵸṱύ⁾⅐≰⍣⑪┮♚❄⡎⥘⩌⬨ⱎⵂ⸥⼹へㄲ㉼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000BE04 File Offset: 0x0000A004
		public SurveyRandom GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u007fŮɦͬѫճ؆܏ࠄ॥ੰ୮౭ിํཨၮᅭቿ፠ᑊᕶᙸ᝱᡻᥾ᩐ᭰ᱣᵪḮ὚⁄ⅎ≘⍌␨╎♂✥⠹⥸⨲⭼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000BE30 File Offset: 0x0000A030
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

		// Token: 0x06000108 RID: 264 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
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

		// Token: 0x06000109 RID: 265 RVA: 0x0000C188 File Offset: 0x0000A388
		public List<SurveyRandom> GetList()
		{
			string string_ = global::GClass0.smethod_0("yŬɤ͢ѥձ؄܉ࠂ१ੲ୐౓ഽ๏཮ၨᅯች፮ᑄᕴᙺ᝷᡽᥼ᩒ᭮ᱽᵨḬὄ⁘⅍≍⍕␦╇♝✣⡋⥅");
			return this.GetListBySql(string_);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		public void Add(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u0091ƙʅΐ҆և۲ޘ࢞ছઁ௭ಟ඾ຸ྿ႭᆾኔᎤᒪᖧᚭឬᢂ᧞ᫍᯘᲔᷨữΎ⃮⇲⋯⏪⓽◷⚞⟠⣥⧪⫽⯹⳥ⷤ⻤⿶・㇢㋲㎉㓵㗶㛧㟲㣴㧖㫑㯓㳃㷕㻛㿔䃝䆻䋕䏚䓐䗖䚾䟁䣑䧝䫋䯃䳘䷔仉俆僌凂努受哅嗍囆城壍夠娷嬳尸崾帢录怪愶戸挱搻放昭朢栵椻機歁氾洪渤漭瀧焪爹猶琡男癐睍砲礞稐笙簓紖縅缊耝脃艥荹萝蔀蘍蜗蠙褗詢謞谙贙踜輌逑鄘鈁錐鐍锇陫靡頖饾驲魨鱹鵨鸒鼞ꁃꄇꉋꌒꐘꔔꙉ꜀ꡍ꤈ꨂꬊ걗괙깗꼎뀄넀뉝댖둙딄똎뜆롛뤫멣묺배뵠븯뽤쀴셬술써쐸앨옥읬젼쥴쨶쭰찠쵰츳콴퀤턠퉽팴퐴핾혥휨"), new object[]
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

		// Token: 0x0600010B RID: 267 RVA: 0x0000C25C File Offset: 0x0000A45C
		public void Update(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("¡ƣʶΰҤ֪ێ޾࢙ঙજ஌಑ඵງྋႀᆌ኏Ꭳᒁᖬᚻ៽ᢏᦞ᪎᯹᲋ᶂẄᾃₑ↊⊍⎘⒔◯⛳⟭⣫⦰⫻⮴⳯ⷫ⺗⾐め㆐㊖㎈㒏㗱㛡㟮㣹㧯㪚㮄㲘㶐㻍㾇䃉䆔䊞䏠䓥䗪䛽䟹䣥䧤䫤䯶䳦䷦仫俠傄冞劂历哛喬団垺墰姘嫕寝峝嶷庫徵悳懨抦揬撷斣曞柌棞槎櫄毝泗淄滉濁烁熣犿玡璧甄癋眀硛祗稨笸簶紳縹缸耫脺舼茵萵蔷虎蝐行褐詜謔豄贵踧輫造鄬鈯錾鐳锚阊靬顼饦驺鬢鱯鴪鹺鼇ꀕꄝꈖꌞꐝꔐꘝ꜈ꠘꥹꩪꭴ걨괼깾꼸끨넑눃댏됄땰뙳띢롯륾멮묊반봊븖뽎쀍셎숞썸쑣앰왨읤존줋쨗쬉챓촖츖콘퀈텰퉷퍳푶핚홇흂\ud85b\ud94e\uda53\udb5d\udc38\udd2a\ude36\udf32"), new object[]
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

		// Token: 0x0600010C RID: 268 RVA: 0x0000C31C File Offset: 0x0000A51C
		public void Delete(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("nŬɤ͢Ѳՠ؄ݥࡰ८੭ି్൨๮཭ၿᅠቊ፶ᑸᕱᙻ᝾ᡐᥰᩣ᭪ᰮᵚṄ὎⁘⅌∨⍎⑂┥☹❸⠲⥼"), surveyRandom_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000C354 File Offset: 0x0000A554
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("XŞɖ͜ьՒضݓࡆड़੟଱ృൺ๼ཻၩᅲቘ፨ᑦᕣᙩᝨᡆᥢᩱ᭤");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000C37C File Offset: 0x0000A57C
		public void ExecuteProceddure(string string_0)
		{
			this.dbprovider_0.ExecuteNonQuery(string_0);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000C398 File Offset: 0x0000A598
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

		// Token: 0x06000110 RID: 272 RVA: 0x0000C500 File Offset: 0x0000A700
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

		// Token: 0x06000111 RID: 273 RVA: 0x0000C658 File Offset: 0x0000A858
		public List<SurveyRandom> GetList(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("?ĮȦ̬ЫԳ٦ݯࡤथਰମభട๭཈၎ᅍ቟ፀᑪᕖᙘᝑᡛᥞᩰ᭐᱃ᵊḎ὚⁄ⅎ≘⍌␈╴♳❷⡲⥦⩻⭾Ⱪⵛ⸣⼺ぞㅚ㉉㍜㐿㔷㙷㝻㡰㤳㩃㭄㱕㵜㹚㽄䁃䅅䉕䍚䑍䕓䘻䜢䡿䤳䩿䬦"), string_0);
			return this.GetListBySql(string_);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000C680 File Offset: 0x0000A880
		public List<SurveyRandom> GetGroupInfo()
		{
			string string_ = global::GClass0.smethod_0("ØǏ˅ύӄגڥߥࢭ৓૔௅బപิ༳ဵᄥሪጽᐣᕚᙕ᜷ᠼᤧᨿᬤ᱇ᵄṄὌ‪ℹ≉⌦␤┩☰✪⠷⥂⨧⬲Ⱀⴓ⹽⼏〮ㄨ㈯㌽㐮㔄㘴㜺㠷㤽㨼㬒㰮㴽㸨㽬䀪䅪䈾䌠䐢䔴䘠䝤䡣䤣䩯䬓䱪䵬乫佹偢入剰卼吊唑噷坵塠奷娖嬐屨嵼幢役恻愊扫捱搇敇昋杵桶楧橲歴汖浑湓潃灈煟牍猸瑘畄癑睑硁礲穓等簯絯縣罝聞腏艚荜葎蕉虋蝛衐襇評");
			int num = 0;
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_);
			List<SurveyRandom> list = new List<SurveyRandom>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyRandom
					{
						ID = num,
						SURVEY_ID = global::GClass0.smethod_0(""),
						QUESTION_SET = dataReader[global::GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॐੇ୕")].ToString(),
						QUESTION_NAME = global::GClass0.smethod_0(""),
						CODE = global::GClass0.smethod_0(""),
						PARENT_CODE = global::GClass0.smethod_0(""),
						RANDOM_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Hņɋ͖ьՕ")]),
						RANDOM_SET1 = 0,
						RANDOM_SET2 = 0,
						RANDOM_SET3 = 0,
						IS_FIX = 0
					});
					num++;
				}
			}
			return list;
		}

		// Token: 0x04000014 RID: 20
		private DBProvider dbprovider_0 = new DBProvider(1);

		// Token: 0x04000015 RID: 21
		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
