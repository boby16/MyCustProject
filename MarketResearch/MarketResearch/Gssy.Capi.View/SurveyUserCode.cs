using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Gssy.Capi.View
{
	public class SurveyUserCode : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private UDPX oFunc = new UDPX();

		private QFill oQuestion = new QFill();

		internal TextBlock txtQuestionTitle;

		internal Span span1;

		internal Span span2;

		internal Span span3;

		internal Span span4;

		internal Span span5;

		internal Span span6;

		internal Span span7;

		internal StackPanel stk1;

		internal TextBlock txtBefore;

		internal TextBox txtFill;

		internal TextBlock txtAfter;

		internal TextBlock txtSurvey;

		internal Button btnNav;

		private bool _contentLoaded;

		public SurveyUserCode()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			oQuestion.Init(CurPageId, 0);
			string qUESTION_TITLE = oQuestion.QDefine.QUESTION_TITLE;
			BoldTitle boldTitle = new BoldTitle();
			qUESTION_TITLE = boldTitle.ReplaceTitle(qUESTION_TITLE);
			boldTitle.DealBoldString(qUESTION_TITLE);
			span1.Inlines.Clear();
			span2.Inlines.Clear();
			span3.Inlines.Clear();
			span4.Inlines.Clear();
			span5.Inlines.Clear();
			span6.Inlines.Clear();
			span7.Inlines.Clear();
			if (boldTitle.BoldCount == 0)
			{
				txtQuestionTitle.Text = qUESTION_TITLE;
			}
			else
			{
				span1.Inlines.Add(new Run(boldTitle.SpanString1));
				span2.Inlines.Add(new Run(boldTitle.BoldString1));
				span3.Inlines.Add(new Run(boldTitle.SpanString2));
				span4.Inlines.Add(new Run(boldTitle.BoldString2));
				span5.Inlines.Add(new Run(boldTitle.SpanString3));
				span6.Inlines.Add(new Run(boldTitle.BoldString3));
				span7.Inlines.Add(new Run(boldTitle.SpanString4));
			}
			if (oQuestion.QDefine.TITLE_FONTSIZE != 0)
			{
				txtQuestionTitle.FontSize = (double)oQuestion.QDefine.TITLE_FONTSIZE;
			}
			txtBefore.Text = SurveyHelper.SurveyCity;
			txtAfter.Text = oQuestion.QDefine.NOTE;
			if (oQuestion.QDefine.CONTROL_TYPE > 0)
			{
				txtFill.MaxLength = oQuestion.QDefine.CONTROL_TYPE;
				txtFill.Width = (double)(oQuestion.QDefine.CONTROL_TYPE * 30);
			}
			txtFill.Focus();
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				txtFill.Text = autoFill.FillInt(oQuestion.QDefine);
				_003F58_003F(this, _003F348_003F);
			}
			if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				int cONTROL_TYPE = oQuestion.QDefine.CONTROL_TYPE;
				oQuestion.ReadAnswer(MySurveyId, SurveyHelper.SurveySequence);
				foreach (SurveyAnswer item in oQuestion.QAnswersRead)
				{
					if (item.QUESTION_NAME == oQuestion.QuestionName)
					{
						txtFill.Text = oFunc.RIGHT(item.CODE, cONTROL_TYPE);
					}
				}
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0227: Incompatible stack heights: 0 vs 1
			//IL_0242: Incompatible stack heights: 0 vs 1
			//IL_0252: Incompatible stack heights: 0 vs 1
			//IL_026f: Incompatible stack heights: 0 vs 4
			//IL_028a: Incompatible stack heights: 0 vs 1
			string text = txtFill.Text;
			text = text.Replace(_003F487_003F._003F488_003F("^"), _003F487_003F._003F488_003F(""));
			oQuestion.FillText = text;
			if (oQuestion.FillText == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				txtFill.Focus();
				return;
			}
			int cONTROL_TYPE = oQuestion.QDefine.CONTROL_TYPE;
			if (oQuestion.FillText.Length != cONTROL_TYPE)
			{
				string.Format(SurveyMsg.MsgInteNum, cONTROL_TYPE.ToString());
				MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_0094: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Asterisk);
				txtFill.Focus();
				return;
			}
			List<VEAnswer> list = new List<VEAnswer>();
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = oQuestion.QuestionName;
			vEAnswer.CODE = oQuestion.FillText;
			SurveyHelper.Answer = oQuestion.QuestionName + _003F487_003F._003F488_003F("<") + oQuestion.FillText;
			list.Add(vEAnswer);
			oLogicEngine.PageAnswer = list;
			oLogicEngine.SurveyID = MySurveyId;
			if (!oLogicEngine.CheckLogic(CurPageId))
			{
				LogicEngine oLogicEngine2 = oLogicEngine;
				if (((LogicEngine)/*Error near IL_0139: Stack underflow*/).IS_ALLOW_PASS == 0)
				{
					string logic_Message = oLogicEngine.Logic_Message;
					string msgCaption2 = SurveyMsg.MsgCaption;
					MessageBox.Show((string)/*Error near IL_0143: Stack underflow*/, (string)/*Error near IL_0143: Stack underflow*/, (MessageBoxButton)/*Error near IL_0143: Stack underflow*/, (MessageBoxImage)/*Error near IL_0143: Stack underflow*/);
					return;
				}
				if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
				{
					return;
				}
			}
			goto IL_018c;
			IL_0275:
			goto IL_018c;
			IL_018c:
			oQuestion.FillText = SurveyHelper.SurveyCity + text;
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_01da: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Asterisk);
			}
			if (SurveyMsg.RecordIsOn == _003F487_003F._003F488_003F("]ūɮ\u0363ѹծ\u0640ݻࡈ२ਗ਼୰\u0c71\u0d77\u0e64"))
			{
				bool recordIsRunning = SurveyHelper.RecordIsRunning;
			}
			MyNav.PageAnswer = list;
			_003F81_003F();
		}

		private void _003F81_003F()
		{
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
				uriString = string.Format(_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭"), text);
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
			//IL_00a6: Incompatible stack heights: 0 vs 1
			//IL_00b3: Incompatible stack heights: 0 vs 3
			//IL_00c3: Incompatible stack heights: 0 vs 1
			//IL_00c8: Invalid comparison between Unknown and I4
			//IL_00d3: Incompatible stack heights: 0 vs 1
			//IL_00e5: Incompatible stack heights: 0 vs 2
			//IL_0107: Incompatible stack heights: 0 vs 1
			//IL_010c: Invalid comparison between Unknown and I4
			//IL_011c: Incompatible stack heights: 0 vs 1
			//IL_0121: Invalid comparison between Unknown and I4
			//IL_012c: Incompatible stack heights: 0 vs 1
			//IL_013c: Incompatible stack heights: 0 vs 1
			//IL_0141: Invalid comparison between Unknown and I4
			if (_003F348_003F.Key == Key.Return)
			{
				bool isEnabled = btnNav.IsEnabled;
				if ((int)/*Error near IL_00ab: Stack underflow*/ != 0)
				{
					((SurveyUserCode)/*Error near IL_0016: Stack underflow*/)._003F58_003F((object)/*Error near IL_0016: Stack underflow*/, (RoutedEventArgs)/*Error near IL_0016: Stack underflow*/);
				}
			}
			TextBox textBox = _003F347_003F as TextBox;
			if (_003F348_003F.Key >= Key.NumPad0)
			{
				Key key = _003F348_003F.Key;
				if ((int)/*Error near IL_00c8: Stack underflow*/ <= 83)
				{
					string text = textBox.Text;
					string value = _003F487_003F._003F488_003F("/");
					if (((string)/*Error near IL_0040: Stack underflow*/).Contains(value))
					{
						Key key2 = _003F348_003F.Key;
						if (/*Error near IL_00ea: Stack underflow*/ == /*Error near IL_00ea: Stack underflow*/)
						{
							_003F348_003F.Handled = true;
							return;
						}
					}
					goto IL_004a;
				}
			}
			if (_003F348_003F.Key >= Key.D0)
			{
				Key key3 = _003F348_003F.Key;
				if ((int)/*Error near IL_010c: Stack underflow*/ <= 43)
				{
					ModifierKeys modifier = _003F348_003F.KeyboardDevice.Modifiers;
					if ((int)/*Error near IL_0121: Stack underflow*/ != 4)
					{
						string text2 = textBox.Text;
						string value2 = _003F487_003F._003F488_003F("/");
						if (((string)/*Error near IL_007b: Stack underflow*/).Contains(value2))
						{
							Key key4 = _003F348_003F.Key;
							if ((int)/*Error near IL_0141: Stack underflow*/ == 144)
							{
								_003F348_003F.Handled = true;
								return;
							}
						}
						goto IL_008a;
					}
				}
			}
			_003F348_003F.Handled = true;
			return;
			IL_008a:
			_003F348_003F.Handled = false;
			return;
			IL_00f2:
			goto IL_004a;
			IL_0149:
			goto IL_008a;
			IL_004a:
			_003F348_003F.Handled = false;
		}

		private unsafe void _003F98_003F(object _003F347_003F, TextChangedEventArgs _003F348_003F)
		{
			//IL_0067: Incompatible stack heights: 0 vs 2
			//IL_0079: Incompatible stack heights: 0 vs 3
			TextBox textBox = _003F347_003F as TextBox;
			TextChange[] array = new TextChange[_003F348_003F.Changes.Count];
			_003F348_003F.Changes.CopyTo(array, 0);
			int offset = array[0].Offset;
			if (array[0].AddedLength > 0)
			{
				double num = 0.0;
				string text2 = textBox.Text;
				if (!double.TryParse((string)/*Error near IL_0041: Stack underflow*/, out *(double*)/*Error near IL_0041: Stack underflow*/))
				{
					string text3 = textBox.Text;
					int addedLength = array[0].AddedLength;
					string text = ((string)/*Error near IL_0086: Stack underflow*/).Remove((int)/*Error near IL_0086: Stack underflow*/, addedLength);
					((TextBox)/*Error near IL_008b: Stack underflow*/).Text = text;
					textBox.Select(offset, 0);
				}
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
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0002ūɘ\u0359ѐԆ٤\u0747ࡕ\u094dਘ\u0b41\u0c4e\u0d4d\u0e6f\u0f71\u1073ᅹት፮ᐶᕮᙾ\u1773ᡢ\u193b\u1a60᭧ᱣ\u1d66Ṫί⁸ⅿ≮⍸⑪╧♣❣⠫⥼⩢⭯Ɑ"), UriKind.Relative);
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
				((SurveyUserCode)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				span1 = (Span)_003F350_003F;
				break;
			case 4:
				span2 = (Span)_003F350_003F;
				break;
			case 5:
				span3 = (Span)_003F350_003F;
				break;
			case 6:
				span4 = (Span)_003F350_003F;
				break;
			case 7:
				span5 = (Span)_003F350_003F;
				break;
			case 8:
				span6 = (Span)_003F350_003F;
				break;
			case 9:
				span7 = (Span)_003F350_003F;
				break;
			case 10:
				stk1 = (StackPanel)_003F350_003F;
				break;
			case 11:
				txtBefore = (TextBlock)_003F350_003F;
				break;
			case 12:
				txtFill = (TextBox)_003F350_003F;
				txtFill.KeyDown += _003F86_003F;
				txtFill.TextChanged += _003F98_003F;
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
