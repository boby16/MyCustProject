using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class SurveyLog
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000281D File Offset: 0x00000A1D
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00002825 File Offset: 0x00000A25
		public int ID { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000282E File Offset: 0x00000A2E
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00002836 File Offset: 0x00000A36
		public string LOG_TYPE { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000283F File Offset: 0x00000A3F
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00002847 File Offset: 0x00000A47
		public string LOG_MESSAGE { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002850 File Offset: 0x00000A50
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00002858 File Offset: 0x00000A58
		public DateTime? LOG_DATE { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00002861 File Offset: 0x00000A61
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00002869 File Offset: 0x00000A69
		public string SURVEY_ID { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00002872 File Offset: 0x00000A72
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000287A File Offset: 0x00000A7A
		public string VERSION_ID { get; set; }

		// Token: 0x04000076 RID: 118
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x04000077 RID: 119
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000078 RID: 120
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		// Token: 0x04000079 RID: 121
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DateTime? nullable_0;

		// Token: 0x0400007A RID: 122
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x0400007B RID: 123
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;
	}
}
