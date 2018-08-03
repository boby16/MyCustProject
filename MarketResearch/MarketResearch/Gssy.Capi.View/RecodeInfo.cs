using Gssy.Capi.BIZ;
using Gssy.Capi.Class;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gssy.Capi.View
{
	public class RecodeInfo : Page, IComponentConnector
	{
		private string MySurveyId;

		private string CurPageId;

		private string ErrorPageId = _003F487_003F._003F488_003F("");

		private NavBase MyNav = new NavBase();

		private PageNav oPageNav = new PageNav();

		private LogicEngine oLogicEngine = new LogicEngine();

		private AutoFill oAutoFill = new AutoFill();

		private BoldTitle oBoldTitle = new BoldTitle();

		private QFill oQuestion = new QFill();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private UDPX oFunc = new UDPX();

		private DispatcherTimer timer = new DispatcherTimer();

		private int SecondsWait;

		private int SecondsCountDown;

		private string btnNav_Content = SurveyMsg.MsgbtnNav_Content;

		internal Grid gridContent;

		internal TextBlock txtQuestionTitle;

		internal TextBlock txtCircleTitle;

		internal ScrollViewer scrollNote;

		internal TextBlock txtQuestionNote;

		internal TextBlock txtSurvey;

		internal Button btnAttach;

		internal Button btnNav;

		private bool _contentLoaded;

		public RecodeInfo()
		{
			InitializeComponent();
		}

		private void _003F80_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			//IL_0594: Incompatible stack heights: 0 vs 1
			//IL_05b4: Incompatible stack heights: 0 vs 2
			//IL_05c7: Incompatible stack heights: 0 vs 1
			//IL_05e1: Incompatible stack heights: 0 vs 2
			//IL_05fc: Incompatible stack heights: 0 vs 2
			//IL_0609: Incompatible stack heights: 0 vs 3
			//IL_061f: Incompatible stack heights: 0 vs 2
			//IL_0639: Incompatible stack heights: 0 vs 2
			//IL_0653: Incompatible stack heights: 0 vs 1
			//IL_0658: Incompatible stack heights: 1 vs 0
			//IL_0668: Incompatible stack heights: 0 vs 1
			//IL_068a: Incompatible stack heights: 0 vs 2
			MySurveyId = SurveyHelper.SurveyID;
			CurPageId = SurveyHelper.NavCurPage;
			SurveyHelper.PageStartTime = DateTime.Now;
			oQuestion.Init(CurPageId, 0);
			MyNav.GroupLevel = oQuestion.QDefine.GROUP_LEVEL;
			if (!(MyNav.GroupLevel == _003F487_003F._003F488_003F("@")))
			{
				bool flag = MyNav.GroupLevel == _003F487_003F._003F488_003F("C");
				if ((int)/*Error near IL_0599: Stack underflow*/ == 0)
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
					goto IL_0320;
				}
			}
			MyNav.GroupPageType = oQuestion.QDefine.GROUP_PAGE_TYPE;
			MyNav.GroupCodeA = oQuestion.QDefine.GROUP_CODEA;
			MyNav.CircleACurrent = SurveyHelper.CircleACurrent;
			MyNav.CircleACount = SurveyHelper.CircleACount;
			if (MyNav.GroupLevel == _003F487_003F._003F488_003F("C"))
			{
				NavBase myNav = MyNav;
				QFill oQuestion2 = oQuestion;
				string gROUP_CODEB = ((QFill)/*Error near IL_00eb: Stack underflow*/).QDefine.GROUP_CODEB;
				((NavBase)/*Error near IL_00f5: Stack underflow*/).GroupCodeB = gROUP_CODEB;
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
				string groupCodeB = MyNav.GroupCodeB;
				((VEAnswer)/*Error near IL_0201: Stack underflow*/).QUESTION_NAME = groupCodeB;
				vEAnswer2.CODE = MyNav.CircleBCode;
				vEAnswer2.CODE_TEXT = MyNav.CircleCodeTextB;
				list.Add(vEAnswer2);
				SurveyHelper.CircleBCode = MyNav.CircleBCode;
				SurveyHelper.CircleBCodeText = MyNav.CircleCodeTextB;
				SurveyHelper.CircleBCurrent = MyNav.CircleBCurrent;
				SurveyHelper.CircleBCount = MyNav.CircleBCount;
			}
			goto IL_0320;
			IL_0320:
			oLogicEngine.SurveyID = MySurveyId;
			if (MyNav.GroupLevel != _003F487_003F._003F488_003F(""))
			{
				LogicEngine oLogicEngine2 = oLogicEngine;
				string circleACode = SurveyHelper.CircleACode;
				((LogicEngine)/*Error near IL_0355: Stack underflow*/).CircleACode = (string)/*Error near IL_0355: Stack underflow*/;
				oLogicEngine.CircleACodeText = SurveyHelper.CircleACodeText;
				oLogicEngine.CircleACount = SurveyHelper.CircleACount;
				oLogicEngine.CircleACurrent = SurveyHelper.CircleACurrent;
				oLogicEngine.CircleBCode = SurveyHelper.CircleBCode;
				oLogicEngine.CircleBCodeText = SurveyHelper.CircleBCodeText;
				oLogicEngine.CircleBCount = SurveyHelper.CircleBCount;
				oLogicEngine.CircleBCurrent = SurveyHelper.CircleBCurrent;
			}
			oAutoFill.oLogicEngine = oLogicEngine;
			if (SurveyHelper.AutoFill)
			{
				AutoFill oAutoFill2 = oAutoFill;
				SurveyDefine qDefine = oQuestion.QDefine;
				if (((AutoFill)/*Error near IL_03e5: Stack underflow*/).AutoNext((SurveyDefine)/*Error near IL_03e5: Stack underflow*/))
				{
					((RecodeInfo)/*Error near IL_03ef: Stack underflow*/)._003F58_003F((object)/*Error near IL_03ef: Stack underflow*/, (RoutedEventArgs)/*Error near IL_03ef: Stack underflow*/);
				}
			}
			if (oQuestion.QDefine.CONTROL_WIDTH != 0)
			{
				Grid gridContent2 = gridContent;
				QFill oQuestion3 = oQuestion;
				double width = (double)((QFill)/*Error near IL_0409: Stack underflow*/).QDefine.CONTROL_WIDTH;
				((FrameworkElement)/*Error near IL_0414: Stack underflow*/).Width = width;
			}
			string text = oQuestion.QDefine.QUESTION_TITLE;
			if (text == _003F487_003F._003F488_003F(""))
			{
				_003F487_003F._003F488_003F("YŏɊ\u0347уՃإ\uf81e\u083f\u0940\u0a3f");
				QFill oQuestion4 = oQuestion;
				text = string.Concat(str1: ((QFill)/*Error near IL_043f: Stack underflow*/).QDefine.QUESTION_NAME, str2: _003F487_003F._003F488_003F("8Ĭɀ\u033f"), str0: (string)/*Error near IL_0453: Stack underflow*/);
			}
			List<string> list2 = oBoldTitle.ParaToList(text, _003F487_003F._003F488_003F("-Į"));
			text = list2[0];
			oBoldTitle.SetTextBlock(txtQuestionTitle, text, oQuestion.QDefine.TITLE_FONTSIZE, _003F487_003F._003F488_003F(""), true);
			string text2;
			if (list2.Count > 1)
			{
				text2 = list2[1];
			}
			else
			{
				string qUESTION_CONTENT = oQuestion.QDefine.QUESTION_CONTENT;
			}
			text = text2;
			oBoldTitle.SetTextBlock(txtCircleTitle, text, 0, _003F487_003F._003F488_003F(""), true);
			text = oQuestion.QDefine.NOTE;
			oBoldTitle.SetTextBlock(txtQuestionNote, text, 0, _003F487_003F._003F488_003F(""), true);
			if (SurveyMsg.FunctionAttachments == _003F487_003F._003F488_003F("^ŢɸͶѠպٽݿࡑॻ\u0a7a୬౯\u0d63\u0e67ཬၦᅳትፚᑰᕱᙷᝤ"))
			{
				QFill oQuestion5 = oQuestion;
				if (((QFill)/*Error near IL_0524: Stack underflow*/).QDefine.IS_ATTACH == 1)
				{
					btnAttach.Visibility = Visibility.Visible;
				}
			}
			new SurveyBiz().ClearPageAnswer(MySurveyId, SurveyHelper.SurveySequence);
			SecondsWait = oQuestion.QDefine.PAGE_COUNT_DOWN;
			if (SecondsWait > 0)
			{
				int secondsWait = ((RecodeInfo)/*Error near IL_056b: Stack underflow*/).SecondsWait;
				((RecodeInfo)/*Error near IL_068f: Stack underflow*/).SecondsCountDown = secondsWait;
				btnNav.Foreground = Brushes.Gray;
				btnNav.Content = SecondsCountDown.ToString();
				timer.Interval = TimeSpan.FromMilliseconds(1000.0);
				timer.Tick += _003F84_003F;
				timer.Start();
			}
		}

		private void _003F58_003F(object _003F347_003F, RoutedEventArgs _003F348_003F)
		{
			_003F153_003F();
			oPageNav.oLogicEngine = oLogicEngine;
			if (!oPageNav.CheckLogic(CurPageId))
			{
				btnNav.Content = btnNav_Content;
				return;
			}
			goto IL_002d;
			IL_0060:
			goto IL_002d;
			IL_002d:
			oPageNav.NextPage(MyNav, base.NavigationService);
			btnNav.Content = btnNav_Content;
		}

		private void _003F153_003F()
		{
			new SurveyAnswerDal().ClearBySequenceId(MySurveyId, SurveyHelper.SurveySequence);
			if (oQuestion.QDefine.QUESTION_TYPE == 60)
			{
				_003F154_003F();
			}
			else
			{
				List<SurveyLogic> reCodeLogic = new SurveyLogicDal().GetReCodeLogic(CurPageId, SurveyMsg.MsgProgramType, 0, 99999);
				if (reCodeLogic.Count == 0)
				{
					MessageBox.Show(string.Format(SurveyMsg.MsgNotSetRecode, CurPageId), SurveyMsg.MsgCaptionError, MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				else
				{
					SurveyLogic surveyLogic = new SurveyLogic();
					surveyLogic.PAGE_ID = _003F487_003F._003F488_003F("!");
					surveyLogic.INNER_INDEX = 0;
					reCodeLogic.Add(surveyLogic);
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					string text = _003F487_003F._003F488_003F("");
					string str = _003F487_003F._003F488_003F("");
					foreach (SurveyLogic item in reCodeLogic)
					{
						if (item.PAGE_ID + item.LOGIC_MESSAGE == text + str)
						{
							num2 = num3;
						}
						else
						{
							if (text != _003F487_003F._003F488_003F(""))
							{
								string _003F380_003F = oQuestion.QuestionName;
								SurveyDefine surveyDefine = oQuestion.QDefine;
								if (reCodeLogic[num].LOGIC_MESSAGE != _003F487_003F._003F488_003F(""))
								{
									string text2 = reCodeLogic[num].LOGIC_MESSAGE;
									if (oFunc.LEFT(text2, 1) == _003F487_003F._003F488_003F("_"))
									{
										text2 = oFunc.MID(text2, 1, -9999);
										_003F380_003F = text2 + MyNav.QName_Add;
									}
									else
									{
										_003F380_003F = text2;
									}
									surveyDefine = oSurveyDefineDal.GetByName(text2);
									if (surveyDefine.PAGE_ID == null || surveyDefine.PAGE_ID == _003F487_003F._003F488_003F(""))
									{
										surveyDefine = oQuestion.QDefine;
									}
								}
								if (reCodeLogic[num].LOGIC_TYPE == _003F487_003F._003F488_003F("^Ŏɉ\u0346ьՂ\u0659\u0749ࡋ\u0944\u0a4b\u0b42"))
								{
									_003F155_003F(_003F380_003F, surveyDefine, reCodeLogic[num].INNER_INDEX, reCodeLogic[num2].INNER_INDEX);
								}
								else
								{
									_003F156_003F(_003F380_003F, surveyDefine, reCodeLogic, num, num2);
								}
							}
							num = num3;
							num2 = num3;
							text = item.PAGE_ID;
							str = item.LOGIC_MESSAGE;
						}
						num3++;
					}
				}
			}
		}

		private void _003F154_003F()
		{
			try
			{
				string _003F372_003F = _003F487_003F._003F488_003F("");
				string[] array = oLogicEngine.RecodeAddonLogic(CurPageId, out _003F372_003F, SurveyMsg.MsgProgramType);
				if (MyNav.QName_Add != _003F487_003F._003F488_003F(""))
				{
					for (int i = 0; i < array.Count(); i++)
					{
						array[i] += MyNav.QName_Add;
					}
					_003F372_003F += MyNav.QName_Add;
				}
				SurveyAnswerDal surveyAnswerDal = new SurveyAnswerDal();
				string oneCode = surveyAnswerDal.GetOneCode(MySurveyId, _003F372_003F);
				SurveyDetail one = new SurveyDetailDal().GetOne(oQuestion.QDefine.DETAIL_ID, oneCode);
				string text = _003F487_003F._003F488_003F("");
				SurveyHelper.Answer = _003F487_003F._003F488_003F("");
				for (int j = 0; j < array.Count(); j++)
				{
					switch (j)
					{
					case 0:
						text = one.EXTEND_1;
						break;
					case 1:
						text = one.EXTEND_2;
						break;
					case 2:
						text = one.EXTEND_3;
						break;
					case 3:
						text = one.EXTEND_4;
						break;
					case 4:
						text = one.EXTEND_5;
						break;
					case 5:
						text = one.EXTEND_6;
						break;
					case 6:
						text = one.EXTEND_7;
						break;
					case 7:
						text = one.EXTEND_8;
						break;
					case 8:
						text = one.EXTEND_9;
						break;
					case 9:
						text = one.EXTEND_10;
						break;
					}
					surveyAnswerDal.AddOne(MySurveyId, array[j], text, SurveyHelper.SurveySequence);
					SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + array[j] + _003F487_003F._003F488_003F("<") + text;
				}
				SurveyHelper.Answer = oFunc.MID(SurveyHelper.Answer, 1, -9999);
			}
			catch (Exception)
			{
				MessageBox.Show(string.Format(SurveyMsg.MsgNotSetDataCopy, oQuestion.QDefine.PAGE_ID), SurveyMsg.MsgCaptionError, MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		private void _003F155_003F(string _003F380_003F, SurveyDefine _003F381_003F, int _003F382_003F = 0, int _003F383_003F = 4999)
		{
			//IL_059b: Incompatible stack heights: 0 vs 1
			//IL_05fe: Expected O, but got Unknown
			//IL_062a: Expected O, but got Unknown
			int qUESTION_TYPE = _003F381_003F.QUESTION_TYPE;
			switch (qUESTION_TYPE)
			{
			case 3:
			case 9:
			{
				QMultiple qMultiple = new QMultiple();
				qMultiple.Init(_003F381_003F.PAGE_ID, _003F381_003F.COMBINE_INDEX, true);
				qMultiple.QuestionName = _003F380_003F;
				string[] array3 = (!SurveyHelper.AutoFill || !(_003F381_003F.FILLDATA != _003F487_003F._003F488_003F(""))) ? oLogicEngine.RecodeLogic(CurPageId, SurveyMsg.MsgProgramType, qUESTION_TYPE, _003F382_003F, _003F383_003F) : oAutoFill.RecodeFill(_003F381_003F);
				string sHOW_LOGIC = _003F381_003F.SHOW_LOGIC;
				if (sHOW_LOGIC != _003F487_003F._003F488_003F(""))
				{
					List<string> list = oBoldTitle.ParaToList(sHOW_LOGIC, _003F487_003F._003F488_003F("-Į"));
					if (list[0].Trim() != _003F487_003F._003F488_003F("") && _003F381_003F.IS_RANDOM == 0)
					{
						string[] array4 = oLogicEngine.aryCode(list[0], ',');
						List<string> list2 = new List<string>();
						for (int i = 0; i < array4.Count(); i++)
						{
							string[] array5 = array3;
							foreach (string text in array5)
							{
								if (text == array4[i].ToString())
								{
									list2.Add(text);
									break;
								}
							}
						}
						array3 = list2.ToArray();
					}
				}
				else if (_003F381_003F.IS_RANDOM > 0)
				{
					List<SurveyDetail> list3 = new List<SurveyDetail>();
					List<SurveyDetail>.Enumerator enumerator;
					for (int k = 0; k < array3.Count(); k++)
					{
						enumerator = qMultiple.QDetails.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								SurveyDetail current = enumerator.Current;
								if (current.CODE == array3[k].ToString())
								{
									list3.Add(current);
									break;
								}
							}
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
					}
					qMultiple.QDetails = list3;
					qMultiple.RandomDetails();
					List<string> list4 = new List<string>();
					enumerator = qMultiple.QDetails.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							SurveyDetail current2 = enumerator.Current;
							list4.Add(current2.CODE);
						}
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					array3 = list4.ToArray();
				}
				SurveyHelper.Answer = _003F487_003F._003F488_003F("");
				for (int l = 0; l < array3.Count(); l++)
				{
					qMultiple.SelectedValues.Add(array3[l].ToString());
					SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + qMultiple.QuestionName + _003F487_003F._003F488_003F("<") + array3[l].ToString();
				}
				SurveyHelper.Answer = oFunc.MID(SurveyHelper.Answer, 1, -9999);
				qMultiple.BeforeSave();
				qMultiple.Save(MySurveyId, SurveyHelper.SurveySequence);
				break;
			}
			case 2:
			case 8:
			{
				QSingle qSingle = new QSingle();
				qSingle.Init(_003F381_003F.PAGE_ID, _003F381_003F.COMBINE_INDEX, true);
				qSingle.QuestionName = _003F380_003F;
				string[] array2 = (!SurveyHelper.AutoFill || !(_003F381_003F.FILLDATA != _003F487_003F._003F488_003F(""))) ? oLogicEngine.RecodeLogic(CurPageId, SurveyMsg.MsgProgramType, qUESTION_TYPE, _003F382_003F, _003F383_003F) : ((!(_003F381_003F.DETAIL_ID != _003F487_003F._003F488_003F(""))) ? oAutoFill.RecodeFill(_003F381_003F) : oAutoFill.RecodeSingle(_003F381_003F, qSingle.QDetails));
				SurveyHelper.Answer = qSingle.QuestionName + _003F487_003F._003F488_003F("<") + array2[0].ToString();
				qSingle.SelectedCode = array2[0].ToString();
				qSingle.BeforeSave();
				qSingle.Save(MySurveyId, SurveyHelper.SurveySequence, true);
				break;
			}
			default:
			{
				QFill qFill = new QFill();
				qFill.Init(_003F381_003F.PAGE_ID, _003F381_003F.COMBINE_INDEX);
				qFill.QuestionName = _003F380_003F;
				string[] array = (!SurveyHelper.AutoFill || !(_003F381_003F.FILLDATA != _003F487_003F._003F488_003F(""))) ? oLogicEngine.RecodeLogic(CurPageId, SurveyMsg.MsgProgramType, qUESTION_TYPE, _003F382_003F, _003F383_003F) : oAutoFill.RecodeFill(_003F381_003F);
				SurveyHelper.Answer = ((QFill)/*Error near IL_05f8: Stack underflow*/).QuestionName + _003F487_003F._003F488_003F("<") + array[0].ToString();
				((QFill)/*Error near IL_061b: Stack underflow*/).FillText = array[0].ToString();
				string mySurveyId = MySurveyId;
				int surveySequence = SurveyHelper.SurveySequence;
				((QFill)/*Error near IL_063a: Stack underflow*/).Save(mySurveyId, surveySequence);
				break;
			}
			}
		}

		private void _003F156_003F(string _003F380_003F, SurveyDefine _003F381_003F, List<SurveyLogic> _003F384_003F, int _003F382_003F, int _003F383_003F)
		{
			int qUESTION_TYPE = _003F381_003F.QUESTION_TYPE;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = _003F382_003F; i <= _003F383_003F; i++)
			{
				SurveyLogic surveyLogic = _003F384_003F[i];
				foreach (string item in oLogicEngine.listLoopLevel(surveyLogic.FORMULA, surveyLogic.LOGIC_TYPE))
				{
					bool flag = true;
					if (dictionary.ContainsKey(item))
					{
						if (surveyLogic.NOTE == _003F487_003F._003F488_003F("["))
						{
							flag = false;
						}
						if (qUESTION_TYPE == 1 || qUESTION_TYPE == 2 || qUESTION_TYPE == 7 || qUESTION_TYPE == 8)
						{
							flag = false;
						}
					}
					if (flag)
					{
						string _003F375_003F = _003F157_003F(surveyLogic.RECODE_ANSWER, item);
						if (qUESTION_TYPE == 1 || qUESTION_TYPE == 7)
						{
							string value = oLogicEngine.stringResult(_003F375_003F);
							dictionary.Add(item, value);
							if (ErrorPageId != CurPageId && !_003F159_003F(item, _003F381_003F.GROUP_LEVEL, MySurveyId, surveyLogic))
							{
								ErrorPageId = CurPageId;
							}
						}
						else
						{
							string[] array = oLogicEngine.aryCode(_003F375_003F, ',');
							string value2 = _003F487_003F._003F488_003F("");
							if (dictionary.ContainsKey(item))
							{
								value2 = oFunc.ArrayToString(array, _003F487_003F._003F488_003F("-"), false, _003F487_003F._003F488_003F(""));
								dictionary[item] = dictionary[item] + _003F487_003F._003F488_003F("-") + value2;
							}
							else
							{
								if (qUESTION_TYPE == 2 || qUESTION_TYPE == 8)
								{
									if (array.Count() > 0)
									{
										value2 = array[0];
									}
									dictionary.Add(item, value2);
								}
								else
								{
									value2 = oFunc.ArrayToString(array, _003F487_003F._003F488_003F("-"), false, _003F487_003F._003F488_003F(""));
									dictionary.Add(item, value2);
								}
								if (ErrorPageId != CurPageId && !_003F159_003F(item, _003F381_003F.GROUP_LEVEL, MySurveyId, surveyLogic))
								{
									ErrorPageId = CurPageId;
								}
							}
						}
					}
				}
			}
			Dictionary<string, string>.KeyCollection.Enumerator enumerator2;
			switch (qUESTION_TYPE)
			{
			case 3:
			case 9:
			{
				QMultiple qMultiple = new QMultiple();
				qMultiple.Init(_003F381_003F.PAGE_ID, _003F381_003F.COMBINE_INDEX, true);
				string[] array2 = null;
				if (_003F381_003F.SHOW_LOGIC != _003F487_003F._003F488_003F(""))
				{
					List<string> list = oBoldTitle.ParaToList(_003F381_003F.SHOW_LOGIC, _003F487_003F._003F488_003F("-Į"));
					if (list[0].Trim() != _003F487_003F._003F488_003F("") && _003F381_003F.IS_RANDOM == 0)
					{
						array2 = oLogicEngine.aryCode(list[0].Trim(), ',');
					}
				}
				enumerator2 = dictionary.Keys.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						string current4 = enumerator2.Current;
						qMultiple.QuestionName = _003F380_003F + current4;
						string _003F90_003F = dictionary[current4];
						string[] array3 = oFunc.StringArrayDeleteDuplicate(oFunc.StringToArray(_003F90_003F, ','));
						if (array2 != null)
						{
							List<string> list2 = new List<string>();
							for (int j = 0; j < array2.Count(); j++)
							{
								string[] array4 = array3;
								foreach (string text3 in array4)
								{
									if (text3 == array2[j].ToString())
									{
										list2.Add(text3);
										break;
									}
								}
							}
							array3 = list2.ToArray();
						}
						else if (_003F381_003F.IS_RANDOM > 0)
						{
							List<SurveyDetail> list3 = new List<SurveyDetail>();
							List<SurveyDetail>.Enumerator enumerator3;
							for (int l = 0; l < array3.Count(); l++)
							{
								enumerator3 = qMultiple.QDetails.GetEnumerator();
								try
								{
									while (enumerator3.MoveNext())
									{
										SurveyDetail current5 = enumerator3.Current;
										if (current5.CODE == array3[l].ToString())
										{
											list3.Add(current5);
											break;
										}
									}
								}
								finally
								{
									((IDisposable)enumerator3).Dispose();
								}
							}
							qMultiple.QDetails = list3;
							qMultiple.RandomDetails();
							List<string> list4 = new List<string>();
							enumerator3 = qMultiple.QDetails.GetEnumerator();
							try
							{
								while (enumerator3.MoveNext())
								{
									SurveyDetail current6 = enumerator3.Current;
									list4.Add(current6.CODE);
								}
							}
							finally
							{
								((IDisposable)enumerator3).Dispose();
							}
							array3 = list4.ToArray();
						}
						SurveyHelper.Answer = _003F487_003F._003F488_003F("");
						for (int m = 0; m < array3.Count(); m++)
						{
							qMultiple.SelectedValues.Add(array3[m].ToString());
							SurveyHelper.Answer = SurveyHelper.Answer + _003F487_003F._003F488_003F("-") + qMultiple.QuestionName + _003F487_003F._003F488_003F("<") + array3[m].ToString();
						}
						SurveyHelper.Answer = oFunc.MID(SurveyHelper.Answer, 1, -9999);
					}
				}
				finally
				{
					((IDisposable)enumerator2).Dispose();
				}
				qMultiple.BeforeSave();
				qMultiple.Save(MySurveyId, SurveyHelper.SurveySequence);
				break;
			}
			case 2:
			case 8:
			{
				QSingle qSingle = new QSingle();
				qSingle.Init(_003F381_003F.PAGE_ID, _003F381_003F.COMBINE_INDEX, true);
				qSingle.QuestionName = _003F380_003F;
				enumerator2 = dictionary.Keys.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						string current3 = enumerator2.Current;
						qSingle.QuestionName = _003F380_003F + current3;
						string text2 = dictionary[current3];
						SurveyHelper.Answer = qSingle.QuestionName + _003F487_003F._003F488_003F("<") + text2;
						qSingle.SelectedCode = text2;
						qSingle.BeforeSave();
						qSingle.Save(MySurveyId, SurveyHelper.SurveySequence, false);
					}
				}
				finally
				{
					((IDisposable)enumerator2).Dispose();
				}
				break;
			}
			default:
			{
				QFill qFill = new QFill();
				qFill.Init(_003F381_003F.PAGE_ID, _003F381_003F.COMBINE_INDEX);
				qFill.QuestionName = _003F380_003F;
				enumerator2 = dictionary.Keys.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						string current2 = enumerator2.Current;
						qFill.QuestionName = _003F380_003F + current2;
						string text = dictionary[current2];
						SurveyHelper.Answer = qFill.QuestionName + _003F487_003F._003F488_003F("<") + text;
						qFill.FillText = text;
						qFill.Save(MySurveyId, SurveyHelper.SurveySequence);
					}
				}
				finally
				{
					((IDisposable)enumerator2).Dispose();
				}
				break;
			}
			}
		}

		private string _003F157_003F(string _003F385_003F, string _003F386_003F)
		{
			//IL_0108: Incompatible stack heights: 0 vs 1
			//IL_0122: Incompatible stack heights: 0 vs 1
			//IL_012e: Incompatible stack heights: 0 vs 2
			//IL_014b: Incompatible stack heights: 0 vs 1
			//IL_0165: Incompatible stack heights: 0 vs 1
			string text = _003F487_003F._003F488_003F("");
			string text2 = _003F487_003F._003F488_003F("_");
			string text3 = _003F487_003F._003F488_003F("");
			bool flag = false;
			string text4 = _003F385_003F + _003F487_003F._003F488_003F("!");
			int i = 0;
			goto IL_00ea;
			IL_00ea:
			for (; i < text4.Length; i++)
			{
				string text5 = text4[i].ToString();
				if (text5 == text2)
				{
					if ((int)/*Error near IL_010d: Stack underflow*/ != 0)
					{
						bool flag2 = text3 == _003F487_003F._003F488_003F("");
						text = (((int)/*Error near IL_0127: Stack underflow*/ == 0) ? (text + text3 + _003F386_003F) : ((string)/*Error near IL_006d: Stack underflow*/ + (string)/*Error near IL_006d: Stack underflow*/));
					}
					flag = true;
					text3 = _003F487_003F._003F488_003F("");
				}
				else if (flag)
				{
					_003F158_003F(text5, text3);
					string text6 = (string)/*Error near IL_0096: Stack underflow*/;
					if (text6 == _003F487_003F._003F488_003F(""))
					{
						bool flag3 = text3 == _003F487_003F._003F488_003F("");
						text = (((int)/*Error near IL_016a: Stack underflow*/ != 0) ? (text + text2) : (text + text3 + _003F386_003F + text5));
						flag = false;
						text3 = _003F487_003F._003F488_003F("");
					}
					else
					{
						text3 = text6;
					}
				}
				else
				{
					text += text5;
				}
			}
			return text;
			IL_018d:
			goto IL_00ea;
		}

		private string _003F158_003F(string _003F362_003F, string _003F387_003F)
		{
			//IL_0054: Incompatible stack heights: 0 vs 2
			//IL_0064: Incompatible stack heights: 0 vs 1
			//IL_0069: Invalid comparison between Unknown and I4
			string result = _003F487_003F._003F488_003F("");
			string text = _003F487_003F._003F488_003F("\u0002žɣ\u0338ѽԶذ\u0737࠶त\u0a29ଫహ൴\u0e36ཬၑᅋቒጦᐤᕷᙶᜰᠲ\u1926ᨦᬢ\u1c27ᴣṟἽ‡");
			string text2 = _003F487_003F._003F488_003F(":ĸȺ\u0334в\u0530ز\u0734࠺स");
			if (text.IndexOf(_003F362_003F) == -1)
			{
				text2.IndexOf(_003F362_003F);
				if (/*Error near IL_0059: Stack underflow*/ > /*Error near IL_0059: Stack underflow*/)
				{
					int length = _003F387_003F.Length;
					if ((int)/*Error near IL_0069: Stack underflow*/ > 0)
					{
						result = _003F387_003F + _003F362_003F;
					}
				}
				else
				{
					result = _003F387_003F + _003F362_003F;
				}
			}
			return result;
		}

		private bool _003F159_003F(string _003F386_003F, string _003F388_003F, string _003F389_003F, SurveyLogic _003F381_003F)
		{
			//IL_00a3: Incompatible stack heights: 0 vs 3
			string text = _003F487_003F._003F488_003F("");
			if (!(_003F388_003F == _003F487_003F._003F488_003F("@")))
			{
				if (!(_003F388_003F == _003F487_003F._003F488_003F("C")))
				{
					return true;
				}
				text = _003F487_003F._003F488_003F("Uśɔ\u0363Э՚\u0656ݟ\u0866प");
			}
			else
			{
				text = _003F487_003F._003F488_003F("ZŖɟ\u0366Ъ");
			}
			if (!oFunc.isMatch(_003F386_003F, text))
			{
				string[] array = new string[6];
				string text2 = _003F487_003F._003F488_003F("间剮ɣ\u0327ѫ刽妾璼邊㤁੫\u0b3e\u0c73㴜\u0e5eཎ၉ᅆቌፂ燰䛔挛舚鏭\ue600");
				((object[])/*Error near IL_00a9: Stack underflow*/)[(long)/*Error near IL_00a9: Stack underflow*/] = text2;
				((object[])/*Error near IL_00a9: Stack underflow*/)[1] = Environment.NewLine;
				((object[])/*Error near IL_00b1: Stack underflow*/)[2] = _003F487_003F._003F488_003F("叶臥搸袨筻䮒錊賽纕嚺禠君埱閔㸛内䏾ᅳስ፻␔仸韷憆⠃");
				((object[])/*Error near IL_00be: Stack underflow*/)[3] = Environment.NewLine;
				((object[])/*Error near IL_00c6: Stack underflow*/)[4] = Environment.NewLine;
				((object[])/*Error near IL_00ce: Stack underflow*/)[5] = _003F487_003F._003F488_003F("诺掆跒䴠䯨敧嗊麎盜猏和彚\uf300");
				MessageBox.Show(string.Format(string.Concat((string[])/*Error near IL_00e0: Stack underflow*/), _003F389_003F, _003F381_003F.PAGE_ID, _003F381_003F.INNER_INDEX.ToString()), _003F487_003F._003F488_003F("ZłɅ\u034aрՆ錛賮"), MessageBoxButton.OK, MessageBoxImage.Hand);
				return false;
			}
			return true;
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
			Uri resourceLocator = new Uri(_003F487_003F._003F488_003F("\u0006ůɔ\u0355ќԊ٠\u0743ࡑ\u0949ਤ\u0b7d\u0c72൱\u0e6b\u0f75ၷᅽቹ።ᐺᕢᙺ\u1777ᡦ\u193f\u1a7d\u1b6bᱮ\u1d63ṯὯ\u2060Ⅶ≡⍩\u242b╼♢❯⡭"), UriKind.Relative);
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
				((RecodeInfo)_003F350_003F).Loaded += _003F80_003F;
				break;
			case 2:
				gridContent = (Grid)_003F350_003F;
				break;
			case 3:
				txtQuestionTitle = (TextBlock)_003F350_003F;
				break;
			case 4:
				txtCircleTitle = (TextBlock)_003F350_003F;
				break;
			case 5:
				scrollNote = (ScrollViewer)_003F350_003F;
				break;
			case 6:
				txtQuestionNote = (TextBlock)_003F350_003F;
				break;
			case 7:
				txtSurvey = (TextBlock)_003F350_003F;
				break;
			case 8:
				btnAttach = (Button)_003F350_003F;
				btnAttach.Click += _003F85_003F;
				break;
			case 9:
				btnNav = (Button)_003F350_003F;
				btnNav.Click += _003F58_003F;
				break;
			default:
				_contentLoaded = true;
				break;
			}
			return;
			IL_0031:
			goto IL_003b;
		}
	}
}
