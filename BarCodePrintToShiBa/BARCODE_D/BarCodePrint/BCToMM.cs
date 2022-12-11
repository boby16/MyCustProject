using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class BCToMM : Form
    {
        private DataSet _dsToMM = new DataSet();//存储送缴条码
        private DataSet _dsUnToMM = new DataSet();//存储待送缴条码
        private DataSet _dsMo = new DataSet();
        private string _whNameTem = "";

        /// <summary>
        /// 装箱
        /// </summary>
        public BCToMM()
        {
            InitializeComponent();
        }

        private void BCToMM_Load(object sender, EventArgs e)
        {
            InitData();
            //cbMoNull.Visible = BarRole.CHK_MONULL;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            //已检验未送缴的条码
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _dsUnToMM = new DataSet();
            //--ISNULL(A.QC,'')='1'---- 合格品
            //--(ISNULL(A.QC,'')='2' AND ISNULL(A.PRC_ID,'')='1'----不合格品强制缴库
            string _where = " AND LEN(REPLACE(A.BAR_NO,'#',''))=22"
                          + " AND (ISNULL(A.QC,'')='1' OR (ISNULL(A.QC,'')='2' AND ISNULL(A.PRC_ID,'')='1'))"
                          + " AND ISNULL(A.STATUS, '')='QC' ORDER BY B.BAT_NO,A.BAR_NO ";
            _dsUnToMM = _bps.GetBCUnToMMLst(_where);

            _dsToMM = _dsUnToMM.Clone();
            _dsToMM.Tables["BAR_QC"].Columns.Add("USR_SJ", System.Type.GetType("System.String"));
            _dsToMM.Tables["BAR_QC"].Columns.Add("SJ_DATE", System.Type.GetType("System.DateTime"));
            _dsToMM.Tables["BAR_QC"].Columns.Add("WH_SJ", System.Type.GetType("System.String"));

            DataView _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
            _dv.RowFilter = "QC='1'";
            DataTable _dt = _dv.ToTable();

            lstUnToMM.DataSource = _dt;
            lstUnToMM.DisplayMember = "BAR_NO_DIS";
            lstUnToMM.ValueMember = "BAR_NO";
            lstUnToMM.Refresh();

            _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
            _dv.RowFilter = "QC='2'";
            _dt = _dv.ToTable();

            lstUnToMMR.DataSource = _dt;
            lstUnToMMR.DisplayMember = "BAR_NO_DIS";
            lstUnToMMR.ValueMember = "BAR_NO";
            lstUnToMMR.Refresh();
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

            //重大修改（2010-10-28）：板材送缴制令单可选可不选
            //if (txtMONo.Text == "" && !cbMoNull.Checked)
            //    MessageBox.Show("请选择制令单！");
            //else 
            if (_dsToMM == null || _dsToMM.Tables.Count <= 0 || _dsToMM.Tables[0].Rows.Count <= 0)
                MessageBox.Show("请选择缴库的序列号！");
            //else if (_qty > (_qtyYI - _qtyFin - _qtyLost))
            //    MessageBox.Show("货品已超交！");
            else
            {
                try
                {
                    BarSet _bs = new BarSet();
                    DataRow _drMo = null;
                    if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                        _drMo = _dsMo.Tables[0].Rows[0];
                    if (_drMo != null)
                    {
                        int _lenth = 0;
                        int _width = 0;
                        foreach (DataRow _dr in _dsToMM.Tables[0].Rows)
                        {
                            //控制品号1、3、4位，长度和宽度不能超过制令单的制成品的长度和宽度   2009-06-10
                            //应客户（侯课）要求，去掉【控制品号1、3、4位】的功能，改回为原来的1、2、3、4位都控制到(2010-04-02)
                            if (_drMo["PRD_NO"].ToString().Substring(0, 4) != _dr["PRD_NO"].ToString().Substring(0, 4)
                                || _bs.CharacterToNum(_drMo["PRD_NO"].ToString().Substring(9, 3)) < _bs.CharacterToNum(_dr["PRD_NO"].ToString().Substring(9, 3))
                                || Convert.ToInt32(_drMo["PRD_NO"].ToString().Substring(4, 5)) < Convert.ToInt32(_dr["PRD_NO"].ToString().Substring(4, 5)))
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
                        _bps.UpdataDataQC(_dsToMM);

                        MessageBox.Show("送缴成功！");

                        _dsToMM.Clear();
                        _dsToMM.AcceptChanges();
                        lstToMM.DataSource = _dsToMM.Tables[0];
                        lstToMM.DisplayMember = "BAR_NO_DIS";
                        lstToMM.ValueMember = "BAR_NO";

                        txtMONo.Text = "";
                        txtPrdNo.Text = "";
                        txtSpc.Text = "";
                        txtIdx.Text = "";
                        txtUT.Text = "";
                        txtQty.Text = "";
                        txtYIQty.Text = "";
                        txtQtyFin.Text = "";
                        txtQtyLost.Text = "";
                        txtWH.Text = "";
                        txtWhName.Text = "";
                    }
                    else
                    {
                        //重大修改（2010-10-28）：板材送缴制令单可选可不选
                        //if (!cbMoNull.Checked)
                        //{
                        //    //制令单不允许为空
                        //    MessageBox.Show("制令单输入有误，请重新选择制令单！");
                        //    return;
                        //}
                        //else
                        {
                            foreach (DataRow _dr in _dsToMM.Tables[0].Rows)
                            {
                                _dr["STATUS"] = "SW";
                                _dr["USR_SJ"] = BarRole.USR_NO;
                                _dr["SJ_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                _dr["WH_SJ"] = txtWH.Text;
                            }
                            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                            _bps.UseDefaultCredentials = true;
                            _bps.UpdataDataQC(_dsToMM);

                            MessageBox.Show("送缴成功！");

                            _dsToMM.Clear();
                            _dsToMM.AcceptChanges();
                            _dsMo.Clear();
                            _dsMo.AcceptChanges();
                            lstToMM.DataSource = _dsToMM.Tables[0];
                            lstToMM.DisplayMember = "BAR_NO_DIS";
                            lstToMM.ValueMember = "BAR_NO";

                            txtMONo.Text = "";
                            txtPrdNo.Text = "";
                            txtSpc.Text = "";
                            txtIdx.Text = "";
                            txtUT.Text = "";
                            txtQty.Text = "";
                            txtYIQty.Text = "";
                            txtQtyFin.Text = "";
                            txtQtyLost.Text = "";
                            txtWH.Text = "";
                            txtWhName.Text = "";
                        }
                    }
                }
                catch (Exception _ex)
                {
                    string _err = "";
                    if (_ex.Message.Length > 500)
                        _err = _ex.Message.Substring(0, 500);
                    else
                        _err = _ex.Message;
                    MessageBox.Show("送缴失败！原因：\n" + _err);
                }
            }
        }

        #region 制令单号
        private void txtMONo_Leave(object sender, EventArgs e)
        {
            if (txtMONo.Text != "")
            {
                //DataSet _ds = new DataSet();
                string _sqlWhere = "";
                onlineService.BarPrintServer _drs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
                _drs.UseDefaultCredentials = true;
                _sqlWhere += " AND A.MO_NO = '" + txtMONo.Text + "' AND (ISNULL(A.CHK_MAN,'')<>'' AND A.CLS_DATE IS NOT NULL) AND B.KND='3'"
                          + " AND ((select count(*) from TF_ML WHERE MO_NO=A.MO_NO)>0 or (select count(*) from TF_MO WHERE MO_NO=A.MO_NO)=0) ";
                _dsMo = _drs.QueryMOData(_sqlWhere);
                if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                {
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
            _qm.SQLWhere = " AND (ISNULL(A.CHK_MAN,'')<>'' AND A.CLS_DATE IS NOT NULL) AND B.KND='3' AND ((select count(*) from TF_ML WHERE MO_NO=A.MO_NO)>0 or (select count(*) from TF_MO WHERE MO_NO=A.MO_NO)=0) ";
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
                    txtYIQty.Text = "";
                    txtQtyFin.Text = "";
                    txtQtyLost.Text = "";
                    txtWH.Text = "";
                    txtWhName.Text = "";
                }
            }
        }
        #endregion

        //选择
        private void btnTomm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnToMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnToMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnToMM.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrMM = _dsToMM.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrMM)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsToMM.Tables["BAR_QC"].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["REM"] = _dr["REM"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["WH"] = _dr["WH"];

                    _dsToMM.Tables["BAR_QC"].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnToMM.AcceptChanges();
                _dsToMM.AcceptChanges();

                DataView _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
                _dv.RowFilter = "QC='1'";
                DataTable _dt = _dv.ToTable();

                lstUnToMM.DataSource = _dt;
                lstUnToMM.DisplayMember = "BAR_NO_DIS";
                lstUnToMM.ValueMember = "BAR_NO";
                lstUnToMM.Refresh();

                _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
                _dv.RowFilter = "QC='2'";
                _dt = _dv.ToTable();

                lstUnToMMR.DataSource = _dt;
                lstUnToMMR.DisplayMember = "BAR_NO_DIS";
                lstUnToMMR.ValueMember = "BAR_NO";
                lstUnToMMR.Refresh();

                lstToMM.DataSource = _dsToMM.Tables[0];
                lstToMM.DisplayMember = "BAR_NO_DIS";
                lstToMM.ValueMember = "BAR_NO";
            }
            txtQty.Text = lstToMM.Items.Count.ToString();
        }

        //反选
        private void btnUnmm_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstToMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstToMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnToMM.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsToMM.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnToMM.Tables["BAR_QC"].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = "QC";
                    _drNew["BIL_ID"] = DBNull.Value;
                    _drNew["BIL_NO"] = DBNull.Value;
                    _drNew["REM"] = _dr["REM"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["WH"] = _dr["WH"];

                    _dsUnToMM.Tables["BAR_QC"].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnToMM.AcceptChanges();
                _dsToMM.AcceptChanges();

                DataView _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
                _dv.RowFilter = "QC='1'";
                DataTable _dt = _dv.ToTable();

                lstUnToMM.DataSource = _dt;
                lstUnToMM.DisplayMember = "BAR_NO_DIS";
                lstUnToMM.ValueMember = "BAR_NO";
                lstUnToMM.Refresh();

                _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
                _dv.RowFilter = "QC='2'";
                _dt = _dv.ToTable();

                lstUnToMMR.DataSource = _dt;
                lstUnToMMR.DisplayMember = "BAR_NO_DIS";
                lstUnToMMR.ValueMember = "BAR_NO";
                lstUnToMMR.Refresh();

                lstToMM.DataSource = _dsToMM.Tables["BAR_QC"];
                lstToMM.DisplayMember = "BAR_NO_DIS";
                lstToMM.ValueMember = "BAR_NO";
            }
            txtQty.Text = lstToMM.Items.Count.ToString();
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

        #region 强制缴库

        private void btnTommR_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstUnToMMR.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstUnToMMR.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnToMM.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrMM = _dsToMM.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrMM)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsToMM.Tables["BAR_QC"].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["BIL_ID"] = _dr["BIL_ID"];
                    _drNew["BIL_NO"] = _dr["BIL_NO"];
                    _drNew["STATUS"] = _dr["STATUS"];
                    _drNew["REM"] = _dr["REM"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["WH"] = _dr["WH"];

                    _dsToMM.Tables["BAR_QC"].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnToMM.AcceptChanges();
                _dsToMM.AcceptChanges();

                DataView _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
                _dv.RowFilter = "QC='1'";
                DataTable _dt = _dv.ToTable();

                lstUnToMM.DataSource = _dt;
                lstUnToMM.DisplayMember = "BAR_NO_DIS";
                lstUnToMM.ValueMember = "BAR_NO";
                lstUnToMM.Refresh();

                _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
                _dv.RowFilter = "QC='2'";
                _dt = _dv.ToTable();

                lstUnToMMR.DataSource = _dt;
                lstUnToMMR.DisplayMember = "BAR_NO_DIS";
                lstUnToMMR.ValueMember = "BAR_NO";

                lstToMM.DataSource = _dsToMM.Tables["BAR_QC"];
                lstToMM.DisplayMember = "BAR_NO_DIS";
                lstToMM.ValueMember = "BAR_NO";
            }
            txtQty.Text = lstToMM.Items.Count.ToString();
        }

        private void btnUnmmR_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstToMM.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstToMM.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsUnToMM.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArrE = _dsToMM.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrE)
                {
                    _drNew = _dsUnToMM.Tables["BAR_QC"].NewRow();
                    _drNew["BAR_NO"] = _dr["BAR_NO"];
                    _drNew["BAR_NO_DIS"] = _dr["BAR_NO_DIS"];
                    _drNew["QC"] = _dr["QC"];
                    _drNew["STATUS"] = "QC";
                    _drNew["BIL_ID"] = DBNull.Value;
                    _drNew["BIL_NO"] = DBNull.Value;
                    _drNew["REM"] = _dr["REM"];
                    _drNew["PRD_NO"] = _dr["PRD_NO"];
                    _drNew["BAT_NO"] = _dr["BAT_NO"];
                    _drNew["WH"] = _dr["WH"];

                    _dsUnToMM.Tables["BAR_QC"].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsUnToMM.AcceptChanges();
                _dsToMM.AcceptChanges();

                DataView _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
                _dv.RowFilter = "QC='1'";
                DataTable _dt = _dv.ToTable();

                lstUnToMM.DataSource = _dt;
                lstUnToMM.DisplayMember = "BAR_NO_DIS";
                lstUnToMM.ValueMember = "BAR_NO";
                lstUnToMM.Refresh();

                _dv = _dsUnToMM.Tables["BAR_QC"].DefaultView;
                _dv.RowFilter = "QC='2'";
                _dt = _dv.ToTable();

                lstUnToMMR.DataSource = _dt;
                lstUnToMMR.DisplayMember = "BAR_NO_DIS";
                lstUnToMMR.ValueMember = "BAR_NO";
                lstUnToMMR.Refresh();

                lstToMM.DataSource = _dsToMM.Tables["BAR_QC"];
                lstToMM.DisplayMember = "BAR_NO_DIS";
                lstToMM.ValueMember = "BAR_NO";
            }
            txtQty.Text = lstToMM.Items.Count.ToString();
        }

        #endregion
    }
}