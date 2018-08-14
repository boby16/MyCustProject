using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x0200000E RID: 14
	public class SurveyQuotaDal
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x0000AF9C File Offset: 0x0000919C
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("}Ũɠͮѩս؈ݤࡩ॰੪୷ఊഋฉ༿ၘᅏቓፖᐺᕊ᙭ᝥᡠᥰᩭᭂᱧᵾṤὮ‮⅚≄⍎⑘╌☨❎⡂⤥⨹⭸ⰲ⵼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000AFDC File Offset: 0x000091DC
		public SurveyQuota GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("tţɩ͡Ѡն؁܊࠿क़੏୓ౖഺ๊཭ၥᅠተ፭ᑂᕧᙾᝤᡮ᤮ᩚ᭄ᱎᵘṌἨ⁎⅂∥⌹⑸┲♼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000B008 File Offset: 0x00009208
		public SurveyQuota GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyQuota surveyQuota = new SurveyQuota();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyQuota.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					surveyQuota.PROJECT_ID = dataReader[global::GClass0.smethod_0("Zśɇ͍уՆِݜࡋॅ")].ToString();
					surveyQuota.CLIENT_ID = dataReader[global::GClass0.smethod_0("JńɎ̓ыՐٜ݋ࡅ")].ToString();
					surveyQuota.PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					surveyQuota.QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					surveyQuota.QUESTION_TITLE = dataReader[global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString();
					surveyQuota.INNER_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Bńɇ͍ѕՙٌ݊ࡇेਖ਼")]);
					surveyQuota.CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString();
					surveyQuota.CODE_TEXT = dataReader[global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ")].ToString();
					surveyQuota.SAMPLE_OVER = Convert.ToInt32(dataReader[global::GClass0.smethod_0("XŋɄ͘ыՃٚ݋ࡕे੓")]);
					surveyQuota.SAMPLE_TARGET = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^ōɆ͚хՍ٘ݒࡄॖ੄େౕ")]);
					surveyQuota.SAMPLE_BACKUP = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^ōɆ͚хՍ݄٘ࡄेੈୗ౑")]);
					surveyQuota.SAMPLE_TOTAL = Convert.ToInt32(dataReader[global::GClass0.smethod_0("_Ŋɇ͙фՂٙݑࡋॗ੃୍")]);
					surveyQuota.SAMPLE_FINISH = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^ōɆ͚хՍ٘݀ࡌॊ੊୑౉")]);
					surveyQuota.SAMPLE_RUNNING = Convert.ToInt32(dataReader[global::GClass0.smethod_0("]ŌɁ͛цՌٗݕࡓो੊୊ౌെ")]);
					surveyQuota.SAMPLE_REAL = Convert.ToInt32(dataReader[global::GClass0.smethod_0("XŋɄ͘ыՃٚݖࡆृ੍")]);
					surveyQuota.IS_FULL = dataReader[global::GClass0.smethod_0("Nŕɚ͂іՎٍ")].ToString();
					surveyQuota.SAMPLE_BALANCE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("]ŌɁ͛цՌٗ݅ࡇॉ੅୍ుൄ")]);
					surveyQuota.MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("FŅɍ́с՟ٚ݀ࡂॖ੄")].ToString()));
				}
			}
			return surveyQuota;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000B274 File Offset: 0x00009474
		public List<SurveyQuota> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyQuota> list = new List<SurveyQuota>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyQuota
					{
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						PROJECT_ID = dataReader[global::GClass0.smethod_0("Zśɇ͍уՆِݜࡋॅ")].ToString(),
						CLIENT_ID = dataReader[global::GClass0.smethod_0("JńɎ̓ыՐٜ݋ࡅ")].ToString(),
						PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						QUESTION_TITLE = dataReader[global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString(),
						INNER_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("Bńɇ͍ѕՙٌ݊ࡇेਖ਼")]),
						CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString(),
						CODE_TEXT = dataReader[global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ")].ToString(),
						SAMPLE_OVER = Convert.ToInt32(dataReader[global::GClass0.smethod_0("XŋɄ͘ыՃٚ݋ࡕे੓")]),
						SAMPLE_TARGET = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^ōɆ͚хՍ٘ݒࡄॖ੄େౕ")]),
						SAMPLE_BACKUP = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^ōɆ͚хՍ݄٘ࡄेੈୗ౑")]),
						SAMPLE_TOTAL = Convert.ToInt32(dataReader[global::GClass0.smethod_0("_Ŋɇ͙фՂٙݑࡋॗ੃୍")]),
						SAMPLE_FINISH = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^ōɆ͚хՍ٘݀ࡌॊ੊୑౉")]),
						SAMPLE_RUNNING = Convert.ToInt32(dataReader[global::GClass0.smethod_0("]ŌɁ͛цՌٗݕࡓो੊୊ౌെ")]),
						SAMPLE_REAL = Convert.ToInt32(dataReader[global::GClass0.smethod_0("XŋɄ͘ыՃٚݖࡆृ੍")]),
						IS_FULL = dataReader[global::GClass0.smethod_0("Nŕɚ͂іՎٍ")].ToString(),
						SAMPLE_BALANCE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("]ŌɁ͛цՌٗ݅ࡇॉ੅୍ుൄ")]),
						MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("FŅɍ́с՟ٚ݀ࡂॖ੄")].ToString()))
					});
				}
			}
			return list;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000B4EC File Offset: 0x000096EC
		public List<SurveyQuota> GetList()
		{
			string string_ = global::GClass0.smethod_0("všɯͧѢմؿܴ࠽ग़੉୕౔സไལၧᅢቶ፫ᑀᕥᙠ᝺ᡬ᤬ᩄ᭘ᱍᵍṕἦ⁇⅝∣⍋⑅");
			return this.GetListBySql(string_);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000B510 File Offset: 0x00009710
		public void Add(SurveyQuota surveyQuota_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u0011ęȅ̐Іԇٲܘࠞछਁ୭టാุ༿ိᄾሗጰᐫᔷᘣᝩ᠐ᥭᩱ᭷ᱹᵸṮὦⁱⅳ√⍶⑸╺♷❿⡤⥰⩧⭩Ⰰ⵻⹫⽮ねㅸ㉯㍡㐈㕲㙷㝤㡳㥋㩗㭒㱒㵄㹔㽘䁕䅒䈺䍄䑁䕖䙁䝅䡙䥀䩀䭒䱘䵂乞佅偍儫剏卋告商噐坞塉妱媺宸岤巗庹徶悼憲拚掶撻斷暷枮梤榪檶殹泀涸溫澤炸熫犣玺璫疵皧瞳磌禌窟箐粌綗纟羆肌膖芄莒蒑薇蛾螂袑覂語讁貉趔躈辈邋醌銓鎕铨閐隃鞌颐駳髻鯢鳨鷴黮鿸ꃴꆛꋥꏴꓹꗣ꛾ꟴ꣯ꧩꫧꯣ곥그껢꾅냻뇦닫돵듨뗦뛽럳룵맑뫐믔볒뷜뺶뿊샙쇚싆쏙쓑엌움쟔죑짃쪢쯄쳟췔컌쿜탄퇋튪폖퓅헎훒ퟍ\ud8c5\ud920\uda3c\udb3c\udc30\udd3a\ude34\udf3a濫啕ﭱﱻﴠ﹫Ｄ\u007fŻɱ̮ѦԮٵݽࡷऴ੽ର౫൧๭༲ၼᄺቡ፩ᐿᕶᘿ᝭ᡧ᥄ᨈᭀᰛᴗḝὂ‏⅊∑⌙⑏┋♏✝⡋⤖⩓⬁ⱗⴚ⸚⽔〄ㅜ㈗㌔㑙㔏㙙㜐㠒㥢㨲㭦㰭㴨㹧㼵䁣䄦䈢䍨䐸䔴䙩䜠䠥䥲䨩䬡䱷䴺丼佴値儠剽匴吳啾嘥在"), new object[]
			{
				surveyQuota_0.PROJECT_ID,
				surveyQuota_0.CLIENT_ID,
				surveyQuota_0.PAGE_ID,
				surveyQuota_0.QUESTION_NAME,
				surveyQuota_0.QUESTION_TITLE,
				surveyQuota_0.INNER_INDEX,
				surveyQuota_0.CODE,
				surveyQuota_0.CODE_TEXT,
				surveyQuota_0.SAMPLE_OVER,
				surveyQuota_0.SAMPLE_TARGET,
				surveyQuota_0.SAMPLE_BACKUP,
				surveyQuota_0.SAMPLE_TOTAL,
				surveyQuota_0.SAMPLE_FINISH,
				surveyQuota_0.SAMPLE_RUNNING,
				surveyQuota_0.SAMPLE_REAL,
				surveyQuota_0.IS_FULL,
				surveyQuota_0.SAMPLE_BALANCE,
				surveyQuota_0.MODIFY_DATE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000B620 File Offset: 0x00009820
		public void Update(SurveyQuota surveyQuota_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("×Ǒ˄̾ЪԸٜܨࠏऋ਎଒ఏതก༜ဆᄐቐጼᐫᔹᙌ᜻ᠸᤦᨢᬢᰥᴱḻἪ…⅁≝⍿⑹┦♭✦⡽⥵⨛⬛Ⱏⴐ⸚⼇」ㄘ㈔㍯㑳㕭㙫㜰㡸㤴㩯㭫㰖㴄㸃㼆䀝䄈䈄䌟䐃䔝䘛䝀䠉䥄䨟䬛䱧䵠乱你偦典剿卡呱啣噭坦塯変娕嬇封嵞帐彞怅愍扱捊摛敎晈杒桕楗橇歃江流湘潖瀲焬爰猨瑵甸癱眬砦祀穆等籃絗繛罊职腅艅莧蓞藀蛜螀裌覄諔讴貹趱躱迓郏金鋗鎔铙閐雋韇颩馦骬鮢鲹鶱麡龻ꂶꇁꋝꏿꓹꖦꛤꞦꣽ꧵ꪋꮖ겛궅꺘꾖낍놞늆뎊뒜뗭뛱럫뢱맰몵믫법분뺉뾓삎솄슟쏫쓿엯웻쟾죮즙쪅쮗쳍춄캄쿎킞퇢틱폢퓾헡훩ퟴ\ud8e8\ud9e8\udaeb\udbec\udcf3\uddf5\ude84\udf9e殮﫛﯆ﳋ﷕ﻈￆÝǇˉ̱зԮشݛࡇख़ਃ୆౅ഈ๘༠ဳᄼሠጣᐫᔲᘾ᜾ᠤᤧᨡᬩᰡᵅṙὃ’⅐≔⌢⑲┎☝✖⠊⤕⨝⬈Ⰴⴐ⸕⼟ひㅬ㉰㌴㑿㕸㘱㝧㠃㤚㨗㬁㰓㴉㸈㽣䁿䅡䉧䍄䐏䔋䙁䜜䠖䥪䩹䭺䱦䵹乱佬偰兰剼卮呠啮噩國堗変婓嬖少嵘师彮恭慥扩捙摇敂晘杚桎楜樸欪氶洲湯漢瀪煬爷猯瑙畅癉睙硏礩穁筃簦紸縤罸耲腼"), new object[]
			{
				surveyQuota_0.ID,
				surveyQuota_0.PROJECT_ID,
				surveyQuota_0.CLIENT_ID,
				surveyQuota_0.PAGE_ID,
				surveyQuota_0.QUESTION_NAME,
				surveyQuota_0.QUESTION_TITLE,
				surveyQuota_0.INNER_INDEX,
				surveyQuota_0.CODE,
				surveyQuota_0.CODE_TEXT,
				surveyQuota_0.SAMPLE_OVER,
				surveyQuota_0.SAMPLE_TARGET,
				surveyQuota_0.SAMPLE_BACKUP,
				surveyQuota_0.SAMPLE_TOTAL,
				surveyQuota_0.SAMPLE_FINISH,
				surveyQuota_0.SAMPLE_RUNNING,
				surveyQuota_0.SAMPLE_REAL,
				surveyQuota_0.IS_FULL,
				surveyQuota_0.SAMPLE_BALANCE,
				surveyQuota_0.MODIFY_DATE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000B740 File Offset: 0x00009940
		public void Delete(SurveyQuota surveyQuota_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("ašɯͧѵեؿݘࡏ॓੖଺ొ൭๥འၰᅭቂ፧ᑾᕤ᙮ᜮᡚ᥄ᩎ᭘᱌ᴨṎὂ‥ℹ≸⌲⑼"), surveyQuota_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000B778 File Offset: 0x00009978
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("Sœə͑ч՗رݖ࡝ुੀବౘൿ๻ཾၢᅿቔ፱ᑬᕶᙠ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000B7A0 File Offset: 0x000099A0
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 19;
			string[] array = new string[19];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("臮厫純僶");
				array[1] = global::GClass0.smethod_0("顼矪ȣ͋х");
				array[2] = global::GClass0.smethod_0("宧挳ȣ͋х");
				array[3] = global::GClass0.smethod_0("顱ģɋͅ");
				array[4] = global::GClass0.smethod_0("酇馔骐僰Ю郫麜砕寵न");
				array[5] = global::GClass0.smethod_0("酉馞樅鮙");
				array[6] = global::GClass0.smethod_0("酉馞驸嶎");
				array[7] = global::GClass0.smethod_0("酋馘愃欄笔紀");
				array[8] = global::GClass0.smethod_0("酅馚愁欂笒紂掅怭");
				array[9] = global::GClass0.smethod_0("酅馚泧疁曢朩掻堎");
				array[10] = global::GClass0.smethod_0("盩椁鍈鮙水戮捱");
				array[11] = global::GClass0.smethod_0("酊馛嬂䷹水戮捱");
				array[12] = global::GClass0.smethod_0("酉馞戹晱");
				array[13] = global::GClass0.smethod_0("宗靍念緉徉朔渴怮浱");
				array[14] = global::GClass0.smethod_0("此嘮覺雪水戮捱");
				array[15] = global::GClass0.smethod_0("官靀攍晋搹恱");
				array[16] = global::GClass0.smethod_0("昫唥忰淠");
				array[17] = global::GClass0.smethod_0("术濥忭鮟慱");
				array[18] = global::GClass0.smethod_0("月唉䷨昼懡戜援鋵");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("Zśɇ͍уՆِݜࡋॅ");
				array[2] = global::GClass0.smethod_0("JńɎ̓ыՐٜ݋ࡅ");
				array[3] = global::GClass0.smethod_0("WŇɂ́ќՋم");
				array[4] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[5] = global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ");
				array[6] = global::GClass0.smethod_0("Bńɇ͍ѕՙٌ݊ࡇेਖ਼");
				array[7] = global::GClass0.smethod_0("GŌɆ̈́");
				array[8] = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
				array[9] = global::GClass0.smethod_0("XŋɄ͘ыՃٚ݋ࡕे੓");
				array[10] = global::GClass0.smethod_0("^ōɆ͚хՍ٘ݒࡄॖ੄େౕ");
				array[11] = global::GClass0.smethod_0("^ōɆ͚хՍ݄٘ࡄेੈୗ౑");
				array[12] = global::GClass0.smethod_0("_Ŋɇ͙фՂٙݑࡋॗ੃୍");
				array[13] = global::GClass0.smethod_0("^ōɆ͚хՍ٘݀ࡌॊ੊୑౉");
				array[14] = global::GClass0.smethod_0("]ŌɁ͛цՌٗݕࡓो੊୊ౌെ");
				array[15] = global::GClass0.smethod_0("XŋɄ͘ыՃٚݖࡆृ੍");
				array[16] = global::GClass0.smethod_0("Nŕɚ͂іՎٍ");
				array[17] = global::GClass0.smethod_0("]ŌɁ͛цՌٗ݅ࡇॉ੅୍ుൄ");
				array[18] = global::GClass0.smethod_0("FŅɍ́с՟ٚ݀ࡂॖ੄");
			}
			return array;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000B9CC File Offset: 0x00009BCC
		public string[,] ExcelContent(int int_0, List<SurveyQuota> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyQuota surveyQuota in list_0)
			{
				array[num, 0] = surveyQuota.ID.ToString();
				array[num, 1] = surveyQuota.PROJECT_ID;
				array[num, 2] = surveyQuota.CLIENT_ID;
				array[num, 3] = surveyQuota.PAGE_ID;
				array[num, 4] = surveyQuota.QUESTION_NAME;
				array[num, 5] = surveyQuota.QUESTION_TITLE;
				array[num, 6] = surveyQuota.INNER_INDEX.ToString();
				array[num, 7] = surveyQuota.CODE;
				array[num, 8] = surveyQuota.CODE_TEXT;
				array[num, 9] = surveyQuota.SAMPLE_OVER.ToString();
				array[num, 10] = surveyQuota.SAMPLE_TARGET.ToString();
				array[num, 11] = surveyQuota.SAMPLE_BACKUP.ToString();
				array[num, 12] = surveyQuota.SAMPLE_TOTAL.ToString();
				array[num, 13] = surveyQuota.SAMPLE_FINISH.ToString();
				array[num, 14] = surveyQuota.SAMPLE_RUNNING.ToString();
				array[num, 15] = surveyQuota.SAMPLE_REAL.ToString();
				array[num, 16] = surveyQuota.IS_FULL;
				array[num, 17] = surveyQuota.SAMPLE_BALANCE.ToString();
				array[num, 18] = surveyQuota.MODIFY_DATE.ToString();
				num++;
			}
			return array;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		public void AddRead(SurveyQuota surveyQuota_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u0011ęȅ̐Іԇٲܘࠞछਁ୭టാุ༿ိᄾሗጰᐫᔷᘣᝩ᠐ᥭᩱ᭷ᱹᵸṮὦⁱⅳ√⍶⑸╺♷❿⡤⥰⩧⭩Ⰰ⵻⹫⽮ねㅸ㉯㍡㐈㕲㙷㝤㡳㥋㩗㭒㱒㵄㹔㽘䁕䅒䈺䍄䑁䕖䙁䝅䡙䥀䩀䭒䱘䵂乞佅偍儫剏卋告商噐坞塉妱媺宸岤巗庹徶悼憲拚掶撻斷暷枮梤榪檶殹泀涸溫澤炸熫犣玺璫疵皧瞳磌禌窟箐粌綗纟羆肌膖芄莒蒑薇蛾螂袑覂語讁貉趔躈辈邋醌銓鎕铨閐隃鞌颐駳髻鯢鳨鷴黮鿸ꃴꆛꋥꏴꓹꗣ꛾ꟴ꣯ꧩꫧꯣ곥그껢꾅냻뇦닫돵듨뗦뛽럳룵맑뫐믔볒뷜뺶뿊샙쇚싆쏙쓑엌움쟔죑짃쪢쯄쳟췔컌쿜탄퇋튪폖퓅헎훒ퟍ\ud8c5\ud920\uda3c\udb3c\udc30\udd3a\ude34\udf3a濫啕ﭱﱻﴠ﹫Ｄ\u007fŻɱ̮ѦԮٵݽࡷऴ੽ର౫൧๭༲ၼᄺቡ፩ᐿᕶᘿ᝭ᡧ᥄ᨈᭀᰛᴗḝὂ‏⅊∑⌙⑏┋♏✝⡋⤖⩓⬁ⱗⴚ⸚⽔〄ㅜ㈗㌔㑙㔏㙙㜐㠒㥢㨲㭦㰭㴨㹧㼵䁣䄦䈢䍨䐸䔴䙩䜠䠥䥲䨩䬡䱷䴺丼佴値儠剽匴吳啾嘥在"), new object[]
			{
				surveyQuota_0.PROJECT_ID,
				surveyQuota_0.CLIENT_ID,
				surveyQuota_0.PAGE_ID,
				surveyQuota_0.QUESTION_NAME,
				surveyQuota_0.QUESTION_TITLE,
				surveyQuota_0.INNER_INDEX,
				surveyQuota_0.CODE,
				surveyQuota_0.CODE_TEXT,
				surveyQuota_0.SAMPLE_OVER,
				surveyQuota_0.SAMPLE_TARGET,
				surveyQuota_0.SAMPLE_BACKUP,
				surveyQuota_0.SAMPLE_TOTAL,
				surveyQuota_0.SAMPLE_FINISH,
				surveyQuota_0.SAMPLE_RUNNING,
				surveyQuota_0.SAMPLE_REAL,
				surveyQuota_0.IS_FULL,
				surveyQuota_0.SAMPLE_BALANCE,
				surveyQuota_0.MODIFY_DATE
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public void AddOne(SurveyQuota surveyQuota_0)
		{
			if (this.Exists(surveyQuota_0.ID))
			{
				this.Update(surveyQuota_0);
			}
			else
			{
				this.Add(surveyQuota_0);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000BCFC File Offset: 0x00009EFC
		public bool ExistsByQName(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("0ħȭ̥ќՊ؝ܖࠛड़ੋୗౚഖ๦ཁ၁ᅄቔፉᑾᕛᙂ᝘ᡊᤊᩞᭀ᱂ᵔṀἄ⁲ⅷ≤⍳⑋╗♒❒⡄⥔⩘⭕ⱒⴶ⸨⼳とㄢ㉬㌷㐯㕯㙣㝨㠫㥉㩆㭌㱂㴻㸢㽿䀲䅿䈦"), string_0, string_1);
			int num = this.dbprovider_0.ExecuteScalarInt(string_2);
			return num > 0;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000BD38 File Offset: 0x00009F38
		public bool ExistsMatchQuota(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format(global::GClass0.smethod_0("$ĳȹ̱аԦٱݺ࡯नਿଣద൪บ༽ဵᄰሠጽᐒᔷᘮ᜴ᡞᤞᩊ᭔ᱞᵈṜἘ⁧ⅷ≲⍱⑬╻♵✐⠒⤎⨊⭗Ⱋⵗ⸎⼈うㅈ㉁㌄㑲㕷㙤㝳㡋㥗㩒㭒㱄㵔㹘㽕䁒䄶䈨䌳䑨䔣䙬䜷䠯䥯䩣䭨䰫䵉乆佌偂儻刢卿吱啿嘦"), string_0, string_1, string_2);
			int num = this.dbprovider_0.ExecuteScalarInt(string_3);
			return num > 0;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000BD78 File Offset: 0x00009F78
		public void UpdateStatus(string string_0, string string_1, int int_0, int int_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("ûǽ˨ϪӾ׬ڨߔࣳ৷૲௦೻ැ໵༐ညᄜቜገᐟᔍᙘᜤᠷᤸᨤᬿ᰷ᴮḶἦ†ℤ∿⌣⑊╔♈✴⠧⤨⨴⬯Ⱗⴾ⸦⼖【ㄔ㈏㌓㑺㕲㙸㜬㡤㤨㩸㭳㰁㴐㸝㼟䀂䄈䈓䌙䐟䔇䘆䜎䠈䤂䩤䭾䱢䴒丁佲偮共剹卤周啬噶坹塿奻婳嬓尟崑幋弜恓愍扛捃摏敛晍朇桵楰橶歵汧浸湿潖灚焽爡猼瑡甩癥眰砶祴空筷簲終繅罝聘腈艕荔葍蕜虁蝃蠻褢詿謲豿账"), new object[]
			{
				string_0,
				string_1,
				int_0.ToString(),
				int_1.ToString()
			});
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		// Token: 0x04000012 RID: 18
		private DBProvider dbprovider_0 = new DBProvider(2);

		// Token: 0x04000013 RID: 19
		private DBProvider dbprovider_1 = new DBProvider(1);
	}
}
