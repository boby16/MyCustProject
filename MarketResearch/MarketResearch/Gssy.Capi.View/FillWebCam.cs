using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WPFMediaKit.DirectShow.Controls;

namespace Gssy.Capi.View
{
	public class FillWebCam : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private SurveyBiz oSurveyBiz = new SurveyBiz();

		private QFill oQuestion = new QFill();

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		private bool HaveWebCam = true;

		private int PhotoCount;

		private string PhotoFileName = _003F487_003F._003F488_003F("");

		internal TextBlock txtQuestionTitle;

		internal StackPanel stkWebCam;

		internal VideoCaptureElement videoElement;

		internal TextBlock txtDevice;

		internal ComboBox cmbList;

		internal Button BtnPhoto;

		internal Button BtnViewPic;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public FillWebCam()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_05bf: Incompatible stack heights: 0 vs 2
			//IL_05cf: Incompatible stack heights: 0 vs 1
			//IL_0604: Incompatible stack heights: 0 vs 1
			//IL_0631: Incompatible stack heights: 0 vs 1
			//IL_063c: Incompatible stack heights: 0 vs 1
			//IL_064c: Incompatible stack heights: 0 vs 1
			//IL_068e: Incompatible stack heights: 0 vs 1
			//IL_069f: Incompatible stack heights: 0 vs 2
			//IL_06b5: Incompatible stack heights: 0 vs 2
			//IL_06c6: Incompatible stack heights: 0 vs 2
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = (string)BtnPhoto.Content;
			oQuestion.Init(CurPageId, 0);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				NavBase myNav = MyNav;
				int gROUP_PAGE_TYPE = ((FillWebCam)/*Error near IL_009d: Stack underflow*/).oQuestion.QDefine.GROUP_PAGE_TYPE;
				((NavBase)/*Error near IL_00ac: Stack underflow*/).GroupPageType = gROUP_PAGE_TYPE;
				MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					NavBase myNav2 = MyNav;
					string gROUP_CODEB = oQuestion.QDefine.GROUP_CODEB;
					((NavBase)/*Error near IL_011b: Stack underflow*/).GroupCodeB = gROUP_CODEB;
					MyNav.CircleBCurrent = SurveyHelper.CircleBCurrent;
					MyNav.CircleBCount = SurveyHelper.CircleBCount;
				}
				MyNav.GetCircleInfo(MySurveyId);
				oQuestion.QuestionName += MyNav.QName_Add;
				List<VEAnswer> list = new List<VEAnswer>();
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = MyNav.GroupCodeA;
				vEAnswer.CODE = MyNav.CircleACode;
				vEAnswer.CODE_TEXT = MyNav.CircleCodeTextA;
				list.Add(vEAnswer);
				SurveyHelper.CircleACode = MyNav.CircleACode;
				SurveyHelper.CircleACodeText = MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
				SurveyHelper.CircleACount = MyNav.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					VEAnswer vEAnswer2 = new VEAnswer();
					vEAnswer2.QUESTION_NAME = MyNav.GroupCodeB;
					vEAnswer2.CODE = MyNav.CircleBCode;
					vEAnswer2.CODE_TEXT = MyNav.CircleCodeTextB;
					list.Add(vEAnswer2);
					SurveyHelper.CircleBCode = MyNav.CircleBCode;
					SurveyHelper.CircleBCodeText = MyNav.CircleCodeTextB;
					SurveyHelper.CircleBCurrent = MyNav.CircleBCurrent;
					SurveyHelper.CircleBCount = MyNav.CircleBCount;
				}
			}
			else
			{
				SurveyHelper.CircleACode = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleACodeText = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleACurrent = 0;
				SurveyHelper.CircleACount = 0;
				SurveyHelper.CircleBCode = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleBCodeText = _003F487_003F._003F488_003F("");
				SurveyHelper.CircleBCurrent = 0;
				SurveyHelper.CircleBCount = 0;
				MyNav.GroupCodeA = _003F487_003F._003F488_003F("");
				MyNav.CircleACurrent = 0;
				MyNav.CircleACount = 0;
				MyNav.GroupCodeB = _003F487_003F._003F488_003F("");
				MyNav.CircleBCurrent = 0;
				MyNav.CircleBCount = 0;
			}
			oLogicEngine.SurveyID = MySurveyId;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				LogicEngine oLogicEngine2 = oLogicEngine;
				string circleACodeText = SurveyHelper.CircleACodeText;
				((LogicEngine)/*Error near IL_037e: Stack underflow*/).CircleACodeText = circleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				btnNav.Content = btnNav_Content;
				oQuestion.FillText = autoFill.Fill(oQuestion.QDefine) + _003F487_003F._003F488_003F("*ũɲ\u0366");
				if (autoFill.AutoNext(oQuestion.QDefine))
				{
					((FillWebCam)/*Error near IL_0432: Stack underflow*/)._003F58_003F((object)this, _003F348_003F);
				}
			}
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && ((FillWebCam)/*Error near IL_048e: Stack underflow*/).oQuestion.QDefine.IS_ATTACH == 1)
			{
				Button btnAttach2 = btnAttach;
				((UIElement)/*Error near IL_04a4: Stack underflow*/).Visibility = Visibility.Visible;
			}
			if (cmbList.Items.Count > 0)
			{
				cmbList.SelectedIndex = 0;
				BtnPhoto.Visibility = Visibility.Visible;
			}
			else
			{
				HaveWebCam = false;
			}
			if (!HaveWebCam)
			{
				PhotoFileName = SurveyMsg.MsgNoCamera;
				stkWebCam.Visibility = Visibility.Hidden;
				Button btnViewPic = BtnViewPic;
				((UIElement)/*Error near IL_04e3: Stack underflow*/).Visibility = Visibility.Hidden;
				BtnPhoto.Visibility = Visibility.Hidden;
				cmbList.Visibility = Visibility.Hidden;
				txtDevice.Visibility = Visibility.Hidden;
				MessageBox.Show(SurveyMsg.MsgNoWebCam, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				btnNav.Content = btnNav_Content;
			}
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				string b = _003F487_003F._003F488_003F((string)/*Error near IL_054d: Stack underflow*/);
				if (!((string)/*Error near IL_0552: Stack underflow*/ == b))
				{
					_003F487_003F._003F488_003F("NŶɯͱ");
					bool flag = (string)/*Error near IL_055c: Stack underflow*/ == (string)/*Error near IL_055c: Stack underflow*/;
				}
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				int secondsWait = SecondsWait;
				((FillWebCam)/*Error near IL_0599: Stack underflow*/).SecondsCountDown = (int)/*Error near IL_0599: Stack underflow*/;
				btnNav.Foreground = Brushes.Gray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
		}

		private void _003F108_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			videoElement.Stop();
			videoElement.Close();
		}

		private void _003F109_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_0037: Incompatible stack heights: 0 vs 1
			//IL_004b: Incompatible stack heights: 0 vs 2
			if (BtnPhoto.Visibility == Visibility.Visible)
			{
				object selectedValue = cmbList.SelectedValue;
				if ((int)/*Error near IL_003c: Stack underflow*/ == 0)
				{
					string msgNoWebCamConnect = SurveyMsg.MsgNoWebCamConnect;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_001d: Stack underflow*/, (string)/*Error near IL_001d: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				else
				{
					videoElement.Stop();
					string currentDirectory = Environment.CurrentDirectory;
					string text = Path.Combine(path2: PhotoFileName = string.Format(_003F487_003F._003F488_003F("YĐɝ\u0340ѥԯ١\u0744\u0861ऩਢ୮౯൬\u0e39ཞ\u105fᄼቴ፫ᑑᕅᙄᜦᡧᥤᨥ᭴ᱵ\u1d78ḪὩ\u2072Ⅶ"), DateTime.Now, MySurveyId, oQuestion.QuestionName), path1: currentDirectory + _003F487_003F._003F488_003F("Zŕɬ\u036cѶծ"));
					RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)((FrameworkElement)videoElement).ActualWidth, (int)((FrameworkElement)videoElement).ActualHeight, 96.0, 96.0, PixelFormats.Default);
					renderTargetBitmap.Render((Visual)videoElement);
					JpegBitmapEncoder obj = new JpegBitmapEncoder
					{
						Frames = 
						{
							BitmapFrame.Create(renderTargetBitmap)
						}
					};
					FileStream fileStream = new FileStream(text, FileMode.Create);
					obj.Save(fileStream);
					fileStream.Close();
					PhotoCount++;
					MessageBox.Show(string.Format(SurveyMsg.MsgPhotoSave, text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					BtnViewPic.Visibility = Visibility.Visible;
					BtnPhoto.Visibility = Visibility.Hidden;
					cmbList.Visibility = Visibility.Hidden;
					txtDevice.Visibility = Visibility.Hidden;
					btnNav.Content = btnNav_Content;
					oQuestion.FillText = PhotoFileName;
				}
			}
		}

		private void _003F110_003F(object _003F347_003F, TouchEventArgs _003F348_003F)
		{
			_003F109_003F(_003F347_003F, _003F348_003F);
		}

		private void _003F111_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			_003F109_003F(_003F347_003F, _003F348_003F);
		}

		private void _003F112_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			BtnViewPic.Visibility = Visibility.Hidden;
			BtnPhoto.Visibility = Visibility.Visible;
			cmbList.Visibility = Visibility.Visible;
			txtDevice.Visibility = Visibility.Visible;
			videoElement.Play();
		}

		private bool _003F87_003F()
		{
			//IL_003b: Incompatible stack heights: 0 vs 3
			if (oQuestion.FillText == _003F487_003F._003F488_003F(""))
			{
				string msgNotTakePhoto = SurveyMsg.MsgNotTakePhoto;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0040: Stack underflow*/, (string)/*Error near IL_0040: Stack underflow*/, (MessageBoxButton)/*Error near IL_0040: Stack underflow*/, MessageBoxImage.Hand);
				return true;
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> result = new List<VEAnswer>
			{
				new VEAnswer
				{
					QUESTION_NAME = oQuestion.QuestionName,
					CODE = oQuestion.FillText
				}
			};
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			return result;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00d3: Incompatible stack heights: 0 vs 3
			//IL_00ef: Incompatible stack heights: 0 vs 2
			if ((string)btnNav.Content == (string)BtnPhoto.Content)
			{
				((FillWebCam)/*Error near IL_002f: Stack underflow*/)._003F109_003F((object)/*Error near IL_002f: Stack underflow*/, (RoutedEventArgs)/*Error near IL_002f: Stack underflow*/);
				return;
			}
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0050;
			IL_0106:
			goto IL_00b0;
			IL_00d9:
			goto IL_0050;
			IL_0050:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				Button btnNav2 = btnNav;
				string content = ((FillWebCam)/*Error near IL_0070: Stack underflow*/).btnNav_Content;
				((ContentControl)/*Error near IL_0075: Stack underflow*/).Content = content;
				return;
			}
			List<VEAnswer> list = _003F88_003F();
			oLogicEngine.PageAnswer = list;
			oPageNav.oLogicEngine = oLogicEngine;
			if (!oPageNav.CheckLogic(CurPageId))
			{
				btnNav.Content = btnNav_Content;
				return;
			}
			goto IL_00b0;
			IL_00b0:
			_003F89_003F(list);
			if (SurveyHelper.Debug)
			{
				MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			MyNav.PageAnswer = list;
			oPageNav.NextPage(MyNav, base.NavigationService);
			btnNav.Content = btnNav_Content;
			videoElement.Stop();
			videoElement.Close();
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				btnNav.Foreground = Brushes.Black;
				btnNav.Content = btnNav_Content;
			}
			else
			{
				SecondsCountDown--;
				btnNav.Content = SecondsCountDown.ToString();
			}
		}

		private string _003F92_003F(string _003F362_003F, int _003F363_003F, int _003F364_003F = -9999)
		{
			//IL_0096: Incompatible stack heights: 0 vs 1
			//IL_009b: Incompatible stack heights: 1 vs 0
			//IL_00a6: Incompatible stack heights: 0 vs 1
			//IL_00ab: Incompatible stack heights: 1 vs 0
			//IL_00b6: Incompatible stack heights: 0 vs 1
			//IL_00bb: Incompatible stack heights: 1 vs 0
			//IL_00c6: Incompatible stack heights: 0 vs 1
			//IL_00cb: Incompatible stack heights: 1 vs 0
			//IL_00d6: Incompatible stack heights: 0 vs 1
			//IL_00dc: Incompatible stack heights: 0 vs 1
			int num = _003F364_003F;
			if (num == -9999)
			{
				num = _003F363_003F;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (_003F363_003F < 0)
			{
			}
			int num2 = 0;
			int num3;
			if (num2 < num)
			{
				num3 = num2;
			}
			int num4 = num3;
			int num5;
			if (num2 < num)
			{
				num5 = num;
			}
			num = num5;
			int length;
			if (num2 > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			num4 = length;
			if (_003F364_003F > _003F362_003F.Length)
			{
				int num6 = _003F362_003F.Length - 1;
			}
			num = (int)/*Error near IL_00dd: Stack underflow*/;
			return _003F362_003F.Substring(num4, num - num4 + 1);
		}

		private string _003F93_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 2
			//IL_0042: Incompatible stack heights: 0 vs 1
			//IL_004c: Incompatible stack heights: 0 vs 1
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				int length = _003F362_003F.Length;
			}
			return ((string)/*Error near IL_0051: Stack underflow*/).Substring((int)/*Error near IL_0051: Stack underflow*/, (int)/*Error near IL_0051: Stack underflow*/);
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_006f: Incompatible stack heights: 0 vs 1
			//IL_0074: Incompatible stack heights: 1 vs 0
			//IL_0079: Incompatible stack heights: 0 vs 2
			//IL_007f: Incompatible stack heights: 0 vs 1
			//IL_008b: Incompatible stack heights: 0 vs 1
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
			if (num2 + num > _003F362_003F.Length)
			{
				int num3 = _003F362_003F.Length - num2;
			}
			return ((string)/*Error near IL_0090: Stack underflow*/).Substring((int)/*Error near IL_0090: Stack underflow*/, (int)/*Error near IL_0090: Stack underflow*/);
		}

		private string _003F95_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0049: Incompatible stack heights: 0 vs 1
			//IL_004e: Incompatible stack heights: 1 vs 0
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

		private void _003F85_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyHelper.AttachSurveyId = MySurveyId;
			SurveyHelper.AttachQName = oQuestion.QuestionName;
			SurveyHelper.AttachPageId = CurPageId;
			SurveyHelper.AttachCount = 0;
			SurveyHelper.AttachReadOnlyModel = false;
			new EditAttachments().ShowDialog();
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a69᭧ᱡ\u1d60ṼὯ\u206bⅫ≦⍫\u242b╼♢❯⡭"), UriKind.Relative);
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
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Expected O, but got Unknown
			switch (_003F349_003F)
			{
			case 1:
				((FillWebCam)_003F350_003F).Loaded += _003F80_003F;
				((FillWebCam)_003F350_003F).Unloaded += _003F108_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				stkWebCam = (StackPanel)_003F350_003F;
				break;
			case 4:
				videoElement = _003F350_003F;
				((UIElement)videoElement).PreviewTouchDown += _003F109_003F;
				((UIElement)videoElement).PreviewMouseLeftButtonDown += _003F111_003F;
				break;
			case 5:
				txtDevice = (TextBlock)_003F350_003F;
				break;
			case 6:
				cmbList = (ComboBox)_003F350_003F;
				break;
			case 7:
				BtnPhoto = (Button)_003F350_003F;
				BtnPhoto.Click += _003F109_003F;
				break;
			case 8:
				BtnViewPic = (Button)_003F350_003F;
				BtnViewPic.Click += _003F112_003F;
				break;
			case 9:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 10:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 11:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0039:
			goto IL_0043;
		}
	}
}
