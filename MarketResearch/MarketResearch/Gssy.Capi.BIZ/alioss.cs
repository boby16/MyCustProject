using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aliyun.OpenServices.OpenStorageService;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000003 RID: 3
	public class alioss
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020A5 File Offset: 0x000002A5
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020AD File Offset: 0x000002AD
		public string accessId { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020B6 File Offset: 0x000002B6
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020BE File Offset: 0x000002BE
		public string accessKey { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020C7 File Offset: 0x000002C7
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020CF File Offset: 0x000002CF
		public string bucketName { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020E0 File Offset: 0x000002E0
		public string bucketNameUpdate { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020E9 File Offset: 0x000002E9
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000020F1 File Offset: 0x000002F1
		public Uri endpoint { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000020FA File Offset: 0x000002FA
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002102 File Offset: 0x00000302
		public Uri endpointInner { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000210B File Offset: 0x0000030B
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002113 File Offset: 0x00000313
		public string bucketDir { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002124 File Offset: 0x00000324
		public string bucketDirUpdate { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000212D File Offset: 0x0000032D
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002135 File Offset: 0x00000335
		public OssClient ossClient { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000213E File Offset: 0x0000033E
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002146 File Offset: 0x00000346
		public string OutMessage { get; private set; }

		// Token: 0x0600001E RID: 30 RVA: 0x0000214F File Offset: 0x0000034F
		public alioss(string string_0, bool bool_0, string string_1)
		{
			this.method_0(string_0, bool_0, string_1);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003B84 File Offset: 0x00001D84
		private void method_0(string string_0, bool bool_0, string string_1)
		{
			this.accessId = global::GClass0.smethod_0("ZŹɆ͈ѾԳٯݓࡅै੩ଵ౎ൌ๻༶");
			this.accessKey = global::GClass0.smethod_0("hťɿ͕Ѩ՚فݍ࡚थਢ୉౰ന๨཮၄ᅦቯ፻ᑸᔽᘽ᜷ᡗᥳᩒ᭭᱊ᵀ");
			this.BigFilePartSize = 150;
			if (bool_0)
			{
				this.bucketDir = global::GClass0.smethod_0("ašɡͷѦ") + string_1;
			}
			else
			{
				this.bucketDir = string_1;
			}
			this.bucketDirUpdate = string_1 + global::GClass0.smethod_0("sŵɠ͢Ѷդ");
			string uriString = global::GClass0.smethod_0("");
			string uriString2 = global::GClass0.smethod_0("");
			if (!(string_0 == global::GClass0.smethod_0("jŻ")))
			{
				if (!(string_0 == global::GClass0.smethod_0("sť")))
				{
					if (!(string_0 == global::GClass0.smethod_0("`ū")))
					{
						if (!(string_0 == global::GClass0.smethod_0("jŪ")))
						{
							uriString = global::GClass0.smethod_0("KŖɕ͐ХԱزݳࡨ३਴୻౹഻๦ོၶᅼቫ፸ᑪᕠᘣ᝭ᡧᥣᩰ᭽ᱩᵥṶἪ⁠Ⅽ≬");
							uriString2 = global::GClass0.smethod_0("Dşɞ͙ВԈ؉݊ࡗॐਏୂ౎ല๭ཱུၹᅵበ፱ᑽᕹᘻ᝼᡺ᥧ᩷᭣᱾ᵮṢἣ⁭Ⅷ≣⍰⑽╩♥❶⠪⥠⩭⭬");
							this.bucketName = global::GClass0.smethod_0("gŢɲͨ");
							this.bucketNameUpdate = global::GClass0.smethod_0("iŨɸͮѳյ٠ݢࡶ।");
						}
						else
						{
							uriString = global::GClass0.smethod_0("KŖɕ͐ХԱزݳࡨ३਴୻౹഻๽ཻၽᅵቺ፿ᑡᕩᘣ᝭ᡧᥣᩰ᭽ᱩᵥṶἪ⁠Ⅽ≬");
							uriString2 = global::GClass0.smethod_0("Dşɞ͙ВԈ؉݊ࡗॐਏୂ౎ല๶ིၲᅼቱ፶ᑶᕰᘻ᝼᡺ᥧ᩷᭣᱾ᵮṢἣ⁭Ⅷ≣⍰⑽╩♥❶⠪⥠⩭⭬");
							this.bucketName = global::GClass0.smethod_0("eŤɴͪѪժ");
							this.bucketNameUpdate = global::GClass0.smethod_0("oŪɺ͠ѽշ٢ݤࡰ०੪୪");
						}
					}
					else
					{
						uriString = global::GClass0.smethod_0("JŕɔͯФԲسݴࡩ४ਵ୴౸സ๶ྲྀၻᅻቹ፡ᑩᔣ᙭ᝧᡣᥰ᩽᭩ᱥᵶḪὠ⁭Ⅼ");
						uriString2 = global::GClass0.smethod_0("CŞɝ͘Нԉ؊݋ࡐ॑਌ୃ౱ള๿ཹၲᅰተ፶ᑰᔻᙼ᝺ᡧ᥷ᩣ᭾ᱮᵢḣὭ⁧Ⅳ≰⍽⑩╥♶✪⡠⥭⩬");
						this.bucketName = global::GClass0.smethod_0("eŤɴͪѠի");
						this.bucketNameUpdate = global::GClass0.smethod_0("oŪɺ͠ѽշ٢ݤࡰ०੠୫");
					}
				}
				else
				{
					uriString = global::GClass0.smethod_0("JŕɔͯФԲسݴࡩ४ਵ୴౸സ๥ེၼᅶቴ፮ᑡᔣ᙭ᝧᡣᥰ᩽᭩ᱥᵶḪὠ⁭Ⅼ");
					uriString2 = global::GClass0.smethod_0("CŞɝ͘Нԉ؊݋ࡐ॑਌ୃ౱ള๬ཱུၵᅽች፹ᑸᔻᙼ᝺ᡧ᥷ᩣ᭾ᱮᵢḣὭ⁧Ⅳ≰⍽⑩╥♶✪⡠⥭⩬");
					this.bucketName = global::GClass0.smethod_0("eŤɴͪѳե");
					this.bucketNameUpdate = global::GClass0.smethod_0("oŪɺ͠ѽշ٢ݤࡰ०ੳ୥");
				}
			}
			else
			{
				uriString = global::GClass0.smethod_0("\u007fŢɡͤЩԽؾݿࡼॽਣ୭౧ൣ๰ཽၩᅥቶጪᑠᕭᙬ");
				uriString2 = global::GClass0.smethod_0("HūɪͭЦԴصݶ࡫।਻୼౺൧๷ལၾᅮቢጣᑭᕧᙣᝰ᡽ᥩᩥ᭶ᰪᵠṭὬ");
				this.bucketName = global::GClass0.smethod_0("eŤɴͪѪջ");
				this.bucketNameUpdate = global::GClass0.smethod_0("oŪɺ͠ѽշ٢ݤࡰ०੪୻");
			}
			this.endpoint = new Uri(uriString);
			this.endpointInner = new Uri(uriString2);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003D94 File Offset: 0x00001F94
		public bool CreateOss()
		{
			this.ossClient = new OssClient(this.endpoint, this.accessId, this.accessKey);
			bool flag = false;
			try
			{
				IEnumerable<Bucket> enumerable = this.ossClient.ListBuckets();
				foreach (Bucket bucket in enumerable)
				{
					if (bucket.Name == this.bucketName)
					{
						flag = true;
						break;
					}
				}
			}
			catch (Exception)
			{
				this.OutMessage = global::GClass0.smethod_0("朤臰跒悮指垨偠ܫ菱懅淡瑒狞ഠ");
				return false;
			}
			bool result;
			if (!flag)
			{
				this.OutMessage = global::GClass0.smethod_0("孑冠攊冧剭壶规思ࠠ");
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003E5C File Offset: 0x0000205C
		public string GetNewUploadSequence(string string_0)
		{
			string text = string_0;
			int num = 100;
			if (text == null)
			{
				text = global::GClass0.smethod_0("");
			}
			if (text != global::GClass0.smethod_0(""))
			{
				num = int.Parse(text);
			}
			for (int i = num; i < 999; i++)
			{
				ObjectListing objectListing = this.ossClient.ListObjects(this.bucketName, this.bucketDir + global::GClass0.smethod_0(".") + i.ToString());
				int num2 = objectListing.ObjectSummaries.Count<OssObjectSummary>();
				if (num2 < 70)
				{
					text = i.ToString();
					return text;
				}
			}
			return text;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003F04 File Offset: 0x00002104
		public bool UploadToOss(string string_0, string string_1)
		{
			string text = this.bucketDir + global::GClass0.smethod_0(".") + string_1;
			ObjectMetadata objectMetadata = new ObjectMetadata();
			objectMetadata.UserMetadata.Add(global::GClass0.smethod_0("Yżɺͱѣռـݢࡶॠ"), string_1);
			string text2 = string_0 + string_1;
			bool flag = false;
			FileInfo fileInfo = new FileInfo(text2);
			if (fileInfo.Length > (long)(1024 * this.BigFilePartSize))
			{
				flag = true;
			}
			try
			{
				if (flag)
				{
					this.mutiPartUpload(this.ossClient, text2, text, this.bucketName, this.BigFilePartSize);
				}
				else
				{
					using (FileStream fileStream = File.Open(text2, FileMode.Open))
					{
						this.ossClient.PutObject(this.bucketName, text, fileStream, objectMetadata);
					}
				}
			}
			catch (Exception)
			{
				this.OutMessage = global::GClass0.smethod_0("斊俺䰁䰪崸蠭ث賱情满畒痞ఠ");
				return false;
			}
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003FF8 File Offset: 0x000021F8
		public void mutiPartUpload(OssClient ossClient_0, string string_0, string string_1, string string_2, int int_0)
		{
			InitiateMultipartUploadRequest initiateMultipartUploadRequest = new InitiateMultipartUploadRequest(string_2, string_1);
			InitiateMultipartUploadResult initiateMultipartUploadResult = ossClient_0.InitiateMultipartUpload(initiateMultipartUploadRequest);
			int num = 1024 * int_0;
			FileInfo fileInfo = new FileInfo(string_0);
			int num2 = (int)(fileInfo.Length / (long)num);
			if (fileInfo.Length % (long)num != 0L)
			{
				num2++;
			}
			List<PartETag> list = new List<PartETag>();
			for (int i = 0; i < num2; i++)
			{
				FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open);
				long num3 = (long)(num * i);
				fileStream.Position = num3;
				long value = ((long)num < fileInfo.Length - num3) ? ((long)num) : (fileInfo.Length - num3);
				UploadPartResult uploadPartResult = ossClient_0.UploadPart(new UploadPartRequest(string_2, string_1, initiateMultipartUploadResult.UploadId)
				{
					InputStream = fileStream,
					PartSize = new long?(value),
					PartNumber = new int?(i + 1)
				});
				list.Add(uploadPartResult.PartETag);
				fileStream.Close();
			}
			CompleteMultipartUploadRequest completeMultipartUploadRequest = new CompleteMultipartUploadRequest(string_2, string_1, initiateMultipartUploadResult.UploadId);
			foreach (PartETag item in list)
			{
				completeMultipartUploadRequest.PartETags.Add(item);
			}
			ossClient_0.CompleteMultipartUpload(completeMultipartUploadRequest);
		}

		// Token: 0x0400000E RID: 14
		public int BigFilePartSize = 150;
	}
}
