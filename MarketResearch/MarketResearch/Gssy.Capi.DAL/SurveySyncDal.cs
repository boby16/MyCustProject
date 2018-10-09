using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveySyncDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveySync WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveySync GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveySync WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveySync GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveySync surveySync = new SurveySync();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveySync.ID = Convert.ToInt32(dataReader["ID"]);
					surveySync.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveySync.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
					surveySync.SYNC_STATE = Convert.ToInt32(dataReader["SYNC_STATE"]);
					surveySync.SYNC_DATE = new DateTime?(Convert.ToDateTime(dataReader["SYNC_DATE"].ToString()));
					surveySync.SYNC_NOTE = dataReader["SYNC_NOTE"].ToString();
				}
			}
			return surveySync;
		}

		public List<SurveySync> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveySync> list = new List<SurveySync>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveySync
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString(),
						SYNC_STATE = Convert.ToInt32(dataReader["SYNC_STATE"]),
						SYNC_DATE = new DateTime?(Convert.ToDateTime(dataReader["SYNC_DATE"].ToString())),
						SYNC_NOTE = dataReader["SYNC_NOTE"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveySync> GetList()
		{
			string string_ = "SELECT * FROM SurveySync ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveySync surveySync_0)
		{
			string string_ = string.Format("INSERT INTO SurveySync(SURVEY_ID,SURVEY_GUID,SYNC_STATE,SYNC_DATE,SYNC_NOTE) VALUES('{0}','{1}',{2},'{3}','{4}')", new object[]
			{
				surveySync_0.SURVEY_ID,
				surveySync_0.SURVEY_GUID,
				surveySync_0.SYNC_STATE,
				surveySync_0.SYNC_DATE,
				surveySync_0.SYNC_NOTE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveySync surveySync_0)
		{
			string string_ = string.Format("UPDATE SurveySync SET SURVEY_ID = '{1}',SURVEY_GUID = '{2}',SYNC_STATE = {3},SYNC_DATE = '{4}',SYNC_NOTE = '{5}' WHERE ID = {0}", new object[]
			{
				surveySync_0.ID,
				surveySync_0.SURVEY_ID,
				surveySync_0.SURVEY_GUID,
				surveySync_0.SYNC_STATE,
				surveySync_0.SYNC_DATE,
				surveySync_0.SYNC_NOTE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveySync surveySync_0)
		{
			string string_ = string.Format("DELETE FROM SurveySync WHERE ID ={0}", surveySync_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveySync";
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
				array[2] = "GUID 全球唯一码";
				array[3] = "同步状态";
				array[4] = "同步时间";
				array[5] = "同步情况说明";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "SURVEY_GUID";
				array[3] = "SYNC_STATE";
				array[4] = "SYNC_DATE";
				array[5] = "SYNC_NOTE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveySync> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveySync surveySync in list_0)
			{
				array[num, 0] = surveySync.ID.ToString();
				array[num, 1] = surveySync.SURVEY_ID;
				array[num, 2] = surveySync.SURVEY_GUID;
				array[num, 3] = surveySync.SYNC_STATE.ToString();
				array[num, 4] = surveySync.SYNC_DATE.ToString();
				array[num, 5] = surveySync.SYNC_NOTE;
				num++;
			}
			return array;
		}

		public bool CheckObjectExist(string string_0, string string_1)
		{
			string string_2 = string.Format("SELECT COUNT(*) FROM SurveySync WHERE SURVEY_ID ='{0}' and SURVEY_GUID='{1}'", string_0, string_1);
			int num = this.dbprovider_0.ExecuteScalarInt(string_2);
			return num > 0;
		}

		public SurveySync GetBySuveyID_GUID(string string_0, string string_1)
		{
			string string_2 = string.Format("SELECT * FROM SurveySync WHERE SURVEY_ID ='{0}' and SURVEY_GUID='{1}'", string_0, string_1);
			return this.GetBySql(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
