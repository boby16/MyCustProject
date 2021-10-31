using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Business.Data;
using Sunlike.Common.CommonVar;
using System.Collections;
using System.Data;
using Sunlike.Common.Utility;
namespace Sunlike.Business
{
    /// <summary>
    ///  ���
    /// </summary>
    public class MRPMO : BizObject, IAuditing, Sunlike.Business.ICloseBill
    {
        #region ����
        private bool _isSvs = false;
        private string _loginUsr;
        private bool _isRunAuditing;
        private bool _checkQty = true;
        private bool _allowUpdate = true;
        private bool _hasReturn = false;
        #endregion
        /// <summary>
        /// ���
        /// </summary>
        public MRPMO()
        {
        }

        #region ȡ����
        /// <summary>
        /// ȡ�������
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            DbMRPMO _dbMrpMo = new DbMRPMO(Comp.Conn_DB);
            return _dbMrpMo.GetData(sqlWhere);
        }
        /// <summary>
        /// ȡ�������
        /// </summary>        
        /// <param name="moNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string moNo, bool onlyFillSchema)
        {
            return this.GetUpdateData(null, null, moNo, onlyFillSchema);
        }

        /// <summary>
        /// ȡ�������
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="moNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string pgm, string usr, string moNo, bool onlyFillSchema)
        {
            DbMRPMO _dbMrpMo = new DbMRPMO(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbMrpMo.GetData(moNo, onlyFillSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;

                DataTable _dtHead = _ds.Tables["MF_MO"];
                if (_dtHead.Rows.Count > 0)
                {
                    string _bill_Dep = _dtHead.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _dtHead.Rows[0]["USR"].ToString();
                    string _pgm = "";
                    if (string.IsNullOrEmpty(pgm))
                    {
                        if (_dtHead.Rows[0]["ISSVS"].ToString() == "T")
                            _pgm = "MTNMO";
                        else
                            _pgm = "MRPMO";
                    }
                    else
                    {
                        _pgm = pgm;
                    }
                    System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _bill_Dep, _bill_Usr);
                    _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                }

            }
            _ds.Tables["TF_MO"].Columns["EST_ITM"].AutoIncrement = true;
            _ds.Tables["TF_MO"].Columns["EST_ITM"].AutoIncrementSeed = _ds.Tables["TF_MO"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_MO"].Select("", "EST_ITM desc")[0]["EST_ITM"]) + 1 : 1;
            _ds.Tables["TF_MO"].Columns["EST_ITM"].AutoIncrementStep = 1;
            _ds.Tables["TF_MO"].Columns["PRE_ITM"].AutoIncrement = true;
            _ds.Tables["TF_MO"].Columns["PRE_ITM"].AutoIncrementSeed = _ds.Tables["TF_MO"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_MO"].Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _ds.Tables["TF_MO"].Columns["PRE_ITM"].AutoIncrementStep = 1;
            this.SetCanModify(_ds, true);
            return _ds;
        }
        /// <summary>
        /// ȡ��δ�����������Ϣ
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataUnUse(string moNo)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            return _mo.GetDataUnUse(moNo);
        }
        /// <summary>
        /// ȡ���������������Ϣ
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataUse(string moNo)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            return _mo.GetDataUse(moNo);
        }
        /// <summary>
        /// /ȡ�ñ����¼
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <param name="unit">��λ</param>
        /// <param name="estItm">׷�����</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string moNo, string unit, int estItm)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            SunlikeDataSet _ds = _mo.GetDataBody(moNo, "EST_ITM", estItm);
            if (_ds.Tables.Contains("TF_MO") && _ds.Tables["TF_MO"].Rows.Count > 0)
            {
                Prdt _prdt = new Prdt();
                foreach (DataRow dr in _ds.Tables["TF_MO"].Rows)
                {
                    decimal _qty = 0;
                    if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                        _qty = Convert.ToDecimal(dr["QTY"].ToString());
                    dr["QTY"] = _prdt.GetUnitQty(dr["PRD_NO"].ToString(), dr["UNIT"].ToString(), _qty, unit);
                }
            }
            _ds.AcceptChanges();
            return _ds;
        }
        /// <summary>
        /// ȡ�ñ����¼
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <param name="prdNo">Ʒ��</param>
        /// <param name="prdMark">����</param>
        /// <param name="wh">��λ</param>
        /// <param name="useInNo">��װλ��</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string moNo, string prdNo, string prdMark, string wh, string useInNo)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            SunlikeDataSet _ds = _mo.GetDataBody(moNo, prdNo, prdMark, wh, useInNo);
            return _ds;
        }
        #endregion

        #region  ��鵥���Ƿ�����޸�
        /// <summary>
        /// ��鵥���Ƿ�����޸�
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">�Ƿ��ж��������</param>
        private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            DataTable _dtHead = ds.Tables["MF_MO"];
            DataTable _dtBody = ds.Tables["TF_MO"];
            string _usr = "";
            string errorMsg = "";
            bool _bCanModify = true;
            if (_dtHead.Rows.Count > 0)
            {
                _usr = _dtHead.Rows[0]["USR"].ToString();
                //�жϹ�����
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtHead.Rows[0]["MO_DD"]), _dtHead.Rows[0]["DEP"].ToString(), "CLS_MNU"))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.CLOSE_CLS";
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                }
                //�ж��������
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing("MO", _dtHead.Rows[0]["MO_NO"].ToString()))
                    {
                        _bCanModify = false;
                        errorMsg += "COMMON.HINT.CLOSE_AUDIT";
                        ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
                //�ж��Ƿ�᰸

                if (_dtHead.Rows[0]["CLOSE_ID"].ToString() == "T")
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.CLOSE_MODIFY";
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");
                }

                //�ж��Ƿ�����
                if (!String.IsNullOrEmpty(_dtHead.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
                //�ж��Ƿ�����Դ����
                if (bCheckAuditing)
                {
                    if (!string.IsNullOrEmpty(_dtHead.Rows[0]["BIL_ID"].ToString()) && !string.IsNullOrEmpty(_dtHead.Rows[0]["BIL_NO"].ToString()))
                    {
                        ds.ExtendedProperties["DEL"] = "N";
                        errorMsg += "MTN.HINT.HASBILNO";
                        ////Common.SetCanModifyRem(ds, "RCID=MTN.HINT.HASBILNO");
                    }
                }
                string _pgm = "";
                if (_dtHead.Rows[0]["ISSVS"].ToString() == "T")
                    _pgm = "MTNMO";
                else
                    _pgm = "MRPMO";
                Sunlike.Business.UserProperty _usrProp = new UserProperty();
                string _strAllowUpdate = _usrProp.GetData(_usr, _pgm, "ALLOW_UPDATE");

                if (string.Compare(_strAllowUpdate, "T") != 0)
                {
                    MRPML _ml = new MRPML();
                    if (_ml.IsExistsForMo(_dtHead.Rows[0]["MO_NO"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(_dtHead.Rows[0]["OPN_DD"].ToString()))
                        {
                            _bCanModify = false;
                            errorMsg += "COMMON.HINT.HASSTADD";//�ѿ���
                            ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASSTADD");
                        }
                        if (_dtHead.Select("(ISNULL(QTY_FIN,0) + ISNULL(QTY_FIN_UNSH,0)) > 0 ").Length > 0)
                        {
                            _bCanModify = false;
                            errorMsg += "COMMON.HINT.HASQTYFIN";//���нɿ�����
                            ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASQTYFIN");
                        }
                        if (_dtHead.Select("(ISNULL(QTY_ML,0) + ISNULL(QTY_ML_UNSH,0)) > 0 ").Length > 0)
                        {
                            _bCanModify = false;
                            errorMsg += "COMMON.HINT.HASQTYML";//������������
                            //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASQTYML");
                        }
                        if (_dtHead.Select("(ISNULL(QTY_RK,0) + ISNULL(QTY_RK_UNSH,0)) > 0 ").Length > 0)
                        {
                            _bCanModify = false;
                            errorMsg += "COMMON.HINT.HASQTYRK";//�����������
                            //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASQTYRK");
                        }

                        if (_dtBody.Select("(ISNULL(QTY,0) + ISNULL(QTY_UNSH,0)) > 0").Length > 0)
                        {
                            _bCanModify = false;
                            errorMsg += "COMMON.HINT.HASQTY";//����ԭ��
                            //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASQTY");
                        }
                        if (_dtBody.Select("(ISNULL(QTY_TS,0) + ISNULL(QTY_TS_UNSH,0)) > 0 ").Length > 0)
                        {
                            _bCanModify = false;
                            errorMsg += "COMMON.HINT.HASQTYTS";//�ѵ�����
                            //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASQTYTS");
                        }
                        if (_dtBody.Select("(ISNULL(QTY_QL,0) + ISNULL(QTY_QL_UNSH,0))> 0 ").Length > 0)
                        {
                            _bCanModify = false;
                            errorMsg += "COMMON.HINT.HASQTYQL";//��������
                            //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.HASQTYQL");
                        }
                    }
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return errorMsg;
        }
        #endregion

        #region �������

        #region ����
        /// <summary>
        /// ������� 
        /// ��������˵��:
        /// CHK_QTY �Ƿ������Ӧ���������������ʵ����������һ����λ����Ϊ������ (��:F ��:T)
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="changedDs"></param>
        /// <param name="bubbleException"></param>
        public DataTable UpdateData(string pgm, SunlikeDataSet changedDs, bool bubbleException)
        {
            DataTable _dtHead = changedDs.Tables["MF_MO"];
            DataTable _dtBody = changedDs.Tables["TF_MO"];
            #region ȡ�õ��ݵ����״̬
            if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = _dtHead.Rows[0]["USR"].ToString();
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["ISSVS"].ToString()) && _dtHead.Rows[0]["ISSVS"].ToString() == "T")
                {
                    _isSvs = true;
                }
            }
            else
            {
                _loginUsr = _dtHead.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(_dtHead.Rows[0]["ISSVS", System.Data.DataRowVersion.Original].ToString()) && _dtHead.Rows[0]["ISSVS", System.Data.DataRowVersion.Original].ToString() == "T")
                {
                    _isSvs = true;
                }

            }
            Auditing _auditing = new Auditing();
            DataRow _dr = _dtHead.Rows[0];
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
            //_isRunAuditing = _auditing.IsRunAuditing("MO", _loginUsr, _bilType, _mobID);


            #endregion

            #region �Ƿ��л�д���
            if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
            {
                if (!_hasReturn && !string.IsNullOrEmpty(_dtHead.Rows[0]["OPN_DD"].ToString()))
                {
                    _hasReturn = true;
                }
                if (!_hasReturn && !string.IsNullOrEmpty(_dtHead.Rows[0]["QTY_FIN"].ToString()) && Convert.ToDecimal(_dtHead.Rows[0]["QTY_FIN"].ToString()) > 0)
                {
                    _hasReturn = true;
                }
                if (!_hasReturn && !string.IsNullOrEmpty(_dtHead.Rows[0]["QTY_ML"].ToString()) && Convert.ToDecimal(_dtHead.Rows[0]["QTY_ML"].ToString()) > 0)
                {
                    _hasReturn = true;
                }
                if (!_hasReturn && !string.IsNullOrEmpty(_dtHead.Rows[0]["QTY_RK"].ToString()) && Convert.ToDecimal(_dtHead.Rows[0]["QTY_RK"].ToString()) > 0)
                {
                    _hasReturn = true;
                }
                if (_dtBody.Rows.Count > 0)
                {
                    DataRow[] _drBody = _dtBody.Select();
                    for (int i = 0; i < _drBody.Length; i++)
                    {
                        if (!_hasReturn && !string.IsNullOrEmpty(_drBody[i]["QTY"].ToString()) && Convert.ToDecimal(_drBody[i]["QTY"].ToString()) > 0)
                        {
                            _hasReturn = true;
                            break;
                        }

                        if (!_hasReturn && !string.IsNullOrEmpty(_drBody[i]["QTY_TS"].ToString()) && Convert.ToDecimal(_drBody[i]["QTY_TS"].ToString()) > 0)
                        {
                            _hasReturn = true;
                            break;
                        }
                        if (!_hasReturn && !string.IsNullOrEmpty(_drBody[i]["QTY_QL"].ToString()) && Convert.ToDecimal(_drBody[i]["QTY_QL"].ToString()) > 0)
                        {
                            _hasReturn = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (!_hasReturn && !string.IsNullOrEmpty(_dtHead.Rows[0]["OPN_DD", DataRowVersion.Original].ToString()))
                {
                    _hasReturn = true;
                }
                if (!_hasReturn && !string.IsNullOrEmpty(_dtHead.Rows[0]["QTY_FIN", DataRowVersion.Original].ToString()) && Convert.ToDecimal(_dtHead.Rows[0]["QTY_FIN", DataRowVersion.Original].ToString()) > 0)
                {
                    _hasReturn = true;
                }
                if (!_hasReturn && !string.IsNullOrEmpty(_dtHead.Rows[0]["QTY_ML", DataRowVersion.Original].ToString()) && Convert.ToDecimal(_dtHead.Rows[0]["QTY_ML", DataRowVersion.Original].ToString()) > 0)
                {
                    _hasReturn = true;
                }
                if (!_hasReturn && !string.IsNullOrEmpty(_dtHead.Rows[0]["QTY_RK", DataRowVersion.Original].ToString()) && Convert.ToDecimal(_dtHead.Rows[0]["QTY_RK", DataRowVersion.Original].ToString()) > 0)
                {
                    _hasReturn = true;
                }
                if (_dtBody.Rows.Count > 0)
                {
                    DataRow[] _drBody = _dtBody.Select();
                    for (int i = 0; i < _drBody.Length; i++)
                    {
                        if (!_hasReturn && !string.IsNullOrEmpty(_drBody[i]["QTY", DataRowVersion.Original].ToString()) && Convert.ToDecimal(_drBody[i]["QTY", DataRowVersion.Original].ToString()) > 0)
                        {
                            _hasReturn = true;
                            break;
                        }

                        if (!_hasReturn && !string.IsNullOrEmpty(_drBody[i]["QTY_TS", DataRowVersion.Original].ToString()) && Convert.ToDecimal(_drBody[i]["QTY_TS", DataRowVersion.Original].ToString()) > 0)
                        {
                            _hasReturn = true;
                            break;
                        }
                        if (!_hasReturn && !string.IsNullOrEmpty(_drBody[i]["QTY_QL", DataRowVersion.Original].ToString()) && Convert.ToDecimal(_drBody[i]["QTY_QL", DataRowVersion.Original].ToString()) > 0)
                        {
                            _hasReturn = true;
                            break;
                        }
                    }
                }
            }
            #endregion
            #region �Ƿ������޸�
            string _pgm = "";

            //ȡ��PGM
            if (changedDs.ExtendedProperties.Contains("PGM"))
            {
                _pgm = changedDs.ExtendedProperties["PGM"].ToString();
            }
            if (string.IsNullOrEmpty(_pgm))
            {
                if (_dtHead.Rows.Count > 0 && _dtHead.Rows[0].RowState != DataRowState.Deleted && _dtHead.Rows[0]["ISSVS"].ToString() == "T")
                    _pgm = "MTNMO";
                else
                    _pgm = "MRPMO";
            }
            if (!string.IsNullOrEmpty(pgm))
            {
                _pgm = pgm;
            }
            Sunlike.Business.UserProperty _usrProp = new UserProperty();
            string _strAllowUpdate = _usrProp.GetData(_loginUsr, _pgm, "ALLOW_UPDATE");
            if (string.Compare(_strAllowUpdate, "T") != 0)
            {
                if (_hasReturn)
                    _allowUpdate = false;
            }

            #endregion

            //�ж���Դ���Ƿ������Ӧ���������������ʵ����������һ����λ����Ϊ������
            if (changedDs.ExtendedProperties.Contains("CHK_QTY"))
            {
                if (changedDs.ExtendedProperties["CHK_QTY"].ToString() == "F")
                {
                    _checkQty = false;
                }
            }
            System.Collections.Hashtable _ht = new Hashtable();
            _ht["MF_MO"] = " MO_NO,MO_DD,STA_DD,END_DD,BIL_ID,BIL_NO,MRP_NO,PRD_MARK,WH,SO_NO,UNIT,QTY,QTY1,"
                         + " NEED_DD,DEP,CUS_NO,CLOSE_ID,USR,CHK_MAN,BAT_NO,REM,PO_OK,MO_NO_ADD,"
                         + " QTY_FIN,TIME_AJ,QTY_ML,BUILD_BIL,CST_MAKE,CST_PRD,CST_OUT,CST_MAN,USED_TIME,"
                         + " CST,PRT_SW,OPN_DD,FIN_DD,BIL_MAK,CPY_SW,CONTRACT,EST_ITM,ML_OK,MD_NO,QTY_RK,"
                         + " CLS_DATE,ID_NO,QTY_CHK,CONTROL,ISNORMAL,QC_YN,MM_CURML,TS_ID,BIL_TYPE,CNTT_NO,"
                         + " MOB_ID,LOCK_MAN,SEB_NO,GRP_NO,OUT_DD_MOJ,SYS_DATE,PG_ID,SUP_PRD_NO,TIME_CNT,"
                         + " ML_BY_MM,CAS_NO,TASK_ID,OLD_ID,CF_ID,CUS_OS_NO,PRT_USR,QTY_QL,QL_ID,Q2_ID,"
                         + " Q3_ID,ISSVS,QTY_DM,LOCK,QTY_LOST,ISFROMQD,ZT_ID,ZT_DD,CV_ID";
            _ht["TF_MO"] = " MO_NO,ITM,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY_RSV,QTY_LOST,QTY,BAT_NO,REM,"
                         + " CST,ZC_NO,TW_ID,ZC_REM,USEIN,CPY_SW,USEIN_NO,PRD_NO_CHG,QTY1_RSV,QTY1_LOST,"
                         + " ID_NO,MD_NO,QTY_TS,TS_ITM,COMPOSE_IDNO,EST_ITM,PRE_ITM,LOS_RTO,QTY_STD,SEB_NO,"
                         + " GRP_NO,ZC_PRD,CHG_RTO,CHG_ITM,QTY_CHG_RTO,QTY_QL,QTY1_QL,QTY_DM,QTY1_DM,QTY_BL,CHK_RTN";
            this.UpdateDataSet(changedDs, _ht);

            if (!changedDs.HasErrors)
            {
                #region δ��������
                //���ӵ���Ȩ��
                string _UpdUsr = "";
                if (changedDs.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changedDs.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    DataTable _dtMf = changedDs.Tables["MF_MO"];
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
                #endregion
            }
            else
            {
                #region ��������
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDs);
                    throw new SunlikeException("RCID=MRPMO.UpdateData() Error;" + _errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changedDs);
                }
                #endregion
            }
            return Sunlike.Business.BizObject.GetAllErrors(changedDs);
        }
        #endregion

        #region BeforeUpdate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, System.Data.StatementType statementType, System.Data.DataRow dr, ref System.Data.UpdateStatus status)
        {
            #region �ж��Ƿ�����
            string _moNo = "";
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _moNo = dr["MO_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _moNo = dr["MO_NO"].ToString();
                }
                //�ж��Ƿ�����������Ѿ����������޸ġ�
                Users _Users = new Users();
                string _whereStr = "MO_NO = '" + _moNo + "'";
                if (_Users.IsLocked("MF_MO", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion

            if (tableName == "MF_MO")
            {
                #region ����
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["MO_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["MO_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion

                if (statementType != System.Data.StatementType.Delete)
                {
                    #region �����ͷ��Ϣ�Ƿ���ȷ
                    if (dr["BIL_ID"].ToString() != "TR")
                    {
                        //�ͻ�����
                        Cust _cust = new Cust();
                        if (!string.IsNullOrEmpty(dr["CUS_NO"].ToString()))
                        {
                            if (!_cust.IsExist(_loginUsr, dr["CUS_NO"].ToString()))
                            {
                                dr.SetColumnError("CUS_NO",/*�ͻ����Ų����ڻ�û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString() + "");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }
                    //���ţ����
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_loginUsr, dr["DEP"].ToString(), Convert.ToDateTime(dr["MO_DD"])))
                    {
                        dr.SetColumnError("DEP",/*���Ŵ���[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //��Ʒ��⣨���
                    Prdt _prdt = new Prdt();
                    if (!_prdt.IsExist(_loginUsr, dr["MRP_NO"].ToString(), Convert.ToDateTime(dr["MO_DD"])))
                    {
                        dr.SetColumnError("MRP_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + dr["MRP_NO"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�䷽��

                    //MRPBom _bom = new MRPBom();
                    //MTNWBom _wBom = new MTNWBom();
                    //bool _isExistsBom = false;
                    //if (!string.IsNullOrEmpty(dr["ID_NO"].ToString()))
                    //{
                    //    //����ά���䷽
                    //    if (_isSvs)
                    //    {
                    //        if (_wBom.ChkExistsByBomNo(dr["ID_NO"].ToString()))
                    //        {
                    //            _isExistsBom = true;
                    //        }
                    //    }
                    //    if (!_isExistsBom)
                    //    {
                    //        //���ұ�׼�䷽
                    //        if (_bom.IsExists(dr["ID_NO"].ToString()))
                    //        {
                    //            _isExistsBom = true;
                    //        }
                    //    }
                    //    if (!_isExistsBom)
                    //    {
                    //        dr.SetColumnError("ID_NO",/*�䷽��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.BOMERROR,PARAM=" + dr["ID_NO"].ToString());
                    //        status = UpdateStatus.SkipAllRemainingRows;
                    //    }
                    //}
                    //����
                    string _mark = dr["PRD_MARK"].ToString();
                    int _prdMod = _prdt.CheckPrdtMod(dr["MRP_NO"].ToString(), _mark);
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
                                if (!_prd_Mark.IsExist(_markName, dr["MRP_NO"].ToString(), _prd_markAry[i]))
                                {
                                    dr.SetColumnError(dr.Table.Columns["PRD_MARK"],/*��Ʒ����[{0}]������,����*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[i].Trim());
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                    //����
                    //if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    //{
                    //    Bat _bat = new Bat();
                    //    if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                    //    {
                    //        dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM=" + dr["BAT_NO"].ToString());
                    //        status = UpdateStatus.SkipAllRemainingRows;
                    //    }
                    //}
                    //Ԥ��ֿ�
                    if (!String.IsNullOrEmpty(dr["WH"].ToString()))
                    {
                        WH _wh = new WH();
                        if (_wh.IsExist(_loginUsr, dr["WH"].ToString(), Convert.ToDateTime(dr["MO_DD"])) == false)
                        {
                            dr.SetColumnError("WH",/*�ֿ����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.WHERROR,PARAM=" + dr["WH"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                    }

                    #endregion

                    #region �ж��Ƿ������޸ı�ͷ
                    if (_hasReturn)
                    {
                        if (dr.RowState == DataRowState.Modified)
                        {
                            //��ת�����ϻ�ɿ������޸�
                            MRPML _ml = new MRPML();
                            if (_ml.IsExistsForMo(dr["MO_NO"].ToString()))
                            {
                                for (int i = 0; i < dr.Table.Columns.Count; i++)
                                {
                                    if (string.Compare(dr.Table.Columns[i].ColumnName, "CUS_NO") == 0
                                        || string.Compare(dr.Table.Columns[i].ColumnName, "SO_NO") == 0
                                        || string.Compare(dr.Table.Columns[i].ColumnName, "MRP_NO") == 0
                                        || string.Compare(dr.Table.Columns[i].ColumnName, "ID_NO") == 0
                                        || string.Compare(dr.Table.Columns[i].ColumnName, "QTY") == 0
                                        || string.Compare(dr.Table.Columns[i].ColumnName, "PRD_MARK") == 0
                                        || string.Compare(dr.Table.Columns[i].ColumnName, "UNIT") == 0
                                        )
                                    {
                                        if (string.Compare(dr[dr.Table.Columns[i].ColumnName].ToString(), dr[dr.Table.Columns[i].ColumnName, DataRowVersion.Original].ToString()) != 0)
                                        {
                                            throw new SunlikeException(/*��ͷ�������޸�*/"RCID=MTN.HINT.HEADCHANGE");
                                        }
                                    }

                                }
                            }

                        }
                    }
                    #endregion
                }

                if (statementType == StatementType.Insert)
                {
                    #region --���ɵ���

                    SQNO SunlikeSqNo = new SQNO();
                    DateTime _dtMoDd = System.DateTime.Now;
                    if (dr["MO_DD"] is System.DBNull)
                    {
                        _dtMoDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _dtMoDd = Convert.ToDateTime(dr["MO_DD"]);
                    }
                    _moNo = SunlikeSqNo.Set("MO", _loginUsr, dr["DEP"].ToString(), _dtMoDd, dr["BIL_TYPE"].ToString());
                    dr["MO_NO"] = _moNo;
                    dr["MO_DD"] = _dtMoDd.ToString(Comp.SQLDateFormat);
                    #endregion

                    #region ȱʡ����
                    if (string.IsNullOrEmpty(dr["UNIT"].ToString()))
                        dr["UNIT"] = "1";
                    dr["CF_ID"] = "T";
                    dr["CONTROL"] = "F";
                    dr["CLOSE_ID"] = "F";
                    dr["PRT_SW"] = "N";
                    dr["MM_CURML"] = "F";
                    dr["QL_ID"] = "F";
                    dr["Q2_ID"] = "F";
                    dr["Q3_ID"] = "F";
                    dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    #endregion

                    #region ���Ų�����������
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        Bat _bat = new Bat();
                        if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                        {
                            _bat.AutoInsertData(dr["BAT_NO"].ToString(), dr["MRP_NO"].ToString(), Convert.ToDateTime(dr["MO_DD"].ToString()));
                        }
                    }
                    #endregion
                }

                //#region ��˹���
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.BIL_ID = "MO";
                //    _aps.BIL_NO = dr["MO_NO"].ToString();
                //    if (string.IsNullOrEmpty(dr["MO_DD"].ToString()))
                //        _aps.BIL_DD = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                //    else
                //        _aps.BIL_DD = Convert.ToDateTime(dr["MO_DD"].ToString());
                //    _aps.USR = _loginUsr;
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = "";
                //    _aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
                //}
                //else
                //    _aps = new AudParamStruct("MO", Convert.ToString(dr["MO_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

                if (statementType == System.Data.StatementType.Delete)
                {
                    #region �ж��Ƿ���ɾ����ǰ��
                    if (!string.IsNullOrEmpty(dr["QTY_FIN", DataRowVersion.Original].ToString()) && Convert.ToDecimal(dr["QTY_FIN", DataRowVersion.Original].ToString()) > 0)
                    {
                        throw new SunlikeException(/*���нɿ�����*/"RCID=COMMON.HINT.HASQTYFIN");
                    }
                    if (!string.IsNullOrEmpty(dr["QTY_ML", DataRowVersion.Original].ToString()) && Convert.ToDecimal(dr["QTY_ML", DataRowVersion.Original].ToString()) > 0)
                    {
                        throw new SunlikeException(/*��������*/"RCID=COMMON.HINT.HASQTYML");
                    }
                    if (!string.IsNullOrEmpty(dr["QTY_RK", DataRowVersion.Original].ToString()) && Convert.ToDecimal(dr["QTY_RK", DataRowVersion.Original].ToString()) > 0)
                    {
                        throw new SunlikeException(/*��������*/"RCID=COMMON.HINT.HASQTYRK");
                    }
                    #endregion
                }
            }
            else if (tableName == "TF_MO")
            {
                if (statementType != System.Data.StatementType.Delete)
                {
                    #region �ж��Ƿ������޸ı���
                    if (!_allowUpdate)
                    {
                        if (dr.RowState == DataRowState.Modified)
                        {
                            for (int i = 0; i < dr.Table.Columns.Count; i++)
                            {
                                if (string.Compare(dr[dr.Table.Columns[i].ColumnName].ToString(), dr[dr.Table.Columns[i].ColumnName, DataRowVersion.Original].ToString()) != 0)
                                {
                                    throw new SunlikeException(/*���������޸�*/"RCID=MTN.HINT.BODYCHANGE");
                                }
                            }

                        }

                    }
                    #endregion

                    #region ���������Ϣ�Ƿ���ȷ
                    //��Ʒ��⣨���
                    Prdt _prdt = new Prdt();
                    if (!_prdt.IsExist(_loginUsr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MO"].Rows[0]["MO_DD"])))
                    {
                        dr.SetColumnError("PRD_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + dr["PRD_NO"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�ֿ��⣨���
                    WH _wh = new WH();
                    if (_wh.IsExist(_loginUsr, dr["WH"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MO"].Rows[0]["MO_DD"])) == false)
                    {
                        dr.SetColumnError("WH",/*�ֿ����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.WHERROR,PARAM=" + dr["WH"].ToString());
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
                                    dr.SetColumnError(_markName,/*��Ʒ����[{0}]������,����*/"RCID=MTN.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[i].Trim());
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                    //����
                    //if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    //{
                    //    Bat _bat = new Bat();
                    //    if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                    //    {
                    //        dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM=" + dr["BAT_NO"].ToString());
                    //        status = UpdateStatus.SkipAllRemainingRows;
                    //    }
                    //}
                    //�䷽��
                    //MRPBom _bom = new MRPBom();
                    //MTNWBom _wBom = new MTNWBom();
                    //bool _isExistsBom = false;
                    //if (!string.IsNullOrEmpty(dr["ID_NO"].ToString()))
                    //{
                    //    //����ά���䷽
                    //    if (_isSvs)
                    //    {
                    //        if (_wBom.ChkExistsByBomNo(dr["ID_NO"].ToString()))
                    //        {
                    //            _isExistsBom = true;
                    //        }
                    //    }
                    //    if (!_isExistsBom)
                    //    {
                    //        //���ұ�׼�䷽
                    //        if (_bom.IsExists(dr["ID_NO"].ToString()))
                    //        {
                    //            _isExistsBom = true;
                    //        }
                    //    }
                    //    if (!_isExistsBom)
                    //    {
                    //        dr.SetColumnError("ID_NO",/*�䷽��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.BOMERROR,PARAM=" + dr["ID_NO"].ToString());
                    //        status = UpdateStatus.SkipAllRemainingRows;
                    //    }
                    //}
                    #endregion

                    #region �л�д����ʱ��Ʒ�š����������š���λ����λ�������޸�
                    if (statementType == System.Data.StatementType.Update)
                    {
                        bool _hasReturnQty = false;
                        if (!_hasReturnQty && !string.IsNullOrEmpty(dr["QTY"].ToString()) && Convert.ToDecimal(dr["QTY"].ToString()) > 0)
                        {
                            _hasReturnQty = true;
                        }
                        if (!_hasReturnQty && !string.IsNullOrEmpty(dr["QTY_TS"].ToString()) && Convert.ToDecimal(dr["QTY_TS"].ToString()) > 0)
                        {
                            _hasReturnQty = true;
                        }
                        if (!_hasReturnQty && !string.IsNullOrEmpty(dr["QTY_QL"].ToString()) && Convert.ToDecimal(dr["QTY_QL"].ToString()) > 0)
                        {
                            _hasReturnQty = true;
                        }
                        if (_hasReturnQty)
                        {
                            if (string.Compare(dr["PRD_NO"].ToString(), dr["PRD_NO", DataRowVersion.Original].ToString()) != 0)
                            {
                                dr.SetColumnError("PRD_NO",/*�л�д������Ʒ�Ų��ܱ仯*/"RCID=MTN.HINT.HASQTYPRD_NO");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            if (string.Compare(dr["PRD_MARK"].ToString().Trim(), dr["PRD_MARK", DataRowVersion.Original].ToString().Trim()) != 0)
                            {
                                dr.SetColumnError("PRD_MARK",/*�л�д�������������ܱ仯*/"RCID=MTN.HINT.HASQTYPRD_MARK");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }

                            if (string.Compare(dr["BAT_NO"].ToString(), dr["BAT_NO", DataRowVersion.Original].ToString()) != 0)
                            {
                                dr.SetColumnError("BAT_NO",/*�л�д���������Ų��ܱ仯*/"RCID=MTN.HINT.HASQTYBAT_NO");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            if (string.Compare(dr["WH"].ToString(), dr["WH", DataRowVersion.Original].ToString()) != 0)
                            {
                                dr.SetColumnError("WH",/*�л�д��������λ���ܱ仯*/"RCID=MTN.HINT.HASQTYWH");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                            if (string.Compare(dr["UNIT"].ToString(), dr["UNIT", DataRowVersion.Original].ToString()) != 0)
                            {
                                dr.SetColumnError("UNIT",/*�л�д��������λ���ܱ仯*/"RCID=MTN.HINT.HASQTYUNIT");
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }
                    #endregion
                }
                if (statementType == StatementType.Insert)
                {
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        Bat _bat = new Bat();
                        if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                        {
                            _bat.AutoInsertData(dr["BAT_NO"].ToString(), dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_MO"].Rows[0]["MO_DD"]));
                        }
                    }

                    #region ȱʡ����
                    dr["TS_ITM"] = dr["EST_ITM"];
                    if (dr["PRD_MARK"] == System.DBNull.Value)
                    {
                        dr["PRD_MARK"] = "";
                    }
                    #endregion
                }
                if (statementType != System.Data.StatementType.Delete)
                {
                    #region Ӧ���������������ʵ����������һ����λ����Ϊ��
                    if (_checkQty)
                    {
                        bool _isZero = true;
                        if (!string.IsNullOrEmpty(dr["QTY_RSV"].ToString()) && Convert.ToDecimal(dr["QTY_RSV"].ToString()) != 0)
                            _isZero = false;
                        if (_isZero && !string.IsNullOrEmpty(dr["QTY_LOST"].ToString()) && Convert.ToDecimal(dr["QTY_LOST"].ToString()) != 0)
                            _isZero = false;
                        if (_isZero && !string.IsNullOrEmpty(dr["QTY"].ToString()) && Convert.ToDecimal(dr["QTY"].ToString()) != 0)
                            _isZero = false;
                        if (_isZero)
                        {
                            dr.SetColumnError("QTY_RSV",/*Ӧ���������������ʵ����������һ����λ����Ϊ��,����*/"RCID=MTN.TF_MO.QTYERROR");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    #endregion

                    #region Ӧ������+�����С��������
                    decimal _qtyMo = 0;
                    decimal _qtyHas = 0;
                    if (!string.IsNullOrEmpty(dr["QTY_RSV"].ToString()) && Convert.ToDecimal(dr["QTY_RSV"].ToString()) != 0)
                        _qtyMo += Convert.ToDecimal(dr["QTY_RSV"].ToString());
                    if (!string.IsNullOrEmpty(dr["QTY_LOST"].ToString()) && Convert.ToDecimal(dr["QTY_LOST"].ToString()) != 0)
                        _qtyMo += Convert.ToDecimal(dr["QTY_LOST"].ToString());
                    if (!string.IsNullOrEmpty(dr["QTY"].ToString()) && Convert.ToDecimal(dr["QTY"].ToString()) != 0)
                        _qtyHas += Convert.ToDecimal(dr["QTY"].ToString());
                    if (_qtyMo < _qtyHas)
                    {
                        dr.SetColumnError("QTY_RSV",/*Ӧ������+�����С��������,����*/"RCID=MTN.TF_MO.HASQTYERROR");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }

                    #endregion

                }
                if (statementType == System.Data.StatementType.Delete)
                {
                    #region �ж��Ƿ���ɾ����ǰ��
                    if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()) && Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString()) > 0)
                    {
                        throw new SunlikeException(/*����ԭ��*/"RCID=COMMON.HINT.HASQTY");
                    }
                    if (!string.IsNullOrEmpty(dr["QTY_TS", DataRowVersion.Original].ToString()) && Convert.ToDecimal(dr["QTY_TS", DataRowVersion.Original].ToString()) > 0)
                    {
                        throw new SunlikeException(/*�ѵ�����*/"RCID=COMMON.HINT.HASQTYTS");
                    }
                    if (!string.IsNullOrEmpty(dr["QTY_BL", DataRowVersion.Original].ToString()) && Convert.ToDecimal(dr["QTY_BL", DataRowVersion.Original].ToString()) > 0)
                    {
                        throw new SunlikeException(/*���в�������*/"RCID=COMMON.HINT.HASQTYBL");
                    }
                    #endregion
                }

            }
            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }
        #endregion

        #region BeforeDsSave
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables["MF_MO"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "MO");
            //}
            //#endregion


            base.BeforeDsSave(ds);
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
        protected override void AfterUpdate(string tableName, System.Data.StatementType statementType, System.Data.DataRow dr, ref System.Data.UpdateStatus status, int recordsAffected)
        {
            if (tableName == "MF_MO")
            {
                #region ɾ������
                if (statementType == StatementType.Delete)
                {
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(dr["MO_NO", DataRowVersion.Original].ToString(), _loginUsr);//ɾ��ʱ��BILD�в���һ������
                }
                #endregion

                #region �޸�������
                if (!_isRunAuditing)
                {
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateWhQtyOnPrc(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateWhQtyOnPrc(dr, true);
                        this.UpdateWhQtyOnPrc(dr, false);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateWhQtyOnPrc(dr, true);
                    }
                }
                #endregion

                #region �޸���Դ������Ϣ
                #endregion
            }
            else if (tableName == "TF_MO")
            {
                #region �޸�δ����
                if (!_isRunAuditing)
                {
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateWhQtyOnRsv(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateWhQtyOnRsv(dr, true);
                        this.UpdateWhQtyOnRsv(dr, false);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateWhQtyOnRsv(dr, true);
                    }
                }
                #endregion
            }
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        #endregion

        #region �޸�������
        /// <summary>
        /// �ж�����᰸�Ƿ����:�ǽ᰸���ۼ�δ����,��������������
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <param name="originalCloseId">ԭ�Ƚ᰸���</param>    
        /// <param name="delQtyLost">�Ƿ�ɾ�����ϸ���</param>
        /// <param name="qtyFin">Ҫɾ������</param>
        /// <returns></returns>
        private string UpdateWhQtyOnPrc(string moNo, bool originalCloseId, bool delQtyLost, decimal qtyFin)
        {
            string _result = "";
            bool _closeIdNew = false;
            SunlikeDataSet _dsMo = this.GetUpdateData(moNo, false);
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables.Contains("MF_MO") && _dsMo.Tables["MF_MO"].Rows.Count > 0)
            {
                if (string.Compare("T", _dsMo.Tables["MF_MO"].Rows[0]["CLOSE_ID"].ToString()) == 0)
                {
                    _closeIdNew = true;
                }
                if (originalCloseId != _closeIdNew)
                {
                    try
                    {
                        DataTable _dtHead = _dsMo.Tables["MF_MO"];
                        for (int i = 0; i < _dtHead.Rows.Count; i++)
                        {
                            if (delQtyLost)
                            {
                                _dtHead.Rows[i]["QTY_LOST"] = System.DBNull.Value;
                                _dtHead.AcceptChanges();
                            }
                            else
                            {
                                if (qtyFin != 0)
                                {
                                    decimal _qtyFinUpdate = 0;
                                    if (!string.IsNullOrEmpty(_dtHead.Rows[i]["QTY_FIN"].ToString()))
                                        _qtyFinUpdate = Convert.ToDecimal(_dtHead.Rows[i]["QTY_FIN"].ToString());
                                    _qtyFinUpdate -= qtyFin;
                                    _dtHead.Rows[i]["QTY_FIN"] = _qtyFinUpdate;
                                    _dtHead.AcceptChanges();
                                }
                            }
                            if (_closeIdNew)
                            {
                                _dtHead.Rows[i].Delete();
                                UpdateWhQtyOnPrc(_dtHead.Rows[i], true);
                            }
                            else
                            {
                                UpdateWhQtyOnPrc(_dtHead.Rows[i], false);
                            }
                        }
                    }
                    catch (Exception _ex)
                    {
                        _result = _ex.Message.ToString();
                    }
                }
                else
                {
                    _result = "FALSE";
                }
            }
            return _result;
        }
        /// <summary>
        /// �޸�δ��������������
        /// </summary>
        /// <param name="dr">��ͷ��Ϣ��</param>
        /// <param name="isDel"></param>
        private void UpdateWhQtyOnPrc(DataRow dr, bool isDel)
        {
            string _batNo = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _validDd = "";
            string _unit = "";
            decimal _qty = 0;

            if (isDel)
            {
                _prdNo = dr["MRP_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    _qty = (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                if (!string.IsNullOrEmpty(dr["QTY_FIN", DataRowVersion.Original].ToString()))
                    _qty += Convert.ToDecimal(dr["QTY_FIN", DataRowVersion.Original].ToString());
                if (!string.IsNullOrEmpty(dr["QTY_LOST", DataRowVersion.Original].ToString()))
                    _qty += Convert.ToDecimal(dr["QTY_LOST", DataRowVersion.Original].ToString());
                if (_qty > 0)
                    _qty = 0;
            }
            else
            {
                _prdNo = dr["MRP_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _batNo = dr["BAT_NO"].ToString();
                _whNo = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qty = Convert.ToDecimal(dr["QTY"].ToString());
                if (!string.IsNullOrEmpty(dr["QTY_FIN"].ToString()))
                    _qty -= Convert.ToDecimal(dr["QTY_FIN"].ToString());
                if (!string.IsNullOrEmpty(dr["QTY_LOST"].ToString()))
                    _qty -= Convert.ToDecimal(dr["QTY_LOST"].ToString());
                if (_qty < 0)
                    _qty = 0;
            }
            this.UpdateWhQtyOnPrc(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _qty);
        }
        /// <summary>
        /// �޸�δ��������δ����
        /// </summary>
        /// <param name="batNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="whNo"></param>
        /// <param name="validDd"></param>
        /// <param name="unit"></param>
        /// <param name="qtyRsv"></param>
        private void UpdateWhQtyOnPrc(string batNo, string prdNo, string prdMark, string whNo, string validDd, string unit, decimal qtyPrc)
        {
            WH _wh = new WH();
            System.Collections.Hashtable _fields = new System.Collections.Hashtable();
            _fields[WH.QtyTypes.QTY_ON_PRC] = qtyPrc;
            if (string.IsNullOrEmpty(batNo))
            {
                _wh.UpdateQty(prdNo, prdMark, whNo, unit, _fields);
            }
            else
            {
                Prdt _prdt = new Prdt();
                SunlikeDataSet _ds = _prdt.GetBatRecData(batNo, prdNo, prdMark, whNo);
                if (!String.IsNullOrEmpty(validDd))
                {
                    if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                    {
                        TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(validDd));
                        if (_timeSpan.Days > 0)
                        {
                            _wh.UpdateQty(batNo, prdNo, prdMark, whNo, "", unit, _fields);
                        }
                        else
                        {
                            _wh.UpdateQty(batNo, prdNo, prdMark, whNo, validDd, unit, _fields);
                        }
                    }
                    else
                    {
                        _wh.UpdateQty(batNo, prdNo, prdMark, whNo, validDd, unit, _fields);
                    }
                }
                else
                {
                    _wh.UpdateQty(batNo, prdNo, prdMark, whNo, "", unit, _fields);
                }
            }
        }

        #endregion

        #region �޸�δ����
        /// <summary>
        /// �ж�����᰸�Ƿ����:�ǽ᰸���ۼ�δ����,��������δ����
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <param name="originalCloseId">ԭ�Ƚ᰸���</param>
        /// <returns></returns>
        private string UpdateWhQtyOnRsv(string moNo, bool originalCloseId)
        {
            string _result = "";
            bool _closeIdNew = false;
            SunlikeDataSet _dsMo = this.GetUpdateData(moNo, false);
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables.Contains("MF_MO") && _dsMo.Tables["MF_MO"].Rows.Count > 0)
            {
                if (string.Compare("T", _dsMo.Tables["MF_MO"].Rows[0]["CLOSE_ID"].ToString()) == 0)
                {
                    _closeIdNew = true;
                }
                if (originalCloseId != _closeIdNew)
                {
                    try
                    {
                        DataTable _dtBody = _dsMo.Tables["TF_MO"];
                        for (int i = 0; i < _dtBody.Rows.Count; i++)
                        {
                            if (_closeIdNew)
                            {
                                _dtBody.Rows[i].Delete();
                                UpdateWhQtyOnRsv(_dtBody.Rows[i], true);
                            }
                            else
                            {
                                UpdateWhQtyOnRsv(_dtBody.Rows[i], false);
                            }
                        }
                    }
                    catch (Exception _ex)
                    {
                        _result = _ex.Message.ToString();
                    }

                }
            }
            return _result;
        }
        /// <summary>
        /// ���޸�δ��������δ����
        /// </summary>
        /// <param name="dr">������Ϣ��</param>
        /// <param name="isDel"></param>
        private void UpdateWhQtyOnRsv(DataRow dr, bool isDel)
        {
            string _batNo = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _validDd = "";
            string _unit = "";
            decimal _qtyRsv = 0;

            if (isDel)
            {
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["QTY_RSV", DataRowVersion.Original].ToString()))
                    _qtyRsv += (-1) * Convert.ToDecimal(dr["QTY_RSV", DataRowVersion.Original].ToString());
                if (!string.IsNullOrEmpty(dr["QTY_LOST", DataRowVersion.Original].ToString()))
                    _qtyRsv += (-1) * Convert.ToDecimal(dr["QTY_LOST", DataRowVersion.Original].ToString());
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    _qtyRsv += Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                if (_qtyRsv > 0)
                    _qtyRsv = 0;
            }
            else
            {
                _prdNo = dr["PRD_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _batNo = dr["BAT_NO"].ToString();
                _whNo = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["QTY_RSV"].ToString()))
                    _qtyRsv += Convert.ToDecimal(dr["QTY_RSV"].ToString());
                if (!string.IsNullOrEmpty(dr["QTY_LOST"].ToString()))
                    _qtyRsv += Convert.ToDecimal(dr["QTY_LOST"].ToString());
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qtyRsv += (-1) * Convert.ToDecimal(dr["QTY"].ToString());
                if (_qtyRsv < 0)
                    _qtyRsv = 0;
            }
            UpdateWhQtyOnRsv(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _qtyRsv);
        }
        /// <summary>
        /// �޸�δ��������δ����
        /// </summary>
        /// <param name="batNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="prdMark"></param>
        /// <param name="whNo"></param>
        /// <param name="validDd"></param>
        /// <param name="unit"></param>
        /// <param name="qtyRsv"></param>
        private void UpdateWhQtyOnRsv(string batNo, string prdNo, string prdMark, string whNo, string validDd, string unit, decimal qtyRsv)
        {
            WH _wh = new WH();
            System.Collections.Hashtable _fields = new System.Collections.Hashtable();
            _fields[WH.QtyTypes.QTY_ON_RSV] = qtyRsv;
            if (string.IsNullOrEmpty(batNo))
            {
                _wh.UpdateQty(prdNo, prdMark, whNo, unit, _fields);
            }
            else
            {
                Prdt _prdt = new Prdt();
                SunlikeDataSet _ds = _prdt.GetBatRecData(batNo, prdNo, prdMark, whNo);
                if (!String.IsNullOrEmpty(validDd))
                {
                    if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                    {
                        TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(validDd));
                        if (_timeSpan.Days > 0)
                        {
                            _wh.UpdateQty(batNo, prdNo, prdMark, whNo, "", unit, _fields);
                        }
                        else
                        {
                            _wh.UpdateQty(batNo, prdNo, prdMark, whNo, validDd, unit, _fields);
                        }
                    }
                    else
                    {
                        _wh.UpdateQty(batNo, prdNo, prdMark, whNo, validDd, unit, _fields);
                    }
                }
                else
                {
                    _wh.UpdateQty(batNo, prdNo, prdMark, whNo, "", unit, _fields);
                }
            }
        }
        #endregion


        #endregion

        #region �޸ı�ͷ������
        /// <summary>
        /// �޸ı�ͷ������
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <param name="unit"></param>
        /// <param name="qtyMl">��������</param>
        /// <param name="opnDd">��������</param>
        public void UpdateQtyMl(string moNo, string unit, decimal qtyMl, DateTime opnDd)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            string _unitMo = "";
            string _prdNo = "";
            SunlikeDataSet _dsMo = _mo.GetDataHead(moNo);
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables.Contains("MF_MO") && _dsMo.Tables["MF_MO"].Rows.Count > 0)
            {
                _unitMo = _dsMo.Tables["MF_MO"].Rows[0]["UNIT"].ToString();
                _prdNo = _dsMo.Tables["MF_MO"].Rows[0]["MRP_NO"].ToString();
            }
            Prdt _prdt = new Prdt();
            //���㵥λ
            decimal _qtyMlNew = _prdt.GetUnitQty(_prdNo, unit, qtyMl, _unitMo);
            _mo.UpdateQtyMl(moNo, _qtyMlNew);
            if (qtyMl > 0)
            {
                _mo.UpdateOpnDd(moNo, opnDd);
            }
            else
            {
                _mo.UpdateOpnDdEmpty(moNo);
            }
        }
        #endregion

        #region �޸Ŀ�������
        /// <summary>
        /// �޸Ŀ�������
        /// </summary>
        /// <param name="moNo"></param>
        /// <param name="opnDd"></param>
        public void UpdateOpnDd(string moNo, DateTime opnDd)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            _mo.UpdateOpnDd(moNo, opnDd);
        }
        /// <summary>
        /// ���ÿ�������ΪNULL
        /// </summary>
        /// <param name="moNo"></param>
        public void UpdateOpnDdEmpty(string moNo)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            _mo.UpdateOpnDdEmpty(moNo);
        }
        #endregion

        #region �޸ı���ʵ����
        /// <summary>
        /// �޸ı���ʵ����
        /// </summary>
        /// <param name="moNo">���</param>
        /// <param name="estItm">׷�����</param>
        /// <param name="unit">ԭ��λ</param>
        /// <param name="qty">ԭ����</param>
        public void UpdateQty(string moNo, int estItm, string unit, decimal qty)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            string _batNo = "";
            string _unitMo = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _validDd = "";
            string _unit = "";
            decimal _qtyMo = 0;//��������
            decimal _qtyMoLeft = 0;//���ʣ��Ӧ����
            decimal _qtyRsv = 0;//Ҫ��д����δ����
            bool _isClsId = false;//�Ƿ��޸�δ����

            SunlikeDataSet _dsMo = _mo.GetDataBody(moNo, "EST_ITM", estItm);
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables.Contains("MF_MO") && _dsMo.Tables["MF_MO"].Rows.Count > 0)
            {
                if (string.Compare(_dsMo.Tables["MF_MO"].Rows[0]["CLOSE_ID"].ToString(), "T") == 0)
                {
                    _isClsId = true;
                }
            }
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables.Contains("TF_MO") && _dsMo.Tables["TF_MO"].Rows.Count > 0)
            {
                _unitMo = _dsMo.Tables["TF_MO"].Rows[0]["UNIT"].ToString();
                _prdNo = _dsMo.Tables["TF_MO"].Rows[0]["PRD_NO"].ToString();
                _prdMark = _dsMo.Tables["TF_MO"].Rows[0]["PRD_MARK"].ToString();
                _batNo = _dsMo.Tables["TF_MO"].Rows[0]["BAT_NO"].ToString();
                _whNo = _dsMo.Tables["TF_MO"].Rows[0]["WH"].ToString();
                _unit = _dsMo.Tables["TF_MO"].Rows[0]["UNIT"].ToString();
                if (!string.IsNullOrEmpty(_dsMo.Tables["TF_MO"].Rows[0]["QTY"].ToString()))
                    _qtyMo = Convert.ToDecimal(_dsMo.Tables["TF_MO"].Rows[0]["QTY"].ToString());
                if (!string.IsNullOrEmpty(_dsMo.Tables["TF_MO"].Rows[0]["QTY_RSV"].ToString()))
                    _qtyMoLeft += Convert.ToDecimal(_dsMo.Tables["TF_MO"].Rows[0]["QTY_RSV"].ToString());
                if (!string.IsNullOrEmpty(_dsMo.Tables["TF_MO"].Rows[0]["QTY_LOST"].ToString()))
                    _qtyMoLeft += Convert.ToDecimal(_dsMo.Tables["TF_MO"].Rows[0]["QTY_LOST"].ToString());
            }
            else
            {
                return;
            }
            //����
            Prdt _prdt = new Prdt();
            //���㵥λ
            decimal _qtyNew = _prdt.GetUnitQty(_prdNo, unit, qty, _unitMo);
            //�޸ı���ʵ����
            _mo.UpdateQty(moNo, "EST_ITM", estItm, "QTY", _qtyNew);
            if (!_isClsId)
            {
                //����������λ
                _qtyRsv = _prdt.GetUnitQty(_prdNo, unit, qty, _unitMo);
                if (_qtyRsv > 0)
                {
                    if (_qtyMoLeft > _qtyMo)
                    {
                        _qtyMoLeft -= _qtyMo;//����������
                    }
                    else
                    {
                        _qtyMoLeft = 0;
                    }
                }
                else
                {
                    _qtyMoLeft = System.Math.Abs(_qtyRsv) - _qtyMo + _qtyMoLeft;//�ɿ��� - ������ + ʣ��Ӧ����
                    if (_qtyMoLeft < 0)
                    {
                        return;
                    }
                }
                if (System.Math.Abs(_qtyRsv) > _qtyMoLeft)
                {
                    if (_qtyRsv < 0)
                        _qtyRsv = (-1) * _qtyMoLeft;
                    else
                        _qtyRsv = _qtyMoLeft;
                }
                //��д��λδ����
                this.UpdateWhQtyOnRsv(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, (-1) * _qtyRsv);
            }
        }
        #endregion

        #region �޸ı�������
        /// <summary>
        /// �޸ı�������
        /// </summary>
        /// <param name="moNo">���</param>
        /// <param name="estItm">׷�����</param>
        /// <param name="unit">ԭ��λ</param>
        /// <param name="qtyBl">ԭ����</param>
        public void UpdateQtyBl(string moNo, int estItm, string unit, decimal qtyBl)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            string _unitMo = "";
            string _prdNo = "";
            SunlikeDataSet _dsMo = _mo.GetDataBody(moNo, "EST_ITM", estItm);
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables.Contains("TF_MO") && _dsMo.Tables["TF_MO"].Rows.Count > 0)
            {
                _unitMo = _dsMo.Tables["TF_MO"].Rows[0]["UNIT"].ToString();
                _prdNo = _dsMo.Tables["TF_MO"].Rows[0]["PRD_NO"].ToString();
            }
            else
            {
                return;
            }
            //����
            Prdt _prdt = new Prdt();
            //���㵥λ
            decimal _qtyNew = _prdt.GetUnitQty(_prdNo, unit, qtyBl, _unitMo);
            //�޸ı�������
            _mo.UpdateQty(moNo, "EST_ITM", estItm, "QTY_BL", _qtyNew);
        }
        #endregion

        #region �޸ı���׷�����
        /// <summary>
        /// �޸ı���׷�����
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <param name="prdNo">��Ʒ</param>
        /// <param name="prdName">Ʒ��</param>
        /// <param name="prdMark">����</param>
        /// <param name="wh">��λ</param>
        /// <param name="useInNo">��װλ�ô���</param>
        /// <param name="unit">��λ</param>
        /// <returns></returns>
        public int UpdateEstItm(string moNo, string prdNo, string prdName, string prdMark, string wh, string useInNo, string unit)
        {
            int _result = 0;
            SunlikeDataSet _dsMoBody = null;
            if (!string.IsNullOrEmpty(useInNo))
            {
                _dsMoBody = this.GetDataBody(moNo, prdNo, prdMark, "", useInNo);
            }
            if (_dsMoBody == null || (_dsMoBody != null && _dsMoBody.Tables["TF_MO"].Rows.Count == 0))
            {
                _dsMoBody = this.GetDataBody(moNo, prdNo, prdMark, wh, "");
            }
            else
            {
                if (!string.IsNullOrEmpty(_dsMoBody.Tables["TF_MO"].Rows[0]["EST_ITM"].ToString()))
                {
                    _result = Convert.ToInt32(_dsMoBody.Tables["TF_MO"].Rows[0]["EST_ITM"].ToString());
                }
            }
            if (_dsMoBody == null || (_dsMoBody != null && _dsMoBody.Tables["TF_MO"].Rows.Count == 0))
            {
                _dsMoBody = this.GetDataBody(moNo, prdNo, prdMark, "", "");
            }
            else
            {
                if (!string.IsNullOrEmpty(_dsMoBody.Tables["TF_MO"].Rows[0]["EST_ITM"].ToString()))
                {
                    _result = Convert.ToInt32(_dsMoBody.Tables["TF_MO"].Rows[0]["EST_ITM"].ToString());
                }
            }

            if (_dsMoBody == null || (_dsMoBody != null && _dsMoBody.Tables["TF_MO"].Rows.Count == 0))
            {
                //�¼�һ�ʱ���
                SunlikeDataSet _dsMo = this.GetUpdateData(moNo, false);
                DataRow _drNew = _dsMo.Tables["TF_MO"].NewRow();
                _drNew["MO_NO"] = moNo;
                _drNew["ITM"] = _dsMo.Tables["TF_MO"].Rows.Count + 1;
                _drNew["PRD_NO"] = prdNo;
                _drNew["PRD_NAME"] = prdName;
                _drNew["PRD_MARK"] = prdMark;
                _drNew["WH"] = wh;
                _drNew["USEIN_NO"] = useInNo;
                _drNew["UNIT"] = unit;
                _drNew["REM"] = "���ϵ�����ԭ��";
                _dsMo.Tables["TF_MO"].Rows.Add(_drNew);
                if (!string.IsNullOrEmpty(_drNew["EST_ITM"].ToString()))
                    _result = Convert.ToInt32(_drNew["EST_ITM"].ToString());
                _dsMo.ExtendedProperties["CHK_QTY"] = "F";
                this.UpdateData("", _dsMo, true);
            }
            else
            {
                if (!string.IsNullOrEmpty(_dsMoBody.Tables["TF_MO"].Rows[0]["EST_ITM"].ToString()))
                {
                    _result = Convert.ToInt32(_dsMoBody.Tables["TF_MO"].Rows[0]["EST_ITM"].ToString());
                }
            }
            return _result;
        }
        #endregion

        #region �޸ı�ͷ�ѽɿ���
        /// <summary>
        /// �޸ı�ͷ�ѽɿ���
        /// </summary>
        /// <param name="moNo"></param>
        /// <param name="unit"></param>
        /// <param name="qtyFin"></param>
        public void UpdateQtyFin(string moNo, string unit, decimal qtyFin)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            string _batNo = "";
            string _unitMo = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _validDd = "";
            bool _originalCLoseId = false;
            decimal _qtyLeft = 0;
            decimal _qtyFinMo = 0;
            decimal _qtyFin = 0;
            SunlikeDataSet _dsMo = _mo.GetDataHead(moNo);
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables.Contains("MF_MO") && _dsMo.Tables["MF_MO"].Rows.Count > 0)
            {
                _unitMo = _dsMo.Tables["MF_MO"].Rows[0]["UNIT"].ToString();
                _prdNo = _dsMo.Tables["MF_MO"].Rows[0]["MRP_NO"].ToString();
                _prdMark = _dsMo.Tables["MF_MO"].Rows[0]["PRD_MARK"].ToString();
                _batNo = _dsMo.Tables["MF_MO"].Rows[0]["BAT_NO"].ToString();
                _whNo = _dsMo.Tables["MF_MO"].Rows[0]["WH"].ToString();
                if (string.Compare("T", _dsMo.Tables["MF_MO"].Rows[0]["CLOSE_ID"].ToString()) == 0)
                {
                    _originalCLoseId = true;
                }
                if (!string.IsNullOrEmpty(_dsMo.Tables["MF_MO"].Rows[0]["MRP_NO"].ToString()))
                    _qtyLeft = Convert.ToDecimal(_dsMo.Tables["MF_MO"].Rows[0]["QTY"].ToString());
                if (!string.IsNullOrEmpty(_dsMo.Tables["MF_MO"].Rows[0]["QTY_FIN"].ToString()))
                    _qtyFinMo = Convert.ToDecimal(_dsMo.Tables["MF_MO"].Rows[0]["QTY_FIN"].ToString());

            }
            Prdt _prdt = new Prdt();
            //���㵥λ
            decimal _qtyFinNew = _prdt.GetUnitQty(_prdNo, unit, qtyFin, _unitMo);
            //�޸�����ѽɿ�����
            _mo.UpdateQtyFin(moNo, _qtyFinNew);

            //ת�������λ
            _qtyFin = _prdt.GetUnitQty(_prdNo, unit, qtyFin, _unitMo);
            if (_qtyFin > 0)
            {
                if (_qtyLeft > _qtyFinMo)
                {
                    _qtyLeft -= _qtyFinMo;//�����ѽɿ���
                }
                else
                {
                    _qtyLeft = 0;
                }
            }
            else
            {
                _qtyLeft = System.Math.Abs(_qtyFin) - _qtyFinMo + _qtyLeft;//�ɿ��� - ������ + ʣ��Ӧ����
                if (_qtyLeft < 0)
                {
                    return;
                }
            }
            if (System.Math.Abs(_qtyFin) > _qtyLeft)
            {
                if (_qtyFin < 0)
                    _qtyFin = (-1) * _qtyLeft;
                else
                    _qtyFin = _qtyLeft;
            }

            //�ж��깤���Ƿ�᰸:�ǣ��ۼ�������,��������������
            string _result = this.UpdateWhQtyOnPrc(moNo, _originalCLoseId, true, _qtyFin);
            //�ж��깤���Ƿ�᰸:�ǣ��ۼ�δ����,��������δ����
            UpdateWhQtyOnRsv(moNo, _originalCLoseId);
            //��д������
            if (_qtyFin > 0)
            {
                this.UpdateWhQtyOnPrc(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unitMo, (-1) * _qtyFin);
            }
            else if (string.Compare("FALSE", _result) == 0)
            {
                this.UpdateWhQtyOnPrc(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unitMo, (-1) * _qtyFin);
            }
        }
        #endregion

        #region �޸ı�ͷ����ϸ���(QTY_RK)�����ϸ���(QTY_LOST)
        /// <summary>
        /// �޸ı�ͷ����ϸ���(QTY_RK)�����ϸ���(QTY_LOST)
        /// </summary>
        /// <param name="moNo"></param>
        /// <param name="unit"></param>
        /// <param name="qtyChk"></param>
        /// <param name="qtyLost"></param>
        public void UpdateQtyChk(string moNo, string unit, decimal qtyChk, decimal qtyLost)
        {
            Prdt _prdt = new Prdt();
            string _batNo = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _validDd = "";
            string _unitMo = "";
            bool _originalCLoseId = false;
            SunlikeDataSet _ds = this.GetUpdateData(moNo, false);
            if (_ds.Tables["MF_MO"].Rows.Count > 0)
            {
                _batNo = _ds.Tables["MF_MO"].Rows[0]["BAT_NO"].ToString();
                _prdNo = _ds.Tables["MF_MO"].Rows[0]["MRP_NO"].ToString();
                _prdMark = _ds.Tables["MF_MO"].Rows[0]["PRD_MARK"].ToString();
                _unitMo = _ds.Tables["MF_MO"].Rows[0]["UNIT"].ToString();
                _whNo = _ds.Tables["MF_MO"].Rows[0]["WH"].ToString();
                qtyChk = _prdt.GetUnitQty(_prdNo, unit, qtyChk, _unitMo);
                qtyLost = _prdt.GetUnitQty(_prdNo, unit, qtyLost, _unitMo);
                if (string.Compare("T", _ds.Tables["MF_MO"].Rows[0]["CLOSE_ID"].ToString()) == 0)
                {
                    _originalCLoseId = true;
                }
            }
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            //ת�������λ
            qtyLost = _prdt.GetUnitQty(_prdNo, unit, qtyLost, _unitMo);
            qtyChk = _prdt.GetUnitQty(_prdNo, unit, qtyChk, _unitMo);
            _mo.UpdateQtyChk(moNo, qtyChk, qtyLost);
            this.UpdateClsId(moNo);
            //if (qtyLost < 0)
            //    qtyLost = 0;
            //this.UpdateWhQtyOnPrc(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unitMo, (-1) * qtyLost);
            //�ж��깤���Ƿ�᰸:�ǣ��ۼ�δ����,�����������������ѻ�д��
            this.UpdateWhQtyOnPrc(moNo, _originalCLoseId, true, 0);
            //�ж��깤���Ƿ�᰸:�ǣ��ۼ�δ����,��������δ����
            UpdateWhQtyOnRsv(moNo, _originalCLoseId);

        }
        #endregion

        #region �ж��Ƿ���Ҫǿ���˻�
        /// <summary>
        /// �ж��Ƿ���Ҫǿ���˻�(�н᰸���)
        /// ���ر�ṹ
        /// MO_NO,PRE_ITM,PRD_NO,PRD_NAME,UNIT,QTY
        /// </summary>
        /// <param name="moNo">ά�޵���</param>
        /// <returns></returns>
        public DataTable GetChkRtnInfo(string moNo)
        {
            DataTable _result = new DataTable();
            _result.Columns.Add("MO_NO", typeof(System.String));
            _result.Columns.Add("PRE_ITM", typeof(System.Int32));
            _result.Columns.Add("PRD_NO", typeof(System.String));
            _result.Columns.Add("PRD_NAME", typeof(System.String));
            _result.Columns.Add("UNIT", typeof(System.String));
            _result.Columns.Add("QTY", typeof(System.Decimal));
            SunlikeDataSet _dsMo = this.GetUpdateData(moNo, false);
            if (_dsMo.Tables["MF_MO"].Rows.Count > 0 && string.Compare("T", _dsMo.Tables["MF_MO"].Rows[0]["CLOSE_ID"].ToString()) == 0)
            {
                DataRow[] _drSel = _dsMo.Tables["TF_MO"].Select("ISNULL(CHK_RTN,'F')='T' AND ISNULL(QTY,0) > 0 ");
                if (_drSel.Length > 0)
                {
                    for (int i = 0; i < _drSel.Length; i++)
                    {
                        DataRow _dr = _result.NewRow();
                        _dr["MO_NO"] = _drSel[i]["MO_NO"];
                        _dr["PRE_ITM"] = _drSel[i]["PRE_ITM"];
                        _dr["PRD_NO"] = _drSel[i]["PRD_NO"];
                        _dr["PRD_NAME"] = _drSel[i]["PRD_NAME"];
                        _dr["UNIT"] = _drSel[i]["UNIT"];
                        _dr["QTY"] = _drSel[i]["QTY"];
                        _result.Rows.Add(_dr);
                    }
                }
            }
            return _result;
        }
        /// <summary>
        /// �ж��Ƿ���Ҫǿ���˻�(�޽᰸��ǣ������ͼ���)
        /// ���ر�ṹ
        /// MO_NO,PRE_ITM,PRD_NO,PRD_NAME,UNIT,QTY
        /// </summary>
        /// <param name="moNo">ά�޵���</param>
        /// <param name="unit">�ͼ���λ</param>
        /// <param name="qty">�ͼ�����</param>
        /// <returns></returns>
        public DataTable GetChkRtnInfoTI(string moNo)
        {
            DataTable _result = new DataTable();
            _result.Columns.Add("MO_NO", typeof(System.String));
            _result.Columns.Add("PRE_ITM", typeof(System.Int32));
            _result.Columns.Add("PRD_NO", typeof(System.String));
            _result.Columns.Add("PRD_NAME", typeof(System.String));
            _result.Columns.Add("UNIT", typeof(System.String));
            _result.Columns.Add("QTY", typeof(System.Decimal));
            bool _chkRtn = false;
            SunlikeDataSet _dsMo = this.GetUpdateData(moNo, false);
            if (_dsMo.Tables["MF_MO"].Rows.Count > 0)
            {
                decimal _qtyMo = 0;
                if (!string.IsNullOrEmpty(_dsMo.Tables["MF_MO"].Rows[0]["QTY"].ToString()))
                    _qtyMo = Convert.ToDecimal(_dsMo.Tables["MF_MO"].Rows[0]["QTY"]);
                if (!string.IsNullOrEmpty(_dsMo.Tables["MF_MO"].Rows[0]["QTY_RK"].ToString()))
                    _qtyMo -= Convert.ToDecimal(_dsMo.Tables["MF_MO"].Rows[0]["QTY_RK"]);
                if (_qtyMo <= 0)
                {
                    _chkRtn = true;
                }
            }
            if (_chkRtn)
            {
                DataRow[] _drSel = _dsMo.Tables["TF_MO"].Select("ISNULL(CHK_RTN,'F')='T' AND ISNULL(QTY,0) > 0 ");
                if (_drSel.Length > 0)
                {
                    for (int i = 0; i < _drSel.Length; i++)
                    {
                        DataRow _dr = _result.NewRow();
                        _dr["MO_NO"] = _drSel[i]["MO_NO"];
                        _dr["PRE_ITM"] = _drSel[i]["PRE_ITM"];
                        _dr["PRD_NO"] = _drSel[i]["PRD_NO"];
                        _dr["PRD_NAME"] = _drSel[i]["PRD_NAME"];
                        _dr["UNIT"] = _drSel[i]["UNIT"];
                        _dr["QTY"] = _drSel[i]["QTY"];
                        _result.Rows.Add(_dr);
                    }
                }
            }
            return _result;
        }
        #endregion

        #region �޸������
        /// <summary>
        /// �޸������
        /// </summary>
        /// <param name="bilId">���ݱ�</param>
        /// <param name="bilNo">����</param>
        /// <param name="chkMan">�����</param>
        /// <param name="clsDd">�����</param>
        /// <returns></returns>
        public bool UpdateChkMan(string bilId, string bilNo, string chkMan, DateTime clsDd)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            return _mo.UpdateChkMan(bilId, bilNo, chkMan, clsDd);
        }
        #endregion

        #region �᰸���
        /// <summary>
        /// �޸ı�ͷ�ѽɿ���
        /// ��д���������һ�д�ֹ��᰸���
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <param name="qtyFin">�ɿ���</param>
        /// <returns></returns>
        public void UpdateQtyFin(string moNo, decimal qtyFin)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            _mo.UpdateQtyFin(moNo, qtyFin);
        }
        #endregion

        #region �������Ƿ����
        /// <summary>
        /// �������Ƿ����
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <returns></returns>
        public bool IsExists(string moNo)
        {
            bool _result = false;
            SunlikeDataSet _ds = this.GetUpdateData(moNo, false);
            if (_ds.Tables.Count > 0 && _ds.Tables.Contains("MF_MO") && _ds.Tables["MF_MO"].Rows.Count > 0)
            {
                _result = true;
            }
            return _result;
        }
        #endregion

        #region �޸��������������
        /// <summary>
        /// �޸��������������
        /// <param name="moNo">�����</param>
        /// </summary>
        public void UpdateQtyMlOfMo(string moNo)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            _mo.UpdateQtyMlOfMo(moNo);
        }
        #endregion


        #region �᰸
        /// <summary>
        /// �᰸
        /// </summary>
        /// <param name="moId">���ݱ�</param>
        /// <param name="moNo">���ݺ���</param>
        /// <param name="close">�Ƿ�᰸</param>
        /// <returns></returns>
        public string DoCloseBill(string moId, string moNo, bool close)
        {
            return DoCloseBill(moId, moNo, close, false);
        }
        /// <summary>
        /// �᰸
        /// </summary>
        /// <param name="moId">���ݱ�</param>
        /// <param name="moNo">���ݺ���</param>
        /// <param name="close">�Ƿ�᰸</param>
        /// <param name="forceClose">�Ƿ�ǿ�ƽ᰸</param>
        /// <returns></returns>
        public string DoCloseBill(string moId, string moNo, bool close, bool forceClose)
        {
            bool _isCloseIt = false;
            bool _isCheck = true;
            string _result = "";
            SunlikeDataSet _billDs = this.GetUpdateData(moNo, false);
            DataTable _dtHead = _billDs.Tables["MF_MO"];
            DataTable _dtBody = _billDs.Tables["TF_MO"];
            if (_dtHead.Rows.Count > 0)
            {
                if (_dtHead.Rows[0]["CLOSE_ID"].ToString() == "T")
                    _isCloseIt = true;
                if (_dtHead.Rows[0]["CHK_MAN"].ToString().Length == 0)
                    _isCheck = false;
                if (close == _isCloseIt)
                {
                    if (close)
                    {
                        if (forceClose)//ǿ�ƽ᰸
                        {
                            return "";
                        }
                        else
                        {
                            return "RCID=COMMON.HINT.CLOSEERROR,PARAM=" + moNo;//�õ���[{0}]�ѽ᰸,�᰸�����������!
                        }
                    }
                    else
                    {
                        if (forceClose)//ǿ�ƽ᰸
                        {
                            return "";
                        }
                        else
                        {
                            return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + moNo;//�õ���[{0}]δ�᰸,δ�᰸�����������!
                        }
                    }
                }
                string _prdNo = "";
                string _prdMark = "";
                string _batNo = "";
                string _validDd = "";
                string _whNo = "";
                string _unit = "";
                decimal _qtyHead = 0;               //��ͷ����
                decimal _qtyFin = 0;               //�깤����

                decimal _qtyRsv = 0;                //Ӧ����
                decimal _qtyLost = 0;               //�����
                decimal _qty = 0;                   //��������
                decimal _qtyOnRsv = 0;              //δ����
                decimal _qtyOnPrc = 0;              //������
                WH _wh = new WH();
                try
                {
                    if (_isCheck)
                    {
                        //�᰸	
                        #region �ͷű�ͷ������
                        if (_dtHead.Rows.Count > 0)
                        {
                            _prdNo = _dtHead.Rows[0]["MRP_NO"].ToString();
                            _prdMark = _dtHead.Rows[0]["PRD_MARK"].ToString();
                            _batNo = _dtHead.Rows[0]["BAT_NO"].ToString();
                            _whNo = _dtHead.Rows[0]["WH"].ToString();
                            _unit = _dtHead.Rows[0]["UNIT"].ToString();
                            if (!string.IsNullOrEmpty(_dtHead.Rows[0]["QTY"].ToString()))
                                _qtyHead = Convert.ToDecimal(_dtHead.Rows[0]["QTY"]);
                            if (!string.IsNullOrEmpty(_dtHead.Rows[0]["QTY_FIN"].ToString()))
                                _qtyFin = Convert.ToDecimal(_dtHead.Rows[0]["QTY_FIN"]);
                            if (!string.IsNullOrEmpty(_dtHead.Rows[0]["QTY_LOST"].ToString()))
                                _qtyFin += Convert.ToDecimal(_dtHead.Rows[0]["QTY_LOST"]);
                            _qtyOnPrc = _qtyHead - _qtyFin;//��ͷ�������깤����
                            if (_qtyOnPrc < 0)
                                _qtyOnPrc = 0;
                            if (_qtyOnPrc > 0)//�޸Ŀ��������
                            {
                                if (close)//�۳�ʣ�������� 
                                {
                                    _qtyOnPrc = (-1) * _qtyOnPrc;
                                }

                                this.UpdateWhQtyOnPrc(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _qtyOnPrc);
                            }

                        }
                        #endregion

                        #region	�ͷű���δ����
                        for (int i = 0; i < _dtBody.Rows.Count; i++)
                        {
                            // ��ʼ��
                            _qtyRsv = 0;
                            _qtyLost = 0;
                            _qty = 0;
                            _prdNo = _dtBody.Rows[i]["PRD_NO"].ToString();
                            _prdMark = _dtBody.Rows[i]["PRD_MARK"].ToString();
                            _whNo = _dtBody.Rows[i]["WH"].ToString();
                            _batNo = _dtBody.Rows[i]["BAT_NO"].ToString();
                            _unit = _dtBody.Rows[i]["UNIT"].ToString();
                            //Ӧ����
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["QTY_RSV"].ToString()))
                            {
                                _qtyRsv = Convert.ToDecimal(_dtBody.Rows[i]["QTY_RSV"]);
                            }
                            //�����
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["QTY_LOST"].ToString()))
                            {
                                _qtyLost = Convert.ToDecimal(_dtBody.Rows[i]["QTY_LOST"]);
                            }
                            //��������
                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["QTY"].ToString()))
                            {
                                _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY"]);
                            }
                            _qtyOnRsv = _qtyRsv + _qtyLost - _qty;//Ӧ����+�����-��������
                            if (_qtyOnRsv < 0)
                                _qtyOnRsv = 0;

                            if (_qtyOnRsv > 0)//�޸Ŀ��δ����
                            {
                                if (close)//�۳�ʣ��δ���� 
                                {
                                    _qtyOnRsv = (-1) * _qtyOnRsv;
                                }
                                UpdateWhQtyOnRsv(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _qtyOnRsv);
                            }

                        }
                        #endregion
                    }
                    //���Ͻ᰸���                    
                    UpdateClsId(moNo, close);
                }
                catch (Exception _ex)
                {
                    _result = _ex.Message.ToString();
                }

            }
            return _result;
        }

        /// <summary>
        /// �ڵ����ϴ��Ͻ᰸���
        /// </summary>
        /// <param name="moNo"></param>
        /// <param name="close"></param>
        public bool UpdateClsId(string moNo, bool close)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            return _mo.UpdateClsId(moNo, close);
        }

        /// <summary>
        /// ���Զ����Ͻ᰸���
        /// </summary>
        /// <param name="moNo"></param>
        public bool UpdateClsId(string moNo)
        {
            DbMRPMO _mo = new DbMRPMO(Comp.Conn_DB);
            return _mo.UpdateClsId(moNo);
        }
        #endregion

        #region IAuditing Members
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
            string _errmsg = "";
            SunlikeDataSet _dsMo = this.GetUpdateData(bil_no, false);
            DataTable _dtHead = _dsMo.Tables["MF_MO"];
            DataTable _dtBody = _dsMo.Tables["TF_MO"];
            try
            {
                if (_dtHead.Rows.Count > 0)
                {
                    this.UpdateWhQtyOnPrc(_dtHead.Rows[0], false);
                }
                foreach (DataRow _dr in _dtBody.Rows)
                {
                    this.UpdateWhQtyOnRsv(_dr, false);
                }
                this.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
            }
            catch (Exception _ex)
            {
                _errmsg = _ex.Message.ToString();
            }
            return _errmsg;
        }
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no)
        {
            string _errmsg = "";
            SunlikeDataSet _dsMo = this.GetUpdateData(bil_no, false);

            string _errorMsg = this.SetCanModify(_dsMo, false);
            if (_dsMo.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
            {
                if (_errorMsg.Length > 0)
                {
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;RCID=" + _errorMsg;
                }
                else
                {
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";
                }

            }
            DataTable _dtHead = _dsMo.Tables["MF_MO"];
            DataTable _dtBody = _dsMo.Tables["TF_MO"];
            try
            {
                if (_dtHead.Rows.Count > 0)
                {
                    _dtHead.Rows[0].Delete();
                    this.UpdateWhQtyOnPrc(_dtHead.Rows[0], true);
                }
                foreach (DataRow _dr in _dtBody.Rows)
                {
                    this.UpdateWhQtyOnRsv(_dr, true);
                }
                this.UpdateChkMan(bil_id, bil_no, "", Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat)));
            }
            catch (Exception _ex)
            {
                _errmsg = _ex.Message.ToString();
            }
            return _errmsg;
        }

        #endregion

        #region ICloseBill Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        public string DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "CLS_ID")
            {
                _error = this.DoCloseBill(bil_id, bil_no, true);
            }
            return _error;

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
            string _error = "";
            if (cls_name == "CLS_ID")
            {
                _error = this.DoCloseBill(bil_id, bil_no, false);
            }
            return _error;
        }

        #endregion
    }
}
