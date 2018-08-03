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
	public class SinglePoint : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__18_0;

			internal int _003F325_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private List<string> listPreSet = new List<string>();

		private List<Button> listButton = new List<Button>();

		private int Button_Type;

		private int Button_Height;

		private double Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal Grid gridContent;

		internal RowDefinition PicHeight;

		internal RowDefinition ButtonHeight;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ScrollViewer scrollPic;

		internal StackPanel Picture;

		internal Grid GridContent;

		internal TextBlock txt1;

		internal TextBlock txt3;

		internal TextBlock txt4;

		internal TextBlock txt5;

		internal TextBlock txt6;

		internal TextBlock txt7;

		internal TextBlock txt9;

		internal WrapPanel wrapPanel1;

		internal WrapPanel wrapOther;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public SinglePoint()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0576: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_104e: Incompatible stack heights: 0 vs 2
			//IL_1065: Incompatible stack heights: 0 vs 1
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
				string text6 = list3[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_057e: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F(""))
			{
				oQuestion.InitCircle();
			}
			string text = _003F487_003F._003F488_003F("");
			List<SurveyDetail>.Enumerator enumerator;
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				text = oLogicEngine.Route(oQuestion.QDefine.CONTROL_TOOLTIP);
			}
			else if (oQuestion.QDefine.GROUP_LEVEL != _003F487_003F._003F488_003F("") && oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
			{
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
					PicHeight.Height = new GridLength(1.0, GridUnitType.Star);
					ButtonHeight.Height = GridLength.Auto;
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
							scrollPic.Height = (double)num;
						}
					}
					else
					{
						int num2 = _003F96_003F(cONTROL_MASK);
						if (num2 > 0)
						{
							image.Height = (double)num2;
						}
					}
				}
				image.Stretch = Stretch.Uniform;
				image.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
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
			string text4 = oQuestion.QDefine.NOTE;
			if (text4 == _003F487_003F._003F488_003F(""))
			{
				string text5 = _003F487_003F._003F488_003F("");
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("@"))
				{
					text5 = MyNav.CircleACode;
				}
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					text5 = MyNav.CircleBCode;
				}
				if (text5 != _003F487_003F._003F488_003F(""))
				{
					enumerator = oQuestion.QCircleDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							if (current2.CODE == text5)
							{
								text4 = current2.EXTEND_2;
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
			list3 = new List<string>(text4.Split(new string[1]
			{
				_003F487_003F._003F488_003F("-Į")
			}, StringSplitOptions.RemoveEmptyEntries));
			txt1.Text = _003F487_003F._003F488_003F("");
			txt3.Text = _003F487_003F._003F488_003F("");
			txt4.Text = _003F487_003F._003F488_003F("");
			txt5.Text = _003F487_003F._003F488_003F("");
			txt6.Text = _003F487_003F._003F488_003F("");
			txt7.Text = _003F487_003F._003F488_003F("");
			txt9.Text = _003F487_003F._003F488_003F("");
			if (list3.Count == 2)
			{
				txt1.Text = list3[0];
				txt9.Text = list3[1];
			}
			else if (list3.Count == 3)
			{
				txt1.Text = list3[0];
				txt5.Text = list3[1];
				txt9.Text = list3[2];
			}
			else if (list3.Count == 4)
			{
				txt1.Text = list3[0];
				txt4.Text = list3[1];
				txt6.Text = list3[2];
				txt9.Text = list3[3];
			}
			else if (list3.Count == 5)
			{
				txt1.Text = list3[0];
				txt3.Text = list3[1];
				txt5.Text = list3[2];
				txt7.Text = list3[3];
				txt9.Text = list3[4];
			}
			if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
			{
				txt1.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
				txt3.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
				txt4.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
				txt5.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
				txt6.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
				txt7.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
				txt9.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
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
							SurveyDetail current3 = enumerator.Current;
							if (current3.CODE == array[i].ToString())
							{
								list4.Add(current3);
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
					if (_003F7_003F._003C_003E9__18_0 == null)
					{
						_003F7_003F._003C_003E9__18_0 = _003F7_003F._003C_003E9._003F325_003F;
					}
					((List<SurveyDetail>)/*Error near IL_106a: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_106a: Stack underflow*/);
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
							SurveyDetail current4 = enumerator.Current;
							if (current4.CODE == array3[l].ToString())
							{
								list5.Add(current4);
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
			double num4 = (double)((oQuestion.QDetails.Count - 1) * 2 + 80) / 1024.0;
			Button_Height = SurveyHelper.BtnHeight;
			Button_FontSize = SurveyHelper.BtnFontSize;
			int num3 = 0;
			enumerator = oQuestion.QDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IS_OTHER == 0)
					{
						num3++;
					}
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			if (num3 > 0)
			{
				Button_Width = (GridContent.ActualWidth - (double)(num3 * 4)) / (double)num3;
			}
			else
			{
				Button_Width = GridContent.ActualWidth;
			}
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
			default:
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
			}
			if (Button_FontSize == -1)
			{
				Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			Button_FontSize = Math.Abs(Button_FontSize);
			_003F28_003F();
			if (wrapOther.Children.Count == 0)
			{
				wrapOther.Height = 0.0;
				wrapOther.Visibility = Visibility.Collapsed;
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			bool flag = false;
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
							if ((string)child.Tag == oQuestion.SelectedCode)
							{
								child.Style = style;
								break;
							}
						}
						foreach (Button child2 in wrapOther.Children)
						{
							if ((string)child2.Tag == oQuestion.SelectedCode)
							{
								child2.Style = style;
								break;
							}
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
							flag = true;
						}
					}
					if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && oQuestion.SelectedCode != _003F487_003F._003F488_003F(""))
					{
						flag = true;
					}
					if (SurveyHelper.AutoFill)
					{
						AutoFill autoFill = new AutoFill();
						autoFill.oLogicEngine = oLogicEngine;
						if (oQuestion.SelectedCode == _003F487_003F._003F488_003F(""))
						{
							Button button3 = autoFill.SingleButton(oQuestion.QDefine, listButton);
							if (button3 != null && listPreSet.Count == 0 && button3.Style == style2)
							{
								_003F29_003F(button3, null);
							}
						}
						if (oQuestion.SelectedCode != _003F487_003F._003F488_003F("") && !flag && autoFill.AutoNext(oQuestion.QDefine))
						{
							flag = true;
						}
					}
					if (flag)
					{
						_003F58_003F(this, _003F348_003F);
					}
				}
			}
			else
			{
				oQuestion.SelectedCode = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName);
				foreach (Button child3 in wrapPanel1.Children)
				{
					if ((string)child3.Tag == oQuestion.SelectedCode)
					{
						child3.Style = style;
						break;
					}
				}
				foreach (Button child4 in wrapOther.Children)
				{
					if ((string)child4.Tag == oQuestion.SelectedCode)
					{
						child4.Style = style;
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
				button.Margin = new Thickness(2.0, 2.0, 2.0, 2.0);
				button.Style = style;
				button.Tag = qDetail.CODE;
				button.Click += _003F29_003F;
				button.FontSize = (double)Button_FontSize;
				button.MinWidth = Button_Width;
				button.MinHeight = (double)Button_Height;
				if (qDetail.IS_OTHER == 0)
				{
					wrapPanel.Children.Add(button);
				}
				else
				{
					wrapOther.Children.Add(button);
				}
				listButton.Add(button);
				if (oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("@") || oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("C"))
				{
					string item = _003F487_003F._003F488_003F("");
					if (oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("@"))
					{
						item = SurveyHelper.CircleACode;
					}
					else if (oQuestion.QDefine.GROUP_LEVEL == _003F487_003F._003F488_003F("C"))
					{
						item = SurveyHelper.CircleBCode;
					}
					if (qDetail.IS_OTHER == 2)
					{
						button.Visibility = Visibility.Hidden;
						if (qDetail.EXTEND_2 != _003F487_003F._003F488_003F("") && oBoldTitle.ParaToList(qDetail.EXTEND_2, _003F487_003F._003F488_003F("-Į")).Contains(item))
						{
							button.Visibility = Visibility.Visible;
						}
					}
					else if (qDetail.IS_OTHER == 3 && qDetail.EXTEND_3 != _003F487_003F._003F488_003F("") && oBoldTitle.ParaToList(qDetail.EXTEND_3, _003F487_003F._003F488_003F("-Į")).Contains(item))
					{
						button.Visibility = Visibility.Hidden;
					}
				}
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00c4: Incompatible stack heights: 0 vs 1
			//IL_00d4: Incompatible stack heights: 0 vs 1
			//IL_00d5: Incompatible stack heights: 0 vs 1
			//IL_0168: Incompatible stack heights: 0 vs 1
			//IL_0178: Incompatible stack heights: 0 vs 1
			//IL_0179: Incompatible stack heights: 0 vs 1
			Button obj = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			string text = (string)obj.Tag;
			int num = 0;
			if (obj.Style == style)
			{
				num = 1;
			}
			if (num == 0)
			{
				oQuestion.SelectedCode = text;
				foreach (Button child in wrapPanel1.Children)
				{
					string a = (string)child.Tag;
					if (a == text)
					{
					}
					((FrameworkElement)/*Error near IL_00da: Stack underflow*/).Style = (Style)/*Error near IL_00da: Stack underflow*/;
				}
				foreach (Button child2 in wrapOther.Children)
				{
					string a2 = (string)child2.Tag;
					if (a2 == text)
					{
					}
					((FrameworkElement)/*Error near IL_017e: Stack underflow*/).Style = (Style)/*Error near IL_017e: Stack underflow*/;
				}
			}
			if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode4) && oQuestion.SelectedCode != _003F487_003F._003F488_003F(""))
			{
				_003F58_003F(this, _003F348_003F);
			}
		}

		private bool _003F87_003F()
		{
			//IL_0040: Incompatible stack heights: 0 vs 3
			if (oQuestion.SelectedCode == _003F487_003F._003F488_003F(""))
			{
				string msgNotSelected = SurveyMsg.MsgNotSelected;
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0026: Stack underflow*/, (string)/*Error near IL_0026: Stack underflow*/, (MessageBoxButton)/*Error near IL_0026: Stack underflow*/, MessageBoxImage.Hand);
				return true;
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> result = new List<VEAnswer>
			{
				new VEAnswer
				{
					QUESTION_NAME = oQuestion.QuestionName,
					CODE = oQuestion.SelectedCode
				}
			};
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.SelectedCode;
			return result;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a63᭦ᱠ\u1d6aṠὮ⁺Ⅶ≡⍩⑲┫♼❢⡯⥭"), UriKind.Relative);
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
				((SinglePoint)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				gridContent = (Grid)_003F350_003F;
				break;
			case 3:
				PicHeight = (RowDefinition)_003F350_003F;
				break;
			case 4:
				ButtonHeight = (RowDefinition)_003F350_003F;
				break;
			case 5:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 6:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 7:
				scrollPic = (ScrollViewer)_003F350_003F;
				break;
			case 8:
				Picture = (StackPanel)_003F350_003F;
				break;
			case 9:
				GridContent = (Grid)_003F350_003F;
				break;
			case 10:
				txt1 = (TextBlock)_003F350_003F;
				break;
			case 11:
				txt3 = (TextBlock)_003F350_003F;
				break;
			case 12:
				txt4 = (TextBlock)_003F350_003F;
				break;
			case 13:
				txt5 = (TextBlock)_003F350_003F;
				break;
			case 14:
				txt6 = (TextBlock)_003F350_003F;
				break;
			case 15:
				txt7 = (TextBlock)_003F350_003F;
				break;
			case 16:
				txt9 = (TextBlock)_003F350_003F;
				break;
			case 17:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 18:
				wrapOther = (WrapPanel)_003F350_003F;
				break;
			case 19:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 20:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 21:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0061:
			goto IL_006b;
		}
	}
}
