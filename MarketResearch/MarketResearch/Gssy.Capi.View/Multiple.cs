using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class Multiple : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__26_0;

			internal int _003F306_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QMultiple oQuestion = new QMultiple();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

		private List<string> listFix = new List<string>();

		private List<Button> listBtnNormal = new List<Button>();

		private bool IsFixOther;

		private bool IsFixNone;

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

		public Multiple()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0576: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_0c02: Incompatible stack heights: 0 vs 2
			//IL_0c19: Incompatible stack heights: 0 vs 1
			//IL_104e: Incompatible stack heights: 0 vs 1
			//IL_106d: Incompatible stack heights: 0 vs 1
			//IL_1072: Incompatible stack heights: 0 vs 1
			//IL_10ca: Incompatible stack heights: 0 vs 1
			//IL_10e9: Incompatible stack heights: 0 vs 1
			//IL_10ee: Incompatible stack heights: 0 vs 1
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
				if (oFunc.LEFT(text, 1) == _003F487_003F._003F488_003F("\""))
				{
					text3 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + oFunc.MID(text, 1, -9999);
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
					if (oFunc.LEFT(cONTROL_MASK, 1) == _003F487_003F._003F488_003F("\""))
					{
						cONTROL_MASK = oFunc.MID(cONTROL_MASK, 1, -9999);
						scrollPic.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
						scrollPic.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
						int num = oFunc.StringToInt(cONTROL_MASK);
						if (num > 0)
						{
							scrollPic.Width = (double)num;
						}
					}
					else
					{
						int num2 = oFunc.StringToInt(cONTROL_MASK);
						if (num2 > 0)
						{
							image.Width = (double)num2;
						}
					}
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
					scrollPic.Content = image;
				}
				catch (Exception)
				{
				}
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
					if (_003F7_003F._003C_003E9__26_0 == null)
					{
						_003F7_003F._003C_003E9__26_0 = _003F7_003F._003C_003E9._003F306_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0c1e: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0c1e: Stack underflow*/);
				}
				oQuestion.QDetails = list4;
			}
			if (oQuestion.QDefine.FIX_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array2 = oLogicEngine.aryCode(oQuestion.QDefine.FIX_LOGIC, ',');
				for (int j = 0; j < array2.Count(); j++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array2[j])
							{
								listFix.Add(array2[j]);
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
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array3 = oLogicEngine.aryCode(oQuestion.QDefine.PRESET_LOGIC, ',');
				for (int k = 0; k < array3.Count(); k++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.CODE == array3[k])
							{
								listPreSet.Add(array3[k]);
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
				for (int l = 0; l < oQuestion.QDetails.Count(); l++)
				{
					oQuestion.QDetails[l].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[l].CODE_TEXT);
				}
			}
			if (list2[0].Trim() != _003F487_003F._003F488_003F(""))
			{
				string[] array4 = oLogicEngine.aryCode(list2[0], ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int m = 0; m < array4.Count(); m++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current3 = enumerator.Current;
							if (current3.CODE == array4[m].ToString())
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
			((Multiple)/*Error near IL_1077: Stack underflow*/).Button_FontSize = (int)/*Error near IL_1077: Stack underflow*/;
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
			((Multiple)/*Error near IL_10f3: Stack underflow*/).Button_Height = (int)/*Error near IL_10f3: Stack underflow*/;
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
			if (ExistTextFill || IsFixOther)
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
				if (IsFixOther)
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
				}
			}
			else
			{
				txtFill.Height = 0.0;
				txtFillTitle.Height = 0.0;
				txtAfter.Height = 0.0;
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			bool flag = false;
			bool flag2 = false;
			string navOperation = SurveyHelper.NavOperation;
			List<Button>.Enumerator enumerator3;
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
					foreach (string item in listPreSet)
					{
						if (!listFix.Contains(item))
						{
							oQuestion.SelectedValues.Add(item);
							enumerator3 = listBtnNormal.GetEnumerator();
							try
							{
								while (enumerator3.MoveNext())
								{
									Button current5 = enumerator3.Current;
									if (current5.Name.Substring(2) == item)
									{
										current5.Style = style;
										int num3 = (int)current5.Tag;
										if (num3 == 1 || num3 == 3 || num3 == 5 || ((num3 == 11) | (num3 == 13)) || num3 == 14)
										{
											flag = true;
										}
									}
								}
							}
							finally
							{
								((IDisposable)enumerator3).Dispose();
							}
						}
					}
					if (flag)
					{
						txtFill.IsEnabled = true;
						txtFill.Background = Brushes.White;
					}
					if (oQuestion.QDetails.Count == 1 || listBtnNormal.Count == 0)
					{
						if (listBtnNormal.Count > 0 && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)) && listBtnNormal[0].Style == style2)
						{
							_003F29_003F(listBtnNormal[0], new RoutedEventArgs());
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
					if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3) && oQuestion.SelectedValues.Count > 0)
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
					if (SurveyHelper.AutoFill && !flag2)
					{
						AutoFill autoFill = new AutoFill();
						autoFill.oLogicEngine = oLogicEngine;
						listButton = autoFill.MultiButton(oQuestion.QDefine, listButton, listBtnNormal, 0);
						enumerator3 = listButton.GetEnumerator();
						try
						{
							while (enumerator3.MoveNext())
							{
								Button current6 = enumerator3.Current;
								if (current6.Style == style2)
								{
									_003F29_003F(current6, null);
								}
							}
						}
						finally
						{
							((IDisposable)enumerator3).Dispose();
						}
						if (txtFill.IsEnabled)
						{
							txtFill.Text = autoFill.CommonOther(oQuestion.QDefine, _003F487_003F._003F488_003F(""));
						}
						if (listButton.Count > 0 && autoFill.AutoNext(oQuestion.QDefine))
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
				oQuestion.ReadAnswer(MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer item2 in oQuestion.QAnswersRead)
				{
					if (oFunc.MID(item2.QUESTION_NAME, 0, (oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ")).Length) == oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ"))
					{
						if (!listFix.Contains(item2.CODE))
						{
							oQuestion.SelectedValues.Add(item2.CODE);
							enumerator3 = listBtnNormal.GetEnumerator();
							try
							{
								while (enumerator3.MoveNext())
								{
									Button current8 = enumerator3.Current;
									if (current8.Name.Substring(2) == item2.CODE)
									{
										current8.Style = style;
										int num4 = (int)current8.Tag;
										if (num4 == 1 || num4 == 3 || num4 == 5 || ((num4 == 11) | (num4 == 13)) || num4 == 14)
										{
											flag = true;
										}
									}
								}
							}
							finally
							{
								((IDisposable)enumerator3).Dispose();
							}
						}
					}
					else if (ExistTextFill && item2.QUESTION_NAME == oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349") && item2.CODE != _003F487_003F._003F488_003F(""))
					{
						txtFill.Text = item2.CODE;
					}
				}
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
			//IL_020d: Incompatible stack heights: 0 vs 1
			//IL_021d: Incompatible stack heights: 0 vs 1
			//IL_022d: Incompatible stack heights: 0 vs 1
			//IL_0239: Incompatible stack heights: 0 vs 2
			//IL_024e: Incompatible stack heights: 0 vs 1
			//IL_025f: Incompatible stack heights: 0 vs 2
			//IL_026b: Incompatible stack heights: 0 vs 2
			//IL_0288: Incompatible stack heights: 0 vs 2
			//IL_029e: Incompatible stack heights: 0 vs 2
			//IL_02b3: Incompatible stack heights: 0 vs 1
			//IL_02b8: Invalid comparison between Unknown and I4
			//IL_02d3: Incompatible stack heights: 0 vs 2
			if (oQuestion.QDetails == null)
			{
				return;
			}
			int count = oQuestion.QDetails.Count;
			if ((int)/*Error near IL_0212: Stack underflow*/ == 0)
			{
				return;
			}
			bool pageLoaded = PageLoaded;
			if ((int)/*Error near IL_0222: Stack underflow*/ == 0)
			{
				return;
			}
			WrapPanel wrapPanel2 = wrapPanel1;
			WrapPanel wrapPanel = (WrapPanel)/*Error near IL_001b: Stack underflow*/;
			ScrollViewer scrollViewer = scrollmain;
			if (Button_Type < -20)
			{
				((ScrollViewer)/*Error near IL_0034: Stack underflow*/).VerticalScrollBarVisibility = (ScrollBarVisibility)/*Error near IL_0034: Stack underflow*/;
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
						if ((int)/*Error near IL_02b8: Stack underflow*/ != 4)
						{
							wrapPanel.Orientation = Orientation.Horizontal;
							goto IL_01c9;
						}
					}
					wrapPanel.Orientation = Orientation.Vertical;
					goto IL_01c9;
				}
				((ScrollViewer)/*Error near IL_017d: Stack underflow*/).VerticalScrollBarVisibility = (ScrollBarVisibility)/*Error near IL_017d: Stack underflow*/;
				scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
				wrapPanel.Orientation = Orientation.Vertical;
				wrapPanel.Height = (double)Button_Type;
				PageLoaded = false;
			}
			else
			{
				int button_Type2 = Button_Type;
				if ((int)/*Error near IL_0253: Stack underflow*/ == 0)
				{
					Visibility computedVerticalScrollBarVisibility = scrollViewer.ComputedVerticalScrollBarVisibility;
					if (/*Error near IL_0264: Stack underflow*/ != /*Error near IL_0264: Stack underflow*/)
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
						((Multiple)/*Error near IL_0077: Stack underflow*/).Button_Type = (int)/*Error near IL_0077: Stack underflow*/;
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
					((Multiple)/*Error near IL_0132: Stack underflow*/).PageLoaded = ((byte)/*Error near IL_0132: Stack underflow*/ != 0);
				}
			}
			goto IL_02f6;
			IL_01c9:
			if (Button_Type != 3)
			{
				int button_Type3 = Button_Type;
				if (/*Error near IL_02d8: Stack underflow*/ != /*Error near IL_02d8: Stack underflow*/)
				{
					scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
					scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
					goto IL_02ef;
				}
			}
			scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
			scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
			goto IL_02ef;
			IL_02ef:
			PageLoaded = false;
			goto IL_02f6;
			IL_02f6:
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F28_003F()
		{
			//IL_042a: Incompatible stack heights: 0 vs 1
			//IL_043a: Incompatible stack heights: 0 vs 1
			//IL_043b: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			WrapPanel wrapPanel = wrapPanel1;
			List<SurveyDetail>.Enumerator enumerator;
			if (listFix.Count > 0)
			{
				enumerator = oQuestion.QDetails.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						SurveyDetail current = enumerator.Current;
						if (listFix.Contains(current.CODE))
						{
							if (current.IS_OTHER == 1 || current.IS_OTHER == 3 || ((current.IS_OTHER == 11) | (current.IS_OTHER == 5)) || current.IS_OTHER == 13 || current.IS_OTHER == 14)
							{
								IsFixOther = true;
							}
							if (current.IS_OTHER == 2 || current.IS_OTHER == 3 || ((current.IS_OTHER == 13) | (current.IS_OTHER == 5)) || current.IS_OTHER == 4 || current.IS_OTHER == 14)
							{
								IsFixNone = true;
							}
						}
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			enumerator = oQuestion.QDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current2 = enumerator.Current;
					bool flag = false;
					if (current2.IS_OTHER == 1 || current2.IS_OTHER == 3 || ((current2.IS_OTHER == 11) | (current2.IS_OTHER == 5)) || current2.IS_OTHER == 13 || current2.IS_OTHER == 14)
					{
						flag = true;
					}
					if (flag)
					{
						ExistTextFill = true;
					}
					bool flag2 = false;
					if (current2.IS_OTHER == 2 || current2.IS_OTHER == 3 || ((current2.IS_OTHER == 13) | (current2.IS_OTHER == 4)) || current2.IS_OTHER == 5 || current2.IS_OTHER == 14)
					{
						flag2 = true;
					}
					bool flag3 = listFix.Contains(current2.CODE);
					bool flag4 = true;
					if (!flag3)
					{
						if (IsFixNone)
						{
							flag4 = false;
						}
						else if (flag2 && listFix.Count > 0)
						{
							flag4 = false;
						}
					}
					if (flag4)
					{
						Button button = new Button();
						button.Name = _003F487_003F._003F488_003F("`Ş") + current2.CODE;
						button.Content = current2.CODE_TEXT;
						button.Margin = new Thickness(0.0, 0.0, 15.0, 15.0);
						if (!flag3)
						{
						}
						((FrameworkElement)/*Error near IL_0440: Stack underflow*/).Style = (Style)/*Error near IL_0440: Stack underflow*/;
						if (flag3)
						{
							button.Opacity = 0.5;
						}
						button.Tag = current2.IS_OTHER;
						if (!flag3)
						{
							button.Click += _003F29_003F;
						}
						button.FontSize = (double)Button_FontSize;
						button.MinWidth = (double)Button_Width;
						button.MinHeight = (double)Button_Height;
						wrapPanel.Children.Add(button);
						if (!flag3)
						{
							listBtnNormal.Add(button);
							if (!flag2 || SurveyHelper.FillMode != _003F487_003F._003F488_003F("2"))
							{
								listButton.Add(button);
							}
						}
					}
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F = null)
		{
			Button button = (Button)_003F347_003F;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			int num = (int)button.Tag;
			string text = button.Name.Substring(2);
			int num2 = 0;
			if (num == 2 || num == 3 || num == 5 || num == 13 || num == 4 || num == 14)
			{
				num2 = 1;
			}
			int num3 = 0;
			if (num == 1 || num == 3 || num == 5 || num == 11 || num == 13 || num == 14)
			{
				num3 = 1;
			}
			int num4 = 0;
			if (button.Style == style)
			{
				num4 = 1;
			}
			int num5 = 0;
			if (num4 == 0)
			{
				if (num2 == 1)
				{
					oQuestion.SelectedValues.Clear();
					num5 = 1;
				}
				else
				{
					num5 = 2;
				}
				oQuestion.SelectedValues.Add(text);
				button.Style = style;
			}
			else if (num2 == 1)
			{
				num4 = 2;
			}
			else
			{
				oQuestion.SelectedValues.Remove(text);
				button.Style = style2;
				if (num3 == 0)
				{
					num4 = 2;
				}
			}
			if (num4 < 2)
			{
				bool flag = true;
				bool flag2 = true;
				foreach (Button item in listBtnNormal)
				{
					int num6 = (int)item.Tag;
					string text2 = item.Name.Substring(2);
					if (!(text2 == text))
					{
						if (num5 == 1)
						{
							item.Style = style2;
						}
						else if (num5 == 2 && flag2 && (num6 == 2 || num6 == 3 || num6 == 5 || num6 == 13 || num6 == 4 || num6 == 14) && item.Style == style)
						{
							item.Style = style2;
							oQuestion.SelectedValues.Remove(text2);
							flag2 = false;
						}
					}
					if (!IsFixOther && flag && item.Style == style && (num6 == 1 || num6 == 3 || num6 == 5 || num6 == 11 || num6 == 13 || num6 == 14))
					{
						flag = false;
					}
				}
				if (!IsFixOther)
				{
					if (flag)
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
			}
		}

		private bool _003F87_003F()
		{
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Incompatible stack heights: 0 vs 1
			//IL_0172: Incompatible stack heights: 0 vs 2
			//IL_0182: Incompatible stack heights: 0 vs 1
			//IL_0192: Incompatible stack heights: 0 vs 2
			//IL_01b2: Incompatible stack heights: 0 vs 2
			//IL_01b7: Invalid comparison between Unknown and I4
			//IL_01c2: Incompatible stack heights: 0 vs 2
			//IL_01e1: Incompatible stack heights: 0 vs 2
			//IL_01f5: Incompatible stack heights: 0 vs 2
			//IL_01fa: Incompatible stack heights: 0 vs 1
			//IL_0209: Incompatible stack heights: 0 vs 1
			//IL_0218: Incompatible stack heights: 0 vs 1
			if (listFix.Count == 0)
			{
				int count2 = oQuestion.SelectedValues.Count;
				if ((int)/*Error near IL_0163: Stack underflow*/ == 0)
				{
					string msgNotSelected = SurveyMsg.MsgNotSelected;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_001d: Stack underflow*/, (string)/*Error near IL_001d: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			if (oQuestion.QDefine.MIN_COUNT != 0)
			{
				List<string> listFix2 = listFix;
				if (((List<string>)/*Error near IL_003a: Stack underflow*/).Count + oQuestion.SelectedValues.Count < oQuestion.QDefine.MIN_COUNT)
				{
					string msgMAless = SurveyMsg.MsgMAless;
					MessageBox.Show(string.Format(arg0: ((Multiple)/*Error near IL_0065: Stack underflow*/).oQuestion.QDefine.MIN_COUNT.ToString(), format: (string)/*Error near IL_007c: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			if (oQuestion.QDefine.MAX_COUNT != 0)
			{
				int count3 = listFix.Count;
				List<string> selectedValue = oQuestion.SelectedValues;
				int count = ((List<string>)/*Error near IL_00a6: Stack underflow*/).Count;
				if (/*Error near IL_00a7: Stack underflow*/ + count > oQuestion.QDefine.MAX_COUNT)
				{
					string msgMAmore = SurveyMsg.MsgMAmore;
					MessageBox.Show(string.Format(arg0: ((Multiple)/*Error near IL_00c1: Stack underflow*/).oQuestion.QDefine.MAX_COUNT.ToString(), format: (string)/*Error near IL_00d8: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			if (txtFill.IsEnabled)
			{
				txtFill.Text.Trim();
				string b = _003F487_003F._003F488_003F((string)/*Error near IL_00fd: Stack underflow*/);
				if ((string)/*Error near IL_0102: Stack underflow*/ == b)
				{
					string msgNotFillOther = SurveyMsg.MsgNotFillOther;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_010f: Stack underflow*/, (string)/*Error near IL_010f: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
			}
			QMultiple oQuestion2 = oQuestion;
			if (!txtFill.IsEnabled)
			{
				_003F487_003F._003F488_003F("");
			}
			else
			{
				txtFill.Text.Trim();
			}
			((QMultiple)/*Error near IL_021d: Stack underflow*/).FillText = (string)/*Error near IL_021d: Stack underflow*/;
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			foreach (string item in listFix)
			{
				if (!oQuestion.SelectedValues.Contains(item))
				{
					oQuestion.SelectedValues.Add(item);
				}
			}
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			for (int i = 0; i < oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ") + (i + 1).ToString();
				vEAnswer.CODE = oQuestion.SelectedValues[i].ToString();
				list.Add(vEAnswer);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer.QUESTION_NAME + _003F487_003F._003F488_003F("<") + vEAnswer.CODE;
			}
			SurveyHelper.Answer = oFunc.MID(SurveyHelper.Answer, 1, -9999);
			if (oQuestion.FillText != _003F487_003F._003F488_003F(""))
			{
				VEAnswer vEAnswer2 = new VEAnswer();
				vEAnswer2.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349");
				vEAnswer2.CODE = oQuestion.FillText;
				list.Add(vEAnswer2);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			}
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			//IL_004d: Incompatible stack heights: 0 vs 4
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				PageNav oPageNav2 = oPageNav;
				int delaySecond = SurveyMsg.DelaySeconds;
				Button _003F431_003F = ((Multiple)/*Error near IL_0031: Stack underflow*/).btnNav;
				string mySurveyId = MySurveyId;
				((PageNav)/*Error near IL_0058: Stack underflow*/).PageDataLog((int)/*Error near IL_0058: Stack underflow*/, (List<VEAnswer>)/*Error near IL_0058: Stack underflow*/, _003F431_003F, mySurveyId);
			}
		}

		private void _003F58_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00bd: Incompatible stack heights: 0 vs 2
			//IL_00ce: Incompatible stack heights: 0 vs 2
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
					string content = ((Multiple)/*Error near IL_0080: Stack underflow*/).btnNav_Content;
					((ContentControl)/*Error near IL_0085: Stack underflow*/).Content = content;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_00eb: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Asterisk);
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
				Button btnNav2 = btnNav;
				SolidColorBrush black = Brushes.Black;
				((System.Windows.Controls.Control)/*Error near IL_0035: Stack underflow*/).Foreground = black;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\bšɖ\u0357њԌ٢\u0741\u086fॷਦ\u0b7f\u0c74\u0d77\u0e69\u0f77ၹᅳቻ፠ᐼᕤᙸ\u1775\u1878\u1921\u1a60᭹ᱧᵾṠὸ\u206bⅣ∫⍼③╯♭"), UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
			return;
			IL_0017:
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
				((Multiple)_003F350_003F).Loaded += _003F80_003F;
				((Multiple)_003F350_003F).LayoutUpdated += _003F99_003F;
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
		}
	}
}
