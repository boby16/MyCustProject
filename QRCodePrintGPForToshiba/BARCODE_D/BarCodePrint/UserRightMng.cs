using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class UserRightMng : Form
    {
        private DataSet _dsYes = new DataSet();
        private DataSet _dsNo = new DataSet();
        public UserRightMng()
        {
            InitializeComponent();
        }

        private void txtNo_Leave(object sender, EventArgs e)
        {
            InitData();
            _dsYes.Clear();
            _dsYes.AcceptChanges();
            if (txtNo.Text != "")
            {
                onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                _bps.UseDefaultCredentials = true;
                if (_bps.UserIsExist(txtNo.Text))
                {
                    DataSet _ds = _bps.GetUserRightAll(txtNo.Text);
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow _dr in _ds.Tables[0].Rows)
                        {
                            DataRow _drNo = _dsNo.Tables[0].Rows.Find(_dr["PGM"].ToString());
                            if (_drNo != null)
                            {
                                _dsYes.Tables[0].ImportRow(_drNo);
                                _drNo.Delete();
                            }
                        }
                        _dsNo.AcceptChanges();
                        _dsYes.AcceptChanges();

                        lstNoRight.DataSource = _dsNo.Tables[0];
                        lstNoRight.DisplayMember = "VALUE";
                        lstNoRight.ValueMember = "PGM";

                        lstYesRight.DataSource = _dsYes.Tables[0];
                        lstYesRight.DisplayMember = "VALUE";
                        lstYesRight.ValueMember = "PGM";
                    }
                }
                else
                {
                    MessageBox.Show("用户不存在！");
                    txtNo.Text = "";
                    txtName.Text = "";
                }
            }
            lstYesRight.DataSource = _dsYes.Tables[0];
            lstYesRight.DisplayMember = "VALUE";
            lstYesRight.ValueMember = "PGM";
        }

        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.txtNo_Leave(sender, e);
            }
        }

        private void UserRightMng_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            if (_dsYes != null && _dsYes.Tables.Count > 0)
            { }
            else
            {
                DataTable _dt = new DataTable("USER_RIGHT");
                DataColumn _pkColumn = new DataColumn("PGM", System.Type.GetType("System.String"));
                _dt.Columns.Add(_pkColumn);
                _dt.Columns.Add("VALUE", System.Type.GetType("System.String"));
                _dt.Columns.Add("ITM", System.Type.GetType("System.Int32"));
                _dt.PrimaryKey = new DataColumn[1] { _pkColumn };
                _dsYes.Tables.Add(_dt);
            }
            int _itm = 1;

            _dsNo = _dsYes.Clone();

            //--------------
            DataRow _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BCPRT";
            _dr["VALUE"] = _itm.ToString() + "、板材条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "DFPRT";
            _dr["VALUE"] = _itm.ToString() + "、半成品条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "DFSPCPRT";
            _dr["VALUE"] = _itm.ToString() + "、半成品条码打印(特殊)";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTPRT";
            _dr["VALUE"] = _itm.ToString() + "、成品条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTSPCPRT";
            _dr["VALUE"] = _itm.ToString() + "、成品条码打印(特殊)";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "WXPRT";
            _dr["VALUE"] = _itm.ToString() + "、外箱条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "WXEDIT";
            _dr["VALUE"] = _itm.ToString() + "、修改外箱条码";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //----------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BCCHGPRT";
            _dr["VALUE"] = _itm.ToString() + "、板材换货条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "DFCHGPRT";
            _dr["VALUE"] = _itm.ToString() + "、半成品换货条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTCHGPRT";
            _dr["VALUE"] = _itm.ToString() + "、成品换货条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //-----------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BARDEL";
            _dr["VALUE"] = _itm.ToString() + "、条码删除";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BCEDIT";
            _dr["VALUE"] = _itm.ToString() + "、板材条码修改";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "DFEDIT";
            _dr["VALUE"] = _itm.ToString() + "、半成品条码修改";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTEDIT";
            _dr["VALUE"] = _itm.ToString() + "、成品条码修改";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //----------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BCREP";
            _dr["VALUE"] = _itm.ToString() + "、板材/半成品条码重复打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTREP";
            _dr["VALUE"] = _itm.ToString() + "、成品条码重复打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "WXREP";
            _dr["VALUE"] = _itm.ToString() + "、外箱条码重复打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //----------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BCQC";
            _dr["VALUE"] = _itm.ToString() + "、板材品检";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "DFQC";
            _dr["VALUE"] = _itm.ToString() + "、半成品品检";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTQC";
            _dr["VALUE"] = _itm.ToString() + "、成品品质检验";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "TOTR";
            _dr["VALUE"] = _itm.ToString() + "、转异常通知单";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "UNDOTR";
            _dr["VALUE"] = _itm.ToString() + "、转异常撤销作业";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //----------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "XCBOX";
            _dr["VALUE"] = _itm.ToString() + "、现场装箱";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "XCUNBOX";
            _dr["VALUE"] = _itm.ToString() + "、现场拆箱";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "WHBOX";
            _dr["VALUE"] = _itm.ToString() + "、仓库装箱";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "WHUNBOX";
            _dr["VALUE"] = _itm.ToString() + "、仓库拆箱";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //---------------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BCTOMM";
            _dr["VALUE"] = _itm.ToString() + "、板材送缴";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BCMM";
            _dr["VALUE"] = _itm.ToString() + "、板材缴库";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "DFTOMM";
            _dr["VALUE"] = _itm.ToString() + "、半成品送缴";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "DFMM";
            _dr["VALUE"] = _itm.ToString() + "、半成品缴库";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTTOMM";
            _dr["VALUE"] = _itm.ToString() + "、成品送缴";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTMM";
            _dr["VALUE"] = _itm.ToString() + "、成品缴库";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "UNDOTOMM";
            _dr["VALUE"] = _itm.ToString() + "、送缴撤销作业";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "UNDOMM";
            _dr["VALUE"] = _itm.ToString() + "、缴库撤销作业";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //----------------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "SABARQC";
            _dr["VALUE"] = _itm.ToString() + "、销货单条码品检";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "SACHGBAR";
            _dr["VALUE"] = _itm.ToString() + "、销货单条码替换";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //-----------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BCSB";
            _dr["VALUE"] = _itm.ToString() + "、板材退货条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "FTSB";
            _dr["VALUE"] = _itm.ToString() + "、成品退货条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "SBUNDOBOX";
            _dr["VALUE"] = _itm.ToString() + "、退货拆箱";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "SBBARLIST";
            _dr["VALUE"] = _itm.ToString() + "、退货条码查询";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //------------------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "INITBCPRT";
            _dr["VALUE"] = _itm.ToString() + "、期初板材条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "INITDFPRT";
            _dr["VALUE"] = _itm.ToString() + "、期初半成品条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "INITBARPRT";
            _dr["VALUE"] = _itm.ToString() + "、期初成品条码打印";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "INITBARBOX";
            _dr["VALUE"] = _itm.ToString() + "、期初成品条码装箱";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            //-------------------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BARQCLIST";
            _dr["VALUE"] = _itm.ToString() + "、条码明细报表";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BARQCSTA";
            _dr["VALUE"] = _itm.ToString() + "、条码品质总报表";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "SABARQCLIST";
            _dr["VALUE"] = _itm.ToString() + "、出货检验统计表";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "RPT_PRDT_HIS";
            _dr["VALUE"] = _itm.ToString() + "、产品历史查询";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "RPT_PRDT_STAT";
            _dr["VALUE"] = _itm.ToString() + "、产品生产统计报表";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "RPT_BOXBAR";
            _dr["VALUE"] = _itm.ToString() + "、箱条码信息查询";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "SASTAT";
            _dr["VALUE"] = _itm.ToString() + "、销货单统计表";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            //-----------------------------
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "INITBCMM";
            _dr["VALUE"] = _itm.ToString() + "、期初板材缴库";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "INITAUTOFTBOX";
            _dr["VALUE"] = _itm.ToString() + "、期初成品装箱";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "BAR_IDX_SET";
            _dr["VALUE"] = _itm.ToString() + "、料号/打印品名设定";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "RIGHTMNG";
            _dr["VALUE"] = _itm.ToString() + "、权限设定";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);
            _dr = _dsNo.Tables[0].NewRow();
            _dr["PGM"] = "CHK_MMNULL";
            _dr["VALUE"] = _itm.ToString() + "、[制令单允许为空]选项可见";
            _dr["ITM"] = _itm++;
            _dsNo.Tables[0].Rows.Add(_dr);

            _dsNo.AcceptChanges();

            lstNoRight.DataSource = _dsNo.Tables[0];
            lstNoRight.DisplayMember = "VALUE";
            lstNoRight.ValueMember = "PGM";

            _dsYes.Tables[0].Clear();
            _dsYes.AcceptChanges();
            lstYesRight.DataSource = _dsYes.Tables[0];
            lstYesRight.DisplayMember = "VALUE";
            lstYesRight.ValueMember = "PGM";
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstNoRight.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstNoRight.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["PGM"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsNo.Tables[0].Select("PGM in (" + _selectNo + ")");
                DataRow[] _drArrR = _dsYes.Tables[0].Select("PGM in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArrR)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArr)
                {
                    _drNew = _dsYes.Tables[0].NewRow();
                    _drNew["PGM"] = _dr["PGM"];
                    _drNew["VALUE"] = _dr["VALUE"];
                    _dsYes.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsNo.AcceptChanges();

                DataView _dvNo = _dsNo.Tables[0].DefaultView;
                DataView _dvYes = _dsYes.Tables[0].DefaultView;
                _dvNo.Sort = "ITM ASC";
                _dvYes.Sort = "ITM ASC";

                lstNoRight.DataSource = _dvNo;
                lstNoRight.DisplayMember = "VALUE";
                lstNoRight.ValueMember = "PGM";

                lstYesRight.DataSource = _dvYes;
                lstYesRight.DisplayMember = "VALUE";
                lstYesRight.ValueMember = "PGM";
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DataRowView _drv;
            string _selectNo = "";
            for (int i = 0; i < lstYesRight.SelectedItems.Count; i++)
            {
                _drv = (DataRowView)lstYesRight.SelectedItems[i];
                if (_selectNo != "")
                    _selectNo += ",";
                _selectNo += "'" + _drv["PGM"] + "'";
            }
            if (_selectNo != "")
            {
                DataRow[] _drArr = _dsNo.Tables[0].Select("PGM in (" + _selectNo + ")");
                DataRow[] _drArrR = _dsYes.Tables[0].Select("PGM in (" + _selectNo + ")");
                foreach (DataRow _dr in _drArr)
                {
                    _dr.Delete();
                }
                DataRow _drNew;
                foreach (DataRow _dr in _drArrR)
                {
                    _drNew = _dsNo.Tables[0].NewRow();
                    _drNew["PGM"] = _dr["PGM"];
                    _drNew["VALUE"] = _dr["VALUE"];
                    _dsNo.Tables[0].Rows.Add(_drNew);

                    _dr.Delete();
                }
                _dsNo.AcceptChanges();

                DataView _dvNo = _dsNo.Tables[0].DefaultView;
                DataView _dvYes = _dsYes.Tables[0].DefaultView;
                _dvNo.Sort = "ITM ASC";
                _dvYes.Sort = "ITM ASC";

                lstNoRight.DataSource = _dvNo;
                lstNoRight.DisplayMember = "VALUE";
                lstNoRight.ValueMember = "PGM";

                lstYesRight.DataSource = _dvYes;
                lstYesRight.DisplayMember = "VALUE";
                lstYesRight.ValueMember = "PGM";
            }
        }

        private void btn4Usr_Click(object sender, EventArgs e)
        {
            QueryWin _qw = new QueryWin();
            _qw.PGM = "PSWD";
            _qw.SQLWhere += " AND (ISNULL(E_DAT,GETDATE()+1) >=GETDATE() OR E_DAT='1899-12-30')";
            if (_qw.ShowDialog() == DialogResult.OK)
            {
                txtNo.Text = _qw.NO_RT;
                txtName.Text = _qw.NAME_RTN;
                txtNo_Leave(null, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNo.Text != "")
                {
                    onlineService.BarPrintServer _bps = new BarCodePrintTSB.onlineService.BarPrintServer();
                    _bps.UseDefaultCredentials = true;
                    _bps.UpdateUserRight(_dsYes, txtNo.Text);
                    MessageBox.Show("保存成功！");
                    txtNo.Text = "";
                    txtName.Text = "";
                    UserRightMng_Load(null, null);
                }
                else
                {
                    MessageBox.Show("请选择用户！");
                }
            }
            catch (Exception _ex)
            {
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("保存失败！原因：\n" + _err);
            }
        }
    }
}