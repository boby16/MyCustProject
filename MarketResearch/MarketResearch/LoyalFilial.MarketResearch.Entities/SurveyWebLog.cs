using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LoyalFilial.MarketResearch.Entities
{
	[Serializable]
	public class SurveyWebLog
	{
		public int ID { get; set; }

		public string SURVEY_ID { get; set; }

		public string SURVEY_GUID { get; set; }

		public string URI_FULL { get; set; }

		public string URI_DOMAIN { get; set; }

		public string URI_DOMAIN_TWO { get; set; }

		public DateTime? BEGIN_TIME { get; set; }

		public DateTime? END_TIME { get; set; }

		public int STAY_TIME { get; set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime? nullable_1;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;
	}
}
