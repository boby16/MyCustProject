using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyQuotaAnswerDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("gŶɾʹѳջ؎ݮࡣॾ੤୽ఀ഍ฏ༅ၢᅱቭ፬᐀ᕌᙫᝯᡪ᥾ᩣᭈᱭᵸṢὴ⁕ⅽ≡⍦⑵╽☮❚⡄⥎⩘⭌Ⱘⵎ⹂⼥〹ㅸ㈲㍼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyQuotaAnswer GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("~ũɧͯѪռ؇܌ࠅॢੱ୭౬ഀ์ཫၯᅪቾ፣ᑈᕭᙸᝢᡴᥕ᩽᭡ᱦᵵṽἮ⁚⅄≎⍘⑌┨♎❂⠥⤹⩸⬲ⱼ"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyQuotaAnswer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyQuotaAnswer surveyQuotaAnswer = new SurveyQuotaAnswer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyQuotaAnswer.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyQuotaAnswer.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyQuotaAnswer.SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
					surveyQuotaAnswer.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					surveyQuotaAnswer.CODE = dataReader["CODE"].ToString();
					surveyQuotaAnswer.IS_FINISH = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ̀ьՊيݑࡉ")]);
				}
			}
			return surveyQuotaAnswer;
		}

		public List<SurveyQuotaAnswer> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyQuotaAnswer> list = new List<SurveyQuotaAnswer>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyQuotaAnswer
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString(),
						QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						CODE = dataReader["CODE"].ToString(),
						IS_FINISH = Convert.ToInt32(dataReader[GClass0.smethod_0("@śɘ̀ьՊيݑࡉ")])
					});
				}
			}
			return list;
		}

		public List<SurveyQuotaAnswer> GetList()
		{
			string string_ = GClass0.smethod_0("xůɥͭѤղ؅܎ࠃ।ੳ୯౒ാ๎ཀྵၩᅬቼ፡ᑆᕣᙺᝠᡲᥓ᩿᭣ᱸᵫṿἬ⁄⅘≍⍍⑕┦♇❝⠣⥋⩅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			string string_ = string.Format(GClass0.smethod_0("<ĺȠ̷УԤُܧࠣसਤ୊఺ഝต༐ကᄝሲ጗ᐎᔔᘾᜟᠳ᤯ᨬᬿᰫᵰḄἃ\u2007ℂ∖⌋␎┙☋❢⠞⤙⨙⬜Ⰼⴑ⸘⼁【ㄍ㈇㍮㐐㔕㙺㝭㡩㥵㩴㭴㱦㵶㹶㽻䁰䄘䉰䍽䑵䕵䘃䝧䡾䥳䩭䭣䱧䵡乴佮倌億創卣呭啵噚坍堵夻婠嬪層崿帻弱恮愥扮挵搽攷晴朼桰椫樧欭汲活湺漡瀩煿爷獿琨"), new object[]
			{
				surveyQuotaAnswer_0.SURVEY_ID,
				surveyQuotaAnswer_0.SURVEY_GUID,
				surveyQuotaAnswer_0.QUESTION_NAME,
				surveyQuotaAnswer_0.CODE,
				surveyQuotaAnswer_0.IS_FINISH
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			string string_ = string.Format(GClass0.smethod_0("ÑǓˆπӔԺٞܮࠉउ਌ଜఁദฃ༚ကᄒሳጟᐃᔘᘋᜟᡌᤸᨯᬽ᱈ᴴḳἷ′Ω∻⌾␩┛♾❠⡼⥼⨡⭨Ⱕ⵰⹺⼆、㄁㈄㌔㐉㔐㘉㜘㠅㤏㩪㭴㱨㵠㸽㽷䀹䅤䉮䌐䐕䕺䙭䝩䡵䥴䩴䭦䱶䵶乻佰倔儎划化呋唜噓圊堀奨婥孭屭崇帛弅怃慘或捜搇攳晗李桃楝橓歗汑浄湞漵瀩焳物猤瑭甯癙睅硉祙穏笩籁絃縦缸耤腸舲荼"), new object[]
			{
				surveyQuotaAnswer_0.ID,
				surveyQuotaAnswer_0.SURVEY_ID,
				surveyQuotaAnswer_0.SURVEY_GUID,
				surveyQuotaAnswer_0.QUESTION_NAME,
				surveyQuotaAnswer_0.CODE,
				surveyQuotaAnswer_0.IS_FINISH
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			string string_ = string.Format(GClass0.smethod_0("oůɥͭѳգ؅ݢࡱ७੬଀ౌ൫๯ཪၾᅣቈ፭ᑸᕢᙴ᝕᡽ᥡᩦ᭵ᱽᴮṚὄ⁎⅘≌⌨⑎╂☥✹⡸⤲⩼"), surveyQuotaAnswer_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("Yřɗ͟э՝طݐࡇज़ਫ਼ଲూ൥๽ླྀၨᅵቚ፿ᑦᕼᙦᝇᡫ᥷ᩴ᭧ᱳ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 6;
			string[] array = new string[6];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = "问卷编号";
				array[2] = GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
				array[3] = GClass0.smethod_0("闪馛純僶");
				array[4] = GClass0.smethod_0("筐楋純笀");
				array[5] = GClass0.smethod_0("闪剴炴挀");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = "SURVEY_ID";
				array[2] = GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
				array[3] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[4] = "CODE";
				array[5] = GClass0.smethod_0("@śɘ̀ьՊيݑࡉ");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyQuotaAnswer> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyQuotaAnswer surveyQuotaAnswer in list_0)
			{
				array[num, 0] = surveyQuotaAnswer.ID.ToString();
				array[num, 1] = surveyQuotaAnswer.SURVEY_ID;
				array[num, 2] = surveyQuotaAnswer.SURVEY_GUID;
				array[num, 3] = surveyQuotaAnswer.QUESTION_NAME;
				array[num, 4] = surveyQuotaAnswer.CODE;
				array[num, 5] = surveyQuotaAnswer.IS_FINISH.ToString();
				num++;
			}
			return array;
		}

		public int AddOne(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			int result;
			if (this.ExistsByModel(surveyQuotaAnswer_0))
			{
				this.Update(surveyQuotaAnswer_0);
				result = 1;
			}
			else
			{
				this.Add(surveyQuotaAnswer_0);
				result = 0;
			}
			return result;
		}

		public bool ExistsByModel(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u0017ĆȎ̄Ѓԫپݷࡼऽਨଶవ൷ฅ༠ဦᄥሷጨᐁᔺᘡ᜹ᠭᤊᨤᬺ᰿ᴢḴὥ″Å∧⌳␥┟♭❨⡮⥭⩿⭠Ⱨ⵾⹲⼕〉ㄔ㉉㌁㑍㔈㘎㝌㡂㥏㨊㭺㱽㵵㹰㽠䁽䅼䉥䍴䑩䕛䘣䜺䡧䤪䩧䬾䰸䵶乸佱倴兂則協呃啛噇坂塂奔婄孈居嵂帻弢恿愱承挦"), surveyQuotaAnswer_0.SURVEY_ID, surveyQuotaAnswer_0.SURVEY_GUID, surveyQuotaAnswer_0.QUESTION_NAME);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public int GetSumByModel(int int_0, string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("\u001dĈȀ̎Љԝو܄ࠉऐਊଗొോ้ཿဿᄮቼጕᐙᔶᘭ᜹ᠢ᥵ᨲᬡ᰽ᴼṰἜ※ℿ∺⌮␳┘☽✨⠲⤤⨅⬭ⰱⴶ⸥⽍〞ㅊ㉔㍞㑈㕜㘘㝾㡥㥪㩲㭺㱼㵸㹣㽧䀎䄐䈌䍐䐚䕔䘈䝆䡈䥁䨄䭲䱷䵤乳佋偗兒剒卄呔啘噕坒堶夨娳孨尣嵬帷弯息慣扨挫摉敆晌杂栻椢橿欱汿洦"), int_0.ToString(), string_0, string_1);
			return this.dbprovider_0.ExecuteScalarInt(string_2);
		}

		public void UpdateStatus(int int_0, string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0(",Ĩȳ̷СԱٳ܁ࠤढਹଫఴഝ฾༥ွᄩሆጨᐶᔳᘦᜰᡡᤳᩚᭊᰝᵵṨὥⁿⅱ≹⍿⑦╼☓✏⠑⥋⨟⭓Ⰽⵛ⹃⽏せㅍ㈇㍵㑰㕶㙵㝧㡸㥿㩖㭚㰽㴡㸼㽡䀨䅥䈰䌶䑴䕺䙷䜲䡂䥅䩝䭘䱈䵕乔位停允剃医吢啿嘱坿堦"), int_0.ToString(), string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
