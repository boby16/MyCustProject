using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class S_JUMPDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("zŭɫͣѦհ؃ݡ࡮ॵੑ୊వശา༺ၟᅊቘ፛ᐵᕇᙌ᝘ᡄᥝ᩟ᬮᱚᵄṎ὘⁌ℨ≎⍂␥┹♸✲⡼"), int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public S_JUMP GetByID(int int_0)
		{
			string string_ = string.Format(GClass0.smethod_0("qŤɬ͚ѝՉؼܱ࠺य़੊୘౛വ็ཌၘᅄቝ፟ᐮᕚᙄᝎᡘ᥌ᨨ᭎᱂ᴥḹὸ′ⅼ"), int_0);
			return this.GetBySql(string_);
		}

		public S_JUMP GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			S_JUMP s_JUMP = new S_JUMP();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					s_JUMP.ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]);
					s_JUMP.PAGE_TEXT = dataReader[GClass0.smethod_0("Yŉɀ̓њՐنݚࡕ")].ToString();
					s_JUMP.PAGE_VALUE = dataReader[GClass0.smethod_0("Zňɏ͂љՓمݏࡗॄ")].ToString();
					s_JUMP.PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString();
					s_JUMP.CIRCLE_A_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ")]);
					s_JUMP.CIRCLE_A_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ")]);
					s_JUMP.CIRCLE_B_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ")]);
					s_JUMP.CIRCLE_B_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ")]);
				}
			}
			return s_JUMP;
		}

		public List<S_JUMP> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<S_JUMP> list = new List<S_JUMP>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new S_JUMP
					{
						ID = Convert.ToInt32(dataReader[GClass0.smethod_0("KŅ")]),
						PAGE_TEXT = dataReader[GClass0.smethod_0("Yŉɀ̓њՐنݚࡕ")].ToString(),
						PAGE_VALUE = dataReader[GClass0.smethod_0("Zňɏ͂љՓمݏࡗॄ")].ToString(),
						PAGE_ID = dataReader[GClass0.smethod_0("WŇɂ́ќՋم")].ToString(),
						CIRCLE_A_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ")]),
						CIRCLE_A_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ")]),
						CIRCLE_B_CURRENT = Convert.ToInt32(dataReader[GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ")]),
						CIRCLE_B_COUNT = Convert.ToInt32(dataReader[GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ")])
					});
				}
			}
			return list;
		}

		public List<S_JUMP> GetList()
		{
			string string_ = GClass0.smethod_0("sŚɒ͘џՏغܳ࠸॑੄୚ౙളแཎၚᅚቃ፝ᐬᕄᙘᝍᡍᥕᨦᭇᱝᴣṋὅ");
			return this.GetListBySql(string_);
		}

		public void Add(S_JUMP s_JUMP_0)
		{
			string string_ = string.Format(GClass0.smethod_0("ÓǗˋϒӄׁڴߚࣜ৅૟யೝිໆ࿞ჇᇙአᏗᓇᗂᛁៜᣖᧄ᫘ᬫ᱒ᴭḽἼ‿Ω∮⌶␺┠☱❟⠢⤰⨷⬪ⰱⴤ⸨⽇〩ㄠ㈺㌤㐪㔠㘻㜢㠽㤢㨵㬍㰌㴘㸒㼏䁶䄚䈑䌅䐕䔙䘑䜌䠓䤎䨓䬀䰛䴃丘佧倉儀刚匄吊唀嘛圁堝夂娕孭屬嵸干彯怖慺扱捥摵敹晱杬桰楮橳歠汻浣湸漂瀊煿物獫瑳畠癷看砅祚稐筢簹紱縻罠耫腤舿茻萱蕮蘦蝮蠵褽詫謼豳贡蹷輿遷鄥鉳録鑻锩陿霵顿館"), new object[]
			{
				s_JUMP_0.PAGE_TEXT,
				s_JUMP_0.PAGE_VALUE,
				s_JUMP_0.PAGE_ID,
				s_JUMP_0.CIRCLE_A_CURRENT,
				s_JUMP_0.CIRCLE_A_COUNT,
				s_JUMP_0.CIRCLE_B_CURRENT,
				s_JUMP_0.CIRCLE_B_COUNT
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(S_JUMP s_JUMP_0)
		{
			string string_ = string.Format(GClass0.smethod_0("øǼ˯ϫӽ׭ڇߵࣺ৮૶௯ೱ඀໌࿛჉ᆼዋᏛᓞᗝᛈែᣐ᧌᫇᮲ᲬᶰẨ῵₼⇱⊬⎦ⓙ◉⛀⟃⣚⧒⫂⯎Ⳕⷅ⹟⽃そㅛ㈀㍈㐄㕟㙛㜦㠴㤳㨶㬭㰸㴴㹏㽓䁍䅋䈐䍙䐔䕏䙋䜥䠬䤶䨠䬮䰤䴿丞企倞儉刉匈吜唖嘃坶塨奴娨学尬嵼希弇怟意戇挏搖攉昘朅栊椑樍欖污浽渟潅瀈煁爗獹瑰番癴睺硰祫穱筭籲絥繽罼聨腢艿茊萔蔈虜蜐衘褈詠譫豳赣蹓轛遂酞鉄鍙鑖镍陙靂頵餩騳魩鰦鵭鸯齙ꁅꅉꉙꍏꐩꕁꙃꜦ꠸ꤤ꩸ꬲ걼"), new object[]
			{
				s_JUMP_0.ID,
				s_JUMP_0.PAGE_TEXT,
				s_JUMP_0.PAGE_VALUE,
				s_JUMP_0.PAGE_ID,
				s_JUMP_0.CIRCLE_A_CURRENT,
				s_JUMP_0.CIRCLE_A_COUNT,
				s_JUMP_0.CIRCLE_B_CURRENT,
				s_JUMP_0.CIRCLE_B_COUNT
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(S_JUMP s_JUMP_0)
		{
			string string_ = string.Format(GClass0.smethod_0("dŚɒ͘ш՞غݟࡊक़ਜ਼ଵేൌ๘ངၝᅟሮፚᑄᕎᙘᝌᠨ᥎ᩂᬥ᰹ᵸḲὼ"), s_JUMP_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = GClass0.smethod_0("VŔɜ͊њՈجݍࡘॆ੅ଧౕ൚๎བ၏ᅑ");
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 8;
			string[] array = new string[8];
			if (bool_0)
			{
				array[0] = GClass0.smethod_0("臮厫純僶");
				array[1] = GClass0.smethod_0("顶諶搏");
				array[2] = GClass0.smethod_0("顷儽");
				array[3] = GClass0.smethod_0("顶縔凶");
				array[4] = GClass0.smethod_0("HĨ幅展噈玀妩璭浱");
				array[5] = GClass0.smethod_0("FĦ幇岮瞬改捱");
				array[6] = GClass0.smethod_0("KĨ幅展噈玀妩璭浱");
				array[7] = GClass0.smethod_0("EĦ幇岮瞬改捱");
			}
			else
			{
				array[0] = GClass0.smethod_0("KŅ");
				array[1] = GClass0.smethod_0("Yŉɀ̓њՐنݚࡕ");
				array[2] = GClass0.smethod_0("Zňɏ͂љՓمݏࡗॄ");
				array[3] = GClass0.smethod_0("WŇɂ́ќՋم");
				array[4] = GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ");
				array[5] = GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ");
				array[6] = GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ");
				array[7] = GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ");
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<S_JUMP> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (S_JUMP s_JUMP in list_0)
			{
				array[num, 0] = s_JUMP.ID.ToString();
				array[num, 1] = s_JUMP.PAGE_TEXT;
				array[num, 2] = s_JUMP.PAGE_VALUE;
				array[num, 3] = s_JUMP.PAGE_ID;
				array[num, 4] = s_JUMP.CIRCLE_A_CURRENT.ToString();
				array[num, 5] = s_JUMP.CIRCLE_A_COUNT.ToString();
				array[num, 6] = s_JUMP.CIRCLE_B_CURRENT.ToString();
				array[num, 7] = s_JUMP.CIRCLE_B_COUNT.ToString();
				num++;
			}
			return array;
		}

		public void ExecuteProceddure(string string_0)
		{
			this.dbprovider_0.ExecuteNonQuery(string_0);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
