using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyAttachDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬ᭕ᱧᵦṰέ⁧℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyAttach GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑕᕧᙦᝰᡳᥧᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyAttach GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyAttach surveyAttach = new SurveyAttach();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyAttach.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyAttach.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyAttach.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					surveyAttach.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					surveyAttach.FILE_NAME = dataReader[GClass0.smethod_0("OŁɋ̓њՊقݏࡄ")].ToString();
					surveyAttach.FILE_TYPE = dataReader[GClass0.smethod_0("OŁɋ̓њՐٚݒࡄ")].ToString();
					surveyAttach.ORIGINAL_NAME = dataReader[GClass0.smethod_0("BŞɂ͍рՆن࡚݊ॊੂ୏ౄ")].ToString();
					surveyAttach.SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
				}
			}
			return surveyAttach;
		}

		public List<SurveyAttach> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyAttach> list = new List<SurveyAttach>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyAttach
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						FILE_NAME = dataReader[GClass0.smethod_0("OŁɋ̓њՊقݏࡄ")].ToString(),
						FILE_TYPE = dataReader[GClass0.smethod_0("OŁɋ̓њՐٚݒࡄ")].ToString(),
						ORIGINAL_NAME = dataReader[GClass0.smethod_0("BŞɂ͍рՆن࡚݊ॊੂ୏ౄ")].ToString(),
						SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyAttach> GetList()
		{
			string string_ = GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑓᕥᙤᝮᡭᥥᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyAttach surveyAttach_0)
		{
			string string_ = string.Format(GClass0.smethod_0("Ðǖ˄ϓӇ׀ڳߛࣟৄીமೞ෹໹࿼წᇱ዆Ᏺᓱᗥᛠ៪ᢩ᧓ᨪᬬᰫᴹḢἥ‰ℼ≛⌦␴┳☶✭⠸⤴⩃⬿ⰸ⴩⸸⼾〠ㄧ㈩㌹㐫㔥㘮㜧㡍㤦㨖㬒㰘㴃㸕㼛䀔䄝䉻䌐䐜䔘䘖䜍䠅䤉䨟䬋䱡䴃丙伃倎儁刉匇吉唛嘍圃堌夅娓孭屨嵮幭彿恠慧扰捣摼数昚朒桧楱橣死汨浿渃漍灒焘牚猁琉甃癘眓硜礇稳笹籦紮繦缽耵脿艬茥葨蔳蘿蜵衪褤該謩谡贫蹰輿遴鄯鈫錡鑾锲陾霥頨"), new object[]
			{
				surveyAttach_0.SURVEY_ID,
				surveyAttach_0.PAGE_ID,
				surveyAttach_0.QUESTION_NAME,
				surveyAttach_0.FILE_NAME,
				surveyAttach_0.FILE_TYPE,
				surveyAttach_0.ORIGINAL_NAME,
				surveyAttach_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyAttach surveyAttach_0)
		{
			string string_ = string.Format(GClass0.smethod_0("ùǻˮϨӼעچ߶࣑৑૔௄೙ෞ໪࿩ჽᇸዲᎹᓋᗒᛂ឵ᣇᧆᫀᯇ᳕ᷖốῄ⃈↫⊷⎩⒯◼⚷⟸⢣⦯⫒⯀ⳇⴺ⸡⼴〸ㅛ㉇㍙㑟㔌㙄㜈㡓㥟㨣㬤㰵㴼㸺㼤䀣䄥䈵䌧䐩䔪䘣䝅䡙䥃䩅䬚䱓䴢乹佱倚儒刖匜吇唙嘗團堑女婯孱屷崴幺弰恫慧戌挀搄攂昙朑栝椓樇歡汽洟渙潆瀉煆爝猕瑷略癿睲硽祽穳筽籯絡繯罠聩脋舗茉萏蕜蘐蝘蠃褏話譴豲赉蹛轄遃酜鉏鍐鑜锷阫霵頳饨騥魬鰷鴯鹙齅ꁉꅙꉏꌩꑁꕃ꘦Ꜹꠤꥸꨲꭼ"), new object[]
			{
				surveyAttach_0.ID,
				surveyAttach_0.SURVEY_ID,
				surveyAttach_0.PAGE_ID,
				surveyAttach_0.QUESTION_NAME,
				surveyAttach_0.FILE_NAME,
				surveyAttach_0.FILE_TYPE,
				surveyAttach_0.ORIGINAL_NAME,
				surveyAttach_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyAttach surveyAttach_0)
		{
			string string_ = string.Format(GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬቕ፧ᑦᕰᙳᝧᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyAttach_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾቇ፱ᑰᕢᙡᝩ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 8;
			string[] array = new string[8];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = "问卷编号";
				array[2] = GClass0.smethod_0("顶縔凶");
				array[3] = GClass0.smethod_0("闪馛純僶");
				array[4] = GClass0.smethod_0("陁俲构䷴倌");
				array[5] = GClass0.smethod_0("陀俵繹咊");
				array[6] = GClass0.smethod_0("陎俿冗嫌觩媁嗎抄䛴崌");
				array[7] = GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = "SURVEY_ID";
				array[2] = GClass0.smethod_0("WŇɂ́ќՋم");
				array[3] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[4] = GClass0.smethod_0("OŁɋ̓њՊقݏࡄ");
				array[5] = GClass0.smethod_0("OŁɋ̓њՐٚݒࡄ");
				array[6] = GClass0.smethod_0("BŞɂ͍рՆن࡚݊ॊੂ୏ౄ");
				array[7] = GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyAttach> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyAttach surveyAttach in list_0)
			{
				array[num, 0] = surveyAttach.ID.ToString();
				array[num, 1] = surveyAttach.SURVEY_ID;
				array[num, 2] = surveyAttach.PAGE_ID;
				array[num, 3] = surveyAttach.QUESTION_NAME;
				array[num, 4] = surveyAttach.FILE_NAME;
				array[num, 5] = surveyAttach.FILE_TYPE;
				array[num, 6] = surveyAttach.ORIGINAL_NAME;
				array[num, 7] = surveyAttach.SURVEY_GUID;
				num++;
			}
			return array;
		}

		public List<SurveyAttach> GetListBySurveyId(string string_0)
		{
			List<SurveyAttach> list = new List<SurveyAttach>();
			string string_ = string.Format(GClass0.smethod_0("OŞɖ͜ћՃؖܟࠔॕੀ୞ౝഏ๽མၞᅝ቏ፐᑩᕓᙒᝄᡇ᥋ᨂ᭖᱈ᵺṬὸ‼ⅈ≏⍋⑎╒♏❊⡝⥗⨯⬶Ⱬⴿ⹳⼪〬ㅄ㉘㍍㑍㕕㘦㝇㡝㤣㩋㭅"), string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyAttach> GetListByPageId(string string_0, string string_1)
		{
			List<SurveyAttach> list = new List<SurveyAttach>();
			string string_2 = string.Format(GClass0.smethod_0("=ĨȠ̮ЩԽ٨ݭࡦणਸ਼ବయൡณཊ၌ᅋ቙ፂᑻᕍᙌ᝖ᡕᥝᨔ᭄ᱚᵔṂὊ‎ⅾ≹⍹⑼╬♱❸⡯⥡⨙⬄ⱙⴑ⹝⼸〾ㅼ㉲㍿㐺㕉㙙㝐㡓㥊㩝㭗㰯㴶㹫㼾䁳䄪䈬䍄䑘䕍䙍䝕䠦䥇䩝䬣䱋䵅"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public bool ExistsByPageId(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("9ĬȤ̢ХԱ٤܀ࠍऔ਎୫ఖഗต༛ၜᅋ቗ፚᐖᕦᙁᝁᡄᥔᩉ᭮ᱚᵙṍὈ⁂℉≟⍏⑃╗♁✃⡱⥴⩲⭉ⱛⵄ⹃⽒ぞㄤ㈿㍬㐦㕨㘳㜳㡳㥿㩴㬯㱞㵌㹋㽎䁕䅀䉌䌺䐡䕾䘵䝾䠥䤡"), string_0, string_1);
			int num = this.dbprovider_0.ExecuteScalarInt(string_2);
			return num > 0;
		}

		public bool ExistsByQName(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("#ĪȢ̨ЯԿ٪܊ࠇऒਈ଑౬൩๫ཡဦᅍቑፐᐜᕨᙏᝋᡎᥒᩏ᭴᱀ᵇṓὒ⁘ℏ≙⍅⑉╙♏✉⡻⥲⩴⭳ⱡ⵺⹽⽨つㄢ㈹㍦㐬㕦㘽㜹㡹㥹㩲㬵㱅㵆㹗㽂䁄䅆䉁䍃䑓䕅䙋䝄䡍䤺䨡䭾䰵䵾严伡"), string_0, string_1);
			int num = this.dbprovider_0.ExecuteScalarInt(string_2);
			return num > 0;
		}

		public List<SurveyAttach> GetListByQName(string string_0, string string_1)
		{
			List<SurveyAttach> list = new List<SurveyAttach>();
			string string_2 = string.Format(GClass0.smethod_0("'ĶȾ̴гԻٮݧ࡬भਸଦథ൧ต༰ံᄵሧጸᐁᕋᙊ᝜ᡟᥓᨚ᭎᱐ᵒṄὐ—Ⅰ≧⍣⑦╪♷❲⡥⥯⨗⬎ⱓⴗ⹛⼂〄ㅂ㉌㍅㐀㕎㙋㝘㡏㥏㩓㭖㱖㵈㹘㽔䁙䅖䈯䌶䑫䔾䙳䜪䠬䥄䩘䭍䱍䵕並佇偝儣剋卅"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public void DeleteByPageId(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("\u0004źɲ͸Ѩվؚݿࡪॸ੻କ౧െเཇၕᅖቯፙᑘᕊᙉᝁ᠈ᥰᩮ᭠ᱶᵦḂὲ⁵⅍≈⍘⑅╄♓❝⠥⤰⩭⬥Ⱪⴴ⸲⽰まㅫ㈮㍝㑍㕌㙏㝖㡁㥃㨻㬢㱿㴲㹿㼦"), string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public void DeleteByFileName(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format(GClass0.smethod_0("\u0011đȟ̗Ѕԕٯ܈ࠟःਆ୪చഽี༰ဠᄽሂጶᐵᔡᙜ᝖᠝ᥫᩳ᭿ᱫᵽḗὥ⁠Ⅶ≥⍷⑨╯♦❪⠐⤋⩐⬚ⱔⴏ⸇⽇かㅀ㈃㍲㑠㕧㙚㝁㡔㥘㨦㬽㱢㴩㹪㼱䀵䅵䉽䍶䐱䕖䙆䝂䡈䥓䩅䭋䱄䵍丧伻倢兿刱卿否"), string_0, string_1, string_2);
			this.dbprovider_0.ExecuteNonQuery(string_3);
		}

		public void DeleteByQNameByFileName(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format(GClass0.smethod_0("\u0018ĞȖ̜ЌԒٶܓࠆजਟୱఃഺ฼༻ဩᄲላጽᐼᔦᘥᜭᡤᤔᨊᬄᰒᵺḞὮ⁩Ⅹ≬⍼②╨♿❱⠉⤔⩉⬁ⱍⴈ⸎⽌あㅏ㈊㌉㑹㕲㙣㝶㡰㥪㩭㭯㱿㵑㹟㽐䁙䄦䈽䍢䐩䕪䘱䜵䡵䥽䩶䬱䱖䵆乂佈偓充剋卄呍唧嘻圢塿失婿嬦"), string_0, string_1, string_2);
			this.dbprovider_0.ExecuteNonQuery(string_3);
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("Jňɀ͎ўՌ؈݁ࡔॊ੉ଃ౱ൔ๒ཀྵၻᅤቝ፯ᑮᕸᙻ᝿ᠶᥢ᩼᭶ᱠᵴḰ὜⁛⅟≚⍎⑓╖♁❃⠻⤢⩿⬳Ɀ⴦"), string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
