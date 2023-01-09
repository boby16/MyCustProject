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
	public partial class WXCodeEdit : Form
	{
		private DataSet _printHistoryDS;
        private DataTable _compInfo = new DataTable();
        private DataSet _boxBarDs = new DataSet();
        private bool _canUpdate = true;
        private string _barNO = "";//取得当前日期，格式：yymmdd。加上流水号作为打印箱条码信息

		#region Constructor
		public WXCodeEdit()
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
			_barPrintTable.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("SPC_NO", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("SPC_NAME", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("REM", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("PAGE_COUNT", System.Type.GetType("System.String"));
			_barPrintTable.Columns.Add("COPY_COUNT", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("DEP", System.Type.GetType("System.String"));

            _barPrintTable.Columns.Add("COMP_NAME", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("COMP_ENAME", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("COMP_ADR", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("COMP_TEL", System.Type.GetType("System.String"));
            _barPrintTable.Columns.Add("COMP_FAX", System.Type.GetType("System.String"));

            _barPrintTable.Columns.Add("BOX_PRD", System.Type.GetType("System.String"));//外箱条码：品号
            _barPrintTable.Columns.Add("BOX_SPC", System.Type.GetType("System.String"));//外箱条码：规格
            _barPrintTable.Columns.Add("BOX_BAT", System.Type.GetType("System.String"));//外箱条码：批号
            _barPrintTable.Columns.Add("BOX_LH", System.Type.GetType("System.String"));//外箱条码：打印产品编号
            _barPrintTable.Columns.Add("BOX_IDX1", System.Type.GetType("System.String"));//外箱条码：中类代号
            _barPrintTable.Columns.Add("BOX_IDX_NAME", System.Type.GetType("System.String"));//外箱条码：中类名称
            _barPrintTable.Columns.Add("BOX_FORMAT", System.Type.GetType("System.String"));//外箱条码：打印版式
            _barPrintTable.Columns.Add("BOX_QTY", System.Type.GetType("System.String"));//外箱条码：卷数

            //富士康标签信息
            _barPrintTable.Columns.Add("FOX_SUP", System.Type.GetType("System.String"));//供应商
            _barPrintTable.Columns.Add("FOX_CZ", System.Type.GetType("System.String"));//材质
            _barPrintTable.Columns.Add("FOX_PRD_NAME", System.Type.GetType("System.String"));//品名
            _barPrintTable.Columns.Add("FOX_BC", System.Type.GetType("System.String"));//版次
            _barPrintTable.Columns.Add("FOX_LH", System.Type.GetType("System.String"));//Foxconn料号
            _barPrintTable.Columns.Add("FOX_BAT_MANU", System.Type.GetType("System.String"));//生产批号
            _barPrintTable.Columns.Add("FOX_BAT_MAT", System.Type.GetType("System.String"));//原料批号
            _barPrintTable.Columns.Add("FOX_WEIGHT", System.Type.GetType("System.String"));//重量
            _barPrintTable.Columns.Add("FOX_XINGHAO", System.Type.GetType("System.String"));//型号

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
            onlineService.BarPrintServer _bps = new global::BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            string _format = "BASIC";
            if (cbFormat.SelectedIndex == 1)
                _format = "SAMPLE";
            if (cbFormat.SelectedIndex == 2)
                _format = "SMALL";
            if (cbFormat.SelectedIndex == 3)
                _format = "OTHER";
            if (cbFormat.SelectedIndex == 4)
                _format = "FOXCONN";
            if (cbFormat.SelectedIndex == 5)
                _format = "SBB";
            if (cbFormat.SelectedIndex == 6)
                _format = "HQ";
            DataSet _ds = _bps.GetPrdtFull(txtPrdNo.Text.Trim(),_format); //货品资料

            int SNLength = 0;
            int _snB = Convert.ToInt32(txtSNB.Text);
            int _snE = Convert.ToInt32(txtSNE.Text);
            string _initNo = "";
            string _barLst = "";
            for (int i = _snB; i <= _snE; i++)
            {
                StringBuilder _sn = new StringBuilder();
                SNLength = i.ToString().Length;
                for (int j = 0; j < 4 - SNLength; j++)
                {
                    _sn.Insert(0, "0");
                }
                _initNo = _sn.ToString() + i.ToString();

                if (_barLst != "")
                    _barLst += ",";
                _barLst += "'" + _barNO + _initNo + "'";

                DataRow _dr = printHistory.NewRow();
                _dr["BAR_NO"] = _barNO;
                _dr["PRD_NO"] = "WX";
                _dr["PRD_MARK"] = _barNO;
                _dr["SN"] = _initNo;

                _dr["SPC_NO"] = DBNull.Value;
                _dr["PAGE_COUNT"] = 1;
                _dr["REM"] = DBNull.Value;
                _dr["COPY_COUNT"] = 1;
                _dr["DEP"] = BarRole.DEP;

                if (_compInfo != null && _compInfo.Rows.Count > 0)
                {
                    //取得营业人资料
                    _dr["COMP_NAME"] = Strings.StrConv(_compInfo.Rows[0]["NAME"].ToString(), VbStrConv.SimplifiedChinese, 2);
                    _dr["COMP_ENAME"] = _compInfo.Rows[0]["NAME_ENG"];
                    _dr["COMP_ADR"] = Strings.StrConv(_compInfo.Rows[0]["CMP_ADR"].ToString(), VbStrConv.SimplifiedChinese, 2);
                    _dr["COMP_TEL"] = _compInfo.Rows[0]["TEL1"];
                    _dr["COMP_FAX"] = _compInfo.Rows[0]["TEL3"];
                }
                _boxBarDs = _bps.GetBoxBar1(" AND ISNULL(A.STOP_ID,'F')<>'T' AND A.BOX_NO ='" + _barNO + _initNo + "'");//取箱条码
                if (_boxBarDs != null && _boxBarDs.Tables.Count > 0 && _boxBarDs.Tables[0].Rows.Count > 0)
                {
                    if (_boxBarDs.Tables[0].Rows[0]["CONTENT"].ToString() != "")
                    {
                        MessageBox.Show(String.Format("箱条码[{0}]已使用，不能修改！", _boxBarDs.Tables[0].Rows[0]["BOX_NO"]));
                        _printHistoryDS.Clear();
                        break;
                    }

                    #region 此数据用于打印序列号

                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        _dr["BOX_IDX1"] = _ds.Tables[0].Rows[0]["IDX1"].ToString();
                    }
                    if (cbFormat.SelectedIndex == 3 || cbFormat.SelectedIndex == 6)//特殊版和瀚荃版，打印特殊名称
                        _dr["BOX_IDX_NAME"] = txtPrintName.Text;
                    else
                    {
                        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            _dr["BOX_IDX_NAME"] = _ds.Tables[0].Rows[0]["IDX_UP"].ToString();
                        }
                    }

                    _dr["BOX_PRD"] = txtPrdNo.Text;//品名
                    //当列印版式为--特殊版-产品编号时，用SPC_NO字段存储P/O号，就不用再新增字段了
                    if (cbFormat.SelectedIndex == 5)
                    {
                        _dr["BOX_LH"] = txtPrdBH.Text;//产品编号
                        _dr["SPC_NAME"] = txtPO.Text;
                    }
                    else
                    {
                        _dr["BOX_LH"] = "";
                        _dr["SPC_NAME"] = DBNull.Value;
                    }
                    _dr["BOX_SPC"] = txtSpc.Text;//规格
                    _dr["BOX_BAT"] = txtBatNo.Text;//批号
                    _dr["BOX_FORMAT"] = cbFormat.SelectedIndex + 1;//1:标准版；2、简约版；3、列印编号；4、特殊版；5、富士康；6、特殊版-产品编号
                    _dr["BOX_QTY"] = txtPrdQty.Text; //箱量

                    //富士康标签信息
                    _dr["FOX_SUP"] = txtSup.Text;//供应商
                    _dr["FOX_CZ"] = txtCZ.Text;//材质
                    _dr["FOX_PRD_NAME"] = txtPrdName.Text;//品名
                    _dr["FOX_BC"] = txtBC.Text;//版次
                    _dr["FOX_LH"] = txtFoxLH.Text;//Foxconn料号
                    _dr["FOX_BAT_MANU"] = txtBatManu.Text;//生产批号
                    _dr["FOX_BAT_MAT"] = txtBatMat.Text;//原料批号
                    _dr["FOX_WEIGHT"] = txtWeight.Text; //重量
                    _dr["FOX_XINGHAO"] = txtXingHao.Text;//型号

                    #endregion
                }
                else
                {
                    MessageBox.Show("箱条码不存在！");
                }
                printHistory.Rows.Add(_dr);
            }

            #region 此数据用于更新箱条码信息
            _boxBarDs = _bps.GetBoxBar1(" AND ISNULL(A.STOP_ID,'F')<>'T' AND A.BOX_NO in (" + _barLst + ")");//取箱条码
            if (_boxBarDs != null && _boxBarDs.Tables.Count > 0 && _boxBarDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow _drBox in _boxBarDs.Tables[0].Rows)
                {
                    _drBox["PRD_NO"] = txtPrdNo.Text;
                    _drBox["SPC"] = txtSpc.Text;
                    _drBox["BAT_NO"] = txtBatNo.Text;
                    _drBox["QTY"] = txtPrdQty.Text;
                    _drBox["FORMAT"] = cbFormat.SelectedIndex + 1;//1:标准版；2、简约版；3、列印编号；4、特殊版；5、富士康；6、塑柏碧；7、瀚荃

                    _drBox["IDX1"] = _ds.Tables[0].Rows[0]["IDX1"].ToString();

                    if (cbFormat.SelectedIndex == 3 || cbFormat.SelectedIndex == 6)//特殊版和瀚荃版，打印特殊名称
                        _drBox["IDX_NAME"] = txtPrintName.Text;
                    else
                    {
                        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            _drBox["IDX_NAME"] = _ds.Tables[0].Rows[0]["IDX_UP"].ToString();
                        }
                    }
                    if (cbFormat.SelectedIndex == 5)
                        _drBox["LH"] = txtPrdBH.Text;//产品编号
                    else
                        _drBox["LH"] = "";
                }
            }
            #endregion

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
			_barCodeTable1.Columns.Add("SPC_NO", System.Type.GetType("System.String"));
			_barCodeTable1.Columns["SPC_NO"].MaxLength = 10;
			_barCodeTable1.Columns.Add("SPC_NAME", System.Type.GetType("System.String"));
			_barCodeTable1.Columns["SPC_NAME"].MaxLength = 50;
            _barCodeTable1.Columns.Add("INIT_NO", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("PAGE_COUNT", System.Type.GetType("System.Int32"));
            _barCodeTable1.Columns.Add("COPY_COUNT", System.Type.GetType("System.Int32"));
			_barCodeTable1.Columns.Add("DEP", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("DEP_NAME", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("PX", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("PY", System.Type.GetType("System.String"));

            _barCodeTable1.Columns.Add("COMP_NAME", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("COMP_ENAME", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("COMP_ADR", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("COMP_TEL", System.Type.GetType("System.String"));
            _barCodeTable1.Columns.Add("COMP_FAX", System.Type.GetType("System.String"));

            _barCodeTable1.Columns.Add("BOX_PRD", System.Type.GetType("System.String"));//外箱条码：品名
            _barCodeTable1.Columns.Add("BOX_SPC", System.Type.GetType("System.String"));//外箱条码：规格
            _barCodeTable1.Columns.Add("BOX_BAT", System.Type.GetType("System.String"));//外箱条码：批号
            _barCodeTable1.Columns.Add("BOX_QTY", System.Type.GetType("System.String"));//外箱条码：卷数
            _barCodeTable1.Columns.Add("BOX_LH", System.Type.GetType("System.String"));//外箱条码：产品编号
            _barCodeTable1.Columns.Add("BOX_IDX1", System.Type.GetType("System.String"));//外箱条码：中类代号
            _barCodeTable1.Columns.Add("BOX_IDX_NAME", System.Type.GetType("System.String"));//外箱条码：中类名称

            //富士康标签信息
            _barCodeTable1.Columns.Add("FOX_SUP", System.Type.GetType("System.String"));//供应商
            _barCodeTable1.Columns.Add("FOX_CZ", System.Type.GetType("System.String"));//材质
            _barCodeTable1.Columns.Add("FOX_PRD_NAME", System.Type.GetType("System.String"));//品名
            _barCodeTable1.Columns.Add("FOX_BC", System.Type.GetType("System.String"));//版次
            _barCodeTable1.Columns.Add("FOX_LH", System.Type.GetType("System.String"));//Foxconn料号
            _barCodeTable1.Columns.Add("FOX_BAT_MANU", System.Type.GetType("System.String"));//生产批号
            _barCodeTable1.Columns.Add("FOX_BAT_MAT", System.Type.GetType("System.String"));//原料批号
            _barCodeTable1.Columns.Add("FOX_WEIGHT", System.Type.GetType("System.String"));//重量
            _barCodeTable1.Columns.Add("FOX_XINGHAO", System.Type.GetType("System.String"));//型号

			for (int i = 0; i < printHistoryDS.Tables[0].Rows.Count; i++)
			{
                if (printHistoryDS.Tables[0].Rows[i].RowState != DataRowState.Deleted)
                {
                    //-------------BARCODE1------------
                    DataRow _dr1 = _barCodeTable1.NewRow();
                    _dr1["BARCODE"] = printHistoryDS.Tables[0].Rows[i]["BAR_NO"];
                    _dr1["PRD_NO"] = printHistoryDS.Tables[0].Rows[i]["PRD_NO"];
                    _dr1["PRD_MARK"] = printHistoryDS.Tables[0].Rows[i]["PRD_MARK"];
                    _dr1["INIT_NO"] = printHistoryDS.Tables[0].Rows[i]["SN"];

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

                    _dr1["COMP_NAME"] = printHistoryDS.Tables[0].Rows[i]["COMP_NAME"];
                    _dr1["COMP_ENAME"] = printHistoryDS.Tables[0].Rows[i]["COMP_ENAME"];
                    _dr1["COMP_ADR"] = printHistoryDS.Tables[0].Rows[i]["COMP_ADR"];
                    _dr1["COMP_TEL"] = printHistoryDS.Tables[0].Rows[i]["COMP_TEL"];
                    _dr1["COMP_FAX"] = printHistoryDS.Tables[0].Rows[i]["COMP_FAX"];

                    _dr1["BOX_PRD"] = printHistoryDS.Tables[0].Rows[i]["BOX_PRD"];
                    _dr1["BOX_SPC"] = printHistoryDS.Tables[0].Rows[i]["BOX_SPC"];
                    _dr1["BOX_BAT"] = printHistoryDS.Tables[0].Rows[i]["BOX_BAT"];
                    _dr1["BOX_QTY"] = printHistoryDS.Tables[0].Rows[i]["BOX_QTY"];
                    _dr1["BOX_LH"] = printHistoryDS.Tables[0].Rows[i]["BOX_LH"];
                    _dr1["BOX_IDX1"] = printHistoryDS.Tables[0].Rows[i]["BOX_IDX1"];
                    _dr1["BOX_IDX_NAME"] = printHistoryDS.Tables[0].Rows[i]["BOX_IDX_NAME"];

                    //富士康标签信息
                    _dr1["FOX_SUP"] = printHistoryDS.Tables[0].Rows[i]["FOX_SUP"];
                    _dr1["FOX_CZ"] = printHistoryDS.Tables[0].Rows[i]["FOX_CZ"];
                    _dr1["FOX_PRD_NAME"] = printHistoryDS.Tables[0].Rows[i]["FOX_PRD_NAME"];
                    _dr1["FOX_BC"] = printHistoryDS.Tables[0].Rows[i]["FOX_BC"];
                    _dr1["FOX_LH"] = printHistoryDS.Tables[0].Rows[i]["FOX_LH"];
                    _dr1["FOX_BAT_MANU"] = printHistoryDS.Tables[0].Rows[i]["FOX_BAT_MANU"];
                    _dr1["FOX_BAT_MAT"] = printHistoryDS.Tables[0].Rows[i]["FOX_BAT_MAT"];
                    _dr1["FOX_WEIGHT"] = printHistoryDS.Tables[0].Rows[i]["FOX_WEIGHT"];
                    _dr1["FOX_XINGHAO"] = printHistoryDS.Tables[0].Rows[i]["FOX_XINGHAO"];

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
            string _format = "BASIC";
            if (cbFormat.SelectedIndex == 1)
                _format = "SAMPLE";
            if (cbFormat.SelectedIndex == 2)
                _format = "SMALL";
            if (cbFormat.SelectedIndex == 3)
                _format = "OTHER";
            if (cbFormat.SelectedIndex == 4)
                _format = "FOXCONN";
            if (cbFormat.SelectedIndex == 5)
                _format = "SBB";
            if (cbFormat.SelectedIndex == 6)
                _format = "HQ";
            _result = _bs.PrintBarCode(barCodeDS, "2", _format);
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

		#endregion

		private void bnXpAddPrint_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtSNB.Text.Trim()) || String.IsNullOrEmpty(this.txtSNE.Text.Trim()))
            {
                MessageBox.Show("起止流水号不能为空！");
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
            if (cbFormat.SelectedIndex == 3 && txtPrintName.Text == "")
            {
                MessageBox.Show("请选择打印品名！");
                return;
            }
            if (cbFormat.SelectedIndex == 5 && txtPrdBH.Text == "")
            {
                MessageBox.Show("产品编号不能为空！");
                return;
            }
            _canUpdate = true;
            txtBatNo_Leave(null, null);//判断批号是否存在
			try
			{
                if (_canUpdate)
                {
                    AddToPrintHistory(_printHistoryDS.Tables[0]);
                    this.dataGridView1.DataMember = "BARPRINT";
                    dataGridView1.DataSource = _printHistoryDS;
                    dataGridView1.Refresh();
                    this.dataGridView1.AllowUserToAddRows = false;
                }
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
            DataRow[] _selDr = _printHistoryDS.Tables[0].Select();
            if (_selDr.Length > 0)
            {
                onlineService.BarPrintServer _brSer = new global::BarCodePrintTSB.onlineService.BarPrintServer();
                _brSer.UseDefaultCredentials = true;

                DataSet _ds = _brSer.UpdateBoxPrintData(_boxBarDs);
                if (_ds.HasErrors == false && _ds != null)
                {
                    DataSet _barCodeDS = MakePrintBarCode(_printHistoryDS);
                    if (_barCodeDS != null)
                    {
                        try
                        {
                            ToPrintBarCode(_barCodeDS);
                            _printHistoryDS.Tables[0].Clear();
                            ClearData();
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
            else
            {
                MessageBox.Show("没有数据要打印");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult _result;
            _result = MessageBox.Show("确认清除左边待打印的数据？", "提示", MessageBoxButtons.YesNo);
            if (_result == DialogResult.Yes)
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            txtSNB.Text = "";
            txtSNE.Text = "";
            txtPrdNo.Text = "";
            txtSpc.Text = "";
            txtIdx1.Text = "";
            txtIdxNo.Text = "";
            txtBatNo.Text = "";
            txtPrdQty.Text = "1";
            txtPrintName.Text = "";
            txtPrdBH.Text = "";
            txtPO.Text = "";

            txtCZ.Text = "";
            txtBC.Text = "";
            txtFoxLH.Text = "";
            txtBatManu.Text = "";
            txtBatMat.Text = "";
            txtWeight.Text = "";
            txtWeightPer.Text = "0";
            txtXingHao.Text = "";
        }

		private void WXCodeEdit_Load(object sender, EventArgs e)
        {
            #region 取当前日期
            string _yy = System.DateTime.Now.Year.ToString();//年
            string _mm = System.DateTime.Now.Month.ToString();//月
            if (_mm.Length == 1)
            {
                _mm = "0" + _mm;
            }
            string _dd = System.DateTime.Now.Day.ToString();//日
            if (_dd.Length == 1)
            {
                _dd = "0" + _dd;
            }
            _barNO = BarRole.BoxFlag + _yy + _mm + _dd;
            #endregion

            _printHistoryDS = new DataSet();
			_printHistoryDS.Tables.Add(CreatePrintHistory().Copy());
			GetBar_role();
            cbFormat.SelectedIndex = 0;
            txtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            
            //取得营业人资料
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _compInfo = _bps.GetComp().Tables[0];
		}

		private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			dataGridView1.Columns["PK"].Visible = false;
			dataGridView1.Columns["PRD_NO"].Visible = false;
			dataGridView1.Columns["PRD_MARK"].Visible = false;
			dataGridView1.Columns["SPC_NO"].Visible = false;
			dataGridView1.Columns["SPC_NAME"].Visible = false;
			dataGridView1.Columns["REM"].Visible = false;
			dataGridView1.Columns["COPY_COUNT"].Visible = false;
			dataGridView1.Columns["PAGE_COUNT"].Visible = false;

            dataGridView1.Columns["COMP_NAME"].Visible = false;
            dataGridView1.Columns["COMP_ENAME"].Visible = false;
            dataGridView1.Columns["COMP_ADR"].Visible = false;
            dataGridView1.Columns["COMP_TEL"].Visible = false;
            dataGridView1.Columns["COMP_FAX"].Visible = false;
            dataGridView1.Columns["BOX_IDX1"].Visible = false;

			dataGridView1.Columns["BAR_NO"].HeaderText = "序列号";
			dataGridView1.Columns["SN"].HeaderText = "流水号";
            dataGridView1.Columns["BOX_PRD"].HeaderText = "品名";
            dataGridView1.Columns["BOX_SPC"].HeaderText = "规格";
            dataGridView1.Columns["BOX_BAT"].HeaderText = "批号";
            dataGridView1.Columns["BOX_QTY"].HeaderText = "卷数";
            dataGridView1.Columns["DEP"].HeaderText = "部门";
            dataGridView1.Columns["BOX_LH"].HeaderText = "产品编号";
            dataGridView1.Columns["BOX_IDX_NAME"].HeaderText = "打印名称";

            dataGridView1.Columns["FOX_SUP"].HeaderText = "供应商";
            dataGridView1.Columns["FOX_CZ"].HeaderText = "材质";
            dataGridView1.Columns["FOX_PRD_NAME"].HeaderText = "品名";
            dataGridView1.Columns["FOX_BC"].HeaderText = "版次";
            dataGridView1.Columns["FOX_LH"].HeaderText = "Foxconn料号";
            dataGridView1.Columns["FOX_BAT_MANU"].HeaderText = "生产批号";
            dataGridView1.Columns["FOX_BAT_MAT"].HeaderText = "原料批号";
            dataGridView1.Columns["FOX_WEIGHT"].HeaderText = "重量(KG)";
            dataGridView1.Columns["FOX_XINGHAO"].HeaderText = "型号";
        }

        #region Date
        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            #region 取当前日期
            if (txtDate.Text != "")
            {
                DateTime _dt = Convert.ToDateTime(txtDate.Text);
                string _yy = _dt.Year.ToString();//年
                string _mm = _dt.Month.ToString();//月
                if (_mm.Length == 1)
                {
                    _mm = "0" + _mm;
                }
                string _dd = _dt.Day.ToString();//日
                if (_dd.Length == 1)
                {
                    _dd = "0" + _dd;
                }
                _barNO = BarRole.BoxFlag + _yy + _mm + _dd;
            }
            #endregion
        }
        #endregion

        #region 卷数
        private void txtPrdQty_Leave(object sender, EventArgs e)
        {
            if (txtPrdQty.Text != "")
            {
                StringBuilder _txtTemp = new StringBuilder();
                _txtTemp.Append(txtPrdQty.Text);
                string _regExp = @"^\d{1,}$";//至少为一位数字
                Regex _reg = new Regex(_regExp);
                if (!_reg.IsMatch(_txtTemp.ToString()))
                {
                    txtPrdQty.Text = "1";
                    MessageBox.Show("货品数量必须为正整数！");
                    return;
                }
                else
                    txtPrdQty.Text = _txtTemp.ToString();
            }
            else
            {
                txtPrdQty.Text = "1";
            }
            if (!String.IsNullOrEmpty(txtWeightPer.Text))
            {
                decimal _qty = Convert.ToInt32(txtPrdQty.Text) * Convert.ToDecimal(txtWeightPer.Text);
                txtWeight.Text = String.Format("{0:F2}", _qty);
            }
            else
                txtWeight.Text = "";
        }
        #endregion

        #region 品号
        private void btn4Prdt_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNo.Text = _qw.NO_RT;
                #region 货品资料
                onlineService.BarPrintServer _brs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                string _format = "BASIC";
                if (cbFormat.SelectedIndex == 1)
                    _format = "SAMPLE";
                if (cbFormat.SelectedIndex == 2)
                    _format = "SMALL";
                if (cbFormat.SelectedIndex == 3)
                    _format = "OTHER";
                if (cbFormat.SelectedIndex == 4)
                    _format = "FOXCONN";
                if (cbFormat.SelectedIndex == 5)
                    _format = "SBB";
                if (cbFormat.SelectedIndex == 6)
                    _format = "HQ";
                DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text.Trim(),_format);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    DataRow _dr = _ds.Tables[0].Rows[0];
                    txtSpc.Text = _dr["SPC"].ToString();
                    txtIdx1.Text = _dr["IDX_NAME"].ToString();
                    txtIdxNo.Text = _dr["IDX1"].ToString();
                    txtBatNo.Text = "";
                    txtPrintName.Text = Strings.StrConv(_dr["PRT_NAME_BOX"].ToString(), VbStrConv.SimplifiedChinese, 2);
                    txtPrdBH.Text = _dr["LH_BOX"].ToString();
                    txtPO.Text = "";

                    txtCZ.Text = Strings.StrConv(_dr["CZ_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);
                    txtXingHao.Text = Strings.StrConv(_dr["BC_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);
                    txtFoxLH.Text = Strings.StrConv(_dr["LH_FOXCONN"].ToString(), VbStrConv.SimplifiedChinese, 2);
                    txtBatManu.Text = "";
                    txtBatMat.Text = "";
                    txtWeightPer.Text = _dr["QTY_PER_GP"].ToString();
                    decimal _qtyPer = 0;
                    decimal _qty = 0;
                    if (!String.IsNullOrEmpty(_dr["QTY_PER_GP"].ToString()))
                        _qtyPer = Convert.ToDecimal(_dr["QTY_PER_GP"]);
                    if (!String.IsNullOrEmpty(txtPrdQty.Text))
                        _qty = Convert.ToInt32(txtPrdQty.Text) * _qtyPer;
                    txtWeight.Text = String.Format("{0:F2}", _qty);

                    txtBC.Text = "免检D版";
                }
                #endregion
            }
        }

        private void txtPrdNo_Leave(object sender, EventArgs e)
        {
            if (txtPrdNo.Text.Length > 0)
            {
                onlineService.BarPrintServer _brs = new BarCodePrintTSB.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                string _format = "BASIC";
                if (cbFormat.SelectedIndex == 1)
                    _format = "SAMPLE";
                if (cbFormat.SelectedIndex == 2)
                    _format = "SMALL";
                if (cbFormat.SelectedIndex == 3)
                    _format = "OTHER";
                if (cbFormat.SelectedIndex == 4)
                    _format = "FOXCONN";
                if (cbFormat.SelectedIndex == 5)
                    _format = "SBB";
                if (cbFormat.SelectedIndex == 6)
                    _format = "HQ";
                DataSet _ds = _brs.GetPrdtFull(txtPrdNo.Text,_format);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    DataRow _dr = _ds.Tables[0].Rows[0];
                    txtSpc.Text = _dr["SPC"].ToString();
                    txtIdx1.Text = _dr["IDX_NAME"].ToString();
                    txtIdxNo.Text = _dr["IDX1"].ToString();
                    txtBatNo.Text = "";
                    txtPrintName.Text = Strings.StrConv(_dr["PRT_NAME_BOX"].ToString(), VbStrConv.SimplifiedChinese, 2);
                    txtPrdBH.Text = _dr["LH_BOX"].ToString();
                    txtPO.Text = "";

                    txtCZ.Text = _dr["CZ_GP"].ToString();
                    txtXingHao.Text = _dr["BC_GP"].ToString();
                    txtFoxLH.Text = _dr["LH_FOXCONN"].ToString();
                    txtBatManu.Text = "";
                    txtBatMat.Text = "";
                    txtWeightPer.Text = _dr["QTY_PER_GP"].ToString();
                    decimal _qtyPer = 0;
                    decimal _qty = 0;
                    if (!String.IsNullOrEmpty(_dr["QTY_PER_GP"].ToString()))
                        _qtyPer = Convert.ToDecimal(_dr["QTY_PER_GP"]);
                    if (!String.IsNullOrEmpty(txtPrdQty.Text))
                        _qty = Convert.ToInt32(txtPrdQty.Text) * _qtyPer;
                    txtWeight.Text = String.Format("{0:F2}", _qty);
                    txtBC.Text = "免检D版";
                }
                else
                {
                    MessageBox.Show(String.Format("品号[{0}]不存在！", txtPrdNo.Text));
                    ClearData();
                }
            }
            else
            {
                ClearData();
            }
        }
        #endregion

        #region 批号
        private void btn4BatNo_Click(object sender, EventArgs e)
        {
            QueryBat _bat = new QueryBat();
            _bat.MultiSelect = true;
            if (cbSBBar.Checked)
            {
                _bat.SQLWhere += " AND ISNULL(B.PRD_NO,'')='' AND LEFT(A.BAT_NO,7) IN (SELECT SUBSTRING(BAR_NO,13,7) FROM BAR_REC WHERE PRD_NO='" + txtPrdNo.Text + "') ";
            }
            else
            {
                _bat.SQLWhere += " AND ISNULL(C.KND,'')='2'";
                if (txtPrdNo.Text != "")
                    _bat.SQLWhere += " AND ISNULL(B.PRD_NO,'')='" + txtPrdNo.Text + "'";
            }

            if (_bat.ShowDialog() == DialogResult.OK)
            {
                txtBatNo.Text = _bat.NO_RT;
            }
        }

        private void txtBatNo_Leave(object sender, EventArgs e)
        {
            //string[] _batNoLst = txtBatNo.Text.Split('/');
            //onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            //_bps.UseDefaultCredentials = true;
            //DataSet _dsBat = new DataSet();
            //foreach (string _batNo in _batNoLst)
            //{
            //    if (!String.IsNullOrEmpty(_batNo))
            //    {
            //        if (_batNo.Length != 7)
            //        {
            //            MessageBox.Show("批号输入不正确！");
            //            txtBatNo.Text = "";
            //            _canUpdate = false;
            //            return;
            //        }
            //        else//检测批号有没有打印过（即有没有生产过）
            //        {
            //            //如果是退货条码，由于流水号不跟品号走，只跟批号有关，则BAR_SQNO表中PRD_NO是为空
            //            if (cbSBBar.Checked)
            //                _dsBat = _bps.GetBatNo2(" AND LEFT(A.BAT_NO,7)='" + _batNo + "' AND ISNULL(B.PRD_NO,'')='' AND LEFT(A.BAT_NO,7) IN (SELECT SUBSTRING(BAR_NO,13,7) FROM BAR_REC WHERE PRD_NO='" + txtPrdNo.Text + "')  ");
            //            else
            //            {
            //                //如果不是退货条码，由于流水号跟品号+批号走，则必须检测BAR_SQNO表中对应PRD_NO中此批号有没有存在（PRD_MARK(储存批号)）
            //                if (txtPrdNo.Text.Length > 0)
            //                    _dsBat = _bps.GetBatNo2(" AND LEFT(A.BAT_NO,7)='" + _batNo + "' AND ISNULL(B.PRD_NO,'')='" + txtPrdNo.Text + "' ");
            //                else
            //                    _dsBat = _bps.GetBatNo2(" AND LEFT(A.BAT_NO,7)='" + _batNo + "' AND ISNULL(B.PRD_NO,'')<>'' ");
            //            }
            //            if (_dsBat != null && _dsBat.Tables.Count > 0 && _dsBat.Tables[0].Rows.Count > 0)
            //            {
            //                if (!cbSBBar.Checked && _dsBat.Tables[0].Select("PRD_NO='" + txtPrdNo.Text + "'").Length <= 0)
            //                {
            //                    MessageBox.Show("此批号已使用！");
            //                    txtBatNo.Text = "";
            //                    _canUpdate = false;
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show(string.Format("批号[{0}]不存在！", _batNo));
            //                txtBatNo.Text = "";
            //                _canUpdate = false;
            //                return;
            //            }
            //        }
            //    }
            //}

            string[] _batNoLst = txtBatNo.Text.Split('/');
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            DataSet dataSet = new DataSet();
            foreach (string _batNo in _batNoLst)
            {
                if (!string.IsNullOrEmpty(_batNo))
                {
                    if (_batNo.Length != 7)
                    {
                        MessageBox.Show("批号输入不正确！");
                        txtBatNo.Text = "";
                        _canUpdate = false;
                        break;
                    }
                    dataSet = (cbSBBar.Checked ? _bps.GetBatNo2(" AND LEFT(A.BAT_NO,7)='" + _batNo + "' AND ISNULL(B.PRD_NO,'')='' AND LEFT(A.BAT_NO,7) IN (SELECT SUBSTRING(BAR_NO,13,7) FROM BAR_REC WHERE PRD_NO='" + txtPrdNo.Text + "')  ") : ((txtPrdNo.Text.Length <= 0) ? _bps.GetBatNo2(" AND LEFT(A.BAT_NO,7)='" + _batNo + "' AND ISNULL(B.PRD_NO,'')<>'' ") : _bps.GetBatNo2(" AND LEFT(A.BAT_NO,7)='" + _batNo + "' AND ISNULL(B.PRD_NO,'')='" + txtPrdNo.Text + "' ")));
                    if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
                    {
                        MessageBox.Show(string.Format("批号[{0}]不存在！", _batNo));
                        txtBatNo.Text = "";
                        _canUpdate = false;
                        break;
                    }
                    if (!cbSBBar.Checked && dataSet.Tables[0].Select("PRD_NO='" + txtPrdNo.Text + "'").Length <= 0)
                    {
                        MessageBox.Show("此批号已使用！");
                        txtBatNo.Text = "";
                        _canUpdate = false;
                    }
                }
            }
        }
        #endregion

        #region 起止流水号
        private void txtSNB_Leave(object sender, EventArgs e)
        {
            if (txtSNB.Text != "")
            {
                StringBuilder _txtTemp = new StringBuilder();
                _txtTemp.Append(txtSNB.Text);
                string _regExp = @"^\d{4}$";
                for (int i = 0; i < 4 - txtSNB.Text.Length; i++)
                {
                    _txtTemp.Insert(0, "0");
                }
                Regex _reg = new Regex(_regExp);
                if (!_reg.IsMatch(_txtTemp.ToString()))
                {
                    txtSNB.Text = "";
                    MessageBox.Show("起止流水号必须为数字！");
                    return;
                }
                else
                    txtSNB.Text = _txtTemp.ToString();
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
                StringBuilder _txtTemp = new StringBuilder();
                _txtTemp.Append(txtSNE.Text);
                string _regExp = @"^\d{4}$";
                for (int i = 0; i < 4 - txtSNE.Text.Length; i++)
                {
                    _txtTemp.Insert(0, "0");
                }
                Regex _reg = new Regex(_regExp);
                if (!_reg.IsMatch(_txtTemp.ToString()))
                {
                    txtSNE.Text = "";
                    MessageBox.Show("起止流水号必须为数字！");
                    return;
                }
                else
                    txtSNE.Text = _txtTemp.ToString();
                if (txtSNB.Text == "")
                {
                    txtSNB.Text = txtSNE.Text;
                }
            }
        }
        #endregion

        private void btn4IdxNo_Click(object sender, EventArgs e)
        {
            if (txtIdxNo.Text != "")
            {
                QueryWin _qw = new QueryWin();
                _qw.PGM = "BAR_IDX";
                _qw.SQLWhere += " AND IDX_NO='" + txtIdxNo.Text + "'";
                if (_qw.ShowDialog() == DialogResult.OK)
                {
                    txtPrintName.Text = Strings.StrConv(_qw.NAME_RTN, VbStrConv.SimplifiedChinese, 2); 
                }
            }
            else
            {
                MessageBox.Show("请先选择品号！");
            }
        }

        private void cbFormat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_printHistoryDS.Tables[0].Rows.Count > 0)
            {
                if (MessageBox.Show("修改打印版式将清除右边所有待打印的记录，确认修改打印版式？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _printHistoryDS.Tables[0].Clear();
                }
                else
                {
                    cbFormat.SelectedIndex = _formatSelIdx;
                    return;
                }
            }
            txtPrdNo_Leave(null, null);
            if (cbFormat.SelectedIndex == 3)
            {
                btn4IdxNo.Enabled = true;
                pnlFoxconn.Visible = false;
            }
            else if (cbFormat.SelectedIndex == 4)
            {
                btn4IdxNo.Enabled = false;
                pnlFoxconn.Visible = true;
            }
            else
            {
                btn4IdxNo.Enabled = false;
                pnlFoxconn.Visible = false;
            }
        }

        int _formatSelIdx = 0;
        private void cbFormat_DropDown(object sender, EventArgs e)
        {
            _formatSelIdx = cbFormat.SelectedIndex;
        }

        private void btn4BH_Click(object sender, EventArgs e)
        {
            if (txtPrdNo.Text != "")
            {
                QueryWin _qw = new QueryWin();
                _qw.PGM = "BAR_IDX";
                _qw.SQLWhere += " AND IDX_NO='" + txtPrdNo.Text + "'";
                if (_qw.ShowDialog() == DialogResult.OK)
                {
                    txtPrdBH.Text = Strings.StrConv(_qw.NAME_RTN, VbStrConv.SimplifiedChinese, 2); ;
                }
            }
            else
            {
                MessageBox.Show("请先选择品号！");
            }
        }
    }
}