namespace BarCodePrintTSB
{
    partial class BarBoxUP
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstUnBoxBarA = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxNo = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstBarCode = new System.Windows.Forms.ListBox();
            this.txtFTBar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.lstUnBoxBarB = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstUnBoxBarR = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstUnBoxBarA);
            this.groupBox1.Location = new System.Drawing.Point(4, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "A级品";
            // 
            // lstUnBoxBarA
            // 
            this.lstUnBoxBarA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnBoxBarA.FormattingEnabled = true;
            this.lstUnBoxBarA.ItemHeight = 12;
            this.lstUnBoxBarA.Location = new System.Drawing.Point(3, 17);
            this.lstUnBoxBarA.Name = "lstUnBoxBarA";
            this.lstUnBoxBarA.Size = new System.Drawing.Size(214, 172);
            this.lstUnBoxBarA.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(160, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(60, 21);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "箱条码";
            // 
            // txtBoxNo
            // 
            this.txtBoxNo.Location = new System.Drawing.Point(315, 46);
            this.txtBoxNo.Name = "txtBoxNo";
            this.txtBoxNo.Size = new System.Drawing.Size(140, 21);
            this.txtBoxNo.TabIndex = 2;
            this.txtBoxNo.Leave += new System.EventHandler(this.txtBoxNo_Leave);
            this.txtBoxNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxNo_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearch.Location = new System.Drawing.Point(455, 46);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(20, 20);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lstBarCode
            // 
            this.lstBarCode.FormattingEnabled = true;
            this.lstBarCode.ItemHeight = 12;
            this.lstBarCode.Location = new System.Drawing.Point(269, 248);
            this.lstBarCode.Name = "lstBarCode";
            this.lstBarCode.Size = new System.Drawing.Size(216, 244);
            this.lstBarCode.TabIndex = 4;
            this.lstBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBarCode_KeyDown);
            // 
            // txtFTBar
            // 
            this.txtFTBar.Location = new System.Drawing.Point(315, 221);
            this.txtFTBar.Name = "txtFTBar";
            this.txtFTBar.Size = new System.Drawing.Size(170, 21);
            this.txtFTBar.TabIndex = 6;
            this.txtFTBar.Leave += new System.EventHandler(this.txtFTBar_Leave);
            this.txtFTBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFTBar_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "成品条码";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(396, 503);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 21);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.Location = new System.Drawing.Point(315, 72);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.ReadOnly = true;
            this.txtPrdNo.Size = new System.Drawing.Size(140, 21);
            this.txtPrdNo.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "品号";
            // 
            // txtBatNo
            // 
            this.txtBatNo.Location = new System.Drawing.Point(315, 145);
            this.txtBatNo.Name = "txtBatNo";
            this.txtBatNo.ReadOnly = true;
            this.txtBatNo.Size = new System.Drawing.Size(140, 21);
            this.txtBatNo.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "装箱数量";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(315, 170);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(140, 21);
            this.txtQty.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(278, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "批号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(278, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "中类";
            // 
            // txtIdx
            // 
            this.txtIdx.Location = new System.Drawing.Point(315, 120);
            this.txtIdx.Name = "txtIdx";
            this.txtIdx.ReadOnly = true;
            this.txtIdx.Size = new System.Drawing.Size(140, 21);
            this.txtIdx.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(278, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "规格";
            // 
            // txtSpc
            // 
            this.txtSpc.Location = new System.Drawing.Point(315, 96);
            this.txtSpc.Name = "txtSpc";
            this.txtSpc.ReadOnly = true;
            this.txtSpc.Size = new System.Drawing.Size(140, 21);
            this.txtSpc.TabIndex = 15;
            // 
            // lstUnBoxBarB
            // 
            this.lstUnBoxBarB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnBoxBarB.FormattingEnabled = true;
            this.lstUnBoxBarB.ItemHeight = 12;
            this.lstUnBoxBarB.Location = new System.Drawing.Point(3, 17);
            this.lstUnBoxBarB.Name = "lstUnBoxBarB";
            this.lstUnBoxBarB.Size = new System.Drawing.Size(214, 172);
            this.lstUnBoxBarB.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstUnBoxBarB);
            this.groupBox2.Location = new System.Drawing.Point(4, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 193);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "B级品";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstUnBoxBarR);
            this.groupBox3.Location = new System.Drawing.Point(4, 430);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 155);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "不合格品强制缴库";
            // 
            // lstUnBoxBarR
            // 
            this.lstUnBoxBarR.FormattingEnabled = true;
            this.lstUnBoxBarR.ItemHeight = 12;
            this.lstUnBoxBarR.Location = new System.Drawing.Point(4, 15);
            this.lstUnBoxBarR.Name = "lstUnBoxBarR";
            this.lstUnBoxBarR.Size = new System.Drawing.Size(210, 136);
            this.lstUnBoxBarR.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.btnRefresh);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Location = new System.Drawing.Point(2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(227, 594);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "待装箱成品条码";
            // 
            // BarBoxUP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 596);
            this.Controls.Add(this.groupBox4);
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
            this.Controls.Add(this.txtFTBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstBarCode);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtBoxNo);
            this.Controls.Add(this.label1);
            this.Name = "BarBoxUP";
            this.Text = "装箱作业";
            this.Load += new System.EventHandler(this.BarBoxUP_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstUnBoxBarA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxNo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstBarCode;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtFTBar;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.ListBox lstUnBoxBarB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstUnBoxBarR;
        private System.Windows.Forms.GroupBox groupBox4;

    }
}