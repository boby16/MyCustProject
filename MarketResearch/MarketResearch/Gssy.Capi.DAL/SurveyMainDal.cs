using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyMainDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyMain WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyMain GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyMain WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyMain GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyMain surveyMain = new SurveyMain();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyMain.ID = Convert.ToInt32(dataReader["ID"]);
					surveyMain.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyMain.VERSION_ID = dataReader["VERSION_ID"].ToString();
					surveyMain.USER_ID = dataReader["USER_ID"].ToString();
					surveyMain.CITY_ID = dataReader["CITY_ID"].ToString();
					surveyMain.START_TIME = new DateTime?(Convert.ToDateTime(dataReader["START_TIME"].ToString()));
					surveyMain.END_TIME = new DateTime?(Convert.ToDateTime(dataReader["END_TIME"].ToString()));
					surveyMain.LAST_START_TIME = new DateTime?(Convert.ToDateTime(dataReader["LAST_START_TIME"].ToString()));
					surveyMain.LAST_END_TIME = new DateTime?(Convert.ToDateTime(dataReader["LAST_END_TIME"].ToString()));
					surveyMain.CUR_PAGE_ID = dataReader["CUR_PAGE_ID"].ToString();
					surveyMain.CIRCLE_A_CURRENT = Convert.ToInt32(dataReader["CIRCLE_A_CURRENT"]);
					surveyMain.CIRCLE_A_COUNT = Convert.ToInt32(dataReader["CIRCLE_A_COUNT"]);
					surveyMain.CIRCLE_B_CURRENT = Convert.ToInt32(dataReader["CIRCLE_B_CURRENT"]);
					surveyMain.CIRCLE_B_COUNT = Convert.ToInt32(dataReader["CIRCLE_B_COUNT"]);
					surveyMain.IS_FINISH = Convert.ToInt32(dataReader["IS_FINISH"]);
					surveyMain.SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]);
					surveyMain.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
					surveyMain.CLIENT_ID = dataReader["CLIENT_ID"].ToString();
					surveyMain.PROJECT_ID = dataReader["PROJECT_ID"].ToString();
					surveyMain.ROADMAP_VERSION_ID = Convert.ToInt32(dataReader["ROADMAP_VERSION_ID"]);
				}
			}
			return surveyMain;
		}

		public List<SurveyMain> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyMain> list = new List<SurveyMain>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyMain
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						VERSION_ID = dataReader["VERSION_ID"].ToString(),
						USER_ID = dataReader["USER_ID"].ToString(),
						CITY_ID = dataReader["CITY_ID"].ToString(),
						START_TIME = new DateTime?(Convert.ToDateTime(dataReader["START_TIME"].ToString())),
						END_TIME = new DateTime?(Convert.ToDateTime(dataReader["END_TIME"].ToString())),
						LAST_START_TIME = new DateTime?(Convert.ToDateTime(dataReader["LAST_START_TIME"].ToString())),
						LAST_END_TIME = new DateTime?(Convert.ToDateTime(dataReader["LAST_END_TIME"].ToString())),
						CUR_PAGE_ID = dataReader["CUR_PAGE_ID"].ToString(),
						CIRCLE_A_CURRENT = Convert.ToInt32(dataReader["CIRCLE_A_CURRENT"]),
						CIRCLE_A_COUNT = Convert.ToInt32(dataReader["CIRCLE_A_COUNT"]),
						CIRCLE_B_CURRENT = Convert.ToInt32(dataReader["CIRCLE_B_CURRENT"]),
						CIRCLE_B_COUNT = Convert.ToInt32(dataReader["CIRCLE_B_COUNT"]),
						IS_FINISH = Convert.ToInt32(dataReader["IS_FINISH"]),
						SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString(),
						CLIENT_ID = dataReader["CLIENT_ID"].ToString(),
						PROJECT_ID = dataReader["PROJECT_ID"].ToString(),
						ROADMAP_VERSION_ID = Convert.ToInt32(dataReader["ROADMAP_VERSION_ID"])
					});
				}
			}
			return list;
		}

		public List<SurveyMain> GetList()
		{
			string string_ = "SELECT * FROM SurveyMain ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyMain surveyMain_0)
		{
			string string_ = string.Format("INSERT INTO SurveyMain(SURVEY_ID,VERSION_ID,USER_ID,CITY_ID,START_TIME,END_TIME,LAST_START_TIME,LAST_END_TIME,CUR_PAGE_ID,CIRCLE_A_CURRENT,CIRCLE_A_COUNT,CIRCLE_B_CURRENT,CIRCLE_B_COUNT,IS_FINISH,SEQUENCE_ID,SURVEY_GUID,CLIENT_ID,PROJECT_ID,ROADMAP_VERSION_ID) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},{11},{12},{13},{14},'{15}','{16}','{17}',{18})", new object[]
			{
				surveyMain_0.SURVEY_ID,
				surveyMain_0.VERSION_ID,
				surveyMain_0.USER_ID,
				surveyMain_0.CITY_ID,
				surveyMain_0.START_TIME,
				surveyMain_0.END_TIME,
				surveyMain_0.LAST_START_TIME,
				surveyMain_0.LAST_END_TIME,
				surveyMain_0.CUR_PAGE_ID,
				surveyMain_0.CIRCLE_A_CURRENT,
				surveyMain_0.CIRCLE_A_COUNT,
				surveyMain_0.CIRCLE_B_CURRENT,
				surveyMain_0.CIRCLE_B_COUNT,
				surveyMain_0.IS_FINISH,
				surveyMain_0.SEQUENCE_ID,
				surveyMain_0.SURVEY_GUID,
				surveyMain_0.CLIENT_ID,
				surveyMain_0.PROJECT_ID,
				surveyMain_0.ROADMAP_VERSION_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyMain surveyMain_0)
		{
			string string_ = string.Format("DELETE FROM SurveyMain WHERE ID ={0}", surveyMain_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyMain";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 20;
			string[] array = new string[20];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "问卷编号";
				array[2] = "版本编号";
				array[3] = "访问员编号";
				array[4] = "城市编号";
				array[5] = "第一次问卷开始时间";
				array[6] = "第一次问卷结束时间";
				array[7] = "最后一次问卷开始时间";
				array[8] = "最后一次问卷结束时间";
				array[9] = "问卷当前页";
				array[10] = "A 层当前的循环数";
				array[11] = "A 层循环总数";
				array[12] = "B 层当前的循环数";
				array[13] = "B 层循环总数";
				array[14] = "问卷是否完成";
				array[15] = "问卷序列号";
				array[16] = "GUID 全球唯一码";
				array[17] = "客户 ID";
				array[18] = "项目 ID";
				array[19] = "路由版本编号";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "VERSION_ID";
				array[3] = "USER_ID";
				array[4] = "CITY_ID";
				array[5] = "START_TIME";
				array[6] = "END_TIME";
				array[7] = "LAST_START_TIME";
				array[8] = "LAST_END_TIME";
				array[9] = "CUR_PAGE_ID";
				array[10] = "CIRCLE_A_CURRENT";
				array[11] = "CIRCLE_A_COUNT";
				array[12] = "CIRCLE_B_CURRENT";
				array[13] = "CIRCLE_B_COUNT";
				array[14] = "IS_FINISH";
				array[15] = "SEQUENCE_ID";
				array[16] = "SURVEY_GUID";
				array[17] = "CLIENT_ID";
				array[18] = "PROJECT_ID";
				array[19] = "ROADMAP_VERSION_ID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyMain> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyMain surveyMain in list_0)
			{
				array[num, 0] = surveyMain.ID.ToString();
				array[num, 1] = surveyMain.SURVEY_ID;
				array[num, 2] = surveyMain.VERSION_ID;
				array[num, 3] = surveyMain.USER_ID;
				array[num, 4] = surveyMain.CITY_ID;
				array[num, 5] = surveyMain.START_TIME.ToString();
				array[num, 6] = surveyMain.END_TIME.ToString();
				array[num, 7] = surveyMain.LAST_START_TIME.ToString();
				array[num, 8] = surveyMain.LAST_END_TIME.ToString();
				array[num, 9] = surveyMain.CUR_PAGE_ID;
				array[num, 10] = surveyMain.CIRCLE_A_CURRENT.ToString();
				array[num, 11] = surveyMain.CIRCLE_A_COUNT.ToString();
				array[num, 12] = surveyMain.CIRCLE_B_CURRENT.ToString();
				array[num, 13] = surveyMain.CIRCLE_B_COUNT.ToString();
				array[num, 14] = surveyMain.IS_FINISH.ToString();
				array[num, 15] = surveyMain.SEQUENCE_ID.ToString();
				array[num, 16] = surveyMain.SURVEY_GUID;
				array[num, 17] = surveyMain.CLIENT_ID;
				array[num, 18] = surveyMain.PROJECT_ID;
				array[num, 19] = surveyMain.ROADMAP_VERSION_ID.ToString();
				num++;
			}
			return array;
		}

		public void Add(string string_0, string string_1, string string_2, string string_3, string string_4)
		{
			string text = Guid.NewGuid().ToString();
			string text2 = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
			string string_5 = string.Format("INSERT INTO SurveyMain(SURVEY_ID,VERSION_ID,USER_ID,CITY_ID,START_TIME,END_TIME,LAST_START_TIME,LAST_END_TIME,CUR_PAGE_ID,CIRCLE_A_CURRENT,CIRCLE_A_COUNT,CIRCLE_B_CURRENT,CIRCLE_B_COUNT,IS_FINISH,SEQUENCE_ID,SURVEY_GUID,CLIENT_ID,PROJECT_ID) VALUES('{0}','{1}','','{2}','{3}','{4}','{5}','{6}','',0,0,0,0,0,0,'{7}','{8}','{9}')", new object[]
			{
				string_0,
				string_1,
				string_2,
				text2,
				text2,
				text2,
				text2,
				text,
				string_4,
				string_3
			});
			this.dbprovider_0.ExecuteNonQuery(string_5);
		}

		public void Update(SurveyMain surveyMain_0)
		{
			string string_ = string.Format("UPDATE SurveyMain SET USER_ID = '{1}',END_TIME = '{9}',CUR_PAGE_ID = '{2}',CIRCLE_A_CURRENT = {3},CIRCLE_A_COUNT = {4},CIRCLE_B_CURRENT = {5},CIRCLE_B_COUNT = {6},IS_FINISH = {7},SEQUENCE_ID = {8},ROADMAP_VERSION_ID = {10} WHERE ID = {0}", new object[]
			{
				surveyMain_0.ID,
				surveyMain_0.USER_ID,
				surveyMain_0.CUR_PAGE_ID,
				surveyMain_0.CIRCLE_A_CURRENT,
				surveyMain_0.CIRCLE_A_COUNT,
				surveyMain_0.CIRCLE_B_CURRENT,
				surveyMain_0.CIRCLE_B_COUNT,
				surveyMain_0.IS_FINISH,
				surveyMain_0.SEQUENCE_ID,
				DateTime.Now,
				surveyMain_0.ROADMAP_VERSION_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void UpdateUserId(string string_0, string string_1)
		{
			string string_2 = string.Format("UPDATE SurveyMain SET USER_ID='{1}' WHERE SURVEY_ID='{0}'", string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public void UpdateFinishStatus(string string_0, int int_0)
		{
			string string_ = string.Format("UPDATE SurveyMain SET IS_FINISH={1} WHERE SURVEY_ID='{0}'", string_0, int_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void UpdateCurPageId(string string_0, string string_1)
		{
			string string_2 = string.Format("UPDATE SurveyMain SET CUR_PAGE_ID = '{1}' WHERE SURVEY_ID='{0}'", string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public bool Exists(string string_0)
		{
			string string_ = string.Format("select COUNT(*) from SurveyMain where SURVEY_ID='{0}'", string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyMain GetBySurveyId(string string_0)
		{
			string string_ = string.Format("select * from SurveyMain where Survey_Id='{0}'", string_0);
			return this.GetBySql(string_);
		}

		public SurveyMain GetBySurveyGUID(string string_0)
		{
			string string_ = string.Format("select * from SurveyMain where SURVEY_GUID='{0}'", string_0);
			return this.GetBySql(string_);
		}

		public string GetMaxSurveyID()
		{
			string result = "";
			string string_ = "select COUNT(*) from SurveyMain";
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			if (num > 0)
			{
				string_ = "select Max(ID) from SurveyMain";
				string value = this.dbprovider_0.ExecuteScalarString(string_);
				string_ = string.Format("select SURVEY_ID from SurveyMain where ID={0}", Convert.ToInt32(value));
				result = this.dbprovider_0.ExecuteScalarString(string_);
			}
			return result;
		}

		public string GetMaxSurveyIDByCity(string string_0)
		{
			string result = "";
			string string_ = string.Format("select COUNT(*) from SurveyMain where SURVEY_ID like '{0}%'", string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			if (num > 0)
			{
				string_ = string.Format("select Max(SURVEY_ID) from SurveyMain where SURVEY_ID like '{0}%'", string_0);
				result = this.dbprovider_0.ExecuteScalarString(string_);
			}
			return result;
		}

		public List<SurveyMain> GetSurveyMain(int int_0)
		{
			List<SurveyMain> result = new List<SurveyMain>();
			string string_ = string.Format("select count(*) from SurveyMain where is_finish={0}", int_0);
			string a = this.dbprovider_0.ExecuteScalarString(string_);
			if (a != "0")
			{
				string_ = string.Format("select * from SurveyMain where is_finish={0} order by survey_id", int_0);
				result = this.GetListBySql(string_);
			}
			return result;
		}

		public bool ExistsByGUID(string string_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyMain WHERE SURVEY_GUID ='{0}'", string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public bool ExistsBySurveyID(string string_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyMain WHERE SURVEY_ID ='{0}'", string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public string GetAutoSurveyId(string string_0, int int_0)
		{
			string string_ = string.Format("select max(SURVEY_ID) from SURVEYMAIN where SURVEY_ID like '{0}%' ", string_0);
			string text = this.dbprovider_0.ExecuteScalarString(string_);
			if (text == "")
			{
				text = string_0 + "0011";
				text = text.Substring(0, int_0);
			}
			else
			{
				text = (Convert.ToInt32(text) + 1).ToString();
			}
			return text;
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format("delete from SurveyMain where SURVEY_ID='{0}'", string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
