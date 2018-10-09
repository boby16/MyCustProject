using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace LoyalFilial.MarketResearch.Common
{
	public class DataLogger : ILogger
	{
		public void WriteLog(string string_0, string string_1)
		{
			Logger.Write(new LogEntry
			{
				Categories = 
				{
					"DataLogger"
				},
				TimeStamp = DateTime.Now,
				Title = string_0,
				Message = string_1
			});
		}
	}
}
