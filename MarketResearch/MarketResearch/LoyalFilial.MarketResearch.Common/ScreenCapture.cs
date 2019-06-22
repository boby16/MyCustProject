using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace LoyalFilial.MarketResearch.Common
{
	public class ScreenCapture
	{
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
