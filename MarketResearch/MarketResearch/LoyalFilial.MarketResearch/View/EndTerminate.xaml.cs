using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using LoyalFilial.MarketResearch.BIZ;
using LoyalFilial.MarketResearch.Class;
using LoyalFilial.MarketResearch.Common;

namespace LoyalFilial.MarketResearch.View
{
	public partial class EndTerminate : Page
	{
		public EndTerminate()
		{
			this.InitializeComponent();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.txtSurvey.Text = this.MySurveyId;
			this.oSurvey.RecordFileName = SurveyHelper.RecordFileName;
			this.txtMsg.Text = SurveyMsg.MsgEndTerminate;
			this.labCase.Text = SurveyMsg.MsgFrmCurrentID + SurveyHelper.SurveyID;
			this.EndType = 2;
			this.method_1();
			if (SurveyMsg.StartOne == "StartOne_true")
			{
				this.method_2();
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
				this.method_3();
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
			string arg = "999";
			bool bool_ = SurveyMsg.VersionID.IndexOf("测试版") >= 0 || SurveyMsg.VersionID.IndexOf("演示版") >= 0 || SurveyMsg.VersionID.IndexOf("Demo") >= 0;
			AliOSS alioss = new AliOSS(SurveyMsg.OSSRegion, bool_, SurveyMsg.ProjectId);
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
			Thread.Sleep(500);
			if (alioss.UploadToOss(this.strRarOutputFolder, this.strRarFileName))
			{
				surveyConfigBiz.Save("LastUploadFile", this.strRarFileName);
				this.oSurvey.AddSurvyeSync(SurveyHelper.SurveyID, "单问卷版本上传OSS", 1);
				string string_ = this.strRarOutputFolder + this.strRarFileName;
				string string_2 = this.strRarOutputFolder + this.strRarFileName.Replace(".rar", "\\").Substring(byCodeText.Length + 1);
				if (rarFile.Extract(string_, this.strRarOutputFolder, string_2, this.strRarPassword))
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

		private void method_3()
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

		private string strRarSource = Environment.CurrentDirectory + "\\OutPut\\";

		private string strRarOutputFolder = Environment.CurrentDirectory + "\\Upload\\";

		private string strRarFileName = "";

		private string strRarPassword = "LoyalFilial.MarketResearch";

		private bool strRarExcludeRootFolder;

		private bool strRarIncludeSubFolder = true;
	}
}
