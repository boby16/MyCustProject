using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class SurveyAnswer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public int ID { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002071 File Offset: 0x00000271
		public string SURVEY_ID { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002082 File Offset: 0x00000282
		public string QUESTION_NAME { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002093 File Offset: 0x00000293
		public string CODE { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020A4 File Offset: 0x000002A4
		public int MULTI_ORDER { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020AD File Offset: 0x000002AD
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020B5 File Offset: 0x000002B5
		public DateTime? MODIFY_DATE { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020C6 File Offset: 0x000002C6
		public int SEQUENCE_ID { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020CF File Offset: 0x000002CF
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020D7 File Offset: 0x000002D7
		public string SURVEY_GUID { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000020E8 File Offset: 0x000002E8
		public DateTime? BEGIN_DATE { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000020F1 File Offset: 0x000002F1
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000020F9 File Offset: 0x000002F9
		public string PAGE_ID { get; set; }

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x04000002 RID: 2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000003 RID: 3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000004 RID: 4
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_0;

		// Token: 0x04000007 RID: 7
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		// Token: 0x04000009 RID: 9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DateTime? nullable_1;

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;
	}
}
