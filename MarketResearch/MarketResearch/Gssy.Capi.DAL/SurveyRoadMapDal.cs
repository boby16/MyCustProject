using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyRoadMapDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("cŪɢͨѯտ؊ݪࡧॲ੨ୱఌഉซ༁ၦᅍቑፐᐼᕈᙯᝫᡮᥲᩯᭇᱻᵲṶ὜ⁱⅿ∮⍚⑄╎♘❌⠨⥎⩂⬥ⰹ⵸⸲⽼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyRoadMap GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("zŭɫͣѦհ؃܈ࠁ०੍୑౐഼่཯ၫᅮቲ፯ᑇᕻᙲ᝶ᡜᥱ᩿ᬮᱚᵄṎ὘⁌ℨ≎⍂␥┹♸✲⡼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyRoadMap GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyRoadMap surveyRoadMap = new SurveyRoadMap();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyRoadMap.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyRoadMap.VERSION_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")]);
					surveyRoadMap.PART_NAME = dataReader[GClass0.smethod_0("Yŉɕ͒њՊقݏࡄ")].ToString();
					surveyRoadMap.PAGE_NOTE = dataReader[GClass0.smethod_0("Yŉɀ̓њՊٌݖࡄ")].ToString();
					surveyRoadMap.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					surveyRoadMap.ROUTE_LOGIC = dataReader[GClass0.smethod_0("YŅɜ͜тՙى݋ࡄोੂ")].ToString();
					surveyRoadMap.GROUP_ROUTE_LOGIC = dataReader[GClass0.smethod_0("Vłɀ͛ѝՓٙ݅࡜ड़ੂ୙౉ോไཋ၂")].ToString();
					surveyRoadMap.FORM_NAME = dataReader[GClass0.smethod_0("OŇɕ͋њՊقݏࡄ")].ToString();
					surveyRoadMap.IS_JUMP = Convert.ToInt32(dataReader[GClass0.smethod_0("Nŕɚ͎іՏّ")]);
					surveyRoadMap.NOTE = dataReader[GClass0.smethod_0("JŌɖ̈́")].ToString();
				}
			}
			return surveyRoadMap;
		}

		public List<SurveyRoadMap> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyRoadMap> list = new List<SurveyRoadMap>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyRoadMap
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						VERSION_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")]),
						PART_NAME = dataReader[GClass0.smethod_0("Yŉɕ͒њՊقݏࡄ")].ToString(),
						PAGE_NOTE = dataReader[GClass0.smethod_0("Yŉɀ̓њՊٌݖࡄ")].ToString(),
						PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						ROUTE_LOGIC = dataReader[GClass0.smethod_0("YŅɜ͜тՙى݋ࡄोੂ")].ToString(),
						GROUP_ROUTE_LOGIC = dataReader[GClass0.smethod_0("Vłɀ͛ѝՓٙ݅࡜ड़ੂ୙౉ോไཋ၂")].ToString(),
						FORM_NAME = dataReader[GClass0.smethod_0("OŇɕ͋њՊقݏࡄ")].ToString(),
						IS_JUMP = Convert.ToInt32(dataReader[GClass0.smethod_0("Nŕɚ͎іՏّ")]),
						NOTE = dataReader[GClass0.smethod_0("JŌɖ̈́")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyRoadMap> GetList()
		{
			string string_ = GClass0.smethod_0("tţɩ͡Ѡն؁܊࠿क़੏୓ౖഺ๊཭ၥᅠተ፭ᑁᕽᙰ᝴ᡂ᥯᩽ᬬ᱄ᵘṍὍ⁕Ω≇⍝␣╋♅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyRoadMap surveyRoadMap_0)
		{
			string string_ = string.Format(GClass0.smethod_0("ùǡ˽ϨӾ׿ڊߠࣦ৳૩அ೷ූ໐࿗ჅᇦዌᏲᓽᗿᛗ៸ᣨᦿᫀᯐ᳆᷀ớ῞⃞⇐⋇⏉⒠◛⛋⟛⣜⧘⫈⯄ⳉⷆ⺮⿑チㄸ㈻㌢㐲㔴㘮㜼㡔㤧㨷㬲㰱㴬㸻㼵䁜䄽䈡䌸䐸䔮䘵䜥䠧䤠䨯䬦䱈䴤丰伮倵儏刁匏吓唎嘎圜堇夛娙嬒尝崐幾弗怟愝戃挒搂攊昇朌桤椎樕欚氎洖渏漑灬煱牱獩瑹甒瘚睯硹祻穣筰籧紛繉缁聍脃舉荖萝蕖蘍蜅蠏襜訔識调贏踅轚逓酢鈹錱鐻镠阮靤頿餻騱魮鰡鵮鸵鼽ꀷꅴꈸꍰꐫꔧ꙱Ꜿ꡵꤫ꨡꭾ갼굾긥꼨"), new object[]
			{
				surveyRoadMap_0.VERSION_ID,
				surveyRoadMap_0.PART_NAME,
				surveyRoadMap_0.PAGE_NOTE,
				surveyRoadMap_0.PAGE_ID,
				surveyRoadMap_0.ROUTE_LOGIC,
				surveyRoadMap_0.GROUP_ROUTE_LOGIC,
				surveyRoadMap_0.FORM_NAME,
				surveyRoadMap_0.IS_JUMP,
				surveyRoadMap_0.NOTE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyRoadMap surveyRoadMap_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u0092Ɩʁ΅җևۡޓ࣊ৌો௙ೂ෨໖࿙დᇻዔᏄᒓᗡᛴ៤ᢏ᧸᫨᯾᳸ᷣỦῦ⃸⇯⋡⎄⒞▂⛚➑⣢⦲⫍⯝ⳉⷎ⻆⿖ブ㇛㋐㎴㒮㖲㚶㟫㢽㧳㪪㮠㳛㷋㻎㿍䃘䇈䋊䏐䓆䖢䚼䞠䡘䤅䩎䬁䱜䵖丩伹倰儳刪匽吷啒噌坐塈夕婙嬑屌嵆帻弧怲愲戠挻搯攭昦朩栜楾橠歼汼洡湬漥灰煺爒猆琜甇瘁眏砝礁稘笘簎紕縅缇耀脏舆荤葾蕢虦蜻蠉襃訚謐豽赵蹫轵遨酸鉴鍹鑶锒阌霐須饕騚魑鰌鴆鹠齻ꁸꅬꉰꍩꑳꔂꘜ꜀ꡤꤦꩠꬰ걕굕깍꽝뀷넫눵댳둨딫뙬뜷렯륙멅뭉뱙뵏븩뽁쁃섦숸쌤쑸씲왼"), new object[]
			{
				surveyRoadMap_0.ID,
				surveyRoadMap_0.VERSION_ID,
				surveyRoadMap_0.PART_NAME,
				surveyRoadMap_0.PAGE_NOTE,
				surveyRoadMap_0.PAGE_ID,
				surveyRoadMap_0.ROUTE_LOGIC,
				surveyRoadMap_0.GROUP_ROUTE_LOGIC,
				surveyRoadMap_0.FORM_NAME,
				surveyRoadMap_0.IS_JUMP,
				surveyRoadMap_0.NOTE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyRoadMap surveyRoadMap_0)
		{
			string string_ = string.Format(GClass0.smethod_0("cţɩ͡ѷէ؁ݦࡍ॑੐଼ై൯๫཮ၲᅯቇ፻ᑲᕶᙜ᝱᡿᤮ᩚ᭄ᱎᵘṌἨ⁎⅂∥⌹⑸┲♼"), surveyRoadMap_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("]ŝɛ͓сՑسݔࡃय़ੂମ౞൹๹ོၬᅱቕ፩ᑤᕠᙎᝣᡱ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 10;
			string[] array = new string[10];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = GClass0.smethod_0("跩琴灌搯笔囶");
				array[2] = GClass0.smethod_0("闪剴倄鏩");
				array[3] = GClass0.smethod_0("顶諶搏");
				array[4] = GClass0.smethod_0("顱ģɋͅ");
				array[5] = GClass0.smethod_0("顲賵赩軫焲锹覐");
				array[6] = GClass0.smethod_0("徣犧糃緂喀裷襯霹螐");
				array[7] = GClass0.smethod_0("顱笈岍圌");
				array[8] = GClass0.smethod_0("昪唢凬軱譭");
				array[9] = GClass0.smethod_0("夅淩");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ");
				array[2] = GClass0.smethod_0("Yŉɕ͒њՊقݏࡄ");
				array[3] = GClass0.smethod_0("Yŉɀ̓њՊٌݖࡄ");
				array[4] = GClass0.smethod_0("WŇɂ́ќՋم");
				array[5] = GClass0.smethod_0("YŅɜ͜тՙى݋ࡄोੂ");
				array[6] = GClass0.smethod_0("Vłɀ͛ѝՓٙ݅࡜ड़ੂ୙౉ോไཋ၂");
				array[7] = GClass0.smethod_0("OŇɕ͋њՊقݏࡄ");
				array[8] = GClass0.smethod_0("Nŕɚ͎іՏّ");
				array[9] = GClass0.smethod_0("JŌɖ̈́");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyRoadMap> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyRoadMap surveyRoadMap in list_0)
			{
				array[num, 0] = surveyRoadMap.ID.ToString();
				array[num, 1] = surveyRoadMap.VERSION_ID.ToString();
				array[num, 2] = surveyRoadMap.PART_NAME;
				array[num, 3] = surveyRoadMap.PAGE_NOTE;
				array[num, 4] = surveyRoadMap.PAGE_ID;
				array[num, 5] = surveyRoadMap.ROUTE_LOGIC;
				array[num, 6] = surveyRoadMap.GROUP_ROUTE_LOGIC;
				array[num, 7] = surveyRoadMap.FORM_NAME;
				array[num, 8] = surveyRoadMap.IS_JUMP.ToString();
				array[num, 9] = surveyRoadMap.NOTE;
				num++;
			}
			return array;
		}

		public SurveyRoadMap GetByPageId(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\\ŋɁ͉ш՞؉܂ࠇी੗ୋ౎ം๲ཕၭᅨቸ፥ᑉᕵᙸ᝼ᡚ᥷ᩥ᬴ᱤᵺṴὢ⁪℮≝⍍⑌╏♖❁⡃⤻⨢⭿ⰳ⵿⸦"), string_0);
			return this.GetBySql(string_);
		}

		public SurveyRoadMap GetByPageId(string string_0, string string_1)
		{
			string string_2 = string.Format(GClass0.smethod_0("1ĤȬ͚ѝՉ؜ܑࠚय़੊୘౛ക๧ཆ၀ᅇቕፖᑼᕂᙍᝏᡧ᥈ᩘᬇ᱑ᵍṁὑ⁇℁≰⍞⑙╘♃❒⡞⤤⨿⭬Ⱖ⵨⸳⼳びㅿ㉴㌯㑘㕈㙞㝘㡃㥆㩆㭘㱏㵁㸹㽸䀳䅼"), string_0, string_1);
			if (string_0.IndexOf(GClass0.smethod_0("ZŢɢͬѶզ١ݵ")) > -1)
			{
				string_2 = string.Format(GClass0.smethod_0("\\ŋɁ͉ш՞؉܂ࠇी੗ୋ౎ം๲ཕၭᅨቸ፥ᑉᕵᙸ᝼ᡚ᥷ᩥ᬴ᱤᵺṴὢ⁪℮≝⍍⑌╏♖❁⡃⤻⨢⭿ⰳ⵿⸦"), string_0);
			}
			return this.GetBySql(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
