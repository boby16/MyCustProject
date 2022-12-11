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
    public partial class RPTBarQCSta : Form
    {
        private DataSet _ds = null;
        public RPTBarQCSta()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _ds.Clear();
            DateTime _dateB = System.DateTime.Now;
            DateTime _dateE = System.DateTime.Now;
            _dateB = Convert.ToDateTime(txtDateB.Text.Substring(0,10));
            _dateE = Convert.ToDateTime(txtDateE.Text.Substring(0,10));
            TimeSpan _ts = _dateE.Subtract(_dateB);

            ProcessForm _frmProcess = new ProcessForm();
            _frmProcess.progressBar1.Minimum = 0;
            _frmProcess.progressBar1.Maximum = _ts.Days * 4 + 15;
            DataSet _dsBC = new DataSet();
            DataSet _dsDF = new DataSet();
            DataSet _dsFT = new DataSet();
            DataSet _dsFT1 = new DataSet();
            DataSet _dsTemp = null;
            string _type = "";

            try
            {
                _frmProcess.Show(this);
                Application.DoEvents();
                DateTime _tmpDateTime = _dateB;
                int _days = 1;
                for (int i = 1; i <= 4; i++)
                {
                    _tmpDateTime = _dateB;
                    if (i == 1)
                        _type = "板材：";
                    else if (i == 2)
                        _type = "半成品：";
                    else
                        _type = "卷材：";
                    _dsTemp = new DataSet();
                    if (_tmpDateTime != _dateE)
                    {
                        while (_tmpDateTime <= _dateE && !_frmProcess.Cancel)
                        {
                            DateTime _dateBCurrent = _tmpDateTime;
                            DateTime _dateECurrent = _dateBCurrent.AddDays(_days);
                            if (_dateECurrent < _dateE)
                            {
                                _frmProcess.groupBox1.Text = _type + _dateBCurrent.ToString("yyyy-MM-dd") + " 至 " + _dateECurrent.ToString("yyyy-MM-dd");
                                _frmProcess.Refresh();
                                FindDataByDate(_dsTemp, _dateBCurrent, _dateECurrent, i);
                            }
                            else
                            {
                                _frmProcess.groupBox1.Text = _type + _dateBCurrent.ToString("yyyy-MM-dd") + " 至 " + _dateE.ToString("yyyy-MM-dd");
                                _frmProcess.Refresh();
                                FindDataByDate(_dsTemp, _dateBCurrent, _dateE, i);
                            }
                            _frmProcess.progressBar1.Value++;
                            Application.DoEvents();
                            _tmpDateTime = _tmpDateTime.AddDays(_days + 1);
                        }
                        if (_frmProcess.Cancel)
                            return;
                    }
                    else
                    {
                        FindDataByDate(_dsTemp, _tmpDateTime, _tmpDateTime, i);
                    }
                    if (i == 1)
                    {
                        //板材
                        _dsBC.Merge(_dsTemp.Tables[0].Select("", "BC_BAR_NO ASC"));
                    }
                    else if (i == 2)
                    {
                        //半成品
                        _dsDF.Merge(_dsTemp.Tables[0].Select("", "BC_BAR_NO ASC,DF_BAR_NO ASC"));
                    }
                    else if (i == 3)
                    {
                        //成品（经过对分）
                        _dsFT.Merge(_dsTemp.Tables[0].Select("", "BC_BAR_NO ASC,DF_BAR_NO ASC,FT_BAR_NO ASC"));
                    }
                    else
                    {
                        //成品（板材直接切成）
                        _dsFT1.Merge(_dsTemp.Tables[0].Select("", "BC_BAR_NO ASC,FT_BAR_NO ASC"));
                    }
                }
                _dsTemp.Dispose();
            }
            catch (Exception _ex)
            {
                _frmProcess.Close();
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("数据读取出错，请重试。原因：\n" + _err);
                return;
            }
            int h,j,k,l;
            h = j = k = l= 0;
            string _barNoBC = "";
            string _barNoDF = "";
            DataRow _drOld;
            while (true)
            {
                if (_dsBC == null || _dsBC.Tables.Count <= 0 || h == _dsBC.Tables[0].Rows.Count)
                {
                    break;
                }
                for (; h < _dsBC.Tables[0].Rows.Count; h++)
                {
                    _drOld = _dsBC.Tables[0].Rows[h];
                    if (_barNoBC == "")
                    {
                        _barNoBC = _drOld["BC_BAR_NO"].ToString();
                        //插入当前记录 
                        //_drNew = _ds.Tables[0].NewRow();
                        //_drNew[""] = _drOld[""];
                        //_ds.Tables[0].Rows.Add(_drNew);
                        _ds.Tables[0].ImportRow(_drOld);
                    }
                    else
                    {
                        if (_barNoBC == _dsBC.Tables[0].Rows[h]["BC_BAR_NO"].ToString())
                        {
                            //插入当前记录 
                            //_drNew = _ds.Tables[0].NewRow();
                            //_drNew[""] = _drOld[""];
                            //_ds.Tables[0].Rows.Add(_drNew);
                            _ds.Tables[0].ImportRow(_drOld);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                for (; _dsFT1!= null && _dsFT1.Tables.Count > 0 && l < _dsFT1.Tables[0].Rows.Count; l++)
                {
                    if (_barNoBC == _dsFT1.Tables[0].Rows[l]["BC_BAR_NO"].ToString())
                    {
                        //板材直接切成成品
                        //插入数据
                        _drOld = _dsFT1.Tables[0].Rows[l];
                        //_drNew = _ds.Tables[0].NewRow();
                        //_drNew[""] = _drOld[""];
                        //_ds.Tables[0].Rows.Add(_drNew);
                        _ds.Tables[0].ImportRow(_drOld);
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    if (_dsDF == null || _dsDF.Tables.Count <= 0 || j == _dsDF.Tables[0].Rows.Count)
                    {
                        _barNoBC = "";
                        break;
                    }
                    for (; j < _dsDF.Tables[0].Rows.Count; j++)
                    {
                        #region 半成品对分
                        if (_barNoBC == _dsDF.Tables[0].Rows[j]["BC_BAR_NO"].ToString())
                        {
                            _drOld = _dsDF.Tables[0].Rows[j];
                            //板材对分后生成半成品
                            if (_barNoDF == "")
                            {
                                //_barNoDF为空，表示_ds中还没有半成品序列号_barNoDF
                                _barNoDF = _dsDF.Tables[0].Rows[j]["DF_BAR_NO"].ToString();
                                //插入当前记录 
                                //_drNew = _ds.Tables[0].NewRow();
                                //_drNew[""] = _drOld[""];
                                //_ds.Tables[0].Rows.Add(_drNew);
                                _ds.Tables[0].ImportRow(_drOld);
                            }
                            else
                            {
                                if (_barNoDF == _dsDF.Tables[0].Rows[j]["DF_BAR_NO"].ToString())
                                {
                                    //表示_ds中有多个半成品序列号_barNoDF
                                    //插入当前记录
                                    //_drNew = _ds.Tables[0].NewRow();
                                    //_drNew[""] = _drOld[""];
                                    //_ds.Tables[0].Rows.Add(_drNew);
                                    _ds.Tables[0].ImportRow(_drOld);
                                }
                                else
                                { 
                                    break;
                                }
                            }
                        }
                        else
                        {
                            _barNoBC = "";
                            break;
                        }
                        #endregion
                    }
                    
                    if (_barNoBC == "")
                        break;
                    //如果_dsFT表记录已经循环到最后一笔，则清空_barNoDF，表明不用再循环_dsFT，继续循环dsDF半成品记录
                    if (_dsFT == null || _dsFT.Tables.Count <= 0 || k == _dsFT.Tables[0].Rows.Count)
                    {
                        _barNoDF = "";
                    }
                    else
                    {
                        for (; k < _dsFT.Tables[0].Rows.Count; k++)
                        {
                            if (_barNoDF == _dsFT.Tables[0].Rows[k]["DF_BAR_NO"].ToString())
                            {
                                _drOld = _dsFT.Tables[0].Rows[k];
                                //_drNew = _ds.Tables[0].NewRow();
                                //_drNew[""] = _drOld[""];
                                //_ds.Tables[0].Rows.Add(_drNew);
                                _ds.Tables[0].ImportRow(_drOld);
                            }
                            else
                            {
                                _barNoDF = "";//清空_barNoDF，开始while新的循环，即继续循环_dsDF半成品记录
                                break;
                            }
                        }
                    }
                }
            }
            _dsBC.Dispose();
            _dsDF.Dispose();
            _dsFT.Dispose();
            _dsFT1.Dispose();
            GC.Collect();

            _frmProcess.progressBar1.Value += 5;

            if (_ds != null && _ds.Tables.Count > 0)
            {
                #region 资源转换

                #region 修改Columns属性

                DataColumnCollection _dc = _ds.Tables[0].Columns;
                _dc["BC_WH_NAME"].ReadOnly = false;
                _dc["BC_FORMAT"].ReadOnly = false;
                _dc["BC_FORMAT"].MaxLength = 10;
                _dc["BC_SPC_NAME"].ReadOnly = false;
                _dc["BC_QTY"].MaxLength = 28;
                _dc["BC_QC"].ReadOnly = false;
                _dc["BC_QC"].MaxLength = 10;
                _dc["BC_QC_REM"].ReadOnly = false;
                _dc["BC_PRC_ID"].ReadOnly = false;
                _dc["BC_PRC_ID"].MaxLength = 10;
                _dc["BC_MO_CUS_NAME"].ReadOnly = false;
                _dc["BC_SJ_WH_NAME"].ReadOnly = false;
                _dc["BC_MM_WH_NAME"].ReadOnly = false;
                _dc["BC_SA_CUS_NAME"].ReadOnly = false;
                _dc["BC_SB_REM"].ReadOnly = false;

                _dc["DF_WH_NAME"].ReadOnly = false;
                _dc["DF_SPC_NAME"].ReadOnly = false;
                _dc["DF_QTY"].MaxLength = 28;
                _dc["DF_QC"].ReadOnly = false;
                _dc["DF_QC"].MaxLength = 10;
                _dc["DF_QC_REM"].ReadOnly = false;
                _dc["DF_PRC_ID"].ReadOnly = false;
                _dc["DF_PRC_ID"].MaxLength = 10;
                _dc["DF_MO_CUS_NAME"].ReadOnly = false;
                _dc["DF_SJ_WH_NAME"].ReadOnly = false;
                _dc["DF_MM_WH_NAME"].ReadOnly = false;
                _dc["DF_SA_CUS_NAME"].ReadOnly = false;
                _dc["DF_SB_REM"].ReadOnly = false;

                _dc["FT_WH_NAME"].ReadOnly = false;
                _dc["BOX_FORMAT"].ReadOnly = false;
                _dc["BOX_FORMAT"].MaxLength = 10;
                _dc["FT_SPC_NAME"].ReadOnly = false;
                _dc["FT_QTY"].MaxLength = 28;
                _dc["FT_QC"].ReadOnly = false;
                _dc["FT_QC"].MaxLength = 10;
                _dc["FT_QC_REM"].ReadOnly = false;
                _dc["FT_PRC_ID"].ReadOnly = false;
                _dc["FT_PRC_ID"].MaxLength = 10;
                _dc["FT_MO_CUS_NAME"].ReadOnly = false;
                _dc["FT_SJ_WH_NAME"].ReadOnly = false;
                _dc["FT_MM_WH_NAME"].ReadOnly = false;
                _dc["FT_SA_CUS_NAME"].ReadOnly = false;
                _dc["FT_SB_REM"].ReadOnly = false;

                #endregion

                foreach (DataRow dr in _ds.Tables[0].Rows)
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
                        dr["BC_QC"] = "合格";
                    else if (dr["BC_QC"].ToString() == "A")
                        dr["BC_QC"] = "A级品";
                    else if (dr["BC_QC"].ToString() == "B")
                        dr["BC_QC"] = "B级品";
                    else if (dr["BC_QC"].ToString() == "2")
                        dr["BC_QC"] = "不合格";
                    else if (dr["BC_QC"].ToString() == "N")
                        dr["BC_QC"] = "期初库存";

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
                        dr["DF_QC"] = "合格";
                    else if (dr["DF_QC"].ToString() == "A")
                        dr["DF_QC"] = "A级品";
                    else if (dr["DF_QC"].ToString() == "B")
                        dr["DF_QC"] = "B级品";
                    else if (dr["DF_QC"].ToString() == "2")
                        dr["DF_QC"] = "不合格";
                    else if (dr["DF_QC"].ToString() == "N")
                        dr["DF_QC"] = "期初库存";

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
                    else if (dr["BOX_FORMAT"].ToString() == "5")
                    {
                        dr["BOX_FORMAT"] = "富士康";
                    }

                    #endregion

                    #region 品检信息

                    if (dr["FT_QC"].ToString() == "1")
                        dr["FT_QC"] = "合格";
                    else if (dr["FT_QC"].ToString() == "A")
                        dr["FT_QC"] = "A级品";
                    else if (dr["FT_QC"].ToString() == "B")
                        dr["FT_QC"] = "B级品";
                    else if (dr["FT_QC"].ToString() == "2")
                        dr["FT_QC"] = "不合格";
                    else if (dr["FT_QC"].ToString() == "N")
                        dr["FT_QC"] = "期初库存";

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

            _frmProcess.progressBar1.Value += 5;

            if (_ds != null && _ds.Tables.Count > 0)
            {
                //DataView _dv = _ds.Tables[0].DefaultView;
                //_dv.Sort = "BC_BAR_NO,DF_BAR_NO,FT_BAR_NO";
                dataGridView1.DataSource = _ds.Tables[0];
                //dataGridView1.Refresh();
            }

            foreach (DataGridViewColumn _dc in dataGridView1.Columns)
            {
                if (_dc.Name != "BC_BAR_NO" && _dc.Name != "BC_BAT_NO")
                    _dc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            _frmProcess.progressBar1.Value += 5;

            _frmProcess.Close();
        }

        private void FindDataByDate(DataSet _dsRpt, DateTime beginDate, DateTime endDate, int i)
        {
            StringBuilder _sqlWhere = new StringBuilder();
            _sqlWhere.Append(" AND LEN(REPLACE(A.BAR_NO,'#',''))=22");

            #region 起止时间

            if (lstDateType.SelectedIndex == 1)
            {
                //送缴时间
                _sqlWhere.Append(" AND BCQC.SJ_DATE>='" + txtDateB.Text + ":00' AND BCQC.SJ_DATE<='" + txtDateE.Text + ":59' ")
                         .Append(" and BCQC.SJ_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCQC.SJ_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ");
            }
            else if (lstDateType.SelectedIndex == 2)
            {
                //销货时间
                _sqlWhere.Append(" AND BCSA.CLS_DATE>='" + txtDateB.Text + ":00' AND BCSA.CLS_DATE<='" + txtDateE.Text + ":59' ")
                         .Append(" and BCSA.CLS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCSA.CLS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ");
            }
            else if (lstDateType.SelectedIndex == 3)
            {
                //退货时间
                _sqlWhere.Append(" AND BCSB.SYS_DATE>='" + txtDateB.Text + ":00' AND BCSB.SYS_DATE<='" + txtDateE.Text + ":59' ")
                         .Append(" and BCSB.SYS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCSB.SYS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ");
            }
            else
            {
                //品检时间
                _sqlWhere.Append(" AND BCQC.QC_DATE>='" + txtDateB.Text + ":00' AND BCQC.QC_DATE<='" + txtDateE.Text + ":59' ")
                         .Append(" and BCQC.QC_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCQC.QC_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ");
            }

            #endregion

            #region 品号
            if (txtPrdNoB.Text != "")
                _sqlWhere.Append(" AND A.PRD_NO>='" + txtPrdNoB.Text + "'");
            if (txtPrdNoE.Text != "")
                _sqlWhere.Append(" AND A.PRD_NO<='" + txtPrdNoE.Text + "'");
            #endregion

            #region 批号
            if (txtBatNoB.Text != "")
                _sqlWhere.Append(" AND REPLACE(substring(A.BAR_NO,13,10),'#','')>='" + txtBatNoB.Text + "'");
            if (txtBatNoE.Text != "")
                _sqlWhere.Append(" AND REPLACE(substring(A.BAR_NO,13,10),'#','')<='" + txtBatNoE.Text + "'");
            #endregion

            #region 中类代号
                if (txtIdxB.Text != "")
                    _sqlWhere.Append(" AND BCP.IDX1>='" + txtIdxB.Text + "'");
                if (txtIdxE.Text != "")
                    _sqlWhere.Append(" AND BCP.IDX1<='" + txtIdxE.Text + "'");
            #endregion

            #region 品质代号
                if (txtSpcNoB.Text != "")
                    _sqlWhere.Append(" AND BCQC.SPC_NO>='" + txtSpcNoB.Text + "'");
                if (txtSpcNoE.Text != "")
                    _sqlWhere.Append(" AND BCQC.SPC_NO<='" + txtSpcNoE.Text + "'");
            #endregion

            #region 产品条码
            if (txtBarNoB.Text != "")
                _sqlWhere.Append(" AND A.BAR_NO>='" + txtBarNoB.Text + "'");
            if (txtBarNoE.Text != "")
                _sqlWhere.Append(" AND A.BAR_NO<='" + txtBarNoE.Text + "'");
            #endregion

            DataSet _ds = new DataSet();

            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _ds = _bps.GetBarQCSta(_sqlWhere.ToString(), i);

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

                _dict["BC_BAR_NO"] = "板材序列号";
                _dict["BC_PRD_NO"] = "板材品号";
                _dict["BC_WH_NAME"] = "板材库位";
                _dict["BC_IDX_NAME"] = "板材中类";
                _dict["BC_SPC"] = "板材规格";
                _dict["BC_QTY"] = "板材重量";
                _dict["BC_BAT_NO"] = "板材批号";
                _dict["BC_SN"] = "板材流水号";
                _dict["BC_FORMAT"] = "板材打印版式";
                _dict["BC_LH"] = "板材料号";

                _dict["BC_PRT_NAME"] = "板材打印品名";
                _dict["BC_QC"] = "板材品质信息";
                _dict["BC_PRC_ID"] = "板材处理方式";
                _dict["BC_SPC_NAME"] = "板材异常原因";
                _dict["BC_QC_REM"] = "板材品质备注";
                _dict["BC_BIL_TYPE"] = "制令单类别";
                _dict["BC_MO_NO"] = "送缴制令单";
                _dict["BC_MO_CUS_NAME"] = "送缴客户";
                _dict["BC_SJ_WH_NAME"] = "送缴库位";
                _dict["BC_MM_NO"] = "板材缴库单";

                _dict["BC_MM_WH_NAME"] = "缴库库位";
                _dict["BC_SA_NO"] = "板材销货单";
                _dict["BC_SA_CUS_NAME"] = "板材销货客户";
                _dict["BC_SB_NO"] = "板材退货单号";
                _dict["BC_SB_REM"] = "板材退货备注";
                _dict["DF_ML_NO"] = "半成品领料单";


                _dict["DF_BAR_NO"] = "半成品序列号";
                _dict["DF_PRD_NO"] = "半成品品号";
                _dict["DF_WH_NAME"] = "半成品库位";
                _dict["DF_IDX_NAME"] = "半成品中类";
                _dict["DF_SPC"] = "半成品规格";
                _dict["DF_QTY"] = "半成品重量";
                _dict["DF_BAT_NO"] = "半成品批号";
                _dict["DF_SN"] = "半成品流水号";
                _dict["DF_QC"] = "半成品品质信息";
                _dict["DF_PRC_ID"] = "半成品处理方式";

                _dict["DF_SPC_NAME"] = "半成品异常原因";
                _dict["DF_QC_REM"] = "半成品品质备注";
                _dict["DF_BIL_TYPE"] = "制令单类别";
                _dict["DF_MO_NO"] = "送缴制令单";
                _dict["DF_MO_CUS_NAME"] = "送缴客户";
                _dict["DF_SJ_WH_NAME"] = "送缴库位";
                _dict["DF_MM_NO"] = "半成品缴库单";
                _dict["DF_MM_WH_NAME"] = "缴库库位";
                _dict["DF_SA_NO"] = "半成品销货单";
                _dict["DF_SA_CUS_NAME"] = "半成品销货客户";

                _dict["DF_SB_NO"] = "半成品退货单号";
                _dict["DF_SB_REM"] = "半成品退货备注";
                _dict["FT_ML_NO"] = "半成品领料单";


                _dict["FT_BAR_NO"] = "卷材序列号";
                _dict["FT_PRD_NO"] = "卷材品号";
                _dict["FT_WH_NAME"] = "卷材库位";
                _dict["FT_IDX_NAME"] = "卷材中类";
                _dict["FT_SPC"] = "卷材规格";
                _dict["FT_QTY"] = "卷材重量";
                _dict["FT_BAT_NO"] = "卷材批号";
                _dict["FT_SN"] = "卷材流水号";
                _dict["FT_QC"] = "卷材品质信息";
                _dict["FT_PRC_ID"] = "卷材处理方式";

                _dict["FT_SPC_NAME"] = "卷材异常原因";
                _dict["FT_QC_REM"] = "卷材品质备注";
                _dict["BOX_NO"] = "外箱条码";
                _dict["BOX_PRD_NO"] = "外箱品号";
                _dict["BOX_IDX_NAME"] = "外箱中类";
                _dict["BOX_SPC"] = "外箱规格";
                _dict["BOX_QTY"] = "外箱数量";
                _dict["BOX_PRT_NAME"] = "外箱打印品名";
                _dict["BOX_FORMAT"] = "外箱打印版式";
                _dict["FT_BIL_TYPE"] = "制令单类别";

                _dict["FT_MO_NO"] = "送缴制令单";
                _dict["FT_MO_CUS_NAME"] = "送缴客户";
                _dict["FT_SJ_WH_NAME"] = "送缴库位";
                _dict["FT_MM_NO"] = "卷材缴库单";
                _dict["FT_MM_WH_NAME"] = "缴库库位";
                _dict["FT_SA_NO"] = "卷材销货单";
                _dict["FT_SA_CUS_NAME"] = "卷材销货客户";
                _dict["FT_SB_NO"] = "卷材退货单号";
                _dict["FT_SB_REM"] = "卷材退货备注";
                _fx._columnLst = _dict;
                _fx.ShowDialog();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BC_BAR_NO"].HeaderText = "板材序列号";
            dataGridView1.Columns["BC_PRD_NO"].HeaderText = "板材品号";
            dataGridView1.Columns["BC_WH_NAME"].HeaderText = "板材库位";
            dataGridView1.Columns["BC_IDX_NAME"].HeaderText = "板材中类";
            dataGridView1.Columns["BC_SPC"].HeaderText = "板材规格";
            dataGridView1.Columns["BC_QTY"].HeaderText = "板材重量";
            dataGridView1.Columns["BC_BAT_NO"].HeaderText = "板材批号";
            dataGridView1.Columns["BC_SN"].HeaderText = "板材流水号";
            dataGridView1.Columns["BC_FORMAT"].HeaderText = "板材打印版式";
            dataGridView1.Columns["BC_LH"].HeaderText = "板材料号";

            dataGridView1.Columns["BC_PRT_NAME"].HeaderText = "板材打印品名";
            dataGridView1.Columns["BC_QC"].HeaderText = "板材品质信息";
            dataGridView1.Columns["BC_PRC_ID"].HeaderText = "板材处理方式";
            dataGridView1.Columns["BC_SPC_NAME"].HeaderText = "板材异常原因";
            dataGridView1.Columns["BC_QC_REM"].HeaderText = "板材品质备注";
            dataGridView1.Columns["BC_BIL_TYPE"].HeaderText = "制令单类别";
            dataGridView1.Columns["BC_MO_NO"].HeaderText = "送缴制令单";
            dataGridView1.Columns["BC_MO_CUS_NAME"].HeaderText = "送缴客户";
            dataGridView1.Columns["BC_SJ_WH_NAME"].HeaderText = "送缴库位";
            dataGridView1.Columns["BC_MM_NO"].HeaderText = "板材缴库单";

            dataGridView1.Columns["BC_MM_WH_NAME"].HeaderText = "缴库库位";
            dataGridView1.Columns["BC_SA_NO"].HeaderText = "板材销货单";
            dataGridView1.Columns["BC_SA_CUS_NAME"].HeaderText = "板材销货客户";
            dataGridView1.Columns["BC_SB_NO"].HeaderText = "板材退货单号";
            dataGridView1.Columns["BC_SB_REM"].HeaderText = "板材退货备注";
            dataGridView1.Columns["DF_ML_NO"].HeaderText = "半成品领料单";


            dataGridView1.Columns["DF_BAR_NO"].HeaderText = "半成品序列号";
            dataGridView1.Columns["DF_PRD_NO"].HeaderText = "半成品品号";
            dataGridView1.Columns["DF_WH_NAME"].HeaderText = "半成品库位";
            dataGridView1.Columns["DF_IDX_NAME"].HeaderText = "半成品中类";
            dataGridView1.Columns["DF_SPC"].HeaderText = "半成品规格";
            dataGridView1.Columns["DF_QTY"].HeaderText = "半成品重量";
            dataGridView1.Columns["DF_BAT_NO"].HeaderText = "半成品批号";
            dataGridView1.Columns["DF_SN"].HeaderText = "半成品流水号";
            dataGridView1.Columns["DF_QC"].HeaderText = "半成品品质信息";
            dataGridView1.Columns["DF_PRC_ID"].HeaderText = "半成品处理方式";

            dataGridView1.Columns["DF_SPC_NAME"].HeaderText = "半成品异常原因";
            dataGridView1.Columns["DF_QC_REM"].HeaderText = "半成品品质备注";
            dataGridView1.Columns["DF_BIL_TYPE"].HeaderText = "制令单类别";
            dataGridView1.Columns["DF_MO_NO"].HeaderText = "送缴制令单";
            dataGridView1.Columns["DF_MO_CUS_NAME"].HeaderText = "送缴客户";
            dataGridView1.Columns["DF_SJ_WH_NAME"].HeaderText = "送缴库位";
            dataGridView1.Columns["DF_MM_NO"].HeaderText = "半成品缴库单";
            dataGridView1.Columns["DF_MM_WH_NAME"].HeaderText = "缴库库位";
            dataGridView1.Columns["DF_SA_NO"].HeaderText = "半成品销货单";
            dataGridView1.Columns["DF_SA_CUS_NAME"].HeaderText = "半成品销货客户";

            dataGridView1.Columns["DF_SB_NO"].HeaderText = "半成品退货单号";
            dataGridView1.Columns["DF_SB_REM"].HeaderText = "半成品退货备注";
            dataGridView1.Columns["FT_ML_NO"].HeaderText = "半成品领料单";


            dataGridView1.Columns["FT_BAR_NO"].HeaderText = "卷材序列号";
            dataGridView1.Columns["FT_PRD_NO"].HeaderText = "卷材品号";
            dataGridView1.Columns["FT_WH_NAME"].HeaderText = "卷材库位";
            dataGridView1.Columns["FT_IDX_NAME"].HeaderText = "卷材中类";
            dataGridView1.Columns["FT_SPC"].HeaderText = "卷材规格"; ;
            dataGridView1.Columns["FT_QTY"].HeaderText = "卷材重量";
            dataGridView1.Columns["FT_BAT_NO"].HeaderText = "卷材批号";
            dataGridView1.Columns["FT_SN"].HeaderText = "卷材流水号";
            dataGridView1.Columns["FT_QC"].HeaderText = "卷材品质信息";
            dataGridView1.Columns["FT_PRC_ID"].HeaderText = "卷材处理方式";

            dataGridView1.Columns["FT_SPC_NAME"].HeaderText = "卷材异常原因";
            dataGridView1.Columns["FT_QC_REM"].HeaderText = "卷材品质备注";
            dataGridView1.Columns["BOX_NO"].HeaderText = "外箱条码";
            dataGridView1.Columns["BOX_PRD_NO"].HeaderText = "外箱品号";
            dataGridView1.Columns["BOX_IDX_NAME"].HeaderText = "外箱中类";
            dataGridView1.Columns["BOX_SPC"].HeaderText = "外箱规格";
            dataGridView1.Columns["BOX_QTY"].HeaderText = "外箱数量";
            dataGridView1.Columns["BOX_PRT_NAME"].HeaderText = "外箱打印品名";
            dataGridView1.Columns["BOX_FORMAT"].HeaderText = "外箱打印版式";
            dataGridView1.Columns["FT_BIL_TYPE"].HeaderText = "制令单类别";

            dataGridView1.Columns["FT_MO_NO"].HeaderText = "送缴制令单";
            dataGridView1.Columns["FT_MO_CUS_NAME"].HeaderText = "送缴客户";
            dataGridView1.Columns["FT_SJ_WH_NAME"].HeaderText = "送缴库位";
            dataGridView1.Columns["FT_MM_NO"].HeaderText = "卷材缴库单";
            dataGridView1.Columns["FT_MM_WH_NAME"].HeaderText = "缴库库位";
            dataGridView1.Columns["FT_SA_NO"].HeaderText = "卷材销货单";
            dataGridView1.Columns["FT_SA_CUS_NAME"].HeaderText = "卷材销货客户";
            dataGridView1.Columns["FT_SB_NO"].HeaderText = "卷材退货单号";
            dataGridView1.Columns["FT_SB_REM"].HeaderText = "卷材退货备注";
            if (txtBarNoB.Text != "" || txtBarNoE.Text != "")
                dataGridView1.Columns["BC_BAT_NO"].DisplayIndex = 0;
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

        private void RPTBarQCSta_Load(object sender, EventArgs e)
        {
            lstDateType.SelectedIndex = 0;
            _ds = new DataSet();
            DataTable _dt = new DataTable("BARQC_RPT");
            _dt.Columns.Add("BC_BAR_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_PRD_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_IDX_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_SPC", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_QTY", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_BAT_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_SN", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_FORMAT", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_LH", System.Type.GetType("System.String"));

            _dt.Columns.Add("BC_PRT_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_QC", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_PRC_ID", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_SPC_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_QC_REM", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_BIL_TYPE", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_MO_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_MO_CUS_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_SJ_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_MM_NO", System.Type.GetType("System.String"));

            _dt.Columns.Add("BC_MM_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_SA_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_SA_CUS_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_SB_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("BC_SB_REM", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_ML_NO", System.Type.GetType("System.String"));


            _dt.Columns.Add("DF_BAR_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_PRD_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_IDX_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_SPC", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_QTY", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_BAT_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_SN", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_QC", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_PRC_ID", System.Type.GetType("System.String"));

            _dt.Columns.Add("DF_SPC_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_QC_REM", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_BIL_TYPE", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_MO_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_MO_CUS_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_SJ_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_MM_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_MM_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_SA_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_SA_CUS_NAME", System.Type.GetType("System.String"));

            _dt.Columns.Add("DF_SB_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("DF_SB_REM", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_ML_NO", System.Type.GetType("System.String"));


            _dt.Columns.Add("FT_BAR_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_PRD_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_IDX_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_SPC", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_QTY", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_BAT_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_SN", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_QC", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_PRC_ID", System.Type.GetType("System.String"));

            _dt.Columns.Add("FT_SPC_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_QC_REM", System.Type.GetType("System.String"));
            _dt.Columns.Add("BOX_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("BOX_PRD_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("BOX_IDX_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BOX_SPC", System.Type.GetType("System.String"));
            _dt.Columns.Add("BOX_QTY", System.Type.GetType("System.String"));
            _dt.Columns.Add("BOX_PRT_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("BOX_FORMAT", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_BIL_TYPE", System.Type.GetType("System.String"));

            _dt.Columns.Add("FT_MO_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_MO_CUS_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_SJ_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_MM_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_MM_WH_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_SA_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_SA_CUS_NAME", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_SB_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("FT_SB_REM", System.Type.GetType("System.String"));
            _ds.Tables.Add(_dt);

            txtDateB.Text = System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00";
            txtDateE.Text = System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59";
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

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!dataGridView1.Columns[e.ColumnIndex].Frozen)
            {
                dataGridView1.Columns[e.ColumnIndex].Frozen = true;
                dataGridView1.Columns[e.ColumnIndex].DisplayIndex = 0;
            }
            else
                dataGridView1.Columns[e.ColumnIndex].Frozen = false;
        }
    }
}