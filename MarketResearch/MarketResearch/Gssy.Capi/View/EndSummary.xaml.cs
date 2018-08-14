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
	// Token: 0x0200004A RID: 74
	public partial class EndSummary : Page
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0008BAD0 File Offset: 0x00089CD0
		public EndSummary()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0008BB5C File Offset: 0x00089D5C
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(SurveyMsg.MsgConfirmEnd, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.Yes))
			{
				Application.Current.Shutdown();
				return;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0008BBA0 File Offset: 0x00089DA0
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.txtSurvey.Text = this.MySurveyId;
			this.oSurvey.RecordFileName = SurveyHelper.RecordFileName;
			this.EndType = 1;
			this.method_1();
			this.method_2();
			if (SurveyMsg.StartOne == global::GClass0.smethod_0("^Ÿɪ͸ѽՇ٩ݣ࡚॰ੱ୷౤"))
			{
				Thread.Sleep(2000);
				bool flag = true;
				int num = 0;
				int num2 = 10;
				while (flag)
				{
					if (File.Exists(this.strRarSource + global::GClass0.smethod_0("R") + this.MySurveyId + global::GClass0.smethod_0("DĪɧͣѵ")))
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
				if (SurveyHelper.Survey_Status == global::GClass0.smethod_0("EŤɪ͠ѧխ"))
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

		// Token: 0x060004EC RID: 1260 RVA: 0x0008BCC8 File Offset: 0x00089EC8
		private void method_1()
		{
			this.oSurvey.CloseSurvey(this.MySurveyId, this.EndType);
			Thread.Sleep(1000);
			SurveytoXml surveytoXml = new SurveytoXml();
			string currentDirectory = Environment.CurrentDirectory;
			surveytoXml.SaveSurveyAnswer(this.MySurveyId, currentDirectory, null, true);
			if (SurveyMsg.RecordIsOn == global::GClass0.smethod_0("]ūɮͣѹծـݻࡈ२ਗ਼୰౱൷๤") || SurveyMsg.IsSaveSequence == global::GClass0.smethod_0("Zšɂͱѹիٞݩࡺॿ੬୦౤ൣ๚཰ၱᅷቤ"))
			{
				surveytoXml.SaveSurveySequence(this.MySurveyId, currentDirectory, null, true);
			}
			if (SurveyMsg.FunctionAttachments == global::GClass0.smethod_0("^ŢɸͶѠպٽݿࡑॻ੺୬౯ൣ๧ཬၦᅳትፚᑰᕱᙷᝤ"))
			{
				surveytoXml.SaveSurveyAttach(this.MySurveyId, currentDirectory, null, true);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00003642 File Offset: 0x00001842
		private void method_2()
		{
			this.oSurvey.GetSummary(this.MySurveyId);
			this.DataGrid1.ItemsSource = this.oSurvey.QVSummary.ToList<V_Summary>();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0008BD74 File Offset: 0x00089F74
		private void method_3()
		{
			string arg = global::GClass0.smethod_0(":Ļȸ");
			bool bool_ = SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("浈諗灉")) >= 0 || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("漗砸灉")) >= 0 || SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("@Ŧɯͮ")) >= 0;
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
				this.oSurvey.AddSurvyeSync(SurveyHelper.SurveyID, SurveyMsg.MsgUploadSingleVersion, 1);
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

		// Token: 0x060004EF RID: 1263 RVA: 0x0008C004 File Offset: 0x0008A204
		private void method_4()
		{
			string surveyStart = SurveyHelper.SurveyStart;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			NavBase navBase = new NavBase();
			navBase.StartPage(surveyStart, roadMapVersion);
			string uriString = string.Format(global::GClass0.smethod_0("TłɁ͊К԰رݼ࡬५੶୰౻൶๢ོၻᅽረጽᐼᔣᘡ᝛ᡥ᥮᩽ᬦᱳᴷṻἫ⁼Ⅲ≯⍭"), navBase.RoadMap.FORM_NAME);
			base.NavigationService.Navigate(new Uri(uriString));
			if (SurveyHelper.NavLoad == 0)
			{
				SurveyHelper.SurveySequence = 1;
			}
			SurveyHelper.NavCurPage = navBase.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = navBase.RoadMap.FORM_NAME;
		}

		// Token: 0x0400092C RID: 2348
		private string MySurveyId;

		// Token: 0x0400092D RID: 2349
		private int EndType;

		// Token: 0x0400092E RID: 2350
		private SurveyBiz oSurvey = new SurveyBiz();

		// Token: 0x0400092F RID: 2351
		private PageNav oPageNav = new PageNav();

		// Token: 0x04000930 RID: 2352
		private string strRarSource = Environment.CurrentDirectory + global::GClass0.smethod_0("Tňɳͱєնٶݝ");

		// Token: 0x04000931 RID: 2353
		private string strRarOutputFolder = Environment.CurrentDirectory + global::GClass0.smethod_0("TŒɶͩѫբ٦ݝ");

		// Token: 0x04000932 RID: 2354
		private string strRarFileName = global::GClass0.smethod_0("");

		// Token: 0x04000933 RID: 2355
		private string strRarPassword = global::GClass0.smethod_0("Nśɔ͟Ыէ٢ݲࡨ");

		// Token: 0x04000934 RID: 2356
		private bool strRarExcludeRootFolder;

		// Token: 0x04000935 RID: 2357
		private bool strRarIncludeSubFolder = true;
	}
}
