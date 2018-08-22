using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;

namespace Gssy.Capi
{
	public partial class App : Application
	{
		private void App_Startup(object sender, StartupEventArgs e)
		{
			new SyncTable().SyncReadToWrite();
			string language = SurveyMsg.Language;
			this.AppStart(language, "");
		}
        
		private void App_Exit(object sender, ExitEventArgs e)
		{
		}
        
		private void AppStart(string langCur, string langOld)
		{
			Collection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
			if (langOld != "zh-cn")
			{
				mergedDictionaries.Remove(mergedDictionaries[4]);
				string uriString = $"Resources/Languages/{langCur}.xaml";
				mergedDictionaries.Add(new ResourceDictionary
				{
					Source = new Uri(uriString, UriKind.Relative)
				});
			}
		}
    }
}
