using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyAttachDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyAttach WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyAttach GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyAttach WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyAttach GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyAttach surveyAttach = new SurveyAttach();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyAttach.ID = Convert.ToInt32(dataReader["ID"]);
					surveyAttach.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyAttach.PAGE_ID = dataReader["PAGE_ID"].ToString();
					surveyAttach.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					surveyAttach.FILE_NAME = dataReader["FILE_NAME"].ToString();
					surveyAttach.FILE_TYPE = dataReader["FILE_TYPE"].ToString();
					surveyAttach.ORIGINAL_NAME = dataReader["ORIGINAL_NAME"].ToString();
					surveyAttach.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
				}
			}
			return surveyAttach;
		}

		public List<SurveyAttach> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyAttach> list = new List<SurveyAttach>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyAttach
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						FILE_NAME = dataReader["FILE_NAME"].ToString(),
						FILE_TYPE = dataReader["FILE_TYPE"].ToString(),
						ORIGINAL_NAME = dataReader["ORIGINAL_NAME"].ToString(),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyAttach> GetList()
		{
			string string_ = "SELECT * FROM SurveyAttach ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyAttach surveyAttach_0)
		{
			string string_ = string.Format("INSERT INTO SurveyAttach(SURVEY_ID,PAGE_ID,QUESTION_NAME,FILE_NAME,FILE_TYPE,ORIGINAL_NAME,SURVEY_GUID) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", new object[]
			{
				surveyAttach_0.SURVEY_ID,
				surveyAttach_0.PAGE_ID,
				surveyAttach_0.QUESTION_NAME,
				surveyAttach_0.FILE_NAME,
				surveyAttach_0.FILE_TYPE,
				surveyAttach_0.ORIGINAL_NAME,
				surveyAttach_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyAttach surveyAttach_0)
		{
			string string_ = string.Format("UPDATE SurveyAttach SET SURVEY_ID = '{1}',PAGE_ID = '{2}',QUESTION_NAME = '{3}',FILE_NAME = '{4}',FILE_TYPE = '{5}',ORIGINAL_NAME = '{6}',SURVEY_GUID = '{7}' WHERE ID = {0}", new object[]
			{
				surveyAttach_0.ID,
				surveyAttach_0.SURVEY_ID,
				surveyAttach_0.PAGE_ID,
				surveyAttach_0.QUESTION_NAME,
				surveyAttach_0.FILE_NAME,
				surveyAttach_0.FILE_TYPE,
				surveyAttach_0.ORIGINAL_NAME,
				surveyAttach_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyAttach surveyAttach_0)
		{
			string string_ = string.Format("DELETE FROM SurveyAttach WHERE ID ={0}", surveyAttach_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyAttach";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 8;
			string[] array = new string[8];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "问卷编号";
				array[2] = "页编号";
				array[3] = "问题编号";
				array[4] = "附件文件名";
				array[5] = "附件类型";
				array[6] = "附件原始路径及文件名";
				array[7] = "GUID 全球唯一码";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "PAGE_ID";
				array[3] = "QUESTION_NAME";
				array[4] = "FILE_NAME";
				array[5] = "FILE_TYPE";
				array[6] = "ORIGINAL_NAME";
				array[7] = "SURVEY_GUID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyAttach> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyAttach surveyAttach in list_0)
			{
				array[num, 0] = surveyAttach.ID.ToString();
				array[num, 1] = surveyAttach.SURVEY_ID;
				array[num, 2] = surveyAttach.PAGE_ID;
				array[num, 3] = surveyAttach.QUESTION_NAME;
				array[num, 4] = surveyAttach.FILE_NAME;
				array[num, 5] = surveyAttach.FILE_TYPE;
				array[num, 6] = surveyAttach.ORIGINAL_NAME;
				array[num, 7] = surveyAttach.SURVEY_GUID;
				num++;
			}
			return array;
		}

		public List<SurveyAttach> GetListBySurveyId(string string_0)
		{
			List<SurveyAttach> list = new List<SurveyAttach>();
			string string_ = string.Format("select * from SurveyAttach where SURVEY_ID='{0}' ORDER BY ID", string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyAttach> GetListByPageId(string string_0, string string_1)
		{
			List<SurveyAttach> list = new List<SurveyAttach>();
			string string_2 = string.Format("select * from SurveyAttach where SURVEY_ID='{0}' and PAGE_ID='{1}' ORDER BY ID", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public bool ExistsByPageId(string string_0, string string_1)
		{
			string string_2 = string.Format("select COUNT(*) from SurveyAttach where SURVEY_ID='{0}' and PAGE_ID='{1}' ", string_0, string_1);
			int num = this.dbprovider_0.ExecuteScalarInt(string_2);
			return num > 0;
		}

		public bool ExistsByQName(string string_0, string string_1)
		{
			string string_2 = string.Format("select COUNT(*) from SurveyAttach where SURVEY_ID='{0}' and QUESTION_NAME='{1}' ", string_0, string_1);
			int num = this.dbprovider_0.ExecuteScalarInt(string_2);
			return num > 0;
		}

		public List<SurveyAttach> GetListByQName(string string_0, string string_1)
		{
			List<SurveyAttach> list = new List<SurveyAttach>();
			string string_2 = string.Format("select * from SurveyAttach where SURVEY_ID='{0}' and QUESTION_NAME='{1}' ORDER BY ID", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public void DeleteByPageId(string string_0, string string_1)
		{
			string string_2 = string.Format("DELETE FROM SurveyAttach WHERE SURVEY_ID='{0}' and PAGE_ID='{1}'", string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public void DeleteByFileName(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format("DELETE FROM SurveyAttach WHERE SURVEY_ID='{0}' and PAGE_ID='{1}' and FILE_NAME ='{2}'", string_0, string_1, string_2);
			this.dbprovider_0.ExecuteNonQuery(string_3);
		}

		public void DeleteByQNameByFileName(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format("DELETE FROM SurveyAttach WHERE SURVEY_ID='{0}' and  QUESTION_NAME='{1}' and FILE_NAME ='{2}'", string_0, string_1, string_2);
			this.dbprovider_0.ExecuteNonQuery(string_3);
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format("delete from SurveyAttach where SURVEY_ID='{0}'", string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
