using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.Business
{
	public class QDisplay
	{
		public string QuestionName { get; set; }

		public string QuestionTitle { get; set; }

		public string QuestionContent { get; set; }

		public SurveyDefine QDefine { get; set; }

		public List<SurveyDefine> lDefine { get; set; }

		public void Init(string pageId, int combineIndex)
		{
			foreach (SurveyDefine surveyDefine in this.lDefine.Where(p=>p.PAGE_ID == pageId && p.COMBINE_INDEX == combineIndex))
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

		[CompilerGenerated]
		private sealed class Class75
		{
			internal bool method_0(SurveyDefine surveyDefine_0)
			{
				return surveyDefine_0.PAGE_ID == this.PageId && surveyDefine_0.COMBINE_INDEX == this.CombineIndex;
			}

			public string PageId;

			public int CombineIndex;
		}
	}
}
