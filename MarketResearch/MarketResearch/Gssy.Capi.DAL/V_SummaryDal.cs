using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x0200001B RID: 27
	public class V_SummaryDal
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00012058 File Offset: 0x00010258
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u007fŮɦͬѫճ؆ݦ࡫ॶ੬୵ఈവื༽ၚᅉቕፔᐸᕁᙉᝆᡡ᥾᩿᭰ᱢᵶḮ὚⁄ⅎ≘⍌␨╎♂✥⠹⥸⨲⭼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00012098 File Offset: 0x00010298
		public V_Summary GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("všɯͧѢմؿܴ࠽ग़੉୕౔സแཉ၆ᅡቾ፿ᑰᕢᙶᜮᡚ᥄ᩎ᭘᱌ᴨṎὂ‥ℹ≸⌲⑼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000120C4 File Offset: 0x000102C4
		public V_Summary GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_Summary v_Summary = new V_Summary();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_Summary.SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString();
					v_Summary.SUMMARY_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ")]);
					v_Summary.SUMMARY_TITLE = dataReader[global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ")].ToString();
					v_Summary.CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString();
					v_Summary.CODE_TEXT = dataReader[global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ")].ToString();
					v_Summary.SUMMARY_USE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄")]);
					v_Summary.QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					v_Summary.DETAIL_ID = dataReader[global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString();
					v_Summary.PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
				}
			}
			return v_Summary;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00012218 File Offset: 0x00010418
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
						SURVEY_ID = dataReader[global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")].ToString(),
						SUMMARY_INDEX = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ")]),
						SUMMARY_TITLE = dataReader[global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ")].ToString(),
						CODE = dataReader[global::GClass0.smethod_0("GŌɆ̈́")].ToString(),
						CODE_TEXT = dataReader[global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ")].ToString(),
						SUMMARY_USE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄")]),
						QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						DETAIL_ID = dataReader[global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString(),
						PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00012378 File Offset: 0x00010578
		public List<V_Summary> GetList()
		{
			string string_ = global::GClass0.smethod_0("~ũɧͯѪռ؇܌ࠅॢੱ୭౬ഀ้ཁ၎ᅩቶ፷ᑸᕪ᙮᜶ᡚ᥆ᩗ᭗᱃ᴰṍὗ‭⅍≅⍙⑞╍♕❙⡊⥖⩇⭇ⱓ");
			return this.GetListBySql(string_);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001239C File Offset: 0x0001059C
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 9;
			string[] array = new string[9];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("闪剴純僶");
				array[1] = global::GClass0.smethod_0("摜袂缠尔");
				array[2] = global::GClass0.smethod_0("摜袂樅鮙");
				array[3] = global::GClass0.smethod_0("缐礄ﴌ硗汊行");
				array[4] = global::GClass0.smethod_0("缒礂枅搭");
				array[5] = global::GClass0.smethod_0("昫唥晚誀");
				array[6] = global::GClass0.smethod_0("闦馟紐僲﬌庝遇");
				array[7] = global::GClass0.smethod_0("养腓韨鮝戊篅捲摯");
				array[8] = global::GClass0.smethod_0("爲絸䳡笀");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ");
				array[1] = global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡌॊੇେౙ");
				array[2] = global::GClass0.smethod_0("^řɆ͇ш՚ٞݙࡑ्੗୎ౄ");
				array[3] = global::GClass0.smethod_0("GŌɆ̈́");
				array[4] = global::GClass0.smethod_0("JŇɃ̓њՐنݚࡕ");
				array[5] = global::GClass0.smethod_0("XşɄͅцՔٜݛࡖ॑੄");
				array[6] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[7] = global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ");
				array[8] = global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
			}
			return array;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000124A8 File Offset: 0x000106A8
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

		// Token: 0x060001BC RID: 444 RVA: 0x000125A4 File Offset: 0x000107A4
		public List<V_Summary> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("qŤɬ͚ѝՉؼݙ࠴ॊ੍୅ీ൐ํཌၛᅕሼጯᑏᔣᙟ᝞ᡇ᥄ᩉ᭕ᱟᵚṍὍ⁆⅄≘⏓ⓞ▼⛒➨⢯⦴⪵⮶Ⲥⶬ⺫⾧セㆥ㊼㎪㓂㗍㚮㟅㢩㦦㪬㮢㳊㷅㻃㿄䃂䆠䊳䏿䒝䖒䚘䞞䢅䦍䪝䮏䲂䷹仴侒僼冂劅厂咃喌嚞垒墕妜媛宂峪工庅忭悓憔抅揬擪旴曳柵棥槷櫹毺泳涙溔濲炜燵狵珻瓯痤盠矴磣秭窄箇糧綋维翢胰臤苮菋蓁藞蛓蟟裟覹諞诅賙跘躴迀郍釕鋵鏩铧闣雩鞫飋駚骨鯆鲦鷉黁鿅ꃖꆡꋊꌰꐷꔳꙜꜨꠏꤋꨎꬒ갏괴긚꼀뀅넔눂덏됯딾뙌뜩롊뤦먦뭇밧뵋븵뼶쀧섲숴쌖쐑씓옃윕젛줔쨝쭷챫쵵츖콽퀃턄툕팜퐚프혃휅\ud815\ud907\uda09\udb0a\udc03\udd65\ude05\udf0d契響﬉ﱵﵰ﹩ｮcųɹ̀ыՎٙܦࠫह੗୅౒൐ๆ༳ၐᅈሰፎᐠᕞᙙᝆᡇ᥈ᩚ᭞᱙ᵌṊ὇⁇⅙"), string_0);
			return this.GetListBySql(string_);
		}

		// Token: 0x04000024 RID: 36
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
