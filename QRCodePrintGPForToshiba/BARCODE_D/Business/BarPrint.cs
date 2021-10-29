using System;
using System.Data;
using Sunlike.Business.Data;
using Sunlike.Common.CommonVar;
using System.Text;
namespace Sunlike.Business
{
	/// <summary>
	/// Summary description for BarPrint.
	/// </summary>
	public class BarPrint: BizObject
	{
		/// <summary>
		/// 
		/// </summary>
		public BarPrint()
		{
		}
		#region ȡ�ô�ӡ���õ�ֵ ע(prn_idΪ���������е�����)
		/// <summary>
		/// ȡ�ô�ӡ���õ�ֵ ע(prn_idΪ���������е�����)
		/// </summary>
		/// <param name="prn_id"></param>
		/// <returns></returns>
		public DataTable SelectBarPrnSet(string prn_id)
		{
			DbBarPrint _dbBarPrint = new DbBarPrint(Comp.Conn_DB);
			return _dbBarPrint.SelecrtBarPrnSet(prn_id);
		}
		#endregion

		#region ��ѯ���кŵ���ˮ��
		/// <summary>
		///  ��ѯ���кŵ���ˮ��
		/// </summary>
		/// <param name="prd_no"></param>
		/// <param name="prd_mark"></param>
		/// <returns></returns>
		public DataTable SelectBarSqNo(string prd_no,string prd_mark)
		{
			DbBarPrint _dbBarPrint = new DbBarPrint(Comp.Conn_DB);
			return _dbBarPrint.SelectBarSqNo(prd_no,prd_mark);
		}
		/// <summary>
		/// ȡ����󵥺�
		/// </summary>
		/// <param name="prd_no"></param>
		/// <param name="prd_mark"></param>
		/// <returns></returns>
		public string GetMaxNo(string prd_no,string prd_mark)
		{
			string _result = "";
			DataTable _barSqNoTable = SelectBarSqNo(prd_no,prd_mark);
			if (_barSqNoTable.Rows.Count  > 0 )
			{
				_result = _barSqNoTable.Rows[0]["MAX_NO"].ToString();
			}
			else
			{
				_result = "0";
			}
			return _result;
		}
		#endregion

		#region ��ѯ�Ѿ���ӡ�����кŵ���
		/// <summary>
		/// ��ѯ�Ѿ���ӡ�����кŵ���
		/// </summary>
		/// <param name="bar_no"></param>
		/// <returns></returns>
		public DataTable SelectBarPrint(string bar_no)
		{
			DbBarPrint _dbBarPrint = new DbBarPrint(Comp.Conn_DB);
			return _dbBarPrint.SelectBarPrint(bar_no);
		}
		#endregion

		#region �ж��Ƿ��Ѿ���ӡ
		/// <summary>
		/// �ж��Ƿ��Ѿ���ӡ
		/// </summary>
		/// <param name="bar_no"></param>
		///  <param name="sn"></param>
		///   <param name="page_count"></param>
		/// <returns></returns>
		public bool IsPrinted(string bar_no,string sn,int page_count)
		{
			bool _printed = false;
			int _snLength = sn.Length;
            
            StringBuilder _bar_nos = new StringBuilder();
			for (int i = 0 ; i < page_count ;i++)
			{
                StringBuilder _bar_no = new StringBuilder(bar_no);
				int _snStart = Convert.ToInt32(sn);
				_snStart += i;
				for (int j = 0 ; j < _snLength - _snStart.ToString().Length; j ++)
				{
					_bar_no.Append("0");
				}
				_bar_no.Append(_snStart.ToString());

				if (_bar_nos.Length > 0)
				{
					_bar_nos.Append(",");
				}
				_bar_nos.Append(_bar_no);
			}
			int _maxWhereLength = 1024;
			string _subWhere;
			string _tmpWhere = "";
			int _pos;
			while (true)
			{
				DbBarPrint _dbBarPrint = new DbBarPrint(Comp.Conn_DB);
				DbBarCode _dbBarCode = new DbBarCode(Comp.Conn_DB);
				if (_bar_nos.Length > _maxWhereLength)
				{
					_subWhere = _bar_nos.ToString().Substring(0,_maxWhereLength-1);
					_pos = _subWhere.LastIndexOf(",");
					_tmpWhere = _subWhere.Substring(0,_pos);
					DataTable _barPrintTable = _dbBarPrint.GetBarPrint(_tmpWhere);
					if (_barPrintTable.Rows.Count > 0 )
					{
						_printed = true;
						break;
					}
					
					DataTable _barCodeTable = _dbBarCode.GetBarRecord( "BAR_NO IN ('"+_tmpWhere.Replace(",","','")+"')",true, null);
					if (_barCodeTable.Rows.Count > 0 )
					{
						_printed = true;
						break;						
					}
					_bar_nos =  new StringBuilder(_bar_nos.ToString().Substring(_pos+1,_bar_nos.Length-_pos-1));
				}
				else
				{
					_tmpWhere = _bar_nos.ToString();					
					DataTable _barPrintTable = _dbBarPrint.GetBarPrint(_tmpWhere);
					if (_barPrintTable.Rows.Count > 0 )
					{
						_printed = true;
						break;
					}
					DataTable _barCodeTable = _dbBarCode.GetBarRecord( "BAR_NO IN ('"+_tmpWhere.Replace(",","','")+"')",true, null);
					if (_barCodeTable.Rows.Count > 0 )
					{
						_printed = true;
						break;						
					}
					break;
				}

			}
			return _printed;
		}

		#endregion

		#region �����ӡ��Ϣ
		/// <summary>
		/// 
		/// </summary>
		/// <param name="usr"></param>
		/// <param name="printHistroyDS"></param>
		/// <returns></returns>
		public SunlikeDataSet SavePrintData(string usr,SunlikeDataSet printHistroyDS)
		{
			SunlikeDataSet _ds = null;
            try
            {
                for (int i = 0; i < printHistroyDS.Tables[0].Rows.Count; i++)
                {
                    if (printHistroyDS.Tables[0].Rows[i].RowState == System.Data.DataRowState.Deleted)
                        continue;
                    string _bar_no = printHistroyDS.Tables[0].Rows[i]["BAR_NO"].ToString();
                    string _prdNo = printHistroyDS.Tables[0].Rows[i]["PRD_NO1"].ToString();
                    string _prd_mark = printHistroyDS.Tables[0].Rows[i]["PRD_MARK"].ToString();
                    string _spc_no = printHistroyDS.Tables[0].Rows[i]["SPC_NO"].ToString();
                    string _sn = printHistroyDS.Tables[0].Rows[i]["SN"].ToString();
                    string _dep = printHistroyDS.Tables[0].Rows[i]["DEP"].ToString();
                    Dept _dept = new Dept();
                    //���ż��
                    if (!_dept.IsExist(usr, _dep))
                    {
                        printHistroyDS.Tables[0].Rows[i].SetColumnError("DEP",/*���Ŵ���[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.DEPTERROR,PARAM=" + _dep);
                        this.SetAbort();
                    }
                    //��Ʒ���
                    Prdt _prdt = new Prdt();
                    if (!_prdt.IsExist(usr, _prdNo))
                    {
                        printHistroyDS.Tables[0].Rows[i].SetColumnError("PRD_NO",/*Ʒ��[{0}]������û�ж��������Ȩ��,����*/"RCID=COMMON.HINT.PRDNOERROR,PARAM=" + _prdNo);
                        this.SetAbort();
                    }
                    //�������
                    PrdMark _prdMark = new PrdMark();

                    if (_prdMark.RunByPMark(usr))
                    {
                        string[] _prd_markAry = _prdMark.BreakPrdMark(_prd_mark);
                        DataTable _markTable = _prdMark.GetSplitData("");
                        for (int j = 0; j < _markTable.Rows.Count; j++)
                        {
                            string _markName = _markTable.Rows[j]["FLDNAME"].ToString();
                            if (!_prdMark.IsExist(_markName, _prdNo, _prd_markAry[j]))
                            {
                                printHistroyDS.Tables[0].Rows[i].SetColumnError(_markName,/*��Ʒ����[{0}]������,����*/"RCID=COMMON.HINT.PRDMARKERROR,PARAM=" + _prd_markAry[j].Trim());
                                this.SetAbort();
                            }
                        }
                    }


                    int _page_count = Convert.ToInt32(printHistroyDS.Tables[0].Rows[i]["PAGE_COUNT"]);
                    DbBarPrint _dbBarPrint = new DbBarPrint(Comp.Conn_DB);

                    this.EnterTransaction();
                    _dbBarPrint.SavePrintData(_bar_no, _prdNo, _prd_mark, _spc_no, _dep, _page_count, _sn);
                    //_dbBarPrint.InsertBarSqNo(_prd_no,_prd_mark,_max_no);
                    //_dbBarPrint.InsertBarPrint(_bar_no,_spc_no,System.DateTime.Now.ToShortDateString(),_dep);					
                    this.SetComplete();
                }
                _ds = printHistroyDS;
            }
            catch (Exception _ex)
            {
                this.SetAbort();
                throw _ex;

            }
			finally
			{
				this.LeaveTransaction();
			}
			return _ds;
		}
		#endregion

		#region ��ѯ���
		/// <summary>
		///  ��ѯ���й��
		/// </summary>
		/// <returns></returns>
		public DataTable SelectBar_spc()
		{
			DbBarPrint _dbBarPrint= new DbBarPrint(Comp.Conn_DB);
			return _dbBarPrint.SelectBar_spc("");

		}
		/// <summary>
		///  ��ѯ���������ŵĹ��
		/// </summary>
		/// <param name="spc_no"></param>
		/// <returns></returns>
		public DataTable SelectBar_spc(string spc_no)
		{
			DbBarPrint _dbBarPrint= new DbBarPrint(Comp.Conn_DB);
			return _dbBarPrint.SelectBar_spc(spc_no);
		}
		/// <summary>
		/// ��ѯ���������ŵ�����
		/// </summary>
		/// <param name="spc_no"></param>
		/// <returns></returns>
		public string GetSpc_name(string spc_no)
		{
			string _result = "";
			DbBarPrint _dbBarPrint= new DbBarPrint(Comp.Conn_DB);
			DataTable _spcTable = new DataTable();
			_spcTable =  _dbBarPrint.SelectBar_spc(spc_no);
			if (_spcTable.Rows.Count > 0)
			{
				_result = _spcTable.Rows[0]["NAME"].ToString();
			}
			return _result;
		}
		#endregion 

		#region �������
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="spc_no"></param>
		/// <param name="name"></param>
		public void InsertBar_spc(string spc_no,string name)
		{
			DbBarPrint _dbBarPrint = new DbBarPrint(Comp.Conn_DB);
			_dbBarPrint.InsertBar_spc(spc_no,name);
		}
		#endregion

		#region �޸Ĺ��
		/// <summary>
		/// �޸Ĺ��
		/// </summary>
		/// <param name="spc_no"></param>
		/// <param name="name"></param>
		public void UpdateBar_spc(string spc_no,string name)
		{
			DbBarPrint _dbBarPrint = new DbBarPrint(Comp.Conn_DB);
			_dbBarPrint.UpdateBar_spc(spc_no,name);
		}
		/// <summary>
		/// ɾ�����
		/// </summary>
		/// <param name="spc_no"></param>
		public void DeleteBar_spc(string spc_no)
		{
			DbBarPrint _dbBarPrint = new DbBarPrint(Comp.Conn_DB);
			_dbBarPrint.DeleteBar_spc(spc_no);

		}
		#endregion
	}
}
