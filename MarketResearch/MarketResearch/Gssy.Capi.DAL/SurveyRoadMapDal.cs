using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyRoadMapDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyRoadMap WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyRoadMap GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyRoadMap WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyRoadMap GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyRoadMap surveyRoadMap = new SurveyRoadMap();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyRoadMap.ID = Convert.ToInt32(dataReader["ID"]);
					surveyRoadMap.VERSION_ID = Convert.ToInt32(dataReader["VERSION_ID"]);
					surveyRoadMap.PART_NAME = dataReader["PART_NAME"].ToString();
					surveyRoadMap.PAGE_NOTE = dataReader["PAGE_NOTE"].ToString();
					surveyRoadMap.PAGE_ID = dataReader["PAGE_ID"].ToString();
					surveyRoadMap.ROUTE_LOGIC = dataReader["ROUTE_LOGIC"].ToString();
					surveyRoadMap.GROUP_ROUTE_LOGIC = dataReader["GROUP_ROUTE_LOGIC"].ToString();
					surveyRoadMap.FORM_NAME = dataReader["FORM_NAME"].ToString();
					surveyRoadMap.IS_JUMP = Convert.ToInt32(dataReader["IS_JUMP"]);
					surveyRoadMap.NOTE = dataReader["NOTE"].ToString();
				}
			}
			return surveyRoadMap;
		}

		public List<SurveyRoadMap> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyRoadMap> list = new List<SurveyRoadMap>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyRoadMap
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						VERSION_ID = Convert.ToInt32(dataReader["VERSION_ID"]),
						PART_NAME = dataReader["PART_NAME"].ToString(),
						PAGE_NOTE = dataReader["PAGE_NOTE"].ToString(),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						ROUTE_LOGIC = dataReader["ROUTE_LOGIC"].ToString(),
						GROUP_ROUTE_LOGIC = dataReader["GROUP_ROUTE_LOGIC"].ToString(),
						FORM_NAME = dataReader["FORM_NAME"].ToString(),
						IS_JUMP = Convert.ToInt32(dataReader["IS_JUMP"]),
						NOTE = dataReader["NOTE"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyRoadMap> GetList()
		{
			string string_ = "SELECT * FROM SurveyRoadMap ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyRoadMap surveyRoadMap_0)
		{
			string string_ = string.Format("INSERT INTO SurveyRoadMap(VERSION_ID,PART_NAME,PAGE_NOTE,PAGE_ID,ROUTE_LOGIC,GROUP_ROUTE_LOGIC,FORM_NAME,IS_JUMP,NOTE) VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}')", new object[]
			{
				surveyRoadMap_0.VERSION_ID,
				surveyRoadMap_0.PART_NAME,
				surveyRoadMap_0.PAGE_NOTE,
				surveyRoadMap_0.PAGE_ID,
				surveyRoadMap_0.ROUTE_LOGIC,
				surveyRoadMap_0.GROUP_ROUTE_LOGIC,
				surveyRoadMap_0.FORM_NAME,
				surveyRoadMap_0.IS_JUMP,
				surveyRoadMap_0.NOTE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyRoadMap surveyRoadMap_0)
		{
			string string_ = string.Format("UPDATE SurveyRoadMap SET VERSION_ID = {1},PART_NAME = '{2}',PAGE_NOTE = '{3}',PAGE_ID = '{4}',ROUTE_LOGIC = '{5}',GROUP_ROUTE_LOGIC = '{6}',FORM_NAME = '{7}',IS_JUMP = {8},NOTE = '{9}' WHERE ID = {0}", new object[]
			{
				surveyRoadMap_0.ID,
				surveyRoadMap_0.VERSION_ID,
				surveyRoadMap_0.PART_NAME,
				surveyRoadMap_0.PAGE_NOTE,
				surveyRoadMap_0.PAGE_ID,
				surveyRoadMap_0.ROUTE_LOGIC,
				surveyRoadMap_0.GROUP_ROUTE_LOGIC,
				surveyRoadMap_0.FORM_NAME,
				surveyRoadMap_0.IS_JUMP,
				surveyRoadMap_0.NOTE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyRoadMap surveyRoadMap_0)
		{
			string string_ = string.Format("DELETE FROM SurveyRoadMap WHERE ID ={0}", surveyRoadMap_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyRoadMap";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 10;
			string[] array = new string[10];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "路由版本编号";
				array[2] = "问卷分部";
				array[3] = "页说明";
				array[4] = "页 ID";
				array[5] = "页跳转路由逻辑";
				array[6] = "循环组组内跳转逻辑";
				array[7] = "页程序名";
				array[8] = "是否可跳转";
				array[9] = "备注";
			}
			else
			{
				array[0] = "ID";
				array[1] = "VERSION_ID";
				array[2] = "PART_NAME";
				array[3] = "PAGE_NOTE";
				array[4] = "PAGE_ID";
				array[5] = "ROUTE_LOGIC";
				array[6] = "GROUP_ROUTE_LOGIC";
				array[7] = "FORM_NAME";
				array[8] = "IS_JUMP";
				array[9] = "NOTE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyRoadMap> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyRoadMap surveyRoadMap in list_0)
			{
				array[num, 0] = surveyRoadMap.ID.ToString();
				array[num, 1] = surveyRoadMap.VERSION_ID.ToString();
				array[num, 2] = surveyRoadMap.PART_NAME;
				array[num, 3] = surveyRoadMap.PAGE_NOTE;
				array[num, 4] = surveyRoadMap.PAGE_ID;
				array[num, 5] = surveyRoadMap.ROUTE_LOGIC;
				array[num, 6] = surveyRoadMap.GROUP_ROUTE_LOGIC;
				array[num, 7] = surveyRoadMap.FORM_NAME;
				array[num, 8] = surveyRoadMap.IS_JUMP.ToString();
				array[num, 9] = surveyRoadMap.NOTE;
				num++;
			}
			return array;
		}

		public SurveyRoadMap GetByPageId(string string_0)
		{
			string string_ = string.Format("select * from SurveyRoadMap where PAGE_ID='{0}'", string_0);
			return this.GetBySql(string_);
		}

		public SurveyRoadMap GetByPageId(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyRoadMap where PAGE_ID='{0}' and VERSION_ID={1}", string_0, string_1);
			if (string_0.IndexOf("Redirect") > -1)
			{
				string_2 = string.Format("select * from SurveyRoadMap where PAGE_ID='{0}'", string_0);
			}
			return this.GetBySql(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
