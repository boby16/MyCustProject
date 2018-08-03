using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class SurveyCode : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private string CityCode;

		private Brush brushOK;

		private NavBase MyNav = new NavBase();

		private QFill oQuestion = new QFill();

		private SurveyBiz oSurvey = new SurveyBiz();

		private SurveyConfigBiz oSurveyConfigBiz = new SurveyConfigBiz();

		private UDPX oFunc = new UDPX();

		private PageNav oPageNav = new PageNav();

		private int MyStatus;

		private int SurveyId_Length;

		private int SurveyId_City_Length;

		private int SurveyId_Number_Length;

		private RandomBiz oRandom = new RandomBiz();

		private bool IsRandomOK;

		private BackgroundWorker bw;

		private string SurveyIdBegin;

		private string SurveyIdEnd;

		private DispatcherTimer timer = new DispatcherTimer();

		internal TextBlock txtVersion;

		internal TextBlock txtQuestionTitle;

		internal Button btnLast;

		internal Button btnBack;

		internal TextBox txtFill;

		internal Button btnOK;

		internal Button btnAuto;

		internal TextBlock txtCITY;

		internal TextBlock txtMsg;

		internal TextBlock txtDate;

		internal TextBlock txtMsgBar;

		internal ProgressBar progressBar1;

		internal Button btnExit;

		internal Button btnNav;

		private bool _contentLoaded;

		public SurveyCode()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0495: Incompatible stack heights: 0 vs 2
			//IL_04a6: Incompatible stack heights: 0 vs 2
			//IL_04c0: Incompatible stack heights: 0 vs 2
			//IL_04dc: Incompatible stack heights: 0 vs 1
			//IL_04ec: Incompatible stack heights: 0 vs 1
			//IL_04fc: Incompatible stack heights: 0 vs 1
			//IL_0511: Incompatible stack heights: 0 vs 1
			//IL_052c: Incompatible stack heights: 0 vs 3
			//IL_0542: Incompatible stack heights: 0 vs 1
			//IL_054e: Incompatible stack heights: 0 vs 2
			//IL_0563: Incompatible stack heights: 0 vs 2
			//IL_0573: Incompatible stack heights: 0 vs 1
			//IL_059c: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			CityCode = SurveyHelper.SurveyCity;
			SurveyId_City_Length = CityCode.Length;
			brushOK = btnNav.Foreground;
			int num = SurveyMsg.VersionID.IndexOf('v');
			string text = _003F487_003F._003F488_003F("");
			if (num > -1)
			{
				UDPX oFunc2 = oFunc;
				string versionID = SurveyMsg.VersionID;
				text = ((UDPX)/*Error near IL_0068: Stack underflow*/).LEFT((string)/*Error near IL_0068: Stack underflow*/, num);
				if (text == _003F487_003F._003F488_003F("歠帍灉"))
				{
					TextBlock txtVersion2 = txtVersion;
					((UIElement)/*Error near IL_0083: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0083: Stack underflow*/;
				}
				else
				{
					SurveyHelper.TestVersion = true;
					if (oFunc.RIGHT(text, 1) == _003F487_003F._003F488_003F("牉"))
					{
						_003F487_003F._003F488_003F("札");
						text = (string)/*Error near IL_00b4: Stack underflow*/ + (string)/*Error near IL_00b4: Stack underflow*/;
					}
					txtVersion.Text = text;
				}
			}
			oQuestion.Init(CurPageId, 0);
			SurveyConfigBiz surveyConfigBiz = new SurveyConfigBiz();
			txtCITY.Text = surveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("KŮɲͼѐզٺݵ"));
			timer.Interval = TimeSpan.FromMilliseconds(1000.0);
			timer.Tick += _003F84_003F;
			timer.Start();
			SurveyId_Number_Length = SurveyMsg.Order_Length;
			SurveyId_Length = SurveyMsg.SurveyId_Length;
			if (SurveyId_Length > 0)
			{
				txtFill.MaxLength = SurveyId_Length;
				if (((SurveyCode)/*Error near IL_0156: Stack underflow*/).SurveyId_Length > 5)
				{
					Button btnLast2 = btnLast;
					((FrameworkElement)/*Error near IL_016a: Stack underflow*/).Width = 350.0;
					txtFill.Width = 350.0;
					btnAuto.Width = 350.0;
				}
			}
			MyStatus = 1;
			txtQuestionTitle.Text = oQuestion.QDefine.QUESTION_TITLE;
			txtFill.Focus();
			txtFill.Text = _003F487_003F._003F488_003F("");
			SurveyIdBegin = surveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("^ŹɹͼѬձ\u064e\u0742ࡇॡ\u0a64୫౯"));
			SurveyIdEnd = surveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Xſɻ;Ѣտ\u064c\u0740ࡆ६\u0a65"));
			if (!(SurveyIdBegin == _003F487_003F._003F488_003F("")))
			{
				string surveyIdBegin2 = SurveyIdBegin;
				if ((int)/*Error near IL_0501: Stack underflow*/ != 0)
				{
					goto IL_027f;
				}
			}
			SurveyIdBegin = CityCode + SurveyMsg.SurveyIDBegin;
			if (SurveyMsg.AllowClearCaseNumber == _003F487_003F._003F488_003F("XŴɻ\u0379Ѣ\u0557ٿݷ\u0870\u0962\u0a4c୯౾൩ๅ\u0f7f\u1064ᅪቢ፴ᑚᕰᙱ\u1777ᡤ"))
			{
				string surveyIdEnd = CityCode + SurveyMsg.SurveyIDEnd;
				((SurveyCode)/*Error near IL_0264: Stack underflow*/).SurveyIdEnd = surveyIdEnd;
			}
			else
			{
				SurveyIdEnd = CityCode + SurveyMsg.SurveyIDBegin;
			}
			goto IL_027f;
			IL_027f:
			SurveyIdBegin = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + SurveyIdBegin, SurveyMsg.SurveyId_Length);
			SurveyIdEnd = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + SurveyIdEnd, SurveyMsg.SurveyId_Length);
			txtMsg.Text = string.Format(SurveyMsg.MsgFrmCodeRange, SurveyIdBegin, SurveyIdEnd);
			string str = _003F195_003F();
			str = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + str, SurveyMsg.SurveyId_Length);
			string a = _003F487_003F._003F488_003F("");
			if (str != _003F487_003F._003F488_003F(""))
			{
				_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ");
				int surveyId_Length = SurveyMsg.SurveyId_Length;
				string b = ((SurveyCode)/*Error near IL_033a: Stack underflow*/)._003F95_003F((string)/*Error near IL_033a: Stack underflow*/, surveyId_Length);
				if ((string)/*Error near IL_033f: Stack underflow*/ != b)
				{
					_003F93_003F(str, SurveyMsg.CITY_Length);
					a = (string)/*Error near IL_0346: Stack underflow*/;
				}
			}
			string b2 = _003F487_003F._003F488_003F("");
			if (SurveyIdBegin != _003F487_003F._003F488_003F(""))
			{
				string surveyIdBegin = ((SurveyCode)/*Error near IL_0371: Stack underflow*/).SurveyIdBegin;
				int cITY_Length = SurveyMsg.CITY_Length;
				b2 = ((SurveyCode)/*Error near IL_037b: Stack underflow*/)._003F93_003F(surveyIdBegin, cITY_Length);
			}
			if (!(a == b2))
			{
				btnLast.Content = string.Format(SurveyMsg.MsgFrmCodePre, SurveyIdBegin);
				btnAuto.Content = string.Format(SurveyMsg.MsgFrmCodeAutoNext, SurveyIdBegin);
			}
			else
			{
				_003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_0390: Stack underflow*/ == (string)/*Error near IL_0390: Stack underflow*/)
				{
					string surveyIdBegin3 = SurveyIdBegin;
					str = (string)/*Error near IL_0396: Stack underflow*/;
					btnLast.Content = string.Format(SurveyMsg.MsgFrmCodePre, str);
					btnAuto.Content = string.Format(SurveyMsg.MsgFrmCodeAutoNext, str);
				}
				else
				{
					btnLast.Content = string.Format(SurveyMsg.MsgFrmCodePre, str);
					str = (Convert.ToInt32(str) + 1).ToString();
					str = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + str, SurveyMsg.SurveyId_Length);
					btnAuto.Content = string.Format(SurveyMsg.MsgFrmCodeAutoNext, str);
				}
			}
			if (SurveyHelper.AutoDo)
			{
				txtFill.Text = SurveyHelper.SurveyID;
				SurveyHelper.Survey_Status = _003F487_003F._003F488_003F((string)/*Error near IL_046b: Stack underflow*/);
				MyStatus = 3;
				_003F58_003F(_003F347_003F, _003F348_003F);
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (btnNav.Foreground != brushOK)
			{
				return;
			}
			goto IL_0016;
			IL_0016:
			if (btnOK.Foreground != brushOK)
			{
				return;
			}
			goto IL_002c;
			IL_002c:
			_003F182_003F();
			return;
			IL_0039:
			goto IL_0016;
			IL_0044:
			goto IL_002c;
		}

		private void _003F182_003F()
		{
			//IL_048e: Incompatible stack heights: 0 vs 3
			//IL_049f: Incompatible stack heights: 0 vs 2
			//IL_04c0: Incompatible stack heights: 0 vs 2
			//IL_04d1: Incompatible stack heights: 0 vs 2
			//IL_04e1: Incompatible stack heights: 0 vs 1
			//IL_04fb: Incompatible stack heights: 0 vs 1
			//IL_0506: Incompatible stack heights: 0 vs 1
			//IL_0520: Incompatible stack heights: 0 vs 1
			//IL_0525: Invalid comparison between Unknown and I4
			//IL_053a: Incompatible stack heights: 0 vs 1
			//IL_053f: Invalid comparison between Unknown and I4
			//IL_054f: Incompatible stack heights: 0 vs 1
			//IL_0564: Incompatible stack heights: 0 vs 1
			//IL_0578: Incompatible stack heights: 0 vs 2
			//IL_05ae: Incompatible stack heights: 0 vs 2
			//IL_05d4: Incompatible stack heights: 0 vs 1
			//IL_0617: Incompatible stack heights: 0 vs 2
			string text = txtFill.Text;
			text = text.Trim();
			oQuestion.FillText = text;
			if (oQuestion.FillText == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill = SurveyMsg.MsgNotFill;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0045: Stack underflow*/, (string)/*Error near IL_0045: Stack underflow*/, (MessageBoxButton)/*Error near IL_0045: Stack underflow*/, MessageBoxImage.Asterisk);
				txtFill.Focus();
				_003F194_003F();
				return;
			}
			oQuestion.FillText = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + oQuestion.FillText, SurveyMsg.SurveyId_Length);
			txtFill.Text = oQuestion.FillText;
			if (MyStatus != 1)
			{
				int myStatus = MyStatus;
				if (/*Error near IL_04a4: Stack underflow*/ != /*Error near IL_04a4: Stack underflow*/)
				{
					goto IL_0292;
				}
			}
			bool flag = true;
			if (oQuestion.FillText.Length != SurveyId_Length)
			{
				MyStatus = 1;
				string msgFrmCodeLen = SurveyMsg.MsgFrmCodeLen;
				MessageBox.Show(string.Format(arg0: ((SurveyCode)/*Error near IL_00d2: Stack underflow*/).SurveyId_Length.ToString(), format: (string)/*Error near IL_00dc: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				txtFill.Focus();
				_003F194_003F();
				return;
			}
			if (oSurvey.ExistSurvey(oQuestion.FillText))
			{
				SurveyBiz oSurvey2 = oSurvey;
				string fillText = ((SurveyCode)/*Error near IL_011d: Stack underflow*/).oQuestion.FillText;
				((SurveyBiz)/*Error near IL_0127: Stack underflow*/).GetBySurveyId(fillText);
				if (oSurvey.MySurvey.IS_FINISH != 1)
				{
					SurveyBiz oSurvey3 = oSurvey;
					if (((SurveyBiz)/*Error near IL_0142: Stack underflow*/).MySurvey.IS_FINISH != 2)
					{
						goto IL_014f;
					}
				}
				flag = false;
			}
			goto IL_014f;
			IL_072a:
			_003F190_003F();
			timer.Stop();
			return;
			IL_014f:
			if (flag)
			{
				QFill oQuestion2 = oQuestion;
				if (((QFill)/*Error near IL_015a: Stack underflow*/).FillText.Substring(0, SurveyId_City_Length) != CityCode)
				{
					((SurveyCode)/*Error near IL_017c: Stack underflow*/).MyStatus = 1;
					MessageBox.Show(string.Format(SurveyMsg.MsgFrmCodeNotSame, txtQuestionTitle.Text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					txtFill.Focus();
					_003F194_003F();
					return;
				}
			}
			if (flag)
			{
				Convert.ToInt32(oQuestion.FillText);
				int num = Convert.ToInt32(SurveyIdBegin);
				if ((int)/*Error near IL_0525: Stack underflow*/ >= num)
				{
					Convert.ToInt32(oQuestion.FillText);
					int num2 = Convert.ToInt32(SurveyIdEnd);
					if ((int)/*Error near IL_053f: Stack underflow*/ <= num2)
					{
						goto IL_0205;
					}
				}
				MyStatus = 1;
				MessageBox.Show(SurveyMsg.MsgIdOutRange, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				txtFill.Focus();
				_003F194_003F();
				return;
			}
			goto IL_0205;
			IL_0205:
			if (MyStatus == 1)
			{
				string fillText2 = oQuestion.FillText;
				((SurveyCode)/*Error near IL_0221: Stack underflow*/).MySurveyId = fillText2;
				MyStatus = 2;
				btnBack.Width = 160.0;
				btnBack.Visibility = Visibility.Visible;
				txtQuestionTitle.Text = SurveyMsg.MsgFrmCodeConfirm;
				txtFill.Text = _003F487_003F._003F488_003F("");
				btnAuto.Visibility = Visibility.Hidden;
				btnLast.Visibility = Visibility.Hidden;
				txtFill.Focus();
				return;
			}
			goto IL_0292;
			IL_0292:
			if (MyStatus == 2)
			{
				string fillText3 = oQuestion.FillText;
				string mySurveyId = MySurveyId;
				if ((string)/*Error near IL_02a9: Stack underflow*/ != mySurveyId)
				{
					string msgIdNotSame = SurveyMsg.MsgIdNotSame;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_02b6: Stack underflow*/, (string)/*Error near IL_02b6: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
					txtFill.Focus();
					return;
				}
			}
			_003F188_003F();
			_003F189_003F(20.0);
			MySurveyId = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + MySurveyId, SurveyMsg.SurveyId_Length);
			SurveyHelper.SurveyID = MySurveyId;
			if (oSurvey.ExistSurvey(MySurveyId))
			{
				oSurvey.GetBySurveyId(MySurveyId);
				if (oSurvey.MySurvey.IS_FINISH != 1)
				{
					int iS_FINISH = oSurvey.MySurvey.IS_FINISH;
					if (/*Error near IL_05b3: Stack underflow*/ != /*Error near IL_05b3: Stack underflow*/)
					{
						MessageBoxResult messageBoxResult = MessageBoxResult.Yes;
						if (!SurveyHelper.AutoDo)
						{
							messageBoxResult = MessageBox.Show(SurveyMsg.MsgNotFinished, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
						}
						if (!messageBoxResult.Equals(MessageBoxResult.Yes))
						{
							txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
							txtFill.Text = _003F487_003F._003F488_003F("");
							txtFill.Focus();
							MyStatus = 1;
							txtMsgBar.Text = _003F487_003F._003F488_003F("");
							_003F189_003F(1.0);
							_003F194_003F();
							return;
						}
						SurveyHelper.Survey_Status = _003F487_003F._003F488_003F("EŤɪ\u0360ѧխ");
						TextBox txtFill2 = txtFill;
						string text2 = ((TextBox)/*Error near IL_044e: Stack underflow*/).Text;
						((SurveyCode)/*Error near IL_0453: Stack underflow*/)._003F196_003F(text2);
						_003F189_003F(50.0);
						SurveyHelper.NavLoad = 1;
						NavLoadPage();
						goto IL_072a;
					}
				}
				if (!MessageBox.Show(SurveyMsg.MsgHaveFinished, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk).Equals(MessageBoxResult.Yes))
				{
					txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
					txtFill.Text = _003F487_003F._003F488_003F("");
					txtFill.Focus();
					MyStatus = 1;
					txtMsgBar.Text = _003F487_003F._003F488_003F("");
					_003F189_003F(1.0);
					_003F194_003F();
					return;
				}
				_003F196_003F(txtFill.Text);
				((SurveyCode)/*Error near IL_036b: Stack underflow*/).txtMsgBar.Text = SurveyMsg.MsgFrmQueryID;
				_003F189_003F(60.0);
				string uriString = _003F487_003F._003F488_003F("\\Ŋɉ\u0342ВԈ؉\u0744ࡔ\u0953\u0a4e\u0b48\u0c43ൾ\u0e6a\u0f74\u1073ᅵሠጵᐴᔻᘹᝃ\u187d\u1976\u1a65\u1b3e᱃ᵺṼύ\u2069ⅲ≛⍼⑭╵♿✫⡼⥢⩯⭭");
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
				SurveyHelper.NavCurPage = _003F487_003F._003F488_003F("Xſɻ;Ѣտ\u0654ݱ\u0866॰\u0a78");
			}
			else
			{
				_003F196_003F(txtFill.Text);
				_003F193_003F();
				txtMsgBar.Text = SurveyMsg.MsgNewSurvey;
				_003F189_003F(30.0);
				bw = new BackgroundWorker();
				bw.DoWork += _003F185_003F;
				bw.ProgressChanged += _003F186_003F;
				bw.RunWorkerCompleted += _003F187_003F;
				bw.WorkerReportsProgress = true;
				bw.RunWorkerAsync();
				_003F189_003F(50.0);
			}
			goto IL_072a;
		}

		private void _003F81_003F()
		{
			//IL_00c4: Incompatible stack heights: 0 vs 2
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			MyNav.NextPage(MySurveyId, surveySequence, CurPageId, roadMapVersion);
			string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), MyNav.RoadMap.FORM_NAME);
			if (MyNav.RoadMap.FORM_NAME.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
			{
				_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭");
				uriString = string.Format(arg0: ((SurveyCode)/*Error near IL_0075: Stack underflow*/).MyNav.RoadMap.FORM_NAME, format: (string)/*Error near IL_0084: Stack underflow*/);
			}
			if (MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
			{
				base.NavigationService.Refresh();
			}
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			SurveyHelper.NavLoad = 0;
		}

		public void NavLoadPage()
		{
			//IL_0162: Incompatible stack heights: 0 vs 2
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			MyNav.LoadPage(MySurveyId, roadMapVersion);
			MySurveyId = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + MySurveyId, SurveyMsg.SurveyId_Length);
			SurveyHelper.SurveyID = MySurveyId;
			SurveyHelper.SurveyCity = MyNav.Survey.CITY_ID;
			SurveyHelper.CircleACount = MyNav.Survey.CIRCLE_A_COUNT;
			SurveyHelper.CircleACurrent = MyNav.Survey.CIRCLE_A_CURRENT;
			SurveyHelper.CircleBCount = MyNav.Survey.CIRCLE_B_COUNT;
			SurveyHelper.CircleBCurrent = MyNav.Survey.CIRCLE_B_CURRENT;
			oSurvey.UpdateSurveyLastTime(MySurveyId);
			string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), MyNav.RoadMap.FORM_NAME);
			if (MyNav.RoadMap.FORM_NAME.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
			{
				_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭");
				uriString = string.Format(arg0: ((SurveyCode)/*Error near IL_0113: Stack underflow*/).MyNav.RoadMap.FORM_NAME, format: (string)/*Error near IL_0122: Stack underflow*/);
			}
			if (MyNav.RoadMap.FORM_NAME == SurveyHelper.CurPageName)
			{
				base.NavigationService.Refresh();
			}
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = MyNav.Survey.SEQUENCE_ID;
			SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
			SurveyHelper.RoadMapVersion = MyNav.RoadMap.VERSION_ID.ToString();
			SurveyHelper.NavLoad = 1;
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			SurveyHelper.NavGoBackTimes = 0;
		}

		private void _003F183_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (btnBack.Foreground != brushOK)
			{
				return;
			}
			goto IL_0026;
			IL_0026:
			MySurveyId = _003F487_003F._003F488_003F("");
			MyStatus = 1;
			txtQuestionTitle.Text = SurveyMsg.MsgFrmCode;
			txtFill.Text = _003F487_003F._003F488_003F("");
			txtFill.Focus();
			btnBack.Visibility = Visibility.Collapsed;
			btnBack.Width = 5.0;
			btnAuto.Visibility = Visibility.Visible;
			btnLast.Visibility = Visibility.Visible;
			_003F194_003F();
			return;
			IL_0021:
			goto IL_0026;
		}

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_0098: Incompatible stack heights: 0 vs 1
			//IL_00a5: Incompatible stack heights: 0 vs 3
			//IL_00b7: Incompatible stack heights: 0 vs 2
			//IL_00d6: Incompatible stack heights: 0 vs 1
			//IL_00e8: Incompatible stack heights: 0 vs 2
			//IL_0104: Incompatible stack heights: 0 vs 1
			//IL_011a: Incompatible stack heights: 0 vs 2
			//IL_0134: Incompatible stack heights: 0 vs 2
			//IL_0144: Incompatible stack heights: 0 vs 1
			//IL_0149: Invalid comparison between Unknown and I4
			if (_003F348_003F.Key == Key.Return)
			{
				bool isEnabled = btnNav.IsEnabled;
				if ((int)/*Error near IL_009d: Stack underflow*/ != 0)
				{
					((SurveyCode)/*Error near IL_0016: Stack underflow*/)._003F58_003F((object)/*Error near IL_0016: Stack underflow*/, (RoutedEventArgs)/*Error near IL_0016: Stack underflow*/);
				}
			}
			TextBox textBox = _003F347_003F as TextBox;
			if (_003F348_003F.Key >= Key.NumPad0)
			{
				Key key = _003F348_003F.Key;
				if (/*Error near IL_00bc: Stack underflow*/ <= /*Error near IL_00bc: Stack underflow*/)
				{
					textBox.Text.Contains(_003F487_003F._003F488_003F("/"));
					if ((int)/*Error near IL_00db: Stack underflow*/ != 0)
					{
						Key key2 = _003F348_003F.Key;
						if (/*Error near IL_00ed: Stack underflow*/ == /*Error near IL_00ed: Stack underflow*/)
						{
							_003F348_003F.Handled = true;
							return;
						}
					}
					_003F348_003F.Handled = false;
					return;
				}
			}
			if (_003F348_003F.Key >= Key.D0 && ((KeyEventArgs)/*Error near IL_0054: Stack underflow*/).Key <= Key.D9)
			{
				ModifierKeys modifier = _003F348_003F.KeyboardDevice.Modifiers;
				if (/*Error near IL_011f: Stack underflow*/ != /*Error near IL_011f: Stack underflow*/)
				{
					string text = textBox.Text;
					_003F487_003F._003F488_003F("/");
					if (((string)/*Error near IL_0065: Stack underflow*/).Contains((string)/*Error near IL_0065: Stack underflow*/))
					{
						Key key3 = _003F348_003F.Key;
						if ((int)/*Error near IL_0149: Stack underflow*/ == 144)
						{
							_003F348_003F.Handled = true;
							return;
						}
					}
					_003F348_003F.Handled = false;
					return;
				}
			}
			_003F348_003F.Handled = true;
		}

		private void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_0068: Incompatible stack heights: 0 vs 1
			//IL_0079: Incompatible stack heights: 0 vs 2
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				double result = 0.0;
				if (!double.TryParse(((TextBox)/*Error near IL_0041: Stack underflow*/).Text, out result))
				{
					string text2 = textBox.Text;
					int startIndex = offset;
					int addedLength = array[0].AddedLength;
					string text = ((string)/*Error near IL_0086: Stack underflow*/).Remove(startIndex, addedLength);
					((TextBox)/*Error near IL_008b: Stack underflow*/).Text = text;
					textBox.Select(offset, 0);
				}
			}
		}

		private void _003F184_003F()
		{
			//IL_003d: Incompatible stack heights: 0 vs 2
			if (SurveyHelper.Debug)
			{
				_003F487_003F._003F488_003F("#Ġȡ\u0326ЧԤإܪ\u082b\u0951\u0a71ୱ౧\u0d76ะ停䉃襸巭獤ᐷᔴᘵ\u173aᠻ\u1938ᨹ\u1b3e᰿\u1d3c");
				string newLine = Environment.NewLine;
				MessageBox.Show(string.Concat(str2: _003F487_003F._003F488_003F("录午驱\u0323иԡ"), str3: CurPageId, str0: (string)/*Error near IL_001f: Stack underflow*/, str1: (string)/*Error near IL_001f: Stack underflow*/) + Environment.NewLine + _003F487_003F._003F488_003F("额勳ȣ\u0338С") + oQuestion.QuestionName + Environment.NewLine + _003F487_003F._003F488_003F("*īȨ\u0329Юԯجܭ࠲म煙捄ఫഷ\u0e34\u0f35\u103aᄻሸጹᐾᔿᘼ") + Environment.NewLine + _003F487_003F._003F488_003F("塮签ȣ\u0338С") + oQuestion.FillText + Environment.NewLine + SurveyHelper.ShowInfo(), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
		}

		private void _003F185_003F(object _003F347_003F, DoWorkEventArgs _003F348_003F)
		{
			//IL_015e: Incompatible stack heights: 0 vs 3
			//IL_016f: Incompatible stack heights: 0 vs 2
			IsRandomOK = oRandom.RandomSurveyMain(MySurveyId);
			int percentProgress = 70;
			bw.ReportProgress(percentProgress);
			string versionID = SurveyMsg.VersionID;
			string surveyCity = SurveyHelper.SurveyCity;
			string surveyExtend = SurveyHelper.SurveyExtend1;
			string projectId = SurveyMsg.ProjectId;
			string clientId = SurveyMsg.ClientId;
			oSurvey.AddSurvey(MySurveyId, versionID, surveyCity, projectId, clientId, surveyExtend);
			oQuestion.FillText = MySurveyId;
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			oSurvey.SaveOneAnswer(MySurveyId, SurveyHelper.SurveySequence, _003F487_003F._003F488_003F("GŊɖ\u0358"), surveyCity);
			oSurvey.SaveOneAnswer(MySurveyId, SurveyHelper.SurveySequence, _003F487_003F._003F488_003F("WŅɚ\u0347Ѭզ٤"), oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("Vņɇ\u036cѦդ")));
			if (!IsRandomOK)
			{
				string msgErrorSysSlow = SurveyMsg.MsgErrorSysSlow;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_00f8: Stack underflow*/, (string)/*Error near IL_00f8: Stack underflow*/, (MessageBoxButton)/*Error near IL_00f8: Stack underflow*/, MessageBoxImage.Hand);
				oRandom.DeleteRandom(MySurveyId);
				Thread.Sleep(1000);
				Application.Current.Shutdown();
			}
			else
			{
				if (oSurvey.GetCityCode(MySurveyId) != null)
				{
					SurveyBiz oSurvey2 = oSurvey;
					string mySurveyId = ((SurveyCode)/*Error near IL_013a: Stack underflow*/).MySurveyId;
					if (((SurveyBiz)/*Error near IL_013f: Stack underflow*/).ExistSurvey(mySurveyId))
					{
						return;
					}
				}
				MessageBox.Show(string.Format(SurveyMsg.MsgFrmCodeFailCreate, MySurveyId), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				Application.Current.Shutdown();
			}
		}

		private void _003F186_003F(object _003F347_003F, ProgressChangedEventArgs _003F348_003F)
		{
			double _003F346_003F = Convert.ToDouble(_003F348_003F.ProgressPercentage);
			_003F189_003F(_003F346_003F);
		}

		private void _003F187_003F(object _003F347_003F, RunWorkerCompletedEventArgs _003F348_003F)
		{
			_003F189_003F(100.0);
			txtMsgBar.Text = SurveyMsg.MsgFrmCodeCreate;
			btnNav.IsEnabled = true;
			_003F81_003F();
		}

		private void _003F188_003F()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
		}

		private void _003F189_003F(double _003F346_003F)
		{
			progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(progressBar1.SetValue), DispatcherPriority.Background, RangeBase.ValueProperty, _003F346_003F);
		}

		private void _003F190_003F()
		{
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = new RepeatBehavior(1.0);
			progressBar1.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
			progressBar1.Dispatcher.Invoke(new Action<DependencyProperty, object>(progressBar1.SetValue), DispatcherPriority.Background, RangeBase.ValueProperty, 100.0);
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

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			txtDate.Text = SurveyMsg.MsgFrmCodeNow + DateTime.Now.ToString();
		}

		private void _003F25_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (btnExit.Foreground != brushOK)
			{
				return;
			}
			goto IL_0016;
			IL_0016:
			_003F193_003F();
			if (MessageBox.Show(SurveyMsg.MsgConfirmExit, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.Yes))
			{
				Application.Current.Shutdown();
				return;
			}
			goto IL_0048;
			IL_006a:
			goto IL_0048;
			IL_0048:
			_003F194_003F();
			return;
			IL_0055:
			goto IL_0016;
		}

		private void _003F191_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0109: Incompatible stack heights: 0 vs 1
			//IL_011a: Incompatible stack heights: 0 vs 2
			//IL_0139: Incompatible stack heights: 0 vs 1
			//IL_014a: Incompatible stack heights: 0 vs 2
			if (btnAuto.Foreground != brushOK)
			{
				return;
			}
			goto IL_0016;
			IL_0016:
			_003F193_003F();
			SurveyId_Number_Length = SurveyMsg.SurveyId_Length;
			MySurveyId = _003F195_003F();
			string a = _003F487_003F._003F488_003F("");
			if (MySurveyId != _003F487_003F._003F488_003F(""))
			{
				_003F93_003F(MySurveyId, SurveyMsg.CITY_Length);
				a = (string)/*Error near IL_0059: Stack underflow*/;
			}
			string b = _003F487_003F._003F488_003F("");
			if (SurveyIdBegin != _003F487_003F._003F488_003F(""))
			{
				string surveyIdBegin = SurveyIdBegin;
				int cITY_Length = SurveyMsg.CITY_Length;
				b = ((SurveyCode)/*Error near IL_0088: Stack underflow*/)._003F93_003F((string)/*Error near IL_0088: Stack underflow*/, cITY_Length);
			}
			if (!(a == b))
			{
				goto IL_0154;
			}
			bool flag = MySurveyId == _003F487_003F._003F488_003F("");
			if ((int)/*Error near IL_013e: Stack underflow*/ == 0)
			{
				MySurveyId = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + (Convert.ToInt32(MySurveyId) + 1).ToString(), SurveyMsg.SurveyId_Length);
			}
			else
			{
				string surveyIdBegin2 = SurveyIdBegin;
				((SurveyCode)/*Error near IL_009f: Stack underflow*/).MySurveyId = (string)/*Error near IL_009f: Stack underflow*/;
			}
			goto IL_0160;
			IL_00de:
			goto IL_0154;
			IL_0160:
			MySurveyId = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + MySurveyId, SurveyMsg.SurveyId_Length);
			txtFill.Text = MySurveyId;
			MyStatus = 3;
			_003F182_003F();
			return;
			IL_0154:
			MySurveyId = SurveyIdBegin;
			goto IL_0160;
			IL_00e9:
			goto IL_0016;
		}

		private void _003F192_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00cf: Incompatible stack heights: 0 vs 1
			//IL_00e0: Incompatible stack heights: 0 vs 2
			//IL_00ff: Incompatible stack heights: 0 vs 1
			//IL_0110: Incompatible stack heights: 0 vs 2
			if (btnLast.Foreground != brushOK)
			{
				return;
			}
			goto IL_0016;
			IL_0016:
			_003F193_003F();
			SurveyId_Number_Length = SurveyMsg.SurveyId_Length;
			MySurveyId = _003F195_003F();
			string a = _003F487_003F._003F488_003F("");
			if (MySurveyId != _003F487_003F._003F488_003F(""))
			{
				_003F93_003F(MySurveyId, SurveyMsg.CITY_Length);
				a = (string)/*Error near IL_0059: Stack underflow*/;
			}
			string b = _003F487_003F._003F488_003F("");
			if (SurveyIdBegin != _003F487_003F._003F488_003F(""))
			{
				string surveyIdBegin = SurveyIdBegin;
				int cITY_Length = SurveyMsg.CITY_Length;
				b = ((SurveyCode)/*Error near IL_0088: Stack underflow*/)._003F93_003F((string)/*Error near IL_0088: Stack underflow*/, cITY_Length);
			}
			if (!(a == b))
			{
				goto IL_0115;
			}
			bool flag = MySurveyId == _003F487_003F._003F488_003F("");
			if ((int)/*Error near IL_0104: Stack underflow*/ != 0)
			{
				string surveyIdBegin2 = SurveyIdBegin;
				((SurveyCode)/*Error near IL_009f: Stack underflow*/).MySurveyId = (string)/*Error near IL_009f: Stack underflow*/;
			}
			goto IL_0121;
			IL_00af:
			goto IL_0016;
			IL_00a4:
			goto IL_0115;
			IL_0121:
			MySurveyId = _003F95_003F(_003F487_003F._003F488_003F("=ļȻ\u033aйԸط\u0736࠵ऴਲ਼ଲఱ") + MySurveyId, SurveyMsg.SurveyId_Length);
			txtFill.Text = MySurveyId;
			MyStatus = 3;
			_003F182_003F();
			return;
			IL_0115:
			MySurveyId = SurveyIdBegin;
			goto IL_0121;
		}

		private void _003F193_003F()
		{
			btnAuto.Foreground = new SolidColorBrush(Colors.Gray);
			btnLast.Foreground = new SolidColorBrush(Colors.Gray);
			btnOK.Foreground = new SolidColorBrush(Colors.Gray);
			btnNav.Foreground = new SolidColorBrush(Colors.Gray);
			btnExit.Foreground = new SolidColorBrush(Colors.Gray);
		}

		private void _003F194_003F()
		{
			btnAuto.Foreground = brushOK;
			btnLast.Foreground = brushOK;
			btnOK.Foreground = brushOK;
			btnNav.Foreground = brushOK;
			btnExit.Foreground = brushOK;
		}

		private string _003F195_003F()
		{
			return oSurveyConfigBiz.GetByCodeText(_003F487_003F._003F488_003F("@Ūɹͽћղٴݳ\u0861ॺ\u0a4b\u0b65"));
		}

		private void _003F196_003F(string _003F397_003F)
		{
			oSurveyConfigBiz.Save(_003F487_003F._003F488_003F("@Ūɹͽћղٴݳ\u0861ॺ\u0a4b\u0b65"), _003F397_003F);
		}

		private string _003F93_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0038: Incompatible stack heights: 0 vs 1
			//IL_003d: Incompatible stack heights: 1 vs 0
			//IL_0042: Incompatible stack heights: 0 vs 2
			//IL_0048: Incompatible stack heights: 0 vs 1
			//IL_004d: Incompatible stack heights: 1 vs 0
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int length;
			if (num > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			return ((string)/*Error near IL_002c: Stack underflow*/).Substring((int)/*Error near IL_002c: Stack underflow*/, length);
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_007b: Incompatible stack heights: 0 vs 1
			//IL_0080: Incompatible stack heights: 1 vs 0
			//IL_0085: Incompatible stack heights: 0 vs 2
			//IL_008b: Incompatible stack heights: 0 vs 1
			//IL_0090: Incompatible stack heights: 1 vs 0
			int num = _003F365_003F;
			if (num == -9999)
			{
				num = _003F362_003F.Length;
			}
			if (num < 0)
			{
				num = 0;
			}
			int length;
			if (_003F363_003F > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			int num2 = length;
			int length2;
			if (num2 + num > _003F362_003F.Length)
			{
				length2 = _003F362_003F.Length - num2;
			}
			return ((string)/*Error near IL_004e: Stack underflow*/).Substring((int)/*Error near IL_004e: Stack underflow*/, length2);
		}

		private string _003F95_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0036: Incompatible stack heights: 0 vs 1
			//IL_003b: Incompatible stack heights: 1 vs 0
			//IL_0040: Incompatible stack heights: 0 vs 1
			//IL_004d: Incompatible stack heights: 0 vs 1
			//IL_0052: Incompatible stack heights: 1 vs 0
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num <= _003F362_003F.Length)
			{
				int num2 = _003F362_003F.Length - num;
			}
			return ((string)/*Error near IL_0026: Stack underflow*/).Substring(0);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a7c᭻᱿ᵺṮέ\u206aⅧ≣⍣\u242b╼♢❯⡭"), UriKind.Relative);
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
				((SurveyCode)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				txtVersion = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				btnLast = (Button)_003F350_003F;
				btnLast.Click += _003F192_003F;
				break;
			case 5:
				btnBack = (Button)_003F350_003F;
				btnBack.Click += _003F183_003F;
				break;
			case 6:
				txtFill = (TextBox)_003F350_003F;
				txtFill.KeyDown += _003F86_003F;
				txtFill.TextChanged += _003F98_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				break;
			case 7:
				btnOK = (Button)_003F350_003F;
				btnOK.Click += _003F58_003F;
				break;
			case 8:
				btnAuto = (Button)_003F350_003F;
				btnAuto.Click += _003F191_003F;
				break;
			case 9:
				txtCITY = (TextBlock)_003F350_003F;
				break;
			case 10:
				txtMsg = (TextBlock)_003F350_003F;
				break;
			case 11:
				txtDate = (TextBlock)_003F350_003F;
				break;
			case 12:
				txtMsgBar = (TextBlock)_003F350_003F;
				break;
			case 13:
				progressBar1 = (ProgressBar)_003F350_003F;
				break;
			case 14:
				btnExit = (Button)_003F350_003F;
				btnExit.Click += _003F25_003F;
				break;
			case 15:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
