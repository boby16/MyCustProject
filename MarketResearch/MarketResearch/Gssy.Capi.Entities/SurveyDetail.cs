using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class SurveyDetail
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002641 File Offset: 0x00000841
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002649 File Offset: 0x00000849
		public int ID { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002652 File Offset: 0x00000852
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000265A File Offset: 0x0000085A
		public string DETAIL_ID { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002663 File Offset: 0x00000863
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000266B File Offset: 0x0000086B
		public string CODE { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002674 File Offset: 0x00000874
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000267C File Offset: 0x0000087C
		public string CODE_TEXT { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002685 File Offset: 0x00000885
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000268D File Offset: 0x0000088D
		public int IS_OTHER { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002696 File Offset: 0x00000896
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000269E File Offset: 0x0000089E
		public int INNER_ORDER { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000026A7 File Offset: 0x000008A7
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000026AF File Offset: 0x000008AF
		public string PARENT_CODE { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000026B8 File Offset: 0x000008B8
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000026C0 File Offset: 0x000008C0
		public int RANDOM_BASE { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000026C9 File Offset: 0x000008C9
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000026D1 File Offset: 0x000008D1
		public int RANDOM_SET { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000026DA File Offset: 0x000008DA
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000026E2 File Offset: 0x000008E2
		public int RANDOM_FIX { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000026EB File Offset: 0x000008EB
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000026F3 File Offset: 0x000008F3
		public string EXTEND_1 { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000026FC File Offset: 0x000008FC
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002704 File Offset: 0x00000904
		public string EXTEND_2 { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000270D File Offset: 0x0000090D
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002715 File Offset: 0x00000915
		public string EXTEND_3 { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000271E File Offset: 0x0000091E
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00002726 File Offset: 0x00000926
		public string EXTEND_4 { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000272F File Offset: 0x0000092F
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002737 File Offset: 0x00000937
		public string EXTEND_5 { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002740 File Offset: 0x00000940
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002748 File Offset: 0x00000948
		public string EXTEND_6 { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002751 File Offset: 0x00000951
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00002759 File Offset: 0x00000959
		public string EXTEND_7 { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002762 File Offset: 0x00000962
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000276A File Offset: 0x0000096A
		public string EXTEND_8 { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002773 File Offset: 0x00000973
		// (set) Token: 0x060000DE RID: 222 RVA: 0x0000277B File Offset: 0x0000097B
		public string EXTEND_9 { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002784 File Offset: 0x00000984
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000278C File Offset: 0x0000098C
		public string EXTEND_10 { get; set; }

		// Token: 0x0400005A RID: 90
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x0400005B RID: 91
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x0400005C RID: 92
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x0400005D RID: 93
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		// Token: 0x0400005E RID: 94
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400005F RID: 95
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000060 RID: 96
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		// Token: 0x04000061 RID: 97
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		// Token: 0x04000062 RID: 98
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_4;

		// Token: 0x04000063 RID: 99
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_5;

		// Token: 0x04000064 RID: 100
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_4;

		// Token: 0x04000065 RID: 101
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_5;

		// Token: 0x04000066 RID: 102
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_6;

		// Token: 0x04000067 RID: 103
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_7;

		// Token: 0x04000068 RID: 104
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_8;

		// Token: 0x04000069 RID: 105
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_9;

		// Token: 0x0400006A RID: 106
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_10;

		// Token: 0x0400006B RID: 107
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_11;

		// Token: 0x0400006C RID: 108
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_12;

		// Token: 0x0400006D RID: 109
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_13;
	}
}
