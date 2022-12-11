using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// Summary description for DbINVCommon.
    /// </summary>
    public class DbINVCommon : DbObject
    {
        /// <summary>
        /// 存货公用类
        /// </summary>
        /// <param name="connectionString">连接字串</param>
        public DbINVCommon(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// 取得原单单位
        /// </summary>
        /// <param name="_ht">所含内容：表名TableName,单据别栏位名称IdName,单号栏位名称NoName,追踪项次栏位名称ItmName,单据别OsID,单号OsNO,追踪项次KeyItm</param>
        /// <returns></returns>
        public string GetOldUnit(Hashtable _ht)
        {
            string _subSql = "";
            if (_ht.Contains("IdName") && !string.IsNullOrEmpty(_ht["IdName"].ToString()))
            {
                _subSql = " and '+ @IdName +'='''+@OsID+ ''' ";
            }
            if (_ht.Contains("KeyItm") && !string.IsNullOrEmpty(_ht["KeyItm"].ToString()))
            {
                _subSql += " and '+ @ItmName +'='''+ @KeyItm +''' ";
            }
            string _sql = string.Format("declare @SQL nvarchar(600);\nset @SQL= N'select UNIT from '+ @TableName + ' where  '+ @NoName +'='''+ @OsNo + ''' {0} ';\nexec sp_executesql @SQL", _subSql);

            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[7];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TableName", SqlDbType.VarChar, 20);
            _spc[0].Value = _ht["TableName"].ToString();
            _spc[1] = new System.Data.SqlClient.SqlParameter("@IdName", SqlDbType.VarChar, 20);
            _spc[1].Value = _ht["IdName"].ToString();
            _spc[2] = new System.Data.SqlClient.SqlParameter("@NoName", SqlDbType.VarChar, 20);
            _spc[2].Value = _ht["NoName"].ToString();
            _spc[3] = new System.Data.SqlClient.SqlParameter("@ItmName", SqlDbType.VarChar, 20);
            _spc[3].Value = _ht["ItmName"].ToString();
            _spc[4] = new System.Data.SqlClient.SqlParameter("@OsID", SqlDbType.Char, 2);
            _spc[4].Value = _ht["OsID"].ToString();
            _spc[5] = new System.Data.SqlClient.SqlParameter("@OsNo", SqlDbType.VarChar, 20);
            _spc[5].Value = _ht["OsNO"].ToString();
            _spc[6] = new System.Data.SqlClient.SqlParameter("@KeyItm", SqlDbType.VarChar, 40);
            _spc[6].Value = _ht["KeyItm"].ToString();
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, null, _spc);
            return _ds.Tables[0].Rows[0]["UNIT"].ToString();
        }
    }
}
