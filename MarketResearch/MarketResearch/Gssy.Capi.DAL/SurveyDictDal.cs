using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyDictDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyDict WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyDict GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyDict WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyDict GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyDict surveyDict = new SurveyDict();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyDict.ID = Convert.ToInt32(dataReader["ID"]);
					surveyDict.CODE = dataReader["CODE"].ToString();
					surveyDict.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					surveyDict.CODE_TYPE = dataReader["CODE_TYPE"].ToString();
					surveyDict.PARENT_ID = dataReader["PARENT_ID"].ToString();
					surveyDict.INNER_ORDER = Convert.ToInt32(dataReader["INNER_ORDER"]);
					surveyDict.CODE_NOTE = dataReader["CODE_NOTE"].ToString();
					surveyDict.IS_ENABLE = Convert.ToInt32(dataReader["IS_ENABLE"]);
				}
			}
			return surveyDict;
		}

		public List<SurveyDict> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyDict> list = new List<SurveyDict>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyDict
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						CODE_TYPE = dataReader["CODE_TYPE"].ToString(),
						PARENT_ID = dataReader["PARENT_ID"].ToString(),
						INNER_ORDER = Convert.ToInt32(dataReader["INNER_ORDER"]),
						CODE_NOTE = dataReader["CODE_NOTE"].ToString(),
						IS_ENABLE = Convert.ToInt32(dataReader["IS_ENABLE"])
					});
				}
			}
			return list;
		}

		public List<SurveyDict> GetList()
		{
			string string_ = "SELECT * FROM SurveyDict ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyDict surveyDict_0)
		{
			string string_ = string.Format("INSERT INTO SurveyDict(CODE,CODE_TEXT,CODE_TYPE,PARENT_ID,INNER_ORDER,CODE_NOTE,IS_ENABLE) VALUES('{0}','{1}','{2}','{3}',{4},'{5}',{6})", new object[]
			{
				surveyDict_0.CODE,
				surveyDict_0.CODE_TEXT,
				surveyDict_0.CODE_TYPE,
				surveyDict_0.PARENT_ID,
				surveyDict_0.INNER_ORDER,
				surveyDict_0.CODE_NOTE,
				surveyDict_0.IS_ENABLE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyDict surveyDict_0)
		{
			string string_ = string.Format("UPDATE SurveyDict SET CODE = '{1}',CODE_TEXT = '{2}',CODE_TYPE = '{3}',PARENT_ID = '{4}',INNER_ORDER = {5},CODE_NOTE = '{6}',IS_ENABLE = {7} WHERE ID = {0}", new object[]
			{
				surveyDict_0.ID,
				surveyDict_0.CODE,
				surveyDict_0.CODE_TEXT,
				surveyDict_0.CODE_TYPE,
				surveyDict_0.PARENT_ID,
				surveyDict_0.INNER_ORDER,
				surveyDict_0.CODE_NOTE,
				surveyDict_0.IS_ENABLE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyDict surveyDict_0)
		{
			string string_ = string.Format("DELETE FROM SurveyDict WHERE ID ={0}", surveyDict_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyDict";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 8;
			string[] array = new string[8];
			if (bool_0)
			{
				array[0] = "主键 自动编号";
				array[1] = "自定义代码";
				array[2] = "名称";
				array[3] = "类型";
				array[4] = "父 id";
				array[5] = "内排序";
				array[6] = "内容说明";
				array[7] = "激活";
			}
			else
			{
				array[0] = "ID";
				array[1] = "CODE";
				array[2] = "CODE_TEXT";
				array[3] = "CODE_TYPE";
				array[4] = "PARENT_ID";
				array[5] = "INNER_ORDER";
				array[6] = "CODE_NOTE";
				array[7] = "IS_ENABLE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyDict> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyDict surveyDict in list_0)
			{
				array[num, 0] = surveyDict.ID.ToString();
				array[num, 1] = surveyDict.CODE;
				array[num, 2] = surveyDict.CODE_TEXT;
				array[num, 3] = surveyDict.CODE_TYPE;
				array[num, 4] = surveyDict.PARENT_ID;
				array[num, 5] = surveyDict.INNER_ORDER.ToString();
				array[num, 6] = surveyDict.CODE_NOTE;
				array[num, 7] = surveyDict.IS_ENABLE.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
