using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities.BaiduJson
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class JGeocoding
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000039DC File Offset: 0x00001BDC
		// (set) Token: 0x06000324 RID: 804 RVA: 0x000039E4 File Offset: 0x00001BE4
		public int status { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000039ED File Offset: 0x00001BED
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000039F5 File Offset: 0x00001BF5
		public JGeocoding_Result result { get; set; }

		// Token: 0x04000181 RID: 385
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000182 RID: 386
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JGeocoding_Result jgeocoding_Result_0;
	}
}
