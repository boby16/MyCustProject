using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.View
{
	public partial class EndSummary : Page
	{
		public EndSummary()
		{
			this.InitializeComponent();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(SurveyMsg.MsgConfirmEnd, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.Yes))
			{
				Application.Current.Shutdown();
				return;
			}
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.txtSurvey.Text = this.MySurveyId;
			this.oSurvey.RecordFileName = SurveyHelper.RecordFileName;
			this.EndType = 1;
			this.method_1();
			this.method_2();
			if (SurveyMsg.StartOne == "StartOne_true")
			{
				Thread.Sleep(2000);
				bool flag = true;
				int num = 0;
				int num2 = 10;
				while (flag)
				{
					if (File.Exists(this.strRarSource + "S" + this.MySurveyId + "A.dat"))
					{
						flag = false;
					}
					Thread.Sleep(500);
					num++;
					if (num > num2)
					{
						flag = false;
					}
				}
				this.method_3();
			}
			if (SurveyHelper.AutoDo)
			{
				if (SurveyHelper.Survey_Status == "Cancel")
				{
					SurveyHelper.AutoDo_Filled++;
				}
				else
				{
					SurveyHelper.AutoDo_Create++;
				}
				SurveyHelper.AutoDo_Count++;
				if (SurveyHelper.AutoDo_Count >= SurveyHelper.AutoDo_Total)
				{
					SurveyHelper.AutoDo_CityOrder++;
					SurveyHelper.AutoDo_Count = 0;
				}
				this.method_4();
			}
		}

		private void method_1()
		{
			this.oSurvey.CloseSurvey(this.MySurveyId, this.EndType);
			Thread.Sleep(1000);
			SurveytoXml surveytoXml = new SurveytoXml();
			string currentDirectory = Environment.CurrentDirectory;
			surveytoXml.SaveSurveyAnswer(this.MySurveyId, currentDirectory, null, true);
			if (SurveyMsg.RecordIsOn == "RecordIsOn_true" || SurveyMsg.IsSaveSequence == "IsSaveSequence_true")
			{
				surveytoXml.SaveSurveySequence(this.MySurveyId, currentDirectory, null, true);
			}
			if (SurveyMsg.FunctionAttachments == "FunctionAttachments_true")
			{
				surveytoXml.SaveSurveyAttach(this.MySurveyId, currentDirectory, null, true);
			}
		}

		private void method_2()
		{
			this.oSurvey.GetSummary(this.MySurveyId);
			this.DataGrid1.ItemsSource = this.oSurvey.QVSummary.ToList<V_Summary>();
		}

		private void method_3()
		{
			string arg = "999";
			bool bool_ = SurveyMsg.VersionID.IndexOf("测试版") >= 0 || SurveyMsg.VersionID.IndexOf("演示版") >= 0 || SurveyMsg.VersionID.IndexOf("Demo") >= 0;
			alioss alioss = new alioss(SurveyMsg.OSSRegion, bool_, SurveyMsg.ProjectId);
			if (!alioss.CreateOss())
			{
				this.txtMsg.Text = alioss.OutMessage;
				return;
			}
			SurveyConfigBiz surveyConfigBiz = new SurveyConfigBiz();
			string byCodeText = surveyConfigBiz.GetByCodeText("UploadSequence");
			string newUploadSequence = alioss.GetNewUploadSequence(byCodeText);
			if (byCodeText != newUploadSequence)
			{
				surveyConfigBiz.Save("UploadSequence", newUploadSequence);
			}
			this.strRarFileName = string.Format("{0}_{1}_{2:MMdd_HHmmss}.rar", byCodeText, arg, DateTime.Now);
			RarFile rarFile = new RarFile();
			if (!rarFile.Compress(this.strRarSource + "*.dat", this.strRarOutputFolder, this.strRarFileName, this.strRarPassword, true, true))
			{
				this.txtMsg.Text = SurveyMsg.MsgNoDataFile;
				this.btnExit.IsEnabled = true;
				return;
			}
			Thread.Sleep(1000);
			if (alioss.UploadToOss(this.strRarOutputFolder, this.strRarFileName))
			{
				surveyConfigBiz.Save("LastUploadFile", this.strRarFileName);
				this.oSurvey.AddSurvyeSync(SurveyHelper.SurveyID, SurveyMsg.MsgUploadSingleVersion, 1);
				string string_ = this.strRarOutputFolder + this.strRarFileName;
				string string_2 = this.strRarOutputFolder + this.strRarFileName.Replace(".rar", "\\").Substring(byCodeText.Length + 1);
				bool flag = rarFile.Extract(string_, this.strRarOutputFolder, string_2, this.strRarPassword);
				Thread.Sleep(1000);
				if (flag)
				{
					string searchPattern = "S*.dat";
					foreach (FileInfo fileInfo in new DirectoryInfo(Environment.CurrentDirectory + "\\OutPut").EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly))
					{
						fileInfo.Delete();
					}
				}
				this.txtMsg.Text = SurveyMsg.MsgEndSurveyInfo;
				return;
			}
			this.txtMsg.Text = alioss.OutMessage;
		}

		private void method_4()
		{
			string surveyStart = SurveyHelper.SurveyStart;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			NavBase navBase = new NavBase();
			navBase.StartPage(surveyStart, roadMapVersion);
			string uriString = string.Format("pack://application:,,,/View/{0}.xaml", navBase.RoadMap.FORM_NAME);
			base.NavigationService.Navigate(new Uri(uriString));
			if (SurveyHelper.NavLoad == 0)
			{
				SurveyHelper.SurveySequence = 1;
			}
			SurveyHelper.NavCurPage = navBase.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = navBase.RoadMap.FORM_NAME;
		}

		private string MySurveyId;

		private int EndType;

		private SurveyBiz oSurvey = new SurveyBiz();

		private PageNav oPageNav = new PageNav();

		private string strRarSource = Environment.CurrentDirectory + "\\OutPut\\";

		private string strRarOutputFolder = Environment.CurrentDirectory + "\\Upload\\";

		private string strRarFileName = "";

		private string strRarPassword = "GSSY.capi";

		private bool strRarExcludeRootFolder;

		private bool strRarIncludeSubFolder = true;
	}
}
