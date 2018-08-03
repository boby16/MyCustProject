using Gssy.Capi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gssy.Capi.Business
{
	public class QDisplay
	{
		[CompilerGenerated]
		private sealed class _003F23_003F
		{
			public string PageId;

			public int CombineIndex;

			internal bool _003F339_003F(SurveyDefine _003F483_003F)
			{
				//IL_002c: Incompatible stack heights: 0 vs 1
				//IL_0033: Invalid comparison between Unknown and I4
				if (_003F483_003F.PAGE_ID == PageId)
				{
					int cOMBINE_INDEX = _003F483_003F.COMBINE_INDEX;
					int combineIndex = CombineIndex;
					return (int)/*Error near IL_0033: Stack underflow*/ == combineIndex;
				}
				return false;
			}
		}

		public string QuestionName
		{
			get;
			set;
		}

		public string QuestionTitle
		{
			get;
			set;
		}

		public string QuestionContent
		{
			get;
			set;
		}

		public SurveyDefine QDefine
		{
			get;
			set;
		}

		public List<SurveyDefine> lDefine
		{
			get;
			set;
		}

		public void Init(string _003F441_003F, int _003F442_003F)
		{
			_003F23_003F _003F23_003F = new _003F23_003F();
			_003F23_003F.PageId = _003F441_003F;
			_003F23_003F.CombineIndex = _003F442_003F;
			foreach (SurveyDefine item in lDefine.Where(_003F23_003F._003F339_003F))
			{
				SurveyDefine surveyDefine = new SurveyDefine();
				surveyDefine.ID = item.ID;
				surveyDefine.ANSWER_ORDER = item.ANSWER_ORDER;
				surveyDefine.PAGE_ID = item.PAGE_ID.ToString();
				surveyDefine.QUESTION_NAME = item.QUESTION_NAME.ToString();
				surveyDefine.QUESTION_TITLE = item.QUESTION_TITLE.ToString();
				surveyDefine.QUESTION_TYPE = item.QUESTION_TYPE;
				surveyDefine.QUESTION_USE = item.QUESTION_USE;
				surveyDefine.ANSWER_USE = item.ANSWER_USE;
				surveyDefine.COMBINE_INDEX = item.COMBINE_INDEX;
				surveyDefine.DETAIL_ID = item.DETAIL_ID.ToString();
				surveyDefine.PARENT_CODE = item.PARENT_CODE.ToString();
				surveyDefine.QUESTION_CONTENT = item.QUESTION_CONTENT.ToString();
				surveyDefine.SPSS_TITLE = item.SPSS_TITLE.ToString();
				surveyDefine.SPSS_CASE = item.SPSS_CASE;
				surveyDefine.SPSS_VARIABLE = item.SPSS_VARIABLE;
				surveyDefine.SPSS_PRINT_DECIMAIL = item.SPSS_PRINT_DECIMAIL;
				surveyDefine.MIN_COUNT = item.MIN_COUNT;
				surveyDefine.MAX_COUNT = item.MAX_COUNT;
				surveyDefine.IS_RANDOM = item.IS_RANDOM;
				surveyDefine.PAGE_COUNT_DOWN = item.PAGE_COUNT_DOWN;
				surveyDefine.CONTROL_TYPE = item.CONTROL_TYPE;
				surveyDefine.CONTROL_FONTSIZE = item.CONTROL_FONTSIZE;
				surveyDefine.CONTROL_HEIGHT = item.CONTROL_HEIGHT;
				surveyDefine.CONTROL_WIDTH = item.CONTROL_WIDTH;
				surveyDefine.CONTROL_MASK = item.CONTROL_MASK.ToString();
				surveyDefine.TITLE_FONTSIZE = item.TITLE_FONTSIZE;
				surveyDefine.CONTROL_TOOLTIP = item.CONTROL_TOOLTIP.ToString();
				surveyDefine.NOTE = item.NOTE.ToString();
				surveyDefine.LIMIT_LOGIC = item.LIMIT_LOGIC.ToString();
				surveyDefine.GROUP_LEVEL = item.GROUP_LEVEL.ToString();
				surveyDefine.GROUP_CODEA = item.GROUP_CODEA.ToString();
				surveyDefine.GROUP_CODEB = item.GROUP_CODEB.ToString();
				surveyDefine.GROUP_PAGE_TYPE = item.GROUP_PAGE_TYPE;
				surveyDefine.MT_GROUP_MSG = item.MT_GROUP_MSG.ToString();
				surveyDefine.MT_GROUP_COUNT = item.MT_GROUP_COUNT.ToString();
				surveyDefine.SUMMARY_USE = item.SUMMARY_USE;
				surveyDefine.SUMMARY_TITLE = item.SUMMARY_TITLE.ToString();
				surveyDefine.SUMMARY_INDEX = item.SUMMARY_INDEX;
				QDefine = surveyDefine;
				QuestionName = item.QUESTION_NAME;
				QuestionTitle = item.QUESTION_TITLE;
			}
		}
	}
}
