using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class SurveyUsers
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003048 File Offset: 0x00001248
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00003050 File Offset: 0x00001250
		public int ID { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00003059 File Offset: 0x00001259
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00003061 File Offset: 0x00001261
		public string USER_ID { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000306A File Offset: 0x0000126A
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00003072 File Offset: 0x00001272
		public string USER_NAME { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000307B File Offset: 0x0000127B
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00003083 File Offset: 0x00001283
		public int IS_ENABLE { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000308C File Offset: 0x0000128C
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00003094 File Offset: 0x00001294
		public string PSW { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000309D File Offset: 0x0000129D
		// (set) Token: 0x060001FF RID: 511 RVA: 0x000030A5 File Offset: 0x000012A5
		public string ROLE_ID { get; set; }

		// Token: 0x040000F1 RID: 241
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x040000F2 RID: 242
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040000F3 RID: 243
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x040000F4 RID: 244
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040000F5 RID: 245
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		// Token: 0x040000F6 RID: 246
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;
	}
}
