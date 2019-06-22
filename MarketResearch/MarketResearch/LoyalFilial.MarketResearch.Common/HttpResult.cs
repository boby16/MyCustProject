using System;
using System.Net;

namespace LoyalFilial.MarketResearch.Common
{
	public class HttpResult
	{
		public string Cookie
		{
			get
			{
				return this._Cookie;
			}
			set
			{
				this._Cookie = value;
			}
		}

		public CookieCollection CookieCollection
		{
			get
			{
				return this.cookiecollection;
			}
			set
			{
				this.cookiecollection = value;
			}
		}

		public string Html
		{
			get
			{
				return this.html;
			}
			set
			{
				this.html = value;
			}
		}

		public byte[] ResultByte
		{
			get
			{
				return this.resultbyte;
			}
			set
			{
				this.resultbyte = value;
			}
		}

		public WebHeaderCollection Header
		{
			get
			{
				return this.header;
			}
			set
			{
				this.header = value;
			}
		}

		public string StatusDescription
		{
			get
			{
				return this.statusDescription;
			}
			set
			{
				this.statusDescription = value;
			}
		}

		public HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		private string _Cookie = string.Empty;

		private CookieCollection cookiecollection = new CookieCollection();

		private string html = string.Empty;

		private byte[] resultbyte = null;

		private WebHeaderCollection header = new WebHeaderCollection();

		private string statusDescription = "";

		private HttpStatusCode statusCode = HttpStatusCode.OK;
	}
}
