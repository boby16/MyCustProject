using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbSETLEVEL.
	/// </summary>
	public class DbSETLEVEL : DbObject
	{
		/// <summary>
		/// 会员等级设定
		/// </summary>
		/// <param name="connectionString"></param>
		public DbSETLEVEL(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// 取会员等级资料
		/// </summary>
		/// <param name="sqlWhere"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataSqlWhere(string sqlWhere)
		{
			string _sql = "Select CARD_CLS,NAME,DIS_PER,BTH_DIS from POSCARDTP";
			if(sqlWhere != null && !String.IsNullOrEmpty(sqlWhere))
			{
				_sql += " where " + sqlWhere;
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null);
			_ds.Tables[0].TableName = "POSCARDTP";
			return _ds;
		}

		/// <summary>
		/// 取会员等级资料
		/// </summary>
		/// <param name="CardCls">等级代号</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string CardCls)
		{
			string _sql = "Select CARD_CLS,NAME,DIS_PER,BTH_DIS from POSCARDTP WHERE CARD_CLS=@CardCls";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[1];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@CardCls",SqlDbType.VarChar,20);
			_spc[0].Value = CardCls;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null,_spc);
			_ds.Tables[0].TableName = "POSCARDTP";
			return _ds;
		}

        /// <summary>
        /// 取会员等级升级申请资料
        /// </summary>
        /// <param name="itm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetReqData(int itm)
        {
            string _sql = "select ITM,CARD_NO,CARD_CLS,REQ_DD,REM,STATUS,CHK_MAN,CLS_DATE"
                + " from POSCARDTP_REQ"
                + " where ITM=@Itm";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@Itm", SqlDbType.Int);
            _spc[0].Value = itm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            FillDataset(_sql, _ds, new string[] { "POSCARDTP_REQ" }, _spc);
            return _ds;
        }

        /// <summary>
        /// 取会员等级申请最大项次
        /// </summary>
        /// <returns></returns>
        public int GetReqMaxItm()
        {
            int _result = 1;
            string _sql = "select isnull(max(ITM),0)+1 from POSCARDTP_REQ";
            object _rObj = this.ExecuteScalar(_sql);
            if (_rObj != null && !String.IsNullOrEmpty(_rObj.ToString()))
            {
                _result = Convert.ToInt32(_rObj);
            }
            return _result;
        }
	}
}
