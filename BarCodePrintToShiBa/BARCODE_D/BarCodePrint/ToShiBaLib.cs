/*BAR_PRTNAME中TYPE_ID类别、IDX打印版式
    * 打印版式（LH\TS\BASEC,BASIC\SAMPLE\SAMLL\FOXCONN\OTHER\HQ\SBB）
    *      当类别为：0（板材）时：
    *          LH：列印料号（跟中类关联），即打印的【料号】资料来源于表BAR_PRTNAME中IDX_NO=中类的NAME字段；
    *          TS：特殊版（跟品号关联），即打印的【品名】资料来源于表BAR_PRTNAME中PRD_NO=品号的NAME字段；
    *          BASIC：标准版，即打印的【品名】资料来源于货品中类的上层中类代号（此处不做维护，因为标准版打印的信息全部来自系统）
    *      当类别为：2（箱码）时：
    *          BASIC：标准版，即打印的【品名】资料来源于货品中类的上层中类代号（此处不做维护，因为标准版打印的信息全部来自系统）
    *          SAMPLE：简约版（类似标准版）
    *          SMALL：列印编号（类似标准版）
    *          FOXCONN：富士康版（供应商：固定为【固品】可手工修改；品名固定为【片材】可手工修改；版次、富士康料号、单品重量：分别来源于货品基础资料自定义字段【BC_GP】、【LH_FOXCONN】、【QTY_PER_GP】；材质：来源于中类基础资料自定义字段【CZ_GP】；）
    *          OTHER：特殊版（跟品号关联），即打印的【品名】资料来源于表BAR_PRTNAME中PRD_NO=品号的NAME字段；
    *          SBB：塑柏碧版（跟中类关联），即打印的【产品编号】资料来源于表BAR_PRTNAME中IDX_NO=品号的NAME字段；
    *          HQ：瀚荃版（同特殊版）；
    */
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Net.Sockets;
using System.Net;

namespace BarCodePrintTSB
{
    public class ToShiBaLib
    {
        [DllImport("TSCLIB.DLL")]
        private static extern void openport(string PrinterName);
        [DllImport("TSCLIB.DLL")]
        private static extern void closeport();
        [DllImport("TSCLIB.DLL")]
        private static extern void sendcommand(string command);
        [DllImport("TSCLIB.DLL")]
        private static extern void setup(string LabelWidth, string LabelHeight, string Speed, string Density, string Sensor, string Vertical, string Offset);
        [DllImport("TSCLIB.DLL")]
        private static extern void downloadpcx(string Filename, string ImageName);
        [DllImport("TSCLIB.DLL")]
        private static extern void barcode(string X, string Y, string CodeType, string Height, string Readable, string rotation, string Narrow, string Wide, string Code);
        [DllImport("TSCLIB.DLL")]
        private static extern void printerfont(string X, string Y, string FontName, string rotation, string Xmul, string Ymul, string Content);
        [DllImport("TSCLIB.DLL")]
        private static extern void clearbuffer();
        [DllImport("TSCLIB.DLL")]
        private static extern void printlabel(string NumberOfSet, string NumberOfCopy);
        [DllImport("TSCLIB.DLL")]
        private static extern void formfeed();
        [DllImport("TSCLIB.DLL")]
        private static extern void nobackfeed();
        [DllImport("TSCLIB.DLL")]
        private static extern void windowsfont(Int32 X, Int32 Y, Int32 fontheight, Int32 rotation, Int32 fontstyle, Int32 fontunderline, string FaceName, string TextContent);

		/// <summary>
		/// 打印条码
		/// </summary>
		/// <param name="_dr">打印内容</param>
		/// <param name="_prdtType">条码类别(EX:0、板材；1、成品；2、外箱；3、对分)</param>
        /// <param name="_format">打印模板(EX:BASIC、标准版；FOXCONN、富士康)</param>
		public void barCodePrint(DataRow _dr, string _prdtType, string _format)
        {
            BarSet _bs = new BarSet();
            string _port = _bs.GetPrintSet(System.Windows.Forms.Application.ExecutablePath + ".config");
			if (_prdtType == "0")
            {
                #region 板材
                if (_format == "LH")
                {
                    //板材（列印料号）
                    string _bar_no = _dr["BARCODE"].ToString();
                    string _idx = _dr["LH"].ToString();
                    string _prdt = "料号： " + _idx;
                    //string _spc = "规格： " + _dr["PRD_SPC"].ToString();
                    string _bat_no = "批号： " + _dr["PRD_MARK"].ToString() + "-";
                    string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    openport(_port);//打开打印机
                    setup("100", "50", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    clearbuffer();
                    windowsfont(85, 30, 60, 0, 0, 0, "标楷体", _prdt);//品名
                    //windowsfont(85, 100, 60, 0, 0, 0, "标楷体", _spc);//规格
                    windowsfont(85, 170, 60, 0, 0, 0, "标楷体", _bat_no + _dr["INIT_NO"].ToString());//批号
                    barcode("180", "240", "128", "50", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    printerfont("180", "290", "3", "0", "1", "1", _bar_no.Replace(BarRole.TrimChar, "") + _dr["INIT_NO"].ToString());//条码文字
                    windowsfont(250, 330, 40, 0, 0, 0, "标楷体", _date);//日期
                    printlabel("1", "1");//设定打印卷标式数(set)；设定打印卷标份数(copy)
                    closeport();//关闭打印机
                }
                else if (_format == "TS")
                {
                    //板材（特殊版）
                    string _bar_no = _dr["BARCODE"].ToString();
                    string _idx = _dr["IDX_NAME"].ToString();
                    string _prdt = "品名： " + _idx;
                    string _spc = "规格： " + _dr["PRD_SPC"].ToString();
                    string _bat_no = "批号： " + _dr["PRD_MARK"].ToString() + "-";
                    string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    openport(_port);//打开打印机
                    setup("100", "50", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    clearbuffer();
                    windowsfont(85, 30, 60, 0, 0, 0, "标楷体", _prdt);//品名
                    windowsfont(85, 100, 60, 0, 0, 0, "标楷体", _spc);//规格
                    windowsfont(85, 170, 60, 0, 0, 0, "标楷体", _bat_no + _dr["INIT_NO"].ToString());//批号
                    barcode("180", "240", "128", "50", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    printerfont("180", "290", "3", "0", "1", "1", _bar_no.Replace(BarRole.TrimChar, "") + _dr["INIT_NO"].ToString());//条码文字
                    windowsfont(250, 330, 40, 0, 0, 0, "标楷体", _date);//日期
                    printlabel("1", "1");//设定打印卷标式数(set)；设定打印卷标份数(copy)
                    closeport();//关闭打印机
                }
                else
                {
                    //板材（标准版）
                    string _bar_no = _dr["BARCODE"].ToString();
                    //int _pageCount = 1;//板材数量
                    //if (_dr["PAGE_COUNT"].ToString() != "")
                    //    _pageCount = Convert.ToInt32(_dr["PAGE_COUNT"]);
                    string _idx = _dr["IDX_NAME"].ToString();
                    string _prdt = "品名： " + _idx;
                    string _spc = "规格： " + _dr["PRD_SPC"].ToString();
                    string _bat_no = "批号： " + _dr["PRD_MARK"].ToString() + "-";
                    string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    openport(_port);//打开打印机
                    setup("100", "50", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    clearbuffer();
                    windowsfont(85, 30, 60, 0, 0, 0, "标楷体", _prdt);//品名
                    windowsfont(85, 100, 60, 0, 0, 0, "标楷体", _spc);//规格
                    windowsfont(85, 170, 60, 0, 0, 0, "标楷体", _bat_no + _dr["INIT_NO"].ToString());//批号
                    barcode("180", "240", "128", "50", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    printerfont("180", "290", "3", "0", "1", "1", _bar_no.Replace(BarRole.TrimChar, "") + _dr["INIT_NO"].ToString());//条码文字
                    windowsfont(250, 330, 40, 0, 0, 0, "标楷体", _date);//日期
                    printlabel("1", "1");//设定打印卷标式数(set)；设定打印卷标份数(copy)
                    closeport();//关闭打印机
                }
                #endregion
            }
			if (_prdtType == "1")
			{
                #region 卷材条码
                string _curPos = _dr["INIT_NO"].ToString();//流水号
                string _bar_no = _dr["BARCODE"].ToString().Replace(BarRole.TrimChar, "") + _curPos;
                string _barCode = _dr["BARCODE"].ToString() + _curPos;
                string _prdt = _dr["IDX_NAME"].ToString();
                string _spc = _dr["PRD_SPC"].ToString();

				openport(_port);
				setup("70", "24", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
				clearbuffer();
				windowsfont(5, 10, 35, 0, 0, 0, "标楷体", _bar_no);
				windowsfont(5, 45, 35, 0, 0, 0, "标楷体", _spc);
				barcode("5", "105", "128", "40", "0", "0", "2", "4", _barCode); //条码指令
                printerfont("5", "155", "3", "0", "1", "1", _bar_no);
				printlabel("1", "1");
				closeport();
                #endregion
            }
            if (_prdtType == "2")
            {
                #region 箱条码
                if (_format == "BASIC")
                {
                    //箱条码（标准版）
                    //int _pageCount = 1;//箱量
                    //if (_dr["PAGE_COUNT"].ToString() != "")
                    //    _pageCount = Convert.ToInt32(_dr["PAGE_COUNT"]);
                    string _idx = _dr["BOX_IDX_NAME"].ToString();
                    string _bar_no = _dr["BARCODE"].ToString();//序列号
                    string _prdt = "品名： " + _idx;
                    string _qty = _dr["BOX_QTY"].ToString() + "卷";
                    string _spc = "规格： " + _dr["BOX_SPC"].ToString();
                    string _bat_no = "批号： " + _dr["BOX_BAT"].ToString();

                    openport(_port);
                    setup("100", "50", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    clearbuffer();
                    windowsfont(200, 20, 45, 0, 0, 0, "标楷体", _dr["COMP_NAME"].ToString());
                    windowsfont(100, 70, 30, 0, 0, 0, "标楷体", _dr["COMP_ENAME"].ToString());
                    windowsfont(50, 110, 40, 0, 0, 0, "标楷体", _prdt);
                    windowsfont(600, 110, 40, 0, 0, 0, "标楷体", _qty);
                    windowsfont(50, 160, 40, 0, 0, 0, "标楷体", _spc);
                    windowsfont(50, 210, 40, 0, 0, 0, "标楷体", _bat_no);
                    barcode("250", "250", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    printerfont("250", "290", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    windowsfont(30, 330, 30, 0, 0, 0, "标楷体", _dr["COMP_ADR"].ToString());
                    windowsfont(500, 315, 30, 0, 0, 0, "标楷体", "TEL： " + _dr["COMP_TEL"].ToString());
                    windowsfont(500, 345, 30, 0, 0, 0, "标楷体", "FAX： " + _dr["COMP_FAX"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                if (_format == "SAMPLE")
                {
                    //箱条码(简约版)
                    string _idx = _dr["BOX_IDX_NAME"].ToString();
                    string _bar_no = _dr["BARCODE"].ToString();//序列号
                    string _prdt = "品名： " + _idx;
                    string _qty = _dr["BOX_QTY"].ToString() + "卷";
                    string _spc = "规格： " + _dr["BOX_SPC"].ToString();
                    string _bat_no = "批号： " + _dr["BOX_BAT"].ToString();

                    openport(_port);
                    setup("100", "50", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    clearbuffer();
                    windowsfont(100, 50, 60, 0, 0, 0, "标楷体", _prdt);
                    windowsfont(700, 50, 60, 0, 0, 0, "标楷体", _qty);
                    windowsfont(100, 120, 60, 0, 0, 0, "标楷体", _spc);
                    windowsfont(100, 190, 60, 0, 0, 0, "标楷体", _bat_no);
                    barcode("250", "270", "128", "80", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    printerfont("250", "360", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                if (_format == "SMALL")
                {
                    //箱条码（列印编号）
                    string _bar_no = _dr["BARCODE"].ToString();//序列号
                    openport(_port);
                    setup("70", "24", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    clearbuffer();
                    barcode("40", "105", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    printerfont("40", "155", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                if (_format == "OTHER" || _format == "HQ")
                {
                    //箱条码（其他版）
                    //int _pageCount = 1;//箱量
                    //if (_dr["PAGE_COUNT"].ToString() != "")
                    //    _pageCount = Convert.ToInt32(_dr["PAGE_COUNT"]);
                    string _idx = _dr["BOX_IDX_NAME"].ToString();
                    string _bar_no = _dr["BARCODE"].ToString();//序列号
                    string _prdt = "品名： " + _idx;
                    string _qty = _dr["BOX_QTY"].ToString() + "卷";
                    string _spc = "规格： " + _dr["BOX_SPC"].ToString();
                    string _bat_no = "批号： " + _dr["BOX_BAT"].ToString();

                    openport(_port);
                    setup("100", "50", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    clearbuffer();
                    windowsfont(200, 20, 45, 0, 0, 0, "标楷体", _dr["COMP_NAME"].ToString());
                    windowsfont(100, 70, 30, 0, 0, 0, "标楷体", _dr["COMP_ENAME"].ToString());
                    windowsfont(50, 110, 40, 0, 0, 0, "标楷体", _prdt);
                    windowsfont(600, 110, 40, 0, 0, 0, "标楷体", _qty);
                    windowsfont(50, 160, 40, 0, 0, 0, "标楷体", _spc);
                    windowsfont(50, 210, 40, 0, 0, 0, "标楷体", _bat_no);
                    barcode("250", "250", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    printerfont("250", "290", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    windowsfont(30, 330, 30, 0, 0, 0, "标楷体", _dr["COMP_ADR"].ToString());
                    windowsfont(500, 315, 30, 0, 0, 0, "标楷体", "TEL： " + _dr["COMP_TEL"].ToString());
                    windowsfont(500, 345, 30, 0, 0, 0, "标楷体", "FAX： " + _dr["COMP_FAX"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                if (_format == "FOXCONN")
                {
                    //箱条码（富士康版）
                    barCodePrintNew(_dr);
                    //string _bar_no = _dr["BARCODE"].ToString();//序列号
                    //string _line11 = "供  应  商    " + _dr["FOX_SUP"].ToString(); string _line12 = "材　　　 质    " + _dr["FOX_CZ"].ToString();
                    //string _line21 = "品　　名    " + _dr["FOX_PRD_NAME"].ToString(); string _line22 = "规　　　 格    " + _dr["BOX_SPC"].ToString();
                    //string _line31 = "版　　次    " + _dr["FOX_BC"].ToString(); string _line32 = "Foxconn料号    " + _dr["FOX_LH"].ToString();
                    //string _line41 = "生产批号    " + _dr["FOX_BAT_MANU"].ToString(); string _line42 = "原  料 批  号    " + _dr["FOX_BAT_MAT"].ToString();
                    //string _qty = "数量(卷)      " + _dr["BOX_QTY"].ToString();
                    //string _weight = "重量(KG)    " + _dr["FOX_WEIGHT"].ToString();
                    //string _bat_no = "追溯批号    " + _dr["BOX_BAT"].ToString();

                    //openport(_port);
                    //setup("105", "95", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    //clearbuffer();
                    //windowsfont(50, 60, 40, 0, 0, 0, "标楷体", _line11);
                    //windowsfont(380, 60, 40, 0, 0, 0, "标楷体", _line12);

                    ////printerfont("50", "50", "1", "0", "1", "1", "-");
                    ////printerfont("50", "50", "1", "90", "1", "1", "-");
                    //windowsfont(50, 130, 40, 0, 0, 0, "标楷体", _line21);
                    //windowsfont(380, 130, 40, 0, 0, 0, "标楷体", _line22);
                    //windowsfont(50, 200, 40, 0, 0, 0, "标楷体", _line31);
                    //windowsfont(380, 200, 40, 0, 0, 0, "标楷体", _line32);
                    //windowsfont(50, 270, 40, 0, 0, 0, "标楷体", _line41);
                    //windowsfont(380, 270, 40, 0, 0, 0, "标楷体", _line42);
                    //windowsfont(50, 340, 40, 0, 0, 0, "标楷体", _qty);
                    //windowsfont(50, 410, 40, 0, 0, 0, "标楷体", _weight);
                    //windowsfont(50, 480, 40, 0, 0, 0, "标楷体", _bat_no);
                    //windowsfont(50, 550, 40, 0, 0, 0, "标楷体", "出货日期");
                    //barcode("250", "640", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    //printerfont("250", "680", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    //printlabel("1", "1");
                    //closeport();
                }
                if (_format == "SBB")
                {
                    //箱条码（其他版-产品编号）
                    //int _pageCount = 1;//箱量
                    //if (_dr["PAGE_COUNT"].ToString() != "")
                    //    _pageCount = Convert.ToInt32(_dr["PAGE_COUNT"]);
                    string _idx = _dr["BOX_IDX_NAME"].ToString();
                    string _bar_no = _dr["BARCODE"].ToString();//序列号
                    string _prdt = "品　　名： " + _idx;
                    string _qty = _dr["BOX_QTY"].ToString() + "卷";
                    string _spc = "产品编号： " + _dr["BOX_LH"].ToString();
                    string _bat_no = "批　　号： " + _dr["BOX_BAT"].ToString();
                    string _spcno = "P/O： " + _dr["SPC_NAME"].ToString();

                    openport(_port);
                    setup("100", "50", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                    clearbuffer();
                    windowsfont(200, 20, 45, 0, 0, 0, "标楷体", _dr["COMP_NAME"].ToString());
                    windowsfont(100, 70, 30, 0, 0, 0, "标楷体", _dr["COMP_ENAME"].ToString());
                    windowsfont(50, 110, 40, 0, 0, 0, "标楷体", _prdt);
                    windowsfont(600, 110, 40, 0, 0, 0, "标楷体", _qty);
                    windowsfont(50, 160, 40, 0, 0, 0, "标楷体", _spc);
                    windowsfont(50, 210, 40, 0, 0, 0, "标楷体", _bat_no);
                    windowsfont(350, 210, 40, 0, 0, 0, "标楷体", _spcno);
                    barcode("250", "250", "128", "40", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                    printerfont("250", "290", "3", "0", "1", "1", _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString());
                    windowsfont(30, 330, 30, 0, 0, 0, "标楷体", _dr["COMP_ADR"].ToString());
                    windowsfont(500, 315, 30, 0, 0, 0, "标楷体", "TEL： " + _dr["COMP_TEL"].ToString());
                    windowsfont(500, 345, 30, 0, 0, 0, "标楷体", "FAX： " + _dr["COMP_FAX"].ToString());
                    printlabel("1", "1");
                    closeport();
                }
                #endregion
            }
            if (_prdtType == "3")
            {
                #region 半成品
                string _bar_no = _dr["BARCODE"].ToString();
                string _idx = _dr["IDX_NAME"].ToString();
                string _prdt = "品名： " + _idx;
                string _spc = "规格： " + _dr["PRD_SPC"].ToString();
                string _bat_no = "批号： " + _dr["PRD_MARK"].ToString() + "-";
                string _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                openport(_port);//打开打印机
                setup("100", "50", "3", "12", "0", "2.5", "0");//定义标签的参数和机器参数
                clearbuffer();
                windowsfont(85, 30, 60, 0, 0, 0, "标楷体", _prdt);//品名
                windowsfont(85, 100, 60, 0, 0, 0, "标楷体", _spc);//规格
                windowsfont(85, 170, 60, 0, 0, 0, "标楷体", _bat_no + _dr["INIT_NO"].ToString().Substring(1));//批号
                barcode("180", "240", "128", "50", "0", "0", "2", "4", _bar_no + _dr["INIT_NO"].ToString()); //条码指令
                printerfont("180", "290", "3", "0", "1", "1", _bar_no.Replace(BarRole.TrimChar, "") + _dr["INIT_NO"].ToString());//条码文字
                windowsfont(250, 330, 40, 0, 0, 0, "标楷体", _date);//日期
                printlabel("1", "1");//设定打印卷标式数(set)；设定打印卷标份数(copy)
                closeport();//关闭打印机
                #endregion
            }
        }

        public void barCodePrintNew(DataRow _dr)
        {

            BarSet _bs = new BarSet();
            string fileName = System.Windows.Forms.Application.ExecutablePath + ".config";
            string _ip = _bs.GetPrintSet(fileName, "PrintSetIP");
            string _port = _bs.GetPrintSet(fileName, "PrintSetPort");

            String ipPort = "";
            //建立连接
            IPAddress ipa = IPAddress.Parse(_ip);
            IPEndPoint ipe = new IPEndPoint(ipa, int.Parse(_port));
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect(ipe);


            //string str= "hello,123456789,大家好! ";



            //箱条码（富士康版）
            string _bar_no = _dr["BARCODE"].ToString();//序列号
            string _line11 = "供 应 商   " + _dr["FOX_SUP"].ToString(); string _line12 = "材　　　 质   " + _dr["FOX_CZ"].ToString();
            string _line21 = "品　　名   " + _dr["FOX_PRD_NAME"].ToString(); string _line22 = "规　　　 格   " + _dr["BOX_SPC"].ToString();
            string _line31 = "版　　次   " + _dr["FOX_BC"].ToString(); string _line32 = "Foxconn料号 " + _dr["FOX_LH"].ToString();
            string _line41 = "生产批号   " + _dr["FOX_BAT_MANU"].ToString(); string _line42 = "原 料 批 号   " + _dr["FOX_BAT_MAT"].ToString();
            string _qty = "数量(卷)   " + _dr["BOX_QTY"].ToString();
            string _weight = "重量(KG)   " + _dr["FOX_WEIGHT"].ToString();
            string _bat_no = "追溯批号   " + _dr["BOX_BAT"].ToString();

            StringBuilder str = new StringBuilder();
            str.Append("{D1150,0950,1050|}").Append('\n');
            str.Append("{C|}").Append('\n');

            str.Append("{PC000;0950,0780,20,20,e,22,B,J0101=" + _line11 + "|}").Append('\n');
            str.Append("{PC000;0550,0780,20,20,e,22,B,J0101=" + _line12 + "|}").Append('\n');

            str.Append("{PC000;0950,0700,20,20,e,22,B,J0101=" + _line21 + "|}").Append('\n');
            str.Append("{PC000;0550,0700,20,20,e,22,B,J0101=" + _line22 + "|}").Append('\n');

            str.Append("{PC000;0950,0620,20,20,e,22,B,J0101=" + _line31 + "|}").Append('\n');
            str.Append("{PC000;0550,0620,20,20,e,22,B,J0101=" + _line32 + "|}").Append('\n');

            str.Append("{PC000;0950,0540,20,20,e,22,B,J0101=" + _line41 + "|}").Append('\n');
            str.Append("{PC000;0550,0540,20,20,e,22,B,J0101=" + _line42 + "|}").Append('\n');

            str.Append("{PC000;0950,0460,20,20,e,22,B,J0101=" + _qty + "|}").Append('\n');
            str.Append("{PC000;0950,0380,20,20,e,22,B,J0101=" + _weight + "|}").Append('\n');

            str.Append("{PC000;0950,0300,20,20,e,22,B,J0101=" + _bat_no + "|}").Append('\n');
            str.Append("{PC000;0950,0220,20,20,e,22,B,J0101=出货日期|}").Append('\n');

            str.Append("{XB00;0300,0100,9,1,04,0,0080,+0000000000,000,0,00=" + _bar_no + _dr["INIT_NO"].ToString() + "|}").Append('\n');
            
            str.Append("{PC000;0600,0060,15,15,e,22,B=" +  _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString() + "|}").Append('\n');

            str.Append("{XS;I,0001,0002C6111|}").Append('\n');

            byte[] b = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str.ToString());
            soc.Send(b);
            soc.Close();
        }
    }

}
