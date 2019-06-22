using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace QRCodePrint
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private DataSet myDataSet1 = null;
        private DataSet myDataSet2 = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.myDataSet1 = DbMannager.GetData();
                if (this.myDataSet1 != null && this.myDataSet1.Tables.Count > 0 && this.myDataSet1.Tables["QRCodeData"].Rows.Count > 0)
                {
                    this.myDataSet2 = this.myDataSet1.Copy();
                    this.cbPNBox.DataSource = this.myDataSet1.Tables["QRCodeData"];
                    this.cbPNBox.DisplayMember = "昆山固品料号";
                    this.cbPNBox.ValueMember = "昆山固品料号";

                    this.cbPNPrd.DataSource = this.myDataSet2.Tables["QRCodeData"];
                    this.cbPNPrd.DisplayMember = "昆山固品料号";
                    this.cbPNPrd.ValueMember = "昆山固品料号";
                }

                this.txtSnoBox.Text = TSCLIB_DLL.GetBoxSerialNo(DateTime.Now);

                this.txtSnoPrd.Text = TSCLIB_DLL.GetBoxSerialNo(DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrintOrderNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtOrderNo.Text != "" && this.txtQty.Text != "")
                {
                    TSCLIB_DLL.OrderNoBarCodePrint(this.txtOrderNo.Text, this.txtQty.Text);
                }
                else
                {
                    MessageBox.Show("请将数据输入完整！");
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogs(ex.StackTrace + ex.Source + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBoxPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtBatNoBox.Text != string.Empty && this.txtDwgBox.Text != string.Empty && this.txtFoxPNBox.Text != string.Empty && this.txtQtyBox.Text != string.Empty && this.txtSpcBox.Text != string.Empty)

                {
                    BoxVO boxVo = new BoxVO
                    {
                        CompName = "昆山固品工程塑料有限公司",
                        GPPrdName = this.cbPNBox.SelectedValue.ToString(),
                        DWGRev = this.txtDwgBox.Text,
                        LotNo = this.txtBatNoBox.Text,
                        PDate = Convert.ToDateTime(this.txtDateBox.Text),
                        PN = this.txtFoxPNBox.Text,
                        Qty = this.txtQtyTotalBox.Text,
                        SerialNo = this.txtSnoBox.Text,
                        SPC = this.txtSpcBox.Text,
                        OrderNo = this.txtOrderNoBox.Text,
                        PrintCount = Convert.ToInt32(this.txtPrintCountBox.Text)
                    };
                    TSCLIB_DLL.BoxPrint(boxVo);
                    this.txtSnoBox.Text = TSCLIB_DLL.GetBoxSerialNo(boxVo.PDate);
                }
                else
                {
                    MessageBox.Show("请将数据输入完整！");
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogs(ex.StackTrace + ex.Source + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrintProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtBatNoPrd.Text != string.Empty && this.txtDwgPrd.Text != string.Empty && this.txtFoxPNPrd.Text != string.Empty && this.txtQtyPrd.Text != string.Empty && this.txtSpcPrd.Text != string.Empty)
                {
                    ProductVO productVo = new ProductVO
                    {
                        CompName = "昆山固品工程塑料有限公司",
                        GPPrdName = this.cbPNPrd.SelectedValue.ToString(),
                        DWGRev = this.txtDwgPrd.Text,
                        LotNo = this.txtBatNoPrd.Text,
                        PDate = Convert.ToDateTime(this.txtDatePrd.Text),
                        PN = this.txtFoxPNPrd.Text,
                        Qty = this.txtQtyPrd.Text,
                        SerialNo = this.txtSnoPrd.Text,
                        SPC = this.txtSpcPrd.Text,
                        OrderNo = this.txtOrderNoPrd.Text,
                        PrintCount = Convert.ToInt32(this.txtPrintCountPrd.Text)
                    };
                    TSCLIB_DLL.ProductPrint(productVo);
                    this.txtSnoPrd.Text = TSCLIB_DLL.GetBoxSerialNo(productVo.PDate);
                }
                else
                {
                    MessageBox.Show("请将数据输入完整！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogs(ex.StackTrace + ex.Source + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void cbPNBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataRow[] array = this.myDataSet1.Tables[0].Select("昆山固品料号='" + this.cbPNBox.SelectedValue + "'");
            if (array != null && array.Length > 0)
            {
                DataRow dataRow = array[0];
                this.txtFoxPNBox.Text = dataRow["富士康料号"].ToString();
                this.txtSpcBox.Text = dataRow["规格"].ToString();
                this.txtQtyBox.Text = dataRow["单品净重KG"].ToString();

            }
        }

        private void cbPNPrd_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow[] array = this.myDataSet2.Tables[0].Select("昆山固品料号='" + this.cbPNPrd.SelectedValue + "'");
            if (array != null && array.Length > 0)
            {
                DataRow dataRow = array[0];
                this.txtFoxPNPrd.Text = dataRow["富士康料号"].ToString();
                this.txtSpcPrd.Text = dataRow["规格"].ToString();
                this.txtQtyPrd.Text = dataRow["单品净重KG"].ToString();

            }
        }

        private void txtCountBox_Leave(object sender, EventArgs e)
        {
            if (this.txtQtyBox.Text != "" && this.txtCountBox.Text != "")
            {
                decimal d = 0m;
                int value = 0;

                if (decimal.TryParse(this.txtQtyBox.Text, out d) && int.TryParse(this.txtCountBox.Text, out value))
                {
                    this.txtQtyTotalBox.Text = (d * value).ToString("F1");
                }
            }
        }

        private void btnUnBoxPrdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtOrderNoB.Text != "" && this.txtPrintCount.Text != "")
                {
                    TSCLIB_DLL.OrderNoBarCodePrintNoBox(this.txtOrderNoB.Text, this.txtPrintCount.Text);
                }
                else
                {
                    MessageBox.Show("请将数据输入完整！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogs(ex.StackTrace + ex.Source + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Name == "tabPage1")
            {
                this.txtSnoBox.Text = TSCLIB_DLL.GetBoxSerialNo(DateTime.Now);
            }
            else if (this.tabControl1.SelectedTab.Name == "tabPage3")
            {
                this.txtSnoPrd.Text = TSCLIB_DLL.GetBoxSerialNo(DateTime.Now);
            }
        }

        private void txtDateBox_ValueChanged(object sender, EventArgs e)
        {
            var dateTime = Convert.ToDateTime(txtDateBox.Text);
            var dateCur = dateTime.Year.ToString() + "" + ((dateTime.Month < 10) ? ("0" + dateTime.Month.ToString()) : dateTime.Month.ToString())
                + "" + ((dateTime.Day < 10) ? ("0" + dateTime.Day.ToString()) : dateTime.Day.ToString());

            var sNoBox = TSCLIB_DLL.GetBoxSerialNo(dateTime);
            this.txtSnoBox.Text = dateCur + sNoBox.Substring(8);
        }

        private void txtDatePrd_ValueChanged(object sender, EventArgs e)
        {
            var dateTime = Convert.ToDateTime(txtDatePrd.Text);
            var dateCur = dateTime.Year.ToString() + "" + ((dateTime.Month < 10) ? ("0" + dateTime.Month.ToString()) : dateTime.Month.ToString())
                + "" + ((dateTime.Day < 10) ? ("0" + dateTime.Day.ToString()) : dateTime.Day.ToString());

            var sNoPrd = TSCLIB_DLL.GetBoxSerialNo(dateTime);
            this.txtSnoPrd.Text = dateCur + sNoPrd.Substring(8);
        }
    }
}