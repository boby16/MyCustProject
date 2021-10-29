using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Collections.Generic;
namespace Sunlike.Business.Data
{
    public class DbPOSCTypeDef : Sunlike.Business.Data.DbObject
    {
        public DbPOSCTypeDef(string connectionString)
            : base(connectionString)
        {
        }

        public SunlikeDataSet GetData(string sqlWhere)
        {
            string _sqlStr = "SELECT * FROM CTYPE_DEF";
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                _sqlStr += " WHERE " + sqlWhere;
            }

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "CTYPE_DEF" });
            return _ds;
        }
    }
}
