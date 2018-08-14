using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Entities
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class SurveyDefine
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000229A File Offset: 0x0000049A
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000022A2 File Offset: 0x000004A2
		public int ID { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000022AB File Offset: 0x000004AB
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000022B3 File Offset: 0x000004B3
		public int ANSWER_ORDER { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000022C4 File Offset: 0x000004C4
		public string PAGE_ID { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000022CD File Offset: 0x000004CD
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000022D5 File Offset: 0x000004D5
		public string QUESTION_NAME { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000022DE File Offset: 0x000004DE
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000022E6 File Offset: 0x000004E6
		public string QUESTION_TITLE { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000022EF File Offset: 0x000004EF
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000022F7 File Offset: 0x000004F7
		public int QUESTION_TYPE { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002300 File Offset: 0x00000500
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002308 File Offset: 0x00000508
		public int QUESTION_USE { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002311 File Offset: 0x00000511
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002319 File Offset: 0x00000519
		public int ANSWER_USE { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002322 File Offset: 0x00000522
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000232A File Offset: 0x0000052A
		public int COMBINE_INDEX { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002333 File Offset: 0x00000533
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000233B File Offset: 0x0000053B
		public string DETAIL_ID { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002344 File Offset: 0x00000544
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000234C File Offset: 0x0000054C
		public string PARENT_CODE { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002355 File Offset: 0x00000555
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000235D File Offset: 0x0000055D
		public string SHOW_LOGIC { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002366 File Offset: 0x00000566
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000236E File Offset: 0x0000056E
		public string QUESTION_CONTENT { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002377 File Offset: 0x00000577
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000237F File Offset: 0x0000057F
		public string SPSS_TITLE { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002390 File Offset: 0x00000590
		public int SPSS_CASE { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000023A1 File Offset: 0x000005A1
		public int SPSS_VARIABLE { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000023AA File Offset: 0x000005AA
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000023B2 File Offset: 0x000005B2
		public int SPSS_PRINT_DECIMAIL { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000023BB File Offset: 0x000005BB
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000023C3 File Offset: 0x000005C3
		public int MIN_COUNT { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000023D4 File Offset: 0x000005D4
		public int MAX_COUNT { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000023DD File Offset: 0x000005DD
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000023E5 File Offset: 0x000005E5
		public int IS_RANDOM { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000023EE File Offset: 0x000005EE
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000023F6 File Offset: 0x000005F6
		public int PAGE_COUNT_DOWN { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000023FF File Offset: 0x000005FF
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002407 File Offset: 0x00000607
		public int CONTROL_TYPE { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002418 File Offset: 0x00000618
		public int CONTROL_FONTSIZE { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002421 File Offset: 0x00000621
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002429 File Offset: 0x00000629
		public int CONTROL_HEIGHT { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002432 File Offset: 0x00000632
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000243A File Offset: 0x0000063A
		public int CONTROL_WIDTH { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002443 File Offset: 0x00000643
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000244B File Offset: 0x0000064B
		public string CONTROL_MASK { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002454 File Offset: 0x00000654
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000245C File Offset: 0x0000065C
		public int TITLE_FONTSIZE { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002465 File Offset: 0x00000665
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000246D File Offset: 0x0000066D
		public string CONTROL_TOOLTIP { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002476 File Offset: 0x00000676
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000247E File Offset: 0x0000067E
		public string NOTE { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002487 File Offset: 0x00000687
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000248F File Offset: 0x0000068F
		public string LIMIT_LOGIC { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002498 File Offset: 0x00000698
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000024A0 File Offset: 0x000006A0
		public string FIX_LOGIC { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000024A9 File Offset: 0x000006A9
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000024B1 File Offset: 0x000006B1
		public string PRESET_LOGIC { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000024BA File Offset: 0x000006BA
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000024C2 File Offset: 0x000006C2
		public string GROUP_LEVEL { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000024CB File Offset: 0x000006CB
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000024D3 File Offset: 0x000006D3
		public string GROUP_CODEA { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000024DC File Offset: 0x000006DC
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000024E4 File Offset: 0x000006E4
		public string GROUP_CODEB { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000024ED File Offset: 0x000006ED
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000024F5 File Offset: 0x000006F5
		public int GROUP_PAGE_TYPE { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000024FE File Offset: 0x000006FE
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002506 File Offset: 0x00000706
		public string MT_GROUP_MSG { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000250F File Offset: 0x0000070F
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002517 File Offset: 0x00000717
		public string MT_GROUP_COUNT { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002520 File Offset: 0x00000720
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002528 File Offset: 0x00000728
		public int IS_ATTACH { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002531 File Offset: 0x00000731
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00002539 File Offset: 0x00000739
		public string MP3_FILE { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002542 File Offset: 0x00000742
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000254A File Offset: 0x0000074A
		public int MP3_START_TYPE { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002553 File Offset: 0x00000753
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000255B File Offset: 0x0000075B
		public int SUMMARY_USE { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002564 File Offset: 0x00000764
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000256C File Offset: 0x0000076C
		public string SUMMARY_TITLE { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002575 File Offset: 0x00000775
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000257D File Offset: 0x0000077D
		public int SUMMARY_INDEX { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002586 File Offset: 0x00000786
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000258E File Offset: 0x0000078E
		public string FILLDATA { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002597 File Offset: 0x00000797
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000259F File Offset: 0x0000079F
		public string EXTEND_1 { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000025A8 File Offset: 0x000007A8
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000025B0 File Offset: 0x000007B0
		public string EXTEND_2 { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000025B9 File Offset: 0x000007B9
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000025C1 File Offset: 0x000007C1
		public string EXTEND_3 { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000025CA File Offset: 0x000007CA
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000025D2 File Offset: 0x000007D2
		public string EXTEND_4 { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000025DB File Offset: 0x000007DB
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000025E3 File Offset: 0x000007E3
		public string EXTEND_5 { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000025EC File Offset: 0x000007EC
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000025F4 File Offset: 0x000007F4
		public string EXTEND_6 { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000025FD File Offset: 0x000007FD
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00002605 File Offset: 0x00000805
		public string EXTEND_7 { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000260E File Offset: 0x0000080E
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002616 File Offset: 0x00000816
		public string EXTEND_8 { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000261F File Offset: 0x0000081F
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002627 File Offset: 0x00000827
		public string EXTEND_9 { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002630 File Offset: 0x00000830
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00002638 File Offset: 0x00000838
		public string EXTEND_10 { get; set; }

		// Token: 0x04000023 RID: 35
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		// Token: 0x04000024 RID: 36
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		// Token: 0x04000025 RID: 37
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000026 RID: 38
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000027 RID: 39
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		// Token: 0x04000028 RID: 40
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		// Token: 0x04000029 RID: 41
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		// Token: 0x0400002A RID: 42
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_4;

		// Token: 0x0400002B RID: 43
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_5;

		// Token: 0x0400002C RID: 44
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		// Token: 0x0400002E RID: 46
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_5;

		// Token: 0x0400002F RID: 47
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_6;

		// Token: 0x04000030 RID: 48
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_7;

		// Token: 0x04000031 RID: 49
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_6;

		// Token: 0x04000032 RID: 50
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_7;

		// Token: 0x04000033 RID: 51
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_8;

		// Token: 0x04000034 RID: 52
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_9;

		// Token: 0x04000035 RID: 53
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_10;

		// Token: 0x04000036 RID: 54
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_11;

		// Token: 0x04000037 RID: 55
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_12;

		// Token: 0x04000038 RID: 56
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_13;

		// Token: 0x04000039 RID: 57
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_14;

		// Token: 0x0400003A RID: 58
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_15;

		// Token: 0x0400003B RID: 59
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_16;

		// Token: 0x0400003C RID: 60
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_8;

		// Token: 0x0400003D RID: 61
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_17;

		// Token: 0x0400003E RID: 62
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_9;

		// Token: 0x0400003F RID: 63
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_10;

		// Token: 0x04000040 RID: 64
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_11;

		// Token: 0x04000041 RID: 65
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_12;

		// Token: 0x04000042 RID: 66
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_13;

		// Token: 0x04000043 RID: 67
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_14;

		// Token: 0x04000044 RID: 68
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_15;

		// Token: 0x04000045 RID: 69
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_16;

		// Token: 0x04000046 RID: 70
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_18;

		// Token: 0x04000047 RID: 71
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_17;

		// Token: 0x04000048 RID: 72
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_18;

		// Token: 0x04000049 RID: 73
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_19;

		// Token: 0x0400004A RID: 74
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_19;

		// Token: 0x0400004B RID: 75
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_20;

		// Token: 0x0400004C RID: 76
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_21;

		// Token: 0x0400004D RID: 77
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_20;

		// Token: 0x0400004E RID: 78
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_22;

		// Token: 0x0400004F RID: 79
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_21;

		// Token: 0x04000050 RID: 80
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_22;

		// Token: 0x04000051 RID: 81
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_23;

		// Token: 0x04000052 RID: 82
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_24;

		// Token: 0x04000053 RID: 83
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_25;

		// Token: 0x04000054 RID: 84
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_26;

		// Token: 0x04000055 RID: 85
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_27;

		// Token: 0x04000056 RID: 86
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_28;

		// Token: 0x04000057 RID: 87
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_29;

		// Token: 0x04000058 RID: 88
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_30;

		// Token: 0x04000059 RID: 89
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_31;
	}
}
