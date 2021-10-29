using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
namespace Sunlike.Business.Data
{
	/// <summary>
	/// 费用主库.
	/// </summary>
	public class DbMonEX: Sunlike.Business.Data.DbObject
	{
		#region 构造函数
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connStr"></param>
		public DbMonEX(string connStr):base(connStr)
		{
		}
		#endregion

		#region 取得费用数据
		/// <summary>
		/// 取得费用数据
		/// </summary>
		/// <param name="epId">费用类型</param>
		/// <param name="epNo">费用单号</param>
		/// <param name="isSchema">是否只取单据结构</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string epId, string epNo,bool isSchema)
		{
            string _rpId = "1";
            string _chkId = "0";
			string _sqlWhere = "";
			if (!isSchema)
			{
                //if (!String.IsNullOrEmpty(epId))
                //    _sqlWhere += " AND A.EP_ID=@EP_ID ";
                //if (!String.IsNullOrEmpty(epNo))
                //    _sqlWhere += " AND A.EP_NO=@EP_NO ";
                if (!String.IsNullOrEmpty(epId))
                {
                    _sqlWhere += " AND A.EP_ID=@EP_ID ";                    
                }
                _sqlWhere += " AND A.EP_NO=@EP_NO ";//当为string.Empty的时候不需要查出所有。LWH:090925 bugNO:87903
			}
			else
			{
				_sqlWhere += " AND 1<> 1 ";
			}
            if (String.Compare("EP", epId) == 0)
            {
                _rpId = "2";
                _chkId = "1";
            }
            string _sqlStr = " SELECT A.EP_DD, A.EP_NO, A.REM, A.USR, A.PRT_SW, A.EP_ID, A.USR_NO, A.OPN_ID, A.SYS_DATE,A.BIL_TYPE,A.DEP,A.CHK_MAN,LOCK_MAN,"
                            + " A.PC_NO,A.CLS_DATE,A.BIL_ID,A.VOH_ID,A.VOH_NO,A.MOB_ID,A.CAS_NO,A.TASK_ID FROM MF_EXP A WHERE 1 = 1 " + _sqlWhere
							+ ";"
                            + " SELECT A.ITM, A.IDX_NO, A.CUS_NO, A.CUR_ID, A.EXC_RTO, A.TAX_ID, A.AMT, A.AMTN_NET, A.TAX, A.ARP_NO, A.ACC_NO, A.DEP, A.INV_NO, A.BAT_NO, A.REM, A.PAY_REM,B.RP_ID,A.RP_NO, A.SAL_NO, A.PAY_DD, "
                            + " A.EP_NO, A.EP_ID, A.SHARE_MTH, A.CHK_DD, A.CHK_DAYS, A.CLOSE_FT, A.AMTN_FT_TOT, A.PAY_MTH, A.PAY_DAYS, A.CLS_REM, A.INT_DAYS,A.AMT_RP, A.METH_ID, A.COMPOSE_IDNO, A.INV_DD, A.INV_YM, "
                            + " A.TITLE_BUY, A.AMT_INV, A.TAX_INV,A.RTO_TAX,A.BIL_ID,A.BIL_NO,A.KEY_ITM, "
                            + " B.AMT_BB,B.AMTN_BB,B.AMT_BC,B.AMTN_BC,B.CACC_NO,B.BACC_NO,B.AMT_CHK AMT_CHK,B.AMTN_CHK AMTN_CHK,B.AMT_OTHER,B.AMTN_OTHER, "
                            + " E.CHK_NO,E.CHK_KND,E.BANK_NO,E.BACC_NO BACC_NO_CHK,E.CRECARD_NO,E.END_DD,A.AMTN_NET_FP,A.AMT_FP,A.TAX_FP,A.LZ_CLS_ID,A.CLSLZ "
                            + " FROM TF_EXP A "						
                            + " LEFT JOIN TF_MON B on B.RP_NO=A.RP_NO and B.RP_ID='" + _rpId + "' and B.ITM=1"
                            + " LEFT JOIN TF_MON1 C on C.RP_NO=A.RP_NO and C.RP_ID='" + _rpId + "' and C.ITM=1"
                            + " LEFT JOIN TF_MON4 D on D.RP_NO=A.RP_NO and D.RP_ID='" + _rpId + "' and D.ITM=1"
                            + " LEFT JOIN MF_CHK E on E.CHK_NO=D.CHK_NO and E.CHK_ID='" + _chkId + "'"
                            + " WHERE 1=1 " + _sqlWhere	
                            + " ORDER BY A.ITM ";
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _sqlPara = new SqlParameter[2];
			_sqlPara[0] = new SqlParameter("@EP_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = epId;
			_sqlPara[1] = new SqlParameter("@EP_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = epNo;
			this.FillDataset(_sqlStr,_ds,new string[] {"MF_EXP","TF_EXP"},_sqlPara);
            DataColumn[] _dcPrimary = null;
            //表头
            _dcPrimary = new DataColumn[2];
            _dcPrimary[0] = _ds.Tables["MF_EXP"].Columns["EP_ID"];
            _dcPrimary[1] = _ds.Tables["MF_EXP"].Columns["EP_NO"];
            _ds.Tables["MF_EXP"].PrimaryKey = _dcPrimary;
            //表身
            _dcPrimary = new DataColumn[3];
            _dcPrimary[0] = _ds.Tables["TF_EXP"].Columns["EP_ID"];
            _dcPrimary[1] = _ds.Tables["TF_EXP"].Columns["EP_NO"];
            _dcPrimary[2] = _ds.Tables["TF_EXP"].Columns["ITM"];
            _ds.Tables["TF_EXP"].PrimaryKey = _dcPrimary;
			this.CreateRelation(_ds,"MF_EXP");
            //将KEY_ITM设为自动递增
          //  if (!string.IsNullOrEmpty(epNo))
            //{
                _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrement = true;
                _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementSeed = _ds.Tables["TF_EXP"].Rows.Count > 0 ? Convert.ToInt32(_ds.Tables["TF_EXP"].Select("", "KEY_ITM desc")[0]["KEY_ITM"]) + 1 : 1;
                _ds.Tables["TF_EXP"].Columns["KEY_ITM"].AutoIncrementStep = 1;
                _ds.Tables["TF_EXP"].Columns["KEY_ITM"].Unique = true;
            //}
			return _ds;
		}
		/// <summary>
		/// 根据来源单信息取资料
		/// </summary>
		/// <param name="bilId"></param>
		/// <param name="bilNo"></param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string bilId, string bilNo)
		{
			SqlParameter[] _aryPt = new SqlParameter[2];
			_aryPt[0] = new SqlParameter("@BIL_ID", SqlDbType.VarChar, 2);
			_aryPt[0].Value = bilId;
			_aryPt[1] = new SqlParameter("@BIL_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = bilNo;
			string _sql = "SELECT M.* FROM"
                + " MF_EXP AS M WHERE BIL_ID = @BIL_ID  AND PC_NO = @BIL_NO;"
                //+ " JOIN"
                //+ " TF_EXP AS T"
                //+ " ON M.EP_ID = T.EP_ID"
                //+ " AND M.EP_NO = T.EP_NO"
                //+ " AND T.BIL_ID = @BIL_ID"
                //+ " AND T.BIL_NO = @BIL_NO;"
				+ " SELECT * FROM TF_EXP WHERE BIL_ID = @BIL_ID AND BIL_NO = @BIL_NO ORDER BY ITM";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql, _ds, new string[2]{"MF_EXP", "TF_EXP"}, _aryPt);
			//表头和表身关联
			DataColumn[] _dca1 = new DataColumn[2];
			_dca1[0] = _ds.Tables["MF_EXP"].Columns["EP_ID"];
			_dca1[1] = _ds.Tables["MF_EXP"].Columns["EP_NO"];
			DataColumn[] _dca2 = new DataColumn[2];
			_dca2[0] = _ds.Tables["TF_EXP"].Columns["EP_ID"];
			_dca2[1] = _ds.Tables["TF_EXP"].Columns["EP_NO"];
			_ds.Relations.Add("MF_EXPTF_EXP",_dca1,_dca2);
			return _ds;
		}
		#endregion


		/// <summary>
        /// 更新审核信息
		/// </summary>
		/// <param name="epId"></param>
		/// <param name="epNo"></param>
		/// <param name="chkMan"></param>
		/// <param name="chkDd"></param>
		/// <returns></returns>
		public void UpdateCheck(string epId, string epNo, string chkMan, DateTime chkDd)
		{
            string _sqlStr = "UPDATE MF_EXP SET CHK_MAN=@CHK_MAN,CLS_DATE=@CLS_DD WHERE EP_ID=@EP_ID AND EP_NO=@EP_NO";
			SqlParameter[] _sqlPara = new SqlParameter[4];
			_sqlPara[0] = new SqlParameter("@EP_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = epId;
			_sqlPara[1] = new SqlParameter("@EP_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = epNo;
			_sqlPara[2] = new SqlParameter("@CHK_MAN",SqlDbType.VarChar,12);
            if (String.IsNullOrEmpty(chkMan))
            {
                _sqlPara[2].Value = System.DBNull.Value;
            }
            else
            {
                _sqlPara[2].Value = chkMan;
            }
			_sqlPara[3] = new SqlParameter("@CLS_DD",SqlDbType.DateTime);
            if (String.IsNullOrEmpty(chkMan))
            {
                _sqlPara[3].Value = System.DBNull.Value;
            }
            else
            {
                _sqlPara[3].Value = chkDd.ToString("yyyy-MM-dd HH:mm:ss");
            }

            this.ExecuteNonQuery(_sqlStr, _sqlPara);
		}


        /// <summary>
        ///  更新凭证号码
        /// </summary>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public void UpdateVohNo(string epId, string epNo, string vohNo)
        {
            string _sqlStr = "UPDATE MF_EXP SET VOH_NO=@VOH_NO WHERE EP_ID=@EP_ID AND EP_NO=@EP_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@EP_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = epId;
            _sqlPara[1] = new SqlParameter("@EP_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = epNo;
            _sqlPara[2] = new SqlParameter("@VOH_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = vohNo;

            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }

        /// <summary>
        /// 更新立账单号
        /// </summary>
        /// <param name="EpId"></param>
        /// <param name="EpNo"></param>
        /// <param name="ArpNo"></param>
        /// <param name="KeyItm"></param>
        public void UpdateArpNo(string EpId, string EpNo, string ArpNo, int KeyItm)
        {
            string _sql = "UPDATE TF_EXP SET ARP_NO=@ARP_NO WHERE EP_ID=@EP_ID AND EP_NO=@EP_NO AND KEY_ITM=@KEY_ITM";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@ARP_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = ArpNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@EP_ID", SqlDbType.Char, 2);
            _spc[1].Value = EpId;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@EP_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = EpNo;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@KEY_ITM", SqlDbType.Int);
            _spc[3].Value = KeyItm;

            this.ExecuteNonQuery(_sql, _spc);
        }

        /// <summary>
        /// 更新收款单号
        /// </summary>
        /// <param name="EpId"></param>
        /// <param name="EpNo"></param>
        /// <param name="RpNo"></param>
        /// <param name="KeyItm"></param>
        public void UpdateRpNo(string EpId, string EpNo, string RpNo, int KeyItm)
        {
            string _sql = "UPDATE TF_EXP SET RP_NO=@RP_NO WHERE EP_ID=@EP_ID AND EP_NO=@EP_NO AND KEY_ITM=@KEY_ITM";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@RP_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = RpNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@EP_ID", SqlDbType.Char, 2);
            _spc[1].Value = EpId;
            _spc[2] = new System.Data.SqlClient.SqlParameter("@EP_NO", SqlDbType.VarChar, 20);
            _spc[2].Value = EpNo;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@KEY_ITM", SqlDbType.Int);
            _spc[3].Value = KeyItm;

            this.ExecuteNonQuery(_sql, _spc);
        }

        /// <summary>
        ///补开发票回写来源单表身栏位
        /// </summary>
        /// <param name="amtFp">已开金额</param>
        /// <param name="amtn_netFp">已开未税金额</param>
        /// <param name="taxFp">已开税额</param>
        /// <param name="lzClsId">補開結案</param>
        /// <param name="clsLz">結案否</param>
        /// <param name="invNo">發票號</param>
        /// <param name="epId"></param>
        /// <param name="epNo"></param>
        /// <param name="itm">表身项次</param>
        public void UpdateInvIkBodyData(decimal amtFp, decimal amtn_netFp, decimal taxFp, string lzClsId, string clsLz,string invNo, string epId, string epNo, int itm)
        {
            string _sql = "update TF_EXP set AMT_FP=@AMT_FP,AMTN_NET_FP=@AMTN_NET_FP,TAX_FP=@TAX_FP,LZ_CLS_ID=@LZ_CLS_ID,CLSLZ=@CLSLZ,INV_NO=@INV_NO Where EP_ID=@EP_ID and  EP_NO=@EP_NO and ITM=@ITM  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[9];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_FP", SqlDbType.Decimal);
            _spc[0].Value = amtFp;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP", SqlDbType.Decimal);
            _spc[1].Value = amtn_netFp;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@TAX_FP", SqlDbType.Decimal);
            _spc[2].Value = taxFp;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[3].Value = lzClsId;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@CLSLZ", SqlDbType.VarChar, 1);
            _spc[4].Value = clsLz;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@INV_NO", SqlDbType.VarChar, 14);
            _spc[5].Value = invNo;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@EP_ID", SqlDbType.VarChar, 2);
            _spc[6].Value = epId;

            _spc[7] = new System.Data.SqlClient.SqlParameter("@EP_NO", SqlDbType.VarChar, 20);
            _spc[7].Value = epNo;

            _spc[8] = new System.Data.SqlClient.SqlParameter("@ITM", SqlDbType.Int);
            _spc[8].Value = itm;


           
            this.ExecuteNonQuery(_sql, _spc);

        }

    }
}