namespace BarCodePrintTSB
{
    partial class BarUndoMM
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
            this.lstUnMM = new System.Windows.Forms.ListBox();
            this.lstMM = new System.Windows.Forms.ListBox();
            this.btnUnMM = new System.Windows.Forms.Button();
            this.btnMM = new System.Windows.Forms.Button();
            this.cbPrdType = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn4BatNo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatNo = new System.Windows.Forms.TextBox();
            this.btn4Prdt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrdNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstUnMM
            // 
            this.lstUnMM.FormattingEnabled = true;
            this.lstUnMM.Location = new System.Drawing.Point(245, 50);
            this.lstUnMM.Name = "lstUnMM";
            this.lstUnMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnMM.Size = new System.Drawing.Size(180, 472);
            this.lstUnMM.TabIndex = 0;
            // 
            // lstMM
            // 
            this.lstMM.FormattingEnabled = true;
            this.lstMM.Location = new System.Drawing.Point(6, 50);
            this.lstMM.Name = "lstMM";
            this.lstMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMM.Size = new System.Drawing.Size(180, 472);
            this.lstMM.TabIndex = 5;
            // 
            // btnUnMM
            // 
            this.btnUnMM.Location = new System.Drawing.Point(199, 233);
            this.btnUnMM.Name = "btnUnMM";
            this.btnUnMM.Size = new System.Drawing.Size(30, 23);
            this.btnUnMM.TabIndex = 23;
            this.btnUnMM.Text = ">>";
            this.btnUnMM.UseVisualStyleBackColor = true;
            this.btnUnMM.Click += new System.EventHandler(this.btnUnmm_Click);
            // 
            // btnMM
            // 
            this.btnMM.Location = new System.Drawing.Point(199, 301);
            this.btnMM.Name = "btnMM";
            this.btnMM.Size = new System.Drawing.Size(30, 23);
            this.btnMM.TabIndex = 22;
            this.btnMM.Text = "<<";
            this.btnMM.UseVisualStyleBackColor = true;
            this.btnMM.Click += new System.EventHandler(this.btnTomm_Click);
            // 
            // cbPrdType
            // 
            this.cbPrdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrdType.FormattingEnabled = true;
            this.cbPrdType.Items.AddRange(new object[] {
            "1、板材",
            "2、半成品",
            "3、成品"});
            this.cbPrdType.Location = new System.Drawing.Point(86, 34);
            this.cbPrdType.Name = "cbPrdType";
            this.cbPrdType.Size = new System.Drawing.Size(150, 21);
            this.cbPrdType.TabIndex = 24;
            this.cbPrdType.SelectionChangeCommitted += new System.EventHandler(this.cbPrdType_SelectionChangeCommitted);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(181, 164);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(334, 19);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 25);
            this.btnUndo.TabIndex = 26;
            this.btnUndo.Text = "缴库撤销";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn4BatNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtBatNo);
            this.groupBox1.Controls.Add(this.btn4Prdt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPrdNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.cbPrdType);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 528);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过滤条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 483);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 39);
            this.label3.TabIndex = 37;
            this.label3.Text = "**说明**\r\n请确认需要缴库撤销的条码\r\n已从对应的ERP缴库单中删除";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn4BatNo
            // 
            this.btn4BatNo.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4BatNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4BatNo.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4BatNo.Location = new System.Drawing.Point(236, 87);
            this.btn4BatNo.Name = "btn4BatNo";
            this.btn4BatNo.Size = new System.Drawing.Size(20, 20);
            this.btn4BatNo.TabIndex = 32;
            this.btn4BatNo.UseVisualStyleBackColor = true;
            this.btn4BatNo.Click += new System.EventHandler(this.btn4BatNo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "批号";
            // 
            // txtBatNo
            // 
            this.txtBatNo.Location = new System.Drawing.Point(86, 87);
            this.txtBatNo.Name = "txtBatNo";
            this.txtBatNo.Size = new System.Drawing.Size(150, 20);
            this.txtBatNo.TabIndex = 30;
            // 
            // btn4Prdt
            // 
            this.btn4Prdt.BackgroundImage = global::BarCodePrintTSB.Properties.Resources.qry;
            this.btn4Prdt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn4Prdt.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4Prdt.Location = new System.Drawing.Point(236, 61);
            this.btn4Prdt.Name = "btn4Prdt";
            this.btn4Prdt.Size = new System.Drawing.Size(20, 20);
            this.btn4Prdt.TabIndex = 29;
            this.btn4Prdt.UseVisualStyleBackColor = true;
            this.btn4Prdt.Click += new System.EventHandler(this.btn4Prdt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "品号";
            // 
            // txtPrdNo
            // 
            this.txtPrdNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrdNo.Location = new System.Drawing.Point(86, 61);
            this.txtPrdNo.Name = "txtPrdNo";
            this.txtPrdNo.Size = new System.Drawing.Size(150, 20);
            this.txtPrdNo.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "货品类别";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstMM);
            this.groupBox2.Controls.Add(this.btnMM);
            this.groupBox2.Controls.Add(this.btnUndo);
            this.groupBox2.Controls.Add(this.btnUnMM);
            this.groupBox2.Controls.Add(this.lstUnMM);
            this.groupBox2.Location = new System.Drawing.Point(290, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 528);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "待缴库撤销的条码";
            // 
            // BarUndoMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 543);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BarUndoMM";
            this.Text = "缴库撤销";
            this.Load += new System.EventHandler(this.BarUndoMM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstUnMM;
        private System.Windows.Forms.ListBox lstMM;
        private System.Windows.Forms.Button btnUnMM;
        private System.Windows.Forms.Button btnMM;
        private System.Windows.Forms.ComboBox cbPrdType;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn4Prdt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrdNo;
        private System.Windows.Forms.Button btn4BatNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatNo;
        private System.Windows.Forms.Label label3;

    }
}