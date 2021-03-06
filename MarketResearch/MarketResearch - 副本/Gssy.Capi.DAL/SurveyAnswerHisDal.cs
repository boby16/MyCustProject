﻿using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyAnswerHisDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("aŴɼͪѭչ،ݨࡥॼ੦୳ఎഏญ༃ၤᅳቯፒᐾᕎᙩᝩᡬ᥼ᩡ᭖ᱸᵦṣὶ⁠⅙≹⍼␮╚♄❎⡘⥌⨨⭎ⱂⴥ⸹⽸〲ㅼ"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyAnswerHis GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("xůɥͭѤղ؅܎ࠃ।ੳ୯౒ാ๎ཀྵၩᅬቼ፡ᑖᕸᙦᝣᡶᥠᩙ᭹ᱼᴮṚὄ⁎⅘≌⌨⑎╂☥✹⡸⤲⩼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyAnswerHis GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyAnswerHis surveyAnswerHis = new SurveyAnswerHis();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyAnswerHis.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyAnswerHis.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyAnswerHis.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					surveyAnswerHis.CODE = dataReader["CODE"].ToString();
					surveyAnswerHis.MULTI_ORDER = Convert.ToInt32(dataReader[GClass0.smethod_0("FşɅ͜юՙيݖࡇे੓")]);
					surveyAnswerHis.MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("FŅɍ́с՟ٚ݀ࡂॖ੄")].ToString()));
					surveyAnswerHis.SEQUENCE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅")]);
					surveyAnswerHis.SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
					surveyAnswerHis.BEGIN_DATE = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("HŌɏ͎ш՚ـ݂ࡖॄ")].ToString()));
					surveyAnswerHis.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					surveyAnswerHis.OP_TYPE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Eřɗ͓џՕفݜࡋॅ")]);
				}
			}
			return surveyAnswerHis;
		}

		public List<SurveyAnswerHis> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyAnswerHis> list = new List<SurveyAnswerHis>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyAnswerHis
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
						PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						OP_TYPE_ID = Convert.ToInt32(dataReader[GClass0.smethod_0("Eřɗ͓џՕفݜࡋॅ")])
					});
				}
			}
			return list;
		}

		public List<SurveyAnswerHis> GetList()
		{
			string string_ = GClass0.smethod_0("zŭɫͣѦհ؃܈ࠁ०੍୑౐഼่཯ၫᅮቲ፯ᑔᕺᙠᝥᡴᥢᩇ᭧᱾ᴬṄ὘⁍⅍≕⌦⑇╝☣❋⡅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyAnswerHis surveyAnswerHis_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u008cƊʐ·ғ֔ڟ߷ࣳ২૴ச೪෍໅࿀აᇍዲᏜᓂᗇᛊៜᣥᧅ᫘ᮂᳺ᷽ỵ῰⃠⇽⋼⏫ⓥ▌⛎⟋⣘⧏⫏⯓Ⳗⷖ⻈⿘ピ㇙㋖㎾㓒㗟㛋㟋㢡㧁㫞㯆㳝㷁㻘㿉䃗䇀䋆䏐䒭䗍䘰䜺䠴䤺䨢䬥䰽䴹丣伳偙儧制匣吤唵嘡圭堨夳娢嬮居崻帲弴怳愡戺挽搦攵昖朚桱椞樞欝氐洖済漒瀔焀爖獾琁甑瘈看砒礅稏筦簆紘縘缒耜脔舆茝萈蔄蘖蜞衫襽詷譯豼赫踟輑過鄄鉎錕鐝锗陔霟顐餋騇鬍鱒鴚鹚鼁ꀉꅟꈐꍟꐍꔇꙤꜪꡠꤻ꨷ꭡ갬굥긻꼱끮넢뉮댵됽딷뙴뜹롰뤫먧묭뱲봰빺뼡쀩셿숺썿쐨"), new object[]
			{
				surveyAnswerHis_0.SURVEY_ID,
				surveyAnswerHis_0.QUESTION_NAME,
				surveyAnswerHis_0.CODE,
				surveyAnswerHis_0.MULTI_ORDER,
				surveyAnswerHis_0.MODIFY_DATE,
				surveyAnswerHis_0.SEQUENCE_ID,
				surveyAnswerHis_0.SURVEY_GUID,
				surveyAnswerHis_0.BEGIN_DATE,
				surveyAnswerHis_0.PAGE_ID,
				surveyAnswerHis_0.OP_TYPE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyAnswerHis surveyAnswerHis_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u008aƎʙΝҏ֟۹ދࢢতણறಪඓ຿ྣႸᆫ኿ᎄᒢᖹᛩលᢂᦒ᫥ᮗᲖᶐẗᾅ⃦⇡⋴⏸⒛▇⚙➟⣌⦇⫈⮓ⲟⷣ⻤⿵ーㇺ㋤㏣㓥㗵㛧㟩㣪㧣㪅㮙㲃㶅㻚㾒䃢䆹䊱䏟䓔䗞䛜䞸䢪䦶䪲䯯䲠䷯亶侼僂凛勁变哂嗕囆埚壃姃嫗室岾嶢建徴怂慒戰挳搿攳昿朡栨椲樴欠氶浒湌潐灈焕牘猑瑌畆瘺眭砶礳稠笪簠紧績缩耛腾艠荼萠蕬蘤蝴蠄褃訇謂谖贋踎輗通鄇鈉鍬鑶镪陮霳顰餻驢魨鰁鴇鸆鼉ꁱꅡꉹꍽꑯꕿꘙ꜅ꠗꤑ꩎ꬌ걎괕긝꽠끮녩뉨덳둢땮똉뜕렇뤁멞묝뱞봅븍뽯쁏셁쉉썅쑋앟왆응졓줶쨨쬴챨촣측콭퀯텙퉅퍉푙핏혩흁\ud843\ud926\uda38\udb24\udc78\udd32\ude7c"), new object[]
			{
				surveyAnswerHis_0.ID,
				surveyAnswerHis_0.SURVEY_ID,
				surveyAnswerHis_0.QUESTION_NAME,
				surveyAnswerHis_0.CODE,
				surveyAnswerHis_0.MULTI_ORDER,
				surveyAnswerHis_0.MODIFY_DATE,
				surveyAnswerHis_0.SEQUENCE_ID,
				surveyAnswerHis_0.SURVEY_GUID,
				surveyAnswerHis_0.BEGIN_DATE,
				surveyAnswerHis_0.PAGE_ID,
				surveyAnswerHis_0.OP_TYPE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyAnswerHis surveyAnswerHis_0)
		{
			string string_ = string.Format(GClass0.smethod_0("mŭɫͣѱա؃ݤࡳ९੒ା౎൩๩ཬၼᅡቖ፸ᑦᕣᙶᝠᡙ᥹᩼ᬮᱚᵄṎ὘⁌ℨ≎⍂␥┹♸✲⡼"), surveyAnswerHis_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("_şɕ͝уՓصݒࡁढ़ੜର౜ൻ๿ེၮᅳቈ፦ᑴᕱᙠ᝶ᡋᥫᩲ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 11;
			string[] array = new string[11];
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
				array[10] = GClass0.smethod_0("夎俵杷恨犁應䥟筹徊");
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
				array[10] = GClass0.smethod_0("Eřɗ͓џՕفݜࡋॅ");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyAnswerHis> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyAnswerHis surveyAnswerHis in list_0)
			{
				array[num, 0] = surveyAnswerHis.ID.ToString();
				array[num, 1] = surveyAnswerHis.SURVEY_ID;
				array[num, 2] = surveyAnswerHis.QUESTION_NAME;
				array[num, 3] = surveyAnswerHis.CODE;
				array[num, 4] = surveyAnswerHis.MULTI_ORDER.ToString();
				array[num, 5] = surveyAnswerHis.MODIFY_DATE.ToString();
				array[num, 6] = surveyAnswerHis.SEQUENCE_ID.ToString();
				array[num, 7] = surveyAnswerHis.SURVEY_GUID;
				array[num, 8] = surveyAnswerHis.BEGIN_DATE.ToString();
				array[num, 9] = surveyAnswerHis.PAGE_ID;
				array[num, 10] = surveyAnswerHis.OP_TYPE_ID.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
