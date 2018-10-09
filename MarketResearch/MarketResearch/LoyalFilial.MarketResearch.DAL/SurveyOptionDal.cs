using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyOptionDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyOption WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyOption GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyOption WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyOption GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyOption surveyOption = new SurveyOption();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyOption.ID = Convert.ToInt32(dataReader["ID"]);
					surveyOption.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyOption.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					surveyOption.CODE = dataReader["CODE"].ToString();
					surveyOption.RANDOM_INDEX = Convert.ToInt32(dataReader["RANDOM_INDEX"]);
					surveyOption.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
				}
			}
			return surveyOption;
		}

		public List<SurveyOption> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyOption> list = new List<SurveyOption>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyOption
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						CODE = dataReader["CODE"].ToString(),
						RANDOM_INDEX = Convert.ToInt32(dataReader["RANDOM_INDEX"]),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyOption> GetList()
		{
			string string_ = "SELECT * FROM SurveyOption ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyOption surveyOption_0)
		{
			string string_ = string.Format("INSERT INTO SurveyOption(SURVEY_ID,QUESTION_NAME,CODE,RANDOM_INDEX,SURVEY_GUID) VALUES('{0}','{1}','{2}',{3},'{4}')", new object[]
			{
				surveyOption_0.SURVEY_ID,
				surveyOption_0.QUESTION_NAME,
				surveyOption_0.CODE,
				surveyOption_0.RANDOM_INDEX,
				surveyOption_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyOption surveyOption_0)
		{
			string string_ = string.Format("UPDATE SurveyOption SET SURVEY_ID = '{1}',QUESTION_NAME = '{2}',CODE = '{3}',RANDOM_INDEX = {4},SURVEY_GUID = '{5}' WHERE ID = {0}", new object[]
			{
				surveyOption_0.ID,
				surveyOption_0.SURVEY_ID,
				surveyOption_0.QUESTION_NAME,
				surveyOption_0.CODE,
				surveyOption_0.RANDOM_INDEX,
				surveyOption_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyOption surveyOption_0)
		{
			string string_ = string.Format("DELETE FROM SurveyOption WHERE ID ={0}", surveyOption_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyOption";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 6;
			string[] array = new string[6];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "问卷编号";
				array[2] = "问题编号";
				array[3] = "编码";
				array[4] = "已完成的随机数";
				array[5] = "GUID 全球唯一码";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "QUESTION_NAME";
				array[3] = "CODE";
				array[4] = "RANDOM_INDEX";
				array[5] = "SURVEY_GUID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyOption> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyOption surveyOption in list_0)
			{
				array[num, 0] = surveyOption.ID.ToString();
				array[num, 1] = surveyOption.SURVEY_ID;
				array[num, 2] = surveyOption.QUESTION_NAME;
				array[num, 3] = surveyOption.CODE;
				array[num, 4] = surveyOption.RANDOM_INDEX.ToString();
				array[num, 5] = surveyOption.SURVEY_GUID;
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
