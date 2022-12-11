namespace BarCodePrintTSB
{
    partial class BCQC
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lstUnQC = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCusNo = new System.Windows.Forms.TextBox();
            this.btnCust = new System.Windows.Forms.Button();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lstEligible = new System.Windows.Forms.ListBox();
            this.txtEligibleRem = new System.Windows.Forms.TextBox();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnUnReject = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstPrcId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOk1 = new System.Windows.Forms.Button();
            this.txtSpcNo = new System.Windows.Forms.TextBox();
            this.btnSpcNo = new System.Windows.Forms.Button();
            this.txtSpcName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnToTR = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRejectRem = new System.Windows.Forms.TextBox();
            this.lstReject = new System.Windows.Forms.ListBox();
            this.btnUnEligible = new System.Windows.Forms.Button();
            this.btnEligible = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBarNo = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lstUnMM = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.lstUnQC);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 400);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待品检的板材条码";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(119, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lstUnQC
            // 
            this.lstUnQC.FormattingEnabled = true;
            this.lstUnQC.Location = new System.Drawing.Point(6, 46);
            this.lstUnQC.Name = "lstUnQC";
            this.lstUnQC.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnQC.Size = new System.Drawing.Size(188, 342);
            this.lstUnQC.TabIndex = 0;
            this.lstUnQC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstUnQC_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCusNo);
            this.groupBox2.Controls.Add(this.btnCust);
            this.groupBox2.Controls.Add(this.txtCusName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnOK);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lstEligible);
            this.groupBox2.Controls.Add(this.txtEligibleRem);
            this.groupBox2.Location = new System.Drawing.Point(254, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 295);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "合格板材条码";
            // 
            // txtCusNo
            // 
            this.txtCusNo.Location = new System.Drawing.Point(468, 177);
            this.txtCusNo.Name = "txtCusNo";
            this.txtCusNo.Size = new System.Drawing.Size(20, 20);
            this.txtCusNo.TabIndex = 27;
            this.txtCusNo.Visible = false;
            // 
            // btnCust
            // 
            this.btnCust.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnCust.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCust.Location = new System.Drawing.Point(405, 176);
            this.btnCust.Name = "btnCust";
            this.btnCust.Size = new System.Drawing.Size(20, 22);
            this.btnCust.TabIndex = 26;
            this.btnCust.UseVisualStyleBackColor = true;
            this.btnCust.Click += new System.EventHandler(this.btnCust_Click);
            // 
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(275, 176);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(130, 20);
            this.txtCusName.TabIndex = 25;
            this.txtCusName.Leave += new System.EventHandler(this.txtCusName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "意向客户";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(486, 215);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "备注";
            // 
            // lstEligible
            // 
            this.lstEligible.FormattingEnabled = true;
            this.lstEligible.Location = new System.Drawing.Point(6, 22);
            this.lstEligible.Name = "lstEligible";
            this.lstEligible.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstEligible.Size = new System.Drawing.Size(188, 264);
            this.lstEligible.TabIndex = 0;
            this.lstEligible.SelectedIndexChanged += new System.EventHandler(this.lstEligible_SelectedIndexChanged);
            this.lstEligible.DoubleClick += new System.EventHandler(this.lstEligible_DoubleClick);
            // 
            // txtEligibleRem
            // 
            this.txtEligibleRem.Location = new System.Drawing.Point(275, 20);
            this.txtEligibleRem.Multiline = true;
            this.txtEligibleRem.Name = "txtEligibleRem";
            this.txtEligibleRem.Size = new System.Drawing.Size(316, 150);
            this.txtEligibleRem.TabIndex = 10;
            this.txtEligibleRem.Leave += new System.EventHandler(this.txtEligibleRem_Leave);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(218, 328);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(30, 23);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = ">>";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnUnReject
            // 
            this.btnUnReject.Location = new System.Drawing.Point(218, 373);
            this.btnUnReject.Name = "btnUnReject";
            this.btnUnReject.Size = new System.Drawing.Size(30, 23);
            this.btnUnReject.TabIndex = 3;
            this.btnUnReject.Text = "<<";
            this.btnUnReject.UseVisualStyleBackColor = true;
            this.btnUnReject.Click += new System.EventHandler(this.btnUnReject_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstPrcId);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnOk1);
            this.groupBox3.Controls.Add(this.txtSpcNo);
            this.groupBox3.Controls.Add(this.btnSpcNo);
            this.groupBox3.Controls.Add(this.txtSpcName);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnToTR);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtRejectRem);
            this.groupBox3.Controls.Add(this.lstReject);
            this.groupBox3.Location = new System.Drawing.Point(254, 313);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(608, 296);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "不合格板材条码";
            // 
            // lstPrcId
            // 
            this.lstPrcId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstPrcId.FormattingEnabled = true;
            this.lstPrcId.Items.AddRange(new object[] {
            "1.报废处理",
            "2.重开制令单",
            "3.强制缴库"});
            this.lstPrcId.Location = new System.Drawing.Point(275, 55);
            this.lstPrcId.Name = "lstPrcId";
            this.lstPrcId.Size = new System.Drawing.Size(150, 21);
            this.lstPrcId.TabIndex = 22;
            this.lstPrcId.SelectionChangeCommitted += new System.EventHandler(this.lstPrcId_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(216, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "处理方式";
            // 
            // btnOk1
            // 
            this.btnOk1.Location = new System.Drawing.Point(486, 225);
            this.btnOk1.Name = "btnOk1";
            this.btnOk1.Size = new System.Drawing.Size(75, 23);
            this.btnOk1.TabIndex = 20;
            this.btnOk1.Text = "确认";
            this.btnOk1.UseVisualStyleBackColor = true;
            this.btnOk1.Click += new System.EventHandler(this.btnOk1_Click);
            // 
            // txtSpcNo
            // 
            this.txtSpcNo.Location = new System.Drawing.Point(468, 27);
            this.txtSpcNo.Name = "txtSpcNo";
            this.txtSpcNo.Size = new System.Drawing.Size(20, 20);
            this.txtSpcNo.TabIndex = 19;
            this.txtSpcNo.Visible = false;
            // 
            // btnSpcNo
            // 
            this.btnSpcNo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnSpcNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSpcNo.Location = new System.Drawing.Point(405, 26);
            this.btnSpcNo.Name = "btnSpcNo";
            this.btnSpcNo.Size = new System.Drawing.Size(20, 22);
            this.btnSpcNo.TabIndex = 18;
            this.btnSpcNo.UseVisualStyleBackColor = true;
            this.btnSpcNo.Click += new System.EventHandler(this.btnSpcNo_Click);
            // 
            // txtSpcName
            // 
            this.txtSpcName.Location = new System.Drawing.Point(275, 26);
            this.txtSpcName.Name = "txtSpcName";
            this.txtSpcName.Size = new System.Drawing.Size(130, 20);
            this.txtSpcName.TabIndex = 17;
            this.txtSpcName.Leave += new System.EventHandler(this.txtSpcName_Leave);
            this.txtSpcName.Enter += new System.EventHandler(this.txtSpcName_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "异常原因";
            // 
            // btnToTR
            // 
            this.btnToTR.Location = new System.Drawing.Point(332, 225);
            this.btnToTR.Name = "btnToTR";
            this.btnToTR.Size = new System.Drawing.Size(93, 23);
            this.btnToTR.TabIndex = 14;
            this.btnToTR.Text = "转异常通知单";
            this.btnToTR.UseVisualStyleBackColor = true;
            this.btnToTR.Visible = false;
            this.btnToTR.Click += new System.EventHandler(this.btnToTR_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "备注";
            // 
            // txtRejectRem
            // 
            this.txtRejectRem.Location = new System.Drawing.Point(275, 83);
            this.txtRejectRem.Multiline = true;
            this.txtRejectRem.Name = "txtRejectRem";
            this.txtRejectRem.Size = new System.Drawing.Size(316, 103);
            this.txtRejectRem.TabIndex = 14;
            this.txtRejectRem.Leave += new System.EventHandler(this.txtRejectRem_Leave);
            this.txtRejectRem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRejectRem_KeyPress);
            // 
            // lstReject
            // 
            this.lstReject.FormattingEnabled = true;
            this.lstReject.Location = new System.Drawing.Point(6, 24);
            this.lstReject.Name = "lstReject";
            this.lstReject.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstReject.Size = new System.Drawing.Size(188, 264);
            this.lstReject.TabIndex = 0;
            this.lstReject.SelectedIndexChanged += new System.EventHandler(this.lstReject_SelectedIndexChanged);
            this.lstReject.DoubleClick += new System.EventHandler(this.lstReject_DoubleClick);
            // 
            // btnUnEligible
            // 
            this.btnUnEligible.Location = new System.Drawing.Point(218, 215);
            this.btnUnEligible.Name = "btnUnEligible";
            this.btnUnEligible.Size = new System.Drawing.Size(30, 23);
            this.btnUnEligible.TabIndex = 6;
            this.btnUnEligible.Text = "<<";
            this.btnUnEligible.UseVisualStyleBackColor = true;
            this.btnUnEligible.Click += new System.EventHandler(this.btnUnEligible_Click);
            // 
            // btnEligible
            // 
            this.btnEligible.Location = new System.Drawing.Point(218, 169);
            this.btnEligible.Name = "btnEligible";
            this.btnEligible.Size = new System.Drawing.Size(30, 23);
            this.btnEligible.TabIndex = 5;
            this.btnEligible.Text = ">>";
            this.btnEligible.UseVisualStyleBackColor = true;
            this.btnEligible.Click += new System.EventHandler(this.btnEligible_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtBarNo);
            this.groupBox4.Location = new System.Drawing.Point(12, 418);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 59);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "板材品检撤销条码扫描";
            // 
            // txtBarNo
            // 
            this.txtBarNo.Location = new System.Drawing.Point(6, 22);
            this.txtBarNo.Name = "txtBarNo";
            this.txtBarNo.Size = new System.Drawing.Size(188, 20);
            this.txtBarNo.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtBarNo, "如果要修改板材条码品质信息，\r\n在此文本框中扫入板材条码，\r\n条码即进入上面的待品检板材条码列表中，\r\n对条码重新进行品检即可。");
            this.txtBarNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarNo_KeyDown);
            this.txtBarNo.Leave += new System.EventHandler(this.txtBarNo_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lstUnMM);
            this.groupBox5.Location = new System.Drawing.Point(12, 483);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 126);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "可修改品质信息的板材条码";
            // 
            // lstUnMM
            // 
            this.lstUnMM.FormattingEnabled = true;
            this.lstUnMM.Location = new System.Drawing.Point(6, 20);
            this.lstUnMM.Name = "lstUnMM";
            this.lstUnMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnMM.Size = new System.Drawing.Size(188, 95);
            this.lstUnMM.TabIndex = 0;
            // 
            // BCQC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 623);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnUnEligible);
            this.Controls.Add(this.btnEligible);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnUnReject);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BCQC";
            this.Text = "板材品检";
            this.Load += new System.EventHandler(this.BCQC_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstUnQC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstEligible;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnUnReject;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstReject;
        private System.Windows.Forms.Button btnUnEligible;
        private System.Windows.Forms.Button btnEligible;
        private System.Windows.Forms.TextBox txtEligibleRem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRejectRem;
        private System.Windows.Forms.Button btnToTR;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtBarNo;
        private System.Windows.Forms.Button btnSpcNo;
        private System.Windows.Forms.TextBox txtSpcName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSpcNo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lstUnMM;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOk1;
        private System.Windows.Forms.ComboBox lstPrcId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtCusNo;
        private System.Windows.Forms.Button btnCust;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.Label label1;

    }
}