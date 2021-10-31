using System;
using System.Data;
using System.Configuration;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using System.Collections;
using Sunlike.Common.Utility;
using System.Collections.Generic;


namespace Sunlike.Business
{
    public class MRPBE : BizObject, IAuditing
    {
        #region ����
        private bool _isRunAuditing;
        private string m_usr;
        #endregion

        #region ȡ����
        /// <summary>
        /// ȡ����
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="bilno"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm, string usr, string bilno)
        {
            DbMRPBE _db = new DbMRPBE(Comp.Conn_DB);
            SunlikeDataSet _ds = _db.GetData(bilno);

            #region ���ݼӹ�
            //�Զ�������
            SetColAutoIncrement(_ds.Tables["TF_TW"].Columns["TS_ITM"]);
            SetColAutoIncrement(_ds.Tables["TF_TW"].Columns["EST_ITM"]);
            SetColAutoIncrement(_ds.Tables["TF_TW"].Columns["PRE_ITM"]);

            //��¼ԭ���ݵ�����
            #endregion

            if (!string.IsNullOrEmpty(usr))
            {
                Users _usrs = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_usrs.GetUserDepNo(usr)).DecimalDigitsInfo.System;
                this.SetCanModify(_ds, pgm, usr, true);
            }
            return _ds;
        }

        public string GetMoNo(string bilno)
        {
            string moNo = "";
            DbMRPBE _db = new DbMRPBE(Comp.Conn_DB);
            DataTable _dt = _db.GetData(bilno).Tables[0];
            if (_dt.Rows.Count > 0)
                moNo = _dt.Rows[0]["MO_NO"].ToString();
            return moNo;
        }
        //��ȡָ����MO��ת�뵽TW������
        public DataTable GetMoQty(string moNo)
        {
            DbMRPBE _db = new DbMRPBE(Comp.Conn_DB);
            return _db.GetMoQty(moNo).Tables[0];
        }

        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="_ds"></param>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="bubbleException"></param>
        /// <returns></returns>
        public DataTable UpdateData(SunlikeDataSet _ds, string pgm, string usr, bool bubbleException)
        {
            DataTable _dtErr = null;
            Auditing _auditing = new Auditing();

            m_usr = usr; //�������ж�Ȩ�޸��ݵ�ǰ��¼�û�

            #region �ж��Ƿ����������
            if (_ds.Tables.Contains("MF_TW"))
            {
                DataRow _dr = _ds.Tables["MF_TW"].Rows[0];
                string _bilID = "TW";
                string _usr = GetStrDrValue(_dr, "USR");
                string _bilType = GetStrDrValue(_dr, "BIL_TYPE");
                string _mobID = GetStrDrValue(_dr, "MOB_ID");
                //_isRunAuditing = _auditing.IsRunAuditing(_bilID, _usr, _bilType, _mobID);
            }
            #endregion

            Hashtable _ht = new Hashtable();
            _ht["MF_TW"] = @"TW_NO, TW_DD, BAT_NO, BIL_ID, BIL_NO, VOH_ID, VOH_NO, SEND_MTH, SEND_WH, EST_DD, CUS_NO, SAL_NO, DEP, CUR_ID, EXC_RTO, 
                            CLOSE_ID, TAX_ID, INV_NO, RP_NO, AMTN_NET, TAX, AMT, REM, ADR, MO_NO, ZC_NO, MRP_NO, PRD_MARK, WH, UNIT, QTY, UP, QTY_RTN, 
                            QTY_RTN_UNSH, QTY_LOST, QTY_LOST_UNSH, QTY_ML, QTY_ML_UNSH, AMT_INT, AMTN_NET_INT, TAX_INT, ML_NO, CONTRACT, CPY_SW, 
                            CUS_UP, CUS_DOWN, ML_OK, USR, CHK_MAN, PRT_SW, QTY_RK, QTY_RK_UNSH, ZC_ITM, FIN_DD, CHK_ID, MV_ID, QTY_PRC, QTY_MV, 
                            QTY_MV_UNSH, QTY_CHK, QTY_CHK_UNSH, ZC_NO_UP, ZC_NO_DN, QTY_WH, CLS_DATE, ID_NO, CST, WT_NO, QC_YN, MM_CURML, SO_NO, 
                            EST_ITM, QTY1, TS_ID, ISFIRST, QTY_TC, QTY_TC_UNSH, RTO_TAX, UP1, BIL_TYPE, CNTT_NO, MOB_ID, LOCK_MAN, FJ_NUM, SYS_DATE, CAS_NO, 
                            TASK_ID, OLD_ID, QTY_SL, QTY_SL_UNSH, SCM_USR, SCM_DD, CUS_OS_NO, PRT_USR, QTY_QL, QTY_QL_UNSH, QL_ID, Q2_ID, Q3_ID, QTY_DZ, 
                            ISSVS, PRT_NUM, ML_BY_MM, QTY_DM, QTY_DM_UNSH, CONTROL, PAY_DD, CHK_DD, PAY_MTH, PAY_DAYS, CHK_DAYS, INT_DAYS, PAY_REM, 
                            CLS_REM, ZC_REM, PO_OK, CF_ID, ZT_ID, ZT_DD, CV_ID, QTY_DZ_UNSH, CANCEL_ID, RTO_FQSK, DATEFLAG_FQSK, DATE_FQSK, QS_FQSK";
            _ht["TF_TW"] = @"TW_NO, ITM, PRD_NO, PRD_NAME, PRD_MARK, WH, UNIT, QTY, QTY_RTN, QTY_RTN_UNSH, BAT_NO, USEIN_NO, CPY_SW, REM, PRD_NO_CHG, 
                            QTY1, ID_NO, CST, WT_NO, QTY_TS, QTY_TS_UNSH, TS_ITM, CNTT_NO, COMPOSE_IDNO, EST_ITM, PRE_ITM, LOS_RTO, QTY_STD, ZC_PRD, 
                            QTY_RSV, QTY_LOST, CHG_RTO, CST_STD, CHG_ITM, QTY_CHG_RTO, QTY_QL, QTY_QL_UNSH, QTY1_QL, QTY1_QL_UNSH, QTY_DM, 
                            QTY_DM_UNSH, QTY1_DM, QTY1_DM_UNSH, QTY_BL, QTY_BL_UNSH";

            this.UpdateDataSet(_ds, _ht);
            //�жϵ����ܷ��޸�
            if (!_ds.HasErrors)
            {
                this.SetCanModify(_ds, pgm, usr, true);
            }
            else
            {
                _dtErr = GetAllErrors(_ds);
            }
            return _dtErr;
        }

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
            if (string.Compare(tableName, "MF_TW") == 0)
            {

                string bilID = "TW";
                string bilNO = GetStrDrValue(dr, "TW_NO");
                DateTime bilDD = Convert.ToDateTime(GetDrValue(dr, "TW_DD"));
                #region ����
                if (Comp.HasCloseBill(bilDD, GetStrDrValue(dr, "DEP"), "CLS_MNU"))
                {
                    throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                }
                #endregion

                if (statementType != StatementType.Delete)
                {
                    #region ������������Ϸ���


                    //��������ͻ�
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(m_usr, dr["CUS_NO"].ToString(), bilDD))
                    {
                        dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//�ͻ�����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //���ҵ��Ա
                    Salm _salm = new Salm();
                    if (!String.IsNullOrEmpty(dr["SAL_NO"].ToString()) && !_salm.IsExist(m_usr, dr["SAL_NO"].ToString(), bilDD))
                    {
                        dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//ҵ��Ա����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //��鲿��
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(m_usr, dr["DEP"].ToString(), bilDD))
                    {
                        dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEP_NOTEXIST,PARAM=" + dr["DEP"].ToString());//���Ŵ���[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    #endregion
                    if (statementType == StatementType.Insert)
                    {
                        #region ȡ����
                        dr["TW_NO"] = _sq.Set(bilID, m_usr, dr["DEP"].ToString(), bilDD, dr["BIL_TYPE"].ToString());
                        #endregion
                        #region MFĬ����λ
                        dr["PRT_SW"] = "N";
                        dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        SetDefaultValue(dr, new string[] { "CUR_OS_NO", "PRT_USR", "PRD_MARK" }, "");
                        SetDefaultValue(dr, new string[] { "ML_BY_MM", "MM_CURML", "CLOSE_ID", "CONTROL", "CF_ID", "QL_ID", "Q2_ID", "Q3_ID" }, "F");
                        if (!string.IsNullOrEmpty(dr["EST_DD"].ToString()))
                            dr["EST_DD"] = Convert.ToDateTime(dr["EST_DD"]).ToString(Comp.SQLDateFormat);
                        #endregion
                    }
                }
                else
                {
                    #region ɾ��SQNO,AUDITING
                    string _error = _sq.Delete(bilNO, dr["USR", DataRowVersion.Original].ToString());
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
                //    _aps.BIL_DD = bilDD;
                //    _aps.BIL_ID = bilID;
                //    _aps.BIL_NO = bilNO;
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    //_aps.MOB_ID = "";
                //}
                //else
                //    //_aps = new AudParamStruct(bilID, bilNO);
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //    throw new SunlikeException(_auditErr);
                //#endregion

                #region ���Ӱ��
                UpdateMfWH(dr, false);
                #endregion
            }
            #endregion

            #region TF
            if (string.Compare(tableName, "TF_TW") == 0)
            {
                if (statementType != StatementType.Delete)
                {
                    #region ������������Ϸ���
                    Prdt _prdt = new Prdt();
                    string _prd_no = dr["PRD_NO"].ToString();
                    DateTime twdd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_TW"].Rows[0]["TW_DD"]);
                    //����Ʒ����
                    if (!_prdt.IsExist(m_usr, _prd_no, twdd))
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
                        if (_mark.RunByPMark(m_usr))
                        {
                            string[] _aryMark = _mark.BreakPrdMark(_prdMark);
                            DataTable _dtMark = _mark.GetSplitData("");
                            //����������Ʒ����ΪA��B��C����������Ϊ��
                            bool _markCanNull = false;
                            DataTable _dtPrdt = _prdt.GetPrdt(_prd_no);
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
                    if (!SunlikeWH.IsExist(m_usr, _wh, twdd))
                    {
                        dr.SetColumnError("WH",/*�ֿ����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    #endregion

                    if (statementType == StatementType.Insert)
                    {
                        #region TFĬ����λ
                        SetDefaultValue(dr, new string[] { "ID_NO" }, "");
                        #endregion

                    }
                }
                #region ��дԭ����д����
                if (!string.IsNullOrEmpty(GetStrDrValue(dr.Table.DataSet.Tables[0].Rows[0], "MO_NO")))//��������Դ
                    UpdateMrpMO(dr);
                else
                    UpdateWH(dr, false);
                #endregion


            }
            #endregion

            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }

        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            if (string.Compare(tableName, "MF_TW") == 0)
            {
                #region ��д���BUILD_BIL�ֶ�
                UpdateMOBuild(dr);//ֻ��MF�й�ϵ,��������Ӱ��
                #endregion
            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        /// <summary>
        /// BeforeDsSave������׷��
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables["MF_TW"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "TW");
            //}
            //#endregion
            base.BeforeDsSave(ds);
        }


        #endregion

        #region ����Դ��

        #region ���
        private void UpdateMrpMO(DataRow _dr)
        {
            MRPMO _mrpMo = new MRPMO();

            DataRow _drMF = _dr.Table.DataSet.Tables[0].Rows[0];
            string zcNo = GetStrDrValue(_drMF, "ZC_NO");
            string _mmNo = GetStrDrValue(_drMF, "MO_NO");
            SunlikeDataSet _ds = _mrpMo.GetUpdateData(_mmNo, false);
            DataTable _tfdt = _ds.Tables["TF_MO"];


            if (_dr.RowState != DataRowState.Deleted)
            {
                string _filter = string.Format("PRD_NO='{0}' AND WH='{1}' AND ISNULL(PRD_MARK,'')='{2}' AND ISNULL(USEIN,'')='{3}' ", _dr["PRD_NO"], _dr["WH"], _dr["PRD_MARK"], _dr["USEIN_NO"]);
                if (_tfdt.Rows.Count > 0 && !string.IsNullOrEmpty(GetStrDrValue(_tfdt.Rows[0], "ZC_NO"))
                          && !string.IsNullOrEmpty(zcNo))//������Ƴ� and TW���Ƴ�
                {
                    _filter += " AND ZC_NO='" + zcNo + "'";
                }
                DataRow[] _drs = _tfdt.Select(_filter);
                if (_drs.Length == 0)
                {
                    #region �����������
                    DataRow _drTf = _tfdt.NewRow();
                    _drTf["MO_NO"] = _mmNo;
                    _drTf["ITM"] = _tfdt.Rows.Count + 1;
                    _drTf["USEIN"] = _dr["USEIN_NO"];
                    _drTf["TW_ID"] = "1";
                    _drTf["ZC_NO"] = zcNo;
                    List<string> li = new List<string>(new string[] { "PRD_NO", "PRD_MARK", "QTY_LOST", "QTY_RSV", "UNIT", "WH", "PRD_NAME" });
                    foreach (string col in li)
                    {
                        if (_dr.Table.Columns.Contains(col) && _tfdt.Columns.Contains(col))
                            _drTf[col] = _dr[col];
                    }
                    _tfdt.Rows.Add(_drTf);
                    _mrpMo.UpdateData("", _ds, true);
                    #endregion
                }
            }

        }
        #endregion

        #region ���BUILD_BIL�ֶ�
        private void UpdateMOBuild(DataRow _dr)
        {   //��tw���Ƴ���Ҫ��дMO��BUILD_BIL
            if (!string.IsNullOrEmpty(GetStrDrValue(_dr, "ZC_NO")))
            {
                DbMRPBE _db = new DbMRPBE(Comp.Conn_DB);
                string moNo1 = _dr.RowState == DataRowState.Modified ? GetStrDrValue(_dr, "MO_NO", true) : "";//ԭʼֵ;�޸�״̬��Ҫ����ԭMO��
                string moNo = GetStrDrValue(_dr, "MO_NO");

                if (!string.IsNullOrEmpty(moNo))
                    _db.UpdateMOBuild(moNo);
                if (!string.IsNullOrEmpty(moNo1))
                    _db.UpdateMOBuild(moNo1);
            }
        }
        #endregion

        #region �޸Ŀ��

        private void UpdateWH(DataRow dr, bool isFromIAuditing)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr.RowState == DataRowState.Modified)
                    UpdateWH(dr, true, isFromIAuditing);
                UpdateWH(dr, false, isFromIAuditing);
            }
            else
                UpdateWH(dr, true, isFromIAuditing);

        }

        private void UpdateWH(DataRow dr, bool isDel, bool isFromIAuditing)
        {
            UpdateWH(dr, isDel, isFromIAuditing, false);
        }

        private void UpdateWH(DataRow dr, bool isDel, bool isFromIAuditing, bool skipUNSH)
        {
            #region �����������ֵ
            string _ckid = GetStrDrValue(dr, "", isDel);
            string _prdNo = GetStrDrValue(dr, "PRD_NO", isDel);
            string _prdMark = GetStrDrValue(dr, "PRD_MARK", isDel);
            string _wh = GetStrDrValue(dr, "WH", isDel);
            string _validDd = GetStrDrValue(dr, "VALID_DD", isDel);
            string _batNo = GetStrDrValue(dr, "BAT_NO", isDel);
            string _unit = GetStrDrValue(dr, "UNIT", isDel);
            decimal _qty = Convert2Decimal(GetDrValue(dr, "QTY", isDel));
            DataRow _drMF = dr.Table.DataSet.Tables[0].Rows[0];
            string _chkMan = GetStrDrValue(_drMF, "CHK_MAN", isDel);
            if (isDel)
                _qty *= -1;
            #endregion
            WH wh = new WH();
            if (!string.IsNullOrEmpty(_chkMan) || isFromIAuditing) //�����ͨ�����߲���Ҫ���������
            {
                #region �޸Ŀ��
                Prdt _prdt = new Prdt();
                if (String.IsNullOrEmpty(_batNo))//������
                {
                    wh.UpdateQty(_prdNo, _prdMark, _wh, _unit, WH.QtyTypes.QTY_ON_RSV, _qty);
                }
                else//���ſع�
                {
                    SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _wh);
                    Hashtable _ht = new Hashtable();
                    _ht[WH.QtyTypes.QTY_ON_RSV] = _qty;
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
                if (string.IsNullOrEmpty(_chkMan) || isFromIAuditing) //��Ҫ��������̻��߱����ͨ��
                {
                    if (isFromIAuditing)
                        _qty *= -1;

                    #region �޸Ŀ��
                    //Hashtable _ht = new Hashtable();
                    //_ht[WH.QtyTypes.QTY_ON_RSV] = _qty;
                    //wh.UpdateShQty(_batNo, _prdNo, _prdMark, _wh, _unit, _ht);
                    #endregion
                }
            }
        }

        private void UpdateMfWH(DataRow dr, bool isFromIAuditing)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr.RowState == DataRowState.Modified)
                    UpdateMfWH(dr, true, isFromIAuditing);
                UpdateMfWH(dr, false, isFromIAuditing);
            }
            else
                UpdateMfWH(dr, true, isFromIAuditing);

        }

        private void UpdateMfWH(DataRow dr, bool isDel, bool isFromIAuditing)
        {
            UpdateMfWH(dr, isDel, isFromIAuditing, false);
        }

        private void UpdateMfWH(DataRow dr, bool isDel, bool isFromIAuditing, bool skipUNSH)
        {
            #region �����������ֵ
            string _ckid = GetStrDrValue(dr, "", isDel);
            string _prdNo = GetStrDrValue(dr, "MRP_NO", isDel);
            string _prdMark = GetStrDrValue(dr, "PRD_MARK", isDel);
            string _wh = GetStrDrValue(dr, "WH", isDel);
            string _validDd = GetStrDrValue(dr, "VALID_DD", isDel);
            string _batNo = GetStrDrValue(dr, "BAT_NO", isDel);
            string _unit = GetStrDrValue(dr, "UNIT", isDel);
            decimal _qty = Convert2Decimal(GetDrValue(dr, "QTY", isDel));
            string _chkMan = GetStrDrValue(dr, "CHK_MAN", isDel);
            if (isDel)
                _qty *= -1;
            #endregion
            WH wh = new WH();
            if (!string.IsNullOrEmpty(_chkMan) || isFromIAuditing) //�����ͨ�����߲���Ҫ���������
            {
                #region �޸Ŀ��
                Prdt _prdt = new Prdt();
                if (String.IsNullOrEmpty(_batNo))//������
                {
                    wh.UpdateQty(_prdNo, _prdMark, _wh, _unit, WH.QtyTypes.QTY_ON_PRC, _qty);
                }
                else//���ſع�
                {
                    SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _wh);
                    Hashtable _ht = new Hashtable();
                    _ht[WH.QtyTypes.QTY_ON_PRC] = _qty;
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
                if (string.IsNullOrEmpty(_chkMan) || isFromIAuditing) //��Ҫ��������̻��߱����ͨ��
                {
                    if (isFromIAuditing)
                        _qty *= -1;

                    #region �޸Ŀ��
                    //Hashtable _ht = new Hashtable();
                    //_ht[WH.QtyTypes.QTY_ON_PRC] = _qty;
                    //wh.UpdateShQty(_batNo, _prdNo, _prdMark, _wh, _unit, _ht);
                    #endregion
                }
            }
        }

        #endregion

        #endregion

        #region Function
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ds">ds</param>
        /// <param name="pgm">pgm</param>
        /// <param name="_usr">�û�</param>
        /// <param name="isCheckAuditing">��鵥���ѽ����������</param>
        /// <returns>��Ϊ"",���ܷ����</returns>
        private string SetCanModify(SunlikeDataSet _ds, string pgm, string _usr, bool isCheckAuditing)
        {
            string _err = "";
            Auditing _auditing = new Auditing();
            if (_ds.Tables.Contains("MF_TW") && _ds.Tables["MF_TW"].Rows.Count > 0)
            {
                DataRow _drMf = _ds.Tables["MF_TW"].Rows[0];
                DataTable _dtTf = _ds.Tables["TF_TW"];
                string bilID = "TW";
                string bilNO = Convert.ToString(_drMf["TW_NO"]);

                #region ����Ȩ�޿ع�
                if (!string.IsNullOrEmpty(_usr))
                {
                    Hashtable _billRight = Users.GetBillRight(pgm, _usr, _drMf["DEP"].ToString(), _drMf["USR"].ToString());
                    _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _ds.ExtendedProperties["LCK"] = _billRight["LCK"];

                    #region ����CanModify
                    bool _canModify = true;//�����˵����Ƿ���޸�     
                    //����
                    if (Comp.HasCloseBill(Convert.ToDateTime(_drMf["TW_DD"]), _drMf["DEP"].ToString(), "CLS_MNU"))
                    {
                        _err = "RCID=COMMON.HINT.CLOSE_CLS";
                        ////Common.SetCanModifyRem(_ds, _err);
                        _canModify = false;
                    }
                    //�����������
                    if (isCheckAuditing && _auditing.GetIfEnterAuditing(bilID, bilNO))
                    {
                        _err = "RCID=COMMON.HINT.CLOSE_AUDIT";
                        ////Common.SetCanModifyRem(_ds, _err);
                        _canModify = false;
                    }
                    //����
                    if (!String.IsNullOrEmpty(_drMf["LOCK_MAN"].ToString()))
                    {
                        _err = "RCID=COMMON.HINT.CLOSE_LOCK";
                        ////Common.SetCanModifyRem(_ds, _err);
                        _canModify = false;
                    }
                    //�����ϵ�
                    if (!String.IsNullOrEmpty(_drMf["ML_NO"].ToString()))
                    {
                        _err = "RCID=COMMON.HINT.ML_NONULL";
                        ////Common.SetCanModifyRem(_ds, _err);
                        _canModify = false;
                    }
                    //������
                    if (Convert2Decimal(_drMf["QTY_ML"]) > 0)
                    {
                        _err = "RCID=COMMON.HINT.ML_NONULL";
                        ////Common.SetCanModifyRem(_ds, _err);
                        _canModify = false;
                    }
                    //�нɿ�
                    if (Convert2Decimal(_drMf["QTY_RTN"]) > 0)
                    {
                        _ds.ExtendedProperties["DEL"] = "N";
                        if (Convert2Decimal(_drMf["QTY_RTN"]) >= Convert2Decimal(_drMf["QTY"]))
                        {
                            _err = "RCID=MRP.MF_TW.QTY_RTNFULL";
                            ////Common.SetCanModifyRem(_ds, _err);
                            _canModify = false;
                        }
                    }
                    //��ȷ�Ͻ���
                    if (!string.IsNullOrEmpty(_drMf["SCM_USR"].ToString()))
                    {
                        _err = "RCID=MRP.MF_TW.SCM_USROK";
                        ////Common.SetCanModifyRem(_ds, _err);
                        _canModify = false;

                    }
                    //�Ѿ��᰸
                    if (String.Compare("T", _drMf["CLOSE_ID"].ToString()) == 0 && Convert2Decimal(_drMf["QTY"]) + Convert2Decimal(_drMf["QTY_LOST"]) == Convert2Decimal(_drMf["QTY_RTN"]))
                    {
                        _err = "RCID=COMMON.HINT.CLOSE_ID";
                        //Common.SetCanModifyRem(_ds, _err);
                        _canModify = false;
                    }

                    _ds.ExtendedProperties["CAN_MODIFY"] = _canModify ? "T" : "F";
                    #endregion
                }
                #endregion
            }
            return _err;
        }

        private void SetColAutoIncrement(DataColumn _dc)
        {
            _dc.AutoIncrement = true;
            _dc.AutoIncrementSeed = _dc.Table.Rows.Count > 0 ? Convert2Int(_dc.Table.Select("", _dc.ColumnName + " desc")[0][_dc.ColumnName]) + 1 : 1;
            _dc.AutoIncrementStep = 1;
            _dc.Unique = true;
        }

        private decimal Convert2Decimal(object obj)
        {
            decimal _d = 0;
            if (!decimal.TryParse(Convert.ToString(obj), out _d))
                _d = 0;
            return _d;
        }

        private int Convert2Int(object obj)
        {
            int _d = 0;
            if (!int.TryParse(Convert.ToString(obj), out _d))
                _d = 0;
            return _d;
        }

        private object GetDrValue(DataRow _dr, string field)
        {
            if (_dr != null && _dr.Table.Columns.Contains(field))
                return _dr.RowState == DataRowState.Deleted ? _dr[field, DataRowVersion.Original] : _dr[field];
            return null;
        }

        private string GetStrDrValue(DataRow _dr, string field)
        {
            return Convert.ToString(GetDrValue(_dr, field));
        }

        private object GetDrValue(DataRow _dr, string field, bool Original)
        {
            if (_dr != null && _dr.Table.Columns.Contains(field))
                return Original ? _dr[field, DataRowVersion.Original] : _dr[field];
            return DBNull.Value;
        }

        private string GetStrDrValue(DataRow _dr, string field, bool Original)
        {
            return Convert.ToString(GetDrValue(_dr, field, Original));
        }

        private void SetDefaultValue(DataRow _dr, string[] fields, object defauleValue)
        {
            foreach (string col in fields)
            {
                if (_dr != null && _dr.Table.Columns.Contains(col) && string.IsNullOrEmpty(Convert.ToString(_dr[col])))
                    _dr[col] = defauleValue;
            }
        }

        #endregion

        #region IAuditing Members

        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = String.Empty;
            try
            {
                DbMRPBE _db = new DbMRPBE(Comp.Conn_DB);
                _db.UpdateChkMan(bil_no, chk_man, cls_dd);
                SunlikeDataSet _ds = this.GetData("", "", bil_no);
                DataTable _mfTable = _ds.Tables["MF_TW"];
                DataTable _tfTable = _ds.Tables["TF_TW"];
                DataRow _mfdr = _mfTable.Rows[0];

                #region ���Ӱ��
                UpdateMfWH(_mfdr, false, true);
                #endregion

                #region ��дԭ��
                foreach (DataRow dr in _tfTable.Rows)
                {
                    #region ��дԭ����д����
                    if (string.IsNullOrEmpty(GetStrDrValue(_mfdr, "MO_NO")))//��������Դ
                        UpdateWH(dr, false, true);
                    #endregion
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

        public string RollBack(string bil_id, string bil_no)
        {
            string _error = String.Empty;
            try
            {
                SunlikeDataSet _ds = GetData("", "", bil_no);
                string _err = SetCanModify(_ds, "", "", false);
                if (!string.IsNullOrEmpty(_err))
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;" + _err;

                DataTable _mfTable = _ds.Tables["MF_TW"];
                DataTable _tfTable = _ds.Tables["TF_TW"];
                DataRow _mfdr = _mfTable.Rows[0];

                #region ���Ӱ��
                UpdateMfWH(_mfdr, true, true);
                #endregion

                #region ��дԭ��
                foreach (DataRow dr in _tfTable.Rows)
                {
                    #region ��дԭ����д����
                    if (string.IsNullOrEmpty(GetStrDrValue(_mfdr, "MO_NO")))//��������Դ
                        UpdateWH(dr, true, true);
                    #endregion
                }
                #endregion

                DbMRPBE _db = new DbMRPBE(Comp.Conn_DB);
                _db.UpdateChkMan(bil_no, "", DateTime.Now);

            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }

        #endregion


    }
}
