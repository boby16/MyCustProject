﻿using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyOptionDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("|ūɡͩѨվ؉ݫࡨॳ੫୰ఋഈจༀၙᅌቒፑᐻᕉᙬᝪᡡᥳᩬ᭛ᱣᵦṸ὿⁡℮≚⍄⑎╘♌✨⡎⥂⨥⬹ⱸⴲ⹼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyOption GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("{Ţɪ͠ѧշ؂܋ࠀख़ੌ୒౑഻้ཬၪᅡታ፬ᑛᕣᙦ᝸᡿ᥡᨮ᭚᱄ᵎṘὌ\u2028ⅎ≂⌥␹╸☲❼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyOption GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyOption surveyOption = new SurveyOption();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyOption.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyOption.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyOption.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					surveyOption.CODE = dataReader["CODE"].ToString();
					surveyOption.RANDOM_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("^ŊɄ͍чՊٙ݌ࡊेੇ୙")]);
					surveyOption.SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString();
				}
			}
			return surveyOption;
		}

		public List<SurveyOption> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyOption> list = new List<SurveyOption>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyOption
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						CODE = dataReader["CODE"].ToString(),
						RANDOM_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("^ŊɄ͍чՊٙ݌ࡊेੇ୙")]),
						SURVEY_GUID = dataReader[GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyOption> GetList()
		{
			string string_ = GClass0.smethod_0("uŠɨͦѡյ؀ܵ࠾ज़੎୔౗ഹ๋རၤᅣቱ፪ᑝᕡᙤᝦᡡᥣᨬ᭄᱘ᵍṍὕ…ⅇ≝⌣⑋╅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyOption surveyOption_0)
		{
			string string_ = string.Format(GClass0.smethod_0(":ļȢ̵нԺٍܥࠥाਦୈఴഓท༒ဆᄛሮጐᐫᔷᘲᜲᡳᤉᨌᬊᰁᴓḌἋ‚№≽⌁␚┋☞✘⠂⤅⨇⬗Ⰹⴇ⸈⼁は㄁㈎㌄㑺㔒㙯㝽㡵㥾㩶㭵㱨㵿㹻㽰䁶䅪䈝䍣䑺䕼䙻䝩䡲䥵䩮䭽䱮䵢丌伄偵兣剭卵呚啍嘵圻塠太婤嬿尻崱幮弥恮愵戽挷摴攼晰末栧楱樺歵氫洡湾漰灾焥爨"), new object[]
			{
				surveyOption_0.SURVEY_ID,
				surveyOption_0.QUESTION_NAME,
				surveyOption_0.CODE,
				surveyOption_0.RANDOM_INDEX,
				surveyOption_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyOption surveyOption_0)
		{
			string string_ = string.Format(GClass0.smethod_0("×Ǒ˄̾ЪԸٜܨࠏऋ਎଒ఏഺค༇ရᄞሞፏᐽᔨᘸᝋᠹ᤼ᨺᬱᰣᴼḻἪ…⅁≝⍿⑹┦♭✦⡽⥵⨉⬂Ⱃⴆ⸀⼚〝ㄟ㈏㌁㐏㔀㘉㝫㡷㥩㩯㬼㱴㴸㹣㽯䀁䄎䈄䍺䐞䔀䘜䜜䡁䤊䩅䬐䰚䵧乵佽偶兾剽印呧啣器坮塲変娕嬇屝崑幙式恱慴扲捉摛敄晃杜桏楐橜欷氫洵渳潨瀧煬爷猯瑙畅癉睙硏礩穁筃簦紸縤罸耲腼"), new object[]
			{
				surveyOption_0.ID,
				surveyOption_0.SURVEY_ID,
				surveyOption_0.QUESTION_NAME,
				surveyOption_0.CODE,
				surveyOption_0.RANDOM_INDEX,
				surveyOption_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyOption surveyOption_0)
		{
			string string_ = string.Format(GClass0.smethod_0("bŠɨͦѶդ؀ݙࡌ॒ੑ଻౉൬๪ཡၳᅬቛ፣ᑦᕸᙿᝡᠮᥚᩄ᭎᱘ᵌḨ὎⁂℥∹⍸␲╼"), surveyOption_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("\\Œɚ͐рՖزݗࡂी੃ଭ౟ൾ๸ཿၭᅾ቉፵ᑰᕪ᙭ᝯ");
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
				array[2] = GClass0.smethod_0("闪馛純僶");
				array[3] = "编码";
				array[4] = GClass0.smethod_0("巵媊怕疀銌戸捱");
				array[5] = GClass0.smethod_0("MŜɁ̓Ц呭爇刬䘂焀");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = "SURVEY_ID";
				array[2] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[3] = "CODE";
				array[4] = GClass0.smethod_0("^ŊɄ͍чՊٙ݌ࡊेੇ୙");
				array[5] = GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyOption> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyOption surveyOption in list_0)
			{
				array[num, 0] = surveyOption.ID.ToString();
				array[num, 1] = surveyOption.SURVEY_ID;
				array[num, 2] = surveyOption.QUESTION_NAME;
				array[num, 3] = surveyOption.CODE;
				array[num, 4] = surveyOption.RANDOM_INDEX.ToString();
				array[num, 5] = surveyOption.SURVEY_GUID;
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
