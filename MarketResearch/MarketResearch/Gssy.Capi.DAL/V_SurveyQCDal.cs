using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class V_SurveyQCDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("~ũɧͯѪռ؇ݥࡪॱ੭୶ఉഊึ༾ၛᅎቔፗᐹᕎᙈᝅᡠᥦᩥ᭷ᱨᵁṌἮ⁚⅄≎⍘⑌┨♎❂⠥⤹⩸⬲ⱼ"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_SurveyQC GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๎཈၅ᅠቦ፥ᑷᕨᙁᝌᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), int_0);
			return this.GetBySql(string_);
		}

		public V_SurveyQC GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_SurveyQC v_SurveyQC = new V_SurveyQC();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_SurveyQC.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_SurveyQC.ANSWER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]);
					v_SurveyQC.QUESTION_TITLE = dataReader[GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString();
					v_SurveyQC.QUESTION_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]);
					v_SurveyQC.SPSS_TITLE = dataReader[GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ")].ToString();
					v_SurveyQC.CODE = dataReader["CODE"].ToString();
					v_SurveyQC.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					v_SurveyQC.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					v_SurveyQC.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					v_SurveyQC.DETAIL_ID = dataReader[GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString();
					v_SurveyQC.PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
					v_SurveyQC.QUESTION_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ")]);
					v_SurveyQC.ANSWER_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ")]);
					v_SurveyQC.COMBINE_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ")]);
					v_SurveyQC.SPSS_CASE = Convert.ToInt32(dataReader[GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ")]);
					v_SurveyQC.SPSS_VARIABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]);
					v_SurveyQC.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader[GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ")]);
					if (dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")] is DBNull)
					{
						v_SurveyQC.SEQUENCE_ID = 0;
					}
					else
					{
						v_SurveyQC.SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]);
					}
				}
			}
			return v_SurveyQC;
		}

		public List<V_SurveyQC> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_SurveyQC> list = new List<V_SurveyQC>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					V_SurveyQC v_SurveyQC = new V_SurveyQC();
					v_SurveyQC.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_SurveyQC.ANSWER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]);
					v_SurveyQC.QUESTION_TITLE = dataReader[GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString();
					v_SurveyQC.QUESTION_TYPE = Convert.ToInt32(dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]);
					v_SurveyQC.SPSS_TITLE = dataReader[GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ")].ToString();
					v_SurveyQC.CODE = dataReader["CODE"].ToString();
					v_SurveyQC.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					v_SurveyQC.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					v_SurveyQC.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					v_SurveyQC.DETAIL_ID = dataReader[GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString();
					v_SurveyQC.PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
					v_SurveyQC.QUESTION_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ")]);
					v_SurveyQC.ANSWER_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ")]);
					v_SurveyQC.COMBINE_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ")]);
					v_SurveyQC.SPSS_CASE = Convert.ToInt32(dataReader[GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ")]);
					v_SurveyQC.SPSS_VARIABLE = Convert.ToInt32(dataReader[GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]);
					v_SurveyQC.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader[GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ")]);
					if (dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")] is DBNull)
					{
						v_SurveyQC.SEQUENCE_ID = 0;
					}
					else
					{
						v_SurveyQC.SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]);
					}
					list.Add(v_SurveyQC);
				}
			}
			return list;
		}

		public List<V_SurveyQC> GetList()
		{
			string string_ = GClass0.smethod_0("}Ũɠͮѩս؈܍ࠆॣ੶୬౯ഁ๶ཀ၍ᅨቮ፭ᑿᕠᙉ᝔ᠶᥚᩆ᭗᱗ᵃḰὍ⁗ℭ≍⍅⑙╞♍❕⡙⥊⩖⭇ⱇⵓ");
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 18;
			string[] array = new string[18];
			if (bool_0)
			{
				array[0] = "问卷编号";
				array[1] = GClass0.smethod_0("颞矫趗勹鱸宎");
				array[2] = GClass0.smethod_0("辕僿韪鮛䭽瀩");
				array[3] = GClass0.smethod_0("丿馛骚咊");
				array[4] = GClass0.smethod_0("TŖɖ͗У洅麙");
				array[5] = GClass0.smethod_0("缐礄ﴌ硗汊行");
				array[6] = GClass0.smethod_0("缒礂枅搭");
				array[7] = GClass0.smethod_0("顶縔凶");
				array[8] = GClass0.smethod_0("闦馟紐僲﬌庝遇");
				array[9] = GClass0.smethod_0("养腓韨鮝戊篅捲摯");
				array[10] = GClass0.smethod_0("爲絸䳡笀");
				array[11] = GClass0.smethod_0("颐矩搩圣䬞趨婗縻");
				array[12] = GClass0.smethod_0("筜楏搩圣䬞趨巾囻");
				array[13] = GClass0.smethod_0("绌唏骞疁彔鶛笠堔");
				array[14] = GClass0.smethod_0("TŖɖ͗У鶚冊");
				array[15] = GClass0.smethod_0("ZŘɔ͕Х囜韌筹徊");
				array[16] = GClass0.smethod_0("[ŗɕ͖Ф夌捲䡌");
				array[17] = GClass0.smethod_0("闫剳岌儕埶");
			}
			else
			{
				array[0] = "SURVEY_ID";
				array[1] = GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓");
				array[2] = GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ");
				array[3] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ");
				array[4] = GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ");
				array[5] = "CODE";
				array[6] = "CODE_TEXT";
				array[7] = GClass0.smethod_0("WŇɂ́ќՋم");
				array[8] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[9] = GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ");
				array[10] = GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
				array[11] = GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ");
				array[12] = GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ");
				array[13] = GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ");
				array[14] = GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ");
				array[15] = GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ");
				array[16] = GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ");
				array[17] = GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<V_SurveyQC> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_SurveyQC v_SurveyQC in list_0)
			{
				array[num, 0] = v_SurveyQC.SURVEY_ID;
				array[num, 1] = v_SurveyQC.ANSWER_ORDER.ToString();
				array[num, 2] = v_SurveyQC.QUESTION_TITLE;
				array[num, 3] = v_SurveyQC.QUESTION_TYPE.ToString();
				array[num, 4] = v_SurveyQC.SPSS_TITLE;
				array[num, 5] = v_SurveyQC.CODE;
				array[num, 6] = v_SurveyQC.CODE_TEXT;
				array[num, 7] = v_SurveyQC.PAGE_ID;
				array[num, 8] = v_SurveyQC.QUESTION_NAME;
				array[num, 9] = v_SurveyQC.DETAIL_ID;
				array[num, 10] = v_SurveyQC.PARENT_CODE;
				array[num, 11] = v_SurveyQC.QUESTION_USE.ToString();
				array[num, 12] = v_SurveyQC.ANSWER_USE.ToString();
				array[num, 13] = v_SurveyQC.COMBINE_INDEX.ToString();
				array[num, 14] = v_SurveyQC.SPSS_CASE.ToString();
				array[num, 15] = v_SurveyQC.SPSS_VARIABLE.ToString();
				array[num, 16] = v_SurveyQC.SPSS_PRINT_DECIMAIL.ToString();
				array[num, 17] = v_SurveyQC.SEQUENCE_ID.ToString();
				num++;
			}
			return array;
		}

		public List<V_SurveyQC> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u0099ƌʄ΂֑҅ۤށ࣬঒ક௭೨෸໥࿤ჳᇽኘᏖᓅᖕ᛻៟ᣖ᧮᫹ᯫᲂᶍẋῐₚ⇔⊏⎇ⓧ◶⚄⟰⣷⧳⫶⯚ⳇⷂ⻕⿟ザㆹ㋙㎹㓗㗛㛇㟄㣗㧃㫏㯀㳜㷉㻉㿙䂦䆩䋉䎩䓗䗐䛁䟐䣖䧈䫏䬱䰡䴩丵伯倶儼剔匶员唦嘤圠堡央娤嬦尺崡帩彇恊愫扆挤搩攡昡杏桂楆橇歿氟洎湼漘瀕焝爝猈琂甐瘌眇硾祱稑筡簞紌縋缎耕脀舌荫葦蔄虪蜒蠗褄訓譫豷赲蹲轤遴酸鉵鍲鐚锕陵霝顣饤驵魼鱺鵤鹣齥ꁵꅽꉱꍷꑣꔉꙥ꜍ꡦꥤꩴꭞ걗굑깃꽒끞넵눸덖됸땅뙕띁롗륟멄뭐뱍뵂빈뽎쀦섩쉉쌩쑗앐왁읐졖쥈쩏쮱첡춨캯쾾탖퇙특폙풷햻횧힤\ud8b7\ud9a3\udaaf\udbba\udcbd\udda8\udec0\udfcb戀廙ﮊﳢ﷭ﺍ￥\u0099ƙʛΔҙ֓څޑࢋঀં௳೻එ໽ྕჩᇩያᏤᓩᗥᛦ៺᣼᧥᫯ᯫᳫᷮụῦ⃫⇠⋤⎋ⓤ▋⛷⟦⣳⧴⫥⯑ⳝⷘ⻃⿒マㆹ㋞㏅㓙㗘㚴㟀㣍㧕㫵㯩㳧㷣㻩㾫䃋䆩䋄䏂䓀䗑䚤䟉䣍䧈䫎䭟䱞䴮三伉倌儜刁匶吘唆嘃圖堀契娱嬼屎崯幌弤怤慉戩捉搷攰昡朰栶椨樯欑氁洓渝漖瀟煹牥獷琔畻瘅眆砗礂稄笆簁紃縓缅耋脄舍荧葦蔄蘊蜇衢褃詮譬豫赯蹪轾遣酦鉱鍳鐖锈阔霔顉餁驍鬈鰎鵺鹤齮ꁸꅬꈈꍦꐈꕤꙪꝰ꡵ꥤꩲꭀ걋굎깙꼥뀪넹뉗덅둒땐뙆뜳롐륈먰뭎밠뵌빂뽘쁝셌쉚썘쑉앗와읆졐줺"), string_0);
			return this.GetListBySql(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
