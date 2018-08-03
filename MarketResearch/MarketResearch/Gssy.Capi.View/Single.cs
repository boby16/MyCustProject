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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class Single : Page, IComponentConnector
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

		private QSingle oQuestion = new QSingle();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

		private List<Button> listButton = new List<Button>();

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private double w_Height;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal Grid gridContent;

		internal ColumnDefinition PicWidth;

		internal ColumnDefinition ButtonWidth;

		internal ScrollViewer scrollPic;

		internal ScrollViewer scrollmain;

		internal WrapPanel wrapPanel1;

		internal StackPanel stackPanel1;

		internal TextBlock txtFillTitle;

		internal TextBox txtFill;

		internal TextBlock txtAfter;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public Single()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0576: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_0c33: Incompatible stack heights: 0 vs 2
			//IL_0c4a: Incompatible stack heights: 0 vs 1
			//IL_0fb5: Incompatible stack heights: 0 vs 1
			//IL_0fd4: Incompatible stack heights: 0 vs 1
			//IL_0fd9: Incompatible stack heights: 0 vs 1
			//IL_1031: Incompatible stack heights: 0 vs 1
			//IL_1050: Incompatible stack heights: 0 vs 1
			//IL_1055: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0, false);
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
			string sHOW_LOGIC = oQuestion.QDefine.SHOW_LOGIC;
			List<string> list2 = new List<string>();
			list2.Add(_003F487_003F._003F488_003F(""));
			if (sHOW_LOGIC != _003F487_003F._003F488_003F(""))
			{
				list2 = oBoldTitle.ParaToList(sHOW_LOGIC, _003F487_003F._003F488_003F("-Į"));
				if (list2.Count > 1)
				{
					oQuestion.QDefine.DETAIL_ID = oLogicEngine.Route(list2[1]);
				}
			}
			oQuestion.InitDetailID(CurPageId, 0);
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list3 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list3[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (list3.Count <= 1)
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			else
			{
				string text4 = list3[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_057e: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			string text = _003F487_003F._003F488_003F("");
			List<SurveyDetail>.Enumerator enumerator;
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				text = oLogicEngine.Route(oQuestion.QDefine.CONTROL_TOOLTIP);
			}
			else if (oQuestion.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F("") && oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
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
							SurveyDetail current = enumerator.Current;
							if (current.CODE == text2)
							{
								text = oLogicEngine.Route(current.EXTEND_1);
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
				if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("+") || oQuestion.QDefine.CONTROL_MASK.Trim() == _003F487_003F._003F488_003F("") || oQuestion.QDefine.CONTROL_MASK == null)
				{
					PicWidth.Width = new GridLength(1.0, GridUnitType.Star);
					ButtonWidth.Width = GridLength.Auto;
				}
				else
				{
					string cONTROL_MASK = oQuestion.QDefine.CONTROL_MASK;
					if (_003F93_003F(cONTROL_MASK, 1) == _003F487_003F._003F488_003F("\""))
					{
						cONTROL_MASK = _003F94_003F(cONTROL_MASK, 1, -9999);
						scrollPic.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						scrollPic.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						int num = _003F96_003F(cONTROL_MASK);
						if (num > 0)
						{
							scrollPic.Width = (double)num;
						}
					}
					else
					{
						int num2 = _003F96_003F(cONTROL_MASK);
						if (num2 > 0)
						{
							image.Width = (double)num2;
						}
					}
				}
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
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
					scrollPic.Content = image;
				}
				catch (Exception)
				{
				}
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
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
								list4.Add(current2);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				if (oQuestion.QDefine.SHOW_LOGIC == _003F487_003F._003F488_003F("") && oQuestion.QDefine.IS_RANDOM == 0)
				{
					if (_003F7_003F._003C_003E9__21_0 == null)
					{
						_003F7_003F._003C_003E9__21_0 = _003F7_003F._003C_003E9._003F315_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0c4f: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0c4f: Stack underflow*/);
				}
				oQuestion.QDetails = list4;
			}
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F("") && (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == _003F487_003F._003F488_003F("2"))))
			{
				string[] array2 = oLogicEngine.aryCode(oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int j = 0; j < array2.Count(); j++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								listPreSet.Add(array2[j]);
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
			if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int k = 0; k < oQuestion.QDetails.Count(); k++)
				{
					oQuestion.QDetails[k].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[k].CODE_TEXT);
				}
			}
			if (list2[0].Trim() != _003F487_003F._003F488_003F(""))
			{
				string[] array3 = oLogicEngine.aryCode(list2[0], ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count(); l++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current3 = enumerator.Current;
							if (current3.CODE == array3[l].ToString())
							{
								list5.Add(current3);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				oQuestion.QDetails = list5;
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
			((Single)/*Error near IL_0fde: Stack underflow*/).Button_FontSize = (int)/*Error near IL_0fde: Stack underflow*/;
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
			((Single)/*Error near IL_105a: Stack underflow*/).Button_Height = (int)/*Error near IL_105a: Stack underflow*/;
			if (oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (Button_Type == 2 || Button_Type == 4)
				{
					Button_Width = 440;
				}
				else
				{
					Button_Width = SurveyHelper.BtnWidth;
				}
			}
			else
			{
				Button_Width = oQuestion.QDefine.CONTROL_WIDTH;
			}
			_003F28_003F();
			if (ExistTextFill)
			{
				txtFill.Visibility = Visibility.Visible;
				if (oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F(""))
				{
					txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					qUESTION_TITLE = oQuestion.QDefine.NOTE;
					list3 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
					qUESTION_TITLE = list3[0];
					oBoldTitle.SetTextBlock(txtFillTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					if (list3.Count > 1)
					{
						qUESTION_TITLE = list3[1];
						oBoldTitle.SetTextBlock(txtAfter, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					}
				}
			}
			else
			{
				txtFill.Height = 0.0;
				txtFillTitle.Height = 0.0;
				txtAfter.Height = 0.0;
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			bool flag = false;
			bool flag2 = false;
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				if (!(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")))
				{
					if (navOperation == _003F487_003F._003F488_003F("NŶɯͱ"))
					{
					}
				}
				else
				{
					if (listPreSet.Count > 0)
					{
						oQuestion.SelectedCode = listPreSet[0];
						foreach (Button child in wrapPanel1.Children)
						{
							if (child.Name.Substring(2) == oQuestion.SelectedCode)
							{
								child.Style = style;
								int num3 = (int)child.Tag;
								if (num3 == 1 || num3 == 3 || num3 == 5 || ((num3 == 11) | (num3 == 13)) || num3 == 14)
								{
									flag = true;
								}
								break;
							}
						}
						if (flag)
						{
							txtFill.IsEnabled = true;
							txtFill.Background = Brushes.White;
						}
					}
					if (oQuestion.QDetails.Count == 1)
					{
						if (listPreSet.Count == 0 && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
						{
							_003F29_003F(listButton[0], null);
						}
						if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (txtFill.IsEnabled)
							{
								txtFill.Focus();
							}
							else
							{
								flag2 = true;
							}
						}
					}
					if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && oQuestion.SelectedCode != _003F487_003F._003F488_003F(""))
					{
						if (txtFill.IsEnabled)
						{
							txtFill.Focus();
						}
						else
						{
							flag2 = true;
						}
					}
					if (SurveyHelper.AutoFill)
					{
						AutoFill autoFill = new AutoFill();
						autoFill.oLogicEngine = oLogicEngine;
						if (oQuestion.SelectedCode == _003F487_003F._003F488_003F(""))
						{
							Button button2 = autoFill.SingleButton(oQuestion.QDefine, listButton);
							if (button2 != null)
							{
								if (listPreSet.Count == 0 && button2.Style == style2)
								{
									_003F29_003F(button2, null);
								}
								if (txtFill.IsEnabled)
								{
									txtFill.Text = autoFill.CommonOther(oQuestion.QDefine, _003F487_003F._003F488_003F(""));
								}
							}
						}
						if (oQuestion.SelectedCode != _003F487_003F._003F488_003F("") && !flag2 && autoFill.AutoNext(oQuestion.QDefine))
						{
							flag2 = true;
						}
					}
					if (flag2)
					{
						_003F58_003F(this, _003F348_003F);
					}
				}
			}
			else
			{
				oQuestion.SelectedCode = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName);
				foreach (Button child2 in wrapPanel1.Children)
				{
					if (child2.Name.Substring(2) == oQuestion.SelectedCode)
					{
						child2.Style = style;
						int num4 = (int)child2.Tag;
						if (num4 == 1 || num4 == 3 || num4 == 5 || ((num4 == 11) | (num4 == 13)) || num4 == 14)
						{
							flag = true;
						}
						break;
					}
				}
				txtFill.Text = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				if (flag)
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
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
			//IL_0213: Incompatible stack heights: 0 vs 1
			//IL_0223: Incompatible stack heights: 0 vs 1
			//IL_0235: Incompatible stack heights: 0 vs 1
			//IL_0241: Incompatible stack heights: 0 vs 2
			//IL_0256: Incompatible stack heights: 0 vs 1
			//IL_0267: Incompatible stack heights: 0 vs 2
			//IL_0279: Incompatible stack heights: 0 vs 1
			//IL_0296: Incompatible stack heights: 0 vs 2
			//IL_02ac: Incompatible stack heights: 0 vs 2
			//IL_02c2: Incompatible stack heights: 0 vs 2
			//IL_02e2: Incompatible stack heights: 0 vs 2
			if (oQuestion.QDetails == null)
			{
				return;
			}
			List<SurveyDetail> qDetail = oQuestion.QDetails;
			if (((List<SurveyDetail>)/*Error near IL_0015: Stack underflow*/).Count == 0)
			{
				return;
			}
			bool pageLoaded = PageLoaded;
			if ((int)/*Error near IL_0228: Stack underflow*/ == 0)
			{
				return;
			}
			WrapPanel wrapPanel = wrapPanel1;
			ScrollViewer scrollViewer = ((Single)/*Error near IL_0024: Stack underflow*/).scrollmain;
			if (Button_Type < -20)
			{
				((ScrollViewer)/*Error near IL_0037: Stack underflow*/).VerticalScrollBarVisibility = (ScrollBarVisibility)/*Error near IL_0037: Stack underflow*/;
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
						if (/*Error near IL_02c7: Stack underflow*/ != /*Error near IL_02c7: Stack underflow*/)
						{
							wrapPanel.Orientation = Orientation.Horizontal;
							goto IL_01c5;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					goto IL_01c5;
				}
				((ScrollViewer)/*Error near IL_017a: Stack underflow*/).VerticalScrollBarVisibility = (ScrollBarVisibility)/*Error near IL_017a: Stack underflow*/;
				scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
				wrapPanel.Orientation = Orientation.Vertical;
				wrapPanel.Height = (double)Button_Type;
				PageLoaded = false;
			}
			else
			{
				int button_Type2 = Button_Type;
				if ((int)/*Error near IL_025b: Stack underflow*/ == 0)
				{
					Visibility computedVerticalScrollBarVisibility = scrollViewer.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_026c: Stack underflow*/ != /*Error near IL_026c: Stack underflow*/)
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
						((Single)/*Error near IL_007b: Stack underflow*/).PageLoaded = false;
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
					Button_Type = 4;
					((Single)/*Error near IL_012f: Stack underflow*/).PageLoaded = ((byte)/*Error near IL_012f: Stack underflow*/ != 0);
				}
			}
			goto IL_02f6;
			IL_01c5:
			if (Button_Type != 3)
			{
				int button_Type3 = Button_Type;
				if (/*Error near IL_02e7: Stack underflow*/ != /*Error near IL_02e7: Stack underflow*/)
				{
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
					goto IL_01f7;
				}
			}
			scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
			scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
			goto IL_01f7;
			IL_01f7:
			PageLoaded = false;
			goto IL_02f6;
			IL_02f6:
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F28_003F()
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapPanel1;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Name = _003F487_003F._003F488_003F("`Ş") + qDetail.CODE;
				button.Content = qDetail.CODE_TEXT;
				button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
				button.Style = style;
				button.Tag = qDetail.IS_OTHER;
				if (qDetail.IS_OTHER == 1 || qDetail.IS_OTHER == 3 || ((qDetail.IS_OTHER == 11) | (qDetail.IS_OTHER == 5)) || qDetail.IS_OTHER == 13 || qDetail.IS_OTHER == 14)
				{
					ExistTextFill = true;
				}
				button.Click += _003F29_003F;
				button.FontSize = (double)Button_FontSize;
				button.MinWidth = (double)Button_Width;
				button.MinHeight = (double)Button_Height;
				wrapPanel.Children.Add(button);
				listButton.Add(button);
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F = null)
		{
			//IL_005f: Incompatible stack heights: 0 vs 1
			//IL_0144: Incompatible stack heights: 0 vs 1
			//IL_0154: Incompatible stack heights: 0 vs 1
			//IL_0155: Incompatible stack heights: 0 vs 1
			Button obj = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			int num = (int)obj.Tag;
			string text = obj.Name.Substring(2);
			int num2 = 0;
			if (num == 1 || num == 3 || num == 5 || num == 11 || num == 13 || num == 14)
			{
				num2 = 1;
			}
			int num3 = 0;
			if (((FrameworkElement)/*Error near IL_00c2: Stack underflow*/).Style == style)
			{
				num3 = 1;
			}
			if (num3 == 0)
			{
				oQuestion.SelectedCode = text;
				foreach (Button child in wrapPanel1.Children)
				{
					string a = child.Name.Substring(2);
					if (a == text)
					{
					}
					((FrameworkElement)/*Error near IL_015a: Stack underflow*/).Style = (Style)/*Error near IL_015a: Stack underflow*/;
				}
				if (num2 == 0)
				{
					txtFill.Background = Brushes.LightGray;
					txtFill.IsEnabled = false;
				}
				else
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
					if (txtFill.Text == _003F487_003F._003F488_003F(""))
					{
						txtFill.Focus();
					}
				}
			}
			if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode4) && oQuestion.SelectedCode != _003F487_003F._003F488_003F(""))
			{
				if (txtFill.IsEnabled)
				{
					txtFill.Focus();
				}
				else
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_00a6: Incompatible stack heights: 0 vs 3
			//IL_00b6: Incompatible stack heights: 0 vs 1
			//IL_00cd: Incompatible stack heights: 0 vs 4
			//IL_00d2: Incompatible stack heights: 0 vs 1
			//IL_00e1: Incompatible stack heights: 0 vs 1
			//IL_00f0: Incompatible stack heights: 0 vs 1
			if (oQuestion.SelectedCode == _003F487_003F._003F488_003F(""))
			{
				string msgNotSelected = SurveyMsg.MsgNotSelected;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0026: Stack underflow*/, (string)/*Error near IL_0026: Stack underflow*/, (MessageBoxButton)/*Error near IL_0026: Stack underflow*/, MessageBoxImage.Hand);
				return true;
			}
			if (txtFill.IsEnabled)
			{
				TextBox txtFill2 = txtFill;
				if (((TextBox)/*Error near IL_003e: Stack underflow*/).Text.Trim() == _003F487_003F._003F488_003F(""))
				{
					string msgNotFillOther = SurveyMsg.MsgNotFillOther;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_005c: Stack underflow*/, (string)/*Error near IL_005c: Stack underflow*/, (MessageBoxButton)/*Error near IL_005c: Stack underflow*/, (MessageBoxImage)/*Error near IL_005c: Stack underflow*/);
					txtFill.Focus();
					return true;
				}
			}
			QSingle oQuestion2 = oQuestion;
			if (!txtFill.IsEnabled)
			{
				_003F487_003F._003F488_003F("");
			}
			else
			{
				txtFill.Text.Trim();
			}
			((QSingle)/*Error near IL_00f5: Stack underflow*/).FillText = (string)/*Error near IL_00f5: Stack underflow*/;
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			//IL_009a: Incompatible stack heights: 0 vs 1
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQuestion.QuestionName;
			vEAnswer.CODE = oQuestion.SelectedCode;
			list.Add(vEAnswer);
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.SelectedCode;
			if (oQuestion.FillText != _003F487_003F._003F488_003F(""))
			{
				VEAnswer vEAnswer2 = new VEAnswer();
				string qUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349");
				((VEAnswer)/*Error near IL_00b3: Stack underflow*/).QUESTION_NAME = qUESTION_NAME;
				vEAnswer2.CODE = oQuestion.FillText;
				list.Add(vEAnswer2);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F(".ġ") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			}
			return list;
		}

		private void _003F89_003F()
		{
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, true);
		}

		private void _003F58_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00bd: Incompatible stack heights: 0 vs 2
			//IL_00d3: Incompatible stack heights: 0 vs 2
			//IL_00e3: Incompatible stack heights: 0 vs 1
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
					string btnNav_Content3 = btnNav_Content;
					((ContentControl)/*Error near IL_0080: Stack underflow*/).Content = (object)/*Error near IL_0080: Stack underflow*/;
				}
				else
				{
					_003F89_003F();
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_00ea: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Asterisk);
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
			//IL_0090: Incompatible stack heights: 0 vs 1
			//IL_0095: Incompatible stack heights: 1 vs 0
			//IL_00a0: Incompatible stack heights: 0 vs 1
			//IL_00a5: Incompatible stack heights: 1 vs 0
			//IL_00b0: Incompatible stack heights: 0 vs 1
			//IL_00b5: Incompatible stack heights: 1 vs 0
			//IL_00c0: Incompatible stack heights: 0 vs 1
			//IL_00c5: Incompatible stack heights: 1 vs 0
			//IL_00d0: Incompatible stack heights: 0 vs 1
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
			//IL_0037: Incompatible stack heights: 0 vs 1
			//IL_003c: Incompatible stack heights: 1 vs 0
			//IL_0041: Incompatible stack heights: 0 vs 2
			//IL_0047: Incompatible stack heights: 0 vs 1
			//IL_004c: Incompatible stack heights: 1 vs 0
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			int length;
			if (num > _003F362_003F.Length)
			{
				length = _003F362_003F.Length;
			}
			return ((string)/*Error near IL_0051: Stack underflow*/).Substring((int)/*Error near IL_0051: Stack underflow*/, length);
		}

		private string _003F94_003F(string _003F362_003F, int _003F363_003F, int _003F365_003F = -9999)
		{
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
			//IL_0031: Incompatible stack heights: 0 vs 1
			//IL_0036: Incompatible stack heights: 1 vs 0
			//IL_003b: Incompatible stack heights: 0 vs 1
			//IL_0048: Incompatible stack heights: 0 vs 1
			//IL_004d: Incompatible stack heights: 1 vs 0
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num <= _003F362_003F.Length)
			{
				int num2 = _003F362_003F.Length - num;
			}
			return ((string)/*Error near IL_0052: Stack underflow*/).Substring(0);
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\nţɐ\u0351јԎ\u065cݿ\u086dॵਠ\u0b79\u0c76൵\u0e67\u0f79ၻᅱች፦ᐾᕦᙦᝫ\u187a\u1923\u1a78᭣ᱧᵯṫὣ\u202bⅼ≢⍯⑭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0022:
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
				((Single)_003F350_003F).Loaded += _003F80_003F;
				((Single)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				gridContent = (Grid)_003F350_003F;
				break;
			case 5:
				PicWidth = (ColumnDefinition)_003F350_003F;
				break;
			case 6:
				ButtonWidth = (ColumnDefinition)_003F350_003F;
				break;
			case 7:
				scrollPic = (ScrollViewer)_003F350_003F;
				break;
			case 8:
				scrollmain = (ScrollViewer)_003F350_003F;
				break;
			case 9:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 10:
				stackPanel1 = (StackPanel)_003F350_003F;
				break;
			case 11:
				txtFillTitle = (TextBlock)_003F350_003F;
				break;
			case 12:
				txtFill = (TextBox)_003F350_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				break;
			case 13:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 14:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 15:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 16:
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
