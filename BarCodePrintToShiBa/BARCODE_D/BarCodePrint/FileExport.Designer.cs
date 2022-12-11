namespace System.Windows.Forms
{
    partial class FileExport
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
            this.gbFileName = new System.Windows.Forms.GroupBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cPanel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.gbExportType = new System.Windows.Forms.GroupBox();
            this.rdoExcel = new System.Windows.Forms.RadioButton();
            this.rdoTxt = new System.Windows.Forms.RadioButton();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.gbFileName.SuspendLayout();
            this.cPanel2.SuspendLayout();
            this.gbExportType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFileName
            // 
            this.gbFileName.Controls.Add(this.btnFile);
            this.gbFileName.Controls.Add(this.txtFileName);
            this.gbFileName.Location = new System.Drawing.Point(7, 86);
            this.gbFileName.Name = "gbFileName";
            this.gbFileName.Size = new System.Drawing.Size(280, 52);
            this.gbFileName.TabIndex = 14;
            this.gbFileName.TabStop = false;
            this.gbFileName.Text = "文件名称";
            // 
            // btnFile
            // 
            this.btnFile.BackColor = System.Drawing.SystemColors.Control;
            this.btnFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFile.Location = new System.Drawing.Point(239, 15);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(22, 20);
            this.btnFile.TabIndex = 3;
            this.btnFile.Text = "...";
            this.btnFile.UseVisualStyleBackColor = false;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(7, 15);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(226, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // cPanel2
            // 
            this.cPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.cPanel2.Controls.Add(this.btnClose);
            this.cPanel2.Controls.Add(this.btnSubmit);
            this.cPanel2.Location = new System.Drawing.Point(7, 144);
            this.cPanel2.Name = "cPanel2";
            this.cPanel2.Size = new System.Drawing.Size(280, 37);
            this.cPanel2.TabIndex = 15;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(152, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.SystemColors.Control;
            this.btnSubmit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSubmit.Location = new System.Drawing.Point(56, 8);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(67, 23);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "转出";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // gbExportType
            // 
            this.gbExportType.Controls.Add(this.rdoExcel);
            this.gbExportType.Controls.Add(this.rdoTxt);
            this.gbExportType.Location = new System.Drawing.Point(7, 12);
            this.gbExportType.Name = "gbExportType";
            this.gbExportType.Size = new System.Drawing.Size(280, 68);
            this.gbExportType.TabIndex = 13;
            this.gbExportType.TabStop = false;
            this.gbExportType.Text = "转出类型";
            // 
            // rdoExcel
            // 
            this.rdoExcel.Checked = true;
            this.rdoExcel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdoExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdoExcel.Location = new System.Drawing.Point(3, 38);
            this.rdoExcel.Name = "rdoExcel";
            this.rdoExcel.Size = new System.Drawing.Size(274, 22);
            this.rdoExcel.TabIndex = 1;
            this.rdoExcel.TabStop = true;
            this.rdoExcel.Text = "Excel格式";
            this.rdoExcel.UseCompatibleTextRendering = true;
            this.rdoExcel.CheckedChanged += new System.EventHandler(this.rdoExcel_CheckedChanged);
            // 
            // rdoTxt
            // 
            this.rdoTxt.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdoTxt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdoTxt.Location = new System.Drawing.Point(3, 16);
            this.rdoTxt.Name = "rdoTxt";
            this.rdoTxt.Size = new System.Drawing.Size(274, 22);
            this.rdoTxt.TabIndex = 0;
            this.rdoTxt.Text = "文本格式";
            this.rdoTxt.UseCompatibleTextRendering = true;
            this.rdoTxt.CheckedChanged += new System.EventHandler(this.rdoTxt_CheckedChanged);
            // 
            // fileDialog
            // 
            this.fileDialog.CheckFileExists = false;
            this.fileDialog.Filter = "Excel File|*.xls";
            // 
            // FileExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 186);
            this.Controls.Add(this.gbFileName);
            this.Controls.Add(this.cPanel2);
            this.Controls.Add(this.gbExportType);
            this.Name = "FileExport";
            this.Text = "数据导出";
            this.Load += new System.EventHandler(this.FileExport_Load);
            this.gbFileName.ResumeLayout(false);
            this.gbFileName.PerformLayout();
            this.cPanel2.ResumeLayout(false);
            this.gbExportType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gbFileName;
        private Button btnFile;
        private TextBox txtFileName;
        private Panel cPanel2;
        private Button btnClose;
        private Button btnSubmit;
        private GroupBox gbExportType;
        private RadioButton rdoExcel;
        private RadioButton rdoTxt;
        private OpenFileDialog fileDialog;

    }
}