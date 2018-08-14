using System;

// Token: 0x0200000D RID: 13
public class GClass0
{
	// Token: 0x0600004C RID: 76 RVA: 0x00003A8C File Offset: 0x00001C8C
	public static string smethod_0(string string_0)
	{
		int length = string_0.Length;
		char[] array = new char[length];
		for (int i = 0; i < array.Length; i++)
		{
			char c = string_0[i];
			byte b = (byte)((int)c ^ length - i);
			byte b2 = (byte)((int)(c >> 8) ^ i);
			array[i] = (char)((int)b2 << 8 | (int)b);
		}
		return string.Intern(new string(array));
	}
}
