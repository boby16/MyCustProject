using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
    /// <summary>
    /// 暂估单
    /// </summary>
    public class DbDRPZG : DbObject
	{
		/// <summary>
		/// 进货单
		/// </summary>
		/// <param name="connectionString">SQL连接字串</param>
		public DbDRPZG(string connectionString)
            : base(connectionString)
		{
		}

        /// <summary>
        /// 取暂估回冲DataSet
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        public SunlikeDataSet GetZBData(string osId, string osNo)
        {
            string _sql = "SELECT * FROM MF_ZG WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO; \n"
                + "SELECT * FROM TF_ZG WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO;";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = osId;
            _aryPt[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = osNo;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "MF_ZG", "TF_ZG", "TF_ZG_B" }, _aryPt);
            _ds.Relations.Add(new DataRelation("MF_ZGTF_ZG", new DataColumn[] { _ds.Tables["MF_ZG"].Columns["ZG_ID"], _ds.Tables["MF_ZG"].Columns["ZG_NO"] },
                new DataColumn[] { _ds.Tables["TF_ZG"].Columns["ZG_ID"], _ds.Tables["TF_ZG"].Columns["ZG_NO"] }));

            return _ds;
        }

        /// <summary>
        /// 取DataSet
        /// </summary>
        /// <param name="zgId"></param>
        /// <param name="zgNo"></param>
        /// <param name="onlyFillSchema"></param>
        public SunlikeDataSet GetData(string zgId, string zgNo, bool onlyFillSchema)
        {
            string _sql = "SELECT * FROM MF_ZG WHERE ZG_ID = @ZG_ID AND ZG_NO = @ZG_NO; \n"
                + "SELECT TF_ZG.*, PRDT.SPC FROM TF_ZG JOIN PRDT ON TF_ZG.PRD_NO = PRDT.PRD_NO WHERE ZG_ID = @ZG_ID AND ZG_NO = @ZG_NO; \n"
                + "SELECT * FROM TF_ZG_B WHERE ZG_ID = @ZG_ID AND ZG_NO = @ZG_NO;";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@ZG_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = zgId;
            _aryPt[1] = new SqlParameter("@ZG_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = zgNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            if (onlyFillSchema)
            {
                this.FillDatasetSchema(_sql, _ds, new string[] { "MF_ZG", "TF_ZG", "TF_ZG_B" }, _aryPt);
            }
            else
            {
                this.FillDataset(_sql, _ds, new string[] { "MF_ZG", "TF_ZG", "TF_ZG_B" }, _aryPt);
            }

            _ds.Relations.Add(new DataRelation("MF_ZGTF_ZG", new DataColumn[] { _ds.Tables["MF_ZG"].Columns["ZG_ID"], _ds.Tables["MF_ZG"].Columns["ZG_NO"] },
                new DataColumn[] { _ds.Tables["TF_ZG"].Columns["ZG_ID"], _ds.Tables["TF_ZG"].Columns["ZG_NO"] }));

            _ds.Relations.Add(new DataRelation("TF_ZGTF_ZG_B", new DataColumn[] { _ds.Tables["TF_ZG"].Columns["ZG_ID"], _ds.Tables["TF_ZG"].Columns["ZG_NO"], _ds.Tables["TF_ZG"].Columns["PRE_ITM"] },
                new DataColumn[] { _ds.Tables["TF_ZG_B"].Columns["ZG_ID"], _ds.Tables["TF_ZG_B"].Columns["ZG_NO"], _ds.Tables["TF_ZG_B"].Columns["ZG_ITM"] }));

            return _ds;
        }

        /// <summary>
        /// 根据来源单取暂估回冲
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string osId, string osNo)
        {
            string _sql = "SELECT * FROM MF_ZG WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO; \n"
                + "SELECT TF_ZG.* FROM TF_ZG JOIN MF_ZG ON TF_ZG.ZG_ID = MF_ZG.ZG_ID AND TF_ZG.ZG_NO = MF_ZG.ZG_NO WHERE MF_ZG.OS_ID = @OS_ID AND MF_ZG.OS_NO = @OS_NO; \n"
                + "SELECT * FROM TF_ZG_B WHERE 1 <> 1;";

            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = osId;
            _aryPt[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = osNo;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "MF_ZG", "TF_ZG" }, _aryPt);

            _ds.Relations.Add(new DataRelation("MF_ZGTF_ZG", new DataColumn[] { _ds.Tables["MF_ZG"].Columns["ZG_ID"], _ds.Tables["MF_ZG"].Columns["ZG_NO"] },
                new DataColumn[] { _ds.Tables["TF_ZG"].Columns["ZG_ID"], _ds.Tables["TF_ZG"].Columns["ZG_NO"] }));

            return _ds;
        }

        /// <summary>
        /// 取表身
        /// </summary>
        /// <param name="zgId"></param>
        /// <param name="zgNo"></param>
        /// <param name="othItm"></param>
        /// <returns></returns>
        public SunlikeDataSet GetBody(string zgId, string zgNo, int othItm)
        {
            string _sql = "SELECT * FROM TF_ZG WHERE ZG_ID = @ZG_ID AND ZG_NO = @ZG_NO AND OTH_ITM = @OTH_ITM";

            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@ZG_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = zgId;
            _aryPt[1] = new SqlParameter("@ZG_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = zgNo;
            _aryPt[2] = new SqlParameter("@OTH_ITM", SqlDbType.Int);
            _aryPt[2].Value = othItm;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, new string[] { "TF_ZG" }, _aryPt);

            return _ds;
        }

        /// <summary>
        /// 更新HC_ID
        /// </summary>
        /// <param name="zgId"></param>
        /// <param name="zgNo"></param>
        /// <param name="hcId"></param>
        public void UpdateHcId(string zgId, string zgNo, string hcId)
        {
            string _sql = "UPDATE MF_ZG SET HC_ID = @HC_ID WHERE ZG_ID = @ZG_ID AND ZG_NO = @ZG_NO";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@ZG_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = zgId;
            _aryPt[1] = new SqlParameter("@ZG_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = zgNo;
            _aryPt[2] = new SqlParameter("@HC_ID", SqlDbType.VarChar, 1);
            _aryPt[2].Value = hcId;

            this.ExecuteNonQuery(_sql, _aryPt);
        }

        #region 修改审核人和审核日期

        /// <summary>
        /// 修改审核人和审核日期
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="ChkMan">审核人</param>
        /// <param name="ClsDate">审核日期</param>
        /// <param name="vohNo"></param>
        public void UpdateChkMan(string PsID, string PsNo, string ChkMan, DateTime ClsDate, string vohNo)
        {
            string _sql = "UPDATE MF_ZG SET CHK_MAN = @CHK_MAN,CLS_DATE = @CLS_DATE,VOH_NO = @VOH_NO WHERE ZG_ID = @ZG_ID AND ZG_NO = @ZG_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[5];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
            if (String.IsNullOrEmpty(ChkMan))
            {
                _spc[0].Value = System.DBNull.Value;
            }
            else
            {
                _spc[0].Value = ChkMan;
            }
            _spc[1] = new System.Data.SqlClient.SqlParameter("@CLS_DATE", SqlDbType.DateTime);
            if (String.IsNullOrEmpty(ChkMan))
            {
                _spc[1].Value = System.DBNull.Value;
            }
            else
            {
                _spc[1].Value = ClsDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            _spc[2] = new System.Data.SqlClient.SqlParameter("@ZG_ID", SqlDbType.Char, 2);
            _spc[2].Value = PsID;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@ZG_NO", SqlDbType.VarChar, 20);
            _spc[3].Value = PsNo;
            _spc[4] = new System.Data.SqlClient.SqlParameter("@VOH_NO", SqlDbType.VarChar, 20);
            _spc[4].Value = vohNo;
            this.ExecuteNonQuery(_sql, _spc);
        }

        #endregion

        #region 修改已退数量，返回修改的记录数
        /// <summary>
        /// 修改已退数量，返回修改的记录数
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="PreItm">追踪退回数量项次</param>
        /// <param name="Qty">退回数量</param>
        /// <returns></returns>
        public string UpdateQtyRtn(string PsID, string PsNo, int PreItm, decimal Qty)
        {
            string _sql = "";
            if (PsID == "ZG")
            {
                _sql = "		update TF_ZG set QTY_RTN=isNull(QTY_RTN,0)+@QTY where ZG_ID=@PsID and ZG_NO=@PsNo and OTH_ITM=@PreItm \n"
                    + "		if Exists(select ZG_NO from TF_ZG WHERE ZG_ID=@PsID and ZG_NO=@PsNo and (isnull(QTY,0) > isnull(QTY_RTN,0) + isnull(QTY_PC,0))) \n"
                    + "			update MF_ZG set CLS_ID='F' where ZG_ID=@PsID and ZG_NO=@PsNo \n"
                    + "		else \n"
                    + "			update MF_ZG set CLS_ID='T' where ZG_ID=@PsID and ZG_NO=@PsNo \n"
                    + "	select 0\n";
            }
            else if (PsID == "PO")
            {
                _sql = "		update TF_POS set QTY_PS=isNull(QTY_PS,0)+@QTY where OS_ID=@PsID and OS_NO=@PsNo and PRE_ITM=@PreItm \n"
                    + "		if Exists(select OS_NO from TF_POS WHERE OS_ID=@PsID and OS_NO=@PsNo and ( (isnull((QTY),0)-isnull((QTY_PRE),0)) > isnull((QTY_PS),0) )) \n"
                    + "			update MF_POS set CLS_ID='F',BACK_ID=NULL where OS_ID=@PsID and OS_NO=@PsNo AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"//手工结案的单据不能更改
                    + "		else \n"
                    + "			update MF_POS set CLS_ID='T',BACK_ID='ZG' where OS_ID=@PsID and OS_NO=@PsNo AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T')\n"//手工结案的单据不能更改
                    + "	select 0\n";
            }
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@Qty", SqlDbType.Decimal);
            _spc[0].Value = Qty;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[1].Value = PsID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[2].Value = PsNo;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@PreItm", SqlDbType.SmallInt);
            _spc[3].Value = PreItm;
            string _result = "2";
            //}
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }
        /// <summary>
        /// 修改已转数量
        /// </summary>
        /// <param name="PsID">单据代号</param>
        /// <param name="PsNo">单据号码</param>
        /// <param name="PreItm">追踪退回数量项次</param>
        /// <param name="Qty">退回数量</param>
        /// <returns></returns>
        public string UpdateQtyPc(string PsID, string PsNo, int PreItm, decimal Qty)
        {
            string _sql = "update TF_ZG set QTY_PC=isNull(QTY_PC,0)+@QTY where ZG_ID=@PsID and ZG_NO=@PsNo and OTH_ITM=@PreItm \n"
                    + "		if Exists(select ZG_NO from TF_ZG WHERE ZG_ID=@PsID and ZG_NO=@PsNo and (isnull(QTY,0.00) > isnull(QTY_RTN,0.00) + isnull(QTY_PC,0))) \n"
                    + "			update MF_ZG set CLS_ID='F' where ZG_ID=@PsID and ZG_NO=@PsNo \n"
                    + "		else \n"
                    + "			update MF_ZG set CLS_ID='T' where ZG_ID=@PsID and ZG_NO=@PsNo \n"
                    + "	select 0\n";

            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@Qty", SqlDbType.Decimal);
            _spc[0].Value = Qty;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@PsID", SqlDbType.Char, 2);
            _spc[1].Value = PsID;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@PsNo", SqlDbType.VarChar, 20);
            _spc[2].Value = PsNo;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@PreItm", SqlDbType.SmallInt);
            _spc[3].Value = PreItm;
            string _result = "2";
            //}
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }
        #endregion

        /// <summary>
        /// 更新暂估单凭证号码
        /// </summary>
        /// <param name="zgId"></param>
        /// <param name="zgNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string zgId, string zgNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_ZG SET VOH_NO=@VOH_NO WHERE ZG_ID=@ZG_ID AND ZG_NO=@ZG_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@ZG_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = zgId;

            _sqlPara[1] = new SqlParameter("@ZG_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = zgNo;

            _sqlPara[2] = new SqlParameter("@VOH_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = vohNo;
            try
            {
                this.ExecuteNonQuery(_sqlStr, _sqlPara);
                _result = true;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
            return _result;

        }
    }
}
