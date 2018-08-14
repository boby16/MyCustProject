using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class SurveySync
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00002FE2 File Offset: 0x000011E2
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00002FEA File Offset: 0x000011EA
		public int ID { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00002FF3 File Offset: 0x000011F3
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00002FFB File Offset: 0x000011FB
		public string SURVEY_ID { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00003004 File Offset: 0x00001204
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000300C File Offset: 0x0000120C
		public string SURVEY_GUID { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00003015 File Offset: 0x00001215
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000301D File Offset: 0x0000121D
		public int SYNC_STATE { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00003026 File Offset: 0x00001226
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000302E File Offset: 0x0000122E
		public DateTime? SYNC_DATE { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00003037 File Offset: 0x00001237
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000303F File Offset: 0x0000123F
		public string SYNC_NOTE { get; set; }

		// Token: 0x040000EB RID: 235
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x040000EC RID: 236
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040000ED RID: 237
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x040000EE RID: 238
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		// Token: 0x040000EF RID: 239
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DateTime? nullable_0;

		// Token: 0x040000F0 RID: 240
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;
	}
}
