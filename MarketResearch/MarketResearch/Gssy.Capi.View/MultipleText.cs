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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class MultipleText : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__31_0;

			internal int _003F317_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private AutoFill oAutoFill = new AutoFill();

		private BoldTitle oBoldTitle = new BoldTitle();

		private UDPX oFunc = new UDPX();

		private QMultiple oQuestion = new QMultiple();

		private bool ExistTextFill;

		private bool ExistCodeFills;

		private List<string> listPreSet = new List<string>();

		private List<string> listFix = new List<string>();

		private bool IsFixOther;

		private bool IsFixNone;

		private List<Button> listAllButton = new List<Button>();

		private List<Button> listButton = new List<Button>();

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private bool _isValue;

		private string _strMask = _003F487_003F._003F488_003F("");

		private int _MinValue = -1;

		private int _MaxValue = -1;

		private int _MaxLen = 250;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

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

		public MultipleText()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0576: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_070a: Incompatible stack heights: 0 vs 2
			//IL_0721: Incompatible stack heights: 0 vs 1
			//IL_0c8c: Incompatible stack heights: 0 vs 1
			//IL_0cab: Incompatible stack heights: 0 vs 1
			//IL_0cb0: Incompatible stack heights: 0 vs 1
			//IL_0d08: Incompatible stack heights: 0 vs 1
			//IL_0d27: Incompatible stack heights: 0 vs 1
			//IL_0d2c: Incompatible stack heights: 0 vs 1
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
				string text = list3[1];
			}
			qUESTION_TITLE = (string)/*Error near IL_057e: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			List<SurveyDetail>.Enumerator enumerator;
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
							SurveyDetail current = enumerator.Current;
							if (current.CODE == array[i].ToString())
							{
								list4.Add(current);
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
					if (_003F7_003F._003C_003E9__31_0 == null)
					{
						_003F7_003F._003C_003E9__31_0 = _003F7_003F._003C_003E9._003F317_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0726: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0726: Stack underflow*/);
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
							SurveyDetail current2 = enumerator.Current;
							if (current2.CODE == array4[m].ToString())
							{
								list5.Add(current2);
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
			List<string> list6 = oFunc.StringToList(oQuestion.QDefine.CONTROL_TOOLTIP, _003F487_003F._003F488_003F("-Į"));
			if (list6.Count > 0)
			{
				_MaxLen = oFunc.StringToInt(list6[0]);
				if (_MaxLen < 1)
				{
					_MaxLen = 1;
				}
			}
			if (list6.Count > 1)
			{
				List<string> list7 = oFunc.StringToList(list6[1], _003F487_003F._003F488_003F("-"));
				if (list7.Count > 1)
				{
					_MaxValue = oFunc.StringToInt(list7[1]);
				}
				if (list7.Count > 0)
				{
					_MinValue = oFunc.StringToInt(list7[0]);
				}
				_isValue = true;
			}
			if (list6.Count > 2)
			{
				_strMask = list6[2];
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
			((MultipleText)/*Error near IL_0cb5: Stack underflow*/).Button_FontSize = (int)/*Error near IL_0cb5: Stack underflow*/;
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
			((MultipleText)/*Error near IL_0d31: Stack underflow*/).Button_Height = (int)/*Error near IL_0d31: Stack underflow*/;
			Button_Width = 440;
			if (oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (Button_Type == 1 || Button_Type == 3)
				{
					Button_Width = 280;
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
			WrapPanel wrapPanel = wrapPanel1;
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
					foreach (string item in listPreSet)
					{
						if (!listFix.Contains(item))
						{
							oQuestion.SelectedValues.Add(item);
							foreach (UIElement child in wrapPanel.Children)
							{
								foreach (UIElement child2 in ((WrapPanel)child).Children)
								{
									if (child2 is Button)
									{
										Button button = (Button)child2;
										if (button.Name.Substring(2) == item)
										{
											button.Style = style;
											int num = (int)button.Tag;
											if (num == 1 || num == 3 || num == 5)
											{
												flag = true;
											}
										}
									}
									else if (child2 is TextBox)
									{
										TextBox textBox = (TextBox)child2;
										if (textBox.Name.Substring(2) == item)
										{
											textBox.IsEnabled = true;
											textBox.Background = Brushes.White;
										}
									}
								}
							}
						}
					}
					if (flag)
					{
						txtFill.IsEnabled = true;
						txtFill.Background = Brushes.White;
					}
					if (oQuestion.QDetails.Count == 1 || listAllButton.Count == 0)
					{
						if (listAllButton.Count > 0 && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)) && listAllButton[0].Style == style2)
						{
							_003F29_003F(listAllButton[0], null);
						}
						if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (txtFill.IsEnabled)
							{
								txtFill.Focus();
							}
							else if (!SurveyHelper.AutoFill)
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
						else if (!SurveyHelper.AutoFill)
						{
							flag2 = true;
						}
					}
					oAutoFill.oLogicEngine = oLogicEngine;
					if (SurveyHelper.AutoFill && !flag2)
					{
						listButton = oAutoFill.MultiButton(oQuestion.QDefine, listButton, listAllButton, 0);
						foreach (Button item2 in listButton)
						{
							if (item2.Style == style2)
							{
								_003F29_003F(item2, null);
							}
						}
						if (txtFill.IsEnabled)
						{
							txtFill.Text = oAutoFill.CommonOther(oQuestion.QDefine, _003F487_003F._003F488_003F(""));
						}
						if (listButton.Count > 0 && oAutoFill.AutoNext(oQuestion.QDefine))
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
				foreach (SurveyAnswer item3 in oQuestion.QAnswersRead)
				{
					if (_003F94_003F(item3.QUESTION_NAME, 0, (oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ")).Length) == oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ"))
					{
						if (!listFix.Contains(item3.CODE))
						{
							oQuestion.SelectedValues.Add(item3.CODE);
							foreach (UIElement child3 in wrapPanel.Children)
							{
								foreach (UIElement child4 in ((WrapPanel)child3).Children)
								{
									if (child4 is Button)
									{
										Button button2 = (Button)child4;
										if (button2.Name.Substring(2) == item3.CODE)
										{
											button2.Style = style;
											int num2 = (int)button2.Tag;
											if (num2 == 1 || num2 == 3 || num2 == 5)
											{
												flag = true;
											}
										}
									}
								}
							}
						}
					}
					else if (ExistTextFill && item3.QUESTION_NAME == oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"))
					{
						if (item3.CODE != _003F487_003F._003F488_003F(""))
						{
							txtFill.Text = item3.CODE;
						}
					}
					else if (ExistCodeFills && _003F94_003F(item3.QUESTION_NAME, 0, (oQuestion.QuestionName + _003F487_003F._003F488_003F("YŊɐ\u034bѝՂ")).Length) == oQuestion.QuestionName + _003F487_003F._003F488_003F("YŊɐ\u034bѝՂ") && item3.CODE != _003F487_003F._003F488_003F(""))
					{
						string b = _003F94_003F(item3.QUESTION_NAME, (oQuestion.QuestionName + _003F487_003F._003F488_003F("YŊɐ\u034bѝՂ")).Length, -9999);
						foreach (WrapPanel child5 in wrapPanel.Children)
						{
							foreach (UIElement child6 in child5.Children)
							{
								if (child6 is TextBox)
								{
									TextBox textBox2 = (TextBox)child6;
									if (textBox2.Name.Substring(2) == b)
									{
										textBox2.Text = item3.CODE;
										textBox2.IsEnabled = true;
										textBox2.Background = Brushes.White;
										break;
									}
								}
							}
						}
					}
				}
				if (flag)
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
				}
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
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
		}

		private void _003F28_003F()
		{
			//IL_0060: Incompatible stack heights: 0 vs 1
			//IL_0086: Incompatible stack heights: 0 vs 1
			//IL_0087: Incompatible stack heights: 0 vs 1
			//IL_03fa: Incompatible stack heights: 0 vs 1
			//IL_040a: Incompatible stack heights: 0 vs 1
			//IL_040b: Incompatible stack heights: 0 vs 1
			//IL_04c3: Incompatible stack heights: 0 vs 1
			//IL_04d3: Incompatible stack heights: 0 vs 1
			//IL_04d4: Incompatible stack heights: 0 vs 1
			//IL_077c: Incompatible stack heights: 0 vs 1
			//IL_0790: Incompatible stack heights: 0 vs 1
			//IL_0795: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Brush brush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			WrapPanel wrapPanel = wrapPanel1;
			if (Button_Type != 1 && Button_Type != 3)
			{
			}
			((WrapPanel)/*Error near IL_008c: Stack underflow*/).Orientation = (Orientation)/*Error near IL_008c: Stack underflow*/;
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
							if (current.IS_OTHER == 1 || current.IS_OTHER == 3 || current.IS_OTHER == 5)
							{
								IsFixOther = true;
							}
							if (current.IS_OTHER == 2 || current.IS_OTHER == 3 || ((current.IS_OTHER == 13) | (current.IS_OTHER == 4)) || current.IS_OTHER == 5 || current.IS_OTHER == 14)
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
					if (current2.IS_OTHER == 1 || current2.IS_OTHER == 3 || current2.IS_OTHER == 5)
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
						WrapPanel wrapPanel2 = new WrapPanel();
						if (Button_Type == 0)
						{
							wrapPanel2.Orientation = Orientation.Horizontal;
						}
						else
						{
							if (!oQuestion.QDefine.CONTROL_MASK.ToUpper().Contains(_003F487_003F._003F488_003F("I")))
							{
							}
							((WrapPanel)/*Error near IL_0410: Stack underflow*/).Orientation = (Orientation)/*Error near IL_0410: Stack underflow*/;
						}
						wrapPanel2.Margin = new Thickness(0.0, 0.0, 13.0, 13.0);
						wrapPanel.Children.Add(wrapPanel2);
						Button button = new Button();
						button.Name = _003F487_003F._003F488_003F("`Ş") + current2.CODE;
						button.Content = current2.CODE_TEXT;
						button.Margin = new Thickness(0.0, 0.0, 2.0, 2.0);
						if (!flag3)
						{
						}
						((FrameworkElement)/*Error near IL_04d9: Stack underflow*/).Style = (Style)/*Error near IL_04d9: Stack underflow*/;
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
						wrapPanel2.Children.Add(button);
						if (!flag3)
						{
							listAllButton.Add(button);
							if (!flag2 || SurveyHelper.FillMode != _003F487_003F._003F488_003F("2"))
							{
								listButton.Add(button);
							}
						}
						if (current2.IS_OTHER > 10)
						{
							TextBox textBox = new TextBox();
							textBox.Name = _003F487_003F._003F488_003F("vŞ") + current2.CODE;
							textBox.Text = _003F487_003F._003F488_003F("");
							textBox.Tag = current2.IS_OTHER;
							textBox.ToolTip = _003F487_003F._003F488_003F("诰嘮跜鋈屨咛\u065a") + current2.CODE_TEXT + _003F487_003F._003F488_003F("T瞌觡緀迱挊掄怮㠃");
							if (wrapPanel2.Orientation == Orientation.Horizontal)
							{
								textBox.Margin = new Thickness(0.0, 0.0, 15.0, 2.0);
							}
							else
							{
								textBox.Margin = new Thickness(0.0, 0.0, 2.0, 2.0);
							}
							textBox.FontSize = (double)Button_FontSize;
							textBox.Width = button.MinWidth;
							textBox.Height = (double)Button_Height;
							textBox.MaxLength = _MaxLen;
							textBox.GotFocus += _003F91_003F;
							textBox.LostFocus += _003F90_003F;
							if (_isValue)
							{
								textBox.TextChanged += _003F98_003F;
							}
							wrapPanel2.Children.Add(textBox);
							if (!flag3)
							{
								SolidColorBrush lightGray = Brushes.LightGray;
							}
							else
							{
								SolidColorBrush white = Brushes.White;
							}
							((System.Windows.Controls.Control)/*Error near IL_079a: Stack underflow*/).Background = (Brush)/*Error near IL_079a: Stack underflow*/;
							textBox.IsEnabled = flag3;
							ExistCodeFills = true;
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
			if (button.Style == style)
			{
				num3 = 1;
			}
			int num4 = 0;
			bool flag = true;
			if (num3 == 0)
			{
				if (num2 == 1)
				{
					oQuestion.SelectedValues.Clear();
					num4 = 1;
				}
				else
				{
					num4 = 2;
				}
				oQuestion.SelectedValues.Add(text);
				button.Style = style;
			}
			else if (num2 == 1)
			{
				num3 = 2;
			}
			else
			{
				oQuestion.SelectedValues.Remove(text);
				button.Style = style2;
			}
			if (num3 < 2)
			{
				bool flag2 = true;
				bool flag3 = true;
				foreach (WrapPanel child in wrapPanel1.Children)
				{
					Button button2 = new Button();
					TextBox textBox = new TextBox();
					foreach (UIElement child2 in child.Children)
					{
						if (child2 is Button)
						{
							button2 = (Button)child2;
						}
						else if (child2 is TextBox)
						{
							textBox = (TextBox)child2;
						}
					}
					string text2 = button2.Name.Substring(2);
					if (!listFix.Contains(text2))
					{
						bool flag4 = false;
						int num5 = (int)button2.Tag;
						if (!(text2 == text))
						{
							if (num4 == 1)
							{
								button2.Style = style2;
							}
							else if (num4 == 2 && flag3 && (num5 == 2 || num5 == 3 || num5 == 5 || num5 == 13 || num5 == 4 || num5 == 14) && button2.Style == style)
							{
								button2.Style = style2;
								oQuestion.SelectedValues.Remove(text2);
								flag3 = false;
							}
						}
						if (flag2 && button2.Style == style && (num5 == 1 || num5 == 3 || num5 == 5))
						{
							flag2 = false;
						}
						flag4 = (button2.Style == style);
						if (textBox.Name != _003F487_003F._003F488_003F(""))
						{
							if (flag4)
							{
								textBox.IsEnabled = true;
								textBox.Background = Brushes.White;
								if (textBox.Name.Substring(2) == text)
								{
									flag = false;
									if (SurveyHelper.AutoFill)
									{
										if (textBox.Text == _003F487_003F._003F488_003F(""))
										{
											textBox.Text = oAutoFill.CommonOther(oQuestion.QDefine, text);
										}
									}
									else if (textBox.Text == _003F487_003F._003F488_003F(""))
									{
										textBox.Focus();
									}
								}
							}
							else
							{
								textBox.Background = Brushes.LightGray;
								textBox.IsEnabled = false;
							}
						}
					}
				}
				if (!IsFixOther)
				{
					if (flag2)
					{
						txtFill.Background = Brushes.LightGray;
						txtFill.IsEnabled = false;
					}
					else
					{
						txtFill.IsEnabled = true;
						txtFill.Background = Brushes.White;
						if (flag && txtFill.Text == _003F487_003F._003F488_003F(""))
						{
							txtFill.Focus();
						}
					}
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_0209: Incompatible stack heights: 0 vs 1
			//IL_0222: Incompatible stack heights: 0 vs 1
			//IL_0232: Incompatible stack heights: 0 vs 1
			//IL_05a7: Incompatible stack heights: 0 vs 2
			//IL_05c0: Incompatible stack heights: 0 vs 1
			//IL_05c2: Incompatible stack heights: 0 vs 1
			if (listFix.Count == 0 && oQuestion.SelectedValues.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (oQuestion.QDefine.MIN_COUNT != 0 && listFix.Count + oQuestion.SelectedValues.Count < oQuestion.QDefine.MIN_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAless, oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (oQuestion.QDefine.MAX_COUNT != 0 && listFix.Count + oQuestion.SelectedValues.Count > oQuestion.QDefine.MAX_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAmore, oQuestion.QDefine.MAX_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (txtFill.IsEnabled && txtFill.Text.Trim() == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				txtFill.Focus();
				return true;
			}
			if (txtFill.IsEnabled)
			{
				QMultiple oQuestion2 = oQuestion;
				if (!txtFill.IsEnabled)
				{
					_003F487_003F._003F488_003F("");
				}
				else
				{
					txtFill.Text.Trim();
				}
				((QMultiple)/*Error near IL_0237: Stack underflow*/).FillText = (string)/*Error near IL_0237: Stack underflow*/;
			}
			if (ExistCodeFills)
			{
				oQuestion.FillTexts.Clear();
				WrapPanel wrapPanel = wrapPanel1;
				int num = -1;
				foreach (WrapPanel child in wrapPanel.Children)
				{
					foreach (UIElement child2 in child.Children)
					{
						if (child2 is TextBox)
						{
							TextBox textBox = (TextBox)child2;
							if (textBox.IsEnabled)
							{
								string text = textBox.Text.Trim();
								if (text == _003F487_003F._003F488_003F(""))
								{
									MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									textBox.Focus();
									return true;
								}
								if (_MinValue > 0 && Convert.ToDouble(text) < (double)_MinValue)
								{
									MessageBox.Show(string.Format(SurveyMsg.MsgFillNotSmall, _MinValue.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									textBox.Focus();
									return true;
								}
								if (_MaxValue > 0 && Convert.ToDouble(text) > (double)_MaxValue)
								{
									MessageBox.Show(string.Format(SurveyMsg.MsgFillNotBig, _MaxValue.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									textBox.Focus();
									return true;
								}
								if (_isValue && _strMask != _003F487_003F._003F488_003F(""))
								{
									string text2 = _strMask;
									if (text2.IndexOf(_003F487_003F._003F488_003F("-")) == -1)
									{
										text2 += _003F487_003F._003F488_003F(".ı");
									}
									string arg = text2.Replace(_003F487_003F._003F488_003F("-"), SurveyMsg.MsgFillFitReplace);
									if (oLogicEngine.Result(_003F487_003F._003F488_003F(",ŉɩͱъնٯܩ") + text + _003F487_003F._003F488_003F("-") + text2 + _003F487_003F._003F488_003F("(")))
									{
										MessageBox.Show(string.Format(SurveyMsg.MsgFillFit, arg), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
										textBox.Focus();
										return true;
									}
								}
								if (oQuestion.QDefine.CONTROL_MASK.ToUpper().Contains(_003F487_003F._003F488_003F("B")) && num == 0)
								{
									MessageBox.Show(SurveyMsg.MsgFillPrveText, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									textBox.Focus();
									return true;
								}
								string text3 = textBox.Name.Substring(2);
								Dictionary<string, string> fillText = oQuestion.FillTexts;
								if (!textBox.IsEnabled)
								{
									_003F487_003F._003F488_003F("");
								}
								((Dictionary<string, string>)/*Error near IL_05c7: Stack underflow*/).Add((string)/*Error near IL_05c7: Stack underflow*/, (string)/*Error near IL_05c7: Stack underflow*/);
								num = 1;
							}
							else
							{
								num = 0;
							}
						}
					}
				}
			}
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
			SurveyHelper.Answer = _003F94_003F(SurveyHelper.Answer, 1, -9999);
			if (oQuestion.FillText != _003F487_003F._003F488_003F(""))
			{
				VEAnswer vEAnswer2 = new VEAnswer();
				vEAnswer2.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349");
				vEAnswer2.CODE = oQuestion.FillText;
				list.Add(vEAnswer2);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			}
			foreach (string key in oQuestion.FillTexts.Keys)
			{
				VEAnswer vEAnswer3 = new VEAnswer();
				vEAnswer3.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("YŊɐ\u034bѝՂ") + key;
				vEAnswer3.CODE = oQuestion.FillTexts[key];
				list.Add(vEAnswer3);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer3.QUESTION_NAME + _003F487_003F._003F488_003F("<") + vEAnswer3.CODE;
			}
			return list;
		}

		private void _003F89_003F(List<VEAnswer> _003F370_003F)
		{
			//IL_0046: Incompatible stack heights: 0 vs 1
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				PageNav oPageNav2 = oPageNav;
				int delaySeconds = SurveyMsg.DelaySeconds;
				Button _003F431_003F = btnNav;
				string mySurveyId = MySurveyId;
				((PageNav)/*Error near IL_0058: Stack underflow*/).PageDataLog(delaySeconds, _003F370_003F, _003F431_003F, mySurveyId);
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00e8: Incompatible stack heights: 0 vs 1
			//IL_00f9: Incompatible stack heights: 0 vs 2
			//IL_010f: Incompatible stack heights: 0 vs 3
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				((MultipleText)/*Error near IL_0040: Stack underflow*/).btnNav.Content = btnNav_Content;
			}
			else
			{
				List<VEAnswer> list = _003F88_003F();
				oLogicEngine.PageAnswer = list;
				oPageNav.oLogicEngine = oLogicEngine;
				if (!oPageNav.CheckLogic(CurPageId))
				{
					Button btnNav2 = btnNav;
					string content = ((MultipleText)/*Error near IL_008b: Stack underflow*/).btnNav_Content;
					((ContentControl)/*Error near IL_0090: Stack underflow*/).Content = content;
				}
				else
				{
					_003F89_003F(list);
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_00a9: Stack underflow*/, (string)/*Error near IL_00a9: Stack underflow*/, (MessageBoxButton)/*Error near IL_00a9: Stack underflow*/, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00d8:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0025: Incompatible stack heights: 0 vs 1
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

		private void _003F86_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_0027: Incompatible stack heights: 0 vs 1
			if (_003F348_003F.Key == Key.Return)
			{
				Button btnNav2 = btnNav;
				if (((UIElement)/*Error near IL_0011: Stack underflow*/).IsEnabled)
				{
					_003F58_003F(_003F347_003F, _003F348_003F);
				}
			}
		}

		private void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_00da: Incompatible stack heights: 0 vs 5
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
					int addedLength = ((TextChange)((object[])/*Error near IL_0091: Stack underflow*/)[(long)/*Error near IL_0091: Stack underflow*/]).AddedLength;
					string text = ((string)/*Error near IL_009b: Stack underflow*/).Remove((int)/*Error near IL_009b: Stack underflow*/, addedLength);
					((TextBox)/*Error near IL_00a0: Stack underflow*/).Text = text;
					textBox.Select(offset, 0);
				}
			}
		}

		private string _003F92_003F(string _003F362_003F, int _003F363_003F, int _003F364_003F = -9999)
		{
			//IL_009e: Incompatible stack heights: 0 vs 1
			//IL_00a3: Incompatible stack heights: 1 vs 0
			//IL_00ae: Incompatible stack heights: 0 vs 1
			//IL_00b3: Incompatible stack heights: 1 vs 0
			//IL_00be: Incompatible stack heights: 0 vs 1
			//IL_00c3: Incompatible stack heights: 1 vs 0
			//IL_00ce: Incompatible stack heights: 0 vs 1
			//IL_00d3: Incompatible stack heights: 1 vs 0
			//IL_00de: Incompatible stack heights: 0 vs 1
			//IL_00e3: Incompatible stack heights: 1 vs 0
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
			//IL_0058: Incompatible stack heights: 0 vs 1
			//IL_006f: Incompatible stack heights: 0 vs 1
			//IL_0074: Incompatible stack heights: 1 vs 0
			//IL_0079: Incompatible stack heights: 0 vs 2
			//IL_007f: Incompatible stack heights: 0 vs 1
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
			//IL_0037: Incompatible stack heights: 0 vs 1
			//IL_003c: Incompatible stack heights: 1 vs 0
			//IL_0041: Incompatible stack heights: 0 vs 1
			//IL_0047: Incompatible stack heights: 0 vs 1
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				goto IL_004c;
			}
			int startIndex = ((string)/*Error near IL_0020: Stack underflow*/).Length - num;
			goto IL_004d;
			IL_004c:
			startIndex = 0;
			goto IL_004d;
			IL_004d:
			return ((string)/*Error near IL_0052: Stack underflow*/).Substring(startIndex);
			IL_0027:
			goto IL_004c;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0004ŭɚ\u035bўԈ٦\u0745ࡓ\u094bਚ\u0b43\u0c70൳\u0e6d\u0f73ၵᅿቷ፬ᐸᕠᙼ\u1771ᡤ\u193d\u1a7c᭥ᱣᵺṤὼ\u2067Ⅿ≽⍭⑿╲☫❼⡢⥯⩭"), UriKind.Relative);
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
				((MultipleText)_003F350_003F).Loaded += _003F80_003F;
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
				txtFillTitle = (TextBlock)_003F350_003F;
				break;
			case 8:
				txtFill = (TextBox)_003F350_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				break;
			case 9:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 10:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 11:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 12:
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
