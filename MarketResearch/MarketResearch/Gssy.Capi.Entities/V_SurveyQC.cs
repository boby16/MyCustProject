using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class V_SurveyQC
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000037CD File Offset: 0x000019CD
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000037D5 File Offset: 0x000019D5
		public string SURVEY_ID { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000037DE File Offset: 0x000019DE
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000037E6 File Offset: 0x000019E6
		public int ANSWER_ORDER { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000037EF File Offset: 0x000019EF
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x000037F7 File Offset: 0x000019F7
		public string QUESTION_TITLE { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00003800 File Offset: 0x00001A00
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00003808 File Offset: 0x00001A08
		public int QUESTION_TYPE { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00003811 File Offset: 0x00001A11
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00003819 File Offset: 0x00001A19
		public string SPSS_TITLE { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00003822 File Offset: 0x00001A22
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000382A File Offset: 0x00001A2A
		public string CODE { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00003833 File Offset: 0x00001A33
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000383B File Offset: 0x00001A3B
		public string CODE_TEXT { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00003844 File Offset: 0x00001A44
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000384C File Offset: 0x00001A4C
		public string PAGE_ID { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00003855 File Offset: 0x00001A55
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000385D File Offset: 0x00001A5D
		public string QUESTION_NAME { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00003866 File Offset: 0x00001A66
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000386E File Offset: 0x00001A6E
		public string DETAIL_ID { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00003877 File Offset: 0x00001A77
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000387F File Offset: 0x00001A7F
		public string PARENT_CODE { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00003888 File Offset: 0x00001A88
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00003890 File Offset: 0x00001A90
		public int QUESTION_USE { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00003899 File Offset: 0x00001A99
		// (set) Token: 0x060002FB RID: 763 RVA: 0x000038A1 File Offset: 0x00001AA1
		public int ANSWER_USE { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000038AA File Offset: 0x00001AAA
		// (set) Token: 0x060002FD RID: 765 RVA: 0x000038B2 File Offset: 0x00001AB2
		public int COMBINE_INDEX { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000038BB File Offset: 0x00001ABB
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000038C3 File Offset: 0x00001AC3
		public int SPSS_CASE { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000038CC File Offset: 0x00001ACC
		// (set) Token: 0x06000301 RID: 769 RVA: 0x000038D4 File Offset: 0x00001AD4
		public int SPSS_VARIABLE { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000038DD File Offset: 0x00001ADD
		// (set) Token: 0x06000303 RID: 771 RVA: 0x000038E5 File Offset: 0x00001AE5
		public int SPSS_PRINT_DECIMAIL { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000304 RID: 772 RVA: 0x000038EE File Offset: 0x00001AEE
		// (set) Token: 0x06000305 RID: 773 RVA: 0x000038F6 File Offset: 0x00001AF6
		public int SEQUENCE_ID { get; set; }

		// Token: 0x04000162 RID: 354
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000163 RID: 355
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000164 RID: 356
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000165 RID: 357
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000166 RID: 358
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x04000167 RID: 359
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		// Token: 0x04000168 RID: 360
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_4;

		// Token: 0x04000169 RID: 361
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;

		// Token: 0x0400016A RID: 362
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_6;

		// Token: 0x0400016B RID: 363
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_7;

		// Token: 0x0400016C RID: 364
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_8;

		// Token: 0x0400016D RID: 365
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		// Token: 0x0400016E RID: 366
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_3;

		// Token: 0x0400016F RID: 367
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_4;

		// Token: 0x04000170 RID: 368
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_5;

		// Token: 0x04000171 RID: 369
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_6;

		// Token: 0x04000172 RID: 370
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_7;

		// Token: 0x04000173 RID: 371
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_8;
	}
}
