using System;

namespace Gssy.Capi.Update
{
	// Token: 0x02000004 RID: 4
	public class alioss
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002083 File Offset: 0x00000283
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000208B File Offset: 0x0000028B
		public string accessId { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002094 File Offset: 0x00000294
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000209C File Offset: 0x0000029C
		public string accessKey { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020A5 File Offset: 0x000002A5
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020AD File Offset: 0x000002AD
		public string bucketName { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020B6 File Offset: 0x000002B6
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020BE File Offset: 0x000002BE
		public string bucketNameUpdate { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020C7 File Offset: 0x000002C7
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020CF File Offset: 0x000002CF
		public Uri endpoint { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020E0 File Offset: 0x000002E0
		public Uri endpointInner { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020E9 File Offset: 0x000002E9
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020F1 File Offset: 0x000002F1
		public string bucketDir { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020FA File Offset: 0x000002FA
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002102 File Offset: 0x00000302
		public string bucketDirUpdate { get; private set; }

		// Token: 0x06000015 RID: 21 RVA: 0x0000210B File Offset: 0x0000030B
		public alioss(string string_0, bool bool_0, string string_1)
		{
			this.method_0(string_0, bool_0, string_1);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002504 File Offset: 0x00000704
		private void method_0(string string_0, bool bool_0, string string_1)
		{
			this.accessId = GClass0.smethod_0("ZŹɆ͈ѾԳٯݓࡅै੩ଵ౎ൌ๻༶");
			this.accessKey = GClass0.smethod_0("hťɿ͕Ѩ՚فݍ࡚थਢ୉౰ന๨཮၄ᅦቯ፻ᑸᔽᘽ᜷ᡗᥳᩒ᭭᱊ᵀ");
			this.BigFilePartSize = 150;
			if (bool_0)
			{
				this.bucketDir = GClass0.smethod_0("ašɡͷѦ") + string_1;
			}
			else
			{
				this.bucketDir = string_1;
			}
			this.bucketDirUpdate = string_1 + GClass0.smethod_0("sŵɠ͢Ѷդ");
			string uriString = GClass0.smethod_0("");
			string uriString2 = GClass0.smethod_0("");
			if (!(string_0 == GClass0.smethod_0("jŻ")))
			{
				if (!(string_0 == GClass0.smethod_0("sť")))
				{
					if (!(string_0 == GClass0.smethod_0("`ū")))
					{
						if (!(string_0 == GClass0.smethod_0("jŪ")))
						{
							uriString = GClass0.smethod_0("KŖɕ͐ХԱزݳࡨ३਴୻౹഻๦ོၶᅼቫ፸ᑪᕠᘣ᝭ᡧᥣᩰ᭽ᱩᵥṶἪ⁠Ⅽ≬");
							uriString2 = GClass0.smethod_0("Dşɞ͙ВԈ؉݊ࡗॐਏୂ౎ല๭ཱུၹᅵበ፱ᑽᕹᘻ᝼᡺ᥧ᩷᭣᱾ᵮṢἣ⁭Ⅷ≣⍰⑽╩♥❶⠪⥠⩭⭬");
							this.bucketName = GClass0.smethod_0("gŢɲͨ");
							this.bucketNameUpdate = GClass0.smethod_0("iŨɸͮѳյ٠ݢࡶ।");
						}
						else
						{
							uriString = GClass0.smethod_0("KŖɕ͐ХԱزݳࡨ३਴୻౹഻๽ཻၽᅵቺ፿ᑡᕩᘣ᝭ᡧᥣᩰ᭽ᱩᵥṶἪ⁠Ⅽ≬");
							uriString2 = GClass0.smethod_0("Dşɞ͙ВԈ؉݊ࡗॐਏୂ౎ല๶ིၲᅼቱ፶ᑶᕰᘻ᝼᡺ᥧ᩷᭣᱾ᵮṢἣ⁭Ⅷ≣⍰⑽╩♥❶⠪⥠⩭⭬");
							this.bucketName = GClass0.smethod_0("eŤɴͪѪժ");
							this.bucketNameUpdate = GClass0.smethod_0("oŪɺ͠ѽշ٢ݤࡰ०੪୪");
						}
					}
					else
					{
						uriString = GClass0.smethod_0("JŕɔͯФԲسݴࡩ४ਵ୴౸സ๶ྲྀၻᅻቹ፡ᑩᔣ᙭ᝧᡣᥰ᩽᭩ᱥᵶḪὠ⁭Ⅼ");
						uriString2 = GClass0.smethod_0("CŞɝ͘Нԉ؊݋ࡐ॑਌ୃ౱ള๿ཹၲᅰተ፶ᑰᔻᙼ᝺ᡧ᥷ᩣ᭾ᱮᵢḣὭ⁧Ⅳ≰⍽⑩╥♶✪⡠⥭⩬");
						this.bucketName = GClass0.smethod_0("eŤɴͪѠի");
						this.bucketNameUpdate = GClass0.smethod_0("oŪɺ͠ѽշ٢ݤࡰ०੠୫");
					}
				}
				else
				{
					uriString = GClass0.smethod_0("JŕɔͯФԲسݴࡩ४ਵ୴౸സ๥ེၼᅶቴ፮ᑡᔣ᙭ᝧᡣᥰ᩽᭩ᱥᵶḪὠ⁭Ⅼ");
					uriString2 = GClass0.smethod_0("CŞɝ͘Нԉ؊݋ࡐ॑਌ୃ౱ള๬ཱུၵᅽች፹ᑸᔻᙼ᝺ᡧ᥷ᩣ᭾ᱮᵢḣὭ⁧Ⅳ≰⍽⑩╥♶✪⡠⥭⩬");
					this.bucketName = GClass0.smethod_0("eŤɴͪѳե");
					this.bucketNameUpdate = GClass0.smethod_0("oŪɺ͠ѽշ٢ݤࡰ०ੳ୥");
				}
			}
			else
			{
				uriString = GClass0.smethod_0("\u007fŢɡͤЩԽؾݿࡼॽਣ୭౧ൣ๰ཽၩᅥቶጪᑠᕭᙬ");
				uriString2 = GClass0.smethod_0("HūɪͭЦԴصݶ࡫।਻୼౺൧๷ལၾᅮቢጣᑭᕧᙣᝰ᡽ᥩᩥ᭶ᰪᵠṭὬ");
				this.bucketName = GClass0.smethod_0("eŤɴͪѪջ");
				this.bucketNameUpdate = GClass0.smethod_0("oŪɺ͠ѽշ٢ݤࡰ०੪୻");
			}
			this.endpoint = new Uri(uriString);
			this.endpointInner = new Uri(uriString2);
		}

		// Token: 0x04000033 RID: 51
		public int BigFilePartSize = 150;
	}
}
