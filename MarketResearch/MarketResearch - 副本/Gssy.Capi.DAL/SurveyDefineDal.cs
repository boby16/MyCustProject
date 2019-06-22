using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyDefineDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬ᭐ᱶᵴṸ὾⁪℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyDefine GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑐᕶᙴ᝸᡾ᥪᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyDefine GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyDefine surveyDefine = new SurveyDefine();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyDefine.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyDefine.ANSWER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]);
					surveyDefine.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					surveyDefine.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					surveyDefine.QUESTION_TITLE = dataReader[GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString();
					surveyDefine.QUESTION_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]);
					surveyDefine.QUESTION_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ")]);
					surveyDefine.ANSWER_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ")]);
					surveyDefine.COMBINE_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ")]);
					surveyDefine.DETAIL_ID = dataReader[GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString();
					surveyDefine.PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
					surveyDefine.SHOW_LOGIC = dataReader[GClass0.smethod_0("YŁɇ͐љՉً݄ࡋू")].ToString();
					surveyDefine.QUESTION_CONTENT = dataReader[GClass0.smethod_0("AŚɋ͞јՂم݇ࡗॄ੉ୋ౐െ์ཕ")].ToString();
					surveyDefine.SPSS_TITLE = dataReader[GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ")].ToString();
					surveyDefine.SPSS_CASE = Convert.ToInt32(dataReader[GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ")]);
					surveyDefine.SPSS_VARIABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]);
					surveyDefine.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader[GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ")]);
					surveyDefine.MIN_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("DŁɉ͙цՋٖ݌ࡕ")]);
					surveyDefine.MAX_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Dŉɟ͙цՋٖ݌ࡕ")]);
					surveyDefine.IS_RANDOM = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ͔фՊهݍࡌ")]);
					surveyDefine.PAGE_COUNT_DOWN = Convert.ToInt32(dataReader[GClass0.smethod_0("_ŏɊ͉єՉنݝࡉ॒ਗ਼ୀౌൕ๏")]);
					surveyDefine.CONTROL_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("OńɄ͝њՈيݚࡐग़੒ୄ")]);
					surveyDefine.CONTROL_FONTSIZE = Convert.ToInt32(dataReader[GClass0.smethod_0("Sŀɀ͙ўՄنݖࡎैੈ୑౗ൊ๘ང")]);
					surveyDefine.CONTROL_HEIGHT = Convert.ToInt32(dataReader[GClass0.smethod_0("Młɂ͟јՆلݘࡎी੍ୄొൕ")]);
					surveyDefine.CONTROL_WIDTH = Convert.ToInt32(dataReader[GClass0.smethod_0("NŃɅ͞ћՇًݙࡒ्ੇୖ౉")]);
					surveyDefine.CONTROL_MASK = dataReader[GClass0.smethod_0("OńɄ͝њՈيݚࡉूੑ୊")].ToString();
					surveyDefine.TITLE_FONTSIZE = Convert.ToInt32(dataReader[GClass0.smethod_0("Zńɘ͇яՖَ݈ࡈ॑੗୊ౘൄ")]);
					surveyDefine.CONTROL_TOOLTIP = dataReader[GClass0.smethod_0("LŁɃ͘љՅمݗࡓॉ੊ୈ౗ോ๑")].ToString();
					surveyDefine.NOTE = dataReader[GClass0.smethod_0("JŌɖ̈́")].ToString();
					surveyDefine.LIMIT_LOGIC = dataReader[GClass0.smethod_0("GŃɄ́ѓՙى݋ࡄोੂ")].ToString();
					surveyDefine.FIX_LOGIC = dataReader[GClass0.smethod_0("OŁɟ͙щՋل݋ࡂ")].ToString();
					surveyDefine.PRESET_LOGIC = dataReader[GClass0.smethod_0("\\řɏ͚эՓٙ݉ࡋॄੋୂ")].ToString();
					surveyDefine.GROUP_LEVEL = dataReader[GClass0.smethod_0("LŘɆ͝їՙى݁ࡕे੍")].ToString();
					surveyDefine.GROUP_CODEA = dataReader[GClass0.smethod_0("LŘɆ͝їՙن݋ࡇेੀ")].ToString();
					surveyDefine.GROUP_CODEB = dataReader[GClass0.smethod_0("LŘɆ͝їՙن݋ࡇे੃")].ToString();
					surveyDefine.GROUP_PAGE_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("HŜɂ͙ћՕٙ݉ࡀृਗ਼୐ౚ൒ไ")]);
					surveyDefine.MT_GROUP_MSG = dataReader[GClass0.smethod_0("Aşɕ͎њՈٓݕ࡛ॎੑ୆")].ToString();
					surveyDefine.MT_GROUP_COUNT = dataReader[GClass0.smethod_0("Cřɓ͌јՆٝݗ࡙ॆੋୖౌൕ")].ToString();
					surveyDefine.IS_ATTACH = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ͇ёՐق݁ࡉ")]);
					surveyDefine.MP3_FILE = dataReader[GClass0.smethod_0("Eŗȵ͚тՊَ݄")].ToString();
					surveyDefine.MP3_START_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("Cŝȿ͔љ՝ىݕࡒग़੐୚౒ൄ")]);
					surveyDefine.SUMMARY_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄")]);
					surveyDefine.SUMMARY_TITLE = dataReader[GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ")].ToString();
					surveyDefine.SUMMARY_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ")]);
					surveyDefine.FILLDATA = dataReader[GClass0.smethod_0("NŎɊ͉рՂٖ݀")].ToString();
					surveyDefine.EXTEND_1 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܰ")].ToString();
					surveyDefine.EXTEND_2 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܳ")].ToString();
					surveyDefine.EXTEND_3 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܲ")].ToString();
					surveyDefine.EXTEND_4 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܵ")].ToString();
					surveyDefine.EXTEND_5 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܴٝ")].ToString();
					surveyDefine.EXTEND_6 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܷٝ")].ToString();
					surveyDefine.EXTEND_7 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܶ")].ToString();
					surveyDefine.EXTEND_8 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܹٝ")].ToString();
					surveyDefine.EXTEND_9 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܸٝ")].ToString();
					surveyDefine.EXTEND_10 = dataReader[GClass0.smethod_0("LŐɓ̓ыՀٜܳ࠱")].ToString();
				}
			}
			return surveyDefine;
		}

		public List<SurveyDefine> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyDefine> list = new List<SurveyDefine>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyDefine
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						ANSWER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]),
						PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						QUESTION_TITLE = dataReader[GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString(),
						QUESTION_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]),
						QUESTION_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ")]),
						ANSWER_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ")]),
						COMBINE_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ")]),
						DETAIL_ID = dataReader[GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString(),
						PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString(),
						SHOW_LOGIC = dataReader[GClass0.smethod_0("YŁɇ͐љՉً݄ࡋू")].ToString(),
						QUESTION_CONTENT = dataReader[GClass0.smethod_0("AŚɋ͞јՂم݇ࡗॄ੉ୋ౐െ์ཕ")].ToString(),
						SPSS_TITLE = dataReader[GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ")].ToString(),
						SPSS_CASE = Convert.ToInt32(dataReader[GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ")]),
						SPSS_VARIABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]),
						SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader[GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ")]),
						MIN_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("DŁɉ͙цՋٖ݌ࡕ")]),
						MAX_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Dŉɟ͙цՋٖ݌ࡕ")]),
						IS_RANDOM = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ͔фՊهݍࡌ")]),
						PAGE_COUNT_DOWN = Convert.ToInt32(dataReader[GClass0.smethod_0("_ŏɊ͉єՉنݝࡉ॒ਗ਼ୀౌൕ๏")]),
						CONTROL_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("OńɄ͝њՈيݚࡐग़੒ୄ")]),
						CONTROL_FONTSIZE = Convert.ToInt32(dataReader[GClass0.smethod_0("Sŀɀ͙ўՄنݖࡎैੈ୑౗ൊ๘ང")]),
						CONTROL_HEIGHT = Convert.ToInt32(dataReader[GClass0.smethod_0("Młɂ͟јՆلݘࡎी੍ୄొൕ")]),
						CONTROL_WIDTH = Convert.ToInt32(dataReader[GClass0.smethod_0("NŃɅ͞ћՇًݙࡒ्ੇୖ౉")]),
						CONTROL_MASK = dataReader[GClass0.smethod_0("OńɄ͝њՈيݚࡉूੑ୊")].ToString(),
						TITLE_FONTSIZE = Convert.ToInt32(dataReader[GClass0.smethod_0("Zńɘ͇яՖَ݈ࡈ॑੗୊ౘൄ")]),
						CONTROL_TOOLTIP = dataReader[GClass0.smethod_0("LŁɃ͘љՅمݗࡓॉ੊ୈ౗ോ๑")].ToString(),
						NOTE = dataReader[GClass0.smethod_0("JŌɖ̈́")].ToString(),
						LIMIT_LOGIC = dataReader[GClass0.smethod_0("GŃɄ́ѓՙى݋ࡄोੂ")].ToString(),
						FIX_LOGIC = dataReader[GClass0.smethod_0("OŁɟ͙щՋل݋ࡂ")].ToString(),
						PRESET_LOGIC = dataReader[GClass0.smethod_0("\\řɏ͚эՓٙ݉ࡋॄੋୂ")].ToString(),
						GROUP_LEVEL = dataReader[GClass0.smethod_0("LŘɆ͝їՙى݁ࡕे੍")].ToString(),
						GROUP_CODEA = dataReader[GClass0.smethod_0("LŘɆ͝їՙن݋ࡇेੀ")].ToString(),
						GROUP_CODEB = dataReader[GClass0.smethod_0("LŘɆ͝їՙن݋ࡇे੃")].ToString(),
						GROUP_PAGE_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("HŜɂ͙ћՕٙ݉ࡀृਗ਼୐ౚ൒ไ")]),
						MT_GROUP_MSG = dataReader[GClass0.smethod_0("Aşɕ͎њՈٓݕ࡛ॎੑ୆")].ToString(),
						MT_GROUP_COUNT = dataReader[GClass0.smethod_0("Cřɓ͌јՆٝݗ࡙ॆੋୖౌൕ")].ToString(),
						IS_ATTACH = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ͇ёՐق݁ࡉ")]),
						MP3_FILE = dataReader[GClass0.smethod_0("Eŗȵ͚тՊَ݄")].ToString(),
						MP3_START_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("Cŝȿ͔љ՝ىݕࡒग़੐୚౒ൄ")]),
						SUMMARY_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄")]),
						SUMMARY_TITLE = dataReader[GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ")].ToString(),
						SUMMARY_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ")]),
						FILLDATA = dataReader[GClass0.smethod_0("NŎɊ͉рՂٖ݀")].ToString(),
						EXTEND_1 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܰ")].ToString(),
						EXTEND_2 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܳ")].ToString(),
						EXTEND_3 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܲ")].ToString(),
						EXTEND_4 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܵ")].ToString(),
						EXTEND_5 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܴٝ")].ToString(),
						EXTEND_6 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܷٝ")].ToString(),
						EXTEND_7 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܶ")].ToString(),
						EXTEND_8 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܹٝ")].ToString(),
						EXTEND_9 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܸٝ")].ToString(),
						EXTEND_10 = dataReader[GClass0.smethod_0("LŐɓ̓ыՀٜܳ࠱")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyDefine> GetList()
		{
			string string_ = GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑖᕴᙶᝦᡠᥨᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyDefine surveyDefine_0)
		{
			string string_ = string.Format(GClass0.smethod_0("¦ƠʾΩҹ־ۉޡࢩলપ௄ರ඗ຓྖႺᆧኙᎹᒽᖳᚷួ᣿ᦗ᪛ᮇᲄᶗẃᾏ₀↜⊉⎉⒙◦⚙➉⢀⦃⪚⮍ⲇⷮ⺐⾕ヺ㇭㋩㏵㓴㗴㛦㟶㣶㧻㫰㮘㳢㷧㻴㿣䃻䇧䋢䏢䓴䗾䛠䟼䣫䧣䪉䯵䳶䷧仲俴僖凑勓參哏嗃囉埝墻姇嫀寑峀巆廘忟惁懑拘揟擎斦曈柆棔槑櫀毖泜淗滒濅灓焽爲猱琹申瘷眽砨礿稻笰簶紪繝缴耪脺般茥萧蔵蘠蜬衋褶訤謶谦贬踵輿逜鄑鈙錙鑷锉阑霗頀餉騙鬛鰔鴛鸒齼ꀞꄛꈈꌟꐟꔃꘆ꜆ꠘ꤅ꨊꬊ갗괇긏꼔뀓녭뉭덯둨땥뙭띱롣륺며묘뱠뵢빢뽣쁰셭쉬썿쑮씆왺인존쥵쩺쭲챢쵰침콡큝텒퉘팰푈핊홊흋\ud848\ud946\uda47\udb5d\udc5d\udd46\ude4e\udf54隸瑱ﯘﲺﶡﺮﾢ®ƠʩΣҦ׆ڹީࢠণ઺஧ಬභຯྴႀᆚኒᎋᒕᗶᚚភᢙᦂ᪇ᮛᲟᶍẅᾉ₟↋⋡⎏⒄▄⚝➚⢈⦊⪚⮂Ⲍⶌ⺕⾓ヶ㇤㋸㎐㓸㗵㛷㟬㣥㧹㫹㯫㳻㷷㻸㿷䃧䇺䊁䏯䓤䗤䛽䟺䣨䧪䫺䯳䳪䷦仵俨傳凝勒叒哏嗈囖埔壈姛嫔寇峘嶾廅忙惛懂拈揓操旅曇柜棔槏櫟毁沯淁滎濎瀫焬爲猰琤甮瘶眷砻礢稼笤籟紼績缤耪腂舡茥萦蔣蘽蜷蠫褩訢謭谠赎踧輩逇鄁鈑錓鐜锓阚靴頇餄騐鬇鰖鴆鸎鼜ꀀꄉꈄꌏꑧꔍꘛ꜇ꠒꤖꨚ꬈갆괔긄꼌뀓녹뉯덳둮땪뙦띻롸륲며뭵밟뵵빣뽿쁺셾쉲썯쑤앮왬읪젋쥡쩷쭫챶쵲칾콰큞텙퉘퍃푏핃홉흝\ud83b\ud95b\uda41\udb4b\udc54\udd40\ude5e\udf45廉瑱﮵ﲰﶺﻝﾽ¿ǝʲΪҢ֦ڬ߄ࢪশ૖஻ರබຠྲႋᆁ኉ᎅᒋᖟᛵឋᢂᦛ᪘ᮕᲁᶋẎᾅₜ↋⋡⎟⒞▇⚄➉⢕⦟⪚⮐Ⲋⶖ⺍⾅ん㇭㋨㏱㓶㗻㛫㟡㣨㧿㫻㯰㳶㷪㺝㿶䃦䇢䋡䏨䓪䗾䛨䞄䣢䧾䫱䯡䳭䷦仾侑傳凛勅又哞嗔囝埇墥妺嫐富峇巗廟忔惐憽抡揉擓旞曌柆棃槙檱殨泆淚滕濅瀱焺爢獉瑗甿瘡眬砲礸稱笫籅絞縴缨耻脫舣茨萴蕝虅蜭蠿褲訠謪谧贽蹙轌通鄆鈉錙鐕锞阆靡须餓騍鬀鰖鴜鸕鼏ꁾꅾꉤꍬꐝꔋꘅꜝꠂꤕꩭꬿ걳괿깭꽧끄넏뉀댛됗딝뙂뜊롊뤑먙묓뱈봁빌뼗쀃셕숙썑쐇앑옜읕젋쥝쨓쭙찏쵙츖콝퀳턹퉦팤푦픽혵휿\ud86c\ud92f\uda68\udb33\udc3f\udd35\ude6a\udf21蓼愈ﯘﲈﷃﻄﾍÃƕ˜ϚҖ׆ڒߙ࣐ছૉட೒ේຜ࿌ႤᇯዤᎡᓷᖡ᛫៨ᢪ᧺᪮᳢᯦ᶯỽᾫ⃽⇼⊰⏠⒰◸⛺➵⣫⧡⪾⯶⳷⶿⻦⿬ツㆌ㊈㏁㒗㖝㛂㞊㢁㧋㪒㮘㲔㷉㺃㾇䃒䆉䊁䎋䓐䖘䚑䟕䢀䦊䪂䯟䲑䶛仜侇傳冹勦厯咫嗧嚾垴墰姭媦宥峮嶵庽德惴憽抿揱撬斦暮柳梴榵櫸殣沯淹溲澴瀂煒牚猇瑈畏瘄睟硛祑税筇籅紏繖罜耔腝艚茑葇蕍蘒蝛衟褛詂譈谘赑蹘輝遳鄥鉩鍬鐦镶陾霣顣饧騨魳鱿鴩鹥齢ꀲꅢꉪꌷꑿꕹ꘴ꝯꡫꥡ꨾ꭰ걷괿깦꽬뀘녅눉댉둆딝똕뜟롌뤂먃뭉바봞븖뽋쀛섙쉐쌋쐇씍왒윜젟쥛쨂쬈찄쵙츕켙큢턹툱팻푠픯혩흥\ud830\ud93a\uda32\udb6f\udc26\udd23\ude6c\udf37"), new object[]
			{
				surveyDefine_0.ANSWER_ORDER,
				surveyDefine_0.PAGE_ID,
				surveyDefine_0.QUESTION_NAME,
				surveyDefine_0.QUESTION_TITLE,
				surveyDefine_0.QUESTION_TYPE,
				surveyDefine_0.QUESTION_USE,
				surveyDefine_0.ANSWER_USE,
				surveyDefine_0.COMBINE_INDEX,
				surveyDefine_0.DETAIL_ID,
				surveyDefine_0.PARENT_CODE,
				surveyDefine_0.SHOW_LOGIC,
				surveyDefine_0.QUESTION_CONTENT,
				surveyDefine_0.SPSS_TITLE,
				surveyDefine_0.SPSS_CASE,
				surveyDefine_0.SPSS_VARIABLE,
				surveyDefine_0.SPSS_PRINT_DECIMAIL,
				surveyDefine_0.MIN_COUNT,
				surveyDefine_0.MAX_COUNT,
				surveyDefine_0.IS_RANDOM,
				surveyDefine_0.PAGE_COUNT_DOWN,
				surveyDefine_0.CONTROL_TYPE,
				surveyDefine_0.CONTROL_FONTSIZE,
				surveyDefine_0.CONTROL_HEIGHT,
				surveyDefine_0.CONTROL_WIDTH,
				surveyDefine_0.CONTROL_MASK,
				surveyDefine_0.TITLE_FONTSIZE,
				surveyDefine_0.CONTROL_TOOLTIP,
				surveyDefine_0.NOTE,
				surveyDefine_0.LIMIT_LOGIC,
				surveyDefine_0.FIX_LOGIC,
				surveyDefine_0.PRESET_LOGIC,
				surveyDefine_0.GROUP_LEVEL,
				surveyDefine_0.GROUP_CODEA,
				surveyDefine_0.GROUP_CODEB,
				surveyDefine_0.GROUP_PAGE_TYPE,
				surveyDefine_0.MT_GROUP_MSG,
				surveyDefine_0.MT_GROUP_COUNT,
				surveyDefine_0.IS_ATTACH,
				surveyDefine_0.MP3_FILE,
				surveyDefine_0.MP3_START_TYPE,
				surveyDefine_0.SUMMARY_USE,
				surveyDefine_0.SUMMARY_TITLE,
				surveyDefine_0.SUMMARY_INDEX,
				surveyDefine_0.FILLDATA,
				surveyDefine_0.EXTEND_1,
				surveyDefine_0.EXTEND_2,
				surveyDefine_0.EXTEND_3,
				surveyDefine_0.EXTEND_4,
				surveyDefine_0.EXTEND_5,
				surveyDefine_0.EXTEND_6,
				surveyDefine_0.EXTEND_7,
				surveyDefine_0.EXTEND_8,
				surveyDefine_0.EXTEND_9,
				surveyDefine_0.EXTEND_10
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyDefine surveyDefine_0)
		{
			string string_ = string.Format(GClass0.smethod_0("4İț̟Љԙٻ܉ࠬपਡଳబഐึ༴းᄾሪ፮ᐞᔉᘟᝪ᠈ᤆᨔᬑᰀᴖḜἍ–℄≺⍬␝━☛❁⠈⥅⨛⭦ⱴ⵳⹶⽭へㅴ㈏㌓㐍㔋㙐㜘㡔㤏㨋㭷㱰㵡㹰㽶䁨䅯䉑䍁䑓䕝䙖䝟䠹䤥䨷䬱䱮䴧乮伵倽允剚卋呞啘噂坅塇套婓孏屑嵈幆弢怼愠拘掅擉斁曜柖梨榭檲殥没涽溼澼炮熤状玾璨痌盖矊碒秝窚篊粴綱约羱肵膩芐莐蒂薉蚈螟裹觥諷训賣趩軿输邟醃銘鎋钟間隞鞙颌駨髺鯦鲾鷳麾鿮ꂂꆏꋲꏼꓴꗲ꛾꟥꣰꧶ꫳ꯳곭궔꺎꾒냊놈닒뎂듩뗩뛿럫룠매뫸믯볡분뺞뾂삆쇛슦쏣쒺얰웋쟛죋짝쫙쯂쳊췗컜쿖탔톰튲펮풪헷횺ힺ\ud8f4\ud9af\udaab\udbd5\udccd\uddcb\uded4\udfdd蠟﨩גּﰠﴪ︷ｂ\\ŀɸ̥Ѭծئݽࡵऋਇଅఆഋง༛စᄜሊ፮ᑰᕬᙬᜱᡸ᥻ᨺ᭡ᱩᴗḓἑ‒℟≼⍿⑮╹☛✇⠙⥃⨆⬂ⱈⴘ⹠⽢ぢㅣ㉰㍸㑬㕾㙢㝫㡫㥤㩢㬆㰘㴄㹘㼓䀔䅝䈳䍍䑍䕏䙈䝅䡉䥊䩞䭘䱁䵋乗佗偒兙剂协呄啀嘫圷堩女娶嬰屸崨幎彋恏慟押掱撨斲暯柚棄様檌毇泂涉滟澿炰熨犰玭璢疹皥瞾磉秕竇箝糔緜纞翎肨膳芀莌蒜薒蚟螕袔觸諪诶貮跥軪辯都醀銎鎉针間隈鞅颜馆骓鮙鲁鶋麔龌ꃡꇽꊟꏅ꒏ꖌꛆꞖ꣺꧷꫹ꯢ곧귻껿꿭냥뇩닿돫뒍떑뚋럑뢛릙뫚뮊볦뷫뻭뿶샳쇯싓쏁쓛엓웕쟎죊집쫍쯓첵충캳쿩킣톢틲펢퓎헃훅ퟞ\ud8db\ud9c7\udacb\udbd9\udccd\uddc1\udeca\udfc5賂靖ﭔﱑﴙ﹏Ａ.Įȋ̌ВԐ؄ܗ࠘ऋਜ୶౨൴๴༩ၣᅥሲ፩ᑡᔘᘂ᜞᠅ᤍᨘᬀᰊᴊḗἑ\u2008ℚ≺⌞␀├♀✈⠏⥅⨛⭵ⱺ⵺⹧⽠まㅼ㉰㍺㑢㕣㙧㝾㡠㥸㨇㬛㰅㴃㹘㼐䀖䅝䈸䌲䑓䕓䙏䝟䠹䤥䨷䬱䱮䴦丫佯倶儼剃升呀啅噟坕塅奇婀孏屆崤帾弢怦慻拍揇撀旛曗枼械榠檨殺沺涳溺澱烑燍狏珉璖痟盛瞗磎秄窷箴粠綷约羶肾膬芐莙蒔薟蛻蟧裹觿説该賤趩軴迾邖醂銀鎛钝間隇鞏颟馍骋鯦鳸鷤黤龹ꃲꇲꋂꎙ꒑ꗻꛩꟵ꣬ꧨꫨ꯵곺귰껶꿳낑농늏뎉듖떟뚘럗뢎름뫠믴볪뷱뻳뿽샢쇯싛쏛쓟얼욦잺좾짣쪤쮢쳨춳캿쿕탃퇟틚폞퓒헜훊ퟍ\ud8cc\ud9d7\udad3\udbdf\udcd5\uddc1\udea3\udfbf洛懲תּﰨﴰ︼Ｅ3įȊ̎Ђԟؔ܏ࠗऌ੷୫౵൳ศཡၦᄭቨ።ᐄᔟᘔᜋ᠝ᤜᨆᬅᰍᵤṾὢ›ⅳ∇⍃␑╱♫✉⡦⥾⩾⭺Ɒⴔ⸎⼒〖ㅋ㈜㌗㑐㔋㘇㝧㡹㤛㩸㭵㱱㵥㹱㽶䁾䅴䉆䍎䑘䔼䘦䜺䡢䤬䨧䭫䰹䵇乆佟停兑剝南呒啙噘坏堩夵娧孽就崵幾弮恒慕抲掳撼斮暢枥梭榱檣殺沰淔滎濒烖熋狛珜璐痋盇瞹碼禥窪箧粷綽纼羫肯膤芚莆蓽藡蛻螡裭觫說诺貓趝躟辞邕醑銛鎏铭闱雫韭颲駼髳鮻鳢鷨麆龚ꂕꆅꋱꏺꓢꖍꚛꞇꢙꦟ꫌ꮂ검귉꺔꾞냴뇨닻돫듣뗨뛴래뢉릕몇뮁볞붐뺕뿟삆소싚쏆쓉엙웕쟞죆즫쪷쮫첵춳컨쾦킦퇭튨펢퓈헔훟ퟏ\ud8c7\ud9cc\udad8\udbb2\udca5\uddb9\udea3\udfa5擄勉ﬣﰫﴠ︼ｔAŝɿ͹Цթ٫ܧࡾॴ਒଎ఁ഑ฝ༖ဎᅧቯ፳ᑭᕫᘰ᝿ᡸᤵ᩠᭪ᰀᴜḗἇ‏℄≠⌆␝━☛✝⡂⤍⨅⭋Ⱂⴘ⹶⽪づㅵ㉡㍪㑲㔕㘋㜗㠉㤏㩜㬓㰖㵙㸄㼎䁤䅸䉋䍛䑓䕘䙄䜫䠩䤸䨪䬶䰲䵯並伦偬儷刯卙呅啉噙坏堩奁婃嬦尸崤幸弲恼"), new object[]
			{
				surveyDefine_0.ID,
				surveyDefine_0.ANSWER_ORDER,
				surveyDefine_0.PAGE_ID,
				surveyDefine_0.QUESTION_NAME,
				surveyDefine_0.QUESTION_TITLE,
				surveyDefine_0.QUESTION_TYPE,
				surveyDefine_0.QUESTION_USE,
				surveyDefine_0.ANSWER_USE,
				surveyDefine_0.COMBINE_INDEX,
				surveyDefine_0.DETAIL_ID,
				surveyDefine_0.PARENT_CODE,
				surveyDefine_0.SHOW_LOGIC,
				surveyDefine_0.QUESTION_CONTENT,
				surveyDefine_0.SPSS_TITLE,
				surveyDefine_0.SPSS_CASE,
				surveyDefine_0.SPSS_VARIABLE,
				surveyDefine_0.SPSS_PRINT_DECIMAIL,
				surveyDefine_0.MIN_COUNT,
				surveyDefine_0.MAX_COUNT,
				surveyDefine_0.IS_RANDOM,
				surveyDefine_0.PAGE_COUNT_DOWN,
				surveyDefine_0.CONTROL_TYPE,
				surveyDefine_0.CONTROL_FONTSIZE,
				surveyDefine_0.CONTROL_HEIGHT,
				surveyDefine_0.CONTROL_WIDTH,
				surveyDefine_0.CONTROL_MASK,
				surveyDefine_0.TITLE_FONTSIZE,
				surveyDefine_0.CONTROL_TOOLTIP,
				surveyDefine_0.NOTE,
				surveyDefine_0.LIMIT_LOGIC,
				surveyDefine_0.FIX_LOGIC,
				surveyDefine_0.PRESET_LOGIC,
				surveyDefine_0.GROUP_LEVEL,
				surveyDefine_0.GROUP_CODEA,
				surveyDefine_0.GROUP_CODEB,
				surveyDefine_0.GROUP_PAGE_TYPE,
				surveyDefine_0.MT_GROUP_MSG,
				surveyDefine_0.MT_GROUP_COUNT,
				surveyDefine_0.IS_ATTACH,
				surveyDefine_0.MP3_FILE,
				surveyDefine_0.MP3_START_TYPE,
				surveyDefine_0.SUMMARY_USE,
				surveyDefine_0.SUMMARY_TITLE,
				surveyDefine_0.SUMMARY_INDEX,
				surveyDefine_0.FILLDATA,
				surveyDefine_0.EXTEND_1,
				surveyDefine_0.EXTEND_2,
				surveyDefine_0.EXTEND_3,
				surveyDefine_0.EXTEND_4,
				surveyDefine_0.EXTEND_5,
				surveyDefine_0.EXTEND_6,
				surveyDefine_0.EXTEND_7,
				surveyDefine_0.EXTEND_8,
				surveyDefine_0.EXTEND_9,
				surveyDefine_0.EXTEND_10
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyDefine surveyDefine_0)
		{
			string string_ = string.Format(GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬቐ፶ᑴᕸᙾᝪᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyDefine_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾቂ፠ᑢᕪᙬᝤ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 55;
			string[] array = new string[55];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = GClass0.smethod_0("颞矫趗勹鱸宎");
				array[2] = GClass0.smethod_0("顶縔凶");
				array[3] = GClass0.smethod_0("闪馛純僶");
				array[4] = GClass0.smethod_0("丿馛骚嵳");
				array[5] = GClass0.smethod_0("丿馛骚咊");
				array[6] = GClass0.smethod_0("颐矩搩圣䬞趨婗縻");
				array[7] = GClass0.smethod_0("筜楏搩圣䬞趨巾囻");
				array[8] = GClass0.smethod_0("绌唏骞疁彔鶛笠堔");
				array[9] = GClass0.smethod_0("逎揯驼剷著稔縀");
				array[10] = GClass0.smethod_0("夐羮鈁懮鱿眳坷蝗眔焀");
				array[11] = GClass0.smethod_0("送饾幓稿鱾完斥唷");
				array[12] = GClass0.smethod_0("剫馛骚嵳");
				array[13] = GClass0.smethod_0("TŖɖ͗У鶚塳");
				array[14] = GClass0.smethod_0("TŖɖ͗У鶚冊");
				array[15] = GClass0.smethod_0("ZŘɔ͕Х囜韌筹徊");
				array[16] = GClass0.smethod_0("[ŗɕ͖Ф夌捲䡌");
				array[17] = GClass0.smethod_0("夓鄁骟疂挅夋阊旫浱");
				array[18] = GClass0.smethod_0("夓鄁骟疂挅尣阊旫浱");
				array[19] = GClass0.smethod_0("冏釡鈁魾戩儣邋怹殐嬖");
				array[20] = GClass0.smethod_0("顲誧柳化慳Ԣ翓");
				array[21] = GClass0.smethod_0("掠俰璁罿厈暥吷");
				array[22] = GClass0.smethod_0("掠俰璁塓䭐尥娎");
				array[23] = GClass0.smethod_0("探俲璇駚媧");
				array[24] = GClass0.smethod_0("探俲璇墿媧");
				array[25] = GClass0.smethod_0("掠俰ȥ͉Ѣձ٪");
				array[26] = GClass0.smethod_0("闧馐樀鮞犁幓䥐帥后");
				array[27] = GClass0.smethod_0("掭俿Ȩ͓ѩժ٨ݗ࡫ॱ");
				array[28] = GClass0.smethod_0("夊淤觿攄Щԧا愸焿醜粇媇垸");
				array[29] = GClass0.smethod_0("退饱祜錏犁锿覒撥娷");
				array[30] = GClass0.smethod_0("図媒鈎墜犁锿覒撥娷");
				array[31] = GClass0.smethod_0("颍偀鈎墜犁锿覒撥娷");
				array[32] = GClass0.smethod_0("徬犪骜緇窥圪");
				array[33] = GClass0.smethod_0("徦犤骒緍瘾媭疩䧦瀅ूਢ坃");
				array[34] = GClass0.smethod_0("徦犤骒緍瘾媭疩䧦瀅ुਢ坃");
				array[35] = GClass0.smethod_0("闣馔唣岠瞦鶐磃䤫纁䙉畭睹宊");
				array[36] = GClass0.smethod_0("徭犩骝緀鲛䫣普");
				array[37] = GClass0.smethod_0("徢犨骞緁釪鶛捲雎");
				array[38] = GClass0.smethod_0("春唯淈渼啵蕑遀䧵媝觼");
				array[39] = GClass0.smethod_0("闧馐却荒犁鶜塱塗鿲");
				array[40] = GClass0.smethod_0("频彺嵒铵犁儫咫洣圎");
				array[41] = GClass0.smethod_0("昫唥晚誀");
				array[42] = GClass0.smethod_0("摜袂樅鮙");
				array[43] = GClass0.smethod_0("摜袂缠尔");
				array[44] = GClass0.smethod_0("颁奯卆晲杯");
				array[45] = GClass0.smethod_0("扯嵐厁墺Т԰");
				array[46] = GClass0.smethod_0("扯嵐厁墺ТԳ");
				array[47] = GClass0.smethod_0("扯嵐厁墺ТԲ");
				array[48] = GClass0.smethod_0("扯嵐厁墺ТԵ");
				array[49] = GClass0.smethod_0("扯嵐厁墺ТԴ");
				array[50] = GClass0.smethod_0("扯嵐厁墺ТԷ");
				array[51] = GClass0.smethod_0("扯嵐厁墺ТԶ");
				array[52] = GClass0.smethod_0("扯嵐厁墺ТԹ");
				array[53] = GClass0.smethod_0("扯嵐厁墺ТԸ");
				array[54] = GClass0.smethod_0("扮嵓厀墽УԳر");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓");
				array[2] = GClass0.smethod_0("WŇɂ́ќՋم");
				array[3] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[4] = GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ");
				array[5] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ");
				array[6] = GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ");
				array[7] = GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ");
				array[8] = GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ");
				array[9] = GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ");
				array[10] = GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
				array[11] = GClass0.smethod_0("YŁɇ͐љՉً݄ࡋू");
				array[12] = GClass0.smethod_0("AŚɋ͞јՂم݇ࡗॄ੉ୋ౐െ์ཕ");
				array[13] = GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ");
				array[14] = GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ");
				array[15] = GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ");
				array[16] = GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ");
				array[17] = GClass0.smethod_0("DŁɉ͙цՋٖ݌ࡕ");
				array[18] = GClass0.smethod_0("Dŉɟ͙цՋٖ݌ࡕ");
				array[19] = GClass0.smethod_0("@śɘ͔фՊهݍࡌ");
				array[20] = GClass0.smethod_0("_ŏɊ͉єՉنݝࡉ॒ਗ਼ୀౌൕ๏");
				array[21] = GClass0.smethod_0("OńɄ͝њՈيݚࡐग़੒ୄ");
				array[22] = GClass0.smethod_0("Sŀɀ͙ўՄنݖࡎैੈ୑౗ൊ๘ང");
				array[23] = GClass0.smethod_0("Młɂ͟јՆلݘࡎी੍ୄొൕ");
				array[24] = GClass0.smethod_0("NŃɅ͞ћՇًݙࡒ्ੇୖ౉");
				array[25] = GClass0.smethod_0("OńɄ͝њՈيݚࡉूੑ୊");
				array[26] = GClass0.smethod_0("Zńɘ͇яՖَ݈ࡈ॑੗୊ౘൄ");
				array[27] = GClass0.smethod_0("LŁɃ͘љՅمݗࡓॉ੊ୈ౗ോ๑");
				array[28] = GClass0.smethod_0("JŌɖ̈́");
				array[29] = GClass0.smethod_0("GŃɄ́ѓՙى݋ࡄोੂ");
				array[30] = GClass0.smethod_0("OŁɟ͙щՋل݋ࡂ");
				array[31] = GClass0.smethod_0("\\řɏ͚эՓٙ݉ࡋॄੋୂ");
				array[32] = GClass0.smethod_0("LŘɆ͝їՙى݁ࡕे੍");
				array[33] = GClass0.smethod_0("LŘɆ͝їՙن݋ࡇेੀ");
				array[34] = GClass0.smethod_0("LŘɆ͝їՙن݋ࡇे੃");
				array[35] = GClass0.smethod_0("HŜɂ͙ћՕٙ݉ࡀृਗ਼୐ౚ൒ไ");
				array[36] = GClass0.smethod_0("Aşɕ͎њՈٓݕ࡛ॎੑ୆");
				array[37] = GClass0.smethod_0("Cřɓ͌јՆٝݗ࡙ॆੋୖౌൕ");
				array[38] = GClass0.smethod_0("@śɘ͇ёՐق݁ࡉ");
				array[39] = GClass0.smethod_0("Eŗȵ͚тՊَ݄");
				array[40] = GClass0.smethod_0("Cŝȿ͔љ՝ىݕࡒग़੐୚౒ൄ");
				array[41] = GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄");
				array[42] = GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ");
				array[43] = GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ");
				array[44] = GClass0.smethod_0("NŎɊ͉рՂٖ݀");
				array[45] = GClass0.smethod_0("Mşɒ̀ъՇٝܰ");
				array[46] = GClass0.smethod_0("Mşɒ̀ъՇٝܳ");
				array[47] = GClass0.smethod_0("Mşɒ̀ъՇٝܲ");
				array[48] = GClass0.smethod_0("Mşɒ̀ъՇٝܵ");
				array[49] = GClass0.smethod_0("Mşɒ̀ъՇܴٝ");
				array[50] = GClass0.smethod_0("Mşɒ̀ъՇܷٝ");
				array[51] = GClass0.smethod_0("Mşɒ̀ъՇٝܶ");
				array[52] = GClass0.smethod_0("Mşɒ̀ъՇܹٝ");
				array[53] = GClass0.smethod_0("Mşɒ̀ъՇܸٝ");
				array[54] = GClass0.smethod_0("LŐɓ̓ыՀٜܳ࠱");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyDefine> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyDefine surveyDefine in list_0)
			{
				array[num, 0] = surveyDefine.ID.ToString();
				array[num, 1] = surveyDefine.ANSWER_ORDER.ToString();
				array[num, 2] = surveyDefine.PAGE_ID;
				array[num, 3] = surveyDefine.QUESTION_NAME;
				array[num, 4] = surveyDefine.QUESTION_TITLE;
				array[num, 5] = surveyDefine.QUESTION_TYPE.ToString();
				array[num, 6] = surveyDefine.QUESTION_USE.ToString();
				array[num, 7] = surveyDefine.ANSWER_USE.ToString();
				array[num, 8] = surveyDefine.COMBINE_INDEX.ToString();
				array[num, 9] = surveyDefine.DETAIL_ID;
				array[num, 10] = surveyDefine.PARENT_CODE;
				array[num, 11] = surveyDefine.SHOW_LOGIC;
				array[num, 12] = surveyDefine.QUESTION_CONTENT;
				array[num, 13] = surveyDefine.SPSS_TITLE;
				array[num, 14] = surveyDefine.SPSS_CASE.ToString();
				array[num, 15] = surveyDefine.SPSS_VARIABLE.ToString();
				array[num, 16] = surveyDefine.SPSS_PRINT_DECIMAIL.ToString();
				array[num, 17] = surveyDefine.MIN_COUNT.ToString();
				array[num, 18] = surveyDefine.MAX_COUNT.ToString();
				array[num, 19] = surveyDefine.IS_RANDOM.ToString();
				array[num, 20] = surveyDefine.PAGE_COUNT_DOWN.ToString();
				array[num, 21] = surveyDefine.CONTROL_TYPE.ToString();
				array[num, 22] = surveyDefine.CONTROL_FONTSIZE.ToString();
				array[num, 23] = surveyDefine.CONTROL_HEIGHT.ToString();
				array[num, 24] = surveyDefine.CONTROL_WIDTH.ToString();
				array[num, 25] = surveyDefine.CONTROL_MASK;
				array[num, 26] = surveyDefine.TITLE_FONTSIZE.ToString();
				array[num, 27] = surveyDefine.CONTROL_TOOLTIP;
				array[num, 28] = surveyDefine.NOTE;
				array[num, 29] = surveyDefine.LIMIT_LOGIC;
				array[num, 30] = surveyDefine.FIX_LOGIC;
				array[num, 31] = surveyDefine.PRESET_LOGIC;
				array[num, 32] = surveyDefine.GROUP_LEVEL;
				array[num, 33] = surveyDefine.GROUP_CODEA;
				array[num, 34] = surveyDefine.GROUP_CODEB;
				array[num, 35] = surveyDefine.GROUP_PAGE_TYPE.ToString();
				array[num, 36] = surveyDefine.MT_GROUP_MSG;
				array[num, 37] = surveyDefine.MT_GROUP_COUNT;
				array[num, 38] = surveyDefine.IS_ATTACH.ToString();
				array[num, 39] = surveyDefine.MP3_FILE;
				array[num, 40] = surveyDefine.MP3_START_TYPE.ToString();
				array[num, 41] = surveyDefine.SUMMARY_USE.ToString();
				array[num, 42] = surveyDefine.SUMMARY_TITLE;
				array[num, 43] = surveyDefine.SUMMARY_INDEX.ToString();
				array[num, 44] = surveyDefine.FILLDATA;
				array[num, 45] = surveyDefine.EXTEND_1;
				array[num, 46] = surveyDefine.EXTEND_2;
				array[num, 47] = surveyDefine.EXTEND_3;
				array[num, 48] = surveyDefine.EXTEND_4;
				array[num, 49] = surveyDefine.EXTEND_5;
				array[num, 50] = surveyDefine.EXTEND_6;
				array[num, 51] = surveyDefine.EXTEND_7;
				array[num, 52] = surveyDefine.EXTEND_8;
				array[num, 53] = surveyDefine.EXTEND_9;
				array[num, 54] = surveyDefine.EXTEND_10;
				num++;
			}
			return array;
		}

		public string GetQuestionTitleByName(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("2ĥɓ͛ўՈ؛ݫࡌढ़੄ୂ౜൛๝཭ၥᅙቛፂᑈᔌᙍ᝘ᡆ᥅ᨇ᭵᱐ᵖṕ὇⁘Ⅴ≺⍸⑴╲♾✺⡮⥰⩲⭤Ɒⴴ⹂⽧ぴㅣ㉻㍧㑢㕢㙔㝄㡨㥥㩢㬻㰢㵿㸳㽿䀦"), string_0);
			return this.dbprovider_0.ExecuteScalarString(string_);
		}

		public SurveyDefine GetByName(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("GŖɞ͔ѓ՛؎܇ࠌ्੘୆౅ഇ๵ཐၖᅕቇፘᑤᕺᙸ᝴ᡲ᥾ᨺ᭮ᱰᵲṤὰ‴⅂≇⍔⑃╛♇❂⡂⥔⩄⭈ⱅⵂ⸻⼢みㄳ㉿㌦"), string_0);
			return this.GetBySql(string_);
		}

		public string GetChildByIndex(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\"ĵȣ̫ЮԸ٫ܛ࠼भ਴ଲబഫอ༝ုᄡቒ፛ᐝᕚᙉ᝕ᡔᤘᩤᭃ᱇ᵂṖὋ⁵⅕≉⍇⑃╉☋❝⡁⥍⩕⭃Ⰵ⵴⹂⽅いㅿ㉶㍺㐠㔻㙠㜪㡤㤿㨷㭷㱻㵰㸳㽱䁾䅽䉭䍧䑣䕩䙔䝣䡧䥬䩢䭾䰥䴹乸伳偼"), string_0, int_0);
			return this.dbprovider_0.ExecuteScalarString(string_);
		}

		public SurveyDefine GetByPageId(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("1ĤȬ͚ѝՉ؜ܑࠚय़੊୘౛ക๧ཆ၀ᅇቕፖᑪᕈᙊᝂᡄ᥌ᨈ᭐ᱎᵀṖ὆\u2002ⅱ≁⍸⑻╂♵❿⠧⤾⩣⬧Ⱬⴲ⸴⽒ぜㅕ㈰㍌㑁㕀㙎㝂㡄㥌㩗㭎㱈㵁㹁㽛䀾䄰"), string_0);
			return this.GetBySql(string_);
		}

		public SurveyDefine GetByPageId(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("6ġȯ̧ТԴ؟ܔࠝग़੉୕౔ഘ๤གྷ၇ᅂቖፋᑵᕕᙉᝇᡃ᥉ᨋ᭝᱁ᵍṕὃ\u2005ⅴ≂⍅⑄╿♶❺⠠⤻⩠⬪Ɽⴿ⸷⽷ほㅰ㈳㍱㑾㕽㙭㝧㡣㥩㩔㭣㱧㵬㹢㽾䀥䄹䉸䌳䑼"), string_0, int_0);
			return this.GetBySql(string_);
		}

		public List<SurveyDefine> GetListByPageId(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("7ĦȮ̤УՋ؞ܗࠜढ़ੈୖౕഗ๥ཀ၆ᅅ቗ፈᑴᕊᙈᝄᡂ᥎ᨊ᭞᱀ᵂṔὀ\u2004ⅳ≃⍆⑅╀♷❹⠡⤼⩡⬩ⱥⴰ⹹⽧ばㅶ㉠㌱㑲㕶㘮㝎㡃㥆㩈㭀㱆㵂㹙㽌䁊䅇䉇䍙"), string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyDefine> GetQuotaConfig()
		{
			string string_ = GClass0.smethod_0("\u0012ąȳ̻оԨٻݰࡹाਥହస൴฀༧ဣᄦሪጷᐉᔩᘭᜣᠧ᤭ᩧᬱᰭᴡḱἧ⁡ℐ≞⍙⑘╣♒❞⠄⤟⩤⭯ⱦ⵫⹢⽧まㅤ㉮㌉㐍㕍㙅㝎㠉㥹㩲㭣㱶㵰㹪㽭䁯䅿䉋䍇䑍䕙䘥䜫䠩䤨䨷䭹䱧䵰乶你倱兲剶匮呎啃噆坈塀奆婂孙屌嵊幇彇恙");
			return this.GetListBySql(string_);
		}

		public List<SurveyDefine> GetQCAdvanceConfig(int int_0)
		{
			string arg = "";
			if (int_0 != 101)
			{
				if (int_0 != 102)
				{
					arg = GClass0.smethod_0("{żɭʹѲլ٫ݭࡽॵ੹୏౛ഽม༻ါᄩሪጷᑹᕧᘴᝂᡇᥔᩃ᭛᱇ᵂṂὔ⁞⅐≘⍂␦┸☤✲⠲⤰");
				}
				else
				{
					arg = GClass0.smethod_0("BŇɔ̓ћՇق݂ࡔफ़੐୘ూദุ༤ဲᄲሳ");
				}
			}
			else
			{
				arg = GClass0.smethod_0("BŇɔ̓ћՇق݂ࡔफ़੐୘ూദุ༤ဲᄲሰ");
			}
			string string_ = string.Format(GClass0.smethod_0("\u0019ČȄ̂Ѕԑل݉ࡂइ਒ରళൽฏ༮ဨᄯሽጮᐒᔰᘲ᜺ᠼᤴᩰᬸᰦᴨḾἮ⁪ℙ∩⌠␣┚☭✧⡿⥦⨓⭦Ɑⵢ⹭⽮ふㅭ㉹㌐㐖㕔㙚㝗㠒㤙㨐㭔㰞㵐㸌㼂䀊䅈䉆䍃䐆䕤䙪䝰䡵䥤䩲䭀䱋䵎乙伻倧儹利匷呹啧噰坶塠失婲孶尮嵎幃彆恈慀扆捂摙敌晊杇桇楙"), arg);
			return this.GetListBySql(string_);
		}

		public List<SurveyDefine> GetRecodeConfig(int int_0)
		{
			string string_ = "";
			if (int_0 != 1)
			{
				if (int_0 != 2)
				{
					string_ = GClass0.smethod_0("ýǨˠϮө׽ڨޭࢦৣ૶௬೯ඡ໓༊ဌᄋሙጂᐾᔜᘞ᜞᠘ᤐᩔᬄᰚᴔḂἊ⁎ⅅ∽⌾␯┺☼✮⠩⤫⨻⬷ⰻⴱ⸥⽢どㅽ㈳㌩㑺㔈㘍㜒㠅㤁㨝㬜㰜㴎㸄㼖䀞䄈䉱䍳䑪䔦䘺䝧䠗䤐䨁䬐䰖䴈丏佱偡兩剥卫呿唄嘁圞堖奔婚字尒嵖幂彀恛慝扳捇摏敟晍杋栆椘樃欄氂浀湎潻瀾煾牳獶瑸異癶睲硉祼空筷籷絩縰缲耮脽般荤葸蕭虭蝵蠦襧詽謣豫赥");
				}
				else
				{
					string_ = GClass0.smethod_0("¤ƳʹαҰ֦۱ޱ࣡৤૭பಹඥ຤࿨႔ᆳ኷Ꮂᒦᖻᚅឥᣙ᧗᫓ᯙᲛᷛẙ῏⃟⇓⋇⏑⒓◓⚟⟠⣮⧩⫨⯳Ⳣⷮ⺉⿁ドㆆ㊍㏷㓦㗮㛤㟣㣋㦾㫍㯝㳜㷟㻆㿑䃓䆶䋓䏆䓜䗟䚱䟃䣚䧜䫛䯉䳒䷆仆俏僎凅劥右哫嗧図埥塟夒娒嬛尒崙带弌怎愆成捉摔攠昴朳栠椪樨欳氧津渮漡瀤煁牅猅琍甆癁眩砑礐稘笎簄紓縗缜耒脎艵荪葳蕠虡蝠衿襮訊謞谄负踙轨逅鄟鉥錔鐂锅阄霟顶饺騔鬜鱚鵔鹝鼘ꁖꄘꉒꍆꑜꕇꙁꝯꡃꥋ꩛ꭉ걇괊긔꼏뀀넆뉄덊둇딂뙀뜎롼륱며뭾뱲뵴빼뽇쁾셸쉱썱쑫씲올윰젿줮쩢쭾챯쵯칻켨큥텿툥퍥퐭핫홥");
				}
			}
			else
			{
				string_ = GClass0.smethod_0("Öǁˏχӂהڿ޴ࢽ৺૩௵೴මໄ࿣ყᇢዶᏫᓕᗵᛩ៧ᣣ᧩᪫᯽᳡ᷭỵΰ₥↬⋒⏗Ⓞ◓☫✷⠲⤲⨤⬮Ⱐ⴨⸲⽋あㅔ㈜㌀㑑㔡㘺㜫㠾㤸㨢㬥㰧㴷㸳㼿䀵䄡䉞䍚䑁䔏䘭䝾䠌䤉䨞䬉䰍䴑丘优倊儀刊匂吔啭噶坧塭夭娥嬮屩崯帵弩怰愴戜挮搤收晚杒栝椁樜欝氙浙湙潒瀕煗牜獟瑓留癁睋硲祅穅筎籌結縇缛者脔舃荃葏蕄蘿蝝衒襒詏譈豖赔蹈轛達酇鉘録鐬锰阨霿頪餬驤魸鱭鵭鹵鼦ꁧꅽꈣꍫꑥ");
			}
			return this.GetListBySql(string_);
		}

		public List<SurveyDefine> GetListTTS()
		{
			string string_ = GClass0.smethod_0("µƠʨΦҡֵ۠ޕ࢞৛૎௔೗඙໫࿂ჄᇃዑᏊᓶᗔᛖំᣀᧈ᪌ᯜ᳂᷌Ớῂ₆⇴⋱⏦⓱◵⛩⟐⣐⧂⫉⯈ⳟⶹ⺥⾷ェㆵ㋕㏝㓖㖱㛓㟀㣃㧏㫅㯅㳏㷖㻁㿉䃂䇀䋜䎣䒿䖱䚠䝟䠿䤳䨸䭛䱒䵙丩伢倳儦删区吽唿嘯圻堷夽娩孋屖嵐幘彇性愫戠捃搳攴春朌栊椔樓欕氅洍渁漇瀓煩牪獤瑲畱瘑省砊祭穬笚簟紌縛缓耏脊舊茜萖蔘蘐蝺蠂褃訄講谚赸蹶轳逖酤鉡鍶鑡镥陹靠顠饲驸魲鱺鵬鸔鼙ꀟꄅꉥꍭꑦꔁꘀꝎꡋ꥘꩏ꭏ걓굖깖꽈끂녌뉄덖됮딯똡뜿렮뤤먬뭤뱸뵭빭뽵쀦셧쉽쌣쑫앥");
			return this.GetListBySql(string_);
		}

		public bool SyncReadToWrite()
		{
			bool result = true;
			try
			{
				List<SurveyDefine> list = new List<SurveyDefine>();
				string string_ = GClass0.smethod_0("UŀɈ͆сՕ؀ܵ࠾ॻ੮୴౷ഹ๋རၤᅣቱ፪ᑖᕴᙶᝦᡠᥨᨬ᭤ᱸᵭṭή…Ⅷ≽⌣⑫╥");
				list = this.GetListBySql(string_);
				string_ = GClass0.smethod_0("|ŲɺͰѠնزݷࡢॠ੣ଭ౟ൾ๸ཿၭᅾቂ፠ᑢᕪᙬᝤ");
				this.dbprovider_1.ExecuteNonQuery(string_);
				foreach (SurveyDefine surveyDefine_ in list)
				{
					this.AddToWrite(surveyDefine_);
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public void AddToWrite(SurveyDefine surveyDefine_0)
		{
			string string_ = string.Format(GClass0.smethod_0("¦ƠʾΩҹ־ۉޡࢩলપ௄ರ඗ຓྖႺᆧኙᎹᒽᖳᚷួ᣿ᦗ᪛ᮇᲄᶗẃᾏ₀↜⊉⎉⒙◦⚙➉⢀⦃⪚⮍ⲇⷮ⺐⾕ヺ㇭㋩㏵㓴㗴㛦㟶㣶㧻㫰㮘㳢㷧㻴㿣䃻䇧䋢䏢䓴䗾䛠䟼䣫䧣䪉䯵䳶䷧仲俴僖凑勓參哏嗃囉埝墻姇嫀寑峀巆廘忟惁懑拘揟擎斦曈柆棔槑櫀毖泜淗滒濅灓焽爲猱琹申瘷眽砨礿稻笰簶紪繝缴耪脺般茥萧蔵蘠蜬衋褶訤謶谦贬踵輿逜鄑鈙錙鑷锉阑霗頀餉騙鬛鰔鴛鸒齼ꀞꄛꈈꌟꐟꔃꘆ꜆ꠘ꤅ꨊꬊ갗괇긏꼔뀓녭뉭덯둨땥뙭띱롣륺며묘뱠뵢빢뽣쁰셭쉬썿쑮씆왺인존쥵쩺쭲챢쵰침콡큝텒퉘팰푈핊홊흋\ud848\ud946\uda47\udb5d\udc5d\udd46\ude4e\udf54隸瑱ﯘﲺﶡﺮﾢ®ƠʩΣҦ׆ڹީࢠণ઺஧ಬභຯྴႀᆚኒᎋᒕᗶᚚភᢙᦂ᪇ᮛᲟᶍẅᾉ₟↋⋡⎏⒄▄⚝➚⢈⦊⪚⮂Ⲍⶌ⺕⾓ヶ㇤㋸㎐㓸㗵㛷㟬㣥㧹㫹㯫㳻㷷㻸㿷䃧䇺䊁䏯䓤䗤䛽䟺䣨䧪䫺䯳䳪䷦仵俨傳凝勒叒哏嗈囖埔壈姛嫔寇峘嶾廅忙惛懂拈揓操旅曇柜棔槏櫟毁沯淁滎濎瀫焬爲猰琤甮瘶眷砻礢稼笤籟紼績缤耪腂舡茥萦蔣蘽蜷蠫褩訢謭谠赎踧輩逇鄁鈑錓鐜锓阚靴頇餄騐鬇鰖鴆鸎鼜ꀀꄉꈄꌏꑧꔍꘛ꜇ꠒꤖꨚ꬈갆괔긄꼌뀓녹뉯덳둮땪뙦띻롸륲며뭵밟뵵빣뽿쁺셾쉲썯쑤앮왬읪젋쥡쩷쭫챶쵲칾콰큞텙퉘퍃푏핃홉흝\ud83b\ud95b\uda41\udb4b\udc54\udd40\ude5e\udf45廉瑱﮵ﲰﶺﻝﾽ¿ǝʲΪҢ֦ڬ߄ࢪশ૖஻ರබຠྲႋᆁ኉ᎅᒋᖟᛵឋᢂᦛ᪘ᮕᲁᶋẎᾅₜ↋⋡⎟⒞▇⚄➉⢕⦟⪚⮐Ⲋⶖ⺍⾅ん㇭㋨㏱㓶㗻㛫㟡㣨㧿㫻㯰㳶㷪㺝㿶䃦䇢䋡䏨䓪䗾䛨䞄䣢䧾䫱䯡䳭䷦仾侑傳凛勅又哞嗔囝埇墥妺嫐富峇巗廟忔惐憽抡揉擓旞曌柆棃槙檱殨泆淚滕濅瀱焺爢獉瑗甿瘡眬砲礸稱笫籅絞縴缨耻脫舣茨萴蕝虅蜭蠿褲訠謪谧贽蹙轌通鄆鈉錙鐕锞阆靡须餓騍鬀鰖鴜鸕鼏ꁾꅾꉤꍬꐝꔋꘅꜝꠂꤕꩭꬿ걳괿깭꽧끄넏뉀댛됗딝뙂뜊롊뤑먙묓뱈봁빌뼗쀃셕숙썑쐇앑옜읕젋쥝쨓쭙찏쵙츖콝퀳턹퉦팤푦픽혵휿\ud86c\ud92f\uda68\udb33\udc3f\udd35\ude6a\udf21蓼愈ﯘﲈﷃﻄﾍÃƕ˜ϚҖ׆ڒߙ࣐ছૉட೒ේຜ࿌ႤᇯዤᎡᓷᖡ᛫៨ᢪ᧺᪮᳢᯦ᶯỽᾫ⃽⇼⊰⏠⒰◸⛺➵⣫⧡⪾⯶⳷⶿⻦⿬ツㆌ㊈㏁㒗㖝㛂㞊㢁㧋㪒㮘㲔㷉㺃㾇䃒䆉䊁䎋䓐䖘䚑䟕䢀䦊䪂䯟䲑䶛仜侇傳冹勦厯咫嗧嚾垴墰姭媦宥峮嶵庽德惴憽抿揱撬斦暮柳梴榵櫸殣沯淹溲澴瀂煒牚猇瑈畏瘄睟硛祑税筇籅紏繖罜耔腝艚茑葇蕍蘒蝛衟褛詂譈谘赑蹘輝遳鄥鉩鍬鐦镶陾霣顣饧騨魳鱿鴩鹥齢ꀲꅢꉪꌷꑿꕹ꘴ꝯꡫꥡ꨾ꭰ걷괿깦꽬뀘녅눉댉둆딝똕뜟롌뤂먃뭉바봞븖뽋쀛섙쉐쌋쐇씍왒윜젟쥛쨂쬈찄쵙츕켙큢턹툱팻푠픯혩흥\ud830\ud93a\uda32\udb6f\udc26\udd23\ude6c\udf37"), new object[]
			{
				surveyDefine_0.ANSWER_ORDER,
				surveyDefine_0.PAGE_ID,
				surveyDefine_0.QUESTION_NAME,
				surveyDefine_0.QUESTION_TITLE,
				surveyDefine_0.QUESTION_TYPE,
				surveyDefine_0.QUESTION_USE,
				surveyDefine_0.ANSWER_USE,
				surveyDefine_0.COMBINE_INDEX,
				surveyDefine_0.DETAIL_ID,
				surveyDefine_0.PARENT_CODE,
				surveyDefine_0.SHOW_LOGIC,
				surveyDefine_0.QUESTION_CONTENT,
				surveyDefine_0.SPSS_TITLE,
				surveyDefine_0.SPSS_CASE,
				surveyDefine_0.SPSS_VARIABLE,
				surveyDefine_0.SPSS_PRINT_DECIMAIL,
				surveyDefine_0.MIN_COUNT,
				surveyDefine_0.MAX_COUNT,
				surveyDefine_0.IS_RANDOM,
				surveyDefine_0.PAGE_COUNT_DOWN,
				surveyDefine_0.CONTROL_TYPE,
				surveyDefine_0.CONTROL_FONTSIZE,
				surveyDefine_0.CONTROL_HEIGHT,
				surveyDefine_0.CONTROL_WIDTH,
				surveyDefine_0.CONTROL_MASK,
				surveyDefine_0.TITLE_FONTSIZE,
				surveyDefine_0.CONTROL_TOOLTIP,
				surveyDefine_0.NOTE,
				surveyDefine_0.LIMIT_LOGIC,
				surveyDefine_0.FIX_LOGIC,
				surveyDefine_0.PRESET_LOGIC,
				surveyDefine_0.GROUP_LEVEL,
				surveyDefine_0.GROUP_CODEA,
				surveyDefine_0.GROUP_CODEB,
				surveyDefine_0.GROUP_PAGE_TYPE,
				surveyDefine_0.MT_GROUP_MSG,
				surveyDefine_0.MT_GROUP_COUNT,
				surveyDefine_0.IS_ATTACH,
				surveyDefine_0.MP3_FILE,
				surveyDefine_0.MP3_START_TYPE,
				surveyDefine_0.SUMMARY_USE,
				surveyDefine_0.SUMMARY_TITLE,
				surveyDefine_0.SUMMARY_INDEX,
				surveyDefine_0.FILLDATA,
				surveyDefine_0.EXTEND_1,
				surveyDefine_0.EXTEND_2,
				surveyDefine_0.EXTEND_3,
				surveyDefine_0.EXTEND_4,
				surveyDefine_0.EXTEND_5,
				surveyDefine_0.EXTEND_6,
				surveyDefine_0.EXTEND_7,
				surveyDefine_0.EXTEND_8,
				surveyDefine_0.EXTEND_9,
				surveyDefine_0.EXTEND_10
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
