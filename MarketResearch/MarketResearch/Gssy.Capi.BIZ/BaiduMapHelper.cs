using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Gssy.Capi.Entities.BaiduJson;

namespace Gssy.Capi.BIZ
{
	public class BaiduMapHelper
	{
		public JGeocoding GetGeocodingFromAddress(string string_0, string string_1)
		{
			string requestUriString = string.Format("http://api.map.baidu.com/geocoder/v2/?address={2}&city={1}&output=json&ak={0}", this.BaiduAk, string_0, string_1);
			JGeocoding jgeocoding = new JGeocoding();
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
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

		private string BaiduAk = "cobBmb97Mf2mn37noNiy8oxj";
	}
}
