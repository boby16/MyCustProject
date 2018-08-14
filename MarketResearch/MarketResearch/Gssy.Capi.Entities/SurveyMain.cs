using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class SurveyMain
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000291C File Offset: 0x00000B1C
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002924 File Offset: 0x00000B24
		public int ID { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000292D File Offset: 0x00000B2D
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00002935 File Offset: 0x00000B35
		public string SURVEY_ID { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000293E File Offset: 0x00000B3E
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00002946 File Offset: 0x00000B46
		public string VERSION_ID { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000294F File Offset: 0x00000B4F
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00002957 File Offset: 0x00000B57
		public string USER_ID { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00002960 File Offset: 0x00000B60
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00002968 File Offset: 0x00000B68
		public string CITY_ID { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002971 File Offset: 0x00000B71
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00002979 File Offset: 0x00000B79
		public DateTime? START_TIME { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002982 File Offset: 0x00000B82
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000298A File Offset: 0x00000B8A
		public DateTime? END_TIME { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002993 File Offset: 0x00000B93
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000299B File Offset: 0x00000B9B
		public DateTime? LAST_START_TIME { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000029A4 File Offset: 0x00000BA4
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000029AC File Offset: 0x00000BAC
		public DateTime? LAST_END_TIME { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000029B5 File Offset: 0x00000BB5
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000029BD File Offset: 0x00000BBD
		public string CUR_PAGE_ID { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000029C6 File Offset: 0x00000BC6
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000029CE File Offset: 0x00000BCE
		public int CIRCLE_A_CURRENT { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000029D7 File Offset: 0x00000BD7
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000029DF File Offset: 0x00000BDF
		public int CIRCLE_A_COUNT { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000029E8 File Offset: 0x00000BE8
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000029F0 File Offset: 0x00000BF0
		public int CIRCLE_B_CURRENT { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000029F9 File Offset: 0x00000BF9
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00002A01 File Offset: 0x00000C01
		public int CIRCLE_B_COUNT { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00002A0A File Offset: 0x00000C0A
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00002A12 File Offset: 0x00000C12
		public int IS_FINISH { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00002A1B File Offset: 0x00000C1B
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00002A23 File Offset: 0x00000C23
		public int SEQUENCE_ID { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00002A2C File Offset: 0x00000C2C
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00002A34 File Offset: 0x00000C34
		public string SURVEY_GUID { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00002A3D File Offset: 0x00000C3D
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00002A45 File Offset: 0x00000C45
		public string CLIENT_ID { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002A4E File Offset: 0x00000C4E
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00002A56 File Offset: 0x00000C56
		public string PROJECT_ID { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00002A5F File Offset: 0x00000C5F
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00002A67 File Offset: 0x00000C67
		public int ROADMAP_VERSION_ID { get; set; }

		// Token: 0x04000085 RID: 133
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x04000086 RID: 134
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		// Token: 0x04000087 RID: 135
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000088 RID: 136
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x04000089 RID: 137
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;

		// Token: 0x0400008A RID: 138
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_0;

		// Token: 0x0400008B RID: 139
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DateTime? nullable_1;

		// Token: 0x0400008C RID: 140
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DateTime? nullable_2;

		// Token: 0x0400008D RID: 141
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_3;

		// Token: 0x0400008E RID: 142
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		// Token: 0x0400008F RID: 143
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000090 RID: 144
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000091 RID: 145
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000092 RID: 146
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_4;

		// Token: 0x04000093 RID: 147
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_5;

		// Token: 0x04000094 RID: 148
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_6;

		// Token: 0x04000095 RID: 149
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_5;

		// Token: 0x04000096 RID: 150
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_6;

		// Token: 0x04000097 RID: 151
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_7;

		// Token: 0x04000098 RID: 152
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_7;
	}
}
