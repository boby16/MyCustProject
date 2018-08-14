using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Gssy.Capi.Common
{
	// Token: 0x0200000F RID: 15
	public class ErrorLogger : ILogger
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000041F8 File Offset: 0x000023F8
		public void WriteLog(string string_0, string string_1)
		{
			Logger.Write(new LogEntry
			{
				Categories = 
				{
					GClass0.smethod_0("NŸɻͧѵՊ٪ݣࡤ१ੳ")
				},
				TimeStamp = DateTime.Now,
				Title = string_0,
				Message = string_1
			});
		}
	}
}
