using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class SurveyAnswerHis
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002102 File Offset: 0x00000302
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000210A File Offset: 0x0000030A
		public int ID { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000211B File Offset: 0x0000031B
		public string SURVEY_ID { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002124 File Offset: 0x00000324
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000212C File Offset: 0x0000032C
		public string QUESTION_NAME { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002135 File Offset: 0x00000335
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000213D File Offset: 0x0000033D
		public string CODE { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002146 File Offset: 0x00000346
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000214E File Offset: 0x0000034E
		public int MULTI_ORDER { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002157 File Offset: 0x00000357
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000215F File Offset: 0x0000035F
		public DateTime? MODIFY_DATE { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002168 File Offset: 0x00000368
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002170 File Offset: 0x00000370
		public int SEQUENCE_ID { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002179 File Offset: 0x00000379
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002181 File Offset: 0x00000381
		public string SURVEY_GUID { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000218A File Offset: 0x0000038A
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002192 File Offset: 0x00000392
		public DateTime? BEGIN_DATE { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000219B File Offset: 0x0000039B
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000021A3 File Offset: 0x000003A3
		public string PAGE_ID { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000021AC File Offset: 0x000003AC
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000021B4 File Offset: 0x000003B4
		public int OP_TYPE_ID { get; set; }

		// Token: 0x0400000B RID: 11
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x0400000E RID: 14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		// Token: 0x04000010 RID: 16
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DateTime? nullable_0;

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		// Token: 0x04000012 RID: 18
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		// Token: 0x04000013 RID: 19
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DateTime? nullable_1;

		// Token: 0x04000014 RID: 20
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		// Token: 0x04000015 RID: 21
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_3;
	}
}
