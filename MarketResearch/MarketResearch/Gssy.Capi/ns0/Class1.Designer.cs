using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ns0
{
	// Token: 0x02000009 RID: 9
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Class1
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000239D File Offset: 0x0000059D
		internal Class1()
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000023A5 File Offset: 0x000005A5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Class1.resourceMan == null)
				{
					Class1.resourceMan = new ResourceManager(global::GClass0.smethod_0("YŮɯ͢д՚ٹݧࡿऻ੄ୡ౽ൡ๵ཽၺᅤቩ፸ᐤᕛ᙭᝴ᡩᥰ᩶᭠ᱧᵲ"), typeof(Class1).Assembly);
				}
				return Class1.resourceMan;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000023D6 File Offset: 0x000005D6
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000023DD File Offset: 0x000005DD
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Class1.resourceCulture;
			}
			set
			{
				Class1.resourceCulture = value;
			}
		}

		// Token: 0x04000074 RID: 116
		private static ResourceManager resourceMan;

		// Token: 0x04000075 RID: 117
		private static CultureInfo resourceCulture;
	}
}
