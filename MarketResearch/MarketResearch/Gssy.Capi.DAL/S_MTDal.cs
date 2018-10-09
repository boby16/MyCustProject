using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class S_MTDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM S_MT WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public S_MT GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM S_MT WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public S_MT GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			S_MT s_MT = new S_MT();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					s_MT.ID = Convert.ToInt32(dataReader["ID"]);
					s_MT.MT_QUESTION = dataReader["MT_QUESTION"].ToString();
					s_MT.QUESTION_NAME = dataReader["QUESTION_NAME"].ToString();
					s_MT.QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]);
					s_MT.GROUP_LEVEL = dataReader["GROUP_LEVEL"].ToString();
					s_MT.GROUP_CODEA = dataReader["GROUP_CODEA"].ToString();
					s_MT.GROUP_CODEB = dataReader["GROUP_CODEB"].ToString();
					s_MT.GROUP_PAGE_TYPE = Convert.ToInt32(dataReader["GROUP_PAGE_TYPE"]);
				}
			}
			return s_MT;
		}

		public List<S_MT> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<S_MT> list = new List<S_MT>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new S_MT
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						MT_QUESTION = dataReader["MT_QUESTION"].ToString(),
						QUESTION_NAME = dataReader["QUESTION_NAME"].ToString(),
						QUESTION_TYPE = Convert.ToInt32(dataReader["QUESTION_TYPE"]),
						GROUP_LEVEL = dataReader["GROUP_LEVEL"].ToString(),
						GROUP_CODEA = dataReader["GROUP_CODEA"].ToString(),
						GROUP_CODEB = dataReader["GROUP_CODEB"].ToString(),
						GROUP_PAGE_TYPE = Convert.ToInt32(dataReader["GROUP_PAGE_TYPE"])
					});
				}
			}
			return list;
		}

		public List<S_MT> GetList()
		{
			string string_ = "SELECT * FROM S_MT ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(S_MT s_MT_0)
		{
			string string_ = string.Format("INSERT INTO S_MT(MT_QUESTION,QUESTION_NAME,QUESTION_TYPE,GROUP_LEVEL,GROUP_CODEA,GROUP_CODEB,GROUP_PAGE_TYPE) VALUES('{0}','{1}',{2},'{3}','{4}','{5}',{6})", new object[]
			{
				s_MT_0.MT_QUESTION,
				s_MT_0.QUESTION_NAME,
				s_MT_0.QUESTION_TYPE,
				s_MT_0.GROUP_LEVEL,
				s_MT_0.GROUP_CODEA,
				s_MT_0.GROUP_CODEB,
				s_MT_0.GROUP_PAGE_TYPE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(S_MT s_MT_0)
		{
			string string_ = string.Format("UPDATE S_MT SET MT_QUESTION = '{1}',QUESTION_NAME = '{2}',QUESTION_TYPE = {3},GROUP_LEVEL = '{4}',GROUP_CODEA = '{5}',GROUP_CODEB = '{6}',GROUP_PAGE_TYPE = {7} WHERE ID = {0}", new object[]
			{
				s_MT_0.ID,
				s_MT_0.MT_QUESTION,
				s_MT_0.QUESTION_NAME,
				s_MT_0.QUESTION_TYPE,
				s_MT_0.GROUP_LEVEL,
				s_MT_0.GROUP_CODEA,
				s_MT_0.GROUP_CODEB,
				s_MT_0.GROUP_PAGE_TYPE
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(S_MT s_MT_0)
		{
			string string_ = string.Format("DELETE FROM S_MT WHERE ID ={0}", s_MT_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM S_MT";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 8;
			string[] array = new string[8];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "MT题号";
				array[2] = "问题编号";
				array[3] = "题型";
				array[4] = "循环题组级别";
				array[5] = "循环题组父循环代码A层";
				array[6] = "循环题组父循环代码B层";
				array[7] = "问题在循环题组中的位置类型";
			}
			else
			{
				array[0] = "ID";
				array[1] = "MT_QUESTION";
				array[2] = "QUESTION_NAME";
				array[3] = "QUESTION_TYPE";
				array[4] = "GROUP_LEVEL";
				array[5] = "GROUP_CODEA";
				array[6] = "GROUP_CODEB";
				array[7] = "GROUP_PAGE_TYPE";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<S_MT> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (S_MT s_MT in list_0)
			{
				array[num, 0] = s_MT.ID.ToString();
				array[num, 1] = s_MT.MT_QUESTION;
				array[num, 2] = s_MT.QUESTION_NAME;
				array[num, 3] = s_MT.QUESTION_TYPE.ToString();
				array[num, 4] = s_MT.GROUP_LEVEL;
				array[num, 5] = s_MT.GROUP_CODEA;
				array[num, 6] = s_MT.GROUP_CODEB;
				array[num, 7] = s_MT.GROUP_PAGE_TYPE.ToString();
				num++;
			}
			return array;
		}

		private DBProvider dbprovider_0 = new DBProvider(2);
	}
}
