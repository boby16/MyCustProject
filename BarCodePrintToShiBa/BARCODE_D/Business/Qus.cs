using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using Sunlike.Business.Data;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;

namespace Sunlike.Business
{
	/// <summary>
	/// Qus ��ժҪ˵����
	/// </summary>
	public class Qus : BizObject
	{
        private string _loginUsr = "";
		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public Qus()
		{

		}
		#endregion

		#region �ж���������Ƿ���ʹ����
		/// <summary>
		/// �ж���������Ƿ���ʹ����
		/// </summary>
		/// <param name="ID">�������</param>
		/// <returns></returns>
		public SunlikeDataSet GetKND_NO(string ID)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			return _qus.GetKND_NO(ID);
		}
		#endregion

		#region �ж����������Ƿ���ʹ����
		/// <summary>
		/// �ж����������Ƿ���ʹ����
		/// </summary>
		/// <param name="ID">�������</param>
		/// <returns></returns>
		public SunlikeDataSet GetIDX_NO(string ID)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			return _qus.GetIDX_NO(ID);
		}
		#endregion

        #region ȡ�ô�����Ա������
        /// <summary>
        /// ȡ�ô�����Ա������
        /// </summary>
        /// <param name="id">Ա������</param>
        /// <returns></returns>
        public string GetYgName(string id)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);
            return _qus.GetYgName(id);
            
        }
        #endregion

        #region �������
        /// <summary>
		/// �������
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataBigType(string ID)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			return _qus.GetDataBigType(ID);
		}
		#endregion

		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="tree"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataMidstType(string ID,bool tree)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			return _qus.GetDataMidstType(ID,tree);
		}
		#endregion

        #region �õ������������
        /// <summary>
		/// �õ������������
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public string GetKND_NAME(string ID)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			SunlikeDataSet _ds = _qus.GetDataBigType(ID);
			return _ds.Tables["CUS_QUS_K"].Rows[0]["NAME"].ToString();
		}
		#endregion

		#region �õ�������������
		/// <summary>
		/// �õ�������������
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public string GetIDX_NAME(string ID)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			SunlikeDataSet _ds = _qus.GetDataMidstType(ID,false);
			return _ds.Tables["CUS_QUS_I"].Rows[0]["NAME"].ToString();
		}
		#endregion

		#region ȡ�ͻ���������
		/// <summary>
        /// ȡ�ͻ���������
        /// </summary>
		/// <param name="usr">�����û�����</param>
        /// <param name="no">�������</param>
        /// <returns></returns>
		public SunlikeDataSet GetData(string usr, string no)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
            SunlikeDataSet ds = _qus.GetData(usr, no, Comp.CompNo);
            if (!string.IsNullOrEmpty(usr))
            {
                Users _users = new Users();
                string _billDep = "";
                string _billUsr = "";
                if (ds.Tables.Count > 0 && ds.Tables.Contains("MF_QA") && ds.Tables["MF_QA"].Rows.Count > 0)
                {
                    _billDep = ds.Tables["MF_QA"].Rows[0]["DEP"].ToString();
                    _billUsr = ds.Tables["MF_QA"].Rows[0]["USR"].ToString();
                }
                Hashtable _right = Users.GetBillRight("QUS_INDX", usr, _billDep, _billUsr);
                ds.ExtendedProperties["UPD"] = _right["UPD"];
                ds.ExtendedProperties["DEL"] = _right["DEL"];
                ds.ExtendedProperties["PRN"] = _right["PRN"];
                ds.ExtendedProperties["LCK"] = _right["LCK"];
            }
            return ds;
		}
        /// <summary>
        /// ȡ�ÿͻ����⴦��
        /// </summary>
        /// <param name="outNo">�������</param>
        /// <returns></returns>
        public SunlikeDataSet GetData(string outNo)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);            
            return _qus.GetData(outNo);
        }

        /// <summary>
        /// ȡ�ͻ���Ѷ����
        /// </summary>
        /// <param name="usr">�����û�����</param>
        /// <param name="_qaNo">�������</param>
        /// <returns></returns>
        public SunlikeDataSet GetDataInfo(string usr, string _qaNo, string Cus_No)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);
            return _qus.GetDataInfo(_qaNo, Cus_No);
        }
		#endregion

		#region ȡ����
		/// <summary>
		/// ȡ����
		/// </summary>
		/// <param name="fileID"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataFile(int fileID)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			return _qus.GetDataFile(fileID);
		}
		#endregion

		#region �õ�����������
		/// <summary>
		/// �õ�����������
		/// </summary>
		/// <returns></returns>
		public int GetMaxNo()
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			return _qus.GetMaxNo();
		}
		#endregion

		#region �õ����е���������
		/// <summary>
		/// �õ����е���������
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public SunlikeDataSet GetDataHf(string id)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			return _qus.GetDataHf(id);
		}
		#endregion

		#region �ϴ��ļ�
		/// <summary>
		/// �ϴ��ļ�
		/// </summary>
		/// <param name="fileName">�ļ���</param>
		/// <param name="file">�ļ�����</param>
		/// <param name="usr">�ϴ��û�</param>
		/// <param name="fileNo">�Ĺ��ļ�����</param>
		/// <returns></returns>
		public int AddQusFile(string fileName, byte[] file, string usr, string fileNo)
		{
			DbQus _qus = new DbQus(Comp.Conn_DB);
			return _qus.AddQusFile(fileName, file, usr, fileNo);
		}
		#endregion

        #region ȡ�����ⵥ��
        /// <summary>
        /// ȡ�����ⵥ��
        /// </summary>
        /// <returns></returns>
        public string GetQustNo(string QA_ID, string userId, DateTime dateTime)
        {
            string _qaNo = "";
            try
            {
                SQNO _sqlno = new SQNO();
                Users _users = new Users();

                _qaNo = _sqlno.Get(QA_ID, userId, _users.GetUserDepNo(userId), dateTime, "QA");
            }
            catch { }
            return _qaNo;
        }
        #endregion		

		#region UpdateData
		/// <summary>
		/// UpdateData
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public DataTable UpdateData(SunlikeDataSet ds)
		{
            DataRow _dr = ds.Tables["MF_QA"].Rows[0];
            if (_dr.RowState == DataRowState.Deleted)
            {
                _loginUsr = _dr["USR", DataRowVersion.Original].ToString();
            }
            else
            {
                _loginUsr = _dr["USR"].ToString();           
            }           
            Hashtable _ht = new Hashtable();
            _ht["MF_QA"] = "QA_NO,QA_DD,USR,DEP,KND_NO,MOD_NO,MOD_SUB,CUS_NO,SYS_DATE,PAC_NO,WC_NO,"+
                "CNT_NO,CLS_ID,SAL_NO,CLS_ID_DD,QA_TYPE,OBJ1,UPD_DD,OUT_NO,QA_REM,QA_REM1,QA_REM2,"+
                "USE_HH,CNT_REM,EST_DD,PRD_NO,PRD_NAME,CNT_NAME,CNT_TEL,CNT_CELL,CNT_ADR";
            _ht["TF_QA_FILE"] = "QA_NO,ITM,FILE_REM,CONTENT,USR";
			this.UpdateDataSet(ds,_ht);
            ds.AcceptChanges();
			return Sunlike.Business.BizObject.GetAllErrors(ds);
		}
		#endregion

		#region �����������
		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public DataTable UpdateBigType(SunlikeDataSet ds)
		{
			Hashtable _ht = new Hashtable();
			_ht["CUS_QUS_K"] = "KND_NO,NAME";
			this.UpdateDataSet(ds,_ht);
			return Sunlike.Business.BizObject.GetAllErrors(ds);
		}
		#endregion

		#region ������������
		/// <summary>
		/// ������������
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public DataTable UpdateMidstType(SunlikeDataSet ds)
		{
			Hashtable _ht = new Hashtable();
			_ht["CUS_QUS_I"] = "IDX_NO,NAME,UP";
			this.UpdateDataSet(ds,_ht);
			return Sunlike.Business.BizObject.GetAllErrors(ds);
		}
		#endregion

		#region BeforeUpdate
		/// <summary>
		/// BeforeUpdate
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="status"></param>
		protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
		{
			if(tableName == "CUS_QUS_K")
			{
				if(statementType == StatementType.Delete)
				{
					string _errMsg="";
					string _id = dr["KND_NO",DataRowVersion.Original].ToString();
					SunlikeDataSet _ds = GetKND_NO(_id);
					if (_ds.Tables["CUS_QUS"].Rows.Count > 0 || _ds.Tables["KB_QA"].Rows.Count > 0)
					{
						_errMsg = "������ѱ�ʹ��,����ɾ��";
					}
                   
					if (_errMsg != "")
                        throw new Sunlike.Common.Utility.SunlikeException("RCID=CWK.MF_DOC.MSG");
                    
				}
			}
			if(tableName == "CUS_QUS_I")
			{
				if(statementType == StatementType.Delete)
				{
					string _errMsg="";
					string _id = dr["IDX_NO",DataRowVersion.Original].ToString();
					SunlikeDataSet _ds = GetIDX_NO(_id);
					if (_ds.Tables["CUS_QUS"].Rows.Count > 0 || _ds.Tables["KB_QA"].Rows.Count > 0)
					{
						_errMsg = "������ѱ�ʹ��,����ɾ��";
					}

                    SunlikeDataSet _dsType = GetDataMidstType(_id,true);
                    if (_dsType.Tables["CUS_QUS_I"].Rows.Count > 1)
                    {
                        _errMsg = "������»��������,�������ɾ��";
                    }
                    if (_errMsg != "")
                    {
                        throw new Exception(_errMsg);
                    }
				}
			}
			if (tableName == "CUS_QUS")
			{
				Query _query = new Query();
                if (statementType == StatementType.Insert || statementType == StatementType.Update)
                {
                    SunlikeDataSet _ds = (SunlikeDataSet)dr.Table.DataSet;
                    string _fileQusNo = "";
                    if (_ds.ExtendedProperties.Contains("FILE_QUS_NO"))
                    {
                        _fileQusNo = _ds.ExtendedProperties["FILE_QUS_NO"].ToString();
                    }
                    if (_ds.ExtendedProperties.Contains("fileAry") && _ds.ExtendedProperties["fileAry"].ToString() != "")
                    {
                        if (statementType == StatementType.Update && (_fileQusNo == "" || (_fileQusNo != "" && _fileQusNo == dr["QUS_NO"].ToString())))
                        {
                            _query.RunSql("delete from CUS_QUS_F where QUS_NO=" + dr["QUS_NO"].ToString()
                                + " and FILE_ID not in ('"
                                + _ds.ExtendedProperties["fileAry"].ToString().Replace(";", "','") + "')");
                        }
                        if (_fileQusNo == "" || (_fileQusNo != "" && _fileQusNo == dr["QUS_NO"].ToString()))
                        {
                            _query.RunSql("update CUS_QUS_F set QUS_NO=" + dr["QUS_NO"].ToString()
                                + " where FILE_ID in ('"
                                + _ds.ExtendedProperties["fileAry"].ToString().Replace(";", "','") + "')");
                        }
                    }
                    else if (statementType == StatementType.Update && (_fileQusNo == "" || (_fileQusNo != "" && _fileQusNo == dr["QUS_NO"].ToString())))
                    {
                        _query.RunSql("delete from CUS_QUS_F where QUS_NO=" + dr["QUS_NO"].ToString());
                    }
                }
				if (statementType == StatementType.Delete)
				{
					string _no = dr["QUS_NO",DataRowVersion.Original].ToString();
					//ɾ������
					_query.RunSql("delete from CUS_QUS_F where QUS_NO=" + _no);
					//ɾ���ظ�
					DbQus _qus = new DbQus(Comp.Conn_DB);
					SunlikeDataSet _ds = _qus.GetDataHf(_no);
					for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
					{
						_ds.Tables[0].Rows[i].Delete();
					}
					DataTable _dtErr = UpdateData(_ds);
					if (_dtErr.Rows.Count > 0)
					{
						dr.RowError = _dtErr.Rows[0]["REM"].ToString();
					}
				}
			}
            if (tableName == "MF_QA")
            {
                if (statementType == StatementType.Insert)
                {
                    #region --���ɵ���
                    SQNO SunlikeSqNo = new SQNO();
                    DateTime _dtQaDd = System.DateTime.Now;
                    if (dr["QA_DD"] is System.DBNull)
                    {
                        _dtQaDd = Convert.ToDateTime(System.DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                    }
                    else
                    {
                        _dtQaDd = Convert.ToDateTime(dr["QA_DD"]);
                    }
                    string _qaNo = SunlikeSqNo.Set("QA", _loginUsr, dr["DEP"].ToString(), _dtQaDd, "");
                    dr["QA_NO"] = _qaNo;
                    dr["QA_DD"] = _dtQaDd.ToString(Comp.SQLDateFormat);
                    #endregion
                }
                if (statementType == StatementType.Delete)
                {
                    SQNO _sq = new SQNO();
                    string _error = _sq.Delete(dr["QA_NO", DataRowVersion.Original].ToString(), dr["USR", DataRowVersion.Original].ToString());
                    if (!String.IsNullOrEmpty(_error))
                    {
                        throw new SunlikeException("RCID=COMMON.HINT.DEL_NO_ERROR,PARAM=" + _error);//�޷�ɾ�����ţ�ԭ��{0}
                    }
                }
            }
		}

        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //DataTable _dt = ds.Tables[0];
            //if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //{
            //    Sunlike.Business.DataTrace _dataTrce = new DataTrace();

            //    _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), "QA");
            //}
            //#endregion


            base.BeforeDsSave(ds);
        }
		#endregion

        #region ����ת������
        /// <summary>
        /// ����ת������
        /// </summary>
        /// <param name="qaNo">���ⵥ��</param>
        /// <param name="outId">ת������ʶ����</param>
        /// <param name="outNo">ת������</param>
        public void UpdateOutNo(string qaNo, string outId, string outNo)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);
            _qus.UpdateOutNo(qaNo, outId, outNo);
        }
        #endregion

        #region ���¿ͻ����ⵥ

        /// <summary>
        /// ��ѯ MF_QA DataSet for qa_no
        /// </summary>
        public SunlikeDataSet GetDataForCheck(string qaNo)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);
            return _qus.GetDataForCheck(qaNo);
        }

        /// <summary>
        /// ��ѯ MF_QA DataSet for qa_no
        /// </summary>
        public SunlikeDataSet GetDataForQasBatch(string qaNo, string para)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);
            return _qus.GetDataForQasBatch(qaNo, para);
        }

        /// <summary>
        /// ���� Update
        /// </summary>
        public DataTable UpdateDataForQasBatch(SunlikeDataSet ds, string para)
        {
            DataRow _dr = ds.Tables["MF_QA"].Rows[0];
            //if (_dr.RowState == DataRowState.Deleted)
            //{
            //    _loginUsr = _dr["USR", DataRowVersion.Original].ToString();
            //}
            //else
            //{
            //    _loginUsr = _dr["USR"].ToString();
            //}
            Hashtable _ht = new Hashtable();
            _ht["MF_QA"] = para;
            _ht["TF_QA_FILE"] = "QA_NO,ITM,FILE_REM,CONTENT,USR";
            this.UpdateDataSet(ds, _ht);
            ds.AcceptChanges();
            return Sunlike.Business.BizObject.GetAllErrors(ds);
        }

        /// <summary>
        /// ���n
        /// </summary>
        public void UpdateQaForBatch(string qaNo, string knd_no, string mod_no, string mod_sub, string qa_rem, string qa_rem1, string qa_rem2)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);
            _qus.UpdateQaForBatch(qaNo, knd_no, mod_no, mod_sub, qa_rem, qa_rem1, qa_rem2);
        }

        /// <summary>
        /// ��ѯ���� itm
        /// </summary>
        public SunlikeDataSet GetDataItm(string qaNo)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);
            return _qus.GetDataItm(qaNo);
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        public void InsertQaFileForBatch(string qaNo, string itm, string file_rem, byte[] content, string usr)
        {
            DbQus _qus = new DbQus(Comp.Conn_DB);
            _qus.InsertQaFileForBatch(qaNo, itm, file_rem, content, usr);
        }

        #endregion



    }
}
