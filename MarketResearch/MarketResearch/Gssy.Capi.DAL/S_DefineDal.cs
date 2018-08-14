using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000016 RID: 22
	public class S_DefineDal
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("xůɥͭѤղ؅ݧ࡬ॷ੯୴షഴิ༼ၝᅈቖፕᐷᕅᙊᝐᡶᥴ᩸᭾ᱪᴮṚὄ⁎⅘≌⌨⑎╂☥✹⡸⤲⩼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		public S_Define GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("wŦɮͤѣՋؾܷ࠼ढ़ੈୖౕഷๅཊၐᅶቴ፸ᑾᕪᘮ᝚ᡄ᥎ᩘᭌᰨᵎṂἥ‹ⅸ∲⍼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public S_Define GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			S_Define s_Define = new S_Define();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					s_Define.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					s_Define.ANSWER_ORDER = Convert.ToInt32(dataReader[global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]);
					s_Define.PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					s_Define.QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					s_Define.QNAME_MAPPING = dataReader[global::GClass0.smethod_0("\\łɊ͇ь՗ي݇ࡕ॔੊ୌె")].ToString();
					s_Define.QUESTION_TYPE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]);
					s_Define.QUESTION_TITLE = dataReader[global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString();
					s_Define.DETAIL_ID = dataReader[global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString();
					s_Define.PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
					s_Define.QUESTION_USE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ")]);
					s_Define.ANSWER_USE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ")]);
					s_Define.COMBINE_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ")]);
					s_Define.SPSS_TITLE = dataReader[global::GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ")].ToString();
					s_Define.SPSS_CASE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ")]);
					s_Define.SPSS_VARIABLE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]);
					s_Define.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader[global::GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ")]);
					s_Define.SUMMARY_USE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄")]);
					s_Define.SUMMARY_TITLE = dataReader[global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ")].ToString();
					s_Define.SUMMARY_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ")]);
					s_Define.TEST_FIX_ANSWER = dataReader[global::GClass0.smethod_0("[ŋɞ͘єՌـݐࡘेੋୗ౔േ๓")].ToString();
				}
			}
			return s_Define;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000FC94 File Offset: 0x0000DE94
		public List<S_Define> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<S_Define> list = new List<S_Define>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new S_Define
					{
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						ANSWER_ORDER = Convert.ToInt32(dataReader[global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]),
						PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						QNAME_MAPPING = dataReader[global::GClass0.smethod_0("\\łɊ͇ь՗ي݇ࡕ॔੊ୌె")].ToString(),
						QUESTION_TYPE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]),
						QUESTION_TITLE = dataReader[global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString(),
						DETAIL_ID = dataReader[global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString(),
						PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString(),
						QUESTION_USE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ")]),
						ANSWER_USE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ")]),
						COMBINE_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ")]),
						SPSS_TITLE = dataReader[global::GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ")].ToString(),
						SPSS_CASE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ")]),
						SPSS_VARIABLE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]),
						SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader[global::GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ")]),
						SUMMARY_USE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄")]),
						SUMMARY_TITLE = dataReader[global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ")].ToString(),
						SUMMARY_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ")]),
						TEST_FIX_ANSWER = dataReader[global::GClass0.smethod_0("[ŋɞ͘єՌـݐࡘेੋୗ౔േ๓")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000FF20 File Offset: 0x0000E120
		public List<S_Define> GetList()
		{
			string string_ = global::GClass0.smethod_0("qŤɬ͚ѝՉؼܱ࠺य़੊୘౛വ็ཌၖᅴቶ፦ᑠᕨᘬᝄᡘ᥍ᩍ᭕ᰦᵇṝἣ⁋ⅅ");
			return this.GetListBySql(string_);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000FF44 File Offset: 0x0000E144
		public void Add(S_Define s_Define_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("4ĲȨ̿ЫԬٗܿ࠻ठ਼୒ఢയห་ဋᄅህጏᑁᔩᘩ᜵ᠲᤡᨱᬽᰮᴲḛἛ‏ⅰ∋⌛␞┝☈✟⠑⥸⨂⬇Ⱄⴃ⸛⼇。㄂㈔㌄㐈㔅㘂㝪㠔㤊㨂㬏㰄㴟㹲㽿䁭䅬䉲䍴䑾䔔䙦䝣䡰䥧䩧䭻䱾䵾买佺側兼剮匆呸啽噢坵塱奭婬孬屾嵴幖彊恑慙户捞摜敌晖束桙楋橚歖氽浀湎潜灈煂牟獕瑊畇癃睃砩祕穖筇籒絔纶羱肳膣芮莩蒼藔蚶螸袦覣誶讠貮趥躼辫郁醯銤鎧钫閡隩鞣颺馭骭鮦鲤鶸黳龍ꂍꆏꊈꎅ꒍ꖑꚃꞚꢐ꧸ꪀꮂ겂궃꺐꾍낌놟늎돦뒚떘뚔랕뢚릒몂뮐번북뻽뿲샸손싨쏪쓪엫웨쟦죧짽쫽쯦쳮췴컪쿭탤퇡틪폣퓥햄훴ퟳ\ud8e8\ud9e9\udae2\udbf0\udcf8\uddff\udeca\udfcd流難﯇ﳙﶬ︫［.ĨȤ̼аԠبܷ࠻धਤଷణ൙๏༸ာᄠሾጯᐺᕀᘜ᝖᠘᥈ᩄᬙ᱐ᴝṸὲ⁺℧≩⌧⑾╴♰✭⡦⤩⩴⭾Ⱚⵤ⸲⽢なㄷ㉾㌷㑮㕤㙠㜽㡳㤹㩤㭮㱦㴻㸈㽃䀚䄐䉀䌂䑄䔔䙌䜏䡈䤘䩈䬃䰁䵍七伉偖儝刚南后唄噜圗堗奙娏孙尐崓幢弲恦愭戯捧搵散昦朣桨椸樴歩氠洦湲漩瀡煷爺猽瑴甤瘠睽破礼穾笥簨"), new object[]
			{
				s_Define_0.ANSWER_ORDER,
				s_Define_0.PAGE_ID,
				s_Define_0.QUESTION_NAME,
				s_Define_0.QNAME_MAPPING,
				s_Define_0.QUESTION_TYPE,
				s_Define_0.QUESTION_TITLE,
				s_Define_0.DETAIL_ID,
				s_Define_0.PARENT_CODE,
				s_Define_0.QUESTION_USE,
				s_Define_0.ANSWER_USE,
				s_Define_0.COMBINE_INDEX,
				s_Define_0.SPSS_TITLE,
				s_Define_0.SPSS_CASE,
				s_Define_0.SPSS_VARIABLE,
				s_Define_0.SPSS_PRINT_DECIMAIL,
				s_Define_0.SUMMARY_USE,
				s_Define_0.SUMMARY_TITLE,
				s_Define_0.SUMMARY_INDEX,
				s_Define_0.TEST_FIX_ANSWER
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0001005C File Offset: 0x0000E25C
		public void Update(S_Define s_Define_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("üǸˣϧӱסڃ߱ࣾ৤ૺ௸೴ෲ໾ྺ჊ᇝዃᎶᓔᗚᛀៅᣔᧂ᫐ᯁ᳟᷈ỎῘ₩↵⊧⏽⒴◹⚯⟒⣀⧇⨺⬡ⰴⴸ⹛⽇すㅟ㈌㍄㐈㕓㙟㜣㠤㤵㨼㬺㰤㴣㸥㼵䀧䄩䈪䌣䑅䕙䙃䝅䠚䥓䨢䭹䱱䴍丕伛倔儝刈匛吔唄嘃圛堟夗婯孳屭嵫帰彾怴慯扫挗搐攁昐朖栈椏橱歡汩浥湫潿瀙焅爗獍琀畉瘟督硤祵穼筺籤絣繥罵聽腡艳荪葠蔄蘞蜂蠆襛訩譣谺贰蹟轟遍酙鉞鍚鑊镝陗露頬餰騨魵鰺鵱鸬鼦ꁙꅉꉕꍃꑋꕐꙜꝁꡎꥄꪺꯞ곀규껜꾁냁놅닐돚뒤떡뚶랡뢥릹몠뮠벲붹뺸뾯색쇕싇쎝쓜얙웏잣좯즳쪈쮛첏춃캎쾉킜퇸틪폶풮헥훣힯\ud8fd\ud993\uda80\udb83\udc8f\udd85\ude85\udf8f轢摒ﮉﳖﶝﺙￗ\u008eƄ˴϶Ӷ׷ۼߡ࣠৳૚ாಠ඼໠ྫႪᇥኻᏅᓅᗇᛀ៍ᣇ᧑᫝ᯇ᳌᷎ệ῏₩↵⊧⏽⒴▰⛾➮⣒⧐⨬⬭Ⱒ⴬⸩⼳〷ㄬ㈨㌲㐰㔷㘺㜿㠰㤹㨣㭎㱐㵌㸐㽛䁜䄕䉋䌵䐰䔩䘮䜣䠳䤹䨀䬋䰎䴙乻佧偹儣剦占吨啸嘀圇堜夝娎嬜尔崓帟弃思愄戂捦摸敤晤朹桰楷橂欙民浯湮潷灴煹牥獯瑪畽白睶硴票稏笓簍絗縚缒联脄艳荣葶蕰虼蝤表襸詀譟豓赏蹌轟運鄸鈪錶鐲镯阢霫顬餷騯魙鱅鵉鹙齏ꀩꅁꉃꌦꐸꔤꙸꜲ꡼"), new object[]
			{
				s_Define_0.ID,
				s_Define_0.ANSWER_ORDER,
				s_Define_0.PAGE_ID,
				s_Define_0.QUESTION_NAME,
				s_Define_0.QNAME_MAPPING,
				s_Define_0.QUESTION_TYPE,
				s_Define_0.QUESTION_TITLE,
				s_Define_0.DETAIL_ID,
				s_Define_0.PARENT_CODE,
				s_Define_0.QUESTION_USE,
				s_Define_0.ANSWER_USE,
				s_Define_0.COMBINE_INDEX,
				s_Define_0.SPSS_TITLE,
				s_Define_0.SPSS_CASE,
				s_Define_0.SPSS_VARIABLE,
				s_Define_0.SPSS_PRINT_DECIMAIL,
				s_Define_0.SUMMARY_USE,
				s_Define_0.SUMMARY_TITLE,
				s_Define_0.SUMMARY_INDEX,
				s_Define_0.TEST_FIX_ANSWER
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00010184 File Offset: 0x0000E384
		public void Delete(S_Define s_Define_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("fŤɬ͚ъ՘ؼݝࡈॖ੕ଷ౅ൊ๐ྲྀၴᅸቾ፪ᐮᕚᙄᝎᡘ᥌ᨨ᭎᱂ᴥḹὸ′ⅼ"), s_Define_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000101BC File Offset: 0x0000E3BC
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("PŖɞ͔фՊخ݋࡞ॄੇ଩౛൘โའၢᅪቬ፤");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 20;
			string[] array = new string[20];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("臮厫純僶");
				array[1] = global::GClass0.smethod_0("颞矫趗勹鱸宎");
				array[2] = global::GClass0.smethod_0("顶縔凶");
				array[3] = global::GClass0.smethod_0("闪馛純僶");
				array[4] = global::GClass0.smethod_0("门馝紒僴戢夅");
				array[5] = global::GClass0.smethod_0("丿馛骚咊");
				array[6] = global::GClass0.smethod_0("闪馛骚嵳");
				array[7] = global::GClass0.smethod_0("养腓韨鮝戊篅捲摯");
				array[8] = global::GClass0.smethod_0("爲絸䳡笀");
				array[9] = global::GClass0.smethod_0("辘僰ɌͰѤգ٩鋪邛䙽缩");
				array[10] = global::GClass0.smethod_0("辘僰ɌͰѤգ٩籐恋䙽缩");
				array[11] = global::GClass0.smethod_0("绌唏骞疁彔鶛笠堔");
				array[12] = global::GClass0.smethod_0("TŖɖ͗У鶚塳");
				array[13] = global::GClass0.smethod_0("TŖɖ͗У鶚冊");
				array[14] = global::GClass0.smethod_0("ZŘɔ͕Х囜韌筹徊");
				array[15] = global::GClass0.smethod_0("[ŗɕ͖Ф夌捲䡌");
				array[16] = global::GClass0.smethod_0("昫唥晚誀");
				array[17] = global::GClass0.smethod_0("摜袂樅鮙");
				array[18] = global::GClass0.smethod_0("摜袂缠尔");
				array[19] = global::GClass0.smethod_0("浍諐愃墙罖浉");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓");
				array[2] = global::GClass0.smethod_0("WŇɂ́ќՋم");
				array[3] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[4] = global::GClass0.smethod_0("\\łɊ͇ь՗ي݇ࡕ॔੊ୌె");
				array[5] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ");
				array[6] = global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ");
				array[7] = global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ");
				array[8] = global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
				array[9] = global::GClass0.smethod_0("]Şɏ͚ќՎى݋࡛ॖੑୄ");
				array[10] = global::GClass0.smethod_0("KŇɛ͐у՗ٛݖࡑॄ");
				array[11] = global::GClass0.smethod_0("NŃɆ͈рՆقݙࡌॊੇେౙ");
				array[12] = global::GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ");
				array[13] = global::GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ");
				array[14] = global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ");
				array[15] = global::GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ");
				array[16] = global::GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄");
				array[17] = global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ");
				array[18] = global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ");
				array[19] = global::GClass0.smethod_0("[ŋɞ͘єՌـݐࡘेੋୗ౔േ๓");
			}
			return array;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0001042C File Offset: 0x0000E62C
		public string[,] ExcelContent(int int_0, List<S_Define> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (S_Define s_Define in list_0)
			{
				array[num, 0] = s_Define.ID.ToString();
				array[num, 1] = s_Define.ANSWER_ORDER.ToString();
				array[num, 2] = s_Define.PAGE_ID;
				array[num, 3] = s_Define.QUESTION_NAME;
				array[num, 4] = s_Define.QNAME_MAPPING;
				array[num, 5] = s_Define.QUESTION_TYPE.ToString();
				array[num, 6] = s_Define.QUESTION_TITLE;
				array[num, 7] = s_Define.DETAIL_ID;
				array[num, 8] = s_Define.PARENT_CODE;
				array[num, 9] = s_Define.QUESTION_USE.ToString();
				array[num, 10] = s_Define.ANSWER_USE.ToString();
				array[num, 11] = s_Define.COMBINE_INDEX.ToString();
				array[num, 12] = s_Define.SPSS_TITLE;
				array[num, 13] = s_Define.SPSS_CASE.ToString();
				array[num, 14] = s_Define.SPSS_VARIABLE.ToString();
				array[num, 15] = s_Define.SPSS_PRINT_DECIMAIL.ToString();
				array[num, 16] = s_Define.SUMMARY_USE.ToString();
				array[num, 17] = s_Define.SUMMARY_TITLE;
				array[num, 18] = s_Define.SUMMARY_INDEX.ToString();
				array[num, 19] = s_Define.TEST_FIX_ANSWER;
				num++;
			}
			return array;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00010628 File Offset: 0x0000E828
		public bool ExistsByCode(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u0018ďȅ̍ЄԒ٥܇ࠌगਏଔగഔด༜ၽᅨቶ፵ᐗᕥᙪᝰᡖᥔᩘ᭞᱊ᴎṺὤ⁮ⅸ≬⌈⑶╳♠❷⡷⥫⩮⭮ⱀⵐ⹜⽑ぞㄺ㈤㌿㑬㔦㙨㜳㠳㥳㩿㭴㰯㵏㹃㽟䁜䅏䉛䍗䑒䕕䙀䜤䠾䤢䨰"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00010664 File Offset: 0x0000E864
		public S_Define GetByQName(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("CŊɂ͈я՟؊܃ࠈु੔୊౉ഃ๱ཾၤᅺቸ፴ᑲᕾᘺᝮᡰᥲᩤ᭰ᰴᵂṇὔ⁃⅛≇⍂⑂╔♄❈⡅⥂⨻⬢Ɀⴳ⹿⼦"), string_0);
			return this.GetBySql(string_);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0001068C File Offset: 0x0000E88C
		public List<S_Define> GetListByPageId(string string_0, string string_1)
		{
			string string_2 = string.Format(global::GClass0.smethod_0("(Ŀȵ̽дԢٵݾࡳऴਣିఢ൮พ༓ဏᄯሯጡᐩᔣᙥᜳᠫᤧᨳᬥᰟᵮṼύ⁾Ⅵ≰⍼␊┑♎✄⡎⤕⨑⭑ⱁⵊ⸍⽭づㅹ㉾㍭㑵㕹㙰㝷㡦㤂㨜㬀㰮㴾㹼㽲䁿䄺䉈䍍䑒䕅䙁䝝䡜䥜䩎䭞䱎䵃么伬偧兣剢卭吧唡噾圵塾大娦"), string_0, string_1);
			return this.GetListBySql(string_2);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000106B4 File Offset: 0x0000E8B4
		public string GetQNameByMapping(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("2ĥɓ͛ўՈ؛ݙࡖ्ਖ਼ୂఝഞบ༒ၐᅃሏፀᑮᕃᙞᝄᡝᤈᩁ᭔᱊ᵉḃά⁾Ⅴ≺⍸⑴╲♾✺⡮⥰⩲⭤Ɒⴴ⹂⽜ぐㅝ㉊㍑㑀㕍㙛㝚㡀㥆㩀㬻㰢㵿㸳㽿䀦"), string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			string result;
			if (num == 1)
			{
				string_ = string.Format(global::GClass0.smethod_0("CŊɂ͈я՟؊܃ࠈु੔୊౉ഃ๱ཾၤᅺቸ፴ᑲᕾᘺᝮᡰᥲᩤ᭰ᰴᵂṜὐ⁝⅊≑⍀⑍╛♚❀⡆⥀⨻⬢Ɀⴳ⹿⼦"), string_0);
				S_Define bySql = this.GetBySql(string_);
				result = bySql.QUESTION_NAME;
			}
			else
			{
				result = global::GClass0.smethod_0("");
			}
			return result;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00010718 File Offset: 0x0000E918
		public bool SyncReadToWrite()
		{
			bool result = true;
			try
			{
				List<S_Define> list = new List<S_Define>();
				string string_ = global::GClass0.smethod_0("QńɌͺѽթؼܱ࠺ॿ੪୸౻വ็ཌၖᅴቶ፦ᑠᕨᘬᝤᡸᥭᩭ᭵ᰦᵧṽἣ⁫Ⅵ");
				list = this.GetListBySql(string_);
				string_ = global::GClass0.smethod_0("pŶɾʹѤժخݫࡾ।੧଩౛൘โའၢᅪቬ፤");
				this.dbprovider_1.ExecuteNonQuery(string_);
				foreach (S_Define s_Define_ in list)
				{
					this.AddToWrite(s_Define_);
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000107B0 File Offset: 0x0000E9B0
		public void AddToWrite(S_Define s_Define_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("4ĲȨ̿ЫԬٗܿ࠻ठ਼୒ఢയห་ဋᄅህጏᑁᔩᘩ᜵ᠲᤡᨱᬽᰮᴲḛἛ‏ⅰ∋⌛␞┝☈✟⠑⥸⨂⬇Ⱄⴃ⸛⼇。㄂㈔㌄㐈㔅㘂㝪㠔㤊㨂㬏㰄㴟㹲㽿䁭䅬䉲䍴䑾䔔䙦䝣䡰䥧䩧䭻䱾䵾买佺側兼剮匆呸啽噢坵塱奭婬孬屾嵴幖彊恑慙户捞摜敌晖束桙楋橚歖氽浀湎潜灈煂牟獕瑊畇癃睃砩祕穖筇籒絔纶羱肳膣芮莩蒼藔蚶螸袦覣誶讠貮趥躼辫郁醯銤鎧钫閡隩鞣颺馭骭鮦鲤鶸黳龍ꂍꆏꊈꎅ꒍ꖑꚃꞚꢐ꧸ꪀꮂ겂궃꺐꾍낌놟늎돦뒚떘뚔랕뢚릒몂뮐번북뻽뿲샸손싨쏪쓪엫웨쟦죧짽쫽쯦쳮췴컪쿭탤퇡틪폣퓥햄훴ퟳ\ud8e8\ud9e9\udae2\udbf0\udcf8\uddff\udeca\udfcd流難﯇ﳙﶬ︫［.ĨȤ̼аԠبܷ࠻धਤଷణ൙๏༸ာᄠሾጯᐺᕀᘜ᝖᠘᥈ᩄᬙ᱐ᴝṸὲ⁺℧≩⌧⑾╴♰✭⡦⤩⩴⭾Ⱚⵤ⸲⽢なㄷ㉾㌷㑮㕤㙠㜽㡳㤹㩤㭮㱦㴻㸈㽃䀚䄐䉀䌂䑄䔔䙌䜏䡈䤘䩈䬃䰁䵍七伉偖儝刚南后唄噜圗堗奙娏孙尐崓幢弲恦愭戯捧搵散昦朣桨椸樴歩氠洦湲漩瀡煷爺猽瑴甤瘠睽破礼穾笥簨"), new object[]
			{
				s_Define_0.ANSWER_ORDER,
				s_Define_0.PAGE_ID,
				s_Define_0.QUESTION_NAME,
				s_Define_0.QNAME_MAPPING,
				s_Define_0.QUESTION_TYPE,
				s_Define_0.QUESTION_TITLE,
				s_Define_0.DETAIL_ID,
				s_Define_0.PARENT_CODE,
				s_Define_0.QUESTION_USE,
				s_Define_0.ANSWER_USE,
				s_Define_0.COMBINE_INDEX,
				s_Define_0.SPSS_TITLE,
				s_Define_0.SPSS_CASE,
				s_Define_0.SPSS_VARIABLE,
				s_Define_0.SPSS_PRINT_DECIMAIL,
				s_Define_0.SUMMARY_USE,
				s_Define_0.SUMMARY_TITLE,
				s_Define_0.SUMMARY_INDEX,
				s_Define_0.TEST_FIX_ANSWER
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		// Token: 0x0400001D RID: 29
		private DBProvider dbprovider_0 = new DBProvider(1);

		// Token: 0x0400001E RID: 30
		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
