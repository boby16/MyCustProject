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
    public class POSCTypeDef : BizObject
    {
        public POSCTypeDef()
        { }

        public SunlikeDataSet GetData(string sqlWhere)
        {
            DbPOSCTypeDef _cTypeUp = new DbPOSCTypeDef(Comp.Conn_DB);
            return _cTypeUp.GetData(sqlWhere);
        }

        public DataTable UpdateData(SunlikeDataSet changeDs, bool bubbleException)
        {
            Hashtable _ht = new System.Collections.Hashtable();
            _ht["CTYPE_DEF"] = "CTYPE_NO,DEP,START_DD,END_DD,DIS_CNT,USR,CHK_MAN,SYS_DATE,CLS_DATE,UP,CTYPE_NO_USE";
            this.UpdateDataSet(changeDs, _ht);
            DataTable _dtErr = new DataTable();
            if ((changeDs.HasErrors) && (bubbleException))
            {
                throw new System.Exception(changeDs.Tables[0].Rows[0].RowError);
            }
            else
            {
                _dtErr = GetAllErrors(changeDs);
            }
            return _dtErr;
        }

        //protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        //{
        //    if (tableName == "CTYPE_DEF")
        //    {
        //        if (statementType == StatementType.Insert || statementType == StatementType.Update)
        //        {
        //            if (!string.IsNullOrEmpty(dr["START_DD"].ToString()))
        //            {
        //                dr["START_DD"] = Convert.ToDateTime(dr["START_DD"]).Date;
        //            }

        //            if (!string.IsNullOrEmpty(dr["END_DD"].ToString()))
        //            {
        //                dr["END_DD"] = Convert.ToDateTime(dr["END_DD"]).Date;
        //            }
        //        }
        //    }
        //}
        public decimal GetUp(string dep, string cTypeNo, string cusNo, out bool isUp)
        {
            decimal _result = 0;
            isUp = true;

            //会员当前使用的所有卡类
            string _cTypeNoExist = "";
            POSCustCR _custCR = new POSCustCR();
            SunlikeDataSet _dsCR = _custCR.GetDataByCus(cusNo, true);
            foreach (DataRow _dr in _dsCR.Tables["CUST_CARD1"].Rows)
            {
                //if (_dr["SAVING_CHK"].ToString() == "T")
                {
                    if (_dsCR.Tables["CUST_CARD"].Rows.Find(_dr["CARD_NO"])["STATUS_ID"].ToString() == "1")
                    {
                        if (string.IsNullOrEmpty(_dr["END_DD"].ToString()) || Convert.ToDateTime(_dr["END_DD"]) > DateTime.Today)
                        {
                            if (_cTypeNoExist.Length > 0)
                            {
                                _cTypeNoExist += "','";
                            }
                            _cTypeNoExist += _dr["CTYPE_NO"].ToString();
                        }
                    }
                }
            }

            DbPOSCTypeDef _cTypeUp = new DbPOSCTypeDef(Comp.Conn_DB);
            DataTable _dtDef = _cTypeUp.GetData("CTYPE_NO='" + cTypeNo + "'").Tables["CTYPE_DEF"];

            DataRow[] _drs = _dtDef.Select("ISNULL(DEP,'')='" + dep + "' AND CTYPE_NO_USE IN ('" + _cTypeNoExist + "') AND (START_DD IS NULL OR START_DD<='" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "') AND (END_DD IS NULL OR END_DD>='" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            if (_drs.Length == 0)
            {
                _drs = _dtDef.Select("ISNULL(DEP,'')='' AND CTYPE_NO_USE IN('" + _cTypeNoExist + "') AND (START_DD IS NULL OR START_DD<='" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "') AND (END_DD IS NULL OR END_DD>='" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            if (_drs.Length == 0)
            {
                _drs = _dtDef.Select("ISNULL(DEP,'')='" + dep + "' AND ISNULL(CTYPE_NO_USE,'')='' AND (START_DD IS NULL OR START_DD<='" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "') AND (END_DD IS NULL OR END_DD>='" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            if (_drs.Length == 0)
            {
                _drs = _dtDef.Select("ISNULL(DEP,'')='' AND ISNULL(CTYPE_NO_USE,'')='' AND (START_DD IS NULL OR START_DD<='" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "') AND (END_DD IS NULL OR END_DD>='" + DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            if (_drs.Length > 0)
            {
                decimal _up = 0;
                decimal _disCnt = 0;

                foreach (DataRow _dr in _drs)
                {
                    if (!string.IsNullOrEmpty(_dr["UP"].ToString()) && Convert.ToDecimal(_dr["UP"]) != 0)
                    {
                        if (_up == 0 || _up > Convert.ToDecimal(_dr["UP"]))
                        {
                            _up = Convert.ToDecimal(_dr["UP"]);
                        }
                    }
                    else if (!string.IsNullOrEmpty(_dr["DIS_CNT"].ToString()) && Convert.ToDecimal(_dr["DIS_CNT"]) != 0)
                    {
                        if (_disCnt == 0 || _disCnt > Convert.ToDecimal(_dr["DIS_CNT"]))
                        {
                            _disCnt = Convert.ToDecimal(_dr["DIS_CNT"]);
                        } 
                    }
                }

                if (_up > 0)
                {
                    _result = _up;
                }
                else if (_disCnt > 0)
                {
                    _result = _disCnt;
                    isUp = false;
                }
            }

            return _result;
        }

    }
}
