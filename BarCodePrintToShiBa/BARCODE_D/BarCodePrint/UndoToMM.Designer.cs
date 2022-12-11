namespace BarCodePrint
{
    partial class UndoToMM
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
            this.SuspendLayout();
            // 
            // lstUnToMM
            // 
            this.lstUnToMM.FormattingEnabled = true;
            this.lstUnToMM.ItemHeight = 12;
            this.lstUnToMM.Location = new System.Drawing.Point(251, 58);
            this.lstUnToMM.Name = "lstUnToMM";
            this.lstUnToMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnToMM.Size = new System.Drawing.Size(180, 436);
            this.lstUnToMM.TabIndex = 0;
            // 
            // lstToMM
            // 
            this.lstToMM.FormattingEnabled = true;
            this.lstToMM.ItemHeight = 12;
            this.lstToMM.Location = new System.Drawing.Point(12, 58);
            this.lstToMM.Name = "lstToMM";
            this.lstToMM.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstToMM.Size = new System.Drawing.Size(180, 436);
            this.lstToMM.TabIndex = 5;
            // 
            // btnUnmm
            // 
            this.btnUnmm.Location = new System.Drawing.Point(205, 227);
            this.btnUnmm.Name = "btnUnmm";
            this.btnUnmm.Size = new System.Drawing.Size(30, 21);
            this.btnUnmm.TabIndex = 23;
            this.btnUnmm.Text = ">>";
            this.btnUnmm.UseVisualStyleBackColor = true;
            this.btnUnmm.Click += new System.EventHandler(this.btnUnmm_Click);
            // 
            // btnTomm
            // 
            this.btnTomm.Location = new System.Drawing.Point(205, 290);
            this.btnTomm.Name = "btnTomm";
            this.btnTomm.Size = new System.Drawing.Size(30, 21);
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
            this.cbPrdType.Location = new System.Drawing.Point(45, 12);
            this.cbPrdType.Name = "cbPrdType";
            this.cbPrdType.Size = new System.Drawing.Size(121, 20);
            this.cbPrdType.TabIndex = 24;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(205, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(343, 12);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 26;
            this.btnUndo.Text = "送缴撤销";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // UndoToMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 506);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cbPrdType);
            this.Controls.Add(this.lstUnToMM);
            this.Controls.Add(this.lstToMM);
            this.Controls.Add(this.btnUnmm);
            this.Controls.Add(this.btnTomm);
            this.Name = "UndoToMM";
            this.Text = "送缴撤销";
            this.Load += new System.EventHandler(this.UndoToMM_Load);
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

    }
}