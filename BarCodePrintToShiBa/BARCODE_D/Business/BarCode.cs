using System;
using System.Data;
using Sunlike.Business.Data;
using System.Collections;
using Sunlike.Common.Utility;
using Sunlike.Common.CommonVar;
using System.Collections.Generic;
using System.Text;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for BarCode.
	/// </summary>
	public class BarCode : BizObject
	{	
        private static Hashtable _htBarRole = new Hashtable();
		#region ���кű���ԭ��
		/// <summary>
		/// ���кű���ԭ��
		/// </summary>
		public class BarRole
		{
            private  int _barLen;
            private string _boxFlag = "";
            private int _boxPos;
            private int _sPrdt;
            private int _ePrdt;
            private int _bPMark;
            private int _ePMark;
            private int _bSn;
            private int _eSn;
            private int _endChar = 13;
            private string _trimChar = "";
            private int _boxLen;
            private string _legalCh = "";

            private static void EnsureInit()
            {
                if (!_htBarRole.ContainsKey(Comp.CompNo))
                {
                    DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
                    DataTable _dt = _bar.GetBarRole().Tables["BAR_ROLE"];
                    BarRole _barCode = new BarRole();
                    if (_dt.Rows.Count > 0)
                    {
                        DataRow _dr = _dt.Rows[0];
                        if (!String.IsNullOrEmpty(_dr["BAR_LEN"].ToString()))
                        {
                            _barCode._barLen = Convert.ToInt32(_dr["BAR_LEN"]);
                            _barCode._boxFlag = _dr["BOX_FLAG"].ToString();
                            if (!String.IsNullOrEmpty(_dr["BOXPOS"].ToString()))
                            {
                                _barCode._boxPos = Convert.ToInt32(_dr["BOXPOS"]) - 1;
                            }
                            _barCode._sPrdt = Convert.ToInt32(_dr["S_PRDT"]) - 1;
                            _barCode._ePrdt = Convert.ToInt32(_dr["E_PRDT"]) - 1;
                            if (!String.IsNullOrEmpty(_dr["B_PMARK"].ToString()))
                            {
                                _barCode._bPMark = Convert.ToInt32(_dr["B_PMARK"]) - 1;
                            }
                            if (!String.IsNullOrEmpty(_dr["E_PMARK"].ToString()))
                            {
                                _barCode._ePMark = Convert.ToInt32(_dr["E_PMARK"]) - 1;
                            }
                            _barCode._bSn = Convert.ToInt32(_dr["B_SN"]) - 1;
                            _barCode._eSn = Convert.ToInt32(_dr["E_SN"]) - 1;
                            _barCode._endChar = Convert.ToInt32(_dr["END_CHAR"]);
                            _barCode._trimChar = _dr["TRIM_CHAR"].ToString();
                            if (!String.IsNullOrEmpty(_dr["BOX_LEN"].ToString()))
                            {
                                _barCode._boxLen = Convert.ToInt32(_dr["BOX_LEN"]);
                            }
                            _barCode._legalCh = _dr["LEGAL_CH"].ToString();
                        }
                    }
                    _htBarRole[Comp.CompNo] = _barCode;
                }
            }

            private BarRole() {}
			/// <summary>
			/// �ܳ���
			/// </summary>
            public static int BarLen
            {
                get
                {
                   EnsureInit();
                   BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                   return _barRole._barLen;
                }
            }
			/// <summary>
			/// ���ǩ����
			/// </summary>
            public static string BoxFlag
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._boxFlag;
                }
            }
			/// <summary>
			/// ������ʶ��λ��
			/// </summary>
            public static int BoxPos
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._boxPos;
                }
            }
			/// <summary>
			/// ��Ʒ��ʼλ��
			/// </summary>
            public static int SPrdt
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._sPrdt;
                }
            }
			/// <summary>
			/// ��Ʒ��ֹλ��
			/// </summary>
            public static int EPrdt
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._ePrdt;
                }
            }
			/// <summary>
			/// ������ʼλ��
			/// </summary>
            public static int BPMark
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._bPMark;
                }
            }
			/// <summary>
			/// ������ֹλ��
			/// </summary>
            public static int EPMark
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._ePMark;
                }
            }
			/// <summary>
			/// ��ˮ����ʼλ��
			/// </summary>
            public static int BSn
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._bSn;
                }
            }
			/// <summary>
			/// ��ˮ�Ž�ֹλ��
			/// </summary>
            public static int ESn
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._eSn;
                }
            }
			/// <summary>
			/// �����ַ�ASCII��
			/// </summary>
            public static int EndChar
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._endChar;
                }
            }
			/// <summary>
			/// �հ�����ַ�
			/// </summary>
            public static string TrimChar
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._trimChar;
                }
            }
			/// <summary>
			/// �����볤��
			/// </summary>
            public static int BoxLen
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._boxLen;
                }
            }
			/// <summary>
			/// �Ϸ��ַ�
			/// </summary>
            public static string LegalCh
            {
                get
                {
                    EnsureInit();
                    BarRole _barRole = _htBarRole[Comp.CompNo] as BarRole;
                    return _barRole._legalCh;
                }
            }
		}
		#endregion


      
		#region ȡ��������Ϣ
		/// <summary>
		/// ȡ��������Ϣ
		/// </summary>
		/// <param name="bar_Code">��������</param>
		/// <returns></returns>
		public static BarInfo GetBarInfo(string bar_Code)
		{
			BarInfo _info = new BarInfo();
			if(bar_Code.Length == BarRole.BarLen)
			{
				_info.Prd_No = bar_Code.Substring(BarRole.SPrdt, BarRole.EPrdt - BarRole.SPrdt + 1).Replace(BarRole.TrimChar, "");
				_info.Prd_Mark = "";
				if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
				{
					_info.Prd_Mark = bar_Code.Substring(BarRole.BPMark, BarRole.EPMark - BarRole.BPMark + 1);
				}
				_info.Serial_No = bar_Code.Substring(BarRole.BSn, BarRole.ESn - BarRole.BSn + 1).Replace(BarRole.TrimChar, "");
			}
			return _info;
		}


		#endregion

		#region �ṹ

		/// <summary>
		/// ��������
		/// </summary>
		public struct BarInfo
		{
			/// <summary>
			/// ��Ʒ����
			/// </summary>
			public string Prd_No;
			/// <summary>
			/// ��Ʒ��������
			/// </summary>
			public string Prd_Mark;
			/// <summary>
			/// ��Ʒ��ˮ��
			/// </summary>
			public string Serial_No;
		}

		#endregion

		#region ���к�
		/// <summary>
		/// ���к�
		/// </summary>
		public BarCode()
		{
		}
		#endregion

		#region ȡ�����кű���ԭ��

		/// <summary>
		/// ȡ�����кű���ԭ�򣬷ŵ�BarCode.BarRole��
		/// </summary>
		/// <returns></returns>
        //public void GetBarRole()
        //{
        //    DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
        //    DataTable _dt = _bar.GetBarRole().Tables["BAR_ROLE"];
        //    if (_dt.Rows.Count > 0)
        //    {
        //        DataRow _dr = _dt.Rows[0];
        //        if (!String.IsNullOrEmpty(_dr["BAR_LEN"].ToString()))
        //        {
        //            BarRole.BarLen = Convert.ToInt32(_dr["BAR_LEN"]);
        //            BarRole.BoxFlag = _dr["BOX_FLAG"].ToString();
        //            if (!String.IsNullOrEmpty(_dr["BOXPOS"].ToString()))
        //            {
        //                BarRole.BoxPos = Convert.ToInt32(_dr["BOXPOS"]) - 1;
        //            }
        //            BarRole.SPrdt = Convert.ToInt32(_dr["S_PRDT"]) - 1;
        //            BarRole.EPrdt = Convert.ToInt32(_dr["E_PRDT"]) - 1;
        //            if (!String.IsNullOrEmpty(_dr["B_PMARK"].ToString()))
        //            {
        //                BarRole.BPMark = Convert.ToInt32(_dr["B_PMARK"]) - 1;
        //            }
        //            if (!String.IsNullOrEmpty(_dr["E_PMARK"].ToString()))
        //            {
        //                BarRole.EPMark = Convert.ToInt32(_dr["E_PMARK"]) - 1;
        //            }
        //            BarRole.BSn = Convert.ToInt32(_dr["B_SN"]) - 1;
        //            BarRole.ESn = Convert.ToInt32(_dr["E_SN"]) - 1;
        //            BarRole.EndChar = Convert.ToInt32(_dr["END_CHAR"]);
        //            BarRole.TrimChar = _dr["TRIM_CHAR"].ToString();
        //            if (!String.IsNullOrEmpty(_dr["BOX_LEN"].ToString()))
        //            {
        //                BarRole.BoxLen = Convert.ToInt32(_dr["BOX_LEN"]);
        //            }
        //            BarRole.LegalCh = _dr["LEGAL_CH"].ToString();
        //        }
        //    }
        //}

		/// <summary>
		/// ȡ�����кű���ԭ��
		/// </summary>
		/// <returns></returns>
		public SunlikeDataSet GetBarRoleData()
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.GetBarRole();
		}

		#endregion

		#region �������кű���ԭ��

		/// <summary>
		/// �������кű���ԭ��
		/// </summary>
		/// <param name="changeDs"></param>
		/// <param name="bubbleException">�Ƿ�ϣ���׳��쳣</param>
		public DataTable UpdateBarRoleData(SunlikeDataSet changeDs, bool bubbleException)
		{
			DataTable _dtErr = null;
			Hashtable _ht = new Hashtable();
			_ht["BAR_ROLE"] = "ITM,BAR_LEN,BOX_FLAG,BOXPOS,S_PRDT,E_PRDT,B_PMARK,E_PMARK,B_SN,E_SN,END_CHAR,TRIM_CHAR,BOX_LEN,LEGAL_CH";
			this.UpdateDataSet(changeDs, _ht);
			if (changeDs.HasErrors)
			{
				_dtErr = GetAllErrors(changeDs);
				if (bubbleException)
					throw new System.Exception(changeDs.Tables[0].Rows[0].RowError);
			}
			return _dtErr;
		}

		#endregion

		#region ȡ�����кż�¼
		/// <summary>
		/// ȡ�����кż�¼
		/// </summary>
		/// <param name="SqlWhere">������䣬EX��BAR_NO='001' or BAR_NO='002'</param>
		/// <param name="HasStop">�Ƿ����ͣ�õ����к�</param>
		/// <returns></returns>
		public DataTable GetBarRecord(string SqlWhere,bool HasStop)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
            return _bar.GetBarRecord(SqlWhere, HasStop, null);
		}
		/// <summary>
		/// ȡ�����кż�¼
		/// </summary>
		/// <param name="SqlWhere">������䣬EX��BAR_NO='001' or BAR_NO='002'</param>
		/// <param name="HasStop">�Ƿ����ͣ�õ����к�</param>
		/// <param name="dataSetStop">��¼��ͣ�õ����к�</param>
		/// <returns></returns>
		public DataTable GetBarRecord(string SqlWhere,bool HasStop, DataSet dataSetStop)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
            return _bar.GetBarRecord(SqlWhere, HasStop, dataSetStop);
		}
		#endregion

		#region ȡ������������
		/// <summary>
		/// ȡ�����������ӱ�����
		/// </summary>
		/// <param name="BoxNo">������</param>
		/// <param name="OnlyFillSchema">�Ƿ�ֻ��ȡSchema</param>
		/// <returns></returns>
		public SunlikeDataSet GetBoxData(string Usr,string BoxNo, bool OnlyFillSchema)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			SunlikeDataSet _ds = _bar.GetBoxData(BoxNo, OnlyFillSchema);
			//ȡ�������ֶ�����
			PrdMark _mark = new PrdMark();
			DataTable _dtMark = _mark.GetSplitData("");
			//����������кŵı�
			DataTable _dt = new DataTable("BAR_COLLECT");
			_dt.Columns.Add("BAR_CODE");
			_dt.Columns.Add("PRD_NO");
			_dt.Columns.Add("PRD_MARK");
			_dt.Columns.Add("PRD_NAME");
			_dt.Columns.Add("WH");
			_dt.Columns.Add("BAT_NO");
			
			_ds.Tables["BAR_REC"].Columns.Add("PRD_NAME");
            if (_mark.RunByPMark(Usr))
            {
                for (int i = 0; i < _dtMark.Rows.Count; i++)
                {
                    _dt.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString());
                    _dt.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString() + "_DSC");
                }
            }
			_dt.Columns.Add("SERIAL_NO");
			DataColumn[] _dca = new DataColumn[1];
			_dca[0] = _dt.Columns["BAR_CODE"];
			_dt.PrimaryKey = _dca;
			_ds.Tables.Add(_dt);
            //���ӵ���Ȩ��
            if (!OnlyFillSchema)
            {
                if (!String.IsNullOrEmpty(Usr))
                {
                    string _pgm = "BARBOX";
                    DataTable _dtMf = _ds.Tables["MF_BOX"];
                    if (_dtMf.Rows.Count > 0)
                    {
                        string _bill_Dep = _dtMf.Rows[0]["DEP"].ToString();
                        string _bill_Usr = _dtMf.Rows[0]["USR"].ToString();
                        System.Collections.Hashtable _billRight = Users.GetBillRight(_pgm, Usr, _bill_Dep, _bill_Usr);
                        _ds.ExtendedProperties["UPD"] = _billRight["UPD"];
                        _ds.ExtendedProperties["DEL"] = _billRight["DEL"];
                        _ds.ExtendedProperties["PRN"] = _billRight["PRN"];
                    }
                }
            }
			return _ds;
		}

		/// <summary>
		/// ȡ�������������
		/// </summary>
		/// <param name="SqlWhere">������䣬EX��BOX_NO='001' or BOX_NO='002'</param>
		/// <returns></returns>
		public DataTable GetBoxData(string SqlWhere)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBoxData(SqlWhere);
			return _dt;
		}
		#endregion

		#region װ����ҵ��ȡ�����к���������
		/// <summary>
		/// װ����ҵ��ȡ�����к���������
		/// </summary>
		/// <param name="ChangedDS"></param>
		/// <returns></returns>
		public string BreakBarCode(SunlikeDataSet ChangedDS)
		{
			//ȡ�������ֶ�����
			PrdMark _mark = new PrdMark();
			DataTable _dtMark = _mark.GetSplitData("");
			SunlikeDataSet _dsCopy = ChangedDS.Copy();
			DataRow _drHead = ChangedDS.Tables["BAR_BOX"].Rows[0];
			DataTable _dtBody = ChangedDS.Tables["BAR_REC"];
			DataTable _dtBarCode = ChangedDS.Tables["BAR_COLLECT"];
			//�ж�����
			string _batNo = String.Empty;
			if(ChangedDS.ExtendedProperties["BAT_NO"] != null)
			{
				if(string.IsNullOrEmpty(ChangedDS.ExtendedProperties["BAT_NO"].ToString()))
				{
					_batNo = ChangedDS.ExtendedProperties["BAT_NO"].ToString();
				}
			}
			//����ɾ����¼
			DataRow[] _draBody = _dtBody.Select();
			DataRow[] _dra;
			string _barCode;
			for (int i=0;i<_draBody.Length;i++)
			{
				_barCode = _draBody[i]["BAR_NO"].ToString();
				_dra = _dtBarCode.Select("BAR_CODE='" + _barCode + "'");
				if (_dra.Length == 0)
				{
					if (_drHead.RowState == DataRowState.Added)
					{
						//������
						if (_draBody[i].RowState == DataRowState.Added)
						{
							_draBody[i].Delete();
						}
						else
						{
							_draBody[i].Delete();
							_draBody[i].AcceptChanges();
						}
					}
					else
					{
						//�޸���
						_draBody[i]["BOX_NO"] = System.DBNull.Value;
					}
				}
			}
			//ȡ����������к�
			System.Text.StringBuilder _sbBar = new System.Text.StringBuilder();
			for (int i=0;i<_dtBarCode.Rows.Count;i++)
			{
				_barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
				if (_sbBar.Length > 0)
				{
					_sbBar.Append(",");
				}
				_sbBar.Append(_barCode);
			}
			//�������к�����
			string _where = "";
			if (_sbBar.Length > 0)
			{
				if (!String.IsNullOrEmpty(_where))
				{
					_where += " or ";
				}
				_where += "BAR_NO in ('" + _sbBar.ToString().Replace(",","','") + "')";
			}
			if (String.IsNullOrEmpty(_where.Trim()))
			{
				_where = "1<>1";
			}
			//�����Ͽ��ȡ���к�����
			DataTable _dtBarRec = this.GetBarRecord(_where,true);
			string _wh = _drHead["WH"].ToString();			
			
			//������к�
			System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
			if (String.IsNullOrEmpty(_wh))
			{
				//���ǰ��װ��
				for (int i=0;i<_dtBarRec.Rows.Count;i++)
				{
					_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
					if(!String.IsNullOrEmpty(_dtBarRec.Rows[i]["BOX_NO"].ToString()))
					{
						_sbError.Append("���к�[" + _barCode + "]�Ѵ�������[" + _dtBarRec.Rows[i]["BOX_NO"].ToString() + "]��\n");
					}					
					else if (_dtBarRec.Rows[i]["STOP_ID"].ToString() == "T")
					{
						_sbError.Append("���к�[" + _barCode + "]��ͣ�ã�\n");
					}
					if(_dtBarRec.Rows.Count > 0)
					{
						if(_dtBarRec.Rows[0]["WH"].ToString() != _wh 
							|| (_dtBarRec.Rows[0]["BAT_NO"].ToString() != _batNo && !String.IsNullOrEmpty(_batNo)))
						{
                            this.InsertBarChange(_barCode, _dtBarRec.Rows[0]["WH"].ToString(), _wh, "", "BAR_BOX", ChangedDS.Tables["MF_BOX"].Rows[0]["USR"].ToString(), _dtBarRec.Rows[0]["BAT_NO"].ToString(), _batNo, "", "", false);
						}
					}
					DataRow _drWh = _dtBarCode.Rows.Find(new object[]{_dtBarRec.Rows[i]["BAR_NO"]});
					if(_drWh != null)
					{
						_drWh["WH"] = _dtBarRec.Rows[i]["WH"];
					}
				}
			}
			else
			{
				//������װ��
				for (int i=0;i<_dtBarCode.Rows.Count;i++)
				{
					_barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();
					_dra = _dtBarRec.Select("BAR_NO='" + _barCode + "'");
					if (_dra.Length > 0)
					{
						if (_dra[0]["WH"].ToString() != _wh || (_dra[0]["BAT_NO"].ToString() != _batNo && !String.IsNullOrEmpty(_batNo)))
						{
							//�������ڿ�λ����
                            this.InsertBarChange(_barCode, _dra[0]["WH"].ToString(), _wh, "", "BAR_BOX", ChangedDS.Tables["MF_BOX"].Rows[0]["USR"].ToString(), _dra[0]["BAT_NO"].ToString(), _batNo, "", "", false);
						}
					}
				}
			}			

			//��ʼ����
			if (String.IsNullOrEmpty(_sbError.ToString()))
			{
				string _boxNo = _drHead["BOX_NO"].ToString();
				for (int i=0;i<_dtBarCode.Rows.Count;i++)
				{
					_barCode = _dtBarCode.Rows[i]["BAR_CODE"].ToString();					
					string _prdNo = _dtBarCode.Rows[i]["PRD_NO"].ToString();
					if(String.IsNullOrEmpty(_prdNo.Trim()))
					{
						_prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1).Replace(BarCode.BarRole.TrimChar,"");
					}
					string _prdMark = _dtBarCode.Rows[i]["PRD_MARK"].ToString();
					if(String.IsNullOrEmpty(_prdMark.Trim()))
					{
						if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
						{
							_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
						}
					}
					string[] _aryPrdMark = _mark.BreakPrdMark(_prdMark);					
					_dtBarCode.Rows[i]["PRD_NO"] = _prdNo;					
					for (int j=0;j<_dtMark.Rows.Count;j++)
					{
						_dtBarCode.Rows[i][_dtMark.Rows[j]["FLDNAME"].ToString()] = _aryPrdMark[j];
					}					
					//���ұ������޴����к�
					_draBody = _dtBody.Select("BAR_NO='" + _barCode + "'");
					if (_draBody.Length == 0)
					{
						//�����һ�ʼ�¼
						DataRow _dr = _dtBody.NewRow();
						_dr["BAR_NO"] = _barCode;
						_dr["PRD_NO"] = _dtBarCode.Rows[i]["PRD_NO"];
						_dr["PRD_MARK"] = _dtBarCode.Rows[i]["PRD_MARK"];						
						_dtBody.Rows.Add(_dr);
						//�������к��Ƿ��Ѿ�����
						_dra = _dtBarRec.Select("BAR_NO='" + _barCode + "'");
						if (_dra.Length > 0)
						{
							_dr["BOX_NO"] = _dra[0]["BOX_NO"];
                            //_dr["CUS_NO"] = _dra[0]["CUS_NO"];
							_dr["WH"] = _dra[0]["WH"];
							_dr["SPC_NO"] = _dra[0]["SPC_NO"];
							_dr["PRD_NO"] = _dra[0]["PRD_NO"];
							_dr["PRD_MARK"] = _dra[0]["PRD_MARK"];
							_dr["STOP_ID"] = _dra[0]["STOP_ID"];
							_dr["BAT_NO"] = _dra[0]["BAT_NO"];
							_dr.AcceptChanges();
						}
						_dr["BOX_NO"] = _boxNo;
						_dr["WH"] = _wh;
						_dr["STOP_ID"] = "F";
						if(!String.IsNullOrEmpty(_batNo))
						{
							_dr["BAT_NO"] =  _batNo;
						}
					}
				}
			}
			else
			{
				ChangedDS = _dsCopy;
			}
			return _sbError.ToString();
		}
		#endregion

		#region ���������к�����
        		/// <summary>
		/// ���������к�����
		/// </summary>
		/// <param name="ChangedDS">���ݵ�DataSet</param>
		/// <param name="BarCodeDT">���к�ԭʼ����������кŴ���ڴ˱��У�</param>
		/// <param name="BodyTableName">���������</param>
		/// <param name="BodyBarItemName">���������кű�����Ӧ��Key��λ����</param>
		/// <param name="BodyBoxItemName">�����������ݱ�����Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="BarTableName">���кű������</param>
		/// <param name="BarItemName">���кű����������Ӧ��Key��λ����</param>
		/// <param name="BoxTableName">�����ݱ�����ƣ��������ֵ��</param>
		/// <param name="BoxItemName">�����ݱ����������Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="IsControl">�Ƿ��ϸ�ع����к�</param>
        /// <param name="IsStop">���к��Ƿ���ͣ��</param>
        /// <param name="IsExist">���к��Ƿ�������</param>
        /// <param name="IsPhFlag">���к��Ƿ�������</param>
        public void BreakBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName,
            string BodyBoxItemName, string BarTableName, string BarItemName, string BoxTableName, string BoxItemName,
            bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag)

        {
            BreakBilData(ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, "QTY", BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
        }
		/// <summary>
		/// ���������к�����
		/// </summary>
		/// <param name="ChangedDS">���ݵ�DataSet</param>
		/// <param name="BarCodeDT">���к�ԭʼ����������кŴ���ڴ˱��У�</param>
		/// <param name="BodyTableName">���������</param>
		/// <param name="BodyBarItemName">���������кű�����Ӧ��Key��λ����</param>
		/// <param name="BodyBoxItemName">�����������ݱ�����Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
        /// <param name="BodyQtyName">�����������ݱ�����Ӧ��QTY��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="BarTableName">���кű������</param>
		/// <param name="BarItemName">���кű����������Ӧ��Key��λ����</param>
		/// <param name="BoxTableName">�����ݱ�����ƣ��������ֵ��</param>
		/// <param name="BoxItemName">�����ݱ����������Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="IsControl">�Ƿ��ϸ�ع����к�</param>
        /// <param name="IsStop">���к��Ƿ���ͣ��</param>
        /// <param name="IsExist">���к��Ƿ�������</param>
        /// <param name="IsPhFlag">���к��Ƿ�������</param>
        public void BreakBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName,
            string BodyBoxItemName, string BodyQtyName, string BarTableName, string BarItemName, string BoxTableName, string BoxItemName,
            bool IsControl,bool IsStop,bool IsExist, bool IsPhFlag)
		{
            this.GetCollectData(false, ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, BodyQtyName, BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
			//��¼ԭ���к�����,�Ա�ǰ̨�жϿ�λ������
			DataTable _dtBarRec = ChangedDS.Tables["BAR_REC"];
			DataTable _dtBarError = ChangedDS.Tables["BAR_ERROR"];
			if (_dtBarError.Rows.Count == 0)
			{
				//����һ�������ֶΣ���¼���к��Ƿ����
				if (!BarCodeDT.Columns.Contains("ISEXIST"))
				{
					BarCodeDT.Columns.Add("ISEXIST");
					BarCodeDT.Columns["ISEXIST"].DefaultValue = "T";
				}
				DataColumn[] _dca = new DataColumn[2];
				_dca[0] = BarCodeDT.Columns["BAR_CODE"];
				_dca[1] = BarCodeDT.Columns["BOX_NO"];
				BarCodeDT.PrimaryKey = _dca;
				DataRow _drBarCode = null;
				foreach (DataRow dr in _dtBarRec.Rows)
				{
					_drBarCode = null;
					_drBarCode = BarCodeDT.Rows.Find(new object[2]{dr["BAR_NO"].ToString(), ""});
                    if (_drBarCode == null)
                    {
                        _drBarCode = BarCodeDT.Rows.Find(new object[2] { "", dr["BAR_NO"].ToString() });
                    }
                    if (_drBarCode == null)
                    {
                        _drBarCode = BarCodeDT.Rows.Find(new object[2] { "", dr["BOX_NO"].ToString() });
                    }
					if (_drBarCode != null)
					{
						//�Ƿ����
						_drBarCode["ISEXIST"] = dr["ISEXIST"];
						//���кſ�λ
						_drBarCode["WH_REC"] = dr["WH"];
						//û�п����ռ�ʱ���������룬����ռ���������Ĭ��Ϊ���к�����
						if (IsExist)
						{
							_drBarCode["BAT_NO"] = dr["BAT_NO"];
						}
						//���к�����
                        _drBarCode["BAT_NO_REC"] = dr["BAT_NO"];
                        //���к��Ƿ�ͣ��
                        _drBarCode["STOP_ID"] = dr["STOP_ID"];
                        //��;���
                        _drBarCode["PH_FLAG"] = dr["PH_FLAG"];
                        //���кŹ��
                        if (BarCodeDT.Columns.Contains("SPC_NO"))
                        {
                            _drBarCode["SPC_NO"] = dr["SPC_NO"];
                        }
					}
				}
				_dca = new DataColumn[1];
				_dca[0] = BarCodeDT.Columns["ITEM"];
				BarCodeDT.PrimaryKey = _dca;
			}
		}
        		/// <summary>
		/// ���������к�����
		/// </summary>
		/// <param name="IsRepair">�Ƿ����кŲ���</param>
		/// <param name="ChangedDS">���ݵ�DataSet</param>
		/// <param name="BarCodeDT">���к�ԭʼ����������кŴ���ڴ˱��У�</param>
		/// <param name="BodyTableName">���������</param>
		/// <param name="BodyBarItemName">���������кű�����Ӧ��Key��λ����</param>
		/// <param name="BodyBoxItemName">�����������ݱ�����Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="BarTableName">���кű������</param>
		/// <param name="BarItemName">���кű����������Ӧ��Key��λ����</param>
		/// <param name="BoxTableName">�����ݱ�����ƣ��������ֵ��</param>
		/// <param name="BoxItemName">�����ݱ����������Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="IsControl">�Ƿ��ϸ�ع����к�</param>
        /// <param name="IsStop">���к��Ƿ���ͣ��</param>
        /// <param name="IsExist">���к��Ƿ�������</param>
        /// <param name="IsPhFlag">������־</param>
        private void GetCollectData(bool IsRepair, DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName,
            string BodyBarItemName, string BodyBoxItemName, string BarTableName, string BarItemName, string BoxTableName,
            string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag)
        {
            GetCollectData(IsRepair, ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, "QTY", BodyBoxItemName, BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
        }
		/// <summary>
		/// ���������к�����
		/// </summary>
		/// <param name="IsRepair">�Ƿ����кŲ���</param>
		/// <param name="ChangedDS">���ݵ�DataSet</param>
		/// <param name="BarCodeDT">���к�ԭʼ����������кŴ���ڴ˱��У�</param>
		/// <param name="BodyTableName">���������</param>
		/// <param name="BodyBarItemName">���������кű�����Ӧ��Key��λ����</param>
		/// <param name="BodyBoxItemName">�����������ݱ�����Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
        /// <param name="BodyQtyName">�����������ݱ�����Ӧ��QTY��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="BarTableName">���кű������</param>
		/// <param name="BarItemName">���кű����������Ӧ��Key��λ����</param>
		/// <param name="BoxTableName">�����ݱ�����ƣ��������ֵ��</param>
		/// <param name="BoxItemName">�����ݱ����������Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
        /// <param name="IsControl">�Ƿ��ϸ�ع����к�</param>
        /// <param name="IsStop">���к��Ƿ���ͣ��</param>
        /// <param name="IsExist">���к��Ƿ�������</param>
        /// <param name="IsPhFlag">������־</param>
		private void GetCollectData(bool IsRepair, DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName,
            string BodyBarItemName, string BodyBoxItemName, string BodyQtyName, string BarTableName, string BarItemName, string BoxTableName,
            string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag)
		{
			//ȡ�ñ�ͷ��Key
			string[] _headerKey = new string[ChangedDS.Tables[0].PrimaryKey.Length];
			for (int i=0;i<ChangedDS.Tables[0].PrimaryKey.Length;i++)
			{
				_headerKey[i] = ChangedDS.Tables[0].PrimaryKey[i].ColumnName;
			}
			DataTable _dtBody = ChangedDS.Tables[BodyTableName];
			DataTable _dtBarCode = ChangedDS.Tables[BarTableName];
			DataTable _dtBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBox = ChangedDS.Tables[BoxTableName];
			}
			//��¼ԭ�еı���Ϊ��ǰ̨������Щ��¼����ȡ�۸�
            if (ChangedDS.Tables.Contains(BodyTableName))
            {
                StringBuilder _bodyFlag = new StringBuilder();
                foreach (DataRow dr in ChangedDS.Tables[BodyTableName].Select())
                {
                    if (!String.IsNullOrEmpty(_bodyFlag.ToString()))
                    {
                        _bodyFlag.Append(",");
                    }
                    _bodyFlag.Append(dr["ITM"].ToString());
                }
                ChangedDS.ExtendedProperties["INHERE_DATA"] = _bodyFlag;
            }
			//ȡ������������к�
			Hashtable _htBarCode = new Hashtable();
			DataRow[] _draBarCode = _dtBarCode.Select();
			for (int i=0;i<_draBarCode.Length;i++)
			{
				_htBarCode[_draBarCode[i]["BOX_NO"].ToString()] = "";
				_htBarCode[_draBarCode[i]["BAR_CODE"].ToString()] = "";
			}
			//������������кŷֿ�
			System.Text.StringBuilder _sbBox = new System.Text.StringBuilder();
			System.Text.StringBuilder _sbBar = new System.Text.StringBuilder();
			Hashtable _htCollect = new Hashtable();
			string _barCode,_boxNo;
			for (int i=0;i<BarCodeDT.Rows.Count;i++)
			{
				_htCollect[BarCodeDT.Rows[i]["BOX_NO"].ToString()] = "";
				_htCollect[BarCodeDT.Rows[i]["BAR_CODE"].ToString()] = "";
				_barCode = BarCodeDT.Rows[i]["BOX_NO"].ToString();
				if (String.IsNullOrEmpty(_barCode))
				{
					_barCode = BarCodeDT.Rows[i]["BAR_CODE"].ToString();
					BarCodeDT.Rows[i]["BOX_NO"] = "";
				}
				else if (_htBarCode[_barCode] == null)
				{
					BarCodeDT.Rows[i]["BAR_CODE"] = "";
				}
				if (_htBarCode[_barCode] == null)
				{
					//��������ŵ�_boxList�У��������_barList
					if (_barCode.Substring(BarRole.BoxPos,1) == BarRole.BoxFlag)
					{
						if (_sbBox.ToString().IndexOf(_barCode) < 0)
						{
							if (_sbBox.Length > 0)
							{
								_sbBox.Append(",");
							}
							_sbBox.Append(_barCode);
						}
					}
					else
					{
						if (_sbBar.Length > 0)
						{
							_sbBar.Append(",");
						}
						_sbBar.Append(_barCode);
					}
				}
			}
			//�������к�����
			string _where = "";
			string _whereBox = "";
			if (_sbBox.Length > 0)
			{
				_whereBox = "BOX_NO in ('" + _sbBox.ToString().Replace(",","','") + "')";
			}
			else
			{
				_whereBox = "1<>1";
			}
			if (_sbBar.Length > 0)
			{
				_where += "BAR_NO in ('" + _sbBar.ToString().Replace(",","','") + "')";
			}
			if (String.IsNullOrEmpty(_where.Trim()))
			{
				_where = "1<>1";
			}
			DataTable _dtBarError = ChangedDS.Tables["BAR_ERROR"];
			DataRow _drBarError;
			if (_dtBarError == null)
			{
				_dtBarError = new DataTable("BAR_ERROR");
				_dtBarError.Columns.Add("UNUSE");
				DataColumn _dc = new DataColumn("BAR_NO");
				_dtBarError.Columns.Add(_dc);
				_dc = new DataColumn("BOX_NO");
				_dtBarError.Columns.Add(_dc);
				_dc = new DataColumn("REM");
				_dtBarError.Columns.Add(_dc);
				ChangedDS.Tables.Add(_dtBarError);
			}
			else
			{
				_dtBarError.Clear();
			}
			//�������Զ��޳������к�
            DataTable _dtBarDel = ChangedDS.Tables["BAR_DEL"];
			if (_dtBarDel == null)
			{
				_dtBarDel = new DataTable("BAR_DEL");
				DataColumn _dc = new DataColumn("BAR_NO");
				_dtBarDel.Columns.Add(_dc);
				_dc = new DataColumn("BOX_NO");
				_dtBarDel.Columns.Add(_dc);
				_dc = new DataColumn("REM");
				_dtBarDel.Columns.Add(_dc);
				ChangedDS.Tables.Add(_dtBarDel);
			}
			else
			{
				_dtBarDel.Clear();
			}
			//���������ֶ�
			ArrayList _alBox = new ArrayList();
			ArrayList _alBar = new ArrayList();
			int _maxWhereLength = 1024;
			string _subWhere;
			int _pos;
			while (true)
			{
				if (_whereBox.Length > _maxWhereLength)
				{
					_subWhere = _whereBox.Substring(0,_maxWhereLength-1);
					_pos = _subWhere.LastIndexOf(",");
					_alBox.Add(_subWhere.Substring(0,_pos) + ")");
					_whereBox = "BOX_NO in (" + _whereBox.Substring(_pos+1,_whereBox.Length-_pos-1);
				}
				else
				{
					_alBox.Add(_whereBox);
					break;
				}
			}
			_maxWhereLength = 10240;
			while (true)
			{
				if (_where.Length > _maxWhereLength)
				{
					_subWhere = _where.Substring(0,_maxWhereLength-1);
					_pos = _subWhere.LastIndexOf(",");
					_alBar.Add(_subWhere.Substring(0,_pos) + ")");
					_where = "BAR_NO in (" + _where.Substring(_pos+1,_where.Length-_pos-1);
				}
				else
				{
					_alBar.Add(_where);
					break;
				}
			}
			//�����Ͽ��ȡ���к�����
			SunlikeDataSet _dsBar = new SunlikeDataSet();
			DataTable _dtBarBox = null;
//			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				for (int i=0;i<_alBox.Count;i++)
				{
					_dtBarBox = this.GetBoxData(_alBox[i].ToString());
					if (_dsBar.Tables["BAR_BOX"] == null)
					{
						_dsBar.Tables.Add(_dtBarBox.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBarBox,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBarBox = _dsBar.Tables["BAR_BOX"];
			}
			DataTable _dtBarRec;
			for (int i=0;i<_alBar.Count;i++)
			{
				DataSet _dsBarRecStop = new DataSet();
				_dtBarRec = this.GetBarRecord(_alBar[i].ToString(),IsStop,_dsBarRecStop);
				//����һ�������ֶΣ���¼���к��Ƿ����
				if (!_dtBarRec.Columns.Contains("ISEXIST"))
				{
					_dtBarRec.Columns.Add("ISEXIST");
					_dtBarRec.Columns["ISEXIST"].DefaultValue = "T";
                }
                //�ж��Ѿ����������,�����кű������ʹ��
                foreach (DataRow dr in _dtBarRec.Rows)
                {
                    if (!String.IsNullOrEmpty(dr["BOX_NO"].ToString()))
                    {
                        if (_htBarCode.ContainsKey(dr["BOX_NO"].ToString()))
                        {
                            _drBarError = _dtBarError.NewRow();
                            _drBarError["BAR_NO"] = dr["BAR_NO"].ToString();
                            _drBarError["BOX_NO"] = dr["BOX_NO"].ToString();
                            _drBarError["REM"] = "RCID=INV.HINT.SAMEDATA";//"���кŹ��������룬����ͬʱ���������ʼ�¼��";
                            _dtBarError.Rows.Add(_drBarError);
                        }
                    }
                }
				if (_dsBar.Tables["BAR_REC"] == null)
				{
					_dsBar.Tables.Add(_dtBarRec.Copy());
				}
				else
				{
					_dsBar.Merge(_dtBarRec,true,MissingSchemaAction.AddWithKey);
				}
				if (_dsBarRecStop.Tables["BAR_REC_STOP"] != null)
				{
					if (_dsBar.Tables["BAR_REC_STOP"] == null)
					{
						_dsBar.Tables.Add(_dsBarRecStop.Tables["BAR_REC_STOP"].Copy());
					}
					else
					{
						_dsBar.Merge(_dsBarRecStop, true, MissingSchemaAction.AddWithKey);
					}
				}
			}
			for (int i=0;i<_alBox.Count;i++)
			{
				_dtBarRec = this.GetBarRecord(_alBox[i].ToString(),IsStop);
				if (_dsBar.Tables["BAR_REC"] == null)
				{
					_dsBar.Tables.Add(_dtBarRec.Copy());
				}
				else
				{
					for (int j=0;j<_dtBarRec.Rows.Count;j++)
					{
						try
						{
                            //�ж�֮ǰ����������к�
                            if (_htBarCode.ContainsKey(_dtBarRec.Rows[j]["BAR_NO"].ToString()))
                            {
                                throw new Exception();
                            }
							_dsBar.Tables["BAR_REC"].ImportRow(_dtBarRec.Rows[j]);
						}
						catch
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["BAR_NO"] = _dtBarRec.Rows[j]["BAR_NO"].ToString();
							_drBarError["BOX_NO"] = _dtBarRec.Rows[j]["BOX_NO"].ToString();
							_drBarError["REM"] = "RCID=INV.HINT.SAMEDATA";//"���кŹ��������룬����ͬʱ���������ʼ�¼��";
							_dtBarError.Rows.Add(_drBarError);
						}
					}
				}
			}
			_dtBarRec = _dsBar.Tables["BAR_REC"];
			//�����Ƿ��д�������
			if (_sbBox.Length > 0 && _dtBarBox != null)
			{
				string[] _aryBoxList = _sbBox.ToString().Split(new char[] {','});
				for (int i=0;i<_aryBoxList.Length;i++)
				{
					//2006-06-07�޸Ĵ�_dtBarBox��Find���������Ƿ����������
					DataRow _drBoxExist = _dtBarBox.Rows.Find(_aryBoxList[i]);
					if (_drBoxExist == null)
					{
						_drBarError = _dtBarError.NewRow();
						_drBarError["BOX_NO"] = _aryBoxList[i];
						_drBarError["REM"] = "RCID=INV.HINT.BOXNOEXISTS";//"�����벻���ڣ�";
						_dtBarError.Rows.Add(_drBarError);
					}
					else if (_drBoxExist["STOP_ID"].ToString() == "T")
					{
						bool _isBreak = false;
						DataRow[] _draBarRecCheckedNull = _dtBarRec.Select("BOX_NO='" + _aryBoxList[i] + "'");
						//�Ѿ�����
						if (_draBarRecCheckedNull.Length == 0)
						{
							if (_dsBar.Tables.Contains("BAR_REC_STOP"))
							{
								_draBarRecCheckedNull = _dsBar.Tables["BAR_REC_STOP"].Select("BOX_NO='" + _aryBoxList[i] + "'");
								if (_draBarRecCheckedNull.Length == 0)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BOX_NO"] = _aryBoxList[i];
									_drBarError["REM"] = "RCID=INV.HINT.HASBREAK";//�Ѿ�����";
									_dtBarError.Rows.Add(_drBarError);
									_isBreak = true;
								}
								else
								{
									foreach (DataRow drStop in _draBarRecCheckedNull)
									{
										_dtBarRec.ImportRow(drStop);
									}
								}
							}
							else
							{
								_drBarError = _dtBarError.NewRow();
								_drBarError["BOX_NO"] = _aryBoxList[i];
								_drBarError["REM"] = "RCID=INV.HINT.HASBREAK";//�Ѿ�����";
								_dtBarError.Rows.Add(_drBarError);
								_isBreak = true;
							}
						}
						if (!_isBreak && IsControl && IsExist)
						{
							//���кŹ��ƣ��Ѳ���
							_drBarError = _dtBarError.NewRow();
							_drBarError["BOX_NO"] = _aryBoxList[i];
							_drBarError["REM"] = "RCID=INV.HINT.HASSTOP";//�Ѿ�ͣ��";
							_dtBarError.Rows.Add(_drBarError);
						}
					}
					//Bug tag 1
					else if (IsControl && !IsExist && !String.IsNullOrEmpty(_drBoxExist["WH"].ToString()))
					{
						//���кŹ��ƣ�������ӣ���λ��Ϊ��
						_drBarError = _dtBarError.NewRow();
						_drBarError["BOX_NO"] = _aryBoxList[i];
						_drBarError["REM"] = "RCID=INV.HINT.BOXEXISTS,PARAM="+_aryBoxList[i]+",PARAM="+_drBoxExist["WH"].ToString();//XXX���벻�Ϸ��������λӦ��Ϊ[XXX]";
						_dtBarError.Rows.Add(_drBarError);
					}
				}
			}
			//�����Ƿ��д����к�
			if (_sbBar.Length > 0)
			{
				string[] _aryBarList = _sbBar.ToString().Split(new char[] {','});
				for (int i=0;i<_aryBarList.Length;i++)
				{
					DataRow[] _dra = _dtBarRec.Select("BAR_NO='" + _aryBarList[i] + "'");
					if (_dra.Length == 0)
					{
						if (IsControl && IsExist)
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["BAR_NO"] = _aryBarList[i];
							_drBarError["REM"] = "RCID=INV.HINT.BARCODENOEXIST";//"���кŲ����ڻ���ͣ�ã�";
							_dtBarError.Rows.Add(_drBarError);
						}
						else
						{
							//������кŲ����ƣ�����BAR_REC�������������кţ�����ֻ�Ǽ�һ����¼����������
							DataRow _dr = _dtBarRec.NewRow();
							_dr["BAR_NO"] = _aryBarList[i];
							_dr["WH"] = "";
							if (_dsBar.Tables["BAR_REC_STOP"] != null)
							{
								DataRow _drStop = _dsBar.Tables["BAR_REC_STOP"].Rows.Find(_aryBarList[i]);
								if (_drStop == null)
								{
									//�����ͣ�����к���Ҳ�����ҵ������кŵĻ��������к���Ϊ������
									_dr["ISEXIST"] = "F";
								}
								else
								{
									_dr["WH"] = _drStop["WH"];
									_dr["BAT_NO"] = _drStop["BAT_NO"];
                                    _dr["STOP_ID"] = "T";
                                    _dr["PH_FLAG"] = _drStop["PH_FLAG"];
								}
							}
							else
							{
								//�����ͣ�����к���Ҳ�����ҵ������кŵĻ��������к���Ϊ������
								_dr["ISEXIST"] = "F";
							}
							_dtBarRec.Rows.Add(_dr);
						}
					}
					else if (IsStop && IsControl && _dra[0]["STOP_ID"].ToString() != "T" && !String.IsNullOrEmpty(_dra[0]["WH"].ToString()))
					{
						_drBarError = _dtBarError.NewRow();
						_drBarError["BAR_NO"] = _aryBarList[i];
						_drBarError["REM"] = "RCID=INV.HINT.BAREXISTS";//"���к��Ѵ��ڣ�";
						_dtBarError.Rows.Add(_drBarError);
					}
                    else if (IsControl && IsPhFlag && (_dra[0]["PH_FLAG"].ToString() == "" || _dra[0]["PH_FLAG"].ToString() == "F"))
                    {
                        _drBarError = _dtBarError.NewRow();
                        _drBarError["BAR_NO"] = _aryBarList[i];
                        _drBarError["REM"] = "RCID=COMMON.HINT.PH_FLAG1";//"���к�״̬Ϊδ������";
                        _dtBarError.Rows.Add(_drBarError);
                    }
                    else if (IsControl && !IsPhFlag && _dra[0]["PH_FLAG"].ToString() == "T")
                    {
                        _drBarError = _dtBarError.NewRow();
                        _drBarError["BAR_NO"] = _aryBarList[i];
                        _drBarError["REM"] = "RCID=COMMON.HINT.PH_FLAG2";//"���к�״̬Ϊδ������";
                        _dtBarError.Rows.Add(_drBarError);
                    }
				}
			}
			if (_dtBarError.Rows.Count == 0)
			{
				//����ɾ����¼
				DataRow[] _draBody,_draBox;
				int _keyItm,_boxItm,_qty;
				Hashtable _htBoxDel = new Hashtable();
				_draBarCode = _dtBarCode.Select();
				int _countBarCode = _draBarCode.Length;
				bool _isReset = false;
				for (int i=0;i<_countBarCode;i++)
				{
					_barCode = _draBarCode[i]["BAR_CODE"].ToString();
					_boxNo = _draBarCode[i]["BOX_NO"].ToString();
					//�Ҳ�������ɾ��
					if ((!String.IsNullOrEmpty(_boxNo) && _htCollect[_boxNo] == null) || (String.IsNullOrEmpty(_boxNo) && _htCollect[_barCode] == null))
					{
						//����ǲ��룬ֻҪɾ�����кŵļ�¼�Ϳ����ˣ����ռ�����Ҫ�ۼ�����������ݵ�����
						if (IsRepair)
						{
							_draBarCode[i].Delete();
						}
						else if (!String.IsNullOrEmpty(BarItemName))
						{
							_keyItm = Convert.ToInt32(_draBarCode[i][BarItemName]);
							_draBarCode[i].Delete();
							_draBody = _dtBody.Select(BodyBarItemName + "=" + _keyItm.ToString());
							if (_draBody.Length > 0)
							{
								//��������������
								if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName)
									&& !String.IsNullOrEmpty(_boxNo) && _htBoxDel[_boxNo] == null
									&& Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "T")
								{
									_htBoxDel[_boxNo] = "";
									_boxItm = Convert.ToInt32(_draBody[0][BodyBoxItemName]);
									_draBox = _dtBox.Select(BoxItemName + "='" + _boxItm.ToString() + "'");
									_qty = Math.Abs(Convert.ToInt32(_draBox[0]["QTY"])) - 1;
									if (_qty > 0)
									{
                                        _draBox[0]["QTY"] = _qty;
									}
									else
									{
										_draBox[0].Delete();
									}
								}
								//�����Ʒ������Ӧ����
                                _qty = Math.Abs(Convert.ToInt32(_draBody[0][BodyQtyName])) - 1;
								if (_qty > 0)
								{
                                    _draBody[0][BodyQtyName] = _qty;
								}
								else
								{
									_draBody[0].Delete();
								}
							}
						}
						_isReset = true;
					}
				}
				//����ITM
				if (_isReset)
				{
					_draBarCode = _dtBarCode.Select();
					for (int i=0;i<_draBarCode.Length;i++)
					{
						_draBarCode[i]["ITM"] = i + 1;
					}
				}
				ChangedDS.Tables.Add(_dtBarRec.Copy());
				if (_dtBarBox != null)
				{
					ChangedDS.Tables.Add(_dtBarBox.Copy());
				}
			}
//			if (!IsExist && _dtBarError.Rows.Count == 0)
//			{
//				foreach (DataRow dr in ChangedDS.Tables["BAR_COLLECT"].Rows)
//				{
//					if (!String.IsNullOrEmpty(dr["BAR_CODE"].ToString()))
//					{
//						DataRow _dr = ChangedDS.Tables["BAR_REC"].Rows.Find(new string[1]{dr["BAR_CODE"].ToString()});
//						if (_dr != null)
//						{
//							if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
//							{
//								_dr["BAT_NO"] = dr["BAT_NO"];
//							}
//						}
//					}
//					else
//					{
//						//������
//						DataRow[] _draBarCollect = ChangedDS.Tables["BAR_REC"].Select("BOX_NO='"+dr["BOX_NO"].ToString()+"'");
//						foreach (DataRow drCollect in _draBarCollect)
//						{
//							if (!String.IsNullOrEmpty(dr["BAT_NO"].ToString()))
//							{
//								drCollect["BAT_NO"] = dr["BAT_NO"];
//							}
//						}
//					}
//				}
//			}
		}

		/// <summary>
        /// ���������к�����(�÷������������ֶ���������������)
		/// </summary>
		/// <param name="ChangedDS">���ݵ�DataSet</param>
		/// <param name="BarCodeDT">���к�ԭʼ����������кŴ���ڴ˱��У�</param>
		/// <param name="BodyTableName">���������</param>
		/// <param name="BodyBarItemName">���������кű�����Ӧ��Key��λ����</param>
		/// <param name="BodyBoxItemName">�����������ݱ�����Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="BodyWhName">����Ŀ���λ����</param>
        /// <param name="BodyQtyName">�������������(���̵㵥�������ֶ�����ΪQTY2,�ʼӸ��ֶ�)</param>
		/// <param name="BarTableName">���кű������</param>
		/// <param name="BarItemName">���кű����������Ӧ��Key��λ����</param>
		/// <param name="BoxTableName">�����ݱ�����ƣ��������ֵ��</param>
		/// <param name="BoxItemName">�����ݱ����������Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="IsControl">�Ƿ��ϸ�ع����к�</param>
        /// <param name="IsStop">���к��Ƿ���ͣ�û򲻴���</param>
        /// <param name="IsExist">���к��Ƿ�������</param>
        /// <param name="IsPhFlag">������־</param>
		/// <param name="ParaHT">ȡ�û�Ʒ��������Ҫ���Ĳ�������ȡ��������null
		/// PrdField����Ʒ������λ������ȡ��Ʒ���ƣ��˲�����հף�
		/// MarkDscName�������ֶ�������λ�����磺�ֶ���λΪSize���ֶ�������λΪSize_DSC������"_DSC"���ɣ���ȡ�����ֶ����ƣ��˲�����հף�
		/// WhNameField����������λ������ȡ��λ���ƣ��˲�����հף�
		/// UnitField����λ��λ����������λ���˲�����հף�
		/// UnitNameField����λ������λ������ȡ��λ���ƣ��˲�����հף�
		/// UpField����Ʒ������λ������ȡ��Ʒ�������ߣ��˲�����հף�
		/// BilDD���������ڣ�Ҫȡ��Ʒ�������ߣ����뵥�����ڣ����򴫵��죩
		/// CusNo���ͻ����ţ�Ҫȡ��Ʒ�������ߣ�����ͻ����ţ����򴫿գ�
		/// </param>
        public void BreakBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName, string BodyBoxItemName, string BodyWhName, string BodyQtyName, string BarTableName, string BarItemName, string BoxTableName, string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag, System.Collections.Hashtable ParaHT)
		{
            this.BreakBilData(ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
			DataTable _dtBarError = ChangedDS.Tables["BAR_ERROR"];
			DataRow _drBarError;
			string[] _headerKey = new string[ChangedDS.Tables[0].PrimaryKey.Length];
			for (int i=0;i<ChangedDS.Tables[0].PrimaryKey.Length;i++)
			{
				_headerKey[i] = ChangedDS.Tables[0].PrimaryKey[i].ColumnName;
			}
			DataTable _dtBody = ChangedDS.Tables[BodyTableName];
			DataTable _dtBarCode = ChangedDS.Tables[BarTableName];
			DataTable _dtBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBox = ChangedDS.Tables[BoxTableName];
			}
			DataTable _dtBarRec = ChangedDS.Tables["BAR_REC"];
			DataTable _dtBarBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBarBox = ChangedDS.Tables["BAR_BOX"];
			}
			if (_dtBarError.Rows.Count == 0)
			{
				DataRow[] _draBox;
				DataRow _drBody,_drBarCode,_drBox;
				int _keyItm,_boxItm,_qty;
				string _barCode,_boxNo,_batNo;
				DataRow[] _draBarCode = _dtBarCode.Select();
				int _countBarCode = _draBarCode.Length;
				DataRow[] _draBody = _dtBody.Select();
				int _countBody = _draBody.Length;
				//ȡ�������ֶ�����
				PrdMark _mark = new PrdMark();
				DataTable _dtMark = _mark.GetSplitData("");
				//������������кŷŵ�Hashtable��
				System.Collections.Hashtable _htBarCode = new System.Collections.Hashtable();
				_draBarCode = _dtBarCode.Select();
				for (int i=0;i<_draBarCode.Length;i++)
				{
					_htBarCode[_draBarCode[i]["BAR_CODE"].ToString()] = "";
				}
				//��������ŵ�_boxList��
				System.Collections.Hashtable _htBox = new System.Collections.Hashtable();
				DataRow[] _draBarCollect = BarCodeDT.Select();
				for (int i=0;i<_draBarCollect.Length;i++)
				{
					_boxNo = _draBarCollect[i]["BOX_NO"].ToString();
					if (!String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] == null)
					{
						_htBox[_boxNo] = "";
					}
				}
				System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
				for (int i=0;i<_dtBarCode.Rows.Count;i++)
				{
					if (_dtBarCode.Rows[i].RowState != DataRowState.Deleted)
					{
						if (_htBoxNo[_dtBarCode.Rows[i]["BOX_NO"].ToString()] == null)
						{
							_htBoxNo[_dtBarCode.Rows[i]["BOX_NO"].ToString()] = i;
						}
					}
				}
				for (int i=0;i<_dtBarRec.Rows.Count;i++)
				{
					_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
					_batNo = _dtBarRec.Rows[i]["BAT_NO"].ToString();
					//��BAR_CODE���в��Ҵ����к��Ƿ����
					if (_htBarCode[_barCode] == null)
					{
						//������š�����
						string _prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
						_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
						string _prdMark = "";
						if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
						{
							_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
						}
						string[] _markList = _mark.BreakPrdMark(_prdMark);
						_boxNo = _dtBarRec.Rows[i]["BOX_NO"].ToString();
						//�Ƿ�Ҫ����������
						if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName)
							&& !String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] != null
							&& Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "T")
						{
							if (String.IsNullOrEmpty(_htBoxNo[_boxNo].ToString()) || _htBoxNo[_boxNo] == null)
							{
								_drBarCode = null;
							}
							else
							{
								_drBarCode = _dtBarCode.Rows[Convert.ToInt32(_htBoxNo[_boxNo])];
							}
							//���кű����Ƿ��д�BOX_NO
							if (_drBarCode != null)
							{
								_boxItm = Convert.ToInt32(_dtBody.Select(BodyBarItemName + "=" + _drBarCode[BarItemName].ToString())[0][BodyBoxItemName]);
								_draBody = _dtBody.Select("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
									+ "' and " + BodyBoxItemName + "=" + _boxItm.ToString());
								if (_draBody.Length > 0)
								{
									//����������һ
                                    _draBody[0][BodyQtyName] = Math.Abs(Convert.ToInt32(_draBody[0][BodyQtyName])) + 1;
									_keyItm = Convert.ToInt32(_draBody[0][BodyBarItemName]);
								}
								else
								{
									//����һ�ʱ���
									_drBody = _dtBody.NewRow();
									for (int j=0;j<_headerKey.Length;j++)
									{
										_drBody[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
									}
									_countBody ++;
									_drBody["ITM"] = _countBody;
									_drBody["PRD_NO"] = _prdNo;
									_drBody["PRD_MARK"] = _prdMark;
									for (int j=0;j<_markList.Length;j++)
									{
										_drBody[_dtMark.Rows[j]["FLDNAME"].ToString()] = _markList[j];
									}
									_drBody[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
                                    _drBody[BodyQtyName] = 1;
									_drBody[BodyBoxItemName] = _boxItm;
									_drBody["BAT_NO"] = _batNo;
									_dtBody.Rows.Add(_drBody);
									_keyItm = Convert.ToInt32(_drBody[BodyBarItemName]);
								}
							}
							else
							{
								string _content = _dtBarBox.Select("BOX_NO='" + _boxNo + "'")[0]["CONTENT"].ToString();
								_draBox = _dtBox.Select("PRD_NO='" + _prdNo + "' and CONTENT='" + _content + "'");
								//���������޴˻�Ʒ������ȵļ�¼
								if (_draBox.Length > 0)
								{
                                    _qty = Math.Abs(Convert.ToInt32(_draBox[0][BodyQtyName]));
									_boxItm = Convert.ToInt32(_draBox[0][BoxItemName]);
									//��������һ
                                    _draBox[0][BodyQtyName] = _qty + 1;
									//���ұ���ĸ����¼
									_draBody = _dtBody.Select(BodyBoxItemName + "=" + _boxItm.ToString()
										+ " and PRD_MARK='" + _prdMark + "'");
									if (_draBody.Length > 0)
									{
                                        _draBody[0][BodyQtyName] = Math.Abs(Convert.ToInt32(_draBody[0][BodyQtyName])) + 1;
										_keyItm = Convert.ToInt32(_draBody[0][BodyBarItemName]);
									}
									else
									{
										//��һ�ʱ���
										_drBody = _dtBody.NewRow();
										for (int j=0;j<_headerKey.Length;j++)
										{
											_drBody[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
										}
										_countBody ++;
										_drBody["ITM"] = _countBody;
										_drBody["PRD_NO"] = _prdNo;
										_drBody["PRD_MARK"] = _prdMark;
										for (int j=0;j<_markList.Length;j++)
										{
											_drBody[_dtMark.Rows[j]["FLDNAME"].ToString()] = _markList[j];
										}
										_drBody[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
                                        _drBody[BodyQtyName] = 1;
										_drBody[BodyBoxItemName] = _boxItm;
										_drBody["BAT_NO"] = _batNo;
										_dtBody.Rows.Add(_drBody);
										_keyItm = Convert.ToInt32(_drBody[BodyBarItemName]);
									}
								}
								else
								{
									//��һ�������ݼ�¼
									_drBox = _dtBox.NewRow();
									for (int j=0;j<_headerKey.Length;j++)
									{
										_drBox[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
									}
									_drBox["ITM"] = _dtBox.Select().Length + 1;
									_drBox["PRD_NO"] = _prdNo;
									_drBox["CONTENT"] = _content;
                                    _drBox["QTY"] = 1;
									_drBox[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
									_dtBox.Rows.Add(_drBox);
									_boxItm = Convert.ToInt32(_drBox[BoxItemName]);
									//��һ�ʱ���
									_drBody = _dtBody.NewRow();
									for (int j=0;j<_headerKey.Length;j++)
									{
										_drBody[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
									}
									_countBody ++;
									_drBody["ITM"] = _countBody;
									_drBody["PRD_NO"] = _prdNo;
									_drBody["PRD_MARK"] = _prdMark;
									for (int j=0;j<_markList.Length;j++)
									{
										_drBody[_dtMark.Rows[j]["FLDNAME"].ToString()] = _markList[j];
									}
									_drBody[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
                                    _drBody[BodyQtyName] = 1;
									_drBody[BodyBoxItemName] = _boxItm;
									_drBody["BAT_NO"] = _batNo;
									_dtBody.Rows.Add(_drBody);
									_keyItm = Convert.ToInt32(_drBody[BodyBarItemName]);
								}
								if (String.IsNullOrEmpty(_htBoxNo[_boxNo].ToString()) || _htBoxNo[_boxNo].ToString() == null)
								{
									_htBoxNo[_boxNo] = _dtBarCode.Rows.Count;
								}
							}
						}
						else
						{
							//�Զ����䴦��
							_keyItm = 0;
							if (!String.IsNullOrEmpty(_boxNo))
							{
								if (_htBox[_boxNo] == null)
								{
									if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
									{
										_boxNo = "";
									}
									else
									{
										_drBarError = _dtBarError.NewRow();
										_drBarError["BAR_NO"] = _barCode;
										_drBarError["REM"] = "RCID=INV.HINT.NOTBREAK";//"���к��Ѿ�װ�䣬�㲻�ܲ���¼�룡";
										_dtBarError.Rows.Add(_drBarError);
									}
								}
							}
							if (_dtBarError.Rows.Count == 0)
							{
								//�ڱ����в��Ҵ˻�Ʒ�Ƿ����
								StringBuilder _where = new StringBuilder("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark + "'");
								if (IsControl && !String.IsNullOrEmpty(_dtBarRec.Rows[i]["WH"].ToString()))
								{
									_where.Append(" and " + BodyWhName + "='" + _dtBarRec.Rows[i]["WH"].ToString() + "'");
								}
								if (!String.IsNullOrEmpty(_dtBarRec.Rows[i]["BAT_NO"].ToString()))
								{
									_where.Append(" and BAT_NO='" + _batNo + "'");
								}
								if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
								{
									_where.Append(" and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
								}
								_draBody = _dtBody.Select(_where.ToString());
								//�ҵ��д˻�Ʒ����������һ�������һ��
								if (_draBody.Length > 0)
								{
                                    _draBody[0][BodyQtyName] = Math.Abs(Convert.ToInt32(_draBody[0][BodyQtyName])) + 1;
									_keyItm = Convert.ToInt32(_draBody[0][BodyBarItemName]);
								}
								else
								{
									_drBody = _dtBody.NewRow();
									for (int j=0;j<_headerKey.Length;j++)
									{
										_drBody[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
									}
									_countBody ++;
									_drBody["ITM"] = _countBody;
									_drBody["PRD_NO"] = _prdNo;
									_drBody["PRD_MARK"] = _prdMark;
									for (int j=0;j<_markList.Length;j++)
									{
										_drBody[_dtMark.Rows[j]["FLDNAME"].ToString()] = _markList[j];
									}
									_drBody[BodyWhName] = _dtBarRec.Rows[i]["WH"].ToString();
                                    _drBody[BodyQtyName] = 1;
									_drBody["BAT_NO"] = _batNo;
									_dtBody.Rows.Add(_drBody);
									_keyItm = Convert.ToInt32(_drBody[BodyBarItemName]);
								}
							}
						}
						if (_dtBarError.Rows.Count == 0)
						{
							//���뵽BAR_CODE����
							_drBarCode = _dtBarCode.NewRow();
							for (int j=0;j<_headerKey.Length;j++)
							{
								_drBarCode[_headerKey[j]] = ChangedDS.Tables[0].Rows[0][_headerKey[j]];
							}
							_countBarCode ++;
							_drBarCode["ITM"] = _countBarCode;
							_drBarCode["PRD_NO"] = _prdNo;
							_drBarCode["PRD_MARK"] = _prdMark;
							_drBarCode["BAR_CODE"] = _barCode;
							_drBarCode["BOX_NO"] = _boxNo;
							_drBarCode[BarItemName] = _keyItm;
							_dtBarCode.Rows.Add(_drBarCode);
							_htBarCode[_barCode] = "";
						}
					}
				}
				//ȡ�û�Ʒ������ϵ�����
				if (_dtBarError.Rows.Count == 0)
				{
					if (_countBody > 0)
					{
						if (ParaHT == null)
						{
							ParaHT = new System.Collections.Hashtable();
						}
						string _prdField = (string)ParaHT["PrdField"];
						string _markDscName = (string)ParaHT["MarkDscName"];
						string _whNameField = (string)ParaHT["WhNameField"];
						string _unitField = (string)ParaHT["UnitField"];
						string _unitNameField = (string)ParaHT["UnitNameField"];
						string _upField = (string)ParaHT["UpField"];
						DateTime _bilDD = DateTime.Today;
						if (ParaHT["BilDD"] != null)
						{
							_bilDD = Convert.ToDateTime(ParaHT["BilDD"]);
						}
						string _cusNo = (string)ParaHT["CusNo"];
						Prdt _prdt = new Prdt();
						string _errorPrdt = _prdt.AddBilPrdName(_dtBody,_prdField,_markDscName,BodyWhName,_whNameField,false,_unitField,_unitNameField,_upField,_bilDD,_cusNo);
						if (!String.IsNullOrEmpty(_errorPrdt))
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["REM"] = _errorPrdt;
							_dtBarError.Rows.Add(_drBarError);
						}
						if (_dtBarError.Rows.Count == 0)
						{
							//��������ITM
							_draBody = _dtBody.Select();
							_countBody = _draBody.Length;
							if (_countBody > 0 && _draBody[_countBody - 1]["ITM"].ToString() != (_countBody - 1).ToString())
							{
								for (int i=0;i<_countBody;i++)
								{
									_draBody[i]["ITM"] = i + 1;
								}
							}
						}
					}
				}
				if (_dtBarError.Rows.Count == 0)
				{
					BarCodeDT.AcceptChanges();
				}
				ChangedDS.Tables.Remove("BAR_REC");
				if (ChangedDS.Tables["BAR_BOX"] != null)
				{
					ChangedDS.Tables.Remove("BAR_BOX");
				}
			}
		}
        /// <summary>
        /// ���������к�����(�÷������������ֶ����ƹ̶�ΪQTY)
        /// </summary>
        /// <param name="ChangedDS">���ݵ�DataSet</param>
        /// <param name="BarCodeDT">���к�ԭʼ����������кŴ���ڴ˱��У�</param>
        /// <param name="BodyTableName">���������</param>
        /// <param name="BodyBarItemName">���������кű�����Ӧ��Key��λ����</param>
        /// <param name="BodyBoxItemName">�����������ݱ�����Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
        /// <param name="BodyWhName">����Ŀ���λ����</param>
        /// <param name="BarTableName">���кű������</param>
        /// <param name="BarItemName">���кű����������Ӧ��Key��λ����</param>
        /// <param name="BoxTableName">�����ݱ�����ƣ��������ֵ��</param>        
        /// <param name="BoxItemName">�����ݱ����������Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
        /// <param name="IsControl">�Ƿ��ϸ�ع����к�</param>
        /// <param name="IsStop">���к��Ƿ���ͣ�û򲻴���</param>
        /// <param name="IsExist">���к��Ƿ�������</param>
        /// <param name="IsPhFlag">������־</param>
        /// <param name="ParaHT">ȡ�û�Ʒ��������Ҫ���Ĳ�������ȡ��������null
        /// PrdField����Ʒ������λ������ȡ��Ʒ���ƣ��˲�����հף�
        /// MarkDscName�������ֶ�������λ�����磺�ֶ���λΪSize���ֶ�������λΪSize_DSC������"_DSC"���ɣ���ȡ�����ֶ����ƣ��˲�����հף�
        /// WhNameField����������λ������ȡ��λ���ƣ��˲�����հף�
        /// UnitField����λ��λ����������λ���˲�����հף�
        /// UnitNameField����λ������λ������ȡ��λ���ƣ��˲�����հף�
        /// UpField����Ʒ������λ������ȡ��Ʒ�������ߣ��˲�����հף�
        /// BilDD���������ڣ�Ҫȡ��Ʒ�������ߣ����뵥�����ڣ����򴫵��죩
        /// CusNo���ͻ����ţ�Ҫȡ��Ʒ�������ߣ�����ͻ����ţ����򴫿գ�
        /// </param>
        public void BreakBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName, string BodyBoxItemName, string BodyWhName, string BarTableName, string BarItemName, string BoxTableName, string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag, System.Collections.Hashtable ParaHT)
        {
            BreakBilData(ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, BodyWhName, "QTY", BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag, ParaHT);
        }
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
        /// <param name="alBoxNo">�����뼯��</param>
		/// <param name="usr">�û�����</param>
        /// <param name="bilId">�������</param>
		/// <param name="bilNo">��Դ����</param>
        public void DeleteBox(ArrayList alBoxNo, string usr, string bilId, string bilNo)
        {
            string[] _aryBoxNo = new string[alBoxNo.Count];
            for (int i = 0; i < alBoxNo.Count; i++)
            {
                _aryBoxNo[i] = alBoxNo[i].ToString();
            }
            this.DeleteBox(_aryBoxNo, usr, bilId, bilNo);
        }
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="AryBoxNo">�����뼯��</param>
		/// <param name="usr">�û�����</param>
		/// <param name="bilID">�������</param>
		/// <param name="bilNo">��Դ����</param>
		public void DeleteBox(string[] AryBoxNo,string usr,string bilID,string bilNo)
		{
			System.Text.StringBuilder _sbWhere = new System.Text.StringBuilder();
			for (int i=0;i<AryBoxNo.Length;i++)
			{
				if (AryBoxNo[i] != null && !String.IsNullOrEmpty(AryBoxNo[i]) && _sbWhere.ToString().IndexOf(AryBoxNo[i]) < 0)
				{
					if (!String.IsNullOrEmpty(_sbWhere.ToString()))
					{
						_sbWhere.Append(",");
					}
					_sbWhere.Append(AryBoxNo[i]);
				}
			}
			if (!String.IsNullOrEmpty(_sbWhere.ToString()))
			{
				//���������ֶ�
				System.Collections.ArrayList _alBar = new System.Collections.ArrayList();
				int _maxWhereLength = 20480;
				string _subWhere;
				int _pos;
				string _where = "BOX_NO in ('" + _sbWhere.ToString().Replace(",","','") + "')";
				while (true)
				{
					if (_where.Length > _maxWhereLength)
					{
						_subWhere = _where.Substring(0,_maxWhereLength-1);
						_pos = _subWhere.LastIndexOf(",");
						_alBar.Add(_subWhere.Substring(0,_pos) + ")");
						_where = "BOX_NO in (" + _where.Substring(_pos+1,_where.Length-_pos-1);
					}
					else
					{
						_alBar.Add(_where);
						break;
					}
				}
				//�����Ͽ��ȡ���к�����
				DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
				SunlikeDataSet _dsBar = new SunlikeDataSet();
				DataTable _dtBox;
				for (int i=0;i<_alBar.Count;i++)
				{
					_dtBox = _bar.GetBoxData(_alBar[i].ToString());
					if (_dsBar.Tables["BAR_BOX"] == null)
					{
						_dsBar.Tables.Add(_dtBox.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBox,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBox = _dsBar.Tables["BAR_BOX"];
				string[] _aryBox = _sbWhere.ToString().Split(',');
				if (_dtBox.Rows.Count == _aryBox.Length)
				{
					//����װ������
					Hashtable _ht = new Hashtable();
					for (int i=0;i<_dtBox.Rows.Count;i++)
					{
						string _wh = _dtBox.Rows[i]["WH"].ToString();
						string _boxNo = _dtBox.Rows[i]["BOX_NO"].ToString();
						if (_ht[_wh] == null)
						{
							_ht[_wh] = _boxNo;
						}
						else
						{
							_ht[_wh] += "," + _boxNo;
						}
					}
					IDictionaryEnumerator _ide = _ht.GetEnumerator();
					SunlikeDataSet _dsMfBox = null;
					while (_ide.MoveNext())
					{
						if (_dsMfBox == null)
						{
							_dsMfBox = _bar.GetMfBox(_ide.Key.ToString(),_ide.Value.ToString().Split(','));
						}
						else
						{
							SunlikeDataSet _dsTemp = _bar.GetMfBox(_ide.Key.ToString(),_ide.Value.ToString().Split(','));
							_dsMfBox.Merge(_dsTemp,false,MissingSchemaAction.AddWithKey);
						}
					}
					for (int i=0;i<_dtBox.Rows.Count;i++)
					{
						DataRow[] _dra = _dsMfBox.Tables["MF_BOX"].Select("WH='"
							+ _dtBox.Rows[i]["WH"].ToString() + "' and BOX_NO='"
							+ _dtBox.Rows[i]["BOX_NO"].ToString() + "'");
						if (_dra.Length > 0)
						{
							_dra[0]["QTY"] = Convert.ToInt32(_dra[0]["QTY"]) - 1;
							_dra[0]["UB_DD"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
						}
						else
						{
							DataRow _dr = _dsMfBox.Tables["MF_BOX"].NewRow();
							_dr["WH"] = _dtBox.Rows[i]["WH"].ToString();
							_dr["BOX_NO"] = _dtBox.Rows[i]["BOX_NO"].ToString();
							_dr["QTY"] = -1;
							_dr["UB_DD"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
							_dsMfBox.Tables["MF_BOX"].Rows.Add(_dr);
						}
						//�޸�����
						WH _wh = new WH();
						_wh.UpdateBoxQty(_dtBox.Rows[i]["PRD_NO"].ToString(),_dtBox.Rows[i]["WH"].ToString(),_dtBox.Rows[i]["CONTENT"].ToString(),WH.BoxQtyTypes.QTY,-1);
					}
					this.UpdateDataSet(_dsMfBox);
					if (_dsMfBox.HasErrors)
					{
						throw new SunlikeException("�޷�����װ�����⣡");
					}
				}
				else
				{
					System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
					for (int i=0;i<_aryBox.Length;i++)
					{
						DataRow[] _draBox = _dtBox.Select("BOX_NO='" + _aryBox[i] + "'");
						if (_draBox.Length == 0)
						{
							_sbError.Append("������[" + _aryBox[i] + "]�����ڣ�\n");
						}
					}
					throw new Sunlike.Common.Utility.SunlikeException(_sbError.ToString());
				}
				//����
				_where = "BOX_NO in ('" + _sbWhere.ToString().Replace(",","','") + "')";
				_bar.DeleteBox(_where,usr,bilID,bilNo);
			}
		}

		/// <summary>
		/// ���䣨װ����ҵ��
		/// </summary>
		/// <param name="Wh">�ֿ����</param>
		/// <param name="BoxNo">������</param>
		/// <param name="Dep">���Ŵ���</param>
		/// <param name="Usr">װ����Ա</param>
		public void DeleteBox(string Wh,string BoxNo,string Dep,string Usr)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			_bar.DeleteBox(Wh,BoxNo,Dep,Usr);
		}
		#endregion

		#region ������Ŀ�λ
		/// <summary>
		/// ������Ŀ�λ
		/// </summary>
		/// <param name="BoxNoList">�����뼯��</param>
		/// <param name="WhList">��λ����</param>
		/// <param name="StopList">ͣ�ü���</param>
		public void UpdateBarBox(System.Collections.ArrayList BoxNoList,System.Collections.ArrayList WhList,System.Collections.ArrayList StopList)
		{
			StringBuilder _sql = new StringBuilder();
			for (int i=0;i<BoxNoList.Count;i++)
			{
				_sql.Append("update BAR_BOX set ");
				if (WhList[i] != null)
				{
					_sql.Append("WH='" + WhList[i] + "'");
				}
				if (StopList[i] != null)
				{
					if (WhList[i] != null)
					{
						_sql.Append(",");
					}
					_sql.Append("STOP_ID='" + StopList[i] + "'");
				}
				_sql.Append(" where BOX_NO='" + BoxNoList[i] + "'\n");
			}
			if (!String.IsNullOrEmpty(_sql.ToString()))
			{
				Query _query = new Query();
				_query.RunSql(_sql.ToString());
			}
		}
		#endregion

		#region �������кż�¼
		/// <summary>
		/// �������кż�¼
		/// </summary>
		/// <param name="ChangedDS"></param>
		/// <returns>������Ϣ�����û������û����</returns>
		public DataTable UpdateRec(DataSet ChangedDS)
		{
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
			_ht["BAR_REC"] = "BAR_NO,BOX_NO,WH,UPDDATE,STOP_ID,PRD_NO,PRD_MARK,BAT_NO,SPC_NO,PH_FLAG";
			this.UpdateDataSet(ChangedDS,_ht);
			DataTable _dt = GetAllErrors(ChangedDS);
			return _dt;
		}
		/// <summary>
		/// BeforeUpdate
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="updateStatus"></param>
		protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus updateStatus)
		{
			if (tableName == "BAR_REC")
			{
                if (statementType == StatementType.Insert)
                {
                    //д��������
                    BarPrint _barPrint = new BarPrint();
                    DataTable _dt = _barPrint.SelectBarPrint(dr["BAR_NO"].ToString());
                    if (_dt.Rows.Count > 0)
                    {
                        dr["SPC_NO"] = _dt.Rows[0]["SPC_NO"];
                    }
                }
                if (statementType != StatementType.Delete)
                {
                    //�������ݱ���ʱ��Ҫ����ȷ��
                    if (dr.Table.DataSet.ExtendedProperties.ContainsKey("IC_CFM_SW"))
                    {
                        dr["PH_FLAG"] = dr.Table.DataSet.ExtendedProperties["IC_CFM_SW"].ToString();
                    }
                }
			}
		}

		#endregion

		#region �������кŲ���
		
		/// <summary>
		/// �������кŲ���
		/// </summary>
		/// <param name="ChangedDS">���ݵ�DataSet</param>
		/// <param name="BarCodeDT">���к�ԭʼ����������кŴ���ڴ˱��У�</param>
		/// <param name="BodyTableName">���������</param>
		/// <param name="BodyBarItemName">���������кű�����Ӧ��Key����</param>
		/// <param name="BodyBoxItemName">�����������ݱ�����Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="BodyWh1Name">����Ŀ���λ����</param>
		/// <param name="BodyWh2Name">����Ŀ���λ����</param>
		/// <param name="BarTableName">���кű������</param>
		/// <param name="BarItemName">���кű����������Ӧ��Key����</param>
		/// <param name="BoxTableName">�����ݱ�����ƣ��������ֵ��</param>
		/// <param name="BoxItemName">�����ݱ����������Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="IsControl">�Ƿ��ϸ�ع����к�</param>
        /// <param name="IsStop">���к��Ƿ���ͣ�û򲻴���</param>
        /// <param name="IsExist">���к��Ƿ�������</param>
        /// <param name="IsPhFlag">������־</param>
		public void RepairBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName,
            string BodyBoxItemName, string BodyWh1Name, string BodyWh2Name, string BarTableName, string BarItemName,
            string BoxTableName, string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsPhFlag)
		{
			RepairBilData(ChangedDS,BarCodeDT,BodyTableName,BodyBarItemName,BodyBoxItemName,BodyWh1Name,BodyWh2Name,BarTableName,BarItemName,BoxTableName,BoxItemName,IsControl,IsStop,IsExist,false,IsPhFlag);
		}
		/// <summary>
		/// �������кŲ���
		/// </summary>
		/// <param name="ChangedDS">���ݵ�DataSet</param>
		/// <param name="BarCodeDT">���к�ԭʼ����������кŴ���ڴ˱��У�</param>
		/// <param name="BodyTableName">���������</param>
		/// <param name="BodyBarItemName">���������кű�����Ӧ��Key����</param>
		/// <param name="BodyBoxItemName">�����������ݱ�����Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="BodyWh1Name">����ĳ�������λ����</param>
		/// <param name="BodyWh2Name">������������λ����</param>
		/// <param name="BarTableName">���кű������</param>
		/// <param name="BarItemName">���кű����������Ӧ��Key����</param>
		/// <param name="BoxTableName">�����ݱ�����ƣ��������ֵ��</param>
		/// <param name="BoxItemName">�����ݱ����������Ӧ��Key��λ���ƣ��������ݱ������ֵ��</param>
		/// <param name="IsControl">�Ƿ��ϸ�ع����к�</param>
		/// <param name="IsStop">���к��Ƿ���ͣ�û򲻴���</param>
        /// <param name="IsExist">���к��Ƿ�������</param>
        /// <param name="IsSmartSplit">�Ƿ����ܲ��</param>
        /// <param name="IsPhFlag">������־</param>
		public void RepairBilData(DataSet ChangedDS, DataTable BarCodeDT, string BodyTableName, string BodyBarItemName,
            string BodyBoxItemName, string BodyWh1Name, string BodyWh2Name, string BarTableName, string BarItemName,
            string BoxTableName, string BoxItemName, bool IsControl, bool IsStop, bool IsExist, bool IsSmartSplit, bool IsPhFlag)
		{
            this.GetCollectData(true, ChangedDS, BarCodeDT, BodyTableName, BodyBarItemName, BodyBoxItemName, BarTableName, BarItemName, BoxTableName, BoxItemName, IsControl, IsStop, IsExist, IsPhFlag);
			DataTable _dtBarError = ChangedDS.Tables["BAR_ERROR"];
			DataTable _dtBarDel = ChangedDS.Tables["BAR_DEL"];
			DataRow _drBarError,_drBarDel;
			string[] _headerKey = new string[ChangedDS.Tables[0].PrimaryKey.Length];
			for (int i=0;i<ChangedDS.Tables[0].PrimaryKey.Length;i++)
			{
				_headerKey[i] = ChangedDS.Tables[0].PrimaryKey[i].ColumnName;
			}
			DataTable _dtBody = ChangedDS.Tables[BodyTableName];
			DataTable _dtBarCode = ChangedDS.Tables[BarTableName];
			DataTable _dtBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBox = ChangedDS.Tables[BoxTableName];
			}
			DataTable _dtBarRec = ChangedDS.Tables["BAR_REC"];
			DataTable _dtBarCollect = ChangedDS.Tables["BAR_COLLECT"];
			DataTable _dtBarBox = null;
			if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
			{
				_dtBarBox = ChangedDS.Tables["BAR_BOX"];
			}
			if (_dtBarError.Rows.Count == 0)
			{
				DataRow[] _draBox;
				DataRow _drBarCode;
				int _keyItm = 0;
				int _boxItm = 0;
				string _barCode,_boxNo;
                StringBuilder _where = new StringBuilder();
				DataRow[] _draBarCode = _dtBarCode.Select();
				DataRow[] _draBody = _dtBody.Select();
				//������������кŷŵ�Hashtable��
				System.Collections.Hashtable _htBarCode = new System.Collections.Hashtable();
				//����Ҫ�Զ��޳������кŷŵ�Hashtable��
				System.Collections.Hashtable _htAutoDel = new System.Collections.Hashtable();
				_draBarCode = _dtBarCode.Select();
				for (int i=0;i<_draBarCode.Length;i++)
				{
					_htBarCode[_draBarCode[i]["BAR_CODE"].ToString()] = "";
				}
				//��������ŵ�_boxList��
				System.Collections.Hashtable _htBox = new System.Collections.Hashtable();
				DataRow[] _draBarCollect = BarCodeDT.Select();
				for (int i=0;i<_draBarCollect.Length;i++)
				{
					_boxNo = _draBarCollect[i]["BOX_NO"].ToString();
					if (!String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] == null)
					{
						_htBox[_boxNo] = "";
					}
				}
				if (_dtBarError.Rows.Count == 0)
				{
					System.Collections.Hashtable _htBoxNo = new System.Collections.Hashtable();
					for (int i=0;i<_dtBarCode.Rows.Count;i++)
					{
						if (_dtBarCode.Rows[i].RowState != DataRowState.Deleted)
						{
							if (_htBoxNo[_dtBarCode.Rows[i]["BOX_NO"].ToString()] == null)
							{
								_htBoxNo[_dtBarCode.Rows[i]["BOX_NO"].ToString()] = i;
							}
						}
					}
					
					for (int i=0;i<_dtBarRec.Rows.Count;i++)
					{
						_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
						string _batNo = _dtBarRec.Rows[i]["BAT_NO"].ToString();
						bool _canAdd = true;
						//��BAR_CODE���в��Ҵ����к��Ƿ��Ѿ�����
						if (_htBarCode[_barCode] == null)
						{
							//������š�����
							string _prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
							_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
							string _prdMark = "";
							if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
							{
								_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
							}
							_boxNo = _dtBarRec.Rows[i]["BOX_NO"].ToString();
							DataRow[] _aryDrCode = _dtBarCollect.Select("BAR_CODE='"+_barCode+"'");
							if (_aryDrCode.Length == 0 && !String.IsNullOrEmpty(_boxNo))
							{
								_aryDrCode = _dtBarCollect.Select("BOX_NO='"+_boxNo+"'");
							}
							//WebForm�������ռ���ʱ��¼��BAR_CODE
							if (_aryDrCode.Length == 0 && !String.IsNullOrEmpty(_boxNo))
							{
								_aryDrCode = _dtBarCollect.Select("BAR_CODE='"+_boxNo+"'");
							}
							//��¼ԭ���к�����,�Ա�ǰ̨�жϿ�λ������
							_aryDrCode[0]["ISEXIST"] = _dtBarRec.Rows[i]["ISEXIST"];
                            _aryDrCode[0]["WH_REC"] = _dtBarRec.Rows[i]["WH"];
                            _aryDrCode[0]["BAT_NO_REC"] = _dtBarRec.Rows[i]["BAT_NO"];
                            _aryDrCode[0]["PH_FLAG"] = _dtBarRec.Rows[i]["PH_FLAG"];
							//�Ƿ�Ҫ����������
							if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName)
								&& !String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] != null
								&& Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "T")
							{
								if (_htBoxNo[_boxNo] == null || string.IsNullOrEmpty(_htBoxNo[_boxNo].ToString()))
								{
									_drBarCode = null;
								}
								else
								{
									_drBarCode = _dtBarCode.Rows[Convert.ToInt32(_htBoxNo[_boxNo])];
								}
								//���кű����Ƿ��д�BOX_NO
								if (_drBarCode != null)
								{
									_draBody = _dtBody.Select(BodyBarItemName + "=" + _drBarCode[BarItemName].ToString());
									if (String.IsNullOrEmpty(_draBody[0][BodyBoxItemName].ToString()))
									{
										_draBody = _dtBody.Select("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
											+ "' and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
									}
									else
									{
										_boxItm = Convert.ToInt32(_draBody[0][BodyBoxItemName]);
										_draBody = _dtBody.Select("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
											+ "' and " + BodyBoxItemName + "=" + _boxItm.ToString());
									}
									if (_draBody.Length == 0)
									{
										_drBarError = _dtBarError.NewRow();
										_drBarError["BAR_NO"] = _barCode;
										_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"�Ҳ������кŶ�Ӧ�ı����¼��";
										_drBarError["UNUSE"] = "T";
										_dtBarError.Rows.Add(_drBarError);
										_canAdd = false;
									}
									else
									{
										_canAdd = false;
										for (int j=0;j<_draBody.Length;j++)
										{
											//��BAR_CODE���в��Ҵ˻�Ʒ������������
											_keyItm = Convert.ToInt32(_draBody[j][BodyBarItemName]);
											_draBarCode = _dtBarCode.Select(BarItemName + "=" + _keyItm.ToString());
											if (_draBarCode.Length < Math.Abs(Convert.ToDecimal(_draBody[j]["QTY"])))
											{
												if (!String.IsNullOrEmpty(_batNo) && _draBody[j]["BAT_NO"].ToString() != _batNo)
												{
													_drBarError = _dtBarError.NewRow();
													_drBarError["BAR_NO"] = _barCode;
													_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"�Ҳ������кŶ�Ӧ�ı����¼��";
													_drBarError["UNUSE"] = "T";
													_dtBarError.Rows.Add(_drBarError);
												}
												_canAdd = true;
												break;
											}
										}
										if (!_canAdd)
										{
											_drBarError = _dtBarError.NewRow();
											_drBarError["BAR_NO"] = _barCode;
											_drBarError["REM"] = "RCID=INV.HINT.COMPLETED";//"���кŶ�Ӧ��Ʒ�����к��Ѿ���ȫ��";
											_dtBarError.Rows.Add(_drBarError);
										}
									}
								}
								else
								{
									string _content = _dtBarBox.Select("BOX_NO='" + _boxNo + "'")[0]["CONTENT"].ToString();
									_draBox = _dtBox.Select("PRD_NO='" + _prdNo + "' and CONTENT='" + _content + "'");
									//���������޴˻�Ʒ������ȵļ�¼
									if (_draBox.Length > 0)
									{
										_canAdd = false;
										for (int j=0;j<_draBox.Length;j++)
										{
											//���������䲹�����кŵ�����
											_boxItm = Convert.ToInt32(_draBox[j][BoxItemName]);
											_draBody = _dtBody.Select(BodyBoxItemName + "=" + _boxItm.ToString());
											StringBuilder _itmList = new StringBuilder();
											for (int k=0;k<_draBody.Length;k++)
											{
												if (!String.IsNullOrEmpty(_itmList.ToString()))
												{
													 _itmList.Append(",");
												}
												_itmList.Append(_draBody[k][BodyBarItemName].ToString());
											}
											_draBarCode = _dtBarCode.Select(BarItemName + " in (" + _itmList + ")");
											StringBuilder _boxList = new StringBuilder();
											int _boxCount = 0;
											for (int k=0;k<_draBarCode.Length;k++)
											{
												if (_boxList.ToString().IndexOf(_draBarCode[k]["BOX_NO"].ToString()) < 0)
												{
													_boxList.Append(_draBarCode[k]["BOX_NO"].ToString() + ",");
													_boxCount ++;
												}
											}
											//�������Ѳ��������С����������������ͼ������룬�����ٿ�������Ʒ
											if (_boxCount < Math.Abs(Convert.ToInt32(_draBox[j]["QTY"])))
											{
												_canAdd = true;
												_where = new StringBuilder("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
													+ "' and " + BodyBoxItemName + "=" + _boxItm.ToString());
												_keyItm = Convert.ToInt32(_dtBody.Select(_where.ToString())[0][BodyBarItemName]);
												if (!String.IsNullOrEmpty(_batNo))
												{
													_where.Append(" and BAT_NO = '" + _batNo + "'");
													if (_dtBody.Select(_where.ToString()).Length == 0)
													{
														_drBarError = _dtBarError.NewRow();
														_drBarError["BAR_NO"] = _barCode;
														_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"�Ҳ������кŶ�Ӧ�ı����¼��";
														_drBarError["UNUSE"] = "T";
														_dtBarError.Rows.Add(_drBarError);
													}
												}
												break;
											}
										}
										if (!_canAdd)
										{
											//�ڱ����в��Ҵ˻�Ʒ�Ƿ����
                                            _where.Append("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark);
                                            _where.Append("' and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
											if (!String.IsNullOrEmpty(_batNo))
											{
												_where.Append(" and BAT_NO = '" + _batNo + "'");
											}
											_where.Append(" and " + BodyWh1Name + "='" + _aryDrCode[0]["WH1"].ToString() + "'");
											if (!String.IsNullOrEmpty(BodyWh2Name))
											{
												_where.Append(" and " + BodyWh2Name + "='" + _aryDrCode[0]["WH2"].ToString() + "'");
											}
											_draBody = _dtBody.Select(_where.ToString());
											if (_draBody.Length == 0 && ((IsControl && IsStop && string.IsNullOrEmpty(_dtBarRec.Rows[i]["WH"].ToString())) || !IsControl))
											{
												_draBody = _dtBody.Select(_where.ToString());
											}
											for (int k=0;k<_draBody.Length;k++)
											{
												//��BAR_CODE���в��Ҵ˻�Ʒ������������
												_keyItm = Convert.ToInt32(_draBody[k][BodyBarItemName]);
												_draBarCode = _dtBarCode.Select(BarItemName + "=" + _keyItm.ToString());
												if (_draBarCode.Length < Math.Abs(Convert.ToDecimal(_draBody[k]["QTY"])))
												{
													_canAdd = true;
													break;
												}
											}
											if (_draBody.Length == 0)
											{
												_drBarError = _dtBarError.NewRow();
												_drBarError["BAR_NO"] = _barCode;
												_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"�Ҳ������кŶ�Ӧ�ı����¼��";
												_drBarError["UNUSE"] = "T";
												_dtBarError.Rows.Add(_drBarError);
											}
											else if (!_canAdd)
											{
												_drBarError = _dtBarError.NewRow();
												_drBarError["BAR_NO"] = _barCode;
												_drBarError["REM"] = "RCID=INV.HINT.COMPLETED";//"���кŶ�Ӧ��Ʒ�����к��Ѿ���ȫ��";
												_drBarError["UNUSE"] = "T";
												_dtBarError.Rows.Add(_drBarError);
											}
										}
									}
									else
									{
										//�ڱ����в��Ҵ˻�Ʒ�Ƿ����
										_where = new StringBuilder("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark
											+ "' and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
										if (IsExist)
										{
											_where.Append(_where.Append(_where.Append(_where.Append(" and ISNULL(BAT_NO, '') = '" + _batNo + "'"))));
										}
										else
										{
                                            _where.Append(" and ISNULL(BAT_NO, '') = '" + _aryDrCode[0]["BAT_NO"].ToString() + "'");
										}
										_where.Append(" and " + BodyWh1Name + "='" + _aryDrCode[0]["WH1"].ToString() + "'");
										if (!String.IsNullOrEmpty(BodyWh2Name))
										{
											_where.Append(" and " + BodyWh2Name + "='" + _aryDrCode[0]["WH2"].ToString() + "'");
										}
										_draBody = _dtBody.Select(_where.ToString());
										_canAdd = false;
										for (int j=0;j<_draBody.Length;j++)
										{
											//��BAR_CODE���в��Ҵ˻�Ʒ������������
											_keyItm = Convert.ToInt32(_draBody[j][BodyBarItemName]);
											_draBarCode = _dtBarCode.Select(BarItemName + "=" + _keyItm.ToString());
											if (_draBarCode.Length < Math.Abs(Convert.ToDecimal(_draBody[j]["QTY"])))
											{
												_canAdd = true;
												break;
											}
										}
										if (_draBody.Length == 0)
										{
											_drBarError = _dtBarError.NewRow();
											_drBarError["BAR_NO"] = _barCode;
											_drBarError["REM"] = "RCID=COMMON.HINT.NOBODY";//"�Ҳ������кŶ�Ӧ�ı����¼��";
											_drBarError["UNUSE"] = "T";
											_dtBarError.Rows.Add(_drBarError);
										}
										else if (!_canAdd)
										{
											_drBarError = _dtBarError.NewRow();
											_drBarError["BAR_NO"] = _barCode;
											_drBarError["REM"] = "RCID=INV.HINT.COMPLETED";//"���кŶ�Ӧ��Ʒ�����к��Ѿ���ȫ��";
											_drBarError["UNUSE"] = "T";
											_dtBarError.Rows.Add(_drBarError);
										}
									}
									if (_canAdd && (_htBoxNo[_boxNo] == null || string.IsNullOrEmpty(_htBoxNo[_boxNo].ToString())))
									{
										_htBoxNo[_boxNo] = _dtBarCode.Rows.Count;
									}
								}
							}
							else
							{
								//�ڱ����в��Ҵ˻�Ʒ�Ƿ����
                                _where = new StringBuilder("PRD_NO='" + _prdNo + "' and PRD_MARK='" + _prdMark + "'");
								if (IsExist)
								{
									_where.Append(" and ISNULL(BAT_NO, '') = '" + _batNo + "'");
								}
								else
								{
									_where.Append(" and ISNULL(BAT_NO, '') = '" + _aryDrCode[0]["BAT_NO"].ToString() + "'");
								}
								if (BoxTableName != null && !String.IsNullOrEmpty(BoxTableName))
								{
									_where.Append(" and (" + BodyBoxItemName + " is null or " + BodyBoxItemName + "<1)");
								}
								_where.Append(" and " + BodyWh1Name + "='" + _aryDrCode[0]["WH1"].ToString() + "'");
								if (!String.IsNullOrEmpty(BodyWh2Name))
								{
									_where.Append(" and " + BodyWh2Name + "='" + _aryDrCode[0]["WH2"].ToString() + "'");
								}
								_draBody = _dtBody.Select(_where.ToString());
								_canAdd = false;
								for (int j=0;j<_draBody.Length;j++)
								{
									//��BAR_CODE���в��Ҵ˻�Ʒ������������
									_keyItm = Convert.ToInt32(_draBody[j][BodyBarItemName]);
									_draBarCode = _dtBarCode.Select(BarItemName + "=" + _keyItm.ToString());
									if (_draBarCode.Length < Math.Abs(Convert.ToDecimal(_draBody[j]["QTY"])))
									{
										_canAdd = true;
										break;
									}
								}
								if (_draBody.Length == 0)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BAR_NO"] = _barCode;
									_drBarError["REM"] = "RCID=INV.HINT.NOBODY";//"�Ҳ������кŶ�Ӧ�ı����¼��";
									_drBarError["UNUSE"] = "T";
									if (String.IsNullOrEmpty(_boxNo) || (!String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] == null))
									{
										_htAutoDel[_barCode] = "";
									}
									_dtBarError.Rows.Add(_drBarError);
								}
								else if (!_canAdd)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BAR_NO"] = _barCode;
									_drBarError["REM"] = "RCID=INV.HINT.COMPLETED";//"���кŶ�Ӧ��Ʒ�����к��Ѿ���ȫ��";
									_drBarError["UNUSE"] = "T";
									if (String.IsNullOrEmpty(_boxNo) || (!String.IsNullOrEmpty(_boxNo) && _htBox[_boxNo] == null))
									{
										_htAutoDel[_barCode] = "";
									}
									_dtBarError.Rows.Add(_drBarError);
								}
								else if (!String.IsNullOrEmpty(_boxNo))
								{
									//�Զ����䴦��
									if (_htBox[_boxNo] == null)
									{
										if (Comp.DRP_Prop["AUTO_UNBOX"].ToString() == "T")
										{
											_boxNo = "";
										}
										else
										{
											_canAdd = false;
											_drBarError = _dtBarError.NewRow();
											_drBarError["BAR_NO"] = _barCode;
											_drBarError["REM"] = "RCID=INV.HINT.NOTBREAK";//"���к��Ѿ�װ�䣬�㲻�ܲ���¼�룡";
											_dtBarError.Rows.Add(_drBarError);
										}
									}
								}
							}
							//���뵽BAR_CODE����
							if (_canAdd)
							{
								_drBarCode = _dtBarCode.NewRow();
								for (int k=0;k<_headerKey.Length;k++)
								{
									_drBarCode[_headerKey[k]] = ChangedDS.Tables[0].Rows[0][_headerKey[k]];
								}
								_drBarCode["ITM"] = _dtBarCode.Select().Length + 1;
								_drBarCode["PRD_NO"] = _prdNo;
								_drBarCode["PRD_MARK"] = _prdMark;
								_drBarCode["BAR_CODE"] = _barCode;
								_drBarCode["BOX_NO"] = _boxNo;
								_drBarCode[BarItemName] = _keyItm;
								_dtBarCode.Rows.Add(_drBarCode);
								_htBarCode[_barCode] = "";
							}
						}
					}
				}
				//���ܲ��
				if (IsSmartSplit)
				{
					string _billIdFieldName = "";
					string _billNoFieldName = "";
					string _billItmFiledName = "";
					if (ChangedDS.ExtendedProperties["BILLID_FIELD"] != null)
					{
						_billIdFieldName = ChangedDS.ExtendedProperties["BILLID_FIELD"].ToString();
					}
                    if (ChangedDS.ExtendedProperties["BILLNO_FIELD"] != null)
					{
						_billNoFieldName = ChangedDS.ExtendedProperties["BILLNO_FIELD"].ToString();
					}
                    if (ChangedDS.ExtendedProperties["BILLITM_FIELD"] != null)
					{
						_billItmFiledName = ChangedDS.ExtendedProperties["BILLITM_FIELD"].ToString();
					}
					Prdt _prdt = new Prdt();
					foreach (DataRow dr in _dtBody.Select("isnull(BAT_NO, '') = ''"))
					{
						DataRow[] _dra = _dtBarCode.Select(BarItemName + "=" + dr[BodyBarItemName].ToString());
						int _codeCount = _dra.Length;
						if (_codeCount < Math.Abs(Convert.ToInt32(dr["QTY"])))
						{
							//���к�û�в�ȫ
							DataRow[] _draError = _dtBarError.Select("UNUSE = 'T' AND BAR_NO LIKE '%" + dr["PRD_NO"].ToString() + "%'");
							foreach (DataRow drError in _draError)
							{
								DataRow _drBarRec = _dtBarRec.Rows.Find(new string[1]{drError["BAR_NO"].ToString()});
								if (_drBarRec != null)
								{
									//������š�����
									int _keyItm1;
									string _barCode1 = _drBarRec["BAR_NO"].ToString();
									string _boxNo1 = _drBarRec["BOX_NO"].ToString();
									if (_htBox[_boxNo1] == null)
									{
										_boxNo1 = "";
									}
									string _prdNo = _barCode1.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1).Replace(BarRole.TrimChar,"");
									string _prdMark = "";
									if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
									{
										_prdMark = _barCode1.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
									}
									string _wh1 = "";
									string _wh2 = "";
									string _batNoNew = _drBarRec["BAT_NO"].ToString();
									DataRow[] _aryBarCollect = _dtBarCollect.Select("BAR_CODE='"+_barCode1+"'");
									if (_aryBarCollect.Length == 0)
									{
										_aryBarCollect = _dtBarCollect.Select("BOX_NO='"+_boxNo1+"'");
									}
									if (_aryBarCollect.Length > 0)
									{
										_wh1 = _aryBarCollect[0]["WH1"].ToString();
										if (!String.IsNullOrEmpty(BodyWh2Name))
										{
											_wh2 = _aryBarCollect[0]["WH2"].ToString();
										}
										if (!IsExist)
										{
											_batNoNew = _aryBarCollect[0]["BAT_NO"].ToString();
										}
									}
									if (dr["PRD_NO"].ToString() == _prdNo
										&& dr["PRD_MARK"].ToString() == _prdMark
										&& dr[BodyWh1Name].ToString() == _wh1)
									{
										if (String.IsNullOrEmpty(BodyWh2Name) || (!String.IsNullOrEmpty(BodyWh2Name) && dr[BodyWh2Name].ToString() == _wh2))
										{
											string _sqlWhere = "PRD_NO='" + dr["PRD_NO"].ToString()
												+ "' AND PRD_MARK='" + dr["PRD_MARK"].ToString()
												+ "' AND BAT_NO='" + _batNoNew
												+ "' AND " + BodyWh1Name + "='" + _wh1 + "'";
											if (!String.IsNullOrEmpty(BodyWh2Name))
											{
												_sqlWhere += " AND " + BodyWh2Name + "='" + _wh2 + "'";
											}
											if (!String.IsNullOrEmpty(_billIdFieldName))
											{
												_sqlWhere += " AND ISNULL(" + _billIdFieldName + ", '') = '" + dr[_billIdFieldName].ToString() + "'";
											}
											if (!String.IsNullOrEmpty(_billNoFieldName))
											{
												_sqlWhere += " AND ISNULL(" + _billNoFieldName + ", '') = '" + dr[_billNoFieldName].ToString() + "'";
											}
											if (!String.IsNullOrEmpty(_billItmFiledName))
											{
												_sqlWhere += " AND ISNULL(" + _billItmFiledName + ", '') = '" + dr[_billItmFiledName].ToString() + "'";
											}
											DataRow[] _draBody1 = _dtBody.Select(_sqlWhere);
											if (_draBody1.Length == 0)
											{
												//��ֲ���������
												DataRow _drNew = _dtBody.NewRow();
												foreach (DataColumn dc in _dtBody.Columns)
												{
													if (!dc.AutoIncrement)
													{
														_drNew[dc.ColumnName] = dr[dc.ColumnName];
													}
												}
												_drNew["ITM"] = GetMaxItm(_dtBody, "ITM");
												_drNew["QTY"] = Convert.ToInt32(dr["QTY"]) < 0 ? -1 : 1;
												_drNew["BAT_NO"] = _batNoNew;
												SunlikeDataSet _dsBatRec = _prdt.GetBatRecData(_batNoNew, dr["PRD_NO"].ToString(),
													dr["PRD_MARK"].ToString(), _wh1);
												if (_dsBatRec.Tables[0].Rows.Count > 0)
												{
													_drNew["VALID_DD"] = _dsBatRec.Tables[0].Rows[0]["VALID_DD"];
												}
												_dtBody.Rows.Add(_drNew);
												_keyItm1 = Convert.ToInt32(_drNew[BodyBarItemName]);
												//ɾ��BarError
												drError.Delete();
											}
											else
											{
												//�޸����м�¼
												_draBody1[0]["QTY"] = Convert.ToDecimal(_draBody1[0]["QTY"])
													+ (Convert.ToDecimal(_draBody1[0]["QTY"]) < 0 ? -1 : 1);
												_keyItm1 = Convert.ToInt32(_draBody1[0][BodyBarItemName]);
												//ɾ��BarError
												drError.Delete();
											}
											//�ۼ�ԭ����
											dr["QTY"] = Convert.ToDecimal(dr["QTY"]) - (Convert.ToInt32(dr["QTY"]) < 0 ? -1 : 1);

											//�������кż�¼
											DataRow _drBarCode1 = _dtBarCode.NewRow();
											for (int k=0;k<_headerKey.Length;k++)
											{
												_drBarCode1[_headerKey[k]] = ChangedDS.Tables[0].Rows[0][_headerKey[k]];
											}
											_drBarCode1["ITM"] = this.GetMaxItm(_dtBarCode, "ITM");
											_drBarCode1["PRD_NO"] = _prdNo;
											_drBarCode1["PRD_MARK"] = _prdMark;
											_drBarCode1["BAR_CODE"] = _barCode1;
											_drBarCode1["BOX_NO"] = _boxNo1;
											_drBarCode1[BarItemName] = _keyItm1;
											_dtBarCode.Rows.Add(_drBarCode1);
											if (Convert.ToDecimal(dr["QTY"]) == 0)
											{
												dr.Delete();
												break;
											}
										}
									}
								}
							}
						}
					}
				}
				//����Ҫ�Զ��޳������кŷŵ�BAR_DEL
				for (int i=_dtBarError.Rows.Count-1;i>=0;i--)
				{
					if (_htAutoDel[_dtBarError.Rows[i]["BAR_NO"].ToString()] != null)
					{
						//ɾ�����к���Ϣ
						_draBarCollect = _dtBarCollect.Select("BAR_CODE='" + _dtBarError.Rows[i]["BAR_NO"].ToString() + "'");
						if (_draBarCollect.Length > 0)
						{
							_draBarCollect[0].Delete();
						}
						//���Ӿ�����Ϣ
						_drBarDel = _dtBarDel.NewRow();
						_drBarDel["BAR_NO"] = _dtBarError.Rows[i]["BAR_NO"].ToString();
						_drBarDel["REM"] = _dtBarError.Rows[i]["REM"];//"�Ҳ������кŶ�Ӧ�ı����¼��";
						_dtBarDel.Rows.Add(_drBarDel);
						//ɾ��������Ϣ
						_dtBarError.Rows[i].Delete();
					}
				}
				if (_dtBarError.Rows.Count == 0)
				{
					BarCodeDT.AcceptChanges();
				}
				ChangedDS.Tables.Remove("BAR_REC");
				if (ChangedDS.Tables["BAR_BOX"] != null)
				{
					ChangedDS.Tables.Remove("BAR_BOX");
				}
			}
		}
		private int GetMaxItm(DataTable dt, string field)
		{
			DataRow[] _aryDr = dt.Select("", field + " DESC");
			if (_aryDr.Length > 0)
			{
				return Convert.ToInt32(_aryDr[0][field]) + 1;
			}
			return 1;
		}
		#endregion

		#region ȡ�����к�ת���ʽ
		/// <summary>
		/// ȡ�����к�ת���ʽ
		/// </summary>
		/// <param name="DocNo">�ĵ���ʽ����</param>
		/// <returns></returns>
		public DataTable GetBarDoc(string DocNo)
		{
			string _where = " where ";
			if (String.IsNullOrEmpty(DocNo.Trim()))
			{
				_where += "DEF_ID='T'";
			}
			else
			{
				_where += "DOC_NO='" + DocNo + "'";
			}
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBarDoc(_where);
			return _dt;
		}
		#endregion
		
		#region ���к�׷�� by db
		/// <summary>
		/// ���к�׷��
		/// </summary>
		/// <param name="BarCodeTrail">���кź������</param>
		/// <returns></returns>
		public SunlikeDataSet GetBarCodeTrail(string BarCodeTrail)
		{
			//ȡ�������ֶ�����
			PrdMark _mark = new PrdMark();
			DataTable _dtMark = _mark.GetSplitData("");

			#region Create Table BAR_CODE
			DataTable _dtBarCode = new DataTable("BAR_CODE");

			DataColumn BAR_NO = new DataColumn("BAR_NO");//���к�
			BAR_NO.DataType = typeof(System.String);
			BAR_NO.MaxLength = 90;

			DataColumn UPDDATE = new DataColumn("UPDDATE");//����ʱ��
			UPDDATE.DataType = typeof(System.DateTime);			

			DataColumn BOX_NO = new DataColumn("BOX_NO");//������
			BOX_NO.DataType = typeof(System.String);
			BOX_NO.MaxLength = 90;

			DataColumn CONTENT = new DataColumn("CONTENT");//�����
			BOX_NO.DataType = typeof(System.String);
			BOX_NO.MaxLength = 255;

			DataColumn PRD_NO = new DataColumn("PRD_NO");//��Ʒ����
			PRD_NO.DataType = typeof(System.String);
			PRD_NO.MaxLength = 30;

			DataColumn PRD_NAME = new DataColumn("PRD_NAME");//��Ʒ����
			PRD_NAME.DataType = typeof(System.String);
			PRD_NAME.MaxLength = 40;

			DataColumn WH = new DataColumn("WH");//��λ����
			WH.DataType = typeof(System.String);
			WH.MaxLength = 12;

			DataColumn WH_NAME = new DataColumn("WH_NAME");//��λ����
			WH_NAME.DataType = typeof(System.String);
			WH_NAME.MaxLength = 40;

			DataColumn CUS_NO = new DataColumn("CUS_NO");//�ͻ�����
			CUS_NO.DataType = typeof(System.String);
			CUS_NO.MaxLength = 12;

			DataColumn CUS_NAME = new DataColumn("CUS_NAME");//�ͻ�����
			CUS_NAME.DataType = typeof(System.String);
			CUS_NAME.MaxLength = 50;

			DataColumn STOP_ID = new DataColumn("STOP_ID");//ͣ�÷�
			STOP_ID.DataType = typeof(System.String);
			STOP_ID.MaxLength = 2;

			DataColumn SPC_NAME = new DataColumn("SPC_NAME");//���
			SPC_NAME.DataType = typeof(System.String);
			SPC_NAME.MaxLength = 30;

			DataColumn PMARK = new DataColumn("PMARK");//PMARK
			PMARK.DataType = typeof(System.String);
			PMARK.MaxLength = 100;

			DataColumn PMARK_NAME = new DataColumn("PMARK_NAME");//PMARK_NAME
			PMARK_NAME.DataType = typeof(System.String);
			PMARK_NAME.MaxLength = 100;

			DataColumn USR = new DataColumn("USR");//PMARK_NAME
			USR.DataType = typeof(System.String);
			USR.MaxLength = 50;

            
			_dtBarCode.Columns.Add(BAR_NO);
			_dtBarCode.Columns.Add(UPDDATE);
			_dtBarCode.Columns.Add(BOX_NO);
			_dtBarCode.Columns.Add(CONTENT);
			_dtBarCode.Columns.Add(PRD_NO);
			_dtBarCode.Columns.Add(PRD_NAME);
			_dtBarCode.Columns.Add(WH);
			_dtBarCode.Columns.Add(WH_NAME);
			_dtBarCode.Columns.Add(CUS_NO);
			_dtBarCode.Columns.Add(CUS_NAME);
			_dtBarCode.Columns.Add(STOP_ID);
			_dtBarCode.Columns.Add(SPC_NAME);
			_dtBarCode.Columns.Add(PMARK);
			_dtBarCode.Columns.Add(PMARK_NAME);
			_dtBarCode.Columns.Add(USR);

			for (int i=0;i<_dtMark.Rows.Count;i++)
			{
				_dtBarCode.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString());
				_dtBarCode.Columns.Add(_dtMark.Rows[i]["FLDNAME"].ToString() + "_DSC");
			}
			#endregion

			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			StringBuilder _barBoxStr = new StringBuilder(" 1<>1 ");
            StringBuilder _barCodeStr = new StringBuilder(" ");
			string[] _barCodeAry = BarCodeTrail.Split(new char[]{';'});
			SunlikeDataSet _dsBox = new SunlikeDataSet();
			SunlikeDataSet _dsRec = new SunlikeDataSet();
			SunlikeDataSet _ds = new SunlikeDataSet();
			Prdt _prdt = new Prdt();

			if (!String.IsNullOrEmpty(BarCodeTrail))
			{

				#region ȫ�����к�ͳһ����Ϊ��Ʒ���к��ַ���
				for (int i = 0; i < _barCodeAry.Length; i++)
				{
					if ((_barCodeAry[i] != null) && (!String.IsNullOrEmpty(_barCodeAry[i])))
					{
						//�ж��ǲ���������
						if (_barCodeAry[i].Substring(BarCode.BarRole.BoxPos,1) == BarCode.BarRole.BoxFlag)
						{
							_barBoxStr.Append(" OR A.BOX_NO = '"+_barCodeAry[i]+"'");
						}
						else
						{
                            _barCodeStr.Append(_barCodeAry[i] + ";");
						}
					}
				}
				#endregion	
				

				#region ��ʼ����������в���붯��
				_dsBox = _bar.GetBoxDataFind(_barBoxStr.ToString(),Comp.CompNo);
				if ( _dsBox.Tables["BAR_BOX"].Rows.Count > 0 )
				{
					DataTable _dtBarBox = _dsBox.Tables[0];

					if (_dtBarBox.Rows.Count > 0)
					{
						//��ʼ���
						DataRow[] _drb = _dtBarBox.Select();
						for (int i=0;i<_drb.Length;i++)
						{
							DataRow _dr = _dtBarCode.NewRow();							
							_dr["BAR_NO"] = _drb[i]["BOX_NO"];
							_dr["BOX_NO"] = _drb[i]["BOX_NO"];
							_dr["CONTENT"] = _drb[i]["CONTENT"];
							_dr["WH"] = _drb[i]["WH"];
							_dr["UPDDATE"] = _drb[i]["BB_DD"];
							_dr["STOP_ID"] = _drb[i]["STOP_ID"];
							_dr["SPC_NAME"] = "";
                            //_dr["CUS_NO"] = _drb[i]["CUS_NO"];
                            //_dr["CUS_NAME"] = _drb[i]["CUS_NAME"];
							_dr["PRD_NO"] = _drb[i]["PRD_NO"];
							_dr["USR"] = _drb[i]["USR_NAME"];
							_dtBarCode.Rows.Add(_dr);
						}
						_dtBarCode.AcceptChanges();
						//ȡ�û������ơ�������						
						_prdt.AddBilPrdName(_dtBarCode,"PRD_NAME","_DSC","WH","WH_NAME",false,"","","",DateTime.Today,"");
					}
				}
				#endregion

				#region ��ʼ�Ի�Ʒ���кŽ��в���붯��
				_dsRec = _bar.GetBarCodeTrail(_barCodeStr.ToString(),Comp.CompNo);
				if ( _dsRec.Tables["BAR_REC"].Rows.Count > 0 )
				{
					DataTable _dtBarRec = _dsRec.Tables["BAR_REC"];

					if (_dtBarRec.Rows.Count > 0)
					{
						//��ʼ���
						DataRow[] _dra = _dtBarRec.Select();
						for (int i=0;i<_dra.Length;i++)
						{
							DataRow _dr = _dtBarCode.NewRow();
							string _barCode = _dra[i]["BAR_NO"].ToString();
							_dr["BAR_NO"] = _barCode;
							_dr["BOX_NO"] = _dra[i]["BOX_NO"];
							_dr["WH"] = _dra[i]["WH"];
							_dr["UPDDATE"] = _dra[i]["UPDDATE"];
                            //_dr["CUS_NO"] = _dra[i]["CUS_NO"];
							_dr["CUS_NAME"] = _dra[i]["CUS_NAME"];
							_dr["STOP_ID"] = _dra[i]["STOP_ID"];
							_dr["SPC_NAME"] = _dra[i]["SPC_NAME"];
							_dr["USR"] = _dra[i]["USR_NAME"];


							string _prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
							_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
							_dr["PRD_NO"] = _prdNo;
							string _prdMark = "";
							if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
							{
								_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
							}
							string[] _aryPrdMark = _mark.BreakPrdMark(_prdMark);
							for (int j=0;j<_dtMark.Rows.Count;j++)
							{
								_dr[_dtMark.Rows[j]["FLDNAME"].ToString()] = _aryPrdMark[j];
								if (j == 0)
									_dr["PMARK"] += _aryPrdMark[j].ToString();
								else
									_dr["PMARK"] += " " + _aryPrdMark[j].ToString();
							}
							_dtBarCode.Rows.Add(_dr);
						}
						_dtBarCode.AcceptChanges();
						//ȡ�û������ơ�������
						_prdt.AddBilPrdName(_dtBarCode,"PRD_NAME","_DSC","WH","WH_NAME",false,"","","",DateTime.Today,"");
						for (int i = 0; i < _dtBarCode.Rows.Count; i++)
						{
							for (int j=0;j<_dtMark.Rows.Count;j++)
							{							
								if ( j == 0)
								{
									_dtBarCode.Rows[i]["PMARK_NAME"] += _dtBarCode.Rows[i][_dtMark.Rows[j]["FLDNAME"].ToString() + "_DSC"].ToString();
								}
								else
								{
									_dtBarCode.Rows[i]["PMARK_NAME"] += " " + _dtBarCode.Rows[i][_dtMark.Rows[j]["FLDNAME"].ToString() + "_DSC"].ToString();
								}
							}
						}
						for (int i=0;i<_dra.Length;i++)
						{
							if (_dtBarCode.Select("BAR_NO='" + _dra[i]["BAR_NO"].ToString() + "'").Length == 0)
							{
								_dra[i].Delete();
							}
						}
					}
				}
				#endregion

			}	
			
			_ds.Tables.Add(_dtBarCode);		

			return _ds;
		}
		#endregion

		#region �õ����к�׷����ϸ��¼ by db
		/// <summary>
		/// �õ����к�׷����ϸ��¼
		/// </summary>
		/// <param name="Bar_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarCodeDetailList(string Bar_No)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.GetBarCodeDetailList(Bar_No);			
		}
		/// <summary>
		/// �õ����к�׷����ϸ��¼
		/// </summary>
		/// <param name="Box_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarBoxDetailList(string Box_No)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			SunlikeDataSet _ds = _bar.GetBarBoxDetailList(Box_No);
			return _ds;
		}
		#endregion

		#region �õ�Ҫ���ص����ص��ļ�(����װ��)
		/// <summary>
		/// �õ�Ҫ���ص����ص��ļ�(����װ��)
		/// </summary>
		/// <param name="Ht"></param>
		/// <returns></returns>
        public SunlikeDataSet GetBarBoxToLocal(Dictionary<String, Object> Ht)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);			
			SunlikeDataSet _ds = _bar.GetBarBoxToLocal(Ht);			
			return _ds;
		}
		#endregion
		
		#region װ���װ�
		/// <summary>
		/// װ���װ�
		/// </summary>
		/// <param name="boxNo"></param>
		/// <returns></returns>
		public DataTable GetPrintData(string boxNo)
		{
			DbBarCode _dbCode = new DbBarCode(Comp.Conn_DB);
			return _dbCode.GetPrintData(boxNo);
		}
		#endregion

		#region д�����кű����¼
		/// <summary>
		/// д�����кű����¼
		/// </summary>
		/// <param name="BarNo">���к�</param>
		/// <param name="Wh1">ԭʼ��λ</param>
		/// <param name="Wh2">��ǰ��λ</param>
		/// <param name="BilID">��Դ���ݱ�</param>
		/// <param name="BilNo">��Դ����</param>
        /// <param name="Usr">����Ա</param>
        /// <param name="BatNo1">ԭʼ����</param>
        /// <param name="BatNo2">��ǰ����</param>
        /// <param name="phFlag1"></param>
        /// <param name="phFlag2"></param>
        /// <param name="stopId1"></param>
        /// <param name="stopId2"></param>
		/// <param name="IsGetSQL">True������Insert SQL��䣬�������������ӡ�False��ֱ��д��</param>
		/// <returns></returns>
		public string InsertBarChange(string BarNo,string Wh1,string Wh2,string BilID,string BilNo,string Usr,string BatNo1,string BatNo2,string phFlag1, string phFlag2, string stopId1, string stopId2, bool IsGetSQL)
		{
            string _sql = "insert into BAR_CHANGE (BAR_NO,UPDDATE,WH1,WH2,BIL_NO,USR,BIL_ID,BAT_NO1,BAT_NO2,PH_FLAG1,PH_FLAG2,STOP_ID1,STOP_ID2) values ('"
                + BarNo + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Wh1 + "','"
                + Wh2 + "','" + BilNo + "','" + Usr + "','" + BilID + "','" + BatNo1 + "','" + BatNo2 + "','" + phFlag1 + "','" + phFlag2 + "','" + stopId1 + "','" + stopId2 + "')\n";
			if (IsGetSQL)
			{
				return _sql;
			}
			else
			{
				Query _query = new Query();
				_query.RunSql(_sql);
				return "";
			}
        }
        /// <summary>
        /// д�����кű����¼
        /// </summary>
        /// <param name="BarNo">���к�</param>
        /// <param name="Wh1">ԭʼ��λ</param>
        /// <param name="Wh2">��ǰ��λ</param>
        /// <param name="BilID">��Դ���ݱ�</param>
        /// <param name="BilNo">��Դ����</param>
        /// <param name="Usr">����Ա</param>
        /// <param name="BatNo1">ԭʼ����</param>
        /// <param name="BatNo2">��ǰ����</param>
        /// <param name="phFlag1"></param>
        /// <param name="phFlag2"></param>
        /// <param name="IsGetSQL">True������Insert SQL��䣬�������������ӡ�False��ֱ��д��</param>
        /// <returns></returns>
        public string InsertBarChange(string BarNo, string Wh1, string Wh2, string BilID, string BilNo, string Usr, string BatNo1, string BatNo2, string phFlag1, string phFlag2, bool IsGetSQL)
        {
            string _sql = "insert into BAR_CHANGE (BAR_NO,UPDDATE,WH1,WH2,BIL_NO,USR,BIL_ID,BAT_NO1,BAT_NO2,PH_FLAG1,PH_FLAG2) values ('"
                + BarNo + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Wh1 + "','"
                + Wh2 + "','" + BilNo + "','" + Usr + "','" + BilID + "','" + BatNo1 + "','" + BatNo2 + "','" + phFlag1 + "','" + phFlag2 + "')\n";
            if (IsGetSQL)
            {
                return _sql;
            }
            else
            {
                Query _query = new Query();
                _query.RunSql(_sql);
                return "";
            }
        }
		#endregion

		#region ȡ�����кű����¼
		/// <summary>
		/// ȡ�����кű����¼
		/// </summary>
		/// <param name="BilID">��Դ�������</param>
		/// <param name="BilNo">��Դ����</param>
		/// <returns></returns>
		public DataTable GetBarChange(string BilID,string BilNo)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBarChange(BilID,BilNo);
			return _dt;
		}
        /// <summary>
        /// ȡ�����кű����¼
        /// </summary>
        /// <param name="barNo">���к�</param>
        /// <param name="upDate">��������</param>
        /// <returns></returns>
        public DataTable GetBarChange(string barNo, DateTime upDate)
        {
            DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
            DataTable _dt = _bar.GetBarChange(barNo, upDate);
            return _dt;
        }
		#endregion

		#region ɾ�����кű����¼
		/// <summary>
		/// ɾ�����кű����¼
		/// </summary>
		/// <param name="BarNo">���к�</param>
		/// <param name="BilID">��Դ���ݱ�</param>
		/// <param name="BilNo">��Դ����</param>
		/// <param name="IsGetSQL">True������Delete SQL��䣬�������������ӡ�False��ֱ��ɾ��</param>
		/// <returns></returns>
		public string DeleteBarChange(string BarNo,string BilID,string BilNo,bool IsGetSQL)
		{
			string _sql = "delete from BAR_CHANGE where BAR_NO='" + BarNo + "' and BIL_ID='" + BilID + "' and BIL_NO='" + BilNo + "'\n";
			if (IsGetSQL)
			{
				return _sql;
			}
			else
			{
				Query _query = new Query();
				_query.RunSql(_sql);
				return "";
			}
		}
		#endregion

		#region �ж���BAR_PRINT�����޴����к�
		/// <summary>
		/// �ж���BAR_PRINT�����޴����к�
		/// </summary>
		/// <param name="BarCode">�����ַ���(��SQL����)</param>
		/// <returns>����һ��ArrayList,�洢�����ַ�������һ���洢�����ڵ������¼���ڶ����洢�����ڹ�������</returns>
		public ArrayList ChkBarPrint(string BarCode)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.ChkBarPrint(BarCode);			
		}
		#endregion

		#region �õ���Ʒ�Ĺ��(����װ����)
		/// <summary>
		/// �õ���Ʒ�Ĺ��(����װ����)
		/// </summary>
		/// <param name="Ht">�洢����Hashtable</param>
		/// <returns></returns>
        public SunlikeDataSet GetSpc(Dictionary<String, Object> Ht)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.GetSpc(Ht);	
		}
		#endregion

		#region ���������кŵó��估��������
		/// <summary>
		/// ���������кŵó��估��������  
		/// ����DataSet���б� BARCODE(PRD_NO,PRD_MARK,QTY), BOX(PRD_NO,CONENT,QTY), BARBOX(PRD_NO,PRD_MARK,QTY)
		/// </summary>
		/// <param name="barCollect">�ռ������к�</param>
		/// <param name="errorMessage">���صĴ�����Ϣ</param>
		/// <returns></returns>
		public SunlikeDataSet CollectBarCode(DataTable barCollect,out string errorMessage)
		{
			SunlikeDataSet _coalitionBarDS = new SunlikeDataSet();
			#region ��ṹ
			//���кű�
			DataTable _barTable = new DataTable("BARCODE");
			_barTable.Columns.Add("PRD_NO");
			_barTable.Columns.Add("PRD_MARK");
			_barTable.Columns.Add("QTY");
			DataColumn[] _dcaBar = new DataColumn[2];
			_dcaBar[0] = _barTable.Columns["PRD_NO"];
			_dcaBar[1] = _barTable.Columns["PRD_MARK"];
			_barTable.PrimaryKey = _dcaBar;
			//�������
			DataTable _boxTable = new DataTable("BOX");
			_boxTable.Columns.Add("PRD_NO");
			_boxTable.Columns.Add("CONTENT");
			_boxTable.Columns.Add("QTY");
			DataColumn[] _dcaBox = new DataColumn[2];
			_dcaBar[0] = _boxTable.Columns["PRD_NO"];
			_dcaBar[1] = _boxTable.Columns["CONTENT"];
			_boxTable.PrimaryKey = _dcaBox;

			#endregion

			//������������кŷֿ�
			System.Text.StringBuilder _sbBox = new System.Text.StringBuilder();
			System.Text.StringBuilder _sbBar = new System.Text.StringBuilder();
			string _barCode,_boxNo;

			try
			{
				for (int i = 0 ; i < barCollect.Rows.Count;i++)			
				{
					_barCode = barCollect.Rows[i]["BOX_NO"].ToString();
					if (String.IsNullOrEmpty(_barCode))
						_barCode = barCollect.Rows[i]["BAR_CODE"].ToString();
					if (_barCode.Substring(BarRole.BoxPos,1) == BarRole.BoxFlag)
					{
						if (_sbBox.ToString().IndexOf(_barCode) < 0)
						{
							if (_sbBox.Length > 0)
							{
								_sbBox.Append(",");
							}
							_sbBox.Append(_barCode);
						}
					}
					else
					{
						if (_sbBar.Length > 0)
						{
							_sbBar.Append(",");
						}
						_sbBar.Append(_barCode);
					}
				
				}
				//�������к�����
				string _where = "";
				string _whereBox = "";
				if (_sbBox.Length > 0)
				{
					_whereBox = "BOX_NO in ('" + _sbBox.ToString().Replace(",","','") + "')";
				}
				else
				{
					_whereBox = "1<>1";
				}
				if (_sbBar.Length > 0)
				{
					_where += "BAR_NO in ('" + _sbBar.ToString().Replace(",","','") + "')";
				}
				if (String.IsNullOrEmpty(_where.Trim()))
				{
					_where = "1<>1";
				}
				//�����Ͽ��ȡ���к�����
				DataTable _dtBarBox = this.GetBoxData(_whereBox);
				DataTable _dtBarRec = this.GetBarRecord(_where,false);
				DataTable _dtBarRec1 = this.GetBarRecord(_whereBox,false);
				//������к��Ƿ����
				System.Text.StringBuilder _sbError = new System.Text.StringBuilder();
				//����������Ƿ����
				if (_sbBox.Length > 0)
				{
					string[] _aryBoxList = _sbBox.ToString().Split(new char[] {','});
					for (int i = 0 ; i < _aryBoxList.Length;i++)
					{
						if (_dtBarRec1.Select("BOX_NO='" + _aryBoxList[i] + "'").Length == 0)
						{
							_sbError.Append("������" + _aryBoxList[i] + "�����ڣ�\n");
						}
					}
				}
				//������к��Ƿ����
				if (_sbBar.Length > 0)
				{
					string[] _aryBarList = _sbBar.ToString().Split(new char[] {','});
					for (int i=0;i<_aryBarList.Length;i++)
					{
						if (_dtBarRec.Select("BAR_NO='" + _aryBarList[i] + "'").Length == 0)
						{
							_sbError.Append("���к�" + _aryBarList[i] + "�����ڣ�\n");
						}
					}
				}
				if (_sbError.Length == 0 )
				{
					string _prdNo = "";
					string _prdMark  = "";
					//���к��ռ�
					for (int i = 0 ; i < _dtBarRec.Rows.Count; i++)
					{
						_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
						if (_sbBar.ToString().IndexOf(_barCode) > -1)
						{
							//������š�����
							_prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
							_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
							_prdMark = "";
							if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
							{
								_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
							}
							DataRow[] _aryHasBar = _barTable.Select("PRD_NO='"+_prdNo+"' AND PRD_MARK='"+_prdMark+"'");
							if (_aryHasBar.Length > 0 )
							{
								decimal _qtyBar = 0;
								if (!String.IsNullOrEmpty(_aryHasBar[0]["QTY"].ToString()))
								{
									_qtyBar = Convert.ToDecimal(_aryHasBar[0]["QTY"]);
								}
								_aryHasBar[0]["QTY"] = _qtyBar + 1 ;
							}
							else
							{
								DataRow _drBar = _barTable.NewRow();
								_drBar["PRD_NO"] = _prdNo;
								_drBar["PRD_MARK"] = _prdMark;
								_drBar["QTY"] = 1;	
								_barTable.Rows.Add(_drBar);
							}
						}
					}
					//�������ռ�
					for (int i= 0 ; i < _dtBarRec1.Rows.Count;i++)
					{
						_boxNo = _dtBarRec1.Rows[i]["BOX_NO"].ToString();
						DataRow[] _aryBoxSelect = _dtBarBox.Select(" BOX_NO='"+_boxNo+"'");
						if (_aryBoxSelect.Length > 0 )
						{
							_prdNo =_aryBoxSelect[0]["PRD_NO"].ToString();
						}
						if (_sbBox.ToString().IndexOf(_boxNo) > -1)
						{
							string _content = _dtBarBox.Select("BOX_NO='" + _boxNo + "'")[0]["CONTENT"].ToString();						
							DataRow[] _aryHasBox = _boxTable.Select("PRD_NO='"+_prdNo+"' AND CONTENT='"+_content+"'");
							if (_aryHasBox.Length > 0 )
							{
								decimal _qtyBox = 0;
								if (!String.IsNullOrEmpty(_aryHasBox[0]["QTY"].ToString()))
								{
									_qtyBox = Convert.ToDecimal(_aryHasBox[0]["QTY"]);
								}
								_aryHasBox[0]["QTY"] = _qtyBox + 1 ;
							}
							else
							{
								DataRow _drBox = _boxTable.NewRow();
								_drBox["PRD_NO"] = _prdNo;
								_drBox["CONTENT"] = _content;
								_drBox["QTY"] = 1;
								_boxTable.Rows.Add(_drBox);
							}
						}
					}

				}
				errorMessage = _sbError.ToString();
			}
			catch (Exception _ex)
			{
				errorMessage = _ex.Message.ToString();
			}
			
			_coalitionBarDS.Tables.Add(_boxTable);
			_coalitionBarDS.Tables.Add(_barTable);
			return _coalitionBarDS;
		}
		#endregion

		#region ���������кŵó��估�����������ֲֿ⣩
		/// <summary>
		/// ���������кŵó��估�����������ֲֿ⣩
		/// </summary>
		/// <param name="controlBarCode">���к��Ƿ����</param>
		/// <param name="barCollect">�ռ������к�</param>	
		/// <param name="hasBarNo">��������</param>		
		/// <returns></returns>
		public SunlikeDataSet CollectBarCodeWH(bool controlBarCode,DataTable barCollect,bool hasBarNo)
		{
			SunlikeDataSet _coalitionBarDS = new SunlikeDataSet();
			#region ��ṹ
			//���кű�
			DataTable _barTable = new DataTable("BARCODE");
			if (hasBarNo)
			{
				_barTable.Columns.Add("BAT_NO");
			}
			_barTable.Columns.Add("PRD_NO");
			_barTable.Columns.Add("PRD_MARK");
			_barTable.Columns.Add("WH");
			_barTable.Columns.Add("QTY");

			DataColumn[] _dcaBar = null;
			if (hasBarNo)
			{
				_dcaBar = new DataColumn[4];
				_dcaBar[0] = _barTable.Columns["BAT_NO"];
				_dcaBar[1] = _barTable.Columns["PRD_NO"];
				_dcaBar[2] = _barTable.Columns["PRD_MARK"];
				_dcaBar[3] = _barTable.Columns["WH"];

			}
			else
			{
				_dcaBar = new DataColumn[3];				
				_dcaBar[0] = _barTable.Columns["PRD_NO"];
				_dcaBar[1] = _barTable.Columns["PRD_MARK"];
				_dcaBar[2] = _barTable.Columns["WH"];
			}
			_barTable.PrimaryKey = _dcaBar;
			//�������
			DataTable _boxTable = new DataTable("BOX");
			_boxTable.Columns.Add("PRD_NO");
			_boxTable.Columns.Add("WH");
			_boxTable.Columns.Add("CONTENT");
			_boxTable.Columns.Add("QTY");
			DataColumn[] _dcaBox = new DataColumn[3];
			_dcaBox[0] = _boxTable.Columns["PRD_NO"];
			_dcaBox[1] = _boxTable.Columns["WH"];
			_dcaBox[2] = _boxTable.Columns["CONTENT"];
			_boxTable.PrimaryKey = _dcaBox;
			#endregion
			bool controlBoxQty = false;
			if (Comp.DRP_Prop["CONTROL_BOX_QTY"].ToString() == "T")
				controlBoxQty = true;
			//������������кŷֿ�
			System.Text.StringBuilder _sbBox = new System.Text.StringBuilder();
			System.Text.StringBuilder _sbBar = new System.Text.StringBuilder();
			string _barCode;			
			try
			{
				#region �ֿ�����������к�
				for (int i = 0 ; i < barCollect.Rows.Count;i++)
				{
					_barCode = barCollect.Rows[i]["BOX_NO"].ToString();
					if (String.IsNullOrEmpty(_barCode))
						_barCode = barCollect.Rows[i]["BAR_CODE"].ToString();
					//������
					if (_barCode.Substring(BarRole.BoxPos,1) == BarRole.BoxFlag)
					{
						if (controlBoxQty)  //�����ع�
						{
							if (_sbBox.ToString().IndexOf(_barCode) < 0)
							{
								if (_sbBox.Length > 0)
								{
									_sbBox.Append(",");
								}
								_sbBox.Append(_barCode);
							}
						}
						else
						{							
							DataTable _barRecTable = GetBarRecord("BOX_NO='"+_barCode+"'",false);
							if (_barRecTable.Rows.Count > 0 )
							{
								for (int j = 0 ; j < _barRecTable.Rows.Count;j++)
								{
									if (_sbBar.Length > 0)
									{
										_sbBar.Append(",");
									}
									_sbBar.Append(_barRecTable.Rows[j]["BAR_NO"].ToString());
								}
							}

						}
					}
					else//���к�
					{
						if (_sbBar.Length > 0)
						{
							_sbBar.Append(",");
						}
						_sbBar.Append(_barCode);
					}
				}
				#endregion
				//�������к�����
				string _where = "";
				string _whereBox = "";
				#region ��������
				if (_sbBox.Length > 0)
				{
					_whereBox = "BOX_NO in ('" + _sbBox.ToString().Replace(",","','") + "')";
				}
				else
				{
					_whereBox = "1<>1";
				}
				if (_sbBar.Length > 0)
				{
					_where += "BAR_NO in ('" + _sbBar.ToString().Replace(",","','") + "')";
				}
				if (String.IsNullOrEmpty(_where.Trim()))
				{
					_where = "1<>1";
				}
				#endregion

				#region ���������ֶ�
				//���������ֶ�
				ArrayList _alBox = new ArrayList();
				ArrayList _alBar = new ArrayList();
				int _maxWhereLength = 1024;
				string _subWhere;
				int _pos;
				while (true)
				{
					if (_whereBox.Length > _maxWhereLength)
					{
						_subWhere = _whereBox.Substring(0,_maxWhereLength-1);
						_pos = _subWhere.LastIndexOf(",");
						_alBox.Add(_subWhere.Substring(0,_pos) + ")");
						_whereBox = "BOX_NO in (" + _whereBox.Substring(_pos+1,_whereBox.Length-_pos-1);
					}
					else
					{
						_alBox.Add(_whereBox);
						break;
					}
				}
				_maxWhereLength = 10240;
				while (true)
				{
					if (_where.Length > _maxWhereLength)
					{
						_subWhere = _where.Substring(0,_maxWhereLength-1);
						_pos = _subWhere.LastIndexOf(",");
						_alBar.Add(_subWhere.Substring(0,_pos) + ")");
						_where = "BAR_NO in (" + _where.Substring(_pos+1,_where.Length-_pos-1);
					}
					else
					{
						_alBar.Add(_where);
						break;
					}
				}
				#endregion

				//�����Ͽ��ȡ���к�����
				//DataTable _dtBarBox = this.GetBoxData(_whereBox);
				//DataTable _dtBarRec = this.GetBarRecord(_where,false);
				//DataTable _dtBarRec1 = this.GetBarRecord(_whereBox,false);
				//��������
				#region ��������
				DataTable _dtBarError = null;//ChangedDS.Tables["BAR_ERROR"];
				DataRow _drBarError;	
				//DataTable _dtBarError = new DataTable();
				if (_dtBarError == null)
				{
					_dtBarError = new DataTable("BAR_ERROR");
					_dtBarError.Columns.Add("UNUSE");
					DataColumn _dc = new DataColumn("BAR_NO");
					_dtBarError.Columns.Add(_dc);
					_dc = new DataColumn("BOX_NO");
					_dtBarError.Columns.Add(_dc);
					_dc = new DataColumn("REM");
					_dtBarError.Columns.Add(_dc);
					_coalitionBarDS.Tables.Add(_dtBarError);
				}
				else
				{
					_dtBarError.Clear();
				}
				#endregion

				
				#region �����Ͽ��ȡ���к�
				//�����Ͽ��ȡ���к�
				SunlikeDataSet _dsBar = new SunlikeDataSet();
				DataTable _dtBarBox = null;
				for (int i=0;i<_alBox.Count;i++)
				{
					_dtBarBox = this.GetBoxData(_alBox[i].ToString());
					if (_dsBar.Tables["BAR_BOX"] == null)
					{
						_dsBar.Tables.Add(_dtBarBox.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBarBox,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBarBox = _dsBar.Tables["BAR_BOX"];
				DataTable _dtBarRec;
				for (int i=0;i<_alBar.Count;i++)
				{
					_dtBarRec = this.GetBarRecord(_alBar[i].ToString(),false);
					if (_dsBar.Tables["BAR_REC"] == null)
					{
						_dsBar.Tables.Add(_dtBarRec.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBarRec,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBarRec = _dsBar.Tables["BAR_REC"];
				DataTable _dtBarRec1 = null;
				for (int i=0;i<_alBox.Count;i++)
				{
					_dtBarRec1 = this.GetBarRecord(_alBox[i].ToString(),false);
					_dtBarRec1.TableName = "BAR_REC1";
					if (_dsBar.Tables["BAR_REC1"] == null)
					{
						_dsBar.Tables.Add(_dtBarRec1.Copy());
					}
					else
					{
						_dsBar.Merge(_dtBarRec1,true,MissingSchemaAction.AddWithKey);
					}
				}
				_dtBarRec1 = _dsBar.Tables["BAR_REC1"];
				#endregion

				//����������Ƿ����
				if (_sbBox.Length > 0)
				{
					string[] _aryBoxList = _sbBox.ToString().Split(new char[] {','});
					for (int i = 0 ; i < _aryBoxList.Length;i++)
					{
						if (_dtBarRec1.Select("BOX_NO='" + _aryBoxList[i] + "'").Length == 0)
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["BOX_NO"] = _aryBoxList[i];
							_drBarError["REM"] = "RCID=INV.HINT.BOXNOEXISTS";//�����벻���ڣ�
							_dtBarError.Rows.Add(_drBarError);								
						}
					}
				}
				//������к��Ƿ����
				if (_sbBar.Length > 0)
				{
					string[] _aryBarList = _sbBar.ToString().Split(new char[] {','});
					for (int i=0;i<_aryBarList.Length;i++)
					{
						if (_dtBarRec.Select("BAR_NO='" + _aryBarList[i] + "'").Length == 0)
						{
							_drBarError = _dtBarError.NewRow();
							_drBarError["BAR_NO"] = _aryBarList[i];
                            _drBarError["REM"] = "RCID=INV.HINT.BARCODENOEXIST";//���кŲ����ڻ���ͣ�ã�
							_dtBarError.Rows.Add(_drBarError);															
						}
					}
				}
				//
				if (_dtBarError.Rows.Count == 0)
				{
					string _prdNo = "";
					string _prdMark  = "";
					string _wh = "";
					string _batNo = "";
					DataRow[] _aryDrCollect = null;
					#region ����
					for (int i = 0 ; i < _dtBarRec.Rows.Count;i++)
					{
						_barCode = _dtBarRec.Rows[i]["BAR_NO"].ToString();
						//������š�����
						_prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
						_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
						_prdMark = "";
						if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
						{
							_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
						}
						_wh = _dtBarRec.Rows[i]["WH"].ToString();
						string _whNo = "";
						_aryDrCollect = barCollect.Select("BAR_CODE='" + _barCode + "'");
						if (_aryDrCollect != null && _aryDrCollect.Length > 0)
						{
							_whNo = _aryDrCollect[0]["WH1"].ToString();
						}
						//�Ƿ���ǿ�ƿ�λ
						if ( _whNo.Length > 0)
						{
							//�Ƿ����
							if (controlBarCode)
							{
								if (_wh != _whNo)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BAR_NO"] =_barCode;							
									_drBarError["REM"] = "RCID=INV.HINT.CONTROLBARCOE,PARAM="+_barCode+",PARAM="+_wh+",PARAM="+_whNo;//���к�[{0}]�Ŀ�λ��[{1}],������ѡ��λ[{2}]�����к��ѹ��Ʋ���������
									_dtBarError.Rows.Add(_drBarError);	
									continue;
								}
							}
							else
							{
								_wh = _whNo;
							}
						}
						if (hasBarNo)
						{
							_batNo = _dtBarRec.Rows[i]["BAT_NO"].ToString();
						}
						DataRow _drBarSel = null;
						if (hasBarNo)
						{
							_drBarSel = _barTable.Rows.Find(new object[4]{_batNo,_prdNo,_prdMark,_wh});
						}
						else
						{
							_drBarSel = _barTable.Rows.Find(new object[3] {_prdNo,_prdMark,_wh});
						}

						if (_drBarSel == null)
						{
							DataRow _drBarNew = _barTable.NewRow();
							if (hasBarNo)
							{
								_drBarNew["BAT_NO"] = _batNo;
							}
							_drBarNew["PRD_NO"] = _prdNo;
							_drBarNew["PRD_MARK"] = _prdMark;
							_drBarNew["WH"] = _wh;
							_drBarNew["QTY"] = 1;
							_barTable.Rows.Add(_drBarNew);
						}
						else
						{
							_drBarSel["QTY"] = Convert.ToDecimal(_drBarSel["QTY"])+1;
						}
					}
					#endregion

					#region ������
					for (int i = 0 ; i < _dtBarBox.Rows.Count;i++)
					{
						string _whNo = "";
						_aryDrCollect = barCollect.Select("BOX_NO='" + _dtBarBox.Rows[i]["BOX_NO"].ToString() + "'");
						if (_aryDrCollect != null && _aryDrCollect.Length > 0)
						{
							_whNo = _aryDrCollect[0]["WH1"].ToString();
						}
						//�Ƿ���ǿ�ƿ�λ
						if ( _whNo.Length > 0)
						{
							//�Ƿ����
							if (controlBarCode)
							{
								if (_dtBarBox.Rows[i]["WH"].ToString() != _whNo)
								{
									_drBarError = _dtBarError.NewRow();
									_drBarError["BOX_NO"] = _dtBarBox.Rows[i]["BOX_NO"].ToString();							
									_drBarError["REM"] = "RCID=INV.HINT.CONTROLBARCOE,PARAM="+_dtBarBox.Rows[i]["BOX_NO"].ToString()+",PARAM="+_dtBarBox.Rows[i]["WH"].ToString()+",PARAM="+_whNo;//���к�[{0}]�Ŀ�λ��[{1}],������ѡ��λ[{2}]�����к��ѹ��Ʋ���������
									_dtBarError.Rows.Add(_drBarError);	
									continue;
								}
							}
							else
							{
								_wh = _whNo;
							}
						}
						DataRow _drBoxSel = _boxTable.Rows.Find(new object[3] {_dtBarBox.Rows[i]["PRD_NO"],_dtBarBox.Rows[i]["WH"],_dtBarBox.Rows[i]["CONTENT"]});
						if (_drBoxSel == null)
						{
							DataRow _drBoxNew = _boxTable.NewRow();
							_drBoxNew["PRD_NO"] = _dtBarBox.Rows[i]["PRD_NO"];
							_drBoxNew["WH"] = _dtBarBox.Rows[i]["WH"];
							_drBoxNew["CONTENT"] = _dtBarBox.Rows[i]["CONTENT"];
							_drBoxNew["QTY"] = 1;
							_boxTable.Rows.Add(_drBoxNew);
						}
						else
						{
							_drBoxSel["QTY"] = Convert.ToDecimal(_drBoxSel["QTY"])+1;
						}
					}
					#endregion

					#region ����1
					if (!controlBoxQty)
					{
						
						for (int i = 0 ; i < _dtBarRec1.Rows.Count;i++)
						{
							_barCode = _dtBarRec1.Rows[i]["BAR_NO"].ToString();
							//������š�����
							_prdNo = _barCode.Substring(BarRole.SPrdt,BarRole.EPrdt - BarRole.SPrdt + 1);
							_prdNo = _prdNo.Replace(BarRole.TrimChar,"");
							_prdMark = "";
							if (!(BarRole.BPMark == BarRole.EPMark && BarRole.EPMark == 0))
							{
								_prdMark = _barCode.Substring(BarRole.BPMark,BarRole.EPMark - BarRole.BPMark + 1);
							}
							_wh = _dtBarRec1.Rows[i]["WH"].ToString();
							string _whNo = "";
							_aryDrCollect = barCollect.Select("BAR_CODE='" + _barCode + "'");
							if (_aryDrCollect != null && _aryDrCollect.Length > 0)
							{
								_whNo = _aryDrCollect[0]["WH1"].ToString();
							}
							//�Ƿ���ǿ�ƿ�λ
							if ( _whNo.Length > 0)
							{
								//�Ƿ����
								if (controlBarCode)
								{
									if (_wh != _whNo)
									{
										_drBarError = _dtBarError.NewRow();
										_drBarError["BAR_NO"] =_barCode;							
										_drBarError["REM"] = "RCID=INV.HINT.CONTROLBARCOE,PARAM="+_barCode+",PARAM="+_wh+",PARAM="+_whNo;//���к�[{0}]�Ŀ�λ��[{1}],������ѡ��λ[{2}]�����к��ѹ��Ʋ���������
										_dtBarError.Rows.Add(_drBarError);	
										continue;
									}
								}
								else
								{
									_wh = _whNo;
								}
							}
							DataRow _drBarBoxSel = _barTable.Rows.Find(new object[3] {_prdNo,_prdMark,_wh});
							if (_drBarBoxSel == null)
							{
								DataRow _drBarBoxNew = _barTable.NewRow();
								_drBarBoxNew["PRD_NO"] = _prdNo;
								_drBarBoxNew["PRD_MARK"] = _prdMark;
								_drBarBoxNew["WH"] = _wh;
								_drBarBoxNew["QTY"] = 1;
								_barTable.Rows.Add(_drBarBoxNew);
							}
							else
							{
								_drBarBoxSel["QTY"] = Convert.ToDecimal(_drBarBoxSel["QTY"])+1;
							}
						}						
					}
					#endregion

					if (_dtBarError.Rows.Count > 0)
					{
						_coalitionBarDS.Merge(_dtBarError);
					}
					else
					{
						_coalitionBarDS.Merge(_barTable);
						_coalitionBarDS.Merge(_boxTable);
						_coalitionBarDS.Merge(_barTable);
					}
				}
				else
				{
					_coalitionBarDS.Merge(_dtBarError);
				}

			}
			catch (Exception _ex)
			{
				throw _ex;
			}
			return _coalitionBarDS;
		}
		#endregion

		#region ��������õ�BAR_REC�е���ϸ���� by db
		/// <summary>
		/// ��������õ�BAR_REC�е���ϸ���� by db
		/// </summary>
		/// <param name="Box_No"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarRecByBoxNo(string Box_No)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			SunlikeDataSet _ds = _bar.GetBarRecByBoxNo(Box_No);
			return _ds;
		}
		#endregion

		#region ����Ʒ����Ƿ����
		/// <summary>
		/// ����Ʒ����Ƿ����
		/// </summary>
		/// <param name="SpcNo">���</param>
		/// <returns></returns>
		public bool IsSpcExist(string SpcNo)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.IsSpcExist(SpcNo);
		}
		#endregion

		#region �ж����к��Ƿ����
		/// <summary>
		/// �ж����к��Ƿ����
		/// </summary>
		/// <param name="BarCode">����</param>
		/// <returns></returns>
		public bool IsExistBarCode(string BarCode)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			return _bar.IsExistBarCode(BarCode);
		}
		#endregion

        #region �������к�PH_FLAG
        
        /// <summary>
        /// �������к�PH_FLAG
        /// </summary>
        /// <param name="barCodes">���кż���</param>
        /// <param name="cfmSw">true:T,false:F</param>
        public void UpdatePhFlag(string barCodes, bool cfmSw)
        {
            try
            {
                DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
                _dbBarCode.UpdatePhFlag(barCodes, cfmSw);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        #endregion
    }
}
/*
 * Bug tag 1:���кŹ��ƣ������˻�ʱ����һ��ͣ�õ����룬��λ����˻��Ǳ��������λ���Ϸ���
 * */