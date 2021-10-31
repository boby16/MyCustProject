using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;
using System.Collections;

namespace Sunlike.Business.Data
{
    public class DbPOSCustCType : DbObject
    {
        public DbPOSCustCType(string connStr)
            : base(connStr)
        {
        }

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="cTypeNo"></param>
        /// <param name="isSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string sqlWhere)
        {
            string _sqlStr = "SELECT CTYPE_NO,NAME,VALID_DAY,CARD_CHK,SAVING_CHK,PRDT_CHK,UP,SYS_DATE,USR,DEP,DSC_NO,BEGIN_DD,END_DD,COUNT_TYPE,NUM_SET"
                + " FROM CTYPE";
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                _sqlStr += " WHERE " + sqlWhere;
            }

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "CTYPE" });
            return _ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataDsc(string sqlWhere)
        {
            string _sqlStr = "SELECT DSC_NO,NAME "
                + " FROM CTYPE_DSC";
            if (!string.IsNullOrEmpty(sqlWhere))
            {
                _sqlStr += " WHERE " + sqlWhere;
            }

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "CTYPE_DSC" });
            return _ds;
        }
        /// <summary>
        /// 客卡-服务折扣
        /// </summary>
        /// <param name="alCTypeNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetCType_Dis(ArrayList alCTypeNo)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT CTYPE_NO,IDX_NO,DIS_CNT FROM CTYPE_DIS WHERE ");
            if (alCTypeNo.Count > 0)
            {
                _sb.Append("CTYPE_NO IN (");
                for (int i = 0; i < alCTypeNo.Count; i++)
                {
                    if (i > 0)
                    {
                        _sb.Append(",");
                    }
                    _sb.Append("'" + alCTypeNo[i].ToString() + "'");
                }
                _sb.Append(")");
            }
            else
            {
                _sb.Append("1<>1");
            }
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sb.ToString(), _ds, new string[] { "CTYPE_DIS" });
            return _ds;
        }

        /// <summary>
        /// 客卡-服务可用次数
        /// </summary>
        /// <param name="alCTypeNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetCType_Num(ArrayList alCTypeNo)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT CTYPE_NUM.CTYPE_NO,CTYPE_NUM.PRD_NO,CTYPE_NUM.NUM_SET,CTYPE_NUM.DAY_ZQ,CTYPE_NUM.UP_FREEZE,CTYPE_NUM.USE_TIME,PRDT.UPR,CTYPE_NUM.GROUP_CHK,CTYPE_NUM.GROUP_PRDT,CTYPE_NUM.PRD_NAME FROM CTYPE_NUM LEFT JOIN PRDT ON CTYPE_NUM.PRD_NO=PRDT.PRD_NO WHERE ");
            if (alCTypeNo.Count > 0)
            {
                _sb.Append("CTYPE_NO IN (");
                for (int i = 0; i < alCTypeNo.Count; i++)
                {
                    if (i > 0)
                    {
                        _sb.Append(",");
                    }
                    _sb.Append("'" + alCTypeNo[i].ToString() + "'");
                }
                _sb.Append(")");
            }
            else
            {
                _sb.Append("1<>1");
            }
            //_sb.Append(" AND ISNULL(GROUP_PRDT,'')=''");
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sb.ToString(), _ds, new string[] { "CTYPE_NUM" });
            return _ds;
        }

        public SunlikeDataSet GetCType_NumGroup(ArrayList alCTypeNo)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT CTYPE_NUM.CTYPE_NO,CTYPE_NUM.PRD_NO,CTYPE_NUM.NUM_SET,CTYPE_NUM.DAY_ZQ,CTYPE_NUM.UP_FREEZE,CTYPE_NUM.USE_TIME,PRDT.UPR,CTYPE_NUM.GROUP_CHK,CTYPE_NUM.GROUP_PRDT FROM CTYPE_NUM LEFT JOIN PRDT ON CTYPE_NUM.PRD_NO=PRDT.PRD_NO WHERE ");
            if (alCTypeNo.Count > 0)
            {
                _sb.Append("CTYPE_NO IN (");
                for (int i = 0; i < alCTypeNo.Count; i++)
                {
                    if (i > 0)
                    {
                        _sb.Append(",");
                    }
                    _sb.Append("'" + alCTypeNo[i].ToString() + "'");
                }
                _sb.Append(")");
            }
            else
            {
                _sb.Append("1<>1");
            }
            _sb.Append(" AND ISNULL(GROUP_PRDT,'')<>''");
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sb.ToString(), _ds, new string[] { "CTYPE_NUM_GROUP" });
            return _ds;
        }

        /// <summary>
        /// 客卡-现金折扣例外
        /// </summary>
        /// <param name="alCTypeNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetCType_Ly(ArrayList alCTypeNo)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT CTYPE_NO,IDX_NO,DIS_CNT FROM CTYPE_LY WHERE ");
            if (alCTypeNo.Count > 0)
            {
                _sb.Append("CTYPE_NO IN (");
                for (int i = 0; i < alCTypeNo.Count; i++)
                {
                    if (i > 0)
                    {
                        _sb.Append(",");
                    }
                    _sb.Append("'" + alCTypeNo[i].ToString() + "'");
                }
                _sb.Append(")");
            }
            else
            {
                _sb.Append("1<>1");
            }
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sb.ToString(), _ds, new string[] { "CTYPE_LY" });
            return _ds;
        }
    }
}
