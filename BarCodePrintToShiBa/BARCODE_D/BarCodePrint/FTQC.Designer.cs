namespace BarCodePrintTSB
{
    partial class FTQC
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
            this.btnOKA = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lstEligibleA = new System.Windows.Forms.ListBox();
            this.txtEligibleRemA = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtSpcNoB = new System.Windows.Forms.TextBox();
            this.btnSpcNoB = new System.Windows.Forms.Button();
            this.txtSpcNameB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOKB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEligibleRemB = new System.Windows.Forms.TextBox();
            this.lstEligibleB = new System.Windows.Forms.ListBox();
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
            this.btnUnEligibleA = new System.Windows.Forms.Button();
            this.btnEligibleA = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBarNo = new System.Windows.Forms.TextBox();
            this.btnUnEligibleB = new System.Windows.Forms.Button();
            this.btnEligibleB = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.lstUnQC);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 547);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待品检的成品条码";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(119, 20);
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
            this.lstUnQC.Location = new System.Drawing.Point(6, 48);
            this.lstUnQC.Name = "lstUnQC";
            this.lstUnQC.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnQC.Size = new System.Drawing.Size(188, 485);
            this.lstUnQC.TabIndex = 0;
            this.lstUnQC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstUnQC_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOKA);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lstEligibleA);
            this.groupBox2.Controls.Add(this.txtEligibleRemA);
            this.groupBox2.Location = new System.Drawing.Point(254, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 203);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "合格A类成品条码";
            // 
            // btnOKA
            // 
            this.btnOKA.Location = new System.Drawing.Point(505, 149);
            this.btnOKA.Name = "btnOKA";
            this.btnOKA.Size = new System.Drawing.Size(75, 23);
            this.btnOKA.TabIndex = 15;
            this.btnOKA.Text = "确认";
            this.btnOKA.UseVisualStyleBackColor = true;
            this.btnOKA.Click += new System.EventHandler(this.btnOKA_Click);
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
            // lstEligibleA
            // 
            this.lstEligibleA.FormattingEnabled = true;
            this.lstEligibleA.Location = new System.Drawing.Point(6, 20);
            this.lstEligibleA.Name = "lstEligibleA";
            this.lstEligibleA.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstEligibleA.Size = new System.Drawing.Size(188, 173);
            this.lstEligibleA.TabIndex = 0;
            this.lstEligibleA.SelectedIndexChanged += new System.EventHandler(this.lstEligibleA_SelectedIndexChanged);
            this.lstEligibleA.DoubleClick += new System.EventHandler(this.lstEligibleA_DoubleClick);
            // 
            // txtEligibleRemA
            // 
            this.txtEligibleRemA.Location = new System.Drawing.Point(275, 20);
            this.txtEligibleRemA.Multiline = true;
            this.txtEligibleRemA.Name = "txtEligibleRemA";
            this.txtEligibleRemA.Size = new System.Drawing.Size(316, 102);
            this.txtEligibleRemA.TabIndex = 16;
            this.txtEligibleRemA.Leave += new System.EventHandler(this.txtEligibleRemA_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtSpcNoB);
            this.groupBox5.Controls.Add(this.btnSpcNoB);
            this.groupBox5.Controls.Add(this.txtSpcNameB);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.btnOKB);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtEligibleRemB);
            this.groupBox5.Controls.Add(this.lstEligibleB);
            this.groupBox5.Location = new System.Drawing.Point(254, 221);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(608, 200);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "合格B类成品条码";
            // 
            // txtSpcNoB
            // 
            this.txtSpcNoB.Location = new System.Drawing.Point(432, 15);
            this.txtSpcNoB.Name = "txtSpcNoB";
            this.txtSpcNoB.Size = new System.Drawing.Size(10, 20);
            this.txtSpcNoB.TabIndex = 26;
            this.txtSpcNoB.Visible = false;
            // 
            // btnSpcNoB
            // 
            this.btnSpcNoB.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnSpcNoB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSpcNoB.Location = new System.Drawing.Point(405, 16);
            this.btnSpcNoB.Name = "btnSpcNoB";
            this.btnSpcNoB.Size = new System.Drawing.Size(20, 22);
            this.btnSpcNoB.TabIndex = 25;
            this.btnSpcNoB.UseVisualStyleBackColor = true;
            this.btnSpcNoB.Click += new System.EventHandler(this.btnSpcNoB_Click);
            // 
            // txtSpcNameB
            // 
            this.txtSpcNameB.Location = new System.Drawing.Point(275, 16);
            this.txtSpcNameB.Name = "txtSpcNameB";
            this.txtSpcNameB.Size = new System.Drawing.Size(130, 20);
            this.txtSpcNameB.TabIndex = 24;
            this.txtSpcNameB.Leave += new System.EventHandler(this.txtSpcNameB_Leave);
            this.txtSpcNameB.Enter += new System.EventHandler(this.txtSpcNameB_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "原因";
            // 
            // btnOKB
            // 
            this.btnOKB.Location = new System.Drawing.Point(505, 147);
            this.btnOKB.Name = "btnOKB";
            this.btnOKB.Size = new System.Drawing.Size(75, 23);
            this.btnOKB.TabIndex = 16;
            this.btnOKB.Text = "确认";
            this.btnOKB.UseVisualStyleBackColor = true;
            this.btnOKB.Click += new System.EventHandler(this.btnOKB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "备注";
            // 
            // txtEligibleRemB
            // 
            this.txtEligibleRemB.Location = new System.Drawing.Point(275, 47);
            this.txtEligibleRemB.Multiline = true;
            this.txtEligibleRemB.Name = "txtEligibleRemB";
            this.txtEligibleRemB.Size = new System.Drawing.Size(316, 80);
            this.txtEligibleRemB.TabIndex = 14;
            this.txtEligibleRemB.Leave += new System.EventHandler(this.txtEligibleRemB_Leave);
            // 
            // lstEligibleB
            // 
            this.lstEligibleB.FormattingEnabled = true;
            this.lstEligibleB.Location = new System.Drawing.Point(6, 20);
            this.lstEligibleB.Name = "lstEligibleB";
            this.lstEligibleB.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstEligibleB.Size = new System.Drawing.Size(188, 173);
            this.lstEligibleB.TabIndex = 13;
            this.lstEligibleB.SelectedIndexChanged += new System.EventHandler(this.lstEligibleB_SelectedIndexChanged);
            this.lstEligibleB.DoubleClick += new System.EventHandler(this.lstEligibleB_DoubleClick);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(218, 465);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(30, 23);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = ">>";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnUnReject
            // 
            this.btnUnReject.Location = new System.Drawing.Point(218, 494);
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
            this.groupBox3.Location = new System.Drawing.Point(254, 427);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(608, 184);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "不合格成品条码";
            // 
            // lstPrcId
            // 
            this.lstPrcId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstPrcId.FormattingEnabled = true;
            this.lstPrcId.Items.AddRange(new object[] {
            "1.报废处理",
            "2.重开制令单",
            "3.强制缴库"});
            this.lstPrcId.Location = new System.Drawing.Point(275, 46);
            this.lstPrcId.Name = "lstPrcId";
            this.lstPrcId.Size = new System.Drawing.Size(150, 21);
            this.lstPrcId.TabIndex = 27;
            this.lstPrcId.SelectionChangeCommitted += new System.EventHandler(this.lstPrcId_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "处理方式";
            // 
            // btnOk1
            // 
            this.btnOk1.Location = new System.Drawing.Point(505, 155);
            this.btnOk1.Name = "btnOk1";
            this.btnOk1.Size = new System.Drawing.Size(75, 23);
            this.btnOk1.TabIndex = 24;
            this.btnOk1.Text = "确认";
            this.btnOk1.UseVisualStyleBackColor = true;
            this.btnOk1.Click += new System.EventHandler(this.btnOk1_Click);
            // 
            // txtSpcNo
            // 
            this.txtSpcNo.Location = new System.Drawing.Point(432, 16);
            this.txtSpcNo.Name = "txtSpcNo";
            this.txtSpcNo.Size = new System.Drawing.Size(10, 20);
            this.txtSpcNo.TabIndex = 22;
            this.txtSpcNo.Visible = false;
            // 
            // btnSpcNo
            // 
            this.btnSpcNo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btnSpcNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSpcNo.Location = new System.Drawing.Point(405, 17);
            this.btnSpcNo.Name = "btnSpcNo";
            this.btnSpcNo.Size = new System.Drawing.Size(20, 22);
            this.btnSpcNo.TabIndex = 21;
            this.btnSpcNo.UseVisualStyleBackColor = true;
            this.btnSpcNo.Click += new System.EventHandler(this.btnSpcNo_Click);
            // 
            // txtSpcName
            // 
            this.txtSpcName.Location = new System.Drawing.Point(275, 16);
            this.txtSpcName.Name = "txtSpcName";
            this.txtSpcName.Size = new System.Drawing.Size(130, 20);
            this.txtSpcName.TabIndex = 20;
            this.txtSpcName.Leave += new System.EventHandler(this.txtSpcName_Leave);
            this.txtSpcName.Enter += new System.EventHandler(this.txtSpcName_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "异常原因";
            // 
            // btnToTR
            // 
            this.btnToTR.Location = new System.Drawing.Point(349, 155);
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
            this.label3.Location = new System.Drawing.Point(238, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "备注";
            // 
            // txtRejectRem
            // 
            this.txtRejectRem.Location = new System.Drawing.Point(275, 74);
            this.txtRejectRem.Multiline = true;
            this.txtRejectRem.Name = "txtRejectRem";
            this.txtRejectRem.Size = new System.Drawing.Size(316, 74);
            this.txtRejectRem.TabIndex = 14;
            this.txtRejectRem.Leave += new System.EventHandler(this.txtRejectRem_Leave);
            this.txtRejectRem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRejectRem_KeyPress);
            // 
            // lstReject
            // 
            this.lstReject.FormattingEnabled = true;
            this.lstReject.Location = new System.Drawing.Point(6, 20);
            this.lstReject.Name = "lstReject";
            this.lstReject.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstReject.Size = new System.Drawing.Size(188, 160);
            this.lstReject.TabIndex = 0;
            this.lstReject.SelectedIndexChanged += new System.EventHandler(this.lstReject_SelectedIndexChanged);
            this.lstReject.DoubleClick += new System.EventHandler(this.lstReject_DoubleClick);
            // 
            // btnUnEligibleA
            // 
            this.btnUnEligibleA.Location = new System.Drawing.Point(218, 141);
            this.btnUnEligibleA.Name = "btnUnEligibleA";
            this.btnUnEligibleA.Size = new System.Drawing.Size(30, 23);
            this.btnUnEligibleA.TabIndex = 6;
            this.btnUnEligibleA.Text = "<<";
            this.btnUnEligibleA.UseVisualStyleBackColor = true;
            this.btnUnEligibleA.Click += new System.EventHandler(this.btnUnEligibleA_Click);
            // 
            // btnEligibleA
            // 
            this.btnEligibleA.Location = new System.Drawing.Point(218, 111);
            this.btnEligibleA.Name = "btnEligibleA";
            this.btnEligibleA.Size = new System.Drawing.Size(30, 23);
            this.btnEligibleA.TabIndex = 5;
            this.btnEligibleA.Text = ">>";
            this.btnEligibleA.UseVisualStyleBackColor = true;
            this.btnEligibleA.Click += new System.EventHandler(this.btnEligibleA_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtBarNo);
            this.groupBox4.Location = new System.Drawing.Point(12, 565);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 46);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "成品品检撤销条码扫描";
            // 
            // txtBarNo
            // 
            this.txtBarNo.Location = new System.Drawing.Point(6, 20);
            this.txtBarNo.Name = "txtBarNo";
            this.txtBarNo.Size = new System.Drawing.Size(188, 20);
            this.txtBarNo.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtBarNo, "如果要修改成品条码品质信息，\r\n在此文本框中扫入成品条码，\r\n条码即进入上面的待品检改成品条码列表中，\r\n对条码重新进行品检即可。");
            this.txtBarNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarNo_KeyDown);
            this.txtBarNo.Leave += new System.EventHandler(this.txtBarNo_Leave);
            // 
            // btnUnEligibleB
            // 
            this.btnUnEligibleB.Location = new System.Drawing.Point(218, 284);
            this.btnUnEligibleB.Name = "btnUnEligibleB";
            this.btnUnEligibleB.Size = new System.Drawing.Size(30, 23);
            this.btnUnEligibleB.TabIndex = 16;
            this.btnUnEligibleB.Text = "<<";
            this.btnUnEligibleB.UseVisualStyleBackColor = true;
            this.btnUnEligibleB.Click += new System.EventHandler(this.btnUnEligibleB_Click);
            // 
            // btnEligibleB
            // 
            this.btnEligibleB.Location = new System.Drawing.Point(218, 255);
            this.btnEligibleB.Name = "btnEligibleB";
            this.btnEligibleB.Size = new System.Drawing.Size(30, 23);
            this.btnEligibleB.TabIndex = 15;
            this.btnEligibleB.Text = ">>";
            this.btnEligibleB.UseVisualStyleBackColor = true;
            this.btnEligibleB.Click += new System.EventHandler(this.btnEligibleB_Click);
            // 
            // FTQC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 623);
            this.Controls.Add(this.btnUnEligibleB);
            this.Controls.Add(this.btnEligibleB);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnUnEligibleA);
            this.Controls.Add(this.btnEligibleA);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnUnReject);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FTQC";
            this.Text = "成品品检";
            this.Load += new System.EventHandler(this.FTQC_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstUnQC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstEligibleA;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnUnReject;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstReject;
        private System.Windows.Forms.Button btnUnEligibleA;
        private System.Windows.Forms.Button btnEligibleA;
        private System.Windows.Forms.TextBox txtEligibleRemA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRejectRem;
        private System.Windows.Forms.Button btnToTR;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtBarNo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lstEligibleB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEligibleRemB;
        private System.Windows.Forms.Button btnSpcNo;
        private System.Windows.Forms.TextBox txtSpcName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSpcNo;
        private System.Windows.Forms.Button btnUnEligibleB;
        private System.Windows.Forms.Button btnEligibleB;
        private System.Windows.Forms.Button btnOKA;
        private System.Windows.Forms.Button btnOKB;
        private System.Windows.Forms.TextBox txtSpcNoB;
        private System.Windows.Forms.Button btnSpcNoB;
        private System.Windows.Forms.TextBox txtSpcNameB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOk1;
        private System.Windows.Forms.ComboBox lstPrcId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}