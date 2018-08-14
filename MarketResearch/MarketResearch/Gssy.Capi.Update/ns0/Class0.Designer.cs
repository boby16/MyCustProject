using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ns0
{
	// Token: 0x0200000B RID: 11
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	internal class Class0
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000021BB File Offset: 0x000003BB
		internal Class0()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002260 File Offset: 0x00000460
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Class0.resourceMan == null)
				{
					Class0.resourceMan = new ResourceManager(GClass0.smethod_0("bŗɐ͛Џգپݮࡴल੎୪౽൹๣ཱིျᅄቡ፽ᑡᕵᙽ᝺ᡤᥩ᩸ᬤᱛᵭṴὩ⁰ⅶ≠⍧⑲"), typeof(Class0).Assembly);
				}
				return Class0.resourceMan;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002291 File Offset: 0x00000491
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002298 File Offset: 0x00000498
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Class0.resourceCulture;
			}
			set
			{
				Class0.resourceCulture = value;
			}
		}

		// Token: 0x0400004B RID: 75
		private static ResourceManager resourceMan;

		// Token: 0x0400004C RID: 76
		private static CultureInfo resourceCulture;
	}
}
