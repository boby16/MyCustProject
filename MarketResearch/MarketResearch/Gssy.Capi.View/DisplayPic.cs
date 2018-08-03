using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
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

namespace Gssy.Capi.View
{
	public class DisplayPic : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QDisplay oQuestion = new QDisplay();

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal Grid gridContent;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal TextBlock txtLeftTitle;

		internal TextBlock txtRightTitle;

		internal TextBlock txtQuestionNote;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public DisplayPic()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0500: Incompatible stack heights: 0 vs 1
			//IL_0507: Incompatible stack heights: 0 vs 1
			//IL_07f4: Incompatible stack heights: 0 vs 1
			//IL_0813: Incompatible stack heights: 0 vs 1
			//IL_0815: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				MyNav.GroupPageType = oQuestion.QDefine.GROUP_PAGE_TYPE;
				MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					MyNav.GroupCodeB = oQuestion.QDefine.GROUP_CODEB;
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
				oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			if (SurveyHelper.AutoFill && new AutoFill().AutoNext(oQuestion.QDefine))
			{
				_003F58_003F(this, _003F348_003F);
			}
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list2[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (list2.Count <= 1)
			{
				_003F487_003F._003F488_003F("");
			}
			else
			{
				string text6 = list2[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_0508: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				string text = _003F487_003F._003F488_003F("");
				if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
				{
					text = oLogicEngine.Route(oQuestion.QDefine.CONTROL_TOOLTIP);
				}
				else if (oQuestion.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F(""))
				{
					oQuestion.InitCircle();
					string text2 = _003F487_003F._003F488_003F("");
					if (MyNav.GroupLevel == _003F487_003F._003F488_003F("@"))
					{
						text2 = MyNav.CircleACode;
					}
					if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
					{
						text2 = MyNav.CircleBCode;
					}
					if (text2 != _003F487_003F._003F488_003F(""))
					{
						foreach (SurveyDetail qCircleDetail in oQuestion.QCircleDetails)
						{
							if (qCircleDetail.CODE == text2)
							{
								text = oLogicEngine.Route(qCircleDetail.EXTEND_1);
								break;
							}
						}
					}
				}
				string text3 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŋɠ\u0360Ѫգ\u065d") + text;
				if (oFunc.LEFT(oQuestion.QDefine.CONTROL_TOOLTIP, 1) == _003F487_003F._003F488_003F("\""))
				{
					text3 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + oFunc.MID(text, 1, -9999);
				}
				else if (!File.Exists(text3))
				{
					text3 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + text;
				}
				Image image = new Image();
				if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
				{
					int cONTROL_HEIGHT = oQuestion.QDefine.CONTROL_HEIGHT;
				}
				double minHeight = (double)/*Error near IL_0816: Stack underflow*/;
				((FrameworkElement)/*Error near IL_081b: Stack underflow*/).MinHeight = minHeight;
				if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
				{
					image.Height = (double)oQuestion.QDefine.CONTROL_HEIGHT;
				}
				if (oQuestion.QDefine.CONTROL_WIDTH != 0)
				{
					image.Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
				}
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 5.0, 0.0, 10.0);
				image.SetValue(Grid.ColumnProperty, 1);
				image.SetValue(Grid.RowProperty, 2);
				image.HorizontalAlignment = HorizontalAlignment.Center;
				image.VerticalAlignment = VerticalAlignment.Center;
				if (oQuestion.QDefine.CONTROL_TYPE == 1)
				{
					image.HorizontalAlignment = HorizontalAlignment.Left;
				}
				else if (oQuestion.QDefine.CONTROL_TYPE == 2)
				{
					image.HorizontalAlignment = HorizontalAlignment.Right;
				}
				else if (oQuestion.QDefine.CONTROL_TYPE == 3)
				{
					image.VerticalAlignment = VerticalAlignment.Top;
				}
				else if (oQuestion.QDefine.CONTROL_TYPE == 4)
				{
					image.VerticalAlignment = VerticalAlignment.Bottom;
				}
				else if (oQuestion.QDefine.CONTROL_TYPE == 13)
				{
					image.HorizontalAlignment = HorizontalAlignment.Left;
					image.VerticalAlignment = VerticalAlignment.Top;
				}
				else if (oQuestion.QDefine.CONTROL_TYPE == 14)
				{
					image.HorizontalAlignment = HorizontalAlignment.Left;
					image.VerticalAlignment = VerticalAlignment.Bottom;
				}
				else if (oQuestion.QDefine.CONTROL_TYPE == 23)
				{
					image.HorizontalAlignment = HorizontalAlignment.Right;
					image.VerticalAlignment = VerticalAlignment.Top;
				}
				else if (oQuestion.QDefine.CONTROL_TYPE == 24)
				{
					image.HorizontalAlignment = HorizontalAlignment.Right;
					image.VerticalAlignment = VerticalAlignment.Bottom;
				}
				try
				{
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.UriSource = new Uri(text3, UriKind.RelativeOrAbsolute);
					bitmapImage.EndInit();
					image.Source = bitmapImage;
					gridContent.Children.Add(image);
				}
				catch (Exception)
				{
				}
				qUESTION_TITLE = oQuestion.QDefine.QUESTION_CONTENT;
				string text4 = oQuestion.QDefine.CONTROL_MASK;
				string a = oFunc.LEFT(text4, 1).ToUpper();
				if (a == _003F487_003F._003F488_003F("M"))
				{
					text4 = oFunc.MID(text4, 1, -9999);
					oBoldTitle.SetTextBlock(txtLeftTitle, qUESTION_TITLE, oQuestion.QDefine.CONTROL_FONTSIZE, text4, true);
					oBoldTitle.SetTextBlock(txtRightTitle, _003F487_003F._003F488_003F(""), 0, _003F487_003F._003F488_003F(""), true);
				}
				else
				{
					if (a == _003F487_003F._003F488_003F("S"))
					{
						text4 = oFunc.MID(text4, 1, -9999);
					}
					oBoldTitle.SetTextBlock(txtRightTitle, qUESTION_TITLE, oQuestion.QDefine.CONTROL_FONTSIZE, text4, true);
					oBoldTitle.SetTextBlock(txtLeftTitle, _003F487_003F._003F488_003F(""), 0, _003F487_003F._003F488_003F(""), true);
				}
			}
			else
			{
				qUESTION_TITLE = oQuestion.QDefine.QUESTION_CONTENT;
				string text5 = oQuestion.QDefine.CONTROL_MASK;
				string a2 = oFunc.LEFT(text5, 1).ToUpper();
				if (a2 == _003F487_003F._003F488_003F("M") || a2 == _003F487_003F._003F488_003F("S"))
				{
					text5 = oFunc.MID(text5, 1, -9999);
				}
				TextBlock textBlock = new TextBlock();
				textBlock.HorizontalAlignment = HorizontalAlignment.Center;
				textBlock.Style = (Style)FindResource(_003F487_003F._003F488_003F("ZŤɸ\u0367ѯ՝٭ݿ\u0872\u0956\u0a70\u0b7a౮\u0d64"));
				textBlock.FontSize = 32.0;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.SetValue(Grid.ColumnProperty, 1);
				textBlock.SetValue(Grid.RowProperty, 2);
				gridContent.Children.Add(textBlock);
				oBoldTitle.SetTextBlock(textBlock, qUESTION_TITLE, oQuestion.QDefine.CONTROL_FONTSIZE, text5, true);
				oBoldTitle.SetTextBlock(txtLeftTitle, _003F487_003F._003F488_003F(""), 0, _003F487_003F._003F488_003F(""), true);
				oBoldTitle.SetTextBlock(txtRightTitle, _003F487_003F._003F488_003F(""), 0, _003F487_003F._003F488_003F(""), true);
			}
			qUESTION_TITLE = oQuestion.QDefine.NOTE;
			oBoldTitle.SetTextBlock(txtQuestionNote, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				btnNav.Foreground = Brushes.Gray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
		}

		private void _003F83_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			if (_003F348_003F.Key == Key.Next)
			{
				_003F58_003F(_003F347_003F, _003F348_003F);
			}
			return;
			IL_0034:
			goto IL_0020;
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			oPageNav.oLogicEngine = oLogicEngine;
			if (!oPageNav.CheckLogic(CurPageId))
			{
				btnNav.Content = btnNav_Content;
			}
			else
			{
				oPageNav.NextPage(MyNav, base.NavigationService);
				btnNav.Content = btnNav_Content;
			}
			return;
			IL_0069:
			goto IL_0020;
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
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a6b᭧᱾ᵼṧὫ⁰ⅸ≮⍥\u242b╼♢❯⡭"), UriKind.Relative);
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
				((DisplayPic)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				gridContent = (Grid)_003F350_003F;
				break;
			case 3:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 5:
				txtLeftTitle = (TextBlock)_003F350_003F;
				break;
			case 6:
				txtRightTitle = (TextBlock)_003F350_003F;
				break;
			case 7:
				txtQuestionNote = (TextBlock)_003F350_003F;
				break;
			case 8:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 9:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 10:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
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
