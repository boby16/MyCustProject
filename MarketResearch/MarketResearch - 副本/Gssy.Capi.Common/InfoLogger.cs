﻿using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Gssy.Capi.Common
{
	public class InfoLogger : ILogger
	{
		public void WriteLog(string string_0, string string_1)
		{
			Logger.Write(new LogEntry
			{
				Categories = 
				{
					GClass0.smethod_0("Cŧɮͨъժ٣ݤࡧॳ")
				},
				TimeStamp = DateTime.Now,
				Title = string_0,
				Message = string_1
			});
		}
	}
}
