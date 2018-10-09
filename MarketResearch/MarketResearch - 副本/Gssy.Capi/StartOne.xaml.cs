using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Control;

namespace Gssy.Capi
{
	public partial class StartOne : Window
	{
		public StartOne()
		{
			this.InitializeComponent();
		}

		private void method_0(object sender, RoutedEventArgs e)
		{
			Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata
			{
				DefaultValue = 5
			});
		}

		private void method_1(object sender, EventArgs e)
		{
			SurveyMsg.StartOne = "StartOne_true";
			SurveyMsg.VersionID = this.oSurveyConfigBiz.GetByCodeTextRead("VersionID");
			this.txtTitle.Text = SurveyMsg.MsgProjectName;
			this.txtVersion.Text = SurveyMsg.VersionText + SurveyMsg.VersionID;
			if (this.IsFirstActive)
			{
				this.IsFirstActive = false;
				if (!this.method_3())
				{
					this.method_2(SurveyMsg.MsgNoKeyFile);
					return;
				}
				this.MySurveyId = this.method_4(this.KeyFile);
				if (this.MySurveyId == "")
				{
					this.method_2(SurveyMsg.MsgErrorKeyFile);
					return;
				}
				SurveyHelper.SurveyFirstPage = "STARTONE";
				SurveyHelper.SurveyID = this.MySurveyId;
				this.method_5(this.MySurveyId);
			}
		}

		private void method_2(string string_0)
		{
			this.txtMsg.Text = string_0;
			this.UCLoading.Visibility = Visibility.Collapsed;
			this.btnExit.Visibility = Visibility.Visible;
		}

		private bool method_3()
		{
			this.KeyFile = this.oCK.SearchKeyFileOfStartOne(this.strRarSource, ".key");
			return this.KeyFile != "";
		}

		private string method_4(string string_0)
		{
			return this.oCK.GetIDFromKeyFile(this.KeyFile, SurveyMsg.ProjectId, SurveyMsg.SurveyId_Length);
		}
		private void method_5(string string_0)
		{
			if (!this.oSurvey.ExistSurvey(string_0))
			{
				this.txtMsg.Text = SurveyMsg.MsgNewSurvey;
				if (this.method_6())
				{
					new MainWindow().Show();
					base.Close();
					return;
				}
			}
			else
			{
				this.oSurvey.GetBySurveyId(this.MySurveyId);
				if (this.oSurvey.MySurvey.IS_FINISH != 1)
				{
					if (this.oSurvey.MySurvey.IS_FINISH != 2)
					{
						if (this.method_7())
						{
							new MainWindow().Show();
							base.Close();
							return;
						}
						return;
					}
				}
				if (this.method_8(this.MySurveyId))
				{
					this.method_2(string.Format(SurveyMsg.MsgFinishAndUploaded, this.MySurveyId));
					return;
				}
				this.method_9();
				return;
			}
		}

		private bool method_6()
		{
			this.SurveyId_City_Length = 1;
			SurveyHelper.SurveyID = this.MySurveyId;
			SurveyHelper.SurveyCity = this.MySurveyId.Substring(0, this.SurveyId_City_Length);
			SurveyHelper.SurveyStart = "STARTONE";
			this.IsRandomOK = this.oRandom.RandomSurveyMain(this.MySurveyId);
			string versionID = SurveyMsg.VersionID;
			string surveyCity = SurveyHelper.SurveyCity;
			string surveyExtend = SurveyHelper.SurveyExtend1;
			string projectId = SurveyMsg.ProjectId;
			string clientId = SurveyMsg.ClientId;
			this.oSurvey.AddSurvey(this.MySurveyId, versionID, surveyCity, projectId, clientId, surveyExtend);
			this.oQuestion.QuestionName = "SURVEY_CODE";
			this.oQuestion.FillText = this.MySurveyId;
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			this.oSurvey.SaveOneAnswer(this.MySurveyId, SurveyHelper.SurveySequence, "CITY", surveyCity);
			this.oSurvey.SaveOneAnswer(this.MySurveyId, SurveyHelper.SurveySequence, "PC_Code", this.oSurveyConfigBiz.GetByCodeText("PCCode"));
			if (!this.IsRandomOK)
			{
				this.method_2(SurveyMsg.MsgErrorSysSlow);
				this.oRandom.DeleteRandom(this.MySurveyId);
				Thread.Sleep(1000);
				return false;
			}
			if (this.oSurvey.GetCityCode(this.MySurveyId) != null && this.oSurvey.ExistSurvey(this.MySurveyId))
			{
				return true;
			}
			this.method_2(string.Format(SurveyMsg.MsgErrorCreateSurvey, this.MySurveyId));
			return false;
		}

		private bool method_7()
		{
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			this.MyNav.LoadPage(this.MySurveyId, roadMapVersion);
			SurveyHelper.SurveyID = this.MySurveyId;
			SurveyHelper.SurveyCity = this.MyNav.Survey.CITY_ID;
			SurveyHelper.CircleACount = this.MyNav.Survey.CIRCLE_A_COUNT;
			SurveyHelper.CircleACurrent = this.MyNav.Survey.CIRCLE_A_CURRENT;
			SurveyHelper.CircleBCount = this.MyNav.Survey.CIRCLE_B_COUNT;
			SurveyHelper.CircleBCurrent = this.MyNav.Survey.CIRCLE_B_CURRENT;
			this.oSurvey.UpdateSurveyLastTime(this.MySurveyId);
			SurveyHelper.SurveyStart = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.SurveySequence = this.MyNav.Survey.SEQUENCE_ID;
			SurveyHelper.NavCurPage = this.MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = this.MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavLoad = 1;
			SurveyHelper.NavOperation = "Normal";
			SurveyHelper.NavGoBackTimes = 0;
			return true;
		}

		private bool method_8(string string_0)
		{
			return this.oSurvey.CheckUploadSync(string_0);
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Application.Current.Shutdown();
		}

		private void method_9()
		{
			string arg = "000";
			bool bool_ = SurveyMsg.VersionID.IndexOf("正式版") <= -1;
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
				this.oSurvey.AddSurvyeSync(this.MySurveyId, SurveyMsg.MsgUploadSingleVersion, 1);
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

		private void ImgLogo_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void ImgLogo2_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void txtVersion_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate method_10(Type type_0, string string_0)
		{
			return Delegate.CreateDelegate(type_0, this, string_0);
		}

		private bool IsFirstActive = true;

		private string MySurveyId;

		private string CurPageId;

		private string CityCode;

		private NavBase MyNav = new NavBase();

		private QFill oQuestion = new QFill();

		private SurveyBiz oSurvey = new SurveyBiz();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private CheckExpiredClass oCK = new CheckExpiredClass();

		private string KeyFile = "";

		private int MyStatus;

		private int SurveyId_Length;

		private int SurveyId_City_Length;

		private int SurveyId_Number_Length;

		private RandomBiz oRandom = new RandomBiz();

		private bool IsRandomOK;

		private string strRarSource = Environment.CurrentDirectory + "\\OutPut\\";

		private string strRarOutputFolder = Environment.CurrentDirectory + "\\Upload\\";

		private string strRarFileName = "";

		private string strRarPassword = "GSSY.capi";

		private bool strRarExcludeRootFolder;

		private bool strRarIncludeSubFolder = true;
	}
}
