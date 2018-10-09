using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyQuotaDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyQuota WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyQuota GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyQuota WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyQuota GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyQuota surveyQuota = new SurveyQuota();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyQuota.ID = Convert.ToInt32(dataReader["ID"]);
					surveyQuota.PROJECT_ID = dataReader["PROJECT_ID"].ToString();
					surveyQuota.CLIENT_ID = dataReader["CLIENT_ID"].ToString();
					surveyQuota.PAGE_ID = dataReader["PAGE_ID"].ToString();
					surveyQuota.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					surveyQuota.QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString();
					surveyQuota.INNER_INDEX = Convert.ToInt32(dataReader["INNER_INDEX"]);
					surveyQuota.CODE = dataReader["CODE"].ToString();
					surveyQuota.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					surveyQuota.SAMPLE_OVER = Convert.ToInt32(dataReader["SAMPLE_OVER"]);
					surveyQuota.SAMPLE_TARGET = Convert.ToInt32(dataReader["SAMPLE_TARGET"]);
					surveyQuota.SAMPLE_BACKUP = Convert.ToInt32(dataReader["SAMPLE_BACKUP"]);
					surveyQuota.SAMPLE_TOTAL = Convert.ToInt32(dataReader["SAMPLE_TOTAL"]);
					surveyQuota.SAMPLE_FINISH = Convert.ToInt32(dataReader["SAMPLE_FINISH"]);
					surveyQuota.SAMPLE_RUNNING = Convert.ToInt32(dataReader["SAMPLE_RUNNING"]);
					surveyQuota.SAMPLE_REAL = Convert.ToInt32(dataReader["SAMPLE_REAL"]);
					surveyQuota.IS_FULL = dataReader["IS_FULL"].ToString();
					surveyQuota.SAMPLE_BALANCE = Convert.ToInt32(dataReader["SAMPLE_BALANCE"]);
					surveyQuota.MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader["MODIFY_DATE"].ToString()));
				}
			}
			return surveyQuota;
		}

		public List<SurveyQuota> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyQuota> list = new List<SurveyQuota>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyQuota
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						PROJECT_ID = dataReader["PROJECT_ID"].ToString(),
						CLIENT_ID = dataReader["CLIENT_ID"].ToString(),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						QUESTION_TITLE = dataReader["QUESTION_TITLE"].ToString(),
						INNER_INDEX = Convert.ToInt32(dataReader["INNER_INDEX"]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						SAMPLE_OVER = Convert.ToInt32(dataReader["SAMPLE_OVER"]),
						SAMPLE_TARGET = Convert.ToInt32(dataReader["SAMPLE_TARGET"]),
						SAMPLE_BACKUP = Convert.ToInt32(dataReader["SAMPLE_BACKUP"]),
						SAMPLE_TOTAL = Convert.ToInt32(dataReader["SAMPLE_TOTAL"]),
						SAMPLE_FINISH = Convert.ToInt32(dataReader["SAMPLE_FINISH"]),
						SAMPLE_RUNNING = Convert.ToInt32(dataReader["SAMPLE_RUNNING"]),
						SAMPLE_REAL = Convert.ToInt32(dataReader["SAMPLE_REAL"]),
						IS_FULL = dataReader["IS_FULL"].ToString(),
						SAMPLE_BALANCE = Convert.ToInt32(dataReader["SAMPLE_BALANCE"]),
						MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader["MODIFY_DATE"].ToString()))
					});
				}
			}
			return list;
		}

		public List<SurveyQuota> GetList()
		{
			string string_ = "SELECT * FROM SurveyQuota ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyQuota surveyQuota_0)
		{
			string string_ = string.Format("INSERT INTO SurveyQuota(PROJECT_ID,CLIENT_ID,PAGE_ID,QUESTION_NAME,QUESTION_TITLE,INNER_INDEX,CODE,CODE_TEXT,SAMPLE_OVER,SAMPLE_TARGET,SAMPLE_BACKUP,SAMPLE_TOTAL,SAMPLE_FINISH,SAMPLE_RUNNING,SAMPLE_REAL,IS_FULL,SAMPLE_BALANCE,MODIFY_DATE) VALUES('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8},{9},{10},{11},{12},{13},{14},'{15}',{16},'{17}')", new object[]
			{
				surveyQuota_0.PROJECT_ID,
				surveyQuota_0.CLIENT_ID,
				surveyQuota_0.PAGE_ID,
				surveyQuota_0.QUESTION_NAME,
				surveyQuota_0.QUESTION_TITLE,
				surveyQuota_0.INNER_INDEX,
				surveyQuota_0.CODE,
				surveyQuota_0.CODE_TEXT,
				surveyQuota_0.SAMPLE_OVER,
				surveyQuota_0.SAMPLE_TARGET,
				surveyQuota_0.SAMPLE_BACKUP,
				surveyQuota_0.SAMPLE_TOTAL,
				surveyQuota_0.SAMPLE_FINISH,
				surveyQuota_0.SAMPLE_RUNNING,
				surveyQuota_0.SAMPLE_REAL,
				surveyQuota_0.IS_FULL,
				surveyQuota_0.SAMPLE_BALANCE,
				surveyQuota_0.MODIFY_DATE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyQuota surveyQuota_0)
		{
			string string_ = string.Format("UPDATE SurveyQuota SET PROJECT_ID = '{1}',CLIENT_ID = '{2}',PAGE_ID = '{3}',QUESTION_NAME = '{4}',QUESTION_TITLE = '{5}',INNER_INDEX = {6},CODE = '{7}',CODE_TEXT = '{8}',SAMPLE_OVER = {9},SAMPLE_TARGET = {10},SAMPLE_BACKUP = {11},SAMPLE_TOTAL = {12},SAMPLE_FINISH = {13},SAMPLE_RUNNING = {14},SAMPLE_REAL = {15},IS_FULL = '{16}',SAMPLE_BALANCE = {17},MODIFY_DATE = '{18}' WHERE ID = {0}", new object[]
			{
				surveyQuota_0.ID,
				surveyQuota_0.PROJECT_ID,
				surveyQuota_0.CLIENT_ID,
				surveyQuota_0.PAGE_ID,
				surveyQuota_0.QUESTION_NAME,
				surveyQuota_0.QUESTION_TITLE,
				surveyQuota_0.INNER_INDEX,
				surveyQuota_0.CODE,
				surveyQuota_0.CODE_TEXT,
				surveyQuota_0.SAMPLE_OVER,
				surveyQuota_0.SAMPLE_TARGET,
				surveyQuota_0.SAMPLE_BACKUP,
				surveyQuota_0.SAMPLE_TOTAL,
				surveyQuota_0.SAMPLE_FINISH,
				surveyQuota_0.SAMPLE_RUNNING,
				surveyQuota_0.SAMPLE_REAL,
				surveyQuota_0.IS_FULL,
				surveyQuota_0.SAMPLE_BALANCE,
				surveyQuota_0.MODIFY_DATE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyQuota surveyQuota_0)
		{
			string string_ = string.Format("DELETE FROM SurveyQuota WHERE ID ={0}", surveyQuota_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyQuota";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 19;
			string[] array = new string[19];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "项目 ID";
				array[2] = "客户 ID";
				array[3] = "页 ID";
				array[4] = "配额题号(问题编号)";
				array[5] = "配额标题";
				array[6] = "配额顺序";
				array[7] = "配额指标编码";
				array[8] = "配额指标编码文本";
				array[9] = "配额满的拦截方式";
				array[10] = "目标配额样本数";
				array[11] = "配额备份样本数";
				array[12] = "配额总数";
				array[13] = "实际已经完成样本数";
				array[14] = "正在访问样本数";
				array[15] = "实际有效总数";
				array[16] = "是否已满";
				array[17] = "未满差额数";
				array[18] = "最后修改日期时间";
			}
			else
			{
				array[0] = "ID";
				array[1] = "PROJECT_ID";
				array[2] = "CLIENT_ID";
				array[3] = "PAGE_ID";
				array[4] = "QUESTION_NAME";
				array[5] = "QUESTION_TITLE";
				array[6] = "INNER_INDEX";
				array[7] = "CODE";
				array[8] = "CODE_TEXT";
				array[9] = "SAMPLE_OVER";
				array[10] = "SAMPLE_TARGET";
				array[11] = "SAMPLE_BACKUP";
				array[12] = "SAMPLE_TOTAL";
				array[13] = "SAMPLE_FINISH";
				array[14] = "SAMPLE_RUNNING";
				array[15] = "SAMPLE_REAL";
				array[16] = "IS_FULL";
				array[17] = "SAMPLE_BALANCE";
				array[18] = "MODIFY_DATE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyQuota> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyQuota surveyQuota in list_0)
			{
				array[num, 0] = surveyQuota.ID.ToString();
				array[num, 1] = surveyQuota.PROJECT_ID;
				array[num, 2] = surveyQuota.CLIENT_ID;
				array[num, 3] = surveyQuota.PAGE_ID;
				array[num, 4] = surveyQuota.QUESTION_NAME;
				array[num, 5] = surveyQuota.QUESTION_TITLE;
				array[num, 6] = surveyQuota.INNER_INDEX.ToString();
				array[num, 7] = surveyQuota.CODE;
				array[num, 8] = surveyQuota.CODE_TEXT;
				array[num, 9] = surveyQuota.SAMPLE_OVER.ToString();
				array[num, 10] = surveyQuota.SAMPLE_TARGET.ToString();
				array[num, 11] = surveyQuota.SAMPLE_BACKUP.ToString();
				array[num, 12] = surveyQuota.SAMPLE_TOTAL.ToString();
				array[num, 13] = surveyQuota.SAMPLE_FINISH.ToString();
				array[num, 14] = surveyQuota.SAMPLE_RUNNING.ToString();
				array[num, 15] = surveyQuota.SAMPLE_REAL.ToString();
				array[num, 16] = surveyQuota.IS_FULL;
				array[num, 17] = surveyQuota.SAMPLE_BALANCE.ToString();
				array[num, 18] = surveyQuota.MODIFY_DATE.ToString();
				num++;
			}
			return array;
		}

		public void AddRead(SurveyQuota surveyQuota_0)
		{
			string string_ = string.Format("INSERT INTO SurveyQuota(PROJECT_ID,CLIENT_ID,PAGE_ID,QUESTION_NAME,QUESTION_TITLE,INNER_INDEX,CODE,CODE_TEXT,SAMPLE_OVER,SAMPLE_TARGET,SAMPLE_BACKUP,SAMPLE_TOTAL,SAMPLE_FINISH,SAMPLE_RUNNING,SAMPLE_REAL,IS_FULL,SAMPLE_BALANCE,MODIFY_DATE) VALUES('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8},{9},{10},{11},{12},{13},{14},'{15}',{16},'{17}')", new object[]
			{
				surveyQuota_0.PROJECT_ID,
				surveyQuota_0.CLIENT_ID,
				surveyQuota_0.PAGE_ID,
				surveyQuota_0.QUESTION_NAME,
				surveyQuota_0.QUESTION_TITLE,
				surveyQuota_0.INNER_INDEX,
				surveyQuota_0.CODE,
				surveyQuota_0.CODE_TEXT,
				surveyQuota_0.SAMPLE_OVER,
				surveyQuota_0.SAMPLE_TARGET,
				surveyQuota_0.SAMPLE_BACKUP,
				surveyQuota_0.SAMPLE_TOTAL,
				surveyQuota_0.SAMPLE_FINISH,
				surveyQuota_0.SAMPLE_RUNNING,
				surveyQuota_0.SAMPLE_REAL,
				surveyQuota_0.IS_FULL,
				surveyQuota_0.SAMPLE_BALANCE,
				surveyQuota_0.MODIFY_DATE
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void AddOne(SurveyQuota surveyQuota_0)
		{
			if (this.Exists(surveyQuota_0.ID))
			{
				this.Update(surveyQuota_0);
			}
			else
			{
				this.Add(surveyQuota_0);
			}
		}

		public bool ExistsByQName(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyQuota where QUESTION_NAME ='{0}' and CODE='{1}'", string_0, string_1);
			int num = this.dbprovider_0.ExecuteScalarInt(string_2);
			return num > 0;
		}

		public bool ExistsMatchQuota(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format("select * from SurveyQuota where PAGE_ID = '{0}' and QUESTION_NAME ='{1}' and CODE='{2}'", string_0, string_1, string_2);
			int num = this.dbprovider_0.ExecuteScalarInt(string_3);
			return num > 0;
		}

		public void UpdateStatus(string string_0, string string_1, int int_0, int int_1)
		{
			string string_2 = string.Format("update SurveyQuota set SAMPLE_FINISH = SAMPLE_FINISH + {2}, SAMPLE_RUNNING = SAMPLE_RUNNING - {3} where SURVEY_ID ='{0}' and SURVEY_GUID='{1}'", new object[]
			{
				string_0,
				string_1,
				int_0.ToString(),
				int_1.ToString()
			});
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);

		private DBProvider dbprovider_1 = new DBProvider(1);
	}
}
