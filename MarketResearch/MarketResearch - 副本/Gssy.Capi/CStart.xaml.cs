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
	public partial class CStart : Window
	{
		public CStart()
		{
			this.InitializeComponent();
		}

		private void CStart_Load(object sender, RoutedEventArgs e)
		{
			SurveyMsg.StartOne = "StartOne_false";
			SurveyMsg.VersionID = this.oSurveyConfigBiz.GetByCodeTextRead("VersionID");
			this.txtTitle.Text = SurveyMsg.MsgProjectName;
			this.txtVersion.Text = SurveyMsg.VersionText + SurveyMsg.VersionID;
			this.oSurveyConfigBiz.Save("RecordIsOn", SurveyMsg.RecordIsOn);
			this.oSurveyConfigBiz.Save("RecordIsRunning", "false");
			this.Init();
			if (this.oSurveyConfigBiz.GetByCodeText("TouchPad") == "1")
			{
				SurveyHelper.IsTouch = "IsTouch_true";
			}
			else
			{
				SurveyHelper.IsTouch = "IsTouch_false";
			}
			if (SurveyMsg.FunctionUpload == "FunctionUpload_true")
			{
				this.btnUpload.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnUpload.Visibility = Visibility.Collapsed;
			}
			if (SurveyMsg.FunctionDelete == "FunctionDelete_true")
			{
				this.btnDelete.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnDelete.Visibility = Visibility.Collapsed;
			}
			this.btnAutoDo.Visibility = Visibility.Collapsed;
		}

		private void Init()
		{
			CheckExpiredClass checkExpiredClass = new CheckExpiredClass();
			checkExpiredClass._StartDate = SurveyMsg.VersionDate;
			checkExpiredClass._UseDays = SurveyMsg.TestVersionActiveDays;
			if (SurveyMsg.VersionID.IndexOf("正式版") > -1)
			{
				checkExpiredClass._UseDays = SurveyMsg.VersionActiveDays;
			}
			this.btnNav.Visibility = Visibility.Hidden;
			this.StkTools.Visibility = Visibility.Hidden;
			List<SurveyDetail> list = new List<SurveyDetail>();
			list = new SurveyDetailDal().GetDetails("JobName");
			if (list.Count > 0 && list[0].CODE_TEXT == SurveyMsg.MsgProjectName)
			{
                string text = "";
				if (SurveyMsg.IsCheckLicnese != "IsCheckLicnese_false")
				{
					text = checkExpiredClass.ExpiredFlag("", "", "", "", "", "", "", "", "", 500);
				}
				if (text != "")
				{
					if (text.Contains("C"))
					{
						MessageBox.Show(SurveyMsg.MsgErrorTime, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						return;
					}
					if (SurveyMsg.VersionID.IndexOf("测试版") < 0)
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
					if (File.Exists(SurveyHelper.DebugFlagFile) && SurveyHelper.ShowAutoDo == "ShowAutoDo_true")
					{
						this.btnAutoDo.Visibility = Visibility.Visible;
					}
				}
			}
		}

		private void btnNav_Click(object sender, RoutedEventArgs e)
		{
			string byCodeText = this.oSurveyConfigBiz.GetByCodeText("PCCode");
			if (byCodeText != null && !(byCodeText == ""))
			{
				string byCodeText2 = this.oSurveyConfigBiz.GetByCodeText("SurveyIDBegin");
				string byCodeText3 = this.oSurveyConfigBiz.GetByCodeText("SurveyIDEnd");
				string byCodeText4 = this.oSurveyConfigBiz.GetByCodeText("TouchPad");
				if (byCodeText2 != "")
				{
					SurveyHelper.SurveyCity = byCodeText2.Substring(0, byCodeText2.Length - SurveyMsg.SurveyIDEnd.Length);
					SurveyMsg.SurveyIDBegin = byCodeText2;
					SurveyMsg.SurveyIDEnd = byCodeText3;
					SurveyHelper.SurveyStart = SurveyHelper.SurveyCodePage;
					if (byCodeText4 == "1")
					{
						SurveyHelper.IsTouch = "IsTouch_true";
					}
					else
					{
						SurveyHelper.IsTouch = "IsTouch_false";
					}
				}
				new MainWindow().Show();
				base.Close();
				return;
			}
			MessageBox.Show(SurveyMsg.MsgNeedConfig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Application.Current.Shutdown();
		}

		private void btnConfig_Click(object sender, RoutedEventArgs e)
		{
			new SurveyRange().Show();
			base.Close();
		}

		private void btnUpload_Click(object sender, RoutedEventArgs e)
		{
			new SurveyCloud().Show();
			base.Close();
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			new SurveyDelete().Show();
			base.Close();
		}

		private void btnAutoDo_Click(object sender, RoutedEventArgs e)
		{
			new AutoDo().Show();
			base.Close();
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

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private UDPX oFunc = new UDPX();
	}
}
