using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
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

namespace Gssy.Capi.QEdit
{
	public class EditAnswerSingle : Window, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__27_0;

			internal int _003F334_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string EditQn = _003F487_003F._003F488_003F("");

		private string EditVar = _003F487_003F._003F488_003F("");

		private string EditTitle = _003F487_003F._003F488_003F("");

		private SurveyDefine QDefine;

		private SurveyDefine QCircleDefineA;

		private SurveyDefine QCircleDefineB;

		private string AnswerCode = _003F487_003F._003F488_003F("");

		private int iSeq = 9990;

		private string CodeText = _003F487_003F._003F488_003F("");

		private string CircleTextA = _003F487_003F._003F488_003F("");

		private string CircleTextB = _003F487_003F._003F488_003F("");

		private List<SurveyDetail> QDetails = new List<SurveyDetail>();

		private List<SurveyDetail> QCircleDetailsA;

		private List<SurveyDetail> QCircleDetailsB;

		private BoldTitle oBoldTitle = new BoldTitle();

		public LogicEngine oLogicEngine = new LogicEngine();

		public LogicExplain oLogicExplain = new LogicExplain();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtTitle;

		internal Grid gridContent;

		internal TextBlock txtAnswerTitle;

		internal TextBox txtAnswer;

		internal TextBlock txtNewAnswerTitle;

		internal TextBox txtNewAnswer;

		internal ListBox ListSelect;

		internal Button btnSave;

		internal Button btnCancel;

		internal Button btnKeyboard;

		private bool _contentLoaded;

		public EditAnswerSingle()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			base.Topmost = true;
			Hide();
			Show();
			MySurveyId = SurveyHelper.SurveyID;
			SurveyHelper.QueryEditQn = oLogicEngine.ReplaceSpecialFlag(SurveyHelper.QueryEditQn);
			List<string> list = oBoldTitle.ParaToList(SurveyHelper.QueryEditQn, _003F487_003F._003F488_003F("-Į"));
			if (list.Count == 0)
			{
				MessageBox.Show(SurveyMsg.MsgNoModifyQSet, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				Close();
			}
			else
			{
				EditQn = list[0];
				EditVar = EditQn;
				if (list.Count > 1)
				{
					EditVar = EditVar + _003F487_003F._003F488_003F("]œ") + list[1];
				}
				if (list.Count > 2)
				{
					EditVar = EditVar + _003F487_003F._003F488_003F("]œ") + list[2];
				}
				QDefine = oSurveyDefineDal.GetByName(EditQn);
				if (QDefine.DETAIL_ID != _003F487_003F._003F488_003F(""))
				{
					QDetails = oSurveyDetailDal.GetDetails(QDefine.DETAIL_ID);
				}
				List<SurveyDetail>.Enumerator enumerator;
				if (list.Count > 1 && QDefine.GROUP_CODEA != _003F487_003F._003F488_003F(""))
				{
					QCircleDefineA = oSurveyDefineDal.GetByName(QDefine.GROUP_CODEA);
					if (QCircleDefineA.DETAIL_ID != _003F487_003F._003F488_003F(""))
					{
						QCircleDetailsA = oSurveyDetailDal.GetDetails(QCircleDefineA.DETAIL_ID);
					}
					if (list.Count > 0)
					{
						enumerator = QCircleDetailsA.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail current = enumerator.Current;
								if (current.CODE == list[1])
								{
									CircleTextA = current.CODE_TEXT;
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
				if (list.Count > 2 && QDefine.GROUP_CODEB != _003F487_003F._003F488_003F(""))
				{
					QCircleDefineB = oSurveyDefineDal.GetByName(QDefine.GROUP_CODEB);
					if (QCircleDefineB.DETAIL_ID != _003F487_003F._003F488_003F(""))
					{
						QCircleDetailsB = oSurveyDetailDal.GetDetails(QCircleDefineB.DETAIL_ID);
					}
					if (list.Count > 1)
					{
						enumerator = QCircleDetailsB.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail current2 = enumerator.Current;
								if (current2.CODE == list[2])
								{
									CircleTextB = current2.CODE_TEXT;
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
				EditTitle = QDefine.SPSS_TITLE;
				txtQuestionTitle.Text = EditTitle;
				txtTitle.Text = EditVar;
				if (CircleTextA != _003F487_003F._003F488_003F(""))
				{
					TextBlock textBlock = txtTitle;
					textBlock.Text = textBlock.Text + _003F487_003F._003F488_003F("#įȡ") + CircleTextA;
				}
				if (CircleTextB != _003F487_003F._003F488_003F(""))
				{
					TextBlock textBlock2 = txtTitle;
					textBlock2.Text = textBlock2.Text + _003F487_003F._003F488_003F("#įȡ") + CircleTextB;
				}
				SurveyAnswer one = oSurveyAnswerDal.GetOne(MySurveyId, EditVar);
				AnswerCode = one.CODE;
				iSeq = one.SEQUENCE_ID;
				txtAnswer.Text = AnswerCode;
				if (QDefine.DETAIL_ID != _003F487_003F._003F488_003F(""))
				{
					enumerator = QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current3 = enumerator.Current;
							if (current3.CODE == AnswerCode)
							{
								CodeText = current3.CODE_TEXT;
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					if (CodeText != _003F487_003F._003F488_003F(""))
					{
						txtAnswer.Text = AnswerCode + _003F487_003F._003F488_003F("）") + CodeText + _003F487_003F._003F488_003F("（");
					}
					_003F257_003F();
					ListSelect.ItemsSource = QDetails;
					ListSelect.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
					ListSelect.Visibility = Visibility.Visible;
					txtNewAnswer.IsEnabled = false;
					ListSelect.Focus();
				}
				else
				{
					ListSelect.Visibility = Visibility.Collapsed;
					txtNewAnswer.Focus();
				}
			}
		}

		private void _003F128_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Close();
		}

		private void _003F211_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_00f4: Incompatible stack heights: 0 vs 1
			//IL_010e: Incompatible stack heights: 0 vs 1
			//IL_0122: Incompatible stack heights: 0 vs 1
			string text = txtNewAnswer.Text;
			if (text == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else
			{
				oSurveyAnswerDal.AddOne(MySurveyId, EditVar, text, iSeq);
				Logging.Data.WriteLog(_003F487_003F._003F488_003F("逵躜櫌擮懼杂寭䣩洿牑扌濎䍞\u0d3b"), MySurveyId + _003F487_003F._003F488_003F("$Įȯ\u0321") + EditVar + _003F487_003F._003F488_003F("$Įȯ\u0321") + AnswerCode + _003F487_003F._003F488_003F("%ĩȮ\u033cС") + text);
				if (SurveyMsg.OutputHistory == _003F487_003F._003F488_003F("]ŤɤͿѻչلݢ\u0879ॽ੧୵౿\u0d5a\u0e70\u0f71ၷᅤ"))
				{
					string text2 = _003F487_003F._003F488_003F("");
					string answer2 = SurveyHelper.Answer;
					string b = _003F487_003F._003F488_003F("");
					if ((string)/*Error near IL_00ce: Stack underflow*/ != b)
					{
						_003F487_003F._003F488_003F("4ĳȲ\u0331аԯخܭ\u082cफ\u0a4b୧౻൰\u0e63\u0f77ဤᄹሢፚ");
						text2 = string.Concat(str1: SurveyHelper.Answer, str2: _003F487_003F._003F488_003F("\\"), str3: Environment.NewLine, str0: (string)/*Error near IL_013b: Stack underflow*/);
					}
					text2 = text2 + DateTime.Now.ToString() + _003F487_003F._003F488_003F("=ĦɆ\u0365Ѱէؼ") + MySurveyId + _003F487_003F._003F488_003F("$ħɕ\u0360ѵՊن\u073c") + iSeq + _003F487_003F._003F488_003F("*ĥɒ\u0366ѰԼ") + SurveyHelper.RoadMapVersion + _003F487_003F._003F488_003F(")Ĥɒ\u036cм") + EditVar + _003F487_003F._003F488_003F("9Ĵɕͽѣսفݯ\u0860३ਸ਼\u0b4f౭ൡ\u0e73ཇ\u106bᅷቴ፧ᑳ") + Environment.NewLine + _003F487_003F._003F488_003F("4ĳȲ\u0331аԯخܭ\u082cफ\u0a4b୧౻൰\u0e63\u0f77ဤᄹሢፚ") + text + _003F487_003F._003F488_003F("\\");
					oLogicExplain.OutputResult(text2, _003F487_003F._003F488_003F("@Ůɵͱѫձٻݞ") + SurveyHelper.SurveyID + _003F487_003F._003F488_003F("*ŏɭ\u0366"), true);
				}
				Close();
			}
		}

		private void _003F254_003F(object _003F347_003F, KeyEventArgs _003F348_003F)
		{
			//IL_002c: Incompatible stack heights: 0 vs 1
			//IL_0039: Incompatible stack heights: 0 vs 3
			if (_003F348_003F.Key == Key.Return)
			{
				bool isEnabled = btnSave.IsEnabled;
				if ((int)/*Error near IL_0031: Stack underflow*/ != 0)
				{
					((EditAnswerSingle)/*Error near IL_0016: Stack underflow*/)._003F211_003F((object)/*Error near IL_0016: Stack underflow*/, (RoutedEventArgs)/*Error near IL_0016: Stack underflow*/);
				}
			}
		}

		private void _003F256_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F)
		{
			txtNewAnswer.Text = (string)ListSelect.SelectedValue;
		}

		private void _003F257_003F()
		{
			//IL_014f: Incompatible stack heights: 0 vs 2
			//IL_0166: Incompatible stack heights: 0 vs 1
			List<SurveyDetail> list = new List<SurveyDetail>();
			List<SurveyDetail>.Enumerator enumerator;
			if (QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				string[] array = oLogicEngine.aryCode(QDefine.LIMIT_LOGIC, ',');
				list = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					enumerator = QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current = enumerator.Current;
							if (current.CODE == array[i].ToString())
							{
								list.Add(current);
								break;
							}
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				if (QDefine.SHOW_LOGIC == _003F487_003F._003F488_003F("") && QDefine.IS_RANDOM == 0)
				{
					if (_003F7_003F._003C_003E9__27_0 == null)
					{
						_003F7_003F._003C_003E9__27_0 = _003F7_003F._003C_003E9._003F334_003F;
					}
					((List<SurveyDetail>)/*Error near IL_016b: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_016b: Stack underflow*/);
				}
				QDetails = list;
			}
			if (QDefine.DETAIL_ID.Substring(0, 1) == _003F487_003F._003F488_003F("\""))
			{
				for (int j = 0; j < QDetails.Count(); j++)
				{
					QDetails[j].CODE_TEXT = oBoldTitle.ReplaceABTitle(QDetails[j].CODE_TEXT);
				}
			}
			enumerator = QDetails.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					SurveyDetail current2 = enumerator.Current;
					if (current2.CODE != AnswerCode)
					{
						list.Add(current2);
					}
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			QDetails = list;
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

		private void _003F253_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			SurveyTaptip.ShowInputPanel();
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u001fŨɝ\u035eѕԅ٩\u0748ࡘ\u094eਝ\u0b46\u0c4bൎ๒ཎ၎ᅺተ፩ᐳᕪᙿ\u177dᡱᥣᨹ\u1b70ᱰᵺṦὰ⁾ⅼ≹⍨⑾╸♣❧⡯⥫⩣⬫\u2c7cⵢ\u2e6f⽭"), UriKind.Relative);
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
				((EditAnswerSingle)_003F350_003F).Loaded += _003F24_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				txtTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				gridContent = (Grid)_003F350_003F;
				break;
			case 5:
				txtAnswerTitle = (TextBlock)_003F350_003F;
				break;
			case 6:
				txtAnswer = (TextBox)_003F350_003F;
				break;
			case 7:
				txtNewAnswerTitle = (TextBlock)_003F350_003F;
				break;
			case 8:
				txtNewAnswer = (TextBox)_003F350_003F;
				txtNewAnswer.GotFocus += _003F91_003F;
				txtNewAnswer.LostFocus += _003F90_003F;
				txtNewAnswer.KeyDown += _003F254_003F;
				break;
			case 9:
				ListSelect = (ListBox)_003F350_003F;
				ListSelect.SelectionChanged += _003F256_003F;
				break;
			case 10:
				btnSave = (Button)_003F350_003F;
				btnSave.Click += _003F211_003F;
				break;
			case 11:
				btnCancel = (Button)_003F350_003F;
				btnCancel.Click += _003F128_003F;
				break;
			case 12:
				btnKeyboard = (Button)_003F350_003F;
				btnKeyboard.Click += _003F253_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
