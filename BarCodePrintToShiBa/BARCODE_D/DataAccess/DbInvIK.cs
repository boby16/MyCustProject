using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data;
using System.Data.SqlClient;
namespace Sunlike.Business.Data
{
    public class DbInvIK:DbObject
    {
        #region Construction
        public DbInvIK(string connstr):base(connstr)
        { }
        #endregion

        private bool _upIsZero;
        public bool UpIsZero
        {
            get { return _upIsZero; }
            set { _upIsZero = value; }
         }
        /// <summary>
        /// 取得独立开立发票
        /// </summary>
        /// <param name="LzID">单据代号</param>
        /// <param name="LzNo">单据号码</param>
        /// <param name="OnlyFillSchema">是否只读取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string LzID, string LzNo, bool OnlyFillSchema)
        {
            string _rpId = "1";
            string _sql = "Select A.LZ_ID,A.LZ_NO,A.LZ_DD,A.PAY_DD,A.CUS_NO,A.INV_NO,A.CUR_ID,A.Exc_RTO," +
                        "A.REM,A.USR,A.CHK_MAN,A.PRT_SW,A.CPY_SW,A.AMT,A.AMTN_NET,A.TAX,A.DEP,A.VOH_ID," +
                        "A.VOH_NO,A.ARP_NO,A.CLS_DATE,A.SAL_NO,A.TAX_ID,A.PAY_MTH,A.PAY_DAYS,A.CHK_DAYS,A.INT_DAYS," +
                        "A.CHK_DD,A.PAY_REM,A.CLS_REM,A.ZHANG_ID,A.VERSION,A.BAT_NO,A.MOB_ID,A.LOCK_MAN," +
                        "A.CB_ID,A.FJ_NUM,A.SYS_DATE,A.BIL_ID,A.BIL_NO,A.TURN_ID,A.VOH_CHK,A.CAS_NO,A.TASK_ID," +
                        "A.RP_NO,A.PRT_USR,A.BIL_TYPE,A.ZC_FLAG,A.QC_SW ,B.NAME As CUS_NAME,D.NAME AS DEPNAME," +
                        "C.NAME As SAL_NAME ," +
                        " H.AMTN_BC,H.CACC_NO,H.BACC_NO,H.AMT_CHK AMT_CHK,H.AMTN_CHK AMTN_CHK,H.AMT_OTHER,H.AMTN_OTHER,I.CHK_NO,I.CHK_KND,I.BANK_NO,I.BACC_NO BACC_NO_CHK,I.CRECARD_NO,I.END_DD,H.AMT_BB,H.AMTN_BB,H.AMT_BC " +
                        " from MF_LZ A " +
                        " left join CUST B on B.CUS_NO=A.CUS_NO " +
                        " left join SALM C on C.SAL_NO=A.SAL_NO " +
                        " left join DEPT D on D.DEP=A.DEP " +
                        " left join MF_ARP F on F.ARP_ID = '1' AND F.OPN_ID = '2' AND F.ARP_NO=A.ARP_NO"+
                        " left join TF_MON H on A.RP_NO=H.RP_NO and H.RP_ID='" + _rpId + "' and H.ITM=1"+
                        " left join TF_MON4 N4 on A.RP_NO=N4.RP_NO and N4.RP_ID='" + _rpId + "' and N4.ITM=1"+
                        " left join MF_CHK I on N4.CHK_NO=I.CHK_NO and I.CHK_ID='0'"+
                        " Where A.LZ_ID=@LZ_ID And A.LZ_NO=@LZ_NO;"+
                        " Select A.LZ_ID,A.LZ_NO,A.ITM,A.CK_NO,A.AMT,A.AMTN_NET,A.TAX,A.PRD_NO, "+
                        " A.EST_ITM,A.PAY_DD,BIL_ID,A.TAX_RTO,A.PRD_MARK,A.QTY,A.UP,"+
                        " A.COMPOSE_IDNO,A.BAT_NO,A.CUS_OS_NO,A.SUP_PRD_NO,A.UNIT,A.REM, "+
                        " A.IC_ITM,TJ_ITM,IMTAG,A.PRD_NAME,A.CUS_NO,"+
                        "(case when A.UNIT='1' then B.UT when A.UNIT='2' then B.PK2_UT when A.UNIT='3' then B.PK3_UT else A.UNIT end) as UNIT_NAME,"+
                        " B.SPC "+
                        " From TF_LZ A "+
                        " left join PRDT B on B.PRD_NO=A.PRD_NO "+
                        " Where A.LZ_ID=@LZ_ID And A.LZ_NO=@LZ_NO";
            SqlParameter[] _spc = new  SqlParameter[2];
            _spc[0] = new SqlParameter("@LZ_ID",SqlDbType.Char,2);
			_spc[0].Value = LzID;
			_spc[1] = new SqlParameter("@LZ_NO",SqlDbType.VarChar,20);
			_spc[1].Value = LzNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (OnlyFillSchema)
                this.FillDatasetSchema(_sql,_ds,new string[]{"MF_LZ","TF_LZ"},_spc);
            else
                this.FillDataset(_sql,_ds,new string[]{"MF_LZ","TF_LZ"},_spc);
            _ds.Tables["TF_LZ"].Columns["UNIT_NAME"].ReadOnly = true;
            //设定PK，因为用了left join以后，PK会取不到
            DataColumn[] _dca = new DataColumn[2];
            _dca[0] = _ds.Tables["MF_LZ"].Columns["LZ_ID"];
            _dca[1] = _ds.Tables["MF_LZ"].Columns["LZ_NO"];
            _ds.Tables["MF_LZ"].PrimaryKey = _dca;

            _dca = new DataColumn[3];
            _dca[0] = _ds.Tables["TF_LZ"].Columns["LZ_ID"];
            _dca[1] = _ds.Tables["TF_LZ"].Columns["LZ_NO"];
            _dca[2] = _ds.Tables["TF_LZ"].Columns["ITM"];
            _ds.Tables["TF_LZ"].PrimaryKey = _dca;
            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_LZ"].Columns["LZ_ID"];
            _dca1[1] = _ds.Tables["MF_LZ"].Columns["LZ_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_LZ"].Columns["LZ_ID"];
            _dca2[1] = _ds.Tables["TF_LZ"].Columns["LZ_NO"];
            _ds.Relations.Add("MF_LZTF_LZ",_dca1,_dca2);
            return _ds;





        }

    

        public void DeleteToTF_LZ_EP(string _lzID, string lZ_NO)
        {
            string _sql = " Delete from TF_LZ_EP Where LZ_ID=@LZ_ID and LZ_NO=@LZ_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
            _spc[0] = new SqlParameter("@LZ_ID", SqlDbType.Char, 2);
            _spc[0].Value = _lzID;
            _spc[1] = new SqlParameter("@LZ_NO", SqlDbType.VarChar, 20);
            _spc[1].Value = lZ_NO;
            this.ExecuteNonQuery(_sql, _spc);
        }
        public void DeleteToTF_LZ_EP(string _lzID, string lZ_NO, int itm)
        {
            string _sql = " Delete from TF_LZ_EP Where LZ_ID=@LZ_ID and LZ_NO=@LZ_NO and LZ_ITM=@ITM";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new SqlParameter("@LZ_ID", SqlDbType.Char, 2);
            _spc[0].Value = _lzID;
            _spc[1] = new SqlParameter("@LZ_NO", SqlDbType.VarChar, 20);
            _spc[1].Value = lZ_NO;
            _spc[2] = new SqlParameter("@ITM", SqlDbType.Int);
            _spc[2].Value = itm;
            this.ExecuteNonQuery(_sql, _spc);
        }
        /// <summary>
        /// 更新MF_ARP的发票资讯
        /// </summary>
        /// <param name="BIL_ID">来源单号试别</param>
        /// <param name="BIL_NO">来源单号</param>
        /// <param name="ID">'D'表示delete</param>
        /// <param name="inv_no">发票号</param>
        public void UpdateMFARPINVNO(string BIL_ID,string BIL_NO, string ID,string inv_no)
        {
            string _sql = "";
            if (ID == "D")
                _sql = "UpDate MF_ARP Set INV_NO=NULL Where ARP_ID='1' And BIL_ID=@BIL_ID" +
                     " and BIL_NO=@BIL_NO and  ( ((IsNull(INV_NO,'')='')) or (INV_NO=@INV_NO)  )";
            else
                _sql = "Update MF_ARP Set INV_NO=@INV_NO Where ARP_ID='1' And BIL_ID=@BIL_ID and BIL_NO=@BIL_NO";

            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new SqlParameter("@BIL_ID", SqlDbType.Char, 2);
            _spc[0].Value = BIL_ID;
            _spc[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _spc[1].Value = BIL_NO;
            _spc[2] = new SqlParameter("@INV_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = inv_no;
            this.ExecuteNonQuery(_sql, _spc);

            
        }

        public void UpdateChkMan(string lzid, string lzno, string chkMan, DateTime clsDate)
        {
            string _sql = "UPDATE MF_LZ SET CHK_MAN = @CHK_MAN, CLS_DATE = @CLS_DATE WHERE LZ_ID = @LZ_ID AND LZ_NO = @LZ_NO";
            SqlParameter[] _aryPt = new SqlParameter[4];
            _aryPt[0] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _aryPt[0].Value = chkMan;
            _aryPt[1] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            if (clsDate == DateTime.MinValue)
            {
                _aryPt[1].Value = DBNull.Value;
            }
            else
            {
                _aryPt[1].Value = clsDate;
            }
            _aryPt[2] = new SqlParameter("@LZ_ID", SqlDbType.VarChar, 2);
            _aryPt[2].Value = lzid;
            _aryPt[3] = new SqlParameter("@LZ_NO", SqlDbType.VarChar, 20);
            _aryPt[3].Value = lzno;
            this.ExecuteNonQuery(_sql, _aryPt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slzNo">補開發票單號</param>
        /// <param name="ckNo"></param>
        public void UpdateInv_noForMFPss(string slzNo,string ckNo)
        { 
             string _sqlStr = " UpDate MF_PSS Set ACC_FP_NO=@LZ_NO Where ACC_FP_NO='' And "+
                              " Exists(Select B.CK_NO From TF_CK B Where B.CK_ID='CK' And B.CK_NO=@CK_NO And B.OS_ID='SA' And B.OS_NO=MF_PSS.PS_NO)";

             SqlParameter[] _aryPt = new SqlParameter[2];
             _aryPt[0] = new SqlParameter("@LZ_NO", SqlDbType.VarChar, 20);
             _aryPt[0].Value = slzNo;
             _aryPt[1] = new SqlParameter("@CK_NO", SqlDbType.VarChar,20);
             _aryPt[1].Value = ckNo;
             this.ExecuteNonQuery(_sqlStr, _aryPt);
        }

        /// <summary>
        /// 将MF_PSS 的ACC_FP_NO 清空(for 出库单)
        /// </summary>
        /// <param name="slzNo">来源单号</param>
        /// <param name="ckNo">出库单</param>
        public void UpdateMFPssforACC_FP_NO(string slzNo, string ckNo)
        {
            string _sqlStr = " UpDate MF_PSS Set ACC_FP_NO='',INV_NO='' Where ACC_FP_NO=@LZ_NO And "+
                              " Exists(Select B.CK_NO From TF_CK B Where B.CK_ID='CK' And B.CK_NO=@CK_NO And B.OS_ID='SA' And B.OS_NO=MF_PSS.PS_NO)";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@LZ_NO", SqlDbType.VarChar, 20);
            _aryPt[0].Value = slzNo;
            _aryPt[1] = new SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = ckNo;
            this.ExecuteNonQuery(_sqlStr, _aryPt);

        }

        public void UpdateMFARPINVNOForEXP(string BIL_ID, string BIL_NO,int itm, string ID, string inv_no)
        {
             string _sql = "";
            if (ID == "D")
                _sql = "UpDate MF_ARP Set INV_NO=NULL Where ARP_ID='1' And BIL_ID=@BIL_ID" +
                     " and BIL_NO=@BIL_NO and  ( ((IsNull(INV_NO,'')='')) or (INV_NO=@INV_NO)  ) and BIL_ITM=@ITM";
            else
                _sql = "Update MF_ARP Set INV_NO=@INV_NO Where ARP_ID='1' And BIL_ID=@BIL_ID and BIL_NO=@BIL_NO and BIL_ITM=@ITM";

            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new SqlParameter("@BIL_ID", SqlDbType.Char, 2);
            _spc[0].Value = BIL_ID;
            _spc[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _spc[1].Value = BIL_NO;
            _spc[2] = new SqlParameter("@INV_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = inv_no;
            _spc[3] = new SqlParameter("@ITM", SqlDbType.Int);
            _spc[3].Value = itm;
            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        /// 更新单据的立账单号
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="ArpNo">立账单号</param>
        public void UpdateArpNo(string LzID, string LzNo, string ArpNo,decimal Amtn_net,decimal Tax,decimal Amt)
        {
            string _sql = "update MF_LZ set ARP_NO=@ArpNo,AMTN_NET=@Amtn_net,TAX=@Tax,AMT=@Amt where LZ_ID=@LzId and LZ_NO=@LzNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[6];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ArpNo", SqlDbType.VarChar, 20);
            _spc[0].Value = ArpNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@Amtn_net", SqlDbType.Decimal, 0);
            _spc[1].Value = Amtn_net;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@Tax", SqlDbType.Decimal, 0);
            _spc[2].Value = Tax;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@Amt", SqlDbType.Decimal, 0);
            _spc[3].Value = Amt;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@LzId", SqlDbType.Char, 2);
            _spc[4].Value = LzID;
            _spc[5] = new System.Data.SqlClient.SqlParameter("@LzNo", SqlDbType.VarChar, 20);
            _spc[5].Value = LzNo;
            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        /// 回写MF_CK栏位
        /// </summary>
        /// <param name="sourceDr">来源单DataRow</param>
        /// <param name="bilno">来源单号</param>
        /// <param name="tblName"></param>
        public void UpDateMFCKHead(DataRow sourceDr, string bilno,string bilid)
        {
            string _sql = "update MF_CK set TURN_ID=@TURN_ID,LZ_CLS_ID=@LZ_CLS_ID,CLSLZ=@CLSLZ Where CK_NO=@CK_NO and CK_ID=@CK_ID";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[5];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TURN_ID", SqlDbType.VarChar, 1);
            _spc[0].Value = sourceDr["TURN_ID"].ToString();

            _spc[1] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = sourceDr["LZ_CLS_ID"].ToString();

            _spc[2] = new System.Data.SqlClient.SqlParameter("@CLSLZ", SqlDbType.VarChar, 1);
            _spc[2].Value = sourceDr["CLSLZ"].ToString();

            _spc[3] = new System.Data.SqlClient.SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _spc[3].Value = bilno;
            _spc[4] = new System.Data.SqlClient.SqlParameter("@CK_ID", SqlDbType.VarChar, 2);
            _spc[4].Value = bilid;
            this.ExecuteNonQuery(_sql, _spc);
        }



        /// <summary>回写MF_CK表头栏位滙总合并
        /// 回写MF_CK表头栏位滙总合并
        /// </summary>
        /// <param name="sourceDr">来源单DataRow</param>
        /// <param name="bilno">来源单号</param>
        /// <param name="tblName"></param>
        public void UpDateMFCKHead2(DataRow sourceDr, string bilno, string bilid)
        {
            string _sql = "update MF_CK set TURN_ID=@TURN_ID,LZ_CLS_ID=@LZ_CLS_ID,AMT_CLS=@AMT_CLS,AMTN_NET_CLS=@AMTN_NET_CLS,TAX_CLS=@TAX_CLS,QTY_CLS=@QTY_CLS Where CK_NO=@CK_NO and CK_ID=@CK_ID";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[8];
           
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TURN_ID", SqlDbType.VarChar, 1);
            _spc[0].Value = sourceDr["TURN_ID"].ToString();

            _spc[1] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = sourceDr["LZ_CLS_ID"].ToString();

            _spc[2] = new System.Data.SqlClient.SqlParameter("@AMT_CLS", SqlDbType.Decimal);
            if (!string.IsNullOrEmpty(sourceDr["AMT_CLS"].ToString()))
                _spc[2].Value = Convert.ToDecimal(sourceDr["AMT_CLS"].ToString());
            else
                _spc[2].Value = DBNull.Value;


            _spc[3] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_CLS", SqlDbType.Decimal);
            if (!string.IsNullOrEmpty(sourceDr["AMTN_NET_CLS"].ToString()))
                _spc[3].Value = Convert.ToDecimal(sourceDr["AMTN_NET_CLS"].ToString());
            else
                _spc[3].Value = DBNull.Value;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@TAX_CLS", SqlDbType.Decimal);
            if (!string.IsNullOrEmpty(sourceDr["TAX_CLS"].ToString()))
                _spc[4].Value = Convert.ToDecimal(sourceDr["TAX_CLS"].ToString());
            else
                _spc[4].Value = DBNull.Value;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@QTY_CLS", SqlDbType.Decimal);
            if (!string.IsNullOrEmpty(sourceDr["QTY_CLS"].ToString()))
                _spc[5].Value = Convert.ToDecimal(sourceDr["QTY_CLS"].ToString());
            else
                _spc[5].Value = DBNull.Value;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _spc[6].Value = bilno;

            _spc[7] = new System.Data.SqlClient.SqlParameter("@CK_ID", SqlDbType.VarChar, 2);
            _spc[7].Value = bilid;
            this.ExecuteNonQuery(_sql, _spc);
        }

        /// <summary>
        /// 回写TF_CK表身栏位
        /// </summary>
        /// <param name="sourceDr"></param>
        /// <param name="bilno"></param>
        public void UpDateTFCKBody(DataRow sourceDr, string bilno,string bilid)
        {
            string _sql = "update TF_CK set AMT_FP=@AMT_FP,AMTN_NET_FP=@AMTN_NET_FP,TAX_FP=@TAX_FP,QTY_FP=@QTY_FP Where CK_NO=@CK_NO and  PRE_ITM=@EST_ITM and CK_ID=@CK_ID";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[7];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_FP", SqlDbType.Decimal);
            if (!string.IsNullOrEmpty(sourceDr["AMT_FP"].ToString()))
                _spc[0].Value = Convert.ToDecimal(sourceDr["AMT_FP"]);
            else
                _spc[0].Value = DBNull.Value;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP", SqlDbType.Decimal);
            if (!string.IsNullOrEmpty(sourceDr["AMTN_NET_FP"].ToString()))
                _spc[1].Value = Convert.ToDecimal(sourceDr["AMTN_NET_FP"]);
            else
                _spc[1].Value = DBNull.Value;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@TAX_FP", SqlDbType.Decimal);
            if (!string.IsNullOrEmpty(sourceDr["TAX_FP"].ToString()))
                _spc[2].Value = Convert.ToDecimal(sourceDr["TAX_FP"]);
            else
                _spc[2].Value = DBNull.Value;


            _spc[3] = new System.Data.SqlClient.SqlParameter("@QTY_FP", SqlDbType.Decimal);
            if (!string.IsNullOrEmpty(sourceDr["QTY_FP"].ToString()))
                _spc[3].Value = Convert.ToDecimal(sourceDr["QTY_FP"]);
            else
                _spc[3].Value = DBNull.Value;


            _spc[4] = new System.Data.SqlClient.SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _spc[4].Value = bilno;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@EST_ITM", SqlDbType.VarChar, 1);
            _spc[5].Value = sourceDr["PRE_ITM"].ToString();


            _spc[6] = new System.Data.SqlClient.SqlParameter("@CK_ID", SqlDbType.VarChar, 2);
            _spc[6].Value = bilid;
            this.ExecuteNonQuery(_sql, _spc);
        }



        /// <summary>
        /// 判断来源单是否存在TF_LZ
        /// </summary>
        /// <param name="lzId">TF_LZ的单据别</param>
        /// <param name="bilId">来源单识ID</param>
        /// <param name="ckno">来源单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetInTfLz(string lzId, string bilId, string ckno)
        {
            string _sql = "Select Top 1 LZ_NO From TF_LZ Where (LZ_ID=@LZ_ID) And (BIL_ID=@BIL_ID) And (CK_NO=@CK_NO)";
            SqlParameter[] _spc = new SqlParameter[3];
            _spc[0] = new SqlParameter("@LZ_ID", SqlDbType.Char, 2);
            _spc[0].Value = lzId;

            _spc[1] = new SqlParameter("@BIL_ID", SqlDbType.Char, 2);
            _spc[1].Value = bilId;

            _spc[2] = new SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = ckno;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_LZ" }, _spc);
            return _ds;
        }


        /// <summary>
        /// 判断来源单是否存在TF_LZ
        /// </summary>
        
        /// <param name="bilId">来源单识ID</param>
        /// <param name="ckno">来源单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetInTfLz(string bilId, string ckno)
        {
            string _sql = "Select Top 1 LZ_NO From TF_LZ Where (LZ_ID in ('LZ','LO') ) And (BIL_ID=@BIL_ID) And (CK_NO=@CK_NO)";
            SqlParameter[] _spc = new SqlParameter[2];

            _spc[0] = new SqlParameter("@BIL_ID", SqlDbType.Char, 2);
            _spc[0].Value = bilId;

            _spc[1] = new SqlParameter("@CK_NO", SqlDbType.VarChar, 20);
            _spc[1].Value = ckno;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_LZ" }, _spc);
            return _ds;
        }

        #region 更新进货立帐开票单凭证号码
        /// <summary>
        /// 更新进货立帐开票单凭证号码
        /// </summary>
        /// <param name="lzId"></param>
        /// <param name="lzNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string lzId, string lzNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_LZ SET VOH_NO=@VOH_NO WHERE LZ_ID=@LZ_ID AND LZ_NO=@LZ_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@LZ_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = lzId;

            _sqlPara[1] = new SqlParameter("@LZ_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = lzNo;

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

        public decimal GetAmtn_EPForQuery(string lzId,string lzNo, string epId, int lzItm)
        {
            string _sql = " SELECT SUM(Isnull(AMTN_NET,0) ) AMTN_NET From TF_LZ_EP "+
                          " Where LZ_ID=@LZ_ID And LZ_NO=@LZ_NO And LZ_ITM=@lZ_ITM And EP_ID=@EP_ID ";
            SqlParameter[] _spc = new SqlParameter[4];

            _spc[0] = new SqlParameter("@LZ_ID", SqlDbType.Char, 2);
            _spc[0].Value = lzId;

            _spc[1] = new SqlParameter("@LZ_NO", SqlDbType.VarChar, 20);
            _spc[1].Value = lzNo;

            _spc[2] = new SqlParameter("@LZ_ITM", SqlDbType.Int);
            _spc[2].Value = lzItm;

            _spc[3] = new SqlParameter("@EP_ID", SqlDbType.Char, 2);
            _spc[3].Value = epId;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_LZ_EP" }, _spc);
            if (_ds.Tables["TF_LZ_EP"].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(_ds.Tables["TF_LZ_EP"].Rows[0]["AMTN_NET"].ToString()))
                    return Convert.ToDecimal(_ds.Tables["TF_LZ_EP"].Rows[0]["AMTN_NET"].ToString());
                else
                    return 0;
            }
            else
                return 0;
        }

        public SunlikeDataSet GetAllInvNoForBil(string bilId, string bilNo)
        {
            string _sql = "";
            if (bilId == "SA")
            {
                _sql = " SELECT Distinct B.BIL_NO,0 As EST_ITM,IsNull(H.Amtn_Net_FP,0)+IsNull(H.Tax_FP,0) As FP_AMTN,B.BIL_ID,H.REM1 AS INV_NO From TF_BIL B,MF_BIL H " +
                         " Where ((H.BIL_ID='KP') Or (H.BIL_ID='KW')) And (B.REM3='SA') And (B.REM1=@BILNO)" +
                         " And (H.BIL_ID=B.BIL_ID) And (H.BIL_NO=B.BIL_NO) " +
                         " UNION All " +
                         " SELECT Distinct B.LZ_NO As BIL_NO,B.EST_ITM,(IsNull(B.AMTN_NET,0)+IsNull(B.Tax,0)) As FP_AMTN,B.LZ_ID As BIL_ID,A.Inv_No " +
                         " From TF_LZ B, MF_LZ A  " +
                         " Where (B.BIL_ID='SA') And (B.CK_NO=@BILNO)" +
                         " And (B.LZ_ID=B.LZ_ID) And (B.LZ_NO=A.LZ_NO) And (IsNull(A.Inv_No,'')<>'') And (IsNull(A.CHK_MAN,'')<>'')" +
                         " UNION All " +
                         " SELECT Distinct A.LZ_NO As BIL_NO,A.EST_ITM,(IsNull(A.AMTN_NET,0)+IsNull(A.Tax,0)) As FP_AMTN,A.LZ_ID As BIL_ID,H.Inv_No " +
                         " From TF_LZ A Left Outer Join TF_CK B On A.BIL_ID=B.CK_ID And A.CK_NO=B.CK_NO " +
                         " Left Outer Join MF_CK G On G.CK_ID=A.BIL_ID And G.CK_NO=A.CK_NO " +
                         " Left Outer Join MF_LZ H On H.LZ_ID=A.LZ_ID And H.LZ_NO=A.LZ_NO" +
                         " Where ((A.BIL_ID='CK') Or (IsNull(A.BIL_ID,'')='')) And (IsNull(H.Inv_No,'')<>'') And (IsNull(H.CHK_MAN,'')<>'') " +
                         " And (((B.OS_ID='SA')And(B.OS_NO=@BILNO))or(G.PS_NO=@BILNO))";
            }
            else
                if (bilId == "SB" || bilId == "SD" || bilId == "LN")
                {
                    _sql = " SELECT Distinct B.BIL_NO,B.REM2 As EST_ITM,IsNull(H.Amtn_Net_FP,0)+IsNull(H.Tax_FP,0) As FP_AMTN,B.BIL_ID,H.REM1 AS INV_NO From TF_BIL B,MF_BIL H " +
                           " Where ((H.BIL_ID='KP') Or (H.BIL_ID='KW')) And (B.REM3='"+bilId+"') And (B.REM1=@BILNO)" +
                           " And (H.BIL_ID=B.BIL_ID) And (H.BIL_NO=B.BIL_NO) " +
                           " UNION All " +
                           " SELECT Distinct B.LZ_NO As BIL_NO,B.EST_ITM,(IsNull(B.AMTN_NET,0)+IsNull(B.Tax,0)) As FP_AMTN,B.LZ_ID As BIL_ID,A.Inv_No " +
                           " From TF_LZ B, MF_LZ A " +
                           " Where (B.BIL_ID='"+bilId+"') And (B.CK_NO=@BILNO) " +
                           " And (B.LZ_ID=B.LZ_ID) And (B.LZ_NO=A.LZ_NO) And (IsNull(A.Inv_No,'')<>'') And (IsNull(A.CHK_MAN,'')<>'')";
                }
            if (bilId == "") return null;
            SqlParameter[] _spc = new SqlParameter[1];

            _spc[0] = new SqlParameter("@BILNO", SqlDbType.VarChar, 20);
            _spc[0].Value = bilNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "INVNOLIST" }, _spc);
            _ds.Tables["INVNOLIST"].Columns.Add("ITM", typeof(System.Int32));
            
            int _itm = 0;
            foreach (DataRow dr in _ds.Tables["INVNOLIST"].Rows)
            {
                _itm++;
                dr["ITM"] = _itm;
            }
            _ds.AcceptChanges();
            return _ds;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="BIL_ID">来源单据别</param>
        /// <param name="CHK_DATE">判断日期否</param>
        /// <param name="FDATE">起日期</param>
        /// <param name="EDATE">止日期</param>
        /// <param name="FPS_NO">起单号</param>
        /// <param name="EPS_NO">止单号</param>
        /// <param name="CUS_NO">客户代号</param>
        /// <param name="INCUS">客户含下属否</param>
        /// <param name="TAX_ID">税别</param>
        /// <param name="TURN_ID">会总开</param>
        /// <param name="Zhang_ID">立帐别</param>
        /// <param name="DEP">部门代号</param>
        /// <param name="SAL_NO">业务</param>
        /// <param name="CUR_ID">币别</param>
        /// <param name="CAS_NO"></param>
        /// <param name="TASK_ID"></param>
        /// <param name="CHKPRD">判断货品否</param>
        /// <param name="FPRD_NO">起货号</param>
        /// <param name="EPRD_NO">止货号</param>
        /// <param name="CHKBilType">单据别</param>
        /// <param name="FBILTYPE">起单据别</param>
        /// <param name="EBILTYPE">止单据别</param>
        /// <param name="BILPGM">来源单据别权限代号</param>
        /// <param name="USR"></param>
        /// <param name="COMPNO"></param>
        /// <param name="LOGINDEP">登入部门</param>
        /// <param name="LOGINUPDEP">上层部门代号</param>
        /// <param name="rightWhere">权限查询条件</param>
        /// <param name="CHKAUD">显示已稽核的调价单</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForBil(string BIL_ID, string CHK_DATE, string FDATE, string EDATE,
              string FPS_NO, string EPS_NO, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID,
              string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID,
              string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE,
              string EBILTYPE, string BILPGM, string USR, string COMPNO, string LOGINDEP, string LOGINUPDEP,
              string rightWhere, string CHKAUD)
        {
            string _bilID = BIL_ID;
            string SQLStr = "";
            switch (_bilID)
            {
                case "SA":
                    SQLStr = GetSqlStrForPS(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID,DEP,SAL_NO,CUR_ID,CAS_NO,TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "SB":
                    SQLStr = GetSqlStrForPS(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "SD":
                    SQLStr = GetSqlStrForPS(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "LN":
                    SQLStr = GetSqlStrForMF_BLN(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "LB":
                    SQLStr = GetSqlStrForMF_BLN(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "ER":
                    SQLStr = GetSqlStrForMF_EXP(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "IO":
                    SQLStr = GetSqlStrForMF_IC_IO(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "IB":
                    SQLStr = GetSqlStrForMF_IC_IB(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "TJ":
                    SQLStr = GetSqlStrForMF_TJ(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE,CHKAUD);
                    break;
                case "IM":
                    SQLStr = GetSqlStrForMF_IC_IO(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "CK":
                    SQLStr = GetSqlStrForMFCK(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "KB":
                    SQLStr = GetSqlStrForMFCK(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "PC":
                    SQLStr = GetSqlStrForPS(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "PB":
                    SQLStr = GetSqlStrForPS(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
                case "PD":
                    SQLStr = GetSqlStrForPS(BIL_ID, CHK_DATE, FDATE, EDATE,
                                            FPS_NO, EPS_NO, CUS_NO, INCUS, TAX_ID, TURN_ID,
                                            Zhang_ID, DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID,
                                            CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
                    break;
            }
            if (!string.IsNullOrEmpty(BILPGM))
                SQLStr += rightWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(SQLStr, _ds, new string[] { "BILDATA" });

            return _ds;

        }

        #region  各单据的SQL 语法
        /// <summary>
        /// MF_TJ 专用
        /// </summary>
        /// <param name="BIL_ID">来源单据别</param>
        /// <param name="CHK_DATE">判断日期否</param>
        /// <param name="FDATE">起日期</param>
        /// <param name="EDATE">止日期</param>
        /// <param name="FPS_NO">起单号</param>
        /// <param name="EPS_NO">止单号</param>
        /// <param name="CUS_NO">客户代号</param>
        /// <param name="INCUS">客户含下属否</param>
        /// <param name="TAX_ID">税别</param>
        /// <param name="TURN_ID">会总开</param>
        /// <param name="Zhang_ID">立帐别</param>
        /// <param name="DEP">部门代号</param>
        /// <param name="SAL_NO">业务</param>
        /// <param name="CUR_ID">币别</param>
        /// <param name="CAS_NO"></param>
        /// <param name="TASK_ID"></param>
        /// <param name="CHKPRD">判断货品否</param>
        /// <param name="FPRD_NO">起货号</param>
        /// <param name="EPRD_NO">止货号</param>
        /// <param name="CHKBilType">单据别</param>
        /// <param name="FBILTYPE">起单据别</param>
        /// <param name="EBILTYPE">止单据别</param>
        /// <param name="CHKAUD">显示已稽核的调价单</param>
        /// <returns></returns>
        private string GetSqlStrForMF_TJ(string BIL_ID, string CHK_DATE, string FDATE, string EDATE, string FPS_NO, string EPS_NO, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE,string CHKAUD)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" SELECT B.PRD_NO,G.NAME As PRD_NAME,B.PRD_MARK,B.AMTN_NET,0 TAX,B.AMTN_NET AS AMT,B.AMT_FP,B.AMTN_NET_FP,");
            _sb.Append(" B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.KEY_ITM As PRE_ITM,G.SPC_TAX AS TAX_RTO,1 As EXC_RTO,A.TJ_DD As PAY_DD,A.TJ_NO As BIL_NO,'TJ' As BIL_ID,0 As BIL_ITM,");
            _sb.Append(" 0 AS DIS_CNT,'' AS OS_NO,'' AS ZHANG_ID,0 AS AMTN_EP,0 As AMTN_ER,'' AS OS_ID,B.UPR4 As UP,'' AS BAT_NO,G.CHK_TAX,'' As CUS_OS_NO,'' As SUP_PRD_NO,'1' As UNIT,");
            _sb.Append(" A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,'' As CAS_NO, 0 As TASK_ID,'' As IMTAG,B.REM AS REM1,A.CUS_NO,'' as VOH_ID,A.REM ");
            _sb.Append(" FROM MF_TJ A,TF_TJ B LEFT OUTER JOIN PRDT G ON B.PRD_NO=G.PRD_NO ");

            GetSQLStrForTJ(_sb, BIL_ID, TURN_ID, DEP, SAL_NO, CUS_NO, INCUS, CAS_NO, TASK_ID, CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);
            if (CHK_DATE == "Y")
                _sb.Append(" and (A.TJ_DD BETWEEN '" + Convert.ToDateTime(FDATE) + "' and '" + Convert.ToDateTime(EDATE) + "') ");
            if (!string.IsNullOrEmpty(FPS_NO))
                _sb.Append(" and (A.TJ_NO>='" + FPS_NO + "')");
            if (!string.IsNullOrEmpty(EPS_NO))
                _sb.Append(" and (A.TJ_NO<='" + EPS_NO + "') ");
            if (CHKAUD == "T")
            {
                _sb.Append(" And (IsNull(AUD_FLAG,'')='T') ");
            }
            return _sb.ToString();

        }

        private void GetSQLStrForTJ(StringBuilder _sb, string BIL_ID, string TURN_ID, string DEP, string SAL_NO, string CUS_NO, string INCUS, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            _sb.Append(" Where  (A.TJ_NO=B.TJ_NO) And (IsNull(A.LZ_CLS_ID,'')<>'T')");
            if (!UpIsZero)
                _sb.Append(" And (IsNull(B.AMTN_NET,0)<>0) ");
            _sb.Append(" and ((A.TURN_ID='" + TURN_ID + "') Or (IsNull(A.TURN_ID,'')='')) ");
            if (!string.IsNullOrEmpty(DEP))
                _sb.Append(" AND (IsNull(A.DEP,'')='" + DEP + "')");
            if (!string.IsNullOrEmpty(SAL_NO))
                _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + SAL_NO + "')");

            string _cus_no = CUS_NO;
            if (!string.IsNullOrEmpty(_cus_no))
            {
                if (INCUS == "Y")  //含下属
                    _sb.Append(" and A.Cus_NO in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
                else
                    _sb.Append(" And  (IsNull(A.CUS_NO,'')='" + _cus_no + "')");
            }
            _sb.Append(" And (IsNull(A.CHK_MAN,'')<>'') ");
            //if ChkAud_Flag then SQLStr := SQLStr + ' And (IsNull(A.AUD_FLAG,'''')=''T'') ';//wfp 2006.08.28



            if (CHKPRD == "Y")
            {
                if (!string.IsNullOrEmpty(FPRD_NO))
                    _sb.Append(" and (B.PRD_NO>='" + FPRD_NO + "')");
                if (!string.IsNullOrEmpty(EPRD_NO))
                    _sb.Append(" and (B.PRD_NO<='" + EPRD_NO + "')");

            }
            if (CHKBilType == "Y")
            {
                if (!string.IsNullOrEmpty(FBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE>='" + FBILTYPE + "')");
                if (!string.IsNullOrEmpty(EBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE<='" + EBILTYPE + "')");

            }

        }

        /// <summary>
        /// MF_CK 专用
        /// </summary>
        /// <param name="BIL_ID">来源单据别</param>
        /// <param name="CHK_DATE">判断日期否</param>
        /// <param name="FDATE">起日期</param>
        /// <param name="EDATE">止日期</param>
        /// <param name="FPS_NO">起单号</param>
        /// <param name="EPS_NO">止单号</param>
        /// <param name="CUS_NO">客户代号</param>
        /// <param name="INCUS">客户含下属否</param>
        /// <param name="TAX_ID">税别</param>
        /// <param name="TURN_ID">会总开</param>
        /// <param name="Zhang_ID">立帐别</param>
        /// <param name="DEP">部门代号</param>
        /// <param name="SAL_NO">业务</param>
        /// <param name="CUR_ID">币别</param>
        /// <param name="CAS_NO"></param>
        /// <param name="TASK_ID"></param>
        /// <param name="CHKPRD">判断货品否</param>
        /// <param name="FPRD_NO">起货号</param>
        /// <param name="EPRD_NO">止货号</param>
        /// <param name="CHKBilType">单据别</param>
        /// <param name="FBILTYPE">起单据别</param>
        /// <param name="EBILTYPE">止单据别</param>
        /// <param name="CHKAUD">显示已稽核的调价单</param>
        /// <returns></returns>
        private string GetSqlStrForMFCK(string BIL_ID, string CHK_DATE, string FDATE, string EDATE, string FPS_NO, string EPS_NO, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" SELECT B.PRD_NO,B.PRD_NAME,B.PRD_MARK,B.AMTN_NET,B.TAX,B.AMT,B.AMT_FP,B.AMTN_NET_FP,");
            _sb.Append(" B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.PRE_ITM,B.TAX_RTO,A.EXC_RTO,A.CK_DD AS PAY_DD,A.CK_NO As BIL_NO,A.CK_ID As BIL_ID,0 As BIL_ITM,");
            _sb.Append(" A.Dis_Cnt,A.OS_NO,B.ZHANG_ID,0 As AMTN_EP,0 As AMTN_ER,B.OS_NO As OS_NO_B,B.OTH_ITM,B.OS_ID,B.UP,B.BAT_NO,B.CHK_TAX, ");
            _sb.Append(" Cast (A.REM As VarChar(200)) As REM, ");
            _sb.Append(" B.CUS_OS_NO,B.SUP_PRD_NO,B.UNIT,'' AS PSSCK_NOA,A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,0 As AMTN_EP1, ");
            _sb.Append("  0 As AMTN_ER1,A.CAS_NO,A.TASK_ID,B.REM AS REM1,A.CUS_NO,A.PAY_MTH,A.CUR_ID,A.DEP,A.SAL_NO,A.TAX_ID,'' as VOH_ID,'' IMTAG,A.REM   ");
            _sb.Append(" From MF_CK A,TF_CK B ");
            GetSQLStrForCK(_sb, BIL_ID, TURN_ID, DEP, SAL_NO, CUS_NO, INCUS, CAS_NO, TASK_ID, CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE,Zhang_ID,CUR_ID,TAX_ID);
            if (CHK_DATE == "Y")
                _sb.Append(" and (A.CK_DD BETWEEN '" + Convert.ToDateTime(FDATE) + "' and '" + Convert.ToDateTime(EDATE) + "') ");
            if (!string.IsNullOrEmpty(FPS_NO))
                _sb.Append(" and (A.CK_NO>='" + FPS_NO + "')");
            if (!string.IsNullOrEmpty(EPS_NO))
                _sb.Append(" and (A.CK_NO<='" + EPS_NO + "') ");
            return _sb.ToString();

        }

        private void GetSQLStrForCK(StringBuilder _sb, string BIL_ID, string TURN_ID, string DEP, string SAL_NO, string CUS_NO, string INCUS, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE, string Zhang_ID, string CUR_ID, string TAX_ID)
        {
            _sb.Append(" Where ");
            _sb.Append("(A.CK_ID='" + BIL_ID + "')");
            _sb.Append(" and (A.CK_ID=B.CK_ID) And (A.CK_NO=B.CK_NO) And (IsNull(A.LZ_CLS_ID,'')<>'T')");
            _sb.Append(" and ((A.TURN_ID='" + TURN_ID + "') Or (IsNull(A.TURN_ID,'')='')) ");
            if (!UpIsZero)
                _sb.Append(" And (IsNull(B.AMTN_NET,0)+IsNull(B.TAX,0)<>0) ");
            _sb.Append(" And (IsNull(B.Zhang_ID,'')='" + Zhang_ID + "' )  ");
            _sb.Append(" And (IsNull(A.YD_ID,'')='T') ");
            if (!string.IsNullOrEmpty(DEP))
                _sb.Append(" And  (IsNull(A.DEP,'')='" + DEP + "')");
            if (!string.IsNullOrEmpty(SAL_NO))
                _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + SAL_NO + "')");
            if (!string.IsNullOrEmpty(CUR_ID))
                _sb.Append(" And  (IsNull(A.CUR_ID,'')='" + CUR_ID + "')");
            else
                _sb.Append(" And  (IsNull(A.CUR_ID,'')='')");
            _sb.Append(" And  (IsNull(A.TAX_ID,'')='" + TAX_ID + "') ");  //税别
            _sb.Append(" And (A.CHK_MAN Is Not Null) And (A.CHK_MAN <>'')");
            string _cus_no = CUS_NO;
            if (!string.IsNullOrEmpty(_cus_no))
            {
                if (INCUS == "Y")  //含下属
                    _sb.Append(" and A.Cus_NO in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
                else
                    _sb.Append(" And  (IsNull(A.CUS_NO,'')='" + _cus_no + "')");
            }

        }


        /// <summary>
        /// MF_IC 专用
        /// </summary>
        /// <param name="BIL_ID">来源单据别</param>
        /// <param name="CHK_DATE">判断日期否</param>
        /// <param name="FDATE">起日期</param>
        /// <param name="EDATE">止日期</param>
        /// <param name="FPS_NO">起单号</param>
        /// <param name="EPS_NO">止单号</param>
        /// <param name="CUS_NO">客户代号</param>
        /// <param name="INCUS">客户含下属否</param>
        /// <param name="TAX_ID">税别</param>
        /// <param name="TURN_ID">会总开</param>
        /// <param name="Zhang_ID">立帐别</param>
        /// <param name="DEP">部门代号</param>
        /// <param name="SAL_NO">业务</param>
        /// <param name="CUR_ID">币别</param>
        /// <param name="CAS_NO"></param>
        /// <param name="TASK_ID"></param>
        /// <param name="CHKPRD">判断货品否</param>
        /// <param name="FPRD_NO">起货号</param>
        /// <param name="EPRD_NO">止货号</param>
        /// <param name="CHKBilType">单据别</param>
        /// <param name="FBILTYPE">起单据别</param>
        /// <param name="EBILTYPE">止单据别</param>
        /// <returns></returns>
        private string GetSqlStrForMF_IC_IB(string BIL_ID, string CHK_DATE, string FDATE, string EDATE, string FPS_NO, string EPS_NO, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT B.PRD_NO,G.NAME As PRD_NAME,B.PRD_MARK,IsNull(B.CST,0)-IsNull(B.AMTN_EP,0) As AMTN_NET,0 TAX,IsNull(B.CST,0)-IsNull(B.AMTN_EP,0) AS AMT,B.AMT_FP,B.AMTN_NET_FP, ");
            _sb.Append(" B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.KEY_ITM As PRE_ITM,G.SPC_TAX AS TAX_RTO,1 As EXC_RTO,A.IC_DD AS PAY_DD,A.IC_NO As BIL_NO,A.IC_ID As BIL_ID,0 As BIL_ITM, ");
            _sb.Append(" 0 AS DIS_CNT,'' As OS_NO,'' AS ZHANG_ID,0 AS AMTN_EP,0 As AMTN_ER,'' As OS_ID,B.UP,B.BAT_NO,G.CHK_TAX,'' As CUS_OS_NO,'' As SUP_PRD_NO,B.UNIT,");
            _sb.Append(" A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,A.CAS_NO,A.TASK_ID, ");
            _sb.Append(" IMTAG=(Case When A.IC_ID='IM' then 'O' else '' end),B.REM AS REM1,A.CUS_NO1 As CUS_NO,A.VOH_ID,A.REM ");
            _sb.Append(" FROM MF_IC A,TF_IC B LEFT OUTER JOIN PRDT G ON B.PRD_NO=G.PRD_NO ");


            GetSQLStrForIB(_sb, BIL_ID, TURN_ID, DEP, SAL_NO, CUS_NO, INCUS, CAS_NO, TASK_ID, CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);

            if (CHK_DATE == "Y")
                _sb.Append(" and (A.IC_DD BETWEEN '" + Convert.ToDateTime(FDATE) + "' and '" + Convert.ToDateTime(EDATE) + "') ");
            if (!string.IsNullOrEmpty(FPS_NO))
                _sb.Append(" and (A.IC_NO>='" + FPS_NO + "')");
            if (!string.IsNullOrEmpty(EPS_NO))
                _sb.Append(" and (A.IC_NO<='" +EPS_NO + "') ");
            return _sb.ToString();

        }

        private void GetSQLStrForIB(StringBuilder _sb, string BIL_ID, string TURN_ID, string DEP, string SAL_NO, string CUS_NO, string INCUS, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            string _id = BIL_ID;
            _sb.Append(" Where (A.IC_ID=B.IC_ID) AND (A.IC_NO=B.IC_NO) And (IsNull(LZ_CLS_ID,'')<>'T') ");
            //if Not Chk_Up_Zero then
            if (!UpIsZero)
                _sb.Append(" And (IsNull(B.CST,0)<>0)");
            _sb.Append(" And ((A.TURN_ID='" + TURN_ID + "') Or (IsNull(A.TURN_ID,'')='')) ");

            if (!string.IsNullOrEmpty(DEP))
            {
                if (_id == "IB")
                    _sb.Append(" AND (IsNull(A.OUTDEP,'')='" + DEP + "')");
                else
                    _sb.Append(" AND (IsNull(A.DEP,'')='" + DEP + "')");
            }
            if (!string.IsNullOrEmpty(SAL_NO))
                _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + SAL_NO + "')");
            string _cus_no = CUS_NO;
            if (!string.IsNullOrEmpty(_cus_no))
            {
                if (INCUS == "Y")  //含下属
                    _sb.Append(" and A.Cus_NO1 in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
                else
                    _sb.Append(" And  (IsNull(A.CUS_NO1,'')='" + _cus_no + "')");
            }
            _sb.Append(" And (A.CHK_MAN Is Not Null) And (A.CHK_MAN <>'')");
            if (!string.IsNullOrEmpty(CAS_NO))
            {
                string _str = CAS_NO;
                _str = _str.Replace(";", "','");
                _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
            }

            if (!string.IsNullOrEmpty(TASK_ID))
            {
                string _str = TASK_ID;
                _str = _str.Replace(";", ",");
                _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
            }
            if (CHKPRD == "Y")
            {
                if (!string.IsNullOrEmpty(FPRD_NO))
                    _sb.Append(" and (B.PRD_NO>='" + FPRD_NO + "')");
                if (!string.IsNullOrEmpty(EPRD_NO))
                    _sb.Append(" and (B.PRD_NO<='" + EPRD_NO + "')");

            }
            if (CHKBilType == "Y")
            {
                if (!string.IsNullOrEmpty(FBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE>='" + FBILTYPE + "')");
                if (!string.IsNullOrEmpty(EBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE<='" + EBILTYPE + "')");

            }

        }
        /// <summary>
        /// MF_IC 专用
        /// </summary>
        /// <param name="BIL_ID">来源单据别</param>
        /// <param name="CHK_DATE">判断日期否</param>
        /// <param name="FDATE">起日期</param>
        /// <param name="EDATE">止日期</param>
        /// <param name="FPS_NO">起单号</param>
        /// <param name="EPS_NO">止单号</param>
        /// <param name="CUS_NO">客户代号</param>
        /// <param name="INCUS">客户含下属否</param>
        /// <param name="TAX_ID">税别</param>
        /// <param name="TURN_ID">会总开</param>
        /// <param name="Zhang_ID">立帐别</param>
        /// <param name="DEP">部门代号</param>
        /// <param name="SAL_NO">业务</param>
        /// <param name="CUR_ID">币别</param>
        /// <param name="CAS_NO"></param>
        /// <param name="TASK_ID"></param>
        /// <param name="CHKPRD">判断货品否</param>
        /// <param name="FPRD_NO">起货号</param>
        /// <param name="EPRD_NO">止货号</param>
        /// <param name="CHKBilType">单据别</param>
        /// <param name="FBILTYPE">起单据别</param>
        /// <param name="EBILTYPE">止单据别</param>
        /// <returns></returns>
        private string GetSqlStrForMF_IC_IO(string BIL_ID, string CHK_DATE, string FDATE, string EDATE, string FPS_NO, string EPS_NO, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append(" SELECT B.PRD_NO,G.NAME As PRD_NAME,B.PRD_MARK,B.AMTN_NET,0.0 TAX,B.AMTN_NET AS AMT,B.AMT_FP2 As AMT_FP,B.AMTN_NET_FP2 As AMTN_NET_FP, ");
            _sb.Append(" B.TAX_FP2 As TAX_FP,B.QTY_FP2 As QTY_FP,B.ITM,B.QTY,B.KEY_ITM As PRE_ITM,G.SPC_TAX AS TAX_RTO,1 As EXC_RTO,A.IC_DD AS PAY_DD,A.IC_NO As BIL_NO,A.IC_ID As BIL_ID,0 As BIL_ITM, ");
            _sb.Append(" 0 As DIS_CNT,'' As OS_NO,'' AS ZHANG_ID,0 AS AMTN_EP,0 As AMTN_ER,'' As OS_ID,B.UP,B.BAT_NO2 As BAT_NO,G.CHK_TAX,'' As CUS_OS_NO,'' As SUP_PRD_NO,B.UNIT, ");
            _sb.Append(" A.AMT_CLS2 As AMT_CLS,A.AMTN_NET_CLS2 As AMTN_NET_CLS,A.TAX_CLS2 As TAX_CLS,A.QTY_CLS2 As QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,A.CAS_NO,A.TASK_ID, ");
            _sb.Append(" IMTAG=(Case When A.IC_ID='IM' then 'I' else '' end),B.REM AS REM1,A.CUS_NO2 As CUS_NO,A.VOH_ID,A.REM ");
            _sb.Append(" FROM MF_IC A,TF_IC B LEFT OUTER JOIN PRDT G ON B.PRD_NO=G.PRD_NO ");

            _sb.Append(" Where  (A.IC_ID=B.IC_ID) And (A.IC_NO=B.IC_NO) And (IsNull(LZ_CLS_ID2,'')<>'T') ");
            _sb.Append(" and (A.IC_ID='" + BIL_ID + "') ");
            //if Not Chk_Up_Zero then
            if (!UpIsZero)
                _sb.Append(" And (IsNull(B.AMTN_NET,0)<>0)");


            if (CHK_DATE == "Y")
                _sb.Append(" and (A.IC_DD BETWEEN '" + Convert.ToDateTime(FDATE) + "' and '" + Convert.ToDateTime(EDATE) + "') ");
            if (!string.IsNullOrEmpty(FPS_NO))
                _sb.Append(" and (A.IC_NO>='" + FPS_NO + "')");
            if (!string.IsNullOrEmpty(EPS_NO))
                _sb.Append(" and (A.IC_NO<='" + EPS_NO + "') ");
            GetConditionString2(_sb, BIL_ID, CUS_NO, INCUS, TAX_ID, TURN_ID, Zhang_ID
              , DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID, CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);

            return _sb.ToString();

        }

        private void GetConditionString2(StringBuilder _sb, string BIL_ID, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            string _cus_no = CUS_NO;
            if (!string.IsNullOrEmpty(_cus_no))
            {
                if (INCUS == "Y")  //含下属
                    _sb.Append(" and A.Cus_NO2 in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
                else
                    _sb.Append(" And  (IsNull(A.CUS_NO2,'')='" + _cus_no + "')");
            }

            //_sb.Append(" And  (IsNull(A.TAX_ID,'')='" + ht["TAX_ID"].ToString() + "') ");  //税别

            _sb.Append(" And ((A.TURN_ID2='" + TURN_ID + "') Or (IsNull(A.TURN_ID2,'')='')) ");
            //_sb.Append(" And (IsNull(A.Zhang_ID,'')='" + ht["Zhang_ID"].ToString() + "' )  And (IsNull(KP_ID,'')<>'T')");
            if (!string.IsNullOrEmpty(DEP))
                _sb.Append(" And  (IsNull(A.DEP,'')='" + DEP + "')");
            if (!string.IsNullOrEmpty(SAL_NO))
            {
                if (BIL_ID == "IM")
                    _sb.Append(" And  (IsNull(A.SAL_NO2,'')='" + SAL_NO + "')");
                else
                    _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + SAL_NO + "')");
            }
    

            _sb.Append(" And (A.CHK_MAN Is Not Null) And (A.CHK_MAN <>'')");
            if (!string.IsNullOrEmpty(CAS_NO))
            {
                string _str = CAS_NO;
                _str = _str.Replace(";", "','");
                _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
            }

            if (!string.IsNullOrEmpty(TASK_ID))
            {
                string _str = TASK_ID;
                _str = _str.Replace(";", ",");
                _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
            }
            if (CHKPRD == "Y")
            {
                if (!string.IsNullOrEmpty(FPRD_NO))
                    _sb.Append(" and (B.PRD_NO>='" + FPRD_NO + "')");
                if (!string.IsNullOrEmpty(EPRD_NO))
                    _sb.Append(" and (B.PRD_NO<='" + EPRD_NO + "')");

            }
            if (CHKBilType == "Y")
            {
                if (!string.IsNullOrEmpty(FBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE>='" + FBILTYPE + "')");
                if (!string.IsNullOrEmpty(EBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE<='" + EBILTYPE + "')");

            }

        }


        /// <summary>
        /// MF_EXP 专用
        /// </summary>
        /// <param name="BIL_ID">来源单据别</param>
        /// <param name="CHK_DATE">判断日期否</param>
        /// <param name="FDATE">起日期</param>
        /// <param name="EDATE">止日期</param>
        /// <param name="FPS_NO">起单号</param>
        /// <param name="EPS_NO">止单号</param>
        /// <param name="CUS_NO">客户代号</param>
        /// <param name="INCUS">客户含下属否</param>
        /// <param name="TAX_ID">税别</param>
        /// <param name="TURN_ID">会总开</param>
        /// <param name="Zhang_ID">立帐别</param>
        /// <param name="DEP">部门代号</param>
        /// <param name="SAL_NO">业务</param>
        /// <param name="CUR_ID">币别</param>
        /// <param name="CAS_NO"></param>
        /// <param name="TASK_ID"></param>
        /// <param name="CHKPRD">判断货品否</param>
        /// <param name="FPRD_NO">起货号</param>
        /// <param name="EPRD_NO">止货号</param>
        /// <param name="CHKBilType">单据别</param>
        /// <param name="FBILTYPE">起单据别</param>
        /// <param name="EBILTYPE">止单据别</param>
        /// <returns></returns>
        private string GetSqlStrForMF_EXP(string BIL_ID, string CHK_DATE, string FDATE, string EDATE, string FPS_NO, string EPS_NO, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT B.IDX_NO As PRD_NO,G.Name As PRD_NAME,'' As PRD_MARK,B.AMTN_NET,B.TAX,B.AMT,B.AMT_FP,B.AMTN_NET_FP,B.TAX_FP,0 As QTY_FP,B.ITM,0 As QTY,B.KEY_ITM As PRE_ITM,B.RTO_TAX As TAX_RTO,B.EXC_RTO,A.EP_DD AS PAY_DD,A.EP_NO As BIL_NO,A.EP_ID As BIL_ID,0 As BIL_ITM,0 As Dis_Cnt,'' As OS_NO,'' As ZHANG_ID,0 As AMTN_EP,0 As AMTN_ER,'' As OS_ID,0 As UP,B.BAT_NO,'' As CHK_TAX,'' As CUS_OS_NO,'' As SUP_PRD_NO,'' As UNIT,B.AMT_FP As AMT_CLS,B.AMTN_NET_FP As AMTN_NET_CLS,B.TAX_FP As TAX_CLS,0 As QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,A.CAS_NO,A.TASK_ID,'' As IMTAG,B.REM AS REM1,B.CUS_NO,A.VOH_ID,A.REM ");
            _sb.Append(" From MF_EXP A, TF_EXP B LEFT OUTER JOIN INDX1 G ON B.IDX_NO=G.IDX1 ");
            _sb.Append(" Where  (A.EP_ID=B.EP_ID) And (A.EP_NO=B.EP_NO)");
            _sb.Append(" and (A.EP_ID='" + BIL_ID + "') And (IsNull(B.LZ_CLS_ID,'')<>'T') ");
            if (!UpIsZero)
                _sb.Append(" And ( (IsNull(B.AMTN_NET,0)+IsNull(B.TAX,0))<>0) ");
            _sb.Append("And (IsNull(B.KP_ID,'')<>'T') "); ;
            _sb.Append(" And (A.CHK_MAN <> '') ");

            if (CHK_DATE == "Y")
                _sb.Append(" and (A.EP_DD BETWEEN '" + Convert.ToDateTime(FDATE) + "' and '" + Convert.ToDateTime(EDATE) + "') ");
            if (!string.IsNullOrEmpty(FPS_NO))
                _sb.Append(" and (A.EP_NO>='" + FPS_NO + "')");
            if (!string.IsNullOrEmpty(EPS_NO))
                _sb.Append(" and (A.EP_NO<='" + EPS_NO + "') ");
            if (!string.IsNullOrEmpty(CUR_ID))
                _sb.Append(" And  (IsNull(B.CUR_ID,'')='" + CUR_ID + "')");
            else
                _sb.Append(" And  (IsNull(B.CUR_ID,'')='')");
            _sb.Append(" And  (IsNull(B.TAX_ID,'')='" + TAX_ID + "') ");  //税别
            string _cus_no = CUS_NO;
            if (!string.IsNullOrEmpty(_cus_no))
            {
                if (INCUS == "Y")  //含下属
                    _sb.Append(" and B.Cus_NO in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
                else
                    _sb.Append(" And  (IsNull(B.CUS_NO,'')='" + _cus_no + "')");
            }
            if (!string.IsNullOrEmpty(CAS_NO))
            {
                string _str = CAS_NO;
                _str = _str.Replace(";", "','");
                _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
            }

            if (!string.IsNullOrEmpty(TASK_ID))
            {
                string _str = TASK_ID;
                _str = _str.Replace(";", ",");
                _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
            } if (!string.IsNullOrEmpty(CAS_NO))
            {
                string _str = CAS_NO;
                _str = _str.Replace(";", "','");
                _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
            }

            if (!string.IsNullOrEmpty(TASK_ID))
            {
                string _str = TASK_ID;
                _str = _str.Replace(";", ",");
                _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
            }
            if (CHKBilType == "Y")
            {
                if (!string.IsNullOrEmpty(FBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE>='" + FBILTYPE + "')");
                if (!string.IsNullOrEmpty(EBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE<='" + EBILTYPE + "')");
            }
            return _sb.ToString();
            
        }

        /// <summary>
        /// MF_BLN 专用
        /// </summary>
        /// <param name="BIL_ID">来源单据别</param>
        /// <param name="CHK_DATE">判断日期否</param>
        /// <param name="FDATE">起日期</param>
        /// <param name="EDATE">止日期</param>
        /// <param name="FPS_NO">起单号</param>
        /// <param name="EPS_NO">止单号</param>
        /// <param name="CUS_NO">客户代号</param>
        /// <param name="INCUS">客户含下属否</param>
        /// <param name="TAX_ID">税别</param>
        /// <param name="TURN_ID">会总开</param>
        /// <param name="Zhang_ID">立帐别</param>
        /// <param name="DEP">部门代号</param>
        /// <param name="SAL_NO">业务</param>
        /// <param name="CUR_ID">币别</param>
        /// <param name="CAS_NO"></param>
        /// <param name="TASK_ID"></param>
        /// <param name="CHKPRD">判断货品否</param>
        /// <param name="FPRD_NO">起货号</param>
        /// <param name="EPRD_NO">止货号</param>
        /// <param name="CHKBilType">单据别</param>
        /// <param name="FBILTYPE">起单据别</param>
        /// <param name="EBILTYPE">止单据别</param>
        /// <returns></returns>
        private string GetSqlStrForMF_BLN(string BIL_ID, string CHK_DATE, string FDATE, string EDATE, string FPS_NO, string EPS_NO, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" SELECT B.PRD_NO,B.PRD_NAME,B.PRD_MARK,B.AMTN_RNT_NET As AMTN_NET,B.TAX_RNT As TAX,B.AMT_RNT As AMT,B.AMT_FP,B.AMTN_NET_FP,B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.PRE_ITM,G.SPC_TAX As TAX_RTO,A.EXC_RTO,A.BL_DD AS PAY_DD,A.BL_NO As BIL_NO,A.BL_ID As BIL_ID,0 As BIL_ITM,0 As Dis_Cnt,'' As OS_NO,A.ZHANG_ID,0 As AMTN_EP,0 As AMTN_ER,'' As OS_ID,B.UP,B.BAT_NO,G.CHK_TAX,'' As CUS_OS_NO,B.SUP_PRD_NO,B.UNIT,A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,A.CAS_NO,A.TASK_ID,'' As IMTAG,B.REM AS REM1,A.CUS_NO,A.VOH_ID,A.REM  ");
            _sb.Append(" From MF_BLN A, TF_BLN B Left Outer Join PRDT G On B.PRD_NO=G.PRD_NO ");
            _sb.Append(" Where  (A.BL_ID=B.BL_ID) And (A.BL_NO=B.BL_NO) ");
            if (!UpIsZero)
                _sb.Append(" and ( (IsNull(B.AMTN_RNT_NET,0)+IsNull(B.TAX_RNT,0))<>0) ");
            _sb.Append(" and (A.BL_ID='" + BIL_ID + "') And (IsNull(A.LZ_CLS_ID,'')<>'T') ");
            if (CHK_DATE == "Y")
                _sb.Append(" and (A.BL_DD BETWEEN '" + Convert.ToDateTime(FDATE) + "' and '" + Convert.ToDateTime(EDATE) + "') ");
            if (!string.IsNullOrEmpty(FPS_NO))
                _sb.Append(" and (A.BL_NO>='" + FPS_NO + "')");
            if (!string.IsNullOrEmpty(EPS_NO))
                _sb.Append(" and (A.BL_NO<='" + EPS_NO + "') ");
            GetConditionString(_sb, CUS_NO, INCUS, TAX_ID, TURN_ID, Zhang_ID
              , DEP, SAL_NO, CUR_ID, CAS_NO, TASK_ID, CHKPRD, FPRD_NO, EPRD_NO, CHKBilType, FBILTYPE, EBILTYPE);


            return _sb.ToString();

        }

        /// <summary>
        /// MF_PSS 专用
        /// </summary>
        /// <param name="BIL_ID">来源单据别</param>
        /// <param name="CHK_DATE">判断日期否</param>
        /// <param name="FDATE">起日期</param>
        /// <param name="EDATE">止日期</param>
        /// <param name="FPS_NO">起单号</param>
        /// <param name="EPS_NO">止单号</param>
        /// <param name="CUS_NO">客户代号</param>
        /// <param name="INCUS">客户含下属否</param>
        /// <param name="TAX_ID">税别</param>
        /// <param name="TURN_ID">会总开</param>
        /// <param name="Zhang_ID">立帐别</param>
        /// <param name="DEP">部门代号</param>
        /// <param name="SAL_NO">业务</param>
        /// <param name="CUR_ID">币别</param>
        /// <param name="CAS_NO"></param>
        /// <param name="TASK_ID"></param>
        /// <param name="CHKPRD">判断货品否</param>
        /// <param name="FPRD_NO">起货号</param>
        /// <param name="EPRD_NO">止货号</param>
        /// <param name="CHKBilType">单据别</param>
        /// <param name="FBILTYPE">起单据别</param>
        /// <param name="EBILTYPE">止单据别</param>
        /// <returns></returns>
        private string GetSqlStrForPS(string BIL_ID, string CHK_DATE, string FDATE, string EDATE, string FPS_NO, string EPS_NO, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" SELECT B.PRD_NO,B.PRD_NAME,B.PRD_MARK,B.AMTN_NET,B.TAX,B.AMT,B.AMT_FP,B.AMTN_NET_FP,B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.PRE_ITM,B.TAX_RTO,A.EXC_RTO,A.PS_DD AS PAY_DD,A.PS_NO As BIL_NO,A.PS_ID As BIL_ID,0 As BIL_ITM,A.Dis_Cnt,A.OS_NO,A.ZHANG_ID,B.AMTN_EP,A.AMTN_EP As AMTN_ER,B.OS_ID,B.UP,B.BAT_NO,B.CHK_TAX,B.CUS_OS_NO,B.SUP_PRD_NO,B.UNIT,A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,A.AMTN_EP1,A.AMTN_EP As AMTN_ER1,A.CAS_NO,A.TASK_ID,'' As IMTAG,B.REM AS REM1,A.CUS_NO,A.VOH_ID,A.REM ");
            _sb.Append(" From MF_PSS A, TF_PSS B ");
            _sb.Append(" Where  (A.PS_ID=B.PS_ID) And (A.PS_NO=B.PS_NO) ");
            _sb.Append(" and (A.PS_ID='" + BIL_ID + "') And (IsNull(A.LZ_CLS_ID,'')<>'T') ");
            if (!UpIsZero)
                _sb.Append(" And ( (IsNull(B.AMTN_NET,0)+IsNull(B.TAX,0))<>0 )  ");
            if (BIL_ID == "SA")
            {
                _sb.Append(" And (IsNull(A.YD_ID,'')='T') ");
            }
            if (CHK_DATE == "Y")
                _sb.Append(" and (A.PS_DD BETWEEN '" + Convert.ToDateTime(FDATE) + "' and '" + Convert.ToDateTime(EDATE) + "') ");
            if (!string.IsNullOrEmpty(FPS_NO))
                _sb.Append(" and (A.PS_NO>='" + FPS_NO+ "')");
            if (!string.IsNullOrEmpty(EPS_NO))
                _sb.Append(" and (A.PS_NO<='" + EPS_NO + "') ");
            // _sb.Append(" And  (IsNull(A.TAX_ID,'')='" + ht["TAX_ID"].ToString() + "') ");  //税别
            GetConditionString(_sb,CUS_NO,INCUS,TAX_ID,TURN_ID,Zhang_ID
                  ,DEP,SAL_NO,CUR_ID,CAS_NO,TASK_ID,CHKPRD,FPRD_NO, EPRD_NO,CHKBilType,  FBILTYPE,  EBILTYPE);
            return _sb.ToString();
            
        }

        private void GetConditionString(StringBuilder _sb, string CUS_NO, string INCUS, string TAX_ID, string TURN_ID, string Zhang_ID, string DEP, string SAL_NO, string CUR_ID, string CAS_NO, string TASK_ID, string CHKPRD, string FPRD_NO, string EPRD_NO, string CHKBilType, string FBILTYPE, string EBILTYPE)
        {
            string _cus_no = CUS_NO;
            if (!string.IsNullOrEmpty(_cus_no))
            {
                if (INCUS == "Y")  //含下属
                    _sb.Append(" and A.Cus_NO in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
                else
                    _sb.Append(" And  (IsNull(A.CUS_NO,'')='" + _cus_no + "')");
            }
            _sb.Append(" And  (IsNull(A.TAX_ID,'')='" + TAX_ID + "') ");  //税别
            _sb.Append(" And ((A.TURN_ID='" + TURN_ID + "') Or (IsNull(A.TURN_ID,'')='')) ");
            _sb.Append(" And (IsNull(A.Zhang_ID,'')='" + Zhang_ID+ "' )  And (IsNull(KP_ID,'')<>'T')");
            if (!string.IsNullOrEmpty(DEP))
                _sb.Append(" And  (IsNull(A.DEP,'')='" + DEP + "')");
            if (!string.IsNullOrEmpty(SAL_NO))
                _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + SAL_NO + "')");
            if (!string.IsNullOrEmpty(CUR_ID))
                _sb.Append(" And  (IsNull(A.CUR_ID,'')='" + CUR_ID + "')");
            else
                _sb.Append(" And  (IsNull(A.CUR_ID,'')='')");

            _sb.Append(" And (A.CHK_MAN Is Not Null) And (A.CHK_MAN <>'')");
            if (!string.IsNullOrEmpty(CAS_NO))
            {
                string _str = CAS_NO;
                _str = _str.Replace(";", "','");
                _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
            }

            if (!string.IsNullOrEmpty(TASK_ID))
            {
                string _str = TASK_ID;
                _str = _str.Replace(";", ",");
                _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
            }
            if (CHKPRD == "Y")
            {
                if (!string.IsNullOrEmpty(FPRD_NO))
                    _sb.Append(" and (B.PRD_NO>='" + FPRD_NO + "')");
                if (!string.IsNullOrEmpty(EPRD_NO))
                    _sb.Append(" and (B.PRD_NO<='" +EPRD_NO+ "')");

            }
            if (CHKBilType== "Y")
            {
                if (!string.IsNullOrEmpty(FBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE>='" + FBILTYPE + "')");
                if (!string.IsNullOrEmpty(EBILTYPE))
                    _sb.Append(" and (A.BIL_TYPE<='" + EBILTYPE + "')");
            }

        }
        #endregion



        //#region 各单据SQL 语法 for HT
        ///// <summary>
        ///// 取得过滤的资
        ///// </summary>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        //public SunlikeDataSet GetDataForBil(Dictionary<string, object> ht)
        //{
        //    string _bilID = ht["BIL_ID"].ToString();
        //    string SQLStr = "";
        //    switch (_bilID)
        //    {
        //        case "SA":
        //            SQLStr = GetSqlStrForPS(ht);
        //            break;
        //        case "SB":
        //            SQLStr = GetSqlStrForPS(ht);
        //            break;
        //        case "SD":
        //            SQLStr = GetSqlStrForPS(ht);
        //            break;
        //        case "LN":
        //            SQLStr = GetSqlStrForMF_BLN(ht);
        //            break;
        //        case "LB":
        //            SQLStr = GetSqlStrForMF_BLN(ht);
        //            break;
        //        case "ER":
        //            SQLStr = GetSqlStrForMF_EXP(ht);
        //            break;
        //        case "IO":
        //            SQLStr = GetSqlStrForMF_IC_IO(ht);
        //            break;
        //        case "IB":
        //            SQLStr = GetSqlStrForMF_IC_IB(ht);
        //            break;
        //        case "TJ":
        //            SQLStr = GetSqlStrForMF_TJ(ht);
        //            break;
        //        case "IM":
        //            SQLStr = GetSqlStrForMF_IC_IO(ht);
        //            break;
        //        case "CK":
        //            SQLStr = GetSqlStrForMFCK(ht);
        //            break;
        //        case "KB":
        //            SQLStr = GetSqlStrForMFCK(ht);
        //            break;
        //        case "PC":
        //            SQLStr = GetSqlStrForPS(ht);
        //            break;
        //        case "PB":
        //            SQLStr = GetSqlStrForPS(ht);
        //            break;
        //        case "PD":
        //            SQLStr = GetSqlStrForPS(ht);
        //            break;
        //    }
        //    if (!string.IsNullOrEmpty(ht["BILPGM"].ToString()))
        //        SQLStr += GetPowerSql(ht["USR"].ToString(), ht["COMPNO"].ToString(), ht["LOGINDEP"].ToString(), ht["LOGINUPDEP"].ToString(), ht["RIGHTPSWD"].ToString(), "A.");
        //    SunlikeDataSet _ds = new SunlikeDataSet();
        //    this.FillDataset(SQLStr, _ds, new string[] { "BILDATA" });

        //    return _ds;
        //}

        //private string GetSqlStrForMFCK(Dictionary<string, object> ht)
        //{
        //    StringBuilder _sb = new StringBuilder();
        //    _sb.Append(" SELECT B.PRD_NO,B.PRD_NAME,B.PRD_MARK,B.AMTN_NET,B.TAX,B.AMT,B.AMT_FP,B.AMTN_NET_FP,");
        //    _sb.Append(" B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.PRE_ITM,B.TAX_RTO,A.EXC_RTO,A.CK_DD AS PAY_DD,A.CK_NO As BIL_NO,A.CK_ID As BIL_ID,0 As BIL_ITM,");
        //    _sb.Append(" A.Dis_Cnt,A.OS_NO,B.ZHANG_ID,0 As AMTN_EP,0 As AMTN_ER,B.OS_NO As OS_NO_B,B.OTH_ITM,B.OS_ID,B.UP,B.BAT_NO,B.CHK_TAX, ");
        //    _sb.Append(" Cast (A.REM As VarChar(200)) As REM, ");
        //    _sb.Append(" B.CUS_OS_NO,B.SUP_PRD_NO,B.UNIT,'' AS PSSCK_NOA,A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,0 As AMTN_EP1, ");
        //    _sb.Append("  0 As AMTN_ER1,A.CAS_NO,A.TASK_ID,B.REM AS REM1,A.CUS_NO,A.PAY_MTH,A.CUR_ID,A.DEP,A.SAL_NO,A.TAX_ID,'' as VOH_ID,'' IMTAG   ");
        //    _sb.Append(" From MF_CK A,TF_CK B ");
        //    GetSQLStrForCK(_sb, ht);
        //    if (ht["CHK_DATE"].ToString() == "Y")
        //        _sb.Append(" and (A.CK_DD BETWEEN '" + Convert.ToDateTime(ht["FDATE"].ToString()) + "' and '" + Convert.ToDateTime(ht["EDATE"].ToString()) + "') ");
        //    if (!string.IsNullOrEmpty(ht["FPS_NO"].ToString()))
        //        _sb.Append(" and (A.CK_NO>='" + ht["FPS_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["EPS_NO"].ToString()))
        //        _sb.Append(" and (A.CK_NO<='" + ht["EPS_NO"].ToString() + "') ");
        //    return _sb.ToString();
        //}

        //private void GetSQLStrForCK(StringBuilder _sb, Dictionary<string, object> ht)
        //{
        //    _sb.Append(" Where ");
        //    _sb.Append("(A.CK_ID='" + ht["BIL_ID"] + "')");
        //    _sb.Append(" and (A.CK_ID=B.CK_ID) And (A.CK_NO=B.CK_NO) And (IsNull(A.LZ_CLS_ID,'')<>'T')");
        //    _sb.Append(" and ((A.TURN_ID='" + ht["TURN_ID"].ToString() + "') Or (IsNull(A.TURN_ID,'')='')) ");
        //    if (!UpIsZero)
        //        _sb.Append(" And (IsNull(B.AMTN_NET,0)+IsNull(B.TAX,0)<>0) ");
        //    _sb.Append(" And (IsNull(B.Zhang_ID,'')='" + ht["Zhang_ID"].ToString() + "' )  ");
        //    _sb.Append(" And (IsNull(A.YD_ID,'')='T') ");
        //    if (!string.IsNullOrEmpty(ht["DEP"].ToString()))
        //        _sb.Append(" And  (IsNull(A.DEP,'')='" + ht["DEP"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["SAL_NO"].ToString()))
        //        _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + ht["SAL_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["CUR_ID"].ToString()))
        //        _sb.Append(" And  (IsNull(A.CUR_ID,'')='" + ht["CUR_ID"].ToString() + "')");
        //    else
        //        _sb.Append(" And  (IsNull(A.CUR_ID,'')='')");
        //    _sb.Append(" And  (IsNull(A.TAX_ID,'')='" + ht["TAX_ID"].ToString() + "') ");  //税别
        //    _sb.Append(" And (A.CHK_MAN Is Not Null) And (A.CHK_MAN <>'')");
        //    string _cus_no = ht["CUS_NO"].ToString();
        //    if (!string.IsNullOrEmpty(_cus_no))
        //    {
        //        if (ht["INCUS"].ToString() == "Y")  //含下属
        //            _sb.Append(" and A.Cus_NO in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
        //        else
        //            _sb.Append(" And  (IsNull(A.CUS_NO,'')='" + _cus_no + "')");
        //    }


        //}

        //private string GetSqlStrForMF_TJ(Dictionary<string, object> ht)
        //{
        //    StringBuilder _sb = new StringBuilder();
        //    _sb.Append(" SELECT B.PRD_NO,G.NAME As PRD_NAME,B.PRD_MARK,B.AMTN_NET,0 TAX,B.AMTN_NET AS AMT,B.AMT_FP,B.AMTN_NET_FP,");
        //    _sb.Append(" B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.KEY_ITM As PRE_ITM,G.SPC_TAX AS TAX_RTO,1 As EXC_RTO,A.TJ_DD As PAY_DD,A.TJ_NO As BIL_NO,'TJ' As BIL_ID,0 As BIL_ITM,");
        //    _sb.Append(" 0 AS DIS_CNT,'' AS OS_NO,'' AS ZHANG_ID,0 AS AMTN_EP,0 As AMTN_ER,'' AS OS_ID,B.UPR4 As UP,'' AS BAT_NO,G.CHK_TAX,'' As CUS_OS_NO,'' As SUP_PRD_NO,'1' As UNIT,");
        //    _sb.Append(" A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,'' As CAS_NO, 0 As TASK_ID,'' As IMTAG,B.REM AS REM1,A.CUS_NO,'' as VOH_ID ");
        //    _sb.Append(" FROM MF_TJ A,TF_TJ B LEFT OUTER JOIN PRDT G ON B.PRD_NO=G.PRD_NO ");

        //    GetSQLStrForTJ(_sb, ht);
        //    if (ht["CHK_DATE"].ToString() == "Y")
        //        _sb.Append(" and (A.TJ_DD BETWEEN '" + Convert.ToDateTime(ht["FDATE"].ToString()) + "' and '" + Convert.ToDateTime(ht["EDATE"].ToString()) + "') ");
        //    if (!string.IsNullOrEmpty(ht["FPS_NO"].ToString()))
        //        _sb.Append(" and (A.TJ_NO>='" + ht["FPS_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["EPS_NO"].ToString()))
        //        _sb.Append(" and (A.TJ_NO<='" + ht["EPS_NO"].ToString() + "') ");
        //    if (ht.ContainsKey("CHKAUD") && ht["CHKAUD"].ToString() == "T")
        //    {
        //        _sb.Append(" And (IsNull(AUD_FLAG,'')='T') ");
        //    }
        //    return _sb.ToString();
        //}

        //private void GetSQLStrForTJ(StringBuilder _sb, Dictionary<string, object> ht)
        //{
        //    _sb.Append(" Where  (A.TJ_NO=B.TJ_NO) And (IsNull(A.LZ_CLS_ID,'')<>'T')");
        //    if (!UpIsZero)
        //        _sb.Append(" And (IsNull(B.AMTN_NET,0)<>0) ");
        //    _sb.Append(" and ((A.TURN_ID='" + ht["TURN_ID"].ToString() + "') Or (IsNull(A.TURN_ID,'')='')) ");
        //    if (!string.IsNullOrEmpty(ht["DEP"].ToString()))
        //        _sb.Append(" AND (IsNull(A.DEP,'')='" + ht["DEP"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["SAL_NO"].ToString()))
        //        _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + ht["SAL_NO"].ToString() + "')");

        //    string _cus_no = ht["CUS_NO"].ToString();
        //    if (!string.IsNullOrEmpty(_cus_no))
        //    {
        //        if (ht["INCUS"].ToString() == "Y")  //含下属
        //            _sb.Append(" and A.Cus_NO in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
        //        else
        //            _sb.Append(" And  (IsNull(A.CUS_NO,'')='" + _cus_no + "')");
        //    }
        //    _sb.Append(" And (IsNull(A.CHK_MAN,'')<>'') ");
        //    //if ChkAud_Flag then SQLStr := SQLStr + ' And (IsNull(A.AUD_FLAG,'''')=''T'') ';//wfp 2006.08.28



        //    if (ht["CHKPRD"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FPRD_NO"].ToString()))
        //            _sb.Append(" and (B.PRD_NO>='" + ht["FPRD_NO"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EPRD_NO"].ToString()))
        //            _sb.Append(" and (B.PRD_NO<='" + ht["EPRD_NO"].ToString() + "')");

        //    }
        //    if (ht["CHKBilType"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE>='" + ht["FBILTYPE"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE<='" + ht["EBILTYPE"].ToString() + "')");

        //    }


        //}

        //private string GetSqlStrForMF_IC_IB(Dictionary<string, object> ht)
        //{
        //    StringBuilder _sb = new StringBuilder();
        //    _sb.Append("SELECT B.PRD_NO,G.NAME As PRD_NAME,B.PRD_MARK,IsNull(B.CST,0)-IsNull(B.AMTN_EP,0) As AMTN_NET,0 TAX,IsNull(B.CST,0)-IsNull(B.AMTN_EP,0) AS AMT,B.AMT_FP,B.AMTN_NET_FP, ");
        //    _sb.Append(" B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.KEY_ITM As PRE_ITM,G.SPC_TAX AS TAX_RTO,1 As EXC_RTO,A.IC_DD AS PAY_DD,A.IC_NO As BIL_NO,A.IC_ID As BIL_ID,0 As BIL_ITM, ");
        //    _sb.Append(" 0 AS DIS_CNT,'' As OS_NO,'' AS ZHANG_ID,0 AS AMTN_EP,0 As AMTN_ER,'' As OS_ID,B.UP,B.BAT_NO,G.CHK_TAX,'' As CUS_OS_NO,'' As SUP_PRD_NO,B.UNIT,");
        //    _sb.Append(" A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,A.CAS_NO,A.TASK_ID, ");
        //    _sb.Append(" IMTAG=(Case When A.IC_ID='IM' then 'O' else '' end),B.REM AS REM1,A.CUS_NO1 As CUS_NO,A.VOH_ID ");
        //    _sb.Append(" FROM MF_IC A,TF_IC B LEFT OUTER JOIN PRDT G ON B.PRD_NO=G.PRD_NO ");


        //    GetSQLStrForIB(_sb, ht);

        //    if (ht["CHK_DATE"].ToString() == "Y")
        //        _sb.Append(" and (A.IC_DD BETWEEN '" + Convert.ToDateTime(ht["FDATE"].ToString()) + "' and '" + Convert.ToDateTime(ht["EDATE"].ToString()) + "') ");
        //    if (!string.IsNullOrEmpty(ht["FPS_NO"].ToString()))
        //        _sb.Append(" and (A.IC_NO>='" + ht["FPS_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["EPS_NO"].ToString()))
        //        _sb.Append(" and (A.IC_NO<='" + ht["EPS_NO"].ToString() + "') ");
        //    return _sb.ToString();
        //}

        //private void GetSQLStrForIB(StringBuilder _sb, Dictionary<string, object> ht)
        //{
        //    string _id = ht["BIL_ID"].ToString();
        //    _sb.Append(" Where (A.IC_ID=B.IC_ID) AND (A.IC_NO=B.IC_NO) And (IsNull(LZ_CLS_ID,'')<>'T') ");
        //    //if Not Chk_Up_Zero then
        //    if (!UpIsZero)
        //        _sb.Append(" And (IsNull(B.CST,0)<>0)");
        //    _sb.Append(" And ((A.TURN_ID='" + ht["TURN_ID"].ToString() + "') Or (IsNull(A.TURN_ID,'')='')) ");

        //    if (!string.IsNullOrEmpty(ht["DEP"].ToString()))
        //    {
        //        if (_id == "IB")
        //            _sb.Append(" AND (IsNull(A.OUTDEP,'')='" + ht["DEP"].ToString() + "')");
        //        else
        //            _sb.Append(" AND (IsNull(A.DEP,'')='" + ht["DEP"].ToString() + "')");
        //    }
        //    if (!string.IsNullOrEmpty(ht["SAL_NO"].ToString()))
        //        _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + ht["SAL_NO"].ToString() + "')");
        //    string _cus_no = ht["CUS_NO"].ToString();
        //    if (!string.IsNullOrEmpty(_cus_no))
        //    {
        //        if (ht["INCUS"].ToString() == "Y")  //含下属
        //            _sb.Append(" and A.Cus_NO1 in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
        //        else
        //            _sb.Append(" And  (IsNull(A.CUS_NO1,'')='" + _cus_no + "')");
        //    }
        //    _sb.Append(" And (A.CHK_MAN Is Not Null) And (A.CHK_MAN <>'')");
        //    if (!string.IsNullOrEmpty(ht["CAS_NO"].ToString()))
        //    {
        //        string _str = ht["CAS_NO"].ToString();
        //        _str = _str.Replace(";", "','");
        //        _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
        //    }

        //    if (!string.IsNullOrEmpty(ht["TASK_ID"].ToString()))
        //    {
        //        string _str = ht["TASK_ID"].ToString();
        //        _str = _str.Replace(";", ",");
        //        _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
        //    }
        //    if (ht["CHKPRD"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FPRD_NO"].ToString()))
        //            _sb.Append(" and (B.PRD_NO>='" + ht["FPRD_NO"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EPRD_NO"].ToString()))
        //            _sb.Append(" and (B.PRD_NO<='" + ht["EPRD_NO"].ToString() + "')");

        //    }
        //    if (ht["CHKBilType"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE>='" + ht["FBILTYPE"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE<='" + ht["EBILTYPE"].ToString() + "')");

        //    }

        //}

        //private string GetSqlStrForMF_IC_IO(Dictionary<string, object> ht)
        //{
        //    StringBuilder _sb = new StringBuilder();

        //    _sb.Append(" SELECT B.PRD_NO,G.NAME As PRD_NAME,B.PRD_MARK,B.AMTN_NET,0.0 TAX,B.AMTN_NET AS AMT,B.AMT_FP2 As AMT_FP,B.AMTN_NET_FP2 As AMTN_NET_FP, ");
        //    _sb.Append(" B.TAX_FP2 As TAX_FP,B.QTY_FP2 As QTY_FP,B.ITM,B.QTY,B.KEY_ITM As PRE_ITM,G.SPC_TAX AS TAX_RTO,1 As EXC_RTO,A.IC_DD AS PAY_DD,A.IC_NO As BIL_NO,A.IC_ID As BIL_ID,0 As BIL_ITM, ");
        //    _sb.Append(" 0 As DIS_CNT,'' As OS_NO,'' AS ZHANG_ID,0 AS AMTN_EP,0 As AMTN_ER,'' As OS_ID,B.UP,B.BAT_NO2 As BAT_NO,G.CHK_TAX,'' As CUS_OS_NO,'' As SUP_PRD_NO,B.UNIT, ");
        //    _sb.Append(" A.AMT_CLS2 As AMT_CLS,A.AMTN_NET_CLS2 As AMTN_NET_CLS,A.TAX_CLS2 As TAX_CLS,A.QTY_CLS2 As QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,A.CAS_NO,A.TASK_ID, ");
        //    _sb.Append(" IMTAG=(Case When A.IC_ID='IM' then 'I' else '' end),B.REM AS REM1,A.CUS_NO2 As CUS_NO,A.VOH_ID  ");
        //    _sb.Append(" FROM MF_IC A,TF_IC B LEFT OUTER JOIN PRDT G ON B.PRD_NO=G.PRD_NO ");

        //    _sb.Append(" Where  (A.IC_ID=B.IC_ID) And (A.IC_NO=B.IC_NO) And (IsNull(LZ_CLS_ID2,'')<>'T') ");
        //    _sb.Append(" and (A.IC_ID='" + ht["BIL_ID"] + "') ");
        //    //if Not Chk_Up_Zero then
        //    if (!UpIsZero)
        //        _sb.Append(" And (IsNull(B.AMTN_NET,0)<>0)");


        //    if (ht["CHK_DATE"].ToString() == "Y")
        //        _sb.Append(" and (A.IC_DD BETWEEN '" + Convert.ToDateTime(ht["FDATE"].ToString()) + "' and '" + Convert.ToDateTime(ht["EDATE"].ToString()) + "') ");
        //    if (!string.IsNullOrEmpty(ht["FPS_NO"].ToString()))
        //        _sb.Append(" and (A.IC_NO>='" + ht["FPS_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["EPS_NO"].ToString()))
        //        _sb.Append(" and (A.IC_NO<='" + ht["EPS_NO"].ToString() + "') ");
        //    GetConditionString2(_sb, ht);

        //    return _sb.ToString();

        //}

        //private string GetSqlStrForMF_EXP(Dictionary<string, object> ht)
        //{
        //    StringBuilder _sb = new StringBuilder();
        //    _sb.Append("SELECT B.IDX_NO As PRD_NO,G.Name As PRD_NAME,'' As PRD_MARK,B.AMTN_NET,B.TAX,B.AMT,B.AMT_FP,B.AMTN_NET_FP,B.TAX_FP,0 As QTY_FP,B.ITM,0 As QTY,B.KEY_ITM As PRE_ITM,B.RTO_TAX As TAX_RTO,B.EXC_RTO,A.EP_DD AS PAY_DD,A.EP_NO As BIL_NO,A.EP_ID As BIL_ID,0 As BIL_ITM,0 As Dis_Cnt,'' As OS_NO,'' As ZHANG_ID,0 As AMTN_EP,0 As AMTN_ER,'' As OS_ID,0 As UP,B.BAT_NO,'' As CHK_TAX,'' As CUS_OS_NO,'' As SUP_PRD_NO,'' As UNIT,B.AMT_FP As AMT_CLS,B.AMTN_NET_FP As AMTN_NET_CLS,B.TAX_FP As TAX_CLS,0 As QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,A.CAS_NO,A.TASK_ID,'' As IMTAG,B.REM AS REM1,B.CUS_NO,A.VOH_ID ");
        //    _sb.Append(" From MF_EXP A, TF_EXP B LEFT OUTER JOIN INDX1 G ON B.IDX_NO=G.IDX1 ");
        //    _sb.Append(" Where  (A.EP_ID=B.EP_ID) And (A.EP_NO=B.EP_NO)");
        //    _sb.Append(" and (A.EP_ID='" + ht["BIL_ID"] + "') And (IsNull(B.LZ_CLS_ID,'')<>'T') ");
        //    if (!UpIsZero)
        //        _sb.Append(" And (IsNull(B.AMTN_NET,0)+IsNull(B.TAX,0)<>0) ");
        //    _sb.Append("And (IsNull(B.KP_ID,'')<>'T') "); ;
        //    _sb.Append(" And (A.CHK_MAN <> '') ");

        //    if (ht["CHK_DATE"].ToString() == "Y")
        //        _sb.Append(" and (A.EP_DD BETWEEN '" + Convert.ToDateTime(ht["FDATE"].ToString()) + "' and '" + Convert.ToDateTime(ht["EDATE"].ToString()) + "') ");
        //    if (!string.IsNullOrEmpty(ht["FPS_NO"].ToString()))
        //        _sb.Append(" and (A.EP_NO>='" + ht["FPS_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["EPS_NO"].ToString()))
        //        _sb.Append(" and (A.EP_NO<='" + ht["EPS_NO"].ToString() + "') ");
        //    if (!string.IsNullOrEmpty(ht["CUR_ID"].ToString()))
        //        _sb.Append(" And  (IsNull(B.CUR_ID,'')='" + ht["CUR_ID"].ToString() + "')");
        //    else
        //        _sb.Append(" And  (IsNull(B.CUR_ID,'')='')");
        //    _sb.Append(" And  (IsNull(B.TAX_ID,'')='" + ht["TAX_ID"].ToString() + "') ");  //税别
        //    string _cus_no = ht["CUS_NO"].ToString();
        //    if (!string.IsNullOrEmpty(_cus_no))
        //    {
        //        if (ht["INCUS"].ToString() == "Y")  //含下属
        //            _sb.Append(" and B.Cus_NO in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
        //        else
        //            _sb.Append(" And  (IsNull(B.CUS_NO,'')='" + _cus_no + "')");
        //    }
        //    if (!string.IsNullOrEmpty(ht["CAS_NO"].ToString()))
        //    {
        //        string _str = ht["CAS_NO"].ToString();
        //        _str = _str.Replace(";", "','");
        //        _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
        //    }

        //    if (!string.IsNullOrEmpty(ht["TASK_ID"].ToString()))
        //    {
        //        string _str = ht["TASK_ID"].ToString();
        //        _str = _str.Replace(";", ",");
        //        _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
        //    } if (!string.IsNullOrEmpty(ht["CAS_NO"].ToString()))
        //    {
        //        string _str = ht["CAS_NO"].ToString();
        //        _str = _str.Replace(";", "','");
        //        _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
        //    }

        //    if (!string.IsNullOrEmpty(ht["TASK_ID"].ToString()))
        //    {
        //        string _str = ht["TASK_ID"].ToString();
        //        _str = _str.Replace(";", ",");
        //        _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
        //    }
        //    if (ht["CHKBilType"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE>='" + ht["FBILTYPE"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE<='" + ht["EBILTYPE"].ToString() + "')");
        //    }
        //    return _sb.ToString();
        //}

        //private string GetSqlStrForMF_BLN(Dictionary<string, object> ht)
        //{
        //    StringBuilder _sb = new StringBuilder();
        //    _sb.Append(" SELECT B.PRD_NO,B.PRD_NAME,B.PRD_MARK,B.AMTN_RNT_NET As AMTN_NET,B.TAX_RNT As TAX,B.AMT_RNT As AMT,B.AMT_FP,B.AMTN_NET_FP,B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.PRE_ITM,G.SPC_TAX As TAX_RTO,A.EXC_RTO,A.BL_DD AS PAY_DD,A.BL_NO As BIL_NO,A.BL_ID As BIL_ID,0 As BIL_ITM,0 As Dis_Cnt,'' As OS_NO,A.ZHANG_ID,0 As AMTN_EP,0 As AMTN_ER,'' As OS_ID,B.UP,B.BAT_NO,G.CHK_TAX,'' As CUS_OS_NO,B.SUP_PRD_NO,B.UNIT,A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,0 As AMTN_EP1,0 As AMTN_ER1,A.CAS_NO,A.TASK_ID,'' As IMTAG,B.REM AS REM1,A.CUS_NO,A.VOH_ID  ");
        //    _sb.Append(" From MF_BLN A, TF_BLN B Left Outer Join PRDT G On B.PRD_NO=G.PRD_NO ");
        //    _sb.Append(" Where  (A.BL_ID=B.BL_ID) And (A.BL_NO=B.BL_NO) ");
        //    if (!UpIsZero)
        //        _sb.Append(" and (IsNull(B.AMTN_RNT_NET,0)+IsNull(B.TAX_RNT,0)<>0) ");
        //    _sb.Append(" and (A.BL_ID='" + ht["BIL_ID"] + "') And (IsNull(A.LZ_CLS_ID,'')<>'T') ");
        //    if (ht["CHK_DATE"].ToString() == "Y")
        //        _sb.Append(" and (A.BL_DD BETWEEN '" + Convert.ToDateTime(ht["FDATE"].ToString()) + "' and '" + Convert.ToDateTime(ht["EDATE"].ToString()) + "') ");
        //    if (!string.IsNullOrEmpty(ht["FPS_NO"].ToString()))
        //        _sb.Append(" and (A.BL_NO>='" + ht["FPS_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["EPS_NO"].ToString()))
        //        _sb.Append(" and (A.BL_NO<='" + ht["EPS_NO"].ToString() + "') ");
        //    GetConditionString(_sb, ht);

        //    return _sb.ToString();
        //}

        ///// <summary>
        ///// 取得销货单，锁退，销折过滤资料
        ///// </summary>
        ///// <param name="ht"></param>
        ///// <returns></returns>
        //private string GetSqlStrForPS(Dictionary<string, object> ht)
        //{
        //    StringBuilder _sb = new StringBuilder();
        //    _sb.Append(" SELECT B.PRD_NO,B.PRD_NAME,B.PRD_MARK,B.AMTN_NET,B.TAX,B.AMT,B.AMT_FP,B.AMTN_NET_FP,B.TAX_FP,B.QTY_FP,B.ITM,B.QTY,B.PRE_ITM,B.TAX_RTO,A.EXC_RTO,A.PS_DD AS PAY_DD,A.PS_NO As BIL_NO,A.PS_ID As BIL_ID,0 As BIL_ITM,A.Dis_Cnt,A.OS_NO,A.ZHANG_ID,B.AMTN_EP,A.AMTN_EP As AMTN_ER,B.OS_ID,B.UP,B.BAT_NO,B.CHK_TAX,B.CUS_OS_NO,B.SUP_PRD_NO,B.UNIT,A.AMT_CLS,A.AMTN_NET_CLS,A.TAX_CLS,A.QTY_CLS,A.AMTN_EP1,A.AMTN_EP As AMTN_ER1,A.CAS_NO,A.TASK_ID,'' As IMTAG,B.REM AS REM1,A.CUS_NO,A.VOH_ID ");
        //    _sb.Append(" From MF_PSS A, TF_PSS B ");
        //    _sb.Append(" Where  (A.PS_ID=B.PS_ID) And (A.PS_NO=B.PS_NO) ");
        //    _sb.Append(" and (A.PS_ID='" + ht["BIL_ID"] + "') And (IsNull(A.LZ_CLS_ID,'')<>'T') ");
        //    if (!UpIsZero)
        //        _sb.Append(" And (IsNull(B.AMTN_NET,0)+IsNull(B.TAX,0)<>0 )  ");
        //    if (ht["BIL_ID"].ToString() == "SA")
        //    {
        //        _sb.Append(" And (IsNull(A.YD_ID,'')='T') ");
        //    }
        //    if (ht["CHK_DATE"].ToString() == "Y")
        //        _sb.Append(" and (A.PS_DD BETWEEN '" + Convert.ToDateTime(ht["FDATE"].ToString()) + "' and '" + Convert.ToDateTime(ht["EDATE"].ToString()) + "') ");
        //    if (!string.IsNullOrEmpty(ht["FPS_NO"].ToString()))
        //        _sb.Append(" and (A.PS_NO>='" + ht["FPS_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["EPS_NO"].ToString()))
        //        _sb.Append(" and (A.PS_NO<='" + ht["EPS_NO"].ToString() + "') ");
        //    // _sb.Append(" And  (IsNull(A.TAX_ID,'')='" + ht["TAX_ID"].ToString() + "') ");  //税别
        //    GetConditionString(_sb, ht);
        //    return _sb.ToString();

        //}

        //private void GetConditionString(StringBuilder _sb, Dictionary<string, object> ht)
        //{

        //    string _cus_no = ht["CUS_NO"].ToString();
        //    if (!string.IsNullOrEmpty(_cus_no))
        //    {
        //        if (ht["INCUS"].ToString() == "Y")  //含下属
        //            _sb.Append(" and A.Cus_NO in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
        //        else
        //            _sb.Append(" And  (IsNull(A.CUS_NO,'')='" + _cus_no + "')");
        //    }
        //    _sb.Append(" And  (IsNull(A.TAX_ID,'')='" + ht["TAX_ID"].ToString() + "') ");  //税别
        //    _sb.Append(" And ((A.TURN_ID='" + ht["TURN_ID"].ToString() + "') Or (IsNull(A.TURN_ID,'')='')) ");
        //    _sb.Append(" And (IsNull(A.Zhang_ID,'')='" + ht["Zhang_ID"].ToString() + "' )  And (IsNull(KP_ID,'')<>'T')");
        //    if (!string.IsNullOrEmpty(ht["DEP"].ToString()))
        //        _sb.Append(" And  (IsNull(A.DEP,'')='" + ht["DEP"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["SAL_NO"].ToString()))
        //        _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + ht["SAL_NO"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["CUR_ID"].ToString()))
        //        _sb.Append(" And  (IsNull(A.CUR_ID,'')='" + ht["CUR_ID"].ToString() + "')");
        //    else
        //        _sb.Append(" And  (IsNull(A.CUR_ID,'')='')");

        //    _sb.Append(" And (A.CHK_MAN Is Not Null) And (A.CHK_MAN <>'')");
        //    if (!string.IsNullOrEmpty(ht["CAS_NO"].ToString()))
        //    {
        //        string _str = ht["CAS_NO"].ToString();
        //        _str = _str.Replace(";", "','");
        //        _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
        //    }

        //    if (!string.IsNullOrEmpty(ht["TASK_ID"].ToString()))
        //    {
        //        string _str = ht["TASK_ID"].ToString();
        //        _str = _str.Replace(";", ",");
        //        _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
        //    }
        //    if (ht["CHKPRD"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FPRD_NO"].ToString()))
        //            _sb.Append(" and (B.PRD_NO>='" + ht["FPRD_NO"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EPRD_NO"].ToString()))
        //            _sb.Append(" and (B.PRD_NO<='" + ht["EPRD_NO"].ToString() + "')");

        //    }
        //    if (ht["CHKBilType"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE>='" + ht["FBILTYPE"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE<='" + ht["EBILTYPE"].ToString() + "')");
        //    }

        //}
        //private void GetConditionString2(StringBuilder _sb, Dictionary<string, object> ht)
        //{

        //    string _cus_no = ht["CUS_NO"].ToString();
        //    if (!string.IsNullOrEmpty(_cus_no))
        //    {
        //        if (ht["INCUS"].ToString() == "Y")  //含下属
        //            _sb.Append(" and A.Cus_NO2 in (select CUS_NO from fn_GetSubCustTree('" + _cus_no + "'))");
        //        else
        //            _sb.Append(" And  (IsNull(A.CUS_NO2,'')='" + _cus_no + "')");
        //    }

        //    //_sb.Append(" And  (IsNull(A.TAX_ID,'')='" + ht["TAX_ID"].ToString() + "') ");  //税别

        //    _sb.Append(" And ((A.TURN_ID2='" + ht["TURN_ID"].ToString() + "') Or (IsNull(A.TURN_ID2,'')='')) ");
        //    //_sb.Append(" And (IsNull(A.Zhang_ID,'')='" + ht["Zhang_ID"].ToString() + "' )  And (IsNull(KP_ID,'')<>'T')");
        //    if (!string.IsNullOrEmpty(ht["DEP"].ToString()))
        //        _sb.Append(" And  (IsNull(A.DEP,'')='" + ht["DEP"].ToString() + "')");
        //    if (!string.IsNullOrEmpty(ht["SAL_NO"].ToString()))
        //    {
        //        if (ht["BIL_ID"].ToString() == "IM")
        //            _sb.Append(" And  (IsNull(A.SAL_NO2,'')='" + ht["SAL_NO"].ToString() + "')");
        //        else
        //            _sb.Append(" And  (IsNull(A.SAL_NO,'')='" + ht["SAL_NO"].ToString() + "')");
        //    }
        //    //if (!string.IsNullOrEmpty(ht["CUR_ID"].ToString()))
        //    //    _sb.Append(" And  (IsNull(A.CUR_ID,'')='" + ht["CUR_ID"].ToString() + "')");
        //    //else
        //    //    _sb.Append(" And  (IsNull(A.CUR_ID,'')='')");

        //    _sb.Append(" And (A.CHK_MAN Is Not Null) And (A.CHK_MAN <>'')");
        //    if (!string.IsNullOrEmpty(ht["CAS_NO"].ToString()))
        //    {
        //        string _str = ht["CAS_NO"].ToString();
        //        _str = _str.Replace(";", "','");
        //        _sb.Append(" And (A.CAS_NO In ('" + _str + "'))");
        //    }

        //    if (!string.IsNullOrEmpty(ht["TASK_ID"].ToString()))
        //    {
        //        string _str = ht["TASK_ID"].ToString();
        //        _str = _str.Replace(";", ",");
        //        _sb.Append(" And (A.TASK_ID In ('" + _str + "'))");
        //    }
        //    if (ht["CHKPRD"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FPRD_NO"].ToString()))
        //            _sb.Append(" and (B.PRD_NO>='" + ht["FPRD_NO"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EPRD_NO"].ToString()))
        //            _sb.Append(" and (B.PRD_NO<='" + ht["EPRD_NO"].ToString() + "')");

        //    }
        //    if (ht["CHKBilType"].ToString() == "Y")
        //    {
        //        if (!string.IsNullOrEmpty(ht["FBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE>='" + ht["FBILTYPE"].ToString() + "')");
        //        if (!string.IsNullOrEmpty(ht["EBILTYPE"].ToString()))
        //            _sb.Append(" and (A.BIL_TYPE<='" + ht["EBILTYPE"].ToString() + "')");

        //    }

        //}
        //#endregion

    }
}
