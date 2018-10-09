using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyLogDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyLog WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyLog GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyLog WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyLog GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyLog surveyLog = new SurveyLog();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyLog.ID = Convert.ToInt32(dataReader["ID"]);
					surveyLog.LOG_TYPE = dataReader["LOG_TYPE"].ToString();
					surveyLog.LOG_MESSAGE = dataReader["LOG_MESSAGE"].ToString();
					surveyLog.LOG_DATE = new DateTime?(Convert.ToDateTime(dataReader["LOG_DATE"].ToString()));
					surveyLog.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyLog.VERSION_ID = dataReader["VERSION_ID"].ToString();
				}
			}
			return surveyLog;
		}

		public List<SurveyLog> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyLog> list = new List<SurveyLog>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyLog
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						LOG_TYPE = dataReader["LOG_TYPE"].ToString(),
						LOG_MESSAGE = dataReader["LOG_MESSAGE"].ToString(),
						LOG_DATE = new DateTime?(Convert.ToDateTime(dataReader["LOG_DATE"].ToString())),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						VERSION_ID = dataReader["VERSION_ID"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyLog> GetList()
		{
			string string_ = "SELECT * FROM SurveyLog ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyLog surveyLog_0)
		{
			string string_ = string.Format("INSERT INTO SurveyLog(LOG_TYPE,LOG_MESSAGE,LOG_DATE,SURVEY_ID,VERSION_ID) VALUES('{0}','{1}','{2}','{3}','{4}')", new object[]
			{
				surveyLog_0.LOG_TYPE,
				surveyLog_0.LOG_MESSAGE,
				surveyLog_0.LOG_DATE,
				surveyLog_0.SURVEY_ID,
				surveyLog_0.VERSION_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyLog surveyLog_0)
		{
			string string_ = string.Format("UPDATE SurveyLog SET LOG_TYPE = '{1}',LOG_MESSAGE = '{2}',LOG_DATE = '{3}',SURVEY_ID = '{4}',VERSION_ID = '{5}' WHERE ID = {0}", new object[]
			{
				surveyLog_0.ID,
				surveyLog_0.LOG_TYPE,
				surveyLog_0.LOG_MESSAGE,
				surveyLog_0.LOG_DATE,
				surveyLog_0.SURVEY_ID,
				surveyLog_0.VERSION_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyLog surveyLog_0)
		{
			string string_ = string.Format("DELETE FROM SurveyLog WHERE ID ={0}", surveyLog_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyLog";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 6;
			string[] array = new string[6];
			if (bool_0)
			{
				array[0] = "主键 自动编号";
				array[1] = "日志类型";
				array[2] = "日志信息";
				array[3] = "日志日期";
				array[4] = "问卷编号";
				array[5] = "版本编号";
			}
			else
			{
				array[0] = "ID";
				array[1] = "LOG_TYPE";
				array[2] = "LOG_MESSAGE";
				array[3] = "LOG_DATE";
				array[4] = "SURVEY_ID";
				array[5] = "VERSION_ID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyLog> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyLog surveyLog in list_0)
			{
				array[num, 0] = surveyLog.ID.ToString();
				array[num, 1] = surveyLog.LOG_TYPE;
				array[num, 2] = surveyLog.LOG_MESSAGE;
				array[num, 3] = surveyLog.LOG_DATE.ToString();
				array[num, 4] = surveyLog.SURVEY_ID;
				array[num, 5] = surveyLog.VERSION_ID;
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
