using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    public class DbMRPBE : DbObject
    {
        #region 构造
        public DbMRPBE(string connectionString)
            : base(connectionString)
        {

        }
        #endregion

        #region 取数据
        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="twno"></param>
        /// <returns>DS</returns>
        public SunlikeDataSet GetData(string twno)
        {
            string _sql = @"SELECT     MF.TW_NO, MF.TW_DD, MF.BAT_NO, MF.BIL_ID, MF.BIL_NO, MF.VOH_ID, MF.VOH_NO, MF.SEND_MTH, MF.SEND_WH, MF.EST_DD, MF.CUS_NO, 
                              MF.SAL_NO, MF.DEP, MF.CUR_ID, MF.EXC_RTO, MF.CLOSE_ID, MF.TAX_ID, MF.INV_NO, MF.RP_NO, MF.AMTN_NET, MF.TAX, MF.AMT, MF.REM, 
                              MF.ADR, MF.MO_NO, MF.ZC_NO, MF.MRP_NO, MF.PRD_MARK, MF.WH, MF.UNIT, MF.QTY, MF.UP, MF.QTY_RTN, MF.QTY_RTN_UNSH, 
                              MF.QTY_LOST, MF.QTY_LOST_UNSH, MF.QTY_ML, MF.QTY_ML_UNSH, MF.AMT_INT, MF.AMTN_NET_INT, MF.TAX_INT, MF.ML_NO, MF.CONTRACT, 
                              MF.CPY_SW, MF.CUS_UP, MF.CUS_DOWN, MF.ML_OK, MF.USR, MF.CHK_MAN, MF.PRT_SW, MF.QTY_RK, MF.QTY_RK_UNSH, MF.ZC_ITM, 
                              MF.FIN_DD, MF.CHK_ID, MF.MV_ID, MF.QTY_PRC, MF.QTY_MV, MF.QTY_MV_UNSH, MF.QTY_CHK, MF.QTY_CHK_UNSH, MF.ZC_NO_UP, 
                              MF.ZC_NO_DN, MF.QTY_WH, MF.CLS_DATE, MF.ID_NO, MF.CST, MF.WT_NO, MF.QC_YN, MF.MM_CURML, MF.SO_NO, MF.EST_ITM, MF.QTY1, 
                              MF.TS_ID, MF.ISFIRST, MF.QTY_TC, MF.QTY_TC_UNSH, MF.RTO_TAX, MF.UP1, MF.BIL_TYPE, MF.CNTT_NO, MF.MOB_ID, MF.LOCK_MAN, 
                              MF.FJ_NUM, MF.SYS_DATE, MF.CAS_NO, MF.TASK_ID, MF.OLD_ID, MF.QTY_SL, MF.QTY_SL_UNSH, MF.SCM_USR, MF.SCM_DD, MF.CUS_OS_NO, 
                              MF.PRT_USR, MF.QTY_QL, MF.QTY_QL_UNSH, MF.QL_ID, MF.Q2_ID, MF.Q3_ID, MF.QTY_DZ, MF.ISSVS, MF.PRT_NUM, MF.ML_BY_MM, 
                              MF.QTY_DM, MF.QTY_DM_UNSH, MF.CONTROL, MF.PAY_DD, MF.CHK_DD, MF.PAY_MTH, MF.PAY_DAYS, MF.CHK_DAYS, MF.INT_DAYS, 
                              MF.PAY_REM, MF.CLS_REM, MF.ZC_REM, MF.PO_OK, MF.CF_ID, MF.ZT_ID, MF.ZT_DD, MF.CV_ID, MF.QTY_DZ_UNSH, MF.CANCEL_ID, 
                              MF.RTO_FQSK, MF.DATEFLAG_FQSK, MF.DATE_FQSK, MF.QS_FQSK, P.SPC,
                             H.AMT_BB, H.AMTN_BB, H.AMT_BC, H.AMTN_BC, H.CACC_NO, H.BACC_NO, H.AMT_CHK1 AS AMT_CHK,H.AMTN_CHK AS AMTN_CHK,
                             H.AMTN_CLS, I.CHK_NO, I.CHK_KND, I.BANK_NO, I.BACC_NO AS BACC_NO_CHK, I.CRECARD_NO, I.END_DD 
                              FROM         MF_TW AS MF LEFT OUTER JOIN
                              PRDT AS P ON P.PRD_NO = MF.MRP_NO
                             LEFT JOIN TF_MON H ON MF.RP_NO=H.RP_NO and H.RP_ID='2'
                             left join TF_MON4 N4 on MF.RP_NO=N4.RP_NO and N4.RP_ID='2' and N4.ITM=1
                             left join MF_CHK I on N4.CHK_NO=I.CHK_NO and I.CHK_ID='0'
                              WHERE TW_NO = @TW_NO;
                            SELECT     TF.TW_NO, TF.ITM, TF.PRD_NO, TF.PRD_NAME, TF.PRD_MARK, TF.WH, TF.UNIT, TF.QTY, TF.QTY_RTN, TF.QTY_RTN_UNSH, TF.BAT_NO, 
                            TF.USEIN_NO, TF.CPY_SW, TF.REM, TF.PRD_NO_CHG, TF.QTY1, TF.ID_NO, TF.CST, TF.WT_NO, TF.QTY_TS, TF.QTY_TS_UNSH, TF.TS_ITM, 
                            TF.CNTT_NO, TF.COMPOSE_IDNO, TF.EST_ITM, TF.PRE_ITM, TF.LOS_RTO, TF.QTY_STD, TF.ZC_PRD, TF.QTY_RSV, TF.QTY_LOST, TF.CHG_RTO, 
                            TF.CST_STD, TF.CHG_ITM, TF.QTY_CHG_RTO, TF.QTY_QL, TF.QTY_QL_UNSH, TF.QTY1_QL, TF.QTY1_QL_UNSH, TF.QTY_DM, TF.QTY_DM_UNSH, 
                            TF.QTY1_DM, TF.QTY1_DM_UNSH, TF.QTY_BL, TF.QTY_BL_UNSH
                            FROM         MF_TW AS MF LEFT OUTER JOIN
                            TF_TW AS TF ON MF.TW_NO = TF.TW_NO
                            WHERE MF.TW_NO = @TW_NO;";
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _sp = new SqlParameter[1];
            _sp[0] = new SqlParameter("@TW_NO", SqlDbType.VarChar, 20);
            _sp[0].Value = twno;

            this.FillDataset(_sql, _ds, new String[] { "MF_TW", "TF_TW" }, _sp);
            DataColumn[] _dcPk1 = new DataColumn[1];
            _dcPk1[0] = _ds.Tables["MF_TW"].Columns["TW_NO"];
            _ds.Tables["MF_TW"].PrimaryKey = _dcPk1;
            DataColumn[] _dcPk2 = new DataColumn[1];
            _dcPk2[0] = _ds.Tables["TF_TW"].Columns["TW_NO"];
            DataColumn[] _dcPk3 = new DataColumn[2];
            _dcPk3[0] = _ds.Tables["TF_TW"].Columns["TW_NO"];
            _dcPk3[1] = _ds.Tables["TF_TW"].Columns["ITM"];
            _ds.Tables["TF_TW"].PrimaryKey = _dcPk3;

            _ds.Relations.Add("MF_TWTF_TW", _dcPk1, _dcPk2);
            return _ds;
        }

        //获取指定的MO单转入到TW的数据
        public SunlikeDataSet GetMoQty(string twno)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            string _Sql = "Select MRP_NO,QTY,UNIT from MF_TW Where MO_NO=@MO_NO";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = twno;
            this.FillDataset(_Sql, _ds, new string[] { "MO_TW" },_spc);
            return _ds;
        }

        #endregion

        #region 审核写值
        /// <summary>
        /// 修改审核人和审核日期
        /// </summary>
        /// <param name="bil_No">bil_No</param>
        /// <param name="chk_Man">审核人</param>
        /// <param name="cls_Date">审核日期</param>
        public void UpdateChkMan(string bil_No, string chk_Man, DateTime cls_Date)
        {
            string _sql = @"UPDATE MF_TW SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE
                           WHERE TW_NO=@TW_NO";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _spc[2] = new SqlParameter("@TW_NO", SqlDbType.VarChar, 38);
            _spc[2].Value = bil_No;
            if (string.IsNullOrEmpty(chk_Man))
            {
                _spc[0].Value = System.DBNull.Value;
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = chk_Man;
                _spc[1].Value = cls_Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion

        #region 更新原单
        /// <summary>
        /// 更新制令单的Build_BIL字段 
        /// </summary>
        /// <param name="moNo">制令单号</param>
        public void UpdateMOBuild(string moNo)
        {
            string _sql = @"SELECT @TW_NO=@TW_NO+'TW-'+TW_NO+'  '
                            FROM MF_TW WHERE MO_NO=@MO_NO;
                            UPDATE MF_MO SET
                            BIL_MAK='T' , BUILD_BIL=@TW_NO
                            WHERE MO_NO=@MO_NO AND LEN(@TW_NO)>0;
                            UPDATE MF_MO SET
                            BIL_MAK = NULL , BUILD_BIL = NULL
                            WHERE MO_NO=@MO_NO AND LEN(@TW_NO)=0;";

            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@MO_NO", SqlDbType.VarChar, 38);
            _spc[0].Value = moNo;
            _spc[1] = new SqlParameter("@TW_NO", SqlDbType.VarChar, 1000);
            _spc[1].Value = "";
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion


    }
}
