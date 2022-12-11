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
        private string _barNO = "";//ȡ�õ�ǰ���ڣ���ʽ��yyyymmdd��������ˮ����Ϊ��ӡ��������Ϣ
        DataSet _boxBarDsUpdate = new DataSet();//���ڸ�����������Ϣ

		#region Constructor
		public WXRepPrint()
		{
			InitializeComponent();
		}
		#endregion

        #region function

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

            _barPrintTable.Columns.Add("BOX_PRD", System.Type.GetType("System.String"));//�������룺Ʒ��
            _barPrintTable.Columns.Add("BOX_SPC", System.Type.GetType("System.String"));//�������룺���
            _barPrintTable.Columns.Add("BOX_BAT", System.Type.GetType("System.String"));//�������룺����
            _barPrintTable.Columns.Add("BOX_QTY", System.Type.GetType("System.String"));//�������룺����
            _barPrintTable.Columns.Add("BOX_LH", System.Type.GetType("System.String"));//�������룺��ӡ��Ʒ���
            _barPrintTable.Columns.Add("BOX_IDX1", System.Type.GetType("System.String"));//�������룺�������
            _barPrintTable.Columns.Add("BOX_IDX_NAME", System.Type.GetType("System.String"));//�������룺��������
            _barPrintTable.Columns.Add("BOX_FORMAT", System.Type.GetType("System.String"));//�������룺��ӡ��ʽ

            //��ʿ����ǩ��Ϣ
            _barPrintTable.Columns.Add("FOX_SUP", System.Type.GetType("System.String"));//��Ӧ��
            _barPrintTable.Columns.Add("FOX_CZ", System.Type.GetType("System.String"));//����
            _barPrintTable.Columns.Add("FOX_PRD_NAME", System.Type.GetType("System.String"));//Ʒ��
            _barPrintTable.Columns.Add("FOX_BC", System.Type.GetType("System.String"));//���
            _barPrintTable.Columns.Add("FOX_LH", System.Type.GetType("System.String"));//Foxconn�Ϻ�
            _barPrintTable.Columns.Add("FOX_BAT_MANU", System.Type.GetType("System.String"));//��������
            _barPrintTable.Columns.Add("FOX_BAT_MAT", System.Type.GetType("System.String"));//ԭ������
            _barPrintTable.Columns.Add("FOX_WEIGHT", System.Type.GetType("System.String"));//����
            _barPrintTable.Columns.Add("FOX_XINGHAO", System.Type.GetType("System.String"));//�ͺ�

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
                _boxBarDs = _bps.GetBoxBarPrint(_barNO + _initNo);//ȡ������
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
                        //ȡ��Ӫҵ������
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
                            MessageBox.Show("�����롾" + _barNO + _initNo + "����Ʒ�š����ź������뵱ǰ�Ĵ�ӡƷ�š����ź�������һ�£������ڴ˴δ�ӡ��");
                            return;
                        }

                        _dr["BOX_PRD"] = txtPrdNo.Text;//Ʒ��
                        //����ӡ��ʽΪ--�����-��Ʒ���ʱ����SPC_NO�ֶδ洢P/O�ţ��Ͳ����������ֶ���
                        if (cbFormat.SelectedIndex == 5)
                        {
                            _dr["BOX_LH"] = txtPrdBH.Text;//��Ʒ���
                            _dr["SPC_NAME"] = txtPO.Text;
                        }
                        else
                        {
                            _dr["BOX_LH"] = "";
                            _dr["SPC_NAME"] = DBNull.Value;
                        }
                        _dr["BOX_SPC"] = txtSpc.Text;//���
                        _dr["BOX_BAT"] = txtBatNo.Text;//����
                        _dr["BOX_IDX1"] = txtIdxNo.Text;//�������
                        _dr["BOX_FORMAT"] = cbFormat.SelectedIndex + 1;//1:��׼�棻2����Լ�棻3����ӡ��ţ�4������棻5����ʿ����6���ܰر̰棻7�������
                        _dr["BOX_QTY"] = txtPrdQty.Text; //����

                        if (cbFormat.SelectedIndex == 3 || cbFormat.SelectedIndex == 6)//����������棬��ӡ��������
                            _dr["BOX_IDX_NAME"] = txtPrintName.Text;//��Ʒ��ӡ������
                        else
                            _dr["BOX_IDX_NAME"] = txtIdx1.Text;//��Ʒ��ӡ������

                        if (cbFormat.SelectedIndex == 4)
                        {
                            //��ʿ����ǩ��Ϣ
                            _dr["FOX_SUP"] = txtSup.Text;//��Ӧ��
                            _dr["FOX_CZ"] = txtCZ.Text;//����
                            _dr["FOX_PRD_NAME"] = txtPrdName.Text;//Ʒ��
                            _dr["FOX_BC"] = txtBC.Text;//���
                            _dr["FOX_LH"] = txtFoxLH.Text;//Foxconn�Ϻ�
                            _dr["FOX_BAT_MANU"] = txtBatManu.Text;//��������
                            _dr["FOX_BAT_MAT"] = txtBatMat.Text;//ԭ������
                            _dr["FOX_WEIGHT"] = txtWeight.Text; //����

                            _dr["FOX_XINGHAO"] = txtXingHao.Text;//�ͺ�

                            ////��ʿ����ǩ��Ϣ
                            //_dr["FOX_SUP"] = "��Ʒ";//��Ӧ��
                            //_dr["FOX_CZ"] = Strings.StrConv(_drBox["CZ_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);//����
                            //_dr["FOX_PRD_NAME"] = "Ƭ��";//Ʒ��
                            //_dr["FOX_BC"] = Strings.StrConv(_drBox["BC_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);//���
                            //_dr["FOX_LH"] = Strings.StrConv(_drBox["LH_FOXCONN"].ToString(), VbStrConv.SimplifiedChinese, 2);//Foxconn�Ϻ�
                            //_dr["FOX_BAT_MANU"] = "";//��������
                            //_dr["FOX_BAT_MAT"] = "";//ԭ������
                            //decimal _qtyPer = 0;
                            //decimal _qty = 0;
                            //if (!String.IsNullOrEmpty(_drBox["QTY_PER_GP"].ToString()))
                            //    _qtyPer = Convert.ToDecimal(_drBox["QTY_PER_GP"]);
                            //if (!String.IsNullOrEmpty(_drBox["QTY"].ToString()))
                            //    _qty = Convert.ToDecimal(_drBox["QTY"]) * _qtyPer;
                            //_dr["FOX_WEIGHT"] = String.Format("{0:F2}", _qty); ; //����
                        }
                    }

                    printHistory.Rows.Add(_dr);
                }
                #region ���������ڸ���
                _boxBarDsUpdate = _bps.GetBoxBar1(" AND ISNULL(A.STOP_ID,'F')<>'T' AND A.BOX_NO in (" + _barLst + ")");//ȡ������
                if (_boxBarDsUpdate != null && _boxBarDsUpdate.Tables.Count > 0 && _boxBarDsUpdate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow _drBoxUpd in _boxBarDsUpdate.Tables[0].Rows)
                    {
                        _drBoxUpd["PRD_NO"] = txtPrdNo.Text;
                        _drBoxUpd["SPC"] = txtSpc.Text;
                        _drBoxUpd["BAT_NO"] = txtBatNo.Text;
                        _drBoxUpd["QTY"] = txtPrdQty.Text;
                        _drBoxUpd["FORMAT"] = cbFormat.SelectedIndex + 1;//1:��׼�棻2����Լ�棻3����ӡ��ţ�4������棻5����ʿ����6���ܰر̣�7�����
                        _drBoxUpd["IDX1"] = txtIdxNo.Text;//�������
                        if (cbFormat.SelectedIndex == 3 || cbFormat.SelectedIndex == 6)//����������棬��ӡ��������
                            _drBoxUpd["IDX_NAME"] = txtPrintName.Text;//��ӡ����
                        else
                            _drBoxUpd["IDX_NAME"] = txtIdx1.Text;//��ӡ���ƣ��ϲ����ࣩ
                        if (cbFormat.SelectedIndex == 5)
                            _drBoxUpd["LH"] = txtPrdBH.Text;//��Ʒ���
                        else
                            _drBoxUpd["LH"] = "";
                    }
                }
                #endregion
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

            _barCodeTable1.Columns.Add("BOX_PRD", System.Type.GetType("System.String"));//�������룺Ʒ��
            _barCodeTable1.Columns.Add("BOX_SPC", System.Type.GetType("System.String"));//�������룺���
            _barCodeTable1.Columns.Add("BOX_BAT", System.Type.GetType("System.String"));//�������룺����
            _barCodeTable1.Columns.Add("BOX_QTY", System.Type.GetType("System.String"));//�������룺����
            _barCodeTable1.Columns.Add("BOX_LH", System.Type.GetType("System.String"));//�������룺��Ʒ���
            _barCodeTable1.Columns.Add("BOX_IDX1", System.Type.GetType("System.String"));//�������룺�������
            _barCodeTable1.Columns.Add("BOX_IDX_NAME", System.Type.GetType("System.String"));//�������룺��������

            //��ʿ����ǩ��Ϣ
            _barCodeTable1.Columns.Add("FOX_SUP", System.Type.GetType("System.String"));//��Ӧ��
            _barCodeTable1.Columns.Add("FOX_CZ", System.Type.GetType("System.String"));//����
            _barCodeTable1.Columns.Add("FOX_PRD_NAME", System.Type.GetType("System.String"));//Ʒ��
            _barCodeTable1.Columns.Add("FOX_BC", System.Type.GetType("System.String"));//���
            _barCodeTable1.Columns.Add("FOX_LH", System.Type.GetType("System.String"));//Foxconn�Ϻ�
            _barCodeTable1.Columns.Add("FOX_BAT_MANU", System.Type.GetType("System.String"));//��������
            _barCodeTable1.Columns.Add("FOX_BAT_MAT", System.Type.GetType("System.String"));//ԭ������
            _barCodeTable1.Columns.Add("FOX_WEIGHT", System.Type.GetType("System.String"));//����
            _barCodeTable1.Columns.Add("FOX_XINGHAO", System.Type.GetType("System.String"));//�ͺ�

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

                    //��ʿ����ǩ��Ϣ
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
		/// ת������ӡ�����к����ϼ�
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
			}
		}
		#endregion

		#endregion

		private void cbXpNew_Click(object sender, EventArgs e)
        {
            DialogResult _result;
            _result = MessageBox.Show("ȷ�������ߴ���ӡ�����ݣ�", "��ʾ", MessageBoxButtons.YesNo);
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

            txtSup.Text = "��Ʒ";//��Ӧ��
            txtPrdName.Text = "Ƭ��";//Ʒ��
            txtCZ.Text = "";
            txtBC.Text = "";
            txtFoxLH.Text = "";
            txtWeightPer.Text = "0";
            txtWeight.Text = "";
            txtBatManu.Text = "";//��������
            txtBatMat.Text = "";//ԭ������
            txtXingHao.Text = "";
        }

		private void bnXpAddPrint_Click(object sender, EventArgs e)
		{
            if (String.IsNullOrEmpty(this.txtSNB.Text.Trim()) || String.IsNullOrEmpty(this.txtSNE.Text.Trim()))
            {
                MessageBox.Show("��ֹ��ˮ�Ų���Ϊ�գ�");
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
                    //�ж��Ƿ��δ��ӡ
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
                    //��δ��ӡ��������
                    MessageBox.Show(String.Format("��������[{0}]�����ڣ�", _BarPrinted));
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

		private void WXRepPrint_Load(object sender, EventArgs e)
        {
            #region ȡ��ǰ����
            string _yy = System.DateTime.Now.Year.ToString();//��
            string _mm = System.DateTime.Now.Month.ToString();//��
            if (_mm.Length == 1)
            {
                _mm = "0" + _mm;
            }
            string _dd = System.DateTime.Now.Day.ToString();//��
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

            //ȡ��Ӫҵ������
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

			dataGridView1.Columns["BAR_NO"].HeaderText = "���к�";
			dataGridView1.Columns["SN"].HeaderText = "��ˮ��";
            dataGridView1.Columns["BOX_PRD"].HeaderText = "Ʒ��";
            dataGridView1.Columns["BOX_SPC"].HeaderText = "���";
            dataGridView1.Columns["BOX_BAT"].HeaderText = "����";
            dataGridView1.Columns["BOX_QTY"].HeaderText = "����";
            dataGridView1.Columns["PAGE_COUNT"].Visible = false;
            dataGridView1.Columns["DEP"].HeaderText = "����";
            dataGridView1.Columns["BOX_IDX1"].Visible = false;
            dataGridView1.Columns["BOX_LH"].HeaderText = "��Ʒ���";
            dataGridView1.Columns["BOX_IDX_NAME"].HeaderText = "��ӡ����";

            dataGridView1.Columns["FOX_SUP"].HeaderText = "��Ӧ��";
            dataGridView1.Columns["FOX_CZ"].HeaderText = "����";
            dataGridView1.Columns["FOX_PRD_NAME"].HeaderText = "Ʒ��";
            dataGridView1.Columns["FOX_BC"].HeaderText = "���";
            dataGridView1.Columns["FOX_LH"].HeaderText = "Foxconn�Ϻ�";
            dataGridView1.Columns["FOX_BAT_MANU"].HeaderText = "��������";
            dataGridView1.Columns["FOX_BAT_MAT"].HeaderText = "ԭ������";
            dataGridView1.Columns["FOX_WEIGHT"].HeaderText = "����(KG)";
            dataGridView1.Columns["FOX_XINGHAO"].HeaderText = "�ͺ�";

		}

		private void txtCount_Leave(object sender, EventArgs e)
		{
			if (txtCount.Text != "")
			{
                StringBuilder _txtTemp = new StringBuilder();
                _txtTemp.Append(txtCount.Text);
                string _regExp = @"^\d{1,}$";//����Ϊһλ����
                Regex _reg = new Regex(_regExp);
                if (!_reg.IsMatch(_txtTemp.ToString()))
                {
                    txtCount.Text = "1";
                    MessageBox.Show("��ӡ��������Ϊ��������");
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
            #region ȡ��ǰ����
            if (txtDate.Text != "")
            {
                DateTime _dt = Convert.ToDateTime(txtDate.Text);
                string _yy = _dt.Year.ToString();//��
                string _mm = _dt.Month.ToString();//��
                if (_mm.Length == 1)
                {
                    _mm = "0" + _mm;
                }
                string _dd = _dt.Day.ToString();//��
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
                    MessageBox.Show("��ֹ��ˮ�ű���Ϊ���֣�");
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
                    MessageBox.Show("��ֹ��ˮ�ű���Ϊ���֣�");
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
                if (MessageBox.Show("�޸Ĵ�ӡ��ʽ������ұ����д���ӡ�ļ�¼��ȷ���޸Ĵ�ӡ��ʽ��", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            //�Զ��������������ʷ��Ϣ�������ӡ���������ж����ֻȡ��һ�����������ʷ��Ϣ
            int SNLength = 0;
            int _snB = 1;
            try
            {
                _snB = Convert.ToInt32(txtSNB.Text);
            }
            catch
            {
                MessageBox.Show("��������ˮ�ţ�");
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
            _boxBarDs = _bps.GetBoxBarPrint(_barNO + _initNo);//ȡ������
            if (_boxBarDs != null && _boxBarDs.Tables.Count > 0 && _boxBarDs.Tables[0].Rows.Count > 0)
            {
                _drBox = _boxBarDs.Tables[0].Rows[0];
                txtPrdNo.Text = _drBox["PRD_NO"].ToString();
                txtSpc.Text = _drBox["SPC"].ToString();
                txtIdxNo.Text = _drBox["IDX1"].ToString();//������ţ����ã�����txtPrintNameѡ��ʱ��������
                txtIdx1.Text = _drBox["IDX_UP"].ToString();//�������Ĵ�ӡ����
                txtBatNo.Text = _drBox["BAT_NO"].ToString();
                txtPrdQty.Text = _drBox["QTY"].ToString();
                txtPrintName.Text = _drBox["IDX_NAME"].ToString();//�����Ĵ�ӡ���� 
                txtPrdBH.Text = _drBox["LH"].ToString();//��Ʒ���
                txtPO.Text = "";
                if (cbFormat.SelectedIndex == 4)
                {
                    //��ʿ����ǩ��Ϣ
                    txtSup.Text = "��Ʒ";//��Ӧ��
                    txtCZ.Text = Strings.StrConv(_drBox["CZ_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);//����
                    txtPrdName.Text = "Ƭ��";//Ʒ��
                    txtBC.Text = Strings.StrConv(_drBox["BC_GP"].ToString(), VbStrConv.SimplifiedChinese, 2);//���
                    txtFoxLH.Text = Strings.StrConv(_drBox["LH_FOXCONN"].ToString(), VbStrConv.SimplifiedChinese, 2);//Foxconn�Ϻ�
                    txtBatManu.Text = "";//��������
                    txtBatMat.Text = "";//ԭ������
                    decimal _qtyPer = 0;
                    decimal _qty = 0;
                    if (!String.IsNullOrEmpty(_drBox["QTY_PER_GP"].ToString()))
                        _qtyPer = Convert.ToDecimal(_drBox["QTY_PER_GP"]);
                    if (!String.IsNullOrEmpty(_drBox["QTY"].ToString()))
                        _qty = Convert.ToDecimal(_drBox["QTY"]) * _qtyPer;
                    txtWeightPer.Text = _qtyPer.ToString();
                    txtWeight.Text = String.Format("{0:F2}", _qty); ; //����

                    txtXingHao.Text = _drBox["KEHUPINHAO"].ToString();
                }
            }
            else
            {
                MessageBox.Show("�����벻���ڣ�");
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
                MessageBox.Show("����ѡ��Ʒ�ţ�");
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
                MessageBox.Show("����ѡ��Ʒ�ţ�");
            }
        }
	}
}