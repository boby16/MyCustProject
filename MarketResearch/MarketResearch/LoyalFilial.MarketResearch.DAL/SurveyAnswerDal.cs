using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyAnswerDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyAnswer WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyAnswer GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyAnswer WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyAnswer GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyAnswer.ID = Convert.ToInt32(dataReader["ID"]);
					surveyAnswer.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyAnswer.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					surveyAnswer.CODE = dataReader["CODE"].ToString();
					surveyAnswer.MULTI_ORDER = Convert.ToInt32(dataReader["MULTI_ORDER"]);
					surveyAnswer.MODIFY_DATE = new DateTime?(Convert.ToDateTime(dataReader["MODIFY_DATE"].ToString()));
					surveyAnswer.SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]);
					surveyAnswer.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
					surveyAnswer.BEGIN_DATE = new DateTime?(Convert.ToDateTime(dataReader["BEGIN_DATE"].ToString()));
					surveyAnswer.PAGE_ID = dataReader["PAGE_ID"].ToString();
				}
			}
			return surveyAnswer;
		}

		public List<SurveyAnswer> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyAnswer
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
						PAGE_ID = dataReader["PAGE_ID"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyAnswer> GetList()
		{
			string string_ = "SELECT * FROM SurveyAnswer ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Delete(SurveyAnswer surveyAnswer_0)
		{
			string string_ = string.Format("DELETE FROM SurveyAnswer WHERE ID ={0}", surveyAnswer_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyAnswer";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 10;
			string[] array = new string[10];
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
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyAnswer> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyAnswer surveyAnswer in list_0)
			{
				array[num, 0] = surveyAnswer.ID.ToString();
				array[num, 1] = surveyAnswer.SURVEY_ID;
				array[num, 2] = surveyAnswer.QUESTION_NAME;
				array[num, 3] = surveyAnswer.CODE;
				array[num, 4] = surveyAnswer.MULTI_ORDER.ToString();
				array[num, 5] = surveyAnswer.MODIFY_DATE.ToString();
				array[num, 6] = surveyAnswer.SEQUENCE_ID.ToString();
				array[num, 7] = surveyAnswer.SURVEY_GUID;
				array[num, 8] = surveyAnswer.BEGIN_DATE.ToString();
				array[num, 9] = surveyAnswer.PAGE_ID;
				num++;
			}
			return array;
		}

		public bool Exists(string string_0, string string_1, string string_2 = "")
		{
			string string_3;
			if (string_2 == "")
			{
				string_3 = string.Format("select count(*) from SurveyAnswer where SURVEY_ID='{0}' and QUESTION_NAME='{1}'", string_0, string_1);
			}
			else
			{
				string_3 = string.Format("select count(*) from SurveyAnswer where SURVEY_ID='{0}' and QUESTION_NAME LIKE '{1}{2}%'", string_0, string_1, string_2.Trim());
			}
			int num = this.dbprovider_0.ExecuteScalarInt(string_3);
			return num > 0;
		}

		public void AddByModel(SurveyAnswer surveyAnswer_0)
		{
			string string_ = string.Format("INSERT INTO SurveyAnswer(SURVEY_ID,QUESTION_NAME,CODE,MULTI_ORDER,MODIFY_DATE,SEQUENCE_ID,BEGIN_DATE,SURVEY_GUID,PAGE_ID) VALUES('{0}','{1}','{2}',{3},'{4}',{5},'{6}','{7}','{8}')", new object[]
			{
				surveyAnswer_0.SURVEY_ID,
				surveyAnswer_0.QUESTION_NAME,
				surveyAnswer_0.CODE,
				surveyAnswer_0.MULTI_ORDER,
				surveyAnswer_0.MODIFY_DATE,
				surveyAnswer_0.SEQUENCE_ID,
				surveyAnswer_0.BEGIN_DATE,
				surveyAnswer_0.SURVEY_GUID,
				surveyAnswer_0.PAGE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Add(SurveyAnswer surveyAnswer_0)
		{
			surveyAnswer_0.MODIFY_DATE = new DateTime?(DateTime.Now);
			if (surveyAnswer_0.BEGIN_DATE == null)
			{
				surveyAnswer_0.BEGIN_DATE = new DateTime?(DateTime.Now);
			}
			string string_ = string.Format("INSERT INTO SurveyAnswer(SURVEY_ID,QUESTION_NAME,CODE,MULTI_ORDER,MODIFY_DATE,SEQUENCE_ID,BEGIN_DATE,PAGE_ID) VALUES('{0}','{1}','{2}',{3},'{4}',{5},'{6}','{7}')", new object[]
			{
				surveyAnswer_0.SURVEY_ID,
				surveyAnswer_0.QUESTION_NAME,
				surveyAnswer_0.CODE,
				surveyAnswer_0.MULTI_ORDER,
				surveyAnswer_0.MODIFY_DATE,
				surveyAnswer_0.SEQUENCE_ID,
				surveyAnswer_0.BEGIN_DATE,
				surveyAnswer_0.PAGE_ID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyAnswer surveyAnswer_0)
		{
			string string_ = string.Format("INSERT INTO SurveyAnswerHIS ( SURVEY_ID, QUESTION_NAME, CODE, MULTI_ORDER, MODIFY_DATE, SEQUENCE_ID, SURVEY_GUID, OP_TYPE_ID,BEGIN_DATE, PAGE_ID)  SELECT a.SURVEY_ID, a.QUESTION_NAME, a.CODE, a.MULTI_ORDER, '{2}', a.SEQUENCE_ID, a.SURVEY_GUID, 1, a.BEGIN_DATE, a.PAGE_ID  FROM SurveyAnswer a where a.SURVEY_ID = '{0}' AND a.QUESTION_NAME = '{1}'", surveyAnswer_0.SURVEY_ID, surveyAnswer_0.QUESTION_NAME, DateTime.Now);
			this.dbprovider_0.ExecuteNonQuery(string_);
			string_ = string.Format("UPDATE SurveyAnswer SET CODE = '{2}',MULTI_ORDER = {3},SEQUENCE_ID = {4},MODIFY_DATE = '{7}',BEGIN_DATE = '{5}',PAGE_ID = '{6}' WHERE SURVEY_ID = '{0}' AND QUESTION_NAME = '{1}'", new object[]
			{
				surveyAnswer_0.SURVEY_ID,
				surveyAnswer_0.QUESTION_NAME,
				surveyAnswer_0.CODE,
				surveyAnswer_0.MULTI_ORDER,
				surveyAnswer_0.SEQUENCE_ID,
				surveyAnswer_0.BEGIN_DATE,
				surveyAnswer_0.PAGE_ID,
				DateTime.Now
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void AddOne(string string_0, string string_1, string string_2, int int_0)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.SURVEY_ID = string_0;
			surveyAnswer.SEQUENCE_ID = int_0;
			surveyAnswer.QUESTION_NAME = string_1;
			surveyAnswer.CODE = string_2;
			surveyAnswer.MULTI_ORDER = 0;
			surveyAnswer.BEGIN_DATE = new DateTime?(DateTime.Now);
			if (this.Exists(surveyAnswer.SURVEY_ID, surveyAnswer.QUESTION_NAME, ""))
			{
				this.Update(surveyAnswer);
			}
			else
			{
				this.Add(surveyAnswer);
			}
		}

		public void AddOneNoUpdate(string string_0, string string_1, string string_2, int int_0)
		{
			this.Add(new SurveyAnswer
			{
				SURVEY_ID = string_0,
				SEQUENCE_ID = int_0,
				QUESTION_NAME = string_1,
				CODE = string_2,
				MULTI_ORDER = 0,
				BEGIN_DATE = new DateTime?(DateTime.Now)
			});
		}

		public void AddList(string string_0, int int_0, List<SurveyAnswer> list_0)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			SurveyAnswer surveyAnswer2 = new SurveyAnswer();
			surveyAnswer2.SURVEY_ID = string_0;
			surveyAnswer2.SEQUENCE_ID = int_0;
			for (int i = 0; i < list_0.Count; i++)
			{
				surveyAnswer = list_0[i];
				surveyAnswer2.QUESTION_NAME = surveyAnswer.QUESTION_NAME;
				surveyAnswer2.CODE = surveyAnswer.CODE;
				surveyAnswer2.MULTI_ORDER = surveyAnswer.MULTI_ORDER;
				surveyAnswer2.BEGIN_DATE = surveyAnswer.BEGIN_DATE;
				this.Add(surveyAnswer2);
			}
		}

		public void SaveOneAnswer(SurveyAnswer surveyAnswer_0)
		{
			if (this.Exists(surveyAnswer_0.SURVEY_ID, surveyAnswer_0.QUESTION_NAME, ""))
			{
				this.Update(surveyAnswer_0);
			}
			else
			{
				this.Add(surveyAnswer_0);
			}
		}

		public void UpdateList(string string_0, int int_0, List<SurveyAnswer> list_0)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.SURVEY_ID = string_0;
			surveyAnswer.SEQUENCE_ID = int_0;
			foreach (SurveyAnswer surveyAnswer2 in list_0)
			{
				surveyAnswer.QUESTION_NAME = surveyAnswer2.QUESTION_NAME;
				surveyAnswer.CODE = surveyAnswer2.CODE;
				surveyAnswer.MULTI_ORDER = surveyAnswer2.MULTI_ORDER;
				surveyAnswer.BEGIN_DATE = surveyAnswer2.BEGIN_DATE;
				if (this.Exists(string_0, surveyAnswer.QUESTION_NAME, ""))
				{
					this.Update(surveyAnswer);
				}
				else
				{
					this.Add(surveyAnswer);
				}
			}
		}

		public void ClearBySequenceId(string string_0, int int_0)
		{
			string string_ = string.Format("INSERT INTO SurveyAnswerHIS ( SURVEY_ID, QUESTION_NAME, CODE, MULTI_ORDER, MODIFY_DATE, SEQUENCE_ID, SURVEY_GUID, OP_TYPE_ID,BEGIN_DATE,PAGE_ID)  SELECT a.SURVEY_ID, a.QUESTION_NAME, a.CODE, a.MULTI_ORDER, '{2}', a.SEQUENCE_ID, a.SURVEY_GUID, 2, a.BEGIN_DATE, a.PAGE_ID  FROM SurveyAnswer a WHERE a.SURVEY_ID = '{0}' AND a.SEQUENCE_ID = {1}", string_0, int_0, DateTime.Now);
			this.dbprovider_0.ExecuteNonQuery(string_);
			string_ = string.Format("UPDATE SurveyAnswer SET CODE = '',MULTI_ORDER = 0, SEQUENCE_ID =999999 WHERE SURVEY_ID = '{0}' AND SEQUENCE_ID = {1}", string_0, int_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void ClearMultiple(string string_0, string string_1)
		{
			string string_2 = string.Format("INSERT INTO SurveyAnswerHIS ( SURVEY_ID, QUESTION_NAME, CODE, MULTI_ORDER, MODIFY_DATE, SEQUENCE_ID, SURVEY_GUID, OP_TYPE_ID, BEGIN_DATE, PAGE_ID)  SELECT a.SURVEY_ID, a.QUESTION_NAME, a.CODE, a.MULTI_ORDER, '{2}', a.SEQUENCE_ID, a.SURVEY_GUID, 3, a.BEGIN_DATE,a.PAGE_ID  FROM SurveyAnswer a WHERE a.SURVEY_ID='{0}' AND ( a.QUESTION_NAME LIKE '{1}_A%' OR a.QUESTION_NAME LIKE '{1}_OTH' OR a.QUESTION_NAME LIKE '{1}_OTH_C%')", string_0, string_1, DateTime.Now);
			this.dbprovider_0.ExecuteNonQuery(string_2);
			string_2 = string.Format("UPDATE SurveyAnswer SET CODE = '',MULTI_ORDER = 0, SEQUENCE_ID =999998 WHERE SURVEY_ID='{0}' AND ( QUESTION_NAME LIKE '{1}_A%' OR QUESTION_NAME LIKE '{1}_OTH' OR QUESTION_NAME LIKE '{1}_OTH_C%')", string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public void ClearSingle(string string_0, string string_1)
		{
			string string_2 = string.Format("INSERT INTO SurveyAnswerHIS ( SURVEY_ID, QUESTION_NAME, CODE, MULTI_ORDER, MODIFY_DATE, SEQUENCE_ID, SURVEY_GUID, OP_TYPE_ID, BEGIN_DATE, PAGE_ID)  SELECT a.SURVEY_ID, a.QUESTION_NAME, a.CODE, a.MULTI_ORDER, '{2}', a.SEQUENCE_ID, a.SURVEY_GUID, 3, a.BEGIN_DATE,a.PAGE_ID FROM SurveyAnswer a WHERE a.SURVEY_ID = '{0}' AND ( a.QUESTION_NAME = '{1}' OR a.QUESTION_NAME = '{1}_OTH')", string_0, string_1, DateTime.Now);
			this.dbprovider_0.ExecuteNonQuery(string_2);
			string_2 = string.Format("UPDATE SurveyAnswer SET CODE = '',MULTI_ORDER = 0, SEQUENCE_ID =999997 WHERE SURVEY_ID = '{0}' AND ( QUESTION_NAME = '{1}' OR QUESTION_NAME = '{1}_OTH')", string_0, string_1);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		public void SaveByArray(string string_0, int int_0, string[,] string_1, int int_1)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			for (int i = 0; i < int_1; i++)
			{
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = string_1[i, 0],
					CODE = string_1[i, 1],
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(DateTime.Now)
				});
			}
			this.UpdateList(string_0, int_0, list);
		}

		public List<SurveyAnswer> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' order by id", string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyAnswer> GetListBySequenceId(string string_0, int int_0)
		{
			string string_ = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and Sequence_id = {1} order by id", string_0, int_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyAnswer> GetListByMultiple(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}_A%' order by id", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetListByMultipleBySequenceId(string string_0, string string_1, int int_0)
		{
			string string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}_A%'  and Sequence_id = {2} order by id", string_0, string_1, int_0);
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetListByCode(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}%' order by id", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetListRecord(string string_0)
		{
			string string_ = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name ='SURVEY_AUDIO'  and CODE<>'' order by id", string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyAnswer> GetCircleList(string string_0, string string_1, bool bool_0 = false)
		{
			string string_2 = "";
			if (bool_0)
			{
				string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}_R%' order by id", string_0, string_1);
			}
			else
			{
				string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}_R%' and question_name not like '%_OTH%' order by id", string_0, string_1);
			}
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetCircleList_C(string string_0, string string_1, string string_2 = "C")
		{
			string string_3 = "";
			string_3 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}_{2}%' and question_name not like '%_OTH%' order by id", string_0, string_1, string_2);
			return this.GetListBySql(string_3);
		}

		public List<SurveyAnswer> GetCircleListOther(string string_0, string string_1)
		{
			string string_2 = "";
			string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}_R%' and question_name like '%_OTH' order by id", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public List<SurveyAnswer> GetCircleListByConbine(string string_0, string string_1, bool bool_0 = false)
		{
			string string_2 = "";
			if (bool_0)
			{
				string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}%' order by id", string_0, string_1);
			}
			else
			{
				string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}%' and question_name not like '%_OTH' order by id", string_0, string_1);
			}
			return this.GetListBySql(string_2);
		}

		public int GetCountByMultiple(string string_0, string string_1)
		{
			string string_2 = string.Format("select count(*) from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}_A%' AND question_name NOT LIKE '{1}_OTH_C%'", string_0, string_1);
			return this.dbprovider_0.ExecuteScalarInt(string_2);
		}

		public int GetCountByMultiple(string string_0, string string_1, out string string_2)
		{
			string string_3 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name like '{1}_A%' AND question_name NOT LIKE '{1}_OTH_C%' order by id", string_0, string_1);
			List<SurveyAnswer> listBySql = this.GetListBySql(string_3);
			string_2 = "";
			if (listBySql.Count<SurveyAnswer>() == 1)
			{
				string_2 = listBySql[0].CODE;
			}
			return listBySql.Count<SurveyAnswer>();
		}

		public SurveyAnswer GetOne(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyAnswer where SURVEY_ID='{0}' and question_name='{1}'", string_0, string_1);
			return this.GetBySql(string_2);
		}

		public string GetOneCode(string string_0, string string_1)
		{
			string string_2 = string.Format("select CODE from SurveyAnswer where SURVEY_ID='{0}' and question_name='{1}'", string_0, string_1);
			return this.dbprovider_0.ExecuteScalarString(string_2);
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format("delete from SurveyAnswer where SURVEY_ID='{0}'", string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
