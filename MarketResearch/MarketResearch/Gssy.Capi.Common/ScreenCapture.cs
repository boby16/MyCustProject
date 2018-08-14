using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Gssy.Capi.Common
{
	// Token: 0x02000013 RID: 19
	public class ScreenCapture
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00004530 File Offset: 0x00002730
		public bool Capture(string string_0, int int_0 = 0)
		{
			try
			{
				Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - int_0);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CopyFromScreen(0, int_0, 0, 0, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
				bitmap.Save(string_0, ImageFormat.Jpeg);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000045CC File Offset: 0x000027CC
		public bool CaptureBottom(string string_0, int int_0 = 0)
		{
			try
			{
				Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, int_0);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CopyFromScreen(0, Screen.PrimaryScreen.Bounds.Height - int_0, 0, 0, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
				bitmap.Save(string_0, ImageFormat.Jpeg);
			}
			catch
			{
				return false;
			}
			return true;
		}
	}
}
