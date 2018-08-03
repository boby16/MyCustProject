using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using Gssy.Capi.Entities.BaiduJson;
using Gssy.Capi.QEdit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class FillMap : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QFill oQuestion = new QFill();

		private int PageLoaded;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		private DispatcherTimer timerSearch = new DispatcherTimer();

		private int SecondsCountDownSearch;

		private string strCity = _003F487_003F._003F488_003F("");

		private string lng = _003F487_003F._003F488_003F("");

		private string lat = _003F487_003F._003F488_003F("");

		private bool SearchClick;

		internal RowDefinition RowNote;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal StackPanel stk1;

		internal TextBlock txtBefore;

		internal System.Windows.Controls.TextBox txtFill;

		internal TextBlock txtAfter;

		internal System.Windows.Controls.Button btnSearch;

		internal StackPanel PanelConnet;

		internal ScrollViewer scrollNote;

		internal WindowsFormsHost c_wfh;

		internal System.Windows.Forms.WebBrowser c_webBrowser;

		internal TextBlock txtSurvey;

		internal System.Windows.Controls.Button btnAttach;

		internal System.Windows.Controls.Button btnNav;

		private bool _contentLoaded;

		public FillMap()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0811: Incompatible stack heights: 0 vs 2
			//IL_0821: Incompatible stack heights: 0 vs 1
			//IL_0857: Incompatible stack heights: 0 vs 1
			//IL_0871: Incompatible stack heights: 0 vs 1
			//IL_0876: Incompatible stack heights: 1 vs 0
			//IL_088c: Incompatible stack heights: 0 vs 2
			//IL_0897: Incompatible stack heights: 0 vs 1
			//IL_08a7: Incompatible stack heights: 0 vs 1
			//IL_08bd: Incompatible stack heights: 0 vs 2
			//IL_08e4: Incompatible stack heights: 0 vs 3
			//IL_08f0: Incompatible stack heights: 0 vs 2
			//IL_0901: Incompatible stack heights: 0 vs 2
			//IL_0916: Incompatible stack heights: 0 vs 1
			//IL_0927: Incompatible stack heights: 0 vs 2
			//IL_093a: Incompatible stack heights: 0 vs 1
			//IL_0952: Incompatible stack heights: 0 vs 3
			//IL_0969: Incompatible stack heights: 0 vs 1
			//IL_0984: Incompatible stack heights: 0 vs 1
			//IL_099a: Incompatible stack heights: 0 vs 2
			//IL_09c8: Incompatible stack heights: 0 vs 2
			//IL_09d7: Incompatible stack heights: 0 vs 1
			//IL_09f8: Incompatible stack heights: 0 vs 1
			//IL_0a09: Incompatible stack heights: 0 vs 2
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.Init(CurPageId, 0);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				NavBase myNav = MyNav;
				int gROUP_PAGE_TYPE = ((FillMap)/*Error near IL_0093: Stack underflow*/).oQuestion.QDefine.GROUP_PAGE_TYPE;
				((NavBase)/*Error near IL_00a2: Stack underflow*/).GroupPageType = gROUP_PAGE_TYPE;
				MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
				MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
				MyNav.CircleACount = SurveyHelper.CircleACount;
				if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
				{
					NavBase myNav2 = MyNav;
					string gROUP_CODEB = oQuestion.QDefine.GROUP_CODEB;
					((NavBase)/*Error near IL_0111: Stack underflow*/).GroupCodeB = gROUP_CODEB;
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
				LogicEngine oLogicEngine2 = oLogicEngine;
				string circleACodeText = SurveyHelper.CircleACodeText;
				((LogicEngine)/*Error near IL_0378: Stack underflow*/).CircleACodeText = circleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			List<string> list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
			qUESTION_TITLE = list2[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, qUESTION_TITLE, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			string text;
			if (list2.Count > 1)
			{
				text = list2[1];
			}
			else
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			qUESTION_TITLE = text;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
			if (oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				System.Windows.Controls.TextBox txtFill2 = txtFill;
				QFill oQuestion2 = oQuestion;
				int cONTROL_TYPE = ((QFill)/*Error near IL_0487: Stack underflow*/).QDefine.CONTROL_TYPE;
				((System.Windows.Controls.TextBox)/*Error near IL_0491: Stack underflow*/).MaxLength = cONTROL_TYPE;
			}
			if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
			{
				((FillMap)/*Error near IL_04ab: Stack underflow*/).txtFill.Height = (double)oQuestion.QDefine.CONTROL_HEIGHT;
			}
			if (oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				System.Windows.Controls.TextBox txtFill3 = txtFill;
				double width = (double)oQuestion.QDefine.CONTROL_WIDTH;
				((FrameworkElement)/*Error near IL_04ec: Stack underflow*/).Width = width;
			}
			if (oQuestion.QDefine.CONTROL_FONTSIZE > 0)
			{
				System.Windows.Controls.TextBox txtFill4 = txtFill;
				QFill oQuestion3 = oQuestion;
				double fontSize = (double)((QFill)/*Error near IL_0507: Stack underflow*/).QDefine.CONTROL_FONTSIZE;
				((System.Windows.Controls.Control)/*Error near IL_0512: Stack underflow*/).FontSize = fontSize;
			}
			if (oQuestion.QDefine.CONTROL_TOOLTIP.Trim() != _003F487_003F._003F488_003F(""))
			{
				qUESTION_TITLE = oQuestion.QDefine.CONTROL_TOOLTIP;
				BoldTitle oBoldTitle2 = oBoldTitle;
				string _003F460_003F = _003F487_003F._003F488_003F((string)/*Error near IL_0540: Stack underflow*/);
				list2 = ((BoldTitle)/*Error near IL_0545: Stack underflow*/).ParaToList((string)/*Error near IL_0545: Stack underflow*/, _003F460_003F);
				qUESTION_TITLE = list2[0];
				oBoldTitle.SetTextBlock(txtBefore, qUESTION_TITLE, oQuestion.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				if (list2.Count > 1)
				{
					qUESTION_TITLE = ((List<string>)/*Error near IL_058c: Stack underflow*/)[(int)/*Error near IL_058c: Stack underflow*/];
					oBoldTitle.SetTextBlock(txtAfter, qUESTION_TITLE, oQuestion.QDefine.CONTROL_FONTSIZE, _003F487_003F._003F488_003F(""), true);
				}
			}
			if (oQuestion.QDefine.PRESET_LOGIC != _003F487_003F._003F488_003F(""))
			{
				System.Windows.Controls.TextBox txtFill5 = txtFill;
				string text2 = ((FillMap)/*Error near IL_05e3: Stack underflow*/).oLogicEngine.stringResult(oQuestion.QDefine.PRESET_LOGIC);
				((System.Windows.Controls.TextBox)/*Error near IL_05fd: Stack underflow*/).Text = text2;
				txtFill.SelectAll();
			}
			txtFill.Focus();
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ"))
			{
				SurveyDefine qDefine2 = oQuestion.QDefine;
				if (((SurveyDefine)/*Error near IL_0632: Stack underflow*/).IS_ATTACH == 1)
				{
					System.Windows.Controls.Button btnAttach2 = btnAttach;
					((UIElement)/*Error near IL_063d: Stack underflow*/).Visibility = (Visibility)/*Error near IL_063d: Stack underflow*/;
				}
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				LogicEngine logicEngine = oLogicEngine;
				((AutoFill)/*Error near IL_0652: Stack underflow*/).oLogicEngine = logicEngine;
				if (txtFill.Text == _003F487_003F._003F488_003F(""))
				{
					System.Windows.Controls.TextBox txtFill6 = txtFill;
					QFill oQuestion4 = oQuestion;
					SurveyDefine qDefine = ((QFill)/*Error near IL_0676: Stack underflow*/).QDefine;
					string text3 = ((AutoFill)/*Error near IL_067b: Stack underflow*/).Fill(qDefine);
					((System.Windows.Controls.TextBox)/*Error near IL_0680: Stack underflow*/).Text = text3;
				}
				if (autoFill.AutoNext(oQuestion.QDefine))
				{
					SecondsCountDown = 2;
					DispatcherTimer timer2 = timer;
					TimeSpan interval = TimeSpan.FromMilliseconds(1000.0);
					((DispatcherTimer)/*Error near IL_06aa: Stack underflow*/).Interval = interval;
					timer.Tick += _003F107_003F;
					timer.Start();
				}
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			string navOperation = SurveyHelper.NavOperation;
			if (navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				txtFill.Text = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName);
				lng = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName + _003F487_003F._003F488_003F("Xŋɤ\u0374яլ٦"));
				lat = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName + _003F487_003F._003F488_003F("Xŋɤ\u0374яգٵ"));
			}
			else
			{
				bool flag = navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
				if ((int)/*Error near IL_0989: Stack underflow*/ != 0)
				{
					if (oQuestion.QDefine.EXTEND_1.Contains(SurveyHelper.Only1CodeMode3))
					{
						string text4 = txtFill.Text;
						string b = _003F487_003F._003F488_003F((string)/*Error near IL_07c5: Stack underflow*/);
						if ((string)/*Error near IL_07ca: Stack underflow*/ != b)
						{
							bool autoFill2 = SurveyHelper.AutoFill;
							if ((int)/*Error near IL_09dc: Stack underflow*/ == 0)
							{
								_003F58_003F(this, _003F348_003F);
							}
						}
					}
				}
				else
				{
					_003F487_003F._003F488_003F("NŶɯͱ");
					if ((string)/*Error near IL_0709: Stack underflow*/ == (string)/*Error near IL_0709: Stack underflow*/)
					{
					}
				}
			}
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				bool autoFill3 = SurveyHelper.AutoFill;
				if ((int)/*Error near IL_09fd: Stack underflow*/ == 0)
				{
					int secondsWait = SecondsWait;
					((FillMap)/*Error near IL_0a0e: Stack underflow*/).SecondsCountDown = (int)/*Error near IL_0a0e: Stack underflow*/;
					btnNav.Foreground = Brushes.Gray;
					btnNav.Content = SecondsCountDown.ToString();
					timer.Interval = TimeSpan.FromMilliseconds(1000.0);
					timer.Tick += _003F84_003F;
					timer.Start();
				}
			}
			PageLoaded = 1;
		}

		private void _003F99_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_00f6: Incompatible stack heights: 0 vs 1
			//IL_0106: Incompatible stack heights: 0 vs 2
			//IL_0116: Incompatible stack heights: 0 vs 1
			//IL_0132: Incompatible stack heights: 0 vs 4
			if (PageLoaded == 2)
			{
				string text2 = txtFill.Text;
				string b = _003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_001b: Stack underflow*/ != b)
				{
					((FillMap)/*Error near IL_0025: Stack underflow*/).SecondsCountDownSearch = (int)/*Error near IL_0025: Stack underflow*/;
					timerSearch.Interval = TimeSpan.FromMilliseconds(100.0);
					timerSearch.Tick += _003F105_003F;
					timerSearch.Start();
				}
				PageLoaded++;
				new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			}
			if (PageLoaded == 1)
			{
				QFill oQuestion2 = oQuestion;
				if (((QFill)/*Error near IL_0094: Stack underflow*/).QDefine.DETAIL_ID == _003F487_003F._003F488_003F(""))
				{
					LogicEngine oLogicEngine2 = oLogicEngine;
					_003F487_003F._003F488_003F("!Ś");
					string _003F375_003F = string.Concat(str1: ((FillMap)/*Error near IL_00b2: Stack underflow*/).oQuestion.QDefine.PARENT_CODE, str2: _003F487_003F._003F488_003F("\\"), str0: (string)/*Error near IL_00cb: Stack underflow*/);
					string text = ((LogicEngine)/*Error near IL_00d0: Stack underflow*/).stringResult(_003F375_003F);
					((FillMap)/*Error near IL_00d5: Stack underflow*/).strCity = text;
				}
				else
				{
					strCity = oLogicEngine.stringResult(_003F487_003F._003F488_003F(".Ŋɇ\u0343уՑفݛࡖऩ") + oQuestion.QDefine.DETAIL_ID + _003F487_003F._003F488_003F(";") + oQuestion.QDefine.PARENT_CODE + _003F487_003F._003F488_003F("("));
				}
				_003F102_003F((int)scrollNote.ActualWidth, (int)RowNote.ActualHeight);
				PageLoaded++;
			}
		}

		private void _003F86_003F(object _003F347_003F, System.Windows.Input.KeyEventArgs _003F348_003F)
		{
			//IL_0023: Incompatible stack heights: 0 vs 3
			if (_003F348_003F.Key == Key.Return)
			{
				((FillMap)/*Error near IL_0011: Stack underflow*/)._003F104_003F((object)/*Error near IL_0011: Stack underflow*/, (RoutedEventArgs)/*Error near IL_0011: Stack underflow*/);
			}
		}

		private bool _003F87_003F()
		{
			//IL_00ff: Incompatible stack heights: 0 vs 2
			//IL_0113: Incompatible stack heights: 0 vs 2
			//IL_0127: Incompatible stack heights: 0 vs 2
			//IL_0146: Incompatible stack heights: 0 vs 1
			string text = txtFill.Text.Trim();
			if (txtFill.IsEnabled)
			{
				_003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_0026: Stack underflow*/ == (string)/*Error near IL_0026: Stack underflow*/)
				{
					string msgNotFill = SurveyMsg.MsgNotFill;
					string msgCaption = SurveyMsg.MsgCaption;
					System.Windows.MessageBox.Show((string)/*Error near IL_0033: Stack underflow*/, (string)/*Error near IL_0033: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
				text = oQuestion.ConvertText(text, oQuestion.QDefine.CONTROL_MASK);
				txtFill.Text = text;
			}
			if (!SearchClick)
			{
				string msgNotSearch = SurveyMsg.MsgNotSearch;
				string msgCaption2 = SurveyMsg.MsgCaption;
				System.Windows.MessageBox.Show((string)/*Error near IL_007e: Stack underflow*/, (string)/*Error near IL_007e: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
				txtFill.Focus();
				return true;
			}
			_003F103_003F();
			if (!(lng == _003F487_003F._003F488_003F("")))
			{
				bool flag = lat == _003F487_003F._003F488_003F("");
				if ((int)/*Error near IL_014b: Stack underflow*/ == 0)
				{
					goto IL_00e4;
				}
			}
			if (System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgNoLngLat, text), SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
			{
				txtFill.Focus();
				return true;
			}
			goto IL_00e4;
			IL_0163:
			goto IL_00e4;
			IL_00e4:
			oQuestion.FillText = text;
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> result = new List<VEAnswer>
			{
				new VEAnswer
				{
					QUESTION_NAME = oQuestion.QuestionName,
					CODE = oQuestion.FillText
				}
			};
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			return result;
		}

		private void _003F89_003F()
		{
			string text = _003F100_003F();
			if (text == _003F487_003F._003F488_003F(""))
			{
				text = SurveyMsg.MsgCaptureFail;
			}
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, oQuestion.QuestionName + _003F487_003F._003F488_003F("Xŋɤ\u0374ѓի٢"), text, DateTime.Now);
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, oQuestion.QuestionName + _003F487_003F._003F488_003F("Xŋɤ\u0374яլ٦"), lng, DateTime.Now);
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, oQuestion.QuestionName + _003F487_003F._003F488_003F("Xŋɤ\u0374яգٵ"), lat, DateTime.Now);
		}

		private void _003F58_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00bf: Incompatible stack heights: 0 vs 2
			//IL_00cf: Incompatible stack heights: 0 vs 1
			//IL_00ec: Incompatible stack heights: 0 vs 1
			if ((string)btnNav.Content != btnNav_Content)
			{
				return;
			}
			goto IL_0020;
			IL_0020:
			btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
			if (_003F87_003F())
			{
				System.Windows.Controls.Button btnNav2 = btnNav;
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
					System.Windows.Controls.Button btnNav3 = btnNav;
					string content = btnNav_Content;
					((ContentControl)/*Error near IL_0086: Stack underflow*/).Content = content;
				}
				else
				{
					_003F89_003F();
					if (SurveyHelper.Debug)
					{
						System.Windows.MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_00a4:
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
			//IL_006f: Incompatible stack heights: 0 vs 1
			//IL_0074: Incompatible stack heights: 1 vs 0
			//IL_0079: Incompatible stack heights: 0 vs 2
			//IL_007f: Incompatible stack heights: 0 vs 1
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

		public string CleanString(string _003F367_003F)
		{
			return _003F367_003F.Replace(_003F487_003F._003F488_003F("、"), _003F487_003F._003F488_003F("!")).Trim().Replace('\n', ' ')
				.Replace('\r', ' ')
				.Replace('\t', ' ')
				.Replace(_003F487_003F._003F488_003F("Ａ"), _003F487_003F._003F488_003F("A"))
				.Replace(_003F487_003F._003F488_003F("１"), _003F487_003F._003F488_003F("1"))
				.Replace(_003F487_003F._003F488_003F("０"), _003F487_003F._003F488_003F("0"))
				.Replace(_003F487_003F._003F488_003F("３"), _003F487_003F._003F488_003F("3"))
				.Replace(_003F487_003F._003F488_003F("２"), _003F487_003F._003F488_003F("2"))
				.Replace(_003F487_003F._003F488_003F("５"), _003F487_003F._003F488_003F("5"))
				.Replace(_003F487_003F._003F488_003F("４"), _003F487_003F._003F488_003F("4"))
				.Replace(_003F487_003F._003F488_003F("７"), _003F487_003F._003F488_003F("7"))
				.Replace(_003F487_003F._003F488_003F("６"), _003F487_003F._003F488_003F("6"))
				.Replace(_003F487_003F._003F488_003F("９"), _003F487_003F._003F488_003F("9"))
				.Replace(_003F487_003F._003F488_003F("８"), _003F487_003F._003F488_003F("8"))
				.Replace(_003F487_003F._003F488_003F("＠"), _003F487_003F._003F488_003F("@"))
				.Replace(_003F487_003F._003F488_003F("Ｃ"), _003F487_003F._003F488_003F("C"))
				.Replace(_003F487_003F._003F488_003F("Ｂ"), _003F487_003F._003F488_003F("B"))
				.Replace(_003F487_003F._003F488_003F("Ｅ"), _003F487_003F._003F488_003F("E"))
				.Replace(_003F487_003F._003F488_003F("Ｄ"), _003F487_003F._003F488_003F("D"))
				.Replace(_003F487_003F._003F488_003F("Ｇ"), _003F487_003F._003F488_003F("G"))
				.Replace(_003F487_003F._003F488_003F("Ｆ"), _003F487_003F._003F488_003F("F"))
				.Replace(_003F487_003F._003F488_003F("Ｉ"), _003F487_003F._003F488_003F("I"))
				.Replace(_003F487_003F._003F488_003F("Ｈ"), _003F487_003F._003F488_003F("H"))
				.Replace(_003F487_003F._003F488_003F("Ｋ"), _003F487_003F._003F488_003F("K"))
				.Replace(_003F487_003F._003F488_003F("Ｊ"), _003F487_003F._003F488_003F("J"))
				.Replace(_003F487_003F._003F488_003F("Ｍ"), _003F487_003F._003F488_003F("M"))
				.Replace(_003F487_003F._003F488_003F("Ｌ"), _003F487_003F._003F488_003F("L"))
				.Replace(_003F487_003F._003F488_003F("Ｏ"), _003F487_003F._003F488_003F("O"))
				.Replace(_003F487_003F._003F488_003F("Ｎ"), _003F487_003F._003F488_003F("N"))
				.Replace(_003F487_003F._003F488_003F("Ｑ"), _003F487_003F._003F488_003F("Q"))
				.Replace(_003F487_003F._003F488_003F("Ｐ"), _003F487_003F._003F488_003F("P"))
				.Replace(_003F487_003F._003F488_003F("Ｓ"), _003F487_003F._003F488_003F("S"))
				.Replace(_003F487_003F._003F488_003F("Ｒ"), _003F487_003F._003F488_003F("R"))
				.Replace(_003F487_003F._003F488_003F("Ｕ"), _003F487_003F._003F488_003F("U"))
				.Replace(_003F487_003F._003F488_003F("Ｔ"), _003F487_003F._003F488_003F("T"))
				.Replace(_003F487_003F._003F488_003F("Ｗ"), _003F487_003F._003F488_003F("W"))
				.Replace(_003F487_003F._003F488_003F("Ｖ"), _003F487_003F._003F488_003F("V"))
				.Replace(_003F487_003F._003F488_003F("Ｙ"), _003F487_003F._003F488_003F("Y"))
				.Replace(_003F487_003F._003F488_003F("Ｘ"), _003F487_003F._003F488_003F("X"))
				.Replace(_003F487_003F._003F488_003F("［"), _003F487_003F._003F488_003F("["))
				.Replace(_003F487_003F._003F488_003F("\uff40"), _003F487_003F._003F488_003F("`"))
				.Replace(_003F487_003F._003F488_003F("ｃ"), _003F487_003F._003F488_003F("c"))
				.Replace(_003F487_003F._003F488_003F("ｂ"), _003F487_003F._003F488_003F("b"))
				.Replace(_003F487_003F._003F488_003F("ｅ"), _003F487_003F._003F488_003F("e"))
				.Replace(_003F487_003F._003F488_003F("ｄ"), _003F487_003F._003F488_003F("d"))
				.Replace(_003F487_003F._003F488_003F("ｇ"), _003F487_003F._003F488_003F("g"))
				.Replace(_003F487_003F._003F488_003F("ｆ"), _003F487_003F._003F488_003F("f"))
				.Replace(_003F487_003F._003F488_003F("ｉ"), _003F487_003F._003F488_003F("i"))
				.Replace(_003F487_003F._003F488_003F("ｈ"), _003F487_003F._003F488_003F("h"))
				.Replace(_003F487_003F._003F488_003F("ｋ"), _003F487_003F._003F488_003F("k"))
				.Replace(_003F487_003F._003F488_003F("ｊ"), _003F487_003F._003F488_003F("j"))
				.Replace(_003F487_003F._003F488_003F("ｍ"), _003F487_003F._003F488_003F("m"))
				.Replace(_003F487_003F._003F488_003F("ｌ"), _003F487_003F._003F488_003F("l"))
				.Replace(_003F487_003F._003F488_003F("ｏ"), _003F487_003F._003F488_003F("o"))
				.Replace(_003F487_003F._003F488_003F("ｎ"), _003F487_003F._003F488_003F("n"))
				.Replace(_003F487_003F._003F488_003F("ｑ"), _003F487_003F._003F488_003F("q"))
				.Replace(_003F487_003F._003F488_003F("ｐ"), _003F487_003F._003F488_003F("p"))
				.Replace(_003F487_003F._003F488_003F("ｓ"), _003F487_003F._003F488_003F("s"))
				.Replace(_003F487_003F._003F488_003F("ｒ"), _003F487_003F._003F488_003F("r"))
				.Replace(_003F487_003F._003F488_003F("ｕ"), _003F487_003F._003F488_003F("u"))
				.Replace(_003F487_003F._003F488_003F("ｔ"), _003F487_003F._003F488_003F("t"))
				.Replace(_003F487_003F._003F488_003F("ｗ"), _003F487_003F._003F488_003F("w"))
				.Replace(_003F487_003F._003F488_003F("ｖ"), _003F487_003F._003F488_003F("v"))
				.Replace(_003F487_003F._003F488_003F("ｙ"), _003F487_003F._003F488_003F("y"))
				.Replace(_003F487_003F._003F488_003F("ｘ"), _003F487_003F._003F488_003F("x"))
				.Replace(_003F487_003F._003F488_003F("｛"), _003F487_003F._003F488_003F("{"));
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

		private string _003F100_003F()
		{
			//IL_0070: Incompatible stack heights: 0 vs 3
			string text = MySurveyId + _003F487_003F._003F488_003F("^") + CurPageId + _003F487_003F._003F488_003F("*ũɲ\u0366");
			string result = text;
			text = Directory.GetCurrentDirectory() + _003F487_003F._003F488_003F("[Ŗɭ\u036bѷխ\u065d") + text;
			if (!new ScreenCapture().Capture(text, (int)SurveyHelper.Screen_LeftTop))
			{
				string msgNotCapture = SurveyMsg.MsgNotCapture;
				string msgCaption = SurveyMsg.MsgCaption;
				System.Windows.MessageBox.Show((string)/*Error near IL_0075: Stack underflow*/, (string)/*Error near IL_0075: Stack underflow*/, (MessageBoxButton)/*Error near IL_0075: Stack underflow*/, MessageBoxImage.Hand);
				return _003F487_003F._003F488_003F("");
			}
			return result;
		}

		private void _003F101_003F(object _003F347_003F, CancelEventArgs _003F348_003F)
		{
			_003F348_003F.Cancel = true;
		}

		private void _003F102_003F(int _003F368_003F = 1280, int _003F369_003F = 600)
		{
			string urlString = Environment.CurrentDirectory + _003F487_003F._003F488_003F("8Œɴ\u0360ѲԽٸݾ\u087b\u0962ਢ\u0b4a\u0c62൦\u0e65ཅၦᅶራ፬ᑷᕯ᙭");
			c_webBrowser.Width = _003F368_003F;
			c_webBrowser.Height = _003F369_003F;
			c_webBrowser.ScriptErrorsSuppressed = true;
			c_webBrowser.Navigate(urlString);
		}

		private void _003F103_003F()
		{
			lng = c_webBrowser.Document.All[_003F487_003F._003F488_003F("hŠɳ\u036fѬզ")].GetAttribute(_003F487_003F._003F488_003F("sťɯͷѤ"));
			lat = c_webBrowser.Document.All[_003F487_003F._003F488_003F("hŠɳ\u036fѣյ")].GetAttribute(_003F487_003F._003F488_003F("sťɯͷѤ"));
		}

		private void _003F104_003F(object _003F347_003F = null, RoutedEventArgs _003F348_003F = null)
		{
			//IL_00eb: Incompatible stack heights: 0 vs 1
			//IL_00ff: Incompatible stack heights: 0 vs 2
			//IL_010f: Incompatible stack heights: 0 vs 1
			string text = _003F487_003F._003F488_003F("3Ĺ");
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				SurveyDefine qDefine = oQuestion.QDefine;
				text = ((SurveyDefine)/*Error near IL_0034: Stack underflow*/).NOTE;
			}
			txtFill.Text = oQuestion.ConvertText(txtFill.Text, oQuestion.QDefine.CONTROL_MASK);
			string text2 = txtFill.Text;
			if (text2.Trim() == _003F487_003F._003F488_003F(""))
			{
				string msgNotFill = SurveyMsg.MsgNotFill;
				string msgCaption = SurveyMsg.MsgCaption;
				System.Windows.MessageBox.Show((string)/*Error near IL_0094: Stack underflow*/, (string)/*Error near IL_0094: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
				txtFill.Focus();
			}
			else
			{
				SearchClick = true;
				JGeocoding geocodingFromAddress = new BaiduMapHelper().GetGeocodingFromAddress(strCity, strCity + text2);
				if (geocodingFromAddress.status == 0)
				{
					StackPanel panelConnet = PanelConnet;
					((UIElement)/*Error near IL_0115: Stack underflow*/).Visibility = Visibility.Collapsed;
					scrollNote.Visibility = Visibility.Visible;
					c_webBrowser.Document.InvokeScript(_003F487_003F._003F488_003F("|Ũɡ\u0364Ѽլ\u0657ݨ\u0870ॠ\u0a76୯\u0c63\u0d78"));
					c_webBrowser.Document.InvokeScript(_003F487_003F._003F488_003F("Můɮ\u0344ѧձ٣ݕ\u086b४੬୵"), new object[4]
					{
						geocodingFromAddress.result.location.lng,
						geocodingFromAddress.result.location.lat,
						text2,
						text
					});
				}
				else
				{
					PanelConnet.Visibility = Visibility.Visible;
					scrollNote.Visibility = Visibility.Collapsed;
					System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgMapNotFound, strCity + _003F487_003F._003F488_003F("；") + text2, geocodingFromAddress.status.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					txtFill.Focus();
				}
			}
		}

		private void _003F105_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0065: Incompatible stack heights: 0 vs 1
			if (SecondsCountDownSearch == 0)
			{
				timerSearch.Stop();
				if (!(lng == _003F487_003F._003F488_003F("")))
				{
					string lat2 = lat;
					string b = _003F487_003F._003F488_003F("");
					if (!((string)/*Error near IL_0034: Stack underflow*/ == b))
					{
						_003F106_003F();
						return;
					}
				}
				_003F104_003F(null, null);
			}
			else
			{
				SecondsCountDownSearch -= 100;
			}
		}

		private void _003F106_003F()
		{
			//IL_0091: Incompatible stack heights: 0 vs 1
			//IL_00a1: Incompatible stack heights: 0 vs 1
			string text = _003F487_003F._003F488_003F("3Ĺ");
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				SurveyDefine qDefine = oQuestion.QDefine;
				text = ((SurveyDefine)/*Error near IL_0034: Stack underflow*/).NOTE;
			}
			string text2 = txtFill.Text;
			SearchClick = true;
			JGeocoding geocodingFromAddress = new BaiduMapHelper().GetGeocodingFromAddress(strCity, strCity + text2);
			if (geocodingFromAddress.status == 0)
			{
				StackPanel panelConnet = PanelConnet;
				((UIElement)/*Error near IL_0076: Stack underflow*/).Visibility = Visibility.Collapsed;
				scrollNote.Visibility = Visibility.Visible;
				c_webBrowser.Document.InvokeScript(_003F487_003F._003F488_003F("|Ũɡ\u0364Ѽլ\u0657ݨ\u0870ॠ\u0a76୯\u0c63\u0d78"));
				c_webBrowser.Document.InvokeScript(_003F487_003F._003F488_003F("Můɮ\u0344ѧձ٣ݕ\u086b४੬୵"), new object[4]
				{
					lng,
					lat,
					text2,
					text
				});
			}
			else
			{
				PanelConnet.Visibility = Visibility.Visible;
				scrollNote.Visibility = Visibility.Collapsed;
				System.Windows.MessageBox.Show(string.Format(SurveyMsg.MsgMapNotFound, strCity + _003F487_003F._003F488_003F("；") + text2, geocodingFromAddress.status.ToString()), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				txtFill.Focus();
			}
		}

		private void _003F107_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			if (SecondsCountDown == 0)
			{
				timer.Stop();
				_003F58_003F(null, null);
			}
			else
			{
				SecondsCountDown--;
			}
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\tŢɗ\u0350ћԏ٣ݾ\u086eॴਧ\u0b78\u0c75൴\u0e68\u0f78ၸᅰቺ፧ᐽᕧᙹᝪ\u1879\u1922\u1a6a᭢ᱦ\u1d65ṥὦ⁶Å≼⍢⑯╭"), UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
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
				((FillMap)_003F350_003F).Loaded += _003F80_003F;
				((FillMap)_003F350_003F).LayoutUpdated += _003F99_003F;
				break;
			case 2:
				RowNote = (RowDefinition)_003F350_003F;
				break;
			case 3:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 5:
				stk1 = (StackPanel)_003F350_003F;
				break;
			case 6:
				txtBefore = (TextBlock)_003F350_003F;
				break;
			case 7:
				txtFill = (System.Windows.Controls.TextBox)_003F350_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				txtFill.PreviewKeyDown += _003F86_003F;
				break;
			case 8:
				txtAfter = (TextBlock)_003F350_003F;
				break;
			case 9:
				btnSearch = (System.Windows.Controls.Button)_003F350_003F;
				btnSearch.Click += _003F104_003F;
				break;
			case 10:
				PanelConnet = (StackPanel)_003F350_003F;
				break;
			case 11:
				scrollNote = (ScrollViewer)_003F350_003F;
				break;
			case 12:
				c_wfh = (WindowsFormsHost)_003F350_003F;
				break;
			case 13:
				c_webBrowser = (System.Windows.Forms.WebBrowser)_003F350_003F;
				c_webBrowser.NewWindow += _003F101_003F;
				break;
			case 14:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 15:
				btnAttach = (System.Windows.Controls.Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 16:
				btnNav = (System.Windows.Controls.Button)_003F350_003F;
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
