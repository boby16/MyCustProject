using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class SurveyRandom
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00002D4B File Offset: 0x00000F4B
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00002D53 File Offset: 0x00000F53
		public int ID { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00002D64 File Offset: 0x00000F64
		public string SURVEY_ID { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00002D6D File Offset: 0x00000F6D
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00002D75 File Offset: 0x00000F75
		public string QUESTION_SET { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00002D7E File Offset: 0x00000F7E
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00002D86 File Offset: 0x00000F86
		public string QUESTION_NAME { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00002D8F File Offset: 0x00000F8F
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00002D97 File Offset: 0x00000F97
		public string CODE { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00002DA0 File Offset: 0x00000FA0
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public string PARENT_CODE { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00002DB1 File Offset: 0x00000FB1
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00002DB9 File Offset: 0x00000FB9
		public int RANDOM_INDEX { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00002DC2 File Offset: 0x00000FC2
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00002DCA File Offset: 0x00000FCA
		public int RANDOM_SET1 { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00002DD3 File Offset: 0x00000FD3
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00002DDB File Offset: 0x00000FDB
		public int RANDOM_SET2 { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00002DE4 File Offset: 0x00000FE4
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00002DEC File Offset: 0x00000FEC
		public int RANDOM_SET3 { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00002DF5 File Offset: 0x00000FF5
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00002DFD File Offset: 0x00000FFD
		public int IS_FIX { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00002E06 File Offset: 0x00001006
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00002E0E File Offset: 0x0000100E
		public string SURVEY_GUID { get; set; }

		// Token: 0x040000C4 RID: 196
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000C5 RID: 197
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x040000C7 RID: 199
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x040000C8 RID: 200
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;

		// Token: 0x040000C9 RID: 201
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_4;

		// Token: 0x040000CA RID: 202
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		// Token: 0x040000CB RID: 203
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		// Token: 0x040000CC RID: 204
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_3;

		// Token: 0x040000CD RID: 205
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_4;

		// Token: 0x040000CE RID: 206
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_5;

		// Token: 0x040000CF RID: 207
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;
	}
}
