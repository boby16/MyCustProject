using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace QRCodePrintTSB
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private DataSet myDataSet1 = null;
        private DataSet myDataSet2 = null;
        private DataSet myDataSet3 = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.myDataSet1 = DbMannager.GetData();
                if (this.myDataSet1 != null && this.myDataSet1.Tables.Count > 0 
                    && this.myDataSet1.Tables["QRCodeData"].Rows.Count > 0)
                {
                    this.myDataSet2 = this.myDataSet1.Copy();
                    this.myDataSet3 = this.myDataSet1.Copy();

                    this.cbPNBox.DataSource = this.myDataSet1.Tables["QRCodeData"];
                    this.cbPNBox.DisplayMember = "昆山固品料号";
                    this.cbPNBox.ValueMember = "昆山固品料号";

                    this.cbPNPrd.DataSource = this.myDataSet2.Tables["QRCodeData"];
                    this.cbPNPrd.DisplayMember = "昆山固品料号";
                    this.cbPNPrd.ValueMember = "昆山固品料号";

                    this.cbPNZB.DataSource = this.myDataSet3.Tables["QRCodeData"];
                    this.cbPNZB.DisplayMember = "昆山固品料号";
                    this.cbPNZB.ValueMember = "昆山固品料号";
                }

                this.txtSnoBox.Text = ToShiBaLIB_DLL.GetBoxSerialNo(DateTime.Now);

                this.txtSnoPrd.Text = ToShiBaLIB_DLL.GetBoxSerialNo(DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnZBPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtBanciZB.Text != string.Empty
                    && this.txtFoxPNZB.Text != string.Empty && this.txtQtyZB.Text != string.Empty
                    && this.txtSpcZB.Text != string.Empty && this.txtModelZB.Text != string.Empty)

                {
                    ZhanbanVO zhanbanVo = new ZhanbanVO
                    {
                        CompName = "昆山固品工程塑料有限公司",
                        CompNo = txtSupplierCodeZB.Text,
                        GPPrdName = this.cbPNZB.SelectedValue.ToString(),
                        PDate = Convert.ToDateTime(this.txtDateZB.Text),
                        PN = this.txtFoxPNZB.Text,
                        Qty = this.txtQtyTotalZB.Text,
                        SPC = this.txtSpcZB.Text,
                        OrderNo = this.txtOrderNoZB.Text,
                        ModelNo = this.txtModelZB.Text,
                        DWGRev = this.txtBanciZB.Text,
                        PrintCount = Convert.ToInt32(this.txtPrintCountZB.Text)
                    };
                    ToShiBaLIB_DLL.ZBPrint(zhanbanVo);
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
                if (this.txtBatNoBox.Text != string.Empty && this.txtBanciBox.Text != string.Empty 
                    && this.txtFoxPNBox.Text != string.Empty && this.txtQtyBox.Text != string.Empty 
                    && this.txtSpcBox.Text != string.Empty && this.txtModelBox.Text != string.Empty
                    && this.txtShiduBox.Text != string.Empty)

                {
                    BoxVO boxVo = new BoxVO
                    {
                        CompName = "昆山固品工程塑料有限公司",
                        CompNo = txtSupplierCodeBox.Text,
                        GPPrdName = this.cbPNBox.SelectedValue.ToString(),
                        LotNo = this.txtBatNoBox.Text,
                        PDate = Convert.ToDateTime(this.txtDateBox.Text),
                        PN = this.txtFoxPNBox.Text,
                        Qty = this.txtQtyTotalBox.Text,
                        SPC = this.txtSpcBox.Text,
                        OrderNo = this.txtOrderNoBox.Text,
                        ModelNo = this.txtModelBox.Text,
                        Shidu = this.txtShiduBox.Text,
                        SerialNo = this.txtSnoBox.Text,
                        DWGRev = this.txtBanciBox.Text,
                        PrintCount = Convert.ToInt32(this.txtPrintCountBox.Text)
                    };
                    ToShiBaLIB_DLL.BoxPrint(boxVo);
                    this.txtSnoBox.Text = ToShiBaLIB_DLL.GetBoxSerialNo(boxVo.PDate);
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
                if (this.txtBatNoPrd.Text != string.Empty && this.txtBanciPrd.Text != string.Empty
                    && this.txtFoxPNPrd.Text != string.Empty && this.txtQtyPrd.Text != string.Empty
                    && this.txtSpcPrd.Text != string.Empty && this.txtModelPrd.Text != string.Empty
                    && this.txtBanciPrd.Text != string.Empty)
                {
                    ProductVO productVo = new ProductVO
                    {
                        CompName = "昆山固品工程塑料有限公司",
                        CompNo = txtSupplierCodePrd.Text,
                        GPPrdName = this.cbPNPrd.SelectedValue.ToString(),
                        LotNo = this.txtBatNoPrd.Text,
                        PDate = Convert.ToDateTime(this.txtDatePrd.Text),
                        PN = this.txtFoxPNPrd.Text,
                        Qty = this.txtQtyPrd.Text,
                        SerialNo = this.txtSnoPrd.Text,
                        SPC = this.txtSpcPrd.Text,
                        ModelNo = this.txtModelPrd.Text,
                        Shidu = this.txtShiduPrd.Text,
                        DWGRev = this.txtBanciPrd.Text,
                        PrintCount = Convert.ToInt32(this.txtPrintCountPrd.Text)
                    };
                    ToShiBaLIB_DLL.ProductPrint(productVo);
                    this.txtSnoPrd.Text = ToShiBaLIB_DLL.GetBoxSerialNo(productVo.PDate);
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
                this.txtModelBox.Text = dataRow["型号"].ToString();

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
                this.txtModelPrd.Text = dataRow["型号"].ToString();

            }
        }


        private void cbPNZB_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow[] array = this.myDataSet3.Tables[0].Select("昆山固品料号='" + this.cbPNZB.SelectedValue + "'");
            if (array != null && array.Length > 0)
            {
                DataRow dataRow = array[0];
                this.txtFoxPNZB.Text = dataRow["富士康料号"].ToString();
                this.txtSpcZB.Text = dataRow["规格"].ToString();
                this.txtQtyZB.Text = dataRow["单品净重KG"].ToString();
                this.txtModelZB.Text = dataRow["型号"].ToString();

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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Name == "tabPageBox")
            {
                this.txtSnoBox.Text = ToShiBaLIB_DLL.GetBoxSerialNo(DateTime.Now);
            }
            else if (this.tabControl1.SelectedTab.Name == "tabPagePrd")
            {
                this.txtSnoPrd.Text = ToShiBaLIB_DLL.GetBoxSerialNo(DateTime.Now);
            }
        }

        private void txtDateBox_ValueChanged(object sender, EventArgs e)
        {
            var dateTime = Convert.ToDateTime(txtDatePrd.Text);
            var dateCur = dateTime.Year.ToString() + "" + ((dateTime.Month < 10) ? ("0" + dateTime.Month.ToString()) : dateTime.Month.ToString())
                + "" + ((dateTime.Day < 10) ? ("0" + dateTime.Day.ToString()) : dateTime.Day.ToString());

            var sNoPrd = ToShiBaLIB_DLL.GetBoxSerialNo(dateTime);
            this.txtSnoBox.Text = dateCur + sNoPrd.Substring(8);
        }

        private void txtDatePrd_ValueChanged(object sender, EventArgs e)
        {
            var dateTime = Convert.ToDateTime(txtDatePrd.Text);
            var dateCur = dateTime.Year.ToString() + "" + ((dateTime.Month < 10) ? ("0" + dateTime.Month.ToString()) : dateTime.Month.ToString())
                + "" + ((dateTime.Day < 10) ? ("0" + dateTime.Day.ToString()) : dateTime.Day.ToString());

            var sNoPrd = ToShiBaLIB_DLL.GetBoxSerialNo(dateTime);
            this.txtSnoPrd.Text = dateCur + sNoPrd.Substring(8);
        }

        private void txtCountZB_Leave(object sender, EventArgs e)
        {

            if (this.txtQtyZB.Text != "" && this.txtCountZB.Text != "")
            {
                decimal d = 0m;
                int value = 0;

                if (decimal.TryParse(this.txtQtyZB.Text, out d) && int.TryParse(this.txtCountZB.Text, out value))
                {
                    this.txtQtyTotalZB.Text = (d * value).ToString("F1");
                }
            }
        }
    }
}