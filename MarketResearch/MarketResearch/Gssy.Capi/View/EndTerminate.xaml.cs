using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;

namespace Gssy.Capi.View
{
	// Token: 0x0200004B RID: 75
	public partial class EndTerminate : Page
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0008C154 File Offset: 0x0008A354
		public EndTerminate()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00002283 File Offset: 0x00000483
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0008C1D4 File Offset: 0x0008A3D4
		private void method_0(object sender, RoutedEventArgs e)
		{
			this.MySurveyId = SurveyHelper.SurveyID;
			this.txtSurvey.Text = this.MySurveyId;
			this.oSurvey.RecordFileName = SurveyHelper.RecordFileName;
			this.txtMsg.Text = SurveyMsg.MsgEndTerminate;
			this.labCase.Text = SurveyMsg.MsgFrmCurrentID + SurveyHelper.SurveyID;
			this.EndType = 2;
			this.method_1();
			if (SurveyMsg.StartOne == global::GClass0.smethod_0("^Ÿɪ͸ѽՇ٩ݣ࡚॰ੱ୷౤"))
			{
				this.method_2();
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
				this.method_3();
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0008C2C8 File Offset: 0x0008A4C8
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

		// Token: 0x060004F6 RID: 1270 RVA: 0x0008C374 File Offset: 0x0008A574
		private void method_2()
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
			Thread.Sleep(500);
			if (alioss.UploadToOss(this.strRarOutputFolder, this.strRarFileName))
			{
				surveyConfigBiz.Save(global::GClass0.smethod_0("BŬɿͿџչ٤ݨࡧॡੂ୪౮൤"), this.strRarFileName);
				this.oSurvey.AddSurvyeSync(SurveyHelper.SurveyID, global::GClass0.smethod_0("卟铧兿煏挪䬏䤤݌ࡑ॒"), 1);
				string string_ = this.strRarOutputFolder + this.strRarFileName;
				string string_2 = this.strRarOutputFolder + this.strRarFileName.Replace(global::GClass0.smethod_0("*űɣͳ"), global::GClass0.smethod_0("]")).Substring(byCodeText.Length + 1);
				if (rarFile.Extract(string_, this.strRarOutputFolder, string_2, this.strRarPassword))
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

		// Token: 0x060004F7 RID: 1271 RVA: 0x0008C004 File Offset: 0x0008A204
		private void method_3()
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

		// Token: 0x0400093B RID: 2363
		private string MySurveyId;

		// Token: 0x0400093C RID: 2364
		private int EndType;

		// Token: 0x0400093D RID: 2365
		private SurveyBiz oSurvey = new SurveyBiz();

		// Token: 0x0400093E RID: 2366
		private string strRarSource = Environment.CurrentDirectory + global::GClass0.smethod_0("Tňɳͱєնٶݝ");

		// Token: 0x0400093F RID: 2367
		private string strRarOutputFolder = Environment.CurrentDirectory + global::GClass0.smethod_0("TŒɶͩѫբ٦ݝ");

		// Token: 0x04000940 RID: 2368
		private string strRarFileName = global::GClass0.smethod_0("");

		// Token: 0x04000941 RID: 2369
		private string strRarPassword = global::GClass0.smethod_0("Nśɔ͟Ыէ٢ݲࡨ");

		// Token: 0x04000942 RID: 2370
		private bool strRarExcludeRootFolder;

		// Token: 0x04000943 RID: 2371
		private bool strRarIncludeSubFolder = true;
	}
}
