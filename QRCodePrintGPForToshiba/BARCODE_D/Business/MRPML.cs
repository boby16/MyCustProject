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
    /// �졢�ˡ����ϵ�
    /// </summary>
    public class MRPML : BizObject, IAuditing
    {
        #region ����
        private string _mlId = "";
        private string _loginUsr;
        private bool _isRunAuditing;
        private Dictionary<string, string> _updateMoNo = new Dictionary<string, string>();
        /// <summary>
        /// �Ƿ���������ƾ֤
        /// </summary>
        private bool _reBuildVohNo = false;

        #endregion
        public MRPML()
        {
        }

        #region ȡ����
        /// <summary>
        /// ȡ�졢�ˡ����ϵ�
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="mlId"></param>
        /// <param name="mlNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string usr, string mlId, string mlNo, bool onlyFillSchema)
        {
            return this.GetUpdateData("MRPML", usr, mlId, mlNo, onlyFillSchema);
        }
        /// <summary>
        /// ȡ�졢�ˡ����ϵ�
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="usr"></param>
        /// <param name="mlId"></param>
        /// <param name="mlNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetUpdateData(string pgm, string usr, string mlId, string mlNo, bool onlyFillSchema)
        {
            DbMRPML _ml = new DbMRPML(Comp.Conn_DB);
            SunlikeDataSet _ds = _ml.GetData(mlId, mlNo, onlyFillSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;

                DataTable _dtHead = _ds.Tables["MF_ML"];
                if (_dtHead.Rows.Count > 0)
                {
                    string _bill_Dep = _dtHead.Rows[0]["DEP"].ToString();
                    string _bill_Usr = _dtHead.Rows[0]["USR"].ToString();
                    System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, usr, _bill_Dep, _bill_Usr);
                    _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                    _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                    _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                }
            }
            //�������������δ������
            if (_ds.Tables.Contains("TF_ML"))
            {
                if (!_ds.Tables["TF_ML"].Columns.Contains("QTY_LEFT_V"))
                {
                    _ds.Tables["TF_ML"].Columns.Add("QTY_LEFT_V", typeof(System.Decimal), string.Compare("M2", mlId) == 0 ? "" : "ISNULL(QTY_RSV,0)+ISNULL(QTY_LOST,0)-ISNULL(QTY_RTN,0) ");
                }
                if (!_ds.Tables["TF_ML"].Columns.Contains("QTY_LEFT_V1"))
                {
                    _ds.Tables["TF_ML"].Columns.Add("QTY_LEFT_V1", typeof(System.Decimal), string.Compare("M2", mlId) == 0 ? "" : "IIF(ISNULL(QTY_LEFT_V,0)> ISNULL(QTY,0),ISNULL(QTY_LEFT_V,0)-ISNULL(QTY,0),0)");
                }
            }
            _ds.Tables["TF_ML"].Columns["PRE_ITM"].AutoIncrement = true;
            _ds.Tables["TF_ML"].Columns["PRE_ITM"].AutoIncrementSeed = _ds.Tables["TF_ML"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_ML"].Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _ds.Tables["TF_ML"].Columns["PRE_ITM"].AutoIncrementStep = 1;
            this.SetCanModify(_ds, true);
            return _ds;
        }

        public SunlikeDataSet GetDataBody(string mlId, string mlNo, int preItm)
        {
            DbMRPML _ml = new DbMRPML(Comp.Conn_DB);
            return _ml.GetDataBody(mlId, mlNo, preItm);
        }

        /// <summary>
        /// ��ȡָ���й�������С������
        /// </summary>
        /// <param name="twNo">�й�����</param>
        /// <returns>��������</returns>
        public object GetMinMlDd(string twNo)
        {
            DbMRPML _db = new DbMRPML(Comp.Conn_DB);
            return _db.GetMinMlDd(twNo);
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
            DataTable _dtHead = ds.Tables["MF_ML"];
            DataTable _dtBody = ds.Tables["TF_ML"];
            string errorMsg = "";
            bool _bCanModify = true;
            if (_dtHead.Rows.Count > 0)
            {
                //�жϹ�����
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtHead.Rows[0]["ML_DD"]), _dtHead.Rows[0]["DEP"].ToString(), "CLS_MNU"))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.CLOSE_CLS";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_CLS");
                }
                //�ж��������
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_dtHead.Rows[0]["MLID"].ToString(), _dtHead.Rows[0]["ML_NO"].ToString()))
                    {
                        _bCanModify = false;
                        errorMsg += "COMMON.HINT.CLOSE_AUDIT";
                        //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
                if (!String.IsNullOrEmpty(_dtHead.Rows[0]["VOH_NO"].ToString()))
                {
                    //�ж�ƾ֤
                    string _acNo = "";
                    DrpVoh _drpVoh = new DrpVoh();
                    string _updUsr = "";
                    if (ds.ExtendedProperties.ContainsKey("UPD_USR"))
                    {
                        _updUsr = ds.ExtendedProperties["UPD_USR"].ToString();
                    }
                    else
                    {
                        _updUsr = _dtHead.Rows[0]["USR"].ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_dtHead.Rows[0]["VOH_NO"].ToString(), _updUsr, ref _acNo);
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
                //�ж��Ƿ�����
                if (!String.IsNullOrEmpty(_dtHead.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.CLOSE_LOCK";
                    //Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return errorMsg;
        }
        #endregion

        #region ���� �졢�ˡ����ϵ�

        #region ����
        /// <summary>
        /// ���� �졢�ˡ����ϵ�
        /// </summary>
        /// <param name="pgm"></param>
        /// <param name="changedDs"></param>
        /// <param name="bubbleException"></param>
        public DataTable UpdateData(string pgm, SunlikeDataSet changedDs, bool bubbleException)
        {
            DataTable _dtHead = changedDs.Tables["MF_ML"];
            #region ȡ�õ��ݵ����״̬
            if (_dtHead.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = _dtHead.Rows[0]["USR"].ToString();
                _mlId = _dtHead.Rows[0]["MLID"].ToString();
            }
            else
            {
                _loginUsr = _dtHead.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
                _mlId = _dtHead.Rows[0]["MLID", System.Data.DataRowVersion.Original].ToString();

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
            //_isRunAuditing = _auditing.IsRunAuditing(_mlId, _loginUsr, _bilType, _mobID);


            #endregion
            //�Ƿ��ؽ�ƾ֤����
            if (changedDs.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", changedDs.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }


            System.Collections.Hashtable _ht = new Hashtable();
            _ht["MF_ML"] = "MLID,ML_NO,ML_DD,BIL_ID,BIL_NO,ML_ID,FIX_CST,FIX_CST1,BAT_NO,MO_NO,MRP_NO,PRD_NAME,"
                        + " PRD_MARK,UNIT,WH_MTL,QTY,QTY1,VOH_ID,VOH_NO,USR_NO,DEP,REM,CUS_NO,TZ_NO,CPY_SW,"
                        + " FT_ID,USR,CHK_MAN,PRT_SW,CLS_DATE,ID_NO,LM_NO,BIL_TYPE,CNTT_NO,MOB_ID,LOCK_MAN,"
                        + " FJ_NUM,SYS_DATE,MM_NO,CAS_NO,TASK_ID,CUS_OS_NO,PRT_USR,QL_ID,QL_NO,MC_NO,VOH_ID_MC,"
                        + " QL_TYPE,IDX_NO,TAX_ID,CUR_ID,EXC_RTO,AMT,AMTN_NET,TAX,EP_ID,EP_NO,VOH_ID_EP,YL_ID,TAX_RTO ";
            _ht["TF_ML"] = " MLID,ML_NO,ITM,ML_DD,ML_ID,PRD_NO,PRD_NAME,PRD_MARK,UNIT,QTY,QTY1,WH,CST,REM,"
                        + " BAT_NO,CPY_SW,CST_STD,PRD_NO_CHG,ID_NO,PRD_NO_MO,QTY_LM,QTY_DIFF,CNTT_NO,COMPOSE_IDNO,"
                        + " PRE_ITM,EST_ITM,USEIN_NO,LOS_RTO,QTY_STD,ZC_PRD,QTY_LEFT,CHG_RTO,CHG_ITM,QTY_CHG_RTO,"
                        + " WH_LC,QTY_OVER,PK_ID,QTY_LC,RK_DD,QL_TYPE,QTY_ST,VALID_DD,DEP_RK,PW_ITM,QTY_QL_GG,MO_NO,"
                        + " QTY_RSV,QTY_LOST,QTY_RTN,FIX_CST1,QTY_ML,UNIT_H,TZ_NO";
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
                    string _pgm = "";
                    //ȡ��PGM
                    if (changedDs.ExtendedProperties.Contains("PGM"))
                    {
                        _pgm = changedDs.ExtendedProperties["PGM"].ToString();
                    }
                    if (string.IsNullOrEmpty(_pgm))
                    {
                        _pgm = "DRP" + this._mlId;
                    }
                    if (!string.IsNullOrEmpty(pgm))
                    {
                        _pgm = pgm;
                    }
                    DataTable _dtMf = changedDs.Tables["MF_ML"];
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
                    throw new SunlikeException("RCID=MRPML.UpdateData() Error:;" + _errorMsg);
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

        #region  BeforeDsSave
        /// <summary>
        /// BeforeDsSave
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables["MF_ML"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["MLID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["MLID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion

            #region ��������
            _updateMoNo.Clear();
            #endregion

            #region ���ӱ�ͷ������
            /*
             */
            if (!_isRunAuditing)
            {
                UpdateMoQtyBody(ds);
            }
            #endregion
            base.BeforeDsSave(ds);
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
            string _mlNo = GetStrDrValue(dr, "ML_NO");
            string _mlId = GetStrDrValue(dr, "MLID");            
            #endregion
            if (tableName == "MF_ML")
            {
                #region ����
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["ML_DD"]), dr["DEP"].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["ML_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_MNU"))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                #endregion

                if (statementType != System.Data.StatementType.Delete)
                {
                    #region �����ͷ��Ϣ�Ƿ���ȷ
                    //���ţ����
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_loginUsr, dr["DEP"].ToString(), Convert.ToDateTime(dr["ML_DD"])))
                    {
                        dr.SetColumnError("DEP",/*���Ŵ���[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //��λ�����
                    WH _wh = new WH();
                    if (!string.IsNullOrEmpty(dr["WH_MTL"].ToString()))
                    {
                        if (!_wh.IsExist(_loginUsr, dr["WH_MTL"].ToString(), Convert.ToDateTime(dr["ML_DD"])))
                        {
                            dr.SetColumnError("WH_MTL",/*���Ŵ���[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.WHERROR,PARAM=" + dr["WH_MTL"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //��Ʒ���
                    Prdt _prdt = new Prdt();
                    if (!string.IsNullOrEmpty(dr["MRP_NO"].ToString()))
                    {
                        if (!_prdt.IsExist(_loginUsr, dr["MRP_NO"].ToString(), Convert.ToDateTime(dr["ML_DD"])))
                        {
                            dr.SetColumnError("MRP_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + dr["MRP_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //�䷽��                    

                    if (!string.IsNullOrEmpty(dr["ID_NO"].ToString()))
                    {
                        MRPBom _bom = new MRPBom();
                        MTNWBom _wBom = new MTNWBom();
                        bool _isExistsBom = false;
                        //����ά���䷽
                        if (_wBom.ChkExistsByBomNo(dr["ID_NO"].ToString()))
                        {
                            _isExistsBom = true;
                        }
                        if (!_isExistsBom)
                        {
                            //���ұ�׼�䷽
                            if (_bom.IsExists(dr["ID_NO"].ToString()))
                            {
                                _isExistsBom = true;
                            }
                        }
                        if (!_isExistsBom)
                        {
                            dr.SetColumnError("ID_NO",/*�䷽��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.BOMERROR,PARAM=" + dr["ID_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
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
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        Bat _bat = new Bat();
                        if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                        {
                            dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM=" + dr["BAT_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //������
                    if (!String.IsNullOrEmpty(dr["USR_NO"].ToString()))
                    {
                        Salm _salm = new Salm();
                        if (_salm.IsExist(_loginUsr, dr["USR_NO"].ToString(), Convert.ToDateTime(dr["ML_DD"])) == false)
                        {
                            dr.SetColumnError("USR_NO",/*������[{0}]������û�ж��������Ȩ��,����*/"RCID=MTN.HINT.USR_NO_NOTEXIST,PARAM=" + dr["USR_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion
                }

                if (statementType == StatementType.Insert)
                {
                    #region --���ɵ���

                    SQNO SunlikeSqNo = new SQNO();
                    DateTime _dtMlDd = System.DateTime.Now;
                    if (dr["ML_DD"] is System.DBNull)
                    {
                        _dtMlDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _dtMlDd = Convert.ToDateTime(dr["ML_DD"]);
                    }
                    _mlNo = SunlikeSqNo.Set(dr["MLID"].ToString(), _loginUsr, dr["DEP"].ToString(), _dtMlDd, dr["BIL_TYPE"].ToString());
                    dr["ML_NO"] = _mlNo;
                    dr["ML_DD"] = _dtMlDd.ToString(Comp.SQLDateFormat);
                    #endregion

                    #region ȱʡ����
                    if (string.IsNullOrEmpty(dr["UNIT"].ToString()))
                        dr["UNIT"] = "1";
                    if (string.IsNullOrEmpty(dr["FIX_CST1"].ToString()))
                        dr["FIX_CST1"] = "1";
                    dr["PRT_SW"] = "N";
                    dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    #endregion
                }

                //#region ��˹���
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.BIL_ID = _mlId;
                //    _aps.BIL_NO = dr["ML_NO"].ToString();
                //    if (string.IsNullOrEmpty(dr["ML_DD"].ToString()))
                //        _aps.BIL_DD = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                //    else
                //        _aps.BIL_DD = Convert.ToDateTime(dr["ML_DD"].ToString());
                //    _aps.USR = _loginUsr;
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = "";
                //    _aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
                //}
                //else
                //    _aps = new AudParamStruct(_mlId, Convert.ToString(dr["ML_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

                #region ���ӱ�ͷ������
                //if (!_isRunAuditing)
                //{
                //    if (statementType == StatementType.Insert)
                //    {
                //        UpdateMoQtyMl(dr, false);
                //    }
                //    else if (statementType == StatementType.Update)
                //    {
                //        UpdateMoQtyMl(dr, true);
                //        UpdateMoQtyMl(dr, false);
                //    }
                //    else if (statementType == StatementType.Delete)
                //    {
                //        UpdateMoQtyMl(dr, true);
                //    }
                //}
                #endregion

                #region ����ƾ֤
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }
                #endregion


            }
            else if (tableName == "TF_ML")
            {
                if (statementType != System.Data.StatementType.Delete)
                {
                    #region ���ñ�����������
                    if (string.IsNullOrEmpty(dr["ML_DD"].ToString()))
                    {
                        dr["ML_DD"] = dr.Table.DataSet.Tables["MF_ML"].Rows[0]["ML_DD"];
                    }
                    #endregion

                    #region ���������Ϣ�Ƿ���ȷ
                    //��Ʒ��⣨���
                    Prdt _prdt = new Prdt();
                    if (!_prdt.IsExist(_loginUsr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_ML"].Rows[0]["ML_DD"])))
                    {
                        dr.SetColumnError("PRD_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + dr["PRD_NO"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�ֿ��⣨���
                    WH _wh = new WH();
                    if (_wh.IsExist(_loginUsr, dr["WH"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_ML"].Rows[0]["ML_DD"])) == false)
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
                                    dr.SetColumnError(_markName,/*��Ʒ����[{0}]������,����*/"RCID=MTN.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[i]);
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                    if (dr["PRD_MARK"] == System.DBNull.Value)
                    {
                        dr["PRD_MARK"] = "";
                    }
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        Bat _bat = new Bat();
                        if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                        {
                            dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM=" + dr["BAT_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //�䷽��
                    MRPBom _bom = new MRPBom();
                    MTNWBom _wBom = new MTNWBom();
                    bool _isExistsBom = false;
                    if (!string.IsNullOrEmpty(dr["ID_NO"].ToString()) && string.IsNullOrEmpty(dr["TZ_NO"].ToString()))
                    {

                        if (_wBom.ChkExistsByBomNo(dr["ID_NO"].ToString()))
                        {
                            _isExistsBom = true;
                        }
                        if (!_isExistsBom)
                        {
                            //���ұ�׼�䷽
                            if (_bom.IsExists(dr["ID_NO"].ToString()))
                            {
                                _isExistsBom = true;
                            }
                        }
                        if (!_isExistsBom)
                        {
                            dr.SetColumnError("ID_NO",/*�䷽��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.BOMERROR,PARAM=" + dr["ID_NO"].ToString());
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion

                    #region ����δ����������ԭδ����
                    dr["QTY_LEFT"] = dr["QTY_LEFT_V"];
                    #endregion

                    #region ���㳬����
                    decimal _deciQtyDiff = 0;
                    if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                        _deciQtyDiff = Convert.ToDecimal(dr["QTY"].ToString());
                    if (!string.IsNullOrEmpty(dr["QTY_RSV"].ToString()))
                        _deciQtyDiff -= Convert.ToDecimal(dr["QTY_RSV"].ToString());
                    if (_deciQtyDiff > 0)
                        dr["QTY_DIFF"] = _deciQtyDiff;
                    else
                        dr["QTY_DIFF"] = System.DBNull.Value;
                    #endregion

                    #region ȱʡֵ
                    //���
                    if (dr.Table.Columns.Contains("MO_NO"))
                    {
                        if (string.IsNullOrEmpty(dr["MO_NO"].ToString()))
                        {
                            if (dr.Table.DataSet.Tables.Contains("MF_ML") && dr.Table.DataSet.Tables["MF_ML"].Rows.Count > 0)
                            {
                                dr["MO_NO"] = dr.Table.DataSet.Tables["MF_ML"].Rows[0]["MO_NO"];
                            }
                        }
                    }
                    //�ɱ���
                    if (dr.Table.Columns.Contains("FIX_CST1"))
                    {
                        if (string.IsNullOrEmpty(dr["FIX_CST1"].ToString()))
                        {
                            if (dr.Table.DataSet.Tables.Contains("MF_ML") && dr.Table.DataSet.Tables["MF_ML"].Rows.Count > 0)
                            {
                                dr["FIX_CST1"] = dr.Table.DataSet.Tables["MF_ML"].Rows[0]["FIX_CST1"];
                            }
                        }
                    }
                    #endregion

                    #region ����ԭ���û�еı���
                    if (statementType == System.Data.StatementType.Insert)
                    {
                        if (string.IsNullOrEmpty(dr["EST_ITM"].ToString()))
                        {
                            dr["EST_ITM"] = UpdateMoEstItm(dr, false);
                            //��дʵ����
                            UpdateMoQty(dr, false);
                        }
                    }
                    #endregion
                }
                else
                {
                    #region ǿ���˻����Ƿ�����ɾ��
                    string _moNo = dr["MO_NO", DataRowVersion.Original].ToString();
                    string _estItm = dr["EST_ITM", DataRowVersion.Original].ToString();
                    if (!this.chkRtnDel(_moNo, _estItm))
                    {
                        throw new SunlikeException("RCID=MTN.HINT.CHKRTNFORBIT,PARAM=" + _moNo + ",PARAM=" + dr["PRD_NO", DataRowVersion.Original].ToString());
                    }
                    #endregion
                }

                #region ȡ���������ϵ���
                if (statementType == StatementType.Delete)
                {
                    if (dr.Table.Columns.Contains("MO_NO"))
                    {
                        if (!_updateMoNo.ContainsKey(dr["MO_NO", DataRowVersion.Original].ToString()))
                        {
                            _updateMoNo[dr["MO_NO", DataRowVersion.Original].ToString()] = "T";
                        }
                    }
                }
                else
                {
                    if (dr.Table.Columns.Contains("MO_NO"))
                    {
                        if (!_updateMoNo.ContainsKey(dr["MO_NO"].ToString()))
                        {
                            _updateMoNo[dr["MO_NO"].ToString()] = "T";
                        }
                    }
                }

                #endregion
            }
            base.BeforeUpdate(tableName, statementType, dr, ref status);
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
            if (tableName == "MF_ML")
            {
                #region ɾ������
                if (statementType == StatementType.Delete)
                {
                    SQNO SunlikeSqNo = new SQNO();
                    SunlikeSqNo.Delete(dr["ML_NO", DataRowVersion.Original].ToString(), _loginUsr);//ɾ��ʱ��BILD�в���һ������
                }
                #endregion

                #region ��д����Ŀ�������
                if (statementType == StatementType.Delete)
                {
                    UpdateMoOpnDdEmpty(dr["MO_NO", DataRowVersion.Original].ToString());
                }

                #endregion

            }
            else if (tableName == "TF_ML")
            {
                #region ��д�������ʵ�����������
                if (!_isRunAuditing)
                {
                    if (statementType == StatementType.Insert)
                    {
                        UpdateWhQty(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        UpdateWhQty(dr, true);
                        UpdateWhQty(dr, false);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        UpdateWhQty(dr, true);
                    }
                }
                #endregion
            }
            #region ��д�й������������ֶ�
            UpdateTWQtyML(dr.Table.DataSet.Tables[0].Rows[0]);
            #endregion
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        #endregion

        #region AfterDsSave
        /// <summary>
        /// AfterDsSave
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            UpdateMoMrpQty();

            base.AfterDsSave(ds);
        }
        #endregion

        #region �޸��������������
        private void UpdateMoMrpQty()
        {
            if (_updateMoNo.Count > 0)
            {
                MRPMO _mo = new MRPMO();
                foreach (KeyValuePair<string, string> kvp in _updateMoNo)
                {
                    _mo.UpdateQtyMlOfMo(kvp.Key);
                }
                _updateMoNo.Clear();
            }
        }
        #endregion

        #region �޸ı�ͷ������
        /// <summary>
        /// �޸ı�ͷ������
        /// </summary>
        /// <param name="dr">��ͷ��Ϣ��</param>
        /// <param name="isDel"></param>
        private void UpdateMoQtyMl(DataRow dr, bool isDel)
        {
            string _mlId = "";
            string _moNo = "";
            string _unit = "1";
            string _ylId = "F";//���粹��־
            Decimal _qtyMl = 0;
            DateTime _mlDd = System.DateTime.Now;
            if (isDel)
            {
                _mlId = dr["MLID", DataRowVersion.Original].ToString();
                _moNo = dr["MO_NO", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                _ylId = dr["YL_ID", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["ML_DD", DataRowVersion.Original].ToString()))
                    _mlDd = Convert.ToDateTime(dr["ML_DD", DataRowVersion.Original].ToString());
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    _qtyMl = (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
            }
            else
            {
                _mlId = dr["MLID"].ToString();
                _moNo = dr["MO_NO"].ToString();
                _unit = dr["UNIT"].ToString();
                _ylId = dr["YL_ID"].ToString();
                if (!string.IsNullOrEmpty(dr["ML_DD"].ToString()))
                    _mlDd = Convert.ToDateTime(dr["ML_DD"].ToString());
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qtyMl = Convert.ToDecimal(dr["QTY"].ToString());
            }
            MRPMO _mo = new MRPMO();
            if (_ylId == "T")//���粹��־�򲻻�д�����������
            {
                if (_qtyMl > 0)
                {
                    _mo.UpdateOpnDd(_moNo, _mlDd);
                }
                else
                {
                    _mo.UpdateOpnDdEmpty(_moNo);
                }
                return;
            }
            if (_mlId == "M2")
                _qtyMl = (-1) * _qtyMl;

            _mo.UpdateQtyMl(_moNo, _unit, _qtyMl, _mlDd);
        }
        #endregion

        #region ��д�������ʵ����
        /// <summary>
        /// ��д�������ʵ����
        /// </summary>
        /// <param name="ds"></param>
        private void UpdateMoQtyBody(DataSet ds)
        {
            DataTable _dtBody = null;
            _dtBody = ds.Tables["TF_ML"];
            for (int i = 0; i < _dtBody.Rows.Count; i++)
            {
                if (_dtBody.Rows[i].RowState == DataRowState.Added)
                {
                    UpdateMoQty(_dtBody.Rows[i], false);
                }
                else if (_dtBody.Rows[i].RowState == DataRowState.Modified)
                {
                    UpdateMoQty(_dtBody.Rows[i], true);
                    UpdateMoQty(_dtBody.Rows[i], false);
                }
                else if (_dtBody.Rows[i].RowState == DataRowState.Deleted)
                {
                    UpdateMoQty(_dtBody.Rows[i], true);
                }
            }
        }
        #endregion

        #region ��д�������ʵ����
        /// <summary>
        /// ��д�������ʵ����
        /// </summary>
        /// <param name="dr">������Ϣ��</param>
        /// <param name="isDel"></param>
        private void UpdateMoQty(DataRow dr, bool isDel)
        {
            string _mlId = "";
            string _moNo = "";
            string _unit = "";
            string _estItm = "";
            string _ylId = "F";//���粹��־
            decimal _qty = 0;
            DataSet _dsMl = dr.Table.DataSet;
            if (_dsMl != null && _dsMl.Tables.Contains("MF_ML") && _dsMl.Tables["MF_ML"].Rows.Count > 0)
            {
                if (_dsMl.Tables["MF_ML"].Rows[0].RowState == DataRowState.Added
                    || _dsMl.Tables["MF_ML"].Rows[0].RowState == DataRowState.Unchanged
                    || !isDel)
                {
                    /* �������
                     * 1����ͷ��������������
                     * 2����ͷ���䣬�����޸�
                     */
                    _moNo = _dsMl.Tables["MF_ML"].Rows[0]["MO_NO"].ToString();
                    _ylId = _dsMl.Tables["MF_ML"].Rows[0]["YL_ID"].ToString();
                }
                else
                {
                    /* �������                     
                     * 1����ͷ���޸��粹��־�������޸�                     
                     * 2����ͷ�޸��粹��־�������޸�
                     * 3����ͷ�޸��粹��־����������
                     * 4����ͷ���޸��粹��־����������
                     * 5��ɾ������
                     */
                    if (isDel)
                    {
                        _moNo = _dsMl.Tables["MF_ML"].Rows[0]["MO_NO", DataRowVersion.Original].ToString();
                        _ylId = _dsMl.Tables["MF_ML"].Rows[0]["YL_ID", DataRowVersion.Original].ToString();
                    }
                }
            }
            if (isDel)
            {
                if (dr["MO_NO", DataRowVersion.Original].ToString().Length > 0)
                {
                    _moNo = dr["MO_NO", DataRowVersion.Original].ToString();
                }
                _mlId = dr["MLID", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    _qty = (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                _estItm = dr["EST_ITM", DataRowVersion.Original].ToString();

            }
            else
            {
                if (dr["MO_NO"].ToString().Length > 0)
                {
                    _moNo = dr["MO_NO"].ToString();
                }
                _mlId = dr["MLID"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qty = Convert.ToDecimal(dr["QTY"].ToString());
                _estItm = dr["EST_ITM"].ToString();
            }
            if (_mlId == "M2" || _mlId == "M5")
                _qty = (-1) * _qty;


            //����MO��������MO��Դ��TW��
            string twNo = GetStrDrValue(dr, "TZ_NO", isDel);
            if (!string.IsNullOrEmpty(_moNo) && string.IsNullOrEmpty(twNo))
            {
                MRPMO _mo = new MRPMO();
                if (!string.IsNullOrEmpty(_estItm))
                {
                    if (_ylId == "T")//���粹��־
                    {
                        _mo.UpdateQtyBl(_moNo, Convert.ToInt32(_estItm), _unit, _qty);
                    }
                    else
                    {
                        _mo.UpdateQty(_moNo, Convert.ToInt32(_estItm), _unit, _qty);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(twNo))
            {
                string prdNo = GetStrDrValue(dr, "PRD_NO", isDel);
                DbMRPML _db = new DbMRPML(Comp.Conn_DB);
                if (!string.IsNullOrEmpty(_estItm))
                {
                    #region ���� �����Ӧ�û�д��ԭ��������
                    Hashtable _ht = new Hashtable();
                    _ht["OsID"] = "TW";
                    _ht["OsNO"] = twNo;
                    _ht["KeyItm"] = _estItm;

                    _ht["TableName"] = "TF_TW";
                    _ht["IdName"] = "";
                    _ht["NoName"] = "TW_NO";
                    _ht["ItmName"] = "EST_ITM";
                    _qty = INVCommon.GetRtnQty(prdNo, _qty, Convert.ToInt16(_unit), _ht);
                    #endregion

                    if (_ylId == "T")// QTY_BL
                    {
                        _db.UpdateOS("TW", twNo, _estItm, _qty, "QTY_BL");
                    }
                    else
                    {
                        _db.UpdateOS("TW", twNo, _estItm, _qty, "QTY_RTN");
                        //�����д�ѷ���- 
                        MRPBE _be = new MRPBE();
                        SunlikeDataSet _dsBE = _be.GetData("", "", twNo);
                        if (_dsBE.Tables.Contains("TF_TW") && _dsBE.Tables["TF_TW"].Rows.Count > 0)
                        {
                            DataRow[] _drs = _dsBE.Tables["TF_TW"].Select("EST_ITM='" + _estItm + "'");
                            if (_drs.Length > 0)
                            {

                                decimal qty = Convert2Decimal(_drs[0]["QTY"]);
                                decimal qtyRtn = Convert2Decimal(_drs[0]["QTY_RTN"]);
                                decimal curOnRsv = qty < qtyRtn ? qty : qtyRtn;//��Ҫ��δ����ֵ
                                decimal origOnRsv = qty < (qtyRtn - _qty) ? qty : (qtyRtn - _qty);//���ݸ���ǰ��δ����ֵ
                                decimal _qtyOnRsv = origOnRsv - curOnRsv;

                                string _prdNo = GetStrDrValue(_drs[0], "PRD_NO");
                                string _prdMark = GetStrDrValue(_drs[0], "PRD_MARK");
                                string _wh = GetStrDrValue(_drs[0], "WH");
                                _unit = GetStrDrValue(_drs[0], "UNIT");
                                string _batNo = GetStrDrValue(_drs[0], "BAT_NO");
                                string _validDd = GetStrDrValue(_drs[0], "VALID_DD");

                                #region �޸Ŀ��
                                Prdt _prdt = new Prdt();
                                WH wh = new WH();
                                if (String.IsNullOrEmpty(_batNo))//������
                                {
                                    wh.UpdateQty(_prdNo, _prdMark, _wh, _unit, WH.QtyTypes.QTY_ON_RSV, _qtyOnRsv);
                                }
                                else//���ſع�
                                {
                                    SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _wh);
                                    _ht = new Hashtable();
                                    _ht[WH.QtyTypes.QTY_ON_RSV] = _qtyOnRsv;
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
                        }
                    }
                }
            }
        }
        #endregion

        #region ��������������
        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="moNo"></param>
        private void UpdateMoOpnDdEmpty(string moNo)
        {
            if (string.IsNullOrEmpty(moNo))
                return;
            MRPMO _mo = new MRPMO();
            _mo.UpdateOpnDdEmpty(moNo);
        }
        #endregion

        #region ȡ���������׷�����
        /// <summary>
        /// ȡ���������׷����Σ��÷���Ҫȡ������е�EST_ITM���Ա���ŵ�beforeupdate�У�
        /// </summary>
        /// <param name="dr">����������</param>
        /// <param name="isDel">�Ƿ�ɾ��</param>
        /// <returns></returns>
        private int UpdateMoEstItm(DataRow dr, bool isDel)
        {
            MRPMO _mo = new MRPMO();
            string _moNo = "";
            string _prdNo = "";
            string _prdName = "";
            string _prdMark = "";
            string _useInNo = "";
            string _whNo = "";
            string _unit = "";
            DataSet _dsMl = dr.Table.DataSet;
            if (isDel)
            {
                //if (_dsMl != null && _dsMl.Tables.Contains("MF_ML") && _dsMl.Tables["MF_ML"].Rows.Count > 0)
                //{
                //    _moNo = _dsMl.Tables["MF_ML"].Rows[0]["MO_NO", DataRowVersion.Original].ToString();
                //}
                _moNo = dr["MO_NO"].ToString();
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdName = dr["PRD_NAME", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                _useInNo = dr["USEIN_NO", DataRowVersion.Original].ToString();
            }
            else
            {
                //if (_dsMl != null && _dsMl.Tables.Contains("MF_ML") && _dsMl.Tables["MF_ML"].Rows.Count > 0)
                //{
                //    _moNo = _dsMl.Tables["MF_ML"].Rows[0]["MO_NO"].ToString();
                //}
                _moNo = dr["MO_NO"].ToString();
                _prdNo = dr["PRD_NO"].ToString();
                _prdName = dr["PRD_NAME"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _whNo = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                _useInNo = dr["USEIN_NO"].ToString();
            }
            return _mo.UpdateEstItm(_moNo, _prdNo, _prdName, _prdMark, _whNo, _useInNo, _unit);
        }
        #endregion

        #region �޸Ŀ������
        /// <summary>
        /// ���޸Ŀ������
        /// </summary>
        /// <param name="dr">������Ϣ��</param>
        /// <param name="isDel"></param>
        private void UpdateWhQty(DataRow dr, bool isDel)
        {
            string _mlId = "";
            string _batNo = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _validDd = "";
            string _unit = "";
            decimal _qty = 0;
            decimal _cst = 0;

            if (isDel)
            {
                _mlId = dr["MLID", DataRowVersion.Original].ToString();
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    _qty += (-1) * Convert.ToDecimal(dr["QTY", DataRowVersion.Original].ToString());
                if (!string.IsNullOrEmpty(dr["CST", DataRowVersion.Original].ToString()))
                    _cst += (-1) * Convert.ToDecimal(dr["CST", DataRowVersion.Original].ToString());
            }
            else
            {
                _mlId = dr["MLID"].ToString();
                _prdNo = dr["PRD_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _batNo = dr["BAT_NO"].ToString();
                _whNo = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qty += Convert.ToDecimal(dr["QTY"].ToString());
                if (!string.IsNullOrEmpty(dr["CST"].ToString()))
                    _cst += Convert.ToDecimal(dr["CST"].ToString());
            }
            if (_mlId == "M2" || _mlId == "M5")
            {
                _qty = (-1) * _qty;
                _cst = (-1) * _cst;
            }
            WH _wh = new WH();
            System.Collections.Hashtable _fields = new System.Collections.Hashtable();

            if (string.IsNullOrEmpty(_batNo))
            {
                _fields[WH.QtyTypes.QTY] = (-1) * _qty;
                _fields[WH.QtyTypes.AMT_CST] = (-1) * _cst;
                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _fields);
            }
            else
            {
                _fields[WH.QtyTypes.QTY_OUT] = (-1) * _qty;
                _fields[WH.QtyTypes.CST] = (-1) * _cst;
                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _fields);
            }
        }

        #endregion

        #region ǿ���˻�ɾ�����
        /// <summary>
        /// ǿ���˻�ɾ�����
        /// </summary>
        /// <param name="moNo">�����</param>
        /// <param name="estItm"></param>
        /// <returns></returns>
        private bool chkRtnDel(string moNo, string estItm)
        {
            if (string.IsNullOrEmpty(moNo))
                return true;
            if (string.IsNullOrEmpty(estItm))
                return true;
            bool _result = true;
            MRPMO _mo = new MRPMO();
            SunlikeDataSet _dsMo = _mo.GetUpdateData(moNo, false);
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables["MF_MO"].Rows.Count > 0)
            {
                DataRow[] _drSel = _dsMo.Tables["TF_MO"].Select("MO_NO='" + moNo + "' AND EST_ITM=" + estItm + " AND ISNULL(CHK_RTN,'F') = 'T'");
                //����Ѿ��᰸����ǿ���˻���������ɾ��
                if (_drSel != null && _drSel.Length > 0 && string.Compare("T", _dsMo.Tables["MF_MO"].Rows[0]["CLOSE_ID"].ToString()) == 0)
                {
                    _result = false;
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
            DbMRPML _mo = new DbMRPML(Comp.Conn_DB);
            return _mo.UpdateChkMan(bilId, bilNo, chkMan, clsDd);
        }
        #endregion

        #region �޸�ƾ֤
        /// <summary>
        /// ����ƾ֤����
        /// </summary>
        /// <param name="dr">MF_PSS��������</param>
        /// <param name="statementType"></param>
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
            if (statementType == StatementType.Update)
            {
                DrpVoh _voh = new DrpVoh();
                string _mlId = dr["MLID"].ToString();
                if (this._reBuildVohNo)
                {
                    if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_updUsr = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("ML", _mlId) == 0 || string.Compare("M2", _mlId) == 0 || string.Compare("M3", _mlId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.ML;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetUpdateData("", _mlId, dr["ML_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _mlId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("ML", _mlId) == 0 || string.Compare("M2", _mlId) == 0 || string.Compare("M3", _mlId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.ML;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetUpdateData("", _mlId, dr["ML_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _mlId, out _vohNoError);
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
            else if (statementType == StatementType.Insert)
            {
                string _mlId = dr["MLID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("ML", _mlId) == 0 || string.Compare("M2", _mlId) == 0 || string.Compare("M3", _mlId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.ML;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _mlId, out _vohNoError);
                    _vohNo = dr["VOH_NO"].ToString();
                }
            }
            else if (statementType == StatementType.Delete)
            {
                if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
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
        #endregion

        #region ����������ƾ֤����
        /// <summary>
        /// ����������ƾ֤����
        /// </summary>
        /// <param name="mlId"></param>
        /// <param name="mlNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string mlId, string mlNo, string vohNo)
        {
            DbMRPML _ml = new DbMRPML(Comp.Conn_DB);
            _ml.UpdateVohNo(mlId, mlNo, vohNo);
        }
        #endregion

        #region ȡ�����ϵ������������
        private Dictionary<string, string> GetBodyMoNo(DataSet dsMl)
        {
            if (dsMl == null)
                return null;
            Dictionary<string, string> _result = new Dictionary<string, string>();
            if (dsMl.Tables["TF_ML"].Rows.Count > 0)
            {
                foreach (DataRow dr in dsMl.Tables["TF_ML"].Rows)
                {
                    string _moNo = "";
                    if (dr.RowState == DataRowState.Deleted)
                        _moNo = dr["MO_NO", DataRowVersion.Original].ToString();
                    else
                        _moNo = dr["MO_NO"].ToString();
                    if (!string.IsNullOrEmpty(_moNo))
                    {
                        if (!_result.ContainsKey(_moNo))
                        {
                            _result[_moNo] = "T";
                        }
                    }
                }
            }
            return _result;

        }
        #endregion

        #region �Ƿ�������ϵ����
        /// <summary>
        /// �Ƿ�������ϵ����
        /// </summary>
        /// <param name="moNo">���</param>
        /// <returns></returns>
        public bool IsExistsForMo(string moNo)
        {
            DbMRPML _ml = new DbMRPML(Comp.Conn_DB);
            return _ml.IsExistsForMo(moNo);
        }
        #endregion
        #endregion

        #region function && ��дԭ��

        #region MF_TW QTY_ML/QTY_ML_UNSH�ֶ�
        private void UpdateTWQtyML(DataRow _dr)
        {   //��д���ⵥ�ı�ͷ��������
            if (_dr != null)
            {
                string bilId = GetStrDrValue(_dr, "BIL_ID");
                string bilNo = GetStrDrValue(_dr, "BIL_NO");
                string bilNo1 = _dr.RowState == DataRowState.Modified ? GetStrDrValue(_dr, "BIL_NO", true) : "";//ԭʼֵ;
                if (string.Compare(bilId, "TW") == 0 && !string.IsNullOrEmpty(bilNo))
                {
                    DbMRPML _db = new DbMRPML(Comp.Conn_DB);
                    if (!string.IsNullOrEmpty(bilNo))
                        _db.UpdateTWQtyML(bilNo);
                    if (!string.IsNullOrEmpty(bilNo1))
                        _db.UpdateTWQtyML(bilNo1);
                }
            }
        }
        #endregion

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

        private decimal Convert2Decimal(object obj)
        {
            decimal _d = 0;
            if (!decimal.TryParse(Convert.ToString(obj), out _d))
                _d = 0;
            return _d;
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
            SunlikeDataSet _dsMl = this.GetUpdateData(null, bil_id, bil_no, false);
            DataTable _dtHead = _dsMl.Tables["MF_ML"];
            DataTable _dtBody = _dsMl.Tables["TF_ML"];
            try
            {
                if (_dtHead.Rows.Count > 0)
                {

                    //����ƾ֤
                    string _vohNo = this.UpdateVohNo(_dtHead.Rows[0], StatementType.Insert);
                    this.UpdateVohNo(bil_id, bil_no, _vohNo);
                }
                foreach (DataRow _dr in _dtBody.Rows)
                {
                    this.UpdateMoQty(_dr, false);
                    this.UpdateWhQty(_dr, false);
                }
                this.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
                this._updateMoNo = GetBodyMoNo(_dsMl);
                this.UpdateMoMrpQty();
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
            SunlikeDataSet _dsMl = this.GetUpdateData(null, bil_id, bil_no, false);
            string _errorMsg = this.SetCanModify(_dsMl, false);
            if (_dsMl.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
            {
                if (_errorMsg.Length > 0)
                {
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT;RCID=" + _errorMsg;
                }
                else
                {
                    return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";
                }
                if (_dsMl.ExtendedProperties.ContainsKey("BILL_VOH_AC_CONTROL"))
                {
                    return "RCID=INV.HINT.BILL_VOH_CONTRL3,PARAM=" + _dsMl.ExtendedProperties["VOH_AC_NO"].ToString();
                }
            }
            DataTable _dtHead = _dsMl.Tables["MF_ML"];
            DataTable _dtBody = _dsMl.Tables["TF_ML"];
            try
            {
                if (_dtHead.Rows.Count > 0)
                {
                    _dtHead.Rows[0].Delete();
                    UpdateMoOpnDdEmpty(_dtHead.Rows[0]["MO_NO", DataRowVersion.Original].ToString());

                    //����ƾ֤
                    this.UpdateVohNo(_dtHead.Rows[0], StatementType.Delete);
                    this.UpdateVohNo(bil_id, bil_no, "");
                }
                foreach (DataRow _dr in _dtBody.Rows)
                {
                    this.UpdateMoQty(_dr, true);
                    this.UpdateWhQty(_dr, true);
                }
                this.UpdateChkMan(bil_id, bil_no, "", Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat)));
                this._updateMoNo = GetBodyMoNo(_dsMl);
                this.UpdateMoMrpQty();
            }
            catch (Exception _ex)
            {
                _errmsg = _ex.Message.ToString();
            }
            return _errmsg;
        }

        #endregion
    }
}
