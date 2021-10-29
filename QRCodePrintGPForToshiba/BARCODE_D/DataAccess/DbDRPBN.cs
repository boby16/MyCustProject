using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPPC.
	/// </summary>
	public class DbDRPBN : DbObject
	{
		/// <summary>
		/// 进货单
		/// </summary>
		/// <param name="connectionString">SQL连接字串</param>
		public DbDRPBN(string connectionString) : base(connectionString)
		{
        }

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string blId, string blNo, bool onlyFillSchema)
        {
            string _sql = "SELECT BL_ID,BL_NO,BAT_NO,BL_MOD,AMT_PLED,AMT_FINE,CUS_NO,BL_DD,PAY_DD,REP_BL_NO,ARP_NO,VOH_ID,VOH_NO, \n"
                + "DEP,INV_NO,TRAD_MTH,SEND_MTH,RP_NO,ZHANG_ID,CUR_ID,EXC_RTO,SAL_NO,BL_DAYS,RTN_DD,CLS_ID,TAX,AMTN_NET,AMT_NET, \n"
                + "TAX_ID,ADR,REM,PAY_MTH,PAY_DAYS,CHK_DAYS,PAY_REM,SEND_WH,END_DAYS,CHK_DD,INT_DAYS,EST_DD,USR,CHK_MAN,PRT_SW,CPY_SW, \n"
                + "BIL_NO,CLS_REM,BIL_ID,CLS_DATE,CK_CLS_ID,CB_ID,BIL_TYPE,MOB_ID,LOCK_MAN,SYS_DATE,OS_ID,OS_NO,FJ_NUM,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,PO_ID FROM MF_BLN \n"
                + "WHERE BL_ID = @BL_ID AND BL_NO = @BL_NO; \n"
                + "SELECT BL_ID,BL_NO,ITM,BL_DD,WH,PRD_NO,PRD_NAME,PRD_MARK,UNIT,QTY,QTY1,CST,AMT_RNT,AMTN_RNT_NET,TAX_RNT,UP,AMT,AMTN, \n"
                + "QTY_RTN,QTY1_RTN,PRE_BL_NO,PRE_ITM,EST_DD,CST_STD,UP_QTY1,EST_ITM,BAT_NO,QTY_CK,SUP_PRD_NO,RK_NO,CK_NO,TI_ITM,OS_ID,DIS_CNT, \n"
                + "OS_NO,OS_ITM,TAX_RTO,TAX,VALID_DD,AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,REM,PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT FROM TF_BLN WHERE BL_ID = @BL_ID AND BL_NO = @BL_NO;\n"
                
                + "SELECT BL_ID,BL_NO,BL_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO FROM TF_BLN_B WHERE BL_ID = @BL_ID AND BL_NO = @BL_NO;\n";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@BL_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = blId;
            _aryPt[1] = new SqlParameter("@BL_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = blNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[] { "MF_BLN", "TF_BLN", "TF_BLN_B" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[] { "MF_BLN", "TF_BLN", "TF_BLN_B" }, _aryPt);
            }

            _ds.Relations.Add(new DataRelation("MF_BLNTF_BLN", new DataColumn[] { _ds.Tables["MF_BLN"].Columns["BL_ID"], _ds.Tables["MF_BLN"].Columns["BL_NO"] },
                new DataColumn[] { _ds.Tables["TF_BLN"].Columns["BL_ID"], _ds.Tables["TF_BLN"].Columns["BL_NO"] }));

            _ds.Relations.Add(new DataRelation("TF_BLNTF_BLN_B", new DataColumn[] { _ds.Tables["TF_BLN"].Columns["BL_ID"], _ds.Tables["TF_BLN"].Columns["BL_NO"], _ds.Tables["TF_BLN"].Columns["PRE_ITM"] },
                new DataColumn[] { _ds.Tables["TF_BLN_B"].Columns["BL_ID"], _ds.Tables["TF_BLN_B"].Columns["BL_NO"], _ds.Tables["TF_BLN_B"].Columns["BL_ITM"] }));

            return _ds;
        }
        /// <summary>
        /// UpdateQtyRtn
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="preItm"></param>
        /// <param name="qty"></param>
        public void UpdateQtyRtn(string blId, string blNo, string preItm, decimal qty,string qtyFieldName)
        {
            
            
            string _sql = "";
            if (blId == "PO")
            {
                _sql = "update TF_POS set " + qtyFieldName + "=isNull(" + qtyFieldName + ",0)+@QTY_RTN where OS_ID=@BL_ID and OS_NO=@BL_NO and PRE_ITM=@PRE_ITM \n"
                    + "	if Exists(select OS_NO from TF_POS WHERE OS_ID=@BL_ID and OS_NO=@BL_NO and ( (isnull((QTY),0)-isnull((QTY_PRE),0)) > isnull((QTY_PS),0) )) \n"
                    + "		update MF_POS set CLS_ID='F',BACK_ID=NULL where OS_ID=@BL_ID and OS_NO=@BL_NO AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"
                    + "	else \n"
                    + "		update MF_POS set CLS_ID='T',BACK_ID='PC' where OS_ID=@BL_ID and OS_NO=@BL_NO AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n";
            }
            else
            {
                _sql = "UPDATE TF_BLN SET  " + qtyFieldName + " = ISNULL( " + qtyFieldName + ", 0) + @QTY_RTN WHERE BL_ID = @BL_ID AND BL_NO = @BL_NO AND PRE_ITM = @PRE_ITM; \n"
                   + "IF EXISTS(SELECT 1 FROM TF_BLN WHERE BL_ID = @BL_ID AND BL_NO = @BL_NO AND ISNULL(QTY, 0) > ISNULL(QTY_RTN, 0)) \n"
                   + " UPDATE MF_BLN SET CLS_ID = 'F' WHERE BL_ID = @BL_ID AND BL_NO = @BL_NO \n"
                   + "ELSE \n"
                   + " UPDATE MF_BLN SET CLS_ID = 'T' WHERE BL_ID = @BL_ID AND BL_NO = @BL_NO";
            }
            SqlParameter[] _aryPt = new SqlParameter[4];
            _aryPt[0] = new SqlParameter("@BL_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = blId;
            _aryPt[1] = new SqlParameter("@BL_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = blNo;
            _aryPt[2] = new SqlParameter("@PRE_ITM", SqlDbType.Int);
            _aryPt[2].Value = Convert.ToInt32(preItm);
            _aryPt[3] = new SqlParameter("@QTY_RTN", SqlDbType.Decimal);
            _aryPt[3].Value = qty;
            this.ExecuteNonQuery(_sql, _aryPt);
        }

        /// <summary>
        /// 取未还数量
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public Decimal GetQtyNotRtn(string blId, string blNo, int preItm)
        {
            string _sql = "select (Case When A.UNIT='2' Then B.PK2_QTY When A.UNIT='3' Then B.PK3_QTY Else 1 End )*(isNull(A.QTY,0)-isNull(A.QTY_RTN,0)) QTY_BN from TF_BLN A "
                        + "left join prdt B on A.prd_No=B.prd_no where A.BL_ID=@blId and A.BL_NO=@blNo and A.PRE_ITM=@preItm ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@blId", SqlDbType.Char, 2);
            _spc[0].Value = blId;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@blNo", SqlDbType.VarChar, 20);
            _spc[1].Value = blNo;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@preItm", SqlDbType.Int);
            _spc[2].Value = preItm;
            decimal _result = 0;
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = Convert.ToDecimal(_rObj);
            }
            return _result;
        }

        /// <summary>
        /// 修改审核人和审核日期
        /// </summary>
        /// <param name="blId">单据代号</param>
        /// <param name="blNo">单据号码</param>
        /// <param name="chkMan">审核人</param>
        /// <param name="clsDate">审核日期</param>
        public void UpdateChkMan(string blId, string blNo, string chkMan, DateTime clsDate, string vohNo)
        {
            string _sql = "update MF_BLN set CHK_MAN=@ChkMan,CLS_DATE=@ClsDate,VOH_NO=@VohNo where BL_ID=@BlID and BL_NO=@BlNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[5];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ChkMan", SqlDbType.VarChar, 12);
            if (String.IsNullOrEmpty(chkMan))
            {
                _spc[0].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = chkMan;
            }
            _spc[1] = new System.Data.SqlClient.SqlParameter("@ClsDate", SqlDbType.DateTime);
            if (String.IsNullOrEmpty(chkMan))
            {
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[1].Value = clsDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            _spc[2] = new System.Data.SqlClient.SqlParameter("@BlID", SqlDbType.Char, 2);
            _spc[2].Value = blId;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@BlNo", SqlDbType.VarChar, 20);
            _spc[3].Value = blNo;
            _spc[4] = new System.Data.SqlClient.SqlParameter("@VohNo", SqlDbType.VarChar, 20);
            _spc[4].Value = vohNo;
            this.ExecuteNonQuery(_sql, _spc);
        }
        /// <summary>
        /// UpdateArpNo
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="arpNo"></param>
        public void UpdateArpNo(string blId, string blNo, string arpNo)
        {
            string _sql = "UPDATE MF_BLN SET ARP_NO = @ARP_NO WHERE BL_ID = @BL_ID AND BL_NO = @BL_NO";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@BL_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = blId;
            _aryPt[1] = new SqlParameter("@BL_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = blNo;
            _aryPt[2] = new SqlParameter("@ARP_NO", SqlDbType.VarChar, 20);
            _aryPt[2].Value = arpNo;
            this.ExecuteNonQuery(_sql, _aryPt);
        }

        /// <summary>
        /// 取表身数据
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBodyData(string blId, string blNo, int preItm)
        {
            string _sql = "SELECT TF_BLN.*,ISNULL(MF_BLN.CLS_ID,'F') AS CLS_ID "
            + " FROM TF_BLN JOIN MF_BLN ON MF_BLN.BL_NO=TF_BLN.BL_NO AND MF_BLN.BL_ID=TF_BLN.BL_ID "
            + " WHERE TF_BLN.BL_ID = @BL_ID AND TF_BLN.BL_NO = @BL_NO AND TF_BLN.PRE_ITM = @PRE_ITM";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@BL_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = blId;
            _aryPt[1] = new SqlParameter("@BL_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = blNo;
            _aryPt[2] = new SqlParameter("@PRE_ITM", SqlDbType.SmallInt);
            _aryPt[2].Value = preItm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_BLN" }, _aryPt);
            return _ds;
        }
        #region 更新凭证号码
        /// <summary>
        /// 更新凭证号码
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string blId, string blNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_BLN SET VOH_NO=@VOH_NO WHERE BL_ID=@BL_ID AND BL_NO=@BL_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@BL_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = blId;

            _sqlPara[1] = new SqlParameter("@BL_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = blNo;

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

        #region INVIK & INVLI  回写用



        /// <summary>
        /// 补收发票回写MF_BLN表头档
        /// </summary>
        /// <param name="turnId">会滙开票识别 1.明细 2.滙总</param>
        /// <param name="lzclsId">开票结案注记</param>
        /// <param name="invNo">发票号码</param>
        /// <param name="amtCls">已开金额</param>
        /// <param name="amtn_netCls">已开未税金额</param>
        /// <param name="taxCls">已开税额</param> 
        /// <param name="qtyCls">已开数量</param> 
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        public void UpdateInvIkHeadData(string turnId, string lzclsId, string invNo, decimal amtCls, decimal amtn_netCls, decimal taxCls, decimal qtyCls, string blId, string blNo)
        {
            string _sql = "update MF_BLN set TURN_ID=@TURN_ID,LZ_CLS_ID=@LZ_CLS_ID,INV_NO=@INV_NO,AMT_CLS=@AMT_CLS,AMTN_NET_CLS=@AMTN_NET_CLS,TAX_CLS=@TAX_CLS,QTY_CLS=@QTY_CLS " +
                           " Where BL_ID=@BL_ID and BL_NO=@BL_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[9];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TURN_ID", SqlDbType.VarChar, 1);
            _spc[0].Value = turnId;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = lzclsId;


            _spc[2] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[2].Value = invNo;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@AMT_CLS", SqlDbType.Decimal, 0);
            _spc[3].Value = amtCls;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_CLS", SqlDbType.Decimal, 0);
            _spc[4].Value = amtn_netCls;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@TAX_CLS", SqlDbType.Decimal, 0);
            _spc[5].Value = taxCls;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@QTY_CLS", SqlDbType.Decimal, 0);
            _spc[6].Value = qtyCls;

            _spc[7] = new System.Data.SqlClient.SqlParameter("@BL_ID", SqlDbType.VarChar, 2);
            _spc[7].Value = blId;
            _spc[8] = new System.Data.SqlClient.SqlParameter("@BL_NO", SqlDbType.VarChar, 20);
            _spc[8].Value = blNo;
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
        public void UpdateInvIkBodyData(decimal amtFp, decimal amtn_netFp, decimal taxFp, decimal qtyFp, string blId, string blNo, int itm)
        {
            string _sql = "update TF_BLN set AMT_FP=@AMT_FP,AMTN_NET_FP=@AMTN_NET_FP,TAX_FP=@TAX_FP,QTY_FP=@QTY_FP Where BL_ID=@BL_ID and  BL_NO=@BL_NO and ITM=@ITM  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[7];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_FP", SqlDbType.Decimal);
            _spc[0].Value = amtFp;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP", SqlDbType.Decimal);
            _spc[1].Value = amtn_netFp;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@TAX_FP", SqlDbType.Decimal);
            _spc[2].Value = taxFp;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@QTY_FP", SqlDbType.Decimal);
            _spc[3].Value = qtyFp;


            _spc[4] = new System.Data.SqlClient.SqlParameter("@BL_NO", SqlDbType.VarChar, 20);
            _spc[4].Value = blNo;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@ITM", SqlDbType.Int);
            _spc[5].Value = itm;


            _spc[6] = new System.Data.SqlClient.SqlParameter("@BL_ID", SqlDbType.VarChar, 2);
            _spc[6].Value = blId;
            this.ExecuteNonQuery(_sql, _spc);

        }
        #endregion
    }
}