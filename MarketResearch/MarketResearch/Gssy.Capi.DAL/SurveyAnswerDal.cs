using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyAnswerDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬ᭕ᱽᵡṦή⁽℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyAnswer GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑕᕽᙡᝦᡵ᥽ᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyAnswer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyAnswer.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyAnswer.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyAnswer.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					surveyAnswer.CODE = dataReader["CODE"].ToString();
					surveyAnswer.MULTI_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("FşɅ͜юՙيݖࡇे੓")]);
					surveyAnswer.MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("FŅɍ́с՟ٚ݀ࡂॖ੄")].ToString()));
					surveyAnswer.SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]);
					surveyAnswer.SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
					surveyAnswer.BEGIN_DATE = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("HŌɏ͎ш՚ـ݂ࡖॄ")].ToString()));
					surveyAnswer.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
				}
			}
			return surveyAnswer;
		}

		public List<SurveyAnswer> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyAnswer
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						CODE = dataReader["CODE"].ToString(),
						MULTI_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("FşɅ͜юՙيݖࡇे੓")]),
						MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("FŅɍ́с՟ٚ݀ࡂॖ੄")].ToString())),
						SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]),
						SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString(),
						BEGIN_DATE = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("HŌɏ͎ш՚ـ݂ࡖॄ")].ToString())),
						PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyAnswer> GetList()
		{
			string string_ = GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑓᕿᙣ᝸ᡫ᥿ᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		public void Delete(SurveyAnswer surveyAnswer_0)
		{
			string string_ = string.Format(GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬቕ፽ᑡᕦᙵ᝽ᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyAnswer_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾቇ፫ᑷᕴᙧᝳ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 10;
			string[] array = new string[10];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = "问卷编号";
				array[2] = GClass0.smethod_0("闪馛純僶");
				array[3] = GClass0.smethod_0("筐楋純笀");
				array[4] = GClass0.smethod_0("筓楎鈌懭犇鵸墎");
				array[5] = GClass0.smethod_0("俪携柴雵");
				array[6] = GClass0.smethod_0("闫剳岌儕埶");
				array[7] = GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
				array[8] = GClass0.smethod_0("近偬韦鮟鱳鉧处廈淴鳵");
				array[9] = GClass0.smethod_0("顶縔凶");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = "SURVEY_ID";
				array[2] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[3] = "CODE";
				array[4] = GClass0.smethod_0("FşɅ͜юՙيݖࡇे੓");
				array[5] = GClass0.smethod_0("FŅɍ́с՟ٚ݀ࡂॖ੄");
				array[6] = GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅");
				array[7] = GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
				array[8] = GClass0.smethod_0("HŌɏ͎ш՚ـ݂ࡖॄ");
				array[9] = GClass0.smethod_0("WŇɂ́ќՋم");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyAnswer> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyAnswer surveyAnswer in list_0)
			{
				array[num, 0] = surveyAnswer.ID.ToString();
				array[num, 1] = surveyAnswer.SURVEY_ID;
				array[num, 2] = surveyAnswer.QUESTION_NAME;
				array[num, 3] = surveyAnswer.CODE;
				array[num, 4] = surveyAnswer.MULTI_ORDER.ToString();
				array[num, 5] = surveyAnswer.MODIFY_DATE.ToString();
				array[num, 6] = surveyAnswer.SEQUENCE_ID.ToString();
				array[num, 7] = surveyAnswer.SURVEY_GUID;
				array[num, 8] = surveyAnswer.BEGIN_DATE.ToString();
				array[num, 9] = surveyAnswer.PAGE_ID;
				num++;
			}
			return array;
		}

		public bool Exists(string string_0, string string_1, string string_2 = "")
		{
			string string_3;
			if (string_2 == "")
			{
				string_3 = string.Format(GClass0.smethod_0("<īȡ̩ШԾ٩ܫࠨळਫର౫൨๨འၙᅌቒፑᐛᕩᙌᝊᡁᥓᩌ᭵ᱝᵁṆὕ⁝ℎ≚⍄⑎╘♌✈⡴⥳⩷⭲ⱦ⵻⹾⽩せㄣ㈺㍧㐫㕧㘾㜸㡶㥸㩱㬴㱂㵇㹔㽃䁛䅇䉂䍂䑔䕄䙈䝅䡂䤻䨢䭿䰲䵿並"), string_0, string_1);
			}
			else
			{
				string_3 = string.Format(GClass0.smethod_0("+ĲȺ̰зԧٲܲ࠿ऺਠହ౤ൡ๣ཀྵီᄵሩጨᑤᔐᘷᜳᠶᥚᩇ᭼᱒ᵈṍ὜⁊℗≁⍝⑑╁♗✑⡣⥺⩼⭻Ⱪ⵲⹵⽠ぬㄚ㈁㍞㐔㕞㘅㜁㡁㥱㩺㬽㱍㵎㹟㽊䁌䅞䉙䍛䑋䕝䙓䝜䡕䤯䩂䭄䱇䵎个伮偳儶剻卾吶啾嘧圦"), string_0, string_1, string_2.Trim());
			}
			int num = this.dbprovider_0.ExecuteScalarInt(string_3);
			return num > 0;
		}

		public void AddByModel(SurveyAnswer surveyAnswer_0)
		{
			string string_ = string.Format(GClass0.smethod_0("úǼˢϵӽ׺ڍߥࣥ৾૦ஈ೴ී໗࿒჆ᇛዠᏎᓬᗩᛸ៮ᢳᧉᫌᯊ᳁ᷓỌΉ⃚⇖⊽⏁ⓚ○⛞⟘⣂⧅⫇⯗ⳉ⷇⻈⿁ク㇁㋎㏄㐺㕒㘰㜩㠷㤮㨰㬧㰸㴤㸱㼱䀡䅞䈼䌿䐫䔧䘫䜵䠴䤮䨨䬼䰢䵊丶伡倲儷判匮吜唛嘂圕堟奶娛嬝尐崟帛弋怗愓戅挕摣攝昘朞栝椏樐欗氀洓渌漀灯焒爀猇瑺畡癴睸砒礚穯筹类絣繰罧耛脕艊茀葒蔉蘁蜋衐褛詔謏谋贁蹞輖遞鄅鈍鍛鐬镣阱霻顠餮驤鬿鰻鵭鸠齩ꀿꄵꉪꌦꑲꔩ꘡ꜫꡰꤽꩴ꬯갫괡깾꼼끾넥눨"), new object[]
			{
				surveyAnswer_0.SURVEY_ID,
				surveyAnswer_0.QUESTION_NAME,
				surveyAnswer_0.CODE,
				surveyAnswer_0.MULTI_ORDER,
				surveyAnswer_0.MODIFY_DATE,
				surveyAnswer_0.SEQUENCE_ID,
				surveyAnswer_0.BEGIN_DATE,
				surveyAnswer_0.SURVEY_GUID,
				surveyAnswer_0.PAGE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Add(SurveyAnswer surveyAnswer_0)
		{
			surveyAnswer_0.MODIFY_DATE = new DateTime?(DateTime.Now);
			if (surveyAnswer_0.BEGIN_DATE == null)
			{
				surveyAnswer_0.BEGIN_DATE = new DateTime?(DateTime.Now);
			}
			string string_ = string.Format(GClass0.smethod_0("èǮˌϛӏ׈ڻߓࣗৌ૘ஶೆ෡໡࿤ჴᇩዎᏠᓾᗻᛮ៸ᢡ᧛᫒ᯔ᳓᷁Ớ῝⃈⇄≓⌯␨┹☨✮⠰⤷⨹⬩ⰻⴵ⸾⼷そㄳ㈠㌪㐨㕀㘦㜿㠥㤼㨮㬹㰪㴶㸧㼧䀳䅌䈒䌑䐙䔕䘝䜃䠆䤜䨖䬂䰐䵸一众倀儅刊匀后唉嘔圃堍奤娅嬃専崍帍弝怅愁扫捻搑敬智杽桼楧橾歲氜洔湥潳災煥牪獽琅甋癐眚硔礏程笁籞紕繞缅耍脇艤茬葠蔻蘷蝡蠪襥註謱豮贠蹮輵逽酫鈺鍳鐡锫陰霼顴餯騫鬡鱾鴳鹾鼥ꀨ"), new object[]
			{
				surveyAnswer_0.SURVEY_ID,
				surveyAnswer_0.QUESTION_NAME,
				surveyAnswer_0.CODE,
				surveyAnswer_0.MULTI_ORDER,
				surveyAnswer_0.MODIFY_DATE,
				surveyAnswer_0.SEQUENCE_ID,
				surveyAnswer_0.BEGIN_DATE,
				surveyAnswer_0.PAGE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyAnswer surveyAnswer_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u0010ĖȄ̓ЇԀٳܛࠟऄ਀୮ఞഹู༼ာᄱሆጨᐶᔳᘦᜰ᠉ᤉᩬᬞᰕᴜṨὯ⁫Ⅾ≲⍯⑪╽♷✞⠑⥡⩺⭫Ȿ⵸⹢⽥でㅷ㉩㍧㑨㕡㘏㜂㡢㥯㩛㭛㰱㴼㹖㽏䁕䅌䉞䍉䑚䕆䙗䝗䡃䤼䨯䭃䱂䵈乂佌偐兗剃升呑啁嘯圢塒奅媮宫岸嶲庸徿悦憱抳揚擕斧暦枠梧榵檶殱沪涹溢澮烅燈犨玶璺疰皺瞲碤禿窖箚糱綞纞羝肐膖芈莒蒔薀蚖蟾裱覀誎讉貈趓躂辎郠釨鋧鎕钀閈隆鞁颕駠髞鮐鳮鷩黩鿬ꃼꇡꋨꏿꓱꖘꚓꟓꢟꧡ꫺꯫곾그껢꿥냧뇷닩돧듨뗡뚏랂룀릎뫜믑볙뷙뺷뾺샸솶싚쏃쓙엀웚쟍죞짂쫋쯋쳟춠캫쾭탲톺틺펡풩햤훢힬\ud8d2\ud9c5\uda2e\udb2b\udc38\udd32\ude38\udf3f濫祥﬙ﰔﴒ︄＞\u0018ČȒͺѵԵٽ܂ࠐगਊ଑ఄഈ๫ཪဏᄚለጋᑥᔗᘶᜰᠷᤥᩆ᭿᱓ᵏṌὟ⁋℘≖⌖⑂╜♖❀⡔⤐⩎⬀Ȿ⵹⹹⽼ぬㅱ㉸㍯㑡㔄㘞㜂㠆㥛㨯㭣㰺㴼㹚㽔䁝䄸䉶䌸䑄䕁䙖䝁䡅䥙䩀䭀䱒䵂乊佇偌儨刺匦吢啿嘲坿堦"), surveyAnswer_0.SURVEY_ID, surveyAnswer_0.QUESTION_NAME, DateTime.Now);
			this.dbprovider_0.ExecuteNonQuery(string_);
			string_ = string.Format(GClass0.smethod_0("äǠ˫ϯӹשڋ߹ࣜ৚૑௃೜෥ໍ࿑ზᇅይᎾᓎᗙᛏឺᣚ᧗᫓ᯓᲵᶩẳ᾵⃪↢⋲⎩⒡◁⛞⟆⣝⧁⫘⯉ⳗⷀ⻆⿐ァㆽ㉟㌅㑎㔁㙗㜩㠼㤩㨢㬳㰻㴷㸶㼭䀸䄴䉏䍓䑍䔗䙟䜗䡅䤥䨨䬢䰬䴢为伽倥儡刋匛命啡噻坽堢奯娪孱屹崖帖引怘愞成挊搌攘明杪桴楨橠欽汰洹湤潮瀑焁牸獻瑢畵癿眚砄礘稐筍簃絉縔缒聦腸艪荼葨蔌虸蝿衻襾詢譿豺赭蹧輂逜鄀鈸鍥鐭镡阼霺願饖驓鬶鱄鵁鹖齁ꁅꅙꉀꍀꑒꕂꙊꝇꡌꤨ꨺ꬦ갢굿긲꽿뀦"), new object[]
			{
				surveyAnswer_0.SURVEY_ID,
				surveyAnswer_0.QUESTION_NAME,
				surveyAnswer_0.CODE,
				surveyAnswer_0.MULTI_ORDER,
				surveyAnswer_0.SEQUENCE_ID,
				surveyAnswer_0.BEGIN_DATE,
				surveyAnswer_0.PAGE_ID,
				DateTime.Now
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void AddOne(string string_0, string string_1, string string_2, int int_0)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.SURVEY_ID = string_0;
			surveyAnswer.SEQUENCE_ID = int_0;
			surveyAnswer.QUESTION_NAME = string_1;
			surveyAnswer.CODE = string_2;
			surveyAnswer.MULTI_ORDER = 0;
			surveyAnswer.BEGIN_DATE = new DateTime?(DateTime.Now);
			if (this.Exists(surveyAnswer.SURVEY_ID, surveyAnswer.QUESTION_NAME, ""))
			{
				this.Update(surveyAnswer);
			}
			else
			{
				this.Add(surveyAnswer);
			}
		}

		public void AddOneNoUpdate(string string_0, string string_1, string string_2, int int_0)
		{
			this.Add(new SurveyAnswer
			{
				SURVEY_ID = string_0,
				SEQUENCE_ID = int_0,
				QUESTION_NAME = string_1,
				CODE = string_2,
				MULTI_ORDER = 0,
				BEGIN_DATE = new DateTime?(DateTime.Now)
			});
		}

		public void AddList(string string_0, int int_0, List<SurveyAnswer> list_0)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			SurveyAnswer surveyAnswer2 = new SurveyAnswer();
			surveyAnswer2.SURVEY_ID = string_0;
			surveyAnswer2.SEQUENCE_ID = int_0;
			for (int i = 0; i < list_0.Count; i++)
			{
				surveyAnswer = list_0[i];
				surveyAnswer2.QUESTION_NAME = surveyAnswer.QUESTION_NAME;
				surveyAnswer2.CODE = surveyAnswer.CODE;
				surveyAnswer2.MULTI_ORDER = surveyAnswer.MULTI_ORDER;
				surveyAnswer2.BEGIN_DATE = surveyAnswer.BEGIN_DATE;
				this.Add(surveyAnswer2);
			}
		}

		public void SaveOneAnswer(SurveyAnswer surveyAnswer_0)
		{
			if (this.Exists(surveyAnswer_0.SURVEY_ID, surveyAnswer_0.QUESTION_NAME, ""))
			{
				this.Update(surveyAnswer_0);
			}
			else
			{
				this.Add(surveyAnswer_0);
			}
		}

		public void UpdateList(string string_0, int int_0, List<SurveyAnswer> list_0)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.SURVEY_ID = string_0;
			surveyAnswer.SEQUENCE_ID = int_0;
			foreach (SurveyAnswer surveyAnswer2 in list_0)
			{
				surveyAnswer.QUESTION_NAME = surveyAnswer2.QUESTION_NAME;
				surveyAnswer.CODE = surveyAnswer2.CODE;
				surveyAnswer.MULTI_ORDER = surveyAnswer2.MULTI_ORDER;
				surveyAnswer.BEGIN_DATE = surveyAnswer2.BEGIN_DATE;
				if (this.Exists(string_0, surveyAnswer.QUESTION_NAME, ""))
				{
					this.Update(surveyAnswer);
				}
				else
				{
					this.Add(surveyAnswer);
				}
			}
		}

		public void ClearBySequenceId(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u001dĝȁ̔Ђԛٮ܄ࠂटਅ୩ఛലิ༳အᄺሃጯᐳᕈᙛᝏᡴᥲᩩᬙᰐᴗṥὠ⁦Ⅵ≷⍨⑯╦♪✁⠌⥺⩿⭬ⱻ⵳⹯⽪なㅼ㉬㍠㑭㕚㘲㜽㡟㥔㩞㭜㰴㴷㹛㽀䁘䅇䉛䍎䑟䕝䙊䝈䡞䤧䨪䭄䱇䵃乏佃偝兜剆區呔喺囒埝墯妾媫宬岽嶹庵徰悫憺抶揝擐於暻枿梺榮檳殶沯液溯澡烈燃犭玱璿疋皇瞍碙禄窓箝糴綕纓羒肝膝芍莕蒑薛蚋蟡袜覊認讌貗趎躂迬郤釣銑鎄钌闺雽韩颜駚骔鯪鳭鷥黠鿰ꃭꇬꋻꏵ꒜ꖏꛏꞃꣽꧾꫯ꯺과귮껩꿫냻뇭닣돬듥떳뚾럼뢲맘뫕믝볝붻뺶뿴삺쇞싇쏝쓄없웑쟂죞짏쫏쯛첤춧캡쿾킶퇾튥펭풠픞홐휮\ud839\ud92a\uda2f\udb3c\udc36\udd34\ude33\udf2a爛精﬐ﰖﴈ︒４\0Ėɾͱбա؞܌ࠋऎਕ଀ఌ൧๦༃ဖᄌሏ፡ᐓᕊᙌᝋᡙ᥂᩻᭗᱋ᵀṓ὇—⅒−⍦⑸╪♼❨⠌⥊⨄⭺ⱽ⵵⹰⽠ぽㅼ㉫㍥㐀㔢㘾㜺㡧㤫㩧㬾㰸㵖㹘㽑䀴䅲䈼䍂䑕䕞䙛䝈䡂䥈䩏䭖䱁䵃並伸値典刳卼"), string_0, int_0, DateTime.Now);
			this.dbprovider_0.ExecuteNonQuery(string_);
			string_ = string.Format(GClass0.smethod_0("!ģȶ̰ФԪَܾ࠙ङਜଌ఑ദจ༖ဓᄆሐፁᐳᔚᘊ᝽᠟ᤔ᨞ᬜᱸᵪṶὲ⁳ⅿ∟⌄␜┛☇✒⠃⤙⨎⬌Ⱊⵧ⹻⽥ぴㅯ㉢㌒㐅㕮㙫㝸㡲㥸㩿㭦㱱㵳㸖㼈䀍䄊䈋䌈䐉䔖䘎䝺䡤䥮䩸䭬䰈䵴乳佷偲兦剻卾呩啛嘾圠堼夼婡嬩履崰帶彔恚慗戲捂摕敞晛杈桂楈橏歖汁浃渦漸瀤煸爳獼"), string_0, int_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void ClearMultiple(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("îǨ˶ϡӱ׶ځߩ࣑৊૒஼ೈ෯໫࿮ჲᇯዔᏺᓠᗥᛴ២ᣇᧇ᫞ᮬᲣᶪỚ῝⃕⇐⋀⏝ⓜ○⛅➬⡟⤯⨨⬹Ⱘ⴮⸰⼷〹ㄩ㈻㌵㐾㔷㙝㝐㠬㤡㨩㬩㱇㵊㸤㼽䀫䄲䈬䌻䐬䔰䘥䜥䠍䥲䩽䬑䰔䴞丐伞倎儉刑匕吇唗噽坰堜夋娜嬙導崄帊弍怘意戁捨摣攑昔朒桩楻橤正汼浯湰潼瀛焖牺獤瑬畦癨睠硪祱穤筨簇紊繫罭聠腯艫荻葧蕣虵蝥蠳褾詍譝豜赟蹆轑道鄿鈵錴鑀镗陝靕題饚騭魭鰥鵙鹜齚ꁑꅃꉜꍛꑊꕆ꘭꜠ꢞ꧐ꪬꮩ겾궩꺭꾱낸놸늪뎺뒲떿뚴럜룏릏뫃뮯벤붮뺬뿄샇솇싋쎩쒶얮욵잩좀즑쪏쮘첞춈컵쿸탰톭틧펩퓴헾훱ힱ\ud8e1\ud99d\uda88\udb9d\udc9e\udd8f\ude87\udf8b呂﫯ﯩﳬ﷣ﻧ￷ãǧ˱ϡҏ׃ڏ߰ࣞ৙૘௃೒ෞູྸბᇄዚᏙᒳᗁᛤ២᣹᧫᫴ᯍ᳥᷹Ỿ῭⃵↦⋤⎤ⓔ◊⛄⟒⠺⥞⨜⭒Ⱘ⴯⸫⼮〲ㄯ㈪㌽㐷㕏㙖㜋㡟㤓㩊㭌㰪㴤㸭㽈䁏䅆䈄䍊䐲䔷䘤䜳䠋䤗䨒䬒䰄䴔丘伕倒其则匝吘唗噱坷場奿娰嬓尊嵯幮彨怈愔扥挥摭攓昔朅桬楪橴歳汵浥湷潹灺煳爕獸瑺畹癴眐砈祕稜筑籴絥繽罠耀脆艪荶萃蕃蘏蝱衊襛詎譈豒赕蹗轇遙酗鉘鍑鐳镞陘靛顊餮騪魷鰺鵷鹖齇ꁓꅎꉚꍇꐦꔥ꘨"), string_0, string_1, DateTime.Now);
			this.dbprovider_0.ExecuteNonQuery(string_2);
			string_2 = string.Format(GClass0.smethod_0("\u0097ƑʄϾӪ׸ڜߨ࣏ো૎௒೏෴໚࿀Ⴥᇔዂᎏᓽᗨᛸឋᣩ᧦᫬ᯢᲆᶘẄᾄ₅↍⋭⏊ⓒ◉⛕⟄⣕⧋⫝̸⯒Ⳅⶵ⺩⾳アㆽ㊰㏜㓋㗜㛙㟎㣄㧊㫍㯘㳏㷁㺤㾾䂻䆸䊹䍆䑇䕅䙜䜬䠲䤼䨪䬲䱖䴦両伡値儴利匰吧唩噑坌堑奙娕孀屆崤帪弧恂慉所挎搋攘昏朏栓椖樖欈氘洔渙漖灲焝爙猄琋畭癫眰硻礴稗笆籣絢繤缌耐腡舑荪葻蕮虨蝲衵襷詧譹豷赸蹱輓遾酸鉻鍪鐎锊陗霚顗饶驧魳鱮鴂鸄齬ꁰꄁꉱꍊꑛꕎꙈꝒꡕ꥗ꩇꭙ걗굘깑꼳끞녘뉛덊됮딪뙷뜺롷륖멇뭓뱎뵚빇뼦쀥섨"), string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public void ClearSingle(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("3ķȫ̲Фԡٔܺ࠼थਿ୏ఽഘพ༝ဏᄐሩጉᐕᔒᘁᜑᠪᤨᨳ᭿ᱶᵽḏἎ\u2008ℏ∝⌎␉├☐❿⡲⤀⨅⬊Ⱍⴙ⸅⼄〄ㄖ㈆㌆㐋㔀㙨㝣㠁㤎㨄㭺㰒㴝㹱㽮䁶䅭䉱䍨䑹䕧䙰䝶䡠䤝䨐䭢䱡䵩乥佭偳其剬卦呲啠嘈圃塱奤婱孊屛嵓幟彞恅慐扜挻搶敆晁杁桄楔橉歐汉浘湅潏瀦焩片獗瑙畑癝睓硇神穉箻糒緝纾羾肽膰芶莨蒲薴蚠螶裞觑誠议販趨躳辢邮釀鋈鏇钵閠隨鞦颡馵髀鮾鳰鶎麉龉ꂌꆜꊁꎈ꒟ꖑ꛸ꟳꢳ꧿ꪁꮚ겋궞꺘꾂낅놇늗뎉뒇떈뚁럯룢릠뫮민볱뷹뻹뾗삚쇘슖쏺쓣엹웠쟺죭짾쫢쯫쳫췿캀쾋킍퇒튚폚풁행횄ퟂ\ud88c\ud9f2\udae5\udbce\udccb\uddd8\uded2\udfd8羚輸ךּﰹﴴ︲Ｄ>ĸȬ̲њԔٚܣ࠳शਵରధഩ์༭းᄦሥፇᐵᔐᘖ᜕᠇ᤘᨡᬱᰭᴪḹἩ⁺ℸ≸⌀␞┐☆✖⡲⤰⩾⬜Ⱋⴟ⸚⼎〓ㄖ㈁㌃㑦㕸㙤㝤㠹㥱㨽㬘㰞㵼㹲㽿䀚䄑䈘䍖䐘䕤䙡䝶䡡䥥䩹䭠䱠䵲乢佪偧公刈匚吆唂噟園塟夆娀子屌崽幽張恋慌扝捄摂敜晛杝桍楟橑歂汋洭渱漫瀭煲爹獺瑙畊癐睋砥礨"), string_0, string_1, DateTime.Now);
			this.dbprovider_0.ExecuteNonQuery(string_2);
			string_2 = string.Format(GClass0.smethod_0("ÍǇ˒ϔӀזڲ߂ࣥ৽૸௨೵්໤࿺ჿᇢዴᎥᓗᗆᛖឡᣃᤰᨺᬸᱜᵆṚ὞\u205f⅛∻⌠␸┧☻✮⠿⤽⨪⬨ⰾⵋ⹗⽉じㅋ㉆㌶㐡㔲㘷㜤㠮㤜㨛㬂㰕㴟㹺㽤䁡䅮䉯䍬䑭䕤䙲䜆䠘䤊䨜䬈䱬䴘丟伛倞儂刟匚名唇噢坼塠夘婅嬍屁崜帚彸恶慳或挝搔敢晧杴档楻橧止汢浴湤潨灥煢爆猘琄甄癙眐硝礸稾筒籎紻繋罌聝腄艂荜葛蕝虍蝟衑襂詋謭谱贫踭轲逹酺鉙鍊鑐镋阥霨"), string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public void SaveByArray(string string_0, int int_0, string[,] string_1, int int_1)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			for (int i = 0; i < int_1; i++)
			{
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = string_1[i, 0],
					CODE = string_1[i, 1],
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(DateTime.Now)
				});
			}
			this.UpdateList(string_0, int_0, list);
		}

		public List<SurveyAnswer> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("OŞɖ͜ћՃؖܟࠔॕੀ୞ౝഏ๽མၞᅝ቏ፐᑩᕉᙕᝒᡁᥑᨂ᭖᱈ᵺṬὸ‼ⅈ≏⍋⑎╒♏❊⡝⥗⨯⬶Ⱬⴿ⹳⼪〬ㅤ㉸㍭㑭㕵㘦㝧㡽㤣㩫㭥"), string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyAnswer> GetListBySequenceId(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("!Ĵȼ̪ЭԹ٬ݡࡪय਺ନఫ൥ท༶ူᄷሥፆᑿᕓᙏᝌᡟ᥋ᨘᭀᱞᵐṆὖ‒Ⅲ≥⍽⑸╨♵❴⡣⥭⨕⬀ⱝⴕ⹙⼄。ㅀ㉎㍻㐾㕎㙹㝪㡯㥼㩶㭴㱳㵊㹽㽷䀲䄬䈰䍴䐿䕰䘬䝤䡸䥭䩭䭵䰦䵧乽伣偫入"), string_0, int_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyAnswer> GetListByMultiple(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("/ľȶ̼лԣٶݿࡴवਠାఽ൯ฝ༸ှᄽሯጰᐉᔩᘵᜲᠡᤱᩢᬶᰨᵚṌ὘“Ⅸ≯⍫⑮╲♯❪⡽⥷⨏⬖ⱋⴟ⹓⼊「ㅊ㉄㍍㐈㕖㙓㝀㡗㥗㩋㭎㱎㵀㹰㽼䁱䅾䈺䍵䑱䕼䙳䜵䠳䥨䨣䭬䱏䵎丫伪倬兤剸卭呭啵嘦坧塽夣婫孥"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetListByMultipleBySequenceId(string string_0, string string_1, int int_0)
		{
			string string_2 = string.Format(GClass0.smethod_0("\0ėȝ̕ЌԚٍ݆ࡋऌਛଇఊെึ༑ထᄔሄጙᐞᔰᘮᜫᠾᤨ᩹ᬯ᰿ᴳḧἱ⁳℁∄⌂␙┋☔✓⠂⤎⩴⭯ⰼ⵶⸸⽣っㄣ㈯㌤㐟㕏㙈㝙㡈㥎㩐㭗㱙㵩㹛㽕䁞䅗䈑䍜䑆䕅䙈䜌䠌䥑䨘䭕䱸䵧一伃倃儂剀华呻唾噎坹塪奯婼孶屴嵳幊彽恷愲戬挰摴攼晰本桤楸橭歭汵洦湧潽瀣煫牥"), string_0, string_1, int_0);
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetListByCode(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0(")ļȴ̲еԡٴݹࡲषਢଠణ൭ฟ༾းᄿርጾᐇᔫᘷ᜴ᠧᤳ᩠ᭈ᱖ᵘṎ὞‚Ⅺ≭⍥①╰♭❬⡻⥵⨍⬈ⱕⴝ⹑⼌《ㅈ㉆㍃㐆㕔㙑㝆㡑㥕㩉㭰㱰㵂㹲㽺䁷䅼䈸䍻䑿䕾䙱䜳䠵䥪䨡䭲䰫䴪丬佤偸六剭卵否啧噽圣填奥"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetListRecord(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u001fĎȆ̌Ћԓنݏࡄअਐ଎఍ൿญ༨ီᄭሿጠᐙᔹᘥᜢᠱᤡᩲᬦ᰸ᴪḼἨ⁬℘∟⌛␞│☟✚⠍⤇⩿⭦ⰻⴏ⹃⼚〜ㅚ㉔㍝㐘㕆㙃㝐㡇㥇㩛㭞㱞㵰㹀㽌䁁䅎䈊䌔䐏䕴䙳䝷䡲䥦䩻䭾䱡䵊乚佔偓儼刺匹呹啹噲圵塗奜婖孔尬崱帩弪怬慤扸捭摭敵昦杧桽椣橫步"), string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyAnswer> GetCircleList(string string_0, string string_1, bool bool_0 = false)
		{
			string string_2 = "";
			if (bool_0)
			{
				string_2 = string.Format(GClass0.smethod_0("/ľȶ̼лԣٶݿࡴवਠାఽ൯ฝ༸ှᄽሯጰᐉᔩᘵᜲᠡᤱᩢᬶᰨᵚṌ὘“Ⅸ≯⍫⑮╲♯❪⡽⥷⨏⬖ⱋⴟ⹓⼊「ㅊ㉄㍍㐈㕖㙓㝀㡗㥗㩋㭎㱎㵀㹰㽼䁱䅾䈺䍵䑱䕼䙳䜵䠳䥨䨣䭬䱏䵝丫伪倬兤剸卭呭啵嘦坧塽夣婫孥"), string_0, string_1);
			}
			else
			{
				string_2 = string.Format(GClass0.smethod_0("óĚȒ̘Пԏٚݓࡘऑ਄ଚఙ൓ม༄ဂᄙላጔᐭᔅᘙ᜞᠍ᤕᩆᬒᰌᴆḐἄ⁀ℌ∋⌏␊┞☃✆⠑⤓⩫⭲Ⱟⵣⸯ⽶ばㄮ㈠㌩㑬㔺㘿㜬㠻㤳㨯㬪㰪㴜㸬㼠䀭䅚䈞䍑䑕䕐䙟䜙䠟䥌䨇䭈䱫䵡丗伖倐兎剀卉同啚噟坌塛奓婏孊届嵼幌彀恍慺戾捳摳敯昺杵桱楼橳欵氳洶湍潞灄煇爫猪琬畤癸睭硭祵稦筧籽紣繫罥"), string_0, string_1);
			}
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetCircleList_C(string string_0, string string_1, string string_2 = "C")
		{
			string string_3 = "";
			string_3 = string.Format(GClass0.smethod_0("ñǤˬ̚Нԉٜݑ࡚टਊଘఛൕว༆ကᄇሕ጖ᐯᔃᘟ᜜᠏ᤛᩈᬐᰎᴀḖἆ⁂Ⅎ∵⌍␈┘★✄⠓⤝⩥⭰Ⱝⵥ⸩⽴ひ㄰㈾㌫㑮㔼㘹㜮㠹㤽㨡㬨㰨㴚㸪㼢䀯䄤䉠䍓䑗䕖䙙䜛䠝䥂䨉䭊䱩䵎丆低倗儖刐华呀啉嘌坚塟奌婛孓屏嵊幊彼恌慀才捺搾敳晳杯栺極橱歼汳洵渳漶灍煞牄獇琫甪瘬睤硸祭穭筵簦絧繽缣聫腥"), string_0, string_1, string_2);
			return this.GetListBySql(string_3);
		}

		public List<SurveyAnswer> GetCircleListOther(string string_0, string string_1)
		{
			string string_2 = "";
			string_2 = string.Format(GClass0.smethod_0("\bğȕ̝ДԂٕݞࡓऔਃଟంൎ฾༙မᄜሌ጑ᐦᔈᘖᜓ᠆ᤐᩁᬗ᰷ᴻḯἹ⁻℉∌⌊␁┓☌✋⠚⤖⩬⭷ⰴ⵾⸰⽫にㄫ㈧㌬㑧㔷㘰㜡㠰㤶㨨㬯㱑㵡㹓㽝䁖䅟䈙䍔䑞䕝䙐䜔䠔䥉䨀䭍䱰䵼丈伋個克則卌吇啗噐坁塐奖婈孏山嵁平彽恶慿戹捴摾敽晰朴栴椷橎歟汛浆渪漬灤煸牭獭瑵甦癧睽砣祫穥"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetCircleListByConbine(string string_0, string string_1, bool bool_0 = false)
		{
			string string_2 = "";
			if (bool_0)
			{
				string_2 = string.Format(GClass0.smethod_0(")ļȴ̲еԡٴݹࡲषਢଠణ൭ฟ༾းᄿርጾᐇᔫᘷ᜴ᠧᤳ᩠ᭈ᱖ᵘṎ὞‚Ⅺ≭⍥①╰♭❬⡻⥵⨍⬈ⱕⴝ⹑⼌《ㅈ㉆㍃㐆㕔㙑㝆㡑㥕㩉㭰㱰㵂㹲㽺䁷䅼䈸䍻䑿䕾䙱䜳䠵䥪䨡䭲䰫䴪丬佤偸六剭卵否啧噽圣填奥"), string_0, string_1);
			}
			else
			{
				string_2 = string.Format(GClass0.smethod_0("\u000eęȗ̟КԌٗݜࡕऒਁଝజ൐฼༛ဟᄚሎጓᐨᔆᘔᜑ᠀ᤖᩃᬕᰉᴅḭἻ⁽ℏ∎⌈␏┝☎✉⠜⤐⩮⭵Ⱚⵠ⸲⽩ねㄭ㈥㌮㑩㔹㘲㜣㠶㤰㨪㬭㰯㴟㹑㽟䁐䅙䈛䍖䑐䕓䙒䜖䠒䥏䨂䭏䰔䴗丏住偃先刋卛呜啍噔坒塌奋婍孽屏嵁干彻怽慲扴据搹整晾杽桰椴樴欷汎浟湛潆瀪焬牤獸瑭畭癵眦硧祽稣筫籥"), string_0, string_1);
			}
			return this.GetListBySql(string_2);
		}

		public int GetCountByMultiple(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("\fěȑ̙ИԎٙܛ࠘ःਛ଀౛൘๘ཐဉᄜሂጁᑋᔹᘜ᜚᠑ᤃ᨜ᬥᰍᴑḖἅ‭ⅾ∪⌴␾┨☼❸⠄⤃⨇⬂Ⱆⴋ⸎⼙》ㅳ㉪㌷㑻㔷㙮㝨㠦㤨㨡㭤㰲㴷㸤㼳䁋䅗䉒䍒䑤䕔䙘䝕䡒䤖䩙䭝䱘䵗丑众偔償剐即呪唏嘎圈塦奨婡嬄屒嵗幄当恫慷扲捲摄整晸杵桲椶橛歛汇洲湝潙灄煋爭猫瑰画癴睗硈祒積筛籀紧縦"), string_0, string_1);
			return this.dbprovider_0.ExecuteScalarInt(string_2);
		}

		public int GetCountByMultiple(string string_0, string string_1, out string string_2)
		{
			string string_3 = string.Format(GClass0.smethod_0("÷ǦˮϤӣԋٞݗ࡜झਈଖకൗลༀဆᄅሗገᐱᔁᘝ᜚᠉ᤙᩊᬞᰀᴂḔἀ⁄ℰ∷⌳␶┚☇✂⠕⤟⩧⭾Ⱓⵧ⸫⽲ぴㄲ㈼㌵㑰㔾㘻㜨㠿㤿㨣㬦㰦㴘㸨㼤䀩䄦䉢䌭䐩䕔䙛䜝䠛䥀䨋䭄䱧䵶专伒倔兲剼卵吐啞噛坈塟奟婃孆屆嵸幈彄恉慆戂捯摯敋显村桕楐機欹氿浬渧潨灋煜牆獙瑏界瘫眪砬祤穸筭籭絵縦罧聽脣艫荥"), string_0, string_1);
			List<SurveyAnswer> listBySql = this.GetListBySql(string_3);
			string_2 = "";
			if (listBySql.Count<SurveyAnswer>() == 1)
			{
				string_2 = listBySql[0].CODE;
			}
			return listBySql.Count<SurveyAnswer>();
		}

		public SurveyAnswer GetOne(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0(";ĢȪ̠ЧԷ٢ݫࡠख़ੌ୒౑ഛ๩ཌ၊ᅁቓፌᑵᕝᙁᝆᡕᥝᨎ᭚᱄ᵎṘὌ\u2008ⅴ≳⍷⑲╦♻❾⡩⥛⨣⬺Ⱨ⴫⹧⼾〸ㅶ㉸㍱㐴㕢㙧㝴㡣㥻㩧㭢㱢㵔㹤㽨䁥䅢䈻䌢䑿䔲䙿䜦"), string_0, string_1);
			return this.GetBySql(string_2);
		}

		public string GetOneCode(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("8įȥ̭ФԲ٥܇ࠌआ਄ୠౙൌ๒དရᅩቌፊᑁᕓᙌ᝵ᡝ᥁ᩆ᭕ᱝᴎṚὄ⁎⅘≌⌈⑴╳♷❲⡦⥻⩾⭩ⱛⴣ⸺⽧〫ㅧ㈾㌸㑶㕸㙱㜴㡢㥧㩴㭣㱻㵧㹢㽢䁔䅤䉨䍥䑢䔻䘢䝿䠲䥿䨦"), string_0, string_1);
			return this.dbprovider_0.ExecuteScalarString(string_2);
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("Jňɀ͎ўՌ؈݁ࡔॊ੉ଃ౱ൔ๒ཀྵၻᅤቝ፵ᑩᕮᙽᝥᠶᥢ᩼᭶ᱠᵴḰ὜⁛⅟≚⍎⑓╖♁❃⠻⤢⩿⬳Ɀ⴦"), string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
