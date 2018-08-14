using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Gssy.Capi.Common
{
	// Token: 0x02000011 RID: 17
	public class DataLogger : ILogger
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00004288 File Offset: 0x00002488
		public void WriteLog(string string_0, string string_1)
		{
			Logger.Write(new LogEntry
			{
				Categories = 
				{
					GClass0.smethod_0("NŨɼͦъժ٣ݤࡧॳ")
				},
				TimeStamp = DateTime.Now,
				Title = string_0,
				Message = string_1
			});
		}
	}
}
