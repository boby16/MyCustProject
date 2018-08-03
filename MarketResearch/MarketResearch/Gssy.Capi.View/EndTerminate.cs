using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Gssy.Capi.View
{
	public class EndTerminate : Page, IComponentConnector
	{
		private string MySurveyId;

		private int EndType;

		private SurveyBiz oSurvey = new SurveyBiz();

		private string strRarSource = Environment.CurrentDirectory + _003F487_003F._003F488_003F("Tňɳͱєնٶݝ");

		private string strRarOutputFolder = Environment.CurrentDirectory + _003F487_003F._003F488_003F("TŒɶ\u0369ѫբ٦ݝ");

		private string strRarFileName = _003F487_003F._003F488_003F("");

		private string strRarPassword = _003F487_003F._003F488_003F("Nśɔ\u035fЫէ٢ݲ\u0868");

		private bool strRarExcludeRootFolder;

		private bool strRarIncludeSubFolder = true;

		internal TextBlock labTitle;

		internal TextBlock labCase;

		internal TextBlock txtMsg;

		internal TextBlock txtSurvey;

		internal Button btnExit;

		private bool _contentLoaded;

		public EndTerminate()
		{
			InitializeComponent();
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Application.Current.Shutdown();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00f6: Incompatible stack heights: 0 vs 1
			//IL_0107: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			txtSurvey.Text = MySurveyId;
			oSurvey.RecordFileName = SurveyHelper.RecordFileName;
			int surveySequence = SurveyHelper.SurveySequence;
			txtMsg.Text = SurveyMsg.MsgEndTerminate;
			labCase.Text = SurveyMsg.MsgFrmCurrentID + SurveyHelper.SurveyID;
			EndType = 2;
			_003F169_003F();
			if (SurveyMsg.StartOne == _003F487_003F._003F488_003F("^Ÿɪ\u0378ѽՇ٩ݣ\u085a॰\u0a71୷\u0c64"))
			{
				_003F46_003F();
			}
			if (SurveyHelper.AutoDo)
			{
				bool flag = SurveyHelper.Survey_Status == _003F487_003F._003F488_003F("EŤɪ\u0360ѧխ");
				if ((int)/*Error near IL_00fb: Stack underflow*/ != 0)
				{
					int num = SurveyHelper.AutoDo_Filled + 1;
					SurveyHelper.AutoDo_Filled = (int)/*Error near IL_0096: Stack underflow*/;
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
				_003F63_003F();
			}
		}

		private void _003F169_003F()
		{
			//IL_00ac: Incompatible stack heights: 0 vs 2
			//IL_00c2: Incompatible stack heights: 0 vs 2
			oSurvey.CloseSurvey(MySurveyId, EndType);
			Thread.Sleep(1000);
			SurveytoXml surveytoXml = new SurveytoXml();
			string currentDirectory = Environment.CurrentDirectory;
			surveytoXml.SaveSurveyAnswer(MySurveyId, currentDirectory, null, true);
			if (!(SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64")))
			{
				string isSaveSequence = SurveyMsg.IsSaveSequence;
				_003F487_003F._003F488_003F("Zšɂͱѹի\u065eݩ\u087aॿ੬୦\u0c64\u0d63๚\u0f70\u1071ᅷቤ");
				if (!((string)/*Error near IL_005b: Stack underflow*/ == (string)/*Error near IL_005b: Stack underflow*/))
				{
					goto IL_0070;
				}
			}
			surveytoXml.SaveSurveySequence(MySurveyId, currentDirectory, null, true);
			goto IL_0070;
			IL_0070:
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ"))
			{
				string mySurveyId = ((EndTerminate)/*Error near IL_008e: Stack underflow*/).MySurveyId;
				((SurveytoXml)/*Error near IL_00ca: Stack underflow*/).SaveSurveyAttach(mySurveyId, currentDirectory, (List<SurveyAttach>)null, true);
			}
		}

		private void _003F46_003F()
		{
			string arg = _003F487_003F._003F488_003F(":Ļȸ");
			bool flag = true;
			alioss alioss = new alioss(_003F267_003F: (SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("浈諗灉")) >= 0 || SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("漗砸灉")) >= 0 || SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("@Ŧɯ\u036e")) >= 0) ? true : false, _003F266_003F: SurveyMsg.OSSRegion, _003F268_003F: SurveyMsg.ProjectId);
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
					Thread.Sleep(500);
					if (alioss.UploadToOss(strRarOutputFolder, strRarFileName))
					{
						surveyConfigBiz.Save(_003F487_003F._003F488_003F("BŬɿͿџչ٤ݨ\u0867ॡ\u0a42୪౮\u0d64"), strRarFileName);
						oSurvey.AddSurvyeSync(SurveyHelper.SurveyID, _003F487_003F._003F488_003F("卟铧兿煏挪䬏䤤\u074cࡑ\u0952"), 1);
						string _003F62_003F = strRarOutputFolder + strRarFileName;
						string _003F57_003F = strRarOutputFolder + strRarFileName.Replace(_003F487_003F._003F488_003F("*űɣͳ"), _003F487_003F._003F488_003F("]")).Substring(byCodeText.Length + 1);
						if (rarFile.Extract(_003F62_003F, strRarOutputFolder, _003F57_003F, strRarPassword))
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

		private void _003F63_003F()
		{
			string surveyStart = SurveyHelper.SurveyStart;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			NavBase navBase = new NavBase();
			navBase.StartPage(surveyStart, roadMapVersion);
			string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), navBase.RoadMap.FORM_NAME);
			base.NavigationService.Navigate(new Uri(uriString));
			if (SurveyHelper.NavLoad == 0)
			{
				SurveyHelper.SurveySequence = 1;
			}
			SurveyHelper.NavCurPage = navBase.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = navBase.RoadMap.FORM_NAME;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (_contentLoaded)
			{
				return;
			}
			goto IL_001b;
			IL_001b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0004ŭɚ\u035bўԈ٦\u0745ࡓ\u094bਚ\u0b43\u0c70൳\u0e6d\u0f73ၵᅿቷ፬ᐸᕠᙼ\u1771ᡤ\u193d\u1a74\u1b7eᱫᵺṨ\u1f7e\u2066Ⅳ≧⍩⑳╣☫❼⡢⥯⩭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0016:
			goto IL_001b;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int _003F349_003F, object _003F350_003F)
		{
			switch (_003F349_003F)
			{
			case 1:
				((EndTerminate)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				labTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				labCase = (TextBlock)_003F350_003F;
				break;
			case 4:
				txtMsg = (TextBlock)_003F350_003F;
				break;
			case 5:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 6:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
