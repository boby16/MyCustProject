using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using Sunlike.Common.CommonVar;
using Microsoft.VisualBasic;

namespace Sunlike.Windows.Forms
{
    public partial class CheckQTY : CDialogForm
    {
        #region properties
        private string _prdNo;
        /// <summary>
        /// 品号
        /// </summary>
        public string Prd_No
        {
            get { return _prdNo; }
            set { _prdNo = value; }
        }
        private string _prdMark;
        /// <summary>
        /// 特征
        /// </summary>
        public string Prd_Mark
        {
            get { return _prdMark; }
            set { _prdMark = value; }
        }

        private decimal _mf = 0;
        /// <summary>
        /// 门幅
        /// </summary>
        public decimal MF
        {
            get { return _mf; }
            set { _mf = value; }
        }

        private decimal _cd = 0;
        /// <summary>
        /// 长度
        /// </summary>
        public decimal CD
        {
            get { return _cd; }
            set { _cd = value; }
        }

        private decimal _qty;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal QTY
        {
            get { return _qty; }
            set { _qty = value; }
        }

        private decimal _result = 0;
        /// <summary>
        /// 返回的数量
        /// </summary>
        public decimal RESULT
        {
            get { return _result; }
            set { _result = value; }
        }

        private bool _isZDW;
        /// <summary>
        /// true:主=>副；false:副=>主
        /// </summary>
        public bool isZDW
        {
            get { return _isZDW; }
            set { _isZDW = value; }
        }
        #endregion

        public bool showPage = false;//如果主副单位换算公式不存在除主副单位之外的变量，则直接计算得到对应的数量，否则弹出变量输入框。
        private string _formula;//记录主副单位换算公式
        string _strList = "";//记录变量（不重复）   例：主单位数量=副单位数量+A+A+B；则_strList=“A;B”
        string _backList = "";//记录变量（重复）    例：主单位数量=副单位数量+A+A+B；则_backList=“A;A;B”
        SunlikeDataSet _dsPara = new SunlikeDataSet();//记录公式变量输入的历史值

        #region constructor
        public CheckQTY()
        {
            InitializeComponent();
        }
        #endregion

        ///   <summary>   
        ///   接受一个string类型的表达式并计算结果,返回一个object对象,静态方法   
        ///   </summary>   
        ///   <param   name="expression">表达式</param>   
        ///   <returns></returns>   
        private static object Calculate(string expression)
        {
            //   创建编译器实例。     
            //ICodeCompiler complier = (new CSharpCodeProvider().CreateCompiler());

            //   设置编译参数。     
            CompilerParameters paras = new CompilerParameters();
            paras.GenerateExecutable = false;
            paras.GenerateInMemory = true;
            paras.ReferencedAssemblies.Add("System.Windows.Forms.dll");

            //   创建动态代码。     
            StringBuilder classSource = new StringBuilder();
            classSource.Append("using System.Windows.Forms; ");
            classSource.Append("public   class   Calc \n");
            classSource.Append("{\n");
            classSource.Append("         public   object   Run()\n");
            classSource.Append("         {\n");
            classSource.Append("                 return   " + expression + ";\n");
            classSource.Append("         }\n");
            classSource.Append("}");

            //   编译代码。     
            //CompilerResults result = complier.CompileAssemblyFromSource(paras, classSource.ToString());
            CompilerResults result = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(paras, classSource.ToString());

            //   获取编译后的程序集。     
            Assembly assembly = result.CompiledAssembly;

            //   动态调用方法。     
            object eval = assembly.CreateInstance("Calc");
            MethodInfo method = eval.GetType().GetMethod("Run");
            object reobj = method.Invoke(eval, null);
            GC.Collect();
            return reobj;
        }

        private void GetFormula()
        {
            _formula = "";
            DataSet _ds = CLocalDataManager.GetLocalData("PRDT");
            if (_ds != null && _ds.Tables.Contains("PRDT"))
            {
                DataRow _dr = _ds.Tables["PRDT"].Rows.Find(Prd_No);
                if (_dr != null)
                {
                    _formula = Strings.StrConv(_dr["FORMULA"].ToString(), VbStrConv.SimplifiedChinese, 0); ;
                }
            }
            if (_formula != "")
            {
                string _formulaStr = _formula.Substring(10, _formula.Length - 10);
                int _pos = _formulaStr.IndexOf(';');
                if (_pos >= 0)
                {
                    string _zdwStr,_fdwStr;
                    _zdwStr = _formulaStr.Substring(0, _pos);
                    _fdwStr = _formulaStr.Substring(_pos + 1, _formulaStr.Length - _pos - 1);
                    if (isZDW)
                    {
                        _formula = _zdwStr;
                        txtFormula.Text = _zdwStr;
                        cLabel1.Text = "副单位数量";
                    }
                    else
                    {
                        _formula = _fdwStr;
                        txtFormula.Text = _fdwStr;
                        cLabel1.Text = "主单位数量";
                    }
                }
            }
        }

        /// <summary>
        /// 主/副单位数量换算
        /// </summary>
        /// <param name="_prdNo">货品代号</param>
        /// <param name="_qty">数量</param>
        /// <param name="_isZDW">主单位否（true:由主单位推算副单位；false：由副单位推算主单位）</param>
        /// <returns></returns>
        private void CheckFormula(string _formu)
        {
            _strList = "";
            _backList = "";
            showPage = false;
            string[] formuList;
            if (_formu != "")
            {
                string _str = "";
                bool _isVar = false;
                int _len, _position, _curP;
                string _validChar = "+-*/()0123456789.";

                while (true)
                {
                    _isVar = false;
                    _len = _formu.Length;
                    if (_len <= 0)
                        break;
                    for (int i = 0; i < _len; i++)
                    {
                        _position = _validChar.IndexOf(_formu[i]);
                        if (_position >= 0)
                        {
                            _curP = 1;
                            if (_isVar)
                            {
                                _curP = i;
                                _str = _formu.Substring(0, i);
                                if (_strList.IndexOf(_str) < 0)
                                {
                                    if (_strList.Length > 0)
                                        _strList += ";";
                                    _strList += _str;
                                }
                                if (_backList.Length > 0)
                                    _backList += ";";
                                _backList += _str;
                            }
                            _formu = _formu.Substring(_curP, _formu.Length - _curP);
                            break;
                        }
                        else
                        {
                            _isVar = true;
                            if (i == _len)
                            {
                                _str = _formu.Substring(0, i);
                                if (_strList.IndexOf(_str) < 0)
                                {
                                    if (_strList.Length > 0)
                                        _strList += ";";
                                    _strList += _str;
                                }
                                if (_backList.Length > 0)
                                    _backList += ";";
                                _backList += _str;
                                _formu = "";
                                break;
                            }
                        }
                    }
                }
                formuList = _strList.Split(';');
                if (_formula.IndexOf("主单位数量") >= 0)
                    _formula = _formula.Replace("主单位数量", QTY.ToString());
                if (_formula.IndexOf("副单位数量") >= 0)
                    _formula = _formula.Replace("副单位数量", QTY.ToString());
                if (formuList.Length == 1 && formuList[0] == "主单位数量")
                    RESULT = CommonHelper.Calculator(_formula);
                else if (formuList.Length == 1 && formuList[0] == "副单位数量")
                    RESULT = CommonHelper.Calculator(_formula);
                else
                {
                    showPage = true;
                    for (int i = 0; i < formuList.Length; i++)
                    {
                        if (formuList[i] != "主单位数量" && formuList[i] != "副单位数量")
                        {
                            if (this.Controls.Find("lbl" + i.ToString(), true).Length > 0)
                            {
                                CLabel _lblTemp = (CLabel)(this.Controls.Find("lbl" + i.ToString(), true)[0]);
                                _lblTemp.Text = formuList[i];
                                _lblTemp.Visible = true;
                            }
                            if (this.Controls.Find("txt" + i.ToString(), true).Length > 0)
                            {
                                CTextBox _txtTemp = (CTextBox)(this.Controls.Find("txt" + i.ToString(), true)[0]);
                                _txtTemp.Visible = true;
                                if (_dsPara != null && _dsPara.Tables.Count > 0 && _dsPara.Tables.Contains("FORMU_PARA"))
                                {
                                    DataRow[] _drArr = _dsPara.Tables["FORMU_PARA"].Select("PARA='" + formuList[i] + "'");
                                    if (_drArr.Length > 0)
                                        _txtTemp.Text = _drArr[0]["P_VALUE"].ToString();
                                    else
                                    {
                                        if (formuList[i] == "制造长度" && CD > 0)
                                            _txtTemp.Text = CD.ToString();
                                        if (formuList[i] == "制造宽度" && MF > 0)
                                            _txtTemp.Text = MF.ToString();
                                    }
                                }
                                else
                                {
                                    if (formuList[i] == "制造长度" && CD > 0)
                                        _txtTemp.Text = CD.ToString();
                                    if (formuList[i] == "制造宽度" && MF > 0)
                                        _txtTemp.Text = MF.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        public void GetQty()
        {
            GetFormula();
            CheckFormula(_formula);
        }

        private void CheckQTY_Load(object sender, EventArgs e)
        {
            Sunlike.Interface.IDataSetData _iDsData = Sunlike.RemotingManagement.CRemotingManager.GetDataSetDataObject("MZ.rem");
            System.Collections.Generic.Dictionary<string, object> _ht = new Dictionary<string, object>();
            _ht["GET_FORMU_PARA"] = "T";
            _ht["PRD_NO"] = Prd_No;
            _ht["PRD_MARK"] = Prd_Mark;
            _dsPara = _iDsData.GetDataSet(Sunlike.Global.CompHelper.Information.CompanyInfo.COMPNO, Sunlike.Global.LoginUserHelper.LOGINID, _ht);

            GetQty();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool _empty = false;
            bool _updFormuPara = false;

            if (txt0.Visible == true)
            {
                if (txt0.Text == "")
                    _empty = true;
                else
                {
                    DataRow[] _drArr = _dsPara.Tables["FORMU_PARA"].Select("PARA='" + lbl0.Text + "'");
                    if (_drArr.Length > 0)
                    {
                        if (_drArr[0]["P_VALUE"].ToString() != txt0.Text)
                        {
                            _drArr[0]["P_VALUE"] = txt0.Text;
                            _updFormuPara = true;
                        }
                    }
                    else
                    {
                        DataRow _drNew = _dsPara.Tables["FORMU_PARA"].NewRow();
                        _drNew["PRD_NO"] = Prd_No;
                        _drNew["PRD_MARK"] = Prd_Mark;
                        _drNew["BAT_NO"] = "";
                        _drNew["PARA"] = lbl0.Text;
                        _drNew["P_VALUE"] = txt0.Text;
                        _dsPara.Tables["FORMU_PARA"].Rows.Add(_drNew);

                        _updFormuPara = true;
                    }
                }
            }
            if (txt1.Visible == true)
            {
                if (txt1.Text == "")
                    _empty = true;
                else
                {
                    DataRow[] _drArr = _dsPara.Tables["FORMU_PARA"].Select("PARA='" + lbl1.Text + "'");
                    if (_drArr.Length > 0)
                    {
                        if (_drArr[0]["P_VALUE"].ToString() != txt1.Text)
                        {
                            _drArr[0]["P_VALUE"] = txt1.Text;
                            _updFormuPara = true;
                        }
                    }
                    else
                    {
                        DataRow _drNew = _dsPara.Tables["FORMU_PARA"].NewRow();
                        _drNew["PRD_NO"] = Prd_No;
                        _drNew["PRD_MARK"] = Prd_Mark;
                        _drNew["BAT_NO"] = "";
                        _drNew["PARA"] = lbl1.Text;
                        _drNew["P_VALUE"] = txt1.Text;
                        _dsPara.Tables["FORMU_PARA"].Rows.Add(_drNew);

                        _updFormuPara = true;
                    }
                }
            }
            if (txt2.Visible == true)
            {
                if (txt2.Text == "")
                    _empty = true;
                else
                {
                    DataRow[] _drArr = _dsPara.Tables["FORMU_PARA"].Select("PARA='" + lbl2.Text + "'");
                    if (_drArr.Length > 0)
                    {
                        if (_drArr[0]["P_VALUE"].ToString() != txt2.Text)
                        {
                            _drArr[0]["P_VALUE"] = txt2.Text;
                            _updFormuPara = true;
                        }
                    }
                    else
                    {
                        DataRow _drNew = _dsPara.Tables["FORMU_PARA"].NewRow();
                        _drNew["PRD_NO"] = Prd_No;
                        _drNew["PRD_MARK"] = Prd_Mark;
                        _drNew["BAT_NO"] = "";
                        _drNew["PARA"] = lbl2.Text;
                        _drNew["P_VALUE"] = txt2.Text;
                        _dsPara.Tables["FORMU_PARA"].Rows.Add(_drNew);

                        _updFormuPara = true;
                    }
                }
            }
            if (txt3.Visible == true)
            {
                if (txt3.Text == "")
                    _empty = true;
                else
                {
                    DataRow[] _drArr = _dsPara.Tables["FORMU_PARA"].Select("PARA='" + lbl3.Text + "'");
                    if (_drArr.Length > 0)
                    {
                        if (_drArr[0]["P_VALUE"].ToString() != txt3.Text)
                        {
                            _drArr[0]["P_VALUE"] = txt3.Text;
                            _updFormuPara = true;
                        }
                    }
                    else
                    {
                        DataRow _drNew = _dsPara.Tables["FORMU_PARA"].NewRow();
                        _drNew["PRD_NO"] = Prd_No;
                        _drNew["PRD_MARK"] = Prd_Mark;
                        _drNew["BAT_NO"] = "";
                        _drNew["PARA"] = lbl3.Text;
                        _drNew["P_VALUE"] = txt3.Text;
                        _dsPara.Tables["FORMU_PARA"].Rows.Add(_drNew);

                        _updFormuPara = true;
                    }
                }
            }
            if (txt4.Visible == true)
            {
                if (txt4.Text == "")
                    _empty = true;
                else
                {
                    DataRow[] _drArr = _dsPara.Tables["FORMU_PARA"].Select("PARA='" + lbl4.Text + "'");
                    if (_drArr.Length > 0)
                    {
                        if (_drArr[0]["P_VALUE"].ToString() != txt4.Text)
                        {
                            _drArr[0]["P_VALUE"] = txt4.Text;
                            _updFormuPara = true;
                        }
                    }
                    else
                    {
                        DataRow _drNew = _dsPara.Tables["FORMU_PARA"].NewRow();
                        _drNew["PRD_NO"] = Prd_No;
                        _drNew["PRD_MARK"] = Prd_Mark;
                        _drNew["BAT_NO"] = "";
                        _drNew["PARA"] = lbl4.Text;
                        _drNew["P_VALUE"] = txt4.Text;
                        _dsPara.Tables["FORMU_PARA"].Rows.Add(_drNew);

                        _updFormuPara = true;
                    }
                }
            }
            if (_empty)
            {
                CMessageBox.Show("数据不能为空。");
                return;
            }
            else if (_updFormuPara)
            {
                SunlikeDataSet _ds = new SunlikeDataSet();
                Sunlike.Interface.IDataSetData _iMz = Sunlike.RemotingManagement.CRemotingManager.GetDataSetDataObject("MZ.rem");
                Dictionary<string, object> _parameters = new Dictionary<string, object>();
                _parameters["UPDATE_FORMU_PARA"] = "T";
                _ds = _iMz.UpdateDataSet(Sunlike.Global.CompHelper.Information.CompanyInfo.COMPNO, Sunlike.Global.LoginUserHelper.LOGINID, this._dsPara, _parameters);
                if (!_ds.HasErrors)
                {
                    _dsPara.AcceptChanges();
                }
                else
                {
                    _dsPara.RejectChanges();
                }
            }

            string str1 = "";
            string str2 = "";
            int pos = -1;
            int len = 0;
            string[] strArr = _strList.Split(';');
            string[] backArr = _backList.Split(';');
            for (int i = 0; i < backArr.Length; i++)
            {
                pos = _formula.IndexOf(backArr[i]);
                len = backArr[i].Length;
                if (backArr[i] != "主单位数量" && backArr[i] != "副单位数量")
                {
                    str1 = _formula.Substring(0, pos);
                    str2 = _formula.Substring(pos + len, _formula.Length - pos - len);
                    for (int j = 0; j < strArr.Length; j++)
                    {
                        if (strArr[j] == backArr[i])
                        {
                            CTextBox _txtTemp = (CTextBox)(this.Controls.Find("txt" + j.ToString(), true)[0]);
                            if (_txtTemp.Text.Trim() != "")
                                _formula = str1 + _txtTemp.Text + str2;
                            break;
                        }
                    }
                }
            }
            //RESULT = Convert.ToDecimal(Calculate(_formula));
            RESULT = CommonHelper.Calculator(_formula);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}