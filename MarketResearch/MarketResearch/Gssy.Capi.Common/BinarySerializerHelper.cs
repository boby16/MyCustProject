using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gssy.Capi.Common
{
	// Token: 0x02000002 RID: 2
	public class BinarySerializerHelper : ISerializerHelper
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002088 File Offset: 0x00000288
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002090 File Offset: 0x00000290
		public object SerializeObject { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002099 File Offset: 0x00000299
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020A1 File Offset: 0x000002A1
		public string FilePath { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020AA File Offset: 0x000002AA
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020B2 File Offset: 0x000002B2
		public string FileName { get; set; }

		// Token: 0x06000007 RID: 7 RVA: 0x000020BB File Offset: 0x000002BB
		public BinarySerializerHelper(object object_0, string string_0, string string_1)
		{
			this.SerializeObject = object_0;
			this.FilePath = string_0;
			this.FileName = string_1;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022CC File Offset: 0x000004CC
		public void Serialize<T>()
		{
			string path = Path.Combine(this.FilePath, this.FileName);
			IFormatter formatter = new BinaryFormatter();
			try
			{
				using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					formatter.Serialize(stream, this.SerializeObject);
					stream.Close();
				}
			}
			catch (Exception ex)
			{
				Logging.Info.WriteLog(GClass0.smethod_0("Bžɵͫѱնػ"), ex.Message + Environment.NewLine + ex.StackTrace);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002368 File Offset: 0x00000568
		public T Deserialize<T>()
		{
			string path = Path.Combine(this.FilePath, this.FileName);
			IFormatter formatter = new BinaryFormatter();
			T result;
			try
			{
				using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					T t = (T)((object)formatter.Deserialize(stream));
					stream.Close();
					result = t;
				}
			}
			catch (Exception ex)
			{
				Logging.Info.WriteLog(GClass0.smethod_0("Bžɵͫѱնػ"), ex.Message + Environment.NewLine + ex.StackTrace);
				result = default(T);
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002414 File Offset: 0x00000614
		public static void Serialize<T>(T gparam_0, string string_0, string string_1)
		{
			BinarySerializerHelper binarySerializerHelper = new BinarySerializerHelper(gparam_0, string_0, string_1);
			binarySerializerHelper.Serialize<T>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002438 File Offset: 0x00000638
		public static T Deserialize<T>(string string_0, string string_1)
		{
			BinarySerializerHelper binarySerializerHelper = new BinarySerializerHelper(default(T), string_0, string_1);
			return binarySerializerHelper.Deserialize<T>();
		}
	}
}
