using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace QRCodePrintTSB
{
    public class ToShiBaLIB_DLL
    {
        //300DPI
        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
                    string height, string readable, string rotation,
                    string narrow, string wide, string code);

        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
                        string rotation, string xmul, string ymul,
                        string text);

        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height,
                  string speed, string density,
                  string sensor, string vertical,
                  string offset);

        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
                        int rotation, int fontstyle, int fontunderline,
                        string szFaceName, string content);

        private static string dpi = GetPrintSetDPI();
        private static string printSet = ToShiBaLIB_DLL.GetPrintSet();
        private static string compType = GetCompanyType();


        public static void BoxPrint(BoxVO boxVo)
        {
            int curSerialNo = 0;
            if (int.TryParse(boxVo.SerialNo.Substring(8), out curSerialNo) && curSerialNo > 0)
            {
                for (var i = 1; i <= boxVo.PrintCount; i++)
                {
                    if (i > 1)
                    {
                        curSerialNo += 1;
                    }
                    boxVo.SerialNo = boxVo.SerialNo.Substring(0, boxVo.SerialNo.Length - 6) + curSerialNo.ToString("000000");
                    if (dpi == "200")
                        BoxPrint200(boxVo);
                    else if (dpi == "300")
                        BoxPrint300(boxVo);
                    else
                        BoxPrintNew(boxVo);
                }
                //SaveBoxSerialNo(curSerialNo);
                DbMannager.SaveDateSerailNo(boxVo.PDate, curSerialNo);
            }
        }

        public static void ProductPrint(ProductVO productVo)
        {
            int curSerialNo = 0;
            if (int.TryParse(productVo.SerialNo.Substring(8), out curSerialNo) && curSerialNo > 0)
            {
                for (var i = 1; i <= productVo.PrintCount; i++)
                {
                    if (i > 1)
                    {
                        curSerialNo += 1;
                    }
                    productVo.SerialNo = productVo.SerialNo.Substring(0, productVo.SerialNo.Length - 6) + curSerialNo.ToString("000000");
                    if (dpi == "200")
                        ProductPrint200(productVo);
                    else if (dpi == "300")
                        ProductPrint300(productVo);
                    else
                        ProductPrintNew(productVo);
                }
                //SaveBoxSerialNo(curSerialNo);
                DbMannager.SaveDateSerailNo(productVo.PDate, curSerialNo);
            }
        }

        public static void ZBPrint(ZhanbanVO zhanbanVo)
        {
            if (dpi == "200")
                ZBPrint200(zhanbanVo);
            else if (dpi == "300")
                ZBPrint300(zhanbanVo);
            else
                ZBPrintNew(zhanbanVo);
        }


        public static void ZBPrint300(ZhanbanVO zhanbanVo)
        {
            string _dateVal = zhanbanVo.PDate.Year + ""
                + ((zhanbanVo.PDate.Month < 10) ? ("0" + zhanbanVo.PDate.Month.ToString()) : zhanbanVo.PDate.Month.ToString()) 
                + "" + ((zhanbanVo.PDate.Day < 10) ? ("0" + zhanbanVo.PDate.Day.ToString()) : zhanbanVo.PDate.Day.ToString());

            string qrCode = string.Concat(new string[]
            {
                zhanbanVo.OrderNo,
                ",",
                zhanbanVo.CompNo,
                ",",
                zhanbanVo.PN,
                ",",
                zhanbanVo.Qty,
                ",",
                _dateVal,
                ",",
                zhanbanVo.DWGRev
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("100", "50", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            
            ToShiBaLIB_DLL.sendcommand("QRCODE 300,100,L,10,A,0,M2,S3,\"" + qrCode + "\"");
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public static void BoxPrint300(BoxVO boxVo)
        {
            string _dateVal = boxVo.PDate.Year
                + "" + ((boxVo.PDate.Month < 10) ? ("0" + boxVo.PDate.Month.ToString()) : boxVo.PDate.Month.ToString())
                + "" + ((boxVo.PDate.Day < 10) ? ("0" + boxVo.PDate.Day.ToString()) : boxVo.PDate.Day.ToString());
            
            string qrCode = string.Concat(new string[]
            {
                boxVo.OrderNo,
                ",",
                boxVo.CompNo,
                ",",
                boxVo.PN,
                ",",
                boxVo.Qty,
                ",",
                _dateVal,
                ",",
                boxVo.LotNo,
                ",",
                boxVo.SerialNo,
                ",",
                boxVo.DWGRev
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("105", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            

            ToShiBaLIB_DLL.windowsfont(100, 80, 60, 0, 0, 0, "标楷体", "订 单 号");
            ToShiBaLIB_DLL.windowsfont(400, 80, 60, 0, 0, 0, "标楷体", boxVo.OrderNo);
            ToShiBaLIB_DLL.windowsfont(100, 170, 60, 0, 0, 0, "标楷体", "供应商代码");
            ToShiBaLIB_DLL.windowsfont(400, 170, 60, 0, 0, 0, "标楷体", boxVo.CompNo);
            ToShiBaLIB_DLL.windowsfont(100, 260, 60, 0, 0, 0, "标楷体", "供应商料号");
            ToShiBaLIB_DLL.windowsfont(400, 260, 60, 0, 0, 0, "标楷体", boxVo.PN);
            ToShiBaLIB_DLL.windowsfont(100, 350, 60, 0, 0, 0, "标楷体", "客户料号");
            ToShiBaLIB_DLL.windowsfont(400, 350, 60, 0, 0, 0, "标楷体", boxVo.ModelNo);

            ToShiBaLIB_DLL.sendcommand("QRCODE 950,350,L,8,A,0,M2,S3,\"" + qrCode + "\"");
            ToShiBaLIB_DLL.windowsfont(980, 620, 60, 0, 0, 0, "标楷体", "ROHS+HF");

            ToShiBaLIB_DLL.windowsfont(100, 440, 60, 0, 0, 0, "标楷体", "重量（KG）");
            ToShiBaLIB_DLL.windowsfont(400, 440, 60, 0, 0, 0, "标楷体", boxVo.Qty);
            ToShiBaLIB_DLL.windowsfont(100, 530, 60, 0, 0, 0, "标楷体", "生产日期");
            ToShiBaLIB_DLL.windowsfont(400, 530, 60, 0, 0, 0, "标楷体", _dateVal);
            ToShiBaLIB_DLL.windowsfont(100, 620, 60, 0, 0, 0, "标楷体", "生产批号");
            ToShiBaLIB_DLL.windowsfont(400, 620, 60, 0, 0, 0, "标楷体", boxVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(100, 710, 60, 0, 0, 0, "标楷体", "追溯代码");
            ToShiBaLIB_DLL.windowsfont(400, 710, 60, 0, 0, 0, "标楷体", boxVo.SerialNo);
            ToShiBaLIB_DLL.windowsfont(100, 800, 60, 0, 0, 0, "标楷体", "湿度等级");
            ToShiBaLIB_DLL.windowsfont(400, 800, 60, 0, 0, 0, "标楷体", boxVo.Shidu);
            ToShiBaLIB_DLL.windowsfont(100, 890, 60, 0, 0, 0, "标楷体", "版    次");
            ToShiBaLIB_DLL.windowsfont(400, 890, 60, 0, 0, 0, "标楷体", boxVo.DWGRev);
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public static void ProductPrint300(ProductVO productVo)
        {

            string _dateVal = productVo.PDate.Year
                + "" + ((productVo.PDate.Month < 10) ? ("0" + productVo.PDate.Month.ToString()) : productVo.PDate.Month.ToString())
                + "" + ((productVo.PDate.Day < 10) ? ("0" + productVo.PDate.Day.ToString()) : productVo.PDate.Day.ToString());

            string qrCode = string.Concat(new string[]
            {
                productVo.CompNo,
                ",",
                productVo.PN,
                ",",
                productVo.Qty,
                ",",
                _dateVal,
                ",",
                productVo.LotNo,
                ",",
                productVo.SerialNo,
                ",",
                productVo.DWGRev
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("105", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();

            
            ToShiBaLIB_DLL.windowsfont(100, 100, 60, 0, 0, 0, "标楷体", "供应商代码");
            ToShiBaLIB_DLL.windowsfont(400, 100, 60, 0, 0, 0, "标楷体", productVo.CompNo);
            ToShiBaLIB_DLL.windowsfont(100, 200, 60, 0, 0, 0, "标楷体", "供应商料号");
            ToShiBaLIB_DLL.windowsfont(400, 200, 60, 0, 0, 0, "标楷体", productVo.PN);
            ToShiBaLIB_DLL.windowsfont(100, 300, 60, 0, 0, 0, "标楷体", "客户料号");
            ToShiBaLIB_DLL.windowsfont(400, 300, 60, 0, 0, 0, "标楷体", productVo.ModelNo);

            ToShiBaLIB_DLL.sendcommand("QRCODE 950,350,L,8,A,0,M2,S3,\"" + qrCode + "\"");
            ToShiBaLIB_DLL.windowsfont(980, 620, 60, 0, 0, 0, "标楷体", "ROHS+HF");

            ToShiBaLIB_DLL.windowsfont(100, 400, 60, 0, 0, 0, "标楷体", "重量（KG）");
            ToShiBaLIB_DLL.windowsfont(400, 400, 60, 0, 0, 0, "标楷体", productVo.Qty);
            ToShiBaLIB_DLL.windowsfont(100, 500, 60, 0, 0, 0, "标楷体", "生产日期");
            ToShiBaLIB_DLL.windowsfont(400, 500, 60, 0, 0, 0, "标楷体", _dateVal);
            ToShiBaLIB_DLL.windowsfont(100, 600, 60, 0, 0, 0, "标楷体", "生产批号");
            ToShiBaLIB_DLL.windowsfont(400, 600, 60, 0, 0, 0, "标楷体", productVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(100, 700, 60, 0, 0, 0, "标楷体", "追溯代码");
            ToShiBaLIB_DLL.windowsfont(400, 700, 60, 0, 0, 0, "标楷体", productVo.SerialNo);
            ToShiBaLIB_DLL.windowsfont(100, 800, 60, 0, 0, 0, "标楷体", "湿度等级");
            ToShiBaLIB_DLL.windowsfont(400, 800, 60, 0, 0, 0, "标楷体", productVo.Shidu);
            ToShiBaLIB_DLL.windowsfont(100, 900, 60, 0, 0, 0, "标楷体", "版次");
            ToShiBaLIB_DLL.windowsfont(400, 900, 60, 0, 0, 0, "标楷体", productVo.DWGRev);
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }


        public static void ZBPrint200(ZhanbanVO zhanbanVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = zhanbanVo.PDate.Year + "/" + ((zhanbanVo.PDate.Month < 10) ? ("0" + zhanbanVo.PDate.Month.ToString()) : zhanbanVo.PDate.Month.ToString()) + "/" + ((zhanbanVo.PDate.Day < 10) ? ("0" + zhanbanVo.PDate.Day.ToString()) : zhanbanVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string content = "PO：";
            string content2 = "P/N：";
            string content3 = "LOT NO：";
            string lblDate = "DATE：";
            string content5 = "QTY：";
            string content6 = "DWG REV：";
            string content7 = "Serial NO：";
            string str = string.Concat(new string[]
            {
                zhanbanVo.PN,
                "\\",
                zhanbanVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                zhanbanVo.Qty,
                "\\",
                zhanbanVo.DWGRev,
                "\\GP",
                zhanbanVo.SerialNo
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("100", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.sendcommand("DIRECTION 1,0");
            ToShiBaLIB_DLL.sendcommand("REFERENCE 0,0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(45, 45, 40, 0, 0, 0, "标楷体", zhanbanVo.CompName);
            ToShiBaLIB_DLL.windowsfont(65, 150, 40, 0, 0, 0, "标楷体", content);
            ToShiBaLIB_DLL.sendcommand("QRCODE 600,500,L,8,A,0,M2,S3,\"" + str + "\"");
            ToShiBaLIB_DLL.windowsfont(65, 220, 40, 0, 0, 0, "标楷体", content2);
            ToShiBaLIB_DLL.windowsfont(250, 220, 40, 0, 0, 0, "标楷体", zhanbanVo.PN);
            ToShiBaLIB_DLL.windowsfont(65, 285, 40, 0, 0, 0, "标楷体", content3);
            ToShiBaLIB_DLL.windowsfont(250, 285, 40, 0, 0, 0, "标楷体", zhanbanVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(65, 350, 40, 0, 0, 0, "标楷体", lblDate);
            ToShiBaLIB_DLL.windowsfont(250, 350, 40, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(65, 415, 40, 0, 0, 0, "标楷体", content5);
            ToShiBaLIB_DLL.windowsfont(250, 415, 40, 0, 0, 0, "标楷体", zhanbanVo.Qty);
            ToShiBaLIB_DLL.windowsfont(65, 480, 40, 0, 0, 0, "标楷体", content6);
            ToShiBaLIB_DLL.windowsfont(250, 480, 40, 0, 0, 0, "标楷体", zhanbanVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(65, 535, 40, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(250, 535, 40, 0, 0, 0, "标楷体", zhanbanVo.GPPrdName.Substring(0, 2) + zhanbanVo.SPC);
            ToShiBaLIB_DLL.windowsfont(65, 600, 40, 0, 0, 0, "标楷体", content7);
            ToShiBaLIB_DLL.barcode("250", "600", "128", "40", "2", "0", "2", "4", ("GP" + zhanbanVo.SerialNo));
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public static void BoxPrint200(BoxVO boxVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = boxVo.PDate.Year + "/" + ((boxVo.PDate.Month < 10) ? ("0" + boxVo.PDate.Month.ToString()) : boxVo.PDate.Month.ToString()) + "/" + ((boxVo.PDate.Day < 10) ? ("0" + boxVo.PDate.Day.ToString()) : boxVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string content = "PO：";
            string content2 = "P/N：";
            string content3 = "LOT NO：";
            string lblDate = "DATE：";
            string content5 = "QTY：";
            string content6 = "DWG REV：";
            string content7 = "Serial NO：";
            string str = string.Concat(new string[]
            {
                boxVo.PN,
                "\\",
                boxVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                boxVo.Qty,
                "\\",
                boxVo.DWGRev,
                "\\GP",
                boxVo.SerialNo
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("100", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.sendcommand("DIRECTION 1,0");
            ToShiBaLIB_DLL.sendcommand("REFERENCE 0,0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(45, 45, 40, 0, 0, 0, "标楷体", boxVo.CompName);
            ToShiBaLIB_DLL.windowsfont(65, 150, 40, 0, 0, 0, "标楷体", content);
            ToShiBaLIB_DLL.sendcommand("QRCODE 600,500,L,8,A,0,M2,S3,\"" + str + "\"");
            ToShiBaLIB_DLL.windowsfont(65, 220, 40, 0, 0, 0, "标楷体", content2);
            ToShiBaLIB_DLL.windowsfont(250, 220, 40, 0, 0, 0, "标楷体", boxVo.PN);
            ToShiBaLIB_DLL.windowsfont(65, 285, 40, 0, 0, 0, "标楷体", content3);
            ToShiBaLIB_DLL.windowsfont(250, 285, 40, 0, 0, 0, "标楷体", boxVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(65, 350, 40, 0, 0, 0, "标楷体", lblDate);
            ToShiBaLIB_DLL.windowsfont(250, 350, 40, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(65, 415, 40, 0, 0, 0, "标楷体", content5);
            ToShiBaLIB_DLL.windowsfont(250, 415, 40, 0, 0, 0, "标楷体", boxVo.Qty);
            ToShiBaLIB_DLL.windowsfont(65, 480, 40, 0, 0, 0, "标楷体", content6);
            ToShiBaLIB_DLL.windowsfont(250, 480, 40, 0, 0, 0, "标楷体", boxVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(65, 535, 40, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(250, 535, 40, 0, 0, 0, "标楷体", boxVo.GPPrdName.Substring(0, 2) + boxVo.SPC);
            ToShiBaLIB_DLL.windowsfont(65, 600, 40, 0, 0, 0, "标楷体", content7);
            ToShiBaLIB_DLL.barcode("250", "600", "128", "40", "2", "0", "2", "4", ("GP" + boxVo.SerialNo));
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public static void ProductPrint200(ProductVO productVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = productVo.PDate.Year + "/" + ((productVo.PDate.Month < 10) ? ("0" + productVo.PDate.Month.ToString()) : productVo.PDate.Month.ToString()) + "/" + ((productVo.PDate.Day < 10) ? ("0" + productVo.PDate.Day.ToString()) : productVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string content = "PO：";
            string content2 = "P/N：";
            string content3 = "LOT NO：";
            string content4 = "DATE：";
            string content5 = "QTY：";
            string content6 = "DWG REV：";
            string content7 = "Serial NO：";
            string _barcodetxt = productVo.GPPrdName + productVo.LotNo + productVo.SerialNo;//一维码
            string str = string.Concat(new string[]
            {
                productVo.PN,
                "\\",
                productVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                productVo.Qty,
                "\\",
                productVo.DWGRev,
                "\\",
                _barcodetxt
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("100", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.sendcommand("DIRECTION 1,0");
            ToShiBaLIB_DLL.sendcommand("REFERENCE 0,0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(65, 45, 40, 0, 0, 0, "标楷体", productVo.CompName);
            ToShiBaLIB_DLL.windowsfont(65, 150, 40, 0, 0, 0, "标楷体", content);
            ToShiBaLIB_DLL.sendcommand("QRCODE 600,500,L,8,A,0,M2,S3,\"" + str + "\"");
            ToShiBaLIB_DLL.windowsfont(65, 220, 40, 0, 0, 0, "标楷体", content2);
            ToShiBaLIB_DLL.windowsfont(250, 220, 40, 0, 0, 0, "标楷体", productVo.PN);
            ToShiBaLIB_DLL.windowsfont(65, 285, 40, 0, 0, 0, "标楷体", content3);
            ToShiBaLIB_DLL.windowsfont(250, 285, 40, 0, 0, 0, "标楷体", productVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(65, 350, 40, 0, 0, 0, "标楷体", content4);
            ToShiBaLIB_DLL.windowsfont(250, 350, 40, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(65, 415, 40, 0, 0, 0, "标楷体", content5);
            ToShiBaLIB_DLL.windowsfont(250, 415, 40, 0, 0, 0, "标楷体", productVo.Qty);
            ToShiBaLIB_DLL.windowsfont(65, 480, 40, 0, 0, 0, "标楷体", content6);
            ToShiBaLIB_DLL.windowsfont(250, 480, 40, 0, 0, 0, "标楷体", productVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(65, 535, 40, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(250, 535, 40, 0, 0, 0, "标楷体", productVo.GPPrdName.Substring(0, 2) + productVo.SPC);
            ToShiBaLIB_DLL.windowsfont(65, 600, 40, 0, 0, 0, "标楷体", content7);
            ToShiBaLIB_DLL.barcode("250", "600", "128", "40", "2", "0", "2", "4", _barcodetxt);

            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }



        public static void ZBPrintNew(ZhanbanVO zhanbanVo)
        {
            string _dateVal = zhanbanVo.PDate.Year + ""
                + ((zhanbanVo.PDate.Month < 10) ? ("0" + zhanbanVo.PDate.Month.ToString()) : zhanbanVo.PDate.Month.ToString())
                + "" + ((zhanbanVo.PDate.Day < 10) ? ("0" + zhanbanVo.PDate.Day.ToString()) : zhanbanVo.PDate.Day.ToString());

            string qrCode = string.Concat(new string[]
            {
                zhanbanVo.OrderNo,
                ",",
                zhanbanVo.CompNo,
                ",",
                zhanbanVo.PN,
                ",",
                zhanbanVo.Qty,
                ",",
                _dateVal,
                ",",
                zhanbanVo.DWGRev
            });

            string _ip = GetPrintSetIP();
            string _port = GetPrintSetPort();

            //建立连接
            IPAddress ipa = IPAddress.Parse(_ip);
            IPEndPoint ipe = new IPEndPoint(ipa, int.Parse(_port));
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect(ipe);


            StringBuilder str = new StringBuilder();
            str.Append("{D1000,0500,0900|}").Append('\n');
            str.Append("{C|}").Append('\n');
            
            
            str.Append("{XB00;0200,0200,T,L,10,A,0,M2=" + qrCode + "|}").Append('\n');//二维码
            

            str.Append("{XS;I,0001,0002C6111|}").Append('\n');

            byte[] b = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str.ToString());
            soc.Send(b);
            soc.Close();
        }

        public static void BoxPrintNew(BoxVO boxVo)
        {
            string _dateVal = boxVo.PDate.Year
                + "" + ((boxVo.PDate.Month < 10) ? ("0" + boxVo.PDate.Month.ToString()) : boxVo.PDate.Month.ToString())
                + "" + ((boxVo.PDate.Day < 10) ? ("0" + boxVo.PDate.Day.ToString()) : boxVo.PDate.Day.ToString());

            string qrCode = string.Concat(new string[]
            {
                boxVo.OrderNo,
                ",",
                boxVo.CompNo,
                ",",
                boxVo.PN,
                ",",
                boxVo.Qty,
                ",",
                _dateVal,
                ",",
                boxVo.LotNo,
                ",",
                boxVo.SerialNo,
                ",",
                boxVo.DWGRev
            });

            string _ip = GetPrintSetIP();
            string _port = GetPrintSetPort();
            
            //建立连接
            IPAddress ipa = IPAddress.Parse(_ip);
            IPEndPoint ipe = new IPEndPoint(ipa, int.Parse(_port));
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect(ipe);


            StringBuilder str = new StringBuilder();
            str.Append("{D1150,0950,1050|}").Append('\n');
            str.Append("{C|}").Append('\n');

            //str.Append("{LC;0700,0050,0830,0100,1,3,040|}").Append('\n');//椭圆
            //str.Append("{PC000;0730,0095,20,20,e,00,B,J0101=ROHS|}").Append('\n');

            //str.Append("{LC;0880,0050,0950,0100,1,3,000|}").Append('\n');//方框
            //str.Append("{PC000;0900,0095,20,20,e,00,B,J0101=HF|}").Append('\n');

            //str.Append("{XB00;0400,0180,9,1,04,0,0050,+0000000000,000,1,00=" + boxVo.OrderNo + "|}").Append('\n');//一维条形码


            str.Append("{PC000;0100,0100,20,20,e,00,B,J0101=订 单 号|}").Append('\n');
            str.Append("{PC000;0400,0100,20,20,e,00,B,J0101=" + boxVo.OrderNo + "|}").Append('\n');

            str.Append("{PC000;0100,0190,20,20,e,00,B,J0101=供应商代码|}").Append('\n');
            str.Append("{PC000;0400,0190,20,20,e,00,B,J0101=" + boxVo.CompNo + "|}").Append('\n');

            str.Append("{PC000;0100,0280,20,20,e,00,B,J0101=供应商料号|}").Append('\n');
            str.Append("{PC000;0400,0280,20,20,e,00,B,J0101=" + boxVo.PN + "|}").Append('\n');

            str.Append("{PC000;0100,0370,20,20,e,00,B,J0101=客户料号|}").Append('\n');
            str.Append("{PC000;0400,0370,20,20,e,00,B,J0101=" + boxVo.ModelNo + "|}").Append('\n');

            str.Append("{PC000;0100,0460,20,20,e,00,B,J0101=重量（KG）|}").Append('\n');
            str.Append("{PC000;0400,0460,20,20,e,00,B,J0101=" + boxVo.Qty + "|}").Append('\n');

            str.Append("{PC000;0100,0550,20,20,e,00,B,J0101=生产日期|}").Append('\n');
            str.Append("{PC000;0400,0550,20,20,e,00,B,J0101=" + _dateVal + "|}").Append('\n');


            //str.Append("{XB00;0750,0650,T,H,09,A,0,M1,K8=" + qrCode + "|}").Append('\n');//二维码

            str.Append("{XB00;0700,0400,T,L,08,A,0,M2=" + qrCode + "|}").Append('\n');//二维码
            str.Append("{PC000;0750,0700,20,20,e,00,B,J0101=ROHS+HF|}").Append('\n');

            str.Append("{PC000;0100,0640,20,20,e,00,B,J0101=生产批号|}").Append('\n');
            str.Append("{PC000;0400,0640,20,20,e,00,B,J0101=" + boxVo.LotNo + "|}").Append('\n');

            str.Append("{PC000;0100,0730,20,20,e,00,B,J0101=追溯代码|}").Append('\n');
            str.Append("{PC000;0400,0730,20,20,e,00,B,J0101=" + boxVo.SerialNo + "|}").Append('\n');
            
            str.Append("{PC000;0100,0820,20,20,e,00,B,J0101=湿度等级|}").Append('\n');
            str.Append("{PC000;0400,0820,20,20,e,00,B,J0101=" + boxVo.Shidu + "|}").Append('\n');
            
            str.Append("{PC000;0100,0910,20,20,e,00,B,J0101=版    次|}").Append('\n');
            str.Append("{PC000;0400,0910,20,20,e,00,B,J0101=" + boxVo.DWGRev + "|}").Append('\n');

            str.Append("{XS;I,0001,0002C6111|}").Append('\n');

            byte[] b = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str.ToString());
            soc.Send(b);
            soc.Close();
        }
        
        public static void ProductPrintNew(ProductVO productVo)
        {
            string _dateVal = productVo.PDate.Year
                + "" + ((productVo.PDate.Month < 10) ? ("0" + productVo.PDate.Month.ToString()) : productVo.PDate.Month.ToString())
                + "" + ((productVo.PDate.Day < 10) ? ("0" + productVo.PDate.Day.ToString()) : productVo.PDate.Day.ToString());

            string qrCode = string.Concat(new string[]
            {
                productVo.CompNo,
                ",",
                productVo.PN,
                ",",
                productVo.Qty,
                ",",
                _dateVal,
                ",",
                productVo.LotNo,
                ",",
                productVo.SerialNo,
                ",",
                productVo.DWGRev
            });

            string _ip = GetPrintSetIP();
            string _port = GetPrintSetPort();

            //建立连接
            IPAddress ipa = IPAddress.Parse(_ip);
            IPEndPoint ipe = new IPEndPoint(ipa, int.Parse(_port));
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect(ipe);


            StringBuilder str = new StringBuilder();
            str.Append("{D1050,0950,1050|}").Append('\n');
            str.Append("{C|}").Append('\n');

            //str.Append("{LC;0700,0050,0830,0100,1,3,040|}").Append('\n');//椭圆
            //str.Append("{PC000;0730,0095,20,20,e,00,B,J0101=ROHS|}").Append('\n');

            //str.Append("{LC;0880,0050,0950,0100,1,3,000|}").Append('\n');//方框
            //str.Append("{PC000;0900,0095,20,20,e,00,B,J0101=HF|}").Append('\n');

            //str.Append("{XB00;0400,0180,9,1,04,0,0050,+0000000000,000,1,00=" + boxVo.OrderNo + "|}").Append('\n');//一维条形码

            str.Append("{PC000;0100,0100,20,20,e,00,B,J0101=供应商代码|}").Append('\n');
            str.Append("{PC000;0400,0100,20,20,e,00,B,J0101=" + productVo.CompNo + "|}").Append('\n');

            str.Append("{PC000;0100,0200,20,20,e,00,B,J0101=供应商料号|}").Append('\n');
            str.Append("{PC000;0400,0200,20,20,e,00,B,J0101=" + productVo.PN + "|}").Append('\n');

            str.Append("{PC000;0100,0300,20,20,e,00,B,J0101=客户料号|}").Append('\n');
            str.Append("{PC000;0400,0300,20,20,e,00,B,J0101=" + productVo.ModelNo + "|}").Append('\n');

            str.Append("{PC000;0100,0400,20,20,e,00,B,J0101=重量（KG）|}").Append('\n');
            str.Append("{PC000;0400,0400,20,20,e,00,B,J0101=" + productVo.Qty + "|}").Append('\n');

            str.Append("{PC000;0100,0500,20,20,e,00,B,J0101=生产日期|}").Append('\n');
            str.Append("{PC000;0400,0500,20,20,e,00,B,J0101=" + _dateVal + "|}").Append('\n');


            //str.Append("{XB00;0750,0650,T,H,09,A,0,M1,K8=" + qrCode + "|}").Append('\n');//二维码

            str.Append("{XB00;0700,0350,T,L,08,A,0,M2=" + qrCode + "|}").Append('\n');//二维码
            str.Append("{PC000;0750,0600,20,20,e,00,B,J0101=ROHS+HF|}").Append('\n');

            str.Append("{PC000;0100,0600,20,20,e,00,B,J0101=生产批号|}").Append('\n');
            str.Append("{PC000;0400,0600,20,20,e,00,B,J0101=" + productVo.LotNo + "|}").Append('\n');

            str.Append("{PC000;0100,0700,20,20,e,00,B,J0101=追溯代码|}").Append('\n');
            str.Append("{PC000;0400,0700,20,20,e,00,B,J0101=" + productVo.SerialNo + "|}").Append('\n');

            str.Append("{PC000;0100,0800,20,20,e,00,B,J0101=湿度等级|}").Append('\n');
            str.Append("{PC000;0400,0800,20,20,e,00,B,J0101=" + productVo.Shidu + "|}").Append('\n');

            str.Append("{PC000;0100,0900,20,20,e,00,B,J0101=版    次|}").Append('\n');
            str.Append("{PC000;0400,0900,20,20,e,00,B,J0101=" + productVo.DWGRev + "|}").Append('\n');

            str.Append("{XS;I,0001,0002C6111|}").Append('\n');

            byte[] b = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str.ToString());
            soc.Send(b);
            soc.Close();
        }
        




        public static string GetPrintSet()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"PrintSetPath\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }

        /// <summary>
        /// DPI分为200（分公司为243）和300（总公司342）
        /// </summary>
        /// <returns></returns>
        public static string GetPrintSetDPI()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"PrintSetDPI\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }
        /// <summary>
        /// 1为分公司；否则为总公司
        /// </summary>
        /// <returns></returns>
        public static string GetCompanyType()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"CompanyType\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }

        /// <summary>
        /// IP
        /// </summary>
        /// <returns></returns>
        public static string GetPrintSetIP()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"PrintSetIP\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }
        
        /// <summary>
        /// PORT
        /// </summary>
        /// <returns></returns>
        public static string GetPrintSetPort()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"PrintSetPort\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }

        public static string GetBoxSerialNo(DateTime date)
        {
            //return GetBoxSerialDate() + GetBoxMaxSerialNo().ToString("000000");

            var dateCur = date.Year.ToString() + "" + ((date.Month < 10) ? ("0" + date.Month.ToString()) : date.Month.ToString())
                + "" + ((date.Day < 10) ? ("0" + date.Day.ToString()) : date.Day.ToString());

            var serialNo = DbMannager.GetMaxSerailNo(date).ToString("000000");
            return dateCur + serialNo;
        }

        #region ///
        //private static int GetBoxMaxSerialNo()
        //{
        //    GetBoxSerialDate();

        //    int result = 0;
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
        //    string xpath = "/configuration/appSettings/add[@key=\"BoxSerialNo\"]/@value";
        //    XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
        //    if (xmlNode != null)
        //    {
        //        result = Convert.ToInt32(xmlNode.InnerText);
        //    }
        //    result += 1;
        //    return result;
        //}
        //private static string GetBoxSerialDate()
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
        //    string xpath = "/configuration/appSettings/add[@key=\"BoxSerialDate\"]/@value";
        //    XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
        //    if (xmlNode != null)
        //    {
        //        var result = Convert.ToDateTime(xmlNode.InnerText);
        //        if (result.Date != DateTime.Today)
        //        {
        //            SaveBoxSerialNo(0, true);
        //            SaveBoxSerialDate();
        //        }
        //    }
        //    else
        //    {
        //        SaveBoxSerialNo(0, true);
        //        SaveBoxSerialDate();
        //    }
        //    var dateCur = DateTime.Today.Year.ToString() + "" + ((DateTime.Today.Month < 10) ? ("0" + DateTime.Today.Month.ToString()) : DateTime.Today.Month.ToString())
        //        + "" + ((DateTime.Today.Day < 10) ? ("0" + DateTime.Today.Day.ToString()) : DateTime.Today.Day.ToString());
        //    return dateCur;
        //}

        //private static bool SaveBoxSerialNo(int boxSerialNo, bool modifyEnforce = false)
        //{
        //    var key = "BoxSerialNo";
        //    try
        //    {
        //        int result = 0;
        //        XmlDocument xmlDocument = new XmlDocument();
        //        xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
        //        string xpath = "/configuration/appSettings/add[@key=\"BoxSerialNo\"]/@value";
        //        XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
        //        if (xmlNode != null)
        //        {
        //            result = Convert.ToInt32(xmlNode.InnerText);
        //        }

        //        if (result < boxSerialNo || modifyEnforce)
        //        {
        //            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //            if (config.AppSettings.Settings[key] != null)
        //                config.AppSettings.Settings[key].Value = boxSerialNo.ToString();
        //            else
        //                config.AppSettings.Settings.Add(key, boxSerialNo.ToString());
        //            config.Save(ConfigurationSaveMode.Modified);
        //            ConfigurationManager.RefreshSection("appSettings");
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private static bool SaveBoxSerialDate()
        //{
        //    var key = "BoxSerialDate";
        //    try
        //    {
        //        var dateCur = DateTime.Today.Year.ToString() + "-" + ((DateTime.Today.Month < 10) ? ("0" + DateTime.Today.Month.ToString()) : DateTime.Today.Month.ToString())
        //            + "-" + ((DateTime.Today.Day < 10) ? ("0" + DateTime.Today.Day.ToString()) : DateTime.Today.Day.ToString());

        //        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //        if (config.AppSettings.Settings[key] != null)
        //            config.AppSettings.Settings[key].Value = dateCur;
        //        else
        //            config.AppSettings.Settings.Add(key, dateCur);
        //        config.Save(ConfigurationSaveMode.Modified);
        //        ConfigurationManager.RefreshSection("appSettings");
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }

    public class DbMannager
    {
        public static OleDbConnection getConn()
        {
            var dataSourceUrl = System.Windows.Forms.Application.StartupPath;
            string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dataSourceUrl + "\\foconn.accdb";
            OleDbConnection tempconn = new OleDbConnection(connstr);
            return (tempconn);
        }

        public static DataSet GetData()
        {
            OleDbConnection conn = DbMannager.getConn();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string sqlstr = "select * from BAR_IDX";
            var mydataset = new DataSet();
            adapter.SelectCommand = new OleDbCommand(sqlstr, conn);
            adapter.Fill(mydataset, "QRCodeData");
            conn.Close();
            return mydataset;
        }

        public static DataSet GetDateSerailNo(DateTime date)
        {
            OleDbConnection conn = DbMannager.getConn();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string sqlstr = "select * from Date_SerailNo Where S_Date=#" + date.ToString("yyyy-MM-dd") + "#";
            var mydataset = new DataSet();
            adapter.SelectCommand = new OleDbCommand(sqlstr, conn);
            adapter.Fill(mydataset, "Date_SerailNo");
            conn.Close();
            conn.Dispose();
            return mydataset;
        }


        public static int GetMaxSerailNo(DateTime date)
        {
            OleDbConnection conn = DbMannager.getConn();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string sqlstr = "select * from Date_SerailNo Where S_Date=#" + date.ToString("yyyy-MM-dd") + "#";
            var mydataset = new DataSet();
            adapter.SelectCommand = new OleDbCommand(sqlstr, conn);
            adapter.Fill(mydataset, "Date_SerailNo");
            conn.Close();
            conn.Dispose();
            if (mydataset != null && mydataset.Tables.Count > 0 && mydataset.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(mydataset.Tables[0].Rows[0]["SerialNo"]) + 1;
            }
            return 1;
        }

        public static bool SaveDateSerailNo(DateTime date, int serialNo)
        {
            var mydataset = GetDateSerailNo(date);
            if (mydataset != null && mydataset.Tables.Count > 0 && mydataset.Tables[0].Rows.Count > 0)
            {
                OleDbConnection conn = DbMannager.getConn();
                try
                {
                    conn.Open();
                    string sqlstr = string.Format("Update Date_SerailNo Set SerialNo={1} where S_Date=#{0}#", date.ToString("yyyy-MM-dd"), serialNo);
                    OleDbCommand inst = new OleDbCommand(sqlstr, conn);
                    inst.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }
                return true;
            }
            else
            {
                OleDbConnection conn = DbMannager.getConn();
                try
                {
                    conn.Open();
                    string sqlstr = "Insert into Date_SerailNo (S_Date,SerialNo) values ('" + date.ToString("yyyy-MM-dd") + "'," + serialNo + ")";
                    OleDbCommand inst = new OleDbCommand(sqlstr, conn);
                    inst.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }
                return true;
            }
        }
    }

    /// <summary>
    /// 外箱条码
    /// </summary>
    public class BoxVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 固品供应商名称
        /// </summary>
        public string CompName
        {
            get;
            set;
        }
        /// <summary>
        /// 固品供应商代码
        /// </summary>
        public string CompNo { get; set; }
        /// <summary>
        /// 固品料号
        /// </summary>
        public string GPPrdName
        {
            get;
            set;
        }
        /// <summary>
        /// 富士康料号（供应商料号）
        /// </summary>
        public string PN
        {
            get;
            set;
        }
        /// <summary>
        /// 富士康型号（客户料号）
        /// </summary>
        public string ModelNo { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string SPC
        {
            get;
            set;
        }
        /// <summary>
        /// 总重
        /// </summary>
        public string Qty
        {
            get;
            set;
        }
        /// <summary>
        /// 批号
        /// </summary>
        public string LotNo
        {
            get;
            set;
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime PDate
        {
            get;
            set;
        }
        /// <summary>
        /// 追溯代码（流水号）
        /// </summary>
        public string SerialNo
        {
            get;
            set;
        }
        /// <summary>
        /// 湿度等级
        /// </summary>
        public string Shidu { get; set; }

        /// <summary>
        /// 版次
        /// </summary>
        public string DWGRev
        {
            get;
            set;
        }
        /// <summary>
        /// 打印张数
        /// </summary>
        public int PrintCount { get; set; }
    }

    /// <summary>
    /// 内箱条码
    /// </summary>
    public class ProductVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 固品供应商名称
        /// </summary>
        public string CompName
        {
            get;
            set;
        }
        /// <summary>
        /// 固品供应商代码
        /// </summary>
        public string CompNo { get; set; }
        /// <summary>
        /// 固品料号
        /// </summary>
        public string GPPrdName
        {
            get;
            set;
        }
        /// <summary>
        /// 富士康料号（供应商料号）
        /// </summary>
        public string PN
        {
            get;
            set;
        }
        /// <summary>
        /// 富士康型号（客户料号）
        /// </summary>
        public string ModelNo { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string SPC
        {
            get;
            set;
        }
        /// <summary>
        /// 总重
        /// </summary>
        public string Qty
        {
            get;
            set;
        }
        /// <summary>
        /// 批号
        /// </summary>
        public string LotNo
        {
            get;
            set;
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime PDate
        {
            get;
            set;
        }
        /// <summary>
        /// 追溯代码（流水号）
        /// </summary>
        public string SerialNo
        {
            get;
            set;
        }
        /// <summary>
        /// 湿度等级
        /// </summary>
        public string Shidu { get; set; }

        /// <summary>
        /// 版次
        /// </summary>
        public string DWGRev
        {
            get;
            set;
        }
        /// <summary>
        /// 打印张数
        /// </summary>
        public int PrintCount { get; set; }
    }



    /// <summary>
    /// 栈板条码
    /// </summary>
    public class ZhanbanVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 固品供应商名称
        /// </summary>
        public string CompName
        {
            get;
            set;
        }
        /// <summary>
        /// 固品供应商代码
        /// </summary>
        public string CompNo { get; set; }
        /// <summary>
        /// 固品料号
        /// </summary>
        public string GPPrdName
        {
            get;
            set;
        }
        /// <summary>
        /// 富士康料号（供应商料号）
        /// </summary>
        public string PN
        {
            get;
            set;
        }
        /// <summary>
        /// 富士康型号（客户料号）
        /// </summary>
        public string ModelNo { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string SPC
        {
            get;
            set;
        }
        /// <summary>
        /// 总重
        /// </summary>
        public string Qty
        {
            get;
            set;
        }
        /// <summary>
        /// 批号
        /// </summary>
        public string LotNo
        {
            get;
            set;
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime PDate
        {
            get;
            set;
        }
        /// <summary>
        /// 追溯代码（流水号）
        /// </summary>
        public string SerialNo
        {
            get;
            set;
        }
        /// <summary>
        /// 湿度等级
        /// </summary>
        public string Shidu { get; set; }

        /// <summary>
        /// 版次
        /// </summary>
        public string DWGRev
        {
            get;
            set;
        }
        /// <summary>
        /// 打印张数
        /// </summary>
        public int PrintCount { get; set; }
    }
}
