using System;
using System.Collections.Generic;
using System.Data;
using LoyalFilial.MarketResearch.Common;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyRandomDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyRandom WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyRandom GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyRandom WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyRandom GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyRandom surveyRandom = new SurveyRandom();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyRandom.ID = Convert.ToInt32(dataReader["ID"]);
					surveyRandom.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveyRandom.QUESTION_SET = dataReader["QUESTION_SET"].ToString();
					surveyRandom.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					surveyRandom.CODE = dataReader["CODE"].ToString();
					surveyRandom.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
					surveyRandom.RANDOM_INDEX = Convert.ToInt32(dataReader["RANDOM_INDEX"]);
					surveyRandom.RANDOM_SET1 = Convert.ToInt32(dataReader["RANDOM_SET1"]);
					surveyRandom.RANDOM_SET2 = Convert.ToInt32(dataReader["RANDOM_SET2"]);
					surveyRandom.RANDOM_SET3 = Convert.ToInt32(dataReader["RANDOM_SET3"]);
					surveyRandom.IS_FIX = Convert.ToInt32(dataReader["IS_FIX"]);
					surveyRandom.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
				}
			}
			return surveyRandom;
		}

		public List<SurveyRandom> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyRandom> list = new List<SurveyRandom>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyRandom
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						QUESTION_SET = dataReader["QUESTION_SET"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						CODE = dataReader["CODE"].ToString(),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString(),
						RANDOM_INDEX = Convert.ToInt32(dataReader["RANDOM_INDEX"]),
						RANDOM_SET1 = Convert.ToInt32(dataReader["RANDOM_SET1"]),
						RANDOM_SET2 = Convert.ToInt32(dataReader["RANDOM_SET2"]),
						RANDOM_SET3 = Convert.ToInt32(dataReader["RANDOM_SET3"]),
						IS_FIX = Convert.ToInt32(dataReader["IS_FIX"]),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyRandom> GetList()
		{
			string string_ = "SELECT * FROM SurveyRandom ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format("INSERT INTO SurveyRandom(SURVEY_ID,QUESTION_SET,QUESTION_NAME,CODE,PARENT_CODE,RANDOM_INDEX,RANDOM_SET1,RANDOM_SET2,RANDOM_SET3,IS_FIX,SURVEY_GUID) VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},'{10}')", new object[]
			{
				surveyRandom_0.SURVEY_ID,
				surveyRandom_0.QUESTION_SET,
				surveyRandom_0.QUESTION_NAME,
				surveyRandom_0.CODE,
				surveyRandom_0.PARENT_CODE,
				surveyRandom_0.RANDOM_INDEX,
				surveyRandom_0.RANDOM_SET1,
				surveyRandom_0.RANDOM_SET2,
				surveyRandom_0.RANDOM_SET3,
				surveyRandom_0.IS_FIX,
				surveyRandom_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format("UPDATE SurveyRandom SET SURVEY_ID = '{1}',QUESTION_SET = '{2}',QUESTION_NAME = '{3}',CODE = '{4}',PARENT_CODE = '{5}',RANDOM_INDEX = {6},RANDOM_SET1 = {7},RANDOM_SET2 = {8},RANDOM_SET3 = {9},IS_FIX = {10},SURVEY_GUID = '{11}' WHERE ID = {0}", new object[]
			{
				surveyRandom_0.ID,
				surveyRandom_0.SURVEY_ID,
				surveyRandom_0.QUESTION_SET,
				surveyRandom_0.QUESTION_NAME,
				surveyRandom_0.CODE,
				surveyRandom_0.PARENT_CODE,
				surveyRandom_0.RANDOM_INDEX,
				surveyRandom_0.RANDOM_SET1,
				surveyRandom_0.RANDOM_SET2,
				surveyRandom_0.RANDOM_SET3,
				surveyRandom_0.IS_FIX,
				surveyRandom_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyRandom surveyRandom_0)
		{
			string string_ = string.Format("DELETE FROM SurveyRandom WHERE ID ={0}", surveyRandom_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyRandom";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 12;
			string[] array = new string[12];
			if (bool_0)
			{
				array[0] = "主键 自动编号";
				array[1] = "问卷编号";
				array[2] = "题集标识";
				array[3] = "题号";
				array[4] = "编码";
				array[5] = "上级编码";
				array[6] = "随机索引";
				array[7] = "随机组 第 1 层级";
				array[8] = "随机组 第 2 层级";
				array[9] = "随机组 第 3 层级";
				array[10] = "固定标识";
				array[11] = "GUID 全球唯一码";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "QUESTION_SET";
				array[3] = "QUESTION_NAME";
				array[4] = "CODE";
				array[5] = "PARENT_CODE";
				array[6] = "RANDOM_INDEX";
				array[7] = "RANDOM_SET1";
				array[8] = "RANDOM_SET2";
				array[9] = "RANDOM_SET3";
				array[10] = "IS_FIX";
				array[11] = "SURVEY_GUID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyRandom> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyRandom surveyRandom in list_0)
			{
				array[num, 0] = surveyRandom.ID.ToString();
				array[num, 1] = surveyRandom.SURVEY_ID;
				array[num, 2] = surveyRandom.QUESTION_SET;
				array[num, 3] = surveyRandom.QUESTION_NAME;
				array[num, 4] = surveyRandom.CODE;
				array[num, 5] = surveyRandom.PARENT_CODE;
				array[num, 6] = surveyRandom.RANDOM_INDEX.ToString();
				array[num, 7] = surveyRandom.RANDOM_SET1.ToString();
				array[num, 8] = surveyRandom.RANDOM_SET2.ToString();
				array[num, 9] = surveyRandom.RANDOM_SET3.ToString();
				array[num, 10] = surveyRandom.IS_FIX.ToString();
				array[num, 11] = surveyRandom.SURVEY_GUID;
				num++;
			}
			return array;
		}

		public SurveyRandom GetById(int int_0)
		{
			string string_ = string.Format("select * from SurveyRandom where ID='{0}'", int_0);
			return this.GetBySql(string_);
		}

		public void AddList(string string_0, List<SurveyRandom> list_0)
		{
			foreach (SurveyRandom surveyRandom in list_0)
			{
				surveyRandom.SURVEY_ID = string_0;
				this.Add(surveyRandom);
			}
		}

		public void DeleteRandom(string string_0)
		{
			string string_ = string.Format("delete from SurveyRandom where survey_id ='{0}'", string_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void CopyRandom(string string_0, List<SurveyRandom> list_0)
		{
			this.AddList(string_0, list_0);
		}

		public bool CheckBaseCopyOK(string string_0, int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyRandom WHERE SURVEY_ID='{0}'", string_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num == int_0;
		}

		public List<SurveyRandom> GetList(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' order by random_index", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public List<SurveyRandom> GetListNoFix(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND IS_FIX=0", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public void UpdateRandom(List<SurveyRandom> list_0)
		{
			foreach (SurveyRandom surveyRandom in list_0)
			{
				string string_ = string.Format("update SurveyRandom set RANDOM_INDEX={0},RANDOM_SET1={1},RANDOM_SET2={2},RANDOM_SET3={3} where ID={4}", new object[]
				{
					surveyRandom.RANDOM_INDEX,
					surveyRandom.RANDOM_SET1,
					surveyRandom.RANDOM_SET2,
					surveyRandom.RANDOM_SET3,
					surveyRandom.ID
				});
				this.dbprovider_0.ExecuteNonQuery(string_);
			}
		}

		public List<SurveyRandom> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format("select * from SurveyRandom where SURVEY_ID='{0}' order by QUESTION_SET,RANDOM_INDEX", string_0);
			return this.GetListBySql(string_);
		}

		public string GetOneCode(string string_0, string string_1, int int_0, int int_1)
		{
			string string_2 = string.Format("select CODE from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND RANDOM_INDEX={2}", string_0, string_1, int_1);
			return this.dbprovider_0.ExecuteScalarString(string_2);
		}

		public int GetGroupCount(string string_0, string string_1, int int_0)
		{
			string string_2 = "";
			switch (int_0)
			{
			case 1:
				string_2 = string.Format("select MAX(RANDOM_SET1) AS MAX_SET from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' ", string_0, string_1);
				break;
			case 2:
				string_2 = string.Format("select MAX(RANDOM_SET2) AS MAX_SET from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' ", string_0, string_1);
				break;
			case 3:
				string_2 = string.Format("select MAX(RANDOM_SET3) AS MAX_SET from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' ", string_0, string_1);
				break;
			}
			string s = this.dbprovider_0.ExecuteScalarString(string_2);
			return int.Parse(s);
		}

		public int GetGroupCountNoFix(string string_0, string string_1, int int_0)
		{
			string string_2 = "";
			switch (int_0)
			{
			case 1:
				string_2 = string.Format("select MAX(RANDOM_SET1) AS MAX_SET from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND IS_FIX=0", string_0, string_1);
				break;
			case 2:
				string_2 = string.Format("select MAX(RANDOM_SET2) AS MAX_SET from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND IS_FIX=0", string_0, string_1);
				break;
			case 3:
				string_2 = string.Format("select MAX(RANDOM_SET3) AS MAX_SET from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND IS_FIX=0", string_0, string_1);
				break;
			}
			string s = this.dbprovider_0.ExecuteScalarString(string_2);
			return int.Parse(s);
		}

		public void RunSQL(string string_0)
		{
			this.dbprovider_0.ExecuteNonQuery(string_0);
		}

		public SurveyRandom GetCircleOne(string string_0, string string_1, int int_0)
		{
			string string_2 = string.Format("select * from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND RANDOM_INDEX={2}", string_0, string_1, int_0);
			return this.GetBySql(string_2);
		}

		public List<SurveyRandom> GetListNoJump(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND QUESTION_NAME<>'JUMP'", string_0, string_1);
			return this.GetListBySql(string_2);
		}

		public int GetCircleCount(string string_0, string string_1)
		{
			string string_2 = string.Format("select Count(*) as NCount from SurveyRandom where SURVEY_ID='{0}' AND QUESTION_SET='{1}' AND QUESTION_NAME<>'JUMP'", string_0, string_1);
			return this.dbprovider_0.ExecuteScalarInt(string_2);
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format("delete from SurveyRandom where SURVEY_ID='{0}'", string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
