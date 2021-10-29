using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// ������Ŀ���Ͽ�
	/// </summary>
	public class DbIndx1: Sunlike.Business.Data.DbObject
	{
		#region ���캯��
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
		public DbIndx1(string connStr):base(connStr)
		{
		}
		#endregion

		#region ȡ�÷�����Ŀ
		/// <summary>
		/// ȡ�÷�����Ŀ
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
              /// ȡ�÷�����Ŀ
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

                    #region ����Ƿ����ɾ��
        /// <summary>
        /// ����Ƿ����ɾ��
        /// </summary>
        /// <param name="idx1">���ô���</param>
        /// <returns>0:����ɾ��;1:������ɾ��;</returns>
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
