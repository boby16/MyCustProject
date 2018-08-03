using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.DAL;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Gssy.Capi.View
{
	public class SurveyDelete : Window, IComponentConnector
	{
		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private SurveyImport oSurveyImport = new SurveyImport();

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		internal ComboBox cmbCITY;

		internal TextBox txtSurveyId;

		internal PasswordBox passwordBox1;

		internal TextBlock PasswordMsg;

		internal Button btnExit;

		internal Button btnSave;

		private bool _contentLoaded;

		public SurveyDelete()
		{
			InitializeComponent();
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			new CStart().Show();
			Close();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_005a: Incompatible stack heights: 0 vs 2
			//IL_0073: Incompatible stack heights: 0 vs 2
			//IL_008c: Incompatible stack heights: 0 vs 2
			if (!(SurveyMsg.SurveyRangePsw == SurveyMsg.SurveyRangeDemoPsw))
			{
				string versionID = SurveyMsg.VersionID;
				_003F487_003F._003F488_003F("浈諗灉");
				if (((string)/*Error near IL_0019: Stack underflow*/).IndexOf((string)/*Error near IL_0019: Stack underflow*/) < 0)
				{
					string versionID2 = SurveyMsg.VersionID;
					_003F487_003F._003F488_003F("漗砸灉");
					if (((string)/*Error near IL_0024: Stack underflow*/).IndexOf((string)/*Error near IL_0024: Stack underflow*/) < 0)
					{
						string versionID3 = SurveyMsg.VersionID;
						_003F487_003F._003F488_003F("@Ŧɯ\u036e");
						if (((string)/*Error near IL_002f: Stack underflow*/).IndexOf((string)/*Error near IL_002f: Stack underflow*/) < 0)
						{
							goto IL_009b;
						}
					}
				}
			}
			PasswordMsg.Visibility = Visibility.Visible;
			goto IL_009b;
			IL_009b:
			txtSurveyId.MaxLength = SurveyMsg.SurveyId_Length;
			txtSurveyId.Focus();
		}

		private void _003F211_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_014a: Incompatible stack heights: 0 vs 3
			//IL_0163: Incompatible stack heights: 0 vs 2
			//IL_0178: Incompatible stack heights: 0 vs 1
			//IL_0197: Incompatible stack heights: 0 vs 2
			//IL_01b0: Incompatible stack heights: 0 vs 2
			//IL_01ce: Incompatible stack heights: 0 vs 2
			//IL_01d9: Incompatible stack heights: 0 vs 1
			string text = txtSurveyId.Text;
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgDeleteMissSurveyId = SurveyMsg.MsgDeleteMissSurveyId;
				string msgCaption2 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0028: Stack underflow*/, (string)/*Error near IL_0028: Stack underflow*/, (MessageBoxButton)/*Error near IL_0028: Stack underflow*/, MessageBoxImage.Hand);
				txtSurveyId.Focus();
				return;
			}
			if (text.Length != SurveyMsg.SurveyId_Length)
			{
				string msgRangeLength = SurveyMsg.MsgRangeLength;
				SurveyMsg.SurveyId_Length.ToString();
				MessageBox.Show(string.Format((string)/*Error near IL_004b: Stack underflow*/, (object)/*Error near IL_004b: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				txtSurveyId.Focus();
				return;
			}
			if (!oSurveyMainDal.ExistsBySurveyID(text))
			{
				string.Format(SurveyMsg.MsgDeleteNotExist, text);
				MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_0084: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Asterisk);
				txtSurveyId.Focus();
				return;
			}
			string password = passwordBox1.Password;
			if (SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("漗砸灉")) < 0)
			{
				SurveyMsg.VersionID.IndexOf(_003F487_003F._003F488_003F("浈諗灉"));
				if (/*Error near IL_019c: Stack underflow*/ < /*Error near IL_019c: Stack underflow*/)
				{
					string versionID = SurveyMsg.VersionID;
					_003F487_003F._003F488_003F("@Ŧɯ\u036e");
					if (((string)/*Error near IL_00c2: Stack underflow*/).IndexOf((string)/*Error near IL_00c2: Stack underflow*/) < 0)
					{
						if (!(password == _003F487_003F._003F488_003F("")))
						{
							string surveyRangePsw = SurveyMsg.SurveyRangePsw;
							if (!((string)/*Error near IL_010d: Stack underflow*/ != surveyRangePsw))
							{
								goto IL_01e5;
							}
						}
						MessageBox.Show(SurveyMsg.MsgRangeMissPsw, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
						passwordBox1.Focus();
						return;
					}
				}
			}
			if (password != SurveyMsg.SurveyRangeDemoPsw)
			{
				string msgRangeMissPsw = SurveyMsg.MsgRangeMissPsw;
				string msgCaption3 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_00e0: Stack underflow*/, (string)/*Error near IL_00e0: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				passwordBox1.Focus();
				return;
			}
			goto IL_01e5;
			IL_01e5:
			string arg = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŉɰͰѳշٵ");
			string arg2 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("YŀɢͶѠ");
			string _003F404_003F = string.Format(_003F487_003F._003F488_003F("vļɶ\u0356њճضݻࡄप੧\u0b63\u0c75"), arg, text);
			string _003F405_003F = string.Format(_003F487_003F._003F488_003F("]ĕəͿѠժ\u065bܮ\u0824।\u0a65\u0b62ష\u0d54๕༺\u1072ᅱቋ\u135bᑚᔼᙽᝢᠣ\u197e\u1a7f᭶᱕\u1d5aṳἵ⁻⅄∪⍧④╵"), arg2, DateTime.Now, text);
			_003F213_003F(_003F404_003F, _003F405_003F);
			_003F404_003F = string.Format(_003F487_003F._003F488_003F("vļɶ\u0356њճضݻࡖप੧\u0b63\u0c75"), arg, text);
			_003F405_003F = string.Format(_003F487_003F._003F488_003F("]ĕəͿѠժ\u065bܮ\u0824।\u0a65\u0b62ష\u0d54๕༺\u1072ᅱቋ\u135bᑚᔼᙽᝢᠣ\u197e\u1a7f᭶᱕\u1d5aṳἵ⁻⅖∪⍧④╵"), arg2, DateTime.Now, text);
			_003F213_003F(_003F404_003F, _003F405_003F);
			_003F404_003F = string.Format(_003F487_003F._003F488_003F("vļɶ\u0356њճضݻࡍप੧\u0b63\u0c75"), arg, text);
			_003F405_003F = string.Format(_003F487_003F._003F488_003F("]ĕəͿѠժ\u065bܮ\u0824।\u0a65\u0b62ష\u0d54๕༺\u1072ᅱቋ\u135bᑚᔼᙽᝢᠣ\u197e\u1a7f᭶᱕\u1d5aṳἵ⁻⅍∪⍧④╵"), arg2, DateTime.Now, text);
			_003F213_003F(_003F404_003F, _003F405_003F);
			oSurveyImport.DeleteOneSurvey(text, _003F487_003F._003F488_003F(""));
			MessageBox.Show(string.Format(SurveyMsg.MsgDeleteOk, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void _003F213_003F(string _003F404_003F, string _003F405_003F)
		{
			if (File.Exists(_003F404_003F))
			{
				File.Copy(_003F404_003F, _003F405_003F);
				File.Delete(_003F404_003F);
			}
		}

		private void _003F90_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.HideInputPanel();
			}
		}

		private void _003F91_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.ShowInputPanel();
			}
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0004ŭɚ\u035bўԈ٦\u0745ࡓ\u094bਚ\u0b43\u0c70൳\u0e6d\u0f73ၵᅿቷ፬ᐸᕠᙼ\u1771ᡤ\u193d\u1a62᭥\u1c7d\u1d78Ṩή\u206fⅯ≥⍭⑳╣☫❼⡢⥯⩭"), UriKind.Relative);
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
				((SurveyDelete)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				cmbCITY = (ComboBox)_003F350_003F;
				break;
			case 3:
				txtSurveyId = (TextBox)_003F350_003F;
				txtSurveyId.GotFocus += _003F91_003F;
				txtSurveyId.LostFocus += _003F90_003F;
				break;
			case 4:
				passwordBox1 = (PasswordBox)_003F350_003F;
				passwordBox1.GotFocus += _003F91_003F;
				passwordBox1.LostFocus += _003F90_003F;
				break;
			case 5:
				PasswordMsg = (TextBlock)_003F350_003F;
				break;
			case 6:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			case 7:
				btnSave = (Button)_003F350_003F;
				btnSave.Click += _003F211_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
