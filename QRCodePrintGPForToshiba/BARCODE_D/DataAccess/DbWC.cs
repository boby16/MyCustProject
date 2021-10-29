using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// DbWC
    /// </summary>
    public class DbWC : DbObject
    {
        /// <summary>
        /// DbWC
        /// </summary>
        /// <param name="connStr"></param>
        public DbWC(string connStr)
            : base(connStr)
        {
        }

        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="wcNo">WC_NO</param>
        /// <param name="onlyFillSchema">BOOL</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string wcNo, bool onlyFillSchema)
        {
            string _sql = "SELECT MF.*,A.SPC FROM MF_WC MF LEFT JOIN PRDT A ON A.PRD_NO=MF.PRD_NO WHERE MF.WC_NO = @WC_NO;"
                + "SELECT TF.*,A.SPC FROM TF_WC TF LEFT JOIN PRDT A ON A.PRD_NO=TF.PRD_NO WHERE TF.WC_NO = @WC_NO;";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@WC_NO", SqlDbType.VarChar, 25);
            _aryPt[0].Value = wcNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[2] { "MF_WC", "TF_WC" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[2] { "MF_WC", "TF_WC" }, _aryPt);
            }

            _ds.Relations.Add("MF_WCTF_WC", _ds.Tables["MF_WC"].Columns["WC_NO"], _ds.Tables["TF_WC"].Columns["WC_NO"]);

            return _ds;
        }

        /// <summary>
        /// GetDataByBarCode
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByBarCode(string barCode, bool head)
        {
            string _tableName = head ? "MF_WC" : "TF_WC";
            string _sql = "SELECT WC_NO FROM " + _tableName + " WHERE BAR_CODE = @BAR_CODE";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@BAR_CODE", SqlDbType.VarChar, 90);
            _aryPt[0].Value = barCode;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { _tableName }, _aryPt);
            return _ds;
        }
        /// <summary>
        /// GetDataByBarCodeList
        /// </summary>
        /// <param name="barCodeList"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByBarCodeList(string barCodeList)
        {
            string _sql = "SELECT WC_NO FROM MF_WC WHERE BAR_CODE IN (" + barCodeList + ")";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "MF_WC" });
            return _ds;
        }

        /// <summary>
        /// 去缴库单资料
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromMM(string sqlWhere)
        {
            string _sql = "SELECT M1.MM_DD, M1.MM_NO, MB.PRD_NO AS MRP_NO, MB.PRD_MARK, MB.PRD_NAME, MB.MO_NO, ISNULL(MO.QTY_FIN,0) as QTY_FIN, ISNULL(MB.QTY, 0) AS QTY, ISNULL(M2.QTY_BAR_CODE, 0) AS QTY_BAR_CODE FROM \n"
            + "MF_MM0 M1 \n"
            + "INNER JOIN TF_MM0 MB ON MB.MM_ID = M1.MM_ID AND MB.MM_NO = M1.MM_NO \n"
            + "LEFT JOIN \n"
            + "(SELECT MM_NO, COUNT(ITM) AS QTY_BAR_CODE FROM MF_MM_B GROUP BY MM_NO) M2 \n"
            + "ON M1.MM_NO = M2.MM_NO \n"
            + "LEFT JOIN MF_MO MO\n"
            + "ON M1.MO_NO = MO.MO_NO\n"
            + "WHERE 1 = 1" + sqlWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "MF_MM" });

            StringBuilder _sb = new StringBuilder();
            foreach (DataRow dr in _ds.Tables["MF_MM"].Rows)
            {
                if (!String.IsNullOrEmpty(_sb.ToString()))
                {
                    _sb.Append(",");
                }
                _sb.Append("'" + dr["MM_NO"].ToString() + "'");
            }
            if (String.IsNullOrEmpty(_sb.ToString()))
            {
                _sb.Append("''");
            }
            _sql = "SELECT MB.*,M1.MM_DD FROM\n"
                + "TF_MM0_B MB\n"
                + "LEFT JOIN\n"
                + "MF_MM0 M1\n"
                + "ON MB.MM_ID = M1.MM_ID AND MB.MM_NO = M1.MM_NO\n"
                + "WHERE MB.MM_ID = 'MM' AND MB.MM_NO IN (" + _sb.ToString() + ")";
            SunlikeDataSet _dsMmb = new SunlikeDataSet();
            this.FillDataset(_sql, _dsMmb, new string[1] { "MF_MM_B" });
            _ds.Merge(_dsMmb);

            return _ds;
        }
        /// <summary>
        /// 取得产品保修卡信息　取的字段有:WC_NO,PRD_NO,PRD_MARK,BAR_CODE,CUS_NO,BUY_DD,MTN_DD,RETURN_DD,NEED_DAYS[货品基础资料的前置天数]
        /// </summary>
        /// <param name="wcNo">产品保修卡</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string wcNo)
        {
            string _sqlStr = " SELECT A.MTN_ALL_ID,A.WC_NO,A.PRD_NO,A.PRD_MARK,A.BAR_CODE,A.CUS_NO,A.BUY_DD,A.MTN_DD,A.RETURN_DD,A.SA_NO,A.STOP_DD, "
                            + " B.NAME AS PRD_NAME,B.MTN_LTIME,B.SPC, "
                            + " C.NAME CUS_NAME, C.SNM CUS_SNM "
                            + " FROM MF_WC A"
                            + " LEFT JOIN PRDT B ON B.PRD_NO = A.PRD_NO "
                            + " LEFT JOIN CUST C ON C.CUS_NO = A.CUS_NO "
                            + " WHERE A.WC_NO =@WC_NO ";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@WC_NO", SqlDbType.VarChar, 25);
            _sqlPara[0].Value = wcNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "MF_WC" }, _sqlPara);
            return _ds;

        }
        /// <summary>
        /// 返回维修记录datatable
        /// </summary>
        /// <param name="wcNo">保修卡号</param>
        /// <returns>维修记录datatable</returns>
        public DataTable GetDataWcH(string wcNo) 
        {
            string _sql = @"SELECT WC_NO, ITM, BIL_DD, BIL_ID, BIL_NO, REM, SYS_DATE
                            FROM  TF_WC_H WHERE WC_NO=@WC_NO";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@WC_NO", SqlDbType.VarChar, 25);
            _aryPt[0].Value = wcNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "TF_WC_H"}, _aryPt);            
            return _ds.Tables["TF_WC_H"];        
        }

        public SunlikeDataSet GetDataWcH(string _bilID, string _bilNO)
        {
            string _sql = string.Empty;
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _aryPt = new SqlParameter[1];
            switch (_bilID)
            {
                case "MA":
                    _sql = "SELECT T.NAME BIL_TYPE_NAME,M.REM,M.BIL_NO FBIL_NO FROM MF_MA M " +
                        " LEFT OUTER JOIN BIL_SPC T ON M.BIL_TYPE=T.SPC_NO " +
                        " WHERE M.MA_NO=@BIL_NO ";                                        
                    _aryPt[0] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 30);
                    _aryPt[0].Value = _bilNO;                    
                    this.FillDataset(_sql, _ds, new string[1] { "TF_WC_H_DESC" }, _aryPt);
                    break;

                case "OT":
                    _sql = "SELECT T.NAME BIL_TYPE_NAME,M.REM,M.MA_NO FBIL_NO FROM MF_MOUT M " +
                        " LEFT OUTER JOIN BIL_SPC T ON M.BIL_TYPE=T.SPC_NO " +
                        " WHERE M.OT_NO=@BIL_NO ";
                    _aryPt[0] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 30);
                    _aryPt[0].Value = _bilNO;                    
                    this.FillDataset(_sql, _ds, new string[1] { "TF_WC_H_DESC" }, _aryPt);
                    break;

                case "OW":
                    _sql = "SELECT T.NAME BIL_TYPE_NAME,M.REM,M.OT_NO FBIL_NO FROM MF_MFIN M " +
                        " LEFT OUTER JOIN BIL_SPC T ON M.BIL_TYPE=T.SPC_NO " +
                        " WHERE M.OW_NO=@BIL_NO ";
                    _aryPt[0] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 30);
                    _aryPt[0].Value = _bilNO;                   
                    this.FillDataset(_sql, _ds, new string[1] { "TF_WC_H_DESC" }, _aryPt);
                    break;
                default:
                    break;
            }
            
            return _ds;            
        }
        /// <summary>
        /// 返回维修记录datatable
        /// </summary>
        /// <param name="wcNo">保修卡号</param>
        /// <returns>维修记录datatable</returns>
        public DataTable GetDataPact(string wcNo)
        {
            string _sql = @"SELECT A.PAC_NO,B.TITLE,B.CUS_NO,B.DEP,B.START_DD,B.END_DD,B.REM
                             FROM CUS_PACT1 A
                             LEFT JOIN CUS_PACT B ON B.PAC_NO = A.PAC_NO
                             WHERE A.WC_NO=@WC_NO ";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@WC_NO", SqlDbType.VarChar, 25);
            _aryPt[0].Value = wcNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "CUS_PACT" }, _aryPt);
            return _ds.Tables["CUS_PACT"];
        }

    }
}