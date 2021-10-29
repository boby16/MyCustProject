using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    public class DbDRPHC : DbObject
    {
        public DbDRPHC(string connString)
            : base(connString)
        {
        }

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="hcNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string hcNo, bool onlyFillSchema)
        {
            string _sql = "SELECT * FROM MF_HC WHERE HC_NO = @HC_NO; \n"
                + "SELECT TF_HC.*,MF_ZG.ZG_DD,MF_ZG.BAT_NO,MF_ZG.CUS_NO,MF_ZG.DEP,MF_ZG.REM,CUST.SNM AS CUS_NAME FROM TF_HC LEFT JOIN MF_ZG \n"
                + "ON MF_ZG.ZG_ID = 'ZB' AND TF_HC.ZB_NO = MF_ZG.ZG_NO \n"
                + "LEFT JOIN CUST ON MF_ZG.CUS_NO = CUST.CUS_NO \n"
                + "WHERE HC_NO = @HC_NO;";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@HC_NO", SqlDbType.VarChar, 20);
            _aryPt[0].Value = hcNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[] { "MF_HC", "TF_HC" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[] { "MF_HC", "TF_HC" }, _aryPt);
            }

            _ds.Relations.Add(new System.Data.DataRelation("MF_HCTF_HC",
                _ds.Tables["MF_HC"].Columns["HC_NO"], _ds.Tables["TF_HC"].Columns["HC_NO"]));

            return _ds;
        }

        /// <summary>
        /// 根据PC取单
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="bilNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bilId, string bilNo)
        {
            string _sql = "SELECT * FROM TF_HC WHERE BIL_ID = @BIL_ID AND PC_NO = @PC_NO";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = bilId;
            _aryPt[1] = new SqlParameter("@PC_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = bilNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_HC" }, _aryPt);
            return _ds;
        }
    }
}
