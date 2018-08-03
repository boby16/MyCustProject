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
	public class P_FillDec2 : Page, IComponentConnector
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

		private QFill oQFill1 = new QFill();

		private QFill oQFill2 = new QFill();

		private string SelectedValue;

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private bool PageEntry;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal RowDefinition RowNote;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal WrapPanel wrapFill;

		internal WrapPanel wrapFill1;

		internal TextBlock txtBefore1;

		internal TextBox txtFill1;

		internal TextBlock txtAfter1;

		internal WrapPanel wrapFill2;

		internal TextBlock txtBefore2;

		internal TextBox txtFill2;

		internal TextBlock txtAfter2;

		internal TextBlock txtQuestionNote;

		internal ScrollViewer scrollNote;

		internal StackPanel NoteArea;

		internal WrapPanel wrapButton;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public P_FillDec2()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_053b: Incompatible stack heights: 0 vs 1
			//IL_0542: Incompatible stack heights: 0 vs 1
			//IL_061b: Incompatible stack heights: 0 vs 1
			//IL_0622: Incompatible stack heights: 0 vs 1
			//IL_083f: Incompatible stack heights: 0 vs 1
			//IL_0846: Incompatible stack heights: 0 vs 1
			//IL_114e: Incompatible stack heights: 0 vs 2
			//IL_1165: Incompatible stack heights: 0 vs 1
			//IL_174d: Incompatible stack heights: 0 vs 1
			//IL_1754: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0);
			oQFill1.Init(CurPageId, 1);
			oQFill2.Init(CurPageId, 2);
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
				oQFill1.QuestionName += MyNav.QName_Add;
				oQFill2.QuestionName += MyNav.QName_Add;
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
				string text5 = list2[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_0543: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQFill1.QDefine.CONTROL_TOOLTIP != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill1.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore1, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text6 = list2[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_0623: Stack underflow*/;
				oBoldTitle.SetTextBlock(txtAfter1, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			if (oQFill1.QDefine.CONTROL_TYPE > 0)
			{
				txtFill1.MaxLength = oQFill1.QDefine.CONTROL_TYPE;
				txtFill1.Width = (double)oQFill1.QDefine.CONTROL_TYPE * txtFill1.FontSize * Math.Pow(0.955, (double)oQFill1.QDefine.CONTROL_TYPE);
			}
			if (oQFill1.QDefine.CONTROL_HEIGHT != 0)
			{
				txtFill1.Height = (double)oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (oQFill1.QDefine.CONTROL_WIDTH != 0)
			{
				txtFill1.Width = (double)oQFill1.QDefine.CONTROL_WIDTH;
			}
			if (oQFill1.QDefine.CONTROL_FONTSIZE > 0)
			{
				txtFill1.FontSize = (double)oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (oQFill2.QDefine.CONTROL_TOOLTIP != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill2.QDefine.CONTROL_TOOLTIP;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore2, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count <= 1)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					string text7 = list2[1];
				}
				qUESTION_TITLE = (string)/*Error near IL_0847: Stack underflow*/;
				oBoldTitle.SetTextBlock(txtAfter2, qUESTION_TITLE, oQFill1.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			}
			if (oQFill2.QDefine.CONTROL_TYPE > 0)
			{
				txtFill2.MaxLength = oQFill2.QDefine.CONTROL_TYPE;
				txtFill2.Width = (double)oQFill2.QDefine.CONTROL_TYPE * txtFill2.FontSize * Math.Pow(0.955, (double)oQFill2.QDefine.CONTROL_TYPE);
			}
			if (oQFill2.QDefine.CONTROL_HEIGHT != 0)
			{
				txtFill2.Height = (double)oQFill1.QDefine.CONTROL_HEIGHT;
			}
			if (oQFill2.QDefine.CONTROL_WIDTH != 0)
			{
				txtFill2.Width = (double)oQFill2.QDefine.CONTROL_WIDTH;
			}
			if (oQFill2.QDefine.CONTROL_FONTSIZE > 0)
			{
				txtFill2.FontSize = (double)oQFill1.QDefine.CONTROL_FONTSIZE;
			}
			if (oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper() == _003F487_003F._003F488_003F("W"))
			{
				wrapFill.Orientation = Orientation.Vertical;
			}
			if (oQFill1.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				txtFill1.Text = oLogicEngine.stringResult(oQFill1.QDefine.PRESET_LOGIC);
				txtFill1.SelectAll();
			}
			if (oQFill2.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				txtFill2.Text = oLogicEngine.stringResult(oQFill2.QDefine.PRESET_LOGIC);
				txtFill2.SelectAll();
			}
			txtFill1.Focus();
			List<SurveyDetail>.Enumerator enumerator;
			if (oQFill1.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQFill1.QDefine.NOTE;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtQuestionNote, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
				if (list2.Count > 1)
				{
					string text = _003F487_003F._003F488_003F("");
					string text2 = _003F487_003F._003F488_003F("");
					int num = list2[1].IndexOf(_003F487_003F._003F488_003F("?"));
					if (num > 0)
					{
						text = _003F94_003F(list2[1], num + 1, -9999);
						text2 = _003F92_003F(list2[1], 1, num - 1);
						num = _003F96_003F(text2);
					}
					else
					{
						text = list2[1];
					}
					if (oQFill1.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F("") && num > 0)
					{
						oQFill1.InitCircle();
						string text3 = _003F487_003F._003F488_003F("");
						if (MyNav.GroupLevel == _003F487_003F._003F488_003F("@"))
						{
							text3 = MyNav.CircleACode;
						}
						if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
						{
							text3 = MyNav.CircleBCode;
						}
						if (text3 != _003F487_003F._003F488_003F(""))
						{
							enumerator = oQFill1.QCircleDetails.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									SurveyDetail current = enumerator.Current;
									if (current.CODE == text3)
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
						string text4 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŋɠ\u0360Ѫգ\u065d") + text;
						if (_003F93_003F(text, 1) == _003F487_003F._003F488_003F("\""))
						{
							text4 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + _003F94_003F(text, 1, -9999);
						}
						else if (!File.Exists(text4))
						{
							text4 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + text;
						}
						Image image = new Image();
						if (num > 0)
						{
							scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
							image.Height = (double)num;
						}
						else if (text2 == _003F487_003F._003F488_003F("\""))
						{
							scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
							scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						}
						else
						{
							scrollNote.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
							scrollNote.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
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
							bitmapImage.UriSource = new Uri(text4, UriKind.RelativeOrAbsolute);
							bitmapImage.EndInit();
							image.Source = bitmapImage;
							scrollNote.Content = image;
						}
						catch (Exception)
						{
						}
					}
				}
			}
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
					((List<SurveyDetail>)/*Error near IL_116a: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_116a: Stack underflow*/);
					oQuestion.QDetails = list3;
				}
				if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
				{
					for (int j = 0; j < oQuestion.QDetails.Count(); j++)
					{
						oQuestion.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[j].CODE_TEXT);
					}
				}
				Button_Height = SurveyHelper.BtnHeight;
				Button_FontSize = SurveyHelper.BtnFontSize;
				Button_Width = (double)SurveyHelper.BtnWidth;
				switch (oQuestion.QDefine.CONTROL_TYPE)
				{
				case 1:
					Button_Type = oQuestion.QDefine.CONTROL_TYPE;
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
					break;
				case 2:
					Button_Width = 440.0;
					Button_Type = oQuestion.QDefine.CONTROL_TYPE;
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
					break;
				case 20:
					Button_Height = SurveyHelper.BtnMediumHeight;
					Button_FontSize = SurveyHelper.BtnMediumFontSize;
					Button_Width = (double)SurveyHelper.BtnMediumWidth;
					break;
				case 30:
					Button_Height = SurveyHelper.BtnSmallHeight;
					Button_FontSize = SurveyHelper.BtnSmallFontSize;
					Button_Width = (double)SurveyHelper.BtnSmallWidth;
					break;
				}
				_003F28_003F();
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				if (txtFill1.Text == _003F487_003F._003F488_003F(""))
				{
					txtFill1.Text = autoFill.FillDec(oQFill1.QDefine);
				}
				if (txtFill2.Text == _003F487_003F._003F488_003F(""))
				{
					txtFill2.Text = autoFill.FillDec(oQFill2.QDefine);
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
				else if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && txtFill1.Text != _003F487_003F._003F488_003F("") && txtFill2.Text != _003F487_003F._003F488_003F("") && !SurveyHelper.AutoFill)
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			else
			{
				txtFill1.Text = oQFill1.ReadAnswerByQuestionName(MySurveyId, oQFill1.QuestionName);
				txtFill2.Text = oQFill2.ReadAnswerByQuestionName(MySurveyId, oQFill2.QuestionName);
				foreach (Button child in wrapButton.Children)
				{
					list2 = oBoldTitle.ParaToList((string)child.Tag, _003F487_003F._003F488_003F("\u007f"));
					string b = list2[0];
					if (list2.Count <= 1)
					{
						_003F487_003F._003F488_003F("");
					}
					else
					{
						string text8 = list2[1];
					}
					string b2 = (string)/*Error near IL_1756: Stack underflow*/;
					if (txtFill1.Text == b && txtFill2.Text == b2)
					{
						child.Style = style;
						txtFill1.Background = Brushes.LightGray;
						txtFill1.Foreground = Brushes.LightGray;
						txtFill2.Background = Brushes.LightGray;
						txtFill2.Foreground = Brushes.LightGray;
						txtFill1.IsEnabled = false;
						txtFill2.IsEnabled = false;
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
			txtFill1.SelectAll();
			txtFill1.Focus();
			PageLoaded = true;
			PageEntry = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0067: Incompatible stack heights: 0 vs 1
			//IL_0077: Incompatible stack heights: 0 vs 1
			//IL_0089: Incompatible stack heights: 0 vs 1
			//IL_0098: Incompatible stack heights: 0 vs 1
			//IL_009e: Incompatible stack heights: 0 vs 1
			//IL_00a4: Incompatible stack heights: 0 vs 1
			if (!PageLoaded)
			{
				return;
			}
			WrapPanel wrapButton2 = wrapButton;
			WrapPanel wrapPanel = (WrapPanel)/*Error near IL_000c: Stack underflow*/;
			if (Button_Type != 0)
			{
				if (Button_Type == 2)
				{
					goto IL_00a3;
				}
				goto IL_00a4;
			}
			ScrollViewer scrollNote2 = scrollNote;
			if (((ScrollViewer)/*Error near IL_001c: Stack underflow*/).ComputedVerticalScrollBarVisibility != Visibility.Collapsed)
			{
				wrapPanel.Orientation = Orientation.Horizontal;
				Button_Type = 1;
			}
			else
			{
				wrapPanel.Orientation = Orientation.Vertical;
				((P_FillDec2)/*Error near IL_0028: Stack underflow*/).Button_Type = 2;
			}
			goto IL_00a9;
			IL_00a3:
			goto IL_00a4;
			IL_0052:
			goto IL_00a3;
			IL_00a4:
			((WrapPanel)/*Error near IL_00a9: Stack underflow*/).Orientation = (Orientation)/*Error near IL_00a9: Stack underflow*/;
			goto IL_00a9;
			IL_00a9:
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			PageLoaded = false;
		}

		private void _003F28_003F()
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapButton;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = qDetail.EXTEND_1 + _003F487_003F._003F488_003F("\u007f") + qDetail.EXTEND_2;
				button.Click += _003F29_003F;
				button.FontSize = (double)Button_FontSize;
				button.MinWidth = Button_Width;
				button.MinHeight = (double)Button_Height;
				wrapPanel.Children.Add(button);
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_008c: Incompatible stack heights: 0 vs 1
			//IL_0093: Incompatible stack heights: 0 vs 1
			//IL_01c7: Incompatible stack heights: 0 vs 1
			//IL_01d7: Incompatible stack heights: 0 vs 1
			//IL_01d8: Incompatible stack heights: 0 vs 1
			Button button = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			List<string> list = oBoldTitle.ParaToList((string)button.Tag, _003F487_003F._003F488_003F("\u007f"));
			string text = list[0];
			if (list.Count <= 1)
			{
				_003F487_003F._003F488_003F("");
			}
			else
			{
				string text3 = list[1];
			}
			string text2 = (string)/*Error near IL_0095: Stack underflow*/;
			int num = 0;
			if (button.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				if (txtFill1.IsEnabled)
				{
					txtFill1.Tag = txtFill1.Text;
					txtFill1.Background = Brushes.LightGray;
					txtFill1.Foreground = Brushes.LightGray;
					txtFill1.IsEnabled = false;
					txtFill2.Tag = txtFill2.Text;
					txtFill2.Background = Brushes.LightGray;
					txtFill2.Foreground = Brushes.LightGray;
					txtFill2.IsEnabled = false;
				}
				txtFill1.Text = text;
				txtFill2.Text = text2;
				foreach (Button child in wrapButton.Children)
				{
					if (child.Name == button.Name)
					{
					}
					((FrameworkElement)/*Error near IL_01dd: Stack underflow*/).Style = (Style)/*Error near IL_01dd: Stack underflow*/;
				}
			}
			else
			{
				txtFill1.Text = (string)txtFill1.Tag;
				txtFill1.IsEnabled = true;
				txtFill1.Background = Brushes.White;
				txtFill1.Foreground = Brushes.Black;
				txtFill2.Text = (string)txtFill2.Tag;
				txtFill2.IsEnabled = true;
				txtFill2.Background = Brushes.White;
				txtFill2.Foreground = Brushes.Black;
				button.Style = style2;
				txtFill1.Focus();
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
					((P_FillDec2)/*Error near IL_001c: Stack underflow*/)._003F58_003F((object)/*Error near IL_001c: Stack underflow*/, (RoutedEventArgs)_003F348_003F);
				}
			}
		}

		private void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_0144: Incompatible stack heights: 0 vs 3
			//IL_0159: Incompatible stack heights: 0 vs 1
			//IL_0178: Incompatible stack heights: 0 vs 1
			//IL_0198: Incompatible stack heights: 0 vs 2
			//IL_019d: Invalid comparison between Unknown and I4
			//IL_01a3: Incompatible stack heights: 0 vs 1
			//IL_01be: Incompatible stack heights: 0 vs 2
			//IL_01c3: Invalid comparison between Unknown and I4
			//IL_01ca: Incompatible stack heights: 0 vs 2
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				bool flag = false;
				if (!(textBox.Text.Substring(offset, array[0].AddedLength).Trim() == _003F487_003F._003F488_003F("")))
				{
					double result = 0.0;
					flag = !double.TryParse(textBox.Text, out result);
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					string text2 = textBox.Text;
					int addedLength = array[0].AddedLength;
					string text = ((string)/*Error near IL_009d: Stack underflow*/).Remove((int)/*Error near IL_009d: Stack underflow*/, addedLength);
					((TextBox)/*Error near IL_00a2: Stack underflow*/).Text = text;
					textBox.Select(offset, 0);
				}
				else if (PageEntry)
				{
					SurveyDefine qDefine = oQuestion.QDefine;
					if (((SurveyDefine)/*Error near IL_00bb: Stack underflow*/).PAGE_COUNT_DOWN == -1)
					{
						bool flag2 = textBox.Name == _003F487_003F._003F488_003F("|ſɲ\u0343ѭկٮ\u0730");
						if ((int)/*Error near IL_017d: Stack underflow*/ != 0)
						{
							int length = textBox.Text.Length;
							SurveyDefine qDefine2 = oQFill1.QDefine;
							int cONTROL_TYPE = ((SurveyDefine)/*Error near IL_00cb: Stack underflow*/).CONTROL_TYPE;
							if ((int)/*Error near IL_019d: Stack underflow*/ == cONTROL_TYPE)
							{
								((P_FillDec2)/*Error near IL_00d5: Stack underflow*/).txtFill2.SelectAll();
								txtFill2.Focus();
								return;
							}
						}
						if (textBox.Name == _003F487_003F._003F488_003F("|ſɲ\u0343ѭկٮ\u0733"))
						{
							int length2 = textBox.Text.Length;
							QFill oQFill3 = oQFill2;
							int cONTROL_TYPE2 = ((QFill)/*Error near IL_0106: Stack underflow*/).QDefine.CONTROL_TYPE;
							if ((int)/*Error near IL_01c3: Stack underflow*/ == cONTROL_TYPE2)
							{
								((P_FillDec2)/*Error near IL_01d0: Stack underflow*/)._003F58_003F((object)/*Error near IL_01d0: Stack underflow*/, (RoutedEventArgs)null);
							}
						}
					}
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_023f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0245: Expected I4, but got Unknown
			//IL_025a: Unknown result type (might be due to invalid IL or missing references)
			//IL_025f: Expected I4, but got Unknown
			//IL_0614: Incompatible stack heights: 0 vs 2
			//IL_0620: Incompatible stack heights: 0 vs 2
			//IL_0637: Incompatible stack heights: 0 vs 4
			//IL_0663: Incompatible stack heights: 0 vs 2
			//IL_0668: Invalid comparison between Unknown and F8
			//IL_0683: Incompatible stack heights: 0 vs 1
			//IL_068e: Incompatible stack heights: 0 vs 1
			//IL_0693: Invalid comparison between Unknown and F8
			//IL_06ad: Incompatible stack heights: 0 vs 2
			//IL_06bd: Incompatible stack heights: 0 vs 1
			//IL_06f5: Incompatible stack heights: 0 vs 2
			//IL_0707: Incompatible stack heights: 0 vs 3
			//IL_071a: Incompatible stack heights: 0 vs 4
			//IL_0729: Incompatible stack heights: 0 vs 1
			//IL_0750: Incompatible stack heights: 0 vs 2
			//IL_0755: Invalid comparison between Unknown and F8
			//IL_076a: Incompatible stack heights: 0 vs 2
			//IL_0775: Incompatible stack heights: 0 vs 1
			//IL_077a: Invalid comparison between Unknown and F8
			//IL_078a: Incompatible stack heights: 0 vs 2
			//IL_079a: Incompatible stack heights: 0 vs 1
			//IL_07b5: Incompatible stack heights: 0 vs 1
			//IL_07d8: Incompatible stack heights: 0 vs 1
			//IL_07e8: Incompatible stack heights: 0 vs 1
			//IL_080c: Incompatible stack heights: 0 vs 2
			//IL_0826: Incompatible stack heights: 0 vs 1
			//IL_0859: Incompatible stack heights: 0 vs 2
			//IL_0870: Incompatible stack heights: 0 vs 4
			//IL_087c: Incompatible stack heights: 0 vs 2
			//IL_0898: Incompatible stack heights: 0 vs 1
			//IL_08a4: Incompatible stack heights: 0 vs 2
			//IL_08b3: Incompatible stack heights: 0 vs 1
			//IL_08cb: Incompatible stack heights: 0 vs 1
			//IL_08df: Incompatible stack heights: 0 vs 2
			string text = txtFill1.Text;
			double num = 0.0;
			if (text.Length > 0)
			{
				int startIndex = ((string)/*Error near IL_0027: Stack underflow*/).Length - 1;
				if (((string)/*Error near IL_002f: Stack underflow*/).Substring(startIndex, 1) == _003F487_003F._003F488_003F("/"))
				{
					int length = text.Length - 1;
					text = ((string)/*Error near IL_0050: Stack underflow*/).Substring((int)/*Error near IL_0050: Stack underflow*/, length);
				}
			}
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill = SurveyMsg.MsgNotFill;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_006b: Stack underflow*/, (string)/*Error near IL_006b: Stack underflow*/, (MessageBoxButton)/*Error near IL_006b: Stack underflow*/, (MessageBoxImage)/*Error near IL_006b: Stack underflow*/);
				txtFill1.SelectAll();
				txtFill1.Focus();
				return true;
			}
			if (txtFill1.IsEnabled)
			{
				num = Convert.ToDouble(text);
				if (oQFill1.QDefine.MIN_COUNT > 0)
				{
					int mIN_COUNT = oQFill1.QDefine.MIN_COUNT;
					double num2 = (double)/*Error near IL_00ac: Stack underflow*/;
					if (!((double)/*Error near IL_0668: Stack underflow*/ >= num2))
					{
						string msgFillNotSmall = SurveyMsg.MsgFillNotSmall;
						MessageBox.Show(string.Format(arg0: oQFill1.QDefine.MIN_COUNT.ToString(), format: (string)/*Error near IL_00bd: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill1.SelectAll();
						txtFill1.Focus();
						return true;
					}
				}
				if (oQFill1.QDefine.MAX_COUNT > 0)
				{
					double num3 = (double)oQFill1.QDefine.MAX_COUNT;
					if (!((double)/*Error near IL_0693: Stack underflow*/ <= num3))
					{
						string msgFillNotBig = SurveyMsg.MsgFillNotBig;
						int mAX_COUNT = oQFill1.QDefine.MAX_COUNT;
						MessageBox.Show(string.Format(arg0: ((int)/*Error near IL_0110: Stack underflow*/).ToString(), format: (string)/*Error near IL_011d: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill1.SelectAll();
						txtFill1.Focus();
						return true;
					}
				}
				if (oQFill1.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
				{
					QFill oQFill3 = oQFill1;
					string text2 = ((QFill)/*Error near IL_016d: Stack underflow*/).QDefine.CONTROL_MASK;
					if (text2.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
					{
						text2 += _003F487_003F._003F488_003F(".ı");
					}
					string arg2 = text2.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
					if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text2 + _003F487_003F._003F488_003F("(")))
					{
						string.Format(SurveyMsg.MsgFillFit, arg2);
						string msgCaption2 = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_01f6: Stack underflow*/, (string)/*Error near IL_01f6: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill1.SelectAll();
						txtFill1.Focus();
						return true;
					}
				}
			}
			oQFill1.FillText = text;
			text = txtFill2.Text;
			double num4 = 0.0;
			if (text.Length > 0)
			{
				int length2 = text.Length;
				_003F val = /*Error near IL_023f: Stack underflow*/- /*Error near IL_023f: Stack underflow*/;
				if (((string)/*Error near IL_0245: Stack underflow*/).Substring((int)val, 1) == _003F487_003F._003F488_003F("/"))
				{
					int length3 = text.Length;
					_003F val2 = /*Error near IL_025a: Stack underflow*/- /*Error near IL_025a: Stack underflow*/;
					text = ((string)/*Error near IL_025f: Stack underflow*/).Substring((int)/*Error near IL_025f: Stack underflow*/, (int)val2);
				}
			}
			if (text == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill2 = SurveyMsg.MsgNotFill;
				MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_0282: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Hand);
				txtFill2.SelectAll();
				txtFill2.Focus();
				return true;
			}
			if (txtFill2.IsEnabled)
			{
				num4 = Convert.ToDouble(text);
				if (oQFill2.QDefine.MIN_COUNT > 0)
				{
					SurveyDefine qDefine = oQFill2.QDefine;
					double num5 = (double)((SurveyDefine)/*Error near IL_02c7: Stack underflow*/).MIN_COUNT;
					if (!((double)/*Error near IL_0755: Stack underflow*/ >= num5))
					{
						string msgFillNotSmall2 = SurveyMsg.MsgFillNotSmall;
						SurveyDefine qDefine2 = oQFill2.QDefine;
						MessageBox.Show(string.Format(arg0: ((SurveyDefine)/*Error near IL_02d2: Stack underflow*/).MIN_COUNT.ToString(), format: (string)/*Error near IL_02df: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill2.SelectAll();
						txtFill2.Focus();
						return true;
					}
				}
				if (oQFill2.QDefine.MAX_COUNT > 0)
				{
					double num6 = (double)oQFill2.QDefine.MAX_COUNT;
					if (!((double)/*Error near IL_077a: Stack underflow*/ <= num6))
					{
						string msgFillNotBig2 = SurveyMsg.MsgFillNotBig;
						QFill oQFill4 = oQFill2;
						MessageBox.Show(string.Format(arg0: ((QFill)/*Error near IL_0337: Stack underflow*/).QDefine.MAX_COUNT.ToString(), format: (string)/*Error near IL_0349: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill2.SelectAll();
						txtFill2.Focus();
						return true;
					}
				}
				if (oQFill2.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
				{
					QFill oQFill5 = oQFill2;
					string text3 = ((QFill)/*Error near IL_0399: Stack underflow*/).QDefine.CONTROL_MASK;
					if (text3.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
					{
						string text4 = text3 + _003F487_003F._003F488_003F(".ı");
						text3 = (string)/*Error near IL_03b9: Stack underflow*/;
					}
					string arg3 = text3.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
					if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text3 + _003F487_003F._003F488_003F("(")))
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgFillFit, arg3), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill2.SelectAll();
						txtFill2.Focus();
						return true;
					}
				}
			}
			oQFill2.FillText = text;
			if (txtFill1.IsEnabled)
			{
				QBase oQuestion2 = oQuestion;
				if (((QBase)/*Error near IL_0457: Stack underflow*/).QDefine.CONTROL_MASK != _003F487_003F._003F488_003F("8"))
				{
					string cONTROL_MASK = oQuestion.QDefine.CONTROL_MASK;
					_003F487_003F._003F488_003F("");
					if (!((string)/*Error near IL_0475: Stack underflow*/ == (string)/*Error near IL_0475: Stack underflow*/))
					{
						string cONTROL_MASK2 = oQuestion.QDefine.CONTROL_MASK;
						string b = _003F487_003F._003F488_003F("1");
						if (!((string)/*Error near IL_0489: Stack underflow*/ == b))
						{
							if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("0"))
							{
								if (/*Error near IL_085e: Stack underflow*/ > /*Error near IL_085e: Stack underflow*/)
								{
									string msgRightNotSmallLeft = SurveyMsg.MsgRightNotSmallLeft;
									string msgCaption3 = SurveyMsg.MsgCaption;
									MessageBox.Show((string)/*Error near IL_04dc: Stack underflow*/, (string)/*Error near IL_04dc: Stack underflow*/, (MessageBoxButton)/*Error near IL_04dc: Stack underflow*/, (MessageBoxImage)/*Error near IL_04dc: Stack underflow*/);
									txtFill1.SelectAll();
									txtFill1.Focus();
									return true;
								}
							}
							else if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("3"))
							{
								if (/*Error near IL_0881: Stack underflow*/ <= /*Error near IL_0881: Stack underflow*/)
								{
									MessageBox.Show(SurveyMsg.MsgRightSmallLeft, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									txtFill1.SelectAll();
									txtFill1.Focus();
									return true;
								}
							}
							else if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("2"))
							{
								if (/*Error near IL_08a9: Stack underflow*/ < /*Error near IL_08a9: Stack underflow*/)
								{
									string msgLeftNotSmallRight = SurveyMsg.MsgLeftNotSmallRight;
									MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_056f: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Hand);
									txtFill1.SelectAll();
									txtFill1.Focus();
									return true;
								}
							}
							else if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("9"))
							{
								DateTime date = DateTime.Now.Date;
								DateTime t = (DateTime)/*Error near IL_05af: Stack underflow*/;
								if (Convert.ToDateTime(txtFill1.Text + _003F487_003F._003F488_003F(",") + txtFill2.Text + _003F487_003F._003F488_003F(".ĲȰ")) > t)
								{
									string msgNotAfterYM = SurveyMsg.MsgNotAfterYM;
									string msgCaption4 = SurveyMsg.MsgCaption;
									MessageBox.Show((string)/*Error near IL_05f7: Stack underflow*/, (string)/*Error near IL_05f7: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
									txtFill1.SelectAll();
									txtFill1.Focus();
									return true;
								}
							}
							goto IL_08ed;
						}
					}
					if (num >= num4)
					{
						MessageBox.Show(SurveyMsg.MsgLeftSmallRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						txtFill1.SelectAll();
						txtFill1.Focus();
						return true;
					}
				}
			}
			goto IL_08ed;
			IL_08ed:
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQFill1.QuestionName;
			vEAnswer.CODE = oQFill1.FillText;
			list.Add(vEAnswer);
			SurveyHelper.Answer = oQFill1.QuestionName + _003F487_003F._003F488_003F("<") + oQFill1.FillText;
			vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQFill2.QuestionName;
			vEAnswer.CODE = oQFill2.FillText;
			list.Add(vEAnswer);
			SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + oQFill2.QuestionName + _003F487_003F._003F488_003F("<") + oQFill2.FillText;
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			oQFill1.BeforeSave();
			oQFill1.Save(MySurveyId, SurveyHelper.SurveySequence);
			oQFill2.BeforeSave();
			oQFill2.Save(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F58_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
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
				string content = ((P_FillDec2)/*Error near IL_0040: Stack underflow*/).btnNav_Content;
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
			SurveyHelper.AttachQName = oQFill1.QuestionName;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a7f᭑ᱫ\u1d65ṧὦ\u206dⅭ≤⌴\u242b╼♢❯⡭"), UriKind.Relative);
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
				((P_FillDec2)_003F350_003F).Loaded += _003F80_003F;
				((P_FillDec2)_003F350_003F).LayoutUpdated += _003F99_003F;
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
				wrapFill = (WrapPanel)_003F350_003F;
				break;
			case 6:
				wrapFill1 = (WrapPanel)_003F350_003F;
				break;
			case 7:
				txtBefore1 = (TextBlock)_003F350_003F;
				break;
			case 8:
				txtFill1 = (TextBox)_003F350_003F;
				txtFill1.TextChanged += _003F98_003F;
				txtFill1.GotFocus += _003F91_003F;
				txtFill1.LostFocus += _003F90_003F;
				txtFill1.PreviewKeyDown += _003F86_003F;
				break;
			case 9:
				txtAfter1 = (TextBlock)_003F350_003F;
				break;
			case 10:
				wrapFill2 = (WrapPanel)_003F350_003F;
				break;
			case 11:
				txtBefore2 = (TextBlock)_003F350_003F;
				break;
			case 12:
				txtFill2 = (TextBox)_003F350_003F;
				txtFill2.TextChanged += _003F98_003F;
				txtFill2.GotFocus += _003F91_003F;
				txtFill2.LostFocus += _003F90_003F;
				txtFill2.PreviewKeyDown += _003F86_003F;
				break;
			case 13:
				txtAfter2 = (TextBlock)_003F350_003F;
				break;
			case 14:
				txtQuestionNote = (TextBlock)_003F350_003F;
				break;
			case 15:
				scrollNote = (ScrollViewer)_003F350_003F;
				break;
			case 16:
				NoteArea = (StackPanel)_003F350_003F;
				break;
			case 17:
				wrapButton = (WrapPanel)_003F350_003F;
				break;
			case 18:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 19:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 20:
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
