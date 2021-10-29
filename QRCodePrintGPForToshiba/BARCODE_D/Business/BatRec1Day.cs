using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using System.Data;
using System.Collections;

namespace Sunlike.Business
{
    /// <summary>
    /// Daily storage
    /// </summary>
    public class BatRec1Day : BizObject
    {
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
        public SunlikeDataSet GetData(string batNo, string prdNo, string prdMark, string dep, DateTime bilDd)
        {
            return GetData(batNo, prdNo, prdMark, dep, null, bilDd);
        }
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="batNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="dep"></param>
        /// <param name="wh"></param>
        /// <param name="bilDd"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string batNo, string prdNo, string prdMark, string dep, string wh, DateTime bilDd)
        {
            DbBatRec1Day _dbpd = new DbBatRec1Day(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbpd.GetData(batNo, prdNo, prdMark, dep, wh, bilDd);
            if (_ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Columns.Contains("QTY"))
                {
                    _ds.Tables[0].Columns["QTY"].ReadOnly = false;
                }
            }
            return _ds;
        }
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="sqlWhere">sqlWhere</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            DbBatRec1Day _dbpd = new DbBatRec1Day(Comp.Conn_DB);
            return _dbpd.GetData(sqlWhere);
        }
        /// <summary>
        /// WINForm 批次取
        /// </summary>
        /// <param name="_pgm"></param>
        /// <param name="_sqlWhere"></param>
        /// <param name="currentPage"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="totalPageCount"></param>
        /// <param name="totalRowCount"></param>
        /// <param name="_filter"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string _pgm, string _sqlWhere, long currentPage, long rowsPerPage, ref int totalPageCount, ref int totalRowCount, int _filter)
        {
            DbBatRec1Day _db = new DbBatRec1Day(Comp.Conn_DB);
            SunlikeDataSet _ds = new SunlikeDataSet();
            _ds = _db.GetData(Comp.Conn_DB, _sqlWhere, currentPage, rowsPerPage, ref totalPageCount, ref totalRowCount);
            return _ds;
        }

        #endregion

        #region 更新数据
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            #region BAT_REC1_DAY
            if (string.Compare(tableName, "BAT_REC1_DAY") == 0)
            {
                if (statementType != StatementType.Delete)
                {
                    #region MF默认栏位
                    if (!string.IsNullOrEmpty(dr["LST_OTD"].ToString()))
                        dr["LST_OTD"] = Convert.ToDateTime(dr["LST_OTD"]).ToString(Comp.SQLDateFormat);
                    if (!string.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                        dr["VALID_DD"] = Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat);
                    #endregion

                }
            }
            #endregion
            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="_ds">dataset数据</param>
        public DataTable UpdateData(SunlikeDataSet _ds)
        {
            DataTable _dtErr = null;
            Hashtable _ht = new Hashtable();
            _ht["BAT_REC1_DAY"] = "BAT_NO,WH,DEP,PRD_NO,PRD_MARK,RK_DD,QTY_IN,QTY_IN_UNSH,QTY_OUT,QTY_OUT_UNSH,QTY1_IN,QTY1_IN_UNSH,QTY1_OUT,QTY1_OUT_UNSH,VALID_DD,LST_OTD,LOCK_ID,PRODU_DD,REM";
            this.UpdateDataSet(_ds, _ht);
            //判断单据能否修改
            if (_ds.HasErrors)
            {
                _dtErr = GetAllErrors(_ds);
            }
            return _dtErr;
        }
        #endregion

        #region 方法
        private decimal Convert2Decimal(object p)
        {
            decimal _d = 0;
            if (!decimal.TryParse(Convert.ToString(p), out _d))
                _d = 0;
            return _d;
        }
        private int Convert2Int(object p)
        {
            int _d = 0;
            if (!int.TryParse(Convert.ToString(p), out _d))
                _d = 0;
            return _d;
        }
        #endregion

    }
}

