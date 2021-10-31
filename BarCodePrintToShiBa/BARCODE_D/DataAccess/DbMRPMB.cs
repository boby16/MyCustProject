using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business.Data
{
    public class DbMRPMB : Sunlike.Business.Data.DbObject
    {
        /// <summary>
        /// Bom组合生产单
        /// </summary>
        /// <param name="connStr"></param>
        public DbMRPMB(string connStr)
            : base(connStr)
        {
        }

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="mbId"></param>
        /// <param name="mbNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string mbId, string mbNo, bool onlyFillSchema)
        {
            string _sql = " SELECT MB_ID,MB_NO,MB_DD,BIL_ID,BIL_NO,BIL_ITM,MO_NO,SO_NO,USR_NO,WH_PRD,WH_MTL,MRP_NO,PRD_MARK,PRD_NAME,"
                   + " EXP_MTH,UNIT,QTY,QTY1,BAT_NO,VALID_DD,DEP,CST,CST_MAKE,CST_PRD,CST_MAN,CST_OUT,USED_TIME,VOH_ID,VOH_NO,REM,CPY_SW,"
                   + " CST_STD,CST_SMAKE,CST_SPRD,CST_SMAN,CST_SOUT,USED_STIME,CST_DIFF,CST_SDIFF,USR,CHK_MAN,PRT_SW,MD_NO,CLS_DATE,ID_NO,"
                   + " EST_ITM,POSID,BIL_TYPE,RAT,MOB_ID,LOCK_MAN,FJ_NUM,SYS_DATE,TIME_CNT,TIME_SCNT,CAS_NO,TASK_ID,PRT_USR,RK_DD,ISSVS,DEP_RK,"
                   + " AMTN_VAL,UP_MAIN,CANCEL_ID "
                   + " FROM MF_MB "
                   + " WHERE MB_ID=@MB_ID AND MB_NO=@MB_NO;\n"
                   + " SELECT MB_ID,MB_NO,ITM,MB_DD,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY,QTY1,QTY_LOST,QTY1_LOST,CST,BAT_NO,REM,CST_EP,CPY_SW,CST_STD,"
                   + " CST_SEP,FT_ID,PRD_NO_CHG,ID_NO,VAL_FT,POSID,CNTT_NO,COMPOSE_IDNO,EST_ITM,PRE_ITM,USEIN_NO,LOS_RTO,QTY_STD,RK_DD,DEP_RK,CAS_NO,TASK_ID,"
                   + " OS_ID,OS_NO,UP_MAIN"
                   + " FROM TF_MB"
                   + " WHERE MB_ID=@MB_ID AND MB_NO=@MB_NO;\n"
                   + " SELECT MB_ID,MB_NO,MB_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO FROM TF_MB_B WHERE  MB_ID=@MB_ID AND MB_NO=@MB_NO;\n";

            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@MB_ID",  SqlDbType.VarChar, 2);
            _aryPt[0].Value = mbId;
            _aryPt[1] = new SqlParameter("@MB_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = mbNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[] { "MF_MB", "TF_MB", "TF_MB_B" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[] { "MF_MB", "TF_MB", "TF_MB_B" }, _aryPt);
            }

            _ds.Relations.Add(new DataRelation("MF_MBTF_MB", new DataColumn[] { _ds.Tables["MF_MB"].Columns["MB_ID"], _ds.Tables["MF_MB"].Columns["MB_NO"] },
                new DataColumn[] { _ds.Tables["TF_MB"].Columns["MB_ID"], _ds.Tables["TF_MB"].Columns["MB_NO"] }));

            _ds.Relations.Add(new DataRelation("TF_MBTF_MB_B", new DataColumn[] { _ds.Tables["TF_MB"].Columns["MB_ID"], _ds.Tables["TF_MB"].Columns["MB_NO"], _ds.Tables["TF_MB"].Columns["PRE_ITM"] },
                new DataColumn[] { _ds.Tables["TF_MB_B"].Columns["MB_ID"], _ds.Tables["TF_MB_B"].Columns["MB_NO"], _ds.Tables["TF_MB_B"].Columns["MB_ITM"] }));
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="bilNo"></param>
        /// <param name="bilItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBilNo(string bilId, string bilNo, int bilItm)
        {
            string _sql = " SELECT MB_ID,MB_NO,MB_DD,BIL_ID,BIL_NO,BIL_ITM,MO_NO,SO_NO,USR_NO,WH_PRD,WH_MTL,MRP_NO,PRD_MARK,PRD_NAME,"
                   + " EXP_MTH,UNIT,QTY,QTY1,BAT_NO,VALID_DD,DEP,CST,CST_MAKE,CST_PRD,CST_MAN,CST_OUT,USED_TIME,VOH_ID,VOH_NO,REM,CPY_SW,"
                   + " CST_STD,CST_SMAKE,CST_SPRD,CST_SMAN,CST_SOUT,USED_STIME,CST_DIFF,CST_SDIFF,USR,CHK_MAN,PRT_SW,MD_NO,CLS_DATE,ID_NO,"
                   + " EST_ITM,POSID,BIL_TYPE,RAT,MOB_ID,LOCK_MAN,FJ_NUM,SYS_DATE,TIME_CNT,TIME_SCNT,CAS_NO,TASK_ID,PRT_USR,RK_DD,ISSVS,DEP_RK,"
                   + " AMTN_VAL,UP_MAIN,CANCEL_ID "
                   + " FROM MF_MB "
                   + " WHERE BIL_ID=@BIL_ID AND BIL_NO=@BIL_NO AND BIL_ITM=@BIL_ITM;\n";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = bilId;
            _aryPt[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = bilNo;
            _aryPt[2] = new SqlParameter("@BIL_ITM", SqlDbType.Int);
            _aryPt[2].Value = bilItm;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "MF_MB" }, _aryPt);

            string mbNo = "";
            string mbId = "";
            if (_ds.Tables["MF_MB"].Rows.Count > 0)
            {
                mbNo = _ds.Tables["MF_MB"].Rows[0]["MB_NO"].ToString();
                mbId = _ds.Tables["MF_MB"].Rows[0]["MB_ID"].ToString();
            }

            _sql = "SELECT MB_ID,MB_NO,ITM,MB_DD,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY,QTY1,QTY_LOST,QTY1_LOST,CST,BAT_NO,REM,CST_EP,CPY_SW,CST_STD,"
                   + " CST_SEP,FT_ID,PRD_NO_CHG,ID_NO,VAL_FT,POSID,CNTT_NO,COMPOSE_IDNO,EST_ITM,PRE_ITM,USEIN_NO,LOS_RTO,QTY_STD,RK_DD,DEP_RK,CAS_NO,TASK_ID,"
                   + " OS_ID,OS_NO,UP_MAIN"
                   + " FROM TF_MB"
                   + " WHERE MB_ID=@MB_ID AND MB_NO=@MB_NO;\n";

            _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@MB_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = mbId;
            _aryPt[1] = new SqlParameter("@MB_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = mbNo;
            SunlikeDataSet _ds2 = new SunlikeDataSet();
            this.FillDataset(_sql, _ds2, new string[] { "TF_MB" }, _aryPt);
            _ds.Merge(_ds2);
            _ds.Relations.Add(new DataRelation("MF_MBTF_MB", new DataColumn[] { _ds.Tables["MF_MB"].Columns["MB_ID"], _ds.Tables["MF_MB"].Columns["MB_NO"] },
                new DataColumn[] { _ds.Tables["TF_MB"].Columns["MB_ID"], _ds.Tables["TF_MB"].Columns["MB_NO"] }));
            return _ds;
        }

        /// <summary>
        /// 修改审核人和审核日期
        /// </summary>
        /// <param name="blId">单据代号</param>
        /// <param name="blNo">单据号码</param>
        /// <param name="chkMan">审核人</param>
        /// <param name="clsDate">审核日期</param>
        public void UpdateChkMan(string mbId, string mbNo, string chkMan, DateTime clsDate, string vohNo)
        {
            string _sql = "UPDATE MF_MB SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE,VOH_NO=@VOH_NO WHERE MB_ID=@MB_ID AND MB_NO=@MB_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[5];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            if (String.IsNullOrEmpty(chkMan))
            {
                _spc[0].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = chkMan;
            }
            _spc[1] = new System.Data.SqlClient.SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            if (String.IsNullOrEmpty(chkMan))
            {
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[1].Value = clsDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            _spc[2] = new System.Data.SqlClient.SqlParameter("@MB_ID", SqlDbType.Char, 2);
            _spc[2].Value = mbId;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@MB_NO", SqlDbType.VarChar, 20);
            _spc[3].Value = mbNo;
            _spc[4] = new System.Data.SqlClient.SqlParameter("@VOH_NO", SqlDbType.VarChar, 20);
            _spc[4].Value = vohNo;
            this.ExecuteNonQuery(_sql, _spc);
        }
    }
}
