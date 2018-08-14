using System;
using System.Diagnostics;
using System.IO;

namespace Gssy.Capi.Update
{
	// Token: 0x02000007 RID: 7
	public class RarFile
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002C8C File Offset: 0x00000E8C
		public bool Compress(string string_0, string string_1, string string_2, string string_3, bool bool_0 = true, bool bool_1 = true)
		{
			bool result = false;
			string text = Environment.CurrentDirectory + GClass0.smethod_0("Słɤͮї՝٠ݦࡕ१੷ପ౦ൺ๤");
			if (Directory.Exists(string_1) && File.Exists(text))
			{
				if (File.Exists(string_1 + string_2))
				{
					File.Delete(string_1 + string_2);
				}
				string text2 = bool_1 ? GClass0.smethod_0("$Įɰ̡") : GClass0.smethod_0("");
				string text3 = bool_0 ? GClass0.smethod_0("&Ĩɡͳгԡ") : GClass0.smethod_0("");
				string string_4 = string.Concat(new string[]
				{
					GClass0.smethod_0("&ŤȤ̮Ѫձ"),
					string_3,
					text2,
					text3,
					GClass0.smethod_0("(Īɿ̥Щլةܡ"),
					string_2,
					GClass0.smethod_0("!"),
					string_0,
					GClass0.smethod_0("%ĺɭͷѭ")
				});
				this.StartProcess(text, string_1, string_4, ProcessWindowStyle.Minimized, true, 0, false);
				result = File.Exists(string_1 + string_2);
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002D8C File Offset: 0x00000F8C
		public bool Extract(string string_0, string string_1, string string_2, string string_3)
		{
			bool result = false;
			string text = Environment.CurrentDirectory + GClass0.smethod_0("Słɤͮї՝٠ݦࡕ१੷ପ౦ൺ๤");
			if (File.Exists(string_0) && File.Exists(text))
			{
				string string_4 = string.Concat(new string[]
				{
					GClass0.smethod_0("&ŽȤ̮Ѫձ"),
					string_3,
					GClass0.smethod_0("(Īɿ̥Щլةܡ"),
					string_0,
					GClass0.smethod_0("!"),
					string_2,
					GClass0.smethod_0("!")
				});
				result = this.StartProcess(text, string_1, string_4, ProcessWindowStyle.Minimized, true, 0, false);
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E1C File Offset: 0x0000101C
		public bool StartProcess(string string_0, string string_1, string string_2, ProcessWindowStyle processWindowStyle_0, bool bool_0 = false, int int_0 = 0, bool bool_1 = false)
		{
			try
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo(string_0, string_2);
				processStartInfo.WindowStyle = processWindowStyle_0;
				processStartInfo.WorkingDirectory = string_1;
				Process process = new Process();
				process.StartInfo = processStartInfo;
				process.StartInfo.UseShellExecute = bool_1;
				process.StartInfo.CreateNoWindow = (processWindowStyle_0 == ProcessWindowStyle.Hidden);
				process.Start();
				if (bool_0)
				{
					if (!bool_1 && processWindowStyle_0 != ProcessWindowStyle.Hidden)
					{
						process.WaitForInputIdle();
					}
					if (int_0 > 0)
					{
						process.WaitForExit(int_0);
						if (!process.HasExited)
						{
							if (process.Responding)
							{
								process.CloseMainWindow();
							}
							else
							{
								process.Kill();
							}
						}
					}
					else
					{
						process.WaitForExit();
					}
				}
				return true;
			}
			catch (Exception)
			{
			}
			return false;
		}
	}
}
