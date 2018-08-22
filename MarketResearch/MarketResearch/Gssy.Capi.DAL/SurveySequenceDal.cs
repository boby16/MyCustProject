using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveySequenceDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("bŵɣͫѮո؋ݩࡦॽ੩୲఍എช༂ၧᅲቐፓᐽᕏ᙮ᝨᡯ᥽ᩮᭅᱰᵥṦίⁿⅳ≪⌮⑚╄♎❘⡌⤨⩎⭂Ⱕⴹ⹸⼲ぼ"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveySequence GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("yŬɤ͢ѥձ؄܉ࠂ१ੲ୐౓ഽ๏཮ၨᅯች፮ᑅᕰᙥᝦᡷ᥿ᩳ᭪ᰮᵚṄ὎⁘⅌∨⍎⑂┥☹❸⠲⥼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveySequence GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveySequence surveySequence = new SurveySequence();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveySequence.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveySequence.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveySequence.SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]);
					surveySequence.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					surveySequence.CIRCLE_A_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ")]);
					surveySequence.CIRCLE_A_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ")]);
					surveySequence.CIRCLE_B_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ")]);
					surveySequence.CIRCLE_B_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ")]);
					surveySequence.VERSION_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")]);
					surveySequence.PAGE_BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("_ŏɊ͉єՈٌݏࡎैਗ਼୐ొ൏ไ")].ToString()));
					surveySequence.PAGE_END_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("]ōɌ͏іՍى݂࡚ॐ੊୏ౄ")].ToString()));
					surveySequence.RECORD_FILE = dataReader[GClass0.smethod_0("YŏɊ͇ѕՂ݂ٚࡊॎ੄")].ToString();
					surveySequence.RECORD_START_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("CŕɌ́џՈٔݙ࡝ॉ੕୒ౚ൐๊ཏ၄")].ToString()));
					surveySequence.RECORD_BEGIN_TIME = Convert.ToInt32(dataReader[GClass0.smethod_0("CŕɌ́џՈ݈ٔࡌॏ੎ୈౚ൐๊ཏ၄")]);
					surveySequence.RECORD_END_TIME = Convert.ToInt32(dataReader[GClass0.smethod_0("]ŋɎ̓љՎٖݍࡉूਗ਼୐ొ൏ไ")]);
					surveySequence.PAGE_TIME = Convert.ToInt32(dataReader[GClass0.smethod_0("Yŉɀ̓њՐيݏࡄ")]);
					surveySequence.SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
				}
			}
			return surveySequence;
		}

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
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]),
						PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						CIRCLE_A_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ")]),
						CIRCLE_A_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ")]),
						CIRCLE_B_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ")]),
						CIRCLE_B_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ")]),
						VERSION_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")]),
						PAGE_BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("_ŏɊ͉єՈٌݏࡎैਗ਼୐ొ൏ไ")].ToString())),
						PAGE_END_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("]ōɌ͏іՍى݂࡚ॐ੊୏ౄ")].ToString())),
						RECORD_FILE = dataReader[GClass0.smethod_0("YŏɊ͇ѕՂ݂ٚࡊॎ੄")].ToString(),
						RECORD_START_TIME = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("CŕɌ́џՈٔݙ࡝ॉ੕୒ౚ൐๊ཏ၄")].ToString())),
						RECORD_BEGIN_TIME = Convert.ToInt32(dataReader[GClass0.smethod_0("CŕɌ́џՈ݈ٔࡌॏ੎ୈౚ൐๊ཏ၄")]),
						RECORD_END_TIME = Convert.ToInt32(dataReader[GClass0.smethod_0("]ŋɎ̓љՎٖݍࡉूਗ਼୐ొ൏ไ")]),
						PAGE_TIME = Convert.ToInt32(dataReader[GClass0.smethod_0("Yŉɀ̓њՐيݏࡄ")]),
						SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveySequence> GetList()
		{
			string string_ = GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑇᕶᙣᝤᡵᥡᩭ᭨ᰬᵄṘὍ⁍⅕∦⍇⑝┣♋❅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u001dĝȁ̔Ђԛٮ܄ࠂटਅ୩ఛലิ༳အᄺሑጤᐱᕊᙛᝓᡟᥞᨒ᭪ᱭᵥṠὰ⁭Ⅼ≻⍵␜╼♫❼⡹⥮⩤⭪Ɑ⵸⹯⽡〈ㅳ㉣㍦㑥㕀㙗㝙㠰㥘㩓㭋㱛㵛㹓㽊䁕䅌䉑䍄䑂䕝䙋䝃䡘䤧䩉䭀䱚䵄乊佀偛兂剝卂呏喪嚰垩壐妸媳宫岻嶻庳循悶憬抱掤撢施暫枣梸槇檩殠沺涤溪澠炻熡犽玢璯疊皐瞉磰禍窟箋粋綞纙羛肋膚芖菽蒀薎蚉螈袓覉誏讎貁趉躙辑邍醎銇鏭钐闾雹韸飣駾髴鯽鳧鷣黿鿸ꃱꆟꋠꏴꓳꗠ꛼꟩ꣳꧭꫣꯥ곭궋껴꿠냧뇬닰돥듿뗌뛊럜룎맏뫅믍병뷚뻓뾹샆쇖싑쏞쓂엋웑쟏죉짌쫃쯇쳗췓컏쿈탁톯틐폄퓃픰혬휹\ud823\ud93e\uda34\udb3d\udc27\udd23\ude3f\udf38懶福ﬕﰍﴒ︅ｽsĨɢ̬ѷգصݼ࠱१੭ଲ౺ഺ๡ཀྵဿᅰሿ፭ᐻᔋᙃᜑᡇᤎᩇᬕ᱃ᴁṋἙ⁏℄≏⌝␗╔☖❐⠋⤇⨍⭒Ⱁⵚ⸁⼉〃ㅘ㈓㌑㑝㔸㘲㜺㡧㤪㨫㭤㰿㴻㹭㼤䀦䅮䈾䍪䐡䔼䙳䜡䡷䤺䨾䭴䰤䴠乽伴倱兾別匨"), new object[]
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

		public void Update(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(GClass0.smethod_0("/ĩȼ̶Т԰ٔܠࠇःਆଊగാฉ༚ဟᄌሆጄᐃᕅᘷᜦᠶ᥁ᨳᬊᰌᴋḙἂ\u2005ℐ∜⍷⑫╵♳✨⡣⤬⩷⭣Ⱍⴈ⸝⼞』ㄇ㈋㌂㐙㔌㘀㝣㡿㥡㨻㬍㱃㴑㹬㽺䁽䅼䉧䍾䑲䔕䘉䜓䠕䥊䨃䭒䰉䴁乯佢偸兪剤卢呹啤噻坠塷女婲孚屐嵉帼弦怺慢戬捪携敖晝杁桑楝橕歐汏浒湏潄灟煇牜猧琻甥癿眶硿礭穃箶粬綾纰羾肥膻芧莴蒣薧蚦螶袼覥諐诒賎趖軚辖郆醪銡鎵钥閩隡鞼颠馾骣鮐鲋鶓麈鿻ꃧꇹꊣꏠ꒫ꗹꚂꞖꢀꦂꪙꮀ검궒꺅꾏냪뇴단뎼듾떸뛨랓뢃릆명믠볼뷸뻻뿲샴쇦심쏾쓻연요잎좒즖쫋쮖쳓춊캀쿻탫퇮틭폸퓣헫훠퟼\ud8f6\ud9e8\udaed\udbda\udcbe\udda0\udebc\udfbc簾窱שּׂﰻ﴾︳Ｉ>Ħọ̈̄зԧؠܬࠦस਽ପ౎൐์ཌထᅘቚጚᑁᕉᘶᜦᠡ᤮ᨲᬛᰁᴟḙἜ–℗∇⌃␟┘☑❳⡯⥱⨫⭾ⱽⴰ⹠⼙』ㄊ㈇㌕㐂㔚㘁㜍㠆㤞㨔㭶㱳㵸㸜㼆䀚䅂䈉䌃䑋䔙䙤䝲䡵䥴䩯䭻䱧䵠乩伋倗儉剓化吓啘嘈坰塷女婶孚屇嵂幛彎恓慝戸挪搶攲景朢栤楬樷欯汙浅湉潙灏焩牁獃琦甸瘤睸砲祼"), new object[]
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

		public void Delete(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(GClass0.smethod_0("lŢɪ͠Ѱզ؂ݧࡲॐ੓ଽ౏൮๨཯ၽᅮቅ፰ᑥᕦᙷ᝿ᡳᥪᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), surveySequence_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("^Ŝɔ͒тՐشݕࡀफ़੝ଯౝ൸๾ཽၯᅰቛ።ᑷᕰᙡ᝭ᡡᥤ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 17;
			string[] array = new string[17];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = "问卷编号";
				array[2] = GClass0.smethod_0("闫剳岌儕埶");
				array[3] = GClass0.smethod_0("闫剳驶͋х");
				array[4] = GClass0.smethod_0("HĨ幅展噈玀妩璭浱");
				array[5] = GClass0.smethod_0("FĦ幇岮瞬改捱");
				array[6] = GClass0.smethod_0("KĨ幅展噈玀妩璭浱");
				array[7] = GClass0.smethod_0("EĦ幇岮瞬改捱");
				array[8] = GClass0.smethod_0("跩琴灌搯笔囶");
				array[9] = GClass0.smethod_0("迓偢韨鮝鱱鉡援鋵");
				array[10] = GClass0.smethod_0("禳帇韨鮝鱱鉡援鋵");
				array[11] = GClass0.smethod_0("闧馐驲壿媑婑釰抅䛷");
				array[12] = GClass0.smethod_0("寲從嵜铻憀䯰炁堄凈泴鿵");
				array[13] = GClass0.smethod_0("闤馑驽屒鏵娅忏ܣ燐汱");
				array[14] = GClass0.smethod_0("闤馑驽屒鏵篖慛ܣ燐汱");
				array[15] = GClass0.smethod_0("闤馑驽䱸焮想鍻ܣ燐汱");
				array[16] = GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = "SURVEY_ID";
				array[2] = GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅");
				array[3] = GClass0.smethod_0("WŇɂ́ќՋم");
				array[4] = GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ");
				array[5] = GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ");
				array[6] = GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ");
				array[7] = GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ");
				array[8] = GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ");
				array[9] = GClass0.smethod_0("_ŏɊ͉єՈٌݏࡎैਗ਼୐ొ൏ไ");
				array[10] = GClass0.smethod_0("]ōɌ͏іՍى݂࡚ॐ੊୏ౄ");
				array[11] = GClass0.smethod_0("YŏɊ͇ѕՂ݂ٚࡊॎ੄");
				array[12] = GClass0.smethod_0("CŕɌ́џՈٔݙ࡝ॉ੕୒ౚ൐๊ཏ၄");
				array[13] = GClass0.smethod_0("CŕɌ́џՈ݈ٔࡌॏ੎ୈౚ൐๊ཏ၄");
				array[14] = GClass0.smethod_0("]ŋɎ̓љՎٖݍࡉूਗ਼୐ొ൏ไ");
				array[15] = GClass0.smethod_0("Yŉɀ̓њՐيݏࡄ");
				array[16] = GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
			}
			return array;
		}

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

		public SurveySequence GetBySequenceID(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u0014ăȉ́ЀԖ١ݪࠟॸ੯୳౶ച๪ཌྷ၅ᅀቐፍᑠᕗᙀᝅᡊ᥀ᩎᭉᰋᵽṡὭ⁵Ⅳ∅⍷⑶╰♷❥⡆⥁⩔⭘Ⱖⴽ⹢⼨なㄱ㈵㍕㑝㕖㘱㝃㡊㥟㩘㭉㱅㵉㹌㽗䁎䅂䈥䌹䑸䔳䙼"), string_0, int_0);
			return this.GetBySql(string_);
		}

		public List<SurveySequence> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("mŸɰ;ѹխؘܝࠖॳ੦୼౿഑๣ཚၜᅛ቉ፒᑹᕌᙙᝒᡃ᥋ᩇᭆᰂᵶṨ὚⁌⅘∼⍈⑏╋♎❒⡏⥊⩝⭗Ⱟⴶ⹫⼿びㄪ㈬㍄㑘㕍㙍㝕㠦㥇㩝㬣㱋㵅"), string_0);
			return this.GetListBySql(string_);
		}

		public void DeleteBySequenceID(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u0001āȏ̇Еԅ؟ݸ࡯ॳ੶ଚ౪്ๅཀၐᅍበፗᑀᕅᙊᝀᡎ᥉ᨋ᭽ᱡᵭṵὣ\u2005ⅷ≶⍰⑷╥♆❁⡔⥘⨦⬽Ɫ⴨⹪⼱〵ㅕ㉝㍖㐱㕃㙊㝟㡘㥉㩅㭉㱌㵗㹎㽂䀥䄹䉸䌳䑼"), string_0, int_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void UpdateNext(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u001dĈȀ̎Љԝ٨܄ࠉऐਊଗ౪൫๩༟ၸᅯታ፶ᐚᕪᙍᝅᡀᥐᩍ᭠᱗ᵀṅὊ⁀ⅎ≉⌋⑽╡♭❵⡣⤅⩷⭶Ɒ⵷⹥⽆ぁㅔ㉘㌦㐽㕢㘨㝪㠱㤵㩕㭝㱖㴱㹃㽊䁟䅘䉉䍅䑉䕌䙗䝎䡂䤥䨹䭸䰳䵼"), surveySequence_0.SURVEY_ID, surveySequence_0.SEQUENCE_ID);
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

		public void UpdateBySequenceID(SurveySequence surveySequence_0)
		{
			string string_ = string.Format(GClass0.smethod_0("5ďȚ̜ЈԞٺ܊࠭थਠରభഀื༠ဥᄪሠጮᐩᕫᘙᜌ᠜ᥧᨖᬄᰃᴆḝἈ\u2004℟∃⌝␛╀☉❄⠟⤛⩵⭼ⱦ⵰⹾⽴はㅮ㉱㍮㑹㕹㙸㝬㡦㥳㨆㬘㰄㵘㸖㽜䀌䅜䉗䍏䑟䕗䙟䝆䡙䥈䩕䭚䱁䵝乆伱倭儯創匸呱唧噉址塚奄婊孀屛嵁幝彂恕憭抬掸撲斯曚柄棘榌櫀殈泘涰溻澣炳熣犫玲璮疴皩瞦碽禩窲篅糙緃纙翖肝至芈莘蒎薈蚓螖袖覈誟讑賴跮軲辪部醲鋢鎝钍閌随鞖颊馂骁鮌鲊鶜麖龈ꂍꇺꊞꎀ꒜ꖜꛁꞀꣅꦐꪚꯥ공귴껷꿮냵뇡닪돲듸뗢뛧러뢈릚몆뮂볟붒뺒뿜삇솳싌쏘쓟엔웈쟝죇집쫟쯙쳑춳캯쾱킷퇴튿펼퓱햬횦ퟛ\ud8cd\ud9c4\udac9\udbd7\udcc0\udddc\uded1\udfd5拉﨣ﬢﰭﴭ︽Ｕ)Ēțͽѡջءݨ࡫प੺ଇ఑ഐฝ༃နᄐላጃᐈᔔᘞᜀ᠅ᤂᩦ᭸ᱤᴸṳή‽ℓ≮⍼⑻╾♥❭⡱⥺⩳⬕Ⰹⴓ⹉⼀々ㅒ㈎㍺㑤㕮㙸㝬㠈㥴㩳㭷㱲㵦㹻㽾䁩䅛䈾䌠䐼䔼䙡䜨䡥䤰䨶䭔䱚䵗串佂偕兞剛午呂啈噏坖塁奃娦嬸尤嵸帰彼"), new object[]
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

		public SurveySequence GetAudioByPageId(string string_0, int int_0, string string_1)
		{
			string string_2 = "";
			string text = "0";
			int num = int_0;
			if (int_0 == 0)
			{
				string_2 = string.Format(GClass0.smethod_0("7ĦȮ̤Уԋپ܎࠙ऊਏଜఖഔณ༊ဝᄗቲ጗ᐂᔀᘃ᝭᠟᤾ᨸᬿᰭᴾḕἠ‵ℶ∧⌯␣╚☞❪⡴⥾⩨⭼Ⱈⵤ⹣⽧ぢㅶ㉫㍮㑹㕫㘓㜊㡗㤛㩗㬎㰈㵦㹨㽡䀄䅳䉣䍦䑥䕀䙗䝙䠼䤦䨽䭢䰩䵪丱伵偕兝剖匱呃啊噟坘塉奅婉孌屗嵎幂弥怸愽戲挡"), string_0, string_1);
				text = this.dbprovider_0.ExecuteScalarString(string_2);
				if (text != "" && text != null)
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
				string_2 = string.Format(GClass0.smethod_0("\u0014ăȉ́ЀԖ١ݪࠟॸ੯୳౶ച๪ཌྷ၅ᅀቐፍᑠᕗᙀᝅᡊ᥀ᩎᭉᰋᵽṡὭ⁵Ⅳ∅⍷⑶╰♷❥⡆⥁⩔⭘Ⱖⴽ⹢⼨なㄱ㈵㍕㑝㕖㘱㝃㡊㥟㩘㭉㱅㵉㹌㽗䁎䅂䈥䌹䑸䔳䙼"), string_0, num);
				result = this.GetBySql(string_2);
			}
			return result;
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("TŊɂ͈јՎ؊ݏ࡚ैੋଅ౷ൖ๐བྷ၅ᅦቍ፸ᑭᕮᙿ᝷᡻ᥲᨶ᭢ᱼᵶṠὴ‰⅜≛⍟⑚╎♓❖⡁⥃⨻⬢Ɀⴳ⹿⼦"), string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
