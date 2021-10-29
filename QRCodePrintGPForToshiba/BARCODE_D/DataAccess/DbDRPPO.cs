using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbDRPPO.
	/// </summary>
	public class DbDRPPO : DbObject
	{
		#region 构造函数
		/// <summary>
		/// 采购单
		/// </summary>
		/// <param name="connectionString">SQL连接字串</param>
		public DbDRPPO(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region GetData
		/// <summary>
		/// GetData
		/// </summary>
		/// <param name="osId">单据别</param>
		/// <param name="osNo">单号</param>
		/// <param name="bChk">是否走确认</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string osId, string osNo, bool bChk)
		{
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@OS_ID",SqlDbType.VarChar,2);
			_spc[0].Value = osId;
			_spc[1] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_spc[1].Value = osNo;
			string _filter = "";
			if (bChk)
			{
				_filter = " AND ISNULL(A.SCM_USR, '') <> ''";
			}

            string _sql = "SELECT A.*,C.NAME SAL_NAME,D.NAME DEP_NAME,E.NAME CUS_NAME,F.NAME BIL_NAME FROM MF_POS A "
                    + " LEFT JOIN SALM C ON A.SAL_NO=C.SAL_NO "
                    + " LEFT JOIN DEPT D ON A.PO_DEP=D.DEP "
                    + " INNER JOIN CUST E ON A.CUS_NO=E.CUS_NO "
                    + " LEFT JOIN BIL_SPC F ON F.BIL_ID='PC' AND F.SPC_ID='IB' AND A.BIL_TYPE=F.SPC_NO "
                    + " WHERE A.OS_ID=@OS_ID AND A.OS_NO=@OS_NO; "

                    + " SELECT A.*,B.UT,C.NAME WH_NAME,B.SPC FROM TF_POS A "
                    + " INNER JOIN PRDT B ON A.PRD_NO=B.PRD_NO "
                    + " LEFT JOIN MY_WH C ON A.WH=C.WH "

                    + " WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO" + _filter + " order by A.ITM;";

			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"MF_POS","TF_POS"},_spc);

			//设定PK，因为用了left join以后，PK会取不到
			DataColumn[] _dca = new DataColumn[2];
			_dca[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
			_dca[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];
			_ds.Tables["MF_POS"].PrimaryKey = _dca;
			_dca = new DataColumn[3];
			_dca[0] = _ds.Tables["TF_POS"].Columns["OS_ID"];
			_dca[1] = _ds.Tables["TF_POS"].Columns["OS_NO"];
			_dca[2] = _ds.Tables["TF_POS"].Columns["ITM"];
			_ds.Tables["TF_POS"].PrimaryKey = _dca;
			//表头和表身关联
			DataColumn[] _dca1 = new DataColumn[2];
			_dca1[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
			_dca1[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];
			DataColumn[] _dca2 = new DataColumn[2];
			_dca2[0] = _ds.Tables["TF_POS"].Columns["OS_ID"];
			_dca2[1] = _ds.Tables["TF_POS"].Columns["OS_NO"];
			_ds.Relations.Add("MF_POSTF_POS",_dca1,_dca2);

			return _ds;
		}

		/// <summary>
		/// 取原单单位，退回时用
		/// </summary>
		/// <param name="osId">单据别</param>
		/// <param name="osNo">单号</param>
		/// <param name="preItm">追踪项次</param>
		/// <returns></returns>
		public SunlikeDataSet GetData(string osId, string osNo, int preItm)
		{
			SqlParameter[] _spc = new SqlParameter[3];
			_spc[0] = new SqlParameter("@OS_ID",SqlDbType.VarChar,2);
			_spc[0].Value = osId;
			_spc[1] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_spc[1].Value = osNo;
			_spc[2] = new SqlParameter("@PRE_ITM",SqlDbType.Int);
			_spc[2].Value = preItm;
			string _sql = "SELECT TF_POS.UNIT,TF_POS.BAT_NO,TF_POS.PRD_NO,TF_POS.WH,TF_POS.PRD_MARK,isNull(TF_POS.QTY,0) QTY,isNull(TF_POS.QTY_PS,0) QTY_PS,isNull(TF_POS.QTY_PRE,0) QTY_PRE,"
                        + " isNull(TF_POS.QTY_PO,0) QTY_PO,isNull(TF_POS.QTY_PO_UNSH,0) AS QTY_PO_UNSH,"
						+" ISNULL(MF_POS.BACK_ID,'') AS BACK_ID,ISNULL(MF_POS.CLS_ID,'F') AS CLS_ID"
						+" FROM TF_POS "
						+" LEFT JOIN MF_POS ON MF_POS.OS_ID=TF_POS.OS_ID AND MF_POS.OS_NO=TF_POS.OS_NO "
						+" WHERE TF_POS.OS_ID=@OS_ID AND TF_POS.OS_NO=@OS_NO AND TF_POS.PRE_ITM=@PRE_ITM";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[] {"TF_POS"},_spc);
			return _ds;
		}
		/// <summary>
		/// 取得采购单数据
		/// </summary>
		/// <param name="osId"></param>
		/// <param name="osNo"></param>
		/// <param name="isSchema"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataPO(string osId,string osNo,bool isSchema)
		{
			SqlParameter[] _sqlPara = new SqlParameter[2];
			_sqlPara[0] = new SqlParameter("@OS_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = osId;
			_sqlPara[1] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = osNo;
			string _sqlStr = "";
			string _sqlWhere = "";
			if (isSchema)
			{
				_sqlWhere += " AND 1<>1 ";
			}
			_sqlStr = " SELECT "
					+ " OS_ID,OS_NO,OS_DD,BAT_NO,CUS_NO,QT_NO,SAL_NO,USE_DEP,"
					+ " EST_DD,CUR_ID,TAX_ID,BIL_TYPE,CUS_OS_NO,"
					+ " CNTT_NO,REM,BIL_ID,"
					+ "	PAY_MTH,PAY_DAYS,CHK_DAYS,PAY_REM,SEND_MTH,SEND_WH,ADR,"//交易方式
					+ " DIS_CNT,AMTN_NET,FX_WH,YH_NO,PO_DEP,"
					+ " PAY_DD,CHK_DD,INT_DAYS,CLS_REM,USR,CLS_ID,PRT_SW,BIL_NO,CLS_DATE,CHK_MAN,EXC_RTO,BYBOX,TOT_BOX,TOT_QTY,SYS_DATE,ISOVERSH,HS_ID,HIS_PRICE,BACK_ID,"
					+ " INV_DIS_ID,CONTRACT,LOCK_MAN,"
					+ " PO_SO_NO,PRE_ID,(SELECT TOP 1 NAME FROM CUR_ID WHERE CUR_ID= MF_POS.CUR_ID) AS CUR_NAME,MOB_ID "	
					+ " FROM MF_POS "
					+ " WHERE 1=1 AND OS_ID=@OS_ID AND OS_NO=@OS_NO " + _sqlWhere
					+ " ; "
					+ " SELECT "
					+ " TF_POS.OS_ID,TF_POS.OS_NO,TF_POS.ITM,TF_POS.PRD_NO,TF_POS.PRD_NAME,TF_POS.PRD_MARK,TF_POS.WH,TF_POS.UNIT,"
                    + " TF_POS.QTY,TF_POS.UP,TF_POS.DIS_CNT,TF_POS.AMT,TF_POS.AMTN,TF_POS.TAX_RTO,TF_POS.TAX,TF_POS.PRE_ITM,TF_POS.QTY1,TF_POS.UP_QTY1,"
                    + " TF_POS.QTY_PS,TF_POS.EST_DD,TF_POS.CST_STD,TF_POS.QTY_PO,TF_POS.QTY_PO_UNSH,TF_POS.QTY_PRE,TF_POS.QTY_RK,TF_POS.OS_DD,"
					+ " TF_POS.BOX_ITM,TF_POS.REM,TF_POS.BIL_ID,TF_POS.EST_ITM,TF_POS.CUS_OS_NO,TF_POS.QT_NO,TF_POS.OTH_ITM,"
					+ " TF_POS.BAT_NO,TF_POS.VALID_DD,CONVERT(VARCHAR(1000),PRDT.SPC) AS SPC, "
                    + " TF_POS.SCM_USR,TF_POS.SCM_DD,TF_POS.QTY_SL,TF_POS.FREE_ID, "
                    + " TF_POS.PAK_UNIT,TF_POS.PAK_EXC,TF_POS.PAK_NW,TF_POS.PAK_WEIGHT_UNIT,TF_POS.PAK_GW,TF_POS.PAK_MEAST,TF_POS.PAK_MEAST_UNIT "
					+ " FROM TF_POS "
					+ " LEFT JOIN PRDT "
					+ " ON TF_POS.PRD_NO= PRDT.PRD_NO "
					+ " WHERE 1=1 AND TF_POS.OS_ID=@OS_ID AND TF_POS.OS_NO=@OS_NO " + _sqlWhere
					+ " ORDER BY ITM "
					+ " ; "
					+ " SELECT "
					+ "OS_ID,OS_NO,ITM,PRD_NO,WH,CONTENT,QTY,KEY_ITM,EST_DD,BIL_ID,EST_ITM "
					+ " FROM TF_POS1 "
					+ " WHERE 1=1 AND OS_ID=@OS_ID AND OS_NO=@OS_NO " + _sqlWhere
					+ " ORDER BY ITM ";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,_ds,new string[3]{"MF_POS","TF_POS","TF_POS_BOX"},_sqlPara);
			//去除as只读
			DataTable _dtMf = _ds.Tables["MF_POS"];
			DataTable _dtBody = _ds.Tables["TF_POS"];
			if (_dtMf.Columns.Contains("CUR_NAME"))
			{
				_dtMf.Columns["CUR_NAME"].ReadOnly = false;
			}
			if (_dtBody.Columns.Contains("SPC"))
			{
				_dtBody.Columns["SPC"].ReadOnly = false;
			}

			//建立主键
			DataColumn[] _pkmf_pos = new DataColumn[2];
			_pkmf_pos[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
			_pkmf_pos[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];

			DataColumn[] _pktf_pos = new DataColumn[3];
			_pktf_pos[0] = _ds.Tables["TF_POS"].Columns["OS_ID"];
			_pktf_pos[1] = _ds.Tables["TF_POS"].Columns["OS_NO"];
			_pktf_pos[2] = _ds.Tables["TF_POS"].Columns["ITM"];

			DataColumn[] _pktf_pos_box = new DataColumn[3];
			_pktf_pos_box[0] = _ds.Tables["TF_POS_BOX"].Columns["OS_ID"];
			_pktf_pos_box[1] = _ds.Tables["TF_POS_BOX"].Columns["OS_NO"];
			_pktf_pos_box[2] = _ds.Tables["TF_POS_BOX"].Columns["ITM"];


			_ds.Tables["MF_POS"].PrimaryKey = _pkmf_pos;
			_ds.Tables["TF_POS"].PrimaryKey = _pktf_pos;
			_ds.Tables["TF_POS_BOX"].PrimaryKey = _pktf_pos_box;


			//表头和表身关联
			DataColumn[] _dc1 = new DataColumn[2];
			_dc1[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
			_dc1[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];
			DataColumn[] _dc2 = new DataColumn[2];
			_dc2[0] = _ds.Tables["TF_POS"].Columns["OS_ID"];
			_dc2[1] = _ds.Tables["TF_POS"].Columns["OS_NO"];
			_ds.Relations.Add("MF_POSTF_POS",_dc1,_dc2);
			//表头和表身(箱)关联
			_dc1 = new DataColumn[2];
			_dc1[0] = _ds.Tables["MF_POS"].Columns["OS_ID"];
			_dc1[1] = _ds.Tables["MF_POS"].Columns["OS_NO"];
			_dc2 = new DataColumn[2];
			_dc2[0] = _ds.Tables["TF_POS_BOX"].Columns["OS_ID"];
			_dc2[1] = _ds.Tables["TF_POS_BOX"].Columns["OS_NO"];
			_ds.Relations.Add("MF_POSTF_POS_BOX",_dc1,_dc2);

			return _ds;
		}
		#region 取得表身数据
		/// <summary>
		/// 取得表身数据
		/// </summary>
		/// <param name="osId">单据别</param>
		/// <param name="osNo">单号</param>
		/// <param name="itmColumnName">项次名</param>
		/// <param name="preItm">项次值</param>
        /// <param name="isPrimaryUnit">是否转成主单位数量</param>
		/// <returns></returns>
		public SunlikeDataSet GetBody(string osId,string osNo,string itmColumnName,int preItm,bool isPrimaryUnit)
		{
			SqlParameter[] _sqlPara = new SqlParameter[3];
			_sqlPara[0] = new SqlParameter("@OS_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = osId;
			_sqlPara[1] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = osNo;
			_sqlPara[2] = new SqlParameter("@ITM",System.Data.SqlDbType.Int);
			_sqlPara[2].Value = preItm;
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
			string _sqlStr = "SELECT "
                        + " ITM,A.OS_ID,A.OS_NO,A.PRD_NO,A.PRD_NAME,A.PRD_MARK,A.WH" + _qtyStr + ",A.UP,A.DIS_CNT,A.AMT,A.AMTN,A.TAX_RTO,A.TAX,A.PRE_ITM,"
                        + " A.QTY_PS,A.EST_DD,A.CST_STD,A.QTY_PO,A.QTY_PO_UNSH,A.QTY_PRE,A.QTY_RK,A.OS_DD,A.BOX_ITM,A.REM,A.BIL_ID,A.EST_ITM,A.CUS_OS_NO,A.QT_NO,A.OTH_ITM "
						+ " FROM TF_POS A "
                        + _joinStr
                        + " WHERE A.OS_ID=@OS_ID AND A.OS_NO=@OS_NO AND A." + itmColumnName + "=@ITM ";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,_ds,new string[1]{"TF_POS"},_sqlPara);
			return _ds;
		}
		#endregion 
		#endregion

		#region 得到待确认采购单
		/// <summary>
		/// 得到待确认采购单
		/// </summary>
		/// <param name="usr">确认人</param>
		/// <returns></returns>
		public int GetScmNum(string usr)
		{
			int _scm = 0;
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@CUS_NO",SqlDbType.VarChar,12);
			_spc[0].Value = usr;

			string _sql = " SELECT COUNT(*) FROM MF_POS A WITH (NOLOCK)"
						 +" INNER JOIN TF_POS B ON A.OS_ID=B.OS_ID AND A.OS_NO=B.OS_NO "
						 +" WHERE A.CUS_NO=@CUS_NO AND A.OS_ID='PO' AND ISNULL(SCM_USR,'')='' "
						 +" AND (ISNULL(B.QTY_PS,0)=0 AND ISNULL(B.QTY_SL,0)=0 AND ISNULL(B.QTY_RK,0)=0)";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"TF_POS"},_spc);
			if(_ds.Tables.Contains("TF_POS"))
			{
				if(_ds.Tables["TF_POS"].Rows.Count > 0)
				{
					_scm = Convert.ToInt32(_ds.Tables["TF_POS"].Rows[0][0]);
				}
			}
			return _scm;
		}
		#endregion

		#region 确认/反确认采购单
		/// <summary>
		/// 确认/反确认采购单
		/// </summary>		
		/// <param name="poNoAry">采购单号</param>
		/// <param name="usr">确认人</param>
		/// <param name="scm">确认否</param>
		/// <returns></returns>
		public int UpdatePoScm(string[] poNoAry,string usr,bool scm)
		{
			string _sql = "UPDATE TF_POS SET SCM_USR=@SCM_USR,SCM_DD=@SCM_DD ";			
			if(!scm)
			{			
				_sql = "UPDATE TF_POS SET SCM_USR=null,SCM_DD=null ";
			}
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@SCM_USR",SqlDbType.VarChar,12);
			_spc[0].Value = usr;
			_spc[1] = new SqlParameter("@SCM_DD",SqlDbType.DateTime);
			_spc[1].Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			
			string _sqlWhere = " WHERE OS_ID='PO' AND (1<>1";
			
			if(poNoAry.Length > 0)
			{
				string[] _noItm = new string[2];
				for(int i = 0;i < poNoAry.Length;i++)
				{
					_noItm = poNoAry[i].Split(new char[]{';'});
					_sqlWhere += " OR (OS_NO='"+_noItm[0]+"' AND ITM='"+_noItm[1]+"' AND (ISNULL(QTY_PS,0)=0 AND ISNULL(QTY_SL,0)=0 AND ISNULL(QTY_RK,0)=0))";
				}				
			}
			_sqlWhere += ")";
			_sql += _sqlWhere;
			return this.ExecuteNonQuery(_sql,_spc);
		}
		#endregion

		#region 入库回写
		/// <summary>
		/// 入库回写
		/// </summary>
		/// <param name="osId">单据别</param>
		/// <param name="osNo">单号</param>
		/// <param name="itm">项次</param>
		/// <param name="qty">数量</param>
		public void SetFromTi(string osId, string osNo, string itm, decimal qty)
		{
			string _sql = "UPDATE TF_POS SET QTY_RK = ISNULL(QTY_RK, 0) + @QTY WHERE OS_ID = @OS_ID AND OS_NO = @OS_NO AND ITM = @ITM";
			SqlParameter[] _aryPt = new SqlParameter[4];
			_aryPt[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
			_aryPt[0].Value = osId;
			_aryPt[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
			_aryPt[1].Value = osNo;
			_aryPt[2] = new SqlParameter("@QTY", SqlDbType.Decimal);
			_aryPt[2].Precision = 28;
			_aryPt[2].Scale = 8;
			_aryPt[2].Value = qty;
			_aryPt[3] = new SqlParameter("@ITM", SqlDbType.Int);
			_aryPt[3].Value = Convert.ToInt32(itm);
			this.ExecuteNonQuery(_sql, _aryPt);
		}
		#endregion

		#region 采购单审核
		/// <summary>
		/// 采购单审核
		/// </summary>
		/// <param name="osId">单据别</param>
		/// <param name="osNo">单号</param>
		/// <param name="chkMan">审核人（反审核时传入空值）</param>
		/// <param name="chkDD">审核日期</param>
		public void	UpdateChkMan(string osId,string osNo,string chkMan,DateTime chkDD)
		{
			string _sqlStr = "";
			_sqlStr = "UPDATE MF_POS SET CHK_MAN=@CHK_MAN,CLS_DATE=@CHK_DD,CHK_DD=@CHK_DD WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO";
			SqlParameter[] _sqlPara = new SqlParameter[4];
			_sqlPara[0] = new SqlParameter("@OS_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = osId;
			_sqlPara[1] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = osNo;
			_sqlPara[2] = new SqlParameter("@CHK_MAN",SqlDbType.VarChar,12);			
			_sqlPara[3] = new SqlParameter("@CHK_DD",System.Data.SqlDbType.DateTime);
			if (String.IsNullOrEmpty(chkMan))
			{
				_sqlPara[2].Value = System.DBNull.Value;
				_sqlPara[3].Value =  System.DBNull.Value;
			}
			else
			{
				_sqlPara[2].Value = chkMan;
				_sqlPara[3].Value =  chkDD;
			}
			this.ExecuteNonQuery(_sqlStr,_sqlPara);					
		}
		#endregion

		#region 修改修改采购单的已退数量
		/// <summary>
		/// 修改修改采购单的已退数量
		/// </summary>
		/// <param name="osId"></param>
		/// <param name="osNo"></param>
		/// <param name="itm"></param>
		/// <param name="qtyPre"></param>
		public string UpdateQtyPre(string osId,string osNo,int itm,decimal qtyPre)
		{
			string _sql = "	update TF_POS set QTY_PRE=isNull(QTY_PRE,0)+@QTY where OS_ID=@OS_ID and OS_NO=@OS_NO and PRE_ITM=@ITM \n"
				+ "		if Exists(select OS_NO from TF_POS WHERE OS_ID=@OS_ID and OS_NO=@OS_NO and ( (isnull(QTY,0)-isnull(QTY_PRE,0)) > isnull(QTY_PS,0) )) \n"
				+ "			update MF_POS set CLS_ID='F',BACK_ID=NULL where OS_ID=@OS_ID and OS_NO=@OS_NO AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T') \n"//手工结案的单据不能更改
				+ "		else \n"
				+ "			update MF_POS set CLS_ID='T',BACK_ID='PR' where OS_ID=@OS_ID and OS_NO=@OS_NO AND (ISNULL(BACK_ID,'')<>'' OR ISNULL(CLS_ID,'F')<>'T') \n"//手工结案的单据不能更改
				+ "	select 0\n";
			SqlParameter[] _sqlPara = new SqlParameter[4];
			_sqlPara[0] = new SqlParameter("@OS_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = osId;
			_sqlPara[1] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = osNo;
			_sqlPara[2] = new SqlParameter("@ITM",SqlDbType.Int);
			_sqlPara[2].Value = itm;
			_sqlPara[3] = new SqlParameter("@QTY",SqlDbType.Decimal);
			_sqlPara[3].Precision = 38;
			_sqlPara[3].Scale = 10;
            _sqlPara[3].Value = qtyPre;
            string _result = "2";
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _sqlPara))
            //{
            //    if (_sdr.Read())
            //    {
            //        _result = _sdr[0].ToString();
            //    }
            //}
            object _rObj = this.ExecuteScalar(_sql, _sqlPara);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
			return _result;
		}
		#endregion

		#region 判断是否手工结案
		/// <summary>
		/// 判断是否手工结案
		/// </summary>
		/// <param name="osId"></param>
		/// <param name="osNo"></param>
		/// <returns></returns>
		public bool IsCloseByUsr(string osId,string osNo)
		{
			bool _result = false;
			SunlikeDataSet _ds = new SunlikeDataSet();
			string _sqlStr = "SELECT OS_ID FROM MF_POS WHERE OS_ID=@OS_ID AND OS_NO=@OS_NO AND ISNULL(BACK_ID,'')='' AND ISNULL(CLS_ID,'')='T'";
			SqlParameter [] _sqlPara = new SqlParameter[2];
			_sqlPara[0] = new SqlParameter("@OS_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = osId;
			_sqlPara[1] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = osNo;
			this.FillDataset(_sqlStr,_ds,new string[1] {"MF_POS"},_sqlPara);
			if (_ds != null && _ds.Tables["MF_POS"].Rows.Count > 0 )
			{
				_result = true;	
			}
			return _result;
		}
		#endregion

		#region 结案
		/// <summary>
		/// 在单据上打上结案标记
		/// </summary>
		/// <param name="osId"></param>
		/// <param name="osNo"></param>
		/// <param name="close"></param>
		public bool DoCloseSO(string osId,string osNo,bool close)
		{
			string _where = "";
			bool _result = false;
			_where = " OS_ID=@OS_ID AND OS_NO=@OS_NO ";
			string _sqlStr = "";
			_sqlStr = " UPDATE MF_POS SET CLS_ID=@CLS_ID WHERE "+_where;
			SqlParameter[] _sqlPara = new SqlParameter[3];
			_sqlPara[0] = new SqlParameter("@OS_ID",SqlDbType.VarChar,2);
			_sqlPara[0].Value = osId;
			_sqlPara[1] = new SqlParameter("@OS_NO",SqlDbType.VarChar,20);
			_sqlPara[1].Value = osNo;
			_sqlPara[2] = new SqlParameter("@CLS_ID",SqlDbType.VarChar,1);
			_sqlPara[2].Value = close.ToString().Substring(0,1);
			try
			{
				this.ExecuteNonQuery(_sqlStr,_sqlPara);				
				_result = false;
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _result;

		}

		#endregion

        #region 修改修改采购单的已采购数量
        /// <summary>
        /// 修改修改采购单的已采购数量
        /// </summary>
        /// </summary>
        /// <param name="osId"></param>
        /// <param name="osNo"></param>
        /// <param name="itmColumnName"></param>
        /// <param name="itm"></param>
        /// <param name="qtyColumnName"></param>
        /// <param name="qty"></param>
        public string UpdateQty(string osId, string osNo, string itmColumnName, int itm,string qtyColumnName, decimal qty)
        {
            string _sql = "	update TF_POS set " + qtyColumnName + "=isNull(" + qtyColumnName + ",0)+@QTY where OS_ID=@OS_ID and OS_NO=@OS_NO and " + itmColumnName + "=@ITM ";
            SqlParameter[] _sqlPara = new SqlParameter[4];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;
            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;
            _sqlPara[2] = new SqlParameter("@ITM", SqlDbType.Int);
            _sqlPara[2].Value = itm;
            _sqlPara[3] = new SqlParameter("@QTY", SqlDbType.Decimal);
            _sqlPara[3].Precision = 38;
            _sqlPara[3].Scale = 10;
            _sqlPara[3].Value = qty;
            string _result = "2";          
            object _rObj = this.ExecuteScalar(_sql, _sqlPara);
            if (_rObj != null)
            {
                _result = _rObj.ToString();
            }
            return _result;
        }
      
        #endregion

        public SunlikeDataSet GetJDData(string osNo, string osId, int estItm)
        {
            string _sqlStr = "SELECT * FROM TF_JD WHERE OS_NO=@OS_NO AND OS_ID=@OS_ID AND EST_ITM=@EST_ITM";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;
            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;
            _sqlPara[2] = new SqlParameter("@EST_ITM", SqlDbType.Int);
            _sqlPara[2].Value = estItm;

            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sqlStr, _ds, new string[] { "TF_JD" }, _sqlPara);
            return _ds;
        }

        public void DeleteTfJD(string osNo, string osId, int estItm)
        {
            string _sqlStr = "IF EXISTS (SELECT 1 FROM TF_JD WHERE OS_NO=@OS_NO AND OS_ID=@OS_ID AND EST_ITM=@EST_ITM) \r\n"
                + "DELETE FROM TF_JD WHERE OS_NO=@OS_NO AND OS_ID=@OS_ID AND EST_ITM=@EST_ITM";
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@OS_ID", SqlDbType.VarChar, 2);
            _sqlPara[0].Value = osId;
            _sqlPara[1] = new SqlParameter("@OS_NO", SqlDbType.VarChar, 20);
            _sqlPara[1].Value = osNo;
            _sqlPara[2] = new SqlParameter("@EST_ITM", SqlDbType.Int);
            _sqlPara[2].Value = estItm;

            this.ExecuteNonQuery(_sqlStr, _sqlPara);
        }
	}

}
