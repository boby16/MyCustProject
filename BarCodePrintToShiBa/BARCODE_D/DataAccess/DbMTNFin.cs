using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbMTNFin : DbObject
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="connectionString"></param>
        public DbMTNFin(string connectionString)  : base(connectionString)
        {

        }

        /// <summary>
        /// 取数据


        /// </summary>
        /// <param name="owId">OT</param>
        /// <param name="owNo">外派维修单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string owId,string owNo)
        {
            SunlikeDataSet _ds=new SunlikeDataSet();

            SqlParameter[] _sp = new SqlParameter[2];
            _sp[0] = new SqlParameter("@OW_ID", SqlDbType.VarChar, 2);
            _sp[1] = new SqlParameter("@OW_NO", SqlDbType.VarChar, 20);
            _sp[0].Value = owId;
            _sp[1].Value = owNo;

            StringBuilder _sql = new StringBuilder();
            _sql.Append(" SELECT A.KIND_NO,A.OW_ID, A.OW_NO, A.OW_DD, A.OT_ID, A.OT_NO, A.CUS_NO, A.DEP, A.SAL_NO, A.BIL_TYPE, A.CUR_ID, A.EXC_RTO, A.TAX_ID, A.ZHANG_ID, A.REM, A.SYS_DATE, A.CLS_DATE, A.PRT_SW, A.USR, A.CHK_MAN, A.CNT_NO, A.CNT_REM, A.DIS_CNT, A.INV_NO, A.RP_NO, H.AMT_BB, H.AMTN_BB, H.AMT_BC, H.AMTN_BC, H.CACC_NO, H.BACC_NO, H.AMT_CHK, H.AMTN_CHK, H.AMTN_CLS, I.CHK_NO, I.CHK_KND, I.BANK_NO, I.CRECARD_NO, I.END_DD, A.PAY_MTH, A.PAY_DAYS, A.CHK_DAYS, A.PAY_REM, A.PAY_DD, A.CHK_DD, A.INT_DAYS, A.CLS_REM, A.AMTN_IRP, A.AMT_IRP, A.TAX_IRP, A.CLS_LZ_ID, A.CLS_LZ_AUTO, A.AMTN_NET, A.TAX, A.AMTN_INT, A.AMT_INT, A.AMT_CLS, A.AMTN_NET_CLS, A.TAX_CLS, A.QTY_CLS, A.KP_ID, A.TURN_ID, A.ARP_NO, H.AMT_CHK1 AS AMT_CHK, H.AMT_OTHER, H.AMTN_OTHER, I.BACC_NO AS BACC_NO_CHK,A.CNT_NAME,A.OTH_NAME,A.TEL_NO,A.CELL_NO,A.CNT_ADR,A.REM_RUN,A.REM_CUST,A.CHK_CUST,A.VOH_ID,A.VOH_NO,A.LOCK_MAN,A.MOB_ID ")
                .Append(" FROM MF_MFIN AS A LEFT JOIN")
                .Append(" TF_MON AS H ON A.RP_NO = H.RP_NO AND H.RP_ID = '1' LEFT OUTER JOIN")
                .Append(" CONTACT C ON C.CUS_NO = A.CUS_NO AND C.CNT_NO = A.CNT_NO LEFT JOIN")
               //.Append(" TF_MON1 J on A.RP_NO=J.RP_NO and J.RP_ID='1' and J.ITM=1 LEFT OUTER JOIN")
                .Append(" TF_MON4 N4 on A.RP_NO=N4.RP_NO and N4.RP_ID='1' and N4.ITM=1  LEFT OUTER JOIN")
                .Append(" MF_CHK AS I ON N4.CHK_NO = I.CHK_NO AND I.CHK_ID = '0'")
                .Append(" WHERE OW_NO = @OW_NO AND OW_ID=@OW_ID;")
                //.Append(" SELECT  ISNULL(OT.QTY, 0) - ISNULL(OT.QTY_FIN, 0)+ISNULL(TF.QTY,0) AS QTY_OS_ORG,TF.ITM, TF.PRD_NO,PRDT.SPC, TF.PRD_MARK, TF.QTY, TF.UNIT, TF.WC_NO, TF.SA_NO, TF.SA_ITM, TF.MTN_DD,TF.KEY_ITM, TF.RTN_DD,TF.MTN_TYPE, TF.MTN_ALL_ID, TF.REM, TF.OW_ID, TF.OW_NO, TF.OT_ID, TF.OT_NO, TF.OT_ITM ")
                //.Append(" FROM  TF_MFIN AS TF LEFT OUTER JOIN TF_MOUT AS OT ON OT.OT_ID = TF.OT_ID AND OT.OT_NO = TF.OT_NO AND OT.KEY_ITM = TF.OT_ITM")
                .Append("SELECT     (ISNULL(OT.QTY, 0) - ISNULL(OT.QTY_FIN, 0)) ")
                .Append(" * (CASE WHEN OT.UNIT = '1' THEN 1 WHEN OT.UNIT = '2' THEN P.PK2_QTY WHEN OT.UNIT = '3' THEN P.PK3_QTY END) + ISNULL(TF.QTY, 0) ")
                .Append(" * (CASE WHEN TF.UNIT = '1' THEN 1 WHEN TF.UNIT = '2' THEN P.PK2_QTY WHEN TF.UNIT = '3' THEN P.PK3_QTY END) AS QTY_OS_ORG, TF.ITM, ")
                .Append(" TF.PRD_NO,TF.PRD_NAME, P.SPC, TF.PRD_MARK, TF.QTY, TF.UNIT, TF.WC_NO, TF.SA_NO, TF.SA_ITM, TF.MTN_DD, TF.KEY_ITM, TF.RTN_DD, TF.MTN_TYPE,")
                .Append(" TF.MTN_ALL_ID, TF.REM, TF.OW_ID, TF.OW_NO, TF.OT_ID, TF.OT_NO, TF.OT_ITM")
                .Append(" FROM TF_MFIN AS TF LEFT OUTER JOIN TF_MOUT AS OT ON OT.OT_ID = TF.OT_ID AND OT.OT_NO = TF.OT_NO AND OT.KEY_ITM = TF.OT_ITM LEFT OUTER JOIN PRDT AS P ON P.PRD_NO = TF.PRD_NO ")
                .Append(" WHERE OW_NO = @OW_NO AND OW_ID=@OW_ID")
                .Append(" SELECT  TF1.OW_ID, TF1.OW_NO, TF1.OW_ITM, TF1.ITM, TF1.WH, TF1.PRD_NO,TF1.PRD_NAME, PRDT.SPC,TF1.PRD_MARK, TF1.QTY, TF1.UNIT, TF1.TAX_RTO, TF1.UP, TF1.AMTN_NET, TF1.AMT, TF1.TAX, TF1.DIS_CNT, TF1.MTN_DD, TF1.BAT_NO, TF1.AMT_FP,TF1.AMTN_NET_FP, TF1.TAX_FP, TF1.QTY_FP, TF1.VALID_DD, TF1.KEY_ITM ")
                .Append(" FROM  TF_MFIN1 TF1 LEFT JOIN PRDT ON PRDT.PRD_NO=TF1.PRD_NO")
                .Append(" WHERE TF1.OW_NO=@OW_NO AND TF1.OW_ID=@OW_ID ");
            this.FillDataset(_sql.ToString(), _ds, new string[] { "MF_MFIN", "TF_MFIN", "TF_MFIN_CL" }, _sp);

            DataColumn[] _dcPk = null;
            _dcPk = new DataColumn[2];
            _dcPk[0] = _ds.Tables["MF_MFIN"].Columns["OW_ID"];
            _dcPk[1] = _ds.Tables["MF_MFIN"].Columns["OW_NO"];
            _ds.Tables["MF_MFIN"].PrimaryKey = _dcPk;
            _dcPk = new DataColumn[3];
            _dcPk[0] = _ds.Tables["TF_MFIN"].Columns["OW_ID"];
            _dcPk[1] = _ds.Tables["TF_MFIN"].Columns["OW_NO"];
            _dcPk[2] = _ds.Tables["TF_MFIN"].Columns["ITM"];
            _ds.Tables["TF_MFIN"].PrimaryKey = _dcPk;
            _dcPk = new DataColumn[4];
            _dcPk[0] = _ds.Tables["TF_MFIN_CL"].Columns["OW_ID"];
            _dcPk[1] = _ds.Tables["TF_MFIN_CL"].Columns["OW_NO"];
            _dcPk[2] = _ds.Tables["TF_MFIN_CL"].Columns["ITM"];
            _dcPk[3] = _ds.Tables["TF_MFIN_CL"].Columns["OW_ITM"];
            _ds.Tables["TF_MFIN_CL"].PrimaryKey = _dcPk;

            DataColumn[] _dcA1 = new DataColumn[2];
            _dcA1[0] = _ds.Tables["MF_MFIN"].Columns["OW_ID"];
            _dcA1[1] = _ds.Tables["MF_MFIN"].Columns["OW_NO"];
            DataColumn[] _dcA2 = new DataColumn[2];
            _dcA2[0] = _ds.Tables["TF_MFIN"].Columns["OW_ID"];
            _dcA2[1] = _ds.Tables["TF_MFIN"].Columns["OW_NO"];

            DataColumn[] _dcB1 = new DataColumn[3];
            _dcB1[0] = _ds.Tables["TF_MFIN"].Columns["OW_ID"];
            _dcB1[1] = _ds.Tables["TF_MFIN"].Columns["OW_NO"];
            _dcB1[2] = _ds.Tables["TF_MFIN"].Columns["KEY_ITM"];
            DataColumn[] _dcB2 = new DataColumn[3];
            _dcB2[0] = _ds.Tables["TF_MFIN_CL"].Columns["OW_ID"];
            _dcB2[1] = _ds.Tables["TF_MFIN_CL"].Columns["OW_NO"];
            _dcB2[2] = _ds.Tables["TF_MFIN_CL"].Columns["OW_ITM"];

            _ds.Relations.Add("MF_MFINTF_MFIN", _dcA1, _dcA2);
            _ds.Relations.Add("TF_MFINTF_MFIN_CL", _dcB1, _dcB2);
            return _ds;           
        }

        /// <summary>
        /// 是否已经做成本的凭证切制品


        /// </summary>
        /// <param name="owId"></param>
        /// <param name="owNo"></param>
        /// <returns></returns>
        public bool IsCbidT(string owId, string owNo) 
        {
            string sql = @"SELECT OT.CB_ID FROM MF_MOUT OT
                           INNER JOIN MF_MFIN OW ON OT.OT_ID=OW.OT_ID AND OT.OT_NO=OW.OT_NO 
                           WHERE OW.OW_ID=@OW_ID AND OW.OW_NO=@OW_NO";
            SqlParameter[] _sp = new SqlParameter[2];
            _sp[0] = new SqlParameter("@OW_ID", SqlDbType.VarChar, 2);
            _sp[1] = new SqlParameter("@OW_NO", SqlDbType.VarChar, 20);
            _sp[0].Value = owId;
            _sp[1].Value = owNo;
          return  CaseInsensitiveComparer.Default.Compare("T",Convert.ToString(ExecuteScalar(sql, _sp)))==0;        
        }


        #region IAuditing
        /// <summary>
        /// 修改结案标记、审核人和审核日期


        /// </summary>
        ///<param name="ow_Id">ow_Id</param>
        /// <param name="ow_No">ow_No</param>
        /// <param name="chk_Man">审核人</param>
        /// <param name="cls_Date">审核日期</param>
        public void UpdateChkMan(string ow_Id,string ow_No, string chk_Man, DateTime cls_Date)
        {
            string _sql = "UPDATE MF_MFIN SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DATE WHERE OW_NO=@OW_NO AND OW_ID=@OW_ID";
            SqlParameter[] _spc = new SqlParameter[4];
            _spc[0] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _spc[1] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _spc[2] = new SqlParameter("@OW_NO", SqlDbType.VarChar, 38);
            _spc[2].Value = ow_No;
            _spc[3] = new SqlParameter("@OW_ID", SqlDbType.VarChar, 38);
            _spc[3].Value = ow_Id;

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
    
        /// <summary>
        /// 更新单据的立账单号


        /// </summary>
        /// <param name="owID">单据代号</param>
        /// <param name="owNo">单据号码</param>
        /// <param name="ArpNo">立账单号</param>
        public void UpdateArpNo(string owID, string owNo, string ArpNo)
        {
            string _sql = "update MF_MFIN set ARP_NO=@ArpNo where OW_ID=@OWID and OW_NO=@OWNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ArpNo", SqlDbType.VarChar, 20);
            _spc[0].Value = ArpNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@OWID", SqlDbType.Char, 2);
            _spc[1].Value = owID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@OWNo", SqlDbType.VarChar, 20);
            _spc[2].Value = owNo;
            this.ExecuteNonQuery(_sql, _spc);
        }

        #region 更新维修完工单凭证号码


        /// <summary>
        /// 更新维修完工单凭证号码


        /// </summary>
        /// <param name="owId"></param>
        /// <param name="owNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string owId, string owNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_MFIN SET VOH_NO=@VOH_NO WHERE OW_ID=@OW_ID AND OW_NO=@OW_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OW_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = owId;

            _sqlPara[1] = new SqlParameter("@OW_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = owNo;

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


        #region 更新发票资讯
        /// <summary>
        /// 
        /// </summary>
        /// <param name="turnId"></param>
        /// <param name="lzclsId"></param>
        /// <param name="amtCls"></param>
        /// <param name="amtn_netCls"></param>
        /// <param name="taxCls"></param>
        /// <param name="qtyCls"></param>
        /// <param name="owId"></param>
        /// <param name="owNo"></param>
        /// <param name="clsLzAuto"></param>
        /// <param name="invNo"></param>
        public void UpdateInvIkHeadData(string turnId, string lzclsId, decimal amtCls, decimal amtn_netCls, decimal taxCls, decimal qtyCls, string owId, string owNo, string clsLzAuto,string invNo)
        {
            string _sql = "update MF_MFIN set TURN_ID=@TURN_ID,CLS_LZ_ID=@LZ_CLS_ID,AMT_CLS=@AMT_CLS,AMTN_NET_CLS=@AMTN_NET_CLS,TAX_CLS=@TAX_CLS,QTY_CLS=@QTY_CLS,CLS_LZ_AUTO=@CLS_LZ_AUTO,INV_NO=@INV_NO " +
                           " Where  OW_ID=@OW_ID and OW_NO=@OW_NO" ;

            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[10];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TURN_ID", SqlDbType.VarChar, 1);
            _spc[0].Value = turnId;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = lzclsId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@AMT_CLS", SqlDbType.Decimal, 0);
            _spc[2].Value = amtCls;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_CLS", SqlDbType.Decimal, 0);
            _spc[3].Value = amtn_netCls;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@TAX_CLS", SqlDbType.Decimal, 0);
            _spc[4].Value = taxCls;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@QTY_CLS", SqlDbType.Decimal, 0);
            _spc[5].Value = qtyCls;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@OW_ID", SqlDbType.VarChar, 2);
            _spc[6].Value = owId;

            _spc[7] = new System.Data.SqlClient.SqlParameter("@OW_NO", SqlDbType.VarChar, 20);
            _spc[7].Value = owNo;

            _spc[8] = new System.Data.SqlClient.SqlParameter("@CLS_LZ_AUTO", SqlDbType.VarChar, 1);
            _spc[8].Value = clsLzAuto;

            _spc[9] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[9].Value = invNo;
            this.ExecuteNonQuery(_sql, _spc);
        }
        /// <summary>
        ///补开发票回写来源单表身栏位

        /// </summary>
        /// <param name="amtFp">已开金额</param>
        /// <param name="amtn_netFp">已开未税金额</param>
        /// <param name="taxFp">已开税额</param>
        /// <param name="qtyFp">已开数量</param>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="itm">表身项次</param>
        public void UpdateInvIkBodyData(decimal amtFp, decimal amtn_netFp, decimal taxFp, decimal qtyFp, string owId, string owNo, int itm)
        {
            string _sql = "update TF_MFIN1 set AMT_FP=@AMT_FP,AMTN_NET_FP=@AMTN_NET_FP,TAX_FP=@TAX_FP,QTY_FP=@QTY_FP Where OW_ID=@OW_ID and  OW_NO=@OW_NO and ITM=@ITM  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[7];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_FP", SqlDbType.Decimal);
            _spc[0].Value = amtFp;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP", SqlDbType.Decimal);
            _spc[1].Value = amtn_netFp;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@TAX_FP", SqlDbType.Decimal);
            _spc[2].Value = taxFp;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@QTY_FP", SqlDbType.Decimal);
            _spc[3].Value = qtyFp;


            _spc[4] = new System.Data.SqlClient.SqlParameter("@OW_NO", SqlDbType.VarChar, 20);
            _spc[4].Value = owNo;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@ITM", SqlDbType.Int);
            _spc[5].Value = itm;


            _spc[6] = new System.Data.SqlClient.SqlParameter("@OW_ID", SqlDbType.VarChar, 2);
            _spc[6].Value = owId;
            this.ExecuteNonQuery(_sql, _spc);

        }
        #endregion
    }
}
