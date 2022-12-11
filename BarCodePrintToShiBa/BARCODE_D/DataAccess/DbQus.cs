using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business.Data
{
	/// <summary>
	/// DbQus 的摘要说明。
	/// </summary>
	public class DbQus : Sunlike.Business.Data.DbObject
	{
		#region 构造
		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="connStr"></param>
		public DbQus(string connStr):base(connStr)
		{

		}
		#endregion

		#region 判断问题大类是否在使用中
		/// <summary>
		/// 判断问题大类是否在使用中
		/// </summary>
		/// <param name="ID">大类代号</param>
		/// <returns></returns>
		public SunlikeDataSet GetKND_NO(string ID)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@ID",SqlDbType.VarChar,10);
			_spc[0].Value = ID;
			string _sql = "SELECT QUS_NO,KND_NO FROM CUS_QUS WHERE KND_NO=@ID;SELECT KND_NO FROM KB_QA WHERE KND_NO=@ID; ";
			this.FillDataset(_sql,_ds,new string[]{"CUS_QUS","KB_QA"},_spc);
			return _ds;
		}
		#endregion

		#region 判断问题中类是否在使用中
		/// <summary>
		/// 判断问题中类是否在使用中
		/// </summary>
		/// <param name="ID">中类代号</param>
		/// <returns></returns>
		public SunlikeDataSet GetIDX_NO(string ID)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@ID",SqlDbType.VarChar,10);
			_spc[0].Value = ID;
			string _sql = "SELECT QUS_NO,IDX_NO FROM CUS_QUS WHERE IDX_NO=@ID;SELECT KND_NO FROM KB_QA WHERE KND_NO=@ID;";
			this.FillDataset(_sql, _ds, new string[] { "CUS_QUS", "KB_QA" }, _spc);
			return _ds;
		}
		#endregion

		#region 问题大类
		/// <summary>
		/// 问题大类
		/// </summary>
		/// <param name="ID">大类代号</param>
		/// <returns></returns>
		public SunlikeDataSet GetDataBigType(string ID)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@ID",SqlDbType.VarChar,10);
			_spc[0].Value = ID;
			string _sql = "SELECT KND_NO,NAME FROM CUS_QUS_K WHERE KND_NO = @ID";
			this.FillDataset(_sql,_ds,new string[]{"CUS_QUS_K"},_spc);
			return _ds;
		}
		#endregion

        #region 取处理人员的名字
        /// <summary>
        /// 取处理人员的名字
        /// </summary>
        /// <param name="ygID">员工代号</param>
        /// <returns></returns>
        public string GetYgName(string ygID)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@YG", SqlDbType.VarChar, 12);
            _spc[0].Value = ygID;
            string _sql = "SELECT YG_NO,NAME FROM MF_YG WHERE YG_NO=@YG";
            this.FillDataset(_sql, _ds, new string[] { "MF_YG" }, _spc);
            if (_ds.Tables["MF_YG"].Rows.Count > 0)
            {
                return _ds.Tables["MF_YG"].Rows[0]["NAME"].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 取得问题中类
        /// <summary>
		/// 问题中类
		/// </summary>
		/// <param name="ID">中类代号</param>
        /// <param name="tree">含下属否</param>
		/// <returns></returns>
		public SunlikeDataSet GetDataMidstType(string ID,bool tree)
		{
            string _sql = "";
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@ID",SqlDbType.VarChar,10);
			_spc[0].Value = ID;
            if (!tree)
            {
                _sql = "SELECT IDX_NO,NAME,UP FROM CUS_QUS_I WHERE IDX_NO=@ID";
            }
            else
            {
                _sql = "SELECT IDX_NO,NAME,UP,LEVEL FROM UFN_GETCUSQUSIDXTREE(@ID)";
            }
			this.FillDataset(_sql,_ds,new string[]{"CUS_QUS_I"},_spc);
			return _ds;
		}
		#endregion
			
		#region GetData
        /// <summary>
        /// 取客户问题资料
        /// </summary>
		/// <param name="usr">操作用户代号</param>
        /// <param name="qusNo">问题代号</param>
        /// <param name="compNo">公司代号</param>
        /// <returns></returns>
		public SunlikeDataSet GetData(string usr, string qusNo, string compNo)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[2];
			_spc[0] = new SqlParameter("@NO", SqlDbType.VarChar);
			_spc[0].Value = qusNo;
			_spc[1] = new SqlParameter("@CompNo", SqlDbType.VarChar, 4);
			_spc[1].Value = compNo;
            string _sql = "Select A.QA_NO,A.QA_DD,A.USR,A.DEP,A.KND_NO,A.MOD_NO,A.MOD_SUB,"
                        + " A.CUS_NO,A.SYS_DATE,A.PAC_NO,A.WC_NO,A.CNT_NO,A.CLS_ID,A.SAL_NO, S.NAME SAL_NAME,"
                        + " A.CLS_ID_DD,A.QA_TYPE,A.OBJ1,A.UPD_DD,A.OUT_NO,A.QA_REM,A.QA_REM1,"
                        + " A.QA_REM2,A.USE_HH,A.CNT_REM,A.EST_DD,A.PRD_NO,A.PRD_NAME,A.CNT_NAME,A.CNT_TEL,A.CNT_CELL,A.CNT_ADR,"
                        + " T.NAME CNT_NAMES,T.TEL_NO CNT_TELS, T.CELL_NO CNT_CELLS, T.ADR CNT_ADRS,"
                        + " B.NAME CUS_NAME,C.NAME DEP_NAME,E.NAME KND_NAME,F.NAME MOD_NAME,G.NAME USR_NAME, "
                        + " W.PRD_NO, W.PRD_MARK, W.MTN_DD, W.RETURN_DD "
                        + " FROM MF_QA A WITH (readpast)"
                        + " left join CONTACT T ON T.CNT_NO=A.CNT_NO "
                        + " left join ufn_GetCustList('" + usr + "') B on B.CUS_NO=A.CUS_NO "
                        + " left join DEPT C on C.DEP=A.DEP "
                        + " left join QA_KND E on E.KND_NO=A.KND_NO "
                        + " left join QA_MOD F on F.MOD_NO=A.MOD_NO "
                        + " left join MF_WC W on A.WC_NO=W.WC_NO "
                        + " left join SUNSYSTEM..PSWD G on G.COMPNO=@CompNo and G.USR=A.USR "
                        + " left join SALM S on A.SAL_NO=S.SAL_NO "
                        + " WHERE QA_NO = @NO "
                        + "Select * FROM TF_QA_FILE WHERE QA_NO = @NO ";
			this.FillDataset(_sql, _ds, new string[2] { "MF_QA", "TF_QA_FILE" }, _spc);
            //表头和表身关联
            DataColumn[] _dca1 = new DataColumn[1];
            _dca1[0] = _ds.Tables["MF_QA"].Columns["QA_NO"];
            DataColumn[] _dca2 = new DataColumn[1];
            _dca2[0] = _ds.Tables["TF_QA_FILE"].Columns["QA_NO"];

            _ds.Relations.Add("MF_QATF_QA_FILE", _dca1, _dca2);
			return _ds;
		}
        /// <summary>
        /// 取得客户问题处理单
        /// </summary>
        /// <param name="outNo">外出单号</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string outNo)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _sqlPara = new SqlParameter[2];
            _sqlPara[0] = new SqlParameter("@OUT_NO", SqlDbType.VarChar);
            _sqlPara[0].Value = outNo;
            string _sqlStr = "Select A.QA_NO,A.QA_DD,A.USR,A.DEP,A.KND_NO,A.MOD_NO,A.MOD_SUB,"
                        + " A.CUS_NO,A.SYS_DATE,A.PAC_NO,A.WC_NO,A.CNT_NO,A.CLS_ID,A.SAL_NO,"
                        + " A.CLS_ID_DD,A.QA_TYPE,A.OBJ1,A.UPD_DD,A.OUT_NO,A.QA_REM,A.QA_REM1,"
                        + " A.QA_REM2,A.USE_HH,A.CNT_REM,A.EST_DD "
                        + " FROM MF_QA A WITH (readpast)"
                        + " WHERE OUT_NO = @OUT_NO ";
            this.FillDataset(_sqlStr, _ds, new string[1] { "MF_QA" }, _sqlPara);
            return _ds;
        }

        /// <summary>
        /// 取得客户资讯资料
        /// </summary>
        /// <param name="_qaNo"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataInfo(string _qaNo, string Cus_No)
        {
            SunlikeDataSet ds = new SunlikeDataSet();
            SqlParameter[] _spc = new SqlParameter[2];
            _spc[0] = new SqlParameter("@QA_NO", SqlDbType.VarChar);
            _spc[0].Value = _qaNo;
            _spc[1] = new SqlParameter("@CUS_NO", SqlDbType.VarChar);
            _spc[1].Value = Cus_No;
            string _sql = "Select Top 1 END_DD From CUS_PACT WHERE CUS_NO=@CUS_NO " +
                " Order By PAC_DD DESC " +
                "Select * From CUST_ATTN Where CUS_NO=@CUS_NO " +
                "Select * From MF_QA Where QA_NO=@QA_NO";
            this.FillDataset(_sql, ds, new string[3] { "CUS_PACT","CUST_ATTN","MF_QA" }, _spc);
            return ds;
        }
		#endregion

		#region 取附件
		/// <summary>
		/// 取附件
		/// </summary>
        /// <param name="fileID"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataFile(int fileID)
		{
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@ID",SqlDbType.Int);
			_spc[0].Value = fileID;
			string _sql = "SELECT FILE_ID,FILE_NAME,FILE_DD,USR,QUS_NO,CONTENT FROM CUS_QUS_F WHERE FILE_ID = @ID";
			SunlikeDataSet _ds = new SunlikeDataSet();
			this.FillDataset(_sql,_ds,new string[]{"CUS_QUS_F"},_spc);
			return _ds;
		}
		#endregion

		#region 得到问题最大代号
		/// <summary>
		/// 得到问题最大代号
		/// </summary>
		/// <returns></returns>
		public int GetMaxNo()
		{
			int _no = 1;
			string _sql = "SELECT MAX(QUS_NO) FROM CUS_QUS";
			DataSet _ds = new DataSet();
			this.FillDataset(_sql,_ds,new string[]{"CUS_QUS"});
			if(_ds.Tables["CUS_QUS"].Rows[0][0] != System.DBNull.Value)
			{
				_no = Convert.ToInt32(_ds.Tables["CUS_QUS"].Rows[0][0]) + 1;
			}
			return _no;
		}
		#endregion

		#region 得到所有的回复描述
		/// <summary>
		/// 得到所有的回复描述
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataHf(string id)
		{
			SunlikeDataSet _ds = new SunlikeDataSet();
			SqlParameter[] _spc = new SqlParameter[1];
			_spc[0] = new SqlParameter("@NO", SqlDbType.Int);
			_spc[0].Value = Convert.ToInt32(id);
			string _sql = "Select QUS_NO,CUS_NO,QUS_DD,USR,DEP,REM,RUN_USR,TYPE,CLOSE_ID,UP,SRV_ID,KND_NO,IDX_NO,TITLE,URGENT FROM CUS_QUS WHERE UP = @NO";
			this.FillDataset(_sql,_ds,new string[]{"CUS_QUS"},_spc);
			return _ds;
		}
		#endregion

		#region 上传文件
		/// <summary>
		/// 上传文件
		/// </summary>
		/// <param name="fileName">文件名</param>
		/// <param name="file">文件内容</param>
		/// <param name="usr">上传用户</param>
		/// <param name="fileNo">文管文件代号</param>
		/// <returns></returns>
		public int AddQusFile(string fileName, byte[] file, string usr, string fileNo)
		{
			SqlParameter[] _spc = new SqlParameter[5];
			_spc[0] = new SqlParameter("@FILENAME",SqlDbType.VarChar,100);
			_spc[0].Value = fileName;
			_spc[1] = new SqlParameter("@DATE",System.Data.SqlDbType.DateTime);
			_spc[1].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			_spc[2] = new SqlParameter("@CONTENT",System.Data.SqlDbType.Image);
			_spc[2].Value = file;
			_spc[3] = new SqlParameter("@USR",SqlDbType.VarChar,12);
			_spc[3].Value = usr;
			_spc[4] = new SqlParameter("@FileNo", SqlDbType.VarChar, 100);
			_spc[4].Value = fileNo;
			string _sql = "delete from CUS_QUS_F where isnull(QUS_NO,0)=0 and (FILE_DD+1)<getdate() \n"
				+ "INSERT INTO CUS_QUS_F(FILE_NAME,FILE_DD,CONTENT,USR,QUS_NO,FILE_NO) VALUES (@FILENAME,@DATE,@CONTENT,@USR,0,@FileNo) \n"
				+ "Select Max(FILE_ID) FROM CUS_QUS_F";
			DataSet _ds = this.ExecuteDataset(_sql, _spc);
			return Convert.ToInt32(_ds.Tables[0].Rows[0][0].ToString());
		}
		#endregion

        #region 更新转出单号
        /// <summary>
        /// 更新转出单号
        /// </summary>
        /// <param name="qaNo">问题单号</param>
        /// <param name="outId">转出单据识别码</param>
        /// <param name="outNo">转出单号</param>
        /// <returns></returns>
        public bool UpdateOutNo(string qaNo, string outId, string outNo)
        {
            bool _result = false;
            string _sqlStr = "";
            if (!string.IsNullOrEmpty(outNo))
            {
                _sqlStr = " UPDATE MF_QA SET OUT_ID=@OUT_ID,OUT_NO=@OUT_NO,CLS_ID='T' WHERE QA_NO=@QA_NO";
            }
            else
            {
                _sqlStr = " UPDATE MF_QA SET OUT_ID=@OUT_ID,OUT_NO=@OUT_NO,CLS_ID='F' WHERE QA_NO=@QA_NO";
            }
            SqlParameter[] _sqlPara = new SqlParameter[3];
            _sqlPara[0] = new SqlParameter("@QA_NO", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = qaNo;
            _sqlPara[1] = new SqlParameter("@OUT_ID", SqlDbType.VarChar, 2);
            _sqlPara[1].Value = outId;
            _sqlPara[2] = new SqlParameter("@OUT_NO", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = outNo;
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

        #region 批次更新客户问题单

        /// <summary>
        /// 查询 GetData for qa_no in s
        /// </summary>
        /// <param name="qa_no"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForCheck(string qa_no)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            //SqlParameter[] _spc = new SqlParameter[1];
            //_spc[0] = new SqlParameter("@qa_no", SqlDbType.VarChar);
            //_spc[0].Value = qa_no;
            string _sql = " select * from MF_QA where qa_no in " + qa_no;
            //this.FillDataset(_sql, _ds, new string[] { "MF_QA" }, _spc);
            this.FillDataset(_sql, _ds, new string[] { "MF_QA" });
            return _ds;
        }

        /// <summary>
        /// 批次结案GetData
        /// </summary>
        /// <param name="qa_no"></param>
        /// <returns></returns>
        public SunlikeDataSet GetDataForQasBatch(string qa_no, string para)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@qa_no", SqlDbType.VarChar);
            _spc[0].Value = qa_no;
            string _sql = " select " + para + " from MF_QA where qa_no = @qa_no "
                        + " select QA_NO, ITM, FILE_REM, CONTENT, USR from TF_QA_FILE where QA_NO = @qa_no ";
            //+ " seelct * from TF_QA_FILE where qa_no = @qa_no ";
            this.FillDataset(_sql, _ds, new string[2] { "MF_QA", "TF_QA_FILE" }, _spc);
            //this.FillDataset(_sql, _ds, new string[1] { "MF_QA" }, _spc);

            return _ds;
        }

        /// <summary>
        /// 更新客户问题单更新
        /// </summary>
        /// <param name="qaNo">问题单集合(ex: str = 'QA9B020001','QA9B020002' )</param>
        public bool UpdateQaForBatch(string qaNo, string knd_no, string mod_no, string mod_sub, string qa_rem, string qa_rem1, string qa_rem2)
        {
            bool _result = false;

            // 纪录参数arr
            string para = "";
            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            System.Collections.ArrayList arrPara = new System.Collections.ArrayList();
            //if (true) { arrPara.Add(qaNo); System.Collections.ArrayList tmp = new System.Collections.ArrayList(); tmp.Add("@qa_no"); tmp.Add(SqlDbType.VarChar); arr.Add(tmp); }
            if (knd_no != "") { para += "knd_no=@knd_no,"; arrPara.Add(knd_no); System.Collections.ArrayList tmp = new System.Collections.ArrayList(); tmp.Add("@knd_no"); tmp.Add(SqlDbType.VarChar); tmp.Add(10); arr.Add(tmp); }
            if (mod_no != "") { para += "mod_no=@mod_no,"; arrPara.Add(mod_no); System.Collections.ArrayList tmp = new System.Collections.ArrayList(); tmp.Add("@mod_no"); tmp.Add(SqlDbType.VarChar); tmp.Add(10); arr.Add(tmp); }
            if (mod_sub != "") { para += "mod_sub=@mod_sub,"; arrPara.Add(mod_sub); System.Collections.ArrayList tmp = new System.Collections.ArrayList(); tmp.Add("@mod_sub"); tmp.Add(SqlDbType.VarChar); tmp.Add(20); arr.Add(tmp); }
            if (qa_rem != "") { para += "qa_rem=@qa_rem,"; arrPara.Add(qa_rem); System.Collections.ArrayList tmp = new System.Collections.ArrayList(); tmp.Add("@qa_rem"); tmp.Add(SqlDbType.Text); arr.Add(tmp); }
            if (qa_rem1 != "") { para += "qa_rem1=@qa_rem1,"; arrPara.Add(qa_rem1); System.Collections.ArrayList tmp = new System.Collections.ArrayList(); tmp.Add("@qa_rem1"); tmp.Add(SqlDbType.Text); arr.Add(tmp); }
            if (qa_rem2 != "") { para += "qa_rem2=@qa_rem2,"; arrPara.Add(qa_rem2); System.Collections.ArrayList tmp = new System.Collections.ArrayList(); tmp.Add("@qa_rem2"); tmp.Add(SqlDbType.Text); arr.Add(tmp); }
            para += " cls_id='T', cls_id_dd=getDate() ";
            //if (para.Length > 0) para = para.Substring(0, para.Length - 1);

            // 设定参数para
            //string _sqlStr = " update mf_qa set " + para + " where QA_NO in (@qa_No) ";
            string _sqlStr = " update mf_qa set " + para + " where qa_no in (" + qaNo + ")  ";

            SqlParameter[] _sqlPara = new SqlParameter[arr.Count];
            for (int i = 0; i < _sqlPara.Length; i++)
            {
                System.Collections.ArrayList tmp = (System.Collections.ArrayList)arr[i];
                if (tmp.Count == 3)
                    _sqlPara[i] = new SqlParameter(tmp[0].ToString(), (SqlDbType)tmp[1], Convert.ToInt32(tmp[2]));
                else
                    _sqlPara[i] = new SqlParameter(tmp[0].ToString(), (SqlDbType)tmp[1]);

                _sqlPara[i].Value = arrPara[i].ToString();
            }

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

        /// <summary>
        /// 读取客户附件文件项次
        /// </summary>
        public SunlikeDataSet GetDataItm(string qaNo)
        {
            SunlikeDataSet _ds = new SunlikeDataSet();
            SqlParameter[] _spc = new SqlParameter[1];
            _spc[0] = new SqlParameter("@qaNo", SqlDbType.VarChar, 20);
            _spc[0].Value = qaNo;
            string _sql = " select isnull(max(itm), 0) as itm from TF_QA_FILE where qa_no = @qaNo";
            this.FillDataset(_sql, _ds, new string[] { "TF_QA_FILE" }, _spc);
            return _ds;
        }

        /// <summary>
        /// 添加客户附件文件
        /// </summary>
        public bool InsertQaFileForBatch(string qaNo, string itm, string file_rem, byte[] content, string usr)
        {
            bool _result = false;
            string _sqlStr = " insert into TF_QA_FILE (qa_no, itm, file_rem, content, usr) values (@qa_no, @itm, @file_rem, @content, @usr) ";

            SqlParameter[] _sqlPara = new SqlParameter[5];
            _sqlPara[0] = new SqlParameter("@qa_no", SqlDbType.VarChar, 20);
            _sqlPara[0].Value = qaNo;
            _sqlPara[1] = new SqlParameter("@itm", SqlDbType.VarChar);
            _sqlPara[1].Value = itm;
            _sqlPara[2] = new SqlParameter("@file_rem", SqlDbType.VarChar, 20);
            _sqlPara[2].Value = file_rem;
            _sqlPara[3] = new SqlParameter("@content", System.Data.SqlDbType.Image);
            _sqlPara[3].Value = content;
            _sqlPara[4] = new SqlParameter("@usr", SqlDbType.VarChar, 12);
            _sqlPara[4].Value = usr;
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

    }
}
