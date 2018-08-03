using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Gssy.Capi.View;
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

namespace Gssy.Capi
{
	public class CStart : Window, IComponentConnector
	{
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private UDPX oFunc = new UDPX();

		internal Grid LayoutRoot;

		internal Grid loginArea;

		internal Image ImgLogo;

		internal Image ImgLogo2;

		internal TextBlock txtTitle;

		internal Button btnNav;

		internal Button btnExit;

		internal StackPanel StkTools;

		internal Button btnAutoDo;

		internal Button btnDelete;

		internal Button btnUpload;

		internal Button btnConfig;

		internal TextBlock txtVersion;

		private bool _contentLoaded;

		public CStart()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0156: Incompatible stack heights: 0 vs 2
			//IL_016c: Incompatible stack heights: 0 vs 2
			SurveyMsg.StartOne = _003F487_003F._003F488_003F("]Źɭ\u0379ѾՆ٦ݢ\u0859\u0963\u0a65୯\u0c71\u0d64");
			SurveyMsg.VersionID = oSurveyConfigBiz.GetByCodeTextRead(_003F487_003F._003F488_003F("_ŭɵ\u0375Ѭի٭\u074bࡅ"));
			txtTitle.Text = SurveyMsg.MsgProjectName;
			txtVersion.Text = SurveyMsg.VersionText + SurveyMsg.VersionID;
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("XŬɫ\u0368Ѵա\u064dݰࡍ९"), SurveyMsg.RecordIsOn);
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡕॳ੫୪౪൬\u0e66"), _003F487_003F._003F488_003F("cťɯͱѤ"));
			_003F57_003F();
			if (!(oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("\\Ũɳ\u0366ѬՓ٣ݥ")) == _003F487_003F._003F488_003F("0")))
			{
				SurveyHelper.IsTouch = _003F487_003F._003F488_003F("Dſɟ\u0365Ѽիٯݙ\u0863॥੯ୱ\u0c64");
			}
			else
			{
				SurveyHelper.IsTouch = _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64");
			}
			if (SurveyMsg.FunctionUpload == _003F487_003F._003F488_003F("Uŧɿͳѻէ٢ݢ࡞ॺ\u0a65୧౦\u0d62๚\u0f70\u1071ᅷቤ"))
			{
				Button btnUpload2 = btnUpload;
				((UIElement)/*Error near IL_00ed: Stack underflow*/).Visibility = (Visibility)/*Error near IL_00ed: Stack underflow*/;
			}
			else
			{
				btnUpload.Visibility = Visibility.Collapsed;
			}
			if (SurveyMsg.FunctionDelete == _003F487_003F._003F488_003F("Uŧɿͳѻէ٢ݢࡏ९\u0a65୭\u0c73\u0d63๚\u0f70\u1071ᅷቤ"))
			{
				Button btnDelete2 = btnDelete;
				((UIElement)/*Error near IL_011c: Stack underflow*/).Visibility = (Visibility)/*Error near IL_011c: Stack underflow*/;
			}
			else
			{
				btnDelete.Visibility = Visibility.Collapsed;
			}
			btnAutoDo.Visibility = Visibility.Collapsed;
		}

		private void _003F57_003F()
		{
			//IL_01a3: Incompatible stack heights: 0 vs 1
			//IL_01d7: Incompatible stack heights: 0 vs 3
			//IL_01f1: Incompatible stack heights: 0 vs 1
			//IL_020d: Incompatible stack heights: 0 vs 1
			//IL_0224: Incompatible stack heights: 0 vs 4
			//IL_0242: Incompatible stack heights: 0 vs 1
			CheckExpiredClass checkExpiredClass = new CheckExpiredClass();
			checkExpiredClass._StartDate = SurveyMsg.VersionDate;
			checkExpiredClass._UseDays = SurveyMsg.TestVersionActiveDays;
			if (SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("歠帍灉")) > -1)
			{
				checkExpiredClass._UseDays = SurveyMsg.VersionActiveDays;
			}
			btnNav.Visibility = Visibility.Hidden;
			StkTools.Visibility = Visibility.Hidden;
			List<SurveyDetail> list = new List<SurveyDetail>();
			list = new SurveyDetailDal().GetDetails(_003F487_003F._003F488_003F("Mũɧ\u034aѢկ٤"));
			if (list.Count > 0)
			{
				SurveyDetail surveyDetail = list[0];
				if (((SurveyDetail)/*Error near IL_007a: Stack underflow*/).CODE_TEXT == SurveyMsg.MsgProjectName)
				{
					string text = _003F487_003F._003F488_003F("");
					if (SurveyMsg.IsCheckLicnese != _003F487_003F._003F488_003F("]Šɑ\u0379ѵլ٥\u0741\u0865२\u0a64୬౻\u0d62๙ལၥᅯቱ፤"))
					{
						_003F487_003F._003F488_003F("");
						_003F487_003F._003F488_003F("");
						string _003F292_003F = _003F487_003F._003F488_003F("");
						string _003F293_003F = _003F487_003F._003F488_003F("");
						string expiredFlag = _003F487_003F._003F488_003F("");
						string _003F294_003F = _003F487_003F._003F488_003F("");
						string _003F295_003F = _003F487_003F._003F488_003F("");
						string _003F296_003F = _003F487_003F._003F488_003F("");
						string _003F297_003F = _003F487_003F._003F488_003F("");
						text = ((CheckExpiredClass)/*Error near IL_00f2: Stack underflow*/).ExpiredFlag((string)/*Error near IL_00f2: Stack underflow*/, (string)/*Error near IL_00f2: Stack underflow*/, _003F292_003F, _003F293_003F, expiredFlag, _003F294_003F, _003F295_003F, _003F296_003F, _003F297_003F, 500);
					}
					if (text != _003F487_003F._003F488_003F(""))
					{
						text.Contains(_003F487_003F._003F488_003F("B"));
						if ((int)/*Error near IL_01f6: Stack underflow*/ != 0)
						{
							MessageBox.Show(SurveyMsg.MsgErrorTime, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						}
						else if (SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("浈諗灉")) < 0)
						{
							string msgOverTime = SurveyMsg.MsgOverTime;
							string msgCaption = SurveyMsg.MsgCaption;
							MessageBox.Show((string)/*Error near IL_012e: Stack underflow*/, (string)/*Error near IL_012e: Stack underflow*/, (MessageBoxButton)/*Error near IL_012e: Stack underflow*/, (MessageBoxImage)/*Error near IL_012e: Stack underflow*/);
						}
						else
						{
							MessageBox.Show(SurveyMsg.MsgTestOverTime, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						}
					}
					else
					{
						btnNav.Visibility = Visibility.Visible;
						StkTools.Visibility = Visibility.Visible;
						btnAutoDo.Visibility = Visibility.Collapsed;
						if (File.Exists(SurveyHelper.DebugFlagFile))
						{
							bool flag = SurveyHelper.ShowAutoDo == _003F487_003F._003F488_003F("\\Ŧɢͻъտٽݧࡃ३ਗ਼୰\u0c71\u0d77\u0e64");
							if ((int)/*Error near IL_0247: Stack underflow*/ != 0)
							{
								btnAutoDo.Visibility = Visibility.Visible;
							}
						}
					}
				}
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0100: Incompatible stack heights: 0 vs 1
			//IL_0116: Incompatible stack heights: 0 vs 2
			//IL_012a: Incompatible stack heights: 0 vs 1
			string byCodeText = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Vņɇ\u036cѦդ"));
			if (byCodeText != null)
			{
				bool flag = byCodeText == _003F487_003F._003F488_003F("");
				if ((int)/*Error near IL_0105: Stack underflow*/ == 0)
				{
					string byCodeText2 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("^ŹɹͼѬձ\u064e\u0742ࡇॡ\u0a64୫౯"));
					string byCodeText3 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Xſɻ;Ѣտ\u064c\u0740ࡆ६\u0a65"));
					string byCodeText4 = oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("\\Ũɳ\u0366ѬՓ٣ݥ"));
					if (byCodeText2 != _003F487_003F._003F488_003F(""))
					{
						int length = byCodeText2.Length - SurveyMsg.SurveyIDEnd.Length;
						SurveyHelper.SurveyCity = ((string)/*Error near IL_00a2: Stack underflow*/).Substring((int)/*Error near IL_00a2: Stack underflow*/, length);
						SurveyMsg.SurveyIDBegin = byCodeText2;
						SurveyMsg.SurveyIDEnd = byCodeText3;
						SurveyHelper.SurveyStart = SurveyHelper.SurveyCodePage;
						if (byCodeText4 == _003F487_003F._003F488_003F("0"))
						{
							_003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64");
							SurveyHelper.IsTouch = (string)/*Error near IL_00d7: Stack underflow*/;
						}
						else
						{
							SurveyHelper.IsTouch = _003F487_003F._003F488_003F("Dſɟ\u0365Ѽիٯݙ\u0863॥੯ୱ\u0c64");
						}
					}
					new MainWindow().Show();
					Close();
					return;
				}
			}
			MessageBox.Show(SurveyMsg.MsgNeedConfig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Close();
			Application.Current.Shutdown();
		}

		private void _003F59_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new SurveyRange().Show();
			Close();
		}

		private void _003F60_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new SurveyCloud().Show();
			Close();
		}

		private void _003F61_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new SurveyDelete().Show();
			Close();
		}

		private void _003F62_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new AutoDo().Show();
			Close();
		}

		private void _003F47_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void _003F48_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void _003F49_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			MessageBox.Show(SurveyMsg.MsgRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u000fŘɭ\u036eѥԵ\u0659ݸ\u0868ॾਭ୶౻ൾ\u0e62\u0f7eၾᅪበ፹ᐣᕨᙹ\u177dᡩ\u1975\u1a72ᬫ\u1c7c\u1d62ṯὭ"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0018:
			goto IL_000b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((CStart)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				LayoutRoot = (Grid)_003F350_003F;
				break;
			case 3:
				loginArea = (Grid)_003F350_003F;
				break;
			case 4:
				ImgLogo = (Image)_003F350_003F;
				ImgLogo.MouseUp += _003F47_003F;
				break;
			case 5:
				ImgLogo2 = (Image)_003F350_003F;
				ImgLogo2.MouseUp += _003F48_003F;
				break;
			case 6:
				txtTitle = (TextBlock)_003F350_003F;
				break;
			case 7:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			case 8:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			case 9:
				StkTools = (StackPanel)_003F350_003F;
				break;
			case 10:
				btnAutoDo = (Button)_003F350_003F;
				btnAutoDo.Click += _003F62_003F;
				break;
			case 11:
				btnDelete = (Button)_003F350_003F;
				btnDelete.Click += _003F61_003F;
				break;
			case 12:
				btnUpload = (Button)_003F350_003F;
				btnUpload.Click += _003F60_003F;
				break;
			case 13:
				btnConfig = (Button)_003F350_003F;
				btnConfig.Click += _003F59_003F;
				break;
			case 14:
				txtVersion = (TextBlock)_003F350_003F;
				txtVersion.MouseUp += _003F49_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0045:
			goto IL_004f;
		}
	}
}
