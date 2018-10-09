using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveySequenceDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveySequence WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveySequence GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveySequence WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveySequence GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveySequence surveySequence = new SurveySequence();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveySequence.ID = Convert.ToInt32(dataReader["ID"]);
					surveySequence.SURVEY_ID = dataReader["SURVEY_ID"].ToString();
					surveySequence.SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]);
					surveySequence.PAGE_ID = dataReader["PAGE_ID"].ToString();
					surveySequence.CIRCLE_A_CURRENT = Convert.ToInt32(dataReader["CIRCLE_A_CURRENT"]);
					surveySequence.CIRCLE_A_COUNT = Convert.ToInt32(dataReader["CIRCLE_A_COUNT"]);
					surveySequence.CIRCLE_B_CURRENT = Convert.ToInt32(dataReader["CIRCLE_B_CURRENT"]);
					surveySequence.CIRCLE_B_COUNT = Convert.ToInt32(dataReader["CIRCLE_B_COUNT"]);
					surveySequence.VERSION_ID = Convert.ToInt32(dataReader["VERSION_ID"]);
					surveySequence.PAGE_BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader["PAGE_BEGIN_TIME"].ToString()));
					surveySequence.PAGE_END_TIME = new DateTime?(Convert.ToDateTime(dataReader["PAGE_END_TIME"].ToString()));
					surveySequence.RECORD_FILE = dataReader["RECORD_FILE"].ToString();
					surveySequence.RECORD_START_TIME = new DateTime?(Convert.ToDateTime(dataReader["RECORD_START_TIME"].ToString()));
					surveySequence.RECORD_BEGIN_TIME = Convert.ToInt32(dataReader["RECORD_BEGIN_TIME"]);
					surveySequence.RECORD_END_TIME = Convert.ToInt32(dataReader["RECORD_END_TIME"]);
					surveySequence.PAGE_TIME = Convert.ToInt32(dataReader["PAGE_TIME"]);
					surveySequence.SURVEY_GUID = dataReader["SURVEY_GUID"].ToString();
				}
			}
			return surveySequence;
		}

		public List<SurveySequence> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveySequence> list = new List<SurveySequence>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveySequence
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						SURVEY_ID = dataReader["SURVEY_ID"].ToString(),
						SEQUENCE_ID = Convert.ToInt32(dataReader["SEQUENCE_ID"]),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						CIRCLE_A_CURRENT = Convert.ToInt32(dataReader["CIRCLE_A_CURRENT"]),
						CIRCLE_A_COUNT = Convert.ToInt32(dataReader["CIRCLE_A_COUNT"]),
						CIRCLE_B_CURRENT = Convert.ToInt32(dataReader["CIRCLE_B_CURRENT"]),
						CIRCLE_B_COUNT = Convert.ToInt32(dataReader["CIRCLE_B_COUNT"]),
						VERSION_ID = Convert.ToInt32(dataReader["VERSION_ID"]),
						PAGE_BEGIN_TIME = new DateTime?(Convert.ToDateTime(dataReader["PAGE_BEGIN_TIME"].ToString())),
						PAGE_END_TIME = new DateTime?(Convert.ToDateTime(dataReader["PAGE_END_TIME"].ToString())),
						RECORD_FILE = dataReader["RECORD_FILE"].ToString(),
						RECORD_START_TIME = new DateTime?(Convert.ToDateTime(dataReader["RECORD_START_TIME"].ToString())),
						RECORD_BEGIN_TIME = Convert.ToInt32(dataReader["RECORD_BEGIN_TIME"]),
						RECORD_END_TIME = Convert.ToInt32(dataReader["RECORD_END_TIME"]),
						PAGE_TIME = Convert.ToInt32(dataReader["PAGE_TIME"]),
						SURVEY_GUID = dataReader["SURVEY_GUID"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveySequence> GetList()
		{
			string string_ = "SELECT * FROM SurveySequence ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveySequence surveySequence_0)
		{
			string string_ = string.Format("INSERT INTO SurveySequence(SURVEY_ID,SEQUENCE_ID,PAGE_ID,CIRCLE_A_CURRENT,CIRCLE_A_COUNT,CIRCLE_B_CURRENT,CIRCLE_B_COUNT,VERSION_ID,PAGE_BEGIN_TIME,PAGE_END_TIME,RECORD_FILE,RECORD_START_TIME,RECORD_BEGIN_TIME,RECORD_END_TIME,PAGE_TIME,SURVEY_GUID) VALUES('{0}',{1},'{2}',{3},{4},{5},{6},{7},'{8}','{9}','{10}','{11}',{12},{13},{14},'{15}')", new object[]
			{
				surveySequence_0.SURVEY_ID,
				surveySequence_0.SEQUENCE_ID,
				surveySequence_0.PAGE_ID,
				surveySequence_0.CIRCLE_A_CURRENT,
				surveySequence_0.CIRCLE_A_COUNT,
				surveySequence_0.CIRCLE_B_CURRENT,
				surveySequence_0.CIRCLE_B_COUNT,
				surveySequence_0.VERSION_ID,
				surveySequence_0.PAGE_BEGIN_TIME,
				surveySequence_0.PAGE_END_TIME,
				surveySequence_0.RECORD_FILE,
				surveySequence_0.RECORD_START_TIME,
				surveySequence_0.RECORD_BEGIN_TIME,
				surveySequence_0.RECORD_END_TIME,
				surveySequence_0.PAGE_TIME,
				surveySequence_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveySequence surveySequence_0)
		{
			string string_ = string.Format("UPDATE SurveySequence SET SURVEY_ID = '{1}',SEQUENCE_ID = {2},PAGE_ID = '{3}',CIRCLE_A_CURRENT = {4},CIRCLE_A_COUNT = {5},CIRCLE_B_CURRENT = {6},CIRCLE_B_COUNT = {7},VERSION_ID = {8},PAGE_BEGIN_TIME = '{9}',PAGE_END_TIME = '{10}',RECORD_FILE = '{11}',RECORD_START_TIME = '{12}',RECORD_BEGIN_TIME = {13},RECORD_END_TIME = {14},PAGE_TIME = {15},SURVEY_GUID = '{16}' WHERE ID = {0}", new object[]
			{
				surveySequence_0.ID,
				surveySequence_0.SURVEY_ID,
				surveySequence_0.SEQUENCE_ID,
				surveySequence_0.PAGE_ID,
				surveySequence_0.CIRCLE_A_CURRENT,
				surveySequence_0.CIRCLE_A_COUNT,
				surveySequence_0.CIRCLE_B_CURRENT,
				surveySequence_0.CIRCLE_B_COUNT,
				surveySequence_0.VERSION_ID,
				surveySequence_0.PAGE_BEGIN_TIME,
				surveySequence_0.PAGE_END_TIME,
				surveySequence_0.RECORD_FILE,
				surveySequence_0.RECORD_START_TIME,
				surveySequence_0.RECORD_BEGIN_TIME,
				surveySequence_0.RECORD_END_TIME,
				surveySequence_0.PAGE_TIME,
				surveySequence_0.SURVEY_GUID
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveySequence surveySequence_0)
		{
			string string_ = string.Format("DELETE FROM SurveySequence WHERE ID ={0}", surveySequence_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveySequence";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 17;
			string[] array = new string[17];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "问卷编号";
				array[2] = "问卷序列号";
				array[3] = "问卷页ID";
				array[4] = "A 层当前的循环数";
				array[5] = "A 层循环总数";
				array[6] = "B 层当前的循环数";
				array[7] = "B 层循环总数";
				array[8] = "路由版本编号";
				array[9] = "进入问题页面时间";
				array[10] = "离开问题页面时间";
				array[11] = "问题页对应录音文件";
				array[12] = "对应录音文件的开始时间";
				array[13] = "问题页录音开始 秒数";
				array[14] = "问题页录音结束 秒数";
				array[15] = "问题页使用时长 秒数";
				array[16] = "GUID 全球唯一码";
			}
			else
			{
				array[0] = "ID";
				array[1] = "SURVEY_ID";
				array[2] = "SEQUENCE_ID";
				array[3] = "PAGE_ID";
				array[4] = "CIRCLE_A_CURRENT";
				array[5] = "CIRCLE_A_COUNT";
				array[6] = "CIRCLE_B_CURRENT";
				array[7] = "CIRCLE_B_COUNT";
				array[8] = "VERSION_ID";
				array[9] = "PAGE_BEGIN_TIME";
				array[10] = "PAGE_END_TIME";
				array[11] = "RECORD_FILE";
				array[12] = "RECORD_START_TIME";
				array[13] = "RECORD_BEGIN_TIME";
				array[14] = "RECORD_END_TIME";
				array[15] = "PAGE_TIME";
				array[16] = "SURVEY_GUID";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveySequence> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveySequence surveySequence in list_0)
			{
				array[num, 0] = surveySequence.ID.ToString();
				array[num, 1] = surveySequence.SURVEY_ID;
				array[num, 2] = surveySequence.SEQUENCE_ID.ToString();
				array[num, 3] = surveySequence.PAGE_ID;
				array[num, 4] = surveySequence.CIRCLE_A_CURRENT.ToString();
				array[num, 5] = surveySequence.CIRCLE_A_COUNT.ToString();
				array[num, 6] = surveySequence.CIRCLE_B_CURRENT.ToString();
				array[num, 7] = surveySequence.CIRCLE_B_COUNT.ToString();
				array[num, 8] = surveySequence.VERSION_ID.ToString();
				array[num, 9] = surveySequence.PAGE_BEGIN_TIME.ToString();
				array[num, 10] = surveySequence.PAGE_END_TIME.ToString();
				array[num, 11] = surveySequence.RECORD_FILE;
				array[num, 12] = surveySequence.RECORD_START_TIME.ToString();
				array[num, 13] = surveySequence.RECORD_BEGIN_TIME.ToString();
				array[num, 14] = surveySequence.RECORD_END_TIME.ToString();
				array[num, 15] = surveySequence.PAGE_TIME.ToString();
				array[num, 16] = surveySequence.SURVEY_GUID;
				num++;
			}
			return array;
		}

		public SurveySequence GetBySequenceID(string string_0, int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveySequence WHERE SURVEY_ID='{0}' AND SEQUENCE_ID ={1}", string_0, int_0);
			return this.GetBySql(string_);
		}

		public List<SurveySequence> GetListBySurveyId(string string_0)
		{
			string string_ = string.Format("SELECT * FROM SurveySequence WHERE SURVEY_ID='{0}' ORDER BY ID", string_0);
			return this.GetListBySql(string_);
		}

		public void DeleteBySequenceID(string string_0, int int_0)
		{
			string string_ = string.Format("DELETE FROM SurveySequence WHERE SURVEY_ID='{0}' AND SEQUENCE_ID ={1}", string_0, int_0);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void UpdateNext(SurveySequence surveySequence_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveySequence WHERE SURVEY_ID='{0}' AND SEQUENCE_ID ={1}", surveySequence_0.SURVEY_ID, surveySequence_0.SEQUENCE_ID);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			if (num <= 0)
			{
				this.Add(surveySequence_0);
			}
			else
			{
				this.UpdateBySequenceID(surveySequence_0);
			}
		}

		public void UpdateBySequenceID(SurveySequence surveySequence_0)
		{
			string string_ = string.Format("UPDATE SurveySequence SET PAGE_ID = '{3}',CIRCLE_A_CURRENT = {4},CIRCLE_A_COUNT = {5},CIRCLE_B_CURRENT = {6},CIRCLE_B_COUNT = {7},VERSION_ID = {8},PAGE_BEGIN_TIME = '{9}',PAGE_END_TIME = '{10}',RECORD_FILE = '{11}',RECORD_START_TIME = '{12}',RECORD_BEGIN_TIME = {13},RECORD_END_TIME = {14},PAGE_TIME = {15} WHERE SURVEY_ID = '{1}' AND SEQUENCE_ID = {2}", new object[]
			{
				surveySequence_0.ID,
				surveySequence_0.SURVEY_ID,
				surveySequence_0.SEQUENCE_ID,
				surveySequence_0.PAGE_ID,
				surveySequence_0.CIRCLE_A_CURRENT,
				surveySequence_0.CIRCLE_A_COUNT,
				surveySequence_0.CIRCLE_B_CURRENT,
				surveySequence_0.CIRCLE_B_COUNT,
				surveySequence_0.VERSION_ID,
				surveySequence_0.PAGE_BEGIN_TIME,
				surveySequence_0.PAGE_END_TIME,
				surveySequence_0.RECORD_FILE,
				surveySequence_0.RECORD_START_TIME,
				surveySequence_0.RECORD_BEGIN_TIME,
				surveySequence_0.RECORD_END_TIME,
				surveySequence_0.PAGE_TIME
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public SurveySequence GetAudioByPageId(string string_0, int int_0, string string_1)
		{
			string string_2 = "";
			string text = "0";
			int num = int_0;
			if (int_0 == 0)
			{
				string_2 = string.Format("SELECT SEQUENCE_ID FROM SurveySequence WHERE SURVEY_ID='{0}' AND PAGE_ID ='{1}' AND SEQUENCE_ID <>0 ", string_0, string_1);
				text = this.dbprovider_0.ExecuteScalarString(string_2);
				if (text != "" && text != null)
				{
					num = int.Parse(text);
				}
			}
			SurveySequence result;
			if (num == 0)
			{
				SurveySequence surveySequence = new SurveySequence();
				result = surveySequence;
			}
			else
			{
				string_2 = string.Format("SELECT * FROM SurveySequence WHERE SURVEY_ID='{0}' AND SEQUENCE_ID ={1}", string_0, num);
				result = this.GetBySql(string_2);
			}
			return result;
		}

		public void DeleteOneSurvey(string string_0, string string_1)
		{
			string string_2 = string.Format("delete from SurveySequence where SURVEY_ID='{0}'", string_0);
			this.dbprovider_0.ExecuteNonQuery(string_2);
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
