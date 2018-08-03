using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class P_RankBrand_H : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__25_0;

			public static Comparison<SurveyDetail> _003C_003E9__25_1;

			internal int _003F323_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal int _003F324_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QMatrixSingle oQuestion = new QMatrixSingle();

		private int nCurrent;

		private List<TextBlock> txtOrders = new List<TextBlock>();

		private List<Button> btnOrders = new List<Button>();

		private Dictionary<string, Button> btnBrands = new Dictionary<string, Button>();

		private List<Button> listButton = new List<Button>();

		private bool PageLoaded;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize = 40;

		private int Text_Height;

		private int Text_Width;

		private int Text_FontSize = 40;

		private int Button_Type;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ColumnDefinition BrandArea;

		internal ScrollViewer scrollmain;

		internal WrapPanel wrapPanel1;

		internal StackPanel stackPanel1;

		internal TextBlock txtBefore;

		internal TextBlock txtFill;

		internal TextBlock txtAfter;

		internal ScrollViewer scrollcode;

		internal WrapPanel wrapPanel2;

		internal TextBlock txtSurvey;

		internal Button btnNav;

		private bool _contentLoaded;

		public P_RankBrand_H()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0449: Incompatible stack heights: 0 vs 1
			//IL_0450: Incompatible stack heights: 0 vs 1
			//IL_05dd: Incompatible stack heights: 0 vs 2
			//IL_05f4: Incompatible stack heights: 0 vs 1
			//IL_0913: Incompatible stack heights: 0 vs 2
			//IL_092a: Incompatible stack heights: 0 vs 1
			//IL_0a3d: Incompatible stack heights: 0 vs 1
			//IL_0a51: Incompatible stack heights: 0 vs 1
			//IL_0a53: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0, false);
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
			qUESTION_TITLE = (string)/*Error near IL_0451: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			List<SurveyDetail>.Enumerator enumerator;
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
				if (oQuestion.QDefine.SHOW_LOGIC == _003F487_003F._003F488_003F("") && oQuestion.QDefine.IS_RANDOM == 0)
				{
					if (_003F7_003F._003C_003E9__25_0 == null)
					{
						_003F7_003F._003C_003E9__25_0 = _003F7_003F._003C_003E9._003F323_003F;
					}
					((List<SurveyDetail>)/*Error near IL_05f9: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_05f9: Stack underflow*/);
				}
				oQuestion.QDetails = list3;
			}
			if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int j = 0; j < oQuestion.QDetails.Count(); j++)
				{
					oQuestion.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[j].CODE_TEXT);
				}
			}
			if (list[0].Trim() != _003F487_003F._003F488_003F(""))
			{
				string[] array2 = oLogicEngine.aryCode(list[0], ',');
				List<SurveyDetail> list4 = new List<SurveyDetail>();
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
				oQuestion.QDetails = list4;
			}
			else if (oQuestion.QDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomDetails(1);
			}
			if (oQuestion.QCircleDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array3 = oLogicEngine.aryCode(oQuestion.QCircleDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count(); l++)
				{
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
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
				if (_003F7_003F._003C_003E9__25_1 == null)
				{
					_003F7_003F._003C_003E9__25_1 = _003F7_003F._003C_003E9._003F324_003F;
				}
				((List<SurveyDetail>)/*Error near IL_092f: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_092f: Stack underflow*/);
				oQuestion.QCircleDetails = list5;
			}
			if (oQuestion.QCircleDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int m = 0; m < oQuestion.QCircleDetails.Count(); m++)
				{
					oQuestion.QCircleDetails[m].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QCircleDetails[m].CODE_TEXT);
				}
			}
			Button_Width = SurveyHelper.BtnWidth;
			Button_Height = SurveyHelper.BtnHeight;
			Button_FontSize = SurveyHelper.BtnFontSize;
			if (!(oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == _003F487_003F._003F488_003F("I")))
			{
				int btnWidth = SurveyHelper.BtnWidth;
			}
			((P_RankBrand_H)/*Error near IL_0a58: Stack underflow*/).Text_Width = (int)/*Error near IL_0a58: Stack underflow*/;
			Text_Height = SurveyHelper.BtnHeight;
			Text_FontSize = SurveyHelper.BtnFontSize;
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				Button_Height = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				Button_Width = oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				Button_FontSize = oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			if (oQuestion.QCircleDefine.CONTROL_HEIGHT != 0)
			{
				Text_Height = oQuestion.QCircleDefine.CONTROL_HEIGHT;
			}
			if (oQuestion.QCircleDefine.CONTROL_WIDTH != 0)
			{
				Text_Width = oQuestion.QCircleDefine.CONTROL_WIDTH;
			}
			if (oQuestion.QCircleDefine.CONTROL_FONTSIZE != 0)
			{
				Text_FontSize = oQuestion.QCircleDefine.CONTROL_FONTSIZE;
			}
			_003F28_003F();
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQuestion.QDefine.NOTE;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
				if (list2.Count > 1)
				{
					qUESTION_TITLE = list2[1];
					oBoldTitle.SetTextBlock(txtAfter, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
				}
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				foreach (Button item in listButton)
				{
					_003F151_003F(item, new RoutedEventArgs());
				}
				if (listButton.Count > 0 && autoFill.AutoNext(oQuestion.QDefine))
				{
					_003F58_003F(this, _003F348_003F);
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
			//IL_00cf: Incompatible stack heights: 0 vs 2
			//IL_00e5: Incompatible stack heights: 0 vs 2
			//IL_0105: Incompatible stack heights: 0 vs 1
			//IL_010b: Incompatible stack heights: 0 vs 1
			//IL_0110: Incompatible stack heights: 1 vs 0
			//IL_011c: Incompatible stack heights: 0 vs 2
			//IL_014c: Incompatible stack heights: 0 vs 2
			if (PageLoaded)
			{
				WrapPanel wrapPanel = wrapPanel2;
				SurveyDefine qDefine = oQuestion.QDefine;
				int cONTROL_TYPE = ((SurveyDefine)/*Error near IL_0010: Stack underflow*/).CONTROL_TYPE;
				((P_RankBrand_H)/*Error near IL_0015: Stack underflow*/).Button_Type = cONTROL_TYPE;
				if (Button_Type != 0)
				{
					if (Button_Type == 2)
					{
					}
					((WrapPanel)/*Error near IL_005c: Stack underflow*/).Orientation = Orientation.Vertical;
				}
				else
				{
					Visibility computedVerticalScrollBarVisibility = scrollmain.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_00ea: Stack underflow*/ == /*Error near IL_00ea: Stack underflow*/)
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
				if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
				{
					string cONTROL_TOOLTIP = ((P_RankBrand_H)/*Error near IL_008a: Stack underflow*/).oQuestion.QDefine.CONTROL_TOOLTIP;
					int num = ((P_RankBrand_H)/*Error near IL_0099: Stack underflow*/)._003F96_003F(cONTROL_TOOLTIP);
					if (num == 1)
					{
						BrandArea.Width = GridLength.Auto;
					}
					else if (num > 1)
					{
						ColumnDefinition brandArea = BrandArea;
						GridLength width = new GridLength((double)/*Error near IL_014d: Stack underflow*/);
						((ColumnDefinition)/*Error near IL_0157: Stack underflow*/).Width = width;
					}
				}
				new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
				PageLoaded = false;
			}
		}

		private void _003F28_003F()
		{
			//IL_0328: Incompatible stack heights: 0 vs 1
			//IL_0338: Incompatible stack heights: 0 vs 1
			//IL_0339: Incompatible stack heights: 0 vs 1
			//IL_0463: Incompatible stack heights: 0 vs 1
			//IL_0473: Incompatible stack heights: 0 vs 1
			//IL_0475: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Style style3 = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
			Brush brush2 = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			Brush brush = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			WrapPanel wrapPanel = this.wrapPanel2;
			int num = 0;
			List<SurveyDetail>.Enumerator enumerator = oQuestion.QDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current = enumerator.Current;
					string cODE = current.CODE;
					string cODE_TEXT = current.CODE_TEXT;
					Button button = new Button();
					button.Content = cODE_TEXT;
					button.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
					button.Style = style2;
					button.Tag = cODE;
					button.Click += _003F151_003F;
					button.FontSize = (double)Button_FontSize;
					button.MinHeight = (double)Button_Height;
					button.MinWidth = (double)Button_Width;
					wrapPanel.Children.Add(button);
					btnBrands.Add(cODE, button);
					listButton.Add(button);
					num++;
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			wrapPanel = wrapPanel1;
			bool flag = true;
			num = 0;
			enumerator = oQuestion.QCircleDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current2 = enumerator.Current;
					string cODE2 = current2.CODE;
					string cODE_TEXT2 = current2.CODE_TEXT;
					string text = _003F487_003F._003F488_003F("");
					string content = _003F487_003F._003F488_003F("");
					if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
					{
						string _003F285_003F = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + cODE2;
						text = oQuestion.ReadAnswerByQuestionName(MySurveyId, _003F285_003F);
						if (text != _003F487_003F._003F488_003F(""))
						{
							if (!btnBrands.ContainsKey(text))
							{
								text = _003F487_003F._003F488_003F("");
							}
							if (text != _003F487_003F._003F488_003F(""))
							{
								content = (string)btnBrands[text].Content;
							}
						}
					}
					WrapPanel wrapPanel2 = new WrapPanel();
					if (!(oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == _003F487_003F._003F488_003F("I")))
					{
					}
					((WrapPanel)/*Error near IL_033e: Stack underflow*/).Orientation = (Orientation)/*Error near IL_033e: Stack underflow*/;
					wrapPanel2.Margin = new Thickness(5.0, 5.0, 5.0, 5.0);
					wrapPanel2.VerticalAlignment = VerticalAlignment.Center;
					wrapPanel2.HorizontalAlignment = HorizontalAlignment.Center;
					wrapPanel.Children.Add(wrapPanel2);
					WrapPanel wrapPanel3 = new WrapPanel();
					wrapPanel3.VerticalAlignment = VerticalAlignment.Center;
					if (oQuestion.QDefine.CONTROL_MASK.Trim().ToUpper() == _003F487_003F._003F488_003F("I"))
					{
						wrapPanel3.HorizontalAlignment = HorizontalAlignment.Right;
					}
					wrapPanel3.Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
					wrapPanel3.MinWidth = (double)Text_Width;
					wrapPanel2.Children.Add(wrapPanel3);
					TextBlock textBlock = new TextBlock();
					textBlock.Text = cODE_TEXT2;
					textBlock.Style = style3;
					if (!(text == _003F487_003F._003F488_003F("")))
					{
					}
					((TextBlock)/*Error near IL_047a: Stack underflow*/).Foreground = (Brush)/*Error near IL_047a: Stack underflow*/;
					textBlock.TextWrapping = TextWrapping.Wrap;
					textBlock.HorizontalAlignment = HorizontalAlignment.Right;
					wrapPanel3.Children.Add(textBlock);
					txtOrders.Add(textBlock);
					Button button2 = new Button();
					button2.Content = content;
					button2.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
					button2.Style = style2;
					button2.Tag = num;
					button2.Click += _003F150_003F;
					button2.FontSize = (double)Text_FontSize;
					button2.MinHeight = (double)Text_Height;
					button2.MinWidth = (double)Button_Width;
					wrapPanel2.Children.Add(button2);
					btnOrders.Add(button2);
					oQuestion.SelectedCode.Add(text);
					if (text != _003F487_003F._003F488_003F(""))
					{
						btnBrands[text].Style = style;
						btnBrands[text].Visibility = Visibility.Collapsed;
					}
					if (flag)
					{
						txtFill.Text = cODE_TEXT2;
						nCurrent = num;
						button2.Style = style;
						txtOrders[num].Foreground = foreground;
						flag = false;
					}
					num++;
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private void _003F150_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			Brush foreground2 = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			Button button = (Button)_003F347_003F;
			int index = (int)button.Tag;
			string text = (string)button.Content;
			nCurrent = index;
			txtFill.Text = oQuestion.QCircleDetails[nCurrent].CODE_TEXT;
			foreach (Button btnOrder in btnOrders)
			{
				btnOrder.Style = style2;
			}
			button.Style = style;
			if (oQuestion.SelectedCode[index] != _003F487_003F._003F488_003F(""))
			{
				btnBrands[oQuestion.SelectedCode[index]].Style = style2;
				btnBrands[oQuestion.SelectedCode[index]].Visibility = Visibility.Visible;
				button.Content = _003F487_003F._003F488_003F("");
				oQuestion.SelectedCode[index] = _003F487_003F._003F488_003F("");
			}
			int num = 0;
			foreach (TextBlock txtOrder in txtOrders)
			{
				if (oQuestion.SelectedCode[num] == _003F487_003F._003F488_003F(""))
				{
					txtOrder.Foreground = foreground2;
				}
				else
				{
					txtOrder.Foreground = foreground;
				}
				num++;
			}
			txtOrders[nCurrent].Foreground = foreground;
		}

		private void _003F151_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			Brush brush = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			Button button = (Button)_003F347_003F;
			if (button.Style != style)
			{
				button.Style = style;
				button.Visibility = Visibility.Collapsed;
				if (oQuestion.SelectedCode[nCurrent] != _003F487_003F._003F488_003F(""))
				{
					btnBrands[oQuestion.SelectedCode[nCurrent]].Visibility = Visibility.Visible;
					btnBrands[oQuestion.SelectedCode[nCurrent]].Style = style2;
				}
				string value = (string)button.Tag;
				oQuestion.SelectedCode[nCurrent] = value;
				btnOrders[nCurrent].Content = button.Content;
				int num = 0;
				foreach (Button btnOrder in btnOrders)
				{
					Button button2 = btnOrder;
					if (oQuestion.SelectedCode[num] == _003F487_003F._003F488_003F(""))
					{
						btnOrders[nCurrent].Style = style2;
						txtFill.Text = oQuestion.QCircleDetails[num].CODE_TEXT;
						btnOrders[num].Style = style;
						btnOrders[num].Focus();
						txtOrders[num].Foreground = foreground;
						nCurrent = num;
						break;
					}
					num++;
				}
			}
		}

		private void _003F152_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0156: Incompatible stack heights: 0 vs 2
			//IL_016c: Incompatible stack heights: 0 vs 1
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Brush brush = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			Brush foreground = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			int num = nCurrent;
			num = nCurrent;
			goto IL_011f;
			IL_011f:
			for (; num < btnOrders.Count; num++)
			{
				if (oQuestion.SelectedCode[num] != _003F487_003F._003F488_003F(""))
				{
					Dictionary<string, Button> btnBrand = btnBrands;
					string text = oQuestion.SelectedCode[num];
					((Dictionary<string, Button>)/*Error near IL_0099: Stack underflow*/)[(string)/*Error near IL_0099: Stack underflow*/].Style = style;
					btnBrands[oQuestion.SelectedCode[num]].Visibility = Visibility.Visible;
					btnOrders[num].Content = _003F487_003F._003F488_003F("");
					oQuestion.SelectedCode[num] = _003F487_003F._003F488_003F("");
				}
				if (num > nCurrent)
				{
					Button button = btnOrders[num];
					((FrameworkElement)/*Error near IL_0109: Stack underflow*/).Style = style;
					txtOrders[num].Foreground = foreground;
				}
			}
			return;
			IL_0172:
			goto IL_011f;
		}

		private bool _003F87_003F()
		{
			UpdateLayout();
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			Brush foreground2 = (Brush)new BrushConverter().ConvertFromString(_003F487_003F._003F488_003F("RŬɪͶѤ"));
			int num = 0;
			foreach (string item in oQuestion.SelectedCode)
			{
				if (item == _003F487_003F._003F488_003F(""))
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgSelectFixAnswer, oQuestion.QCircleDetails[num].CODE_TEXT), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					nCurrent = num;
					txtFill.Text = oQuestion.QCircleDetails[nCurrent].CODE_TEXT;
					foreach (Button btnOrder in btnOrders)
					{
						btnOrder.Style = style2;
					}
					btnOrders[nCurrent].Style = style;
					btnOrders[nCurrent].Focus();
					int num2 = 0;
					foreach (TextBlock txtOrder in txtOrders)
					{
						if (oQuestion.SelectedCode[num2] == _003F487_003F._003F488_003F(""))
						{
							txtOrder.Foreground = foreground2;
						}
						else
						{
							txtOrder.Foreground = foreground;
						}
						num2++;
					}
					txtOrders[nCurrent].Foreground = foreground;
					return true;
				}
				num++;
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			//IL_0053: Incompatible stack heights: 0 vs 1
			//IL_0063: Incompatible stack heights: 0 vs 1
			List<VEAnswer> list = new List<VEAnswer>();
			if (!(oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("@")))
			{
				string gROUP_CODEB = oQuestion.QDefine.GROUP_CODEB;
			}
			else
			{
				string gROUP_CODEA = oQuestion.QDefine.GROUP_CODEA;
			}
			string str = (string)/*Error near IL_0064: Stack underflow*/;
			str += MyNav.QName_Add;
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			int num = 0;
			foreach (string item in oQuestion.SelectedCode)
			{
				string text = oQuestion.QuestionName + _003F487_003F._003F488_003F("]œ") + oQuestion.QCircleDetails[num].CODE;
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = text;
				vEAnswer.CODE = item;
				list.Add(vEAnswer);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + text + _003F487_003F._003F488_003F("<") + item;
				text = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ł") + item;
				vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = text;
				vEAnswer.CODE = oQuestion.QCircleDetails[num].CODE;
				list.Add(vEAnswer);
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
			//IL_0074: Incompatible stack heights: 0 vs 3
			oQuestion.BeforeSavebyCode(_003F487_003F._003F488_003F(";ĸ"));
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (oQuestion.QCircleDefine.PAGE_COUNT_DOWN > 0)
			{
				PageNav oPageNav2 = oPageNav;
				int pAGE_COUNT_DOWN = oQuestion.QCircleDefine.PAGE_COUNT_DOWN;
				Button _003F431_003F = btnNav;
				string mySurveyId = MySurveyId;
				((PageNav)/*Error near IL_0052: Stack underflow*/).PageDataLog((int)/*Error near IL_0052: Stack underflow*/, (List<VEAnswer>)/*Error near IL_0052: Stack underflow*/, _003F431_003F, mySurveyId);
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00ce: Incompatible stack heights: 0 vs 2
			//IL_00e3: Incompatible stack heights: 0 vs 2
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
					string btnNav_Content2 = btnNav_Content;
					((ContentControl)/*Error near IL_007b: Stack underflow*/).Content = (object)/*Error near IL_007b: Stack underflow*/;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_00eb: Stack underflow*/, (string)/*Error near IL_00eb: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_0098:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				btnNav.Foreground = Brushes.Black;
				btnNav.Content = btnNav_Content;
				return;
			}
			goto IL_0047;
			IL_001d:
			goto IL_0047;
			IL_0047:
			SecondsCountDown--;
			btnNav.Content = SecondsCountDown.ToString();
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
			//IL_0075: Incompatible stack heights: 0 vs 1
			//IL_007a: Incompatible stack heights: 1 vs 0
			//IL_007f: Incompatible stack heights: 0 vs 2
			//IL_0085: Incompatible stack heights: 0 vs 1
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
			IL_005d:
			goto IL_0015;
			IL_0015:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_002a;
			IL_0069:
			goto IL_002a;
			IL_002a:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_003f;
			IL_0075:
			goto IL_003f;
			IL_003f:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_004b;
			IL_0081:
			goto IL_004b;
			IL_004b:
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0003Ŭə\u035aёԉ٥\u0744ࡔ\u094aਙ\u0b42\u0c4f൲\u0e6e\u0f72\u1072ᅾቴ፭ᐷᕡᙿᝰᡣ\u193c\u1a62\u1b4eᱢᵮṠὦ\u206eⅹ≫⍧⑬╘♮✫⡼⥢⩯⭭"), UriKind.Relative);
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
				((P_RankBrand_H)_003F350_003F).Loaded += _003F80_003F;
				((P_RankBrand_H)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				BrandArea = (ColumnDefinition)_003F350_003F;
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
				txtBefore = (TextBlock)_003F350_003F;
				break;
			case 9:
				txtFill = (TextBlock)_003F350_003F;
				break;
			case 10:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 11:
				((Button)_003F350_003F).Click += _003F152_003F;
				break;
			case 12:
				scrollcode = (ScrollViewer)_003F350_003F;
				break;
			case 13:
				wrapPanel2 = (WrapPanel)_003F350_003F;
				break;
			case 14:
				txtSurvey = (TextBlock)_003F350_003F;
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
