using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class SurveyConfig
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002245 File Offset: 0x00000445
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000224D File Offset: 0x0000044D
		public int ID { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002256 File Offset: 0x00000456
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000225E File Offset: 0x0000045E
		public string CODE { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002267 File Offset: 0x00000467
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000226F File Offset: 0x0000046F
		public string CODE_TEXT { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002278 File Offset: 0x00000478
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002280 File Offset: 0x00000480
		public string CODE_NOTE { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002289 File Offset: 0x00000489
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002291 File Offset: 0x00000491
		public string PARENT_CODE { get; set; }

		// Token: 0x0400001E RID: 30
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x0400001F RID: 31
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x04000020 RID: 32
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000021 RID: 33
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x04000022 RID: 34
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;
	}
}
