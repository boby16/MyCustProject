using System;
using System.Data;
using System.Collections;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
    public class MTNRcv : BizObject, IAuditing, ICloseBill
    {
        #region Variablue
        private bool _isRunAuditing;
        private string _usr = String.Empty;
        #endregion

        #region ����
        /// <summary>
        /// ά�޲�Ʒ�ռ���
        /// </summary>
        public MTNRcv()
        {
        }
        #endregion

        #region ȡ��ά�޲�Ʒ�ռ���
        /// <summary>
        /// ȡ��ά�޲�Ʒ�ռ���
        /// </summary>
        /// <param name="usr">�Ƶ���</param>
        /// <param name="pgm">PGM</param>
        /// <param name="rv_Id">�ռ�/�ռ��˻ص�</param>
        /// <param name="rv_No">����</param>
        /// <param name="onlyFillSchema">�Ƿ�ֻ��ȡSchema</param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string usr, string pgm, string rv_Id, string rv_No, bool onlyFillSchema)
        {
            DbMTNRcv _dbMTNRcv = new DbMTNRcv(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMTNRcv.GetData(rv_Id, rv_No, onlyFillSchema);

            if (!string.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //ԭ�ռ�������(�˻ص���)
            DataColumn old_qty_so = new DataColumn("OLD_QTY_SO", Type.GetType("System.Decimal"));
            _ds.Tables["TF_RCV"].Columns.Add(old_qty_so);

            //ά�����뵥���ռ���(�ռ�����)
            _ds.Tables["TF_RCV"].Columns["QTY_RV_ORG"].ReadOnly = false;
            //�Զ�����
            _ds.Tables["TF_RCV"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_RCV"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_RCV"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_RCV"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_RCV"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_RCV"].Columns["KEY_ITM"].Unique = true;
            // �жϵ����Ƿ�����޸�
            this.SetCanModify(usr, pgm, _ds, true);
            return _ds;
        }
        #endregion

        #region SetCanModify
        /// <summary>
        /// �Ƿ���Ը���
        /// </summary>
        /// <param name="usr">��ǰ�û�</param>
        /// <param name="pgm">PGM</param>
        /// <param name="ds">DataSet</param>
        /// <param name="chkAuditing">�Ƿ����ж��������</param>
        /// <returns></returns>
        private string SetCanModify(string usr, string pgm, SunlikeDataSet ds, bool chkAuditing)
        {
            bool _bCanModify = true;
            string _errMsg = "";
            if (!string.IsNullOrEmpty(usr))
            {
                //����Ȩ��
                string _billDep = "";
                string _billUsr = "";
                string _MaId = "";
                if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_RCV") && ds.Tables["MF_RCV"].Rows.Count > 0)
                {
                    _billDep = ds.Tables["MF_RCV"].Rows[0]["DEP_RCV"].ToString();
                    _billUsr = ds.Tables["MF_RCV"].Rows[0]["USR"].ToString();
                    _MaId = ds.Tables["MF_RCV"].Rows[0]["RV_ID"].ToString();
                }
                Hashtable _right = Users.GetBillRight(pgm, usr, _billDep, _billUsr);
                ds.ExtendedProperties["UPD"] = _right["UPD"];
                ds.ExtendedProperties["DEL"] = _right["DEL"];
                ds.ExtendedProperties["PRN"] = _right["PRN"];
                ds.ExtendedProperties["LCK"] = _right["LCK"];
                Users _users = new Users();
                ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //�жϹ�����
            if (ds.Tables["MF_RCV"].Rows.Count > 0)
            {
                if (Comp.HasCloseBill(Convert.ToDateTime(ds.Tables["MF_RCV"].Rows[0]["RV_DD"]), ds.Tables["MF_RCV"].Rows[0]["DEP_RCV"].ToString(), "CLS_MNU"))
                {
                    _bCanModify = false;
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                }
            }
            //�ж��������
            if (chkAuditing)
            {
                if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_RCV") && ds.Tables["MF_RCV"].Rows.Count > 0)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(ds.Tables["MF_RCV"].Rows[0]["RV_ID"].ToString(), ds.Tables["MF_RCV"].Rows[0]["RV_NO"].ToString()))
                    {
                        _bCanModify = false;
                        _errMsg += "COMMON.HINT.CLOSE_AUDIT";//�����Ѿ������������!
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
            }
            //�ж��Ƿ�᰸

            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_RCV") && ds.Tables["MF_RCV"].Rows.Count > 0)
            {
                if (ds.Tables["MF_RCV"].Rows[0]["CLS_ID"].ToString() == "T"
                    && ds.Tables["MF_RCV"].Rows[0]["CLS_AUTO"].ToString() == "T")
                {
                    _bCanModify = false;
                    _errMsg += "COMMON.HINT.CLOSE_MODIFY";//�ѽ᰸
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");

                }
            }

            //�ж��Ƿ�����

            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_RCV") && ds.Tables["MF_RCV"].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(ds.Tables["MF_RCV"].Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    _errMsg += "COMMON.HINT.CLOSE_LOCK";//�ѽ᰸
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
            }

            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return _errMsg;
        }
        #endregion

        #region ȡ���ռ�����������
        /// <summary>
        /// ȡ���ռ�����������
        /// </summary>
        /// <param name="rvId">���ݱ�</param>
        /// <param name="rvNo">����</param>
        /// <param name="key_itm">����Ψһ���</param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string rvId, string rvNo, int key_itm)
        {
            DbMTNRcv _dbRcv = new DbMTNRcv(Comp.Conn_DB);
            return _dbRcv.GetBody(rvId, rvNo, key_itm);
        }
        #endregion

        #region ȡ���뵥����
        /// <summary>
        /// ȡ���뵥����
        /// </summary>
        /// <param name="maId">���ݱ�</param>
        /// <param name="maNo">���뵥��</param>
        /// <param name="key_itm">����Ψһ���</param>
        /// <returns></returns>
        public SunlikeDataSet GetMaQty(string maId, string maNo, int key_itm)
        {
            DbMTNRcv _dbRcv = new DbMTNRcv(Comp.Conn_DB);
            return _dbRcv.GetMaQty(maId, maNo, key_itm);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="changeDs"></param>
        /// <returns></returns>
        public DataTable UpdateData(SunlikeDataSet changeDs)
        {
            if (changeDs.Tables.Contains("MF_RCV") && changeDs.Tables["MF_RCV"].Rows.Count > 0)
            {
                string _rvId = string.Empty;
                string _rvNo = string.Empty;
                if (changeDs.Tables["MF_RCV"].Rows[0].RowState == DataRowState.Deleted)
                {
                    _usr = changeDs.Tables["MF_RCV"].Rows[0]["USR", DataRowVersion.Original].ToString();
                    _rvId = changeDs.Tables["MF_RCV"].Rows[0]["RV_ID", DataRowVersion.Original].ToString();
                    _rvNo = changeDs.Tables["MF_RCV"].Rows[0]["RV_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = changeDs.Tables["MF_RCV"].Rows[0]["USR"].ToString();
                    _rvId = changeDs.Tables["MF_RCV"].Rows[0]["RV_ID"].ToString();
                    _rvNo = changeDs.Tables["MF_RCV"].Rows[0]["RV_NO"].ToString();
                }

                #region ȡ�õ��ݵ����״̬

                Auditing _auditing = new Auditing();
                DataRow _dr = changeDs.Tables["MF_RCV"].Rows[0];
                string _bilType = "";
                string _mobID = "";//֧��ֱ������mobID=@@ �򵥾ݲ����������
                if (_dr.RowState == DataRowState.Deleted)
                {
                    if (_dr.Table.Columns.Contains("BIL_TYPE"))
                        _bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
                    if (_dr.Table.Columns.Contains("MOB_ID"))
                        _mobID = _dr["MOB_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    if (_dr.Table.Columns.Contains("BIL_TYPE"))
                        _bilType = _dr["BIL_TYPE"].ToString();
                    if (_dr.Table.Columns.Contains("MOB_ID"))
                        _mobID = _dr["MOB_ID"].ToString();
                }
                //_isRunAuditing = _auditing.IsRunAuditing(_rvId, _usr, _bilType, _mobID);



                #endregion
            }

            DataTable _dtErr = null;
            Hashtable _ht = new Hashtable();
            _ht["MF_RCV"] = "RV_ID,RV_NO,RV_DD,DEP_RCV,CUS_NO,BIL_TYPE,SAL_NO,DEP_SEND,RCV_TYPE,IJ_ID,IJ_NO,REM,CLS_ID,USR,SYS_DATE,CHK_MAN,CLS_DATE,MOB_ID,LOCK_MAN,PRT_SW,PRT_USR,CPY_SW,MA_ID,MA_NO,CNT_NO,CNT_NAME,TEL_NO,CELL_NO,CNT_ADR,OTH_NAME,CNT_REM,CLS_AUTO,EST_DD";
            _ht["TF_RCV"] = "RV_ID,RV_NO,ITM,KEY_ITM,PRD_NO,PRD_NAME,UNIT,WH,PRD_MARK,WC_NO,QTY,MTN_DD,MTN_ALL_ID,RTN_DD,REM,MA_ID,MA_NO,MA_ITM,BAT_NO,VALID_DD,SA_NO,SA_ITM,QTY_SO,QTY_RR,MTN_TYPE,EST_DD";
            this.UpdateDataSet(changeDs, _ht);
            if (!changeDs.HasErrors)
            {
                if (changeDs.ExtendedProperties.Contains("UPD_USR"))
                    _usr = changeDs.ExtendedProperties["UPD_USR"].ToString();
                SetCanModify(_usr, "MTNRcv", changeDs, true);
            }
            _dtErr = GetAllErrors(changeDs);
            return _dtErr;
        }

        /// <summary>
        /// ���浥��֮ǰ
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            #region �ж��Ƿ�����
            string _rvNo = "", _rvId = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _rvNo = dr["RV_NO", DataRowVersion.Original].ToString();
                    _rvId = dr["RV_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    _rvNo = dr["RV_NO"].ToString();
                    _rvId = dr["RV_ID"].ToString();
                }
                //�ж��Ƿ�����������Ѿ����������޸ġ�
                Users _Users = new Users();
                string _whereStr = "RV_ID = '" + _rvId + "' AND RV_NO = '" + _rvNo + "'";
                if (_Users.IsLocked("MF_RCV", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            switch (tableName)
            {
                case "MF_RCV":
                    {
                        #region MF_RCV

                        #region ����
                        if (statementType != StatementType.Delete)
                        {
                            if (Comp.HasCloseBill(Convert.ToDateTime(dr["RV_DD"]), dr["DEP_RCV"].ToString(), "CLS_MNU"))
                            {
                                throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                            }
                        }
                        else
                        {
                            if (Comp.HasCloseBill(Convert.ToDateTime(dr["RV_DD", DataRowVersion.Original]), dr["DEP_RCV", DataRowVersion.Original].ToString(), "CLS_MNU"))
                            {
                                throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                            }
                        }
                        #endregion

                        SQNO _sq = new SQNO();
                        if (statementType != StatementType.Delete)
                        {
                            #region ����ǰ������ݵ�������

                            // ����ռ�����
                            Dept _dept = new Dept();
                            if (!_dept.IsExist(_usr, dr["DEP_RCV"].ToString(), Convert.ToDateTime(dr["RV_DD"])))
                            {
                                dr.SetColumnError("DEP_RCV", "RCID=COMMON.HINT.DEP_NOTEXIST,PARAM=" + dr["DEP_RCV"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            // ����ͼ�����
                            if (!_dept.IsExist(_usr, dr["DEP_SEND"].ToString(), Convert.ToDateTime(dr["RV_DD"])))
                            {
                                dr.SetColumnError("DEP_SEND", "RCID=COMMON.HINT.DEP_NOTEXIST,PARAM=" + dr["DEP_SEND"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            // ���ͻ�����
                            Cust _cust = new Cust();
                            if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString(), Convert.ToDateTime(dr["RV_DD"])))
                            {
                                dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            // ��龭����
                            Salm _salm = new Salm();
                            if (!_salm.IsExist(_usr, dr["SAL_NO"].ToString(), Convert.ToDateTime(dr["RV_DD"])))
                            {
                                dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }

                            #endregion

                            if (statementType == StatementType.Insert)
                            {
                                //ȡ�ñ��浥��
                                dr["RV_NO"] = _sq.Set(dr["RV_ID"].ToString(), _usr, dr["DEP_RCV"].ToString(), Convert.ToDateTime(dr["RV_DD"]), dr["BIL_TYPE"].ToString());
                                //д��Ĭ����λֵ
                                dr["PRT_SW"] = "N";
                                dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            }
                            if (!string.IsNullOrEmpty(dr["EST_DD"].ToString()))
                                dr["EST_DD"] = Convert.ToDateTime(dr["EST_DD"]).ToString(Comp.SQLDateFormat);
                        }
                        else
                        {
                            string _error = _sq.Delete(dr["RV_NO", DataRowVersion.Original].ToString(), _usr);
                            if (!string.IsNullOrEmpty(_error))
                                throw new SunlikeException("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//�޷�ɾ�����ţ�ԭ��{0}
                        }
                        if (dr.Table.DataSet.ExtendedProperties.Contains("IJADDED"))
                        {
                            dr.Table.DataSet.ExtendedProperties.Remove("IJADDED");
                        }
                        if (statementType == StatementType.Insert)
                        {
                            if (!_isRunAuditing)
                            {
                                #region ���µ���������������ʱ��

                                DataSet ds = dr.Table.DataSet;
                                //���ɵ�����
                                this.UpdateDRPIJ(ds);
                                dr.Table.DataSet.ExtendedProperties["IJADDED"] = "T";

                                #endregion
                            }
                        }
                        if (statementType == StatementType.Delete)
                        {
                            //ɾ��������
                            this.UpdateDRPIJ(dr.Table.DataSet);
                        }

                        #endregion

                        //#region ��˹���
                        //AudParamStruct _aps;
                        //if (statementType != StatementType.Delete)
                        //{
                        //    _aps.BIL_DD = DateTime.Now;
                        //    _aps.BIL_ID = dr["RV_ID"].ToString();
                        //    _aps.BIL_NO = dr["RV_NO"].ToString();
                        //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                        //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                        //    _aps.DEP = dr["DEP_RCV"].ToString();
                        //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                        //    _aps.USR = dr["USR"].ToString();
                        //    _aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
                        //}
                        //else
                        //    _aps = new AudParamStruct(Convert.ToString(dr["RV_ID", DataRowVersion.Original]), Convert.ToString(dr["RV_NO", DataRowVersion.Original]));
                        //Auditing _auditing = new Auditing();
                        //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                        //if (!string.IsNullOrEmpty(_auditErr))
                        //{
                        //    throw new SunlikeException(_auditErr);
                        //}
                        //#endregion
                    }
                    break;
                case "TF_RCV":
                    {
                        #region TF_RCV
                        DbMTNRcv _db = new DbMTNRcv(Comp.Conn_DB);
                        if (statementType != StatementType.Delete)
                        {
                            #region
                            //����Ʒ����
                            Prdt _prdt = new Prdt();
                            if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_RCV"].Rows[0]["RV_DD"])))
                            {
                                dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + dr["PRD_NO"].ToString());//��Ʒ����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����

                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            //��������ֶ�
                            string _prdMark = dr["PRD_MARK"].ToString();
                            int _prdMod = _prdt.CheckPrdtMod(dr["PRD_NO"].ToString(), _prdMark);
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
                                    for (int i = 0; i < _dtMark.Rows.Count; i++)
                                    {
                                        string _fldName = _dtMark.Rows[i]["FLDNAME"].ToString();
                                        if (!_mark.IsExist(_fldName, dr["PRD_NO"].ToString(), _aryMark[i]))
                                        {
                                            dr.SetColumnError(_fldName, "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _aryMark[i].Trim());//��Ʒ����[{0}]������

                                            status = UpdateStatus.SkipAllRemainingRows;
                                        }
                                    }
                                }
                            }
                            if (dr["PRD_MARK"] == System.DBNull.Value)
                            {
                                dr["PRD_MARK"] = "";
                            }
                            // ����λ
                            WH _wh = new WH();
                            if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_RCV"].Rows[0]["RV_DD"])))
                            {
                                dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            #region ��дά�����뵥

                            if (dr["RV_ID"].ToString() == "RV")
                            {
                                if (!_isRunAuditing)
                                {
                                    UpdateMA(dr, true);
                                    if (statementType == StatementType.Update)
                                    {
                                        UpdateMA(dr, false);
                                    }
                                }
                            }

                            #endregion

                            #region ����ֵ��ʽ��

                            if (!string.IsNullOrEmpty(dr["MTN_DD"].ToString()))
                                dr["MTN_DD"] = Convert.ToDateTime(dr["MTN_DD"]).ToString(Comp.SQLDateFormat);
                            if (!string.IsNullOrEmpty(dr["RTN_DD"].ToString()))
                                dr["RTN_DD"] = Convert.ToDateTime(dr["RTN_DD"]).ToString(Comp.SQLDateFormat);
                            if (!string.IsNullOrEmpty(dr["EST_DD"].ToString()))
                                dr["EST_DD"] = Convert.ToDateTime(dr["EST_DD"]).ToString(Comp.SQLDateFormat);

                            #endregion

                            #region ��д���޿�[��Ʒ��״]

                            if (dr["RV_ID"].ToString() == "RV")
                            {
                                //�ռ���
                                if (statementType == StatementType.Insert)
                                {
                                    if (dr["WC_NO"].ToString() != "" && !_isRunAuditing)
                                    {
                                        _db.UpdateWC(dr["WC_NO"].ToString(), "1");//[��Ʒ��״]���ռ�����
                                    }
                                }
                                else
                                {
                                    if (dr.HasVersion(DataRowVersion.Original) && dr["WC_NO", DataRowVersion.Original].ToString() != "" && dr["WC_NO", DataRowVersion.Original].ToString() != dr["WC_NO"].ToString() && !_isRunAuditing)
                                    {
                                        _db.UpdateWC(dr["WC_NO", DataRowVersion.Original].ToString(), "0");//[��Ʒ��״]����Ʒ����
                                    }
                                }
                            }
                            if (dr["RV_ID"].ToString() == "RR")
                            {
                                //�ռ��˻ص�
                                if (statementType == StatementType.Insert)
                                {
                                    if (dr["WC_NO"].ToString() != "" && !_isRunAuditing)
                                    {
                                        _db.UpdateWC(dr["WC_NO"].ToString(), "2");//[��Ʒ��״]����Ʒ�˼�
                                    }
                                }
                                else
                                {
                                    if (dr.HasVersion(DataRowVersion.Original) && dr["WC_NO", DataRowVersion.Original].ToString() != "" && dr["WC_NO", DataRowVersion.Original].ToString() != dr["WC_NO"].ToString() && !_isRunAuditing)
                                    {
                                        _db.UpdateWC(dr["WC_NO", DataRowVersion.Original].ToString(), "1");//[��Ʒ��״]���ռ�����
                                    }
                                }
                            }

                            #endregion

                            #endregion

                            #region ���޿�ά�޼�¼
                            if (statementType == StatementType.Update)
                                UpdateWCH(dr, true, false);
                            UpdateWCH(dr, false, false);
                            #endregion

                        }
                        else
                        {
                            if (dr["RV_ID", DataRowVersion.Original].ToString() == "RV")
                            {
                                #region ��дά�����뵥

                                if (!_isRunAuditing)
                                {
                                    UpdateMA(dr, false);
                                }

                                #endregion
                            }

                            #region ��д���޿�[��Ʒ��״]

                            if (dr["RV_ID", DataRowVersion.Original].ToString() == "RV" && dr["WC_NO", DataRowVersion.Original].ToString() != "")
                            {
                                if (!_isRunAuditing)
                                {
                                    _db.UpdateWC(dr["WC_NO", DataRowVersion.Original].ToString(), "0");
                                }
                            }
                            if (dr["RV_ID", DataRowVersion.Original].ToString() == "RR" && dr["WC_NO", DataRowVersion.Original].ToString() != "")
                            {
                                if (!_isRunAuditing)
                                {
                                    _db.UpdateWC(dr["WC_NO", DataRowVersion.Original].ToString(), "1");
                                }
                            }

                            #endregion

                            #region ���޿�ά�޼�¼
                            UpdateWCH(dr, true, false);
                            #endregion
                        }

                        #endregion
                    }
                    break;
            }
        }
        /// <summary>
        /// ���浥��֮��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            if (tableName == "TF_RCV")
            {
                if (!_isRunAuditing)
                {
                    #region ��д�˻���

                    if (statementType == StatementType.Insert)
                    {
                        if (dr["RV_ID"].ToString() == "RR")
                        {
                            UpdateQtyRtn(dr, true);
                        }
                    }
                    if (statementType == StatementType.Update)
                    {
                        if (dr["RV_ID"].ToString() == "RR")
                        {
                            UpdateQtyRtn(dr, false);
                            UpdateQtyRtn(dr, true);
                        }
                    }
                    if (statementType == StatementType.Delete)
                    {
                        if (dr["RV_ID", DataRowVersion.Original].ToString() == "RR")
                        {
                            UpdateQtyRtn(dr, false);
                        }
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// ������ȫ�����
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            if (ds.Tables["MF_RCV"].Rows.Count > 0)
            {
                if (!_isRunAuditing)
                {
                    #region ���µ����� (�޸ĵ���ʱ)
                    if (!ds.ExtendedProperties.Contains("IJADDED") || (ds.ExtendedProperties.Contains("IJADDED") && ds.ExtendedProperties["IJADDED"].ToString() != "T"))
                    {
                        DataRow _dr = ds.Tables["MF_RCV"].Rows[0];
                        DbMTNRcv _dbMTNRcv = new DbMTNRcv(Comp.Conn_DB);
                        string rvId = _dr["RV_ID"].ToString();
                        string rvNo = _dr["RV_NO"].ToString();
                        //dsֻ�������ĵ�����,Ҫ����ȡ����
                        SunlikeDataSet _ds = _dbMTNRcv.GetData(rvId, rvNo, false);
                        //_ds�е�ƾ֤ģ��ʹ����Ǿ�ֵ������Ҫ������ֵ
                        _ds.Tables["MF_RCV"].Rows[0]["VOH_ID"] = _dr["VOH_ID"];
                        _ds.Tables["MF_RCV"].Rows[0]["VOH_NO"] = _dr["VOH_NO"];
                        _ds.AcceptChanges();
                        this.UpdateDRPIJ((DataSet)_ds);

                        //�����ռ�����[�������ݱ�]��[��������]�ֶ�
                        string _ij_No = _ds.Tables["MF_RCV"].Rows[0]["IJ_NO"].ToString();
                        _dbMTNRcv.UpdateIJ_NO(rvId, rvNo, "IJ", _ij_No);
                        ds.Tables["MF_RCV"].Rows[0]["IJ_NO"] = "IJ";
                        ds.Tables["MF_RCV"].Rows[0]["IJ_NO"] = _ij_No;
                        ds.AcceptChanges();
                    }

                    #endregion
                }
            }
        }

        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables["MF_RCV"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["RV_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["RV_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion

            base.BeforeDsSave(ds);
        }

        #region ���µ�����

        private void UpdateDRPIJ(DataSet ds)
        {
            DataTable _dtMF = ds.Tables["MF_RCV"];
            DataTable _dtTF = ds.Tables["TF_RCV"];
            string _ij_no;
            if (_dtMF.Rows[0].RowState == DataRowState.Deleted)
                _ij_no = _dtMF.Rows[0]["IJ_NO", DataRowVersion.Original].ToString();
            else
                _ij_no = _dtMF.Rows[0]["IJ_NO"].ToString();
            DRPIJ _drpij = new DRPIJ();
            SunlikeDataSet _dsIJ;
            string _error = "";
            if (_ij_no.Length > 0)
            {
                // ɾ��ԭ������
                _dsIJ = _drpij.GetData(_usr, "IJ", _ij_no, false);
                if (_dsIJ.Tables["MF_IJ"].Rows.Count > 0)
                {
                    //// �жϵ������Ƿ������
                    //Auditing _auditing = new Auditing();
                    //if (_auditing.IsExistBill("IJ", _ij_no) && _drpij.IsFinalAuditing(_ij_no))
                    //{
                    //    _error = _drpij.RollBack("IJ", _ij_no);

                    //    if (!string.IsNullOrEmpty(_error))
                    //    {
                    //        throw new SunlikeException(_error);
                    //    }
                    //}
                    _dsIJ.Tables["MF_IJ"].Rows[0].Delete();
                    _dsIJ.ExtendedProperties["PGM_MTNRCV"] = "MTNRCV";
                    _drpij.UpdateData(null, _dsIJ, true);
                }
            }
            // �������ɵ�����
            if (_dtMF.Rows[0].RowState != DataRowState.Deleted)
            {
                _dsIJ = _drpij.GetData(_usr, "IJ", "", true);
                DataTable _dtMF_IJ = _dsIJ.Tables["MF_IJ"];
                DataTable _dtTF_IJ = _dsIJ.Tables["TF_IJ"];
                // ��ͷ
                DataRow _drMF_IJ = _dtMF_IJ.NewRow();
                _drMF_IJ["IJ_ID"] = "IJ";
                _drMF_IJ["IJ_NO"] = "";
                _drMF_IJ["IJ_DD"] = _dtMF.Rows[0]["RV_DD"];
                _drMF_IJ["BIL_TYPE"] = _dtMF.Rows[0]["BIL_TYPE"];
                _drMF_IJ["FIX_CST"] = "1";
                _drMF_IJ["DEP"] = _dtMF.Rows[0]["DEP_RCV"];
                _drMF_IJ["MAN_NO"] = _dtMF.Rows[0]["SAL_NO"];
                _drMF_IJ["REM"] = _dtMF.Rows[0]["REM"];
                _drMF_IJ["USR"] = _dtMF.Rows[0]["USR"];
                _drMF_IJ["SYS_DATE"] = _dtMF.Rows[0]["SYS_DATE"];
                _drMF_IJ["BIL_ID"] = _dtMF.Rows[0]["RV_ID"];
                _drMF_IJ["BIL_NO"] = _dtMF.Rows[0]["RV_NO"];
                _drMF_IJ["VOH_ID"] = _dtMF.Rows[0]["VOH_ID"];
                _drMF_IJ["VOH_NO"] = _dtMF.Rows[0]["VOH_NO"];
                _dtMF_IJ.Rows.Add(_drMF_IJ);
                // ����
                int _index = 1;
                foreach (DataRow _drTF in _dtTF.Rows)
                {
                    if (_drTF.RowState != DataRowState.Deleted)
                    {
                        DataRow _drTF_IJ = _dtTF_IJ.NewRow();
                        _drTF_IJ["IJ_ID"] = "IJ";
                        _drTF_IJ["IJ_NO"] = "";
                        _drTF_IJ["ITM"] = _index;
                        _drTF_IJ["PRE_ITM"] = _index;
                        _drTF_IJ["KEY_ITM"] = _index++;
                        _drTF_IJ["PRD_NO"] = _drTF["PRD_NO"];
                        _drTF_IJ["PRD_MARK"] = _drTF["PRD_MARK"];
                        _drTF_IJ["UNIT"] = _drTF["UNIT"];
                        _drTF_IJ["WH"] = _drTF["WH"];
                        //_drTF_IJ["BIL_ID"] = _drTF["RV_ID"];
                        //_drTF_IJ["BIL_NO"] = _drTF["RV_NO"];
                        //_drTF_IJ["EST_ITM"] = _drTF["ITM"];
                        _drTF_IJ["BAT_NO"] = _drTF["BAT_NO"];
                        _drTF_IJ["VALID_DD"] = _drTF["VALID_DD"];
                        _drTF_IJ["CST"] = 0;
                        _drTF_IJ["FIX_CST"] = "1";

                        decimal qty = 0;
                        qty = _drTF["QTY"].ToString() == "" ? 0 : Convert.ToDecimal(_drTF["QTY"]);
                        if (_drTF["RV_ID"].ToString() == "RR")
                        {
                            qty = qty * (-1);
                        }
                        _drTF_IJ["QTY"] = qty;
                        _dtTF_IJ.Rows.Add(_drTF_IJ);
                    }
                }
                _dsIJ.ExtendedProperties["PGM_MTNRCV"] = "MTNRCV";
                _drpij.UpdateData(null, _dsIJ, true);

                //// ��˵�����
                //string _ij_No = _drMF_IJ["IJ_NO"].ToString();
                //if (_ij_No.Length > 0)
                //{
                //    // �жϵ������Ƿ������
                //    Auditing _auditing = new Auditing();
                //    if (_auditing.IsRunAuditing("IJ",_drMF_IJ["USR"].ToString()) && !_drpij.IsFinalAuditing(_ij_No))
                //    {
                //        _error = _drpij.Approve("IJ", _ij_No, _dtMF.Rows[0]["CHK_MAN"].ToString(), Convert.ToDateTime(_dtMF.Rows[0]["CLS_DATE"].ToString()));

                //        if (!string.IsNullOrEmpty(_error))
                //        {
                //            throw new SunlikeException(_error);
                //        }
                //    }
                //}
                _dtMF.Rows[0]["IJ_ID"] = "IJ";
                _dtMF.Rows[0]["IJ_NO"] = _drMF_IJ["IJ_NO"];
            }
        }

        #endregion

        #region �������˻�����
        /// <summary>
        /// �������˻�����
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="IsAdd"></param>
        private void UpdateQtyRtn(DataRow dr, bool IsAdd)
        {
            string _prdNo = "";
            string _unit = "1";
            string maId = "";
            string maNo = "";
            string maItm = "";
            decimal _qty = 0;
            if (IsAdd)
            {
                _prdNo = dr["PRD_NO"].ToString();
                _unit = dr["UNIT"].ToString();
                maId = dr["MA_ID"].ToString();
                maNo = dr["MA_NO"].ToString();
                maItm = dr["MA_ITM"].ToString();
                if (!String.IsNullOrEmpty(maNo))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
            }
            else
            {
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                maId = dr["MA_ID", DataRowVersion.Original].ToString();
                maNo = dr["MA_NO", DataRowVersion.Original].ToString();
                maItm = dr["MA_ITM", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(maNo))
                {
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                _qty *= -1;
            }
            if (!String.IsNullOrEmpty(maId) && !String.IsNullOrEmpty(maNo) && !String.IsNullOrEmpty(maItm))
            {
                Hashtable _ht = new Hashtable();
                _ht["TableName"] = "TF_RCV";
                _ht["IdName"] = "RV_ID";
                _ht["NoName"] = "RV_NO";
                _ht["ItmName"] = "KEY_ITM";
                _ht["OsID"] = maId;
                _ht["OsNO"] = maNo;
                _ht["KeyItm"] = maItm;
                _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_unit), _ht);

                DbMTNRcv _dbRcv = new DbMTNRcv(Comp.Conn_DB);
                _dbRcv.UpdateQtyRtn(maId, maNo, Convert.ToInt16(maItm), _qty);
            }
        }
        #endregion

        #region ��дά�����뵥

        private void UpdateMA(DataRow dr, bool isAdd)
        {
            DbMTNRcv _db = new DbMTNRcv(Comp.Conn_DB);
            string _prdNo = "";
            string _unit = "";
            string osId = "";
            string osNo = "";
            string osItm = "";
            decimal _qty = 0;
            if (isAdd)
            {
                _prdNo = Convert.ToString(dr["PRD_NO"]);
                _unit = Convert.ToString(dr["UNIT"]);
                osId = Convert.ToString(dr["MA_ID"]);
                osNo = Convert.ToString(dr["MA_NO"]);
                osItm = Convert.ToString(dr["MA_ITM"]);
                _qty = Convert.ToDecimal(dr["QTY"]);
            }
            else
            {
                _prdNo = Convert.ToString(dr["PRD_NO", DataRowVersion.Original]);
                _unit = Convert.ToString(dr["UNIT", DataRowVersion.Original]);
                osId = Convert.ToString(dr["MA_ID", DataRowVersion.Original]);
                osNo = Convert.ToString(dr["MA_NO", DataRowVersion.Original]);
                osItm = Convert.ToString(dr["MA_ITM", DataRowVersion.Original]);
                _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]) * (-1);
            }
            if (!String.IsNullOrEmpty(osId) && !String.IsNullOrEmpty(osNo) && !String.IsNullOrEmpty(osItm))
            {
                if (CaseInsensitiveComparer.Default.Compare(osId, "MA") == 0)
                {
                    Hashtable _ht = new Hashtable();
                    _ht["TableName"] = "TF_MA";
                    _ht["IdName"] = "MA_ID";
                    _ht["NoName"] = "MA_NO";
                    _ht["ItmName"] = "KEY_ITM";
                    _ht["OsID"] = osId;
                    _ht["OsNO"] = osNo;
                    _ht["KeyItm"] = osItm;
                    _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_unit), _ht);
                    _db.UpdateMA(osId, osNo, osItm, _qty);
                }
            }
        }

        #endregion

        #region �����ռ�������ȡ�õ���������
        /// <summary>
        /// �����ռ�������ȡ�õ���������
        /// </summary>
        /// <param name="rv_Id">�ռ�/�ռ��˻�</param>
        /// <param name="rv_No">�ռ�������</param>
        /// <returns></returns>
        public string GetIJ_NO(string rv_Id, string rv_No)
        {
            DbMTNRcv _dbMTNRcv = new DbMTNRcv(Comp.Conn_DB);
            return _dbMTNRcv.GetIJ_NO(rv_Id, rv_No);
        }
        #endregion

        #endregion

        #region ������������
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="rvId">�ռ����ݱ�</param>
        /// <param name="rvNo">�ռ�����</param>
        /// <param name="keyItm">���</param>
        /// <param name="unit">��λ</param>
        /// <param name="qty">��������</param>
        public void UpdateQtySo(string rvId, string rvNo, int keyItm, string unit, decimal qty)
        {

            DbMTNRcv _rcv = new DbMTNRcv(Comp.Conn_DB);
            string _prdNo = "";
            string _unitRcv = "";
            SunlikeDataSet _dsRcv = _rcv.GetBody(rvId, rvNo, keyItm);
            if (_dsRcv != null && _dsRcv.Tables.Count > 0 && _dsRcv.Tables.Contains("TF_RCV") && _dsRcv.Tables["TF_RCV"].Rows.Count > 0)
            {
                _prdNo = _dsRcv.Tables[0].Rows[0]["PRD_NO"].ToString();
                _unitRcv = _dsRcv.Tables[0].Rows[0]["UNIT"].ToString();
            }
            //����
            Prdt _prdt = new Prdt();
            //���㵥λ
            decimal _qtyNew = _prdt.GetUnitQty(_prdNo, unit, qty, _unitRcv);
            //�޸ı���������
            _rcv.UpdateQtySo(rvId, rvNo, keyItm, _qtyNew);
        }
        #endregion

        #region IAuditing Members

        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = String.Empty;
            try
            {
                DbMTNRcv _dbMTNRcv = new DbMTNRcv(Comp.Conn_DB);
                _dbMTNRcv.UpdateChkMan(bil_no, chk_man, cls_dd);

                // ���ɵ�����
                SunlikeDataSet _ds = _dbMTNRcv.GetData(bil_id, bil_no, false);
                this.UpdateDRPIJ((DataSet)_ds);

                //��д�ռ����ĵ�������
                string _ij_No = _ds.Tables["MF_RCV"].Rows[0]["IJ_NO"].ToString();
                _dbMTNRcv.UpdateIJ_NO(bil_id, bil_no, "IJ", _ij_No);

                foreach (DataRow dr in _ds.Tables["TF_RCV"].Rows)
                {
                    if (dr["RV_ID"].ToString() == "RR")
                    {
                        //������ռ��˻أ���д�ռ��������˻�����
                        UpdateQtyRtn(dr, true);
                    }
                    else if (dr["RV_ID"].ToString() == "RV")
                    {
                        UpdateMA(dr, true);
                    }
                    UpdateWCH(dr, false, true);//���޿�ά�޼�¼
                }
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
                DbMTNRcv _dbMTNRcv = new DbMTNRcv(Comp.Conn_DB);
                SunlikeDataSet _ds = _dbMTNRcv.GetData(bil_id, bil_no, false);
                if (_ds.Tables.Contains("MF_RCV") && _ds.Tables["MF_RCV"].Rows.Count > 0)
                {
                    //�����ѽ᰸�����ܷ����
                    _usr = _ds.Tables["MF_RCV"].Rows[0]["USR"].ToString();
                    string _errorMsg = this.SetCanModify(_usr, "MTNRcv", _ds, false);
                    if (_errorMsg != "" && _ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                    {
                        if (_errorMsg.Length > 0)
                        {
                            return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;RCID=" + _errorMsg;//�����ʧ��
                        }
                        else
                        {
                            return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";//�����ʧ��
                        }
                    }
                    for (int i = 0; i < _ds.Tables["TF_RCV"].Rows.Count; i++)
                    {
                        if (bil_id == "RV")
                        {
                            //�ж��Ƿ������ռ��˻�
                            if (_ds.Tables["TF_RCV"].Rows[i]["QTY_RR"].ToString() != "")
                            {
                                if (Convert.ToDecimal(_ds.Tables["TF_RCV"].Rows[i]["QTY_RR"]) > 0)
                                {
                                    throw new SunlikeException("MTN.HINT.RCV_RR_ROLLBACK");//�����ռ��˻أ����ܷ���ˣ�
                                }
                            }
                            //�ж��Ƿ�����
                            if (_ds.Tables["TF_RCV"].Rows[i]["QTY_SO"].ToString() != "")
                            {
                                if (Convert.ToDecimal(_ds.Tables["TF_RCV"].Rows[i]["QTY_SO"]) > 0)
                                {
                                    throw new SunlikeException("MTN.HINT.RCV_SO_ROLLBACK");//�������ޣ����ܷ���ˣ�
                                }
                            }
                        }
                        UpdateWCH(_ds.Tables["TF_RCV"].Rows[i], true, true);
                    }

                    //��������
                    _dbMTNRcv.UpdateChkMan(bil_no, "", DateTime.Now);

                    foreach (DataRow dr in _ds.Tables["TF_RCV"].Rows)
                    {
                        if (dr["RV_ID"].ToString() == "RR")
                        {
                            //������ռ��˻أ���д�ռ��������˻�����
                            UpdateQtyRtn(dr, false);
                        }
                        else if (dr["RV_ID"].ToString() == "RV")
                        {
                            UpdateMA(dr, false);
                        }
                    }

                    // ɾ��������
                    _ds.Tables["MF_RCV"].Rows[0].Delete();
                    this.UpdateDRPIJ((DataSet)_ds);

                    //����ռ����ĵ�������
                    _dbMTNRcv.UpdateIJ_NO(bil_id, bil_no, "", "");
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }

        #endregion

        #region ICloseBill Members

        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            return CloseBill(bil_id, bil_no, true);
        }

        public string UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            return CloseBill(bil_id, bil_no, false);
        }

        private string CloseBill(string rvId, string rvNo, bool close)
        {
            SunlikeDataSet _ds = this.GetUpdateData(null, null, rvId, rvNo, false);
            DataTable _Mf = _ds.Tables["MF_RCV"];
            DataTable _Tf = _ds.Tables["TF_RCV"];
            if (_Mf.Rows.Count > 0)
            {
                DataRow _dr = _Mf.Rows[0];
                bool cls_id = Convert.ToString(_dr["CLS_ID"]) == "T";
                bool isCheck = string.IsNullOrEmpty(Convert.ToString(_dr["CHK_MAN"]));
                if (close && cls_id)
                    return "RCID=COMMON.HINT.CLOSEERROR,PARAM=" + rvNo;//�õ���[{0}]�ѽ᰸,�᰸�����������!
                if (!close && !cls_id)
                    return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + rvNo;//�õ���[{0}]δ�᰸,δ�᰸�����������!

                #region �᰸
                DbMTNRcv _dbMtn = new DbMTNRcv(Comp.Conn_DB);
                _dbMtn.CloseBill(rvId, rvNo, close);
                #endregion
            }
            return "";
        }

        #endregion

        #region ���±��޿�ά�޼�¼
        /// <summary>
        /// ���±��޿�ά�޼�¼
        /// </summary>
        /// <param name="dr">dr��</param>
        /// <param name="isDel">+-��ʶ�Ƿ�ɾ��</param>
        /// <param name="isByAuditPass">�Ǳ����ͨ��</param>
        private void UpdateWCH(DataRow dr, bool isDel, bool isByAuditPass)
        {
            if (!_isRunAuditing || isByAuditPass) //�����ͨ�����߲���Ҫ���������
            {
                string wcNo = "";
                string bilNo = "";
                if (dr.Table.Columns.Contains("WC_NO"))
                {
                    if (isDel)
                    {
                        wcNo = Convert.ToString(dr["WC_NO", DataRowVersion.Original]);
                        bilNo = Convert.ToString(dr["RV_NO", DataRowVersion.Original]);
                    }
                    else
                    {
                        wcNo = Convert.ToString(dr["WC_NO"]);
                        bilNo = Convert.ToString(dr["RV_NO"]);
                    }
                }
                if (!string.IsNullOrEmpty(wcNo))
                {
                    #region  ���¼�¼��TF_WC_H

                    WC _wc = new WC();
                    SunlikeDataSet _ds = _wc.GetDataWcH(wcNo);
                    using (DataTable _dt = _ds.Tables["TF_WC_H"])
                    {
                        DataRow[] _drs = _dt.Select("BIL_NO='" + bilNo + "'");
                        if (_drs.Length == 0)
                        {
                            if (!isDel) //����
                            {
                                DataRow _dr = _dt.NewRow();
                                _dr["WC_NO"] = wcNo;
                                _dr["BIL_ID"] = dr["RV_ID"];
                                _dr["BIL_DD"] = dr.Table.DataSet.Tables["MF_RCV"].Rows[0]["RV_DD"];
                                _dr["BIL_NO"] = bilNo;
                                //_dr["REM"] = dr["REM"];
                                _dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                _dt.Rows.Add(_dr);
                            }
                        }
                        else
                        {
                            DataRow _dr = _drs[0];
                            if (isDel)//ɾ��
                                _dr.Delete();
                            else//�޸�
                            {
                                _dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                                //_dr["REM"] = dr["REM"];
                            }
                        }
                        _wc.UpdateDataWcH(_ds);
                    }

                    #endregion
                }

            }
        }
        #endregion
    }
}
