using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LoyalFilial.MarketResearch.Entities.BaiduJson
{
	[Serializable]
	public class JGeocoding
	{
		public int status { get; set; }

		public JGeocoding_Result result { get; set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JGeocoding_Result jgeocoding_Result_0;
	}
}
