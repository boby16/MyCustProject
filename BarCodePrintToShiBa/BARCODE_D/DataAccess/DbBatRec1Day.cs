using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Data;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// Daily storage
    /// </summary>
    public class DbBatRec1Day : DbObject
    {
        #region 构造
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public DbBatRec1Day(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region 取数据
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="batNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="dep"></param>
        /// <param name="bilDd"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string batNo, string prdNo, string prdMark, string dep, string wh, DateTime bilDd)
        {
            string _sql = "SELECT ISNULL(QTY_IN, 0) - ISNULL(QTY_OUT, 0) AS QTY, * FROM BAT_REC1_DAY WHERE PRD_NO = @PRD_NO AND PRD_MARK = @PRD_MARK AND ISNULL(QTY_IN, 0) - ISNULL(QTY_OUT, 0) > 0 AND (VALID_DD IS NULL OR VALID_DD >= @VALID_DD)";
            if (!String.IsNullOrEmpty(dep))
            {
                _sql += " AND DEP = @DEP";
            }
            if (!String.IsNullOrEmpty(wh))
            {
                _sql += " AND WH = @WH";
            }
            if (!String.IsNullOrEmpty(batNo))
            {
                _sql += " AND BAT_NO = @BAT_NO";
            }
            SqlParameter[] _aryParam = new SqlParameter[6];
            _aryParam[0] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
            _aryParam[0].Value = prdNo;
            _aryParam[1] = new SqlParameter("@PRD_MARK", SqlDbType.VarChar, 40);
            _aryParam[1].Value = prdMark;
            _aryParam[2] = new SqlParameter("@DEP", SqlDbType.VarChar, 8); 
            _aryParam[2].Value = dep;
            _aryParam[3] = new SqlParameter("@VALID_DD", SqlDbType.DateTime);
            _aryParam[3].Value = bilDd;
            _aryParam[4] = new SqlParameter("@BAT_NO", SqlDbType.VarChar, 40);
            _aryParam[4].Value = batNo;
            _aryParam[5] = new SqlParameter("@WH", SqlDbType.VarChar, 12);
            _aryParam[5].Value = wh;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "BAT_REC1_DAY" }, _aryParam);
            return _ds;
        }
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="sqlWhere">sqlWhere</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            string _sql = @"SELECT PD.BAT_NO, PD.WH,W.NAME AS WH_NAME,I.NAME AS IDX_NAME,CASE P.DFU_UT WHEN 1 THEN P.UT WHEN 2 THEN P.PK2_UT WHEN 3 THEN P.PK3_UT END AS DFU_UT, PD.DEP, PD.PRD_NO,P.NAME AS PRD_NAME,P.SPC, PD.PRD_MARK, PD.RK_DD, PD.QTY_IN, PD.QTY_IN_UNSH, PD.QTY_OUT, PD.QTY_OUT_UNSH, 
                            ISNULL(PD.QTY_IN,0)-ISNULL(PD.QTY_OUT,0) AS QTY,ISNULL(PD.QTY_IN_UNSH,0)-ISNULL(PD.QTY_OUT_UNSH,0) AS QTY_UNSH,
                            ISNULL(PD.QTY1_IN,0)-ISNULL(PD.QTY1_OUT,0) AS QTY1,ISNULL(PD.QTY1_IN_UNSH,0)-ISNULL(PD.QTY1_OUT_UNSH,0) AS QTY1_UNSH,
                            PD.QTY1_IN, PD.QTY1_IN_UNSH, PD.QTY1_OUT, PD.QTY1_OUT_UNSH, PD.VALID_DD, PD.LST_OTD, PD.LOCK_ID, PD.PRODU_DD, PD.REM
                            FROM  BAT_REC1_DAY AS PD LEFT OUTER JOIN
                            PRDT AS P ON PD.PRD_NO = P.PRD_NO LEFT OUTER JOIN
                            MY_WH W ON W.WH=PD.WH LEFT OUTER JOIN
                            INDX I ON I.IDX_NO=P.IDX1 ";
            if (!String.IsNullOrEmpty(sqlWhere.Trim()))
            {
                if (sqlWhere.Trim().ToUpper().IndexOf("AND") == 0)
                {
                    sqlWhere = " 1=1 " + sqlWhere;
                }
                _sql += " WHERE " + sqlWhere;
            }

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "BAT_REC1_DAY" });
            return _ds;
        }
        /// <summary>
        /// WinForm 数据分批 PRDT1_DAY
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="_sqlWhere"></param>
        /// <param name="currentPage"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="totalPageCount"></param>
        /// <param name="totalRowCount"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string conn, string _sqlWhere, long currentPage, long rowsPerPage, ref int totalPageCount, ref int totalRowCount)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            string _sql = @"SELECT PD.BAT_NO, PD.WH,W.NAME AS WH_NAME,I.NAME AS IDX_NAME,CASE P.DFU_UT WHEN 1 THEN P.UT WHEN 2 THEN P.PK2_UT WHEN 3 THEN P.PK3_UT END AS DFU_UT, PD.DEP, PD.PRD_NO,P.NAME AS PRD_NAME,P.SPC, PD.PRD_MARK, PD.RK_DD, PD.QTY_IN, PD.QTY_IN_UNSH, PD.QTY_OUT, PD.QTY_OUT_UNSH, 
                            ISNULL(PD.QTY_IN,0)-ISNULL(PD.QTY_OUT,0) AS QTY,ISNULL(PD.QTY_IN_UNSH,0)-ISNULL(PD.QTY_OUT_UNSH,0) AS QTY_UNSH,
                            ISNULL(PD.QTY1_IN,0)-ISNULL(PD.QTY1_OUT,0) AS QTY1,ISNULL(PD.QTY1_IN_UNSH,0)-ISNULL(PD.QTY1_OUT_UNSH,0) AS QTY1_UNSH,
                            PD.QTY1_IN, PD.QTY1_IN_UNSH, PD.QTY1_OUT, PD.QTY1_OUT_UNSH, PD.VALID_DD, PD.LST_OTD, PD.LOCK_ID, PD.PRODU_DD, PD.REM
                            FROM  BAT_REC1_DAY AS PD LEFT OUTER JOIN
                            PRDT AS P ON PD.PRD_NO = P.PRD_NO LEFT OUTER JOIN
                            MY_WH W ON W.WH=PD.WH LEFT OUTER JOIN
                            INDX I ON I.IDX_NO=P.IDX1 ";

            if (!String.IsNullOrEmpty(_sqlWhere.Trim()))
            {
                if (_sqlWhere.Trim().ToUpper().IndexOf("AND") == 0)
                {
                    _sqlWhere = " 1=1 " + _sqlWhere;
                }
                _sql += " WHERE " + _sqlWhere;
            }
            DbQuery _query = new DbQuery(conn);
            _ds = _query.DoSQLPaging("BAT_REC1_DAY", _sql, "  ORDER BY PD.BAT_NO,PD.WH,PD.DEP,PD.RK_DD ", (int)rowsPerPage, (int)currentPage, ref totalPageCount, ref totalRowCount);
            return _ds;
        }


        #endregion
    }
}
