using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.VisualBasic;

namespace BarCodePrintTSB
{
	public class BarRole
	{
		#region 序列号编码原则
		/// <summary>
		/// 总长度
		/// </summary>
		public static int BarLen;
		/// <summary>
		/// 箱标签代号
		/// </summary>
		public static string BoxFlag = "";
		/// <summary>
		/// 箱条码识别位置
		/// </summary>
		public static int BoxPos;
		/// <summary>
		/// 货品起始位置
		/// </summary>
		public static int SPrdt;
		/// <summary>
		/// 货品截止位置
		/// </summary>
		public static int EPrdt;
		/// <summary>
		/// 特征起始位置
		/// </summary>
		public static int BPMark;
		/// <summary>
		/// 特征截止位置
		/// </summary>
		public static int EPMark;
		/// <summary>
		/// 流水号起始位置
		/// </summary>
		public static int BSn;
		/// <summary>
		/// 流水号截止位置
		/// </summary>
		public static int ESn;
		/// <summary>
		/// 结束字符ASCII码
		/// </summary>
		public static int EndChar = 13;
		/// <summary>
		/// 空白替代字符
		/// </summary>
		public static string TrimChar = "";
		/// <summary>
		/// 箱条码长度
		/// </summary>
		public static int BoxLen;
		/// <summary>
		/// 合法字符
		/// </summary>
		public static string LegalCh = "";
		/// <summary>
		/// 自动拆箱
		/// </summary>
		public static bool AutoUnbox = true;
		/// <summary>
		/// 箱量控管否
		/// </summary>
		public static bool ControlBoxQty = true;
		/// <summary>
		/// 登录用户所属部门
		/// </summary>
		public static string DEP = "";
		/// <summary>
		/// 登录用户
		/// </summary>
        public static string USR_NO = "";
        /// <summary>
        /// 当前帐套
        /// </summary>
        public static string COMP_NO = "";
        /// <summary>
        /// 制令单允许为空选项权限
        /// </summary>
        public static bool CHK_MONULL = false;
        /// <summary>
        /// 箱码规则
        /// </summary>
        public static string PAT = "";
        public static string DB_Language = "zh-tw";
        #endregion

        #region 序列号转入格式
        public class BarDoc
        {
            /// <summary>
            /// 序列号座落位置
            /// </summary>
            public static int BarPos = -1;
            /// <summary>
            /// 出货库位座落位置
            /// </summary>
            public static int WhPos = -1;
            /// <summary>
            /// 入货库位座落位置
            /// </summary>
            public static int Wh2Pos = 4;
            /// <summary>
            /// 客户座落位置
            /// </summary>
            public static int CusPos = -1;
            /// <summary>
            /// 批号座落位置
            /// </summary>
            public static int BatPos = -1;
            /// <summary>
            /// 分隔字符
            /// </summary>
            public static char SplitChar;
        }
        #endregion

		#region 构造函数
		public BarRole()
		{
		}
		#endregion

        public static string ConvertToDBLanguage(string _txt)
        {
            if (DB_Language.ToLower() == "zh-cn")
            {
                return Strings.StrConv(_txt, VbStrConv.SimplifiedChinese, 2);
            }
            else
            {
                return Strings.StrConv(_txt, VbStrConv.TraditionalChinese, 2);
            }
        }

		#region 取得序列号编码原则，放到BarRole的静态变量中
		/// <summary>
		/// 取得序列号编码原则，放到BarRole的静态变量中
		/// </summary>
		/// <returns></returns>
		public void GetBarRole()
		{
			onlineService.BarPrintServer _brs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
			_brs.UseDefaultCredentials = true;
			DataSet _ds = _brs.GetBarRole();
            if (_ds.Tables.Count > 0)
            {
                BarRole.BarLen = Convert.ToInt32(_ds.Tables[0].Rows[0]["BAR_LEN"]);
                BarRole.BoxFlag = _ds.Tables[0].Rows[0]["BOX_FLAG"].ToString();
                BarRole.BoxPos = Convert.ToInt32(_ds.Tables[0].Rows[0]["BOXPOS"]);
                BarRole.SPrdt = Convert.ToInt32(_ds.Tables[0].Rows[0]["S_PRDT"]);
                BarRole.EPrdt = Convert.ToInt32(_ds.Tables[0].Rows[0]["E_PRDT"]);
                if (_ds.Tables[0].Rows[0]["B_PMARK"].ToString() != "")
                    BarRole.BPMark = Convert.ToInt32(_ds.Tables[0].Rows[0]["B_PMARK"]);
                if (_ds.Tables[0].Rows[0]["E_PMARK"].ToString() != "")
                    BarRole.EPMark = Convert.ToInt32(_ds.Tables[0].Rows[0]["E_PMARK"]);
                BarRole.BSn = Convert.ToInt32(_ds.Tables[0].Rows[0]["B_SN"]);
                BarRole.ESn = Convert.ToInt32(_ds.Tables[0].Rows[0]["E_SN"]);
                BarRole.EndChar = Convert.ToInt32(_ds.Tables[0].Rows[0]["END_CHAR"]);
                BarRole.TrimChar = _ds.Tables[0].Rows[0]["TRIM_CHAR"].ToString();
                if (_ds.Tables[0].Rows[0]["BOX_LEN"].ToString() != "")
                    BarRole.BoxLen = Convert.ToInt32(_ds.Tables[0].Rows[0]["BOX_LEN"]);
                BarRole.LegalCh = _ds.Tables[0].Rows[0]["LEGAL_CH"].ToString();
                BarRole.DB_Language = _ds.ExtendedProperties["DB_LANGUAGE"].ToString();
            }
		}
        #endregion

        #region 取得序列号转入格式
        /// <summary>
        /// 取得序列号转入格式
        /// </summary>
        /// <returns></returns>
        public bool GetBarDoc()
        {
            try
            {
                onlineService.BarPrintServer _brs = new global::BarCodePrintTSB.onlineService.BarPrintServer();
                _brs.UseDefaultCredentials = true;
                DataSet _ds = _brs.GetBarDoc();
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    DataRow _dr = _ds.Tables[0].Rows[0];
                    BarRole.BarDoc.BarPos = Convert.ToInt32(_dr["BAR_POS"]);
                    if (_dr["WH_POS"].ToString() != "")
                    {
                        BarRole.BarDoc.WhPos = Convert.ToInt32(_dr["WH_POS"]);
                    }
                    if (_dr["WH2_POS"].ToString() != "")
                    {
                        BarRole.BarDoc.Wh2Pos = Convert.ToInt32(_dr["WH2_POS"]);
                    }
                    if (_dr["CUS_POS"].ToString() != "")
                    {
                        BarRole.BarDoc.CusPos = Convert.ToInt32(_dr["CUS_POS"]);
                    }
                    if (_dr["BAT_POS"].ToString() != "")
                    {
                        BarRole.BarDoc.BatPos = Convert.ToInt32(_dr["BAT_POS"]);
                    }
                    BarRole.BarDoc.SplitChar = Convert.ToChar(Convert.ToInt32(_dr["SPLIT_CHAR"]));
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion



        #region 计算器
        /// <summary>
        /// 计算器
        /// </summary>
        /// <param name="exp">计算表达式</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1818:DoNotConcatenateStringsInsideLoops")]
        public static decimal Calculator(string exp)
        {
            ArrayList _al = new ArrayList();
            ExpInn _expInn = new ExpInn();
            _expInn.Tag = "";
            _expInn.Expression = exp;
            _expInn.Value = 0;
            _al.Add(_expInn);
            bool _ngt = false;
            bool _finish = false;
            int _tag = 0;
            //去除括号
            while (!_finish)
            {
                _finish = true;
                for (int i = 0; i < _al.Count; i++)
                {
                    ExpInn _inn = (ExpInn)_al[i];
                    string _innExp = _inn.Expression;
                    if (_innExp.IndexOf("(") >= 0)
                    {
                        _finish = false;
                        //取出"（"右边的字符
                        string _exp = _innExp.Substring(_innExp.IndexOf("(") + 1, _innExp.Length - _innExp.IndexOf("(") - 1);
                        string _expTemp = _exp;
                        //得到"（"个数
                        int _count = _expTemp.Substring(0, _expTemp.IndexOf(")")).Length - _expTemp.Substring(0, _expTemp.IndexOf(")")).Replace("(", "").Length;
                        //得到匹配的"）"位置
                        while (_count > 0)
                        {
                            _expTemp = "*" + _expTemp.Remove(_expTemp.IndexOf(")"), 1);
                            _count--;
                        }
                        ExpInn _expNew = new ExpInn();
                        _expNew.Tag = "E" + _tag++ + "E";
                        _expNew.Expression = _exp.Substring(0, _expTemp.IndexOf(")"));
                        _expNew.Value = 0;

                        _inn.Expression = _innExp.Replace("(" + _expNew.Expression + ")", _expNew.Tag);
                        _al[i] = _inn;

                        _al.Insert(i, _expNew);

                        break;
                    }
                }
            }
            //拆分简单表达式
            _finish = false;
            while (!_finish)
            {
                _finish = true;
                for (int i = 0; i < _al.Count; i++)
                {
                    ExpInn _inn = (ExpInn)_al[i];
                    string _innExp = _inn.Expression;
                    if ((_innExp.IndexOf("+") >= 0 || _innExp.IndexOf("-") >= 0)
                        && (_innExp.IndexOf("*") >= 0 || _innExp.IndexOf("/") >= 0))
                    {
                        string _opt = "";
                        if (_innExp.IndexOf("*") >= 0)
                        {
                            _opt = "*";
                        }
                        else
                        {
                            _opt = "/";
                        }
                        string _exp = _opt;
                        //合并前
                        bool _isFind = false;
                        for (int j = _innExp.IndexOf(_opt) - 1; j >= 0; j--)
                        {
                            if (_innExp[j] != '*' && _innExp[j] != '/' && _innExp[j] != '+' && _innExp[j] != '-')
                            {
                                _exp = _innExp[j].ToString() + _exp;
                                _isFind = true;
                            }
                            else if (_isFind)
                            {
                                break;
                            }
                        }
                        //合并后
                        _isFind = false;
                        foreach (char c in _innExp.Substring(_innExp.IndexOf(_opt) + 1, _innExp.Length - _innExp.IndexOf(_opt) - 1))
                        {
                            if (c != '*' && c != '/' && c != '+' && c != '-')
                            {
                                _exp += c.ToString();
                                _isFind = true;
                            }
                            else if (_isFind)
                            {
                                break;
                            }
                        }
                        ExpInn _expNew = new ExpInn();
                        _expNew.Tag = "E" + _tag++ + "E";
                        _expNew.Expression = _exp;
                        _expNew.Value = 0;

                        _inn.Expression = _inn.Expression.Replace(_exp, _expNew.Tag);
                        _al[i] = _inn;

                        _al.Insert(i, _expNew);
                    }
                }
            }
            //开始计算
            for (int i = 0; i < _al.Count; i++)
            {
                ExpInn _inn = (ExpInn)_al[i];
                string _exp = _inn.Expression;
                while (_exp.IndexOf("E") >= 0)
                {
                    string _tagInExp = "E";
                    foreach (char c in _exp.Substring(_exp.IndexOf("E") + 1, _exp.Length - _exp.IndexOf("E") - 1))
                    {
                        if (c != '+' && c != '-' && c != '*' && c != '/')
                        {
                            _tagInExp += c.ToString();
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int j = 0; j < i; j++)
                    {
                        ExpInn _inn1 = (ExpInn)_al[j];
                        if (_inn1.Tag == _tagInExp)
                        {
                            _exp = _exp.Replace(_tagInExp, _inn1.Value.ToString());
                            break;
                        }
                    }
                }

                decimal _ans = Calc(_exp);
                if (_ans < 0)
                {
                    _ngt = !_ngt;
                    _ans = Math.Abs(_ans);
                }
                _inn.Value = _ans;
                _al[i] = _inn;
            }
            if (_ngt)
            {
                return ((ExpInn)_al[_al.Count - 1]).Value * -1;
            }
            return ((ExpInn)_al[_al.Count - 1]).Value;
        }
        public static decimal Calc(string exp)
        {
            try
            {
                ArrayList _alOpt = new ArrayList();
                ArrayList _alNum = new ArrayList();
                StringBuilder _exp = new StringBuilder();
                foreach (char c in exp)
                {
                    if (c != '+' && c != '-' && c != '*' && c != '/')
                    {
                        _exp.Append(c.ToString());
                    }
                    else
                    {
                        _alOpt.Add(c.ToString());
                        _alNum.Add(_exp.ToString());
                        _exp = new StringBuilder();
                    }
                }
                _alNum.Add(_exp.ToString());
                decimal _result = Convert.ToDecimal(_alNum[0]);
                for (int i = 1; i < _alNum.Count; i++)
                {
                    if (_alOpt[i - 1].ToString() == "+")
                    {
                        _result += Convert.ToDecimal(_alNum[i]);
                    }
                    else if (_alOpt[i - 1].ToString() == "-")
                    {
                        _result -= Convert.ToDecimal(_alNum[i]);
                    }
                    else if (_alOpt[i - 1].ToString() == "*")
                    {
                        _result *= Convert.ToDecimal(_alNum[i]);
                    }
                    else if (_alOpt[i - 1].ToString() == "/")
                    {
                        _result /= Convert.ToDecimal(_alNum[i]);
                    }
                }
                return _result;
            }
            catch
            {
                throw new Exception("表达式错误！");
            }
        }
        public struct ExpInn
        {
            private string mTag;
            public string Tag
            {
                get { return mTag; }
                set { mTag = value; }
            }
            private string mExpression;
            public string Expression
            {
                get { return mExpression; }
                set { mExpression = value; }
            }
            private decimal mValue;
            public decimal Value
            {
                get { return mValue; }
                set { mValue = value; }
            }
        }
        #endregion
	}
}
