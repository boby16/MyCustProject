namespace BarCodePrintTSB
{
    partial class BarUndoToMM
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
            this.lstUnToMM = new System.Windows.Forms.ListBox();
            this.lstToMM = new System.Windows.Forms.ListBox();
            this.btnUnmm = new System.Windows.Forms.Button();
            this.btnTomm = new System.Windows.Forms.Button();
            this.cbPrdType = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn4BatNo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatNo = new System.Windows.Forms.TextBox();
            this.btn4Prdt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstUnToMM
            // 
            this.lstUnToMM.FormattingEnabled = true;
            this.lstUnToMM.Location = new System.Drawing.Point(245, 50);
            this.lstUnToMM.Name = "lstUnToMM";
            this.lstUnToMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnToMM.Size = new System.Drawing.Size(180, 472);
            this.lstUnToMM.TabIndex = 0;
            // 
            // lstToMM
            // 
            this.lstToMM.FormattingEnabled = true;
            this.lstToMM.Location = new System.Drawing.Point(6, 50);
            this.lstToMM.Name = "lstToMM";
            this.lstToMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstToMM.Size = new System.Drawing.Size(180, 472);
            this.lstToMM.TabIndex = 5;
            // 
            // btnUnmm
            // 
            this.btnUnmm.Location = new System.Drawing.Point(199, 233);
            this.btnUnmm.Name = "btnUnmm";
            this.btnUnmm.Size = new System.Drawing.Size(30, 23);
            this.btnUnmm.TabIndex = 23;
            this.btnUnmm.Text = ">>";
            this.btnUnmm.UseVisualStyleBackColor = true;
            this.btnUnmm.Click += new System.EventHandler(this.btnUnmm_Click);
            // 
            // btnTomm
            // 
            this.btnTomm.Location = new System.Drawing.Point(199, 301);
            this.btnTomm.Name = "btnTomm";
            this.btnTomm.Size = new System.Drawing.Size(30, 23);
            this.btnTomm.TabIndex = 22;
            this.btnTomm.Text = "<<";
            this.btnTomm.UseVisualStyleBackColor = true;
            this.btnTomm.Click += new System.EventHandler(this.btnTomm_Click);
            // 
            // cbPrdType
            // 
            this.cbPrdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrdType.FormattingEnabled = true;
            this.cbPrdType.Items.AddRange(new object[] {
            "板材",
            "半成品",
            "成品"});
            this.cbPrdType.Location = new System.Drawing.Point(73, 33);
            this.cbPrdType.Name = "cbPrdType";
            this.cbPrdType.Size = new System.Drawing.Size(150, 21);
            this.cbPrdType.TabIndex = 24;
            this.cbPrdType.SelectionChangeCommitted += new System.EventHandler(this.cbPrdType_SelectionChangeCommitted);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(168, 129);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 25);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.Text = "查询";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(350, 19);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 25);
            this.btnUndo.TabIndex = 26;
            this.btnUndo.Text = "送缴撤销";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstToMM);
            this.groupBox1.Controls.Add(this.btnUndo);
            this.groupBox1.Controls.Add(this.btnTomm);
            this.groupBox1.Controls.Add(this.btnUnmm);
            this.groupBox1.Controls.Add(this.lstUnToMM);
            this.groupBox1.Location = new System.Drawing.Point(275, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 528);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待送缴撤销的条码";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn4BatNo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtBatNo);
            this.groupBox2.Controls.Add(this.btn4Prdt);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtPrdNo);
            this.groupBox2.Controls.Add(this.cbPrdType);
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 528);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "过滤条件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "货品类别";
            // 
            // btn4BatNo
            // 
            this.btn4BatNo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4BatNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4BatNo.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4BatNo.Location = new System.Drawing.Point(223, 86);
            this.btn4BatNo.Name = "btn4BatNo";
            this.btn4BatNo.Size = new System.Drawing.Size(20, 20);
            this.btn4BatNo.TabIndex = 38;
            this.btn4BatNo.UseVisualStyleBackColor = true;
            this.btn4BatNo.Click += new System.EventHandler(this.btn4BatNo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "批号";
            // 
            // txtBatNo
            // 
            this.txtBatNo.Location = new System.Drawing.Point(73, 86);
            this.txtBatNo.Name = "txtBatNo";
            this.txtBatNo.Size = new System.Drawing.Size(150, 20);
            this.txtBatNo.TabIndex = 36;
            // 
            // btn4Prdt
            // 
            this.btn4Prdt.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4Prdt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4Prdt.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4Prdt.Location = new System.Drawing.Point(223, 60);
            this.btn4Prdt.Name = "btn4Prdt";
            this.btn4Prdt.Size = new System.Drawing.Size(20, 20);
            this.btn4Prdt.TabIndex = 35;
            this.btn4Prdt.UseVisualStyleBackColor = true;
            this.btn4Prdt.Click += new System.EventHandler(this.btn4Prdt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrdNo.Location = new System.Drawing.Point(73, 60);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.Size = new System.Drawing.Size(150, 20);
            this.txtPrdNo.TabIndex = 33;
            // 
            // BarUndoToMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 548);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BarUndoToMM";
            this.Text = "送缴撤销";
            this.Load += new System.EventHandler(this.BarUndoToMM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstUnToMM;
        private System.Windows.Forms.ListBox lstToMM;
        private System.Windows.Forms.Button btnUnmm;
        private System.Windows.Forms.Button btnTomm;
        private System.Windows.Forms.ComboBox cbPrdType;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn4BatNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatNo;
        private System.Windows.Forms.Button btn4Prdt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrdNo;
        private System.Windows.Forms.Label label1;

    }
}