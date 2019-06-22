using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyAnswerHisDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyAnswerHis WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyAnswerHis GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyAnswerHis WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyAnswerHis GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyAnswerHis surveyAnswerHis = new SurveyAnswerHis();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyAnswerHis.ID = Convert.ToInt32(dataReader["ID"]);
					surveyAnswerHis.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyAnswerHis.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					surveyAnswerHis.CODE = dataReader["CODE"].ToString();
					surveyAnswerHis.MULTI_ORDER = Convert.ToInt32(dataReader["MULTI_ORDER"]);
					surveyAnswerHis.MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader["MODIFY_DATE"].ToString()));
					surveyAnswerHis.SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]);
					surveyAnswerHis.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
					surveyAnswerHis.BEGIN_DATE = new DateTime?(Convert.ToDateTime(dataReader["BEGIN_DATE"].ToString()));
					surveyAnswerHis.PAGE_ID = dataReader["PAGE_ID"].ToString();
					surveyAnswerHis.OP_TYPE_ID = Convert.ToInt32(dataReader["OP_TYPE_ID"]);
				}
			}
			return surveyAnswerHis;
		}

		public List<SurveyAnswerHis> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyAnswerHis> list = new List<SurveyAnswerHis>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyAnswerHis
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						CODE = dataReader["CODE"].ToString(),
						MULTI_ORDER = Convert.ToInt32(dataReader["MULTI_ORDER"]),
						MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader["MODIFY_DATE"].ToString())),
						SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString(),
						BEGIN_DATE = new DateTime?(Convert.ToDateTime(dataReader["BEGIN_DATE"].ToString())),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						OP_TYPE_ID = Convert.ToInt32(dataReader["OP_TYPE_ID"])
					});
				}
			}
			return list;
		}

		public List<SurveyAnswerHis> GetList()
		{
			string string_ = "SELECT * FROM SurveyAnswerHis ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyAnswerHis surveyAnswerHis_0)
		{
			string string_ = string.Format("INSERT INTO SurveyAnswerHis(SURVEY_ID,QUESTION_NAME,CODE,MULTI_ORDER,MODIFY_DATE,SEQUENCE_ID,SURVEY_GUID,BEGIN_DATE,PAGE_ID,OP_TYPE_ID) VALUES('{0}','{1}','{2}',{3},'{4}',{5},'{6}','{7}','{8}',{9})", new object[]
			{
				surveyAnswerHis_0.SURVEY_ID,
				surveyAnswerHis_0.QUESTION_NAME,
				surveyAnswerHis_0.CODE,
				surveyAnswerHis_0.MULTI_ORDER,
				surveyAnswerHis_0.MODIFY_DATE,
				surveyAnswerHis_0.SEQUENCE_ID,
				surveyAnswerHis_0.SURVEY_GUID,
				surveyAnswerHis_0.BEGIN_DATE,
				surveyAnswerHis_0.PAGE_ID,
				surveyAnswerHis_0.OP_TYPE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyAnswerHis surveyAnswerHis_0)
		{
			string string_ = string.Format("UPDATE SurveyAnswerHis SET SURVEY_ID = '{1}',QUESTION_NAME = '{2}',CODE = '{3}',MULTI_ORDER = {4},MODIFY_DATE = '{5}',SEQUENCE_ID = {6},SURVEY_GUID = '{7}',BEGIN_DATE = '{8}',PAGE_ID = '{9}',OP_TYPE_ID = {10} WHERE ID = {0}", new object[]
			{
				surveyAnswerHis_0.ID,
				surveyAnswerHis_0.SURVEY_ID,
				surveyAnswerHis_0.QUESTION_NAME,
				surveyAnswerHis_0.CODE,
				surveyAnswerHis_0.MULTI_ORDER,
				surveyAnswerHis_0.MODIFY_DATE,
				surveyAnswerHis_0.SEQUENCE_ID,
				surveyAnswerHis_0.SURVEY_GUID,
				surveyAnswerHis_0.BEGIN_DATE,
				surveyAnswerHis_0.PAGE_ID,
				surveyAnswerHis_0.OP_TYPE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyAnswerHis surveyAnswerHis_0)
		{
			string string_ = string.Format("DELETE FROM SurveyAnswerHis WHERE ID ={0}", surveyAnswerHis_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyAnswerHis";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 11;
			string[] array = new string[11];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "问卷编号";
				array[2] = "问题编号";
				array[3] = "答案编码";
				array[4] = "答案选择的顺序";
				array[5] = "修改时间";
				array[6] = "问卷序列号";
				array[7] = "GUID 全球唯一码";
				array[8] = "进入问题页面开始时间";
				array[9] = "页编号";
				array[10] = "备份数据的操作类型";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "QUESTION_NAME";
				array[3] = "CODE";
				array[4] = "MULTI_ORDER";
				array[5] = "MODIFY_DATE";
				array[6] = "SEQUENCE_ID";
				array[7] = "SURVEY_GUID";
				array[8] = "BEGIN_DATE";
				array[9] = "PAGE_ID";
				array[10] = "OP_TYPE_ID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyAnswerHis> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyAnswerHis surveyAnswerHis in list_0)
			{
				array[num, 0] = surveyAnswerHis.ID.ToString();
				array[num, 1] = surveyAnswerHis.SURVEY_ID;
				array[num, 2] = surveyAnswerHis.QUESTION_NAME;
				array[num, 3] = surveyAnswerHis.CODE;
				array[num, 4] = surveyAnswerHis.MULTI_ORDER.ToString();
				array[num, 5] = surveyAnswerHis.MODIFY_DATE.ToString();
				array[num, 6] = surveyAnswerHis.SEQUENCE_ID.ToString();
				array[num, 7] = surveyAnswerHis.SURVEY_GUID;
				array[num, 8] = surveyAnswerHis.BEGIN_DATE.ToString();
				array[num, 9] = surveyAnswerHis.PAGE_ID;
				array[num, 10] = surveyAnswerHis.OP_TYPE_ID.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
