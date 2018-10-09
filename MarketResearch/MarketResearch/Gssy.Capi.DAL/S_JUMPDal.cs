using System;
using System.Collections.Generic;
using System.Data;
using Gssy.Capi.Common;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class S_JUMPDal
	{
		public bool Exists(int int_0)
		{
			string string_ = string.Format("SELECT COUNT(*) FROM S_JUMP WHERE ID ={0}", int_0);
			int num = this.dbprovider_0.ExecuteScalarInt(string_);
			return num > 0;
		}

		public S_JUMP GetByID(int int_0)
		{
			string string_ = string.Format("SELECT * FROM S_JUMP WHERE ID ={0}", int_0);
			return this.GetBySql(string_);
		}

		public S_JUMP GetBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			S_JUMP s_JUMP = new S_JUMP();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					s_JUMP.ID = Convert.ToInt32(dataReader["ID"]);
					s_JUMP.PAGE_TEXT = dataReader["PAGE_TEXT"].ToString();
					s_JUMP.PAGE_VALUE = dataReader["PAGE_VALUE"].ToString();
					s_JUMP.PAGE_ID = dataReader["PAGE_ID"].ToString();
					s_JUMP.CIRCLE_A_CURRENT = Convert.ToInt32(dataReader["CIRCLE_A_CURRENT"]);
					s_JUMP.CIRCLE_A_COUNT = Convert.ToInt32(dataReader["CIRCLE_A_COUNT"]);
					s_JUMP.CIRCLE_B_CURRENT = Convert.ToInt32(dataReader["CIRCLE_B_CURRENT"]);
					s_JUMP.CIRCLE_B_COUNT = Convert.ToInt32(dataReader["CIRCLE_B_COUNT"]);
				}
			}
			return s_JUMP;
		}

		public List<S_JUMP> GetListBySql(string string_0)
		{
			IDataReader dataReader = this.dbprovider_0.ExecuteReader(string_0);
			List<S_JUMP> list = new List<S_JUMP>();
			using (dataReader)
			{
				while (dataReader.Read())
				{
					list.Add(new S_JUMP
					{
						ID = Convert.ToInt32(dataReader["ID"]),
						PAGE_TEXT = dataReader["PAGE_TEXT"].ToString(),
						PAGE_VALUE = dataReader["PAGE_VALUE"].ToString(),
						PAGE_ID = dataReader["PAGE_ID"].ToString(),
						CIRCLE_A_CURRENT = Convert.ToInt32(dataReader["CIRCLE_A_CURRENT"]),
						CIRCLE_A_COUNT = Convert.ToInt32(dataReader["CIRCLE_A_COUNT"]),
						CIRCLE_B_CURRENT = Convert.ToInt32(dataReader["CIRCLE_B_CURRENT"]),
						CIRCLE_B_COUNT = Convert.ToInt32(dataReader["CIRCLE_B_COUNT"])
					});
				}
			}
			return list;
		}

		public List<S_JUMP> GetList()
		{
			string string_ = "SELECT * FROM S_JUMP ORDER BY ID";
			return this.GetListBySql(string_);
		}

		public void Add(S_JUMP s_JUMP_0)
		{
			string string_ = string.Format("INSERT INTO S_JUMP(PAGE_TEXT,PAGE_VALUE,PAGE_ID,CIRCLE_A_CURRENT,CIRCLE_A_COUNT,CIRCLE_B_CURRENT,CIRCLE_B_COUNT) VALUES('{0}','{1}','{2}',{3},{4},{5},{6})", new object[]
			{
				s_JUMP_0.PAGE_TEXT,
				s_JUMP_0.PAGE_VALUE,
				s_JUMP_0.PAGE_ID,
				s_JUMP_0.CIRCLE_A_CURRENT,
				s_JUMP_0.CIRCLE_A_COUNT,
				s_JUMP_0.CIRCLE_B_CURRENT,
				s_JUMP_0.CIRCLE_B_COUNT
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Update(S_JUMP s_JUMP_0)
		{
			string string_ = string.Format("UPDATE S_JUMP SET PAGE_TEXT = '{1}',PAGE_VALUE = '{2}',PAGE_ID = '{3}',CIRCLE_A_CURRENT = {4},CIRCLE_A_COUNT = {5},CIRCLE_B_CURRENT = {6},CIRCLE_B_COUNT = {7} WHERE ID = {0}", new object[]
			{
				s_JUMP_0.ID,
				s_JUMP_0.PAGE_TEXT,
				s_JUMP_0.PAGE_VALUE,
				s_JUMP_0.PAGE_ID,
				s_JUMP_0.CIRCLE_A_CURRENT,
				s_JUMP_0.CIRCLE_A_COUNT,
				s_JUMP_0.CIRCLE_B_CURRENT,
				s_JUMP_0.CIRCLE_B_COUNT
			});
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Delete(S_JUMP s_JUMP_0)
		{
			string string_ = string.Format("DELETE FROM S_JUMP WHERE ID ={0}", s_JUMP_0.ID);
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public void Truncate()
		{
			string string_ = "DELETE FROM S_JUMP";
			this.dbprovider_0.ExecuteNonQuery(string_);
		}

		public string[] ExcelTitle(out int int_0, bool bool_0 = true)
		{
			int_0 = 8;
			string[] array = new string[8];
			if (bool_0)
			{
				array[0] = "自动编号";
				array[1] = "页说明";
				array[2] = "页值";
				array[3] = "页编号";
				array[4] = "A 层当前的循环数";
				array[5] = "A 层循环总数";
				array[6] = "B 层当前的循环数";
				array[7] = "B 层循环总数";
			}
			else
			{
				array[0] = "ID";
				array[1] = "PAGE_TEXT";
				array[2] = "PAGE_VALUE";
				array[3] = "PAGE_ID";
				array[4] = "CIRCLE_A_CURRENT";
				array[5] = "CIRCLE_A_COUNT";
				array[6] = "CIRCLE_B_CURRENT";
				array[7] = "CIRCLE_B_COUNT";
			}
			return array;
		}

		public string[,] ExcelContent(int int_0, List<S_JUMP> list_0, out int int_1)
		{
			int_1 = list_0.Count;
			string[,] array = new string[int_1, int_0];
			int num = 0;
			foreach (S_JUMP s_JUMP in list_0)
			{
				array[num, 0] = s_JUMP.ID.ToString();
				array[num, 1] = s_JUMP.PAGE_TEXT;
				array[num, 2] = s_JUMP.PAGE_VALUE;
				array[num, 3] = s_JUMP.PAGE_ID;
				array[num, 4] = s_JUMP.CIRCLE_A_CURRENT.ToString();
				array[num, 5] = s_JUMP.CIRCLE_A_COUNT.ToString();
				array[num, 6] = s_JUMP.CIRCLE_B_CURRENT.ToString();
				array[num, 7] = s_JUMP.CIRCLE_B_COUNT.ToString();
				num++;
			}
			return array;
		}

		public void ExecuteProceddure(string string_0)
		{
			this.dbprovider_0.ExecuteNonQuery(string_0);
		}

		private DBProvider dbprovider_0 = new DBProvider(1);

		private DBProvider dbprovider_1 = new DBProvider(2);
	}
}
