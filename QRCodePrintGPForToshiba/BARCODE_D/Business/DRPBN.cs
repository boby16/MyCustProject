/*
 * modify: cjc 090805 
 * 审核流程的改动(单据别和审核模板)
 */

using System;
using System.Data;
using System.Collections;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Text;
using System.Collections.Generic;

namespace Sunlike.Business
{
    /// <summary>
    /// Summary description for DRPPC.
    /// </summary>
    public class DRPBN : BizObject, IAuditing
    {
        private bool _isRunAuditing;
        private int _barCodeNo;
        private bool _auditBarCode;
        /// <summary>
        /// 是否重新切制凭证
        /// </summary>
        private bool _reBuildVohNo = false;
        private Dictionary<string, string> _updateWhFlag = new Dictionary<string, string>();

        private System.Collections.ArrayList _alOsNo = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alOsItm = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alPrdNo = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alUnit = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alQty = new System.Collections.ArrayList();
        private System.Collections.ArrayList _alQty1 = new System.Collections.ArrayList();

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string pgm,string usr, string blId, string blNo, bool onlyFillSchema)
        {
            DbDRPBN _dbBn = new DbDRPBN(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbBn.GetData(blId, blNo, onlyFillSchema);
            if (usr != null && !String.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                _ds.DecimalDigits = Comp.GetCompInfo(_users.GetUserDepNo(usr)).DecimalDigitsInfo.System;

                //管制立帐方式
                if (INVCommon.IsControlZhangId(usr, blId))
                {
                    _ds.ExtendedProperties["CTRL_ZHANG_ID"] = "T";
                }
            }

            //转入单据的数量
            _ds.Tables["TF_BLN"].Columns.Add(new DataColumn("QTY_SO_ORG", typeof(System.Decimal)));
            //表身加库存量栏位
            _ds.Tables["TF_BLN"].Columns.Add(new DataColumn("WH_QTY"));
            //单位标准成本
            _ds.Tables["TF_BLN"].Columns.Add(new DataColumn("CST_STD_UNIT", typeof(decimal)));

            //增加单据权限
            if (!onlyFillSchema)
            {
                this.SetCanModify(usr, _ds, true, false);
            }

            //设定表身的PreItm为自动递增
            DataTable _dtTfBln = _ds.Tables["TF_BLN"];
            DataTable _dtMfBln = _ds.Tables["MF_BLN"];
            _dtTfBln.Columns["PRE_ITM"].AutoIncrement = true;
            _dtTfBln.Columns["PRE_ITM"].AutoIncrementSeed = _dtTfBln.Rows.Count > 0 ? Convert.ToInt32(_dtTfBln.Select("", "PRE_ITM desc")[0]["PRE_ITM"]) + 1 : 1;
            _dtTfBln.Columns["PRE_ITM"].AutoIncrementStep = 1;
            _dtTfBln.Columns["PRE_ITM"].Unique = true;

            if (blId == "BN" || blId == "LN")
            {
                _dtTfBln.Columns["EST_ITM"].AutoIncrement = true;
                _dtTfBln.Columns["EST_ITM"].AutoIncrementSeed = _dtTfBln.Rows.Count > 0 ? Convert.ToInt32(_dtTfBln.Select("", "EST_ITM desc")[0]["EST_ITM"]) + 1 : 1;
                _dtTfBln.Columns["EST_ITM"].AutoIncrementStep = 1;
                _dtTfBln.Columns["EST_ITM"].Unique = true;
            }
            _dtTfBln.Columns.Add("PRD_NO_NO", typeof(System.String));
            _dtTfBln.Columns.Add("UNIT_DP", typeof(System.String)).MaxLength = 8;
            foreach (DataRow _drTfBln in _dtTfBln.Rows)
            {
                _drTfBln["PRD_NO_NO"] = _drTfBln["PRD_NO"];

                if (!String.IsNullOrEmpty(_drTfBln["CST_STD"].ToString())
                   && !String.IsNullOrEmpty(_drTfBln["QTY"].ToString())
                   && Convert.ToDecimal(_drTfBln["QTY"]) != 0)
                    _drTfBln["CST_STD_UNIT"] = Convert.ToDecimal(_drTfBln["CST_STD"]) / Convert.ToDecimal(_drTfBln["QTY"]);
                else
                    _drTfBln["CST_STD_UNIT"] = 0;

                if (blId == "BB" || blId == "LB")
                {//导入原单数量
                    using (DataSet _dsBn = GetBodyData(blId == "BB" ? "BN" : "LN", _drTfBln["PRE_BL_NO"].ToString(), Convert.ToInt32(_drTfBln["EST_ITM"])))
                    {
                        if (_dsBn.Tables[0].Rows.Count > 0)
                        {
                            _drTfBln["QTY_SO_ORG"] = _dsBn.Tables[0].Rows[0]["QTY"];
                        }
                    }
                }
                else if (_drTfBln["OS_ID"].ToString() == "PO")
                {
                    DRPPO _drpPo = new DRPPO();
                    using (DataSet _dsPo = _drpPo.GetBody("PO", _drTfBln["OS_NO"].ToString(), "PRE_ITM", Convert.ToInt32(_drTfBln["OS_ITM"]), true))
                    {
                        if (_dsPo.Tables["TF_POS"].Rows.Count > 0)
                        {
                            _drTfBln["QTY_SO_ORG"] = _dsPo.Tables["TF_POS"].Rows[0]["QTY"];
                        }
                    }
                }
                else if (_drTfBln["OS_ID"].ToString() == "SO")
                {
                    DrpSO _drpSo = new DrpSO();
                    using (DataSet _dsSo = _drpSo.GetBody("SO", _drTfBln["OS_NO"].ToString(), "PRE_ITM", Convert.ToInt32(_drTfBln["OS_ITM"]), true))
                    {
                        if (_dsSo.Tables["TF_POS"].Rows.Count > 0)
                        {
                            _drTfBln["QTY_SO_ORG"] = _dsSo.Tables["TF_POS"].Rows[0]["QTY"];
                        }
                    }

                }
            }

            if (blId == "BB" || blId == "LB")
            { //取借入日期
                _dtMfBln.Columns.Add("BN_DD", typeof(DateTime));
                if (_dtMfBln.Rows.Count > 0)
                {
                    Query _query = new Query();
                    string _sql = "select BL_DD from MF_BLN where BL_NO='" + _dtMfBln.Rows[0]["REP_BL_NO"].ToString() + "' AND BL_ID='" + (blId == "BB" ? "BN" : "LN") + "'";
                    DataSet _dsBN = _query.DoSQLString(_sql);
                    if (_dsBN.Tables[0].Rows.Count > 0)
                    {
                        _dtMfBln.Rows[0]["BN_DD"] = _dsBN.Tables[0].Rows[0][0];
                    }
                }
            }

            //创建存放序列号的暂存表
            DataTable _dt = new DataTable("BAR_COLLECT");
            _dt.Columns.Add("ITEM", typeof(int));
            _dt.Columns.Add("BAR_CODE");
            _dt.Columns.Add("BAT_NO");
            _dt.Columns.Add("BOX_NO");
            _dt.Columns.Add("PRD_NO");
            _dt.Columns.Add("PRD_MARK");
            //表身库位
            _dt.Columns.Add("WH1");
            _dt.Columns.Add("SERIAL_NO");
            //序列号库位和批号
            _dt.Columns.Add("ISEXIST");
            _dt.Columns.Add("WH_REC");
            _dt.Columns.Add("BAT_NO_REC");
            _dt.Columns.Add("STOP_ID");
            _dt.Columns.Add("PH_FLAG");
            DataColumn[] _dca = new DataColumn[1];
            _dca[0] = _dt.Columns["ITEM"];
            _dt.PrimaryKey = _dca;
            _ds.Tables.Add(_dt);
            //把序列号的记录转入暂存表中
            DataView _dv = new DataView(_ds.Tables["TF_BLN_B"]);
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
            //取序列号库位、批号
            foreach (DataRow dr in _dt.Rows)
            {
                DataRow[] _aryDrBar = _ds.Tables["TF_BLN_B"].Select("BAR_CODE='" + dr["BAR_CODE"].ToString() + "'");
                if (_aryDrBar.Length > 0)
                {
                    DataRow[] _aryDr = _ds.Tables["TF_BLN"].Select("PRE_ITM=" + _aryDrBar[0]["BL_ITM"].ToString());
                    if (_aryDr.Length > 0)
                    {
                        dr["WH1"] = _aryDr[0]["WH"];
                        dr["BAT_NO"] = _aryDr[0]["BAT_NO"];
                    }
                }
            }
            _dv.Dispose();
            GC.Collect(GC.GetGeneration(_dv));

            /*
            Auditing _auditing = new Auditing();

            string bilType = "";
            if (_ds.Tables["MF_BLN"].Rows[0].RowState != DataRowState.Deleted)
                if (_ds.Tables["MF_BLN"].Columns.Contains("BIL_TYPE"))
                    bilType = _ds.Tables["MF_BLN"].Rows[0]["BIL_TYPE"].ToString();
            _isRunAuditing = _auditing.IsRunAuditing(blId, usr,bilType);



            if (_isRunAuditing)
            {//租金为零是否立账
                _ds.ExtendedProperties["CFM_LZ"] = "F";
            }
            else
            {
                _ds.ExtendedProperties["CFM_LZ"] = "T";
            }
            */

            _ds.AcceptChanges();
            return _ds;
        }

        /// <summary>
        /// SetCanModify
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="dataSet"></param>
        /// <param name="isChkAuditing"></param>
        /// <param name="isRollBack"></param>
        private void SetCanModify(string usr, SunlikeDataSet dataSet, bool isChkAuditing, bool isRollBack)
        {

            DataTable _dtMf = dataSet.Tables["MF_BLN"];
            DataTable _dtTf = dataSet.Tables["TF_BLN"];

            if (_dtMf.Rows.Count > 0)
            {
                string _blId = _dtMf.Rows[0]["BL_ID"].ToString();
                string _pgm = "DRP" + _blId;
                string _billDep = _dtMf.Rows[0]["DEP"].ToString();
                string _billUsr = _dtMf.Rows[0]["USR"].ToString();
                System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, usr, _billDep, _billUsr);
                dataSet.ExtendedProperties["UPD"] = _billRight["UPD"];
                dataSet.ExtendedProperties["DEL"] = _billRight["DEL"];
                dataSet.ExtendedProperties["PRN"] = _billRight["PRN"];
                dataSet.ExtendedProperties["LCK"] = _billRight["LCK"];
                bool _canModify = true;

                if (_dtMf.Rows[0]["CLS_ID"].ToString() == "T")
                {
                    _canModify = false;
                    //Common.SetCanModifyRem(dataSet, "RCID=COMMON.HINT.CLOSE_MODIFY");
                }

                if (_dtMf.Rows[0]["LZ_CLS_ID"].ToString() == "T")
                {
                    _canModify = false;
                    //Common.SetCanModifyRem(dataSet, "立账结案不能修改");
                }
                if (_dtMf.Rows[0]["ZHANG_ID"].ToString() == "3")
                {
                    DbDRPSA _dbSa = new DbDRPSA(Comp.Conn_DB);
                    SunlikeDataSet _dsInv = _dbSa.GetInvBill(_dtMf.Rows[0]["BL_ID"].ToString(), _dtMf.Rows[0]["BL_NO"].ToString());
                    if (_dsInv.Tables[0].Rows.Count > 0)
                    {
                        _canModify = false;
                        //Common.SetCanModifyRem(dataSet, "RCID=COMMON.HINT.CLOSE_LZ_MODIFY");
                    }
                }
                if (Comp.HasCloseBill(Convert.ToDateTime(_dtMf.Rows[0]["BL_DD"]), _dtMf.Rows[0]["DEP"].ToString(), "CLS_INV"))
                {
                    //Common.SetCanModifyRem(dataSet, "RCID=COMMON.HINT.CLOSE_CLS");
                    _canModify = false;
                }

                if (isChkAuditing)
                {
                    string _blID = _dtMf.Rows[0]["BL_ID"].ToString();
                    string _blNo = _dtMf.Rows[0]["BL_NO"].ToString();
                    Auditing _aud = new Auditing();
                    if (_aud.GetIfEnterAuditing(_blID, _blNo))
                    {
                        //Common.SetCanModifyRem(dataSet, "RCID=COMMON.HINT.CLOSE_AUDIT");
                        _canModify = false;
                    }
                }

                //判断是否锁单
                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["LOCK_MAN"].ToString()))
                {
                    _canModify = false;
                    //Common.SetCanModifyRem(dataSet, "RCID=COMMON.HINT.CLOSE_LOCK");
                }
                //判断是否做了销货成本切制
                if (string.Compare("T", _dtMf.Rows[0]["CB_ID"].ToString()) == 0)
                {
                    //Common.SetCanModifyRem(dataSet, "RCID=COMMON.HINT.CB_ID");//已经切制销货成本，不能修改单据
                    _canModify = false;
                }

                if (_blId == "BN" || _blId == "LN")
                {//借入单
                    DataRow[] _dra = dataSet.Tables["TF_BLN"].Select("ISNULL(QTY_RTN, 0) > 0");
                    if (_dra.Length > 0)
                    {
                        _canModify = false;
                        //Common.SetCanModifyRem(dataSet, "有已还量不能修改");
                    }
                }



                if (!String.IsNullOrEmpty(_dtMf.Rows[0]["ARP_NO"].ToString()))
                {
                    Arp _arp = new Arp();
                    if (!String.IsNullOrEmpty(_dtMf.Rows[0]["ARP_NO"].ToString()))
                    {
                        try
                        {
                            if (_arp.HasReceiveDollar(_dtMf.Rows[0]["ARP_NO"].ToString()))
                            {
                                _canModify = false;
                                //Common.SetCanModifyRem(dataSet, "已冲帐不能修改");
                            }
                        }
                        catch { }
                    }
                }
                dataSet.ExtendedProperties["CAN_MODIFY"] = _canModify.ToString().Substring(0, 1);
            }

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="dataSet"></param>
        /// <param name="throwException"></param>
        public DataTable UpdateData(string pgm,string usr, SunlikeDataSet dataSet, bool throwException)
        {
            string _blId, _usr;
            string _bilType;
            string _mobID = "";
            DataRow _dr = dataSet.Tables["MF_BLN"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _blId = _dr["BL_ID", DataRowVersion.Original].ToString();
                _usr = _dr["USR", DataRowVersion.Original].ToString();
                _bilType = _dr["BIL_TYPE", DataRowVersion.Original].ToString();
                if (_dr.Table.Columns.Contains("MOB_ID"))
                    _mobID = _dr["MOB_ID", DataRowVersion.Original].ToString();//@@
            }
            else
            {
                _blId = _dr["BL_ID"].ToString();
                _usr = _dr["USR"].ToString();
                _bilType = _dr["BIL_TYPE"].ToString();
                if (_dr.Table.Columns.Contains("MOB_ID"))
                    _mobID = _dr["MOB_ID"].ToString();//@@
            }
            //是否重建凭证号码
            if (dataSet.ExtendedProperties.ContainsKey("RESET_VOH_NO"))
            {
                if (string.Compare("True", dataSet.ExtendedProperties["RESET_VOH_NO"].ToString()) == 0)
                {
                    this._reBuildVohNo = true;
                }
            }
            Hashtable _ht = new Hashtable();
            _ht["MF_BLN"] = "BL_ID,BL_NO,BAT_NO,BL_MOD,AMT_PLED,AMT_FINE,CUS_NO,BL_DD,PAY_DD,REP_BL_NO,ARP_NO,"
                + "VOH_ID,VOH_NO,DEP,INV_NO,TRAD_MTH,SEND_MTH,RP_NO,ZHANG_ID,CUR_ID,EXC_RTO,SAL_NO,BL_DAYS,RTN_DD,"
                + "CLS_ID,TAX,AMTN_NET,AMT_NET,TAX_ID,ADR,REM,PAY_MTH,PAY_DAYS,CHK_DAYS,PAY_REM,SEND_WH,END_DAYS,"
                + "CHK_DD,INT_DAYS,EST_DD,USR,CHK_MAN,PRT_SW,CPY_SW,BIL_NO,CLS_REM,BIL_ID,CLS_DATE,CK_CLS_ID,CB_ID,"
                + "BIL_TYPE,MOB_ID,LOCK_MAN,SYS_DATE,OS_ID,OS_NO,FJ_NUM,CAS_NO,TASK_ID,LZ_CLS_ID,TURN_ID,AMTN_NET_CLS,AMT_CLS,TAX_CLS,QTY_CLS,PO_ID";

            _ht["TF_BLN"] = "BL_ID,BL_NO,ITM,BL_DD,WH,PRD_NO,PRD_NAME,PRD_MARK,UNIT,QTY,QTY1,CST,AMT_RNT,"
                + "AMTN_RNT_NET,TAX_RNT,UP,AMT,AMTN,QTY_RTN,QTY1_RTN,PRE_BL_NO,PRE_ITM,EST_DD,CST_STD,UP_QTY1,"
                + "EST_ITM,BAT_NO,QTY_CK,SUP_PRD_NO,RK_NO,CK_NO,TI_ITM,OS_ID,OS_NO,OS_ITM,TAX_RTO,TAX,VALID_DD,AMTN_NET_FP,AMT_FP,TAX_FP,QTY_FP,REM,"
                + "PAK_UNIT,PAK_EXC,PAK_NW,PAK_WEIGHT_UNIT,PAK_GW,PAK_MEAST,PAK_MEAST_UNIT,DIS_CNT";

            //判断是否走审核流程
            Auditing _auditing = new Auditing();

            //_isRunAuditing = _auditing.IsRunAuditing(_blId, _usr, _bilType, _mobID);

            if (!dataSet.ExtendedProperties.ContainsKey("LZ_ID"))
            {
                dataSet.ExtendedProperties["LZ_ID"] = "T";
            }
            this.UpdateDataSet(dataSet, _ht);
            if (!dataSet.HasErrors)
            {
                //增加单据权限
                string _UpdUsr = "";
                if (dataSet.ExtendedProperties.Contains("UPD_USR"))
                    _UpdUsr = dataSet.ExtendedProperties["UPD_USR"].ToString();
                if (!String.IsNullOrEmpty(_UpdUsr))
                {
                    DataTable _dtMf = dataSet.Tables["MF_BLN"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(pgm, _UpdUsr, _bill_Dep, _bill_Usr);
                        dataSet.ExtendedProperties["UPD"] = _billRight["UPD"];
                        dataSet.ExtendedProperties["DEL"] = _billRight["DEL"];
                        dataSet.ExtendedProperties["PRN"] = _billRight["PRN"];
                        dataSet.ExtendedProperties["LCK"] = _billRight["LCK"];
                    }
                }

                this.SetCanModify(usr, dataSet, false, false);
                dataSet.AcceptChanges();
            }
            else if (throwException)
            {
                throw new Exception(GetErrorsString(dataSet));
            }
            return GetAllErrors(dataSet);
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
            string _blId = "", _blNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _blId = dr["BL_ID"].ToString();
                _blNo = dr["BL_NO"].ToString();
            }
            else
            {
                _blId = dr["BL_ID", DataRowVersion.Original].ToString();
                _blNo = dr["BL_NO", DataRowVersion.Original].ToString();
            }
            if (statementType != StatementType.Insert)
            {
                Auditing _auditing = new Auditing();
                if (_auditing.GetIfEnterAuditing(_blId, _blNo))//如果进去审核了就不能修改和新增删除
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=UNKNOWN.DRPSO.NOTALLOW");
                }

                //判断是否锁单，如果已经锁单则不让修改。
                Users _Users = new Users();
                string _whereStr = "BL_ID = '" + _blId + "' AND BL_NO = '" + _blNo + "'";
                if (_Users.IsLocked("MF_BLN", _whereStr))
                {
                    throw new Sunlike.Common.Utility.SunlikeException("RCID=COMMON.HINT.LOCKED");
                }
            }
            if (tableName == "MF_BLN")
            {
                string _usr;
                if (statementType == StatementType.Delete)
                {
                    _usr = dr["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _usr = dr["USR"].ToString();
                }
                //检查资料正确否
                if (statementType != StatementType.Delete)
                {
                    if (string.IsNullOrEmpty(dr["CUR_ID"].ToString()))
                    {
                        dr["EXC_RTO"] = 1;
                    }
                    dr["SYS_DATE"] = Convert.ToDateTime(DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    //新增时判断关账日期
                    if (statementType != StatementType.Delete)
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["BL_DD"]), dr["DEP"].ToString(), "CLS_INV"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    else
                    {
                        if (Comp.HasCloseBill(Convert.ToDateTime(dr["BL_DD", DataRowVersion.Original]), dr["DEP", DataRowVersion.Original].ToString(), "CLS_INV"))
                        {
                            throw new Exception("RCID=COMMON.HINT.HASCLOSEBILL");
                        }
                    }
                    //如果立帐方式由“单张立帐”改为“不立帐”时，清空立帐单号
                    if (dr.RowState != DataRowState.Added
                        && (dr["ZHANG_ID"].ToString() != dr["ZHANG_ID", DataRowVersion.Original].ToString() || dr.Table.DataSet.ExtendedProperties["LZ_ID"].ToString() == "F"))
                    {
                        dr["ARP_NO"] = System.DBNull.Value;
                    }
                    //检查进货厂商
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString(), Convert.ToDateTime(dr["BL_DD"])))
                    {
                        dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//客户代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //检查业务员
                    Salm _salm = new Salm();
                    if (!_salm.IsExist(_usr, dr["SAL_NO"].ToString(), Convert.ToDateTime(dr["BL_DD"])))
                    {
                        dr.SetColumnError("SAL_NO", "RCID=COMMON.HINT.SAL_NO_NOTEXIST,PARAM=" + dr["SAL_NO"].ToString());//业务员代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //检查部门
                    Dept _dept = new Dept();
                    if (!_dept.IsExist(_usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["BL_DD"])))
                    {
                        dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEPTERROR,PARAM=" + dr["DEP"].ToString());//部门代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                }
                SQNO _sq = new SQNO();
                if (statementType == StatementType.Insert)
                {
                    #region 新增

                    //取得保存单号
                    dr["BL_NO"] = _sq.Set(_blId, _usr, dr["DEP"].ToString(), Convert.ToDateTime(dr["BL_DD"]), dr["BIL_TYPE"].ToString());
                    //写入默认栏位值
                    dr["PRT_SW"] = "N";

                    if (String.IsNullOrEmpty(dr["BL_MOD"].ToString()))
                    {
                        dr["BL_MOD"] = "1";
                    }
                    //取得交易方式
                    if (String.IsNullOrEmpty(dr["PAY_MTH"].ToString()))
                    {
                        Cust _cust = new Cust();
                        System.Collections.Hashtable _ht = _cust.GetPAYInfo(dr["CUS_NO"].ToString(), dr["BL_DD"].ToString());
                        if (_ht != null)
                        {
                            dr["PAY_DD"] = _ht["PAY_DD"];
                            dr["CHK_DD"] = _ht["CHK_DD"];
                            dr["SEND_MTH"] = _ht["SEND_MTH"];
                            dr["SEND_WH"] = _ht["SEND_WH"];
                            dr["PAY_MTH"] = _ht["PAY_MTH"];
                            dr["PAY_DAYS"] = _ht["PAY_DAYS"];
                            dr["CHK_DAYS"] = _ht["CHK_DAYS"];
                            dr["INT_DAYS"] = _ht["INT_DAYS"];
                            dr["PAY_REM"] = _ht["PAY_REM"];
                            dr["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        }
                        else
                        {
                            throw new Exception("RCID=INV.HINT.GETPAYINFO_ERROR");//无法取得客户交易方式
                        }
                    }

                    #endregion
                }
                else
                {
                    if (statementType == StatementType.Delete)
                    {
                        #region 删除

                        string _error = _sq.Delete(dr["BL_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                        if (!String.IsNullOrEmpty(_error))
                        {
                            throw new Exception("RCID=COMMON.SQNO.DEL_NO_ERROR,PARAM=" + _error);//无法删除单号，原因：{0}
                        }
                        ////判断是否走审核流程
                        //if (_isRunAuditing)
                        //{
                        //    Auditing _auditing = new Auditing();
                        //    _auditing.DelBillWaitAuditing("DRP", _blId, dr["BL_NO", DataRowVersion.Original].ToString());
                        //}

                        #endregion
                    }
                }

                //#region 审核相关

                //AudParamStruct _aps;
                //if (dr.RowState != DataRowState.Deleted)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["BL_DD"]);
                //    _aps.BIL_ID = dr["BL_ID"].ToString();
                //    _aps.BIL_NO = dr["BL_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //{
                //    _aps = new AudParamStruct(Convert.ToString(dr["BL_ID", DataRowVersion.Original]), Convert.ToString(dr["BL_NO", DataRowVersion.Original]));
                //}

                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

                //产生凭证
                if (!this._isRunAuditing)
                {
                    this.UpdateVohNo(dr, statementType);
                }

                //try
                //{
                //    DrpTaxAa _drpTaxAa = new DrpTaxAa();
                //    _drpTaxAa.UpdateTaxAa(dr.Table.DataSet, _blId, _blNo);
                //}
                //catch (Exception _ex)
                //{
                //    dr.SetColumnError("INV_NO", _ex.Message);
                //    status = UpdateStatus.SkipAllRemainingRows;
                //}
            }
            else if (tableName == "TF_BLN")
            {
                #region 新增或者修改时
                if (statementType != StatementType.Delete)
                {
                    string _usr = dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["USR"].ToString();
                    Prdt _prdt = new Prdt();
                    //检查货品代号
                    if (!_prdt.IsExist(_usr, dr["PRD_NO"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["BL_DD"])))
                    {
                        dr.SetColumnError("PRD_NO", "RCID=COMMON.HINT.PRD_NO_NOTEXIST,PARAM=" + dr["PRD_NO"].ToString());//货品代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //检查特征分段
                    string _prdMark = dr["PRD_MARK"].ToString();
                    int _prdMod = _prdt.CheckPrdtMod(dr["PRD_NO"].ToString(), _prdMark);
                    if (_prdMod == 1)
                    {
                        dr.SetColumnError(dr.Table.Columns["PRD_MARK"], "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prdMark);//货品特征[{0}]不存在
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
                                    dr.SetColumnError(_fldName, "RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _aryMark[i].Trim());//货品特征[{0}]不存在
                                    status = UpdateStatus.SkipAllRemainingRows;
                                }
                            }
                        }
                    }
                    //检查库位
                    WH _wh = new WH();
                    if (!_wh.IsExist(_usr, dr["WH"].ToString(), Convert.ToDateTime(dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["BL_DD"])))
                    {
                        dr.SetColumnError("WH", "RCID=COMMON.HINT.WH_NOTEXIST,PARAM=" + dr["WH"].ToString());//仓库代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    //修改PRD_NAME字段
                    if (String.IsNullOrEmpty(dr["PRD_NAME"].ToString()))
                    {
                        DataTable _dt = _prdt.GetPrdt(dr["PRD_NO"].ToString());
                        if (_dt.Rows.Count > 0)
                        {
                            dr["PRD_NAME"] = _dt.Rows[0]["NAME"];
                        }
                    }

                    //自动新增批号
                    if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
                    {
                        Bat _bat = new Bat();
                        if (!_bat.IsExist(dr["BAT_NO"].ToString()) && Users.GetSpcPswdString(_usr, "AUTO_NEW_BATNO") == "F")
                        {
                            throw new Exception("RCID=COMMON.HINT.AUTO_NEW_BATNO,PARAM=" + dr["BAT_NO"].ToString());
                        }
                        _bat.AutoInsertData(dr["BAT_NO"].ToString(), dr["PRD_NO"].ToString(), Convert.ToDateTime(dr["BL_DD"]));
                    }
                }
                else
                {

                    //判断是否有补开发票则不允许删除
                    checktflz(dr);

                }
                #endregion
            }

            if (_barCodeNo == 0)
            {
                #region 更新序列号记录["TF_BLN_B"]
                if (!_isRunAuditing)
                {
                    if (_blId == "BN" || _blId == "BB")
                    {
                        this.UpdateBarCodeB(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                    }
                    else
                    {
                        this.UpdateBarCodeL(SunlikeDataSet.ConvertTo(dr.Table.DataSet));
                    }
                }
                if (dr.Table.DataSet.Tables["MF_BLN"].Rows[0].RowState == DataRowState.Deleted)
                {
                    Query _query = new Query();
                    _query.RunSql("delete from TF_BLN_B where BL_ID='" + dr["BL_ID", DataRowVersion.Original].ToString()
                        + "' and  BL_NO='" + dr["BL_NO", DataRowVersion.Original].ToString() + "'");
                }
                else
                {
                    string _fieldList = "BL_ID,BL_NO,BL_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO";
                    SQLBatchUpdater _sbu = new SQLBatchUpdater(Comp.Conn_DB);
                    _sbu.BatchUpdateSize = 50;
                    _sbu.BatchUpdate(dr.Table.DataSet.Tables["TF_BLN_B"], _fieldList);
                }
                #endregion
            }
            _barCodeNo++;

            if (!_isRunAuditing && tableName == "MF_BLN")
            {
                //立账
                this.UpdateMfArp(dr);
                //更新SARP
                this.UpdateSarp(dr);
            }
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
            string _psId = "";
            string _psNo = "";
            if (dr.RowState != DataRowState.Deleted)
            {
                _psId = dr["BL_ID"].ToString();
                _psNo = dr["BL_NO"].ToString();
            }
            else
            {
                _psId = dr["BL_ID", DataRowVersion.Original].ToString();
                _psNo = dr["BL_NO", DataRowVersion.Original].ToString();
            }
            //判断是否走审核流程

            if (tableName == "MF_BLN")
            {
                //#region 审核关联
                //AudParamStruct _aps;
                //if (statementType != StatementType.Delete)
                //{
                //    _aps.BIL_DD = Convert.ToDateTime(dr["BL_DD"]);
                //    _aps.BIL_ID = dr["BL_ID"].ToString();
                //    _aps.BIL_NO = dr["BL_NO"].ToString();
                //    _aps.BIL_TYPE = dr["BIL_TYPE"].ToString();
                //    _aps.CUS_NO = dr["CUS_NO"].ToString();
                //    _aps.DEP = dr["DEP"].ToString();
                //    _aps.SAL_NO = dr["SAL_NO"].ToString();
                //    _aps.USR = dr["USR"].ToString();
                //    _aps.MOB_ID = "";
                //}
                //else
                //    _aps = new AudParamStruct(Convert.ToString(dr["BL_ID", DataRowVersion.Original]), Convert.ToString(dr["BL_NO", DataRowVersion.Original]));
                //Auditing _auditing = new Auditing();
                //string _auditErr = _auditing.AuditingBill(_isRunAuditing, _aps, statementType, dr);
                //if (!string.IsNullOrEmpty(_auditErr))
                //{
                //    throw new SunlikeException(_auditErr);
                //}
                //#endregion

            }
            else if (tableName == "TF_BLN")
            {
                if (!_isRunAuditing)
                {
                    #region 更新产品库存
                    if (statementType == StatementType.Insert)
                    {
                        this.UpdateWh(dr, true);
                        this.UpdateQtyRtn(dr, true);
                    }
                    else if (statementType == StatementType.Delete)
                    {
                        this.UpdateWh(dr, false);
                        this.UpdateQtyRtn(dr, false);
                    }
                    else if (statementType == StatementType.Update)
                    {
                        this.UpdateWh(dr, false);
                        this.UpdateQtyRtn(dr, false);
                        this.UpdateWh(dr, true);
                        this.UpdateQtyRtn(dr, true);

                        if (this._updateWhFlag.ContainsKey(dr["ITM"].ToString()))
                        {
                            this._updateWhFlag.Remove(dr["ITM"].ToString());
                        }
                    }
                    #endregion
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void BeforeDsSave(DataSet ds)
        {
            DataTable _dtMf = ds.Tables["MF_BLN"];
            DataTable _dtTf = ds.Tables["TF_BLN"];

            //#region 单据追踪
            //DataTable _dt = ds.Tables["MF_BLN"];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //    if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //    {
            //        _bilId = _dt.Rows[0]["BL_ID"].ToString();
            //    }
            //    else
            //    {
            //        _bilId = _dt.Rows[0]["BL_ID", DataRowVersion.Original].ToString();
            //    }
            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //}
            //#endregion


            if (_dtMf.Rows.Count > 0)
            {
                if (_dtMf.Rows[0].RowState == DataRowState.Modified && !_isRunAuditing)
                {
                    if ((_dtMf.Rows[0]["BL_ID"].ToString() == "BB" || _dtMf.Rows[0]["BL_ID"].ToString() == "LB")
                        && _dtMf.Rows[0]["PO_ID"].ToString() != _dtMf.Rows[0]["PO_ID", DataRowVersion.Original].ToString())
                    {
                        foreach (DataRow _drTf in _dtTf.Select(""))
                        {
                            if (_drTf.RowState == DataRowState.Unchanged || _drTf.RowState == DataRowState.Modified)
                            {
                                this._updateWhFlag.Add(_drTf["ITM"].ToString(), _dtMf.Rows[0]["PO_ID", DataRowVersion.Original].ToString());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        protected override void AfterDsSave(DataSet ds)
        {
            foreach (KeyValuePair<string, string> _kvp in this._updateWhFlag)
            {
                DataRow[] _drs = ds.Tables["TF_BLN"].Select("ITM='" + _kvp.Key + "'");
                if (_drs.Length > 0)
                {
                    this.UpdateWh(_drs[0], false, _kvp.Value);
                    this.UpdateQtyRtn(_drs[0], false, _kvp.Value);
                    this.UpdateWh(_drs[0], true);
                    this.UpdateQtyRtn(_drs[0], true);
                }
            }

            //回写受订单
            if (this._alOsNo.Count > 0)
            {
                DrpSO _so = new DrpSO();
                _so.UpdateQtyPs(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
            }


        }

        #region 更新库存
        private void UpdateWh(DataRow dr, bool IsAdd)
        {
            UpdateWh(dr, IsAdd, null);
        }
        private void UpdateWh(DataRow dr, bool IsAdd, string poId)
        {
            string _blId = "";
            string _batNo = "";
            string _validDd = "";
            string _prdNo = "";
            string _prdMark = "";
            string _whNo = "";
            string _unit = "";
            decimal _qty = 0;
            decimal _qty1 = 0;
            decimal _cst = 0;

            string _poId = "";
            string _osNo = "";
            string _osId = "";
            int _estItm = 0;

            if (IsAdd)
            {
                _blId = dr["BL_ID"].ToString();
                _poId = dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["PO_ID"].ToString();
                if (_blId == "BB" || _blId == "LB")
                {
                    _osId = (_blId == "BB" ? "BN" : "LN");
                    _osNo = dr["PRE_BL_NO"].ToString();
                    if (!String.IsNullOrEmpty(dr["EST_ITM"].ToString()))
                    {
                        _estItm = Convert.ToInt32(dr["EST_ITM"]);
                    }
                }
                else
                {
                    _osId = dr["OS_ID"].ToString();
                    _osNo = dr["OS_NO"].ToString();
                    if (!String.IsNullOrEmpty(dr["OS_ITM"].ToString()))
                    {
                        _estItm = Convert.ToInt32(dr["OS_ITM"]);
                    }
                }
                _batNo = dr["BAT_NO"].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD"].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD"]).ToString(Comp.SQLDateFormat);
                }


                _prdNo = dr["PRD_NO"].ToString();
                _prdMark = dr["PRD_MARK"].ToString();
                _whNo = dr["WH"].ToString();
                _unit = dr["UNIT"].ToString();
                if (!String.IsNullOrEmpty(dr["QTY"].ToString()))
                {
                    _qty = Convert.ToDecimal(dr["QTY"]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1"].ToString()))
                {
                    _qty1 = Convert.ToDecimal(dr["QTY1"]);
                }
                if (!String.IsNullOrEmpty(dr["CST"].ToString()))
                {
                    _cst += Convert.ToDecimal(dr["CST"]);
                }
            }
            else
            {
                _blId = dr["BL_ID", DataRowVersion.Original].ToString();
                _poId = dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["PO_ID", DataRowVersion.Original].ToString();
                if (_blId == "BB" || _blId == "LB")
                {
                    _osId = (_blId == "BB" ? "BN" : "LN");
                    _osNo = dr["PRE_BL_NO", DataRowVersion.Original].ToString();
                    if (!String.IsNullOrEmpty(dr["EST_ITM", DataRowVersion.Original].ToString()))
                    {
                        _estItm = Convert.ToInt32(dr["EST_ITM", DataRowVersion.Original]);
                    }
                }
                else
                {
                    _osId = dr["OS_ID", DataRowVersion.Original].ToString();
                    _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                    if (!String.IsNullOrEmpty(dr["OS_ITM", DataRowVersion.Original].ToString()))
                    {
                        _estItm = Convert.ToInt32(dr["OS_ITM", DataRowVersion.Original]);
                    }
                }
                _batNo = dr["BAT_NO", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["VALID_DD", DataRowVersion.Original].ToString()))
                {
                    _validDd = Convert.ToDateTime(dr["VALID_DD", DataRowVersion.Original]).ToString(Comp.SQLDateFormat);
                }

                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
                _prdMark = dr["PRD_MARK", DataRowVersion.Original].ToString();
                _whNo = dr["WH", DataRowVersion.Original].ToString();
                _unit = dr["UNIT", DataRowVersion.Original].ToString();
                if (!String.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                {
                    _qty -= Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                {
                    _qty1 -= Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                }
                if (!String.IsNullOrEmpty(dr["CST", DataRowVersion.Original].ToString()))
                {
                    _cst -= Convert.ToDecimal(dr["CST", DataRowVersion.Original]);
                }
            }

            if (_blId == "BB" || _blId == "LN")
            {
                _cst = -1 * _cst;
            }

            if (_blId == "BB" || _blId == "LB")
            {
                _qty = -1 * _qty;
                _qty1 = -1 * _qty1;
            }

            WH _wh = new WH();
            System.Collections.Hashtable _ht = new System.Collections.Hashtable();
            WH.QtyTypes _qtyType = WH.QtyTypes.QTY_BRW;
            WH.QtyTypes _qty1Type = WH.QtyTypes.QTY1_BRW;
            if (_blId == "LN" || _blId == "LB")
            {
                _qtyType = WH.QtyTypes.QTY_LRN;
                _qty1Type = WH.QtyTypes.QTY1_LRN;
            }

            if (!String.IsNullOrEmpty(_batNo))
            {
                _ht = new System.Collections.Hashtable();
                _ht[_qtyType] = _qty;
                _ht[_qty1Type] = _qty1;
                _ht[WH.QtyTypes.CST] = _cst;

                Prdt _prdt = new Prdt();
                SunlikeDataSet _ds = _prdt.GetBatRecData(_batNo, _prdNo, _prdMark, _whNo);
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
            else
            {
                _ht = new System.Collections.Hashtable();
                _ht[_qtyType] = _qty;
                _ht[_qty1Type] = _qty1;
                _ht[WH.QtyTypes.AMT_CST] = _cst;
                _wh.UpdateQty(_prdNo, _prdMark, _whNo, _unit, _ht);
            }

            if (poId != null)
            {
                _poId = poId;
            }
            if (_poId == "T" && _osId == "BN" && !String.IsNullOrEmpty(_osNo) && _estItm > 0)
            {//还出单如果影响采购单已交量，更新在途
                SunlikeDataSet _dsBN = GetBodyData(_osId, _osNo, _estItm);
                if (_dsBN.Tables[0].Rows.Count > 0)
                {
                    string _poNo = _dsBN.Tables[0].Rows[0]["OS_NO"].ToString();
                    string _poItm = _dsBN.Tables[0].Rows[0]["OS_ITM"].ToString();

                    if (!String.IsNullOrEmpty(_poNo) && !String.IsNullOrEmpty(_poItm))
                    {
                        //更新在途量
                        UpdateQtyOnWay(_poNo, Convert.ToInt32(_poItm), _prdNo, _unit, -_qty);
                    }
                }
            }

            if (_osId == "PO" && !String.IsNullOrEmpty(_osNo) && _estItm > 0)
            {
                UpdateQtyOnWay(_osNo, _estItm, _prdNo, _unit, -_qty);
            }
        }

        #endregion

        /// <summary>
        /// 更新在途量
        /// </summary>
        /// <param name="osNo"></param>
        /// <param name="estItm"></param>
        /// <param name="prdNo"></param>
        /// <param name="unit"></param>
        /// <param name="qty"></param>
        private void UpdateQtyOnWay(string osNo, int estItm, string prdNo, string unit, decimal qty)
        {
            if (qty == 0)
                return;
            string _tmpStr = "";
            Prdt _prdt = new Prdt();
            DataTable _prdTb = _prdt.GetUnit(prdNo, out _tmpStr);
            DataRow[] _prdRow = _prdTb.Select("ID='" + unit + "'");
            if (!String.IsNullOrEmpty(_prdRow[0]["VALUE"].ToString()))
            {
                qty *= Convert.ToDecimal(_prdRow[0]["VALUE"]);
            }

            DRPPO _po = new DRPPO();
            DataRow _drPo = _po.GetData("PO", osNo, estItm).Tables[0].Rows[0];
            string _backId = "";
            string _clsId = "";
            _backId = _drPo["BACK_ID"].ToString();
            _clsId = _drPo["CLS_ID"].ToString();
            //手工结案
            if (_clsId == "T" && _backId.Length == 0)
                return;
            decimal _pkQty = 1;
            _prdRow = _prdTb.Select("ID='" + _drPo["UNIT"].ToString() + "'");
            if (!String.IsNullOrEmpty(_prdRow[0]["VALUE"].ToString()))
            {
                _pkQty *= Convert.ToDecimal(_prdRow[0]["VALUE"]);
            }
            decimal _qty = Convert.ToDecimal(_drPo["QTY"]) * _pkQty;
            decimal _qtyPs = Convert.ToDecimal(_drPo["QTY_PS"]) * _pkQty;
            decimal _qtyPre = Convert.ToDecimal(_drPo["QTY_PRE"]) * _pkQty;
            decimal _oldPs = _qtyPs;
            _qty -= _qtyPre;
            _qtyPs -= qty;

            if (_oldPs > _qty && _qtyPs > _qty)
            {
                return;
            }
            else if (_oldPs > _qty && _qtyPs < _qty)
            {
                qty = _qty - _qtyPs;
            }
            else if (_oldPs < _qty && _qtyPs > _qty)
            {
                qty = _oldPs - _qty;
            }

            WH _wh = new WH();
            Hashtable _ht = new System.Collections.Hashtable();
            _ht[WH.QtyTypes.QTY_ON_WAY] = qty;
            if (String.IsNullOrEmpty(_drPo["BAT_NO", DataRowVersion.Original].ToString()))
            {
                _wh.UpdateQty(_drPo["PRD_NO"].ToString(), _drPo["PRD_MARK"].ToString(), _drPo["WH"].ToString(), "1", _ht);
            }
            else
            {
                _wh.UpdateQty(_drPo["BAT_NO", DataRowVersion.Original].ToString(), _drPo["PRD_NO"].ToString(), _drPo["PRD_MARK"].ToString(), _drPo["WH"].ToString(), "", "1", _ht);
            }
        }

        #region 更新销货单凭证号码
        /// <summary>
        /// 更新凭证号码
        /// </summary>
        /// <param name="dr">MF_PSS的数据行</param>
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
                string _blId = dr["BL_ID"].ToString();
                if (this._reBuildVohNo)
                {
                    if (!string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_updUsr = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0)
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        //借入BN
                        //借入还出BB
                        //借出LN
                        //借出还入LB
                        if (string.Compare("BN", _blId) == 0 || string.Compare("BB", _blId) == 0
                            || string.Compare("LN", _blId) == 0 || string.Compare("LB", _blId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.BN;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("","", _blId, dr["BL_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _blId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0 && string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        string _depNo = dr["DEP"].ToString();
                        CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                        bool _getVoh = false;
                        if (string.Compare("BN", _blId) == 0 || string.Compare("BB", _blId) == 0
                            || string.Compare("LN", _blId) == 0 || string.Compare("LB", _blId) == 0)
                        {
                            _getVoh = _compInfo.VoucherInfo.GenVoh.BN;
                        }
                        if (_getVoh)
                        {
                            DataSet _dsBills = dr.Table.DataSet.Copy();
                            _dsBills.Merge(this.GetData("", "", _blId, dr["BL_NO"].ToString(), false), true);
                            _dsBills.ExtendedProperties["VOH_USR"] = _updUsr;
                            dr["VOH_NO"] = _voh.BuildVoucher(_dsBills, _blId, out _vohNoError);
                            _vohNo = dr["VOH_NO"].ToString();
                        }
                    }
                    else if (string.IsNullOrEmpty(dr["VOH_ID"].ToString())&& !string.IsNullOrEmpty(dr["VOH_NO", DataRowVersion.Original].ToString()))
                    {
                        //_updUsr = _voh.DeleteVoucher(dr["VOH_NO", DataRowVersion.Original].ToString());
                        dr["VOH_NO"] = System.DBNull.Value;
                    }

                }
            }
            else if (statementType == StatementType.Insert)
            {
                string _blId = dr["BL_ID"].ToString();
                string _depNo = dr["DEP"].ToString();
                bool _getVoh = false;
                CompInfo _compInfo = Comp.GetCompInfo(_depNo);
                if (string.Compare("BN", _blId) == 0 || string.Compare("BB", _blId) == 0
                             || string.Compare("LN", _blId) == 0 || string.Compare("LB", _blId) == 0)
                {
                    _getVoh = _compInfo.VoucherInfo.GenVoh.BN;
                }
                if (_getVoh && !string.IsNullOrEmpty(dr["VOH_ID"].ToString()) && string.Compare(dr["ZHANG_ID"].ToString(), "1") == 0)
                {
                    DrpVoh _voh = new DrpVoh();
                    dr.Table.DataSet.ExtendedProperties["VOH_USR"] = _updUsr;
                    dr["VOH_NO"] = _voh.BuildVoucher(dr.Table.DataSet, _blId, out _vohNoError);
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
        /// 更新凭证号码
        /// </summary>
        /// <param name="psId"></param>
        /// <param name="psNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string blId, string blNo, string vohNo)
        {
            DbDRPBN _bn = new DbDRPBN(Comp.Conn_DB);
            _bn.UpdateVohNo(blId, blNo, vohNo);
        }
        #endregion

        #region 立帐

        /// <summary>
        /// 立帐
        /// </summary>
        /// <param name="dr">表头</param>
        private void UpdateMfArp(DataRow dr)
        {
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_BLN"];
            decimal _tax = 0;
            decimal _amtn = 0;
            decimal _amt = 0;
            if (dr.RowState != DataRowState.Deleted)
            {
                DataRow[] _darBody = _dtBody.Select();
                for (int i = 0; i < _darBody.Length; i++)
                {
                    if (!String.IsNullOrEmpty(_darBody[i]["AMTN_RNT_NET"].ToString()))
                    {
                        _amtn += Convert.ToDecimal(_darBody[i]["AMTN_RNT_NET"]);
                        if (!String.IsNullOrEmpty(_darBody[i]["TAX_RNT"].ToString()))
                        {
                            _tax += Convert.ToDecimal(_darBody[i]["TAX_RNT"]);
                        }
                    }
                    if (!String.IsNullOrEmpty(_darBody[i]["AMT_RNT"].ToString()))
                    {
                        _amt += Convert.ToDecimal(_darBody[i]["AMT_RNT"]);
                    }
                }
            }

            Arp _arp = new Arp();
            string _arpNo = "";
            //如果是删除或者是立帐方式由“单张立帐”改成“不立帐”
            //又或者立帐的情况下改变产商，要先删除原来的帐款，再新增帐款
            if (dr.RowState == DataRowState.Deleted ||
                (dr.RowState != DataRowState.Added && dr["ZHANG_ID", DataRowVersion.Original].ToString() == "1" &&
                (dr["ZHANG_ID"].ToString() != "1" || dr["CUS_NO", DataRowVersion.Original].ToString() != dr["CUS_NO"].ToString()
                || (dr.Table.DataSet.ExtendedProperties["LZ_ID"] != null && dr.Table.DataSet.ExtendedProperties["LZ_ID"].ToString() == "F"))))
            {
                _arpNo = dr["ARP_NO", DataRowVersion.Original].ToString();
                if (!_arp.DeleteMfArp(_arpNo))
                {
                    throw new Exception("RCID=INV.HINT.DELETE_ARP_FAILED,PARAM=" + _arpNo);
                }
            }
            if (dr.RowState != DataRowState.Deleted && dr["ZHANG_ID"].ToString() == "1")
            {
                if (dr.Table.DataSet.ExtendedProperties["LZ_ID"] == null || dr.Table.DataSet.ExtendedProperties["LZ_ID"].ToString() == "T")
                {
                    string _arpId = "2";
                    if (dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["BL_ID"].ToString() == "LN"
                        || dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["BL_ID"].ToString() == "LB")
                    {
                        _arpId = "1";
                    }

                    decimal _disCnt = 100;
                    //if (!String.IsNullOrEmpty(dr["DIS_CNT"].ToString()))
                    //{
                    //    _disCnt = Convert.ToDecimal(dr["DIS_CNT"]);
                    //}
                    _tax = _tax * _disCnt / 100;
                    _amtn = _amtn * _disCnt / 100;
                    _amt = _amt * _disCnt / 100;
                    decimal _excRto = 0;
                    if (!String.IsNullOrEmpty(dr["EXC_RTO"].ToString()))
                        _excRto = Convert.ToDecimal(dr["EXC_RTO"]);
                    _arpNo = _arp.UpdateMfArp(_arpId, "2", dr["BL_ID"].ToString(), dr["BL_NO"].ToString(), Convert.ToDateTime(dr["BL_DD"]),
                        dr["BIL_TYPE"].ToString(), dr["DEP"].ToString(), dr["USR"].ToString(), dr["CUR_ID"].ToString(), _excRto,
                        _amtn + _tax, _amtn, _amt, _tax, dr, "");
                    //if (_arpNo != dr["ARP_NO"].ToString())
                    {
                        //DbDRPBN _obj = new DbDRPBN(Comp.Conn_DB);
                        //_obj.UpdateArpNo(dr["BL_ID"].ToString(),dr["BL_NO"].ToString(),_arpNo);
                        dr["ARP_NO"] = _arpNo;
                    }
                }
            }
        }

        #endregion

        #region 更新月余额档

        private void UpdateSarp(DataRow dr)
        {
            DataTable _dtBody = dr.Table.DataSet.Tables["TF_BLN"];
            int _year = System.DateTime.Now.Year;
            int _month = System.DateTime.Now.Month;
            string _psID = "";
            string _cusNo = "";
            decimal _up, _qty;
            decimal _amtn = 0;
            decimal _disCnt = 100;
            string _arpId = "";
            Arp _arp = new Arp();
            //如果是删除或者是立帐方式由“单张立帐”改成“不立帐”
            //又或者立帐的情况下改变产商或立帐方式，要先删除原来的帐款
            if (dr.RowState != DataRowState.Added)
            {
                _year = Convert.ToDateTime(dr["BL_DD", DataRowVersion.Original]).Year;
                _month = Convert.ToDateTime(dr["BL_DD", DataRowVersion.Original]).Month;
                _psID = dr["BL_ID", DataRowVersion.Original].ToString();
                _cusNo = dr["CUS_NO", DataRowVersion.Original].ToString();
                //if (!String.IsNullOrEmpty(dr["DIS_CNT", DataRowVersion.Original].ToString()))
                //{
                //    _disCnt = Convert.ToDecimal(dr["DIS_CNT", DataRowVersion.Original]);
                //}
                _arpId = (_psID == "LN" || _psID == "LB") ? "1" : "2";

                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    if (_dtBody.Rows[i].RowState == DataRowState.Added)
                    {
                        continue;
                    }
                    //if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP", DataRowVersion.Original].ToString()))
                    //{
                    //    _up = 0;
                    //}
                    //else
                    //{
                    //    _up = Convert.ToDecimal(_dtBody.Rows[i]["UP", DataRowVersion.Original]);
                    //}
                    //if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY", DataRowVersion.Original].ToString()))
                    //{
                    //    _qty = 0;
                    //}
                    //else
                    //{
                    //    _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY", DataRowVersion.Original]);
                    //}
                    //_amtn += _up * _qty;

                    if (!String.IsNullOrEmpty(_dtBody.Rows[i]["AMT_RNT", DataRowVersion.Original].ToString()))
                    {
                        _amtn += Convert.ToDecimal(_dtBody.Rows[i]["AMT_RNT", DataRowVersion.Original]);
                    }

                    if (!String.IsNullOrEmpty(_dtBody.Rows[i]["TAX_RNT", DataRowVersion.Original].ToString()))
                    {
                        _amtn += Convert.ToDecimal(_dtBody.Rows[i]["TAX_RNT", DataRowVersion.Original]);
                    }
                }
                if (_psID == "BB" || _psID == "BN")
                {
                    _amtn *= -1;
                }
                _amtn *= _disCnt / 100;
                _arp.UpdateSarp(_arpId, _year, _cusNo, _month, "", "AMTN_INV", _amtn);
            }
            if (dr.RowState != DataRowState.Deleted && dr["ZHANG_ID"].ToString() == "1")
            {
                if (dr.Table.DataSet.ExtendedProperties["LZ_ID"] == null || dr.Table.DataSet.ExtendedProperties["LZ_ID"].ToString() == "T")
                {

                    _up = 0;
                    _qty = 0;
                    _amtn = 0;
                    _year = Convert.ToDateTime(dr["BL_DD"]).Year;
                    _month = Convert.ToDateTime(dr["BL_DD"]).Month;
                    _psID = dr["BL_ID"].ToString();
                    _cusNo = dr["CUS_NO"].ToString();
                    //if (!String.IsNullOrEmpty(dr["DIS_CNT"].ToString()))
                    //{
                    //    _disCnt = Convert.ToDecimal(dr["DIS_CNT"]);
                    //}
                    _arpId = (_psID == "LN" || _psID == "LB") ? "1" : "2";
                    for (int i = 0; i < _dtBody.Rows.Count; i++)
                    {
                        if (_dtBody.Rows[i].RowState == DataRowState.Deleted)
                        {
                            //if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP", DataRowVersion.Original].ToString()))
                            //{
                            //    _up = 0;
                            //}
                            //else
                            //{
                            //    _up = Convert.ToDecimal(_dtBody.Rows[i]["UP", DataRowVersion.Original]);
                            //}
                            //if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY", DataRowVersion.Original].ToString()))
                            //{
                            //    _qty = 0;
                            //}
                            //else
                            //{
                            //    _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY", DataRowVersion.Original]);
                            //}
                            //_amtn -= _up * _qty;

                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["AMT_RNT", DataRowVersion.Original].ToString()))
                            {
                                _amtn -= Convert.ToDecimal(_dtBody.Rows[i]["AMT_RNT", DataRowVersion.Original]);
                            }

                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["TAX_RNT", DataRowVersion.Original].ToString()))
                            {
                                _amtn -= Convert.ToDecimal(_dtBody.Rows[i]["TAX_RNT", DataRowVersion.Original]);
                            }
                        }
                        else
                        {
                            //if (String.IsNullOrEmpty(_dtBody.Rows[i]["UP"].ToString()))
                            //{
                            //    _up = 0;
                            //}
                            //else
                            //{
                            //    _up = Convert.ToDecimal(_dtBody.Rows[i]["UP"]);
                            //}
                            //if (String.IsNullOrEmpty(_dtBody.Rows[i]["QTY"].ToString()))
                            //{
                            //    _qty = 0;
                            //}
                            //else
                            //{
                            //    _qty = Convert.ToDecimal(_dtBody.Rows[i]["QTY"]);
                            //}
                            //_amtn += _up * _qty;

                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["AMT_RNT"].ToString()))
                            {
                                _amtn += Convert.ToDecimal(_dtBody.Rows[i]["AMT_RNT"]);
                            }

                            if (!String.IsNullOrEmpty(_dtBody.Rows[i]["TAX_RNT"].ToString()))
                            {
                                _amtn += Convert.ToDecimal(_dtBody.Rows[i]["TAX_RNT"]);
                            }
                        }
                    }
                    if (_psID == "LB" || _psID == "LN")
                    {
                        _amtn *= -1;
                    }
                    _amtn *= _disCnt / 100;
                    _arp.UpdateSarp(_arpId, _year, _cusNo, _month, "", "AMTN_INV", _amtn);
                }
            }
        }

        #endregion

        #region 回写已交量
        /// <summary>
        /// 回写已交量
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="preItm"></param>
        /// <param name="qty"></param>
        public void UpdateQtyRtn(string blId, string blNo, string preItm, decimal qty)
        {
            if (!String.IsNullOrEmpty(blId) && !String.IsNullOrEmpty(blNo) && !String.IsNullOrEmpty(preItm))
            {
                DbDRPBN _dbBn = new DbDRPBN(Comp.Conn_DB);
                _dbBn.UpdateQtyRtn(blId, blNo, preItm, qty, blId=="PO"?"QTY_PS":"QTY_RTN");
            }
        }

        /// <summary>
        /// 取未还数量
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public Decimal GetQtyNotRtn(string blId, string blNo, int preItm)
        {
            DbDRPBN _obj = new DbDRPBN(Comp.Conn_DB);
            return _obj.GetQtyNotRtn(blId, blNo, preItm);
        }

        /// <summary>
        /// 回写已交量,用于借入转进货
        /// </summary>
        /// <param name="bl_no"></param>
        /// <param name="est_itm"></param>
        /// <param name="prdNo"></param>
        /// <param name="unit"></param>
        /// <param name="qty_rtn"></param>
        public void UpdateQtyRtn(ArrayList bl_no, ArrayList est_itm, ArrayList prdNo, ArrayList unit, ArrayList qty_rtn, ArrayList qty1_rtn)
        {
            if (bl_no.Count != est_itm.Count || bl_no.Count != qty_rtn.Count || est_itm.Count != qty_rtn.Count)
            {
                throw new Sunlike.Common.Utility.SunlikeException("修改已还数量传入值不正确");
            }
            string blId = "";

            //创建新表记录传入信息
            #region 创建新表记录传入信息
            DataTable _fromBL = new DataTable();
            _fromBL.Columns.Add("BL_ID", typeof(System.String));
            _fromBL.Columns.Add("BL_NO", typeof(System.String));
            _fromBL.Columns.Add("PRE_ITM", typeof(System.Int32));
            _fromBL.Columns.Add("PRD_NO", typeof(System.String));
            _fromBL.Columns.Add("UNIT", typeof(System.Int32));
            _fromBL.Columns.Add("QTY_RTN", typeof(System.Decimal));
            _fromBL.Columns.Add("QTY1_RTN", typeof(System.Decimal));
            for (int i = 0; i < bl_no.Count; i++)
            {
                blId = bl_no[i].ToString().Substring(0, 2);
                DataRow[] _selBl = _fromBL.Select("BL_ID='" + blId + "' AND BL_NO='" + bl_no[i] + "' AND PRE_ITM='" + est_itm[i] + "'");
                if (_selBl == null || _selBl.Length == 0)
                {
                    DataRow _newDr = _fromBL.NewRow();
                    _newDr["BL_ID"] = blId;
                    _newDr["BL_NO"] = bl_no[i];
                    _newDr["PRE_ITM"] = Convert.ToInt32(est_itm[i]);
                    _newDr["PRD_NO"] = prdNo[i];
                    _newDr["UNIT"] = Convert.ToInt32(unit[i]);
                    _newDr["QTY_RTN"] = Convert.ToDecimal(qty_rtn[i]);
                    _newDr["QTY1_RTN"] = Convert.ToDecimal(qty1_rtn[i]);
                    _fromBL.Rows.Add(_newDr);
                }
                else
                {
                    _selBl[0]["QTY_RTN"] = Convert.ToDecimal(_selBl[0]["QTY_RTN"]) + Convert.ToDecimal(qty_rtn[i]);
                    _selBl[0]["QTY1_RTN"] = Convert.ToDecimal(_selBl[0]["QTY1_RTN"]) + Convert.ToDecimal(qty1_rtn[i]);
                }
            }
            _fromBL.AcceptChanges();
            #endregion

            #region 读取数据
            SunlikeDataSet _dsBn = new SunlikeDataSet();
            DbDRPBN _dbDrpBn = new DbDRPBN(Comp.Conn_DB);
            for (int i = 0; i < bl_no.Count; i++)
            {
                _dsBn.Merge(_dbDrpBn.GetBodyData(blId, bl_no[i].ToString(), Convert.ToInt32(est_itm[i])));
            }
            DataTable _dtTfBn = _dsBn.Tables["TF_BLN"];

            #endregion

            #region	 修改已还量
            string _bl_id = "";
            string _bl_no = "";
            string _prdNo = "";
            int _iUnit = 1;
            decimal _qty_rtn = 0;
            decimal _qty1_rtn = 0;
            decimal _qty = 0;
            decimal _qty1 = 0;
            for (int i = 0; i < _fromBL.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(_fromBL.Rows[i]["BL_ID"].ToString()))
                    _bl_id = _fromBL.Rows[i]["BL_ID"].ToString();
                if (!String.IsNullOrEmpty(_fromBL.Rows[i]["BL_NO"].ToString()))
                    _bl_no = _fromBL.Rows[i]["BL_NO"].ToString();
                int _est_itm = 0;
                if (!String.IsNullOrEmpty(_fromBL.Rows[i]["PRE_ITM"].ToString()))
                    _est_itm = Convert.ToInt32(_fromBL.Rows[i]["PRE_ITM"]);
                if (!String.IsNullOrEmpty(_fromBL.Rows[i]["PRD_NO"].ToString()))
                    _prdNo = _fromBL.Rows[i]["PRD_NO"].ToString();
                if (!String.IsNullOrEmpty(_fromBL.Rows[i]["UNIT"].ToString()))
                    _iUnit = Convert.ToInt32(_fromBL.Rows[i]["UNIT"]);
                if (!String.IsNullOrEmpty(_fromBL.Rows[i]["QTY_RTN"].ToString()))
                    _qty_rtn = Convert.ToDecimal(_fromBL.Rows[i]["QTY_RTN"]);
                if (!String.IsNullOrEmpty(_fromBL.Rows[i]["QTY1_RTN"].ToString()))
                    _qty1_rtn = Convert.ToDecimal(_fromBL.Rows[i]["QTY1_RTN"]);
                Hashtable _ht = new Hashtable();
                _ht["TableName"] = "TF_BLN";
                _ht["IdName"] = "BL_ID";
                _ht["NoName"] = "BL_NO";
                _ht["ItmName"] = "PRE_ITM";
                _ht["OsID"] = _bl_id;
                _ht["OsNO"] = _bl_no;
                _ht["KeyItm"] = _est_itm;
                _qty_rtn = INVCommon.GetRtnQty(_prdNo, _qty_rtn, _iUnit, _ht);
                _qty1_rtn = INVCommon.GetRtnQty(_prdNo, _qty1_rtn, _iUnit, _ht);
                _dbDrpBn.UpdateQtyRtn(_bl_id, _bl_no, _est_itm.ToString(), _qty_rtn, "QTY_RTN");
                _dbDrpBn.UpdateQtyRtn(_bl_id, _bl_no, _est_itm.ToString(), _qty1_rtn, "QTY1_RTN");
            }
            #endregion
            //修改货品分仓存量的借入量
            WH _wh = new WH();
            #region 修改货品分仓存量的已借入量
            for (int i = 0; i < _dtTfBn.Rows.Count; i++)
            {
                string _prd_no = "";
                string _prd_mark = "";
                string _swh = "";
                string _unit = "";
                string _batNo = "";
                string _validDd = "";
                string _blId = "";
                _qty_rtn = 0;
                _qty1_rtn = 0;
                _qty = 0;
                _qty1 = 0;
                _prd_no = _dtTfBn.Rows[i]["PRD_NO"].ToString();
                _prd_mark = _dtTfBn.Rows[i]["PRD_MARK"].ToString();
                _swh = _dtTfBn.Rows[i]["WH"].ToString();
                _unit = _dtTfBn.Rows[i]["UNIT"].ToString();
                _batNo = _dtTfBn.Rows[i]["BAT_NO"].ToString();
                _blId = _dtTfBn.Rows[i]["BL_ID"].ToString();
                if (!String.IsNullOrEmpty(_dtTfBn.Rows[i]["VALID_DD"].ToString()))
                {
                    _validDd = Convert.ToDateTime(_dtTfBn.Rows[i]["VALID_DD"]).ToString(Comp.SQLDateFormat);
                }

                if (!String.IsNullOrEmpty(_dtTfBn.Rows[i]["QTY"].ToString()))
                    _qty = Convert.ToDecimal(_dtTfBn.Rows[i]["QTY"]);
                if (!String.IsNullOrEmpty(_dtTfBn.Rows[i]["QTY1"].ToString()))
                    _qty1 = Convert.ToDecimal(_dtTfBn.Rows[i]["QTY1"]);
                DataRow[] _selFormBL = _fromBL.Select("BL_ID='" + _dtTfBn.Rows[i]["BL_ID"].ToString() + "' AND BL_NO='" + _dtTfBn.Rows[i]["BL_NO"].ToString() + "' AND PRE_ITM='" + _dtTfBn.Rows[i]["PRE_ITM"].ToString() + "' ");

                if ((_selFormBL.Length > 0) && (!String.IsNullOrEmpty(_selFormBL[0]["QTY_RTN"].ToString())))
                {
                    _qty_rtn = Convert.ToDecimal(_selFormBL[0]["QTY_RTN"]);
                }
                if ((_selFormBL.Length > 0) && (!String.IsNullOrEmpty(_selFormBL[0]["QTY1_RTN"].ToString())))
                {
                    _qty1_rtn = Convert.ToDecimal(_selFormBL[0]["QTY1_RTN"]);
                }
                if (_qty_rtn > 0)
                {
                    //扣除已还量
                    if (!String.IsNullOrEmpty(_dtTfBn.Rows[i]["QTY_RTN"].ToString()))
                    {
                        if (_qty > Convert.ToDecimal(_dtTfBn.Rows[i]["QTY_RTN"]))
                        {
                            _qty -= Convert.ToDecimal(_dtTfBn.Rows[i]["QTY_RTN"]);
                        }
                        else
                        {
                            _qty = 0;
                        }
                    }
                }
                else
                {
                    //取得已还量
                    if (!String.IsNullOrEmpty(_dtTfBn.Rows[i]["QTY_RTN"].ToString()))
                    {
                        _qty = System.Math.Abs(_qty_rtn) - Convert.ToDecimal(_dtTfBn.Rows[i]["QTY_RTN"]) + _qty;
                        if (_qty < 0)
                        {
                            //return;
                        }

                    }
                }

                if (_qty1_rtn > 0)
                {
                    //扣除已还量
                    if (!String.IsNullOrEmpty(_dtTfBn.Rows[i]["QTY1_RTN"].ToString()))
                    {
                        if (_qty1 > Convert.ToDecimal(_dtTfBn.Rows[i]["QTY1_RTN"]))
                        {
                            _qty1 -= Convert.ToDecimal(_dtTfBn.Rows[i]["QTY1_RTN"]);
                        }
                        else
                        {
                            _qty1 = 0;
                        }
                    }
                }
                else
                {
                    //取得已还量
                    if (!String.IsNullOrEmpty(_dtTfBn.Rows[i]["QTY1_RTN"].ToString()))
                    {
                        _qty1 = System.Math.Abs(_qty1_rtn) - Convert.ToDecimal(_dtTfBn.Rows[i]["QTY1_RTN"]) + _qty1;
                        if (_qty1 < 0)
                        {
                            //return;
                        }

                    }
                }


                bool _updateQty = true;
                if (_qty < 0)
                {
                    _updateQty = false;
                }
                bool _updateQty1 = true;
                if (_qty1 < 0)
                {
                    _updateQty1 = false;
                }
                if (System.Math.Abs(_qty_rtn) > _qty)
                {
                    if (_qty_rtn < 0)
                        _qty_rtn = (-1) * _qty;
                    else
                        _qty_rtn = _qty;
                }

                if (System.Math.Abs(_qty1_rtn) > _qty1)
                {
                    if (_qty1_rtn < 0)
                        _qty1_rtn = (-1) * _qty1;
                    else
                        _qty1_rtn = _qty1;
                }

                //修改货品分仓存量的借入量
                WH.QtyTypes _qtyType = WH.QtyTypes.QTY_BRW;
                WH.QtyTypes _qtyType1 = WH.QtyTypes.QTY1_BRW;
                if (_blId == "LN")
                {
                    _qtyType = WH.QtyTypes.QTY_LRN;
                    _qtyType1 = WH.QtyTypes.QTY1_LRN;
                }
                if (_batNo.Length == 0)
                {
                    if (_updateQty)
                        _wh.UpdateQty(_prd_no, _prd_mark, _swh, _unit, _qtyType, _qty_rtn * (-1));
                    if (_updateQty1)
                        _wh.UpdateQty(_prd_no, _prd_mark, _swh, _unit, _qtyType1, _qty1_rtn * (-1));
                }
                else
                {
                    System.Collections.Hashtable _ht = new Hashtable();
                    if (_updateQty)
                        _ht[_qtyType] = _qty_rtn * (-1);
                    if (_updateQty1)
                        _ht[_qtyType1] = _qty1_rtn * (-1);
                    if (_ht.Count > 0)
                        _wh.UpdateQty(_batNo, _prd_no, _prd_mark, _swh, _validDd, _unit, _ht);
                }
            }  //end for 
            #endregion
        }

        /// <summary>
        /// 回写已交量
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="IsAdd"></param>
        /// <param name="poId"></param>
        private void UpdateQtyRtn(DataRow dr, bool IsAdd, string poId)
        {
            string _osID, _osNo, _poID, _bilId;
            int _preItm = 0;
            decimal _qty = 0;
            decimal _qty1 = 0;
            int _unit = 1;
            string _prdNo = "";
            if (IsAdd)
            {
                _bilId = dr["BL_ID"].ToString();
                if (_bilId == "BB" || _bilId == "LB")
                {
                    _osID = _bilId == "BB" ? "BN" : "LN";
                    _osNo = dr["PRE_BL_NO"].ToString();
                    if (!String.IsNullOrEmpty(dr["EST_ITM"].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["EST_ITM"]);
                    }
                }
                else
                {
                    _osID = dr["OS_ID"].ToString();
                    _osNo = dr["OS_NO"].ToString();
                    if (!String.IsNullOrEmpty(dr["OS_ITM"].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["OS_ITM"]);
                    }
                }
                _poID = dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["PO_ID"].ToString();
                _prdNo = dr["PRD_NO"].ToString();


                if (!String.IsNullOrEmpty(_osNo))
                {
                    if (!string.IsNullOrEmpty(dr["QTY"].ToString()))
                    {
                        _qty = Convert.ToDecimal(dr["QTY"]);
                    }
                    if (!string.IsNullOrEmpty(dr["QTY1"].ToString()))
                    {
                        _qty1 = Convert.ToDecimal(dr["QTY1"]);
                    }
                }
                if (!String.IsNullOrEmpty(dr["UNIT"].ToString()))
                {
                    _unit = Convert.ToInt32(dr["UNIT"]);
                }
            }
            else
            {
                _bilId = dr["BL_ID", DataRowVersion.Original].ToString();
                if (_bilId == "BB" || _bilId == "LB")
                {
                    _osID = _bilId == "BB" ? "BN" : "LN";
                    _osNo = dr["PRE_BL_NO", DataRowVersion.Original].ToString();
                    if (!String.IsNullOrEmpty(dr["EST_ITM", DataRowVersion.Original].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["EST_ITM", DataRowVersion.Original]);
                    }
                }
                else
                {
                    _osID = dr["OS_ID", DataRowVersion.Original].ToString();
                    _osNo = dr["OS_NO", DataRowVersion.Original].ToString();
                    if (!String.IsNullOrEmpty(dr["OS_ITM", DataRowVersion.Original].ToString()))
                    {
                        _preItm = Convert.ToInt32(dr["OS_ITM", DataRowVersion.Original]);
                    }
                }

                _poID = dr.Table.DataSet.Tables["MF_BLN"].Rows[0]["PO_ID", DataRowVersion.Original].ToString();
                _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();

                if (!String.IsNullOrEmpty(_osNo))
                {
                    if (!string.IsNullOrEmpty(dr["QTY", DataRowVersion.Original].ToString()))
                    {
                        _qty -= Convert.ToDecimal(dr["QTY", DataRowVersion.Original]);
                    }
                    if (!string.IsNullOrEmpty(dr["QTY1", DataRowVersion.Original].ToString()))
                    {
                        _qty1 -= Convert.ToDecimal(dr["QTY1", DataRowVersion.Original]);
                    }
                }
                if (!String.IsNullOrEmpty(dr["UNIT", DataRowVersion.Original].ToString()))
                {
                    _unit = Convert.ToInt32(dr["UNIT", DataRowVersion.Original]);
                }
            }

            if ((_osID == "BN" || _osID == "LN" || _osID == "PO") && !String.IsNullOrEmpty(_osNo) && _preItm > 0)
            {
                Hashtable _ht = new Hashtable();
                if (_osID == "BN" || _osID == "LN")
                {//回写借入单
                    _ht["TableName"] = "TF_BLN";
                    _ht["IdName"] = "BL_ID";
                    _ht["NoName"] = "BL_NO";
                }
                else
                {//回写采购单
                    _ht["TableName"] = "TF_POS";
                    _ht["IdName"] = "OS_ID";
                    _ht["NoName"] = "OS_NO";
                }
                _ht["ItmName"] = "PRE_ITM";
                _ht["OsID"] = _osID;
                _ht["OsNO"] = _osNo;
                _ht["KeyItm"] = _preItm;

                _qty = INVCommon.GetRtnQty(_prdNo, _qty, _unit, _ht);
                _qty1 = INVCommon.GetRtnQty(_prdNo, _qty1, _unit, _ht);


                DbDRPBN _dbDrpBn = new DbDRPBN(Comp.Conn_DB);
                _dbDrpBn.UpdateQtyRtn(_osID, _osNo, _preItm.ToString(), _qty, _osID == "PO" ? "QTY_PS" : "QTY_RTN");

                if (poId != null)
                {
                    _poID = poId;
                }
                if (_osID == "BN" && _poID == "T")
                { //是否影响采购单已交量

                    //取借入单资料
                    SunlikeDataSet _dsBN = GetBodyData(_osID, _osNo, _preItm);
                    if (_dsBN.Tables[0].Rows.Count > 0)
                    {
                        string _no = _dsBN.Tables[0].Rows[0]["OS_NO"].ToString();
                        string _id = _dsBN.Tables[0].Rows[0]["OS_ID"].ToString();
                        string _itm = _dsBN.Tables[0].Rows[0]["OS_ITM"].ToString();

                        if (!String.IsNullOrEmpty(_no) && !String.IsNullOrEmpty(_itm) && _id == "PO")
                        {
                            _ht["TableName"] = "TF_POS";
                            _ht["IdName"] = "OS_ID";
                            _ht["NoName"] = "OS_NO";

                            _ht["ItmName"] = "PRE_ITM";
                            _ht["OsID"] = _id;
                            _ht["OsNO"] = _no;
                            _ht["KeyItm"] = _itm;
                            _qty = INVCommon.GetRtnQty(_prdNo, _qty, _unit, _ht);
                            _dbDrpBn.UpdateQtyRtn(_id, _no, _itm, -_qty, _id == "PO" ? "QTY_PS" : "QTY_RTN");
                        }
                    }
                }
                else if (_osID == "LN" && _poID == "T")
                {//是否影响受订单已交量
                    //取借入单资料
                    SunlikeDataSet _dsLN = GetBodyData(_osID, _osNo, _preItm);
                    if (_dsLN.Tables[0].Rows.Count > 0)
                    {

                        string _no = _dsLN.Tables[0].Rows[0]["OS_NO"].ToString();
                        string _id = _dsLN.Tables[0].Rows[0]["OS_ID"].ToString();
                        string _itm = _dsLN.Tables[0].Rows[0]["OS_ITM"].ToString();
                        if (!String.IsNullOrEmpty(_no) && !String.IsNullOrEmpty(_itm) && _id == "SO")
                        {
                            this._alOsNo.Add(_no);
                            this._alOsItm.Add(_itm);
                            this._alPrdNo.Add(_prdNo);
                            this._alUnit.Add(_unit);
                            this._alQty.Add(_qty * -1);
                            this._alQty1.Add(_qty1 * -1);
                        }
                    }
                }
            }
            else if (_osID == "SO" && !String.IsNullOrEmpty(_osNo) && _preItm > 0)
            {
                this._alOsNo.Add(_osNo);
                this._alOsItm.Add(_preItm);
                this._alPrdNo.Add(_prdNo);
                this._alUnit.Add(_unit);
                this._alQty.Add(_qty);
                this._alQty1.Add(_qty1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="IsAdd"></param>
        private void UpdateQtyRtn(DataRow dr, bool IsAdd)
        {
            UpdateQtyRtn(dr, IsAdd, null);
        }
        #endregion


        #region UpdateBarCode
        /// <summary>
        /// 用于BN,BB
        /// </summary>
        /// <param name="ChangedDS"></param>
        private void UpdateBarCodeB(SunlikeDataSet ChangedDS)
        {
            DataRow _drHead = ChangedDS.Tables["MF_BLN"].Rows[0];
            DataTable _dtBody = ChangedDS.Tables["TF_BLN"];
            //查找表身有修改过库位的记录
            System.Collections.Hashtable _htKeyItm = new System.Collections.Hashtable();
            for (int i = 0; i < _dtBody.Rows.Count; i++)
            {
                if (_dtBody.Rows[i].RowState == DataRowState.Modified
                    && (_dtBody.Rows[i]["WH"].ToString() != _dtBody.Rows[i]["WH", DataRowVersion.Original].ToString()
                    || _dtBody.Rows[i]["BAT_NO"].ToString() != _dtBody.Rows[i]["BAT_NO", DataRowVersion.Original].ToString()))
                {
                    _htKeyItm[_dtBody.Rows[i]["PRE_ITM"].ToString()] = "";
                }
            }
            //查找BAR_CODE
            DataTable _dtBarCode;
            if ((_drHead.RowState == DataRowState.Modified || _drHead.RowState == DataRowState.Unchanged) && !this._auditBarCode)
            {
                string _sql = "select BL_ID,BL_NO,BL_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO from TF_BLN_B"
                    + " where BL_ID='" + _drHead["BL_ID"].ToString() + "' and BL_NO='" + _drHead["BL_NO"].ToString() + "'";
                Query _query = new Query();
                SunlikeDataSet _dsQuery = _query.DoSQLString(_sql);
                _dsQuery.Tables[0].TableName = "TF_BLN_B";
                //				_dsQuery.Merge(ChangedDS.Tables["TF_PSS3"],false,MissingSchemaAction.AddWithKey);
                //				_dtBarCode = _dsQuery.Tables["TF_PSS3"];
                ChangedDS.Merge(_dsQuery.Tables["TF_BLN_B"], true, MissingSchemaAction.AddWithKey);
                _dtBarCode = ChangedDS.Tables["TF_BLN_B"];
            }
            else
            {
                _dtBarCode = ChangedDS.Tables["TF_BLN_B"];
            }
            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            for (int i = 0; i < _dtBarCode.Rows.Count; i++)
            {
                if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["BL_ITM"].ToString()] != null)
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
            //有新增/修改过BAR_CODE，则修改BAR_REC表
            if (_sb.Length > 0)
            {
                _sb.Insert(0, "BAR_NO in (");
                _sb.Append(")");
                //把条件语句分段
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
                //从资料库读取序列号资料
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
                //取表头资料
                string _cusNo, _bilID, _bilNo, _usr;
                DateTime _bilDd = DateTime.Today;
                if (_drHead.RowState == DataRowState.Deleted)
                {
                    _cusNo = _drHead["CUS_NO", DataRowVersion.Original].ToString();
                    _bilID = _drHead["BL_ID", DataRowVersion.Original].ToString();
                    _bilNo = _drHead["BL_NO", DataRowVersion.Original].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["BL_DD", DataRowVersion.Original]);
                    _usr = _drHead["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _cusNo = _drHead["CUS_NO"].ToString();
                    _bilID = _drHead["BL_ID"].ToString();
                    _bilNo = _drHead["BL_NO"].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["BL_DD"]);
                    _usr = _drHead["USR"].ToString();
                }
                //取得该单据做过的change
                DataTable _dtBarChange = null;
                if (_drHead.RowState != DataRowState.Added)
                {
                    _dtBarChange = _bar.GetBarChange(_bilID, _bilNo);
                }
                //更新BAR_REC表
                System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
                System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
                System.Collections.ArrayList _alBoxNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alWhNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alStop = new System.Collections.ArrayList();
                System.Text.StringBuilder _sbChange = new System.Text.StringBuilder();
                double _total = 0;
                for (int i = 0; i < _dtBarCode.Rows.Count; i++)
                {
                    if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["BL_ITM"].ToString()] != null)
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
                            _keyItm = _dtBarCode.Rows[i]["BL_ITM", DataRowVersion.Original].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _keyItm, "", DataViewRowState.CurrentRows | DataViewRowState.OriginalRows);
                            if (_dra[0].RowState == DataRowState.Deleted)
                            {
                                _whNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _batNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                                if (_dra[0]["BL_ID", DataRowVersion.Original].ToString() == "BB" || _dra[0]["BL_ID", DataRowVersion.Original].ToString() == "LN")
                                {
                                    _isPlus = false;
                                }
                            }
                            else
                            {
                                _batNo = _dra[0]["BAT_NO"].ToString();
                                _whNo = _dra[0]["WH"].ToString();
                                if (_dra[0]["BL_ID"].ToString() == "BB" || _dra[0]["BL_ID"].ToString() == "LN")
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
                            _keyItm = _dtBarCode.Rows[i]["BL_ITM"].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _keyItm);
                            _whNo = _dra[0]["WH"].ToString();
                            _batNo = _dra[0]["BAT_NO"].ToString();
                            if (_dra[0].RowState == DataRowState.Modified)
                            {
                                _oldWhNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _oldBatNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                            }
                            if (_dra[0]["BL_ID"].ToString() == "BB" || _dra[0]["BL_ID"].ToString() == "LN")
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
                                //如果出货库位与序列号所在的库位不同，则要写入BAR_CHANGE
                                //调增：只要bar_rec里有此序列号，且库位不为空，都要写入bar_change
                                //调减：只要bar_rec里有此序列号，且库位不为空，且调减的库位不等于当前序列号所在库位，则写入bar_change
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
                                        _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
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
                                                _sbChange.Append(_bar.InsertBarChange(_barCode, _changeWh1, _whNo, _bilID, _bilNo, _usr, _changeBatNo1, _batNo, _changePhFlag1, "", true));
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
                                        //BAR_REC.WH必须和单据库位一致，且BAR_REC.STOP_ID='F'，否则不允许删除
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
                                            _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
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
                                            _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
                                        }
                                    }
                                    //如果有做过bar_change，则库位回到做bar_change之前的地方，且把bar_change的记录删掉
                                    if (_draBarChange.Length > 0)
                                    {
                                        _boxWh = _draBarChange[0]["WH1"].ToString();
                                        _drBarRec["WH"] = _boxWh;
                                        _drBarRec["BAT_NO"] = _draBarChange[0]["BAT_NO1"].ToString();
                                        _drBarRec["STOP_ID"] = "T";
                                        //到货确认标记
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
                                //如果B0X_NO不一样，则要做自动拆箱
                                if (_drBarRec["BOX_NO"].ToString() != _boxNo)
                                {
                                    if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
                                    {
                                        _aryBoxNo[i] = _drBarRec["BOX_NO"].ToString().Trim();
                                    }
                                    else
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.DOUBLEBOX,PARAM=" + _barCode);//序列号" + _barCode + "已经装箱，你不能拆箱录入！
                                    }
                                }
                                else if (!String.IsNullOrEmpty(_boxNo))
                                {
                                    if (_htBoxNo[_boxNo] == null)
                                    {
                                        _htBoxNo[_boxNo] = "";
                                        _alBoxNo.Add(_boxNo);
                                        if (_isPlus)
                                        {
                                            if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                                            {
                                                _alWhNo.Add(_boxWh);
                                            }
                                            else
                                            {
                                                _alWhNo.Add(_whNo);
                                            }
                                            _alStop.Add("F");
                                        }
                                        else
                                        {
                                            _alWhNo.Add(null);
                                            if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                                            {
                                                _alStop.Add("F");
                                            }
                                            else
                                            {
                                                _alStop.Add("T");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
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
                    //写入序列号变更记录
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
                    DataTable _dtError = _bar.UpdateRec(_dsBar);
                    if (_dtError.Rows.Count > 0)
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.BOXERROR");//更新序列号记录失败！
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
                    //修改BAR_BOX
                    _bar.UpdateBarBox(_alBoxNo, _alWhNo, _alStop);
                    //拆箱
                    _bar.DeleteBox(_aryBoxNo, _usr, _bilID, _bilNo);
                }
                else
                {
                    throw new SunlikeException(_sbError.ToString());
                }
            }
        }

        /// <summary>
        /// 用于LN,LB
        /// </summary>
        /// <param name="ChangedDS"></param>
        private void UpdateBarCodeL(SunlikeDataSet ChangedDS)
        {
            DataRow _drHead = ChangedDS.Tables["MF_BLN"].Rows[0];
            DataTable _dtBody = ChangedDS.Tables["TF_BLN"];
            //查找表身有修改过库位的记录
            System.Collections.Hashtable _htKeyItm = new System.Collections.Hashtable();
            for (int i = 0; i < _dtBody.Rows.Count; i++)
            {
                if (_dtBody.Rows[i].RowState == DataRowState.Modified
                    && (_dtBody.Rows[i]["WH"].ToString() != _dtBody.Rows[i]["WH", DataRowVersion.Original].ToString()
                    || _dtBody.Rows[i]["BAT_NO"].ToString() != _dtBody.Rows[i]["BAT_NO", DataRowVersion.Original].ToString()))
                {
                    _htKeyItm[_dtBody.Rows[i]["PRE_ITM"].ToString()] = "";
                }
            }
            //查找BAR_CODE
            DataTable _dtBarCode;
            if ((_drHead.RowState == DataRowState.Modified || _drHead.RowState == DataRowState.Unchanged) && !this._auditBarCode)
            {
                string _sql = "select BL_ID,BL_NO,BL_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO from TF_BLN_B"
                    + " where BL_ID='" + _drHead["BL_ID"].ToString() + "' and BL_NO='" + _drHead["BL_NO"].ToString() + "'";
                Query _query = new Query();
                SunlikeDataSet _dsQuery = _query.DoSQLString(_sql);
                _dsQuery.Merge(ChangedDS.Tables["TF_BLN_B"], false, MissingSchemaAction.AddWithKey);
                _dtBarCode = _dsQuery.Tables["TF_BLN_B"];
            }
            else
            {
                _dtBarCode = ChangedDS.Tables["TF_BLN_B"];
            }
            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            for (int i = 0; i < _dtBarCode.Rows.Count; i++)
            {
                if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["BL_ITM"].ToString()] != null)
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
            //有新增/修改过BAR_CODE，则修改BAR_REC表
            if (_sb.Length > 0)
            {
                _sb.Insert(0, "BAR_NO in (");
                _sb.Append(")");
                //把条件语句分段
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
                //从资料库读取序列号资料
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
                //取表头资料
                string _psID, _cusNo, _bilNo, _usr;
                DateTime _bilDd = DateTime.Today;
                if (_drHead.RowState == DataRowState.Deleted)
                {
                    _psID = _drHead["BL_ID", DataRowVersion.Original].ToString();
                    _cusNo = _drHead["CUS_NO", DataRowVersion.Original].ToString();
                    _bilNo = _drHead["BL_NO", DataRowVersion.Original].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["BL_DD", DataRowVersion.Original]);
                    _usr = _drHead["USR", DataRowVersion.Original].ToString();
                }
                else
                {
                    _psID = _drHead["BL_ID"].ToString();
                    _cusNo = _drHead["CUS_NO"].ToString();
                    _bilNo = _drHead["BL_NO"].ToString();
                    _bilDd = Convert.ToDateTime(_drHead["BL_DD"]);
                    _usr = _drHead["USR"].ToString();
                }
                //取得该单据做过的change
                DataTable _dtBarChange = null;
                if (_drHead.RowState != DataRowState.Added)
                {
                    _dtBarChange = _bar.GetBarChange(_psID, _bilNo);
                }
                //更新BAR_REC表
                System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
                System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
                System.Collections.ArrayList _alBoxNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alWhNo = new System.Collections.ArrayList();
                System.Collections.ArrayList _alStop = new System.Collections.ArrayList();
                System.Text.StringBuilder _sbChange = new System.Text.StringBuilder();
                for (int i = 0; i < _dtBarCode.Rows.Count; i++)
                {
                    if (_dtBarCode.Rows[i].RowState != DataRowState.Unchanged || _htKeyItm[_dtBarCode.Rows[i]["BL_ITM"].ToString()] != null)
                    {
                        string _barCode, _boxNo, _preItm, _whNo, _batNo;
                        string _oldWhNo = "";
                        string _oldBatNo = "";
                        string _barWh = "";
                        DataRow[] _dra;
                        if (_dtBarCode.Rows[i].RowState == DataRowState.Deleted)
                        {
                            _barCode = _dtBarCode.Rows[i]["BAR_CODE", DataRowVersion.Original].ToString();
                            _boxNo = _dtBarCode.Rows[i]["BOX_NO", DataRowVersion.Original].ToString();
                            _preItm = _dtBarCode.Rows[i]["BL_ITM", DataRowVersion.Original].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _preItm, "", DataViewRowState.CurrentRows | DataViewRowState.OriginalRows);
                            if (_dra[0].RowState == DataRowState.Deleted)
                            {
                                _whNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _batNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                            }
                            else
                            {
                                _whNo = _dra[0]["WH"].ToString();
                                _batNo = _dra[0]["BAT_NO"].ToString();
                            }
                        }
                        else
                        {
                            _barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
                            _boxNo = _dtBarCode.Rows[i]["BOX_NO"].ToString();
                            _preItm = _dtBarCode.Rows[i]["BL_ITM"].ToString();
                            _dra = _dtBody.Select("PRE_ITM=" + _preItm);
                            _whNo = _dra[0]["WH"].ToString();
                            _batNo = _dra[0]["BAT_NO"].ToString();
                            if (_dra[0].RowState == DataRowState.Modified)
                            {
                                _oldWhNo = _dra[0]["WH", DataRowVersion.Original].ToString();
                                _oldBatNo = _dra[0]["BAT_NO", DataRowVersion.Original].ToString();
                            }
                        }
                        _barWh = _whNo;
                        DataRow _drBarRec = _dtBarRec.Rows.Find(new string[1] { _barCode });
                        string _stopID = "T";
                        if (_drBarRec != null)
                        {
                            bool _canUpdate = true;
                            Sunlike.Business.UserProperty _userProp = new UserProperty();
                            string _pgm = "DRP" + _psID;
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
                                //如果出货库位与序列号所在的库位不同，则要写入BAR_CHANGE
                                if (_dtBarCode.Rows[i].RowState == DataRowState.Added)
                                {
                                    if (_drBarRec["WH"].ToString() != _whNo || _drBarRec["BAT_NO"].ToString() != _batNo || _drBarRec["PH_FLAG"].ToString() == "T")
                                    {
                                        _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo, _psID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
                                        _drBarRec["WH"] = _whNo;
                                        _drBarRec["BAT_NO"] = _batNo;
                                        _drBarRec["PH_FLAG"] = "F";
                                    }
                                }
                                else if (_dtBarCode.Rows[i].RowState == DataRowState.Unchanged || _dtBarCode.Rows[i].RowState == DataRowState.Modified)
                                {
                                    if (_oldWhNo != _drBarRec["WH"].ToString() || _oldBatNo != _drBarRec["BAT_NO"].ToString() || (_psID == "LN" && _drBarRec["STOP_ID"].ToString() != "T") || (_psID == "LB" && _drBarRec["STOP_ID"].ToString() == "T"))
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
                                    }
                                    else
                                    {
                                        DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
                                        if (_draBarChange.Length > 0)
                                        {
                                            string _changeWh1 = _draBarChange[0]["WH1"].ToString();
                                            string _changeBatNo1 = _draBarChange[0]["BAT_NO1"].ToString();
                                            string _changePhFlag1 = _draBarChange[0]["PH_FLAG1"].ToString();
                                            _sbChange.Append(_bar.DeleteBarChange(_barCode, _psID, _bilNo, true));
                                            if (_changeWh1 != _whNo || _changeBatNo1 != _batNo)
                                            {
                                                _sbChange.Append(_bar.InsertBarChange(_barCode, _changeWh1, _whNo, _psID, _bilNo, _usr, _changeBatNo1, _batNo, _changePhFlag1, "F", true));
                                            }
                                        }
                                        else if (_drBarRec["WH"].ToString() != _whNo || _drBarRec["BAT_NO"].ToString() != _batNo || _drBarRec["PH_FLAG"].ToString() == "T")
                                        {
                                            _sbChange.Append(_bar.InsertBarChange(_barCode, _drBarRec["WH"].ToString(), _whNo, _psID, _bilNo, _usr, _drBarRec["BAT_NO"].ToString(), _batNo, _drBarRec["PH_FLAG"].ToString(), "F", true));
                                        }
                                        if (_drBarRec["WH"].ToString() != _whNo)
                                        {
                                            _drBarRec["WH"] = _whNo;
                                        }
                                        if (_drBarRec["BAT_NO"].ToString() != _batNo)
                                        {
                                            _drBarRec["BAT_NO"] = _batNo;
                                        }
                                        if (_drBarRec["PH_FLAG"].ToString() != "F")
                                        {
                                            _drBarRec["PH_FLAG"] = "F";
                                        }
                                    }
                                }
                                else
                                {
                                    //BAR_REC.WH必须和单据库位一致，且BAR_REC.STOP_ID='F'，否则不允许删除
                                    if (_whNo == _drBarRec["WH"].ToString() && _batNo == _drBarRec["BAT_NO"].ToString() && (_psID == "LN" && _drBarRec["STOP_ID"].ToString() == "T") || (_psID == "LB" && _drBarRec["STOP_ID"].ToString() != "T"))
                                    {
                                        //如果有做过bar_change，则库位回到做bar_change之前的地方，且把bar_change的记录删掉
                                        DataRow[] _draBarChange = _dtBarChange.Select("BAR_NO='" + _barCode + "'");
                                        if (_draBarChange.Length > 0)
                                        {
                                            _drBarRec["WH"] = _draBarChange[0]["WH1"].ToString();
                                            _drBarRec["BAT_NO"] = _draBarChange[0]["BAT_NO1"].ToString();
                                            //到货确认标记
                                            _drBarRec["PH_FLAG"] = _draBarChange[0]["PH_FLAG1"];
                                            _barWh = _draBarChange[0]["WH1"].ToString();
                                            _sbChange.Append(_bar.DeleteBarChange(_barCode, _psID, _bilNo, true));
                                        }
                                    }
                                    else
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.WH_CHANGED,PARAM=" + _barCode);//序列号_barCode已不在当前库位，不允许删除！
                                    }
                                }
                                _drBarRec["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                                if ((_psID == "LB" && _dtBarCode.Rows[i].RowState != DataRowState.Deleted)
                                    || (_psID == "LN" && _dtBarCode.Rows[i].RowState == DataRowState.Deleted))
                                {
                                    _stopID = "F";
                                }
                                _drBarRec["STOP_ID"] = _stopID;
                                //如果B0X_NO不一样，则要做自动拆箱
                                if (_drBarRec["BOX_NO"].ToString() != _boxNo)
                                {
                                    if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
                                    {
                                        _aryBoxNo[i] = _drBarRec["BOX_NO"].ToString().Trim();
                                    }
                                    else
                                    {
                                        _sbError.Append("RCID=COMMON.HINT.DOUBLEBOX,PARAM=" + _barCode);//序列号" + _barCode + "已经装箱，你不能拆箱录入！
                                    }
                                }
                                else if (!String.IsNullOrEmpty(_boxNo))
                                {
                                    if (_htBoxNo[_boxNo] == null)
                                    {
                                        _htBoxNo[_boxNo] = "";
                                        _alBoxNo.Add(_boxNo);
                                        _alWhNo.Add(_barWh);
                                        _alStop.Add(_stopID);
                                    }
                                }
                            }
                            else
                            {
                                _dtBarRec.Rows.Remove(_drBarRec);
                            }
                        }
                        else if (_dtBarCode.Rows[i].RowState != DataRowState.Deleted)
                        {
                            DataRow _dr = _dtBarRec.NewRow();
                            _dr["BAR_NO"] = _barCode;
                            _dr["WH"] = _whNo;
                            _dr["UPDDATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                            if (_psID == "LB")
                            {
                                _stopID = "F";
                            }
                            _dr["STOP_ID"] = _stopID;
                            _dr["PRD_NO"] = _barCode.Substring(BarCode.BarRole.SPrdt, BarCode.BarRole.EPrdt - BarCode.BarRole.SPrdt + 1).Replace(BarCode.BarRole.TrimChar, "");
                            if (!(BarCode.BarRole.BPMark == BarCode.BarRole.EPMark && BarCode.BarRole.EPMark == 0))
                            {
                                _dr["PRD_MARK"] = _barCode.Substring(BarCode.BarRole.BPMark, BarCode.BarRole.EPMark - BarCode.BarRole.BPMark + 1);
                            }
                            _dr["BAT_NO"] = _batNo;
                            _dtBarRec.Rows.Add(_dr);

                            //序列号不存在，写BAR_CHANGE
                            _sbChange.Append(_bar.InsertBarChange(_barCode, "", _whNo, _psID, _bilNo, _usr, "", _batNo, "", "", true));
                        }
                    }
                }
                if (String.IsNullOrEmpty(_sbError.ToString()))
                {
                    //写入序列号变更记录
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
                    DataTable _dtError = _bar.UpdateRec(SunlikeDataSet.ConvertTo(_dtBarRec.DataSet));
                    if (_dtError.Rows.Count > 0)
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.BOXERROR");//更新序列号记录失败！
                    }
                    //修改BAR_BOX
                    _bar.UpdateBarBox(_alBoxNo, _alWhNo, _alStop);
                    //拆箱
                    _bar.DeleteBox(_aryBoxNo, _usr, _psID, _bilNo);
                }
                else
                {
                    throw new SunlikeException(_sbError.ToString());
                }
            }
        }
        #endregion

        #region IAuditing 成员

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="bil_id"></param>
        /// <param name="bil_no"></param>
        /// <param name="chk_man"></param>
        /// <param name="cls_dd"></param>
        /// <returns></returns>
        public string Approve(string bil_id, string bil_no, string chk_man, DateTime cls_dd)
        {
            SunlikeDataSet _ds = this.GetData("", "", bil_id, bil_no, false);
            DataTable _dtHead = _ds.Tables["MF_BLN"];
            DataTable _dtBody = _ds.Tables["TF_BLN"];
            DataTable _dtBar = _ds.Tables["TF_BLN_B"];
            if (_dtHead.Rows.Count > 0)
            {
                //立账
                this.UpdateMfArp(_dtHead.Rows[0]);
                DbDRPBN _bn = new DbDRPBN(Comp.Conn_DB);
                _bn.UpdateArpNo(_dtHead.Rows[0]["BL_ID"].ToString(), _dtHead.Rows[0]["BL_NO"].ToString(), _dtHead.Rows[0]["ARP_NO"].ToString());

                //更新SARP
                this.UpdateSarp(_dtHead.Rows[0]);
                //更新凭证
                string _vohNo = this.UpdateVohNo(_dtHead.Rows[0], StatementType.Insert);
                this.UpdateVohNo(bil_id, bil_no, _vohNo);
                //修改库存
                foreach (DataRow dr in _dtBody.Rows)
                {
                    this.UpdateWh(dr, true);

                    this.UpdateQtyRtn(dr, true);
                }

                if (this._alOsNo.Count > 0)
                {
                    DrpSO _so = new DrpSO();
                    _so.UpdateQtyPs(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                }


                DbDRPBN _dbBn = new DbDRPBN(Comp.Conn_DB);
                _dbBn.UpdateChkMan(bil_id, bil_no, chk_man, cls_dd, _vohNo);

                //更新序列号记录
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
                _auditBarCode = true;
                if (bil_id == "BN" || bil_id == "BB")
                {
                    this.UpdateBarCodeB(_ds);
                }
                else
                {
                    this.UpdateBarCodeL(_ds);
                }
            }

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
            return this.RollBack(bil_id, bil_no, true);
        }

        /// <summary>
        /// 审核错误回退
        /// </summary>
        /// <param name="bil_id">单据识别码</param>
        /// <param name="bil_no">单据号码</param>
        /// <param name="isUpdateHead">更新表头审核信息</param>
        /// <returns>错误信息</returns>
        public string RollBack(string bil_id, string bil_no, bool isUpdateHead)
        {
            string _error = "";
            try
            {
                SunlikeDataSet _ds = this.GetData("", "", bil_id, bil_no, false);
                this.SetCanModify("", _ds, false, true);
                if (_ds.ExtendedProperties.Contains("CAN_MODIFY"))
                {
                    if (_ds.ExtendedProperties["CAN_MODIFY"].ToString() == "F")
                    {
                        return "RCID=INV.HINT.NOTALLOW";
                    }
                }
                DataRow _drHead = _ds.Tables["MF_BLN"].Rows[0];
                DataTable _dtBody = _ds.Tables["TF_BLN"];
                DataTable _dtBar = _ds.Tables["TF_BLN_B"];
                for (int i = 0; i < _dtBody.Rows.Count; i++)
                {
                    //修改分仓存量
                    this.UpdateWh(_dtBody.Rows[i], false);

                    //修改已还数量
                    this.UpdateQtyRtn(_dtBody.Rows[i], false);
                }

                if (this._alOsNo.Count > 0)
                {
                    DrpSO _so = new DrpSO();
                    _so.UpdateQtyPs(this._alOsNo, this._alOsItm, this._alPrdNo, this._alUnit, this._alQty, this._alQty1);
                }

                //更新序列号记录
                for (int i = 0; i < _dtBar.Rows.Count; i++)
                {
                    _dtBar.Rows[i].Delete();
                }
                _auditBarCode = true;
                if (bil_id == "BN" || bil_id == "BB")
                {
                    this.UpdateBarCodeB(_ds);
                }
                else
                {
                    this.UpdateBarCodeL(_ds);
                }



                DbDRPBN _bn = new DbDRPBN(Comp.Conn_DB);
                //设定审核人

                if (isUpdateHead)
                {
                    _bn.UpdateChkMan(bil_id, bil_no, "", DateTime.Now, "");
                }

                //立账
                _drHead.Delete();
                this.UpdateMfArp(_drHead);
                _bn.UpdateArpNo(_drHead["BL_ID", DataRowVersion.Original].ToString(),
                    _drHead["BL_NO", DataRowVersion.Original].ToString(), "");
                //更新SARP
                this.UpdateSarp(_drHead);
                //更新凭证
                this.UpdateVohNo(_drHead, StatementType.Delete);
                this.UpdateVohNo(bil_id, bil_no, "");
            }
            catch (Exception _ex)
            {
                _error = _ex.Message.ToString();
            }
            return _error;
        }

        #endregion

        /// <summary>
        /// 取表身数据
        /// </summary>
        /// <param name="blId"></param>
        /// <param name="blNo"></param>
        /// <param name="preItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBodyData(string blId, string blNo, int preItm)
        {
            DbDRPBN _dbDrpBn = new DbDRPBN(Comp.Conn_DB);
            return _dbDrpBn.GetBodyData(blId, blNo, preItm);
        }



        /// <summary>
        /// 判断单据表身是否补开发票则不允许删除
        /// </summary>
        /// <param name="dr">tf_ck.row</param>
        private void checktflz(DataRow dr)
        {
            string _blId = dr["BL_ID", DataRowVersion.Original].ToString();
            if (_blId.Equals("LN") || _blId.Equals("LB"))
            {
                InvIK _invik = new InvIK();

                string bilId = dr["BL_ID", DataRowVersion.Original].ToString();
                string ckNo = dr["BL_NO", DataRowVersion.Original].ToString();

                SunlikeDataSet _ds = _invik.GetInTfLz(bilId, ckNo);
                if (_ds.Tables["TF_LZ"].Rows.Count > 0)
                {
                    throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + ",PARAM=" + _ds.Tables["TF_LZ"].Rows[0]["LZ_NO"].ToString());//无法删除单号，原因：{0}
                }
            }
            else
            {
                InvLI _invli = new InvLI();
                string bilId = dr["BL_ID", DataRowVersion.Original].ToString();
                string ckNo = dr["BL_NO", DataRowVersion.Original].ToString();

                SunlikeDataSet _ds = _invli.GetInTfLz(bilId, ckNo);
                if (_ds.Tables["TF_LZ1"].Rows.Count > 0)
                {
                    throw new Exception("RCID=COMMON.HINT.DELTF_LZError,PARAM=" + ckNo + ",PARAM=" + _ds.Tables["TF_LZ1"].Rows[0]["LZ_NO"].ToString());//无法删除单号，原因：{0}
                }
            }

        }

    }
}