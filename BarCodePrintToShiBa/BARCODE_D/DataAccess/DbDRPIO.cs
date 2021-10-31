using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPIO.
	/// </summary>
	public class DbDRPIO : DbObject
	{
		/// <summary>
		/// ���͵�
		/// </summary>
		/// <param name="connectionString"></param>
		public DbDRPIO(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// ȡ�õ�������
		/// </summary>
		/// <param name="IcID">�������IO�����͵� IB�������˻� IC�������� IM��DRP��������</param>
		/// <param name="IcNo">���͵���</param>
		/// <param name="YhNo">�����˻ص��˻����뵥��</param>
		/// <param name="OnlyFillSchema">�Ƿ�ֻ��ȡSchema</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string IcID,string IcNo,string YhNo,bool OnlyFillSchema)
		{
			string _sql = "select IC_ID,IC_NO,IC_DD,FIX_CST,REM,USR,CHK_MAN,PRT_SW,CLS_DATE,DEP,BIL_TYPE,SAL_NO,CUS_NO1,"
                + " CUS_NO2,CLS_ID,SYS_DATE,TOT_BOX,TOT_QTY,BIL_ID,BIL_NO,OUTDEP,SAL_NO2,EP_ID,EP_NO,BAT_NO,VOH_ID,VOH_NO,"
                + " CFM_SW,IZ_CLS_ID,IZ_BACK_ID,LOCK_MAN,TURN_ID2,LZ_CLS_ID2,AMT_CLS2,AMTN_NET_CLS2,TAX_CLS2,QTY_CLS2, "
                + " TURN_ID,LZ_CLS_ID,AMT_CLS,AMTN_NET_CLS,TAX_CLS,QTY_CLS,CAS_NO,TASK_ID,MOB_ID "
				+ " from MF_IC"
				+ " where IC_ID=@IcID and IC_NO=@IcNo;"
				+ "select T.IC_ID,T.IC_NO,T.ITM,T.IC_DD,T.PRD_NO,T.PRD_NAME,T.PRD_MARK,T.CST_STD,P.SPC,T.UNIT,T.QTY,T.WH1,T.WH2,T.CST,"
                + "T.FIX_CST,T.QTY_FA,T.KEY_ITM,T.UP,T.AMTN_NET,T.RTN_ID,T.BOX_ITM,T.BIL_ID,T.BIL_NO,T.BIL_ITM,T.UP_CST,T.DIS_CNT,"
                + "T.PRE_ITM,T.BAT_NO,T.BAT_NO2,T.VALID_DD,T.AMTN_NET_FP,T.AMTN_NET_FP2,T.QTY_FP,T.AMTN_EP,T.QTY_CFM,T.QTY_LOST,T.QTY1,T.UP_QTY1,T.UP_QTY1_CST,"
                + "T.EST_DD,T.REM,T.AMT_FP2,T.TAX_FP2,T.QTY_FP2,T.AMT_FP,T.TAX_FP,T.PAK_UNIT,T.PAK_EXC,T.PAK_NW,T.PAK_WEIGHT_UNIT,T.PAK_GW,T.PAK_MEAST,T.PAK_MEAST_UNIT,T.PRD_MARK2  "
				+ " from TF_IC T JOIN PRDT P ON T.PRD_NO = P.PRD_NO"
				+ " where IC_ID=@IcID and IC_NO=@IcNo order by T.ITM;"
				+ "select IC_ID,IC_NO,IC_ITM,ITM,PRD_NO,PRD_MARK,BAR_CODE,BOX_NO"
				+ " from TF_IC1"
				+ " where IC_ID=@IcID and IC_NO=@IcNo;"
				+ "select IC_ID,IC_NO,ITM,PRD_NO,CONTENT,QTY,QTY_FA,KEY_ITM,WH1,WH2,BIL_ID,BIL_NO,BIL_ITM"
				+ " from TF_IC2"
				+ " where IC_ID=@IcID and IC_NO=@IcNo;"
				+ "select YH_ID,YH_NO,ITM,PRD_NO,WH,KEY_ITM,EST_DD,QTY,UNIT,AMTN,QTY_RTN,UP,REM,QTY_OLD,WH_OLD,EST_OLD,DEL_ID"
				+ " from TF_DYH where YH_ID='YI' and YH_NO=@YhNo";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@IcID",SqlDbType.Char,2);
			_spc[0].Value = IcID;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@IcNo",SqlDbType.VarChar,20);
			_spc[1].Value = IcNo;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@YhNo",SqlDbType.VarChar,20);
			_spc[2].Value = YhNo;
			string[] _aryTableName = new string[] {"MF_IC","TF_IC","TF_IC_BAR","TF_IC_BOX","TF_DYH"};
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (OnlyFillSchema)
			{
				this.FillDatasetSchema(_sql,_ds,_aryTableName,_spc);
			}
			else
			{
				this.FillDataset(_sql,_ds,_aryTableName,_spc);
			}
			DataColumn[] _dca = new DataColumn[3];
			_dca[0] = _ds.Tables["TF_IC"].Columns["IC_ID"];
			_dca[1] = _ds.Tables["TF_IC"].Columns["IC_NO"];
			_dca[2] = _ds.Tables["TF_IC"].Columns["ITM"];
			_ds.Tables["TF_IC"].PrimaryKey = _dca;
			//��ͷ�ͱ������
			DataColumn[] _dca1 = new DataColumn[2];
			_dca1[0] = _ds.Tables["MF_IC"].Columns["IC_ID"];
			_dca1[1] = _ds.Tables["MF_IC"].Columns["IC_NO"];
			DataColumn[] _dca2 = new DataColumn[2];
			_dca2[0] = _ds.Tables["TF_IC"].Columns["IC_ID"];
			_dca2[1] = _ds.Tables["TF_IC"].Columns["IC_NO"];
			_ds.Relations.Add("MF_ICTF_IC",_dca1,_dca2);
			//�����BAR_CODE����
			_dca1 = new DataColumn[3];
			_dca1[0] = _ds.Tables["TF_IC"].Columns["IC_ID"];
			_dca1[1] = _ds.Tables["TF_IC"].Columns["IC_NO"];
			_dca1[2] = _ds.Tables["TF_IC"].Columns["KEY_ITM"];
			_dca2 = new DataColumn[3];
			_dca2[0] = _ds.Tables["TF_IC_BAR"].Columns["IC_ID"];
			_dca2[1] = _ds.Tables["TF_IC_BAR"].Columns["IC_NO"];
			_dca2[2] = _ds.Tables["TF_IC_BAR"].Columns["IC_ITM"];
			_ds.Relations.Add("TF_ICTF_IC_BAR",_dca1,_dca2);
			//��ͷ�������ݹ���
			_dca1 = new DataColumn[2];
			_dca1[0] = _ds.Tables["MF_IC"].Columns["IC_ID"];
			_dca1[1] = _ds.Tables["MF_IC"].Columns["IC_NO"];
			_dca2 = new DataColumn[2];
			_dca2[0] = _ds.Tables["TF_IC_BOX"].Columns["IC_ID"];
			_dca2[1] = _ds.Tables["TF_IC_BOX"].Columns["IC_NO"];
			_ds.Relations.Add("MF_ICTF_IC_BOX",_dca1,_dca2);
			//ȡ��TF_DYH�������������˻����뵥(����PMARK)ת����ʱ������ITM���Զ�ά�����޷�ת�룩-------by yola
			//_ds.Tables["TF_DYH"].PrimaryKey = null;
			return _ds;
		}

		/// <summary>
		/// �޸Ľ᰸��ǡ�����˺��������
		/// </summary>
		/// <param name="IcID">�������IO�����͵� IB�������˻� IC�������� IM��DRP��������</param>
		/// <param name="IcNo">���͵���</param>
		/// <param name="ChkMan">�����</param>
		/// <param name="ClsDate">�������</param>
		public void UpdateChkMan(string IcID,string IcNo,string ChkMan,DateTime ClsDate)
		{
			string _sql = "update MF_IC set CHK_MAN=@ChkMan,CLS_DATE=@ClsDate where IC_ID=@IcID and IC_NO=@IcNo";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@ChkMan",SqlDbType.VarChar,12);
			if (String.IsNullOrEmpty(ChkMan))
			{
				_spc[0].Value = System.DBNull.Value;
			}
			else
			{
				_spc[0].Value = ChkMan;
			}
			_spc[1] = new System.Data.SqlClient.SqlParameter("@ClsDate",SqlDbType.DateTime);
			if (String.IsNullOrEmpty(ChkMan))
			{
				_spc[1].Value = System.DBNull.Value;
			}
			else
			{
				_spc[1].Value = ClsDate.ToString("yyyy-MM-dd HH:mm:ss");
			}
			_spc[2] = new System.Data.SqlClient.SqlParameter("@IcID",SqlDbType.Char,2);
			_spc[2].Value = IcID;
			_spc[3] = new System.Data.SqlClient.SqlParameter("@IcNo",SqlDbType.VarChar,20);
			_spc[3].Value = IcNo;
			this.ExecuteNonQuery(_sql,_spc);
		}

		/// <summary>
		/// �޸����͵���������������
		/// </summary>
		/// <param name="IcID">�������IO�����͵� IB�������˻� IC�������� IM���ͻ���������</param>
		/// <param name="IcNo">���͵���</param>
		/// <param name="KeyItm">׷��������</param>
		/// <param name="Qty">��������</param>
		public string UpdateBoxQty(string IcID,string IcNo,string KeyItm,decimal Qty)
		{
			string _sql = "declare @QtyBox numeric\n"
				+ "declare @QtyFa numeric\n"
				+ "set @QtyBox=(select isnull(QTY,0) from TF_IC2 where IC_ID=@IcID and IC_NO=@IcNo and KEY_ITM=@KeyItm)\n"
				+ "set @QtyFa=@Qty+isnull((select isnull(QTY_FA,0) from TF_IC2 where IC_ID=@IcID and IC_NO=@IcNo and KEY_ITM=@KeyItm),0)\n"
				+ "if (@QtyBox is null)\n"
				+ "	select 1\n"
				+ "else\n"
				+ "begin\n"
				+ "	if (@QtyFa>@QtyBox)\n"
				+ "		select 2\n"
				+ "	else\n"
				+ "	begin\n"
				+ "		update TF_IC2 set QTY_FA=@QtyFa where IC_ID=@IcID and IC_NO=@IcNo and KEY_ITM=@KeyItm\n"
				+ "		update TF_IC set QTY_FA=isnull(QTY,0)*@QtyFa/@QtyBox where IC_ID=@IcID and IC_NO=@IcNo and BOX_ITM=@KeyItm\n"
				+ "		select 0\n"
				+ "		if (select count(*) from TF_IC where IC_ID=@IcID and IC_NO=@IcNo and (isnull(QTY,0)-isnull(QTY_FA,0)<>0))=0\n"
				+ "			update MF_IC set CLS_ID='T' where IC_ID=@IcID and IC_NO=@IcNo\n"
				+ "		else\n"
				+ "			update MF_IC set CLS_ID='F' where IC_ID=@IcID and IC_NO=@IcNo\n"
				+ "	end\n"
				+ "end";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@IcNo",SqlDbType.VarChar,20);
			_spc[0].Value = IcID;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@IcNo",SqlDbType.VarChar,20);
			_spc[1].Value = IcNo;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@KeyItm",SqlDbType.SmallInt);
			_spc[2].Value = KeyItm;
			_spc[3] = new System.Data.SqlClient.SqlParameter("@Qty",SqlDbType.Decimal);
			_spc[3].Precision = 28;
			_spc[3].Scale = 8;
            _spc[3].Value = Qty;
            string _result = "1";
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _spc))
            //{
            //    if (_sdr.Read())
            //    {
            //        _result = _sdr[0].ToString();
            //    }
            //}
            object _rObj = this.ExecuteScalar(_sql, _spc);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
			return _result;
		}

		#region ����
		/// <summary>
		/// ȡ�ñ����¼
		/// </summary>
		/// <param name="pIcId"></param>
		/// <returns></returns>
		public DataTable GetBodyData(string pIcId)
		{
			string _sqlStr = "SELECT M.IC_NO,M.IC_DD,M.CUS_NO2 AS CUS_NO,T.PRD_NO,PT.PRD_NAME,T.KEY_ITM,T.QTY, "
				+ " ISNULL(T.QTY_FA,0) AS QTY_FA, "
				+ " ISNULL(T.QTY_L,0) AS QTY_L,T.BOX_ITM,CUS.CUS_NAME  "
				+ " FROM  "
				+ " (SELECT IC_NO,IC_DD,CUS_NO2  "
				+ " FROM MF_IC WHERE IC_ID='IO') AS M  "
				+ " JOIN  "
				+ " (SELECT IC_NO,KEY_ITM,PRD_NO, "
				+ " ISNULL(QTY,0) AS QTY, "
				+ " ISNULL(QTY_FA,0) AS QTY_FA, "
				+ " ISNULL(QTY,0)-ISNULL(QTY_FA,0) AS QTY_L,BOX_ITM  "
				+ " FROM TF_IC WHERE ISNULL(QTY,0)-ISNULL(QTY_FA,0) > 0 AND (BOX_ITM IS NULL OR BOX_ITM = '')) AS T  "
				+ " ON M.IC_NO=T.IC_NO "
				+ " LEFT JOIN "
				+ " (SELECT PRD_NO,SNM AS PRD_NAME FROM PRDT) AS PT "
				+ " ON.T.PRD_NO=PT.PRD_NO "
				+ " LEFT JOIN "
				+ " (SELECT CUS_NO,SNM AS CUS_NAME FROM CUST) AS CUS"
				+ " ON M.CUS_NO2 = CUS.CUS_NO"
				+ " UNION "
				+ " SELECT M.IC_NO,M.IC_DD,M.CUS_NO2 AS CUS_NO,T.PRD_NO,T.PRD_NAME,T.KEY_ITM,T.QTY,  "
				+ " ISNULL(T.QTY_FA,0) AS QTY_FA, "
				+ " ISNULL(T.QTY_L,0) AS QTY_L,T.BOX_ITM,CUS.CUS_NAME FROM "
				+ " (SELECT IC_NO,IC_DD,CUS_NO2 FROM MF_IC WHERE IC_ID='IO') AS M "
				+ " JOIN "
				+ " (SELECT IC_NO,PRD_NO,CONTENT AS PRD_NAME,KEY_ITM,ISNULL(QTY,0) AS QTY, "
				+ " ISNULL(QTY_FA,0) AS QTY_FA,(ISNULL(QTY,0)-ISNULL(QTY_FA,0)) AS QTY_L,KEY_ITM AS BOX_ITM "
				+ " FROM TF_IC2 WHERE ISNULL(QTY,0)-ISNULL(QTY_FA,0) > 0) AS T "
				+ " ON M.IC_NO = T.IC_NO "
				+ " LEFT JOIN "
				+ " (SELECT PRD_NO,SNM AS PRD_NAME FROM PRDT) AS PT "
				+ " ON T.PRD_NO = PT.PRD_NO"
				+ " LEFT JOIN "
				+ " (SELECT CUS_NO,SNM AS CUS_NAME FROM CUST) AS CUS "
				+ " ON M.CUS_NO2 = CUS.CUS_NO";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			return _dt;
		}

		/// <summary>
		/// ȡ�ñ����¼
		/// </summary>
		/// <param name="pIcId"></param>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public DataTable GetBodyData(string pIcId, string pIcNo, string pItm)
		{
			string _sqlStr = "SELECT M.IC_NO,M.IC_DD,T.PRD_NO,T.PRD_NAME,T.KEY_ITM,T.QTY,ISNULL(T.QTY_FA,0) AS QTY_FA,ISNULL(T.QTY_L,0) AS QTY_L,M.CUS_NO2 FROM"
				+" (SELECT IC_NO,IC_DD,CUS_NO2 FROM MF_IC WHERE IC_ID='"+pIcId+"' AND IC_NO='"+pIcNo+"') AS M"
				+" LEFT JOIN (SELECT IC_NO,KEY_ITM,PRD_NO,ISNULL(QTY,0) AS QTY,ISNULL(QTY_FA,0) AS QTY_FA,ISNULL(QTY,0)-ISNULL(QTY_FA,0) AS QTY_L,"
				+" (SELECT TOP 1 NAME FROM PRDT T WHERE T.PRD_NO=PRD_NO) AS PRD_NAME FROM TF_IC WHERE KEY_ITM='"+pItm+"') AS T"
				+" ON M.IC_NO=T.IC_NO";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			return _dt;
		}

		/// <summary>
		/// ȡ�ñ����¼
		/// </summary>
		/// <param name="pIcId"></param>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public DataTable GetBodyData2(string pIcId, string pIcNo, string pItm)
		{
			string _sqlStr = "SELECT M.IC_NO,M.IC_DD,T.PRD_NO,T.PRD_NAME,T.KEY_ITM,T.QTY,ISNULL(T.QTY_FA,0) AS QTY_FA,ISNULL(T.QTY_L,0) AS QTY_L,M.CUS_NO2 FROM"
				+" (SELECT IC_NO,IC_DD,CUS_NO2 FROM MF_IC WHERE IC_ID='"+pIcId+"' AND IC_NO='"+pIcNo+"') AS M"
				+" LEFT JOIN (SELECT IC_NO,KEY_ITM,PRD_NO,ISNULL(QTY,0) AS QTY,ISNULL(QTY_FA,0) AS QTY_FA,ISNULL(QTY,0)-ISNULL(QTY_FA,0) AS QTY_L,"
				+" CONTENT AS PRD_NAME FROM TF_IC2 WHERE KEY_ITM='"+pItm+"') AS T"
				+" ON M.IC_NO=T.IC_NO";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			return _dt;
		}
        /// <summary>
        /// ȡ�ñ����¼
        /// </summary>
        /// <param name="icId"></param>
        /// <param name="icNo"></param>
        /// <param name="keyItm"></param>
        /// <returns></returns>
        public DataSet GetBodyData3(string icId, string icNo, string keyItm)
        {
            string _sql = "SELECT QTY, QTY_CFM, QTY_LOST FROM TF_IC WHERE IC_ID = @IC_ID AND IC_NO = @IC_NO AND KEY_ITM = @KEY_ITM";
            SqlParameter[] _aryPt = new SqlParameter[3];
            _aryPt[0] = new SqlParameter("@IC_ID", SqlDbType.VarChar, 2);
            _aryPt[0].Value = icId;
            _aryPt[1] = new SqlParameter("@IC_NO", SqlDbType.VarChar, 30);
            _aryPt[1].Value = icNo;
            _aryPt[2] = new SqlParameter("@KEY_ITM", SqlDbType.Int);
            _aryPt[2].Value = Convert.ToInt32(keyItm);
            DataSet _ds = new DataSet();
            this.FillDataset(_sql, _ds, new string[1] { "TF_IC" }, _aryPt);
            return _ds;
        }
		/// <summary>
		/// ȡ������еĲ�Ʒ���
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetPrdNo(string pIcNo, string pItm)
		{
			string _sqlStr = "SELECT PRD_NO FROM TF_IC2 WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pItm+"'";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			if (_dt.Rows.Count > 0)
			{
				return _dt.Rows[0][0].ToString();
			}
			else
			{
				_sqlStr = "SELECT PRD_NO FROM TF_IC WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pItm+"'";
				DataTable _dt1 = this.ExecuteDataset(_sqlStr).Tables[0];
				if (_dt1.Rows.Count > 0)
				{
					return _dt1.Rows[0][0].ToString();
				}
			}
			return "";
		}
		
		/// <summary>
		/// GetQty
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetQty(string pIcNo, string pItm)
		{
			string _sqlStr = "SELECT QTY FROM TF_IC WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pItm+"'";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			if (_dt.Rows.Count > 0)
			{
				return _dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}
		
		/// <summary>
		/// GetQty
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetQty2(string pIcNo, string pItm)
		{
			string _sqlStr = "SELECT QTY FROM TF_IC2 WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pItm+"'";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			if (_dt.Rows.Count > 0)
			{
				return _dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetQtySum(string pIcNo, string pItm)
		{
			string _sqlStr = "SELECT QTY_FA FROM TF_IC WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pItm+"'";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			if (_dt.Rows.Count > 0)
			{
				return _dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pItm"></param>
		/// <returns></returns>
		public string GetQtySum2(string pIcNo, string pItm)
		{
			string _sqlStr = "SELECT QTY_FA FROM TF_IC2 WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pItm+"'";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			if (_dt.Rows.Count > 0)
			{
				return _dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// ��д��������(����)
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pKeyItm"></param>
		/// <param name="pQty"></param>
		public void SetQtyFx(string pIcNo, string pKeyItm, string pQty)
		{
			string _sqlStr = "UPDATE TF_IC SET QTY_FA = ISNULL(QTY_FA,0) + "+pQty+" WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pKeyItm+"'";
			this.ExecuteNonQuery(_sqlStr);
		}

		/// <summary>
		/// ��д��������(ɾ��)
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pKeyItm"></param>
		/// <param name="pQty"></param>
		public void SetQtyFxd(string pIcNo, string pKeyItm, string pQty)
		{
			string _sqlStr = "UPDATE TF_IC SET QTY_FA = ISNULL(QTY_FA,0) - "+pQty+" WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pKeyItm+"'";
			this.ExecuteNonQuery(_sqlStr);
		}
		
		/// <summary>
		/// ��д��������(�޸�)
		/// </summary>
		/// <param name="pIcNo"></param>
		/// <param name="pKeyItm"></param>
		/// <param name="pQty"></param>
		/// <param name="pOldQty"></param>
		public void SetQtyFxu(string pIcNo, string pKeyItm, string pOldQty, string pQty)
		{
			string _sqlStr = "UPDATE TF_IC SET QTY_FA = ISNULL(QTY_FA,0) - "+Convert.ToString(Convert.ToDecimal(pOldQty)-Convert.ToDecimal(pQty))+" WHERE IC_NO='"+pIcNo+"' AND KEY_ITM='"+pKeyItm+"'";
			this.ExecuteNonQuery(_sqlStr);
		}

		/// <summary>
		/// ���͵��᰸
		/// </summary>
		/// <param name="pIcNo"></param>
		public void ChkEnd(string pIcNo)
		{
			bool _flag = true;
			string _sqlStr = "SELECT QTY, QTY_FA FROM TF_IC WHERE IC_NO='"+pIcNo+"'";
			DataTable _dt = this.ExecuteDataset(_sqlStr).Tables[0];
			for(int i=0;i<_dt.Rows.Count;i++)
			{
				if (_dt.Rows[i]["QTY"].ToString() != _dt.Rows[i]["QTY_FA"].ToString())
				{
					_flag = false;
					break;
				}
			}
			if (_flag)
			{
				_sqlStr = "UPDATE MF_IC SET CLS_ID='T' WHERE IC_NO='"+pIcNo+"'";
			}
			else
			{
				_sqlStr = "UPDATE MF_IC SET CLS_ID='F' WHERE IC_NO='"+pIcNo+"'";
			}
			this.ExecuteNonQuery(_sqlStr);
		}
		#endregion

		#region ����������
		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="BeginDate">��ʼ����</param>
		/// <param name="EndDate">��������</param>
		public void ResetBoxQty(string BeginDate,string EndDate)
		{
			SqlParameter [] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@BDate",SqlDbType.VarChar,10);
			_spc[0].Value = BeginDate;
			_spc[1] = new SqlParameter("@EDate",SqlDbType.VarChar,10);
			_spc[1].Value = EndDate;
			this.ExecuteSpNonQuery("sp_CalculateBOXQTYIC",_spc);
		}

		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="IcNo">���͵���</param>
		/// <param name="PrdMark">��Ʒ����</param>
		/// <param name="BoxItm">�����</param>
		/// <param name="Qty">����</param>
		public void ResetBoxQty(string IcNo,string PrdMark,int BoxItm,decimal Qty)
		{
			SqlParameter [] _spc = new SqlParameter[4];
			_spc[0] = new SqlParameter("@IcNo",SqlDbType.VarChar,20);
			_spc[0].Value = IcNo;
			_spc[1] = new SqlParameter("@PrdMark",SqlDbType.VarChar,40);
			_spc[1].Value = PrdMark;
			_spc[2] = new SqlParameter("@BoxItm",SqlDbType.Int);
			_spc[2].Value = BoxItm;
			_spc[3] = new SqlParameter("@Qty",SqlDbType.Decimal);
			_spc[3].Precision = 28;
			_spc[3].Scale = 8;
			_spc[3].Value = Qty;
			this.ExecuteSpNonQuery("sp_CalculateBOXQTYIC1",_spc);
		}
		#endregion

		#region �ó����͵��ݲ�ͬ�������prdt1�еĵ�һ������
		/// <summary>
		///  �ó����͵��ݲ�ͬ�������prdt1�еĵ�һ������
		/// </summary>
		/// <param name="BeginDate">��ʼ����</param>
		/// <param name="EndDate">��������</param>
		/// <returns></returns>
		public DataTable GetFirstPrdt1ByBox(string BeginDate,string EndDate)
		{
			SqlParameter [] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@BDate",SqlDbType.VarChar,10);
			_spc[0].Value = BeginDate;
			_spc[1] = new SqlParameter("@EDate",SqlDbType.VarChar,10);
			_spc[1].Value = EndDate;
			string _sqlStr= "select IC_ID,IC_NO,ITM,PRD_NO,PRD_MARK,BOX_ITM,QTY from fn_box_tf_ic(@BDate,@EDate)";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,_ds,null,_spc);
			return _ds.Tables[0];
		}
		#endregion

        #region �ֶ��������˽᰸

        /// <summary>
        /// �ֶ��������˽᰸
        /// </summary>
        /// <param name="icId">���ݱ�</param>
        /// <param name="icNo">����</param>
        /// <param name="clsName">�᰸�ֶ�</param>
        /// <param name="clsId">�᰸��־:trueΪ�᰸��falseΪ���᰸</param>
        /// <returns>������Ϣ</returns>
        public string CloseBill(string icId, string icNo, string clsName, bool clsId)
        {
            string _backId = "";

            string _sql = "UPDATE MF_IC SET " + clsName + " = @CLS_ID,IZ_BACK_ID = @IZ_BACK_ID WHERE IC_ID = @IC_ID AND IC_NO = @IC_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[4];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@IC_NO", SqlDbType.VarChar, 20);
            _spc[0].Value = icNo;
            _spc[1] = new System.Data.SqlClient.SqlParameter("@CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = (clsId ? "T" : "F");
            _spc[2] = new System.Data.SqlClient.SqlParameter("@IC_ID", SqlDbType.VarChar, 2);
            _spc[2].Value = icId;
            _spc[3] = new System.Data.SqlClient.SqlParameter("@IZ_BACK_ID", SqlDbType.VarChar, 2);
            _spc[3].Value = _backId;
            string _result = "";
            if (this.ExecuteNonQuery(_sql, _spc) == 0)
            {
                if (!clsId)
                {
                    _result = "RCID=COMMON.HINT.CLS_ERROR";	//�ֶ����᰸������{0}ʧ�ܣ���Ϊ�õ��Ǳ�ϵͳ�Զ��᰸�ģ��޷����᰸��
                }
            }
            return _result;
        }

        #endregion

        #region �������͵�ƾ֤����
        /// <summary>
        /// �������͵�ƾ֤����
        /// </summary>
        /// <param name="icId"></param>
        /// <param name="icNo"></param>
        /// <param name="vohNo"></param>
        /// <returns></returns>
        public bool UpdateVohNo(string icId, string icNo, string vohNo)
        {
            bool _result = false;
            string _sqlStr = "";
            _sqlStr = " UPDATE MF_IC SET VOH_NO=@VOH_NO WHERE IC_ID=@IC_ID AND IC_NO=@IC_NO";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@IC_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = icId;

            _sqlPara[1] = new SqlParameter("@IC_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = icNo;

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
        #endregion

        #region INVIK & INVLI  ��д��



        /// <summary>
        /// ���շ�Ʊ��дMF_BLN��ͷ��
        /// </summary>
        /// <param name="turnId">���Ʊʶ�� 1.��ϸ 2.����</param>
        /// <param name="lzclsId">��Ʊ�᰸ע��</param>
        /// <param name="amtCls">�ѿ����</param>
        /// <param name="amtn_netCls">�ѿ�δ˰���</param>
        /// <param name="taxCls">�ѿ�˰��</param> 
        /// <param name="qtyCls">�ѿ�����</param> 
        /// <param name="turnId2">���Ʊʶ�� 1.��ϸ 2.����</param>
        /// <param name="lzclsId2">��Ʊ�᰸ע��</param>
        /// <param name="amtCls2">�ѿ����</param>
        /// <param name="amtn_netCls2">�ѿ�δ˰���</param>
        /// <param name="taxCls2">�ѿ�˰��</param> 
        /// <param name="qtyCls2">�ѿ�����</param> 
        /// <param name="icId"></param>
        /// <param name="icNo"></param>
        public void UpdateInvIkHeadData(string turnId, string lzclsId, decimal amtCls, decimal amtn_netCls, decimal taxCls, decimal qtyCls, string turnId2, string lzclsId2, decimal amtCls2, decimal amtn_netCls2, decimal taxCls2, decimal qtyCls2, string icId, string icNo)
        {
            string _sql = "update MF_IC set TURN_ID=@TURN_ID,LZ_CLS_ID=@LZ_CLS_ID,AMT_CLS=@AMT_CLS,AMTN_NET_CLS=@AMTN_NET_CLS,TAX_CLS=@TAX_CLS,QTY_CLS=@QTY_CLS, " +
                           "TURN_ID2=@TURN_ID2,LZ_CLS_ID2=@LZ_CLS_ID2,AMT_CLS2=@AMT_CLS2,AMTN_NET_CLS2=@AMTN_NET_CLS2,TAX_CLS2=@TAX_CLS2,QTY_CLS2=@QTY_CLS2 " +
                           " Where IC_ID=@IC_ID and IC_NO=@IC_NO";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[14];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@TURN_ID", SqlDbType.VarChar, 1);
            _spc[0].Value = turnId;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID", SqlDbType.VarChar, 1);
            _spc[1].Value = lzclsId;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@AMT_CLS", SqlDbType.Decimal, 0);
            _spc[2].Value = amtCls;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_CLS", SqlDbType.Decimal, 0);
            _spc[3].Value = amtn_netCls;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@TAX_CLS", SqlDbType.Decimal, 0);
            _spc[4].Value = taxCls;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@QTY_CLS", SqlDbType.Decimal, 0);
            _spc[5].Value = qtyCls;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@TURN_ID2", SqlDbType.VarChar, 1);
            _spc[6].Value = turnId2;

            _spc[7] = new System.Data.SqlClient.SqlParameter("@LZ_CLS_ID2", SqlDbType.VarChar, 1);
            _spc[7].Value = lzclsId2;

            _spc[8] = new System.Data.SqlClient.SqlParameter("@AMT_CLS2", SqlDbType.Decimal, 0);
            _spc[8].Value = amtCls2;

            _spc[9] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_CLS2", SqlDbType.Decimal, 0);
            _spc[9].Value = amtn_netCls2;

            _spc[10] = new System.Data.SqlClient.SqlParameter("@TAX_CLS2", SqlDbType.Decimal, 0);
            _spc[10].Value = taxCls2;

            _spc[11] = new System.Data.SqlClient.SqlParameter("@QTY_CLS2", SqlDbType.Decimal, 0);
            _spc[11].Value = qtyCls2;
            _spc[12] = new System.Data.SqlClient.SqlParameter("@IC_ID", SqlDbType.VarChar, 2);
            _spc[12].Value = icId;
            _spc[13] = new System.Data.SqlClient.SqlParameter("@IC_NO", SqlDbType.VarChar, 20);
            _spc[13].Value = icNo;
            this.ExecuteNonQuery(_sql, _spc);
        }


        /// <summary>
        ///������Ʊ��д��Դ��������λ
        /// </summary>
        /// <param name="amtFp">�ѿ����</param>
        /// <param name="amtn_netFp">�ѿ�δ˰���</param>
        /// <param name="taxFp">�ѿ�˰��</param>
        /// <param name="qtyFp">�ѿ�����</param>
        /// <param name="amtFp2">�ѿ����</param>
        /// <param name="amtn_netFp2">�ѿ�δ˰���</param>
        /// <param name="taxFp2">�ѿ�˰��</param>
        /// <param name="qtyFp2">�ѿ�����</param>
        /// <param name="icId"></param>
        /// <param name="icNo"></param>
        /// <param name="itm">�������</param>
        public void UpdateInvIkBodyData(decimal amtFp, decimal amtn_netFp, decimal taxFp, decimal qtyFp, decimal amtFp2, decimal amtn_netFp2, decimal taxFp2, decimal qtyFp2, string icId, string icNo, int itm)
        {
            string _sql = "update TF_IC set AMT_FP=@AMT_FP,AMTN_NET_FP=@AMTN_NET_FP,TAX_FP=@TAX_FP,QTY_FP=@QTY_FP,AMT_FP2=@AMT_FP2,AMTN_NET_FP2=@AMTN_NET_FP2,TAX_FP2=@TAX_FP2,QTY_FP2=@QTY_FP2 Where IC_ID=@IC_ID and  IC_NO=@IC_NO and ITM=@ITM  ";
            System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[11];
            _spc[0] = new System.Data.SqlClient.SqlParameter("@AMT_FP", SqlDbType.Decimal);
            _spc[0].Value = amtFp;

            _spc[1] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP", SqlDbType.Decimal);
            _spc[1].Value = amtn_netFp;

            _spc[2] = new System.Data.SqlClient.SqlParameter("@TAX_FP", SqlDbType.Decimal);
            _spc[2].Value = taxFp;

            _spc[3] = new System.Data.SqlClient.SqlParameter("@QTY_FP", SqlDbType.Decimal);
            _spc[3].Value = qtyFp;

            _spc[4] = new System.Data.SqlClient.SqlParameter("@AMT_FP2", SqlDbType.Decimal);
            _spc[4].Value = amtFp2;

            _spc[5] = new System.Data.SqlClient.SqlParameter("@AMTN_NET_FP2", SqlDbType.Decimal);
            _spc[5].Value = amtn_netFp2;

            _spc[6] = new System.Data.SqlClient.SqlParameter("@TAX_FP2", SqlDbType.Decimal);
            _spc[6].Value = taxFp2;

            _spc[7] = new System.Data.SqlClient.SqlParameter("@QTY_FP2", SqlDbType.Decimal);
            _spc[7].Value = qtyFp2;

            _spc[8] = new System.Data.SqlClient.SqlParameter("@IC_ID", SqlDbType.VarChar, 2);
            _spc[8].Value = icId;
            _spc[9] = new System.Data.SqlClient.SqlParameter("@IC_NO", SqlDbType.VarChar, 20);
            _spc[9].Value = icNo;

            _spc[10] = new System.Data.SqlClient.SqlParameter("@ITM", SqlDbType.Int);
            _spc[10].Value = itm;


         
            this.ExecuteNonQuery(_sql, _spc);

        }
        #endregion
	}
}
