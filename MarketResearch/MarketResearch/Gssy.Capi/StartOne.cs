using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Control;
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

namespace Gssy.Capi
{
	public class StartOne : Window, IComponentConnector
	{
		private bool IsFirstActive = true;

		private string MySurveyId;

		private string CurPageId;

		private string CityCode;

		private NavBase MyNav = new NavBase();

		private QFill oQuestion = new QFill();

		private SurveyBiz oSurvey = new SurveyBiz();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private CheckExpiredClass oCK = new CheckExpiredClass();

		private string KeyFile = _003F487_003F._003F488_003F("");

		private int MyStatus;

		private int SurveyId_Length;

		private int SurveyId_City_Length;

		private int SurveyId_Number_Length;

		private RandomBiz oRandom = new RandomBiz();

		private bool IsRandomOK;

		private string strRarSource = Environment.CurrentDirectory + _003F487_003F._003F488_003F("Tňɳͱєնٶݝ");

		private string strRarOutputFolder = Environment.CurrentDirectory + _003F487_003F._003F488_003F("TŒɶ\u0369ѫբ٦ݝ");

		private string strRarFileName = _003F487_003F._003F488_003F("");

		private string strRarPassword = _003F487_003F._003F488_003F("Nśɔ\u035fЫէ٢ݲ\u0868");

		private bool strRarExcludeRootFolder;

		private bool strRarIncludeSubFolder = true;

		internal Grid LayoutRoot;

		internal Grid loginArea;

		internal Image ImgLogo;

		internal Image ImgLogo2;

		internal TextBlock txtTitle;

		internal UCProgress UCLoading;

		internal TextBlock txtMsg;

		internal Button btnExit;

		internal TextBlock txtVersion;

		private bool _contentLoaded;

		public StartOne()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata
			{
				DefaultValue = (object)5
			});
		}

		private void _003F38_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_00b6: Incompatible stack heights: 0 vs 1
			//IL_00c6: Incompatible stack heights: 0 vs 2
			//IL_00d6: Incompatible stack heights: 0 vs 2
			SurveyMsg.StartOne = _003F487_003F._003F488_003F("^Ÿɪ\u0378ѽՇ٩ݣ\u085a॰\u0a71୷\u0c64");
			SurveyMsg.VersionID = oSurveyConfigBiz.GetByCodeTextRead(_003F487_003F._003F488_003F("_ŭɵ\u0375Ѭի٭\u074bࡅ"));
			txtTitle.Text = SurveyMsg.MsgProjectName;
			txtVersion.Text = SurveyMsg.VersionText + SurveyMsg.VersionID;
			if (IsFirstActive)
			{
				IsFirstActive = false;
				_003F40_003F();
				if ((int)/*Error near IL_00bb: Stack underflow*/ == 0)
				{
					string msgNoKeyFile = SurveyMsg.MsgNoKeyFile;
					((StartOne)/*Error near IL_0068: Stack underflow*/)._003F39_003F((string)/*Error near IL_0068: Stack underflow*/);
				}
				else
				{
					MySurveyId = _003F41_003F(KeyFile);
					if (MySurveyId == _003F487_003F._003F488_003F(""))
					{
						string msgErrorKeyFile = SurveyMsg.MsgErrorKeyFile;
						((StartOne)/*Error near IL_009a: Stack underflow*/)._003F39_003F((string)/*Error near IL_009a: Stack underflow*/);
					}
					else
					{
						SurveyHelper.SurveyFirstPage = _003F487_003F._003F488_003F("[œɇ\u0357ѐՌ\u064c\u0744");
						SurveyHelper.SurveyID = MySurveyId;
						_003F42_003F(MySurveyId);
					}
				}
			}
		}

		private void _003F39_003F(string _003F351_003F)
		{
			txtMsg.Text = _003F351_003F;
			UCLoading.Visibility = Visibility.Collapsed;
			btnExit.Visibility = Visibility.Visible;
		}

		private bool _003F40_003F()
		{
			KeyFile = oCK.SearchKeyFileOfStartOne(strRarSource, _003F487_003F._003F488_003F("*Ũɧ\u0378"));
			return KeyFile != _003F487_003F._003F488_003F("");
		}

		private string _003F41_003F(string _003F352_003F)
		{
			return oCK.GetIDFromKeyFile(KeyFile, SurveyMsg.ProjectId, SurveyMsg.SurveyId_Length);
		}

		private void _003F42_003F(string _003F353_003F)
		{
			//IL_0091: Incompatible stack heights: 0 vs 1
			//IL_00c6: Incompatible stack heights: 0 vs 2
			//IL_00eb: Incompatible stack heights: 0 vs 2
			//IL_0100: Incompatible stack heights: 0 vs 1
			if (oSurvey.ExistSurvey(_003F353_003F))
			{
				oSurvey.GetBySurveyId(MySurveyId);
				if (oSurvey.MySurvey.IS_FINISH != 1)
				{
					int iS_FINISH = oSurvey.MySurvey.IS_FINISH;
					if (/*Error near IL_00cb: Stack underflow*/ != /*Error near IL_00cb: Stack underflow*/)
					{
						if (_003F44_003F())
						{
							new MainWindow().Show();
							((Window)/*Error near IL_0105: Stack underflow*/).Close();
						}
						return;
					}
				}
				if (_003F45_003F(MySurveyId))
				{
					string.Format(SurveyMsg.MsgFinishAndUploaded, MySurveyId);
					((StartOne)/*Error near IL_0059: Stack underflow*/)._003F39_003F((string)/*Error near IL_0059: Stack underflow*/);
				}
				else
				{
					_003F46_003F();
				}
			}
			else
			{
				txtMsg.Text = SurveyMsg.MsgNewSurvey;
				_003F43_003F();
				if ((int)/*Error near IL_0096: Stack underflow*/ != 0)
				{
					new MainWindow().Show();
					Close();
				}
			}
		}

		private bool _003F43_003F()
		{
			//IL_0189: Incompatible stack heights: 0 vs 2
			//IL_019a: Incompatible stack heights: 0 vs 2
			SurveyId_City_Length = 1;
			SurveyHelper.SurveyID = MySurveyId;
			SurveyHelper.SurveyCity = MySurveyId.Substring(0, SurveyId_City_Length);
			SurveyHelper.SurveyStart = _003F487_003F._003F488_003F("[œɇ\u0357ѐՌ\u064c\u0744");
			IsRandomOK = oRandom.RandomSurveyMain(MySurveyId);
			string versionID = SurveyMsg.VersionID;
			string surveyCity = SurveyHelper.SurveyCity;
			string surveyExtend = SurveyHelper.SurveyExtend1;
			string projectId = SurveyMsg.ProjectId;
			string clientId = SurveyMsg.ClientId;
			oSurvey.AddSurvey(MySurveyId, versionID, surveyCity, projectId, clientId, surveyExtend);
			oQuestion.QuestionName = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");
			oQuestion.FillText = MySurveyId;
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			oSurvey.SaveOneAnswer(MySurveyId, SurveyHelper.SurveySequence, _003F487_003F._003F488_003F("GŊɖ\u0358"), surveyCity);
			oSurvey.SaveOneAnswer(MySurveyId, SurveyHelper.SurveySequence, _003F487_003F._003F488_003F("WŅɚ\u0347Ѭզ٤"), oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Vņɇ\u036cѦդ")));
			if (!IsRandomOK)
			{
				_003F39_003F(SurveyMsg.MsgErrorSysSlow);
				RandomBiz oRandom2 = oRandom;
				string mySurveyId2 = MySurveyId;
				((RandomBiz)/*Error near IL_0132: Stack underflow*/).DeleteRandom((string)/*Error near IL_0132: Stack underflow*/);
				Thread.Sleep(1000);
				return false;
			}
			if (oSurvey.GetCityCode(MySurveyId) != null)
			{
				SurveyBiz oSurvey2 = oSurvey;
				string mySurveyId = ((StartOne)/*Error near IL_0159: Stack underflow*/).MySurveyId;
				if (((SurveyBiz)/*Error near IL_015e: Stack underflow*/).ExistSurvey(mySurveyId))
				{
					return true;
				}
			}
			_003F39_003F(string.Format(SurveyMsg.MsgErrorCreateSurvey, MySurveyId));
			return false;
		}

		private bool _003F44_003F()
		{
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			MyNav.LoadPage(MySurveyId, roadMapVersion);
			SurveyHelper.SurveyID = MySurveyId;
			SurveyHelper.SurveyCity = MyNav.Survey.CITY_ID;
			SurveyHelper.CircleACount = MyNav.Survey.CIRCLE_A_COUNT;
			SurveyHelper.CircleACurrent = MyNav.Survey.CIRCLE_A_CURRENT;
			SurveyHelper.CircleBCount = MyNav.Survey.CIRCLE_B_COUNT;
			SurveyHelper.CircleBCurrent = MyNav.Survey.CIRCLE_B_CURRENT;
			oSurvey.UpdateSurveyLastTime(MySurveyId);
			SurveyHelper.SurveyStart = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.SurveySequence = MyNav.Survey.SEQUENCE_ID;
			SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavLoad = 1;
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			SurveyHelper.NavGoBackTimes = 0;
			return true;
		}

		private bool _003F45_003F(string _003F353_003F)
		{
			return oSurvey.CheckUploadSync(_003F353_003F);
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Close();
			Application.Current.Shutdown();
		}

		private void _003F46_003F()
		{
			string arg = _003F487_003F._003F488_003F("3Ĳȱ");
			bool flag = true;
			alioss alioss = new alioss(_003F267_003F: (SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("歠帍灉")) <= -1) ? true : false, _003F266_003F: SurveyMsg.OSSRegion, _003F268_003F: SurveyMsg.ProjectId);
			if (!alioss.CreateOss())
			{
				txtMsg.Text = alioss.OutMessage;
			}
			else
			{
				SurveyConfigBiz surveyConfigBiz = new SurveyConfigBiz();
				string byCodeText = surveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("[Žɠ\u0364ѫխ\u065bݢ\u0877॰\u0a61୭ౡ\u0d64"));
				string newUploadSequence = alioss.GetNewUploadSequence(byCodeText);
				if (byCodeText != newUploadSequence)
				{
					surveyConfigBiz.Save(_003F487_003F._003F488_003F("[Žɠ\u0364ѫխ\u065bݢ\u0877॰\u0a61୭ౡ\u0d64"), newUploadSequence);
				}
				strRarFileName = string.Format(_003F487_003F._003F488_003F("`Īɤ\u0347Ѭԧ٨\u074b\u0868ठਫଢ଼\u0c42൪\u0e69ན၃ᅂቤ፥ᑴᕵᙸᜪᡱᥣ\u1a73"), byCodeText, arg, DateTime.Now);
				RarFile rarFile = new RarFile();
				if (!rarFile.Compress(strRarSource + _003F487_003F._003F488_003F("/Īɧ\u0363ѵ"), strRarOutputFolder, strRarFileName, strRarPassword, true, true))
				{
					txtMsg.Text = SurveyMsg.MsgNoDataFile;
					btnExit.IsEnabled = true;
				}
				else
				{
					Thread.Sleep(1000);
					if (alioss.UploadToOss(strRarOutputFolder, strRarFileName))
					{
						surveyConfigBiz.Save(_003F487_003F._003F488_003F("BŬɿͿџչ٤ݨ\u0867ॡ\u0a42୪౮\u0d64"), strRarFileName);
						oSurvey.AddSurvyeSync(MySurveyId, SurveyMsg.MsgUploadSingleVersion, 1);
						string _003F62_003F = strRarOutputFolder + strRarFileName;
						string _003F57_003F = strRarOutputFolder + strRarFileName.Replace(_003F487_003F._003F488_003F("*űɣͳ"), _003F487_003F._003F488_003F("]")).Substring(byCodeText.Length + 1);
						bool num = rarFile.Extract(_003F62_003F, strRarOutputFolder, _003F57_003F, strRarPassword);
						Thread.Sleep(1000);
						if (num)
						{
							string searchPattern = _003F487_003F._003F488_003F("UįȪ\u0367ѣյ");
							foreach (FileInfo item in new DirectoryInfo(Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŉɰͰѓշٵ")).EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly))
							{
								item.Delete();
							}
						}
						txtMsg.Text = SurveyMsg.MsgEndSurveyInfo;
					}
					else
					{
						txtMsg.Text = alioss.OutMessage;
					}
				}
			}
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\rŦɓ\u036cѧԳ\u065fݺ\u086a॰ਣ୴౹\u0d78\u0e64\u0f7cၼᅴቾ፻ᐡᕾᙸᝪ\u1878\u197d\u1a67᭩ᱣᴫṼὢ\u206fⅭ"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0018:
			goto IL_000b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _003F50_003F(Type _003F354_003F, string _003F355_003F)
		{
			return Delegate.CreateDelegate(_003F354_003F, this, _003F355_003F);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((StartOne)_003F350_003F).Loaded += _003F24_003F;
				((StartOne)_003F350_003F).Activated += _003F38_003F;
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
				UCLoading = (UCProgress)_003F350_003F;
				break;
			case 8:
				txtMsg = (TextBlock)_003F350_003F;
				break;
			case 9:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			case 10:
				txtVersion = (TextBlock)_003F350_003F;
				txtVersion.MouseUp += _003F49_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0035:
			goto IL_003f;
		}
	}
}
