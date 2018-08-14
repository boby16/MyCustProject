using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x0200001A RID: 26
	public class V_DefineAnswerDal
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00011944 File Offset: 0x0000FB44
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("bŵɣͫѮո؋ݩࡦॽ੩୲఍എช༂ၧᅲቐፓᐽᕊᙄ᝞᡼᥾᩾᭸ᱰᵕṽὡ⁦ⅵ≽⌮⑚╄♎❘⡌⤨⩎⭂Ⱕⴹ⹸⼲ぼ"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00011984 File Offset: 0x0000FB84
		public V_DefineAnswer GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("yŬɤ͢ѥձ؄܉ࠂ१ੲ୐౓ഽ๊ངၞᅼቾ፾ᑸᕰᙕ᝽ᡡᥦ᩵᭽ᰮᵚṄ὎⁘⅌∨⍎⑂┥☹❸⠲⥼"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000119B0 File Offset: 0x0000FBB0
		public V_DefineAnswer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_DefineAnswer v_DefineAnswer = new V_DefineAnswer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_DefineAnswer.ANSWER_ORDER = Convert.ToInt32(dataReader[global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]);
					v_DefineAnswer.QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					v_DefineAnswer.QUESTION_TYPE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]);
					v_DefineAnswer.QUESTION_TITLE = dataReader[global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString();
					v_DefineAnswer.DETAIL_ID = dataReader[global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString();
					v_DefineAnswer.PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					v_DefineAnswer.PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString();
					v_DefineAnswer.SPSS_CASE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ")]);
					v_DefineAnswer.SPSS_VARIABLE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]);
					v_DefineAnswer.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader[global::GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ")]);
					v_DefineAnswer.QNAME_MAPPING = dataReader[global::GClass0.smethod_0("\\łɊ͇ь՗ي݇ࡕ॔੊ୌె")].ToString();
					v_DefineAnswer.SPSS_TITLE = dataReader[global::GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ")].ToString();
					v_DefineAnswer.TEST_FIX_ANSWER = dataReader[global::GClass0.smethod_0("[ŋɞ͘єՌـݐࡘेੋୗ౔േ๓")].ToString();
				}
			}
			return v_DefineAnswer;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00011B70 File Offset: 0x0000FD70
		public List<V_DefineAnswer> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_DefineAnswer> list = new List<V_DefineAnswer>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_DefineAnswer
					{
						ANSWER_ORDER = Convert.ToInt32(dataReader[global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓")]),
						QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						QUESTION_TYPE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]),
						QUESTION_TITLE = dataReader[global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ")].ToString(),
						DETAIL_ID = dataReader[global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ")].ToString(),
						PAGE_ID = dataReader[global::GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						PARENT_CODE = dataReader[global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄")].ToString(),
						SPSS_CASE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ")]),
						SPSS_VARIABLE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ")]),
						SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader[global::GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ")]),
						QNAME_MAPPING = dataReader[global::GClass0.smethod_0("\\łɊ͇ь՗ي݇ࡕ॔੊ୌె")].ToString(),
						SPSS_TITLE = dataReader[global::GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ")].ToString(),
						TEST_FIX_ANSWER = dataReader[global::GClass0.smethod_0("[ŋɞ͘єՌـݐࡘेੋୗ౔േ๓")].ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00011D3C File Offset: 0x0000FF3C
		public List<V_DefineAnswer> GetList()
		{
			string string_ = global::GClass0.smethod_0("aŴɼͪѭչ،܁ࠊ९੺୨౫അ๲ོၦᅄቆ፶ᑰᕸᙝ᝵ᡩ᥮᩽᭥ᰶᵚṆὗ⁗⅃∰⍍⑗┭♍❅⡙⥞⩍⭕ⱙⵊ⹖⽇ぇㅓ");
			return this.GetListBySql(string_);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00011D60 File Offset: 0x0000FF60
		public List<V_DefineAnswer> GetListByQuestionType(string string_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("\u001fĎȆ̌Ћԓ٦ݯࡤअਐ଎఍ട๨རၸᅞቜፐᑖᕒᙷ᝛ᡇ᥄ᩗᭃᰐᵸṦὨ⁾Ⅾ∊⍸⑽╢♵❱⡭⥬⩬⭾ⱴⵆ⹎⽘〼ㄦ㉡㌩㑥㔷㙙㝇㡐㥖㩀㬱㱒㵖㸮㽌䁂䅘䉝䍌䑚䕘䙉䝗䡀䥆䩐䬡"), string_0);
			return this.GetListBySql(string_);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00011D88 File Offset: 0x0000FF88
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 12;
			string[] array = new string[12];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("颞矫趗勹鱸宎");
				array[1] = global::GClass0.smethod_0("闦馟紐僲﬌庝遇");
				array[2] = global::GClass0.smethod_0("丿馛骚咊");
				array[3] = global::GClass0.smethod_0("颚彳");
				array[4] = global::GClass0.smethod_0("养腓韨鮝戊篅捲摯");
				array[5] = global::GClass0.smethod_0("顶縔凶");
				array[6] = global::GClass0.smethod_0("TŖɖ͗У鶚冊");
				array[7] = global::GClass0.smethod_0("ZŘɔ͕Х囜韌筹徊");
				array[8] = global::GClass0.smethod_0("[ŗɕ͖Ф夌捲䡌");
				array[9] = global::GClass0.smethod_0("门馝紒僴戢夅");
				array[10] = global::GClass0.smethod_0("TŖɖ͗У鶚塳");
				array[11] = global::GClass0.smethod_0("爲絸䳡笀");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("MŅə͞эՕٙ݊ࡖेੇ୓");
				array[1] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[2] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ");
				array[3] = global::GClass0.smethod_0("_Řɉ͘ўՀه੍࡙݉॑ୗ౎ൄ");
				array[4] = global::GClass0.smethod_0("Mōɓ͇ьՈٜ݋ࡅ");
				array[5] = global::GClass0.smethod_0("WŇɂ́ќՋم");
				array[6] = global::GClass0.smethod_0("ZŘɔ͕њՇقݑࡄ");
				array[7] = global::GClass0.smethod_0("^Ŝɘ͙і՞نݔࡌॅੁ୎ౄ");
				array[8] = global::GClass0.smethod_0("@łɂ̓ѐ՞ٟ݅ࡅफ़੖ୌూ൅์ཉ၂ᅋቍ");
				array[9] = global::GClass0.smethod_0("\\łɊ͇ь՗ي݇ࡕ॔੊ୌె");
				array[10] = global::GClass0.smethod_0("Yřɛ͔љՑٍݗࡎॄ");
				array[11] = global::GClass0.smethod_0("[ŋɛ͍щՒٚ݇ࡌॆ੄");
			}
			return array;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00011EF0 File Offset: 0x000100F0
		public string[,] ExcelContent(int int_0, List<V_DefineAnswer> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_DefineAnswer v_DefineAnswer in list_0)
			{
				array[num, 0] = v_DefineAnswer.ANSWER_ORDER.ToString();
				array[num, 1] = v_DefineAnswer.QUESTION_NAME;
				array[num, 2] = v_DefineAnswer.QUESTION_TYPE.ToString();
				array[num, 3] = v_DefineAnswer.QUESTION_TITLE;
				array[num, 4] = v_DefineAnswer.DETAIL_ID;
				array[num, 5] = v_DefineAnswer.PAGE_ID;
				array[num, 6] = v_DefineAnswer.SPSS_CASE.ToString();
				array[num, 7] = v_DefineAnswer.SPSS_VARIABLE.ToString();
				array[num, 8] = v_DefineAnswer.SPSS_PRINT_DECIMAIL.ToString();
				array[num, 9] = v_DefineAnswer.QNAME_MAPPING;
				array[num, 10] = v_DefineAnswer.SPSS_TITLE;
				array[num, 11] = v_DefineAnswer.PARENT_CODE;
				num++;
			}
			return array;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00012034 File Offset: 0x00010234
		public List<V_DefineAnswer> GetListByTime()
		{
			string string_ = global::GClass0.smethod_0(">ĩȧ̯ЪԼه݌ࡅढ਱ଭబീฉ༁မᄹሽጳᐷᔽᘖ᜸ᠦᤣᨶᬠᱱᴇḇἋ‟℉≫⌞␌┛☓✙⠃⤍⨛⬝Ⰰⴎ⹬⽩へㅮ㈛㌆㐇㔟㘐㜖㡔㥚㩗㬒㱥㵵㹼㽺䁲䅪䉢䍲䑶䕩䙩䝵䡲䥡䩱䬂䱈䵓丿佰偲全刻却呬啴噻圶塚奆婗字屃崰幍彗怭慍扅捙摞敍晕杙桊楖橇歇汓");
			return this.GetListBySql(string_);
		}

		// Token: 0x04000023 RID: 35
		private DBProvider dbprovider_0 = new DBProvider(1);
	}
}
