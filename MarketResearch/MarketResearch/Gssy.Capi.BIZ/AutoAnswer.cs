using System;
using System.Collections.Generic;
using System.Text;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class AutoAnswer
	{
		public string SurveyId { get; set; }

		public string RouteLogic { get; set; }

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

		public string MyAnswer { get; set; }

		public AutoAnswer()
		{
			this.method_0();
		}

		public void SurveyInit(string string_0, string string_1, int int_0, string string_2, string string_3)
		{
			this.oRandom.RandomSurveyMain(string_0);
			string string_4 = "201401";
			string string_5 = "1001";
			this.oSurvey.AddSurvey(string_0, string_1, string_2, string_4, string_5, string_3);
			QFill qfill = new QFill();
			qfill.Init("SURVEY_CODE", 0);
			qfill.FillText = string_0;
			qfill.BeforeSave();
			qfill.Save(string_0, int_0);
			this.oSurveyAnswerDal.AddOneNoUpdate(string_0, "AUSER_GROUP", string_3, int_0);
		}

		public string GetChildByIndex(string string_0, int int_0)
		{
			return this.oSurveyDefineDal.GetChildByIndex(string_0, int_0);
		}

		public void QuestionInit(string string_0, string string_1)
		{
			this.MySurveyDefine = this.oSurveyDefineDal.GetByName(string_1);
			this.MySurveyRoadMap = this.oSurveyRoadMapDal.GetByPageId(this.MySurveyDefine.PAGE_ID, "0");
			if (this.MySurveyDefine.QUESTION_TYPE == 2 || this.MySurveyDefine.QUESTION_TYPE == 3)
			{
				this.lSurveyDetail = this.oSurveyDetailDal.GetDetails(this.MySurveyDefine.DETAIL_ID);
			}
			this.lSurveyLogic = this.oSurveyLogicDal.GetCheckLogic(this.MySurveyDefine.PAGE_ID);
			if (this.MySurveyDefine.GROUP_LEVEL == "A")
			{
				this.lSurveyRandomA = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEA);
			}
			if (this.MySurveyDefine.GROUP_LEVEL == "B")
			{
				this.lSurveyRandomA = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEA);
				this.lSurveyRandomB = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEB);
			}
		}

		public int PageInfo(string string_0)
		{
			this.lPageDefine = this.oSurveyDefineDal.GetListByPageId(string_0);
			return this.lPageDefine.Count;
		}

		public string QuestionInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("============== SurveyDefine Information ==============");
			stringBuilder.AppendLine();
			if (this.MySurveyDefine.ID > 0)
			{
				stringBuilder.Append("自动编号  ,  ");
				stringBuilder.Append(this.MySurveyDefine.ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.ANSWER_ORDER > 0)
			{
				stringBuilder.Append("题目输出顺序  ,  ");
				stringBuilder.Append(this.MySurveyDefine.ANSWER_ORDER.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PAGE_ID != "")
			{
				stringBuilder.Append("页编号  ,  ");
				stringBuilder.Append(this.MySurveyDefine.PAGE_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_NAME != "")
			{
				stringBuilder.Append("问题编号  ,  ");
				stringBuilder.Append(this.MySurveyDefine.QUESTION_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TITLE != "")
			{
				stringBuilder.Append("主题题干  ,  ");
				stringBuilder.Append(this.MySurveyDefine.QUESTION_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TYPE > 0)
			{
				stringBuilder.Append("主题题型  ,  ");
				stringBuilder.Append(this.MySurveyDefine.QUESTION_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_USE > 0)
			{
				stringBuilder.Append("输出问题使用  ,  ");
				stringBuilder.Append(this.MySurveyDefine.QUESTION_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.ANSWER_USE > 0)
			{
				stringBuilder.Append("输出问题答案使用  ,  ");
				stringBuilder.Append(this.MySurveyDefine.ANSWER_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.COMBINE_INDEX > 0)
			{
				stringBuilder.Append("组合题的子题索引  ,  ");
				stringBuilder.Append(this.MySurveyDefine.COMBINE_INDEX.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.DETAIL_ID != "")
			{
				stringBuilder.Append("选择项关联编码  ,  ");
				stringBuilder.Append(this.MySurveyDefine.DETAIL_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PARENT_CODE != "")
			{
				stringBuilder.Append("多级选择项父关联编码  ,  ");
				stringBuilder.Append(this.MySurveyDefine.PARENT_CODE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_CONTENT != "")
			{
				stringBuilder.Append("副题题干  ,  ");
				stringBuilder.Append(this.MySurveyDefine.QUESTION_CONTENT);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_TITLE != "")
			{
				stringBuilder.Append("SPSS 题干  ,  ");
				stringBuilder.Append(this.MySurveyDefine.SPSS_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_CASE > 0)
			{
				stringBuilder.Append("SPSS 题型  ,  ");
				stringBuilder.Append(this.MySurveyDefine.SPSS_CASE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_VARIABLE > 0)
			{
				stringBuilder.Append("SPSS 变量类型  ,  ");
				stringBuilder.Append(this.MySurveyDefine.SPSS_VARIABLE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_PRINT_DECIMAIL > 0)
			{
				stringBuilder.Append("SPSS 小数位  ,  ");
				stringBuilder.Append(this.MySurveyDefine.SPSS_PRINT_DECIMAIL.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MIN_COUNT > 0)
			{
				stringBuilder.Append("多选题的最小选择数  ,  ");
				stringBuilder.Append(this.MySurveyDefine.MIN_COUNT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MAX_COUNT > 0)
			{
				stringBuilder.Append("多选题的最大选择数  ,  ");
				stringBuilder.Append(this.MySurveyDefine.MAX_COUNT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.IS_RANDOM > 0)
			{
				stringBuilder.Append("内部选项是否随机排列  ,  ");
				stringBuilder.Append(this.MySurveyDefine.IS_RANDOM.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PAGE_COUNT_DOWN > 0)
			{
				stringBuilder.Append("页计时倒数 秒  ,  ");
				stringBuilder.Append(this.MySurveyDefine.PAGE_COUNT_DOWN.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_TYPE > 0)
			{
				stringBuilder.Append("控件的类型控制  ,  ");
				stringBuilder.Append(this.MySurveyDefine.CONTROL_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_FONTSIZE > 0)
			{
				stringBuilder.Append("控件的字体大小  ,  ");
				stringBuilder.Append(this.MySurveyDefine.CONTROL_FONTSIZE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_HEIGHT > 0)
			{
				stringBuilder.Append("控件的高度  ,  ");
				stringBuilder.Append(this.MySurveyDefine.CONTROL_HEIGHT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_WIDTH > 0)
			{
				stringBuilder.Append("控件的宽度  ,  ");
				stringBuilder.Append(this.MySurveyDefine.CONTROL_WIDTH.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_MASK != "")
			{
				stringBuilder.Append("控件 Mask  ,  ");
				stringBuilder.Append(this.MySurveyDefine.CONTROL_MASK);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.TITLE_FONTSIZE > 0)
			{
				stringBuilder.Append("问题标题的字体大小  ,  ");
				stringBuilder.Append(this.MySurveyDefine.TITLE_FONTSIZE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_TOOLTIP != "")
			{
				stringBuilder.Append("控件 ToolTip  ,  ");
				stringBuilder.Append(this.MySurveyDefine.CONTROL_TOOLTIP);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.NOTE != "")
			{
				stringBuilder.Append("备注说明 / 显示题的内容  ,  ");
				stringBuilder.Append(this.MySurveyDefine.NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.LIMIT_LOGIC != "")
			{
				stringBuilder.Append("互动限制的逻辑控制  ,  ");
				stringBuilder.Append(this.MySurveyDefine.LIMIT_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_LEVEL != "")
			{
				stringBuilder.Append("循环题组级别  ,  ");
				stringBuilder.Append(this.MySurveyDefine.GROUP_LEVEL);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_CODEA != "")
			{
				stringBuilder.Append("循环题组父循环代码A 层  ,  ");
				stringBuilder.Append(this.MySurveyDefine.GROUP_CODEA);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_CODEB != "")
			{
				stringBuilder.Append("循环题组父循环代码B 层  ,  ");
				stringBuilder.Append(this.MySurveyDefine.GROUP_CODEB);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_PAGE_TYPE > 0)
			{
				stringBuilder.Append("问题在循环题组中的位置类型  ,  ");
				stringBuilder.Append(this.MySurveyDefine.GROUP_PAGE_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MT_GROUP_MSG != "")
			{
				stringBuilder.Append("循环题组题信息  ,  ");
				stringBuilder.Append(this.MySurveyDefine.MT_GROUP_MSG);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MT_GROUP_COUNT != "")
			{
				stringBuilder.Append("循环题组问题数量  ,  ");
				stringBuilder.Append(this.MySurveyDefine.MT_GROUP_COUNT);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_USE > 0)
			{
				stringBuilder.Append("是否摘要  ,  ");
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_TITLE != "")
			{
				stringBuilder.Append("摘要标题  ,  ");
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_INDEX > 0)
			{
				stringBuilder.Append("摘要索引  ,  ");
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_INDEX.ToString());
				stringBuilder.AppendLine();
			}
			stringBuilder.Append("============== SurveyRoadMap Information ==============");
			stringBuilder.AppendLine();
			if (this.MySurveyRoadMap.ID > 0)
			{
				stringBuilder.Append("自动编号  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.VERSION_ID > 0)
			{
				stringBuilder.Append("路由版本编号  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.VERSION_ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PART_NAME != "")
			{
				stringBuilder.Append("问卷分部  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.PART_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PAGE_NOTE != "")
			{
				stringBuilder.Append("页说明  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.PAGE_NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PAGE_ID != "")
			{
				stringBuilder.Append("页 ID  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.PAGE_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.ROUTE_LOGIC != "")
			{
				stringBuilder.Append("页跳转路由逻辑  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.ROUTE_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.GROUP_ROUTE_LOGIC != "")
			{
				stringBuilder.Append("循环组组内跳转逻辑  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.GROUP_ROUTE_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.FORM_NAME != "")
			{
				stringBuilder.Append("页程序名  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.FORM_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.IS_JUMP > 0)
			{
				stringBuilder.Append("是否可跳转  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.IS_JUMP.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.NOTE != "")
			{
				stringBuilder.Append("备注  ,  ");
				stringBuilder.Append(this.MySurveyRoadMap.NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TYPE == 2 || this.MySurveyDefine.QUESTION_TYPE == 3)
			{
				stringBuilder.Append("============== SurveyDetail Information ==============");
				stringBuilder.AppendLine();
				stringBuilder.Append("编码  ,  编码文本 , 是否其他");
				stringBuilder.AppendLine();
				foreach (SurveyDetail surveyDetail in this.lSurveyDetail)
				{
					stringBuilder.Append(string.Format("{0}  ,  {1} , {2}", surveyDetail.CODE, surveyDetail.CODE_TEXT, surveyDetail.IS_OTHER.ToString()));
					stringBuilder.AppendLine();
				}
			}
			if (this.lSurveyLogic.Count > 0)
			{
				stringBuilder.Append("============== SurveyLogic Information ==============");
				stringBuilder.AppendLine();
				foreach (SurveyLogic surveyLogic in this.lSurveyLogic)
				{
					stringBuilder.Append("逻辑公式定义  ,  ");
					stringBuilder.Append(surveyLogic.FORMULA);
					stringBuilder.AppendLine();
					stringBuilder.Append("提示信息  ,  ");
					stringBuilder.Append(surveyLogic.LOGIC_MESSAGE);
					stringBuilder.AppendLine();
					stringBuilder.Append("逻辑描述  ,  ");
					stringBuilder.Append(surveyLogic.NOTE);
					stringBuilder.AppendLine();
					stringBuilder.Append("允许督导确认后通过  ,  ");
					stringBuilder.Append(surveyLogic.IS_ALLOW_PASS.ToString());
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
				}
			}
			if (this.MySurveyDefine.GROUP_LEVEL == "A")
			{
				stringBuilder.Append("============== SurveyRandom [" + this.MySurveyDefine.GROUP_CODEA + "] Information ==============");
				stringBuilder.AppendLine();
				foreach (SurveyRandom surveyRandom in this.lSurveyRandomA)
				{
					stringBuilder.Append(surveyRandom.CODE);
					stringBuilder.Append(" , ");
				}
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_LEVEL == "B")
			{
				stringBuilder.Append("============== SurveyRandom [" + this.MySurveyDefine.GROUP_CODEB + "] Information ==============");
				stringBuilder.AppendLine();
				foreach (SurveyRandom surveyRandom2 in this.lSurveyRandomB)
				{
					stringBuilder.Append(surveyRandom2.CODE);
					stringBuilder.Append(" , ");
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		public string NextPage(string string_0, int int_0, string string_1, string string_2)
		{
			string route_LOGIC = this.MySurveyRoadMap.ROUTE_LOGIC;
			string result = new LogicEngine
			{
				SurveyID = string_0
			}.Route(route_LOGIC);
			this.RouteLogic = route_LOGIC;
			return result;
		}

		public string GetAutoSurveyId(string string_0, int int_0)
		{
			return this.oSurveyMainDal.GetAutoSurveyId(string_0, int_0);
		}

		public void SetMain(string string_0, string string_1, int int_0)
		{
			string text = string_1;
			if (this.MySurveyDefine.GROUP_LEVEL == "A")
			{
				text = text + "_R" + this.CircleACode;
			}
			if (this.MySurveyDefine.GROUP_LEVEL == "B")
			{
				text = string.Concat(new string[]
				{
					text,
					"_R",
					this.CircleACode,
					"_R",
					this.CircleBCode
				});
			}
			switch (this.MySurveyDefine.QUESTION_TYPE)
			{
			case 1:
				this.SetFill(string_0, text, int_0);
				break;
			case 2:
				this.SetSingle(string_0, text, int_0);
				break;
			case 3:
				this.SetMultiple(string_0, text, int_0);
				break;
			}
		}

		public void SetFill(string string_0, string string_1, int int_0)
		{
			QFill qfill = new QFill();
			qfill.Init(this.MySurveyDefine.PAGE_ID, this.MySurveyDefine.COMBINE_INDEX);
			qfill.FillText = this.FixSetSingle(string_1);
			if (qfill.FillText == null)
			{
				qfill.FillText = this.MySurveyDefine.QUESTION_NAME;
			}
			this.MyAnswer = qfill.FillText;
			qfill.QuestionName = string_1;
			qfill.Save(string_0, int_0);
		}

		public void SetSingle(string string_0, string string_1, int int_0)
		{
			QSingle qsingle = new QSingle();
			qsingle.Init(this.MySurveyDefine.PAGE_ID, this.MySurveyDefine.COMBINE_INDEX, true);
			if (this.MySurveyDefine.PARENT_CODE != "")
			{
				qsingle.ParentCode = new LogicAnswer
				{
					SurveyID = string_0
				}.GetAnswer(this.MySurveyDefine.PARENT_CODE);
				qsingle.GetDynamicDetails();
			}
			qsingle.SelectedCode = this.FixSetSingle(string_1);
			if (qsingle.SelectedCode == null)
			{
				qsingle.RandomDetails();
				qsingle.SelectedCode = qsingle.QDetails[0].CODE;
			}
			this.MyAnswer = qsingle.SelectedCode;
			if (qsingle.OtherCode != "")
			{
				qsingle.FillText = "其他 " + this.MySurveyDefine.QUESTION_NAME;
				this.MyAnswer = this.MyAnswer + " , " + qsingle.FillText;
			}
			qsingle.QuestionName = string_1;
			qsingle.BeforeSave();
			qsingle.Save(string_0, int_0, true);
		}

		public void SetMultiple(string string_0, string string_1, int int_0)
		{
			QMultiple qmultiple = new QMultiple();
			qmultiple.Init(this.MySurveyDefine.PAGE_ID, this.MySurveyDefine.COMBINE_INDEX, true);
			if (this.MySurveyDefine.PARENT_CODE != "")
			{
				qmultiple.ParentCode = new LogicAnswer
				{
					SurveyID = string_0
				}.GetAnswer(this.MySurveyDefine.PARENT_CODE);
				qmultiple.GetDynamicDetails();
			}
			qmultiple.RandomDetails();
			this.MyAnswer = "";
			foreach (SurveyDetail surveyDetail in qmultiple.QDetails)
			{
				qmultiple.SelectedValues.Add(surveyDetail.CODE);
				this.MyAnswer = this.MyAnswer + surveyDetail.CODE + " , ";
			}
			if (qmultiple.OtherCode != "")
			{
				qmultiple.FillText = "其他 " + this.MySurveyDefine.QUESTION_NAME;
				this.MyAnswer = this.MyAnswer + " , " + qmultiple.FillText;
			}
			qmultiple.QuestionName = string_1;
			qmultiple.BeforeSave();
			qmultiple.Save(string_0, int_0);
		}

		public string FixSetSingle(string string_0)
		{
			string result = "";
			this.DAnswer.TryGetValue(string_0, out result);
			return result;
		}

		private void method_0()
		{
			this.DAnswer.Add("S1", "5");
			this.DAnswer.Add("S2", "2");
			this.DAnswer.Add("S3", "3");
			this.DAnswer.Add("S4", "1");
			this.DAnswer.Add("S7", "1");
			this.DAnswer.Add("S8", "1");
			this.DAnswer.Add("S9", "1");
			this.DAnswer.Add("S10", "2");
			this.DAnswer.Add("S13", "39");
		}

		private RandomBiz oRandom = new RandomBiz();

		private SurveyBiz oSurvey = new SurveyBiz();

		private Dictionary<string, string> DAnswer = new Dictionary<string, string>();

		public SurveyDefine MySurveyDefine = new SurveyDefine();

		private List<SurveyDetail> lSurveyDetail = new List<SurveyDetail>();

		private SurveyRoadMap MySurveyRoadMap = new SurveyRoadMap();

		private List<SurveyLogic> lSurveyLogic = new List<SurveyLogic>();

		private List<SurveyRandom> lSurveyRandomA = new List<SurveyRandom>();

		private List<SurveyRandom> lSurveyRandomB = new List<SurveyRandom>();

		private List<SurveyMain> lSurveyMain = new List<SurveyMain>();

		private List<SurveyAnswer> lSurveyAnswer = new List<SurveyAnswer>();

		private List<SurveyDefine> lPageDefine = new List<SurveyDefine>();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveyLogicDal oSurveyLogicDal = new SurveyLogicDal();

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();
	}
}
