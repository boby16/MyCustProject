using System;

namespace Gssy.Capi.Common
{
	public class Logging
	{
		public static ILogger Error = new ErrorLogger();

		public static ILogger Info = new InfoLogger();

		public static ILogger Data = new DataLogger();
	}
}
