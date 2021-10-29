using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BarCodePrintTSB
{
    public partial class SAChgBarCode : Form
    {
        private DataSet _dsSA = null;
        private DataSet _dsWH = null;
        public SAChgBarCode()
        {
            InitializeComponent();
        }

        #region 销货单序列号

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string _sqlWhere = " AND A.PS_ID='SA' AND ISNULL(J.BAR_CODE,'')<>'' ";
            if (txtSANO.Text != "")
                _sqlWhere += " AND A.PS_NO='" + txtSANO.Text + "' ";
            else
            {
                MessageBox.Show("请选择销货单号！");
                return;
            }
            //else
            //    _sqlWhere += " AND A.PS_NO='' ";
            if (txtBarNoSA.Text != "")
                _sqlWhere += " AND A.BAR_CODE='" + txtBarNoSA.Text + "'";
            if (txtBoxNoSA.Text != "")
                _sqlWhere += " AND A.BOX_NO='" + txtBoxNoSA.Text + "' ";


            //if (txtBarNoSA.Text != "")
            //    _sqlWhere += " AND A.BAR_CODE>='" + txtBarNoSA.Text + "'";
            //if (txtBarNoSAE.Text != "")
            //    _sqlWhere += " AND A.BAR_CODE<='" + txtBarNoSAE.Text + "'";
            //if (txtBarNoWHB.Text != "")
            //    _sqlWhere += " AND A.BOX_NO>='" + txtBoxNoSAB.Text + "'";
            //if (txtBoxNoSAE.Text != "")
            //    _sqlWhere += " AND A.BOX_NO<='" + txtBoxNoSAE.Text + "'";

            if (_dsSA != null && _dsSA.Tables.Contains("TF_PSS3"))
            {
                DataRow[] _drArr;
                if (txtBarNoSA.Text != "")
                {
                    _drArr = _dsSA.Tables["TF_PSS3"].Select("BAR_CODE='" + txtBarNoSA.Text + "'");
                    if (_drArr.Length > 0)
                    {
                        return;
                    }
                }
                if (txtBoxNoSA.Text != "")
                {
                    _drArr = _dsSA.Tables["TF_PSS3"].Select("BOX_NO='" + txtBoxNoSA.Text + "'");
                    if (_drArr.Length > 0)
                    {
                        return;
                    }
                }
            }
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            DataSet _dsTemp = _bps.GetSABarRec(_sqlWhere);
            if (_dsTemp != null && _dsTemp.Tables.Contains("TF_PSS3"))
            {
                _dsTemp.Tables[0].Columns["CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["CHG_SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["QC_REM"].ReadOnly = false;
                _dsSA = _dsTemp.Clone();

                if (_dsTemp.Tables["TF_PSS3"].Rows.Count > 0)
                {
                    if (txtSANO.Text == "")
                        txtSANO.Text = _dsTemp.Tables["TF_PSS3"].Rows[0]["PS_NO"].ToString();
                    foreach (DataRow dr in _dsTemp.Tables["TF_PSS3"].Rows)
                    {
                        dr["CUS_NAME"] = Strings.StrConv(dr["CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                        dr["WH_NAME"] = Strings.StrConv(dr["WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                        dr["SPC_NAME"] = Strings.StrConv(dr["SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                        dr["CHG_SPC_NAME"] = Strings.StrConv(dr["CHG_SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);

                        if (dr["QC"].ToString() == "1")
                            dr["QC"] = "合格";
                        else if (dr["QC"].ToString() == "A")
                            dr["QC"] = "A级品";
                        else if (dr["QC"].ToString() == "B")
                            dr["QC"] = "B级品";
                        else if (dr["QC"].ToString() == "2")
                            dr["QC"] = "不合格";
                        else if (dr["QC"].ToString() == "N")
                            dr["QC"] = "期初库存";
                        dr["QC_REM"] = Strings.StrConv(dr["QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);

                        _dsSA.Tables["TF_PSS3"].ImportRow(dr);
                    }
                }
                else
                {
                    MessageBox.Show("序列号不存在！");
                }

                dgSABar.DataMember = "TF_PSS3";
                dgSABar.DataSource = _dsSA;
                dgSABar.Refresh();
            }
        }

        private void dgSABar_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgSABar.Columns["PS_NO"].Visible = false;
            dgSABar.Columns["PS_ITM"].Visible = false;
            dgSABar.Columns["PS_ID"].Visible = false;
            dgSABar.Columns["ITM"].Visible = false;
            dgSABar.Columns["PRD_NO"].HeaderText = "品名";
            dgSABar.Columns["BAR_CODE"].HeaderText = "序列号";
            dgSABar.Columns["BOX_NO"].HeaderText = "箱号";
            dgSABar.Columns["CUS_NO"].Visible = false;
            dgSABar.Columns["CUS_NAME"].HeaderText = "客户";
            dgSABar.Columns["WH"].Visible = false;
            dgSABar.Columns["WH_NAME"].HeaderText = "库位";
            dgSABar.Columns["BAT_NO"].HeaderText = "批号";
            dgSABar.Columns["SPC"].HeaderText = "规格";
            dgSABar.Columns["IDX_NAME"].HeaderText = "中类";
            dgSABar.Columns["SPC_NAME"].HeaderText = "不合格原因";
            dgSABar.Columns["SPC_NO"].Visible = false;
            dgSABar.Columns["PRD_MARK"].Visible = false;
            dgSABar.Columns["QC"].HeaderText = "品质信息";
            dgSABar.Columns["QC_REM"].HeaderText = "品质备注";
            dgSABar.Columns["CHG_SPC_NAME"].HeaderText = "替换原因";
            dgSABar.Columns["CHK_COL"].Visible = false;
            dgSABar.Columns["IS_CHANGED"].Visible = false;
            //dgSABar.Columns["BC_BAR_NO"].Visible = false;
            dgSABar.Columns["REM"].Visible = false;
            dgSABar.Columns["CHG_SPC_NO"].Visible = false;

            dgSABar.Columns["BAR_CODE"].DisplayIndex = 0;
            dgSABar.Columns["BAR_CODE"].Width = 180;
            dgSABar.Columns["BOX_NO"].DisplayIndex = 1;
            dgSABar.Columns["QC"].DisplayIndex = 2;
            dgSABar.Columns["SPC_NAME"].DisplayIndex = 3;
            dgSABar.Columns["QC_REM"].DisplayIndex = 4;
            dgSABar.Columns["WH_NAME"].DisplayIndex = 5;
            dgSABar.Columns["IDX_NAME"].DisplayIndex = 6;
            dgSABar.Columns["SPC"].DisplayIndex = 7;
            dgSABar.Columns["BAT_NO"].DisplayIndex = 8;
            dgSABar.Columns["PRD_NO"].DisplayIndex = 9;
            dgSABar.Columns["CUS_NAME"].DisplayIndex = 10;
            dgSABar.Columns["CHG_SPC_NAME"].DisplayIndex = 11;
        }

        #endregion

        #region 新序列号

        private void btnSearchBar_Click(object sender, EventArgs e)
        {
            string _sqlWhere = " AND ISNULL(A.STOP_ID,'')<>'T' AND ISNULL(A.WH,'')<>'' ";
            if (txtBarNoWH.Text != "")
                _sqlWhere += " AND A.BAR_NO='" + txtBarNoWH.Text + "'";
            if (txtBoxNoWH.Text != "")
                _sqlWhere += " AND A.BOX_NO='" + txtBoxNoWH.Text + "' ";

            //if (txtBarNoWHB.Text != "")
            //    _sqlWhere += " AND A.BAR_CODE>='" + txtBarNoWHB.Text + "'";
            //if (txtBarNoWHE.Text != "")
            //    _sqlWhere += " AND A.BAR_CODE<='" + txtBarNoWHE.Text + "'";
            //if (txtBoxNoWHB.Text != "")
            //    _sqlWhere += " AND A.BOX_NO>='" + txtBoxNoWHB.Text + "' ";
            //if (txtBoxNoWHE.Text != "")
            //    _sqlWhere += " AND A.BOX_NO<='" + txtBoxNoWHE.Text + "' ";
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            DataSet _dsTemp = _bps.GetChgBar(_sqlWhere);
            if (_dsTemp != null && _dsTemp.Tables.Contains("BAR_REC"))
            {
                _dsTemp.Tables[0].Columns["WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["QC"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["QC_REM"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["REM"].ReadOnly = false;
                if (_dsWH == null || !_dsTemp.Tables.Contains("BAR_REC"))
                {
                    _dsWH = _dsTemp.Clone();
                }

                if (_dsTemp.Tables["BAR_REC"].Rows.Count > 0)
                {
                    foreach (DataRow dr in _dsTemp.Tables["BAR_REC"].Rows)
                    {
                        dr["WH_NAME"] = Strings.StrConv(dr["WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                        dr["SPC_NAME"] = Strings.StrConv(dr["SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                        
                        if (dr["QC"].ToString() == "1")
                            dr["QC"] = "合格";
                        else if (dr["QC"].ToString() == "A")
                            dr["QC"] = "A级品";
                        else if (dr["QC"].ToString() == "B")
                            dr["QC"] = "B级品";
                        else if (dr["QC"].ToString() == "2")
                            dr["QC"] = "不合格";
                        else if (dr["QC"].ToString() == "N")
                            dr["QC"] = "期初库存";
                        dr["QC_REM"] = Strings.StrConv(dr["QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                        dr["REM"] = Strings.StrConv(dr["REM"].ToString(), VbStrConv.SimplifiedChinese, 0);

                        _dsWH.Tables["BAR_REC"].ImportRow(dr);
                    }
                }
                else
                {
                    MessageBox.Show("序列号不存在！");
                }

                dgWHBar.DataMember = "BAR_REC";
                dgWHBar.DataSource = _dsWH;
                dgWHBar.Refresh();
            }
        }

        private void dgWHBar_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgWHBar.Columns["BAR_NO"].HeaderText = "序列号";
            dgWHBar.Columns["BOX_NO"].HeaderText = "箱号";
            dgWHBar.Columns["PRD_NO"].HeaderText = "品名";
            dgWHBar.Columns["WH"].Visible = false;
            dgWHBar.Columns["WH_NAME"].HeaderText = "库位";
            dgWHBar.Columns["JT"].Visible = false;
            dgWHBar.Columns["PRN_DD"].Visible = false;
            dgWHBar.Columns["BAT_NO"].HeaderText = "批号";
            dgWHBar.Columns["SPC"].HeaderText = "规格";
            dgWHBar.Columns["IDX_NAME"].HeaderText = "中类";
            dgWHBar.Columns["SPC_NAME"].HeaderText = "异常原因";
            dgWHBar.Columns["SPC_NO"].Visible = false;
            dgWHBar.Columns["QC"].HeaderText = "品质信息";
            dgWHBar.Columns["QC_REM"].HeaderText = "品质备注";
            dgWHBar.Columns["BIL_NOLIST"].Visible = false;
            dgWHBar.Columns["REM"].Visible = false;
            dgWHBar.Columns["MM_NO"].Visible = false;
            dgWHBar.Columns["CUS_NO"].Visible = false;
            dgWHBar.Columns["UPDDATE"].Visible = false;
            dgWHBar.Columns["PH_FLAG"].Visible = false;
            dgWHBar.Columns["STOP_ID"].Visible = false;
            dgWHBar.Columns["PRD_MARK"].Visible = false;
            dgWHBar.Columns["SPC_NO1"].Visible = false;

            dgWHBar.Columns["BAR_NO"].DisplayIndex = 0;
            dgWHBar.Columns["BAR_NO"].Width = 180;
            dgWHBar.Columns["BOX_NO"].DisplayIndex = 1;
            dgWHBar.Columns["QC"].DisplayIndex = 2;
            dgWHBar.Columns["SPC_NAME"].DisplayIndex = 3;
            dgWHBar.Columns["QC_REM"].DisplayIndex = 4;
            dgWHBar.Columns["WH_NAME"].DisplayIndex = 5;
            dgWHBar.Columns["IDX_NAME"].DisplayIndex = 6;
            dgWHBar.Columns["SPC"].DisplayIndex = 7;
            dgWHBar.Columns["BAT_NO"].DisplayIndex = 8;
            dgWHBar.Columns["PRD_NO"].DisplayIndex = 9;
        }

        #endregion

        #region 替换

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (_dsSA == null || !_dsSA.Tables.Contains("TF_PSS3") || _dsWH == null || !_dsWH.Tables.Contains("BAR_REC"))
            {
                return;
            }

            _dsSA.AcceptChanges();
            _dsWH.AcceptChanges();

            int _saCnt = _dsSA.Tables["TF_PSS3"].Rows.Count;
            int _whCnt = _dsWH.Tables["BAR_REC"].Rows.Count;

            if (_saCnt == 0 || _whCnt == 0)
            {
                return;
            }
            if (MessageBox.Show("确定要进行替换吗？", "条码替换", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (_saCnt != _whCnt)
                {
                    MessageBox.Show("替换条码数不相等，请检查");
                    return;
                }
                else
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    //int _maxItm = _bps.GetMaxSAItm(_dsSA.Tables["TF_PSS3"].Rows[0]["PS_NO"].ToString());

                    DataRow[] _drArrSA;
                    DataRow[] _drArrWH;
                    //DataRow _dr;
                    foreach (DataRow drWH in _dsWH.Tables["BAR_REC"].Rows)
                    {
                        _drArrSA = _dsSA.Tables["TF_PSS3"].Select("PRD_NO='" + drWH["PRD_NO"].ToString() + "' AND ISNULL(PRD_MARK,'')='" + drWH["PRD_MARK"].ToString() + "' AND WH='" + drWH["WH"].ToString() + "'");
                        _drArrWH = _dsWH.Tables["BAR_REC"].Select("PRD_NO='" + drWH["PRD_NO"].ToString() + "' AND ISNULL(PRD_MARK,'')='" + drWH["PRD_MARK"].ToString() + "' AND WH='" + drWH["WH"].ToString() + "'");
                        if (_drArrSA.Length != _drArrWH.Length)
                        {
                            MessageBox.Show("货品【" + drWH["PRD_NO"].ToString() + "】不能替换，请检查品名和库位是否一致。");
                            return;
                        }
                    }
                    //foreach (DataRow drWH in _dsWH.Tables["BAR_REC"].Rows)
                    //{
                    //    _drArrSA = _dsSA.Tables["TF_PSS3"].Select("PRD_NO='" + drWH["PRD_NO"].ToString() + "' AND PRD_MARK='" + drWH["PRD_MARK"].ToString() + "'");
                    //    if (_drArrSA.Length > 0)
                    //    {
                    //        _dr = _dsSA.Tables["TF_PSS3"].NewRow();
                    //        _dr["PS_ID"] = _dsSA.Tables["TF_PSS3"].Rows[0]["PS_ID"];
                    //        _dr["PS_NO"] = _dsSA.Tables["TF_PSS3"].Rows[0]["PS_NO"];
                    //        _dr["PS_ITM"] = _drArrSA[0]["PS_ITM"];
                    //        _dr["ITM"] = ++_maxItm;
                    //        _dr["BAR_CODE"] = drWH["BAR_NO"];
                    //        _dr["BOX_NO"] = drWH["BOX_NO"];
                    //        _dr["PRD_NO"] = drWH["PRD_NO"];
                    //        _dr["PRD_MARK"] = drWH["PRD_MARK"];
                    //        _dsSA.Tables["TF_PSS3"].Rows.Add(_dr);
                    //    }
                    //    else
                    //    {
                    //    }
                    //}
                    //foreach (DataRow drSA in _dsSA.Tables["TF_PSS3"].Rows)
                    //{
                    //    if (drSA.RowState != DataRowState.Added)
                    //        drSA.Delete();
                    //}
                    _dsSA.Tables["TF_PSS3"].Columns.Add("USR_CHG", System.Type.GetType("System.String"));
                    _dsSA.Tables["TF_PSS3"].Columns.Add("CHG_DATE", System.Type.GetType("System.DateTime"));
                    for (int i = 0; i < _dsWH.Tables["BAR_REC"].Rows.Count; i++)
                    {
                        _dsSA.Tables["TF_PSS3"].Rows[i]["BAR_CODE"] = _dsWH.Tables["BAR_REC"].Rows[i]["BAR_NO"];
                        _dsSA.Tables["TF_PSS3"].Rows[i]["BOX_NO"] = _dsWH.Tables["BAR_REC"].Rows[i]["BOX_NO"];
                        _dsSA.Tables["TF_PSS3"].Rows[i]["PRD_NO"] = _dsWH.Tables["BAR_REC"].Rows[i]["PRD_NO"];
                        _dsSA.Tables["TF_PSS3"].Rows[i]["PRD_MARK"] = _dsWH.Tables["BAR_REC"].Rows[i]["PRD_MARK"];

                        _dsSA.Tables["TF_PSS3"].Rows[i]["USR_CHG"] = BarRole.USR_NO;
                        _dsSA.Tables["TF_PSS3"].Rows[i]["CHG_DATE"] = DateTime.Now;
                    }
                    try
                    {
                        string _msg = _bps.ChangeSABar(_dsSA);
                        if (_msg != "")
                        {
                            _dsSA.RejectChanges();
                            MessageBox.Show(_msg);
                        }
                        else
                        {
                            MessageBox.Show("替换成功！");
                            _dsSA.Clear();
                            _dsWH.Clear();
                            dgWHBar.DataMember = "BAR_REC";
                            dgWHBar.DataSource = _dsWH;
                            dgWHBar.Refresh();
                        }
                    }
                    catch (Exception _ex)
                    {
                        _dsSA.RejectChanges();
                        string _err = "";
                        if (_ex.Message.Length > 500)
                            _err = _ex.Message.Substring(0, 500);
                        else
                            _err = _ex.Message;
                        MessageBox.Show("条码替换失败，原因：\n" + _err);
                    }
                }
            }
        }

        #endregion

        #region 过滤条件
        private void txtBarNoWH_Leave(object sender, EventArgs e)
        {
            if (txtBarNoWH.Text != "")
            {
                btnSearchBar_Click(null, null);
                txtBarNoWH.Text = "";
            }
        }

        private void txtBarNoWH_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBarNoWH.Text != "")
            {
                if (e.KeyValue == 13)
                {
                    btnSearchBar_Click(null, null);
                    txtBarNoWH.Text = "";
                }
            }
        }

        private void txtBoxNoWH_Leave(object sender, EventArgs e)
        {
            if (txtBoxNoWH.Text != "")
            {
                btnSearchBar_Click(null, null);
                txtBoxNoWH.Text = "";
            }
        }

        private void txtBoxNoWH_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBoxNoWH.Text != "")
            {
                if (e.KeyValue == 13)
                {
                    btnSearchBar_Click(null, null);
                    txtBoxNoWH.Text = "";
                }
            }
        }

        private void txtBarNoSA_Leave(object sender, EventArgs e)
        {
            if (txtBarNoSA.Text != "")
            {
                btnSearch_Click(null, null);
                txtBarNoSA.Text = "";
            }
        }

        private void txtBarNoSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBarNoSA.Text != "")
            {
                if (e.KeyValue == 13)
                {
                    btnSearch_Click(null, null);
                    txtBarNoSA.Text = "";
                }
            }
        }

        private void txtBoxNoSA_Leave(object sender, EventArgs e)
        {
            if (txtBoxNoSA.Text != "")
            {
                btnSearch_Click(null, null);
                txtBoxNoSA.Text = "";
            }
        }

        private void txtBoxNoSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBoxNoSA.Text != "")
            {
                if (e.KeyValue == 13)
                {
                    btnSearch_Click(null, null);
                    txtBoxNoSA.Text = "";
                }
            }
        }

        private void qrySA_Click(object sender, EventArgs e)
        {
            QuerySA _qw = new QuerySA();
            _qw.SQLWhere += " AND ISNULL(CLSID,'')<>'T' ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                this.txtSANO.Text = _qw.SaNo;
            }
        }
        #endregion

        private void dgWHBar_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
             dgWHBar.Columns[e.ColumnIndex].ReadOnly = true;
        }
    }
}