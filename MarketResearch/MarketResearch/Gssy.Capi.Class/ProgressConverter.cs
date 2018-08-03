using System;
using System.Globalization;
using System.Windows.Data;

namespace Gssy.Capi.Class
{
	public class ProgressConverter : IValueConverter
	{
		public object Convert(object _003F346_003F, Type _003F438_003F, object _003F439_003F, CultureInfo _003F440_003F)
		{
			return ((TimeSpan)_003F346_003F).TotalSeconds;
		}

		public object ConvertBack(object _003F346_003F, Type _003F438_003F, object _003F439_003F, CultureInfo _003F440_003F)
		{
			return TimeSpan.FromSeconds((double)_003F346_003F);
		}
	}
}
