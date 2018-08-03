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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class P_FillIntSingle : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__21_0;

			internal int _003F315_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QBase oQuestion = new QBase();

		private QFill oQFill = new QFill();

		private QSingle oQSingle = new QSingle();

		private List<Button> listButton = new List<Button>();

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private double w_Height;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal RowDefinition RowNote;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal StackPanel stk1;

		internal TextBlock txtBefore;

		internal TextBox txtFill;

		internal TextBlock txtAfter;

		internal TextBlock txtQuestionNote;

		internal ScrollViewer scrollNote;

		internal StackPanel NoteArea;

		internal WrapPanel wrapButton;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public P_FillIntSingle()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_053c: Incompatible stack heights: 0 vs 1
			//IL_0543: Incompatible stack heights: 0 vs 1
			//IL_0d63: Incompatible stack heights: 0 vs 2
			//IL_0d7a: Incompatible stack heights: 0 vs 1
			//IL_0e94: Incompatible stack heights: 0 vs 1
			//IL_0eb3: Incompatible stack heights: 0 vs 1
			//IL_0eb8: Incompatible stack heights: 0 vs 1
			//IL_0f10: Incompatible stack heights: 0 vs 1
			//IL_0f2f: Incompatible stack heights: 0 vs 1
			//IL_0f34: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0);
			oQFill.Init(CurPageId, 1);
			oQSingle.Init(CurPageId, 2, true);
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
				oQFill.QuestionName += MyNav.QName_Add;
				oQSingle.QuestionName += MyNav.QName_Add;
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
				string text4 = list2[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_0544: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQFill.QDefine.CONTROL_TYPE > 0)
			{
				txtFill.MaxLength = oQFill.QDefine.CONTROL_TYPE;
				txtFill.Width = (double)oQFill.QDefine.CONTROL_TYPE * txtFill.FontSize * Math.Pow(0.955, (double)oQFill.QDefine.CONTROL_TYPE);
			}
			if (oQFill.QDefine.CONTROL_HEIGHT != 0)
			{
				txtFill.Height = (double)oQFill.QDefine.CONTROL_HEIGHT;
			}
			if (oQFill.QDefine.CONTROL_WIDTH != 0)
			{
				txtFill.Width = (double)oQFill.QDefine.CONTROL_WIDTH;
			}
			if (oQFill.QDefine.CONTROL_FONTSIZE > 0)
			{
				txtFill.FontSize = (double)oQFill.QDefine.CONTROL_FONTSIZE;
			}
			if (oQFill.QDefine.CONTROL_TOOLTIP != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore, qUESTION_TITLE, oQFill.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count > 1)
				{
					qUESTION_TITLE = list2[1];
					oBoldTitle.SetTextBlock(txtAfter, qUESTION_TITLE, oQFill.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				}
			}
			txtFill.Focus();
			List<SurveyDetail>.Enumerator enumerator;
			if (oQFill.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill.QDefine.NOTE;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtQuestionNote, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
				if (list2.Count > 1)
				{
					string text = _003F487_003F._003F488_003F("");
					int num = list2[1].IndexOf(_003F487_003F._003F488_003F("?"));
					if (num > 0)
					{
						text = _003F94_003F(list2[1], num + 1, -9999);
						num = _003F96_003F(_003F92_003F(list2[1], 1, num - 1));
					}
					else
					{
						text = list2[1];
					}
					if (oQFill.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F("") && num > 0)
					{
						oQFill.InitCircle();
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
							enumerator = oQFill.QCircleDetails.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									SurveyDetail current = enumerator.Current;
									if (current.CODE == text2)
									{
										text = current.EXTEND_1;
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
						}
					}
					if (text != _003F487_003F._003F488_003F(""))
					{
						string text3 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŋɠ\u0360Ѫգ\u065d") + text;
						if (_003F93_003F(text, 1) == _003F487_003F._003F488_003F("\""))
						{
							text3 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + _003F94_003F(text, 1, -9999);
						}
						else if (!File.Exists(text3))
						{
							text3 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + text;
						}
						Image image = new Image();
						if (num > 0)
						{
							image.Height = (double)num;
						}
						image.Stretch = Stretch.Uniform;
						image.Margin = new Thickness(0.0, 10.0, 20.0, 10.0);
						image.SetValue(Grid.ColumnProperty, 0);
						image.SetValue(Grid.RowProperty, 0);
						image.HorizontalAlignment = HorizontalAlignment.Center;
						image.VerticalAlignment = VerticalAlignment.Center;
						try
						{
							BitmapImage bitmapImage = new BitmapImage();
							bitmapImage.BeginInit();
							bitmapImage.UriSource = new Uri(text3, UriKind.RelativeOrAbsolute);
							bitmapImage.EndInit();
							image.Source = bitmapImage;
							NoteArea.Children.Add(image);
							wrapButton.Orientation = Orientation.Horizontal;
						}
						catch (Exception)
						{
						}
					}
				}
			}
			if (oQSingle.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
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
				string[] array = oLogicEngine.aryCode(oQSingle.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					enumerator = oQSingle.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							if (current2.CODE == array[i].ToString())
							{
								list3.Add(current2);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				if (_003F7_003F._003C_003E9__21_0 == null)
				{
					_003F7_003F._003C_003E9__21_0 = _003F7_003F._003C_003E9._003F315_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0d7f: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0d7f: Stack underflow*/);
				oQSingle.QDetails = list3;
			}
			if (oQSingle.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int j = 0; j < oQSingle.QDetails.Count(); j++)
				{
					oQSingle.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQSingle.QDetails[j].CODE_TEXT);
				}
			}
			if (oQSingle.QDefine.IS_RANDOM > 0)
			{
				oQSingle.RandomDetails();
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
			((P_FillIntSingle)/*Error near IL_0ebd: Stack underflow*/).Button_FontSize = (int)/*Error near IL_0ebd: Stack underflow*/;
			if (Button_FontSize == -1)
			{
				Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			Button_FontSize = Math.Abs(Button_FontSize);
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				int cONTROL_HEIGHT = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			else
			{
				int btnHeight = SurveyHelper.BtnHeight;
			}
			((P_FillIntSingle)/*Error near IL_0f39: Stack underflow*/).Button_Height = (int)/*Error near IL_0f39: Stack underflow*/;
			if (oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (Button_Type == 2 || Button_Type == 4)
				{
					Button_Width = 440.0;
				}
				else
				{
					Button_Width = (double)SurveyHelper.BtnWidth;
				}
			}
			else
			{
				Button_Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
			}
			_003F28_003F();
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				txtFill.Text = autoFill.FillInt(oQFill.QDefine);
				Button button = autoFill.SingleButton(oQSingle.QDefine, listButton);
				if (button != null)
				{
					_003F29_003F(button, new RoutedEventArgs());
					if (autoFill.AutoNext(oQuestion.QDefine))
					{
						_003F58_003F(this, _003F348_003F);
					}
				}
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				if (!(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")) && navOperation == _003F487_003F._003F488_003F("NŶɯͱ"))
				{
				}
			}
			else
			{
				txtFill.Text = oQFill.ReadAnswerByQuestionName(MySurveyId, oQFill.QuestionName);
				oQSingle.SelectedCode = oQSingle.ReadAnswerByQuestionName(MySurveyId, oQSingle.QuestionName);
				foreach (Button child in wrapButton.Children)
				{
					if ((string)child.Tag == oQSingle.SelectedCode)
					{
						child.Style = style;
						break;
					}
				}
			}
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
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_01fb: Incompatible stack heights: 0 vs 1
			//IL_020b: Incompatible stack heights: 0 vs 1
			//IL_021d: Incompatible stack heights: 0 vs 1
			//IL_0243: Incompatible stack heights: 0 vs 1
			//IL_0253: Incompatible stack heights: 0 vs 1
			//IL_0258: Invalid comparison between Unknown and I4
			//IL_0280: Incompatible stack heights: 0 vs 1
			//IL_029d: Incompatible stack heights: 0 vs 2
			//IL_02b2: Incompatible stack heights: 0 vs 1
			//IL_02b7: Invalid comparison between Unknown and I4
			//IL_02d2: Incompatible stack heights: 0 vs 2
			if (oQuestion.QDetails == null)
			{
				return;
			}
			QBase oQuestion2 = oQuestion;
			if (((QBase)/*Error near IL_0015: Stack underflow*/).QDetails.Count == 0)
			{
				return;
			}
			bool pageLoaded = PageLoaded;
			if ((int)/*Error near IL_0210: Stack underflow*/ == 0)
			{
				return;
			}
			WrapPanel wrapPanel = wrapButton;
			ScrollViewer scrollViewer = ((P_FillIntSingle)/*Error near IL_0029: Stack underflow*/).scrollNote;
			if (Button_Type < -20)
			{
				scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
				wrapPanel.Orientation = Orientation.Horizontal;
				wrapPanel.Width = (double)(-Button_Type);
				PageLoaded = false;
			}
			else if (Button_Type >= 1)
			{
				if (Button_Type <= 20)
				{
					if (Button_Type != 2)
					{
						int button_Type = Button_Type;
						if ((int)/*Error near IL_02b7: Stack underflow*/ != 4)
						{
							wrapPanel.Orientation = Orientation.Horizontal;
							goto IL_01c2;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					goto IL_01c2;
				}
				scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				((ScrollViewer)/*Error near IL_017d: Stack underflow*/).HorizontalScrollBarVisibility = (ScrollBarVisibility)/*Error near IL_017d: Stack underflow*/;
				wrapPanel.Orientation = Orientation.Vertical;
				wrapPanel.Height = (double)Button_Type;
				PageLoaded = false;
			}
			else
			{
				int button_Type2 = Button_Type;
				if ((int)/*Error near IL_0248: Stack underflow*/ == 0)
				{
					Visibility computedVerticalScrollBarVisibility = scrollViewer.ComputedVerticalScrollBarVisibility;
					if ((int)/*Error near IL_0258: Stack underflow*/ != 2)
					{
						int num = Convert.ToInt32(scrollViewer.ActualHeight / (double)(Button_Height + 15));
						int num2 = Convert.ToInt32((double)(oQuestion.QDetails.Count / num) + 0.99999999);
						int num3 = Convert.ToInt32(Convert.ToInt32(num * num2 - oQuestion.QDetails.Count) / num2);
						w_Height = wrapPanel.Height;
						wrapPanel.Height = (double)((num - num3) * (Button_Height + 15));
						scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
						scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						wrapPanel.Orientation = Orientation.Vertical;
						Button_Type = -1;
					}
					else
					{
						Button_Type = 2;
						PageLoaded = false;
					}
				}
				else if (scrollViewer.ComputedHorizontalScrollBarVisibility != Visibility.Collapsed)
				{
					wrapPanel.Height = w_Height;
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
					wrapPanel.Orientation = Orientation.Horizontal;
					Button_Type = 1;
					PageLoaded = false;
				}
				else
				{
					((P_FillIntSingle)/*Error near IL_012b: Stack underflow*/).Button_Type = 4;
					PageLoaded = false;
				}
			}
			goto IL_02f6;
			IL_02ef:
			PageLoaded = false;
			goto IL_02f6;
			IL_02e1:
			scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
			goto IL_02ef;
			IL_01e6:
			goto IL_02e1;
			IL_02f6:
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			return;
			IL_01c2:
			if (Button_Type != 3)
			{
				int button_Type3 = Button_Type;
				if (/*Error near IL_02d7: Stack underflow*/ != /*Error near IL_02d7: Stack underflow*/)
				{
					goto IL_02e1;
				}
			}
			scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
			scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
			goto IL_02ef;
		}

		private void _003F28_003F()
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapButton;
			foreach (SurveyDetail qDetail in oQSingle.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = qDetail.CODE;
				button.Click += _003F29_003F;
				button.FontSize = (double)Button_FontSize;
				button.MinWidth = Button_Width;
				button.MinHeight = (double)Button_Height;
				wrapPanel.Children.Add(button);
				listButton.Add(button);
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00c0: Incompatible stack heights: 0 vs 1
			//IL_00d0: Incompatible stack heights: 0 vs 1
			//IL_00d1: Incompatible stack heights: 0 vs 1
			Button button = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			string selectedCode = (string)button.Tag;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				oQSingle.SelectedCode = selectedCode;
				foreach (Button child in wrapButton.Children)
				{
					if (child.Tag == button.Tag)
					{
					}
					((FrameworkElement)/*Error near IL_00d6: Stack underflow*/).Style = (Style)/*Error near IL_00d6: Stack underflow*/;
				}
			}
		}

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_002d: Incompatible stack heights: 0 vs 1
			//IL_0039: Incompatible stack heights: 0 vs 2
			if (_003F348_003F.Key == Key.Return)
			{
				Button btnNav2 = btnNav;
				if (((UIElement)/*Error near IL_0011: Stack underflow*/).IsEnabled)
				{
					((P_FillIntSingle)/*Error near IL_001c: Stack underflow*/)._003F58_003F((object)/*Error near IL_001c: Stack underflow*/, (RoutedEventArgs)_003F348_003F);
				}
			}
		}

		private void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_00c9: Incompatible stack heights: 0 vs 2
			//IL_00e9: Incompatible stack heights: 0 vs 2
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength <= 0)
			{
				return;
			}
			bool flag = false;
			string a = textBox.Text.Substring(offset, array[0].AddedLength).Trim();
			if (!(a == _003F487_003F._003F488_003F("")))
			{
				string b = _003F487_003F._003F488_003F((string)/*Error near IL_0072: Stack underflow*/);
				if (!((string)/*Error near IL_0077: Stack underflow*/ == b))
				{
					double result = 0.0;
					flag = !double.TryParse(textBox.Text, out result);
					goto IL_009f;
				}
			}
			flag = true;
			goto IL_009f;
			IL_009f:
			if (flag)
			{
				string text2 = textBox.Text;
				int startIndex = offset;
				int addedLength = array[0].AddedLength;
				string text = ((string)/*Error near IL_00f5: Stack underflow*/).Remove(startIndex, addedLength);
				((TextBox)/*Error near IL_00fa: Stack underflow*/).Text = text;
				textBox.Select(offset, 0);
			}
		}

		private bool _003F87_003F()
		{
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected I4, but got Unknown
			//IL_01f8: Incompatible stack heights: 0 vs 2
			//IL_0204: Incompatible stack heights: 0 vs 2
			//IL_0216: Incompatible stack heights: 0 vs 3
			//IL_022b: Incompatible stack heights: 0 vs 3
			//IL_0246: Incompatible stack heights: 0 vs 2
			//IL_024b: Invalid comparison between Unknown and F8
			//IL_0266: Incompatible stack heights: 0 vs 1
			//IL_0271: Incompatible stack heights: 0 vs 1
			//IL_0290: Incompatible stack heights: 0 vs 2
			//IL_02a0: Incompatible stack heights: 0 vs 1
			//IL_02d5: Incompatible stack heights: 0 vs 2
			if (oQSingle.SelectedCode == _003F487_003F._003F488_003F(""))
			{
				string msgNotSelected = SurveyMsg.MsgNotSelected;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0027: Stack underflow*/, (string)/*Error near IL_0027: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			string text = txtFill.Text;
			if (text.Length > 0)
			{
				int startIndex = ((string)/*Error near IL_0047: Stack underflow*/).Length - 1;
				if (((string)/*Error near IL_004f: Stack underflow*/).Substring(startIndex, 1) == _003F487_003F._003F488_003F("/"))
				{
					int length = text.Length;
					_003F val = /*Error near IL_0065: Stack underflow*/- 1;
					text = ((string)/*Error near IL_006a: Stack underflow*/).Substring((int)/*Error near IL_006a: Stack underflow*/, (int)val);
				}
			}
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill = SurveyMsg.MsgNotFill;
				string msgCaption2 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0087: Stack underflow*/, (string)/*Error near IL_0087: Stack underflow*/, (MessageBoxButton)/*Error near IL_0087: Stack underflow*/, MessageBoxImage.Hand);
				txtFill.Focus();
				return true;
			}
			if (oQFill.QDefine.MIN_COUNT > 0)
			{
				Convert.ToDouble(text);
				SurveyDefine qDefine = oQFill.QDefine;
				double num = (double)((SurveyDefine)/*Error near IL_00b1: Stack underflow*/).MIN_COUNT;
				if (!((double)/*Error near IL_024b: Stack underflow*/ >= num))
				{
					string msgFillNotSmall = SurveyMsg.MsgFillNotSmall;
					MessageBox.Show(string.Format(arg0: oQFill.QDefine.MIN_COUNT.ToString(), format: (string)/*Error near IL_00c3: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
			}
			if (oQFill.QDefine.MAX_COUNT > 0 && Convert.ToDouble((string)/*Error near IL_00fa: Stack underflow*/) > (double)oQFill.QDefine.MAX_COUNT)
			{
				string msgFillNotBig = SurveyMsg.MsgFillNotBig;
				int mAX_COUNT = oQFill.QDefine.MAX_COUNT;
				MessageBox.Show(string.Format(arg0: ((int)/*Error near IL_0110: Stack underflow*/).ToString(), format: (string)/*Error near IL_011d: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				txtFill.Focus();
				return true;
			}
			if (oQFill.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
			{
				QFill oQFill2 = oQFill;
				string text2 = ((QFill)/*Error near IL_0162: Stack underflow*/).QDefine.CONTROL_MASK;
				string arg2 = text2.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
				if (text2.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
				{
					text2 += _003F487_003F._003F488_003F(".ı");
				}
				if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text2 + _003F487_003F._003F488_003F("(")))
				{
					string.Format(SurveyMsg.MsgFillFit, arg2);
					string msgCaption3 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_02dc: Stack underflow*/, (string)/*Error near IL_02dc: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
			}
			oQFill.FillText = text;
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQFill.QuestionName;
			vEAnswer.CODE = oQFill.FillText;
			list.Add(vEAnswer);
			SurveyHelper.Answer = oQFill.QuestionName + _003F487_003F._003F488_003F("<") + oQFill.FillText;
			VEAnswer vEAnswer2 = new VEAnswer();
			vEAnswer2.QUESTION_NAME = oQSingle.QuestionName;
			vEAnswer2.CODE = oQSingle.SelectedCode;
			list.Add(vEAnswer2);
			SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + oQSingle.QuestionName + _003F487_003F._003F488_003F("<") + oQSingle.SelectedCode;
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			oQSingle.BeforeSave();
			oQSingle.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			oQFill.BeforeSave();
			oQFill.Save(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00c0: Incompatible stack heights: 0 vs 2
			//IL_00d6: Incompatible stack heights: 0 vs 2
			//IL_00eb: Incompatible stack heights: 0 vs 2
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				Button btnNav2 = btnNav;
				string content = ((P_FillIntSingle)/*Error near IL_0040: Stack underflow*/).btnNav_Content;
				((ContentControl)/*Error near IL_0045: Stack underflow*/).Content = content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					Button btnNav3 = btnNav;
					string btnNav_Content2 = btnNav_Content;
					((ContentControl)/*Error near IL_0085: Stack underflow*/).Content = (object)/*Error near IL_0085: Stack underflow*/;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_009f: Stack underflow*/, (string)/*Error near IL_009f: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00aa:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0026: Incompatible stack heights: 0 vs 1
			if (SecondsCountDown == 0)
			{
				DispatcherTimer timer2 = timer;
				((DispatcherTimer)/*Error near IL_0010: Stack underflow*/).Stop();
				btnNav.Foreground = Brushes.Black;
				btnNav.Content = btnNav_Content;
			}
			else
			{
				SecondsCountDown--;
				btnNav.Content = SecondsCountDown.ToString();
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

		private string _003F92_003F(string _003F362_003F, int _003F363_003F, int _003F364_003F = -9999)
		{
			//IL_0095: Incompatible stack heights: 0 vs 1
			//IL_009a: Incompatible stack heights: 1 vs 0
			//IL_00a5: Incompatible stack heights: 0 vs 1
			//IL_00aa: Incompatible stack heights: 1 vs 0
			//IL_00b5: Incompatible stack heights: 0 vs 1
			//IL_00ba: Incompatible stack heights: 1 vs 0
			//IL_00c5: Incompatible stack heights: 0 vs 1
			//IL_00ca: Incompatible stack heights: 1 vs 0
			//IL_00d5: Incompatible stack heights: 0 vs 1
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
			//IL_005e: Incompatible stack heights: 0 vs 1
			//IL_0075: Incompatible stack heights: 0 vs 1
			//IL_007a: Incompatible stack heights: 1 vs 0
			//IL_007f: Incompatible stack heights: 0 vs 2
			//IL_0085: Incompatible stack heights: 0 vs 1
			//IL_008b: Incompatible stack heights: 0 vs 1
			int num = _003F365_003F;
			if (num == -9999)
			{
				int length2 = _003F362_003F.Length;
				num = (int)/*Error near IL_000e: Stack underflow*/;
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
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Incompatible stack heights: 0 vs 1
			//IL_0038: Incompatible stack heights: 1 vs 0
			//IL_003d: Incompatible stack heights: 0 vs 1
			//IL_0049: Incompatible stack heights: 0 vs 2
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int startIndex;
			if (num <= _003F362_003F.Length)
			{
				int length = _003F362_003F.Length;
				startIndex = /*Error near IL_001c: Stack underflow*/- /*Error near IL_001c: Stack underflow*/;
			}
			else
			{
				startIndex = 0;
			}
			return ((string)/*Error near IL_0027: Stack underflow*/).Substring(startIndex);
		}

		private int _003F96_003F(string _003F362_003F)
		{
			if (_003F362_003F == _003F487_003F._003F488_003F(""))
			{
				return 0;
			}
			goto IL_0015;
			IL_0058:
			goto IL_0015;
			IL_0015:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_002a;
			IL_0064:
			goto IL_002a;
			IL_002a:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_003f;
			IL_0070:
			goto IL_003f;
			IL_003f:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_004b;
			IL_007c:
			goto IL_004b;
			IL_004b:
			return Convert.ToInt32(_003F362_003F);
		}

		private bool _003F97_003F(string _003F366_003F)
		{
			return new Regex(_003F487_003F._003F488_003F("Kļɏ\u033fѭՌؤܧ࠲ॐ੯ଡడ\u0d54ษཚၡᄯሪጽᐥ")).IsMatch(_003F366_003F);
		}

		private void _003F85_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyHelper.AttachSurveyId = MySurveyId;
			SurveyHelper.AttachQName = oQFill.QuestionName;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0001Ūɟ\u0358ѓԇ٫\u0746ࡖ\u094cਟ\u0b40\u0c4d\u0d4c๐\u0f70ၰᅸቲ፯ᐵᕯᙱ\u1772ᡡ\u193a\u1a64\u1b4cᱴ\u1d78Ṽὣ\u2067Ⅳ≸⍸④╧♯❫⡣⤫⩼⭢Ɐ\u2d6d"), UriKind.Relative);
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
				((P_FillIntSingle)_003F350_003F).Loaded += _003F80_003F;
				((P_FillIntSingle)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				RowNote = (RowDefinition)_003F350_003F;
				break;
			case 3:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 5:
				stk1 = (StackPanel)_003F350_003F;
				break;
			case 6:
				txtBefore = (TextBlock)_003F350_003F;
				break;
			case 7:
				txtFill = (TextBox)_003F350_003F;
				txtFill.TextChanged += _003F98_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				txtFill.PreviewKeyDown += _003F86_003F;
				break;
			case 8:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 9:
				txtQuestionNote = (TextBlock)_003F350_003F;
				break;
			case 10:
				scrollNote = (ScrollViewer)_003F350_003F;
				break;
			case 11:
				NoteArea = (StackPanel)_003F350_003F;
				break;
			case 12:
				wrapButton = (WrapPanel)_003F350_003F;
				break;
			case 13:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 14:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
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
