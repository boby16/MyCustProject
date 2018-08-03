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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class PageSingleOther : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__14_0;

			internal int _003F332_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private QSingle oQuestion = new QSingle();

		private string SelectedValue;

		private int Button_Type;

		private int Button_Height;

		private int Button_Width;

		private int Button_FontSize;

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		internal TextBlock txtQuestionTitle;

		internal CheckBox chkOther;

		internal TextBox txtFill;

		internal WrapPanel wrapPanel1;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public PageSingleOther()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_034a: Incompatible stack heights: 0 vs 2
			//IL_0361: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			oQuestion.Init(CurPageId, 0, true);
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			txtQuestionTitle.Text = _003F487_003F._003F488_003F("");
			BoldTitle boldTitle = new BoldTitle();
			boldTitle.SpanTitle(MySurveyId, qUESTION_TITLE, SurveyHelper.CircleACode, SurveyHelper.CircleBCode);
			foreach (classHtmlText item in boldTitle.lSpan)
			{
				if (item.TitleTextType == _003F487_003F._003F488_003F("?ŀȿ"))
				{
					Span span = new Span();
					span.Inlines.Add(new Run(item.TitleText));
					span.Foreground = (Brush)FindResource(_003F487_003F._003F488_003F("\\Źɯ\u037aѻբ٢\u0747\u0876ॶ\u0a71୩"));
					span.FontWeight = FontWeights.Bold;
					txtQuestionTitle.Inlines.Add(span);
				}
				else
				{
					Span span2 = new Span();
					span2.Inlines.Add(new Run(item.TitleText));
					txtQuestionTitle.Inlines.Add(span2);
				}
			}
			if (oQuestion.QDefine.TITLE_FONTSIZE != 0)
			{
				txtQuestionTitle.FontSize = (double)oQuestion.QDefine.TITLE_FONTSIZE;
			}
			if (oQuestion.QDefine.NOTE != _003F487_003F._003F488_003F(""))
			{
				chkOther.Content = boldTitle.ReplaceABTitle(oQuestion.QDefine.NOTE);
			}
			if (oQuestion.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				oLogicEngine.SurveyID = MySurveyId;
				string[] array = oLogicEngine.aryCode(oQuestion.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					foreach (SurveyDetail qDetail in oQuestion.QDetails)
					{
						if (qDetail.CODE == array[i].ToString())
						{
							list.Add(qDetail);
							break;
						}
					}
				}
				if (_003F7_003F._003C_003E9__14_0 == null)
				{
					_003F7_003F._003C_003E9__14_0 = _003F7_003F._003C_003E9._003F332_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0366: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0366: Stack underflow*/);
				oQuestion.QDetails = list;
				oQuestion.ResetOtherCode();
			}
			if (oQuestion.QDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomDetails();
			}
			if (oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				Button_Type = oQuestion.QDefine.CONTROL_TYPE;
				Button_Height = oQuestion.QDefine.CONTROL_HEIGHT;
				Button_Width = oQuestion.QDefine.CONTROL_WIDTH;
				Button_FontSize = oQuestion.QDefine.CONTROL_FONTSIZE;
			}
			wrapPanel1.Visibility = Visibility.Hidden;
			_003F28_003F();
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				oQuestion.ReadAnswer(MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer item2 in oQuestion.QAnswersRead)
				{
					if (item2.QUESTION_NAME == oQuestion.QuestionName && item2.CODE != _003F487_003F._003F488_003F(""))
					{
						SelectedValue = item2.CODE;
						chkOther.IsChecked = true;
						txtFill.IsEnabled = true;
						wrapPanel1.Visibility = Visibility.Visible;
					}
					if (item2.QUESTION_NAME == oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"))
					{
						txtFill.Text = item2.CODE;
					}
				}
				foreach (Button child in wrapPanel1.Children)
				{
					if (child.Tag.ToString() == SelectedValue)
					{
						child.Style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
					}
				}
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				SecondsCountDown = SecondsWait;
				btnNav.IsEnabled = false;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_02e5: Incompatible stack heights: 0 vs 1
			//IL_02ea: Incompatible stack heights: 1 vs 0
			//IL_0303: Incompatible stack heights: 0 vs 1
			//IL_0313: Incompatible stack heights: 0 vs 1
			//IL_0318: Incompatible stack heights: 1 vs 0
			//IL_0332: Incompatible stack heights: 0 vs 2
			//IL_0342: Incompatible stack heights: 0 vs 1
			//IL_0360: Incompatible stack heights: 0 vs 2
			//IL_0375: Incompatible stack heights: 0 vs 1
			//IL_0380: Incompatible stack heights: 0 vs 1
			//IL_039b: Incompatible stack heights: 0 vs 1
			bool? isChecked = chkOther.IsChecked;
			bool flag = true;
			bool hasValue;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue = isChecked.HasValue;
			}
			if (hasValue)
			{
				string text = txtFill.Text;
				text = ((string)/*Error near IL_0033: Stack underflow*/).Trim();
				oQuestion.FillText = text;
				oQuestion.OtherCode = _003F487_003F._003F488_003F(";Ķ");
				oQuestion.SelectedCode = SelectedValue;
			}
			else
			{
				oQuestion.SelectedCode = _003F487_003F._003F488_003F("");
				oQuestion.FillText = _003F487_003F._003F488_003F("");
			}
			isChecked = chkOther.IsChecked;
			flag = true;
			bool hasValue2;
			if (isChecked.GetValueOrDefault() == flag)
			{
				hasValue2 = isChecked.HasValue;
			}
			if (hasValue2)
			{
				string selectedValue = SelectedValue;
				_003F487_003F._003F488_003F("");
				if (!((string)/*Error near IL_00ca: Stack underflow*/ == (string)/*Error near IL_00ca: Stack underflow*/))
				{
					string selectedValue2 = SelectedValue;
					if ((int)/*Error near IL_0347: Stack underflow*/ != 0)
					{
						if (oQuestion.FillText == _003F487_003F._003F488_003F(""))
						{
							string msgNotFillOther = SurveyMsg.MsgNotFillOther;
							string msgCaption2 = SurveyMsg.MsgCaption;
							MessageBox.Show((string)/*Error near IL_010f: Stack underflow*/, (string)/*Error near IL_010f: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
							txtFill.Focus();
							return;
						}
						goto IL_011d;
					}
				}
				MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			goto IL_011d;
			IL_029b:
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			List<VEAnswer> list;
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_02d4: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Asterisk);
			}
			MyNav.PageAnswer = list;
			_003F81_003F();
			return;
			IL_011d:
			list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQuestion.QuestionName;
			vEAnswer.CODE = oQuestion.SelectedCode;
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.SelectedCode;
			list.Add(vEAnswer);
			VEAnswer vEAnswer2 = new VEAnswer();
			vEAnswer2.QUESTION_NAME = oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349");
			vEAnswer2.CODE = oQuestion.FillText;
			SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + vEAnswer2.QUESTION_NAME + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			list.Add(vEAnswer2);
			oLogicEngine.PageAnswer = list;
			oLogicEngine.SurveyID = MySurveyId;
			if (!oLogicEngine.CheckLogic(CurPageId))
			{
				int iS_ALLOW_PASS = oLogicEngine.IS_ALLOW_PASS;
				if ((int)/*Error near IL_037a: Stack underflow*/ == 0)
				{
					MessageBox.Show(((PageSingleOther)/*Error near IL_0240: Stack underflow*/).oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
				if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
				{
					return;
				}
			}
			goto IL_029b;
			IL_0386:
			goto IL_029b;
		}

		private void _003F81_003F()
		{
			//IL_00f2: Incompatible stack heights: 0 vs 2
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			MyNav.PageStartTime = SurveyHelper.PageStartTime;
			MyNav.RecordFileName = SurveyHelper.RecordFileName;
			MyNav.RecordStartTime = SurveyHelper.RecordStartTime;
			MyNav.NextPage(MySurveyId, surveySequence, CurPageId, roadMapVersion);
			string text = oLogicEngine.Route(MyNav.RoadMap.FORM_NAME);
			SurveyHelper.RoadMapVersion = MyNav.RoadMap.VERSION_ID.ToString();
			string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), text);
			if (text.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
			{
				_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭");
				uriString = string.Format((string)/*Error near IL_00c1: Stack underflow*/, (object)/*Error near IL_00c1: Stack underflow*/);
			}
			if (text == SurveyHelper.CurPageName)
			{
				base.NavigationService.Refresh();
			}
			else
			{
				base.NavigationService.RemoveBackEntry();
				base.NavigationService.Navigate(new Uri(uriString));
			}
			SurveyHelper.SurveySequence = surveySequence + 1;
			SurveyHelper.NavCurPage = MyNav.RoadMap.PAGE_ID;
			SurveyHelper.CurPageName = MyNav.RoadMap.FORM_NAME;
			SurveyHelper.NavGoBackTimes = 0;
			SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
			SurveyHelper.NavLoad = 0;
		}

		private void _003F84_003F(object _003F347_003F, EventArgs _003F348_003F)
		{
			//IL_0036: Incompatible stack heights: 0 vs 2
			if (SecondsCountDown == 0)
			{
				Button btnNav2 = btnNav;
				string content = _003F487_003F._003F488_003F((string)/*Error near IL_0010: Stack underflow*/);
				((ContentControl)/*Error near IL_0015: Stack underflow*/).Content = content;
				btnNav.IsEnabled = true;
				btnNav.Style = (Style)FindResource(_003F487_003F._003F488_003F("Eūɿ\u034aѳը\u0656ݰ\u087a८\u0a64"));
				timer.Stop();
			}
			else
			{
				SecondsCountDown--;
				btnNav.Content = SecondsCountDown.ToString();
			}
		}

		private void _003F179_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			txtFill.IsEnabled = true;
			txtFill.Background = Brushes.White;
			wrapPanel1.Visibility = Visibility.Visible;
		}

		private void _003F180_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			txtFill.IsEnabled = false;
			txtFill.Background = Brushes.LightGray;
			wrapPanel1.Visibility = Visibility.Hidden;
		}

		private void _003F28_003F()
		{
			int num = 1;
			foreach (SurveyDetail qDetail in oQuestion.QDetails)
			{
				Button button = new Button();
				button.Content = qDetail.CODE_TEXT;
				button.Tag = qDetail.CODE;
				button.Margin = new Thickness(8.0, 8.0, 8.0, 8.0);
				button.Name = _003F487_003F._003F488_003F("aŶɯ") + num.ToString();
				button.Style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
				button.Click += _003F29_003F;
				button.FontSize = 28.0;
				button.Width = 260.0;
				button.Height = 50.0;
				if (Button_Type != 0)
				{
					if (Button_FontSize != 0)
					{
						button.FontSize = (double)Button_FontSize;
					}
					if (Button_Width != 0)
					{
						button.Width = (double)Button_Width;
					}
					if (Button_Height != 0)
					{
						button.Height = (double)Button_Height;
					}
				}
				wrapPanel1.Children.Add(button);
				num++;
			}
		}

		private void _003F29_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Button button = (Button)_003F347_003F;
			if (SelectedValue == button.Tag.ToString())
			{
				SelectedValue = _003F487_003F._003F488_003F("");
				button.Style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
			}
			else
			{
				foreach (Button child in wrapPanel1.Children)
				{
					if (child.Tag.ToString() == SelectedValue)
					{
						child.Style = (Style)FindResource(_003F487_003F._003F488_003F("XŢɘ\u036fѥՊٳݨࡖ॰\u0a7a୮\u0c64"));
					}
				}
				button.Style = (Style)FindResource(_003F487_003F._003F488_003F("Xůɥ\u034aѳը\u0656ݰ\u087a८\u0a64"));
				SelectedValue = button.Tag.ToString();
			}
			if (oQuestion.OtherCode != null && oQuestion.OtherCode != _003F487_003F._003F488_003F(""))
			{
				if (SelectedValue == oQuestion.OtherCode)
				{
					txtFill.IsEnabled = true;
					txtFill.Background = Brushes.White;
				}
				else
				{
					txtFill.IsEnabled = false;
					txtFill.Background = Brushes.LightGray;
				}
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
			goto IL_001b;
			IL_001b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0001Ūɟ\u0358ѓԇ٫\u0746ࡖ\u094cਟ\u0b40\u0c4d\u0d4c๐\u0f70ၰᅸቲ፯ᐵᕯᙱ\u1772ᡡ\u193a\u1a64\u1b72ᱵᵴṣὦ\u2060Ⅺ≠⍮⑥╽♠❢⡴⤫⩼⭢Ɐ\u2d6d"), UriKind.Relative);
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
				((PageSingleOther)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				chkOther = (CheckBox)_003F350_003F;
				chkOther.Checked += _003F179_003F;
				chkOther.Unchecked += _003F180_003F;
				break;
			case 4:
				txtFill = (TextBox)_003F350_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				break;
			case 5:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 6:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 7:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 8:
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
