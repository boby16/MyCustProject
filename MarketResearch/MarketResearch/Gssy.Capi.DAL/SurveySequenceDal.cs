using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000012 RID: 18
	public class SurveySequenceDal
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D920 File Offset: 0x0000BB20
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("bŵɣͫѮո؋ݩࡦॽ੩୲఍എช༂ၧᅲቐፓᐽᕏ᙮ᝨᡯ᥽ᩮᭅᱰᵥṦίⁿⅳ≪⌮⑚╄♎❘⡌⤨⩎⭂Ⱕⴹ⹸⼲ぼ"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000D960 File Offset: 0x0000BB60
		public SurveySequence GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("yŬɤ͢ѥձ؄܉ࠂ१ੲ୐౓ഽ๏཮ၨᅯች፮ᑅᕰᙥᝦᡷ᥿ᩳ᭪ᰮᵚṄ὎⁘⅌∨⍎⑂┥☹❸⠲⥼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000D98C File Offset: 0x0000BB8C
		public SurveySequence GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveySequence surveySequence = new SurveySequence();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveySequence.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					surveySequence.SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString();
					surveySequence.SEQUENCE_ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]);
					surveySequence.PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					surveySequence.CIRCLE_A_CURRENT = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ")]);
					surveySequence.CIRCLE_A_COUNT = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ")]);
					surveySequence.CIRCLE_B_CURRENT = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ")]);
					surveySequence.CIRCLE_B_COUNT = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ")]);
					surveySequence.VERSION_ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")]);
					surveySequence.PAGE_BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("_ŏɊ͉єՈٌݏࡎैਗ਼୐ొ൏ไ")].ToString()));
					surveySequence.PAGE_END_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("]ōɌ͏іՍى݂࡚ॐ੊୏ౄ")].ToString()));
					surveySequence.RECORD_FILE = dataReader[global::GClass0.smethod_0("YŏɊ͇ѕՂ݂ٚࡊॎ੄")].ToString();
					surveySequence.RECORD_START_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("CŕɌ́џՈٔݙ࡝ॉ੕୒ౚ൐๊ཏ၄")].ToString()));
					surveySequence.RECORD_BEGIN_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("CŕɌ́џՈ݈ٔࡌॏ੎ୈౚ൐๊ཏ၄")]);
					surveySequence.RECORD_END_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("]ŋɎ̓љՎٖݍࡉूਗ਼୐ొ൏ไ")]);
					surveySequence.PAGE_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Yŉɀ̓њՐيݏࡄ")]);
					surveySequence.SURVEY_GUID = dataReader[global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
				}
			}
			return surveySequence;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		public List<SurveySequence> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveySequence> list = new List<SurveySequence>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveySequence
					{
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString(),
						SEQUENCE_ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]),
						PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						CIRCLE_A_CURRENT = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ")]),
						CIRCLE_A_COUNT = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ")]),
						CIRCLE_B_CURRENT = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ")]),
						CIRCLE_B_COUNT = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ")]),
						VERSION_ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")]),
						PAGE_BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("_ŏɊ͉єՈٌݏࡎैਗ਼୐ొ൏ไ")].ToString())),
						PAGE_END_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("]ōɌ͏іՍى݂࡚ॐ੊୏ౄ")].ToString())),
						RECORD_FILE = dataReader[global::GClass0.smethod_0("YŏɊ͇ѕՂ݂ٚࡊॎ੄")].ToString(),
						RECORD_START_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("CŕɌ́џՈٔݙ࡝ॉ੕୒ౚ൐๊ཏ၄")].ToString())),
						RECORD_BEGIN_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("CŕɌ́џՈ݈ٔࡌॏ੎ୈౚ൐๊ཏ၄")]),
						RECORD_END_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("]ŋɎ̓љՎٖݍࡉूਗ਼୐ొ൏ไ")]),
						PAGE_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Yŉɀ̓њՐيݏࡄ")]),
						SURVEY_GUID = dataReader[global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000DE30 File Offset: 0x0000C030
		public List<SurveySequence> GetList()
		{
			string string_ = global::GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑇᕶᙣᝤᡵᥡᩭ᭨ᰬᵄṘὍ⁍⅕∦⍇⑝┣♋❅");
			return this.GetListBySql(string_);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000DE54 File Offset: 0x0000C054
		public void Add(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u001dĝȁ̔Ђԛٮ܄ࠂटਅ୩ఛലิ༳အᄺሑጤᐱᕊᙛᝓᡟᥞᨒ᭪ᱭᵥṠὰ⁭Ⅼ≻⍵␜╼♫❼⡹⥮⩤⭪Ɑ⵸⹯⽡〈ㅳ㉣㍦㑥㕀㙗㝙㠰㥘㩓㭋㱛㵛㹓㽊䁕䅌䉑䍄䑂䕝䙋䝃䡘䤧䩉䭀䱚䵄乊佀偛兂剝卂呏喪嚰垩壐妸媳宫岻嶻庳循悶憬抱掤撢施暫枣梸槇檩殠沺涤溪澠炻熡犽玢璯疊皐瞉磰禍窟箋粋綞纙羛肋膚芖菽蒀薎蚉螈袓覉誏讎貁趉躙辑邍醎銇鏭钐闾雹韸飣駾髴鯽鳧鷣黿鿸ꃱꆟꋠꏴꓳꗠ꛼꟩ꣳꧭꫣꯥ곭궋껴꿠냧뇬닰돥듿뗌뛊럜룎맏뫅믍병뷚뻓뾹샆쇖싑쏞쓂엋웑쟏죉짌쫃쯇쳗췓컏쿈탁톯틐폄퓃픰혬휹\ud823\ud93e\uda34\udb3d\udc27\udd23\ude3f\udf38懶福ﬕﰍﴒ︅ｽsĨɢ̬ѷգصݼ࠱१੭ଲ౺ഺ๡ཀྵဿᅰሿ፭ᐻᔋᙃᜑᡇᤎᩇᬕ᱃ᴁṋἙ⁏℄≏⌝␗╔☖❐⠋⤇⨍⭒Ⱁⵚ⸁⼉〃ㅘ㈓㌑㑝㔸㘲㜺㡧㤪㨫㭤㰿㴻㹭㼤䀦䅮䈾䍪䐡䔼䙳䜡䡷䤺䨾䭴䰤䴠乽伴倱兾別匨"), new object[]
			{
				surveySequence_0.SURVEY_ID,
				surveySequence_0.SEQUENCE_ID,
				surveySequence_0.PAGE_ID,
				surveySequence_0.CIRCLE_A_CURRENT,
				surveySequence_0.CIRCLE_A_COUNT,
				surveySequence_0.CIRCLE_B_CURRENT,
				surveySequence_0.CIRCLE_B_COUNT,
				surveySequence_0.VERSION_ID,
				surveySequence_0.PAGE_BEGIN_TIME,
				surveySequence_0.PAGE_END_TIME,
				surveySequence_0.RECORD_FILE,
				surveySequence_0.RECORD_START_TIME,
				surveySequence_0.RECORD_BEGIN_TIME,
				surveySequence_0.RECORD_END_TIME,
				surveySequence_0.PAGE_TIME,
				surveySequence_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000DF58 File Offset: 0x0000C158
		public void Update(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("/ĩȼ̶Т԰ٔܠࠇःਆଊగാฉ༚ဟᄌሆጄᐃᕅᘷᜦᠶ᥁ᨳᬊᰌᴋḙἂ\u2005ℐ∜⍷⑫╵♳✨⡣⤬⩷⭣Ⱍⴈ⸝⼞』ㄇ㈋㌂㐙㔌㘀㝣㡿㥡㨻㬍㱃㴑㹬㽺䁽䅼䉧䍾䑲䔕䘉䜓䠕䥊䨃䭒䰉䴁乯佢偸兪剤卢呹啤噻坠塷女婲孚屐嵉帼弦怺慢戬捪携敖晝杁桑楝橕歐汏浒湏潄灟煇牜猧琻甥癿眶硿礭穃箶粬綾纰羾肥膻芧莴蒣薧蚦螶袼覥諐诒賎趖軚辖郆醪銡鎵钥閩隡鞼颠馾骣鮐鲋鶓麈鿻ꃧꇹꊣꏠ꒫ꗹꚂꞖꢀꦂꪙꮀ검궒꺅꾏냪뇴단뎼듾떸뛨랓뢃릆명믠볼뷸뻻뿲샴쇦심쏾쓻연요잎좒즖쫋쮖쳓춊캀쿻탫퇮틭폸퓣헫훠퟼\ud8f6\ud9e8\udaed\udbda\udcbe\udda0\udebc\udfbc簾窱שּׂﰻ﴾︳Ｉ>Ħọ̈̄зԧؠܬࠦस਽ପ౎൐์ཌထᅘቚጚᑁᕉᘶᜦᠡ᤮ᨲᬛᰁᴟḙἜ–℗∇⌃␟┘☑❳⡯⥱⨫⭾ⱽⴰ⹠⼙』ㄊ㈇㌕㐂㔚㘁㜍㠆㤞㨔㭶㱳㵸㸜㼆䀚䅂䈉䌃䑋䔙䙤䝲䡵䥴䩯䭻䱧䵠乩伋倗儉剓化吓啘嘈坰塷女婶孚屇嵂幛彎恓慝戸挪搶攲景朢栤楬樷欯汙浅湉潙灏焩牁獃琦甸瘤睸砲祼"), new object[]
			{
				surveySequence_0.ID,
				surveySequence_0.SURVEY_ID,
				surveySequence_0.SEQUENCE_ID,
				surveySequence_0.PAGE_ID,
				surveySequence_0.CIRCLE_A_CURRENT,
				surveySequence_0.CIRCLE_A_COUNT,
				surveySequence_0.CIRCLE_B_CURRENT,
				surveySequence_0.CIRCLE_B_COUNT,
				surveySequence_0.VERSION_ID,
				surveySequence_0.PAGE_BEGIN_TIME,
				surveySequence_0.PAGE_END_TIME,
				surveySequence_0.RECORD_FILE,
				surveySequence_0.RECORD_START_TIME,
				surveySequence_0.RECORD_BEGIN_TIME,
				surveySequence_0.RECORD_END_TIME,
				surveySequence_0.PAGE_TIME,
				surveySequence_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000E06C File Offset: 0x0000C26C
		public void Delete(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("lŢɪ͠Ѱզ؂ݧࡲॐ੓ଽ౏൮๨཯ၽᅮቅ፰ᑥᕦᙷ᝿ᡳᥪᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), surveySequence_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("^Ŝɔ͒тՐشݕࡀफ़੝ଯౝ൸๾ཽၯᅰቛ።ᑷᕰᙡ᝭ᡡᥤ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 17;
			string[] array = new string[17];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("臮厫純僶");
				array[1] = global::GClass0.smethod_0("闪剴純僶");
				array[2] = global::GClass0.smethod_0("闫剳岌儕埶");
				array[3] = global::GClass0.smethod_0("闫剳驶͋х");
				array[4] = global::GClass0.smethod_0("HĨ幅展噈玀妩璭浱");
				array[5] = global::GClass0.smethod_0("FĦ幇岮瞬改捱");
				array[6] = global::GClass0.smethod_0("KĨ幅展噈玀妩璭浱");
				array[7] = global::GClass0.smethod_0("EĦ幇岮瞬改捱");
				array[8] = global::GClass0.smethod_0("跩琴灌搯笔囶");
				array[9] = global::GClass0.smethod_0("迓偢韨鮝鱱鉡援鋵");
				array[10] = global::GClass0.smethod_0("禳帇韨鮝鱱鉡援鋵");
				array[11] = global::GClass0.smethod_0("闧馐驲壿媑婑釰抅䛷");
				array[12] = global::GClass0.smethod_0("寲從嵜铻憀䯰炁堄凈泴鿵");
				array[13] = global::GClass0.smethod_0("闤馑驽屒鏵娅忏ܣ燐汱");
				array[14] = global::GClass0.smethod_0("闤馑驽屒鏵篖慛ܣ燐汱");
				array[15] = global::GClass0.smethod_0("闤馑驽䱸焮想鍻ܣ燐汱");
				array[16] = global::GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
				array[2] = global::GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅");
				array[3] = global::GClass0.smethod_0("WŇɂ́ќՋم");
				array[4] = global::GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ");
				array[5] = global::GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ");
				array[6] = global::GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ");
				array[7] = global::GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ");
				array[8] = global::GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ");
				array[9] = global::GClass0.smethod_0("_ŏɊ͉єՈٌݏࡎैਗ਼୐ొ൏ไ");
				array[10] = global::GClass0.smethod_0("]ōɌ͏іՍى݂࡚ॐ੊୏ౄ");
				array[11] = global::GClass0.smethod_0("YŏɊ͇ѕՂ݂ٚࡊॎ੄");
				array[12] = global::GClass0.smethod_0("CŕɌ́џՈٔݙ࡝ॉ੕୒ౚ൐๊ཏ၄");
				array[13] = global::GClass0.smethod_0("CŕɌ́џՈ݈ٔࡌॏ੎ୈౚ൐๊ཏ၄");
				array[14] = global::GClass0.smethod_0("]ŋɎ̓љՎٖݍࡉूਗ਼୐ొ൏ไ");
				array[15] = global::GClass0.smethod_0("Yŉɀ̓њՐيݏࡄ");
				array[16] = global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
			}
			return array;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		public string[,] ExcelContent(int int_0, List<SurveySequence> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveySequence surveySequence in list_0)
			{
				array[num, 0] = surveySequence.ID.ToString();
				array[num, 1] = surveySequence.SURVEY_ID;
				array[num, 2] = surveySequence.SEQUENCE_ID.ToString();
				array[num, 3] = surveySequence.PAGE_ID;
				array[num, 4] = surveySequence.CIRCLE_A_CURRENT.ToString();
				array[num, 5] = surveySequence.CIRCLE_A_COUNT.ToString();
				array[num, 6] = surveySequence.CIRCLE_B_CURRENT.ToString();
				array[num, 7] = surveySequence.CIRCLE_B_COUNT.ToString();
				array[num, 8] = surveySequence.VERSION_ID.ToString();
				array[num, 9] = surveySequence.PAGE_BEGIN_TIME.ToString();
				array[num, 10] = surveySequence.PAGE_END_TIME.ToString();
				array[num, 11] = surveySequence.RECORD_FILE;
				array[num, 12] = surveySequence.RECORD_START_TIME.ToString();
				array[num, 13] = surveySequence.RECORD_BEGIN_TIME.ToString();
				array[num, 14] = surveySequence.RECORD_END_TIME.ToString();
				array[num, 15] = surveySequence.PAGE_TIME.ToString();
				array[num, 16] = surveySequence.SURVEY_GUID;
				num++;
			}
			return array;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public SurveySequence GetBySequenceID(string string_0, int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u0014ăȉ́ЀԖ١ݪࠟॸ੯୳౶ച๪ཌྷ၅ᅀቐፍᑠᕗᙀᝅᡊ᥀ᩎᭉᰋᵽṡὭ⁵Ⅳ∅⍷⑶╰♷❥⡆⥁⩔⭘Ⱖⴽ⹢⼨なㄱ㈵㍕㑝㕖㘱㝃㡊㥟㩘㭉㱅㵉㹌㽗䁎䅂䈥䌹䑸䔳䙼"), string_0, int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		public List<SurveySequence> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("mŸɰ;ѹխؘܝࠖॳ੦୼౿഑๣ཚၜᅛ቉ፒᑹᕌᙙᝒᡃ᥋ᩇᭆᰂᵶṨ὚⁌⅘∼⍈⑏╋♎❒⡏⥊⩝⭗Ⱟⴶ⹫⼿びㄪ㈬㍄㑘㕍㙍㝕㠦㥇㩝㬣㱋㵅"), string_0);
			return this.GetListBySql(string_);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000E510 File Offset: 0x0000C710
		public void DeleteBySequenceID(string string_0, int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u0001āȏ̇Еԅ؟ݸ࡯ॳ੶ଚ౪്ๅཀၐᅍበፗᑀᕅᙊᝀᡎ᥉ᨋ᭽ᱡᵭṵὣ\u2005ⅷ≶⍰⑷╥♆❁⡔⥘⨦⬽Ɫ⴨⹪⼱〵ㅕ㉝㍖㐱㕃㙊㝟㡘㥉㩅㭉㱌㵗㹎㽂䀥䄹䉸䌳䑼"), string_0, int_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000E544 File Offset: 0x0000C744
		public void UpdateNext(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u001dĈȀ̎Љԝ٨܄ࠉऐਊଗ౪൫๩༟ၸᅯታ፶ᐚᕪᙍᝅᡀᥐᩍ᭠᱗ᵀṅὊ⁀ⅎ≉⌋⑽╡♭❵⡣⤅⩷⭶Ɒ⵷⹥⽆ぁㅔ㉘㌦㐽㕢㘨㝪㠱㤵㩕㭝㱖㴱㹃㽊䁟䅘䉉䍅䑉䕌䙗䝎䡂䤥䨹䭸䰳䵼"), surveySequence_0.SURVEY_ID, surveySequence_0.SEQUENCE_ID);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			if (num <= 0)
			{
				this.Add(surveySequence_0);
			}
			else
			{
				this.UpdateBySequenceID(surveySequence_0);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000E598 File Offset: 0x0000C798
		public void UpdateBySequenceID(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("5ďȚ̜ЈԞٺ܊࠭थਠରభഀื༠ဥᄪሠጮᐩᕫᘙᜌ᠜ᥧᨖᬄᰃᴆḝἈ\u2004℟∃⌝␛╀☉❄⠟⤛⩵⭼ⱦ⵰⹾⽴はㅮ㉱㍮㑹㕹㙸㝬㡦㥳㨆㬘㰄㵘㸖㽜䀌䅜䉗䍏䑟䕗䙟䝆䡙䥈䩕䭚䱁䵝乆伱倭儯創匸呱唧噉址塚奄婊孀屛嵁幝彂恕憭抬掸撲斯曚柄棘榌櫀殈泘涰溻澣炳熣犫玲璮疴皩瞦碽禩窲篅糙緃纙翖肝至芈莘蒎薈蚓螖袖覈誟讑賴跮軲辪部醲鋢鎝钍閌随鞖颊馂骁鮌鲊鶜麖龈ꂍꇺꊞꎀ꒜ꖜꛁꞀꣅꦐꪚꯥ공귴껷꿮냵뇡닪돲듸뗢뛧러뢈릚몆뮂볟붒뺒뿜삇솳싌쏘쓟엔웈쟝죇집쫟쯙쳑춳캯쾱킷퇴튿펼퓱햬횦ퟛ\ud8cd\ud9c4\udac9\udbd7\udcc0\udddc\uded1\udfd5拉﨣ﬢﰭﴭ︽Ｕ)Ēțͽѡջءݨ࡫प੺ଇ఑ഐฝ༃နᄐላጃᐈᔔᘞᜀ᠅ᤂᩦ᭸ᱤᴸṳή‽ℓ≮⍼⑻╾♥❭⡱⥺⩳⬕Ⰹⴓ⹉⼀々ㅒ㈎㍺㑤㕮㙸㝬㠈㥴㩳㭷㱲㵦㹻㽾䁩䅛䈾䌠䐼䔼䙡䜨䡥䤰䨶䭔䱚䵗串佂偕兞剛午呂啈噏坖塁奃娦嬸尤嵸帰彼"), new object[]
			{
				surveySequence_0.ID,
				surveySequence_0.SURVEY_ID,
				surveySequence_0.SEQUENCE_ID,
				surveySequence_0.PAGE_ID,
				surveySequence_0.CIRCLE_A_CURRENT,
				surveySequence_0.CIRCLE_A_COUNT,
				surveySequence_0.CIRCLE_B_CURRENT,
				surveySequence_0.CIRCLE_B_COUNT,
				surveySequence_0.VERSION_ID,
				surveySequence_0.PAGE_BEGIN_TIME,
				surveySequence_0.PAGE_END_TIME,
				surveySequence_0.RECORD_FILE,
				surveySequence_0.RECORD_START_TIME,
				surveySequence_0.RECORD_BEGIN_TIME,
				surveySequence_0.RECORD_END_TIME,
				surveySequence_0.PAGE_TIME
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000E6A4 File Offset: 0x0000C8A4
		public SurveySequence GetAudioByPageId(string string_0, int int_0, string string_1)
		{
			string string_2 = global::GClass0.smethod_0("");
			string text = global::GClass0.smethod_0("1");
			int num = int_0;
			if (int_0 == 0)
			{
				string_2 = string.Format(global::GClass0.smethod_0("7ĦȮ̤Уԋپ܎࠙ऊਏଜఖഔณ༊ဝᄗቲ጗ᐂᔀᘃ᝭᠟᤾ᨸᬿᰭᴾḕἠ‵ℶ∧⌯␣╚☞❪⡴⥾⩨⭼Ⱈⵤ⹣⽧ぢㅶ㉫㍮㑹㕫㘓㜊㡗㤛㩗㬎㰈㵦㹨㽡䀄䅳䉣䍦䑥䕀䙗䝙䠼䤦䨽䭢䰩䵪丱伵偕兝剖匱呃啊噟坘塉奅婉孌屗嵎幂弥怸愽戲挡"), string_0, string_1);
				text = this.dbprovider_0.ExecuteScalarString(string_2);
				if (text != global::GClass0.smethod_0("") && text != null)
				{
					num = int.Parse(text);
				}
			}
			SurveySequence result;
			if (num == 0)
			{
				SurveySequence surveySequence = new SurveySequence();
				result = surveySequence;
			}
			else
			{
				string_2 = string.Format(global::GClass0.smethod_0("\u0014ăȉ́ЀԖ١ݪࠟॸ੯୳౶ച๪ཌྷ၅ᅀቐፍᑠᕗᙀᝅᡊ᥀ᩎᭉᰋᵽṡὭ⁵Ⅳ∅⍷⑶╰♷❥⡆⥁⩔⭘Ⱖⴽ⹢⼨なㄱ㈵㍕㑝㕖㘱㝃㡊㥟㩘㭉㱅㵉㹌㽗䁎䅂䈥䌹䑸䔳䙼"), string_0, num);
				result = this.GetBySql(string_2);
			}
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000E748 File Offset: 0x0000C948
		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("TŊɂ͈јՎ؊ݏ࡚ैੋଅ౷ൖ๐བྷ၅ᅦቍ፸ᑭᕮᙿ᝷᡻ᥲᨶ᭢ᱼᵶṠὴ‰⅜≛⍟⑚╎♓❖⡁⥃⨻⬢Ɀⴳ⹿⼦"), string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		// Token: 0x04000019 RID: 25
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
