using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyDefineDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyDefine WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyDefine GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyDefine WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyDefine GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyDefine surveyDefine = new SurveyDefine();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyDefine.ID = Convert.ToInt32(dataReader["ID"]);
					surveyDefine.ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]);
					surveyDefine.PAGE_ID = dataReader["PAGE_ID"].ToString();
					surveyDefine.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					surveyDefine.QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString();
					surveyDefine.QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]);
					surveyDefine.QUESTION_USE = Convert.ToInt32(dataReader["QUESTION_USE"]);
					surveyDefine.ANSWER_USE = Convert.ToInt32(dataReader["ANSWER_USE"]);
					surveyDefine.COMBINE_INDEX = Convert.ToInt32(dataReader["COMBINE_INDEX"]);
					surveyDefine.DETAIL_ID = dataReader["DETAIL_ID"].ToString();
					surveyDefine.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
					surveyDefine.SHOW_LOGIC = dataReader["SHOW_LOGIC"].ToString();
					surveyDefine.QUESTION_CONTENT = dataReader["QUESTION_CONTENT"].ToString();
					surveyDefine.SPSS_TITLE = dataReader["SPSS_TITLE"].ToString();
					surveyDefine.SPSS_CASE = Convert.ToInt32(dataReader["SPSS_CASE"]);
					surveyDefine.SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]);
					surveyDefine.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader["SPSS_PRINT_DECIMAIL"]);
					surveyDefine.MIN_COUNT = Convert.ToInt32(dataReader["MIN_COUNT"]);
					surveyDefine.MAX_COUNT = Convert.ToInt32(dataReader["MAX_COUNT"]);
					surveyDefine.IS_RANDOM = Convert.ToInt32(dataReader["IS_RANDOM"]);
					surveyDefine.PAGE_COUNT_DOWN = Convert.ToInt32(dataReader["PAGE_COUNT_DOWN"]);
					surveyDefine.CONTROL_TYPE = Convert.ToInt32(dataReader["CONTROL_TYPE"]);
					surveyDefine.CONTROL_FONTSIZE = Convert.ToInt32(dataReader["CONTROL_FONTSIZE"]);
					surveyDefine.CONTROL_HEIGHT = Convert.ToInt32(dataReader["CONTROL_HEIGHT"]);
					surveyDefine.CONTROL_WIDTH = Convert.ToInt32(dataReader["CONTROL_WIDTH"]);
					surveyDefine.CONTROL_MASK = dataReader["CONTROL_MASK"].ToString();
					surveyDefine.TITLE_FONTSIZE = Convert.ToInt32(dataReader["TITLE_FONTSIZE"]);
					surveyDefine.CONTROL_TOOLTIP = dataReader["CONTROL_TOOLTIP"].ToString();
					surveyDefine.NOTE = dataReader["NOTE"].ToString();
					surveyDefine.LIMIT_LOGIC = dataReader["LIMIT_LOGIC"].ToString();
					surveyDefine.FIX_LOGIC = dataReader["FIX_LOGIC"].ToString();
					surveyDefine.PRESET_LOGIC = dataReader["PRESET_LOGIC"].ToString();
					surveyDefine.GROUP_LEVEL = dataReader["GROUP_LEVEL"].ToString();
					surveyDefine.GROUP_CODEA = dataReader["GROUP_CODEA"].ToString();
					surveyDefine.GROUP_CODEB = dataReader["GROUP_CODEB"].ToString();
					surveyDefine.GROUP_PAGE_TYPE = Convert.ToInt32(dataReader["GROUP_PAGE_TYPE"]);
					surveyDefine.MT_GROUP_MSG = dataReader["MT_GROUP_MSG"].ToString();
					surveyDefine.MT_GROUP_COUNT = dataReader["MT_GROUP_COUNT"].ToString();
					surveyDefine.IS_ATTACH = Convert.ToInt32(dataReader["IS_ATTACH"]);
					surveyDefine.MP3_FILE = dataReader["MP3_FILE"].ToString();
					surveyDefine.MP3_START_TYPE = Convert.ToInt32(dataReader["MP3_START_TYPE"]);
					surveyDefine.SUMMARY_USE = Convert.ToInt32(dataReader["SUMMARY_USE"]);
					surveyDefine.SUMMARY_TITLE = dataReader["SUMMARY_TITLE"].ToString();
					surveyDefine.SUMMARY_INDEX = Convert.ToInt32(dataReader["SUMMARY_INDEX"]);
					surveyDefine.FILLDATA = dataReader["FILLDATA"].ToString();
					surveyDefine.EXTEND_1 = dataReader["EXTEND_1"].ToString();
					surveyDefine.EXTEND_2 = dataReader["EXTEND_2"].ToString();
					surveyDefine.EXTEND_3 = dataReader["EXTEND_3"].ToString();
					surveyDefine.EXTEND_4 = dataReader["EXTEND_4"].ToString();
					surveyDefine.EXTEND_5 = dataReader["EXTEND_5"].ToString();
					surveyDefine.EXTEND_6 = dataReader["EXTEND_6"].ToString();
					surveyDefine.EXTEND_7 = dataReader["EXTEND_7"].ToString();
					surveyDefine.EXTEND_8 = dataReader["EXTEND_8"].ToString();
					surveyDefine.EXTEND_9 = dataReader["EXTEND_9"].ToString();
					surveyDefine.EXTEND_10 = dataReader["EXTEND_10"].ToString();
				}
			}
			return surveyDefine;
		}

		public List<SurveyDefine> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyDefine> list = new List<SurveyDefine>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyDefine
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString(),
						QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]),
						QUESTION_USE = Convert.ToInt32(dataReader["QUESTION_USE"]),
						ANSWER_USE = Convert.ToInt32(dataReader["ANSWER_USE"]),
						COMBINE_INDEX = Convert.ToInt32(dataReader["COMBINE_INDEX"]),
						DETAIL_ID = dataReader["DETAIL_ID"].ToString(),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString(),
						SHOW_LOGIC = dataReader["SHOW_LOGIC"].ToString(),
						QUESTION_CONTENT = dataReader["QUESTION_CONTENT"].ToString(),
						SPSS_TITLE = dataReader["SPSS_TITLE"].ToString(),
						SPSS_CASE = Convert.ToInt32(dataReader["SPSS_CASE"]),
						SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]),
						SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader["SPSS_PRINT_DECIMAIL"]),
						MIN_COUNT = Convert.ToInt32(dataReader["MIN_COUNT"]),
						MAX_COUNT = Convert.ToInt32(dataReader["MAX_COUNT"]),
						IS_RANDOM = Convert.ToInt32(dataReader["IS_RANDOM"]),
						PAGE_COUNT_DOWN = Convert.ToInt32(dataReader["PAGE_COUNT_DOWN"]),
						CONTROL_TYPE = Convert.ToInt32(dataReader["CONTROL_TYPE"]),
						CONTROL_FONTSIZE = Convert.ToInt32(dataReader["CONTROL_FONTSIZE"]),
						CONTROL_HEIGHT = Convert.ToInt32(dataReader["CONTROL_HEIGHT"]),
						CONTROL_WIDTH = Convert.ToInt32(dataReader["CONTROL_WIDTH"]),
						CONTROL_MASK = dataReader["CONTROL_MASK"].ToString(),
						TITLE_FONTSIZE = Convert.ToInt32(dataReader["TITLE_FONTSIZE"]),
						CONTROL_TOOLTIP = dataReader["CONTROL_TOOLTIP"].ToString(),
						NOTE = dataReader["NOTE"].ToString(),
						LIMIT_LOGIC = dataReader["LIMIT_LOGIC"].ToString(),
						FIX_LOGIC = dataReader["FIX_LOGIC"].ToString(),
						PRESET_LOGIC = dataReader["PRESET_LOGIC"].ToString(),
						GROUP_LEVEL = dataReader["GROUP_LEVEL"].ToString(),
						GROUP_CODEA = dataReader["GROUP_CODEA"].ToString(),
						GROUP_CODEB = dataReader["GROUP_CODEB"].ToString(),
						GROUP_PAGE_TYPE = Convert.ToInt32(dataReader["GROUP_PAGE_TYPE"]),
						MT_GROUP_MSG = dataReader["MT_GROUP_MSG"].ToString(),
						MT_GROUP_COUNT = dataReader["MT_GROUP_COUNT"].ToString(),
						IS_ATTACH = Convert.ToInt32(dataReader["IS_ATTACH"]),
						MP3_FILE = dataReader["MP3_FILE"].ToString(),
						MP3_START_TYPE = Convert.ToInt32(dataReader["MP3_START_TYPE"]),
						SUMMARY_USE = Convert.ToInt32(dataReader["SUMMARY_USE"]),
						SUMMARY_TITLE = dataReader["SUMMARY_TITLE"].ToString(),
						SUMMARY_INDEX = Convert.ToInt32(dataReader["SUMMARY_INDEX"]),
						FILLDATA = dataReader["FILLDATA"].ToString(),
						EXTEND_1 = dataReader["EXTEND_1"].ToString(),
						EXTEND_2 = dataReader["EXTEND_2"].ToString(),
						EXTEND_3 = dataReader["EXTEND_3"].ToString(),
						EXTEND_4 = dataReader["EXTEND_4"].ToString(),
						EXTEND_5 = dataReader["EXTEND_5"].ToString(),
						EXTEND_6 = dataReader["EXTEND_6"].ToString(),
						EXTEND_7 = dataReader["EXTEND_7"].ToString(),
						EXTEND_8 = dataReader["EXTEND_8"].ToString(),
						EXTEND_9 = dataReader["EXTEND_9"].ToString(),
						EXTEND_10 = dataReader["EXTEND_10"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyDefine> GetList()
		{
			string string_ = "SELECT * FROM SurveyDefine ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyDefine surveyDefine_0)
		{
			string string_ = string.Format("INSERT INTO SurveyDefine(ANSWER_ORDER,PAGE_ID,QUESTION_NAME,QUESTION_TITLE,QUESTION_TYPE,QUESTION_USE,ANSWER_USE,COMBINE_INDEX,DETAIL_ID,PARENT_CODE,SHOW_LOGIC,QUESTION_CONTENT,SPSS_TITLE,SPSS_CASE,SPSS_VARIABLE,SPSS_PRINT_DECIMAIL,MIN_COUNT,MAX_COUNT,IS_RANDOM,PAGE_COUNT_DOWN,CONTROL_TYPE,CONTROL_FONTSIZE,CONTROL_HEIGHT,CONTROL_WIDTH,CONTROL_MASK,TITLE_FONTSIZE,CONTROL_TOOLTIP,NOTE,LIMIT_LOGIC,FIX_LOGIC,PRESET_LOGIC,GROUP_LEVEL,GROUP_CODEA,GROUP_CODEB,GROUP_PAGE_TYPE,MT_GROUP_MSG,MT_GROUP_COUNT,IS_ATTACH,MP3_FILE,MP3_START_TYPE,SUMMARY_USE,SUMMARY_TITLE,SUMMARY_INDEX,FILLDATA,EXTEND_1,EXTEND_2,EXTEND_3,EXTEND_4,EXTEND_5,EXTEND_6,EXTEND_7,EXTEND_8,EXTEND_9,EXTEND_10) VALUES({0},'{1}','{2}','{3}',{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}',{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},'{24}',{25},'{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}',{34},'{35}','{36}',{37},'{38}',{39},{40},'{41}',{42},'{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}')", new object[]
			{
				surveyDefine_0.ANSWER_ORDER,
				surveyDefine_0.PAGE_ID,
				surveyDefine_0.QUESTION_NAME,
				surveyDefine_0.QUESTION_TITLE,
				surveyDefine_0.QUESTION_TYPE,
				surveyDefine_0.QUESTION_USE,
				surveyDefine_0.ANSWER_USE,
				surveyDefine_0.COMBINE_INDEX,
				surveyDefine_0.DETAIL_ID,
				surveyDefine_0.PARENT_CODE,
				surveyDefine_0.SHOW_LOGIC,
				surveyDefine_0.QUESTION_CONTENT,
				surveyDefine_0.SPSS_TITLE,
				surveyDefine_0.SPSS_CASE,
				surveyDefine_0.SPSS_VARIABLE,
				surveyDefine_0.SPSS_PRINT_DECIMAIL,
				surveyDefine_0.MIN_COUNT,
				surveyDefine_0.MAX_COUNT,
				surveyDefine_0.IS_RANDOM,
				surveyDefine_0.PAGE_COUNT_DOWN,
				surveyDefine_0.CONTROL_TYPE,
				surveyDefine_0.CONTROL_FONTSIZE,
				surveyDefine_0.CONTROL_HEIGHT,
				surveyDefine_0.CONTROL_WIDTH,
				surveyDefine_0.CONTROL_MASK,
				surveyDefine_0.TITLE_FONTSIZE,
				surveyDefine_0.CONTROL_TOOLTIP,
				surveyDefine_0.NOTE,
				surveyDefine_0.LIMIT_LOGIC,
				surveyDefine_0.FIX_LOGIC,
				surveyDefine_0.PRESET_LOGIC,
				surveyDefine_0.GROUP_LEVEL,
				surveyDefine_0.GROUP_CODEA,
				surveyDefine_0.GROUP_CODEB,
				surveyDefine_0.GROUP_PAGE_TYPE,
				surveyDefine_0.MT_GROUP_MSG,
				surveyDefine_0.MT_GROUP_COUNT,
				surveyDefine_0.IS_ATTACH,
				surveyDefine_0.MP3_FILE,
				surveyDefine_0.MP3_START_TYPE,
				surveyDefine_0.SUMMARY_USE,
				surveyDefine_0.SUMMARY_TITLE,
				surveyDefine_0.SUMMARY_INDEX,
				surveyDefine_0.FILLDATA,
				surveyDefine_0.EXTEND_1,
				surveyDefine_0.EXTEND_2,
				surveyDefine_0.EXTEND_3,
				surveyDefine_0.EXTEND_4,
				surveyDefine_0.EXTEND_5,
				surveyDefine_0.EXTEND_6,
				surveyDefine_0.EXTEND_7,
				surveyDefine_0.EXTEND_8,
				surveyDefine_0.EXTEND_9,
				surveyDefine_0.EXTEND_10
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyDefine surveyDefine_0)
		{
			string string_ = string.Format("UPDATE SurveyDefine SET ANSWER_ORDER = {1},PAGE_ID = '{2}',QUESTION_NAME = '{3}',QUESTION_TITLE = '{4}',QUESTION_TYPE = {5},QUESTION_USE = {6},ANSWER_USE = {7},COMBINE_INDEX = {8},DETAIL_ID = '{9}',PARENT_CODE = '{10}',SHOW_LOGIC = '{11}',QUESTION_CONTENT = '{12}',SPSS_TITLE = '{13}',SPSS_CASE = {14},SPSS_VARIABLE = {15},SPSS_PRINT_DECIMAIL = {16},MIN_COUNT = {17},MAX_COUNT = {18},IS_RANDOM = {19},PAGE_COUNT_DOWN = {20},CONTROL_TYPE = {21},CONTROL_FONTSIZE = {22},CONTROL_HEIGHT = {23},CONTROL_WIDTH = {24},CONTROL_MASK = '{25}',TITLE_FONTSIZE = {26},CONTROL_TOOLTIP = '{27}',NOTE = '{28}',LIMIT_LOGIC = '{29}',FIX_LOGIC = '{30}',PRESET_LOGIC = '{31}',GROUP_LEVEL = '{32}',GROUP_CODEA = '{33}',GROUP_CODEB = '{34}',GROUP_PAGE_TYPE = {35},MT_GROUP_MSG = '{36}',MT_GROUP_COUNT = '{37}',IS_ATTACH = {38},MP3_FILE = '{39}',MP3_START_TYPE = {40},SUMMARY_USE = {41},SUMMARY_TITLE = '{42}',SUMMARY_INDEX = {43},FILLDATA = '{44}',EXTEND_1 = '{45}',EXTEND_2 = '{46}',EXTEND_3 = '{47}',EXTEND_4 = '{48}',EXTEND_5 = '{49}',EXTEND_6 = '{50}',EXTEND_7 = '{51}',EXTEND_8 = '{52}',EXTEND_9 = '{53}',EXTEND_10 = '{54}' WHERE ID = {0}", new object[]
			{
				surveyDefine_0.ID,
				surveyDefine_0.ANSWER_ORDER,
				surveyDefine_0.PAGE_ID,
				surveyDefine_0.QUESTION_NAME,
				surveyDefine_0.QUESTION_TITLE,
				surveyDefine_0.QUESTION_TYPE,
				surveyDefine_0.QUESTION_USE,
				surveyDefine_0.ANSWER_USE,
				surveyDefine_0.COMBINE_INDEX,
				surveyDefine_0.DETAIL_ID,
				surveyDefine_0.PARENT_CODE,
				surveyDefine_0.SHOW_LOGIC,
				surveyDefine_0.QUESTION_CONTENT,
				surveyDefine_0.SPSS_TITLE,
				surveyDefine_0.SPSS_CASE,
				surveyDefine_0.SPSS_VARIABLE,
				surveyDefine_0.SPSS_PRINT_DECIMAIL,
				surveyDefine_0.MIN_COUNT,
				surveyDefine_0.MAX_COUNT,
				surveyDefine_0.IS_RANDOM,
				surveyDefine_0.PAGE_COUNT_DOWN,
				surveyDefine_0.CONTROL_TYPE,
				surveyDefine_0.CONTROL_FONTSIZE,
				surveyDefine_0.CONTROL_HEIGHT,
				surveyDefine_0.CONTROL_WIDTH,
				surveyDefine_0.CONTROL_MASK,
				surveyDefine_0.TITLE_FONTSIZE,
				surveyDefine_0.CONTROL_TOOLTIP,
				surveyDefine_0.NOTE,
				surveyDefine_0.LIMIT_LOGIC,
				surveyDefine_0.FIX_LOGIC,
				surveyDefine_0.PRESET_LOGIC,
				surveyDefine_0.GROUP_LEVEL,
				surveyDefine_0.GROUP_CODEA,
				surveyDefine_0.GROUP_CODEB,
				surveyDefine_0.GROUP_PAGE_TYPE,
				surveyDefine_0.MT_GROUP_MSG,
				surveyDefine_0.MT_GROUP_COUNT,
				surveyDefine_0.IS_ATTACH,
				surveyDefine_0.MP3_FILE,
				surveyDefine_0.MP3_START_TYPE,
				surveyDefine_0.SUMMARY_USE,
				surveyDefine_0.SUMMARY_TITLE,
				surveyDefine_0.SUMMARY_INDEX,
				surveyDefine_0.FILLDATA,
				surveyDefine_0.EXTEND_1,
				surveyDefine_0.EXTEND_2,
				surveyDefine_0.EXTEND_3,
				surveyDefine_0.EXTEND_4,
				surveyDefine_0.EXTEND_5,
				surveyDefine_0.EXTEND_6,
				surveyDefine_0.EXTEND_7,
				surveyDefine_0.EXTEND_8,
				surveyDefine_0.EXTEND_9,
				surveyDefine_0.EXTEND_10
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyDefine surveyDefine_0)
		{
			string string_ = string.Format("DELETE FROM SurveyDefine WHERE ID ={0}", surveyDefine_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyDefine";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 55;
			string[] array = new string[55];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "题目输出顺序";
				array[2] = "页编号";
				array[3] = "问题编号";
				array[4] = "主题题干";
				array[5] = "主题题型";
				array[6] = "题目是否会被展示";
				array[7] = "答案是否会被导出";
				array[8] = "组合题的子题索引";
				array[9] = "选择项关联编码";
				array[10] = "多级选择项父关联编码";
				array[11] = "选项展示顺序控制";
				array[12] = "副题题干";
				array[13] = "SPSS 题干";
				array[14] = "SPSS 题型";
				array[15] = "SPSS 变量类型";
				array[16] = "SPSS 小数位";
				array[17] = "多选题的最小选择数";
				array[18] = "多选题的最大选择数";
				array[19] = "内部选项是否随机排列";
				array[20] = "页计时倒数 秒";
				array[21] = "控件的类型控制";
				array[22] = "控件的字体大小";
				array[23] = "控件的高度";
				array[24] = "控件的宽度";
				array[25] = "控件 Mask";
				array[26] = "问题标题的字体大小";
				array[27] = "控件 ToolTip";
				array[28] = "备注说明 / 显示题的内容";
				array[29] = "选项筛选的逻辑控制";
				array[30] = "固定选定的逻辑控制";
				array[31] = "预先选定的逻辑控制";
				array[32] = "循环题组级别";
				array[33] = "循环题组父循环代码A 层";
				array[34] = "循环题组父循环代码B 层";
				array[35] = "问题在循环题组中的位置类型";
				array[36] = "循环题组题信息";
				array[37] = "循环题组问题数量";
				array[38] = "是否激活关联附件功能";
				array[39] = "问题关联的题干录音";
				array[40] = "题干录音的启动模式";
				array[41] = "是否摘要";
				array[42] = "摘要标题";
				array[43] = "摘要索引";
				array[44] = "预填充数据";
				array[45] = "扩展内容 1";
				array[46] = "扩展内容 2";
				array[47] = "扩展内容 3";
				array[48] = "扩展内容 4";
				array[49] = "扩展内容 5";
				array[50] = "扩展内容 6";
				array[51] = "扩展内容 7";
				array[52] = "扩展内容 8";
				array[53] = "扩展内容 9";
				array[54] = "扩展内容 10";
			}
			else
			{
				array[0] = "ID";
				array[1] = "ANSWER_ORDER";
				array[2] = "PAGE_ID";
				array[3] = "QUESTION_NAME";
				array[4] = "QUESTION_TITLE";
				array[5] = "QUESTION_TYPE";
				array[6] = "QUESTION_USE";
				array[7] = "ANSWER_USE";
				array[8] = "COMBINE_INDEX";
				array[9] = "DETAIL_ID";
				array[10] = "PARENT_CODE";
				array[11] = "SHOW_LOGIC";
				array[12] = "QUESTION_CONTENT";
				array[13] = "SPSS_TITLE";
				array[14] = "SPSS_CASE";
				array[15] = "SPSS_VARIABLE";
				array[16] = "SPSS_PRINT_DECIMAIL";
				array[17] = "MIN_COUNT";
				array[18] = "MAX_COUNT";
				array[19] = "IS_RANDOM";
				array[20] = "PAGE_COUNT_DOWN";
				array[21] = "CONTROL_TYPE";
				array[22] = "CONTROL_FONTSIZE";
				array[23] = "CONTROL_HEIGHT";
				array[24] = "CONTROL_WIDTH";
				array[25] = "CONTROL_MASK";
				array[26] = "TITLE_FONTSIZE";
				array[27] = "CONTROL_TOOLTIP";
				array[28] = "NOTE";
				array[29] = "LIMIT_LOGIC";
				array[30] = "FIX_LOGIC";
				array[31] = "PRESET_LOGIC";
				array[32] = "GROUP_LEVEL";
				array[33] = "GROUP_CODEA";
				array[34] = "GROUP_CODEB";
				array[35] = "GROUP_PAGE_TYPE";
				array[36] = "MT_GROUP_MSG";
				array[37] = "MT_GROUP_COUNT";
				array[38] = "IS_ATTACH";
				array[39] = "MP3_FILE";
				array[40] = "MP3_START_TYPE";
				array[41] = "SUMMARY_USE";
				array[42] = "SUMMARY_TITLE";
				array[43] = "SUMMARY_INDEX";
				array[44] = "FILLDATA";
				array[45] = "EXTEND_1";
				array[46] = "EXTEND_2";
				array[47] = "EXTEND_3";
				array[48] = "EXTEND_4";
				array[49] = "EXTEND_5";
				array[50] = "EXTEND_6";
				array[51] = "EXTEND_7";
				array[52] = "EXTEND_8";
				array[53] = "EXTEND_9";
				array[54] = "EXTEND_10";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyDefine> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyDefine surveyDefine in list_0)
			{
				array[num, 0] = surveyDefine.ID.ToString();
				array[num, 1] = surveyDefine.ANSWER_ORDER.ToString();
				array[num, 2] = surveyDefine.PAGE_ID;
				array[num, 3] = surveyDefine.QUESTION_NAME;
				array[num, 4] = surveyDefine.QUESTION_TITLE;
				array[num, 5] = surveyDefine.QUESTION_TYPE.ToString();
				array[num, 6] = surveyDefine.QUESTION_USE.ToString();
				array[num, 7] = surveyDefine.ANSWER_USE.ToString();
				array[num, 8] = surveyDefine.COMBINE_INDEX.ToString();
				array[num, 9] = surveyDefine.DETAIL_ID;
				array[num, 10] = surveyDefine.PARENT_CODE;
				array[num, 11] = surveyDefine.SHOW_LOGIC;
				array[num, 12] = surveyDefine.QUESTION_CONTENT;
				array[num, 13] = surveyDefine.SPSS_TITLE;
				array[num, 14] = surveyDefine.SPSS_CASE.ToString();
				array[num, 15] = surveyDefine.SPSS_VARIABLE.ToString();
				array[num, 16] = surveyDefine.SPSS_PRINT_DECIMAIL.ToString();
				array[num, 17] = surveyDefine.MIN_COUNT.ToString();
				array[num, 18] = surveyDefine.MAX_COUNT.ToString();
				array[num, 19] = surveyDefine.IS_RANDOM.ToString();
				array[num, 20] = surveyDefine.PAGE_COUNT_DOWN.ToString();
				array[num, 21] = surveyDefine.CONTROL_TYPE.ToString();
				array[num, 22] = surveyDefine.CONTROL_FONTSIZE.ToString();
				array[num, 23] = surveyDefine.CONTROL_HEIGHT.ToString();
				array[num, 24] = surveyDefine.CONTROL_WIDTH.ToString();
				array[num, 25] = surveyDefine.CONTROL_MASK;
				array[num, 26] = surveyDefine.TITLE_FONTSIZE.ToString();
				array[num, 27] = surveyDefine.CONTROL_TOOLTIP;
				array[num, 28] = surveyDefine.NOTE;
				array[num, 29] = surveyDefine.LIMIT_LOGIC;
				array[num, 30] = surveyDefine.FIX_LOGIC;
				array[num, 31] = surveyDefine.PRESET_LOGIC;
				array[num, 32] = surveyDefine.GROUP_LEVEL;
				array[num, 33] = surveyDefine.GROUP_CODEA;
				array[num, 34] = surveyDefine.GROUP_CODEB;
				array[num, 35] = surveyDefine.GROUP_PAGE_TYPE.ToString();
				array[num, 36] = surveyDefine.MT_GROUP_MSG;
				array[num, 37] = surveyDefine.MT_GROUP_COUNT;
				array[num, 38] = surveyDefine.IS_ATTACH.ToString();
				array[num, 39] = surveyDefine.MP3_FILE;
				array[num, 40] = surveyDefine.MP3_START_TYPE.ToString();
				array[num, 41] = surveyDefine.SUMMARY_USE.ToString();
				array[num, 42] = surveyDefine.SUMMARY_TITLE;
				array[num, 43] = surveyDefine.SUMMARY_INDEX.ToString();
				array[num, 44] = surveyDefine.FILLDATA;
				array[num, 45] = surveyDefine.EXTEND_1;
				array[num, 46] = surveyDefine.EXTEND_2;
				array[num, 47] = surveyDefine.EXTEND_3;
				array[num, 48] = surveyDefine.EXTEND_4;
				array[num, 49] = surveyDefine.EXTEND_5;
				array[num, 50] = surveyDefine.EXTEND_6;
				array[num, 51] = surveyDefine.EXTEND_7;
				array[num, 52] = surveyDefine.EXTEND_8;
				array[num, 53] = surveyDefine.EXTEND_9;
				array[num, 54] = surveyDefine.EXTEND_10;
				num++;
			}
			return array;
		}

		public string GetQuestionTitleByName(string string_0)
		{
			string string_ = string.Format("select Question_Title from SurveyDefine where Question_Name='{0}'", string_0);
			return this.dbprovider_0.ExecuteScalarString(string_);
		}

		public SurveyDefine GetByName(string string_0)
		{
			string string_ = string.Format("select * from SurveyDefine where QUESTION_NAME='{0}'", string_0);
			return this.GetBySql(string_);
		}

		public string GetChildByIndex(string string_0, int int_0)
		{
			string string_ = string.Format("select Question_name from SurveyDefine where Page_id='{0}' and combine_index ={1}", string_0, int_0);
			return this.dbprovider_0.ExecuteScalarString(string_);
		}

		public SurveyDefine GetByPageId(string string_0)
		{
			string string_ = string.Format("select * from SurveyDefine where Page_id='{0}' AND COMBINE_INDEX<1", string_0);
			return this.GetBySql(string_);
		}

		public SurveyDefine GetByPageId(string string_0, int int_0)
		{
			string string_ = string.Format("select * from SurveyDefine where Page_id='{0}' and combine_index ={1}", string_0, int_0);
			return this.GetBySql(string_);
		}

		public List<SurveyDefine> GetListByPageId(string string_0)
		{
			string string_ = string.Format("select * from SurveyDefine where Page_id='{0}'order by COMBINE_INDEX", string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyDefine> GetQuotaConfig()
		{
			string string_ = "select * from SurveyDefine where Page_id='SYS_QUOTA' and QUESTION_TYPE>100 order by COMBINE_INDEX";
			return this.GetListBySql(string_);
		}

		public List<SurveyDefine> GetQCAdvanceConfig(int int_0)
		{
			string arg = "";
			if (int_0 != 101)
			{
				if (int_0 != 102)
				{
					arg = "QUESTION_TYPE = 102 or QUESTION_TYPE = 101";
				}
				else
				{
					arg = "QUESTION_TYPE = 102";
				}
			}
			else
			{
				arg = "QUESTION_TYPE = 101";
			}
			string string_ = string.Format("select * from SurveyDefine where Page_id='SYS_QUOTA' and ( {0} ) and ANSWER_USE = 1 order by COMBINE_INDEX", arg);
			return this.GetListBySql(string_);
		}

		public List<SurveyDefine> GetRecodeConfig(int int_0)
		{
			string string_ = "";
			if (int_0 != 1)
			{
				if (int_0 != 2)
				{
					string_ = "select * from SurveyDefine where (QUESTION_TYPE=7 or QUESTION_TYPE=8 or QUESTION_TYPE=9) and group_level ='' and combine_index = 0 order by id";
				}
				else
				{
					string_ = "select a.* from SurveyDefine a where a.PAGE_ID in (SELECT PAGE_ID FROM SURVEYLOGIC where logic_type='RECODE_LOGIC' and INNER_INDEX > 2000 GROUP BY PAGE_ID) and a.group_level ='' and a.combine_index = 0 order by a.id";
				}
			}
			else
			{
				string_ = "select * from SurveyDefine where (QUESTION_TYPE=7 or QUESTION_TYPE=8 or QUESTION_TYPE=9) and group_level ='' and combine_index = 0 and CONTROL_MASK = '1' order by id";
			}
			return this.GetListBySql(string_);
		}

		public List<SurveyDefine> GetListTTS()
		{
			string string_ = "select * from SurveyDefine where QUESTION_USE = 1 AND COMBINE_INDEX =0  AND ( QUESTION_TYPE <90 AND QUESTION_TYPE<>7  AND  QUESTION_TYPE<>8  AND QUESTION_TYPE<>9 AND  QUESTION_TYPE<>10 ) order by id";
			return this.GetListBySql(string_);
		}

		public bool SyncReadToWrite()
		{
			bool result = true;
			try
			{
				List<SurveyDefine> list = new List<SurveyDefine>();
				string string_ = "select * from SurveyDefine order by id";
				list = this.GetListBySql(string_);
				string_ = "delete from SurveyDefine";
				this.dbprovider_1.ExecuteNonQuery(string_);
				foreach (SurveyDefine surveyDefine_ in list)
				{
					this.AddToWrite(surveyDefine_);
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public void AddToWrite(SurveyDefine surveyDefine_0)
		{
			string string_ = string.Format("INSERT INTO SurveyDefine(ANSWER_ORDER,PAGE_ID,QUESTION_NAME,QUESTION_TITLE,QUESTION_TYPE,QUESTION_USE,ANSWER_USE,COMBINE_INDEX,DETAIL_ID,PARENT_CODE,SHOW_LOGIC,QUESTION_CONTENT,SPSS_TITLE,SPSS_CASE,SPSS_VARIABLE,SPSS_PRINT_DECIMAIL,MIN_COUNT,MAX_COUNT,IS_RANDOM,PAGE_COUNT_DOWN,CONTROL_TYPE,CONTROL_FONTSIZE,CONTROL_HEIGHT,CONTROL_WIDTH,CONTROL_MASK,TITLE_FONTSIZE,CONTROL_TOOLTIP,NOTE,LIMIT_LOGIC,FIX_LOGIC,PRESET_LOGIC,GROUP_LEVEL,GROUP_CODEA,GROUP_CODEB,GROUP_PAGE_TYPE,MT_GROUP_MSG,MT_GROUP_COUNT,IS_ATTACH,MP3_FILE,MP3_START_TYPE,SUMMARY_USE,SUMMARY_TITLE,SUMMARY_INDEX,FILLDATA,EXTEND_1,EXTEND_2,EXTEND_3,EXTEND_4,EXTEND_5,EXTEND_6,EXTEND_7,EXTEND_8,EXTEND_9,EXTEND_10) VALUES({0},'{1}','{2}','{3}',{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}',{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},'{24}',{25},'{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}',{34},'{35}','{36}',{37},'{38}',{39},{40},'{41}',{42},'{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}')", new object[]
			{
				surveyDefine_0.ANSWER_ORDER,
				surveyDefine_0.PAGE_ID,
				surveyDefine_0.QUESTION_NAME,
				surveyDefine_0.QUESTION_TITLE,
				surveyDefine_0.QUESTION_TYPE,
				surveyDefine_0.QUESTION_USE,
				surveyDefine_0.ANSWER_USE,
				surveyDefine_0.COMBINE_INDEX,
				surveyDefine_0.DETAIL_ID,
				surveyDefine_0.PARENT_CODE,
				surveyDefine_0.SHOW_LOGIC,
				surveyDefine_0.QUESTION_CONTENT,
				surveyDefine_0.SPSS_TITLE,
				surveyDefine_0.SPSS_CASE,
				surveyDefine_0.SPSS_VARIABLE,
				surveyDefine_0.SPSS_PRINT_DECIMAIL,
				surveyDefine_0.MIN_COUNT,
				surveyDefine_0.MAX_COUNT,
				surveyDefine_0.IS_RANDOM,
				surveyDefine_0.PAGE_COUNT_DOWN,
				surveyDefine_0.CONTROL_TYPE,
				surveyDefine_0.CONTROL_FONTSIZE,
				surveyDefine_0.CONTROL_HEIGHT,
				surveyDefine_0.CONTROL_WIDTH,
				surveyDefine_0.CONTROL_MASK,
				surveyDefine_0.TITLE_FONTSIZE,
				surveyDefine_0.CONTROL_TOOLTIP,
				surveyDefine_0.NOTE,
				surveyDefine_0.LIMIT_LOGIC,
				surveyDefine_0.FIX_LOGIC,
				surveyDefine_0.PRESET_LOGIC,
				surveyDefine_0.GROUP_LEVEL,
				surveyDefine_0.GROUP_CODEA,
				surveyDefine_0.GROUP_CODEB,
				surveyDefine_0.GROUP_PAGE_TYPE,
				surveyDefine_0.MT_GROUP_MSG,
				surveyDefine_0.MT_GROUP_COUNT,
				surveyDefine_0.IS_ATTACH,
				surveyDefine_0.MP3_FILE,
				surveyDefine_0.MP3_START_TYPE,
				surveyDefine_0.SUMMARY_USE,
				surveyDefine_0.SUMMARY_TITLE,
				surveyDefine_0.SUMMARY_INDEX,
				surveyDefine_0.FILLDATA,
				surveyDefine_0.EXTEND_1,
				surveyDefine_0.EXTEND_2,
				surveyDefine_0.EXTEND_3,
				surveyDefine_0.EXTEND_4,
				surveyDefine_0.EXTEND_5,
				surveyDefine_0.EXTEND_6,
				surveyDefine_0.EXTEND_7,
				surveyDefine_0.EXTEND_8,
				surveyDefine_0.EXTEND_9,
				surveyDefine_0.EXTEND_10
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
