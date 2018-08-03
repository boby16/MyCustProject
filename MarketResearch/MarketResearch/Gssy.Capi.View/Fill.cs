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
	public class Fill : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__15_0;

			internal int _003F297_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

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

		public Fill()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_04c6: Incompatible stack heights: 0 vs 1
			//IL_04cd: Incompatible stack heights: 0 vs 1
			//IL_086e: Incompatible stack heights: 0 vs 2
			//IL_0885: Incompatible stack heights: 0 vs 1
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
			qUESTION_TITLE = (string)/*Error near IL_04ce: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				txtFill.MaxLength = oQuestion.QDefine.CONTROL_TYPE;
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
					if (_003F7_003F._003C_003E9__15_0 == null)
					{
						_003F7_003F._003C_003E9__15_0 = _003F7_003F._003C_003E9._003F297_003F;
					}
					((List<SurveyDetail>)/*Error near IL_088a: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_088a: Stack underflow*/);
					oQuestion.QDetails = list3;
				}
				if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
				{
					for (int j = 0; j < oQuestion.QDetails.Count(); j++)
					{
						oQuestion.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[j].CODE_TEXT);
					}
				}
				Button_Width = (double)SurveyHelper.BtnWidth;
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
					txtFill.Text = autoFill.Fill(oQuestion.QDefine);
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

		private void _003F28_003F()
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapOther;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 10.0, 10.0, 10.0);
				button.Style = style;
				button.Tag = qDetail.IS_OTHER;
				button.Click += _003F29_003F;
				button.FontSize = (double)Button_FontSize;
				button.MinWidth = Button_Width;
				button.MinHeight = (double)Button_Height;
				wrapPanel.Children.Add(button);
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0058: Incompatible stack heights: 0 vs 2
			//IL_006e: Incompatible stack heights: 0 vs 2
			Button obj = (Button)_003F347_003F;
			string text = (string)obj.Content;
			if ((int)obj.Tag == 0)
			{
				txtFill.Text.IndexOf(text);
				if (/*Error near IL_005d: Stack underflow*/ == /*Error near IL_005d: Stack underflow*/)
				{
					string text3 = txtFill.Text;
					string text2 = (string)/*Error near IL_002c: Stack underflow*/ + text;
					((TextBox)/*Error near IL_0031: Stack underflow*/).Text = text2;
				}
			}
			else
			{
				txtFill.Text = text;
			}
			txtFill.Focus();
		}

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_002c: Incompatible stack heights: 0 vs 1
			//IL_0039: Incompatible stack heights: 0 vs 3
			if (_003F348_003F.Key == Key.Return)
			{
				bool isEnabled = btnNav.IsEnabled;
				if ((int)/*Error near IL_0031: Stack underflow*/ != 0)
				{
					((Fill)/*Error near IL_0016: Stack underflow*/)._003F58_003F((object)/*Error near IL_0016: Stack underflow*/, (RoutedEventArgs)/*Error near IL_0016: Stack underflow*/);
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_004c: Incompatible stack heights: 0 vs 1
			//IL_0061: Incompatible stack heights: 0 vs 3
			string text = txtFill.Text.Trim();
			if (txtFill.IsEnabled)
			{
				bool flag = text == _003F487_003F._003F488_003F("");
				if ((int)/*Error near IL_0051: Stack underflow*/ != 0)
				{
					string msgNotFill = SurveyMsg.MsgNotFill;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_002d: Stack underflow*/, (string)/*Error near IL_002d: Stack underflow*/, (MessageBoxButton)/*Error near IL_002d: Stack underflow*/, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
				text = oQuestion.ConvertText(text, oQuestion.QDefine.CONTROL_MASK);
				txtFill.Text = text;
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

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00d4: Incompatible stack heights: 0 vs 2
			//IL_00ec: Incompatible stack heights: 0 vs 4
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				btnNav.Content = btnNav_Content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					Button btnNav2 = btnNav;
					string content = ((Fill)/*Error near IL_007b: Stack underflow*/).btnNav_Content;
					((ContentControl)/*Error near IL_0080: Stack underflow*/).Content = content;
				}
				else
				{
					_003F89_003F();
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_0096: Stack underflow*/, (string)/*Error near IL_0096: Stack underflow*/, (MessageBoxButton)/*Error near IL_0096: Stack underflow*/, (MessageBoxImage)/*Error near IL_0096: Stack underflow*/);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00a3:
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
			//IL_0098: Incompatible stack heights: 0 vs 1
			//IL_009d: Incompatible stack heights: 1 vs 0
			//IL_00a8: Incompatible stack heights: 0 vs 1
			//IL_00ad: Incompatible stack heights: 1 vs 0
			//IL_00b8: Incompatible stack heights: 0 vs 1
			//IL_00bd: Incompatible stack heights: 1 vs 0
			//IL_00c8: Incompatible stack heights: 0 vs 1
			//IL_00cd: Incompatible stack heights: 1 vs 0
			//IL_00d8: Incompatible stack heights: 0 vs 1
			//IL_00dd: Incompatible stack heights: 1 vs 0
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
			int num6;
			if (_003F364_003F > _003F362_003F.Length)
			{
				num6 = _003F362_003F.Length - 1;
			}
			num = num6;
			return _003F362_003F.Substring(num4, num - num4 + 1);
		}

		private string _003F93_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_0031: Incompatible stack heights: 0 vs 1
			//IL_0036: Incompatible stack heights: 1 vs 0
			//IL_003b: Incompatible stack heights: 0 vs 2
			//IL_0041: Incompatible stack heights: 0 vs 1
			//IL_004c: Incompatible stack heights: 0 vs 1
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				goto IL_0046;
			}
			goto IL_004c;
			IL_0046:
			int length = _003F362_003F.Length;
			goto IL_004c;
			IL_004c:
			return ((string)/*Error near IL_0051: Stack underflow*/).Substring((int)/*Error near IL_0051: Stack underflow*/, (int)/*Error near IL_0051: Stack underflow*/);
			IL_0021:
			goto IL_0046;
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
			//IL_006e: Incompatible stack heights: 0 vs 1
			//IL_0073: Incompatible stack heights: 1 vs 0
			//IL_0078: Incompatible stack heights: 0 vs 2
			//IL_007e: Incompatible stack heights: 0 vs 1
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
				goto IL_0083;
			}
			goto IL_008b;
			IL_0083:
			int num3 = _003F362_003F.Length - num2;
			goto IL_008b;
			IL_008b:
			return ((string)/*Error near IL_0090: Stack underflow*/).Substring((int)/*Error near IL_0090: Stack underflow*/, (int)/*Error near IL_0090: Stack underflow*/);
			IL_0041:
			goto IL_0083;
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

		public string CleanString(string _003F367_003F)
		{
			return _003F367_003F.Replace(_003F487_003F._003F488_003F("、"), _003F487_003F._003F488_003F("!")).Trim().Replace('\n', ' ')
				.Replace('\r', ' ')
				.Replace('\t', ' ')
				.Replace(_003F487_003F._003F488_003F("Ａ"), _003F487_003F._003F488_003F("A"))
				.Replace(_003F487_003F._003F488_003F("１"), _003F487_003F._003F488_003F("1"))
				.Replace(_003F487_003F._003F488_003F("０"), _003F487_003F._003F488_003F("0"))
				.Replace(_003F487_003F._003F488_003F("３"), _003F487_003F._003F488_003F("3"))
				.Replace(_003F487_003F._003F488_003F("２"), _003F487_003F._003F488_003F("2"))
				.Replace(_003F487_003F._003F488_003F("５"), _003F487_003F._003F488_003F("5"))
				.Replace(_003F487_003F._003F488_003F("４"), _003F487_003F._003F488_003F("4"))
				.Replace(_003F487_003F._003F488_003F("７"), _003F487_003F._003F488_003F("7"))
				.Replace(_003F487_003F._003F488_003F("６"), _003F487_003F._003F488_003F("6"))
				.Replace(_003F487_003F._003F488_003F("９"), _003F487_003F._003F488_003F("9"))
				.Replace(_003F487_003F._003F488_003F("８"), _003F487_003F._003F488_003F("8"))
				.Replace(_003F487_003F._003F488_003F("＠"), _003F487_003F._003F488_003F("@"))
				.Replace(_003F487_003F._003F488_003F("Ｃ"), _003F487_003F._003F488_003F("C"))
				.Replace(_003F487_003F._003F488_003F("Ｂ"), _003F487_003F._003F488_003F("B"))
				.Replace(_003F487_003F._003F488_003F("Ｅ"), _003F487_003F._003F488_003F("E"))
				.Replace(_003F487_003F._003F488_003F("Ｄ"), _003F487_003F._003F488_003F("D"))
				.Replace(_003F487_003F._003F488_003F("Ｇ"), _003F487_003F._003F488_003F("G"))
				.Replace(_003F487_003F._003F488_003F("Ｆ"), _003F487_003F._003F488_003F("F"))
				.Replace(_003F487_003F._003F488_003F("Ｉ"), _003F487_003F._003F488_003F("I"))
				.Replace(_003F487_003F._003F488_003F("Ｈ"), _003F487_003F._003F488_003F("H"))
				.Replace(_003F487_003F._003F488_003F("Ｋ"), _003F487_003F._003F488_003F("K"))
				.Replace(_003F487_003F._003F488_003F("Ｊ"), _003F487_003F._003F488_003F("J"))
				.Replace(_003F487_003F._003F488_003F("Ｍ"), _003F487_003F._003F488_003F("M"))
				.Replace(_003F487_003F._003F488_003F("Ｌ"), _003F487_003F._003F488_003F("L"))
				.Replace(_003F487_003F._003F488_003F("Ｏ"), _003F487_003F._003F488_003F("O"))
				.Replace(_003F487_003F._003F488_003F("Ｎ"), _003F487_003F._003F488_003F("N"))
				.Replace(_003F487_003F._003F488_003F("Ｑ"), _003F487_003F._003F488_003F("Q"))
				.Replace(_003F487_003F._003F488_003F("Ｐ"), _003F487_003F._003F488_003F("P"))
				.Replace(_003F487_003F._003F488_003F("Ｓ"), _003F487_003F._003F488_003F("S"))
				.Replace(_003F487_003F._003F488_003F("Ｒ"), _003F487_003F._003F488_003F("R"))
				.Replace(_003F487_003F._003F488_003F("Ｕ"), _003F487_003F._003F488_003F("U"))
				.Replace(_003F487_003F._003F488_003F("Ｔ"), _003F487_003F._003F488_003F("T"))
				.Replace(_003F487_003F._003F488_003F("Ｗ"), _003F487_003F._003F488_003F("W"))
				.Replace(_003F487_003F._003F488_003F("Ｖ"), _003F487_003F._003F488_003F("V"))
				.Replace(_003F487_003F._003F488_003F("Ｙ"), _003F487_003F._003F488_003F("Y"))
				.Replace(_003F487_003F._003F488_003F("Ｘ"), _003F487_003F._003F488_003F("X"))
				.Replace(_003F487_003F._003F488_003F("［"), _003F487_003F._003F488_003F("["))
				.Replace(_003F487_003F._003F488_003F("\uff40"), _003F487_003F._003F488_003F("`"))
				.Replace(_003F487_003F._003F488_003F("ｃ"), _003F487_003F._003F488_003F("c"))
				.Replace(_003F487_003F._003F488_003F("ｂ"), _003F487_003F._003F488_003F("b"))
				.Replace(_003F487_003F._003F488_003F("ｅ"), _003F487_003F._003F488_003F("e"))
				.Replace(_003F487_003F._003F488_003F("ｄ"), _003F487_003F._003F488_003F("d"))
				.Replace(_003F487_003F._003F488_003F("ｇ"), _003F487_003F._003F488_003F("g"))
				.Replace(_003F487_003F._003F488_003F("ｆ"), _003F487_003F._003F488_003F("f"))
				.Replace(_003F487_003F._003F488_003F("ｉ"), _003F487_003F._003F488_003F("i"))
				.Replace(_003F487_003F._003F488_003F("ｈ"), _003F487_003F._003F488_003F("h"))
				.Replace(_003F487_003F._003F488_003F("ｋ"), _003F487_003F._003F488_003F("k"))
				.Replace(_003F487_003F._003F488_003F("ｊ"), _003F487_003F._003F488_003F("j"))
				.Replace(_003F487_003F._003F488_003F("ｍ"), _003F487_003F._003F488_003F("m"))
				.Replace(_003F487_003F._003F488_003F("ｌ"), _003F487_003F._003F488_003F("l"))
				.Replace(_003F487_003F._003F488_003F("ｏ"), _003F487_003F._003F488_003F("o"))
				.Replace(_003F487_003F._003F488_003F("ｎ"), _003F487_003F._003F488_003F("n"))
				.Replace(_003F487_003F._003F488_003F("ｑ"), _003F487_003F._003F488_003F("q"))
				.Replace(_003F487_003F._003F488_003F("ｐ"), _003F487_003F._003F488_003F("p"))
				.Replace(_003F487_003F._003F488_003F("ｓ"), _003F487_003F._003F488_003F("s"))
				.Replace(_003F487_003F._003F488_003F("ｒ"), _003F487_003F._003F488_003F("r"))
				.Replace(_003F487_003F._003F488_003F("ｕ"), _003F487_003F._003F488_003F("u"))
				.Replace(_003F487_003F._003F488_003F("ｔ"), _003F487_003F._003F488_003F("t"))
				.Replace(_003F487_003F._003F488_003F("ｗ"), _003F487_003F._003F488_003F("w"))
				.Replace(_003F487_003F._003F488_003F("ｖ"), _003F487_003F._003F488_003F("v"))
				.Replace(_003F487_003F._003F488_003F("ｙ"), _003F487_003F._003F488_003F("y"))
				.Replace(_003F487_003F._003F488_003F("ｘ"), _003F487_003F._003F488_003F("x"))
				.Replace(_003F487_003F._003F488_003F("｛"), _003F487_003F._003F488_003F("{"));
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\fťɒ\u0353Ѧ\u0530\u065eݽ\u086bॳਢ\u0b7b౸ൻ\u0e65\u0f7bၽᅷቿ፤ᐠᕸᙤᝩ\u187c\u1925\u1a6f᭡ᱫ\u1d6aḫὼ\u2062Ⅿ≭"), UriKind.Relative);
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
				((Fill)_003F350_003F).Loaded += _003F80_003F;
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
