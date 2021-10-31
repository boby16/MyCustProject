using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BCInitMM : Form
    {
        private DataSet _dsMM = new DataSet();//存储送缴条码
        private DataSet _dsUnMM = new DataSet();//存储待送缴条码
        private DataSet _dsBarCode = new DataSet();
        private string _whNameTem = "";

        public BCInitMM()
        {
            InitializeComponent();
        }

        private void BCInitMM_Load(object sender, EventArgs e)
        {
            txtSalNo.Text = BarRole.USR_NO;
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _dsUnMM = _bps.GetBCUnMMLst("");//取得送缴的序列号

            lstUnMM.DataSource = _dsUnMM.Tables[0];
            lstUnMM.DisplayMember = "BAR_NO_DIS";
            lstUnMM.ValueMember = "BAR_NO";
            lstUnMM.Refresh();

            _dsMM = _dsUnMM.Clone();
        }

        private void toMM()
        {
            DataSet _ds = new DataSet();

            //记录缴库信息（临时表），包括表头、表身
            DataTable _dt = new DataTable("TF_TY_EXPORT");
            _dt.Columns.Add("MM_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("QTY_OK", System.Type.GetType("System.Decimal"));
            _dt.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("PRD_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
            _dt.Columns.Add("UNIT", System.Type.GetType("System.String"));
            _dt.Columns.Add("DEP", System.Type.GetType("System.String"));
            _dt.Columns.Add("SPC_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("PRC_ID", System.Type.GetType("System.String"));
            _dt.Columns.Add("PRC_ID2", System.Type.GetType("System.String"));

            _dt.Columns.Add("TY_ID", System.Type.GetType("System.String"));
            _dt.Columns.Add("TY_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("MO_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("ID_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("USR_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("WH", System.Type.GetType("System.String"));
            _dt.Columns.Add("USR", System.Type.GetType("System.String"));

            _dt.Columns.Add("PRE_ITM", System.Type.GetType("System.Int32"));
            _dt.Columns.Add("BAT_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("REM", System.Type.GetType("System.String"));
            _ds.Tables.Add(_dt);

            //缴库条码信息（临时表）
            DataTable _dtBar = new DataTable("TOMM_BAR");
            _dtBar.Columns.Add("MM_NO", System.Type.GetType("System.String"));
            _dtBar.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
            _dtBar.Columns.Add("PRD_MARK", System.Type.GetType("System.String"));
            _dtBar.Columns.Add("BAR_CODE", System.Type.GetType("System.String"));
            _dtBar.Columns.Add("BOX_NO", System.Type.GetType("System.String"));
            _ds.Tables.Add(_dtBar);

            string _mmNo = "";
            DataRow _drNew = null;

            foreach (DataRow dr in _dsMM.Tables["BAR_REC"].Rows)
            {
                //系统不采用批号
                //DataRow[] _drChk = _ds.Tables["TF_TY_EXPORT"].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "' AND ISNULL(PRD_MARK,'')='" + dr["PRD_MARK"].ToString() + "' AND BAT_NO='" + dr["BAT_NO"].ToString() + "'");

                DataRow[] _drChk = _ds.Tables["TF_TY_EXPORT"].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "' AND ISNULL(PRD_MARK,'')='" + dr["PRD_MARK"].ToString() + "' ");
                if (_drChk.Length > 0)
                {
                    _drChk[0]["QTY_OK"] = Convert.ToInt32(_drChk[0]["QTY_OK"]) + 1;//缴库数量累加1
                }
                else
                {
                    //缴库信息（表头、表身）
                    _drNew = _dt.NewRow();
                    _drNew["MM_NO"] = _mmNo;
                    _drNew["QTY_OK"] = 1;
                    _drNew["PRD_NO"] = dr["PRD_NO"];
                    _drNew["PRD_NAME"] = dr["PRD_NAME"];
                    _drNew["PRD_MARK"] = dr["PRD_MARK"].ToString();
                    _drNew["UNIT"] = "1";
                    //_drNew["BAT_NO"] = dr["BAT_NO"];//系统不采用批号
                    _drNew["USR"] = BarRole.USR_NO;
                    _drNew["USR_NO"] = txtSalNo.Text;
                    _drNew["DEP"] = BarRole.DEP;
                    _drNew["MO_NO"] = DBNull.Value;//期初条码入库时没有制令单
                    _drNew["ID_NO"] = DBNull.Value;
                    _drNew["WH"] = txtWH.Text;
                    _drNew["REM"] = DBNull.Value;
                    _dt.Rows.Add(_drNew);
                }

                //缴库序列号记录
                _drNew = _dtBar.NewRow();
                _drNew["MM_NO"] = _mmNo;
                _drNew["PRD_NO"] = dr["PRD_NO"];
                _drNew["PRD_MARK"] = dr["PRD_MARK"].ToString();
                _drNew["BAR_CODE"] = dr["BAR_NO"];
                //_drNew["BOX_NO"] = dr["BOX_NO"];//由于是板材条码，所以没有箱条码
                _dtBar.Rows.Add(_drNew);
            }
            try
            {
                //_ds.Merge(_dsBarCode.Tables["BAR_ELIGIBLE"]);
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                _bps.UpdatePassData(_ds);

                MessageBox.Show("转单成功！");
                _dsMM.Clear();
                _dsMM.AcceptChanges();

                lstMM.DataSource = _dsMM.Tables[0];
                lstMM.DisplayMember = "BAR_NO_DIS";
                lstMM.ValueMember = "BAR_NO";
                lstMM.Refresh();

                txtWH.Text = "";
                txtWhName.Text = "";
            }
            catch (Exception _ex)
            {
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("转单出错，原因：\n" + _err);
                _ds.Clear();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtWH.Text == "")
                MessageBox.Show("请选择库位！");
            else
            {
                toMM();
            }
        }

        #region 库位
        private void btnWH_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "MY_WH";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtWH.Text = _qw.NO_RT;
                txtWhName.Text = _qw.NAME_RTN;
            }
        }

        private void txtWhName_Enter(object sender, EventArgs e)
        {
            _whNameTem = txtWhName.Text;
            txtWhName.Text = txtWH.Text;
        }

        private void txtWhName_Leave(object sender, EventArgs e)
        {
            if (txtWhName.Text != txtWH.Text)
            {
                if (txtWhName.Text != "")
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    DataSet _dsWh = _bps.GetWh(BarRole.USR_NO, txtWhName.Text);
                    if (_dsWh != null && _dsWh.Tables.Count > 0 && _dsWh.Tables[0].Rows.Count > 0)
                    {
                        txtWH.Text = _dsWh.Tables[0].Rows[0]["WH"].ToString();
                        txtWhName.Text = _dsWh.Tables[0].Rows[0]["NAME"].ToString();
                    }
                    else
                    {
                        txtWH.Text = "";
                        txtWhName.Text = "";
                        MessageBox.Show("代号不存在！");
                    }
                }
                else
                {
                    txtWH.Text = "";
                    txtWhName.Text = "";
                }
            }
            else
            {
                txtWhName.Text = _whNameTem;
            }
            _whNameTem = "";
        }
        #endregion

        private void btnTomm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrMM = _dsMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrMM)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsMM.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["BOX_NO"] = _dr["BOX_NO"];
                    _drNew["STOP_ID"] = _dr["STOP_ID"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["REM"] = _dr["REM"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];

                    _dsMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnMM.AcceptChanges();
                _dsMM.AcceptChanges();

                lstUnMM.DataSource = _dsUnMM.Tables[0];
                lstUnMM.DisplayMember = "BAR_NO_DIS";
                lstUnMM.ValueMember = "BAR_NO";

                lstMM.DataSource = _dsMM.Tables[0];
                lstMM.DisplayMember = "BAR_NO_DIS";
                lstMM.ValueMember = "BAR_NO";
            }
        }

        private void btnUnmm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsMM.Tables[0].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnMM.Tables[0].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["BOX_NO"] = _dr["BOX_NO"];
                    _drNew["STOP_ID"] = _dr["STOP_ID"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["PRD_MARK"] = _dr["PRD_MARK"];
                    _drNew["PRD_NAME"] = _dr["PRD_NAME"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["REM"] = _dr["REM"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];

                    _dsUnMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnMM.AcceptChanges();
                _dsMM.AcceptChanges();

                lstUnMM.DataSource = _dsUnMM.Tables[0];
                lstUnMM.DisplayMember = "BAR_NO_DIS";
                lstUnMM.ValueMember = "BAR_NO";

                lstMM.DataSource = _dsMM.Tables[0];
                lstMM.DisplayMember = "BAR_NO_DIS";
                lstMM.ValueMember = "BAR_NO";
            }
        }
    }
}