using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using System.Collections;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Xml;
using System.Reflection;

namespace Sunlike.Business
{
    /// <summary>
    /// �᰸������ҵ
    /// </summary>
    public class DRPEN : BizObject
    {
        private bool _isRunAuditing;
        private string _loginUsr;
        private string _type;
        private string _bilId;
        private DateTime _endDd = System.DateTime.Now;

        #region ���캯��
        /// <summary>
        /// �᰸������ҵ
        /// </summary>
        public DRPEN()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region ȡ����
        /// <summary>
        /// ȡ�᰸����
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="pgm"></param>
        /// <param name="endNo"></param>
        /// <param name="isSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string pgm, string endNo, bool isSchema)
        {
            DbDRPEN _en = new DbDRPEN(Comp.Conn_DB);
            SunlikeDataSet _ds = _en.GetData(endNo, isSchema);

            //���ӵ���Ȩ��
            if (!isSchema)
            {
                if (usr != null)
                {
                    DataTable _dtMf = _ds.Tables["MF_END"];
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
            //�����ֶ�
            if (_ds.Tables.Contains("TF_END"))
            {
                //�������
                _ds.Tables["TF_END"].Columns.Add("FLD_RES", typeof(System.String));
            }
            SetCanModify(_ds, true);
            _ds.AcceptChanges();
            return _ds;
        }

        #region ��鵥���Ƿ�����޸�
        /// <summary>
        /// ��鵥���Ƿ�����޸�
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">�Ƿ��ж��������</param>
        private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            DataTable _dtMf = ds.Tables["MF_END"];
            string errorMsg = "";
            bool _bCanModify = true;
            if (_dtMf.Rows.Count > 0)
            {
                //�жϹ�����
                if (_bCanModify && Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["END_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.ACCCLOSE";
                }
                //�ж��������
                if (_bCanModify && bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing("EN", _dtMf.Rows[0]["END_NO"].ToString()))
                    {
                        _bCanModify = false;
                        errorMsg += "COMMON.HINT.INTOAUT";
                    }
                }
                //�ж��Ƿ�����
                if (_bCanModify && !String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                }

            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return errorMsg;
        }
        #endregion

        #region �Ƿ���ڽ᰸��
        /// <summary>
        /// �Ƿ���ڽ᰸��
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public bool IsExists(string bil_id, string bil_no)
        {
            DbDRPEN _en = new DbDRPEN(Comp.Conn_DB);
            return _en.IsExists(bil_id, bil_no);
        }
        #endregion
        #endregion

        #region �᰸����

        #region ȡ�᰸����
        /// <summary>
        /// ȡ�᰸����
        /// </summary>
        /// <param name="usr">�û�</param>
        /// <param name="pgm">�������</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataCloseType(string usr, string pgm)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            //���嵥�ݽ᰸��ṹ
            DataTable _clsDt = CreateCloseBill();
            //��ȡ���ݽ᰸����
            GetCloseType(usr, pgm, _clsDt);
            _ds.Merge(_clsDt);
            return _ds;
        }
        #endregion

        #region �������ݽ᰸���ͱ�
        /// <summary>
        /// ���嵥�ݽ᰸��ṹ
        /// </summary>
        /// <returns></returns>
        private DataTable CreateCloseBill()
        {
            DataTable _clsDt = new DataTable("CLOSEBILL");
            if (Comp.CloseBill != null && Comp.CloseBill.ChildNodes.Count > 0 && Comp.CloseBill.ChildNodes[0].ChildNodes.Count > 0)
            {
                System.Xml.XmlNode _nd = Comp.CloseBill.ChildNodes[0];
                System.Xml.XmlElement _xe = ((System.Xml.XmlElement)_nd);
                _clsDt.Columns.Add("ITM", typeof(int));
                _clsDt.Columns["ITM"].AutoIncrement = true;
                _clsDt.Columns["ITM"].AutoIncrementSeed = 1;
                _clsDt.Columns["ITM"].AutoIncrementStep = 1;
                DataColumn[] _dc = new DataColumn[1] { _clsDt.Columns["ITM"] };
                _clsDt.PrimaryKey = _dc;
                CreateColumns(_xe, _clsDt);
            }
            return _clsDt;
        }
        private void CreateColumns(System.Xml.XmlElement xe, DataTable dt)
        {
            System.Xml.XmlNode _nd = xe.FirstChild;
            if (_nd != null)
            {
                System.Xml.XmlElement _xe = ((System.Xml.XmlElement)_nd);
                for (int i = 0; i < _xe.Attributes.Count; i++)
                {
                    string _columnName = _xe.Attributes[i].Name;
                    if (!dt.Columns.Contains(_columnName))
                        dt.Columns.Add(_columnName);
                }
                this.CreateColumns(_xe, dt);
            }
        }
        #endregion

        #region ��ȡ���ݽ᰸����
        /// <summary>
        ///	 ��ȡ���ݽ᰸����
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="clsTypeDt"></param>
        /// <returns></returns>
        private void GetCloseType(string usr, string pgm, DataTable clsTypeDt)
        {

            if (Comp.CloseBill == null)
            {
                return;
            }
            string _pgm = "";
            if (Comp.CloseBill.ChildNodes.Count > 0)
            {
                System.Xml.XmlNode _nd = Comp.CloseBill.ChildNodes[0];
                for (int i = 0; i < _nd.ChildNodes.Count; i++)
                {
                    System.Xml.XmlElement _xe = (System.Xml.XmlElement)_nd.ChildNodes[i];
                    _pgm = ((System.Xml.XmlElement)_nd.ChildNodes[i]).GetAttribute("PGM");
                    if (_pgm != pgm)
                        continue;

                    if (_pgm.Length > 0)
                    {
                        System.Collections.Hashtable _ht = new Hashtable();
                        SetRow(_xe, _ht, clsTypeDt);
                    }
                }
            }
        }
        private void SetRow(System.Xml.XmlElement xe, System.Collections.Hashtable ht, DataTable clsTypeDt)
        {
            for (int i = 0; i < xe.Attributes.Count; i++)
            {
                ht[xe.Attributes[i].Name] = xe.GetAttribute(xe.Attributes[i].Name);
            }
            if (xe.ChildNodes.Count > 0)
            {
                for (int i = 0; i < xe.ChildNodes.Count; i++)
                {
                    SetRow((System.Xml.XmlElement)xe.ChildNodes[i], ht, clsTypeDt);
                }
            }
            else
            {
                DataRow _newDr = clsTypeDt.NewRow();
                System.Collections.IDictionaryEnumerator _myEnumerator = ht.GetEnumerator();

                while (_myEnumerator.MoveNext())
                {
                    _newDr[_myEnumerator.Key.ToString()] = _myEnumerator.Value.ToString();
                }
                clsTypeDt.Rows.Add(_newDr);

            }
        }
        #endregion

        #endregion

        #region ���˵���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="loginID"></param>
        /// <param name="tableName"></param>
        /// <param name="sqlString"></param>
        /// <param name="sqlOrderby"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBill(string pgm, string loginID, string tableName, string sqlString, string sqlOrderby)
        {
            Users _users = new Users();
            _users.PowerSqlStr(loginID, pgm, tableName, ref sqlString);
            Query _query = new Query();
            string sqlStr = sqlString + " " + sqlOrderby;
            SunlikeDataSet _ds = null;
            _ds = _query.GetBilContent(sqlStr, new string[1] { tableName });
            return _ds;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����᰸����
        /// </summary>
        /// <param name="changedDs">DataSet</param>
        /// <param name="bubbleException">�Ƿ��׳��쳣��trueΪֱ���׳��쳣��false����ErrorTable��</param>
        /// <returns></returns>
        public DataTable UpdateData(SunlikeDataSet changedDs, bool bubbleException)
        {
            DataTable MF_ENDTable = changedDs.Tables["MF_END"];
            #region ȡ��¼���˼���Դ���ݱ�
            if (MF_ENDTable.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = MF_ENDTable.Rows[0]["USR"].ToString();
                _bilId = MF_ENDTable.Rows[0]["BIL_ID"].ToString();
                if (!String.IsNullOrEmpty(MF_ENDTable.Rows[0]["END_DD"].ToString()))
                {
                    _endDd = Convert.ToDateTime(MF_ENDTable.Rows[0]["END_DD"]);
                }
            }
            else
            {
                _loginUsr = MF_ENDTable.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
                _bilId = MF_ENDTable.Rows[0]["BIL_ID", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(MF_ENDTable.Rows[0]["END_DD", System.Data.DataRowVersion.Original].ToString()))
                {
                    _endDd = Convert.ToDateTime(MF_ENDTable.Rows[0]["END_DD", System.Data.DataRowVersion.Original]);
                }

            }
            Auditing _auditing = new Auditing();

            DataRow _dr = MF_ENDTable.Rows[0];
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
            //_isRunAuditing = _auditing.IsRunAuditing("EN", _loginUsr, _bilType,_mobID);


            #endregion

            //ȡ����Uri
            if (changedDs.ExtendedProperties.Contains("TYPE"))
            {
                this._type = changedDs.ExtendedProperties["TYPE"].ToString();
            }
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_END"] = "END_NO,END_DD,BIL_ID,DEP,SAL_NO,USR,CHK_MAN,BIL_TYPE,CLS_DATE,SYS_DATE,LOCK_MAN,MOB_ID,CPY_SW,PRT_SW,REM";
            _ht["TF_END"] = "END_NO,ITM,BIL_NO,CLS_TYPE,CLS_NAME,REM";
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
                    string _pgm = "";
                    //ȡ��PGM
                    if (changedDs.ExtendedProperties.Contains("PGM"))
                    {
                        _pgm = changedDs.ExtendedProperties["PGM"].ToString();
                    }

                    DataTable _dtMf = changedDs.Tables["MF_END"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, _UpdUsr, _bill_Dep, _bill_Usr);
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
                    throw new SunlikeException("RCID=DRPEN.UpdateData() Error:;" + _errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changedDs);
                }
            }
            return Sunlike.Business.BizObject.GetAllErrors(changedDs);
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
            string _dept = "";
            string _bil_type = "";
            string _endNo = "";
            Auditing _auditing = new Auditing();
            #region ȡ�õ��ż��ж��Ƿ��Ƿ�����
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _endNo = dr["END_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _endNo = dr["END_NO"].ToString();
                }
                //�ж��Ƿ�����������Ѿ����������޸ġ�
                Users _Users = new Users();
                string _whereStr = "END_NO = '" + _endNo + "'";
                if (_Users.IsLocked("MF_END", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion
            if (tableName == "MF_END")
            {
                if (statementType != StatementType.Delete)
                {
                    #region ���
                    //����(����)
                    Dept SunlikeDept = new Dept();
                    if (SunlikeDept.IsExist(_loginUsr, dr["DEP"].ToString()) == false)
                    {
                        dr.SetColumnError("DEP",/*���Ŵ���[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�����Ա
                    Salm _salm = new Salm();
                    if (dr["SAL_NO"].ToString().Length > 0)
                    {
                        if (_salm.IsExist(_loginUsr, dr["SAL_NO"].ToString()) == false)
                        {
                            dr.SetColumnError("SAL_NO",/*�����Ա[{0}]������û�ж��������Ȩ��,����*/"RCID=INV.HINT.SALNOISNULL,PARAM=" + dr["SAL_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion

                    if (statementType == StatementType.Insert)
                    {
                        #region --���ɵ���
                        SQNO SunlikeSqNo = new SQNO();
                        _dept = dr["DEP"].ToString();//����
                        _bil_type = dr["BIL_TYPE"].ToString();
                        string _endNoNew = SunlikeSqNo.Set("EN", _loginUsr, _dept, _endDd, _bil_type);
                        dr["END_NO"] = _endNoNew;
                        dr["END_DD"] = _endDd.ToString(Comp.SQLDateFormat);
                        dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        dr["PRT_SW"] = "N";
                        #endregion                       
                    }
                }
                //#region ��˹���
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_TYPE = "FX";
                //    _aps.BIL_ID = "EN";
                //    _aps.BIL_NO = dr["END_NO"].ToString();
                //    _aps.BIL_DD = _endDd;
                //    _aps.USR = _loginUsr;
                //    _aps.CUS_NO = "";
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //    _aps = new AudParamStruct("EN", Convert.ToString(dr["END_NO", DataRowVersion.Original]));
                //_auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

            }
            if (tableName == "TF_END")
            {
                if (statementType != StatementType.Delete)
                {
                    //���
                    #region ���
                    //��Դ����(����)
                    if (dr["BIL_NO"].ToString().Length == 0)
                    {
                        dr.SetColumnError("BIL_NO",/*��Դ���Ų�����Ϊ��*/"RCID=INV.HINT.BIL_NOISNULL");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�����ʽ(����)
                    if (dr["CLS_TYPE"].ToString().Length == 0)
                    {
                        dr.SetColumnError("CLS_TYPE",/*�����ʽ������Ϊ��*/"RCID=INV.HINT.CLS_NAMEISNULL");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�᰸����(����)
                    if (dr["CLS_NAME"].ToString().Length == 0)
                    {
                        dr.SetColumnError("CLS_NAME",/*�᰸���Ͳ�����Ϊ��*/"RCID=INV.HINT.CLS_TYPEISNULL");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    #endregion
                }
            }

            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }
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
            Auditing _auditing = new Auditing();
            if (tableName == "MF_END")
            {
                string _endNo = "";
                if (statementType != StatementType.Delete)
                {               

                }
                else
                {
                    #region ɾ������
                    _endNo = dr["END_NO", DataRowVersion.Original].ToString();//����
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(_endNo, _loginUsr);//ɾ��ʱ��BILD�в���һ������
                    #endregion

                }

            }
            if (tableName == "TF_END")
            {
                ICloseBill _iCloseBill = CreateInstance(this._type);
                string _error = "";
                if (statementType != StatementType.Delete)
                {
                    #region �᰸����
                    if (this._type.Length > 0)
                    {
                        bool _modifyBill = true;
                        if (statementType == StatementType.Update)
                        {
                            if (dr["CLS_TYPE", System.Data.DataRowVersion.Original].ToString() == dr["CLS_TYPE"].ToString())
                            {
                                _modifyBill = false;
                            }
                        }
                        if (_modifyBill)
                        {
                            if (!String.IsNullOrEmpty(dr["CLS_TYPE"].ToString()) && dr["CLS_TYPE"].ToString() == "T")
                            {
                                _error = _iCloseBill.DoCloseBill(_bilId, dr["BIL_NO"].ToString(), dr["CLS_NAME"].ToString());
                            }
                            else
                            {
                                _error = _iCloseBill.UndoCloseBill(_bilId, dr["BIL_NO"].ToString(), dr["CLS_NAME"].ToString());
                            }
                            if (_error.Length > 0)
                            {
                                dr.SetColumnError("BIL_NO", _error);
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }
                    #endregion
                }
                else
                {

                    if (!String.IsNullOrEmpty(dr["CLS_TYPE", System.Data.DataRowVersion.Original].ToString()) && dr["CLS_TYPE", System.Data.DataRowVersion.Original].ToString() == "T")
                    {
                        _error = _iCloseBill.UndoCloseBill(_bilId, dr["BIL_NO", System.Data.DataRowVersion.Original].ToString(), dr["CLS_NAME", System.Data.DataRowVersion.Original].ToString());
                    }
                    else
                    {
                        _error = _iCloseBill.DoCloseBill(_bilId, dr["BIL_NO", System.Data.DataRowVersion.Original].ToString(), dr["CLS_NAME", System.Data.DataRowVersion.Original].ToString());
                    }
                    if (_error.Length > 0)
                    {
                        dr.SetColumnError("BIL_NO", _error);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        /// <summary>
        /// ȡ�ӿ�
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private Sunlike.Business.ICloseBill CreateInstance(string uri)
        {
            string[] _objectUri = uri.Split(new char[] { ';' });
            bool _para = true;
            Assembly _ass = null;
            ICloseBill _icb = null;
            try
            {
                _ass = Assembly.Load(_objectUri[1]);
            }
            catch (System.IO.FileNotFoundException _ex)
            {
                _para = false;
                throw _ex;
            }
            if (_para)
            {
                Type _business = _ass.GetType(_objectUri[0]);
                object _obj = Activator.CreateInstance(_business);

                if (TypeInfo.Support(_obj, "ICloseBill"))
                {
                    _icb = (_obj as ICloseBill);
                }
            }
            return _icb;
        }
        #endregion

    }
}
