using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sunlike.Business.Data
{
    public class DbMTNOut : DbObject
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="connectionString"></param>
        public DbMTNOut(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// 取数据

        /// </summary>
        /// <param name="otId">OT</param>
        /// <param name="otNo">外派维修单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string otId, string otNo)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();

            SqlParameter[] _sp = new SqlParameter[2];
            _sp[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 2);
            _sp[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _sp[0].Value = otId;
            _sp[1].Value = otNo;

            StringBuilder _sql = new StringBuilder();
            _sql.Append(" SELECT A.OT_ID,A.CB_ID, A.OT_NO, A.OT_DD, A.CUS_NO, A.DEP, A.SAL_NO, A.BIL_TYPE, A.CLS_ID, A.CLS_AUTO, A.CUR_ID, A.EXC_RTO, A.TAX_ID, A.ZHANG_ID, A.REM, A.MA_ID, A.MA_NO, A.SYS_DATE, A.CLS_DATE, A.PRT_SW, A.USR, A.CHK_MAN,A.CNT_NO, A.CNT_REM, A.DIS_CNT, A.INV_NO, A.RP_NO,A.CLS_LJ_ID,A.CLS_LJ_AUTO,A.AMTN_YJBX,A.AMTN_BX, A.VOH_ID,A.VOH_NO,A.FJ_NUM,A.LOCK_MAN,")
                .Append(" H.AMT_BB, H.AMTN_BB, H.AMT_BC, H.AMTN_BC, H.CACC_NO, H.BACC_NO, H.AMT_CHK1 AS AMT_CHK,H.AMTN_CHK AS AMTN_CHK, H.AMTN_CLS, I.CHK_NO, I.CHK_KND, I.BANK_NO, I.BACC_NO AS BACC_NO_CHK, I.CRECARD_NO, I.END_DD, A.PAY_MTH, A.PAY_DAYS, A.CHK_DAYS, A.PAY_REM, A.PAY_DD, A.CHK_DD, A.INT_DAYS, A.CLS_REM, A.HS_ID, A.AMTN_IRP, A.AMT_IRP, A.TAX_IRP,A.CNT_NAME,A.OTH_NAME,A.TEL_NO,A.CELL_NO,A.CNT_ADR,")
                .Append(" A.MOB_ID FROM  MF_MOUT A")
                .Append(" LEFT JOIN CONTACT C ON C.CUS_NO = A.CUS_NO AND C.CNT_NO = A.CNT_NO ")
                .Append(" LEFT JOIN TF_MON H ON A.RP_NO=H.RP_NO and H.RP_ID='1'")
                .Append(" left join TF_MON4 N4 on A.RP_NO=N4.RP_NO and N4.RP_ID='1' and N4.ITM=1")
                .Append(" left join MF_CHK I on N4.CHK_NO=I.CHK_NO and I.CHK_ID='0'")
                .Append(" WHERE OT_NO = @OT_NO AND OT_ID=@OT_ID;")
                .Append(" SELECT (ISNULL(MA.QTY, 0) - ISNULL(MA.QTY_MTN, 0))  ")
                .Append(" * (CASE WHEN MA.UNIT = '1' THEN 1 WHEN MA.UNIT = '2' THEN P.PK2_QTY WHEN MA.UNIT = '3' THEN P.PK3_QTY END) + ISNULL(TF.QTY, 0)  ")
                .Append(" * (CASE WHEN TF.UNIT = '1' THEN 1 WHEN TF.UNIT = '2' THEN P.PK2_QTY WHEN TF.UNIT = '3' THEN P.PK3_QTY END) AS QTY_OS_ORG,  ")
                .Append(" (ISNULL(SA.QTY, 0) - ISNULL(SA.QTY_RTN, 0) - ISNULL(SA.QTY_OI, 0))  ")
                .Append(" * (CASE WHEN SA.UNIT = '1' THEN 1 WHEN SA.UNIT = '2' THEN P.PK2_QTY WHEN SA.UNIT = '3' THEN P.PK3_QTY END)  ")
                .Append(" + TF.QTY * (CASE WHEN TF.UNIT = '1' THEN 1 WHEN TF.UNIT = '2' THEN P.PK2_QTY WHEN TF.UNIT = '3' THEN P.PK3_QTY END) AS QTY_OS_ORG1, ")
                .Append(" TF.OT_ID, TF.OT_NO, TF.ITM, TF.PRD_NO,TF.PRD_NAME, P.SPC, TF.PRD_MARK, TF.QTY, TF.UNIT, TF.WC_NO, TF.MA_ID, TF.MA_NO, TF.QTY_FIN, TF.MA_ITM,  ")
                .Append(" TF.SA_NO, TF.SA_ITM, TF.MTN_DD, TF.EST_DD, TF.RTN_DD, TF.MTN_TYPE, TF.MTN_ALL_ID, TF.REM, TF.KEY_ITM,TF.CST_FT,TF.CST_ML_FT ")
                .Append(" FROM  TF_MOUT AS TF LEFT OUTER JOIN TF_MA AS MA ON MA.MA_ID = TF.MA_ID AND MA.MA_NO = TF.MA_NO AND MA.KEY_ITM = TF.MA_ITM")
                .Append(" LEFT OUTER JOIN TF_PSS AS SA ON TF.MA_ID = SA.PS_ID AND TF.MA_NO = SA.PS_NO AND TF.MA_ITM = SA.PRE_ITM LEFT JOIN PRDT P ON P.PRD_NO=TF.PRD_NO")
                .Append(" WHERE OT_NO = @OT_NO AND OT_ID=@OT_ID;")
                .Append(" SELECT  TF1.OT_ID,TF1.OT_NO, TF1.OT_ITM, TF1.ITM, TF1.WH, TF1.PRD_NO,TF1.PRD_NAME,prdt.SPC,PRDT.NAME AS PRD_NAME, TF1.PRD_MARK, TF1.QTY, TF1.UNIT, TF1.TAX_RTO, TF1.UP, TF1.AMTN_NET, TF1.AMT, TF1.TAX, TF1.DIS_CNT, TF1.QTY_OT, TF1.BAT_NO,TF1. KEY_ITM,TF1.VALID_DD,TF1.CHK_RTN ")
                .Append(" FROM  TF_MOUT1 TF1 LEFT JOIN PRDT ON PRDT.PRD_NO=TF1.PRD_NO")
                .Append(" WHERE TF1.OT_NO=@OT_NO AND TF1.OT_ID=@OT_ID");
            this.FillDataset(_sql.ToString(), _ds, new string[] { "MF_MOUT", "TF_MOUT", "TF_MOUT_CL" }, _sp);

            DataColumn[] _dcPk = null;
            _dcPk = new DataColumn[2];
            _dcPk[0] = _ds.Tables["MF_MOUT"].Columns["OT_ID"];
            _dcPk[1] = _ds.Tables["MF_MOUT"].Columns["OT_NO"];
            _ds.Tables["MF_MOUT"].PrimaryKey = _dcPk;
            _dcPk = new DataColumn[3];
            _dcPk[0] = _ds.Tables["TF_MOUT"].Columns["OT_ID"];
            _dcPk[1] = _ds.Tables["TF_MOUT"].Columns["OT_NO"];
            _dcPk[2] = _ds.Tables["TF_MOUT"].Columns["ITM"];
            _ds.Tables["TF_MOUT"].PrimaryKey = _dcPk;
            _dcPk = new DataColumn[4];
            _dcPk[0] = _ds.Tables["TF_MOUT_CL"].Columns["OT_ID"];
            _dcPk[1] = _ds.Tables["TF_MOUT_CL"].Columns["OT_NO"];
            _dcPk[2] = _ds.Tables["TF_MOUT_CL"].Columns["ITM"];
            _dcPk[3] = _ds.Tables["TF_MOUT_CL"].Columns["OT_ITM"];
            _ds.Tables["TF_MOUT_CL"].PrimaryKey = _dcPk;

            DataColumn[] _dcA1 = new DataColumn[2];
            _dcA1[0] = _ds.Tables["MF_MOUT"].Columns["OT_ID"];
            _dcA1[1] = _ds.Tables["MF_MOUT"].Columns["OT_NO"];
            DataColumn[] _dcA2 = new DataColumn[2];
            _dcA2[0] = _ds.Tables["TF_MOUT"].Columns["OT_ID"];
            _dcA2[1] = _ds.Tables["TF_MOUT"].Columns["OT_NO"];

            DataColumn[] _dcB1 = new DataColumn[3];
            _dcB1[0] = _ds.Tables["TF_MOUT"].Columns["OT_ID"];
            _dcB1[1] = _ds.Tables["TF_MOUT"].Columns["OT_NO"];
            _dcB1[2] = _ds.Tables["TF_MOUT"].Columns["KEY_ITM"];
            DataColumn[] _dcB2 = new DataColumn[3];
            _dcB2[0] = _ds.Tables["TF_MOUT_CL"].Columns["OT_ID"];
            _dcB2[1] = _ds.Tables["TF_MOUT_CL"].Columns["OT_NO"];
            _dcB2[2] = _ds.Tables["TF_MOUT_CL"].Columns["OT_ITM"];

            _ds.Relations.Add("MF_MOUTTF_MOUT", _dcA1, _dcA2);
            _ds.Relations.Add("TF_MOUTTF_MOUT_CL", _dcB1, _dcB2);
            return _ds;
        }

        /// <summary>
        /// 根据itm 获取维修材料data 
        /// </summary>
        /// <param name="otId">otid</param>
        /// <param name="otNo">otno</param>
        /// <returns>ds</returns>
        public SunlikeDataSet GetDataTf1(string otId, string otNo)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();

            SqlParameter[] _sp = new SqlParameter[2];
            _sp[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 2);
            _sp[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _sp[0].Value = otId;
            _sp[1].Value = otNo;

            StringBuilder _sql = new StringBuilder();
            _sql.Append(" SELECT  TF1.OT_ID,TF1.OT_NO, TF1.OT_ITM, TF1.ITM, TF1.WH, TF1.PRD_NO,TF1.PRD_NAME,TF1.PRD_MARK, TF1.QTY, TF1.UNIT, TF1.TAX_RTO, TF1.UP, TF1.AMTN_NET, TF1.AMT, TF1.TAX, TF1.DIS_CNT, TF1.QTY_OT, TF1.BAT_NO,TF1.KEY_ITM,TF1.VALID_DD ")
                .Append(" FROM  TF_MOUT1 TF1 ")
                .Append(" WHERE TF1.OT_NO=@OT_NO AND TF1.OT_ID=@OT_ID ");
            this.FillDataset(_sql.ToString(), _ds, new string[] { "TF_MOUT_CL" }, _sp);
            return _ds;
        }

        #region 暂时不用这个方法
        ///// <summary>
        ///// 根据itm 获取维修材料data 
        ///// </summary>
        ///// <param name="otId">otid</param>
        ///// <param name="otNo">otno</param>
        ///// <param name="otItm">otitm</param>
        ///// <returns>ds</returns>
        //public SunlikeDataSet GetDataTf1(string otId, string otNo,string otItm)
        //{
        //    SunlikeDataSet _ds = new SunlikeDataSet();
        //    SqlParameter[] _sp = new SqlParameter[3];
        //    _sp[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 2);
        //    _sp[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
        //    _sp[2] = new SqlParameter("@OT_ITM", SqlDbType.VarChar, 20);
        //    _sp[0].Value = otId;
        //    _sp[1].Value = otNo;
        //    _sp[2].Value = otItm;

        //    StringBuilder _sql = new StringBuilder();
        //    _sql.Append(" SELECT  TF1.OT_ID,TF1.OT_NO, TF1.OT_ITM, TF1.ITM, TF1.WH, TF1.PRD_NO,TF1.PRD_MARK, TF1.QTY, TF1.UNIT, TF1.TAX_RTO, TF1.UP, TF1.AMTN_NET, TF1.AMT, TF1.TAX, TF1.DIS_CNT, TF1.QTY_OT, TF1.BAT_NO,TF1.KEY_ITM,TF1.VALID_DD ")
        //        .Append(" FROM  MF_MOUT MF INNER JOIN TF_MOUT AS TF ON MF.OT_ID = TF.OT_ID AND MF.OT_NO = TF.OT_NO INNER JOIN TF_MOUT1 AS TF1 ON TF.OT_ID = TF1.OT_ID AND TF.OT_NO = TF1.OT_NO AND TF.KEY_ITM = TF1.OT_ITM ")
        //        .Append(" WHERE TF1.OT_NO=@OT_NO AND TF1.OT_ID=@OT_ID AND OT_ITM=@OT_ITM ");
        //    this.FillDataset(_sql.ToString(), _ds, new string[] { "TF_MOUT_CL" }, _sp);
        //    return _ds;
        //}
        #endregion

        #region 结案作业

        /// <summary>
        /// 在单据上打上结案标记
        /// </summary>
        /// <param name="otId"></param>
        /// <param name="otNo"></param>
        /// <param name="cls_name"></param>
        /// <param name="close"></param>
        public void DoCloseBill(string otId, string otNo, string cls_name, bool close)
        {

            string _sqlStr = " UPDATE MF_MOUT SET CLS_ID=@CLS_ID,CLS_DATE=getdate() WHERE  OT_ID=@OT_ID AND OT_NO=@OT_NO";
            if (System.Collections.CaseInsensitiveComparer.Default.Compare(cls_name, "CLS_ID") != 0)
                _sqlStr = " UPDATE MF_MOUT SET CLS_LJ_ID=@CLS_ID WHERE  OT_ID=@OT_ID AND OT_NO=@OT_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = otId;
            _sqlPara[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = otNo;
            _sqlPara[2] = new SqlParameter("@CLS_ID", SqlDbType.VarChar, 1);
            _sqlPara[2].Value = close ? "T" : "F";

            this.ExecuteNonQuery(_sqlStr, _sqlPara);
            ClearTfCst(otId, otNo);
        }

        #endregion

        #region IAuditing
        /// <summary>
        /// 修改结案标记、审核人和审核日期

        /// </summary>
        /// <param name="ot_No">BOM代号</param>
        /// <param name="chk_Man">审核人</param>
        /// <param name="cls_Date">审核日期</param>
        public void UpdateChkMan(string ot_No, string chk_Man, DateTime cls_Date)
        {
            string _sql = "UPDATE MF_MOUT SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE OT_NO=@OT_NO ";//AND OT_ID='OT'";
            SqlParameter[] _spc = new SqlParameter[3];
            _spc[0] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _spc[2] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 38);
            _spc[2].Value = ot_No;

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

        #region 回写维修申请单



        public void UpdateMA(string os_Id, string os_No, string itm, decimal qty)
        {
            string sql = " UPDATE TF_MA SET QTY_MTN=ISNULL(QTY_MTN,0)+@QTY WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND KEY_ITM=@ITM ;"
            + " IF (@@ROWCOUNT > 0) "
            + " BEGIN \n"
            + " UPDATE MF_MA SET CLS_ID='T',CLS_AUTO='T' WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND (SELECT COUNT(MA_ID) FROM TF_MA WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND ISNULL(QTY,0)-ISNULL(QTY_MTN,0)>0)=0;"
            + " UPDATE MF_MA SET CLS_ID='F',CLS_AUTO='F' WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND (SELECT COUNT(MA_ID) FROM TF_MA WHERE MA_ID=@OS_ID AND MA_NO=@OS_NO AND ISNULL(QTY,0)-ISNULL(QTY_MTN,0)>0)>0;"
            + " END ";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[0].Value = os_Id;
            _spc[1].Value = os_No;
            _spc[2].Value = Convert.ToInt16(itm);
            _spc[3].Value = qty;
            this.ExecuteNonQuery(sql, _spc);
        }
        #endregion

        #region 回写SA销货单

        public void UpdateSA(string os_Id, string os_No, string itm, decimal qty)
        {
            string sql = " UPDATE TF_PSS SET QTY_OI=ISNULL(QTY_OI,0)+@QTY WHERE PS_ID=@OS_ID AND PS_NO=@OS_NO AND PRE_ITM=@ITM  ;";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[0].Value = os_Id;
            _spc[1].Value = os_No;
            _spc[2].Value = Convert.ToInt16(itm);
            _spc[3].Value = qty;
            this.ExecuteNonQuery(sql, _spc);
        }
        #endregion

        #region 取得超交数量
        ///// <summary>
        /////取得超交数量
        ///// </summary>
        ///// <param name="_sqID">_sqID</param>
        ///// <param name="_sqNO">_sqNO</param>
        ///// <param name="_sqITM">_sqITM</param>
        ///// <returns>取得超交数量</returns>
        //public decimal getExQty(string _sqID, string _sqNO, string _sqITM, decimal _qty)
        //{
        //    decimal _exqty = 0;
        //    string sql = " SELECT ISNULL(qty_ot,0)-ISNULL(qty,0) as EXQTY  FROM TF_MOUT1 WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND KEY_ITM=@ITM ;";
        //    SqlParameter[] _spc = new SqlParameter[3];
        //    _spc[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 12);
        //    _spc[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
        //    _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
        //    _spc[0].Value = _sqID;
        //    _spc[1].Value = _sqNO;
        //    _spc[2].Value = _sqITM;

        //    try
        //    {
        //        _exqty = Convert.ToDecimal(this.ExecuteScalar(sql, _spc));
        //        if (_exqty < 0)
        //        {
        //            if (_qty < 0 || (_qty > 0 && _qty <= Math.Abs(_exqty)))//不超交




        //                _exqty = 0;

        //        }

        //    }
        //    catch { }
        //    return _exqty;
        //}
        #endregion

        #region 判断是否已经被结案



        /// <summary>
        /// 判断是否已经被结案

        /// </summary>
        /// <param name="_sqID">_sqID</param>
        /// <param name="_sqNO">_sqNO</param>
        /// <param name="isClsLj">是否领料结案</param>
        /// <returns></returns>
        public bool isClosed(string _sqID, string _sqNO, bool isClsLj)
        {
            string _sql = "select " + (isClsLj ? "isnull(CLS_LJ_ID,'F')" : "isnull(CLS_ID,'F')") + " from mf_mout where ot_id=@OT_ID and ot_no=@OT_NO";
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = _sqID;
            _spc[1].Value = _sqNO;
            return Convert.ToString(this.ExecuteScalar(_sql, _spc)) == "T";
        }
        #endregion

        #region 更改结案标记
        public void ClsMout(string _sqID, string _sqNO)
        {
            string _sql = "  UPDATE MF_MOUT SET CLS_ID='T',CLS_AUTO='T' WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND (SELECT COUNT(OT_ID) FROM TF_MOUT WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND ISNULL(QTY,0)-ISNULL(QTY_FIN,0)>0)=0;"
                         + " UPDATE MF_MOUT SET CLS_ID='F',CLS_AUTO='F' WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND (SELECT COUNT(OT_ID) FROM TF_MOUT WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND ISNULL(QTY,0)-ISNULL(QTY_FIN,0)>0)>0;";
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = _sqID;
            _spc[1].Value = _sqNO;
            this.ExecuteNonQuery(_sql, _spc);
            ClearTfCst(_sqID, _sqNO);
        }
        public void ClsMoutLj(string _sqID, string _sqNO)
        {
            ClsMoutLj(_sqID, _sqNO, true);
        }
        public void ClsMoutLj(string _sqID, string _sqNO, bool isAuto)
        {
            string _sql = " UPDATE MF_MOUT SET CLS_LJ_ID='T' " + (isAuto ? ",CLS_LJ_AUTO='T'" : "") + " WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND ((SELECT COUNT(OT_ID) FROM TF_MOUT1 WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO  AND (ISNULL(QTY,0)-ISNULL(QTY_OT,0)>0))=0 OR ISNULL(CLS_ID,'')='T');"
                        + " UPDATE MF_MOUT SET CLS_LJ_ID='F'" + (isAuto ? ",CLS_LJ_AUTO='F'" : "") + " WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND ((SELECT COUNT(OT_ID) FROM TF_MOUT1 WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO  AND ISNULL(QTY,0)-ISNULL(QTY_OT,0)>0))>0 AND ISNULL(CLS_ID,'')!='T'";
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = _sqID;
            _spc[1].Value = _sqNO;
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion

        #region 清除成本
        public void ClearTfCst(string _sqID, string _sqNO)
        {
            string _sql = @"  UPDATE  TF_MOUT SET CST_FT = NULL,CST_ML_FT=NULL 
                              WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND EXISTS(SELECT 1 FROM MF_MOUT 
                              WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND ISNULL(CLS_ID,'F')='F')";
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = _sqID;
            _spc[1].Value = _sqNO;
            this.ExecuteNonQuery(_sql, _spc);
        }
        #endregion

        #region 回写外派维修单



        /// <summary>
        /// 回写来源单的已完成量
        /// </summary>
        /// <param name="ot_Id">ot_Id</param>
        /// <param name="ot_No">ot_No</param>
        /// <param name="itm">itm</param>
        /// <param name="qty">回写库存</param>
        public void UpdateMoutTf(string ot_Id, string ot_No, string itm, decimal qty)
        {
            string sql = " UPDATE TF_MOUT SET QTY_FIN=ISNULL(QTY_FIN,0)+@QTY WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND KEY_ITM=@ITM ;";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[0].Value = ot_Id;
            _spc[1].Value = ot_No;
            _spc[2].Value = Convert.ToInt16(itm);
            _spc[3].Value = qty;
            this.ExecuteNonQuery(sql, _spc);
        }
        /// <summary>
        /// 回写来源单的已领料量
        /// </summary>
        /// <param name="_sqID">_sqID</param>
        /// <param name="_sqNO">_sqNO</param>
        /// <param name="_sqITM">_sqITM</param>
        /// <param name="_qty">回写库存</param>
        public void UpdateMoutTf1(string _sqID, string _sqNO, string _sqITM, decimal _qty)
        {
            string sql = " UPDATE TF_MOUT1 SET QTY_OT=ISNULL(QTY_OT,0)+@QTY WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO AND KEY_ITM=@ITM ;";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _spc[2] = new SqlParameter("@ITM", SqlDbType.VarChar);
            _spc[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _spc[0].Value = _sqID;
            _spc[1].Value = _sqNO;
            _spc[2].Value = Convert.ToInt16(_sqITM);
            _spc[3].Value = _qty;
            this.ExecuteNonQuery(sql, _spc);
        }
        #endregion

        #region 回写维修成本切制标记
        /// <summary>
        /// 回写维修成本切制标记
        /// </summary>
        /// <param name="otId"></param>
        /// <param name="otNo"></param>
        /// <param name="cbId"></param>
        public void UpdateCbId(string otId, string otNo, bool cbId)
        {
            string sqlStr = " IF EXISTS( SELECT CB_ID FROM MF_MOUT WHERE OT_ID=@OT_ID AND OT_NO = @OT_NO)"
                    + " BEGIN "
                    + "     UPDATE MF_MOUT SET CB_ID=@CB_ID WHERE OT_ID=@OT_ID AND OT_NO = @OT_NO "
                    + " END ";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 12);
            _sqlPara[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _sqlPara[2] = new SqlParameter("@CB_ID", SqlDbType.VarChar,1);
            _sqlPara[0].Value = otId;
            _sqlPara[1].Value = otNo;
            _sqlPara[2].Value = cbId.ToString().ToUpper().Substring(0,1);
            this.ExecuteNonQuery(sqlStr, _sqlPara);
        }
        #endregion

        #region 更新维修单凭证号码

        /// <summary>
        /// 更新缴库单凭证号码

        /// </summary>
        /// <param name="otId"></param>
        /// <param name="otNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string otId, string otNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_MOUT SET VOH_NO=@VOH_NO WHERE OT_ID=@OT_ID AND OT_NO=@OT_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OT_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = otId;

            _sqlPara[1] = new SqlParameter("@OT_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = otNo;

            _sqlPara[2] = new SqlParameter("@VOH_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = vohNo;
            try
            {
                this.ExecuteNonQuery(_sqlStr, _sqlPara);
                _result = true;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _result;

        }
        #endregion

    }
}
