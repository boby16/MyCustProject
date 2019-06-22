using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class V_SummaryDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("\u007fŮɦͬѫճ؆ݦ࡫ॶ੬୵ఈവื༽ၚᅉቕፔᐸᕁᙉᝆᡡ᥾᩿᭰ᱢᵶḮ὚⁄ⅎ≘⍌␨╎♂✥⠹⥸⨲⭼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_Summary GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("všɯͧѢմؿܴ࠽ग़੉୕౔സแཉ၆ᅡቾ፿ᑰᕢᙶᜮᡚ᥄ᩎ᭘᱌ᴨṎὂ‥ℹ≸⌲⑼"), int_0);
			return this.GetBySql(string_);
		}

		public V_Summary GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_Summary v_Summary = new V_Summary();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_Summary.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_Summary.SUMMARY_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ")]);
					v_Summary.SUMMARY_TITLE = dataReader[GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ")].ToString();
					v_Summary.CODE = dataReader["CODE"].ToString();
					v_Summary.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					v_Summary.SUMMARY_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄")]);
					v_Summary.QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					v_Summary.DETAIL_ID = dataReader[GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString();
					v_Summary.PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
				}
			}
			return v_Summary;
		}

		public List<V_Summary> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_Summary> list = new List<V_Summary>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_Summary
					{
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SUMMARY_INDEX = Convert.ToInt32(dataReader[GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ")]),
						SUMMARY_TITLE = dataReader[GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ")].ToString(),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						SUMMARY_USE = Convert.ToInt32(dataReader[GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄")]),
						QUESTION_NAME = dataReader[GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						DETAIL_ID = dataReader[GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString(),
						PARENT_CODE = dataReader[GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString()
					});
				}
			}
			return list;
		}

		public List<V_Summary> GetList()
		{
			string string_ = GClass0.smethod_0("~ũɧͯѪռ؇܌ࠅॢੱ୭౬ഀ้ཁ၎ᅩቶ፷ᑸᕪ᙮᜶ᡚ᥆ᩗ᭗᱃ᴰṍὗ‭⅍≅⍙⑞╍♕❙⡊⥖⩇⭇ⱓ");
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 9;
			string[] array = new string[9];
			if (bool_0)
			{
				array[0] = "问卷编号";
				array[1] = GClass0.smethod_0("摜袂缠尔");
				array[2] = GClass0.smethod_0("摜袂樅鮙");
				array[3] = GClass0.smethod_0("缐礄ﴌ硗汊行");
				array[4] = GClass0.smethod_0("缒礂枅搭");
				array[5] = GClass0.smethod_0("昫唥晚誀");
				array[6] = GClass0.smethod_0("闦馟紐僲﬌庝遇");
				array[7] = GClass0.smethod_0("养腓韨鮝戊篅捲摯");
				array[8] = GClass0.smethod_0("爲絸䳡笀");
			}
			else
			{
				array[0] = "SURVEY_ID";
				array[1] = GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ");
				array[2] = GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ");
				array[3] = "CODE";
				array[4] = "CODE_TEXT";
				array[5] = GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄");
				array[6] = GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[7] = GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ");
				array[8] = GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<V_Summary> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_Summary v_Summary in list_0)
			{
				array[num, 0] = v_Summary.SURVEY_ID;
				array[num, 1] = v_Summary.SUMMARY_INDEX.ToString();
				array[num, 2] = v_Summary.SUMMARY_TITLE;
				array[num, 3] = v_Summary.CODE;
				array[num, 4] = v_Summary.CODE_TEXT;
				array[num, 5] = v_Summary.SUMMARY_USE.ToString();
				array[num, 6] = v_Summary.QUESTION_NAME;
				array[num, 7] = v_Summary.DETAIL_ID;
				array[num, 8] = v_Summary.PARENT_CODE;
				num++;
			}
			return array;
		}

		public List<V_Summary> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("qŤɬ͚ѝՉؼݙ࠴ॊ੍୅ీ൐ํཌၛᅕሼጯᑏᔣᙟ᝞ᡇ᥄ᩉ᭕ᱟᵚṍὍ⁆⅄≘⏓ⓞ▼⛒➨⢯⦴⪵⮶Ⲥⶬ⺫⾧セㆥ㊼㎪㓂㗍㚮㟅㢩㦦㪬㮢㳊㷅㻃㿄䃂䆠䊳䏿䒝䖒䚘䞞䢅䦍䪝䮏䲂䷹仴侒僼冂劅厂咃喌嚞垒墕妜媛宂峪工庅忭悓憔抅揬擪旴曳柵棥槷櫹毺泳涙溔濲炜燵狵珻瓯痤盠矴磣秭窄箇糧綋维翢胰臤苮菋蓁藞蛓蟟裟覹諞诅賙跘躴迀郍釕鋵鏩铧闣雩鞫飋駚骨鯆鲦鷉黁鿅ꃖꆡꋊꌰꐷꔳꙜꜨꠏꤋꨎꬒ갏괴긚꼀뀅넔눂덏됯딾뙌뜩롊뤦먦뭇밧뵋븵뼶쀧섲숴쌖쐑씓옃윕젛줔쨝쭷챫쵵츖콽퀃턄툕팜퐚프혃휅\ud815\ud907\uda09\udb0a\udc03\udd65\ude05\udf0d契響﬉ﱵﵰ﹩ｮcųɹ̀ыՎٙܦࠫह੗୅౒൐ๆ༳ၐᅈሰፎᐠᕞᙙᝆᡇ᥈ᩚ᭞᱙ᵌṊ὇⁇⅙"), string_0);
			return this.GetListBySql(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
