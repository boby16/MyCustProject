/*
 * modify: cjc 090805 
 * ������̵ĸĶ�(���ݱ�����ģ��)
 */

using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using System.Collections;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
    /// <summary>
    /// �ɹ���
    /// </summary>
    public class DRPPO : BizObject, Sunlike.Business.IAuditing, Sunlike.Business.ICloseBill
    {
        #region ����
        private bool _isRunAuditing;
        private string _loginUsr = "";
        private string _osId = "";
        private bool _isNeedaffirm;//�Ƿ���Ҫ�ɹ�ȷ��
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public DRPPO()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region GetData
        /// <summary>
        ///  ȡ�òɹ�����Ϣ(�ɹ�����)
        /// </summary>
        /// <param name="osId">���ݱ�</param>
        /// <param name="osNo">����</param>
        /// <param name="bListData">�г�����</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string osId, string osNo, bool bListData)
        {
            DbDRPPO _po = new DbDRPPO(Comp.Conn_DB);
            CompInfo _compInfo = Comp.GetCompInfo("");
            if (_compInfo.SystemInfo.CHK_DRP1)
            {
                return _po.GetData(osId, osNo, true);
            }
            return _po.GetData(osId, osNo, false);
        }
        /// <summary>
        /// ȡԭ����λ���˻�ʱ��(�ɹ�����)
        /// </summary>
        /// <param name="osId">���ݱ�</param>
        /// <param name="osNo">����</param>
        /// <param name="preItm">׷�����</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string osId, string osNo, int preItm)
        {
            DbDRPPO _po = new DbDRPPO(Comp.Conn_DB);
            return _po.GetData(osId, osNo, preItm);
        }

        /// <summary>
        /// ȡ�òɹ�����Ϣ(�ɹ�����)
        /// </summary>
        /// <param name="osId">���ݱ�</param>
        /// <param name="osNo">����</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string osId, string osNo)
        {
            DbDRPPO _po = new DbDRPPO(Comp.Conn_DB);
            return _po.GetData(osId, osNo, false);
        }
        /// <summary>
        ///	 ȡ�òɹ�����Ϣ
        /// </summary>
        /// <param name="usr">¼����</param>
        /// <param name="osId">�ɹ����ݱ�</param>
        /// <param name="osNo">�ɹ�����</param>
        /// <param name="isSchema">�Ƿ�ȡ�ɹ����ܹ�</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string osId, string osNo, bool isSchema)
        {
            DbDRPPO _po = new DbDRPPO(Comp.Conn_DB);
            SunlikeDataSet _ds = _po.GetDataPO(osId, osNo, isSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //���ӵ���Ȩ��
            if (!isSchema)
            {
                if (usr != null)
                {
                    string _pgm = "DRP" + osId;
                    DataTable _dtMf = _ds.Tables["MF_POS"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["PO_DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
            }
            //���������ֶ�
            if (_ds.Tables.Contains("TF_POS"))
            {
                DataTable _TF_POSTable = _ds.Tables["TF_POS"];
                //�ɹ����м�¼ѯ�۵��ļ۸�
                DataColumn _v_up_qs = new DataColumn("V_UP_QS", Type.GetType("System.Decimal"));
                _TF_POSTable.Columns.Add(_v_up_qs);
                //�ɹ����е�λ��׼�ɱ�
                DataColumn _up_std = new DataColumn("CST_STD_UNIT", Type.GetType("System.Decimal"));
                _TF_POSTable.Columns.Add(_up_std);
                //�ɹ��˻ص��м�¼ԭ�ɹ�������
                DataColumn _v_qty_po = new DataColumn("V_QTY_PO", Type.GetType("System.Decimal"));
                _TF_POSTable.Columns.Add(_v_qty_po);
                //�趨�����estItmΪ�Զ�����				
                if (osId != "PR")
                {
                    _TF_POSTable.Columns["EST_ITM"].AutoIncrement = true;
                    _TF_POSTable.Columns["EST_ITM"].AutoIncrementSeed = _TF_POSTable.Rows.Count > 0 ? Convert.ToInt32(_TF_POSTable.Select("", "EST_ITM desc")[0]["EST_ITM"]) + 1 : 1;
                    _TF_POSTable.Columns["EST_ITM"].AutoIncrementStep = 1;
                    _TF_POSTable.Columns["EST_ITM"].Unique = true;
                }
                _TF_POSTable.Columns.Add("PRD_NO_NO", typeof(System.String));
                foreach (DataRow _TF_POSRow in _TF_POSTable.Rows)
                {
                    _TF_POSRow["PRD_NO_NO"] = _TF_POSRow["PRD_NO"];
                }
                //����λ��׼�ɱ���ֵ
                for (int i = 0; i < _ds.Tables["TF_POS"].Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(_ds.Tables["TF_POS"].Rows[i]["CST_STD"].ToString()) && !String.IsNullOrEmpty(_ds.Tables["TF_POS"].Rows[i]["QTY"].ToString()) && Convert.ToDecimal(_ds.Tables["TF_POS"].Rows[i]["QTY"]) != 0)
                        _ds.Tables["TF_POS"].Rows[i]["CST_STD_UNIT"] = Convert.ToDecimal(_ds.Tables["TF_POS"].Rows[i]["CST_STD"]) / Convert.ToDecimal(_ds.Tables["TF_POS"].Rows[i]["QTY"]);
                    else
                        _ds.Tables["TF_POS"].Rows[i]["CST_STD_UNIT"] = 0;
                }

                _TF_POSTable.Columns.Add("STATUS_JD");
                for (int i = 0; i < _TF_POSTable.Rows.Count; i++)
                {
                    int _poEstItm = 0;
                    string _poOsNo = "";
                    if (osId == "PO")
                    {
                        if (!string.IsNullOrEmpty(_TF_POSTable.Rows[i]["EST_ITM"].ToString()))
                        {
                            _poEstItm = Convert.ToInt16(_TF_POSTable.Rows[i]["EST_ITM"]);
                        }
                        _poOsNo = _TF_POSTable.Rows[i]["OS_NO"].ToString();
                    }
                    else if (osId == "PR")
                    {
                        int _preItm = 0;
                        if (!string.IsNullOrEmpty(_TF_POSTable.Rows[i]["PRE_ITM"].ToString()))
                        {
                            _preItm = Convert.ToInt32(_TF_POSTable.Rows[i]["PRE_ITM"]);
                        }
                        SunlikeDataSet _dsPo = GetBody(_TF_POSTable.Rows[i]["QT_NO"].ToString(), "PO", "PRE_ITM", _preItm, false);
                        if (_dsPo.Tables[0].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_dsPo.Tables[0].Rows[0]["EST_ITM"].ToString()))
                            {
                                _poEstItm = Convert.ToInt16(_dsPo.Tables[0].Rows[0]["EST_ITM"]);
                            }
                            _poOsNo = _dsPo.Tables[0].Rows[0]["OS_NO"].ToString();
                        }

                    }

                    using (SunlikeDataSet _dsjD = GetJDData(_poOsNo, "PO", _poEstItm))
                    {
                        if (_dsjD.Tables[0].Rows.Count > 0)
                        {
                            if (_dsjD.Tables[0].Rows[0]["STATUS_JD"].ToString() == "Y")
                            {
                                _ds.Tables["TF_POS"].Rows[i]["STATUS_JD"] = _dsjD.Tables[0].Rows[0]["STATUS_JD"];
                            }
                        }
                    }
                }
            }
            //ȡ������Ϣ
            if (_ds.Tables.Contains("TF_POS_BOX"))
            {
                DataTable _TF_POS_BOXTable = _ds.Tables["TF_POS_BOX"];
                //�趨������KeyItmΪ�Զ�����
                _TF_POS_BOXTable.Columns["KEY_ITM"].AutoIncrement = true;
                _TF_POS_BOXTable.Columns["KEY_ITM"].AutoIncrementSeed = _TF_POS_BOXTable.Rows.Count > 0 ? Convert.ToInt32(_TF_POS_BOXTable.Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
                _TF_POS_BOXTable.Columns["KEY_ITM"].AutoIncrementStep = 1;
                _TF_POS_BOXTable.Columns["KEY_ITM"].Unique = true;
                _TF_POS_BOXTable.Columns.Add("PRD_NO_NO", typeof(System.String));

                Sunlike.Business.BarBox _bar = new Sunlike.Business.BarBox();
                DataColumn _dc = new DataColumn("CONTENT_DSC", Type.GetType("System.String"));
                _dc.MaxLength = 64 * 1024;
                _TF_POS_BOXTable.Columns.Add(_dc);
                foreach (DataRow _TF_POS_BOXRow in _TF_POS_BOXTable.Rows)
                {
                    _TF_POS_BOXRow["CONTENT_DSC"] = _bar.GetBar_BoxDsc(_TF_POS_BOXRow["PRD_NO"].ToString(), _TF_POS_BOXRow["CONTENT"].ToString());
                    _TF_POS_BOXRow["PRD_NO_NO"] = _TF_POS_BOXRow["PRD_NO"];
                }

            }
            //��λ������
            _ds.Tables["TF_POS"].Columns.Add("UNIT_DP", typeof(String)).MaxLength = 8;
            SetCanModify(_ds, true);
            _ds.AcceptChanges();
            return _ds;
        }
        /// <summary>
        /// ȡ�ñ�������
        /// </summary>
        /// <param name="osId">���ݱ�</param>
        /// <param name="osNo">����</param>
        /// <param name="itm">���</param>
        /// <param name="itmColumnName"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string osId, string osNo, string itmColumnName, int itm, bool isPrimaryUnit)
        {
            Sunlike.Business.Data.DbDRPPO _dbDrpPo = new DbDRPPO(Comp.Conn_DB);
            return _dbDrpPo.GetBody(osId, osNo, itmColumnName, itm, isPrimaryUnit);
        }
        #endregion

        #region �õ���ȷ�ϲɹ���
        /// <summary>
        /// �õ���ȷ�ϲɹ�����Ŀ
        /// </summary>
        /// <param name="usr">ȷ����</param>
        /// <returns></returns>
        public int GetScmNum(string usr)
        {
            DbDRPPO _po = new DbDRPPO(Comp.Conn_DB);
            return _po.GetScmNum(usr);
        }
        #endregion

        #region ȷ��/��ȷ�ϲɹ���
        /// <summary>
        /// ȷ��/��ȷ�ϲɹ���
        /// </summary>		
        /// <param name="poNoAry">�ɹ�����</param>
        /// <param name="usr">ȷ����</param>
        /// <param name="scm">ȷ�Ϸ�</param>
        /// <returns></returns>
        public int UpdatePoScm(string[] poNoAry, string usr, bool scm)
        {
            DbDRPPO _po = new DbDRPPO(Comp.Conn_DB);
            int _rowCount = _po.UpdatePoScm(poNoAry, usr, scm);

            #region �޸�Ԥ����¼
            AlertModule _am = new AlertModule();
            _am.SetAltDoc("DRP", "PO", usr);
            #endregion

            return _rowCount;
        }
        #endregion

        #region ����д
        /// <summary>
        /// ����д
        /// </summary>
        /// <param name="osId">���ݱ�</param>
        /// <param name="osNo">����</param>
        /// <param name="itm">���</param>
        /// <param name="qty">����</param>
        public void SetFromTi(string osId, string osNo, string itm, decimal qty)
        {
            if (!String.IsNullOrEmpty(osId) && !String.IsNullOrEmpty(osNo) && !String.IsNullOrEmpty(itm))
            {
                DbDRPPO _dbDrpPo = new DbDRPPO(Comp.Conn_DB);
                _dbDrpPo.SetFromTi(osId, osNo, itm, qty);
            }
        }
        #endregion

        #region ����
        /// <summary>
        ///	 ����
        /// </summary>
        /// <param name="changedDs">DataSet</param>
        /// <param name="bubbleException">�Ƿ��׳��쳣��trueΪֱ���׳��쳣��false����ErrorTable��</param>
        /// <returns></returns>
        public DataTable UpdateData(string pgm,SunlikeDataSet changedDs, bool bubbleException)
        {
            changedDs.Tables["TF_POS_BOX"].TableName = "TF_POS1";
            DataTable MF_POSTable = changedDs.Tables["MF_POS"];
            #region ȡ�õ��ݵ����״̬
            if (MF_POSTable.Rows[0].RowState != DataRowState.Deleted)
            {
                _loginUsr = MF_POSTable.Rows[0]["USR"].ToString();
                _osId = MF_POSTable.Rows[0]["OS_ID"].ToString();
            }
            else
            {
                _loginUsr = MF_POSTable.Rows[0]["USR", System.Data.DataRowVersion.Original].ToString();
                _osId = MF_POSTable.Rows[0]["OS_ID", System.Data.DataRowVersion.Original].ToString();
            }
            Auditing _auditing = new Auditing();

            DataRow _dr = MF_POSTable.Rows[0];
            string bilType = "";
            if (_dr.Table.Columns.Contains("BIL_TYPE"))
            {
                if (_dr.RowState == DataRowState.Deleted)
                    bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
                else
                    bilType = _dr["BIL_TYPE"].ToString();
            }
            string _mobID = "";
            if (_dr.Table.Columns.Contains("MOB_ID"))
            {
                if (_dr.RowState == DataRowState.Deleted)
                    _mobID = _dr["MOB_ID", DataRowVersion.Original].ToString();
                else
                    _mobID = _dr["MOB_ID"].ToString();
            }
            //_isRunAuditing = _auditing.IsRunAuditing(_osId, _loginUsr, bilType, _mobID);


            #endregion

            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_POS"] = "OS_ID,OS_NO,OS_DD,BAT_NO,CUS_NO,QT_NO,SAL_NO,USE_DEP,EST_DD,CUR_ID,TAX_ID,BIL_TYPE,CUS_OS_NO,CNTT_NO,REM,BIL_ID,PAY_MTH,PAY_DAYS,CHK_DAYS,PAY_REM,SEND_MTH,SEND_WH,ADR,DIS_CNT,AMTN_NET,FX_WH,YH_NO,PO_DEP,PAY_DD,CHK_DD,INT_DAYS,CLS_REM,USR,CLS_ID,PRT_SW,BIL_NO,CLS_DATE,CHK_MAN,EXC_RTO,BYBOX,TOT_BOX,TOT_QTY,SYS_DATE,ISOVERSH,HS_ID,HIS_PRICE,BACK_ID,INV_DIS_ID,PO_SO_NO,PRE_ID,CONTRACT,MOB_ID";
            _ht["TF_POS"] = "ITM,OS_ID,OS_NO,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY,UP,DIS_CNT,AMT,AMTN,TAX_RTO,TAX,PRE_ITM,QTY_PS,EST_DD,CST_STD,QTY_PO,QTY_PRE,QTY_RK,OS_DD,BOX_ITM,REM,BIL_ID,EST_ITM,CUS_OS_NO,QT_NO,OTH_ITM,BAT_NO,VALID_DD,SCM_USR,SCM_DD,FREE_ID,PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,QTY1,UP_QTY1";
            _ht["TF_POS1"] = "OS_ID,OS_NO,ITM,PRD_NO,WH,CONTENT,QTY,KEY_ITM,EST_DD,BIL_ID,EST_ITM";
            this.UpdateDataSet(changedDs, _ht);
            changedDs.Tables["TF_POS1"].TableName = "TF_POS_BOX";
            //�жϵ����ܷ��޸�
            if (!changedDs.HasErrors)
            {
                //���ӵ���Ȩ��
                string _UpdUsr = "";
                if (changedDs.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changedDs.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    //string _pgm = "DRPPO";
                    DataTable _dtMf = changedDs.Tables["MF_POS"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["PO_DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changedDs.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changedDs.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changedDs.ExtendedProperties["PRN"] = _billRight["PRN"];
                        changedDs.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
                this.SetCanModify(changedDs, true);
                //ȡ���������
                if (changedDs.Tables.Contains("TF_POS_BOX"))
                {
                    DataTable _TF_POS_BOXTable = changedDs.Tables["TF_POS_BOX"];
                    Sunlike.Business.BarBox _bar = new Sunlike.Business.BarBox();
                    DataColumn _dc = new DataColumn("CONTENT_DSC", Type.GetType("System.String"));
                    _dc.MaxLength = 64 * 1024;
                    if (!_TF_POS_BOXTable.Columns.Contains("CONTENT_DSC"))
                        _TF_POS_BOXTable.Columns.Add(_dc);
                    foreach (DataRow _TF_POS_BOXRow in _TF_POS_BOXTable.Rows)
                    {
                        _TF_POS_BOXRow["CONTENT_DSC"] = _bar.GetBar_BoxDsc(_TF_POS_BOXRow["PRD_NO"].ToString(), _TF_POS_BOXRow["CONTENT"].ToString());
                    }
                    _TF_POS_BOXTable.AcceptChanges();
                }
            }
            else
            {
                if (bubbleException)
                {
                    string _errorMsg = Sunlike.Business.BizObject.GetErrorsString(changedDs);
                    throw new SunlikeException("RCID=DRPPO.UpdateData() Error:;" + _errorMsg);
                }
                else
                {
                    return Sunlike.Business.BizObject.GetAllErrors(changedDs);
                }
            }
            return Sunlike.Business.BizObject.GetAllErrors(changedDs);
        }
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dtMf = ds.Tables["MF_POS"];
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["OS_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["OS_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion

            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Deleted)
            //{
            //    _isNeedaffirm = new Cust().IsNeedAffirm(_dtMf.Rows[0]["CUS_NO"].ToString(), "CHK_DRP1");
            //}
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
            string _osNo = "";
            Auditing _auditing = new Auditing();
            #region ȡ�õ��ż��ж��Ƿ��ȥ���
            if (statementType != StatementType.Insert)
            {
                if (statementType == StatementType.Delete)
                {
                    _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _osNo = dr["OS_NO"].ToString();
                }
                if (_auditing.GetIfEnterAuditing(_osId, _osNo))//�����ȥ����˾Ͳ����޸ĺ�����ɾ��
                {
                    throw new Sunlike.Common.Utility.SunlikeException("DRPSO.NOTALLOW");
                }
                //�ж��Ƿ�����������Ѿ����������޸ġ�
                Users _Users = new Users();
                string _whereStr = "OS_ID = '" + _osId + "' AND OS_NO = '" + _osNo + "'";
                if (_Users.IsLocked("MF_POS", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            #endregion

            #region ��ͷ
            if (tableName == "MF_POS")
            {
                string _cusNo = "";
                string _poDep = "";
                string _useDep = "";
                string _salNo = "";
                string _bilType = "";
                DateTime _osDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                //����ʱ�жϹ�������
                if (statementType != StatementType.Delete)
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["OS_DD"]), dr["PO_DEP"].ToString(), "CLS_INV"))
                    {
                        throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                else
                {
                    if (Comp.HasCloseBill(Convert.ToDateTime(dr["OS_DD", DataRowVersion.Original]), dr["PO_DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                    {
                        throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                    }
                }
                if (statementType != StatementType.Delete)
                {
                    //ȡ����
                    #region ȡ����
                    if (dr["OS_DD"] is System.DBNull)
                    {
                        _osDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _osDd = Convert.ToDateTime(dr["OS_DD"]);
                    }
                    if (!String.IsNullOrEmpty(dr["PO_DEP"].ToString()))
                    {
                        _poDep = dr["PO_DEP"].ToString();
                    }
                    #endregion

                    #region --�����ͷ��Ϣ�Ƿ���ȷ
                    //�ͻ����ţ����
                    _cusNo = dr["CUS_NO"].ToString();
                    Cust SunlikeCust = new Cust();
                    DataTable _custTable = SunlikeCust.GetData(_loginUsr, _cusNo);
                    if (_custTable.Rows.Count > 0 && _custTable.Rows[0]["OBJ_ID"].ToString() != "1")
                    {
                        //�Ƿ���Ҫ�ɹ�ȷ��
                        //_isNeedaffirm = SunlikeCust.IsNeedAffirm(_cusNo, "CHK_DRP1");
                    }
                    else
                    {
                        dr.SetColumnError("CUS_NO",/*�ͻ����Ų����ڻ�û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + _cusNo + "");
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�ɹ����ţ����
                    _poDep = dr["PO_DEP"].ToString();
                    Dept SunlikeDept = new Dept();
                    if (SunlikeDept.IsExist(_loginUsr, _poDep, Convert.ToDateTime(dr["OS_DD"])) == false)
                    {
                        dr.SetColumnError("PO_DEP",/*���Ŵ���[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + _poDep);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //ʹ�ò���
                    _useDep = dr["USE_DEP"].ToString();
                    if (!String.IsNullOrEmpty(_useDep))
                    {
                        if (SunlikeDept.IsExist(_loginUsr, _useDep, Convert.ToDateTime(dr["OS_DD"])) == false)
                        {
                            dr.SetColumnError("PO_DEP",/*���Ŵ���[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + _useDep);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //ҵ��Ա
                    _salNo = dr["SAL_NO"].ToString();
                    if (!String.IsNullOrEmpty(_salNo))
                    {
                        Salm SunlikeSalm = new Salm();
                        if (SunlikeSalm.IsExist(_loginUsr, _salNo, Convert.ToDateTime(dr["OS_DD"])) == false)
                        {
                            dr.SetColumnError("SAL_NO",/*ҵ��Ա����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + _salNo);
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    #endregion

                    #region �����ֶ�
                    if (_osId == "PO")
                    {
                        if (dr["PRE_ID"] is System.DBNull)
                        {
                            dr["PRE_ID"] = "F";
                        }
                        if (dr["HS_ID"] is System.DBNull)
                        {
                            dr["HS_ID"] = "T";
                        }
                    }
                    else
                    {
                        if (dr["CLS_ID"] is System.DBNull)
                        {
                            dr["CLS_ID"] = "F";
                        }

                        if (dr["ISOVERSH"] is System.DBNull)
                        {
                            dr["ISOVERSH"] = "F";
                        }
                        if (dr["HIS_PRICE"] is System.DBNull)
                        {
                            dr["HIS_PRICE"] = "F";
                        }

                    }
                    #endregion


                    if (statementType == StatementType.Insert)
                    {

                        #region --���ɵ���--
                        SQNO SunlikeSqNo = new SQNO();
                        _poDep = dr["PO_DEP"].ToString();//�ɹ�����
                        if (dr["OS_DD"] is System.DBNull)
                        {
                            _osDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                        }
                        else
                        {
                            _osDd = Convert.ToDateTime(dr["OS_DD"]);
                        }

                        if (dr["EST_DD"] is System.DBNull)
                        {
                            dr["EST_DD"] = System.DateTime.Now.ToString(Comp.SQLDateFormat);
                        }
                        if (dr["EXC_RTO"] is System.DBNull)
                        {
                            dr["EXC_RTO"] = "1";
                        }

                        _bilType = dr["BIL_TYPE"].ToString();
                        _osNo = SunlikeSqNo.Set(_osId, _loginUsr, _poDep, _osDd, _bilType);
                        dr["OS_NO"] = _osNo;
                        dr["OS_DD"] = _osDd.ToString(Comp.SQLDateFormat);
                        dr["SYS_DATE"] = System.DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        dr["PRT_SW"] = "N";
                        #endregion

                    }
                    if (string.IsNullOrEmpty(dr["CUR_ID"].ToString()))
                    {
                        dr["EXC_RTO"] = 1;
                    }
                    #region �ж��Ƿ����������
                    //�����������
                    //if (!_isRunAuditing)
                    //{
                    //    if (statementType == StatementType.Insert)
                    //    {
                    //        dr["CHK_MAN"] = _loginUsr;
                    //        //dr["CHK_DD"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //        dr["CLS_DATE"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //    }
                    //}
                    //else //�����
                    //{
                    //    if ((_isRunAuditing) && (statementType == StatementType.Update) && (!String.IsNullOrEmpty(dr["CHK_MAN"].ToString())))
                    //    //if (statementType == StatementType.Update)
                    //    {
                    //        //�ع���
                    //        string _error = this.RollBack(_osId, _osNo, false);
                    //        if (_error.Length > 0)
                    //            throw new Exception(_error);
                    //    }
                    //    #region д���������
                    //    AudParamStruct _aps;
                    //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                    //    _aps.BIL_ID = _osId;
                    //    _aps.BIL_NO = _osNo;
                    //    _aps.BIL_DD = _osDd;
                    //    _aps.USR = _loginUsr;
                    //    _aps.CUS_NO = _cusNo;
                    //    _aps.DEP = "";
                    //    _aps.SAL_NO = _salNo;

                    //    //�¼ӵĲ��֣���Ӧ���ģ��
                    //    _aps.MOB_ID = "";
                    //    if (dr.Table.Columns.Contains("MOB_ID"))
                    //    {
                    //        dr["MOB_ID"] = _aps.MOB_ID = _auditing.GetSHMobID(_aps.BIL_ID, _aps.USR, _aps.BIL_TYPE, Convert.ToString(dr["MOB_ID"]));
                    //    }
                    //    _auditing.SetBillToAudtingFlow("DRP", _aps, null);
                    //    #endregion
                    //    dr["CHK_MAN"] = System.DBNull.Value;
                    //    //dr["CHK_DD"] = System.DBNull.Value;
                    //    dr["CLS_DATE"] = System.DBNull.Value;
                    //}
                    #endregion
                }
                else //ɾ������
                {
                    #region ɾ������ͬʱɾ�������Ϣ
                    //_auditing.DelBillWaitAuditing("DRP", _osId, _osNo);
                    #endregion

                    #region �ֶ��᰸
                    if (!_isRunAuditing)
                    {
                        if (dr["CLS_ID", DataRowVersion.Original].ToString() == "T")
                        {
                            DoCloseBill(_osId, _osNo);
                        }
                    }
                    #endregion

                }

                //#region ������
                //AudParamStruct _aps;
                //if (dr.RowState != DataRowState.Deleted)
                //{
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.BIL_ID = _osId;
                //    _aps.BIL_NO = _osNo;
                //    _aps.BIL_DD = _osDd;
                //    _aps.USR = _loginUsr;
                //    _aps.CUS_NO = _cusNo;
                //    _aps.DEP = "";
                //    _aps.SAL_NO = _salNo;
                //    //_aps.MOB_ID = "";
                //}
                //else
                //{
                //    //_aps = new AudParamStruct(_osId, _osNo);
                //}

                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion
            }
            #endregion

            WH SunlikeWH = new WH();
            #region ����
            if (tableName == "TF_POS")
            {
                string _prdNo = "";
                string _whNo = "";
                if (statementType != StatementType.Delete)
                {
                    #region ���������Ϣ�Ƿ���ȷ
                    //��Ʒ��⣨���
                    _prdNo = dr["PRD_NO"].ToString();
                    Prdt _prdt = new Prdt();
                    if (_prdt.IsExist(_loginUsr, _prdNo, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])) == false)
                    {
                        dr.SetColumnError("PRD_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prdNo);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }

                    //�ֿ��⣨���
                    _whNo = dr["WH"].ToString();

                    if (SunlikeWH.IsExist(_loginUsr, _whNo, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])) == false)
                    {
                        dr.SetColumnError("WH",/*�ֿ����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _whNo);
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
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        if (Convert.ToDecimal(dr["QTY"]) < 0)
                        {
                            Bat _bat = new Bat();
                            if (_bat.GetData(dr["BAT_NO"].ToString()).Tables["BAT_NO"].Rows.Count == 0)
                            {
                                dr.SetColumnError("BAT_NO", "RCID=COMMON.HINT.ISEXIST,PARAM=" + dr["BAT_NO"].ToString());
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                        else
                        {
                            Bat _bat = new Bat();
                            if (String.IsNullOrEmpty(dr["OS_DD"].ToString()))
                            {
                                dr["OS_DD"] = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"];
                            }
                            _bat.AutoInsertData(dr["BAT_NO"].ToString(), dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["OS_DD"]));
                        }
                    }

                    #endregion

                    #region �滻����Ĳɹ�����

                    if (dr.Table.DataSet.Tables["MF_POS"].Rows.Count > 0)
                    {
                        if (dr["OS_DD"] != dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])
                            dr["OS_DD"] = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"];
                    }
                    #endregion

                    #region �޸ı����SCM_USR��SCM_DD�ֶ�
                    if (!_isNeedaffirm)
                    {
                        dr["SCM_USR"] = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["USR"];
                        dr["SCM_DD"] = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"];
                    }
                    else if ((String.IsNullOrEmpty(dr["QTY_PS"].ToString()) || Convert.ToDecimal(dr["QTY_PS"]) == 0)
                            && (String.IsNullOrEmpty(dr["QTY_SL"].ToString()) || Convert.ToDecimal(dr["QTY_SL"]) == 0)
                            && (String.IsNullOrEmpty(dr["QTY_RK"].ToString()) || Convert.ToDecimal(dr["QTY_RK"]) == 0))
                    {
                        dr["SCM_USR"] = DBNull.Value;
                        dr["SCM_DD"] = DBNull.Value;
                    }
                    #endregion

                    #region �ж� �޸ĵ�����ֻ���޸�Ϊ>=����-�ѽ�����-�ɹ��˻�����-�Ѳɹ�����
                    if (_osId == "PO")
                    {
                        decimal _qty = 0;
                        decimal _qtyPre = 0;
                        decimal _qtyPs = 0;
                        decimal _qtyPo = 0;
                        if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                        {
                            _qty = Convert.ToDecimal(dr["QTY"]);
                        }
                        if (!String.IsNullOrEmpty(dr["QTY_PRE"].ToString()))
                        {
                            _qtyPre = Convert.ToDecimal(dr["QTY_PRE"]);
                        }
                        if (!String.IsNullOrEmpty(dr["QTY_PS"].ToString()))
                        {
                            _qtyPs = Convert.ToDecimal(dr["QTY_PS"]);
                        }
                        if (!String.IsNullOrEmpty(dr["QTY_PO"].ToString()))
                        {
                            _qtyPo += Convert.ToDecimal(dr["QTY_PO"]);
                        }
                        if (!String.IsNullOrEmpty(dr["QTY_PO_UNSH"].ToString()))
                        {
                            _qtyPo += Convert.ToDecimal(dr["QTY_PO_UNSH"]);
                        }
                        if (_qty < _qtyPre + _qtyPs + _qtyPo)
                        {
                            dr.SetColumnError("QTY",/*����������Χ*/"RCID=COMMON.HINT.SCOPEERROR");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    if (_osId == "PR")
                    {
                        decimal _dQty = 0;
                        decimal _dQtyPo = 0;
                        decimal _dQtyPoPre = 0;
                        decimal _dQtyPoPs = 0;
                        decimal _dQtyOriginal = 0;
                        int _preItm = -1;
                        if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                        {
                            _dQty = Convert.ToDecimal(dr["QTY"]);
                        }
                        if (!String.IsNullOrEmpty(dr["PRE_ITM"].ToString()))
                        {
                            _preItm = Convert.ToInt32(dr["PRE_ITM"]);
                        }
                        if (statementType == StatementType.Update)
                        {
                            if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                            {
                                _dQtyOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                            }
                        }
                        //ԭ�ɹ�����
                        string _qtNoPre = "";
                        _qtNoPre = dr["QT_NO"].ToString();
                        SunlikeDataSet _poDs = GetBody("PO", _qtNoPre, "PRE_ITM", _preItm, false);
                        if (_poDs.Tables["TF_POS"].Rows.Count > 0)
                        {
                            if (!String.IsNullOrEmpty(_poDs.Tables["TF_POS"].Rows[0]["QTY"].ToString()))
                            {
                                _dQtyPo = Convert.ToDecimal(_poDs.Tables["TF_POS"].Rows[0]["QTY"]);
                            }
                            if (!String.IsNullOrEmpty(_poDs.Tables["TF_POS"].Rows[0]["QTY_PRE"].ToString()))
                            {
                                _dQtyPoPre = Convert.ToDecimal(_poDs.Tables["TF_POS"].Rows[0]["QTY_PRE"]);
                            }
                            if (!String.IsNullOrEmpty(_poDs.Tables["TF_POS"].Rows[0]["QTY_PS"].ToString()))
                            {
                                _dQtyPoPs = Convert.ToDecimal(_poDs.Tables["TF_POS"].Rows[0]["QTY_PS"]);
                            }
                        }
                        if (_dQty > _dQtyPo + _dQtyOriginal - _dQtyPoPre - _dQtyPoPs)//����
                        {
                            dr.SetColumnError("QTY",/*����������Χ*/"RCID=COMMON.HINT.SCOPEERROR");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }

                    }

                    #endregion

                    #region ������
                    //EST_ITMΪ������
                    if (dr["PRE_ITM"] is System.DBNull)
                    {
                        dr["PRE_ITM"] = dr["EST_ITM"];
                    }
                    #endregion
                }
                else//ɾ��ʱ
                {
                    if (!(dr["QTY_PS", System.Data.DataRowVersion.Original] is System.DBNull))
                    {
                        decimal _qtyPs = Convert.ToDecimal(dr["QTY_PS", System.Data.DataRowVersion.Original]);
                        if (_qtyPs != 0)
                        {
                            dr.SetColumnError("QTY_PS", "RCID=INV.HINT.UNDELETEPS");//�Ѿ��ɹ��˻ز���ɾ��
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    if (!(dr["QTY_PRE", System.Data.DataRowVersion.Original] is System.DBNull))
                    {
                        decimal _qtyPre = Convert.ToDecimal(dr["QTY_PRE", System.Data.DataRowVersion.Original]);
                        if (_qtyPre != 0)
                        {
                            dr.SetColumnError("QTY_PRE", "RCID=INV.HINT.UNDELETEPRE1");//�Ѿ��ɹ��˻ز���ɾ��
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    if (dr["QTY_SL", System.Data.DataRowVersion.Original] != System.DBNull.Value)
                    {
                        if (Convert.ToDecimal(dr["QTY_SL", System.Data.DataRowVersion.Original]) != 0)
                        {
                            dr.SetColumnError("QTY_SL", "�����������ڣ��޷�ɾ��");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    if (dr["QTY_PO", System.Data.DataRowVersion.Original] != System.DBNull.Value)
                    {
                        if (Convert.ToDecimal(dr["QTY_PO", System.Data.DataRowVersion.Original]) != 0)
                        {
                            dr.SetColumnError("QTY_PO", "�Ѳɹ������ڣ��޷�ɾ��");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    if (dr["QTY_PO_UNSH", System.Data.DataRowVersion.Original] != System.DBNull.Value)
                    {
                        if (Convert.ToDecimal(dr["QTY_PO_UNSH", System.Data.DataRowVersion.Original]) != 0)
                        {
                            dr.SetColumnError("QTY_PO_UNSH", "δ����Ѳɹ������ڣ��޷�ɾ��");
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    if (_osId == "PO")
                    {
                        CompInfo _compInfo = Comp.GetCompInfo("");
                        if (_compInfo.SystemInfo.CHK_DRP1)
                        {
                            string _cusNo = "";
                            DataRow _drMF = dr.Table.DataSet.Tables["MF_POS"].Rows[0];
                            if (_drMF.RowState == DataRowState.Deleted)
                            {
                                _cusNo = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CUS_NO", DataRowVersion.Original].ToString();
                            }
                            else
                            {
                                _cusNo = dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CUS_NO"].ToString();
                            }
                            Cust _cust = new Cust();
                            if (_cust.IsNeedAffirm(_cusNo, "CHK_DRP1"))
                            {
                                if (!String.IsNullOrEmpty(dr["SCM_USR", System.Data.DataRowVersion.Original].ToString()))
                                {
                                    dr.SetColumnError("SCM_USR", "RCID=DRPPO.HAS_CMF");
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }

                    int _poEstItm = 0;
                    string _poOsNo = "";
                    string _error = "";
                    if (_osId == "PO")
                    {
                        if (!string.IsNullOrEmpty(dr["EST_ITM", System.Data.DataRowVersion.Original].ToString()))
                        {
                            _poEstItm = Convert.ToInt16(dr["EST_ITM", System.Data.DataRowVersion.Original]);
                        }
                        _poOsNo = dr["OS_NO", System.Data.DataRowVersion.Original].ToString();
                        _error = "�����ڲ��ӵ�ȷ���У������޸�/ɾ��/����";
                    }
                    else if (_osId == "PR")
                    {
                        int _preItm = 0;
                        if (!string.IsNullOrEmpty(dr["PRE_ITM", System.Data.DataRowVersion.Original].ToString()))
                        {
                            _preItm = Convert.ToInt32(dr["PRE_ITM", System.Data.DataRowVersion.Original]);
                        }
                        SunlikeDataSet _dsPo = GetBody(dr["QT_NO", System.Data.DataRowVersion.Original].ToString(), "PO", "PRE_ITM", _preItm, false);
                        if (_dsPo.Tables[0].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_dsPo.Tables[0].Rows[0]["EST_ITM"].ToString()))
                            {
                                _poEstItm = Convert.ToInt16(_dsPo.Tables[0].Rows[0]["EST_ITM"]);
                            }
                            _poOsNo = _dsPo.Tables[0].Rows[0]["OS_NO"].ToString();
                        }
                        _error = "�����Ѳ����������ݣ�";
                    }

                    using (SunlikeDataSet _dsjD = GetJDData(_poOsNo, "PO", _poEstItm))
                    {
                        if (_dsjD.Tables[0].Rows.Count > 0)
                        {
                            if (_dsjD.Tables[0].Rows[0]["STATUS_JD"].ToString() == "Y")
                            {
                                dr.SetColumnError("STATUS_JD", _error);
                                status = UpdateStatus.SkipAllRemainingRows;
                            }
                        }
                    }

                }//end ɾ��ʱ
            }
            #endregion

            #region �����䣩
            if (tableName == "TF_POS1")
            {
                string _prdNoBox = "";
                string _whBox = "";
                if (statementType != StatementType.Delete)
                {
                    #region ���������Ϣ�Ƿ���ȷ
                    //��Ʒ��⣨���
                    _prdNoBox = dr["PRD_NO"].ToString();
                    Prdt _prdt = new Prdt();
                    if (_prdt.IsExist(_loginUsr, _prdNoBox, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])) == false)
                    {
                        dr.SetColumnError("PRD_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prdNoBox);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //�ֿ⣨���						
                    _whBox = dr["WH"].ToString();
                    if (SunlikeWH.IsExist(_loginUsr, _whBox, Convert.ToDateTime(dr.Table.DataSet.Tables["MF_POS"].Rows[0]["OS_DD"])) == false)
                    {
                        dr.SetColumnError("WH",/*�ֿ����[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.WHERROR,PARAM=" + _whBox);
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //����ȣ����
                    if (SunlikeWH.IsBoxExist(_prdNoBox, _whBox, dr["CONTENT"].ToString()) == false)
                    {
                        dr.SetColumnError("CONTENT",/*string.Format("��Ʒ[{0}]�����[{1}]�ڲֿ�[{2}]������",_prd_no,_wh,_content)*/"RCID=INV.HINT.PRDTCONTENTERROR,PARAM=" + _prdNoBox + ",PARAM=" + _whBox + ",PARAM=" + dr["CONTENT"].ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    #endregion

                    #region ������
                    //EST_ITMΪ������
                    if (dr["EST_ITM"] is System.DBNull)
                    {
                        dr["EST_ITM"] = dr["KEY_ITM"];
                    }
                    #endregion

                }
            }
            #endregion
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
            string _osNo = "";
            string _cusNo = "";
            string _poDepNo = "";
            string _salNo = "";
            DateTime _osDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
            Auditing _auditing = new Auditing();
            if (tableName == "MF_POS")
            {
                if (statementType != StatementType.Delete)
                {
                    #region ȡ����
                    _osNo = dr["OS_NO"].ToString();
                    if (dr["OS_DD"] is System.DBNull)
                    {
                        _osDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _osDd = Convert.ToDateTime(dr["OS_DD"]);
                    }
                    _cusNo = dr["CUS_NO"].ToString();
                    _poDepNo = dr["CUS_NO"].ToString();
                    _salNo = dr["SAL_NO"].ToString();
                    #endregion
                    #region �ֶ��᰸
                    if (!_isRunAuditing)
                    {
                        if (statementType == StatementType.Update)
                        {
                            if (dr["CLS_ID"].ToString() != dr["CLS_ID", DataRowVersion.Original].ToString())
                            {
                                DoCloseBill(_osId, _osNo);
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    _osNo = dr["OS_NO", System.Data.DataRowVersion.Original].ToString();
                    SQNO _sqNo = new SQNO();
                    _sqNo.Delete(_osNo, _loginUsr);//ɾ��ʱ��BILD�в���һ������
                }
            }
            if ((tableName == "TF_POS"))
            {
                if (!_isRunAuditing)
                {
                    //�޸Ĳ�Ʒ���

                    UpdateTf(_osId, dr, statementType);
                    if (_osId == "PR")
                    {
                        UpdateQtyPre(dr, statementType);
                    }
                    if (_osId == "PO")
                    {
                        UpdateQtyPo(dr, statementType);
                    }

                }

                if (_osId == "PO")
                {
                    if (statementType == StatementType.Delete || statementType == StatementType.Update)
                    {
                        int _estItm = 0;
                        if (!string.IsNullOrEmpty(dr["EST_ITM", DataRowVersion.Original].ToString()))
                        {
                            _estItm = Convert.ToInt16(dr["EST_ITM", DataRowVersion.Original]);
                        }
                        DeleteTfJD(dr["OS_NO", DataRowVersion.Original].ToString(), dr["OS_ID", DataRowVersion.Original].ToString(), _estItm);
                    }
                }
            }
            if (tableName == "TF_POS1")
            {

                if (!_isRunAuditing)
                {
                    //�޸�����
                    UpdateTfBox(_osId, dr, statementType);
                    if (_osId == "PR")
                    {
                        //UpdateQtyPreBox(_osId,dr,statementType);
                    }
                    if (_osId == "PO")
                    {
                        //UpdateQtyPoBox(_osId,dr,statementType);
                    }
                }
            }

            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }

        #region �޸Ŀ��
        //�޸Ĳ�Ʒ���
        private void UpdateTf(string bilId, DataRow dr, StatementType statementType)
        {
            string _prdt1prd_no = "";
            string _prdt1park_mark = "";
            string _prdt1wh = "";
            decimal _prdt1qty_on_way = 0;
            decimal _qtyFlag = 1;
            string _prd_noOriginal = "";
            string _park_markOriginal = "";
            string _whOriginal = "";
            string _utOriginal = "";
            string _batNo = "";
            string _batNoOrg = "";
            string _qtNo = "";			//ԭ�ɹ�����
            string _qtNoOriginal = "";  //ԭ�ɹ�����
            decimal _qty_on_wayOriginal = 0;
            bool _updateCurent = true;
            bool _updateOriginal = true;

            bool _isCloseBill = false;
            StatementType _billStatementType = StatementType.Insert;
            //�Ƿ�᰸
            if (dr.Table.DataSet != null && dr.Table.DataSet.Tables.Contains("MF_POS") && dr.Table.DataSet.Tables["MF_POS"].Rows.Count > 0)
            {
                if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Deleted)
                {
                    _billStatementType = StatementType.Delete;
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CLS_ID", System.Data.DataRowVersion.Original].ToString() == "T")
                        _isCloseBill = true;
                }
                else
                {
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Added)
                    {
                        _billStatementType = StatementType.Insert;
                    }
                    else if (dr.Table.DataSet.Tables["MF_POS"].Rows[0].RowState == System.Data.DataRowState.Modified)
                    {
                        _billStatementType = StatementType.Update;
                    }
                    if (dr.Table.DataSet.Tables["MF_POS"].Rows[0]["CLS_ID"].ToString() == "T")
                        _isCloseBill = true;
                }
            }
            #region �ֶ��᰸
            if (_isCloseBill && _billStatementType == StatementType.Insert)
                return;
            if (_isCloseBill && _billStatementType == StatementType.Delete)
                return;
            #endregion

            WH SunlikeWH = new WH();
            string _ut = "";

            #region ȡ����
            if (statementType == StatementType.Insert)
            {
                _prdt1prd_no = dr["PRD_NO"].ToString();
                _prdt1park_mark = dr["PRD_MARK"].ToString();
                _prdt1wh = dr["WH"].ToString();
                _ut = dr["UNIT"].ToString();
                _batNo = dr["BAT_NO"].ToString();
                _qtNo = dr["QT_NO"].ToString();
                if (!(dr["QTY"] is System.DBNull))
                {
                    _prdt1qty_on_way = Convert.ToDecimal(dr["QTY"]);
                }
            }
            if (statementType == StatementType.Delete)
            {
                _prd_noOriginal = dr["PRD_NO", DataRowVersion.Original].ToString();
                _park_markOriginal = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _batNoOrg = dr["BAT_NO", DataRowVersion.Original].ToString();
                _qtNoOriginal = dr["QT_NO", DataRowVersion.Original].ToString();
                _whOriginal = dr["WH", DataRowVersion.Original].ToString();
                _utOriginal = dr["UNIT", DataRowVersion.Original].ToString();
                if (!(dr["QTY", DataRowVersion.Original] is System.DBNull))
                {
                    _qty_on_wayOriginal = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
            }
            if (statementType == StatementType.Update)
            {
                _prdt1prd_no = dr["PRD_NO"].ToString();
                _prdt1park_mark = dr["PRD_MARK"].ToString();
                _batNo = dr["BAT_NO"].ToString();
                _qtNo = dr["QT_NO"].ToString();
                _prdt1wh = dr["WH"].ToString();
                _ut = dr["UNIT"].ToString();
                if (!(dr["QTY"] is System.DBNull))
                {
                    _prdt1qty_on_way = Convert.ToDecimal(dr["QTY"]);
                }
                _prd_noOriginal = dr["PRD_NO", DataRowVersion.Original].ToString();
                _park_markOriginal = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _batNoOrg = dr["BAT_NO", DataRowVersion.Original].ToString();
                _qtNoOriginal = dr["QT_NO", DataRowVersion.Original].ToString();
                _whOriginal = dr["WH", DataRowVersion.Original].ToString();
                _utOriginal = dr["UNIT", DataRowVersion.Original].ToString();
                _qty_on_wayOriginal = 0;

                if (!(dr["QTY", DataRowVersion.Original] is System.DBNull))
                {
                    _qty_on_wayOriginal = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
            }
            #endregion

            Hashtable _ht = new Hashtable();
            if (_prdt1prd_no.Length > 0)
            {
                #region �жϵ��ݱ�
                if (bilId == "PR")
                {
                    _qtyFlag = -1;
                    //���ԭ�Ȳɹ������ֹ��᰸���޸�
                    DataTable _poTable = this.GetData("PO", _qtNo, false).Tables["MF_POS"];
                    if (_poTable.Rows.Count > 0 && _poTable.Rows[0]["CLS_ID"].ToString() == "T" && _poTable.Rows[0]["BACK_ID"].ToString().Length == 0)
                    {
                        _updateCurent = false;
                    }
                }
                else if (bilId == "PO")
                    _qtyFlag = 1;
                #endregion

                #region �޸Ļ�Ʒ�ֲִ���
                if (_updateCurent)
                {
                    if (String.IsNullOrEmpty(_batNo))
                    {
                        SunlikeWH.UpdateQty(_prdt1prd_no, _prdt1park_mark, _prdt1wh, _ut, WH.QtyTypes.QTY_ON_WAY, _qtyFlag * _prdt1qty_on_way);
                    }
                    else
                    {
                        _ht[WH.QtyTypes.QTY_ON_WAY] = _qtyFlag * _prdt1qty_on_way;
                        SunlikeWH.UpdateQty(_batNo, _prdt1prd_no, _prdt1park_mark, _prdt1wh, "", _ut, _ht);
                    }
                }
                #endregion

            }
            if (_prd_noOriginal.Length > 0)
            {
                #region �жϵ��ݱ�
                if (bilId == "PR")
                {
                    _qtyFlag = -1;
                    //���ԭ�Ȳɹ������ֹ��᰸���޸�
                    DataTable _poTable = this.GetData("PO", _qtNoOriginal, false).Tables["MF_POS"];
                    if (_poTable.Rows.Count > 0 && _poTable.Rows[0]["CLS_ID"].ToString() == "T" && _poTable.Rows[0]["BACK_ID"].ToString().Length == 0)
                    {
                        _updateCurent = false;
                    }
                }
                else if (bilId == "PO")
                    _qtyFlag = 1;
                #endregion

                #region �޸Ļ�Ʒ�ֲִ���
                if (_updateOriginal)
                {
                    if (String.IsNullOrEmpty(_batNoOrg))
                    {
                        SunlikeWH.UpdateQty(_prd_noOriginal, _park_markOriginal, _whOriginal, _utOriginal, WH.QtyTypes.QTY_ON_WAY, (-1) * _qtyFlag * _qty_on_wayOriginal);
                    }
                    else
                    {
                        _ht[WH.QtyTypes.QTY_ON_WAY] = (-1) * _qtyFlag * _qty_on_wayOriginal;
                        SunlikeWH.UpdateQty(_batNoOrg, _prd_noOriginal, _park_markOriginal, _whOriginal, "", _utOriginal, _ht);
                    }
                }
                #endregion
            }
        }
        //�޸�����
        private void UpdateTfBox(string bilId, DataRow dr, StatementType statementType)
        {

            /*
            string _prdt1BoxPrd_no = "";
            string _prdt1BoxWh = "";
            string _prdt1BoxContent = "";
            decimal _prdt1Boxqty_on_way = 0;

            WH SunlikeWH = new WH();
            Prdt _prdt = new Prdt();
            if (statementType == StatementType.Insert)
            {
                _prdt1BoxPrd_no = dr["PRD_NO"].ToString();
                _prdt1BoxWh = dr["WH"].ToString();
                _prdt1BoxContent = dr["CONTENT"].ToString();
                if (!(dr["QTY"] is System.DBNull))
                {
                    _prdt1Boxqty_on_way = Convert.ToDecimal(dr["QTY"]);
                }
                SunlikeWH.UpdateBoxQty(_prdt1BoxPrd_no,_prdt1BoxWh,_prdt1BoxContent,WH.BoxQtyTypes.QTY_ON_WAY,_prdt1Boxqty_on_way);
            }
            if (statementType == StatementType.Delete)
            {
                _prdt1BoxPrd_no = dr["PRD_NO",DataRowVersion.Original].ToString();
                _prdt1BoxWh = dr["WH",DataRowVersion.Original].ToString();
                _prdt1BoxContent = dr["CONTENT",DataRowVersion.Original].ToString();
                if (!(dr["QTY",DataRowVersion.Original] is System.DBNull))
                {
                    _prdt1Boxqty_on_way = Convert.ToDecimal(dr["QTY",DataRowVersion.Original]);
                }
                SunlikeWH.UpdateBoxQty(_prdt1BoxPrd_no,_prdt1BoxWh,_prdt1BoxContent,WH.BoxQtyTypes.QTY_ON_WAY,-_prdt1Boxqty_on_way);
			
            }
            if (statementType == StatementType.Update)
            {
                string _prdt1BoxPrd_noCurrent = dr["PRD_NO",DataRowVersion.Current].ToString();;
                string _prdt1BoxPrd_noOriginal = dr["PRD_NO",DataRowVersion.Original].ToString();

                string _prdt1BoxWhCurrent = dr["WH",DataRowVersion.Current].ToString();;
                string _prdt1BoxWhOriginal =  dr["WH",DataRowVersion.Original].ToString();

                string _prdt1BoxContentCurrent = dr["CONTENT",DataRowVersion.Current].ToString();;
                string _prdt1BoxContentOriginal = dr["CONTENT",DataRowVersion.Original].ToString();

                decimal _prdt1Boxqty_on_wayCurrent = 0;
                decimal _prdt1Boxqty_on_wayOriginal = 0;

                if (!(dr["QTY",DataRowVersion.Current] is System.DBNull))
                {
                    _prdt1Boxqty_on_wayCurrent = Convert.ToDecimal(dr["QTY",DataRowVersion.Current]);
                }

                if (!(dr["QTY",DataRowVersion.Original] is System.DBNull))
                {
                    _prdt1Boxqty_on_wayOriginal = Convert.ToDecimal(dr["QTY",DataRowVersion.Original]);
                }

                SunlikeWH.UpdateBoxQty(_prdt1BoxPrd_noOriginal,_prdt1BoxWhOriginal,_prdt1BoxContentCurrent,WH.BoxQtyTypes.QTY_ON_WAY,-_prdt1Boxqty_on_wayOriginal);
                SunlikeWH.UpdateBoxQty(_prdt1BoxPrd_noCurrent,_prdt1BoxWhCurrent,_prdt1BoxContentCurrent,WH.BoxQtyTypes.QTY_ON_WAY,_prdt1Boxqty_on_wayCurrent );
            }
            */
        }
        #endregion

        #region �޸�ѯ�۵����ѽ��빺��
        private void UpdateQtyPo(DataRow dr, StatementType statementType)
        {
            string _qtId = "";
            string _qtNo = "";
            string _osNo = "";
            string _qtNoOriginal = "";
            int _itm = 0;				//ѯ�۵��������
            int _itmOriginal = 0;		//ѯ�۵��������
            decimal _qtyPo = 0;
            decimal _qtyPoOriginal = 0;
            string _prdNo = "";
            string _oldPrdNo = "";
            int _unit = 1;
            int _oldUnit = 1;
            _qtId = "QS";
            if (statementType == StatementType.Insert)
            {
                _qtId = dr["BIL_ID"].ToString();
                _qtNo = dr["QT_NO"].ToString();
                _osNo = dr["OS_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                    _itm = Convert.ToInt32(dr["OTH_ITM"].ToString());
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qtyPo = Convert.ToDecimal(dr["QTY"]);
                _prdNo = dr["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    _unit = Convert.ToInt32(dr["UNIT"]);
            }
            else if (statementType == StatementType.Update)
            {
                _qtId = dr["BIL_ID"].ToString();
                _qtNo = dr["QT_NO"].ToString();
                _osNo = dr["OS_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                    _itm = Convert.ToInt32(dr["OTH_ITM"].ToString());
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qtyPo = Convert.ToDecimal(dr["QTY"]);
                _prdNo = dr["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    _unit = Convert.ToInt32(dr["UNIT"]);

                _qtNoOriginal = dr["QT_NO", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["OTH_ITM", System.Data.DataRowVersion.Original].ToString()))
                    _itmOriginal = Convert.ToInt32(dr["OTH_ITM", System.Data.DataRowVersion.Original].ToString());
                if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                    _qtyPoOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                _oldPrdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    _oldUnit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);


            }
            else if (statementType == StatementType.Delete)
            {
                _qtId = dr["BIL_ID", System.Data.DataRowVersion.Original].ToString();
                _qtNoOriginal = dr["QT_NO", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["OTH_ITM", System.Data.DataRowVersion.Original].ToString()))
                    _itmOriginal = Convert.ToInt32(dr["OTH_ITM", System.Data.DataRowVersion.Original].ToString());
                if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                    _qtyPoOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                _oldPrdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    _oldUnit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
            }
            if (_qtId != "QS")
                return;
            Sunlike.Business.DRPQS _drpQs = new DRPQS();
            Hashtable _ht = new Hashtable();
            _ht["TableName"] = "TF_QTS";
            _ht["IdName"] = "QT_ID";
            _ht["NoName"] = "QT_NO";
            _ht["ItmName"] = "OTH_ITM";
            _ht["OsID"] = _qtId;
            if (_qtNoOriginal.Length > 0)
            {
                _ht["OsNO"] = _qtNoOriginal;
                _ht["KeyItm"] = _itmOriginal;
                _qtyPoOriginal = INVCommon.GetRtnQty(_oldPrdNo, (-1) * _qtyPoOriginal, _oldUnit, _ht);
                _drpQs.UpdateQtyPo("QS", _qtNoOriginal, _itmOriginal, _qtyPoOriginal);
                _drpQs.UpdateOsNo("QS", _qtNoOriginal, "");
            }
            if (_qtNo.Length > 0)
            {
                _ht["OsNO"] = _qtNo;
                _ht["KeyItm"] = _itm;
                _qtyPo = INVCommon.GetRtnQty(_prdNo, _qtyPo, _unit, _ht);
                _drpQs.UpdateQtyPo("QS", _qtNo, _itm, _qtyPo);
                _drpQs.UpdateOsNo("QS", _qtNo, _osNo);
            }
        }

        #endregion

        #region �޸Ĳɹ�������������
        /// <summary>
        /// �޸Ĳɹ�������������
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="statementType"></param>
        private void UpdateQtyPre(DataRow dr, StatementType statementType)
        {
            string _osId = "";
            string _osNo = "";
            string _osNoOriginal = "";
            int _itm = 0;
            int _itmOriginal = 0;
            decimal _qtyPre = 0;
            decimal _qtyPreOriginal = 0;
            string _prdNo = "";
            string _oldPrdNo = "";
            int _unit = 1;
            int _oldUnit = 1;
            _osId = "PO";
            if (statementType == StatementType.Insert)
            {
                _osNo = dr["QT_NO"].ToString();

                if (!String.IsNullOrEmpty(dr["PRE_ITM"].ToString()))
                    _itm = Convert.ToInt32(dr["PRE_ITM"]);
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qtyPre = Convert.ToDecimal(dr["QTY"]);
                _prdNo = dr["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    _unit = Convert.ToInt32(dr["UNIT"]);
            }
            else if (statementType == StatementType.Update)
            {
                _osNo = dr["QT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["PRE_ITM"].ToString()))
                    _itm = Convert.ToInt32(dr["PRE_ITM"]);
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                    _qtyPre = Convert.ToDecimal(dr["QTY"]);
                _prdNo = dr["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    _unit = Convert.ToInt32(dr["UNIT"]);

                _osNoOriginal = dr["QT_NO", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["PRE_ITM", System.Data.DataRowVersion.Original].ToString()))
                    _itmOriginal = Convert.ToInt32(dr["PRE_ITM", System.Data.DataRowVersion.Original]);
                if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                    _qtyPreOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                _oldPrdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    _oldUnit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
            }
            else if (statementType == StatementType.Delete)
            {

                _osNoOriginal = dr["QT_NO", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["PRE_ITM", System.Data.DataRowVersion.Original].ToString()))
                    _itmOriginal = Convert.ToInt32(dr["PRE_ITM", System.Data.DataRowVersion.Original]);
                if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                    _qtyPreOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                _oldPrdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    _oldUnit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
            }
            DbDRPPO _drpPo = new DbDRPPO(Comp.Conn_DB);

            Hashtable _ht = new Hashtable();
            _ht["TableName"] = "TF_POS";
            _ht["IdName"] = "OS_ID";
            _ht["NoName"] = "OS_NO";
            _ht["ItmName"] = "PRE_ITM";
            _ht["OsID"] = _osId;
            if (_osNoOriginal.Length > 0)
            {
                _ht["OsNO"] = _osNoOriginal;
                _ht["KeyItm"] = _itmOriginal;
                _qtyPreOriginal = INVCommon.GetRtnQty(_oldPrdNo, (-1) * _qtyPreOriginal, _oldUnit, _ht);
                _drpPo.UpdateQtyPre(_osId, _osNoOriginal, _itmOriginal, _qtyPreOriginal);
            }
            if (_osNo.Length > 0)
            {
                _ht["OsNO"] = _osNo;
                _ht["KeyItm"] = _itm;
                _qtyPre = INVCommon.GetRtnQty(_prdNo, _qtyPre, _unit, _ht);
                _drpPo.UpdateQtyPre(_osId, _osNo, _itm, _qtyPre);
            }
        }
        #endregion

        #region �޸��ܶ����Ѳɹ���
        /// <summary>
        /// �޸��ܶ����Ѳɹ���
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="statementType"></param>
        public void UpdateQtyPoSo(DataRow dr, StatementType statementType)
        {
            string _osId = "";
            string _osNo = "";
            string _osNoOriginal = "";
            int _itm = 0;
            int _itmOriginal = 0;
            decimal _qtyPre = 0;
            decimal _qtyPreOriginal = 0;
            string _prdNo = "";
            string _oldPrdNo = "";
            int _unit = 1;
            int _oldUnit = 1;
            _osId = "SO";
            if (statementType == StatementType.Insert)
            {
                _osId = dr["BIL_ID"].ToString();
                _osNo = dr["QT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                    _itm = Convert.ToInt32(dr["OTH_ITM"]);
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qtyPre = Convert.ToDecimal(dr["QTY"]);
                }
                _prdNo = dr["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    _unit = Convert.ToInt32(dr["UNIT"]);
            }
            else if (statementType == StatementType.Update)
            {
                _osId = dr["BIL_ID"].ToString();
                _osNo = dr["QT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["OTH_ITM"].ToString()))
                    _itm = Convert.ToInt32(dr["OTH_ITM"]);
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qtyPre = Convert.ToDecimal(dr["QTY"]);
                }
                _prdNo = dr["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                    _unit = Convert.ToInt32(dr["UNIT"]);

                _osNoOriginal = dr["QT_NO", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["OTH_ITM", System.Data.DataRowVersion.Original].ToString()))
                    _itmOriginal = Convert.ToInt32(dr["OTH_ITM", System.Data.DataRowVersion.Original]);
                if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                {
                    _qtyPreOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                }
                _oldPrdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    _oldUnit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
            }
            else if (statementType == StatementType.Delete)
            {
                _osId = dr["BIL_ID", System.Data.DataRowVersion.Original].ToString();
                _osNoOriginal = dr["QT_NO", System.Data.DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["OTH_ITM", System.Data.DataRowVersion.Original].ToString()))
                    _itmOriginal = Convert.ToInt32(dr["OTH_ITM", System.Data.DataRowVersion.Original]);
                if (!String.IsNullOrEmpty(dr["QTY", System.Data.DataRowVersion.Original].ToString()))
                {
                    _qtyPreOriginal = Convert.ToDecimal(dr["QTY", System.Data.DataRowVersion.Original]);
                }
                _oldPrdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                    _oldUnit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
            }
            if (_osId != "SO")
                return;
            DrpSO _so = new DrpSO();
            Hashtable _ht = new Hashtable();
            _ht["TableName"] = "TF_POS";
            _ht["IdName"] = "OS_ID";
            _ht["NoName"] = "OS_NO";
            _ht["ItmName"] = "PRE_ITM";
            _ht["OsID"] = _osId;
            if (_osNoOriginal.Length > 0)
            {
                _ht["OsNO"] = _osNoOriginal;
                _ht["KeyItm"] = _itmOriginal;
                _qtyPreOriginal = INVCommon.GetRtnQty(_oldPrdNo, (-1) * _qtyPreOriginal, _oldUnit, _ht);
                _so.UpdateClsId("PO", _osNoOriginal, "PRE_ITM", _itmOriginal, "QTY_PO", _qtyPreOriginal);
            }
            if (_osNo.Length > 0)
            {
                _ht["OsNO"] = _osNo;
                _ht["KeyItm"] = _itm;
                _qtyPre = INVCommon.GetRtnQty(_prdNo, _qtyPre, _unit, _ht);
                _so.UpdateClsId("PO", _osNo, "PRE_ITM", _itm, "QTY_PO", _qtyPre);
            }
        }
        #endregion


        #region �᰸
        /// <summary>
        /// ���ݵ��ݵĽ᰸�����᰸����Ӧ�ļӡ�������е��ܶ���
        /// </summary>
        ///<param name="osId"></param>
        /// <param name="osNo"></param>
        public void DoCloseBill(string osId, string osNo)
        {
            bool _isCloseIt = false;
            string _backId = "";
            if (osId == "PR")
                return;
            SunlikeDataSet _soDs = this.GetData(null, osId, osNo, false);
            DataTable _soMf = _soDs.Tables["MF_POS"];
            DataTable _soTf = _soDs.Tables["TF_POS"];
            DataTable _soTfBox = _soDs.Tables["TF_POS_BOX"];
            if (_soMf.Rows.Count > 0)
            {
                if (_soMf.Rows[0]["CLS_ID"].ToString() == "T")
                    _isCloseIt = true;
                _backId = _soMf.Rows[0]["BACK_ID"].ToString();
                if (_backId.Length > 0)
                    return;
                string _prdNo = "";
                string _prdMark = "";
                string _batNo = "";
                string _validDd = "";
                string _whNo = "";
                string _unit = "";
                decimal _qty = 0;
                decimal _qtyPre = 0;
                decimal _qtyPs = 0;
                decimal _qtyOnWay = 0;
                WH _wh = new WH();
                //�᰸					
                #region	��
                for (int i = 0; i < _soTf.Rows.Count; i++)
                {
                    // ��ʼ��
                    _qty = 0;
                    _qtyPre = 0;//�ɹ��˻�
                    _qtyPs = 0;//������

                    _prdNo = _soTf.Rows[i]["PRD_NO"].ToString();
                    _prdMark = _soTf.Rows[i]["PRD_MARK"].ToString();
                    _whNo = _soTf.Rows[i]["WH"].ToString();
                    _batNo = _soTf.Rows[i]["BAT_NO"].ToString();
                    _validDd = _soTf.Rows[i]["VALID_DD"].ToString();
                    _unit = _soTf.Rows[i]["UNIT"].ToString();
                    //����
                    if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY"].ToString()))
                    {
                        _qty = Convert.ToDecimal(_soTf.Rows[i]["QTY"]);
                    }
                    //�ɹ��˻���
                    if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_PRE"].ToString()))
                    {
                        _qtyPre = Convert.ToDecimal(_soTf.Rows[i]["QTY_PRE"]);
                    }
                    //�ѽ�����
                    if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_PS"].ToString()))
                    {
                        _qtyPs = Convert.ToDecimal(_soTf.Rows[i]["QTY_PS"]);
                    }
                    _qtyOnWay = _qty - _qtyPre - _qtyPs;//����-�ѽ�����-�ɹ��˻���
                    if (_qtyOnWay < 0)
                        _qtyOnWay = 0;

                    if (_qtyOnWay > 0)//�޸Ŀ���ܶ���
                    {
                        if (_isCloseIt)//�۳�ʣ���ܶ��� 
                        {
                            _qtyOnWay = (-1) * _qtyOnWay;
                        }
                        if (String.IsNullOrEmpty(_batNo))
                        {
                            _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, WH.QtyTypes.QTY_ON_WAY, _qtyOnWay);
                        }
                        else//���ſ��
                        {
                            Prdt _prdt = new Prdt();
                            SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                            Hashtable _ht = new Hashtable();
                            _ht[WH.QtyTypes.QTY_ON_WAY] = _qtyOnWay;
                            if (!String.IsNullOrEmpty(_validDd))
                            {

                                if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                                {
                                    TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                                    if (_timeSpan.Days > 0)
                                    {
                                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                                    }
                                    else
                                    {
                                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht);
                                    }
                                }
                                else
                                {
                                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht);
                                }
                            }
                            else
                            {
                                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                            }
                        }
                    }

                }
                #endregion

                #region ��

                for (int i = 0; i < _soTfBox.Rows.Count; i++)
                {
                    // ��ʼ��
                    _qty = 0;
                    _prdNo = _soTfBox.Rows[i]["PRD_NO"].ToString();
                    _whNo = _soTfBox.Rows[i]["WH"].ToString();
                    if (!String.IsNullOrEmpty(_soTfBox.Rows[i]["QTY"].ToString()))
                    {
                        _qtyOnWay = Convert.ToDecimal(_soTfBox.Rows[i]["QTY"]);
                    }
                    if (_qtyOnWay > 0)
                    {
                        if (_isCloseIt)//�۳�ʣ���ܶ��� 
                        {
                            _qtyOnWay = (-1) * _qtyOnWay;
                        }
                        //_wh.UpdateBoxQty(_prdNo,_whNo,_content,WH.BoxQtyTypes.QTY_ON_WAY,_qtyOnWay);
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="close"></param>
        /// <returns></returns>
        public string DoCloseBill(string osId, string osNo, bool close)
        {
            bool _isCloseIt = false;
            string _backId = "";
            string _result = "";
            SunlikeDataSet _soDs = this.GetData(null, osId, osNo, false);
            DataTable _soMf = _soDs.Tables["MF_POS"];
            DataTable _soTf = _soDs.Tables["TF_POS"];
            DataTable _soTfBox = _soDs.Tables["TF_POS_BOX"];
            if (_soMf.Rows.Count > 0)
            {
                if (_soMf.Rows[0]["CLS_ID"].ToString() == "T")
                    _isCloseIt = true;
                if (close == _isCloseIt)
                {
                    if (close)
                    {
                        return "RCID=COMMON.HINT.PRDTCONTENTERROR,PARAM=" + osNo;//�õ���[{0}]�ѽ᰸,�᰸�����������!
                    }
                    else
                    {
                        return "RCID=COMMON.HINT.CLOSEERROR1,PARAM=" + osNo;//�õ���[{0}]δ�᰸,δ�᰸�����������!
                    }
                }
                _backId = _soMf.Rows[0]["BACK_ID"].ToString();
                if (_backId.Length > 0)
                    return "RCID=COMMON.HINT.CLOSEERROR2,PARAM=" + osNo;//�õ���[{0}]�Ƿ��ֹ��᰸��
                string _prdNo = "";
                string _prdMark = "";
                string _batNo = "";
                string _validDd = "";
                string _whNo = "";
                string _unit = "";
                decimal _qty = 0;
                decimal _qtyPre = 0;
                decimal _qtyPs = 0;
                decimal _qtyOnWay = 0;
                WH _wh = new WH();
                try
                {
                    //�᰸					
                    #region	��
                    for (int i = 0; i < _soTf.Rows.Count; i++)
                    {
                        // ��ʼ��
                        _qty = 0;
                        _qtyPre = 0;//�ɹ��˻�
                        _qtyPs = 0;//������

                        _prdNo = _soTf.Rows[i]["PRD_NO"].ToString();
                        _prdMark = _soTf.Rows[i]["PRD_MARK"].ToString();
                        _whNo = _soTf.Rows[i]["WH"].ToString();
                        _batNo = _soTf.Rows[i]["BAT_NO"].ToString();
                        _validDd = _soTf.Rows[i]["VALID_DD"].ToString();
                        _unit = _soTf.Rows[i]["UNIT"].ToString();
                        //����
                        if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY"].ToString()))
                        {
                            _qty = Convert.ToDecimal(_soTf.Rows[i]["QTY"]);
                        }
                        //�ɹ��˻���
                        if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_PRE"].ToString()))
                        {
                            _qtyPre = Convert.ToDecimal(_soTf.Rows[i]["QTY_PRE"]);
                        }
                        //�ѽ�����
                        if (!String.IsNullOrEmpty(_soTf.Rows[i]["QTY_PS"].ToString()))
                        {
                            _qtyPs = Convert.ToDecimal(_soTf.Rows[i]["QTY_PS"]);
                        }
                        _qtyOnWay = _qty - _qtyPre - _qtyPs;//����-�ѽ�����-�ɹ��˻���
                        if (_qtyOnWay < 0)
                            _qtyOnWay = 0;

                        if (_qtyOnWay > 0)//�޸Ŀ���ܶ���
                        {
                            if (close)//�۳�ʣ���ܶ��� 
                            {
                                _qtyOnWay = (-1) * _qtyOnWay;
                            }
                            if (String.IsNullOrEmpty(_batNo))
                            {
                                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, WH.QtyTypes.QTY_ON_WAY, _qtyOnWay);
                            }
                            else//���ſ��
                            {
                                Prdt _prdt = new Prdt();
                                SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                                Hashtable _ht = new Hashtable();
                                _ht[WH.QtyTypes.QTY_ON_WAY] = _qtyOnWay;
                                if (!String.IsNullOrEmpty(_validDd))
                                {

                                    if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                                    {
                                        TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                                        if (_timeSpan.Days > 0)
                                        {
                                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                                        }
                                        else
                                        {
                                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht);
                                        }
                                    }
                                    else
                                    {
                                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, _validDd, _unit, _ht);
                                    }
                                }
                                else
                                {
                                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo, "", _unit, _ht);
                                }
                            }
                        }

                    }
                    #endregion

                    #region ��

                    for (int i = 0; i < _soTfBox.Rows.Count; i++)
                    {
                        // ��ʼ��
                        _qty = 0;
                        _prdNo = _soTfBox.Rows[i]["PRD_NO"].ToString();
                        _whNo = _soTfBox.Rows[i]["WH"].ToString();
                        if (!String.IsNullOrEmpty(_soTfBox.Rows[i]["QTY"].ToString()))
                        {
                            _qtyOnWay = Convert.ToDecimal(_soTfBox.Rows[i]["QTY"]);
                        }
                        if (_qtyOnWay > 0)
                        {
                            if (close)//�۳�ʣ���ܶ��� 
                            {
                                _qtyOnWay = (-1) * _qtyOnWay;
                            }
                            //_wh.UpdateBoxQty(_prdNo,_whNo,_content,WH.BoxQtyTypes.QTY_ON_WAY,_qtyOnWay);
                        }
                    }
                    #endregion
                    //���Ͻ᰸���
                    DoClosePO("PO", osNo, close);
                }
                catch (Exception _ex)
                {
                    _result = _ex.Message.ToString();
                }
            }
            return _result;
        }
        /// <summary>
        /// �᰸
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="close"></param>
        public void DoClosePO(string osId, string osNo, bool close)
        {
            DbDRPPO _po = new DbDRPPO(Comp.Conn_DB);
            _po.DoCloseSO(osId, osNo, close);
        }
        #endregion

        #endregion

        #region ��鵥���Ƿ�����޸�
        /// <summary>
        /// ��鵥���Ƿ�����޸�
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="bCheckAuditing">�Ƿ��ж��������</param>
        private string SetCanModify(SunlikeDataSet ds, bool bCheckAuditing)
        {
            DataTable _dtMf = ds.Tables["MF_POS"];
            DataTable _dtTf = ds.Tables["TF_POS"];
            string _osId = "";
            string errorMsg = "";


            bool _bCanModify = true;
            if (_dtMf.Rows.Count > 0)
            {
                //ȡ�õ��ݱ�
                _osId = _dtMf.Rows[0]["OS_ID"].ToString();
                //if (_dtMf.Rows[0]["BIL_TYPE"].ToString() != "FX")
                //{
                //    _bCanModify = false;
                //    errorMsg += "COMMON.HINT.NOFX";//���Ƿ������ݣ������޸�
                //}
                //�жϹ�����
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["OS_DD"]), _dtMf.Rows[0]["PO_DEP"].ToString(), "CLS_INV"))
                {
                    _bCanModify = false;
                    errorMsg += "COMMON.HINT.ACCCLOSE";
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.ACCCLOSE");

                }
                //�ж��������
                if (bCheckAuditing)
                {
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_dtMf.Rows[0]["OS_ID"].ToString(), _dtMf.Rows[0]["OS_NO"].ToString()))
                    {
                        _bCanModify = false;
                        errorMsg += "COMMON.HINT.INTOAUT";
                        ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_AUDIT");
                    }
                }
                //�ж��Ƿ�᰸
                //if (_bCanModify)
                {
                    if (_dtMf.Rows[0]["CLS_ID"].ToString() == "T")
                    {
                        _bCanModify = false;
                        ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_MODIFY");
                        errorMsg += "DRPYI.ISCLOSE";
                    }
                }
                //�ж��Ƿ�����
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _bCanModify = false;
                    ////Common.SetCanModifyRem(ds, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
                if (_osId == "PO")
                {
                    decimal _qtyPo = 0;
                    //�ж��ѽ���
                    //if (_bCanModify)
                    {
                        _qtyPo = 0;
                        decimal _qtyPs = 0;
                        bool _isCompletePs = true;
                        DataRow[] _aryDr = _dtTf.Select();
                        if (_aryDr.Length == 0)//��ֻ�޸ı�ͷ����ʱ���������Ϣ�������changeDS�У���Ӧ�ж�û�б�������
                            _isCompletePs = false;
                        for (int i = 0; i < _aryDr.Length; i++)
                        {
                            _qtyPo = 0;
                            _qtyPs = 0;
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY"].ToString()))
                            {
                                _qtyPo = Convert.ToDecimal(_aryDr[i]["QTY"]);
                            }
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY_PS"].ToString()))
                            {
                                _qtyPs = Convert.ToDecimal(_aryDr[i]["QTY_PS"]);
                            }
                            if (_qtyPo > _qtyPs || _qtyPs == 0)
                            {
                                _isCompletePs = false;
                                break;
                            }
                        }
                        if (_isCompletePs)
                        {
                            _bCanModify = false;
                            errorMsg += "DRPPO.PS_IN";//�Ѿ�����
                            ////Common.SetCanModifyRem(ds, "RCID=UNKNOWN.DRPPO.PS_IN");
                        }
                    }  //�ж��ѽ���
                    //�ж��Ѳɹ��˻�
                    //if (_bCanModify)
                    {
                        _qtyPo = 0;
                        decimal _qtyPre = 0;
                        bool _isCompletePre = true;
                        DataRow[] _aryDr = _dtTf.Select();
                        if (_aryDr.Length == 0)//��ֻ�޸ı�ͷ����ʱ���������Ϣ�������changeDS�У���Ӧ�ж�û�б�������
                            _isCompletePre = false;
                        for (int i = 0; i < _aryDr.Length; i++)
                        {
                            _qtyPo = 0;
                            _qtyPre = 0;
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY"].ToString()))
                            {
                                _qtyPo = Convert.ToDecimal(_aryDr[i]["QTY"]);
                            }
                            if (!String.IsNullOrEmpty(_aryDr[i]["QTY_PRE"].ToString()))
                            {
                                _qtyPre = Convert.ToDecimal(_aryDr[i]["QTY_PRE"]);
                            }
                            if (_qtyPo > _qtyPre || _qtyPre == 0)
                            {
                                _isCompletePre = false;
                                break;
                            }
                        }
                        if (_isCompletePre)
                        {
                            _bCanModify = false;
                            errorMsg += "DRPPO.PR_IN";//�Ѿ��ɹ��˻�
                            ////Common.SetCanModifyRem(ds, "RCID=UNKNOWN.DRPPO.PR_IN");
                        }
                    }// end �ж��Ѳɹ��˻�

                    //if (_bCanModify)
                    {
                        CompInfo _compInfo = Comp.GetCompInfo("");
                        if (_compInfo.SystemInfo.CHK_DRP1)
                        {
                            Cust _cust = new Cust();
                            if (_cust.IsNeedAffirm(_dtMf.Rows[0]["CUS_NO"].ToString(), "CHK_DRP1"))
                            {
                                bool _isCompleteCfm = true;
                                DataRow[] _aryDr = _dtTf.Select();
                                if (_aryDr.Length == 0)//��ֻ�޸ı�ͷ����ʱ���������Ϣ�������changeDS�У���Ӧ�ж�û�б�������
                                    _isCompleteCfm = false;
                                for (int i = 0; i < _aryDr.Length; i++)
                                {
                                    if (String.IsNullOrEmpty(_aryDr[i]["SCM_USR"].ToString()))
                                    {
                                        _isCompleteCfm = false;
                                    }
                                }

                                if (_isCompleteCfm)
                                {
                                    _bCanModify = false;
                                    errorMsg += "DRPPO.COMPLETE_CFM";//�Ѿ�ȫ��ȷ��
                                    ////Common.SetCanModifyRem(ds, "RCID=DRPPO.COMPLETE_CFM");
                                }
                            }
                        }
                    }
                }//end if (_osId == "PO") 
            }
            ds.ExtendedProperties["CAN_MODIFY"] = _bCanModify.ToString().Substring(0, 1);
            return errorMsg;
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
            try
            {

                Sunlike.Business.Data.DbDRPPO _dbPo = new DbDRPPO(Comp.Conn_DB);
                SunlikeDataSet _poDs = _dbPo.GetDataPO(bil_id, bil_no, false);
                #region �Ƿ��Ѿ��᰸
                DataTable _mfTable = _poDs.Tables["MF_POS"];
                if (_mfTable.Rows.Count > 0)
                {
                    if (_mfTable.Rows[0]["CLS_ID"].ToString() == "T")
                    {
                        throw new SunlikeException(/*�Ѿ��᰸�����ܷ���ˣ�*/ "RCID=INV.HINT.HASCLS");
                    }
                }

                #endregion

                DataTable _tfTable = _poDs.Tables["TF_POS"];

                if (bil_id == "PR")
                {
                    int _poEstItm = 0;
                    string _poOsNo = "";

                    for (int i = 0; i < _tfTable.Rows.Count; i++)
                    {
                        int _preItm = 0;
                        if (!string.IsNullOrEmpty(_tfTable.Rows[i]["PRE_ITM"].ToString()))
                        {
                            _preItm = Convert.ToInt32(_tfTable.Rows[i]["PRE_ITM"]);
                        }
                        SunlikeDataSet _dsPo = GetBody(_tfTable.Rows[i]["QT_NO"].ToString(), "PO", "PRE_ITM", _preItm, false);
                        if (_dsPo.Tables[0].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_dsPo.Tables[0].Rows[0]["EST_ITM"].ToString()))
                            {
                                _poEstItm = Convert.ToInt16(_dsPo.Tables[0].Rows[0]["EST_ITM"]);
                            }
                            _poOsNo = _dsPo.Tables[0].Rows[0]["OS_NO"].ToString();

                            SunlikeDataSet _dsJD = GetJDData(_poOsNo, "PO", _poEstItm);
                            if (_dsJD.Tables[0].Rows.Count > 0)
                            {
                                throw new SunlikeException("�Ѳ�����������");
                            }
                        }
                    }
                }

                #region �޸���;��
                //DataTable _tfTable = _poDs.Tables["TF_POS"];
                for (int i = 0; i < _tfTable.Rows.Count; i++)
                {
                    this.UpdateTf(bil_id, _tfTable.Rows[i], System.Data.StatementType.Insert);
                    if (CaseInsensitiveComparer.Default.Compare(bil_id, "PR") == 0)
                    {
                        this.UpdateQtyPre(_tfTable.Rows[i], System.Data.StatementType.Insert);
                    }
                    if (CaseInsensitiveComparer.Default.Compare(bil_id, "PO") == 0)
                    {
                        if (CaseInsensitiveComparer.Default.Compare(_tfTable.Rows[i]["BIL_ID"].ToString(), "QS") == 0)
                            this.UpdateQtyPo(_tfTable.Rows[i], System.Data.StatementType.Insert);
                        else if (CaseInsensitiveComparer.Default.Compare(_tfTable.Rows[i]["BIL_ID"].ToString(), "SO") == 0)
                            this.UpdateQtyPoSo(_tfTable.Rows[i], System.Data.StatementType.Insert);
                    }
                }
                DataTable _tfBoxTable = _poDs.Tables["TF_POS_BOX"];
                for (int i = 0; i < _tfBoxTable.Rows.Count; i++)
                {
                    this.UpdateTfBox(bil_id, _tfBoxTable.Rows[i], System.Data.StatementType.Insert);
                }
                #endregion
                _dbPo.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
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
            // TODO:  Add DRPPO.Deny implementation
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
            return RollBack(bil_id, bil_no, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="isUpdateHead"></param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no, bool isUpdateHead)
        {
            string _errmsg = "";
            try
            {
                Sunlike.Business.Data.DbDRPPO _dbPo = new DbDRPPO(Comp.Conn_DB);
                SunlikeDataSet _poDs = _dbPo.GetDataPO(bil_id, bil_no, false);
                GetData(bil_id, bil_no, false);
                string _errorMsg = this.SetCanModify(_poDs, false);
                if (_errorMsg != "COMMON.HINT.NOFX" && _poDs.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
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
                #region �Ƿ��Ѿ��᰸
                DataTable _mfTable = _poDs.Tables["MF_POS"];
                if (_mfTable.Rows.Count > 0)
                {
                    if (_mfTable.Rows[0]["CLS_ID"].ToString() == "T")
                    {
                        throw new SunlikeException(/*�Ѿ��᰸�����ܷ���ˣ�*/ "RCID=INV.HINT.HASCLS");
                    }
                }

                #endregion

                DataTable _tfTable = _poDs.Tables["TF_POS"];
                if (bil_id == "PO")
                {
                    Cust _cust = new Cust();
                    if (_cust.IsNeedAffirm(_mfTable.Rows[0]["CUS_NO"].ToString(), "CHK_DRP1"))
                    {
                        if (_tfTable.Select("ISNULL(SCM_USR,'')<>''").Length > 0)
                        {
                            throw new SunlikeException(/*����ȷ���������ܷ���ˣ�*/ "RCID=INV.HINT.HASCFM");
                        }
                    }


                    foreach (DataRow _dr in _tfTable.Rows)
                    {
                        int _estItm = 0;
                        if (!string.IsNullOrEmpty(_dr["EST_ITM"].ToString()))
                        {
                            _estItm = Convert.ToInt32(_dr["EST_ITM"]);
                        }
                        using (SunlikeDataSet _dsJD = GetJDData(_dr["OS_NO"].ToString(), _dr["OS_ID"].ToString(), _estItm))
                        {
                            if (_dsJD.Tables[0].Rows.Count > 0)
                            {
                                if (_dsJD.Tables[0].Rows[0]["STATUS_JD"].ToString() == "Y")
                                {
                                    throw new SunlikeException(/*���нӵ������ܷ���ˣ�*/ "RCID=INV.HINT.JD_MODIFY_DENIED");
                                }
                            }
                        }
                    }
                }


                #region �޸���;��

                for (int i = 0; i < _tfTable.Rows.Count; i++)
                {
                    decimal _qtyPre = 0;
                    decimal _qtyPs = 0;
                    decimal _qtyPo = 0;
                    if (CaseInsensitiveComparer.Default.Compare(bil_id, "PO") == 0)
                    {
                        //�ж��Ƿ����вɹ��˻�
                        if (!(_tfTable.Rows[i]["QTY_PRE"] is System.DBNull))
                        {
                            _qtyPre += Convert.ToDecimal(_tfTable.Rows[i]["QTY_PRE"]);
                            if (_qtyPre > 0)
                            {
                                throw new SunlikeException(/*���вɹ��˻أ����ܷ���ˣ�*/ "RCID=INV.HINT.HASPRE");
                            }
                        }
                        //�ж��Ƿ����
                        if (!(_tfTable.Rows[i]["QTY_PS"] is System.DBNull))
                        {
                            _qtyPs += Convert.ToDecimal(_tfTable.Rows[i]["QTY_PS"]);
                            if (_qtyPs > 0)
                            {
                                throw new SunlikeException(/*���н��������ܷ���ˣ�*/ "RCID=INV.HINT.HASPS");
                            }
                        }

                        if (!(_tfTable.Rows[i]["QTY_PO"] is System.DBNull))
                        {
                            _qtyPo += Convert.ToDecimal(_tfTable.Rows[i]["QTY_PO"]);
                        }

                        if (!(_tfTable.Rows[i]["QTY_PO_UNSH"] is System.DBNull))
                        {
                            _qtyPo += Convert.ToDecimal(_tfTable.Rows[i]["QTY_PO_UNSH"]);
                        }
                        if (_qtyPo > 0)
                        {
                            throw new SunlikeException(/*���вɹ������ܷ���ˣ�*/ "INV.HINT.HASPO");
                        }

                    }
                    _tfTable.Rows[i].Delete();
                    this.UpdateTf(bil_id, _tfTable.Rows[i], System.Data.StatementType.Delete);
                    if (CaseInsensitiveComparer.Default.Compare(bil_id, "PR") == 0)
                        this.UpdateQtyPre(_tfTable.Rows[i], System.Data.StatementType.Delete);
                    if (CaseInsensitiveComparer.Default.Compare(bil_id, "PO") == 0)
                    {
                        if (CaseInsensitiveComparer.Default.Compare(_tfTable.Rows[i]["BIL_ID", DataRowVersion.Original].ToString(), "QS") == 0)
                            this.UpdateQtyPo(_tfTable.Rows[i], System.Data.StatementType.Delete);
                        else if (CaseInsensitiveComparer.Default.Compare(_tfTable.Rows[i]["BIL_ID", DataRowVersion.Original].ToString(), "SO") == 0)
                            this.UpdateQtyPoSo(_tfTable.Rows[i], System.Data.StatementType.Delete);
                    }
                }
                DataTable _tfBoxTable = _poDs.Tables["TF_POS_BOX"];
                for (int i = 0; i < _tfBoxTable.Rows.Count; i++)
                {
                    _tfBoxTable.Rows[i].Delete();
                    this.UpdateTfBox(bil_id, _tfBoxTable.Rows[i], System.Data.StatementType.Delete);
                }
                #endregion
                if (isUpdateHead)
                {
                    //�޸����״̬
                    _dbPo.UpdateChkMan(bil_id, bil_no, "", Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat)));
                }
            }
            catch (Exception _ex)
            {
                _errmsg = _ex.Message.ToString();
            }
            return _errmsg;
        }
        #endregion

        #region �ж��Ƿ��ֹ��᰸
        /// <summary>
        /// �ж��Ƿ��ֹ��᰸
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <returns></returns>
        public bool IsCloseByUsr(string osId, string osNo)
        {
            DbDRPPO _dbPo = new DbDRPPO(Comp.Conn_DB);
            return _dbPo.IsCloseByUsr(osId, osNo);
        }
        #endregion

        #region ICloseBill Members
        /// <summary>
        /// ���᰸
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

        /// <summary>
        /// �᰸
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="cls_name"></param>
        /// <returns></returns>
        string Sunlike.Business.ICloseBill.DoCloseBill(string bil_id, string bil_no, string cls_name)
        {
            string _error = "";
            if (cls_name == "CLS_ID")
            {
                _error = this.DoCloseBill(bil_id, bil_no, true);
            }
            return _error;
        }

        #endregion

        #region ��д�Ѳɹ��������δ�ɹ������
        /// <summary>
        ///  ��д�Ѳɹ������
        /// </summary>
        /// <param name="osId">�ɹ����ݱ�</param>
        /// <param name="osNo">�ɹ�����</param>
        /// <param name="unit">��Ʒ����</param>
        /// <param name="itm">EST_ITM���</param>
        /// <param name="qty">����</param>
        public void UpdateQtyPo(string osId, string osNo, int itm, string unit, decimal qty)
        {
            this.UpdateQty(osId, osNo, "EST_ITM", itm, unit, "QTY_PO", qty);
        }
        /// <summary>
        ///  ��дδ�ɹ������
        /// </summary>
        /// <param name="osId">�ɹ����ݱ�</param>
        /// <param name="osNo">�ɹ�����</param>
        /// <param name="unit">��Ʒ����</param>
        /// <param name="itm">EST_ITM���</param>
        /// <param name="qty">����</param>
        public void UpdateQtyPoUnsh(string osId, string osNo, int itm, string unit, decimal qty)
        {
            this.UpdateQty(osId, osNo, "EST_ITM", itm, unit, "QTY_PO_UNSH", qty);
        }
        private void UpdateQty(string osId, string osNo, string itmColumnName, int itm, string unit, string qtyColumnName, decimal qty)
        {
            string _prdNo = "";
            String _unitPo = "";
            SunlikeDataSet _dsPo = this.GetBody(osId, osNo, itmColumnName, itm, false);
            if (_dsPo != null && _dsPo.Tables["TF_POS"].Rows.Count > 0)
            {
                _prdNo = _dsPo.Tables["TF_POS"].Rows[0]["PRD_NO"].ToString();
                _unitPo = _dsPo.Tables["TF_POS"].Rows[0]["UNIT"].ToString();
            }
            //����
            Prdt _prdt = new Prdt();
            //���㵥λ
            decimal _qtyNew = _prdt.GetUnitQty(_prdNo, unit, qty, _unitPo);
            DbDRPPO _po = new DbDRPPO(Comp.Conn_DB);
            _po.UpdateQty(osId, osNo, itmColumnName, itm, qtyColumnName, qty);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="osNo"></param>
        /// <param name="osId"></param>
        /// <param name="estItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetJDData(string osNo, string osId, int estItm)
        {
            Sunlike.Business.Data.DbDRPPO _dbDrpPo = new DbDRPPO(Comp.Conn_DB);
            return _dbDrpPo.GetJDData(osNo, osId, estItm);
        }

        public void DeleteTfJD(string osNo, string osId, int estItm)
        {
            Sunlike.Business.Data.DbDRPPO _dbDrpPo = new DbDRPPO(Comp.Conn_DB);
            _dbDrpPo.DeleteTfJD(osNo, osId, estItm);
        }
    }
}
