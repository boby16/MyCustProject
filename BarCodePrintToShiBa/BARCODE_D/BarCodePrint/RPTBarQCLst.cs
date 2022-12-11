using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BarCodePrint
{
    public partial class RPTBarQCLst : Form
    {
        private DataSet _ds = null;
        public RPTBarQCLst()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime _dateB = System.DateTime.Now;
            DateTime _dateE = System.DateTime.Now;
            _dateB = Convert.ToDateTime(this.txtDateB.Text);
            _dateE = Convert.ToDateTime(this.txtDateE.Text);
            TimeSpan _ts = _dateE.Subtract(_dateB);

            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.progressBar1.Minimum = 0;
            _frmProcess.progressBar1.Maximum = _ts.Days + 3;
            DataSet _dsTemp = new DataSet();

            try
            {
                _frmProcess.Show(this);
                Application.DoEvents();
                DateTime _tmpDateTime = _dateB;
                int _days = 1;
                if (_tmpDateTime != _dateE)
                {
                    while (_tmpDateTime <= _dateE)
                    {
                        DateTime _dateBCurrent = _tmpDateTime;
                        DateTime _dateECurrent = _dateBCurrent.AddDays(_days);
                        if (_dateECurrent < _dateE)
                        {
                            _frmProcess.groupBox1.Text = _dateBCurrent.ToString("yyyy-MM-dd") + " 至 " + _dateECurrent.ToString("yyyy-MM-dd");
                            _frmProcess.Refresh();
                            FindDataByDate(_dsTemp, _dateBCurrent, _dateECurrent);
                        }
                        else
                        {
                            _frmProcess.groupBox1.Text = _dateBCurrent.ToString("yyyy-MM-dd") + " 至 " + _dateE.ToString("yyyy-MM-dd");
                            _frmProcess.Refresh();
                            FindDataByDate(_dsTemp, _dateBCurrent, _dateE);
                        }
                        _frmProcess.progressBar1.Value++;
                        Application.DoEvents();
                        _tmpDateTime = _tmpDateTime.AddDays(_days + 1);
                    }
                }
                else
                {
                    FindDataByDate(_dsTemp, _tmpDateTime, _tmpDateTime);
                }
            }
            catch
            {
                _frmProcess.Close();
                MessageBox.Show("数据读取出错，请重试。");
            }

            _frmProcess.progressBar1.Value++;

            if (_dsTemp != null && _dsTemp.Tables.Count > 0 && _dsTemp.Tables.Contains("BARQC_RPT"))
            {
                #region 资源转换
                _dsTemp.Tables[0].Columns["BC_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_FORMAT"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_FORMAT"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["BC_SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_QC"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_QC"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["BC_QC_REM"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_PRC_ID"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_PRC_ID"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["BC_MO_CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_SJ_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_MM_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_SA_CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BC_SB_REM"].ReadOnly = false;

                _dsTemp.Tables[0].Columns["DF_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_QC"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_QC"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["DF_QC_REM"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_PRC_ID"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_PRC_ID"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["DF_MO_CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_SJ_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_MM_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_SA_CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["DF_SB_REM"].ReadOnly = false;

                _dsTemp.Tables[0].Columns["FT_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BOX_FORMAT"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["BOX_FORMAT"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["FT_SPC_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["FT_QC"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["FT_QC"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["FT_QC_REM"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["FT_PRC_ID"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["FT_PRC_ID"].MaxLength = 10;
                _dsTemp.Tables[0].Columns["FT_MO_CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["FT_SJ_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["FT_MM_WH_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["FT_SA_CUS_NAME"].ReadOnly = false;
                _dsTemp.Tables[0].Columns["FT_SB_REM"].ReadOnly = false;

                foreach (DataRow dr in _dsTemp.Tables[0].Rows)
                {
                    #region 板材

                    #region 打印版式

                    if (dr["BC_FORMAT"].ToString() == "1")
                    {
                        dr["BC_FORMAT"] = "标准版";
                    }
                    else if (dr["BC_FORMAT"].ToString() == "2")
                    {
                        dr["BC_FORMAT"] = "列印料号";
                    }
                    else if (dr["BC_FORMAT"].ToString() == "3")
                    {
                        dr["BC_FORMAT"] = "特殊版";
                    }

                    #endregion

                    #region 品检信息

                    if (dr["BC_QC"].ToString() == "1")
                    {
                        dr["BC_QC"] = "合格";
                    }
                    else if (dr["BC_QC"].ToString() == "2")
                    {
                        dr["BC_QC"] = "不合格";
                    }
                    else if (dr["BC_QC"].ToString() == "N")
                    {
                        dr["BC_QC"] = "期初库存";
                    }

                    #endregion

                    #region 处理方式

                    if (dr["BC_PRC_ID"].ToString() == "1")
                    {
                        dr["BC_PRC_ID"] = "强制缴库";
                    }
                    else if (dr["BC_PRC_ID"].ToString() == "2")
                    {
                        dr["BC_PRC_ID"] = "报废";
                    }
                    else if (dr["BC_PRC_ID"].ToString() == "3")
                    {
                        dr["BC_PRC_ID"] = "重开制令";
                    }

                    #endregion

                    dr["BC_WH_NAME"] = Strings.StrConv(dr["BC_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_SPC_NAME"] = Strings.StrConv(dr["BC_SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_QC_REM"] = Strings.StrConv(dr["BC_QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_MO_CUS_NAME"] = Strings.StrConv(dr["BC_MO_CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_SJ_WH_NAME"] = Strings.StrConv(dr["BC_SJ_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_MM_WH_NAME"] = Strings.StrConv(dr["BC_MM_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_SA_CUS_NAME"] = Strings.StrConv(dr["BC_SA_CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["BC_SB_REM"] = Strings.StrConv(dr["BC_SB_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);

                    #endregion

                    #region 半成品

                    #region 品检信息

                    if (dr["DF_QC"].ToString() == "1")
                    {
                        dr["DF_QC"] = "合格";
                    }
                    else if (dr["DF_QC"].ToString() == "2")
                    {
                        dr["DF_QC"] = "不合格";
                    }
                    else if (dr["DF_QC"].ToString() == "N")
                    {
                        dr["DF_QC"] = "期初库存";
                    }

                    #endregion

                    #region 处理方式

                    if (dr["DF_PRC_ID"].ToString() == "1")
                    {
                        dr["DF_PRC_ID"] = "强制缴库";
                    }
                    else if (dr["DF_PRC_ID"].ToString() == "2")
                    {
                        dr["DF_PRC_ID"] = "报废";
                    }
                    else if (dr["DF_PRC_ID"].ToString() == "3")
                    {
                        dr["DF_PRC_ID"] = "重开制令";
                    }

                    #endregion

                    dr["DF_WH_NAME"] = Strings.StrConv(dr["DF_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_SPC_NAME"] = Strings.StrConv(dr["DF_SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_QC_REM"] = Strings.StrConv(dr["DF_QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_MO_CUS_NAME"] = Strings.StrConv(dr["DF_MO_CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_SJ_WH_NAME"] = Strings.StrConv(dr["DF_SJ_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_MM_WH_NAME"] = Strings.StrConv(dr["DF_MM_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_SA_CUS_NAME"] = Strings.StrConv(dr["DF_SA_CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["DF_SB_REM"] = Strings.StrConv(dr["DF_SB_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);

                    #endregion

                    #region 成品

                    #region 打印版式

                    if (dr["BOX_FORMAT"].ToString() == "1")
                    {
                        dr["BOX_FORMAT"] = "标准版";
                    }
                    else if (dr["BOX_FORMAT"].ToString() == "2")
                    {
                        dr["BOX_FORMAT"] = "简约版";
                    }
                    else if (dr["BOX_FORMAT"].ToString() == "3")
                    {
                        dr["BOX_FORMAT"] = "列印编号";
                    }
                    else if (dr["BOX_FORMAT"].ToString() == "4")
                    {
                        dr["BOX_FORMAT"] = "特殊版";
                    }

                    #endregion

                    #region 品检信息

                    if (dr["FT_QC"].ToString() == "1")
                    {
                        dr["FT_QC"] = "合格";
                    }
                    else if (dr["FT_QC"].ToString() == "2")
                    {
                        dr["FT_QC"] = "不合格";
                    }
                    else if (dr["FT_QC"].ToString() == "N")
                    {
                        dr["FT_QC"] = "期初库存";
                    }

                    #endregion

                    #region 处理方式

                    if (dr["FT_PRC_ID"].ToString() == "1")
                    {
                        dr["FT_PRC_ID"] = "强制缴库";
                    }
                    else if (dr["FT_PRC_ID"].ToString() == "2")
                    {
                        dr["FT_PRC_ID"] = "报废";
                    }
                    else if (dr["FT_PRC_ID"].ToString() == "3")
                    {
                        dr["FT_PRC_ID"] = "重开制令";
                    }

                    #endregion

                    dr["FT_WH_NAME"] = Strings.StrConv(dr["FT_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["FT_SPC_NAME"] = Strings.StrConv(dr["FT_SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["FT_QC_REM"] = Strings.StrConv(dr["FT_QC_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["FT_MO_CUS_NAME"] = Strings.StrConv(dr["FT_MO_CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["FT_SJ_WH_NAME"] = Strings.StrConv(dr["FT_SJ_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["FT_MM_WH_NAME"] = Strings.StrConv(dr["FT_MM_WH_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["FT_SA_CUS_NAME"] = Strings.StrConv(dr["FT_SA_CUS_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["FT_SB_REM"] = Strings.StrConv(dr["FT_SB_REM"].ToString(), VbStrConv.SimplifiedChinese, 0);

                    #endregion
                }
                #endregion
            }

            _frmProcess.progressBar1.Value++;

            _ds = _dsTemp.Copy();
            _dsTemp.Dispose();

            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables.Contains("BARQC_RPT"))
            {
                DataView _dv = _ds.Tables[0].DefaultView;
                _dv.Sort = "BC_BAR_NO,DF_BAR_NO,FT_BAR_NO";
                dataGridView1.DataSource = _dv;
                dataGridView1.Refresh();
            }

            foreach (DataGridViewColumn _dc in dataGridView1.Columns)
            {
                if (_dc.Name != "BC_BAR_NO")
                    _dc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            _frmProcess.progressBar1.Value++;
            _frmProcess.Close();
        }

        private void FindDataByDate(DataSet _dsRpt, DateTime beginDate, DateTime endDate)
        {
            StringBuilder _sqlWhere = new StringBuilder();
            _sqlWhere.Append(" and LEN(REPLACE(A.BAR_NO,'#',''))=22");

            #region 起止时间

            if (lstPrdtKnd.SelectedIndex == 1)
            {
                #region 半成品
                if (lstDateType.SelectedIndex == 1)
                {
                    //送缴时间
                    _sqlWhere.Append(" AND DFQC.SJ_DATE>='" + txtDateB.Text + "' AND DFQC.SJ_DATE<='" + txtDateE.Text + "'")
                             .Append(" and DFQC.SJ_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  DFQC.SJ_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else if (lstDateType.SelectedIndex == 2)
                {
                    //销货时间
                    _sqlWhere.Append(" AND DFSA.SYS_DATE>='" + txtDateB.Text + "' AND DFSA.SYS_DATE<='" + txtDateE.Text + "'")
                             .Append(" and DFSA.SYS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  DFSA.SYS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else if (lstDateType.SelectedIndex == 3)
                {
                    //退货时间
                    _sqlWhere.Append(" AND DFSB.SYS_DATE>='" + txtDateB.Text + "' AND DFSB.SYS_DATE<='" + txtDateE.Text + "'")
                             .Append(" and DFSB.SYS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  DFSB.SYS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else
                {
                    //品检时间
                    _sqlWhere.Append(" AND DFQC.QC_DATE>='" + txtDateB.Text + "' AND DFQC.QC_DATE<='" + txtDateE.Text + "'")
                             .Append(" and DFQC.QC_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  DFQC.QC_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                #endregion
            }
            else if (lstPrdtKnd.SelectedIndex == 2)
            {
                #region 成品
                if (lstDateType.SelectedIndex == 1)
                {
                    //送缴时间
                    _sqlWhere.Append(" AND FTQC.SJ_DATE>='" + txtDateB.Text + "' AND FTQC.SJ_DATE<='" + txtDateE.Text + "'")
                             .Append(" and FTQC.SJ_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  FTQC.SJ_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else if (lstDateType.SelectedIndex == 2)
                {
                    //销货时间
                    _sqlWhere.Append(" AND FTSA.SYS_DATE>='" + txtDateB.Text + "' AND FTSA.SYS_DATE<='" + txtDateE.Text + "'")
                             .Append(" and FTSA.SYS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  FTSA.SYS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else if (lstDateType.SelectedIndex == 3)
                {
                    //退货时间
                    _sqlWhere.Append(" AND FTSB.SYS_DATE>='" + txtDateB.Text + "' AND FTSB.SYS_DATE<='" + txtDateE.Text + "'")
                             .Append(" and FTSB.SYS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  FTSB.SYS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else
                {
                    //品检时间
                    _sqlWhere.Append(" AND FTQC.QC_DATE>='" + txtDateB.Text + "' AND FTQC.QC_DATE<='" + txtDateE.Text + "'")
                             .Append(" and FTQC.QC_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  FTQC.QC_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                #endregion
            }
            else
            {
                #region 板材
                if (lstDateType.SelectedIndex == 1)
                {
                    //送缴时间
                    _sqlWhere.Append(" AND BCQC.SJ_DATE>='" + txtDateB.Text + "' AND BCQC.SJ_DATE<='" + txtDateE.Text + "'")
                             .Append(" and BCQC.SJ_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCQC.SJ_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else if (lstDateType.SelectedIndex == 2)
                {
                    //销货时间
                    _sqlWhere.Append(" AND BCSA.SYS_DATE>='" + txtDateB.Text + "' AND BCSA.SYS_DATE<='" + txtDateE.Text + "'")
                             .Append(" and BCSA.SYS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCSA.SYS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else if (lstDateType.SelectedIndex == 3)
                {
                    //退货时间
                    _sqlWhere.Append(" AND BCSB.SYS_DATE>='" + txtDateB.Text + "' AND BCSB.SYS_DATE<='" + txtDateE.Text + "'")
                             .Append(" and BCSB.SYS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCSB.SYS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                else
                {
                    //品检时间
                    _sqlWhere.Append(" AND BCQC.QC_DATE>='" + txtDateB.Text + "' AND BCQC.QC_DATE<='" + txtDateE.Text + "'")
                             .Append(" and BCQC.QC_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCQC.QC_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'");
                }
                #endregion
            }

            #endregion

            string _talbe = "A";
            if (lstPrdtKnd.SelectedIndex == 1)
                _talbe = "DF";//半成品
            else if (lstPrdtKnd.SelectedIndex == 2)
                _talbe = "FT";//成品

            #region 品号
            if (txtPrdNoB.Text != "")
                _sqlWhere.Append(" AND " + _talbe + ".PRD_NO>='" + txtPrdNoB.Text + "'");
            if (txtPrdNoE.Text != "")
                _sqlWhere.Append(" AND " + _talbe + ".PRD_NO<='" + txtPrdNoE.Text + "'");
            #endregion

            #region 批号
            if (txtBatNoB.Text != "")
                _sqlWhere.Append(" AND REPLACE(substring(" + _talbe + ".BAR_NO,13,10),'#','')>='" + txtBatNoB.Text + "'");
            if (txtBatNoE.Text != "")
                _sqlWhere.Append(" AND REPLACE(substring(" + _talbe + ".BAR_NO,13,10),'#','')<='" + txtBatNoE.Text + "'");
            #endregion

            #region 中类代号
            if (lstPrdtKnd.SelectedIndex == 1)
            {
                if (txtIdxB.Text != "")
                    _sqlWhere.Append(" AND DFP.IDX1>='" + txtIdxB.Text + "'");
                if (txtIdxE.Text != "")
                    _sqlWhere.Append(" AND DFP.IDX1<='" + txtIdxE.Text + "'");
            }
            else if (lstPrdtKnd.SelectedIndex == 2)
            {
                if (txtIdxB.Text != "")
                    _sqlWhere.Append(" AND FTP.IDX1>='" + txtIdxB.Text + "'");
                if (txtIdxE.Text != "")
                    _sqlWhere.Append(" AND FTP.IDX1<='" + txtIdxE.Text + "'");
            }
            else
            {
                if (txtIdxB.Text != "")
                    _sqlWhere.Append(" AND BCP.IDX1>='" + txtIdxB.Text + "'");
                if (txtIdxE.Text != "")
                    _sqlWhere.Append(" AND BCP.IDX1<='" + txtIdxE.Text + "'");
            }
            #endregion

            #region 品质代号
            if (lstPrdtKnd.SelectedIndex == 1)
            {
                if (txtSpcNoB.Text != "")
                    _sqlWhere.Append(" AND DFQC.SPC_NO>='" + txtSpcNoB.Text + "'");
                if (txtSpcNoE.Text != "")
                    _sqlWhere.Append(" AND DFQC.SPC_NO<='" + txtSpcNoE.Text + "'");
            }
            else if (lstPrdtKnd.SelectedIndex == 2)
            {
                if (txtSpcNoB.Text != "")
                    _sqlWhere.Append(" AND FTQC.SPC_NO>='" + txtSpcNoB.Text + "'");
                if (txtSpcNoE.Text != "")
                    _sqlWhere.Append(" AND FTQC.SPC_NO<='" + txtSpcNoE.Text + "'");
            }
            else
            {
                if (txtSpcNoB.Text != "")
                    _sqlWhere.Append(" AND BCQC.SPC_NO>='" + txtSpcNoB.Text + "'");
                if (txtSpcNoE.Text != "")
                    _sqlWhere.Append(" AND BCQC.SPC_NO<='" + txtSpcNoE.Text + "'");
            }
            #endregion

            #region 产品条码
            if (txtBarNoB.Text != "")
                _sqlWhere.Append(" AND " + _talbe + ".BAR_NO>='" + txtBarNoB.Text + "'");
            if (txtBarNoE.Text != "")
                _sqlWhere.Append(" AND " + _talbe + ".BAR_NO<='" + txtBarNoE.Text + "'");
            #endregion

            #region 箱条码
            if (txtBoxNoB.Text != "")
                _sqlWhere.Append(" AND FT.BOX_NO>='" + txtBoxNoB.Text + "'");
            if (txtBoxNoE.Text != "")
                _sqlWhere.Append(" AND FT.BOX_NO<='" + txtBoxNoE.Text + "'");
            #endregion

            DataSet _ds = new DataSet();

            onlineService.BarPrintServer _bps = new BarCodePrint.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _ds = _bps.GetBarQCRpt(_sqlWhere.ToString());

            _dsRpt.Merge(_ds, false, MissingSchemaAction.AddWithKey);

            _ds.Dispose();
            GC.Collect();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                FileExport _fx = new FileExport();
                _fx._sourceDataSet = _ds;
                Dictionary<string, string> _dict = new Dictionary<string, string>();
                _dict["BAR_NO"] = "序列号";
                _dict["BOX_NO"] = "箱号";
                _dict["WH"] = "库位代号";
                _dict["WH_NAME"] = "库位";
                _dict["IDX_NAME"] = "中类";
                _dict["SPC"] = "规格";
                _dict["PRD_NO"] = "品号";
                _dict["BAT_NO"] = "批号";
                _dict["QC"] = "品质信息";
                _dict["SPC_NAME"] = "异常原因";
                _dict["QC_REM"] = "品质备注";
                _dict["JT"] = "机台";
                _dict["PRN_DD"] = "打印时间";
                _dict["BIL_NOLIST"] = "历次单号";
                _dict["REM"] = "备注";
                _dict["DF_BAR_NO"] = "半成品序列号";
                _dict["DF_QC"] = "半成品品质信息";
                _dict["DF_SPC_NAME"] = "半成品异常原因";
                _dict["DF_QC_REM"] = "半成品品质备注";
                _dict["BC_BAR_NO"] = "板材序列号";
                _dict["BC_QC"] = "板材品质信息";
                _dict["BC_SPC_NAME"] = "板材异常原因";
                _dict["BC_QC_REM"] = "板材品质备注";
                _dict["MM_NO"] = "缴库单号";
                _fx._columnLst = _dict;
                _fx.ShowDialog();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //dataGridView1.Columns["BC_BAR_NO"].HeaderText = "序列号";
            //dataGridView1.Columns["PRD_NO"].HeaderText = "品名";
            //dataGridView1.Columns["WH"].HeaderText = "库位代号";
            //dataGridView1.Columns["WH_NAME"].HeaderText = "库位";
            //dataGridView1.Columns["JT"].HeaderText = "机台";
            //dataGridView1.Columns["PRN_DD"].HeaderText = "打印时间";
            //dataGridView1.Columns["BAT_NO"].HeaderText = "批号";
            //dataGridView1.Columns["SPC"].HeaderText = "规格";
            //dataGridView1.Columns["IDX_NAME"].HeaderText = "中类";
            //dataGridView1.Columns["SPC_NAME"].HeaderText = "异常原因";
            //dataGridView1.Columns["QC"].HeaderText = "品质信息";
            //dataGridView1.Columns["QC_REM"].HeaderText = "品质备注";
            //dataGridView1.Columns["BIL_NOLIST"].HeaderText = "历次单号";
            //dataGridView1.Columns["REM"].HeaderText = "备注";
            //dataGridView1.Columns["DF_BAR_NO"].HeaderText = "半成品序列号";
            //dataGridView1.Columns["DF_QC"].HeaderText = "半成品品质信息";
            //dataGridView1.Columns["DF_SPC_NAME"].HeaderText = "半成品异常原因";
            //dataGridView1.Columns["DF_QC_REM"].HeaderText = "半成品品质备注";
            //dataGridView1.Columns["BC_BAR_NO"].HeaderText = "板材序列号";
            //dataGridView1.Columns["BC_QC"].HeaderText = "板材品质信息";
            //dataGridView1.Columns["BC_SPC_NAME"].HeaderText = "板材异常原因";
            //dataGridView1.Columns["BC_QC_REM"].HeaderText = "板材品质备注";
            //dataGridView1.Columns["MM_NO"].HeaderText = "缴库单号";
            //dataGridView1.Columns["BOX_NO"].HeaderText = "箱号";
            //dataGridView1.Columns["CUS_NO"].Visible = false;
            //dataGridView1.Columns["STOP_ID"].Visible = false;
            //dataGridView1.Columns["DF_SPC_NO"].Visible = false;
            //dataGridView1.Columns["BC_SPC_NO"].Visible = false;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dataGridView1.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
              dataGridView1.RowHeadersDefaultCellStyle.Font,
              rectangle,
              dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }

        private void RPTBarQCLst_Load(object sender, EventArgs e)
        {
            lstPrdtKnd.SelectedIndex = 0;
            lstDateType.SelectedIndex = 0;
        }

        #region 表头

        private void qryPrdNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNoB.Text = _qw.NO_RT;
            }
        }

        private void qryPrdNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNoE.Text = _qw.NO_RT;
            }
        }

        private void qryBatNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//表名
            _qw.SQLWhere += " AND LEN(BAT_NO)=7 ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNoB.Text = _qw.NO_RT;
            }
        }

        private void qryBatNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//表名
            _qw.SQLWhere += " AND LEN(BAT_NO)=7 ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNoE.Text = _qw.NO_RT;
            }
        }

        private void qryIdxB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "INDX";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtIdxB.Text = _qw.NO_RT;
            }
        }

        private void qryIdxE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "INDX";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtIdxE.Text = _qw.NO_RT;
            }
        }

        private void txtPrdNoE_Enter(object sender, EventArgs e)
        {
            if (txtPrdNoE.Text == "")
                txtPrdNoE.Text = txtPrdNoB.Text;
        }

        private void txtBatNoE_Enter(object sender, EventArgs e)
        {
            if (txtBatNoE.Text == "")
                txtBatNoE.Text = txtBatNoB.Text;
        }

        private void txtBarNoE_Enter(object sender, EventArgs e)
        {
            if (txtBarNoE.Text == "")
                txtBarNoE.Text = txtBarNoB.Text;
        }

        private void txtBoxNoE_Enter(object sender, EventArgs e)
        {
            if (txtBoxNoE.Text == "")
                txtBoxNoE.Text = txtBoxNoB.Text;
        }

        private void txtIdxE_Enter(object sender, EventArgs e)
        {
            if (txtIdxE.Text == "")
                txtIdxE.Text = txtIdxB.Text;
        }

        private void qrySpcNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "SPC_LST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtSpcNoB.Text = _qw.NO_RT;
            }
        }

        private void qrySpcNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "SPC_LST";//表名
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtSpcNoE.Text = _qw.NO_RT;
            }
        }

        private void txtSpcNoE_Enter(object sender, EventArgs e)
        {
            if (txtSpcNoE.Text == "")
                txtSpcNoE.Text = txtSpcNoB.Text;
        }

        #endregion
    }
}