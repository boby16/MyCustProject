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
	public class FillInt : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__17_0;

			internal int _003F299_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QFill oQuestion = new QFill();

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private bool PageLoaded;

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

		internal WrapPanel wrapOther;

		internal ScrollViewer scrollNote;

		internal StackPanel NoteArea;

		internal TextBlock txtQuestionNote;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public FillInt()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_04cb: Incompatible stack heights: 0 vs 1
			//IL_04d2: Incompatible stack heights: 0 vs 1
			//IL_0975: Incompatible stack heights: 0 vs 2
			//IL_098c: Incompatible stack heights: 0 vs 1
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
			qUESTION_TITLE = (string)/*Error near IL_04d3: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				txtFill.MaxLength = oQuestion.QDefine.CONTROL_TYPE;
				txtFill.Width = (double)oQuestion.QDefine.CONTROL_TYPE * txtFill.FontSize * Math.Pow(0.955, (double)oQuestion.QDefine.CONTROL_TYPE);
			}
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				txtFill.Height = (double)oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				txtFill.Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (oQuestion.QDefine.CONTROL_FONTSIZE > 0)
			{
				txtFill.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQuestion.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore, qUESTION_TITLE, oQuestion.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count > 1)
				{
					qUESTION_TITLE = list2[1];
					oBoldTitle.SetTextBlock(txtAfter, qUESTION_TITLE, oQuestion.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				}
			}
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				txtFill.Text = oLogicEngine.stringResult(oQuestion.QDefine.PRESET_LOGIC);
				txtFill.SelectAll();
			}
			txtFill.Focus();
			List<SurveyDetail>.Enumerator enumerator;
			if (oQuestion.QDefine.DETAIL_ID != _003F487_003F._003F488_003F(""))
			{
				if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
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
					string[] array = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list3 = new List<SurveyDetail>();
					for (int i = 0; i < array.Count(); i++)
					{
						enumerator = oQuestion.QDetails.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail current = enumerator.Current;
								if (current.CODE == array[i].ToString())
								{
									list3.Add(current);
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
					}
					if (_003F7_003F._003C_003E9__17_0 == null)
					{
						_003F7_003F._003C_003E9__17_0 = _003F7_003F._003C_003E9._003F299_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0991: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0991: Stack underflow*/);
					oQuestion.QDetails = list3;
				}
				if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
				{
					for (int j = 0; j < oQuestion.QDetails.Count(); j++)
					{
						oQuestion.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[j].CODE_TEXT);
					}
				}
				Button_Width = 200.0;
				Button_Height = SurveyHelper.BtnHeight;
				Button_FontSize = SurveyHelper.BtnFontSize;
				if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
				{
					Button_Height = oQuestion.QDefine.CONTROL_HEIGHT;
				}
				if (oQuestion.QDefine.CONTROL_WIDTH != 0)
				{
					Button_Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
				}
				if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
				{
					Button_FontSize = oQuestion.QDefine.CONTROL_FONTSIZE;
				}
				_003F28_003F();
			}
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQuestion.QDefine.NOTE;
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
					if (oQuestion.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F("") && num > 0)
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
							enumerator = oQuestion.QCircleDetails.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									SurveyDetail current2 = enumerator.Current;
									if (current2.CODE == text2)
									{
										text = current2.EXTEND_1;
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
						}
						catch (Exception)
						{
						}
					}
				}
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				if (txtFill.Text == _003F487_003F._003F488_003F(""))
				{
					txtFill.Text = autoFill.FillInt(oQuestion.QDefine);
				}
				if (autoFill.AutoNext(oQuestion.QDefine))
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				if (!(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")))
				{
					if (navOperation == _003F487_003F._003F488_003F("NŶɯͱ"))
					{
					}
				}
				else if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && txtFill.Text != _003F487_003F._003F488_003F("") && !SurveyHelper.AutoFill)
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			else
			{
				txtFill.Text = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName);
				foreach (Button child in wrapOther.Children)
				{
					string b = (string)child.Tag;
					if (txtFill.Text == b)
					{
						child.Style = style;
						txtFill.Background = Brushes.LightGray;
						txtFill.Foreground = Brushes.LightGray;
						txtFill.IsEnabled = false;
						break;
					}
				}
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
			txtFill.SelectAll();
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			ScrollViewer scrollViewer = scrollNote;
			if (scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
			{
				scrollViewer.PanningMode = PanningMode.Both;
			}
		}

		private void _003F28_003F()
		{
			//IL_00e8: Incompatible stack heights: 0 vs 1
			//IL_00fd: Incompatible stack heights: 0 vs 1
			//IL_0103: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapOther;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				if (qDetail.IS_OTHER != 0)
				{
					Button button = new Button();
					button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
					button.Content = qDetail.CODE_TEXT;
					button.Margin = new Thickness(0.0, 10.0, 15.0, 10.0);
					button.Style = style;
					if (!(qDetail.EXTEND_1 == _003F487_003F._003F488_003F("")))
					{
						string eXTEND_ = qDetail.EXTEND_1;
					}
					else
					{
						string cODE = qDetail.CODE;
					}
					((FrameworkElement)/*Error near IL_0108: Stack underflow*/).Tag = (object)/*Error near IL_0108: Stack underflow*/;
					button.Click += _003F29_003F;
					button.FontSize = (double)Button_FontSize;
					button.MinWidth = Button_Width;
					button.MinHeight = (double)Button_Height;
					wrapPanel.Children.Add(button);
				}
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0125: Incompatible stack heights: 0 vs 1
			//IL_0135: Incompatible stack heights: 0 vs 1
			//IL_0136: Incompatible stack heights: 0 vs 1
			Button button = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			string text = (string)button.Tag;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				if (txtFill.IsEnabled)
				{
					txtFill.Tag = txtFill.Text;
					txtFill.Background = Brushes.LightGray;
					txtFill.Foreground = Brushes.LightGray;
					txtFill.IsEnabled = false;
				}
				txtFill.Text = text;
				foreach (Button child in wrapOther.Children)
				{
					string a = (string)child.Tag;
					if (a == text)
					{
					}
					((FrameworkElement)/*Error near IL_013b: Stack underflow*/).Style = (Style)/*Error near IL_013b: Stack underflow*/;
				}
			}
			else
			{
				txtFill.Text = (string)txtFill.Tag;
				txtFill.IsEnabled = true;
				txtFill.Background = Brushes.White;
				txtFill.Foreground = Brushes.Black;
				button.Style = style2;
				txtFill.Focus();
			}
		}

		private bool _003F87_003F()
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected I4, but got Unknown
			//IL_01dc: Incompatible stack heights: 0 vs 2
			//IL_01e8: Incompatible stack heights: 0 vs 2
			//IL_01fc: Incompatible stack heights: 0 vs 2
			//IL_0217: Incompatible stack heights: 0 vs 2
			//IL_0238: Incompatible stack heights: 0 vs 2
			//IL_0248: Incompatible stack heights: 0 vs 2
			//IL_0253: Incompatible stack heights: 0 vs 1
			//IL_0263: Incompatible stack heights: 0 vs 2
			//IL_027d: Incompatible stack heights: 0 vs 1
			//IL_02b3: Incompatible stack heights: 0 vs 3
			string text = txtFill.Text;
			if (text.Length > 0)
			{
				int length2 = text.Length;
				_003F val = /*Error near IL_001a: Stack underflow*/- 1;
				if (((string)/*Error near IL_0020: Stack underflow*/).Substring((int)val, 1) == _003F487_003F._003F488_003F("/"))
				{
					int length = text.Length - 1;
					text = ((string)/*Error near IL_0041: Stack underflow*/).Substring((int)/*Error near IL_0041: Stack underflow*/, length);
				}
			}
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill = SurveyMsg.MsgNotFill;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_005f: Stack underflow*/, (string)/*Error near IL_005f: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
				txtFill.Focus();
				return true;
			}
			if (txtFill.IsEnabled)
			{
				int mIN_COUNT = oQuestion.QDefine.MIN_COUNT;
				if (/*Error near IL_021c: Stack underflow*/ > /*Error near IL_021c: Stack underflow*/)
				{
					Convert.ToDouble(text);
					double num = (double)oQuestion.QDefine.MIN_COUNT;
					if (/*Error near IL_023d: Stack underflow*/ < /*Error near IL_023d: Stack underflow*/)
					{
						string msgFillNotSmall = SurveyMsg.MsgFillNotSmall;
						MessageBox.Show(string.Format(arg0: ((FillInt)/*Error near IL_008d: Stack underflow*/).oQuestion.QDefine.MIN_COUNT.ToString(), format: (string)/*Error near IL_00a4: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill.Focus();
						return true;
					}
				}
				if (oQuestion.QDefine.MAX_COUNT > 0 && Convert.ToDouble((string)/*Error near IL_00db: Stack underflow*/) > (double)oQuestion.QDefine.MAX_COUNT)
				{
					string msgFillNotBig = SurveyMsg.MsgFillNotBig;
					MessageBox.Show(string.Format(arg0: ((FillInt)/*Error near IL_00f6: Stack underflow*/).oQuestion.QDefine.MAX_COUNT.ToString(), format: (string)/*Error near IL_010d: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
				if (oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
				{
					string cONTROL_MASK = oQuestion.QDefine.CONTROL_MASK;
					string text2 = (string)/*Error near IL_014e: Stack underflow*/;
					if (text2.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
					{
						text2 += _003F487_003F._003F488_003F(".ı");
					}
					string arg2 = text2.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
					if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text2 + _003F487_003F._003F488_003F("(")))
					{
						string.Format(SurveyMsg.MsgFillIntFit, arg2);
						string msgCaption2 = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_02b8: Stack underflow*/, (string)/*Error near IL_02b8: Stack underflow*/, (MessageBoxButton)/*Error near IL_02b8: Stack underflow*/, MessageBoxImage.Hand);
						txtFill.Focus();
						return true;
					}
				}
			}
			oQuestion.FillText = text;
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

		private void _003F89_003F()
		{
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_002d: Incompatible stack heights: 0 vs 1
			//IL_0039: Incompatible stack heights: 0 vs 2
			if (_003F348_003F.Key == Key.Return)
			{
				bool isEnabled = btnNav.IsEnabled;
				if ((int)/*Error near IL_0032: Stack underflow*/ != 0)
				{
					((FillInt)/*Error near IL_0017: Stack underflow*/)._003F58_003F((object)/*Error near IL_0017: Stack underflow*/, (RoutedEventArgs)_003F348_003F);
				}
			}
		}

		private void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_0108: Incompatible stack heights: 0 vs 1
			//IL_0119: Incompatible stack heights: 0 vs 2
			//IL_0133: Incompatible stack heights: 0 vs 1
			//IL_0143: Incompatible stack heights: 0 vs 1
			//IL_0158: Incompatible stack heights: 0 vs 1
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength <= 0)
			{
				return;
			}
			bool flag = false;
			string a = ((TextBox)/*Error near IL_0041: Stack underflow*/).Text.Substring(offset, array[0].AddedLength).Trim();
			if (!(a == _003F487_003F._003F488_003F("")))
			{
				string b = _003F487_003F._003F488_003F((string)/*Error near IL_0071: Stack underflow*/);
				if (!((string)/*Error near IL_0076: Stack underflow*/ == b))
				{
					double result = 0.0;
					flag = !double.TryParse(textBox.Text, out result);
					goto IL_009e;
				}
			}
			flag = true;
			goto IL_009e;
			IL_009e:
			if (flag)
			{
				string text = textBox.Text.Remove(offset, array[0].AddedLength);
				((TextBox)/*Error near IL_00bd: Stack underflow*/).Text = text;
				textBox.Select(offset, 0);
			}
			else if (textBox.Text.Length == oQuestion.QDefine.CONTROL_TYPE)
			{
				bool pageLoaded = PageLoaded;
				if ((int)/*Error near IL_0148: Stack underflow*/ != 0)
				{
					SurveyDefine qDefine = oQuestion.QDefine;
					if (((SurveyDefine)/*Error near IL_00f0: Stack underflow*/).PAGE_COUNT_DOWN == -1)
					{
						_003F58_003F(null, null);
					}
				}
			}
		}

		private void _003F58_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00bf: Incompatible stack heights: 0 vs 2
			//IL_00cf: Incompatible stack heights: 0 vs 1
			//IL_00ec: Incompatible stack heights: 0 vs 1
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
				string btnNav_Content2 = btnNav_Content;
				((ContentControl)/*Error near IL_0040: Stack underflow*/).Content = (object)/*Error near IL_0040: Stack underflow*/;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					Button btnNav3 = btnNav;
					string content = btnNav_Content;
					((ContentControl)/*Error near IL_0086: Stack underflow*/).Content = content;
				}
				else
				{
					_003F89_003F();
					if (SurveyHelper.Debug)
					{
						MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00a4:
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

		private int _003F96_003F(string _003F362_003F)
		{
			if (_003F362_003F == _003F487_003F._003F488_003F(""))
			{
				return 0;
			}
			goto IL_0015;
			IL_0057:
			goto IL_0015;
			IL_0015:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_002a;
			IL_0063:
			goto IL_002a;
			IL_002a:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_003f;
			IL_006f:
			goto IL_003f;
			IL_003f:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_0080;
			IL_007b:
			goto IL_0080;
			IL_0080:
			return Convert.ToInt32(_003F362_003F);
		}

		private bool _003F97_003F(string _003F366_003F)
		{
			return new Regex(_003F487_003F._003F488_003F("Kļɏ\u033fѭՌؤܧ࠲ॐ੯ଡడ\u0d54ษཚၡᄯሪጽᐥ")).IsMatch(_003F366_003F);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\tŢɗ\u0350ћԏ٣ݾ\u086eॴਧ\u0b78\u0c75൴\u0e68\u0f78ၸᅰቺ፧ᐽᕧᙹᝪ\u1879\u1922\u1a6a᭢ᱦ\u1d65ṡὩ\u2072Å≼⍢⑯╭"), UriKind.Relative);
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
				((FillInt)_003F350_003F).Loaded += _003F80_003F;
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
				wrapOther = (WrapPanel)_003F350_003F;
				break;
			case 10:
				scrollNote = (ScrollViewer)_003F350_003F;
				break;
			case 11:
				NoteArea = (StackPanel)_003F350_003F;
				break;
			case 12:
				txtQuestionNote = (TextBlock)_003F350_003F;
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
			return;
			IL_0049:
			goto IL_0053;
		}
	}
}
