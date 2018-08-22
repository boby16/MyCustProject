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
			RegistryKey registryKey = localMachine.OpenSubKey(GClass0.smethod_0("bſɩͺѺխٹݯࡵ॥੎୅౗ോ๐ཌྷ၇ᅔቃፑᑻᕺᙲ᝹᡼᥄ᨦᬧ᰻ᴤṏὗ⁩ⅳ≪⍢⑑╅♥❹⡽⥩⩫⭪ⱗ⵫⹬⽶そ"));
			RegistryKey registryKey2 = localMachine.OpenSubKey(GClass0.smethod_0("bſɩͺѺխٹݯࡵ॥੎୅౗ോ๐ཌྷ၇ᅔቃፑᑻᕺᙲ᝹᡼᥄ᨦᬤ᰻ᴤṏὗ⁩ⅳ≪⍢⑑╅♥❹⡽⥩⩫⭪ⱗ⵫⹬⽶そ"));
			RegistryKey registryKey3 = localMachine.OpenSubKey(GClass0.smethod_0("bſɩͺѺխٹݯࡵ॥੎୅౗ോ๐ཌྷ၇ᅔቃፑᑻᕺᙲ᝹᡼᥄ᨦᬢ᰻ᴤṏὗ⁩ⅳ≪⍢⑑╅♥❹⡽⥩⩫⭪ⱗ⵫⹬⽶そ"));
			RegistryKey registryKey4 = localMachine.OpenSubKey(GClass0.smethod_0("bſɩͺѺխٹݯࡵ॥੎୅౗ോ๐ཌྷ၇ᅔቃፑᑻᕺᙲ᝹᡼᥄ᨦᬣ᰻ᴤṏὗ⁩ⅳ≪⍢⑑╅♥❹⡽⥩⩫⭪ⱗ⵫⹬⽶そ"));
			if (registryKey3 != null)
			{
				string str = registryKey3.GetValue(GClass0.smethod_0("TŢɶͩ")).ToString();
				if (File.Exists(str + GClass0.smethod_0("LŰɤͣѩԪ٦ݺࡤ")))
				{
					return true;
				}
			}
			if (registryKey2 != null)
			{
				string str2 = registryKey2.GetValue(GClass0.smethod_0("TŢɶͩ")).ToString();
				if (File.Exists(str2 + GClass0.smethod_0("LŰɤͣѩԪ٦ݺࡤ")))
				{
					return true;
				}
			}
			if (registryKey4 != null)
			{
				string str3 = registryKey4.GetValue(GClass0.smethod_0("TŢɶͩ")).ToString();
				if (File.Exists(str3 + GClass0.smethod_0("LŰɤͣѩԪ٦ݺࡤ")))
				{
					return true;
				}
			}
			if (registryKey != null)
			{
				string str4 = registryKey.GetValue(GClass0.smethod_0("TŢɶͩ")).ToString();
				if (File.Exists(str4 + GClass0.smethod_0("LŰɤͣѩԪ٦ݺࡤ")))
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
