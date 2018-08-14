using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class SurveyWebLog
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000030AE File Offset: 0x000012AE
		// (set) Token: 0x06000202 RID: 514 RVA: 0x000030B6 File Offset: 0x000012B6
		public int ID { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000030BF File Offset: 0x000012BF
		// (set) Token: 0x06000204 RID: 516 RVA: 0x000030C7 File Offset: 0x000012C7
		public string SURVEY_ID { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000030D0 File Offset: 0x000012D0
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000030D8 File Offset: 0x000012D8
		public string SURVEY_GUID { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000030E1 File Offset: 0x000012E1
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000030E9 File Offset: 0x000012E9
		public string URI_FULL { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000030F2 File Offset: 0x000012F2
		// (set) Token: 0x0600020A RID: 522 RVA: 0x000030FA File Offset: 0x000012FA
		public string URI_DOMAIN { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00003103 File Offset: 0x00001303
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000310B File Offset: 0x0000130B
		public string URI_DOMAIN_TWO { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00003114 File Offset: 0x00001314
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000311C File Offset: 0x0000131C
		public DateTime? BEGIN_TIME { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00003125 File Offset: 0x00001325
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000312D File Offset: 0x0000132D
		public DateTime? END_TIME { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00003136 File Offset: 0x00001336
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000313E File Offset: 0x0000133E
		public int STAY_TIME { get; set; }

		// Token: 0x040000F7 RID: 247
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000F8 RID: 248
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x040000F9 RID: 249
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x040000FA RID: 250
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x040000FB RID: 251
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		// Token: 0x040000FC RID: 252
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_4;

		// Token: 0x040000FD RID: 253
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_0;

		// Token: 0x040000FE RID: 254
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_1;

		// Token: 0x040000FF RID: 255
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;
	}
}
