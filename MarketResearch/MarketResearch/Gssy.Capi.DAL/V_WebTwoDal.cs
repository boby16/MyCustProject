using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class V_WebTwoDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM V_WebTwo WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public V_WebTwo GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM V_WebTwo WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public V_WebTwo GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			V_WebTwo v_WebTwo = new V_WebTwo();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					v_WebTwo.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					v_WebTwo.URI_DOMAIN = dataReader["URI_DOMAIN"].ToString();
					v_WebTwo.URI_DOMAIN_TWO = dataReader["URI_DOMAIN_TWO"].ToString();
					v_WebTwo.STAY_TIME = Convert.ToInt32(dataReader["STAY_TIME"]);
				}
			}
			return v_WebTwo;
		}

		public List<V_WebTwo> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<V_WebTwo> list = new List<V_WebTwo>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new V_WebTwo
					{
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						URI_DOMAIN = dataReader["URI_DOMAIN"].ToString(),
						URI_DOMAIN_TWO = dataReader["URI_DOMAIN_TWO"].ToString(),
						STAY_TIME = Convert.ToInt32(dataReader["STAY_TIME"])
					});
				}
			}
			return list;
		}

		public List<V_WebTwo> GetList()
		{
			string string_ = "SELECT * FROM V_WebTwo ORDER BY ANSWER_ORDER";
			return this.GetListBySql(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 4;
			string[] array = new string[4];
			if (bool_0)
			{
				array[0] = "问卷编号";
				array[1] = "一级域名";
				array[2] = "二级域名";
				array[3] = "停留时长 秒数";
			}
			else
			{
				array[0] = "SURVEY_ID";
				array[1] = "URI_DOMAIN";
				array[2] = "URI_DOMAIN_TWO";
				array[3] = "STAY_TIME";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<V_WebTwo> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (V_WebTwo v_WebTwo in list_0)
			{
				array[num, 0] = v_WebTwo.SURVEY_ID;
				array[num, 1] = v_WebTwo.URI_DOMAIN;
				array[num, 2] = v_WebTwo.URI_DOMAIN_TWO;
				array[num, 3] = v_WebTwo.STAY_TIME.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
