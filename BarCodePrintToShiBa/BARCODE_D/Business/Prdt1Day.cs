/* title: Prdt1Day  日分仓存量查询
 * create : lzj 091223
 * doc:
 * 
 *
 */

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
    public class Prdt1Day : BizObject
    {

        #region 取数据
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="dep"></param>
        /// <param name="bilDd"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string prdNo, string prdMark, string dep, string wh, DateTime bilDd)
        {
            DbPrdt1Day _dbpd = new DbPrdt1Day(Comp.Conn_DB);
            return _dbpd.GetData(prdNo, prdMark, dep, wh, bilDd);
        }
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="dep"></param>
        /// <param name="bilDd"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string prdNo, string prdMark, string dep, DateTime bilDd)
        {
            return GetData(prdNo, prdMark, dep, null, bilDd);
        }
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="sqlWhere">sqlWhere</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            DbPrdt1Day _dbpd = new DbPrdt1Day(Comp.Conn_DB);
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
            DbPrdt1Day _db = new DbPrdt1Day(Comp.Conn_DB);
            SunlikeDataSet _ds = new SunlikeDataSet();
            _ds = _db.GetData(Comp.Conn_DB, _sqlWhere, currentPage, rowsPerPage, ref totalPageCount, ref totalRowCount);
            return _ds;
        }

        #endregion

        #region 更新数据
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            #region PRDT1_DAY
            if (string.Compare(tableName, "PRDT1_DAY") == 0)
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
            _ht["PRDT1_DAY"] = "WH,DEP,PRD_NO,PRD_MARK,RK_DD,QTY,QTY1,VALID_DD,LST_OTD,LOCK_ID,REM";
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

        #region 判断日库存是否锁定

        /// <summary>
        /// 判断日库存是否锁定
        /// </summary>
        /// <param name="batNo">批号</param>
        /// <param name="wh">仓库</param>
        /// <param name="dep">集团分公司</param>
        /// <param name="prdNo">品号</param>
        /// <param name="prdMark">特征</param>
        /// <param name="rkDd">入库日期</param>
        /// <returns></returns>
        public static bool CheckLockID(string batNo, string wh, string dep, string prdNo, string prdMark, DateTime rkDd)
        {
            DataSet _ds = new DataSet();
            if (!String.IsNullOrEmpty(batNo))
            {
                BatRec1Day _brd = new BatRec1Day();
                _ds = _brd.GetData(
                    String.Format("BAT_NO='{0}' AND WH='{1}' AND DEP='{2}' AND PRD_NO='{3}' AND PRD_MARK='{4}' AND RK_DD='{5}' AND ISNULL(LOCK_ID,'F')<>'T'",
                        batNo, wh, dep, prdNo, prdMark, rkDd.Date.ToString())
                    );
            }
            else
            {
                Prdt1Day _pd = new Prdt1Day();
                _ds = _pd.GetData(
                    String.Format("WH='{1}' AND DEP='{2}' AND PRD_NO='{3}' AND PRD_MARK='{4}' AND RK_DD='{5}' AND ISNULL(LOCK_ID,'F')<>'T'",
                        batNo, wh, dep, prdNo, prdMark, rkDd.Date.ToString())
                    );
            }
            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
