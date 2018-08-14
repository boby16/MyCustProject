using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Gssy.Capi.Entities;

namespace Gssy.Capi.Business
{
	// Token: 0x02000070 RID: 112
	public class QDisplay
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000042E6 File Offset: 0x000024E6
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x000042EE File Offset: 0x000024EE
		public string QuestionName { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000042F7 File Offset: 0x000024F7
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x000042FF File Offset: 0x000024FF
		public string QuestionTitle { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00004308 File Offset: 0x00002508
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00004310 File Offset: 0x00002510
		public string QuestionContent { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00004319 File Offset: 0x00002519
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00004321 File Offset: 0x00002521
		public SurveyDefine QDefine { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000432A File Offset: 0x0000252A
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00004332 File Offset: 0x00002532
		public List<SurveyDefine> lDefine { get; set; }

		// Token: 0x060006C3 RID: 1731 RVA: 0x000A144C File Offset: 0x0009F64C
		public void Init(string string_0, int int_0)
		{
			QDisplay.Class75 @class = new QDisplay.Class75();
			@class.PageId = string_0;
			@class.CombineIndex = int_0;
			foreach (SurveyDefine surveyDefine in this.lDefine.Where(new Func<SurveyDefine, bool>(@class.method_0)))
			{
				this.QDefine = new SurveyDefine
				{
					ID = surveyDefine.ID,
					ANSWER_ORDER = surveyDefine.ANSWER_ORDER,
					PAGE_ID = surveyDefine.PAGE_ID.ToString(),
					QUESTION_NAME = surveyDefine.QUESTION_NAME.ToString(),
					QUESTION_TITLE = surveyDefine.QUESTION_TITLE.ToString(),
					QUESTION_TYPE = surveyDefine.QUESTION_TYPE,
					QUESTION_USE = surveyDefine.QUESTION_USE,
					ANSWER_USE = surveyDefine.ANSWER_USE,
					COMBINE_INDEX = surveyDefine.COMBINE_INDEX,
					DETAIL_ID = surveyDefine.DETAIL_ID.ToString(),
					PARENT_CODE = surveyDefine.PARENT_CODE.ToString(),
					QUESTION_CONTENT = surveyDefine.QUESTION_CONTENT.ToString(),
					SPSS_TITLE = surveyDefine.SPSS_TITLE.ToString(),
					SPSS_CASE = surveyDefine.SPSS_CASE,
					SPSS_VARIABLE = surveyDefine.SPSS_VARIABLE,
					SPSS_PRINT_DECIMAIL = surveyDefine.SPSS_PRINT_DECIMAIL,
					MIN_COUNT = surveyDefine.MIN_COUNT,
					MAX_COUNT = surveyDefine.MAX_COUNT,
					IS_RANDOM = surveyDefine.IS_RANDOM,
					PAGE_COUNT_DOWN = surveyDefine.PAGE_COUNT_DOWN,
					CONTROL_TYPE = surveyDefine.CONTROL_TYPE,
					CONTROL_FONTSIZE = surveyDefine.CONTROL_FONTSIZE,
					CONTROL_HEIGHT = surveyDefine.CONTROL_HEIGHT,
					CONTROL_WIDTH = surveyDefine.CONTROL_WIDTH,
					CONTROL_MASK = surveyDefine.CONTROL_MASK.ToString(),
					TITLE_FONTSIZE = surveyDefine.TITLE_FONTSIZE,
					CONTROL_TOOLTIP = surveyDefine.CONTROL_TOOLTIP.ToString(),
					NOTE = surveyDefine.NOTE.ToString(),
					LIMIT_LOGIC = surveyDefine.LIMIT_LOGIC.ToString(),
					GROUP_LEVEL = surveyDefine.GROUP_LEVEL.ToString(),
					GROUP_CODEA = surveyDefine.GROUP_CODEA.ToString(),
					GROUP_CODEB = surveyDefine.GROUP_CODEB.ToString(),
					GROUP_PAGE_TYPE = surveyDefine.GROUP_PAGE_TYPE,
					MT_GROUP_MSG = surveyDefine.MT_GROUP_MSG.ToString(),
					MT_GROUP_COUNT = surveyDefine.MT_GROUP_COUNT.ToString(),
					SUMMARY_USE = surveyDefine.SUMMARY_USE,
					SUMMARY_TITLE = surveyDefine.SUMMARY_TITLE.ToString(),
					SUMMARY_INDEX = surveyDefine.SUMMARY_INDEX
				};
				this.QuestionName = surveyDefine.QUESTION_NAME;
				this.QuestionTitle = surveyDefine.QUESTION_TITLE;
			}
		}

		// Token: 0x020000CB RID: 203
		[CompilerGenerated]
		private sealed class Class75
		{
			// Token: 0x060007C1 RID: 1985 RVA: 0x000047FE File Offset: 0x000029FE
			internal bool method_0(SurveyDefine surveyDefine_0)
			{
				return surveyDefine_0.PAGE_ID == this.PageId && surveyDefine_0.COMBINE_INDEX == this.CombineIndex;
			}

			// Token: 0x04000D66 RID: 3430
			public string PageId;

			// Token: 0x04000D67 RID: 3431
			public int CombineIndex;
		}
	}
}
