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
	public partial class FTRepPrint : Form
	{
		private DataSet _printHistoryDS;

		#region Constructor
		public FTRepPrint()
		{
			InitializeComponent();
		}
		#endregion

		#region function

		#region 创建打印历史表
		/// <summary>
		/// 创建打印历史表
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

		#region 增加数据到历史表
		/// <summary>
		/// 增加数据到历史表
		/// </summary>
		/// <param name="printHistory"></param>
		private void AddToPrintHistory(DataTable printHistory)
        {
            #region 货品规格
            onlineService.BarPrintServer _brs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
            _brs.UseDefaultCredentials = true;
            DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text.Trim(),"");
            #endregion

            int _SNLength = 0;
			int _snB = Convert.ToInt32(txtSNB.Text);
			int _snE = Convert.ToInt32(txtSNE.Text);
			for (int i = _snB; i <= _snE; i++)
			{
                for (int k = 0; k < Convert.ToInt32(txtCount.Text); k++)
                {
                    DataRow _dr = printHistory.NewRow();
                    _dr["BAR_NO"] = GetBarContent();
                    _dr["PRD_NO"] = GetBarContent(txtPrdNo);//补空格
                    _dr["PRD_NO1"] = txtPrdNo.Text;
                    _dr["PRD_NO2"] = GetBarContent(txtPrdNo);//补空格
                    _dr["PRD_MARK"] = GetBarContent(txtBatNo);//补空格
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

                    _dr["SPC_NO"] = DBNull.Value;
                    _dr["SPC_NAME"] = DBNull.Value;
                    _dr["PAGE_COUNT"] = 1;
                    _dr["REM"] = DBNull.Value;
                    _dr["COPY_COUNT"] = 1;
                    _dr["DEP"] = BarRole.DEP;

                    StringBuilder _sn = new StringBuilder();
                    _SNLength = i.ToString().Length;
                    for (int j = 0; j < txtSNB.Text.Length - _SNLength; j++)
                    {
                        _sn.Insert(0, "0");
                    }
                    _dr["SN"] = _sn.ToString() + i.ToString();

                    printHistory.Rows.Add(_dr);
                }
			}
			printHistory.AcceptChanges();
		}

		#endregion

		#region 转入欲打印的序列号资料集
		/// <summary>
		/// 转入欲打印的序列号资料集
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

                    _dr1["PRD_NAME"] = printHistoryDS.Tables[0].Rows[i]["PRD_NAME"].ToString();
                    _dr1["PRD_SPC"] = printHistoryDS.Tables[0].Rows[i]["PRD_SPC"].ToString();
                    _dr1["IDX1"] = printHistoryDS.Tables[0].Rows[i]["IDX1"].ToString();
                    _dr1["IDX_NAME"] = printHistoryDS.Tables[0].Rows[i]["IDX_NAME"].ToString();
					
					_dr1["SPC_NO"] = printHistoryDS.Tables[0].Rows[i]["SPC_NO"];
					_dr1["SPC_NAME"] = printHistoryDS.Tables[0].Rows[i]["SPC_NAME"];
					_dr1["PRD_SPC"] = printHistoryDS.Tables[0].Rows[i]["PRD_SPC"];
                    _dr1["DEP"] = printHistoryDS.Tables[0].Rows[i]["DEP"];
                    //条码个数（本系统没有用到，默认为1）（例如序列号流水为1，如果PAGE_COUNT为5，传入打印时，自动打到流水号为第5个条码）
                    //本系统没有用到，所以此处的设置不会影响系统
                    if (!(printHistoryDS.Tables[0].Rows[i]["PAGE_COUNT"] is System.DBNull) && !String.IsNullOrEmpty(printHistoryDS.Tables[0].Rows[i]["PAGE_COUNT"].ToString()))
                        _dr1["PAGE_COUNT"] = printHistoryDS.Tables[0].Rows[i]["PAGE_COUNT"];
                    else
                        _dr1["PAGE_COUNT"] = 1;
                    //打印张数（本系统没有用到，默认为1）（例如条码为A001，如果COPY_COUNT为5，则打印5张A001的条码标签）
                    //本系统没有用到，所以此处的设置不会影响系统
                    if (!(printHistoryDS.Tables[0].Rows[i]["COPY_COUNT"] is System.DBNull) && !String.IsNullOrEmpty(printHistoryDS.Tables[0].Rows[i]["COPY_COUNT"].ToString()))
                        _dr1["COPY_COUNT"] = printHistoryDS.Tables[0].Rows[i]["COPY_COUNT"];
                    else
                        _dr1["COPY_COUNT"] = 1;
					_barCodeTable1.Rows.Add(_dr1);
				}
			}
			_barCodeDS.Tables.Add(_barCodeTable1);
			return _barCodeDS;
		}
		/// <summary>
		/// 转入欲打印的序列号资料集
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

		#region 得到bar_role
		/// <summary>
		/// 得到bar_role
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
				MessageBox.Show("没有设定条码的编码原则！");
			}
		}
		#endregion

		#region 取得序列号内容
		/// <summary>
		/// 取得序列号内容
		/// </summary>
		/// <returns></returns>
		private string GetBarContent()
		{
			StringBuilder _bar = new StringBuilder();
			//货品代号
			_bar.Append(ReplaceSpace("PRD_NO", txtPrdNo.Text));

			//批号
			_bar.Append(ReplaceSpace("BAT_NO", txtBatNo.Text));

			return _bar.ToString();
		}

		#endregion

		#region 取得给定名称控件的内容
		/// <summary>
		/// 取得给定名称控件的内容
		/// </summary>
		/// <param name="controlName">控件名称</param>
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

		#region 根据bar_role来补空格(不含流水号)
		/// <summary>
		/// 根据bar_role来补空格(不含流水号)
		/// </summary>
		/// <param name="keyName"></param>
		/// <param name="inputString"></param>
		/// <returns></returns>
		public string ReplaceSpace(string keyName, string inputString)
		{
			StringBuilder _result = new StringBuilder();
			string _replaceStr = "";
			//取得替换字符
			_replaceStr = BarRole.TrimChar;
			int _count = 0;
			//取得设置的长度
            if (keyName == "PRD_NO")
            {
                _count = BarRole.EPrdt - BarRole.SPrdt + 1;//货品长度
            }
            if (keyName == "BAT_NO")
            {
                if (txtSNB.Text.Length == 4)//对分后的成品
                    _count = BarRole.ESn - BarRole.BSn - 3;
                else//板材直接分条
                    _count = BarRole.ESn - BarRole.BSn - 2;//此处得到批号长度（流水号长度=批号+3位流水号）

            }

			for (int i = 0; i < _count - inputString.Length; i++)
			{
				_result.Append(_replaceStr);
			}
            return inputString + _result.ToString();//在货品或批号后面补空格
		}
		#endregion

		#endregion

		private void cbXpNew_Click(object sender, EventArgs e)
		{
			txtPrdNo.Text = "";
			txtBatNo.Text = "";
			txtSNB.Text = "";
			txtSNE.Text = "";
			txtCount.Text = "1";
		}

		private void bnXpAddPrint_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(txtPrdNo.Text.Trim()))
			{
				MessageBox.Show("品号不能为空！");
				return;
			}
			if (String.IsNullOrEmpty(this.txtBatNo.Text.Trim()))
			{
				MessageBox.Show("批号不能为空！");
				return;
			}
			if (String.IsNullOrEmpty(this.txtSNB.Text.Trim()) || String.IsNullOrEmpty(this.txtSNE.Text.Trim()))
			{
				MessageBox.Show("起止流水号不能为空！");
				return;
			}
			try
			{

				AddToPrintHistory(_printHistoryDS.Tables[0]);
				this.dataGridView1.DataMember = "BARPRINT";
				dataGridView1.DataSource = _printHistoryDS;
				dataGridView1.Refresh();
				this.dataGridView1.AllowUserToAddRows = false;
			}
			catch (Exception _ex)
            {
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("加入打印失败，原因：\n" + _err);
			}
		}

		private void bnXpClear_Click(object sender, EventArgs e)
		{
            DialogResult _result;
            _result = MessageBox.Show("确认清除所有待打印的记录？", "提示", MessageBoxButtons.YesNo);
            if (_result == DialogResult.Yes)
            {
                _printHistoryDS.Tables[0].Clear();
            }
		}

		private void bnXpPrint_Click(object sender, EventArgs e)
		{
			bool _hasPrinted = true;
			DataRow[] _selDr = _printHistoryDS.Tables[0].Select();
			if (_selDr.Length > 0)
			{
				string _BarPrinted = "";
				onlineService.BarPrintServer _brSer = new global::BarCodePrintTSB.onlineService.BarPrintServer();
				_brSer.UseDefaultCredentials = true;
				for (int i = 0; i < _selDr.Length; i++)
				{
					string _bar_no = _selDr[i]["BAR_NO"].ToString();
					string _sn = _selDr[i]["SN"].ToString();
					int _page_count = 0;
					if (!(_selDr[i]["PAGE_COUNT"] is System.DBNull) && (!String.IsNullOrEmpty(_selDr[i]["PAGE_COUNT"].ToString())))
					{
						_page_count = Convert.ToInt32(_selDr[i]["PAGE_COUNT"]);
					}
					//判断是否从未打印
					if (!_brSer.IsPrinted(_bar_no, _sn, _page_count))
					{
						if (_BarPrinted.Length > 0)
						{
							_BarPrinted += ",";
						}
						_hasPrinted = false;
						_BarPrinted += _bar_no + _sn;
					}
				}
				if (!_hasPrinted)
				{
					//从未打印过的条码
					MessageBox.Show(String.Format("序列号[{0}]不存在！", _BarPrinted));
					return;
				}
				DataSet _barCodeDS = MakePrintBarCode(_printHistoryDS);
				if (_barCodeDS != null)
				{
					try
					{
                        ToPrintBarCode(_barCodeDS);
                        _printHistoryDS.Tables[0].Clear();
						MessageBox.Show("发送成功！");

					}
					catch (Exception _ex)
                    {
                        string _err = "";
                        if (_ex.Message.Length > 500)
                            _err = _ex.Message.Substring(0, 500);
                        else
                            _err = _ex.Message;
                        MessageBox.Show("打印失败，原因：\n" + _err);
					}

				}
				else
				{
					MessageBox.Show("套版格式不存在！");
				}
			}
			else
			{
				MessageBox.Show("没有数据要打印");
			}
		}

		private void FTRepPrint_Load(object sender, EventArgs e)
		{
			_printHistoryDS = new DataSet();
			_printHistoryDS.Tables.Add(CreatePrintHistory().Copy());
			GetBar_role();
			cbFormat.SelectedIndex = 0;
			cbPrdType.SelectedIndex = 0;
		}

		private void btn4Prdt_Click(object sender, EventArgs e)
		{
			QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            _qw.SQLWhere = " AND KND='2' ";
			if (_qw.ShowDialog() == DialogResult.OK)
			{
				txtPrdNo.Text = _qw.NO_RT;
			}
		}

		private void btn4BatNo_Click(object sender, EventArgs e)
		{
			QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//表名
            _qw.SQLWhere = " AND LEN(BAT_NO)=10 ";
            if (txtPrdNo.Text != "")
                _qw.SQLWhere += " AND BAT_NO IN (SELECT PRD_MARK FROM BAR_SQNO WHERE PRD_NO='" + txtPrdNo.Text + "') ";
            else
                _qw.SQLWhere += " AND BAT_NO IN (SELECT PRD_MARK FROM BAR_SQNO) ";
			if (_qw.ShowDialog() == DialogResult.OK)
			{
				txtBatNo.Text = _qw.NO_RT;
			}
		}

		private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			dataGridView1.Columns["PK"].Visible = false;
			dataGridView1.Columns["PRD_NO"].Visible = false;
			dataGridView1.Columns["PRD_NO2"].Visible = false;
			dataGridView1.Columns["PRD_MARK"].Visible = false;
			dataGridView1.Columns["SPC_NO"].Visible = false;
			dataGridView1.Columns["SPC_NAME"].Visible = false;
			dataGridView1.Columns["REM"].Visible = false;
			dataGridView1.Columns["COPY_COUNT"].Visible = false;

			dataGridView1.Columns["BAR_NO"].HeaderText = "序列号";
			dataGridView1.Columns["SN"].HeaderText = "流水号";
            dataGridView1.Columns["PRD_NO1"].HeaderText = "品号";
            dataGridView1.Columns["PRD_NAME"].HeaderText = "品名";
			dataGridView1.Columns["PRD_MARK1"].HeaderText = "批号";
			dataGridView1.Columns["PRD_SPC"].HeaderText = "规格";
			dataGridView1.Columns["PAGE_COUNT"].Visible = false;
            dataGridView1.Columns["DEP"].HeaderText = "部门";
            dataGridView1.Columns["IDX1"].Visible = false;
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
		}

		private void txtSNB_Leave(object sender, EventArgs e)
		{
            if (txtSNB.Text != "")
            {
                string _regExp = @"^\d{1,}$";
                Regex _reg = new Regex(_regExp);
                if (!_reg.IsMatch(txtSNB.Text))
                {
                    txtSNB.Text = "";
                    MessageBox.Show("起止流水号必须为数字！");
                    return;
                }

                if (txtSNE.Text == "")
                {
                    txtSNE.Text = txtSNB.Text;
                }
            }
		}

		private void txtSNE_Leave(object sender, EventArgs e)
		{
			if (txtSNE.Text != "")
			{
                string _regExp = @"^\d{1,}$";
                Regex _reg = new Regex(_regExp);
                if (!_reg.IsMatch(txtSNE.Text))
                {
                    txtSNE.Text = "";
                    MessageBox.Show("起止流水号必须为数字！");
                    return;
                }

				if (txtSNB.Text == "")
				{
					txtSNB.Text = txtSNE.Text;
				}
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
                    MessageBox.Show("张数必须为正整数！");
                    return;
                }
			}
			else 
			{
				txtCount.Text = "1";
			}
		}

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBatNo.Text = "";
            txtCount.Text = "1";
            txtPrdNo.Text = "";
            txtSNB.Text = "";
            txtSNE.Text = "";
        }
	}
}