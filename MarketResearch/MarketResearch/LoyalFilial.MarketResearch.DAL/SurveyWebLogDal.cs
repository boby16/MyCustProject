using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyWebLogDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyWebLog WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyWebLog GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyWebLog WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyWebLog GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyWebLog surveyWebLog = new SurveyWebLog();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyWebLog.ID = Convert.ToInt32(dataReader["ID"]);
					surveyWebLog.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyWebLog.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
					surveyWebLog.URI_FULL = dataReader["URI_FULL"].ToString();
					surveyWebLog.URI_DOMAIN = dataReader["URI_DOMAIN"].ToString();
					surveyWebLog.URI_DOMAIN_TWO = dataReader["URI_DOMAIN_TWO"].ToString();
					surveyWebLog.BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader["BEGIN_TIME"].ToString()));
					surveyWebLog.END_TIME = new DateTime?(Convert.ToDateTime(dataReader["END_TIME"].ToString()));
					surveyWebLog.STAY_TIME = Convert.ToInt32(dataReader["STAY_TIME"]);
				}
			}
			return surveyWebLog;
		}

		public List<SurveyWebLog> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyWebLog> list = new List<SurveyWebLog>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyWebLog
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString(),
						URI_FULL = dataReader["URI_FULL"].ToString(),
						URI_DOMAIN = dataReader["URI_DOMAIN"].ToString(),
						URI_DOMAIN_TWO = dataReader["URI_DOMAIN_TWO"].ToString(),
						BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader["BEGIN_TIME"].ToString())),
						END_TIME = new DateTime?(Convert.ToDateTime(dataReader["END_TIME"].ToString())),
						STAY_TIME = Convert.ToInt32(dataReader["STAY_TIME"])
					});
				}
			}
			return list;
		}

		public List<SurveyWebLog> GetList()
		{
			string string_ = "SELECT * FROM SurveyWebLog ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyWebLog surveyWebLog_0)
		{
			string string_ = string.Format("INSERT INTO SurveyWebLog(SURVEY_ID,SURVEY_GUID,URI_FULL,URI_DOMAIN,URI_DOMAIN_TWO,BEGIN_TIME,END_TIME,STAY_TIME) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7})", new object[]
			{
				surveyWebLog_0.SURVEY_ID,
				surveyWebLog_0.SURVEY_GUID,
				surveyWebLog_0.URI_FULL,
				surveyWebLog_0.URI_DOMAIN,
				surveyWebLog_0.URI_DOMAIN_TWO,
				surveyWebLog_0.BEGIN_TIME,
				surveyWebLog_0.END_TIME,
				surveyWebLog_0.STAY_TIME
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyWebLog surveyWebLog_0)
		{
			string string_ = string.Format("UPDATE SurveyWebLog SET SURVEY_ID = '{1}',SURVEY_GUID = '{2}',URI_FULL = '{3}',URI_DOMAIN = '{4}',URI_DOMAIN_TWO = '{5}',BEGIN_TIME = '{6}',END_TIME = '{7}',STAY_TIME = {8} WHERE ID = {0}", new object[]
			{
				surveyWebLog_0.ID,
				surveyWebLog_0.SURVEY_ID,
				surveyWebLog_0.SURVEY_GUID,
				surveyWebLog_0.URI_FULL,
				surveyWebLog_0.URI_DOMAIN,
				surveyWebLog_0.URI_DOMAIN_TWO,
				surveyWebLog_0.BEGIN_TIME,
				surveyWebLog_0.END_TIME,
				surveyWebLog_0.STAY_TIME
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyWebLog surveyWebLog_0)
		{
			string string_ = string.Format("DELETE FROM SurveyWebLog WHERE ID ={0}", surveyWebLog_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyWebLog";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 9;
			string[] array = new string[9];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "问卷编号";
				array[2] = "GUID 全球唯一码";
				array[3] = "原始网页地址";
				array[4] = "一级域名";
				array[5] = "二级域名";
				array[6] = "开始时间";
				array[7] = "结束时间";
				array[8] = "停留时长 秒数";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "SURVEY_GUID";
				array[3] = "URI_FULL";
				array[4] = "URI_DOMAIN";
				array[5] = "URI_DOMAIN_TWO";
				array[6] = "BEGIN_TIME";
				array[7] = "END_TIME";
				array[8] = "STAY_TIME";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyWebLog> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyWebLog surveyWebLog in list_0)
			{
				array[num, 0] = surveyWebLog.ID.ToString();
				array[num, 1] = surveyWebLog.SURVEY_ID;
				array[num, 2] = surveyWebLog.SURVEY_GUID;
				array[num, 3] = surveyWebLog.URI_FULL;
				array[num, 4] = surveyWebLog.URI_DOMAIN;
				array[num, 5] = surveyWebLog.URI_DOMAIN_TWO;
				array[num, 6] = surveyWebLog.BEGIN_TIME.ToString();
				array[num, 7] = surveyWebLog.END_TIME.ToString();
				array[num, 8] = surveyWebLog.STAY_TIME.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
