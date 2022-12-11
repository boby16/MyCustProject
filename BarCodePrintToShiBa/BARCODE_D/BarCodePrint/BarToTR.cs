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
    public partial class BarToTR : Form
    {
        private DataSet _initDS = new DataSet();
        public DataSet InitDS
        {
            get { return this._initDS; }
            set { this._initDS = value; }
        }
        private DataSet _dsTY = new DataSet();

        private DataSet _dsMo = new DataSet();
        private string _whNameTem = "";

        public BarToTR()
        {
            InitializeComponent();
        }

        #region ת�쳣֪ͨ��
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMONo.Text.Trim()) && !cbMoNull.Checked)
            {
                MessageBox.Show("��ѡ�������");
            }
            else if (txtWH.Text == "")
                MessageBox.Show("��ѡ���λ��");
            else if (InitDS == null || !InitDS.Tables.Contains("BAR_REJECT") || InitDS.Tables["BAR_REJECT"].Rows.Count <= 0)
            {
                return;
            }
            else
            {
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                DataSet _dsWh = _bps.GetWh(BarRole.USR_NO, txtWH.Text);
                if (_dsWh != null && _dsWh.Tables.Count > 0 && _dsWh.Tables[0].Rows.Count > 0)
                {
                    if (lstPrcId.SelectedIndex == 0 && _dsWh.Tables[0].Rows[0]["INVALID"].ToString() != "T")
                    {
                        //����ֻ��ת���Ʒ��
                        txtWH.Text = "";
                        txtWhName.Text = "";
                        MessageBox.Show("��λ�����ڣ�");
                    }
                    else
                        toTR();//ת���쳣��

                }
                else
                    MessageBox.Show("��λ�����ڣ�");
            }
        }

        #region ת���쳣��

        private void toTR()
        {
            DataSet _ds = new DataSet();
            DataTable _dt = new DataTable("TF_TY_EXPORT");

            _dt.Columns.Add("QTY_LOST", System.Type.GetType("System.Decimal"));
            _dt.Columns.Add("PRD_NO", System.Type.GetType("System.String"));
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
            _dt.Columns.Add("BAR_NO", System.Type.GetType("System.String"));
            _dt.Columns.Add("REM", System.Type.GetType("System.String"));
            _dt.Columns.Add("REM_LST", System.Type.GetType("System.String"));
            _dt.Columns.Add("SPC_NO_LST", System.Type.GetType("System.String"));

            _ds.Tables.Add(_dt);

            DataRow _drNew;
            DataRow _drMo = null;
            if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                _drMo = _dsMo.Tables[0].Rows[0];
            if (_drMo != null)
            {
                foreach (DataRow dr in InitDS.Tables["BAR_REJECT"].Rows)
                {
                    if (txtPrdNo.Text != dr["PRD_NO"].ToString())
                    {
                        MessageBox.Show("��Ʒ[" + dr["PRD_NO"].ToString() + "]�������Ʒ[" + txtPrdNo.Text + "]���������飡");
                        return;
                    }
                    //if (dr["PRD_NO"].ToString() == _drMo["PRD_NO"].ToString() && dr["PRD_MARK"].ToString() == _drMo["PRD_MARK"].ToString())
                    //{
                    DataRow[] _drChk = _ds.Tables[0].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "'");// AND BAT_NO='" + dr["BAT_NO"].ToString() + "'");
                    if (_drChk.Length > 0)
                    {
                        _drChk[0]["QTY_LOST"] = Convert.ToInt32(_drChk[0]["QTY_LOST"]) + 1;
                        _drChk[0]["BAR_NO"] += ";" + dr["BAR_NO"].ToString();
                        _drChk[0]["REM"] += "\n" + dr["BAR_NO"].ToString() + "�� " + BarRole.ConvertToDBLanguage(dr["REM"].ToString());
                        _drChk[0]["SPC_NO_LST"] += ";" + dr["SPC_NO"];
                        _drChk[0]["REM_LST"] += ";" + BarRole.ConvertToDBLanguage(dr["REM"].ToString());
                    }
                    else
                    {
                        _drNew = _dt.NewRow();
                        _drNew["QTY_LOST"] = 1;
                        _drNew["PRD_NO"] = dr["PRD_NO"];
                        _drNew["PRD_MARK"] = dr["PRD_MARK"];
                        _drNew["UNIT"] = "1";
                        //_drNew["BAT_NO"] = dr["BAT_NO"];
                        _drNew["USR"] = BarRole.USR_NO;
                        _drNew["USR_NO"] = BarRole.USR_NO;
                        _drNew["DEP"] = BarRole.DEP;
                        _drNew["SPC_NO"] = dr["SPC_NO"];
                        _drNew["PRC_ID"] = dr["PRC_ID"];

                        _drNew["MO_NO"] = _drMo["MO_NO"];
                        _drNew["ID_NO"] = _drMo["ID_NO"];
                        _drNew["WH"] = txtWH.Text;
                        _drNew["BAR_NO"] = dr["BAR_NO"];
                        _drNew["REM"] = dr["BAR_NO"].ToString() + "�� " + BarRole.ConvertToDBLanguage(dr["REM"].ToString());
                        _drNew["SPC_NO_LST"] = dr["SPC_NO"];
                        _drNew["REM_LST"] = BarRole.ConvertToDBLanguage(dr["REM"].ToString());
                        _dt.Rows.Add(_drNew);
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("ת�쳣֪ͨ���Ļ�Ʒ�������������" + _drMo["MO_NO"].ToString() + "���еĻ�Ʒ����Ϊ��" + _drMo["PRD_NO"].ToString() + "�ݻ�Ʒ");
                    //    _ds.Clear();
                    //    return;
                    //}
                }
            }
            else
            {
                if (!cbMoNull.Checked)
                {
                    //���������Ϊ��
                    MessageBox.Show("�����������������ѡ�������");
                    return;
                }
                else
                {
                    foreach (DataRow dr in InitDS.Tables["BAR_REJECT"].Rows)
                    {
                        //DataRow[] _drChk = _ds.Tables[0].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "' AND BAT_NO='" + dr["BAT_NO"].ToString() + "'");
                        DataRow[] _drChk = _ds.Tables[0].Select("PRD_NO='" + dr["PRD_NO"].ToString() + "'");
                        if (_drChk.Length > 0)
                        {
                            _drChk[0]["QTY_LOST"] = Convert.ToInt32(_drChk[0]["QTY_LOST"]) + 1;
                            _drChk[0]["BAR_NO"] += ";" + dr["BAR_NO"].ToString();
                            _drChk[0]["REM"] += "\n " + dr["BAR_NO"].ToString() + "�� " + BarRole.ConvertToDBLanguage(dr["REM"].ToString());
                            _drChk[0]["SPC_NO_LST"] += ";" + dr["SPC_NO"];
                            _drChk[0]["REM_LST"] += ";" + BarRole.ConvertToDBLanguage(dr["REM"].ToString());
                        }
                        else
                        {
                            _drNew = _dt.NewRow();
                            _drNew["QTY_LOST"] = 1;
                            _drNew["PRD_NO"] = dr["PRD_NO"];
                            _drNew["PRD_MARK"] = dr["PRD_MARK"];
                            _drNew["UNIT"] = "1";
                            //_drNew["BAT_NO"] = dr["BAT_NO"];
                            _drNew["USR"] = BarRole.USR_NO;
                            _drNew["USR_NO"] = BarRole.USR_NO;
                            _drNew["DEP"] = BarRole.DEP;
                            _drNew["SPC_NO"] = dr["SPC_NO"];
                            _drNew["PRC_ID"] = dr["PRC_ID"];

                            _drNew["MO_NO"] = DBNull.Value;
                            _drNew["ID_NO"] = DBNull.Value;
                            _drNew["WH"] = txtWH.Text;
                            _drNew["BAR_NO"] = dr["BAR_NO"];
                            _drNew["REM"] = dr["BAR_NO"].ToString() + "�� " + BarRole.ConvertToDBLanguage(dr["REM"].ToString());
                            _drNew["SPC_NO_LST"] = dr["SPC_NO"];
                            _drNew["REM_LST"] = BarRole.ConvertToDBLanguage(dr["REM"].ToString());

                            _dt.Rows.Add(_drNew);
                        }
                    }
                }
            }

            try
            {
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                _bps.UpdateFaultData(_ds);
                MessageBox.Show("ת���ɹ���");
                ClearData();
            }
            catch (Exception _ex)
            {
                string _err = _ex.Message;
                int _idx = _err.IndexOf("SunlikeException");
                if (_idx > 0)
                    _err = _err.Substring(_idx + 17);
                if (_err.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                MessageBox.Show("ת������ԭ��\n" + _err);
                _ds.Dispose();
            }
        }

        #endregion

        #endregion

        #region �����
        private void btnMONo_Click(object sender, EventArgs e)
        {
            QueryMo _qm = new QueryMo();
            _qm.SQLWhere = " AND (ISNULL(A.CHK_MAN,'')<>'' AND A.CLS_DATE IS NOT NULL) ";
            if (_qm.ShowDialog() == DialogResult.OK)
            {
                _dsMo = _qm.MoDS;
                if (_dsMo != null && _dsMo.Tables.Count > 0 && _dsMo.Tables[0].Rows.Count > 0)
                {
                    txtMONo.Text = _dsMo.Tables[0].Rows[0]["MO_NO"].ToString();
                    txtPrdNo.Text = _dsMo.Tables[0].Rows[0]["PRD_NO"].ToString();
                    txtSpc.Text = _dsMo.Tables[0].Rows[0]["SPC"].ToString();
                    txtPMark.Text = _dsMo.Tables[0].Rows[0]["PRD_MARK"].ToString();
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
                    txtPMark.Text = "";
                    txtIdx.Text = "";
                    txtUT.Text = "";
                    txtYIQty.Text = "";
                    txtQtyFin.Text = "";
                    txtQtyLost.Text = "";
                    txtWH.Text = "";
                    txtWhName.Text = "";
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
                    txtPrdNo.Text = _ds.Tables[0].Rows[0]["PRD_NO"].ToString();
                    txtSpc.Text = _ds.Tables[0].Rows[0]["SPC"].ToString();
                    txtPMark.Text = _ds.Tables[0].Rows[0]["PRD_MARK"].ToString();
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
                    txtPMark.Text = "";
                    txtIdx.Text = "";
                    txtUT.Text = "";
                    txtYIQty.Text = "";
                    txtQtyFin.Text = "";
                    txtQtyLost.Text = "";
                    txtWH.Text = "";
                    txtWhName.Text = "";
                }
            }
            else
            {
                txtPrdNo.Text = "";
                txtSpc.Text = "";
                txtPMark.Text = "";
                txtIdx.Text = "";
                txtUT.Text = "";
                txtYIQty.Text = "";
                txtQtyFin.Text = "";
                txtQtyLost.Text = "";
                txtWH.Text = "";
                txtWhName.Text = "";
            }
        }
        #endregion

        #region Page_Load
        private void BarToTR_Load(object sender, EventArgs e)
        {
            cbPrdType.SelectedIndex = 0;
            lstPrcId.SelectedIndex = 0;
            cbMoNull.Visible = BarRole.CHK_MONULL;
        }
        #endregion

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["BAR_NO"].HeaderText = "���к�";
            dataGridView1.Columns["QC"].Visible = false;
            dataGridView1.Columns["REM"].HeaderText = "��ע";

            dataGridView1.Columns["PRD_NO"].HeaderText = "Ʒ��";
            dataGridView1.Columns["PRD_NAME"].Visible = false;
            dataGridView1.Columns["PRD_MARK"].Visible = false;
            dataGridView1.Columns["IDX_NAME"].HeaderText = "����";
            dataGridView1.Columns["SPC"].HeaderText = "���";
            dataGridView1.Columns["SPC_NO"].Visible = false;
            dataGridView1.Columns["STATUS"].Visible = false;
            dataGridView1.Columns["BIL_ID"].Visible = false;
            dataGridView1.Columns["BIL_NO"].Visible = false;
            dataGridView1.Columns["SPC_NAME"].HeaderText = "�쳣ԭ��";
            dataGridView1.Columns["BAT_NO"].HeaderText = "����";
            dataGridView1.Columns["PRC_ID"].Visible = false;
            dataGridView1.Columns["PRC_NAME"].HeaderText = "����ʽ";
            dataGridView1.Columns["WH"].Visible = false;
            dataGridView1.Columns["BOX_NO"].Visible = false;

            dataGridView1.Columns["BAR_NO"].DisplayIndex = 0;
            dataGridView1.Columns["PRD_NO"].DisplayIndex = 1;
            dataGridView1.Columns["IDX_NAME"].DisplayIndex = 2;
            dataGridView1.Columns["SPC"].DisplayIndex = 3;
            dataGridView1.Columns["BAT_NO"].DisplayIndex = 4;
            dataGridView1.Columns["SPC_NAME"].DisplayIndex = 5;
            dataGridView1.Columns["REM"].DisplayIndex = 6;
        }

        #region ��λ
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
                        if (lstPrcId.SelectedIndex == 0 && _dsWh.Tables[0].Rows[0]["INVALID"].ToString() != "T")
                        {
                            //����ֻ��ת���Ʒ��
                            txtWH.Text = "";
                            txtWhName.Text = "";
                            MessageBox.Show("���Ų����ڣ�");
                        }
                        else
                        {
                            txtWH.Text = _dsWh.Tables[0].Rows[0]["WH"].ToString();
                            txtWhName.Text = _dsWh.Tables[0].Rows[0]["NAME"].ToString();
                        }

                    }
                    else
                    {
                        txtWH.Text = "";
                        txtWhName.Text = "";
                        MessageBox.Show("���Ų����ڣ�");
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
            _qw.PGM = "MY_WH";//����
            if (lstPrcId.SelectedIndex == 0)
                _qw.SQLWhere = " AND ISNULL(INVALID,'F')='T' ";//��Ʒ��
            else
                _qw.SQLWhere = " AND ISNULL(INVALID,'F')<>'T' ";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtWH.Text = _qw.NO_RT;
                txtWhName.Text = _qw.NAME_RTN;
            }
        }
        #endregion

        #region ˢ��
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //PRC_ID:  2:���ϡ�4:�ؿ�����
            //lstPrcId.SelectedIndex��0:���ϣ�1:�ؿ�����
            string _prcId = "2";
            if (lstPrcId.SelectedIndex == 1)
                _prcId = "4";

            //ȡ�ò��ϸ����ؿ�����򱨷ϵ�����
            onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
            _bps.UseDefaultCredentials = true;
            _dsTY = new DataSet();
            if (cbPrdType.SelectedIndex == 1)
                _dsTY = _bps.GetDataToTY(" AND LEN(REPLACE(A.BAR_NO,'#',''))=24 AND (ISNULL(A.QC,'')='2' AND ISNULL(A.PRC_ID,'')='" + _prcId + "') AND ISNULL(A.STATUS, '')='QC' ORDER BY B.BAT_NO,A.BAR_NO ");
            else if (cbPrdType.SelectedIndex == 2)
                _dsTY = _bps.GetDataToTY(" AND LEN(REPLACE(A.BAR_NO,'#',''))>24 AND (ISNULL(A.QC,'')='2' AND ISNULL(A.PRC_ID,'')='" + _prcId + "') AND ISNULL(A.STATUS, '')='QC' AND ISNULL(B.BOX_NO,'')='' ORDER BY B.BAT_NO,A.BAR_NO ");
            else
                _dsTY = _bps.GetDataToTY(" AND LEN(REPLACE(A.BAR_NO,'#',''))=22 AND (ISNULL(A.QC,'')='2' AND ISNULL(A.PRC_ID,'')='" + _prcId + "') AND ISNULL(A.STATUS, '')='QC' ORDER BY B.BAT_NO,A.BAR_NO ");


            if (_dsTY != null && _dsTY.Tables.Count > 0)
            {
                _dsTY.Tables[0].Columns["SPC_NAME"].ReadOnly = false;
                _dsTY.Tables[0].Columns["PRC_NAME"].ReadOnly = false;
                _dsTY.Tables[0].Columns["REM"].ReadOnly = false;

                InitDS = _dsTY.Clone();
                InitDS.Tables[0].TableName = "BAR_REJECT";

                foreach (DataRow dr in _dsTY.Tables[0].Rows)
                {
                    dr["SPC_NAME"] = Strings.StrConv(dr["SPC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["PRC_NAME"] = Strings.StrConv(dr["PRC_NAME"].ToString(), VbStrConv.SimplifiedChinese, 0);
                    dr["REM"] = Strings.StrConv(dr["REM"].ToString(), VbStrConv.SimplifiedChinese, 0);
                }

                lstReject.DataSource = _dsTY.Tables["BAR_QC"];
                lstReject.DisplayMember = "BAR_NO";
                lstReject.ValueMember = "BAR_NO";
            }
            ClearData();
        }
        #endregion

        private void btnReject_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstReject.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstReject.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArrR = InitDS.Tables["BAR_REJECT"].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArr = _dsTY.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                if (InitDS.Tables["BAR_REJECT"].Rows.Count > 0)
                {
                    if (_drArr[0]["PRC_ID"].ToString() != InitDS.Tables["BAR_REJECT"].Rows[0]["PRC_ID"].ToString())
                    {
                        MessageBox.Show("���кŴ���ʽ��һ��������ͬʱת����");
                        return;
                    }
                }
                foreach (DataRow _dr in _drArrR)
                {
                    _dr.Delete();
                }
                foreach (DataRow _dr in _drArr)
                {
                    InitDS.Tables["BAR_REJECT"].ImportRow(_dr);

                    _dr.Delete();
                }
                InitDS.AcceptChanges();
                _dsTY.AcceptChanges();

                lstReject.DataSource = _dsTY.Tables["BAR_QC"];
                lstReject.DisplayMember = "BAR_NO";
                lstReject.ValueMember = "BAR_NO";

                dataGridView1.DataMember = "BAR_REJECT";
                dataGridView1.DataSource = InitDS;
                dataGridView1.Refresh();
            }
            txtQty.Text = dataGridView1.Rows.Count.ToString();
        }

        private void btnUnReject_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                _drv = (DataRowView)dataGridView1.SelectedRows[i].DataBoundItem;
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["BAR_NO"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArrR = InitDS.Tables["BAR_REJECT"].Select("BAR_NO in (" + _selectNo + ")");
                DataRow[] _drArr = _dsTY.Tables["BAR_QC"].Select("BAR_NO in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                foreach (DataRow _dr in _drArrR)
                {
                    _dsTY.Tables["BAR_QC"].ImportRow(_dr);
                    _dr.Delete();
                }
                InitDS.AcceptChanges();
                _dsTY.AcceptChanges();

                lstReject.DataSource = _dsTY.Tables["BAR_QC"];
                lstReject.DisplayMember = "BAR_NO";
                lstReject.ValueMember = "BAR_NO";

                dataGridView1.DataMember = "BAR_REJECT";
                dataGridView1.DataSource = InitDS;
                dataGridView1.Refresh();
            }
            txtQty.Text = dataGridView1.Rows.Count.ToString();
        }

        private void lstPrcId_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRefresh_Click(null, null);
        }

        #region ���ҳ������
        /// <summary>
        /// ���ҳ������
        /// </summary>
        private void ClearData()
        {
            InitDS.Clear();
            dataGridView1.DataMember = "BAR_REJECT";
            dataGridView1.DataSource = InitDS;
            dataGridView1.Refresh();

            txtMONo.Text = "";
            txtPrdNo.Text = "";
            txtSpc.Text = "";
            txtIdx.Text = "";
            txtYIQty.Text = "";
            txtQtyFin.Text = "";
            txtQtyLost.Text = "";
            txtQty.Text = "";
            txtUT.Text = "";
            txtWH.Text = "";
            txtWhName.Text = "";
            _whNameTem = "";
            txtPMark.Text = "";
            cbMoNull.Checked = false;
        }
        #endregion
    }
}