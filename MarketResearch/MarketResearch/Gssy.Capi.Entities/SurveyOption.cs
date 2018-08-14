using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class SurveyOption
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00002A70 File Offset: 0x00000C70
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00002A78 File Offset: 0x00000C78
		public int ID { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00002A81 File Offset: 0x00000C81
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00002A89 File Offset: 0x00000C89
		public string SURVEY_ID { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00002A92 File Offset: 0x00000C92
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00002A9A File Offset: 0x00000C9A
		public string QUESTION_NAME { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00002AA3 File Offset: 0x00000CA3
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00002AAB File Offset: 0x00000CAB
		public string CODE { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00002AB4 File Offset: 0x00000CB4
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00002ABC File Offset: 0x00000CBC
		public int RANDOM_INDEX { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00002AC5 File Offset: 0x00000CC5
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00002ACD File Offset: 0x00000CCD
		public string SURVEY_GUID { get; set; }

		// Token: 0x04000099 RID: 153
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400009A RID: 154
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x0400009B RID: 155
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x0400009C RID: 156
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x0400009D RID: 157
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400009E RID: 158
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;
	}
}
