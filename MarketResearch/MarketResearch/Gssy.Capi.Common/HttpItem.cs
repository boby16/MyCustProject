using System;
using System.Net;
using System.Text;

namespace Gssy.Capi.Common
{
	public class HttpItem
	{
		public string URL
		{
			get
			{
				return this._URL;
			}
			set
			{
				this._URL = value;
			}
		}

		public string Method
		{
			get
			{
				return this._Method;
			}
			set
			{
				this._Method = value;
			}
		}

		public int Timeout
		{
			get
			{
				return this._Timeout;
			}
			set
			{
				this._Timeout = value;
			}
		}

		public int ReadWriteTimeout
		{
			get
			{
				return this._ReadWriteTimeout;
			}
			set
			{
				this._ReadWriteTimeout = value;
			}
		}

		public string Accept
		{
			get
			{
				return this._Accept;
			}
			set
			{
				this._Accept = value;
			}
		}

		public string ContentType
		{
			get
			{
				return this._ContentType;
			}
			set
			{
				this._ContentType = value;
			}
		}

		public string UserAgent
		{
			get
			{
				return this._UserAgent;
			}
			set
			{
				this._UserAgent = value;
			}
		}

		public Encoding Encoding
		{
			get
			{
				return this._Encoding;
			}
			set
			{
				this._Encoding = value;
			}
		}

		public PostDataType PostDataType
		{
			get
			{
				return this._PostDataType;
			}
			set
			{
				this._PostDataType = value;
			}
		}

		public string Postdata
		{
			get
			{
				return this._Postdata;
			}
			set
			{
				this._Postdata = value;
			}
		}

		public byte[] PostdataByte
		{
			get
			{
				return this._PostdataByte;
			}
			set
			{
				this._PostdataByte = value;
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

		public string Referer
		{
			get
			{
				return this._Referer;
			}
			set
			{
				this._Referer = value;
			}
		}

		public string CerPath
		{
			get
			{
				return this._CerPath;
			}
			set
			{
				this._CerPath = value;
			}
		}

		public bool IsToLower
		{
			get
			{
				return this.isToLower;
			}
			set
			{
				this.isToLower = value;
			}
		}

		public bool Allowautoredirect
		{
			get
			{
				return this.allowautoredirect;
			}
			set
			{
				this.allowautoredirect = value;
			}
		}

		public int Connectionlimit
		{
			get
			{
				return this.connectionlimit;
			}
			set
			{
				this.connectionlimit = value;
			}
		}

		public string ProxyUserName
		{
			get
			{
				return this.proxyusername;
			}
			set
			{
				this.proxyusername = value;
			}
		}

		public string ProxyPwd
		{
			get
			{
				return this.proxypwd;
			}
			set
			{
				this.proxypwd = value;
			}
		}

		public string ProxyIp
		{
			get
			{
				return this.proxyip;
			}
			set
			{
				this.proxyip = value;
			}
		}

		public ResultType ResultType
		{
			get
			{
				return this.resulttype;
			}
			set
			{
				this.resulttype = value;
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

		private string _URL = string.Empty;

		private string _Method = "GET";

		private int _Timeout = 100000;

		private int _ReadWriteTimeout = 30000;

		private string _Accept = "text/html, application/xhtml+xml, */*";

		private string _ContentType = "text/html";

		private string _UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";

		private Encoding _Encoding = null;

		private PostDataType _PostDataType = PostDataType.String;

		private string _Postdata = string.Empty;

		private byte[] _PostdataByte = null;

		private CookieCollection cookiecollection = null;

		private string _Cookie = string.Empty;

		private string _Referer = string.Empty;

		private string _CerPath = string.Empty;

		private bool isToLower = false;

		private bool allowautoredirect = false;

		private int connectionlimit = 1024;

		private string proxyusername = string.Empty;

		private string proxypwd = string.Empty;

		private string proxyip = string.Empty;

		private ResultType resulttype = ResultType.String;

		private WebHeaderCollection header = new WebHeaderCollection();
	}
}
