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
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class P_ChipGame : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QMatrixFill oQuestion = new QMatrixFill();

		private List<TextBlock> ChipTexts = new List<TextBlock>();

		private List<TextBox> ChipFills = new List<TextBox>();

		private int nTotalChips = 20;

		private int nMaxChips = 20;

		private int nUseChips;

		private int nMin;

		private int nMax = 20;

		private bool CheckChipsLogic = true;

		private bool CheckSameChips = true;

		private bool PageLoaded;

		private int BrandText_Width;

		private int Fill_Height = 60;

		private double Fill_Width = 80.0;

		private int Fill_FontSize = 45;

		private int Fill_Length = 2;

		private int Button_Type;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ScrollViewer scrollmain;

		internal WrapPanel wrapPanel1;

		internal StackPanel stackPanel1;

		internal StackPanel used;

		internal TextBlock txtBefore1;

		internal TextBlock txtUsed;

		internal TextBlock txtAfter1;

		internal StackPanel unused;

		internal TextBlock txtBefore;

		internal TextBlock txtFill;

		internal TextBlock txtAfter;

		internal TextBlock txtSurvey;

		internal Button btnNav;

		private bool _contentLoaded;

		public P_ChipGame()
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
			//IL_0b55: Incompatible stack heights: 0 vs 1
			//IL_0b6a: Incompatible stack heights: 0 vs 1
			//IL_0b7a: Incompatible stack heights: 0 vs 1
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
			if (oQuestion.QCircleDefine.SHOW_LOGIC.Trim() != _003F487_003F._003F488_003F(""))
			{
				string[] array2 = oLogicEngine.aryCode(oQuestion.QCircleDefine.SHOW_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int k = 0; k < array2.Count(); k++)
				{
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
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
				oQuestion.QCircleDetails = list3;
			}
			else if (oQuestion.QCircleDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomCircleDetails();
			}
			if (oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("0") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("2") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("/İ") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("/Ĳ"))
			{
				used.Visibility = Visibility.Visible;
				if (oQuestion.QCircleDefine.NOTE != _003F487_003F._003F488_003F(""))
				{
					qUESTION_TITLE = oQuestion.QCircleDefine.NOTE;
					list = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
					qUESTION_TITLE = list[0];
					oBoldTitle.SetTextBlock(txtBefore1, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					if (list.Count > 1)
					{
						qUESTION_TITLE = list[1];
						oBoldTitle.SetTextBlock(txtAfter1, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					}
				}
			}
			if (oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("3") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("2") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("/ĳ") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("/Ĳ"))
			{
				unused.Visibility = Visibility.Visible;
				if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F(""))
				{
					qUESTION_TITLE = oQuestion.QDefine.NOTE;
					list = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
					qUESTION_TITLE = list[0];
					oBoldTitle.SetTextBlock(txtBefore, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					if (list.Count > 1)
					{
						qUESTION_TITLE = list[1];
						oBoldTitle.SetTextBlock(txtAfter, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					}
				}
			}
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				oQuestion.QDefine.CONTROL_TOOLTIP = oLogicEngine.doubleResult(oQuestion.QDefine.CONTROL_TOOLTIP).ToString();
				nTotalChips = _003F96_003F(oQuestion.QDefine.CONTROL_TOOLTIP);
				nMaxChips = nTotalChips;
				txtFill.Text = nTotalChips.ToString();
			}
			if (oQuestion.QDefine.MIN_COUNT > 0)
			{
				nMin = oQuestion.QDefine.MIN_COUNT;
			}
			if (oQuestion.QDefine.MAX_COUNT <= 0)
			{
				int nMaxChip = nMaxChips;
			}
			else
			{
				int mAX_COUNT = oQuestion.QDefine.MAX_COUNT;
			}
			((P_ChipGame)/*Error near IL_0b7f: Stack underflow*/).nMax = (int)/*Error near IL_0b7f: Stack underflow*/;
			Fill_Length = oQuestion.QDefine.CONTROL_TOOLTIP.Length;
			Fill_Width = (double)Fill_Length * txtFill.FontSize * Math.Pow(0.955, (double)Fill_Length);
			if (Fill_Width < 20.0)
			{
				Fill_Width = 20.0;
			}
			if (oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == _003F487_003F._003F488_003F("I"))
			{
				BrandText_Width = 250;
			}
			if (oQuestion.QCircleDefine.CONTROL_WIDTH != 0)
			{
				BrandText_Width = oQuestion.QCircleDefine.CONTROL_WIDTH;
			}
			if (oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				Fill_Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (oQuestion.QCircleDefine.CONTROL_HEIGHT != 0)
			{
				Fill_Height = oQuestion.QCircleDefine.CONTROL_HEIGHT;
			}
			if (oQuestion.QCircleDefine.CONTROL_FONTSIZE != 0)
			{
				Fill_FontSize = oQuestion.QCircleDefine.CONTROL_FONTSIZE;
			}
			_003F28_003F();
			nTotalChips = nMaxChips - nUseChips;
			txtUsed.Text = nUseChips.ToString();
			txtFill.Text = nTotalChips.ToString();
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				if (ChipFills.Count > 0)
				{
					int num = nTotalChips;
					foreach (TextBox chipFill in ChipFills)
					{
						if (num <= nMax)
						{
							chipFill.Text = num.ToString();
							_003F147_003F(chipFill);
							break;
						}
						chipFill.Text = nMax.ToString();
						num -= nMax;
						_003F147_003F(chipFill);
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
			PageLoaded = true;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0083: Incompatible stack heights: 0 vs 1
			//IL_0099: Incompatible stack heights: 0 vs 2
			//IL_00b9: Incompatible stack heights: 0 vs 1
			//IL_00bf: Incompatible stack heights: 0 vs 1
			//IL_00c4: Incompatible stack heights: 1 vs 0
			if (PageLoaded)
			{
				WrapPanel wrapPanel = wrapPanel1;
				int cONTROL_TYPE = oQuestion.QDefine.CONTROL_TYPE;
				((P_ChipGame)/*Error near IL_0020: Stack underflow*/).Button_Type = cONTROL_TYPE;
				if (Button_Type != 0)
				{
					if (Button_Type == 2)
					{
					}
					((WrapPanel)/*Error near IL_0067: Stack underflow*/).Orientation = Orientation.Vertical;
				}
				else
				{
					Visibility computedVerticalScrollBarVisibility = scrollmain.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_009e: Stack underflow*/ == /*Error near IL_009e: Stack underflow*/)
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
			//IL_01db: Incompatible stack heights: 0 vs 1
			//IL_01eb: Incompatible stack heights: 0 vs 1
			//IL_01ec: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
			Brush brush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			WrapPanel wrapPanel = wrapPanel1;
			bool flag = _003F96_003F(oQuestion.QCircleDefine.CONTROL_MASK) > 0;
			int num = 0;
			foreach (SurveyDetail qCircleDetail in oQuestion.QCircleDetails)
			{
				string cODE = qCircleDetail.CODE;
				string text = qCircleDetail.CODE_TEXT;
				if (flag)
				{
					text = _003F487_003F._003F488_003F(")") + (num + 1).ToString() + _003F487_003F._003F488_003F("+ġ") + text;
				}
				string text2 = nMin.ToString();
				if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
				{
					string _003F285_003F = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ł") + cODE;
					text2 = oQuestion.ReadAnswerByQuestionName(MySurveyId, _003F285_003F);
					int num2 = _003F96_003F(text2);
					text2 = num2.ToString();
					nUseChips += num2;
				}
				else
				{
					nUseChips += nMin;
				}
				WrapPanel wrapPanel2 = new WrapPanel();
				if (!(oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == _003F487_003F._003F488_003F("I")))
				{
				}
				((WrapPanel)/*Error near IL_01f1: Stack underflow*/).Orientation = (Orientation)/*Error near IL_01f1: Stack underflow*/;
				wrapPanel2.Margin = new Thickness(20.0, 20.0, 20.0, 20.0);
				wrapPanel.Children.Add(wrapPanel2);
				WrapPanel wrapPanel3 = new WrapPanel();
				wrapPanel3.VerticalAlignment = VerticalAlignment.Center;
				if (BrandText_Width > 0)
				{
					wrapPanel3.Width = (double)BrandText_Width;
				}
				wrapPanel2.Children.Add(wrapPanel3);
				TextBlock textBlock = new TextBlock();
				textBlock.Text = text;
				textBlock.Style = style2;
				textBlock.Foreground = foreground;
				textBlock.TextWrapping = TextWrapping.Wrap;
				textBlock.FontSize = (double)Fill_FontSize;
				wrapPanel3.Children.Add(textBlock);
				ChipTexts.Add(textBlock);
				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;
				wrapPanel2.Children.Add(stackPanel);
				Button button = new Button();
				button.Content = _003F487_003F._003F488_003F("#įȡ");
				button.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				button.Style = style;
				button.Tag = num;
				button.Click += _003F145_003F;
				button.FontSize = (double)Fill_FontSize;
				button.Height = (double)Fill_Height;
				stackPanel.Children.Add(button);
				TextBox textBox = new TextBox();
				textBox.Text = text2;
				textBox.Tag = num;
				textBox.ToolTip = _003F487_003F._003F488_003F("诽嘡跑鋋屭呠吂陎盛ग़") + text + _003F487_003F._003F488_003F("X瞀杳鋍㐃");
				textBox.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				textBox.FontSize = (double)Fill_FontSize;
				textBox.Width = Fill_Width;
				textBox.Height = (double)Fill_Height;
				textBox.MaxLength = Fill_Length;
				textBox.HorizontalAlignment = HorizontalAlignment.Right;
				textBox.GotFocus += _003F91_003F;
				textBox.LostFocus += _003F90_003F;
				textBox.KeyDown += _003F86_003F;
				textBox.TextChanged += _003F98_003F;
				stackPanel.Children.Add(textBox);
				ChipFills.Add(textBox);
				button = new Button();
				button.Content = _003F487_003F._003F488_003F("#ĩȡ");
				button.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
				button.Style = style;
				button.Tag = num;
				button.Click += _003F146_003F;
				button.FontSize = (double)Fill_FontSize;
				button.Height = (double)Fill_Height;
				stackPanel.Children.Add(button);
				oQuestion.FillTexts.Add(_003F487_003F._003F488_003F(""));
				num++;
			}
		}

		private void _003F145_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Expected I4, but got Unknown
			//IL_00ad: Incompatible stack heights: 0 vs 3
			int index = (int)((Button)_003F347_003F).Tag;
			int num = _003F96_003F(ChipFills[index].Text);
			if (num > nMin)
			{
				ChipFills[index].Text = (num - 1).ToString();
				nUseChips--;
				txtUsed.Text = nUseChips.ToString();
				if (nTotalChips < nMaxChips)
				{
					int nTotalChip = nTotalChips;
					_003F val = /*Error near IL_0083: Stack underflow*/+ /*Error near IL_0083: Stack underflow*/;
					((P_ChipGame)/*Error near IL_0088: Stack underflow*/).nTotalChips = (int)val;
					txtFill.Text = nTotalChips.ToString();
				}
			}
		}

		private void _003F146_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0056: Incompatible stack heights: 0 vs 3
			int index = (int)((Button)_003F347_003F).Tag;
			if (nTotalChips > 0)
			{
				List<TextBox> chipFill = ChipFills;
				string text = ((List<TextBox>)/*Error near IL_0022: Stack underflow*/)[(int)/*Error near IL_0022: Stack underflow*/].Text;
				int num = ((P_ChipGame)/*Error near IL_002c: Stack underflow*/)._003F96_003F(text);
				if (num < nMax)
				{
					ChipFills[index].Text = (num + 1).ToString();
					nUseChips++;
					txtUsed.Text = nUseChips.ToString();
				}
				nTotalChips--;
				txtFill.Text = nTotalChips.ToString();
			}
		}

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected I4, but got Unknown
			//IL_004e: Incompatible stack heights: 0 vs 1
			//IL_0060: Incompatible stack heights: 0 vs 3
			if (_003F348_003F.Key == Key.Return)
			{
				TextBox textBox = (TextBox)_003F347_003F;
				((P_ChipGame)/*Error near IL_0012: Stack underflow*/)._003F147_003F(textBox);
				int num = (int)textBox.Tag;
				if (num < ChipFills.Count - 1)
				{
					List<TextBox> chipFill = ChipFills;
					_003F val = /*Error near IL_0032: Stack underflow*/+ /*Error near IL_0032: Stack underflow*/;
					((List<TextBox>)/*Error near IL_0037: Stack underflow*/)[(int)val].Focus();
				}
				else
				{
					ChipFills[0].Focus();
				}
			}
		}

		private void _003F147_003F(TextBox _003F379_003F)
		{
			if (_003F379_003F.Text == _003F487_003F._003F488_003F(""))
			{
				_003F379_003F.Text = _003F487_003F._003F488_003F("1");
			}
			nUseChips = 0;
			foreach (TextBox chipFill in ChipFills)
			{
				nUseChips += _003F96_003F(chipFill.Text);
			}
			nTotalChips = nMaxChips - nUseChips;
			txtUsed.Text = nUseChips.ToString();
			txtFill.Text = nTotalChips.ToString();
		}

		private void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_00b1: Incompatible stack heights: 0 vs 1
			//IL_00cc: Incompatible stack heights: 0 vs 1
			//IL_00e7: Incompatible stack heights: 0 vs 2
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength <= 0)
			{
				return;
			}
			bool flag = false;
			string text2 = textBox.Text;
			int startIndex = offset;
			int addedLength = array[0].AddedLength;
			string a = ((string)/*Error near IL_004a: Stack underflow*/).Substring(startIndex, addedLength).Trim();
			if (!(a == _003F487_003F._003F488_003F("")))
			{
				bool flag2 = a == _003F487_003F._003F488_003F("/");
				if ((int)/*Error near IL_00d1: Stack underflow*/ == 0)
				{
					double result = 0.0;
					flag = !double.TryParse(textBox.Text, out result);
					goto IL_008f;
				}
			}
			flag = true;
			goto IL_008f;
			IL_008f:
			if (flag)
			{
				string text = ((TextBox)/*Error near IL_009a: Stack underflow*/).Text.Remove(offset, array[0].AddedLength);
				((TextBox)/*Error near IL_00fa: Stack underflow*/).Text = text;
				textBox.Select(offset, 0);
			}
		}

		private bool _003F87_003F()
		{
			int num = 0;
			if (oQuestion.QCircleDefine.CONTROL_TYPE > 0)
			{
				num = oQuestion.QCircleDefine.CONTROL_TYPE;
			}
			else if (oQuestion.QCircleDefine.CONTROL_TYPE < 0)
			{
				num = -oQuestion.QCircleDefine.CONTROL_TYPE;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (TextBox chipFill in ChipFills)
			{
				if (chipFill.Text == _003F487_003F._003F488_003F(""))
				{
					chipFill.Text = _003F487_003F._003F488_003F("1");
				}
				string text = chipFill.Text;
				int num5 = _003F96_003F(text);
				if (oQuestion.QCircleDefine.CONTROL_TYPE != 0 && num5 > 0)
				{
					if (dictionary.ContainsKey(text))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string key = text;
						dictionary2[key]++;
						if (dictionary[text] >= num)
						{
							if (oQuestion.QCircleDefine.CONTROL_TYPE > 0)
							{
								MessageBox.Show(string.Format(SurveyMsg.MsgNotOverOption, dictionary[text].ToString(), text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								chipFill.Focus();
								return true;
							}
							if (oQuestion.QCircleDefine.CONTROL_TYPE < 0 && CheckSameChips)
							{
								if (MessageBox.Show(string.Format(SurveyMsg.MsgNotOverOptionWeak, dictionary[text], text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
								{
									chipFill.Focus();
									return true;
								}
								CheckSameChips = false;
							}
						}
					}
					else
					{
						dictionary.Add(text, 1);
					}
				}
				if (num5 < nMin)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, nMin.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					chipFill.Focus();
					return true;
				}
				if (num5 > nMax)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, nMax.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					chipFill.Focus();
					return true;
				}
				if (num2 > 0 && oQuestion.QCircleDefine.CONTROL_MASK == _003F487_003F._003F488_003F("3"))
				{
					if (num4 < num5)
					{
						MessageBox.Show(string.Format(SurveyMsg.MsgNeedMore, ChipTexts[num2].Text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						chipFill.Focus();
						return true;
					}
				}
				else if (num2 > 0 && oQuestion.QCircleDefine.CONTROL_MASK == _003F487_003F._003F488_003F("2") && CheckChipsLogic && num4 < num5)
				{
					if (MessageBox.Show(string.Format(SurveyMsg.MsgNeedMoreWeak, ChipTexts[num2].Text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No) != MessageBoxResult.Yes)
					{
						chipFill.Focus();
						return true;
					}
					CheckChipsLogic = false;
				}
				num4 = num5;
				num3 += num5;
				oQuestion.FillTexts[num2] = text;
				num2++;
			}
			if (oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("1") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("/İ") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("/ĳ") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("/Ĳ") || oQuestion.QCircleDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("/ĵ"))
			{
				if (num3 == 0)
				{
					MessageBox.Show(SurveyMsg.MsgNotOptionZero, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					ChipFills[0].Focus();
					return true;
				}
			}
			else
			{
				nUseChips = num3;
				nTotalChips = nMaxChips - nUseChips;
				txtUsed.Text = nUseChips.ToString();
				txtFill.Text = nTotalChips.ToString();
				if (nTotalChips > 0)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgNeedFinishNum, txtFill.Text), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					ChipFills[0].Focus();
					return true;
				}
				if (nTotalChips < 0)
				{
					MessageBox.Show(SurveyMsg.MsgNeedFinishRight, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					ChipFills[0].Focus();
					return true;
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
				string text = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ł") + oQuestion.QCircleDetails[num].CODE;
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
			Dictionary<string, string> dictionary2 = oFunc.SortDictSDbyValue(dictionary, true, false, _003F487_003F._003F488_003F("-"));
			num = 1;
			foreach (string key in dictionary2.Keys)
			{
				string text = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + num.ToString();
				VEAnswer vEAnswer2 = new VEAnswer();
				vEAnswer2.QUESTION_NAME = text;
				vEAnswer2.CODE = dictionary2[key];
				list.Add(vEAnswer2);
				num++;
			}
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			//IL_005d: Incompatible stack heights: 0 vs 2
			oQuestion.BeforeSave(1);
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (oQuestion.QCircleDefine.PAGE_COUNT_DOWN > 0)
			{
				PageNav oPageNav2 = oPageNav;
				QMatrixFill oQuestion2 = oQuestion;
				int pAGE_COUNT_DOWN = ((QMatrixFill)/*Error near IL_003d: Stack underflow*/).QCircleDefine.PAGE_COUNT_DOWN;
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
				string content = ((P_ChipGame)/*Error near IL_0040: Stack underflow*/).btnNav_Content;
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
				((P_ChipGame)/*Error near IL_0010: Stack underflow*/).btnNav.Foreground = Brushes.Black;
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
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			int index = (int)((TextBox)_003F347_003F).Tag;
			ChipTexts[index].Foreground = foreground;
			if (SurveyHelper.IsTouch == _003F487_003F._003F488_003F("EŸɞ\u0366ѽդٮݚ\u0870\u0971\u0a77\u0b64"))
			{
				SurveyTaptip.HideInputPanel();
			}
			_003F147_003F((TextBox)_003F347_003F);
		}

		private void _003F91_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			TextBox textBox = (TextBox)_003F347_003F;
			int index = (int)textBox.Tag;
			ChipTexts[index].Foreground = foreground;
			if (!(textBox.Text == _003F487_003F._003F488_003F("1")))
			{
				textBox.SelectAll();
			}
			else
			{
				textBox.Text = _003F487_003F._003F488_003F("");
			}
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a7f᭑ᱮ\u1d64Ṣὺ\u206eⅩ≪⍣\u242b╼♢❯⡭"), UriKind.Relative);
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
				((P_ChipGame)_003F350_003F).Loaded += _003F80_003F;
				((P_ChipGame)_003F350_003F).LayoutUpdated += _003F99_003F;
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
				stackPanel1 = (StackPanel)_003F350_003F;
				break;
			case 7:
				used = (StackPanel)_003F350_003F;
				break;
			case 8:
				txtBefore1 = (TextBlock)_003F350_003F;
				break;
			case 9:
				txtUsed = (TextBlock)_003F350_003F;
				break;
			case 10:
				txtAfter1 = (TextBlock)_003F350_003F;
				break;
			case 11:
				unused = (StackPanel)_003F350_003F;
				break;
			case 12:
				txtBefore = (TextBlock)_003F350_003F;
				break;
			case 13:
				txtFill = (TextBlock)_003F350_003F;
				break;
			case 14:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 15:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 16:
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
