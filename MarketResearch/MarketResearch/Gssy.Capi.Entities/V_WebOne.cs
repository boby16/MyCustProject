using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class V_WebOne
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000038FF File Offset: 0x00001AFF
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00003907 File Offset: 0x00001B07
		public string SURVEY_ID { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00003910 File Offset: 0x00001B10
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00003918 File Offset: 0x00001B18
		public string URI_DOMAIN { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00003921 File Offset: 0x00001B21
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00003929 File Offset: 0x00001B29
		public int STAY_TIME { get; set; }

		// Token: 0x04000174 RID: 372
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000175 RID: 373
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x04000176 RID: 374
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;
	}
}
