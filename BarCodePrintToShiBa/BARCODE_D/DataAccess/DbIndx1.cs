using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// 费用项目资料库
	/// </summary>
	public class DbIndx1: Sunlike.Business.Data.DbObject
	{
		#region 构造函数
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
		public DbIndx1(string connStr):base(connStr)
		{
		}
		#endregion

		#region 取得费用项目
		/// <summary>
		/// 取得费用项目
		/// </summary>
        /// <param name="sqlWhere"></param>
		/// <returns></returns>
        public SunlikeDataSet GetData_Indx1(string sqlWhere)
		{
			string _sqlStr = "SELECT IDX1,NAME,ACC_NO,CHK_COMP,CHK_SUM FROM INDX1 ";
            if (!String.IsNullOrEmpty(sqlWhere))
			{
                _sqlStr += " WHERE " + sqlWhere;
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,_ds,new string[] {"INDX1"});			
			return _ds;
		}
              /// <summary>
              /// 取得费用项目
              /// </summary>
              /// <param name="idx1"></param>
              /// <returns></returns>
        public SunlikeDataSet GetData(string idx1)
        {
            string _sqlStr = "SELECT IDX1,NAME,ACC_NO,CHK_COMP,CHK_SUM FROM INDX1 WHERE IDX1 = @IDX1 ";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@IDX1", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = idx1;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "INDX1" }, _sqlPara);
            return _ds;
        }
		#endregion	

                    #region 检查是否可以删除
        /// <summary>
        /// 检查是否可以删除
        /// </summary>
        /// <param name="idx1">费用代号</param>
        /// <returns>0:可以删除;1:不可以删除;</returns>
        public int CanDelete(string idx1)
        {
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@idx1", SqlDbType.VarChar, 12);
            _aryPt[0].Value = idx1;
            string _sql = "select top 1 1 from ME_ROLE where IDX1=@idx1";
            SunlikeDataSet _ds = SunlikeDataSet.ConvertTo(this.ExecuteDataset(_sql, _aryPt));
            if (_ds.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }
        #endregion

	}
}
