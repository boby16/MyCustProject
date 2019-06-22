using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class V_SurveyQCDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM V_SurveyQC WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_SurveyQC GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM V_SurveyQC WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public V_SurveyQC GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_SurveyQC v_SurveyQC = new V_SurveyQC();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_SurveyQC.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_SurveyQC.ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]);
					v_SurveyQC.QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString();
					v_SurveyQC.QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]);
					v_SurveyQC.SPSS_TITLE = dataReader["SPSS_TITLE"].ToString();
					v_SurveyQC.CODE = dataReader["CODE"].ToString();
					v_SurveyQC.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					v_SurveyQC.PAGE_ID = dataReader["PAGE_ID"].ToString();
					v_SurveyQC.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					v_SurveyQC.DETAIL_ID = dataReader["DETAIL_ID"].ToString();
					v_SurveyQC.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
					v_SurveyQC.QUESTION_USE = Convert.ToInt32(dataReader["QUESTION_USE"]);
					v_SurveyQC.ANSWER_USE = Convert.ToInt32(dataReader["ANSWER_USE"]);
					v_SurveyQC.COMBINE_INDEX = Convert.ToInt32(dataReader["COMBINE_INDEX"]);
					v_SurveyQC.SPSS_CASE = Convert.ToInt32(dataReader["SPSS_CASE"]);
					v_SurveyQC.SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]);
					v_SurveyQC.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader["SPSS_PRINT_DECIMAIL"]);
					if (dataReader["SEQUENCE_ID"] is DBNull)
					{
						v_SurveyQC.SEQUENCE_ID = 0;
					}
					else
					{
						v_SurveyQC.SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]);
					}
				}
			}
			return v_SurveyQC;
		}

		public List<V_SurveyQC> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_SurveyQC> list = new List<V_SurveyQC>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					V_SurveyQC v_SurveyQC = new V_SurveyQC();
					v_SurveyQC.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_SurveyQC.ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]);
					v_SurveyQC.QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString();
					v_SurveyQC.QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]);
					v_SurveyQC.SPSS_TITLE = dataReader["SPSS_TITLE"].ToString();
					v_SurveyQC.CODE = dataReader["CODE"].ToString();
					v_SurveyQC.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					v_SurveyQC.PAGE_ID = dataReader["PAGE_ID"].ToString();
					v_SurveyQC.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					v_SurveyQC.DETAIL_ID = dataReader["DETAIL_ID"].ToString();
					v_SurveyQC.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
					v_SurveyQC.QUESTION_USE = Convert.ToInt32(dataReader["QUESTION_USE"]);
					v_SurveyQC.ANSWER_USE = Convert.ToInt32(dataReader["ANSWER_USE"]);
					v_SurveyQC.COMBINE_INDEX = Convert.ToInt32(dataReader["COMBINE_INDEX"]);
					v_SurveyQC.SPSS_CASE = Convert.ToInt32(dataReader["SPSS_CASE"]);
					v_SurveyQC.SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]);
					v_SurveyQC.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader["SPSS_PRINT_DECIMAIL"]);
					if (dataReader["SEQUENCE_ID"] is DBNull)
					{
						v_SurveyQC.SEQUENCE_ID = 0;
					}
					else
					{
						v_SurveyQC.SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]);
					}
					list.Add(v_SurveyQC);
				}
			}
			return list;
		}

		public List<V_SurveyQC> GetList()
		{
			string string_ = "SELECT * FROM V_SurveyQC ORDER BY ANSWER_ORDER";
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 18;
			string[] array = new string[18];
			if (bool_0)
			{
				array[0] = "问卷编号";
				array[1] = "题目输出顺序";
				array[2] = "输出问题使用";
				array[3] = "主题题型";
				array[4] = "SPSS 标题";
				array[5] = "编码（答案）";
				array[6] = "编码文本";
				array[7] = "页编号";
				array[8] = "问题编号（实际）";
				array[9] = "关联问题明细数据";
				array[10] = "父类代码";
				array[11] = "题目是否会被展示";
				array[12] = "答案是否会被导出";
				array[13] = "组合题的子题索引";
				array[14] = "SPSS 题型";
				array[15] = "SPSS 变量类型";
				array[16] = "SPSS 小数位";
				array[17] = "问卷序列号";
			}
			else
			{
				array[0] = "SURVEY_ID";
				array[1] = "ANSWER_ORDER";
				array[2] = "QUESTION_TITLE";
				array[3] = "QUESTION_TYPE";
				array[4] = "SPSS_TITLE";
				array[5] = "CODE";
				array[6] = "CODE_TEXT";
				array[7] = "PAGE_ID";
				array[8] = "QUESTION_NAME";
				array[9] = "DETAIL_ID";
				array[10] = "PARENT_CODE";
				array[11] = "QUESTION_USE";
				array[12] = "ANSWER_USE";
				array[13] = "COMBINE_INDEX";
				array[14] = "SPSS_CASE";
				array[15] = "SPSS_VARIABLE";
				array[16] = "SPSS_PRINT_DECIMAIL";
				array[17] = "SEQUENCE_ID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<V_SurveyQC> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_SurveyQC v_SurveyQC in list_0)
			{
				array[num, 0] = v_SurveyQC.SURVEY_ID;
				array[num, 1] = v_SurveyQC.ANSWER_ORDER.ToString();
				array[num, 2] = v_SurveyQC.QUESTION_TITLE;
				array[num, 3] = v_SurveyQC.QUESTION_TYPE.ToString();
				array[num, 4] = v_SurveyQC.SPSS_TITLE;
				array[num, 5] = v_SurveyQC.CODE;
				array[num, 6] = v_SurveyQC.CODE_TEXT;
				array[num, 7] = v_SurveyQC.PAGE_ID;
				array[num, 8] = v_SurveyQC.QUESTION_NAME;
				array[num, 9] = v_SurveyQC.DETAIL_ID;
				array[num, 10] = v_SurveyQC.PARENT_CODE;
				array[num, 11] = v_SurveyQC.QUESTION_USE.ToString();
				array[num, 12] = v_SurveyQC.ANSWER_USE.ToString();
				array[num, 13] = v_SurveyQC.COMBINE_INDEX.ToString();
				array[num, 14] = v_SurveyQC.SPSS_CASE.ToString();
				array[num, 15] = v_SurveyQC.SPSS_VARIABLE.ToString();
				array[num, 16] = v_SurveyQC.SPSS_PRINT_DECIMAIL.ToString();
				array[num, 17] = v_SurveyQC.SEQUENCE_ID.ToString();
				num++;
			}
			return array;
		}

		public List<V_SurveyQC> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format("SELECT B.SURVEY_ID as Old_ID, '{0}' AS SURVEY_ID, A.ANSWER_ORDER, A.QUESTION_TITLE,A.SPSS_TITLE, B.CODE, '' AS CODE_TEXT, A.PAGE_ID, A.QUESTION_NAME, A.QUESTION_TYPE,A.DETAIL_ID, A.PARENT_CODE, A.QUESTION_USE, A.ANSWER_USE, A.COMBINE_INDEX, A.SPSS_CASE, A.SPSS_VARIABLE,A.SPSS_PRINT_DECIMAIL,B.SEQUENCE_ID FROM S_Define A LEFT JOIN  SurveyAnswer AS B ON A.QUESTION_NAME = B.QUESTION_NAME  AND B.SURVEY_ID = '{0}' WHERE A.ANSWER_USE>0 ORDER BY A.ANSWER_ORDER;", string_0);
			return this.GetListBySql(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
