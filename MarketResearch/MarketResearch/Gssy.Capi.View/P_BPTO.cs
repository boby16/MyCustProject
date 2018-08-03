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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class P_BPTO : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private UDPX oFunc = new UDPX();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QBPTO oQuestion = new QBPTO();

		private List<Button> listButton = new List<Button>();

		private List<classListBorder> matrixButton = new List<classListBorder>();

		private List<int> listCurrent = new List<int>();

		private List<Border> listHideBorder = new List<Border>();

		private List<Border> listShowBorder = new List<Border>();

		private List<Button> listAnswerButton = new List<Button>();

		private bool IsFinish;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private bool Button_Hide;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal Button btnReturn;

		internal ScrollViewer scrollmain;

		internal WrapPanel wrapPanel1;

		internal StackPanel stackPanel1;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public P_BPTO()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_02f0: Incompatible stack heights: 0 vs 1
			//IL_02f7: Incompatible stack heights: 0 vs 1
			//IL_0537: Incompatible stack heights: 0 vs 1
			//IL_0556: Incompatible stack heights: 0 vs 1
			//IL_055b: Incompatible stack heights: 0 vs 1
			//IL_0580: Incompatible stack heights: 0 vs 1
			//IL_059f: Incompatible stack heights: 0 vs 1
			//IL_05a4: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			btnReturn.Content = _003F487_003F._003F488_003F("Fœɖ\u034e") + btnReturn.Content;
			oQuestion.Init(CurPageId, 0, false);
			MyNav.GroupLevel = _003F487_003F._003F488_003F("");
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
			oLogicEngine.SurveyID = MySurveyId;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			}
			string sHOW_LOGIC = oQuestion.QDefine.SHOW_LOGIC;
			List<string> list = new List<string>();
			list.Add(_003F487_003F._003F488_003F(""));
			if (sHOW_LOGIC != _003F487_003F._003F488_003F(""))
			{
				list = oBoldTitle.ParaToList(sHOW_LOGIC, _003F487_003F._003F488_003F("-Į"));
				if (list.Count > 1)
				{
					oQuestion.QDefine.DETAIL_ID = oLogicEngine.Route(list[1]);
				}
			}
			oQuestion.InitDetailID(CurPageId, 0);
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list2[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (list2.Count <= 1)
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			else
			{
				string text = list2[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_02f8: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int i = 0; i < oQuestion.QDetails.Count(); i++)
				{
					oQuestion.QDetails[i].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[i].CODE_TEXT);
				}
			}
			if (list[0].Trim() != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(list[0], ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int j = 0; j < array.Count(); j++)
				{
					foreach (SurveyDetail qDetail in oQuestion.QDetails)
					{
						if (qDetail.CODE == array[j].ToString())
						{
							list3.Add(qDetail);
							break;
						}
					}
				}
				oQuestion.QDetails = list3;
			}
			else if (oQuestion.QDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomDetails();
			}
			Button_Type = oQuestion.QDefine.CONTROL_TYPE;
			if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				int cONTROL_FONTSIZE = oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			else
			{
				int btnFontSize = SurveyHelper.BtnFontSize;
			}
			((P_BPTO)/*Error near IL_0560: Stack underflow*/).Button_FontSize = (int)/*Error near IL_0560: Stack underflow*/;
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				int cONTROL_HEIGHT = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			else
			{
				int btnHeight = SurveyHelper.BtnHeight;
			}
			((P_BPTO)/*Error near IL_05a9: Stack underflow*/).Button_Height = (int)/*Error near IL_05a9: Stack underflow*/;
			Button_Width = 280;
			if (oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (Button_Type == 2 || Button_Type == 4)
				{
					Button_Width = 440;
				}
			}
			else
			{
				Button_Width = oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (Button_FontSize == -1)
			{
				Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			Button_Hide = (Button_FontSize < 0);
			Button_FontSize = Math.Abs(Button_FontSize);
			_003F28_003F();
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				if (!(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")))
				{
					if (navOperation == _003F487_003F._003F488_003F("NŶɯͱ"))
					{
					}
				}
				else if (SurveyHelper.AutoFill)
				{
					foreach (Border child in wrapPanel1.Children)
					{
						Border border = child;
						if (IsFinish)
						{
							break;
						}
						_003F139_003F();
					}
				}
			}
			else
			{
				oQuestion.ReadAnswer(MySurveyId, SurveyHelper.SurveySequence);
				List<string> list4 = new List<string>();
				foreach (SurveyAnswer item in oQuestion.QAnswersRead)
				{
					if (oFunc.MID(item.QUESTION_NAME, 0, (oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ")).Length) == oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ"))
					{
						list4.Add(item.CODE);
					}
				}
				int num = 0;
				foreach (string item2 in list4)
				{
					num++;
					if (num < list4.Count)
					{
						_003F137_003F(item2);
					}
					else
					{
						_003F138_003F(item2);
					}
				}
				SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QCircleADefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				btnNav.Foreground = Brushes.LightGray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F("2") && oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F("5"))
			{
				btnNav.Visibility = Visibility.Hidden;
			}
		}

		private void _003F137_003F(string _003F371_003F)
		{
			foreach (Button item in listButton)
			{
				string b = item.Name.Substring(2);
				if (_003F371_003F == b)
				{
					_003F29_003F(item, null);
					break;
				}
			}
		}

		private void _003F138_003F(string _003F371_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double opacity = 0.2;
			bool flag = false;
			foreach (Border child in wrapPanel1.Children)
			{
				foreach (UIElement child2 in ((WrapPanel)child.Child).Children)
				{
					if (child2 is Button)
					{
						Button button = (Button)child2;
						string b = button.Name.Substring(2);
						if (_003F371_003F == b)
						{
							button.Style = style;
							flag = true;
						}
					}
					if (child2 is Image)
					{
						Image image = (Image)child2;
						string b2 = image.Name.Substring(2);
						if (_003F371_003F == b2)
						{
							image.Opacity = opacity;
							flag = true;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
		}

		private void _003F139_003F()
		{
			foreach (Border child in wrapPanel1.Children)
			{
				if (child.Visibility == Visibility.Visible)
				{
					foreach (UIElement child2 in ((WrapPanel)child.Child).Children)
					{
						if (child2 is Button)
						{
							_003F29_003F((Button)child2, null);
							return;
						}
					}
				}
			}
		}

		private void _003F28_003F()
		{
			//IL_00b7: Incompatible stack heights: 0 vs 1
			//IL_00dd: Incompatible stack heights: 0 vs 1
			//IL_00de: Incompatible stack heights: 0 vs 1
			//IL_0143: Incompatible stack heights: 0 vs 1
			//IL_0160: Incompatible stack heights: 0 vs 1
			//IL_016e: Incompatible stack heights: 0 vs 1
			//IL_01c0: Incompatible stack heights: 0 vs 1
			//IL_01fe: Incompatible stack heights: 0 vs 1
			//IL_01ff: Incompatible stack heights: 0 vs 1
			List<SurveyDetail>.Enumerator enumerator = oQuestion.QCircleADetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current2 = enumerator.Current;
					matrixButton.Add(new classListBorder());
					listCurrent.Add(0);
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Brush borderBrush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			WrapPanel wrapPanel = wrapPanel1;
			if (Button_Type != 2 && Button_Type != 4)
			{
			}
			((WrapPanel)/*Error near IL_00e3: Stack underflow*/).Orientation = (Orientation)/*Error near IL_00e3: Stack underflow*/;
			enumerator = oQuestion.QDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current = enumerator.Current;
					Border border = new Border();
					if (!(oQuestion.QDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("")))
					{
						new Thickness(0.0);
					}
					else
					{
						new Thickness(1.0);
					}
					((Border)/*Error near IL_0173: Stack underflow*/).BorderThickness = (Thickness)/*Error near IL_0173: Stack underflow*/;
					border.BorderBrush = borderBrush;
					wrapPanel.Children.Add(border);
					WrapPanel wrapPanel2 = new WrapPanel();
					if (!(oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("2")) && !(oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("5")))
					{
					}
					((WrapPanel)/*Error near IL_0204: Stack underflow*/).Orientation = (Orientation)/*Error near IL_0204: Stack underflow*/;
					List<string> list = oFunc.StringToList(oQuestion.QDefine.CONTROL_TOOLTIP, _003F487_003F._003F488_003F("-"));
					int num = 5;
					int num2 = 5;
					int num3 = 5;
					int num4 = 3;
					if (list.Count == 1)
					{
						num4 = (num3 = (num2 = (num = oFunc.StringToInt(list[0]))));
					}
					else if (list.Count == 4)
					{
						num = oFunc.StringToInt(list[0]);
						num2 = oFunc.StringToInt(list[1]);
						num3 = oFunc.StringToInt(list[2]);
						num4 = oFunc.StringToInt(list[3]);
					}
					wrapPanel2.Margin = new Thickness((double)num, (double)num2, (double)num3, (double)num4);
					border.Child = wrapPanel2;
					matrixButton[current.RANDOM_SET - 1].Borders.Add(border);
					if (matrixButton[current.RANDOM_SET - 1].Borders.Count > 1)
					{
						border.Visibility = Visibility.Collapsed;
					}
					Button button = new Button();
					button.Name = _003F487_003F._003F488_003F("`Ş") + current.CODE;
					button.Content = current.CODE_TEXT;
					button.Tag = current.RANDOM_SET;
					button.Margin = new Thickness(10.0, 10.0, 10.0, 10.0);
					button.Style = style;
					button.Click += _003F29_003F;
					button.FontSize = (double)Button_FontSize;
					button.MinWidth = (double)Button_Width;
					button.MinHeight = (double)Button_Height;
					if (oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F("3") && oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F("5"))
					{
						wrapPanel2.Children.Add(button);
					}
					listButton.Add(button);
					string text = oLogicEngine.Route(current.EXTEND_1);
					if (text != _003F487_003F._003F488_003F(""))
					{
						Image image = new Image();
						image.Name = _003F487_003F._003F488_003F("rŞ") + current.CODE;
						image.Tag = current.RANDOM_SET;
						if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("2") || oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("5"))
						{
							image.MinWidth = 46.0;
							image.Height = (double)Button_Height;
						}
						else
						{
							image.MinHeight = 46.0;
							image.Width = (double)Button_Width;
						}
						image.Stretch = Stretch.Uniform;
						image.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
						try
						{
							string text2 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŋɠ\u0360Ѫգ\u065d") + text;
							if (_003F93_003F(text, 1) == _003F487_003F._003F488_003F("\""))
							{
								text2 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + _003F94_003F(text, 1, -9999);
							}
							else if (!File.Exists(text2))
							{
								text2 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + text;
							}
							BitmapImage bitmapImage = new BitmapImage();
							bitmapImage.BeginInit();
							bitmapImage.UriSource = new Uri(text2, UriKind.RelativeOrAbsolute);
							bitmapImage.EndInit();
							image.Source = bitmapImage;
							image.MouseLeftButtonUp += _003F120_003F;
							image.MouseEnter += _003F140_003F;
							image.MouseLeave += _003F141_003F;
							wrapPanel2.Children.Add(image);
							if (Button_Hide)
							{
								button.Visibility = Visibility.Collapsed;
							}
						}
						catch (Exception)
						{
						}
					}
					if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("3") || oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("5"))
					{
						wrapPanel2.Children.Add(button);
					}
					if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("3"))
					{
						wrapPanel2.VerticalAlignment = VerticalAlignment.Bottom;
					}
					if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("5"))
					{
						wrapPanel2.HorizontalAlignment = HorizontalAlignment.Right;
					}
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private void _003F140_003F(object _003F347_003F, MouseEventArgs _003F348_003F)
		{
			((Image)_003F347_003F).Opacity = 0.4;
		}

		private void _003F141_003F(object _003F347_003F, MouseEventArgs _003F348_003F)
		{
			((Image)_003F347_003F).Opacity = 1.0;
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F = null)
		{
			Button obj = (Button)_003F347_003F;
			int _003F376_003F = (int)obj.Tag;
			string _003F377_003F = obj.Name.Substring(2);
			_003F142_003F(_003F376_003F, _003F377_003F);
		}

		private void _003F120_003F(object _003F347_003F, RoutedEventArgs _003F348_003F = null)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double opacity = 0.2;
			double opacity2 = 1.0;
			Image obj = (Image)_003F347_003F;
			obj.Opacity = opacity;
			int _003F376_003F = (int)obj.Tag;
			string _003F377_003F = obj.Name.Substring(2);
			_003F142_003F(_003F376_003F, _003F377_003F);
			obj.Opacity = opacity2;
		}

		private void _003F142_003F(int _003F376_003F, string _003F377_003F)
		{
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double opacity = 1.0;
			foreach (Border child in wrapPanel1.Children)
			{
				if (child.Visibility == Visibility.Visible)
				{
					foreach (UIElement child2 in ((WrapPanel)child.Child).Children)
					{
						if (child2 is Button)
						{
							((Button)child2).Style = style;
						}
						if (child2 is Image)
						{
							((Image)child2).Opacity = opacity;
						}
					}
				}
			}
			foreach (UIElement child3 in ((WrapPanel)matrixButton[_003F376_003F - 1].Borders[listCurrent[_003F376_003F - 1]].Child).Children)
			{
				if (child3 is Button)
				{
					listAnswerButton.Add((Button)child3);
				}
			}
			btnReturn.Visibility = Visibility.Visible;
			matrixButton[_003F376_003F - 1].Borders[listCurrent[_003F376_003F - 1]].Visibility = Visibility.Collapsed;
			listHideBorder.Add(matrixButton[_003F376_003F - 1].Borders[listCurrent[_003F376_003F - 1]]);
			List<int> list = listCurrent;
			int index = _003F376_003F - 1;
			list[index]++;
			if (listCurrent[_003F376_003F - 1] < matrixButton[_003F376_003F - 1].Borders.Count)
			{
				matrixButton[_003F376_003F - 1].Borders[listCurrent[_003F376_003F - 1]].Visibility = Visibility.Visible;
				listShowBorder.Add(matrixButton[_003F376_003F - 1].Borders[listCurrent[_003F376_003F - 1]]);
			}
			else if (oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F("3") || oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F("5"))
			{
				if (listAnswerButton.Count >= oQuestion.QCircleADetails.Count * oQuestion.QCircleBDetails.Count)
				{
					_003F144_003F(false);
				}
			}
			else
			{
				_003F144_003F(false);
			}
			if ((oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F("3") || oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F("5")) && SurveyHelper.NavOperation != _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				int num = 0;
				Button _003F347_003F = new Button();
				foreach (Border child4 in wrapPanel1.Children)
				{
					if (child4.Visibility == Visibility.Visible)
					{
						num++;
						foreach (UIElement child5 in ((WrapPanel)child4.Child).Children)
						{
							if (child5 is Button)
							{
								_003F347_003F = (Button)child5;
								break;
							}
						}
					}
				}
				if (num == 1)
				{
					_003F29_003F(_003F347_003F, null);
				}
			}
		}

		private void _003F143_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double opacity = 0.2;
			double opacity2 = 1.0;
			int num = (int)listAnswerButton[listAnswerButton.Count - 1].Tag;
			if (listCurrent[num - 1] < oQuestion.QCircleBDetails.Count)
			{
				listShowBorder[listShowBorder.Count - 1].Visibility = Visibility.Collapsed;
				listShowBorder.RemoveAt(listShowBorder.Count - 1);
			}
			List<int> list = listCurrent;
			int index = num - 1;
			list[index]--;
			Border border = listHideBorder[listHideBorder.Count - 1];
			border.Visibility = Visibility.Visible;
			foreach (Border child in wrapPanel1.Children)
			{
				if (child.Visibility == Visibility.Visible)
				{
					foreach (UIElement child2 in ((WrapPanel)child.Child).Children)
					{
						if (child2 is Button)
						{
							((Button)child2).Style = style2;
						}
						if (child2 is Image)
						{
							((Image)child2).Opacity = opacity2;
						}
					}
				}
			}
			foreach (UIElement child3 in ((WrapPanel)border.Child).Children)
			{
				if (child3 is Button)
				{
					((Button)child3).Style = style;
				}
				if (child3 is Image)
				{
					((Image)child3).Opacity = opacity;
				}
			}
			listHideBorder.RemoveAt(listHideBorder.Count - 1);
			listAnswerButton.RemoveAt(listAnswerButton.Count - 1);
			if (listHideBorder.Count == 0)
			{
				btnReturn.Visibility = Visibility.Collapsed;
			}
		}

		private bool _003F87_003F()
		{
			if (MessageBox.Show(SurveyMsg.MsgBPTO_NotFinish + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
			{
				return true;
			}
			goto IL_0040;
			IL_0049:
			goto IL_0040;
			IL_0040:
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			List<int> list2 = new List<int>();
			List<SurveyDetail>.Enumerator enumerator = oQuestion.QCircleADetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current3 = enumerator.Current;
					list2.Add(0);
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			int num = 1;
			foreach (Button item in listAnswerButton)
			{
				string text = item.Name.Substring(2);
				string text2 = ((int)item.Tag).ToString();
				int num2 = (int)item.Tag - 1;
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ") + num.ToString();
				vEAnswer.CODE = text;
				list.Add(vEAnswer);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer.QUESTION_NAME + _003F487_003F._003F488_003F("<") + vEAnswer.CODE;
				vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ł") + text;
				vEAnswer.CODE = num.ToString();
				list.Add(vEAnswer);
				vEAnswer = new VEAnswer();
				List<int> list3 = list2;
				int index = num2;
				list3[index]++;
				vEAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + text2 + _003F487_003F._003F488_003F("]œ") + list2[num2];
				vEAnswer.CODE = num.ToString();
				list.Add(vEAnswer);
				num++;
			}
			num = 1;
			enumerator = oQuestion.QCircleADetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current2 = enumerator.Current;
					VEAnswer vEAnswer2 = new VEAnswer();
					vEAnswer2.QUESTION_NAME = oQuestion.CircleAQuestionName + _003F487_003F._003F488_003F("]œ") + num.ToString();
					vEAnswer2.CODE = current2.CODE;
					list.Add(vEAnswer2);
					int num3 = 1;
					foreach (SurveyDetail qCircleBDetail in oQuestion.QCircleBDetails)
					{
						VEAnswer vEAnswer3 = new VEAnswer();
						vEAnswer3.QUESTION_NAME = oQuestion.CircleBQuestionName + _003F487_003F._003F488_003F("]œ") + num.ToString() + _003F487_003F._003F488_003F("]œ") + num3.ToString();
						vEAnswer3.CODE = qCircleBDetail.CODE;
						list.Add(vEAnswer3);
						num3++;
					}
					num++;
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			SurveyHelper.Answer = _003F94_003F(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			List<int> list2 = new List<int>();
			List<SurveyDetail>.Enumerator enumerator = oQuestion.QCircleADetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current3 = enumerator.Current;
					list2.Add(0);
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			DateTime now = DateTime.Now;
			int num = 1;
			foreach (Button item in listAnswerButton)
			{
				string text = item.Name.Substring(2);
				string text2 = ((int)item.Tag).ToString();
				int num2 = (int)item.Tag - 1;
				SurveyAnswer surveyAnswer = new SurveyAnswer();
				surveyAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ") + num.ToString();
				surveyAnswer.CODE = text;
				surveyAnswer.MULTI_ORDER = num;
				surveyAnswer.BEGIN_DATE = oQuestion.QInitDateTime;
				surveyAnswer.MODIFY_DATE = now;
				list.Add(surveyAnswer);
				surveyAnswer = new SurveyAnswer();
				surveyAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ł") + text;
				surveyAnswer.CODE = num.ToString();
				surveyAnswer.MULTI_ORDER = 0;
				surveyAnswer.BEGIN_DATE = oQuestion.QInitDateTime;
				surveyAnswer.MODIFY_DATE = now;
				list.Add(surveyAnswer);
				surveyAnswer = new SurveyAnswer();
				List<int> list3 = list2;
				int index = num2;
				list3[index]++;
				surveyAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + text2 + _003F487_003F._003F488_003F("]œ") + list2[num2];
				surveyAnswer.CODE = num.ToString();
				surveyAnswer.MULTI_ORDER = 0;
				surveyAnswer.BEGIN_DATE = oQuestion.QInitDateTime;
				surveyAnswer.MODIFY_DATE = now;
				list.Add(surveyAnswer);
				num++;
			}
			num = 1;
			enumerator = oQuestion.QCircleADetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current2 = enumerator.Current;
					SurveyAnswer surveyAnswer2 = new SurveyAnswer();
					surveyAnswer2.QUESTION_NAME = oQuestion.CircleAQuestionName + _003F487_003F._003F488_003F("]œ") + num.ToString();
					surveyAnswer2.CODE = current2.CODE;
					surveyAnswer2.MULTI_ORDER = 0;
					surveyAnswer2.BEGIN_DATE = oQuestion.QInitDateTime;
					surveyAnswer2.MODIFY_DATE = now;
					list.Add(surveyAnswer2);
					int num3 = 1;
					foreach (SurveyDetail qCircleBDetail in oQuestion.QCircleBDetails)
					{
						SurveyAnswer surveyAnswer3 = new SurveyAnswer();
						surveyAnswer3.QUESTION_NAME = oQuestion.CircleBQuestionName + _003F487_003F._003F488_003F("]œ") + num.ToString() + _003F487_003F._003F488_003F("]œ") + num3.ToString();
						surveyAnswer3.CODE = qCircleBDetail.CODE;
						surveyAnswer3.MULTI_ORDER = 0;
						surveyAnswer3.BEGIN_DATE = oQuestion.QInitDateTime;
						surveyAnswer3.MODIFY_DATE = now;
						list.Add(surveyAnswer3);
						num3++;
					}
					num++;
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			oQuestion.BeforeSave(list);
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				oPageNav.PageDataLog(oQuestion.QDefine.PAGE_COUNT_DOWN, _003F370_003F, btnNav, MySurveyId);
			}
		}

		private void _003F58_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			_003F144_003F(true);
		}

		private void _003F144_003F(bool _003F378_003F = false)
		{
			//IL_00ca: Incompatible stack heights: 0 vs 1
			//IL_00e0: Incompatible stack heights: 0 vs 2
			//IL_00f1: Incompatible stack heights: 0 vs 2
			//IL_0106: Incompatible stack heights: 0 vs 2
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			oPageNav.Refresh();
			if (_003F378_003F)
			{
				_003F87_003F();
				if ((int)/*Error near IL_00cf: Stack underflow*/ != 0)
				{
					Button btnNav2 = btnNav;
					string btnNav_Content2 = btnNav_Content;
					((ContentControl)/*Error near IL_004b: Stack underflow*/).Content = (object)/*Error near IL_004b: Stack underflow*/;
					return;
				}
			}
			List<VEAnswer> list = _003F88_003F();
			oLogicEngine.PageAnswer = list;
			oPageNav.oLogicEngine = oLogicEngine;
			if (!oPageNav.CheckLogic(CurPageId))
			{
				Button btnNav3 = btnNav;
				string content = ((P_BPTO)/*Error near IL_008b: Stack underflow*/).btnNav_Content;
				((ContentControl)/*Error near IL_0090: Stack underflow*/).Content = content;
			}
			else
			{
				IsFinish = true;
				_003F89_003F(list);
				if (SurveyHelper.Debug)
				{
					SurveyHelper.ShowPageAnswer(list);
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_010d: Stack underflow*/, (string)/*Error near IL_010d: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				}
				MyNav.PageAnswer = list;
				oPageNav.NextPage(MyNav, base.NavigationService);
				btnNav.Content = btnNav_Content;
			}
			return;
			IL_00b5:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0030: Incompatible stack heights: 0 vs 1
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				((P_BPTO)/*Error near IL_0010: Stack underflow*/).btnNav.Foreground = Brushes.Black;
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
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0041: Incompatible stack heights: 1 vs 0
			//IL_0046: Incompatible stack heights: 0 vs 2
			//IL_004c: Incompatible stack heights: 0 vs 1
			//IL_0051: Incompatible stack heights: 1 vs 0
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
			//IL_0069: Incompatible stack heights: 0 vs 1
			//IL_0074: Incompatible stack heights: 0 vs 1
			//IL_0079: Incompatible stack heights: 1 vs 0
			//IL_007e: Incompatible stack heights: 0 vs 2
			//IL_0084: Incompatible stack heights: 0 vs 1
			//IL_008b: Incompatible stack heights: 0 vs 1
			int num = _003F365_003F;
			if (num == -9999)
			{
				num = _003F362_003F.Length;
			}
			if (num < 0)
			{
				num = (int)/*Error near IL_0015: Stack underflow*/;
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
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0048: Incompatible stack heights: 0 vs 2
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int startIndex;
			if (num > _003F362_003F.Length)
			{
				startIndex = 0;
			}
			else
			{
				int length = _003F362_003F.Length;
				startIndex = /*Error near IL_001c: Stack underflow*/- /*Error near IL_001c: Stack underflow*/;
			}
			return ((string)/*Error near IL_0052: Stack underflow*/).Substring(startIndex);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\nţɐ\u0351јԎ\u065cݿ\u086dॵਠ\u0b79\u0c76൵\u0e67\u0f79ၻᅱች፦ᐾᕦᙦᝫ\u187a\u1923\u1a7b᭕ᱫ\u1d78ṳὩ\u202bⅼ≢⍯⑭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0027:
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
				((P_BPTO)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				btnReturn = (Button)_003F350_003F;
				btnReturn.Click += _003F143_003F;
				break;
			case 5:
				scrollmain = (ScrollViewer)_003F350_003F;
				break;
			case 6:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 7:
				stackPanel1 = (StackPanel)_003F350_003F;
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
			IL_004d:
			goto IL_0057;
		}
	}
}
