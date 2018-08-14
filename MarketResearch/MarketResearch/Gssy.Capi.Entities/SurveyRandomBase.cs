using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class SurveyRandomBase
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00002C7F File Offset: 0x00000E7F
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00002C87 File Offset: 0x00000E87
		public int ID { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00002C90 File Offset: 0x00000E90
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00002C98 File Offset: 0x00000E98
		public string SURVEY_ID { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00002CA1 File Offset: 0x00000EA1
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00002CA9 File Offset: 0x00000EA9
		public string QUESTION_SET { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00002CB2 File Offset: 0x00000EB2
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00002CBA File Offset: 0x00000EBA
		public string QUESTION_NAME { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00002CC3 File Offset: 0x00000EC3
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00002CCB File Offset: 0x00000ECB
		public string CODE { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00002CD4 File Offset: 0x00000ED4
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00002CDC File Offset: 0x00000EDC
		public string PARENT_CODE { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00002CE5 File Offset: 0x00000EE5
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00002CED File Offset: 0x00000EED
		public int RANDOM_INDEX { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00002CF6 File Offset: 0x00000EF6
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00002CFE File Offset: 0x00000EFE
		public int RANDOM_SET1 { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00002D07 File Offset: 0x00000F07
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00002D0F File Offset: 0x00000F0F
		public int RANDOM_SET2 { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00002D18 File Offset: 0x00000F18
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00002D20 File Offset: 0x00000F20
		public int RANDOM_SET3 { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00002D29 File Offset: 0x00000F29
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00002D31 File Offset: 0x00000F31
		public int IS_FIX { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00002D3A File Offset: 0x00000F3A
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00002D42 File Offset: 0x00000F42
		public string SURVEY_GUID { get; set; }

		// Token: 0x040000B8 RID: 184
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x040000B9 RID: 185
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040000BA RID: 186
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x040000BB RID: 187
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		// Token: 0x040000BC RID: 188
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;

		// Token: 0x040000BD RID: 189
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		// Token: 0x040000BE RID: 190
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		// Token: 0x040000BF RID: 191
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		// Token: 0x040000C0 RID: 192
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		// Token: 0x040000C1 RID: 193
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_4;

		// Token: 0x040000C2 RID: 194
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_5;

		// Token: 0x040000C3 RID: 195
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_5;
	}
}
