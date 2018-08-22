using System;

namespace Gssy.Capi.Update
{
	public class SurveyConfigDal
	{
		public bool Exists(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("AŔɜ͊эՙ،݈ࡅड़੆୓ఎഏญ༃၄ᅓ቏፲ᐾᕎᙩᝩᡬ᥼ᩡ᭔ᱹᵻṲὺ⁵ℱ≧⍧⑫╿♩✫⡉⥆⩌⭂ⰻⴢ⹿⼳みㄦ"), string_0);
			return this.oDB.ExecuteScalarInt(string_) > 0;
		}

		public string GetByCodeText(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("@ŗɝ͕ь՚؍ݯࡤ८੬୷౳ൣ๽཰ဃᅄቓፏᑲᔾᙎᝩᡩᥬ᩼᭡᱔ᵹṻὲ⁺ⅵ∱⍧⑧╫♿❩⠫⥉⩆⭌ⱂⴻ⸢⽿〳ㅿ㈦"), string_0);
			return this.oDB.ExecuteScalarString(string_);
		}

		public bool ExistsRead(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("AŔɜ͊эՙ،݈ࡅड़੆୓ఎഏญ༃၄ᅓ቏፲ᐾᕎᙩᝩᡬ᥼ᩡ᭔ᱹᵻṲὺ⁵ℱ≧⍧⑫╿♩✫⡉⥆⩌⭂ⰻⴢ⹿⼳みㄦ"), string_0);
			return this.oDBRead.ExecuteScalarInt(string_) > 0;
		}

		public string GetByCodeTextRead(string string_0)
		{
			string string_ = string.Format(GClass0.smethod_0("@ŗɝ͕ь՚؍ݯࡤ८੬୷౳ൣ๽཰ဃᅄቓፏᑲᔾᙎᝩᡩᥬ᩼᭡᱔ᵹṻὲ⁺ⅵ∱⍧⑧╫♿❩⠫⥉⩆⭌ⱂⴻ⸢⽿〳ㅿ㈦"), string_0);
			return this.oDBRead.ExecuteScalarString(string_);
		}

		private DBProvider oDBRead = new DBProvider(1);

		private DBProvider oDB = new DBProvider(2);
	}
}
