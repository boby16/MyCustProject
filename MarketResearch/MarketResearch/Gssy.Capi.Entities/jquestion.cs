using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x0200001B RID: 27
	public class jquestion
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00003411 File Offset: 0x00001611
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00003419 File Offset: 0x00001619
		public string questionname { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00003422 File Offset: 0x00001622
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000342A File Offset: 0x0000162A
		public string questiontitle { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00003433 File Offset: 0x00001633
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000343B File Offset: 0x0000163B
		public string questioncontent { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00003444 File Offset: 0x00001644
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000344C File Offset: 0x0000164C
		public string othercode { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00003455 File Offset: 0x00001655
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000345D File Offset: 0x0000165D
		public string exclusivecode { get; set; }

		// Token: 0x0400012A RID: 298
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x0400012B RID: 299
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x0400012C RID: 300
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x0400012D RID: 301
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;

		// Token: 0x0400012E RID: 302
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		// Token: 0x0400012F RID: 303
		public List<jdetail> qdetails = new List<jdetail>();
	}
}
