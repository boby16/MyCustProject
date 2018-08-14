using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000015 RID: 21
	public class SurveyWebLogDal
	{
		// Token: 0x0600016C RID: 364 RVA: 0x0000F29C File Offset: 0x0000D49C
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬᭃᱶᵰṝ὿⁨℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public SurveyWebLog GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑃᕶᙰ᝝᡿ᥨᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000F308 File Offset: 0x0000D508
		public SurveyWebLog GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyWebLog surveyWebLog = new SurveyWebLog();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyWebLog.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					surveyWebLog.SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString();
					surveyWebLog.SURVEY_GUID = dataReader[global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
					surveyWebLog.URI_FULL = dataReader[global::GClass0.smethod_0("]ŕɏ͚тՖَݍ")].ToString();
					surveyWebLog.URI_DOMAIN = dataReader[global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ")].ToString();
					surveyWebLog.URI_DOMAIN_TWO = dataReader[global::GClass0.smethod_0("[şɅ͔юՆم݆ࡏोਜ਼ୗౕൎ")].ToString();
					surveyWebLog.BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("HŌɏ͎ш՚ِ݊ࡏॄ")].ToString()));
					surveyWebLog.END_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("Mŉɂ͚ѐՊُ݄")].ToString()));
					surveyWebLog.STAY_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ")]);
				}
			}
			return surveyWebLog;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000F470 File Offset: 0x0000D670
		public List<SurveyWebLog> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyWebLog> list = new List<SurveyWebLog>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyWebLog
					{
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString(),
						SURVEY_GUID = dataReader[global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString(),
						URI_FULL = dataReader[global::GClass0.smethod_0("]ŕɏ͚тՖَݍ")].ToString(),
						URI_DOMAIN = dataReader[global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ")].ToString(),
						URI_DOMAIN_TWO = dataReader[global::GClass0.smethod_0("[şɅ͔юՆم݆ࡏोਜ਼ୗౕൎ")].ToString(),
						BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("HŌɏ͎ш՚ِ݊ࡏॄ")].ToString())),
						END_TIME = new DateTime?(Convert.ToDateTime(dataReader[global::GClass0.smethod_0("Mŉɂ͚ѐՊُ݄")].ToString())),
						STAY_TIME = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ")])
					});
				}
			}
			return list;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		public List<SurveyWebLog> GetList()
		{
			string string_ = global::GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑅᕴᙲᝃᡡᥪᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000F608 File Offset: 0x0000D808
		public void Add(SurveyWebLog surveyWebLog_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("ïǫ˷ϦӰ׵ڀߖ࣐৉૓஻೉෬໪࿡ჳᇬዃ᏶ᓰᗝ᛿៨ᢦ᧞᫙ᯙ᳜᷌ốῘ⃏⇁⊨⏐ⓗ◓⛖✺⠧⤢⨻⬮ⰳⴽ⹔⼢〤ㄼ㈫㌵㐧㔽㘼㝃㠻㤿㨥㬴㰮㴦㸥㼦䀯䄫䉈䌶䐰䔨䘿䜛䠑䤐䨝䬒䰔䴆丌伀候兹刖化吕唘嘞圐堚处威嬎屦崌帆弃怙愑戍挎搇敭易杫桿楤橣歯汳浴湽漞瀖煣牵獿瑧畴癣眇砉祖稜策簍紅縏罜耗腘舃茏萅蕚蘒蝢蠹褱註譠谩赤踿輻週酮鈠鍮鐵锽阷靴頻饰騫鬧鰭鵲鸾齺ꀡꄩꉿꌴꑿꔨ"), new object[]
			{
				surveyWebLog_0.SURVEY_ID,
				surveyWebLog_0.SURVEY_GUID,
				surveyWebLog_0.URI_FULL,
				surveyWebLog_0.URI_DOMAIN,
				surveyWebLog_0.URI_DOMAIN_TWO,
				surveyWebLog_0.BEGIN_TIME,
				surveyWebLog_0.END_TIME,
				surveyWebLog_0.STAY_TIME
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000F690 File Offset: 0x0000D890
		public void Update(SurveyWebLog surveyWebLog_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("îǪ˽Ϲӣ׳ڕߧࣆীે௕ೖ෹່࿎ყᇅዎᎈᓴᗣᛱងᣰ᧷᫳᯶᳚᷇Ể῕⃟↺⊤⎸⒰◭⚤⟩⢴⦾⫂⯅ⳝⷘ⻈⿕ピ㇍㋜㏁㓃㖦㚸㞤㢤㧹㪳㯽㱘㵒㸨㼮䀲䄥䈿䌭䐻䔺䙕䝉䡓䥕䨊䭃䰒䵉乁伹倹儣制匬吨唫嘤圭堭奂婜孀屸崥幩弡恼慶戌挊搞攉昑望栞椓樘欞氐洚渚漃火煷物獯琼畳瘸督硯礀稄笇籶絰繢罨聲腷艼茘萊蔖蘒蝏蠅襏訖謜豪赠蹩轳避酣鉤鍭鐇锛阅霃願餕驜鬇鰳鵍鹉齝ꁂꅅꉍꍑꑚꕓ꘵ꜩ꠳ꥩꨩ꭭갯굙깅꽉끙녏눩덁둃딦똸뜤롸뤲멼"), new object[]
			{
				surveyWebLog_0.ID,
				surveyWebLog_0.SURVEY_ID,
				surveyWebLog_0.SURVEY_GUID,
				surveyWebLog_0.URI_FULL,
				surveyWebLog_0.URI_DOMAIN,
				surveyWebLog_0.URI_DOMAIN_TWO,
				surveyWebLog_0.BEGIN_TIME,
				surveyWebLog_0.END_TIME,
				surveyWebLog_0.STAY_TIME
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000F728 File Offset: 0x0000D928
		public void Delete(SurveyWebLog surveyWebLog_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬቃ፶ᑰᕝᙿᝨᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyWebLog_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000F760 File Offset: 0x0000D960
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾቑ፠ᑦᕏ᙭ᝦ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000F788 File Offset: 0x0000D988
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 9;
			string[] array = new string[9];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("臮厫純僶");
				array[1] = global::GClass0.smethod_0("闪剴純僶");
				array[2] = global::GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
				array[3] = global::GClass0.smethod_0("厙壎絕魶匲剁");
				array[4] = global::GClass0.smethod_0("丄群嗝圌");
				array[5] = global::GClass0.smethod_0("予群嗝圌");
				array[6] = global::GClass0.smethod_0("弄壈柴雵");
				array[7] = global::GClass0.smethod_0("绗晜柴雵");
				array[8] = global::GClass0.smethod_0("偛瑟柳陻У糐捱");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
				array[2] = global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
				array[3] = global::GClass0.smethod_0("]ŕɏ͚тՖَݍ");
				array[4] = global::GClass0.smethod_0("_śɁ͘тՊى݂ࡋॏ");
				array[5] = global::GClass0.smethod_0("[şɅ͔юՆم݆ࡏोਜ਼ୗౕൎ");
				array[6] = global::GClass0.smethod_0("HŌɏ͎ш՚ِ݊ࡏॄ");
				array[7] = global::GClass0.smethod_0("Mŉɂ͚ѐՊُ݄");
				array[8] = global::GClass0.smethod_0("ZŜɆ͟њՐيݏࡄ");
			}
			return array;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000F894 File Offset: 0x0000DA94
		public string[,] ExcelContent(int int_0, List<SurveyWebLog> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyWebLog surveyWebLog in list_0)
			{
				array[num, 0] = surveyWebLog.ID.ToString();
				array[num, 1] = surveyWebLog.SURVEY_ID;
				array[num, 2] = surveyWebLog.SURVEY_GUID;
				array[num, 3] = surveyWebLog.URI_FULL;
				array[num, 4] = surveyWebLog.URI_DOMAIN;
				array[num, 5] = surveyWebLog.URI_DOMAIN_TWO;
				array[num, 6] = surveyWebLog.BEGIN_TIME.ToString();
				array[num, 7] = surveyWebLog.END_TIME.ToString();
				array[num, 8] = surveyWebLog.STAY_TIME.ToString();
				num++;
			}
			return array;
		}

		// Token: 0x0400001C RID: 28
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
