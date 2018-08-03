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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class MultipleGroup : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__27_0;

			public static Func<SurveyDetail, global::_003F1_003F<string, string, int, string>> _003C_003E9__28_1;

			internal int _003F308_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal global::_003F1_003F<string, string, int, string> _003F313_003F(SurveyDetail _003F483_003F)
			{
				return new global::_003F1_003F<string, string, int, string>(_003F483_003F.CODE, _003F483_003F.CODE_TEXT, _003F483_003F.IS_OTHER, _003F483_003F.EXTEND_1);
			}
		}

		[CompilerGenerated]
		private sealed class _003F9_003F
		{
			public string strGroupCode;

			internal bool _003F314_003F(SurveyDetail _003F483_003F)
			{
				return _003F483_003F.PARENT_CODE == strGroupCode;
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private AutoFill oAutoFill = new AutoFill();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QMultiple oQuestion = new QMultiple();

		private bool ExistTextFill;

		private bool ExistCodeFills;

		private List<string> listPreSet = new List<string>();

		private List<string> listFix = new List<string>();

		private bool IsFixOther;

		private bool IsFixNone;

		private List<Button> listAllButton = new List<Button>();

		private List<Button> listButton = new List<Button>();

		private bool ShowLogic;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private bool Button_Hide;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ScrollViewer scrollmain;

		internal Grid GridContent;

		internal StackPanel stackPanel1;

		internal TextBlock txtFillTitle;

		internal TextBox txtFill;

		internal TextBlock txtAfter;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public MultipleGroup()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0576: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_070a: Incompatible stack heights: 0 vs 2
			//IL_0721: Incompatible stack heights: 0 vs 1
			//IL_0b7d: Incompatible stack heights: 0 vs 1
			//IL_0b9c: Incompatible stack heights: 0 vs 1
			//IL_0ba1: Incompatible stack heights: 0 vs 1
			//IL_0bc6: Incompatible stack heights: 0 vs 1
			//IL_0be5: Incompatible stack heights: 0 vs 1
			//IL_0bea: Incompatible stack heights: 0 vs 1
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
					if (_003F7_003F._003C_003E9__27_0 == null)
					{
						_003F7_003F._003C_003E9__27_0 = _003F7_003F._003C_003E9._003F308_003F;
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
				ShowLogic = true;
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
			else if (oQuestion.QDefine.IS_RANDOM == 1 || oQuestion.QDefine.IS_RANDOM == 3)
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
			((MultipleGroup)/*Error near IL_0ba6: Stack underflow*/).Button_FontSize = (int)/*Error near IL_0ba6: Stack underflow*/;
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				int cONTROL_HEIGHT = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			else
			{
				int btnHeight = SurveyHelper.BtnHeight;
			}
			((MultipleGroup)/*Error near IL_0bef: Stack underflow*/).Button_Height = (int)/*Error near IL_0bef: Stack underflow*/;
			Button_Width = 280;
			if (oQuestion.QDefine.CONTROL_WIDTH == 0)
			{
				if (Button_Type == 2 || Button_Type == 4)
				{
					Button_Width = 440;
				}
			}
			else
			{
				Button_Width = oQuestion.QDefine.CONTROL_WIDTH;
			}
			if (Button_FontSize == -1)
			{
				Button_FontSize = -SurveyHelper.BtnFontSize;
			}
			Button_Hide = (Button_FontSize < 0);
			Button_FontSize = Math.Abs(Button_FontSize);
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
			double opacity = 0.2;
			Grid gridContent = GridContent;
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
							bool flag3 = false;
							foreach (Border child in gridContent.Children)
							{
								WrapPanel wrapPanel = (WrapPanel)child.Child;
								if (wrapPanel.Name.Substring(1, 1) == _003F487_003F._003F488_003F("S"))
								{
									foreach (WrapPanel child2 in wrapPanel.Children)
									{
										foreach (UIElement child3 in child2.Children)
										{
											if (child3 is Button)
											{
												Button button = (Button)child3;
												if (button.Name.Substring(2) == item)
												{
													button.Style = style;
													flag3 = true;
													int num = Convert.ToInt16(((string)button.Tag).Substring(0, 2).Trim());
													if (num == 1 || num == 3 || num == 5)
													{
														flag = true;
													}
												}
											}
											else if (child3 is Image)
											{
												Image image = (Image)child3;
												if (image.Name.Substring(2) == item)
												{
													image.Opacity = opacity;
												}
											}
											else if (child3 is TextBox)
											{
												TextBox textBox = (TextBox)child3;
												if (textBox.Name.Substring(2) == item)
												{
													textBox.IsEnabled = true;
													textBox.Background = Brushes.White;
												}
											}
										}
										if (flag3)
										{
											break;
										}
									}
									if (flag3)
									{
										break;
									}
								}
							}
							if (flag3)
							{
								oQuestion.SelectedValues.Add(item);
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
							_003F29_003F(listAllButton[0], new RoutedEventArgs());
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
							bool flag4 = false;
							foreach (Border child4 in gridContent.Children)
							{
								WrapPanel wrapPanel2 = (WrapPanel)child4.Child;
								if (wrapPanel2.Name.Substring(1, 1) == _003F487_003F._003F488_003F("S"))
								{
									foreach (WrapPanel child5 in wrapPanel2.Children)
									{
										foreach (UIElement child6 in child5.Children)
										{
											if (child6 is Button)
											{
												Button button2 = (Button)child6;
												if (button2.Name.Substring(2) == item3.CODE)
												{
													button2.Style = style;
													flag4 = true;
													int num2 = Convert.ToInt16(((string)button2.Tag).Substring(0, 2).Trim());
													if (num2 == 1 || num2 == 3 || num2 == 5)
													{
														flag = true;
													}
												}
											}
											else if (child6 is Image)
											{
												Image image2 = (Image)child6;
												if (image2.Name.Substring(2) == item3.CODE)
												{
													image2.Opacity = opacity;
												}
											}
										}
										if (flag4)
										{
											break;
										}
									}
									if (flag4)
									{
										break;
									}
								}
							}
							if (flag4)
							{
								oQuestion.SelectedValues.Add(item3.CODE);
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
						foreach (Border child7 in gridContent.Children)
						{
							WrapPanel wrapPanel3 = (WrapPanel)child7.Child;
							if (wrapPanel3.Name.Substring(1, 1) == _003F487_003F._003F488_003F("S"))
							{
								foreach (WrapPanel child8 in wrapPanel3.Children)
								{
									foreach (UIElement child9 in child8.Children)
									{
										if (child9 is TextBox)
										{
											TextBox textBox2 = (TextBox)child9;
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
			//IL_0540: Incompatible stack heights: 0 vs 1
			//IL_0541: Incompatible stack heights: 0 vs 1
			//IL_0581: Incompatible stack heights: 0 vs 1
			//IL_0582: Incompatible stack heights: 0 vs 1
			//IL_0933: Incompatible stack heights: 0 vs 2
			//IL_094a: Incompatible stack heights: 0 vs 1
			//IL_0c6b: Incompatible stack heights: 0 vs 1
			//IL_0c7b: Incompatible stack heights: 0 vs 1
			//IL_0c7c: Incompatible stack heights: 0 vs 1
			//IL_0d3a: Incompatible stack heights: 0 vs 1
			//IL_0d4b: Incompatible stack heights: 0 vs 1
			//IL_0d55: Incompatible stack heights: 0 vs 1
			//IL_0e0f: Incompatible stack heights: 0 vs 1
			//IL_0e1f: Incompatible stack heights: 0 vs 1
			//IL_0e20: Incompatible stack heights: 0 vs 1
			//IL_1030: Incompatible stack heights: 0 vs 1
			//IL_1040: Incompatible stack heights: 0 vs 1
			//IL_1041: Incompatible stack heights: 0 vs 1
			//IL_13a7: Incompatible stack heights: 0 vs 1
			//IL_13bb: Incompatible stack heights: 0 vs 1
			//IL_13c0: Incompatible stack heights: 0 vs 1
			//IL_151f: Incompatible stack heights: 0 vs 1
			//IL_152f: Incompatible stack heights: 0 vs 1
			//IL_1530: Incompatible stack heights: 0 vs 1
			double num = 0.1;
			double num2 = 1.0;
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Style style3 = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
			Brush borderBrush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			string text = oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim();
			string text2 = _003F487_003F._003F488_003F("S");
			string text3 = _003F487_003F._003F488_003F("B");
			int num3 = 0;
			if (text != _003F487_003F._003F488_003F(""))
			{
				text2 = _003F93_003F(text, 1);
				if (_003F487_003F._003F488_003F(":ĸȺ\u0334в\u0530ز\u0734࠺स").Contains(text2))
				{
					text2 = _003F487_003F._003F488_003F("S");
					if (text != _003F487_003F._003F488_003F(""))
					{
						num3 = Convert.ToInt32(text);
					}
				}
				else
				{
					text = _003F94_003F(text, 1, -9999);
					text3 = _003F93_003F(text, 1);
					if (_003F487_003F._003F488_003F(":ĸȺ\u0334в\u0530ز\u0734࠺स").Contains(text3))
					{
						if (text2 != _003F487_003F._003F488_003F("U") && text2 != _003F487_003F._003F488_003F("C"))
						{
							text3 = _003F487_003F._003F488_003F("B");
						}
						if (text != _003F487_003F._003F488_003F(""))
						{
							num3 = Convert.ToInt32(text);
						}
					}
					else if (_003F94_003F(text, 1, -9999) != _003F487_003F._003F488_003F(""))
					{
						num3 = Convert.ToInt32(_003F94_003F(text, 1, -9999));
					}
				}
				text = text2 + text3;
				if (text.Contains(_003F487_003F._003F488_003F("M")))
				{
					text2 = _003F487_003F._003F488_003F("M");
				}
				else if (text.Contains(_003F487_003F._003F488_003F("S")))
				{
					text2 = _003F487_003F._003F488_003F("S");
				}
				if (text.Contains(_003F487_003F._003F488_003F("U")))
				{
					text3 = _003F487_003F._003F488_003F("U");
				}
				else if (text.Contains(_003F487_003F._003F488_003F("C")))
				{
					text3 = _003F487_003F._003F488_003F("C");
				}
				if (text.Contains(_003F487_003F._003F488_003F("B")) && (text.Contains(_003F487_003F._003F488_003F("U")) || text.Contains(_003F487_003F._003F488_003F("C"))))
				{
					text2 = _003F487_003F._003F488_003F("B");
				}
				if (text.Contains(_003F487_003F._003F488_003F("B")) && (text.Contains(_003F487_003F._003F488_003F("S")) || text.Contains(_003F487_003F._003F488_003F("M"))))
				{
					text3 = _003F487_003F._003F488_003F("B");
				}
				if (text == _003F487_003F._003F488_003F("B") || text == _003F487_003F._003F488_003F("Ał"))
				{
					text2 = _003F487_003F._003F488_003F("B");
					text3 = _003F487_003F._003F488_003F("B");
				}
			}
			HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right;
			if (text2 == _003F487_003F._003F488_003F("B"))
			{
				horizontalAlignment = HorizontalAlignment.Center;
			}
			else if (text2 == _003F487_003F._003F488_003F("M"))
			{
				horizontalAlignment = HorizontalAlignment.Left;
			}
			VerticalAlignment verticalAlignment = VerticalAlignment.Center;
			if (text3 == _003F487_003F._003F488_003F("U"))
			{
				verticalAlignment = VerticalAlignment.Top;
			}
			else if (text3 == _003F487_003F._003F488_003F("C"))
			{
				verticalAlignment = VerticalAlignment.Bottom;
			}
			if (Button_Type != 2 && Button_Type != 4)
			{
			}
			Orientation orientation = (Orientation)/*Error near IL_0543: Stack underflow*/;
			if (!(oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("I")))
			{
			}
			Orientation orientation2 = (Orientation)/*Error near IL_0584: Stack underflow*/;
			Grid gridContent = GridContent;
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
							if (current.IS_OTHER == 2 || current.IS_OTHER == 3 || current.IS_OTHER == 13)
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
			oQuestion.GetGroupDetails();
			if (ShowLogic)
			{
				List<SurveyDetail> list = new List<SurveyDetail>();
				enumerator = oQuestion.QDetails.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						SurveyDetail current2 = enumerator.Current;
						bool flag = true;
						List<SurveyDetail>.Enumerator enumerator2 = list.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								SurveyDetail current3 = enumerator2.Current;
								if (current2.PARENT_CODE == current3.CODE)
								{
									flag = false;
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator2).Dispose();
						}
						if (flag)
						{
							enumerator2 = oQuestion.QGroupDetails.GetEnumerator();
							try
							{
								while (enumerator2.MoveNext())
								{
									SurveyDetail current4 = enumerator2.Current;
									if (current2.PARENT_CODE == current4.CODE)
									{
										list.Add(current4);
										break;
									}
								}
							}
							finally
							{
								((IDisposable)enumerator2).Dispose();
							}
						}
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				oQuestion.QGroupDetails = list;
			}
			else if (oQuestion.QDefine.IS_RANDOM == 2 || oQuestion.QDefine.IS_RANDOM == 1)
			{
				oQuestion.QGroupDetails = oQuestion.RandomDetails(oQuestion.QGroupDetails);
			}
			int num4 = 0;
			enumerator = oQuestion.QGroupDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current5 = enumerator.Current;
					_003F9_003F _003F9_003F = new _003F9_003F();
					_003F9_003F.strGroupCode = current5.CODE;
					string cODE_TEXT = current5.CODE_TEXT;
					oQuestion.QDetails.Where(_003F9_003F._003F314_003F);
					if (_003F7_003F._003C_003E9__28_1 == null)
					{
						_003F7_003F._003C_003E9__28_1 = _003F7_003F._003C_003E9._003F313_003F;
					}
					IEnumerable<global::_003F1_003F<string, string, int, string>> enumerable = ((IEnumerable<SurveyDetail>)/*Error near IL_094f: Stack underflow*/).Select((Func<SurveyDetail, global::_003F1_003F<string, string, int, string>>)/*Error near IL_094f: Stack underflow*/);
					List<string> list2 = new List<string>();
					bool flag2 = false;
					bool flag3 = false;
					foreach (global::_003F1_003F<string, string, int, string> item in enumerable)
					{
						if (listFix.Contains(item.CODE))
						{
							flag3 = true;
							if (item.IS_OTHER == 2 || item.IS_OTHER == 3 || ((item.IS_OTHER == 13) | (item.IS_OTHER == 4)) || item.IS_OTHER == 5 || item.IS_OTHER == 14)
							{
								flag2 = true;
							}
						}
					}
					foreach (global::_003F1_003F<string, string, int, string> item2 in enumerable)
					{
						if (listFix.Contains(item2.CODE))
						{
							list2.Add(item2.CODE);
						}
						else if (!flag2 && !IsFixNone)
						{
							bool flag4 = false;
							bool flag5 = false;
							if (item2.IS_OTHER == 2 || item2.IS_OTHER == 3 || item2.IS_OTHER == 13)
							{
								flag4 = true;
							}
							if (item2.IS_OTHER == 4 || item2.IS_OTHER == 5 || item2.IS_OTHER == 14)
							{
								flag5 = true;
							}
							if ((listFix.Count <= 0 || !flag4) && (!flag3 || !flag5))
							{
								list2.Add(item2.CODE);
							}
						}
					}
					if (list2.Count() > 0)
					{
						gridContent.RowDefinitions.Add(new RowDefinition
						{
							Height = GridLength.Auto
						});
						Border border = new Border();
						if (!((cODE_TEXT == _003F487_003F._003F488_003F("")) | (oQuestion.QDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("1"))))
						{
						}
						Thickness borderThickness = new Thickness((double)/*Error near IL_0c7d: Stack underflow*/);
						((Border)/*Error near IL_0c87: Stack underflow*/).BorderThickness = borderThickness;
						border.BorderBrush = borderBrush;
						border.SetValue(Grid.RowProperty, num4);
						border.SetValue(Grid.ColumnProperty, 0);
						gridContent.Children.Add(border);
						WrapPanel wrapPanel = new WrapPanel();
						wrapPanel.VerticalAlignment = verticalAlignment;
						wrapPanel.HorizontalAlignment = horizontalAlignment;
						wrapPanel.Name = _003F487_003F._003F488_003F("uō") + _003F9_003F.strGroupCode;
						border.Child = wrapPanel;
						TextBlock textBlock = new TextBlock();
						if (oQuestion.QDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("1"))
						{
							_003F487_003F._003F488_003F("");
						}
						((TextBlock)/*Error near IL_0d5a: Stack underflow*/).Text = (string)/*Error near IL_0d5a: Stack underflow*/;
						textBlock.Style = style3;
						textBlock.Foreground = foreground;
						if (num3 > 0)
						{
							textBlock.Width = (double)num3;
						}
						textBlock.TextWrapping = TextWrapping.Wrap;
						textBlock.Margin = new Thickness(15.0, 0.0, 15.0, 0.0);
						textBlock.VerticalAlignment = verticalAlignment;
						wrapPanel.Children.Add(textBlock);
						border = new Border();
						if (!(oQuestion.QDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("1")))
						{
						}
						Thickness borderThickness2 = new Thickness((double)/*Error near IL_0e21: Stack underflow*/);
						((Border)/*Error near IL_0e2b: Stack underflow*/).BorderThickness = borderThickness2;
						border.BorderBrush = borderBrush;
						border.SetValue(Grid.RowProperty, num4);
						border.SetValue(Grid.ColumnProperty, 1);
						gridContent.Children.Add(border);
						WrapPanel wrapPanel2 = new WrapPanel();
						wrapPanel2.Orientation = orientation;
						wrapPanel2.Margin = new Thickness(15.0, 0.0, 0.0, 13.0);
						wrapPanel2.Name = _003F487_003F._003F488_003F("uœ") + _003F9_003F.strGroupCode;
						border.Child = wrapPanel2;
						foreach (global::_003F1_003F<string, string, int, string> item3 in enumerable)
						{
							if (list2.Contains(item3.CODE))
							{
								if (item3.IS_OTHER == 1 || item3.IS_OTHER == 3 || item3.IS_OTHER == 5)
								{
									ExistTextFill = true;
								}
								bool flag6 = listFix.Contains(item3.CODE);
								WrapPanel wrapPanel3 = new WrapPanel();
								wrapPanel3.Margin = new Thickness(0.0, 15.0, 15.0, 0.0);
								wrapPanel3.Orientation = orientation2;
								wrapPanel2.Children.Add(wrapPanel3);
								Button button = new Button();
								button.Name = _003F487_003F._003F488_003F("`Ş") + item3.CODE;
								button.Content = item3.CODE_TEXT;
								button.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
								if (!flag6)
								{
								}
								((FrameworkElement)/*Error near IL_1046: Stack underflow*/).Style = (Style)/*Error near IL_1046: Stack underflow*/;
								if (flag6)
								{
									button.Opacity = 0.5;
								}
								button.Tag = (item3.IS_OTHER + _003F487_003F._003F488_003F("!")).Substring(0, 2) + _003F487_003F._003F488_003F("F") + _003F9_003F.strGroupCode;
								if (!flag6)
								{
									button.Click += _003F29_003F;
								}
								button.FontSize = (double)Button_FontSize;
								button.MinWidth = (double)Button_Width;
								button.MinHeight = (double)Button_Height;
								wrapPanel3.Children.Add(button);
								if (!flag6)
								{
									listAllButton.Add(button);
									int iS_OTHER = item3.IS_OTHER;
									int num5 = 0;
									switch (iS_OTHER)
									{
									case 2:
									case 3:
									case 13:
										num5 = 1;
										break;
									case 4:
									case 5:
									case 14:
										num5 = 2;
										break;
									}
									if (num5 == 0 || SurveyHelper.FillMode != _003F487_003F._003F488_003F("2"))
									{
										listButton.Add(button);
									}
								}
								if (item3.IS_OTHER > 10)
								{
									TextBox textBox = new TextBox();
									textBox.Name = _003F487_003F._003F488_003F("vŞ") + item3.CODE;
									textBox.Text = _003F487_003F._003F488_003F("");
									textBox.Tag = (item3.IS_OTHER + _003F487_003F._003F488_003F("!")).Substring(0, 2) + _003F487_003F._003F488_003F("F") + _003F9_003F.strGroupCode;
									textBox.ToolTip = _003F487_003F._003F488_003F("诰嘮跜鋈屨咛\u065a") + item3.CODE_TEXT + _003F487_003F._003F488_003F("T瞌觡緀迱挊掄怮㠃");
									if (wrapPanel3.Orientation == Orientation.Horizontal)
									{
										textBox.Margin = new Thickness(0.0, 0.0, 15.0, 2.0);
									}
									else
									{
										textBox.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
									}
									textBox.FontSize = (double)Button_FontSize;
									textBox.Width = (double)Button_Width;
									textBox.Height = (double)Button_Height;
									textBox.MaxLength = 250;
									textBox.GotFocus += _003F91_003F;
									textBox.LostFocus += _003F90_003F;
									wrapPanel3.Children.Add(textBox);
									if (!flag6)
									{
										SolidColorBrush lightGray = Brushes.LightGray;
									}
									else
									{
										SolidColorBrush white = Brushes.White;
									}
									((System.Windows.Controls.Control)/*Error near IL_13c5: Stack underflow*/).Background = (Brush)/*Error near IL_13c5: Stack underflow*/;
									textBox.IsEnabled = flag6;
									ExistCodeFills = true;
								}
								string eXTEND_ = item3.EXTEND_1;
								if (eXTEND_ != _003F487_003F._003F488_003F(""))
								{
									Image image = new Image();
									image.Name = _003F487_003F._003F488_003F("rŞ") + item3.CODE;
									image.Tag = (item3.IS_OTHER + _003F487_003F._003F488_003F("!")).Substring(0, 2) + _003F487_003F._003F488_003F("F") + _003F9_003F.strGroupCode;
									image.MinHeight = 46.0;
									image.Width = (double)Button_Width;
									image.Stretch = Stretch.Uniform;
									if (wrapPanel3.Orientation == Orientation.Horizontal)
									{
										image.Margin = new Thickness(0.0, 0.0, 15.0, 2.0);
									}
									else
									{
										image.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
									}
									if (!flag6)
									{
									}
									((UIElement)/*Error near IL_1535: Stack underflow*/).Opacity = (double)/*Error near IL_1535: Stack underflow*/;
									try
									{
										string text4 = Environment.CurrentDirectory + _003F487_003F._003F488_003F("[ŋɠ\u0360Ѫգ\u065d") + eXTEND_;
										if (_003F93_003F(eXTEND_, 1) == _003F487_003F._003F488_003F("\""))
										{
											text4 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + _003F94_003F(eXTEND_, 1, -9999);
										}
										else if (!File.Exists(text4))
										{
											text4 = _003F487_003F._003F488_003F("?ľɓ\u035cѨտ٤ݿ\u087b५\u0a62୵ౙ\u0d54\u0e6aཡၝ") + eXTEND_;
										}
										BitmapImage bitmapImage = new BitmapImage();
										bitmapImage.BeginInit();
										bitmapImage.UriSource = new Uri(text4, UriKind.RelativeOrAbsolute);
										bitmapImage.EndInit();
										image.Source = bitmapImage;
										if (!flag6)
										{
											image.MouseLeftButtonUp += _003F120_003F;
										}
										wrapPanel3.Children.Add(image);
										if (Button_Hide)
										{
											button.Visibility = Visibility.Collapsed;
										}
									}
									catch (Exception)
									{
									}
								}
							}
						}
						num4++;
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
			//IL_0639: Incompatible stack heights: 0 vs 1
			//IL_0649: Incompatible stack heights: 0 vs 1
			//IL_064a: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double num = 0.2;
			double opacity = 1.0;
			Button button = (Button)_003F347_003F;
			int num2 = Convert.ToInt16(((string)button.Tag).Substring(0, 2).Trim());
			string text = button.Name.Substring(2);
			string text2 = ((string)button.Tag).Substring(3);
			int num3 = 0;
			switch (num2)
			{
			case 2:
			case 3:
			case 13:
				num3 = 1;
				break;
			case 4:
			case 5:
			case 14:
				num3 = 2;
				break;
			}
			int num4 = 0;
			if (button.Style == style)
			{
				num4 = 1;
			}
			int num5 = 0;
			bool flag = true;
			if (num4 == 0)
			{
				switch (num3)
				{
				case 1:
					oQuestion.SelectedValues.Clear();
					num5 = 1;
					break;
				case 2:
					num5 = 3;
					break;
				default:
					num5 = 2;
					break;
				}
				oQuestion.SelectedValues.Add(text);
				button.Style = style;
			}
			else if (num3 == 1)
			{
				num4 = 2;
			}
			else
			{
				oQuestion.SelectedValues.Remove(text);
				button.Style = style2;
			}
			if (num4 < 2)
			{
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				bool flag5 = true;
				bool flag6 = true;
				foreach (Border child in GridContent.Children)
				{
					WrapPanel wrapPanel = (WrapPanel)child.Child;
					if (wrapPanel.Name.Substring(1, 1) == _003F487_003F._003F488_003F("S"))
					{
						foreach (WrapPanel child2 in wrapPanel.Children)
						{
							foreach (UIElement child3 in child2.Children)
							{
								if (child3 is Button)
								{
									Button button2 = (Button)child3;
									int num6 = Convert.ToInt16(((string)button2.Tag).Substring(0, 2).Trim());
									string text3 = button2.Name.Substring(2);
									string b = ((string)button2.Tag).Substring(3);
									if (!listFix.Contains(text3))
									{
										if (!(text3 == text))
										{
											switch (num5)
											{
											case 1:
												button2.Style = style2;
												break;
											case 2:
											case 3:
												if (flag3 && (num6 == 2 || num6 == 3 || num6 == 13) && button2.Style == style)
												{
													button2.Style = style2;
													oQuestion.SelectedValues.Remove(text3);
													flag3 = false;
												}
												if (flag4 && text2 == b && (num6 == 4 || num6 == 5 || num6 == 14) && button2.Style == style)
												{
													button2.Style = style2;
													oQuestion.SelectedValues.Remove(text3);
													flag4 = false;
												}
												break;
											}
											if (num5 == 3 && text2 == b)
											{
												button2.Style = style2;
												oQuestion.SelectedValues.Remove(text3);
											}
										}
										if (flag2 && button2.Style == style)
										{
											switch (num6)
											{
											case 1:
											case 3:
											case 5:
												flag2 = false;
												break;
											}
										}
									}
								}
								if (child3 is Image)
								{
									Image image = (Image)child3;
									int num7 = Convert.ToInt16(((string)image.Tag).Substring(0, 2).Trim());
									string text4 = image.Name.Substring(2);
									string b2 = ((string)image.Tag).Substring(3);
									if (!listFix.Contains(text4))
									{
										if (text4 == text)
										{
											if (button.Style != style)
											{
											}
											((UIElement)/*Error near IL_064f: Stack underflow*/).Opacity = (double)/*Error near IL_064f: Stack underflow*/;
										}
										else
										{
											switch (num5)
											{
											case 1:
												image.Opacity = opacity;
												break;
											case 2:
											case 3:
												if (flag5 && (num7 == 2 || num7 == 3 || num7 == 13) && image.Opacity == num)
												{
													image.Opacity = opacity;
													flag5 = false;
												}
												if (flag6 && text2 == b2 && (num7 == 4 || num7 == 5 || num7 == 14) && image.Opacity == num)
												{
													image.Opacity = opacity;
													flag6 = false;
												}
												break;
											}
											if (num5 == 3 && text2 == b2)
											{
												image.Opacity = opacity;
											}
										}
									}
								}
								else if (child3 is TextBox)
								{
									TextBox textBox = (TextBox)child3;
									int num8 = Convert.ToInt16(((string)textBox.Tag).Substring(0, 2).Trim());
									string text5 = textBox.Name.Substring(2);
									string a = ((string)textBox.Tag).Substring(3);
									if (!listFix.Contains(text5))
									{
										if (text5 == text)
										{
											if (button.Style == style)
											{
												textBox.IsEnabled = true;
												textBox.Background = Brushes.White;
												if (SurveyHelper.AutoFill)
												{
													if (textBox.Text == _003F487_003F._003F488_003F(""))
													{
														textBox.Text = oAutoFill.CommonOther(oQuestion.QDefine, text5);
													}
												}
												else if (textBox.Text == _003F487_003F._003F488_003F(""))
												{
													textBox.Focus();
												}
												flag = false;
											}
											else
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
										}
										else
										{
											switch (num5)
											{
											case 1:
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
												break;
											case 2:
											case 3:
												switch (num8)
												{
												case 14:
													if (!(a == text2))
													{
														break;
													}
													goto case 13;
												case 13:
													textBox.Background = Brushes.LightGray;
													textBox.IsEnabled = false;
													break;
												}
												break;
											}
											if (num5 == 3 && a == text2)
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
										}
									}
								}
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

		private void _003F120_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double num = 0.2;
			double opacity = 1.0;
			Image image = (Image)_003F347_003F;
			int num2 = Convert.ToInt16(((string)image.Tag).Substring(0, 2).Trim());
			string text = image.Name.Substring(2);
			string text2 = ((string)image.Tag).Substring(3);
			int num3 = 0;
			switch (num2)
			{
			case 2:
			case 3:
			case 13:
				num3 = 1;
				break;
			case 4:
			case 5:
			case 14:
				num3 = 2;
				break;
			}
			int num4 = 0;
			if (image.Opacity == num)
			{
				num4 = 1;
			}
			int num5 = 0;
			bool flag = true;
			if (num4 == 0)
			{
				switch (num3)
				{
				case 1:
					oQuestion.SelectedValues.Clear();
					num5 = 1;
					break;
				case 2:
					num5 = 3;
					break;
				default:
					num5 = 2;
					break;
				}
				oQuestion.SelectedValues.Add(text);
				image.Opacity = num;
			}
			else if (num3 == 1)
			{
				num4 = 2;
			}
			else
			{
				oQuestion.SelectedValues.Remove(text);
				image.Opacity = opacity;
			}
			if (num4 < 2)
			{
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				bool flag5 = true;
				bool flag6 = true;
				foreach (Border child in GridContent.Children)
				{
					WrapPanel wrapPanel = (WrapPanel)child.Child;
					if (wrapPanel.Name.Substring(1, 1) == _003F487_003F._003F488_003F("S"))
					{
						foreach (WrapPanel child2 in wrapPanel.Children)
						{
							foreach (UIElement child3 in child2.Children)
							{
								if (child3 is Button)
								{
									Button button = (Button)child3;
									int num6 = Convert.ToInt16(((string)button.Tag).Substring(0, 2).Trim());
									string text3 = button.Name.Substring(2);
									string b = ((string)button.Tag).Substring(3);
									if (!listFix.Contains(text3))
									{
										if (text3 == text)
										{
											if (image.Opacity == num)
											{
												button.Style = style;
											}
											else
											{
												button.Style = style2;
											}
										}
										else
										{
											switch (num5)
											{
											case 1:
												button.Style = style2;
												break;
											case 2:
											case 3:
												if (flag3 && (num6 == 2 || num6 == 3 || num6 == 13) && button.Style == style)
												{
													button.Style = style2;
													oQuestion.SelectedValues.Remove(text3);
													flag3 = false;
												}
												if (flag4 && text2 == b && (num6 == 4 || num6 == 5 || num6 == 14) && button.Style == style)
												{
													button.Style = style2;
													oQuestion.SelectedValues.Remove(text3);
													flag4 = false;
												}
												break;
											}
											if (num5 == 3 && text2 == b)
											{
												button.Style = style2;
												oQuestion.SelectedValues.Remove(text3);
											}
										}
										if (flag2 && button.Style == style && (num6 == 1 || num6 == 3 || num6 == 5))
										{
											flag2 = false;
										}
									}
								}
								if (child3 is Image)
								{
									Image image2 = (Image)child3;
									int num7 = Convert.ToInt16(((string)image2.Tag).Substring(0, 2).Trim());
									string text4 = image2.Name.Substring(2);
									string b2 = ((string)image2.Tag).Substring(3);
									if (!listFix.Contains(text4) && !(text4 == text))
									{
										switch (num5)
										{
										case 1:
											image2.Opacity = opacity;
											break;
										case 2:
										case 3:
											if (flag5 && (num7 == 2 || num7 == 3 || num7 == 13) && image2.Opacity == num)
											{
												image2.Opacity = opacity;
												flag5 = false;
											}
											if (flag6 && text2 == b2 && (num7 == 4 || num7 == 5 || num7 == 14) && image2.Opacity == num)
											{
												image2.Opacity = opacity;
												flag6 = false;
											}
											break;
										}
										if (num5 == 3 && text2 == b2)
										{
											image2.Opacity = opacity;
										}
									}
								}
								else if (child3 is TextBox)
								{
									TextBox textBox = (TextBox)child3;
									int num8 = Convert.ToInt16(((string)textBox.Tag).Substring(0, 2).Trim());
									string text5 = textBox.Name.Substring(2);
									string a = ((string)textBox.Tag).Substring(3);
									if (!listFix.Contains(text5))
									{
										if (text5 == text)
										{
											if (image.Opacity == num)
											{
												textBox.IsEnabled = true;
												textBox.Background = Brushes.White;
												if (textBox.Text == _003F487_003F._003F488_003F(""))
												{
													textBox.Focus();
												}
												flag = false;
											}
											else
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
										}
										else
										{
											switch (num5)
											{
											case 1:
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
												break;
											case 2:
											case 3:
												switch (num8)
												{
												case 14:
													if (!(a == text2))
													{
														break;
													}
													goto case 13;
												case 13:
													textBox.Background = Brushes.LightGray;
													textBox.IsEnabled = false;
													break;
												}
												break;
											}
											if (num5 == 3 && a == text2)
											{
												textBox.Background = Brushes.LightGray;
												textBox.IsEnabled = false;
											}
										}
									}
								}
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
			//IL_01ef: Incompatible stack heights: 0 vs 1
			//IL_0208: Incompatible stack heights: 0 vs 1
			//IL_0218: Incompatible stack heights: 0 vs 1
			//IL_03a6: Incompatible stack heights: 0 vs 2
			//IL_03bf: Incompatible stack heights: 0 vs 1
			//IL_03cb: Incompatible stack heights: 0 vs 1
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
			if (ExistCodeFills)
			{
				oQuestion.FillTexts.Clear();
				foreach (Border child in GridContent.Children)
				{
					WrapPanel wrapPanel = (WrapPanel)child.Child;
					if (wrapPanel.Name.Substring(1, 1) == _003F487_003F._003F488_003F("S"))
					{
						foreach (WrapPanel child2 in wrapPanel.Children)
						{
							foreach (UIElement child3 in child2.Children)
							{
								if (child3 is TextBox)
								{
									TextBox textBox = (TextBox)child3;
									if (textBox.IsEnabled && textBox.Text.Trim() == _003F487_003F._003F488_003F(""))
									{
										MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
										textBox.Focus();
										return true;
									}
									string text = textBox.Name.Substring(2);
									Dictionary<string, string> fillText = oQuestion.FillTexts;
									if (!textBox.IsEnabled)
									{
										_003F487_003F._003F488_003F("");
									}
									else
									{
										textBox.Text.Trim();
									}
									((Dictionary<string, string>)/*Error near IL_03d0: Stack underflow*/).Add((string)/*Error near IL_03d0: Stack underflow*/, (string)/*Error near IL_03d0: Stack underflow*/);
								}
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
			//IL_0047: Incompatible stack heights: 0 vs 3
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyMsg.DelaySeconds > 0)
			{
				PageNav oPageNav2 = oPageNav;
				int delaySecond = SurveyMsg.DelaySeconds;
				Button _003F431_003F = btnNav;
				string mySurveyId = MySurveyId;
				((PageNav)/*Error near IL_0058: Stack underflow*/).PageDataLog((int)/*Error near IL_0058: Stack underflow*/, (List<VEAnswer>)/*Error near IL_0058: Stack underflow*/, _003F431_003F, mySurveyId);
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00b2: Incompatible stack heights: 0 vs 1
			//IL_00e6: Incompatible stack heights: 0 vs 4
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
				string content = btnNav_Content;
				((ContentControl)/*Error near IL_0046: Stack underflow*/).Content = content;
				return;
			}
			List<VEAnswer> list = _003F88_003F();
			oLogicEngine.PageAnswer = list;
			oPageNav.oLogicEngine = oLogicEngine;
			if (!oPageNav.CheckLogic(CurPageId))
			{
				btnNav.Content = btnNav_Content;
				return;
			}
			goto IL_0081;
			IL_0081:
			_003F89_003F(list);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				string msgCaption = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_00eb: Stack underflow*/, (string)/*Error near IL_00eb: Stack underflow*/, (MessageBoxButton)/*Error near IL_00eb: Stack underflow*/, (MessageBoxImage)/*Error near IL_00eb: Stack underflow*/);
			}
			MyNav.PageAnswer = list;
			oPageNav.NextPage(MyNav, base.NavigationService);
			btnNav.Content = btnNav_Content;
			return;
			IL_00c9:
			goto IL_0081;
			IL_009d:
			goto IL_0020;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0026: Incompatible stack heights: 0 vs 1
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				((MultipleGroup)/*Error near IL_002b: Stack underflow*/).btnNav.Foreground = Brushes.Black;
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
			//IL_0076: Incompatible stack heights: 0 vs 1
			//IL_007b: Incompatible stack heights: 1 vs 0
			//IL_0080: Incompatible stack heights: 0 vs 2
			//IL_0086: Incompatible stack heights: 0 vs 1
			//IL_008b: Incompatible stack heights: 1 vs 0
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
			int length2;
			if (num2 + num > _003F362_003F.Length)
			{
				length2 = _003F362_003F.Length - num2;
			}
			return ((string)/*Error near IL_0090: Stack underflow*/).Substring((int)/*Error near IL_0090: Stack underflow*/, length2);
		}

		private string _003F95_003F(string _003F362_003F, int _003F365_003F = 1)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Incompatible stack heights: 0 vs 1
			//IL_0037: Incompatible stack heights: 1 vs 0
			//IL_003c: Incompatible stack heights: 0 vs 1
			//IL_0047: Incompatible stack heights: 0 vs 1
			if (_003F365_003F < 0)
			{
			}
			int num = 0;
			if (num > _003F362_003F.Length)
			{
				goto IL_004c;
			}
			int length = _003F362_003F.Length;
			int startIndex = /*Error near IL_001d: Stack underflow*/- num;
			goto IL_004d;
			IL_004c:
			startIndex = 0;
			goto IL_004d;
			IL_004d:
			return ((string)/*Error near IL_0052: Stack underflow*/).Substring(startIndex);
			IL_0022:
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
			goto IL_001b;
			IL_001b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0003Ŭə\u035aёԉ٥\u0744ࡔ\u094aਙ\u0b42\u0c4f൲\u0e6e\u0f72\u1072ᅾቴ፭ᐷᕡᙿᝰᡣ\u193c\u1a7f᭤\u1c7cᵻṧώ\u2060Ⅾ≭⍻⑧╲♶✫⡼⥢⩯⭭"), UriKind.Relative);
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
				((MultipleGroup)_003F350_003F).Loaded += _003F80_003F;
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
				GridContent = (Grid)_003F350_003F;
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
