using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Gssy.Capi.Common
{
	public class EncryptTool
	{
		public static string Encrypt(string string_0, string string_1)
		{
			string result;
			using (DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(string_0);
				descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(string_1);
				descryptoServiceProvider.IV = Encoding.ASCII.GetBytes(string_1);
				MemoryStream memoryStream = new MemoryStream();
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cryptoStream.Write(bytes, 0, bytes.Length);
					cryptoStream.FlushFinalBlock();
					cryptoStream.Close();
				}
				string text = Convert.ToBase64String(memoryStream.ToArray());
				memoryStream.Close();
				result = text;
			}
			return result;
		}

		public static string Decrypt(string string_0, string string_1)
		{
			byte[] array = Convert.FromBase64String(string_0);
			string result;
			using (DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider())
			{
				descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(string_1);
				descryptoServiceProvider.IV = Encoding.ASCII.GetBytes(string_1);
				MemoryStream memoryStream = new MemoryStream();
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cryptoStream.Write(array, 0, array.Length);
					cryptoStream.FlushFinalBlock();
					cryptoStream.Close();
				}
				string @string = Encoding.UTF8.GetString(memoryStream.ToArray());
				memoryStream.Close();
				result = @string;
			}
			return result;
		}

		public static void EncryptTextToFile(string string_0, string string_1, byte[] byte_0, byte[] byte_1)
		{
			try
			{
				FileStream fileStream = File.Open(string_1, FileMode.OpenOrCreate);
				Rijndael rijndael = Rijndael.Create();
				CryptoStream cryptoStream = new CryptoStream(fileStream, rijndael.CreateEncryptor(byte_0, byte_1), CryptoStreamMode.Write);
				StreamWriter streamWriter = new StreamWriter(cryptoStream);
				try
				{
					streamWriter.WriteLine(string_0);
				}
				catch (Exception ex)
				{
					Logging.Error.WriteLog(GClass0.smethod_0("WŻȴͶѠգٿݽ࠮ॢ੯୨౿ൻ๺རၢᄿሤ፸ᐲᕼ"), ex.Message);
				}
				finally
				{
					streamWriter.Close();
					cryptoStream.Close();
					fileStream.Close();
				}
			}
			catch (CryptographicException ex2)
			{
				Console.WriteLine(GClass0.smethod_0("bĂɢ͒Ѧծ٩ݳࡼ२੸୨౿ൿ๶༴ၶᅠባ፿ᑽᔮᙢᝯᡨ᥿᩻᭺ᱢᵢḿἤ⁸Ⅎ≼"), ex2.Message);
			}
			catch (UnauthorizedAccessException ex3)
			{
				Console.WriteLine(GClass0.smethod_0("[Ĺɾ;Ѻհشݶࡠॣ੿୽మൢ๯ཨၿᅻቺ።ᑢᔿᘤ᝸ᠲ᥼"), ex3.Message);
			}
		}

		public static string DecryptTextFromFile(string string_0, byte[] byte_0, byte[] byte_1)
		{
			string result;
			try
			{
				FileStream fileStream = File.Open(string_0, FileMode.OpenOrCreate);
				Rijndael rijndael = Rijndael.Create();
				CryptoStream cryptoStream = new CryptoStream(fileStream, rijndael.CreateDecryptor(byte_0, byte_1), CryptoStreamMode.Read);
				StreamReader streamReader = new StreamReader(cryptoStream);
				string text = null;
				try
				{
					text = streamReader.ReadLine();
				}
				catch (Exception ex)
				{
					Logging.Error.WriteLog(GClass0.smethod_0("WŻȴͶѠգٿݽ࠮ॢ੯୨౿ൻ๺རၢᄿሤ፸ᐲᕼ"), ex.Message);
				}
				finally
				{
					streamReader.Close();
					cryptoStream.Close();
					fileStream.Close();
				}
				result = text;
			}
			catch (CryptographicException ex2)
			{
				Logging.Error.WriteLog(GClass0.smethod_0("bĂɢ͒Ѧծ٩ݳࡼ२੸୨౿ൿ๶༴ၶᅠባ፿ᑽᔮᙢᝯᡨ᥿᩻᭺ᱢᵢḿἤ⁸Ⅎ≼"), ex2.Message);
				result = null;
			}
			catch (UnauthorizedAccessException ex3)
			{
				Logging.Error.WriteLog(GClass0.smethod_0("WŻȴͶѠգٿݽ࠮ॢ੯୨౿ൻ๺རၢᄿሤ፸ᐲᕼ"), ex3.Message);
				result = null;
			}
			return result;
		}
	}
}
