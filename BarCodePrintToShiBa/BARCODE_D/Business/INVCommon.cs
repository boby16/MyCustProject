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
        /// ���������
        /// </summary>
        public INVCommon()
        {
        }

        #region ȡ��ʵ�ʻ�дԭ��������
        /// <summary>
        /// ȡ��ʵ�ʻ�дԭ��������
        /// </summary>
        /// <param name="prdNo">��Ʒ����</param>
        /// <param name="qty">�˻�����</param>
        /// <param name="unit">�˻ص�λ</param>
        /// <param name="_ht">�������ݣ�����TableName,���ݱ���λ����IdName,������λ����NoName,׷�������λ����ItmName,���ݱ�OsID,����OsNO,׷�����KeyItm</param>
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
        /// �Ƿ�ܿ����˷�ʽ
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="bilId"></param>
        /// <returns></returns>
        public static bool IsControlZhangId(string usr, string bilId)
        {
            bool _result = false;
            if (!string.IsNullOrEmpty(usr) && !string.IsNullOrEmpty(bilId))
            {
                //�������ʷ�ʽ
                string _ctrlZhangId = Users.GetSpcPswdString(usr, "CTRL_ZHANG_ID");
                if (_ctrlZhangId.IndexOf(bilId) >= 0)
                {
                    _result = true;
                }
            }
            return _result;
        }

        /// <summary>
        /// IJ,IC,MM,MC,MB,MD ��Ҫ����+-��
        /// </summary>
        /// <param name="bilId">���ݱ�</param>
        /// <returns>���ݵĽ�������1=����2=��</returns>
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
