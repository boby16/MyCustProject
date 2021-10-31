using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class FileOut : Form
    {
        /// <summary>
        /// 单据数据表
        /// </summary>
        public DataSet BilDS;

        public FileOut()
        {
            InitializeComponent();
        }

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                string _path = folderBrowserDialog1.SelectedPath;
                if (_path.Substring(_path.Length - 1, 1) != "\\")
                {
                    _path += "\\";
                }
                this.txtFilePath.Text = _path;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataTable _dt = this.BilDS.Tables["TF_PSS3"];
            _dt.AcceptChanges();
            System.IO.StreamWriter _sw = null;
            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            try
            {
                if (_dt.Rows.Count > 0)
                {
                    string[] _aryLine = new string[5];
                    StringBuilder _lineStr = new StringBuilder();
                    string _psNo = "";
                    if (_dt.Rows.Count > 0)
                    {
                        _psNo = _dt.Rows[0]["PS_NO"].ToString();
                        _sw = new System.IO.StreamWriter(this.txtFilePath.Text + _psNo + ".txt", false, System.Text.Encoding.Default);
                    }
                    //导出表身数据
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        if (_psNo != _dt.Rows[i]["PS_NO"].ToString())
                        {
                            _sw = new System.IO.StreamWriter(this.txtFilePath.Text + _dt.Rows[i]["PS_NO"].ToString() + ".txt", false, System.Text.Encoding.Default);
                            _lineStr = new StringBuilder();
                        }
                        //序列号转出
                        string _barCode = _dt.Rows[i]["BAR_CODE"].ToString();
                        if (_sb.ToString().IndexOf(_barCode) < 0)
                        {
                            _sb.Append(_barCode + ",");
                            _aryLine[BarRole.BarDoc.BarPos] = _barCode;
                            if (BarRole.BarDoc.CusPos >= 0)
                            {
                                _aryLine[BarRole.BarDoc.CusPos] = this.BilDS.Tables[0].Rows[0]["CUS_NO"].ToString();
                            }
                            if (BarRole.BarDoc.BatPos >= 0)
                            {
                                _aryLine[BarRole.BarDoc.BatPos] = _dt.Rows[i]["BAT_NO"].ToString();
                            }
                            if (BarRole.BarDoc.WhPos >= 0)
                            {
                                _aryLine[BarRole.BarDoc.WhPos] = _dt.Rows[i]["WH"].ToString();
                            }
                            if (BarRole.BarDoc.Wh2Pos >= 0 && _dt.Columns.Contains("WH2"))
                            {
                                _aryLine[BarRole.BarDoc.Wh2Pos] = _dt.Rows[i]["WH2"].ToString();
                            }
                            _lineStr = new StringBuilder();
                            for (int j = BarRole.BarDoc.BarPos; j < 5; j++)
                            {
                                if (j > BarRole.BarDoc.BarPos)
                                {
                                    _lineStr.Append(BarRole.BarDoc.SplitChar.ToString());
                                }
                                _lineStr.Append(_aryLine[j]);
                            }
                            _sw.WriteLine(_lineStr.ToString());
                        }
                    }
                    _sw.Close();
                    MessageBox.Show("文件转出成功！");//文件转出成功！
                    this.Close();
                }
                else
                {
                    MessageBox.Show("序列号没输入，无法转出！");//序列号没输入，无法转出！
                }
            }
            catch (Exception _ex)
            {
                string _err = "";
                if (_ex.Message.Length > 500)
                    _err = _ex.Message.Substring(0, 500);
                else
                    _err = _ex.Message;
                MessageBox.Show("序列号转出失败！原因：\n"+_err);
                if (_sw != null)
                {
                    _sw.Close();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}