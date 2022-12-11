using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections.Generic;
using System.Text;
namespace Sunlike.Business.Data
{
	/// <summary>
    /// 营销费用规则
	/// </summary>
	public class DbMe_Role: Sunlike.Business.Data.DbObject
	{
		#region 构造函数
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
		public DbMe_Role(string connStr):base(connStr)
		{
		}
		#endregion

        #region 取得营销费用规则
        /// <summary>
        /// 取得营销费用规则
		/// </summary>
        /// <param name="sqlWhere"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string sqlWhere)
		{
            string _sqlStr = "SELECT ITM,IDX1,BDATE,EDATE,PRD_NO,IDX_NO,AREA_NO,CUS_NO,AMTN_ME,PRD_MARK FROM ME_ROLE ";
            if (!String.IsNullOrEmpty(sqlWhere))
			{
                _sqlStr += " WHERE " + sqlWhere;
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "ME_ROLE" });			
			return _ds;
		}
		#endregion		
	
        #region 判断重复规则
        /// <summary>
        /// 判断是否有重复的规则存在,返回重复规则的ITM。
        /// </summary>
        /// <param name="Ht"></param>
        /// <returns></returns>
        public string GetExistMeRoleItm(Dictionary<String, Object> Ht)
        {
            string _IDX1 = Ht["IDX1"].ToString();//费用项目
            string _bDate = Convert.ToDateTime(Ht["BDATE"]).ToString("yyyy-MM-dd");//开始时间
            string _eDate = String.Empty;
            if (!string.IsNullOrEmpty(Ht["EDATE"].ToString()))
            {
                _eDate = Convert.ToDateTime(Ht["EDATE"]).ToString("yyyy-MM-dd");//结束时间
            }
            string _area = Ht["AREA_NO"].ToString();//区域
            string _cus = Ht["CUS_NO"].ToString();//客户
            string _idx = Ht["IDX_NO"].ToString();//中类
            string _prdNo = Ht["PRD_NO"].ToString();//货品编号
            string _prdMark = Ht["PRD_MARK"].ToString();//货品特征

            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[8];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@IDX1", SqlDbType.VarChar, 20);
            _spc[0].Value = _IDX1;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@BDATE", SqlDbType.DateTime);
            _spc[1].Value = Convert.ToDateTime(_bDate);
            _spc[2] = new System.Data.SqlClient.SqlParameter("@EDATE", SqlDbType.DateTime);
            if (!String.IsNullOrEmpty(_eDate))
            {
                _spc[2].Value = Convert.ToDateTime(_eDate);
            }
            else
            {
                _spc[2].Value = System.DBNull.Value;
            }
            _spc[3] = new System.Data.SqlClient.SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _spc[3].Value = _prdNo;
            _spc[4] = new System.Data.SqlClient.SqlParameter("@PRD_MARK", SqlDbType.VarChar, 40);
            _spc[4].Value = _prdMark;
            _spc[5] = new System.Data.SqlClient.SqlParameter("@IDX_NO", SqlDbType.VarChar, 10);
            _spc[5].Value = _idx;
            _spc[6] = new System.Data.SqlClient.SqlParameter("@AREA_NO", SqlDbType.VarChar, 8);
            _spc[6].Value = _area;
            _spc[7] = new System.Data.SqlClient.SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _spc[7].Value = _cus;

            StringBuilder _sql = new StringBuilder("SELECT * FROM ME_ROLE WHERE IDX1=@IDX1");
            if (string.IsNullOrEmpty(_eDate))
            {
                _sql.Append(" AND ((BDATE>=@BDATE) or (BDATE<=@BDATE and EDATE >= @BDATE) or (EDATE is null))");
            }
            else
            {
                _sql.Append(" AND ((BDATE>=@BDATE AND BDATE<=@EDATE) ");
                _sql.Append(" or (EDATE>=@BDATE AND EDATE<=@EDATE) ");
                _sql.Append(" or (BDATE<=@BDATE AND EDATE>=@BDATE) ");
                _sql.Append(" or (BDATE<=@EDATE AND EDATE is null))");
            }
            if (!String.IsNullOrEmpty(_area))
            {
                _sql.Append(" AND isnull(AREA_NO,'')=@AREA_NO ");
            }
            else
            {
                _sql.Append(" AND isnull(AREA_NO,'')='' ");
            }
            if (!String.IsNullOrEmpty(_cus))
            {
                _sql.Append(" AND isnull(CUS_NO,'')=@CUS_NO ");
            }
            else
            {
                _sql.Append(" AND isnull(CUS_NO,'')='' ");
            }
            if (!String.IsNullOrEmpty(_idx))
            {
                _sql.Append(" AND isnull(IDX_NO,'')=@IDX_NO ");
            }
            else
            {
                _sql.Append(" AND isnull(IDX_NO,'')='' ");
            }
            if (!String.IsNullOrEmpty(_prdNo))
            {
                _sql.Append(" AND isnull(PRD_NO,'')=@PRD_NO ");
            }
            else
            {
                _sql.Append(" AND isnull(PRD_NO,'')='' ");
            }
            if (!String.IsNullOrEmpty(_prdMark))
            {
                _sql.Append(" AND isnull(PRD_MARK,'')=@PRD_MARK ");
            }
            else
            {
                _sql.Append(" AND isnull(PRD_MARK,'')='' ");
            }
            if (Ht.ContainsKey("ITM"))
            {
                _sql.Append(" AND ITM<>" + Convert.ToInt64(Ht["ITM"]) + "");
            }
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql.ToString(), _ds, new string[] { "ME_ROLE" }, _spc);
            if (_ds.Tables["ME_ROLE"].Rows.Count > 0)
            {
                return _ds.Tables["ME_ROLE"].Rows[0]["ITM"].ToString();
            }
            return null;
        }
        #endregion

        #region 取最大项次
        /// <summary>
        /// 取最大项次
        /// </summary>
        /// <returns></returns>
        public int GetMaxItm()
        {
            string _sql = "SELECT ISNULL(MAX(ITM),0)+1 FROM ME_ROLE";
            return Convert.ToInt16(ExecuteScalar(_sql, null));
        }
        #endregion
    }
}
