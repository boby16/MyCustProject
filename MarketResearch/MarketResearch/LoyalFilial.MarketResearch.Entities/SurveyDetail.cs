using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LoyalFilial.MarketResearch.Entities
{
	[Serializable]
	public class SurveyDetail
	{
		public int ID { get; set; }

		public string DETAIL_ID { get; set; }
        
		public string CODE { get; set; }
        
		public string CODE_TEXT { get; set; }

		public int IS_OTHER { get; set; }

		public int INNER_ORDER { get; set; }

		public string PARENT_CODE { get; set; }

		public int RANDOM_BASE { get; set; }

		public int RANDOM_SET { get; set; }

		public int RANDOM_FIX { get; set; }

		public string EXTEND_1 { get; set; }

		public string EXTEND_2 { get; set; }

		public string EXTEND_3 { get; set; }

		public string EXTEND_4 { get; set; }

		public string EXTEND_5 { get; set; }

		public string EXTEND_6 { get; set; }

		public string EXTEND_7 { get; set; }

		public string EXTEND_8 { get; set; }

		public string EXTEND_9 { get; set; }

		public string EXTEND_10 { get; set; }
	}
}
