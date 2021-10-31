using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BarCodePrintTSB
{
	public partial class FTSBPrint : Form
	{
		private DataSet _printHistoryDS;
        private string _batNo = "";

		#region Constructor
		public FTSBPrint()
		{
			InitializeComponent();
		}
		#endregion

		#region function

		#region ���õ�ǰ��ˮ��
		private void GetPrintSN()
		{
            //��ˮ���ܳ��̶�3λ
			int _SNMax = 0;
			int SNLength = 0;
			StringBuilder _sn = new StringBuilder();

			DataRow[] _findDrAry = _printHistoryDS.Tables[0].Select("BAR_NO='" + GetBarContent() + "'");
			if (_findDrAry.Length > 0)//�����ʷ�����м�¼��ô�͵���¼�в������ˮ��
			{
				if (!String.IsNullOrEmpty(_findDrAry[_findDrAry.Length - 1]["SN"].ToString()))
					_SNMax = Convert.ToInt32(_findDrAry[_findDrAry.Length - 1]["SN"]);

                //int _page_count = 0;
                //if (!String.IsNullOrEmpty(_findDrAry[_findDrAry.Length - 1]["PAGE_COUNT"].ToString()))
                //    _page_count = Convert.ToInt32(_findDrAry[_findDrAry.Length - 1]["PAGE_COUNT"]);

				_SNMax += 1;

				SNLength = _SNMax.ToString().Length;
				for (int i = 0; i < 3 - SNLength; i++)
				{
					_sn.Insert(0, "0");
				}

			}
			else
			{
				string _bat_no = txtBatNo.Text;
				if (!String.IsNullOrEmpty(_bat_no))
                {
                    //ȡ��ˮ��.�˻���ˮ��ֻ�������ߣ���Ʒ��û�й�ϵ
					onlineService.BarPrintServer _brSer = new global::BarCodePrintTSB.onlineService.BarPrintServer();
					_brSer.UseDefaultCredentials = true;
					_SNMax = Convert.ToInt32(_brSer.GetMaxSN("",_bat_no));
				}
				_SNMax += 1;

				SNLength = _SNMax.ToString().Length;
				for (int i = 0; i < 3 - SNLength; i++)
				{
					_sn.Insert(0, "0");
				}
			}
			txtSN.Text = _sn.ToString() + _SNMax.ToString();
		}
		#endregion

		#region ������ӡ��ʷ��
		/// <summary>
		/// ������ӡ��ʷ��
		/// </summary>
		/// <returns></returns>
		private DataTable CreatePrintHistory()
		{
			DataTable _barPrintTable = new DataTable("BARPRINT");
			DataColumn _pkColumn = new DataColumn("PK", System.Type.GetType("System.Int32"));
			_pkColumn.AutoIncrement = true;
			_pkColumn.AutoIncrementSeed = 1;
			_pkColumn.AutoIncrementStep = 1;
			_barPrintTable.Columns.Add(_pkColumn);
			_barPrintTable.Columns.Add("BAR_NO", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("SN", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("PRD_NO1", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("PRD_NO2", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("PRD_MARK1", System.Type.GetType("System.String"));

            _barPrintTable.Columns.Add("PRD_NAME", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("PRD_SPC", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("IDX1", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("IDX_NAME", System.Type.GetType("System.String"));

			_barPrintTable.Columns.Add("SPC_NO", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("SPC_NAME", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("REM", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("PAGE_COUNT", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("COPY_COUNT", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("DEP", System.Type.GetType("System.String"));
			_barPrintTable.PrimaryKey = new DataColumn[1] { _pkColumn };
			return _barPrintTable;
		}
		#endregion

		#region �������ݵ���ʷ��
		/// <summary>
		/// �������ݵ���ʷ��
		/// </summary>
		/// <param name="printHistory"></param>
		private void AddToPrintHistory(DataTable printHistory)
        {
            StringBuilder _sn = null;
            int _snno = 0;

            #region ��Ʒ���
            onlineService.BarPrintServer _brs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
            _brs.UseDefaultCredentials = true;
            DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text.Trim(),"");
            #endregion

            for (int i = 0; i < Convert.ToInt32(txtCount.Text); i++)
            {
                DataRow _dr = printHistory.NewRow();
                _dr["BAR_NO"] = GetBarContent();
                _dr["PRD_NO"] = GetBarContent(txtPrdNo);//���ո�
                _dr["PRD_NO1"] = txtPrdNo.Text;
                _dr["PRD_NO2"] = GetBarContent(txtPrdNo);//���ո�
                _dr["PRD_MARK"] = GetBarContent(txtBatNo);//���ո�
                _dr["PRD_MARK1"] = txtBatNo.Text;

                if (_ds != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        _dr["PRD_NAME"] = _ds.Tables[0].Rows[0]["NAME"].ToString();
                        _dr["PRD_SPC"] = _ds.Tables[0].Rows[0]["SPC"].ToString();
                        _dr["IDX1"] = _ds.Tables[0].Rows[0]["IDX1"].ToString();
                        _dr["IDX_NAME"] = _ds.Tables[0].Rows[0]["IDX_UP"].ToString();
                    }
                }
                _sn = new StringBuilder();
                _snno = Convert.ToInt32(txtSN.Text);
                _snno = _snno + i;
                for (int j = 0; j < 3 - _snno.ToString().Length; j++)
                {
                    _sn.Insert(0, "0");
                }
                _dr["SN"] = _sn.ToString() + _snno.ToString();
                _dr["SPC_NO"] = DBNull.Value;
                _dr["SPC_NAME"] = DBNull.Value;
                _dr["PAGE_COUNT"] = txtCount.Text;
                _dr["REM"] = DBNull.Value;
                _dr["COPY_COUNT"] = 1;
                _dr["DEP"] = BarRole.DEP;
                printHistory.Rows.Add(_dr);
            }
            printHistory.AcceptChanges();
		}

		#endregion

		#region ת������ӡ�����к����ϼ�
		/// <summary>
		/// ת������ӡ�����к����ϼ�
		/// </summary>
		/// <param name="printHistoryDS"></param>
		/// <returns></returns>
		public DataSet MakePrintBarCode(DataSet printHistoryDS)
		{
			DataSet _barCodeDS = new DataSet();
			//-------BARCODE1------
			DataTable _barCodeTable1 = new DataTable("BARCODE1");
			_barCodeTable1.Columns.Add("BARCODE", System.Type.GetType("System.String"));
			_barCodeTable1.Columns[0].MaxLength = 90;
			_barCodeTable1.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
			_barCodeTable1.Columns["PRD_NO"].MaxLength = 30;
			_barCodeTable1.Columns.Add("PRD_NAME", System.Type.GetType("System.String"));
			_barCodeTable1.Columns["PRD_NAME"].MaxLength = 50;
			_barCodeTable1.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
			_barCodeTable1.Columns["PRD_MARK"].MaxLength = 40;
			_barCodeTable1.Columns.Add("IDX1", System.Type.GetType("System.String"));
            _barCodeTable1.Columns["IDX1"].MaxLength = 50;
            _barCodeTable1.Columns.Add("IDX_NAME", System.Type.GetType("System.String"));
            _barCodeTable1.Columns["IDX_NAME"].MaxLength = 50;
			_barCodeTable1.Columns.Add("SPC_NO", System.Type.GetType("System.String"));
			_barCodeTable1.Columns["SPC_NO"].MaxLength = 10;
			_barCodeTable1.Columns.Add("SPC_NAME", System.Type.GetType("System.String"));
			_barCodeTable1.Columns["SPC_NAME"].MaxLength = 50;
            _barCodeTable1.Columns.Add("PRD_SPC", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("INIT_NO", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("PAGE_COUNT", System.Type.GetType("System.Int32"));
            _barCodeTable1.Columns.Add("COPY_COUNT", System.Type.GetType("System.Int32"));
			_barCodeTable1.Columns.Add("DEP", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("DEP_NAME", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("PX", System.Type.GetType("System.String"));
			_barCodeTable1.Columns.Add("PY", System.Type.GetType("System.String"));

			for (int i = 0; i < printHistoryDS.Tables[0].Rows.Count; i++)
			{
				if (printHistoryDS.Tables[0].Rows[i].RowState != DataRowState.Deleted)
				{
					//-------------BARCODE1------------
					DataRow _dr1 = _barCodeTable1.NewRow();
					_dr1["BARCODE"] = printHistoryDS.Tables[0].Rows[i]["BAR_NO"];
					_dr1["PRD_NO"] = printHistoryDS.Tables[0].Rows[i]["PRD_NO1"];
					_dr1["PRD_MARK"] = printHistoryDS.Tables[0].Rows[i]["PRD_MARK1"];
					_dr1["INIT_NO"] = printHistoryDS.Tables[0].Rows[i]["SN"];

                    _dr1["IDX1"] = printHistoryDS.Tables[0].Rows[i]["IDX1"];
                    _dr1["IDX_NAME"] = printHistoryDS.Tables[0].Rows[i]["IDX_NAME"];
                    _dr1["PRD_NAME"] = printHistoryDS.Tables[0].Rows[i]["PRD_NAME"];
					_dr1["PRD_SPC"] = printHistoryDS.Tables[0].Rows[i]["PRD_SPC"];

					_dr1["SPC_NO"] = printHistoryDS.Tables[0].Rows[i]["SPC_NO"];
					_dr1["SPC_NAME"] = printHistoryDS.Tables[0].Rows[i]["SPC_NAME"];
                    _dr1["DEP"] = printHistoryDS.Tables[0].Rows[i]["DEP"];
                    //�����������ϵͳû���õ���Ĭ��Ϊ1�����������к���ˮΪ1�����PAGE_COUNTΪ5�������ӡʱ���Զ�����ˮ��Ϊ��5�����룩
                    //��ϵͳû���õ������Դ˴������ò���Ӱ��ϵͳ
                    if (!(printHistoryDS.Tables[0].Rows[i]["PAGE_COUNT"] is System.DBNull) && !String.IsNullOrEmpty(printHistoryDS.Tables[0].Rows[i]["PAGE_COUNT"].ToString()))
                        _dr1["PAGE_COUNT"] = printHistoryDS.Tables[0].Rows[i]["PAGE_COUNT"];
                    else
                        _dr1["PAGE_COUNT"] = 1;
                    //��ӡ��������ϵͳû���õ���Ĭ��Ϊ1������������ΪA001�����COPY_COUNTΪ5�����ӡ5��A001�������ǩ��
                    //��ϵͳû���õ������Դ˴������ò���Ӱ��ϵͳ
                    if (!(printHistoryDS.Tables[0].Rows[i]["COPY_COUNT"] is System.DBNull) && !String.IsNullOrEmpty(printHistoryDS.Tables[0].Rows[i]["COPY_COUNT"].ToString()))
                        _dr1["COPY_COUNT"] = printHistoryDS.Tables[0].Rows[i]["COPY_COUNT"];
                    else
                        _dr1["COPY_COUNT"] =1;
					_barCodeTable1.Rows.Add(_dr1);
				}
			}
			_barCodeDS.Tables.Add(_barCodeTable1);
			return _barCodeDS;
		}
		/// <summary>
		/// ת������ӡ�����к����ϼ�
		/// </summary>
		/// <param name="barCodeDS"></param>
		/// <returns></returns>
		public bool ToPrintBarCode(DataSet barCodeDS)
		{
			bool _result = true;
            BarSet _bs = new BarSet();
			_result = _bs.PrintBarCode(barCodeDS,"1",cbFormat.SelectedIndex.ToString());
			return _result;
		}
		#endregion

		#region �õ�bar_role
		/// <summary>
		/// �õ�bar_role
		/// </summary>
		/// <returns></returns>
		private void GetBar_role()
		{

			BarRole _barRole = new BarRole();
			try
			{
				_barRole.GetBarRole();
			}

			catch
			{
				MessageBox.Show("û���趨����ı���ԭ��");
                return;
			}
		}
		#endregion

		#region ȡ�����к�����
		/// <summary>
		/// ȡ�����к�����
		/// </summary>
		/// <returns></returns>
		private string GetBarContent()
		{
			StringBuilder _bar = new StringBuilder();

			//��Ʒ����
			_bar.Append(ReplaceSpace("PRD_NO", txtPrdNo.Text));
			//����
            _bar.Append(ReplaceSpace("BAT_NO", txtBatNo.Text));

			return _bar.ToString();
		}

		#endregion

		#region ȡ�ø������ƿؼ�������
		/// <summary>
		/// ȡ�ø������ƿؼ�������
		/// </summary>
		/// <param name="controlName">�����ؼ�</param>
		/// <returns></returns>
		private string GetBarContent(TextBox _txtBox)
		{
			StringBuilder _result = new StringBuilder();
			string _keyName = "";
			if (_txtBox.Name == "txtPrdNo")
				_keyName = "PRD_NO";
			if (_txtBox.Name == "txtBatNo")
				_keyName = "BAT_NO";
			_result.Append(ReplaceSpace(_keyName, _txtBox.Text.Trim()));
			return _result.ToString();
		}
		#endregion

		#region ����bar_role�����ո�(������ˮ��)
		/// <summary>
		/// ����bar_role�����ո�(������ˮ��)
		/// </summary>
		/// <param name="keyName"></param>
		/// <param name="inputString"></param>
		/// <returns></returns>
		public string ReplaceSpace(string keyName, string inputString)
		{
			StringBuilder _result = new StringBuilder();
			//ȡ���滻�ַ�
            string _replaceStr = BarRole.TrimChar;
			int _count = 0;
            if (keyName == "PRD_NO")
            {
                _count = BarRole.EPrdt - BarRole.SPrdt + 1;//��Ʒ����
            }
            if (keyName == "BAT_NO")
            {
                _count = BarRole.ESn - BarRole.BSn - 2;//�˴��õ����ų��ȣ���ˮ�ų���=����+3λ��ˮ�ţ�
            }

			for (int i = 0; i < _count - inputString.Length; i++)
			{
				_result.Append(_replaceStr);
			}
			return inputString + _result.ToString();//�ڻ�Ʒ�����ź��油�ո�
		}
		#endregion

        #region ȡ����
        private void GetPrintBatNo()
        {
            #region ȡ��ǰ����
            string _yy = System.DateTime.Now.Year.ToString().Substring(2);//��
            string _mm = System.DateTime.Now.Month.ToString();//��
            if (_mm == "10")
            {
                _mm = "A";
            }
            if (_mm == "11")
            {
                _mm = "B";
            }
            if (_mm == "12")
            {
                _mm = "C";
            }
            string _dd = System.DateTime.Now.Day.ToString();//��
            if (_dd.Length == 1)
            {
                _dd = "0" + _dd;
            }
            string _ftdd = "0" + _dd;
            _dd = "90" + _dd;
            _batNo = _yy + _mm + _dd + _ftdd;
            txtBatNo.Text = _batNo;
            #endregion
        }
        #endregion

        #endregion

        private void btnClear_Click(object sender, EventArgs e)
		{
            txtPrdNo.Text = "";
            txtSpc.Text = "";
            txtCount.Text = "1";
            GetPrintBatNo();
            GetPrintSN();
		}

		private void bnXpAddPrint_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(txtPrdNo.Text.Trim()))
			{
				MessageBox.Show("��ƷƷ�Ų���Ϊ�գ�");
				return;
			}
			if (String.IsNullOrEmpty(this.txtBatNo.Text.Trim()))
			{
                GetPrintBatNo();
			}
            try
            {
				AddToPrintHistory(_printHistoryDS.Tables[0]);
				this.dataGridView1.DataMember = "BARPRINT";
				dataGridView1.DataSource = _printHistoryDS;
				dataGridView1.Refresh();
				this.dataGridView1.AllowUserToAddRows = false;
				GetPrintSN();
            }
            catch (Exception _ex)
            {
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("�����ӡʧ�ܣ�ԭ��\n" + _err);
            }
		}

		private void bnXpClear_Click(object sender, EventArgs e)
		{
            DialogResult _result;
            _result = MessageBox.Show("ȷ��������д���ӡ�ļ�¼��", "��ʾ", MessageBoxButtons.YesNo);
            if (_result == DialogResult.Yes)
            {
                _printHistoryDS.Tables[0].Clear();
                GetPrintSN();
            }
		}

		private void bnXpPrint_Click(object sender, EventArgs e)
        {
            bool _hasPrinted = false;

			DataRow[] _selDr = _printHistoryDS.Tables[0].Select();
            if (_selDr.Length > 0)
            {
                string _BarPrinted = "";
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                for (int i = 0; i < _selDr.Length; i++)
                {
                    string _bar_no = _selDr[i]["BAR_NO"].ToString();
                    string _sn = _selDr[i]["SN"].ToString();
                    int _page_count = 0;
                    if (!(_selDr[i]["PAGE_COUNT"] is System.DBNull) && (!String.IsNullOrEmpty(_selDr[i]["PAGE_COUNT"].ToString())))
                    {
                        _page_count = Convert.ToInt32(_selDr[i]["PAGE_COUNT"]);
                    }
                    //�ж��Ƿ��Ѿ���ӡ
                    if (_bps.SBIsPrinted(_selDr[i]["PRD_MARK1"].ToString(), _sn))
                    {
                        if (i > 0)
                        {
                            _BarPrinted += ",";
                        }
                        _hasPrinted = true;
                        _BarPrinted += _bar_no + _sn;
                    }
                }
                if (_hasPrinted)
                {
                    MessageBox.Show(String.Format("���к�[{0}]�Ѵ�ӡ��", _BarPrinted));
                    return;
                }
                //�����ӡ��Ϣ
                _printHistoryDS.ExtendedProperties["SB_BAR_PRINT"] = "T";//��ע��TΪ�˻������ӡ�������������ˮ��ʱ��ֻ�豣���Ӧ�����ţ�Ʒ�Ų��豣�棩
                DataSet _ds = _bps.SavePrintData(BarRole.USR_NO, _printHistoryDS);
                if (_ds.HasErrors == false && _ds != null)
                {
                    DataSet _barCodeDS = MakePrintBarCode(_printHistoryDS);
                    if (_barCodeDS != null)
                    {
                        try
                        {
                            ToPrintBarCode(_barCodeDS);
                            _printHistoryDS.Tables[0].Clear();
                            btnClear_Click(null, null);
                            MessageBox.Show("���ͳɹ���");

                        }
                        catch (Exception _ex)
                        {
                            string _err = "";
                            if (_ex.Message.Length > 500)
                                _err = _ex.Message.Substring(0, 500);
                            else
                                _err = _ex.Message;
                            MessageBox.Show("��ӡʧ�ܣ�ԭ��\n" + _err);
                        }

                    }
                    else
                    {
                        MessageBox.Show("�װ��ʽ�����ڣ�");
                    }
                }
                else
                {
                    _printHistoryDS.Merge(_ds);
                    MessageBox.Show("����ʧ��");
                }
            }
            else
            {
                MessageBox.Show("û������Ҫ��ӡ");
            }
		}

		private void FTSBPrint_Load(object sender, EventArgs e)
		{
			_printHistoryDS = new DataSet();
			_printHistoryDS.Tables.Add(CreatePrintHistory().Copy());
			GetBar_role();
            cbFormat.SelectedIndex = 0;
            GetPrintBatNo();
            GetPrintSN();
		}

        #region ��ͷ

		private void btnDept_Click(object sender, EventArgs e)
		{
			//QueryWin _qw = new QueryWin();
			//_qw.PGM = "DEPT";//����
			//if (_qw.ShowDialog() == DialogResult.OK)
			//{
			//    txt4Dep.Text = _qw.NO_RT;
			//}
		}

		private void btn4Prdt_Click(object sender, EventArgs e)
		{
			QueryWin _qw = new QueryWin();
			_qw.PGM = "PRDT";//����
			if (_qw.ShowDialog() == DialogResult.OK)
			{
                txtPrdNo.Text = _qw.NO_RT;
                txtSpc.Text = _qw.OTHER_RTN;
                GetPrintSN();
			}
		}

        private void txtPrdNo_Leave(object sender, EventArgs e)
        {
            if (txtPrdNo.Text.Length > 0)
            {
                onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text,"");
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    txtSpc.Text = _ds.Tables[0].Rows[0]["SPC"].ToString();
                    GetPrintSN();
                }
                else
                {
                    MessageBox.Show(String.Format("Ʒ��[{0}]�����ڣ�", txtPrdNo.Text));
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    return;
                }
            }
            else
            {
                txtSpc.Text = "";
            }
        }

        private void txtCount_Leave(object sender, EventArgs e)
        {
            if (txtCount.Text != "")
            {
                StringBuilder _txtTemp = new StringBuilder();
                string _regExp = @"^\d{1,}$";
                Regex _reg = new Regex(_regExp);
                if (!_reg.IsMatch(txtCount.Text))
                {
                    txtCount.Text = "1";
                    MessageBox.Show("��Ʒ��������Ϊ��������");
                    return;
                }
            }
            else
            {
                txtCount.Text = "1";
            }
        }
		#endregion

		private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			dataGridView1.Columns["PK"].Visible = false;
			dataGridView1.Columns["PRD_NO"].Visible = false;
			dataGridView1.Columns["PRD_NO2"].Visible = false;
            dataGridView1.Columns["PRD_MARK"].Visible = false;
            dataGridView1.Columns["IDX1"].Visible = false;
			dataGridView1.Columns["SPC_NO"].Visible = false;
			dataGridView1.Columns["SPC_NAME"].Visible = false;
			dataGridView1.Columns["REM"].Visible = false;
            dataGridView1.Columns["COPY_COUNT"].Visible = false;
            dataGridView1.Columns["PAGE_COUNT"].Visible = false ;

			dataGridView1.Columns["BAR_NO"].HeaderText = "���к�";
			dataGridView1.Columns["SN"].HeaderText = "��ˮ��";
            dataGridView1.Columns["PRD_NO1"].HeaderText = "��Ʒ����";
            dataGridView1.Columns["PRD_NAME"].HeaderText = "��Ʒ����";
			dataGridView1.Columns["PRD_MARK1"].HeaderText = "����";
            dataGridView1.Columns["PRD_SPC"].HeaderText = "���";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "����";
			dataGridView1.Columns["DEP"].HeaderText = "����";

            dataGridView1.Columns["BAR_NO"].DisplayIndex = 0;
            dataGridView1.Columns["SN"].DisplayIndex = 1;
            dataGridView1.Columns["PRD_NO1"].DisplayIndex = 2;
            dataGridView1.Columns["PRD_NAME"].DisplayIndex = 3;
            dataGridView1.Columns["PRD_MARK1"].DisplayIndex = 4;
            dataGridView1.Columns["PRD_SPC"].DisplayIndex = 5;
            dataGridView1.Columns["IDX_NAME"].DisplayIndex = 6;
            dataGridView1.Columns["DEP"].DisplayIndex = 7;
		}

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            GetPrintSN();
        }

        private void txtPrdNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtCount.Focus();
        }
	}
}