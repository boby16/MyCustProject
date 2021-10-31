using System;
using System.Data;
using System.Data.SqlClient;
using Sunlike.Common.Utility;
using Sunlike.Business.Data;
using Sunlike.Common.CommonVar;
using System.Text;

namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for Box.
	/// </summary>
	public class BarBox : BizObject
	{
		private bool _runAfterUpdate;
		/// <summary>
		/// װ��
		/// </summary>
		public BarBox()
		{
		}

		#region BOX_QTY
		/// <summary>
		/// ȡ��װ������
		/// </summary>
		/// <returns></returns>
		public DataTable GetBoxQty()
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBoxQty(0);
			return _dt;
		}

		/// <summary>
		/// ȡ��װ������
		/// </summary>
		/// <param name="Itm">���</param>
		/// <returns></returns>
		public DataTable GetBoxQty(int Itm)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBoxQty(Itm);
			return _dt;
		}

		/// <summary>
		/// ����װ�������趨���������ֵ
		/// </summary>
		/// <param name="Qty">����</param>
		public int InsertBoxQty(int Qty)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			int _itm = Convert.ToInt16(_bar.InsertBoxQty(Qty));
			return _itm;
		}

		/// <summary>
		/// ����װ�������趨
		/// </summary>
		/// <param name="Itm">���</param>
		/// <param name="Qty">����</param>
		public void UpdateBoxQty(int Itm,int Qty)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			_bar.UpdateBoxQty(Itm,Qty);
		}

		/// <summary>
		/// ɾ��װ������
		/// </summary>
		/// <param name="Itm">���</param>
		public void DeleteBoxQty(int Itm)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			_bar.DeleteBoxQty(Itm);
		}
		#endregion

		#region ȡװ�����к�
		/// <summary>
		/// ȡװ�����к�
		/// </summary>
		/// <param name="prd_No">��Ʒ����</param>
		/// <param name="content">װ�������</param>
		/// <param name="qty">������</param>
		/// <returns></returns>
		public string GetBar_Box_No(string prd_No , string content, out decimal qty)
		{
			DataTable _dt = null;
			decimal _qty = 0;
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			_dt = _bar.GetBar_Box_No(prd_No , content);
			string _box_No = "";
			foreach(DataRow _dr in _dt.Rows)
			{
				_box_No = _dr["BOX_NO"].ToString();
				_qty = Convert.ToDecimal(_dr["QTY"]);
				break;
			}
			qty = _qty;
			return _box_No;
		}
		/// <summary>
		/// ȡװ�����к�
		/// </summary>
		/// <param name="prd_No">��Ʒ����</param>
		/// <param name="content">װ�������</param>
		/// <returns></returns>
		public string GetBar_Box_No(string prd_No , string content)
		{
			decimal _qty;
			return GetBar_Box_No(prd_No, content, out _qty);
		}

		/// <summary>
		/// ȡ����������Ϣ
		/// </summary>
		/// <param name="pBoxNo">������</param>
		/// <param name="pPrdNo">�������</param>
		/// <param name="pPrdName">���Ʒ��</param>
		/// <param name="pSameArray">���ͬ��ͬ��PMARK</param>
		/// <param name="pMarkArray">����ֶ�PMARK</param>
		/// <param name="pCountArray">����ֶλ�Ʒ����</param>
		public void GetBoxInfo(string pBoxNo, ref string pPrdNo, ref string pPrdName, ref string[] pSameArray, ref string[] pMarkArray, ref string[] pCountArray)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			PrdMark _pmark = new PrdMark();
			DataTable _dtMark = _pmark.GetSplitData("");
			pSameArray = new string[_dtMark.Select("ISSAME = 'Y'").Length];
			DataTable _dt = _bar.GetBoxInfo(pBoxNo);
			if (_dt.Rows.Count > 0 && !String.IsNullOrEmpty(_dt.Rows[0]["PRD_NO"].ToString()))
			{
				pPrdNo = _dt.Rows[0]["PRD_NO"].ToString();
				pPrdName = _dt.Rows[0]["PRD_NAME"].ToString();
				string _content = _dt.Rows[0]["CONTENT"].ToString();
				_content = _content.Substring(0,_content.Length - 1);
				string[] _conArray = _content.Split(';');
				pMarkArray = new string[_conArray.Length];
				pCountArray = new string[_conArray.Length];
				string _conValue;
				string _conStr;
				string _cutStr;
				int _strPos;
				int _sameCount;
				for(int i=0;i<_conArray.Length;i++)
				{
					_conValue = _conArray[i];
					_conStr = "";
					_strPos = 0;
					_sameCount = 0;
					if (_conValue.IndexOf("=") >= 0)
					{
						_conStr = _conValue.Substring(0,_conValue.IndexOf("="));
						pCountArray[i] = _conValue.Replace(_conStr+"=","");
						foreach(DataRow _dr in _dtMark.Rows)
						{
							_cutStr = _conStr.Substring(_strPos,Convert.ToInt32(_dr["SIZE"]));
							if (_dr["ISSAME"].ToString() == "Y")
							{
								if (i == 0)
								{
									pSameArray[_sameCount++] += _pmark.GetPmarkDsc(_dt.Rows[0]["PRD_NO"].ToString(),_dr["FLDNAME"].ToString(),_cutStr) + "|" + _dr["DSC"].ToString();
								}
							}
							else
							{
								pMarkArray[i] += _pmark.GetPmarkDsc(_dt.Rows[0]["PRD_NO"].ToString(),_dr["FLDNAME"].ToString(),_cutStr);
							}
							_strPos += Convert.ToInt32(_dr["SIZE"]);
						}
					}
				}
			}
		}
		#endregion

		#region BOX_SET
		/// <summary>
		/// ȡ������ȹ���
		/// </summary>
		/// <param name="BxID">����ȴ���</param>
		/// <returns></returns>
		public DataTable GetBoxSet(string BxID)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBoxSet(BxID);
			return _dt;
		}

		/// <summary>
		/// �������ȹ���
		/// </summary>
		/// <param name="PrdNo">��Ʒ����</param>
		/// <param name="Qty">װ������</param>
		/// <param name="Content">���������</param>
		/// <param name="BxID">����ȹ������</param>
		/// <param name="Dep">���Ŵ���</param>
		/// <returns></returns>
		public bool CheckBoxSet(string PrdNo,int Qty,ref string Content,ref string BxID,string Dep)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			DataTable _dt = _bar.GetBoxSet(PrdNo,Qty,Dep);
			bool _isExist = false;
			//��ѯ�Ƿ�������ȹ���
			if (_dt.Rows.Count > 0)
			{
				PrdMark _mark = new PrdMark();
				DataTable _dtMark = _mark.GetSplitData("");
				for (int i=0;i<_dt.Rows.Count;i++)
				{
					_isExist = true;
					BxID = _dt.Rows[i]["BX_ID"].ToString();
					//�ȶ������
					string _rem = ";" + _dt.Rows[i]["REM"].ToString() + ";";
					string[] _aryContent = Content.Split(new char[] {';'});
					string[] _aryQty,_aryMark;
					for (int j=0;j<_aryContent.Length;j++)
					{
						_aryQty = _aryContent[j].Split(new char[] {'='});
						_aryMark = _mark.BreakPrdMark(_aryQty[0]);
                        StringBuilder _newMark = new StringBuilder();
						for (int k=0;k<_aryMark.Length;k++)
						{
							if (_dtMark.Rows[k]["TYPE"].ToString() != "1" && _dtMark.Rows[k]["ISSAME"].ToString() == "N")
							{
                                _newMark.Append(_aryMark[k]); ;
							}
						}
						if (_rem.IndexOf(";" + _newMark.ToString() + "=" + _aryQty[1] + ";") < 0)
						{
							_isExist = false;
							break;
						}
					}
					//����ȷ��Ϲ������д����
					if (_isExist)
					{
						string[] _aryFirstMark = _mark.BreakPrdMark(Content.Substring(0,Content.IndexOf("=")));
						_rem = _dt.Rows[i]["REM"].ToString();
						string[] _aryRem = _rem.Split(new char[] {';'});
						Content = "";
						for (int j=0;j<_aryRem.Length;j++)
						{
							_aryQty = _aryRem[j].Split(new char[] {'='});
							int _pos = 0;
							for (int k=0;k<_aryFirstMark.Length;k++)
							{
								//�����ͬ��ͬ�Σ���ȡ��һ�����кŵĴ�����ֵ
								if (_dtMark.Rows[k]["ISSAME"].ToString() != "N" || _dtMark.Rows[k]["TYPE"].ToString() == "1")
								{
									Content += _aryFirstMark[k];
								}
								else
								{
									int _size = Convert.ToInt16(_dtMark.Rows[k]["SIZE"]);
									Content += _aryQty[0].Substring(_pos,_size);
									_pos += _size;
								}
							}
							Content += "=" + _aryQty[1] + ";";
						}
						break;
					}
				}
			}
			return _isExist;
		}

		/// <summary>
		/// ��������ȹ���
		/// </summary>
		/// <param name="BxID">����ȴ���</param>
		/// <param name="Rem">�������������</param>
		/// <param name="Qty">װ������</param>
		/// <param name="Usr">������Ա</param>
		/// <param name="Idx1">�������1</param>
		/// <param name="Dep">���Ŵ���</param>
		public string InsertBoxSet(string BxID,string Rem,int Qty,string Usr,string Idx1,string Dep)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			string _error = _bar.InsertBoxSet(BxID,Rem,Qty,Usr,Idx1,Dep);
			if (_error == "1")
			{
				_error = "BOXSETEDIT.IS_EXIST";
			}
			else if (_error == "2")
			{
				_error = "CHKDESIGN.ERROR9";
			}
			else
			{
				_error = "";
			}
			return _error;
		}

		/// <summary>
		/// ��������ȹ���
		/// </summary>
		/// <param name="BxID">����ȴ���</param>
		/// <param name="Rem">�������������</param>
		/// <param name="Qty">װ������</param>
		/// <param name="Usr">������Ա</param>
		/// <param name="Idx1">�������1</param>
		/// <param name="Dep">���Ŵ���</param>
		/// <param name="IsStop">ͣ�÷�</param>
		public void UpdateBoxSet(string BxID,string Rem,int Qty,string Usr,string Idx1,string Dep,bool IsStop)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			string _stopID = IsStop.ToString().Substring(0,1).ToUpper();
			_bar.UpdateBoxSet(BxID,Rem,Qty,Usr,Idx1,Dep,_stopID);
		}

		/// <summary>
		/// ɾ������ȹ���
		/// </summary>
		/// <param name="BxID">����ȴ���</param>
		public void DeleteBoxSet(string BxID)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			_bar.DeleteBoxSet(BxID);
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
			DbBarCode _dbCode = new DbBarCode(Comp.Conn_DB);
			return _dbCode.GetStat(pWh, pCtn, pPrdNo, pSqlPow);
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
			DbBarCode _dbCode = new DbBarCode(Comp.Conn_DB);
			return _dbCode.GetBarCtn(pContent, pQty, pWh, pPrdNo);
		}
		
		/// <summary>
		/// ȡ��������
		/// </summary>
		/// <param name="sqlWhere"></param>
		/// <returns></returns>
		public int GetStatQty(string sqlWhere)
		{
			DbBarCode _barCode = new DbBarCode(Comp.Conn_DB);
			return _barCode.GetStatQty(sqlWhere);
		}
		#endregion

		#region �ж��������Ƿ��㹻
		/// <summary>
		/// �ж��������Ƿ��㹻(������������)
		/// </summary>
		/// <param name="prd_No">����</param>
		/// <param name="content">�������</param>
		/// <param name="wh">��λ</param>
		/// <param name="qty">����</param>
		/// <param name="wh_Qty">������</param>
		/// <returns></returns>
		public bool CheckQty(string prd_No , string content , string wh , decimal qty , out decimal wh_Qty)
		{
			bool _isOk = false;
			int _pmark_ColnNum = 0;
			decimal _wh_Box_Qty = 0;
			Sunlike.Business.WH _wh = new WH();
			DataTable _boxDt = this.GetBar_BoxDetail(this.GetBar_Box_No(prd_No , content) , false , out _pmark_ColnNum);
			#region ����tmpPrdt(��¼��Ʒ��Ҫ�����Ϳ����)
			DataTable _tmpPrdtDt = new DataTable("tmpPrdt");
			_tmpPrdtDt.Columns.Add("PRD_NO" , System.Type.GetType("System.String"));
			_tmpPrdtDt.Columns.Add("PRD_MARK" , System.Type.GetType("System.String"));
			_tmpPrdtDt.Columns.Add("PRDT_RATE" , System.Type.GetType("System.Decimal"));
			_tmpPrdtDt.Columns.Add("QTY" , System.Type.GetType("System.Decimal"));
			_tmpPrdtDt.Columns.Add("WH_QTY" , System.Type.GetType("System.Decimal"));
			#endregion
			#region �ѻ�Ʒд��tmpPrdt
			foreach(DataRow _boxDr in _boxDt.Rows)
			{
				decimal _wh_Qty = 0;
				string _prd_No = _boxDr["PRD_NO"].ToString();
				string _prd_Mark = _boxDr["PRD_MARK"].ToString();
				if(Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
				{
					_wh_Qty = Convert.ToDecimal(_wh.GetSumQty(false, _prd_No, _prd_Mark, wh, false, "", true));
				}
				else
				{
					_wh_Qty = Convert.ToDecimal(_wh.GetSumQty(true, _prd_No, _prd_Mark, wh, false, "", true));
				}
				DataRow _tmpPrdtDr = _tmpPrdtDt.NewRow();
				_tmpPrdtDr["PRD_NO"] = _prd_No;
				_tmpPrdtDr["PRD_MARK"] = _prd_Mark;
				_tmpPrdtDr["PRDT_RATE"] = Convert.ToDecimal(_boxDr["PRD_QTY"]);
				_tmpPrdtDr["QTY"] = Convert.ToDecimal(_boxDr["PRD_QTY"]) * qty;
				_tmpPrdtDr["WH_QTY"] = _wh_Qty;
				_tmpPrdtDt.Rows.Add(_tmpPrdtDr);
			}
			_tmpPrdtDt.AcceptChanges();
			#endregion
			#region ȡ��Ŀ����
			bool _isFirst = true;
			foreach(DataRow _tmpPrdtDr in _tmpPrdtDt.Rows)
			{
				int _box_Wh_Qty = Convert.ToInt32(Convert.ToInt32(_tmpPrdtDr["WH_QTY"]) / Convert.ToInt32(_tmpPrdtDr["PRDT_RATE"]));
				if(_isFirst)
				{
					_wh_Box_Qty = Convert.ToDecimal(_box_Wh_Qty);
				}
				else
				{
					if(_wh_Box_Qty > Convert.ToDecimal(_box_Wh_Qty))
					{
						_wh_Box_Qty = Convert.ToDecimal(_box_Wh_Qty);
					}
				}
				_isFirst = false;
			}
			#endregion
			wh_Qty = _wh_Box_Qty;
			if(_wh_Box_Qty >= qty)
				_isOk = true;
			return _isOk;
		}
		/// <summary>
		/// �ж��������Ƿ��㹻(������������)
		/// </summary>
		/// <param name="prd_No">����</param>
		/// <param name="content">�������</param>
		/// <param name="wh">��λ</param>
		/// <param name="qty">����</param>
		/// <returns></returns>
		public bool CheckQty(string prd_No , string content , string wh , decimal qty)
		{
			decimal _wh_Qty = 0;
			return CheckQty(prd_No , content , wh , qty , out _wh_Qty);
		}
		/// <summary>
		/// �ж��������Ƿ��㹻
		/// </summary>
		/// <param name="prd_No">����</param>
		/// <param name="content">�������</param>
		/// <param name="wh">��λ</param>
		/// <param name="qty">����</param>
		/// <param name="checkAll">�Ƿ񰴼�����</param>
		/// <returns></returns>
		public bool CheckQty(string prd_No, string content, string[] wh, int qty, bool checkAll)
		{
			bool _isOk = false;
			//------------�ж������Ƿ��㹻----------
			Sunlike.Business.WH _whCls = new WH();
			decimal _qtyBox = 0;
			for (int x = 0; x < wh.Length; x++)
			{
				if(Comp.DRP_Prop["DRPYH_CHK_QTY_WAY"].ToString() == "0")
					_qtyBox += _whCls.GetBoxQty(false,prd_No,wh[x],content);
				else
					_qtyBox += _whCls.GetBoxQty(true,prd_No,wh[x],content);
			}
			if (_qtyBox < qty)
			{
				_isOk = false;
			}
			else
			{
				_isOk = true;
			}
			if(checkAll && _isOk)
			{
				_qtyBox = 0;
				decimal _qtyBox1 = 0;
				for (int x = 0; x < wh.Length; x++)
				{
					CheckQty(prd_No, content, wh[x], Convert.ToDecimal(qty), out _qtyBox1);
					_qtyBox += Convert.ToInt32(_qtyBox1);
				}
				if (_qtyBox < qty)
				{
					_isOk = false;
				}
				else
				{
					_isOk = true;
				}
			}
			return _isOk;
			//--------------------------
		}
		#endregion

		#region ȡ����������
		/// <summary>
		/// ȡ����������
		/// </summary>
		/// <param name="box_No"></param>
		/// <returns></returns>
		public DataTable GetBar_Box(string box_No)
		{
			DataTable _dt = null;
			try
			{
				Sunlike.Business.Data.DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
				_dt = _dbBarCode.GetBar_Box(box_No);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return _dt;
		}
		
		/// <summary>
		/// ȡ�������
		/// </summary>
		/// <param name="prdNo"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public string GetBarBoxDsc(string prdNo, string content)
		{
            string _cacheKey = "CACHE_BARBOX_CONTENT_" + Comp.CompNo;
            SunlikeDataSet _ds = SunlikeDataSet.ConvertTo(CacheManager.Get(_cacheKey));
			if (_ds == null)
			{
				_ds = new SunlikeDataSet();
				DataTable _dtNew = new DataTable();
				_dtNew.Columns.Add(new DataColumn("PRD_NO", typeof(System.String)));
				_dtNew.Columns.Add(new DataColumn("CONTENT", typeof(System.String)));
				_dtNew.Columns.Add(new DataColumn("CONTENT_DSC", typeof(System.String)));
				DataColumn[] _aryDc = new DataColumn[2];
				_aryDc[0] = _dtNew.Columns["PRD_NO"];
				_aryDc[1] = _dtNew.Columns["CONTENT"];
				_dtNew.PrimaryKey = _aryDc;
				_ds.Tables.Add(_dtNew);
                CacheManager.Insert(_cacheKey, _ds);
			}
			DataTable _dt = _ds.Tables[0];
			DataRow _dr = _dt.Rows.Find(new string[2]{prdNo, content});
			if (_dr != null)
			{
				return _dr["CONTENT_DSC"].ToString();
			}
			else
			{
				DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
				DataRow _drNew = _dt.NewRow();
				_drNew["PRD_NO"] = prdNo;
				_drNew["CONTENT"] = content;
				_drNew["CONTENT_DSC"] = _dbBarCode.GetBarBoxDsc(prdNo, content);
				_dt.Rows.Add(_drNew);
				return _drNew["CONTENT_DSC"].ToString();
			}
		}
		#endregion

		#region ȡ������ȵ���������Ϣ
		/// <summary>
		/// ȡ������ȵ���������Ϣ
		/// </summary>
		/// <param name="prd_No"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public string GetBar_BoxDsc(string prd_No, string content)
		{
			string _contentDsc = "";
			DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
			_contentDsc = _dbBarCode.GetBarBoxDsc(prd_No, content);
			return _contentDsc;
		}
		#endregion

		#region ȡ���в�Ʒ���������Ƽ�����
		/// <summary>
		/// ȡ���в�Ʒ���������Ƽ�����
		/// </summary>
		/// <param name="bar_Box"></param>
		/// <returns></returns>
		public DataTable GetBar_BoxDetail(DataTable bar_Box)
		{
			int _pmark_ColnNum = 0;
			return GetBar_BoxDetail(bar_Box , true , out _pmark_ColnNum);
		}
		/// <summary>
		/// ȡ���в�Ʒ����Ϣ
		/// </summary>
		/// <param name="bar_Box">������</param>
		/// <param name="showAll">�Ƿ�ȫ��ʾ</param>
		/// <returns></returns>
		public DataTable GetBar_BoxDetail(DataTable bar_Box , bool showAll)
		{
			int _pmark_ColnNum = 0;
			return GetBar_BoxDetail(bar_Box , showAll , out _pmark_ColnNum);
		}
		/// <summary>
		/// ȡ���в�Ʒ���������Ƽ�����
		/// </summary>
		/// <param name="bar_Box">������</param>
		/// <param name="showAll">�Ƿ�ȫ��ʾ</param>
		/// <param name="pmark_ColnNum">�������ڵ�����</param>
		/// <returns></returns>
		public DataTable GetBar_BoxDetail(DataTable bar_Box , bool showAll , out int pmark_ColnNum)
		{
			Sunlike.Business.PrdMark _prdMark = new PrdMark();
			Sunlike.Business.Prdt _prdt = new Prdt();
			DataTable _dt = bar_Box;
			DataTable _markDt = _prdMark.GetSplitData("");
			if(!_dt.Columns.Contains("BOX_NO"))
			{
				_dt.Columns.Add("BOX_NO" , System.Type.GetType("System.String"));
			}
			foreach(DataRow _dr in _dt.Rows)
			{
				decimal _qty = 0;
				_dr["BOX_NO"] = this.GetBar_Box_No(_dr["PRD_NO"].ToString(), _dr["CONTENT"].ToString(), out _qty);
				_dr["QTY"] = _qty;
			}
			_dt.AcceptChanges();
			//--------------------------------------------������----------------------------------------
			DataTable _boxDt = new DataTable("BAR_BOX");
			_boxDt.Columns.Add("BOX_NO" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("CONTENT" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("PRD_NO" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("PRD_NAME" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("PRD_MARK" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("BOX_QTY" , System.Type.GetType("System.Decimal"));
			_boxDt.Columns.Add("PRD_QTY" , System.Type.GetType("System.Decimal"));
			foreach(DataRow _dr in _markDt.Rows)
			{
				_boxDt.Columns.Add(_dr["FLDNAME"].ToString() , System.Type.GetType("System.String"));
				_boxDt.Columns.Add(_dr["FLDNAME"].ToString() + "_DSC" , System.Type.GetType("System.String"));
			}
			_boxDt.Columns.Add("WH" , System.Type.GetType("System.String"));
			//------------------------------------------------------------------------------------------

			//--------------------------------------------ȡ���ڵĲ�Ʒ��Ϣ------------------------------
			foreach(DataRow _dr in _dt.Rows)
			{
				string _content = _dr["CONTENT"].ToString();
				if (_content.Length > 0 )
				{
					if (_content.Substring(_content.Length-1,1) == ";")
					{
						_content = _content.Substring(0,_content.Length-1);
					}
				}
				string[] _aryContent = _content.Split(new char[]{';'});
				decimal _boxQTY = 0;
				foreach(string _prdContent in _aryContent)
				{
					bool _isAdd = false;
					string[] _aryPmark = _prdContent.Split(new char[]{'='});
					string _pmarkStr = _aryPmark[0].ToString();
					decimal _prdQTY = Convert.ToDecimal(_aryPmark[1].ToString());
					_boxQTY += _prdQTY;
					if(showAll)
					{
						_isAdd = true;
					}
					else
					{
						if(_prdQTY > 0)
							_isAdd = true;
						else
							_isAdd = false;
					}
					if(_isAdd)
					{
						DataRow _boxDr = _boxDt.NewRow();

						_boxDr["BOX_NO"] = _dr["BOX_NO"].ToString();
						_boxDr["CONTENT"] = _dr["CONTENT"].ToString();
						_boxDr["PRD_NO"] = _dr["PRD_NO"].ToString();
						_boxDr["PRD_MARK"] = _pmarkStr;
						_boxDr["BOX_QTY"] = _dr["QTY"].ToString();
						_boxDr["PRD_QTY"] = Convert.ToDecimal(_prdQTY);
						_boxDr["WH"] = _dr["WH"].ToString();
						_boxDt.Rows.Add(_boxDr);
						string[] _aryPrdMark = _prdMark.BreakPrdMark(_pmarkStr);
						for(int i=0;i<_aryPrdMark.Length;i++)
						{
							_boxDr[(7+i*2)] = _aryPrdMark[i].ToString();
						}
					}
				}
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			_ds.Tables.Add(_boxDt);
			if(_ds.Tables["BAR_BOX"].Rows.Count > 0)
				_prdt.AddBilPrdName(_ds.Tables["BAR_BOX"],"PRD_NAME","_DSC","","",false,"","","",DateTime.Today,"");
			//------------------------------------------------------------------------------------------
			pmark_ColnNum = 7;
			return _boxDt;
		}
		/// <summary>
		/// ȡ���в�Ʒ���������Ƽ�����
		/// </summary>
		/// <param name="box_No"></param>
		/// <returns></returns>
		public DataTable GetBar_BoxDetail(string box_No)
		{
			int _pmark_ColnNum = 0;
			return GetBar_BoxDetail(box_No , true , out _pmark_ColnNum);
		}
		/// <summary>
		/// ȡ���в�Ʒ���������Ƽ�����
		/// </summary>
		/// <param name="box_No">������</param>
		/// <param name="showAll">�Ƿ�ȫ��ʾ</param>
		/// <returns></returns>
		public DataTable GetBar_BoxDetail(string box_No , bool showAll)
		{
			int _pmark_ColnNum = 0;
			return GetBar_BoxDetail(box_No , showAll , out _pmark_ColnNum);
		}
		/// <summary>
		/// ȡ���в�Ʒ���������Ƽ�����
		/// </summary>
		/// <param name="box_No">������</param>
		/// <param name="showAll">�Ƿ�ȫ��ʾ</param>
		/// <param name="pmark_ColnNum">������ʼ������</param>
		/// <returns></returns>
		public DataTable GetBar_BoxDetail(string box_No , bool showAll , out int pmark_ColnNum)
		{
			Sunlike.Business.PrdMark _prdMark = new PrdMark();
			Sunlike.Business.Prdt _prdt = new Prdt();
			DataTable _dt = this.GetBar_Box(box_No);
			DataTable _markDt = _prdMark.GetSplitData("");

			//--------------------------------------------������----------------------------------------
			DataTable _boxDt = new DataTable("BAR_BOX");
			_boxDt.Columns.Add("BOX_NO" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("CONTENT" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("PRD_NO" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("PRD_NAME" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("UT" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("PRD_MARK" , System.Type.GetType("System.String"));
			_boxDt.Columns.Add("BOX_QTY" , System.Type.GetType("System.Decimal"));
			_boxDt.Columns.Add("PRD_QTY" , System.Type.GetType("System.Decimal"));
			foreach(DataRow _dr in _markDt.Rows)
			{
				_boxDt.Columns.Add(_dr["FLDNAME"].ToString() , System.Type.GetType("System.String"));
				_boxDt.Columns.Add(_dr["FLDNAME"].ToString() + "_DSC" , System.Type.GetType("System.String"));
			}
			//------------------------------------------------------------------------------------------

			//--------------------------------------------ȡ���ڵĲ�Ʒ��Ϣ------------------------------
			foreach(DataRow _dr in _dt.Rows)
			{
				string _content = _dr["CONTENT"].ToString();
				if (_content.Length > 0 )
				{
					if (_content.Substring(_content.Length-1,1) == ";")
					{
						_content = _content.Substring(0,_content.Length-1);
					}
				}

				string[] _aryContent = _content.Split(new char[]{';'});
				foreach(string _prdContent in _aryContent)
				{
					bool _isAdd = false;
					string[] _aryPmark = _prdContent.Split(new char[]{'='});
					string _pmarkStr = _aryPmark[0].ToString();
					decimal _prdQTY = Convert.ToDecimal(_aryPmark[1].ToString());
					if(showAll)
					{
						_isAdd = true;
					}
					else
					{
						if(_prdQTY > 0)
							_isAdd = true;
						else
							_isAdd = false;
					}
					if(_isAdd)
					{
						DataRow _boxDr = _boxDt.NewRow();
						_boxDr["BOX_NO"] = _dr["BOX_NO"].ToString();
						_boxDr["CONTENT"] = _dr["CONTENT"].ToString();
						_boxDr["PRD_NO"] = _dr["PRD_NO"].ToString();
						_boxDr["UT"] = _dr["UT"].ToString();
						_boxDr["PRD_MARK"] = _pmarkStr;
						_boxDr["BOX_QTY"] = _dr["QTY"];
						_boxDr["PRD_QTY"] = Convert.ToDecimal(_prdQTY);
						_boxDt.Rows.Add(_boxDr);
						string[] _aryPrdMark = _prdMark.BreakPrdMark(_pmarkStr);
						for(int i=0;i<_aryPrdMark.Length;i++)
						{
							_boxDr[(8+i*2)] = _aryPrdMark[i].ToString();
						}
					}
				}
			}
			SunlikeDataSet _ds = new SunlikeDataSet();
			_ds.Tables.Add(_boxDt);
			if(_ds.Tables["BAR_BOX"].Rows.Count > 0)
				_prdt.AddBilPrdName(_ds.Tables["BAR_BOX"],"PRD_NAME","_DSC","","",false,"","","",DateTime.Today,"");
			//------------------------------------------------------------------------------------------
			pmark_ColnNum = 8;
			return _boxDt;
		}
		#endregion

		#region ȡ�����в�Ʒ��Ϣ(PRDT1_BOX)
		/// <summary>
		/// ȡ�����в�Ʒ��Ϣ
		/// </summary>
		/// <param name="dtBox"></param>
		/// <returns></returns>
		public DataTable GetBoxDetail(DataTable dtBox)
		{
			PrdMark _pmark = new PrdMark();
			DataTable _dtMark = _pmark.GetSplitData("");
			DataTable _dtPrdt = new DataTable();
			_dtPrdt.Columns.Add(new DataColumn("WH", typeof(System.String)));
			_dtPrdt.Columns.Add(new DataColumn("PRD_NO", typeof(System.String)));
			_dtPrdt.Columns.Add(new DataColumn("PRD_NAME", typeof(System.String)));
			_dtPrdt.Columns.Add(new DataColumn("CONTENT", typeof(System.String)));
			_dtPrdt.Columns.Add(new DataColumn("SPC", typeof(System.String)));
			foreach (DataRow dr in _dtMark.Rows)
			{
				_dtPrdt.Columns.Add(new DataColumn(dr["FLDNAME"].ToString(), typeof(System.String)));
				_dtPrdt.Columns.Add(new DataColumn(dr["FLDNAME"].ToString() + "_DSC", typeof(System.String)));
			}
			_dtPrdt.Columns.Add(new DataColumn("QTY", typeof(System.Decimal)));

			//
			Prdt _prdt = new Prdt();
			foreach (DataRow dr in dtBox.Rows)
			{
				string _spc = "";
				DataTable _dtPrdt1 = _prdt.GetPrdt(dr["PRD_NO"].ToString());
				if (_dtPrdt1.Rows.Count > 0)
				{
					if (_dtPrdt1.Rows[0]["SPC"] != DBNull.Value)
					{
						_spc = _dtPrdt1.Rows[0]["SPC"].ToString();
					}
				}
				//����Contentȡ�����еĲ�Ʒ
				DataTable _dtTmp = this.GetMarkByContent(dr["CONTENT"].ToString());
				foreach (DataRow drTmp in _dtTmp.Rows)
				{
					string[] _aryPrdMark = _pmark.BreakPrdMark(drTmp["PRD_MARK"].ToString());
					DataRow _drNew = _dtPrdt.NewRow();
					_drNew["WH"] = dr["WH"];
					_drNew["PRD_NO"] = dr["PRD_NO"];
					_drNew["CONTENT"] = dr["CONTENT"];
					_drNew["SPC"] = _spc;
					//�������
					for (int i=0;i<_dtMark.Rows.Count;i++)
					{
						_drNew[_dtMark.Rows[i]["FLDNAME"].ToString()] = _aryPrdMark[i];
					}
					_drNew["QTY"] = drTmp["QTY"];
					_dtPrdt.Rows.Add(_drNew);
				}
			}
			if (_dtPrdt.Rows.Count > 0)
			{
				_prdt.AddBilPrdName(_dtPrdt, "PRD_NAME", "_DSC", "", "", false, "", "", "",DateTime.Today, "");
			}
			_dtPrdt.AcceptChanges();
			return _dtPrdt;
		}
		/// <summary>
		/// ����Contentȡ�ò�Ʒ����
		/// </summary>
		public DataTable GetMarkByContent(string content)
		{
			DataTable _resDt = new DataTable();
			_resDt.Columns.Add(new DataColumn("PRD_MARK", typeof(System.String)));
			_resDt.Columns.Add(new DataColumn("QTY", typeof(System.Decimal)));
			//ȥ��ĩβ�ķֺ�
			content = content.Substring(0, content.Length - 1);
			//ȡ��A1=2
			string[] _aryCnt = content.Split(';');
			foreach (string strCnt in _aryCnt)
			{
				string[] _aryPrdt = strCnt.Split('=');
				DataRow _dr = _resDt.NewRow();
				_dr["PRD_MARK"] = _aryPrdt[0];
				_dr["QTY"] = Convert.ToInt32(_aryPrdt[1]);
				_resDt.Rows.Add(_dr);
			}
			//����
			_resDt.AcceptChanges();
			return _resDt;
		}
		#endregion

		//�����ܶ�����д��BeforeUpdate��AfterUpdate,�����ܶ�����޸ĵĲ��־ͷŵ��˴�

		#region �޸��ܶ���ϸ(��˫ For Auditing)
		/// <summary>
		/// �޸��ܶ���ϸ(��˫ For Auditing)
		/// </summary>
        /// <param name="USR">�޸���</param>
		/// <param name="Os_No">Ҫ������</param>
		/// <param name="itm">�����ƷITM���</param>
		/// <param name="wh">��λ����</param>
		/// <param name="qty">����</param>
		/// <param name="est_DD">Ԥ������</param>
		/// <returns></returns>
		public string UpdateSoPro(string Usr,string Os_No , string itm , string wh , decimal qty , DateTime est_DD)
		{
			DrpSO _drpSo = new DrpSO();
			StringBuilder _error = new StringBuilder();
			try
			{
				SunlikeDataSet _ds = _drpSo.GetTableOsForAudting("SO",Os_No);
				DataTable _dtT = _ds.Tables["TF_POS"];
				DataTable _dtM = _ds.Tables["MF_POS"];				
				//ȡ���ʡ���˰���
				decimal _excRto = 1;
				string _taxId = "1";
				if (_dtM.Rows.Count > 0 )
				{
					if (!String.IsNullOrEmpty(_dtM.Rows[0]["EXC_RTO"].ToString()))
						_excRto = Convert.ToDecimal(_dtM.Rows[0]["EXC_RTO"]);
					if (!String.IsNullOrEmpty(_dtM.Rows[0]["TAX_ID"].ToString()))
						_taxId = _dtM.Rows[0]["TAX_ID"].ToString();
				}

				DataRow[] _drs = _dtT.Select("ITM = " + itm);
				foreach(DataRow _dr in _drs)
				{	
					decimal _up = 0;
					decimal _disCnt = 0;
					decimal _amt = 0 ;
					decimal _taxRto = 0;
					if (!String.IsNullOrEmpty(_dr["UP"].ToString()))
					{
						_up = Convert.ToDecimal(_dr["UP"]);
					}
					if (!(_dr["DIS_CNT"] is System.DBNull))
					{
						_disCnt =	Convert.ToDecimal(_dr["DIS_CNT"]);
					}
					if (!(_dr["TAX_RTO"] is System.DBNull))
					{
						_taxRto =	Convert.ToDecimal(_dr["TAX_RTO"]);
					}
					
					_dr["WH"] = wh;
					_dr["QTY"] = qty;
					if (_disCnt == 0)
					{
						_dr["AMT"] = qty * _up;						
					}
					else
					{
						_dr["AMT"] = qty * _up * _disCnt/100;
					}	
					if (!(_dr["AMT"] is System.DBNull))
					{
						_amt =	Convert.ToDecimal(_dr["AMT"]);
					}
					
					if (_taxId == "2")//Ӧ˰�ں�
					{												
						_dr["TAX"] = _amt * _excRto -((_amt*_excRto) *100 / (100+_taxRto));//(��ҽ��*����)-((��ҽ��*����)*100/(100+˰��))
						_dr["AMTN"] = (_amt*_excRto) *100 / (100+_taxRto);//(��ҽ��*����)*100/(100+˰��)
						if (_dr["TAX"].ToString() == "0")
							_dr["TAX"] = System.DBNull.Value;											
					}
					else if (_taxId == "3")//Ӧ˰���
					{
						_dr["TAX"] = _amt * _excRto* _taxRto/100;//��ҽ��*����*˰��/100
						_dr["AMTN"] = _amt * _excRto * (1-_taxRto/100);//��ҽ��*����*��1-˰��/100��
						if (_dr["TAX"].ToString() == "0")
							_dr["TAX"] = System.DBNull.Value;
					}
					else
					{
						_dr["TAX"] = System.DBNull.Value;
						_dr["AMTN"] = _amt*_excRto ;//(��ҽ��*����)
					}

					_dr["EST_DD"] = est_DD;
					//�жϿ���Ƿ��㹻
					_drpSo.CheckWhUsefulTf(_dr,false);
				}
                _ds.ExtendedProperties["UPD_USR"] = Usr;  
				this.UpdateDataSet(_ds);

				DataTable _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
				if(_errorDt.Rows.Count > 0)
				{
					foreach(DataRow _dr in _errorDt.Rows)
					{
						_error.Append(_dr["TableName"].ToString() + ":" + _dr["REM"].ToString());
					}
				}
			}
			catch(Exception ex)
			{
				_error.Append(ex.Message.ToString());
			}
			return _error.ToString();
		}
		#endregion

		#region �޸��ܶ���ϸ(���� For Auditing)
		/// <summary>
		/// �޸��ܶ���ϸ(���� For Auditing)
		/// </summary>
        /// <param name="usr">�޸���</param>
		/// <param name="Os_No">Ҫ������</param>
		/// <param name="itm">������ITM���</param>
		/// <param name="wh">��λ����</param>
		/// <param name="qty">����</param>
		/// <param name="est_DD"></param>
		/// <returns></returns>
		public string UpdateSoBox(string usr,string Os_No , string itm , string wh , decimal qty, DateTime est_DD)
		{
			DrpSO _drpSo = new DrpSO();
            StringBuilder _error = new StringBuilder();
			decimal _qty_Old = 0;
			decimal _box_Qty_Old = 0;			
			try
			{
				SunlikeDataSet _ds = _drpSo.GetTableOsForAudting("SO",Os_No);
				DataTable _dtT = _ds.Tables["TF_POS"];
				DataTable _dtT1 = _ds.Tables["TF_POS1"];
				DataTable _dtM = _ds.Tables["MF_POS"];
				int _box_Itm = 0;
				//ȡ���ʡ���˰���
				decimal _excRto = 1;
				string _taxId = "1";
				if (_dtM.Rows.Count > 0 )
				{
					if (!String.IsNullOrEmpty(_dtM.Rows[0]["EXC_RTO"].ToString()))
						_excRto = Convert.ToDecimal(_dtM.Rows[0]["EXC_RTO"]);
					if (!String.IsNullOrEmpty(_dtM.Rows[0]["TAX_ID"].ToString()))
						_taxId = _dtM.Rows[0]["TAX_ID"].ToString();
				}

				#region ������
				DataRow[] _drsT1 = _dtT1.Select("ITM = " + itm);
				foreach(DataRow _drT1 in _drsT1)
				{
					_box_Qty_Old = Convert.ToDecimal(_drT1["QTY"]);
					_box_Itm = Convert.ToInt16(_drT1["KEY_ITM"]);

					_drT1["WH"] = wh;
					_drT1["QTY"] = qty;
					_drT1["EST_DD"] = est_DD;
					_drpSo.CheckWhUsefulTfBox(_drT1,false);
					break;
				}
				#endregion
				
				DataRow[] _drsT = _dtT.Select("BOX_ITM = " + _box_Itm);

				#region ��Ʒ����
				foreach(DataRow _drT in _drsT)
				{
					decimal _up = 0;
					decimal _disCnt = 0;
					decimal _amt = 0 ;
					decimal _taxRto = 0;
					if (!String.IsNullOrEmpty(_drT["UP"].ToString()))
					{
						_up = Convert.ToDecimal(_drT["UP"]);
					}
					if (!(_drT["DIS_CNT"] is System.DBNull))
					{
						_disCnt =	Convert.ToDecimal(_drT["DIS_CNT"]);
					}
					if (!(_drT["TAX_RTO"] is System.DBNull))
					{
						_taxRto =	Convert.ToDecimal(_drT["TAX_RTO"]);
					}

					_drT["WH"] = wh;
					_qty_Old = Convert.ToDecimal(_drT["QTY"]);
					int _prdt_Rate = Convert.ToInt32(Convert.ToInt32(_qty_Old) / Convert.ToInt32(_box_Qty_Old));
					_drT["QTY"] = _prdt_Rate * qty;
					

					if (_disCnt == 0)
					{
						_drT["AMT"] = _prdt_Rate * qty * _up;						
					}
					else
					{
						_drT["AMT"] = _prdt_Rate * qty * _up * _disCnt/100;
					}	
					if (!(_drT["AMT"] is System.DBNull))
					{
						_amt =	Convert.ToDecimal(_drT["AMT"]);
					}
					
					if (_taxId == "2")//Ӧ˰�ں�
					{												
						_drT["TAX"] = _amt * _excRto -((_amt*_excRto) *100 / (100+_taxRto));//(��ҽ��*����)-((��ҽ��*����)*100/(100+˰��))
						_drT["AMTN"] = (_amt*_excRto) *100 / (100+_taxRto);//(��ҽ��*����)*100/(100+˰��)
						if (_drT["TAX"].ToString() == "0")
							_drT["TAX"] = System.DBNull.Value;											
					}
					else if (_taxId == "3")//Ӧ˰���
					{
						_drT["TAX"] = _amt * _excRto* _taxRto/100;//��ҽ��*����*˰��/100
						_drT["AMTN"] = _amt * _excRto * (1-_taxRto/100);//��ҽ��*����*��1-˰��/100��
						if (_drT["TAX"].ToString() == "0")
							_drT["TAX"] = System.DBNull.Value;
					}
					else
					{
						_drT["TAX"] = System.DBNull.Value;
						_drT["AMTN"] = _amt*_excRto ;//(��ҽ��*����)
					}


					_drT["EST_DD"] = est_DD;
					_drpSo.CheckWhUsefulTf(_drT,false);
				}
				#endregion

                _ds.ExtendedProperties["UPD_USR"] = usr;
				this.UpdateDataSet(_ds);
				DataTable _errorDt = Sunlike.Business.BizObject.GetAllErrors(_ds);
				if(_errorDt.Rows.Count > 0)
				{
					foreach(DataRow _dr in _errorDt.Rows)
					{
						_error.Append(_dr["TableName"].ToString() + ":" + _dr["REM"].ToString());
					}
				}
			}
			catch(Exception ex)
			{
				  _error.Append(ex.Message);
			}
			return _error.ToString();
		}
		#endregion

		//-------------------------------------------------------------------------

		#region ȡBAR_ROLE
		/// <summary>
		/// ȡBAR_ROLE
		/// </summary>
		/// <returns></returns>
		public DataTable SelectBar_role()
		{
			DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
			return _dbBarCode.SelectBar_role();
		}
		#endregion

		#region �������洫������sql���
		/// <summary>
		/// �������洫������sql���
		/// </summary>
		/// <param name="TableName"></param>
		/// <param name="SqlWhere"></param>
		/// <returns></returns>
		public SunlikeDataSet RunSqlForBox(string TableName,string SqlWhere)
		{
			DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
			return _dbBarCode.RunSqlForBox(TableName,SqlWhere);
		}
		#endregion

		#region �õ������ԭ��
		/// <summary>
		/// �õ������ԭ��
		/// </summary>
		/// <param name="Dep">����</param>
		/// <returns></returns>
		public SunlikeDataSet GetBoxConfig(string Dep)
		{
			DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
			return _dbBarCode.GetBoxConfig(Dep);
		}
		#endregion

		#region ���������ԭ��
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ds"></param>
		/// <returns></returns>
		public SunlikeDataSet UpdateBoxConfig(SunlikeDataSet Ds)
		{
			this.UpdateDataSet(Ds);
			return Ds;
		}
		#endregion

		#region �õ�BAR_BOX�е���ϸ���� by db
		/// <summary>
		/// �õ�BAR_BOX�е���ϸ���� by db
		/// </summary>
		/// <param name="Wh"></param>
		/// <param name="Prd_No"></param>
		/// <param name="Content"></param>
		/// <returns></returns>
		public SunlikeDataSet GetBarBoxByContent(string Wh,string Prd_No,string Content)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			SunlikeDataSet _ds = _bar.GetBarBoxByContent(Wh,Prd_No,Content);
			return _ds;
		}
		#endregion

		#region ��������ҵ
		/// <summary>
		/// �õ�����������
		/// </summary>
		/// <param name="BoxNo">����</param>
		/// <returns></returns>
		public SunlikeDataSet GetBoxChange(string BoxNo)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			SunlikeDataSet _ds = _bar.GetBoxChange(BoxNo);
			return _ds;
		}
		/// <summary>
		/// ��������ҵ
		/// </summary>
		/// <param name="ChangedDS"></param>
		/// <returns></returns>
		public string RollBackUnBox(SunlikeDataSet ChangedDS)
		{
			string _msg = String.Empty;
			if(ChangedDS.Tables[0].Rows.Count > 0)
			{
				DataTable _dt = ChangedDS.Tables[0];
				if(_dt.Rows.Count > 0)
				{
					string _boxNo = _dt.Rows[0]["BOX_NO"].ToString();
					string _wh = _dt.Rows[0]["WH"].ToString();
					string _content = _dt.Rows[0]["CONTENT"].ToString();
					string _prdNo = _dt.Rows[0]["PRD_NO"].ToString();
                    StringBuilder _barNo = new StringBuilder();
					for(int i = 0;i < _dt.Rows.Count;i++)
					{
                        _barNo.Append(_dt.Rows[i]["BAR_NO"].ToString() + ";");
					}
					string[] _aryBar = _barNo.ToString().Split(new char[]{';'});
					StringBuilder _sqlBarWhere = new StringBuilder();					
					for(int i = 0;i < _aryBar.Length;i++)
					{
						if(!string.IsNullOrEmpty(_aryBar[i].Trim()))
						{
							if(i == 0)
							{
                                _sqlBarWhere.Append("'" + _aryBar[i] + "'");								
							}
							else
							{
								_sqlBarWhere.Append(",'" + _aryBar[i] + "'");
							}
						}
					}
					//����MF_BOX
					string _sqlMfBox = "update mf_box set ub_dd=null,qty=qty+1 where wh='"+_wh+"' and box_no='"+_boxNo+"'";
					//����BAR_BOX
					string _sqlBarBox = "update bar_box set stop_id='F' where box_no='"+_boxNo+"'";
					//����BAR_REC
					string _sqlBarRec = "update bar_rec set box_no='"+_boxNo+"',stop_id='F' where bar_no in ("+_sqlBarWhere+")";
					//ɾ��BOX_CHANGE
					string _sqlBoxC = "delete from box_change where box_no='"+_boxNo+"' and bar_no in ("+_sqlBarWhere+")";

					SqlConnection _conn = new SqlConnection(Comp.Conn_DB);
					SqlCommand _comm = new SqlCommand();					
					_conn.Open();
					_comm.Connection = _conn;
					SqlTransaction _trans = _conn.BeginTransaction();
					_comm.Transaction = _trans;
					try
					{
						_comm.CommandText = _sqlMfBox;
						_comm.ExecuteNonQuery();
						_comm.CommandText = _sqlBarBox;
						_comm.ExecuteNonQuery();
						_comm.CommandText = _sqlBarRec;
						_comm.ExecuteNonQuery();
						_comm.CommandText = _sqlBoxC;
						_comm.ExecuteNonQuery();

						try
						{
							WH _wH = new WH();
							_wH.UpdateBoxQty(_prdNo,_wh,_content,WH.BoxQtyTypes.QTY,1);
						}
						catch(Exception _exMsg)
						{
							_msg = _exMsg.Message;
							_trans.Rollback();
						}

						_trans.Commit();
					}
					catch(Exception _ex)
					{
						_msg = _ex.Message;
						_trans.Rollback();
					}
					finally
					{
						_conn.Close();
					}
				}
			}
			return _msg;
		}
		#endregion

		#region ����װ��Ϸ��Լ�鼰����
		/// <summary>
		/// ����װ��Ϸ��Լ�鼰�޸�
		/// </summary>
		/// <param name="ChangedDS"></param>
		/// <param name="SqlBarCode"></param>		
		/// <returns></returns>
		public SunlikeDataSet UpdateBarBox(SunlikeDataSet ChangedDS,string SqlBarCode)
		{
			try
			{
				BarCode _bc = new BarCode();
				DataTable _dtChkBar = _bc.GetBarRecord(SqlBarCode,true);

				this.EnterTransaction();

				if(ChangedDS.Tables[0].Rows[0].RowState != DataRowState.Deleted)
				{
					#region �����к��Ѿ�װ�䣬����в���Ķ���
					if(_dtChkBar.Rows.Count > 0)
					{
						StringBuilder _boxLen = new StringBuilder();
						for(int i = 0; i < _dtChkBar.Rows.Count; i++)
						{
							if(!String.IsNullOrEmpty(_dtChkBar.Rows[i]["BOX_NO"].ToString()))
							{
								if(_boxLen.ToString().IndexOf(_dtChkBar.Rows[i]["BOX_NO"].ToString()) == -1)
								{
                                    _boxLen.Append(_dtChkBar.Rows[i]["BOX_NO"].ToString() + ",");
								}
							}
						
						}
						if(!String.IsNullOrEmpty(_boxLen.ToString().Trim()))
						{
							string[] _boxAry = _boxLen.ToString().Split(new char[]{','});
							_bc.DeleteBox(_boxAry,ChangedDS.Tables["MF_BOX"].Rows[0]["USR"].ToString(),"BO",ChangedDS.Tables["MF_BOX"].Rows[0]["BOX_NO"].ToString());
						}
					}
					#endregion
					
					string _err = _bc.BreakBarCode(ChangedDS);
					if(!String.IsNullOrEmpty(_err))
					{
						ChangedDS.Tables["MF_BOX"].Rows[0].RowError = _err;
						return ChangedDS;
					}
					else
					{
						#region ���ſ���
						if(ChangedDS.ExtendedProperties["BAT_NO"] != null)
						{
							if(!String.IsNullOrEmpty(ChangedDS.ExtendedProperties["BAT_NO"].ToString()))
							{
								//��������ſ��ƣ����жϴ������Ƿ���ڣ���������д���µ�����
								DateTime _bilDd = Convert.ToDateTime(ChangedDS.Tables["MF_BOX"].Rows[0]["BB_DD"]);
								if(string.IsNullOrEmpty(ChangedDS.Tables["MF_BOX"].Rows[0]["WH"].ToString()))
								{
									if(ChangedDS.Tables["BAR_REC"].Rows.Count > 0)
									{
										string _batNo = String.Empty;
										string _prdNo = String.Empty;
										for(int i = 0;i < ChangedDS.Tables["BAR_REC"].Rows.Count;i++)
										{
											if(ChangedDS.Tables["BAR_REC"].Rows[i].RowState != DataRowState.Deleted)
											{
												_prdNo = ChangedDS.Tables["BAR_REC"].Rows[i]["PRD_NO"].ToString();
												_batNo = ChangedDS.Tables["BAR_REC"].Rows[i]["BAT_NO"].ToString();
												break;
											}
										}
										//�ж�PRDT�ж�Ӧ�Ĳ�Ʒ�Ƿ�Ϊ���Ź��Ʋ�Ʒ
										Prdt _prdt = new Prdt();
										bool _isBatPrdt = _prdt.CheckBat(_prdNo);
										if(!_isBatPrdt)
										{
											ChangedDS.Tables["MF_BOX"].Rows[0].RowError = "��Ʒ"+_prdNo+"�������Ź��Ʋ�Ʒ�����鱾���ļ��Ƿ���ȷ";
											return ChangedDS;
										}
										else
										{
											if(!String.IsNullOrEmpty(_batNo))
											{
												Bat _bat = new Bat();
												_bat.AutoInsertData(_batNo,_prdNo,_bilDd);
											}
										}
									}
								}
							}
						}
						#endregion
					}
					this.UpdateBox(ChangedDS);
				}
				else
				{
					#region ����
					if(_dtChkBar.Rows.Count > 0)
					{
                        StringBuilder _boxLen = new StringBuilder() ;
						for(int i = 0; i < _dtChkBar.Rows.Count; i++)
						{
							if(!string.IsNullOrEmpty(_dtChkBar.Rows[i]["BOX_NO",DataRowVersion.Original].ToString()))
							{
								if(_boxLen.ToString().IndexOf(_dtChkBar.Rows[i]["BOX_NO",DataRowVersion.Original].ToString()) == -1)
								{
									_boxLen.Append(_dtChkBar.Rows[i]["BOX_NO",DataRowVersion.Original].ToString() + ",");
								}
							}
						
						}
                        if (!String.IsNullOrEmpty(_boxLen.ToString().Trim()))
						{
                            string[] _boxAry = _boxLen.ToString().Split(new char[] { ',' });
							_bc.DeleteBox(_boxAry,ChangedDS.Tables["MF_BOX"].Rows[0]["USR",DataRowVersion.Original].ToString(),"BO",ChangedDS.Tables["MF_BOX"].Rows[0]["BOX_NO",DataRowVersion.Original].ToString());
						}
					}
					#endregion
				}
			}
			catch(Exception _ex)
			{
				ChangedDS.Tables["MF_BOX"].Rows[0].RowError = _ex.Message;
				this.SetAbort();
			}
			finally
			{
				this.LeaveTransaction();
			}
			return ChangedDS;
		}
		/// <summary>
		/// ����װ����ҵ  // for WinForm OnLine and WebForm
		/// </summary>
		/// <param name="ChangedDS"></param>
		/// <returns>������Ϣ�����û������û����</returns>
		public DataTable UpdateBox(SunlikeDataSet ChangedDS)
		{
			DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
			System.Collections.Hashtable _ht = new System.Collections.Hashtable();
			_ht["MF_BOX"] = "WH,BOX_NO,QTY,BB_DD,UB_DD,DEP,USR,BX_ID,BATCH_NO,SYS_DATE";
			_ht["BAR_BOX"] = "BOX_NO,QTY,CONTENT,WH,PRD_NO,STOP_ID";
			_ht["BAR_REC"] = "BAR_NO,BOX_NO,WH,SPC_NO,PRD_NO,PRD_MARK,BAT_NO,STOP_ID";

			if(ChangedDS.Tables["MF_BOX"].Rows[0].RowState == DataRowState.Deleted)
			{
//				SunlikeDataSet _ds = _bar.GetBoxData(ChangedDS.Tables["MF_BOX"].Rows[0]["BOX_NO",DataRowVersion.Original].ToString(), false);
//				ChangedDS.Clear();
//				ChangedDS.AcceptChanges();
//
//				_ds.Tables["MF_BOX"].Rows[0]["QTY"] = Convert.ToInt32(_ds.Tables["MF_BOX"].Rows[0]["QTY"]) - 1;
//				_ds.Tables["MF_BOX"].Rows[0]["UB_DD"] = DateTime.Now.ToString(Comp.SQLDateTimeFormat);
//				//ͣ��������
//				_ds.Tables["BAR_BOX"].Rows[0]["STOP_ID"] = "T";
//				//��ɢ���к�
//				DataTable _dtBody = _ds.Tables["BAR_REC"];
//				for (int i=0;i<_dtBody.Rows.Count;i++)
//				{
//					_dtBody.Rows[i]["BOX_NO"] = System.DBNull.Value;
//				}
//				ChangedDS = _ds.Copy();
			}
			else
			{
				if(ChangedDS.Tables["BAR_BOX"].Rows[0]["STOP_ID"].ToString() != "T")
				{					
					//���Ϊ����״̬��ץȡBAR_PRINT�е�SPC���ǵ�������SPC
					if(ChangedDS.Tables["BAR_REC"].Rows.Count > 0)
					{
						StringBuilder _sqlWh =  new StringBuilder("WHERE 1<>1 ");
						string _spc = "";
						for(int i = 0; i < ChangedDS.Tables["BAR_REC"].Rows.Count; i++)
						{
							_spc = _bar.GetBarPrintSpc(ChangedDS.Tables["BAR_REC"].Rows[i]["BAR_NO"].ToString());
							if(!String.IsNullOrEmpty(_spc))
							{
								ChangedDS.Tables["BAR_REC"].Rows[i]["SPC_NO"] = _spc;
							}
                            _sqlWh.Append(" OR BAR_NO='" + ChangedDS.Tables["BAR_REC"].Rows[i]["BAR_NO"].ToString() + "' ");
						}
						DataTable _dtPrintDel = _bar.GetBarPrintSpcForDel(_sqlWh.ToString());
						if(_dtPrintDel.Rows.Count > 0)
						{
							for(int i = 0;i < _dtPrintDel.Rows.Count;i++)
							{
								_dtPrintDel.Rows[i].Delete();
							}
							DataTable _dtPD = _dtPrintDel.Copy();
							if(ChangedDS.Tables.Contains("BAR_PRINT"))
							{
								ChangedDS.Tables.Remove("BAR_PRINT");
							}
							ChangedDS.Tables.Add(_dtPD);
							_ht["BAR_PRINT"] = "BAR_NO,SPC_NO,PRN_DD,DEP";
						}
					}
				}
			}
			this._runAfterUpdate = true;
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
        /// <param name="status"></param>
        protected override void BeforeUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status)
        {
            if (tableName == "MF_BOX")
            {
                if (statementType != StatementType.Delete)
                {
                    dr["SYS_DATE"] = Convert.ToDateTime(DateTime.Now.ToString(Comp.SQLDateTimeFormat));
                }
            }
            if (tableName == "TF_POS")
            {
                string usr = "";
                DataTable dt = dr.Table;
                if (dt != null && dt.DataSet != null)
                {
                    if (dt.DataSet.ExtendedProperties.Contains("UPD_USR"))
                    {
                        usr = dt.DataSet.ExtendedProperties["UPD_USR"].ToString();
                    }
                    else
                    {
                        usr = "NO";
                    }
                }

                //����δ�����
                DRPYHut _drpYh = new DRPYHut();
                if (dr.RowState == DataRowState.Deleted || dr.RowState == DataRowState.Modified)
                {
                    if (!String.IsNullOrEmpty(dr["OTH_ITM", DataRowVersion.Original].ToString()))
                    {
                        _drpYh.UpdateQtySoUnSh(usr, dr["BIL_ID", DataRowVersion.Original].ToString(), dr["QT_NO", DataRowVersion.Original].ToString(),
                            Convert.ToInt32(dr["OTH_ITM", DataRowVersion.Original]), Convert.ToDecimal(dr["QTY", DataRowVersion.Original]) * -1);
                    }
                }
                if (dr.RowState != DataRowState.Deleted && dr.RowState != DataRowState.Unchanged)
                {
                    if (!String.IsNullOrEmpty(dr["OTH_ITM", DataRowVersion.Original].ToString()))
                    {
                        _drpYh.UpdateQtySoUnSh(usr, dr["BIL_ID"].ToString(), dr["QT_NO"].ToString(),
                            Convert.ToInt32(dr["OTH_ITM"]), Convert.ToDecimal(dr["QTY"]));
                    }
                }
            }
        }

		/// <summary>
		/// ���浥��֮��Ķ���
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="statementType"></param>
		/// <param name="dr"></param>
		/// <param name="status"></param>
		/// <param name="recordsAffected"></param>
		protected override void AfterUpdate(string tableName, StatementType statementType, DataRow dr, ref UpdateStatus status, int recordsAffected)
		{
			if (this._runAfterUpdate)
			{
				if (tableName == "BAR_BOX")
				{
					//ֻ��������ɾ������������ڣ�����������������¶����޸ĵ�����
					string _prdNo = dr["PRD_NO"].ToString();
					string _whNo = dr["WH"].ToString();
					string _content = dr["CONTENT"].ToString();
					int _qty = 1;
					if (dr["STOP_ID"].ToString() == "T")
					{
						_qty = -1;
					}
					if (!String.IsNullOrEmpty(_whNo))
					{
						WH _wh = new WH();
						try
						{
							_wh.UpdateBoxQty(_prdNo,_whNo,_content,WH.BoxQtyTypes.QTY,_qty);
						}
						catch(Exception _ex)
						{
							dr.RowError = _ex.Message;
							status = UpdateStatus.SkipAllRemainingRows;
						}
					}
					string _boxNo = dr["BOX_NO"].ToString();
					DbBarCode _bar = new DbBarCode(Comp.Conn_DB);
					_bar.UpdateDateTimeBox(_boxNo);
				}
			}
		}

		#endregion

        #region BeforeDsSave(����׷��)
        protected override void BeforeDsSave(DataSet ds)
        {
            //#region ����׷��
            //if (ds.Tables.Contains("MF_POS"))
            //{
            //    DataTable _dt = ds.Tables["MF_POS"];
            //    if (_dt.Rows.Count > 0 && _dt.Rows[0].RowState != DataRowState.Added)
            //    {
            //        Sunlike.Business.DataTrace _dataTrce = new DataTrace(); string _bilId = "";
            //        if (_dt.Rows[0].RowState != DataRowState.Deleted)
            //        {
            //            _bilId = _dt.Rows[0]["OS_ID"].ToString();
            //        }
            //        else
            //        {
            //            _bilId = _dt.Rows[0]["OS_ID", DataRowVersion.Original].ToString();
            //        }
            //        _dataTrce.SetDataHistory(SunlikeDataSet.ConvertTo(ds), _bilId);
            //    }
            //}
            //#endregion

            base.BeforeDsSave(ds);
        }
        #endregion
    }
}
