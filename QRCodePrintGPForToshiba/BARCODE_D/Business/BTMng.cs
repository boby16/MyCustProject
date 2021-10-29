using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Collections;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
    /// <summary>
    /// Summary description for DRPBT.
    /// </summary>
    public class BTMng : Sunlike.Business.BizObject, IAuditing
    {
        private Sunlike.Business.Data.DbBTMng _dbBt;
        /// <summary>
        /// ��������ƾ֤
        /// </summary>
        private bool _reBuildVohNo = false;
        #region	���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public BTMng()
        { }
        #endregion

        #region	ȡ�÷���/��������¼

        /// <summary>
        /// ȡ�÷���/�����ͷ��¼
        /// </summary>
        /// <param name="pBB_ID">�ʻ�ʶ����</param>
        /// <param name="pBB_NO">�ʻ��䶯����</param>
        /// <returns></returns>
        public DataTable GetMF_BAC(string pBB_ID, string pBB_NO)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            return _dbBt.GetMF_BAC(pBB_ID, pBB_NO);
        }

        /// <summary>
        /// ȡ�÷���/��������¼
        /// </summary>
        /// <param name="pBB_ID">�ʻ�ʶ����</param>
        /// <param name="pBB_NO">�ʻ��䶯����</param>
        /// <returns></returns>
        public DataTable GetTF_BAC(string pBB_ID, string pBB_NO)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            return _dbBt.GetTF_BAC(pBB_ID, pBB_NO);
        }

        #endregion

        #region ȡ�ñұ���Ϣ
        /// <summary>
        /// ȡ�ñұ�����
        /// </summary>
        /// <param name="pCurId">�ұ��</param>
        /// <returns></returns>
        public string GetCurName(string pCurId)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            return _dbBt.GetCurName(pCurId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pCurId"></param>
        /// <param name="ExcType"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string GetExcRto(string pCurId, DateTime dateTime, int ExcType)
        {
            Currency _dbFi = new Currency();
            string _rot = _dbFi.GetExcRto(pCurId, dateTime, ExcType);
            if (String.IsNullOrEmpty(_rot))
            {
                return "1";
            }
            return _rot;
        }
        #endregion

        #region	ȡ�������ʻ���Ϣ

        /// <summary>
        /// ȡ�������ʻ�����
        /// </summary>
        /// <param name="pBaccNo">�ʻ���</param>
        /// <returns></returns>
        public string GetBaccName(string pBaccNo)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            return _dbBt.GetBaccName(pBaccNo);
        }
        /// <summary>
        /// ȡ��������Ϣ
        /// </summary>
        /// <param name="pBaccNo"></param>
        /// <returns></returns>
        public DataTable GetBaccInfo(string pBaccNo)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            return _dbBt.GetBACC(pBaccNo);
        }
        #endregion

        #region	ȡ�õ�����Ϣ

        /// <summary>
        /// ȡ�õ�����Ϣ
        /// </summary>
        /// <param name="pUserId">�û�id</param>
        /// <returns></returns>
        public string GetBtNo(string pUserId)
        {
            SQNO _sqlno = new SQNO();
            Users _users = new Users();
            string _btNo = _sqlno.Get("BT", pUserId, _users.GetUserDepNo(pUserId), DateTime.Now, "");
            return _btNo;
        }
        /// <summary>
        /// ȡ�õ���
        /// </summary>
        /// <returns></returns>
        public string GetbtNo(string btId, string userId, DateTime dateTime)
        {
            string _btNo = "BT00000000";
            try
            {
                SQNO _sqlno = new SQNO();
                Users _users = new Users();
                _btNo = _sqlno.Get(btId, userId, _users.GetUserDepNo(userId), dateTime, "FX");
            }
            catch { }
            return _btNo;
        }
        #endregion

        #region	ȡ�û�ƿ�Ŀ��Ϣ
        /// <summary>
        /// ȡ�û�ƿ�Ŀ����
        /// </summary>
        /// <param name="pAccNo">��Ŀ���</param>
        /// <returns>��ƿ�Ŀ����</returns>
        public string GetAccName(string pAccNo)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            return _dbBt.GetAccName(pAccNo);
        }

        /// <summary>
        /// ȡ�û�ƿ�Ŀ���
        /// </summary>
        /// <param name="pAccNo">��Ŀ���</param>
        /// <returns></returns>
        public string GetAccArp(string pAccNo)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            return _dbBt.GetAccArp(pAccNo);
        }
        #region ����ƿ�Ŀ�Ƿ����
        /// <summary>
        /// ���û�ƿ�Ŀ�Ƿ����
        /// </summary>
        /// <param name="accNo"></param>
        /// <returns></returns>
        public bool IsExistsAccNo(string accNo)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            DataTable _accTable = _dbBt.GetAccnInfo(accNo);
            if (_accTable.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        #endregion

        #region Session�е�DataSet
        /// <summary>
        /// ȡ�ñ�ṹ
        /// </summary>
        /// <param name="usr">��ǰ�û�</param>
        /// <param name="pBB_NO"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string pBB_NO)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            //���ӵ���Ȩ��
            SunlikeDataSet _ds = _dbBt.GetBacDs(pBB_NO);
            Users _users = new Users();
            _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            SetCanModify(_ds, true);
            //
            if (usr != null)
            {
                DataTable _dtMfBac = _ds.Tables["MF_BAC"];
                if (_dtMfBac.Rows.Count > 0)
                {
                    string _bill_Dep = _dtMfBac.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _dtMfBac.Rows[0]["USR"].ToString();
                    System.Collections.Hashtable _billRight = Users.GetBillRight("DRPBT", usr, _bill_Dep, _bill_Usr);
                    _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                }
            }
            return _ds;
        }

        private void SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            bool _bCanModify = true;
            DataTable _dtMf = ds.Tables["MF_BAC"];
            if (_dtMf.Rows.Count > 0)
            {
                if (_dtMf.Rows[0]["BB_DD"] != DBNull.Value)
                {
                    if (_bCanModify && Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["BB_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                    {
                        _bCanModify = false;
                    }
                    if (_bCanModify && bCheckAuditing)
                    {
                        Auditing _aud = new Auditing();
                        if (_aud.GetIfEnterAuditing(_dtMf.Rows[0]["BB_ID"].ToString(), _dtMf.Rows[0]["BB_NO"].ToString()))
                        {
                            _bCanModify = false;
                        }
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
        }
        #endregion

        #region �����Ϣ
        /// <summary>
        /// �����Ϣ
        /// </summary>
        /// <param name="pDs">DataSet</param>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pADD_ID"></param>
        /// <param name="pDEP"></param>
        /// <param name="pCUR_ID"></param>
        /// <param name="pAMT"></param>
        /// <param name="pAMTN"></param>
        /// <param name="pRCV_MAN"></param>
        /// <param name="pACC_NO"></param>
        /// <param name="pREM"></param>
        public void InsertTF_BAC(ref SunlikeDataSet pDs, string pBB_ID, string pBB_NO, string pADD_ID, string pDEP, string pCUR_ID, string pAMT, string pAMTN, string pRCV_MAN, string pACC_NO, string pREM)
        {
            try
            {
                DataTable _dt = pDs.Tables["TF_BAC"];
                DataRow _dr = _dt.NewRow();
                _dr["BB_ID"] = pBB_ID;
                _dr["BB_NO"] = pBB_NO;
                _dr["BB_DD"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                _dr["ITM"] = GetITM(_dt).ToString();
                _dr["ADD_ID"] = pADD_ID;
                _dr["DEP"] = pDEP;
                if (!String.IsNullOrEmpty(pAMT))
                    _dr["AMT"] = Convert.ToDecimal(pAMT);
                if (!String.IsNullOrEmpty(pAMTN))
                    _dr["AMTN"] = Convert.ToDecimal(pAMTN);
                _dr["RCV_MAN"] = pRCV_MAN;
                _dr["ACC_NO"] = pACC_NO;
                _dr["REM"] = pREM;
                if (!String.IsNullOrEmpty(pCUR_ID))
                {
                    Currency _dbFi = new Currency();
                    string _tempExc = _dbFi.GetExcRto(pCUR_ID, 2);
                    if (String.IsNullOrEmpty(_tempExc))
                        _tempExc = "1";
                    _dr["EXC_RTO"] = Convert.ToDecimal(_tempExc);
                }
                else
                {
                    _dr["EXC_RTO"] = "1.00000000";
                }
                _dt.Rows.Add(_dr);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDt"></param>
        /// <returns></returns>
        public int GetITM(DataTable pDt)
        {
            int _itm = 0;
            for (int i = 0; i < pDt.Rows.Count; i++)
                if (pDt.Rows[i].RowState != DataRowState.Deleted)
                    _itm += 1;
            return _itm + 1;
        }
        /// <summary>
        /// ɾ����
        /// </summary>
        /// <param name="pDs"></param>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pITM"></param>
        public void DeleteTF_BAC(ref SunlikeDataSet pDs, string pBB_ID, string pBB_NO, string pITM)
        {
            DataTable _dt = pDs.Tables["TF_BAC"];
            string _field = "BB_ID='" + pBB_ID + "' AND BB_NO='" + pBB_NO + "' AND ITM=" + pITM;
            DataRow[] _drArray = _dt.Select(_field);
            if (_drArray.Length > 0)
                _drArray[0].Delete();
            DataRow[] _curdr = _dt.Select("ITM>" + pITM);
            for (int i = 0; i < _curdr.Length; i++)
            {
                _curdr[i]["ITM"] = Convert.ToInt32(_curdr[i]["ITM"].ToString()) - 1;
            }
        }
        /// <summary>
        /// ���±���
        /// </summary>
        /// <param name="pDs"></param>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pITM"></param>
        /// <param name="pADD_ID"></param>
        /// <param name="pDEP"></param>
        /// <param name="pAMT"></param>
        /// <param name="pAMTN"></param>
        /// <param name="pRCV_MAN"></param>
        /// <param name="pACC_NO"></param>
        /// <param name="pCUR_ID"></param>
        /// <param name="pREM"></param>
        public void UpdateTF_BAC(ref SunlikeDataSet pDs, string pBB_ID, string pBB_NO, string pITM, string pADD_ID, string pDEP, string pAMT, string pAMTN, string pRCV_MAN, string pACC_NO, string pCUR_ID, string pREM)
        {
            DataTable _dt = pDs.Tables["TF_BAC"];
            string _field = "BB_ID='" + pBB_ID + "' AND BB_NO='" + pBB_NO + "' AND ITM=" + pITM;
            DataRow[] _drArray = _dt.Select(_field);
            if (_drArray.Length > 0)
            {
                DataRow _dr = _drArray[0];
                _dr["ADD_ID"] = pADD_ID;
                _dr["DEP"] = pDEP;
                if (!String.IsNullOrEmpty(pAMT))
                    _dr["AMT"] = Convert.ToDecimal(pAMT);
                if (!String.IsNullOrEmpty(pAMTN))
                    _dr["AMTN"] = Convert.ToDecimal(pAMTN);
                _dr["RCV_MAN"] = pRCV_MAN;
                _dr["ACC_NO"] = pACC_NO;
                _dr["REM"] = pREM;
                if (!String.IsNullOrEmpty(pCUR_ID))
                {
                    Currency _dbFi = new Currency();
                    string _tempExc = _dbFi.GetExcRto(pCUR_ID, 2);
                    if (String.IsNullOrEmpty(_tempExc))
                        _tempExc = "0";
                    _dr["EXC_RTO"] = Convert.ToDecimal(_tempExc);
                }
                else
                {
                    _dr["EXC_RTO"] = "1.00000000";
                }
            }
        }

        /// <summary>
        /// ���±�ͷ
        /// </summary>
        /// <param name="pDs"></param>
        /// <param name="pBB_DD"></param>
        /// <param name="pBACC_NO"></param>
        /// <param name="pACC_NO"></param>
        /// <param name="pDEP"></param>
        /// <param name="pPAY_MAN"></param>
        /// <param name="pCUR_ID"></param>
        /// <param name="pREM"></param>
        public void UpdateMF_BAC(ref SunlikeDataSet pDs, string pBB_DD, string pBACC_NO, string pACC_NO, string pDEP, string pPAY_MAN, string pCUR_ID, string pREM)
        {
            DataTable _dt = pDs.Tables["MF_BAC"];
            if (_dt.Rows.Count > 0)
            {
                DataRow _dr = _dt.Rows[0];
                _dr["BB_DD"] = pBB_DD;
                _dr["BACC_NO"] = pBACC_NO;
                _dr["ACC_NO"] = pACC_NO;
                _dr["DEP"] = pDEP;
                _dr["PAY_MAN"] = pPAY_MAN;
                _dr["CUR_ID"] = pCUR_ID;
                _dr["REM"] = pREM;
                _dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                if (!String.IsNullOrEmpty(pCUR_ID))
                {
                    Currency _dbFi = new Currency();
                    string _tempExc = _dbFi.GetExcRto(pCUR_ID, 2);
                    if (String.IsNullOrEmpty(_tempExc))
                        _tempExc = "0";
                    _dr["EXC_RTO"] = Convert.ToDecimal(_tempExc);
                }
                else
                {
                    _dr["EXC_RTO"] = "1.00000000";
                }
            }
        }
        /// <summary>
        /// ȡ��itm
        /// </summary>
        /// <param name="pDt"></param>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pITM"></param>
        /// <returns></returns>
        public DataRow FindData(DataTable pDt, string pBB_ID, string pBB_NO, string pITM)
        {
            string _field = "BB_ID='" + pBB_ID + "' AND BB_NO='" + pBB_NO + "' AND ITM=" + pITM;
            DataRow[] _drArray = pDt.Select(_field);
            if (_drArray.Length > 0)
                return _drArray[0];
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            if (tableName == "MF_BAC")
            {
                string _bbId = "";
                string _bbNo = "";
                DateTime _bbDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                string _usr = "";
                string _dep = "";
                bool runAudit;
                Auditing _aud = new Auditing();
                if (dr.RowState == DataRowState.Deleted)
                {
                    _bbId = dr["BB_ID", DataRowVersion.Original].ToString();
                    _bbNo = dr["BB_NO", DataRowVersion.Original].ToString();
                    _bbDd = Convert.ToDateTime(dr["BB_DD", DataRowVersion.Original]);
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                    _dep = dr["DEP", DataRowVersion.Original].ToString();
                    _aud.DelBillWaitAuditing("DRP", _bbId, _bbNo);
                }
                else
                {
                    _bbId = dr["BB_ID"].ToString();
                    _bbNo = dr["BB_NO"].ToString();
                    _bbDd = Convert.ToDateTime(dr["BB_DD"]);
                    _usr = dr["USR"].ToString();
                    _dep = dr["DEP"].ToString();
                }
                string _bilType = "";
                string _mobID = "";//֧��ֱ������mobID=@@ �򵥾ݲ����������
                if (dr.RowState == DataRowState.Deleted)
                {
                    if (dr.Table.Columns.Contains("BIL_TYPE"))
                        _bilType = dr["BIL_TYPE", DataRowVersion.Original].ToString();
                    if (dr.Table.Columns.Contains("MOB_ID"))
                        _mobID = dr["MOB_ID", DataRowVersion.Original].ToString();
                }
                else
                {
                    if (dr.Table.Columns.Contains("BIL_TYPE"))
                        _bilType = dr["BIL_TYPE"].ToString();
                    if (dr.Table.Columns.Contains("MOB_ID"))
                        _mobID = dr["MOB_ID"].ToString();
                }
                //runAudit = _aud.IsRunAuditing(_bbId, _usr, _bilType, _mobID);

                //#region ��˹���
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_TYPE = "FX";
                //    _aps.BIL_ID = _bbId;
                //    _aps.BIL_NO = _bbNo;
                //    _aps.BIL_DD = _bbDd;
                //    _aps.USR = _usr;
                //    _aps.CUS_NO = "";
                //    _aps.DEP = _dep;
                //    _aps.SAL_NO = dr["PAY_MAN"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //    _aps = new AudParamStruct(_bbId, _bbNo);
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(runAudit, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);                  
                //}
                //#endregion

            }
            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dtMf = ds.Tables["MF_BAC"];
            //if (_dtMf.Rows.Count > 0)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["BB_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["BB_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion
            //��д�ʻ���
            //Bacc _bacc = new Bacc();
            //_bacc.UpdateSBAC(_dtMf.Rows[0]);
        }


        /// <summary>
        /// �����¼
        /// </summary>
        /// <param name="pDs"></param>
        /// <param name="pUserId"></param>
        /// <param name="pCUR_ID"></param>
        /// <param name="pMod"></param>
        public void UpdateData(SunlikeDataSet pDs, string pUserId, string pCUR_ID, bool pMod)
        {
            if (pDs.Tables["MF_BAC"].Rows.Count > 0)
            {
                Users _users = new Users();
                if (!pMod)
                {
                    SQNO _sqlno = new SQNO();
                    string _btNo = _sqlno.Set("BT", pUserId, _users.GetUserDepNo(pUserId), DateTime.Now, "FX");
                    pDs.Tables["MF_BAC"].Rows[0]["BB_NO"] = _btNo;
                    //�Ʊ���
                    pDs.Tables["MF_BAC"].Rows[0]["USR"] = pUserId;
                    for (int i = 0; i < pDs.Tables["TF_BAC"].Rows.Count; i++)
                    {
                        pDs.Tables["TF_BAC"].Rows[i]["BB_NO"] = _btNo;
                    }
                }
                string _excRto = "1";
                if (!String.IsNullOrEmpty(pCUR_ID))
                {
                    Currency _dbFi = new Currency();
                    _excRto = _dbFi.GetExcRto(pCUR_ID, 2);
                }

                pDs.Tables["MF_BAC"].Rows[0]["AMT"] = 0;
                pDs.Tables["MF_BAC"].Rows[0]["AMTN"] = 0;
                for (int i = 0; i < pDs.Tables["TF_BAC"].Rows.Count; i++)
                {
                    if (pDs.Tables["TF_BAC"].Rows[i].RowState == DataRowState.Deleted)
                        continue;
                    pDs.Tables["TF_BAC"].Rows[i]["EXC_RTO"] = _excRto;
                    if (pDs.Tables["TF_BAC"].Rows[i]["ADD_ID"].ToString().Trim() == "+")
                    {
                        if (!String.IsNullOrEmpty(pDs.Tables["TF_BAC"].Rows[i]["AMT"].ToString()))
                        {
                            if (!String.IsNullOrEmpty(pCUR_ID))
                                pDs.Tables["MF_BAC"].Rows[0]["AMT"] = Convert.ToDecimal(pDs.Tables["MF_BAC"].Rows[0]["AMT"]) + Convert.ToDecimal(pDs.Tables["TF_BAC"].Rows[i]["AMT"]);
                        }
                        if (!String.IsNullOrEmpty(pDs.Tables["TF_BAC"].Rows[i]["AMTN"].ToString()))
                        {
                            pDs.Tables["MF_BAC"].Rows[0]["AMTN"] = Convert.ToDecimal(pDs.Tables["MF_BAC"].Rows[0]["AMTN"]) + Convert.ToDecimal(pDs.Tables["TF_BAC"].Rows[i]["AMTN"]);
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(pDs.Tables["TF_BAC"].Rows[i]["AMT"].ToString()))
                        {
                            if (!String.IsNullOrEmpty(pCUR_ID))
                                pDs.Tables["MF_BAC"].Rows[0]["AMT"] = Convert.ToDecimal(pDs.Tables["MF_BAC"].Rows[0]["AMT"]) - Convert.ToDecimal(pDs.Tables["TF_BAC"].Rows[i]["AMT"]);
                        }
                        if (!String.IsNullOrEmpty(pDs.Tables["TF_BAC"].Rows[i]["AMTN"].ToString()))
                        {
                            pDs.Tables["MF_BAC"].Rows[0]["AMTN"] = Convert.ToDecimal(pDs.Tables["MF_BAC"].Rows[0]["AMTN"]) - Convert.ToDecimal(pDs.Tables["TF_BAC"].Rows[i]["AMTN"]);
                        }
                    }
                }

                this.UpdateDataSet(pDs);
            }
        }
        /// <summary>
        /// ȡ���������
        /// </summary>
        /// <param name="pDs"></param>
        /// <param name="pCUR_ID"></param>
        /// <param name="pUp"></param>
        /// <param name="pDown"></param>
        public void GetAMTSum(SunlikeDataSet pDs, string pCUR_ID, out string pUp, out string pDown)
        {
            decimal _up = 0;
            decimal _down = 0;
            string _amtn = "AMTN";
            if (!String.IsNullOrEmpty(pCUR_ID))
                _amtn = "AMT";
            for (int i = 0; i < pDs.Tables["TF_BAC"].Rows.Count; i++)
            {
                if (pDs.Tables["TF_BAC"].Rows[i].RowState == DataRowState.Deleted)
                    continue;
                if (pDs.Tables["TF_BAC"].Rows[i][_amtn] != null && !string.IsNullOrEmpty(pDs.Tables["TF_BAC"].Rows[i][_amtn].ToString()))
                {
                    if (pDs.Tables["TF_BAC"].Rows[i]["ADD_ID"].ToString() == "+")
                        _up += Convert.ToDecimal(pDs.Tables["TF_BAC"].Rows[i][_amtn]);
                    else
                        _down += Convert.ToDecimal(pDs.Tables["TF_BAC"].Rows[i][_amtn]);
                }
            }
            pUp = _up.ToString();
            pDown = _down.ToString();
        }

        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="pDs"></param>
        public void DeleteDs(SunlikeDataSet pDs)
        {
            foreach (DataRow _dr in pDs.Tables["TF_BAC"].Rows)
            {
                _dr.Delete();
            }
            foreach (DataRow _dr in pDs.Tables["MF_BAC"].Rows)
            {
                _dr.Delete();
            }
            this.UpdateDataSet(pDs);
        }
        #endregion

        #region �᰸

        /// <summary>
        /// �᰸
        /// </summary>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pCHK_MAN"></param>
        /// <param name="chk_DD"></param>
        /// <returns></returns>
        public string Approve(string pBB_ID, string pBB_NO, string pCHK_MAN, System.DateTime chk_DD)
        {
            string _error = "";
            try
            {
                //��д�ʻ���
                _dbBt = new DbBTMng(Comp.Conn_DB);
                DataTable _dtMF = _dbBt.GetMF_BAC(pBB_ID, pBB_NO);
                _dtMF.Rows[0]["CHK_MAN"] = pCHK_MAN;
                _dtMF.Rows[0]["CLS_DATE"] = chk_DD;
                Bacc _bacc = new Bacc();
                _bacc.UpdateSBAC(_dtMF.Rows[0]);

                _dbBt.Approve(pBB_NO, pCHK_MAN, chk_DD);
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
            }
            return _error;
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <returns></returns>
        public string RollBack(string pBB_ID, string pBB_NO)
        {
            string _error = "";
            try
            {
                _dbBt = new DbBTMng(Comp.Conn_DB);
                SunlikeDataSet _ds = _dbBt.GetBacDs(pBB_NO);
                SetCanModify(_ds, false);
                if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                {
                    return "RCID=EXCEPTION.INTOAUT";
                }
                _dbBt.RollBack(pBB_NO, pBB_NO);
                //��д�ʻ���
                DataTable _dtMF = _ds.Tables["MF_BAC"];
                if (_dtMF.Rows.Count > 0)
                {
                    _dtMF.Rows[0].Delete();
                }
                Bacc _bacc = new Bacc();
                _bacc.UpdateSBAC(_dtMF.Rows[0]);
            }
            catch (Exception ex)
            {
                _error = ex.Message.ToString();
            }
            return _error;
        }
        /// <summary>
        /// ��˲�ͬ��
        /// </summary>
        /// <param name="pBB_ID"></param>
        /// <param name="pBB_NO"></param>
        /// <param name="pCHK_MAN"></param>
        /// <param name="chk_DD"></param>
        /// <returns></returns>
        public string Deny(string pBB_ID, string pBB_NO, string pCHK_MAN, System.DateTime chk_DD)
        {
            return "";
        }
        #endregion

        #region ȡ����Ѷ
        /// <summary>
        /// ȡ����Ѷ
        /// </summary>
        /// <param name="pBB_NO"></param>
        /// <returns></returns>
        public DataTable GetAuView(string pBB_NO)
        {
            _dbBt = new DbBTMng(Comp.Conn_DB);
            return _dbBt.GetAuView(pBB_NO);
        }

        #endregion

        #region ȡ�÷�����Ŀ��Ϣ

        #endregion

        #region ��д�ʻ���
        #endregion

    }
}