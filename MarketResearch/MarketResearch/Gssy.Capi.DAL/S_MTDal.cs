﻿using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000018 RID: 24
	public class S_MTDal
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00010F4C File Offset: 0x0000F14C
		public bool Exists(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("tţɩ͡Ѡն؁ݣࡐो੓ୈళരะ༸ၑᅄቚፙᐳᕁᙎ᝝ᡛ᤮ᩚ᭄ᱎᵘṌἨ⁎⅂∥⌹⑸┲♼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00010F8C File Offset: 0x0000F18C
		public S_MT GetByID(int int_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("sŚɒ͘џՏغܳ࠸॑੄୚ౙളแཎၝᅛሮፚᑄᕎᙘᝌᠨ᥎ᩂᬥ᰹ᵸḲὼ"), int_0);
			return this.GetBySql(string_);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		public S_MT GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			S_MT s_MT = new S_MT();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					s_MT.ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]);
					s_MT.MT_QUESTION = dataReader[global::GClass0.smethod_0("FŞɖ͙ђՃٖݐࡊ्੏")].ToString();
					s_MT.QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString();
					s_MT.QUESTION_TYPE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]);
					s_MT.GROUP_LEVEL = dataReader[global::GClass0.smethod_0("LŘɆ͝їՙى݁ࡕे੍")].ToString();
					s_MT.GROUP_CODEA = dataReader[global::GClass0.smethod_0("LŘɆ͝їՙن݋ࡇेੀ")].ToString();
					s_MT.GROUP_CODEB = dataReader[global::GClass0.smethod_0("LŘɆ͝їՙن݋ࡇे੃")].ToString();
					s_MT.GROUP_PAGE_TYPE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("HŜɂ͙ћՕٙ݉ࡀृਗ਼୐ౚ൒ไ")]);
				}
			}
			return s_MT;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public List<S_MT> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<S_MT> list = new List<S_MT>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new S_MT
					{
						ID = Convert.ToInt32(dataReader[global::GClass0.smethod_0("KŅ")]),
						MT_QUESTION = dataReader[global::GClass0.smethod_0("FŞɖ͙ђՃٖݐࡊ्੏")].ToString(),
						QUESTION_NAME = dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")].ToString(),
						QUESTION_TYPE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ")]),
						GROUP_LEVEL = dataReader[global::GClass0.smethod_0("LŘɆ͝їՙى݁ࡕे੍")].ToString(),
						GROUP_CODEA = dataReader[global::GClass0.smethod_0("LŘɆ͝їՙن݋ࡇेੀ")].ToString(),
						GROUP_CODEB = dataReader[global::GClass0.smethod_0("LŘɆ͝їՙن݋ࡇे੃")].ToString(),
						GROUP_PAGE_TYPE = Convert.ToInt32(dataReader[global::GClass0.smethod_0("HŜɂ͙ћՕٙ݉ࡀृਗ਼୐ౚ൒ไ")])
					});
				}
			}
			return list;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00011220 File Offset: 0x0000F420
		public List<S_MT> GetList()
		{
			string string_ = global::GClass0.smethod_0("MŘɐ͞љՍظܽ࠶॓੆ଡ଼౟റใཐ၃ᅙሬፄᑘᕍᙍ᝕ᠦ᥇ᩝᬣ᱋ᵅ");
			return this.GetListBySql(string_);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00011244 File Offset: 0x0000F444
		public void Add(S_MT s_MT_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("ÒǔˊϝӅׂڵߝࣝ৆૞ர೜ෑເ࿘ႣᇇዝᏗᓖᗓᛀៗᣗ᧋ᫎᯎ᱓ᴯḨἹ\u2028℮∰⌷␹┩☻✵⠾⤷⩝⬡ⰺ⴫⸾⼸〢ㄥ㈧㌷㐳㔿㘵㜡㡏㤥㨳㬯㰊㴎㸂㼐䀞䄌䈜䌔䑻䔑䘇䜛䠆䤂䨎䬓䰀䴊丈伍偧儍创匇吒唖嘚圇堌夆娄嬂尓嵹幯彳恮慪扦捨摶敱晰杫桧楫橡歵氆洎湻潭灧煿牬獻琏甁癞眔硞礅稍笇籤累繠缻耷腡舫荥萻蔱虮蜧衮褵訽謷豴贺蹰輫逧鄭鉲錽鑺锡阩靿頵饿騨"), new object[]
			{
				s_MT_0.MT_QUESTION,
				s_MT_0.QUESTION_NAME,
				s_MT_0.QUESTION_TYPE,
				s_MT_0.GROUP_LEVEL,
				s_MT_0.GROUP_CODEA,
				s_MT_0.GROUP_CODEB,
				s_MT_0.GROUP_PAGE_TYPE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public void Update(S_MT s_MT_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("ûǽ˨ϪӾ׬ڈߴࣹ২૰ஃೱ෤໴྿დᇉዃᏊᓏᗜᛋៃᣟ᧚᫚᮳Ჯᶱặῴ₿⇰⊫⎧ⓛ◜⛍⟔⣒⧌⫋⯍ⳝ⷏⻁⼲〻ㅝ㉁㍛㑝㔂㙊㜊㡑㥙㨥㬦㰷㴢㸤㼦䀡䄣䈳䌿䐳䔹䘭䝇䡛䥅䨟䭐䰟䵍丧伍們儈刌匄吖唜嘎園堚奵婩孳屵崪幤弲恩慡戋挙搅攜昘朘栅椊樀欆氃浡湽漟瀙煆爉獆琝甕癿睥硹祠穤筬籱絾繴罪聬脍舑茋萍蕒蘞蝚蠁褉詣譱豭赴蹰轀過酜鉛鍞鑅镍陁靇顓餵騩鬳鱩鴦鹭鼯ꁙꅅꉉꍙꑏꔩꙁꝃꠦꤸꨤꭸ갲굼"), new object[]
			{
				s_MT_0.ID,
				s_MT_0.MT_QUESTION,
				s_MT_0.QUESTION_NAME,
				s_MT_0.QUESTION_TYPE,
				s_MT_0.GROUP_LEVEL,
				s_MT_0.GROUP_CODEA,
				s_MT_0.GROUP_CODEB,
				s_MT_0.GROUP_PAGE_TYPE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00011348 File Offset: 0x0000F548
		public void Delete(S_MT s_MT_0)
		{
			string string_ = string.Format(global::GClass0.smethod_0("ZŘɐ͞ю՜ظݑࡄग़ਖ਼ଳుൎ๝ཛီᅚቄፎᑘᕌᘨᝎᡂᤥᨹ᭸ᰲᵼ"), s_MT_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00011380 File Offset: 0x0000F580
		public void Truncate()
		{
			string string_ = global::GClass0.smethod_0("TŊɂ͈јՎتݏ࡚ैੋଥ౗൜๏ཕ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 8;
			string[] array = new string[8];
			if (bool_0)
			{
				array[0] = global::GClass0.smethod_0("臮厫純僶");
				array[1] = global::GClass0.smethod_0("Iŗ骚僶");
				array[2] = global::GClass0.smethod_0("闪馛純僶");
				array[3] = global::GClass0.smethod_0("颚嚊");
				array[4] = global::GClass0.smethod_0("徬犪骜緇窥圪");
				array[5] = global::GClass0.smethod_0("御犥骑緌瘱媬疪䧧瀂ृ噃");
				array[6] = global::GClass0.smethod_0("御犥骑緌瘱媬疪䧧瀂ी噃");
				array[7] = global::GClass0.smethod_0("闣馔唣岠瞦鶐磃䤫纁䙉畭睹宊");
			}
			else
			{
				array[0] = global::GClass0.smethod_0("KŅ");
				array[1] = global::GClass0.smethod_0("FŞɖ͙ђՃٖݐࡊ्੏");
				array[2] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ");
				array[3] = global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॐਗ਼୒ౄ");
				array[4] = global::GClass0.smethod_0("LŘɆ͝їՙى݁ࡕे੍");
				array[5] = global::GClass0.smethod_0("LŘɆ͝їՙن݋ࡇेੀ");
				array[6] = global::GClass0.smethod_0("LŘɆ͝їՙن݋ࡇे੃");
				array[7] = global::GClass0.smethod_0("HŜɂ͙ћՕٙ݉ࡀृਗ਼୐ౚ൒ไ");
			}
			return array;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00011498 File Offset: 0x0000F698
		public string[,] ExcelContent(int int_0, List<S_MT> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (S_MT s_MT in list_0)
			{
				array[num, 0] = s_MT.ID.ToString();
				array[num, 1] = s_MT.MT_QUESTION;
				array[num, 2] = s_MT.QUESTION_NAME;
				array[num, 3] = s_MT.QUESTION_TYPE.ToString();
				array[num, 4] = s_MT.GROUP_LEVEL;
				array[num, 5] = s_MT.GROUP_CODEA;
				array[num, 6] = s_MT.GROUP_CODEB;
				array[num, 7] = s_MT.GROUP_PAGE_TYPE.ToString();
				num++;
			}
			return array;
		}

		// Token: 0x04000021 RID: 33
		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
