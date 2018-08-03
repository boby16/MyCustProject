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
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class MultipleEntry : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__23_0;

			public static Func<SurveyDetail, int> _003C_003E9__24_1;

			internal int _003F310_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}

			internal int _003F311_003F(SurveyDetail _003F483_003F)
			{
				return _003F483_003F.INNER_ORDER;
			}
		}

		[CompilerGenerated]
		private sealed class _003F8_003F
		{
			public int nSearch;

			public MultipleEntry _003C_003E4__this;

			internal bool _003F312_003F(SurveyDetail _003F483_003F)
			{
				return _003C_003E4__this._003F96_003F(_003F483_003F.CODE) == nSearch;
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private UDPX oFunc = new UDPX();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QMultiple oQuestion = new QMultiple();

		private bool ExistTextFill;

		private List<string> listPreSet = new List<string>();

		private List<string> listOther = new List<string>();

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private int CodeMaxLen = 1;

		private List<SurveyDetail> oListSource = new List<SurveyDetail>();

		private string SearchKey = _003F487_003F._003F488_003F("");

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal Grid gridContent;

		internal TextBlock txtSearchTitle;

		internal TextBox txtSearch;

		internal Button btnSelect;

		internal ListBox ListOption;

		internal TextBlock txtSelectTitle;

		internal TextBlock DelMsg;

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

		public MultipleEntry()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0576: Incompatible stack heights: 0 vs 1
			//IL_057d: Incompatible stack heights: 0 vs 1
			//IL_070a: Incompatible stack heights: 0 vs 2
			//IL_0721: Incompatible stack heights: 0 vs 1
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
					if (_003F7_003F._003C_003E9__23_0 == null)
					{
						_003F7_003F._003C_003E9__23_0 = _003F7_003F._003C_003E9._003F310_003F;
					}
					((List<SurveyDetail>)/*Error near IL_0726: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0726: Stack underflow*/);
				}
				oQuestion.QDetails = list4;
			}
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
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
			else
			{
				int iS_RANDOM = oQuestion.QDefine.IS_RANDOM;
			}
			for (int m = 0; m < oQuestion.QDetails.Count(); m++)
			{
				if (CodeMaxLen < oQuestion.QDetails[m].CODE.Length)
				{
					CodeMaxLen = oQuestion.QDetails[m].CODE.Length;
				}
			}
			txtSearch.MaxLength = CodeMaxLen;
			Button_Height = SurveyHelper.BtnHeight;
			Button_FontSize = SurveyHelper.BtnFontSize;
			Button_Width = SurveyHelper.BtnWidth;
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
			ExistTextFill = false;
			enumerator = oQuestion.QDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int iS_OTHER = enumerator.Current.IS_OTHER;
					if (iS_OTHER == 1 || iS_OTHER == 3 || iS_OTHER == 5 || iS_OTHER == 11 || iS_OTHER == 13 || iS_OTHER == 14)
					{
						ExistTextFill = true;
						break;
					}
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
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
			if (oQuestion.QDefine.CONTROL_MASK != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQuestion.QDefine.CONTROL_MASK;
				oBoldTitle.SetTextBlock(txtSelectTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			}
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQuestion.QDefine.CONTROL_TOOLTIP;
				oBoldTitle.SetTextBlock(txtSearchTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			}
			oListSource = oQuestion.QDetails;
			_003F113_003F();
			txtSearch.Focus();
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				List<SurveyDetail> list6 = autoFill.MultiDetail(oQuestion.QDefine, oQuestion.QDetails, 10);
				enumerator = list6.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						SurveyDetail current3 = enumerator.Current;
						txtSearch.Text = current3.CODE_TEXT;
						_003F117_003F(btnSelect, new RoutedEventArgs());
					}
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				if (txtFill.IsEnabled)
				{
					txtFill.Text = autoFill.CommonOther(oQuestion.QDefine, _003F487_003F._003F488_003F(""));
				}
				if (list6.Count > 0 && autoFill.AutoNext(oQuestion.QDefine))
				{
					_003F58_003F(this, _003F348_003F);
				}
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
					foreach (string item in listPreSet)
					{
						if (!oQuestion.SelectedValues.Contains(item))
						{
							oQuestion.SelectedValues.Add(item);
							enumerator = oQuestion.QDetails.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									SurveyDetail current5 = enumerator.Current;
									if (current5.CODE == item)
									{
										_003F119_003F(item, current5.CODE_TEXT, current5.IS_OTHER);
										int iS_OTHER2 = current5.IS_OTHER;
										if (iS_OTHER2 == 1 || iS_OTHER2 == 3 || iS_OTHER2 == 5 || ((iS_OTHER2 == 11) | (iS_OTHER2 == 13)) || iS_OTHER2 == 14)
										{
											listOther.Add(item);
											flag = true;
										}
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
					if (flag)
					{
						txtFill.IsEnabled = true;
						txtFill.Background = Brushes.White;
						txtFill.Focus();
					}
					if (oQuestion.QDetails.Count == 1)
					{
						if (oQuestion.SelectedValues.Count == 0 && (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode1) || oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2)))
						{
							ListOption.SelectedValue = oQuestion.QDetails[0].CODE_TEXT;
							_003F114_003F(ListOption, null);
							_003F117_003F(null, null);
						}
						if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode2))
						{
							if (txtFill.IsEnabled)
							{
								txtFill.Focus();
							}
							else if (!SurveyHelper.AutoFill)
							{
								_003F58_003F(this, _003F348_003F);
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
							_003F58_003F(this, _003F348_003F);
						}
					}
				}
			}
			else
			{
				oQuestion.ReadAnswer(MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer item2 in oQuestion.QAnswersRead)
				{
					if (_003F94_003F(item2.QUESTION_NAME, 0, (oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ")).Length) == oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ"))
					{
						oQuestion.SelectedValues.Add(item2.CODE);
						enumerator = oQuestion.QDetails.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail current7 = enumerator.Current;
								if (current7.CODE == item2.CODE)
								{
									_003F119_003F(item2.CODE, current7.CODE_TEXT, current7.IS_OTHER);
									int iS_OTHER3 = current7.IS_OTHER;
									if (iS_OTHER3 == 1 || iS_OTHER3 == 3 || iS_OTHER3 == 5 || ((iS_OTHER3 == 11) | (iS_OTHER3 == 13)) || iS_OTHER3 == 14)
									{
										listOther.Add(item2.CODE);
										flag = true;
									}
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
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

		private void _003F104_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00c3: Incompatible stack heights: 0 vs 2
			//IL_00e8: Incompatible stack heights: 0 vs 2
			//IL_0104: Incompatible stack heights: 0 vs 1
			_003F8_003F _003F8_003F = new _003F8_003F();
			_003F8_003F._003C_003E4__this = this;
			string text = txtSearch.Text;
			if (text == _003F487_003F._003F488_003F("+"))
			{
				text = _003F487_003F._003F488_003F("");
			}
			_003F8_003F.nSearch = _003F96_003F(text);
			if (SearchKey != text)
			{
				_003F487_003F._003F488_003F("");
				if (!((string)/*Error near IL_0051: Stack underflow*/ == (string)/*Error near IL_0051: Stack underflow*/))
				{
					oQuestion.QDetails.Where(_003F8_003F._003F312_003F);
					if (_003F7_003F._003C_003E9__24_1 == null)
					{
						_003F7_003F._003C_003E9__24_1 = _003F7_003F._003C_003E9._003F311_003F;
					}
					IOrderedEnumerable<SurveyDetail> source = ((IEnumerable<SurveyDetail>)/*Error near IL_0087: Stack underflow*/).OrderBy((Func<SurveyDetail, int>)/*Error near IL_0087: Stack underflow*/);
					oListSource = source.ToList();
				}
				else
				{
					oListSource = oQuestion.QDetails;
				}
				_003F113_003F();
				SearchKey = text;
			}
		}

		private void _003F113_003F()
		{
			ListOption.Items.Clear();
			foreach (SurveyDetail item in oListSource)
			{
				ListOption.Items.Add(item.CODE + _003F487_003F._003F488_003F("/") + item.CODE_TEXT);
			}
		}

		private void _003F114_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
		}

		private void _003F115_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_005e: Incompatible stack heights: 0 vs 2
			//IL_0073: Incompatible stack heights: 0 vs 1
			//IL_008d: Incompatible stack heights: 0 vs 1
			if (_003F348_003F.Key != Key.Return)
			{
				return;
			}
			string text = txtSearch.Text;
			string b = _003F487_003F._003F488_003F((string)/*Error near IL_0011: Stack underflow*/);
			if (!((string)/*Error near IL_0016: Stack underflow*/ == b))
			{
				_003F117_003F(null, null);
				return;
			}
			bool isEnabled = txtFill.IsEnabled;
			if ((int)/*Error near IL_0078: Stack underflow*/ != 0)
			{
				txtFill.Text.Trim();
				string b2 = _003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_002f: Stack underflow*/ == b2)
				{
					txtFill.Focus();
					return;
				}
			}
			goto IL_0034;
			IL_0034:
			_003F58_003F(null, null);
			return;
			IL_009f:
			goto IL_0034;
		}

		private void _003F116_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			if (txtSearch.Text.Length == CodeMaxLen)
			{
				_003F117_003F(null, null);
			}
		}

		private void _003F117_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			_003F104_003F(null, null);
			txtSearch.Focus();
			txtSearch.SelectAll();
			if (ListOption.Items.Count == 0)
			{
				oFunc.BEEP(500, 700);
			}
			else if (txtSearch.Text == _003F487_003F._003F488_003F("+"))
			{
				txtSearch.Text = _003F487_003F._003F488_003F("");
			}
			else
			{
				if (txtSearch.Text != _003F487_003F._003F488_003F(""))
				{
					ListOption.SelectedValue = ListOption.Items[0];
				}
				if (!((string)ListOption.SelectedValue == _003F487_003F._003F488_003F("")) && (string)ListOption.SelectedValue != null)
				{
					string text = _003F487_003F._003F488_003F("");
					string _003F372_003F = _003F487_003F._003F488_003F("");
					int num = 0;
					foreach (SurveyDetail item in oListSource)
					{
						if (item.CODE + _003F487_003F._003F488_003F("/") + item.CODE_TEXT == (string)ListOption.SelectedValue)
						{
							text = item.CODE;
							_003F372_003F = item.CODE_TEXT;
							num = item.IS_OTHER;
							if (!SurveyHelper.AutoFill || !(SurveyHelper.FillMode == _003F487_003F._003F488_003F("2")) || !(oQuestion.QDefine.FILLDATA == _003F487_003F._003F488_003F("")) || (num != 2 && num != 4 && num != 3 && num != 5 && num != 13 && num != 14))
							{
								if (num == 1 || num == 3 || num == 5 || num == 11 || num == 13 || num == 14)
								{
									if (!listOther.Contains(text))
									{
										listOther.Add(text);
									}
									txtFill.IsEnabled = true;
									txtFill.Background = Brushes.White;
									txtFill.Focus();
								}
								break;
							}
							return;
						}
					}
					bool flag = false;
					for (int i = 0; i < oQuestion.SelectedValues.Count; i++)
					{
						if (oQuestion.SelectedValues[i] == text)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						_003F119_003F(text, _003F372_003F, num);
						oQuestion.SelectedValues.Add(text);
					}
					txtSearch.Focus();
					txtSearch.Text = _003F487_003F._003F488_003F("");
				}
			}
		}

		private void _003F118_003F(object _003F347_003F, MouseButtonEventArgs _003F348_003F)
		{
			_003F117_003F(_003F347_003F, _003F348_003F);
		}

		private void _003F119_003F(string _003F371_003F, string _003F372_003F, int _003F344_003F)
		{
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			WrapPanel wrapPanel = wrapPanel1;
			Button element = new Button
			{
				Name = _003F487_003F._003F488_003F("`Ş") + _003F371_003F,
				Content = _003F371_003F + _003F487_003F._003F488_003F("/") + _003F372_003F,
				Margin = new Thickness(0.0, 0.0, 15.0, 15.0),
				Style = style,
				Tag = (object)_003F344_003F,
				Click = new RoutedEventHandler(_003F29_003F),
				FontSize = (double)Button_FontSize,
				MinWidth = (double)Button_Width,
				MinHeight = (double)Button_Height
			};
			wrapPanel.Children.Add(element);
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00a2: Incompatible stack heights: 0 vs 2
			//IL_00c4: Incompatible stack heights: 0 vs 1
			//IL_00e5: Incompatible stack heights: 0 vs 2
			Button button = (Button)_003F347_003F;
			string text = button.Name.Substring(2);
			for (int i = 0; i < oQuestion.SelectedValues.Count; i++)
			{
				if (oQuestion.SelectedValues[i] == text)
				{
					List<string> selectedValue = oQuestion.SelectedValues;
					((List<string>)/*Error near IL_003c: Stack underflow*/).RemoveAt((int)/*Error near IL_003c: Stack underflow*/);
					if (listOther.Contains(text))
					{
						listOther.Remove(text);
						int count = listOther.Count;
						if ((int)/*Error near IL_00c9: Stack underflow*/ == 0)
						{
							txtFill.IsEnabled = false;
							TextBox txtFill2 = txtFill;
							SolidColorBrush lightGray = Brushes.LightGray;
							((System.Windows.Controls.Control)/*Error near IL_0057: Stack underflow*/).Background = (Brush)/*Error near IL_0057: Stack underflow*/;
						}
					}
					break;
				}
			}
			wrapPanel1.Children.Remove(button);
			txtSearch.Focus();
			txtSearch.SelectAll();
		}

		private bool _003F87_003F()
		{
			//IL_01d7: Incompatible stack heights: 0 vs 1
			//IL_01f0: Incompatible stack heights: 0 vs 1
			//IL_0200: Incompatible stack heights: 0 vs 1
			//IL_0261: Incompatible stack heights: 0 vs 1
			//IL_02ba: Incompatible stack heights: 0 vs 1
			//IL_02bb: Incompatible stack heights: 0 vs 1
			//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
			if (oQuestion.SelectedValues.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (oQuestion.QDefine.MIN_COUNT != 0 && oQuestion.SelectedValues.Count < oQuestion.QDefine.MIN_COUNT)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgMAless, oQuestion.QDefine.MIN_COUNT.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return true;
			}
			if (oQuestion.QDefine.MAX_COUNT != 0 && oQuestion.SelectedValues.Count > oQuestion.QDefine.MAX_COUNT)
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
				((QMultiple)/*Error near IL_0205: Stack underflow*/).FillText = (string)/*Error near IL_0205: Stack underflow*/;
			}
			foreach (Button child in wrapPanel1.Children)
			{
				int num = (int)child.Tag;
				bool flag = wrapPanel1.Children.Count > 1;
				if (num != 2 && num != 4 && num != 3 && num != 5 && num != 13)
				{
				}
				if ((/*Error near IL_02bc: Stack underflow*/ & /*Error near IL_02bc: Stack underflow*/) != 0)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgNotSelectOther, child.Content), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			//IL_011c: Incompatible stack heights: 0 vs 4
			//IL_0144: Incompatible stack heights: 0 vs 3
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			for (int i = 0; i < oQuestion.SelectedValues.Count; i++)
			{
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("]ŀ") + (i + 1).ToString();
				vEAnswer.CODE = oQuestion.SelectedValues[i].ToString();
				list.Add(vEAnswer);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer.QUESTION_NAME + _003F487_003F._003F488_003F("<") + vEAnswer.CODE;
			}
			string answer = SurveyHelper.Answer;
			SurveyHelper.Answer = ((MultipleEntry)/*Error near IL_00d3: Stack underflow*/)._003F94_003F((string)/*Error near IL_00d3: Stack underflow*/, (int)/*Error near IL_00d3: Stack underflow*/, (int)/*Error near IL_00d3: Stack underflow*/);
			if (oQuestion.FillText != _003F487_003F._003F488_003F(""))
			{
				VEAnswer vEAnswer2 = new VEAnswer();
				string questionName = oQuestion.QuestionName;
				_003F487_003F._003F488_003F("[Ōɖ\u0349");
				string qUESTION_NAME = (string)/*Error near IL_00fc: Stack underflow*/ + (string)/*Error near IL_00fc: Stack underflow*/;
				((VEAnswer)/*Error near IL_0149: Stack underflow*/).QUESTION_NAME = qUESTION_NAME;
				vEAnswer2.CODE = oQuestion.FillText;
				list.Add(vEAnswer2);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
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

		private void _003F58_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
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
				((MultipleEntry)/*Error near IL_002b: Stack underflow*/).btnNav.Foreground = Brushes.Black;
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

		private int _003F96_003F(string _003F362_003F)
		{
			if (_003F362_003F == _003F487_003F._003F488_003F(""))
			{
				return 0;
			}
			goto IL_0015;
			IL_0059:
			goto IL_0015;
			IL_0015:
			if (_003F362_003F == _003F487_003F._003F488_003F("1"))
			{
				return 0;
			}
			goto IL_002a;
			IL_0065:
			goto IL_002a;
			IL_002a:
			if (_003F362_003F == _003F487_003F._003F488_003F("/ı"))
			{
				return 0;
			}
			goto IL_003f;
			IL_0071:
			goto IL_003f;
			IL_003f:
			if (!_003F97_003F(_003F362_003F))
			{
				return 0;
			}
			goto IL_004b;
			IL_007d:
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
			goto IL_001b;
			IL_001b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0003Ŭə\u035aёԉ٥\u0744ࡔ\u094aਙ\u0b42\u0c4f൲\u0e6e\u0f72\u1072ᅾቴ፭ᐷᕡᙿᝰᡣ\u193c\u1a7f᭤\u1c7cᵻṧώ\u2060Ⅾ≯⍧⑼╵♿✫⡼⥢⩯⭭"), UriKind.Relative);
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
				((MultipleEntry)_003F350_003F).Loaded += _003F80_003F;
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
				txtSearchTitle = (TextBlock)_003F350_003F;
				break;
			case 6:
				txtSearch = (TextBox)_003F350_003F;
				txtSearch.PreviewKeyDown += _003F115_003F;
				txtSearch.TextChanged += _003F116_003F;
				break;
			case 7:
				btnSelect = (Button)_003F350_003F;
				btnSelect.Click += _003F117_003F;
				break;
			case 8:
				ListOption = (ListBox)_003F350_003F;
				ListOption.MouseDoubleClick += _003F118_003F;
				ListOption.SelectionChanged += _003F114_003F;
				break;
			case 9:
				txtSelectTitle = (TextBlock)_003F350_003F;
				break;
			case 10:
				DelMsg = (TextBlock)_003F350_003F;
				break;
			case 11:
				scrollmain = (ScrollViewer)_003F350_003F;
				break;
			case 12:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 13:
				stackPanel1 = (StackPanel)_003F350_003F;
				break;
			case 14:
				txtFillTitle = (TextBlock)_003F350_003F;
				break;
			case 15:
				txtFill = (TextBox)_003F350_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				txtFill.PreviewKeyDown += _003F115_003F;
				break;
			case 16:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 17:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 18:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 19:
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
