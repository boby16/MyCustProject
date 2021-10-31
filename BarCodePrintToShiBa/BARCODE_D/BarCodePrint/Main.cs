using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
	public partial class Main : Form
    {
        #region resources
        private BCPrint _bcp = null;
		private RepPrint _bcRep = null;
        private DFPrint _dfp = null;
        private DFSpcPrint _dfSpcP = null;
        private FTPrint _ftp = null;
        private FTSpcPrint _ftSpcP = null;
        private FTRepPrint _ftRep = null;
        private WXPrint _wxp = null;
        private WXRepPrint _wxRep = null;
        private BarPrintSet _bpSet = null;
        private BCQC _bcQC = null;
        private FTQC _bcFTQC = null;
        private BarBoxUP _bbup = null;
        private BarUndoBox _buBox = null;
        private WXCodeEdit _wxEdit = null;
        private UserRightMng _urMng = null;
        private BCSBPrint _bcpSB = null;
        private FTSBPrint _ftpSB = null;
        private SBUndoBox _sbUndoBox = null;
        private RPTSBBarList _rptSABarLst = null;
        private FTInitPrint _ftInitPrt = null;
        private FTInitBarBoxUP _ftInitBox = null;
        private RPTSABarQCLst _rptSABarQCLst = null;
        private BarCodeDel _barDel = null;
        private FTWHBarBoxUP _whBoxUP = null;
        private FTWHUndoBox _whUndoBox = null;
        private BCToMM _bcToMM = null;
        private BCMM _bcMM = null;
        private DFToMM _dfToMM = null;
        private DFMM _dfMM = null;
        private FTToMM _ftToMM = null;
        private FTMM _ftMM = null;
        private BCInitMM _bcInitMm = null;
        private DFQC _dfQC = null;
        private BarUndoToMM _undoTomm = null;
        private RptSAStat _rptSaStat = null;
        private BCEditPrint _bcEidt = null;
        private FTEditPrint _ftEdit = null;
        private DFEditPrint _dfEdit = null;
        private FTInitAutoBox _ftInitAutoBox = null;
        private RPTBoxBarLst _rptBoxBarLst = null;
        private BCInitPrint _bcInitPrint = null;
        private DFInitPrint _dfInitPrint = null;
        private BCChgPrint _bcChgPrint = null;
        private DFChgPrint _dfChgPrint = null;
        private FTChgPrint _ftChgPrint = null;
        private SAChgBarCode _saChgBar = null;
        private BarToTR _barToTR = null;
        private RPTPrdtHisList _rptPrdtHis = null;
        private RPTPrdtStat _rptPrdtStat = null;
        private RPTBarQCSta _rptBarQCSta = null;
        private RPTBarQCList _rptBarQCLst = null;
        private SASelBarCode _saBarQC = null;
        private BarPrtNameSet _barIdxSet = null;
        private BarUndoMM _undoMM = null;
        private BarUndoTR _undoTR = null;
        #endregion

        /*-------------------------------------------------------
          
         * ������к���ɣ�Ʒ�� + ���� + ��̨ +���ڼ��� + ��ˮ�� ex. A32003500400 + 086 + 1 + 001 + 001
          
         * �Էֺ�ľ�����к���ɣ�Ʒ�� + ���ţ��������+��ˮ�ţ�+ �Էֱ�� + ��ˮ�� ex. A32003500400 + 0861001 + 1 + 1
       
         * ���ֱ�ӷ����������к���ɣ�Ʒ�� + ���ţ��������+��ˮ�ţ�+ ��ˮ�� ex. A32003500400 + 0861001 + 001

         * �Էֺ�ľ�ķ����������к���ɣ�Ʒ�� + ���ţ��Էֺ�ľ������+ �Էֱ�� + ��ˮ�ţ�+ ��ˮ�� ex. A32003500400 + 0861001 + 1 + 1 + 01
         
        -------------------------------------------------------*/

		public Main()
		{
			InitializeComponent();
        }

        #region page_load
        private void Main_Load(object sender, EventArgs e)
        {
            //paintX = lblNotice.Width;
            this.lblLogInfo.Text = "����:" + BarRole.COMP_NO + "  �û�����:" + BarRole.USR_NO + "  ����:" + BarRole.DEP;
            lblTimeInfo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            timer1.Start();

            #region Ȩ��
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            DataSet _ds = _bps.GetUserRightAll(BarRole.USR_NO);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                if (_ds.Tables[0].Rows[0]["COMP_BOSS"].ToString() != "T")
                {
                    //MenuBCPrint.Enabled = false;//��Ĵ�ӡ
                    //MenuDFPrint.Enabled = false;//���Ʒ��ӡ
                    //MenuDFSpcPrint.Enabled = false;//���Ʒ��ӡ(����)
                    //MenuFTPrint.Enabled = false;//��Ʒ��ӡ
                    //MenuFTSpcPrint.Enabled = false;//��Ʒ��ӡ(����)
                    //MenuWXPrint.Enabled = false;//�����ӡ
                    //MenuWXEdit.Enabled = false;//�����޸�

                    //MenuChgBCPrint.Enabled = false;//������������ӡ
                    //MenuChgDFPrint.Enabled = false;//�������Ʒ�����ӡ
                    //MenuChgFTPrint.Enabled = false;//������Ʒ�����ӡ

                    //MenuBarDel.Enabled = false;//����ɾ��
                    //MenuBCEditPrint.Enabled = false;//��������޸�
                    //MenuDFEditPrint.Enabled = false;//���Ʒ�����޸�
                    //MenuFTEditPrint.Enabled = false;//��Ʒ�����޸�


                    //MenuBcRep.Enabled = false; //����ظ���ӡ
                    //MenuDFRep.Enabled = false;//���Ʒ�ظ���ӡ
                    //MenuFTRep.Enabled = false;//��Ʒ�ظ���ӡ
                    //MenuWxRep.Enabled = false;//�����ظ���ӡ

                    //MenuBCQC.Enabled = false;//��ļ���
                    //MenuDFQC.Enabled = false;//���Ʒ����
                    //MenuFTQC.Enabled = false;//��Ʒ����
                    //MenuToTR.Enabled = false;//ת�쳣֪ͨ��
                    //MenuUndoTR.Enabled = false;//ת�쳣������ҵ

                    //MenuXCBoxUp.Enabled = false;//�ֳ�װ��
                    //MenuXCUnDoBox.Enabled = false;//�ֳ�����
                    //MenuWHBoxUp.Enabled = false;//�ֿ�װ��
                    //MenuWHUndoBox.Enabled = false;//�ֿ����

                    //MenuBCToMM.Enabled = false;//����ͽ�
                    //MenuBCMM.Enabled = false;//��Ľɿ�
                    //MenuDFToMM.Enabled = false;//���Ʒ�ͽ�
                    //MenuDFMM.Enabled = false;//���Ʒ�ɿ�
                    //MenuFTToMM.Enabled = false;//��Ʒ�ͽ�
                    //MenuFTMM.Enabled = false;//��Ʒ�ɿ�
                    //MenuUndoToMM.Enabled = false;//�ͽɳ�����ҵ
                    //MenuUndoMM.Enabled = false;//�ɿ⳷����ҵ

                    //MenuSABarQC.Enabled = false;//��������Ʒ��
                    //MenuSABarChg.Enabled = false;//���������滻

                    //MenuBCSB.Enabled = false;//����˻������ӡ
                    //MenuFTSB.Enabled = false;//��Ʒ�˻������ӡ
                    //MenuSBBarList.Enabled = false;//�˻������ѯ
                    //MenuSBUndoBox.Enabled = false;//�˻�����

                    //MenuInitBCPrint.Enabled = false;//�ڳ���������ӡ
                    //MenuInitDFPrint.Enabled = false;//�ڳ����Ʒ�����ӡ
                    //MenuInitFTPrint.Enabled = false;//�ڳ���Ʒ��ӡ
                    //MenuInitFTBox.Enabled = false;//�ڳ���Ʒװ��

                    //MenuRptBarQCList.Enabled = false;//������ϸ����
                    //MenuRptBarQCSta.Enabled = false;//����Ʒ���ܱ���
                    //MenuRptSABarQCList.Enabled = false;//��������ͳ�Ʊ�
                    //MenuRptPrdtHisList.Enabled = false;//��Ʒ��ʷ��ѯ
                    //MenuRptPrdtStat.Enabled = false;//��Ʒ����ͳ�Ʊ���
                    //MenuRptBoxBarLst.Enabled = false;//��������Ϣ��ѯ
                    //MenuRptSAStat.Enabled = false;//����ͳ�Ʊ�

                    //MenuInitBCMM.Enabled = false;//�ڳ���Ľɿ⣨ѡ�����룩
                    //MenuInitAutoFTBox.Enabled = false;//�ڳ���Ʒװ�䣨ѡ�����룩
                    //MenuRightMng.Enabled = false;//Ȩ���趨
                    //MenuBarIdxSet.Enabled = false;//�Ϻ�/��ӡƷ���趨

                    //foreach (DataRow dr in _ds.Tables[0].Rows)
                    //{
                    //    //��Ĵ�ӡ
                    //    if (dr["PGM"].ToString() == "BCPRT" && dr["INS"].ToString() == "Y" && !MenuBCPrint.Enabled)
                    //    {
                    //        MenuBCPrint.Enabled = true;
                    //    }
                    //    //���Ʒ��ӡ
                    //    if (dr["PGM"].ToString() == "DFPRT" && dr["INS"].ToString() == "Y" && !MenuDFPrint.Enabled)
                    //    {
                    //        this.MenuDFPrint.Enabled = true;
                    //    }
                    //    //���Ʒ��ӡ(����)
                    //    if (dr["PGM"].ToString() == "DFSPCPRT" && dr["INS"].ToString() == "Y" && !MenuDFSpcPrint.Enabled)
                    //    {
                    //        this.MenuDFSpcPrint.Enabled = true;
                    //    }
                    //    //��Ʒ��ӡ
                    //    if (dr["PGM"].ToString() == "FTPRT" && dr["INS"].ToString() == "Y" && !MenuFTPrint.Enabled)
                    //    {
                    //        this.MenuFTPrint.Enabled = true;
                    //    }
                    //    //��Ʒ��ӡ(����)
                    //    if (dr["PGM"].ToString() == "FTSPCPRT" && dr["INS"].ToString() == "Y" && !MenuFTSpcPrint.Enabled)
                    //    {
                    //        this.MenuFTSpcPrint.Enabled = true;
                    //    }
                    //    //�����ӡ
                    //    if (dr["PGM"].ToString() == "WXPRT" && dr["INS"].ToString() == "Y" && !MenuWXPrint.Enabled)
                    //    {
                    //        this.MenuWXPrint.Enabled = true;
                    //    }
                    //    //�����޸�
                    //    if (dr["PGM"].ToString() == "WXEDIT" && dr["INS"].ToString() == "Y" && !MenuWXEdit.Enabled)
                    //    {
                    //        this.MenuWXEdit.Enabled = true;
                    //    }
                    //    //------------------------------
                    //    //��Ļ�����ӡ
                    //    if (dr["PGM"].ToString() == "BCCHGPRT" && dr["INS"].ToString() == "Y" && !MenuChgBCPrint.Enabled)
                    //    {
                    //        MenuChgBCPrint.Enabled = true;
                    //    }
                    //    //���Ʒ������ӡ
                    //    if (dr["PGM"].ToString() == "DFCHGPRT" && dr["INS"].ToString() == "Y" && !MenuChgDFPrint.Enabled)
                    //    {
                    //        this.MenuChgDFPrint.Enabled = true;
                    //    }
                    //    //��Ʒ������ӡ
                    //    if (dr["PGM"].ToString() == "FTCHGPRT" && dr["INS"].ToString() == "Y" && !MenuChgFTPrint.Enabled)
                    //    {
                    //        this.MenuChgFTPrint.Enabled = true;
                    //    }
                    //    //-------------------------------
                    //    //����ɾ��
                    //    if (dr["PGM"].ToString() == "BARDEL" && dr["INS"].ToString() == "Y" && !MenuBarDel.Enabled)
                    //    {
                    //        this.MenuBarDel.Enabled = true;
                    //    }
                    //    //��������޸�
                    //    if (dr["PGM"].ToString() == "BCEDIT" && dr["INS"].ToString() == "Y" && !MenuBCEditPrint.Enabled)
                    //    {
                    //        this.MenuBCEditPrint.Enabled = true;
                    //    }
                    //    //���Ʒ�����޸�
                    //    if (dr["PGM"].ToString() == "DFEDIT" && dr["INS"].ToString() == "Y" && !MenuDFEditPrint.Enabled)
                    //    {
                    //        this.MenuDFEditPrint.Enabled = true;
                    //    }
                    //    //��Ʒ�����޸�
                    //    if (dr["PGM"].ToString() == "FTEDIT" && dr["INS"].ToString() == "Y" && !MenuFTEditPrint.Enabled)
                    //    {
                    //        this.MenuFTEditPrint.Enabled = true;
                    //    }
                    //    //------------------------
                    //    //����˻������ӡ
                    //    if (dr["PGM"].ToString() == "BCSB" && dr["INS"].ToString() == "Y" && !MenuBCSB.Enabled)
                    //    {
                    //        this.MenuBCSB.Enabled = true;
                    //    }
                    //    //��Ʒ�˻������ӡ
                    //    if (dr["PGM"].ToString() == "FTSB" && dr["INS"].ToString() == "Y" && !MenuFTSB.Enabled)
                    //    {
                    //        this.MenuFTSB.Enabled = true;
                    //    }
                    //    //�˻�����
                    //    if (dr["PGM"].ToString() == "SBUNDOBOX" && dr["INS"].ToString() == "Y" && !MenuSBUndoBox.Enabled)
                    //    {
                    //        this.MenuSBUndoBox.Enabled = true;
                    //    }
                    //    //�˻������ѯ
                    //    if (dr["PGM"].ToString() == "SBBARLIST" && dr["INS"].ToString() == "Y" && !MenuSBBarList.Enabled)
                    //    {
                    //        this.MenuSBBarList.Enabled = true;
                    //    }
                    //    //-----------------------
                    //    //����ظ���ӡ  ���Ʒ�ظ���ӡ
                    //    if (dr["PGM"].ToString() == "BCREP" && dr["INS"].ToString() == "Y" && !MenuBcRep.Enabled)
                    //    {
                    //        this.MenuBcRep.Enabled = true;
                    //        this.MenuDFRep.Enabled = true;
                    //    }
                    //    //��Ʒ�ظ���ӡ
                    //    if (dr["PGM"].ToString() == "FTREP" && dr["INS"].ToString() == "Y" && !MenuFTRep.Enabled)
                    //    {
                    //        this.MenuFTRep.Enabled = true;
                    //    }
                    //    //�����ظ���ӡ
                    //    if (dr["PGM"].ToString() == "WXREP" && dr["INS"].ToString() == "Y" && !MenuWxRep.Enabled)
                    //    {
                    //        this.MenuWxRep.Enabled = true;
                    //    }
                    //    //-----------------------
                    //    //���Ʒ��
                    //    if (dr["PGM"].ToString() == "BCQC" && dr["INS"].ToString() == "Y" && !MenuBCQC.Enabled)
                    //    {
                    //        this.MenuBCQC.Enabled = true;
                    //    }
                    //    //���ƷƷ��
                    //    if (dr["PGM"].ToString() == "DFQC" && dr["INS"].ToString() == "Y" && !MenuDFQC.Enabled)
                    //    {
                    //        this.MenuDFQC.Enabled = true;
                    //    }
                    //    //��Ʒ����
                    //    if (dr["PGM"].ToString() == "FTQC" && dr["INS"].ToString() == "Y" && !MenuFTQC.Enabled)
                    //    {
                    //        this.MenuFTQC.Enabled = true;
                    //    }
                    //    //ת�쳣֪ͨ��
                    //    if (dr["PGM"].ToString() == "TOTR" && dr["INS"].ToString() == "Y" && !MenuToTR.Enabled)
                    //    {
                    //        this.MenuToTR.Enabled = true;
                    //    }
                    //    //ת�쳣������ҵ
                    //    if (dr["PGM"].ToString() == "UNDOTR" && dr["INS"].ToString() == "Y" && !MenuUndoTR.Enabled)
                    //    {
                    //        this.MenuUndoTR.Enabled = true;
                    //    }
                    //    //------------------------
                    //    //�ֳ�װ��
                    //    if (dr["PGM"].ToString() == "XCBOX" && dr["INS"].ToString() == "Y" && !MenuXCBoxUp.Enabled)
                    //    {
                    //        this.MenuXCBoxUp.Enabled = true;
                    //    }
                    //    //�ֳ�����
                    //    if (dr["PGM"].ToString() == "XCUNBOX" && dr["INS"].ToString() == "Y" && !MenuXCUnDoBox.Enabled)
                    //    {
                    //        this.MenuXCUnDoBox.Enabled = true;
                    //    }
                    //    //�ֿ�װ��
                    //    if (dr["PGM"].ToString() == "WHBOX" && dr["INS"].ToString() == "Y" && !MenuWHBoxUp.Enabled)
                    //    {
                    //        this.MenuWHBoxUp.Enabled = true;
                    //    }
                    //    //�ֿ����
                    //    if (dr["PGM"].ToString() == "WHUNBOX" && dr["INS"].ToString() == "Y" && !MenuWHUndoBox.Enabled)
                    //    {
                    //        this.MenuWHUndoBox.Enabled = true;
                    //    }
                    //    //------------------
                    //    //����ͽ�
                    //    if (dr["PGM"].ToString() == "BCTOMM" && dr["INS"].ToString() == "Y" && !MenuBCToMM.Enabled)
                    //    {
                    //        this.MenuBCToMM.Enabled = true;
                    //    }
                    //    //��Ľɿ�
                    //    if (dr["PGM"].ToString() == "BCMM" && dr["INS"].ToString() == "Y" && !MenuBCMM.Enabled)
                    //    {
                    //        this.MenuBCMM.Enabled = true;
                    //    }
                    //    //���Ʒ�ͽ�
                    //    if (dr["PGM"].ToString() == "DFTOMM" && dr["INS"].ToString() == "Y" && !MenuDFToMM.Enabled)
                    //    {
                    //        this.MenuDFToMM.Enabled = true;
                    //    }
                    //    //���Ʒ�ɿ�
                    //    if (dr["PGM"].ToString() == "DFMM" && dr["INS"].ToString() == "Y" && !MenuDFMM.Enabled)
                    //    {
                    //        this.MenuDFMM.Enabled = true;
                    //    }
                    //    //��Ʒ�ͽ�
                    //    if (dr["PGM"].ToString() == "FTTOMM" && dr["INS"].ToString() == "Y" && !MenuFTToMM.Enabled)
                    //    {
                    //        this.MenuFTToMM.Enabled = true;
                    //    }
                    //    //��Ʒ�ɿ�
                    //    if (dr["PGM"].ToString() == "FTMM" && dr["INS"].ToString() == "Y" && !MenuFTMM.Enabled)
                    //    {
                    //        this.MenuFTMM.Enabled = true;
                    //    }
                    //    //�ͽɳ�����ҵ
                    //    if (dr["PGM"].ToString() == "UNDOTOMM" && dr["INS"].ToString() == "Y" && !MenuUndoToMM.Enabled)
                    //    {
                    //        this.MenuUndoToMM.Enabled = true;
                    //    }
                    //    //�ɿ⳷����ҵ
                    //    if (dr["PGM"].ToString() == "UNDOMM" && dr["INS"].ToString() == "Y" && !MenuUndoMM.Enabled)
                    //    {
                    //        this.MenuUndoMM.Enabled = true;
                    //    }
                    //    //-------------------

                    //    //����������Ʒ��
                    //    if (dr["PGM"].ToString() == "SABARQC" && dr["INS"].ToString() == "Y" && !MenuSABarQC.Enabled)
                    //    {
                    //        this.MenuSABarQC.Enabled = true;
                    //    }
                    //    //�����������滻
                    //    if (dr["PGM"].ToString() == "SACHGBAR" && dr["INS"].ToString() == "Y" && !MenuSABarChg.Enabled)
                    //    {
                    //        this.MenuSABarChg.Enabled = true;
                    //    }
                    //    //-----------------
                    //    //�ڳ���������ӡ
                    //    if (dr["PGM"].ToString() == "INITBCPRT" && dr["INS"].ToString() == "Y" && !MenuInitBCPrint.Enabled)
                    //    {
                    //        this.MenuInitBCPrint.Enabled = true;
                    //    }
                    //    //�ڳ����Ʒ�����ӡ
                    //    if (dr["PGM"].ToString() == "INITDFPRT" && dr["INS"].ToString() == "Y" && !this.MenuInitDFPrint.Enabled)
                    //    {
                    //        this.MenuInitDFPrint.Enabled = true;
                    //    }
                    //    //-------------------------------
                    //    //�ڳ���Ʒ�����ӡ
                    //    if (dr["PGM"].ToString() == "INITBARPRT" && dr["INS"].ToString() == "Y" && !MenuInitFTPrint.Enabled)
                    //    {
                    //        this.MenuInitFTPrint.Enabled = true;
                    //    }
                    //    //�ڳ���Ʒ����װ��
                    //    if (dr["PGM"].ToString() == "INITBARBOX" && dr["INS"].ToString() == "Y" && !MenuInitFTBox.Enabled)
                    //    {
                    //        this.MenuInitFTBox.Enabled = true;
                    //    }
                    //    //----------------------------------
                    //    //��������Ʒ����Ϣ��ϸ��
                    //    if (dr["PGM"].ToString() == "SABARQCLIST" && dr["INS"].ToString() == "Y" && !this.MenuRptSABarQCList.Enabled)
                    //    {
                    //        this.MenuRptSABarQCList.Enabled = true;
                    //    }
                    //    //������ͳ�Ʊ�
                    //    if (dr["PGM"].ToString() == "SASTAT" && dr["INS"].ToString() == "Y" && !MenuRptSAStat.Enabled)
                    //    {
                    //        this.MenuRptSAStat.Enabled = true;
                    //    }
                    //    //����Ʒ���ܱ���
                    //    if (dr["PGM"].ToString() == "BARQCSTA" && dr["INS"].ToString() == "Y" && !this.MenuRptBarQCSta.Enabled)
                    //    {
                    //        this.MenuRptBarQCSta.Enabled = true;
                    //    }
                    //    //����Ʒ����Ϣ��ϸ��
                    //    if (dr["PGM"].ToString() == "BARQCLIST" && dr["INS"].ToString() == "Y" && !this.MenuRptBarQCList.Enabled)
                    //    {
                    //        this.MenuRptBarQCList.Enabled = true;
                    //    }
                    //    //��������Ϣ��ϸ��
                    //    if (dr["PGM"].ToString() == "RPT_BOXBAR" && dr["INS"].ToString() == "Y" && !this.MenuRptBoxBarLst.Enabled)
                    //    {
                    //        this.MenuRptBoxBarLst.Enabled = true;
                    //    }
                    //    //��Ʒ��ʷ��ѯ
                    //    if (dr["PGM"].ToString() == "RPT_PRDT_HIS" && dr["INS"].ToString() == "Y" && !this.MenuRptPrdtHisList.Enabled)
                    //    {
                    //        this.MenuRptPrdtHisList.Enabled = true;
                    //    }
                    //    //��Ʒ����ͳ�Ʊ���
                    //    if (dr["PGM"].ToString() == "RPT_PRDT_STAT" && dr["INS"].ToString() == "Y" && !this.MenuRptPrdtStat.Enabled)
                    //    {
                    //        this.MenuRptPrdtStat.Enabled = true;
                    //    }
                    //    //----------------------------------
                    //    //�ڳ���Ľɿ�
                    //    if (dr["PGM"].ToString() == "INITBCMM" && dr["INS"].ToString() == "Y" && !MenuInitBCMM.Enabled)
                    //    {
                    //        this.MenuInitBCMM.Enabled = true;
                    //    }
                    //    //�ڳ���Ʒװ��
                    //    if (dr["PGM"].ToString() == "INITAUTOFTBOX" && dr["INS"].ToString() == "Y" && !MenuInitAutoFTBox.Enabled)
                    //    {
                    //        this.MenuInitAutoFTBox.Enabled = true;
                    //    }
                    //    //�Ϻ�/��ӡƷ���趨
                    //    if (dr["PGM"].ToString() == "BAR_IDX_SET" && dr["INS"].ToString() == "Y" && !MenuBarIdxSet.Enabled)
                    //    {
                    //        this.MenuBarIdxSet.Enabled = true;
                    //    }
                    //    //Ȩ���趨
                    //    if (dr["PGM"].ToString() == "RIGHTMNG" && dr["INS"].ToString() == "Y" && !MenuRightMng.Enabled)
                    //    {
                    //        this.MenuRightMng.Enabled = true;
                    //    }
                    //}
                }
            }
            #endregion
        }
        #endregion

        #region ϵͳ�趨
        private void MenuSysSet_Click(object sender, EventArgs e)
        {
            if (_bpSet == null || (_bpSet != null && _bpSet.IsDisposed))
            {
                _bpSet = new BarPrintSet();
                _bpSet.StartPosition = FormStartPosition.CenterParent;
                _bpSet.MaximizeBox = false;
                _bpSet.MinimizeBox = false;
                _bpSet.ShowDialog();
            }
            else
            {
                _bpSet.ShowDialog();
            }
        }
        #endregion

        #region ע��
        private string _logoutMsg = "ȷ���˳�ϵͳ��";
        private void MenuLogOff_Click(object sender, EventArgs e)
        {
            _logoutMsg = "ȷ��ע����";
            Application.Restart();
        }
        #endregion

        #region �˳�
        private void MenuExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult _result;
            _result = MessageBox.Show(_logoutMsg, "��ʾ", MessageBoxButtons.YesNo);
			if (_result == DialogResult.Yes)
			{
				e.Cancel = false;
			}
			else
				e.Cancel = true;
		}

		private void Main_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}
		#endregion

        #region �����ӡ

        #region ��Ĵ�ӡ
        private void MenuBCPrint_Click(object sender, EventArgs e)
		{
			if (_bcp == null || (_bcp != null && _bcp.IsDisposed))
			{
				_bcp = new BCPrint();
				_bcp.MdiParent = this;
				_bcp.WindowState = FormWindowState.Maximized;
				_bcp.Show();
                _bcp.BringToFront();
			}
			else
			{
				_bcp.Focus();
			}
        }

        private void MenuBcRep_Click(object sender, EventArgs e)
        {
            if (_bcRep == null || (_bcRep != null && _bcRep.IsDisposed))
            {
                _bcRep = new RepPrint();
                _bcRep.MdiParent = this;
                _bcRep.WindowState = FormWindowState.Maximized;
                _bcRep.cbPrdType.SelectedIndex = 0;
                _bcRep.cbPrdType.Enabled = false;
                _bcRep.cbFormat.Enabled = true;
                _bcRep.Show();
            }
            else
            {
                _bcRep.Close();
                _bcRep = new RepPrint();
                _bcRep.MdiParent = this;
                _bcRep.WindowState = FormWindowState.Maximized;
                _bcRep.cbPrdType.SelectedIndex = 0;
                _bcRep.cbPrdType.Enabled = false;
                _bcRep.cbFormat.Enabled = true;
                _bcRep.Show();
            }
        }
        #endregion

        #region  ���Ʒ��ӡ
        //��ͨ��ӡ
        private void MenuDFPrint_Click(object sender, EventArgs e)
        {
            if (_dfp == null || (_dfp != null && _dfp.IsDisposed))
            {
                _dfp = new DFPrint();
                _dfp.MdiParent = this;
                _dfp.WindowState = FormWindowState.Maximized;
                _dfp.Show();
            }
            else
            {
                _dfp.Focus();
            }
        }
        //�����ӡ
        private void MenuDFSpcPrint_Click(object sender, EventArgs e)
        {
            if (_dfSpcP == null || (_dfSpcP != null && _dfSpcP.IsDisposed))
            {
                _dfSpcP = new DFSpcPrint();
                _dfSpcP.MdiParent = this;
                _dfSpcP.WindowState = FormWindowState.Maximized;
                _dfSpcP.Show();
            }
            else
            {
                _dfSpcP.Focus();
            }
        }

        private void MenuDFRep_Click(object sender, EventArgs e)
        {
            if (_bcRep == null || (_bcRep != null && _bcRep.IsDisposed))
            {
                _bcRep = new RepPrint();
                _bcRep.MdiParent = this;
                _bcRep.WindowState = FormWindowState.Maximized;
                _bcRep.cbPrdType.SelectedIndex = 1;
                _bcRep.cbPrdType.Enabled = false;
                _bcRep.cbFormat.Enabled = false;
                _bcRep.Show();
            }
            else
            {
                _bcRep.Close();
                _bcRep = new RepPrint();
                _bcRep.MdiParent = this;
                _bcRep.WindowState = FormWindowState.Maximized;
                _bcRep.cbPrdType.SelectedIndex = 1;
                _bcRep.cbPrdType.Enabled = false;
                _bcRep.cbFormat.Enabled = false;
                _bcRep.Show();
            }
        }
        #endregion

        #region ��Ʒ��ӡ
        //��ͨ��ӡ
        private void MenuFTPrint_Click(object sender, EventArgs e)
		{
			if (_ftp == null || (_ftp != null && _ftp.IsDisposed))
			{
				_ftp = new FTPrint();
				_ftp.MdiParent = this;
				_ftp.WindowState = FormWindowState.Maximized;
				_ftp.Show();
			}
			else
			{
				_ftp.Focus();
			}
        }
        //�����ӡ
        private void MenuFTSpcPrint_Click(object sender, EventArgs e)
        {
            if (_ftSpcP == null || (_ftSpcP != null && _ftSpcP.IsDisposed))
            {
                _ftSpcP = new FTSpcPrint();
                _ftSpcP.MdiParent = this;
                _ftSpcP.WindowState = FormWindowState.Maximized;
                _ftSpcP.Show();
            }
            else
            {
                _ftSpcP.Focus();
            }
        }

        private void MenuFTRep_Click(object sender, EventArgs e)
        {
            if (_ftRep == null || (_ftRep != null && _ftRep.IsDisposed))
            {
                _ftRep = new FTRepPrint();
                _ftRep.MdiParent = this;
                _ftRep.WindowState = FormWindowState.Maximized;
                _ftRep.Show();
            }
            else
            {
                _ftRep.Focus();
            }
        }
        #endregion

        #region ���������ӡ
        //���������ظ���ӡ
        private void MenuWxRep_Click(object sender, EventArgs e)
        {
            if (_wxRep == null || (_wxRep != null && _wxRep.IsDisposed))
            {
                _wxRep = new WXRepPrint();
                _wxRep.MdiParent = this;
                _wxRep.WindowState = FormWindowState.Maximized;
                _wxRep.Show();
            }
            else
            {
                _wxRep.Focus();
            }
        }
        //���������ӡ
        private void MenuWXPrint_Click(object sender, EventArgs e)
        {
            if (_wxp == null || (_wxp != null && _wxp.IsDisposed))
            {
                _wxp = new WXPrint();
                _wxp.MdiParent = this;
                _wxp.WindowState = FormWindowState.Maximized;
                _wxp.Show();
            }
            else
            {
                _wxp.Focus();
            }
        }

        //�޸���������
        private void MenuWXEdit_Click(object sender, EventArgs e)
        {
            if (_wxEdit == null || (_wxEdit != null && _wxEdit.IsDisposed))
            {
                _wxEdit = new WXCodeEdit();
                _wxEdit.MdiParent = this;
                _wxEdit.WindowState = FormWindowState.Maximized;
                _wxEdit.Show();
            }
            else
            {
                _wxEdit.Focus();
            }
        }
        #endregion

        #endregion

        #region Ʒ�ʼ���
        private void MenuFTQC_Click(object sender, EventArgs e)
        {
            if (_bcFTQC == null || (_bcFTQC != null && _bcFTQC.IsDisposed))
            {
                _bcFTQC = new FTQC();
                _bcFTQC.MdiParent = this;
                _bcFTQC.WindowState = FormWindowState.Maximized;
                _bcFTQC.Show();
            }
            else
            {
                _bcFTQC.Focus();
            }
        }

        private void MenuBCQC_Click(object sender, EventArgs e)
        {
            if (_bcQC == null || (_bcQC != null && _bcQC.IsDisposed))
            {
                _bcQC = new BCQC();
                _bcQC.MdiParent = this;
                _bcQC.WindowState = FormWindowState.Maximized;
                _bcQC.Show();
            }
            else
            {
                _bcQC.Focus();
            }
        }
        #endregion

        #region װ��
        //�ֳ�װ��
        private void MenuXCBoxUp_Click(object sender, EventArgs e)
        {
            if (_bbup == null || (_bbup != null && _bbup.IsDisposed))
            {
                _bbup = new BarBoxUP();
                _bbup.MdiParent = this;
                _bbup.WindowState = FormWindowState.Maximized;
                _bbup.Show();
            }
            else
            {
                _bbup.Focus();
            }
        }
        //�ֳ�����
        private void MenuXCUnDoBox_Click(object sender, EventArgs e)
        {
            if (_buBox == null || (_buBox != null && _buBox.IsDisposed))
            {
                _buBox = new BarUndoBox();
                _buBox.MdiParent = this;
                _buBox.WindowState = FormWindowState.Maximized;
                _buBox.Show();
            }
            else
            {
                _buBox.Focus();
            }
        }
        #endregion

        #region Ȩ���趨
        private void MenuRightMng_Click(object sender, EventArgs e)
        {
            if (_urMng == null || (_urMng != null && _urMng.IsDisposed))
            {
                _urMng = new UserRightMng();
                _urMng.MdiParent = this;
                _urMng.WindowState = FormWindowState.Maximized;
                _urMng.Show();
            }
            else
            {
                _urMng.Focus();
            }
        }
        #endregion

        #region ����˻�����
        private void MenuBCSB_Click(object sender, EventArgs e)
        {
            if (_bcpSB == null || (_bcpSB != null && _bcpSB.IsDisposed))
            {
                _bcpSB = new BCSBPrint();
                _bcpSB.MdiParent = this;
                _bcpSB.WindowState = FormWindowState.Maximized;
                _bcpSB.Show();
            }
            else
            {
                _bcpSB.Focus();
            }
        }
        #endregion

        #region ��Ʒ�˻�����
        private void MenuFTSB_Click(object sender, EventArgs e)
        {
            if (_ftpSB == null || (_ftpSB != null && _ftpSB.IsDisposed))
            {
                _ftpSB = new FTSBPrint();
                _ftpSB.MdiParent = this;
                _ftpSB.WindowState = FormWindowState.Maximized;
                _ftpSB.Show();
            }
            else
            {
                _ftpSB.Focus();
            }
        }
        #endregion

        #region �˻�����
        private void MenuSBUndoBox_Click(object sender, EventArgs e)
        {
            if (_sbUndoBox == null || (_sbUndoBox != null && _sbUndoBox.IsDisposed))
            {
                _sbUndoBox = new SBUndoBox();
                _sbUndoBox.MdiParent = this;
                _sbUndoBox.WindowState = FormWindowState.Maximized;
                _sbUndoBox.Show();
            }
            else
            {
                _sbUndoBox.Focus();
            }
        }
        #endregion

        #region �˻������ѯ
        private void MenuSBBarList_Click(object sender, EventArgs e)
        {
            if (_rptSABarLst == null || (_rptSABarLst != null && _rptSABarLst.IsDisposed))
            {
                _rptSABarLst = new RPTSBBarList();
                _rptSABarLst.MdiParent = this;
                _rptSABarLst.WindowState = FormWindowState.Maximized;
                _rptSABarLst.Show();
            }
            else
            {
                _rptSABarLst.Focus();
            }
        }
        #endregion

        #region ����Ʒװ��
        private void MenuInitFTBox_Click(object sender, EventArgs e)
        {
            if (_ftInitBox == null || (_ftInitBox != null && _ftInitBox.IsDisposed))
            {
                _ftInitBox = new FTInitBarBoxUP();
                _ftInitBox.MdiParent = this;
                _ftInitBox.WindowState = FormWindowState.Maximized;
                _ftInitBox.Show();
            }
            else
            {
                _ftInitBox.Focus();
            }
        }
        #endregion

        #region ���к�Ʒ����Ϣ
        private void MenuBarQCList_Click(object sender, EventArgs e)
        {
            if (_rptSABarQCLst == null || (_rptSABarQCLst != null && _rptSABarQCLst.IsDisposed))
            {
                _rptSABarQCLst = new RPTSABarQCLst();
                _rptSABarQCLst.MdiParent = this;
                _rptSABarQCLst.WindowState = FormWindowState.Maximized;
                _rptSABarQCLst.Show();
            }
            else
            {
                _rptSABarQCLst.Focus();
            }

        }
        #endregion

        #region ���к�ɾ��
        private void MenuBarDel_Click(object sender, EventArgs e)
        {
            if (_barDel == null || (_barDel != null && _barDel.IsDisposed))
            {
                _barDel = new BarCodeDel();
                _barDel.MdiParent = this;
                _barDel.WindowState = FormWindowState.Maximized;
                _barDel.Show();
            }
            else
            {
                _barDel.Focus();
            }
        }
        #endregion

        #region �ֿ��Ʒװ��
        private void MenuWHBoxUp_Click(object sender, EventArgs e)
        {
            if (_whBoxUP == null || (_whBoxUP != null && _whBoxUP.IsDisposed))
            {
                _whBoxUP = new FTWHBarBoxUP();
                _whBoxUP.MdiParent = this;
                _whBoxUP.WindowState = FormWindowState.Maximized;
                _whBoxUP.Show();
            }
            else
            {
                _whBoxUP.Focus();
            }
        }
        #endregion

        #region �ֿ��Ʒ����
        private void MenuWHUndoBox_Click(object sender, EventArgs e)
        {
            if (_whUndoBox == null || (_whUndoBox != null && _whUndoBox.IsDisposed))
            {
                _whUndoBox = new FTWHUndoBox();
                _whUndoBox.MdiParent = this;
                _whUndoBox.WindowState = FormWindowState.Maximized;
                _whUndoBox.Show();
            }
            else
            {
                _whUndoBox.Focus();
            }
        }
        #endregion

        #region �ɿ�

        #region ����ͽ�
        private void MenuBCToMM_Click(object sender, EventArgs e)
        {
            if (_bcToMM == null || (_bcToMM != null && _bcToMM.IsDisposed))
            {
                _bcToMM = new BCToMM();
                _bcToMM.MdiParent = this;
                _bcToMM.WindowState = FormWindowState.Maximized;
                _bcToMM.Show();
            }
            else
            {
                _bcToMM.Focus();
            }
        }
        #endregion

        #region ��Ľɿ�
        private void MenuBCMM_Click(object sender, EventArgs e)
        {
            if (_bcMM == null || (_bcMM != null && _bcMM.IsDisposed))
            {
                _bcMM = new BCMM();
                _bcMM.MdiParent = this;
                _bcMM.WindowState = FormWindowState.Maximized;
                _bcMM.Show();
            }
            else
            {
                _bcMM.Focus();
            }
        }
        #endregion

        #region ���Ʒ�ͽ�
        private void MenuDFToMM_Click(object sender, EventArgs e)
        {
            if (_dfToMM == null || (_dfToMM != null && _dfToMM.IsDisposed))
            {
                _dfToMM = new DFToMM();
                _dfToMM.MdiParent = this;
                _dfToMM.WindowState = FormWindowState.Maximized;
                _dfToMM.Show();
            }
            else
            {
                _dfToMM.Focus();
            }
        }
        #endregion

        #region ���Ʒ�ɿ�
        private void MenuDFMM_Click(object sender, EventArgs e)
        {
            if (_dfMM == null || (_dfMM != null && _dfMM.IsDisposed))
            {
                _dfMM = new DFMM();
                _dfMM.MdiParent = this;
                _dfMM.WindowState = FormWindowState.Maximized;
                _dfMM.Show();
            }
            else
            {
                _dfMM.Focus();
            }
        }
        #endregion

        #region ��Ʒ�ͽ�
        private void MenuFTToMM_Click(object sender, EventArgs e)
        {
            if (_ftToMM == null || (_ftToMM != null && _ftToMM.IsDisposed))
            {
                _ftToMM = new FTToMM();
                _ftToMM.MdiParent = this;
                _ftToMM.WindowState = FormWindowState.Maximized;
                _ftToMM.Show();
            }
            else
            {
                _ftToMM.Focus();
            }
        }
        #endregion

        #region ��Ʒ�ɿ�
        private void MenuFTMM_Click(object sender, EventArgs e)
        {
            if (_ftMM == null || (_ftMM != null && _ftMM.IsDisposed))
            {
                _ftMM = new FTMM();
                _ftMM.MdiParent = this;
                _ftMM.WindowState = FormWindowState.Maximized;
                _ftMM.Show();
            }
            else
            {
                _ftMM.Focus();
            }
        }
        #endregion

        #region �����ͽ�
        private void MenuUndoToMM_Click(object sender, EventArgs e)
        {
            if (_undoTomm == null || (_undoTomm != null && _undoTomm.IsDisposed))
            {
                _undoTomm = new BarUndoToMM();
                _undoTomm.MdiParent = this;
                _undoTomm.WindowState = FormWindowState.Maximized;
                _undoTomm.Show();
            }
            else
            {
                _undoTomm.Focus();
            }
        }
        #endregion

        #region �ɿ⳷����ҵ
        private void MenuUndoMM_Click(object sender, EventArgs e)
        {
            if (_undoMM == null || (_undoMM != null && _undoMM.IsDisposed))
            {
                _undoMM = new BarUndoMM();
                _undoMM.MdiParent = this;
                _undoMM.WindowState = FormWindowState.Maximized;
                _undoMM.Show();
            }
            else
            {
                _undoMM.Focus();
            }
        }
        #endregion

        #endregion

        #region �ڳ���Ľɿ�
        private void MenuInitBCMM_Click(object sender, EventArgs e)
        {
            if (_bcInitMm == null || (_bcInitMm != null && _bcInitMm.IsDisposed))
            {
                _bcInitMm = new BCInitMM();
                _bcInitMm.MdiParent = this;
                _bcInitMm.WindowState = FormWindowState.Maximized;
                _bcInitMm.Show();
            }
            else
            {
                _bcInitMm.Focus();
            }
        }
        #endregion

        #region ���ƷƷ��
        private void MenuDFQC_Click(object sender, EventArgs e)
        {
            if (_dfQC == null || (_dfQC != null && _dfQC.IsDisposed))
            {
                _dfQC = new DFQC();
                _dfQC.MdiParent = this;
                _dfQC.WindowState = FormWindowState.Maximized;
                _dfQC.Show();
            }
            else
            {
                _dfQC.Focus();
            }
        }
        #endregion

        #region ��������޸�
        private void MenuBCEidtPrint_Click(object sender, EventArgs e)
        {
            if (_bcEidt == null || (_bcEidt != null && _bcEidt.IsDisposed))
            {
                _bcEidt = new BCEditPrint();
                _bcEidt.MdiParent = this;
                _bcEidt.WindowState = FormWindowState.Maximized;
                _bcEidt.Show();
            }
            else
            {
                _bcEidt.Focus();
            }
        }
        #endregion

        #region ��Ʒ�����޸�
        private void MenuFTEditPrint_Click(object sender, EventArgs e)
        {
            if (_ftEdit == null || (_ftEdit != null && _ftEdit.IsDisposed))
            {
                _ftEdit = new FTEditPrint();
                _ftEdit.MdiParent = this;
                _ftEdit.WindowState = FormWindowState.Maximized;
                _ftEdit.Show();
            }
            else
            {
                _ftEdit.Focus();
            }
        }
        #endregion

        #region ���Ʒ�����޸�
        private void MenuDFEditPrint_Click(object sender, EventArgs e)
        {
            if (_dfEdit == null || (_dfEdit != null && _dfEdit.IsDisposed))
            {
                _dfEdit = new DFEditPrint();
                _dfEdit.MdiParent = this;
                _dfEdit.WindowState = FormWindowState.Maximized;
                _dfEdit.Show();
            }
            else
            {
                _dfEdit.Focus();
            }
        }
        #endregion

        #region �ڳ���Ʒװ�䣨ѡ�����룩
        private void MenuInitAutoFTBox_Click(object sender, EventArgs e)
        {
            if (_ftInitAutoBox == null || (_ftInitAutoBox != null && _ftInitAutoBox.IsDisposed))
            {
                _ftInitAutoBox = new FTInitAutoBox();
                _ftInitAutoBox.MdiParent = this;
                _ftInitAutoBox.WindowState = FormWindowState.Maximized;
                _ftInitAutoBox.Show();
            }
            else
            {
                _ftInitAutoBox.Focus();
            }
        }
        #endregion

        #region ����

        #region ������ͳ�Ʊ�
        private void MenuRptSAStat_Click(object sender, EventArgs e)
        {
            if (_rptSaStat == null || (_rptSaStat != null && _rptSaStat.IsDisposed))
            {
                _rptSaStat = new RptSAStat();
                _rptSaStat.MdiParent = this;
                _rptSaStat.WindowState = FormWindowState.Maximized;
                _rptSaStat.Show();
            }
            else
            {
                _rptSaStat.Focus();
            }
        }
        #endregion

        private void MenuRptBarQCSta_Click(object sender, EventArgs e)
        {
            if (_rptBarQCSta == null || (_rptBarQCSta != null && _rptBarQCSta.IsDisposed))
            {
                _rptBarQCSta = new RPTBarQCSta();
                _rptBarQCSta.MdiParent = this;
                _rptBarQCSta.WindowState = FormWindowState.Maximized;
                _rptBarQCSta.Show();
            }
            else
            {
                _rptBarQCSta.Focus();
            }
        }

        #region ������ϸ����
        private void MenuRptBarQCList_Click(object sender, EventArgs e)
        {
            if (_rptBarQCLst == null || (_rptBarQCLst != null && _rptBarQCLst.IsDisposed))
            {
                _rptBarQCLst = new RPTBarQCList();
                _rptBarQCLst.MdiParent = this;
                _rptBarQCLst.WindowState = FormWindowState.Maximized;
                _rptBarQCLst.Show();
            }
            else
            {
                _rptBarQCLst.Focus();
            }
        }
        #endregion

        private void MenuRptBoxBarLst_Click(object sender, EventArgs e)
        {
            if (_rptBoxBarLst == null || (_rptBoxBarLst != null && _rptBoxBarLst.IsDisposed))
            {
                _rptBoxBarLst = new RPTBoxBarLst();
                _rptBoxBarLst.MdiParent = this;
                _rptBoxBarLst.WindowState = FormWindowState.Maximized;
                _rptBoxBarLst.Show();
            }
            else
            {
                _rptBoxBarLst.Focus();
            }
        }
        //��Ʒ��ʷ��ѯ
        private void MenuRptPrdtHisList_Click(object sender, EventArgs e)
        {
            if (_rptPrdtHis == null || (_rptPrdtHis != null && _rptPrdtHis.IsDisposed))
            {
                _rptPrdtHis = new RPTPrdtHisList();
                _rptPrdtHis.MdiParent = this;
                _rptPrdtHis.WindowState = FormWindowState.Maximized;
                _rptPrdtHis.Show();
            }
            else
            {
                _rptPrdtHis.Focus();
            }
        }
        //��Ʒ����ͳ�Ʊ���
        private void MenuRptPrdtStat_Click(object sender, EventArgs e)
        {
            if (_rptPrdtStat == null || (_rptPrdtStat != null && _rptPrdtStat.IsDisposed))
            {
                _rptPrdtStat = new RPTPrdtStat();
                _rptPrdtStat.MdiParent = this;
                _rptPrdtStat.WindowState = FormWindowState.Maximized;
                _rptPrdtStat.Show();
            }
            else
            {
                _rptPrdtStat.Focus();
            }
        }
        #endregion

        #region ��������ӡ

        private void MenuInitBCPrint_Click(object sender, EventArgs e)
        {
            if (_bcInitPrint == null || (_bcInitPrint != null && _bcInitPrint.IsDisposed))
            {
                _bcInitPrint = new BCInitPrint();
                _bcInitPrint.MdiParent = this;
                _bcInitPrint.WindowState = FormWindowState.Maximized;
                _bcInitPrint.Show();
            }
            else
            {
                _bcInitPrint.Focus();
            }
        }

        private void MenuInitDFPrint_Click(object sender, EventArgs e)
        {
            if (_dfInitPrint == null || (_dfInitPrint != null && _dfInitPrint.IsDisposed))
            {
                _dfInitPrint = new DFInitPrint();
                _dfInitPrint.MdiParent = this;
                _dfInitPrint.WindowState = FormWindowState.Maximized;
                _dfInitPrint.Show();
            }
            else
            {
                _dfInitPrint.Focus();
            }
        }

        #region ����Ʒ�����ӡ
        private void MenuInitFTPrint_Click(object sender, EventArgs e)
        {
            if (_ftInitPrt == null || (_ftInitPrt != null && _ftInitPrt.IsDisposed))
            {
                _ftInitPrt = new FTInitPrint();
                _ftInitPrt.MdiParent = this;
                _ftInitPrt.WindowState = FormWindowState.Maximized;
                _ftInitPrt.Show();
            }
            else
            {
                _ftInitPrt.Focus();
            }
        }
        #endregion

        #endregion

        #region ���������ӡ
        private void MenuChgBCPrint_Click(object sender, EventArgs e)
        {
            if (_bcChgPrint == null || (_bcChgPrint != null && _bcChgPrint.IsDisposed))
            {
                _bcChgPrint = new BCChgPrint();
                _bcChgPrint.MdiParent = this;
                _bcChgPrint.WindowState = FormWindowState.Maximized;
                _bcChgPrint.Show();
            }
            else
            {
                _bcChgPrint.Focus();
            }
        }

        private void MenuChgDFPrint_Click(object sender, EventArgs e)
        {
            if (_dfChgPrint == null || (_dfChgPrint != null && _dfChgPrint.IsDisposed))
            {
                _dfChgPrint = new DFChgPrint();
                _dfChgPrint.MdiParent = this;
                _dfChgPrint.WindowState = FormWindowState.Maximized;
                _dfChgPrint.Show();
            }
            else
            {
                _dfChgPrint.Focus();
            }
        }

        private void MenuChgFTPrint_Click(object sender, EventArgs e)
        {
            if (_ftChgPrint == null || (_ftChgPrint != null && _ftChgPrint.IsDisposed))
            {
                _ftChgPrint = new FTChgPrint();
                _ftChgPrint.MdiParent = this;
                _ftChgPrint.WindowState = FormWindowState.Maximized;
                _ftChgPrint.Show();
            }
            else
            {
                _ftChgPrint.Focus();
            }
        }
        #endregion

        #region ת�쳣֪ͨ��
        /// <summary>
        /// ת�쳣��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuToTR_Click(object sender, EventArgs e)
        {
            if (_barToTR == null || (_barToTR != null && _barToTR.IsDisposed))
            {
                _barToTR = new BarToTR();
                _barToTR.MdiParent = this;
                _barToTR.WindowState = FormWindowState.Maximized;
                _barToTR.Show();
            }
            else
            {
                _barToTR.Focus();
            }
        }
        /// <summary>
        /// ת�쳣������ҵ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuUndoTR_Click(object sender, EventArgs e)
        {
            if (_undoTR == null || (_undoTR != null && _undoTR.IsDisposed))
            {
                _undoTR = new BarUndoTR();
                _undoTR.MdiParent = this;
                _undoTR.WindowState = FormWindowState.Maximized;
                _undoTR.Show();
            }
            else
            {
                _undoTR.Focus();
            }
        }
        #endregion

        #region ���������滻
        private void MenuSABarQC_Click(object sender, EventArgs e)
        {
            if (_saBarQC == null || (_saBarQC != null && _saBarQC.IsDisposed))
            {
                _saBarQC = new SASelBarCode();
                _saBarQC.MdiParent = this;
                _saBarQC.WindowState = FormWindowState.Maximized;
                _saBarQC.Show();
            }
            else
            {
                _saBarQC.Focus();
            }
        }

        private void MenuSABarChg_Click(object sender, EventArgs e)
        {
            if (_saChgBar == null || (_saChgBar != null && _saChgBar.IsDisposed))
            {
                _saChgBar = new SAChgBarCode();
                _saChgBar.MdiParent = this;
                _saChgBar.WindowState = FormWindowState.Maximized;
                _saChgBar.Show();
            }
            else
            {
                _saChgBar.Focus();
            }
        }
        #endregion

        #region �Ϻ�/��ӡƷ���趨
        private void MenuBarIdxSet_Click(object sender, EventArgs e)
        {
            if (_barIdxSet == null || (_barIdxSet != null && _barIdxSet.IsDisposed))
            {
                _barIdxSet = new BarPrtNameSet();
                _barIdxSet.StartPosition = FormStartPosition.CenterParent;
                _barIdxSet.MaximizeBox = false;
                _barIdxSet.MinimizeBox = false;
                _barIdxSet.ShowDialog();
            }
            else
            {
                _barIdxSet.ShowDialog();
            }
        }
        #endregion

        //int paintX;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeInfo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    paintX = (paintX-10) % lblNotice.Width;
        //    if (paintX == -1 * lblNotice.Width+10)
        //        paintX = lblNotice.Width;
        //    lblNotice.Invalidate();
        }

        //private void lblNotice_Paint(object sender, PaintEventArgs e)
        //{
        //    ToolStripStatusLabel lb = sender as ToolStripStatusLabel;
        //    lb.TextAlign = ContentAlignment.MiddleLeft;
        //    e.Graphics.DrawString("ע�⣺�˰汾�������SUNLIKE v9.0ʹ��  ", lb.Font, Brushes.Black, new PointF(paintX, 0)); 
        //}
    }
}