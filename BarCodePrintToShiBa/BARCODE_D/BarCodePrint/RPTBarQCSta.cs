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
                        _type = "��ģ�";
                    else if (i == 2)
                        _type = "���Ʒ��";
                    else
                        _type = "��ģ�";
                    _dsTemp = new DataSet();
                    if (_tmpDateTime != _dateE)
                    {
                        while (_tmpDateTime <= _dateE && !_frmProcess.Cancel)
                        {
                            DateTime _dateBCurrent = _tmpDateTime;
                            DateTime _dateECurrent = _dateBCurrent.AddDays(_days);
                            if (_dateECurrent < _dateE)
                            {
                                _frmProcess.groupBox1.Text = _type + _dateBCurrent.ToString("yyyy-MM-dd") + " �� " + _dateECurrent.ToString("yyyy-MM-dd");
                                _frmProcess.Refresh();
                                FindDataByDate(_dsTemp, _dateBCurrent, _dateECurrent, i);
                            }
                            else
                            {
                                _frmProcess.groupBox1.Text = _type + _dateBCurrent.ToString("yyyy-MM-dd") + " �� " + _dateE.ToString("yyyy-MM-dd");
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
                        //���
                        _dsBC.Merge(_dsTemp.Tables[0].Select("", "BC_BAR_NO ASC"));
                    }
                    else if (i == 2)
                    {
                        //���Ʒ
                        _dsDF.Merge(_dsTemp.Tables[0].Select("", "BC_BAR_NO ASC,DF_BAR_NO ASC"));
                    }
                    else if (i == 3)
                    {
                        //��Ʒ�������Է֣�
                        _dsFT.Merge(_dsTemp.Tables[0].Select("", "BC_BAR_NO ASC,DF_BAR_NO ASC,FT_BAR_NO ASC"));
                    }
                    else
                    {
                        //��Ʒ�����ֱ���гɣ�
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
                MessageBox.Show("���ݶ�ȡ���������ԡ�ԭ��\n" + _err);
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
                        //���뵱ǰ��¼ 
                        //_drNew = _ds.Tables[0].NewRow();
                        //_drNew[""] = _drOld[""];
                        //_ds.Tables[0].Rows.Add(_drNew);
                        _ds.Tables[0].ImportRow(_drOld);
                    }
                    else
                    {
                        if (_barNoBC == _dsBC.Tables[0].Rows[h]["BC_BAR_NO"].ToString())
                        {
                            //���뵱ǰ��¼ 
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
                        //���ֱ���гɳ�Ʒ
                        //��������
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
                        #region ���Ʒ�Է�
                        if (_barNoBC == _dsDF.Tables[0].Rows[j]["BC_BAR_NO"].ToString())
                        {
                            _drOld = _dsDF.Tables[0].Rows[j];
                            //��ĶԷֺ����ɰ��Ʒ
                            if (_barNoDF == "")
                            {
                                //_barNoDFΪ�գ���ʾ_ds�л�û�а��Ʒ���к�_barNoDF
                                _barNoDF = _dsDF.Tables[0].Rows[j]["DF_BAR_NO"].ToString();
                                //���뵱ǰ��¼ 
                                //_drNew = _ds.Tables[0].NewRow();
                                //_drNew[""] = _drOld[""];
                                //_ds.Tables[0].Rows.Add(_drNew);
                                _ds.Tables[0].ImportRow(_drOld);
                            }
                            else
                            {
                                if (_barNoDF == _dsDF.Tables[0].Rows[j]["DF_BAR_NO"].ToString())
                                {
                                    //��ʾ_ds���ж�����Ʒ���к�_barNoDF
                                    //���뵱ǰ��¼
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
                    //���_dsFT���¼�Ѿ�ѭ�������һ�ʣ������_barNoDF������������ѭ��_dsFT������ѭ��dsDF���Ʒ��¼
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
                                _barNoDF = "";//���_barNoDF����ʼwhile�µ�ѭ����������ѭ��_dsDF���Ʒ��¼
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
                #region ��Դת��

                #region �޸�Columns����

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
                    #region ���

                    #region ��ӡ��ʽ

                    if (dr["BC_FORMAT"].ToString() == "1")
                    {
                        dr["BC_FORMAT"] = "��׼��";
                    }
                    else if (dr["BC_FORMAT"].ToString() == "2")
                    {
                        dr["BC_FORMAT"] = "��ӡ�Ϻ�";
                    }
                    else if (dr["BC_FORMAT"].ToString() == "3")
                    {
                        dr["BC_FORMAT"] = "�����";
                    }

                    #endregion

                    #region Ʒ����Ϣ

                    if (dr["BC_QC"].ToString() == "1")
                        dr["BC_QC"] = "�ϸ�";
                    else if (dr["BC_QC"].ToString() == "A")
                        dr["BC_QC"] = "A��Ʒ";
                    else if (dr["BC_QC"].ToString() == "B")
                        dr["BC_QC"] = "B��Ʒ";
                    else if (dr["BC_QC"].ToString() == "2")
                        dr["BC_QC"] = "���ϸ�";
                    else if (dr["BC_QC"].ToString() == "N")
                        dr["BC_QC"] = "�ڳ����";

                    #endregion

                    #region ����ʽ

                    if (dr["BC_PRC_ID"].ToString() == "1")
                    {
                        dr["BC_PRC_ID"] = "ǿ�ƽɿ�";
                    }
                    else if (dr["BC_PRC_ID"].ToString() == "2")
                    {
                        dr["BC_PRC_ID"] = "����";
                    }
                    else if (dr["BC_PRC_ID"].ToString() == "3")
                    {
                        dr["BC_PRC_ID"] = "�ؿ�����";
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

                    #region ���Ʒ

                    #region Ʒ����Ϣ

                    if (dr["DF_QC"].ToString() == "1")
                        dr["DF_QC"] = "�ϸ�";
                    else if (dr["DF_QC"].ToString() == "A")
                        dr["DF_QC"] = "A��Ʒ";
                    else if (dr["DF_QC"].ToString() == "B")
                        dr["DF_QC"] = "B��Ʒ";
                    else if (dr["DF_QC"].ToString() == "2")
                        dr["DF_QC"] = "���ϸ�";
                    else if (dr["DF_QC"].ToString() == "N")
                        dr["DF_QC"] = "�ڳ����";

                    #endregion

                    #region ����ʽ

                    if (dr["DF_PRC_ID"].ToString() == "1")
                    {
                        dr["DF_PRC_ID"] = "ǿ�ƽɿ�";
                    }
                    else if (dr["DF_PRC_ID"].ToString() == "2")
                    {
                        dr["DF_PRC_ID"] = "����";
                    }
                    else if (dr["DF_PRC_ID"].ToString() == "3")
                    {
                        dr["DF_PRC_ID"] = "�ؿ�����";
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

                    #region ��Ʒ

                    #region ��ӡ��ʽ

                    if (dr["BOX_FORMAT"].ToString() == "1")
                    {
                        dr["BOX_FORMAT"] = "��׼��";
                    }
                    else if (dr["BOX_FORMAT"].ToString() == "2")
                    {
                        dr["BOX_FORMAT"] = "��Լ��";
                    }
                    else if (dr["BOX_FORMAT"].ToString() == "3")
                    {
                        dr["BOX_FORMAT"] = "��ӡ���";
                    }
                    else if (dr["BOX_FORMAT"].ToString() == "4")
                    {
                        dr["BOX_FORMAT"] = "�����";
                    }
                    else if (dr["BOX_FORMAT"].ToString() == "5")
                    {
                        dr["BOX_FORMAT"] = "��ʿ��";
                    }

                    #endregion

                    #region Ʒ����Ϣ

                    if (dr["FT_QC"].ToString() == "1")
                        dr["FT_QC"] = "�ϸ�";
                    else if (dr["FT_QC"].ToString() == "A")
                        dr["FT_QC"] = "A��Ʒ";
                    else if (dr["FT_QC"].ToString() == "B")
                        dr["FT_QC"] = "B��Ʒ";
                    else if (dr["FT_QC"].ToString() == "2")
                        dr["FT_QC"] = "���ϸ�";
                    else if (dr["FT_QC"].ToString() == "N")
                        dr["FT_QC"] = "�ڳ����";

                    #endregion

                    #region ����ʽ

                    if (dr["FT_PRC_ID"].ToString() == "1")
                    {
                        dr["FT_PRC_ID"] = "ǿ�ƽɿ�";
                    }
                    else if (dr["FT_PRC_ID"].ToString() == "2")
                    {
                        dr["FT_PRC_ID"] = "����";
                    }
                    else if (dr["FT_PRC_ID"].ToString() == "3")
                    {
                        dr["FT_PRC_ID"] = "�ؿ�����";
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

            #region ��ֹʱ��

            if (lstDateType.SelectedIndex == 1)
            {
                //�ͽ�ʱ��
                _sqlWhere.Append(" AND BCQC.SJ_DATE>='" + txtDateB.Text + ":00' AND BCQC.SJ_DATE<='" + txtDateE.Text + ":59' ")
                         .Append(" and BCQC.SJ_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCQC.SJ_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ");
            }
            else if (lstDateType.SelectedIndex == 2)
            {
                //����ʱ��
                _sqlWhere.Append(" AND BCSA.CLS_DATE>='" + txtDateB.Text + ":00' AND BCSA.CLS_DATE<='" + txtDateE.Text + ":59' ")
                         .Append(" and BCSA.CLS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCSA.CLS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ");
            }
            else if (lstDateType.SelectedIndex == 3)
            {
                //�˻�ʱ��
                _sqlWhere.Append(" AND BCSB.SYS_DATE>='" + txtDateB.Text + ":00' AND BCSB.SYS_DATE<='" + txtDateE.Text + ":59' ")
                         .Append(" and BCSB.SYS_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCSB.SYS_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ");
            }
            else
            {
                //Ʒ��ʱ��
                _sqlWhere.Append(" AND BCQC.QC_DATE>='" + txtDateB.Text + ":00' AND BCQC.QC_DATE<='" + txtDateE.Text + ":59' ")
                         .Append(" and BCQC.QC_DATE>='" + beginDate.ToString("yyyy-MM-dd") + " 00:00:00' and  BCQC.QC_DATE<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ");
            }

            #endregion

            #region Ʒ��
            if (txtPrdNoB.Text != "")
                _sqlWhere.Append(" AND A.PRD_NO>='" + txtPrdNoB.Text + "'");
            if (txtPrdNoE.Text != "")
                _sqlWhere.Append(" AND A.PRD_NO<='" + txtPrdNoE.Text + "'");
            #endregion

            #region ����
            if (txtBatNoB.Text != "")
                _sqlWhere.Append(" AND REPLACE(substring(A.BAR_NO,13,10),'#','')>='" + txtBatNoB.Text + "'");
            if (txtBatNoE.Text != "")
                _sqlWhere.Append(" AND REPLACE(substring(A.BAR_NO,13,10),'#','')<='" + txtBatNoE.Text + "'");
            #endregion

            #region �������
                if (txtIdxB.Text != "")
                    _sqlWhere.Append(" AND BCP.IDX1>='" + txtIdxB.Text + "'");
                if (txtIdxE.Text != "")
                    _sqlWhere.Append(" AND BCP.IDX1<='" + txtIdxE.Text + "'");
            #endregion

            #region Ʒ�ʴ���
                if (txtSpcNoB.Text != "")
                    _sqlWhere.Append(" AND BCQC.SPC_NO>='" + txtSpcNoB.Text + "'");
                if (txtSpcNoE.Text != "")
                    _sqlWhere.Append(" AND BCQC.SPC_NO<='" + txtSpcNoE.Text + "'");
            #endregion

            #region ��Ʒ����
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

                _dict["BC_BAR_NO"] = "������к�";
                _dict["BC_PRD_NO"] = "���Ʒ��";
                _dict["BC_WH_NAME"] = "��Ŀ�λ";
                _dict["BC_IDX_NAME"] = "�������";
                _dict["BC_SPC"] = "��Ĺ��";
                _dict["BC_QTY"] = "�������";
                _dict["BC_BAT_NO"] = "�������";
                _dict["BC_SN"] = "�����ˮ��";
                _dict["BC_FORMAT"] = "��Ĵ�ӡ��ʽ";
                _dict["BC_LH"] = "����Ϻ�";

                _dict["BC_PRT_NAME"] = "��Ĵ�ӡƷ��";
                _dict["BC_QC"] = "���Ʒ����Ϣ";
                _dict["BC_PRC_ID"] = "��Ĵ���ʽ";
                _dict["BC_SPC_NAME"] = "����쳣ԭ��";
                _dict["BC_QC_REM"] = "���Ʒ�ʱ�ע";
                _dict["BC_BIL_TYPE"] = "������";
                _dict["BC_MO_NO"] = "�ͽ����";
                _dict["BC_MO_CUS_NAME"] = "�ͽɿͻ�";
                _dict["BC_SJ_WH_NAME"] = "�ͽɿ�λ";
                _dict["BC_MM_NO"] = "��Ľɿⵥ";

                _dict["BC_MM_WH_NAME"] = "�ɿ��λ";
                _dict["BC_SA_NO"] = "���������";
                _dict["BC_SA_CUS_NAME"] = "��������ͻ�";
                _dict["BC_SB_NO"] = "����˻�����";
                _dict["BC_SB_REM"] = "����˻���ע";
                _dict["DF_ML_NO"] = "���Ʒ���ϵ�";


                _dict["DF_BAR_NO"] = "���Ʒ���к�";
                _dict["DF_PRD_NO"] = "���ƷƷ��";
                _dict["DF_WH_NAME"] = "���Ʒ��λ";
                _dict["DF_IDX_NAME"] = "���Ʒ����";
                _dict["DF_SPC"] = "���Ʒ���";
                _dict["DF_QTY"] = "���Ʒ����";
                _dict["DF_BAT_NO"] = "���Ʒ����";
                _dict["DF_SN"] = "���Ʒ��ˮ��";
                _dict["DF_QC"] = "���ƷƷ����Ϣ";
                _dict["DF_PRC_ID"] = "���Ʒ����ʽ";

                _dict["DF_SPC_NAME"] = "���Ʒ�쳣ԭ��";
                _dict["DF_QC_REM"] = "���ƷƷ�ʱ�ע";
                _dict["DF_BIL_TYPE"] = "������";
                _dict["DF_MO_NO"] = "�ͽ����";
                _dict["DF_MO_CUS_NAME"] = "�ͽɿͻ�";
                _dict["DF_SJ_WH_NAME"] = "�ͽɿ�λ";
                _dict["DF_MM_NO"] = "���Ʒ�ɿⵥ";
                _dict["DF_MM_WH_NAME"] = "�ɿ��λ";
                _dict["DF_SA_NO"] = "���Ʒ������";
                _dict["DF_SA_CUS_NAME"] = "���Ʒ�����ͻ�";

                _dict["DF_SB_NO"] = "���Ʒ�˻�����";
                _dict["DF_SB_REM"] = "���Ʒ�˻���ע";
                _dict["FT_ML_NO"] = "���Ʒ���ϵ�";


                _dict["FT_BAR_NO"] = "������к�";
                _dict["FT_PRD_NO"] = "���Ʒ��";
                _dict["FT_WH_NAME"] = "��Ŀ�λ";
                _dict["FT_IDX_NAME"] = "�������";
                _dict["FT_SPC"] = "��Ĺ��";
                _dict["FT_QTY"] = "�������";
                _dict["FT_BAT_NO"] = "�������";
                _dict["FT_SN"] = "�����ˮ��";
                _dict["FT_QC"] = "���Ʒ����Ϣ";
                _dict["FT_PRC_ID"] = "��Ĵ���ʽ";

                _dict["FT_SPC_NAME"] = "����쳣ԭ��";
                _dict["FT_QC_REM"] = "���Ʒ�ʱ�ע";
                _dict["BOX_NO"] = "��������";
                _dict["BOX_PRD_NO"] = "����Ʒ��";
                _dict["BOX_IDX_NAME"] = "��������";
                _dict["BOX_SPC"] = "������";
                _dict["BOX_QTY"] = "��������";
                _dict["BOX_PRT_NAME"] = "�����ӡƷ��";
                _dict["BOX_FORMAT"] = "�����ӡ��ʽ";
                _dict["FT_BIL_TYPE"] = "������";

                _dict["FT_MO_NO"] = "�ͽ����";
                _dict["FT_MO_CUS_NAME"] = "�ͽɿͻ�";
                _dict["FT_SJ_WH_NAME"] = "�ͽɿ�λ";
                _dict["FT_MM_NO"] = "��Ľɿⵥ";
                _dict["FT_MM_WH_NAME"] = "�ɿ��λ";
                _dict["FT_SA_NO"] = "���������";
                _dict["FT_SA_CUS_NAME"] = "��������ͻ�";
                _dict["FT_SB_NO"] = "����˻�����";
                _dict["FT_SB_REM"] = "����˻���ע";
                _fx._columnLst = _dict;
                _fx.ShowDialog();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BC_BAR_NO"].HeaderText = "������к�";
            dataGridView1.Columns["BC_PRD_NO"].HeaderText = "���Ʒ��";
            dataGridView1.Columns["BC_WH_NAME"].HeaderText = "��Ŀ�λ";
            dataGridView1.Columns["BC_IDX_NAME"].HeaderText = "�������";
            dataGridView1.Columns["BC_SPC"].HeaderText = "��Ĺ��";
            dataGridView1.Columns["BC_QTY"].HeaderText = "�������";
            dataGridView1.Columns["BC_BAT_NO"].HeaderText = "�������";
            dataGridView1.Columns["BC_SN"].HeaderText = "�����ˮ��";
            dataGridView1.Columns["BC_FORMAT"].HeaderText = "��Ĵ�ӡ��ʽ";
            dataGridView1.Columns["BC_LH"].HeaderText = "����Ϻ�";

            dataGridView1.Columns["BC_PRT_NAME"].HeaderText = "��Ĵ�ӡƷ��";
            dataGridView1.Columns["BC_QC"].HeaderText = "���Ʒ����Ϣ";
            dataGridView1.Columns["BC_PRC_ID"].HeaderText = "��Ĵ���ʽ";
            dataGridView1.Columns["BC_SPC_NAME"].HeaderText = "����쳣ԭ��";
            dataGridView1.Columns["BC_QC_REM"].HeaderText = "���Ʒ�ʱ�ע";
            dataGridView1.Columns["BC_BIL_TYPE"].HeaderText = "������";
            dataGridView1.Columns["BC_MO_NO"].HeaderText = "�ͽ����";
            dataGridView1.Columns["BC_MO_CUS_NAME"].HeaderText = "�ͽɿͻ�";
            dataGridView1.Columns["BC_SJ_WH_NAME"].HeaderText = "�ͽɿ�λ";
            dataGridView1.Columns["BC_MM_NO"].HeaderText = "��Ľɿⵥ";

            dataGridView1.Columns["BC_MM_WH_NAME"].HeaderText = "�ɿ��λ";
            dataGridView1.Columns["BC_SA_NO"].HeaderText = "���������";
            dataGridView1.Columns["BC_SA_CUS_NAME"].HeaderText = "��������ͻ�";
            dataGridView1.Columns["BC_SB_NO"].HeaderText = "����˻�����";
            dataGridView1.Columns["BC_SB_REM"].HeaderText = "����˻���ע";
            dataGridView1.Columns["DF_ML_NO"].HeaderText = "���Ʒ���ϵ�";


            dataGridView1.Columns["DF_BAR_NO"].HeaderText = "���Ʒ���к�";
            dataGridView1.Columns["DF_PRD_NO"].HeaderText = "���ƷƷ��";
            dataGridView1.Columns["DF_WH_NAME"].HeaderText = "���Ʒ��λ";
            dataGridView1.Columns["DF_IDX_NAME"].HeaderText = "���Ʒ����";
            dataGridView1.Columns["DF_SPC"].HeaderText = "���Ʒ���";
            dataGridView1.Columns["DF_QTY"].HeaderText = "���Ʒ����";
            dataGridView1.Columns["DF_BAT_NO"].HeaderText = "���Ʒ����";
            dataGridView1.Columns["DF_SN"].HeaderText = "���Ʒ��ˮ��";
            dataGridView1.Columns["DF_QC"].HeaderText = "���ƷƷ����Ϣ";
            dataGridView1.Columns["DF_PRC_ID"].HeaderText = "���Ʒ����ʽ";

            dataGridView1.Columns["DF_SPC_NAME"].HeaderText = "���Ʒ�쳣ԭ��";
            dataGridView1.Columns["DF_QC_REM"].HeaderText = "���ƷƷ�ʱ�ע";
            dataGridView1.Columns["DF_BIL_TYPE"].HeaderText = "������";
            dataGridView1.Columns["DF_MO_NO"].HeaderText = "�ͽ����";
            dataGridView1.Columns["DF_MO_CUS_NAME"].HeaderText = "�ͽɿͻ�";
            dataGridView1.Columns["DF_SJ_WH_NAME"].HeaderText = "�ͽɿ�λ";
            dataGridView1.Columns["DF_MM_NO"].HeaderText = "���Ʒ�ɿⵥ";
            dataGridView1.Columns["DF_MM_WH_NAME"].HeaderText = "�ɿ��λ";
            dataGridView1.Columns["DF_SA_NO"].HeaderText = "���Ʒ������";
            dataGridView1.Columns["DF_SA_CUS_NAME"].HeaderText = "���Ʒ�����ͻ�";

            dataGridView1.Columns["DF_SB_NO"].HeaderText = "���Ʒ�˻�����";
            dataGridView1.Columns["DF_SB_REM"].HeaderText = "���Ʒ�˻���ע";
            dataGridView1.Columns["FT_ML_NO"].HeaderText = "���Ʒ���ϵ�";


            dataGridView1.Columns["FT_BAR_NO"].HeaderText = "������к�";
            dataGridView1.Columns["FT_PRD_NO"].HeaderText = "���Ʒ��";
            dataGridView1.Columns["FT_WH_NAME"].HeaderText = "��Ŀ�λ";
            dataGridView1.Columns["FT_IDX_NAME"].HeaderText = "�������";
            dataGridView1.Columns["FT_SPC"].HeaderText = "��Ĺ��"; ;
            dataGridView1.Columns["FT_QTY"].HeaderText = "�������";
            dataGridView1.Columns["FT_BAT_NO"].HeaderText = "�������";
            dataGridView1.Columns["FT_SN"].HeaderText = "�����ˮ��";
            dataGridView1.Columns["FT_QC"].HeaderText = "���Ʒ����Ϣ";
            dataGridView1.Columns["FT_PRC_ID"].HeaderText = "��Ĵ���ʽ";

            dataGridView1.Columns["FT_SPC_NAME"].HeaderText = "����쳣ԭ��";
            dataGridView1.Columns["FT_QC_REM"].HeaderText = "���Ʒ�ʱ�ע";
            dataGridView1.Columns["BOX_NO"].HeaderText = "��������";
            dataGridView1.Columns["BOX_PRD_NO"].HeaderText = "����Ʒ��";
            dataGridView1.Columns["BOX_IDX_NAME"].HeaderText = "��������";
            dataGridView1.Columns["BOX_SPC"].HeaderText = "������";
            dataGridView1.Columns["BOX_QTY"].HeaderText = "��������";
            dataGridView1.Columns["BOX_PRT_NAME"].HeaderText = "�����ӡƷ��";
            dataGridView1.Columns["BOX_FORMAT"].HeaderText = "�����ӡ��ʽ";
            dataGridView1.Columns["FT_BIL_TYPE"].HeaderText = "������";

            dataGridView1.Columns["FT_MO_NO"].HeaderText = "�ͽ����";
            dataGridView1.Columns["FT_MO_CUS_NAME"].HeaderText = "�ͽɿͻ�";
            dataGridView1.Columns["FT_SJ_WH_NAME"].HeaderText = "�ͽɿ�λ";
            dataGridView1.Columns["FT_MM_NO"].HeaderText = "��Ľɿⵥ";
            dataGridView1.Columns["FT_MM_WH_NAME"].HeaderText = "�ɿ��λ";
            dataGridView1.Columns["FT_SA_NO"].HeaderText = "���������";
            dataGridView1.Columns["FT_SA_CUS_NAME"].HeaderText = "��������ͻ�";
            dataGridView1.Columns["FT_SB_NO"].HeaderText = "����˻�����";
            dataGridView1.Columns["FT_SB_REM"].HeaderText = "����˻���ע";
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

        #region ��ͷ

        private void qryPrdNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//����
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNoB.Text = _qw.NO_RT;
            }
        }

        private void qryPrdNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PRDT";//����
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtPrdNoE.Text = _qw.NO_RT;
            }
        }

        private void qryBatNoB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//����
            _qw.SQLWhere += " AND LEN(BAT_NO)=7 ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNoB.Text = _qw.NO_RT;
            }
        }

        private void qryBatNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "BAT_NO";//����
            _qw.SQLWhere += " AND LEN(BAT_NO)=7 ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtBatNoE.Text = _qw.NO_RT;
            }
        }

        private void qryIdxB_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "INDX";//����
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtIdxB.Text = _qw.NO_RT;
            }
        }

        private void qryIdxE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "INDX";//����
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
            _qw.PGM = "SPC_LST";//����
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtSpcNoB.Text = _qw.NO_RT;
            }
        }

        private void qrySpcNoE_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "SPC_LST";//����
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