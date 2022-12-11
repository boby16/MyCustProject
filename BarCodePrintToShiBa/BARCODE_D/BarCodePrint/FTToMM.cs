using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class FTToMM : Form
    {
        private DataSet _dsBoxToMM = new DataSet();//存储缴库箱条码
        private DataSet _dsBoxUnToMM = new DataSet();//存储待缴库箱条码
        private DataSet _dsMo = new DataSet();
        private string _whNameTem = "";

        /// <summary>
        /// 装箱
        /// </summary>
        public FTToMM()
        {
            InitializeComponent();
        }

        private void FTToMM_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            //取得BAR_BOX记录
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            //取得未缴库已装箱箱条码
            _dsBoxUnToMM = _bps.GetFTUnToMMLst();

            DataView _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
            _dv.RowFilter = "QC='A' or QC='B'";
            DataTable _dt = _dv.ToTable();

            //dgBoxUnToMM.DataMember = "BAR_BOX";
            dgBoxUnToMM.DataSource = _dt;
            dgBoxUnToMM.Refresh();

            _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
            _dv.RowFilter = "QC='2'";
            _dt = _dv.ToTable();

            //dgBoxUnToMMR.DataMember = "BAR_BOX";
            dgBoxUnToMMR.DataSource = _dt;
            dgBoxUnToMMR.Refresh();

            _dsBoxToMM = _dsBoxUnToMM.Clone();

            _dsBoxToMM.Tables["BAR_BOX"].Columns.Add("USR_SJ", System.Type.GetType("System.String"));
            _dsBoxToMM.Tables["BAR_BOX"].Columns.Add("SJ_DATE", System.Type.GetType("System.DateTime"));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //decimal _qty = 0;
            //decimal _qtyFin = 0;
            //decimal _qtyLost = 0;
            //decimal _qtyYI = 0;
            //if (txtQty.Text != "")
            //    _qty = Convert.ToDecimal(txtQty.Text);
            //if (txtQtyFin.Text != "")
            //    _qtyFin = Convert.ToDecimal(txtQtyFin.Text);
            //if (txtQtyLost.Text != "")
            //    _qtyLost = Convert.ToDecimal(txtQtyLost.Text);
            //if (txtYIQty.Text != "")
            //    _qtyYI = Convert.ToDecimal(txtYIQty.Text);
            //if (_qty > (_qtyYI - _qtyFin - _qtyLost))
            //    MessageBox.Show("货品已超交！");

            if (txtMONo.Text == "")//暂时制令单不能为空
                MessageBox.Show("请选择制令单！");
            else if (txtWH.Text == "")
                MessageBox.Show("请选择库位！");
            else if (_dsBoxToMM == null || _dsBoxToMM.Tables.Count <= 0 || _dsBoxToMM.Tables[0].Rows.Count <= 0)
                MessageBox.Show("请选择送缴的箱条码！");
            else
            {
                BarSet _bs = new BarSet();
                try
                {
                    foreach (DataRow _dr in this._dsBoxToMM.Tables[0].Rows)
                    {
                        //控制品号1、3、4位，长度和宽度不能超过制令单的制成品的长度和宽度    2009-06-10
                        //应客户（侯课）要求，去掉【控制品号1、3、4位】的功能，改回为原来的1、2、3、4位都控制到       2010-04-02
                        if (txtPrdNo.Text.Substring(0, 4) != _dr["PRD_NO"].ToString().Substring(0, 4)
                            || _bs.CharacterToNum(txtPrdNo.Text.Substring(9, 3)) < _bs.CharacterToNum(_dr["PRD_NO"].ToString().Substring(9, 3))
                            || Convert.ToInt32(txtPrdNo.Text.Substring(4, 5)) < Convert.ToInt32(_dr["PRD_NO"].ToString().Substring(4, 5)))
                        {
                            MessageBox.Show("送缴的货品[" + _dr["PRD_NO"].ToString() + "]与制令单货品[" + txtPrdNo.Text + "]不符，请检查！");
                            return;
                        }
                        _dr["STATUS"] = "SW";
                        _dr["BIL_ID"] = "MO";
                        _dr["BIL_NO"] = txtMONo.Text;
                        _dr["USR_SJ"] = BarRole.USR_NO;
                        _dr["SJ_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        _dr["WH_SJ"] = txtWH.Text;
                    }
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    _bps.UpdataDataBoxBat(_dsBoxToMM);

                    MessageBox.Show("送缴成功！");

                    _dsBoxToMM.Clear();
                    _dsBoxToMM.AcceptChanges();
                    _dsMo.Clear();
                    _dsMo.AcceptChanges();

                    txtMONo.Text = "";
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    txtIdx.Text = "";
                    txtUT.Text = "";
                    txtQty.Text = "";
                    txtBoxQty.Text = "";
                    txtYIQty.Text = "";
                    txtQtyFin.Text = "";
                    txtQtyLost.Text = "";
                    txtWH.Text = "";
                    txtWhName.Text = "";

                }
                catch (Exception _ex)
                {
                    MessageBox.Show("送缴失败！原因：" + _ex.ToString());
                }
            }
        }

        #region 制令单号
        private void txtMONo_Leave(object sender, EventArgs e)
        {
            if (txtMONo.Text != "")
            {
                DataSet _ds = new DataSet();
                string _sqlWhere = "";
                onlineService.BarPrintServer _drs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
                _drs.UseDefaultCredentials = true;
                _sqlWhere += " AND A.MO_NO = '" + txtMONo.Text + "' AND (ISNULL(A.CHK_MAN,'')<>'' AND A.CLS_DATE IS NOT NULL) AND B.KND='2'"
                          + " AND ((select count(*) from TF_ML WHERE MO_NO=A.MO_NO)>0 or (select count(*) from TF_MO WHERE MO_NO=A.MO_NO)=0) ";
                _ds = _drs.QueryMOData(_sqlWhere);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    txtPrdNo.Text = _ds.Tables[0].Rows[0]["PRD_NO"].ToString();
                    txtSpc.Text = _ds.Tables[0].Rows[0]["SPC"].ToString();
                    txtIdx.Text = _ds.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    txtUT.Text = _ds.Tables[0].Rows[0]["UT"].ToString();
                    if (_ds.Tables[0].Rows[0]["QTY"].ToString() != "")
                        txtYIQty.Text = String.Format("{0:F2}", Convert.ToDecimal(_ds.Tables[0].Rows[0]["QTY"]));
                    else
                        txtYIQty.Text = "";
                    if (_ds.Tables[0].Rows[0]["QTY_FIN"].ToString() != "")
                        txtQtyFin.Text = String.Format("{0:F2}", Convert.ToDecimal(_ds.Tables[0].Rows[0]["QTY_FIN"]));
                    else
                        txtQtyFin.Text = "";
                    if (_ds.Tables[0].Rows[0]["QTY_LOST"].ToString() != "")
                        txtQtyLost.Text = String.Format("{0:F2}", Convert.ToDecimal(_ds.Tables[0].Rows[0]["QTY_LOST"]));
                    else
                        txtQtyLost.Text = "";
                    txtWH.Text = _ds.Tables[0].Rows[0]["WH"].ToString();
                    txtWhName.Text = _ds.Tables[0].Rows[0]["WH_NAME"].ToString();
                }
                else
                {
                    txtMONo.Text = "";
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    txtIdx.Text = "";
                    txtUT.Text = "";
                    txtQty.Text = "";
                    txtBoxQty.Text = "";
                    txtYIQty.Text = "";
                    txtQtyFin.Text = "";
                    txtQtyLost.Text = "";
                    txtWH.Text = "";
                    txtWhName.Text = "";
                    if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                    {
                        _dsMo.Clear();
                        _dsMo.AcceptChanges();
                    }
                }
            }
            else
            {
                txtPrdNo.Text = "";
                txtSpc.Text = "";
                txtIdx.Text = "";
                txtUT.Text = "";
                txtQty.Text = "";
                txtBoxQty.Text = "";
                txtYIQty.Text = "";
                txtQtyFin.Text = "";
                txtQtyLost.Text = "";
                txtWH.Text = "";
                txtWhName.Text = "";
                if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                {
                    _dsMo.Clear();
                    _dsMo.AcceptChanges();
                }
            }
        }

        private void btnMONo_Click(object sender, EventArgs e)
        {
            QueryMo _qm = new QueryMo();
            _qm.SQLWhere = " AND (ISNULL(A.CHK_MAN,'')<>'' AND A.CLS_DATE IS NOT NULL) AND B.KND='2' AND ((select count(*) from TF_ML WHERE MO_NO=A.MO_NO)>0 or (select count(*) from TF_MO WHERE MO_NO=A.MO_NO)=0) ";
            if (_qm.ShowDialog() == DialogResult.OK)
            {
                _dsMo = _qm.MoDS;
                if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                {
                    txtMONo.Text = _dsMo.Tables[0].Rows[0]["MO_NO"].ToString();
                    txtPrdNo.Text = _dsMo.Tables[0].Rows[0]["PRD_NO"].ToString();
                    txtSpc.Text = _dsMo.Tables[0].Rows[0]["SPC"].ToString();
                    txtIdx.Text = _dsMo.Tables[0].Rows[0]["IDX_NAME"].ToString();
                    txtUT.Text = _dsMo.Tables[0].Rows[0]["UT"].ToString();
                    if (_dsMo.Tables[0].Rows[0]["QTY"].ToString() != "")
                        txtYIQty.Text = String.Format("{0:F2}", Convert.ToDecimal(_dsMo.Tables[0].Rows[0]["QTY"]));
                    else
                        txtYIQty.Text = "";
                    if (_dsMo.Tables[0].Rows[0]["QTY_FIN"].ToString() != "")
                        txtQtyFin.Text = String.Format("{0:F2}", Convert.ToDecimal(_dsMo.Tables[0].Rows[0]["QTY_FIN"]));
                    else
                        txtQtyFin.Text = "";
                    if (_dsMo.Tables[0].Rows[0]["QTY_LOST"].ToString() != "")
                        txtQtyLost.Text = String.Format("{0:F2}", Convert.ToDecimal(_dsMo.Tables[0].Rows[0]["QTY_LOST"]));
                    else
                        txtQtyLost.Text = "";
                    txtWH.Text = _dsMo.Tables[0].Rows[0]["WH"].ToString();
                    txtWhName.Text = _dsMo.Tables[0].Rows[0]["WH_NAME"].ToString();
                }
                else
                {
                    txtMONo.Text = "";
                    txtPrdNo.Text = "";
                    txtSpc.Text = "";
                    txtIdx.Text = "";
                    txtUT.Text = "";
                    txtQty.Text = "";
                    txtBoxQty.Text = "";
                    txtYIQty.Text = "";
                    txtQtyFin.Text = "";
                    txtQtyLost.Text = "";
                    txtWH.Text = "";
                    txtWhName.Text = "";
                    if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                    {
                        _dsMo.Clear();
                        _dsMo.AcceptChanges();
                    }
                }
            }
        }
        #endregion

        //选择
        private void btnTomm_Click(object sender, EventArgs e)
        {
            DataSet _dsTemp = new DataSet();
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < dgBoxUnToMM.SelectedRows.Count; i++)
            {
                _drv = (DataRowView)dgBoxUnToMM.SelectedRows[i].DataBoundItem;
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BOX_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsBoxUnToMM.Tables[0].Select("BOX_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsBoxToMM.Tables[0].Select("BOX_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrE)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsBoxToMM.Tables[0].NewRow();
                    _drNew["BOX_NO"] = _dr["BOX_NO"];
                    _drNew["BOX_NO_DIS"] = _dr["BOX_NO_DIS"];
                    _drNew["BOX_DD"] = _dr["BOX_DD"];
                    _drNew["DEP"] = _dr["DEP"];
                    _drNew["QTY"] = _dr["QTY"];
                    _drNew["CONTENT"] = _dr["CONTENT"];
                    _drNew["USR"] = _dr["USR"];
                    _drNew["WH"] = _dr["WH"];
                    _drNew["PH_FLAG"] = _dr["PH_FLAG"];
                    _drNew["STOP_ID"] = _dr["STOP_ID"];
                    _drNew["VALID_DD"] = _dr["VALID_DD"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["IDX1"] = _dr["IDX1"];
                    _drNew["IDX_NAME"] = _dr["IDX_NAME"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];
                    _drNew["WH_SJ"] = _dr["WH_SJ"];
                    _drNew["REM"] = _dr["REM"];
                    _dsBoxToMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsBoxUnToMM.AcceptChanges();
                _dsBoxToMM.AcceptChanges();

                DataView _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
                _dv.RowFilter = "QC='A' or QC='B'";
                DataTable _dt = _dv.ToTable();

                //dgBoxUnToMM.DataMember = "BAR_BOX";
                dgBoxUnToMM.DataSource = _dt;
                dgBoxUnToMM.Refresh();

                _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
                _dv.RowFilter = "QC='2'";
                _dt = _dv.ToTable();

                //dgBoxUnToMMR.DataMember = "BAR_BOX";
                dgBoxUnToMMR.DataSource = _dt;
                dgBoxUnToMMR.Refresh();

                dgBoxToMM.DataMember = "BAR_BOX";
                dgBoxToMM.DataSource = _dsBoxToMM;
                dgBoxToMM.Refresh();

                decimal _qty = 0;
                foreach (DataRow _dr in _dsBoxToMM.Tables[0].Rows)
                {
                    if (_dr["QTY"].ToString() != "")
                        _qty += Convert.ToDecimal(_dr["QTY"]);
                }
                txtQty.Text = String.Format("{0:F2}", Convert.ToDecimal(_qty));
                txtBoxQty.Text = _dsBoxToMM.Tables[0].Rows.Count.ToString();
            }
        }

        //反选
        private void btnUnmm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < dgBoxToMM.SelectedRows.Count; i++)
            {
                _drv = (DataRowView)dgBoxToMM.SelectedRows[i].DataBoundItem;
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BOX_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsBoxUnToMM.Tables[0].Select("BOX_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsBoxToMM.Tables[0].Select("BOX_NO in (" + _selectNo + ")");

                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsBoxUnToMM.Tables[0].NewRow();
                    _drNew["BOX_NO"] = _dr["BOX_NO"];
                    _drNew["BOX_NO_DIS"] = _dr["BOX_NO_DIS"];
                    _drNew["BOX_DD"] = _dr["BOX_DD"];
                    _drNew["DEP"] = _dr["DEP"];
                    _drNew["QTY"] = _dr["QTY"];
                    _drNew["CONTENT"] = _dr["CONTENT"];
                    _drNew["USR"] = _dr["USR"];
                    _drNew["WH"] = _dr["WH"];
                    _drNew["PH_FLAG"] = _dr["PH_FLAG"];
                    _drNew["STOP_ID"] = _dr["STOP_ID"];
                    _drNew["VALID_DD"] = _dr["VALID_DD"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["IDX1"] = _dr["IDX1"];
                    _drNew["IDX_NAME"] = _dr["IDX_NAME"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];
                    _drNew["WH_SJ"] = _dr["WH_SJ"];
                    _drNew["REM"] = _dr["REM"];
                    _dsBoxUnToMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsBoxUnToMM.AcceptChanges();
                _dsBoxToMM.AcceptChanges();

                DataView _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
                _dv.RowFilter = "QC='A' or QC='B'";
                DataTable _dt = _dv.ToTable();

                //dgBoxUnToMM.DataMember = "BAR_BOX";
                dgBoxUnToMM.DataSource = _dt;
                dgBoxUnToMM.Refresh();

                _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
                _dv.RowFilter = "QC='2'";
                _dt = _dv.ToTable();

                //dgBoxUnToMMR.DataMember = "BAR_BOX";
                dgBoxUnToMMR.DataSource = _dt;
                dgBoxUnToMMR.Refresh();

                dgBoxToMM.DataMember = "BAR_BOX";
                dgBoxToMM.DataSource = _dsBoxToMM;
                dgBoxToMM.Refresh();

                decimal _qty = 0;
                foreach (DataRow _dr in _dsBoxToMM.Tables[0].Rows)
                {
                    if (_dr["QTY"].ToString() != "")
                        _qty += Convert.ToDecimal(_dr["QTY"]);
                }
                txtQty.Text = String.Format("{0:F2}", Convert.ToDecimal(_qty));
                txtBoxQty.Text = _dsBoxToMM.Tables[0].Rows.Count.ToString();
            }
        }

        #region 库位
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
        #endregion

        private void dgBoxUnToMM_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBoxUnToMM.Columns["BOX_NO"].Visible = false;
            dgBoxUnToMM.Columns["BOX_DD"].Visible = false;
            dgBoxUnToMM.Columns["DEP"].Visible = false;
            dgBoxUnToMM.Columns["CONTENT"].Visible = false;
            dgBoxUnToMM.Columns["USR"].Visible = false;
            dgBoxUnToMM.Columns["WH"].Visible = false;
            dgBoxUnToMM.Columns["PH_FLAG"].Visible = false;
            dgBoxUnToMM.Columns["STOP_ID"].Visible = false;
            dgBoxUnToMM.Columns["VALID_DD"].Visible = false;
            dgBoxUnToMM.Columns["IDX_UP"].Visible = false;
            dgBoxUnToMM.Columns["IDX1"].Visible = false;
            dgBoxUnToMM.Columns["QC"].Visible = false;
            dgBoxUnToMM.Columns["STATUS"].Visible = false;
            dgBoxUnToMM.Columns["BIL_ID"].Visible = false;
            dgBoxUnToMM.Columns["BIL_NO"].Visible = false;
            dgBoxUnToMM.Columns["WH_SJ"].Visible = false;
            dgBoxUnToMM.Columns["REM"].Visible = false;

            dgBoxUnToMM.Columns["BOX_NO_DIS"].HeaderText = "箱码";
            dgBoxUnToMM.Columns["QTY"].HeaderText = "数量";
            dgBoxUnToMM.Columns["PRD_NO"].HeaderText = "品号";
            dgBoxUnToMM.Columns["BAT_NO"].HeaderText = "批号";
            dgBoxUnToMM.Columns["SPC"].HeaderText = "规格";
            dgBoxUnToMM.Columns["IDX_NAME"].HeaderText = "中类";

            dgBoxUnToMM.Columns["BOX_NO_DIS"].DisplayIndex = 0;
            dgBoxUnToMM.Columns["IDX_NAME"].DisplayIndex = 1;
            dgBoxUnToMM.Columns["SPC"].DisplayIndex = 2;
            dgBoxUnToMM.Columns["BAT_NO"].DisplayIndex = 3;
            dgBoxUnToMM.Columns["PRD_NO"].DisplayIndex = 4;
            dgBoxUnToMM.Columns["QTY"].DisplayIndex = 5;

            dgBoxUnToMM.RowHeadersWidth = 20;
        }

        private void dgBoxToMM_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBoxToMM.Columns["BOX_NO"].Visible = false;
            dgBoxToMM.Columns["BOX_DD"].Visible = false;
            dgBoxToMM.Columns["DEP"].Visible = false;
            dgBoxToMM.Columns["CONTENT"].Visible = false;
            dgBoxToMM.Columns["USR"].Visible = false;
            dgBoxToMM.Columns["WH"].Visible = false;
            dgBoxToMM.Columns["PH_FLAG"].Visible = false;
            dgBoxToMM.Columns["STOP_ID"].Visible = false;
            dgBoxToMM.Columns["VALID_DD"].Visible = false;
            dgBoxToMM.Columns["IDX_UP"].Visible = false;
            dgBoxToMM.Columns["IDX1"].Visible = false;
            dgBoxToMM.Columns["QC"].Visible = false;
            dgBoxToMM.Columns["STATUS"].Visible = false;
            dgBoxToMM.Columns["BIL_ID"].Visible = false;
            dgBoxToMM.Columns["BIL_NO"].Visible = false;
            dgBoxToMM.Columns["REM"].Visible = false;
            dgBoxToMM.Columns["USR_SJ"].Visible = false;
            dgBoxToMM.Columns["SJ_DATE"].Visible = false;
            dgBoxToMM.Columns["WH_SJ"].Visible = false;

            dgBoxToMM.Columns["BOX_NO_DIS"].HeaderText = "箱码";
            dgBoxToMM.Columns["QTY"].HeaderText = "数量";
            dgBoxToMM.Columns["PRD_NO"].HeaderText = "品号";
            dgBoxToMM.Columns["BAT_NO"].HeaderText = "批号";
            dgBoxToMM.Columns["SPC"].HeaderText = "规格";
            dgBoxToMM.Columns["IDX_NAME"].HeaderText = "中类";

            dgBoxToMM.Columns["BOX_NO_DIS"].DisplayIndex = 0;
            dgBoxToMM.Columns["IDX_NAME"].DisplayIndex = 1;
            dgBoxToMM.Columns["SPC"].DisplayIndex = 2;
            dgBoxToMM.Columns["BAT_NO"].DisplayIndex = 3;
            dgBoxToMM.Columns["PRD_NO"].DisplayIndex = 4;
            dgBoxToMM.Columns["QTY"].DisplayIndex = 5;

            dgBoxToMM.RowHeadersWidth = 20;
        }

        private void dgBoxUnToMMR_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgBoxUnToMMR.Columns["BOX_NO"].Visible = false;
            dgBoxUnToMMR.Columns["BOX_DD"].Visible = false;
            dgBoxUnToMMR.Columns["DEP"].Visible = false;
            dgBoxUnToMMR.Columns["CONTENT"].Visible = false;
            dgBoxUnToMMR.Columns["USR"].Visible = false;
            dgBoxUnToMMR.Columns["WH"].Visible = false;
            dgBoxUnToMMR.Columns["PH_FLAG"].Visible = false;
            dgBoxUnToMMR.Columns["STOP_ID"].Visible = false;
            dgBoxUnToMMR.Columns["VALID_DD"].Visible = false;
            dgBoxUnToMMR.Columns["IDX_UP"].Visible = false;
            dgBoxUnToMMR.Columns["IDX1"].Visible = false;
            dgBoxUnToMMR.Columns["QC"].Visible = false;
            dgBoxUnToMMR.Columns["STATUS"].Visible = false;
            dgBoxUnToMMR.Columns["BIL_ID"].Visible = false;
            dgBoxUnToMMR.Columns["BIL_NO"].Visible = false;
            dgBoxUnToMMR.Columns["WH_SJ"].Visible = false;
            dgBoxUnToMMR.Columns["REM"].Visible = false;

            dgBoxUnToMMR.Columns["BOX_NO_DIS"].HeaderText = "箱码";
            dgBoxUnToMMR.Columns["QTY"].HeaderText = "数量";
            dgBoxUnToMMR.Columns["PRD_NO"].HeaderText = "品号";
            dgBoxUnToMMR.Columns["BAT_NO"].HeaderText = "批号";
            dgBoxUnToMMR.Columns["SPC"].HeaderText = "规格";
            dgBoxUnToMMR.Columns["IDX_NAME"].HeaderText = "中类";

            dgBoxUnToMMR.Columns["BOX_NO_DIS"].DisplayIndex = 0;
            dgBoxUnToMMR.Columns["IDX_NAME"].DisplayIndex = 1;
            dgBoxUnToMMR.Columns["SPC"].DisplayIndex = 2;
            dgBoxUnToMMR.Columns["BAT_NO"].DisplayIndex = 3;
            dgBoxUnToMMR.Columns["PRD_NO"].DisplayIndex = 4;
            dgBoxUnToMMR.Columns["QTY"].DisplayIndex = 5;

            dgBoxUnToMMR.RowHeadersWidth = 20;
        }

        private void btnTommR_Click(object sender, EventArgs e)
        {
            DataSet _dsTemp = new DataSet();
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < dgBoxUnToMMR.SelectedRows.Count; i++)
            {
                _drv = (DataRowView)dgBoxUnToMMR.SelectedRows[i].DataBoundItem;
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BOX_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsBoxUnToMM.Tables[0].Select("BOX_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsBoxToMM.Tables[0].Select("BOX_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrE)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsBoxToMM.Tables[0].NewRow();
                    _drNew["BOX_NO"] = _dr["BOX_NO"];
                    _drNew["BOX_NO_DIS"] = _dr["BOX_NO_DIS"];
                    _drNew["BOX_DD"] = _dr["BOX_DD"];
                    _drNew["DEP"] = _dr["DEP"];
                    _drNew["QTY"] = _dr["QTY"];
                    _drNew["CONTENT"] = _dr["CONTENT"];
                    _drNew["USR"] = _dr["USR"];
                    _drNew["WH"] = _dr["WH"];
                    _drNew["PH_FLAG"] = _dr["PH_FLAG"];
                    _drNew["STOP_ID"] = _dr["STOP_ID"];
                    _drNew["VALID_DD"] = _dr["VALID_DD"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["IDX1"] = _dr["IDX1"];
                    _drNew["IDX_NAME"] = _dr["IDX_NAME"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];
                    _drNew["WH_SJ"] = _dr["WH_SJ"];
                    _drNew["REM"] = _dr["REM"];
                    _dsBoxToMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsBoxUnToMM.AcceptChanges();
                _dsBoxToMM.AcceptChanges();

                DataView _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
                _dv.RowFilter = "QC='A' or QC='B'";
                DataTable _dt = _dv.ToTable();

                //dgBoxUnToMM.DataMember = "BAR_BOX";
                dgBoxUnToMM.DataSource = _dt;
                dgBoxUnToMM.Refresh();

                _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
                _dv.RowFilter = "QC='2'";
                _dt = _dv.ToTable();

                //dgBoxUnToMMR.DataMember = "BAR_BOX";
                dgBoxUnToMMR.DataSource = _dt;
                dgBoxUnToMMR.Refresh();

                dgBoxToMM.DataMember = "BAR_BOX";
                dgBoxToMM.DataSource = _dsBoxToMM;
                dgBoxToMM.Refresh();

                decimal _qty = 0;
                foreach (DataRow _dr in _dsBoxToMM.Tables[0].Rows)
                {
                    if (_dr["QTY"].ToString() != "")
                        _qty += Convert.ToDecimal(_dr["QTY"]);
                }
                txtQty.Text = String.Format("{0:F2}", Convert.ToDecimal(_qty));
                txtBoxQty.Text = _dsBoxToMM.Tables[0].Rows.Count.ToString();
            }
        }

        private void btnUnmmR_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < dgBoxToMM.SelectedRows.Count; i++)
            {
                _drv = (DataRowView)dgBoxToMM.SelectedRows[i].DataBoundItem;
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BOX_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsBoxUnToMM.Tables[0].Select("BOX_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsBoxToMM.Tables[0].Select("BOX_NO in (" + _selectNo + ")");

                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsBoxUnToMM.Tables[0].NewRow();
                    _drNew["BOX_NO"] = _dr["BOX_NO"];
                    _drNew["BOX_NO_DIS"] = _dr["BOX_NO_DIS"];
                    _drNew["BOX_DD"] = _dr["BOX_DD"];
                    _drNew["DEP"] = _dr["DEP"];
                    _drNew["QTY"] = _dr["QTY"];
                    _drNew["CONTENT"] = _dr["CONTENT"];
                    _drNew["USR"] = _dr["USR"];
                    _drNew["WH"] = _dr["WH"];
                    _drNew["PH_FLAG"] = _dr["PH_FLAG"];
                    _drNew["STOP_ID"] = _dr["STOP_ID"];
                    _drNew["VALID_DD"] = _dr["VALID_DD"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["SPC"] = _dr["SPC"];
                    _drNew["IDX1"] = _dr["IDX1"];
                    _drNew["IDX_NAME"] = _dr["IDX_NAME"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];
                    _drNew["WH_SJ"] = _dr["WH_SJ"];
                    _drNew["REM"] = _dr["REM"];
                    _dsBoxUnToMM.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsBoxUnToMM.AcceptChanges();
                _dsBoxToMM.AcceptChanges();

                DataView _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
                _dv.RowFilter = "QC='A' or QC='B'";
                DataTable _dt = _dv.ToTable();

                //dgBoxUnToMM.DataMember = "BAR_BOX";
                dgBoxUnToMM.DataSource = _dt;
                dgBoxUnToMM.Refresh();

                _dv = _dsBoxUnToMM.Tables["BAR_BOX"].DefaultView;
                _dv.RowFilter = "QC='2'";
                _dt = _dv.ToTable();

                //dgBoxUnToMMR.DataMember = "BAR_BOX";
                dgBoxUnToMMR.DataSource = _dt;
                dgBoxUnToMMR.Refresh();

                dgBoxToMM.DataMember = "BAR_BOX";
                dgBoxToMM.DataSource = _dsBoxToMM;
                dgBoxToMM.Refresh();

                decimal _qty = 0;
                foreach (DataRow _dr in _dsBoxToMM.Tables[0].Rows)
                {
                    if (_dr["QTY"].ToString() != "")
                        _qty += Convert.ToDecimal(_dr["QTY"]);
                }
                txtQty.Text = String.Format("{0:F2}", Convert.ToDecimal(_qty));
                txtBoxQty.Text = _dsBoxToMM.Tables[0].Rows.Count.ToString();
            }
        }
    }
}