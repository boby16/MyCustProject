using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Gssy.Capi.View;

namespace Gssy.Capi
{
	// Token: 0x02000007 RID: 7
	public partial class CStart : Window
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000022C1 File Offset: 0x000004C1
		public CStart()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000060E8 File Offset: 0x000042E8
		private void method_0(object sender, RoutedEventArgs e)
		{
			SurveyMsg.StartOne = global::GClass0.smethod_0("]Źɭ͹ѾՆ٦ݢ࡙ॣ੥୯౱൤");
			SurveyMsg.VersionID = this.oSurveyConfigBiz.GetByCodeTextRead(global::GClass0.smethod_0("_ŭɵ͵Ѭի٭݋ࡅ"));
			this.txtTitle.Text = SurveyMsg.MsgProjectName;
			this.txtVersion.Text = SurveyMsg.VersionText + SurveyMsg.VersionID;
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("XŬɫͨѴաٍݰࡍ९"), SurveyMsg.RecordIsOn);
			this.oSurveyConfigBiz.Save(global::GClass0.smethod_0("]ūɮͣѹծـݻࡕॳ੫୪౪൬๦"), global::GClass0.smethod_0("cťɯͱѤ"));
			this.method_1();
			if (this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("\\ŨɳͦѬՓ٣ݥ")) == global::GClass0.smethod_0("0"))
			{
				SurveyHelper.IsTouch = global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤");
			}
			else
			{
				SurveyHelper.IsTouch = global::GClass0.smethod_0("DſɟͥѼիٯݙࡣ॥੯ୱ౤");
			}
			if (SurveyMsg.FunctionUpload == global::GClass0.smethod_0("Uŧɿͳѻէ٢ݢ࡞ॺ੥୧౦ൢ๚཰ၱᅷቤ"))
			{
				this.btnUpload.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnUpload.Visibility = Visibility.Collapsed;
			}
			if (SurveyMsg.FunctionDelete == global::GClass0.smethod_0("Uŧɿͳѻէ٢ݢࡏ९੥୭౳ൣ๚཰ၱᅷቤ"))
			{
				this.btnDelete.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnDelete.Visibility = Visibility.Collapsed;
			}
			this.btnAutoDo.Visibility = Visibility.Collapsed;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000623C File Offset: 0x0000443C
		private void method_1()
		{
			CheckExpiredClass checkExpiredClass = new CheckExpiredClass();
			checkExpiredClass._StartDate = SurveyMsg.VersionDate;
			checkExpiredClass._UseDays = SurveyMsg.TestVersionActiveDays;
			if (SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("歠帍灉")) > -1)
			{
				checkExpiredClass._UseDays = SurveyMsg.VersionActiveDays;
			}
			this.btnNav.Visibility = Visibility.Hidden;
			this.StkTools.Visibility = Visibility.Hidden;
			List<SurveyDetail> list = new List<SurveyDetail>();
			list = new SurveyDetailDal().GetDetails(global::GClass0.smethod_0("Mũɧ͊Ѣկ٤"));
			if (list.Count > 0 && list[0].CODE_TEXT == SurveyMsg.MsgProjectName)
			{
				string text = global::GClass0.smethod_0("");
				if (SurveyMsg.IsCheckLicnese != global::GClass0.smethod_0("]Šɑ͹ѵլ٥݁ࡥ२੤୬౻ൢ๙ལၥᅯቱ፤"))
				{
					text = checkExpiredClass.ExpiredFlag(global::GClass0.smethod_0(""), global::GClass0.smethod_0(""), global::GClass0.smethod_0(""), global::GClass0.smethod_0(""), global::GClass0.smethod_0(""), global::GClass0.smethod_0(""), global::GClass0.smethod_0(""), global::GClass0.smethod_0(""), global::GClass0.smethod_0(""), 500);
				}
				if (text != global::GClass0.smethod_0(""))
				{
					if (text.Contains(global::GClass0.smethod_0("B")))
					{
						MessageBox.Show(SurveyMsg.MsgErrorTime, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						return;
					}
					if (SurveyMsg.VersionID.IndexOf(global::GClass0.smethod_0("浈諗灉")) < 0)
					{
						MessageBox.Show(SurveyMsg.MsgOverTime, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						return;
					}
					MessageBox.Show(SurveyMsg.MsgTestOverTime, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					return;
				}
				else
				{
					this.btnNav.Visibility = Visibility.Visible;
					this.StkTools.Visibility = Visibility.Visible;
					this.btnAutoDo.Visibility = Visibility.Collapsed;
					if (File.Exists(SurveyHelper.DebugFlagFile) && SurveyHelper.ShowAutoDo == global::GClass0.smethod_0("\\Ŧɢͻъտٽݧࡃ३ਗ਼୰౱൷๤"))
					{
						this.btnAutoDo.Visibility = Visibility.Visible;
					}
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00006434 File Offset: 0x00004634
		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("VņɇͬѦդ"));
			if (byCodeText != null && !(byCodeText == global::GClass0.smethod_0("")))
			{
				string byCodeText2 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("^ŹɹͼѬձَ݂ࡇॡ੤୫౯"));
				string byCodeText3 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("Xſɻ;Ѣտٌ݀ࡆ६੥"));
				string byCodeText4 = this.oSurveyConfigBiz.GetByCodeText(global::GClass0.smethod_0("\\ŨɳͦѬՓ٣ݥ"));
				if (byCodeText2 != global::GClass0.smethod_0(""))
				{
					SurveyHelper.SurveyCity = byCodeText2.Substring(0, byCodeText2.Length - SurveyMsg.SurveyIDEnd.Length);
					SurveyMsg.SurveyIDBegin = byCodeText2;
					SurveyMsg.SurveyIDEnd = byCodeText3;
					SurveyHelper.SurveyStart = SurveyHelper.SurveyCodePage;
					if (byCodeText4 == global::GClass0.smethod_0("0"))
					{
						SurveyHelper.IsTouch = global::GClass0.smethod_0("EŸɞͦѽդٮݚࡰॱ੷୤");
					}
					else
					{
						SurveyHelper.IsTouch = global::GClass0.smethod_0("DſɟͥѼիٯݙࡣ॥੯ୱ౤");
					}
				}
				new MainWindow().Show();
				base.Close();
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNeedConfig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002228 File Offset: 0x00000428
		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Application.Current.Shutdown();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000022E5 File Offset: 0x000004E5
		private void btnConfig_Click(object sender, RoutedEventArgs e)
		{
			new SurveyRange().Show();
			base.Close();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000022F7 File Offset: 0x000004F7
		private void btnUpload_Click(object sender, RoutedEventArgs e)
		{
			new SurveyCloud().Show();
			base.Close();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002309 File Offset: 0x00000509
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			new SurveyDelete().Show();
			base.Close();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000231B File Offset: 0x0000051B
		private void btnAutoDo_Click(object sender, RoutedEventArgs e)
		{
			new AutoDo().Show();
			base.Close();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000223A File Offset: 0x0000043A
		private void ImgLogo_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000223A File Offset: 0x0000043A
		private void ImgLogo2_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000223A File Offset: 0x0000043A
		private void txtVersion_MouseUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x04000043 RID: 67
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		// Token: 0x04000044 RID: 68
		private UDPX oFunc = new UDPX();
	}
}
