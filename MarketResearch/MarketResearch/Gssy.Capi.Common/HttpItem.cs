using System;
using System.Net;
using System.Text;

namespace Gssy.Capi.Common
{
	// Token: 0x02000008 RID: 8
	public class HttpItem
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003DC4 File Offset: 0x00001FC4
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002113 File Offset: 0x00000313
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003DDC File Offset: 0x00001FDC
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000211C File Offset: 0x0000031C
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003DF4 File Offset: 0x00001FF4
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002125 File Offset: 0x00000325
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003E0C File Offset: 0x0000200C
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000212E File Offset: 0x0000032E
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003E24 File Offset: 0x00002024
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002137 File Offset: 0x00000337
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003E3C File Offset: 0x0000203C
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002140 File Offset: 0x00000340
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003E54 File Offset: 0x00002054
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002149 File Offset: 0x00000349
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003E6C File Offset: 0x0000206C
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002152 File Offset: 0x00000352
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003E84 File Offset: 0x00002084
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000215B File Offset: 0x0000035B
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003E9C File Offset: 0x0000209C
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002164 File Offset: 0x00000364
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003EB4 File Offset: 0x000020B4
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003ECC File Offset: 0x000020CC
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002176 File Offset: 0x00000376
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003EE4 File Offset: 0x000020E4
		// (set) Token: 0x0600004B RID: 75 RVA: 0x0000217F File Offset: 0x0000037F
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003EFC File Offset: 0x000020FC
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002188 File Offset: 0x00000388
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003F14 File Offset: 0x00002114
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002191 File Offset: 0x00000391
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003F2C File Offset: 0x0000212C
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000219A File Offset: 0x0000039A
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003F40 File Offset: 0x00002140
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000021A3 File Offset: 0x000003A3
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003F54 File Offset: 0x00002154
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000021AC File Offset: 0x000003AC
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003F6C File Offset: 0x0000216C
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000021B5 File Offset: 0x000003B5
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003F84 File Offset: 0x00002184
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000021BE File Offset: 0x000003BE
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003F9C File Offset: 0x0000219C
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000021C7 File Offset: 0x000003C7
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003FB4 File Offset: 0x000021B4
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000021D0 File Offset: 0x000003D0
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003FCC File Offset: 0x000021CC
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000021D9 File Offset: 0x000003D9
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

		// Token: 0x0400001E RID: 30
		private string _URL = string.Empty;

		// Token: 0x0400001F RID: 31
		private string _Method = GClass0.smethod_0("DŇɕ");

		// Token: 0x04000020 RID: 32
		private int _Timeout = 100000;

		// Token: 0x04000021 RID: 33
		private int _ReadWriteTimeout = 30000;

		// Token: 0x04000022 RID: 34
		private string _Accept = GClass0.smethod_0("QŁɛ͖ЎՈ٫ݳࡱर਻୻౩൨๻ཿၶᅵቧ፻ᑾᕾᘠ᝶ᡥ᥸ᩦ᭦ᰢᵰṪὪ\u2029ℤ∩⌭␫");

		// Token: 0x04000023 RID: 35
		private string _ContentType = GClass0.smethod_0("}ŭɿͲЪլٷݯ࡭");

		// Token: 0x04000024 RID: 36
		private string _UserAgent = GClass0.smethod_0("rőɇ͕їՖ٘ܗࠂघਅଔఛ൑๞ཝၟᅏ቙ፅᑉᕆᙌᜓ᠇ᥫ᩶᭭ᱦᴂḘἎ\u202f℥∽⍋⑲╴♽❷⡠⥥⨵⭚ⱇⴲ⸧⼾〾ㄵ㈭㍘㑹㕣㙭㝭㡩㥲㨪㬱㰭㴲㸨");

		// Token: 0x04000025 RID: 37
		private Encoding _Encoding = null;

		// Token: 0x04000026 RID: 38
		private PostDataType _PostDataType = PostDataType.String;

		// Token: 0x04000027 RID: 39
		private string _Postdata = string.Empty;

		// Token: 0x04000028 RID: 40
		private byte[] _PostdataByte = null;

		// Token: 0x04000029 RID: 41
		private CookieCollection cookiecollection = null;

		// Token: 0x0400002A RID: 42
		private string _Cookie = string.Empty;

		// Token: 0x0400002B RID: 43
		private string _Referer = string.Empty;

		// Token: 0x0400002C RID: 44
		private string _CerPath = string.Empty;

		// Token: 0x0400002D RID: 45
		private bool isToLower = false;

		// Token: 0x0400002E RID: 46
		private bool allowautoredirect = false;

		// Token: 0x0400002F RID: 47
		private int connectionlimit = 1024;

		// Token: 0x04000030 RID: 48
		private string proxyusername = string.Empty;

		// Token: 0x04000031 RID: 49
		private string proxypwd = string.Empty;

		// Token: 0x04000032 RID: 50
		private string proxyip = string.Empty;

		// Token: 0x04000033 RID: 51
		private ResultType resulttype = ResultType.String;

		// Token: 0x04000034 RID: 52
		private WebHeaderCollection header = new WebHeaderCollection();
	}
}
