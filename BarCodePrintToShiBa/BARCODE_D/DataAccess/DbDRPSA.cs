using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPSA.
	/// </summary>
    public class DbDRPSA : DbObject
    {
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="connectionString">SQL�����ִ�</param>
        public DbDRPSA(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// ȡ������������
        /// </summary>
        /// <param name="PsID">���ݴ���</param>
        /// <param name="PsNo">���ݺ���</param>
        /// <param name="OnlyFillSchema">�Ƿ�ֻ��ȡSchema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string PsID, string PsNo, bool OnlyFillSchema)
        {
            string _sqlOsHead = "";
            string _sqlOsJoin = "";
            string _rpId = "1";
            if (string.Compare("SA", PsID) == 0)
            {
                _sqlOsHead = ",SO.MTN_TYPE,SO.MTN_ALL_ID,SO.MTN_DD,SO.RTN_DD,SO.WC_NO ";
                _sqlOsJoin = " left join TF_POS SO on SO.OS_ID=A.OS_ID AND SO.OS_NO = A.OS_NO AND SO.PRE_ITM = A.OTH_ITM ";
                _rpId = "1";
            }
            if (string.Compare("SB", PsID) == 0)
            {
                _rpId = "2";
            }
            string _sqlPlus = "";
            _sqlPlus += ",A.CUS_NO_POS,A.INST_TEAM,A.AMTN_DS";
            string _sqlCaseTf = "";


            string _sql = "select A.PS_ID,A.PS_NO,A.PS_DD,A.PAY_DD,A.CHK_DD,A.CUS_NO,B.NAME as CUS_NAME,A.DEP,D.NAME as DEP_NAME,A.TAX_ID,A.SEND_MTH,"
                + "A.SEND_WH,A.ZHANG_ID,A.CUR_ID,A.EXC_RTO,A.SAL_NO,C.NAME as SAL_NAME,A.ARP_NO,A.PAY_MTH,A.PAY_DAYS,A.CHK_DAYS,A.INT_DAYS,A.PAY_REM,"
                + "A.CLS_ID,A.USR,A.CHK_MAN,A.PRT_SW,A.CLS_DATE,A.CK_CLS_ID,A.LZ_CLS_ID,A.CLSCK,A.CLSLZ,A.YD_ID,A.BIL_TYPE,A.PH_NO,A.CUST_YG,A.OS_ID,A.CUS_OS_NO,"
                + "E.NAME as CUST_YG_NAME,isnull(F.AMTN_RCV,0) as AMTN_RCV,A.OS_NO,A.PO_ID,A.SYS_DATE,A.BIL_NO,A.REM,A.TOT_QTY,A.CARD_NO,A.SEND_AREA,"
                + "A.EP_NO,A.EP_NO1,A.VOH_ID,A.VOH_NO,A.INV_NO,A.CLS_REM,A.ADR,A.RP_NO,A.AMTN_IRP,A.AMT_IRP,A.TAX_IRP,A.MOB_ID,H.AMT_BB,H.AMTN_BB,H.AMT_BC,"
                + "H.AMTN_BC,H.CACC_NO,H.BACC_NO,H.AMT_CHK AMT_CHK,H.AMTN_CHK AMTN_CHK,H.AMT_OTHER,H.AMTN_OTHER,I.CHK_NO,I.CHK_KND,I.BANK_NO,I.BACC_NO BACC_NO_CHK,I.CRECARD_NO,I.END_DD,"
                + "A.CB_ID,A.AMTN_EP,A.AMTN_EP1,A.KP_ID,A.PO_NO,A.POPC_ID,A.HAS_FX,A.ISSVS,A.AMTN_CBAC,G.CBAC_CLS,A.LOCK_MAN,A.AMTN_NET_CLS,A.AMT_CLS,A.TAX_CLS,A.QTY_CLS,A.TURN_ID,A.DIS_CNT,A.ACC_FP_NO,A.CAS_NO,A.TASK_ID,A.SB_CHK,A.SUB_NO,A.CUS_CARD_NO,A.CASH_ID,A.POS_OS_ID,A.POS_OS_CLS " + _sqlPlus
                + " from MF_PSS A"
                + " left join CUST B on B.CUS_NO=A.CUS_NO"
                + " left join SALM C on C.SAL_NO=A.SAL_NO"
                + " left join DEPT D on D.DEP=A.DEP"
                + " left join CUST_YG E on (E.CUS_NO=A.CUS_NO and E.YG_NO=A.CUST_YG)"
                + " left join MF_ARP F on F.ARP_ID = '1' AND F.OPN_ID = '2' AND F.ARP_NO=A.ARP_NO"
                + " left join MF_POS G on G.OS_ID=A.OS_ID and G.OS_NO=A.OS_NO"
                + " left join TF_MON H on A.RP_NO=H.RP_NO and H.RP_ID='" + _rpId + "' and H.ITM=1"
                //+ " left join TF_MON1 J on A.RP_NO=J.RP_NO and J.RP_ID='" + _rpId + "' and J.ITM=1"
                + " left join TF_MON4 N4 on A.RP_NO=N4.RP_NO and N4.RP_ID='" + _rpId + "' and N4.ITM=1"
                + " left join MF_CHK I on N4.CHK_NO=I.CHK_NO and I.CHK_ID='0'"
                + " where PS_ID=@PsID and PS_NO=@PsNo;"
                + "select A.PS_ID,A.PS_NO,A.ITM,A.PS_DD,A.WH,B.NAME as WH_NAME,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,C.SPC,C.BAR_CODE,C.UPR,A.UNIT,A.QTY_PS,A.SH_NO_CUS,A.CUS_OS_NO"
                + ",(case when A.UNIT='1' then C.UT when A.UNIT='2' then C.PK2_UT when A.UNIT='3' then C.PK3_UT else A.UNIT end) as UNIT_NAME"
                + ",A.QTY,A.QTY1,A.UP,A.UP_QTY1,A.DIS_CNT,A.AMTN_NET,A.AMT,A.UP_SALE,A.AMTN_SALE,A.PRE_ITM,A.OS_NO,A.OS_ID,A.QTY_OI,A.QTY_RTN,A.CSTN_SAL"
                + ",A.AMTN,A.TAX,A.OTH_ITM,A.VALID_DD,A.CST_STD,A.BAT_NO,A.TAX_RTO,A.EST_ITM,A.AMTN_NET_FP,A.AMT_FP,A.REM,A.MTN_REM,A.QTY_FP,A.FREE_ID,A.CHK_RTN, "
                + " A.PAK_UNIT,A.PAK_EXC,A.PAK_NW,A.PAK_WEIGHT_UNIT,A.PAK_GW,A.PAK_MEAST,A.PAK_MEAST_UNIT,A.ME_FLAG,A.QTY_SB,A.INV_B2C,A.TAX_FP,A.AMTN_COM,A.CK_NO,A.ID_NO,A.SBAC_CHK,A.SAL_NO,A.SAL_NO1,A.AMTN_EP,A.QTY_RTN_UNSH,A.QTY_SB_UNSH,A.AMTN_RSV "
                + ",C.TPL_NO,C.TPL_REM,A.DEP_RK,A.RK_DD " + _sqlOsHead + _sqlCaseTf
                + " from TF_PSS A"
                + " left join MY_WH B on B.WH=A.WH"
                + " left join PRDT C on C.PRD_NO=A.PRD_NO"
                + _sqlOsJoin
                + " where A.PS_ID=@PsID and A.PS_NO=@PsNo order by A.ITM;"
                + "select PS_ID,PS_NO,PS_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO"
                + " from TF_PSS3"
                + " where PS_ID=@PsID and PS_NO=@PsNo;"
                + "select PRD_NO,CARD_ID,PSWD,VALID_DD,PS_NO,ITM from PRDT_VIR where PS_NO=@PsNo;"
				+ "select BIL_ID,BIL_NO,PAY_REM,PAY_NO,PAY_ID,PAY_DD,REM from PAY_B2C"
                + " where BIL_ID=@PsID and BIL_NO=@PsNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[0].Value = PsID;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[1].Value = PsNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            if (OnlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[] { "MF_PSS", "TF_PSS", "TF_PSS_BAR", "PRDT_VIR", "PAY_B2C" }, _spc);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[] { "MF_PSS", "TF_PSS", "TF_PSS_BAR", "PRDT_VIR", "PAY_B2C" }, _spc);
            }
            _ds.Tables["TF_PSS"].Columns["UNIT_NAME"].ReadOnly = false;
            //�趨PK����Ϊ����left join�Ժ�PK��ȡ����
            DataColumn[] _dca = new DataColumn[2];
            _dca[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _ds.Tables["MF_PSS"].PrimaryKey = _dca;
            _dca = new DataColumn[3];
            _dca[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca[2] = _ds.Tables["TF_PSS"].Columns["ITM"];
            _ds.Tables["TF_PSS"].PrimaryKey = _dca;
            //��ͷ�ͱ������
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _ds.Relations.Add("MF_PSSTF_PSS", _dca1, _dca2);
            //�����BAR_CODE����
            _dca1 = new DataColumn[3];
            _dca1[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca1[2] = _ds.Tables["TF_PSS"].Columns["PRE_ITM"];
            _dca2 = new DataColumn[3];
            _dca2[0] = _ds.Tables["TF_PSS_BAR"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS_BAR"].Columns["PS_NO"];
            _dca2[2] = _ds.Tables["TF_PSS_BAR"].Columns["PS_ITM"];
            _ds.Relations.Add("TF_PSSTF_PSS_BAR", _dca1, _dca2);
            //�����������Ʒ����
            _dca1 = new DataColumn[3];
            _dca1[0] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca1[1] = _ds.Tables["TF_PSS"].Columns["ITM"];
            _dca1[2] = _ds.Tables["TF_PSS"].Columns["PRD_NO"];
            _dca2 = new DataColumn[3];
            _dca2[0] = _ds.Tables["PRDT_VIR"].Columns["PS_NO"];
            _dca2[1] = _ds.Tables["PRDT_VIR"].Columns["ITM"];
            _dca2[2] = _ds.Tables["PRDT_VIR"].Columns["PRD_NO"];
            _ds.Relations.Add("TF_PSSPRDT_VIR", _dca1, _dca2);
            //��ͷ��B2C���߸����¼����
            _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["PAY_B2C"].Columns["BIL_ID"];
            _dca2[1] = _ds.Tables["PAY_B2C"].Columns["BIL_NO"];
            _ds.Relations.Add("MF_PSSPAY_B2C", _dca1, _dca2);
            return _ds;
        }
        /// <summary>
        /// ȡ������������
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string psId, string psNo, string preItm)
        {
            string _sqlStr = "";
            _sqlStr = " SELECT A.PS_ID,A.PS_NO,A.PS_DD,A.CUS_NO,B.NAME AS CUS_NAME,A.DEP,D.NAME AS DEP_NAME,A.SAL_NO,C.NAME AS SAL_NAME "
                    + " FROM MF_PSS A"
                    + " LEFT JOIN CUST B on B.CUS_NO=A.CUS_NO"
                    + " LEFT JOIN SALM C on C.SAL_NO=A.SAL_NO"
                    + " LEFT JOIN DEPT D on D.DEP=A.DEP "
                    + " WHERE A.PS_ID=@PS_ID AND A.PS_NO = @PS_NO;"
                    + " SELECT  A.PS_ID,A.PS_NO,A.ITM,A.PS_DD,A.WH,A.ME_FLAG,B.NAME as WH_NAME,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.PRE_ITM,A.UNIT,A.CHK_RTN,C.SPC,A.QTY_SB,A.SH_NO_CUS "
                    + " FROM TF_PSS A "
                    + " LEFT JOIN MY_WH B on B.WH=A.WH "
                    + " LEFT JOIN PRDT C ON C.PRD_NO=A.PRD_NO "
                    + " WHERE A.PS_ID=@PS_ID AND A.PS_NO = @PS_NO AND A.PRE_ITM =@PRE_ITM";
            System.Data.SqlClient.SqlParameter[] _sqlPara = new System.Data.SqlClient.SqlParameter[3];
            _sqlPara[0] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.Char, 2);
            _sqlPara[0].Value = psId;
            _sqlPara[1] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = psNo;
            _sqlPara[2] = new System.Data.SqlClient.SqlParameter("@PRE_ITM", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = preItm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "MF_PSS", "TF_PSS" }, _sqlPara);
            //�趨PK����Ϊ����left join�Ժ�PK��ȡ����
            DataColumn[] _dca = new DataColumn[2];
            _dca[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            _ds.Tables["MF_PSS"].PrimaryKey = _dca;
            _dca = new DataColumn[3];
            _dca[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _dca[2] = _ds.Tables["TF_PSS"].Columns["ITM"];
            _ds.Tables["TF_PSS"].PrimaryKey = _dca;
            //��ͷ�ͱ������
            DataColumn[] _dca1 = new DataColumn[2];
            _dca1[0] = _ds.Tables["MF_PSS"].Columns["PS_ID"];
            _dca1[1] = _ds.Tables["MF_PSS"].Columns["PS_NO"];
            DataColumn[] _dca2 = new DataColumn[2];
            _dca2[0] = _ds.Tables["TF_PSS"].Columns["PS_ID"];
            _dca2[1] = _ds.Tables["TF_PSS"].Columns["PS_NO"];
            _ds.Relations.Add("MF_PSSTF_PSS", _dca1, _dca2);
            return _ds;
        }

        /// <summary>
        /// ���µ��ݵ����˵���
        /// </summary>
        /// <param name="PsID">���ݴ���</param>
        /// <param name="PsNo">���ݺ���</param>
        /// <param name="ArpNo">���˵���</param>
        public void UpdateArpNo(string PsID, string PsNo, string ArpNo)
        {
            string _sql = "update MF_PSS set ARP_NO=@ArpNo where PS_ID=@PsID and PS_NO=@PsNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ArpNo", SqlDbType.VarChar, 20);
            _spc[0].Value = ArpNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[1].Value = PsID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[2].Value = PsNo;
            this.ExecuteNonQuery(_sql, _spc);
        }
        /// <summary>
        /// ���µ��ݵ��տ��
        /// </summary>
        /// <param name="PsID">���ݴ���</param>
        /// <param name="PsNo">���ݺ���</param>
        /// <param name="rpNo">�տ��</param>
        public void UpdateRpNo(string psId, string psNo, string rpNo)
        {
            string _sql = "update MF_PSS set RP_NO=@RP_NO where PS_ID=@PsID and PS_NO=@PsNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@RP_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = rpNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[1].Value = psId;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[2].Value = psNo;
            this.ExecuteNonQuery(_sql, _spc);
        }
        /// <summary>
        /// �޸�����˺��������
        /// </summary>
        /// <param name="PsID">���ݴ���</param>
        /// <param name="PsNo">���ݺ���</param>
        /// <param name="ChkMan">�����</param>
        /// <param name="ClsDate">�������</param>
        public void UpdateChkMan(string PsID, string PsNo, string ChkMan, DateTime ClsDate)
        {
            string _sql = "update MF_PSS set CHK_MAN=@ChkMan,CLS_DATE=@ClsDate where PS_ID=@PsID and PS_NO=@PsNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ChkMan", SqlDbType.VarChar, 12);
            if (String.IsNullOrEmpty(ChkMan))
            {
                _spc[0].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = ChkMan;
            }
            _spc[1] = new System.Data.SqlClient.SqlParameter("@ClsDate", SqlDbType.DateTime);
            if (String.IsNullOrEmpty(ChkMan))
            {
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[1].Value = ClsDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[2].Value = PsID;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[3].Value = PsNo;
            this.ExecuteNonQuery(_sql, _spc);
        }

        /// <summary>
        /// �޸����������������޸ĵļ�¼��
        /// </summary>
        /// <param name="PsID">���ݴ���</param>
        /// <param name="PsNo">���ݺ���</param>
        /// <param name="PreItm">׷���˻��������</param>
        /// <param name="Qty">�˻�����</param>
        public string UpdateQtyRtn(string PsID, string PsNo, int PreItm, decimal Qty)
        {
            string _sql = "		update TF_PSS set QTY_RTN=isNull(QTY_RTN,0)+@QTY where PS_ID=@PsID and PS_NO=@PsNo and PRE_ITM=@PreItm \n"
                + "		if Exists(select PS_NO from TF_PSS where PS_ID=@PsID and PS_NO=@PsNo and (isnull(QTY,0) > isnull(QTY_RTN,0))) \n"
                + "			update MF_PSS set CLS_ID='F' where PS_ID=@PsID and PS_NO=@PsNo \n"
                + "		else \n"
                + "			update MF_PSS set CLS_ID='T' where PS_ID=@PsID and PS_NO=@PsNo \n"
                + "	select 0\n";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@Qty", SqlDbType.Decimal);
            _spc[0].Precision = 38;
            _spc[0].Scale = 10;
            _spc[0].Value = Qty;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[1].Value = PsID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[2].Value = PsNo;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@PreItm", SqlDbType.SmallInt);
            _spc[3].Value = PreItm;
            string _result = "2";
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _spc))
            //{
            //    if (_sdr.Read())
            //    {
            //        _result = _sdr[0].ToString();
            //    }
            //}
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }

        /// <summary>
        /// �޸����������������޸ĵļ�¼��
        /// </summary>
        /// <param name="PsID">���ݴ���</param>
        /// <param name="PsNo">���ݺ���</param>
        /// <param name="PreItm">׷���˻��������</param>
        /// <param name="Qty">�˻�����</param>
        public string UpdateQtySb(string PsID, string PsNo, int PreItm, decimal Qty)
        {
            string _sql = "		update TF_PSS set QTY_SB=isNull(QTY_SB,0)+@QTY where PS_ID=@PsID and PS_NO=@PsNo and PRE_ITM=@PreItm \n"
                + "		if Exists(select PS_NO from TF_PSS where PS_ID=@PsID and PS_NO=@PsNo and (ABS(isnull(QTY,0)) > isnull(QTY_SB,0))) \n"
                + "			update MF_PSS set CLS_ID='F' where PS_ID=@PsID and PS_NO=@PsNo \n"
                + "		else \n"
                + "			update MF_PSS set CLS_ID='T' where PS_ID=@PsID and PS_NO=@PsNo \n"
                + "	select 0\n";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@Qty", SqlDbType.Decimal);
            _spc[0].Precision = 38;
            _spc[0].Scale = 10;
            _spc[0].Value = Qty;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[1].Value = PsID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[2].Value = PsNo;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@PreItm", SqlDbType.SmallInt);
            _spc[3].Value = PreItm;
            string _result = "2";
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _spc))
            //{
            //    if (_sdr.Read())
            //    {
            //        _result = _sdr[0].ToString();
            //    }
            //}
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }

        /// <summary>
        /// �޸ļ���Ӫ������״̬
        /// </summary>
        /// <param name="PsID">���ݴ���</param>
        /// <param name="PsNo">���ݺ���</param>
        /// <param name="PreItm">׷���˻��������</param>
        /// <param name="flag">Ӫ���������״̬</param>
        public string UpdateMeRtn(string PsID, string PsNo, int PreItm, bool flag)
        {
            string _sql = "";
            if (flag)
                _sql = " update TF_PSS SET ME_FLAG='T' where PS_ID=@PsID and PS_NO=@PsNo and PRE_ITM=@PreItm ";
            else
                _sql = " update TF_PSS SET ME_FLAG = NULL where PS_ID=@PsID and PS_NO=@PsNo and PRE_ITM=@PreItm ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[0].Value = PsID;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[1].Value = PsNo;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PreItm", SqlDbType.SmallInt);
            _spc[2].Value = PreItm;
            string _result = "2";
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }

        /// <summary>
        /// ȡ��������
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public DataSet GetBodyData(string psId, string psNo, int preItm)
        {
            string _sql = "SELECT * FROM TF_PSS WHERE PS_ID = @PS_ID AND PS_NO = @PS_NO AND PRE_ITM = @PRE_ITM";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = psId;
            _aryPt[1] = new SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = psNo;
            _aryPt[2] = new SqlParameter("@PRE_ITM", SqlDbType.SmallInt);
            _aryPt[2].Value = preItm;
            DataSet _ds = new DataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_PSS" }, _aryPt);
            return _ds;
        }

        /// <summary>
        /// �ж��Ƿ���Ҫ�����
        /// </summary>
        /// <param name="_dt"></param>
        /// <param name="psNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="itm"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public decimal ChkQty(DataTable _dt, string psNo, string prdNo, string prdMark, string itm, decimal qty)
        {
            string _sql = "SELECT SUM(A.QTY*CASE WHEN A.UNIT='2' THEN B.PK2_QTY WHEN A.UNIT='3' THEN B.PK3_QTY ELSE 1 END) AS QTY FROM TF_PSS A INNER JOIN PRDT B ON A.PRD_NO=B.PRD_NO "
                + "WHERE A.PS_ID='SA' AND A.PS_NO = @psNo AND A.PRD_NO = @prdNo AND A.PRD_MARK = @prdMark";
            SqlParameter[] _sp = new SqlParameter[3];
            _sp[0] = new SqlParameter("@psNo", SqlDbType.VarChar, 12);
            _sp[0].Value = psNo;
            _sp[1] = new SqlParameter("@prdNo", SqlDbType.VarChar, 30);
            _sp[1].Value = prdNo;
            _sp[2] = new SqlParameter("@prdMark", SqlDbType.VarChar, 50);
            _sp[2].Value = prdMark;

            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _sp))
            {
                decimal _othQty = 0;//ԭ����ȥ�ñ���������
                decimal _orderQty = 0;//ԭ������
                object _rObj = this.ExecuteScalar(_sql, _sp);
                if (!String.IsNullOrEmpty(_rObj.ToString()))
                {
                    _orderQty = Convert.ToDecimal(_rObj);
                }
                if (string.IsNullOrEmpty(itm.ToString()))//add
                {
                    DataRow[] _drArr = _dt.Select("PS_NO='" + psNo + "' AND PRD_NO ='" + prdNo + "' AND PRD_MARK = '" + prdMark + "'");
                    foreach (DataRow dr in _drArr)
                    {
                        if (dr["UNIT"].ToString() == "2" || dr["UNIT"].ToString() == "3")
                        {
                            _sql = "SELECT ISNULL(PK" + dr["UNIT"].ToString() + "_QTY,0) FROM PRDT WHERE PRD_NO='" + prdNo + "'";
                            _rObj = this.ExecuteScalar(_sql);

                            if (_rObj != null && !string.IsNullOrEmpty(_rObj.ToString()))
                            {
                                _othQty += Convert.ToDecimal(dr["QTY"]) * Convert.ToDecimal(_rObj);
                            }
                        }
                        else
                        {
                            _othQty += Convert.ToDecimal(dr["QTY"]);
                        }
                    }
                    return _othQty + qty - _orderQty;
                }
                else//update
                {
                    return qty - _orderQty;
                }
            }
        }
        #region ȥ����Ʊ��¼
        /// <summary>
        /// ȥ����Ʊ��¼
        /// </summary>
        /// <param name="bilId">�������</param>
        /// <param name="bilNo">���ݺ���</param>
        /// <returns></returns>
        public SunlikeDataSet GetInvBill(string bilId, string bilNo)
        {
            string _sql = "SELECT * FROM MF_BIL WHERE BIL_ID = @BIL_ID AND BIL_NO = @BIL_NO;"
                + "SELECT * FROM TF_BIL WHERE BIL_ID = @BIL_ID AND BIL_NO = @BIL_NO";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = bilId;
            _aryPt[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = bilNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, null, _aryPt);
            return _ds;
        }
        #endregion

        #region ��Ʊ
        /// <summary>
        /// ����������ѯ��ͷ(��Ʊ)
        /// </summary>
        /// <param name="sqlWhere">����</param>
        /// <returns></returns>
        public SunlikeDataSet GetData4TaxAa(string sqlWhere)
        {
            string _sql = "SELECT M.*, D.TAX_ID FROM "
                + " (SELECT MF.*, SUM(TF.AMT) AS AMT, SUM(TF.TAX) AS TAX FROM"
                + " (SELECT PS_ID, PS_NO, PS_DD, CUS_NO, SAL_NO, DEP, BAT_NO FROM MF_PSS M WHERE PS_ID = 'SA' AND ISNULL(KP_ID, 'F') <> 'T' AND ISNULL(INV_NO, '') = '' " + sqlWhere + ") AS MF"
                + " JOIN"
                + " TF_PSS AS TF"
                + " ON MF.PS_ID = TF.PS_ID AND MF.PS_NO = TF.PS_NO"
                + " GROUP BY MF.PS_ID, MF.PS_NO, MF.PS_DD, MF.CUS_NO, MF.SAL_NO, MF.DEP, MF.BAT_NO) M"
                + " JOIN"
                + " MF_PSS D"
                + " ON M.PS_ID = D.PS_ID AND M.PS_NO = D.PS_NO;"
                + " SELECT T.PS_ID, T.PS_NO, T.PRD_NO, T.PRD_NAME, T.PRD_MARK, T.WH, T.UNIT, T.QTY, T.UP, T.AMT, T.AMTN_NET, T.TAX, T.DIS_CNT, T.REM,T.MTN_REM FROM"
                + " MF_PSS M"
                + " JOIN"
                + " TF_PSS T"
                + " ON M.PS_ID = T.PS_ID AND M.PS_NO = T.PS_NO"
                + " WHERE M.PS_ID = 'SA' AND ISNULL(INV_NO, '') = '' " + sqlWhere;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[2] { "MF_PSS", "TF_PSS" });
            return _ds;
        }
        /// <summary>
        /// ����MF_PSS��Ʊ����
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="invNo"></param>
        /// <param name="kpId"></param>
        public void UpdateInvNo(string psId, string psNo, string invNo, string kpId)
        {
            string _sql = "UPDATE MF_PSS SET INV_NO = @INV_NO, KP_ID = @KP_ID WHERE PS_ID = @PS_ID AND PS_NO = @PS_NO";
            SqlParameter[] _aryPt = new SqlParameter[4];
            _aryPt[0] = new SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _aryPt[0].Value = invNo;
            _aryPt[1] = new SqlParameter("@KP_ID", SqlDbType.VarChar, 1);
            _aryPt[1].Value = kpId;
            _aryPt[2] = new SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _aryPt[2].Value = psId;
            _aryPt[3] = new SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _aryPt[3].Value = psNo;
            this.ExecuteNonQuery(_sql, _aryPt);
        }
        #endregion

        #region ��������
        /// <summary>
        /// ȡ����ת�������õ�ԭ�������б�
        /// </summary>
        /// <param name="osNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetImportedSAList(string osNo)
        {
            string _sqlWhere = osNo.Replace(",", "','");
            string _sql = "SELECT PS_NO,ITM,PRE_ITM FROM TF_PSS "
                + " WHERE PRE_ITM IN "
                + " (SELECT OTH_ITM FROM TF_PSS WHERE OS_NO IN ('" + _sqlWhere + "') AND PS_ID='SD' GROUP BY OTH_ITM) "
                + " AND PS_NO IN ('" + _sqlWhere + "') AND PS_ID='SA'";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "TF_PSS" });
            return _ds;
        }
        /// <summary>
        /// ȡ������������
        /// </summary>
        /// <param name="BilId">���ݱ����</param>
        /// <param name="pPsNo">��������</param>
        /// <returns></returns>
        public SunlikeDataSet GetSAList(string BilId, string pPsNo)
        {
            string _psNos = "'" + pPsNo.Replace(",", "','") + "'";
            string _sqlStr = "SELECT SA.PS_ID,SA.PS_NO, SA.ITM, SA.PS_DD, SA.WH, SA.PRD_NO, SA.PRD_NAME, SA.PRD_MARK, SA.UNIT,SA.PRE_ITM,SA.FREE_ID,SA.PAK_UNIT,SA.PAK_EXC,SA.PAK_NW,SA.PAK_WEIGHT_UNIT,SA.PAK_GW,SA.PAK_MEAST,SA.PAK_MEAST_UNIT,"
                + " ISNULL(SA.QTY,0)-ISNULL(SA.QTY_RTN,0)  AS QTY,SA.UP," //lzj  bugNo:89875
                + " ISNULL(SA.AMT,0) - ISNULL(SB.AMT,0) AS AMT,"
                + " ISNULL(SA.AMTN_SALE,0) AS AMTN_SALE,"
                + " ISNULL(SA.AMTN_NET,0) - ISNULL(SB.AMTN_NET,0) AS AMTN_NET,"
                + " ISNULL(SA.TAX,0) - ISNULL(SB.TAX,0) AS TAX,"
                + " ISNULL(SA.TAX_RTO, 0.0) AS TAX_RTO,SA.CUS_OS_NO FROM"
                + " (SELECT PS_ID,PS_NO,ITM,PS_DD,WH,PRD_NO,PRD_NAME,PRD_MARK,UNIT,QTY,QTY_RTN,UP,"
                + " AMT,AMTN_SALE,AMTN_NET,TAX,TAX_RTO,PRE_ITM,FREE_ID,PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,CUS_OS_NO FROM TF_PSS WHERE PS_ID='" + BilId + "' AND PS_NO IN (" + _psNos + ")) AS SA"
                + " LEFT JOIN TF_PSS SB"
                + " ON SB.OS_ID = SA.PS_ID AND SB.OS_NO = SA.PS_NO AND SB.OTH_ITM = SA.PRE_ITM";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, null);
            return _ds;
        }

        /// <summary>
        /// ȡ������������
        /// </summary>
        /// <param name="dBdate">��ʼ����</param>
        /// <param name="dEdate">��������</param>
        /// <param name="cusNo">�ͻ�����</param>
        /// <returns></returns>
        public SunlikeDataSet GetData4SD(DateTime dBdate, DateTime dEdate, string cusNo)
        {
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@BDATE", SqlDbType.DateTime);
            _aryPt[0].Value = dBdate;
            _aryPt[1] = new SqlParameter("@EDATE", SqlDbType.DateTime);
            _aryPt[1].Value = dEdate;
            _aryPt[2] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 20);
            _aryPt[2].Value = cusNo;

            string _sql = "SELECT PS.*, CT.NAME AS CUS_NAME, SM.NAME AS SAL_NAME FROM"
                + " (SELECT PS_ID, PS_NO, PS_DD, DEP, CUS_NO, SAL_NO, REM "
                + " FROM MF_PSS"
                + " WHERE (PS_ID = 'SA' OR PS_ID='SB') AND ISNULL(CHK_MAN, '') <> ''"
                + " AND PS_DD >= @BDATE"
                + " AND PS_DD <= @EDATE"
                + " AND ISNULL(CLS_ID,'F')<>'T' ";
            if (!String.IsNullOrEmpty(cusNo))
            {
                _sql += " AND CUS_NO = @CUS_NO";
            }
            _sql += " ) AS PS"
                + " JOIN"
                + " (SELECT CUS_NO, NAME FROM CUST) AS CT"
                + " ON PS.CUS_NO = CT.CUS_NO"
                + " JOIN"
                + " (SELECT SAL_NO, NAME FROM SALM) AS SM"
                + " ON PS.SAL_NO = SM.SAL_NO";
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[1] { "MF_PSS" }, _aryPt);
            return _ds;
        }
        /// <summary>
        /// ȡ��������
        /// </summary>
        /// <param name="psNo"></param>
        /// <param name="Incorporate">�Ƿ�ϲ�</param>
        /// <returns></returns>
        public SunlikeDataSet GetBodydata4SD(string psNo, bool Incorporate)
        {
            string _sql = "";
            if (Incorporate)
            {
                _sql = "SELECT PS.*, PT.NAME AS PRD_NAME FROM"
                    + " (SELECT PS_ID,PRD_NO, UP, UNIT,WH ,ISNULL(DIS_CNT,0) AS DIS_CNT_SA, SUM(QTY) AS QTY, SUM(AMTN_NET) AS AMTN_BF"
                    + " FROM TF_PSS WHERE (PS_ID='SA' OR PS_ID='SB') AND PS_NO IN (" + psNo + ")"
                    + " GROUP BY PS_ID,PRD_NO, UP, UNIT,WH, DIS_CNT) AS PS"
                    + " JOIN"
                    + " (SELECT PRD_NO, NAME FROM PRDT) AS PT"
                    + " ON PS.PRD_NO = PT.PRD_NO ORDER BY PS.PRD_NO, PS.AMTN_BF";
            }
            else
            {
                _sql = "SELECT PS.*, PT.NAME AS PRD_NAME FROM"
                    + " (SELECT PS_ID,PS_NO, PRD_NO, PRD_MARK, UP, UNIT,WH, ISNULL(DIS_CNT,0) AS DIS_CNT_SA, QTY,AMTN_NET AS AMTN_BF,PRE_ITM,FREE_ID"
                    + " FROM TF_PSS WHERE (PS_ID='SA' OR PS_ID='SB') AND PS_NO IN (" + psNo + ") "
                    + " ) AS PS"
                    + " JOIN"
                    + " (SELECT PRD_NO, NAME FROM PRDT) AS PT"
                    + " ON PS.PRD_NO = PT.PRD_NO ORDER BY PS.PRD_NO, PS.AMTN_BF, PS.PS_NO";

            }
            SunlikeDataSet _ds = new SunlikeDataSet();
            _ds.EnforceConstraints = false;
            this.FillDataset(_sql, _ds, new string[] { "TF_PSS" });
            return _ds;
        }
        #endregion

        #region �ֶ��������˽᰸
        /// <summary>
        /// �ֶ��������˽᰸
        /// </summary>
        /// <param name="psId">���ݱ�</param>
        /// <param name="psNo">����</param>
        /// <param name="clsName">�᰸�ֶ�</param>
        /// <param name="clsId">�᰸��־:trueΪ�᰸��falseΪ���᰸</param>
        /// <returns>������Ϣ</returns>
        public string CloseBill(string psId, string psNo, string clsName, bool clsId)
        {
            string _sql = "Update MF_PSS set " + clsName + "=@ClsID where PS_ID=@PsId and PS_NO=@PsNo";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[0].Value = psNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@ClsID", SqlDbType.VarChar, 1);
            _spc[1].Value = (clsId ? "T" : "F");
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsId", SqlDbType.VarChar, 2);
            _spc[2].Value = psId;
            string _result = "";
            if (this.ExecuteNonQuery(_sql, _spc) == 0)
            {
                if (!clsId)
                    _result = "RCID=COMMON.HINT.CLS_ERROR";	//�ֶ����᰸������{0}ʧ�ܣ���Ϊ�õ��Ǳ�ϵͳ�Զ��᰸�ģ��޷����᰸��
            }
            return _result;
        }
        #endregion

        #region �޸��ܶ��᰸״̬
        /// <summary>
        /// �޸��ܶ��᰸״̬
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="hasFx"></param>
        /// <returns></returns>
        public bool UpdateHasFx(string psId, string psNo, bool hasFx)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_PSS SET HAS_FX=@HAS_FX WHERE PS_ID=@PS_ID AND PS_NO=@PS_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = psId;

            _sqlPara[1] = new SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = psNo;

            _sqlPara[2] = new SqlParameter("@HAS_FX", SqlDbType.VarChar, 1);
            _sqlPara[2].Value = hasFx.ToString().Substring(0, 1);
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

        #region ����������ƾ֤����
        /// <summary>
        /// ����������ƾ֤����
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string psId, string psNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_PSS SET VOH_NO=@VOH_NO WHERE PS_ID=@PS_ID AND PS_NO=@PS_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = psId;

            _sqlPara[1] = new SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = psNo;

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

        #region �Ƿ���ת���õ�
        /// <summary>
        /// �Ƿ���ת���õ�
        /// </summary>
        /// <param name="osNo"></param>
        /// <returns></returns>
        public bool CheckSD(string osNo)
        {
            string _sql = "select count(1) from tf_pss where ps_id='SD' and os_no=@os_no";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            _sqlPara[0] = new SqlParameter("@os_no", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = osNo;

            return ExecuteScalar(_sql, _sqlPara).ToString() != "0";
        }
        #endregion

        #region ���¹���
        /// <summary>
        /// ���¹���
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="bilNo"></param>
        /// <param name="bilId"></param>
        /// <param name="isAdd"></param>
        public void UpdatePOSGZ(string dep, string bilNo, string bilId, bool isAdd)
        {
            string _sql = "";
            if (isAdd)
            {
                _sql = "IF NOT EXISTS(SELECT 1 FROM WEBPOS_GZ WHERE DEP=@DEP AND BIL_ID=@BIL_ID AND BIL_NO=@BIL_NO )\n"
                    + "INSERT INTO WEBPOS_GZ(DEP,BIL_ID,BIL_NO,SYS_DATE) VALUES (@DEP,@BIL_ID,@BIL_NO,GETDATE())";
            }
            else
            {
                _sql = "IF EXISTS(SELECT 1 FROM WEBPOS_GZ WHERE DEP=@DEP AND BIL_ID=@BIL_ID AND BIL_NO=@BIL_NO )\n"
                    + "DELETE FROM WEBPOS_GZ WHERE DEP=@DEP AND BIL_ID=@BIL_ID AND BIL_NO=@BIL_NO";
            }
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@DEP", SqlDbType.VarChar, 8);
            _sqlPara[0].Value = dep;
            _sqlPara[1] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
            _sqlPara[1].Value = bilId;
            _sqlPara[2] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = bilNo;
            ExecuteNonQuery(_sql, _sqlPara);
        }
        #endregion

        public bool IsExistPOSZG(string dep, string bilNo, string bilId)
        {
            string _sql = "SELECT 1 FROM WEBPOS_GZ WHERE DEP=@DEP AND BIL_ID=@BIL_ID AND BIL_NO=@BIL_NO ";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@DEP", SqlDbType.VarChar, 8);
            _sqlPara[0].Value = dep;
            _sqlPara[1] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
            _sqlPara[1].Value = bilId;
            _sqlPara[2] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = bilNo;
            if (ExecuteScalar(_sql, _sqlPara) == null)
                return false;
            return true;
        }


        #region INVIK & INVLI  ��д��

        /// <summary>
        /// ������Ʊ����ԴΪCK ,ת��������ƱҪ��MF_PSS ACC_FP_NO
        /// </summary>
        /// <param name="LzNo">������Ʊ����</param>
        /// <param name="invNo">��Ʊ��</param>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        public void UpdateMFPSSACC_FP_NO(string LzNo, string psId, string psNo)
        {
            string _sql = "update MF_PSS set ACC_FP_NO=@ACC_FP_NO Where PS_ID=@PS_ID and  PS_NO=@PS_NO  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ACC_FP_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = LzNo;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[1].Value = psId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = psNo;

            this.ExecuteNonQuery(_sql, _spc);
        }

        /// <summary>
        /// ������Ʊ����ԴΪCK ,ת��������ƱҪ��MF_PSS ACC_FP_NO
        /// </summary>
        /// <param name="LzNo">������Ʊ����</param>
        /// <param name="invNo">��Ʊ��</param>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        public void UpdateMFPSSACC_FP_NO(string LzNo,string invNo, string psId, string psNo)
        {
            string _sql = "update MF_PSS set ACC_FP_NO=@ACC_FP_NO,INV_NO=@INV_NO Where PS_ID=@PS_ID and  PS_NO=@PS_NO  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ACC_FP_NO", SqlDbType.VarChar,20);
            _spc[0].Value = LzNo;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[1].Value = invNo;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar,2);
            _spc[2].Value = psId;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[3].Value = psNo;

            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        /// ������Ʊ����ԴΪCK ,ת��������ƱҪ��MF_PSS INV_NO
        /// </summary>
        /// <param name="invNo">��Ʊ��</param>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        public void UpdateMFPSSINV_NO(string invNo, string psId, string psNo)
        {
            string _sql = "update MF_PSS set INV_NO=@INV_NO Where PS_ID=@PS_ID and  PS_NO=@PS_NO  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[0].Value = invNo;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[1].Value = psId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = psNo;

            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        /// ���շ�Ʊ��д������ͷ��
        /// </summary>
        /// <param name="turnId">���Ʊʶ�� 1.��ϸ 2.����</param>
        /// <param name="lzclsId">��Ʊ�᰸ע��</param>
        /// <param name="clsLz">��д�᰸ע��</param>
        /// <param name="accFpNo">��������</param>
        /// <param name="invNo">��Ʊ����</param>
        /// <param name="amtCls">�ѿ����</param>
        /// <param name="amtn_netCls">�ѿ�δ˰���</param>
        /// <param name="taxCls">�ѿ�˰��</param> 
        /// <param name="qtyCls">�ѿ�����</param> 
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        public void UpdateInvIkHeadData(string turnId, string lzclsId, string clsLz, string accFpNo, string invNo, decimal amtCls, decimal amtn_netCls,decimal taxCls,decimal qtyCls, string psId, string psNo)
        {
            string _sql = "update MF_PSS set TURN_ID=@TURN_ID,LZ_CLS_ID=@LZ_CLS_ID,CLSLZ=@CLSLZ,ACC_FP_NO=@ACC_FP_NO,INV_NO=@INV_NO,AMT_CLS=@AMT_CLS,AMTN_NET_CLS=@AMTN_NET_CLS,TAX_CLS=@TAX_CLS,QTY_CLS=@QTY_CLS " +
                           " Where PS_ID=@PS_ID and PS_NO=@PS_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[11];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TURN_ID", SqlDbType.VarChar, 1);
            _spc[0].Value = turnId;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = lzclsId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@CLSLZ", SqlDbType.VarChar, 1);
            _spc[2].Value = clsLz;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@ACC_FP_NO", SqlDbType.VarChar, 20);
            _spc[3].Value = accFpNo;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[4].Value = invNo;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@AMT_CLS", SqlDbType.Decimal, 0);
            _spc[5].Value = amtCls;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_CLS", SqlDbType.Decimal, 0);
            _spc[6].Value = amtn_netCls;

            _spc[7] = new System.Data.SqlClient.SqlParameter("@TAX_CLS", SqlDbType.Decimal, 0);
           _spc[7].Value = taxCls;

           _spc[8] = new System.Data.SqlClient.SqlParameter("@QTY_CLS", SqlDbType.Decimal, 0);
           _spc[8].Value = qtyCls;

            _spc[9] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[9].Value = psId;
            _spc[10] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[10].Value = psNo;
            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        ///������Ʊ��д��Դ��������λ
        /// </summary>
        /// <param name="amtFp">�ѿ����</param>
        /// <param name="amtn_netFp">�ѿ�δ˰���</param>
        /// <param name="taxFp">�ѿ�˰��</param>
        /// <param name="qtyFp">�ѿ�����</param>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="itm">�������</param>
        public void UpdateInvIkBodyData(decimal amtFp, decimal amtn_netFp,decimal taxFp,decimal qtyFp,string psId,string psNo,int itm)
        {
            string _sql = "update TF_PSS set AMT_FP=@AMT_FP,AMTN_NET_FP=@AMTN_NET_FP,TAX_FP=@TAX_FP,QTY_FP=@QTY_FP Where PS_ID=@PS_ID and  PS_NO=@PS_NO and ITM=@ITM  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[7];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_FP", SqlDbType.Decimal);
            _spc[0].Value = amtFp;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP", SqlDbType.Decimal);
            _spc[1].Value = amtn_netFp;
            
            _spc[2] = new System.Data.SqlClient.SqlParameter("@TAX_FP", SqlDbType.Decimal);
            _spc[2].Value = taxFp;
            
            _spc[3] = new System.Data.SqlClient.SqlParameter("@QTY_FP", SqlDbType.Decimal);
            _spc[3].Value = qtyFp;


            _spc[4] = new System.Data.SqlClient.SqlParameter("@PS_NO", SqlDbType.VarChar, 20);
            _spc[4].Value = psNo;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@ITM", SqlDbType.Int, 1);
            _spc[5].Value =itm;


            _spc[6] = new System.Data.SqlClient.SqlParameter("@PS_ID", SqlDbType.VarChar, 2);
            _spc[6].Value = psId;
            this.ExecuteNonQuery(_sql, _spc);

        }
        #endregion
    }
}
