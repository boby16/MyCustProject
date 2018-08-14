using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class VEAnswer
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000035BE File Offset: 0x000017BE
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000035C6 File Offset: 0x000017C6
		public string QUESTION_NAME { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000035CF File Offset: 0x000017CF
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x000035D7 File Offset: 0x000017D7
		public string CODE { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000035E0 File Offset: 0x000017E0
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x000035E8 File Offset: 0x000017E8
		public string CODE_TEXT { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000035F1 File Offset: 0x000017F1
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x000035F9 File Offset: 0x000017F9
		public int SEQUENCE_ID { get; set; }

		// Token: 0x04000143 RID: 323
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000144 RID: 324
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000145 RID: 325
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		// Token: 0x04000146 RID: 326
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;
	}
}
