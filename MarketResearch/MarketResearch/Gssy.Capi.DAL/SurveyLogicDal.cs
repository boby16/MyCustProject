using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyLogicDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyLogic WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyLogic GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyLogic WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyLogic GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyLogic surveyLogic = new SurveyLogic();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyLogic.ID = Convert.ToInt32(dataReader["ID"]);
					surveyLogic.PAGE_ID = dataReader["PAGE_ID"].ToString();
					surveyLogic.LOGIC_TYPE = dataReader["LOGIC_TYPE"].ToString();
					surveyLogic.INNER_INDEX = Convert.ToInt32(dataReader["INNER_INDEX"]);
					surveyLogic.FORMULA = dataReader["FORMULA"].ToString();
					surveyLogic.RECODE_ANSWER = dataReader["RECODE_ANSWER"].ToString();
					surveyLogic.LOGIC_MESSAGE = dataReader["LOGIC_MESSAGE"].ToString();
					surveyLogic.NOTE = dataReader["NOTE"].ToString();
					surveyLogic.IS_ALLOW_PASS = Convert.ToInt32(dataReader["IS_ALLOW_PASS"]);
				}
			}
			return surveyLogic;
		}

		public List<SurveyLogic> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyLogic> list = new List<SurveyLogic>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyLogic
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						LOGIC_TYPE = dataReader["LOGIC_TYPE"].ToString(),
						INNER_INDEX = Convert.ToInt32(dataReader["INNER_INDEX"]),
						FORMULA = dataReader["FORMULA"].ToString(),
						RECODE_ANSWER = dataReader["RECODE_ANSWER"].ToString(),
						LOGIC_MESSAGE = dataReader["LOGIC_MESSAGE"].ToString(),
						NOTE = dataReader["NOTE"].ToString(),
						IS_ALLOW_PASS = Convert.ToInt32(dataReader["IS_ALLOW_PASS"])
					});
				}
			}
			return list;
		}

		public List<SurveyLogic> GetList()
		{
			string string_ = "SELECT * FROM SurveyLogic ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyLogic surveyLogic_0)
		{
			string string_ = string.Format("INSERT INTO SurveyLogic(PAGE_ID,LOGIC_TYPE,INNER_INDEX,FORMULA,RECODE_ANSWER,LOGIC_MESSAGE,NOTE,IS_ALLOW_PASS) VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}',{7})", new object[]
			{
				surveyLogic_0.PAGE_ID,
				surveyLogic_0.LOGIC_TYPE,
				surveyLogic_0.INNER_INDEX,
				surveyLogic_0.FORMULA,
				surveyLogic_0.RECODE_ANSWER,
				surveyLogic_0.LOGIC_MESSAGE,
				surveyLogic_0.NOTE,
				surveyLogic_0.IS_ALLOW_PASS
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyLogic surveyLogic_0)
		{
			string string_ = string.Format("UPDATE SurveyLogic SET PAGE_ID = '{1}',LOGIC_TYPE = '{2}',INNER_INDEX = {3},FORMULA = '{4}',RECODE_ANSWER = '{5}',LOGIC_MESSAGE = '{6}',NOTE = '{7}',IS_ALLOW_PASS = {8} WHERE ID = {0}", new object[]
			{
				surveyLogic_0.ID,
				surveyLogic_0.PAGE_ID,
				surveyLogic_0.LOGIC_TYPE,
				surveyLogic_0.INNER_INDEX,
				surveyLogic_0.FORMULA,
				surveyLogic_0.RECODE_ANSWER,
				surveyLogic_0.LOGIC_MESSAGE,
				surveyLogic_0.NOTE,
				surveyLogic_0.IS_ALLOW_PASS
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyLogic surveyLogic_0)
		{
			string string_ = string.Format("DELETE FROM SurveyLogic WHERE ID ={0}", surveyLogic_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyLogic";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 9;
			string[] array = new string[9];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "页编号";
				array[2] = "逻辑类型";
				array[3] = "逻辑顺序";
				array[4] = "逻辑公式定义";
				array[5] = "RECODE 答案";
				array[6] = "提示信息";
				array[7] = "逻辑描述";
				array[8] = "允许督导确认后通过";
			}
			else
			{
				array[0] = "ID";
				array[1] = "PAGE_ID";
				array[2] = "LOGIC_TYPE";
				array[3] = "INNER_INDEX";
				array[4] = "FORMULA";
				array[5] = "RECODE_ANSWER";
				array[6] = "LOGIC_MESSAGE";
				array[7] = "NOTE";
				array[8] = "IS_ALLOW_PASS";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyLogic> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyLogic surveyLogic in list_0)
			{
				array[num, 0] = surveyLogic.ID.ToString();
				array[num, 1] = surveyLogic.PAGE_ID;
				array[num, 2] = surveyLogic.LOGIC_TYPE;
				array[num, 3] = surveyLogic.INNER_INDEX.ToString();
				array[num, 4] = surveyLogic.FORMULA;
				array[num, 5] = surveyLogic.RECODE_ANSWER;
				array[num, 6] = surveyLogic.LOGIC_MESSAGE;
				array[num, 7] = surveyLogic.NOTE;
				array[num, 8] = surveyLogic.IS_ALLOW_PASS.ToString();
				num++;
			}
			return array;
		}

		public List<SurveyLogic> GetCheckLogic(string string_0)
		{
			string arg = "CHECK_LOGIC";
			string string_ = string.Format("select * from SurveyLogic where PAGE_ID='{0}' and LOGIC_TYPE='{1}' order by INNER_INDEX", string_0, arg);
			return this.GetListBySql(string_);
		}

		public List<SurveyLogic> GetReCodeLogic(string string_0, int int_0 = 1, int int_1 = 0, int int_2 = 99999)
		{
			string text = "CHECK_LOGIC";
			string string_ = "";
			if (int_0 == 1)
			{
				if (int_2 >= 5000)
				{
					int_2 = 4999;
				}
			}
			else if (int_1 <= 2000)
			{
				int_1 = 2001;
			}
			string_ = string.Format("select * from SurveyLogic where PAGE_ID='{0}' and LOGIC_TYPE<>'{1}' and INNER_INDEX>={2} and INNER_INDEX<={3} order by INNER_INDEX", new object[]
			{
				string_0,
				text,
				int_1.ToString(),
				int_2.ToString()
			});
			return this.GetListBySql(string_);
		}

		public List<SurveyLogic> GetRecodeList(int int_0 = 2)
		{
			string arg = "CHECK_LOGIC";
			string string_ = "";
			if (int_0 == 1)
			{
				string_ = string.Format("select * from SurveyLogic where LOGIC_TYPE<>'{0}'  and INNER_INDEX<5000 order by INNER_INDEX", arg);
			}
			else
			{
				string_ = string.Format("select * from SurveyLogic where LOGIC_TYPE<>'{0}'  and INNER_INDEX>2000 order by INNER_INDEX", arg);
			}
			return this.GetListBySql(string_);
		}

		public List<SurveyLogic> GetCircleGuideLogic(string string_0, int int_0 = 1)
		{
			string arg = "CIRCLE_GUIDE_LOGIC";
			string string_ = string.Format("select * from SurveyLogic where PAGE_ID='{0}' and LOGIC_TYPE='{1}' order by INNER_INDEX", string_0, arg);
			if (int_0 == 1)
			{
				string_ = string.Format("select * from SurveyLogic where PAGE_ID='{0}' and LOGIC_TYPE='{1}' and INNER_INDEX<5000  order by INNER_INDEX", string_0, arg);
			}
			else
			{
				string_ = string.Format("select * from SurveyLogic where PAGE_ID='{0}' and LOGIC_TYPE='{1}' and INNER_INDEX>2000  order by INNER_INDEX", string_0, arg);
			}
			return this.GetListBySql(string_);
		}

		public List<SurveyLogic> GetPageInfo(int int_0 = 1)
		{
			string arg = "CHECK_LOGIC";
			string string_ = "";
			if (int_0 == 1)
			{
				string_ = string.Format("select 0 as ID, PAGE_ID, LOGIC_TYPE, Count(*) as INNER_INDEX, '' as FORMULA, '' as RECODE_ANSWER, '' as LOGIC_MESSAGE, '' as NOTE, 0 as IS_ALLOW_PASS from SurveyLogic where LOGIC_TYPE='{0}' GROUP BY PAGE_ID, LOGIC_TYPE", arg);
			}
			else
			{
				string_ = string.Format("select 0 as ID, PAGE_ID, LOGIC_TYPE, Count(*) as INNER_INDEX, '' as FORMULA, '' as RECODE_ANSWER, '' as LOGIC_MESSAGE, '' as NOTE, 0 as IS_ALLOW_PASS from SurveyLogic where LOGIC_TYPE<>'{0}' GROUP BY PAGE_ID, LOGIC_TYPE", arg);
			}
			return this.GetListBySql(string_);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
