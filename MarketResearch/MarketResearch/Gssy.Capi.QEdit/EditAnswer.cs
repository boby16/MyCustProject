using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Gssy.Capi.QEdit
{
	public class EditAnswer : Window, IComponentConnector
	{
		private List<SurveyDetail> lOptions = new List<SurveyDetail>();

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

		private bool _contentLoaded;

		public EditAnswer()
		{
			InitializeComponent();
		}

		private void _003F24_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_006d: Incompatible stack heights: 0 vs 3
			base.Topmost = true;
			Hide();
			Show();
			txtQuestionTitle.Text = SurveyHelper.QueryEditQTitle;
			txtTitle.Text = SurveyHelper.QueryEditQName;
			if (SurveyHelper.QueryEditDetailID != _003F487_003F._003F488_003F(""))
			{
				SurveyDetailDal surveyDetailDal = new SurveyDetailDal();
				string queryEditDetailID = SurveyHelper.QueryEditDetailID;
				List<SurveyDetail> details = ((SurveyDetailDal)/*Error near IL_0051: Stack underflow*/).GetDetails((string)/*Error near IL_0051: Stack underflow*/);
				((EditAnswer)/*Error near IL_0072: Stack underflow*/).lOptions = details;
				ListSelect.ItemsSource = lOptions;
				ListSelect.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
				base.Height = 450.0;
				ListSelect.Visibility = Visibility.Visible;
				txtAnswer.Text = SurveyHelper.QueryEditCODE + _003F487_003F._003F488_003F("）") + SurveyHelper.QueryEditCODE_TEXT + _003F487_003F._003F488_003F("（");
				txtNewAnswer.IsEnabled = false;
				ListSelect.Focus();
			}
			else
			{
				ListSelect.Visibility = Visibility.Collapsed;
				base.Height = 350.0;
				txtAnswer.Text = SurveyHelper.QueryEditCODE;
				txtNewAnswer.Focus();
			}
		}

		private void _003F128_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			Close();
		}

		private void _003F211_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			string text = txtNewAnswer.Text;
			if (text == _003F487_003F._003F488_003F(""))
			{
				MessageBox.Show(SurveyMsg.MsgNotFill, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else
			{
				SurveyHelper.QueryEditCODE = text;
				if (SurveyHelper.QueryEditDetailID != _003F487_003F._003F488_003F(""))
				{
					text = ListSelect.SelectedValue.ToString();
					foreach (SurveyDetail lOption in lOptions)
					{
						if (text == lOption.CODE)
						{
							SurveyHelper.QueryEditCODE_TEXT = lOption.CODE_TEXT;
							break;
						}
					}
				}
				if (!SurveyHelper.QueryEditMemModel)
				{
					new SurveyAnswerDal().AddOne(SurveyHelper.QueryEditSurveyId, SurveyHelper.QueryEditQName, text, SurveyHelper.QueryEditSequence);
				}
				Logging.Data.WriteLog(_003F487_003F._003F488_003F("柬諪䷩昿嚚藹拎䡞࠻"), SurveyHelper.QueryEditSurveyId + _003F487_003F._003F488_003F("$Įȯ\u0321") + SurveyHelper.QueryEditQName + _003F487_003F._003F488_003F("$Įȯ\u0321") + txtAnswer.Text + _003F487_003F._003F488_003F("%ĩȮ\u033cС") + text);
				SurveyHelper.QueryEditConfirm = true;
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
					((EditAnswer)/*Error near IL_0016: Stack underflow*/)._003F211_003F((object)/*Error near IL_0016: Stack underflow*/, (RoutedEventArgs)/*Error near IL_0016: Stack underflow*/);
				}
			}
		}

		private void _003F256_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F)
		{
			txtNewAnswer.Text = (string)ListSelect.SelectedValue;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0005Ůɛ\u0354џԋ٧\u0742ࡒ\u0948ਛ\u0b7c\u0c71൰\u0e6c\u0f74\u1074ᅼቶ፣ᐹᕤᙱ\u1777\u187bᥥᨿ᭪ᱪ\u1d64ṸὪ\u2064ⅺ≿⍢⑴┫♼❢⡯⥭"), UriKind.Relative);
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
				((EditAnswer)_003F350_003F).Loaded += _003F24_003F;
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
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
