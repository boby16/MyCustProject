using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class NavBase
	{
		public SurveyRoadMap CurPageRoadMap { get; set; }

		public SurveyRoadMap RoadMap { get; set; }

		public SurveyMain Survey { get; set; }

		public SurveySequence Sequence { get; set; }

		public List<S_JUMP> NavQJump { get; set; }

		public DateTime PageStartTime { get; set; }

		public DateTime RecordStartTime { get; set; }

		public string MyCircleCurrent { get; set; }

		public string GroupLevel { get; set; }

		public int GroupPageType { get; set; }

		public string GroupCodeA { get; set; }

		public string CircleACode { get; set; }

		public string CircleCodeTextA { get; set; }

		public int CircleACount { get; set; }

		public int CircleACurrent { get; set; }

		public bool IsLastA { get; set; }

		public string GroupCodeB { get; set; }

		public string CircleBCode { get; set; }

		public string CircleCodeTextB { get; set; }

		public int CircleBCount { get; set; }

		public int CircleBCurrent { get; set; }

		public bool IsLastB { get; set; }

		public string QName_Add { get; set; }

		public Dictionary<string, string> CircleInfo { get; set; }

		public SurveyDetail InfoDetail { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public NavBase()
		{
			this.RecordStartTime = DateTime.Now;
		}

		public void StartPage(string string_0, string string_1)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
		}

		public void NextPageNoSurveyId(string string_0, string string_1)
		{
			string string_2 = "";
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
			string_2 = this.RoadMap.ROUTE_LOGIC;
			List<string> list = this.Page_Ver(string_2, "@");
			string_2 = list[0];
			string string_3 = (list.Count > 1) ? list[1] : string_1;
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_2, string_3);
		}

		public void NextPage(string string_0, int int_0, string string_1, string string_2)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_1, string_2);
			string route_LOGIC = this.RoadMap.ROUTE_LOGIC;
			LogicEngine logicEngine = new LogicEngine();
			logicEngine.SurveyID = string_0;
			logicEngine.PageAnswer = this.PageAnswer;
			logicEngine.CircleACode = this.CircleACode;
			logicEngine.CircleACodeText = this.CircleCodeTextA;
			logicEngine.CircleACount = this.CircleACount;
			logicEngine.CircleACurrent = this.CircleACurrent;
			logicEngine.CircleBCode = this.CircleBCode;
			logicEngine.CircleBCodeText = this.CircleCodeTextB;
			logicEngine.CircleBCount = this.CircleBCount;
			logicEngine.CircleBCurrent = this.CircleBCurrent;
			string text = logicEngine.Route(route_LOGIC);
			List<string> list = this.Page_Ver(text, "@");
			text = list[0];
			string text2 = (list.Count > 1) ? logicEngine.stringResult(list[1]) : string_2;
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(text, text2);
			if (this.RoadMap.FORM_NAME == null || this.RoadMap.FORM_NAME == "")
			{
				string string_3 = string.Concat(new string[]
				{
					"问卷(",
					string_0,
					")无法直接从当前页[",
					string_1,
					"]跳到：VERSION_ID=",
					text2,
					"，PAGE_ID=",
					text,
					"，FORM_NAME=",
					this.RoadMap.FORM_NAME
				});
				this.oLogicExplain.OutputResult(string_3, "CapiDebug.Log", true);
			}
			this.AddSurveySequence(string_0, int_0, string_1, string_2);
			this.UpdateSurveyMain(string_0, int_0 + 1, text, this.RoadMap.VERSION_ID);
		}

		public void NextCirclePage(string string_0, int int_0, string string_1, string string_2)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_1, string_2);
			string string_3 = this.RoadMap.GROUP_ROUTE_LOGIC;
			if (this.GroupLevel == "A")
			{
				if (this.GroupPageType == 0 || this.GroupPageType == 2)
				{
					if (this.CircleACurrent == this.CircleACount)
					{
						string_3 = this.RoadMap.ROUTE_LOGIC;
					}
				}
				else if (this.GroupPageType == 10 || this.GroupPageType == 30)
				{
					string_3 = this.RoadMap.ROUTE_LOGIC;
				}
			}
			else if (this.GroupLevel == "B")
			{
				if (this.GroupPageType == 0)
				{
					if (this.CircleACurrent == this.CircleACount && this.CircleBCurrent == this.CircleBCount)
					{
						string_3 = this.RoadMap.ROUTE_LOGIC;
					}
				}
				else if (this.GroupPageType == 2)
				{
					if (this.CircleACurrent == this.CircleACount)
					{
						string_3 = this.RoadMap.ROUTE_LOGIC;
					}
				}
				else if ((this.GroupPageType == 10 || this.GroupPageType == 12 || this.GroupPageType == 30 || this.GroupPageType == 32) && this.CircleBCurrent == this.CircleBCount)
				{
					string_3 = this.RoadMap.ROUTE_LOGIC;
				}
			}
			LogicEngine logicEngine = new LogicEngine();
			logicEngine.SurveyID = string_0;
			logicEngine.PageAnswer = this.PageAnswer;
			logicEngine.CircleACode = this.CircleACode;
			logicEngine.CircleACodeText = this.CircleCodeTextA;
			logicEngine.CircleACount = this.CircleACount;
			logicEngine.CircleACurrent = this.CircleACurrent;
			logicEngine.CircleBCode = this.CircleBCode;
			logicEngine.CircleBCodeText = this.CircleCodeTextB;
			logicEngine.CircleBCount = this.CircleBCount;
			logicEngine.CircleBCurrent = this.CircleBCurrent;
			string text = logicEngine.Route(string_3);
			if (this.GroupLevel == "A" && this.GroupPageType == 2 && this.RoadMap.ROUTE_LOGIC == text)
			{
				this.IsLastA = true;
			}
			if (this.GroupLevel == "B" && this.GroupPageType == 2 && this.RoadMap.ROUTE_LOGIC == text)
			{
				this.IsLastB = true;
			}
			List<string> list = this.Page_Ver(text, "@");
			text = list[0];
			string text2 = (list.Count > 1) ? logicEngine.stringResult(list[1]) : string_2;
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(text, text2);
			if (this.RoadMap.FORM_NAME == null || this.RoadMap.FORM_NAME == "")
			{
				string string_4 = string.Concat(new string[]
				{
					"问卷(",
					string_0,
					")无法直接从当前页[",
					string_1,
					"]跳到：VERSION_ID=",
					text2,
					"，PAGE_ID=",
					text,
					"，FORM_NAME=",
					this.RoadMap.FORM_NAME
				});
				this.oLogicExplain.OutputResult(string_4, "CapiDebug.Log", true);
				string_4 = string.Concat(new object[]
				{
					"       上一错误循环信息：LoopA=",
					this.CircleACurrent,
					"/",
					this.CircleACount,
					", LoopB=",
					this.CircleBCurrent,
					"/",
					this.CircleBCount,
					", GroupPageType=",
					this.GroupPageType
				});
				this.oLogicExplain.OutputResult(string_4, "CapiDebug.Log", true);
			}
			this.AddSurveySequence(string_0, int_0, string_1, string_2);
			this.UpdateSurveyMain(string_0, int_0 + 1, text, this.RoadMap.VERSION_ID);
			this.SetNextCircle();
		}

		public void PrePage(string string_0, int int_0, string string_1)
		{
			int int_ = int_0 - 1;
			this.Sequence = this.oSurveySequenceDal.GetBySequenceID(string_0, int_);
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(this.Sequence.PAGE_ID, this.Sequence.VERSION_ID.ToString());
			this.UpdateSurveyMain(string_0, int_0 - 1, this.RoadMap.PAGE_ID, this.RoadMap.VERSION_ID);
			this.oSurveySequenceDal.DeleteBySequenceID(string_0, int_0);
		}

		public void LoadPage(string string_0, string string_1)
		{
			this.Survey = this.oSurveyMainDal.GetBySurveyId(string_0);
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(this.Survey.CUR_PAGE_ID, this.Survey.ROADMAP_VERSION_ID.ToString());
		}

		public void GoPage(string string_0, int int_0, string string_1, string string_2)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_1, string_2);
			this.AddSurveySequence(string_0, int_0, string_1, string_2);
			this.UpdateSurveyMain(string_0, int_0 + 1, string_1, this.RoadMap.VERSION_ID);
		}

		public string GetRedirectVersion(string string_0, string string_1)
		{
			string text = "";
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_1);
			string group_ROUTE_LOGIC = this.RoadMap.GROUP_ROUTE_LOGIC;
			return new LogicEngine
			{
				SurveyID = string_0,
				PageAnswer = this.PageAnswer,
				CircleACode = this.CircleACode,
				CircleACodeText = this.CircleCodeTextA,
				CircleACount = this.CircleACount,
				CircleACurrent = this.CircleACurrent,
				CircleBCode = this.CircleBCode,
				CircleBCodeText = this.CircleCodeTextB,
				CircleBCount = this.CircleBCount,
				CircleBCurrent = this.CircleBCurrent
			}.Route(group_ROUTE_LOGIC);
		}

		public string GetRedirectBack(string string_0, int int_0, string string_1, string string_2)
		{
			string text = "";
			SurveySequence bySequenceID = this.oSurveySequenceDal.GetBySequenceID(string_0, int_0);
			return bySequenceID.VERSION_ID.ToString();
		}

		public void RedirectNextPage(string string_0, int int_0, string string_1, string string_2, string string_3)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_1, string_2);
			string route_LOGIC = this.RoadMap.ROUTE_LOGIC;
			string text = new LogicEngine
			{
				SurveyID = string_0,
				PageAnswer = this.PageAnswer,
				CircleACode = this.CircleACode,
				CircleACodeText = this.CircleCodeTextA,
				CircleACount = this.CircleACount,
				CircleACurrent = this.CircleACurrent,
				CircleBCode = this.CircleBCode,
				CircleBCodeText = this.CircleCodeTextB,
				CircleBCount = this.CircleBCount,
				CircleBCurrent = this.CircleBCurrent
			}.Route(route_LOGIC);
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(text, string_3);
			this.AddSurveySequence(string_0, int_0, string_1, string_2);
			this.UpdateSurveyMain(string_0, int_0 + 1, text, this.RoadMap.VERSION_ID);
		}

		public void NextPageByPageId(string string_0, string string_1)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
		}

		public void GetCurrentRoadMap(string string_0)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0);
		}

		public void AddSurveySequence(string string_0, int int_0, string string_1, string string_2)
		{
			SurveySequence surveySequence = new SurveySequence();
			surveySequence.SURVEY_ID = string_0;
			surveySequence.SEQUENCE_ID = int_0;
			surveySequence.PAGE_ID = string_1;
			surveySequence.CIRCLE_A_CURRENT = this.CircleACurrent;
			surveySequence.CIRCLE_A_COUNT = this.CircleACount;
			surveySequence.CIRCLE_B_CURRENT = this.CircleBCurrent;
			surveySequence.CIRCLE_B_COUNT = this.CircleBCount;
			surveySequence.VERSION_ID = int.Parse(string_2);
			surveySequence.PAGE_BEGIN_TIME = new DateTime?(this.PageStartTime);
			surveySequence.PAGE_END_TIME = new DateTime?(DateTime.Now);
			surveySequence.PAGE_TIME = (int)(surveySequence.PAGE_END_TIME.Value - surveySequence.PAGE_BEGIN_TIME.Value).TotalSeconds;
			if (surveySequence.PAGE_TIME < 0)
			{
				surveySequence.PAGE_TIME = 0;
			}
			if (this.RecordFileName != "")
			{
				surveySequence.RECORD_FILE = this.RecordFileName;
				surveySequence.RECORD_START_TIME = new DateTime?(this.RecordStartTime);
				surveySequence.RECORD_BEGIN_TIME = (int)(surveySequence.PAGE_BEGIN_TIME.Value - surveySequence.RECORD_START_TIME.Value).TotalSeconds;
				surveySequence.RECORD_END_TIME = surveySequence.RECORD_BEGIN_TIME + surveySequence.PAGE_TIME;
			}
			else
			{
				surveySequence.RECORD_START_TIME = new DateTime?(DateTime.Now);
			}
			this.oSurveySequenceDal.UpdateNext(surveySequence);
		}

		public void UpdateSurveyMain(string string_0, int int_0, string string_1, int int_1 = 0)
		{
			this.Survey = this.oSurveyMainDal.GetBySurveyId(string_0);
			this.Survey.CUR_PAGE_ID = string_1;
			this.Survey.CIRCLE_A_CURRENT = this.CircleACurrent;
			this.Survey.CIRCLE_A_COUNT = this.CircleACount;
			this.Survey.CIRCLE_B_CURRENT = this.CircleBCurrent;
			this.Survey.CIRCLE_B_COUNT = this.CircleBCount;
			this.Survey.SEQUENCE_ID = int_0;
			this.Survey.ROADMAP_VERSION_ID = int_1;
			this.oSurveyMainDal.Update(this.Survey);
		}

		public void GetJump()
		{
			this.NavQJump = this.oS_JUMPDal.GetList();
		}

		public void GetCircleInfo(string string_0)
		{
			string groupLevel = this.GroupLevel;
			if (!(groupLevel == "A"))
			{
				if (groupLevel == "B")
				{
					if (this.CircleACurrent == 0)
					{
						this.CircleACurrent = 1;
					}
					if (this.CircleACount == 0)
					{
						this.CircleACount = this.GetCircleCount(string_0, this.GroupCodeA);
					}
					if (this.CircleBCurrent == 0)
					{
						this.CircleBCurrent = 1;
					}
					if (this.CircleBCount == 0)
					{
						this.CircleBCount = this.GetCircleCount(string_0, this.GroupCodeB);
					}
					this.CircleACode = this.GetCurrentCode(string_0, this.CircleACurrent, this.GroupCodeA);
					this.CircleCodeTextA = this.oSurveyDetailDal.GetCodeText(this.GroupCodeA, this.CircleACode);
					this.CircleBCode = this.GetCurrentCode(string_0, this.CircleBCurrent, this.GroupCodeB);
					this.CircleCodeTextB = this.oSurveyDetailDal.GetCodeText(this.GroupCodeB, this.CircleBCode);
					if (this.CircleACurrent == this.CircleACount)
					{
						this.IsLastA = true;
					}
					else
					{
						this.IsLastA = false;
					}
					if (this.CircleBCurrent == this.CircleBCount)
					{
						this.IsLastB = true;
					}
					else
					{
						this.IsLastB = false;
					}
					this.QName_Add = "_R" + this.CircleACode + "_R" + this.CircleBCode;
				}
			}
			else
			{
				if (this.CircleACurrent == 0)
				{
					this.CircleACurrent = 1;
				}
				if (this.CircleACount == 0)
				{
					this.CircleACount = this.GetCircleCount(string_0, this.GroupCodeA);
				}
				this.CircleACode = this.GetCurrentCode(string_0, this.CircleACurrent, this.GroupCodeA);
				this.CircleCodeTextA = this.oSurveyDetailDal.GetCodeText(this.GroupCodeA, this.CircleACode);
				if (this.CircleACurrent == this.CircleACount)
				{
					this.IsLastA = true;
				}
				else
				{
					this.IsLastA = false;
				}
				this.QName_Add = "_R" + this.CircleACode;
			}
		}

		public void SetNextCircle()
		{
			if (this.GroupLevel == "A")
			{
				if (this.GroupPageType == 0 || this.GroupPageType == 2)
				{
					if (this.IsLastA)
					{
						this.CircleACurrent = 0;
						this.CircleACount = 0;
					}
					else
					{
						this.CircleACurrent++;
					}
				}
			}
			else if (this.GroupLevel == "B")
			{
				if (this.GroupPageType == 0 || this.GroupPageType == 2)
				{
					if (this.IsLastA && this.IsLastB)
					{
						this.CircleACurrent = 0;
						this.CircleACount = 0;
						this.CircleBCurrent = 0;
						this.CircleBCount = 0;
					}
					else if (this.IsLastB)
					{
						this.CircleBCurrent = 0;
						this.CircleBCount = 0;
						this.CircleACurrent++;
					}
					else
					{
						this.CircleBCurrent++;
					}
				}
				else if (this.GroupPageType == 10 || this.GroupPageType == 12 || this.GroupPageType == 30 || this.GroupPageType == 32)
				{
					if (this.IsLastB)
					{
						this.CircleBCurrent = 0;
						this.CircleBCount = 0;
					}
					else
					{
						this.CircleBCurrent++;
					}
				}
			}
		}

		private int GetCircleCount(string string_0, string string_1)
		{
			return this.oSurveyRandomDal.GetCircleCount(string_0, string_1);
		}

		public string GetCurrentCode(string string_0, int int_0, string string_1)
		{
			return this.oSurveyRandomDal.GetOneCode(string_0, string_1, 1, int_0);
		}

		public string GetCircleCurrentCode(string string_0, string string_1, int int_0, int int_1, out int int_2)
		{
			string result = "";
			int_2 = 90000;
			for (int i = int_1; i <= int_0; i++)
			{
				SurveyRandom circleOne = this.oSurveyRandomDal.GetCircleOne(string_0, string_1, i);
				if (circleOne.QUESTION_NAME != "JUMP")
				{
					result = circleOne.CODE;
					int_2 = i;
					return result;
				}
			}
			return result;
		}

		public List<string> Page_Ver(string string_0, string string_1 = "@")
		{
			List<string> list = new List<string>();
			if (string_0 == "" || string_0 == null)
			{
				list.Add("");
			}
			else
			{
				int num = string_0.IndexOf(string_1);
				if (num > 0)
				{
					list.Add(this.LEFT(string_0, num));
					list.Add(this.MID(string_0, num + 1, -9999));
				}
				else
				{
					list.Add(string_0);
				}
			}
			return list;
		}

		public string LEFT(string string_0, int int_0 = 1)
		{
			string result;
			if (string_0 == null)
			{
				result = "";
			}
			else if (string_0.Length == 0)
			{
				result = "";
			}
			else
			{
				int num = (int_0 < 0) ? 0 : int_0;
				result = string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
			}
			return result;
		}

		public string MID(string string_0, int int_0, int int_1 = -9999)
		{
			string result;
			if (string_0 == null)
			{
				result = "";
			}
			else if (string_0.Length == 0)
			{
				result = "";
			}
			else
			{
				int num = int_1;
				if (num == -9999)
				{
					num = string_0.Length;
				}
				if (num < 0)
				{
					num = 0;
				}
				int num2 = (int_0 > string_0.Length) ? string_0.Length : int_0;
				result = string_0.Substring(num2, (num2 + num > string_0.Length) ? (string_0.Length - num2) : num);
			}
			return result;
		}

		public string RecordFileName = "";

		public List<VEAnswer> PageAnswer = new List<VEAnswer>();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		private SurveySequenceDal oSurveySequenceDal = new SurveySequenceDal();

		private S_JUMPDal oS_JUMPDal = new S_JUMPDal();

		private LogicExplain oLogicExplain = new LogicExplain();
	}
}
