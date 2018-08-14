using System;

// Token: 0x02000022 RID: 34
public class GClass0
{
	// Token: 0x06000100 RID: 256 RVA: 0x00008498 File Offset: 0x00006698
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
