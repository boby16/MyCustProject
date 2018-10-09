using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Gssy.Capi.Common
{
	public class SecurityManager
	{
		public static string Encrypt(string string_0)
		{
			string result;
			if (string.IsNullOrEmpty(string_0))
			{
				result = string_0;
			}
			else
			{
				result = SecurityManager.Encrypt(string_0, "Dylan_Depp", "7C401");
			}
			return result;
		}

		public static string Encrypt(string string_0, string string_1, string string_2)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(string_0);
				byte[] bytes2 = Encoding.UTF8.GetBytes(string_1);
				AesManaged aesManaged = new AesManaged();
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(string_2, bytes2);
				aesManaged.BlockSize = aesManaged.LegalBlockSizes[0].MaxSize;
				aesManaged.KeySize = aesManaged.LegalKeySizes[0].MaxSize;
				aesManaged.Key = rfc2898DeriveBytes.GetBytes(aesManaged.KeySize / 8);
				aesManaged.IV = rfc2898DeriveBytes.GetBytes(aesManaged.BlockSize / 8);
				ICryptoTransform transform = aesManaged.CreateEncryptor();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.Close();
				string text = Convert.ToBase64String(memoryStream.ToArray());
				result = text;
			}
			catch
			{
				result = string_0;
			}
			return result;
		}

		public static string Decrypt(string string_0)
		{
			string result;
			if (string.IsNullOrEmpty(string_0))
			{
				result = string_0;
			}
			else
			{
				result = SecurityManager.Decrypt(string_0, "Dylan_Depp", "7C401");
			}
			return result;
		}

		public static string Decrypt(string string_0, string string_1, string string_2)
		{
			string result;
			try
			{
				byte[] array = Convert.FromBase64String(string_0);
				byte[] bytes = Encoding.UTF8.GetBytes(string_1);
				AesManaged aesManaged = new AesManaged();
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(string_2, bytes);
				aesManaged.BlockSize = aesManaged.LegalBlockSizes[0].MaxSize;
				aesManaged.KeySize = aesManaged.LegalKeySizes[0].MaxSize;
				aesManaged.Key = rfc2898DeriveBytes.GetBytes(aesManaged.KeySize / 8);
				aesManaged.IV = rfc2898DeriveBytes.GetBytes(aesManaged.BlockSize / 8);
				ICryptoTransform transform = aesManaged.CreateDecryptor();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.Close();
				byte[] array2 = memoryStream.ToArray();
				string @string = Encoding.UTF8.GetString(array2, 0, array2.Length);
				result = @string;
			}
			catch
			{
				result = string_0;
			}
			return result;
		}

		private const string DEFAULT_SALT = "Dylan_Depp";

		private const string DEFAULT_PWD = "7C401";
	}
}
