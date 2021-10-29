using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
namespace Sunlike.Business
{
    public class MRPBom : BizObject, IAuditing
    {
        private string _usr = "";
        private bool _isRunAuditing;

        #region Structure
        /// <summary>
        /// ��׼�䷽
        /// </summary>
        public MRPBom()
        {
        }
        #endregion

        #region GetData


        /// <summary>
        /// ȡ���䷽����
        /// </summary>
        /// <param name="usr">�Ƶ���</param>
        /// <param name="bomNo">BOM�䷽</param>
        /// <param name="onlyFillSchema">�Ƿ�ֻ��ȡSchema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string bomNo, bool onlyFillSchema)
        {
            DbMRPBom _bom = new DbMRPBom(Comp.Conn_DB);
            SunlikeDataSet _ds = _bom.GetData(bomNo, onlyFillSchema);

            if (!string.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            if (_ds.Tables.Contains("TF_BOM"))
            {
                DataTable _dtBody = _ds.Tables["TF_BOM"];
                //�����Ʒ���
                _dtBody.Columns.Add("PRD_SPC");
                _dtBody.Columns.Add("UNIT_DP");
                Prdt _prdt = new Prdt();
                foreach (DataRow _dr in _dtBody.Rows)
                {
                    DataTable _dtPrdt = _prdt.GetPrdt(_dr["PRD_NO"].ToString());
                    _dr["PRD_SPC"] = _dtPrdt.Rows[0]["SPC"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(usr))
            {
                this.SetCanModify(_ds, usr, true);
            }
            return _ds;
        }
        /// <summary>
        /// ȡ���䷽����
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string bomNo)
        {
            return this.GetData(null, bomNo, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bom_no"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string bom_no)
        {
            DbMRPBom dbBom = new DbMRPBom(Comp.Conn_DB);
            return dbBom.GetDataBody(bom_no);
        }
        /// <summary>
        /// ���ݻ�Ʒ����ȡ��׼�䷽
        /// </summary>
        /// <param name="prdNo">��Ʒ����</param>
        /// <param name="isTop1">�Ƿ�ֻȡTOP 1</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByPrdNo(string prdNo, bool isTop1)
        {
            DbMRPBom _bom = new DbMRPBom(Comp.Conn_DB);
            return _bom.GetDataByPrdNo(prdNo, isTop1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByIDNO(string idNo)
        {
            DbMRPBom dbBom = new DbMRPBom(Comp.Conn_DB);
            return dbBom.GetDataByIDNO(idNo);
        }

        /// <summary>
        /// ͨ����Ʒ����ȡ���䷽����
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromMF(string prdNo)
        {
            DbMRPBom dbBom = new DbMRPBom(Comp.Conn_DB);
            return dbBom.GetDataFromMF(prdNo);
        }

        /// <summary>
        /// ͨ����Ʒ����ȡ���䷽����
        /// </summary>
        /// <param name="prdNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataFromTF(string prdNo)
        {
            DbMRPBom dbBom = new DbMRPBom(Comp.Conn_DB);
            return dbBom.GetDataFromTF(prdNo);
        }



        #endregion

        #region Updata
        /// <summary>
        /// Updata
        /// </summary>
        /// <param name="changedDs"></param>
        /// <param name="bubbleException"></param>
        /// <returns></returns>
        public DataTable UpdateData(SunlikeDataSet changeDs, bool bubbleException)
        {
            DataTable _dtErr = null;
            Hashtable _ht = new Hashtable();
            _ht["MF_BOM"] = "BOM_NO,NAME,PRD_NO,PRD_MARK,PF_NO,WH_NO,PRD_KND,UNIT, QTY,QTY1,CST_MAKE,CST_PRD,CST_MAN,CST_OUT,USED_TIME,CST,USR_NO,TREE_STRU, DEP,PHOTO_BOM,EC_NO,VALID_DD,END_DD,REM,USR,CHK_MAN,PRT_SW,CPY_SW,CLS_DATE, MOB_ID,LOCK_MAN,SEB_NO,MOD_NO,TIME_CNT,PRT_USR,DEP_INC,SPC,SYS_DATE,BZ_KND ";
            _ht["TF_BOM"] = "BOM_NO,ITM,PRD_NO,PRD_MARK,ID_NO,NAME,WH_NO,BOM_ID,UNIT,QTY,QTY1,LOS_RTO,CST,PRD_NO_UP,ID_NO_UP,EXP_ID,PRD_NO_CHG,REM,START_DD,END_DD,ZC_NO, TW_ID,USEIN_NO,QTY_BAS,PZ_ID,COMPOSE_IDNO,UP_STD,UP_TAX,CUS_NO,RTO_TAX,EXC_RTO, CUR_ID,IS_SO_RES,CHG_RTO,PRE_ITM,BL_RTO,CUS_NO2,PRD_NO_OLD,CL_RTO,RTO_PEI,PEI_RTO ";
            //�ж��Ƿ����������
            DataRow _dr = changeDs.Tables["MF_BOM"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
                _usr = _dr["USR", DataRowVersion.Original].ToString();
            else
                _usr = _dr["USR"].ToString();
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
            //_isRunAuditing = _auditing.IsRunAuditing("ZZ", _usr, _bilType, _mobID);


            this.UpdateDataSet(changeDs, _ht);
            //�жϵ����ܷ��޸�
            if (!changeDs.HasErrors)
                this.SetCanModify(changeDs, _usr, true);
            else
            {
                _dtErr = GetAllErrors(changeDs);
                if (bubbleException)
                    throw new System.Exception(BizObject.GetErrorsString(changeDs));
            }
            return _dtErr;
        }

        /// <summary>
        /// ���浥��֮ǰ�Ķ���
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            #region SAVE����
            if (tableName == "MF_BOM")
            {
                if (statementType == StatementType.Insert)
                {
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    ChkDrData(dr, ref status);
                    //����BOM_NO
                    string _bomNo = dr["PRD_NO"].ToString() + "->" + dr["PF_NO"].ToString();
                    //if (string.IsNullOrEmpty(dr["BOM_NO"].ToString()))
                    dr["BOM_NO"] = _bomNo;
                    if (string.IsNullOrEmpty(dr["UNIT"].ToString()))
                    {
                        dr["UNIT"] = "1";
                    }
                    //�ж��䷽�����Ƿ����
                    if (IsExists(dr["BOM_NO"].ToString()))
                    {
                        dr.SetColumnError("BOM_NO", "RCID=MTN.HINT.BOMNOEXISTS");//"���䷽�Ѿ�����"
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //��д�ϲ�ڵ�
                    ModifyIDNO(_bomNo, dr["PRD_NO"].ToString(), "ADD");
                }
                else if (statementType == StatementType.Update)
                {
                    ChkDrData(dr, ref status);
                }
                else if (statementType == StatementType.Delete)
                {
                    ModifyIDNO(dr["BOM_NO", DataRowVersion.Original].ToString(), dr["PRD_NO", DataRowVersion.Original].ToString(), "DEL");
                }
                //#region ��˹���
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = DateTime.Now;
                //    _aps.BIL_ID = "ZZ";
                //    _aps.BIL_NO = dr["BOM_NO"].ToString();
                //    _aps.BIL_TYPE = "FX";
                //    _aps.CUS_NO = "";
                //    _aps.DEP = "";
                //    _aps.SAL_NO = "";
                //    _aps.USR = _usr;
                //    _aps.MOB_ID = ""; //�¼ӵĲ��֣���Ӧ���ģ��
                //}
                //else
                //    _aps = new AudParamStruct("ZZ", Convert.ToString(dr["BOM_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
            #endregion

        }

        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables["MF_BOM"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "ZZ");
            //}
            //#endregion


            base.BeforeDsSave(ds);
        }

        #endregion
        #region �䷽�Ƿ����
        /// <summary>
        /// �䷽�Ƿ����
        /// </summary>
        /// <param name="bomNo">�䷽��</param>
        /// <returns></returns>
        public bool IsExists(string bomNo)
        {
            bool _isExists = false;
            SunlikeDataSet _ds = this.GetData(bomNo);
            if (_ds.Tables["MF_BOM"].Rows.Count > 0)
                _isExists = true;
            return _isExists;
        }
        #endregion

        /// <summary>
        /// �ж��䷽���Ƿ���� FOR ID_NO
        /// </summary>
        /// <param name="bomNo"></param>
        /// <returns></returns>
        public bool ChkExistsForIdNo(string bomNo)
        {
            SunlikeDataSet _ds = GetDataByIDNO(bomNo);
            if (_ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        #region �Ƿ����޸�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changedDS"></param>
        /// <param name="usr"></param>
        /// <param name="isCheckAuditing"></param>
        private void SetCanModify(SunlikeDataSet changedDS, string usr, bool isCheckAuditing)
        {
            if (changedDS.Tables["MF_BOM"].Rows.Count > 0)
            {
                DataTable _dtHead = changedDS.Tables["MF_BOM"];
                // ����Ȩ�޿ع�
                if (usr != null)
                {
                    if (_dtHead.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtHead.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtHead.Rows[0]["USR"].ToString();
                        Hashtable _billRight = Users.GetBillRight("ZW", usr, _bill_Dep, _bill_Usr);
                        changedDS.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDS.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDS.ExtendedProperties["PRN"] = _billRight["PRN"];
                    }
                }
                // �Ƿ�����޸�
                bool _canModify = true;
                Auditing _auditing = new Auditing();
                if (isCheckAuditing)
                    _canModify = _canModify && !_auditing.GetIfEnterAuditing("ZW", changedDS.Tables["MF_BOM"].Rows[0]["BOM_NO"].ToString());
                changedDS.ExtendedProperties["CAN_MODIFY"] = _canModify.ToString().Substring(0, 1).ToUpper();
            }
        }
        #endregion

        #region ȡ�õ�λBOM�䷽
        /// <summary>
        /// ȡ�õ�λBOM�䷽
        /// </summary>
        /// <param name="headName">�䷽��ͷ����</param>
        /// <param name="bodyName">�䷽��������</param>
        /// <param name="unitStd">��ͷ��Ʒת����λ</param>
        /// <param name="bomInfo">�䷽��Ϣ</param>
        public DataSet GetStdBom(string headName, string bodyName, string unitStd, DataSet bomInfo)
        {
            if (bomInfo != null && bomInfo.Tables.Count > 0 && bomInfo.Tables[0].Rows.Count > 0)
            {
                decimal _qtyBase = 1;       //����
                decimal _qtyHead = 1;       //��ͷ����
                decimal _upStd = 0;         //��λ�ɱ�
                string _unitHead = "1";

                if (bomInfo.Tables[headName].Rows.Count > 0)
                {
                    //ȡ�䷽������
                    if (!string.IsNullOrEmpty(bomInfo.Tables[headName].Rows[0]["QTY"].ToString()))
                        _qtyHead = Convert.ToDecimal(bomInfo.Tables[headName].Rows[0]["QTY"].ToString());
                    //ȡ���䷽��λ
                    _unitHead = bomInfo.Tables[headName].Rows[0]["UNIT"].ToString();
                    //ת����λ
                    Prdt _prdt = new Prdt();
                    _qtyHead = _prdt.GetUnitQty(bomInfo.Tables[headName].Rows[0]["PRD_NO"].ToString(), _unitHead, _qtyHead, unitStd);
                    bomInfo.Tables[headName].Rows[0]["UNIT"] = unitStd;
                }

                foreach (DataRow _drBom in bomInfo.Tables[bodyName].Rows)
                {
                    _qtyBase = 1;
                    _qtyHead = 1;
                    //����
                    if (_drBom.Table.Columns.Contains("QTY_BAS"))
                    {
                        if (!string.IsNullOrEmpty(_drBom["QTY_BAS"].ToString()))
                            _qtyBase = Convert.ToDecimal(_drBom["QTY_BAS"].ToString());
                    }
                    //��λ�ɱ�
                    if (_drBom.Table.Columns.Contains("CST_STD_UNIT"))
                    {
                        if (!string.IsNullOrEmpty(_drBom["CST_STD_UNIT"].ToString()))
                            _upStd = Convert.ToDecimal(_drBom["CST_STD_UNIT"].ToString());
                    }
                    //ά���䷽����ĵ�λ����/����/��ͷά������
                    _drBom["QTY"] = Convert.ToDecimal(_drBom["QTY"].ToString()) / _qtyBase / _qtyHead;
                    //����ɱ�
                    if (_drBom.Table.Columns.Contains("CST"))
                    {
                        if (!string.IsNullOrEmpty(_drBom["QTY"].ToString()))
                            _drBom["CST"] = _upStd * Convert.ToDecimal(_drBom["QTY"]);
                    }
                }
                bomInfo.AcceptChanges();
            }
            if (bomInfo != null)
                return bomInfo.Copy();
            else
                return null;
        }
        #endregion

        private void ChkDrData(DataRow dr, ref UpdateStatus status)
        {
            #region ���
            //��Ʒ���(����)
            Prdt SunlikePrdt = new Prdt();
            string _prd_no = dr["PRD_NO"].ToString();
            if (!SunlikePrdt.IsExist(_usr, _prd_no))
            {
                dr.SetColumnError("PRD_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prd_no);
                status = UpdateStatus.SkipAllRemainingRows;
            }
            //�ֿ�(����)
            string _wh = dr["WH_NO"].ToString();
            if (!string.IsNullOrEmpty(_wh))
            {
                WH SunlikeWH = new WH();
                if (!SunlikeWH.IsExist(_usr, _wh))
                {
                    dr.SetColumnError("WH",/*�ֿ����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _wh);
                    status = UpdateStatus.SkipAllRemainingRows;
                }
            }
            //PMARK
            string _mark = dr["PRD_MARK"].ToString();
            int _prdMod = SunlikePrdt.CheckPrdtMod(_prd_no, _mark);
            if (_prdMod == 1)
            {
                dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _mark);//��Ʒ����[{0}]������
                status = UpdateStatus.SkipAllRemainingRows;
            }
            else if (_prdMod == 2)
            {
                PrdMark _prd_Mark = new PrdMark();
                if (_prd_Mark.RunByPMark(_usr))
                {
                    string[] _prd_markAry = _prd_Mark.BreakPrdMark(_mark);
                    DataTable _markTable = _prd_Mark.GetSplitData("");
                    for (int i = 0; i < _markTable.Rows.Count; i++)
                    {
                        string _markName = _markTable.Rows[i]["FLDNAME"].ToString();
                        if (!_prd_Mark.IsExist(_markName, dr["PRD_NO"].ToString(), _prd_markAry[i]))
                        {
                            dr.SetColumnError("PRD_MARK",/*��Ʒ����[{0}]������,����*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _mark);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bomNo"></param>
        /// <param name="prdNo"></param>
        /// <param name="operType"></param>
        private void ModifyIDNO(string bomNo, string prdNo, string operType)
        {
            SunlikeDataSet _ds = GetDataFromTF(prdNo);
            if (operType == "ADD")
            {
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    if (string.IsNullOrEmpty(_dr["ID_NO"].ToString()))
                    {
                        _dr["ID_NO"] = bomNo;
                    }
                }
            }
            else
            {
                DataRow[] _drAry = _ds.Tables[0].Select(" ID_NO = '" + bomNo + "' AND BOM_NO <> '" + bomNo + "'");
                foreach (DataRow _dr in _ds.Tables[0].Rows)
                {
                    if (_dr["ID_NO"].ToString() == bomNo)
                    {
                        _dr["ID_NO"] = DBNull.Value;
                    }
                }
            }
            if (_ds.GetChanges() != null)
            {
                Hashtable _ht = new Hashtable();
                _ht["TF_BOM"] = "BOM_NO,ITM,PRD_NO,PRD_MARK,ID_NO,WH_NO,UNIT,QTY,REM";
                this.UpdateDataSet(_ds.GetChanges());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataMFByCondition(string sqlWhere)
        {
            DbMRPBom dbBom = new DbMRPBom(Comp.Conn_DB);
            return dbBom.GetDataMFByCondition(sqlWhere);
        }

        public void GetBomDetail(DataTable _dt, string bomNo, int level)
        {
            if (_dt.Columns.Count == 0)
            {
                _dt.Columns.Add("PRD_NO", typeof(string));
                _dt.Columns.Add("PRD_NAME", typeof(string));
                _dt.Columns.Add("PRD_MARK", typeof(string));
                _dt.Columns.Add("BOM_NO", typeof(string));
                _dt.Columns.Add("WH_NO", typeof(string));
                _dt.Columns.Add("KND", typeof(string));
                _dt.Columns.Add("UNIT", typeof(string));
                _dt.Columns.Add("UP_UNIT", typeof(string));
                _dt.Columns.Add("BOM_QTY", typeof(decimal));//��׼����
                _dt.Columns.Add("BOM_QTY1", typeof(decimal));//��׼����(��)
                _dt.Columns.Add("UP_NO", typeof(string));//ĸ������
                _dt.Columns.Add("UP_QTY", typeof(decimal));//ĸ������
                _dt.Columns.Add("UP_QTY1", typeof(decimal));//ĸ������(��)
                _dt.Columns.Add("LEVEL", typeof(int));
                _dt.Columns.Add("IS_CHILD", typeof(string));//�ӽ��
            }
            DbMRPBom dbBom = new DbMRPBom(Comp.Conn_DB);
            SunlikeDataSet _ds = dbBom.GetDataForDetail(bomNo);

            foreach (DataRow _dr in _ds.Tables[0].Rows)
            {
                DataRow _drNew = _dt.NewRow();
                _drNew["PRD_NO"] = _dr["PRD_NO"];
                _drNew["BOM_NO"] = _dr["BOM_NO"];
                _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                _drNew["KND"] = _dr["KND"];
                _drNew["WH_NO"] = _dr["WH_NO"];
                _drNew["UNIT"] = _dr["UNIT"];
                _drNew["UP_UNIT"] = _dr["UP_UNIT"];
                if (!string.IsNullOrEmpty(_dr["QTY"].ToString()))
                {
                    _drNew["BOM_QTY"] = _dr["QTY"];
                }
                else
                {
                    _drNew["BOM_QTY"] = 0;
                }

                if (!string.IsNullOrEmpty(_dr["QTY1"].ToString()))
                {
                    _drNew["BOM_QTY1"] = _dr["QTY1"];
                }
                else
                {
                    _drNew["BOM_QTY1"] = 0;
                }
                _drNew["PRD_NAME"] = _dr["NAME"];
                _drNew["UP_NO"] = _dr["UP_NO"];
                _drNew["UP_QTY"] = _dr["UP_QTY"];
                _drNew["UP_QTY1"] = _dr["UP_QTY1"];
                if (level == 0)
                {
                    if (string.IsNullOrEmpty(_drNew["UP_QTY1"].ToString()) || Convert.ToDecimal(_drNew["UP_QTY1"]) == 0)
                    {
                        _drNew["UP_QTY1"] = _drNew["UP_QTY"];
                    }
                }
                _drNew["LEVEL"] = level;
                _dt.Rows.Add(_drNew);

                if (!String.IsNullOrEmpty(_dr["ID_NO"].ToString()))
                {
                    GetBomDetail(_dt, _dr["ID_NO"].ToString(), level + 1);
                }
                else
                    _drNew["IS_CHILD"] = "T";
            }
        }

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
            string _error = String.Empty;
            try
            {
                DbMRPBom _dbMrpBom = new DbMRPBom(Comp.Conn_DB);
                _dbMrpBom.UpdateChkMan(bil_no, chk_man, cls_dd);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
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
            string _error = String.Empty;
            try
            {
                DbMRPBom _dbMrpBom = new DbMRPBom(Comp.Conn_DB);
                _dbMrpBom.UpdateChkMan(bil_no, "", DateTime.Now);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            return _error;
        }

        #endregion

        #region MRP�й� BOM���
        /// <summary>
        ///  ��ȡ��Ʒ��Ӧ��BOM_NO
        /// </summary>
        /// <param name="prdNo">��Ʒ����</param>
        /// <param name="bomTb">BOM����</param>
        /// <param name="isMaxVer">�Ƿ�ȡ�����䷽</param>
        /// <returns>bomNo����""</returns>
        public string GetBomNoByPrdt(string prdNo, string bomTb, bool isMaxVer)
        {
            DbMRPBom _db = new DbMRPBom(Comp.Conn_DB);
            return _db.GetBomNoByPrdt(prdNo, bomTb, isMaxVer);
        }

        /// <summary>
        ///  ˳�δ�MF_BOM->MF_BOM_SO��ȡ��Ʒ��Ӧ��BOM_NO
        /// </summary>
        /// <param name="prdNo">��Ʒ����</param>
        /// <param name="isMaxVer">�Ƿ�ȡ�����䷽</param>
        /// <returns>bomNo����""</returns>
        public string GetBomNoByPrdt(string prdNo, bool isMaxVer)
        {
            string bomNo = GetBomNoByPrdt(prdNo, "MF_BOM", isMaxVer);
            if (string.IsNullOrEmpty(bomNo))
                bomNo = GetBomNoByPrdt(prdNo, "MF_BOM_SO", isMaxVer);
            return bomNo;
        }

        /// <summary>
        /// ��ȡBOM_NO��Ӧ��MF����
        /// </summary>
        /// <param name="bomNo">BOM_NO</param>
        /// <returns></returns>
        public string GetMFbyBom(string bomNo)
        {
            DbMRPBom _db = new DbMRPBom(Comp.Conn_DB);
            string bomMF = "";
            if (_db.IsExistsBom(bomNo, "MF_BOM"))
                bomMF = "MF_BOM";
            if (_db.IsExistsBom(bomNo, "MF_BOM_SO"))
                bomMF = "MF_BOM_SO";
            return bomMF;
        }
        /// <summary>
        /// ��ȡBOM_NO��Ӧ��MF��Ʒ�ɱ�
        /// </summary>
        /// <param name="bomNo">BOM_NO</param>
        /// <returns>CST</returns>
        public string GetMFCstbyBom(string bomNo)
        {
            DbMRPBom _db = new DbMRPBom(Comp.Conn_DB);
            string bomMF = "";
            if (_db.IsExistsBom(bomNo, "MF_BOM"))
                bomMF = "MF_BOM";
            if (_db.IsExistsBom(bomNo, "MF_BOM_SO"))
                bomMF = "MF_BOM_SO";
            DataTable _dt = _db.GetTFBom(bomMF);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                DataRow _dr = _dt.Rows.Find(new object[] { bomNo });
                if (_dr != null && _dt.Columns.Contains("CST"))
                    return Convert.ToString(_dr["CST"]);
            }
            return "";
        }

        /// <summary>
        /// ��ȡBOM_NO��Ӧ��TF_BOM����
        /// </summary>
        /// <param name="bomNo">BOM_NO</param>
        /// <param name="bomMF">MF����</param>
        /// <param name="level">չ������</param>
        /// <returns></returns>
        public SunlikeDataSet GetTFBom(string bomNo, string bomMF, int level)
        {
            if (string.IsNullOrEmpty(bomMF))
            {
                bomMF = GetMFbyBom(bomNo);
            }
            string bomTF = string.Compare(bomMF, "MF_BOM") == 0 ? "TF_BOM" : "TF_BOM_SO";
            DbMRPBom _db = new DbMRPBom(Comp.Conn_DB);
            DataTable _dt = _db.GetTFBom(bomTF);
            _dt.Columns["FACTOR"].ReadOnly = false;
            DataTable _dtNew = _dt.Clone();
            decimal factor = 1 / _db.GetMFQty(bomMF, bomNo);
            ExpandBom(_dt, _dtNew, bomNo, level, factor);
            SunlikeDataSet _ds = new SunlikeDataSet();
            _ds.Merge(_dtNew);
            return _ds;
        }

        private void ExpandBom(DataTable sourceDt, DataTable newDt, string bomNo, int level, Decimal factor)
        {
            if (!string.IsNullOrEmpty(bomNo) && level-- > 0)
            {
                foreach (DataRow _dr in sourceDt.Select("BOM_NO='" + bomNo + "'"))
                {
                    decimal temp = factor * Convert2Decimal(_dr["QTY"]) / Convert2Decimal(_dr["QTY_BAS"]);
                    _dr["FACTOR"] = temp;
                    string idNo = Convert.ToString(_dr["ID_NO"]);
                    if (string.IsNullOrEmpty(idNo) || level == 0)
                        newDt.ImportRow(_dr);
                    ExpandBom(sourceDt, newDt, idNo, level, temp);
                }
            }
        }

        private decimal Convert2Decimal(object obj)
        {
            decimal _d = 0;
            if (!decimal.TryParse(Convert.ToString(obj), out _d))
                _d = 0;
            return _d;
        }

        #endregion
    }
}
