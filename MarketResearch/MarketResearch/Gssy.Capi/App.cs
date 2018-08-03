using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Gssy.Capi
{
	public class App : Application
	{
		private bool _contentLoaded;

		private void _003F54_003F(object _003F347_003F, StartupEventArgs _003F348_003F)
		{
			new SyncTable().SyncReadToWrite();
			string language = SurveyMsg.Language;
			_003F56_003F(language, _003F487_003F._003F488_003F(""));
		}

		private void _003F55_003F(object _003F347_003F, ExitEventArgs _003F348_003F)
		{
		}

		private void _003F56_003F(string _003F356_003F, string _003F357_003F)
		{
			//IL_0052: Incompatible stack heights: 0 vs 1
			Collection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
			if (_003F357_003F != _003F487_003F._003F488_003F("\u007fŬȮ\u0361ѯ"))
			{
				mergedDictionaries.Remove(mergedDictionaries[4]);
				string uriString = _003F487_003F._003F488_003F((string)/*Error near IL_002a: Stack underflow*/) + _003F356_003F + _003F487_003F._003F488_003F("+żɢ\u036fѭ");
				ResourceDictionary resourceDictionary = new ResourceDictionary();
				resourceDictionary.Source = new Uri(uriString, UriKind.Relative);
				mergedDictionaries.Add(resourceDictionary);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			base.Startup += _003F54_003F;
			base.Exit += _003F55_003F;
			base.StartupUri = new Uri(_003F487_003F._003F488_003F("hŹɽ\u0369ѵղثݼ\u0862९੭"), UriKind.Relative);
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("2śɨ\u0369ѠԶ\u0654ݷ\u0865ॽਨୱ౾ൽ\u0e7fཡ\u1063ᅩብ\u137eᐦᕩᙷ\u1776ᠫ\u197c\u1a62\u1b6fᱭ"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0018:
			goto IL_000b;
		}

		[STAThread]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public static void Main()
		{
			new SplashScreen(_003F487_003F._003F488_003F("hżɫ\u0378ѣէٷݶ\u0861\u093e\u0a63\u0b7f\u0c62൬\u0e7fལၹᅪቺ።ᑣᕫᘪ\u1773ᡬᥦ")).Show(true);
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}
