using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections;

namespace Sunlike.Business
{
    /// <summary>
    /// ��������.
    /// </summary>e
    public class MonEX : Sunlike.Business.BizObject, Sunlike.Business.IAuditing, Sunlike.Business.IAuditingInfo
    {
        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        public enum ExpType
        {
            /// <summary>
            /// �ͻ�����
            /// </summary>
            CUSTER,
            /// <summary>
            /// ������������
            /// </summary>
            MONEXR,
            /// <summary>
            /// ��������֧�� 
            /// </summary>
            MONEXP
        }
        #endregion

        #region variable
        private bool _isRunAuditing;//�Ƿ����������
        private bool _reBuildVohNo = false;//�Ƿ��ؽ�ƾ֤����
        private bool _makeVohNo = false;//�Ƿ�ǿ����ƾ֤
        #endregion

        #region ���캯��
        /// <summary>
        /// MonEX
        /// </summary>
        public MonEX()
        {
        }
        #endregion

        #region GetData
        /// <summary>
        ///	 ȡ�÷��õ�����ȡPGMȨ�ޣ�InsertSpcPswd
        /// </summary>
        /// <param name="expType">��������</param>
        /// <param name="usr">¼����</param>
        /// <param name="epId">���ݱ�</param>
        /// <param name="epNo">���ݺ�</param>
        /// <param name="isSchema">�Ƿ�ȡSchema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm, ExpType expType, string usr, string epId, string epNo, bool isSchema)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _expDataSet = _dbMonEX.GetData(epId, epNo, isSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _expDataSet.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            if (!String.IsNullOrEmpty(usr))
            {
                DataTable _mf_expTable = _expDataSet.Tables["MF_EXP"];
                if (string.IsNullOrEmpty(pgm))
                {
                    switch (expType)
                    {
                        case ExpType.CUSTER://�ͻ�����
                            pgm = "CUSTER";
                            break;
                        case ExpType.MONEXR://	������������	
                            pgm = "MONEXR";
                            break;
                        case ExpType.MONEXP://��������֧��	
                            pgm = "MONEXP";
                            break;
                        default:
                            pgm = "MONEXR";
                            break;
                    }
                }

                if (_mf_expTable.Rows.Count > 0)
                {
                    string _bill_Dep = _mf_expTable.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _mf_expTable.Rows[0]["USR"].ToString();
                    System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                    _expDataSet.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _expDataSet.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _expDataSet.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _expDataSet.ExtendedProperties["LCK"] = _billRight["LCK"];
                }
            }
            this.SetCanModify(_expDataSet, true);
            if (_expDataSet.Tables.Contains("TF_EXP"))
            {
                //if (!string.IsNullOrEmpty(epNo))
                //{
                    _expDataSet.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrement = true;
                    _expDataSet.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementSeed = _expDataSet.Tables["TF_EXP"].Rows.Count > 0 ? Convert.ToInt32(_expDataSet.Tables["TF_EXP"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
                    _expDataSet.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementStep = 1;
                    _expDataSet.Tables["TF_EXP"].Columns["KEY_ITM"].Unique = true;
               // }

            }
            return _expDataSet;
        }
        /// <summary>
        /// ȡ�÷��õ�����Ȩ�ޣ�
        /// </summary>
        /// <param name="epId">�������</param>
        /// <param name="epNo">���ݺ�</param>
        /// <param name="isSchema">�Ƿ�ȡSchema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string epId, string epNo, bool isSchema)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMonEX.GetData(epId, epNo, isSchema);
            this.SetCanModify(_ds, true);
            //if (_ds.Tables.Contains("TF_EXP"))
            //{
            //    _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrement = true;
            //    _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_EXP"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_EXP"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            //    _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementStep = 1;
            //    _ds.Tables["TF_EXP"].Columns["KEY_ITM"].Unique = true;
            //}
            return _ds;
        }
        #endregion

        #region ����ʱ��������
        /// <summary>
        /// ����ʱ��������
        /// </summary>
        /// <param name="MonEX">DataSet</param>
        /// <param name="epId">���ݱ�</param>
        /// <param name="usr">��½��</param>
        public void AddNewData(SunlikeDataSet MonEX, string epId, string usr)
        {
            DataTable _mf_expTable = MonEX.Tables["MF_EXP"];
            DataRow _dr = _mf_expTable.NewRow();
            try
            {
                _dr["EP_ID"] = epId;
                SQNO _sqlNo = new SQNO();
                _dr["EP_NO"] = _sqlNo.Get(epId, usr, "", System.DateTime.Now, "FX");
                _dr["EP_DD"] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                _dr["USR"] = usr;
                _dr["OPN_ID"] = "2";
                _dr["PRT_SW"] = "N";
                _dr["BIL_TYPE"] = "FX";
                _dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                _mf_expTable.Rows.Add(_dr);
                //MonEX.AcceptChanges();
            }
            catch (Exception _ex)
            {
                throw new Sunlike.Common.Utility.SunlikeException(_ex.Message);
            }

        }
        #endregion

        #region ���DataSet�Ƿ�����޸�
        /// <summary>
        /// ���DataSet�Ƿ�����޸�
        /// �����Ƿ����޸ĵĴ������(0:���޸� 1:�ѽ���������� 2:�Ѳ���Ӧ���˿�3:������Դ����4:����)
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">�Ƿ��ж��������</param>
        /// <returns>�����Ƿ����޸ĵĴ������(0:���޸� 1:�ѽ���������� 2:�Ѳ���Ӧ���˿�3:������Դ����4:����)</returns>
        private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)/*MODIFY LWH 090907 ��Ӳ����޸���ʾ*/
        {
            bool _bCanModify = true;
            string _result = "0";
            DataTable _dtMf = ds.Tables["MF_EXP"];
            DataTable _dtTf = ds.Tables["TF_EXP"];



            if (_dtMf.Rows.Count > 0)
            {
                #region �жϹ�����

                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["EP_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_MON"))
                {
                    _bCanModify = false;
                    _result = "RCID=COMMON.HINT.HASCLOSEBILL";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");/*�ѹ��˲����޸�*/
                }
                #endregion

                #region �ж��Ƿ���Ҫ���
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_dtMf.Rows[0]["EP_ID"].ToString(), _dtMf.Rows[0]["EP_NO"].ToString()))
                    {
                        _bCanModify = false;
                        _result = "1";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");/*�ѽ���������̲����޸�*/

                    }
                }
                #endregion

                #region �ж��Ƿ�����
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    _result = "4";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");/*�����������޸�*/
                }
                #endregion

                #region �ж��Ƿ��ѿ���Ʊ
                if (true)
                {
                    Arp _arp = new Arp();
                    foreach (DataRow dr in _dtTf.Rows)
                    {
                        Decimal _amtnRcv = 0;
                        if (!String.IsNullOrEmpty(dr["ARP_NO"].ToString()))
                        {
                            if (!string.IsNullOrEmpty(dr["AMT_RP"].ToString()))
                            {
                                _amtnRcv = Convert.ToDecimal(dr["AMT_RP"]);
                            }
                            if (Convert.ToDecimal(dr["AMT"]) != 0)
                            {
                                if (_arp.HasReceiveDollar(dr["ARP_NO"].ToString(), _amtnRcv))
                                {
                                    _bCanModify = false;
                                    // _result = "2";
                                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LZ_MODIFY");/* �ѿ�����Ʊ���˲����޸�*/
                                    break;
                                }
                            }
                        }
                    }
                }
                #endregion

                #region �ж��Ƿ�����Դ��
                //if (_bCanModify)
                //{

                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["PC_NO"].ToString()))
                {
                    _bCanModify = false;
                    _result = "3";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CANNOTMODIFY");/*������Դ���ţ������޸ģ�*/
                }
                // }
                #endregion

                #region �ж��Ƿ����
                //foreach (DataRow _dr in _dtTf.Rows)
                //{
                //    if (_dr.RowState != DataRowState.Deleted && _dr.RowState != DataRowState.Detached)
                //    {
                //        string _arpNO = _dr["ARP_NO"].ToString();
                //        if (!string.IsNullOrEmpty(_arpNO))
                //        {
                //            Arp _arp = new Arp();
                //            try
                //            {
                //                if (_arp.HasReceiveDollar(_arpNO))
                //                {
                //                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_WRITEUP");
                //                    _bCanModify = false;
                //                    break;
                //                }
                //            }
                //            catch (Exception ex) { throw new Exception(ex.ToString()); }
                //        }
                //    }
                //}

                #endregion

                #region �ж�ƾ֤
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["VOH_NO"].ToString()))
                {
                    string _acNo = "";
                    DrpVoh _drpVoh = new DrpVoh();
                    string _updUsr = "";
                    if (ds.ExtendedProperties.ContainsKey("UPD_USR"))
                    {
                        _updUsr = ds.ExtendedProperties["UPD_USR"].ToString();
                    }
                    else
                    {
                        _updUsr = _dtMf.Rows[0]["USR"].ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_dtMf.Rows[0]["VOH_NO"].ToString(), _updUsr, ref _acNo);
                    if (_resVoh == 0 || _resVoh == 1)
                    {
                        ds.ExtendedProperties["BILL_VOH_AC_CONTROL"] = false;
                        ds.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                    else if (_resVoh == 2)
                    {
                        ds.ExtendedProperties["BILL_VOH_AC_CONTROL"] = true;
                        ds.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                }
                #endregion
            }


            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return _result;
        }
        #endregion

        #region ����ǰ�����



        /// <summary>
        /// ��չ����:
        /// MAKE_VOH_NO  �Ƿ�ǿ�Ʋ���ƾ֤��: T:ǿ������ƾ֤����
        /// </summary>
        /// <param name="changedDs">DataSet</param>
        /// <param name="pgm" >PGM</param>
        /// <param name="bubbleException">�Ƿ��׳��쳣��trueΪֱ���׳��쳣��false����ErrorTable��</param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm, SunlikeDataSet changedDs, bool bubbleException)
        {
            string _epId, _usr;
            DataRow _dr = changedDs.Tables["MF_EXP"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _usr = _dr["USR", DataRowVersion.Original].ToString();
                _epId = _dr["EP_ID", DataRowVersion.Original].ToString();
            }
            else
            {
                _usr = _dr["USR"].ToString();
                _epId = _dr["EP_ID"].ToString();
            }

            //�Ƿ��ؽ�ƾ֤����
            if (changedDs.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (String.Compare("True", changedDs.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            //�Ƿ�ǿ�Ʋ���ƾ֤��
            if (changedDs.ExtendedProperties.ContainsKey("MAKE_VOH_NO"))
            {
                if (String.Compare("T", changedDs.ExtendedProperties["MAKE_VOH_NO"].ToString()) == 0)
                {
                    this._makeVohNo = true;
                }
            }

            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht.Add("MF_EXP", "EP_DD, EP_NO, REM, USR, PRT_SW, EP_ID, USR_NO, OPN_ID, SYS_DATE,BIL_TYPE,DEP,CHK_MAN,PC_NO,CLS_DATE,BIL_ID,VOH_ID,VOH_NO");
            _ht.Add("TF_EXP", "ITM, IDX_NO, CUS_NO, CUR_ID, EXC_RTO, TAX_ID, AMT, AMTN_NET, TAX, ARP_NO, ACC_NO, DEP, INV_NO, BAT_NO, REM, PAY_REM, AMT_RP, RP_NO, SAL_NO, PAY_DD, EP_NO, EP_ID, SHARE_MTH, CHK_DD, CHK_DAYS, CLOSE_FT, AMTN_FT_TOT, PAY_MTH, PAY_DAYS, CLS_REM, INT_DAYS, METH_ID, COMPOSE_IDNO, INV_DD, INV_YM, TITLE_BUY, AMT_INV, TAX_INV,RTO_TAX,BIL_ID,BIL_NO,KEY_ITM,AMTN_NET_FP,AMT_FP,TAX_FP,LZ_CLS_ID,CLSLZ");

            //�ж��Ƿ����������
            Auditing _auditing = new Auditing();
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
            //_isRunAuditing = _auditing.IsRunAuditing(_epId, _usr, _bilType, _mobID);


            this.UpdateDataSet(changedDs, _ht);

            //�жϵ����ܷ��޸�
            if (!changedDs.HasErrors)
            {
                //���ӵ���Ȩ��
                string _UpdUsr = "";
                if (changedDs.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changedDs.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    if (pgm == null || pgm == string.Empty)
                    {
                        pgm = "DRP" + _epId;
                        switch (_epId)
                        {
                            case "ER"://������������	
                                pgm = "MONEXR";
                                break;
                            case "EP"://��������֧��	
                                pgm = "MONEXP";
                                break;
                            default://�ͻ�����
                                pgm = "CUSTER";
                                break;
                        }
                    }
                    DataTable _dtMf = changedDs.Tables["MF_EXP"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changedDs.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDs.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDs.ExtendedProperties["PRN"] = _billRight["PRN"];
                        changedDs.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
                this.SetCanModify(changedDs, true);
            }
            else
            {
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDs);
                    throw new SunlikeException("RCID=MonEX.UpdateData() Error:;" + _errorMsg);
                }
            }
            return GetAllErrors(changedDs);
        }
        /// <summary>
        /// BeforeUpdate
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            string _ep_No = ""; //���ݺ���
            string _ep_Id = "";//����ID
            string _usr = "";//��½�û�
            if (dr.RowState != DataRowState.Deleted)
            {
                _ep_No = dr["EP_NO"].ToString();
                _ep_Id = dr["EP_ID"].ToString();
                _usr = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR"].ToString();
            }
            else
            {
                _ep_No = dr["EP_NO", DataRowVersion.Original].ToString();
                _ep_Id = dr["EP_ID", DataRowVersion.Original].ToString();
                _usr = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR", DataRowVersion.Original].ToString();
            }
            if (statementType != StatementType.Insert)
            {
                //�ж��Ƿ�����������Ѿ����������޸ġ�
                Users _Users = new Users();
                string _whereStr = "EP_ID = '" + _ep_Id + "' AND EP_NO = '" + _ep_No + "'";
                if (_Users.IsLocked("MF_EXP", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }

                Auditing _auditing = new Auditing();
                if (_auditing.GetIfEnterAuditing(_ep_Id, _ep_No))//�����ȥ����˾Ͳ����޸ĺ�����ɾ��
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");
                }
            }
            #region ���������ж�
            if (tableName == "MF_EXP")
            {
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["EP_DD"]), dr["DEP"].ToString(), "CLS_MON"))
                    {
                        CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL1,PARAM=" + _compInfo.CloseAccountDateInfo.CLS_MON.ToString(_compInfo.SystemInfo.DATEPATTERN));
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["EP_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MON"))
                    {
                        CompInfo _compInfo = Comp.GetCompInfo(dr["DEP", DataRowVersion.Original].ToString());
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL1,PARAM=" + _compInfo.CloseAccountDateInfo.CLS_MON.ToString(_compInfo.SystemInfo.DATEPATTERN));
                    }
                }
            }
            #endregion


            #region ���ݼ��
            if (tableName == "MF_EXP" && statementType != StatementType.Delete)
            {
                //��龭����
                Salm _salm = new Salm();
                if (!_salm.IsExist(_usr, dr["USR_NO"].ToString()))
                {
                    dr.SetColumnError("USR_NO",/*������[{0}]�����ڻ�û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.MAN_NO_NOTEXIST,PARAM=" + dr["USR_NO"].ToString());
                    status = UpdateStatus.SkipAllRemainingRows;
                }

                // ����(����)
                Dept _dept = new Dept();
                if (!_dept.IsExist(_usr, dr["DEP"].ToString()))
                {
                    dr.SetColumnError("DEP",/*���Ŵ���[{0}]�����ڻ�û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                    status = UpdateStatus.SkipAllRemainingRows;
                }
            }

            if (tableName == "TF_EXP" && statementType != StatementType.Delete)
            {
                if (!String.IsNullOrEmpty(dr["AMT_RP"].ToString()))
                {
                    if (dr["AMT"] == null || dr["AMT"] == DBNull.Value)
                    {
                        dr["AMT"] = 0;
                    }
                    if (dr["AMT_RP"] == null || dr["AMT_RP"] == DBNull.Value)
                    {
                        dr["AMT_RP"] = 0;
                    }
                    if (Convert.ToDecimal(dr["AMT"]) < Convert.ToDecimal(dr["AMT_RP"]))
                    {
                        dr.SetColumnError("AMT_RP", /*���ո����ɴ������˽�*/"RCID=MON.HINT.AMTRP,PARAM=" + dr["AMT_RP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }

                //��������Ŀ(����)
                Indx1 _indx1 = new Indx1();
                if (!_indx1.IsExists(dr["IDX_NO"].ToString()))
                {
                    dr.SetColumnError("IDX_NO",/*���ÿ�Ŀ[{0}]�����ڻ�û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.NOIDX1,PARAM=" + dr["IDX_NO"].ToString());
                    status = UpdateStatus.SkipAllRemainingRows;
                }

                //���ͻ����� (����)
                Cust _cust = new Cust();
                if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString()))
                {
                    dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//�ͻ����Ų����ڻ�û�ж��������Ȩ��,����
                    status = UpdateStatus.SkipAllRemainingRows;
                }

                //����ƿ�Ŀ
                BTMng _bTMng = new BTMng();
                if (!String.IsNullOrEmpty(dr["ACC_NO"].ToString()))
                {
                    if (!_bTMng.IsExistsAccNo(dr["ACC_NO"].ToString()))
                    {
                        dr.SetColumnError("ACC_NO",/*��ƿ�Ŀ[{0}]�����ڻ�û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.ACCNOERROR,PARAM=" + dr["ACC_NO"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else
                    {
                        Accn _accn = new Accn();
                        if (_accn.CheckCls(dr["ACC_NO"].ToString()))
                        {
                            dr.SetColumnError("ACC_NO",/*����ͳ�ο�Ŀ*/"RCID=MON.HINT.NOCLS");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
                //����
                Dept _dept = new Dept();
                if (!String.IsNullOrEmpty(dr["DEP"].ToString()))
                {
                    if (!_dept.IsExist(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR"].ToString(), dr["DEP"].ToString()))
                    {
                        dr.SetColumnError("DEP",/*���Ŵ���[{0}]�����ڻ�û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }

                //��̯��ʽ
                if (String.IsNullOrEmpty(dr["SHARE_MTH"].ToString()))
                {
                    dr["SHARE_MTH"] = "1";
                }
            }

            #endregion

            string _saveId = "T";
            string _billAuditing = "";
            //�ж���Դ���Ƿ���Ҫ���� SAVE_ID=FΪ����(����Ҫд������̲��Ҳ��������)������Ϊ�����������
            if (SunlikeDataSet.ConvertTo(dr.Table.DataSet).ExtendedProperties.Contains("SAVE_ID"))
            {
                _saveId = SunlikeDataSet.ConvertTo(dr.Table.DataSet).ExtendedProperties["SAVE_ID"].ToString();
            }
            if (SunlikeDataSet.ConvertTo(dr.Table.DataSet).ExtendedProperties.Contains("BILL_AUDITING"))
            {
                _billAuditing = SunlikeDataSet.ConvertTo(dr.Table.DataSet).ExtendedProperties.Contains("BILL_AUDITING").ToString();
            }
            if (tableName == "MF_EXP")
            {
                //����ʱ�жϹ�������
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["EP_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                    {
                        throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["EP_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                    {
                        throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                SQNO _sqNo = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //ȡ����
                    dr["EP_NO"] = _sqNo.Set(_ep_Id, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["EP_DD"]), dr["BIL_TYPE"].ToString());
                    //д��Ĭ����λֵ
                    dr["PRT_SW"] = "N";
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    dr["OPN_ID"] = "2";
                }

                //����ƾ֤
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }
                // if (String.IsNullOrEmpty(_billAuditing) && string.IsNullOrEmpty(_saveId))
                if (String.IsNullOrEmpty(_billAuditing) && _saveId == "T")//MODIFY LWH 090914 ������˱���Ҳû��д��CHK_MAN��ֵ�޸ġ�
                {
                    //#region ��˹���
                    //AudParamStruct _aps = new AudParamStruct();
                    //if (statementType != StatementType.Delete)
                    //{
                    //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                    //    _aps.BIL_ID = dr["EP_ID"].ToString();
                    //    _aps.BIL_NO = dr["EP_NO"].ToString();
                    //    _aps.BIL_DD = Convert.ToDateTime(dr["EP_DD"]);
                    //    _aps.USR = dr["USR"].ToString();
                    //    _aps.DEP = dr["DEP"].ToString();
                    //    _aps.SAL_NO = dr["USR_NO"].ToString();
                    //    //_aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
                    //}
                    //else
                    //    _aps = new AudParamStruct(Convert.ToString(dr["EP_ID", DataRowVersion.Original]), Convert.ToString(dr["EP_NO", DataRowVersion.Original]));
                    //Auditing _auditing = new Auditing();
                    //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                    //if (!string.IsNullOrEmpty(_auditErr))
                    //{
                    //    throw new SunlikeException(_auditErr);
                    //}
                    //#endregion
                }
            }
            else if ((tableName == "TF_EXP"))
            {
                if (!_isRunAuditing && (_saveId == "T" || _billAuditing == "T"))
                {
                    if (statementType == StatementType.Delete)
                    {
                        //ɾ����ʱ����ɾ���տ���ֳ�����ɾ�����˵�
                        //�տ����
                        this.UpdateMon(dr, false);
                        //���˲���
                        this.UpdateArp(dr);
                    }
                    else
                    {
                        //���˲���
                        this.UpdateArp(dr);
                        //�տ����
                        this.UpdateMon(dr, false);
                    }
                }
                else
                {
                    this.UpdateMon(dr, true);
                }
                if (dr.RowState == DataRowState.Deleted)
                {
                    //�ж��Ƿ��в�����Ʊ������ɾ��
                    checktflz(dr);

                }
            }
            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }

        private object SunlikeException(string _auditErr)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// BeforeDsSave
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dtMf = ds.Tables["MF_EXP"];
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["EP_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["EP_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion
        }
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
            if (tableName == "MF_EXP" && statementType == StatementType.Delete)
            {
                SQNO _sqNo = new SQNO();
                _sqNo.Delete(dr["EP_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());

            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        public void SetArp(string epId, string epNo)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMonEX.GetData(epId, epNo, false);
            DataTable _mf_expTable = _ds.Tables["MF_EXP"];
            DataTable _tf_expTable = _ds.Tables["TF_EXP"];
            if (_mf_expTable.Rows.Count > 0 && (!Convert.IsDBNull(_mf_expTable.Rows[0]["USR"])))
            {
                Arp _arp = new Arp();
                string _bilDd = _mf_expTable.Rows[0]["EP_DD"].ToString();
                string _dep = _mf_expTable.Rows[0]["DEP"].ToString();
                string _usr = _mf_expTable.Rows[0]["USR"].ToString();

                string _curId = "";
                string _excrto = ""; //�������¼
                string _amtn = ""; //�������¼
                string _amtn_net = ""; //�������¼
                string _amt = ""; //�������¼
                string _tax = ""; //�������¼
                string _arpNo = "";
                string _arpId = "";

                for (int i = 0; i < _tf_expTable.Rows.Count; i++)
                {
                    Cust _cust = new Cust();
                    string _cls2 = _cust.GetCustCls2(_tf_expTable.Rows[0]["CUS_NO"].ToString());
                    if (_cls2 == "1")
                    {
                        if (Convert.IsDBNull(_tf_expTable.Rows[i]["ARP_NO"]))
                        {
                            System.Collections.Hashtable _ht = _cust.GetPAYInfo(_tf_expTable.Rows[0]["CUS_NO"].ToString(), _mf_expTable.Rows[0]["EP_DD"].ToString());
                            if (_ht["PAY_DD"] != null)
                            {
                                _tf_expTable.Rows[i]["PAY_DD"] = _ht["PAY_DD"].ToString();
                            }
                            if (_ht["PAY_REM"] != null)
                            {
                                _tf_expTable.Rows[i]["PAY_REM"] = _ht["PAY_REM"].ToString();
                            }
                            if (_ht["CHK_DD"] != null)
                            {
                                _tf_expTable.Rows[i]["CHK_DD"] = _ht["CHK_DD"].ToString();
                            }
                            if (_ht["CHK_DAYS"] != null)
                            {
                                _tf_expTable.Rows[i]["CHK_DAYS"] = _ht["CHK_DAYS"].ToString();
                            }
                            if (_ht["PAY_DAYS"] != null)
                            {
                                _tf_expTable.Rows[i]["PAY_DAYS"] = _ht["PAY_DAYS"].ToString();
                            }
                            if (_ht["INT_DAYS"] != null)
                            {
                                _tf_expTable.Rows[i]["INT_DAYS"] = _ht["INT_DAYS"].ToString();
                            }
                            if (_ht["PAY_MTH"] != null)
                            {
                                _tf_expTable.Rows[i]["PAY_MTH"] = _ht["PAY_MTH"].ToString();
                            }
                            _excrto = _tf_expTable.Rows[i]["EXC_RTO"].ToString();
                            _amtn = Convert.ToString(Convert.ToDecimal(_tf_expTable.Rows[i]["AMTN_NET"].ToString()) + Convert.ToDecimal(_tf_expTable.Rows[i]["TAX"].ToString()));
                            _amtn_net = _tf_expTable.Rows[i]["AMTN_NET"].ToString();
                            if (!Convert.IsDBNull(_tf_expTable.Rows[i]["AMT"].ToString()))
                                _amt = _tf_expTable.Rows[i]["AMT"].ToString();
                            else
                                _amt = null;
                            if (!Convert.IsDBNull(_tf_expTable.Rows[i]["TAX"].ToString()))
                                _tax = _tf_expTable.Rows[i]["TAX"].ToString();
                            else
                                _tax = null;
                            if (Convert.IsDBNull(_tf_expTable.Rows[i]["CUR_ID"]))
                                _curId = _tf_expTable.Rows[i]["CUR_ID"].ToString();
                            else
                                _curId = "";
                            if (epId == "EP")
                                _arpId = "2";
                            else
                                _arpId = "1";
                            _arpNo = _arp.UpdateMfArp(_arpId, "2", epId, epNo, Convert.ToDateTime(_bilDd), "", _dep, _usr, _curId, Convert.ToDecimal(_excrto), Convert.ToDecimal(_amtn), Convert.ToDecimal(_amtn_net), Convert.ToDecimal(_amt), Convert.ToDecimal(_tax), _tf_expTable.Rows[i], "");
                            _tf_expTable.Rows[i]["ARP_NO"] = _arpNo;
                        }
                    }
                }
                this.UpdateDataSet(_ds);
            }
        }

        /// <summary>
        /// ���˲���
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="isAdd"></param>
        private string UpdateArp(DataRow dr)
        {
            decimal _amtn = 0;//δ˰��λ��
            decimal _amt = 0;//���
            decimal _tax = 0;//˰��
            string _arpId = "1";//Ӧ�ո�ע�ǣ�1Ӧ�գ�2Ӧ����
            decimal _rtoTax = 0;//˰��RTO_TAX
            if (dr.RowState != DataRowState.Deleted)
            {
                if (!String.IsNullOrEmpty(dr["RTO_TAX"].ToString()))
                {
                    _rtoTax = Convert.ToDecimal(dr["RTO_TAX"]) / 100;
                }
                else
                {
                    _rtoTax = 1;
                }
                if (!String.IsNullOrEmpty(dr["AMT"].ToString()))
                {
                    _amt += Convert.ToDecimal(dr["AMT"]);
                    if (dr["TAX_ID"].ToString() == "3")
                    {
                        _amt += _amt * _rtoTax;
                    }
                }
                if (!String.IsNullOrEmpty(dr["AMTN_NET"].ToString()))
                {
                    _amtn += Convert.ToDecimal(dr["AMTN_NET"]);
                }
                if (!String.IsNullOrEmpty(dr["TAX"].ToString()))
                {
                    _tax += Convert.ToDecimal(dr["TAX"]);
                }
                if (dr["EP_ID"].ToString() == "EP")
                {
                    _arpId = "2";
                }
            }
            Arp _arp = new Arp();
            string _arpNo = "";

            //�޸ĺ�ɾ������ǰ�ж����˵��Ƿ��ɾ��
            if (dr.RowState != DataRowState.Added && dr.RowState != DataRowState.Unchanged)
            {
                _arpNo = dr["ARP_NO", DataRowVersion.Original].ToString();
                //ȡ�ñ������տ��Ϣ
                Bills _bills = new Bills();
                string _rpId = "1";
                if (dr["EP_ID", DataRowVersion.Original].ToString() == "EP")
                {
                    _rpId = "2";
                }
                string _rpNo = "";
                _rpNo = dr["RP_NO", DataRowVersion.Original].ToString();
                SunlikeDataSet _dsMon = _bills.GetData(_rpId, _rpNo, false);
                if (!_arp.DeleteMfArp(_arpNo, _dsMon.Tables["TF_MON"], true))
                {
                    throw new SunlikeException("�޷�ɾ�����ʵ�:" + _arpNo);
                }
            }
            //�������޸�ʱ�������µ����˵�
            if (dr.RowState != DataRowState.Deleted && _amt != 0)
            {
                decimal _excRto = 1;
                string _usr = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR"].ToString();
                string _bilType = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["BIL_TYPE"].ToString();
                if (!String.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                    _excRto = Convert.ToDecimal(dr["EXC_RTO"]);

                Indx1 indx1 = new Indx1();
                SunlikeDataSet indxDs = indx1.GetData(dr["IDX_NO"].ToString());
                string _rem = indxDs.Tables[0].Rows[0]["NAME"].ToString() + ";" + dr["REM"].ToString();

                _arpNo = _arp.UpdateMfArp(_arpId, "2", dr["EP_ID"].ToString(), dr["EP_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["EP_DD"]), _bilType,
                    dr["DEP"].ToString(), _usr, dr["CUR_ID"].ToString(), _excRto, _amtn + _tax, _amtn, _amt, _tax, dr, _rem);
                if (_arpNo != dr["ARP_NO"].ToString())
                {
                    dr["ARP_NO"] = _arpNo;
                }
            }
            return _arpNo;
        }

        /// <summary>
        /// �������˵���
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        /// <param name="arpNo"></param>
        /// <param name="keyItm"></param>
        private void UpdataArpNo(string epId, string epNo, string arpNo, int keyItm)
        {
            DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
            _exp.UpdateArpNo(epId, epNo, arpNo, keyItm);
        }
        #endregion

        #region �����ո���

        /// <summary>
        /// �����ո���
        /// </summary>
        /// <param name="dr">������������/֧������</param>
        /// <param name="chk">�Ƿ����������</param>
        private void UpdateMon(DataRow dr, bool chk)
        {
            DateTime _epDd = DateTime.Now;
            DateTime _clsDd = DateTime.Now;
            string _rpId = "2";
            string _usr = "";
            string _chkMan = "";
            string _usrNo = "";
            string _salNo = "";
            string _dep = "";
            string _mobId = "";

            Bills _bills = new Bills();
            MonStruct _mon = new MonStruct();

            #region �ո���ݱ�rpId
            if (dr.RowState != DataRowState.Deleted)
            {
                if (string.Compare("ER", dr["EP_ID"].ToString()) == 0)
                {
                    _rpId = "1";      //������������
                }
                else
                {
                    _rpId = "2";     //��������֧��
                }
            }
            else
            {
                if (string.Compare("ER", dr["EP_ID", DataRowVersion.Original].ToString()) == 0)
                {
                    _rpId = "1";      //������������
                }
                else
                {
                    _rpId = "2";     //��������֧��
                }
            }
            #endregion

            if (dr.Table.DataSet.Tables["MF_EXP"].Rows[0].RowState != DataRowState.Deleted)
            {
                if (chk)
                {
                    _mon.AddTcMon = false;//�����ʱ��д��TC_MON
                    _chkMan = "";
                }
                else
                {
                    _mon.AddTcMon = true;
                    _chkMan = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["CHK_MAN"].ToString();
                    if (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["CLS_DATE"].ToString()))
                    {
                        _clsDd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["CLS_DATE"]);
                    }
                }

                if (!string.IsNullOrEmpty(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["EP_DD"].ToString()))
                {
                    _epDd = Convert.ToDateTime(dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["EP_DD"]);//�ո�������
                }
                _mobId = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["MOB_ID"].ToString();
                _usrNo = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR_NO"].ToString();
                _usr = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["USR"].ToString();
                if (dr.RowState != DataRowState.Deleted)
                {
                    _salNo = dr["SAL_NO"].ToString();
                }
                else
                {
                    _salNo = dr["SAL_NO", DataRowVersion.Original].ToString();
                }
                _dep = dr.Table.DataSet.Tables["MF_EXP"].Rows[0]["DEP"].ToString();
            }

            if (dr.RowState == DataRowState.Deleted)
            {
                _bills.DelRcvPay(_rpId, dr["RP_NO", DataRowVersion.Original].ToString());
            }
            else
            {
                bool rebill = true;
                if (dr.RowState == DataRowState.Modified && String.IsNullOrEmpty(dr["AMT_RP"].ToString()))
                {
                    rebill = false;
                    _bills.DelRcvPay(_rpId, dr["RP_NO", DataRowVersion.Original].ToString());
                    dr["RP_NO"] = DBNull.Value;
                }
                if (!String.IsNullOrEmpty(dr["AMT_RP"].ToString()) && rebill)
                {
                    //����Ԥ�տ��
                    _mon.Usr = _usr;
                    _mon.ChkMan = _chkMan;
                    _mon.MobId = _mobId;
                    _mon.UsrNo = _usrNo;
                    //_mon.SalNo = _salNo;
                    _mon.Dep = _dep;
                    _mon.RpDd = _epDd;
                    _mon.ClsDate = _clsDd;
                    _mon.RpId = _rpId;
                    _mon.RpNo = dr["RP_NO"].ToString();
                    _mon.BilId = dr["EP_ID"].ToString();
                    _mon.BilNo = dr["EP_NO"].ToString();
                    _mon.IrpId = "F";
                    _mon.JsfId = "T";
                    _mon.CusNo = dr["CUS_NO"].ToString();
                    _mon.CurId = dr["CUR_ID"].ToString();
                    _mon.BaccNo = dr["BACC_NO"].ToString();
                    _mon.CaccNo = dr["CACC_NO"].ToString();
                    if (string.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                        _mon.ExcRto = 1;
                    else
                        _mon.ExcRto = Convert.ToDecimal(dr["EXC_RTO"].ToString());

                    #region ����
                    if (string.IsNullOrEmpty(dr["AMT_BB"].ToString()))
                    {
                        _mon.AmtBb = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto == 1)
                        {
                            _mon.AmtBb = Convert.ToDecimal(dr["AMTN_BB"]);//ȡ��λ��
                        }
                        else
                        {
                            _mon.AmtBb = Convert.ToDecimal(dr["AMT_BB"].ToString());
                        }
                    }

                    if (string.IsNullOrEmpty(dr["AMTN_BB"].ToString()))
                    {
                        _mon.AmtnBb = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto != 1)
                        {
                            _mon.AmtnBb = Convert.ToDecimal(dr["AMT_BB"].ToString()) * _mon.ExcRto;
                        }
                        else
                        {
                            _mon.AmtnBb = Convert.ToDecimal(dr["AMTN_BB"].ToString());
                        }
                    }
                    #endregion

                    #region �ֽ�
                    if (string.IsNullOrEmpty(dr["AMT_BC"].ToString()))
                    {
                        _mon.AmtBc = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto == 1)
                        {
                            _mon.AmtBc = Convert.ToDecimal(dr["AMTN_BC"]);
                        }
                        else
                        {
                            _mon.AmtBc = Convert.ToDecimal(dr["AMT_BC"].ToString());
                        }
                    }

                    if (string.IsNullOrEmpty(dr["AMTN_BC"].ToString()))
                    {
                        _mon.AmtnBc = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto != 1)
                        {
                            _mon.AmtnBc = Convert.ToDecimal(dr["AMT_BC"].ToString()) * _mon.ExcRto;
                        }
                        else
                        {
                            _mon.AmtnBc = Convert.ToDecimal(dr["AMTN_BC"].ToString());
                        }
                    }
                    #endregion

                    #region Ʊ��

                    if (string.IsNullOrEmpty(dr["AMT_CHK"].ToString()))
                    {
                        _mon.AmtChk = 0;
                    }
                    else
                    {
                        if (_mon.ExcRto == 1)
                        {
                            _mon.AmtChk = Convert.ToDecimal(dr["AMTN_CHK"]);
                        }
                        else
                        {
                            _mon.AmtChk = Convert.ToDecimal(dr["AMT_CHK"].ToString());
                        }
                    }

                    if (string.IsNullOrEmpty(dr["AMTN_CHK"].ToString()))
                    {
                        _mon.AmtnChk = 0;
                        //������ΪTRUE����TF_MON4�ı���
                        _mon.AddMon4 = false;
                    }
                    else
                    {
                        if (_mon.ExcRto != 1)
                        {
                            _mon.AmtnChk = Convert.ToDecimal(dr["AMT_CHK"].ToString()) * _mon.ExcRto;
                        }
                        else
                        {
                            _mon.AmtnChk = Convert.ToDecimal(dr["AMTN_CHK"].ToString());
                        }

                        _mon.ChkNo = dr["CHK_NO"].ToString();
                        _mon.BankNo = dr["BANK_NO"].ToString();
                        _mon.BaccNoChk = dr["BACC_NO_CHK"].ToString();
                        if (string.IsNullOrEmpty(dr["END_DD"].ToString()))
                        {
                            _mon.EndDd = DateTime.Now;
                        }
                        else
                        {
                            _mon.EndDd = Convert.ToDateTime(dr["END_DD"].ToString());
                        }
                        _mon.ChkKnd = dr["CHK_KND"].ToString();
                        //������ΪTRUE����TF_MON4�ı���
                        if (_mon.AmtnChk != 0)
                            _mon.AddMon4 = true;
                    }

                    #endregion
                    //_mon.AmtnCls = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
                    //_mon.AmtCls = _mon.AmtBb + _mon.AmtBc + _mon.AmtChk + _mon.AmtOther;
                    _mon.Amtn = _mon.AmtnBb + _mon.AmtnBc + _mon.AmtnChk + _mon.AmtnOther;
                    _mon.ArpNo = dr["ARP_NO"].ToString();
                    dr["RP_NO"] = _bills.AddRcvPay(_mon);
                }
            }
        }

        /// <summary>
        /// �����տ��
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        /// <param name="rpNo"></param>
        /// <param name="keyItm"></param>
        private void UpdateMonRpNo(string epId, string epNo, string rpNo, int keyItm)
        {
            DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
            _exp.UpdateRpNo(epId, epNo, rpNo, keyItm);
        }
        #endregion

        #region ����ƾ֤��
        /// <summary>
        /// ����ƾ֤��
        /// </summary>
        /// <param name="dr">MF������</param>
        /// <param name="statementType"></param>
        /// <returns></returns>
        private string UpdateVohNo(DataRow dr, StatementType statementType)
        {
            string _vohNo = "";
            string _vohNoError = "";
            string _updUsr = "";
            if (dr != null && dr.Table.DataSet != null)
            {
                if (dr.Table.DataSet.ExtendedProperties.ContainsKey("UPD_USR"))
                {
                    _updUsr = dr.Table.DataSet.ExtendedProperties["UPD_USR"].ToString();
                }
            }
            if (statementType == StatementType.Insert)
            {
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                _getVoh = _compInfo.VoucherInfo.GenVoh.EXP;
                if ((_getVoh || this._makeVohNo) && !String.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, dr["EP_ID"].ToString(), out _vohNoError);
                    _vohNo = dr["VOH_NO"].ToString();
                }
            }
            else if (statementType == StatementType.Update)
            {
                DrpVoh _voh = new DrpVoh();
                if (this._reBuildVohNo)
                {
                    if (!String.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_vohNo = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                    if (!String.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                    {
                        CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                        bool _getVoh = false;
                        _getVoh = _compInfo.VoucherInfo.GenVoh.EXP;
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData(dr["EP_ID"].ToString(), dr["EP_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, dr["EP_ID"].ToString(), out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(dr["VOH_ID"].ToString()) && String.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        CompInfo _compInfo = Comp.GetCompInfo(dr["DEP"].ToString());
                        bool _getVoh = false;
                        _getVoh = _compInfo.VoucherInfo.GenVoh.EXP;
                        if (_getVoh || this._makeVohNo)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData(dr["EP_ID"].ToString(), dr["EP_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, dr["EP_ID"].ToString(), out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                    else if (string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && !string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_updUsr = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                }
            }
            else if (statementType == StatementType.Delete)
            {
                if (!String.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                }
            }
            if (dr.Table.DataSet.ExtendedProperties.ContainsKey("DRPVOH_ERROR"))
                dr.Table.DataSet.ExtendedProperties.Remove("DRPVOH_ERROR");
            if (!string.IsNullOrEmpty(_vohNoError))
            {
                dr.Table.DataSet.ExtendedProperties.Add("DRPVOH_ERROR", _vohNoError);
            }
            return _vohNo;
        }
        /// <summary>
        /// ����ƾ֤����
        /// </summary>
        /// <param name="epId">���ñ�</param>
        /// <param name="epNo">���ú���</param>
        /// <param name="vohNo">ƾ֤����</param>
        public void UpdateVohNo(string epId, string epNo, string vohNo)
        {
            DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
            _exp.UpdateVohNo(epId, epNo, vohNo);
        }
        #endregion

        #region ����˺�ɾ�����˼�¼
        /// <summary>
        /// ����˺�ɾ�����˼�¼
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        public void DeleteArp(string epId, string epNo)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMonEX.GetData(epId, epNo, false);
            DataTable _mf_expTable = _ds.Tables["TF_EXP"];
            Arp _arp = new Arp();
            try
            {
                for (int i = 0; i < _mf_expTable.Rows.Count; i++)
                {
                    if (_mf_expTable.Rows[i]["ARP_NO"] != null)
                    {
                        if (!_arp.DeleteMfArp(_mf_expTable.Rows[i]["ARP_NO"].ToString()))//ɾ�����˵�
                        {
                            throw new SunlikeException(/*�ѳ���,����ɾ���ñ�����*/"RCID=MON.HINT.ARP_NO");
                        }
                    }
                }
                DeleteArpNo(epId, epNo);//SetArpNull(pEpId,pEpNo);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        #endregion

        #region ����˺�ɾ��������Ŀ�е����˵�����Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        public void DeleteArpNo(string epId, string epNo)
        {
            DbMonEX _dbMonEX = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMonEX.GetData(epId, epNo, false);
            DataTable _tf_expTable = _ds.Tables["TF_EXP"];
            for (int i = 0; i < _tf_expTable.Rows.Count; i++)
            {
                _tf_expTable.Rows[i]["ARP_NO"] = System.DBNull.Value;
            }
            this.UpdateDataSet(_ds);
        }

        #endregion

        #region ���ݵ������ɷ��õ�
        /// <summary>
        /// ���ݵ������ɷ��õ�
        /// </summary>
        /// <param name="epId">���û�֧��</param>
        /// <param name="bilId">��������</param>
        /// <param name="bilNo">���ݺ�</param>
        /// <param name="amtn">���</param>
        /// <param name="idxNo">�������</param>
        /// <param name="cusNo">�ͻ�����</param>
        /// <param name="usr"></param>
        /// <param name="accNo"></param>
        /// <param name="dep"></param>
        /// <param name="usrNo"></param>
        public string SetExpForBill(string epId, string bilId, string bilNo, decimal amtn, string idxNo, string cusNo, string usr, string dep, string accNo, string usrNo)
        {
            DbMonEX _dbMon = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMon.GetData(bilId, bilNo);
            DataTable _dtMf = _ds.Tables["MF_EXP"];
            DataTable _dtTf = _ds.Tables["TF_EXP"];
            if (_dtMf.Rows.Count > 0)
            {
                // SetCanModify(_ds, false);
                string _modiyCode = SetCanModify(_ds, false);/*MODIFY LWH 090907*/
                if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F" && _modiyCode != "3")
                {
                    if (_modiyCode == "1")
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.INTOAUT");
                    }
                    else if (_modiyCode == "2")
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.INARP");
                    }
                    else if (_modiyCode == "4")
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.LOCKED");
                    }
                    else
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.INTOAUT");
                    }
                }
                _dtMf.Rows[0]["EP_ID"] = epId;
                _dtMf.Rows[0]["USR"] = usr;
                _dtMf.Rows[0]["USR_NO"] = usr;
                _dtTf.Rows[0]["AMT"] = amtn;
                _dtTf.Rows[0]["AMTN_NET"] = amtn;
                _dtTf.Rows[0]["TAX"] = 0;
                _dtTf.Rows[0]["IDX_NO"] = idxNo;
                _dtTf.Rows[0]["BIL_ID"] = bilId;
                _dtTf.Rows[0]["BIL_NO"] = bilNo;
                _dtTf.Rows[0]["CUS_NO"] = cusNo;
            }
            else
            {
                this.AddNewData(_ds, epId, usr);
                _ds.Tables["MF_EXP"].Rows[0]["DEP"] = dep;
                DataRow _dr = _dtTf.NewRow();
                _dr["EP_ID"] = epId;
                _dr["EP_NO"] = _dtMf.Rows[0]["EP_NO"];
                _dr["ITM"] = 1;
                _dr["AMT"] = amtn;
                _dr["AMTN_NET"] = amtn;
                _dr["TAX"] = 0;
                _dr["IDX_NO"] = idxNo;
                _dr["ACC_NO"] = accNo;
                _dr["BIL_ID"] = bilId;
                _dr["BIL_NO"] = bilNo;
                _dr["CUS_NO"] = cusNo;
                _dtTf.Rows.Add(_dr);
            }
            this.UpdateData(null, _ds, true);
            return _ds.Tables["MF_EXP"].Rows[0]["EP_NO"].ToString();
        }
        /// <summary>
        /// ɾ������ʱɾ�����õ�
        /// </summary>
        /// <param name="bilId"></param>
        /// <param name="bilNo"></param>
        public void DelExpForBill(string bilId, string bilNo)
        {
            DbMonEX _dbMon = new DbMonEX(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMon.GetData(bilId, bilNo);
            DataTable _dtMf = _ds.Tables["MF_EXP"];
            DataTable _dtTf = _ds.Tables["TF_EXP"];

            // SetCanModify(_ds, false);
            string _modiyCode = SetCanModify(_ds, false);/*MODIFY LWH 090907*/
            if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F" && _modiyCode != "3")
            {
                if (_modiyCode == "1")
                {
                    throw new SunlikeException("RCID=COMMON.HINT.INTOAUT");
                }
                else if (_modiyCode == "2")
                {
                    throw new SunlikeException("RCID=COMMON.HINT.INARP");
                }
                else if (_modiyCode == "4")
                {
                    throw new SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
                else
                {
                    throw new SunlikeException("RCID=COMMON.HINT.INTOAUT");
                }
            }
            foreach (DataRow dr in _dtTf.Rows)
            {
                dr.Delete();
            }
            foreach (DataRow dr in _dtMf.Rows)
            {
                dr.Delete();
            }
            this.UpdateDataSet(_ds);
        }
        #endregion

        #region �жϵ��ݱ����Ƿ񲹿���Ʊ������ɾ��
        /// <summary>
        /// �жϵ��ݱ����Ƿ񲹿���Ʊ������ɾ��
        /// </summary>
        /// <param name="dr">tf_ck.row</param>
        private void checktflz(DataRow dr)
        {
            string _blId = dr["EP_ID", DataRowVersion.Original].ToString();
            if (_blId.Equals("ER"))
            {
                InvIK _invik = new InvIK();

                string bilId = dr["EP_ID", DataRowVersion.Original].ToString();
                string ckNo = dr["EP_NO", DataRowVersion.Original].ToString();

                SunlikeDataSet _ds = _invik.GetInTfLz(bilId, ckNo);
                if (_ds.Tables["TF_LZ"].Rows.Count > 0)
                {
                    throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + "PARAM=" + _ds.Tables["TF_LZ"].Rows[0]["LZ_NO"].ToString());//�޷�ɾ�����ţ�ԭ��{0}
                }
            }
            else
            {
                InvLI _invli = new InvLI();
                string bilId = dr["EP_ID", DataRowVersion.Original].ToString();
                string ckNo = dr["EP_NO", DataRowVersion.Original].ToString();

                SunlikeDataSet _ds = _invli.GetInTfLz(bilId, ckNo);
                if (_ds.Tables["TF_LZ1"].Rows.Count > 0)
                {
                    throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + ",PARAM=" + _ds.Tables["TF_LZ1"].Rows[0]["LZ_NO"].ToString());//�޷�ɾ�����ţ�ԭ��{0}
                }
            }

        }

        #endregion

        #region IAuditing Members
        /// <summary>
        /// ���ͬ��
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
                SunlikeDataSet _ds = this.GetData(bil_id, bil_no, false);
                DataRow _drMf = _ds.Tables["MF_EXP"].Rows[0];
                DataTable _dtTf = _ds.Tables["TF_EXP"];

                //�����������Ϣ
                DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
                _exp.UpdateCheck(bil_id, bil_no, chk_man, cls_dd);
                _drMf["CHK_MAN"] = chk_man;
                _drMf["CLS_DATE"] = cls_dd;

                //����ƾ֤
                string _vohNo = this.UpdateVohNo(_drMf, StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);

                foreach (DataRow dr in _dtTf.Rows)
                {
                    //����
                    string _arpNo = this.UpdateArp(dr);
                    this.UpdataArpNo(bil_id, bil_no, _arpNo, Convert.ToInt32(dr["KEY_ITM"]));
                    //�տ�
                    this.UpdateMon(dr, false);
                }
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }

        /// <summary>
        /// ��˲�ͬ��
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Deny(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            // TODO:  Add MonEX.Deny implementation
            return null;
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData(bil_id, bil_no, false);
                DataRow _drMf = _ds.Tables["MF_EXP"].Rows[0];
                DataTable _dtTf = _ds.Tables["TF_EXP"];
                SetCanModify(_ds, false);
                string _modiyCode = SetCanModify(_ds, false); /*MODIFY LWH 090907*/
                if (_modiyCode == "0")
                //if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "T")
                {
                    //�����������Ϣ
                    DbMonEX _exp = new DbMonEX(Comp.Conn_DB);
                    _exp.UpdateCheck(bil_id, bil_no, "", DateTime.Now);

                    //����ƾ֤����
                    this.UpdateVohNo(_drMf, StatementType.Delete);
                    this.UpdateVohNo(bil_id, bil_no, "");

                    foreach (DataRow dr in _dtTf.Rows)
                    {
                        //�����տ
                        this.UpdateMon(dr, true);
                        dr.Delete();
                        //�������˵���
                        this.UpdateArp(dr);
                        this.UpdataArpNo(bil_id, bil_no, "", Convert.ToInt32(dr["KEY_ITM", DataRowVersion.Original]));
                    }
                }
                //else
                //{
                //    throw new Exception("" + _modiyCode + "");
                //}
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
            }
            return _error;
        }

        #endregion

        #region IAuditingInfo Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bil_Id"></param>
        /// <param name="Bil_No"></param>
        /// <param name="RejectSH"></param>
        /// <returns></returns>
        public string GetBillInfo(string Bil_Id, string Bil_No, ref bool RejectSH)
        {
            // TODO:  Add MonEX.GetBillInfo implementation
            return null;
        }

        #endregion
    }
}
