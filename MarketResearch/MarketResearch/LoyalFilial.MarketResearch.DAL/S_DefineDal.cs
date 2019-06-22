using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class S_DefineDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM S_Define WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public S_Define GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM S_Define WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public S_Define GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			S_Define s_Define = new S_Define();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					s_Define.ID = Convert.ToInt32(dataReader["ID"]);
					s_Define.ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]);
					s_Define.PAGE_ID = dataReader["PAGE_ID"].ToString();
					s_Define.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					s_Define.QNAME_MAPPING = dataReader["QNAME_MAPPING"].ToString();
					s_Define.QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]);
					s_Define.QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString();
					s_Define.DETAIL_ID = dataReader["DETAIL_ID"].ToString();
					s_Define.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
					s_Define.QUESTION_USE = Convert.ToInt32(dataReader["QUESTION_USE"]);
					s_Define.ANSWER_USE = Convert.ToInt32(dataReader["ANSWER_USE"]);
					s_Define.COMBINE_INDEX = Convert.ToInt32(dataReader["COMBINE_INDEX"]);
					s_Define.SPSS_TITLE = dataReader["SPSS_TITLE"].ToString();
					s_Define.SPSS_CASE = Convert.ToInt32(dataReader["SPSS_CASE"]);
					s_Define.SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]);
					s_Define.SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader["SPSS_PRINT_DECIMAIL"]);
					s_Define.SUMMARY_USE = Convert.ToInt32(dataReader["SUMMARY_USE"]);
					s_Define.SUMMARY_TITLE = dataReader["SUMMARY_TITLE"].ToString();
					s_Define.SUMMARY_INDEX = Convert.ToInt32(dataReader["SUMMARY_INDEX"]);
					s_Define.TEST_FIX_ANSWER = dataReader["TEST_FIX_ANSWER"].ToString();
				}
			}
			return s_Define;
		}

		public List<S_Define> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<S_Define> list = new List<S_Define>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new S_Define
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						ANSWER_ORDER = Convert.ToInt32(dataReader["ANSWER_ORDER"]),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						QNAME_MAPPING = dataReader["QNAME_MAPPING"].ToString(),
						QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]),
						QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString(),
						DETAIL_ID = dataReader["DETAIL_ID"].ToString(),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString(),
						QUESTION_USE = Convert.ToInt32(dataReader["QUESTION_USE"]),
						ANSWER_USE = Convert.ToInt32(dataReader["ANSWER_USE"]),
						COMBINE_INDEX = Convert.ToInt32(dataReader["COMBINE_INDEX"]),
						SPSS_TITLE = dataReader["SPSS_TITLE"].ToString(),
						SPSS_CASE = Convert.ToInt32(dataReader["SPSS_CASE"]),
						SPSS_VARIABLE = Convert.ToInt32(dataReader["SPSS_VARIABLE"]),
						SPSS_PRINT_DECIMAIL = Convert.ToInt32(dataReader["SPSS_PRINT_DECIMAIL"]),
						SUMMARY_USE = Convert.ToInt32(dataReader["SUMMARY_USE"]),
						SUMMARY_TITLE = dataReader["SUMMARY_TITLE"].ToString(),
						SUMMARY_INDEX = Convert.ToInt32(dataReader["SUMMARY_INDEX"]),
						TEST_FIX_ANSWER = dataReader["TEST_FIX_ANSWER"].ToString()
					});
				}
			}
			return list;
		}

		public List<S_Define> GetList()
		{
			string string_ = "SELECT * FROM S_Define ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(S_Define s_Define_0)
		{
			string string_ = string.Format("INSERT INTO S_Define(ANSWER_ORDER,PAGE_ID,QUESTION_NAME,QNAME_MAPPING,QUESTION_TYPE,QUESTION_TITLE,DETAIL_ID,PARENT_CODE,QUESTION_USE,ANSWER_USE,COMBINE_INDEX,SPSS_TITLE,SPSS_CASE,SPSS_VARIABLE,SPSS_PRINT_DECIMAIL,SUMMARY_USE,SUMMARY_TITLE,SUMMARY_INDEX,TEST_FIX_ANSWER) VALUES({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}',{8},{9},{10},'{11}',{12},{13},{14},{15},'{16}',{17},'{18}')", new object[]
			{
				s_Define_0.ANSWER_ORDER,
				s_Define_0.PAGE_ID,
				s_Define_0.QUESTION_NAME,
				s_Define_0.QNAME_MAPPING,
				s_Define_0.QUESTION_TYPE,
				s_Define_0.QUESTION_TITLE,
				s_Define_0.DETAIL_ID,
				s_Define_0.PARENT_CODE,
				s_Define_0.QUESTION_USE,
				s_Define_0.ANSWER_USE,
				s_Define_0.COMBINE_INDEX,
				s_Define_0.SPSS_TITLE,
				s_Define_0.SPSS_CASE,
				s_Define_0.SPSS_VARIABLE,
				s_Define_0.SPSS_PRINT_DECIMAIL,
				s_Define_0.SUMMARY_USE,
				s_Define_0.SUMMARY_TITLE,
				s_Define_0.SUMMARY_INDEX,
				s_Define_0.TEST_FIX_ANSWER
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(S_Define s_Define_0)
		{
			string string_ = string.Format("UPDATE S_Define SET ANSWER_ORDER = {1},PAGE_ID = '{2}',QUESTION_NAME = '{3}',QNAME_MAPPING = '{4}',QUESTION_TYPE = {5},QUESTION_TITLE = '{6}',DETAIL_ID = '{7}',PARENT_CODE = '{8}',QUESTION_USE = {9},ANSWER_USE = {10},COMBINE_INDEX = {11},SPSS_TITLE = '{12}',SPSS_CASE = {13},SPSS_VARIABLE = {14},SPSS_PRINT_DECIMAIL = {15},SUMMARY_USE = {16},SUMMARY_TITLE = '{17}',SUMMARY_INDEX = {18},TEST_FIX_ANSWER = '{19}' WHERE ID = {0}", new object[]
			{
				s_Define_0.ID,
				s_Define_0.ANSWER_ORDER,
				s_Define_0.PAGE_ID,
				s_Define_0.QUESTION_NAME,
				s_Define_0.QNAME_MAPPING,
				s_Define_0.QUESTION_TYPE,
				s_Define_0.QUESTION_TITLE,
				s_Define_0.DETAIL_ID,
				s_Define_0.PARENT_CODE,
				s_Define_0.QUESTION_USE,
				s_Define_0.ANSWER_USE,
				s_Define_0.COMBINE_INDEX,
				s_Define_0.SPSS_TITLE,
				s_Define_0.SPSS_CASE,
				s_Define_0.SPSS_VARIABLE,
				s_Define_0.SPSS_PRINT_DECIMAIL,
				s_Define_0.SUMMARY_USE,
				s_Define_0.SUMMARY_TITLE,
				s_Define_0.SUMMARY_INDEX,
				s_Define_0.TEST_FIX_ANSWER
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(S_Define s_Define_0)
		{
			string string_ = string.Format("DELETE FROM S_Define WHERE ID ={0}", s_Define_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM S_Define";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 20;
			string[] array = new string[20];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "题目输出顺序";
				array[2] = "页编号";
				array[3] = "问题编号";
				array[4] = "问题编号映射";
				array[5] = "主题题型";
				array[6] = "问题题干";
				array[7] = "关联问题明细数据";
				array[8] = "父类代码";
				array[9] = "输出Excel问题使用";
				array[10] = "输出Excel答案使用";
				array[11] = "组合题的子题索引";
				array[12] = "SPSS 题干";
				array[13] = "SPSS 题型";
				array[14] = "SPSS 变量类型";
				array[15] = "SPSS 小数位";
				array[16] = "是否摘要";
				array[17] = "摘要标题";
				array[18] = "摘要索引";
				array[19] = "测试指定答案";
			}
			else
			{
				array[0] = "ID";
				array[1] = "ANSWER_ORDER";
				array[2] = "PAGE_ID";
				array[3] = "QUESTION_NAME";
				array[4] = "QNAME_MAPPING";
				array[5] = "QUESTION_TYPE";
				array[6] = "QUESTION_TITLE";
				array[7] = "DETAIL_ID";
				array[8] = "PARENT_CODE";
				array[9] = "QUESTION_USE";
				array[10] = "ANSWER_USE";
				array[11] = "COMBINE_INDEX";
				array[12] = "SPSS_TITLE";
				array[13] = "SPSS_CASE";
				array[14] = "SPSS_VARIABLE";
				array[15] = "SPSS_PRINT_DECIMAIL";
				array[16] = "SUMMARY_USE";
				array[17] = "SUMMARY_TITLE";
				array[18] = "SUMMARY_INDEX";
				array[19] = "TEST_FIX_ANSWER";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<S_Define> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (S_Define s_Define in list_0)
			{
				array[num, 0] = s_Define.ID.ToString();
				array[num, 1] = s_Define.ANSWER_ORDER.ToString();
				array[num, 2] = s_Define.PAGE_ID;
				array[num, 3] = s_Define.QUESTION_NAME;
				array[num, 4] = s_Define.QNAME_MAPPING;
				array[num, 5] = s_Define.QUESTION_TYPE.ToString();
				array[num, 6] = s_Define.QUESTION_TITLE;
				array[num, 7] = s_Define.DETAIL_ID;
				array[num, 8] = s_Define.PARENT_CODE;
				array[num, 9] = s_Define.QUESTION_USE.ToString();
				array[num, 10] = s_Define.ANSWER_USE.ToString();
				array[num, 11] = s_Define.COMBINE_INDEX.ToString();
				array[num, 12] = s_Define.SPSS_TITLE;
				array[num, 13] = s_Define.SPSS_CASE.ToString();
				array[num, 14] = s_Define.SPSS_VARIABLE.ToString();
				array[num, 15] = s_Define.SPSS_PRINT_DECIMAIL.ToString();
				array[num, 16] = s_Define.SUMMARY_USE.ToString();
				array[num, 17] = s_Define.SUMMARY_TITLE;
				array[num, 18] = s_Define.SUMMARY_INDEX.ToString();
				array[num, 19] = s_Define.TEST_FIX_ANSWER;
				num++;
			}
			return array;
		}

		public bool ExistsByCode(string string_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM S_Define WHERE QUESTION_NAME ='{0}' and ANSWER_USE = 1", string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public S_Define GetByQName(string string_0)
		{
			string string_ = string.Format("select * from S_Define where QUESTION_NAME='{0}'", string_0);
			return this.GetBySql(string_);
		}

		public List<S_Define> GetListByPageId(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from S_Define where PAGE_ID='{0}' and ANSWER_USE = 1 and QUESTION_NAME like '{1}%'", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public string GetQNameByMapping(string string_0)
		{
			string string_ = string.Format("select count(*) as nCount from S_Define where QNAME_MAPPING='{0}'", string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			string result;
			if (num == 1)
			{
				string_ = string.Format("select * from S_Define where QNAME_MAPPING='{0}'", string_0);
				S_Define bySql = this.GetBySql(string_);
				result = bySql.QUESTION_NAME;
			}
			else
			{
				result = "";
			}
			return result;
		}

		public bool SyncReadToWrite()
		{
			bool result = true;
			try
			{
				List<S_Define> list = new List<S_Define>();
				string string_ = "select * from S_Define order by id";
				list = this.GetListBySql(string_);
				string_ = "delete from S_Define";
				this.dbprovider_1.ExecuteNonQuery(string_);
				foreach (S_Define s_Define_ in list)
				{
					this.AddToWrite(s_Define_);
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public void AddToWrite(S_Define s_Define_0)
		{
			string string_ = string.Format("INSERT INTO S_Define(ANSWER_ORDER,PAGE_ID,QUESTION_NAME,QNAME_MAPPING,QUESTION_TYPE,QUESTION_TITLE,DETAIL_ID,PARENT_CODE,QUESTION_USE,ANSWER_USE,COMBINE_INDEX,SPSS_TITLE,SPSS_CASE,SPSS_VARIABLE,SPSS_PRINT_DECIMAIL,SUMMARY_USE,SUMMARY_TITLE,SUMMARY_INDEX,TEST_FIX_ANSWER) VALUES({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}',{8},{9},{10},'{11}',{12},{13},{14},{15},'{16}',{17},'{18}')", new object[]
			{
				s_Define_0.ANSWER_ORDER,
				s_Define_0.PAGE_ID,
				s_Define_0.QUESTION_NAME,
				s_Define_0.QNAME_MAPPING,
				s_Define_0.QUESTION_TYPE,
				s_Define_0.QUESTION_TITLE,
				s_Define_0.DETAIL_ID,
				s_Define_0.PARENT_CODE,
				s_Define_0.QUESTION_USE,
				s_Define_0.ANSWER_USE,
				s_Define_0.COMBINE_INDEX,
				s_Define_0.SPSS_TITLE,
				s_Define_0.SPSS_CASE,
				s_Define_0.SPSS_VARIABLE,
				s_Define_0.SPSS_PRINT_DECIMAIL,
				s_Define_0.SUMMARY_USE,
				s_Define_0.SUMMARY_TITLE,
				s_Define_0.SUMMARY_INDEX,
				s_Define_0.TEST_FIX_ANSWER
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
