using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyQuotaAnswerDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyQuotaAnswer WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyQuotaAnswer GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyQuotaAnswer WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyQuotaAnswer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyQuotaAnswer surveyQuotaAnswer = new SurveyQuotaAnswer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyQuotaAnswer.ID = Convert.ToInt32(dataReader["ID"]);
					surveyQuotaAnswer.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyQuotaAnswer.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
					surveyQuotaAnswer.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					surveyQuotaAnswer.CODE = dataReader["CODE"].ToString();
					surveyQuotaAnswer.IS_FINISH = Convert.ToInt32(dataReader["IS_FINISH"]);
				}
			}
			return surveyQuotaAnswer;
		}

		public List<SurveyQuotaAnswer> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyQuotaAnswer> list = new List<SurveyQuotaAnswer>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyQuotaAnswer
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						CODE = dataReader["CODE"].ToString(),
						IS_FINISH = Convert.ToInt32(dataReader["IS_FINISH"])
					});
				}
			}
			return list;
		}

		public List<SurveyQuotaAnswer> GetList()
		{
			string string_ = "SELECT * FROM SurveyQuotaAnswer ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			string string_ = string.Format("INSERT INTO SurveyQuotaAnswer(SURVEY_ID,SURVEY_GUID,QUESTION_NAME,CODE,IS_FINISH) VALUES('{0}','{1}','{2}','{3}',{4})", new object[]
			{
				surveyQuotaAnswer_0.SURVEY_ID,
				surveyQuotaAnswer_0.SURVEY_GUID,
				surveyQuotaAnswer_0.QUESTION_NAME,
				surveyQuotaAnswer_0.CODE,
				surveyQuotaAnswer_0.IS_FINISH
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			string string_ = string.Format("UPDATE SurveyQuotaAnswer SET SURVEY_ID = '{1}',SURVEY_GUID = '{2}',QUESTION_NAME = '{3}',CODE = '{4}',IS_FINISH = {5} WHERE ID = {0}", new object[]
			{
				surveyQuotaAnswer_0.ID,
				surveyQuotaAnswer_0.SURVEY_ID,
				surveyQuotaAnswer_0.SURVEY_GUID,
				surveyQuotaAnswer_0.QUESTION_NAME,
				surveyQuotaAnswer_0.CODE,
				surveyQuotaAnswer_0.IS_FINISH
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			string string_ = string.Format("DELETE FROM SurveyQuotaAnswer WHERE ID ={0}", surveyQuotaAnswer_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyQuotaAnswer";
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
				array[3] = "问题编号";
				array[4] = "答案编码";
				array[5] = "问卷状态";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "SURVEY_GUID";
				array[3] = "QUESTION_NAME";
				array[4] = "CODE";
				array[5] = "IS_FINISH";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyQuotaAnswer> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyQuotaAnswer surveyQuotaAnswer in list_0)
			{
				array[num, 0] = surveyQuotaAnswer.ID.ToString();
				array[num, 1] = surveyQuotaAnswer.SURVEY_ID;
				array[num, 2] = surveyQuotaAnswer.SURVEY_GUID;
				array[num, 3] = surveyQuotaAnswer.QUESTION_NAME;
				array[num, 4] = surveyQuotaAnswer.CODE;
				array[num, 5] = surveyQuotaAnswer.IS_FINISH.ToString();
				num++;
			}
			return array;
		}

		public int AddOne(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			int result;
			if (this.ExistsByModel(surveyQuotaAnswer_0))
			{
				this.Update(surveyQuotaAnswer_0);
				result = 1;
			}
			else
			{
				this.Add(surveyQuotaAnswer_0);
				result = 0;
			}
			return result;
		}

		public bool ExistsByModel(SurveyQuotaAnswer surveyQuotaAnswer_0)
		{
			string string_ = string.Format("select * from SurveyQuotaAnswer where SURVEY_ID ='{0}' and SURVEY_GUID='{1}' and QUESTION_NAME='{2}'", surveyQuotaAnswer_0.SURVEY_ID, surveyQuotaAnswer_0.SURVEY_GUID, surveyQuotaAnswer_0.QUESTION_NAME);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public int GetSumByModel(int int_0, string string_0, string string_1)
		{
			string string_2 = string.Format("select count(*) as NCount from SurveyQuotaAnswer where IS_FINISH = {0} and QUESTION_NAME ='{1}' and CODE='{2}'", int_0.ToString(), string_0, string_1);
			return this.dbprovider_0.ExecuteScalarInt(string_2);
		}

		public void UpdateStatus(int int_0, string string_0, string string_1)
		{
			string string_2 = string.Format("update SurveyQuotaAnswer set IS_FINISH = {0} where SURVEY_ID ='{1}' and SURVEY_GUID='{2}'", int_0.ToString(), string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
