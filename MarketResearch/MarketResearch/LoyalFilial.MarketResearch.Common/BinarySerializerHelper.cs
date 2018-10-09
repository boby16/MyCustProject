using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LoyalFilial.MarketResearch.Common
{
	public class BinarySerializerHelper : ISerializerHelper
	{
		public object SerializeObject { get; set; }

		public string FilePath { get; set; }

		public string FileName { get; set; }

		public BinarySerializerHelper(object object_0, string string_0, string string_1)
		{
			this.SerializeObject = object_0;
			this.FilePath = string_0;
			this.FileName = string_1;
		}

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
				Logging.Info.WriteLog("Export:", ex.Message + Environment.NewLine + ex.StackTrace);
			}
		}

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
				Logging.Info.WriteLog("Export:", ex.Message + Environment.NewLine + ex.StackTrace);
				result = default(T);
			}
			return result;
		}

		public static void Serialize<T>(T gparam_0, string string_0, string string_1)
		{
			BinarySerializerHelper binarySerializerHelper = new BinarySerializerHelper(gparam_0, string_0, string_1);
			binarySerializerHelper.Serialize<T>();
		}

		public static T Deserialize<T>(string string_0, string string_1)
		{
			BinarySerializerHelper binarySerializerHelper = new BinarySerializerHelper(default(T), string_0, string_1);
			return binarySerializerHelper.Deserialize<T>();
		}
	}
}
