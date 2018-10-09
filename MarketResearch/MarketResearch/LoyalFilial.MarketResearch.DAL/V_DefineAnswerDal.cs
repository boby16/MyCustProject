using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class V_DefineAnswerDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM V_DefineAnswer WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_DefineAnswer GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM V_DefineAnswer WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public V_DefineAnswer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_DefineAnswer v_DefineAnswer = new V_DefineAnswer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_DefineAnswer.ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]);
					v_DefineAnswer.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					v_DefineAnswer.QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]);
					v_DefineAnswer.QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString();
					v_DefineAnswer.DETAIL_ID = dataReader["DETAIL_ID"].ToString();
					v_DefineAnswer.PAGE_ID = dataReader["PAGE_ID"].ToString();
					v_DefineAnswer.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
					v_DefineAnswer.SPSS_CASE = Convert.ToInt32(dataReader["SPSS_CASE"]);
					v_DefineAnswer.SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]);
					v_DefineAnswer.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader["SPSS_PRINT_DECIMAIL"]);
					v_DefineAnswer.QNAME_MAPPING = dataReader["QNAME_MAPPING"].ToString();
					v_DefineAnswer.SPSS_TITLE = dataReader["SPSS_TITLE"].ToString();
					v_DefineAnswer.TEST_FIX_ANSWER = dataReader["TEST_FIX_ANSWER"].ToString();
				}
			}
			return v_DefineAnswer;
		}

		public List<V_DefineAnswer> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_DefineAnswer> list = new List<V_DefineAnswer>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_DefineAnswer
					{
						ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]),
						QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString(),
						DETAIL_ID = dataReader["DETAIL_ID"].ToString(),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString(),
						SPSS_CASE = Convert.ToInt32(dataReader["SPSS_CASE"]),
						SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]),
						SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader["SPSS_PRINT_DECIMAIL"]),
						QNAME_MAPPING = dataReader["QNAME_MAPPING"].ToString(),
						SPSS_TITLE = dataReader["SPSS_TITLE"].ToString(),
						TEST_FIX_ANSWER = dataReader["TEST_FIX_ANSWER"].ToString()
					});
				}
			}
			return list;
		}

		public List<V_DefineAnswer> GetList()
		{
			string string_ = "SELECT * FROM V_DefineAnswer ORDER BY ANSWER_ORDER";
			return this.GetListBySql(string_);
		}

		public List<V_DefineAnswer> GetListByQuestionType(string string_0)
		{
			string string_ = string.Format("SELECT * FROM V_DefineAnswer WHERE QUESTION_TYPE ={0} ORDER BY ANSWER_ORDER ", string_0);
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 12;
			string[] array = new string[12];
			if (bool_0)
			{
				array[0] = "题目输出顺序";
				array[1] = "问题编号（实际）";
				array[2] = "主题题型";
				array[3] = "题干";
				array[4] = "关联问题明细数据";
				array[5] = "页编号";
				array[6] = "SPSS 题型";
				array[7] = "SPSS 变量类型";
				array[8] = "SPSS 小数位";
				array[9] = "问题编号映射";
				array[10] = "SPSS 题干";
				array[11] = "父类代码";
			}
			else
			{
				array[0] = "ANSWER_ORDER";
				array[1] = "QUESTION_NAME";
				array[2] = "QUESTION_TYPE";
				array[3] = "QUESTION_TITLE";
				array[4] = "DETAIL_ID";
				array[5] = "PAGE_ID";
				array[6] = "SPSS_CASE";
				array[7] = "SPSS_VARIABLE";
				array[8] = "SPSS_PRINT_DECIMAIL";
				array[9] = "QNAME_MAPPING";
				array[10] = "SPSS_TITLE";
				array[11] = "PARENT_CODE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<V_DefineAnswer> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_DefineAnswer v_DefineAnswer in list_0)
			{
				array[num, 0] = v_DefineAnswer.ANSWER_ORDER.ToString();
				array[num, 1] = v_DefineAnswer.QUESTION_NAME;
				array[num, 2] = v_DefineAnswer.QUESTION_TYPE.ToString();
				array[num, 3] = v_DefineAnswer.QUESTION_TITLE;
				array[num, 4] = v_DefineAnswer.DETAIL_ID;
				array[num, 5] = v_DefineAnswer.PAGE_ID;
				array[num, 6] = v_DefineAnswer.SPSS_CASE.ToString();
				array[num, 7] = v_DefineAnswer.SPSS_VARIABLE.ToString();
				array[num, 8] = v_DefineAnswer.SPSS_PRINT_DECIMAIL.ToString();
				array[num, 9] = v_DefineAnswer.QNAME_MAPPING;
				array[num, 10] = v_DefineAnswer.SPSS_TITLE;
				array[num, 11] = v_DefineAnswer.PARENT_CODE;
				num++;
			}
			return array;
		}

		public List<V_DefineAnswer> GetListByTime()
		{
			string string_ = "SELECT * FROM V_DefineAnswer WHERE TEST_FIX_ANSWER <>'' and TEST_FIX_ANSWER is not null ORDER BY ANSWER_ORDER";
			return this.GetListBySql(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);
	}
}
