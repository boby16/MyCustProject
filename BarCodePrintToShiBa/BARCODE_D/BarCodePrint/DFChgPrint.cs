using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace BarCodePrintTSB
{
	public partial class DFChgPrint : Form
	{
        private DataSet _printHistoryDS;

		#region Constructor
		public DFChgPrint()
		{
			InitializeComponent();
		}
		#endregion

		#region function

		#region 设置当前流水号
		private void GetPrintSN()
		{
            int _SNMax = 10;

			DataRow[] _findDrAry = _printHistoryDS.Tables[0].Select("BAR_NO='" + GetBarContent() + "'");
            if (_findDrAry.Length > 0)//如果历史库里有记录那么就到记录中查最大流水号
            {
                if (!String.IsNullOrEmpty(_findDrAry[_findDrAry.Length - 1]["SN"].ToString()))
                    _SNMax = Convert.ToInt32(_findDrAry[_findDrAry.Length - 1]["SN"]);
            }
            _SNMax += 1;
			txtSN.Text = _SNMax.ToString();
		}
		#endregion

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
            _barPrintTable.Columns.Add("LH", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("OS_BAR_NO", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("USR", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("SYS_DATE", System.Type.GetType("System.DateTime"));
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
            int _snno = 0;

            #region 货品规格
            onlineService.BarPrintServer _brs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
            _brs.UseDefaultCredentials = true;
            DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text.Trim(),"");
            #endregion

            for (int i = 0; i < Convert.ToInt32(txtCount.Text); i++)
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
                _snno = Convert.ToInt32(txtSN.Text);
                _snno = _snno + i;

                _dr["SN"] = _snno.ToString();
                _dr["SPC_NO"] = DBNull.Value;
                _dr["SPC_NAME"] = DBNull.Value;
                _dr["PAGE_COUNT"] = 1;
                _dr["REM"] = DBNull.Value;
                _dr["COPY_COUNT"] = 1;
                _dr["DEP"] = BarRole.DEP;
                _dr["OS_BAR_NO"] = txtOsBarNo.Text;
                _dr["USR"] = BarRole.USR_NO;
                _dr["SYS_DATE"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DataRow[] _drArr = printHistory.Select("BAR_NO='" + GetBarContent() + "' AND SN='" + _snno.ToString() + "'");
                if (_drArr.Length > 0)
                {
                    MessageBox.Show("条码已存在!");
                }
                else
                    printHistory.Rows.Add(_dr);
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
            _barCodeTable1.Columns.Add("LH", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("OS_BAR_NO", System.Type.GetType("System.String"));

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
                    _dr1["LH"] = printHistoryDS.Tables[0].Rows[i]["LH"];
                    _dr1["OS_BAR_NO"] = printHistoryDS.Tables[0].Rows[i]["OS_BAR_NO"];
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
            _result = _bs.PrintBarCode(barCodeDS, "3", cbFormat.SelectedIndex.ToString());
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
                return;
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
		/// <param name="controlName">给定控件</param>
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
			//取得替换字符
            string _replaceStr = BarRole.TrimChar;
			int _count = 0;
            if (keyName == "PRD_NO")
            {
                _count = BarRole.EPrdt - BarRole.SPrdt + 1;//货品长度
            }
            if (keyName == "BAT_NO")
            {
                _count = BarRole.ESn - BarRole.BSn - 1;//此处得到批号长度（流水号长度=批号+2位流水号）
            }

			for (int i = 0; i < _count - inputString.Length; i++)
			{
				_result.Append(_replaceStr);
			}
			return inputString + _result.ToString();//在货品或批号后面补空格
		}
		#endregion

		#endregion

        private void btnClear_Click(object sender, EventArgs e)
		{
            txtOsBarNo.Text = "";
            txtPrdNo.Text = "";
            txtSpc.Text = "";
			txtBatNo.Text = "";
			txtSN.Text = "";
            txtIdxNo.Text = "";
            txtIdx1.Text = "";
			txtCount.Text = "1";
            cbFormat.SelectedIndex = 0;
		}

        private void bnXpAddPrint_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtOsBarNo.Text.Trim()))
            {
                MessageBox.Show("原条码不能为空！");
                return;
            }
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
            else if (txtBatNo.Text.Length != 10)
            {
                MessageBox.Show("批号输入不正确！");
                return;
            }
            string _regExp = @"^1[1-9]$";
            Regex _reg = new Regex(_regExp);
            if (!_reg.IsMatch(txtSN.Text))
            {
                MessageBox.Show("流水号输入不正确！");
                txtSN.Text = "11";
                return;
            }
            try
            {
                AddToPrintHistory(_printHistoryDS.Tables[0]);
                dataGridView1.DataMember = "BARPRINT";
                dataGridView1.DataSource = _printHistoryDS;
                dataGridView1.Refresh();
                this.dataGridView1.AllowUserToAddRows = false;
                //GetPrintSN();
                btnClear_Click(null, null);
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
                //GetPrintSN();
            }
		}

		private void bnXpPrint_Click(object sender, EventArgs e)
		{
			bool _hasPrinted = false;
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
					//判断是否已经打印
					if (_brSer.IsPrinted(_bar_no, _sn, _page_count))
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
					MessageBox.Show(String.Format("序列号[{0}]已打印！", _BarPrinted));
					return;
				}
				{
					//保存打印信息
                    DataSet _ds = _brSer.SaveChangePrintData(BarRole.USR_NO, _printHistoryDS);
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
                        _printHistoryDS.Merge(_ds);
                        MessageBox.Show("操作失败");
                    }
				}
			}
			else
			{
				MessageBox.Show("没有数据要打印");
			}
		}

		private void DFChgPrint_Load(object sender, EventArgs e)
		{
			_printHistoryDS = new DataSet();
			_printHistoryDS.Tables.Add(CreatePrintHistory().Copy());
			GetBar_role();
			cbFormat.SelectedIndex = 0;
            txtSN.Text = "";
        }

        #region 表头

        private void txtOsBarNo_Leave(object sender, EventArgs e)
        {
            string _barCode = txtOsBarNo.Text.Trim();
            bool _hasErr = false;
            if (!String.IsNullOrEmpty(_barCode))
            {
                if (_barCode.Replace("#", "").Length != 24)
                {
                    _hasErr = true;
                    MessageBox.Show("输入的条码不是半成品条码!");
                }
                else
                {
                    try
                    {
                        onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                        _bps.UseDefaultCredentials = true;
                        DataSet _ds = _bps.GetBarChg(_barCode);
                        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow _dr = _ds.Tables[0].Rows[0];
                            if (_dr["ML_NO"].ToString() == "")
                            {
                                _hasErr = true;
                                MessageBox.Show("半成品条码未领料，不能换货!");
                            }
                            else
                            {
                                txtPrdNo.Text = _dr["MRP_NO"].ToString();
                                txtIdx1.Text = _dr["IDX_NAME"].ToString();
                                txtIdxNo.Text = _dr["IDX1"].ToString();
                                txtSpc.Text = _dr["SPC"].ToString();
                                txtBatNo.Text = _barCode.Substring(12, 10);
                                txtSN.Text = _barCode.Substring(24, 2);
                                cbFormat.SelectedIndex = 0;

                            }
                        }
                        else
                        {
                            _hasErr = true;
                            MessageBox.Show("条码不存在!");
                        }
                    }
                    catch (Exception _ex)
                    {
                        _hasErr = true;
                        string _err1 = "";
                        if (_ex.Message.Length > 500)
                            _err1 = _ex.Message.Substring(0, 500);
                        else
                            _err1 = _ex.Message;
                        MessageBox.Show("条码不存在! 原因：\n" + _err1);
                    }
                }
                if (_hasErr)
                {
                    btnClear_Click(null, null);
                }
            }
            else
            {
                btnClear_Click(null, null);
            }
        }

        private void txtOsBarNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == BarRole.EndChar)
            {
                this.txtOsBarNo_Leave(sender, e);
            }
        }

        private void txtSN_Leave(object sender, EventArgs e)
        {
            //string _regExp = @"^1[1-9]$";
            //Regex _reg = new Regex(_regExp);
            //if (!_reg.IsMatch(txtSN.Text))
            //{
            //    MessageBox.Show("流水号输入不正确！");
            //    txtSN.Text = "11";
            //    return;
            //}
        }

		private void btn4Prdt_Click(object sender, EventArgs e)
		{
			QueryWin _qw = new QueryWin();
			_qw.PGM = "PRDT";//表名
			if (_qw.ShowDialog() == DialogResult.OK)
			{
                txtPrdNo.Text = _qw.NO_RT;

                #region 货品资料
                onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text.Trim(),"");
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        txtSpc.Text = _ds.Tables[0].Rows[0]["SPC"].ToString();
                        txtIdxNo.Text = _ds.Tables[0].Rows[0]["IDX1"].ToString();
                        txtIdx1.Text = _ds.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    }
                }
                #endregion
			}
		}

        private void txtPrdNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtBatNo.Focus();
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
                    txtIdx1.Text = _ds.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    txtIdxNo.Text = _ds.Tables[0].Rows[0]["IDX1"].ToString();
                }
                else
                {
                    MessageBox.Show(String.Format("品号[{0}]不存在！", txtPrdNo.Text));
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    txtIdx1.Text = "";
                    txtIdxNo.Text = "";
                    return;
                }
            }
            else
            {
                txtSpc.Text = "";
                txtIdx1.Text = "";
                txtIdxNo.Text = "";
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

        private void txtBatNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtCount.Focus();
        }

        private void txtBatNo_Leave(object sender, EventArgs e)
        {
            //if (txtBatNo.Text != "")
            //{
            //    if (txtBatNo.Text.Length != 10)
            //    {
            //        MessageBox.Show("批号输入不正确！");
            //        txtBatNo.Text = "";
            //    }
            //}
        }

        private void txtCount_Leave(object sender, EventArgs e)
        {
            //if (txtCount.Text != "")
            //{
            //    StringBuilder _txtTemp = new StringBuilder();
            //    string _regExp = @"^\d{1,}$";
            //    Regex _reg = new Regex(_regExp);
            //    if (!_reg.IsMatch(txtCount.Text))
            //    {
            //        txtCount.Text = "1";
            //        MessageBox.Show("数量必须为正整数！");
            //        return;
            //    }
            //}
            //else
            //{
            //    txtCount.Text = "1";
            //}
        }

        #endregion

        #region DataGridView

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			dataGridView1.Columns["PK"].Visible = false;
			dataGridView1.Columns["PRD_NO"].Visible = false;
			dataGridView1.Columns["PRD_NO2"].Visible = false;
            dataGridView1.Columns["PRD_MARK"].Visible = false;
            dataGridView1.Columns["PRD_NAME"].Visible = false;
            dataGridView1.Columns["IDX1"].Visible = false;
			dataGridView1.Columns["SPC_NO"].Visible = false;
			dataGridView1.Columns["SPC_NAME"].Visible = false;
			dataGridView1.Columns["REM"].Visible = false;
            dataGridView1.Columns["COPY_COUNT"].Visible = false;
            dataGridView1.Columns["PAGE_COUNT"].Visible = false;
            dataGridView1.Columns["LH"].Visible = false;
            dataGridView1.Columns["DEP"].Visible = false;
            dataGridView1.Columns["USR"].Visible = false;
            dataGridView1.Columns["SYS_DATE"].Visible = false;

			dataGridView1.Columns["BAR_NO"].HeaderText = "序列号";
			dataGridView1.Columns["SN"].HeaderText = "流水号";
			dataGridView1.Columns["PRD_MARK1"].HeaderText = "批号";
            dataGridView1.Columns["PRD_NO1"].HeaderText = "品号";
            dataGridView1.Columns["PRD_SPC"].HeaderText = "规格";
            dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            dataGridView1.Columns["OS_BAR_NO"].HeaderText = "原序列号";

            dataGridView1.Columns["BAR_NO"].DisplayIndex = 0;
            dataGridView1.Columns["SN"].DisplayIndex = 1;
            dataGridView1.Columns["PRD_MARK1"].DisplayIndex = 2;
            dataGridView1.Columns["PRD_NO1"].DisplayIndex = 3;
            dataGridView1.Columns["PRD_SPC"].DisplayIndex = 4;
            dataGridView1.Columns["IDX_NAME"].DisplayIndex = 5;
            dataGridView1.Columns["OS_BAR_NO"].DisplayIndex = 6;
		}

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //GetPrintSN();
        }

        #endregion
    }
}