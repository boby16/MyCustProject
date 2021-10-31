using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sunlike.Common.CommonVar;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Collections;

namespace Sunlike.Business
{
    public class POSCustCR : BizObject
    {
        /// <summary>
        /// 客户卡
        /// </summary>
        public POSCustCR()
        { }

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="onlyFillSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string cardNo, bool onlyFillSchema)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbCustCR.GetData(cardNo, onlyFillSchema);
            _ds.DecimalDigits = Comp.GetCompInfo("").DecimalDigitsInfo.System;
            return _ds;
        }

        /// <summary>
        /// 去客户卡资料
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="getBody"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataByCus(string cusNo, bool getBody)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbCustCR.GetData(cusNo);

            if (getBody)
            {
                if (_ds.Tables["CUST_CARD"].Rows.Count <= 0)
                {
                    _ds.Merge(_dbCustCR.GetDataBody("", ""));
                }
                foreach (DataRow dr in _ds.Tables["CUST_CARD"].Rows)
                {
                    _ds.Merge(_dbCustCR.GetDataBody(dr["CARD_NO"].ToString(), ""));
                }
            }
            if (_ds.Tables.Contains("CUST_CARD1"))
            {
                _ds.Relations.Add(new DataRelation("CUST_CARDCUST_CARD1", new DataColumn[] { _ds.Tables["CUST_CARD"].Columns["CARD_NO"] },
                                new DataColumn[] { _ds.Tables["CUST_CARD1"].Columns["CARD_NO"] }));
            }
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="snNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBySnNo(string snNo)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            return _dbCustCR.GetDataBySnNo(snNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataBody(string cardNo, string subNo)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            return _dbCustCR.GetDataBody(cardNo, subNo);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="changeDs"></param>
        /// <param name="throwException"></param>
        /// <returns></returns>
        public DataTable UpdateData(SunlikeDataSet changeDs, bool throwException)
        {
            Hashtable _ht = new Hashtable();
            _ht["CUST_CARD"] = "CARD_NO,CUS_NO,CARD_DD,USR,SYS_DATE,STATUS_ID,NEW_NO,DEP,SN_NO";
            if (changeDs.Tables.Contains("CUST_CARD1"))
            {
                _ht["CUST_CARD1"] = " CARD_NO,SUB_NO,START_DD,END_DD,CTYPE_NO,AMTN_PAY,CHG_USR_NO,CHG_DD,OLDSUB_NO,SAVING_CHK,SYS_CHK,UP,USR,SYS_DATE,FIN_DD,SAL_NO,AMTN";
            }
            this.UpdateDataSet(changeDs, _ht);
            DataTable _dtErr = new DataTable();
            if ((changeDs.HasErrors) && (throwException))
            {
                throw new System.Exception(changeDs.Tables[0].Rows[0].RowError);
            }
            else
            {
                _dtErr = GetAllErrors(changeDs);
            }
            return _dtErr;
        }

        protected override void BeforeDsSave(DataSet ds)
        {
            if (ds.Tables.Contains("CUST_CARD"))
            {
                //有购卡记录不能删除
                POSCustCR _posCustCR = new POSCustCR();
                foreach(DataRow dr in  ds.Tables["CUST_CARD"].Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        if (dr.RowState == DataRowState.Deleted)
                        {

                            if (_posCustCR.CheckCardUse(dr["CARD_NO", DataRowVersion.Original].ToString()))
                            {
                                throw new Exception("RCID=INV.CUST_CARD.HAS_CUST_CARD1");//本卡已经有购卡记录，无法删除，只能进行停用
                            }
                        }
                    }
                }
            }
            
            base.BeforeDsSave(ds);
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
            if (tableName == "CUST_CARD")
            {
                string _usr = "";
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
                    //检查销货客户
                    Cust _cust = new Cust();
                    if (!_cust.IsExist(_usr, dr["CUS_NO"].ToString()))
                    {
                        dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUS_NO_NOTEXIST,PARAM=" + dr["CUS_NO"].ToString());//客户代号[{0}]不存在或没有对其操作的权限，请检查
                        status = UpdateStatus.SkipAllRemainingRows;
                    }
                    else
                    {
                        SunlikeDataSet _dsCust = new SunlikeDataSet();
                        _dsCust.Merge(_cust.GetData(dr["CUS_NO"].ToString()));
                        DataTable _dtCust = _dsCust.Tables["CUST"];
                        if (_dtCust.Rows[0]["CUS_NO"].ToString() != _dtCust.Rows[0]["CARD_NO"].ToString())
                        {
                            dr.SetColumnError("CUS_NO", "RCID=COMMON.HINT.CUST_TYPE_ERROR");//客户类型不正确
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }

                    //检查部门
                    if (!string.IsNullOrEmpty(dr["DEP"].ToString()))
                    {
                        Dept _dept = new Dept();
                        if (!_dept.IsExist(_usr, dr["DEP"].ToString()))
                        {
                            dr.SetColumnError("DEP", "RCID=COMMON.HINT.DEP_NOTEXIST,PARAM=" + dr["DEP"].ToString());//部门代号[{0}]不存在或没有对其操作的权限，请检查
                            status = UpdateStatus.SkipAllRemainingRows;
                        }
                    }
                }
            }
            else if (tableName == "CUST_CARD1")
            {
                if (dr.RowState == DataRowState.Added)
                {
                    if (string.IsNullOrEmpty(dr["SUB_NO"].ToString()))
                    {
                        dr["SUB_NO"] = CreateSubNo(dr["CARD_NO"].ToString());
                    }
                }
            }

            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SunlikeDataSet UpdatePosCust(SunlikeDataSet changeData, string userId)
        {
            CardMember _cardMember = new CardMember();
            Cust _cust = new Cust();

            this.EnterTransaction();
            try
            {
                foreach (DataRow _dr in changeData.Tables[0].Rows)
                {
                    if (_dr.RowState == DataRowState.Added)
                    {
                        #region Poscard
                        SunlikeDataSet _ds = _cardMember.GetData(userId, "", true);
                        DataRow _drNew = _ds.Tables["POSCARD"].NewRow();
                        _drNew["CARD_NO"] = _dr["CUS_NO"];
                        _drNew["NAME"] = _dr["NAME"];
                        if (!string.IsNullOrEmpty(_dr["BTH_DAY"].ToString()))
                        {
                            _drNew["BTH_DAY"] = _dr["BTH_DAY"];
                        }
                        _drNew["SEX_ID"] = _dr["SEX_ID"];
                        _drNew["USER_ID"] = _dr["USER_ID"];
                        _drNew["JOB_REM"] = _dr["JOB_REM"];
                        _drNew["EN_DD"] = _dr["END_DD"];
                        _ds.Tables["POSCARD"].Rows.Add(_drNew);
                        _cardMember.UpdateData(_ds, true);
                        #endregion

                        #region Cust
                        SunlikeDataSet _dsCust = new SunlikeDataSet();
                        _dsCust.Merge(_cust.GetData(""));
                        DataTable _dt = _dsCust.Tables["CUST"];
                        _drNew = _dt.NewRow();
                        _drNew["CUS_NO"] = _dr["CUS_NO"];
                        _drNew["CARD_NO"] = _dr["CUS_NO"];
                        _drNew["NAME"] = _dr["NAME"];
                        _drNew["SNM"] = _dr["NAME"];
                        _drNew["CUS_ARE"] = _dr["CUS_ARE"];
                        _drNew["ZIP"] = _dr["ZIP"];
                        _drNew["TEL1"] = _dr["TEL1"];
                        _drNew["TEL2"] = _dr["TEL2"];
                        _drNew["SAL_NO"] = _dr["SAL_NO"];
                        _drNew["E_MAIL"] = _dr["E_MAIL"];
                        _drNew["ADR1"] = _dr["ADR1"];
                        _drNew["REM"] = _dr["REM"];
                        _drNew["PSWD_PAY"] = _dr["PSWD_PAY"];
                        _drNew["END_DD"] = _dr["END_DD"];

                        _drNew["DEP"] = _dr["DEP"];
                        _drNew["OBJ_ID"] = "1";
                        _drNew["CUS_LEVEL"] = "5";
                        _drNew["CLS_MTH"] = "3";
                        _drNew["CLS_DD"] = "1";
                        _drNew["MM_END"] = "30";
                        _drNew["CHK_DD"] = "30";
                        _drNew["CRD_ID"] = "1";
                        _drNew["ID1_TAX"] = "1";
                        _drNew["ID2_TAX"] = "F";
                        _drNew["CLS2"] = "1";
                        _drNew["CHK_CRD"] = "F";
                        _drNew["SO_CRD"] = "F";
                        _drNew["CHK_FAX"] = "F";
                        _drNew["CHK_CUS_IDX"] = "1";
                        _drNew["CY_ID"] = "F";
                        _drNew["CHK_INCLUDE"] = "F";
                        _drNew["CHK_IRP"] = "F";
                        _drNew["CHK_TRP"] = "F";
                        _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                        _drNew["CLS_DATE"] = DateTime.Now.ToString(Comp.SQLDateFormat);
                        _drNew["CHK_MAN"] = userId;
                        _drNew["USR"] = userId;
                        _dt.Rows.Add(_drNew);
                        _cust.UpdateDate(SunlikeDataSet.ConvertTo(_dt.DataSet), true);

                        #endregion

                    }
                    else if (_dr.RowState == DataRowState.Modified)
                    {
                        #region Poscard
                        SunlikeDataSet _ds = _cardMember.GetData(userId, _dr["CUS_NO"].ToString(), true);
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            _ds.Tables[0].Rows[0]["NAME"] = _dr["NAME"];
                            _ds.Tables[0].Rows[0]["BTH_DAY"] = _dr["BTH_DAY"];
                            _ds.Tables[0].Rows[0]["SEX_ID"] = _dr["SEX_ID"];
                            _ds.Tables[0].Rows[0]["USER_ID"] = _dr["USER_ID"];
                            _ds.Tables[0].Rows[0]["JOB_REM"] = _dr["JOB_REM"];
                            _ds.Tables[0].Rows[0]["EN_DD"] = _dr["END_DD"];
                            _cardMember.UpdateData(_ds, true);
                        }
                        #endregion

                        #region Cust
                        SunlikeDataSet _dsCust = new SunlikeDataSet();
                        _dsCust.Merge(_cust.GetData(_dr["CUS_NO"].ToString()));
                        DataTable _dt = _dsCust.Tables["CUST"];
                        if (_dt.Rows.Count > 0)
                        {
                            _dt.Rows[0]["NAME"] = _dr["NAME"];
                            _dt.Rows[0]["SNM"] = _dr["NAME"];
                            _dt.Rows[0]["CUS_ARE"] = _dr["CUS_ARE"];
                            _dt.Rows[0]["ZIP"] = _dr["ZIP"];
                            _dt.Rows[0]["TEL1"] = _dr["TEL1"];
                            _dt.Rows[0]["TEL2"] = _dr["TEL2"];
                            _dt.Rows[0]["SAL_NO"] = _dr["SAL_NO"];
                            _dt.Rows[0]["E_MAIL"] = _dr["E_MAIL"];
                            _dt.Rows[0]["ADR1"] = _dr["ADR1"];
                            _dt.Rows[0]["REM"] = _dr["REM"];
                            _dt.Rows[0]["PSWD_PAY"] = _dr["PSWD_PAY"];
                            _dt.Rows[0]["END_DD"] = _dr["END_DD"];
                            _dt.Rows[0]["DEP"] = _dr["DEP"];
                            _cust.UpdateDate(SunlikeDataSet.ConvertTo(_dt.DataSet), true);
                        }
                        #endregion
                    }
                    else if (_dr.RowState == DataRowState.Deleted)
                    {
                        #region Cust
                        SunlikeDataSet _dsCust =new SunlikeDataSet();
                        _dsCust.Merge( _cust.GetData(_dr["CUS_NO", DataRowVersion.Original].ToString()));
                        DataTable _dt = _dsCust.Tables["CUST"];
                        if (_dt.Rows.Count > 0)
                        {
                            _dt.Rows[0].Delete();
                            _cust.UpdateDate(SunlikeDataSet.ConvertTo(_dt.DataSet), true);
                        }
                        #endregion

                        #region Poscard
                        SunlikeDataSet _ds = _cardMember.GetData(userId, _dr["CUS_NO", DataRowVersion.Original].ToString(), true);
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            _ds.Tables[0].Rows[0].Delete();
                            _cardMember.UpdateData(_ds, true);
                        }
                        #endregion
                    }
                }
                changeData.AcceptChanges();
            }
            catch (Exception _ex)
            {
                this.SetAbort();
                throw _ex;
            }
            finally
            {
                this.LeaveTransaction();
            }
            return changeData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cardNo"></param>
        /// <param name="_subNo"></param>
        /// <param name="amtn"></param>
        public void UpdatePayAmtn(string _cardNo, string _subNo, decimal amtn)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            _dbCustCR.UpdatePayAmtn(_cardNo, _subNo, amtn);

            //更新完成日期
            SunlikeDataSet _ds = GetDataBody(_cardNo, _subNo);
            if (_ds.Tables["CUST_CARD1"].Rows.Count > 0)
            {
                DataRow _dr = _ds.Tables["CUST_CARD1"].Rows[0];
                decimal _amtn = 0;
                decimal _amtnpay = 0;
                if (!string.IsNullOrEmpty(_dr["AMTN"].ToString()))
                {
                    _amtn = Convert.ToDecimal(_dr["AMTN"]);
                }
                if (!string.IsNullOrEmpty(_dr["AMTN_PAY"].ToString()))
                {
                    _amtnpay = Convert.ToDecimal(_dr["AMTN_PAY"]);
                }

                if (_amtnpay >= _amtn)
                {
                    if (string.IsNullOrEmpty(_dr["FIN_DD"].ToString()))
                    {
                        _dr["FIN_DD"] = DateTime.Now;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_dr["FIN_DD"].ToString()))
                    {
                        _dr["FIN_DD"] = DBNull.Value;
                    }
                }
            }

            if (_ds.GetChanges() != null)
            {
                UpdateData(_ds, true);
            }
        }

        /// <summary>
        /// 卡号是否存在
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool CheckCardExist(string cardNo)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            return _dbCustCR.CheckCardExist(cardNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <returns></returns>
        public bool CheckCardExist(string cardNo, string subNo)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            return _dbCustCR.CheckCardExist(cardNo, subNo);
        }

        /// <summary>
        /// 转卡
        /// </summary>
        /// <param name="newCardNo"></param>
        /// <param name="oldCardNo"></param>
        /// <param name="usr"></param>
        public void ChangeCard(string newCardNo, string oldCardNo, string usr)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            if (_dbCustCR.CheckCardExist(newCardNo))
            {
                throw new Exception(string.Format("卡号[{0}]已存在", newCardNo));
            }
            else
            {
                this.EnterTransaction();
                try
                {
                    SunlikeDataSet _ds = _dbCustCR.GetData(oldCardNo, false);
                    if (_ds.Tables["CUST_CARD"].Rows.Count > 0)
                    {
                        DataRow _drOld = _ds.Tables["CUST_CARD"].Rows[0];
                        _drOld["STATUS_ID"] = "4";
                        _drOld["NEW_NO"] = newCardNo;

                        DataRow _drNew = _ds.Tables["CUST_CARD"].NewRow();
                        _drNew["CARD_NO"] = newCardNo;
                        _drNew["CUS_NO"] = _drOld["CUS_NO"];
                        _drNew["CARD_DD"] = DateTime.Today.ToString(Comp.SQLDateTimeFormat);
                        _drNew["SYS_DATE"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                        _drNew["STATUS_ID"] = "1";
                        _drNew["USR"] = usr;
                        _drNew["DEP"] = _drOld["DEP"];
                        _ds.Tables["CUST_CARD"].Rows.Add(_drNew);
                        string _cusNo = _drOld["CUS_NO"].ToString();

                        foreach (DataRow _dr in _ds.Tables["CUST_CARD1"].Select("CARD_NO='" + oldCardNo + "'"))
                        {
                            _drNew = _ds.Tables["CUST_CARD1"].NewRow();
                            _drNew["CARD_NO"] = newCardNo;
                            _drNew["SUB_NO"] = _dr["SUB_NO"];
                            _drNew["START_DD"] = _dr["START_DD"];
                            _drNew["END_DD"] = _dr["END_DD"];
                            _drNew["CTYPE_NO"] = _dr["CTYPE_NO"];
                            _drNew["AMTN_PAY"] = _dr["AMTN_PAY"];
                            _drNew["CHG_USR_NO"] = _dr["CHG_USR_NO"];
                            _drNew["CHG_DD"] = _dr["CHG_DD"];
                            _drNew["SAVING_CHK"] = _dr["SAVING_CHK"];
                            _drNew["SYS_DATE"] = _dr["SYS_DATE"];
                            _drNew["USR"] = _dr["USR"];
                            _drNew["AMTN"] = _dr["AMTN"];
                            _drNew["FIN_DD"] = _dr["FIN_DD"];
                            _drNew["SAL_NO"] = _dr["SAL_NO"];
                            _ds.Tables["CUST_CARD1"].Rows.Add(_drNew);
                        }

                        UpdateData(_ds, true);

                        Query _query = new Query();
                        _query.DoSQLString("INSERT INTO CUST_SBAC (CUS_NO,CARD_NO,SUB_NO,PRD_NO,NUM_SET,NUM_USE,LAST_DD,DAY_ZQ,UP_FREEZE,COUNT_TYPE) SELECT CUS_NO,'" + newCardNo + "',SUB_NO,PRD_NO,NUM_SET,NUM_USE,LAST_DD,DAY_ZQ,UP_FREEZE,COUNT_TYPE FROM CUST_SBAC WHERE CARD_NO='" + oldCardNo + "' AND CUS_NO='" + _cusNo + "'");
                    }
                }
                catch (Exception _ex)
                {
                    this.SetAbort();
                    throw _ex;
                }
                finally
                {
                    this.LeaveTransaction();
                }
            }
        }

        /// <summary>
        /// 卡号是否存在购卡记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool CheckCardUse(string cardNo)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            return _dbCustCR.CheckCardUse(cardNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cTypeNo"></param>
        /// <returns></returns>
        public bool CheckCTypeUse(string cTypeNo)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            return _dbCustCR.CheckCTypeUse(cTypeNo);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        public void EnabledCard(string cardNo, string subNo)
        {
            SunlikeDataSet _ds = GetDataBody(cardNo, subNo);
            if (_ds.Tables["CUST_CARD1"].Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(_ds.Tables["CUST_CARD1"].Rows[0]["START_DD"].ToString()))
                {
                    _ds.Tables["CUST_CARD1"].Rows[0]["START_DD"] = DateTime.Today;
                    string _cTypeNo = _ds.Tables["CUST_CARD1"].Rows[0]["CTYPE_NO"].ToString();
                    POSCustCType _zyCtype = new POSCustCType();
                    DataTable _dtCtype = _zyCtype.GetData("CTYPE_NO='" + _cTypeNo + "'").Tables["CTYPE"];
                    if (_dtCtype.Rows.Count > 0)
                    {
                        int validDay = 0;
                        if (!string.IsNullOrEmpty(_dtCtype.Rows[0]["VALID_DAY"].ToString()))
                        {
                            validDay = Convert.ToInt32(_dtCtype.Rows[0]["VALID_DAY"]);
                        }
                        if (validDay > 0)
                        {
                            _ds.Tables["CUST_CARD1"].Rows[0]["END_DD"] = Convert.ToDateTime(DateTime.Today).AddDays(validDay);
                        }

                        UpdateData(_ds, true);
                    }
                }
            }
        }



        /// <summary>
        /// 判断客户是否有未过期的储值卡
        /// </summary>
        /// <param name="cusNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <returns></returns>
        public bool CheckSavingCardExist(string cusNo, string cardNo, string subNo)
        {
            SunlikeDataSet _ds = GetDataByCus(cusNo, true);
            foreach (DataRow _dr in _ds.Tables["CUST_CARD1"].Rows)
            {
                if (_dr["SAVING_CHK"].ToString() == "T")
                {
                    if (_ds.Tables["CUST_CARD"].Rows.Find(_dr["CARD_NO"])["STATUS_ID"].ToString() == "1")
                    {
                        if (string.IsNullOrEmpty(_dr["END_DD"].ToString()) || Convert.ToDateTime(_dr["END_DD"]) > DateTime.Today)
                        {
                            if (_dr["CARD_NO"].ToString() != cardNo || _dr["SUB_NO"].ToString() != subNo)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 换卡升级
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="oldSubNo"></param>
        /// <param name="newSubNo"></param>
        /// <param name="newCtypeNo"></param>
        /// <param name="newStartDd"></param>
        /// <param name="newEndDd"></param>
        /// <param name="usr"></param>
        public void SubChange(string cardNo, string oldSubNo, string newCtypeNo, DateTime newStartDd, DateTime newEndDd, string usr, string salNo)
        {

            SunlikeDataSet _ds = GetData(cardNo, false);
            if (_ds.Tables["CUST_CARD"].Rows.Count > 0)
            {
                if (_ds.Tables["CUST_CARD"].Rows[0]["STATUS_ID"].ToString() != "1")
                {
                    throw new Exception("RCID=INV.CUST_CARD.CARD_STATUS_ERROR");
                }
            }

            DataRow _drOld = _ds.Tables["CUST_CARD1"].Rows.Find(new object[] { cardNo, oldSubNo });
            if (_drOld == null)
            {
                throw new Exception("找不到旧卡记录,请检查!");

            }

            //回写旧卡资料
            _drOld["END_DD"] = DateTime.Today;
            _drOld["CHG_USR_NO"] = usr;
            _drOld["CHG_DD"] = DateTime.Now;
            _drOld["SYS_CHK"] = "T";

            //新增卡
            DataRow _drNew = _ds.Tables["CUST_CARD1"].NewRow();
            _drNew["CARD_NO"] = cardNo;
            _drNew["SUB_NO"] = CreateSubNo(cardNo);
            _drNew["CTYPE_NO"] = newCtypeNo;
            _drNew["OLDSUB_NO"] = oldSubNo;
            _drNew["SAVING_CHK"] = "T";
            _drNew["SYS_DATE"] = DateTime.Now;
            _drNew["USR"] = usr;
            _drNew["SAL_NO"] = salNo;

            if (newStartDd == DateTime.MinValue)
            {
                _drNew["START_DD"] = DBNull.Value;
            }
            else
            {
                _drNew["START_DD"] = newStartDd.Date;
            }
            if (newEndDd == DateTime.MinValue)
            {
                _drNew["END_DD"] = DBNull.Value;
            }
            else
            {
                _drNew["END_DD"] = newEndDd.Date;
            }

            //新卡当前单价
            decimal _upCurrent = 0;
            POSCustCType _cType = new POSCustCType();
            SunlikeDataSet _dsCtype = _cType.GetHeadData(newCtypeNo);
            if (_dsCtype.Tables[0].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(_dsCtype.Tables[0].Rows[0]["UP"].ToString()))
                {
                    _upCurrent = Convert.ToDecimal(_dsCtype.Tables[0].Rows[0]["UP"]);
                }
                
                Users _user = new Users();
                string _dep = _user.GetUserDepNo(usr);
                bool _isUp;
                POSCTypeDef _cTypeDef = new POSCTypeDef();
                decimal _result = _cTypeDef.GetUp(_dep, newCtypeNo, "", out _isUp);
                if (_result != 0)
                {
                    if (_isUp)
                    {
                        _upCurrent = _result;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(_dsCtype.Tables[0].Rows[0]["UP"].ToString()))
                        {
                            _upCurrent = Convert.ToDecimal(_dsCtype.Tables[0].Rows[0]["UP"]) * _result / 100;
                        }
                    }
                }
            }
            decimal _totalUp = 0;
            decimal _totalAmtnPay = 0;

            while (!string.IsNullOrEmpty(oldSubNo))
            {
                DataRow[] _drs = _ds.Tables["CUST_CARD1"].Select("SUB_NO='" + oldSubNo + "' AND ISNULL(SUB_NO,'')<>ISNULL(OLDSUB_NO,'')");
                if (_drs.Length > 0)
                {
                    if (!string.IsNullOrEmpty(_drs[0]["UP"].ToString()))
                    {
                        _totalUp += Convert.ToDecimal(_drs[0]["UP"]);
                    }
                    if (!string.IsNullOrEmpty(_drs[0]["AMTN_PAY"].ToString()))
                    {
                        _totalAmtnPay += Convert.ToDecimal(_drs[0]["AMTN_PAY"]);
                    }
                    oldSubNo = _drs[0]["OLDSUB_NO"].ToString();
                }
                else
                {
                    oldSubNo = "";
                }

            }
            _drNew["AMTN_PAY"] = 0;
            _drNew["UP"] = _upCurrent - _totalUp;
            _drNew["AMTN"] = _upCurrent - _totalAmtnPay;
            _ds.Tables["CUST_CARD1"].Rows.Add(_drNew);
            UpdateData(_ds, true);
        }

        /// <summary>
        /// 停用/反停用
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="subNo"></param>
        /// <param name="_stopDd"></param>
        /// <param name="usr"></param>
        public void SubStopChange(string cardNo, string subNo, DateTime _stopDd, string usr)
        {
            SunlikeDataSet _ds = GetDataBody(cardNo, subNo);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                if (_ds.Tables[0].Rows[0]["SYS_CHK"].ToString() == "T")
                {
                    throw new Exception("RCID=INV.CUST_CARD1.SYS_CHK_ALERT");
                }

                if (_stopDd == DateTime.MinValue)
                {
                    _ds.Tables[0].Rows[0]["END_DD"] = DBNull.Value;
                }
                else
                {
                    _ds.Tables[0].Rows[0]["END_DD"] = _stopDd.Date;
                }
                _ds.Tables[0].Rows[0]["CHG_USR_NO"] = usr;
                _ds.Tables[0].Rows[0]["CHG_DD"] = DateTime.Now;
                UpdateData(_ds, true);
            }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string CreateSubNo(string cardNo)
        {
            string _sql = "SELECT SUB_NO FROM CUST_CARD1 WHERE CARD_NO='" + cardNo + "'";
            Query _query = new Query();
            SunlikeDataSet _ds = _query.DoSQLString(_sql);
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            int _subCount = _ds.Tables[0].Rows.Count + 1;

            while (_ds.Tables[0].Select(string.Format("SUB_NO='{0}-{1}'", cardNo, _subCount)).Length > 0)
            {
                _subCount++;
            }

            return string.Format("{0}-{1}", cardNo, _subCount);
        }

        /// <summary>
        /// 序列号
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="snNo"></param>
        public void UpdateSnNo(string cardNo, string snNo)
        {
            DbPOSCustCR _dbCustCR = new DbPOSCustCR(Comp.Conn_DB);
            _dbCustCR.UpdateSnNo(cardNo, snNo);
        }
    }
}
