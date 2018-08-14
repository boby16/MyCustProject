using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Gssy.Capi.Common
{
	// Token: 0x02000007 RID: 7
	public class HttpHelper
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000035CC File Offset: 0x000017CC
		private HttpResult method_0(HttpItem httpItem_0)
		{
			HttpResult httpResult = new HttpResult();
			try
			{
				using (this.response = (HttpWebResponse)this.request.GetResponse())
				{
					httpResult.StatusCode = this.response.StatusCode;
					httpResult.StatusDescription = this.response.StatusDescription;
					httpResult.Header = this.response.Headers;
					if (this.response.Cookies != null)
					{
						httpResult.CookieCollection = this.response.Cookies;
					}
					if (this.response.Headers[GClass0.smethod_0("yŬɼ̪ѥժ٫ݨ࡫।")] != null)
					{
						httpResult.Cookie = this.response.Headers[GClass0.smethod_0("yŬɼ̪ѥժ٫ݨ࡫।")];
					}
					MemoryStream memoryStream = new MemoryStream();
					if (this.response.ContentEncoding != null && this.response.ContentEncoding.Equals(GClass0.smethod_0("cŹɫͱ"), StringComparison.InvariantCultureIgnoreCase))
					{
						memoryStream = HttpHelper.smethod_0(new GZipStream(this.response.GetResponseStream(), CompressionMode.Decompress));
					}
					else
					{
						memoryStream = HttpHelper.smethod_0(this.response.GetResponseStream());
					}
					byte[] array = memoryStream.ToArray();
					memoryStream.Close();
					if (httpItem_0.ResultType == ResultType.Byte)
					{
						httpResult.ResultByte = array;
					}
					if (this.encoding == null)
					{
						Match match = Regex.Match(Encoding.Default.GetString(array), GClass0.smethod_0("#ųɸͨѺԲق݆ࠫोਿଽ౰ൺ๰རၼᅫቹጱᐣᕑᙗ᜴ᡚ᤬ᨬ᭟ᰡᴥṜ"), RegexOptions.IgnoreCase);
						string text = (match.Groups.Count > 2) ? match.Groups[2].Value.ToLower() : string.Empty;
						text = text.Replace(GClass0.smethod_0("#"), GClass0.smethod_0("")).Replace(GClass0.smethod_0("&"), GClass0.smethod_0("")).Replace(GClass0.smethod_0(":"), GClass0.smethod_0("")).Replace(GClass0.smethod_0("cźɧ̪оԽرܺ࠯र"), GClass0.smethod_0("dŠɪ"));
						if (text.Length > 2)
						{
							this.encoding = Encoding.GetEncoding(text.Trim());
						}
						else if (string.IsNullOrEmpty(this.response.CharacterSet))
						{
							this.encoding = Encoding.UTF8;
						}
						else
						{
							this.encoding = Encoding.GetEncoding(this.response.CharacterSet);
						}
					}
					httpResult.Html = this.encoding.GetString(array);
				}
			}
			catch (WebException ex)
			{
				this.response = (HttpWebResponse)ex.Response;
				httpResult.Html = ex.Message;
				HttpStatusCode statusCode = this.response.StatusCode;
				httpResult.StatusCode = this.response.StatusCode;
				httpResult.StatusDescription = this.response.StatusDescription;
			}
			catch (Exception ex2)
			{
				httpResult.Html = ex2.Message;
			}
			if (httpItem_0.IsToLower)
			{
				httpResult.Html = httpResult.Html.ToLower();
			}
			return httpResult;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003910 File Offset: 0x00001B10
		private static MemoryStream smethod_0(Stream stream_0)
		{
			MemoryStream memoryStream = new MemoryStream();
			int count = 256;
			byte[] buffer = new byte[256];
			for (int i = stream_0.Read(buffer, 0, 256); i > 0; i = stream_0.Read(buffer, 0, count))
			{
				memoryStream.Write(buffer, 0, i);
			}
			return memoryStream;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003964 File Offset: 0x00001B64
		private void method_1(HttpItem httpItem_0)
		{
			this.method_2(httpItem_0);
			if (httpItem_0.Header != null && httpItem_0.Header.Count > 0)
			{
				foreach (string name in httpItem_0.Header.AllKeys)
				{
					this.request.Headers.Add(name, httpItem_0.Header[name]);
				}
			}
			this.method_5(httpItem_0);
			this.request.Method = httpItem_0.Method;
			this.request.Timeout = httpItem_0.Timeout;
			this.request.ReadWriteTimeout = httpItem_0.ReadWriteTimeout;
			this.request.Accept = httpItem_0.Accept;
			this.request.ContentType = httpItem_0.ContentType;
			this.request.UserAgent = httpItem_0.UserAgent;
			this.encoding = httpItem_0.Encoding;
			this.method_3(httpItem_0);
			this.request.Referer = httpItem_0.Referer;
			this.request.AllowAutoRedirect = httpItem_0.Allowautoredirect;
			this.method_4(httpItem_0);
			if (httpItem_0.Connectionlimit > 0)
			{
				this.request.ServicePoint.ConnectionLimit = httpItem_0.Connectionlimit;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003A98 File Offset: 0x00001C98
		private void method_2(HttpItem httpItem_0)
		{
			if (!string.IsNullOrEmpty(httpItem_0.CerPath))
			{
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
				this.request = (HttpWebRequest)WebRequest.Create(httpItem_0.URL);
				this.request.ClientCertificates.Add(new X509Certificate(httpItem_0.CerPath));
			}
			else
			{
				this.request = (HttpWebRequest)WebRequest.Create(httpItem_0.URL);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003B10 File Offset: 0x00001D10
		private void method_3(HttpItem httpItem_0)
		{
			if (!string.IsNullOrEmpty(httpItem_0.Cookie))
			{
				this.request.Headers[HttpRequestHeader.Cookie] = httpItem_0.Cookie;
			}
			if (httpItem_0.CookieCollection != null)
			{
				this.request.CookieContainer = new CookieContainer();
				this.request.CookieContainer.Add(httpItem_0.CookieCollection);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003B78 File Offset: 0x00001D78
		private void method_4(HttpItem httpItem_0)
		{
			if (this.request.Method.Trim().ToLower().Contains(GClass0.smethod_0("tŬɱ͵")))
			{
				byte[] array = null;
				if (httpItem_0.PostDataType == PostDataType.Byte && httpItem_0.PostdataByte != null && httpItem_0.PostdataByte.Length != 0)
				{
					array = httpItem_0.PostdataByte;
				}
				else if (httpItem_0.PostDataType == PostDataType.FilePath && !string.IsNullOrEmpty(httpItem_0.Postdata))
				{
					StreamReader streamReader = new StreamReader(httpItem_0.Postdata, this.encoding);
					array = Encoding.Default.GetBytes(streamReader.ReadToEnd());
					streamReader.Close();
				}
				else if (!string.IsNullOrEmpty(httpItem_0.Postdata))
				{
					array = Encoding.Default.GetBytes(httpItem_0.Postdata);
				}
				if (array != null)
				{
					this.request.ContentLength = (long)array.Length;
					this.request.GetRequestStream().Write(array, 0, array.Length);
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003C70 File Offset: 0x00001E70
		private void method_5(HttpItem httpItem_0)
		{
			if (!string.IsNullOrEmpty(httpItem_0.ProxyIp))
			{
				if (httpItem_0.ProxyIp.Contains(GClass0.smethod_0(";")))
				{
					string[] array = httpItem_0.ProxyIp.Split(new char[]
					{
						':'
					});
					WebProxy webProxy = new WebProxy(array[0].Trim(), Convert.ToInt32(array[1].Trim()));
					webProxy.Credentials = new NetworkCredential(httpItem_0.ProxyUserName, httpItem_0.ProxyPwd);
					this.request.Proxy = webProxy;
				}
				else
				{
					WebProxy webProxy2 = new WebProxy(httpItem_0.ProxyIp, false);
					webProxy2.Credentials = new NetworkCredential(httpItem_0.ProxyUserName, httpItem_0.ProxyPwd);
					this.request.Proxy = webProxy2;
				}
				this.request.Credentials = CredentialCache.DefaultNetworkCredentials;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003D44 File Offset: 0x00001F44
		public bool CheckValidationResult(object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0)
		{
			return true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003D54 File Offset: 0x00001F54
		public HttpResult GetHtml(HttpItem httpItem_0)
		{
			try
			{
				this.method_1(httpItem_0);
			}
			catch (Exception ex)
			{
				return new HttpResult
				{
					Cookie = GClass0.smethod_0(""),
					Header = null,
					Html = ex.Message,
					StatusDescription = GClass0.smethod_0("酊繨凇茇懵枧錘")
				};
			}
			return this.method_0(httpItem_0);
		}

		// Token: 0x0400001B RID: 27
		private Encoding encoding = Encoding.Default;

		// Token: 0x0400001C RID: 28
		private HttpWebRequest request = null;

		// Token: 0x0400001D RID: 29
		private HttpWebResponse response = null;
	}
}
