using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class P_AutoNextFillDec : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__37_0;

			internal int _003F300_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private UDPX oFunc = new UDPX();

		private QMatrixFill oQuestion = new QMatrixFill();

		private List<WrapPanel> listW = new List<WrapPanel>();

		private List<TextBlock> listTexts = new List<TextBlock>();

		private List<TextBlock> listLeftTexts = new List<TextBlock>();

		private List<TextBlock> listRightTexts = new List<TextBlock>();

		private List<TextBox> listFills = new List<TextBox>();

		private int _nMin;

		private int _nMax = 20;

		private string _txtLeft = _003F487_003F._003F488_003F("");

		private string _txtRight = _003F487_003F._003F488_003F("");

		private int _nLastTextBox;

		private int _CurrentTextBox;

		private int BrandText_Width;

		private int Fill_Height = 60;

		private double Fill_Width = 80.0;

		private int Fill_FontSize = 45;

		private int Fill_Length = 2;

		private List<string> listOtherValue = new List<string>();

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private double w_Height;

		private bool PageLoaded;

		private bool CheckChipsLogic = true;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ScrollViewer scrollmain;

		internal WrapPanel wrapPanel1;

		internal WrapPanel wrapOther;

		internal TextBlock txtQuestionNote;

		internal TextBlock txtSurvey;

		internal Button btnNav;

		private bool _contentLoaded;

		public P_AutoNextFillDec()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_016e: Incompatible stack heights: 0 vs 1
			//IL_018d: Incompatible stack heights: 0 vs 1
			//IL_019d: Incompatible stack heights: 0 vs 1
			//IL_042a: Incompatible stack heights: 0 vs 1
			//IL_0431: Incompatible stack heights: 0 vs 1
			//IL_0a92: Incompatible stack heights: 0 vs 2
			//IL_0aa9: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
			{
				MyNav.GroupLevel = _003F487_003F._003F488_003F("@");
				MyNav.GroupPageType = oQuestion.QDefine.GROUP_PAGE_TYPE;
				MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				MyNav.GetCircleInfo(MySurveyId);
				oQuestion.QuestionName += MyNav.QName_Add;
				QMatrixFill oQuestion2 = oQuestion;
				if (!(oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("@")))
				{
					string gROUP_CODEB = oQuestion.QDefine.GROUP_CODEB;
				}
				else
				{
					string gROUP_CODEA = oQuestion.QDefine.GROUP_CODEA;
				}
				((QMatrixFill)/*Error near IL_01a2: Stack underflow*/).CircleQuestionName = (string)/*Error near IL_01a2: Stack underflow*/;
				oQuestion.CircleQuestionName += MyNav.QName_Add;
				new List<VEAnswer>().Add(new VEAnswer
				{
					QUESTION_NAME = MyNav.GroupCodeA,
					CODE = MyNav.CircleACode,
					CODE_TEXT = MyNav.CircleCodeTextA
				});
				SurveyHelper.CircleACode = MyNav.CircleACode;
				SurveyHelper.CircleACodeText = MyNav.CircleCodeTextA;
				SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
				SurveyHelper.CircleACount = MyNav.CircleACount;
			}
			else
			{
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
			}
			oLogicEngine.SurveyID = MySurveyId;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				oLogicEngine.CircleACode = SurveyHelper.CircleACode;
				oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
			}
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			if (list.Count <= 1)
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			else
			{
				string text = list[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_0432: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			List<SurveyDetail>.Enumerator enumerator;
			if (oQuestion.QCircleDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list2 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current = enumerator.Current;
							if (current.CODE == array[i].ToString())
							{
								list2.Add(current);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				oQuestion.QCircleDetails = list2;
			}
			if (oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int j = 0; j < oQuestion.QCircleDetails.Count(); j++)
				{
					oQuestion.QCircleDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QCircleDetails[j].CODE_TEXT);
				}
			}
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQuestion.QDefine.CONTROL_TOOLTIP;
				list = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				_txtLeft = list[0];
				if (list.Count > 1)
				{
					_txtRight = list[1];
				}
			}
			Fill_Length = oQuestion.QDefine.CONTROL_TYPE;
			if (Fill_Length == 0)
			{
				Fill_Length = 4;
			}
			Fill_Width = (double)(Fill_Length * Fill_FontSize) * Math.Pow(0.955, (double)Fill_Length);
			if (oQuestion.QCircleDefine.CONTROL_WIDTH != 0)
			{
				BrandText_Width = oQuestion.QCircleDefine.TITLE_FONTSIZE;
			}
			if (oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				Fill_Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				Fill_Height = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				Fill_FontSize = oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (oQuestion.QDefine.MIN_COUNT > 0)
			{
				_nMin = oQuestion.QDefine.MIN_COUNT;
			}
			_nMax = (int)Math.Pow(10.0, (double)Fill_Length) - 1;
			if (oQuestion.QDefine.MAX_COUNT > 0)
			{
				_nMax = oQuestion.QDefine.MAX_COUNT;
			}
			_003F135_003F();
			Button_Type = oQuestion.QCircleDefine.CONTROL_TYPE;
			if (Button_Type == 0)
			{
				Button_Type = 4;
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
					string[] array2 = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
					List<SurveyDetail> list3 = new List<SurveyDetail>();
					for (int k = 0; k < array2.Count(); k++)
					{
						enumerator = oQuestion.QDetails.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail current2 = enumerator.Current;
								if (current2.CODE == array2[k].ToString())
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
					if (_003F7_003F._003C_003E9__37_0 == null)
					{
						_003F7_003F._003C_003E9__37_0 = _003F7_003F._003C_003E9._003F300_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0aae: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0aae: Stack underflow*/);
					oQuestion.QDetails = list3;
				}
				if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
				{
					for (int l = 0; l < oQuestion.QDetails.Count(); l++)
					{
						oQuestion.QDetails[l].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[l].CODE_TEXT);
					}
				}
				Button_Width = 200.0;
				Button_Height = SurveyHelper.BtnHeight;
				Button_FontSize = SurveyHelper.BtnFontSize;
				if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
				{
					Button_Height = oQuestion.QCircleDefine.CONTROL_HEIGHT;
				}
				if (oQuestion.QDefine.CONTROL_WIDTH != 0)
				{
					Button_Width = (double)oQuestion.QCircleDefine.CONTROL_WIDTH;
				}
				if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
				{
					Button_FontSize = oQuestion.QCircleDefine.CONTROL_FONTSIZE;
				}
				_003F28_003F();
			}
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQuestion.QDefine.NOTE;
				list = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list[0];
				oBoldTitle.SetTextBlock(txtQuestionNote, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				if (listFills.Count > 0)
				{
					foreach (TextBox listFill in listFills)
					{
						if (listFill.Text == _003F487_003F._003F488_003F(""))
						{
							listFill.Text = autoFill.FillInt(oQuestion.QDefine);
						}
					}
					if (autoFill.AutoNext(oQuestion.QDefine))
					{
						_003F58_003F(this, _003F348_003F);
					}
				}
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")) && !(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")))
			{
				bool flag = navOperation == _003F487_003F._003F488_003F("NŶɯͱ");
			}
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				btnNav.Foreground = Brushes.LightGray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
			if (_nLastTextBox > 0)
			{
				if (listFills[_nLastTextBox - 1].Text != _003F487_003F._003F488_003F(""))
				{
					listFills[_nLastTextBox - 1].Focus();
				}
				else if (listFills.Count > 0)
				{
					listFills[0].Focus();
				}
			}
			else if (listFills.Count > 0)
			{
				listFills[0].Focus();
			}
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_01e4: Incompatible stack heights: 0 vs 1
			//IL_01f6: Incompatible stack heights: 0 vs 1
			//IL_020b: Incompatible stack heights: 0 vs 1
			//IL_021c: Incompatible stack heights: 0 vs 2
			//IL_026c: Incompatible stack heights: 0 vs 2
			//IL_0282: Incompatible stack heights: 0 vs 2
			//IL_02a2: Incompatible stack heights: 0 vs 2
			if (!PageLoaded)
			{
				return;
			}
			WrapPanel wrapPanel = wrapPanel1;
			ScrollViewer scrollViewer = ((P_AutoNextFillDec)/*Error near IL_0010: Stack underflow*/).scrollmain;
			if (Button_Type < -20)
			{
				scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				((ScrollViewer)/*Error near IL_0024: Stack underflow*/).HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
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
						if (/*Error near IL_0287: Stack underflow*/ != /*Error near IL_0287: Stack underflow*/)
						{
							wrapPanel.Orientation = Orientation.Horizontal;
							goto IL_01a0;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					goto IL_01a0;
				}
				scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
				((ScrollViewer)/*Error near IL_015c: Stack underflow*/).HorizontalScrollBarVisibility = (ScrollBarVisibility)/*Error near IL_015c: Stack underflow*/;
				wrapPanel.Orientation = Orientation.Vertical;
				wrapPanel.Height = (double)Button_Type;
				PageLoaded = false;
			}
			else
			{
				int button_Type2 = Button_Type;
				if ((int)/*Error near IL_0210: Stack underflow*/ == 0)
				{
					Visibility computedVerticalScrollBarVisibility = scrollViewer.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_0221: Stack underflow*/ != /*Error near IL_0221: Stack underflow*/)
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
					Button_Type = 4;
					PageLoaded = false;
				}
			}
			goto IL_02bd;
			IL_02bd:
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			return;
			IL_01a0:
			if (Button_Type != 3)
			{
				int button_Type3 = Button_Type;
				if (/*Error near IL_02a7: Stack underflow*/ != /*Error near IL_02a7: Stack underflow*/)
				{
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
					goto IL_02b6;
				}
			}
			scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
			scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
			goto IL_02b6;
			IL_02b6:
			PageLoaded = false;
			goto IL_02bd;
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
					listOtherValue.Add(button.Tag.ToString());
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
			//IL_0065: Incompatible stack heights: 0 vs 2
			string text = (string)((Button)_003F347_003F).Content;
			listFills[_CurrentTextBox].Text = text;
			if (listFills.Count == _CurrentTextBox + 1)
			{
				List<TextBox> listFill = listFills;
				int currentTextBox = _CurrentTextBox;
				((List<TextBox>)/*Error near IL_0045: Stack underflow*/)[(int)/*Error near IL_0045: Stack underflow*/].Focus();
			}
			else
			{
				_003F90_003F(listFills[_CurrentTextBox], null);
				_CurrentTextBox++;
				listFills[_CurrentTextBox].Focus();
				_003F91_003F(listFills[_CurrentTextBox], null);
			}
		}

		private void _003F135_003F()
		{
			//IL_01b4: Incompatible stack heights: 0 vs 1
			//IL_01c4: Incompatible stack heights: 0 vs 1
			//IL_01c5: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
			Brush brush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			WrapPanel wrapPanel = wrapPanel1;
			if (oQuestion.QCircleDefine.CONTROL_TYPE == 1)
			{
				wrapPanel.Orientation = Orientation.Horizontal;
			}
			int num = 0;
			foreach (SurveyDetail qCircleDetail in oQuestion.QCircleDetails)
			{
				string cODE = qCircleDetail.CODE;
				string cODE_TEXT = qCircleDetail.CODE_TEXT;
				string text = _003F487_003F._003F488_003F("");
				if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
				{
					string _003F285_003F = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + cODE;
					text = oQuestion.ReadAnswerByQuestionName(MySurveyId, _003F285_003F);
					text = ((text != null && !(text == _003F487_003F._003F488_003F(""))) ? oFunc.StringToDouble(text).ToString() : _003F487_003F._003F488_003F(""));
				}
				WrapPanel wrapPanel2 = new WrapPanel();
				if (!(oQuestion.QCircleDefine.CONTROL_TOOLTIP.Trim().ToUpper() == _003F487_003F._003F488_003F("W")))
				{
				}
				((WrapPanel)/*Error near IL_01ca: Stack underflow*/).Orientation = (Orientation)/*Error near IL_01ca: Stack underflow*/;
				wrapPanel2.Margin = new Thickness(20.0, 20.0, 20.0, 20.0);
				wrapPanel2.Visibility = Visibility.Collapsed;
				wrapPanel.Children.Add(wrapPanel2);
				listW.Add(wrapPanel2);
				WrapPanel wrapPanel3 = new WrapPanel();
				wrapPanel3.VerticalAlignment = VerticalAlignment.Center;
				if (BrandText_Width > 0)
				{
					wrapPanel3.Width = (double)BrandText_Width;
				}
				wrapPanel2.Children.Add(wrapPanel3);
				TextBlock textBlock = new TextBlock();
				textBlock.Text = cODE_TEXT;
				textBlock.Style = style;
				textBlock.Foreground = foreground;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.FontSize = (double)Fill_FontSize;
				textBlock.Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
				textBlock.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel3.Children.Add(textBlock);
				listTexts.Add(textBlock);
				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;
				stackPanel.VerticalAlignment = VerticalAlignment.Center;
				wrapPanel2.Children.Add(stackPanel);
				TextBlock textBlock2 = new TextBlock();
				textBlock2.Text = _txtLeft;
				textBlock2.Style = style;
				textBlock2.Foreground = foreground;
				textBlock2.TextWrapping = TextWrapping.Wrap;
				textBlock2.FontSize = (double)Fill_FontSize;
				textBlock2.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBlock2.VerticalAlignment = VerticalAlignment.Center;
				stackPanel.Children.Add(textBlock2);
				listLeftTexts.Add(textBlock2);
				TextBox textBox = new TextBox();
				textBox.Text = text;
				textBox.Tag = num;
				textBox.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBox.FontSize = (double)Fill_FontSize;
				textBox.Width = Fill_Width;
				textBox.Height = (double)Fill_Height;
				textBox.MaxLength = Fill_Length;
				textBox.VerticalAlignment = VerticalAlignment.Center;
				textBox.HorizontalAlignment = HorizontalAlignment.Right;
				textBox.GotFocus += _003F91_003F;
				textBox.LostFocus += _003F90_003F;
				textBox.KeyDown += _003F86_003F;
				textBox.TextChanged += _003F98_003F;
				stackPanel.Children.Add(textBox);
				listFills.Add(textBox);
				TextBlock textBlock3 = new TextBlock();
				textBlock3.Text = _txtRight;
				textBlock3.Style = style;
				textBlock3.Foreground = foreground;
				textBlock3.TextWrapping = TextWrapping.Wrap;
				textBlock3.FontSize = (double)Fill_FontSize;
				textBlock3.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBlock3.VerticalAlignment = VerticalAlignment.Center;
				stackPanel.Children.Add(textBlock3);
				listRightTexts.Add(textBlock3);
				if (num < 2)
				{
					wrapPanel2.Visibility = Visibility.Visible;
					_nLastTextBox = num;
				}
				else if (text != _003F487_003F._003F488_003F(""))
				{
					wrapPanel2.Visibility = Visibility.Visible;
					_nLastTextBox = num;
				}
				else if (listFills[_nLastTextBox].Text != _003F487_003F._003F488_003F(""))
				{
					wrapPanel2.Visibility = Visibility.Visible;
					_nLastTextBox = num;
				}
				num++;
			}
		}

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Incompatible stack heights: 0 vs 1
			//IL_00ef: Incompatible stack heights: 0 vs 3
			//IL_0100: Incompatible stack heights: 0 vs 2
			//IL_0111: Incompatible stack heights: 0 vs 2
			//IL_0116: Invalid comparison between Unknown and I4
			//IL_0121: Incompatible stack heights: 0 vs 1
			//IL_0140: Incompatible stack heights: 0 vs 1
			//IL_0156: Incompatible stack heights: 0 vs 2
			//IL_0168: Incompatible stack heights: 0 vs 3
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_016e: Expected I4, but got Unknown
			if (_003F348_003F.Key == Key.Return)
			{
				TextBox textBox = (TextBox)_003F347_003F;
				int num = (int)((FrameworkElement)/*Error near IL_0011: Stack underflow*/).Tag;
				if (textBox.Text.Trim() == _003F487_003F._003F488_003F(""))
				{
					int nLastTextBox = _nLastTextBox;
					_003F val = /*Error near IL_0037: Stack underflow*/- /*Error near IL_0037: Stack underflow*/;
					if (/*Error near IL_00f4: Stack underflow*/ > val)
					{
						int nLastTextBox2 = _nLastTextBox;
						if (/*Error near IL_0105: Stack underflow*/ > /*Error near IL_0105: Stack underflow*/)
						{
							List<WrapPanel> listW2 = listW;
							int num2 = ((List<WrapPanel>)/*Error near IL_0046: Stack underflow*/).Count - 1;
							if ((int)/*Error near IL_0116: Stack underflow*/ >= num2)
							{
								listFills[num - 1].Focus();
								listTexts[num - 1].BringIntoView();
							}
							else
							{
								List<TextBox> listFill = listFills;
								if (((List<TextBox>)/*Error near IL_0055: Stack underflow*/)[num + 1].Text == _003F487_003F._003F488_003F(""))
								{
									listFills[num - 1].Focus();
									((P_AutoNextFillDec)/*Error near IL_0073: Stack underflow*/).listTexts[num - 1].BringIntoView();
								}
							}
						}
					}
				}
				else if (!_003F136_003F(textBox.Text.Trim()))
				{
					int count = listFills.Count;
					_003F val2 = /*Error near IL_00c1: Stack underflow*/- 1;
					if (/*Error near IL_015b: Stack underflow*/ < val2)
					{
						List<TextBox> listFill2 = listFills;
						_003F val3 = /*Error near IL_0169: Stack underflow*/+ /*Error near IL_0169: Stack underflow*/;
						((List<TextBox>)/*Error near IL_016e: Stack underflow*/)[(int)val3].Focus();
					}
					else
					{
						listFills[0].Focus();
						listTexts[0].BringIntoView();
					}
				}
			}
		}

		private void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_00a1: Incompatible stack heights: 0 vs 1
			//IL_00c3: Incompatible stack heights: 0 vs 2
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				bool flag = false;
				string text2 = textBox.Text;
				int startIndex = offset;
				int addedLength = array[0].AddedLength;
				if (!(((string)/*Error near IL_004a: Stack underflow*/).Substring(startIndex, addedLength).Trim() == _003F487_003F._003F488_003F("")))
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
					string text3 = textBox.Text;
					int startIndex2 = offset;
					int addedLength2 = array[0].AddedLength;
					string text = ((string)/*Error near IL_00d1: Stack underflow*/).Remove(startIndex2, addedLength2);
					((TextBox)/*Error near IL_00d6: Stack underflow*/).Text = text;
					textBox.Select(offset, 0);
				}
			}
		}

		private bool _003F136_003F(string _003F375_003F)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected I4, but got Unknown
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected I4, but got Unknown
			//IL_0194: Incompatible stack heights: 0 vs 3
			//IL_01a6: Incompatible stack heights: 0 vs 3
			//IL_01bb: Incompatible stack heights: 0 vs 3
			//IL_01d0: Incompatible stack heights: 0 vs 1
			//IL_01e0: Incompatible stack heights: 0 vs 1
			//IL_01e5: Invalid comparison between Unknown and F8
			//IL_020c: Incompatible stack heights: 0 vs 1
			//IL_0222: Incompatible stack heights: 0 vs 2
			//IL_0227: Invalid comparison between Unknown and F8
			//IL_023c: Incompatible stack heights: 0 vs 2
			//IL_0256: Incompatible stack heights: 0 vs 1
			//IL_028b: Incompatible stack heights: 0 vs 2
			if (_003F375_003F.Length > 0)
			{
				int length = _003F375_003F.Length;
				_003F val = /*Error near IL_000d: Stack underflow*/- /*Error near IL_000d: Stack underflow*/;
				if (((string)/*Error near IL_0013: Stack underflow*/).Substring((int)val, 1) == _003F487_003F._003F488_003F("/"))
				{
					int length2 = _003F375_003F.Length;
					_003F val2 = /*Error near IL_0029: Stack underflow*/- 1;
					_003F375_003F = ((string)/*Error near IL_002e: Stack underflow*/).Substring((int)/*Error near IL_002e: Stack underflow*/, (int)val2);
				}
			}
			if (_003F375_003F == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill = SurveyMsg.MsgNotFill;
				string msgCaption2 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_004c: Stack underflow*/, (string)/*Error near IL_004c: Stack underflow*/, (MessageBoxButton)/*Error near IL_004c: Stack underflow*/, MessageBoxImage.Hand);
				return true;
			}
			if (!listOtherValue.Contains(_003F375_003F))
			{
				SurveyDefine qDefine = oQuestion.QDefine;
				if (((SurveyDefine)/*Error near IL_0065: Stack underflow*/).MIN_COUNT > 0)
				{
					Convert.ToDouble(_003F375_003F);
					double num = (double)oQuestion.QDefine.MIN_COUNT;
					if (!((double)/*Error near IL_01e5: Stack underflow*/ >= num))
					{
						string.Format(SurveyMsg.MsgFillNotSmall, oQuestion.QDefine.MIN_COUNT.ToString());
						MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_008e: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Hand);
						return true;
					}
				}
				if (oQuestion.QDefine.MAX_COUNT > 0)
				{
					Convert.ToDouble(_003F375_003F);
					QMatrixFill oQuestion2 = oQuestion;
					double num2 = (double)((QMatrixFill)/*Error near IL_00ac: Stack underflow*/).QDefine.MAX_COUNT;
					if (!((double)/*Error near IL_0227: Stack underflow*/ <= num2))
					{
						string msgFillNotBig = SurveyMsg.MsgFillNotBig;
						SurveyDefine qDefine2 = oQuestion.QDefine;
						MessageBox.Show(string.Format(arg0: ((SurveyDefine)/*Error near IL_00bc: Stack underflow*/).MAX_COUNT.ToString(), format: (string)/*Error near IL_00c9: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return true;
					}
				}
			}
			if (oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
			{
				string cONTROL_MASK = oQuestion.QDefine.CONTROL_MASK;
				string text = (string)/*Error near IL_00fe: Stack underflow*/;
				if (text.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
				{
					text += _003F487_003F._003F488_003F(".ı");
				}
				string arg = text.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
				if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + _003F375_003F + _003F487_003F._003F488_003F("-") + text + _003F487_003F._003F488_003F("(")))
				{
					string.Format(SurveyMsg.MsgFillIntFit, arg);
					string msgCaption3 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_017c: Stack underflow*/, (string)/*Error near IL_017c: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			return false;
		}

		private bool _003F87_003F()
		{
			new Dictionary<string, int>();
			oQuestion.FillTexts = new List<string>();
			int num = 0;
			double num2 = -1.0;
			foreach (TextBox listFill in listFills)
			{
				if (listW[num].Visibility == Visibility.Visible && (!(listFill.Text == _003F487_003F._003F488_003F("")) || _nLastTextBox != num || _nLastTextBox <= 0))
				{
					string text = listFill.Text;
					double num3 = oFunc.StringToDouble(text);
					if (text == _003F487_003F._003F488_003F(""))
					{
						MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						listFill.Focus();
						return true;
					}
					if (!listOtherValue.Contains(text))
					{
						if (num3 < (double)_nMin)
						{
							MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, _nMin.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							listFill.Focus();
							return true;
						}
						if (num3 > (double)_nMax)
						{
							MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, _nMax.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							listFill.Focus();
							return true;
						}
						if (oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
						{
							string text2 = oQuestion.QDefine.CONTROL_MASK;
							if (text2.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
							{
								text2 += _003F487_003F._003F488_003F(".ı");
							}
							string arg = text2.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
							if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text2 + _003F487_003F._003F488_003F("(")))
							{
								MessageBox.Show(string.Format(SurveyMsg.MsgFillIntFit, arg), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								listFill.Focus();
								return true;
							}
						}
						if (num2 > -1.0)
						{
							if (oQuestion.QCircleDefine.CONTROL_MASK == _003F487_003F._003F488_003F("0"))
							{
								if (num2 < num3)
								{
									MessageBox.Show(SurveyMsg.MsgFromBigToSmall, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									listFill.Focus();
									return true;
								}
							}
							else if (oQuestion.QCircleDefine.CONTROL_MASK == _003F487_003F._003F488_003F("2"))
							{
								if (num2 > num3)
								{
									MessageBox.Show(SurveyMsg.MsgFromSmallToBig, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									listFill.Focus();
									return true;
								}
							}
							else if (oQuestion.QCircleDefine.CONTROL_MASK == _003F487_003F._003F488_003F("3") && CheckChipsLogic)
							{
								if (num2 < num3)
								{
									if (MessageBox.Show(SurveyMsg.MsgFromBigToSmall + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
									{
										listFill.Focus();
										return true;
									}
									CheckChipsLogic = false;
								}
							}
							else if (oQuestion.QCircleDefine.CONTROL_MASK == _003F487_003F._003F488_003F("5") && CheckChipsLogic && num2 > num3)
							{
								if (MessageBox.Show(SurveyMsg.MsgFromSmallToBig + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
								{
									listFill.Focus();
									return true;
								}
								CheckChipsLogic = false;
							}
						}
						num2 = num3;
					}
					oQuestion.FillTexts.Add(text);
					num++;
				}
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			//IL_0059: Incompatible stack heights: 0 vs 1
			//IL_0069: Incompatible stack heights: 0 vs 1
			List<VEAnswer> list = new List<VEAnswer>();
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			if (!(oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("@")))
			{
				string gROUP_CODEB = oQuestion.QDefine.GROUP_CODEB;
			}
			else
			{
				string gROUP_CODEA = oQuestion.QDefine.GROUP_CODEA;
			}
			string str = (string)/*Error near IL_006a: Stack underflow*/;
			str += MyNav.QName_Add;
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			int num = 0;
			foreach (string fillText in oQuestion.FillTexts)
			{
				string text = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + oQuestion.QCircleDetails[num].CODE;
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = text;
				vEAnswer.CODE = fillText;
				list.Add(vEAnswer);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + text + _003F487_003F._003F488_003F("<") + fillText;
				dictionary.Add(oQuestion.QCircleDetails[num].CODE, oFunc.StringToDouble(fillText));
				text = str + _003F487_003F._003F488_003F("]œ") + (num + 1).ToString();
				vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = text;
				vEAnswer.CODE = oQuestion.QCircleDetails[num].CODE;
				list.Add(vEAnswer);
				num++;
			}
			SurveyHelper.Answer = _003F94_003F(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			//IL_005d: Incompatible stack heights: 0 vs 2
			oQuestion.BeforeSave(2);
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (oQuestion.QDefine.PAGE_COUNT_DOWN > 0)
			{
				PageNav oPageNav2 = oPageNav;
				QMatrixFill oQuestion2 = oQuestion;
				int pAGE_COUNT_DOWN = ((QMatrixFill)/*Error near IL_003d: Stack underflow*/).QDefine.PAGE_COUNT_DOWN;
				Button _003F431_003F = btnNav;
				string mySurveyId = MySurveyId;
				((PageNav)/*Error near IL_006f: Stack underflow*/).PageDataLog(pAGE_COUNT_DOWN, _003F370_003F, _003F431_003F, mySurveyId);
			}
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
				string content = ((P_AutoNextFillDec)/*Error near IL_0040: Stack underflow*/).btnNav_Content;
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
				((P_AutoNextFillDec)/*Error near IL_0010: Stack underflow*/).btnNav.Foreground = Brushes.Black;
				btnNav.Content = btnNav_Content;
			}
			else
			{
				SecondsCountDown--;
				btnNav.Content = SecondsCountDown.ToString();
			}
		}

		private void _003F90_003F(object _003F347_003F, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Expected I4, but got Unknown
			//IL_010a: Incompatible stack heights: 0 vs 2
			//IL_0121: Incompatible stack heights: 0 vs 3
			//IL_012c: Incompatible stack heights: 0 vs 1
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			TextBox obj = (TextBox)_003F347_003F;
			int index = _CurrentTextBox = (int)obj.Tag;
			obj.Text = obj.Text.Trim();
			listTexts[index].Foreground = foreground;
			listLeftTexts[index].Foreground = foreground;
			listRightTexts[index].Foreground = foreground;
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.HideInputPanel();
			}
			if (_nLastTextBox > 1)
			{
				List<TextBox> listFill = listFills;
				int nLastTextBox = ((P_AutoNextFillDec)/*Error near IL_00a3: Stack underflow*/)._nLastTextBox;
				if (((List<TextBox>)/*Error near IL_00a8: Stack underflow*/)[nLastTextBox].Text == _003F487_003F._003F488_003F(""))
				{
					List<TextBox> listFill2 = listFills;
					int nLastTextBox2 = _nLastTextBox;
					_003F val = /*Error near IL_00c2: Stack underflow*/- /*Error near IL_00c2: Stack underflow*/;
					if (((List<TextBox>)/*Error near IL_00c7: Stack underflow*/)[(int)val].Text == _003F487_003F._003F488_003F(""))
					{
						((P_AutoNextFillDec)/*Error near IL_00e5: Stack underflow*/).listW[_nLastTextBox].Visibility = Visibility.Collapsed;
						_nLastTextBox--;
					}
				}
			}
		}

		private void _003F91_003F(object _003F347_003F, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected I4, but got Unknown
			//IL_0143: Incompatible stack heights: 0 vs 1
			//IL_0156: Incompatible stack heights: 0 vs 2
			//IL_0161: Incompatible stack heights: 0 vs 1
			//IL_0166: Invalid comparison between Unknown and I4
			//IL_0172: Incompatible stack heights: 0 vs 2
			//IL_017e: Incompatible stack heights: 0 vs 2
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			TextBox obj = (TextBox)_003F347_003F;
			int num = (int)obj.Tag;
			listTexts[num].Foreground = foreground;
			listLeftTexts[num].Foreground = foreground;
			listRightTexts[num].Foreground = foreground;
			obj.SelectAll();
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.ShowInputPanel();
			}
			if (_nLastTextBox < listW.Count - 1)
			{
				List<TextBox> listFill = listFills;
				int nLastTextBox = _nLastTextBox;
				if (((List<TextBox>)/*Error near IL_009f: Stack underflow*/)[nLastTextBox].Text != _003F487_003F._003F488_003F(""))
				{
					int num2 = _nLastTextBox + 1;
					((P_AutoNextFillDec)/*Error near IL_00bd: Stack underflow*/)._nLastTextBox = (int)/*Error near IL_00bd: Stack underflow*/;
					listW[_nLastTextBox].Visibility = Visibility.Visible;
				}
				if (num > 0)
				{
					int nLastTextBox2 = _nLastTextBox;
					if ((int)/*Error near IL_0166: Stack underflow*/ == nLastTextBox2)
					{
						List<TextBox> listFill2 = listFills;
						_003F val = /*Error near IL_00e8: Stack underflow*/- 1;
						if (((List<TextBox>)/*Error near IL_00ed: Stack underflow*/)[(int)val].Text != _003F487_003F._003F488_003F(""))
						{
							int nLastTextBox3 = ((P_AutoNextFillDec)/*Error near IL_010b: Stack underflow*/)._nLastTextBox + 1;
							((P_AutoNextFillDec)/*Error near IL_0112: Stack underflow*/)._nLastTextBox = nLastTextBox3;
							listW[_nLastTextBox].Visibility = Visibility.Visible;
						}
					}
				}
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u001fŨɝ\u035eѕԅ٩\u0748ࡘ\u094eਝ\u0b46\u0c4bൎ๒ཎ၎ᅺተ፩ᐳᕭᙳ\u177cᡯ\u1938\u1a66ᭊᱵ\u1d66Ṧ\u1f7e⁾Ⅺ≶⍹⑪╢♦❥⡬⥢⩥⬫\u2c7cⵢ\u2e6f⽭"), UriKind.Relative);
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
				((P_AutoNextFillDec)_003F350_003F).Loaded += _003F80_003F;
				((P_AutoNextFillDec)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				scrollmain = (ScrollViewer)_003F350_003F;
				break;
			case 5:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 6:
				wrapOther = (WrapPanel)_003F350_003F;
				break;
			case 7:
				txtQuestionNote = (TextBlock)_003F350_003F;
				break;
			case 8:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 9:
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
