using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class S_Define
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00003147 File Offset: 0x00001347
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000314F File Offset: 0x0000134F
		public int ID { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00003158 File Offset: 0x00001358
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00003160 File Offset: 0x00001360
		public int ANSWER_ORDER { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00003169 File Offset: 0x00001369
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00003171 File Offset: 0x00001371
		public string PAGE_ID { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000317A File Offset: 0x0000137A
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00003182 File Offset: 0x00001382
		public string QUESTION_NAME { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000318B File Offset: 0x0000138B
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00003193 File Offset: 0x00001393
		public string QNAME_MAPPING { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000319C File Offset: 0x0000139C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x000031A4 File Offset: 0x000013A4
		public int QUESTION_TYPE { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000031AD File Offset: 0x000013AD
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000031B5 File Offset: 0x000013B5
		public string QUESTION_TITLE { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000031BE File Offset: 0x000013BE
		// (set) Token: 0x06000223 RID: 547 RVA: 0x000031C6 File Offset: 0x000013C6
		public string SPSS_TITLE { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000031CF File Offset: 0x000013CF
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000031D7 File Offset: 0x000013D7
		public int SPSS_CASE { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000031E0 File Offset: 0x000013E0
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000031E8 File Offset: 0x000013E8
		public int SPSS_VARIABLE { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000031F1 File Offset: 0x000013F1
		// (set) Token: 0x06000229 RID: 553 RVA: 0x000031F9 File Offset: 0x000013F9
		public int SPSS_PRINT_DECIMAIL { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00003202 File Offset: 0x00001402
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000320A File Offset: 0x0000140A
		public string DETAIL_ID { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00003213 File Offset: 0x00001413
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000321B File Offset: 0x0000141B
		public string PARENT_CODE { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00003224 File Offset: 0x00001424
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000322C File Offset: 0x0000142C
		public int QUESTION_USE { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00003235 File Offset: 0x00001435
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000323D File Offset: 0x0000143D
		public int ANSWER_USE { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00003246 File Offset: 0x00001446
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000324E File Offset: 0x0000144E
		public int COMBINE_INDEX { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00003257 File Offset: 0x00001457
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000325F File Offset: 0x0000145F
		public int SUMMARY_USE { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00003268 File Offset: 0x00001468
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00003270 File Offset: 0x00001470
		public string SUMMARY_TITLE { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00003279 File Offset: 0x00001479
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00003281 File Offset: 0x00001481
		public int SUMMARY_INDEX { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000328A File Offset: 0x0000148A
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00003292 File Offset: 0x00001492
		public string TEST_FIX_ANSWER { get; set; }

		// Token: 0x04000100 RID: 256
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000101 RID: 257
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000102 RID: 258
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x04000103 RID: 259
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x04000104 RID: 260
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		// Token: 0x04000105 RID: 261
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000106 RID: 262
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		// Token: 0x04000107 RID: 263
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_4;

		// Token: 0x04000108 RID: 264
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000109 RID: 265
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_4;

		// Token: 0x0400010A RID: 266
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_5;

		// Token: 0x0400010B RID: 267
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;

		// Token: 0x0400010C RID: 268
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_6;

		// Token: 0x0400010D RID: 269
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_6;

		// Token: 0x0400010E RID: 270
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_7;

		// Token: 0x0400010F RID: 271
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_8;

		// Token: 0x04000110 RID: 272
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_9;

		// Token: 0x04000111 RID: 273
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_7;

		// Token: 0x04000112 RID: 274
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_10;

		// Token: 0x04000113 RID: 275
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_8;
	}
}
