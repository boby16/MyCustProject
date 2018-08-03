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
	public class SingleGroup : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__21_0;

			public static Func<SurveyDetail, global::_003F1_003F<string, string, int, string>> _003C_003E9__22_1;

			internal int _003F315_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal global::_003F1_003F<string, string, int, string> _003F327_003F(SurveyDetail _003F483_003F)
			{
				return new global::_003F1_003F<string, string, int, string>(_003F483_003F.CODE, _003F483_003F.CODE_TEXT, _003F483_003F.IS_OTHER, _003F483_003F.EXTEND_1);
			}
		}

		[CompilerGenerated]
		private sealed class _003F13_003F
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

		private BoldTitle oBoldTitle = new BoldTitle();

		private QSingle oQuestion = new QSingle();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

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

		public SingleGroup()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0576: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_070a: Incompatible stack heights: 0 vs 2
			//IL_0721: Incompatible stack heights: 0 vs 1
			//IL_0ab3: Incompatible stack heights: 0 vs 1
			//IL_0ad2: Incompatible stack heights: 0 vs 1
			//IL_0ad7: Incompatible stack heights: 0 vs 1
			//IL_0afc: Incompatible stack heights: 0 vs 1
			//IL_0b1b: Incompatible stack heights: 0 vs 1
			//IL_0b20: Incompatible stack heights: 0 vs 1
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
					if (_003F7_003F._003C_003E9__21_0 == null)
					{
						_003F7_003F._003C_003E9__21_0 = _003F7_003F._003C_003E9._003F315_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0726: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0726: Stack underflow*/);
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
				ShowLogic = true;
				string[] array3 = oLogicEngine.aryCode(list2[0], ',');
				List<SurveyDetail> list5 = new List<SurveyDetail>();
				for (int l = 0; l < array3.Count(); l++)
				{
					enumerator = oQuestion.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							if (current2.CODE == array3[l].ToString())
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
			((SingleGroup)/*Error near IL_0adc: Stack underflow*/).Button_FontSize = (int)/*Error near IL_0adc: Stack underflow*/;
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				int cONTROL_HEIGHT = oQuestion.QDefine.CONTROL_HEIGHT;
			}
			else
			{
				int btnHeight = SurveyHelper.BtnHeight;
			}
			((SingleGroup)/*Error near IL_0b25: Stack underflow*/).Button_Height = (int)/*Error near IL_0b25: Stack underflow*/;
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
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double opacity = 0.2;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Grid gridContent = GridContent;
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
											if (button.Name.Substring(2) == oQuestion.SelectedCode)
											{
												button.Style = style;
												flag3 = true;
												int num = (int)button.Tag;
												if (num == 1 || num == 3 || num == 5 || num == 11 || ((num == 13) | (num == 14)))
												{
													flag = true;
												}
											}
										}
										else if (child3 is Image)
										{
											Image image = (Image)child3;
											if (image.Name.Substring(2) == oQuestion.SelectedCode)
											{
												image.Opacity = opacity;
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
									Button button3 = (Button)child6;
									if (button3.Name.Substring(2) == oQuestion.SelectedCode)
									{
										button3.Style = style;
										flag3 = true;
										int num2 = (int)button3.Tag;
										if (num2 == 1 || num2 == 3 || num2 == 5 || num2 == 11 || ((num2 == 13) | (num2 == 14)))
										{
											flag = true;
										}
									}
								}
								else if (child6 is Image)
								{
									Image image2 = (Image)child6;
									if (image2.Name.Substring(2) == oQuestion.SelectedCode)
									{
										image2.Opacity = opacity;
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
				txtFill.Text = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
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
			//IL_0513: Incompatible stack heights: 0 vs 1
			//IL_0514: Incompatible stack heights: 0 vs 1
			//IL_0554: Incompatible stack heights: 0 vs 1
			//IL_0555: Incompatible stack heights: 0 vs 1
			//IL_07f1: Incompatible stack heights: 0 vs 2
			//IL_0808: Incompatible stack heights: 0 vs 1
			//IL_08df: Incompatible stack heights: 0 vs 1
			//IL_08ef: Incompatible stack heights: 0 vs 1
			//IL_08f0: Incompatible stack heights: 0 vs 1
			//IL_09ad: Incompatible stack heights: 0 vs 1
			//IL_09be: Incompatible stack heights: 0 vs 1
			//IL_09c8: Incompatible stack heights: 0 vs 1
			//IL_0a80: Incompatible stack heights: 0 vs 1
			//IL_0a90: Incompatible stack heights: 0 vs 1
			//IL_0a91: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("Qžɾͻѫգٸ\u0746\u086f७\u0a61୲౫\u0d56\u0e70\u0f7aၮᅤ"));
			Brush borderBrush = (Brush)FindResource(_003F487_003F._003F488_003F("_ſɽ\u0363Ѭ\u0560ىݥ\u087b६\u0a62୴\u0c47\u0d76\u0e76\u0f71\u1069"));
			Brush foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
			string text = oQuestion.QDefine.CONTROL_TOOLTIP.ToUpper().Trim();
			string text2 = _003F487_003F._003F488_003F("S");
			string text3 = _003F487_003F._003F488_003F("B");
			int num = 0;
			if (text != _003F487_003F._003F488_003F(""))
			{
				text2 = _003F93_003F(text, 1);
				if (_003F487_003F._003F488_003F(":ĸȺ\u0334в\u0530ز\u0734࠺स").Contains(text2))
				{
					text2 = _003F487_003F._003F488_003F("S");
					if (text != _003F487_003F._003F488_003F(""))
					{
						num = Convert.ToInt32(text);
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
							num = Convert.ToInt32(text);
						}
					}
					else if (_003F94_003F(text, 1, -9999) != _003F487_003F._003F488_003F(""))
					{
						num = Convert.ToInt32(_003F94_003F(text, 1, -9999));
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
			Orientation orientation = (Orientation)/*Error near IL_0516: Stack underflow*/;
			if (!(oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("I")))
			{
			}
			Orientation orientation2 = (Orientation)/*Error near IL_0557: Stack underflow*/;
			int button_FontSize = Button_FontSize;
			Math.Abs(Button_FontSize);
			Grid gridContent = GridContent;
			oQuestion.GetGroupDetails();
			List<SurveyDetail>.Enumerator enumerator;
			if (ShowLogic)
			{
				List<SurveyDetail> list = new List<SurveyDetail>();
				enumerator = oQuestion.QDetails.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						SurveyDetail current = enumerator.Current;
						bool flag = true;
						List<SurveyDetail>.Enumerator enumerator2 = list.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								SurveyDetail current2 = enumerator2.Current;
								if (current.PARENT_CODE == current2.CODE)
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
									SurveyDetail current3 = enumerator2.Current;
									if (current.PARENT_CODE == current3.CODE)
									{
										list.Add(current3);
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
			int num2 = 0;
			enumerator = oQuestion.QGroupDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current4 = enumerator.Current;
					_003F13_003F _003F13_003F = new _003F13_003F();
					_003F13_003F.strGroupCode = current4.CODE;
					string cODE_TEXT = current4.CODE_TEXT;
					oQuestion.QDetails.Where(_003F13_003F._003F314_003F);
					if (_003F7_003F._003C_003E9__22_1 == null)
					{
						_003F7_003F._003C_003E9__22_1 = _003F7_003F._003C_003E9._003F327_003F;
					}
					IEnumerable<global::_003F1_003F<string, string, int, string>> enumerable = ((IEnumerable<SurveyDetail>)/*Error near IL_080d: Stack underflow*/).Select((Func<SurveyDetail, global::_003F1_003F<string, string, int, string>>)/*Error near IL_080d: Stack underflow*/);
					bool flag2 = false;
					foreach (global::_003F1_003F<string, string, int, string> item in enumerable)
					{
						global::_003F1_003F<string, string, int, string> _003F1_003F = item;
						flag2 = true;
					}
					if (flag2)
					{
						gridContent.RowDefinitions.Add(new RowDefinition
						{
							Height = GridLength.Auto
						});
						Border border = new Border();
						if (!((cODE_TEXT == _003F487_003F._003F488_003F("")) | (oQuestion.QDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("1"))))
						{
						}
						Thickness borderThickness = new Thickness((double)/*Error near IL_08f1: Stack underflow*/);
						((Border)/*Error near IL_08fb: Stack underflow*/).BorderThickness = borderThickness;
						border.BorderBrush = borderBrush;
						border.SetValue(Grid.RowProperty, num2);
						border.SetValue(Grid.ColumnProperty, 0);
						gridContent.Children.Add(border);
						WrapPanel wrapPanel = new WrapPanel();
						wrapPanel.VerticalAlignment = verticalAlignment;
						wrapPanel.HorizontalAlignment = horizontalAlignment;
						wrapPanel.Name = _003F487_003F._003F488_003F("uō") + _003F13_003F.strGroupCode;
						border.Child = wrapPanel;
						TextBlock textBlock = new TextBlock();
						if (oQuestion.QDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("1"))
						{
							_003F487_003F._003F488_003F("");
						}
						((TextBlock)/*Error near IL_09cd: Stack underflow*/).Text = (string)/*Error near IL_09cd: Stack underflow*/;
						textBlock.Style = style2;
						textBlock.Foreground = foreground;
						if (num > 0)
						{
							textBlock.Width = (double)num;
						}
						textBlock.TextWrapping = TextWrapping.Wrap;
						textBlock.Margin = new Thickness(15.0, 0.0, 15.0, 0.0);
						textBlock.VerticalAlignment = verticalAlignment;
						wrapPanel.Children.Add(textBlock);
						border = new Border();
						if (!(oQuestion.QDefine.CONTROL_TOOLTIP == _003F487_003F._003F488_003F("1")))
						{
						}
						Thickness borderThickness2 = new Thickness((double)/*Error near IL_0a92: Stack underflow*/);
						((Border)/*Error near IL_0a9c: Stack underflow*/).BorderThickness = borderThickness2;
						border.BorderBrush = borderBrush;
						border.SetValue(Grid.RowProperty, num2);
						border.SetValue(Grid.ColumnProperty, 1);
						gridContent.Children.Add(border);
						WrapPanel wrapPanel2 = new WrapPanel();
						wrapPanel2.Orientation = orientation;
						wrapPanel2.Margin = new Thickness(15.0, 0.0, 0.0, 13.0);
						wrapPanel2.Name = _003F487_003F._003F488_003F("uœ") + _003F13_003F.strGroupCode;
						border.Child = wrapPanel2;
						foreach (global::_003F1_003F<string, string, int, string> item2 in enumerable)
						{
							WrapPanel wrapPanel3 = new WrapPanel();
							wrapPanel3.Margin = new Thickness(0.0, 15.0, 15.0, 0.0);
							wrapPanel3.Orientation = orientation2;
							wrapPanel2.Children.Add(wrapPanel3);
							Button button = new Button();
							button.Name = _003F487_003F._003F488_003F("`Ş") + item2.CODE;
							button.Content = item2.CODE_TEXT;
							button.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
							button.Style = style;
							button.Tag = item2.IS_OTHER;
							if (item2.IS_OTHER == 1 || item2.IS_OTHER == 3 || item2.IS_OTHER == 5 || ((item2.IS_OTHER == 11) | (item2.IS_OTHER == 13)) || item2.IS_OTHER == 14)
							{
								ExistTextFill = true;
							}
							button.Click += _003F29_003F;
							button.FontSize = (double)Button_FontSize;
							button.MinWidth = (double)Button_Width;
							button.MinHeight = (double)Button_Height;
							wrapPanel3.Children.Add(button);
							listButton.Add(button);
							string eXTEND_ = item2.EXTEND_1;
							if (eXTEND_ != _003F487_003F._003F488_003F(""))
							{
								Image image = new Image();
								image.Name = _003F487_003F._003F488_003F("rŞ") + item2.CODE;
								image.Tag = item2.IS_OTHER;
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
									image.MouseLeftButtonUp += _003F120_003F;
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
						num2++;
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
			//IL_0076: Incompatible stack heights: 0 vs 1
			//IL_0209: Incompatible stack heights: 0 vs 1
			//IL_0219: Incompatible stack heights: 0 vs 1
			//IL_021a: Incompatible stack heights: 0 vs 1
			//IL_0262: Incompatible stack heights: 0 vs 1
			//IL_0272: Incompatible stack heights: 0 vs 1
			//IL_0273: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double num = 0.2;
			double num2 = 1.0;
			Button obj = (Button)_003F347_003F;
			int num3 = (int)obj.Tag;
			string text = obj.Name.Substring(2);
			int num4 = 0;
			if (num3 == 1 || num3 == 3 || num3 == 5 || num3 == 11 || num3 == 13 || num3 == 14)
			{
				num4 = 1;
			}
			int num5 = 0;
			if (((FrameworkElement)/*Error near IL_00de: Stack underflow*/).Style == style)
			{
				num5 = 1;
			}
			if (num5 == 0)
			{
				oQuestion.SelectedCode = text;
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
									string a = ((Button)child3).Name.Substring(2);
									if (!(a == text))
									{
									}
									((FrameworkElement)/*Error near IL_021f: Stack underflow*/).Style = (Style)/*Error near IL_021f: Stack underflow*/;
								}
								if (child3 is Image)
								{
									string a2 = ((Image)child3).Name.Substring(2);
									if (!(a2 == text))
									{
									}
									((UIElement)/*Error near IL_0278: Stack underflow*/).Opacity = (double)/*Error near IL_0278: Stack underflow*/;
								}
							}
						}
					}
				}
				if (num4 == 0)
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

		private void _003F120_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0076: Incompatible stack heights: 0 vs 1
			//IL_0209: Incompatible stack heights: 0 vs 1
			//IL_0219: Incompatible stack heights: 0 vs 1
			//IL_021a: Incompatible stack heights: 0 vs 1
			//IL_0262: Incompatible stack heights: 0 vs 1
			//IL_0272: Incompatible stack heights: 0 vs 1
			//IL_0273: Incompatible stack heights: 0 vs 1
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			double num = 0.2;
			double num2 = 1.0;
			Image obj = (Image)_003F347_003F;
			int num3 = (int)obj.Tag;
			string text = obj.Name.Substring(2);
			int num4 = 0;
			if (num3 == 1 || num3 == 3 || num3 == 5 || num3 == 11 || num3 == 13 || num3 == 14)
			{
				num4 = 1;
			}
			int num5 = 0;
			if (((UIElement)/*Error near IL_00de: Stack underflow*/).Opacity == num)
			{
				num5 = 1;
			}
			if (num5 == 0)
			{
				oQuestion.SelectedCode = text;
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
									string a = ((Button)child3).Name.Substring(2);
									if (!(a == text))
									{
									}
									((FrameworkElement)/*Error near IL_021f: Stack underflow*/).Style = (Style)/*Error near IL_021f: Stack underflow*/;
								}
								if (child3 is Image)
								{
									string a2 = ((Image)child3).Name.Substring(2);
									if (!(a2 == text))
									{
									}
									((UIElement)/*Error near IL_0278: Stack underflow*/).Opacity = (double)/*Error near IL_0278: Stack underflow*/;
								}
							}
						}
					}
				}
				if (num4 == 0)
				{
					txtFill.Background = Brushes.LightGray;
					txtFill.IsEnabled = false;
				}
				else
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
					txtFill.Focus();
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕣᙽ\u1776ᡥ\u193e\u1a63᭦ᱠ\u1d6aṠὮ\u206dⅻ≧⍲⑶┫♼❢⡯⥭"), UriKind.Relative);
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
				((SingleGroup)_003F350_003F).Loaded += _003F80_003F;
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
			return;
			IL_003d:
			goto IL_0047;
		}
	}
}
