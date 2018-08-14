using System;
using System.Net;

namespace Gssy.Capi.Common
{
	// Token: 0x02000009 RID: 9
	public class HttpResult
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000040EC File Offset: 0x000022EC
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000021E2 File Offset: 0x000003E2
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004104 File Offset: 0x00002304
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000021EB File Offset: 0x000003EB
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000411C File Offset: 0x0000231C
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000021F4 File Offset: 0x000003F4
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004134 File Offset: 0x00002334
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000021FD File Offset: 0x000003FD
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000414C File Offset: 0x0000234C
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002206 File Offset: 0x00000406
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004164 File Offset: 0x00002364
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000220F File Offset: 0x0000040F
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000417C File Offset: 0x0000237C
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002218 File Offset: 0x00000418
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

		// Token: 0x04000035 RID: 53
		private string _Cookie = string.Empty;

		// Token: 0x04000036 RID: 54
		private CookieCollection cookiecollection = new CookieCollection();

		// Token: 0x04000037 RID: 55
		private string html = string.Empty;

		// Token: 0x04000038 RID: 56
		private byte[] resultbyte = null;

		// Token: 0x04000039 RID: 57
		private WebHeaderCollection header = new WebHeaderCollection();

		// Token: 0x0400003A RID: 58
		private string statusDescription = GClass0.smethod_0("");

		// Token: 0x0400003B RID: 59
		private HttpStatusCode statusCode = HttpStatusCode.OK;
	}
}
