using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x0200001E RID: 30
	public class jquotaanswer
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00003501 File Offset: 0x00001701
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00003509 File Offset: 0x00001709
		public string surveyid { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00003512 File Offset: 0x00001712
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000351A File Offset: 0x0000171A
		public string surveyguid { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00003523 File Offset: 0x00001723
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000352B File Offset: 0x0000172B
		public string projectid { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00003534 File Offset: 0x00001734
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000353C File Offset: 0x0000173C
		public string pageid { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00003545 File Offset: 0x00001745
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000354D File Offset: 0x0000174D
		public string isfinish { get; set; }

		// Token: 0x04000138 RID: 312
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x04000139 RID: 313
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x0400013A RID: 314
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		// Token: 0x0400013B RID: 315
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;

		// Token: 0x0400013C RID: 316
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		// Token: 0x0400013D RID: 317
		public List<janswer> qanswers = new List<janswer>();
	}
}
