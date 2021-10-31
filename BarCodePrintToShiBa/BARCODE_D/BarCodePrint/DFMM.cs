using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class DFMM : Form
    {
        private DataSet _dsBarCode = new DataSet();
        private DataSet _dsMo = new DataSet();
        private string _whNameTem = "";

        public DFMM()
        {
            InitializeComponent();
        }

        private void btnSalNo_Click(object sender, EventArgs e)
        {
            //QueryWin _qw = new QueryWin();
            //_qw.PGM = "SALM";//表名
            //if (_qw.ShowDialog() == DialogResult.OK)
            //{
            //    txtSalNo.Text = _qw.NO_RT;
            //    txtSalName.Text = _qw.NAME_RTN;
            //}
        }

        #region 制令单号
        private void btnMONo_Click(object sender, EventArgs e)
        {
            QueryMo _qm = new QueryMo();
            _qm.SQLWhere = " AND (ISNULL(A.CHK_MAN,'')<>'' AND A.CLS_DATE IS NOT NULL) AND ISNULL(A.CLOSE_ID,'F')<>'T' AND ISNULL(B.KND,'')='3' ";
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
                    if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                    {
                        _dsMo.Clear();
                        _dsMo.AcceptChanges();
                    }
                }
            }
        }

        private void txtMONo_Leave(object sender, EventArgs e)
        {
            if (txtMONo.Text != "")
            {
                DataSet _ds = new DataSet();
                string _sqlWhere = "";
                onlineService.BarPrintServer _drs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
                _drs.UseDefaultCredentials = true;
                _sqlWhere += " AND A.MO_NO = '" + txtMONo.Text + "' ";
                _ds = _drs.QueryMOData(_sqlWhere);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    _dsMo = _ds.Copy();
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
        #endregion

        private void DFMM_Load(object sender, EventArgs e)
        {
            txtSalNo.Text = BarRole.USR_NO;
            cbMoNull.Visible = BarRole.CHK_MONULL;
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

            BarSet _bs = new BarSet();
            string _mmNo = "";
            DataRow _drNew = null;

            DataRow _drMo = null;
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                _drMo = _dsMo.Tables[0].Rows[0];
            if (_drMo != null)
            {
                //有制令单时
                foreach (DataRow dr in _dsBarCode.Tables["BAR_REC"].Rows)
                {
                    //控制品号1、3、4位，长度和宽度不能超过制令单的制成品的长度和宽度    2009-06-10
                    //应客户（侯课）要求，去掉【控制品号1、3、4位】的功能，改回为原来的1、2、3、4位都控制到(2010-04-02)
                    if (dr["PRD_NO"].ToString().Substring(0, 4) == _drMo["PRD_NO"].ToString().Substring(0, 4)
                        && _bs.CharacterToNum(_drMo["PRD_NO"].ToString().Substring(9, 3)) >= _bs.CharacterToNum(dr["PRD_NO"].ToString().Substring(9, 3))
                        && Convert.ToInt32(_drMo["PRD_NO"].ToString().Substring(4, 5)) >= Convert.ToInt32(dr["PRD_NO"].ToString().Substring(4, 5))
                        && dr["PRD_MARK"].ToString() == _drMo["PRD_MARK"].ToString())
                    {
                        DataRow[] _drChk = _ds.Tables[0].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "' AND ISNULL(PRD_MARK,'')='" + dr["PRD_MARK"].ToString() + "' ");
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
                            _drNew["PRD_MARK"] = dr["PRD_MARK"];
                            _drNew["UNIT"] = "1";
                            //_drNew["BAT_NO"] = dr["BAT_NO"];//系统不采用批号
                            _drNew["USR"] = BarRole.USR_NO;
                            _drNew["USR_NO"] = txtSalNo.Text;
                            _drNew["DEP"] = BarRole.DEP;
                            _drNew["MO_NO"] = _drMo["MO_NO"];
                            _drNew["ID_NO"] = _drMo["ID_NO"];
                            _drNew["WH"] = txtWH.Text;
                            _drNew["REM"] = _drMo["REM"];
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
                    else
                    {
                        MessageBox.Show("缴库的货品与制令单［" + _drMo["MO_NO"].ToString() + "］中的货品［" + _drMo["PRD_NO"].ToString() + "］不符");
                        _ds.Clear();
                        return;
                    }
                }
            }
            else
            {
                if (!cbMoNull.Checked)
                {
                    //制令单不允许为空
                    MessageBox.Show("制令单输入有误，请重新选择制令单！");
                    return;
                }
                else
                {
                    //无制令单时
                    foreach (DataRow dr in _dsBarCode.Tables["BAR_REC"].Rows)
                    {
                        //DataRow[] _drChk = _ds.Tables[0].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "' AND ISNULL(PRD_MARK,'')='" + dr["PRD_MARK"].ToString() + "' AND BAT_NO='" + dr["BAT_NO"].ToString() + "'");
                        DataRow[] _drChk = _ds.Tables[0].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "' AND ISNULL(PRD_MARK,'')='" + dr["PRD_MARK"].ToString() + "'");
                        if (_drChk.Length > 0)
                        {
                            _drChk[0]["QTY_OK"] = Convert.ToInt32(_drChk[0]["QTY_OK"]) + 1;//缴库数量累加1
                        }
                        else
                        {
                            _drNew = _dt.NewRow();
                            _drNew["MM_NO"] = _mmNo;
                            _drNew["QTY_OK"] = 1;
                            _drNew["PRD_NO"] = dr["PRD_NO"];
                            _drNew["PRD_NAME"] = dr["PRD_NAME"];
                            _drNew["PRD_MARK"] = dr["PRD_MARK"];
                            _drNew["UNIT"] = "1";
                            //_drNew["BAT_NO"] = dr["BAT_NO"];
                            _drNew["USR"] = BarRole.USR_NO;
                            _drNew["USR_NO"] = txtSalNo.Text;
                            _drNew["DEP"] = BarRole.DEP;
                            _drNew["MO_NO"] = DBNull.Value;
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
                }
            }
            try
            {
                //_ds.Merge(_dsBarCode.Tables["BAR_ELIGIBLE"]);
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                _bps.UpdatePassData(_ds);

                MessageBox.Show("转单成功！");
                _dsBarCode.Clear();
                _dsBarCode.AcceptChanges();
                _dsMo.Clear();
                _dsMo.AcceptChanges();

                lstMM.DataSource = _dsBarCode.Tables[0];
                lstMM.DisplayMember = "BAR_NO_DIS";
                lstMM.ValueMember = "BAR_NO";
                lstMM.Refresh();

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
            catch(Exception _ex)
            {
                string _err = _ex.Message;
                int _idx = _err.IndexOf("SunlikeException");
                if (_idx > 0)
                    _err = _err.Substring(_idx + 17);
                if (_err.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                MessageBox.Show("转单出错，原因：" + _err);
                _ds.Clear();
            }
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

            if (txtMONo.Text == "" && !cbMoNull.Checked)
                MessageBox.Show("请选择制令单！");
            else if (txtWH.Text == "")
                MessageBox.Show("请选择库位！");
            //else if (_qty > (_qtyYI - _qtyFin - _qtyLost))
            //    MessageBox.Show("货品已超交！");
            else
                toMM();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _dsBarCode.Clear();
            _dsBarCode.AcceptChanges();

            if (_dsBarCode != null && _dsBarCode.Tables.Count > 0)
            {
                lstMM.DataSource = _dsBarCode.Tables[0];
                lstMM.DisplayMember = "BAR_NO_DIS";
                lstMM.ValueMember = "BAR_NO";
                lstMM.Refresh();
            }

            _dsMo.Clear();
            _dsMo.AcceptChanges();

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

        #region 条码扫描
        private void txtBarNo_Leave(object sender, EventArgs e)
        {
            string _barNo = txtBarNo.Text.Trim();
            if (!String.IsNullOrEmpty(_barNo))
            {
                if (_barNo.Replace("#","").Length != 24)
                {
                    MessageBox.Show("输入的序列号不是半成品，请检查！");
                    txtBarNo.Focus();
                    return;
                }
                else
                {
                    try
                    {
                        onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                        _bps.UseDefaultCredentials = true;
                        DataSet _dsTemp = new DataSet();
                        _dsTemp = _bps.GetBCUnMMLst(" AND A.BAR_NO='" + _barNo + "' ");//取得送缴的序列号
                        if (_dsTemp != null && _dsTemp.Tables.Count > 0 && _dsTemp.Tables[0].Rows.Count > 0)
                        {
                            if (_dsBarCode != null && _dsBarCode.Tables.Count > 0)
                            {
                                if (_dsBarCode.Tables[0].Rows.Count > 0 && _dsBarCode.Tables[0].Select("ISNULL(BIL_NO,'')='" + _dsTemp.Tables[0].Rows[0]["BIL_NO"].ToString() + "'").Length == 0)
                                {
                                    MessageBox.Show("输入的序列号与缴库单信息不一致，不能缴库！");
                                    txtBarNo.Text = "";
                                    txtBarNo.Focus();
                                    return;
                                }
                                try
                                {
                                    _dsBarCode.Tables[0].ImportRow(_dsTemp.Tables[0].Rows[0]);
                                }
                                catch
                                {
                                    txtBarNo.Text = "";
                                    txtBarNo.Focus();
                                }
                            }
                            else
                                _dsBarCode.Merge(_dsTemp);

                            if (txtMONo.Text == "")
                            {
                                txtMONo.Text = _dsTemp.Tables[0].Rows[0]["BIL_NO"].ToString();
                                txtMONo_Leave(null, null);
                                if (_dsTemp.Tables[0].Rows[0]["WH_SJ"].ToString() != "")
                                {
                                    txtWH.Text = _dsTemp.Tables[0].Rows[0]["WH_SJ"].ToString();
                                    txtWhName.Text = _dsTemp.Tables[0].Rows[0]["WH_SJ_NAME"].ToString();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("序列号不存在或已入库！");
                        }
                        if (_dsBarCode != null && _dsBarCode.Tables.Count > 0)
                        {
                            lstMM.DataSource = _dsBarCode.Tables[0];
                            lstMM.DisplayMember = "BAR_NO_DIS";
                            lstMM.ValueMember = "BAR_NO";
                            lstMM.Refresh();
                        }
                    }
                    catch (Exception _ex)
                    {
                        string _err = "";
                        if (_ex.Message.Length > 500)
                            _err = _ex.Message.Substring(0, 500);
                        else
                            _err = _ex.Message;
                        MessageBox.Show("序列号不存在! 原因：\n" + _err);
                    }
                    txtBarNo.Text = "";
                    txtBarNo.Focus();
                    txtQty.Text = lstMM.Items.Count.ToString();
                }
            }
        }

        private void txtBarNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == BarRole.EndChar)
                txtBarNo_Leave(null, null);
        }
        #endregion
    }
}