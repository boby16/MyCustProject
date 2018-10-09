using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LoyalFilial.MarketResearch.Entities
{
	[Serializable]
	public class SurveyLog
	{
		public int ID { get; set; }

		public string LOG_TYPE { get; set; }

		public string LOG_MESSAGE { get; set; }

		public DateTime? LOG_DATE { get; set; }

		public string SURVEY_ID { get; set; }

		public string VERSION_ID { get; set; }

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DateTime? nullable_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;
	}
}
