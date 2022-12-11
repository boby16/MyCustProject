namespace BarCodePrintTSB
{
    partial class FTInitAutoBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstUnBoxBar = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxNo = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstBarCode = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBatNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIdx = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSpc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWhName = new System.Windows.Forms.TextBox();
            this.btnWH = new System.Windows.Forms.Button();
            this.txtWH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRem = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnUnToBox = new System.Windows.Forms.Button();
            this.btnToBox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstUnBoxBar
            // 
            this.lstUnBoxBar.FormattingEnabled = true;
            this.lstUnBoxBar.Location = new System.Drawing.Point(7, 34);
            this.lstUnBoxBar.Name = "lstUnBoxBar";
            this.lstUnBoxBar.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnBoxBar.Size = new System.Drawing.Size(210, 589);
            this.lstUnBoxBar.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(121, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(97, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(475, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "箱条码";
            // 
            // txtBoxNo
            // 
            this.txtBoxNo.Location = new System.Drawing.Point(524, 31);
            this.txtBoxNo.Name = "txtBoxNo";
            this.txtBoxNo.Size = new System.Drawing.Size(140, 20);
            this.txtBoxNo.TabIndex = 2;
            this.txtBoxNo.Leave += new System.EventHandler(this.txtBoxNo_Leave);
            this.txtBoxNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxNo_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearch.Location = new System.Drawing.Point(664, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(20, 20);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lstBarCode
            // 
            this.lstBarCode.FormattingEnabled = true;
            this.lstBarCode.Location = new System.Drawing.Point(247, 34);
            this.lstBarCode.Name = "lstBarCode";
            this.lstBarCode.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBarCode.Size = new System.Drawing.Size(210, 589);
            this.lstBarCode.TabIndex = 4;
            this.lstBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBarCode_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(589, 297);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.Location = new System.Drawing.Point(524, 60);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.ReadOnly = true;
            this.txtPrdNo.Size = new System.Drawing.Size(140, 20);
            this.txtPrdNo.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(487, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "品号";
            // 
            // txtBatNo
            // 
            this.txtBatNo.Location = new System.Drawing.Point(524, 139);
            this.txtBatNo.Name = "txtBatNo";
            this.txtBatNo.ReadOnly = true;
            this.txtBatNo.Size = new System.Drawing.Size(140, 20);
            this.txtBatNo.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(463, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "装箱数量";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(524, 166);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(140, 20);
            this.txtQty.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(487, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "批号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(487, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "中类";
            // 
            // txtIdx
            // 
            this.txtIdx.Location = new System.Drawing.Point(524, 112);
            this.txtIdx.Name = "txtIdx";
            this.txtIdx.ReadOnly = true;
            this.txtIdx.Size = new System.Drawing.Size(140, 20);
            this.txtIdx.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(487, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "规格";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(524, 86);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(140, 20);
            this.txtSpc.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "待装箱成品条码";
            // 
            // txtWhName
            // 
            this.txtWhName.Location = new System.Drawing.Point(524, 193);
            this.txtWhName.Name = "txtWhName";
            this.txtWhName.Size = new System.Drawing.Size(140, 20);
            this.txtWhName.TabIndex = 51;
            this.txtWhName.Enter += new System.EventHandler(this.txtWhName_Enter);
            this.txtWhName.Leave += new System.EventHandler(this.txtWhName_Leave);
            // 
            // btnWH
            // 
            this.btnWH.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnWH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnWH.Location = new System.Drawing.Point(664, 192);
            this.btnWH.Name = "btnWH";
            this.btnWH.Size = new System.Drawing.Size(20, 20);
            this.btnWH.TabIndex = 50;
            this.btnWH.UseVisualStyleBackColor = true;
            this.btnWH.Click += new System.EventHandler(this.btnWH_Click);
            // 
            // txtWH
            // 
            this.txtWH.Location = new System.Drawing.Point(465, 193);
            this.txtWH.Name = "txtWH";
            this.txtWH.Size = new System.Drawing.Size(16, 20);
            this.txtWH.TabIndex = 49;
            this.txtWH.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(487, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "库位";
            // 
            // txtRem
            // 
            this.txtRem.Location = new System.Drawing.Point(524, 222);
            this.txtRem.Name = "txtRem";
            this.txtRem.Size = new System.Drawing.Size(140, 20);
            this.txtRem.TabIndex = 53;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(487, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "备注";
            // 
            // btnUnToBox
            // 
            this.btnUnToBox.Location = new System.Drawing.Point(217, 350);
            this.btnUnToBox.Name = "btnUnToBox";
            this.btnUnToBox.Size = new System.Drawing.Size(30, 23);
            this.btnUnToBox.TabIndex = 63;
            this.btnUnToBox.Text = "<<";
            this.btnUnToBox.UseVisualStyleBackColor = true;
            this.btnUnToBox.Click += new System.EventHandler(this.btnUnToBox_Click);
            // 
            // btnToBox
            // 
            this.btnToBox.Location = new System.Drawing.Point(217, 297);
            this.btnToBox.Name = "btnToBox";
            this.btnToBox.Size = new System.Drawing.Size(30, 23);
            this.btnToBox.TabIndex = 62;
            this.btnToBox.Text = ">>";
            this.btnToBox.UseVisualStyleBackColor = true;
            this.btnToBox.Click += new System.EventHandler(this.btnToBox_Click);
            // 
            // FTInitAutoBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 645);
            this.Controls.Add(this.btnUnToBox);
            this.Controls.Add(this.btnToBox);
            this.Controls.Add(this.txtRem);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtWhName);
            this.Controls.Add(this.btnWH);
            this.Controls.Add(this.txtWH);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lstUnBoxBar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSpc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtIdx);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBatNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPrdNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstBarCode);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtBoxNo);
            this.Controls.Add(this.label1);
            this.Name = "FTInitAutoBox";
            this.Text = "期初成品装箱作业";
            this.Load += new System.EventHandler(this.FTInitAutoBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstUnBoxBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxNo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstBarCode;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtPrdNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBatNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIdx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSpc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWhName;
        private System.Windows.Forms.Button btnWH;
        private System.Windows.Forms.TextBox txtWH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnUnToBox;
        private System.Windows.Forms.Button btnToBox;

    }
}