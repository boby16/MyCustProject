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
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace Gssy.Capi.View
{
	public class RelationList : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__8_0;

			internal int _003F331_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private QDisplay oQuestion = new QDisplay();

		private QSingle oQSingle1 = new QSingle();

		private QSingle oQSingle2 = new QSingle();

		internal TextBlock txtQuestionTitle;

		internal Grid GridContent;

		internal TextBlock textBlock1;

		internal ComboBox cmbSelect1;

		internal TextBlock txtOther1;

		internal TextBox txtFillOther1;

		internal TextBlock textBlock2;

		internal ComboBox cmbSelect2;

		internal TextBlock txtOther2;

		internal TextBox txtFillOther2;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public RelationList()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0319: Incompatible stack heights: 0 vs 2
			//IL_0330: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			oQuestion.Init(CurPageId, 0);
			oQSingle1.Init(CurPageId, 1, true);
			oQSingle2.Init(CurPageId, 2, true);
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
			if (oQSingle1.QDefine.LIMIT_LOGIC != _003F487_003F._003F488_003F(""))
			{
				oLogicEngine.SurveyID = MySurveyId;
				string[] array = oLogicEngine.aryCode(oQSingle1.QDefine.LIMIT_LOGIC, ',');
				List<SurveyDetail> list = new List<SurveyDetail>();
				for (int i = 0; i < array.Count(); i++)
				{
					foreach (SurveyDetail qDetail in oQSingle1.QDetails)
					{
						if (qDetail.CODE == array[i].ToString())
						{
							list.Add(qDetail);
							break;
						}
					}
				}
				if (_003F7_003F._003C_003E9__8_0 == null)
				{
					_003F7_003F._003C_003E9__8_0 = _003F7_003F._003C_003E9._003F331_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0335: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0335: Stack underflow*/);
				oQSingle1.QDetails = list;
			}
			textBlock1.Text = oQSingle1.QDefine.QUESTION_TITLE;
			textBlock2.Text = oQSingle2.QDefine.QUESTION_TITLE;
			cmbSelect1.ItemsSource = oQSingle1.QDetails;
			cmbSelect1.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			if (oQSingle2.QDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))
			{
				cmbSelect2.ItemsSource = oQSingle2.QDetails;
				cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
				cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			}
			cmbSelect1.IsEditable = false;
			cmbSelect2.IsEditable = false;
			txtOther1.Visibility = Visibility.Hidden;
			txtOther2.Visibility = Visibility.Hidden;
			txtFillOther1.Visibility = Visibility.Hidden;
			txtFillOther2.Visibility = Visibility.Hidden;
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ") && oQuestion.QDefine.IS_ATTACH == 1)
			{
				btnAttach.Visibility = Visibility.Visible;
			}
			if (SurveyHelper.AutoFill)
			{
				AutoFill autoFill = new AutoFill();
				autoFill.oLogicEngine = oLogicEngine;
				cmbSelect1.SelectedValue = autoFill.SingleDetail(oQSingle1.QDefine, oQSingle1.QDetails).CODE;
				_003F148_003F(cmbSelect1, null);
				cmbSelect2.SelectedValue = autoFill.SingleDetail(oQSingle2.QDefine, oQSingle2.QDetails).CODE;
				_003F149_003F(cmbSelect2, null);
				if (txtFillOther1.IsEnabled)
				{
					txtFillOther1.Text = autoFill.CommonOther(oQSingle1.QDefine, _003F487_003F._003F488_003F(""));
				}
				if (txtFillOther2.IsEnabled)
				{
					txtFillOther2.Text = autoFill.CommonOther(oQSingle2.QDefine, _003F487_003F._003F488_003F(""));
				}
				if (autoFill.AutoNext(oQuestion.QDefine))
				{
					_003F58_003F(this, _003F348_003F);
				}
			}
			if (SurveyHelper.NavOperation == _003F487_003F._003F488_003F("FŢɡ\u036a"))
			{
				cmbSelect1.SelectedValue = oQSingle1.ReadAnswerByQuestionName(MySurveyId, oQSingle1.QuestionName);
				cmbSelect2.SelectedValue = oQSingle2.ReadAnswerByQuestionName(MySurveyId, oQSingle2.QuestionName);
				txtFillOther1.Text = oQSingle1.ReadAnswerByQuestionName(MySurveyId, oQSingle1.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				txtFillOther2.Text = oQSingle2.ReadAnswerByQuestionName(MySurveyId, oQSingle2.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_03c6: Incompatible stack heights: 0 vs 1
			//IL_03e0: Incompatible stack heights: 0 vs 1
			//IL_0409: Incompatible stack heights: 0 vs 2
			//IL_041e: Incompatible stack heights: 0 vs 1
			//IL_042e: Incompatible stack heights: 0 vs 1
			//IL_0443: Incompatible stack heights: 0 vs 3
			//IL_044e: Incompatible stack heights: 0 vs 1
			//IL_0468: Incompatible stack heights: 0 vs 1
			//IL_0482: Incompatible stack heights: 0 vs 2
			//IL_0496: Incompatible stack heights: 0 vs 2
			//IL_04ab: Incompatible stack heights: 0 vs 1
			//IL_04bb: Incompatible stack heights: 0 vs 1
			//IL_04db: Incompatible stack heights: 0 vs 2
			List<VEAnswer> list;
			if (cmbSelect1.SelectedValue != null)
			{
				object selectedValue = cmbSelect1.SelectedValue;
				if (!((string)/*Error near IL_0011: Stack underflow*/ == _003F487_003F._003F488_003F("")))
				{
					if (cmbSelect2.SelectedValue != null)
					{
						ComboBox cmbSelect3 = cmbSelect2;
						if (!((string)((Selector)/*Error near IL_0052: Stack underflow*/).SelectedValue == _003F487_003F._003F488_003F("")))
						{
							oQSingle1.FillText = _003F487_003F._003F488_003F("");
							oQSingle2.FillText = _003F487_003F._003F488_003F("");
							if (oQSingle1.OtherCode != null)
							{
								string otherCode2 = oQSingle1.OtherCode;
								_003F487_003F._003F488_003F("");
								if ((string)/*Error near IL_00be: Stack underflow*/ != (string)/*Error near IL_00be: Stack underflow*/)
								{
									object selectedValue2 = cmbSelect1.SelectedValue;
									if (((object)/*Error near IL_00c8: Stack underflow*/).ToString() == oQSingle1.OtherCode)
									{
										TextBox txtFillOther3 = txtFillOther1;
										if (((TextBox)/*Error near IL_00e2: Stack underflow*/).Text == _003F487_003F._003F488_003F(""))
										{
											string msgNotFillOther = SurveyMsg.MsgNotFillOther;
											string msgCaption = SurveyMsg.MsgCaption;
											MessageBox.Show((string)/*Error near IL_00fd: Stack underflow*/, (string)/*Error near IL_00fd: Stack underflow*/, (MessageBoxButton)/*Error near IL_00fd: Stack underflow*/, MessageBoxImage.Hand);
											txtFillOther1.Focus();
											return;
										}
										oQSingle1.FillText = txtFillOther1.Text;
									}
								}
							}
							if (oQSingle2.OtherCode != null && ((RelationList)/*Error near IL_0136: Stack underflow*/).oQSingle2.OtherCode != _003F487_003F._003F488_003F(""))
							{
								cmbSelect2.SelectedValue.ToString();
								string otherCode = oQSingle2.OtherCode;
								if ((string)/*Error near IL_015f: Stack underflow*/ == otherCode)
								{
									string text = txtFillOther2.Text;
									string b = _003F487_003F._003F488_003F((string)/*Error near IL_0169: Stack underflow*/);
									if ((string)/*Error near IL_016e: Stack underflow*/ == b)
									{
										string msgNotFillOther2 = SurveyMsg.MsgNotFillOther;
										string msgCaption2 = SurveyMsg.MsgCaption;
										MessageBox.Show((string)/*Error near IL_017b: Stack underflow*/, (string)/*Error near IL_017b: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
										txtFillOther2.Focus();
										return;
									}
									oQSingle2.FillText = txtFillOther2.Text;
								}
							}
							oQSingle1.SelectedCode = cmbSelect1.SelectedValue.ToString();
							oQSingle2.SelectedCode = cmbSelect2.SelectedValue.ToString();
							list = new List<VEAnswer>();
							VEAnswer vEAnswer = new VEAnswer();
							vEAnswer.QUESTION_NAME = oQSingle1.QuestionName;
							vEAnswer.CODE = oQSingle1.SelectedCode;
							list.Add(vEAnswer);
							SurveyHelper.Answer = oQSingle1.QuestionName + _003F487_003F._003F488_003F("<") + oQSingle1.SelectedCode;
							VEAnswer vEAnswer2 = new VEAnswer();
							vEAnswer2.QUESTION_NAME = oQSingle2.QuestionName;
							vEAnswer2.CODE = oQSingle2.SelectedCode;
							list.Add(vEAnswer2);
							SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F(".ġ") + oQSingle2.QuestionName + _003F487_003F._003F488_003F("<") + oQSingle2.SelectedCode;
							oLogicEngine.PageAnswer = list;
							oLogicEngine.SurveyID = MySurveyId;
							if (!oLogicEngine.CheckLogic(CurPageId))
							{
								int iS_ALLOW_PASS = oLogicEngine.IS_ALLOW_PASS;
								if ((int)/*Error near IL_04b0: Stack underflow*/ == 0)
								{
									LogicEngine oLogicEngine2 = oLogicEngine;
									MessageBox.Show(((LogicEngine)/*Error near IL_02ee: Stack underflow*/).Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
									return;
								}
								if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
								{
									return;
								}
							}
							goto IL_0343;
						}
					}
					MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
			}
			MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			return;
			IL_04c1:
			goto IL_0343;
			IL_0343:
			oQSingle1.BeforeSave();
			oQSingle1.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			oQSingle2.BeforeSave();
			oQSingle2.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				string msgCaption3 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_0399: Stack underflow*/, (string)/*Error near IL_0399: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Asterisk);
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

		private void _003F148_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			//IL_0140: Incompatible stack heights: 0 vs 1
			//IL_0155: Incompatible stack heights: 0 vs 2
			//IL_016a: Incompatible stack heights: 0 vs 1
			//IL_0175: Incompatible stack heights: 0 vs 1
			//IL_018f: Incompatible stack heights: 0 vs 1
			//IL_019f: Incompatible stack heights: 0 vs 1
			//IL_01b0: Incompatible stack heights: 0 vs 2
			//IL_01bb: Incompatible stack heights: 0 vs 1
			if (cmbSelect1.SelectedValue != null)
			{
				object selectedValue = cmbSelect1.SelectedValue;
				string text = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (text != null)
				{
					_003F487_003F._003F488_003F("");
					if ((string)/*Error near IL_0021: Stack underflow*/ != (string)/*Error near IL_0021: Stack underflow*/)
					{
						SurveyDefine qDefine = oQSingle2.QDefine;
						if (((SurveyDefine)/*Error near IL_002b: Stack underflow*/).PARENT_CODE != _003F487_003F._003F488_003F("") && (int)/*Error near IL_017a: Stack underflow*/ != 0)
						{
							bool flag = text != _003F487_003F._003F488_003F("");
							if ((int)/*Error near IL_0194: Stack underflow*/ != 0)
							{
								QSingle oQSingle3 = oQSingle2;
								((QSingle)/*Error near IL_004f: Stack underflow*/).ParentCode = text;
								oQSingle2.GetDynamicDetails();
								cmbSelect2.ItemsSource = oQSingle2.QDetails;
								cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
								cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
							}
						}
						cmbSelect2.ItemsSource = oQSingle2.QDetails;
						cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
						cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
					}
				}
				if (oQSingle1.OtherCode != _003F487_003F._003F488_003F(""))
				{
					QSingle oQSingle4 = oQSingle1;
					string otherCode = ((QSingle)/*Error near IL_00fe: Stack underflow*/).OtherCode;
					if ((string)/*Error near IL_0103: Stack underflow*/ == otherCode)
					{
						((RelationList)/*Error near IL_010d: Stack underflow*/).txtOther1.Visibility = Visibility.Visible;
						txtFillOther1.Visibility = Visibility.Visible;
					}
					else
					{
						txtOther1.Visibility = Visibility.Hidden;
						txtFillOther1.Visibility = Visibility.Hidden;
					}
				}
			}
		}

		private void _003F149_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			//IL_006a: Incompatible stack heights: 0 vs 1
			//IL_007b: Incompatible stack heights: 0 vs 2
			//IL_008c: Incompatible stack heights: 0 vs 2
			if (cmbSelect2.SelectedValue != null)
			{
				object selectedValue = cmbSelect2.SelectedValue;
				string text = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (oQSingle2.OtherCode != _003F487_003F._003F488_003F(""))
				{
					QSingle oQSingle3 = oQSingle2;
					string otherCode = ((QSingle)/*Error near IL_003a: Stack underflow*/).OtherCode;
					if ((string)/*Error near IL_003f: Stack underflow*/ == otherCode)
					{
						TextBlock txtOther3 = txtOther2;
						((UIElement)/*Error near IL_0049: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0049: Stack underflow*/;
						txtFillOther2.Visibility = Visibility.Visible;
					}
					else
					{
						txtOther2.Visibility = Visibility.Hidden;
						txtFillOther2.Visibility = Visibility.Hidden;
					}
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
			SurveyHelper.AttachQName = oQSingle1.QuestionName;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0004ŭɚ\u035bўԈ٦\u0745ࡓ\u094bਚ\u0b43\u0c70൳\u0e6d\u0f73ၵᅿቷ፬ᐸᕠᙼ\u1771ᡤ\u193d\u1a63᭵ᱣᵯṹὥ\u2064Ⅴ≥⍡⑴╲☫❼⡢⥯⩭"), UriKind.Relative);
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
				((RelationList)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 3:
				GridContent = (Grid)_003F350_003F;
				break;
			case 4:
				textBlock1 = (TextBlock)_003F350_003F;
				break;
			case 5:
				cmbSelect1 = (ComboBox)_003F350_003F;
				cmbSelect1.SelectionChanged += _003F148_003F;
				break;
			case 6:
				txtOther1 = (TextBlock)_003F350_003F;
				break;
			case 7:
				txtFillOther1 = (TextBox)_003F350_003F;
				txtFillOther1.GotFocus += _003F91_003F;
				txtFillOther1.LostFocus += _003F90_003F;
				break;
			case 8:
				textBlock2 = (TextBlock)_003F350_003F;
				break;
			case 9:
				cmbSelect2 = (ComboBox)_003F350_003F;
				cmbSelect2.SelectionChanged += _003F149_003F;
				break;
			case 10:
				txtOther2 = (TextBlock)_003F350_003F;
				break;
			case 11:
				txtFillOther2 = (TextBox)_003F350_003F;
				txtFillOther2.GotFocus += _003F91_003F;
				txtFillOther2.LostFocus += _003F90_003F;
				break;
			case 12:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 13:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 14:
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
