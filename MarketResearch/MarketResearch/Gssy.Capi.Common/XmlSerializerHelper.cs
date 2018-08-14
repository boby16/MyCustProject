using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Gssy.Capi.Common
{
	// Token: 0x0200001B RID: 27
	public class XmlSerializerHelper : ISerializerHelper
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000227B File Offset: 0x0000047B
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002283 File Offset: 0x00000483
		public object SerializeObject { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000228C File Offset: 0x0000048C
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002294 File Offset: 0x00000494
		public string FilePath { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000229D File Offset: 0x0000049D
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000022A5 File Offset: 0x000004A5
		public string FileName { get; set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x000022AE File Offset: 0x000004AE
		public XmlSerializerHelper(object object_0, string string_0, string string_1)
		{
			this.SerializeObject = object_0;
			this.FilePath = string_0;
			this.FileName = string_1;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008204 File Offset: 0x00006404
		public void Serialize<T>()
		{
			string path = Path.Combine(this.FilePath, this.FileName);
			using (StringWriter stringWriter = new StringWriter())
			{
				try
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
					xmlSerializer.Serialize(stringWriter, this.SerializeObject);
					string s = SecurityManager.Encrypt(stringWriter.ToString());
					byte[] bytes = Encoding.Unicode.GetBytes(s);
					using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
					{
						try
						{
							stream.Write(bytes, 0, bytes.Length);
						}
						finally
						{
							stream.Flush();
							stream.Close();
							stream.Dispose();
						}
					}
				}
				catch (Exception ex)
				{
					Logging.Info.WriteLog(GClass0.smethod_0("Bžɵͫѱնػ"), ex.Message + Environment.NewLine + ex.StackTrace);
				}
				finally
				{
					stringWriter.Flush();
					stringWriter.Close();
					stringWriter.Dispose();
				}
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008334 File Offset: 0x00006534
		public T Deserialize<T>()
		{
			string path = Path.Combine(this.FilePath, this.FileName);
			T result = default(T);
			try
			{
				using (StreamReader streamReader = new StreamReader(path, Encoding.Unicode))
				{
					string string_ = streamReader.ReadToEnd();
					string s = SecurityManager.Decrypt(string_);
					byte[] bytes = Encoding.Unicode.GetBytes(s);
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
						result = (T)((object)xmlSerializer.Deserialize(memoryStream));
						memoryStream.Flush();
						memoryStream.Close();
						memoryStream.Dispose();
					}
					streamReader.Close();
					streamReader.Dispose();
				}
			}
			catch (Exception ex)
			{
				Logging.Info.WriteLog(GClass0.smethod_0("Bžɵͫѱնػ"), ex.Message + Environment.NewLine + ex.StackTrace);
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008448 File Offset: 0x00006648
		public static void Serialize<T>(T gparam_0, string string_0, string string_1)
		{
			XmlSerializerHelper xmlSerializerHelper = new XmlSerializerHelper(gparam_0, string_0, string_1);
			xmlSerializerHelper.Serialize<T>();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000846C File Offset: 0x0000666C
		public static T Deserialize<T>(string string_0, string string_1)
		{
			XmlSerializerHelper xmlSerializerHelper = new XmlSerializerHelper(default(T), string_0, string_1);
			return xmlSerializerHelper.Deserialize<T>();
		}
	}
}
