using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace System.Windows.Forms
{
    public partial class FileExport : Form
    {
        public Dictionary<string, string> _columnLst = new Dictionary<string, string>();
        public DataSet _sourceDataSet = null;

        public FileExport()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            try
            {
                string _fileName = this.txtFileName.Text;
                // ********************************
                //			整合数据内容
                // ********************************
                string _content = this.GetContent();
                // **************************
                //			输出文件
                // **************************
                if (this.rdoTxt.Checked)
                {
                    this.BuildTxtFile(_fileName, _content);
                }
                else if (this.rdoExcel.Checked)
                {
                    this.BuildExeclFile(_fileName, _content);
                }
                MessageBox.Show("转出成功");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("转出失败:" + "\r\n" + ex.Message.Substring(0,50));
            }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnFile_Click(object sender, System.EventArgs e)
        {
            if (this.fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = this.fileDialog.FileName;
            }
        }
        private string GetContent()
        {
            DataTable _dataSource = this._sourceDataSet.Tables[0].Copy();

            StringBuilder _sb = new StringBuilder();
            if (_dataSource != null)
            {
                // 标题内容
                bool _isFirst = true;
                foreach (KeyValuePair<string, string> _kvp in _columnLst)
                {
                    if (!_isFirst)
                    {
                        _sb.Append("\t");
                    }
                    else
                    {
                        _isFirst = false;
                    }
                    _sb.Append(_kvp.Value);
                }
                //数据内容
                DataRow[] _drs = _dataSource.Select();
                foreach (DataRow _dr in _drs)
                {
                    _isFirst = true;
                    _sb.Append("\r\n");
                    foreach (KeyValuePair<string, string> _kvp in _columnLst)
                    {
                        if (!_isFirst)
                            _sb.Append("\t");
                        else
                            _isFirst = false;
                        _sb.Append(_dr[_kvp.Key].ToString().Replace("\r\n",""));
                    }
                }
                _dataSource.Dispose();
            }
            return _sb.ToString();
        }
        private void BuildTxtFile(string fileName, string content)
        {
            using (FileStream _fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] _text = new UTF8Encoding().GetBytes(content);
                _fs.Write(_text, 0, _text.Length);
                _fs.Close();
            }
        }
        private void BuildExeclFile(string fileName, string content)
        {
            // 复制到剪切板
            Clipboard.SetDataObject(content);
            object _eOpt = System.Reflection.Missing.Value;

            // 创建Excel
            Excel.Application _eExcel = new Excel.Application();
            Excel.Workbooks _eBooks = (Excel.Workbooks)_eExcel.Workbooks;
            Excel._Workbook _eBook = (Excel._Workbook)(_eBooks.Add(_eOpt));

            // 把数据粘贴在A1
            Excel.Sheets _eSheets = (Excel.Sheets)_eBook.Worksheets;
            Excel._Worksheet _eSheet = (Excel._Worksheet)(_eSheets.get_Item(1));
            Excel.Range _eRange = _eSheet.get_Range("A1", _eOpt);
            _eSheet.Paste(_eRange, false);

            // 保存文件
            _eBook.SaveAs(fileName, _eOpt, _eOpt,
                _eOpt, _eOpt, _eOpt, Excel.XlSaveAsAccessMode.xlNoChange, _eOpt, _eOpt, _eOpt, _eOpt);
            _eBook.Close(false, _eOpt, _eOpt);
            _eExcel.Quit();
        }

        private void rdoExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoExcel.Checked)
            {
                this.txtFileName.Text = System.IO.Path.ChangeExtension(this.txtFileName.Text, ".xls");
            }
        }

        private void rdoTxt_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoTxt.Checked)
            {
                this.txtFileName.Text = System.IO.Path.ChangeExtension(this.txtFileName.Text, ".txt");
            }
        }

        private void FileExport_Load(object sender, EventArgs e)
        {
            string _dir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            string _fileName = "Data.xls";
            this.txtFileName.Text = _dir + @"\" + _fileName;
        }
    }
}