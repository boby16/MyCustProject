using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// Summary description for DbCardMember.
    /// </summary>
    public class DbCardMember : DbObject
    {
        /// <summary>
        /// 会员卡
        /// </summary>
        /// <param name="connectionString">SQL连接字串</param>
        public DbCardMember(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// 取得会员卡资料
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="onlyFillSchema">是否只读取Schema</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string cardNo, bool onlyFillSchema)
        {
            string _sql = "select A.CARD_NO,A.NAME,A.TEL,A.TEL1,A.ADR,A.E_MAIL,A.BTH_DAY,A.BN_DD,A.EN_DD,A.USER_ID,A.DEP,B.NAME as DEP_NAME"
                + ",A.GX_NO,C.NAME as GX_NAME,A.ZIP,A.SEX_ID,A.USR,A.CELL_NO,D.CUS_NO,A.CARD_CLS,E.NAME AS CLS_NAME,A.REM,A.SYSED_ID,A.OLD_NO"
                + ",A.WANGWANG,A.QQ,A.MSN,A.SHOW_ID,A.COUN_ID,A.JOB_REM,F.CITY_ID,G.PROV_ID"
                + " from POSCARD A"
                + " left join DEPT B on B.DEP=A.DEP"
                + " left join CUST C on C.CUS_NO=A.GX_NO"
                + " left join CUST D on D.CARD_NO=A.CARD_NO"
                + " left join POSCARDTP E on E.CARD_CLS=A.CARD_CLS"
                + " left join SUNSYSTEM..ADR_COUN F on F.COUN_ID=A.COUN_ID"
                + " left join SUNSYSTEM..ADR_CITY G on G.CITY_ID=F.CITY_ID"
                + " where A.CARD_NO=@CardNo";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@CardNo", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, null, _spc);
            _ds.Tables[0].TableName = "POSCARD";
            //设定PK，因为用了left join以后，PK会取不到
            DataColumn[] _dca = new DataColumn[1];
            _dca[0] = _ds.Tables["POSCARD"].Columns["CARD_NO"];
            _ds.Tables["POSCARD"].PrimaryKey = _dca;
            return _ds;
        }

        /// <summary>
        /// 取得卡号分销商
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string GetCardCust(string cardNo)
        {
            string _sql = "SELECT GX_NO FROM POSCARD WHERE CARD_NO = @CardNo";
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@CardNo", SqlDbType.VarChar, 20);
            _spc[0].Value = cardNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, null, _spc);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                return _ds.Tables[0].Rows[0]["GX_NO"].ToString();
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="isSchema"></param>
        /// <returns></returns>
        public SunlikeDataSet GetCard4End(string sqlWhere, bool isSchema)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            string _sql = "";
            _sql = "SELECT POS.CARD_NO , POS.NAME ,SA_DD.LAST_DD FROM "
                + " POSCARD POS"
                + " LEFT JOIN "
                + " ("
                + " SELECT A.CARD_NO,MAX(A.PS_DD) AS LAST_DD FROM "
                + "	("
                + "		SELECT CARD_NO,PS_DD FROM MF_PSS WHERE PS_ID = 'SA' AND ISNULL(CARD_NO,'') <> '' "
                + "		GROUP BY CARD_NO,PS_DD,CUS_NO"
                + "	) AS A"
                + " GROUP BY CARD_NO"
                + " ) AS SA_DD ON SA_DD.CARD_NO = POS.CARD_NO"
                + " WHERE ISNULL(SYSED_ID,'F') = 'F' AND (EN_DD > Getdate() or ISNULL(EN_DD,'') = '')"
                + " AND POS.CARD_NO NOT IN "
                + " ("
                + " SELECT CARD_NO FROM MF_PSS "
                + " WHERE PS_ID = 'SA' AND ISNULL(CARD_NO,'') <> '' "
                + sqlWhere
                + " GROUP BY CARD_NO,PS_DD,CUS_NO"
                + " )";
            if (isSchema)
            {
                this.FillDatasetSchema(_sql, _ds, null);
            }
            else
            {
                this.FillDataset(_sql, _ds, null);
            }
            return _ds;
        }
        /// <summary>
        /// 更新停用注记
        /// </summary>
        /// <param name="cardNo"></param>
        public void SetEndId(string cardNo)
        {
            if (!String.IsNullOrEmpty(cardNo))
            {
                string _sql = "UPDATE POSCARD SET SYSED_ID = 'T',EN_DD='" + DateTime.Now.ToString() + "' WHERE CARD_NO IN ('" + cardNo.Replace(",", "','") + "')"
                    + " AND ISNULL(SYSED_ID,'F') = 'F'";
                this.ExecuteNonQuery(_sql);

            }
        }


    }
}
