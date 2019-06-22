using System;
using System.Diagnostics;
using System.IO;

namespace LoyalFilial.MarketResearch.Update
{
	public class RarFile
	{
		public bool Compress(string string_0, string string_1, string string_2, string string_3, bool bool_0 = true, bool bool_1 = true)
		{
			bool result = false;
			string text = Environment.CurrentDirectory + "\\Lib\\WinRar.exe";
			if (Directory.Exists(string_1) && File.Exists(text))
			{
				if (File.Exists(string_1 + string_2))
				{
					File.Delete(string_1 + string_2);
				}
				string text2 = bool_1 ? " -r " : "";
				string text3 = bool_0 ? " -ep1 " : "";
				string string_4 = string.Concat(new string[]
				{
					" a -hp",
					string_3,
					text2,
					text3,
					" -y -o+ ",
					string_2,
					" ",
					string_0,
					" >nul"
				});
				this.StartProcess(text, string_1, string_4, ProcessWindowStyle.Minimized, true, 0, false);
				result = File.Exists(string_1 + string_2);
			}
			return result;
		}

		public bool Extract(string string_0, string string_1, string string_2, string string_3)
		{
			bool result = false;
			string text = Environment.CurrentDirectory + "\\Lib\\WinRar.exe";
			if (File.Exists(string_0) && File.Exists(text))
			{
				string string_4 = string.Concat(new string[]
				{
					" x -hp",
					string_3,
					" -y -o+ ",
					string_0,
					" ",
					string_2,
					" "
				});
				result = this.StartProcess(text, string_1, string_4, ProcessWindowStyle.Minimized, true, 0, false);
			}
			return result;
		}

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
