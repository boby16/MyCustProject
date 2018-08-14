using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace Gssy.Capi.Common
{
	// Token: 0x02000006 RID: 6
	public class ExcelHelper
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000344C File Offset: 0x0000164C
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

		// Token: 0x06000026 RID: 38
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		private static extern int GetWindowThreadProcessId(IntPtr intptr_0, out int int_0);

		// Token: 0x06000027 RID: 39 RVA: 0x00003598 File Offset: 0x00001798
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
