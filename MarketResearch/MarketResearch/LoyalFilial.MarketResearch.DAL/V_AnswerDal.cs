using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class V_AnswerDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM V_Answer WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_Answer GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM V_Answer WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public V_Answer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_Answer v_Answer = new V_Answer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_Answer.ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]);
					v_Answer.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					v_Answer.CODE = dataReader["CODE"].ToString();
					v_Answer.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_Answer.SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]);
				}
			}
			return v_Answer;
		}

		public List<V_Answer> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_Answer> list = new List<V_Answer>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_Answer
					{
						ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						CODE = dataReader["CODE"].ToString(),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"])
					});
				}
			}
			return list;
		}

		public List<V_Answer> GetList()
		{
			string string_ = "SELECT * FROM V_Answer ORDER BY ANSWER_ORDER";
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 5;
			string[] array = new string[5];
			if (bool_0)
			{
				array[0] = "题目输出顺序";
				array[1] = "问题编号（实际）";
				array[2] = "编码（答案）";
				array[3] = "问卷编号";
				array[4] = "SPSS 变量类型";
			}
			else
			{
				array[0] = "ANSWER_ORDER";
				array[1] = "QUESTION_NAME";
				array[2] = "CODE";
				array[3] = "SURVEY_ID";
				array[4] = "SPSS_VARIABLE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<V_Answer> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_Answer v_Answer in list_0)
			{
				array[num, 0] = v_Answer.ANSWER_ORDER.ToString();
				array[num, 1] = v_Answer.QUESTION_NAME;
				array[num, 2] = v_Answer.CODE;
				array[num, 3] = v_Answer.SURVEY_ID;
				array[num, 4] = v_Answer.SPSS_VARIABLE.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
