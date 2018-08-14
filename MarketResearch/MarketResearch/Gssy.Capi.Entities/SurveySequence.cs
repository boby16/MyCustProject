using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class SurveySequence
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00002EC1 File Offset: 0x000010C1
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00002EC9 File Offset: 0x000010C9
		public int ID { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00002ED2 File Offset: 0x000010D2
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00002EDA File Offset: 0x000010DA
		public string SURVEY_ID { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00002EE3 File Offset: 0x000010E3
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00002EEB File Offset: 0x000010EB
		public int SEQUENCE_ID { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00002EF4 File Offset: 0x000010F4
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00002EFC File Offset: 0x000010FC
		public string PAGE_ID { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00002F05 File Offset: 0x00001105
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00002F0D File Offset: 0x0000110D
		public int CIRCLE_A_CURRENT { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00002F16 File Offset: 0x00001116
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00002F1E File Offset: 0x0000111E
		public int CIRCLE_A_COUNT { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00002F27 File Offset: 0x00001127
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00002F2F File Offset: 0x0000112F
		public int CIRCLE_B_CURRENT { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00002F38 File Offset: 0x00001138
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00002F40 File Offset: 0x00001140
		public int CIRCLE_B_COUNT { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00002F49 File Offset: 0x00001149
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00002F51 File Offset: 0x00001151
		public int VERSION_ID { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00002F5A File Offset: 0x0000115A
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00002F62 File Offset: 0x00001162
		public DateTime? PAGE_BEGIN_TIME { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00002F6B File Offset: 0x0000116B
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00002F73 File Offset: 0x00001173
		public DateTime? PAGE_END_TIME { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00002F7C File Offset: 0x0000117C
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00002F84 File Offset: 0x00001184
		public string RECORD_FILE { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00002F8D File Offset: 0x0000118D
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00002F95 File Offset: 0x00001195
		public DateTime? RECORD_START_TIME { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00002F9E File Offset: 0x0000119E
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00002FA6 File Offset: 0x000011A6
		public int RECORD_BEGIN_TIME { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00002FAF File Offset: 0x000011AF
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00002FB7 File Offset: 0x000011B7
		public int RECORD_END_TIME { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00002FC0 File Offset: 0x000011C0
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00002FC8 File Offset: 0x000011C8
		public int PAGE_TIME { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00002FD1 File Offset: 0x000011D1
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00002FD9 File Offset: 0x000011D9
		public string SURVEY_GUID { get; set; }

		// Token: 0x040000DA RID: 218
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000DB RID: 219
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x040000DC RID: 220
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		// Token: 0x040000DD RID: 221
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x040000DE RID: 222
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040000DF RID: 223
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		// Token: 0x040000E0 RID: 224
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_4;

		// Token: 0x040000E1 RID: 225
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_5;

		// Token: 0x040000E2 RID: 226
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_6;

		// Token: 0x040000E3 RID: 227
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_0;

		// Token: 0x040000E4 RID: 228
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_1;

		// Token: 0x040000E5 RID: 229
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x040000E6 RID: 230
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_2;

		// Token: 0x040000E7 RID: 231
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_7;

		// Token: 0x040000E8 RID: 232
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_8;

		// Token: 0x040000E9 RID: 233
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_9;

		// Token: 0x040000EA RID: 234
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;
	}
}
