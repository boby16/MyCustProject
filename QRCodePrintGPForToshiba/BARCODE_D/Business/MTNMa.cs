using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Data;
using System.Collections;
namespace Sunlike.Business
{
    /// <summary>
    /// ά�����뵥
    /// </summary>
    public class MTNMa : BizObject, IAuditing, ICloseBill
    {
        private string _loginUsr = "";
        private string _maId = "";
        private bool _isRunAuditing;
        #region constructor
        /// <summary>
        /// ά�����뵥
        /// </summary>
        public MTNMa()
        {
        }
        #endregion

        #region ȡ����
        /// <summary>
        /// ȡά�����뵥����
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="maId">���뵥�ݱ�</param>
        /// <param name="maNo">���뵥��</param>
        /// <param name="isSchema">�Ƿ�ȡschema</param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string pgm, string usr, string maId, string maNo, bool isSchema)
        {
            DbMTNMa _ma = new DbMTNMa(Comp.Conn_DB);
            SunlikeDataSet _ds = _ma.GetData(maId, maNo, isSchema);
            if (!string.IsNullOrEmpty(usr))
            {
                Users _usrs = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_usrs.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //���ӵ���Ȩ��
            if (!isSchema)
            {
                if (usr != null)
                {
                    DataTable _dtMf = _ds.Tables["MF_MA"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
            }
            //�Զ�����
            _ds.Tables["TF_MA"].Columns["KEY_ITM"].AutoIncrement = true;
            _ds.Tables["TF_MA"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_MA"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_MA"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _ds.Tables["TF_MA"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_MA"].Columns["KEY_ITM"].Unique = true;

            this.SetCanModify(pgm, usr, _ds, true);
            return _ds;
        }
        /// <summary>
        /// ȡά�����뵥����
        /// </summary>
        /// <param name="maId">���뵥�ݱ�</param>
        /// <param name="maNo">���뵥��</param>
        /// <param name="keyIem">�������</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string maId, string maNo, int keyIem)
        {
            DbMTNMa _ma = new DbMTNMa(Comp.Conn_DB);
            return _ma.GetData(maId, maNo, keyIem);
        }
        /// <summary>
        /// ȡ��ͷ��Ϣ
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            DbMTNMa _ma = new DbMTNMa(Comp.Conn_DB);
            return _ma.GetData(sqlWhere);
        }
        #endregion

        #region SetCanModify
        /// <summary>
        /// �Ƿ���Ը���
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>        
        /// <param name="ds"></param>
        ///<param name="bCheckAuditing"></param>
        /// <returns></returns>
        private string SetCanModify(string pgm, string usr, SunlikeDataSet ds, bool bCheckAuditing)
        {
            if (string.IsNullOrEmpty(pgm))
                return "";
            bool _bCanModify = true;
            if (!string.IsNullOrEmpty(usr))
            {
                string _billDep = "";
                string _billUsr = "";
                string _MaId = "";
                if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_MA") && ds.Tables["MF_MA"].Rows.Count > 0)
                {
                    _billDep = ds.Tables["MF_MA"].Rows[0]["DEP"].ToString();
                    _billUsr = ds.Tables["MF_MA"].Rows[0]["USR"].ToString();
                    _MaId = ds.Tables["MF_MA"].Rows[0]["MA_ID"].ToString();
                }
                Hashtable _right = Users.GetBillRight(pgm, usr, _billDep, _billUsr);
                ds.ExtendedProperties["UPD"] = _right["UPD"];
                ds.ExtendedProperties["DEL"] = _right["DEL"];
                ds.ExtendedProperties["PRN"] = _right["PRN"];
                ds.ExtendedProperties["LCK"] = _right["LCK"];
                Users _users = new Users();
                ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //�ж��������
            if (bCheckAuditing)
            {
                Auditing _aud = new Auditing();
                if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_MA") && ds.Tables["MF_MA"].Rows.Count > 0)
                {
                    if (_aud.GetIfEnterAuditing(ds.Tables["MF_MA"].Rows[0]["MA_ID"].ToString(), ds.Tables["MF_MA"].Rows[0]["MA_NO"].ToString()))
                    {
                        _bCanModify = false;
                        ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_MA") && ds.Tables["MF_MA"].Rows.Count > 0)
            {
                if (Comp.HasCloseBill(Convert.ToDateTime(ds.Tables["MF_MA"].Rows[0]["MA_DD"]), ds.Tables["MF_MA"].Rows[0]["DEP"].ToString(), "CLS_MNU"))
                {
                    string _err = "RCID=COMMON.HINT.CLOSE_CLS";//����
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, _err);
                }
            }
            //�ж��Ƿ�᰸

            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_MA") && ds.Tables["MF_MA"].Rows.Count > 0)
            {
                if (ds.Tables["MF_MA"].Rows[0]["CLS_ID"].ToString() == "T"
                    || ds.Tables["MF_MA"].Rows[0]["CLS_AUTO"].ToString() == "T")
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");
                }

            }
            //�ж��Ƿ�����
            if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_MA") && ds.Tables["MF_MA"].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(ds.Tables["MF_MA"].Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return "";
        }
        #endregion

        #region ����
        #region UpdateData
        /// <summary>
        /// ����ά�����뵥
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="changeDs"></param>
        /// <param name="bubbleException"></param>
        public DataTable UpdateData(string pgm ,SunlikeDataSet changeDs, bool bubbleException)
        {
            #region ȡ�õ��ݵ����״̬
            if (changeDs != null && changeDs.Tables.Contains("MF_MA") && changeDs.Tables["MF_MA"].Rows.Count > 0)
            {
                if (changeDs.Tables["MF_MA"].Rows[0].RowState == DataRowState.Deleted)
                {
                    this._loginUsr = changeDs.Tables["MF_MA"].Rows[0]["USR", DataRowVersion.Original].ToString();
                    _maId = changeDs.Tables["MF_MA"].Rows[0]["MA_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    this._loginUsr = changeDs.Tables["MF_MA"].Rows[0]["USR"].ToString();
                    _maId = changeDs.Tables["MF_MA"].Rows[0]["MA_ID"].ToString();
                }
                Auditing _auditing = new Auditing();

                DataRow _dr = changeDs.Tables["MF_MA"].Rows[0];
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
                //_isRunAuditing = _auditing.IsRunAuditing(_maId, _loginUsr,_bilType,_mobID);


            }
            #endregion
            System.Collections.Hashtable _ht = new Hashtable();
            _ht["MF_MA"] = " MA_ID,MA_NO,MA_DD,CUS_NO,SAL_NO,DEP,CNT_NO,CNT_REM,BIL_TYPE,REM,"
                         + " CNT_NAME,OTH_NAME,TEL_NO,CELL_NO,CNT_ADR,"
                         + " SYS_DATE,CLS_DATE,PRT_SW,USR,CHK_MAN,CLS_ID,CLS_AUTO,MTN_FLOW,EST_DD,MOB_ID,QA_NO,BIL_ID,BIL_NO";
            _ht["TF_MA"] = " MA_ID ,MA_NO,ITM,PRD_NO,PRD_NAME,PRD_MARK,WC_NO,UNIT,SA_NO,SA_ITM,QTY,MTN_DD,"
                         + " EST_DD,RTN_DD,REM,KEY_ITM,QTY_MTN,MTN_TYPE,MTN_ALL_ID";
            this.UpdateDataSet(changeDs, _ht);
            if (!changeDs.HasErrors)
            {
                #region δ��������
                //Ȩ���ж�
                if (changeDs.Tables.Contains("MF_MA"))
                {
                    string _pgm = "";
                    //ȡ��PGM
                    if (changeDs.ExtendedProperties.Contains("PGM"))
                    {
                        _pgm = changeDs.ExtendedProperties["PGM"].ToString();
                    }
                    if (string.IsNullOrEmpty(_pgm))
                    {
                        _pgm = "MTNMa";
                    }
                    if (!string.IsNullOrEmpty(pgm))
                    {
                        _pgm = pgm;
                    }
                    DataTable _dtMf = changeDs.Tables["MF_MA"];
                    if (!string.IsNullOrEmpty(_loginUsr))
                    {
                        string _billDep = "";
                        string _billUsr = "";
                        string _MaId = "";
                        if (changeDs.Tables.Count > 0 && changeDs.Tables.Contains("MF_MA") && changeDs.Tables["MF_MA"].Rows.Count > 0)
                        {
                            _billDep = changeDs.Tables["MF_MA"].Rows[0]["DEP"].ToString();
                            _billUsr = changeDs.Tables["MF_MA"].Rows[0]["USR"].ToString();
                            _MaId = changeDs.Tables["MF_MA"].Rows[0]["MA_ID"].ToString();
                        }
                        Hashtable _right = Users.GetBillRight(_pgm, _loginUsr, _billDep, _billUsr);
                        changeDs.ExtendedProperties["UPD"] = _right["UPD"];
                        changeDs.ExtendedProperties["DEL"] = _right["DEL"];
                        changeDs.ExtendedProperties["PRN"] = _right["PRN"];
                       changeDs.ExtendedProperties["LCK"] = _right["LCK"];
                        Users _users = new Users();
                        changeDs.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(_loginUsr)).DecimalDigitsInfo.System;
                    }
                    string _UpdUsr = "";
                    if (changeDs.ExtendedProperties.Contains("UPD_USR"))
                        _UpdUsr = changeDs.ExtendedProperties["UPD_USR"].ToString();
                    this.SetCanModify(_pgm, _UpdUsr, changeDs, true);
                }

                #endregion
            }
            if (changeDs.HasErrors)
            {
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changeDs);
                    throw new SunlikeException("RCID=MTNMa.UpdateData() Error:;" + _errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changeDs);
                }
            }
            return Sunlike.Business.BizObject.GetAllErrors(changeDs);
        }
        #endregion

        #region beforeUpdate
        /// <summary>
        /// beforeUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            //#region �ж��Ƿ�����
            string _maNo = "", _maId = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _maNo = dr["MA_NO", DataRowVersion.Original].ToString();
                    _maId = dr["MA_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    _maNo = dr["MA_NO"].ToString();
                    _maId = dr["MA_ID"].ToString();
                }
                //�ж��Ƿ�����������Ѿ����������޸ġ�
                Users _Users = new Users();
                string _whereStr = "MA_ID = '" + _maId + "' AND MA_NO = '" + _maNo + "'";
                if (_Users.IsLocked("MF_MA", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            //#endregion
            #region MF_MA
            if (tableName == "MF_MA")
            {
                #region ����
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["MA_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["MA_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion

                #region ���
                if (statementType != StatementType.Delete)
                {
                   
                    //���ͻ�(����)
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(this._loginUsr, dr["CUS_NO"].ToString()))
                    {
                        dr.SetColumnError("CUS_NO",/*�ͻ����Ų����ڻ�û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString() + "");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //����(����)
                    Dept _dept = new Dept();
                    if (_dept.IsExist(_loginUsr, dr["DEP"].ToString(), Convert.ToDateTime(dr["MA_DD"])) == false)
                    {
                        dr.SetColumnError("DEP",/*���Ŵ���[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //ҵ��Ա
                    string _salNo = dr["SAL_NO"].ToString();
                    if (!String.IsNullOrEmpty(_salNo))
                    {
                        Salm _salm = new Salm();
                        if (_salm.IsExist(_loginUsr, _salNo, Convert.ToDateTime(dr["MA_DD"])) == false)
                        {
                            dr.SetColumnError("SAL_NO",/*ҵ��Ա����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + _salNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //ά������
                    if (!_isRunAuditing)
                    {
                        if (String.IsNullOrEmpty(dr["MTN_FLOW"].ToString()))
                        {
                            dr.SetColumnError("MTN_FLOW",/*ά������*/"RCID=MTN.HINT.MTN_FLOW_NULL");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
                else
                {
                    #region �ж��Ƿ��н᰸��ҵ
                    Sunlike.Business.DRPEN _en = new DRPEN();
                    if (_en.IsExists(this._maId, dr["MA_NO", DataRowVersion.Original].ToString()))
                    {
                        dr.SetColumnError("MA_NO",/*�Ѿ��᰸,����ɾ��*/"RCID=INV.HINT.HASEN");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    #endregion

                }
                #endregion

                #region ȡ����
                if (statementType == StatementType.Insert)
                {
                    #region ȡ����
                    SQNO SunlikeSqNo = new SQNO();
                    string _depNo = dr["DEP"].ToString();//����
                    DateTime _maDd = System.DateTime.Now;
                    if (dr["MA_DD"] is System.DBNull)
                    {
                        _maDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _maDd = Convert.ToDateTime(dr["MA_DD"]);
                    }
                    string _bilType = dr["BIL_TYPE"].ToString();
                    string _bilNo = SunlikeSqNo.Set(dr["MA_ID"].ToString(), _loginUsr, _depNo, _maDd, _bilType);
                    dr["MA_NO"] = _bilNo;
                    dr["MA_DD"] = _maDd.ToString(Comp.SQLDateFormat);
                    dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    dr["PRT_SW"] = "N";
                    #endregion
                }
                #endregion

      //          #region ��˹���
      //          AudParamStruct _aps;
      //          if (statementType != StatementType.Delete)
      //          {
      //              _aps.BIL_TYPE = "FX";
      //              _aps.BIL_ID = this._maId;
      //              _aps.BIL_NO = dr["MA_NO"].ToString();
      //              _aps.BIL_DD = string.IsNullOrEmpty(dr["MA_DD"].ToString()) ? Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateFormat)) : Convert.ToDateTime(Convert.ToDateTime(dr["MA_DD"].ToString()).ToString(Comp.SQLDateFormat));
      //              _aps.USR = this._loginUsr;
      //              _aps.CUS_NO = dr["CUS_NO"].ToString();
      //              _aps.DEP = dr["DEP"].ToString();
      //              _aps.SAL_NO = dr["SAL_NO"].ToString();
      //              _aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
      //          }
      //          else
      //              _aps = new AudParamStruct(_maId, Convert.ToString(dr["MA_NO", DataRowVersion.Original]));
      //          Auditing _auditing = new Auditing();
      //          string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
      //          if (!string.IsNullOrEmpty(_auditErr))
      //          {
      //              throw new SunlikeException(_auditErr);
      //          }
      //#endregion 
            }
            #endregion

            #region TF_MA
            if (tableName == "TF_MA")
            {
                #region ���
                if (statementType != StatementType.Delete)
                {
                    //��Ʒ���(����)
                    Prdt _prdt = new Prdt();
                    if (_prdt.IsExist(_loginUsr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MA"].Rows[0]["MA_DD"])) == false)
                    {
                        dr.SetColumnError("PRD_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + dr["PRD_NO"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //����
                    string _mark = dr["PRD_MARK"].ToString();
                    int _prdMod = _prdt.CheckPrdtMod(dr["PRD_NO"].ToString(), _mark);
                    if (_prdMod == 1)
                    {
                        dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _mark);//��Ʒ����[{0}]������
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else if (_prdMod == 2)
                    {
                        PrdMark _prd_Mark = new PrdMark();
                        if (_prd_Mark.RunByPMark(_loginUsr))
                        {
                            string[] _prd_markAry = _prd_Mark.BreakPrdMark(_mark);
                            DataTable _markTable = _prd_Mark.GetSplitData("");
                            for (int i = 0; i < _markTable.Rows.Count; i++)
                            {
                                string _markName = _markTable.Rows[i]["FLDNAME"].ToString();
                                if (!_prd_Mark.IsExist(_markName, dr["PRD_NO"].ToString(), _prd_markAry[i]))
                                {
                                    dr.SetColumnError(_markName,/*��Ʒ����[{0}]������,����*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[i].Trim());
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                    //ά�޿�
                    if (dr["WC_NO"].ToString().Length > 0)
                    {
                        WC _wc = new WC();
                        SunlikeDataSet _dsWc = _wc.GetDataForMA(dr["WC_NO"].ToString());
                        if (_dsWc != null && _dsWc.Tables.Contains("MF_WC") && _dsWc.Tables["MF_WC"].Rows.Count > 0)
                        {
                            if (_dsWc.Tables["MF_WC"].Rows[0]["CUS_NO"].ToString().Length > 0)
                            {
                                string _cusNoMa = "";
                                string _prdNoWc = "";
                                if (dr.Table.DataSet.Tables.Contains("MF_MA") && dr.Table.DataSet.Tables["MF_MA"].Rows.Count > 0)
                                {
                                    _cusNoMa = dr.Table.DataSet.Tables["MF_MA"].Rows[0]["CUS_NO"].ToString();
                                }
                                _prdNoWc = _dsWc.Tables["MF_WC"].Rows[0]["PRD_NO"].ToString();
                                //�ͻ��ж�
                                //if (_cusNoMa != _dsWc.Tables["MF_WC"].Rows[0]["CUS_NO"].ToString())
                                //{
                                //    dr.SetColumnError("WC_NO",/*ά�޿��ͻ�[{0}]�뵱ǰ�ͻ�[{1}]����*/"RCID=MTN.HINT.MTNCUSTERROR,PARAM=" + _dsWc.Tables["MF_WC"].Rows[0]["CUS_NO"].ToString() + ",PARAM=" + _cusNoMa);
                                //    status = UpdateStatus.SkipAllRemainingRows;
                                //}
                                //��Ʒ�ж�
                                if (_prdNoWc != dr["PRD_NO"].ToString())
                                {
                                    dr.SetColumnError("WC_NO",/*ά�޿���Ʒ[{0}]�뵱ǰ��Ʒ[{1}]����*/"RCID=MTN.HINT.MTNPRDTERROR,PARAM=" + _prdNoWc + ",PARAM=" + dr["PRD_NO"].ToString());
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }

                            }
                        }
                        else
                        {
                            dr.SetColumnError("WC_NO", "RCID=MTN.HINT.WC_NO_NOT_EXIST;");//���޿��Ų�����!
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //�������ж�
                    if (dr["SA_NO"].ToString().Length > 0)
                    {
                        DRPSA _sa = new DRPSA();
                        SunlikeDataSet _dsSa = _sa.GetDataPreItm("SA", dr["SA_NO"].ToString(), dr["SA_ITM"].ToString());
                        string _prdNoSa = "";
                        string _cusNoSa = "";
                        string _cusNoMa = "";
                        if (_dsSa != null && _dsSa.Tables.Contains("MF_PSS") && _dsSa.Tables["MF_PSS"].Rows.Count > 0)
                        {
                            _cusNoSa = _dsSa.Tables["MF_PSS"].Rows[0]["CUS_NO"].ToString();
                        }
                        if (_dsSa != null && _dsSa.Tables.Contains("TF_PSS") && _dsSa.Tables["TF_PSS"].Rows.Count > 0)
                        {
                            _prdNoSa = _dsSa.Tables["TF_PSS"].Rows[0]["PRD_NO"].ToString();
                        }
                        if (dr.Table.DataSet.Tables.Contains("MF_MA") && dr.Table.DataSet.Tables["MF_MA"].Rows.Count > 0)
                        {
                            _cusNoMa = dr.Table.DataSet.Tables["MF_MA"].Rows[0]["CUS_NO"].ToString();
                        }
                        //�ͻ��ж�
                        if (_cusNoSa != _cusNoMa)
                        {
                            dr.SetColumnError("SA_NO",/*�������ͻ�[{0}]�뵱ǰ�ͻ�[{1}]����*/"RCID=MTN.HINT.SACUSTERROR,PARAM=" + _cusNoSa + ",PARAM=" + _cusNoMa);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                        //��Ʒ�ж�
                        if (_prdNoSa != dr["PRD_NO"].ToString())
                        {
                            dr.SetColumnError("SA_NO",/*��������Ʒ[{0}]�뵱ǰ��Ʒ[{1}]����*/"RCID=MTN.HINT.SAPRDTERROR,PARAM=" + dr["PRD_NO"].ToString() + ",PARAM=" + _prdNoSa);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                    }
                }
                #endregion

                if (statementType != System.Data.StatementType.Delete)
                {
                    #region ������С����ת��
                    decimal _qty = 0;
                    decimal _qtyMtn = 0;
                    if (!string.IsNullOrEmpty(dr["QTY"].ToString()) && Convert.ToDecimal(dr["QTY"].ToString()) != 0)
                        _qty += Convert.ToDecimal(dr["QTY"].ToString());
                    if (!string.IsNullOrEmpty(dr["QTY_MTN"].ToString()) && Convert.ToDecimal(dr["QTY_MTN"].ToString()) != 0)
                        _qtyMtn += Convert.ToDecimal(dr["QTY_MTN"].ToString());
                    if (_qty < _qtyMtn)
                    {
                        dr.SetColumnError("QTY",/*������С����ת��,����*/"RCID=MTN.TF_MA.HASQTYERROR");
                        status = UpdateStatus.SkipAllRemainingRows;
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
                    if (statementType == StatementType.Update)
                    {
                        UpdateWCH(dr, true, false);
                    }
                    UpdateWCH(dr, false, false);//���±��޿�ά�޼�¼
                }

                if (statementType == System.Data.StatementType.Delete)
                {
                    #region �ж��Ƿ���ɾ����ǰ��
                    if (!string.IsNullOrEmpty(dr["QTY_MTN", DataRowVersion.Original].ToString()) && Convert.ToDecimal(dr["QTY_MTN", DataRowVersion.Original].ToString()) > 0)
                    {
                        throw new SunlikeException(/*��תά������*/"RCID=COMMON.HINT.HASQTYMTN");
                    }
                    #endregion
                    UpdateWCH(dr, true, false);//���±��޿�ά�޼�¼
                }
            }
            #endregion

            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }
        #endregion

        #region BeforeDsSave
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables["MF_MA"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["MA_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["MA_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion


            base.BeforeDsSave(ds);
        }
        #endregion

        #region AfterUpdate
        /// <summary>
        /// AfterUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            #region MF_MA
            if (tableName == "MF_MA")
            {
                if (statementType == StatementType.Delete)
                {
                    #region ɾ������
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(dr["MA_NO", DataRowVersion.Original].ToString(), _loginUsr);//ɾ��ʱ��BILD�в���һ������

                    #endregion                   
                }

                #region ���¿ͻ�����
                if (statementType == StatementType.Insert)
                {
                    UpdateQa(dr, true);
                }
                else if (statementType == StatementType.Update)
                {
                    UpdateQa(dr, false);
                    UpdateQa(dr, true);
                }
                else if (statementType == StatementType.Delete)
                {
                    UpdateQa(dr, false);
                }
                #endregion
            }
            #endregion

            #region TF_MA

            #endregion

            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        #endregion

        #region �᰸����
        /// <summary>
        /// �Զ��᰸ά�����뵥
        /// </summary>
        /// <param name="maId">���뵥�ݱ�</param>
        /// <param name="maNo">���뵥��</param>
        public void UpdateClsId(string maId, string maNo)
        {
            DbMTNMa _ma = new DbMTNMa(Comp.Conn_DB);
            _ma.UpdateClsId(maId, maNo);

        }
        #endregion

        #region ��˵��ݱ��
        /// <summary>
        /// ��˵��ݱ��
        /// </summary>
        /// <param name="maId">���뵥�ݱ�</param>
        /// <param name="maNo">���뵥��</param>
        /// <param name="chkMan">�����</param>
        /// <param name="clsDate">�����</param>
        public void UpdateMtnMa(string maId, string maNo, string chkMan, DateTime clsDate)
        {
            DbMTNMa _ma = new DbMTNMa(Comp.Conn_DB);
            _ma.UpdateMtnMa(maId, maNo, chkMan, clsDate);
        }
        /// <summary>
        /// ����˵��ݱ��
        /// </summary>
        /// <param name="maId">���뵥�ݱ�</param>
        /// <param name="maNo">���뵥��</param>
        public void RollbackMtnMa(string maId, string maNo)
        {
            DbMTNMa _ma = new DbMTNMa(Comp.Conn_DB);
            _ma.RollbackMtnMa(maId, maNo);
        }
        #endregion

        #region ���¿ͻ�����
        /// <summary>
        /// ����ͻ���������뵥��
        /// </summary>
        /// <param name="dr">ά�������ͷ��</param>
        /// <param name="isAdd">�Ƿ�����</param>
        private void UpdateQa(DataRow dr, bool isAdd)
        {
            string _maId = "";
            string _maNo = "";
            string _qaNo = "";
            if (isAdd)
            {
                _maId = dr["MA_ID"].ToString();
                _maNo = dr["MA_NO"].ToString();
                _qaNo = dr["QA_NO"].ToString();
            }
            else
            {
                _maId = dr["MA_ID", DataRowVersion.Original].ToString();
                _maNo = dr["MA_NO", DataRowVersion.Original].ToString();
                _qaNo = dr["QA_NO", DataRowVersion.Original].ToString();
            }
            Qus _qus = new Qus();
            SunlikeDataSet _dsQa = null;
            SunlikeDataSet _dsMfQa = null;
            if (isAdd)
            {
                _qus.UpdateOutNo(_qaNo, _maId, _maNo);
            }
            else
            {
                if (!string.IsNullOrEmpty(_qaNo))
                {
                    //���ݿͻ����ⵥ��ѯ

                    _qus.UpdateOutNo(_qaNo, "", "");
                }
                else
                {
                    //�������뵥�Ų�ͻ����ⵥ
                    _dsQa = _qus.GetData(_maNo);
                    if (_dsQa.Tables["MF_QA"].Rows.Count > 0)
                    {
                        foreach (DataRow drQa in _dsQa.Tables["MF_QA"].Rows)
                        {
                            _dsMfQa = _qus.GetData("", drQa["QA_NO"].ToString());
                            if (_dsMfQa.Tables["MF_QA"].Rows.Count > 0)
                            {
                                _dsMfQa.Tables["MF_QA"].Rows[0]["OUT_NO"] = System.DBNull.Value;
                                _qus.UpdateData(_dsMfQa);
                            }
                        }
                    }
                }

            }
        }
        #endregion
        #endregion

        #region ��дά�����뵥����
        /// <summary>
        /// ��дά�����뵥����
        /// </summary>
        /// <param name="maId">���뵥�ݱ�</param>
        /// <param name="maNo">���뵥��</param>
        /// <param name="keyItm">�������</param>
        /// <param name="unit">��λ</param>
        /// <param name="qty">��д������</param>
        public void UpdateQtyMtn(string maId, string maNo, int keyItm, string unit, decimal qty)
        {
            string _prdNo = "";
            String _unitMa = "";
            SunlikeDataSet _dsMa = this.GetData(maId, maNo, keyItm);
            if (_dsMa != null && _dsMa.Tables["TF_MA"].Rows.Count > 0)
            {
                _prdNo = _dsMa.Tables["TF_MA"].Rows[0]["PRD_NO"].ToString();
                _unitMa = _dsMa.Tables["TF_MA"].Rows[0]["UNIT"].ToString();
            }
            //����
            Prdt _prdt = new Prdt();
            //���㵥λ
            decimal _qtyNew = _prdt.GetUnitQty(_prdNo, unit, qty, _unitMa);
            DbMTNMa _ma = new DbMTNMa(Comp.Conn_DB);
            _ma.UpdateQtyMtn(maId, maNo, keyItm, qty);
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
                if (isDel)
                {
                    wcNo = Convert.ToString(dr["WC_NO", DataRowVersion.Original]);
                    bilNo = Convert.ToString(dr["MA_NO", DataRowVersion.Original]);
                }
                else
                {
                    wcNo = Convert.ToString(dr["WC_NO"]);
                    bilNo = Convert.ToString(dr["MA_NO"]);
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
                                _dr["BIL_ID"] = dr["MA_ID"];
                                _dr["BIL_DD"] = dr.Table.DataSet.Tables["MF_MA"].Rows[0]["MA_DD"];
                                _dr["BIL_NO"] = bilNo;
                                _dr["REM"] = dr["REM"];
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
                                _dr["REM"] = dr["REM"];
                            }   
                        }
                        _wc.UpdateDataWcH(_ds);
                    }

                    #endregion
                }

            }
        }
        #endregion

        #region ɾ����Լ�������뵥
        /// <summary>
        /// ɾ����Լ�������뵥
        /// </summary>
        /// <param name="pacNo"></param>
        /// <param name="strStaDd"></param>
        /// <param name="maNos"></param>
        /// <returns></returns>
        public string DeleteMa(string pacNo, string strStaDd, out ArrayList maNos)
        {
            string _errorMsg = "";
            string _sqlWhere = "";
            maNos = new ArrayList();
            DateTime _startDd = System.DateTime.Now;
            _sqlWhere = " AND BIL_ID='KH' AND BIL_NO='" + pacNo + "'";
            if (!string.IsNullOrEmpty(strStaDd))
            {
                _startDd = Convert.ToDateTime(strStaDd);
                _sqlWhere += " AND MA_DD >='" + _startDd.ToString("yyyy-MM-dd") + "' ";                
            }
            SunlikeDataSet _dsMas = this.GetData(_sqlWhere);
            SunlikeDataSet _dsMa = null;
            foreach (DataRow dr in _dsMas.Tables["MF_MA"].Rows)
            {
                _dsMa = this.GetUpdateData("", "", dr["MA_ID"].ToString(), dr["MA_NO"].ToString(), false);
                maNos.Add(dr["MA_NO"].ToString());
                if (_dsMa.Tables["MF_MA"].Rows.Count > 0)
                {                    
                    _dsMa.Tables["MF_MA"].Rows[0].Delete();
                    try
                    {
                        this.UpdateData("",_dsMa, true);
                    }
                    catch (Exception _ex)
                    {
                        _errorMsg = _ex.Message.ToString();
                    }
                }
            }
            return _errorMsg;
        }
        #endregion

        #region IAuditing Members
        /// <summary>
        /// Approve
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _errorMsg = "";
            DbMTNMa _db = new DbMTNMa(Comp.Conn_DB);
            SunlikeDataSet _ds = _db.GetData(bil_id, bil_no, false);
            if (_ds.Tables.Contains("MF_MA") && _ds.Tables["MF_MA"].Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(_ds.Tables["MF_MA"].Rows[0]["MTN_FLOW"].ToString()))
                {
                    _errorMsg = /*ά������*/"RCID=MTN.HINT.MTN_FLOW_NULL";
                }
            }
            if (string.IsNullOrEmpty(_errorMsg))
            {
                this.UpdateMtnMa(bil_id, bil_no, chk_man, cls_dd);
            }
            #region ���޿�ά�޼�¼
            DataTable _dt = _ds.Tables["TF_MA"];
            if (null != _dt)
            {
                foreach (DataRow dr in _dt.Select())
                {
                    UpdateWCH(dr,false, true);
                }
            }
            #endregion
            return _errorMsg;
        }
        /// <summary>
        /// Deny
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            return "";
        }
        /// <summary>
        /// RollBack
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            string _error = String.Empty;
            try
            {
                DbMTNMa _db = new DbMTNMa(Comp.Conn_DB);
                SunlikeDataSet _ds = _db.GetData(bil_id, bil_no, false);
                if (_ds.Tables.Contains("MF_MA") && _ds.Tables["MF_MA"].Rows.Count > 0)
                {
                    //�����ѽ᰸�����ܷ����
                    string _errorMsg = this.SetCanModify("MTNMa", _loginUsr, _ds, false);
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
                    for (int i = 0; i < _ds.Tables["TF_MA"].Rows.Count; i++)
                    {
                        //�ж��Ƿ���ת����һ������
                        if (!string.IsNullOrEmpty(_ds.Tables["TF_MA"].Rows[i]["QTY_MTN"].ToString()))
                        {
                            if (Convert.ToDecimal(_ds.Tables["TF_MA"].Rows[i]["QTY_MTN"]) > 0)
                            {
                                throw new SunlikeException("MTN.HINT.RB_MA");//��װά����������0,���ܷ����.
                            }
                        }

                    }
                    //��������
                    this.RollbackMtnMa(bil_id, bil_no);
                    #region ���޿�ά�޼�¼
                    DataTable _dt = _ds.Tables["TF_MA"];
                    if (null != _dt)
                    {
                        foreach (DataRow dr in _dt.Select())
                        {
                            UpdateWCH(dr, true, true);
                        }
                    }
                    #endregion
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
        /// <summary>
        /// �ֹ��᰸
        /// </summary>
        /// <param name="maId">���뵥�ݱ�</param>
        /// <param name="maNo">���뵥��</param>
        /// <param name="clsName">�᰸�ֶ�</param>
        /// <param name="close">�Ƿ�᰸</param>
        /// <returns></returns>
        private string CloseBill(string maId,string maNo,string clsName,bool close)
        {
            string _errorMsg = "";
            DbMTNMa _db = new DbMTNMa(Comp.Conn_DB);
            SunlikeDataSet _ds = _db.GetData(maId, maNo, false);
            DataTable _drHead = _ds.Tables["MF_MA"];
            DataTable _drBody = _ds.Tables["TF_MA"];
            if (_drHead != null && _drHead.Rows.Count > 0)
            {
                bool _clsId = Convert.ToString(_drHead.Rows[0]["CLS_ID"]) == "T";
                string _chkMan = _drHead.Rows[0]["CHK_MAN"].ToString();

                if (string.IsNullOrEmpty(_chkMan))
                    return "";//����δ���
                if (close && _clsId)
                    return "RCID=COMMON.HINT.CLOSEERROR,PARAM=" + maNo;//�õ���[{0}]�ѽ᰸,�᰸�����������!
                if (!close && !_clsId)
                    return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + maNo;//�õ���[{0}]δ�᰸,δ�᰸�����������!             
                _db.UpdateClsId(maId, maNo, clsName, close);
            }
            return _errorMsg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {

            return CloseBill(bil_id, bil_no, cls_name, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string UndoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            return CloseBill(bil_id, bil_no, cls_name, false);
        }

        #endregion
    }
}
