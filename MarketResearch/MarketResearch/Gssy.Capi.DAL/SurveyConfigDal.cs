using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyConfigDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyConfig WHERE ID ={0}", int_0);
			int num = this.dbprovider_1.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyConfig GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyConfig WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyConfig GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_1.ExecuteReader(string_0);
			SurveyConfig surveyConfig = new SurveyConfig();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyConfig.ID = Convert.ToInt32(dataReader["ID"]);
					surveyConfig.CODE = dataReader["CODE"].ToString();
					surveyConfig.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					surveyConfig.CODE_NOTE = dataReader["CODE_NOTE"].ToString();
					surveyConfig.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
				}
			}
			return surveyConfig;
		}

		public List<SurveyConfig> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_1.ExecuteReader(string_0);
			List<SurveyConfig> list = new List<SurveyConfig>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyConfig
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						CODE_NOTE = dataReader["CODE_NOTE"].ToString(),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyConfig> GetList()
		{
			string string_ = "SELECT * FROM SurveyConfig ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format("INSERT INTO SurveyConfig(CODE,CODE_TEXT,CODE_NOTE,PARENT_CODE) VALUES('{0}','{1}','{2}','{3}')", new object[]
			{
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void Update(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format("UPDATE SurveyConfig SET CODE = '{1}',CODE_TEXT = '{2}',CODE_NOTE = '{3}',PARENT_CODE = '{4}' WHERE ID = {0}", new object[]
			{
				surveyConfig_0.ID,
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format("DELETE FROM SurveyConfig WHERE ID ={0}", surveyConfig_0.ID);
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyConfig";
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 5;
			string[] array = new string[5];
			if (bool_0)
			{
				array[0] = "主键 自动编号";
				array[1] = "配置/状态 编码";
				array[2] = "配置/状态 默认值";
				array[3] = "配置/状态 说明";
				array[4] = "上级编码";
			}
			else
			{
				array[0] = "ID";
				array[1] = "CODE";
				array[2] = "CODE_TEXT";
				array[3] = "CODE_NOTE";
				array[4] = "PARENT_CODE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyConfig> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyConfig surveyConfig in list_0)
			{
				array[num, 0] = surveyConfig.ID.ToString();
				array[num, 1] = surveyConfig.CODE;
				array[num, 2] = surveyConfig.CODE_TEXT;
				array[num, 3] = surveyConfig.CODE_NOTE;
				array[num, 4] = surveyConfig.PARENT_CODE;
				num++;
			}
			return array;
		}

		public bool Exists(string string_0)
		{
			string string_ = string.Format("select count(*) from SurveyConfig where CODE='{0}'", string_0);
			int num = this.dbprovider_1.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyConfig GetByCode(string string_0)
		{
			string string_ = string.Format("select * from SurveyConfig where CODE='{0}'", string_0);
			return this.GetBySql(string_);
		}

		public string GetByCodeText(string string_0)
		{
			string string_ = string.Format("select CODE_TEXT from SurveyConfig where CODE='{0}'", string_0);
			return this.dbprovider_1.ExecuteScalarString(string_);
		}

		public void UpdateByCode(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format("update SurveyConfig set CODE_TEXT = '{0}' where CODE='{1}'", surveyConfig_0.CODE_TEXT, surveyConfig_0.CODE);
			this.dbprovider_1.ExecuteNonQuery(string_);
		}

		public void SaveByKey(string string_0, string string_1)
		{
			SurveyConfig surveyConfig = new SurveyConfig();
			surveyConfig.CODE = string_0;
			surveyConfig.CODE_TEXT = string_1;
			if (this.Exists(string_0))
			{
				this.UpdateByCode(surveyConfig);
			}
			else
			{
				this.Add(surveyConfig);
			}
			if (this.ExistsRead(string_0))
			{
				this.UpdateByCodeRead(surveyConfig);
			}
			else
			{
				this.AddRead(surveyConfig);
			}
		}

		public void SaveDataFilePath(string string_0, string string_1)
		{
			SurveyConfig surveyConfig = new SurveyConfig();
			surveyConfig.CODE = string_0;
			surveyConfig.CODE_TEXT = string_1;
			if (this.Exists(string_0))
			{
				this.UpdateByCode(surveyConfig);
			}
			else
			{
				this.Add(surveyConfig);
			}
		}

		public List<SurveyConfig> GetQuotaConfig()
		{
			string string_ = "select * from SurveyConfig where PARENT_CODE='Quota' and code_text<>'' order by id";
			return this.GetListBySql(string_);
		}

		public bool ExistsRead(string string_0)
		{
			string string_ = string.Format("select count(*) from SurveyConfig where CODE='{0}'", string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public void UpdateByCodeRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format("update SurveyConfig set CODE_TEXT = '{0}' where CODE='{1}'", surveyConfig_0.CODE_TEXT, surveyConfig_0.CODE);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void AddRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format("INSERT INTO SurveyConfig(CODE,CODE_TEXT,CODE_NOTE,PARENT_CODE) VALUES('{0}','{1}','{2}','{3}')", new object[]
			{
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string GetByCodeTextRead(string string_0)
		{
			string string_ = string.Format("select CODE_TEXT from SurveyConfig where CODE='{0}'", string_0);
			return this.dbprovider_0.ExecuteScalarString(string_);
		}

		public List<SurveyConfig> GetSyncList()
		{
			string string_ = "select * from SurveyConfig where PARENT_CODE='SyncReadToWrite' and code_text='1' order by id";
			return this.GetReadListBySql(string_);
		}

		public void UpdateToRead(SurveyConfig surveyConfig_0)
		{
			string string_ = string.Format("UPDATE SurveyConfig SET CODE = '{1}',CODE_TEXT = '{2}',CODE_NOTE = '{3}',PARENT_CODE = '{4}' WHERE ID = {0}", new object[]
			{
				surveyConfig_0.ID,
				surveyConfig_0.CODE,
				surveyConfig_0.CODE_TEXT,
				surveyConfig_0.CODE_NOTE,
				surveyConfig_0.PARENT_CODE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public List<SurveyConfig> GetReadListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyConfig> list = new List<SurveyConfig>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyConfig
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						CODE_NOTE = dataReader["CODE_NOTE"].ToString(),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyConfig> GetListRead()
		{
			string string_ = "SELECT * FROM SurveyConfig ORDER BY ID";
			return this.GetListReadBySql(string_);
		}

		public List<SurveyConfig> GetListReadBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyConfig> list = new List<SurveyConfig>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyConfig
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						CODE_NOTE = dataReader["CODE_NOTE"].ToString(),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString()
					});
				}
			}
			return list;
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
