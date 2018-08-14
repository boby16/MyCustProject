using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Gssy.Capi.Entities.BaiduJson;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000006 RID: 6
	public class BaiduMapHelper
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00005A74 File Offset: 0x00003C74
		public JGeocoding GetGeocodingFromAddress(string string_0, string string_1)
		{
			string requestUriString = string.Format(global::GClass0.smethod_0("%ĸȿ̺ѳէ٨ܧ࠵भ੭ଯఠരฑཛྷၜᅕ቟ፏᐗᕛᙘ᝛᠚ᥓᩖ᭝᱒ᵟṋὋ\u205f℃≝⌘␆┗♆❂⡁⥖⩆⭑ⱒⴝ⹤⼬だㄺ㉸㍳㑭㕡㘪㝭㠤㥩㨵㭽㱤㵤㹿㽻䁹䄱䉡䍹䑦䕦䘡䝧䡮䤹䩸䬲䱼"), this.BaiduAk, string_0, string_1);
			JGeocoding jgeocoding = new JGeocoding();
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding(global::GClass0.smethod_0("pŰɥ̯й")));
				string text = streamReader.ReadToEnd();
				streamReader.Close();
				streamReader.Dispose();
				responseStream.Close();
				string input = text;
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				jgeocoding = javaScriptSerializer.Deserialize<JGeocoding>(input);
			}
			catch (Exception)
			{
				jgeocoding.status = 999;
			}
			return jgeocoding;
		}

		// Token: 0x04000034 RID: 52
		private string BaiduAk = global::GClass0.smethod_0("{Ÿɴ͗ѹձثܦ࡝३਼ୠౢസ฽ཧၧᅉቯ፼ᐼᕬᙺᝫ");
	}
}
