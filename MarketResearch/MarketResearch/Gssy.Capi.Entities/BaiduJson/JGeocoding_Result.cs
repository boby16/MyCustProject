using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities.BaiduJson
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class JGeocoding_Result
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000039FE File Offset: 0x00001BFE
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00003A06 File Offset: 0x00001C06
		public JGeocoding_Location location { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00003A0F File Offset: 0x00001C0F
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00003A17 File Offset: 0x00001C17
		public int precise { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00003A20 File Offset: 0x00001C20
		// (set) Token: 0x0600032D RID: 813 RVA: 0x00003A28 File Offset: 0x00001C28
		public int confidencee { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00003A31 File Offset: 0x00001C31
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00003A39 File Offset: 0x00001C39
		public string level { get; set; }

		// Token: 0x04000183 RID: 387
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private JGeocoding_Location jgeocoding_Location_0;

		// Token: 0x04000184 RID: 388
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x04000185 RID: 389
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		// Token: 0x04000186 RID: 390
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;
	}
}
