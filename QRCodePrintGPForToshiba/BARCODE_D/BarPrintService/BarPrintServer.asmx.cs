using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Sunlike.Business;
using Sunlike.Common.CommonVar;
using System.Text;

namespace BarPrintService
{
	/// <summary>
	/// Summary description for BarPrint
	/// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class BarPrintServer : System.Web.Services.WebService
    {
        #region 板材、半成品和分条条码打印
        [WebMethod]
		public string GetMaxSN(string _prd_no,string _bat_no)
		{
			//取最大流水号
			BarPrint _br = new BarPrint();
			return _br.GetMaxNo(_prd_no, _bat_no);
		}
		[WebMethod]
		public bool IsPrinted(string bar_no, string sn, int page_count)
		{
            //判断条码是否已打印
            BarPrint _br = new BarPrint();
			return _br.IsPrinted(bar_no, sn, page_count);
        }
        [WebMethod]
        public bool BCIsPrinted(string _barNo, string _bat_no, string _sn)
        {
            //修改板材条码时
            //判断板材条码是否已打印
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.BCIsPrinted(_barNo, _bat_no, _sn);
        }
        [WebMethod]
        public bool SBIsPrinted(string _bat_no, string _sn)
        {
            //判断退货条码是否已打印
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.SBIsPrinted(_bat_no, _sn);
        }
        [WebMethod]
        public bool InUse(string _batNo)
        {
            //判断分条条码是否已使用
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.InUse(_batNo);
        }
        [WebMethod]
        public DataSet GetBarRec(string _barCode)
        {
            //取条码信息
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarRecord(_barCode);
        }
        [WebMethod]
        public DataSet GetBarChg(string _barCode)
        {
            //取条码信息(换货时调用)
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarChg(_barCode);
        }
        [WebMethod]
        public DataSet GetBarRec1(string _barCodeLst)
        {
            //取条码信息
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarRecord1(_barCodeLst);
        }
        [WebMethod]
        public DataSet GetBarRecCanDel(string _where)
        {
            //取条码信息(删除条码时用)
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarRecCanDel(_where);
        }
		[WebMethod]
		public DataSet SavePrintData(string usr, DataSet prtHisDS)
		{
			//正常打印时，保存打印信息
			BussBarPrint _br = new BussBarPrint();
            return _br.SavePrintData(usr, prtHisDS, false, false, false);
        }
        [WebMethod]
        public DataSet ModifyPrintData(string usr, DataSet prtHisDS)
        {
            //修改条码时，保存打印信息
            BussBarPrint _br = new BussBarPrint();
            return _br.SavePrintData(usr, prtHisDS, false, false, true);
        }
        [WebMethod]
        public DataSet SaveInitPrintData(string usr, DataSet prtHisDS)
        {
            //库存成品条码打印时，保存打印信息
            BussBarPrint _br = new BussBarPrint();
            return _br.SavePrintData(usr, prtHisDS, true, false, false);
        }
        [WebMethod]
        public DataSet SaveChangePrintData(string usr, DataSet prtHisDS)
        {
            //换货条码打印时，保存打印信息
            BussBarPrint _br = new BussBarPrint();
            return _br.SavePrintData(usr, prtHisDS, false, true, false);
        }
        [WebMethod]
        public void DeleteBarCode(string _where)
        {
            //删除条码
            BussBarPrint _br = new BussBarPrint();
            _br.DeleteBarCode(_where);
        }
        #endregion

        #region 转单作业
        [WebMethod]
        public void UpdatePassData(DataSet dataSet)
        {
            //转缴库单
            BussBarPrint _bbp = new BussBarPrint();
            _bbp.TOMM(dataSet);
        }
        [WebMethod]
        public void UpdateFaultData(DataSet dataSet)
        {
            //转异常通知单
            BussBarPrint _bbp = new BussBarPrint();
            _bbp.TOTY(dataSet);
        }
        [WebMethod]
        public DataSet GetDataToTY(string _sqlWhere)
        {
            //取待转异常单的序列号
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetDataToTY(_sqlWhere);
        }
        [WebMethod]
        public DataSet GetUndoTRBar(string _prdType, string _where)
        {
            //已转异常单的条码（转异常单撤销作业时调用）
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetUndoTRBar(_prdType, _where);
        }
        [WebMethod]
        public string UndoTR(string _barNoLst)
        {
            //转异常单撤销
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.UndoTR(_barNoLst);
        }
        #endregion

        #region 品质检验
        [WebMethod]
        public DataSet GetBarUnQC(string _barNo)
        {
            //取待检验的条码信息(逐条扫描)
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarUnQC(_barNo);
        }
        [WebMethod]
        public DataSet GetBarUnQCLst(string _barType)
        {
            //取待检验的条码信息（全部显示）
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarUnQCLst(_barType);
        }
        [WebMethod]
        public DataSet GetDataQC(bool _getSchema)
        {
            //取条码品质信息
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetDataQC(_getSchema);
        }
        [WebMethod]
        public DataSet GetDataQC1(string _sqlWhere)
        {
            //取条码品质信息
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetDataQC1(_sqlWhere);
        }
        [WebMethod]
        public void UpdataDataQC(DataSet changeDS)
        {
            //更新条码品质信息
            string _barNoList = "";
            foreach (DataRow _dr in changeDS.Tables[0].Rows)
            {
                if (_barNoList != "")
                    _barNoList += "','";
                _barNoList += _dr["BAR_NO"].ToString();
            }
            DataSet _ds = GetDataQC1(" AND BAR_NO IN ('" + _barNoList + "')");
            if (changeDS != null && changeDS.Tables.Count > 0)
            {
                DataRow[] _drAry = null;
                foreach (DataRow _dr in changeDS.Tables[0].Rows)
                {
                    DataRow _drNew = null;
                    _drAry = _ds.Tables[0].Select("BAR_NO='" + _dr["BAR_NO"].ToString() + "'");
                    if (_drAry != null && _drAry.Length > 0)
                    {
                        _drNew = _drAry[0];
                        if (changeDS.Tables[0].Columns.Contains("OS_BAR_NO"))
                            _drNew["OS_BAR_NO"] = _dr["OS_BAR_NO"];
                        _drNew["QC"] = _dr["QC"];
                        if (changeDS.Tables[0].Columns.Contains("SPC_NO"))
                            _drNew["SPC_NO"] = _dr["SPC_NO"];
                        _drNew["STATUS"] = _dr["STATUS"];//状态：QC：品检；ML：领料；FT：分条；SW：送缴；TY：转异常
                        //_drNew["BAT_NO"] = _dr["BAT_NO"];
                        _drNew["BIL_ID"] = _dr["BIL_ID"];
                        _drNew["BIL_NO"] = _dr["BIL_NO"];
                        _drNew["REM"] = _dr["REM"];
                        if (changeDS.Tables[0].Columns.Contains("WH_SJ"))//送缴库位
                            _drNew["WH_SJ"] = _dr["WH_SJ"];
                        if (changeDS.Tables[0].Columns.Contains("PRC_ID"))//处理方式
                            _drNew["PRC_ID"] = _dr["PRC_ID"];
                        if (changeDS.Tables[0].Columns.Contains("USR_QC"))//品检人员
                            _drNew["USR_QC"] = _dr["USR_QC"];
                        if (changeDS.Tables[0].Columns.Contains("QC_DATE"))//品检时间
                            _drNew["QC_DATE"] = _dr["QC_DATE"];
                        if (changeDS.Tables[0].Columns.Contains("USR_SJ"))//送缴人员
                            _drNew["USR_SJ"] = _dr["USR_SJ"];
                        if (changeDS.Tables[0].Columns.Contains("SJ_DATE"))//送缴时间
                            _drNew["SJ_DATE"] = _dr["SJ_DATE"];
                        if (changeDS.Tables[0].Columns.Contains("CUS_NO"))//送缴时间
                            _drNew["TO_CUST"] = _dr["CUS_NO"];
                    }
                    else
                    {
                        _drNew = _ds.Tables[0].NewRow();
                        _drNew["BAR_NO"] = _dr["BAR_NO"];
                        if (changeDS.Tables[0].Columns.Contains("OS_BAR_NO"))
                            _drNew["OS_BAR_NO"] = _dr["OS_BAR_NO"];
                        _drNew["QC"] = _dr["QC"];
                        if (changeDS.Tables[0].Columns.Contains("SPC_NO"))
                            _drNew["SPC_NO"] = _dr["SPC_NO"];
                        _drNew["STATUS"] = _dr["STATUS"];//状态：QC：品检；ML：领料；FT：分条；SW：送缴
                        _drNew["BAT_NO"] = _dr["BAT_NO"];
                        _drNew["BIL_ID"] = _dr["BIL_ID"];
                        _drNew["BIL_NO"] = _dr["BIL_NO"];
                        _drNew["REM"] = _dr["REM"];
                        if (changeDS.Tables[0].Columns.Contains("WH_SJ"))//送缴库位
                            _drNew["WH_SJ"] = _dr["WH_SJ"];
                        if (changeDS.Tables[0].Columns.Contains("PRC_ID"))//处理方式
                            _drNew["PRC_ID"] = _dr["PRC_ID"];
                        if (changeDS.Tables[0].Columns.Contains("USR_QC"))//品检人员
                            _drNew["USR_QC"] = _dr["USR_QC"];
                        if (changeDS.Tables[0].Columns.Contains("QC_DATE"))//品检时间
                            _drNew["QC_DATE"] = _dr["QC_DATE"];
                        if (changeDS.Tables[0].Columns.Contains("USR_SJ"))//送缴人员
                            _drNew["USR_SJ"] = _dr["USR_SJ"];
                        if (changeDS.Tables[0].Columns.Contains("SJ_DATE"))//送缴时间
                            _drNew["SJ_DATE"] = _dr["SJ_DATE"];
                        if (changeDS.Tables[0].Columns.Contains("CUS_NO"))//送缴时间
                            _drNew["TO_CUST"] = _dr["CUS_NO"];
                        _ds.Tables[0].Rows.Add(_drNew);
                    }
                }
            }
            BussBarPrint _br = new BussBarPrint();
            _br.UpdataDataQC(_ds);
        }
        [WebMethod]
        public void UpdataDataBoxBat(DataSet changeDS)
        {
            //成品送缴时调用
            //更新箱条码信息
            BussBarPrint _br = new BussBarPrint();
            StringBuilder _boxNoList = new StringBuilder();
            foreach (DataRow _dr in changeDS.Tables[0].Rows)
            {
                if (_boxNoList.Length > 0)
                    _boxNoList.Append("','");
                _boxNoList.Append(_dr["BOX_NO"].ToString());
            }
            DataSet _ds = _br.GetDataBox(" AND BOX_NO IN ('" + _boxNoList.ToString() + "')");

            _ds.Tables[0].Columns.Add("USR_SJ", System.Type.GetType("System.String"));
            _ds.Tables[0].Columns.Add("SJ_DATE", System.Type.GetType("System.DateTime"));

            if (changeDS != null && changeDS.Tables.Count > 0)
            {
                DataRow[] _drAry = null;
                foreach (DataRow _dr in changeDS.Tables[0].Rows)
                {
                    DataRow _drNew = null;
                    _drAry = _ds.Tables[0].Select("BOX_NO='" + _dr["BOX_NO"].ToString() + "'");
                    if (_drAry != null && _drAry.Length > 0)
                    {
                        _drNew = _drAry[0];
                        _drNew["STATUS"] = _dr["STATUS"];//状态：QC：品检；ML：领料；FT：分条；SW：送缴；TY：转异常
                        _drNew["BIL_ID"] = _dr["BIL_ID"];
                        _drNew["BIL_NO"] = _dr["BIL_NO"];
                        _drNew["WH_SJ"] = _dr["WH_SJ"];//送缴库位
                        _drNew["REM"] = _dr["REM"];
                        _drNew["USR_SJ"] = _dr["USR_SJ"];
                        _drNew["SJ_DATE"] = _dr["SJ_DATE"];
                    }
                    else
                    {
                        _drNew = _ds.Tables[0].NewRow();
                        _drNew["BOX_NO"] = _dr["BOX_NO"];
                        _drNew["STATUS"] = _dr["STATUS"];//状态：QC：品检；ML：领料；FT：分条；SW：送缴；TY：转异常
                        _drNew["BIL_ID"] = _dr["BIL_ID"];
                        _drNew["BIL_NO"] = _dr["BIL_NO"];
                        _drNew["WH_SJ"] = _dr["WH_SJ"];//送缴库位
                        _drNew["REM"] = _dr["REM"];
                        _drNew["USR_SJ"] = _dr["USR_SJ"];
                        _drNew["SJ_DATE"] = _dr["SJ_DATE"];
                        _ds.Tables[0].Rows.Add(_drNew);
                    }
                }
            }
            _br.UpdataDataBoxBat(_ds);
        }
        [WebMethod]
        public void UpdataDataSPC(DataSet changeDS)
        {
            //更新不合格原因档
            BussBarPrint _br = new BussBarPrint();
            _br.UpdataDataSPC(changeDS);
        }
        #endregion

        #region 送缴作业
        [WebMethod]
        public DataSet GetBCUnToMMLst(string _where)
        {
            //取已检验未送缴的板材条码信息
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBCUnToMMLst(_where);
        }
        [WebMethod]
        public DataSet GetFTUnToMMLst()
        {
            //取已检验未送缴的成品条码信息
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetFTUnToMMLst();
        }
        [WebMethod]
        public string UndoToMM(string _barNoLst, string _boxNoList, string _usr)
        {
            //撤销送缴
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.UndoToMM(_barNoLst, _boxNoList, _usr);
        }
        #endregion

        #region 缴库作业
        [WebMethod]
        public DataSet GetBCUnMMLst(string _where)
        {
            //取已送缴未缴库的条码信息
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBCUnMMLst(_where);
        }
        [WebMethod]
        public DataSet GetBoxUnMM(string _where)
        {
            //取已送缴未缴库的箱条码
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBoxUnMM(_where);
        }
        [WebMethod]
        public DataSet GetBoxUnMMLst(string _where)
        {
            //取已送缴未缴库的箱条码及箱明细
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBoxUnMMLst(_where);
        }
        [WebMethod]
        public DataSet GetUndoMMBar(string _prdType, string _where)
        {
            //已缴库的条码（缴库撤销作业时调用）
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetUndoMMBar(_prdType, _where);
        }
        [WebMethod]
        public string UndoMM(string _barNoLst, string _boxNoList)
        {
            //缴库撤销
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.UndoMM(_barNoLst, _boxNoList);
        }
        #endregion

        #region 修改品质信息
        [WebMethod]
        public DataSet GetBarQC(string _barNo, string _barType)
        {
            //修改品质信息时调用
            // 取得待修改品质信息的序列号
            // _barNo：序列号
            // _barType：产品类型（1:板材；2:半成品；3:成品）
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarQC(_barNo, _barType);
        }
        [WebMethod]
        public void UndoBarQC(string _barNo)
        {
            //修改品质信息时调用
            //撤销品检
            BussBarPrint _bbp = new BussBarPrint();
            _bbp.UndoBarQC(_barNo);
        }
        #endregion

        #region 箱操作

        #region 箱信息查询
        [WebMethod]
        public DataSet GetBoxBar(string _boxBar, string _isBox)
        {
            //取已打印箱条码:_boxBar:箱条码;_isBox:T:已装箱的箱条码;F:未装箱的箱条码;null或空:所有
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBar(_boxBar, _isBox, " AND ISNULL(A.STOP_ID,'F')<>'T' ");
        }
        [WebMethod]
        public DataSet GetBoxBar1(string _sqlWhere)
        {
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBar("", "", _sqlWhere);
        }
        [WebMethod]
        public DataSet GetBoxBarRec(string _where)
        {
            //取得分条条码
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBarRec(_where);
        }
        [WebMethod]
        public DataSet GetBoxBarPrint(string _boxNo)
        {
            //取得分条条码
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBarPrint(_boxNo);
        }
        #endregion

        #region 装箱作业
        [WebMethod]
        public string BarBoxing(string _boxNo,string _barCode,string _usr,string _dep)
        {
            //装箱
            string _err = "";
            BussBarPrint _bc = new BussBarPrint();
            DataSet _changedDS = _bc.GetBarBoxRec(_boxNo, _barCode);
            if (_changedDS != null && _changedDS.Tables.Count == 2 && _changedDS.Tables[0].Rows.Count > 0 && _changedDS.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToInt32(_changedDS.Tables[0].Rows[0]["QTY"]) != _changedDS.Tables[1].Rows.Count)
                {
                    _err += "装箱数量不符!\n";
                }
                else
                {
                    _changedDS.Tables[0].Columns.Add("FLAG", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("USR_BOX", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("BOX_DATE", System.Type.GetType("System.DateTime"));
                    _changedDS.Tables[0].Rows[0]["BOX_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                    _changedDS.Tables[0].Rows[0]["USR"] = _usr;
                    _changedDS.Tables[0].Rows[0]["DEP"] = _dep;
                    _changedDS.Tables[0].Rows[0]["QC"] = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    _changedDS.Tables[0].Rows[0]["CONTENT"] = "=" + _changedDS.Tables[1].Rows.Count + ";";
                    //_changedDS.Tables[0].Rows[0]["PRD_NO"] = _changedDS.Tables[1].Rows[0]["PRD_NO"];

                    string _time = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    string _prdNo = _changedDS.Tables[0].Rows[0]["PRD_NO"].ToString();//箱品号
                    string _batNo = _changedDS.Tables[0].Rows[0]["BAT_NO"].ToString();//箱批号
                    string _qc = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    string _batNo1 = "";
                    foreach (DataRow dr in _changedDS.Tables[1].Rows)
                    {
                        if (_prdNo != dr["PRD_NO"].ToString())
                        {
                            _err += "产品[" + dr["PRD_NO"].ToString() + "]不能装入此箱!\n";
                            break;
                        }
                        if (_batNo.IndexOf(dr["BAR_NO"].ToString().Substring(12, 7)) < 0)
                        {
                            _err += "产品批号与箱批号不一致，不能装入此箱!\n";
                            break;
                        }
                        else
                        {
                            if (_batNo1 != "")
                                _batNo1 += "/";
                            _batNo1 += dr["BAR_NO"].ToString().Substring(12, 7);
                        }
                        if (_qc != dr["QC"].ToString())
                        {
                            _err += "产品品质信息不一致，不能装箱!\n";
                            break;
                        }

                        dr["BOX_NO"] = _boxNo;
                        dr["UPDDATE"] = _time;
                        dr["USR_BOX"] = _usr;
                        dr["BOX_DATE"] = _time;
                    }
                    string[] _batArr = _batNo.Split('/');
                    foreach (string _bat in _batArr)
                    {
                        if (_batNo1.IndexOf(_bat) < 0)
                            _err += "批号未装全！";
                    }
                    if (_err == "")
                    {
                        DataTable _dtErr = _bc.BarBoxing(_changedDS);
                        if (_dtErr.Rows.Count > 0)
                            throw new Exception(_dtErr.Rows[0]["REM"].ToString());
                    }
                }
            }
            else
                _err += "记录为空，请重新装箱!\n";
            return _err;
        }
        [WebMethod]
        public string InitBarBoxing(string _boxNo, string _barCode, string _wh, string _rem, string _usr, string _dep)
        {
            //装箱
            string _err = "";
            BussBarPrint _bc = new BussBarPrint();
            DataSet _changedDS = _bc.GetBarBoxRec(_boxNo, _barCode);
            if (_changedDS != null && _changedDS.Tables.Count == 2 && _changedDS.Tables[0].Rows.Count > 0 && _changedDS.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToInt32(_changedDS.Tables[0].Rows[0]["QTY"]) != _changedDS.Tables[1].Rows.Count)
                {
                    _err += "装箱数量不符！\n";
                }
                else
                {
                    _changedDS.Tables[0].Columns.Add("FLAG", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("USR_BOX", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("BOX_DATE", System.Type.GetType("System.DateTime"));
                    _changedDS.Tables[0].Rows[0]["FLAG"] = "T";
                    _changedDS.Tables[0].Rows[0]["BOX_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                    _changedDS.Tables[0].Rows[0]["USR"] = _usr;
                    _changedDS.Tables[0].Rows[0]["DEP"] = _dep;
                    _changedDS.Tables[0].Rows[0]["WH"] = _wh;

                    _changedDS.Tables[0].Rows[0]["CONTENT"] = "=" + _changedDS.Tables[1].Rows.Count + ";";
                    //_changedDS.Tables[0].Rows[0]["PRD_NO"] = _changedDS.Tables[1].Rows[0]["PRD_NO"];

                    string _time = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    string _prdNo = _changedDS.Tables[0].Rows[0]["PRD_NO"].ToString();
                    string _batNo = _changedDS.Tables[0].Rows[0]["BAT_NO"].ToString(); 
                    string _qc = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    string _batNo1 = "";
                    foreach (DataRow dr in _changedDS.Tables[1].Rows)
                    {
                        if (_prdNo != dr["PRD_NO"].ToString())
                        {
                            _err += "产品[" + dr["PRD_NO"].ToString() + "]不能装入此箱！\n";
                            break;
                        }
                        if (_batNo.IndexOf(dr["BAR_NO"].ToString().Substring(12, 7)) < 0)
                        {
                            _err += "产品批号与箱批号不统一，不能装入此箱！\n";
                            break;
                        }
                        else
                        {
                            if (_batNo1 != "")
                                _batNo1 += "/";
                            _batNo1 += dr["BAR_NO"].ToString().Substring(12, 7);
                        }
                        if (_qc != dr["QC"].ToString())
                        {
                            _err += "产品品质信息不一致，不能装箱!\n";
                            break;
                        }

                        dr["BOX_NO"] = _boxNo;
                        dr["WH"] = _wh;
                        dr["UPDDATE"] = _time;
                        dr["REM"] = _rem;
                        dr["USR_BOX"] = _usr;
                        dr["BOX_DATE"] = _time;
                    }
                    string[] _batArr = _batNo.Split('/');
                    foreach (string _bat in _batArr)
                    {
                        if (_batNo1.IndexOf(_bat) < 0)
                            _err += "批号未装全！";
                    }
                    if (_err == "")
                    {
                        DataTable _dtErr = _bc.BarBoxing(_changedDS);
                        if (_dtErr.Rows.Count > 0)
                            throw new Exception(_dtErr.Rows[0]["REM"].ToString());
                    }
                }
            }
            return _err;
        }
        [WebMethod]
        public string WHBarBoxing(string _boxNo, string _barCode, string _usr, string _dep, string _wh)
        {
            //装箱
            string _err = "";
            BussBarPrint _bc = new BussBarPrint();
            DataSet _changedDS = _bc.GetBarBoxRec(_boxNo, _barCode);
            if (_changedDS != null && _changedDS.Tables.Count == 2 && _changedDS.Tables[0].Rows.Count > 0 && _changedDS.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToInt32(_changedDS.Tables[0].Rows[0]["QTY"]) != _changedDS.Tables[1].Rows.Count)
                {
                    _err += "装箱数量不符！\n";
                }
                else
                {
                    _changedDS.Tables[0].Columns.Add("FLAG", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("USR_WHBOX", System.Type.GetType("System.String"));
                    _changedDS.Tables[1].Columns.Add("WHBOX_DATE", System.Type.GetType("System.DateTime"));
                    _changedDS.Tables[0].Rows[0]["BOX_DD"] = DateTime.Today.ToString(Comp.SQLDateFormat);
                    _changedDS.Tables[0].Rows[0]["USR"] = _usr;
                    _changedDS.Tables[0].Rows[0]["DEP"] = _dep;
                    _changedDS.Tables[0].Rows[0]["WH"] = _wh;
                    _changedDS.Tables[0].Rows[0]["QC"] = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    _changedDS.Tables[0].Rows[0]["CONTENT"] = "=" + _changedDS.Tables[1].Rows.Count + ";";
                    //_changedDS.Tables[0].Rows[0]["PRD_NO"] = _changedDS.Tables[1].Rows[0]["PRD_NO"];

                    string _time = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
                    string _prdNo = _changedDS.Tables[0].Rows[0]["PRD_NO"].ToString();
                    string _batNo = _changedDS.Tables[0].Rows[0]["BAT_NO"].ToString();
                    string _qc = _changedDS.Tables[1].Rows[0]["QC"].ToString();
                    string _batNo1 = "";
                    foreach (DataRow dr in _changedDS.Tables[1].Rows)
                    {
                        if (_prdNo != dr["PRD_NO"].ToString())
                        {
                            _err += "产品[" + dr["PRD_NO"].ToString() + "]不能装入此箱！\n";
                            break;
                        }
                        if (_batNo.IndexOf(dr["BAR_NO"].ToString().Substring(12, 7)) < 0)
                        {
                            _err += "产品批号与箱批号不统一，不能装入此箱！\n";
                            break;
                        }
                        else
                        {
                            if (_batNo1 != "")
                                _batNo1 += "/";
                            _batNo1 += dr["BAR_NO"].ToString().Substring(12, 7);
                        }
                        if (_qc != dr["QC"].ToString())
                        {
                            _err += "产品品质信息不统一，不能装箱";
                            break;
                        }

                        dr["BOX_NO"] = _boxNo;
                        dr["UPDDATE"] = _time;
                        dr["USR_WHBOX"] = _usr;
                        dr["WHBOX_DATE"] = _time;
                    }
                    string[] _batArr = _batNo.Split('/');
                    foreach (string _bat in _batArr)
                    {
                        if (_batNo1.IndexOf(_bat) < 0)
                            _err += "批号未装全！";
                    }
                    if (_err == "")
                    {
                        DataTable _dtErr = _bc.BarBoxing(_changedDS);
                        if (_dtErr.Rows.Count > 0)
                            throw new Exception(_dtErr.Rows[0]["REM"].ToString());
                    }
                }
            }
            else
                _err += "记录为空，请重新装箱!\n";
            return _err;
        }
        [WebMethod]
        public void UnDoBox(string _boxNo, string _usr, string _type)
        {
            //拆箱
            BussBarPrint _bc = new BussBarPrint();
            _bc.UnDoBox(_boxNo, _usr, _type);
        }
        #endregion

        #region 箱条码打印
        [WebMethod]
        public DataSet SaveBoxPrintData(string usr, DataSet prtHisDS)
        {
            //保存箱条码打印信息
            BussBarPrint _br = new BussBarPrint();
            return _br.SaveBoxPrintData(usr, prtHisDS);
        }
        [WebMethod]
        public DataSet UpdateBoxPrintData(DataSet _changedDS)
        {
            //保存箱条码打印信息
            BussBarPrint _br = new BussBarPrint();
            DataSet _ds = new DataSet();
            _ds.Merge(_br.UpdataDataBox(_changedDS));
            return _ds;
        }
        #endregion

        #endregion

        #region 基础资料
        //public CredentialSoapHeader _header = new CredentialSoapHeader();
        //[SoapHeader("_header")]
        [WebMethod]
        public DataSet GetUsers()
        {
            //if (_header.UserName != "oNlInEeRp" || _header.UserPassword != "oNlInEeRp")
            //{
            //    _header.DidUnderstand = false;
            //}
            //else
            //    _header.DidUnderstand = true;
            DataSet _ds = new DataSet();
            Query _query = new Query();
            string _sql = "select USR,NAME,DEP,(SELECT TOP 1 DEP FROM DEPT) AS TOP_DEP,COMPNO from SUNSYSTEM..PSWD where COMPNO='" + Comp.CompNo + "' and isnull(ISGROUP,'')='T' and (isnull(E_DAT,getdate()+1)>getdate() OR E_DAT='1899-12-30')";
            _ds = _query.DoSQLString(Comp.Conn_DB, _sql);
            return _ds;
        }
        [WebMethod]
		public DataSet GetBarRole()
		{
			//取条码分段规则
			BarCode _bc = new BarCode();
			DataSet _ds = (DataSet)(_bc.GetBarRoleData());
            _ds.ExtendedProperties["DB_LANGUAGE"] = Comp.DataBaseLanguage;
			return _ds;
        }
        [WebMethod]
        public DataSet GetBarDoc()
        {
            //取条码分段规则
            BarCode _bc = new BarCode();
            DataSet _ds = new DataSet();
            _ds.Merge(_bc.GetBarDoc(""));
            return _ds;
        }
        [WebMethod]
        public string GetBarConfig(string _dep)
        {
            //箱编码原则
            string _pat = "";
            BarBox _barBox = new BarBox();
            SunlikeDataSet _dsBoxConfig = _barBox.GetBoxConfig(_dep);
            if (_dsBoxConfig.Tables["BOX_CONFIG"].Rows.Count > 0)
            {
                _pat = _dsBoxConfig.Tables["BOX_CONFIG"].Rows[0]["PAT"].ToString();
            }
            return _pat;
        }
        [WebMethod]
        public bool LoginValidate(string _uid, string _pwd)
        {
            //登录用户验证
            BussBarPrint _bs = new BussBarPrint();
            return _bs.CheckUserPswd(_uid, _pwd);
        }
        [WebMethod]
        public string GetUsrDep(string _uid)
        {
            //取用户所属部门
            string _depNo = "";
            try
            {
                Users _user = new Users();
                _depNo = _user.GetUserDepNo(_uid);
            }
            catch
            {
                Dept _dep = new Dept();
                _depNo = _dep.GetTopDept();
            }
            return _depNo;
        }
        [WebMethod]
        public DataSet GetComp()
        {
            //取公司资料
            DataSet _ds = new DataSet();
            Comp _comp = new Comp();
            _ds=_comp.GetData(Comp.CompNo, false);
            return _ds;
        }
        [WebMethod]
        public DataSet GetSpcLst(string _batNo)
        {
            //取异常原因档
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetSpcLst(_batNo);
        }
        [WebMethod]
        public DataSet GetCust(string _cusNo)
        {
            //取客户资料档
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetCust(_cusNo);
        }
        [WebMethod]
        public bool UserIsExist(string _usr)
        {
            //判断用户是否存在
            Users _users = new Users();
            DataSet _ds = _users.GetDataByCondition(" AND USR='" + _usr + "' AND COMPNO='" + Comp.CompNo + "'");
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                return true;
            return false;
        }
        #endregion

        #region QueryWin 方法
        [WebMethod]
		public DataSet QueryWinData(string _no, string _name, string _others, string _table, string _sqlWhere)
		{
			//查询窗口
			DataSet _ds = new DataSet();
			if (!String.IsNullOrEmpty(_no) && !String.IsNullOrEmpty(_name) && !String.IsNullOrEmpty(_table))
			{
				Query _query = new Query();
                string _sql = "";
                if (_table == "PSWD")
                    _sql = "SELECT " + _no + "," + _name + _others + " FROM SunSystem.." + _table + " WHERE 1=1 AND ISNULL(COMP_BOSS,'')<>'T' AND COMPNO='" + Comp.CompNo + "' " + _sqlWhere;
                else if (_table == "BAR_IDX")
                    _sql = "SELECT " + _no + "," + _name + _others + " FROM " + Comp.DRP_Prop["BarPrintDB"].ToString() + ".." + _table + " WHERE 1=1 " + _sqlWhere;
                else
				    _sql = "SELECT " + _no + "," + _name + _others +" FROM " + _table + " WHERE 1=1 " + _sqlWhere;
				_ds = _query.DoSQLString(_sql);
				_ds.Tables[0].TableName = _table;
			}
			return _ds;
        }
        [WebMethod]
        public DataSet QueryMOData(string _sqlWhere)
        {
            //查询制令单信息
            DataSet _ds = new DataSet();
            Query _query = new Query();
            StringBuilder _sql = new StringBuilder();
            _sql.Append("SELECT A.MO_DD,A.MO_NO,A.MRP_NO AS PRD_NO,B.SPC,A.PRD_MARK,B.NAME AS PRD_NAME,A.WH,C.NAME AS WH_NAME,A.CUS_NO,D.NAME AS CUS_NAME,A.UNIT,B.UT AS UT,")
                .Append("A.QTY,A.QTY1,A.QTY_FIN,A.QTY_LOST,A.BAT_NO,A.ID_NO,A.USR,A.DEP,B.IDX1,INDX.NAME AS IDX_NAME,A.REM FROM MF_MO A ")
                .Append("LEFT JOIN PRDT B ON B.PRD_NO=A.MRP_NO LEFT JOIN INDX ON B.IDX1=INDX.IDX_NO LEFT JOIN MY_WH C ON C.WH=A.WH LEFT JOIN CUST D ON D.CUS_NO=A.CUS_NO WHERE 1=1 " + _sqlWhere);
            _ds = _query.DoSQLString(_sql.ToString());
            _ds.Tables[0].TableName = "MF_MO";
            return _ds;
        }
        [WebMethod]
        public DataSet QuerySAData(string _sqlWhere)
        {
            //查询销货单信息
            DataSet _ds = new DataSet();
            Query _query = new Query();
            string _sql = "SELECT A.PS_NO,A.PS_DD,B.NAME AS CUS_NAME,A.CUS_NO FROM MF_PSS A "
                        + "LEFT JOIN CUST B ON B.CUS_NO=A.CUS_NO WHERE 1=1 " + _sqlWhere
                        + " ORDER BY A.PS_NO DESC ";
            _ds = _query.DoSQLString(_sql.ToString());
            _ds.Tables[0].TableName = "MF_PSS";
            return _ds;
        }
        [WebMethod]
        public DataSet GetDept()
        {
            //取部门
            DataSet _ds = new DataSet();
            Dept _dept = new Dept();
            _ds.Merge(_dept.GetData());
            return _ds;
        }
        [WebMethod]
        public DataSet GetBatNo()
        {
            //取批号
            Bat _bat = new Bat();
            int _totalPage = 0;
            int _totalRow = 0;
            return (DataSet)(_bat.GetData("", 1, 10000, ref _totalPage, ref _totalRow));
        }
        [WebMethod]
        public DataSet GetBatNo1(string batNo)
        {
            //取批号
            Bat _bat = new Bat();
            return (DataSet)(_bat.GetData(batNo));
        }
        [WebMethod]
        public DataSet GetBatNo2(string _sqlWhere)
        {
            //取板材批号
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBatNo(_sqlWhere);
        }
        [WebMethod]
        public DataSet GetPrdtFull(string _prd_no, string _idx)
        {
            //取单个货品
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetPrdtFull(_prd_no, _idx);
        }
        [WebMethod]
        public DataSet GetWh(string _usr, string _whNo)
        {
            //库位资料
            DataSet _ds = new DataSet();
            WH _wh = new WH();
            _ds.Merge(_wh.GetData(_usr, _whNo));
            return _ds;
        }
        [WebMethod]
        public DataSet GetBarPrtName(string typeId,string idx, string _where)
        {
            //取特殊打印名称
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetBarPrtName(typeId, idx,_where);
        }
        [WebMethod]
        public string UpdateBarPrtName(DataSet _changeDs)
        {
            //更新特殊打印名称资料
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.UpdateBarPrtName(_changeDs);
        }
        #endregion

        #region 权限设定
        [WebMethod]
        public DataSet GetUserRightAll(string _usr)
        {
            BussBarPrint _bbp = new BussBarPrint();
            return _bbp.GetUserRightAll(_usr);
        }
        [WebMethod]
        public string GetUserRight(string _usr, string _pgm)
        {
            string _right = "N";
            BussBarPrint _bbp = new BussBarPrint();
            DataSet _ds = _bbp.GetUserRight(_usr, _pgm);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                _right = _ds.Tables[0].Rows[0]["INS"].ToString();
            }
            else
            {
                Users _users = new Users();
                if (_users.IsCompBoss(_usr))
                    _right = "Y";
            }
            return _right;
        }
        [WebMethod]
        public DataSet UpdateUserRight(DataSet ChangedDS,string _usr)
        {
            DataSet _dsResult = new DataSet();
            BussBarPrint _bbp = new BussBarPrint();
            Query _query = new Query();
            _query.RunSql(Comp.Conn_SunSystem, "DELETE FROM FX_PSWD WHERE ROLENO='" + _usr + "'");

            DataSet _ds = _bbp.GetUserRightAll(_usr);
            DataRow _dr;
            string _compNo = Comp.CompNo;
            foreach (DataRow dr in ChangedDS.Tables[0].Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    _dr = _ds.Tables[0].NewRow();
                    _dr["COMP_BOSS"] = "";
                    _dr["USR"] = _usr;
                    _dr["COMPNO"] = _compNo;
                    _dr["ROLENO"] = _usr;
                    _dr["PGM"] = dr["PGM"];
                    _dr["INS"] = "Y";
                    _ds.Tables[0].Rows.Add(_dr);
                }
            }
            _dsResult.Merge(_bbp.UpdateUserRight(_ds, _usr));
            return _dsResult;
        }
        #endregion

        #region 报表
        [WebMethod]
        public DataSet GetSAStat(string _sql)
        {
            //销货统计信息
            Query _query = new Query();
            DataSet _ds = _query.DoSQLString(_sql);
            _ds.Tables[0].TableName = "SA_STAT";
            return _ds;
        }
        #region 取序列号品质信息
        [WebMethod]
        public DataSet GetBarQCSta(string _where, int i)
        {
            //取序列号品质信息
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBarQCSta(_where, i);

        }
        [WebMethod]
        public DataSet GetRptBarQCList(string _where)
        {
            //取序列号品质信息
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetRptBarQCList(_where);

        }
        [WebMethod]
        public DataSet GetBoxBarRpt(string _where)
        {
            //取销货单序列号
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetBoxBarRpt(_where);
        }
        #endregion

        #region 产品历史查询
        [WebMethod]
        public DataSet GetPrdtHisList(string _where)
        {
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetPrdtHisList(_where);
        }
        #endregion

        #region 产品生产统计报表
        [WebMethod]
        public DataSet GetPrdtStat(string _where, string _whereb)
        {
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetPrdtStat(_where, _whereb);
        }
        #endregion

        #endregion

        #region 取销货单序列号
        [WebMethod]
        public DataSet GetRptSABarQClist(string _where)
        {
            //取销货单序列号
            BussBarPrint _bc = new BussBarPrint();
            DataSet _ds = _bc.GetRptSABarQClist(_where);
            return _ds;
        }
        [WebMethod]
        public DataSet GetSABarRec(string _where)
        {
            //取销货单序列号
            BussBarPrint _bc = new BussBarPrint();
            DataSet _ds = _bc.GetSABarRec(_where);
            return _ds;
        }
        [WebMethod]
        public string SABarQC(DataSet _dsSA)
        {
            //销货单序列号品检
            BussBarPrint _bc = new BussBarPrint();
            return _bc.SABarQC(_dsSA);
        }

        #endregion

        #region 出货条码替换
        [WebMethod]
        public DataSet GetChgBar(string _where)
        {
            //取销货替换条码
            BussBarPrint _bc = new BussBarPrint();
            DataSet _ds = _bc.GetChgBar(_where);
            return _ds;
        }
        [WebMethod]
        public string ChangeSABar(DataSet changeDs)
        {
            //销货条码替换
            BussBarPrint _bc = new BussBarPrint();
            return _bc.ChangeSABar(changeDs);
        }
        [WebMethod]
        public int GetMaxSAItm(string saNo)
        {
            //取销货条码TF_PSS3最大项次
            BussBarPrint _bc = new BussBarPrint();
            return _bc.GetSAMaxItm(saNo);
        }
        [WebMethod]
        public string LockSA(string _saNo, string _lockMan)
        {
            //锁单
            BussBarPrint _bc = new BussBarPrint();
            return _bc.LockSA(_saNo, _lockMan);
        }
        #endregion
    }
}
