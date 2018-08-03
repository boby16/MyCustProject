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

namespace Gssy.Capi.View
{
	public class PageSingleList : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__7_0;

			internal int _003F328_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		internal WrapPanel wrapPanel1;

		internal TextBlock txtQuestionTitle;

		internal ComboBox cmbSelect;

		internal StackPanel stackPanel1;

		internal TextBlock txtFillTitle;

		internal TextBox txtFill;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public PageSingleList()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_02f4: Incompatible stack heights: 0 vs 2
			//IL_030b: Incompatible stack heights: 0 vs 1
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
				if (_003F7_003F._003C_003E9__7_0 == null)
				{
					_003F7_003F._003C_003E9__7_0 = _003F7_003F._003C_003E9._003F328_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0310: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0310: Stack underflow*/);
				oQuestion.QDetails = list;
				oQuestion.ResetOtherCode();
			}
			if (oQuestion.QDefine.IS_RANDOM > 0)
			{
				oQuestion.RandomDetails();
			}
			if (oQuestion.QDefine.CONTROL_TYPE != 0)
			{
				if (oQuestion.QDefine.CONTROL_HEIGHT != 0)
				{
					cmbSelect.Height = (double)oQuestion.QDefine.CONTROL_HEIGHT;
				}
				if (oQuestion.QDefine.CONTROL_WIDTH != 0)
				{
					cmbSelect.Width = (double)oQuestion.QDefine.CONTROL_WIDTH;
				}
				if (oQuestion.QDefine.CONTROL_FONTSIZE != 0)
				{
					cmbSelect.FontSize = (double)oQuestion.QDefine.CONTROL_FONTSIZE;
				}
			}
			cmbSelect.ItemsSource = oQuestion.QDetails;
			cmbSelect.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			if (oQuestion.OtherCode != null && oQuestion.OtherCode != _003F487_003F._003F488_003F(""))
			{
				txtFill.Visibility = Visibility.Visible;
				txtFillTitle.Visibility = Visibility.Visible;
			}
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				SelectedValue = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName);
				txtFill.Text = oQuestion.ReadAnswerByQuestionName(MySurveyId, oQuestion.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				cmbSelect.SelectedValue = SelectedValue;
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_02a3: Incompatible stack heights: 0 vs 1
			//IL_02bd: Incompatible stack heights: 0 vs 1
			//IL_02ce: Incompatible stack heights: 0 vs 2
			//IL_02f2: Incompatible stack heights: 0 vs 1
			//IL_0306: Incompatible stack heights: 0 vs 2
			//IL_031b: Incompatible stack heights: 0 vs 1
			//IL_0326: Incompatible stack heights: 0 vs 1
			//IL_0341: Incompatible stack heights: 0 vs 1
			List<VEAnswer> list;
			if (cmbSelect.SelectedValue != null)
			{
				object selectedValue = cmbSelect.SelectedValue;
				if (!((string)/*Error near IL_0011: Stack underflow*/ == _003F487_003F._003F488_003F("")))
				{
					SelectedValue = cmbSelect.SelectedValue.ToString();
					oQuestion.SelectedCode = SelectedValue;
					if (oQuestion.OtherCode != null)
					{
						QSingle oQuestion2 = oQuestion;
						if (((QSingle)/*Error near IL_0079: Stack underflow*/).OtherCode != _003F487_003F._003F488_003F(""))
						{
							string selectedValue2 = SelectedValue;
							string otherCode = ((PageSingleList)/*Error near IL_0092: Stack underflow*/).oQuestion.OtherCode;
							if ((string)/*Error near IL_009c: Stack underflow*/ == otherCode)
							{
								bool flag = txtFill.Text == _003F487_003F._003F488_003F("");
								if ((int)/*Error near IL_02f7: Stack underflow*/ != 0)
								{
									string msgNotFillOther = SurveyMsg.MsgNotFillOther;
									string msgCaption2 = SurveyMsg.MsgCaption;
									MessageBox.Show((string)/*Error near IL_00ae: Stack underflow*/, (string)/*Error near IL_00ae: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
									txtFill.Focus();
									return;
								}
								oQuestion.FillText = txtFill.Text;
							}
						}
					}
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
						if ((int)/*Error near IL_0320: Stack underflow*/ == 0)
						{
							MessageBox.Show(((PageSingleList)/*Error near IL_01f5: Stack underflow*/).oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							return;
						}
						if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
						{
							return;
						}
					}
					goto IL_024f;
				}
			}
			MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			return;
			IL_032c:
			goto IL_024f;
			IL_024f:
			oQuestion.BeforeSave();
			oQuestion.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_0288: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Asterisk);
			}
			MyNav.PageAnswer = list;
			_003F81_003F();
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0002ūɘ\u0359ѐԆ٤\u0747ࡕ\u094dਘ\u0b41\u0c4e\u0d4d\u0e6f\u0f71\u1073ᅹት፮ᐶᕮᙾ\u1773ᡢ\u193b\u1a63\u1b73ᱶᵵṼὧ\u2063Ⅻ≧⍯⑥╡♴❲⠫⥼⩢⭯Ɑ"), UriKind.Relative);
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
				((PageSingleList)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				wrapPanel1 = (WrapPanel)_003F350_003F;
				break;
			case 3:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				cmbSelect = (ComboBox)_003F350_003F;
				break;
			case 5:
				stackPanel1 = (StackPanel)_003F350_003F;
				break;
			case 6:
				txtFillTitle = (TextBlock)_003F350_003F;
				break;
			case 7:
				txtFill = (TextBox)_003F350_003F;
				txtFill.GotFocus += _003F91_003F;
				txtFill.LostFocus += _003F90_003F;
				break;
			case 8:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 9:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 10:
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
