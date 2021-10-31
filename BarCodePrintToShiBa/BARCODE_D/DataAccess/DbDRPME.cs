using System;
using System.Collections.Generic;
using System.Text;
using Sunlike.Common.CommonVar;
using System.Data.SqlClient;
using System.Data;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// DbDRPME
    /// </summary>
    public class DbDRPME : DbObject
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connStr"></param>
        public DbDRPME(string connStr)
            : base(connStr)
        {
        }

        #region 取数据
        /// <summary>
        /// GetData
        /// </summary>
        /// <param name="meId">单据别</param>
        /// <param name="meNo">单据号码</param>
        /// <param name="onlyFillSchema">是否只取结构</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string meId, string meNo, bool onlyFillSchema)
        {
            string _sql = "SELECT * FROM MF_ME WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO;"
                + "SELECT * FROM TF_ME  WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO;"
                + "SELECT * FROM TF_ME1 WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO;";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@ME_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = meId;
            _aryPt[1] = new SqlParameter("@ME_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = meNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[] { "MF_ME", "TF_ME", "TF_ME_BILL" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[] { "MF_ME", "TF_ME", "TF_ME_BILL" }, _aryPt);
            }
            //表头和表身关联
            DataColumn[] _dcm = new DataColumn[2];
            _dcm[0] = _ds.Tables["MF_ME"].Columns["ME_ID"];
            _dcm[1] = _ds.Tables["MF_ME"].Columns["ME_NO"];
            DataColumn[] _dct = new DataColumn[2];
            _dct[0] = _ds.Tables["TF_ME"].Columns["ME_ID"];
            _dct[1] = _ds.Tables["TF_ME"].Columns["ME_NO"];
            DataColumn[] _dct1 = new DataColumn[2];
            _dct1[0] = _ds.Tables["TF_ME_BILL"].Columns["ME_ID"];
            _dct1[1] = _ds.Tables["TF_ME_BILL"].Columns["ME_NO"];
            _ds.Relations.Add("MF_METF_ME", _dcm, _dct);
            _ds.Relations.Add("MF_METF_ME1", _dcm, _dct1);

            DataColumn _dcMeItm = new DataColumn();
            _dcMeItm = _ds.Tables["TF_ME_BILL"].Columns["ME_ITM"];
            DataColumn _dcKeyItm = new DataColumn();
            _dcKeyItm = _ds.Tables["TF_ME"].Columns["KEY_ITM"];
            _ds.Relations.Add("TF_METF_ME1", _dcKeyItm, _dcMeItm);
            return _ds;
        }
        /// <summary>
        /// 取得表身
        /// </summary>
        /// <param name="meId"></param>
        /// <param name="meNo"></param>
        /// <param name="keyItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBodyData(string meId, string meNo, int keyItm)
        {
            string _sql = "SELECT * FROM TF_ME WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO AND KEY_ITM = @KEY_ITM";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@ME_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = meId;
            _aryPt[1] = new SqlParameter("@ME_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = meNo;
            _aryPt[2] = new SqlParameter("@KEY_ITM", SqlDbType.SmallInt);
            _aryPt[2].Value = keyItm;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_ME" }, _aryPt);
            return _ds;
        }
        /// <summary>
        /// 取得表头
        /// </summary>
        /// <param name="meNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetHeadData(string meNo)
        {
            string _sqlStr = "SELECT * FROM MF_ME WHERE 1 = 1 ";
            SqlParameter[] _sqlPara = new SqlParameter[1];
            if (!String.IsNullOrEmpty(meNo))
            {
                _sqlStr += " AND ME_NO = @meNo";
                _sqlPara[0] = new SqlParameter("@meNo", SqlDbType.VarChar, 20);
                _sqlPara[0].Value = meNo;
            }
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "MF_ME" }, _sqlPara);
            return _ds;
        }
        #endregion

        #region 更新审核人
        /// <summary>
        /// UpdateChkMan
        /// </summary>
        /// <param name="meId"></param>
        /// <param name="meNo"></param>
        /// <param name="chkMan"></param>
        /// <param name="clsDate"></param>
        /// <returns></returns>
        public bool UpdateChkMan(string meId, string meNo, string chkMan, DateTime clsDate)
        {
            string _sql = "UPDATE MF_ME SET CHK_MAN = @CHK_MAN, CLS_DATE = @CLS_DATE WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO";
            SqlParameter[] _aryPt = new SqlParameter[4];
            _aryPt[0] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            _aryPt[0].Value = chkMan;
            _aryPt[1] = new SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            _aryPt[1].Value = clsDate;
            _aryPt[2] = new SqlParameter("@ME_ID", SqlDbType.VarChar, 2);
            _aryPt[2].Value = meId;
            _aryPt[3] = new SqlParameter("@ME_NO", SqlDbType.VarChar, 20);
            _aryPt[3].Value = meNo;
            return this.ExecuteNonQuery(_sql, _aryPt) > 0;
        }
        #endregion

        #region 更新可用费用
        /// <summary>
        /// 更新可用费用
        /// </summary>
        /// <param name="idxNo">费用项目代号</param>
        /// <param name="cusNo">客户代号</param>
        /// <param name="amtn">增减费用额</param>
        public void UpdateAmtnMe(string idxNo, string cusNo, decimal amtn)
        {
            string _sql = "IF NOT EXISTS(SELECT 1 FROM ME_INDX WHERE CUS_NO = @CUS_NO AND IDX_NO = @IDX_NO) \n"
                + " INSERT INTO ME_INDX(IDX_NO, CUS_NO, AMTN) VALUES(@IDX_NO, @CUS_NO, @AMTN) \n"
                + "ELSE \n"
                + "	UPDATE ME_INDX SET AMTN = ISNULL(AMTN, 0.00) + @AMTN WHERE IDX_NO = @IDX_NO AND CUS_NO = @CUS_NO";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _aryPt[0].Value = cusNo;
            _aryPt[1] = new SqlParameter("@IDX_NO", SqlDbType.VarChar, 12);
            _aryPt[1].Value = idxNo;
            _aryPt[2] = new SqlParameter("@AMTN", SqlDbType.Decimal);
            _aryPt[2].Value = amtn;

            this.ExecuteNonQuery(_sql, _aryPt);
        }
        #endregion

        #region 取得可用资源
        /// <summary>
        /// 取得可用资源
        /// </summary>
        /// <param name="idxNo"></param>
        /// <param name="cusNo"></param>
        /// <returns></returns>
        public decimal GetAmtnMe(string idxNo, string cusNo)
        {
            string _sql = "SELECT AMTN FROM ME_INDX WHERE IDX_NO = @IDX_NO AND CUS_NO = @CUS_NO";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@CUS_NO", SqlDbType.VarChar, 12);
            _aryPt[0].Value = cusNo;
            _aryPt[1] = new SqlParameter("@IDX_NO", SqlDbType.VarChar, 12);
            _aryPt[1].Value = idxNo;
            DataSet _ds = new DataSet();
            this.FillDataset(_sql, _ds, new string[] { "ME_INDX" }, _aryPt);
            if (_ds.Tables[0].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(_ds.Tables[0].Rows[0]["AMTN"].ToString()))
                {
                    return Convert.ToDecimal(_ds.Tables[0].Rows[0]["AMTN"]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 取得客户可用资源
        /// </summary>
        /// <param name="cusNo">用户代号</param>
        /// <param name="usr">登录用户</param>
        /// <returns></returns>
        public SunlikeDataSet GetAmtnMeByCust(string usr, string cusNo)
        {
            string _sql = "SELECT * FROM ME_INDX WHERE IDX_NO IN (SELECT IDX1 FROM FN_GETINDX1('" + usr + "'))  AND CUS_NO = @cusNo";
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@cusNo", SqlDbType.VarChar, 12);
            _aryPt[0].Value = cusNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "ME_INDX" },_aryPt);
            return _ds;
        }
        #endregion

        #region 更新已交量
        /// <summary>
        /// 更新已交量
        /// </summary>
        /// <param name="meId"></param>
        /// <param name="meNo"></param>
        /// <param name="keyItm"></param>
        /// <param name="qtySo"></param>
        public void UpdateQtySo(string meId, string meNo, int keyItm, decimal qtySo)
        {
            string _sql = "UPDATE TF_ME SET QTY_SO = ISNULL(QTY_SO, 0) + @QTY_SO WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO AND KEY_ITM = @KEY_ITM";
            SqlParameter[] _aryPt = new SqlParameter[4];
            _aryPt[0] = new SqlParameter("@ME_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = meId;
            _aryPt[1] = new SqlParameter("@ME_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = meNo;
            _aryPt[2] = new SqlParameter("@KEY_ITM", SqlDbType.Int);
            _aryPt[2].Value = keyItm;
            _aryPt[3] = new SqlParameter("@QTY_SO", SqlDbType.Decimal);
            _aryPt[3].Value = qtySo;
            this.ExecuteNonQuery(_sql, _aryPt);
        }
        #endregion

        #region 审核营销费用申请明细资料
        #region	取费用申请单明细资料
        /// <summary>
        ///	取费用申请单明细资料
        /// </summary>
        /// <param name="ME_Id">单据别</param>
        /// <param name="ME_No">单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetTableEA(string ME_Id, string ME_No)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@ME_ID", SqlDbType.VarChar, 8);
            _spc[0].Value = ME_Id;
            _spc[1] = new SqlParameter("@ME_NO", SqlDbType.VarChar, 38);
            _spc[1].Value = ME_No;

            string _sql = " SELECT M.ME_NO, M.ME_DD, M.CUS_NO,C.NAME AS CUS_NAME,M.DEP,D.NAME AS DEP_NAME,M.REM,M.FILE_ID AS EA_FILE_ID FROM MF_ME M"
                    +" LEFT JOIN CUST C ON M.CUS_NO = C.CUS_NO"
                    +" LEFT JOIN DEPT D ON  M.DEP = D.DEP"
                    +" where M.ME_NO=@ME_NO AND M.ME_ID = @ME_ID"
                   
                    +" SELECT T.IDX_NO,I.NAME AS IDX1_NAME,T.PRD_NO,P.NAME AS PRD_NAME,T.PRD_MARK,T.ITM,T.QTY,T.UP,T.AMTN_NET,T.AMTN_USE,T.REM FROM TF_ME T"
                    +" INNER JOIN INDX1 I ON T.IDX_NO = I.IDX1"
                    +" LEFT  JOIN  PRDT P ON T.PRD_NO = P.PRD_NO"
                    +" where T.ME_NO=@ME_NO AND T.ME_ID =@ME_ID";

            this.FillDataset(_sql, _ds, new string[] { "MF_ME", "TF_ME"}, _spc);
            return _ds;
        }
        #endregion
        #region 审核修改费用申请数量
        /// <summary>
        /// 审核修改费用申请数量
        /// </summary>
        /// <param name="ME_Id"></param>
        /// <param name="ME_No"></param>
        /// <param name="Itm"></param>
        /// <param name="Qty"></param>
        /// <param name="AmtnUse"></param>
        public void UpdateEAQty(string ME_Id, string ME_No, int Itm, decimal Qty,decimal AmtnUse)
        {
            try
            {
                SqlParameter[] _pt = new SqlParameter[5];
                _pt[0] = new SqlParameter("@ME_ID", SqlDbType.VarChar, 8);
                _pt[0].Value = ME_Id;
                _pt[1] = new SqlParameter("@ME_NO", SqlDbType.VarChar, 38);
                _pt[1].Value = ME_No;
                _pt[2] = new SqlParameter("@ITM", SqlDbType.SmallInt);
                _pt[2].Value = Itm;
                _pt[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
                _pt[3].Precision = 28;
                _pt[3].Scale = 8;
                _pt[3].Value = Qty;
                _pt[4] = new SqlParameter("@AMTN_USE", SqlDbType.Decimal);
                _pt[4].Precision = 28;
                _pt[4].Scale = 8;
                _pt[4].Value = AmtnUse;
                this.ExecuteNonQuery("UPDATE TF_ME SET QTY=@QTY,AMTN_NET=@QTY * UP,AMTN_USE=@AMTN_USE WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO AND ITM=@ITM", _pt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 审核删除费用申请明细
        /// <summary>
        /// 审核删除费用申请明细
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ME_Id"></param>
        /// <param name="ME_No"></param>
        /// <param name="Itm"></param>
        public void DelEa(string tableName, string ME_Id, string ME_No, int Itm)
        {
            SqlParameter[] _pt = new SqlParameter[3];
            _pt[0] = new SqlParameter("@ME_ID", SqlDbType.VarChar, 8);
            _pt[0].Value = ME_Id;
            _pt[1] = new SqlParameter("@ME_NO", SqlDbType.VarChar, 38);
            _pt[1].Value = ME_No;
            _pt[2] = new SqlParameter("@ITM", SqlDbType.SmallInt);
            _pt[2].Value = Itm;
            string _sql = "";
            if (tableName == "TF_ME")
            {
                 _sql = "Delete FROM TF_ME1 WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO AND ME_ITM = (SELECT KEY_ITM FROM TF_ME WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO AND ITM=@ITM);"
                      +"Delete FROM TF_ME WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO AND ITM=@ITM";
            }
            else
            {
                _sql = "Delete FROM " + tableName + " WHERE ME_ID = @ME_ID AND ME_NO = @ME_NO AND ITM=@ITM";
            }
            this.ExecuteNonQuery(_sql, _pt);
        }
        #endregion
        #endregion
    }
}
