using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;

namespace Gssy.Capi
{
	// Token: 0x02000006 RID: 6
	public partial class App : Application
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00005FD4 File Offset: 0x000041D4
		private void App_Startup(object sender, StartupEventArgs e)
		{
			new SyncTable().SyncReadToWrite();
			string language = SurveyMsg.Language;
			this.method_0(language, global::GClass0.smethod_0(""));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000228F File Offset: 0x0000048F
		private void App_Exit(object sender, ExitEventArgs e)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00006004 File Offset: 0x00004204
		private void method_0(string string_0, string string_1)
		{
			Collection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
			if (string_1 != global::GClass0.smethod_0("\u007fŬȮ͡ѯ"))
			{
				mergedDictionaries.Remove(mergedDictionaries[4]);
				string uriString = global::GClass0.smethod_0("FŶɡ;ѥս٭ݨࡿत੆୨౦ൠ๳ཤၣᅦቱጮ") + string_0 + global::GClass0.smethod_0("+żɢͯѭ");
				mergedDictionaries.Add(new ResourceDictionary
				{
					Source = new Uri(uriString, UriKind.Relative)
				});
			}
		}
    }
}
