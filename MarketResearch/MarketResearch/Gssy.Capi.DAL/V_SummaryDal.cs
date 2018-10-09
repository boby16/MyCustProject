using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class V_SummaryDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM V_Summary WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_Summary GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM V_Summary WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public V_Summary GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_Summary v_Summary = new V_Summary();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_Summary.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_Summary.SUMMARY_INDEX = Convert.ToInt32(dataReader["SUMMARY_INDEX"]);
					v_Summary.SUMMARY_TITLE = dataReader["SUMMARY_TITLE"].ToString();
					v_Summary.CODE = dataReader["CODE"].ToString();
					v_Summary.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					v_Summary.SUMMARY_USE = Convert.ToInt32(dataReader["SUMMARY_USE"]);
					v_Summary.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					v_Summary.DETAIL_ID = dataReader["DETAIL_ID"].ToString();
					v_Summary.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
				}
			}
			return v_Summary;
		}

		public List<V_Summary> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_Summary> list = new List<V_Summary>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_Summary
					{
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SUMMARY_INDEX = Convert.ToInt32(dataReader["SUMMARY_INDEX"]),
						SUMMARY_TITLE = dataReader["SUMMARY_TITLE"].ToString(),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						SUMMARY_USE = Convert.ToInt32(dataReader["SUMMARY_USE"]),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						DETAIL_ID = dataReader["DETAIL_ID"].ToString(),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString()
					});
				}
			}
			return list;
		}

		public List<V_Summary> GetList()
		{
			string string_ = "SELECT * FROM V_Summary ORDER BY ANSWER_ORDER";
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 9;
			string[] array = new string[9];
			if (bool_0)
			{
				array[0] = "问卷编号";
				array[1] = "摘要索引";
				array[2] = "摘要标题";
				array[3] = "编码（答案）";
				array[4] = "编码文本";
				array[5] = "是否摘要";
				array[6] = "问题编号（实际）";
				array[7] = "关联问题明细数据";
				array[8] = "父类代码";
			}
			else
			{
				array[0] = "SURVEY_ID";
				array[1] = "SUMMARY_INDEX";
				array[2] = "SUMMARY_TITLE";
				array[3] = "CODE";
				array[4] = "CODE_TEXT";
				array[5] = "SUMMARY_USE";
				array[6] = "QUESTION_NAME";
				array[7] = "DETAIL_ID";
				array[8] = "PARENT_CODE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<V_Summary> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_Summary v_Summary in list_0)
			{
				array[num, 0] = v_Summary.SURVEY_ID;
				array[num, 1] = v_Summary.SUMMARY_INDEX.ToString();
				array[num, 2] = v_Summary.SUMMARY_TITLE;
				array[num, 3] = v_Summary.CODE;
				array[num, 4] = v_Summary.CODE_TEXT;
				array[num, 5] = v_Summary.SUMMARY_USE.ToString();
				array[num, 6] = v_Summary.QUESTION_NAME;
				array[num, 7] = v_Summary.DETAIL_ID;
				array[num, 8] = v_Summary.PARENT_CODE;
				num++;
			}
			return array;
		}

		public List<V_Summary> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format("SELECT B.SURVEY_ID, A.SUMMARY_INDEX, A.SUMMARY_TITLE, B.CODE, '' AS CODE_TEXT, A.SUMMARY_USE, A.QUESTION_NAME, A.DETAIL_ID, A.PARENT_CODE FROM S_Define AS A LEFT JOIN SurveyAnswer AS B ON A.QUESTION_NAME = B.QUESTION_NAME AND B.SURVEY_ID='{0}' WHERE A.SUMMARY_USE=1 ORDER BY A.SUMMARY_INDEX", string_0);
			return this.GetListBySql(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
