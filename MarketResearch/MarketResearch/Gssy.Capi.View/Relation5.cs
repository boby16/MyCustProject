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
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class Relation5 : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__22_0;

			internal int _003F318_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private BoldTitle oBoldTitle = new BoldTitle();

		private int nTotal = 5;

		private QSingle oQuestion = new QSingle();

		private List<QSingle> oQSingle = new List<QSingle>();

		private List<ComboBox> ListCmbParent = new List<ComboBox>();

		private List<ComboBox> ListCmbCode = new List<ComboBox>();

		private List<TextBox> ListTxtFill = new List<TextBox>();

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

		internal ScrollViewer scrollmain;

		internal Grid gridContent;

		internal TextBlock textBlock1_1;

		internal ComboBox cmbSelect1_1;

		internal TextBlock textBlock2_1;

		internal ComboBox cmbSelect2_1;

		internal TextBlock txtFillTitle1;

		internal TextBox txtFill1;

		internal TextBlock textBlock1_2;

		internal ComboBox cmbSelect1_2;

		internal TextBlock textBlock2_2;

		internal ComboBox cmbSelect2_2;

		internal TextBlock txtFillTitle2;

		internal TextBox txtFill2;

		internal TextBlock textBlock1_3;

		internal ComboBox cmbSelect1_3;

		internal TextBlock textBlock2_3;

		internal ComboBox cmbSelect2_3;

		internal TextBlock txtFillTitle3;

		internal TextBox txtFill3;

		internal TextBlock textBlock1_4;

		internal ComboBox cmbSelect1_4;

		internal TextBlock textBlock2_4;

		internal ComboBox cmbSelect2_4;

		internal TextBlock txtFillTitle4;

		internal TextBox txtFill4;

		internal TextBlock textBlock1_5;

		internal ComboBox cmbSelect1_5;

		internal TextBlock textBlock2_5;

		internal ComboBox cmbSelect2_5;

		internal TextBlock txtFillTitle5;

		internal TextBox txtFill5;

		internal StackPanel stackPanel1;

		internal Button btnNone;

		internal TextBlock txtSurvey;

		internal Button btnNav;

		private bool _contentLoaded;

		public Relation5()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_05a9: Incompatible stack heights: 0 vs 1
			//IL_05b0: Incompatible stack heights: 0 vs 1
			//IL_07aa: Incompatible stack heights: 0 vs 2
			//IL_07c1: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			btnNav.Content = btnNav_Content;
			oQuestion.InitRelation(CurPageId, 0);
			for (int i = 0; i < nTotal; i++)
			{
				oQSingle.Add(new QSingle());
				oQSingle[i].Init(CurPageId, i + 1, true);
				oQSingle[i].OtherCode = _003F487_003F._003F488_003F("");
			}
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
				for (int j = 0; j < nTotal; j++)
				{
					oQSingle[j].QuestionName = oQSingle[j].QuestionName + MyNav.QName_Add;
				}
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
			qUESTION_TITLE = (string)/*Error near IL_05b1: Stack underflow*/;
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
				for (int k = 0; k < array.Count(); k++)
				{
					foreach (SurveyDetail qDetail in oQuestion.QDetails)
					{
						if (qDetail.CODE == array[k].ToString())
						{
							list3.Add(qDetail);
							break;
						}
					}
				}
				if (_003F7_003F._003C_003E9__22_0 == null)
				{
					_003F7_003F._003C_003E9__22_0 = _003F7_003F._003C_003E9._003F318_003F;
				}
				((List<SurveyDetail>)/*Error near IL_07c6: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_07c6: Stack underflow*/);
				oQuestion.QDetails = list3;
			}
			if (oQuestion.QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int l = 0; l < oQuestion.QDetails.Count(); l++)
				{
					oQuestion.QDetails[l].CODE_TEXT = oBoldTitle.ReplaceABTitle(oQuestion.QDetails[l].CODE_TEXT);
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
			string str = _003F487_003F._003F488_003F("哃獍");
			string str2 = _003F487_003F._003F488_003F("轤嚊");
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F("") && oQuestion.QDefine.NOTE != null)
			{
				qUESTION_TITLE = oQuestion.QDefine.NOTE;
				list2 = oBoldTitle.ParaToList(qUESTION_TITLE, _003F487_003F._003F488_003F("-Į"));
				str = list2[0];
				if (list2.Count > 1)
				{
					str2 = list2[1];
				}
			}
			textBlock1_1.Text = str + _003F487_003F._003F488_003F("0");
			textBlock2_1.Text = str2 + _003F487_003F._003F488_003F("0");
			cmbSelect1_1.ItemsSource = oQuestion.QDetailsParent;
			cmbSelect1_1.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1_1.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			ListCmbParent.Add(cmbSelect1_1);
			ListCmbCode.Add(cmbSelect2_1);
			ListTxtFill.Add(txtFill1);
			ListCmbParent[0].Tag = 0;
			ListCmbCode[0].Tag = 0;
			textBlock1_2.Text = str + _003F487_003F._003F488_003F("3");
			textBlock2_2.Text = str2 + _003F487_003F._003F488_003F("3");
			cmbSelect1_2.ItemsSource = oQuestion.QDetailsParent;
			cmbSelect1_2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1_2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			ListCmbParent.Add(cmbSelect1_2);
			ListCmbCode.Add(cmbSelect2_2);
			ListTxtFill.Add(txtFill2);
			ListCmbParent[1].Tag = 1;
			ListCmbCode[1].Tag = 1;
			textBlock1_3.Text = str + _003F487_003F._003F488_003F("2");
			textBlock2_3.Text = str2 + _003F487_003F._003F488_003F("2");
			cmbSelect1_3.ItemsSource = oQuestion.QDetailsParent;
			cmbSelect1_3.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1_3.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			ListCmbParent.Add(cmbSelect1_3);
			ListCmbCode.Add(cmbSelect2_3);
			ListTxtFill.Add(txtFill3);
			ListCmbParent[2].Tag = 2;
			ListCmbCode[2].Tag = 2;
			textBlock1_4.Text = str + _003F487_003F._003F488_003F("5");
			textBlock2_4.Text = str2 + _003F487_003F._003F488_003F("5");
			cmbSelect1_4.ItemsSource = oQuestion.QDetailsParent;
			cmbSelect1_4.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1_4.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			ListCmbParent.Add(cmbSelect1_4);
			ListCmbCode.Add(cmbSelect2_4);
			ListTxtFill.Add(txtFill4);
			ListCmbParent[3].Tag = 3;
			ListCmbCode[3].Tag = 3;
			textBlock1_5.Text = str + _003F487_003F._003F488_003F("4");
			textBlock2_5.Text = str2 + _003F487_003F._003F488_003F("4");
			cmbSelect1_5.ItemsSource = oQuestion.QDetailsParent;
			cmbSelect1_5.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1_5.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			ListCmbParent.Add(cmbSelect1_5);
			ListCmbCode.Add(cmbSelect2_5);
			ListTxtFill.Add(txtFill5);
			ListCmbParent[4].Tag = 4;
			ListCmbCode[4].Tag = 4;
			btnNone.Content = oQuestion.NoneCodeText;
			btnNone.Tag = oQuestion.NoneCode;
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
				for (int m = 0; m < nTotal; m++)
				{
					oQSingle[m].SelectedCode = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQSingle[m].QuestionName);
					oQSingle[m].OtherCode = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQSingle[m].QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
					_003F162_003F(m, oQSingle[m].SelectedCode, oQSingle[m].OtherCode);
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

		private string _003F162_003F(int _003F390_003F, string _003F391_003F, string _003F392_003F)
		{
			//IL_0153: Incompatible stack heights: 0 vs 1
			//IL_0163: Incompatible stack heights: 0 vs 1
			//IL_0179: Incompatible stack heights: 0 vs 1
			if (_003F391_003F != null)
			{
				bool flag = _003F391_003F != _003F487_003F._003F488_003F("");
				if ((int)/*Error near IL_0158: Stack underflow*/ != 0)
				{
					List<QSingle> oQSingle2 = oQSingle;
					((List<QSingle>)/*Error near IL_0011: Stack underflow*/)[_003F390_003F].SelectedCode = _003F391_003F;
					ListCmbParent[_003F390_003F].SelectedValue = oQuestion.GetParentCode(_003F391_003F);
					oQuestion.GetRelation2(oQuestion.ParentCode);
					ListCmbCode[_003F390_003F].ItemsSource = oQuestion.QGroupDetails;
					ListCmbCode[_003F390_003F].DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
					ListCmbCode[_003F390_003F].SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
					ListCmbCode[_003F390_003F].SelectedValue = _003F391_003F;
					oQSingle[_003F390_003F].SelectedCode = _003F391_003F;
					if (_003F391_003F != oQSingle[_003F390_003F].OtherCode)
					{
						QSingle qSingle = oQSingle[_003F390_003F];
						string otherCode = _003F487_003F._003F488_003F("");
						((QSingle)/*Error near IL_00eb: Stack underflow*/).OtherCode = otherCode;
						ListTxtFill[_003F390_003F].Background = Brushes.LightGray;
						ListTxtFill[_003F390_003F].IsEnabled = false;
						ListTxtFill[_003F390_003F].Text = _003F487_003F._003F488_003F("");
					}
					else
					{
						oQSingle[_003F390_003F].OtherCode = _003F392_003F;
						ListTxtFill[_003F390_003F].IsEnabled = true;
						ListTxtFill[_003F390_003F].Background = Brushes.White;
						ListTxtFill[_003F390_003F].Text = _003F392_003F;
					}
					return ListCmbParent[_003F390_003F].SelectedValue.ToString();
				}
			}
			return _003F487_003F._003F488_003F("");
		}

		private void _003F148_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F)
		{
			//IL_0072: Incompatible stack heights: 0 vs 1
			//IL_0083: Incompatible stack heights: 0 vs 2
			int index = (int)((ComboBox)_003F347_003F).Tag;
			if (ListCmbParent[index].SelectedValue != null)
			{
				string text = ListCmbParent[index].SelectedValue.ToString();
				if (text != null)
				{
					bool flag = text != _003F487_003F._003F488_003F("");
					if ((int)/*Error near IL_0077: Stack underflow*/ != 0)
					{
						QSingle oQuestion2 = oQuestion;
						((QSingle)/*Error near IL_0088: Stack underflow*/).GetRelation2((string)/*Error near IL_0088: Stack underflow*/);
						ListCmbCode[index].ItemsSource = oQuestion.QGroupDetails;
						ListCmbCode[index].DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
						ListCmbCode[index].SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
						oQSingle[index].SelectedCode = _003F487_003F._003F488_003F("");
						oQSingle[index].OtherCode = oQuestion.OtherCode;
					}
				}
			}
		}

		private void _003F149_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F)
		{
			//IL_00fb: Incompatible stack heights: 0 vs 1
			//IL_010c: Incompatible stack heights: 0 vs 2
			int index = (int)((ComboBox)_003F347_003F).Tag;
			if (ListCmbCode[index].SelectedValue != null)
			{
				string text = ListCmbCode[index].SelectedValue.ToString();
				oQSingle[index].SelectedCode = text;
				if (text != oQSingle[index].OtherCode)
				{
					TextBox textBox = ListTxtFill[index];
					SolidColorBrush lightGray = Brushes.LightGray;
					((System.Windows.Controls.Control)/*Error near IL_005f: Stack underflow*/).Background = lightGray;
					ListTxtFill[index].IsEnabled = false;
				}
				else
				{
					ListTxtFill[index].IsEnabled = true;
					ListTxtFill[index].Background = Brushes.White;
					if (ListTxtFill[index].Text == _003F487_003F._003F488_003F(""))
					{
						List<TextBox> listTxtFill = ListTxtFill;
						((List<TextBox>)/*Error near IL_0111: Stack underflow*/)[(int)/*Error near IL_0111: Stack underflow*/].Focus();
					}
				}
			}
		}

		private void _003F161_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_015b: Incompatible stack heights: 0 vs 1
			//IL_017b: Incompatible stack heights: 1 vs 0
			bool num = oQuestion.SelectedCode == oQuestion.NoneCode;
			goto IL_0136;
			IL_0136:
			if (num)
			{
				oQuestion.SelectedCode = _003F487_003F._003F488_003F("");
				Button btnNone2 = btnNone;
				Style style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
				((FrameworkElement)/*Error near IL_003a: Stack underflow*/).Style = style;
				for (int i = 0; i < nTotal; i++)
				{
					ListCmbParent[i].IsEnabled = true;
					ListCmbCode[i].IsEnabled = true;
					ListTxtFill[i].IsEnabled = true;
					ListTxtFill[i].Background = Brushes.White;
				}
				return;
			}
			goto IL_009d;
			IL_009d:
			oQuestion.SelectedCode = oQuestion.NoneCode;
			btnNone.Style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
			for (int j = 0; j < nTotal; j++)
			{
				ListCmbParent[j].IsEnabled = false;
				ListCmbCode[j].IsEnabled = false;
				ListTxtFill[j].Background = Brushes.LightGray;
				ListTxtFill[j].IsEnabled = false;
			}
			return;
			IL_0176:
			goto IL_0136;
			IL_0166:
			goto IL_009d;
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			if (!((string)btnNav.Content != btnNav_Content))
			{
				btnNav.Content = SurveyMsg.MsgbtnNav_SaveText;
				if (oQuestion.SelectedCode == oQuestion.NoneCode)
				{
					oQSingle[0].SelectedCode = oQuestion.NoneCode;
					oQSingle[0].FillText = _003F487_003F._003F488_003F("");
					for (int i = 1; i < nTotal; i++)
					{
						oQSingle[i].SelectedCode = _003F487_003F._003F488_003F("");
						oQSingle[i].FillText = _003F487_003F._003F488_003F("");
					}
				}
				else
				{
					int num = 0;
					for (int j = 0; j < nTotal; j++)
					{
						if (ListCmbParent[j].Text.Trim() != _003F487_003F._003F488_003F("") && (ListCmbCode[j].SelectedValue == _003F487_003F._003F488_003F("") || ListCmbCode[j].SelectedValue == null))
						{
							MessageBox.Show(SurveyMsg.MsgNotSelectClassB, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							btnNav.Content = btnNav_Content;
							ListCmbCode[j].Focus();
							return;
						}
						if ((string)ListCmbCode[j].SelectedValue != _003F487_003F._003F488_003F("") && ListCmbCode[j].SelectedValue != null)
						{
							oQSingle[num].SelectedCode = ListCmbCode[j].SelectedValue.ToString();
							oQSingle[num].FillText = ListTxtFill[j].Text;
							if (ListTxtFill[j].IsEnabled && ListTxtFill[j].Text == _003F487_003F._003F488_003F(""))
							{
								MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
								btnNav.Content = btnNav_Content;
								ListTxtFill[j].Focus();
								return;
							}
							num++;
						}
					}
					for (int k = num; k < nTotal; k++)
					{
						oQSingle[k].SelectedCode = _003F487_003F._003F488_003F("");
						oQSingle[k].FillText = _003F487_003F._003F488_003F("");
					}
				}
				if (_003F87_003F())
				{
					btnNav.Content = btnNav_Content;
				}
				else
				{
					List<VEAnswer> list = _003F88_003F();
					oLogicEngine.PageAnswer = list;
					if (!oLogicEngine.CheckLogic(CurPageId))
					{
						if (oLogicEngine.IS_ALLOW_PASS == 0)
						{
							MessageBox.Show(oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							btnNav.Content = btnNav_Content;
							return;
						}
						if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
						{
							btnNav.Content = btnNav_Content;
							return;
						}
					}
					foreach (QSingle item in oQSingle)
					{
						item.BeforeSave();
						item.Save(MySurveyId, SurveyHelper.SurveySequence, true);
					}
					if (SurveyHelper.Debug)
					{
						MessageBox.Show(SurveyHelper.ShowPageAnswer(list), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
					MyNav.PageAnswer = list;
					_003F81_003F();
				}
			}
		}

		private bool _003F87_003F()
		{
			//IL_019c: Incompatible stack heights: 0 vs 1
			//IL_01c5: Incompatible stack heights: 0 vs 2
			//IL_01da: Incompatible stack heights: 0 vs 3
			//IL_0220: Incompatible stack heights: 0 vs 1
			//IL_0244: Incompatible stack heights: 0 vs 2
			//IL_025f: Incompatible stack heights: 0 vs 2
			//IL_0273: Incompatible stack heights: 0 vs 1
			if (oQSingle[0].SelectedCode == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return (byte)/*Error near IL_0026: Stack underflow*/ != 0;
			}
			for (int i = 0; i < nTotal - 1 && !(oQSingle[i].SelectedCode == _003F487_003F._003F488_003F("")); i++)
			{
				for (int j = i + 1; j < nTotal && !(oQSingle[j].SelectedCode == _003F487_003F._003F488_003F("")); j++)
				{
					List<QSingle> oQSingle2 = oQSingle;
					if (((List<QSingle>)/*Error near IL_0081: Stack underflow*/)[(int)/*Error near IL_0081: Stack underflow*/].SelectedCode == oQSingle[j].SelectedCode)
					{
						string msgSelectRepeat = SurveyMsg.MsgSelectRepeat;
						string msgCaption = SurveyMsg.MsgCaption;
						MessageBox.Show((string)/*Error near IL_00a8: Stack underflow*/, (string)/*Error near IL_00a8: Stack underflow*/, (MessageBoxButton)/*Error near IL_00a8: Stack underflow*/, MessageBoxImage.Hand);
						ListCmbCode[j].Focus();
						return true;
					}
				}
			}
			int num = 0;
			for (int k = 0; k < nTotal - 1 && !(oQSingle[k].SelectedCode == _003F487_003F._003F488_003F("")); k++)
			{
				num++;
			}
			if (num < oQuestion.QDefine.MIN_COUNT)
			{
				SurveyDefine qDefine = oQuestion.QDefine;
				if (((SurveyDefine)/*Error near IL_013a: Stack underflow*/).MIN_COUNT > 0)
				{
					_003F487_003F._003F488_003F("臼崟锍認鐂柣ةݳ࠷ॻਥ䔮鰊镻㸃");
					int mIN_COUNT = oQuestion.QDefine.MIN_COUNT;
					MessageBox.Show(string.Format(arg0: ((int)/*Error near IL_0140: Stack underflow*/).ToString(), format: (string)/*Error near IL_014e: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			if (num > oQuestion.QDefine.MAX_COUNT)
			{
				int mAX_COUNT = oQuestion.QDefine.MAX_COUNT;
				if (/*Error near IL_0264: Stack underflow*/ > /*Error near IL_0264: Stack underflow*/)
				{
					_003F487_003F._003F488_003F("朏堔凢䷩鐂柣ةݳ࠷ॻਥ䔮鰊镻㸃");
					MessageBox.Show(string.Format(arg0: oQuestion.QDefine.MAX_COUNT.ToString(), format: (string)/*Error near IL_0291: Stack underflow*/), SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return true;
				}
			}
			return false;
		}

		private List<VEAnswer> _003F88_003F()
		{
			List<VEAnswer> list = new List<VEAnswer>();
			SurveyHelper.Answer = _003F487_003F._003F488_003F("");
			foreach (QSingle item in oQSingle)
			{
				VEAnswer vEAnswer = new VEAnswer();
				vEAnswer.QUESTION_NAME = item.QuestionName;
				vEAnswer.CODE = item.SelectedCode;
				list.Add(vEAnswer);
				SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + item.QuestionName + _003F487_003F._003F488_003F("<") + item.SelectedCode;
				if (item.FillText != _003F487_003F._003F488_003F(""))
				{
					VEAnswer vEAnswer2 = new VEAnswer();
					vEAnswer2.QUESTION_NAME = item.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349");
					vEAnswer2.CODE = item.FillText;
					list.Add(vEAnswer2);
					SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + item.FillText;
				}
			}
			SurveyHelper.Answer = _003F94_003F(SurveyHelper.Answer, 1, -9999);
			return list;
		}

		private void _003F81_003F()
		{
			//IL_01f8: Incompatible stack heights: 0 vs 4
			//IL_0212: Incompatible stack heights: 0 vs 1
			//IL_0228: Incompatible stack heights: 0 vs 2
			//IL_0247: Incompatible stack heights: 0 vs 1
			//IL_025e: Incompatible stack heights: 0 vs 2
			//IL_0273: Incompatible stack heights: 0 vs 1
			//IL_0278: Invalid comparison between Unknown and I4
			//IL_028a: Incompatible stack heights: 0 vs 2
			//IL_02a1: Incompatible stack heights: 0 vs 2
			//IL_02c5: Incompatible stack heights: 0 vs 1
			//IL_02d0: Incompatible stack heights: 0 vs 1
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			MyNav.PageStartTime = SurveyHelper.PageStartTime;
			MyNav.RecordFileName = SurveyHelper.RecordFileName;
			MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			if (!(MyNav.GroupLevel == _003F487_003F._003F488_003F("")))
			{
				MyNav.NextCirclePage(MySurveyId, surveySequence, CurPageId, roadMapVersion);
				SurveyHelper.CircleACount = MyNav.CircleACount;
				SurveyHelper.CircleACurrent = MyNav.CircleACurrent;
				if (MyNav.IsLastA)
				{
					int groupPageType = MyNav.GroupPageType;
					if ((int)/*Error near IL_0217: Stack underflow*/ != 0)
					{
						int groupPageType2 = MyNav.GroupPageType;
						if (/*Error near IL_022d: Stack underflow*/ != /*Error near IL_022d: Stack underflow*/)
						{
							goto IL_00d7;
						}
					}
					SurveyHelper.CircleACode = _003F487_003F._003F488_003F("");
					SurveyHelper.CircleACodeText = _003F487_003F._003F488_003F("");
				}
				goto IL_00d7;
			}
			NavBase myNav = MyNav;
			string mySurveyId = MySurveyId;
			string curPageId = CurPageId;
			((NavBase)/*Error near IL_0061: Stack underflow*/).NextPage((string)/*Error near IL_0061: Stack underflow*/, (int)/*Error near IL_0061: Stack underflow*/, (string)/*Error near IL_0061: Stack underflow*/, roadMapVersion);
			goto IL_014f;
			IL_00d7:
			if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
			{
				int circleBCount = MyNav.CircleBCount;
				SurveyHelper.CircleBCount = (int)/*Error near IL_00fb: Stack underflow*/;
				SurveyHelper.CircleBCurrent = MyNav.CircleBCurrent;
				if (MyNav.IsLastB)
				{
					int groupPageType3 = MyNav.GroupPageType;
					if (/*Error near IL_0263: Stack underflow*/ != /*Error near IL_0263: Stack underflow*/)
					{
						int groupPageType4 = MyNav.GroupPageType;
						if ((int)/*Error near IL_0278: Stack underflow*/ != 12)
						{
							int groupPageType5 = MyNav.GroupPageType;
							if (/*Error near IL_028f: Stack underflow*/ != /*Error near IL_028f: Stack underflow*/)
							{
								int groupPageType6 = MyNav.GroupPageType;
								if (/*Error near IL_02a6: Stack underflow*/ != /*Error near IL_02a6: Stack underflow*/)
								{
									goto IL_014f;
								}
							}
						}
					}
					SurveyHelper.CircleBCode = _003F487_003F._003F488_003F("");
					SurveyHelper.CircleBCodeText = _003F487_003F._003F488_003F("");
				}
			}
			goto IL_014f;
			IL_01d6:
			goto IL_02d5;
			IL_014f:
			string text = oLogicEngine.Route(MyNav.RoadMap.FORM_NAME);
			SurveyHelper.RoadMapVersion = MyNav.RoadMap.VERSION_ID.ToString();
			string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), text);
			if (text.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
			{
				string.Format(_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭"), text);
				uriString = (string)/*Error near IL_01b7: Stack underflow*/;
			}
			if (!(text == SurveyHelper.CurPageName))
			{
				goto IL_02d5;
			}
			((Page)/*Error near IL_01cc: Stack underflow*/).NavigationService.Refresh();
			goto IL_02f3;
			IL_02d5:
			base.NavigationService.RemoveBackEntry();
			base.NavigationService.Navigate(new Uri(uriString));
			goto IL_02f3;
			IL_02f3:
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			SurveyHelper.NavLoad = 0;
		}

		private void _003F163_003F(int _003F393_003F, List<VEAnswer> _003F370_003F)
		{
			btnNav.IsEnabled = false;
			btnNav.Content = _003F487_003F._003F488_003F("歨嘢䷔塐Ы軱簈圝\u082dबਯ");
			foreach (VEAnswer item in _003F370_003F)
			{
				Logging.Data.WriteLog(MySurveyId, item.QUESTION_NAME + _003F487_003F._003F488_003F("-") + item.CODE);
			}
			Thread.Sleep(_003F393_003F);
			btnNav.Content = btnNav_Content;
			btnNav.IsEnabled = true;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\aŠɕ\u0356ѝԍ١\u0740ࡐॶਥ\u0b7e\u0c73\u0d76\u0e6a\u0f76ၶᅲቸ፡ᐻᕥᙻ\u1774ᡧ\u1920\u1a7c᭨ᱠ\u1d6aṾὠ\u2067Ⅹ∳⌫⑼╢♯❭"), UriKind.Relative);
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
				((Relation5)_003F350_003F).Loaded += _003F80_003F;
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
				gridContent = (Grid)_003F350_003F;
				break;
			case 6:
				textBlock1_1 = (TextBlock)_003F350_003F;
				break;
			case 7:
				cmbSelect1_1 = (ComboBox)_003F350_003F;
				cmbSelect1_1.SelectionChanged += _003F148_003F;
				break;
			case 8:
				textBlock2_1 = (TextBlock)_003F350_003F;
				break;
			case 9:
				cmbSelect2_1 = (ComboBox)_003F350_003F;
				cmbSelect2_1.SelectionChanged += _003F149_003F;
				break;
			case 10:
				txtFillTitle1 = (TextBlock)_003F350_003F;
				break;
			case 11:
				txtFill1 = (TextBox)_003F350_003F;
				break;
			case 12:
				textBlock1_2 = (TextBlock)_003F350_003F;
				break;
			case 13:
				cmbSelect1_2 = (ComboBox)_003F350_003F;
				cmbSelect1_2.SelectionChanged += _003F148_003F;
				break;
			case 14:
				textBlock2_2 = (TextBlock)_003F350_003F;
				break;
			case 15:
				cmbSelect2_2 = (ComboBox)_003F350_003F;
				cmbSelect2_2.SelectionChanged += _003F149_003F;
				break;
			case 16:
				txtFillTitle2 = (TextBlock)_003F350_003F;
				break;
			case 17:
				txtFill2 = (TextBox)_003F350_003F;
				break;
			case 18:
				textBlock1_3 = (TextBlock)_003F350_003F;
				break;
			case 19:
				cmbSelect1_3 = (ComboBox)_003F350_003F;
				cmbSelect1_3.SelectionChanged += _003F148_003F;
				break;
			case 20:
				textBlock2_3 = (TextBlock)_003F350_003F;
				break;
			case 21:
				cmbSelect2_3 = (ComboBox)_003F350_003F;
				cmbSelect2_3.SelectionChanged += _003F149_003F;
				break;
			case 22:
				txtFillTitle3 = (TextBlock)_003F350_003F;
				break;
			case 23:
				txtFill3 = (TextBox)_003F350_003F;
				break;
			case 24:
				textBlock1_4 = (TextBlock)_003F350_003F;
				break;
			case 25:
				cmbSelect1_4 = (ComboBox)_003F350_003F;
				cmbSelect1_4.SelectionChanged += _003F148_003F;
				break;
			case 26:
				textBlock2_4 = (TextBlock)_003F350_003F;
				break;
			case 27:
				cmbSelect2_4 = (ComboBox)_003F350_003F;
				cmbSelect2_4.SelectionChanged += _003F149_003F;
				break;
			case 28:
				txtFillTitle4 = (TextBlock)_003F350_003F;
				break;
			case 29:
				txtFill4 = (TextBox)_003F350_003F;
				break;
			case 30:
				textBlock1_5 = (TextBlock)_003F350_003F;
				break;
			case 31:
				cmbSelect1_5 = (ComboBox)_003F350_003F;
				cmbSelect1_5.SelectionChanged += _003F148_003F;
				break;
			case 32:
				textBlock2_5 = (TextBlock)_003F350_003F;
				break;
			case 33:
				cmbSelect2_5 = (ComboBox)_003F350_003F;
				cmbSelect2_5.SelectionChanged += _003F149_003F;
				break;
			case 34:
				txtFillTitle5 = (TextBlock)_003F350_003F;
				break;
			case 35:
				txtFill5 = (TextBox)_003F350_003F;
				break;
			case 36:
				stackPanel1 = (StackPanel)_003F350_003F;
				break;
			case 37:
				btnNone = (Button)_003F350_003F;
				btnNone.Click += _003F161_003F;
				break;
			case 38:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 39:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_00a9:
			goto IL_00b3;
		}
	}
}
