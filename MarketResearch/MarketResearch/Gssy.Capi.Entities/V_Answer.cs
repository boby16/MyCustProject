using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class V_Answer
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00003602 File Offset: 0x00001802
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000360A File Offset: 0x0000180A
		public int ANSWER_ORDER { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00003613 File Offset: 0x00001813
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000361B File Offset: 0x0000181B
		public string QUESTION_NAME { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00003624 File Offset: 0x00001824
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000362C File Offset: 0x0000182C
		public string CODE { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00003635 File Offset: 0x00001835
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000363D File Offset: 0x0000183D
		public string SURVEY_ID { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00003646 File Offset: 0x00001846
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000364E File Offset: 0x0000184E
		public int SPSS_VARIABLE { get; set; }

		// Token: 0x04000147 RID: 327
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000148 RID: 328
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000149 RID: 329
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x0400014A RID: 330
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x0400014B RID: 331
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;
	}
}
