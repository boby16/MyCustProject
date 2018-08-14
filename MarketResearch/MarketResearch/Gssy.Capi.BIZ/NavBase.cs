using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000015 RID: 21
	public class NavBase
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000025F5 File Offset: 0x000007F5
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000025FD File Offset: 0x000007FD
		public SurveyRoadMap CurPageRoadMap { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00002606 File Offset: 0x00000806
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000260E File Offset: 0x0000080E
		public SurveyRoadMap RoadMap { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00002617 File Offset: 0x00000817
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000261F File Offset: 0x0000081F
		public SurveyMain Survey { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00002628 File Offset: 0x00000828
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00002630 File Offset: 0x00000830
		public SurveySequence Sequence { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00002639 File Offset: 0x00000839
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00002641 File Offset: 0x00000841
		public List<S_JUMP> NavQJump { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000264A File Offset: 0x0000084A
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00002652 File Offset: 0x00000852
		public DateTime PageStartTime { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000265B File Offset: 0x0000085B
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00002663 File Offset: 0x00000863
		public DateTime RecordStartTime { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000266C File Offset: 0x0000086C
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00002674 File Offset: 0x00000874
		public string MyCircleCurrent { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000267D File Offset: 0x0000087D
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00002685 File Offset: 0x00000885
		public string GroupLevel { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000268E File Offset: 0x0000088E
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00002696 File Offset: 0x00000896
		public int GroupPageType { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000269F File Offset: 0x0000089F
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000026A7 File Offset: 0x000008A7
		public string GroupCodeA { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000026B0 File Offset: 0x000008B0
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000026B8 File Offset: 0x000008B8
		public string CircleACode { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000026C1 File Offset: 0x000008C1
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000026C9 File Offset: 0x000008C9
		public string CircleCodeTextA { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000026D2 File Offset: 0x000008D2
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000026DA File Offset: 0x000008DA
		public int CircleACount { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000026E3 File Offset: 0x000008E3
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000026EB File Offset: 0x000008EB
		public int CircleACurrent { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000026F4 File Offset: 0x000008F4
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000026FC File Offset: 0x000008FC
		public bool IsLastA { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00002705 File Offset: 0x00000905
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000270D File Offset: 0x0000090D
		public string GroupCodeB { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00002716 File Offset: 0x00000916
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000271E File Offset: 0x0000091E
		public string CircleBCode { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00002727 File Offset: 0x00000927
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000272F File Offset: 0x0000092F
		public string CircleCodeTextB { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00002738 File Offset: 0x00000938
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00002740 File Offset: 0x00000940
		public int CircleBCount { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00002749 File Offset: 0x00000949
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00002751 File Offset: 0x00000951
		public int CircleBCurrent { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000275A File Offset: 0x0000095A
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00002762 File Offset: 0x00000962
		public bool IsLastB { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000276B File Offset: 0x0000096B
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00002773 File Offset: 0x00000973
		public string QName_Add { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000277C File Offset: 0x0000097C
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00002784 File Offset: 0x00000984
		public Dictionary<string, string> CircleInfo { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000278D File Offset: 0x0000098D
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00002795 File Offset: 0x00000995
		public SurveyDetail InfoDetail { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000279E File Offset: 0x0000099E
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000027A6 File Offset: 0x000009A6
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x06000213 RID: 531 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
		public NavBase()
		{
			this.RecordStartTime = DateTime.Now;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000027AF File Offset: 0x000009AF
		public void StartPage(string string_0, string string_1)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0001EE3C File Offset: 0x0001D03C
		public void NextPageNoSurveyId(string string_0, string string_1)
		{
			string string_2 = global::GClass0.smethod_0("");
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
			string_2 = this.RoadMap.ROUTE_LOGIC;
			List<string> list = this.Page_Ver(string_2, global::GClass0.smethod_0("A"));
			string_2 = list[0];
			string string_3 = (list.Count > 1) ? list[1] : string_1;
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_2, string_3);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0001EEB4 File Offset: 0x0001D0B4
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
			List<string> list = this.Page_Ver(text, global::GClass0.smethod_0("A"));
			text = list[0];
			string text2 = (list.Count > 1) ? logicEngine.stringResult(list[1]) : string_2;
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(text, text2);
			if (this.RoadMap.FORM_NAME == null || this.RoadMap.FORM_NAME == global::GClass0.smethod_0(""))
			{
				string string_3 = string.Concat(new string[]
				{
					global::GClass0.smethod_0("闭創ȩ"),
					string_0,
					global::GClass0.smethod_0("#擩滝痳枣䯋套啎遷ग़"),
					string_1,
					global::GClass0.smethod_0("R賽倽ﰖѝՏٛݛࡎॉੋ୛ొെ฼"),
					text2,
					global::GClass0.smethod_0("％ŘɆ́р՛ي݆࠼"),
					text,
					global::GClass0.smethod_0("＇ŌɆ͚ъՙً݅ࡎे਼"),
					this.RoadMap.FORM_NAME
				});
				this.oLogicExplain.OutputResult(string_3, global::GClass0.smethod_0("Nŭɻͣэխ٥ݳࡢप੏୭౦"), true);
			}
			this.AddSurveySequence(string_0, int_0, string_1, string_2);
			this.UpdateSurveyMain(string_0, int_0 + 1, text, this.RoadMap.VERSION_ID);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0001F084 File Offset: 0x0001D284
		public void NextCirclePage(string string_0, int int_0, string string_1, string string_2)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_1, string_2);
			string string_3 = this.RoadMap.GROUP_ROUTE_LOGIC;
			if (this.GroupLevel == global::GClass0.smethod_0("@"))
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
			else if (this.GroupLevel == global::GClass0.smethod_0("C"))
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
			if (this.GroupLevel == global::GClass0.smethod_0("@") && this.GroupPageType == 2 && this.RoadMap.ROUTE_LOGIC == text)
			{
				this.IsLastA = true;
			}
			if (this.GroupLevel == global::GClass0.smethod_0("C") && this.GroupPageType == 2 && this.RoadMap.ROUTE_LOGIC == text)
			{
				this.IsLastB = true;
			}
			List<string> list = this.Page_Ver(text, global::GClass0.smethod_0("A"));
			text = list[0];
			string text2 = (list.Count > 1) ? logicEngine.stringResult(list[1]) : string_2;
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(text, text2);
			if (this.RoadMap.FORM_NAME == null || this.RoadMap.FORM_NAME == global::GClass0.smethod_0(""))
			{
				string string_4 = string.Concat(new string[]
				{
					global::GClass0.smethod_0("闭創ȩ"),
					string_0,
					global::GClass0.smethod_0("#擩滝痳枣䯋套啎遷ग़"),
					string_1,
					global::GClass0.smethod_0("R賽倽ﰖѝՏٛݛࡎॉੋ୛ొെ฼"),
					text2,
					global::GClass0.smethod_0("％ŘɆ́р՛ي݆࠼"),
					text,
					global::GClass0.smethod_0("＇ŌɆ͚ъՙً݅ࡎे਼"),
					this.RoadMap.FORM_NAME
				});
				this.oLogicExplain.OutputResult(string_4, global::GClass0.smethod_0("Nŭɻͣэխ٥ݳࡢप੏୭౦"), true);
				string_4 = string.Concat(new object[]
				{
					global::GClass0.smethod_0("6ĵȴ̳вԱذ䤅䘎鰔臣咡羥䋨湧၊ᅪቫ፳ᑃᔼ"),
					this.CircleACurrent,
					global::GClass0.smethod_0("."),
					this.CircleACount,
					global::GClass0.smethod_0("$ħɊͪѫճـܼ"),
					this.CircleBCurrent,
					global::GClass0.smethod_0("."),
					this.CircleBCount,
					global::GClass0.smethod_0("<įɉͿѣվٺݙࡩॠ੣୑౽൳๧༼"),
					this.GroupPageType
				});
				this.oLogicExplain.OutputResult(string_4, global::GClass0.smethod_0("Nŭɻͣэխ٥ݳࡢप੏୭౦"), true);
			}
			this.AddSurveySequence(string_0, int_0, string_1, string_2);
			this.UpdateSurveyMain(string_0, int_0 + 1, text, this.RoadMap.VERSION_ID);
			this.SetNextCircle();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		public void PrePage(string string_0, int int_0, string string_1)
		{
			int int_ = int_0 - 1;
			this.Sequence = this.oSurveySequenceDal.GetBySequenceID(string_0, int_);
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(this.Sequence.PAGE_ID, this.Sequence.VERSION_ID.ToString());
			this.UpdateSurveyMain(string_0, int_0 - 1, this.RoadMap.PAGE_ID, this.RoadMap.VERSION_ID);
			this.oSurveySequenceDal.DeleteBySequenceID(string_0, int_0);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0001F548 File Offset: 0x0001D748
		public void LoadPage(string string_0, string string_1)
		{
			this.Survey = this.oSurveyMainDal.GetBySurveyId(string_0);
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(this.Survey.CUR_PAGE_ID, this.Survey.ROADMAP_VERSION_ID.ToString());
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001F598 File Offset: 0x0001D798
		public void GoPage(string string_0, int int_0, string string_1, string string_2)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_1, string_2);
			this.AddSurveySequence(string_0, int_0, string_1, string_2);
			this.UpdateSurveyMain(string_0, int_0 + 1, string_1, this.RoadMap.VERSION_ID);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001F5DC File Offset: 0x0001D7DC
		public string GetRedirectVersion(string string_0, string string_1)
		{
			string text = global::GClass0.smethod_0("");
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

		// Token: 0x0600021C RID: 540 RVA: 0x0001F698 File Offset: 0x0001D898
		public string GetRedirectBack(string string_0, int int_0, string string_1, string string_2)
		{
			string text = global::GClass0.smethod_0("");
			SurveySequence bySequenceID = this.oSurveySequenceDal.GetBySequenceID(string_0, int_0);
			return bySequenceID.VERSION_ID.ToString();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0001F6D0 File Offset: 0x0001D8D0
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

		// Token: 0x0600021E RID: 542 RVA: 0x000027AF File Offset: 0x000009AF
		public void NextPageByPageId(string string_0, string string_1)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000027C4 File Offset: 0x000009C4
		public void GetCurrentRoadMap(string string_0)
		{
			this.RoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
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
			if (this.RecordFileName != global::GClass0.smethod_0(""))
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

		// Token: 0x06000221 RID: 545 RVA: 0x0001F914 File Offset: 0x0001DB14
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

		// Token: 0x06000222 RID: 546 RVA: 0x000027D8 File Offset: 0x000009D8
		public void GetJump()
		{
			this.NavQJump = this.oS_JUMPDal.GetList();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
		public void GetCircleInfo(string string_0)
		{
			string groupLevel = this.GroupLevel;
			if (!(groupLevel == global::GClass0.smethod_0("@")))
			{
				if (groupLevel == global::GClass0.smethod_0("C"))
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
					this.QName_Add = global::GClass0.smethod_0("]œ") + this.CircleACode + global::GClass0.smethod_0("]œ") + this.CircleBCode;
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
				this.QName_Add = global::GClass0.smethod_0("]œ") + this.CircleACode;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		public void SetNextCircle()
		{
			if (this.GroupLevel == global::GClass0.smethod_0("@"))
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
			else if (this.GroupLevel == global::GClass0.smethod_0("C"))
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

		// Token: 0x06000225 RID: 549 RVA: 0x0001FD24 File Offset: 0x0001DF24
		private int GetCircleCount(string string_0, string string_1)
		{
			return this.oSurveyRandomDal.GetCircleCount(string_0, string_1);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0001FD40 File Offset: 0x0001DF40
		public string GetCurrentCode(string string_0, int int_0, string string_1)
		{
			return this.oSurveyRandomDal.GetOneCode(string_0, string_1, 1, int_0);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0001FD60 File Offset: 0x0001DF60
		public string GetCircleCurrentCode(string string_0, string string_1, int int_0, int int_1, out int int_2)
		{
			string result = global::GClass0.smethod_0("");
			int_2 = 90000;
			for (int i = int_1; i <= int_0; i++)
			{
				SurveyRandom circleOne = this.oSurveyRandomDal.GetCircleOne(string_0, string_1, i);
				if (circleOne.QUESTION_NAME != global::GClass0.smethod_0("NŖɏ͑"))
				{
					result = circleOne.CODE;
					int_2 = i;
					return result;
				}
			}
			return result;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0001FDCC File Offset: 0x0001DFCC
		public List<string> Page_Ver(string string_0, string string_1 = "@")
		{
			List<string> list = new List<string>();
			if (string_0 == global::GClass0.smethod_0("") || string_0 == null)
			{
				list.Add(global::GClass0.smethod_0(""));
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

		// Token: 0x06000229 RID: 553 RVA: 0x0001FE4C File Offset: 0x0001E04C
		public string LEFT(string string_0, int int_0 = 1)
		{
			string result;
			if (string_0 == null)
			{
				result = global::GClass0.smethod_0("");
			}
			else if (string_0.Length == 0)
			{
				result = global::GClass0.smethod_0("");
			}
			else
			{
				int num = (int_0 < 0) ? 0 : int_0;
				result = string_0.Substring(0, (num > string_0.Length) ? string_0.Length : num);
			}
			return result;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0001FEA8 File Offset: 0x0001E0A8
		public string MID(string string_0, int int_0, int int_1 = -9999)
		{
			string result;
			if (string_0 == null)
			{
				result = global::GClass0.smethod_0("");
			}
			else if (string_0.Length == 0)
			{
				result = global::GClass0.smethod_0("");
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

		// Token: 0x040000CA RID: 202
		public string RecordFileName = global::GClass0.smethod_0("");

		// Token: 0x040000CC RID: 204
		public List<VEAnswer> PageAnswer = new List<VEAnswer>();

		// Token: 0x040000E0 RID: 224
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x040000E1 RID: 225
		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		// Token: 0x040000E2 RID: 226
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x040000E3 RID: 227
		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		// Token: 0x040000E4 RID: 228
		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		// Token: 0x040000E5 RID: 229
		private SurveySequenceDal oSurveySequenceDal = new SurveySequenceDal();

		// Token: 0x040000E6 RID: 230
		private S_JUMPDal oS_JUMPDal = new S_JUMPDal();

		// Token: 0x040000E7 RID: 231
		private LogicExplain oLogicExplain = new LogicExplain();
	}
}
