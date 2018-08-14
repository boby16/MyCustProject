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
	// Token: 0x02000004 RID: 4
	public partial class StartOne : Window
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00005310 File Offset: 0x00003510
		public StartOne()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002178 File Offset: 0x00000378
		private void method_0(object sender, RoutedEventArgs e)
		{
			Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata
			{
				DefaultValue = 5
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000053E0 File Offset: 0x000035E0
		private void method_1(object sender, EventArgs e)
		{
			SurveyMsg.StartOne = global::GClass0.smethod_0("^Ÿɪ͸ѽՇ٩ݣ࡚॰ੱ୷౤");
			SurveyMsg.VersionID = this.oSurveyConfigBiz.GetByCodeTextRead(global::GClass0.smethod_0("_ŭɵ͵Ѭի٭݋ࡅ"));
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
				if (this.MySurveyId == global::GClass0.smethod_0(""))
				{
					this.method_2(SurveyMsg.MsgErrorKeyFile);
					return;
				}
				SurveyHelper.SurveyFirstPage = global::GClass0.smethod_0("[œɇ͗ѐՌٌ݄");
				SurveyHelper.SurveyID = this.MySurveyId;
				this.method_5(this.MySurveyId);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000219F File Offset: 0x0000039F
		private void method_2(string string_0)
		{
			this.txtMsg.Text = string_0;
			this.UCLoading.Visibility = Visibility.Collapsed;
			this.btnExit.Visibility = Visibility.Visible;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021C5 File Offset: 0x000003C5
		private bool method_3()
		{
			this.KeyFile = this.oCK.SearchKeyFileOfStartOne(this.strRarSource, global::GClass0.smethod_0("*Ũɧ͸"));
			return this.KeyFile != global::GClass0.smethod_0("");
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021FD File Offset: 0x000003FD
		private string method_4(string string_0)
		{
			return this.oCK.GetIDFromKeyFile(this.KeyFile, SurveyMsg.ProjectId, SurveyMsg.SurveyId_Length);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000054C0 File Offset: 0x000036C0
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

		// Token: 0x06000021 RID: 33 RVA: 0x00005584 File Offset: 0x00003784
		private bool method_6()
		{
			this.SurveyId_City_Length = 1;
			SurveyHelper.SurveyID = this.MySurveyId;
			SurveyHelper.SurveyCity = this.MySurveyId.Substring(0, this.SurveyId_City_Length);
			SurveyHelper.SurveyStart = global::GClass0.smethod_0("[œɇ͗ѐՌٌ݄");
			this.IsRandomOK = this.oRandom.RandomSurveyMain(this.MySurveyId);
			string versionID = SurveyMsg.VersionID;
			string surveyCity = SurveyHelper.SurveyCity;
			string surveyExtend = SurveyHelper.SurveyExtend1;
			string projectId = SurveyMsg.ProjectId;
			string clientId = SurveyMsg.ClientId;
			this.oSurvey.AddSurvey(this.MySurveyId, versionID, surveyCity, projectId, clientId, surveyExtend);
			this.oQuestion.QuestionName = global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄");
			this.oQuestion.FillText = this.MySurveyId;
			this.oQuestion.BeforeSave();
			this.oQuestion.Save(this.MySurveyId, SurveyHelper.SurveySequence);
			this.oSurvey.SaveOneAnswer(this.MySurveyId, SurveyHelper.SurveySequence, global::GClass0.smethod_0("GŊɖ͘"), surveyCity);
			this.oSurvey.SaveOneAnswer(this.MySurveyId, SurveyHelper.SurveySequence, global::GClass0.smethod_0("WŅɚ͇Ѭզ٤"), this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("VņɇͬѦդ")));
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

		// Token: 0x06000022 RID: 34 RVA: 0x00005724 File Offset: 0x00003924
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
			SurveyHelper.NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");
			SurveyHelper.NavGoBackTimes = 0;
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000221A File Offset: 0x0000041A
		private bool method_8(string string_0)
		{
			return this.oSurvey.CheckUploadSync(string_0);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002228 File Offset: 0x00000428
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Application.Current.Shutdown();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00005840 File Offset: 0x00003A40
		private void method_9()
		{
			string arg = global::GClass0.smethod_0("3Ĳȱ");
			bool bool_ = SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("歠帍灉")) <= -1;
			alioss alioss = new alioss(SurveyMsg.OSSRegion, bool_, SurveyMsg.ProjectId);
			if (!alioss.CreateOss())
			{
				this.txtMsg.Text = alioss.OutMessage;
				return;
			}
			SurveyConfigBiz surveyConfigBiz = new SurveyConfigBiz();
			string byCodeText = surveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("[Žɠͤѫխٛݢࡷ॰੡୭ౡ൤"));
			string newUploadSequence = alioss.GetNewUploadSequence(byCodeText);
			if (byCodeText != newUploadSequence)
			{
				surveyConfigBiz.Save(global::GClass0.smethod_0("[Žɠͤѫխٛݢࡷ॰੡୭ౡ൤"), newUploadSequence);
			}
			this.strRarFileName = string.Format(global::GClass0.smethod_0("`Īɤ͇Ѭԧ٨݋ࡨठਫଢ଼ూ൪๩ན၃ᅂቤ፥ᑴᕵᙸᜪᡱᥣᩳ"), byCodeText, arg, DateTime.Now);
			RarFile rarFile = new RarFile();
			if (!rarFile.Compress(this.strRarSource + global::GClass0.smethod_0("/Īɧͣѵ"), this.strRarOutputFolder, this.strRarFileName, this.strRarPassword, true, true))
			{
				this.txtMsg.Text = SurveyMsg.MsgNoDataFile;
				this.btnExit.IsEnabled = true;
				return;
			}
			Thread.Sleep(1000);
			if (alioss.UploadToOss(this.strRarOutputFolder, this.strRarFileName))
			{
				surveyConfigBiz.Save(global::GClass0.smethod_0("BŬɿͿџչ٤ݨࡧॡੂ୪౮൤"), this.strRarFileName);
				this.oSurvey.AddSurvyeSync(this.MySurveyId, SurveyMsg.MsgUploadSingleVersion, 1);
				string string_ = this.strRarOutputFolder + this.strRarFileName;
				string string_2 = this.strRarOutputFolder + this.strRarFileName.Replace(global::GClass0.smethod_0("*űɣͳ"), global::GClass0.smethod_0("]")).Substring(byCodeText.Length + 1);
				bool flag = rarFile.Extract(string_, this.strRarOutputFolder, string_2, this.strRarPassword);
				Thread.Sleep(1000);
				if (flag)
				{
					string searchPattern = global::GClass0.smethod_0("UįȪͧѣյ");
					foreach (FileInfo fileInfo in new DirectoryInfo(Environment.CurrentDirectory + global::GClass0.smethod_0("[ŉɰͰѓշٵ")).EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly))
					{
						fileInfo.Delete();
					}
				}
				this.txtMsg.Text = SurveyMsg.MsgEndSurveyInfo;
				return;
			}
			this.txtMsg.Text = alioss.OutMessage;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000223A File Offset: 0x0000043A
		private void ImgLogo_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000223A File Offset: 0x0000043A
		private void ImgLogo2_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000223A File Offset: 0x0000043A
		private void txtVersion_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000224F File Offset: 0x0000044F
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate method_10(Type type_0, string string_0)
		{
			return Delegate.CreateDelegate(type_0, this, string_0);
		}

		// Token: 0x04000016 RID: 22
		private bool IsFirstActive = true;

		// Token: 0x04000017 RID: 23
		private string MySurveyId;

		// Token: 0x04000018 RID: 24
		private string CurPageId;

		// Token: 0x04000019 RID: 25
		private string CityCode;

		// Token: 0x0400001A RID: 26
		private NavBase MyNav = new NavBase();

		// Token: 0x0400001B RID: 27
		private QFill oQuestion = new QFill();

		// Token: 0x0400001C RID: 28
		private SurveyBiz oSurvey = new SurveyBiz();

		// Token: 0x0400001D RID: 29
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x0400001E RID: 30
		private CheckExpiredClass oCK = new CheckExpiredClass();

		// Token: 0x0400001F RID: 31
		private string KeyFile = global::GClass0.smethod_0("");

		// Token: 0x04000020 RID: 32
		private int MyStatus;

		// Token: 0x04000021 RID: 33
		private int SurveyId_Length;

		// Token: 0x04000022 RID: 34
		private int SurveyId_City_Length;

		// Token: 0x04000023 RID: 35
		private int SurveyId_Number_Length;

		// Token: 0x04000024 RID: 36
		private RandomBiz oRandom = new RandomBiz();

		// Token: 0x04000025 RID: 37
		private bool IsRandomOK;

		// Token: 0x04000026 RID: 38
		private string strRarSource = Environment.CurrentDirectory + global::GClass0.smethod_0("Tňɳͱєնٶݝ");

		// Token: 0x04000027 RID: 39
		private string strRarOutputFolder = Environment.CurrentDirectory + global::GClass0.smethod_0("TŒɶͩѫբ٦ݝ");

		// Token: 0x04000028 RID: 40
		private string strRarFileName = global::GClass0.smethod_0("");

		// Token: 0x04000029 RID: 41
		private string strRarPassword = global::GClass0.smethod_0("Nśɔ͟Ыէ٢ݲࡨ");

		// Token: 0x0400002A RID: 42
		private bool strRarExcludeRootFolder;

		// Token: 0x0400002B RID: 43
		private bool strRarIncludeSubFolder = true;
	}
}
