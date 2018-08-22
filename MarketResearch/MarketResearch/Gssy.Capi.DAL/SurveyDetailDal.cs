using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyDetailDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬ᭐ᱶᵦṰό⁣℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyDetail GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑐᕶᙦᝰ᡹ᥣᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyDetail GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyDetail surveyDetail = new SurveyDetail();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyDetail.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyDetail.DETAIL_ID = dataReader[GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString();
					surveyDetail.CODE = dataReader["CODE"].ToString();
					surveyDetail.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					surveyDetail.IS_OTHER = Convert.ToInt32(dataReader[GClass0.smethod_0("AŔə͊ѐՋهݓ")]);
					surveyDetail.INNER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("Bńɇ͍ѕՙيݖࡇे੓")]);
					surveyDetail.PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
					surveyDetail.RANDOM_BASE = Convert.ToInt32(dataReader[GClass0.smethod_0("Yŋɇ͌шՋ݆ٚࡂ॑੄")]);
					surveyDetail.RANDOM_SET = Convert.ToInt32(dataReader[GClass0.smethod_0("XňɆ̓щՈٛݐࡇॕ")]);
					surveyDetail.RANDOM_FIX = Convert.ToInt32(dataReader[GClass0.smethod_0("XňɆ̓щՈٛ݅ࡋख़")]);
					surveyDetail.EXTEND_1 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܰ")].ToString();
					surveyDetail.EXTEND_2 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܳ")].ToString();
					surveyDetail.EXTEND_3 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܲ")].ToString();
					surveyDetail.EXTEND_4 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܵ")].ToString();
					surveyDetail.EXTEND_5 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܴٝ")].ToString();
					surveyDetail.EXTEND_6 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܷٝ")].ToString();
					surveyDetail.EXTEND_7 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇٝܶ")].ToString();
					surveyDetail.EXTEND_8 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܹٝ")].ToString();
					surveyDetail.EXTEND_9 = dataReader[GClass0.smethod_0("Mşɒ̀ъՇܸٝ")].ToString();
					surveyDetail.EXTEND_10 = dataReader[GClass0.smethod_0("LŐɓ̓ыՀٜܳ࠱")].ToString();
				}
			}
			return surveyDetail;
		}

		public List<SurveyDetail> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyDetail> list = new List<SurveyDetail>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyDetail
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						DETAIL_ID = dataReader[GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString(),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						IS_OTHER = Convert.ToInt32(dataReader[GClass0.smethod_0("AŔə͊ѐՋهݓ")]),
						INNER_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("Bńɇ͍ѕՙيݖࡇे੓")]),
						PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString(),
						RANDOM_BASE = Convert.ToInt32(dataReader[GClass0.smethod_0("Yŋɇ͌шՋ݆ٚࡂ॑੄")]),
						RANDOM_SET = Convert.ToInt32(dataReader[GClass0.smethod_0("XňɆ̓щՈٛݐࡇॕ")]),
						RANDOM_FIX = Convert.ToInt32(dataReader[GClass0.smethod_0("XňɆ̓щՈٛ݅ࡋख़")]),
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

		public List<SurveyDetail> GetList()
		{
			string string_ = GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑖᕴᙤᝮᡧᥡᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyDetail surveyDetail_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\0ĆȔ̃ЗԐ٣܋ࠏऔੰଞ౮൉้ཌၜᅁታፓᑁᕕᙚ᝞᠙ᥴᩪ᭺ᱬᵥṧή⁠Ⅼ∋⍥⑪╠♦✎⡢⥯⩛⭛ⱂⵈ⹞⽂きㄴ㉞㍅㑊㕛㙇㝚㡔㥂㨣㭇㱃㵂㹎㽘䁖䅇䉕䍂䑀䕖䘯䝒䡀䥒䪺䮰䲩䶣亸侵傽冽勛厤咴喺嚷垽墼妯媭宯岾嶩廇徸您憦抣掩撨斻暰枧梵槌檍殟沓涘溔澗炆熞犞玎瓹疑皋瞆碔禞窋箑糼締纎羒肝膍芉莂蒚藶蛯螇袙覔諺诰賹跣躈辖郼釠鋣鏳铻闰雬鞆额駵髷鯺鳨鷢黯鿵ꂜꆄꋢꏾꓱꗡꛭ꟦ꣾꦖꪳꯛ곅귈껞꿔냝뇇늠뎺듐뗌뛇럗룟맔뫐뮶벡뷉뻓뿞샌쇆싃쏙쒼얨웆쟚죕짅쨱쬺찢쵍칋콓큙턮툶팺퐠픱혠흚\ud856\ud90b\uda5f\udb13\udc4a\udd40\ude4c\udf11復免ﭢﰶﵻ︶ｦ2ŰȺͪѢԿٺܿࡦ६ਘ୅ఌഌๆ༝ပᄟቌጇᐄᕉᘔ᜞᠖᥋᨞ᬜ᱐ᴋḇἍ⁒ℙ∔⍛␂┈☄❙⠐⤔⩢⬹ⰱⴻ⹠⼫〬ㅥ㈰㌺㐲㕯㘢㜤㡬㤷㨣㬩㱶㴽㸼㽷䀮䄤䈠䍽䐴䔼䙾䜥䠨"), new object[]
			{
				surveyDetail_0.DETAIL_ID,
				surveyDetail_0.CODE,
				surveyDetail_0.CODE_TEXT,
				surveyDetail_0.IS_OTHER,
				surveyDetail_0.INNER_ORDER,
				surveyDetail_0.PARENT_CODE,
				surveyDetail_0.RANDOM_BASE,
				surveyDetail_0.RANDOM_SET,
				surveyDetail_0.RANDOM_FIX,
				surveyDetail_0.EXTEND_1,
				surveyDetail_0.EXTEND_2,
				surveyDetail_0.EXTEND_3,
				surveyDetail_0.EXTEND_4,
				surveyDetail_0.EXTEND_5,
				surveyDetail_0.EXTEND_6,
				surveyDetail_0.EXTEND_7,
				surveyDetail_0.EXTEND_8,
				surveyDetail_0.EXTEND_9,
				surveyDetail_0.EXTEND_10
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyDetail surveyDetail_0)
		{
			string string_ = string.Format(GClass0.smethod_0(" Ĥȷ̳ХԵُܽ࠘ञਝଏఐബข༒ငᄍሏፂᐲᔥᘋ᝾᠙ᤙᨏᬛᰐᴔḈ἟‑ⅴ≮⍲⑶┫♾✳⡪⥠⨈⬅Ⰽⴍ⹧⽻づㅣ㈸㍰㐼㕧㘓㝽㡲㥸㩾㭥㱭㵽㹯㽢䀕䄉䈓䌕䑊䔃䙒䜉䠁䥥䩸䭵䱦䵼乯佣偷億刞匂呚唔噢圲塔奒婕孟屋嵇幘彄恑慑扁挲搬攰晴朻桰椠橛歋汛浍湉潒灚煇牌獆瑄甠盂矞磚禇竍箇糞緔纥羷肻膰芼莿蒮薲蚮螽袨觌論诊貒跟躚迊邷醥銭鎦钮閭隀鞍题馈髻鯧鳹鶣黯龫ꃹꆆꊒꎜ꒕ꖟꚂꞑꢋꦅꪓꯪ곴귨꺼꿿낸뇨늆뎚뒕떅뛱럺룢릍몛뮇벙붟뻌뾇삅쇉슔쎞쓴엨웻쟫죣짨쫴쮘첉축캇쾁탞톕튒폟풆햌훚ퟆ\ud8c9\ud9d9\udad5\udbde\udcc6\uddab\udeb7\udfab縷慨ﬢﰭﴽ︹Ｒ*Łɓ͏ё՗ؔݟ࡙ऑੌ୆బരำ༣ါᄠሼፔᑁᕝᙿ᝹ᠦᥭᩮᬧ᱾ᵴḒἎ\u2001ℑ∝⌖␎╧♯❳⡭⥫⨰⭻Ɀⴵ⹠⽪\u3000ㄜ㈗㌇㐏㔄㙠㜆㠝㤁㨛㬝㱂㴉㸀㽋䀒䄘䉶䍪䑥䕵䙡䝪䡲䤕䨋䬗䰉䴏乜众倝兙刄匎呤啸噋坛塓奘婄嬫尩崸帪弶怲慯戢挫摬攷是杙桅楉橙歏氩流湃漦瀸焤牸猲瑼"), new object[]
			{
				surveyDetail_0.ID,
				surveyDetail_0.DETAIL_ID,
				surveyDetail_0.CODE,
				surveyDetail_0.CODE_TEXT,
				surveyDetail_0.IS_OTHER,
				surveyDetail_0.INNER_ORDER,
				surveyDetail_0.PARENT_CODE,
				surveyDetail_0.RANDOM_BASE,
				surveyDetail_0.RANDOM_SET,
				surveyDetail_0.RANDOM_FIX,
				surveyDetail_0.EXTEND_1,
				surveyDetail_0.EXTEND_2,
				surveyDetail_0.EXTEND_3,
				surveyDetail_0.EXTEND_4,
				surveyDetail_0.EXTEND_5,
				surveyDetail_0.EXTEND_6,
				surveyDetail_0.EXTEND_7,
				surveyDetail_0.EXTEND_8,
				surveyDetail_0.EXTEND_9,
				surveyDetail_0.EXTEND_10
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyDetail surveyDetail_0)
		{
			string string_ = string.Format(GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬቐ፶ᑦᕰᙹᝣᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyDetail_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾቂ፠ᑰᕢᙫ᝭");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 20;
			string[] array = new string[20];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = GClass0.smethod_0("NŬɮͮѨՠ蹬电眔焀");
				array[2] = "编码";
				array[3] = GClass0.smethod_0("缒礂枅搭");
				array[4] = GClass0.smethod_0("昫唥却䷗");
				array[5] = GClass0.smethod_0("埼昩厁鏫鱸宎");
				array[6] = GClass0.smethod_0("夜羢璀焵笔紀");
				array[7] = GClass0.smethod_0("隈昼骝儂竇勸繁");
				array[8] = GClass0.smethod_0("隊显骛億竅");
				array[9] = GClass0.smethod_0("昦售针搼鲝玀價岘邙");
				array[10] = GClass0.smethod_0("扯嵐厁墺Т԰");
				array[11] = GClass0.smethod_0("扯嵐厁墺ТԳ");
				array[12] = GClass0.smethod_0("扯嵐厁墺ТԲ");
				array[13] = GClass0.smethod_0("扯嵐厁墺ТԵ");
				array[14] = GClass0.smethod_0("扯嵐厁墺ТԴ");
				array[15] = GClass0.smethod_0("扯嵐厁墺ТԷ");
				array[16] = GClass0.smethod_0("扯嵐厁墺ТԶ");
				array[17] = GClass0.smethod_0("扯嵐厁墺ТԹ");
				array[18] = GClass0.smethod_0("扯嵐厁墺ТԸ");
				array[19] = GClass0.smethod_0("扮嵓厀墽УԳر");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ");
				array[2] = "CODE";
				array[3] = "CODE_TEXT";
				array[4] = GClass0.smethod_0("AŔə͊ѐՋهݓ");
				array[5] = GClass0.smethod_0("Bńɇ͍ѕՙيݖࡇे੓");
				array[6] = GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
				array[7] = GClass0.smethod_0("Yŋɇ͌шՋ݆ٚࡂ॑੄");
				array[8] = GClass0.smethod_0("XňɆ̓щՈٛݐࡇॕ");
				array[9] = GClass0.smethod_0("XňɆ̓щՈٛ݅ࡋख़");
				array[10] = GClass0.smethod_0("Mşɒ̀ъՇٝܰ");
				array[11] = GClass0.smethod_0("Mşɒ̀ъՇٝܳ");
				array[12] = GClass0.smethod_0("Mşɒ̀ъՇٝܲ");
				array[13] = GClass0.smethod_0("Mşɒ̀ъՇٝܵ");
				array[14] = GClass0.smethod_0("Mşɒ̀ъՇܴٝ");
				array[15] = GClass0.smethod_0("Mşɒ̀ъՇܷٝ");
				array[16] = GClass0.smethod_0("Mşɒ̀ъՇٝܶ");
				array[17] = GClass0.smethod_0("Mşɒ̀ъՇܹٝ");
				array[18] = GClass0.smethod_0("Mşɒ̀ъՇܸٝ");
				array[19] = GClass0.smethod_0("LŐɓ̓ыՀٜܳ࠱");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyDetail> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyDetail surveyDetail in list_0)
			{
				array[num, 0] = surveyDetail.ID.ToString();
				array[num, 1] = surveyDetail.DETAIL_ID;
				array[num, 2] = surveyDetail.CODE;
				array[num, 3] = surveyDetail.CODE_TEXT;
				array[num, 4] = surveyDetail.IS_OTHER.ToString();
				array[num, 5] = surveyDetail.INNER_ORDER.ToString();
				array[num, 6] = surveyDetail.PARENT_CODE;
				array[num, 7] = surveyDetail.RANDOM_BASE.ToString();
				array[num, 8] = surveyDetail.RANDOM_SET.ToString();
				array[num, 9] = surveyDetail.RANDOM_FIX.ToString();
				array[num, 10] = surveyDetail.EXTEND_1;
				array[num, 11] = surveyDetail.EXTEND_2;
				array[num, 12] = surveyDetail.EXTEND_3;
				array[num, 13] = surveyDetail.EXTEND_4;
				array[num, 14] = surveyDetail.EXTEND_5;
				array[num, 15] = surveyDetail.EXTEND_6;
				array[num, 16] = surveyDetail.EXTEND_7;
				array[num, 17] = surveyDetail.EXTEND_8;
				array[num, 18] = surveyDetail.EXTEND_9;
				array[num, 19] = surveyDetail.EXTEND_10;
				num++;
			}
			return array;
		}

		public List<SurveyDetail> GetDetails(string string_0)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			string string_ = string.Format(GClass0.smethod_0("6ġȯ̧ТԴ؟ܔࠝग़੉୕౔ഘ๤གྷ၇ᅂቖፋᑵᕕᙛᝏᡄ᥀ᨋ᭝᱁ᵍṕὃ\u2005Ⅰ≦⍶①╩♓❁⡔⥘⨦⬽Ɫ⴨⹪⼱〵ㅛ㉁㍖㑔㕂㘯㝌㡔㤬㩂㭄㱇㵍㹕㽙䁊䅖䉇䍇䑓"), string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyDetail> GetDetails(string string_0, out string string_1)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			string string_2 = string.Format(GClass0.smethod_0("6ġȯ̧ТԴ؟ܔࠝग़੉୕౔ഘ๤གྷ၇ᅂቖፋᑵᕕᙛᝏᡄ᥀ᨋ᭝᱁ᵍṕὃ\u2005Ⅰ≦⍶①╩♓❁⡔⥘⨦⬽Ɫ⴨⹪⼱〵ㅛ㉁㍖㑔㕂㘯㝌㡔㤬㩂㭄㱇㵍㹕㽙䁊䅖䉇䍇䑓"), string_0);
			list = this.GetListBySql(string_2);
			string_1 = "";
			foreach (SurveyDetail surveyDetail in list)
			{
				if (surveyDetail.IS_OTHER == 1)
				{
					string_1 = surveyDetail.CODE;
				}
			}
			return list;
		}

		public List<SurveyDetail> GetDetails(string string_0, out string string_1, out string string_2, out string string_3, out string string_4, out string string_5)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			string string_6 = string.Format(GClass0.smethod_0("6ġȯ̧ТԴ؟ܔࠝग़੉୕౔ഘ๤གྷ၇ᅂቖፋᑵᕕᙛᝏᡄ᥀ᨋ᭝᱁ᵍṕὃ\u2005Ⅰ≦⍶①╩♓❁⡔⥘⨦⬽Ɫ⴨⹪⼱〵ㅛ㉁㍖㑔㕂㘯㝌㡔㤬㩂㭄㱇㵍㹕㽙䁊䅖䉇䍇䑓"), string_0);
			list = this.GetListBySql(string_6);
			string_1 = "";
			string_2 = "";
			string_3 = "";
			string_4 = "";
			string_5 = "";
			foreach (SurveyDetail surveyDetail in list)
			{
				if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3)
				{
					string_1 = surveyDetail.CODE;
					string_2 = surveyDetail.CODE_TEXT;
				}
				if (surveyDetail.IS_OTHER == 2 || surveyDetail.IS_OTHER == 3)
				{
					string_3 = surveyDetail.CODE;
				}
				if (surveyDetail.IS_OTHER == 11 || surveyDetail.IS_OTHER == 13)
				{
					string_4 = surveyDetail.CODE;
					string_5 = surveyDetail.CODE_TEXT;
				}
			}
			return list;
		}

		public List<SurveyDetail> GetList(string string_0, string string_1)
		{
			string string_2;
			if (string_1 == "")
			{
				string_2 = string.Format(GClass0.smethod_0("6ġȯ̧ТԴ؟ܔࠝग़੉୕౔ഘ๤གྷ၇ᅂቖፋᑵᕕᙛᝏᡄ᥀ᨋ᭝᱁ᵍṕὃ\u2005Ⅰ≦⍶①╩♓❁⡔⥘⨦⬽Ɫ⴨⹪⼱〵ㅛ㉁㍖㑔㕂㘯㝌㡔㤬㩂㭄㱇㵍㹕㽙䁊䅖䉇䍇䑓"), string_0);
			}
			else
			{
				string_2 = string.Format(GClass0.smethod_0("(Ŀȵ̽дԢٵݾࡳऴਣିఢ൮พ္༹ᄼሬጱᐃᔣᘱᜥᠪ᤮ᩡᬷ᱗ᵛṏὙ‛ⅾ≼⍬⑶╿♹❫⡺⥶⨌⬗ⱔⴞ⹐⼋》ㅫ㉧㍬㐇㕶㙤㝶㡦㥬㩵㭿㱜㵑㹙㽙䀦䄽䉢䌩䑪䔱䘵䝛䡁䥖䩔䭂䰯䵌乔伬偂兄則卍呕啙噊坖塇奇婓"), string_0, string_1);
			}
			return this.GetListBySql(string_2);
		}

		public string GetCodeText(string string_0, string string_1)
		{
			string text = "";
			string string_2 = string.Format(GClass0.smethod_0(";ĢȪ̠ЧԷ٢ܢ࠯ज़ਜ਼ୢై൞โཌྷဘᅑቄፚᑙᔓᙁᝄᡂᥙᩋ᭔᱈ᵎṞὈ⁁⅋∆⍒⑌╆♐❄⠀⥻⩻⭩ⱽ⵲⹶⽆ぱㅳ㈫㌲㑯㔣㙯㜶㠰㥮㩠㭩㰬㵨㹥㽭䁭䄧䈻䌢䑿䔲䙿䜦"), string_0, string_1);
			return this.dbprovider_0.ExecuteScalarString(string_2);
		}

		public string GetCodeText(string string_0, string string_1, string string_2)
		{
			string text = "";
			string string_3 = string.Format(GClass0.smethod_0(".Ĺȷ̿кԬٷܓ࠭ठਸ਼଼వഏ๼཮ါᄾሤጧᑩᔻᘲ᜴ᠳᤡᨺᬦᰤᴴṞὗ⁑ℜ≌⍒⑜╊♒✖⡑⥑⩇⭓ⱘⵜ⹰⽇ぉㄑ㈌㍑㐙㕕㘀㜆㡄㥊㩇㬂㱑㵁㹭㽻䁳䅨䉄䍹䑶䕼䙲䜫䠲䥯䨢䭯䰶䴰乮你偩儬剨卥呭啭嘧圻堢奿娱孿尦"), string_0, string_2, string_1);
			return this.dbprovider_0.ExecuteScalarString(string_3);
		}

		public string GetCodeTextExtend(string string_0, string string_1, int int_0)
		{
			string text = "";
			string string_2 = string.Format(GClass0.smethod_0(":ĭọ̈̄Ц԰٣܇࠹ऴਗ਼୐ౙൣเ༈၄ᄘቑፄᑚᕙᘓᝁᡄ᥂ᩙᭋ᱔ᵈṎ὞⁈⅁≋⌆⑒╌♆❐⡄⤀⩻⭻Ⱪ⵽⹲⽶うㅱ㉳㌫㐲㕯㘣㝯㠶㤰㩮㭠㱩㴬㹨㽥䁭䅭䈧䌻䐢䕿䘲䝿䠦"), string_0, string_1, int_0.ToString());
			return this.dbprovider_0.ExecuteScalarString(string_2);
		}

		public SurveyDetail GetOne(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("Lśɑ͙јՎؙܒࠗॐੇ୛౞ഒ๢ཅၝᅘቈፕᑯᕏᙝᝉᡎ᥊ᨅ᭓᱋ᵇṓὅ‿⅚≸⍨⑺╳♵❇⡞⥲⨨⬳ⱨⴢ⹬⼷〯ㅯ㉣㍨㐫㕉㙦㝬㡢㤻㨢㭿㰲㵿㸦"), string_0, string_1);
			return this.GetBySql(string_2);
		}

		public SurveyDetail GetOne(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format(GClass0.smethod_0("%İȸ̶бԥٰݥ࡮फਾତధ൩ป༲ဴᄳሡጺᐆᔤᘴ᝞ᡗᥑ᨜ᭌ᱒ᵜṊὒ‖⅑≑⍇⑓╘♜❰⡇⥉⨑⬌ⱑⴙ⹕⼀〆ㅄ㉊㍇㐂㕑㙁㝭㡻㥳㩨㭄㱹㵶㹼㽲䀫䄲䉯䌢䑯䔶䘰䝮䡠䥩䨬䭨䱥䵭乭伧倻儢剿匱呿唦"), string_0, string_2, string_1);
			return this.GetBySql(string_3);
		}

		public SurveyDetail GetOneByOrder(string string_0, int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("7ĦȮ̤УՋ؞ܗࠜढ़ੈୖౕഗ๥ཀ၆ᅅ቗ፈᑴᕊᙚᝌᡅ᥇ᨊ᭞᱀ᵂṔὀ\u2004Ⅷ≇⍕⑁╶♲❂⡕⥿⨧⬾Ᵽⴧ⹫⼲〴ㅲ㉼㍵㐰㕆㙀㝃㡉㥙㩕㭆㱚㵃㹃㽗䀹䅸䈳䍼"), string_0, int_0.ToString());
			return this.GetBySql(string_);
		}

		public int GetDetailCount(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("2ĥɓ͛ўՈ؛ݙࡖ्ਖ਼ୂఝഞบ༒ၐᅃሏፀᑮᕃᙞᝄᡝᤈᩁ᭔᱊ᵉḃά⁔⅒≩⍻⑤╘♾❮⡸⥱⩻⬶Ɫ⵼⹶⽠ぴ㄰㉋㍫㑹㕭㙢㝦㡖㥁㩣㬻㰢㵿㸳㽿䀦"), string_0);
			string s = this.dbprovider_0.ExecuteScalarString(string_);
			return int.Parse(s);
		}

		public int GetExtendCount(string string_0, string string_1, int int_0)
		{
			string string_2 = string.Format(GClass0.smethod_0("&ıȿ̷вԤٯܭࠢहਥାౡൢ๮སဤᄷባጬᐢᔯᙊᝐᡉᤜᩝᭈ᱖ᵕḗὅ⁀ⅆ≅⍗⑈╔♊❚⡌⥅⩇⬊ⱞⵀ⹂⽔぀㄄㉇㍇㑕㕁㙶㝲㡂㥵㩿㬧㰾㵣㸧㽫䀲䄴䉲䍼䑵䔰䙪䝶䡹䥩䩥䭮䱖䴹丧伻倢兿刲卿否"), string_0, string_1);
			string s = this.dbprovider_0.ExecuteScalarString(string_2);
			return int.Parse(s);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
