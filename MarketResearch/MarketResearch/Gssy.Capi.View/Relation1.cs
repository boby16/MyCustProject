using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class Relation1 : Page, IComponentConnector
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

		private bool ExistTextFill;

		private bool PageLoaded;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal Grid gridContent;

		internal TextBlock textBlock1;

		internal ComboBox cmbSelect1;

		internal TextBlock textBlock2;

		internal ComboBox cmbSelect2;

		internal Button btnNone;

		internal StackPanel stackPanel1;

		internal TextBlock txtFillTitle;

		internal TextBox txtFill;

		internal TextBlock txtAfter;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public Relation1()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_04c6: Incompatible stack heights: 0 vs 1
			//IL_04cd: Incompatible stack heights: 0 vs 1
			//IL_06c7: Incompatible stack heights: 0 vs 2
			//IL_06de: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.InitRelation(CurPageId, 0);
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
			qUESTION_TITLE = (string)/*Error near IL_04ce: Stack underflow*/;
			oBoldTitle.SetTextBlock(txtCircleTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
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
				string[] array = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list3 = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					foreach (SurveyDetail qDetail in oQuestion.QDetails)
					{
						if (qDetail.CODE == array[i].ToString())
						{
							list3.Add(qDetail);
							break;
						}
					}
				}
				if (_003F7_003F._003C_003E9__18_0 == null)
				{
					_003F7_003F._003C_003E9__18_0 = _003F7_003F._003C_003E9._003F325_003F;
				}
				((List<SurveyDetail>)/*Error near IL_06e3: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_06e3: Stack underflow*/);
				oQuestion.QDetails = list3;
			}
			if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int j = 0; j < oQuestion.QDetails.Count(); j++)
				{
					oQuestion.QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[j].CODE_TEXT);
				}
			}
			if (oQuestion.QDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomDetails();
			}
			Button_Height = SurveyHelper.BtnHeight;
			Button_FontSize = SurveyHelper.BtnFontSize;
			Button_Width = SurveyHelper.BtnWidth;
			Button_Type = oQuestion.QDefine.CONTROL_TYPE;
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
			textBlock1.Text = _003F487_003F._003F488_003F("");
			textBlock2.Text = _003F487_003F._003F488_003F("");
			cmbSelect1.ItemsSource = oQuestion.QDetailsParent;
			cmbSelect1.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			cmbSelect1.IsEditable = false;
			cmbSelect2.IsEditable = false;
			btnNone.Content = oQuestion.NoneCodeText;
			btnNone.Tag = oQuestion.NoneCode;
			if (oQuestion.OtherCode != _003F487_003F._003F488_003F(""))
			{
				txtFill.Visibility = Visibility.Visible;
				if (oQuestion.QDefine.NOTE == _003F487_003F._003F488_003F(""))
				{
					txtFillTitle.Visibility = Visibility.Visible;
				}
				else
				{
					qUESTION_TITLE = oQuestion.QDefine.NOTE;
					list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
					qUESTION_TITLE = list2[0];
					oBoldTitle.SetTextBlock(txtFillTitle, qUESTION_TITLE, 0, _003F487_003F._003F488_003F(""), true);
					if (list2.Count > 1)
					{
						qUESTION_TITLE = list2[1];
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
			if (oQuestion.QDefine.CONTROL_MASK == _003F487_003F._003F488_003F("0"))
			{
				btnNone.Visibility = Visibility.Collapsed;
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				cmbSelect1.SelectedValue = autoFill.SingleDetail(oQuestion.QDefine, oQuestion.QDetailsParent).CODE;
				_003F148_003F(cmbSelect1, null);
				cmbSelect2.SelectedValue = autoFill.SingleDetail(oQuestion.QDefine, oQuestion.QGroupDetails).CODE;
				_003F149_003F(cmbSelect2, null);
				if (txtFill.IsEnabled)
				{
					txtFill.Text = autoFill.CommonOther(oQuestion.QDefine, _003F487_003F._003F488_003F(""));
				}
				if (autoFill.AutoNext(oQuestion.QDefine))
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			Style style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			Style style2 = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			string navOperation = SurveyHelper.NavOperation;
			if (!(navOperation == _003F487_003F._003F488_003F("FŢɡ\u036a")))
			{
				if (!(navOperation == _003F487_003F._003F488_003F("HŪɶ\u036eѣխ")) && navOperation == _003F487_003F._003F488_003F("NŶɯͱ"))
				{
				}
			}
			else
			{
				oQuestion.SelectedCode = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName);
				cmbSelect1.SelectedValue = oQuestion.GetParentCode(oQuestion.SelectedCode);
				oQuestion.GetRelation2(oQuestion.ParentCode);
				cmbSelect2.ItemsSource = oQuestion.QGroupDetails;
				cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
				cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
				cmbSelect2.SelectedValue = oQuestion.SelectedCode;
				txtFill.Text = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				if (false)
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
			PageLoaded = true;
		}

		private bool _003F87_003F()
		{
			//IL_00b1: Incompatible stack heights: 0 vs 1
			//IL_00cb: Incompatible stack heights: 0 vs 1
			//IL_00e0: Incompatible stack heights: 0 vs 3
			//IL_00f1: Incompatible stack heights: 0 vs 2
			//IL_0105: Incompatible stack heights: 0 vs 1
			//IL_010a: Incompatible stack heights: 1 vs 0
			if (oQuestion.SelectedCode == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return (byte)/*Error near IL_0020: Stack underflow*/ != 0;
			}
			if (txtFill.IsEnabled)
			{
				txtFill.Text.Trim();
				string b = _003F487_003F._003F488_003F("");
				if ((string)/*Error near IL_003f: Stack underflow*/ == b)
				{
					string msgNotFillOther = SurveyMsg.MsgNotFillOther;
					string msgCaption = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_004b: Stack underflow*/, (string)/*Error near IL_004b: Stack underflow*/, (MessageBoxButton)/*Error near IL_004b: Stack underflow*/, MessageBoxImage.Hand);
					txtFill.Focus();
					return true;
				}
			}
			if (txtFill.IsEnabled)
			{
				QSingle oQuestion2 = oQuestion;
				string fillText;
				if (((Relation1)/*Error near IL_006f: Stack underflow*/).txtFill.IsEnabled)
				{
					fillText = txtFill.Text.Trim();
				}
				else
				{
					_003F487_003F._003F488_003F("");
				}
				((QSingle)/*Error near IL_010f: Stack underflow*/).FillText = fillText;
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			//IL_00ba: Incompatible stack heights: 0 vs 3
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQuestion.QuestionName;
			vEAnswer.CODE = oQuestion.SelectedCode;
			list.Add(vEAnswer);
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.SelectedCode;
			if (oQuestion.FillText != _003F487_003F._003F488_003F(""))
			{
				VEAnswer vEAnswer2 = new VEAnswer();
				string questionName = oQuestion.QuestionName;
				string qUESTION_NAME = string.Concat(str1: _003F487_003F._003F488_003F((string)/*Error near IL_0083: Stack underflow*/), str0: (string)/*Error near IL_0088: Stack underflow*/);
				((VEAnswer)/*Error near IL_008d: Stack underflow*/).QUESTION_NAME = qUESTION_NAME;
				vEAnswer2.CODE = oQuestion.FillText;
				list.Add(vEAnswer2);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F(".ġ") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			}
			return list;
		}

		private void _003F148_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			//IL_004b: Incompatible stack heights: 0 vs 1
			//IL_005b: Incompatible stack heights: 0 vs 2
			if (cmbSelect1.SelectedValue != null)
			{
				string _003F488_003F = cmbSelect1.SelectedValue.ToString();
				if ((int)/*Error near IL_0050: Stack underflow*/ != 0)
				{
					string b = _003F487_003F._003F488_003F((string)/*Error near IL_001a: Stack underflow*/);
					if ((string)/*Error near IL_001f: Stack underflow*/ != b)
					{
						oQuestion.GetRelation2(_003F488_003F);
						cmbSelect2.ItemsSource = oQuestion.QGroupDetails;
						cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
						cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
						oQuestion.SelectedCode = _003F487_003F._003F488_003F("");
					}
				}
			}
		}

		private void _003F149_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			_003F160_003F();
		}

		private void _003F160_003F()
		{
			//IL_009a: Incompatible stack heights: 0 vs 1
			//IL_00c9: Incompatible stack heights: 0 vs 1
			if (cmbSelect2.SelectedValue != null)
			{
				string text = cmbSelect2.SelectedValue.ToString();
				QSingle oQuestion2 = oQuestion;
				((QSingle)/*Error near IL_0016: Stack underflow*/).SelectedCode = text;
				if (text != oQuestion.OtherCode)
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

		private void _003F161_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0064: Incompatible stack heights: 0 vs 1
			if (oQuestion.SelectedCode == oQuestion.NoneCode)
			{
				oQuestion.SelectedCode = _003F487_003F._003F488_003F("");
				Button btnNone2 = btnNone;
				Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
				((FrameworkElement)/*Error near IL_003a: Stack underflow*/).Style = style;
				cmbSelect1.IsEnabled = true;
				cmbSelect2.IsEnabled = true;
				_003F160_003F();
			}
			else
			{
				oQuestion.SelectedCode = oQuestion.NoneCode;
				btnNone.Style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
				cmbSelect1.IsEnabled = false;
				cmbSelect2.IsEnabled = false;
				txtFill.Background = Brushes.LightGray;
				txtFill.IsEnabled = false;
			}
		}

		private void _003F89_003F()
		{
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, true);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00cd: Incompatible stack heights: 0 vs 2
			//IL_00e2: Incompatible stack heights: 0 vs 2
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
					_003F89_003F();
					if (SurveyHelper.Debug)
					{
						SurveyHelper.ShowPageAnswer(list);
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_00ea: Stack underflow*/, (string)/*Error near IL_00ea: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					oPageNav.NextPage(MyNav, base.NavigationService);
					btnNav.Content = btnNav_Content;
				}
			}
			return;
			IL_0097:
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\aŠɕ\u0356ѝԍ١\u0740ࡐॶਥ\u0b7e\u0c73\u0d76\u0e6a\u0f76ၶᅲቸ፡ᐻᕥᙻ\u1774ᡧ\u1920\u1a7c᭨ᱠ\u1d6aṾὠ\u2067Ⅹ∷⌫⑼╢♯❭"), UriKind.Relative);
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
				((Relation1)_003F350_003F).Loaded += _003F80_003F;
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
				textBlock1 = (TextBlock)_003F350_003F;
				break;
			case 6:
				cmbSelect1 = (ComboBox)_003F350_003F;
				cmbSelect1.SelectionChanged += _003F148_003F;
				break;
			case 7:
				textBlock2 = (TextBlock)_003F350_003F;
				break;
			case 8:
				cmbSelect2 = (ComboBox)_003F350_003F;
				cmbSelect2.SelectionChanged += _003F149_003F;
				break;
			case 9:
				btnNone = (Button)_003F350_003F;
				btnNone.Click += _003F161_003F;
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
			return;
			IL_004d:
			goto IL_0057;
		}
	}
}
