using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyLogDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u007fŮɦͬѫճ؆ݦ࡫ॶ੬୵ఈവื༽ၚᅉቕፔᐸᕄᙣᝧᡢ᥶ᩫ᭝᱿ᵨḮ὚⁄ⅎ≘⍌␨╎♂✥⠹⥸⨲⭼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyLog GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("všɯͧѢմؿܴ࠽ग़੉୕౔സไལၧᅢቶ፫ᑝᕿᙨᜮᡚ᥄ᩎ᭘᱌ᴨṎὂ‥ℹ≸⌲⑼"), int_0);
			return this.GetBySql(string_);
		}

		public SurveyLog GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyLog surveyLog = new SurveyLog();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyLog.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					surveyLog.LOG_TYPE = dataReader[GClass0.smethod_0("DňɁ͚ѐ՚ْ݄")].ToString();
					surveyLog.LOG_MESSAGE = dataReader[GClass0.smethod_0("GŅɎ͗ъՃٖݗࡂॅ੄")].ToString();
					surveyLog.LOG_DATE = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("DňɁ͚рՂٖ݄")].ToString()));
					surveyLog.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyLog.VERSION_ID = dataReader[GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")].ToString();
				}
			}
			return surveyLog;
		}

		public List<SurveyLog> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyLog> list = new List<SurveyLog>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyLog
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						LOG_TYPE = dataReader[GClass0.smethod_0("DňɁ͚ѐ՚ْ݄")].ToString(),
						LOG_MESSAGE = dataReader[GClass0.smethod_0("GŅɎ͗ъՃٖݗࡂॅ੄")].ToString(),
						LOG_DATE = new DateTime?(Convert.ToDateTime(dataReader[GClass0.smethod_0("DňɁ͚рՂٖ݄")].ToString())),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						VERSION_ID = dataReader[GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ")].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyLog> GetList()
		{
			string string_ = GClass0.smethod_0("pŧɭͥќՊؽܶ࠻ड़ੋୗౚശๆཡၡᅤቴ፩ᑃᕡᙪᜬᡄᥘᩍ᭍᱕ᴦṇὝ‣⅋≅");
			return this.GetListBySql(string_);
		}

		public void Add(SurveyLog surveyLog_0)
		{
			string string_ = string.Format(GClass0.smethod_0("&ĠȾ̩йԾىܡࠩलਪୄరഗณ༖်ᄧሑጳᐼᕲᘕ᜗᠐ᤉᨁᬍᰃᴗṽἜ\u2000℉−⌁␎┙☚✉⠀⤃⩩⬈Ⰼⴅ⸞⼄まㅪ㉸㌐㑨㕯㙫㝮㡲㥯㩪㭽㱷㴞㹧㽵䁽䅽䉤䍣䑥䕵䙠䝬䠎䤆䩳䭥䱯䵷乤佳倷儹剦匬呦唽嘵圿塬大婨嬳尿崵幪弢恲愩戡挫摰改晴术栫椡橾欰汾津渨"), new object[]
			{
				surveyLog_0.LOG_TYPE,
				surveyLog_0.LOG_MESSAGE,
				surveyLog_0.LOG_DATE,
				surveyLog_0.SURVEY_ID,
				surveyLog_0.VERSION_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyLog surveyLog_0)
		{
			string string_ = string.Format(GClass0.smethod_0("+ĭȸ̺ЮԼ٘ܤࠃइਂଖఋഽฟ༈၎ᄾሩጿᑊᔥᘧᜠᠹᤱᨽᬳᰧᵁṝ὿⁹Ω≭⌦⑽╵☔✘⠑⤊⨙⬖Ⰱⴂ⸑⼈》ㅭ㉱㍫㑭㔲㙺㜺㡡㥩㨈㬌㰅㴞㸄㽾䁪䅸䈜䌆䐚䔞䙃䜄䡋䤒䨘䭠䱧䵣书佪偷兲剥卯吊唔嘈圀塝夑婙嬄導嵷幥彍恍慔打捕摅敐晜朷栫椵樳歨氧浬渷漯灙煅牉獙瑏甩癁睃砦礸稤筸簲絼"), new object[]
			{
				surveyLog_0.ID,
				surveyLog_0.LOG_TYPE,
				surveyLog_0.LOG_MESSAGE,
				surveyLog_0.LOG_DATE,
				surveyLog_0.SURVEY_ID,
				surveyLog_0.VERSION_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyLog surveyLog_0)
		{
			string string_ = string.Format(GClass0.smethod_0("gŧɭͥы՛ؽݚࡉॕ੔ସౄൣ๧རၶᅫቝ፿ᑨᔮᙚᝄᡎᥘᩌᬨᱎᵂḥἹ⁸Ⅎ≼"), surveyLog_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("Qőɟ͗хՕد݈࡟ृ੆ପౚൽ๵཰ၠᅽ቏፭ᑦ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 6;
			string[] array = new string[6];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("丼鐨ȥ苮嚫稔嗶");
				array[1] = GClass0.smethod_0("旡廔繹咊");
				array[2] = GClass0.smethod_0("旡廔䷣据");
				array[3] = GClass0.smethod_0("旡廔柧搞");
				array[4] = "问卷编号";
				array[5] = GClass0.smethod_0("牌是純僶");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = GClass0.smethod_0("DňɁ͚ѐ՚ْ݄");
				array[2] = GClass0.smethod_0("GŅɎ͗ъՃٖݗࡂॅ੄");
				array[3] = GClass0.smethod_0("DňɁ͚рՂٖ݄");
				array[4] = "SURVEY_ID";
				array[5] = GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyLog> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyLog surveyLog in list_0)
			{
				array[num, 0] = surveyLog.ID.ToString();
				array[num, 1] = surveyLog.LOG_TYPE;
				array[num, 2] = surveyLog.LOG_MESSAGE;
				array[num, 3] = surveyLog.LOG_DATE.ToString();
				array[num, 4] = surveyLog.SURVEY_ID;
				array[num, 5] = surveyLog.VERSION_ID;
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
