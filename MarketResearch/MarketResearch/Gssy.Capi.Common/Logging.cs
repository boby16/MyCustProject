using System;

namespace Gssy.Capi.Common
{
	// Token: 0x0200000E RID: 14
	public class Logging
	{
		// Token: 0x04000043 RID: 67
		public static ILogger Error = new ErrorLogger();

		// Token: 0x04000044 RID: 68
		public static ILogger Info = new InfoLogger();

		// Token: 0x04000045 RID: 69
		public static ILogger Data = new DataLogger();
	}
}
