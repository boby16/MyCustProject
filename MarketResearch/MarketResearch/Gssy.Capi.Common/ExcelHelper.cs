using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace Gssy.Capi.Common
{
	public class ExcelHelper
	{
		public bool CheckExcelInstall()
		{
			RegistryKey localMachine = Registry.LocalMachine;
			RegistryKey registryKey = localMachine.OpenSubKey("SOFTWARE\\Microsoft\\Office\\11.0\\Excel\\InstallRoot\\");
			RegistryKey registryKey2 = localMachine.OpenSubKey("SOFTWARE\\Microsoft\\Office\\12.0\\Excel\\InstallRoot\\");
			RegistryKey registryKey3 = localMachine.OpenSubKey("SOFTWARE\\Microsoft\\Office\\14.0\\Excel\\InstallRoot\\");
			RegistryKey registryKey4 = localMachine.OpenSubKey("SOFTWARE\\Microsoft\\Office\\15.0\\Excel\\InstallRoot\\");
			if (registryKey3 != null)
			{
				string str = registryKey3.GetValue("Path").ToString();
				if (File.Exists(str + "Excel.exe"))
				{
					return true;
				}
			}
			if (registryKey2 != null)
			{
				string str2 = registryKey2.GetValue("Path").ToString();
				if (File.Exists(str2 + "Excel.exe"))
				{
					return true;
				}
			}
			if (registryKey4 != null)
			{
				string str3 = registryKey4.GetValue("Path").ToString();
				if (File.Exists(str3 + "Excel.exe"))
				{
					return true;
				}
			}
			if (registryKey != null)
			{
				string str4 = registryKey.GetValue("Path").ToString();
				if (File.Exists(str4 + "Excel.exe"))
				{
					return true;
				}
			}
			return false;
		}

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		private static extern int GetWindowThreadProcessId(IntPtr intptr_0, out int int_0);

		public void KillExcelProcess(Application application_0)
		{
			IntPtr intptr_ = new IntPtr(application_0.Hwnd);
			int processId = 0;
			ExcelHelper.GetWindowThreadProcessId(intptr_, out processId);
			Process processById = Process.GetProcessById(processId);
			processById.Kill();
		}
	}
}
