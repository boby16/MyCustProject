using Gssy.Capi.BIZ;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;
using Gssy.Capi.QEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Gssy.Capi.Class
{
	public class PageNav : Page
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003F7_003F
		{
			public static readonly _003F7_003F _003C_003E9 = new _003F7_003F();

			public static DispatcherOperationCallback _003C_003E9__7_0;

			internal object _003F338_003F(object _003F485_003F)
			{
				((DispatcherFrame)_003F485_003F).Continue = false;
				return null;
			}
		}

		public LogicEngine oLogicEngine = new LogicEngine();

		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		private SurveyLogicDal oSurveyLogicDal = new SurveyLogicDal();

		public bool NextPage(NavBase _003F429_003F, NavigationService _003F430_003F)
		{
			//IL_00f4: Incompatible stack heights: 0 vs 3
			//IL_0117: Incompatible stack heights: 0 vs 1
			//IL_0121: Incompatible stack heights: 0 vs 1
			//IL_0147: Incompatible stack heights: 0 vs 2
			//IL_016a: Incompatible stack heights: 0 vs 1
			//IL_0174: Incompatible stack heights: 0 vs 1
			//IL_0200: Incompatible stack heights: 0 vs 3
			//IL_0223: Incompatible stack heights: 0 vs 1
			//IL_022d: Incompatible stack heights: 0 vs 1
			//IL_0253: Incompatible stack heights: 0 vs 2
			//IL_0276: Incompatible stack heights: 0 vs 1
			//IL_0280: Incompatible stack heights: 0 vs 1
			int surveySequence = SurveyHelper.SurveySequence;
			string roadMapVersion = SurveyHelper.RoadMapVersion;
			string surveyID = SurveyHelper.SurveyID;
			string navCurPage = SurveyHelper.NavCurPage;
			_003F429_003F.PageStartTime = SurveyHelper.PageStartTime;
			_003F429_003F.RecordFileName = SurveyHelper.RecordFileName;
			_003F429_003F.RecordStartTime = SurveyHelper.RecordStartTime;
			SurveyRoadMap byPageId = oSurveyRoadMapDal.GetByPageId(navCurPage, roadMapVersion);
			string text = _003F487_003F._003F488_003F("\tŪɁ\u0355хՉفݤࡗ\u0948\u0a44\u0b7aఽ\u0d5e\u0e75ཀྵၹᅵችፄᑢᕴᙦᝧᠱᥔ\u1a7d\u1b7f\u1c7aᵴṆ\u1f7e\u2067ⅹ∫⍕④╦♫❧⡧⤢");
			string value = _003F487_003F._003F488_003F("\"") + byPageId.FORM_NAME.ToUpper() + _003F487_003F._003F488_003F("\"");
			bool flag = text.ToUpper().Contains(value);
			if (SurveyHelper.AutoCapture && !flag)
			{
				string[] obj = new string[6]
				{
					SurveyHelper.SurveyID,
					_003F487_003F._003F488_003F("^"),
					SurveyHelper.NavCurPage,
					null,
					null,
					null
				};
				if (!(SurveyHelper.CircleACode == _003F487_003F._003F488_003F("")))
				{
					string text5 = _003F487_003F._003F488_003F("]ŀ") + SurveyHelper.CircleACode;
				}
				else
				{
					_003F487_003F._003F488_003F("");
				}
				((object[])/*Error near IL_0122: Stack underflow*/)[(long)/*Error near IL_0122: Stack underflow*/] = (object)/*Error near IL_0122: Stack underflow*/;
				/*Error near IL_0122: Stack underflow*/;
				if (!(SurveyHelper.CircleBCode == _003F487_003F._003F488_003F("")))
				{
					string text6 = _003F487_003F._003F488_003F("]Ń") + SurveyHelper.CircleBCode;
				}
				else
				{
					_003F487_003F._003F488_003F("");
				}
				((object[])/*Error near IL_0175: Stack underflow*/)[(long)/*Error near IL_0175: Stack underflow*/] = (object)/*Error near IL_0175: Stack underflow*/;
				((object[])/*Error near IL_0175: Stack underflow*/)[5] = _003F487_003F._003F488_003F("*ũɲ\u0366");
				string str = string.Concat((string[])/*Error near IL_0187: Stack underflow*/);
				str = Directory.GetCurrentDirectory() + _003F487_003F._003F488_003F("[Ŗɭ\u036bѷխ\u065d") + str;
				if (File.Exists(str))
				{
					object[] obj2 = new object[10]
					{
						SurveyHelper.SurveyID,
						_003F487_003F._003F488_003F("^"),
						SurveyHelper.NavCurPage,
						null,
						null,
						null,
						null,
						null,
						null,
						null
					};
					if (!(SurveyHelper.CircleACode == _003F487_003F._003F488_003F("")))
					{
						string text7 = _003F487_003F._003F488_003F("]ŀ") + SurveyHelper.CircleACode;
					}
					else
					{
						_003F487_003F._003F488_003F("");
					}
					((object[])/*Error near IL_022e: Stack underflow*/)[(long)/*Error near IL_022e: Stack underflow*/] = (object)/*Error near IL_022e: Stack underflow*/;
					/*Error near IL_022e: Stack underflow*/;
					if (!(SurveyHelper.CircleBCode == _003F487_003F._003F488_003F("")))
					{
						string text8 = _003F487_003F._003F488_003F("]Ń") + SurveyHelper.CircleBCode;
					}
					else
					{
						_003F487_003F._003F488_003F("");
					}
					((object[])/*Error near IL_0281: Stack underflow*/)[(long)/*Error near IL_0281: Stack underflow*/] = (object)/*Error near IL_0281: Stack underflow*/;
					((object[])/*Error near IL_0281: Stack underflow*/)[5] = _003F487_003F._003F488_003F("^");
					((object[])/*Error near IL_028e: Stack underflow*/)[6] = DateTime.Now.Hour;
					((object[])/*Error near IL_02a4: Stack underflow*/)[7] = DateTime.Now.Minute;
					((object[])/*Error near IL_02ba: Stack underflow*/)[8] = DateTime.Now.Second;
					((object[])/*Error near IL_02d0: Stack underflow*/)[9] = _003F487_003F._003F488_003F("*ũɲ\u0366");
					str = string.Concat((object[])/*Error near IL_02e3: Stack underflow*/);
					str = Directory.GetCurrentDirectory() + _003F487_003F._003F488_003F("[Ŗɭ\u036bѷխ\u065d") + str;
				}
				_003F289_003F(str, (int)SurveyHelper.Screen_LeftTop);
			}
			try
			{
				if (_003F429_003F.GroupLevel == _003F487_003F._003F488_003F(""))
				{
					_003F429_003F.NextPage(surveyID, surveySequence, navCurPage, roadMapVersion);
				}
				else
				{
					_003F429_003F.NextCirclePage(surveyID, surveySequence, navCurPage, roadMapVersion);
					SurveyHelper.CircleACount = _003F429_003F.CircleACount;
					SurveyHelper.CircleACurrent = _003F429_003F.CircleACurrent;
					if (_003F429_003F.IsLastA && (_003F429_003F.GroupPageType == 0 || _003F429_003F.GroupPageType == 2))
					{
						SurveyHelper.CircleACode = _003F487_003F._003F488_003F("");
						SurveyHelper.CircleACodeText = _003F487_003F._003F488_003F("");
					}
					if (_003F429_003F.GroupLevel == _003F487_003F._003F488_003F("C"))
					{
						SurveyHelper.CircleBCount = _003F429_003F.CircleBCount;
						SurveyHelper.CircleBCurrent = _003F429_003F.CircleBCurrent;
						if (_003F429_003F.IsLastB && (_003F429_003F.GroupPageType == 10 || _003F429_003F.GroupPageType == 12 || _003F429_003F.GroupPageType == 30 || _003F429_003F.GroupPageType == 32))
						{
							SurveyHelper.CircleBCode = _003F487_003F._003F488_003F("");
							SurveyHelper.CircleBCodeText = _003F487_003F._003F488_003F("");
						}
					}
				}
				string text2 = oLogicEngine.Route(_003F429_003F.RoadMap.FORM_NAME);
				SurveyHelper.RoadMapVersion = _003F429_003F.RoadMap.VERSION_ID.ToString();
				string uriString = string.Format(_003F487_003F._003F488_003F("TłɁ\u034aК\u0530رݼ\u086c५\u0a76୰౻\u0d76\u0e62\u0f7cၻᅽረጽᐼᔣᘡ\u175bᡥ\u196e\u1a7dᬦᱳ\u1d37ṻἫ⁼Ⅲ≯⍭"), text2);
				if (text2.Substring(0, 1) == _003F487_003F._003F488_003F("@"))
				{
					uriString = string.Format(_003F487_003F._003F488_003F("[ŋɊ\u0343Нԉ؊\u0745ࡓ\u0952\u0a4d\u0b49౼ൿ\u0e69\u0f75\u1074ᅴሣጴᐻᔺᘺᝂ\u187a\u1977\u1a66\u1b40\u1c7d\u1d61ṧὩ\u2068ⅾ∦⍳\u2437╻☫❼⡢⥯⩭"), text2);
				}
				if (text2 == SurveyHelper.CurPageName)
				{
					_003F430_003F.Refresh();
				}
				else
				{
					if (!FormIsOK(text2))
					{
						string text3 = string.Format(SurveyMsg.MsgErrorJump, surveyID, navCurPage, _003F429_003F.RoadMap.VERSION_ID, _003F429_003F.RoadMap.PAGE_ID, _003F429_003F.RoadMap.FORM_NAME);
						MessageBox.Show(SurveyMsg.MsgErrorRoadmap + Environment.NewLine + Environment.NewLine + text3 + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						oLogicEngine.OutputResult(text3, _003F487_003F._003F488_003F("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"));
						return false;
					}
					_003F430_003F.RemoveBackEntry();
					_003F430_003F.Navigate(new Uri(uriString));
				}
				SurveyHelper.SurveySequence = surveySequence + 1;
				SurveyHelper.NavCurPage = _003F429_003F.RoadMap.PAGE_ID;
				SurveyHelper.CurPageName = text2;
				SurveyHelper.NavGoBackTimes = 0;
				SurveyHelper.NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");
				SurveyHelper.NavLoad = 0;
			}
			catch (Exception)
			{
				string text4 = string.Format(SurveyMsg.MsgErrorJump, surveyID, navCurPage, _003F429_003F.RoadMap.VERSION_ID, _003F429_003F.RoadMap.PAGE_ID, _003F429_003F.RoadMap.FORM_NAME);
				MessageBox.Show(SurveyMsg.MsgErrorRoadmap + Environment.NewLine + Environment.NewLine + text4 + SurveyMsg.MsgErrorEnd, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
				oLogicEngine.OutputResult(text4, _003F487_003F._003F488_003F("Nŭɻ\u0363эխ٥ݳ\u0862प\u0a4f୭౦"));
				return false;
			}
			return true;
		}

		public void PageDataLog(int _003F393_003F, List<VEAnswer> _003F370_003F, Button _003F431_003F, string _003F397_003F)
		{
			_003F431_003F.IsEnabled = false;
			_003F431_003F.Content = SurveyMsg.MsgWaitingSave;
			Thread.Sleep(_003F393_003F);
			_003F431_003F.Content = SurveyMsg.MsgbtnNav_Content;
			_003F431_003F.IsEnabled = true;
		}

		public bool CheckLogic(string _003F432_003F)
		{
			if (_003F432_003F == _003F487_003F._003F488_003F("Aŉɀ\u034aћՑه\u0742") && SurveyHelper.AutoFill)
			{
				return true;
			}
			new List<SurveyLogic>();
			foreach (SurveyLogic item in oSurveyLogicDal.GetCheckLogic(_003F432_003F))
			{
				string fORMULA = item.FORMULA;
				if (oLogicEngine.boolResult(fORMULA))
				{
					string _003F379_003F = oLogicEngine.strShowText(item.LOGIC_MESSAGE, true);
					_003F379_003F = oLogicEngine.strShowText(_003F379_003F, false);
					string nOTE = item.NOTE;
					switch (item.IS_ALLOW_PASS)
					{
					case 0:
						MessageBox.Show(_003F379_003F, SurveyMsg.MsgCaption, MessageBoxButton.OK, MessageBoxImage.Hand);
						return false;
					case 1:
						if (!SurveyHelper.AutoFill && MessageBox.Show(_003F379_003F + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.No))
						{
							return false;
						}
						break;
					case 2:
						if (MessageBox.Show(_003F379_003F + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassModify, SurveyMsg.MsgCaption, MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No).Equals(MessageBoxResult.Yes))
						{
							SurveyHelper.QueryEditQn = nOTE;
							EditAnswerSingle editAnswerSingle2 = new EditAnswerSingle();
							editAnswerSingle2.oLogicEngine = oLogicEngine;
							editAnswerSingle2.ShowDialog();
						}
						return false;
					case 3:
						if (!SurveyHelper.AutoFill)
						{
							MessageBoxResult messageBoxResult = MessageBox.Show(_003F379_003F + Environment.NewLine + Environment.NewLine + SurveyMsg.MsgPassModifyCheck, SurveyMsg.MsgCaption, MessageBoxButton.YesNoCancel, MessageBoxImage.Asterisk, MessageBoxResult.Cancel);
							if (messageBoxResult.Equals(MessageBoxResult.Yes))
							{
								SurveyHelper.QueryEditQn = nOTE;
								EditAnswerSingle editAnswerSingle = new EditAnswerSingle();
								editAnswerSingle.oLogicEngine = oLogicEngine;
								editAnswerSingle.ShowDialog();
								return false;
							}
							if (messageBoxResult.Equals(MessageBoxResult.Cancel))
							{
								return false;
							}
						}
						break;
					}
				}
			}
			return true;
		}

		public bool FormIsOK(string _003F433_003F)
		{
			string b = _003F433_003F.ToUpper();
			try
			{
				foreach (TypeInfo definedType in Assembly.GetExecutingAssembly().DefinedTypes)
				{
					if (definedType.Name.ToUpper() == b)
					{
						return true;
					}
				}
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void Refresh()
		{
			//IL_0026: Incompatible stack heights: 0 vs 3
			//IL_003d: Incompatible stack heights: 0 vs 2
			DispatcherFrame dispatcherFrame = new DispatcherFrame();
			Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
			if (_003F7_003F._003C_003E9__7_0 == null)
			{
				new DispatcherOperationCallback(_003F7_003F._003C_003E9._003F338_003F);
				_003F7_003F._003C_003E9__7_0 = (DispatcherOperationCallback)/*Error near IL_001c: Stack underflow*/;
			}
			((Dispatcher)/*Error near IL_0043: Stack underflow*/).BeginInvoke((DispatcherPriority)/*Error near IL_0043: Stack underflow*/, (Delegate)/*Error near IL_0043: Stack underflow*/, (object)dispatcherFrame);
			Dispatcher.PushFrame(dispatcherFrame);
		}

		private void _003F289_003F(string _003F434_003F, int _003F435_003F)
		{
			//IL_002a: Incompatible stack heights: 0 vs 1
			//IL_0044: Incompatible stack heights: 0 vs 1
			if (new ScreenCapture().Capture(_003F434_003F, _003F435_003F))
			{
				bool autoFill = SurveyHelper.AutoFill;
				if ((int)/*Error near IL_002f: Stack underflow*/ == 0)
				{
					string text = SurveyMsg.MsgScreenCaptureDone + Environment.NewLine + _003F434_003F;
					MessageBox.Show((string)/*Error near IL_0049: Stack underflow*/);
				}
			}
			else
			{
				MessageBox.Show(SurveyMsg.MsgScreenCaptureFail + Environment.NewLine + _003F434_003F);
			}
		}
	}
}
