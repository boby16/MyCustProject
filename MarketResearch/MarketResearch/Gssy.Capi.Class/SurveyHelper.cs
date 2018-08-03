using Gssy.Capi.Entities;
using System;
using System.Collections.Generic;

namespace Gssy.Capi.Class
{
	public static class SurveyHelper
	{
		public static string SurveyStart = _003F487_003F._003F488_003F("GŊɖ\u0358");

		public static string SurveyFirstPage = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065aݑࡐ\u0947\u0a53");

		public static string SurveyLastPage = _003F487_003F._003F488_003F("@");

		public static string SurveyCodePage = _003F487_003F._003F488_003F("Xşɛ\u035eт՟\u065a\u0747ࡌ\u0946\u0a44");

		public static int SurveySequence = 0;

		public static string NavCurPage = _003F487_003F._003F488_003F("");

		public static string CurPageName = _003F487_003F._003F488_003F("");

		public static int NavLoad = 0;

		public static string NavOperation = _003F487_003F._003F488_003F("HŪɶ\u036eѣխ");

		public static int NavGoBackTimes = 0;

		public static string RoadMapVersion = _003F487_003F._003F488_003F("1");

		public static string IsTouch = _003F487_003F._003F488_003F("Dſɟ\u0365Ѽիٯݙ\u0863॥੯ୱ\u0c64");

		public static bool HistoryMode = false;

		public static bool OptionMode = false;

		public static string DebugFlagFile = _003F487_003F._003F488_003F("Jķɐ\u035fя՚\u065cݔࡃ\u0954ਪ\u0b57ౚ\u0d55");

		public static bool Debug = false;

		public static bool TestVersion = false;

		public static bool IsDevelopment = false;

		public static string ShowAutoFill = _003F487_003F._003F488_003F("BŸɠ\u0379ьչٿݥࡏॡ੫୪ౚ൰\u0e71\u0f77\u1064");

		public static string FillMode = _003F487_003F._003F488_003F("2");

		public static bool AutoFill = false;

		public static bool AutoCapture = false;

		public static double Screen_LeftTop = 0.0;

		public static string StopFillPage = _003F487_003F._003F488_003F(" ĳȢ");

		public static string Only1CodeMode1 = _003F487_003F._003F488_003F(" ĳȢ");

		public static string Only1CodeMode2 = _003F487_003F._003F488_003F(" İȢ");

		public static string Only1CodeMode3 = _003F487_003F._003F488_003F(" ıȢ");

		public static string Only1CodeMode4 = _003F487_003F._003F488_003F(" ĶȢ");

		public static string ShowAutoDo = _003F487_003F._003F488_003F("\\Ŧɢͻъտٽݧࡃ३ਗ਼୰\u0c71\u0d77\u0e64");

		public static bool AutoDo = false;

		public static List<string> AutoDo_listCity = new List<string>();

		public static int AutoDo_CityOrder = 1;

		public static int AutoDo_StartOrder = 0;

		public static int AutoDo_Total = 0;

		public static int AutoDo_Count = 1;

		public static int AutoDo_Create = 0;

		public static int AutoDo_Exist = 0;

		public static int AutoDo_Filled = 0;

		public static string Survey_Status = _003F487_003F._003F488_003F("");

		public static DateTime AutoDo_Start;

		public static bool RecordIsRunning = false;

		public static string RecordFileName = _003F487_003F._003F488_003F("");

		public static int RecordHz = 22050;

		public static int RecordDevice = 0;

		public static string MP3MaxMinutes = _003F487_003F._003F488_003F("2ĺȱ");

		public static DateTime RecordStartTime = DateTime.Now;

		public static string QueryEditSurveyId = _003F487_003F._003F488_003F("");

		public static string QueryEditQn = _003F487_003F._003F488_003F("");

		public static string QueryEditQTitle = _003F487_003F._003F488_003F("");

		public static string QueryEditQName = _003F487_003F._003F488_003F("");

		public static string QueryEditDetailID = _003F487_003F._003F488_003F("");

		public static string QueryEditCODE = _003F487_003F._003F488_003F("");

		public static string QueryEditCODE_TEXT = _003F487_003F._003F488_003F("");

		public static int QueryEditSequence = 0;

		public static bool QueryEditConfirm = false;

		public static bool QueryEditMemModel = false;

		public static string AttachSurveyId = _003F487_003F._003F488_003F("");

		public static string AttachQName = _003F487_003F._003F488_003F("");

		public static string AttachPageId = _003F487_003F._003F488_003F("");

		public static int AttachCount = 0;

		public static bool AttachReadOnlyModel = false;

		public static string SurveyID = _003F487_003F._003F488_003F("");

		public static string SurveyCity = _003F487_003F._003F488_003F("");

		public static string SurveyCityText = _003F487_003F._003F488_003F("");

		public static int CircleACount = 0;

		public static int CircleACurrent = 0;

		public static string CircleACode = _003F487_003F._003F488_003F("");

		public static string CircleACodeText = _003F487_003F._003F488_003F("");

		public static int CircleBCount = 0;

		public static int CircleBCurrent = 0;

		public static string CircleBCode = _003F487_003F._003F488_003F("");

		public static string CircleBCodeText = _003F487_003F._003F488_003F("");

		public static string SurveyExtend1 = _003F487_003F._003F488_003F("");

		public static string SurveyExtend2 = _003F487_003F._003F488_003F("");

		public static string SurveyExtend3 = _003F487_003F._003F488_003F("");

		public static string Answer = _003F487_003F._003F488_003F("");

		public static List<VEAnswer> SurveyExtend = new List<VEAnswer>();

		public static int BtnFontSize = 26;

		public static int BtnHeight = 46;

		public static int BtnWidth = 280;

		public static int BtnMediumFontSize = 22;

		public static int BtnMediumHeight = 40;

		public static int BtnMediumWidth = 280;

		public static int BtnSmallFontSize = 18;

		public static int BtnSmallHeight = 36;

		public static int BtnSmallWidth = 280;

		public static List<SurveyDefine> G_SurveyDefine;

		public static List<SurveyDetail> G_SurveyDetail;

		public static List<SurveyRoadMap> G_SurveyRoadMap;

		public static List<SurveyLogic> G_SurveyLogic;

		public static DateTime PageStartTime
		{
			get;
			set;
		}

		public static void SurveyExtend_Add(string _003F467_003F, string _003F371_003F, string _003F372_003F, int _003F468_003F)
		{
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = _003F467_003F;
			vEAnswer.CODE = _003F371_003F;
			vEAnswer.CODE_TEXT = _003F372_003F;
			vEAnswer.SEQUENCE_ID = _003F468_003F;
			SurveyExtend.Add(vEAnswer);
		}

		public static void SurveyExtend_Remove(string _003F467_003F, string _003F371_003F, string _003F372_003F, int _003F468_003F)
		{
			VEAnswer vEAnswer = new VEAnswer();
			vEAnswer.QUESTION_NAME = _003F467_003F;
			vEAnswer.CODE = _003F371_003F;
			vEAnswer.CODE_TEXT = _003F372_003F;
			vEAnswer.SEQUENCE_ID = _003F468_003F;
			SurveyExtend.Remove(vEAnswer);
		}

		public static void Init()
		{
		}

		public static void Load()
		{
		}

		public static void Save()
		{
		}

		public static string ShowInfo()
		{
			return _003F487_003F._003F488_003F("") + Environment.NewLine + _003F487_003F._003F488_003F("$ĥȪ\u032bШԩخܯ\u082c鳾奸按巋廎死\u0f37\u1034ᄵሺጻᐸᔹᘾ\u173fᠼ") + Environment.NewLine + _003F487_003F._003F488_003F("闩剱紓僳йԢء") + SurveyID + Environment.NewLine + _003F487_003F._003F488_003F("埋弆ȹ\u0322С") + SurveyCity + Environment.NewLine + _003F487_003F._003F488_003F("*īȨ\u0329Юԯجܭ࠲勲蠧梫帽ഷ\u0e34\u0f35\u103aᄻሸጹᐾᔿᘼ") + Environment.NewLine + _003F487_003F._003F488_003F("闩剱岊儓йԢء") + SurveySequence.ToString() + Environment.NewLine + _003F487_003F._003F488_003F("彃卂驻\u035bщ\u0559\u0659\u0740ࡇ\u0949ਖ਼\u0b4c\u0c40ഹย༡") + RoadMapVersion + Environment.NewLine + _003F487_003F._003F488_003F("彞十驾\u035aшՏقݙࡌ\u0940ਹଢడ") + NavCurPage + Environment.NewLine + _003F487_003F._003F488_003F("彜千驸\u034aф\u0558لݗࡉ\u0947\u0a48\u0b41హഢม") + CurPageName + Environment.NewLine + _003F487_003F._003F488_003F("寻茬曈䱘йԢء") + NavOperation + Environment.NewLine + _003F487_003F._003F488_003F("$ĥȪ\u032bШԩخܯ\u082cर喥碡忏桼ห\u0f37\u1034ᄵሺጻᐸᔹᘾ\u173fᠼ") + Environment.NewLine + _003F487_003F._003F488_003F("Oĭ幎岡瞥鶑炌墭箩椾潴ହఢഡ") + CircleACount.ToString() + Environment.NewLine + _003F487_003F._003F488_003F("Qį幌岧瞣鶓炎塚婅羃喬碪楴ഹย༡") + CircleACurrent.ToString() + Environment.NewLine + _003F487_003F._003F488_003F("Qį幌岧瞣鶓炎䤲䝛餎鉿䗦琅ഹย༡") + CircleACode + Environment.NewLine + _003F487_003F._003F488_003F("Qį幌岧瞣鶓炎䤲䝛餎鉿媀垽ഹย༡") + CircleACodeText + Environment.NewLine + _003F487_003F._003F488_003F("Lĭ幎岡瞥鶑炌墭箩椾潴ହఢഡ") + CircleBCount.ToString() + Environment.NewLine + _003F487_003F._003F488_003F("Rį幌岧瞣鶓炎塚婅羃喬碪楴ഹย༡") + CircleBCurrent.ToString() + Environment.NewLine + _003F487_003F._003F488_003F("Rį幌岧瞣鶓炎䤲䝛餎鉿䗦琅ഹย༡") + CircleBCode + Environment.NewLine + _003F487_003F._003F488_003F("Rį幌岧瞣鶓炎䤲䝛餎鉿媀垽ഹย༡") + CircleBCodeText;
		}

		public static string ShowPageAnswer(List<VEAnswer> _003F469_003F)
		{
			string text = _003F487_003F._003F488_003F("'Ĥȥ\u032aЫԨةܮ\u082fऱ啃奂鑻癙晄༫\u1037ᄴስጺᐻᔸᘹ\u173eᠿ\u193c");
			foreach (VEAnswer item in _003F469_003F)
			{
				text = text + Environment.NewLine + _003F487_003F._003F488_003F("颜勴ȸ\u0321") + item.QUESTION_NAME + _003F487_003F._003F488_003F("(ħȦ\u0325笒紂ظܡ") + item.CODE;
			}
			return text + Environment.NewLine + ShowInfo();
		}
	}
}
