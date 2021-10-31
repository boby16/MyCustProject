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
    public class POSCustCType : BizObject
    {
        /// <summary>
        /// 客户卡类
        /// </summary>
        public POSCustCType()
        { }

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="cTypeNo"></param>
        /// <param name="isSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            DbPOSCustCType _dbCustCType = new DbPOSCustCType(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbCustCType.GetData(sqlWhere);

            ArrayList _alCTypeNo = new ArrayList();
            for (int i = 0; i < _ds.Tables["CTYPE"].Rows.Count; i++)
            {
                _alCTypeNo.Add(_ds.Tables["CTYPE"].Rows[i]["CTYPE_NO"]);
            }
            _ds.Merge(_dbCustCType.GetCType_Dis(_alCTypeNo));
            _ds.Merge(_dbCustCType.GetCType_Ly(_alCTypeNo));
            _ds.Merge(_dbCustCType.GetCType_Num(_alCTypeNo));
            //_ds.Merge(_dbCustCType.GetCType_NumGroup(_alCTypeNo));

            _ds.Relations.Add(new DataRelation("CTYPECTYPE_DIS"
                , _ds.Tables["CTYPE"].Columns["CTYPE_NO"], _ds.Tables["CTYPE_DIS"].Columns["CTYPE_NO"]));
            _ds.Relations.Add(new DataRelation("CTYPECTYPE_NUM"
                , _ds.Tables["CTYPE"].Columns["CTYPE_NO"], _ds.Tables["CTYPE_NUM"].Columns["CTYPE_NO"]));
            _ds.Relations.Add(new DataRelation("CTYPECTYPE_LY"
                , _ds.Tables["CTYPE"].Columns["CTYPE_NO"], _ds.Tables["CTYPE_LY"].Columns["CTYPE_NO"]));
            _ds.Tables["CTYPE_NUM"].Columns["GROUP_CHK"].DefaultValue = "F";

            // DataColumn[] _dca1 = new DataColumn[2];
            //_dca1[0] = _ds.Tables["CTYPE_NUM"].Columns["CTYPE_NO"];
            //_dca1[1] = _ds.Tables["CTYPE_NUM"].Columns["PRD_NO"];
            //DataColumn[] _dca2 = new DataColumn[2];
            //_dca2[0] = _ds.Tables["CTYPE_NUM_GROUP"].Columns["CTYPE_NO"];
            //_dca2[1] = _ds.Tables["CTYPE_NUM_GROUP"].Columns["GROUP_PRDT"];

            //_ds.Relations.Add(new DataRelation("CTYPE_NUMCTYPE_NUM_GROUP", _dca1, _dca2));

            _ds.Tables["CTYPE_NUM"].Columns.Add("DIS_CNT", typeof(decimal));

            foreach (DataRow dr in _ds.Tables["CTYPE_NUM"].Rows)
            {
                decimal _upFreeze = 0;
                decimal _upr = 0;

                if (!string.IsNullOrEmpty(dr["UPR"].ToString()))
                {
                    _upr = Convert.ToDecimal(dr["UPR"]);
                }
                if (!string.IsNullOrEmpty(dr["UP_FREEZE"].ToString()))
                {
                    _upFreeze = Convert.ToDecimal(dr["UP_FREEZE"]);
                }

                if (_upFreeze != 0 && _upr != 0)
                {
                    dr["DIS_CNT"] = _upFreeze / _upr * 100;
                }
            }
            return _ds;
        }

        public SunlikeDataSet GetHeadData(string cTypeNo)
        {
            DbPOSCustCType _dbCustCType = new DbPOSCustCType(Comp.Conn_DB);
            string _where = "";
            if (!string.IsNullOrEmpty(cTypeNo))
            {
                _where = "CTYPE_NO='" + cTypeNo + "'";
            }
            SunlikeDataSet _ds = _dbCustCType.GetData(_where);
            return _ds;
        }

        public SunlikeDataSet GetDataDsc(string sqlWhere)
        {
            DbPOSCustCType _dbCustCType = new DbPOSCustCType(Comp.Conn_DB);
            SunlikeDataSet _ds = _dbCustCType.GetDataDsc(sqlWhere);
            return _ds;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="changeDs"></param>
        /// <param name="bubbleException"></param>
        /// <returns></returns>
        public DataTable UpdateData(SunlikeDataSet changeDs, bool bubbleException)
        {
            Hashtable _ht = new System.Collections.Hashtable();
            if (changeDs.Tables.Contains("CTYPE"))
            {
                _ht["CTYPE"] = "CTYPE_NO,NAME,VALID_DAY,CARD_CHK,SAVING_CHK,PRDT_CHK,UP,SYS_DATE,USR,DEP,DSC_NO,BEGIN_DD,END_DD,COUNT_TYPE,NUM_SET";
            }
                if (changeDs.Tables.Contains("CTYPE_DIS"))
            {
                _ht["CTYPE_DIS"] = "CTYPE_NO,IDX_NO,DIS_CNT";
            }
            if (changeDs.Tables.Contains("CTYPE_NUM"))
            {
                _ht["CTYPE_NUM"] = "CTYPE_NO,PRD_NO,NUM_SET,DAY_ZQ,UP_FREEZE,USE_TIME,PRD_NAME,GROUP_PRDT,GROUP_CHK";
            }
            if (changeDs.Tables.Contains("CTYPE_LY"))
            {
                _ht["CTYPE_LY"] = "CTYPE_NO,IDX_NO,DIS_CNT";
            }
            if (changeDs.Tables.Contains("CTYPE_DSC"))
            {
                _ht["CTYPE_DSC"] = "DSC_NO,NAME";
            }

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

        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            if (tableName == "CTYPE")
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    POSCustCR _posCustCR = new POSCustCR();
                    bool _isUse = _posCustCR.CheckCTypeUse(dr["CTYPE_NO", DataRowVersion.Original].ToString());
                    if (_isUse)
                    {
                       throw new Exception(string.Format("卡类[{0}]已经有购卡记录，不能删除", dr["CTYPE_NO", DataRowVersion.Original]));
                    }
                }
            }
            base.BeforeUpdate(tableName, statementType, dr, ref status);
        }

        protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
        {
            //if (tableName == "CTYPE_NUM")
            //{
            //    Query _query = new Query();
            //    if (statementType == StatementType.Delete)
            //    {
            //        if (dr["GROUP_CHK", DataRowVersion.Original].ToString() == "T")
            //        {
            //            string _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
            //            string _cTypeNo = dr["CTYPE_NO", DataRowVersion.Original].ToString();
            //            string _sql = "DELETE FROM CTYPE_NUM WHERE GROUP_PRDT='" + _prdNo + "' AND CTYPE_NO='" + _cTypeNo + "'";
            //            _query.DoSQLString(_sql);
            //        }
            //    }
            //    else if (statementType == StatementType.Insert)
            //    {

            //    }
            //    else if (statementType == StatementType.Update)
            //    {
            //        if (dr["GROUP_CHK", DataRowVersion.Original].ToString() == "T")
            //        {
            //            string _prdNo = dr["PRD_NO", DataRowVersion.Original].ToString();
            //            string _cTypeNo = dr["CTYPE_NO", DataRowVersion.Original].ToString();
            //            string _sql = "DELETE FROM CTYPE_NUM WHERE GROUP_PRDT='" + _prdNo + "' AND CTYPE_NO='" + _cTypeNo + "'";
            //            _query.DoSQLString(_sql);
            //        }

            //        if (dr["GROUP_CHK"].ToString() == "T")
            //        {
            //            if (dr.Table.DataSet.Tables.Contains("CTYPE_NUM_GROUP"))
            //            {
            //                DataTable _dt = dr.Table.DataSet.Tables["CTYPE_NUM_GROUP"];
            //                string _sql = "";
            //                string _groupPrdt = dr["PRD_NO"].ToString();
            //                foreach (DataRow _dr in _dt.Select(""))
            //                {
            //                    _sql += "INSERT INTO CTYPE_NUM (CTYPE_NO,PRD_NO,GROUP_PRDT) VALUES ('" + _dr["PRD_NO"].ToString() + "','" + _dr["CTYPE_NO"].ToString() + "','" + _groupPrdt + "')";
            //                }
            //            }
            //        }
            //    }
            //}
            
            base.AfterUpdate(tableName, statementType, dr, ref status, recordsAffected);
        }
        protected override void BeforeDsSave(DataSet ds)
        {
            //if (ds.Tables.Contains("CTYPE_NUM_GROUP") && ds.Tables.Contains("CTYPE_NUM"))
            //{
            //    //if (ds.Relations.Contains("CTYPE_NUMCTYPE_NUM_GROUP"))
            //    //{
            //    //    ds.Relations.Remove("CTYPE_NUMCTYPE_NUM_GROUP");
            //    //}                
            //    //DataTable _dt = ds.Tables["CTYPE_NUM_GROUP"].Copy();
            //    //ds.Tables.Remove("CTYPE_NUM_GROUP");
            //    //ds.Merge(_dt);

            //    DataTable _dt = ds.Tables["CTYPE_NUM_GROUP"].GetChanges();
            //    if (_dt != null)
            //    {
            //        foreach (DataRow dr in _dt.Rows)
            //        {
            //            ds.Tables["CTYPE_NUM"].ImportRow(dr);
            //        }
                     
            //    }
            //}
            
            base.BeforeDsSave(ds);
        }
    }
}
