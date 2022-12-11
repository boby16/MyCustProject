/*
 * modify: cjc 090805 
 * ������̵ĸĶ�(���ݱ�����ģ��)
 */

using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections;
using System.Collections.Generic;

namespace Sunlike.Business
{
    /// <summary>
    /// Summary description for DRPIJ.
    /// </summary>
    public class DRPIJ : BizObject, IAuditing
    {
        /// <summary>
        /// �Ƿ���������ƾ֤
        /// </summary>
        private bool _reBuildVohNo = false;
        private bool _isRunAuditing;
        private bool _auditBarCode;
        private int _bodyNo;
        private int _barCodeNo;

        /// <summary>
        /// ������
        /// </summary>
        public DRPIJ()
        {
        }

        /// <summary>
        /// ȡ�õ�������
        /// </summary>
        /// <param name="usr">��ǰ�û�</param>
        /// <param name="IjID">���ݱ�</param>
        /// <param name="IjNo">���ݺ���</param>
        /// <param name="OnlyFillSchema">�Ƿ�ֻ��ȡSchema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string usr, string IjID, string IjNo, bool OnlyFillSchema)
        {
            DbDRPIJ _ij = new DbDRPIJ(Comp.Conn_DB);
            SunlikeDataSet _ds = _ij.GetData(IjID, IjNo, OnlyFillSchema);
            _ds.Tables["TF_IJ"].Columns["QTY_SQ_ORG"].ReadOnly = false;
            _ds.Tables["TF_IJ"].Columns["QTY_SQ_ORG1"].ReadOnly = false;
            _ds.Tables["TF_IJ"].Columns["KEY_ITMTF"].ReadOnly = false;
            #region ��Բ�ֳ������ ��Ҫѭ��2��
            Dictionary<string, decimal> di = new Dictionary<string, decimal>();
            foreach (DataRow dr in _ds.Tables["TF_IJ"].Select())
            {
                string key = Convert.ToString(dr["SQ_ID"]) + Convert.ToString(dr["SQ_NO"]) + Convert.ToString(dr["SQ_ITM"]);
                if (!di.ContainsKey(key))
                    di.Add(key, Convert2Decimal(dr["QTY_SQ_ORG"]));
                else
                    di[key] -= Convert2Decimal(dr["QTY"]);//QTYΪ����
            }
            foreach (DataRow dr in _ds.Tables["TF_IJ"].Select())
            {
                string key = Convert.ToString(dr["SQ_ID"]) + Convert.ToString(dr["SQ_NO"]) + Convert.ToString(dr["SQ_ITM"]);
                dr["QTY_SQ_ORG"] = di[key];
            }
            #endregion
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;
            }
            //���ӵ���Ȩ��
            if (!OnlyFillSchema)
            {
                if (usr != null && !String.IsNullOrEmpty(usr))
                {
                    string _pgm = "DRP" + IjID;
                    DataTable _dtMf = _ds.Tables["MF_IJ"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                        _ds.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
            }
            //����������кŵ��ݴ��
            DataTable _dt = new DataTable("BAR_COLLECT");
            _dt.Columns.Add("ITEM", typeof(int));
            _dt.Columns.Add("BAR_CODE");
            _dt.Columns.Add("BAT_NO");
            _dt.Columns.Add("BOX_NO");
            _dt.Columns.Add("PRD_NO");
            _dt.Columns.Add("PRD_MARK");
            //�����λ
            _dt.Columns.Add("WH1");
            _dt.Columns.Add("SERIAL_NO");
            //���кſ�λ������
            _dt.Columns.Add("ISEXIST");
            _dt.Columns.Add("WH_REC");
            _dt.Columns.Add("BAT_NO_REC");
            _dt.Columns.Add("STOP_ID");
            _dt.Columns.Add("PH_FLAG");
            DataColumn[] _dca = new DataColumn[1];
            _dca[0] = _dt.Columns["ITEM"];
            _dt.PrimaryKey = _dca;
            _ds.Tables.Add(_dt);
            //�趨����KeyItmΪ�Զ�����
            DataTable _dtBody = _ds.Tables["TF_IJ"];
            _dtBody.Columns["KEY_ITM"].AutoIncrement = true;
            _dtBody.Columns["KEY_ITM"].AutoIncrementSeed = _dtBody.Rows.Count > 0 ? Convert.ToInt32(_dtBody.Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _dtBody.Columns["KEY_ITM"].AutoIncrementStep = 1;
            _dtBody.Columns["KEY_ITM"].Unique = true;
            _dtBody.Columns.Add("PRD_NO_NO", typeof(System.String));
            //�̵㵥������� by zb
            _dtBody.Columns.Add("PT_ITM", typeof(int));
            _dtBody.Columns.Add("UNIT_DP", typeof(String));
            //����ӵ�λ��׼�ɱ�
            _dtBody.Columns.Add(new DataColumn("CST_STD_UNIT", typeof(decimal)));
            //�趨������KeyItmΪ�Զ�����
            DataTable _dtBox = _ds.Tables["TF_IJ_BOX"];
            _dtBox.Columns["KEY_ITM"].AutoIncrement = true;
            _dtBox.Columns["KEY_ITM"].AutoIncrementSeed = _dtBox.Rows.Count > 0 ? Convert.ToInt32(_dtBox.Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
            _dtBox.Columns["KEY_ITM"].AutoIncrementStep = 1;
            _dtBox.Columns["KEY_ITM"].Unique = true;
            _dtBox.Columns.Add("PRD_NO_NO", typeof(System.String));
            //�жϵ����ܷ��޸�
            this.SetCanModify(_ds, true, false);
            //�趨����������
            _dtBody.Columns["PLUS_FLAG"].DefaultValue = "+";
            _dtBody.Columns["PLUS_FLAG"].ReadOnly = false;
            _dtBox.Columns["PLUS_FLAG"].DefaultValue = "+";
            _dtBox.Columns["PLUS_FLAG"].ReadOnly = false;
            //����������
            DataColumn _dc = new DataColumn("QTY_SHOW", typeof(decimal));
            _dtBody.Columns.Add(_dc);
            _dc = new DataColumn("CST_SHOW", typeof(decimal));
            _dtBody.Columns.Add(_dc);
            _dc = new DataColumn("QTY1_SHOW", typeof(decimal));
            _dtBody.Columns.Add(_dc);
            for (int i = 0; i < _dtBody.Rows.Count; i++)
            {
                //����λ��׼�ɱ���ֵ
                if (!String.IsNullOrEmpty(_dtBody.Rows[i]["CST_STD"].ToString()) && !String.IsNullOrEmpty(_dtBody.Rows[i]["QTY"].ToString()) && Convert.ToDecimal(_dtBody.Rows[i]["QTY"]) != 0)
                    _dtBody.Rows[i]["CST_STD_UNIT"] = Convert.ToDecimal(_dtBody.Rows[i]["CST_STD"]) / Convert.ToDecimal(_dtBody.Rows[i]["QTY"]);
                else
                    _dtBody.Rows[i]["CST_STD_UNIT"] = 0;

                decimal _cst = 0;
                if (!String.IsNullOrEmpty(_dtBody.Rows[i]["CST"].ToString()))
                {
                    _cst = Convert.ToDecimal(_dtBody.Rows[i]["CST"]);
                }
                decimal _qty = 1;
                decimal _qty1 = 1;
                if (!String.IsNullOrEmpty(_dtBody.Rows[i]["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY"]);
                }
                if (!String.IsNullOrEmpty(_dtBody.Rows[i]["QTY1"].ToString()))
                {
                    _qty1 = Convert.ToDecimal(_dtBody.Rows[i]["QTY1"]);
                }
                if (_qty < 0)
                {
                    _dtBody.Rows[i]["PLUS_FLAG"] = "-";
                    _qty = System.Math.Abs(_qty);
                    _qty1 = System.Math.Abs(_qty1);
                    _cst = System.Math.Abs(_cst);
                }
                _dtBody.Rows[i]["QTY_SHOW"] = _qty;
                _dtBody.Rows[i]["QTY1_SHOW"] = _qty1;
                _dtBody.Rows[i]["CST_SHOW"] = _cst;
                _dtBody.Rows[i]["PRD_NO_NO"] = _dtBody.Rows[i]["PRD_NO"];
            }
            //���������������
            _dc = new DataColumn("CONTENT_DSC");
            _dtBox.Columns.Add(_dc);
            //����������
            _dc = new DataColumn("QTY_SHOW", typeof(decimal));
            _dtBox.Columns.Add(_dc);
            //ȡ������Ϣ
            BarBox _box = new BarBox();
            for (int i = 0; i < _dtBox.Rows.Count; i++)
            {
                DataRow _drBox = _dtBox.Rows[i];
                _drBox["CONTENT_DSC"] = _box.GetBar_BoxDsc(_drBox["PRD_NO"].ToString(), _drBox["CONTENT"].ToString());
                decimal _qty = 0;
                if (!String.IsNullOrEmpty(_drBox["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(_drBox["QTY"]);
                }
                if (_qty < 0)
                {
                    _drBox["PLUS_FLAG"] = "-";
                    _qty = System.Math.Abs(_qty);
                }
                _drBox["QTY_SHOW"] = _qty;
            }
            //�����кŵļ�¼ת���ݴ����
            DataView _dv = new DataView(_ds.Tables["TF_IJ_BAR"]);
            _dv.Sort = "BOX_NO,BAR_CODE";
            for (int i = 0; i < _dv.Count; i++)
            {
                string _barCode = _dv[i]["BAR_CODE"].ToString();
                DataRow _dr = _dt.NewRow();
                _dr["ITEM"] = _dt.Rows.Count + 1;
                _dr["BAR_CODE"] = _barCode;
                if (!String.IsNullOrEmpty(_dv[i]["BOX_NO"].ToString()))
                {
                    _dr["BOX_NO"] = _dv[i]["BOX_NO"];
                }
                BarCode.BarInfo _barInfo = BarCode.GetBarInfo(_barCode);
                _dr["PRD_NO"] = _barInfo.Prd_No;
                _dr["PRD_MARK"] = _barInfo.Prd_Mark;
                _dr["SERIAL_NO"] = _barInfo.Serial_No;
                _dt.Rows.Add(_dr);
            }
            //ȡ���кſ�λ������
            foreach (DataRow dr in _dt.Rows)
            {
                DataRow[] _aryDrBar = _ds.Tables["TF_IJ_BAR"].Select("BAR_CODE='" + dr["BAR_CODE"].ToString() + "'");
                if (_aryDrBar.Length > 0)
                {
                    DataRow[] _aryDr = _ds.Tables["TF_IJ"].Select("KEY_ITM=" + _aryDrBar[0]["IJ_ITM"].ToString());
                    if (_aryDr.Length > 0)
                    {
                        dr["WH1"] = _aryDr[0]["WH"];
                        dr["BAT_NO"] = _aryDr[0]["BAT_NO"];
                    }
                }
            }
            _dv.Dispose();
            GC.Collect(GC.GetGeneration(_dv));
            //�������ԭ���QueryWin��
            DataTable _dtQuery = _ij.GetIjReasonData().Copy();
            _ds.Tables.Add(_dtQuery);
            _ds.AcceptChanges();
            return _ds;
        }

        #region *ǿ��װ��
        private decimal Convert2Decimal(object p)
        {
            decimal _d = 0;
            if (!decimal.TryParse(Convert.ToString(p), out _d))
                _d = 0;
            return _d;
        }
        #endregion

        //�жϵ����ܷ��޸�
        private string SetCanModify(SunlikeDataSet ChangedDS, bool IsCheckAuditing, bool IsRollBack)
        {
            bool _canModify = true;
            bool _isCls = true;
            if (ChangedDS.Tables["MF_IJ"].Rows.Count > 0)
            {
                //if (ChangedDS.Tables["MF_IJ"].Rows[0]["BIL_TYPE"].ToString() != "FX" && !IsRollBack)
                //{
                //    _canModify = false;
                //}

                //�ж��Ƿ����
                if (Comp.HasCloseBill(Convert.ToDateTime(ChangedDS.Tables["MF_IJ"].Rows[0]["IJ_DD"]), ChangedDS.Tables["MF_IJ"].Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    _canModify = false;
                    ////Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_CLS");/*�ѹ��˲����޸�*/
                }
                //�ж��Ƿ�����
                if (!String.IsNullOrEmpty(ChangedDS.Tables["MF_IJ"].Rows[0]["LOCK_MAN"].ToString()))
                {
                    _canModify = false;
                    ////Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_LOCK");/*���������޸�*/
                }
                //����Դ���������޸�
                if (!String.IsNullOrEmpty(ChangedDS.Tables["MF_IJ"].Rows[0]["BIL_NO"].ToString()))
                {
                    _canModify = false;                   
                }
                if (IsCheckAuditing && _canModify)/*�Ƿ�����������*/
                {
                    Auditing _auditing = new Auditing();
                    string _ijID = ChangedDS.Tables["MF_IJ"].Rows[0]["IJ_ID"].ToString();
                    string _ijNo = ChangedDS.Tables["MF_IJ"].Rows[0]["IJ_NO"].ToString();
                    if (_auditing.GetIfEnterAuditing(_ijID, _ijNo))
                    {
                        _canModify = false;
                        ////Common.SetCanModifyRem(ChangedDS, "RCID=COMMON.HINT.CLOSE_AUDIT");/*�ѽ���������̲����޸�*/
                    }
                }
                #region ƾ֤�Ķ�Ӧ
                DataRow _drMf = ChangedDS.Tables["MF_IJ"].Rows[0];
                if (_canModify && !String.IsNullOrEmpty(_drMf["VOH_NO"].ToString()))
                {
                    //�ж�ƾ֤
                    string _acNo = "";
                    DrpVoh _drpVoh = new DrpVoh();
                    string _updUsr = "";
                    if (ChangedDS.ExtendedProperties.ContainsKey("UPD_USR"))
                    {
                        _updUsr = ChangedDS.ExtendedProperties["UPD_USR"].ToString();
                    }
                    else
                    {
                        _updUsr = _drMf["USR"].ToString();
                    }
                    int _resVoh = _drpVoh.CheckBillVohAc(_drMf["VOH_NO"].ToString(), _updUsr, ref _acNo);
                    if (_resVoh == 0 || _resVoh == 1)
                    {
                        ChangedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = false;
                        ChangedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                    else if (_resVoh == 2)
                    {
                        ChangedDS.ExtendedProperties["BILL_VOH_AC_CONTROL"] = true;
                        ChangedDS.ExtendedProperties["VOH_AC_NO"] = _acNo;
                    }
                }
                #endregion

                #region �Ѿ����Ͻ᰸�ĵ����ܱ��޸�***

                foreach (DataRow dr in ChangedDS.Tables["TF_IJ"].Rows)
                {
                    string _cls_lj_id = Convert.ToString(dr["CLS_LJ_ID"]);
                    if (CaseInsensitiveComparer.Default.Compare(_cls_lj_id, "F") == 0 || String.IsNullOrEmpty(_cls_lj_id))
                    {//�ҵ�һ��û�����Ͻ᰸�ľ�BREAK;
                        _isCls = false;
                        break;
                    }
                }
                if (_isCls)
                {
                    _canModify = false;
                    ////Common.SetCanModifyRem(ChangedDS, "RCID=MTN.TF_IJ.RB_ERROR");/*���м�¼�����Ͻ᰸,�����޸�*/
                }
                #endregion

                ChangedDS.ExtendedProperties["CAN_MODIFY"] = _canModify.ToString().Substring(0, 1).ToUpper();
            }

            return _isCls ? "RCID=MTN.TF_IJ.RB_ERROR;" : "";//���м�¼�����Ͻ᰸,�����޸�
        }

        #region ����
        /// <summary>
        /// ���浥������
        /// </summary>
        /// <param name="changeDs"></param>
        /// <param name="bubbleException">�Ƿ�ϣ���׳��쳣</param>
        public DataTable UpdateData(string pgm, SunlikeDataSet changeDs, bool bubbleException)
        {
            DataTable _dtErr = null;
            changeDs.Tables["TF_IJ_BAR"].TableName = "TF_IJ1";
            changeDs.Tables["TF_IJ_BOX"].TableName = "TF_IJ2";
            //�Ƿ��ؽ�ƾ֤����
            if (changeDs.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", changeDs.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            _ht["MF_IJ"] = "IJ_ID,IJ_NO,IJ_DD,IJ_REASON,FIX_CST,DEP,REF_NO,MAN_NO,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,BIL_TYPE,SYS_DATE,CUS_NO,BAT_NO,VOH_ID,VOH_NO,BIL_ID,BIL_NO,SQ_ID,SQ_NO,MOB_ID";
            _ht["TF_IJ"] = "IJ_ID,IJ_NO,ITM,IJ_DD,PRD_NO,PRD_NAME,PRD_MARK,WH,UNIT,QTY,QTY1,CST,CST_STD,FIX_CST,KEY_ITM,BOX_ITM,UP,PRE_ITM,BAT_NO,VALID_DD,BIL_ID,BIL_NO, SQ_ID, SQ_NO, SQ_ITM,PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,DEP_RK,RK_DD";
            //			_ht["TF_IJ1"] = "IJ_ID,IJ_NO,IJ_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
            _ht["TF_IJ2"] = "IJ_ID,IJ_NO,ITM,PRD_NO,CONTENT,QTY,KEY_ITM,WH";
            //�ж��Ƿ����������
            string _usr, _ijID;
            DataRow _dr = changeDs.Tables["MF_IJ"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _usr = _dr["USR", DataRowVersion.Original].ToString();
                _ijID = _dr["IJ_ID", DataRowVersion.Original].ToString();
            }
            else
            {
                _usr = _dr["USR"].ToString();
                _ijID = _dr["IJ_ID"].ToString();
            }
            Auditing _auditing = new Auditing();

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
            //_isRunAuditing = _auditing.IsRunAuditing(_ijID, _usr, bilType, _mobID);



            //--------------����ά���ռ�/�ռ��˻أ�ҳ����ô˷���ʱ�������������----------------
            if (changeDs.ExtendedProperties.Contains("PGM_MTNRCV") && changeDs.ExtendedProperties["PGM_MTNRCV"].ToString() == "MTNRCV")
                _isRunAuditing = false;
            //--------------modified by yzb----------------

            this.UpdateDataSet(changeDs, _ht);
            //�жϵ����ܷ��޸�
            if (!changeDs.HasErrors)
            {
                //���ӵ���Ȩ��
                string _UpdUsr = "";
                if (changeDs.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = changeDs.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                   // string _pgm = "DRP" + _ijID;
                    DataTable _dtMf = changeDs.Tables["MF_IJ"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        changeDs.ExtendedProperties["UPD"] = _billRight["UPD"];
                        changeDs.ExtendedProperties["DEL"] = _billRight["DEL"];
                        changeDs.ExtendedProperties["PRN"] = _billRight["PRN"];
                       changeDs.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }
                this.SetCanModify(changeDs, true, false);
            }
            changeDs.Tables["TF_IJ1"].TableName = "TF_IJ_BAR";
            changeDs.Tables["TF_IJ2"].TableName = "TF_IJ_BOX";
            //ȡ�����������
            if (!changeDs.HasErrors)
            {
                BarBox _box = new BarBox();
                DataTable _dtBox = changeDs.Tables["TF_IJ_BOX"];
                for (int i = 0; i < _dtBox.Rows.Count; i++)
                {
                    DataRow _drBox = _dtBox.Rows[i];
                    if (String.IsNullOrEmpty(_drBox["CONTENT_DSC"].ToString()))
                    {
                        _drBox["CONTENT_DSC"] = _box.GetBar_BoxDsc(_drBox["PRD_NO"].ToString(), _drBox["CONTENT"].ToString());
                    }
                }
                _dtBox.AcceptChanges();
            }
            else
            {
                _dtErr = GetAllErrors(changeDs);
                if (bubbleException)
                    //throw new System.Exception("IJ_NO:" + changeDs.Tables[0].Rows[0].RowError);
                    throw new SunlikeException(changeDs.Tables[0].Rows[0].RowError);
            }
            return _dtErr;
        }

        /// <summary>
        /// BeforeDsSave
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dtMf = ds.Tables["MF_IJ"];
            //if (_dtMf.Rows.Count > 0 && _dtMf.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrace = new DataTrace();
            //    string _bilId = "";
            //    if (_dtMf.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dtMf.Rows[0]["IJ_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dtMf.Rows[0]["IJ_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrace.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion
            if (ds.Tables["MF_IJ"].Rows.Count > 0
                && ds.Tables["MF_IJ"].Rows[0].RowState == DataRowState.Modified
                && this._isRunAuditing
                && !String.IsNullOrEmpty(ds.Tables["MF_IJ"].Rows[0]["CHK_MAN"].ToString()))
            {
                string _rbError = this.RollBack(ds.Tables["MF_IJ"].Rows[0]["IJ_ID"].ToString(),
                    ds.Tables["MF_IJ"].Rows[0]["IJ_NO"].ToString(), false);
                if (!String.IsNullOrEmpty(_rbError))
                {
                    throw new SunlikeException(_rbError);
                }
            }
            base.BeforeDsSave(ds);
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
            Auditing _auditing = new Auditing();
            if (tableName == "MF_IJ")
            {
                #region ��ͷ
                string _usr, _ijID, _ijNo;
                if (statementType == StatementType.Delete)
                {
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                    _ijID = dr["IJ_ID", DataRowVersion.Original].ToString();
                    _ijNo = dr["IJ_NO", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = dr["USR"].ToString();
                    _ijID = dr["IJ_ID"].ToString();
                    _ijNo = dr["IJ_NO"].ToString();
                }
                if (statementType != StatementType.Insert)
                {
                    if (_auditing.GetIfEnterAuditing(_ijID, _ijNo))//�����ȥ����˾Ͳ����޸ĺ�ɾ��
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");
                    }
                    //�ж��Ƿ�����������Ѿ����������޸ġ�
                    Users _Users = new Users();
                    string _whereStr = "IJ_ID = '" + _ijID + "' AND IJ_NO = '" + _ijNo + "'";
                    if (_Users.IsLocked("MF_IJ", _whereStr))
                    {
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                    }
                }
                //���������ȷ��
                if (statementType != StatementType.Delete)
                {
                    //����ʱ�жϹ�������
                    if (statementType != StatementType.Delete)
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["IJ_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    else
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["IJ_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    //���ͻ�
                    Cust _cust = new Cust();
                    if (!String.IsNullOrEmpty(dr["CUS_NO"].ToString()) && !_cust.IsExist(_usr, dr["CUS_NO"].ToString(), Convert.ToDateTime(dr["IJ_DD"])))
                    {
                        dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//�ͻ�����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }

                    //��龭����
                    if (_ijID != "XF")
                    {
                        Salm _salm = new Salm();
                        if (!_salm.IsExist(_usr, dr["MAN_NO"].ToString(), Convert.ToDateTime(dr["IJ_DD"])))
                        {
                            dr.SetColumnError("MAN_NO", "RCID=COMMON.HINT.MAN_NO_NOTEXIST,PARAM=" + dr["MAN_NO"].ToString());//������[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    //��鲿��
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["IJ_DD"])))
                    {
                        dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());//���Ŵ���[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    //ȡ�ñ��浥��
                    dr["IJ_NO"] = _sq.Set(_ijID, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["IJ_DD"]), dr["BIL_TYPE"].ToString());
                    //д��Ĭ����λֵ
                    dr["PRT_SW"] = "N";
                    dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                }
                else if (statementType == StatementType.Delete)
                {
                    string _error = _sq.Delete(dr["IJ_NO", DataRowVersion.Original].ToString(), _usr);
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//�޷�ɾ�����ţ�ԭ��{0}
                    }
                }
                //�ж��Ƿ����������
                //if (_isRunAuditing && dr.RowState == DataRowState.Deleted)
                //{
                //    _auditing.DelBillWaitAuditing("DRP", _ijID, dr["IJ_NO", DataRowVersion.Original].ToString());
                //}

                //#region ������
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["IJ_DD"]);
                //    _aps.BIL_ID = dr["IJ_ID"].ToString();
                //    _aps.BIL_NO = dr["IJ_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["MAN_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    //_aps.MOB_ID = "";
                //}
                //else
                //{
                //    _aps = new AudParamStruct(Convert.ToString(dr["IJ_ID", DataRowVersion.Original]), Convert.ToString(dr["IJ_NO", DataRowVersion.Original]));
                //}
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

                //����ƾ֤
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }
                #endregion
            }
            else if (tableName == "TF_IJ")
            {
                if (statementType != StatementType.Delete)
                {
                    #region ����ǰ����
                    dr["IJ_DD"] = dr.Table.DataSet.Tables["MF_IJ"].Rows[0]["IJ_DD"];
                    string _usr = dr.Table.DataSet.Tables["MF_IJ"].Rows[0]["USR"].ToString();
                    //����Ʒ����
                    Prdt _prdt = new Prdt();
                    if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["IJ_DD"])))
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

                    //����λ
                    WH _wh = new WH();
                    Cust _cust = new Cust();
                    if ((dr.Table.DataSet.ExtendedProperties["CONTROL_CUS_WH"] != null && dr.Table.DataSet.ExtendedProperties["CONTROL_CUS_WH"].ToString() == "1")
                        || !_cust.IsDrp_id(dr.Table.DataSet.Tables["MF_IJ"].Rows[0]["CUS_NO"].ToString()))//���Ǿ����̲���Ҫ���ƿ�λ
                    {
                        if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr["IJ_DD"])))
                        {
                            dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//�ֿ����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                    else if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr["IJ_DD"]), dr.Table.DataSet.Tables["MF_IJ"].Rows[0]["CUS_NO"].ToString()))
                    {
                        dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//�ֿ����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                        status = UpdateStatus.SkipAllRemainingRows;
                    }


                    //WH _wh = new WH();
                    //string _cusNo = dr.Table.DataSet.Tables["MF_IJ"].Rows[0]["CUS_NO"].ToString();
                    //if (String.IsNullOrEmpty(_cusNo))
                    //{
                    //    if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr["IJ_DD"])))
                    //    {
                    //        dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//�ֿ����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                    //        status = UpdateStatus.SkipAllRemainingRows;
                    //    }
                    //}
                    //else
                    //{
                    //    if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr["IJ_DD"]), _cusNo))
                    //    {
                    //        dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//�ֿ����[{0}]�����ڻ�û�ж��������Ȩ�ޣ�����
                    //        status = UpdateStatus.SkipAllRemainingRows;
                    //    }
                    //}
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
                            //Bat _bat = new Bat();
                            //_bat.AutoInsertData(dr["BAT_NO"].ToString(), dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["IJ_DD"]));
                        }
                    }
                    dr["FIX_CST"] = dr.Table.DataSet.Tables["MF_IJ"].Rows[0]["FIX_CST"];
                    if (statementType == StatementType.Insert)
                    {
                        dr["PRE_ITM"] = dr["KEY_ITM"];
                    }
                    //�Զ���������
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        Bat _bat = new Bat();
                        if (!_bat.IsExist(dr["BAT_NO"].ToString()) && Users.GetSpcPswdString(_usr, "AUTO_NEW_BATNO") == "F")
                        {
                            throw new Exception("RCID=COMMON.HINT.AUTO_NEW_BATNO,PARAM=" + dr["BAT_NO"].ToString());
                        }
                        _bat.AutoInsertData(dr["BAT_NO"].ToString(), dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["IJ_DD"]));
                    }
                    #endregion
                    #region ���׷�Ӳ��� lzj
                    if (statementType == StatementType.Insert && !(dr["KEY_ITMTF"] is DBNull))
                    {
                        DbMTNOut _mtnout = new DbMTNOut(Comp.Conn_DB);
                        MTNOut _mout = new MTNOut();
                        string otId = Convert.ToString(dr["SQ_ID"]);
                        string otNo = Convert.ToString(dr["SQ_NO"]);
                        string otItm = Convert.ToString(dr["KEY_ITMTF"]);
                        string prdNo = Convert.ToString(dr["PRD_NO"]);
                        string prdMark = Convert.ToString(dr["PRD_MARK"]);
                        string wh = Convert.ToString(dr["WH"]);
                        string batNo = Convert.ToString(dr["BAT_NO"]);
                        string unit = Convert.ToString(dr["UNIT"]);
                        //decimal qtyOt=Convert2Decimal(dr["QTY"])*(-1);

                        SunlikeDataSet _ds = _mtnout.GetData(otId, otNo);
                        DataTable _dtTf1 = _ds.Tables["TF_MOUT_CL"];
                        DataRow[] _drs = _dtTf1.Select("OT_ITM='" + otItm + "' AND PRD_NO='" + prdNo + "' AND isnull(PRD_MARK,'')='" + prdMark + "' AND WH='" + wh + "' AND BAT_NO='" + batNo + "' ");//4��ȫ��
                        if (_drs.Length < 1)
                        {
                            _drs = _dtTf1.Select("OT_ITM='" + otItm + "' AND PRD_NO='" + prdNo + "' AND isnull(PRD_MARK,'')='" + prdMark + "' AND WH='" + wh + "' ");//3��prdNo,prdMark,wh
                            if (_drs.Length < 1)
                            {
                                _drs = _dtTf1.Select("OT_ITM='" + otItm + "' AND PRD_NO='" + prdNo + "' AND isnull(PRD_MARK,'')='" + prdMark + "' ");//2��prdNo,prdMark,
                                if (_drs.Length < 1)
                                {//����
                                    int keyItm = getMaxItm(_dtTf1.Select(), "KEY_ITM") + 1;
                                    int itm = getMaxItm(_dtTf1.Select(), "ITM") + 1;
                                    DataRow _drTf1 = _dtTf1.NewRow();
                                    _drTf1["OT_ID"] = otId;
                                    _drTf1["OT_NO"] = otNo;
                                    _drTf1["OT_ITM"] = otItm;
                                    _drTf1["ITM"] = itm;
                                    _drTf1["KEY_ITM"] = keyItm;
                                    _drTf1["PRD_NO"] = prdNo;
                                    _drTf1["PRD_MARK"] = prdMark;
                                    _drTf1["BAT_NO"] = batNo;
                                    _drTf1["WH"] = wh;
                                    _drTf1["QTY"] = 0;
                                    _drTf1["UNIT"] = unit;
                                    _dtTf1.Rows.Add(_drTf1);
                                    _mout.UpdateData("", "", _ds);
                                    dr["SQ_ITM"] = keyItm;
                                }
                            }
                        }
                        if (_drs.Length > 0)
                        {
                            dr["SQ_ITM"] = _drs[0]["KEY_ITM"];
                        }
                    }

                    #endregion

                   
                }
                else
                {

                }
            }
            if (_barCodeNo == 0)
            {
                if (!this._isRunAuditing)
                {
                    //�������кż�¼
                    try
                    {
                        this.UpdateBarCode(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                    }
                    catch (Exception _ex)
                    {
                        status = UpdateStatus.SkipAllRemainingRows;
                        throw new SunlikeException(_ex.Message, _ex);
                    }
                }
                //����TF_IJ1
                //				if (dr.Table.DataSet.Tables["MF_IJ"].Rows[0].RowState == DataRowState.Deleted)
                //				{
                //					Query _query = new Query();
                //					_query.RunSql("delete from TF_IJ1 where IJ_ID='" + dr["IJ_ID",DataRowVersion.Original].ToString()
                //						+ "' and IJ_NO='" + dr["IJ_NO",DataRowVersion.Original].ToString() + "'");
                //				}
                //				else
                {
                    string _fieldList = "IJ_ID,IJ_NO,IJ_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
                    SQLBatchUpdater _sbu = new SQLBatchUpdater(Comp.Conn_DB);
                    _sbu.BatchUpdateSize = 50;
                    _sbu.BatchUpdate(dr.Table.DataSet.Tables["TF_IJ1"], _fieldList);
                }
            }
            _barCodeNo++;
        }

        private int getMaxItm(DataRow[] _drs, string field)
        {
            int itm = 0;
            foreach (DataRow _dr in _drs)
            {
                if (itm < Convert.ToInt16(_dr[field]))
                    itm = Convert.ToInt16(_dr[field]);
            }
            return itm;
        }

        /// <summary>
        /// ���浥��֮��Ķ���
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="statementType"></param>
        /// <param name="dr"></param>
        /// <param name="status"></param>
        /// <param name="recordsAffected"></param>
        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            //�ж��Ƿ����������
            if (!_isRunAuditing)
            {
                DRPPI _pi = new DRPPI();
                string _ijID, _ijNo, _refNo;
                bool _isPlus = true;
                if (tableName == "TF_IJ")
                {
                    if (_bodyNo == 0)
                    {
                        //����SARP
                        this.UpdateSarp(dr);

                        //ɾ��������ʱͬ��ɾ���̵㵥��Ӧ����
                        if (statementType == StatementType.Delete)
                        {
                            _ijID = dr["IJ_ID", DataRowVersion.Original].ToString();
                            if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                            {
                                if (Convert.ToDecimal(dr["QTY", DataRowVersion.Original]) < 0)
                                {
                                    _isPlus = false;
                                }
                                if (dr.Table.DataSet.Tables["MF_IJ"].Rows[0].RowState == DataRowState.Deleted)
                                {
                                    _refNo = dr.Table.DataSet.Tables["MF_IJ"].Rows[0]["REF_NO", DataRowVersion.Original].ToString();
                                    _pi.DeleteDRPPI(_refNo, _ijID, _isPlus);
                                }
                            }
                        }

                    }
                    //�޸ķֲִ��� 
                    try
                    {
                        if (statementType == StatementType.Insert)
                        {
                            this.UpdateWh(dr, true);
                            if (!String.IsNullOrEmpty(dr["PT_ITM"].ToString()))
                            {
                                _ijID = dr["IJ_ID"].ToString();
                                _ijNo = dr["IJ_NO"].ToString();
                                _refNo = dr.Table.DataSet.Tables["MF_IJ"].Rows[0]["REF_NO"].ToString();
                                if (Convert.ToDecimal(dr["QTY"]) < 0)
                                {
                                    _isPlus = false;
                                }
                                if (!String.IsNullOrEmpty(_refNo))
                                {
                                    string _errorMsg = _pi.UpdateMF_PT(_refNo, _ijID, _ijNo, _isPlus);
                                    if (!string.IsNullOrEmpty(_errorMsg))
                                        throw new SunlikeException(_errorMsg);
                                    _pi.UpdateTF_PT(_refNo, Convert.ToInt32(dr["PT_ITM"]), _ijID, _ijNo, _isPlus);
                                }
                            }
                        }
                        else if (statementType == StatementType.Delete)
                        {
                            this.UpdateWh(dr, false);
                        }

                        else if (statementType == StatementType.Update)
                        {
                            this.UpdateWh(dr, false);
                            this.UpdateWh(dr, true);
                        }
                    }
                    catch (Exception _ex)
                    {
                        dr.SetColumnError("QTY", _ex.Message.ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    _bodyNo++;
                }
                else if (tableName == "TF_IJ2")
                {
                    try
                    {
                        if (statementType == StatementType.Insert)
                        {
                            this.UpdateBoxWh(dr, true);
                        }
                        else if (statementType == StatementType.Delete)
                        {
                            this.UpdateBoxWh(dr, false);
                        }
                        else if (statementType == StatementType.Update)
                        {
                            this.UpdateBoxWh(dr, false);
                            this.UpdateBoxWh(dr, true);
                        }
                    }
                    catch (Exception _ex)
                    {
                        dr.SetColumnError("QTY", _ex.Message.ToString());
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
            }
        }

        /// <summary>
        /// ȫ���������֮��Ķ���
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            if (!_isRunAuditing && ds.Tables["MF_IJ"].Rows.Count > 0 && ds.Tables["MF_IJ"].Rows[0].RowState != DataRowState.Deleted)
            {
                //��BAR_PRINTת��SPC_NO
                DbDRPIJ _ij = new DbDRPIJ(Comp.Conn_DB);
                _ij.UpdateBarPrint(ds.Tables["MF_IJ"].Rows[0]["IJ_ID"].ToString(), ds.Tables["MF_IJ"].Rows[0]["IJ_NO"].ToString());
            }
            //if (!_isRunAuditing && ds.Tables.Contains("MF_IJ") && ds.Tables["MF_IJ"].Rows.Count > 0) 
            //{
            //    //�������Ͻ᰸���
            //    string _otid = Convert.ToString(ds.Tables["MF_IJ"].Rows[0]["SQ_ID"]);
            //    string _otno = Convert.ToString(ds.Tables["MF_IJ"].Rows[0]["SQ_NO"]);
            //    DbMTNOut _out = new DbMTNOut(Comp.Conn_DB);
            //    _out.ClsMoutLj(_otid, _otno);
            //}
        }

        #region �������
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

        /// <summary>
        /// ���ͨ��
        /// </summary>
        /// <param name="bil_id">����ʶ����</param>
        /// <param name="bil_no">���ݺ���</param>
        /// <param name="chk_man">�����</param>
        /// <param name="cls_dd">�������</param>
        /// <returns>������Ϣ</returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData(chk_man, bil_id, bil_no, false);
                _ds.Tables["TF_IJ_BAR"].TableName = "TF_IJ1";
                _ds.Tables["TF_IJ_BOX"].TableName = "TF_IJ2";
                DataRow _drHead = _ds.Tables["MF_IJ"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_IJ"];
                DataTable _dtBar = _ds.Tables["TF_IJ1"];
                DataTable _dtBox = _ds.Tables["TF_IJ2"];
                //����SARP
                this.UpdateSarp(_drHead);
                //����ƾ֤
                string _vohNo = this.UpdateVohNo(_drHead, StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);
                //�������кż�¼
                DataTable _dtBarCopy = _dtBar.Copy();
                _dtBar.Clear();
                for (int i = 0; i < _dtBarCopy.Rows.Count; i++)
                {
                    DataRow _drBar = _dtBar.NewRow();
                    for (int j = 0; j < _dtBarCopy.Columns.Count; j++)
                    {
                        _drBar[j] = _dtBarCopy.Rows[i][j];
                    }
                    _dtBar.Rows.Add(_drBar);
                }
                _dtBarCopy.Dispose();
                GC.Collect(GC.GetGeneration(_dtBarCopy));
                this._auditBarCode = true;
                this.UpdateBarCode(_ds);
                //�޸ķֲִ���
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    this.UpdateWh(_dtBody.Rows[i], true);
                }
                //�޸��ܶ���������
                for (int i = 0; i < _dtBox.Rows.Count; i++)
                {
                    this.UpdateBoxWh(_dtBox.Rows[i], true);
                }
                //�趨�����
                DbDRPIJ _ij = new DbDRPIJ(Comp.Conn_DB);
                _ij.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd);
            }
            catch (Exception _ex)
            {
                _error = _ex.Message;
            }
            return _error;
        }

        /// <summary>
        /// ��˴������
        /// </summary>
        /// <param name="bil_id">����ʶ����</param>
        /// <param name="bil_no">���ݺ���</param>
        /// <returns>������Ϣ</returns>
        public string RollBack(string bil_id, string bil_no)
        {
            return this.RollBack(bil_id, bil_no, true);
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="bil_id">����ʶ����</param>
        /// <param name="bil_no">���ݺ���</param>
        /// <param name="isUpdateHead">���±�ͷ�����Ϣ</param>
        /// <returns></returns>
        public string RollBack(string bil_id, string bil_no, bool isUpdateHead)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData("", bil_id, bil_no, false);
                _ds.Tables["TF_IJ_BAR"].TableName = "TF_IJ1";
                _ds.Tables["TF_IJ_BOX"].TableName = "TF_IJ2";
                DataRow _drHead = _ds.Tables["MF_IJ"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_IJ"];
                DataTable _dtBar = _ds.Tables["TF_IJ1"];
                DataTable _dtBox = _ds.Tables["TF_IJ2"];
                string _msg = this.SetCanModify(_ds, false, true);
                if (!string.IsNullOrEmpty(_msg))
                {
                    return _msg;//
                }
                else
                {
                    if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                    {
                        return "RCID=COMMON.HINT.AGAINSTCHKDFEAT";
                    }
                }
                //�޸�������
                for (int i = 0; i < _dtBox.Rows.Count; i++)
                {
                    this.UpdateBoxWh(_dtBox.Rows[i], false);
                }
                //�޸ķֲִ������ܶ�����������
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    this.UpdateWh(_dtBody.Rows[i], false);
                }
                //�������кż�¼
                for (int i = 0; i < _dtBar.Rows.Count; i++)
                {
                    _dtBar.Rows[i].Delete();
                }
                this._auditBarCode = true;
                this.UpdateBarCode(_ds);
                DbDRPIJ _ij = new DbDRPIJ(Comp.Conn_DB);
                //�趨�����
                if (isUpdateHead)
                {
                    _ij.UpdateChkMan(bil_id, bil_no, "", DateTime.Now);
                }
                //����SARP
                _drHead.Delete();
                this.UpdateSarp(_drHead);
                //����ƾ֤
                this.UpdateVohNo(_drHead, StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");
            }
            catch (Exception _ex)
            {
                _error = _ex.Message;
            }
            return _error;
        }
        #endregion

        private void UpdateBarCode(SunlikeDataSet ChangedDS)
        {
            DataRow _drHead = ChangedDS.Tables["MF_IJ"].Rows[0];
            DataTable _dtBody = ChangedDS.Tables["TF_IJ"];
            //���ұ������޸Ĺ���λ�ļ�¼
            System.Collections.Hashtable _htKeyItm = new System.Collections.Hashtable();
            for (int i = 0; i < _dtBody.Rows.Count; i++)
            {
                if (_dtBody.Rows[i].RowState == DataRowState.Modified
                    && (_dtBody.Rows[i]["WH"].ToString() != _dtBody.Rows[i]["WH", DataRowVersion.Original].ToString()
                    || _dtBody.Rows[i]["BAT_NO"].ToString() != _dtBody.Rows[i]["BAT_NO", DataRowVersion.Original].ToString()))
                {
                    _htKeyItm[_dtBody.Rows[i]["KEY_ITM"].ToString()] = "";
                }
            }
            //����BAR_CODE
            DataTable _dtBarCode;
            if ((_drHead.RowState == DataRowState.Modified || _drHead.RowState == DataRowState.Unchanged) && !this._auditBarCode)
            {
                string _sql = "select IJ_ID,IJ_NO,IJ_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO from TF_IJ1"
                    + " where IJ_ID='" + _drHead["IJ_ID"].ToString() + "' and IJ_NO='" + _drHead["IJ_NO"].ToString() + "'";
                Query _query = new Query();
                SunlikeDataSet _dsQuery = _query.DoSQLString(_sql);
                _dsQuery.Tables[0].TableName = "TF_IJ1";
                //				_dsQuery.Merge(ChangedDS.Tables["TF_IJ1"],false,MissingSchemaAction.AddWithKey);
                //				_dtBarCode = _dsQuery.Tables["TF_IJ1"];
                ChangedDS.Merge(_dsQuery.Tables["TF_IJ1"], true, MissingSchemaAction.AddWithKey);
                _dtBarCode = ChangedDS.Tables["TF_IJ1"];
            }
            else
            {
                _dtBarCode = ChangedDS.Tables["TF_IJ1"];
            }
            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            for (int i = 0; i < _dtBarCode.Rows.Count; i++)
            {
                if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["IJ_ITM"].ToString()] != null)
                {
                    if (_sb.Length > 0)
                    {
                        _sb.Append(",");
                    }
                    _sb.Append("'");
                    if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                    {
                        _sb.Append(_dtBarCode.Rows[i]["BAR_CODE", DataRowVersion.Original].ToString());
                    }
                    else
                    {
                        _sb.Append(_dtBarCode.Rows[i]["BAR_CODE"].ToString());
                    }
                    _sb.Append("'");
                }
            }
            //������/�޸Ĺ�BAR_CODE�����޸�BAR_REC��
            if (_sb.Length > 0)
            {
                _sb.Insert(0, "BAR_NO in (");
                _sb.Append(")");
                //���������ֶ�
                System.Collections.ArrayList _alBar = new System.Collections.ArrayList();
                int _maxWhereLength = 10240;
                string _subWhere;
                int _pos;
                string _where = _sb.ToString();
                while (true)
                {
                    if (_where.Length > _maxWhereLength)
                    {
                        _subWhere = _where.Substring(0, _maxWhereLength - 1);
                        _pos = _subWhere.LastIndexOf(",");
                        _alBar.Add(_subWhere.Substring(0, _pos) + ")");
                        _where = "BAR_NO in (" + _where.Substring(_pos + 1, _where.Length - _pos - 1);
                    }
                    else
                    {
                        _alBar.Add(_where);
                        break;
                    }
                }
                //�����Ͽ��ȡ���к�����
                BarCode _bar = new BarCode();
                SunlikeDataSet _dsBar = new SunlikeDataSet();
                DataTable _dtBarRec;
                for (int i = 0; i < _alBar.Count; i++)
                {
                    _dtBarRec = _bar.GetBarRecord(_alBar[i].ToString(), true);
                    if (_dsBar.Tables["BAR_REC"] == null)
                    {
                        _dsBar.Tables.Add(_dtBarRec.Copy());
                    }
                    else
                    {
                        _dsBar.Merge(_dtBarRec, true, MissingSchemaAction.AddWithKey);
                    }
                }
                _dtBarRec = _dsBar.Tables["BAR_REC"];
                string[] _aryBoxNo = new string[_dtBarCode.Rows.Count];
                //ȡ��ͷ����
                string _cusNo, _bilID, _bilNo, _usr;
                DateTime _bilDd = DateTime.Today;
                if (_drHead.RowState == DataRowState.Deleted)
                {
                    _cusNo = _drHead["CUS_NO", DataRowVersion.Original].ToString();
                    _bilID = _drHead["IJ_ID", DataRowVersion.Original].ToString();
                    _bilNo = _drHead["IJ_NO", DataRowVersion.Original].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["IJ_DD", DataRowVersion.Original]);
                    _usr = _drHead["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _cusNo = _drHead["CUS_NO"].ToString();
                    _bilID = _drHead["IJ_ID"].ToString();
                    _bilNo = _drHead["IJ_NO"].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["IJ_DD"]);
                    _usr = _drHead["USR"].ToString();
                }
                //ȡ�øõ���������change
                DataTable _dtBarChange = null;
                if (_drHead.RowState != DataRowState.Added)
                {
                    _dtBarChange = _bar.GetBarChange(_bilID, _bilNo);
                }
                //����BAR_REC��
                System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
                System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
                System.Collections.ArrayList _alBoxNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alWhNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alStop = new System.Collections.ArrayList();
                System.Text.StringBuilder _sbChange = new System.Text.StringBuilder();
                double _total = 0;
                for (int i = 0; i < _dtBarCode.Rows.Count; i++)
                {
                    if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["IJ_ITM"].ToString()] != null)
                    {
                        string _barCode, _boxNo, _keyItm, _whNo, _batNo;
                        string _oldWhNo = "";
                        string _oldBatNo = "";
                        DataRow[] _dra;
                        bool _isPlus = true;
                        if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                        {
                            DateTime _dt1 = DateTime.Now;
                            _barCode = _dtBarCode.Rows[i]["BAR_CODE", DataRowVersion.Original].ToString();
                            _boxNo = _dtBarCode.Rows[i]["BOX_NO", DataRowVersion.Original].ToString();
                            _keyItm = _dtBarCode.Rows[i]["IJ_ITM", DataRowVersion.Original].ToString();
                            _dra = _dtBody.Select("KEY_ITM=" + _keyItm, "", DataViewRowState.CurrentRows | DataViewRowState.OriginalRows);
                            if (_dra[0].RowState == DataRowState.Deleted)
                            {
                                _whNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _batNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                                if (_dra[0]["PLUS_FLAG", DataRowVersion.Original].ToString() == "-")
                                {
                                    _isPlus = false;
                                }
                            }
                            else
                            {
                                _batNo = _dra[0]["BAT_NO"].ToString();
                                _whNo = _dra[0]["WH"].ToString();
                                if (_dra[0]["PLUS_FLAG"].ToString() == "-")
                                {
                                    _isPlus = false;
                                }
                            }
                            DateTime _dt2 = DateTime.Now;
                            TimeSpan _ts = _dt2 - _dt1;
                            _total += _ts.TotalSeconds;
                        }
                        else
                        {
                            _barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
                            _boxNo = _dtBarCode.Rows[i]["BOX_NO"].ToString();
                            _keyItm = _dtBarCode.Rows[i]["IJ_ITM"].ToString();
                            _dra = _dtBody.Select("KEY_ITM=" + _keyItm);
                            _whNo = _dra[0]["WH"].ToString();
                            _batNo = _dra[0]["BAT_NO"].ToString();
                            if (_dra[0].RowState == DataRowState.Modified)
                            {
                                _oldWhNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _oldBatNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                            }
                            if (_dra[0]["PLUS_FLAG"].ToString() == "-")
                            {
                                _isPlus = false;
                            }
                        }
                        DataRow _drBarRec = _dtBarRec.Rows.Find(new string[1] { _barCode });
                        if (_drBarRec != null)
                        {
                            bool _canUpdate = true;
                            Sunlike.Business.UserProperty _userProp = new UserProperty();
                            string _pgm = "DRP" + _bilID;
                            if (!String.IsNullOrEmpty(_drBarRec["BOX_NO"].ToString()) && _userProp.GetData(_usr, _pgm, "CONTROL_BARCODE") != "0"
                                && Comp.BarcodeUpdCheck && Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "F")
                            {
                                if (!String.IsNullOrEmpty(_drBarRec["UPDDATE"].ToString()))
                                {
                                    DateTime _barDd = Convert.ToDateTime(_drBarRec["UPDDATE"]);
                                    TimeSpan _timeSpan = _barDd.Subtract(_bilDd);
                                    if (_timeSpan.TotalMilliseconds > 0)
                                    {
                                        _canUpdate = false;
                                    }
                                }
                            }
                            if (_canUpdate)
                            {
                                //���������λ�����к����ڵĿ�λ��ͬ����Ҫд��BAR_CHANGE
                                //������ֻҪbar_rec���д����кţ��ҿ�λ��Ϊ�գ���Ҫд��bar_change
                                //������ֻҪbar_rec���д����кţ��ҿ�λ��Ϊ�գ��ҵ����Ŀ�λ�����ڵ�ǰ���к����ڿ�λ����д��bar_change
                                if (_dtBarCode.Rows[i].RowState == DataRowState.Added)
                                {
                                    if ((_isPlus && (!String.IsNullOrEmpty(_drBarRec["WH"].ToString()) || !String.IsNullOrEmpty(_drBarRec["BAT_NO"].ToString())))
                                        || (!_isPlus && (_drBarRec["WH"].ToString() != _whNo || _drBarRec["BAT_NO"].ToString() != _batNo)) || _drBarRec["PH_FLAG"].ToString() == "T")
                                    {
                                        _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo, _bilID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
                                    }
                                }
                                else if (_dtBarCode.Rows[i].RowState == DataRowState.Unchanged)
                                {
                                    if ((_isPlus && (_oldWhNo != _drBarRec["WH"].ToString() || _oldBatNo != _drBarRec["BAT_NO"].ToString() || _drBarRec["STOP_ID"].ToString() == "T"))
                                        || (!_isPlus && (_oldWhNo != _drBarRec["WH"].ToString() || _oldBatNo != _drBarRec["BAT_NO"].ToString() || _drBarRec["STOP_ID"].ToString() != "T")))
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//���к�_barCode�Ѳ��ڵ�ǰ��λ��������ɾ����
                                    }
                                    else
                                    {
                                        DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
                                        if (_draBarChange.Length > 0)
                                        {
                                            string _changeWh1 = _draBarChange[0]["WH1"].ToString();
                                            string _changeBatNo1 = _draBarChange[0]["BAT_NO1"].ToString();
                                            string _changePhFlag1 = _draBarChange[0]["PH_FLAG1"].ToString();

                                            _sbChange.Append(_bar.DeleteBarChange(_barCode, _bilID, _bilNo, true));
                                            if (_isPlus || (!_isPlus && (_changeWh1 != _whNo || _changeBatNo1 != _batNo)))
                                            {
                                                _sbChange.Append(_bar.InsertBarChange(_barCode, _changeWh1, _whNo, _bilID, _bilNo, _usr, _changeBatNo1, _batNo, _changePhFlag1, "F", true));
                                            }
                                        }
                                        else if ((!_isPlus && (_drBarRec["WH"].ToString() != _whNo || _drBarRec["BAT_NO"].ToString() != _batNo)) || _drBarRec["PH_FLAG"].ToString() == "T")
                                        {
                                            _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo, _bilID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
                                        }
                                    }
                                }
                                string _boxWh = "";
                                if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                                {
                                    DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
                                    if (_isPlus)
                                    {
                                        //BAR_REC.WH����͵��ݿ�λһ�£���BAR_REC.STOP_ID='F'����������ɾ��
                                        if (_batNo == _drBarRec["BAT_NO"].ToString() && _whNo == _drBarRec["WH"].ToString() && _drBarRec["STOP_ID"].ToString() != "T")
                                        {
                                            if (_draBarChange.Length == 0)
                                            {
                                                _drBarRec["WH"] = System.DBNull.Value;
                                                _drBarRec["BAT_NO"] = System.DBNull.Value;
                                            }
                                        }
                                        else
                                        {
                                            _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//���к�_barCode�Ѳ��ڵ�ǰ��λ��������ɾ����
                                        }
                                    }
                                    else
                                    {
                                        if (_batNo == _drBarRec["BAT_NO"].ToString() && _whNo == _drBarRec["WH"].ToString() && _drBarRec["STOP_ID"].ToString() == "T")
                                        {
                                            _drBarRec["STOP_ID"] = "F";
                                        }
                                        else
                                        {
                                            _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//���к�_barCode�Ѳ��ڵ�ǰ��λ��������ɾ����
                                        }
                                    }
                                    //���������bar_change�����λ�ص���bar_change֮ǰ�ĵط����Ұ�bar_change�ļ�¼ɾ��
                                    if (_draBarChange.Length > 0)
                                    {
                                        _boxWh = _draBarChange[0]["WH1"].ToString();
                                        _drBarRec["WH"] = _boxWh;
                                        _drBarRec["BAT_NO"] = _draBarChange[0]["BAT_NO1"].ToString();
                                        //����ȷ�ϱ��
                                        _drBarRec["PH_FLAG"] = _draBarChange[0]["PH_FLAG1"];
                                        _sbChange.Append(_bar.DeleteBarChange(_barCode, _bilID, _bilNo, true));
                                    }
                                }
                                else
                                {
                                    if (_isPlus)
                                    {
                                        _drBarRec["WH"] = _whNo;
                                        _drBarRec["BAT_NO"] = _batNo;
                                        //�������ռ�����ͣ�õ����кţ���STOP_ID���F
                                        _drBarRec["STOP_ID"] = "F";
                                    }
                                    else
                                    {
                                        if (_drBarRec["WH"].ToString() != _whNo)
                                        {
                                            _drBarRec["WH"] = _whNo;
                                            _boxWh = _whNo;
                                        }
                                        if (_drBarRec["BAT_NO"].ToString() != _batNo)
                                        {
                                            _drBarRec["BAT_NO"] = _batNo;
                                        }
                                        _drBarRec["STOP_ID"] = "T";
                                    }
                                    _drBarRec["PH_FLAG"] = "F";
                                }
                                _drBarRec["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                                //���B0X_NO��һ������Ҫ���Զ�����
                                if (_drBarRec["BOX_NO"].ToString() != _boxNo)
                                {
                                    if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
                                    {
                                        _aryBoxNo[i] = _drBarRec["BOX_NO"].ToString().Trim();
                                    }
                                    else
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.DOUBLEBOX,PARAM=" + _barCode);//���к�" + _barCode + "�Ѿ�װ�䣬�㲻�ܲ���¼�룡
                                    }
                                }
                                else if (!String.IsNullOrEmpty(_boxNo))
                                {
                                    if (_htBoxNo[_boxNo] == null)
                                    {
                                        _htBoxNo[_boxNo] = "";
                                        _alBoxNo.Add(_boxNo);
                                    }
                                }
                            }
                            else
                            {
                                //�Ƴ���,��������
                                _dtBarRec.Rows.Remove(_drBarRec);
                            }
                        }
                        else if (_dtBarCode.Rows[i].RowState != DataRowState.Deleted)
                        {
                            DataRow _dr = _dtBarRec.NewRow();
                            _dr["BAR_NO"] = _barCode;
                            _dr["WH"] = _whNo;
                            _dr["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            _dr["STOP_ID"] = (!_isPlus).ToString().ToUpper().Substring(0, 1);
                            _dr["PRD_NO"] = _barCode.Substring(BarCode.BarRole.SPrdt, BarCode.BarRole.EPrdt - BarCode.BarRole.SPrdt + 1).Replace(BarCode.BarRole.TrimChar, "");
                            if (!(BarCode.BarRole.BPMark == BarCode.BarRole.EPMark && BarCode.BarRole.EPMark == 0))
                            {
                                _dr["PRD_MARK"] = _barCode.Substring(BarCode.BarRole.BPMark, BarCode.BarRole.EPMark - BarCode.BarRole.BPMark + 1);
                            }
                            _dr["BAT_NO"] = _batNo;
                            _dtBarRec.Rows.Add(_dr);
                        }
                    }
                }
                if (String.IsNullOrEmpty(_sbError.ToString()))
                {
                    //д�����кű����¼
                    if (_sbChange.Length > 0)
                    {
                        Query _query = new Query();
                        string _sql = _sbChange.ToString();
                        int _maxSqlLength = 10240;
                        while (true)
                        {
                            if (_sql.Length > _maxSqlLength)
                            {
                                string _subSql = _sql.Substring(0, _maxSqlLength - 1);
                                _pos = _subSql.LastIndexOf("\n");
                                _subSql = _subSql.Substring(0, _pos + 1);
                                _sql = _sql.Substring(_pos + 1, _sql.Length - _pos - 1);
                                _query.RunSql(_subSql);
                            }
                            else
                            {
                                _query.RunSql(_sql);
                                break;
                            }
                        }
                    }
                    //�޸�BAR_REC
                    //					try
                    //					{
                    //						_bar.UpdateRec(_dtBarRec);
                    //					}
                    //					catch
                    //					{
                    //						throw new SunlikeException("RCID=COMMON.HINT.BOXERROR");//�������кż�¼ʧ�ܣ�
                    //					}
                    DataTable _dtError = _bar.UpdateRec(SunlikeDataSet.ConvertTo(_dtBarRec.DataSet));
                    if (_dtError.Rows.Count > 0)
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.BOXERROR");//�������кż�¼ʧ�ܣ�
                    }

                    _alWhNo.Clear();
                    _alStop.Clear();
                    foreach (string boxNo in _alBoxNo)
                    {
                        DataRow[] _draBoxInfo = _dtBarRec.Select("BOX_NO='" + boxNo + "'");
                        if (_draBoxInfo.Length > 0)
                        {
                            _alWhNo.Add(_draBoxInfo[0]["WH"].ToString());
                            _alStop.Add(_draBoxInfo[0]["STOP_ID"].ToString());
                        }
                        else
                        {
                            _alWhNo.Add(null);
                            _alStop.Add(null);
                        }
                    }
                    //�޸�BAR_BOX
                    _bar.UpdateBarBox(_alBoxNo, _alWhNo, _alStop);
                    //����
                    _bar.DeleteBox(_aryBoxNo, _usr, _bilID, _bilNo);
                }
                else
                {
                    throw new SunlikeException(_sbError.ToString());
                }
            }
        }

        private void UpdateSarp(DataRow dr)
        {
            DataRow _dr = dr.Table.DataSet.Tables["MF_IJ"].Rows[0];
            DataTable _dtBody = _dr.Table.DataSet.Tables["TF_IJ"];
            int _year, _month;
            string _cusNo, _cusNoOld;
            if (_dr.RowState == DataRowState.Deleted)
            {
                _year = Convert.ToDateTime(_dr["IJ_DD", DataRowVersion.Original]).Year;
                _month = Convert.ToDateTime(_dr["IJ_DD", DataRowVersion.Original]).Month;
                _cusNo = _dr["CUS_NO", DataRowVersion.Original].ToString();
                _cusNoOld = _dr["CUS_NO", DataRowVersion.Original].ToString();
            }
            else
            {
                _year = Convert.ToDateTime(_dr["IJ_DD"]).Year;
                _month = Convert.ToDateTime(_dr["IJ_DD"]).Month;
                _cusNo = _dr["CUS_NO"].ToString();
                if (_dr.RowState == DataRowState.Added)
                {
                    _cusNoOld = _cusNo;
                }
                else
                {
                    _cusNoOld = _dr["CUS_NO", DataRowVersion.Original].ToString();
                }
            }
            Cust _cust = new Cust();
            decimal _amtn = 0;
            decimal _amtnOld = 0;
            decimal _up, _qty;
            foreach (DataRow drBody in _dtBody.Rows)
            {
                if (drBody.RowState != DataRowState.Deleted)
                {
                    if (String.IsNullOrEmpty(drBody["UP"].ToString()))
                    {
                        _up = 0;
                    }
                    else
                    {
                        _up = Convert.ToDecimal(drBody["UP"]);
                    }
                    if (String.IsNullOrEmpty(drBody["QTY"].ToString()))
                    {
                        _qty = 0;
                    }
                    else
                    {
                        _qty = Convert.ToDecimal(drBody["QTY"]);
                    }
                    _amtn += _up * _qty;
                }
                if (drBody.RowState != DataRowState.Added)
                {
                    if (String.IsNullOrEmpty(drBody["UP", DataRowVersion.Original].ToString()))
                    {
                        _up = 0;
                    }
                    else
                    {
                        _up = Convert.ToDecimal(drBody["UP", DataRowVersion.Original]);
                    }
                    if (String.IsNullOrEmpty(drBody["QTY", DataRowVersion.Original].ToString()))
                    {
                        _qty = 0;
                    }
                    else
                    {
                        _qty = Convert.ToDecimal(drBody["QTY", DataRowVersion.Original]);
                    }
                    _amtnOld += _up * _qty;
                }
            }
            Arp _arp = new Arp();
            if (_cust.IsDrp_id(_cusNoOld) && _amtnOld != 0)
            {
                _arp.UpdateSarp("1", _year, _cusNoOld, _month, "", "AMTN_INV", _amtnOld * -1);
            }
            if (_cust.IsDrp_id(_cusNo) && _amtn != 0)
            {
                _arp.UpdateSarp("1", _year, _cusNo, _month, "", "AMTN_INV", _amtn);
            }
        }

        private void UpdateWh(DataRow dr, bool IsAdd)
        {
            string _batNo = "";
            string _validDd = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
            decimal _cst = 0;
            Prdt _prdt = new Prdt();
            string _ut = "";
            string _sqID = "";//ά�ް�װ��
            string _sqNO = "";//ά�ް�װ��
            string _sqITM = "";//ά�ް�װ��
            //RKTypes _rktype = new RKTypes();
            if (IsAdd)
            {
                _batNo = dr["BAT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat);
                }
                _prdNo = dr["PRD_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _whNo = dr["WH"].ToString();
                _ut = dr["UNIT"].ToString();
                //_rktype.DEP = dr["DEP_RK"].ToString();
                //if (!String.IsNullOrEmpty(dr["RK_DD"].ToString()))
                //{
                //    _rktype.RK_DD = Convert.ToDateTime(dr["RK_DD"]);
                //}
                //if (!String.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                //{
                //    _rktype.VALID_DD = Convert.ToDateTime(Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat));
                //}
                if (!String.IsNullOrEmpty(dr["CST"].ToString()))
                {
                    _cst = Convert.ToDecimal(dr["CST"]);
                }
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1"].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1"]);
                }
                _sqID = Convert.ToString(dr["SQ_ID"]);
                _sqNO = Convert.ToString(dr["SQ_NO"]);
                _sqITM = Convert.ToString(dr["SQ_ITM"]);
            }
            else
            {
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD", DataRowVersion.Original].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD", DataRowVersion.Original]).ToString(Comp.SQLDateFormat);
                }
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _ut = dr["UNIT", DataRowVersion.Original].ToString();
                //_rktype.DEP = dr["DEP_RK", DataRowVersion.Original].ToString();
                //if (!String.IsNullOrEmpty(dr["RK_DD", DataRowVersion.Original].ToString()))
                //{
                //    _rktype.RK_DD = Convert.ToDateTime(dr["RK_DD", DataRowVersion.Original]);
                //}
                //if (!String.IsNullOrEmpty(dr["VALID_DD", DataRowVersion.Original].ToString()))
                //{
                //    _rktype.VALID_DD = Convert.ToDateTime(Convert.ToDateTime(dr["VALID_DD", DataRowVersion.Original]).ToString(Comp.SQLDateFormat));
                //}
                if (!String.IsNullOrEmpty(dr["CST", DataRowVersion.Original].ToString()))
                {
                    _cst = Convert.ToDecimal(dr["CST", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                }
                _qty *= -1;
                _qty1 *= -1;
                _cst *= -1;
                _sqID = Convert.ToString(dr["SQ_ID", DataRowVersion.Original]);
                _sqNO = Convert.ToString(dr["SQ_NO", DataRowVersion.Original]);
                _sqITM = Convert.ToString(dr["SQ_ITM", DataRowVersion.Original]);
            }

            if (_sqID == "OT" || _sqID == "OI")//ֻ��Ӧ��ά��,��װ
            {
                if (!string.IsNullOrEmpty(_sqNO))
                {
                    #region ��дά�޵���������
                    MTNOut _mout = new MTNOut();
                    Hashtable ht = new Hashtable();
                    ht["TableName"] = "TF_MOUT1";
                    ht["IdName"] = "OT_ID";
                    ht["NoName"] = "OT_NO";
                    ht["ItmName"] = "KEY_ITM";
                    ht["OsID"] = _sqID;
                    ht["OsNO"] = _sqNO;
                    ht["KeyItm"] = _sqITM;
                    _qty = INVCommon.GetRtnQty(_prdNo, _qty, Convert.ToInt16(_ut), ht);
                    _mout.UpdateMout(_sqID, _sqNO, _sqITM, _qty);
                    #endregion
                }
                #region �������

                WH _wh = new WH();

                Hashtable _ht = new Hashtable();
                _ht.Clear();

                if (!String.IsNullOrEmpty(_batNo))
                {
                    if (IsAdd)
                    {
                        if (_qty < 0)
                        {
                            //���ӵ���  ����
                            //_ht[WH.QtyTypes.QTY_ON_RSV] = _qtyOnRsv;
                            _ht[WH.QtyTypes.QTY_OUT] = _qty * -1;
                            _ht[WH.QtyTypes.CST] = _cst;
                        }
                        else
                        {
                            //���ӵ��� ����
                            //_ht[WH.QtyTypes.QTY_ON_RSV] = _qtyOnRsv;
                            _ht[WH.QtyTypes.QTY_IN] = _qty;
                            _ht[WH.QtyTypes.CST] = _cst;
                        }
                    }
                    else
                    {
                        if (_qty < 0)
                        {
                            //ɾ������ ɾ������
                            //_ht[WH.QtyTypes.QTY_ON_RSV] = _qtyOnRsv;
                            _ht[WH.QtyTypes.QTY_IN] = _qty;
                            _ht[WH.QtyTypes.CST] = _cst;
                        }
                        else
                        {
                            //ɾ������ ɾ������
                            //_ht[WH.QtyTypes.QTY_ON_RSV] = _qtyOnRsv;
                            _ht[WH.QtyTypes.QTY_OUT] = _qty * -1;
                            _ht[WH.QtyTypes.CST] = _cst;
                        }
                    }
                    if ((IsAdd && _qty < 0) || (!IsAdd && _qty > 0))
                    {
                        //��������Ҫд��Ч����  ����
                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                            "", _ut, _ht);
                    }
                    else
                    {
                        SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                        if (!String.IsNullOrEmpty(_validDd))
                        {
                            if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                            {
                                TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                                if (_timeSpan.Days > 0)
                                {
                                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                                        "", _ut, _ht);
                                }
                                else
                                {
                                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                                        _validDd, _ut, _ht);
                                }
                            }
                            else
                            {
                                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                                    _validDd, _ut, _ht);
                            }
                        }
                        else
                        {
                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                                "", _ut, _ht);
                        }
                    }
                }
                else
                {
                    _ht[WH.QtyTypes.QTY] = _qty;
                    _ht[WH.QtyTypes.AMT_CST] = _cst;
                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _ut, _ht);
                }
                #endregion
            }
            else
            {
                #region �������

                WH _wh = new WH();
                System.Collections.Hashtable _ht = new System.Collections.Hashtable();

                if (!String.IsNullOrEmpty(_batNo))
                {
                    if (IsAdd)
                    {
                        if (_qty < 0)
                        {
                            //���ӵ���
                            _ht[WH.QtyTypes.QTY_OUT] = _qty * -1;
                            _ht[WH.QtyTypes.QTY1_OUT] = _qty1 * -1;
                            _ht[WH.QtyTypes.CST] = _cst;
                        }
                        else
                        {
                            //���ӵ���
                            _ht[WH.QtyTypes.QTY_IN] = _qty;
                            _ht[WH.QtyTypes.QTY1_IN] = _qty1;
                            _ht[WH.QtyTypes.CST] = _cst;
                        }
                    }
                    else
                    {
                        if (_qty < 0)
                        {
                            //ɾ������
                            _ht[WH.QtyTypes.QTY_IN] = _qty;
                            _ht[WH.QtyTypes.QTY1_IN] = _qty1;
                            _ht[WH.QtyTypes.CST] = _cst;
                        }
                        else
                        {
                            //ɾ������
                            _ht[WH.QtyTypes.QTY_OUT] = _qty * -1;
                            _ht[WH.QtyTypes.QTY1_OUT] = _qty1 * -1;
                            _ht[WH.QtyTypes.CST] = _cst;
                        }
                    }
                    if ((IsAdd && _qty < 0) || (!IsAdd && _qty > 0))
                    {
                        //��������Ҫд��Ч����
                        _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                            "", _ut, _ht);
                    }
                    else
                    {
                        SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
                        if (!String.IsNullOrEmpty(_validDd))
                        {
                            if (_ds.Tables["BAT_REC1"].Rows.Count > 0 && !String.IsNullOrEmpty(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"].ToString()))
                            {
                                TimeSpan _timeSpan = Convert.ToDateTime(_ds.Tables["BAT_REC1"].Rows[0]["VALID_DD"]).Subtract(Convert.ToDateTime(_validDd));
                                if (_timeSpan.Days > 0)
                                {
                                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                                        "", _ut, _ht);
                                }
                                else
                                {
                                    _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                                        _validDd, _ut, _ht);
                                }
                            }
                            else
                            {
                                _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                                    _validDd, _ut, _ht);
                            }
                        }
                        else
                        {
                            _wh.UpdateQty(_batNo, _prdNo, _prdMark, _whNo,
                                "", _ut, _ht);
                        }
                    }
                }
                else
                {
                    _ht[WH.QtyTypes.QTY] = _qty;
                    _ht[WH.QtyTypes.QTY1] = _qty1;
                    _ht[WH.QtyTypes.AMT_CST] = _cst;

                    _wh.UpdateQty(_prdNo, _prdMark, _whNo, _ut, _ht);
                }
                #endregion
            }


        }

        private void UpdateBoxWh(DataRow dr, bool IsAdd)
        {
            string _prdNo = "";
            string _content = "";
            string _whNo = "";
            decimal _qty = 0;
            if (IsAdd)
            {
                _prdNo = dr["PRD_NO"].ToString();
                _content = dr["CONTENT"].ToString();
                _whNo = dr["WH"].ToString();
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
            }
            else
            {
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _content = dr["CONTENT", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                _qty *= -1;

            }
            WH _wh = new WH();
            _wh.UpdateBoxQty(_prdNo, _whNo, _content, WH.BoxQtyTypes.QTY, _qty);
        }

        /// <summary>
        /// ����ƾ֤����
        /// </summary>
        /// <param name="dr">MF_IJ��������</param>
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
                string _ijId = dr["IJ_ID"].ToString();
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
                        if (string.Compare("IJ", _ijId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.IJ;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _ijId, dr["IJ_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _ijId, out _vohNoError);
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
                        if (string.Compare("IJ", _ijId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.IJ;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", _ijId, dr["IJ_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _ijId, out _vohNoError);
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
                string _ijId = dr["IJ_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("IJ", _ijId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.IJ;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()))
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _ijId, out _vohNoError);
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
        /// <summary>
        /// ����������ƾ֤����
        /// </summary>
        /// <param name="ijId"></param>
        /// <param name="ijNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string ijId, string ijNo, string vohNo)
        {
            DbDRPIJ _sa = new DbDRPIJ(Comp.Conn_DB);
            _sa.UpdateVohNo(ijId, ijNo, vohNo);
        }

        #endregion

        #region ǿ���˻�ɾ�����
        /// <summary>
        /// ǿ���˻�ɾ�����
        /// </summary>
        /// <param name="psId">���ݱ�</param>
        /// <param name="psNo">����</param>
        /// <param name="preItm">׷�����</param>
        /// <returns></returns>
        private bool chkRtnDel(string ijId, string ijNo, string keyItm)
        {
            if (string.Compare("SA", ijId) != 0)
                return true;
            if (string.IsNullOrEmpty(ijNo))
                return true;
            if (string.IsNullOrEmpty(keyItm))
                return true;
            bool _result = true;

            SunlikeDataSet _dsIj = GetData("", ijId, ijNo, false);
            if (_dsIj != null && _dsIj.Tables.Count > 0 && _dsIj.Tables["MF_IJ"].Rows.Count > 0)
            {
                DataRow[] _drSel = _dsIj.Tables["MF_IJ"].Select("IJ_ID='" + ijId + "' AND IJ_NO='" + ijNo + "' AND KEY_ITM=" + keyItm + " AND ISNULL(CHK_RTN,'F') = 'T'");
                //�������ѷ��н᰸����ǿ���˻���������ɾ��
                if (_drSel != null && _drSel.Length > 0 && string.Compare("T", _dsIj.Tables["MF_IJ"].Rows[0]["HAS_FX"].ToString()) == 0)
                {
                    _result = false;
                }
            }
            return _result;
        }
        #endregion

        #region ����������
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="BeginDate">��ʼ����</param>
        /// <param name="EndDate">��������</param>
        public void ResetBoxQty(DateTime BeginDate, DateTime EndDate)
        {
            DbDRPIJ _ij = new DbDRPIJ(Comp.Conn_DB);
            _ij.ResetBoxQty(BeginDate.ToString(Comp.SQLDateFormat), EndDate.ToString(Comp.SQLDateFormat));
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="IjNo">��������</param>
        /// <param name="PrdMark">��Ʒ����</param>
        /// <param name="BoxItm">�����</param>
        /// <param name="Qty">����</param>
        public void ResetBoxQty(string IjNo, string PrdMark, int BoxItm, decimal Qty)
        {
            DbDRPIJ _ij = new DbDRPIJ(Comp.Conn_DB);
            _ij.ResetBoxQty(IjNo, PrdMark, BoxItm, Qty);
        }
        #endregion

        #region �ó����͵��ݲ�ͬ�������prdt1�еĵ�һ������
        /// <summary>
        ///  �ó��������ݲ�ͬ�������prdt1�еĵ�һ������
        /// </summary>
        /// <param name="BeginDate">��ʼ����</param>
        /// <param name="EndDate">��������</param>
        /// <returns></returns>
        public DataTable GetFirstPrdt1ByBox(DateTime BeginDate, DateTime EndDate)
        {
            DbDRPIJ _ij = new DbDRPIJ(Comp.Conn_DB);
            return _ij.GetFirstPrdt1ByBox(BeginDate.ToString(Comp.SQLDateFormat), EndDate.ToString(Comp.SQLDateFormat));
        }
        #endregion

        #region �������Ƿ�������

        /// <summary>
        /// �������Ƿ�������
        /// </summary>
        /// <param name="IjNo">��������</param>
        /// <returns></returns>
        public bool IsFinalAuditing(string IjNo)
        {
            DbDRPIJ _ij = new DbDRPIJ(Comp.Conn_DB);
            return _ij.IsFinalAuditing(IjNo);
        }

        #endregion
    }
}
