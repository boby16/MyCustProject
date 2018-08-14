using System;

namespace Gssy.Capi.Update
{
	// Token: 0x02000009 RID: 9
	public class SurveyConfigDal
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002ED4 File Offset: 0x000010D4
		public bool Exists(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("AŔɜ͊эՙ،݈ࡅड़੆୓ఎഏญ༃၄ᅓ቏፲ᐾᕎᙩᝩᡬ᥼ᩡ᭔ᱹᵻṲὺ⁵ℱ≧⍧⑫╿♩✫⡉⥆⩌⭂ⰻⴢ⹿⼳みㄦ"), string_0);
			return this.oDB.ExecuteScalarInt(string_) > 0;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F04 File Offset: 0x00001104
		public string GetByCodeText(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("@ŗɝ͕ь՚؍ݯࡤ८੬୷౳ൣ๽཰ဃᅄቓፏᑲᔾᙎᝩᡩᥬ᩼᭡᱔ᵹṻὲ⁺ⅵ∱⍧⑧╫♿❩⠫⥉⩆⭌ⱂⴻ⸢⽿〳ㅿ㈦"), string_0);
			return this.oDB.ExecuteScalarString(string_);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F30 File Offset: 0x00001130
		public bool ExistsRead(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("AŔɜ͊эՙ،݈ࡅड़੆୓ఎഏญ༃၄ᅓ቏፲ᐾᕎᙩᝩᡬ᥼ᩡ᭔ᱹᵻṲὺ⁵ℱ≧⍧⑫╿♩✫⡉⥆⩌⭂ⰻⴢ⹿⼳みㄦ"), string_0);
			return this.oDBRead.ExecuteScalarInt(string_) > 0;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F60 File Offset: 0x00001160
		public string GetByCodeTextRead(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("@ŗɝ͕ь՚؍ݯࡤ८੬୷౳ൣ๽཰ဃᅄቓፏᑲᔾᙎᝩᡩᥬ᩼᭡᱔ᵹṻὲ⁺ⅵ∱⍧⑧╫♿❩⠫⥉⩆⭌ⱂⴻ⸢⽿〳ㅿ㈦"), string_0);
			return this.oDBRead.ExecuteScalarString(string_);
		}

		// Token: 0x0400003C RID: 60
		private DBProvider oDBRead = new DBProvider(1);

		// Token: 0x0400003D RID: 61
		private DBProvider oDB = new DBProvider(2);
	}
}
