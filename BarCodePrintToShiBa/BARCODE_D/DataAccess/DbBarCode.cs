using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Sunlike.Common.CommonVar;
using System.Collections.Generic;
using System.Text;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// Summary description for DbBarCode.
	/// </summary>
	public class DbBarCode : DbObject
	{
		private string ConnectionString;

		#region 序列号
		/// <summary>
		/// 序列号
		/// </summary>
		/// <param name="connectionString"></param>
		public DbBarCode(string connectionString) : base(connectionString)
		{
			ConnectionString = connectionString;
		}
		#endregion

		#region 取得序列号编码原则
		/// <summary>
		/// 取得序列号编码原则
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetBarRole()
		{
			string _sql = "select ITM,BAR_LEN,BOX_FLAG,BOXPOS,S_PRDT,E_PRDT,B_PMARK,E_PMARK,B_SN,E_SN,END_CHAR,TRIM_CHAR,BOX_LEN,LEGAL_CH from BAR_ROLE";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[] {"BAR_ROLE"});
			return _ds;
		}
		#endregion

		#region 取得序列号记录
		/// <summary>
		/// 取得序列号记录
		/// </summary>
		/// <param name="SqlWhere">条件语句，EX：BAR_NO='001' and BAR_NO='002'</param>
		/// <param name="HasStop">是否包含停用的序列号</param>
		/// <param name="dataSetStop">记录已停用的序列号</param>
		/// <returns></returns>
		public DataTable GetBarRecord(string SqlWhere,bool HasStop, DataSet dataSetStop)
		{
			string _sql = "select BAR_NO,BOX_NO,WH,UPDDATE,STOP_ID,SPC_NO,PRD_NO,PRD_MARK,BAT_NO,PH_FLAG from BAR_REC where 1=1";
			string _sqlStop = _sql;
			if (!HasStop)
			{
				_sql += " and isnull(STOP_ID,'F')<>'T'";
				if (dataSetStop != null)
				{
					//记录已停用序列号
					_sqlStop += " and STOP_ID = 'T'";
					if (!String.IsNullOrEmpty(SqlWhere.Trim()))
					{
						_sqlStop += " and (" + SqlWhere + ")";
					}
					this.FillDataset(_sqlStop, dataSetStop, new string[1]{"BAR_REC_STOP"});
				}
			}
			if (!String.IsNullOrEmpty(SqlWhere.Trim()))
			{
				_sql += " and (" + SqlWhere + ")";
			}
			DataSet _ds = new DataSet();
			this.FillDataset(_sql, _ds, new string[] {"BAR_REC"});
			return _ds.Tables[0];
		}
		#endregion

		#region 取得箱条码资料
		/// <summary>
		/// 
		/// </summary>
		/// <param name="BoxNo"></param>
		/// <param name="OnlyFillSchema"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBoxData(string BoxNo, bool OnlyFillSchema)
		{
            string _sql = "select A.WH,A.BOX_NO,A.QTY,A.BB_DD,A.UB_DD,A.DEP,A.USR,A.BX_ID,B.NAME as DEP_NAME,C.NAME as WH_NAME,A.BATCH_NO,A.SYS_DATE\n"
				+ " from MF_BOX A\n"
				+ " left join DEPT B on B.DEP=A.DEP\n"
				+ " left join MY_WH C on C.WH=A.WH\n"
				+ " where BOX_NO=@BoxNo and ISNULL(QTY,0)>0;"
				+ "select BOX_NO,QTY,CONTENT,USR,WH,PRD_NO,STOP_ID\n"
				+ " from BAR_BOX\n"
				+ " where BOX_NO=@BoxNo;\n"
				+ "select BAR_NO,BOX_NO,WH,UPDDATE,SPC_NO,PRD_NO,PRD_MARK,STOP_ID,BAT_NO,PH_FLAG from BAR_REC where BOX_NO=@BoxNo\n";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[1];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@BoxNo",SqlDbType.VarChar,90);
			_spc[0].Value = BoxNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (OnlyFillSchema)
			{
				this.FillDatasetSchema(_sql,_ds,new string[] {"MF_BOX","BAR_BOX","BAR_REC"},_spc);
			}
			else
			{
				this.FillDataset(_sql,_ds,new string[] {"MF_BOX","BAR_BOX","BAR_REC"},_spc);
			}
			//设定PK，因为用了left join以后，PK会取不到
			DataColumn[] _dca = new DataColumn[2];
			_dca[0] = _ds.Tables["MF_BOX"].Columns["BOX_NO"];
			_dca[1] = _ds.Tables["MF_BOX"].Columns["WH"];
			_ds.Tables["MF_BOX"].PrimaryKey = _dca;

			DataColumn[] _dca1 = new DataColumn[1];
			_dca1[0] = _ds.Tables["BAR_BOX"].Columns["BOX_NO"];			
			_ds.Tables["BAR_BOX"].PrimaryKey = _dca1;

			DataColumn[] _dca2 = new DataColumn[1];			
			_dca2[0] = _ds.Tables["BAR_REC"].Columns["BAR_NO"];
			_ds.Tables["BAR_REC"].PrimaryKey = _dca2;
			//创建Relation
			DataRelation _dr = new DataRelation("MF_BOXBAR_BOX",_ds.Tables["MF_BOX"].Columns["BOX_NO"],_ds.Tables["BAR_BOX"].Columns["BOX_NO"]);
			_ds.Relations.Add(_dr);
			_dr = new DataRelation("MF_BOXBAR_REC",_ds.Tables["MF_BOX"].Columns["BOX_NO"],_ds.Tables["BAR_REC"].Columns["BOX_NO"]);
			_ds.Relations.Add(_dr);

			return _ds;
		}

		/// <summary>
		/// 取得箱条码表资料
		/// </summary>
		/// <param name="SqlWhere">条件语句，EX：BOX_NO='001' or BOX_NO='002'</param>
		/// <returns></returns>
		public DataTable GetBoxData(string SqlWhere)
		{
			string _sql = "select BOX_NO,CONTENT,PRD_NO,WH,STOP_ID,PH_FLAG from BAR_BOX";
			if (!String.IsNullOrEmpty(SqlWhere.Trim()))
			{
				_sql += " where " + SqlWhere;
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[] {"BAR_BOX"});
			return _ds.Tables[0];
		}
		/// <summary>
		/// 取得箱条码表资料
		/// </summary>
		/// <param name="SqlWhere"></param>
		/// <param name="compNo"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBoxDataFind(string SqlWhere,string compNo)
		{
			string _sql = " SELECT A.*,B.CUS_NO,C.NAME AS CUS_NAME,D.NAME AS USR_NAME,E.CONTENT,E.PH_FLAG,E.STOP_ID,E.PRD_NO "
				+" FROM MF_BOX A "
				+" LEFT JOIN MY_WH B ON A.WH=B.WH"
				+" LEFT JOIN CUST C ON C.CUS_NO=B.CUS_NO"
				+" LEFT JOIN SUNSYSTEM..PSWD D ON A.USR=D.USR AND D.COMPNO='"+compNo+"' "
				+" INNER JOIN BAR_BOX E ON A.BOX_NO=E.BOX_NO ";
			if (!String.IsNullOrEmpty(SqlWhere.Trim()))
			{
				_sql += " where " + SqlWhere;
			}
			_sql += " and isnull(A.qty,0)=1 ";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[] {"BAR_BOX"});
			return _ds;
		}

		/// <summary>
		/// 根据BOX_NO取得BAR_BOX信息
		/// </summary>
		/// <param name="pBoxNo">箱条码</param>
		/// <returns></returns>
		public DataTable GetBoxInfo(string pBoxNo)
		{
			string _sqlString = "SELECT BB.*,PT.NAME AS PRD_NAME FROM"
				+ " BAR_BOX AS BB"
				+ " LEFT JOIN"
				+ " (SELECT PRD_NO,NAME FROM PRDT) AS PT"
				+ " ON BB.PRD_NO = PT.PRD_NO"
				+ " WHERE BB.BOX_NO=@BOX_NO";
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@BOX_NO", SqlDbType.VarChar, 90);
			_pt[0].Value = pBoxNo;
			DataTable _dt = new DataTable();
			_dt = this.ExecuteDataset(_sqlString,_pt).Tables[0];
			return _dt;
		}
		#endregion

		#region 拆箱
		/// <summary>
		/// 拆箱
		/// </summary>
		/// <param name="SqlWhere">SQL条件语句</param>
		/// <param name="usr">用户代号</param>
		/// <param name="bilID">单据类别</param>
		/// <param name="bilNo">来源单号</param>
		public void DeleteBox(string SqlWhere,string usr,string bilID,string bilNo)
		{
			//把条件语句分段
			System.Collections.ArrayList _alBar1 = new System.Collections.ArrayList();
			System.Collections.ArrayList _alBar2 = new System.Collections.ArrayList();
			int _maxWhereLength = 20480;
			string _subWhere;
			int _pos;
			string _where = SqlWhere;
			while (true)
			{
				if (_where.Length > _maxWhereLength)
				{
					_subWhere = _where.Substring(0,_maxWhereLength-1);
					_pos = _subWhere.LastIndexOf(",");
					_alBar1.Add(_subWhere.Substring(0,_pos) + ")");
					_where = "BOX_NO in (" + _where.Substring(_pos+1,_where.Length-_pos-1);
				}
				else
				{
					_alBar1.Add(_where);
					break;
				}
			}
			_maxWhereLength = 2048;
			_where = SqlWhere;
			while (true)
			{
				if (_where.Length > _maxWhereLength)
				{
					_subWhere = _where.Substring(0,_maxWhereLength-1);
					_pos = _subWhere.LastIndexOf(",");
					_alBar2.Add(_subWhere.Substring(0,_pos) + ")");
					_where = "BOX_NO in (" + _where.Substring(_pos+1,_where.Length-_pos-1);
				}
				else
				{
					_alBar2.Add(_where);
					break;
				}
			}
			for (int i=0;i<_alBar2.Count;i++)
			{
				this.ExecuteNonQuery("insert into BOX_CHANGE"
					+ " select A.BOX_NO,A.BAR_NO,convert(varchar(20),getdate(),120),B.WH,'"
					+ usr + "','" + bilID + "','" + bilNo + "'"
					+ " from BAR_REC A"
					+ " inner join BAR_BOX B on B.BOX_NO=A.BOX_NO"
					+ " where A." + _alBar2[i].ToString());
			}
			for (int i=0;i<_alBar1.Count;i++)
			{
				this.ExecuteNonQuery("update BAR_BOX set STOP_ID='T' where " + _alBar1[i].ToString());
			}
			for (int i=0;i<_alBar2.Count;i++)
			{
				this.ExecuteNonQuery("update BAR_REC set BOX_NO=null where " + _alBar2[i].ToString());
			}
		}
		#endregion

		#region 取装箱主库资料
		/// <summary>
		/// 取装箱主库资料
		/// </summary>
		/// <param name="wh">库位</param>
		/// <param name="boxNoAry">箱条码集合</param>
		/// <returns></returns>
		public SunlikeDataSet GetMfBox(string wh,string[] boxNoAry)
		{
			int _per = 100;
			int _count = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(boxNoAry.Length) / Convert.ToDouble(_per)));
			SunlikeDataSet _ds = new SunlikeDataSet();
			for (int i=0;i<_count;i++)
			{
                StringBuilder _sql = new StringBuilder("select WH,BOX_NO,QTY,BB_DD,UB_DD,DEP,USR");
				_sql.Append(" from MF_BOX");
                _sql.Append(" where WH=@Wh and BOX_NO in (");
				for (int j=0;j<_per;j++)
				{
					int _no = j + i * _per;
					if (_no < boxNoAry.Length)
					{
						if (j > 0)
						{
							_sql.Append(",");
						}
						_sql.Append("'" + boxNoAry[_no] + "'");
					}
					else
					{
						break;
					}
				}
				_sql.Append(")");
				SqlParameter[] _spa = new SqlParameter[1];
				_spa[0] = new SqlParameter("@Wh",SqlDbType.VarChar,12);
				_spa[0].Value = wh;
				SunlikeDataSet _dsTemp = new SunlikeDataSet();
				this.FillDataset(_sql.ToString(),_dsTemp,new string[] {"MF_BOX"},_spa);
				_ds.Merge(_dsTemp,false,MissingSchemaAction.AddWithKey);
			}
			return _ds;
		}
		#endregion

		#region BOX_QTY
		/// <summary>
		/// 取得装箱数量
		/// </summary>
		/// <param name="Itm">项次</param>
		/// <returns></returns>
		public DataTable GetBoxQty(int Itm)
		{
			string _sql = "select ITM,QTY from BOX_QTY";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[1];
			if (Itm > 0)
			{
				_sql += " where ITM=@Itm";
				_spc[0] = new System.Data.SqlClient.SqlParameter("@Itm",SqlDbType.SmallInt);
				_spc[0].Value = Itm;
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (Itm > 0)
			{
				this.FillDataset(_sql,_ds,new string[] {"BOX_QTY"},_spc);
			}
			else
			{
				this.FillDataset(_sql,_ds,new string[] {"BOX_QTY"});
			}
			return _ds.Tables[0];
		}

		/// <summary>
		/// 新增装箱数量设定
		/// </summary>
		/// <param name="Qty">数量</param>
		public string InsertBoxQty(int Qty)
		{
			string _sql = "declare @Itm smallint\n"
				+ "set @Itm=(select isnull(max(ITM),0) from BOX_QTY)+1\n"
				+ "insert into BOX_QTY (ITM,QTY) values (@Itm,@Qty)\n"
				+ "select @Itm";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[1];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@Qty",SqlDbType.Int);
            _spc[0].Value = Qty;
            //using (System.Data.SqlClient.SqlDataReader _sdr = this.ExecuteReader(_sql, _spc))
            //{
            //    if (_sdr.Read())
            //    {
            //        _result = _sdr[0].ToString();
            //    }
            //}
			return this.ExecuteScalar(_sql, _spc).ToString();
		}

		/// <summary>
		/// 保存装箱数量设定
		/// </summary>
		/// <param name="Itm">项次</param>
		/// <param name="Qty">数量</param>
		public void UpdateBoxQty(int Itm,int Qty)
		{
			string _sql = "update BOX_QTY set QTY=@Qty where ITM=@Itm";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[2];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@Qty",SqlDbType.Int);
			_spc[0].Value = Qty;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@Itm",SqlDbType.SmallInt);
			_spc[1].Value = Itm;
			this.ExecuteNonQuery(_sql,_spc);
		}

		/// <summary>
		/// 删除装箱数量
		/// </summary>
		/// <param name="Itm">项次</param>
		public void DeleteBoxQty(int Itm)
		{
			string _sql = "delete from BOX_QTY where ITM=@Itm";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[1];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@Itm",SqlDbType.SmallInt);
			_spc[0].Value = Itm;
			this.ExecuteNonQuery(_sql,_spc);
		}
		#endregion

		#region 取装箱条码
		/// <summary>
		/// 取装箱条码
		/// </summary>
		/// <param name="prd_No">货品代号</param>
		/// <param name="content">装箱配码比</param>
		/// <returns></returns>
		public DataTable GetBar_Box_No(string prd_No , string content)
		{
			SqlParameter[] _pt = new SqlParameter[2];
			_pt[0] = new SqlParameter("@PRD_NO" , SqlDbType.VarChar , 30);
			_pt[0].Value = prd_No;
			_pt[1] = new SqlParameter("@CONTENT" , SqlDbType.VarChar , 255);
			_pt[1].Value = content;
			return this.ExecuteDataset("SELECT TOP 1 BOX_NO,QTY FROM BAR_BOX WHERE PRD_NO = @PRD_NO AND CONTENT = @CONTENT" , _pt).Tables[0];
		}
		#endregion
		
		#region 取箱条码内容
		/// <summary>
		/// 取箱条码内容
		/// </summary>
		/// <param name="box_No"></param>
		/// <returns></returns>
		public DataTable GetBar_Box(string box_No)
		{
			DataTable _dt = null;
			SqlParameter[] _pt = new SqlParameter[1];
			_pt[0] = new SqlParameter("@BOX_NO" , SqlDbType.VarChar , 90);
			_pt[0].Value = box_No;
			_dt = this.ExecuteDataset("SELECT BAR_BOX.BOX_NO, BAR_BOX.QTY, BAR_BOX.PRD_NO, BAR_BOX.CONTENT, PRDT.SNM, PRDT.NAME, ISNULL(PRDT.UT,'') AS UT, ISNULL(PRDT.SPC, '') AS SPC, ISNULL(MY_WH.WH, '0000') AS WH, ISNULL(MY_WH.NAME, '') AS WH_NAME FROM BAR_BOX INNER JOIN PRDT ON BAR_BOX.PRD_NO = PRDT.PRD_NO LEFT OUTER JOIN MY_WH ON BAR_BOX.WH = MY_WH.WH WHERE BAR_BOX.BOX_NO = @BOX_NO" , _pt).Tables[0];
			return _dt;
		}

		/// <summary>
		/// 取得配码比
		/// </summary>
		/// <param name="prdNo"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public string GetBarBoxDsc(string prdNo, string content)
		{
			if(content.IndexOf(";") == -1)
			{
				content += ";";
			}
			SqlParameter[] _ptAry = new SqlParameter[2];
			_ptAry[0] = new SqlParameter("@CONTENT", SqlDbType.VarChar, 255);
			_ptAry[0].Value = content;
			_ptAry[1] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
			_ptAry[1].Value = prdNo;
			string _strSql = "SELECT dbo.fn_GetContentDsc(@PRD_NO,@CONTENT)";
			DataTable _dt = this.ExecuteDataset(_strSql, _ptAry).Tables[0];
			if (_dt.Rows.Count > 0)
			{
				return _dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}
		#endregion

		#region BOX_SET
		/// <summary>
		/// 取得配码比规则
		/// </summary>
		/// <param name="BxID">配码比代号</param>
		/// <returns></returns>
		public DataTable GetBoxSet(string BxID)
		{
			string _sql = "select A.BX_ID,A.REM,A.QTY,A.USR,A.IDX1,A.IDX2,A.IDX3,A.STOP_ID,A.DEP,B.NAME as DEP_NAME"
				+ " from BOX_SET A"
				+ " left join DEPT B on B.DEP=A.DEP";
			SunlikeDataSet _ds = new SunlikeDataSet();
			if (!String.IsNullOrEmpty(BxID))
			{
				_sql += " where A.BX_ID=@BxID";
				System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[1];
				_spc[0] = new System.Data.SqlClient.SqlParameter("@BxID",SqlDbType.VarChar,8);
				_spc[0].Value = BxID;
				this.FillDataset(_sql,_ds,new string[] {"BOX_SET"},_spc);
			}
			else
			{
				this.FillDataset(_sql,_ds,new string[] {"BOX_SET"});
			}
			return _ds.Tables[0];
		}

		/// <summary>
		/// 取得配码比规则
		/// </summary>
		/// <param name="PrdNo">货品代号</param>
		/// <param name="Qty">装箱数量</param>
		/// <param name="Dep">部门代号</param>
		/// <returns></returns>
		public DataTable GetBoxSet(string PrdNo,int Qty,string Dep)
		{
			string _sql = "select A.BX_ID,A.REM from BOX_SET A"
				+ " inner join PRDT B on B.PRD_NO=@PrdNo"
				+ " where isnull(B.IDX1,'')=isnull(A.IDX1,isnull(B.IDX1,'')) and A.QTY=@Qty"
				+ " and isnull(A.STOP_ID,'F')<>'T' and (isnull(A.DEP,'')='' or isnull(A.DEP,'')=@Dep)";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[3];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@PrdNo",SqlDbType.VarChar,30);
			_spc[0].Value = PrdNo;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@Qty",SqlDbType.SmallInt);
			_spc[1].Value = Qty;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@Dep",SqlDbType.VarChar,8);
			_spc[2].Value = Dep;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[] {"BOX_SET"},_spc);
			return _ds.Tables[0];
		}

		/// <summary>
		/// 增加配码比规则
		/// </summary>
		/// <param name="BxID">配码比代号</param>
		/// <param name="Rem">配码比限制描述</param>
		/// <param name="Qty">装箱数量</param>
		/// <param name="Usr">编码人员</param>
		/// <param name="Idx1">中类代号1</param>
		/// <param name="Dep">部门代号</param>
		public string InsertBoxSet(string BxID,string Rem,int Qty,string Usr,string Idx1,string Dep)
		{
			string _sql = "if exists (select * from BOX_SET where REM=@Rem)\n"
				+ "	select 1\n"
				+ "else\n"
				+ "begin\n"
				+ "	insert into BOX_SET (BX_ID,REM,QTY,USR,IDX1,DEP) values (@BxID,@Rem,@Qty,@Usr,@Idx1,@Dep)\n"
				+ "	select 0\n"
				+ "end";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[6];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@BxID",SqlDbType.VarChar,8);
			_spc[0].Value = BxID;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@Rem",SqlDbType.VarChar,255);
			_spc[1].Value = Rem;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@Qty",SqlDbType.SmallInt);
			_spc[2].Value = Qty;
			_spc[3] = new System.Data.SqlClient.SqlParameter("@Usr",SqlDbType.VarChar,12);
			_spc[3].Value = Usr;
			_spc[4] = new System.Data.SqlClient.SqlParameter("@Idx1",SqlDbType.VarChar,10);
			if (String.IsNullOrEmpty(Idx1))
			{
				_spc[4].Value = System.DBNull.Value;
			}
			else
			{
				_spc[4].Value = Idx1;
			}
			_spc[5] = new System.Data.SqlClient.SqlParameter("@Dep",SqlDbType.VarChar,8);
			_spc[5].Value = Dep;
			string _result = "2";
            //using (SqlDataReader _sdr = this.ExecuteReader(_sql, _spc))
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

		/// <summary>
		/// 更新配码比规则
		/// </summary>
		/// <param name="BxID">配码比代号</param>
		/// <param name="Rem">配码比限制描述</param>
		/// <param name="Qty">装箱数量</param>
		/// <param name="Usr">编码人员</param>
		/// <param name="Idx1">中类代号1</param>
		/// <param name="Dep">部门代号</param>
		/// <param name="StopID">停用否</param>
		public void UpdateBoxSet(string BxID,string Rem,int Qty,string Usr,string Idx1,string Dep,string StopID)
		{
			string _sql = "update BOX_SET set REM=@Rem,QTY=@Qty,USR=@Usr,IDX1=@Idx1,DEP=@Dep,STOP_ID=@StopID where BX_ID=@BxID";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[7];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@BxID",SqlDbType.VarChar,8);
			_spc[0].Value = BxID;
			_spc[1] = new System.Data.SqlClient.SqlParameter("@Rem",SqlDbType.VarChar,255);
			_spc[1].Value = Rem;
			_spc[2] = new System.Data.SqlClient.SqlParameter("@Qty",SqlDbType.SmallInt);
			_spc[2].Value = Qty;
			_spc[3] = new System.Data.SqlClient.SqlParameter("@Usr",SqlDbType.VarChar,12);
			_spc[3].Value = Usr;
			_spc[4] = new System.Data.SqlClient.SqlParameter("@Idx1",SqlDbType.VarChar,10);
			if (String.IsNullOrEmpty(Idx1))
			{
				_spc[4].Value = System.DBNull.Value;
			}
			else
			{
				_spc[4].Value = Idx1;
			}
			_spc[5] = new System.Data.SqlClient.SqlParameter("@Dep",SqlDbType.VarChar,8);
			_spc[5].Value = Dep;
			_spc[6] = new System.Data.SqlClient.SqlParameter("@StopID",SqlDbType.Char,1);
			_spc[6].Value = StopID;
			this.ExecuteNonQuery(_sql,_spc);
		}

		/// <summary>
		/// 删除配码比规则
		/// </summary>
		/// <param name="BxID">配码比代号</param>
		public void DeleteBoxSet(string BxID)
		{
			string _sql = "delete from BOX_SET where BX_ID=@BxID";
			System.Data.SqlClient.SqlParameter[] _spc = new System.Data.SqlClient.SqlParameter[1];
			_spc[0] = new System.Data.SqlClient.SqlParameter("@BxID",SqlDbType.VarChar,8);
			_spc[0].Value = BxID;
			this.ExecuteNonQuery(_sql,_spc);
		}
		#endregion
		
		#region BAR_BOXSTAT
		/// <summary>
		/// GetStat
		/// </summary>
		/// <param name="pWh"></param>
		/// <param name="pCtn"></param>
		/// <param name="pPrdNo"></param>
		/// <param name="pSqlPow"></param>
		/// <returns></returns>
		public DataTable GetStat(string pWh, string pCtn, string pPrdNo, string pSqlPow)
		{
			SqlParameter[] _pt = new SqlParameter[3];
			_pt[0] = new SqlParameter("@WH" , SqlDbType.VarChar, 30);
			_pt[0].Value = pWh;
			_pt[1] = new SqlParameter("@CTN" , SqlDbType.VarChar, 255);
			_pt[1].Value = pCtn;
			_pt[2] = new SqlParameter("@PRD_NO", SqlDbType.VarChar, 30);
			_pt[2].Value = pPrdNo;

//			string _sql = "SELECT BB.*,MW.WH_NAME,PT.PRD_NAME FROM "
//				+ " (SELECT WH,PRD_NO,ISNULL(SUM(QTY),0) AS QTY,CONTENT,COUNT(*) AS BOX_QTY "
//				+ " FROM BAR_BOX B"
//				+ " WHERE (PH_FLAG <> 'T') "
//				+ _where + " " + pSqlPow
//				+ " GROUP BY WH,PRD_NO,CONTENT) AS BB "
//				+ " LEFT JOIN "
//				+ " (SELECT WH, NAME AS WH_NAME FROM MY_WH) AS MW "
//				+ " ON BB.WH = MW.WH "
//				+ " LEFT JOIN "
//				+ " (SELECT PRD_NO, NAME AS PRD_NAME FROM PRDT) AS PT "
//				+ " ON BB.PRD_NO = PT.PRD_NO";
			string _sql = " SELECT A.*,(ISNULL(QTY,0)-ISNULL(QTY_ON_ODR,0)) AS USR_QTY,B.NAME AS WH_NAME,C.NAME AS PRD_NAME FROM PRDT1_BOX A "
					    + " LEFT JOIN MY_WH B ON A.WH=B.WH  "
					    + " LEFT JOIN PRDT C ON A.PRD_NO=C.PRD_NO WHERE A.WH = @WH AND A.CONTENT = @CTN AND A.PRD_NO = @PRD_NO";			
			DataTable _dt = this.ExecuteDataset(_sql,_pt).Tables[0];
			return _dt;
		}

		/// <summary>
		/// GetBarCtn
		/// </summary>
		/// <param name="pContent"></param>
		/// <param name="pQty"></param>
		/// <param name="pWh"></param>
		/// <param name="pPrdNo"></param>
		/// <returns></returns>
		public DataTable GetBarCtn(string pContent, string pQty, string pWh, string pPrdNo)
		{
			SqlParameter[] _pt = new SqlParameter[4];
			_pt[0] = new SqlParameter("@CONTENT" , SqlDbType.VarChar);
			_pt[0].Value = pContent;
			_pt[1] = new SqlParameter("@QTY" , SqlDbType.VarChar);
			_pt[1].Value = pQty;
			_pt[2] = new SqlParameter("@WH" , SqlDbType.VarChar);
			_pt[2].Value = pWh;
			_pt[3] = new SqlParameter("@PRD_NO", SqlDbType.VarChar);
			_pt[3].Value = pPrdNo;
			string _where = "";
			if (!String.IsNullOrEmpty(pContent))
			{
				_where += " AND CONTENT like '%"+pContent+"%'";
			}
			if (!String.IsNullOrEmpty(pQty))
			{
				_where += " AND QTY = @QTY";
			}
			if (!String.IsNullOrEmpty(pPrdNo))
			{
				_where += " AND PRD_NO = @PRD_NO";
			}
			_where += " AND WH = @WH";
			string _sql = "SELECT WH,PRD_NO,CONTENT,QTY,QTY_ON_ODR,QTY_MAX,QTY_MIN FROM PRDT1_BOX WHERE 1=1 " + _where;
			DataTable _dt = this.ExecuteDataset(_sql,_pt).Tables[0];
			return _dt;
		}

		/// <summary>
		/// 取得总箱数
		/// </summary>
		/// <param name="sqlWhere"></param>
		/// <returns></returns>
		public int GetStatQty(string sqlWhere)
		{
			string _sql = "SELECT SUM((ISNULL(QTY,0)-ISNULL(QTY_ON_ODR,0))) AS USR_QTY"
				+ " FROM"
				+ " PRDT1_BOX WHERE 1=1 "
				+ sqlWhere;
			DataTable _dt = this.ExecuteDataset(_sql).Tables[0];
			if (_dt.Rows.Count >0)
			{
				if (_dt.Rows[0]["USR_QTY"] != System.DBNull.Value)
				{
					return Convert.ToInt32(_dt.Rows[0]["USR_QTY"]);
				}
				else
				{
					return 0;
				}
			}
			else
			{
				return 0;
			}
		}
		#endregion

		#region 取得箱内容
		/// <summary>
		/// 取得箱内容
		/// </summary>
		/// <param name="prd_no">货品代号</param>
		/// <param name="wh">仓库代号</param>
		/// <param name="Content">配码比</param>
		/// <returns></returns>
		public DataTable SelectBar_box(string prd_no,string wh,string Content)
		{
			string _sqlStr = "SELECT BOX_NO,BOX_DD,DEP,QTY,CONTENT,USR,WH,PRD_NO,PH_FLAG FROM BAR_BOX WHERE 1=1 AND PRD_NO=@PRD_NO AND WH=@WH and CONTENT=@Content";
			SqlParameter[] _sqlPara = new SqlParameter[3];
			_sqlPara[0] = new SqlParameter("@PRD_NO",SqlDbType.VarChar,20);
			_sqlPara[0].Value = prd_no;
			_sqlPara[1] = new SqlParameter("@WH",SqlDbType.VarChar,12);
			_sqlPara[1].Value = wh;
			_sqlPara[2] = new SqlParameter("@Content",SqlDbType.VarChar,255);
			_sqlPara[2].Value = Content;

			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,_ds,new string [] {"BAR_BOX"},_sqlPara);
			return _ds.Tables["BAR_BOX"];
		}
		#endregion

		#region 取得序列号转入格式
		/// <summary>
		/// 取得序列号转入格式
		/// </summary>
		/// <param name="SqlWhere">条件语句</param>
		/// <returns></returns>
		public DataTable GetBarDoc(string SqlWhere)
		{
			string _sql = "select DOC_NO,BAR_POS,WH_POS,WH2_POS,BAT_POS,CUS_POS,DEF_ID,SPLIT_CHAR from BAR_DOC" + SqlWhere;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[] {"BAR_DOC"});
			return _ds.Tables["BAR_DOC"];
		}
		#endregion
		
		#region 序列号追踪 by db
		/// <summary>
		/// 序列号追踪
		/// </summary>
		/// <param name="BarCodeTrail">序列号号码组合</param>
		/// <param name="compNo"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarCodeTrail(string BarCodeTrail,string compNo)
		{
			StringBuilder _barCodeSql = new StringBuilder();
			string _sql = " SELECT A.BAR_NO,A.BOX_NO,A.WH,A.UPDDATE,A.PH_FLAG,A.STOP_ID,A.SPC_NO,C.NAME AS SPC_NAME,B.CUS_NO AS CUS_NO,B.NAME AS CUS_NAME,F.NAME AS USR_NAME FROM BAR_REC A WITH (READPAST) "
						+ " LEFT JOIN BAR_SPC C ON A.SPC_NO=C.SPC_NO "
						+ " LEFT JOIN MY_WH D ON A.WH=D.WH "
						+ " LEFT JOIN CUST B ON B.CUS_NO=D.CUS_NO "
						+ " LEFT JOIN BAR_BOX E ON A.BOX_NO=E.BOX_NO "
						+ " LEFT JOIN SUNSYSTEM..PSWD F ON F.COMPNO='"+compNo+"' AND F.USR=E.USR "
						+ " WHERE 1<>1 ";
			string[] _barCodeAry = BarCodeTrail.Split(new char[]{';'});
			if (!String.IsNullOrEmpty(BarCodeTrail.Trim()))
			{
				if (_barCodeAry.Length > 0)
				{
					for (int i = 0; i < _barCodeAry.Length; i++)
					{
						_barCodeSql.Append(" OR A.BAR_NO = '"+_barCodeAry[i].Trim()+"'");
					}
				}
			}
			_sql += _barCodeSql.ToString();
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"BAR_REC"});
			return _ds;
		}
		#endregion 		

		#region 得到序列号追踪明细记录 by db
		/// <summary>
		/// 得到序列号追踪明细记录
		/// </summary>
		/// <param name="Bar_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarCodeDetailList(string Bar_No)
		{
			SqlParameter[] _sqlPara = new SqlParameter[1];
			_sqlPara[0] = new SqlParameter("@BAR_NO",SqlDbType.VarChar,90);
			_sqlPara[0].Value = Bar_No;

			//配送/调拨
			string _SqlIc = "SELECT 3 AS ITM,A.IC_ID AS ID,A.IC_NO AS NO,C.IC_DD AS DD,C.DEP,G.NAME AS DEP_NAME,B.BAT_NO,B.BAT_NO2,"
                + " B.WH1,E.NAME AS WH1_NAME,B.WH2,F.NAME AS WH2_NAME,B.REM,C.USR,C.SYS_DATE,A.BOX_NO,0.00 AS QTY,C.OUTDEP,H.NAME AS OUTDEP_NAME,(CASE C.CFM_SW WHEN 'T' THEN 'T' ELSE '' END) AS PH_FLAG "
				+ " FROM TF_IC1 A WITH (READPAST)"
				+ " inner JOIN TF_IC B ON A.IC_ID=B.IC_ID AND A.IC_NO=B.IC_NO AND A.IC_ITM=B.KEY_ITM"
				+ " inner JOIN MF_IC C ON A.IC_ID=C.IC_ID AND A.IC_NO=C.IC_NO AND ISNULL(C.CHK_MAN,'')<>''"
                + " inner JOIN MY_WH E ON B.WH1=E.WH"
                + " inner JOIN MY_WH F ON B.WH2=F.WH"
				+ " LEFT JOIN DEPT G ON C.DEP=G.DEP"
                + " LEFT JOIN DEPT H ON C.OUTDEP=H.DEP"
				+ " WHERE A.BAR_CODE=@BAR_NO ";
			//销货/销退/进货/进退
            string _SqlBar = "SELECT 4 AS ITM,A.PS_ID AS ID,A.PS_NO AS NO,C.PS_DD AS DD,C.DEP,G.NAME AS DEP_NAME,B.BAT_NO,'' AS BAT_NO2,"
                + " B.WH AS WH1,E.NAME AS WH1_NAME,'' AS WH2,'' AS WH2_NAME,B.REM,C.USR,C.SYS_DATE,A.BOX_NO,0.00 AS QTY,'' AS OUTDEP,'' AS OUTDEP_NAME,'' AS PH_FLAG "
				+ " FROM TF_PSS3 A WITH (READPAST)"
				+ " inner JOIN TF_PSS B ON A.PS_ID=B.PS_ID AND A.PS_NO=B.PS_NO AND A.PS_ITM=B.PRE_ITM"
				+ " inner JOIN MF_PSS C ON A.PS_ID=C.PS_ID AND A.PS_NO=C.PS_NO AND ISNULL(C.CHK_MAN,'')<>''"
				+ " inner JOIN MY_WH E ON B.WH=E.WH"
                + " LEFT JOIN DEPT G ON C.DEP=G.DEP"
				+ " WHERE A.BAR_CODE=@BAR_NO";
			//调整
            string _sqlIj = "SELECT 2 AS ITM,A.IJ_ID AS ID,A.IJ_NO AS NO,D.IJ_DD AS DD,C.DEP,G.NAME AS DEP_NAME,B.BAT_NO,"
                + " B.WH AS WH1,C.NAME AS WH1_NAME,B.WH AS WH2,C.NAME AS WH2_NAME,B.REM,D.USR,D.SYS_DATE,A.BOX_NO,ISNULL(B.QTY,0.00) AS QTY,'' AS OUTDEP,'' AS OUTDEP_NAME,'' AS PH_FLAG "
				+ " FROM TF_IJ1 A WITH (READPAST)"
				+ " inner JOIN TF_IJ B ON A.IJ_ID=B.IJ_ID AND A.IJ_NO=B.IJ_NO AND A.IJ_ITM=B.KEY_ITM"
				+ " inner JOIN MY_WH C ON B.WH=C.WH"
				+ " inner JOIN MF_IJ D ON A.IJ_ID=D.IJ_ID AND A.IJ_NO=D.IJ_NO AND ISNULL(D.CHK_MAN,'')<>'' "
                + " LEFT JOIN DEPT G ON D.DEP=G.DEP"
				+ " WHERE A.BAR_CODE=@BAR_NO";
			//库位调整
			string _sqlBarChange = " SELECT 0 AS ITM,'BG' AS ID,BIL_NO AS NO,A.UPDDATE AS DD,'' AS DEP,'' AS DEP_NAME,A.BAT_NO2 AS BAT_NO,A.BAT_NO2,"
                + " A.WH1,B.NAME AS WH1_NAME,A.WH2,C.NAME AS WH2_NAME,'' AS REM,A.USR,A.UPDDATE AS SYS_DATE,'' AS BOX_NO,0.00 AS QTY,'' AS OUTDEP,'' AS OUTDEP_NAME,A.PH_FLAG2 AS PH_FLAG "
				+ " FROM BAR_CHANGE A WITH (READPAST)"
				+ " left JOIN MY_WH B ON A.WH1=B.WH "
                + " left JOIN MY_WH C ON A.WH2=C.WH "
                + " inner JOIN BAR_REC I ON A.BAR_NO=I.BAR_NO"
                + " WHERE A.BAR_NO=@BAR_NO ";
            //到货确认
            string _sqlIz = "SELECT 5 AS ITM,A.IZ_ID AS ID,A.IZ_NO AS NO,D.IZ_DD AS DD,C.DEP,G.NAME AS DEP_NAME,B.BAT_NO,"
                + " B.WH AS WH1,C.NAME AS WH1_NAME,B.WH AS WH2,C.NAME AS WH2_NAME,B.REM,D.USR,D.SYS_DATE,A.BOX_NO,ISNULL(B.QTY,0.00) AS QTY,'' AS OUTDEP,'' AS OUTDEP_NAME,'T' AS PH_FLAG "
                + " FROM TF_IZ1 A WITH (READPAST)"
                + " inner JOIN TF_IZ B ON A.IZ_ID=B.IZ_ID AND A.IZ_NO=B.IZ_NO AND A.IZ_ITM=B.KEY_ITM"
                + " inner JOIN MY_WH C ON B.WH=C.WH"
                + " inner JOIN MF_IZ D ON A.IZ_ID=D.IZ_ID AND A.IZ_NO=D.IZ_NO AND ISNULL(D.CHK_MAN,'')<>'' "
                + " LEFT JOIN DEPT G ON D.DEP=G.DEP"
                + " WHERE A.BAR_CODE=@BAR_NO";
			//装箱
            string _sqlBarCode = "SELECT 1 AS ITM,'ZX' AS ID,'' AS NO,B.BB_DD AS DD,B.DEP,G.NAME AS DEP_NAME,A.BAT_NO,'' AS BAT_NO2,"
				//+ " A.WH AS WH1,C.NAME AS WH1_NAME,'' AS WH2,'' AS WH2_NAME,'' AS REM,B.USR,"
				+ " '' AS WH1,'' AS WH1_NAME,'' AS WH2,'' AS WH2_NAME,'' AS REM,B.USR,"
                + " B.BB_DD AS SYS_DATE,A.BOX_NO,0.00 AS QTY,'' AS OUTDEP,'' AS OUTDEP_NAME,'' AS PH_FLAG "
				+ " FROM BAR_REC A WITH (READPAST)"
				+ " INNER JOIN MF_BOX B ON A.BOX_NO=B.BOX_NO AND A.WH=B.WH "
				//+ " LEFT JOIN MY_WH C ON A.WH=C.WH "
                + " LEFT JOIN DEPT G ON B.DEP=G.DEP"
				+ " WHERE A.BAR_NO=@BAR_NO ";
            //借进/还出
            string _sqlBn = "SELECT 6 AS ITM,A.BL_ID AS ID,A.BL_NO AS NO,C.BL_DD AS DD,C.DEP,G.NAME AS DEP_NAME,B.BAT_NO,'' AS BAT_NO2,"
                + " B.WH AS WH1,E.NAME AS WH1_NAME,'' AS WH2,'' AS WH2_NAME,B.REM,C.USR,C.SYS_DATE,A.BOX_NO,0.00 AS QTY,'' AS OUTDEP,'' AS OUTDEP_NAME,'' AS PH_FLAG "
				+ " FROM TF_BLN_B A WITH (READPAST)"
                + " inner JOIN TF_BLN B ON A.BL_ID=B.BL_ID AND A.BL_NO=B.BL_NO AND A.BL_ITM=B.PRE_ITM"
                + " inner JOIN MF_BLN C ON A.BL_ID=C.BL_ID AND A.BL_NO=C.BL_NO AND ISNULL(C.CHK_MAN,'')<>''"
				+ " inner JOIN MY_WH E ON B.WH=E.WH"
                + " LEFT JOIN DEPT G ON C.DEP=G.DEP"
				+ " WHERE A.BAR_CODE=@BAR_NO";


			SunlikeDataSet _ds = new SunlikeDataSet();
			SunlikeDataSet _ds1 = new SunlikeDataSet();
			SunlikeDataSet _ds2 = new SunlikeDataSet();
            SunlikeDataSet _ds3 = new SunlikeDataSet();
            SunlikeDataSet _ds4 = new SunlikeDataSet();
            SunlikeDataSet _ds5 = new SunlikeDataSet();
            SunlikeDataSet _ds6 = new SunlikeDataSet();

			this.FillDataset(_SqlIc, _ds, new string[]{"BAR"},_sqlPara);
			this.FillDataset(_SqlBar, _ds1, new string[]{"BAR"},_sqlPara);
			this.FillDataset(_sqlIj, _ds2, new string[]{"BAR"},_sqlPara);
            this.FillDataset(_sqlBarChange, _ds3, new string[] { "BAR" }, _sqlPara);
            this.FillDataset(_sqlBarCode, _ds4, new string[] { "BAR" }, _sqlPara);
            this.FillDataset(_sqlIz, _ds5, new string[] { "BAR" }, _sqlPara);
            this.FillDataset(_sqlBn, _ds6, new string[] { "BAR" }, _sqlPara);

			_ds.Merge(_ds1);
			_ds.Merge(_ds2);
			_ds.Merge(_ds3);
			_ds.Merge(_ds4);
            _ds.Merge(_ds6);

			return _ds;
		}
		/// <summary>
		/// 得到序列号追踪明细记录
		/// </summary>
		/// <param name="Box_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarBoxDetailList(string Box_No)
		{
			SqlParameter[] _sqlPara = new SqlParameter[1];
			_sqlPara[0] = new SqlParameter("@BOX_NO",SqlDbType.VarChar,90);
			_sqlPara[0].Value = Box_No;

			//配送/调拨
			string _sqlIc = "SELECT distinct A.IC_ID AS ID,A.IC_NO AS NO,1 AS ITM,C.IC_DD AS DD,B.WH1,E.NAME AS WH1_NAME,"
						+ " B.WH2,F.NAME AS WH2_NAME,B.REM,C.DEP,ISNULL(G.NAME,'') AS DEP_NAME,A.BOX_NO,C.USR,C.SYS_DATE,C.OUTDEP,H.NAME AS OUTDEP_NAME "
						+ " FROM (SELECT IC_ID,IC_NO,IC_ITM,ITM,BOX_NO FROM TF_IC1 WITH (READPAST) WHERE BOX_NO=@BOX_NO) A"
						+ " inner JOIN TF_IC B ON A.IC_ID=B.IC_ID AND A.IC_NO=B.IC_NO AND A.IC_ITM=B.KEY_ITM"
						+ " inner JOIN MF_IC C ON A.IC_ID=C.IC_ID AND A.IC_NO=C.IC_NO AND ISNULL(C.CHK_MAN,'')<>''"
						+ " inner JOIN MY_WH E ON B.WH1=E.WH"
						+ " inner JOIN MY_WH F ON B.WH2=F.WH"
						+ " LEFT JOIN DEPT G ON G.DEP=C.DEP "
						+ " LEFT JOIN DEPT H ON H.DEP=C.OUTDEP ";
			//销货/销退/进货/进退
			string _SqlBar = "SELECT distinct A.PS_ID AS ID,A.PS_NO AS NO,2 AS ITM,C.PS_DD AS DD,B.WH AS WH1,E.NAME AS WH1_NAME,"
				+ " '' AS WH2,'' AS WH2_NAME,B.REM,C.DEP,G.NAME AS DEP_NAME,A.BOX_NO,C.USR,C.SYS_DATE,'' AS OUTDEP,'' AS OUTDEP_NAME "
				+ " FROM TF_PSS3 A WITH (READPAST)"
				+ " inner JOIN TF_PSS B ON A.PS_ID=B.PS_ID AND A.PS_NO=B.PS_NO AND A.PS_ITM=B.PRE_ITM"
				+ " inner JOIN MF_PSS C ON A.PS_ID=C.PS_ID AND A.PS_NO=C.PS_NO AND ISNULL(C.CHK_MAN,'')<>''"
				+ " inner JOIN MY_WH E ON B.WH=E.WH"
				+ " LEFT JOIN DEPT G ON C.DEP=G.DEP"
				+ " WHERE A.BOX_NO=@BOX_NO";
			//调整
			string _sqlIj = "SELECT A.IJ_ID AS ID,A.IJ_NO AS NO,1 AS ITM,D.IJ_DD AS DD,B.WH AS WH1,C.NAME AS WH1_NAME,"
						+ " B.WH AS WH2,C.NAME AS WH2_NAME,B.REM,D.DEP,ISNULL(E.NAME,'') AS DEP_NAME,A.BOX_NO,D.USR,D.SYS_DATE,'' AS OUTDEP,'' AS OUTDEP_NAME,ISNULL(B.QTY,0.00) AS QTY "
						+ " FROM (SELECT IJ_ID,IJ_NO,IJ_ITM,ITM,BOX_NO FROM TF_IJ1 WITH (READPAST) WHERE BOX_NO=@BOX_NO) A"
						+ " inner JOIN TF_IJ B ON A.IJ_ID=B.IJ_ID AND A.IJ_NO=B.IJ_NO AND A.IJ_ITM=B.KEY_ITM"
						+ " inner JOIN MY_WH C ON B.WH=C.WH "
						+ " inner JOIN MF_IJ D ON A.IJ_ID=D.IJ_ID AND A.IJ_NO=D.IJ_NO AND ISNULL(D.CHK_MAN,'')<>'' "
						+ " LEFT JOIN DEPT E ON E.DEP=D.DEP ";
			//装箱
			string _sqlBc = " SELECT 'ZX' AS ID,'' AS NO,0 AS ITM,A.BB_DD AS DD,'' AS WH1,'' AS WH1_NAME,"				
				+ " '' AS WH2,'' AS WH2_NAME,'' AS REM,A.DEP,ISNULL(C.NAME,'') AS DEP_NAME,"
				+ " A.BOX_NO,A.USR,A.UB_DD AS SYS_DATE,'' AS OUTDEP,'' AS OUTDEP_NAME  "
				+ " FROM MF_BOX A WITH (READPAST) "				
				+ " LEFT JOIN DEPT C ON A.DEP=C.DEP "
				+ " WHERE A.BOX_NO=@BOX_NO AND ISNULL(A.UB_DD,'')=''";
				//+ " WHERE A.BOX_NO=@BOX_NO AND ISNULL(A.BB_DD,'')<>'' AND ISNULL(A.UB_DD,'')=''";
			//拆箱
			string _sqlBc1 = " SELECT 'BO' AS ID,D.BIL_NO AS NO,0 AS ITM,A.UB_DD AS DD,'' AS WH1,'' AS WH1_NAME,"
						  +" A.WH AS WH2,ISNULL(B.NAME,'') AS WH2_NAME,'' AS REM,A.DEP,ISNULL(C.NAME,'') AS DEP_NAME,A.BOX_NO,A.USR,A.UB_DD AS SYS_DATE,'' AS OUTDEP,'' AS OUTDEP_NAME  "
						  +" FROM MF_BOX A WITH (READPAST) "
						  +" LEFT JOIN MY_WH B ON B.WH=A.WH "
						  +" LEFT JOIN DEPT C ON A.DEP=C.DEP "
						  +" INNER JOIN (SELECT DISTINCT BOX_NO,UPDDATE,WH,USR,BIL_ID,BIL_NO FROM BOX_CHANGE) D ON A.BOX_NO=D.BOX_NO"
						  + " WHERE A.BOX_NO=@BOX_NO AND ISNULL(A.UB_DD,'')<>'' ";
//			string _sqlBc1 = "SELECT TOP 1 A.BIL_ID AS ID,A.BIL_NO AS NO,0 AS ITM,A.UPDDATE AS DD,'' AS WH1,'' AS WH1_NAME,"
//						+" A.WH AS WH2,ISNULL(B.NAME,'') AS WH2_NAME,'' AS REM,"
//						+" ''AS DEP,'' AS DEP_NAME,A.BOX_NO,A.USR,A.UPDDATE AS SYS_DATE,'' AS OUTDEP,'' AS OUTDEP_NAME  "
//						+" FROM BOX_CHANGE A"
//						+" LEFT JOIN MY_WH B ON B.WH=A.WH"
//						+" WHERE A.BOX_NO=@BOX_NO";

            //借进/还出
            string _SqlBn = "SELECT distinct A.BL_ID AS ID,A.BL_NO AS NO,2 AS ITM,C.BL_DD AS DD,B.WH AS WH1,E.NAME AS WH1_NAME,"
                + " '' AS WH2,'' AS WH2_NAME,B.REM,C.DEP,G.NAME AS DEP_NAME,A.BOX_NO,C.USR,C.SYS_DATE,'' AS OUTDEP,'' AS OUTDEP_NAME "
                + " FROM TF_BLN_B A WITH (READPAST)"
                + " inner JOIN TF_BLN B ON A.BL_ID=B.BL_ID AND A.BL_NO=B.BL_NO AND A.BL_ITM=B.PRE_ITM"
                + " inner JOIN MF_BLN C ON A.BL_ID=C.BL_ID AND A.BL_NO=C.BL_NO AND ISNULL(C.CHK_MAN,'')<>''"
                + " inner JOIN MY_WH E ON B.WH=E.WH"
                + " LEFT JOIN DEPT G ON C.DEP=G.DEP"
                + " WHERE A.BOX_NO=@BOX_NO";

			SunlikeDataSet _ds = new SunlikeDataSet();
			SunlikeDataSet _ds1 = new SunlikeDataSet();			

			this.FillDataset(_sqlIc,_ds,new string[]{"IC"},_sqlPara);
			#region 删除重复记录
			DataTable _dtIc = _ds.Tables["IC"].Clone();
			if(_ds.Tables["IC"].Rows.Count > 0)
			{
				for(int i = 0;i< _ds.Tables["IC"].Rows.Count;i++)
				{
					DataRow[] _rowAry = _dtIc.Select(" ID='"+_ds.Tables["IC"].Rows[i]["ID"].ToString()+"' AND NO='"+_ds.Tables["IC"].Rows[i]["NO"].ToString()+"'");
					if(_rowAry.Length > 0)
					{
						continue;
					}
					else
					{
						DataRow _row = _dtIc.NewRow();
						for(int j = 0;j < _dtIc.Columns.Count;j++)
						{
							_row[j] = _ds.Tables["IC"].Rows[i][j];
						}
						_dtIc.Rows.Add(_row);
					}
				}
				_ds.Tables.Remove("IC");
				_ds.Tables.Add(_dtIc);
			}
			#endregion

			this.FillDataset(_sqlIj,_ds,new string[]{"IJ"},_sqlPara);
			#region 删除重复记录
			DataTable _dtIj = _ds.Tables["IJ"].Clone();
			if(_ds.Tables["IJ"].Rows.Count > 0)
			{
				for(int i = 0;i< _ds.Tables["IJ"].Rows.Count;i++)
				{
					DataRow[] _rowAry = _dtIj.Select(" ID='"+_ds.Tables["IJ"].Rows[i]["ID"].ToString()+"' AND NO='"+_ds.Tables["IJ"].Rows[i]["NO"].ToString()+"'");
					if(_rowAry.Length > 0)
					{
						continue;
					}
					else
					{
						DataRow _row = _dtIj.NewRow();
						for(int j = 0;j < _dtIj.Columns.Count;j++)
						{
							_row[j] = _ds.Tables["IJ"].Rows[i][j];
						}
						if(Convert.ToDecimal(_row["QTY"]) <= 0)
						{
							_row["WH2"] = "";
							_row["WH2_NAME"] = "";
						}
						else
						{
							_row["WH1"] = "";
							_row["WH1_NAME"] = "";
						}
						_dtIj.Rows.Add(_row);
					}
				}				
				_ds.Tables.Remove("IJ");
				_ds.Tables.Add(_dtIj);
			}
			#endregion
			_ds.Tables["IJ"].Columns.Remove("QTY");

			this.FillDataset(_SqlBar,_ds,new string[]{"PSS"},_sqlPara);
			#region 删除重复记录
			DataTable _dtPss = _ds.Tables["PSS"].Clone();
			if(_ds.Tables["PSS"].Rows.Count > 0)
			{
				for(int i = 0;i< _ds.Tables["PSS"].Rows.Count;i++)
				{
					DataRow[] _rowAry = _dtPss.Select(" ID='"+_ds.Tables["PSS"].Rows[i]["ID"].ToString()+"' AND NO='"+_ds.Tables["PSS"].Rows[i]["NO"].ToString()+"'");
					if(_rowAry.Length > 0)
					{
						continue;
					}
					else
					{
						DataRow _row = _dtPss.NewRow();
						for(int j = 0;j < _dtPss.Columns.Count;j++)
						{
							_row[j] = _ds.Tables["PSS"].Rows[i][j];
						}
						_dtPss.Rows.Add(_row);
					}
				}
				_ds.Tables.Remove("PSS");
				_ds.Tables.Add(_dtPss);
			}
			#endregion

            this.FillDataset(_SqlBn, _ds, new string[] { "BLN" }, _sqlPara);
            #region 删除重复记录
            DataTable _dtBn = _ds.Tables["BLN"].Clone();
            if (_ds.Tables["BLN"].Rows.Count > 0)
            {
                for (int i = 0; i < _ds.Tables["BLN"].Rows.Count; i++)
                {
                    DataRow[] _rowAry = _dtBn.Select(" ID='" + _ds.Tables["BLN"].Rows[i]["ID"].ToString() + "' AND NO='" + _ds.Tables["BLN"].Rows[i]["NO"].ToString() + "'");
                    if (_rowAry.Length > 0)
                    {
                        continue;
                    }
                    else
                    {
                        DataRow _row = _dtBn.NewRow();
                        for (int j = 0; j < _dtBn.Columns.Count; j++)
                        {
                            _row[j] = _ds.Tables["BLN"].Rows[i][j];
                        }
                        _dtBn.Rows.Add(_row);
                    }
                }
                _ds.Tables.Remove("BLN");
                _ds.Tables.Add(_dtBn);
            }
            #endregion

			this.FillDataset(_sqlBc,_ds1,new string[]{"MF_BOX"},_sqlPara);
			this.FillDataset(_sqlBc1,_ds,new string[]{"MF_BOX"},_sqlPara);
			_ds.Merge(_ds1);

			return _ds;
		}
		#endregion

		#region 取得bar_role
		/// <summary>
		/// 取得bar_role
		/// </summary>
		/// <returns></returns>
		public DataTable SelectBar_role()
		{
			string _sqlStr = "";
			_sqlStr = "SELECT ITM,BAR_LEN,BOX_FLAG,BOXPOS,S_PRDT,E_PRDT,B_PMARK,E_PMARK,B_SN,E_SN,END_CHAR,TRIM_CHAR,BOX_LEN FROM BAR_ROLE";
			SunlikeDataSet _ds = new SunlikeDataSet();
			try
			{
				this.FillDataset(_sqlStr,_ds,new string [] {"BAR_ROLE"});
			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _ds.Tables[0];
		}
		#endregion

		#region 运行外面传进来的TableName语句
		/// <summary>
		/// 
		/// </summary>
		/// <param name="TableName"></param>
		/// <param name="SqlWhere"></param>
		/// <returns></returns>
		public SunlikeDataSet RunSqlForBox(string TableName,string SqlWhere)
		{			
			string _sqlStr = "SELECT * FROM " + TableName;
			_sqlStr += " " + SqlWhere;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sqlStr,_ds,new string [] {TableName});
			return _ds;
		}
		#endregion
		
		#region 得到箱编码原则
		/// <summary>
		/// 得到箱编码原则
		/// </summary>
		/// <param name="Dep">部门</param>
		/// <returns></returns>
		public SunlikeDataSet GetBoxConfig(string Dep)
		{
			SqlParameter[] _sqlPara = new SqlParameter[1];
			_sqlPara[0] = new SqlParameter("@DEP",SqlDbType.VarChar,8);
			_sqlPara[0].Value = Dep;

			string _sql = "SELECT * FROM BOX_CONFIG WHERE DEP=@DEP";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string [] {"BOX_CONFIG"},_sqlPara);
			return _ds;
		}
		#endregion

		#region 取待打印序列号SPC by db
		/// <summary>
		/// 取待打印序列号SPC
		/// </summary>
		/// <param name="BarNo"></param>
		/// <returns></returns>
		public string GetBarPrintSpc(string BarNo)
		{
			SqlParameter[] _sqlPara = new SqlParameter[1];
			_sqlPara[0] = new SqlParameter("@BAR_NO",SqlDbType.VarChar,90);
			_sqlPara[0].Value = BarNo;

			string _spc = "";
			string _sql = " SELECT ISNULL(SPC_NO,'') AS SPC_NO FROM BAR_PRINT WHERE BAR_NO=@BAR_NO UNION "
						+ " SELECT ISNULL(SPC_NO,'') AS SPC_NO FROM BAR_REC WHERE BAR_NO=@BAR_NO";
			DataTable _dt = this.ExecuteDataset(_sql,_sqlPara).Tables[0];
			if (_dt.Rows.Count > 0)
			{
				_spc = _dt.Rows[0]["SPC_NO"].ToString();
			}
			return _spc;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="SqlWh"></param>
		/// <returns></returns>
		public DataTable GetBarPrintSpcForDel(string SqlWh)
		{		
			string _sql = "SELECT * FROM BAR_PRINT " + SqlWh;
			DataTable _dt = this.ExecuteDataset(_sql).Tables[0];
			_dt.TableName = "BAR_PRINT";
			return _dt;
		}
		#endregion

		#region 查看BAR_REC中是否存在BAR_NO的记录
		/// <summary>
		/// 查看BAR_REC中是否存在BAR_NO的记录
		/// </summary>
		/// <param name="Wh"></param>
		/// <param name="DtBarRec"></param>
		/// <returns></returns>
		public DataTable GetBarRec(string Wh,DataTable DtBarRec)
		{
			StringBuilder _sqlWhere = new StringBuilder(" WHERE ISNULL(WH,'') = '"+Wh+"' AND ISNULL(BOX_NO,'')='' AND ISNULL(STOP_ID,'')<>'T' ");
			if(DtBarRec.Rows.Count > 0)
			{
				_sqlWhere.Append(" AND (");
				for(int i = 0; i < DtBarRec.Rows.Count; i++)
				{
					_sqlWhere.Append(" BAR_NO='"+DtBarRec.Rows[i]["BAR_NO"].ToString()+"' OR ");
				}
				 _sqlWhere.Append(" 1<>1)");
			}
			string _sql = "SELECT * FROM BAR_REC " + _sqlWhere.ToString();
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"BAR_REC"});
			DataTable _dt = _ds.Tables["BAR_REC"];
			return _dt;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Wh"></param>
		/// <param name="RowBarRec"></param>
		/// <returns></returns>
		public DataTable GetBarRec(string Wh,DataRow[] RowBarRec)
		{
			StringBuilder _sqlWhere = new StringBuilder(" WHERE ISNULL(WH,'') = '"+Wh+"' AND ISNULL(BOX_NO,'')='' AND ISNULL(STOP_ID,'')<>'T' ");
			if(RowBarRec.Length > 0)
			{
				_sqlWhere.Append(" AND (");
				for(int i = 0; i < RowBarRec.Length; i++)
				{
					_sqlWhere.Append(" BAR_NO='"+RowBarRec[i]["BAR_NO"].ToString()+"' OR ");
				}
                _sqlWhere.Append(" 1<>1)");
			}
			string _sql = "SELECT BAR_NO FROM BAR_REC " + _sqlWhere.ToString();
			DataTable _dt = this.ExecuteDataset(_sql).Tables[0];
			return _dt;
		}
		#endregion

		#region 得到要下载到本地的文件(离线装箱)
		/// <summary>
		/// 得到要下载到本地的文件(离线装箱)
		/// </summary>
		/// <param name="Ht"></param>
		/// <returns></returns>
        public SunlikeDataSet GetBarBoxToLocal(Dictionary<String, Object> Ht)
		{
			#region 条件
			int _pageCount = Convert.ToInt32(Ht["PAGECOUNT"]);
			string _tableName = Ht["TABLENAME"].ToString();
			string BDate = Ht["BDATE"].ToString();
			string EDate = Ht["EDATE"].ToString();
			string SqlPrint = Ht["SQLPRINT"].ToString();
			string Sql = Ht["SQL"].ToString();
			string Bar_Print = Ht["BAR_PRINT"].ToString();
			string Bar_Rec = Ht["BAR_REC"].ToString();
			string Dep = Ht["BAR_DEP"].ToString();
			if(Dep == "########")
			{
				Dep = "00000000"; 
			}
			string BarChk = Ht["BAR_CHK"].ToString();
			string RecDate = "";
			string PrintDate = "";
			if(BarChk == "T")
			{
				if(Ht.ContainsKey("REC_DATE"))
				{
					RecDate = Ht["REC_DATE"].ToString();
				}
				if(Ht.ContainsKey("PRINT_DATE"))
				{
					PrintDate = Ht["PRINT_DATE"].ToString();
				}
			}

			string _sqlPrint = "SELECT * FROM BAR_PRINT WHERE 1=1 ";
			string _sqlSpc = " SELECT * FROM BAR_SPC";
			string _sqlRec = "SELECT * FROM BAR_REC WHERE 1=1 ";
			#endregion

			#region 组SQL
			if(!String.IsNullOrEmpty(BDate.Trim()))
			{
				_sqlPrint += " AND PRN_DD>='"+BDate+"' ";
				_sqlRec += " AND UPDDATE>='"+BDate+"' ";
			}
			if(!String.IsNullOrEmpty(EDate.Trim()))
			{
				_sqlPrint += " AND PRN_DD<='"+EDate+"' ";
				_sqlRec += " AND UPDDATE<='"+EDate+"' ";
			}
			if(!String.IsNullOrEmpty(SqlPrint.Trim()))
			{
				_sqlPrint += " " + SqlPrint;
			}
			if(!String.IsNullOrEmpty(Sql.Trim()))
			{
				_sqlRec +=  " " + Sql;
			}
			if(Bar_Print == "F")
			{
				_sqlPrint += " AND 1<>1 ";
			}
			if(Bar_Rec == "F")
			{
				_sqlRec += " AND 1<>1 ";
			}
			if( Dep != "A" )
			{
				_sqlPrint += " AND DEP='"+Dep+"' ";
			}
			if(!String.IsNullOrEmpty(RecDate))
			{
				_sqlRec += " AND UPDDATE >= '"+RecDate+"' ";
			}
			if(!String.IsNullOrEmpty(PrintDate))
			{
				_sqlPrint += " AND PRN_DD >= '"+PrintDate+"' ";
			}
			#endregion

			#region 调用存储过程			
			#endregion

			SunlikeDataSet _ds = new SunlikeDataSet();
			if(_tableName == "BAR_PRINT")
			{
				_ds = this.GetPageData(_sqlPrint,_pageCount);
				_ds.Tables[0].TableName = "BAR_PRINT";
			}
			else if(_tableName == "BAR_SPC")
			{
				_ds = this.GetPageData(_sqlSpc,_pageCount);
				_ds.Tables[0].TableName = "BAR_SPC";
			}
			else
			{
				_ds = this.GetPageData(_sqlRec,_pageCount);
				_ds.Tables[0].TableName = "BAR_REC";
			}
			return _ds;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="pageNo"></param>
		/// <returns></returns>
		private SunlikeDataSet GetPageData(string sql,int pageNo)
		{
			int _pageCount = 1;
			SunlikeDataSet _ds = new SunlikeDataSet();
			try
			{
				SqlConnection _conn = new SqlConnection(ConnectionString);
				string _spName = "sp_browser_paging1";
				SqlCommand _cmd = new SqlCommand(_spName,_conn);
				_cmd.CommandType = CommandType.StoredProcedure;

				_cmd.Parameters.Add(new SqlParameter("@SqlString",SqlDbType.VarChar,4000));
				_cmd.Parameters["@SqlString"].Value = sql;
				_cmd.Parameters.Add(new SqlParameter("@SQLOrderby",SqlDbType.VarChar,1000));
				_cmd.Parameters["@SQLOrderby"].Value = "";
				_cmd.Parameters.Add(new SqlParameter("@NumPerPage",SqlDbType.Int));
				_cmd.Parameters["@NumPerPage"].Value = 10000;
				_cmd.Parameters.Add(new SqlParameter("@CurrentPage",SqlDbType.Int));
				_cmd.Parameters["@CurrentPage"].Value = pageNo;
				_cmd.Parameters.Add(new SqlParameter("@TotalPageCount",SqlDbType.Int,4,ParameterDirection.Output,false,0,0,"TotalPageCount",DataRowVersion.Default,null));
				_cmd.Parameters.Add(new SqlParameter("@TotalRowCount",SqlDbType.Int,4,ParameterDirection.Output,false,0,0,"TotalRowCount",DataRowVersion.Default,null));

				try
				{
					SqlDataAdapter _sda = new SqlDataAdapter(_cmd);					
					_sda.Fill(_ds);
					_ds.Tables.RemoveAt(0);
					_pageCount = Convert.ToInt32(_cmd.Parameters["@TotalPageCount"].Value);
					_ds.ExtendedProperties.Add("PAGECOUNT",_pageCount);
				}
				catch
				{
					_pageCount = 0;
				}
			}
			catch
			{
				_pageCount = 0;
			}
			return _ds;
		}
		#endregion

		#region 装箱套版
		/// <summary>
		/// 装箱套版
		/// </summary>
		/// <param name="boxNo"></param>
		/// <returns></returns>
		public DataTable GetPrintData(string boxNo)
		{
			string _sSql = "DECLARE @B_PMARK INT"
				+ " DECLARE @E_PMARK INT"
				+ " SELECT @B_PMARK = B_PMARK, @E_PMARK = E_PMARK FROM BAR_ROLE"
				+ " SELECT DISTINCT BR.PRD_NO, MB.BATCH_NO, PT.NAME AS PRD_NAME, PT.SNM, BB.PMARK, BB.SPC_NO, BB.QTY, BS.NAME AS SPC_NAME FROM"
				+ " (SELECT BOX_NO, SUBSTRING(BAR_NO, @B_PMARK, @E_PMARK - @B_PMARK + 1) AS PMARK, SPC_NO, COUNT(*) AS QTY"
				+ " FROM BAR_REC WHERE BOX_NO = @BOX_NO"
				+ " GROUP BY BOX_NO, SUBSTRING(BAR_NO, @B_PMARK, @E_PMARK - @B_PMARK + 1), SPC_NO) BB"
				+ " LEFT JOIN"
				+ " BAR_SPC AS BS"
				+ " ON BB.SPC_NO = BS.SPC_NO"
				+ " LEFT JOIN"
				+ " MF_BOX MB"
				+ " ON BB.BOX_NO = MB.BOX_NO"
				+ " LEFT JOIN"
				+ " (SELECT BOX_NO, PRD_NO FROM BAR_BOX) AS BR"
				+ " ON BB.BOX_NO = BR.BOX_NO"
				+ " LEFT JOIN"
				+ " (SELECT PRD_NO, NAME, SNM FROM PRDT) AS PT"
				+ " ON BR.PRD_NO = PT.PRD_NO";
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@BOX_NO", SqlDbType.VarChar, 90);
			_aryPt[0].Value = boxNo;
			return this.ExecuteDataset(_sSql, _aryPt).Tables[0];
		}
		#endregion

		#region 判断在BAR_PRINT和BAR_REC中有无此序列号
		/// <summary>
		/// 判断在BAR_PRINT和BAR_REC中有无此序列号
		/// </summary>
		/// <param name="BarCode">条码字符串(组SQL条件)</param>
		/// <returns></returns>
		public ArrayList ChkBarPrint(string BarCode)
		{
            StringBuilder _errCode = new StringBuilder();//条码错误信息			
			StringBuilder _errSpc = new StringBuilder();//规格错误信息
			string[] _printAry = BarCode.Split(new char[]{';'});
			StringBuilder _printRec = new StringBuilder(" WHERE 1<>1 ");
			if(_printAry.Length > 0)
			{
				for(int i = 0; i < _printAry.Length; i++)
				{
					_printRec.Append(" OR BAR_NO='"+_printAry[i]+"' ");
				}
			}
			string _sqlPrint = "SELECT BAR_NO,SPC_NO FROM BAR_PRINT " + _printRec.ToString();
            string _sqlRec = "SELECT BAR_NO,SPC_NO FROM BAR_REC " + _printRec.ToString();			
			SunlikeDataSet _ds1 = new SunlikeDataSet();
			SunlikeDataSet _ds2 = new SunlikeDataSet();
			this.FillDataset(_sqlPrint,_ds1,new string[] {"BAR_PRINT"});
			this.FillDataset(_sqlRec,_ds2,new string[] {"BAR_PRINT"});
			_ds1.Merge(_ds2);
			DataTable _dt = _ds1.Tables["BAR_PRINT"];
			for(int i = 0;i < _printAry.Length; i++)
			{
				if(!String.IsNullOrEmpty(_printAry[i].Trim()))
				{
					DataRow _row = _dt.Rows.Find(new object[]{_printAry[i]});
					if(_row != null)
					{
						if(String.IsNullOrEmpty(_row["SPC_NO"].ToString().Trim()))
						{
							_errSpc.Append("序列号" + _row["BAR_NO"].ToString() + "的规格不存在;\r\n");
						}
					}
					else
					{
                        _errCode.Append("序列号" + _printAry[i] + "不存在;\r\n");
					}
				}
			}
			ArrayList _aryErr = new ArrayList();
			_aryErr.Add(_errCode);
			_aryErr.Add(_errSpc.ToString());
			return _aryErr;
		}
		#endregion
	
		#region 得到BAR_BOX中的明细资料 by db
		/// <summary>
		/// 得到BAR_BOX中的明细资料 by db
		/// </summary>
		/// <param name="Wh"></param>
		/// <param name="Prd_No"></param>
		/// <param name="Content"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarBoxByContent(string Wh,string Prd_No,string Content)
		{
			SqlParameter[] _sqlPara = new SqlParameter[3];
			_sqlPara[0] = new SqlParameter("@WH",SqlDbType.VarChar,12);
			_sqlPara[0].Value = Wh;
			_sqlPara[1] = new SqlParameter("@PRD_NO",SqlDbType.VarChar,30);
			_sqlPara[1].Value = Prd_No;
			_sqlPara[2] = new SqlParameter("@CONTENT",SqlDbType.VarChar,255);
			_sqlPara[2].Value = Content;

			string _sql = " SELECT A.WH,ISNULL(B.NAME,A.WH) AS WH_NAME,A.PRD_NO,ISNULL(C.NAME,A.PRD_NO) AS PRD_NAME,A.BOX_NO,A.BOX_DD,A.DEP,ISNULL(D.NAME,A.DEP) AS DEP_NAME,A.QTY,A.USR FROM BAR_BOX A "
				+ " LEFT JOIN MY_WH B ON A.WH=B.WH LEFT JOIN PRDT C ON A.PRD_NO=C.PRD_NO LEFT JOIN DEPT D ON A.DEP=D.DEP "
				+ " WHERE A.CONTENT=@CONTENT AND A.WH=@WH AND A.PRD_NO=@PRD_NO AND ISNULL(A.STOP_ID,'')<>'T' ";

			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null,_sqlPara);
			return _ds;
		}
		#endregion
	
		#region 根据葙码得到BAR_REC中的明细资料 by db
		/// <summary>
		/// 根据葙码得到BAR_REC中的明细资料 by db
		/// </summary>
		/// <param name="Box_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarRecByBoxNo(string Box_No)
		{
			SqlParameter[] _sqlPara = new SqlParameter[1];
			_sqlPara[0] = new SqlParameter("@BOX_NO",SqlDbType.VarChar,90);
			_sqlPara[0].Value = Box_No;

			string _sql = " SELECT A.BOX_NO,A.WH,ISNULL(B.NAME,A.WH) AS WH_NAME,A.PRD_NO,"
				+" ISNULL(C.NAME,A.PRD_NO) AS PRD_NAME,D.BAR_NO,D.UPDDATE,D.BAT_NO FROM BAR_BOX A "
				+" LEFT JOIN MY_WH B ON A.WH=B.WH LEFT JOIN PRDT C ON A.PRD_NO=C.PRD_NO "
				+" INNER JOIN BAR_REC D ON A.BOX_NO=D.BOX_NO AND ISNULL(D.STOP_ID,'')<>'T'"
				+" WHERE A.BOX_NO=@BOX_NO  ";

			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null,_sqlPara);
			return _ds;
		}
		#endregion

		#region 取得序列号变更记录
		/// <summary>
		/// 取得序列号变更记录
		/// </summary>
		/// <param name="BilID">来源单据类别</param>
		/// <param name="BilNo">来源单号</param>
		/// <returns></returns>
		public DataTable GetBarChange(string BilID,string BilNo)
		{
			string _sql = "select BAR_NO,UPDDATE,WH1,WH2,BIL_NO,USR,BIL_ID,BAT_NO1,BAT_NO2,PH_FLAG1,PH_FLAG2 from BAR_CHANGE where BIL_ID=@BilID and BIL_NO=@BilNo";
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@BilID",SqlDbType.VarChar,2);
			_spc[0].Value = BilID;
			_spc[1] = new SqlParameter("@BilNo",SqlDbType.VarChar,20);
			_spc[1].Value = BilNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,null,_spc);
			_ds.Tables[0].TableName = "BAR_CHANGE";
			return _ds.Tables["BAR_CHANGE"];
		}
        /// <summary>
        /// 取得序列号变更记录
        /// </summary>
        /// <param name="barNo">序列号</param>
        /// <param name="upDate">更新日期</param>
        /// <returns></returns>
        public DataTable GetBarChange(string barNo, DateTime upDate)
        {
            string _sql = "SELECT BAR_NO,UPDDATE,WH1,WH2,BIL_NO,USR,BIL_ID,BAT_NO1,BAT_NO2,PH_FLAG1,PH_FLAG2 FROM BAR_CHANGE WHERE BAR_NO = @BAR_NO AND UPDDATE = @UPDDATE";
            SqlParameter[] _aryPt = new SqlParameter[2];
            _aryPt[0] = new SqlParameter("@BAR_NO", SqlDbType.VarChar, 90);
            _aryPt[0].Value = barNo;
            _aryPt[1] = new SqlParameter("@UPDDATE", SqlDbType.VarChar, 90);
            _aryPt[1].Value = upDate;
            SunlikeDataSet _ds = new SunlikeDataSet();
            this.FillDataset(_sql, _ds, null, _aryPt);
            _ds.Tables[0].TableName = "BAR_CHANGE";
            return _ds.Tables["BAR_CHANGE"];
        }
		#endregion

		#region 得到产品的规格(离线装箱用)
		/// <summary>
		/// 得到产品的规格(离线装箱用)
		/// </summary>
		/// <param name="Ht">存储规格的Hashtable</param>
		/// <returns></returns>
        public SunlikeDataSet GetSpc(Dictionary<String, Object> Ht)
		{
			string _sql1 = "SELECT BAR_NO,SPC_NO FROM BAR_REC ";
			string _sql2 = "SELECT BAR_NO,SPC_NO FROM BAR_PRINT ";
			string _barAry = "";

			IEnumerator _itms = Ht.GetEnumerator();
			while (_itms.MoveNext())
			{
                _barAry += ",'" + ((KeyValuePair<String, Object>)_itms.Current).Key.ToString() + "'";
			}
			if(!String.IsNullOrEmpty(_barAry))
			{
				_sql1 += " WHERE BAR_NO IN (''"+_barAry+")";
				_sql2 += " WHERE BAR_NO IN (''"+_barAry+")";
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			SunlikeDataSet _ds1 = new SunlikeDataSet();
			this.FillDataset(_sql1,_ds,new string[]{"BAR_SPC"});
			this.FillDataset(_sql2,_ds1,new string[]{"BAR_SPC"});
			_ds.Merge(_ds1);
			return _ds;
		}
		#endregion

		#region 检测产品规格是否存在
		/// <summary>
		/// 检测产品规格是否存在
		/// </summary>
		/// <param name="SpcNo">规格</param>
		/// <returns></returns>
		public bool IsSpcExist(string SpcNo)
		{
			string _sql = "SELECT SPC_NO FROM BAR_SPC WHERE SPC_NO=@SPC_NO";
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@SPC_NO", SqlDbType.VarChar, 10);
			_aryPt[0].Value = SpcNo;
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql, _ds, new string[]{"BAR_SPC"}, _aryPt);
			if (_ds.Tables[0].Rows.Count > 0)
			{
				return true;
			}
			return false;
		}
		#endregion

		#region 得到反拆箱数据
		/// <summary>
		/// 得到反拆箱数据
		/// </summary>
		/// <param name="BoxNo">箱码</param>
		/// <returns></returns>
		public SunlikeDataSet GetBoxChange(string BoxNo)
		{
			SqlParameter[] _sqlPara = new SqlParameter[1];
			_sqlPara[0] = new SqlParameter("@BOX_NO",SqlDbType.VarChar,90);
			_sqlPara[0].Value = BoxNo;

			string _sql = "SELECT A.*,B.PRD_NO,C.NAME AS PRD_NAME,D.NAME AS WH_NAME,E.CONTENT,E.QTY,"
				+" E.STOP_ID,B.WH AS WH_BAR,E.WH AS WH_BOX"
				+" FROM BOX_CHANGE A"
				+" INNER JOIN BAR_REC B ON A.BAR_NO=B.BAR_NO "
				+" INNER JOIN PRDT C ON C.PRD_NO=B.PRD_NO"
				+" LEFT JOIN MY_WH D ON A.WH=D.WH"
				+" INNER JOIN BAR_BOX E ON A.BOX_NO=E.BOX_NO"						
				+" WHERE A.BOX_NO=@BOX_NO";

			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"BOX_CHANGE"},_sqlPara);
			return _ds;
		}		
		#endregion

		#region 判断序列号是否存在
		/// <summary>
		/// 判断序列号是否存在
		/// </summary>
		/// <param name="BarCode">条码</param>
		/// <returns></returns>
		public bool IsExistBarCode(string BarCode)
		{
			bool _exist = false;
			SqlParameter[] _aryPt = new SqlParameter[1];
			_aryPt[0] = new SqlParameter("@BAR_NO", SqlDbType.VarChar, 90);
			_aryPt[0].Value = BarCode;

			string _sqlPrint = "SELECT BAR_NO,SPC_NO FROM BAR_PRINT WHERE BAR_NO=@BAR_NO";
			string _sqlRec = "SELECT BAR_NO,SPC_NO FROM BAR_REC WHERE BAR_NO=@BAR_NO";
			SunlikeDataSet _ds1 = new SunlikeDataSet();
			SunlikeDataSet _ds2 = new SunlikeDataSet();
			this.FillDataset(_sqlPrint,_ds1,new string[] {"BAR_PRINT"},_aryPt);
			this.FillDataset(_sqlRec,_ds2,new string[] {"BAR_PRINT"},_aryPt);
			if(_ds1.Tables[0].Rows.Count > 0)
			{
				return true;
			}
			if(_ds2.Tables[0].Rows.Count > 0)
			{
				return true;
			}
			return _exist;			
		}
		#endregion

		#region 依据箱码取序列号
		/// <summary>
		/// 依据箱码取序列号
		/// </summary>
		/// <param name="boxNo"></param>
		/// <returns></returns>
		public DataSet GetDataByBoxNo(string boxNo)
		{
			string _sql = "SELECT * FROM BAR_REC WHERE BOX_NO=@BOX_NO";
			SqlParameter _sqlPt = new SqlParameter("@BOX_NO", SqlDbType.VarChar, 90);
			_sqlPt.Value = boxNo;
			DataSet _ds = new DataSet();
			this.FillDataset(_sql, _ds, new string[1]{"BAR_REC"}, _sqlPt);
			return _ds;
		}
		#endregion

		#region 更新装箱时间
		/// <summary>
		/// 更新装箱时间
		/// </summary>
		/// <param name="BoxNo"></param>
		public void UpdateDateTimeBox(string BoxNo)
		{
			this.ExecuteNonQuery("update BAR_REC set UPDDATE='"+System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+"' where BOX_NO='"+BoxNo+"'");
		}
		#endregion

        #region 更新序列号PH_FLAG
        
        /// <summary>
        /// 更新序列号PH_FLAG
        /// </summary>
        /// <param name="barCodes">序列号集合</param>
        /// <param name="cfmSw">true:T,false:F</param>
        public void UpdatePhFlag(string barCodes, bool cfmSw)
        {
            string _sql = "UPDATE BAR_REC SET PH_FLAG = @PH_FLAG";
            if (barCodes != "")
            {
                _sql += " WHERE BAR_NO IN (" + barCodes + ")";
            }
            else
            {
                _sql += " WHERE 1 <> 1";
            }
            SqlParameter[] _aryPt = new SqlParameter[1];
            _aryPt[0] = new SqlParameter("@PH_FLAG", SqlDbType.VarChar, 2);
            _aryPt[0].Value = cfmSw ? "T" : "F";

            this.ExecuteNonQuery(_sql, _aryPt);
        }

        #endregion
    }
}
