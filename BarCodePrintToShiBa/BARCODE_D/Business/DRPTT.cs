using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections;

namespace Sunlike.Business
{
    /// <summary>
    /// ���ε���
    /// </summary>
    public class DRPTT : Sunlike.Business.BizObject, IAuditing
    {
        private bool _isRunAuditing;		//�Ƿ����������
        private bool isCreate;				//�Ƿ��������ε��۵�
        Sunlike.Business.Auditing _auditing;        //���״̬

        #region ���캯��
        /// <summary>
        /// ���ε���
        /// </summary>
        public DRPTT()
        {
            _auditing = new Auditing();
        }
        #endregion

        #region GetData
        /// <summary>
        /// �õ����ε��۱�ṹ
        /// </summary>
        /// <returns></returns>
        public SunlikeDataSet GetData()
        {
            DbDRPTT _dbDrpTt = new DbDRPTT(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpTt.GetTT_Comp();
            return _ds;
        }
        /// <summary>
        /// ȡ���ε��۵�����
        /// </summary>
        /// <param name="TtNo">���ε��۵���</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string TtNo)
        {
            DbDRPTT _dbDrpTt = new DbDRPTT(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbDrpTt.GetTT(TtNo);
            return _ds;
        }
        #endregion

        #region ����/�������ε��۵�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ds"></param>
        public DataTable UpdateTt(SunlikeDataSet Ds)
        {
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_TT"] = "TT_NO,TT_DD,SEL_ID,AREA_NO,CHK_MAN,CLS_DATE,USR,DEP,REM,CHG_TYPE,MANU_TJ";
            _ht["TF_TT"] = "TT_NO,ITM,PRD_NO,PRD_MARK,S_DD,E_DD,V_TYPE,VALUE";
            _ht["TF_TT1"] = "TT_NO,CUS_NO";
            this.UpdateDataSet(Ds, _ht);
            if (!Ds.HasErrors)
            {
                //���ӵ���Ȩ��
                string _UpdUsr = "";
                if (Ds.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = Ds.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    string _pgm = "DRPTT";
                    DataTable _dtMf = Ds.Tables["MF_TT"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        Ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        Ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        Ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                       Ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
            }
            DataTable _dtErr = GetAllErrors(Ds);
            return _dtErr;
        }
        #endregion

        #region BeforeUpdate
        /// <summary>
        /// BeforeUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            #region �ж��Ƿ�����
            string _ttNo = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _ttNo = dr["TT_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _ttNo = dr["TT_NO"].ToString();
                }
                //�ж��Ƿ�����������Ѿ����������޸ġ�
                Users _Users = new Users();
                string _whereStr = "TT_NO = '" + _ttNo + "'";
                if (_Users.IsLocked("MF_TT", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            if (tableName == "MF_TT" && statementType != StatementType.Delete)
            {
                #region ��д���ε��۵�

                string _usr = dr["USR"].ToString();

                Sunlike.Business.SQNO _sqno = new SQNO();
                Sunlike.Business.Users _user = new Users();
                DateTime _dateTime = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateFormat));

                //�ж��Ƿ����������
                string bilType = "";
                if (dr.Table.Columns.Contains("BIL_TYPE"))
                {
                    if (dr.RowState == DataRowState.Deleted)
                        bilType = dr["BIL_TYPE", DataRowVersion.Original].ToString();
                    else
                        bilType = dr["BIL_TYPE"].ToString();
                }

                _isRunAuditing = _auditing.IsRunAuditing("TT", _usr, Convert.ToBoolean(dr.Table.DataSet.ExtendedProperties["IS_DTLC"]), dr.Table.DataSet.ExtendedProperties["SH_NO"].ToString());



                Area _area = new Area();
                //��鲦�벿��
                if (dr["AREA_NO"].ToString() != String.Empty)
                {
                    if (!_area.IsExist(dr["AREA_NO"].ToString()))
                    {
                        dr.SetColumnError("AREA_NO", "RCID=COMMON.HINT.AREANOTEXIST,PARAM=" + dr["AREA_NO"].ToString());//����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
                if (statementType == StatementType.Insert)
                {
                    string _dep = _user.GetUserDepNo(_usr);
                    string _tt_No = _sqno.Set("TT", _usr, _dep, _dateTime, "FX");
                    if (statementType == StatementType.Insert)
                    {
                        dr["TT_NO"] = _tt_No;
                    }
                    dr["DEP"] = _dep;
                }
                if (statementType == StatementType.Delete)
                {
                    isCreate = false;
                }
                else
                {
                    isCreate = true;
                }
                #endregion
            }
            if (tableName == "TF_TT")
            {
                if (statementType != StatementType.Delete)
                {
                    Prdt _prdt = new Prdt();
                    if (dr.Table.DataSet.Tables.Contains("MF_TT"))
                    {
                        string _usr = dr.Table.DataSet.Tables["MF_TT"].Rows[0]["USR"].ToString();
                        //����Ʒ����						
                        if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_TT"].Rows[0]["TT_DD"])))
                        {
                            dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + dr["PRD_NO"].ToString());//��Ʒ����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                        if (CaseInsensitiveComparer.Default.Compare(dr.Table.DataSet.Tables["MF_TT"].Rows[0]["SEL_ID"].ToString(), "C") == 0)
                        {
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
                                        if (dr.Table.Columns.Contains(_aryMark[i]))
                                        {
                                            string _fldName = _dtMark.Rows[i]["FLDNAME"].ToString();
                                            if (!_mark.IsExist(_fldName, dr["PRD_NO"].ToString(), _aryMark[i]))
                                            {
                                                dr.SetColumnError(_fldName, "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _aryMark[i].Trim());//��Ʒ����[{0}]������
                                                status = UpdateStatus.SkipAllRemainingRows;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (tableName == "MF_TT" && statementType == StatementType.Delete)
            {

                SQNO _sq = new SQNO();
                string _error = _sq.Delete(dr["PS_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                if (!String.IsNullOrEmpty(_error))
                {
                    throw new SunlikeException("RCID=COMMON.HINT.DEL_NO_ERROR,PARAM=" + _error);//�޷�ɾ�����ţ�ԭ��{0}

                }
            }
            if (tableName == "MF_TT")
            {
                //#region ��˹���
                //AudParamStruct _aps ;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["TT_DD"]);
                //    _aps.BIL_ID = "TT";
                //    _aps.BIL_NO = dr["TT_NO"].ToString();
                //    _aps.BIL_TYPE = "FX";
                //    _aps.CUS_NO = "";
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = "";
                //    _aps.USR = dr["USR"].ToString();
                //    //_aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
                //}
                //else
                //    //_aps = new AudParamStruct("TT", Convert.ToString(dr["TT_NO", DataRowVersion.Original]));

                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
        }

        #endregion

        #region BeforeDsSave
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dtMf = ds.Tables["MF_TT"];
            //if (_dtMf.Rows.Count > 0)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "TT");
            //}
            //#endregion
        }
        #endregion

        #region AfterUpdate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            if (isCreate)
            {
                if (tableName == "MF_TT")
                {
                    SunlikeDataSet _ds = new SunlikeDataSet();
                    _ds = SunlikeDataSet.ConvertTo(dr.Table.DataSet).Copy();

                    if (!_isRunAuditing)
                    {
                        #region �������������̣�����е���,�������ɵ��۵�
                        Sunlike.Business.DRPTJ _drptj = new DRPTJ();
                        try
                        {
                            _drptj.UpdatePrice(_ds);
                        }
                        catch (Exception _ex)
                        {
                            dr.RowError = _ex.Message;
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        #endregion
                    }
                }
            }
        }
        #endregion

        #region ���ε��۵�����
        /// <summary>
        /// ���ε��۵�����
        /// </summary>
        /// <param name="tt_No">����</param>
        /// <param name="chk_Man">�����</param>
        /// <param name="chk_DD">���ʱ��</param>
        private void Auditing_TT(string tt_No, string chk_Man , DateTime chk_DD)
        {
            try
            {
                Sunlike.Business.Data.DbDRPTT _dbDrptt = new DbDRPTT(Comp.Conn_DB);
                _dbDrptt.Auditing_TT(tt_No, chk_Man, chk_DD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ȡ���ε��۵���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usr">�Ƶ���</param>
        /// <param name="tt_No">����</param>
        /// <param name="OnlyFillSchema">�Ƿ�schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetDRPTT(string usr, string tt_No,bool OnlyFillSchema)
        {
            SunlikeDataSet _ds = null;
            try
            {
                Sunlike.Business.Data.DbDRPTT _dbDrptt = new DbDRPTT(Comp.Conn_DB);
                _ds = _dbDrptt.GetDRPTT(tt_No, OnlyFillSchema);
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    _ds.DecimalDigits = Comp.GetCompInfo(usr).DecimalDigitsInfo.System;
                }
                if (!OnlyFillSchema)
                {
                    if (usr != null && !String.IsNullOrEmpty(usr))
                    {
                        DataTable _dtMfTt = _ds.Tables["MF_TT"];
                        if (_dtMfTt.Rows.Count > 0)
                        {
                            string _bill_Dep = _dtMfTt.Rows[0]["DEP"].ToString();
                            string _bill_Usr = _dtMfTt.Rows[0]["USR"].ToString();
                            System.Collections.Hashtable _billRight = Users.GetBillRight("DRPTT", usr, _bill_Dep, _bill_Usr);
                            _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                            _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                            _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                            _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                        }
                        this.SetCanModify(_ds, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ds;
        }
        #endregion

        #region ɾ�����ε��۵�
        /// <summary>
        /// ɾ�����ε��۵�
        /// </summary>
        /// <param name="tt_No">����</param>
        /// <param name="Del">�Ƿ�ɾ�����ε��۵�</param>
        /// <param name="Del_P">�Ƿ�ָ�����ǰ�۸�</param>
        /// <param name="Usr">�����û� </param>
        public string Delete(string tt_No, bool Del,bool Del_P,string Usr)
        {
            string _isVic = String.Empty;
            try
            {
                this.EnterTransaction();

                DRPTJ _tj = new DRPTJ();

                DataTable _dt = _tj.GetTJFromTT(tt_No);
                if (_dt.Rows.Count > 0)
                {
                    decimal _amtn = 0;
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        DataSet _dsTj = _tj.GetData(_dt.Rows[i]["TJ_NO"].ToString(), false);
                        if (_dsTj.Tables["TF_TJ"].Rows.Count > 0)
                        {
                            for (int j = 0; j < _dsTj.Tables["TF_TJ"].Rows.Count; j++)
                            {
                                if (_dsTj.Tables["TF_TJ"].Rows[j]["AMTN_NET"] != System.DBNull.Value)
                                {
                                    _amtn += Convert.ToDecimal(_dsTj.Tables["TF_TJ"].Rows[j]["AMTN_NET"]);
                                }
                            }
                        }
                    }
                }
                Sunlike.Business.Data.DbDRPTT _dbDrptt = new DbDRPTT(Comp.Conn_DB);
                _isVic = _dbDrptt.Delete(tt_No, Del, Del_P);
                if (_isVic == "T")
                {
                    //_auditing.DelBillWaitAuditing("DRP","TT",tt_No);
                }
                else
                {
                    this.SetAbort();
                }
            }
            catch (Exception ex)
            {
                this.SetAbort();
                throw ex;
            }
            finally
            {
                this.LeaveTransaction();
            }
            return _isVic;
        }
        #endregion

        #region IAuditing Members
        /// <summary>
        /// ��˲�ͬ��
        /// </summary>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pCHK_MAN"></param>
        /// <param name="chk_DD"></param>
        /// <returns></returns>
        public string Deny(string pBB_ID, string pBB_NO,string pCHK_MAN,System.DateTime chk_DD)
        {
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetDRPTT(null, bil_no, false);
                if (_ds.Tables["MF_TT"].Rows.Count > 0)
                {
                    System.DateTime _ttDate = Convert.ToDateTime(_ds.Tables["MF_TT"].Rows[0]["TT_DD"]);
                    System.DateTime _ttNow = System.DateTime.Now.Date;
                    if (_ttDate >= _ttNow)
                    {
                        this.EnterTransaction();
                        this.Auditing_TT(bil_no, chk_man, cls_dd);
                        //�����۸�
                        DRPTJ _drpTj = new DRPTJ();
                        _drpTj.UpdatePrice(_ds);
                    }
                    else
                    {
                        _error = bil_no + "�ѹ��������ڣ��޷�����";
                    }
                }
                else
                {
                    _error = bil_no + "�ѱ�ɾ��������";
                }
            }
            catch (Exception ex)
            {
                this.SetAbort();
                _error = ex.Message.ToString();
            }
            finally
            {
                this.LeaveTransaction();
            }
            return _error;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            return RollBack(bil_id, bil_no, true);
        }
        /// <summary>
        /// �ع�����
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no,bool canChangeDS)
        {
            string _error = String.Empty;
            try
            {
                Sunlike.Business.DRPTJ _drptj = new DRPTJ();
                if (!_drptj.GetAud_FlagForTT(bil_no))
                {
                    SunlikeDataSet _ds = this.GetDRPTT(null, bil_no, false);

                    if (_ds.Tables["MF_TT"].Rows.Count > 0 && _ds.Tables["TF_TT"].Rows.Count > 0)
                    {
                        string _bDate = _ds.Tables["TF_TT"].Rows[0]["S_DD"].ToString();
                        TimeSpan _span = System.DateTime.Now.Date - Convert.ToDateTime(_bDate);

                        if (_span.Days > 0)
                        {
                            return "�����ʧ�ܣ��޷��������ε��۵��Ѿ���Ч";
                        }
                        else
                        {
                            try
                            {
                                this.EnterTransaction();
                                //�ָ���Ʒ�۸�
                                this.Delete(bil_no, false, true, _ds.Tables["MF_TT"].Rows[0]["USR"].ToString());
                                if (canChangeDS)
                                {
                                    //��������˺�����ʱ��
                                    _drptj.UpdateMfTt(bil_no);
                                }
                            }
                            catch (Exception _ex)
                            {
                                this.SetAbort();
                                return _ex.Message;
                            }
                            finally
                            {
                                this.LeaveTransaction();
                            }
                        }
                    }
                }
                else
                {
                    _error += "RCID=INV.HINT.AUD_FLAG_ALERT";
                }
            }
            catch (Exception ex)
            {
                _error += ex.Message.ToString();
            }
            return _error;
        }

        #endregion

        #region ������ε���
        /// <summary>
        /// GetTjData
        /// </summary>
        /// <param name="pTjNo"></param>
        /// <returns></returns>
        public DataTable GetTjData(string pTjNo)
        {
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            return _tt.GetTjData(pTjNo);
        }
        /// <summary>
        /// �õ�����/�����˼�¼
        /// </summary>
        /// <param name="Date1"></param>
        /// <param name="Date2"></param>
        /// <param name="pFlag"></param>
        /// <param name="CusB"></param>
        /// <param name="CusE"></param>
        /// <returns></returns>
        public DataTable GetTjData(DateTime Date1, DateTime Date2,bool pFlag,string CusB,string CusE)
        {
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            return _tt.GetTjData(Date1, Date2, pFlag, CusB, CusE);
        }
        /// <summary>
        /// �õ�����/�����˼�¼
        /// </summary>
        /// <param name="pFlag"></param>
        /// <param name="CusB"></param>
        /// <param name="CusE"></param>
        /// <returns></returns>
        public DataTable GetTjData(bool pFlag, string CusB,string CusE)
        {
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            return _tt.GetTjData(pFlag, CusB, CusE);
        }
        /// <summary>
        ///  ������
        /// </summary>
        /// <param name="pTjNo">�����˵��ż���</param>
        /// <param name="isAudit"></param>
        /// <param name="jhDate"></param>
        public int UnSetFlag(string pTjNo, string isAudit, string jhDate)
        {
            int _ok = 0;
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            try
            {
                this.EnterTransaction();
                _ok = _tt.UnSetFlag(pTjNo, isAudit, jhDate);
                if (_ok == 0)
                {
                    DRPTJ _tj = new DRPTJ();
                    SunlikeDataSet _ds = _tj.GetData(pTjNo, false);
                    if (_ds.Tables["MF_TJ"].Rows.Count > 0)
                    {
                        //�������ö��
                        Arp _arp = new Arp();
                        Cust _cust = new Cust();
                        string _cusNo = _ds.Tables["MF_TJ"].Rows[0]["CUS_NO"].ToString();
                        DateTime _date = Convert.ToDateTime(_ds.Tables["MF_TJ"].Rows[0]["TJ_DD"]);
                        if (_cust.IsDrp_id(_cusNo))
                        {
                            decimal _amtn = 0;
                            for (int j = 0; j < _ds.Tables["TF_TJ"].Rows.Count; j++)
                            {
                                if (_ds.Tables["TF_TJ"].Rows[j]["AMTN_NET"] != System.DBNull.Value)
                                {
                                    _amtn += Convert.ToDecimal(_ds.Tables["TF_TJ"].Rows[j]["AMTN_NET"]);
                                }
                            }
                            _arp.UpdateSarp("1", _date.Year, _cusNo, _date.Month, "", "AMTN_INV", _amtn);
                        }
                    }
                }
            }
            catch
            {
                this.SetAbort();
            }
            finally
            {
                this.LeaveTransaction();
            }
            return _ok;
        }

        /// <summary>
        ///  ���˵��۵�
        /// </summary>
        /// <param name="pTjNo">�����˵��ż���</param>
        public void SetFlag(string pTjNo)
        {
            DbDRPTT _tt = new DbDRPTT(Comp.Conn_DB);
            string[] _tjAry = pTjNo.Split(new char[] { ',' });
            bool _isOk = true;
            if (_tjAry.Length > 0)
            {
                for (int i = 0; i < _tjAry.Length; i++)
                {
                    if (_tjAry[i] != String.Empty)
                    {
                        try
                        {
                            this.EnterTransaction();

                            _isOk = _tt.SetFlag(pTjNo);
                            if (_isOk)
                            {
                                DRPTJ _tj = new DRPTJ();
                                SunlikeDataSet _ds = _tj.GetData(pTjNo, false);
                                if (_ds.Tables["MF_TJ"].Rows.Count > 0)
                                {
                                    //�������ö��
                                    Arp _arp = new Arp();
                                    Cust _cust = new Cust();
                                    string _cusNo = _ds.Tables["MF_TJ"].Rows[0]["CUS_NO"].ToString();
                                    DateTime _date = Convert.ToDateTime(_ds.Tables["MF_TJ"].Rows[0]["TJ_DD"]);
                                    if (_cust.IsDrp_id(_cusNo))
                                    {
                                        decimal _amtn = 0;
                                        for (int j = 0; j < _ds.Tables["TF_TJ"].Rows.Count; j++)
                                        {
                                            if (_ds.Tables["TF_TJ"].Rows[j]["AMTN_NET"] != System.DBNull.Value)
                                            {
                                                _amtn += Convert.ToDecimal(_ds.Tables["TF_TJ"].Rows[j]["AMTN_NET"]);
                                            }
                                        }
                                        _arp.UpdateSarp("1", _date.Year, _cusNo, _date.Month, "", "AMTN_INV", _amtn * -1);
                                    }
                                }
                            }
                        }
                        catch (Exception _ex)
                        {
                            this.SetAbort();
                        }
                        finally
                        {
                            this.LeaveTransaction();
                        }
                    }
                }
            }
        }


        #endregion

        #region SetCanModify
        /// <summary>
        /// ��鵥���Ƿ�����޸�
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">�Ƿ��ж��������</param>
        private void SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            //��ɾ�������ε��۵���Ϊ���¼������
            //1.���ʺ�����ɾ��
            //2.��������̣�����û�п�ʼ���
            //3.��������̣��Ѿ�����˵�ԭʼλ��
            //4.����������̣��Ѿ����ɵ��۵������Ѿ����ۣ������۵�δ���ˣ����ҵ�������Ϊ���գ���ɾ������ֵ�ָ���ԭʼ״̬
            //5.����ˣ���û�����ɵ��۵�ʱ��������ɾ��(��Ϊɾ���۸��趨ʱ�����Ե��۵�Ϊ׼������ʱ���۵��ǲ����ڵ�)
            DataTable _dtMf = ds.Tables["MF_TT"];
            bool _bCanModify = true;
            if (_dtMf.Rows.Count > 0)
            {
                //����������޸�
                if (_dtMf.Rows[0]["CHK_MAN"].ToString() != String.Empty)
                {
                    ds.ExtendedProperties["UPD"] = "N";
                }
                //1.���ʺ�����ɾ��
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["TT_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                }
                if (_bCanModify)
                {
                    //2.��������̣�����û�п�ʼ���
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing("TT", _dtMf.Rows[0]["TT_NO"].ToString()))
                    {
                        _bCanModify = false;
                    }
                }
                //�ж��Ƿ�����
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                }
                if (_bCanModify)
                {
                    //4.�����ε��۵���Ӧ�ĵ��۵������л��˹��ĵ��۵�
                    if (_dtMf.Rows[0]["TT_NO"].ToString() != "")
                    {
                        DataTable _dt = GetTjFromTT(_dtMf.Rows[0]["TT_NO"].ToString());
                        if (_dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < _dt.Rows.Count; i++)
                            {
                                if (_dt.Rows[i]["AUD_FLAG"].ToString() == "T")
                                {
                                    _bCanModify = false;
                                    break;
                                }
                                //4.�����������С�ڵ�ǰ���ڣ�������ɾ������Ϊ��ʱ������Ѿ������˵��ۺ��ĵ���,
                                //��ΪĿǰ�Ĵ���ʽ�ǵ��۵ĵڶ���ſ�ʼ��Ч
                                if (_dt.Rows[i]["TJ_DD"].ToString() != String.Empty)
                                {
                                    if (System.DateTime.Now.Date > Convert.ToDateTime(_dt.Rows[i]["TJ_DD"]))
                                    {
                                        _bCanModify = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
        }
        #endregion

        #region �������ε��۵�ȡ�ͻ����۵�����
        /// <summary>
        /// �������ε��۵�ȡ�ͻ����۵�����
        /// </summary>
        /// <param name="ttNo">���ε��۵���</param>
        /// <returns></returns>
        public DataTable GetTjFromTT(string ttNo)
        {
            DbDRPTT _dbTt = new DbDRPTT(Comp.Conn_DB);
            return _dbTt.GetTjFromTT(ttNo).Tables[0];
        }
        #endregion

        #region ������ɺ��д���ε��۱�
        /// <summary>
        /// ������ɺ��д���ε��۱� 
        /// </summary>
        /// <param name="TtNo">�Ѿ���ɵ����ε��۵�</param>
        /// <param name="Manu">�Ƿ��ֹ�����</param>
        public void SetTjState(string TtNo, string Manu)
        {
            string _sql = "update mf_tt set TJ_CUS=NULL,MANU_TJ=NULL where tt_no='" + TtNo + "'";
            if (Manu == "T")
            {
                _sql = "update mf_tt set TJ_CUS=NULL,MANU_TJ='T' where tt_no='" + TtNo + "'";
            }
            Query _query = new Query();
            _query.DoSQLString(_sql);
        }
        #endregion

        #region �ж������ϲ����򶨼������Ƿ�Ϊ������
        /// <summary>
        /// �ж������ϲ����򶨼������Ƿ�Ϊ������
        /// </summary>
        /// <param name="AreaNo"></param>
        /// <param name="PrdNo"></param>
        /// <returns></returns>
        public bool IsAreaPriceIncludeUnder(string AreaNo, string PrdNo)
        {
            DbDRPTT _drpTt = new DbDRPTT(Comp.Conn_DB);
            Area _area = new Area();
            string _areaUp = _area.GetUp(AreaNo);
            if (!String.IsNullOrEmpty(_areaUp))
            {
                SunlikeDataSet _ds = _drpTt.GetAreaPriceIncludeUnder(_areaUp, PrdNo);
                if (_ds.Tables["PRDT_UPR4"].Rows.Count > 0)
                {
                    if (CaseInsensitiveComparer.Default.Compare(_ds.Tables["PRDT_UPR4"].Rows[0]["AREA_INCLUDE"].ToString(), "Y") == 0)
                    {
                        return true;
                    }
                    else
                    {
                        this.IsAreaPriceIncludeUnder(_areaUp, PrdNo);
                    }
                }
                else
                {
                    this.IsAreaPriceIncludeUnder(_areaUp, PrdNo);
                }
            }
            return false;
        }
        #endregion
    }
}
