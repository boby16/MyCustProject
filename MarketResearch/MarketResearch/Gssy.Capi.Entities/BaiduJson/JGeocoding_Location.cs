using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities.BaiduJson
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class JGeocoding_Location
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00003A42 File Offset: 0x00001C42
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00003A4A File Offset: 0x00001C4A
		public float lat { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00003A53 File Offset: 0x00001C53
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00003A5B File Offset: 0x00001C5B
		public float lng { get; set; }

		// Token: 0x04000187 RID: 391
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float float_0;

		// Token: 0x04000188 RID: 392
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float float_1;
	}
}
