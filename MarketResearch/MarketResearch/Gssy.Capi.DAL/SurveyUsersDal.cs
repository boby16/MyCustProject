using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyUsersDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyUsers WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyUsers GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyUsers WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyUsers GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyUsers surveyUsers = new SurveyUsers();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyUsers.ID = Convert.ToInt32(dataReader["ID"]);
					surveyUsers.USER_ID = dataReader["USER_ID"].ToString();
					surveyUsers.USER_NAME = dataReader["USER_NAME"].ToString();
					surveyUsers.IS_ENABLE = Convert.ToInt32(dataReader["IS_ENABLE"]);
					surveyUsers.PSW = dataReader["PSW"].ToString();
					surveyUsers.ROLE_ID = dataReader["ROLE_ID"].ToString();
				}
			}
			return surveyUsers;
		}

		public List<SurveyUsers> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyUsers> list = new List<SurveyUsers>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyUsers
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						USER_ID = dataReader["USER_ID"].ToString(),
						USER_NAME = dataReader["USER_NAME"].ToString(),
						IS_ENABLE = Convert.ToInt32(dataReader["IS_ENABLE"]),
						PSW = dataReader["PSW"].ToString(),
						ROLE_ID = dataReader["ROLE_ID"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyUsers> GetList()
		{
			string string_ = "SELECT * FROM SurveyUsers ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyUsers surveyUsers_0)
		{
			string string_ = string.Format("INSERT INTO SurveyUsers(USER_ID,USER_NAME,IS_ENABLE,PSW,ROLE_ID) VALUES('{0}','{1}',{2},'{3}','{4}')", new object[]
			{
				surveyUsers_0.USER_ID,
				surveyUsers_0.USER_NAME,
				surveyUsers_0.IS_ENABLE,
				surveyUsers_0.PSW,
				surveyUsers_0.ROLE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyUsers surveyUsers_0)
		{
			string string_ = string.Format("UPDATE SurveyUsers SET USER_ID = '{1}',USER_NAME = '{2}',IS_ENABLE = {3},PSW = '{4}',ROLE_ID = '{5}' WHERE ID = {0}", new object[]
			{
				surveyUsers_0.ID,
				surveyUsers_0.USER_ID,
				surveyUsers_0.USER_NAME,
				surveyUsers_0.IS_ENABLE,
				surveyUsers_0.PSW,
				surveyUsers_0.ROLE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyUsers surveyUsers_0)
		{
			string string_ = string.Format("DELETE FROM SurveyUsers WHERE ID ={0}", surveyUsers_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyUsers";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 6;
			string[] array = new string[6];
			if (bool_0)
			{
				array[0] = "主键 自动编号";
				array[1] = "用户/访问员编码";
				array[2] = "用户名";
				array[3] = "激活";
				array[4] = "密码";
				array[5] = "角色ID";
			}
			else
			{
				array[0] = "ID";
				array[1] = "USER_ID";
				array[2] = "USER_NAME";
				array[3] = "IS_ENABLE";
				array[4] = "PSW";
				array[5] = "ROLE_ID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyUsers> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyUsers surveyUsers in list_0)
			{
				array[num, 0] = surveyUsers.ID.ToString();
				array[num, 1] = surveyUsers.USER_ID;
				array[num, 2] = surveyUsers.USER_NAME;
				array[num, 3] = surveyUsers.IS_ENABLE.ToString();
				array[num, 4] = surveyUsers.PSW;
				array[num, 5] = surveyUsers.ROLE_ID;
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
