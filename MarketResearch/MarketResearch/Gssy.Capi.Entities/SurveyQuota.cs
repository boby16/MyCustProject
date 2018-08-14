using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x0200000D RID: 13
	public class SurveyQuota
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002AD6 File Offset: 0x00000CD6
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00002ADE File Offset: 0x00000CDE
		public int ID { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002AE7 File Offset: 0x00000CE7
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00002AEF File Offset: 0x00000CEF
		public string PROJECT_ID { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002AF8 File Offset: 0x00000CF8
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00002B00 File Offset: 0x00000D00
		public string CLIENT_ID { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00002B09 File Offset: 0x00000D09
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00002B11 File Offset: 0x00000D11
		public string PAGE_ID { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002B1A File Offset: 0x00000D1A
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00002B22 File Offset: 0x00000D22
		public string QUESTION_NAME { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002B2B File Offset: 0x00000D2B
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00002B33 File Offset: 0x00000D33
		public string QUESTION_TITLE { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00002B3C File Offset: 0x00000D3C
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00002B44 File Offset: 0x00000D44
		public int INNER_INDEX { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00002B4D File Offset: 0x00000D4D
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00002B55 File Offset: 0x00000D55
		public string CODE { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00002B5E File Offset: 0x00000D5E
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00002B66 File Offset: 0x00000D66
		public string CODE_TEXT { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00002B6F File Offset: 0x00000D6F
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00002B77 File Offset: 0x00000D77
		public int SAMPLE_OVER { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002B80 File Offset: 0x00000D80
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00002B88 File Offset: 0x00000D88
		public int SAMPLE_TARGET { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002B91 File Offset: 0x00000D91
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00002B99 File Offset: 0x00000D99
		public int SAMPLE_BACKUP { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00002BA2 File Offset: 0x00000DA2
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00002BAA File Offset: 0x00000DAA
		public int SAMPLE_TOTAL { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002BB3 File Offset: 0x00000DB3
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00002BBB File Offset: 0x00000DBB
		public int SAMPLE_FINISH { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00002BC4 File Offset: 0x00000DC4
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00002BCC File Offset: 0x00000DCC
		public int SAMPLE_RUNNING { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00002BD5 File Offset: 0x00000DD5
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00002BDD File Offset: 0x00000DDD
		public int SAMPLE_REAL { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00002BE6 File Offset: 0x00000DE6
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00002BEE File Offset: 0x00000DEE
		public string IS_FULL { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00002BF7 File Offset: 0x00000DF7
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00002BFF File Offset: 0x00000DFF
		public int SAMPLE_BALANCE { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00002C08 File Offset: 0x00000E08
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00002C10 File Offset: 0x00000E10
		public DateTime? MODIFY_DATE { get; set; }

		// Token: 0x0400009F RID: 159
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x040000A0 RID: 160
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x040000A1 RID: 161
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x040000A2 RID: 162
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		// Token: 0x040000A3 RID: 163
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;

		// Token: 0x040000A4 RID: 164
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_4;

		// Token: 0x040000A5 RID: 165
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040000A6 RID: 166
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;

		// Token: 0x040000A7 RID: 167
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_6;

		// Token: 0x040000A8 RID: 168
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		// Token: 0x040000A9 RID: 169
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		// Token: 0x040000AA RID: 170
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_4;

		// Token: 0x040000AB RID: 171
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_5;

		// Token: 0x040000AC RID: 172
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_6;

		// Token: 0x040000AD RID: 173
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_7;

		// Token: 0x040000AE RID: 174
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_8;

		// Token: 0x040000AF RID: 175
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_7;

		// Token: 0x040000B0 RID: 176
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_9;

		// Token: 0x040000B1 RID: 177
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_0;
	}
}
