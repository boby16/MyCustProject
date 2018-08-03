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
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace Gssy.Capi.View
{
	public class RelationList3 : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__9_0;

			internal int _003F330_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
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

		private QSingle oQSingle3 = new QSingle();

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

		internal TextBlock textBlock3;

		internal ComboBox cmbSelect3;

		internal TextBlock txtOther3;

		internal TextBox txtFillOther3;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public RelationList3()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_032c: Incompatible stack heights: 0 vs 2
			//IL_0343: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			oQuestion.Init(CurPageId, 0);
			oQSingle1.Init(CurPageId, 1, true);
			oQSingle2.Init(CurPageId, 2, true);
			oQSingle3.Init(CurPageId, 3, true);
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
				if (_003F7_003F._003C_003E9__9_0 == null)
				{
					_003F7_003F._003C_003E9__9_0 = _003F7_003F._003C_003E9._003F330_003F;
				}
				((List<SurveyDetail>)/*Error near IL_0348: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_0348: Stack underflow*/);
				oQSingle1.QDetails = list;
			}
			textBlock1.Text = oQSingle1.QDefine.QUESTION_TITLE;
			textBlock2.Text = oQSingle2.QDefine.QUESTION_TITLE;
			textBlock3.Text = oQSingle3.QDefine.QUESTION_TITLE;
			cmbSelect1.ItemsSource = oQSingle1.QDetails;
			cmbSelect1.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
			cmbSelect1.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			if (oQSingle2.QDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))
			{
				cmbSelect2.ItemsSource = oQSingle2.QDetails;
				cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
				cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			}
			if (oQSingle3.QDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))
			{
				cmbSelect3.ItemsSource = oQSingle3.QDetails;
				cmbSelect3.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
				cmbSelect3.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			}
			cmbSelect1.IsEditable = false;
			cmbSelect2.IsEditable = false;
			cmbSelect3.IsEditable = false;
			txtOther1.Visibility = Visibility.Hidden;
			txtOther2.Visibility = Visibility.Hidden;
			txtOther3.Visibility = Visibility.Hidden;
			txtFillOther1.Visibility = Visibility.Hidden;
			txtFillOther2.Visibility = Visibility.Hidden;
			txtFillOther3.Visibility = Visibility.Hidden;
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
				cmbSelect3.SelectedValue = autoFill.SingleDetail(oQSingle3.QDefine, oQSingle3.QDetails).CODE;
				_003F167_003F(cmbSelect3, null);
				if (txtFillOther1.IsEnabled)
				{
					txtFillOther1.Text = autoFill.CommonOther(oQSingle1.QDefine, _003F487_003F._003F488_003F(""));
				}
				if (txtFillOther2.IsEnabled)
				{
					txtFillOther2.Text = autoFill.CommonOther(oQSingle2.QDefine, _003F487_003F._003F488_003F(""));
				}
				if (txtFillOther3.IsEnabled)
				{
					txtFillOther3.Text = autoFill.CommonOther(oQSingle3.QDefine, _003F487_003F._003F488_003F(""));
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
				cmbSelect3.SelectedValue = oQSingle3.ReadAnswerByQuestionName(MySurveyId, oQSingle3.QuestionName);
				txtFillOther1.Text = oQSingle1.ReadAnswerByQuestionName(MySurveyId, oQSingle1.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				txtFillOther2.Text = oQSingle2.ReadAnswerByQuestionName(MySurveyId, oQSingle2.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				txtFillOther3.Text = oQSingle3.ReadAnswerByQuestionName(MySurveyId, oQSingle3.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0522: Incompatible stack heights: 0 vs 1
			//IL_053c: Incompatible stack heights: 0 vs 1
			//IL_0565: Incompatible stack heights: 0 vs 2
			//IL_058e: Incompatible stack heights: 0 vs 2
			//IL_05a3: Incompatible stack heights: 0 vs 1
			//IL_05ae: Incompatible stack heights: 0 vs 1
			//IL_05c5: Incompatible stack heights: 0 vs 4
			//IL_05df: Incompatible stack heights: 0 vs 2
			//IL_05ef: Incompatible stack heights: 0 vs 1
			//IL_0604: Incompatible stack heights: 0 vs 1
			//IL_0618: Incompatible stack heights: 0 vs 2
			//IL_0637: Incompatible stack heights: 0 vs 2
			//IL_0652: Incompatible stack heights: 0 vs 2
			//IL_065d: Incompatible stack heights: 0 vs 1
			//IL_0679: Incompatible stack heights: 0 vs 1
			//IL_068e: Incompatible stack heights: 0 vs 1
			//IL_06b0: Incompatible stack heights: 0 vs 1
			//IL_06cb: Incompatible stack heights: 0 vs 1
			List<VEAnswer> list;
			if (cmbSelect1.SelectedValue != null)
			{
				object selectedValue = cmbSelect1.SelectedValue;
				if (!((string)/*Error near IL_0011: Stack underflow*/ == _003F487_003F._003F488_003F("")))
				{
					if (cmbSelect2.SelectedValue != null)
					{
						ComboBox cmbSelect4 = cmbSelect2;
						if (!((string)((Selector)/*Error near IL_0052: Stack underflow*/).SelectedValue == _003F487_003F._003F488_003F("")))
						{
							if (cmbSelect3.SelectedValue != null)
							{
								string text = (string)cmbSelect3.SelectedValue;
								string b = _003F487_003F._003F488_003F((string)/*Error near IL_0094: Stack underflow*/);
								if (!((string)/*Error near IL_0099: Stack underflow*/ == b))
								{
									oQSingle1.FillText = _003F487_003F._003F488_003F("");
									oQSingle2.FillText = _003F487_003F._003F488_003F("");
									oQSingle3.FillText = _003F487_003F._003F488_003F("");
									if (oQSingle1.OtherCode != null)
									{
										string otherCode2 = oQSingle1.OtherCode;
										_003F487_003F._003F488_003F("");
										if ((string)/*Error near IL_0106: Stack underflow*/ != (string)/*Error near IL_0106: Stack underflow*/)
										{
											object selectedValue2 = cmbSelect1.SelectedValue;
											if (((object)/*Error near IL_0110: Stack underflow*/).ToString() == oQSingle1.OtherCode)
											{
												if (((RelationList3)/*Error near IL_012a: Stack underflow*/).txtFillOther1.Text == _003F487_003F._003F488_003F(""))
												{
													string msgNotFillOther = SurveyMsg.MsgNotFillOther;
													string msgCaption2 = SurveyMsg.MsgCaption;
													MessageBox.Show((string)/*Error near IL_0148: Stack underflow*/, (string)/*Error near IL_0148: Stack underflow*/, (MessageBoxButton)/*Error near IL_0148: Stack underflow*/, (MessageBoxImage)/*Error near IL_0148: Stack underflow*/);
													txtFillOther1.Focus();
													return;
												}
												oQSingle1.FillText = txtFillOther1.Text;
											}
										}
									}
									if (oQSingle2.OtherCode != null)
									{
										string otherCode3 = oQSingle2.OtherCode;
										string b2 = _003F487_003F._003F488_003F((string)/*Error near IL_0181: Stack underflow*/);
										if ((string)/*Error near IL_0186: Stack underflow*/ != b2)
										{
											ComboBox cmbSelect5 = cmbSelect2;
											if (((Selector)/*Error near IL_0190: Stack underflow*/).SelectedValue.ToString() == oQSingle2.OtherCode)
											{
												string text2 = txtFillOther2.Text;
												string b3 = _003F487_003F._003F488_003F("");
												if ((string)/*Error near IL_01b9: Stack underflow*/ == b3)
												{
													string msgNotFillOther2 = SurveyMsg.MsgNotFillOther;
													string msgCaption3 = SurveyMsg.MsgCaption;
													MessageBox.Show((string)/*Error near IL_01c6: Stack underflow*/, (string)/*Error near IL_01c6: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
													txtFillOther2.Focus();
													return;
												}
												oQSingle2.FillText = txtFillOther2.Text;
											}
										}
									}
									if (oQSingle3.OtherCode != null)
									{
										string otherCode4 = oQSingle3.OtherCode;
										_003F487_003F._003F488_003F("");
										if ((string)/*Error near IL_01ff: Stack underflow*/ != (string)/*Error near IL_01ff: Stack underflow*/)
										{
											cmbSelect3.SelectedValue.ToString();
											string otherCode = ((RelationList3)/*Error near IL_0209: Stack underflow*/).oQSingle3.OtherCode;
											if ((string)/*Error near IL_0213: Stack underflow*/ == otherCode)
											{
												if (((RelationList3)/*Error near IL_021d: Stack underflow*/).txtFillOther3.Text == _003F487_003F._003F488_003F(""))
												{
													MessageBox.Show(SurveyMsg.MsgNotFillOther, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
													txtFillOther3.Focus();
													return;
												}
												oQSingle3.FillText = txtFillOther3.Text;
											}
										}
									}
									oQSingle1.SelectedCode = cmbSelect1.SelectedValue.ToString();
									oQSingle2.SelectedCode = cmbSelect2.SelectedValue.ToString();
									oQSingle3.SelectedCode = cmbSelect3.SelectedValue.ToString();
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
									VEAnswer vEAnswer3 = new VEAnswer();
									vEAnswer3.QUESTION_NAME = oQSingle3.QuestionName;
									vEAnswer3.CODE = oQSingle3.SelectedCode;
									list.Add(vEAnswer3);
									SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F(".ġ") + oQSingle3.QuestionName + _003F487_003F._003F488_003F("<") + oQSingle3.SelectedCode;
									oLogicEngine.PageAnswer = list;
									oLogicEngine.SurveyID = MySurveyId;
									if (!oLogicEngine.CheckLogic(CurPageId))
									{
										int iS_ALLOW_PASS = oLogicEngine.IS_ALLOW_PASS;
										if ((int)/*Error near IL_0693: Stack underflow*/ == 0)
										{
											MessageBox.Show(oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
											return;
										}
										if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
										{
											return;
										}
									}
									goto IL_0485;
								}
							}
							MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
							return;
						}
					}
					MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				}
			}
			MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			return;
			IL_06b6:
			goto IL_0485;
			IL_0485:
			oQSingle1.BeforeSave();
			oQSingle1.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			oQSingle2.BeforeSave();
			oQSingle2.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			oQSingle3.BeforeSave();
			oQSingle3.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			_003F163_003F(SurveyMsg.DelaySeconds);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				MessageBox.Show(caption: SurveyMsg.MsgCaption, messageBoxText: (string)/*Error near IL_06d0: Stack underflow*/, button: MessageBoxButton.OK, icon: MessageBoxImage.Asterisk);
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

		private void _003F163_003F(int _003F393_003F)
		{
			if (_003F393_003F == 0)
			{
				return;
			}
			goto IL_0016;
			IL_0016:
			btnNav.IsEnabled = false;
			btnNav.Content = _003F487_003F._003F488_003F("歨嘢䷔塐Ы軱簈圝\u082dबਯ");
			Logging.Data.WriteLog(MySurveyId, oQSingle1.QuestionName + _003F487_003F._003F488_003F("-") + cmbSelect1.SelectedValue.ToString());
			Logging.Data.WriteLog(MySurveyId, oQSingle2.QuestionName + _003F487_003F._003F488_003F("-") + cmbSelect2.SelectedValue.ToString());
			Logging.Data.WriteLog(MySurveyId, oQSingle3.QuestionName + _003F487_003F._003F488_003F("-") + cmbSelect3.SelectedValue.ToString());
			Thread.Sleep(_003F393_003F);
			btnNav.Content = _003F487_003F._003F488_003F("绣ģȢ緬");
			btnNav.IsEnabled = true;
			return;
			IL_0011:
			goto IL_0016;
		}

		private void _003F148_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			//IL_00e1: Incompatible stack heights: 0 vs 1
			//IL_00ec: Incompatible stack heights: 0 vs 1
			//IL_00f7: Incompatible stack heights: 0 vs 1
			//IL_0108: Incompatible stack heights: 0 vs 2
			//IL_0123: Incompatible stack heights: 0 vs 1
			//IL_0134: Incompatible stack heights: 0 vs 2
			if (cmbSelect1.SelectedValue != null)
			{
				object selectedValue = cmbSelect1.SelectedValue;
				string a = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (oQSingle2.QDefine.PARENT_CODE != _003F487_003F._003F488_003F("") && (int)/*Error near IL_00f1: Stack underflow*/ != 0)
				{
					string b = _003F487_003F._003F488_003F("");
					if ((string)/*Error near IL_004e: Stack underflow*/ != b)
					{
						QSingle oQSingle4 = oQSingle2;
						((QSingle)/*Error near IL_0058: Stack underflow*/).ParentCode = (string)/*Error near IL_0058: Stack underflow*/;
						oQSingle2.GetDynamicDetails();
						cmbSelect2.ItemsSource = oQSingle2.QDetails;
						cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
						cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
					}
				}
				if (oQSingle1.OtherCode != _003F487_003F._003F488_003F(""))
				{
					bool flag = a == oQSingle1.OtherCode;
					if ((int)/*Error near IL_0128: Stack underflow*/ != 0)
					{
						TextBlock txtOther4 = txtOther1;
						((UIElement)/*Error near IL_0139: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0139: Stack underflow*/;
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
			//IL_00e1: Incompatible stack heights: 0 vs 1
			//IL_00ec: Incompatible stack heights: 0 vs 1
			//IL_00f7: Incompatible stack heights: 0 vs 1
			//IL_0108: Incompatible stack heights: 0 vs 2
			//IL_0123: Incompatible stack heights: 0 vs 1
			//IL_0134: Incompatible stack heights: 0 vs 2
			if (cmbSelect2.SelectedValue != null)
			{
				object selectedValue = cmbSelect2.SelectedValue;
				string a = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (oQSingle3.QDefine.PARENT_CODE != _003F487_003F._003F488_003F("") && (int)/*Error near IL_00f1: Stack underflow*/ != 0)
				{
					string b = _003F487_003F._003F488_003F("");
					if ((string)/*Error near IL_004e: Stack underflow*/ != b)
					{
						QSingle oQSingle4 = oQSingle3;
						((QSingle)/*Error near IL_0058: Stack underflow*/).ParentCode = (string)/*Error near IL_0058: Stack underflow*/;
						oQSingle3.GetDynamicDetails();
						cmbSelect3.ItemsSource = oQSingle3.QDetails;
						cmbSelect3.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
						cmbSelect3.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
					}
				}
				if (oQSingle2.OtherCode != _003F487_003F._003F488_003F(""))
				{
					bool flag = a == oQSingle2.OtherCode;
					if ((int)/*Error near IL_0128: Stack underflow*/ != 0)
					{
						TextBlock txtOther4 = txtOther2;
						((UIElement)/*Error near IL_0139: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0139: Stack underflow*/;
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

		private void _003F167_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			//IL_006a: Incompatible stack heights: 0 vs 1
			//IL_007b: Incompatible stack heights: 0 vs 2
			//IL_008c: Incompatible stack heights: 0 vs 2
			if (cmbSelect3.SelectedValue != null)
			{
				object selectedValue = cmbSelect3.SelectedValue;
				string text = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (oQSingle3.OtherCode != _003F487_003F._003F488_003F(""))
				{
					QSingle oQSingle4 = oQSingle3;
					string otherCode = ((QSingle)/*Error near IL_003a: Stack underflow*/).OtherCode;
					if ((string)/*Error near IL_003f: Stack underflow*/ == otherCode)
					{
						TextBlock txtOther4 = txtOther3;
						((UIElement)/*Error near IL_0049: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0049: Stack underflow*/;
						txtFillOther3.Visibility = Visibility.Visible;
					}
					else
					{
						txtOther3.Visibility = Visibility.Hidden;
						txtFillOther3.Visibility = Visibility.Hidden;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0003Ŭə\u035aёԉ٥\u0744ࡔ\u094aਙ\u0b42\u0c4f൲\u0e6e\u0f72\u1072ᅾቴ፭ᐷᕡᙿᝰᡣ\u193c\u1a60᭴\u1c7cᵮṺὤ\u2063Ⅵ≦⍠⑻╳☵✫⡼⥢⩯⭭"), UriKind.Relative);
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
				((RelationList3)_003F350_003F).Loaded += _003F80_003F;
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
				textBlock3 = (TextBlock)_003F350_003F;
				break;
			case 13:
				cmbSelect3 = (ComboBox)_003F350_003F;
				cmbSelect3.SelectionChanged += _003F167_003F;
				break;
			case 14:
				txtOther3 = (TextBlock)_003F350_003F;
				break;
			case 15:
				txtFillOther3 = (TextBox)_003F350_003F;
				txtFillOther3.GotFocus += _003F91_003F;
				txtFillOther3.LostFocus += _003F90_003F;
				break;
			case 16:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 17:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 18:
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
