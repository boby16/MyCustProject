using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Gssy.Capi.Common
{
	// Token: 0x02000010 RID: 16
	public class InfoLogger : ILogger
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00004240 File Offset: 0x00002440
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
