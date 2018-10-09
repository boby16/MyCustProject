using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LoyalFilial.MarketResearch.Common
{
	public class XmlSerializerHelper : ISerializerHelper
	{
		public object SerializeObject { get; set; }

		public string FilePath { get; set; }

		public string FileName { get; set; }

		public XmlSerializerHelper(object object_0, string string_0, string string_1)
		{
			this.SerializeObject = object_0;
			this.FilePath = string_0;
			this.FileName = string_1;
		}

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
					Logging.Info.WriteLog("Export:", ex.Message + Environment.NewLine + ex.StackTrace);
				}
				finally
				{
					stringWriter.Flush();
					stringWriter.Close();
					stringWriter.Dispose();
				}
			}
		}

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
				Logging.Info.WriteLog("Export:", ex.Message + Environment.NewLine + ex.StackTrace);
			}
			return result;
		}

		public static void Serialize<T>(T gparam_0, string string_0, string string_1)
		{
			XmlSerializerHelper xmlSerializerHelper = new XmlSerializerHelper(gparam_0, string_0, string_1);
			xmlSerializerHelper.Serialize<T>();
		}

		public static T Deserialize<T>(string string_0, string string_1)
		{
			XmlSerializerHelper xmlSerializerHelper = new XmlSerializerHelper(default(T), string_0, string_1);
			return xmlSerializerHelper.Deserialize<T>();
		}
	}
}
