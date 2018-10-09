using System;
using System.Collections.Generic;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.Class
{
	public static class SurveyHelper
	{
		public static DateTime PageStartTime { get; set; }

		public static void SurveyExtend_Add(string string_0, string string_1, string string_2, int int_0)
		{
			VEAnswer veanswer = new VEAnswer();
			veanswer.QUESTION_NAME = string_0;
			veanswer.CODE = string_1;
			veanswer.CODE_TEXT = string_2;
			veanswer.SEQUENCE_ID = int_0;
			SurveyHelper.SurveyExtend.Add(veanswer);
		}

		public static void SurveyExtend_Remove(string string_0, string string_1, string string_2, int int_0)
		{
			VEAnswer veanswer = new VEAnswer();
			veanswer.QUESTION_NAME = string_0;
			veanswer.CODE = string_1;
			veanswer.CODE_TEXT = string_2;
			veanswer.SEQUENCE_ID = int_0;
			SurveyHelper.SurveyExtend.Remove(veanswer);
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
			return "" + Environment.NewLine + "=========问卷标准参数==========" + Environment.NewLine 
                + "问卷编号:  " + SurveyHelper.SurveyID + Environment.NewLine 
                + "城市:  " + SurveyHelper.SurveyCity + Environment.NewLine
                + "=========导航控制==========" + Environment.NewLine 
                + "问卷序列:  " + SurveyHelper.SurveySequence.ToString() + Environment.NewLine 
                + "当前页VERSION_ID:  " + SurveyHelper.RoadMapVersion + Environment.NewLine 
                + "当前页PAGE_ID:  " + SurveyHelper.NavCurPage + Environment.NewLine 
                + "当前页FORM_NAME:  " + SurveyHelper.CurPageName + Environment.NewLine 
                + "导航操作:  " + SurveyHelper.NavOperation + Environment.NewLine 
                + "========= 循环参数 ==========" + Environment.NewLine 
                + "A 层循环题的循环总数:  " + SurveyHelper.CircleACount.ToString() + Environment.NewLine 
                + "A 层循环题的当前的循环数:  " + SurveyHelper.CircleACurrent.ToString() + Environment.NewLine 
                + "A 层循环题的主体选项代码:  " + SurveyHelper.CircleACode + Environment.NewLine
                + "A 层循环题的主体选项内容:  " + SurveyHelper.CircleACodeText + Environment.NewLine
                + "B 层循环题的循环总数:  " + SurveyHelper.CircleBCount.ToString() + Environment.NewLine 
                + "B 层循环题的当前的循环数:  " + SurveyHelper.CircleBCurrent.ToString() + Environment.NewLine
                + "B 层循环题的主体选项代码:  " + SurveyHelper.CircleBCode + Environment.NewLine 
                + "B 层循环题的主体选项内容:  " + SurveyHelper.CircleBCodeText;
		}

		public static string ShowPageAnswer(List<VEAnswer> list_0)
		{
			string text = "========= 当前页答案 ==========";
			foreach (VEAnswer veanswer in list_0)
			{
				text = string.Concat(new string[]
				{
					text,
					Environment.NewLine,
					"题号: ",
					veanswer.QUESTION_NAME,
					"    编码: ",
					veanswer.CODE
				});
			}
			text = text + Environment.NewLine + SurveyHelper.ShowInfo();
			return text;
		}

		public static string SurveyStart = "CITY";

		public static string SurveyFirstPage = "SURVEY_USER";

		public static string SurveyLastPage = "A";

		public static string SurveyCodePage = "SURVEY_CODE";

		public static int SurveySequence = 0;

		public static string NavCurPage = "";

		public static string CurPageName = "";

		public static int NavLoad = 0;

		public static string NavOperation = "Normal";

		public static int NavGoBackTimes = 0;

		public static string RoadMapVersion = "0";

		public static string IsTouch = "IsTouch_false";

		public static bool HistoryMode = false;

		public static bool OptionMode = false;

		public static string DebugFlagFile = "D:\\TESTSEQ.TXT";

		public static bool Debug = false;

		public static bool TestVersion = false;

		public static bool IsDevelopment = false;

		public static string ShowAutoFill = "ShowAutoFill_true";

		public static string FillMode = "3";

		public static bool AutoFill = false;

		public static bool AutoCapture = false;

		public static double Screen_LeftTop = 0.0;

		public static string StopFillPage = "#1#";

		public static string Only1CodeMode1 = "#1#";

		public static string Only1CodeMode2 = "#2#";

		public static string Only1CodeMode3 = "#3#";

		public static string Only1CodeMode4 = "#4#";

		public static string ShowAutoDo = "ShowAutoDo_true";

		public static bool AutoDo = false;

		public static List<string> AutoDo_listCity = new List<string>();

		public static int AutoDo_CityOrder = 1;

		public static int AutoDo_StartOrder = 0;

		public static int AutoDo_Total = 0;

		public static int AutoDo_Count = 1;

		public static int AutoDo_Create = 0;

		public static int AutoDo_Exist = 0;

		public static int AutoDo_Filled = 0;

		public static string Survey_Status = "";

		public static DateTime AutoDo_Start;

		public static bool RecordIsRunning = false;

		public static string RecordFileName = "";

		public static int RecordHz = 22050;

		public static int RecordDevice = 0;

		public static string MP3MaxMinutes = "180";

		public static DateTime RecordStartTime = DateTime.Now;

		public static string QueryEditSurveyId = "";

		public static string QueryEditQn = "";

		public static string QueryEditQTitle = "";

		public static string QueryEditQName = "";

		public static string QueryEditDetailID = "";

		public static string QueryEditCODE = "";

		public static string QueryEditCODE_TEXT = "";

		public static int QueryEditSequence = 0;

		public static bool QueryEditConfirm = false;

		public static bool QueryEditMemModel = false;

		public static string AttachSurveyId = "";

		public static string AttachQName = "";

		public static string AttachPageId = "";

		public static int AttachCount = 0;

		public static bool AttachReadOnlyModel = false;

		public static string SurveyID = "";

		public static string SurveyCity = "";

		public static string SurveyCityText = "";

		public static int CircleACount = 0;

		public static int CircleACurrent = 0;

		public static string CircleACode = "";

		public static string CircleACodeText = "";

		public static int CircleBCount = 0;

		public static int CircleBCurrent = 0;

		public static string CircleBCode = "";

		public static string CircleBCodeText = "";

		public static string SurveyExtend1 = "";

		public static string SurveyExtend2 = "";

		public static string SurveyExtend3 = "";

		public static string Answer = "";

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
	}
}
