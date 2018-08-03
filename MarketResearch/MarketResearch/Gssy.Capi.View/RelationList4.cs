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
using System.Windows.Navigation;

namespace Gssy.Capi.View
{
	public class RelationList4 : Page, IComponentConnector
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static Comparison<SurveyDetail> _003C_003E9__10_0;

			internal int _003F329_003F(SurveyDetail _003F481_003F, SurveyDetail _003F482_003F)
			{
				return Comparer<int>.Default.Compare(_003F481_003F.INNER_ORDER, _003F482_003F.INNER_ORDER);
			}
		}

		private string MySurveyId;

		private string CurPageId;

		private NavBase MyNav = new NavBase();

		private LogicEngine oLogicEngine = new LogicEngine();

		private QBase oQuestion = new QBase();

		private QSingle oQSingle1 = new QSingle();

		private QSingle oQSingle2 = new QSingle();

		private QSingle oQSingle3 = new QSingle();

		private QSingle oQSingle4 = new QSingle();

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

		internal TextBlock textBlock4;

		internal ComboBox cmbSelect4;

		internal TextBlock txtOther4;

		internal TextBox txtFillOther4;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public RelationList4()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_033f: Incompatible stack heights: 0 vs 2
			//IL_0356: Incompatible stack heights: 0 vs 1
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			txtSurvey.Text = MySurveyId;
			oQuestion.Init(CurPageId, 0);
			oQSingle1.Init(CurPageId, 1, true);
			oQSingle2.Init(CurPageId, 2, true);
			oQSingle3.Init(CurPageId, 3, true);
			oQSingle4.Init(CurPageId, 4, true);
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
				if (_003F7_003F._003C_003E9__10_0 == null)
				{
					_003F7_003F._003C_003E9__10_0 = _003F7_003F._003C_003E9._003F329_003F;
				}
				((List<SurveyDetail>)/*Error near IL_035b: Stack underflow*/).Sort((Comparison<SurveyDetail>)/*Error near IL_035b: Stack underflow*/);
				oQSingle1.QDetails = list;
			}
			textBlock1.Text = oQSingle1.QDefine.QUESTION_TITLE;
			textBlock2.Text = oQSingle2.QDefine.QUESTION_TITLE;
			textBlock3.Text = oQSingle3.QDefine.QUESTION_TITLE;
			textBlock4.Text = oQSingle4.QDefine.QUESTION_TITLE;
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
			if (oQSingle4.QDefine.PARENT_CODE == _003F487_003F._003F488_003F(""))
			{
				cmbSelect4.ItemsSource = oQSingle4.QDetails;
				cmbSelect4.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
				cmbSelect4.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
			}
			cmbSelect1.IsEditable = false;
			cmbSelect2.IsEditable = false;
			cmbSelect3.IsEditable = false;
			cmbSelect4.IsEditable = false;
			txtOther1.Visibility = Visibility.Hidden;
			txtOther2.Visibility = Visibility.Hidden;
			txtOther3.Visibility = Visibility.Hidden;
			txtOther4.Visibility = Visibility.Hidden;
			txtFillOther1.Visibility = Visibility.Hidden;
			txtFillOther2.Visibility = Visibility.Hidden;
			txtFillOther3.Visibility = Visibility.Hidden;
			txtFillOther4.Visibility = Visibility.Hidden;
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
				cmbSelect4.SelectedValue = autoFill.SingleDetail(oQSingle4.QDefine, oQSingle4.QDetails).CODE;
				_003F168_003F(cmbSelect4, null);
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
				if (txtFillOther4.IsEnabled)
				{
					txtFillOther4.Text = autoFill.CommonOther(oQSingle4.QDefine, _003F487_003F._003F488_003F(""));
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
				cmbSelect4.SelectedValue = oQSingle3.ReadAnswerByQuestionName(MySurveyId, oQSingle4.QuestionName);
				txtFillOther1.Text = oQSingle1.ReadAnswerByQuestionName(MySurveyId, oQSingle1.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				txtFillOther2.Text = oQSingle2.ReadAnswerByQuestionName(MySurveyId, oQSingle2.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				txtFillOther3.Text = oQSingle3.ReadAnswerByQuestionName(MySurveyId, oQSingle3.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
				txtFillOther4.Text = oQSingle4.ReadAnswerByQuestionName(MySurveyId, oQSingle4.QuestionName + _003F487_003F._003F488_003F("[Ōɖ\u0349"));
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_06bc: Incompatible stack heights: 0 vs 1
			//IL_06d6: Incompatible stack heights: 0 vs 1
			//IL_06fa: Incompatible stack heights: 0 vs 1
			//IL_0719: Incompatible stack heights: 0 vs 1
			//IL_0747: Incompatible stack heights: 0 vs 1
			//IL_0762: Incompatible stack heights: 0 vs 2
			//IL_0772: Incompatible stack heights: 0 vs 1
			//IL_0789: Incompatible stack heights: 0 vs 4
			//IL_07a3: Incompatible stack heights: 0 vs 2
			//IL_07b3: Incompatible stack heights: 0 vs 1
			//IL_07cd: Incompatible stack heights: 0 vs 2
			//IL_07e1: Incompatible stack heights: 0 vs 2
			//IL_07f1: Incompatible stack heights: 0 vs 1
			//IL_0801: Incompatible stack heights: 0 vs 1
			//IL_0816: Incompatible stack heights: 0 vs 1
			//IL_082d: Incompatible stack heights: 0 vs 4
			//IL_0851: Incompatible stack heights: 0 vs 1
			//IL_0866: Incompatible stack heights: 0 vs 1
			//IL_0880: Incompatible stack heights: 0 vs 2
			//IL_0894: Incompatible stack heights: 0 vs 2
			//IL_08a9: Incompatible stack heights: 0 vs 1
			//IL_08b4: Incompatible stack heights: 0 vs 1
			//IL_08d7: Incompatible stack heights: 0 vs 4
			List<VEAnswer> list;
			if (cmbSelect1.SelectedValue != null)
			{
				object selectedValue = cmbSelect1.SelectedValue;
				if (!((string)/*Error near IL_0011: Stack underflow*/ == _003F487_003F._003F488_003F("")))
				{
					if (cmbSelect2.SelectedValue != null)
					{
						ComboBox cmbSelect5 = cmbSelect2;
						if (!((string)((Selector)/*Error near IL_0052: Stack underflow*/).SelectedValue == _003F487_003F._003F488_003F("")))
						{
							if (cmbSelect3.SelectedValue != null)
							{
								string text = (string)cmbSelect3.SelectedValue;
								string b = _003F487_003F._003F488_003F("");
								if (!((string)/*Error near IL_009e: Stack underflow*/ == b))
								{
									if (cmbSelect4.SelectedValue != null)
									{
										object selectedValue2 = cmbSelect4.SelectedValue;
										if (!((string)/*Error near IL_00c8: Stack underflow*/ == _003F487_003F._003F488_003F("")))
										{
											oQSingle1.FillText = _003F487_003F._003F488_003F("");
											oQSingle2.FillText = _003F487_003F._003F488_003F("");
											oQSingle3.FillText = _003F487_003F._003F488_003F("");
											oQSingle4.FillText = _003F487_003F._003F488_003F("");
											if (oQSingle1.OtherCode != null)
											{
												bool flag = oQSingle1.OtherCode != _003F487_003F._003F488_003F("");
												if ((int)/*Error near IL_074c: Stack underflow*/ != 0)
												{
													cmbSelect1.SelectedValue.ToString();
													string otherCode = ((RelationList4)/*Error near IL_0162: Stack underflow*/).oQSingle1.OtherCode;
													if ((string)/*Error near IL_016c: Stack underflow*/ == otherCode)
													{
														TextBox txtFillOther5 = txtFillOther1;
														if (((TextBox)/*Error near IL_0176: Stack underflow*/).Text == _003F487_003F._003F488_003F(""))
														{
															string msgNotFillOther = SurveyMsg.MsgNotFillOther;
															string msgCaption = SurveyMsg.MsgCaption;
															MessageBox.Show((string)/*Error near IL_018f: Stack underflow*/, (string)/*Error near IL_018f: Stack underflow*/, (MessageBoxButton)/*Error near IL_018f: Stack underflow*/, (MessageBoxImage)/*Error near IL_018f: Stack underflow*/);
															txtFillOther1.Focus();
															return;
														}
														oQSingle1.FillText = txtFillOther1.Text;
													}
												}
											}
											if (oQSingle2.OtherCode != null)
											{
												string otherCode2 = oQSingle2.OtherCode;
												string b2 = _003F487_003F._003F488_003F((string)/*Error near IL_01c8: Stack underflow*/);
												if ((string)/*Error near IL_01cd: Stack underflow*/ != b2)
												{
													ComboBox cmbSelect6 = cmbSelect2;
													if (((Selector)/*Error near IL_01d7: Stack underflow*/).SelectedValue.ToString() == oQSingle2.OtherCode)
													{
														string text2 = txtFillOther2.Text;
														string b3 = _003F487_003F._003F488_003F((string)/*Error near IL_01f6: Stack underflow*/);
														if ((string)/*Error near IL_01fb: Stack underflow*/ == b3)
														{
															string msgNotFillOther2 = SurveyMsg.MsgNotFillOther;
															string msgCaption2 = SurveyMsg.MsgCaption;
															MessageBox.Show((string)/*Error near IL_0208: Stack underflow*/, (string)/*Error near IL_0208: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
															txtFillOther2.Focus();
															return;
														}
														oQSingle2.FillText = txtFillOther2.Text;
													}
												}
											}
											if (oQSingle3.OtherCode != null)
											{
												QSingle oQSingle5 = oQSingle3;
												if (((QSingle)/*Error near IL_0241: Stack underflow*/).OtherCode != _003F487_003F._003F488_003F(""))
												{
													ComboBox cmbSelect7 = cmbSelect3;
													if (((Selector)/*Error near IL_025a: Stack underflow*/).SelectedValue.ToString() == oQSingle3.OtherCode)
													{
														string text3 = txtFillOther3.Text;
														string b4 = _003F487_003F._003F488_003F("");
														if ((string)/*Error near IL_0283: Stack underflow*/ == b4)
														{
															string msgNotFillOther3 = SurveyMsg.MsgNotFillOther;
															string msgCaption3 = SurveyMsg.MsgCaption;
															MessageBox.Show((string)/*Error near IL_028d: Stack underflow*/, (string)/*Error near IL_028d: Stack underflow*/, (MessageBoxButton)/*Error near IL_028d: Stack underflow*/, (MessageBoxImage)/*Error near IL_028d: Stack underflow*/);
															txtFillOther3.Focus();
															return;
														}
														oQSingle3.FillText = txtFillOther3.Text;
													}
												}
											}
											if (oQSingle4.OtherCode != null)
											{
												bool flag2 = oQSingle4.OtherCode != _003F487_003F._003F488_003F("");
												if ((int)/*Error near IL_0856: Stack underflow*/ != 0)
												{
													object selectedValue3 = cmbSelect4.SelectedValue;
													if (((object)/*Error near IL_02cb: Stack underflow*/).ToString() == oQSingle4.OtherCode)
													{
														string text4 = txtFillOther4.Text;
														string b5 = _003F487_003F._003F488_003F((string)/*Error near IL_02e5: Stack underflow*/);
														if ((string)/*Error near IL_02ea: Stack underflow*/ == b5)
														{
															string msgNotFillOther4 = SurveyMsg.MsgNotFillOther;
															string msgCaption4 = SurveyMsg.MsgCaption;
															MessageBox.Show((string)/*Error near IL_02f7: Stack underflow*/, (string)/*Error near IL_02f7: Stack underflow*/, MessageBoxButton.OK, MessageBoxImage.Hand);
															txtFillOther4.Focus();
															return;
														}
														oQSingle4.FillText = txtFillOther4.Text;
													}
												}
											}
											oQSingle1.SelectedCode = cmbSelect1.SelectedValue.ToString();
											oQSingle2.SelectedCode = cmbSelect2.SelectedValue.ToString();
											oQSingle3.SelectedCode = cmbSelect3.SelectedValue.ToString();
											oQSingle4.SelectedCode = cmbSelect4.SelectedValue.ToString();
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
											VEAnswer vEAnswer4 = new VEAnswer();
											vEAnswer4.QUESTION_NAME = oQSingle4.QuestionName;
											vEAnswer4.CODE = oQSingle4.SelectedCode;
											list.Add(vEAnswer4);
											SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F(".ġ") + oQSingle4.QuestionName + _003F487_003F._003F488_003F("<") + oQSingle4.SelectedCode;
											oLogicEngine.PageAnswer = list;
											oLogicEngine.SurveyID = MySurveyId;
											if (!oLogicEngine.CheckLogic(CurPageId))
											{
												int iS_ALLOW_PASS = oLogicEngine.IS_ALLOW_PASS;
												if ((int)/*Error near IL_08ae: Stack underflow*/ == 0)
												{
													MessageBox.Show(((RelationList4)/*Error near IL_059e: Stack underflow*/).oLogicEngine.Logic_Message, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
													return;
												}
												if (MessageBox.Show(oLogicEngine.Logic_Message + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
												{
													return;
												}
											}
											goto IL_05f9;
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
				}
			}
			MessageBox.Show(SurveyMsg.MsgNotSelected, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
			return;
			IL_05f9:
			oQSingle1.BeforeSave();
			oQSingle1.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			oQSingle2.BeforeSave();
			oQSingle2.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			oQSingle3.BeforeSave();
			oQSingle3.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			oQSingle4.BeforeSave();
			oQSingle4.Save(MySurveyId, SurveyHelper.SurveySequence, true);
			_003F163_003F(SurveyMsg.DelaySeconds);
			if (SurveyHelper.Debug)
			{
				SurveyHelper.ShowPageAnswer(list);
				string msgCaption5 = SurveyMsg.MsgCaption;
				MessageBox.Show((string)/*Error near IL_069b: Stack underflow*/, (string)/*Error near IL_069b: Stack underflow*/, (MessageBoxButton)/*Error near IL_069b: Stack underflow*/, (MessageBoxImage)/*Error near IL_069b: Stack underflow*/);
			}
			MyNav.PageAnswer = list;
			_003F81_003F();
			return;
			IL_08ba:
			goto IL_05f9;
		}

		private void _003F81_003F()
		{
			//IL_00f7: Incompatible stack heights: 0 vs 2
			//IL_0107: Incompatible stack heights: 0 vs 1
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
				NavigationService navigationService = base.NavigationService;
				((NavigationService)/*Error near IL_00d7: Stack underflow*/).Refresh();
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
			goto IL_0006;
			IL_0006:
			btnNav.IsEnabled = false;
			btnNav.Content = _003F487_003F._003F488_003F("歨嘢䷔塐Ы軱簈圝\u082dबਯ");
			Logging.Data.WriteLog(MySurveyId, oQSingle1.QuestionName + _003F487_003F._003F488_003F("-") + cmbSelect1.SelectedValue.ToString());
			Logging.Data.WriteLog(MySurveyId, oQSingle2.QuestionName + _003F487_003F._003F488_003F("-") + cmbSelect2.SelectedValue.ToString());
			Logging.Data.WriteLog(MySurveyId, oQSingle3.QuestionName + _003F487_003F._003F488_003F("-") + cmbSelect3.SelectedValue.ToString());
			Thread.Sleep(_003F393_003F);
			btnNav.Content = _003F487_003F._003F488_003F("绣ģȢ緬");
			btnNav.IsEnabled = true;
			return;
			IL_001d:
			goto IL_0006;
		}

		private void _003F148_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			//IL_00dc: Incompatible stack heights: 0 vs 1
			//IL_00e7: Incompatible stack heights: 0 vs 1
			//IL_0101: Incompatible stack heights: 0 vs 1
			//IL_0112: Incompatible stack heights: 0 vs 2
			//IL_0123: Incompatible stack heights: 0 vs 2
			//IL_0140: Incompatible stack heights: 0 vs 2
			if (cmbSelect1.SelectedValue != null)
			{
				object selectedValue = cmbSelect1.SelectedValue;
				string a = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (oQSingle2.QDefine.PARENT_CODE != _003F487_003F._003F488_003F("") && (int)/*Error near IL_00ec: Stack underflow*/ != 0)
				{
					bool flag = a != _003F487_003F._003F488_003F("");
					if ((int)/*Error near IL_0106: Stack underflow*/ != 0)
					{
						QSingle oQSingle5 = oQSingle2;
						((QSingle)/*Error near IL_0049: Stack underflow*/).ParentCode = (string)/*Error near IL_0049: Stack underflow*/;
						oQSingle2.GetDynamicDetails();
						cmbSelect2.ItemsSource = oQSingle2.QDetails;
						cmbSelect2.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
						cmbSelect2.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
					}
				}
				if (oQSingle1.OtherCode != _003F487_003F._003F488_003F(""))
				{
					QSingle oQSingle6 = oQSingle1;
					string otherCode = ((QSingle)/*Error near IL_00b8: Stack underflow*/).OtherCode;
					if ((string)/*Error near IL_00bd: Stack underflow*/ == otherCode)
					{
						txtOther1.Visibility = Visibility.Visible;
						TextBox txtFillOther5 = txtFillOther1;
						((UIElement)/*Error near IL_0145: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0145: Stack underflow*/;
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
			//IL_00dc: Incompatible stack heights: 0 vs 1
			//IL_00e7: Incompatible stack heights: 0 vs 1
			//IL_0101: Incompatible stack heights: 0 vs 1
			//IL_0112: Incompatible stack heights: 0 vs 2
			//IL_0123: Incompatible stack heights: 0 vs 2
			//IL_0140: Incompatible stack heights: 0 vs 2
			if (cmbSelect2.SelectedValue != null)
			{
				object selectedValue = cmbSelect2.SelectedValue;
				string a = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (oQSingle3.QDefine.PARENT_CODE != _003F487_003F._003F488_003F("") && (int)/*Error near IL_00ec: Stack underflow*/ != 0)
				{
					bool flag = a != _003F487_003F._003F488_003F("");
					if ((int)/*Error near IL_0106: Stack underflow*/ != 0)
					{
						QSingle oQSingle5 = oQSingle3;
						((QSingle)/*Error near IL_0049: Stack underflow*/).ParentCode = (string)/*Error near IL_0049: Stack underflow*/;
						oQSingle3.GetDynamicDetails();
						cmbSelect3.ItemsSource = oQSingle3.QDetails;
						cmbSelect3.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
						cmbSelect3.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
					}
				}
				if (oQSingle2.OtherCode != _003F487_003F._003F488_003F(""))
				{
					QSingle oQSingle6 = oQSingle2;
					string otherCode = ((QSingle)/*Error near IL_00b8: Stack underflow*/).OtherCode;
					if ((string)/*Error near IL_00bd: Stack underflow*/ == otherCode)
					{
						txtOther2.Visibility = Visibility.Visible;
						TextBox txtFillOther5 = txtFillOther2;
						((UIElement)/*Error near IL_0145: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0145: Stack underflow*/;
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
			//IL_00dc: Incompatible stack heights: 0 vs 1
			//IL_00e7: Incompatible stack heights: 0 vs 1
			//IL_0101: Incompatible stack heights: 0 vs 1
			//IL_0112: Incompatible stack heights: 0 vs 2
			//IL_0123: Incompatible stack heights: 0 vs 2
			//IL_0140: Incompatible stack heights: 0 vs 2
			if (cmbSelect3.SelectedValue != null)
			{
				object selectedValue = cmbSelect3.SelectedValue;
				string a = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (oQSingle4.QDefine.PARENT_CODE != _003F487_003F._003F488_003F("") && (int)/*Error near IL_00ec: Stack underflow*/ != 0)
				{
					bool flag = a != _003F487_003F._003F488_003F("");
					if ((int)/*Error near IL_0106: Stack underflow*/ != 0)
					{
						QSingle oQSingle5 = oQSingle4;
						((QSingle)/*Error near IL_0049: Stack underflow*/).ParentCode = (string)/*Error near IL_0049: Stack underflow*/;
						oQSingle4.GetDynamicDetails();
						cmbSelect4.ItemsSource = oQSingle4.QDetails;
						cmbSelect4.DisplayMemberPath = _003F487_003F._003F488_003F("JŇɃ\u0343њՐنݚࡕ");
						cmbSelect4.SelectedValuePath = _003F487_003F._003F488_003F("GŌɆ\u0344");
					}
				}
				if (oQSingle3.OtherCode != _003F487_003F._003F488_003F(""))
				{
					QSingle oQSingle6 = oQSingle3;
					string otherCode = ((QSingle)/*Error near IL_00b8: Stack underflow*/).OtherCode;
					if ((string)/*Error near IL_00bd: Stack underflow*/ == otherCode)
					{
						txtOther3.Visibility = Visibility.Visible;
						TextBox txtFillOther5 = txtFillOther3;
						((UIElement)/*Error near IL_0145: Stack underflow*/).Visibility = (Visibility)/*Error near IL_0145: Stack underflow*/;
					}
					else
					{
						txtOther3.Visibility = Visibility.Hidden;
						txtFillOther3.Visibility = Visibility.Hidden;
					}
				}
			}
		}

		private void _003F168_003F(object _003F347_003F, SelectionChangedEventArgs _003F348_003F = null)
		{
			//IL_0064: Incompatible stack heights: 0 vs 1
			//IL_0070: Incompatible stack heights: 0 vs 2
			if (cmbSelect4.SelectedValue != null)
			{
				object selectedValue = cmbSelect4.SelectedValue;
				string text = ((object)/*Error near IL_0015: Stack underflow*/).ToString();
				if (oQSingle4.OtherCode != _003F487_003F._003F488_003F(""))
				{
					string otherCode = ((RelationList4)/*Error near IL_003a: Stack underflow*/).oQSingle4.OtherCode;
					if ((string)/*Error near IL_0044: Stack underflow*/ == otherCode)
					{
						txtOther4.Visibility = Visibility.Visible;
						txtFillOther4.Visibility = Visibility.Visible;
					}
					else
					{
						txtOther4.Visibility = Visibility.Hidden;
						txtFillOther4.Visibility = Visibility.Hidden;
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
			goto IL_000b;
			IL_000b:
			_contentLoaded = true;
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0003Ŭə\u035aёԉ٥\u0744ࡔ\u094aਙ\u0b42\u0c4f൲\u0e6e\u0f72\u1072ᅾቴ፭ᐷᕡᙿᝰᡣ\u193c\u1a60᭴\u1c7cᵮṺὤ\u2063Ⅵ≦⍠⑻╳☲✫⡼⥢⩯⭭"), UriKind.Relative);
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
				((RelationList4)_003F350_003F).Loaded += _003F80_003F;
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
				textBlock4 = (TextBlock)_003F350_003F;
				break;
			case 17:
				cmbSelect4 = (ComboBox)_003F350_003F;
				cmbSelect4.SelectionChanged += _003F168_003F;
				break;
			case 18:
				txtOther4 = (TextBlock)_003F350_003F;
				break;
			case 19:
				txtFillOther4 = (TextBox)_003F350_003F;
				txtFillOther4.GotFocus += _003F91_003F;
				txtFillOther4.LostFocus += _003F90_003F;
				break;
			case 20:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 21:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 22:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0065:
			goto IL_006f;
		}
	}
}
