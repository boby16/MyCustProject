using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BarCodePrintTSB
{
    public partial class ProcessForm : Form
    {
        public ProcessForm()
        {
            InitializeComponent();
        }

        #region 属性
        bool _cancel = false;
        /// <summary>
        /// 中断进程否
        /// </summary>
        public bool Cancel
        {
            get { return _cancel; }
        }
        #endregion

        private void ProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cancel = true;
        }
    }
}