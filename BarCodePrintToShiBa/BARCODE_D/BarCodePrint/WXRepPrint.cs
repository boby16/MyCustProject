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
	public partial class WXRepPrint : Form
	{
		private DataSet _printHistoryDS;
        private DataTable _compInfo = new DataTable();
        private string _barNO = "";//取得当前日期，格式：yyyymmdd。加上流水号作为打印箱条码信息
        DataSet _boxBarDsUpdate = new DataSet();//用于更新箱条码信息

		#region Constructor
		public WXRepPrint()
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
			_barPrintTable.Columns.Add("PRD_SPC", System.Type.GetType("System.String"));
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

            _barPrintTable.Columns.Add("BOX_PRD", System.Type.GetType("System.String"));//外箱条码：品名
            _barPrintTable.Columns.Add("BOX_SPC", System.Type.GetType("System.String"));//外箱条码：规格
            _barPrintTable.Columns.Add("BOX_BAT", System.Type.GetType("System.String"));//外箱条码：批号
            _barPrintTable.Columns.Add("BOX_QTY", System.Type.GetType("System.String"));//外箱条码：卷数
            _barPrintTable.Columns.Add("BOX_LH", System.Type.GetType("System.String"));//外箱条码：打印产品编号
            _barPrintTable.Columns.Add("BOX_IDX1", System.Type.GetType("System.String"));//外箱条码：中类代号
            _barPrintTable.Columns.Add("BOX_IDX_NAME", System.Type.GetType("System.String"));//外箱条码：中类名称
            _barPrintTable.Columns.Add("BOX_FORMAT", System.Type.GetType("System.String"));//外箱条码：打印版式

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
            int SNLength = 0;
            int _snB = Convert.ToInt32(txtSNB.Text);
            int _snE = Convert.ToInt32(txtSNE.Text);
            string _initNo = "";
            string _barLst = "";
            DataSet _boxBarDs = new DataSet();
            DataRow _drBox = null;
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

                onlineService.BarPrintServer _bps = new global::BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                _boxBarDs = _bps.GetBoxBarPrint(_barNO + _initNo);//取箱条码
                if (_boxBarDs != null && _boxBarDs.Tables.Count > 0 && _boxBarDs.Tables[0].Rows.Count > 0)
                {
                    _drBox = _boxBarDs.Tables[0].Rows[0];
                }

                for (int k = 0; k < Convert.ToInt32(txtCount.Text); k++)
                {
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
                    if (_drBox != null)
                    {
                        if (_drBox["PRD_NO"].ToString() != txtPrdNo.Text || _drBox["BAT_NO"].ToString() != txtBatNo.Text || _drBox["QTY"].ToString() != txtPrdQty.Text)
                        {
                            MessageBox.Show("箱条码【" + _barNO + _initNo + "】的品号、批号和数量与当前的打印品号、批号和数量不一致，不能在此次打印。");
                            return;
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
                        _dr["BOX_IDX1"] = txtIdxNo.Text;//中类代号
                        _dr["BOX_FORMAT"] = cbFormat.SelectedIndex + 1;//1:标准版；2、简约版；3、列印编号；4、特殊版；5、富士康；6、塑柏碧版；7、瀚荃版
                        _dr["BOX_QTY"] = txtPrdQty.Text; //箱量

                        if (cbFormat.SelectedIndex == 3 || cbFormat.SelectedIndex == 6)//特殊版和瀚荃版，打印特殊名称
                            _dr["BOX_IDX_NAME"] = txtPrintName.Text;//货品打印的名称
                        else
                            _dr["BOX_IDX_NAME"] = txtIdx1.Text;//货品打印的名称

                        if (cbFormat.SelectedIndex == 4)
                        {
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

                            ////富士康标签信息
                            //_dr["FOX_SUP"] = "固品";//供应商
                            //_dr["FOX_CZ"] = Strings.StrConv(_drBox["CZ_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);//材质
                            //_dr["FOX_PRD_NAME"] = "片材";//品名
                            //_dr["FOX_BC"] = Strings.StrConv(_drBox["BC_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);//版次
                            //_dr["FOX_LH"] = Strings.StrConv(_drBox["LH_FOXCONN"].ToString(), VbStrConv.SimplifiedChinese, 2);//Foxconn料号
                            //_dr["FOX_BAT_MANU"] = "";//生产批号
                            //_dr["FOX_BAT_MAT"] = "";//原料批号
                            //decimal _qtyPer = 0;
                            //decimal _qty = 0;
                            //if (!String.IsNullOrEmpty(_drBox["QTY_PER_GP"].ToString()))
                            //    _qtyPer = Convert.ToDecimal(_drBox["QTY_PER_GP"]);
                            //if (!String.IsNullOrEmpty(_drBox["QTY"].ToString()))
                            //    _qty = Convert.ToDecimal(_drBox["QTY"]) * _qtyPer;
                            //_dr["FOX_WEIGHT"] = String.Format("{0:F2}", _qty); ; //重量
                        }
                    }

                    printHistory.Rows.Add(_dr);
                }
                #region 此数据用于更新
                _boxBarDsUpdate = _bps.GetBoxBar1(" AND ISNULL(A.STOP_ID,'F')<>'T' AND A.BOX_NO in (" + _barLst + ")");//取箱条码
                if (_boxBarDsUpdate != null && _boxBarDsUpdate.Tables.Count > 0 && _boxBarDsUpdate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow _drBoxUpd in _boxBarDsUpdate.Tables[0].Rows)
                    {
                        _drBoxUpd["PRD_NO"] = txtPrdNo.Text;
                        _drBoxUpd["SPC"] = txtSpc.Text;
                        _drBoxUpd["BAT_NO"] = txtBatNo.Text;
                        _drBoxUpd["QTY"] = txtPrdQty.Text;
                        _drBoxUpd["FORMAT"] = cbFormat.SelectedIndex + 1;//1:标准版；2、简约版；3、列印编号；4、特殊版；5、富士康；6、塑柏碧；7、瀚荃
                        _drBoxUpd["IDX1"] = txtIdxNo.Text;//中类代号
                        if (cbFormat.SelectedIndex == 3 || cbFormat.SelectedIndex == 6)//特殊版和瀚荃版，打印特殊名称
                            _drBoxUpd["IDX_NAME"] = txtPrintName.Text;//打印名称
                        else
                            _drBoxUpd["IDX_NAME"] = txtIdx1.Text;//打印名称（上层中类）
                        if (cbFormat.SelectedIndex == 5)
                            _drBoxUpd["LH"] = txtPrdBH.Text;//产品编号
                        else
                            _drBoxUpd["LH"] = "";
                    }
                }
                #endregion
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
			_result = _bs.PrintBarCode(barCodeDS,"2", _format);
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

		private void cbXpNew_Click(object sender, EventArgs e)
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
            txtCount.Text = "1";
            txtPrdNo.Text = "";
            txtSpc.Text = "";
            txtIdx1.Text = "";
            txtIdxNo.Text = "";
            txtBatNo.Text = "";
            txtPrdQty.Text = "";
            txtPrintName.Text = "";
            txtPrdBH.Text = "";
            txtPO.Text = "";

            txtSup.Text = "固品";//供应商
            txtPrdName.Text = "片材";//品名
            txtCZ.Text = "";
            txtBC.Text = "";
            txtFoxLH.Text = "";
            txtWeightPer.Text = "0";
            txtWeight.Text = "";
            txtBatManu.Text = "";//生产批号
            txtBatMat.Text = "";//原料批号
            txtXingHao.Text = "";
        }

		private void bnXpAddPrint_Click(object sender, EventArgs e)
		{
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
                    MessageBox.Show(String.Format("外箱条码[{0}]不存在！", _BarPrinted));
                    return;
                }
                DataSet _ds = _brSer.UpdateBoxPrintData(_boxBarDsUpdate);
                if (_ds.HasErrors == false && _ds != null)
                {
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
                    _printHistoryDS.Merge(_ds);
                    MessageBox.Show("操作失败");
                }
            }
            else
            {
                MessageBox.Show("没有数据要打印");
            }
        }

		private void WXRepPrint_Load(object sender, EventArgs e)
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
            //cbFormat.SelectedIndex = 0;
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
            dataGridView1.Columns["PRD_SPC"].Visible = false;
			dataGridView1.Columns["SPC_NO"].Visible = false;
			dataGridView1.Columns["SPC_NAME"].Visible = false;
			dataGridView1.Columns["REM"].Visible = false;
			dataGridView1.Columns["COPY_COUNT"].Visible = false;

            dataGridView1.Columns["COMP_NAME"].Visible = false;
            dataGridView1.Columns["COMP_ENAME"].Visible = false;
            dataGridView1.Columns["COMP_ADR"].Visible = false;
            dataGridView1.Columns["COMP_TEL"].Visible = false;
            dataGridView1.Columns["COMP_FAX"].Visible = false;

			dataGridView1.Columns["BAR_NO"].HeaderText = "序列号";
			dataGridView1.Columns["SN"].HeaderText = "流水号";
            dataGridView1.Columns["BOX_PRD"].HeaderText = "品名";
            dataGridView1.Columns["BOX_SPC"].HeaderText = "规格";
            dataGridView1.Columns["BOX_BAT"].HeaderText = "批号";
            dataGridView1.Columns["BOX_QTY"].HeaderText = "卷数";
            dataGridView1.Columns["PAGE_COUNT"].Visible = false;
            dataGridView1.Columns["DEP"].HeaderText = "部门";
            dataGridView1.Columns["BOX_IDX1"].Visible = false;
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

		private void txtCount_Leave(object sender, EventArgs e)
		{
			if (txtCount.Text != "")
			{
                StringBuilder _txtTemp = new StringBuilder();
                _txtTemp.Append(txtCount.Text);
                string _regExp = @"^\d{1,}$";//至少为一位数字
                Regex _reg = new Regex(_regExp);
                if (!_reg.IsMatch(_txtTemp.ToString()))
                {
                    txtCount.Text = "1";
                    MessageBox.Show("打印张数必须为正整数！");
                    return;
                }
                else
                    txtCount.Text = _txtTemp.ToString();
			}
			else 
			{
				txtCount.Text = "1";
			}
        }

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

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name != "FOX_SUP" && dataGridView1.Columns[e.ColumnIndex].Name != "FOX_PRD_NAME" 
                && dataGridView1.Columns[e.ColumnIndex].Name != "FOX_BAT_MANU" && dataGridView1.Columns[e.ColumnIndex].Name != "FOX_BAT_MAT" 
                && dataGridView1.Columns[e.ColumnIndex].Name != "FOX_WEIGHT" && dataGridView1.Columns[e.ColumnIndex].Name != "FOX_XINGHAO")
                dataGridView1.Columns[e.ColumnIndex].ReadOnly = true;
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
            //自动带出箱条码的历史信息，假如打印的箱条码有多个，只取第一个箱条码的历史信息
            int SNLength = 0;
            int _snB = 1;
            try
            {
                _snB = Convert.ToInt32(txtSNB.Text);
            }
            catch
            {
                MessageBox.Show("请输入流水号！");
                return;
            }
            string _initNo = ""; 
            DataSet _boxBarDs = new DataSet();
            DataRow _drBox = null;
            StringBuilder _sn = new StringBuilder();
            SNLength = _snB.ToString().Length;
            for (int j = 0; j < 4 - SNLength; j++)
            {
                _sn.Insert(0, "0");
            }
            _initNo = _sn.ToString() + _snB.ToString();
            onlineService.BarPrintServer _bps = new global::BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _boxBarDs = _bps.GetBoxBarPrint(_barNO + _initNo);//取箱条码
            if (_boxBarDs != null && _boxBarDs.Tables.Count > 0 && _boxBarDs.Tables[0].Rows.Count > 0)
            {
                _drBox = _boxBarDs.Tables[0].Rows[0];
                txtPrdNo.Text = _drBox["PRD_NO"].ToString();
                txtSpc.Text = _drBox["SPC"].ToString();
                txtIdxNo.Text = _drBox["IDX1"].ToString();//中类代号（作用：用于txtPrintName选择时的条件）
                txtIdx1.Text = _drBox["IDX_UP"].ToString();//非特殊版的打印名称
                txtBatNo.Text = _drBox["BAT_NO"].ToString();
                txtPrdQty.Text = _drBox["QTY"].ToString();
                txtPrintName.Text = _drBox["IDX_NAME"].ToString();//特殊版的打印名称 
                txtPrdBH.Text = _drBox["LH"].ToString();//产品编号
                txtPO.Text = "";
                if (cbFormat.SelectedIndex == 4)
                {
                    //富士康标签信息
                    txtSup.Text = "固品";//供应商
                    txtCZ.Text = Strings.StrConv(_drBox["CZ_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);//材质
                    txtPrdName.Text = "片材";//品名
                    txtBC.Text = Strings.StrConv(_drBox["BC_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);//版次
                    txtFoxLH.Text = Strings.StrConv(_drBox["LH_FOXCONN"].ToString(), VbStrConv.SimplifiedChinese, 2);//Foxconn料号
                    txtBatManu.Text = "";//生产批号
                    txtBatMat.Text = "";//原料批号
                    decimal _qtyPer = 0;
                    decimal _qty = 0;
                    if (!String.IsNullOrEmpty(_drBox["QTY_PER_GP"].ToString()))
                        _qtyPer = Convert.ToDecimal(_drBox["QTY_PER_GP"]);
                    if (!String.IsNullOrEmpty(_drBox["QTY"].ToString()))
                        _qty = Convert.ToDecimal(_drBox["QTY"]) * _qtyPer;
                    txtWeightPer.Text = _qtyPer.ToString();
                    txtWeight.Text = String.Format("{0:F2}", _qty); ; //重量

                    txtXingHao.Text = _drBox["KEHUPINHAO"].ToString();
                }
            }
            else
            {
                MessageBox.Show("箱条码不存在！");
                ClearData();
            }
        }

        int _formatSelIdx = 0;
        private void cbFormat_DropDown(object sender, EventArgs e)
        {
            _formatSelIdx = cbFormat.SelectedIndex;
        }

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