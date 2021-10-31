using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;

namespace Sunlike.Business
{
    public class DRPCK : BizObject, IAuditing, ICloseBill
    {
        #region ����
        private bool _isRunAuditing;
        private string _usr;
        #endregion

        #region ȡ����
        /// <summary>
        /// ȡ����
        /// </summary>
        /// <param name="bilid">����</param>
        /// <param name="bilno">����</param>
        /// <returns>ds</returns>
        public SunlikeDataSet GetData(string pgm, string usr, string bilid, string bilno)
        {
            _usr = usr;
            DbDRPCK _db = new DbDRPCK(Comp.Conn_DB);
            SunlikeDataSet _ds = _db.GetData(bilid, bilno);
            if (!string.IsNullOrEmpty(_usr))
            {
                Users _usrs = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_usrs.GetUserDepNo(_usr)).DecimalDigitsInfo.System;

                #region �Զ�������
                //SetColAutoIncrement(_ds.Tables["TF_CK"].Columns["OTH_ITM"]);
                SetColAutoIncrement(_ds.Tables["TF_CK"].Columns["PRE_ITM"]);
                #endregion
                #region ��¼ԭ���ݵ�����
                _ds.Tables["TF_CK"].Columns.Add(new DataColumn("QTY_SO_ORG", typeof(System.Decimal)));
                foreach (DataRow dr in _ds.Tables["TF_CK"].Rows)
                {
                    string _billId = dr["OS_ID"].ToString();
                    string _billNo = dr["OS_NO"].ToString();
                    string _itm = dr["EST_ITM"].ToString();
                    if (!string.IsNullOrEmpty(_billId) && !string.IsNullOrEmpty(_billNo) && !string.IsNullOrEmpty(_itm))
                        dr["QTY_SO_ORG"] = _db.GetQtySoOrg(_billId, _billNo, _itm);
                }
                #endregion
                this.SetCanModify(pgm, _ds, _usr, true);
            }
            return _ds;
        }
        /// <summary>
        /// ȡ��Ӧ����
        /// </summary>
        /// <param name="ckId"></param>
        /// <param name="ckNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string ckId, string ckNo, int preItm)
        {
            DbDRPCK _dbDrpCk = new DbDRPCK(Comp.Conn_DB);
            return _dbDrpCk.GetBody(ckId, ckNo, preItm);
        }

        #endregion

        #region ����
        /// <summary>
        /// ÿ�м�¼����ǰ
        /// </summary>
        /// <param name="tableName">tableName</param>
        /// <param name="statementType">statementType</param>
        /// <param name="dr">dr</param>
        /// <param name="status">status</param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            SQNO _sq = new SQNO();

            #region MF
            if (string.Compare(tableName, "MF_CK") == 0)
            {
                #region ����
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["CK_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["CK_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion

                if (statementType != StatementType.Delete)
                {
                    #region ������������Ϸ���
                    if (string.IsNullOrEmpty(_usr))
                        _usr = Convert.ToString(dr["USR"]);
                    DateTime ckdd = Convert.ToDateTime(dr["CK_DD"]);
                    string _ckId = Convert.ToString(dr["CK_ID"]);
                    string _ckNo = Convert.ToString(dr["CK_NO"]);

                    //��������ͻ�
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString(), ckdd))
                    {
                        dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//�ͻ�����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //���ҵ��Ա
                    Salm _salm = new Salm();
                    if (!String.IsNullOrEmpty(dr["SAL_NO"].ToString()) && !_salm.IsExist(_usr, dr["SAL_NO"].ToString(), ckdd))
                    {
                        dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//ҵ��Ա����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //��鲿��
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_usr, dr["DEP"].ToString(), ckdd))
                    {
                        dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEP_NOTEXIST,PARAM=" + dr["DEP"].ToString());//���Ŵ���[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    #endregion
                    if (statementType == StatementType.Insert)
                    {
                        #region ȡ����
                        dr["CK_NO"] = _sq.Set(_ckId, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["CK_DD"]), dr["BIL_TYPE"].ToString());
                        #endregion
                        #region MFĬ����λ
                        dr["PRT_SW"] = "N";
                        dr["YD_ID"] = "T";
                        dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        if (string.IsNullOrEmpty(dr["CUR_ID"].ToString()))
                        {
                            dr["EXC_RTO"] = 1;
                        }
                        #endregion
                    }
                }
                if (statementType == StatementType.Delete)
                {
                    #region ɾ��SQNO,AUDITING
                    string _error = _sq.Delete(dr["CK_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new Exception("RCID=COMMON.HINT.DEL_NO_ERROR,PARAM=" + _error);//�޷�ɾ�����ţ�ԭ��{0}
                    }
                    #endregion
                }
                //#region ��˹���
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["CK_DD"]);
                //    _aps.BIL_ID = dr["CK_ID"].ToString();
                //    _aps.BIL_NO = dr["CK_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
                //}
                //else
                //    _aps = new AudParamStruct(Convert.ToString(dr["CK_ID", DataRowVersion.Original]), Convert.ToString(dr["CK_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
            #endregion

            #region TF
            if (string.Compare(tableName, "TF_CK") == 0)
            {
                if (statementType != StatementType.Delete)
                {
                    #region ������������Ϸ���
                    Prdt _prdt = new Prdt();
                    string _prd_no = dr["PRD_NO"].ToString();
                    DateTime ckdd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_CK"].Rows[0]["CK_DD"]);
                    //����Ʒ����
                    if (!_prdt.IsExist(_usr, _prd_no, ckdd))
                    {
                        dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + _prd_no);//��Ʒ����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //��������ֶ�
                    string _prdMark = dr["PRD_MARK"].ToString();
                    int _prdMod = _prdt.CheckPrdtMod(_prd_no, _prdMark);
                    if (_prdMod == 1)
                    {
                        dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prdMark);//��Ʒ����[{0}]������
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else if (_prdMod == 2)
                    {
                        PrdMark _mark = new PrdMark();
                        if (_mark.RunByPMark(_usr))
                        {
                            string[] _aryMark = _mark.BreakPrdMark(_prdMark);
                            DataTable _dtMark = _mark.GetSplitData("");
                            //����������Ʒ����ΪA��B��C����������Ϊ��
                            bool _markCanNull = false;
                            DataTable _dtPrdt = _prdt.GetPrdt(dr["PRD_NO"].ToString());
                            if (_dtPrdt.Rows.Count > 0)
                            {
                                if (_dtPrdt.Rows[0]["KND"].ToString().IndexOfAny(new char[] { 'A', 'B', 'C' }) != -1)
                                    _markCanNull = true;
                            }
                            for (int i = 0; i < _dtMark.Rows.Count; i++)
                            {
                                if (_markCanNull && String.IsNullOrEmpty(_aryMark[i].Trim()))
                                    continue;
                                string _fldName = _dtMark.Rows[i]["FLDNAME"].ToString();
                                if (!_mark.IsExist(_fldName, _prd_no, _aryMark[i]))
                                {
                                    dr.SetColumnError(_fldName, "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _aryMark[i].Trim());//��Ʒ����[{0}]������
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                    //�ֿ�(����)
                    string _wh = dr["WH"].ToString();
                    WH SunlikeWH = new WH();
                    if (!SunlikeWH.IsExist(_usr, _wh, ckdd))
                    {
                        dr.SetColumnError("WH",/*�ֿ����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�޸�PRD_NAME�ֶ�
                    if (String.IsNullOrEmpty(dr["PRD_NAME"].ToString()))
                    {
                        DataTable _dt = _prdt.GetPrdt(dr["PRD_NO"].ToString());
                        if (_dt.Rows.Count > 0)
                        {
                            dr["PRD_NAME"] = _dt.Rows[0]["NAME"];
                        }
                    }

                    #endregion

                    #region ����ֵ��ʽ��
                    if (!string.IsNullOrEmpty(dr["EST_DD"].ToString()))
                        dr["EST_DD"] = Convert.ToDateTime(dr["EST_DD"]).ToString(Comp.SQLDateFormat);
                    #endregion

                    if (statementType == StatementType.Insert)
                    {
                        #region TFĬ����λ
                        if (String.IsNullOrEmpty(Convert.ToString(dr["OTH_ITM"])))
                            dr["OTH_ITM"] = dr["PRE_ITM"];
                        if (String.IsNullOrEmpty(Convert.ToString(dr["ZHANG_ID"])))
                            dr["ZHANG_ID"] = "2";

                        if (dr.Table.DataSet.Tables["MF_CK"].Rows.Count > 0)
                        {
                            if (String.IsNullOrEmpty(dr["CUS_OS_NO"].ToString()))
                            {
                                dr["CUS_OS_NO"] = dr.Table.DataSet.Tables["MF_CK"].Rows[0]["CUS_OS_NO"];
                            }
                        }
                        #endregion

                    }
                }
                else
                {
                    //�ж��Ƿ��в�����Ʊ������ɾ��
                    checktflz(dr);
                }

                #region ��дԭ����д����
                UpdateOS(dr, false);
                UpdateWH(dr, false);
                #endregion


            }
            #endregion

            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }

        /// <summary>
        /// �жϵ��ݱ����Ƿ񲹿���Ʊ������ɾ��
        /// </summary>
        /// <param name="dr">tf_ck.row</param>
        private void checktflz(DataRow dr)
        {
            InvIK _invik = new InvIK();
            string lzid = "LZ";
            string bilId = dr["CK_ID", DataRowVersion.Original].ToString();
            string ckNo = dr["CK_NO", DataRowVersion.Original].ToString();

            SunlikeDataSet _ds = _invik.GetInTfLz(lzid, bilId, ckNo);
            if (_ds.Tables["TF_LZ"].Rows.Count > 0)
            {
                throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + ",PARAM=" + _ds.Tables["TF_LZ"].Rows[0]["LZ_NO"].ToString());//�޷�ɾ�����ţ�ԭ��{0}
            }

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="_ds">dataset����</param>
        public DataTable UpdateData(string pgm, SunlikeDataSet _ds, string usr, bool bubbleException)
        {
            DataTable _dtErr = null;
            Auditing _auditing = new Auditing();

            #region //�ж��Ƿ����������
            if (_ds.Tables.Contains("MF_CK"))
            {
                DataRow _dr = _ds.Tables["MF_CK"].Rows[0];
                string _bilID = "";
                string _bilType = "";
                string _mobID = "";
                if (_dr.RowState == DataRowState.Deleted)
                {
                    _bilID = _dr["CK_ID", DataRowVersion.Original].ToString();
                    _usr = _dr["USR", DataRowVersion.Original].ToString();
                    if (_dr.Table.Columns.Contains("BIL_TYPE"))
                        _bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
                    if (_dr.Table.Columns.Contains("MOB_ID"))
                        _mobID = _dr["MOB_ID", DataRowVersion.Original].ToString();//@@
                }
                else
                {
                    _usr = _dr["USR"].ToString();
                    _bilID = _dr["CK_ID"].ToString();
                    if (_dr.Table.Columns.Contains("BIL_TYPE"))
                        _bilType = _dr["BIL_TYPE"].ToString();
                    if (_dr.Table.Columns.Contains("MOB_ID"))
                        _mobID = _dr["MOB_ID"].ToString();//@@
                }
                //_isRunAuditing = _auditing.IsRunAuditing(_bilID, _usr, _bilType, _mobID);//֧��ֱ������MOBID=@@
            }
            #endregion

            Hashtable _ht = new Hashtable();
            _ht["MF_CK"] = @"CK_ID, CK_NO, CK_DD, PAY_DD, CHK_DD, TRAD_MTH, BAT_NO, CUS_NO, DEP, OS_ID, OS_NO, CUR_ID, EXC_RTO, SAL_NO, REM, PAY_MTH, 
                          PAY_DAYS, CHK_DAYS, INT_DAYS, PAY_REM, USR, CHK_MAN, PRT_SW, CPY_SW, CONTRACT, AMT, AMTN_NET, TAX, TAX_ID, SEND_MTH, 
                          SEND_WH, ADR, DIS_CNT, PS_NO, AMT_CLS, AMTN_NET_CLS, TAX_CLS, CLS_REM, CLS_DATE, ZHANG_ID, SA_CLS_ID, LZ_CLS_ID, CLSSA, CLSLZ, 
                          YD_ID, BIL_TYPE, CUS_OS_NO, MOB_ID, LOCK_MAN, SYS_DATE, CAS_NO, TASK_ID, TURN_ID, PRT_USR, QTY_CLS, ZGFX, SOFH_ID, 
                          CANCEL_ID,CUS_NO_POS,INST_TEAM,AMTN_DS";
            _ht["TF_CK"] = @"CK_ID, CK_NO, ITM, CK_DD, WH, BAT_NO, OS_ID, OS_NO, PRD_NO, PRD_NAME, PRD_MARK, UNIT, QTY, QTY1, CSTN_SAL, UP, AMTN_NET, AMT, 
                          TAX, DIS_CNT, VALID_DD, REM, EST_DD, TAX_RTO, CST_STD, UP_QTY1, EST_ITM, QTY_PS, QTY_PS_UNSH, ID_NO, FREE_ID, QTY_RK, 
                          QTY_RK_UNSH, AMT_FP, AMTN_NET_FP, TAX_FP, PRICE_ID, OTH_ITM, PAK_UNIT, PAK_EXC, PAK_NW, PAK_WEIGHT_UNIT, PAK_GW, PAK_MEAST, 
                          PAK_MEAST_UNIT, PRE_ITM, ZHANG_ID, CHK_TAX, SUP_PRD_NO, COMPOSE_IDNO, CUS_OS_NO, REM2, QTY_FP, FH_NO, QTY_ARK, QTY_PRE, 
                          QTY_PRE_UNSH, QTY1_PRE, QTY1_PRE_UNSH, RK_DD, DEP_RK, PW_ITM, CNTT_NO, BZ_KND, WH_SEND, AMT_DIS_CNT";

            this.UpdateDataSet(_ds, _ht);
            //�жϵ����ܷ��޸�
            if (!_ds.HasErrors)
            {
                this.SetCanModify(pgm, _ds, usr, true);
            }
            else
            {
                _dtErr = GetAllErrors(_ds);
            }
            return _dtErr;
        }

        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables["MF_CK"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["CK_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["CK_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion


            base.BeforeDsSave(ds);
        }
        #endregion

        #region ����

        private void SetColAutoIncrement(DataColumn _dc)
        {
            _dc.AutoIncrement = true;
            _dc.AutoIncrementSeed = _dc.Table.Rows.Count > 0 ? Convert2Int(_dc.Table.Select("", _dc.ColumnName + " desc")[0][_dc.ColumnName]) + 1 : 1;
            _dc.AutoIncrementStep = 1;
            _dc.Unique = true;
        }

        private string SetCanModify(string pgm, SunlikeDataSet _ds, string _usr, bool isCheckAuditing)
        {
            string _err = "";
            Auditing _auditing = new Auditing();
            if (_ds.Tables.Contains("MF_CK"))
            {
                if (_ds.Tables["MF_CK"].Rows.Count > 0)
                {
                    DataRow _drMf = _ds.Tables["MF_CK"].Rows[0];
                    DataTable _dtTf = _ds.Tables["TF_CK"];
                    string bilID = Convert.ToString(_drMf["CK_ID"]);
                    string bilNO = Convert.ToString(_drMf["CK_NO"]);

                    #region ����Ȩ�޿ع�
                    if (!string.IsNullOrEmpty(_usr))
                    {
                        string _bill_Dep = _drMf["DEP"].ToString();
                        string _bill_Usr = _drMf["USR"].ToString();

                        Hashtable _billRight = Users.GetBillRight(pgm, _usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                    #endregion

                    bool _canModify = true;// Ĭ�Ͽ����޸�

                    #region /**�������������ģ������޸�**/
                    if (Comp.HasCloseBill(Convert.ToDateTime(_drMf["CK_DD"]), _drMf["DEP"].ToString(), "CLS_INV"))
                    {
                        _err = "RCID=COMMON.HINT.CLOSE_CLS";//����
                        _canModify = false;
                        //Common.SetCanModifyRem(_ds, _err);
                    }
                    //�Ѿ������᰸
                    if (CaseInsensitiveComparer.Default.Compare(_drMf["SA_CLS_ID"].ToString(), "T") == 0)
                    {
                        _err = "RCID=COMMON.HINT.CLOSE_MODIFY";//�᰸�����޸�
                        _canModify = false;
                        //Common.SetCanModifyRem(_ds, _err);
                    }
                    //�Ѿ����˽᰸
                    if (CaseInsensitiveComparer.Default.Compare(_drMf["LZ_CLS_ID"].ToString(), "T") == 0)
                    {
                        _canModify = false;//ֻ���޸����ʱ�Ǻ������᰸���
                        _err = "RCID=COMMON.HINT.CLOSE_LZ_MODIFY";//�ѿ�Ʊ��
                        //Common.SetCanModifyRem(_ds, _err);
                    }
                    //�Ѿ���Ʊ
                    if (Convert2Decimal(_drMf["AMTN_NET_CLS"]) > 0)
                    {
                        _ds.ExtendedProperties["DEL"] = "N";
                        _err = "RCID=COMMON.HINT.CLOSE_LZ_MODIFY;";//�ѿ�Ʊ�Ĳ����������

                    }
                    //��תKB�˻�תSA����
                    foreach (DataRow _dr in _dtTf.Select())
                    {
                        if (Convert2Decimal(_dr["QTY_PRE"]) > 0)
                        {
                            _err = "RCID=INV.HINT.UNROLLBACKPRE;";//��תkb
                            break;
                        }
                        if (Convert2Decimal(_dr["QTY_PS"]) > 0)
                        {
                            _err = "RCID=INV.HINT.UNROLLBACKPS;";//��ת����
                            break;
                        }
                        if (Convert2Decimal(_dr["AMTN_NET_FP"]) > 0)
                        {
                            _err = "RCID=INV.HINT.UNRB_FP;";//�ѿ�Ʊ�Ĳ����������
                            break;
                        }
                    }
                    //�����������
                    if (isCheckAuditing)
                    {
                        if (_auditing.GetIfEnterAuditing(bilID, bilNO))
                        {
                            _canModify = false;
                            //Common.SetCanModifyRem(_ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                        }
                    }
                    //����ת
                    if (!string.IsNullOrEmpty(_drMf["PS_NO"].ToString()))
                    {
                        _canModify = false;
                        //Common.SetCanModifyRem(_ds, "RCID=INV.HINT.UNROLLBACKPS");
                    }
                    //�ж��Ƿ�����
                    if (!String.IsNullOrEmpty(_drMf["LOCK_MAN"].ToString()))
                    {
                        _canModify = false;
                        //Common.SetCanModifyRem(_ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                    }

                    #endregion

                    #region ƾ֤�Ķ�Ӧ
                    #endregion

                    _ds.ExtendedProperties["CAN_MODIFY"] = _canModify ? "T" : "F";
                }
            }
            return _err;
        }

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

        #region ����Դ��

        private void UpdateOS(DataRow dr, bool isByAuditPass)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr.RowState == DataRowState.Modified)
                    UpdateOS(dr, true, isByAuditPass);
                UpdateOS(dr, false, isByAuditPass);
            }
            else
                UpdateOS(dr, true, isByAuditPass);

        }

        private void UpdateWH(DataRow dr, bool isByAuditPass)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr.RowState == DataRowState.Modified)
                    UpdateWH(dr, true, isByAuditPass);
                UpdateWH(dr, false, isByAuditPass);
            }
            else
                UpdateWH(dr, true, isByAuditPass);

        }

        private void UpdateWH(DataRow dr, bool isDel, bool isByAuditPass)
        {
            UpdateWH(dr, isDel, isByAuditPass, false);
        }

        private void UpdateWH(DataRow dr, bool isDel, bool isByAuditPass, bool skipUNSH)
        {// ��д�ֲִ��������ŷֲִ���
            #region �������
            string _ckid = "";
            string _prdNo = "";
            string _prdMark = "";
            string _wh = "";
            string _validDd = "";
            string _batNo = "";
            string _unit = "";
            decimal _qty = 0;

            #endregion
            #region ������ֵ
            if (isDel)
            {
                _ckid = Convert.ToString(dr["CK_ID", DataRowVersion.Original]);
                _prdNo = Convert.ToString(dr["PRD_NO", DataRowVersion.Original]);
                _prdMark = Convert.ToString(dr["PRD_MARK", DataRowVersion.Original]);
                _wh = Convert.ToString(dr["WH", DataRowVersion.Original]);
                _validDd = Convert.ToString(dr["VALID_DD", DataRowVersion.Original]);
                _batNo = Convert.ToString(dr["BAT_NO", DataRowVersion.Original]);
                _unit = Convert.ToString(dr["UNIT", DataRowVersion.Original]);
                _qty = Convert2Decimal(dr["QTY", DataRowVersion.Original]) * (-1);
            }
            else
            {
                _ckid = Convert.ToString(dr["CK_ID"]);
                _prdNo = Convert.ToString(dr["PRD_NO"]);
                _prdMark = Convert.ToString(dr["PRD_MARK"]);
                _wh = Convert.ToString(dr["WH"]);
                _validDd = Convert.ToString(dr["VALID_DD"]);
                _batNo = Convert.ToString(dr["BAT_NO"]);
                _unit = Convert.ToString(dr["UNIT"]);
                _qty = Convert2Decimal(dr["QTY"]);
            }
            #region ����CK or �����˻�KB
            if (string.Compare(_ckid, "KB") == 0)
                _qty *= -1;
            #endregion
            #endregion
            if (!_isRunAuditing || isByAuditPass) //�����ͨ�����߲���Ҫ���������
            {
                #region �޸Ŀ��
                WH wh = new WH();
                Prdt _prdt = new Prdt();
                if (String.IsNullOrEmpty(_batNo))//������
                {
                    //wh.UpdateQty(_prdNo, _prdMark, _wh, _unit, WH.QtyTypes., _qty);
                }
                else//���ſع�
                {
                    SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _wh);
                    Hashtable _ht = new Hashtable();
                    //_ht[WH.QtyTypes.QTY_CK] = _qty;
                    if (!string.IsNullOrEmpty(_validDd))
                    {
                        if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                        {
                            TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                            if (_timeSpan.Days > 0)//δ����                            
                                _validDd = "";
                        }
                    }
                    wh.UpdateQty(_batNo, _prdNo, _prdMark, _wh, _validDd, _unit, _ht);
                }
                #endregion
            }
            if (!skipUNSH)
            {
                if (_isRunAuditing || isByAuditPass) //��Ҫ��������̻��߱����ͨ��
                {
                    #region ����ͬ��
                    if (isByAuditPass)
                        _qty *= -1;
                    #endregion
                    #region �޸Ŀ��
                    Hashtable _ht = new Hashtable();
                    //_ht[WH.QtyTypes.QTY_CK] = _qty;
                    WH wh = new WH();
                    //wh.UpdateShQty(_batNo, _prdNo, _prdMark, _wh, _unit, _ht);
                    #endregion
                }
            }
        }

        private void UpdateOS(DataRow dr, bool isDel, bool isByAuditPass)
        {//��дԴ��
            //if (!_isRunAuditing || isByAuditPass) //�����ͨ�����߲���Ҫ���������
            {
                DbDRPCK _db = new DbDRPCK(Comp.Conn_DB);
                string _ckId = "";
                string _prdNo = "";
                string _unit = "";
                string osId = "";
                string osNo = "";
                string osItm = "";
                decimal _qty = 0;
                decimal _qty1 = 0;
                string _chkMan = "";
                //ȡ��ͷ��Ϣ
                if (dr.Table.DataSet != null && dr.Table.DataSet.Tables.Contains("MF_CK") && dr.Table.DataSet.Tables["MF_CK"].Rows.Count > 0)
                {
                    if (dr.Table.DataSet.Tables["MF_CK"].Rows[0].RowState == DataRowState.Deleted)
                    {
                        _chkMan = dr.Table.DataSet.Tables["MF_CK"].Rows[0]["CHK_MAN", DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        _chkMan = dr.Table.DataSet.Tables["MF_CK"].Rows[0]["CHK_MAN"].ToString();
                    }
                }

                if (!isDel)
                {
                    _ckId = Convert.ToString(dr["CK_ID"]);
                    _prdNo = Convert.ToString(dr["PRD_NO"]);
                    _unit = Convert.ToString(dr["UNIT"]);
                    osId = Convert.ToString(dr["OS_ID"]);
                    osNo = Convert.ToString(dr["OS_NO"]);
                    osItm = Convert.ToString(dr["EST_ITM"]);
                    _qty = Convert2Decimal(dr["QTY"]);
                    _qty1 = Convert2Decimal(dr["QTY1"]);

                }
                else
                {
                    _ckId = Convert.ToString(dr["CK_ID", DataRowVersion.Original]);
                    _prdNo = Convert.ToString(dr["PRD_NO", DataRowVersion.Original]);
                    _unit = Convert.ToString(dr["UNIT", DataRowVersion.Original]);
                    osId = Convert.ToString(dr["OS_ID", DataRowVersion.Original]);
                    osNo = Convert.ToString(dr["OS_NO", DataRowVersion.Original]);
                    osItm = Convert.ToString(dr["EST_ITM", DataRowVersion.Original]);
                    _qty = Convert2Decimal(dr["QTY", DataRowVersion.Original]) * (-1);
                    _qty1 = Convert2Decimal(dr["QTY1", DataRowVersion.Original]) * (-1);
                }
                if (!String.IsNullOrEmpty(osId) && !String.IsNullOrEmpty(osNo) && !String.IsNullOrEmpty(osItm))
                {
                    Hashtable _ht;
                    if (CaseInsensitiveComparer.Default.Compare(_ckId, "KB") == 0)
                    {
                        #region CK_ID='KB'
                        _ht = new Hashtable();
                        _ht["OsID"] = osId;
                        _ht["OsNO"] = osNo;
                        _ht["KeyItm"] = osItm;
                        _ht["TableName"] = "TF_CK";
                        _ht["IdName"] = "CK_ID";
                        _ht["NoName"] = "CK_NO";
                        _ht["ItmName"] = "PRE_ITM";
                        _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_unit), _ht);

                        if (string.IsNullOrEmpty(_chkMan))
                        {
                            _db.UpdateQtyPre(osId, osNo, osItm, _qty, _qty1, "QTY_PRE_UNSH", "QTY1_PRE_UNSH");
                            if (isByAuditPass)
                            {
                                _db.UpdateQtyPre(osId, osNo, osItm, -1 * _qty, -1 * _qty1, "QTY_PRE", "QTY1_PRE");
                            }

                            return;
                        }
                        else
                        {
                            _db.UpdateQtyPre(osId, osNo, osItm, _qty, _qty1, "QTY_PRE", "QTY1_PRE");
                            if (isByAuditPass)
                            {
                                _db.UpdateQtyPre(osId, osNo, osItm, -1 * _qty, -1 * _qty1, "QTY_PRE_UNSH", "QTY1_PRE_UNSH");
                            }
                        }


                        //_db.UpdateQtyPre(osId, osNo, osItm, _qty, _qty1);
                        _db.GetOSbyPre(ref osId, ref osNo, ref osItm);
                        #endregion
                        _qty *= -1;
                    }
                    #region CK_ID=='CK'
                    if (!String.IsNullOrEmpty(osNo))
                    {
                        _ht = new Hashtable();
                        _ht["OsID"] = osId;
                        _ht["OsNO"] = osNo;
                        _ht["KeyItm"] = osItm;
                        switch (osId)
                        {
                            case "SO":
                                _ht["TableName"] = "TF_POS";
                                _ht["IdName"] = "OS_ID";
                                _ht["NoName"] = "OS_NO";
                                _ht["ItmName"] = "EST_ITM";
                                break;
                            case "LN":
                                _ht["TableName"] = "TF_BLN";
                                _ht["IdName"] = "BL_ID";
                                _ht["NoName"] = "BL_NO";
                                _ht["ItmName"] = "EST_ITM";
                                break;
                            case "SA":
                                _ht["TableName"] = "TF_PSS";
                                _ht["IdName"] = "PS_ID";
                                _ht["NoName"] = "PS_NO";
                                _ht["ItmName"] = "OTH_ITM";
                                break;
                        }
                        _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_unit), _ht);

                        string _qtyField = "QTY_CK";
                        if (osId == "SO")
                        {
                            _qtyField = "QTY_RK";
                        }

                        if (string.IsNullOrEmpty(_chkMan))
                        {
                            if (isByAuditPass)
                            {
                                _db.UpdateOS(osId, osNo, osItm, -1 * _qty, _qtyField);
                                _db.UpdateOS(osId, osNo, osItm, _qty, _qtyField + "_UNSH");
                            }
                            else
                            {
                                _db.UpdateOS(osId, osNo, osItm, _qty, _qtyField + "_UNSH");
                            }
                        }
                        else
                        {
                            _db.UpdateOS(osId, osNo, osItm, _qty, _qtyField);
                            if (isByAuditPass)
                            {
                                _db.UpdateOS(osId, osNo, osItm, -1 * _qty, _qtyField + "_UNSH");
                            }
                        }
                        //_db.UpdateOS(osId, osNo, osItm, _qty);
                    }
                    #endregion
                }

            }
        }

        #endregion

        #region ICloseBill Members

        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "SA_CLS_ID" || cls_name == "LZ_CLS_ID")
            {
                _error = this.CloseBill(bil_id, bil_no, cls_name, true);
            }
            return _error;
        }

        public string UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "SA_CLS_ID" || cls_name == "LZ_CLS_ID")
            {
                _error = this.CloseBill(bil_id, bil_no, cls_name, false);
            }
            return _error;
        }

        private string CloseBill(string osId, string osNo, string cls_name, bool close)
        {
            DbDRPCK _db = new DbDRPCK(Comp.Conn_DB);
            SunlikeDataSet _ds = _db.GetData(osId, osNo);
            DataTable _Mf = _ds.Tables["MF_CK"];
            DataTable _Tf = _ds.Tables["TF_CK"];
            if (_Mf.Rows.Count > 0)
            {
                DataRow _dr = _Mf.Rows[0];
                bool clsid = Convert.ToString(_dr[cls_name]) == "T";
                bool skipUnSh = !string.IsNullOrEmpty(Convert.ToString(_dr["CHK_MAN"]));//���Ѿ�����ĵ��ݣ����ֶ��᰸ʱ����Ҫ��������δ�����

                if (close && clsid)
                    return "RCID=COMMON.HINT.CLOSEERROR,PARAM=" + osNo;//�õ���[{0}]�ѽ᰸,�᰸�����������!
                if (!close && !clsid)
                    return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + osNo;//�õ���[{0}]δ�᰸,δ�᰸�����������!

                _db.DoCloseBill(osId, osNo, cls_name, close);
                if (CaseInsensitiveComparer.Default.Compare(cls_name, "SA_CLS_ID") == 0)
                {
                    foreach (DataRow _drTf in _Tf.Select())
                        UpdateWH(_drTf, close, true, skipUnSh);
                }
            }
            return "";
        }

        #endregion

        #region IAuditing Members

        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = String.Empty;
            try
            {
                DbDRPCK _db = new DbDRPCK(Comp.Conn_DB);
                _db.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
                SunlikeDataSet _ds = this.GetData("", chk_man, bil_id, bil_no);
                DataTable _mfTable = _ds.Tables["MF_CK"];
                DataTable _tfTable = _ds.Tables["TF_CK"];
                DataRow _mfdr = _mfTable.Rows[0];
                #region ��дԭ��
                foreach (DataRow dr in _tfTable.Rows)
                {
                    UpdateOS(dr, false, true);
                    UpdateWH(dr, false, true);
                }
                #endregion
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }

        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            return "";
        }

        private string RollBack(string bil_id, string bil_no, bool canChangeDS)
        {
            string _error = String.Empty;
            try
            {
                SunlikeDataSet _ds = this.GetData("", "", bil_id, bil_no);
                string _err = SetCanModify("", _ds, "", false);
                if (!string.IsNullOrEmpty(_err))
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;" + _err;

                DataTable _mfTable = _ds.Tables["MF_CK"];
                DataTable _tfTable = _ds.Tables["TF_CK"];
                DataRow _mfdr = _mfTable.Rows[0];

                #region ��дԭ��
                foreach (DataRow dr in _tfTable.Rows)
                {
                    UpdateOS(dr, true, true);
                    UpdateWH(dr, true, true);
                }
                #endregion

                if (canChangeDS)
                {
                    DbDRPCK _db = new DbDRPCK(Comp.Conn_DB);
                    _db.UpdateChkMan(bil_id, bil_no, "", DateTime.Now);
                }

            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }

        public string RollBack(string bil_id, string bil_no)
        {
            return RollBack(bil_id, bil_no, true);
        }

        #endregion
    }
}
