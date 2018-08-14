using System;
using System.Globalization;
using System.Windows.Data;

namespace Gssy.Capi.Class
{
	// Token: 0x02000066 RID: 102
	public class ProgressConverter : IValueConverter
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x0009E1B0 File Offset: 0x0009C3B0
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((TimeSpan)value).TotalSeconds;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00003FB2 File Offset: 0x000021B2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return TimeSpan.FromSeconds((double)value);
		}
	}
}
