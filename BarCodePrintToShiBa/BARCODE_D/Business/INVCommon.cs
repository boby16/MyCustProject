using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using System.Collections.Generic;

namespace Sunlike.Business
{
    /// <summary>
    /// Summary description for INVCommon.
    /// </summary>
    public class INVCommon : Sunlike.Business.BizObject
    {
        /// <summary>
        /// 存货公用类
        /// </summary>
        public INVCommon()
        {
        }

        #region 取得实际回写原单的数量
        /// <summary>
        /// 取得实际回写原单的数量
        /// </summary>
        /// <param name="prdNo">产品代号</param>
        /// <param name="qty">退回数量</param>
        /// <param name="unit">退回单位</param>
        /// <param name="_ht">所含内容：表名TableName,单据别栏位名称IdName,单号栏位名称NoName,追踪项次栏位名称ItmName,单据别OsID,单号OsNO,追踪项次KeyItm</param>
        /// <returns></returns>
        public static decimal GetRtnQty(string prdNo, decimal qty, int unit, Hashtable _ht)
        {
            DbINVCommon _common = new DbINVCommon(Comp.Conn_DB);
            string oldUnit = _common.GetOldUnit(_ht);
            int _pkQty = 1;
            string _unitName = "";
            Prdt _prdt = new Prdt();
            _prdt.GetUnitDetail(prdNo, unit.ToString(), out _unitName, out _pkQty);
            qty *= _pkQty;
            _pkQty = 1;
            _prdt.GetUnitDetail(prdNo, oldUnit, out _unitName, out _pkQty);
            return qty / _pkQty;
        }
        #endregion

        /// <summary>
        /// 是否管控立账方式
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="bilId"></param>
        /// <returns></returns>
        public static bool IsControlZhangId(string usr, string bilId)
        {
            bool _result = false;
            if (!string.IsNullOrEmpty(usr) && !string.IsNullOrEmpty(bilId))
            {
                //管制立帐方式
                string _ctrlZhangId = Users.GetSpcPswdString(usr, "CTRL_ZHANG_ID");
                if (_ctrlZhangId.IndexOf(bilId) >= 0)
                {
                    _result = true;
                }
            }
            return _result;
        }

        /// <summary>
        /// IJ,IC,MM,MC,MB,MD 需要加上+-号
        /// </summary>
        /// <param name="bilId">单据别</param>
        /// <returns>单据的进销方向，1=进，2=销</returns>
        public static int GetBilOutIn(string bilId)
        {
            List<string> _lstIn = new List<string>(
                new string[] { "PC", "ZG", "BN", "SB", "LB", "IJ+", "XE", "XD", "XC", "XF"
                    , "IC+", "M2", "M5", "MM+", "MC+", "MB+", "MD+"}
                );

            List<string> _lstOut = new List<string>(
                 new string[] { "PB", "ZR", "BB", "SA", "LN", "IJ-", "XB", "XA", "XJ"
                     , "IC-" , "ML", "M3", "M4", "M6", "MM-", "MC-", "MB-", "MD-"}
                );

            string _outStockBil = "";// Comp.GetCompInfo("").CompanyInfo.OUTSTOCK_BIL;
            if (!string.IsNullOrEmpty(_outStockBil))
            {
                _lstIn.AddRange(_lstOut);
                _lstOut.Clear();

                foreach (string _bilId in _outStockBil.Split(','))
                {
                    _lstOut.Add(_bilId);
                    if (_lstIn.Contains(_bilId))
                    {
                        _lstIn.Remove(_bilId);
                    }
                }
            }

            if (_lstOut.Contains(bilId))
            {
                return 2;

            }
            return 1;
        }
    }
}
