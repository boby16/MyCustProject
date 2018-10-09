using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyDetailDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM SurveyDetail WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public SurveyDetail GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM SurveyDetail WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public SurveyDetail GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			SurveyDetail surveyDetail = new SurveyDetail();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					surveyDetail.ID = Convert.ToInt32(dataReader["ID"]);
					surveyDetail.DETAIL_ID = dataReader["DETAIL_ID"].ToString();
					surveyDetail.CODE = dataReader["CODE"].ToString();
					surveyDetail.CODE_TEXT = dataReader["CODE_TEXT"].ToString();
					surveyDetail.IS_OTHER = Convert.ToInt32(dataReader["IS_OTHER"]);
					surveyDetail.INNER_ORDER = Convert.ToInt32(dataReader["INNER_ORDER"]);
					surveyDetail.PARENT_CODE = dataReader["PARENT_CODE"].ToString();
					surveyDetail.RANDOM_BASE = Convert.ToInt32(dataReader["RANDOM_BASE"]);
					surveyDetail.RANDOM_SET = Convert.ToInt32(dataReader["RANDOM_SET"]);
					surveyDetail.RANDOM_FIX = Convert.ToInt32(dataReader["RANDOM_FIX"]);
					surveyDetail.EXTEND_1 = dataReader["EXTEND_1"].ToString();
					surveyDetail.EXTEND_2 = dataReader["EXTEND_2"].ToString();
					surveyDetail.EXTEND_3 = dataReader["EXTEND_3"].ToString();
					surveyDetail.EXTEND_4 = dataReader["EXTEND_4"].ToString();
					surveyDetail.EXTEND_5 = dataReader["EXTEND_5"].ToString();
					surveyDetail.EXTEND_6 = dataReader["EXTEND_6"].ToString();
					surveyDetail.EXTEND_7 = dataReader["EXTEND_7"].ToString();
					surveyDetail.EXTEND_8 = dataReader["EXTEND_8"].ToString();
					surveyDetail.EXTEND_9 = dataReader["EXTEND_9"].ToString();
					surveyDetail.EXTEND_10 = dataReader["EXTEND_10"].ToString();
				}
			}
			return surveyDetail;
		}

		public List<SurveyDetail> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<SurveyDetail> list = new List<SurveyDetail>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new SurveyDetail
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						DETAIL_ID = dataReader["DETAIL_ID"].ToString(),
						CODE = dataReader["CODE"].ToString(),
						CODE_TEXT = dataReader["CODE_TEXT"].ToString(),
						IS_OTHER = Convert.ToInt32(dataReader["IS_OTHER"]),
						INNER_ORDER = Convert.ToInt32(dataReader["INNER_ORDER"]),
						PARENT_CODE = dataReader["PARENT_CODE"].ToString(),
						RANDOM_BASE = Convert.ToInt32(dataReader["RANDOM_BASE"]),
						RANDOM_SET = Convert.ToInt32(dataReader["RANDOM_SET"]),
						RANDOM_FIX = Convert.ToInt32(dataReader["RANDOM_FIX"]),
						EXTEND_1 = dataReader["EXTEND_1"].ToString(),
						EXTEND_2 = dataReader["EXTEND_2"].ToString(),
						EXTEND_3 = dataReader["EXTEND_3"].ToString(),
						EXTEND_4 = dataReader["EXTEND_4"].ToString(),
						EXTEND_5 = dataReader["EXTEND_5"].ToString(),
						EXTEND_6 = dataReader["EXTEND_6"].ToString(),
						EXTEND_7 = dataReader["EXTEND_7"].ToString(),
						EXTEND_8 = dataReader["EXTEND_8"].ToString(),
						EXTEND_9 = dataReader["EXTEND_9"].ToString(),
						EXTEND_10 = dataReader["EXTEND_10"].ToString()
					});
				}
			}
			return list;
		}

		public List<SurveyDetail> GetList()
		{
			string string_ = "SELECT * FROM SurveyDetail ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(SurveyDetail surveyDetail_0)
		{
			string string_ = string.Format("INSERT INTO SurveyDetail(DETAIL_ID,CODE,CODE_TEXT,IS_OTHER,INNER_ORDER,PARENT_CODE,RANDOM_BASE,RANDOM_SET,RANDOM_FIX,EXTEND_1,EXTEND_2,EXTEND_3,EXTEND_4,EXTEND_5,EXTEND_6,EXTEND_7,EXTEND_8,EXTEND_9,EXTEND_10) VALUES('{0}','{1}','{2}',{3},{4},'{5}',{6},{7},{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", new object[]
			{
				surveyDetail_0.DETAIL_ID,
				surveyDetail_0.CODE,
				surveyDetail_0.CODE_TEXT,
				surveyDetail_0.IS_OTHER,
				surveyDetail_0.INNER_ORDER,
				surveyDetail_0.PARENT_CODE,
				surveyDetail_0.RANDOM_BASE,
				surveyDetail_0.RANDOM_SET,
				surveyDetail_0.RANDOM_FIX,
				surveyDetail_0.EXTEND_1,
				surveyDetail_0.EXTEND_2,
				surveyDetail_0.EXTEND_3,
				surveyDetail_0.EXTEND_4,
				surveyDetail_0.EXTEND_5,
				surveyDetail_0.EXTEND_6,
				surveyDetail_0.EXTEND_7,
				surveyDetail_0.EXTEND_8,
				surveyDetail_0.EXTEND_9,
				surveyDetail_0.EXTEND_10
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(SurveyDetail surveyDetail_0)
		{
			string string_ = string.Format("UPDATE SurveyDetail SET DETAIL_ID = '{1}',CODE = '{2}',CODE_TEXT = '{3}',IS_OTHER = {4},INNER_ORDER = {5},PARENT_CODE = '{6}',RANDOM_BASE = {7},RANDOM_SET = {8},RANDOM_FIX = {9},EXTEND_1 = '{10}',EXTEND_2 = '{11}',EXTEND_3 = '{12}',EXTEND_4 = '{13}',EXTEND_5 = '{14}',EXTEND_6 = '{15}',EXTEND_7 = '{16}',EXTEND_8 = '{17}',EXTEND_9 = '{18}',EXTEND_10 = '{19}' WHERE ID = {0}", new object[]
			{
				surveyDetail_0.ID,
				surveyDetail_0.DETAIL_ID,
				surveyDetail_0.CODE,
				surveyDetail_0.CODE_TEXT,
				surveyDetail_0.IS_OTHER,
				surveyDetail_0.INNER_ORDER,
				surveyDetail_0.PARENT_CODE,
				surveyDetail_0.RANDOM_BASE,
				surveyDetail_0.RANDOM_SET,
				surveyDetail_0.RANDOM_FIX,
				surveyDetail_0.EXTEND_1,
				surveyDetail_0.EXTEND_2,
				surveyDetail_0.EXTEND_3,
				surveyDetail_0.EXTEND_4,
				surveyDetail_0.EXTEND_5,
				surveyDetail_0.EXTEND_6,
				surveyDetail_0.EXTEND_7,
				surveyDetail_0.EXTEND_8,
				surveyDetail_0.EXTEND_9,
				surveyDetail_0.EXTEND_10
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(SurveyDetail surveyDetail_0)
		{
			string string_ = string.Format("DELETE FROM SurveyDetail WHERE ID ={0}", surveyDetail_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM SurveyDetail";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 20;
			string[] array = new string[20];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "Define表父编码";
				array[2] = "编码";
				array[3] = "编码文本";
				array[4] = "是否其他";
				array[5] = "基本内部顺序";
				array[6] = "多级的父编码";
				array[7] = "随机题分组基础";
				array[8] = "随机题分组";
				array[9] = "是否随机题的固定题";
				array[10] = "扩展内容 1";
				array[11] = "扩展内容 2";
				array[12] = "扩展内容 3";
				array[13] = "扩展内容 4";
				array[14] = "扩展内容 5";
				array[15] = "扩展内容 6";
				array[16] = "扩展内容 7";
				array[17] = "扩展内容 8";
				array[18] = "扩展内容 9";
				array[19] = "扩展内容 10";
			}
			else
			{
				array[0] = "ID";
				array[1] = "DETAIL_ID";
				array[2] = "CODE";
				array[3] = "CODE_TEXT";
				array[4] = "IS_OTHER";
				array[5] = "INNER_ORDER";
				array[6] = "PARENT_CODE";
				array[7] = "RANDOM_BASE";
				array[8] = "RANDOM_SET";
				array[9] = "RANDOM_FIX";
				array[10] = "EXTEND_1";
				array[11] = "EXTEND_2";
				array[12] = "EXTEND_3";
				array[13] = "EXTEND_4";
				array[14] = "EXTEND_5";
				array[15] = "EXTEND_6";
				array[16] = "EXTEND_7";
				array[17] = "EXTEND_8";
				array[18] = "EXTEND_9";
				array[19] = "EXTEND_10";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<SurveyDetail> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (SurveyDetail surveyDetail in list_0)
			{
				array[num, 0] = surveyDetail.ID.ToString();
				array[num, 1] = surveyDetail.DETAIL_ID;
				array[num, 2] = surveyDetail.CODE;
				array[num, 3] = surveyDetail.CODE_TEXT;
				array[num, 4] = surveyDetail.IS_OTHER.ToString();
				array[num, 5] = surveyDetail.INNER_ORDER.ToString();
				array[num, 6] = surveyDetail.PARENT_CODE;
				array[num, 7] = surveyDetail.RANDOM_BASE.ToString();
				array[num, 8] = surveyDetail.RANDOM_SET.ToString();
				array[num, 9] = surveyDetail.RANDOM_FIX.ToString();
				array[num, 10] = surveyDetail.EXTEND_1;
				array[num, 11] = surveyDetail.EXTEND_2;
				array[num, 12] = surveyDetail.EXTEND_3;
				array[num, 13] = surveyDetail.EXTEND_4;
				array[num, 14] = surveyDetail.EXTEND_5;
				array[num, 15] = surveyDetail.EXTEND_6;
				array[num, 16] = surveyDetail.EXTEND_7;
				array[num, 17] = surveyDetail.EXTEND_8;
				array[num, 18] = surveyDetail.EXTEND_9;
				array[num, 19] = surveyDetail.EXTEND_10;
				num++;
			}
			return array;
		}

		public List<SurveyDetail> GetDetails(string string_0)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			string string_ = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' ORDER BY INNER_ORDER", string_0);
			return this.GetListBySql(string_);
		}

		public List<SurveyDetail> GetDetails(string string_0, out string string_1)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			string string_2 = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' ORDER BY INNER_ORDER", string_0);
			list = this.GetListBySql(string_2);
			string_1 = "";
			foreach (SurveyDetail surveyDetail in list)
			{
				if (surveyDetail.IS_OTHER == 1)
				{
					string_1 = surveyDetail.CODE;
				}
			}
			return list;
		}

		public List<SurveyDetail> GetDetails(string string_0, out string string_1, out string string_2, out string string_3, out string string_4, out string string_5)
		{
			List<SurveyDetail> list = new List<SurveyDetail>();
			string string_6 = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' ORDER BY INNER_ORDER", string_0);
			list = this.GetListBySql(string_6);
			string_1 = "";
			string_2 = "";
			string_3 = "";
			string_4 = "";
			string_5 = "";
			foreach (SurveyDetail surveyDetail in list)
			{
				if (surveyDetail.IS_OTHER == 1 || surveyDetail.IS_OTHER == 3)
				{
					string_1 = surveyDetail.CODE;
					string_2 = surveyDetail.CODE_TEXT;
				}
				if (surveyDetail.IS_OTHER == 2 || surveyDetail.IS_OTHER == 3)
				{
					string_3 = surveyDetail.CODE;
				}
				if (surveyDetail.IS_OTHER == 11 || surveyDetail.IS_OTHER == 13)
				{
					string_4 = surveyDetail.CODE;
					string_5 = surveyDetail.CODE_TEXT;
				}
			}
			return list;
		}

		public List<SurveyDetail> GetList(string string_0, string string_1)
		{
			string string_2;
			if (string_1 == "")
			{
				string_2 = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' ORDER BY INNER_ORDER", string_0);
			}
			else
			{
				string_2 = string.Format("select * from SurveyDetail where DETAIL_ID='{0}' AND PARENT_CODE='{1}' ORDER BY INNER_ORDER", string_0, string_1);
			}
			return this.GetListBySql(string_2);
		}

		public string GetCodeText(string string_0, string string_1)
		{
			string text = "";
			string string_2 = string.Format("select code_text from surveydetail where detail_id='{0}' and code ='{1}'", string_0, string_1);
			return this.dbprovider_0.ExecuteScalarString(string_2);
		}

		public string GetCodeText(string string_0, string string_1, string string_2)
		{
			string text = "";
			string string_3 = string.Format("select Extend_3 from surveydetail where detail_id='{0}' and parent_code='{1}' and code ='{2}'", string_0, string_2, string_1);
			return this.dbprovider_0.ExecuteScalarString(string_3);
		}

		public string GetCodeTextExtend(string string_0, string string_1, int int_0)
		{
			string text = "";
			string string_2 = string.Format("select Extend_{2} from surveydetail where detail_id='{0}' and code ='{1}'", string_0, string_1, int_0.ToString());
			return this.dbprovider_0.ExecuteScalarString(string_2);
		}

		public SurveyDetail GetOne(string string_0, string string_1)
		{
			string string_2 = string.Format("select * from SurveyDetail where Detail_Id='{0}' and Code='{1}'", string_0, string_1);
			return this.GetBySql(string_2);
		}

		public SurveyDetail GetOne(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format("select * from SurveyDetail where detail_id='{0}' and parent_code='{1}' and code ='{2}'", string_0, string_2, string_1);
			return this.GetBySql(string_3);
		}

		public SurveyDetail GetOneByOrder(string string_0, int int_0)
		{
			string string_ = string.Format("select * from SurveyDetail where Detail_Id='{0}' and INNER_ORDER={1}", string_0, int_0.ToString());
			return this.GetBySql(string_);
		}

		public int GetDetailCount(string string_0)
		{
			string string_ = string.Format("select count(*) as nCount from SurveyDetail where Detail_Id='{0}'", string_0);
			string s = this.dbprovider_0.ExecuteScalarString(string_);
			return int.Parse(s);
		}

		public int GetExtendCount(string string_0, string string_1, int int_0)
		{
			string string_2 = string.Format("select count(*) as ncount from surveydetail where detail_id='{0}' and extend_1 ='{1}'", string_0, string_1);
			string s = this.dbprovider_0.ExecuteScalarString(string_2);
			return int.Parse(s);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
