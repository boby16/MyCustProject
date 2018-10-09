using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LoyalFilial.MarketResearch.Entities
{
	public class SurveyQuotaAnswer
	{
		public int ID { get; set; }

		public string SURVEY_ID { get; set; }

		public string SURVEY_GUID { get; set; }

		public string QUESTION_NAME { get; set; }

		public string CODE { get; set; }

		public int IS_FINISH { get; set; }

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_2;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string string_3;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_1;
	}
}
