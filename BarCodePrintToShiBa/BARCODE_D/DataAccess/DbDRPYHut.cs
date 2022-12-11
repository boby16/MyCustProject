using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;


namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPYHut.
	/// </summary>
	public class DbDRPYHut :Sunlike.Business.Data.DbObject
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectinString"></param>
		public DbDRPYHut(string connectinString) : base(connectinString)
		{}

		#region GetData
		/// <summary>
		/// ȡ��Ҫ����Ϣ
		/// </summary>
		/// <param name="yhId">���ݱ�</param>
		/// <param name="yhNo">Ҫ������</param>
		/// <returns>DataSet</returns>
		public SunlikeDataSet GetData(string yhId, string yhNo)
		{
			SqlParameter[] _aryPt = new SqlParameter[2];
			_aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 20);
			_aryPt[0].Value = yhId;
			_aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = yhNo;
			string _strSql;
            _strSql = "SELECT YH_ID, YH_NO, DEP, YH_DD, CLS_ID, CLS_DATE, CHK_MAN, CUS_NO, REM, USR, FX_WH, WH, BYBOX, EST_DD, FUZZY_ID, ISNULL(SAVE_ID, 'T') AS SAVE_ID,SYS_DATE,BIL_TYPE,MOB_ID"
				+ " FROM MF_DYH WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO;"
                + " SELECT YH_ID, YH_NO, ITM, PRD_NO, PRD_MARK, WH, EST_DD, QTY, UNIT, AMTN, UP, REM, BOX_ITM, KEY_ITM, QTY_OLD, WH_OLD, EST_OLD, DEL_ID, QTY_RTN,QTY_SO,QTY_SO_UNSH,QTY_RTN_UNSH"
				+ " FROM TF_DYH WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO;"
				+ "	SELECT YH_ID, YH_NO, ITM, PRD_NO, CONTENT, QTY, KEY_ITM, WH, QTY_OLD, WH_OLD, EST_OLD, EST_DD, DEL_ID "
				+ " FROM TF_DYH1 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO;";
			string[] _aryTableName = new string[] {"MF_DYH","TF_DYH","TF_DYH1"};
			SunlikeDataSet _ds = new SunlikeDataSet("DS_YH");
			if (!String.IsNullOrEmpty(yhNo))
			{
				this.FillDataset(_strSql, _ds, _aryTableName, _aryPt);
			}
			else
			{
				this.FillDatasetSchema(_strSql, _ds, _aryTableName, _aryPt);
			}
			//��ͷ�Ͳ�Ʒ���ݡ���ͷ�������ݵĹ���
			DataColumn[] _aryDcm = new DataColumn[2];
			_aryDcm[0] = _ds.Tables["MF_DYH"].Columns["YH_ID"];
			_aryDcm[1] = _ds.Tables["MF_DYH"].Columns["YH_NO"];
			DataColumn[] _aryDct = new DataColumn[2];
			_aryDct[0] = _ds.Tables["TF_DYH"].Columns["YH_ID"];
			_aryDct[1] = _ds.Tables["TF_DYH"].Columns["YH_NO"];
			DataColumn[] _aryDct1 = new DataColumn[2];
			_aryDct1[0] = _ds.Tables["TF_DYH1"].Columns["YH_ID"];
			_aryDct1[1] = _ds.Tables["TF_DYH1"].Columns["YH_NO"];
			_ds.Relations.Add("MF_DYHTF_DYH", _aryDcm, _aryDct);
			_ds.Relations.Add("MF_DYHTF_DYH1", _aryDcm, _aryDct1);

			//�����ݺͲ�Ʒ���ݵĹ���
			DataColumn _dcKeyItm = new DataColumn();
			_dcKeyItm = _ds.Tables["TF_DYH1"].Columns["KEY_ITM"];
			DataColumn _dcBoxItm = new DataColumn();
			_dcBoxItm = _ds.Tables["TF_DYH"].Columns["BOX_ITM"];
			_ds.Relations.Add("TF_DYHTF_DYH1", _dcKeyItm, _dcBoxItm);

			_ds.Tables["MF_DYH"].Columns["SAVE_ID"].ReadOnly = false;
			return _ds;
		}
		/// <summary>
		/// ����˻ص��Ƿ��������˻�����
		/// </summary>
		/// <param name="yhNo">�˻ص���</param>
		/// <returns></returns>
		public bool HasIoData(string yhNo)
		{
			string _sql = "SELECT TOP 1 1 FROM TF_IC WHERE BIL_ID = 'YI' AND BIL_NO = @YH_NO";
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 12);
			_aryPt[0].Value = yhNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null,_aryPt);
			if (_ds.Tables[0].Rows.Count > 0)
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="YhId"></param>
		/// <param name="YhNo"></param>
		/// <returns></returns>
		public string getCusNo(string YhId,string YhNo)
		{
			string _sql = "SELECT CUS_NO FROM MF_DYH WHERE YH_ID = @YhId AND YH_NO = @YhNo";
			SqlParameter[] _sp = new SqlParameter[2];
			_sp[0] = new SqlParameter("@YhId",SqlDbType.VarChar,2);
			_sp[0].Value = YhId;
			_sp[1] = new SqlParameter("@YhNo",SqlDbType.VarChar,20);
			_sp[1].Value = YhNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null,_sp);
			if ( _ds.Tables[0].Rows.Count > 0)
			{
				return _ds.Tables[0].Rows[0]["CUS_NO"].ToString();
			}
			return null;
		}
		/// <summary>
		/// �ж��Ƿ���Ҫ�����
		/// </summary>
		/// <param name="_dt"></param>
		/// <param name="yhId"></param>
		/// <param name="yhNo"></param>
		/// <param name="prdNo"></param>
		/// <param name="content"></param>
		/// <param name="itm"></param>
		/// <param name="qty"></param>
		/// <returns></returns>
		public decimal ChkQtyByBox(DataTable _dt ,string yhId ,string yhNo, string prdNo, string content,string itm,decimal qty)
		{
			string _sql = "SELECT SUM(QTY) AS QTY FROM TF_DYH1 WHERE YH_NO = @yhNo AND YH_ID=@yhId AND PRD_NO=@prdNo AND CONTENT=@content";
			SqlParameter[] _sp = new SqlParameter[4];
			_sp[0] = new SqlParameter("@yhNo",SqlDbType.VarChar,12);
			_sp[0].Value = yhNo;
			_sp[1] = new SqlParameter("@yhId",SqlDbType.VarChar,2);
			_sp[1].Value = yhId;
			_sp[2] = new SqlParameter("@content",SqlDbType.VarChar,255);
			_sp[2].Value = content;
			_sp[3] = new SqlParameter("@prdNo",SqlDbType.VarChar,30);
			_sp[3].Value = prdNo;

            decimal _chkQty = 0;//ԭ����ȥ�ñ���������
            decimal _orderQty = 0;//ԭ������
            decimal _soQty = 0;//���ܶ���
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _sp))
            //{
            //    if (_sdr.Read())
            //    {
            //        if (_sdr["QTY"] != DBNull.Value)
            //        {
            //            _orderQty = Convert.ToDecimal(_sdr["QTY"]);
            //        }
            //    }
            //}
            object _rObj = this.ExecuteScalar(_sql, _sp);
            if (!String.IsNullOrEmpty(_rObj.ToString()))
            {
                _orderQty = Convert.ToDecimal(_rObj);
            }
			if (!String.IsNullOrEmpty(itm))//update
			{
				DataRow[] _drArr = _dt.Select("YH_NO='" + yhNo + "' AND PRD_NO ='"+ prdNo +"' AND CONTENT = '"+ content + "'");
				foreach (DataRow dr in _drArr)
				{
					if (dr["ITM"].ToString() != itm)
					{
						_chkQty += Convert.ToDecimal(dr["QTY"]);
					}
					else
					{
						_soQty += Convert.ToDecimal(dr["QTY_SO"]);
					}
				}
			}
			else
			{
				DataRow[] _drArr = _dt.Select("YH_NO='" + yhNo + "' AND PRD_NO ='"+ prdNo +"' AND CONTENT = '"+ content + "'");
				foreach (DataRow dr in _drArr)
				{
					_chkQty += Convert.ToDecimal(dr["QTY"]);
				}
			}
			if (_chkQty != 0 )
			{
				return (_chkQty + qty) - _orderQty - _soQty;;
			}
			else
			{
				return  qty - _soQty;;
			}
		}

        /// <summary>
        /// ���½᰸ע��
        /// </summary>
        /// <param name="yhId">YH_ID</param>
        /// <param name="yhNo">YH_NO</param>
        public void UpdateClsId(string yhId, string yhNo)
        {
            string _sql = "IF EXISTS(SELECT 1 FROM TF_DYH WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO \n"
                + "     AND ISNULL(QTY, 0) <= ISNULL(QTY_SO, 0) + ISNULL(QTY_SO_UNSH, 0)) \n"
                + " UPDATE MF_DYH SET CLS_ID = 'T' WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO \n"
                + "ELSE \n"
                + "	UPDATE MF_DYH SET CLS_ID = 'F' WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = yhId;
            _aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = yhNo;
            this.ExecuteNonQuery(_sql, _aryPt);
        }
        
		/// <summary>
		/// �ж��Ƿ���Ҫ�����
		/// </summary>
		/// <param name="_dt"></param>
		/// <param name="yhId"></param>
		/// <param name="yhNo"></param>
		/// <param name="prdNo"></param>
		/// <param name="prdMark"></param>
		/// <param name="itm"></param>
		/// <param name="qty"></param>
		/// <returns></returns>
		public decimal ChkQtyByPrdt(DataTable _dt ,string yhId ,string yhNo, string prdNo, string prdMark,string itm,decimal qty)
		{
			string _sqlSelect = "";
			decimal _chkQty = 0;//ԭ����ȥ�ñ���������
			decimal _orderQty = 0;//ԭ������
			decimal _soQty = 0;//���ܶ���
			if (yhId == "YI" && prdMark == null)//�������˻�
			{
				string _sql = "SELECT SUM(QTY) AS QTY FROM TF_DYH WHERE YH_NO = @yhNo AND YH_ID=@yhId AND PRD_NO=@prdNo";
				SqlParameter[] _sp = new SqlParameter[3];
				_sp[0] = new SqlParameter("@yhNo",SqlDbType.VarChar,20);
				_sp[0].Value = yhNo;
				_sp[1] = new SqlParameter("@yhId",SqlDbType.VarChar,2);
				_sp[1].Value = yhId;
				_sp[2] = new SqlParameter("@prdNo",SqlDbType.VarChar,30);
				_sp[2].Value = prdNo;
				_sqlSelect = "YH_NO='" + yhNo + "' AND PRD_NO ='"+ prdNo +"'";
                //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _sp))
                //{
                //    if (_sdr.Read())
                //    {
                //        if (_sdr["QTY"] != DBNull.Value)
                //        {
                //            _orderQty = Convert.ToDecimal(_sdr["QTY"]);
                //        }
                //    }
                //}
                object _rObj = this.ExecuteScalar(_sql, _sp);
                if (!String.IsNullOrEmpty(_rObj.ToString()))
                {
                    _orderQty = Convert.ToDecimal(_rObj);
                }
			}
			else
			{
				string _sql = "SELECT SUM(QTY) AS QTY FROM TF_DYH WHERE YH_NO = @yhNo AND YH_ID=@yhId AND PRD_NO=@prdNo AND PRD_MARK=@prdMark";
				SqlParameter[] _sp = new SqlParameter[4];
				_sp[0] = new SqlParameter("@yhNo",SqlDbType.VarChar,20);
				_sp[0].Value = yhNo;
				_sp[1] = new SqlParameter("@yhId",SqlDbType.VarChar,2);
				_sp[1].Value = yhId;
				_sp[2] = new SqlParameter("@prdMark",SqlDbType.VarChar,50);
				_sp[2].Value = prdMark;
				_sp[3] = new SqlParameter("@prdNo",SqlDbType.VarChar,30);
				_sp[3].Value = prdNo;
				_sqlSelect = "YH_NO='" + yhNo + "' AND PRD_NO ='"+ prdNo +"' AND PRD_MARK = '"+ prdMark + "'";
                //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _sp))
                //{
                //    if (_sdr.Read())
                //    {
                //        if (_sdr["QTY"] != DBNull.Value)
                //        {
                //            _orderQty = Convert.ToDecimal(_sdr["QTY"]);
                //        }
                //    }
                //}
                object _rObj = this.ExecuteScalar(_sql, _sp);
                if (!String.IsNullOrEmpty(_rObj.ToString()))
                {
                    _orderQty = Convert.ToDecimal(_rObj);
                }
			}
			if (!String.IsNullOrEmpty(itm))//update
			{
				DataRow[] _drArr = _dt.Select(_sqlSelect);
				foreach (DataRow dr in _dt.Rows)
				{
					if (dr.RowState != DataRowState.Deleted)
					{
						if (dr["ITM"].ToString() != itm)
						{
							_chkQty += Convert.ToDecimal(dr["QTY"]);
						}
						else
						{
							if (!String.IsNullOrEmpty(dr["QTY_SO"].ToString()))
							{
								_soQty += Convert.ToDecimal(dr["QTY_SO"]);
							}
						}
					}
				}
				
			}
			else
			{
				DataRow[] _drArr = _dt.Select( _sqlSelect );
				foreach (DataRow dr in _drArr)
				{
					_chkQty += Convert.ToDecimal(dr["QTY"]);
				}
			}
			if (_chkQty != 0 )
			{
				return (_chkQty + qty) - _orderQty - _soQty;
			}
			else
			{
				return  qty - _soQty;
			}
		}

        /// <summary>
        /// ���TF_DYHһ����¼
        /// </summary>
        /// <param name="yhId"></param>
        /// <param name="yhNo"></param>
        /// <param name="keyItm"></param>
        public DataRow  GetTFDYHITM(string yhId, string yhNo, int keyItm)
        {
            DataRow dr = null;

            SunlikeDataSet _ds = new SunlikeDataSet();
            string _sql = "SELECT YH_ID, YH_NO, ITM, PRD_NO, PRD_MARK, WH, EST_DD, QTY, UNIT, AMTN, QTY_RTN, UP, REM,"
                +" BOX_ITM, KEY_ITM, QTY_OLD, WH_OLD,EST_OLD, DEL_ID, QTY_SO, QTY_SO_UNSH, QTY_RTN_UNSH "
                +" FROM TF_DYH WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND KEY_ITM = @KEY_ITM";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = yhId;
            _aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = yhNo;
            _aryPt[2] = new SqlParameter("@KEY_ITM", SqlDbType.Int);
            _aryPt[2].Value = keyItm;
            this.FillDataset(_sql, _ds, new string[] { "TF_DYH" }, _aryPt);
            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                dr = _ds.Tables[0].Rows[0];  
            }
            return dr;
        }
		#endregion

		#region Auditing
		/// <summary>
		/// ���ͨ��
		/// </summary>
		/// <param name="yhId">���ݱ�</param>
		/// <param name="yhNo">���ݺ�</param>
		/// <param name="chk_man">�����</param>
		/// <param name="clsDd">�������</param>
		public void Approve(string yhId, string yhNo, string chk_man, DateTime clsDd)
		{
			SqlParameter[] _aryPt = new SqlParameter[4];
			_aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 20);
			_aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
			_aryPt[2] = new SqlParameter("@CHK_MAN", SqlDbType.VarChar, 12);
			_aryPt[3] = new SqlParameter("@CLS_DD", SqlDbType.DateTime);
			_aryPt[0].Value = yhId;
			_aryPt[1].Value = yhNo;
			_aryPt[2].Value = chk_man;
			_aryPt[3].Value = clsDd.ToString("yyyy-MM-dd HH:mm:ss");
			string _strSql = "UPDATE MF_DYH SET CHK_MAN = @CHK_MAN, CLS_DATE = @CLS_DD WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO";
			this.ExecuteNonQuery(_strSql, _aryPt);
		}
		/// <summary>
		/// �����
		/// </summary>
		/// <param name="yhId">���ݱ�</param>
		/// <param name="yhNo">���ݺ�</param>
		public void RollBack(string yhId, string yhNo)
		{
			SqlParameter[] _aryPt = new SqlParameter[2];
			_aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 20);
			_aryPt[0].Value = yhId;
			_aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = yhNo;
			string _strSql = "UPDATE MF_DYH SET CHK_MAN = null, CLS_DATE = null WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO;"
				+ "UPDATE TF_DYH SET QTY_SO = NULL WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO";
			this.ExecuteNonQuery(_strSql, _aryPt);
		}
		#endregion

		#region �޸��ܶ�����
		/// <summary>
		/// �޸��ܶ�������for Auditing��
		/// </summary>
		/// <param name="YhId">Ҫ�����ݱ�</param>
		/// <param name="YhNo">Ҫ���˻ص���</param>
		/// <param name="Itm">Ψһ���</param>
		/// <param name="Qty">��д����</param>
		public void UpdateQtySo(string YhId,string YhNo,int Itm,decimal Qty)
		{
            string _sqlStr = "	update TF_DYH set QTY_SO=ISNULL(QTY_SO, 0) + @Qty where YH_ID=@YhId and YH_NO=@YhNo and KEY_ITM=@KeyItm;\n"//��д���ܶ���
				+ "	if exists (select YH_NO from TF_DYH where YH_ID=@YhId and YH_NO=@YhNo and (isnull(QTY,0)-isnull(QTY_SO,0))>0  AND ISNULL(DEL_ID,'F')='F')\n"//�޸Ľ᰸��־
				+ "		update MF_DYH set CLS_ID='F' , BACK_ID=NULL where YH_ID=@YhId and YH_NO=@YhNo\n"
				+ "	else\n"
				+ "		update MF_DYH set CLS_ID='T' , BACK_ID='SO' where YH_ID=@YhId and YH_NO=@YhNo\n";
			SqlParameter [] _sqlSpara = new SqlParameter[4];
			_sqlSpara[0] = new SqlParameter("@YhId",SqlDbType.VarChar,2);
			_sqlSpara[0].Value = YhId;
			_sqlSpara[1] = new SqlParameter("@YhNo",SqlDbType.VarChar,20);
			_sqlSpara[1].Value = YhNo;
			_sqlSpara[2] = new SqlParameter("@KeyItm",SqlDbType.Int);
			_sqlSpara[2].Value = Itm;
			_sqlSpara[3] = new SqlParameter("@Qty",SqlDbType.VarChar,28);
			if (Qty == 0 )
			{
				_sqlSpara[3].Value = System.DBNull.Value;
			}
			else
			{
				_sqlSpara[3].Value = Qty.ToString();
			}

			this.ExecuteNonQuery(_sqlStr,_sqlSpara);

		}
		/// <summary>
		/// �޸��ܶ�����
		/// </summary>
		/// <param name="YhId">Ҫ�����ݱ�</param>
		/// <param name="YhNo">Ҫ���˻ص���</param>
		/// <param name="Itm">Ψһ���</param>
		/// <param name="Qty">��д����</param>
		/// <param name="QtyHis">ԭ����</param>
		/// <param name="State">״̬ 1��������2���޸ģ�3��ɾ��</param>
		/// <param name="isAuditing">�ܶ��Ƿ������</param>
		public void UpdateQtySo(string YhId,string YhNo,int Itm,decimal Qty,decimal QtyHis,int State,bool isAuditing)
		{
			string _sqlStr = "";
			if (!isAuditing)
			{
				_sqlStr += "update TF_DYH set QTY_SO=isnull(QTY_SO,0)+@Qty where YH_ID=@YhId and YH_NO=@YhNo and KEY_ITM=@KeyItm;\n";//��д���ܶ���(�ܶ������������)
			}
			_sqlStr	+= "if exists (select YH_NO from TF_DYH where YH_ID=@YhId and YH_NO=@YhNo and (isnull(QTY,0)-isnull(QTY_SO,0))>0 AND ISNULL(DEL_ID,'F')='F')\n"//�޸Ľ᰸��־
				+ "		update MF_DYH set CLS_ID='F' , BACK_ID=NULL where YH_ID=@YhId and YH_NO=@YhNo\n"
				+ "	else\n"
				+ "		update MF_DYH set CLS_ID='T' , BACK_ID='SO' where YH_ID=@YhId and YH_NO=@YhNo\n";
			SqlParameter [] _sqlSpara = new SqlParameter[4];
			_sqlSpara[0] = new SqlParameter("@YhId",SqlDbType.VarChar,2);
			_sqlSpara[0].Value = YhId;
			_sqlSpara[1] = new SqlParameter("@YhNo",SqlDbType.VarChar,20);
			_sqlSpara[1].Value = YhNo;
			_sqlSpara[2] = new SqlParameter("@KeyItm",SqlDbType.Int);
			_sqlSpara[2].Value = Itm;
			_sqlSpara[3] = new SqlParameter("@Qty",SqlDbType.Decimal);
			_sqlSpara[3].Precision = 28;
			_sqlSpara[3].Scale = 8;
			if(State == 3)
			{
				_sqlSpara[3].Value = (Qty - QtyHis)*-1;
			}
			else
			{
				_sqlSpara[3].Value = Qty - QtyHis;
			}

			this.ExecuteNonQuery(_sqlStr,_sqlSpara);
        }

        /// <summary>
        /// ����δ����ܶ���
        /// </summary>
        /// <param name="yhId">Ҫ�����ݱ�</param>
        /// <param name="yhNo">Ҫ���˻ص���</param>
        /// <param name="keyItm">Ψһ���</param>
        /// <param name="qty">��д����</param>
        public void UpdateQtySoUnSh(string yhId, string yhNo, int keyItm, decimal qty)
        {
            string _sql = "UPDATE TF_DYH SET QTY_SO_UNSH = ISNULL(QTY_SO_UNSH, 0) + @QTY WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND KEY_ITM = @KEY_ITM";
            SqlParameter[] _aryPt = new SqlParameter[4];
            _aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = yhId;
            _aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
            _aryPt[1].Value = yhNo;
            _aryPt[2] = new SqlParameter("@KEY_ITM", SqlDbType.Int);
            _aryPt[2].Value = keyItm;
            _aryPt[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _aryPt[3].Value = qty;
            this.ExecuteNonQuery(_sql, _aryPt);
        }
		#endregion
		

		#region	ȡ��Ҫ��������
		/// <summary>
		/// ȡ��Ҫ��������(��ת��������λ����)
		/// </summary>
		/// <param name="yhId">�������</param>
		/// <param name="yhNo">����</param>
		/// <param name="keyItm">KEY_ITM</param>
        /// <param name="isPrimaryUnit">�Ƿ��������λ</param>
		/// <returns></returns>
        public decimal GetOrgYhQty(string yhId, string yhNo, string keyItm, bool isPrimaryUnit)
		{
            string _qtyStr = "";
            string _joinStr = "";
            if (isPrimaryUnit)
            {
                _qtyStr = ",'1' AS UNIT,A.QTY * (CASE WHEN A.UNIT='2' THEN ISNULL(B.PK2_QTY,0) WHEN A.UNIT = '3' THEN ISNULL(B.PK3_QTY,0) ELSE 1 END) AS QTY";
                _joinStr = " LEFT JOIN PRDT B ON B.PRD_NO = A.PRD_NO ";
            }
            else
            {
                _qtyStr = ",A.UNIT,A.QTY";
                _joinStr = "";
            }
            string _sql = " SELECT  YH_ID,YH_NO" + _qtyStr
                         +" FROM TF_DYH A "
                         +_joinStr
                         + "WHERE A.YH_ID = @YH_ID AND A.YH_NO = @YH_NO AND A.KEY_ITM = @KEY_ITM";
			SqlParameter[] _aryPt = new SqlParameter[3];
			_aryPt[0] = new SqlParameter("@YH_ID", SqlDbType.VarChar, 2);
			_aryPt[0].Value = yhId;
			_aryPt[1] = new SqlParameter("@YH_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = yhNo;
			_aryPt[2] = new SqlParameter("@KEY_ITM", SqlDbType.Int);
			_aryPt[2].Value = Convert.ToInt32(keyItm);
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null,_aryPt);
			if (_ds.Tables[0].Rows.Count > 0
                && !string.IsNullOrEmpty(_ds.Tables[0].Rows[0]["QTY"].ToString()))
			{
				return Convert.ToDecimal(_ds.Tables[0].Rows[0]["QTY"]);
			}
			return 0;
		}
		#endregion

		#region �ֶ��������˽᰸
		/// <summary>
		/// 
		/// </summary>
		/// <param name="bilId"></param>
		/// <param name="bilNo"></param>
		/// <param name="clsId"></param>
		/// <returns></returns>
		public string CloseBill(string bilId , string bilNo , bool clsId )
		{
			string _result = "";
			string _sql = "Update MF_DYH set CLS_ID=@ClsID where YH_ID=@YhId and YH_NO=@YhNo and isNull(BACK_ID,'')='' ";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@YhId",SqlDbType.VarChar,20);
			_spc[0].Value = bilId;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@YhNo",SqlDbType.VarChar,20);
			_spc[1].Value = bilNo;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@ClsID",SqlDbType.VarChar,1);
			_spc[2].Value = (clsId?"T":"F");
			
			if ( this.ExecuteNonQuery(_sql,_spc) == 0 )
			{
				if ( !clsId )
					_result = "RCID=INV.HINT.CLS_ERROR";	//�ֶ����᰸������{0}ʧ�ܣ���Ϊ�õ��Ǳ�ϵͳ�Զ��᰸�ģ��޷����᰸��
			}
			return _result;
		}
		#endregion

		#region ��дԭ������,��ɾ��ע��
		/// <summary>
		/// ��д������ԭ������(����)
		/// </summary>
		/// <param name="yhId"></param>
		/// <param name="yhNo"></param>
		/// <param name="itm"></param>
		/// <param name="tableName"></param>
		public void UpdateQtyOld(string yhId,string yhNo,string itm,string tableName)
		{
			SqlParameter[] _sp = new SqlParameter[3];
			_sp[0] = new SqlParameter("@YH_ID" , SqlDbType.VarChar ,2);
			_sp[0].Value = yhId;
			_sp[1] = new SqlParameter("@YH_NO" , SqlDbType.VarChar ,20);
			_sp[1].Value = yhNo;
			_sp[2] = new SqlParameter("@ITM" , SqlDbType.Int);
			_sp[2].Value = itm;
			string _sql = " UPDATE "+ tableName +" SET QTY_OLD = QTY WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ITM = @ITM";
			this.ExecuteNonQuery( _sql, _sp);
		}
		/// <summary>
		/// ��д��ɾ��ע��
		/// </summary>
		/// <param name="yhId"></param>
		/// <param name="yhNo"></param>
		/// <param name="itm"></param>
		/// <param name="tableName"></param>
		public void UpdateDelId(string yhId,string yhNo,string itm,string tableName)
		{
			SqlParameter[] _sp = new SqlParameter[3];
			_sp[0] = new SqlParameter("@YH_ID",SqlDbType.VarChar,2);
			_sp[0].Value = yhId;
			_sp[1] = new SqlParameter("@YH_NO",SqlDbType.VarChar,20);
			_sp[1].Value = yhNo;
			_sp[2] = new SqlParameter("@ITM",SqlDbType.Int);
			_sp[2].Value = itm;
			string _sql = "";
			if (tableName == "TF_DYH")
			{
				_sql = "  UPDATE TF_DYH SET DEL_ID = 'T',QTY_SO =0 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND KEY_ITM = @ITM AND ISNULL(DEL_ID,'F') = 'F'";
			}
			else if (tableName == "TF_DYH1")
			{
				_sql = "  UPDATE TF_DYH1 SET DEL_ID = 'T' WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND KEY_ITM = @ITM AND ISNULL(DEL_ID,'F') = 'F';\n"
					+ "  UPDATE TF_DYH SET DEL_ID = 'T',QTY_SO =0 WHERE YH_ID = @YH_ID AND YH_NO = @YH_NO AND ISNULL(DEL_ID,'F') = 'F' AND BOX_ITM = @ITM ";
			}
			this.ExecuteNonQuery( _sql , _sp );
		}
		#endregion
	}
}
