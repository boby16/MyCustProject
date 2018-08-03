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
	public class P_Fill2 : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__20_0;

			internal int _003F322_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		public P_Fill2()
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
					if (_003F7_003F._003C_003E9__20_0 == null)
					{
						_003F7_003F._003C_003E9__20_0 = _003F7_003F._003C_003E9._003F322_003F;
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
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_006d: Incompatible stack heights: 0 vs 1
			//IL_0083: Incompatible stack heights: 0 vs 2
			//IL_00a3: Incompatible stack heights: 0 vs 1
			//IL_00a9: Incompatible stack heights: 0 vs 1
			//IL_00ae: Incompatible stack heights: 1 vs 0
			if (PageLoaded)
			{
				WrapPanel wrapPanel = wrapButton;
				if (((P_Fill2)/*Error near IL_0010: Stack underflow*/).Button_Type != 0)
				{
					if (Button_Type == 2)
					{
					}
					((WrapPanel)/*Error near IL_0051: Stack underflow*/).Orientation = Orientation.Vertical;
				}
				else
				{
					Visibility computedVerticalScrollBarVisibility = scrollNote.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_0088: Stack underflow*/ == /*Error near IL_0088: Stack underflow*/)
					{
						wrapPanel.Orientation = Orientation.Vertical;
						Button_Type = 2;
					}
					else
					{
						wrapPanel.Orientation = Orientation.Horizontal;
						Button_Type = 1;
					}
				}
				new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
				PageLoaded = false;
			}
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
				button.Tag = qDetail.EXTEND_1 + _003F487_003F._003F488_003F("#įȡ") + qDetail.EXTEND_2;
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
			List<string> list = oBoldTitle.ParaToList((string)button.Tag, _003F487_003F._003F488_003F("#įȡ"));
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
			//IL_002c: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 0 vs 1
			if (_003F348_003F.Key == Key.Return)
			{
				bool isEnabled = btnNav.IsEnabled;
				if ((int)/*Error near IL_0031: Stack underflow*/ != 0)
				{
					((P_Fill2)/*Error near IL_003d: Stack underflow*/)._003F58_003F(_003F347_003F, (RoutedEventArgs)_003F348_003F);
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_00b1: Incompatible stack heights: 0 vs 1
			//IL_00c8: Incompatible stack heights: 0 vs 4
			//IL_00dd: Incompatible stack heights: 0 vs 2
			//IL_00f2: Incompatible stack heights: 0 vs 3
			string text = txtFill1.Text;
			if (txtFill1.IsEnabled)
			{
				bool flag = text == _003F487_003F._003F488_003F("");
				if ((int)/*Error near IL_00b6: Stack underflow*/ != 0)
				{
					string msgNotFill = SurveyMsg.MsgNotFill;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_0026: Stack underflow*/, (string)/*Error near IL_0026: Stack underflow*/, (MessageBoxButton)/*Error near IL_0026: Stack underflow*/, (MessageBoxImage)/*Error near IL_0026: Stack underflow*/);
					txtFill1.Focus();
					return true;
				}
				text = oQFill1.ConvertText(text, oQFill1.QDefine.CONTROL_MASK);
				txtFill1.Text = text;
			}
			oQFill1.FillText = text;
			text = txtFill2.Text;
			if (txtFill2.IsEnabled)
			{
				_003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_008b: Stack underflow*/ == (string)/*Error near IL_008b: Stack underflow*/)
				{
					string msgNotFill2 = SurveyMsg.MsgNotFill;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_00f7: Stack underflow*/, (string)/*Error near IL_00f7: Stack underflow*/, (MessageBoxButton)/*Error near IL_00f7: Stack underflow*/, MessageBoxImage.Hand);
					txtFill2.Focus();
					return true;
				}
				text = oQFill2.ConvertText(text, oQFill2.QDefine.CONTROL_MASK);
				txtFill2.Text = text;
			}
			oQFill2.FillText = text;
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

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00b8: Incompatible stack heights: 0 vs 2
			//IL_00de: Incompatible stack heights: 0 vs 1
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
				string content = ((P_Fill2)/*Error near IL_0040: Stack underflow*/).btnNav_Content;
				((ContentControl)/*Error near IL_0045: Stack underflow*/).Content = content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					btnNav.Content = btnNav_Content;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						MessageBox.Show(SurveyHelper.ShowPageAnswer((List<VEAnswer>)/*Error near IL_0097: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00a2:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0030: Incompatible stack heights: 0 vs 1
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				((P_Fill2)/*Error near IL_0010: Stack underflow*/).btnNav.Foreground = Brushes.Black;
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
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\tŢɗ\u0350ћԏ٣ݾ\u086eॴਧ\u0b78\u0c75൴\u0e68\u0f78ၸᅰቺ፧ᐽᕧᙹᝪ\u1879\u1922\u1a7c᭔ᱬ\u1d60ṤὫ‴Å≼⍢⑯╭"), UriKind.Relative);
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
				((P_Fill2)_003F350_003F).Loaded += _003F80_003F;
				((P_Fill2)_003F350_003F).LayoutUpdated += _003F99_003F;
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
